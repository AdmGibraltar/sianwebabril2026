using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Text;
using System.Net;
using System.IO;
using System.Collections;

namespace SIANWEB
{
    public partial class ProEmbarque_Confirmar : System.Web.UI.Page
    {
        #region Variables
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public List<ProEmbarqueDet> ListEmb
        {
            get
            {
                return (Session["ListEmbarques" + Session.SessionID] as List<ProEmbarqueDet>);
            }
            set
            {
                Session["ListEmbarques" + Session.SessionID] = value;
            }

        }
        #endregion Variables
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (ValidarSesion())
                    if (!Page.IsPostBack)
                    {
                        Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                        if (!Page.IsPostBack)
                        {
                            Session["_IdProducto"] = 0;
                            this.ValidarPermisos();
                            if (sesion.Cu_Modif_Pass_Voluntario == false)
                            {
                                RAM1.ResponseScripts.Add("(function(){var f = function(){AbrirContrasenas "
                                    + "return false;Sys.Application.remove_load(f);};Sys.Application.add_load(f);})()");
                                return;
                            }
                            CargarCentros();
                            this.Inicializar();
                        }
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region Eventos
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rtb1_ButtonClick");
            }
        }
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    case "Aceptar":
                        ConfirmarTodos();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }
        protected void rgEmbarque_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    this.rgEmbarque.DataSource = this.ListEmb;
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgReclamaciones_NeedDataSource"));
            }
        }
        protected void rgEmbarque_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                this.rgEmbarque.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgEmbarque_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                this.ValidarPermisos();
                if (_PermisoModificar)
                {
                    switch (e.CommandName)
                    {
                        case "Confirmar":
                            ConfirmarUno(e);
                            break;

                    }
                }
                else
                {
                    this.Alerta("No tiene autorización para cambiar el estatus a la factura");
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgEmbarque_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    WebControl Button = default(WebControl);
                    string clickHandler = "";

                    Button = (WebControl)item["Confirmar"].Controls[0];
                    clickHandler = Button.Attributes["onclick"];
                    Button.Attributes["onclick"] = clickHandler.Replace("[[ID]]", item.GetDataKeyValue("Id_DocStr").ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (this.dpFecha1.SelectedDate > this.dpFecha2.SelectedDate)
            {
                this.Alerta("La fecha inicial no debe ser mayor a la fecha final");
                this.dpFecha1.Focus();
                return;
            }
            try
            {
                this.ListEmb = GetList();
                this.rgEmbarque.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                this.Inicializar();
                rgEmbarque.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }
        protected void ChkSeleccionado_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = (sender) as CheckBox;

                string UniqueID = (chk.Parent.Parent.FindControl("LblUniqueID") as Label).Text;

                List<ProEmbarqueDet> List = new List<ProEmbarqueDet>();
                List = (List<ProEmbarqueDet>)Session["ListEmbarques" + Session.SessionID];

                ProEmbarqueDet emb = new ProEmbarqueDet();
                emb = List.FirstOrDefault(o => o.UniqueID == UniqueID);
                if (chk.Checked)
                {
                    emb.Seleccionado = true;
                }
                else
                {
                    emb.Seleccionado = false;
                }


                ListEmb = List;

                //foreach (ProEmbarqueDet e in List)
                //{

                //}
                //ErrorManager();
                //Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                //CheckBox chkver = (sender as CheckBox);
                //int Id_Tipo = Convert.ToInt32((chkver.Parent.Parent.FindControl("LblId_Tipo") as Label).Text);
                //int Id_CD = Convert.ToInt32((chkver.Parent.Parent.FindControl("LblId_CD") as Label).Text);
                //if (Id_CD == -1)
                //{
                //    foreach (GridDataItem grd in rgSucursal.Items)
                //    {

                //        int Id_Tipo2 = int.Parse((grd.Controls[0].FindControl("LblId_Tipo") as Label).Text);
                //        if (Id_Tipo == Id_Tipo2)
                //        {

                //            CheckBox chkvr = grd.Controls[0].FindControl("ChkSeleccionado") as CheckBox;
                //            chkvr.Checked = chkver.Checked;
                //        }

                //    }

                //}

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void ChkSeleccionarTodo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = (sender) as CheckBox;
                bool Seleccionado = chk.Checked;
                List<ProEmbarqueDet> List = new List<ProEmbarqueDet>();
                List = (List<ProEmbarqueDet>)Session["ListEmbarques" + Session.SessionID];

                foreach (ProEmbarqueDet emb in List)
                {
                    if (chk.Checked)
                    {
                        emb.Seleccionado = true;
                    }
                    else
                    {
                        emb.Seleccionado = false;
                    }

                }
                ListEmb = List;
                rgEmbarque.Rebind();
                chk.Checked = Seleccionado;
                foreach (GridHeaderItem headerItem in this.rgEmbarque.MasterTableView.GetItems(GridItemType.Header))
                {
                    CheckBox Check = (CheckBox)headerItem["Seleccionar"].Controls[1];
                    Check.Checked = Seleccionado;
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        #endregion Eventos

        #region Funciones
        private void CargarCentros()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();


                if (Sesion.U_MultiOfi == false)
                {
                    CN_Comun.LlenaCombo(2, Sesion.Id_Emp, Sesion.Id_U, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.Visible = false;
                    this.TblEncabezado.Rows[0].Cells[2].InnerText = " " + CmbCentro.FindItemByValue(Sesion.Id_Cd_Ver.ToString()).Text;

                }
                else
                {
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, Sesion.Id_Cd_Ver, Sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = Sesion.Id_Cd_Ver.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ValidarPermisos()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Pagina pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                if (pag.Length > 1)
                {
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                }
                else
                {
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;
                }
                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, Sesion.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;

                Permiso Permiso = new Permiso();
                Permiso.Id_U = Sesion.Id_U;
                Permiso.Id_Cd = Sesion.Id_Cd;
                Permiso.Sm_cve = pagina.Clave;
                //Esta clave depende de la pantalla

                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, Sesion.Emp_Cnx);

                if (Permiso.PAccesar == true)
                {
                    _PermisoModificar = Permiso.PModificar;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool ValidarSesion()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Inicializar()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (sesion.CalendarioIni >= dpFecha1.MinDate && sesion.CalendarioIni <= dpFecha1.MaxDate)
            {
                dpFecha1.DbSelectedDate = sesion.CalendarioIni;
            }
            if (sesion.CalendarioFin >= dpFecha2.MinDate && sesion.CalendarioFin <= dpFecha2.MaxDate)
            {
                dpFecha2.DbSelectedDate = sesion.CalendarioFin;
            }

            this.ListEmb = GetList();
            this.rgEmbarque.Rebind();

            _PermisoModificar = Convert.ToBoolean(Request.QueryString["PermisoModificar"]);
        }
        private List<ProEmbarqueDet> GetList()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ProEmbarqueDet emb = new ProEmbarqueDet();
                List<ProEmbarqueDet> List = new List<ProEmbarqueDet>();
                CN_ProEmbarque cn_emb = new CN_ProEmbarque();

                emb.Id_Emp = sesion.Id_Emp;
                emb.Id_Cd = sesion.Id_Cd_Ver;
                emb.Id_Cte = this.txtCliente1.Text == "" ? (int?)null : int.Parse(txtCliente1.Text);
                emb.Emb_Tipo = int.Parse(CmbTipo.SelectedValue);
                emb.Cte_NomComercial = this.txtNombre.Text.Trim();
                emb.Doc_FechaIni = dpFecha1.SelectedDate.ToString() == "" ? (DateTime?)null : Convert.ToDateTime(dpFecha1.SelectedDate);
                emb.Doc_FechaFin = dpFecha2.SelectedDate.ToString() == "" ? (DateTime?)null : Convert.ToDateTime(dpFecha2.SelectedDate);

                cn_emb.ProEmbarqueConfirmar_Buscar(emb, ref List, sesion.Emp_Cnx);

                return List;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void ConfirmarUno(GridCommandEventArgs e)
        {
            try
            {
                int item = e.Item.ItemIndex;

                ProEmbarqueDet emb = new ProEmbarqueDet();
                CN_ProEmbarque cn_emb = new CN_ProEmbarque();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                int Verificador = 0;

                emb.Id_Emp = int.Parse(rgEmbarque.MasterTableView.Items[item]["Id_Emp"].Text);
                emb.Id_Cd = int.Parse(rgEmbarque.MasterTableView.Items[item]["Id_Cd"].Text);
                emb.Id_Emb = int.Parse(rgEmbarque.MasterTableView.Items[item]["Id_Emb"].Text);
                emb.Id_Doc = int.Parse(rgEmbarque.MasterTableView.Items[item]["Id_Doc"].Text);
                emb.Emb_Tipo = int.Parse(rgEmbarque.MasterTableView.Items[item]["Emb_Tipo"].Text);

                cn_emb.ProEmbarqueConfirmaUno(emb, ref Verificador, sesion.Emp_Cnx);

                if (Verificador == -1)
                {
                    Alerta("Se ha confirmado el documento de manera exitosa");
                    rgEmbarque.Rebind();
                }
                else
                {
                    Alerta("Error al tratar de confirmar el documento");
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void ConfirmarTodos()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_ProEmbarque cn_emb = new CN_ProEmbarque();
                int Verificador = 0;

                List<ProEmbarqueDet> List = new List<ProEmbarqueDet>();
                List = (List<ProEmbarqueDet>)Session["ListEmbarques" + Session.SessionID];

                foreach (ProEmbarqueDet emb in List)
                {
                    if (emb.Seleccionado == true)
                    {
                        cn_emb.ProEmbarqueConfirmaUno(emb, ref Verificador, sesion.Emp_Cnx);
                    }
                }

                if (Verificador == -1)
                {
                    Alerta("Se han confirmado todos los embarques correctamente");
                    ListEmb = GetList();
                    rgEmbarque.Rebind();
                }
                else
                {
                    Alerta("Error al tratar de confirmar todos los embarques");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta("Error al cargar la página");
            else
                if (mensaje.Contains("CapReclamaciones_Imprimir_Denegado"))
                    Alerta("Imposible imprimir el documento");
                else
                    if (mensaje.Contains("CapReclamaciones_Modificar_Denegado"))
                        Alerta("Imposible modificar el documento");
                    else
                        if (mensaje.Contains("CapReclamaciones_print_error"))
                            Alerta("Error al imprimir la reclamación");
                        else
                            if (mensaje.Contains("btnBuscar_error"))
                                Alerta("Error al momento de filtrar la información");
                            else
                                if (mensaje.Contains("RAM1_AjaxRequest"))
                                    Alerta("Error al momento de actualizar el Grid de ordenes de compra");
                                else
                                    if (mensaje.Contains("rgReclamaciones_NeedDataSource"))
                                        Alerta("Error al cargar el Grid de detalle de orden de compra");
                                    else
                                        if (mensaje.Contains("rgReclamaciones_ItemCommand"))
                                            Alerta("Error al ejecutar un evento (rgOrdCompra_ItemCommand) al cargar el Grid de orden de compra");
                                        else
                                            if (mensaje.Contains("radGrid_PageIndexChanged"))
                                                Alerta("Error al cambiar de página");
                                            else
                                                Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
        }
        #endregion Funciones

        #region ErrorManager
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

        #endregion ErrorManager



    }
}