using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaEntidad;
using Telerik.Web.UI;
using CapaNegocios;
using System.Collections;
using CapaDatos;
using System.Web.Services;
using System.IO;
using System.Xml.Serialization;
using System.Configuration;
using System.Data.OleDb;
using System.Xml;
using System.Text;
using System.Net;
using System.Web.UI.HtmlControls;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace SIANWEB
{
    public partial class CapMonitoreoTimbradoComplementoPago : System.Web.UI.Page
    {
        #region Variables
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        private class Timbre
        {
            public CapaEntidad.Clientes Cliente { get; set; }
            public List<CapaEntidad.Factura> Facturas { get; set; }
        }

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
        #endregion

        private string Emp_CnxCob
        {
            get { return ConfigurationManager.AppSettings.Get("strConnectionCobranza"); }
        }
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (ValidarSesion())
                {
                    Sesion Sesion = new Sesion();
                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                    if (Sesion == null)
                        CerrarVentana();

                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();
                        Inicializar();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (txtFecha1.SelectedDate > txtFecha2.SelectedDate)
                {
                    Alerta("La fecha inicial no debe ser mayor a la fecha final");
                    return;
                }
                this.rgPago.Rebind();
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
                Sesion sesion2 = sesion;
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion2);

                sesion = sesion2;
                rgPago.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }

        protected void rg_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    rgPago.DataSource = GetList();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RadGrid1_NeedDataSource");
            }
        }
        protected void rg_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                this.rgPago.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                string cmd = e.Argument.ToString();
                switch (cmd)
                {
                    case "RebindGrid":
                        rgPago.Rebind();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }

        protected void rg_ItemCommand(object source, GridCommandEventArgs e)
        {
            CN_CatCliente clsCatClientes = new CN_CatCliente();
            int Id_Emp = Convert.ToInt32(sesion.Id_Emp.ToString());
            int Id_Cd = Convert.ToInt32(sesion.Id_Cd_Ver.ToString());
            int id_ShowLog = 0;
            int verificador = 0;
            try
            {
                Session["PosBackPagos" + Session.SessionID] = null;
                DateTime fechaPeriodoInicio = sesion.CalendarioIni;
                DateTime fechaPeriodoFinal = sesion.CalendarioFin;
                GridItem gi = e.Item;


                switch (e.CommandName)
                {
                    case "Timbrar":
                        id_ShowLog = int.Parse(gi.Cells[2].Text);

                        PagoDetalleComplementoShowLog PagoDetalleComplementoShowLog = new PagoDetalleComplementoShowLog();

                        PagoDetalleComplementoShowLog.Id_Emp = Id_Emp;
                        PagoDetalleComplementoShowLog.Id_Cd = Id_Cd;
                        PagoDetalleComplementoShowLog.Id_ShowLog = id_ShowLog;

                        new CN_CapPago().ModificarPagoDetTimbradoShowLog(PagoDetalleComplementoShowLog, sesion.Emp_Cnx, ref verificador);

                        rgPago.Rebind();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgPago_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    bool atendido;
                    GridItem item = (GridItem)e.Item;
                    bool.TryParse(item.Cells[8].Text, out atendido);

                    if (atendido)
                    {
                        item.FindControl("Timbrar").Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rtb1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                switch (btn.CommandName.ToLower())
                {                     
                    case "excel":
                        ReportePagos();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rtb1_ButtonClick");
            }
        }

        #endregion
        #region Funciones

        private void ReportePagos()
        {
            try
            {
                int Id_Emp = sesion.Id_Emp;
                int Id_Cd = sesion.Id_Cd_Ver;
                List<PagoDetalleComplementoShowLog> lista = GetList();
                if (lista.Count > 0)
                {
                    string ruta = Server.MapPath("Reportes") + "\\ReporteMonitorPago.xls";
                    if (File.Exists(ruta))
                        File.Delete(ruta);
                    StreamWriter sw = new StreamWriter(ruta, false, Encoding.UTF8);
                    sw.WriteLine("<html>");
                    sw.WriteLine("<head>");
                    sw.WriteLine("</head>");
                    sw.WriteLine("<body>");
                    sw.WriteLine("<table border=1><font size=8pt>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>FOLIO</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>FECHA</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>OBSERVACIONES</td>");
                    sw.WriteLine("<td bgcolor=\"#DDDDDD\"><b>ATENDIDO</td>");
                    sw.WriteLine("</tr>");

                    foreach (var item in lista)
                    {
                        sw.WriteLine("<tr>");
                        sw.WriteLine("<td>" + item.Id_Pag + "</td>");
                        sw.WriteLine("<td>" + string.Format("{0:dd/MM/yyyy}", item.Fecha) + "</td>");
                        sw.WriteLine("<td>" + item.Observaciones + "</td>");
                        sw.WriteLine("<td>" + (item.Atendido == true ? "ATENDIDO" : "NO ATENDIDO") + "</td>");

                        sw.WriteLine("</tr>");
                    }
                    sw.WriteLine("</font></table>");
                    sw.WriteLine("</body>");
                    sw.WriteLine("</html>");
                    sw.Close();
                    RAM1.ResponseScripts.Add(string.Concat(@"abrirArchivo('Reportes\\ReporteMonitorPago.xls')"));
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
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    CerrarVentana();
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<PagoDetalleComplementoShowLog> GetList()
        {
            try
            {
                List<PagoDetalleComplementoShowLog> List = new List<PagoDetalleComplementoShowLog>();
                CN_CapPago consultalista = new CN_CapPago();

                PagoDetalleComplementoShowLog PagoDetalleComplementoShowLog = new CapaEntidad.PagoDetalleComplementoShowLog();
                PagoDetalleComplementoShowLog.Id_Emp = sesion.Id_Emp;
                PagoDetalleComplementoShowLog.Id_Cd = sesion.Id_Cd_Ver;
                PagoDetalleComplementoShowLog.Filtro_FecIni = Convert.ToDateTime(txtFecha1.SelectedDate);
                PagoDetalleComplementoShowLog.Filtro_FecFin = Convert.ToDateTime(txtFecha2.SelectedDate);
                consultalista.ConsultarPagoDetTimbradoShowLog(PagoDetalleComplementoShowLog, sesion.Emp_Cnx, ref List);


                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Inicializar()
        {
            try
            {
                if (sesion.CalendarioIni >= txtFecha1.MinDate && sesion.CalendarioIni <= txtFecha1.MaxDate)
                {
                    txtFecha1.DbSelectedDate = sesion.CalendarioIni;
                }
                if (sesion.CalendarioFin >= txtFecha2.MinDate && sesion.CalendarioFin <= txtFecha2.MaxDate)
                {
                    txtFecha2.DbSelectedDate = sesion.CalendarioFin;
                }
                rgPago.Rebind();
                CargarCentros();
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
                Permiso.Sm_cve = pagina.Clave;

                //Esta clave depende de la pantalla
                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, sesion.Emp_Cnx);

                if (!(Permiso.PAccesar == true))
                    CerrarVentana();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarCentros()
        {
            try
            {
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();

                if (sesion.U_MultiOfi == false)
                {
                    CN_Comun.LlenaCombo(2, sesion.Id_Emp, sesion.Id_U, sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.Visible = false;
                    this.TblEncabezado.Rows[0].Cells[2].InnerText = " " + CmbCentro.FindItemByValue(sesion.Id_Cd_Ver.ToString()).Text;
                }
                else
                {
                    CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_U, sesion.Id_Cd_Ver, sesion.Id_Cd, sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = sesion.Id_Cd_Ver.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

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

        private void CerrarVentana()
        {
            try
            {
                string funcion;
                if (this.HiddenRebind.Value == "0")
                    funcion = "CloseWindow()";
                else
                    funcion = "CloseAndRebind()";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}