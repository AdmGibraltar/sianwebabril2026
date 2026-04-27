using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaEntidad;
using CapaNegocios;
using Telerik.Web.UI;
using System.Collections;

namespace SIANWEB
{
    public partial class ProEmbarques : System.Web.UI.Page
    {
        #region Variables

        public List<ProEmbarqueDet> ListDet
        {
            get { return Session["ListDet" + Session.SessionID] as List<ProEmbarqueDet>; }
            set { Session["ListDet" + Session.SessionID] = value; }

        }

        public bool bndSave { get; set; } = true;

        #endregion Variables

        #region Eventos
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    case "RebindGrid":
                        this.rgDetalles.Rebind();
                        break;
                    case "panel":
                        Unit altura = (Unit)(Convert.ToDouble(HiddenHeight.Value) - 100);
                        RadPageViewDetalles.Height = altura;
                        RadPane1.Height = altura;
                        RadPane1.Width = RadPageViewDGenerales.Width;
                        RadSplitter1.Height = altura;
                        RadPageViewDGenerales.Height = altura;
                        RadSplitter2.Height = altura;
                        RadPane2.Height = altura;
                        RadPane2.Width = RadPageViewDGenerales.Width;
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    RAM1.ResponseScripts.Add("RefreshParentPage()");
                }
                else
                {
                    if (!Page.IsPostBack)
                    {

                        //obtener valores desde la URL
                        //int Id_Emb = Convert.ToInt32(Page.Request.QueryString["Id_Emb"]);
                        //int Id_Cd = Convert.ToInt32(Page.Request.QueryString["Id_Cd"]);
                        //int Id_Emp = Convert.ToInt32(Page.Request.QueryString["Id_Emp"]);
                        //string embModificable = Page.Request.QueryString["embModificable"].ToString();

                        Inicializar();

                        double ancho = 0;
                        foreach (GridColumn gc in rgDetalles.Columns)
                        {
                            if (gc.Display && gc.Visible)
                            {
                                ancho = ancho + gc.HeaderStyle.Width.Value;
                            }
                        }
                        rgDetalles.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        rgDetalles.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgDetalles.DataSource = ListDet;
                }

            }
            catch (Exception)
            {
                this.Alerta("Error al cargar el grid de facturas en embarque");
            }
        }
        protected void rgDetalles_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "InitInsert")
            {
                if (!validarCamposDetalle())
                {
                    e.Canceled = true;
                    return;
                }
                if (!validarFecha(this.dpFecha))
                {
                    e.Canceled = true;
                    return;
                }
            }
            if (e.CommandName == "Edit")
            {
                this.Alerta("No puede editar esta inforación");
                return;

            }
        }
        protected void rgDetalles_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
            {
                GridEditableItem editItem = (GridEditableItem)e.Item;
                this.rgDetalles.Columns.FindByUniqueName("EditCommandColumn").Visible = true;
                //RadComboBox rcmbFac = editItem.FindControl("rcbSerie") as RadComboBox;
                RadNumericTextBox txtFact = editItem.FindControl("txtFact") as RadNumericTextBox;
                //new CN__Comun().LlenaCombo(sesion.Id_Emp, sesion.Id_Cd_Ver, sesion.Emp_Cnx, "spConsecutivos_Combo", ref rcmbFac);

                Control insertbtn = (Control)editItem.FindControl("PerformInsertButton");
                if (insertbtn != null)
                {
                    txtFact.Enabled = true;
                    (e.Item.FindControl("txtFact") as RadNumericTextBox).Enabled = true;
                }
            }
            else
                if (e.Item.IsDataBound)
            {
                GridDataItem item = (GridDataItem)e.Item;
                item["EditCommandColumn"].Controls[0].Visible = false;
            }
            //TODO: AGREGAR PARA PONER EL FOCUS
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem form = (GridEditableItem)e.Item;
                RadNumericTextBox dataField = (RadNumericTextBox)form["Id_Doc"].FindControl("txtFact");
                dataField.Focus();
            }
        }
        protected void rgDetalles_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                List<ProEmbarqueDet> List = (List<ProEmbarqueDet>)Session["ListDet" + Session.SessionID];
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                GridEditableItem editedItem = e.Item as GridEditableItem;

                RadNumericTextBox txtfac = editedItem.FindControl("txtFact") as RadNumericTextBox;
                Label LblId_Cte = editedItem.FindControl("LblId_Cte") as Label;
                RadComboBox CmbTipo = editedItem.FindControl("CmbTipo") as RadComboBox;
                RadTextBox TxtCteNombre = editedItem.FindControl("txtCte") as RadTextBox;
                RadNumericTextBox TxtImporte = editedItem.FindControl("txtImporte") as RadNumericTextBox;
                if (txtfac.Text == string.Empty)
                {
                    this.Alerta("Seleccione primero un documento");
                    this.rgDetalles.Columns.FindByUniqueName("EditCommandColumn").Visible = true;
                    return;
                }
                else
                {
                    ProEmbarqueDet emb = new ProEmbarqueDet();
                    emb.Emb_Tipo = int.Parse(CmbTipo.SelectedValue);
                    emb.Emb_TipoStr = CmbTipo.Text;
                    emb.Id_Doc = int.Parse(txtfac.Text);
                    emb.Cte_NomComercial = TxtCteNombre.Text;
                    emb.Doc_Importe = double.Parse(TxtImporte.Text);
                    emb.Id_Cte = int.Parse(LblId_Cte.Text);
                    emb.Id_Emp = sesion.Id_Emp;
                    emb.Id_Cd = sesion.Id_Cd_Ver;
                    List.Add(emb);

                    Session["ListDet" + Session.SessionID] = List;
                    this.rgDetalles.Columns.FindByUniqueName("EditCommandColumn").Visible = false;
                    rgDetalles.Rebind();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                e.Canceled = true;
            }
        }
        protected void rgDetalles_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                ProEmbarqueDet emb = new ProEmbarqueDet();

                emb.Emb_Tipo = int.Parse((editedItem["Emb_Tipo"].FindControl("lblEmbTipo") as Label).Text);
                emb.Id_Doc = int.Parse((editedItem["Id_Doc"].FindControl("FacLabel") as Label).Text);
                ListDet.Remove(ListDet.Where(E => E.Emb_Tipo == emb.Emb_Tipo && E.Id_Doc == emb.Id_Doc).ToList()[0]);

                rgDetalles.Rebind();
            }
            catch
            {
                this.Alerta("Error al quitar la factura del listado <br/>");
            }
        }
        protected void txtFac_TextChanged(object sender, EventArgs e)
        {
            try
            {

                RadNumericTextBox txtfac = sender as RadNumericTextBox;
                RadComboBox CmbTipo = (txtfac.Parent.FindControl("CmbTipo") as RadComboBox);
                RadTextBox TxtCteNombre = (txtfac.Parent.FindControl("txtCte") as RadTextBox);
                RadNumericTextBox TxtImporte = (txtfac.Parent.FindControl("txtImporte") as RadNumericTextBox);
                Label LblId_Cte = (txtfac.Parent.FindControl("LblId_Cte") as Label);

                if (ListDet.Where(Embarque => Embarque.Emb_Tipo == int.Parse(CmbTipo.SelectedValue) && Embarque.Id_Doc == int.Parse(txtfac.Text)).ToList().Count > 0)
                {
                    AlertaFocus("El documento ya fue capturado", txtfac.ClientID);
                    TxtCteNombre.Text = "";
                    TxtImporte.Text = "";
                    return;
                }

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ProEmbarqueDet emb = new ProEmbarqueDet();
                CN_ProEmbarque cn_emb = new CN_ProEmbarque();

                emb.Emb_Tipo = int.Parse(CmbTipo.SelectedValue);
                emb.Id_Emp = sesion.Id_Emp;
                emb.Id_Cd = sesion.Id_Cd_Ver;
                emb.Id_Doc = int.Parse(txtfac.Text);
                emb.Id_Emb = int.Parse(txtEmbarque.Text);

                try
                {
                    cn_emb.ProEmbarque_ConsultaDocumento(ref emb, sesion.Emp_Cnx);

                }
                catch (Exception ex)
                {

                    txtfac.Text = "";
                    TxtCteNombre.Text = "";
                    TxtImporte.Text = "";
                    AlertaFocus(ex.Message, txtfac.ClientID);
                    return;
                }

                CmbTipo.Enabled = false;
                LblId_Cte.Text = emb.Id_Cte.ToString();
                TxtCteNombre.Text = emb.Cte_NomComercial;
                TxtImporte.Text = emb.Doc_Importe.ToString();


            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            string accionError = string.Empty;
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;

                if (btn.CommandName == "save")
                {
                    if (Page.IsValid)
                    {
                        if (Request.QueryString["id"] == "0")
                        {
                            btn.Enabled = false;
                            this.Guardar();
                            btn.Enabled = true;
                        }
                        else
                        {
                            Modificar();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        #endregion Eventos

        #region Funciones
        private void Inicializar()
        {
            this.dpFecha.SelectedDate = DateTime.Now;
            this.txtDia.SelectedDate = DateTime.Now;
            bndSave = true;
            if (Request.QueryString["id"] != "0")
            {
                this.txtEmbarque.Text = Request.QueryString["id"];
                this.HF_ID.Value = Request.QueryString["id"];

                CargarEmbarque();

                this.CmbTipo.Enabled = false;
                if (Request.QueryString["embModificable"] == "0")
                {
                    this.RadToolBar1.Items[1].Visible = false;

                    GridCommandItem cmdItem = (GridCommandItem)rgDetalles.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                    ((Button)cmdItem.FindControl("AddNewRecordButton")).Visible = false;// remove the image button 

                    rgDetalles.MasterTableView.Columns[rgDetalles.MasterTableView.Columns.Count - 1].Display = false;
                    rgDetalles.MasterTableView.Columns[rgDetalles.MasterTableView.Columns.Count - 2].Display = false;
                    ((LinkButton)cmdItem.FindControl("InitInsertButton")).Visible = false;//remove the link button  
                }


            }
            else
            {
                this.txtEmbarque.Text = MaximoId();
                List<ProEmbarqueDet> List = new List<ProEmbarqueDet>();
                Session["ListDet" + Session.SessionID] = List;

                rgDetalles.Rebind();
            }



        }
        private bool validarFecha(RadDatePicker comparaFecha)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (!((comparaFecha.SelectedDate >= sesion.CalendarioIni) && (comparaFecha.SelectedDate <= sesion.CalendarioFin)))
                {
                    Alerta("Fecha se encuentra fuera del periodo");
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private bool validarCamposDetalle()
        {
            if (this.txtChofer.Text == string.Empty)
            {
                this.Alerta("Especifique el nombre del chofer");
                return false;
            }
            if (this.txtCamioneta.Text == string.Empty)
            {
                this.Alerta("Especifique la camioneta");
                return false;
            }
            return true;
        }
        private string MaximoId()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(sesion.Id_Emp, sesion.Id_Cd_Ver, "ProEmbarqueDocumentos", "Id_Emb", sesion.Emp_Cnx, "spCatLocal_Maximo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Guardar()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ProEmbarque emb = new ProEmbarque();
                CN_ProEmbarque cn_emb = new CN_ProEmbarque();
                List<ProEmbarqueDet> List = (List<ProEmbarqueDet>)Session["ListDet" + Session.SessionID];
                int Verificador = 0;
                int Id_Emb = 0;


                if (List.Count == 0)
                {
                    Alerta("Debe ingresar al menos un documento");
                    return;
                }

                LlenarObjetoEmbarque(sesion, ref emb);

                cn_emb.ProEmbarque_Insertar(emb, ref Verificador, sesion.Emp_Cnx);

                if (Verificador != 0)
                {
                    Id_Emb = Verificador;

                    cn_emb.ProEmbarqueDet_Insertar(List, Id_Emb, ref Verificador, sesion.Emp_Cnx);

                    if (Verificador == -1)
                    {
                        string mensaje = "Se creo correctamente el embarque con folio #" + Id_Emb.ToString();
                        RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow('", mensaje, "')"));
                        Inicializar();
                    }
                    else
                    {
                        Alerta("Error al guardar el detalle del embarque");
                    }

                }
                else
                {
                    Alerta("Error al guardar el embarque");
                }
                bndSave = true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void Modificar()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ProEmbarque emb = new ProEmbarque();
                CN_ProEmbarque cn_emb = new CN_ProEmbarque();
                List<ProEmbarqueDet> List = (List<ProEmbarqueDet>)Session["ListDet" + Session.SessionID];
                int Verificador = 0;

                if (List.Count == 0)
                {
                    Alerta("Debe ingresar al menos un documento");
                    return;
                }

                LlenarObjetoEmbarque(sesion, ref emb);

                cn_emb.ProEmbarque_Modificar(emb, ref Verificador, sesion.Emp_Cnx);

                if (Verificador == -1)
                {
                    cn_emb.ProEmbarqueDet_Insertar(List, emb.Id_Emb, ref Verificador, sesion.Emp_Cnx);

                    if (Verificador == -1)
                    {
                        string mensaje = "Se creo modificó el embarque con folio #" + emb.Id_Emb.ToString();
                        RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow('", mensaje, "')"));
                        //Inicializar();
                    }
                    else
                    {
                        Alerta("Error al modificar el detalle del embarque");
                    }

                }
                else
                {
                    Alerta("Error al tratar de modificar el embarque");
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void LlenarObjetoEmbarque(Sesion sesion, ref ProEmbarque emb)
        {
            try
            {
                emb.Id_Emp = sesion.Id_Emp;
                emb.Id_Cd = sesion.Id_Cd_Ver;
                emb.Id_Emb = int.Parse(txtEmbarque.Text);
                emb.Emb_Destino = int.Parse(CmbTipo.SelectedValue);
                emb.Emb_Fecha = dpFecha.SelectedDate;
                emb.Emb_Dia = txtDia.SelectedDate;
                emb.Emb_Chofer = this.txtChofer.Text.Trim();
                emb.Emb_Camioneta = this.txtCamioneta.Text.Trim();
                emb.Id_U = sesion.Id_U;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void CargarEmbarque()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_ProEmbarque cn_emb = new CN_ProEmbarque();
                List<ProEmbarqueDet> List = new List<ProEmbarqueDet>();
                ProEmbarque emb = new ProEmbarque();

                emb.Id_Emp = int.Parse(Page.Request.QueryString["Id_Emp"].ToString());
                emb.Id_Cd = int.Parse(Page.Request.QueryString["Id_Cd"].ToString());
                emb.Id_Emb = int.Parse(Page.Request.QueryString["Id"].ToString());

                cn_emb.ProEmbarque_Consulta(ref emb, ref List, sesion.Emp_Cnx);

                this.dpFecha.SelectedDate = emb.Emb_Fecha;
                this.txtDia.SelectedDate = emb.Emb_Dia;
                this.txtChofer.Text = emb.Emb_Chofer;
                this.txtCamioneta.Text = emb.Emb_Camioneta;
                this.CmbTipo.SelectedValue = emb.Emb_Destino.ToString();

                Session["ListDet" + Session.SessionID] = List;
                rgDetalles.Rebind();


            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion Funciones

        #region ErrorManager
        private void AlertaFocus(string mensaje, string rtb)
        {
            try
            {
                RAM1.ResponseScripts.Add("AlertaFocus('" + mensaje + "','" + rtb + "');");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
        private void Alerta(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
        private void ErrorManager()
        {
            try
            {
                this.lblMensaje.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ErrorManager(string Message)
        {
            try
            {
                this.lblMensaje.Text = Message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ErrorManager(Exception eme, string NombreFuncion)
        {
            try
            {
                this.lblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();

            }
            catch (Exception ex)
            {
                this.lblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }
        #endregion
    }
}