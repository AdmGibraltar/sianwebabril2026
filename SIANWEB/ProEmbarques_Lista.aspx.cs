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
    public partial class ProEmbarques_Lista : System.Web.UI.Page
    {
        #region Variables
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        private Sesion sesion
        {
            get
            {
                return (Sesion)Session["Sesion" + Session.SessionID];
            }
            set
            {
                Session["Sesion" + Session.SessionID] = value;
            }
        }
        public bool ProcEmbAlm;
        //Propiedad de lista de la factura
        #endregion Variables
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                else
                    if (!Page.IsPostBack)
                    {
                        Session["_IdProducto"] = 0;
                        this.ValidarPermisos();
                        if (sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        this.Inicializar();
                        this.CargarCentros();
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region Eventos
        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Server.Transfer("Login.aspx");
                }
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);

                RAM1.ResponseScripts.Add("ObtenerCentroVer(" + CmbCentro.SelectedValue + ");");
                this.rgEmbarques.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "cmbCentro_SelectedIndexChanged1");
            }
        }
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
        protected void rgFacturaRuta_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    //WebControl Button = default(WebControl); //COMENTARIZADA POR NO SER UTILIZADA
                    //string clickHandler = ""; //COMENTARIZADA POR NO SER UTILIZADA

                    //Button = (WebControl)item["Confirmar"].Controls[0];
                    //clickHandler = Button.Attributes["onclick"];
                    //Button.Attributes["onclick"] = clickHandler.Replace("[[ID]]", item.GetDataKeyValue("Id_Fac").ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    case "RebindGrid":
                        this.rgEmbarques.Rebind();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }
        protected void rg_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                this.rgEmbarques.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rg_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                this.ValidarPermisos();
                if (_PermisoModificar)
                {
                    int Id_Emp = 0;
                    int Id_Cd = 0;
                    int Id_Emb = 0;
                    string statusPosibles = "";
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                    GridItem gi = e.Item;

                    Id_Emp = sesion.Id_Emp;
                    Id_Cd = sesion.Id_Cd_Ver;



                    switch (e.CommandName)
                    {
                        case "Baja":
                            if (_PermisoEliminar)
                            {

                                Id_Emb = int.Parse(rgEmbarques.MasterTableView.Items[e.Item.ItemIndex]["Id_Emb"].Text);
                                statusPosibles = rgEmbarques.MasterTableView.Items[e.Item.ItemIndex]["Emb_EstatusStr"].Text;

                                if (statusPosibles.Contains("Baja"))
                                    this.Alerta("El registro ya ha sido dado de baja");
                                else
                                {
                                    if (statusPosibles.Contains("Entregado"))
                                    {
                                        this.Alerta("No se puede dar de baja un registro con estatus de entregado");
                                    }
                                    else if (statusPosibles.Contains("Parcialmente entregado"))
                                    {
                                        this.Alerta("No se puede dar de baja un registro con estatus de parcialmente entregado");
                                    }
                                    else
                                    {
                                        Baja(Id_Emp, Id_Cd, Id_Emb);
                                    }
                                }
                            }
                            else
                                this.Alerta("No tiene permisos para eliminar registros");
                            break;

                        case "Editar":
                            if (_PermisoModificar)
                            {

                                Id_Emb = int.Parse(rgEmbarques.MasterTableView.Items[e.Item.ItemIndex]["Id_Emb"].Text);
                                statusPosibles = rgEmbarques.MasterTableView.Items[e.Item.ItemIndex]["Emb_EstatusStr"].Text;

                                string embModificable = "1";
                                if (!statusPosibles.Contains("Capturado"))
                                {
                                    embModificable = "0";
                                }
                                RAM1.ResponseScripts.Add(string.Concat(@"AbrirVentana('"
                                    , Id_Emp, "','", Id_Cd, "','", Id_Emb, "','", embModificable, "')"));
                            }
                            else
                                this.Alerta("No tiene permisos para modificar");
                            break;

                        case "Imprimir":
                            if (_PermisoImprimir)
                            {
                                Id_Emb = int.Parse(rgEmbarques.MasterTableView.Items[e.Item.ItemIndex]["Id_Emb"].Text);
                                statusPosibles = rgEmbarques.MasterTableView.Items[e.Item.ItemIndex]["Emb_EstatusStr"].Text;
                                if (statusPosibles.Contains("Baja"))
                                    this.Alerta("El registro se encuentra en estatus inválido para esta operación");
                                else
                                    RAM1.ResponseScripts.Add("return AbrirVentana_Impresion('" + Id_Emp + "','" + Id_Cd + "','" + Id_Emb + "')");
                            }
                            else
                                this.Alerta("No tiene permisos para imprimir");
                            break;
                    }

                }
                else
                {
                    this.Alerta("No tiene autorización para cambiar modificar embarque");
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
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
                this.rgEmbarques.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgEmbarques_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    this.rgEmbarques.DataSource = this.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
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
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, sesion.Id_Cd_Ver, sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
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
                Pagina pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                if (pag.Length > 1)
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                else
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;

                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, sesion.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;
                Permiso Permiso = new Permiso();
                Permiso.Id_U = sesion.Id_U;
                Permiso.Id_Cd = sesion.Id_Cd;
                Permiso.Sm_cve = pagina.Clave; //Esta clave depende de la pantalla

                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, sesion.Emp_Cnx);

                if (Permiso.PAccesar == true)
                {
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;
                }
                else
                    Response.Redirect("Inicio.aspx");

                if (!Permiso.PGrabar)
                    this.RadToolBar1.Items[0].Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool validarFecha(DateTime Emb_Fec)
        {
            try
            {
                if (!((Emb_Fec >= sesion.CalendarioIni) && (Emb_Fec <= sesion.CalendarioFin)))
                    Alerta("Fecha se encuentra fuera del periodo");
                //return false;              
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void Inicializar()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            ProcEmbAlm = Sesion.ProcEmbAlm;
            dpFecha1.DbSelectedDate = Sesion.CalendarioIni;
            dpFecha2.DbSelectedDate = Sesion.CalendarioFin;
            this.rgEmbarques.Rebind();
        }
        private List<ProEmbarque> GetList()
        {
            try
            {
                List<ProEmbarque> List = new List<ProEmbarque>();
                CN_ProEmbarque cn_emb = new CN_ProEmbarque();
                ProEmbarque emb = new ProEmbarque();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                emb.Id_Cd = sesion.Id_Cd_Ver;
                emb.Filtro_EmbFechaIni = dpFecha1.SelectedDate.ToString() == "" ? (DateTime?)null : dpFecha1.SelectedDate;
                emb.Filtro_EmbFechaFin = dpFecha2.SelectedDate.ToString() == "" ? (DateTime?)null : dpFecha2.SelectedDate;
                emb.Filtro_Id_Emb = this.txtEmbarque.Text == "" ? (int?)null : int.Parse(this.txtEmbarque.Text);
                emb.Filtro_Emb_Destino = this.CmbTipo.SelectedValue == "0" ? (int?)null : int.Parse(this.CmbTipo.SelectedValue);

                cn_emb.ProEmbarque_ConsultaLista(emb, ref List, sesion.Emp_Cnx);

                return List;
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
        private void Baja(int Id_Emp, int Id_Cd, int Id_Emb)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_ProEmbarque cn_emb = new CN_ProEmbarque();
                ProEmbarque emb = new ProEmbarque();
                int Verificador = 0;

                emb.Id_Emp = Id_Emp;
                emb.Id_Cd = Id_Cd;
                emb.Id_Emb = Id_Emb;

                cn_emb.ProEmbarque_Baja(emb, ref Verificador, sesion.Emp_Cnx);

                if (Verificador == -1)
                {
                    rgEmbarques.Rebind();
                    Alerta("Se dio de baja el embarque correctamente");

                }
                else
                {
                    Alerta("Error al tratar de dar de baja el convenio");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
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