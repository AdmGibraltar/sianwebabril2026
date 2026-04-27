using CapaEntidad;
using CapaNegocios;
using DevExpress.Export;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIANWEB
{
    public partial class Rep_PortalKey : System.Web.UI.Page
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


        private string Emp_CnxCentral
        {
            get { return ConfigurationManager.AppSettings.Get("strConnectionCentral"); }
        }

        #endregion Variables

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["ReporteSucrusal"] != null)
            {
                List<Portakey> ActividadesRSC = (List<Portakey>)Session["ReporteSucrusal"];
                GrdSucursal.DataSource = ActividadesRSC;
                GrdSucursal.DataBind();

            }
            if (Session["ReporteRepre"] != null)
            {
                List<Portakey> ListaRSCCampo = (List<Portakey>)Session["ReporteRepre"];
                grdRepresentante.DataSource = ListaRSCCampo;
                grdRepresentante.DataBind();
            }


            if (Session["reporteCliente"] != null)
            {
                List<Portakey> ActividadesAsesor = (List<Portakey>)Session["reporteCliente"];
                grdCliente.DataSource = ActividadesAsesor;
                grdCliente.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    ValidarPermisos();
                    Session["reporteCliente"] = null;
                    Session["ReporteRepre"] = null;
                    Session["ReporteSucrusal"] = null;

                    CargarGrupos();
                    CargarTrimestre();
                    año.Style["display"] = "";
                }
            }
            catch (Exception ex)
            {
                danger(string.Concat(ex.Message, "Page_Load_error"));
            }
        }

        /// <summary>
        /// funciona que valida los permiso de la pagina
        /// </summary>
        private void ValidarPermisos()
        {
            try
            {
                if (sesion != null)
                {
                    Session["guardar"] = null;
                    Session["modificar"] = null;
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

                    if (Permiso.PAccesar == true)
                    {
                        _PermisoGuardar = Permiso.PGrabar;
                        _PermisoModificar = Permiso.PModificar;
                        Session["guardar"] = Permiso.PGrabar;
                        Session["modificar"] = Permiso.PModificar;
                    }
                    else
                        Response.Redirect("Inicio.aspx");

                }
                else
                {
                    Response.Redirect("login.aspx");
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void CargarGrupos()
        {
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.DevLlenaCombo(2, sesion.Id_Emp, sesion.Id_U, Emp_CnxCentral, "SP_CatCDI_combo2", ref Cmbsucursal);
            Cmbsucursal.Value = sesion.Id_Cd_Ver.ToString();
            Cmbsucursal.Enabled = false;

            txtAño.Value = DateTime.Now;
            txtFecha.Value = DateTime.Now;
        }

        /// <summary>
        /// Cargar la información del trimestre
        /// </summary>
        private void CargarTrimestre()
        {
            List<Mes> ListaMes = new List<Mes>();
            Mes RegistroMes;

            int MesActual = DateTime.Now.Month;

            RegistroMes = new Mes();
            RegistroMes.Id_mes = "-1";
            RegistroMes.NombreMes = "--Seleccionar--";
            ListaMes.Add(RegistroMes);

            RegistroMes = new Mes();
            RegistroMes.Id_mes = "1";
            RegistroMes.NombreMes = "trimestre 1";
            ListaMes.Add(RegistroMes);

            RegistroMes = new Mes();
            RegistroMes.Id_mes = "2";
            RegistroMes.NombreMes = "trimestre 2";
            ListaMes.Add(RegistroMes);

            RegistroMes = new Mes();
            RegistroMes.Id_mes = "3";
            RegistroMes.NombreMes = "trimestre 3";
            ListaMes.Add(RegistroMes);

            RegistroMes = new Mes();
            RegistroMes.Id_mes = "4";
            RegistroMes.NombreMes = "trimestre 4";
            ListaMes.Add(RegistroMes);


            BcbTrimestre.DataSource = ListaMes;
            BcbTrimestre.ValueField = "Id_mes";
            BcbTrimestre.TextField = "NombreMes";
            BcbTrimestre.DataBind();


            BcbTrimestre.Value = "-1";
            txtfechaTrimestreInicial.Value = DateTime.Now;
        }

        #region Mensajes 


        private void ShowMessage(string Message, string type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }
        private void sucess(string mensaje)
        {
            ShowMessage(mensaje, "Success");
        }
        private void danger(string mensaje)
        {
            ShowMessage(mensaje, "Error");
        }
        private void warning(string mensaje)
        {
            ShowMessage(mensaje, "Warning");
        }
        private void info(string mensaje)
        {
            ShowMessage(mensaje, "Info");
        }

        #endregion

        protected void BtnReporte_ServerClick(object sender, EventArgs e)
        {
            try
            {
                DvCliente.Style["display"] = "none";
                DvRepresentante.Style["display"] = "none";
                sucursal.Style["display"] = "none";
                if (RbtTipo.SelectedValue.ToString() == "1")
                {
                    cargarSucursal();
                }
                else if (RbtTipo.SelectedValue.ToString() == "2")
                {
                    cargarRepresentante();
                }
                else
                {
                    CargarCliente();
                }
            }
            catch (Exception ex)
            {
                warning("Error al procesar el reporte");
            }
        }

        public void cargarSucursal()
        {

            Portakey Portal = new Portakey();
            List<Portakey> lista = new List<Portakey>();
            CN_PortalKey CN = new CN_PortalKey();
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            Portal.Id_Emp = Sesion.Id_Emp;
            Portal.id_Cd = Convert.ToInt32(Cmbsucursal.Value.ToString());
            string conexion = ConfigurationManager.AppSettings.Get("strConnectionCentral");
            if (RBFecha.SelectedValue.ToString() == "1")
            {
                int year = DateTime.Parse(txtAño.Value.ToString()).Year;
                DateTime firstDay = new DateTime(year, 1, 1);
                DateTime lastDay = new DateTime(year, 12, 31);

                Portal.fechainicio = firstDay;
                Portal.fecchafinal = lastDay;
            }
            else if (RBFecha.SelectedValue.ToString() == "2")
            {
                DateTime fecha1 = DateTime.Parse(txtFecha.Value.ToString());
                DateTime fecha2 = DateTime.Parse(txtFecha.Value.ToString()).AddMonths(1).AddDays(-1);

                Portal.fechainicio = DateTime.Parse("01/" + fecha1.Month + "/" + fecha1.Year);
                Portal.fecchafinal = DateTime.Parse("01/" + fecha2.Month + "/" + fecha2.Year).AddMonths(1).AddDays(-1);
            }
            else
            {
                DateTime fechaInicialtrimestral = DateTime.Now;
                DateTime fechafinaltrimestral = DateTime.Now;
                if (BcbTrimestre.Value.ToString() == "1")
                {
                    fechaInicialtrimestral = DateTime.Parse("01/01/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                    fechafinaltrimestral = DateTime.Parse("01/03/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                }
                if (BcbTrimestre.Value.ToString() == "2")
                {
                    fechaInicialtrimestral = DateTime.Parse("01/04/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                    fechafinaltrimestral = DateTime.Parse("01/06/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                }
                if (BcbTrimestre.Value.ToString() == "3")
                {
                    fechaInicialtrimestral = DateTime.Parse("01/07/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                    fechafinaltrimestral = DateTime.Parse("01/09/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                }
                if (BcbTrimestre.Value.ToString() == "4")
                {
                    fechaInicialtrimestral = DateTime.Parse("01/10/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                    fechafinaltrimestral = DateTime.Parse("01/12/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                }

                Portal.fechainicio = fechaInicialtrimestral;
                Portal.fecchafinal = fechafinaltrimestral;
            }


            CN.ConsultaSucursal(Portal, ref lista, conexion);
            if (lista.Count > 0)
            {
                Session["ReporteSucrusal"] = lista;
                sucursal.Style["display"] = "";
                GrdSucursal.DataSource = lista;
                GrdSucursal.DataBind();
            }
        }

        public void cargarRepresentante()
        {
            Portakey Portal = new Portakey();
            List<Portakey> lista = new List<Portakey>();
            CN_PortalKey CN = new CN_PortalKey();
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            if (Convert.ToInt32(Cmbsucursal.Value.ToString()) == 1)
            {
                info("Favor de seleccionar una sucursal");
            }

            Portal.Id_Emp = Sesion.Id_Emp;
            Portal.id_Cd = Convert.ToInt32(Cmbsucursal.Value.ToString());
            string conexion = ConfigurationManager.AppSettings.Get("strConnectionCentral");
            if (RBFecha.SelectedValue.ToString() == "1")
            {
                int year = DateTime.Parse(txtAño.Value.ToString()).Year;
                DateTime firstDay = new DateTime(year, 1, 1);
                DateTime lastDay = new DateTime(year, 12, 31);

                Portal.fechainicio = firstDay;
                Portal.fecchafinal = lastDay;
            }
            else if (RBFecha.SelectedValue.ToString() == "2")
            {
                DateTime fecha1 = DateTime.Parse(txtFecha.Value.ToString());
                DateTime fecha2 = DateTime.Parse(txtFecha.Value.ToString()).AddMonths(1).AddDays(-1);

                Portal.fechainicio = DateTime.Parse("01/" + fecha1.Month + "/" + fecha1.Year);
                Portal.fecchafinal = DateTime.Parse("01/" + fecha2.Month + "/" + fecha2.Year).AddMonths(1).AddDays(-1);
            }
            else
            {
                DateTime fechaInicialtrimestral = DateTime.Now;
                DateTime fechafinaltrimestral = DateTime.Now;
                if (BcbTrimestre.Value.ToString() == "1")
                {
                    fechaInicialtrimestral = DateTime.Parse("01/01/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                    fechafinaltrimestral = DateTime.Parse("01/03/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                }
                if (BcbTrimestre.Value.ToString() == "2")
                {
                    fechaInicialtrimestral = DateTime.Parse("01/04/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                    fechafinaltrimestral = DateTime.Parse("01/06/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                }
                if (BcbTrimestre.Value.ToString() == "3")
                {
                    fechaInicialtrimestral = DateTime.Parse("01/07/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                    fechafinaltrimestral = DateTime.Parse("01/09/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                }
                if (BcbTrimestre.Value.ToString() == "4")
                {
                    fechaInicialtrimestral = DateTime.Parse("01/10/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                    fechafinaltrimestral = DateTime.Parse("01/12/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                }


                Portal.fechainicio = fechaInicialtrimestral;
                Portal.fecchafinal = fechafinaltrimestral;
            }


            CN.ConsultaRepresentante(Portal, ref lista, conexion);

            if (lista.Count > 0)
            {
                Session["ReporteRepre"] = lista;
                DvRepresentante.Style["display"] = "";
                grdRepresentante.DataSource = lista;
                grdRepresentante.DataBind();
            }
        }


        public void CargarCliente()
        {
            Portakey Portal = new Portakey();
            List<Portakey> lista = new List<Portakey>();
            CN_PortalKey CN = new CN_PortalKey();
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            if (Convert.ToInt32(Cmbsucursal.Value.ToString()) == 1)
            {
                info("Favor de seleccionar una sucursal");
                return;
            }
            Portal.Id_Emp = Sesion.Id_Emp;
            Portal.id_Cd = Convert.ToInt32(Cmbsucursal.Value.ToString());
            string conexion = ConfigurationManager.AppSettings.Get("strConnectionCentral");
            if (RBFecha.SelectedValue.ToString() == "1")
            {
                int year = DateTime.Parse(txtAño.Value.ToString()).Year;
                DateTime firstDay = new DateTime(year, 1, 1);
                DateTime lastDay = new DateTime(year, 12, 31);

                Portal.fechainicio = firstDay;
                Portal.fecchafinal = lastDay;
            }
            else if (RBFecha.SelectedValue.ToString() == "2")
            {
                DateTime fecha1 = DateTime.Parse(txtFecha.Value.ToString());
                DateTime fecha2 = DateTime.Parse(txtFecha.Value.ToString()).AddMonths(1).AddDays(-1);

                Portal.fechainicio = DateTime.Parse("01/" + fecha1.Month + "/" + fecha1.Year);
                Portal.fecchafinal = DateTime.Parse("01/" + fecha2.Month + "/" + fecha2.Year).AddMonths(1).AddDays(-1);
            }
            else
            {
                DateTime fechaInicialtrimestral = DateTime.Now;
                DateTime fechafinaltrimestral = DateTime.Now;
                if (BcbTrimestre.Value.ToString() == "1")
                {
                    fechaInicialtrimestral = DateTime.Parse("01/01/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                    fechafinaltrimestral = DateTime.Parse("01/03/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                }
                if (BcbTrimestre.Value.ToString() == "2")
                {
                    fechaInicialtrimestral = DateTime.Parse("01/04/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                    fechafinaltrimestral = DateTime.Parse("01/06/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                }
                if (BcbTrimestre.Value.ToString() == "3")
                {
                    fechaInicialtrimestral = DateTime.Parse("01/07/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                    fechafinaltrimestral = DateTime.Parse("01/09/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                }
                if (BcbTrimestre.Value.ToString() == "4")
                {
                    fechaInicialtrimestral = DateTime.Parse("01/10/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                    fechafinaltrimestral = DateTime.Parse("01/12/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                }


                Portal.fechainicio = fechaInicialtrimestral;
                Portal.fecchafinal = fechafinaltrimestral;
            }


            CN.ConsultaCliente(Portal, ref lista, conexion);

            if (lista.Count > 0)
            {
                Session["reporteCliente"] = lista;
                DvCliente.Style["display"] = "";
                grdCliente.DataSource = lista;
                grdCliente.DataBind();
            }
        }

        protected void btnreporteexcel_ServerClick(object sender, EventArgs e)
        {
            if (RbtTipo.SelectedValue.ToString() == "1")
            {
                GrdSucursal.ExportXlsToResponse(new XlsExportOptionsEx { ExportType = ExportType.WYSIWYG });
            }
            else if (RbtTipo.SelectedValue.ToString() == "2")
            {
                grdRepresentante.ExportXlsToResponse(new XlsExportOptionsEx { ExportType = ExportType.WYSIWYG });
            }
            else
            {
                grdCliente.ExportXlsToResponse(new XlsExportOptionsEx { ExportType = ExportType.WYSIWYG });
            }
        }

        protected void RBFecha_SelectedIndexChanged(object sender, EventArgs e)
        {
            año.Style["display"] = "none";
            Mes.Style["display"] = "none";
            Trimestre.Style["display"] = "none";
            if (RBFecha.SelectedValue.ToString() == "1")
            {

                año.Style["display"] = "";
            }
            else if (RBFecha.SelectedValue.ToString() == "2")
            {

                Mes.Style["display"] = "";
            }
            else if (RBFecha.SelectedValue.ToString() == "3")
            {

                Trimestre.Style["display"] = "";
            }
        }
    }
}