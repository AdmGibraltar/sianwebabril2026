using CapaEntidad;
using CapaNegocios;
using DevExpress.Export;
using DevExpress.Export.Xl;
using DevExpress.Printing.ExportHelpers;
using DevExpress.Web;
using DevExpress.Web.Bootstrap;
using DevExpress.XtraPrinting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIANWEB
{
    public partial class MonitorActividades : System.Web.UI.Page
    {
        #region Variables 

        public Sesion session
        {
            get
            {
                return (Sesion)Session["Sesion" + Session.SessionID];
            }
            set
            {
                Session["session" + Session.SessionID] = value;

            }
        }

        private string Emp_CnxCentral
        {
            get { return ConfigurationManager.AppSettings.Get("strConnectionCentral"); }
        }

        #endregion 

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

        #region procesoInicial 

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["MonitorActividadesRSCOficina"] != null)
            {
                List<AgendaRsc> ActividadesRSC = (List<AgendaRsc>)Session["MonitorActividadesRSCOficina"];
                GrdActividadesRSC.DataSource = ActividadesRSC;
                GrdActividadesRSC.DataBind();

            }
            if (Session["MonitorActividadesRSCCampo"] != null)
            {
                List<AgendaRsc> ListaRSCCampo = (List<AgendaRsc>)Session["MonitorActividadesRSCCampo"];
                GrdActividadesRSCCampo.DataSource = ListaRSCCampo;
                GrdActividadesRSCCampo.DataBind();
            }


            if (Session["MonitorActividadesAsesorOficina"] != null)
            {
                List<AgendaRsc> ActividadesAsesor = (List<AgendaRsc>)Session["MonitorActividadesAsesorOficina"];
                grdActividadeAsesor.DataSource = ActividadesAsesor;
                grdActividadeAsesor.DataBind();
            }

            if (Session["MonitorActividadesAsesorCampo"] != null)
            {
                List<AgendaRsc> ListaAsesorCampo = (List<AgendaRsc>)Session["MonitorActividadesAsesorCampo"];
                grdAsesorCampo.DataSource = ListaAsesorCampo;
                grdAsesorCampo.DataBind();
            }
            if (Session["MonitorFacturacionCliente"] != null)
            {
                List<FacturacionCliente> Lista = (List<FacturacionCliente>)Session["MonitorFacturacionCliente"];
                GrdFacturacionCliente.DataSource = Lista;
                GrdFacturacionCliente.DataBind();
            }
            if (Session["MonitorReporteCaptacion"] != null)
            {
                List<PedidoVtaInst> Lista = (List<PedidoVtaInst>)Session["MonitorReporteCaptacion"];
                GrdCaptacionPedidos.DataSource = Lista;
                GrdCaptacionPedidos.DataBind();
            }
            if (Session["Monitortracking"] != null)
            {
                List<AgendaRsc> Lista = (List<AgendaRsc>)Session["Monitortracking"];
                grdTracking.DataSource = Lista;
                grdTracking.DataBind();
            }
            if (Session["Monitortracking"] != null)
            {
                List<AgendaRsc> Lista = (List<AgendaRsc>)Session["Monitortracking"];
                grdTracking.DataSource = Lista;
                grdTracking.DataBind();
            }
            if (Session["MonitorGraltracking"] != null)
            {
                List<actividades> Lista = (List<actividades>)Session["MonitorGraltracking"];
                grdmonitorgeneral.DataSource = Lista;
                grdmonitorgeneral.DataBind();
            }
            if (Session["RepCumplViReg"] != null)
            {
                List<eClienteBuscar> Lista = (List<eClienteBuscar>)Session["RepCumplViReg"];
                GrdCuentaVI.DataSource = Lista;
                GrdCuentaVI.DataBind();
            }
            if (Session["idRepVentas"] != null)
            {
                if (Session["idRepVentas"] != null)
                {
                    List<VenEstadisticaVentas> List = (List<VenEstadisticaVentas>)Session["RepVentas"];

                    int id = Convert.ToInt32(Session["idRepVentas"].ToString());

                    if (id == 1)
                    {
                        GridCliente.Style["display"] = "";
                        GrdCliente.DataSource = List;
                        GrdCliente.DataBind();
                    }

                    if (id == 2)
                    {
                        GridProducto.Style["display"] = "";
                        grdProducto.DataSource = List;
                        grdProducto.DataBind();
                    }

                    if (id == 3)
                    {
                        GridTerritorio.Style["display"] = "";
                        GrdTerritorio.DataSource = List;
                        GrdTerritorio.DataBind();
                    }

                    if (id == 4)
                    {
                        GridAscRsc.Style["display"] = "";
                        GrdAscRsc.DataSource = List;
                        GrdAscRsc.DataBind();
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (session == null)
                {
                    if (!Page.IsCallback)
                    {
                        string[] pag = Page.Request.Url.ToString().Trim().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                        Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                        Response.Redirect("login.aspx", false);
                    }
                }
                else
                {
                    if (!IsPostBack)
                    {
                        Session["MonitorActividadesRSCOficina"] = null;
                        Session["MonitorActividadesAsesorOficina"] = null;
                        Session["MonitorActividadesRSCCampo"] = null;
                        Session["MonitorActividadesAsesorCampo"] = null;
                        Session["MonitorFacturacionCliente"] = null;
                        Session["MonitorReporteCaptacion"] = null;
                        Session["MonitorAagendaServicioValor"] = null;
                        Session["Monitortracking"] = null;
                        Session["MonitorGraltracking"] = null;
                        Session["RepCumplViReg"] = null;
                        Session["idRepVentas"] = null;

                        GrdActividadesRSC.DataSource = null;
                        GrdActividadesRSC.DataBind();

                        grdActividadeAsesor.DataSource = null;
                        grdActividadeAsesor.DataBind();

                        GrdCaptacionPedidos.DataSource = null;
                        GrdCaptacionPedidos.DataBind();

                        grdTracking.DataSource = null;
                        grdTracking.DataBind();
                        grdmonitorgeneral.DataSource = null;
                        grdmonitorgeneral.DataBind();
                        GrdCuentaVI.DataSource = null;
                        GrdCuentaVI.DataBind();

                        GrdAscRsc.DataSource = null;
                        GrdAscRsc.DataBind();
                        GrdCliente.DataSource = null;
                        GrdCliente.DataBind();
                        GrdTerritorio.DataSource = null;
                        GrdTerritorio.DataBind();

                        //ValidarPermisos();
                        Llenardatos();

                        txtFechaInicialTracking.Enabled = true;
                        txtFechaFinalTracking.Enabled = true;
                        txtFecha.Enabled = false;

                        txtFechaInicialTracking.ReadOnly = false;
                        txtFechaFinalTracking.ReadOnly = false;
                        txtFecha.ReadOnly = true;
                    }
                }
            }
            catch (Exception ex)
            {
                warning(ex.Message + "- " + new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
            Session["activeMenu"] = 5;
        }

        #endregion

        #region Funciones

        private void Llenardatos()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            int Id_CD = session.Id_Cd;
            List<AgendaRsc> Lista = new List<AgendaRsc>();

            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.DevLlenaCombo(2, Sesion.Id_Emp, Sesion.Id_U, Emp_CnxCentral, "SP_CatCDI_combo2", ref ddlSucursal);
            DllReporteCaptacionPedido.Items.AddRange(ddlSucursal.Items.Cast<Object>().ToArray());
            ddlSucursalRepPerdidaCliente.Items.AddRange(ddlSucursal.Items.Cast<Object>().ToArray());
            dllSucursalSerivioValor.Items.AddRange(ddlSucursal.Items.Cast<Object>().ToArray());
            DllSucursalTracking.Items.AddRange(ddlSucursal.Items.Cast<Object>().ToArray());
            DllSucuralCumplVI.Items.AddRange(ddlSucursal.Items.Cast<Object>().ToArray());

            ddlSucursal.Value = Sesion.Id_Cd_Ver.ToString();
            DllReporteCaptacionPedido.Value = Sesion.Id_Cd_Ver.ToString();
            ddlSucursalRepPerdidaCliente.Value = Sesion.Id_Cd_Ver.ToString();
            dllSucursalSerivioValor.Value = Sesion.Id_Cd_Ver.ToString();
            DllSucursalTracking.Value = Sesion.Id_Cd_Ver.ToString();
            DllSucuralCumplVI.Value = Sesion.Id_Cd_Ver.ToString();

            ddlSucursal.ReadOnly = true;
            DllReporteCaptacionPedido.ReadOnly = true;
            ddlSucursalRepPerdidaCliente.ReadOnly = true;
            dllSucursalSerivioValor.ReadOnly = true;
            DllSucursalTracking.ReadOnly = true;
            DllSucuralCumplVI.ReadOnly = true;

            ddlSucursal.Enabled = false;
            DllReporteCaptacionPedido.Enabled = false;
            ddlSucursalRepPerdidaCliente.Enabled = false;
            dllSucursalSerivioValor.Enabled = false;
            DllSucursalTracking.Enabled = false;
            DllSucuralCumplVI.Enabled = false;

            cargarTipoUsuario();
            CargarUsuario(Id_CD);
            CargarUsuarioAsesor(Id_CD);
            CargarUsuarioTracking(Id_CD);
            CargarUsuarioCumplVi(Id_CD);

            Fecha.Value = session.CalendarioIni;
            FechaFinal.Value = session.CalendarioFin;
            FechaCliente.Value = session.CalendarioFin;
            FechaCaptacionInical.Value = session.CalendarioFin;
            TxtFechaInicialServicioValor.Value = session.CalendarioIni;
            TxtFechaFinalServicioValor.Value = session.CalendarioFin;
            txtFechaInicialTracking.Value = session.CalendarioIni;
            txtFechaFinalTracking.Value = session.CalendarioFin;
            txtFechaCumplVi.Value = session.CalendarioIni;
            cmbAnio.Value = session.CalendarioFin;
            txtFecha.Value = session.CalendarioIni;
        }

        private void cargarTipoUsuario()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CNRSCAgenda Agenda = new CNRSCAgenda();
            List<Agenda> list_Riks = new List<Agenda>();

            Agenda.ConsultarTipoUsuario(ref list_Riks, session.Emp_Cnx);

            DllRol.DataSource = list_Riks;
            DllRol.ValueField = "id_tu";
            DllRol.TextField = "Descripcion";
            DllRol.DataBind();

            DllRolTracking.DataSource = list_Riks;
            DllRolTracking.ValueField = "id_tu";
            DllRolTracking.TextField = "Descripcion";
            DllRolTracking.DataBind();

            DllRolCumplVI.DataSource = list_Riks;
            DllRolCumplVI.ValueField = "id_tu";
            DllRolCumplVI.TextField = "Descripcion";
            DllRolCumplVI.DataBind();

            dllrolVenta.DataSource = list_Riks;
            dllrolVenta.ValueField = "id_tu";
            dllrolVenta.TextField = "Descripcion";
            dllrolVenta.DataBind();

            if (Sesion.Id_TU != 15 && Sesion.Id_TU != 16)
            {
                DllRol.Value = "-1";
                DllRol.ReadOnly = false;
                DllRol.Enabled = true;
                DllRolTracking.Value = "-1";
                DllRolTracking.ReadOnly = false;
                DllRolTracking.Enabled = true;
                DllRolCumplVI.Value = "-1";
                DllRolCumplVI.ReadOnly = false;
                DllRolCumplVI.Enabled = true;

                dllrolVenta.Value = "-1";
                dllrolVenta.ReadOnly = false;
                dllrolVenta.Enabled = true;
            }
            else
            {
                DllRol.Value = Sesion.Id_TU.ToString();
                DllRol.ReadOnly = true;
                DllRol.Enabled = false;
                DllRolTracking.Value = Sesion.Id_TU.ToString();
                DllRolTracking.ReadOnly = true;
                DllRolTracking.Enabled = false;
                DllRolCumplVI.Value = Sesion.Id_TU.ToString();
                DllRolCumplVI.ReadOnly = true;
                DllRolCumplVI.Enabled = false;

                dllrolVenta.Value = Sesion.Id_TU.ToString();
                dllrolVenta.ReadOnly = true;
                dllrolVenta.Enabled = false;
            }
        }

        private void CargarUsuario(int id_Cd)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CNRSCAgenda Rik = new CNRSCAgenda();
            Agenda RegistroRik = new Agenda();

            RegistroRik.Id_Emp = Sesion.Id_Emp;
            RegistroRik.Id_Cd = id_Cd;
            RegistroRik.id_tu = Convert.ToInt32(DllRol.Value.ToString());
            if (Sesion.Id_TU != 15 && Sesion.Id_TU != 16)
            {
                RegistroRik.Id_Usu = -1;
            }
            else
            {
                RegistroRik.Id_Usu = session.Id_U;
            }

            List<Agenda> list_Riks = new List<Agenda>();

            Rik.Consultarusuario(RegistroRik, ref list_Riks, session.Emp_Cnx);

            DllUSuario.DataSource = list_Riks;
            DllUSuario.ValueField = "Id_Usu";
            DllUSuario.TextField = "nombre";
            DllUSuario.DataBind();

            if (Sesion.Id_TU != 15 && Sesion.Id_TU != 16)
            {
                DllUSuario.Value = "-1";
                DllUSuario.ReadOnly = false;
                DllUSuario.Enabled = true;
            }
            else
            {
                DllUSuario.Value = Sesion.Id_U.ToString();
                DllUSuario.ReadOnly = true;
                DllUSuario.Enabled = false;
            }
        }

        private void CargarUsuarioAsesor(int id_Cd)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CNRSCAgenda Rik = new CNRSCAgenda();
            Agenda RegistroRik = new Agenda();

            RegistroRik.Id_Emp = Sesion.Id_Emp;
            RegistroRik.Id_Cd = id_Cd;
            RegistroRik.id_tu = 16;
            if (Sesion.Id_TU != 15 && Sesion.Id_TU != 16)
            {
                RegistroRik.Id_Usu = -1;
            }
            else
            {
                RegistroRik.Id_Usu = session.Id_U;
            }

            List<Agenda> list_Riks = new List<Agenda>();

            Rik.Consultarusuario(RegistroRik, ref list_Riks, session.Emp_Cnx);

            DllUsuarioServicioValor.DataSource = list_Riks;
            DllUsuarioServicioValor.ValueField = "Id_Usu";
            DllUsuarioServicioValor.TextField = "nombre";
            DllUsuarioServicioValor.DataBind();

            if (Sesion.Id_TU != 15 && Sesion.Id_TU != 16)
            {
                DllUsuarioServicioValor.Value = "-1";
                DllUsuarioServicioValor.ReadOnly = false;
                DllUsuarioServicioValor.Enabled = true;
            }
            else
            {
                DllUsuarioServicioValor.Value = Sesion.Id_U.ToString();
                DllUsuarioServicioValor.ReadOnly = true;
                DllUsuarioServicioValor.Enabled = false;
            }
        }

        private void CargarUsuarioTracking(int id_Cd)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CNRSCAgenda Rik = new CNRSCAgenda();
            Agenda RegistroRik = new Agenda();

            RegistroRik.Id_Emp = Sesion.Id_Emp;
            RegistroRik.Id_Cd = id_Cd;
            RegistroRik.id_tu = Convert.ToInt32(DllRolTracking.Value.ToString());
            if (Sesion.Id_TU != 15 && Sesion.Id_TU != 16)
            {
                RegistroRik.Id_Usu = -1;
            }
            else
            {
                RegistroRik.Id_Usu = session.Id_U;
            }

            List<Agenda> list_Riks = new List<Agenda>();

            Rik.Consultarusuario(RegistroRik, ref list_Riks, session.Emp_Cnx);

            dllUsuarioTracking.DataSource = list_Riks;
            dllUsuarioTracking.ValueField = "Id_Usu";
            dllUsuarioTracking.TextField = "nombre";
            dllUsuarioTracking.DataBind();

            if (Sesion.Id_TU != 15 && Sesion.Id_TU != 16)
            {
                dllUsuarioTracking.Value = "-1";
                dllUsuarioTracking.ReadOnly = false;
                dllUsuarioTracking.Enabled = true;
            }
            else
            {
                dllUsuarioTracking.Value = Sesion.Id_U.ToString();
                dllUsuarioTracking.ReadOnly = true;
                dllUsuarioTracking.Enabled = false;
            }
        }

        private void CargarUsuarioCumplVi(int id_Cd)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CNRSCAgenda Rik = new CNRSCAgenda();
            Agenda RegistroRik = new Agenda();

            RegistroRik.Id_Emp = Sesion.Id_Emp;
            RegistroRik.Id_Cd = id_Cd;
            RegistroRik.id_tu = Convert.ToInt32(DllRolCumplVI.Value.ToString());
            if (Sesion.Id_TU != 15 && Sesion.Id_TU != 16)
            {
                RegistroRik.Id_Usu = -1;
            }
            else
            {
                RegistroRik.Id_Usu = session.Id_U;
            }

            List<Agenda> list_Riks = new List<Agenda>();

            Rik.Consultarusuario(RegistroRik, ref list_Riks, session.Emp_Cnx);

            DllUsuarioCumplVi.DataSource = list_Riks;
            DllUsuarioCumplVi.ValueField = "Id_Rik";
            DllUsuarioCumplVi.TextField = "nombre";
            DllUsuarioCumplVi.DataBind();

            if (Sesion.Id_TU != 15 && Sesion.Id_TU != 16)
            {
                DllUsuarioCumplVi.Value = "-1";
                DllUsuarioCumplVi.ReadOnly = false;
                DllUsuarioCumplVi.Enabled = true;
            }
            else
            {
                DllUsuarioCumplVi.Value = Sesion.Id_U.ToString();
                DllUsuarioCumplVi.ReadOnly = true;
                DllUsuarioCumplVi.Enabled = false;
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
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                else
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;

                pagina.Url = "acys/" + pagina.Url;

                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, Sesion.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;
            }
            catch (Exception ex)
            {
                this.Title = "Error";
            }
        }

        private bool ValidarSesion()
        {
            try
            {
                if (session == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cargarDatos()
        {
            int Id_CD = session.Id_Cd;
            List<AgendaRsc> Lista = new List<AgendaRsc>();
            List<AgendaRsc> ListaRSCOficina = new List<AgendaRsc>();
            List<AgendaRsc> ListaRSCCampo = new List<AgendaRsc>();
            List<AgendaRsc> ListaAsesorOficina = new List<AgendaRsc>();
            List<AgendaRsc> ListaAsesorCampo = new List<AgendaRsc>();
            List<AgendaRsc> ListaMonitor = new List<AgendaRsc>();

            AgendaRsc agenda = new AgendaRsc();
            agenda.Id_Emp = session.Id_Emp;
            agenda.Id_Cd = Convert.ToInt32(ddlSucursal.Value);
            agenda.Id_Usu = Convert.ToInt32(DllUSuario.Value.ToString());
            agenda.id_tu = Convert.ToInt32(DllRol.Value.ToString());
            agenda.FechaInicio = Convert.ToDateTime(Fecha.Text);
            agenda.fechaFinal = Convert.ToDateTime(FechaFinal.Text);

            CNRSCAgenda CN = new CNRSCAgenda();
            CN.ConsultarMonitorActividadesGral(agenda, ref Lista, session.Emp_Cnx);
            CN.ConsultarMonitorAgenda(agenda, ref ListaMonitor, session.Emp_Cnx);

            //ListaRSCOficina = Lista.Where(x => x.id_tu == 15).Select(x => x).ToList();
            //ListaAsesorOficina = Lista.Where(x => x.id_tu == 16).Select(x => x).ToList();

            ListaRSCOficina = Lista.Where(x => x.id_tu == 15 && x.lugar == "O").Select(x => x).ToList();
            ListaAsesorOficina = Lista.Where(x => x.id_tu == 16 && x.lugar == "O").Select(x => x).ToList();

            ListaRSCCampo = Lista.Where(x => x.id_tu == 15 && x.lugar == "C").Select(x => x).ToList();
            ListaAsesorCampo = Lista.Where(x => x.id_tu == 16 && x.lugar == "C").Select(x => x).ToList();

            Session["MonitorActividadesRSCOficina"] = ListaRSCOficina;
            Session["MonitorActividadesAsesorOficina"] = ListaAsesorOficina;
            Session["MonitorActividadesRSCCampo"] = ListaRSCCampo;
            Session["MonitorActividadesAsesorCampo"] = ListaAsesorCampo;
            Session["MonitorAagenda"] = ListaMonitor;
            Session["MonitorId_Usu"] = Convert.ToInt32(DllUSuario.Value.ToString());
            Session["Monitorid_tu"] = Convert.ToInt32(DllRol.Value.ToString());
            Session["MonitorIdFechaInicio"] = Convert.ToDateTime(Fecha.Text);
            Session["MonitoridfechaFinal"] = Convert.ToDateTime(FechaFinal.Text);

            if (ListaRSCOficina.Count > 0 || ListaRSCCampo.Count > 0)
            {

                int sumaMinutosOficina = ListaRSCOficina.Sum(x => x.TiempoMinutos);
                int sumaMinutosCampo = ListaRSCCampo.Sum(x => x.TiempoMinutos);
                int sumaMinutos = sumaMinutosOficina + sumaMinutosCampo;
                string hora = CalcularTiempo(sumaMinutos);
                string horaOficina = CalcularTiempo(sumaMinutosOficina);
                string horaCampo = CalcularTiempo(sumaMinutosCampo);

                foreach (AgendaRsc Agenda in ListaRSCOficina)
                {
                    double Tiempo = Convert.ToDouble(Agenda.TiempoMinutos) / Convert.ToDouble(sumaMinutos);
                    Agenda.PorcTiempo = Tiempo;
                }

                foreach (AgendaRsc Agenda in ListaRSCCampo)
                {
                    double Tiempo = Convert.ToDouble(Agenda.TiempoMinutos) / Convert.ToDouble(sumaMinutos);
                    Agenda.PorcTiempo = Tiempo;
                }


                double ProcTotalTiempoOficina = ListaRSCOficina.Sum(x => x.PorcTiempo);
                double ProcTotalTiempoCampo = ListaRSCCampo.Sum(x => x.PorcTiempo);

                double PorcTotalTiempo = ProcTotalTiempoOficina + ProcTotalTiempoCampo;

                rsc.Visible = true;
                GrdActividadesRSC.DataSource = ListaRSCOficina;
                GrdActividadesRSC.DataBind();

                GrdActividadesRSCCampo.DataSource = ListaRSCCampo;
                GrdActividadesRSCCampo.DataBind();
                lblActividadTotalRsc.Text = "Total: " + hora + "  Hr(s) " + PorcTotalTiempo.ToString("p") + " </br> Oficina: " + horaOficina + "  Hr(s) " + ProcTotalTiempoOficina.ToString("p") + " </br> Campo: " + horaCampo + "  Hr(s) " + ProcTotalTiempoCampo.ToString("p");
            }
            else
            {
                rsc.Visible = false;
            }


            if (ListaAsesorOficina.Count > 0 || ListaAsesorCampo.Count > 0)
            {
                int sumaMinutosOficina = ListaAsesorOficina.Sum(x => x.TiempoMinutos);
                int sumaMinutosCampo = ListaAsesorCampo.Sum(x => x.TiempoMinutos);
                int sumaMinutos = sumaMinutosOficina + sumaMinutosCampo;
                string hora = CalcularTiempo(sumaMinutos);
                string horaOficina = CalcularTiempo(sumaMinutosOficina);
                string horaCampo = CalcularTiempo(sumaMinutosCampo);

                foreach (AgendaRsc Agenda in ListaAsesorOficina)
                {
                    double Tiempo = Convert.ToDouble(Agenda.TiempoMinutos) / Convert.ToDouble(sumaMinutos);
                    Agenda.PorcTiempo = Tiempo;
                }

                foreach (AgendaRsc Agenda in ListaAsesorCampo)
                {
                    double Tiempo = Convert.ToDouble(Agenda.TiempoMinutos) / Convert.ToDouble(sumaMinutos);
                    Agenda.PorcTiempo = Tiempo;
                }


                double ProcTotalTiempoOficina = ListaAsesorOficina.Sum(x => x.PorcTiempo);
                double ProcTotalTiempoCampo = ListaAsesorCampo.Sum(x => x.PorcTiempo);

                double PorcTotalTiempo = ProcTotalTiempoOficina + ProcTotalTiempoCampo;

                Asesor.Visible = true;
                grdActividadeAsesor.DataSource = ListaAsesorOficina;
                grdActividadeAsesor.DataBind();

                grdAsesorCampo.DataSource = ListaAsesorCampo;
                grdAsesorCampo.DataBind();
                lblTotalAsesor.Text = "Total: " + hora + "  Hr(s) " + PorcTotalTiempo.ToString("p") + " </br> Oficina: " + horaOficina + "  Hr(s) " + ProcTotalTiempoOficina.ToString("p") + " </br> Campo: " + horaCampo + "  Hr(s) " + ProcTotalTiempoCampo.ToString("p");

            }
            else
            {
                Asesor.Visible = false;
            }

            if (ListaMonitor.Count() > 0)
            {
                ReporteAgenda.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CallFunctionGrafica", "CargarGraficas()", true);
            }
            else
            {
                ReporteAgenda.Visible = false;
            }
        }

        protected void BtnReporteActividades_ServerClick(object sender, EventArgs e)
        {
            cargarDatos();
        }

        protected void GrdSubActividades_BeforePerformDataSelect(object sender, EventArgs e)
        {
            var prod = (sender as BootstrapGridView).GetMasterRowKeyValue();

            if (Session["camposMonitor"] == null)
            {
                string[] array = prod.ToString().Trim().Split('|');
                // array[0] = valor del campo id_ActividadGral 

                if (array[0].ToString().Trim() != "0")
                {
                    if (Session["camposMonitor"] == null)
                    {
                        string ActividadGeneral = array[0].ToString().Trim();

                        BootstrapGridView grid = sender as BootstrapGridView;


                        List<AgendaRsc> Lista = new List<AgendaRsc>();

                        AgendaRsc agenda = new AgendaRsc();
                        agenda.Id_Emp = session.Id_Emp;
                        agenda.Id_Cd = session.Id_Cd;
                        agenda.Id_Usu = Convert.ToInt32(Session["MonitorId_Usu"].ToString());
                        agenda.id_tu = Convert.ToInt32(Session["Monitorid_tu"].ToString());
                        agenda.id_ActividadGral = Convert.ToInt32(ActividadGeneral);
                        agenda.FechaInicio = Convert.ToDateTime(Session["MonitorIdFechaInicio"].ToString());
                        agenda.fechaFinal = Convert.ToDateTime(Session["MonitoridfechaFinal"].ToString());

                        CNRSCAgenda CN = new CNRSCAgenda();
                        CN.ConsultarMonitorActividades(agenda, ref Lista, session.Emp_Cnx);

                        Session["camposMonitor"] = Lista;
                        grid.DataSource = Lista;
                        grid.DataBind();
                    }
                }
            }
            else
            {
                Session["camposMonitor"] = null;
            }
        }

        protected void grdActividadesdesglose_BeforePerformDataSelect(object sender, EventArgs e)
        {
            var prod = (sender as BootstrapGridView).GetMasterRowKeyValue();

            if (Session["camposMonitor2"] == null)
            {
                string[] array = prod.ToString().Trim().Split('|');
                // array[0] = valor del campo id_ActividadGral 
                // array[1] = valor del campo id_Actividad

                if (array[0].ToString().Trim() != "0")
                {
                    if (Session["camposMonitor2"] == null)
                    {
                        string ActividadGeneral = array[0].ToString().Trim();
                        string Actividad = array[1].ToString().Trim();
                        BootstrapGridView grid = sender as BootstrapGridView;

                        List<AgendaRsc> Lista = new List<AgendaRsc>();

                        AgendaRsc agenda = new AgendaRsc();
                        agenda.Id_Emp = session.Id_Emp;
                        agenda.Id_Cd = session.Id_Cd;
                        agenda.Id_Usu = Convert.ToInt32(Session["MonitorId_Usu"].ToString());
                        agenda.id_tu = Convert.ToInt32(Session["Monitorid_tu"].ToString());
                        agenda.id_ActividadGral = Convert.ToInt32(ActividadGeneral);
                        agenda.id_Actividad = Convert.ToInt32(Actividad);
                        agenda.FechaInicio = Convert.ToDateTime(Session["MonitorIdFechaInicio"].ToString());
                        agenda.fechaFinal = Convert.ToDateTime(Session["MonitoridfechaFinal"].ToString());

                        CNRSCAgenda CN = new CNRSCAgenda();
                        CN.ConsultarActividades(agenda, ref Lista, session.Emp_Cnx);

                        Session["camposMonitor2"] = Lista;
                        grid.DataSource = Lista;
                        grid.DataBind();
                    }
                }
            }
            else
            {
                Session["camposMonitor2"] = null;
            }
        }

        protected void BtnConsultaCliente_ServerClick(object sender, EventArgs e)
        {
            int Id_CD = session.Id_Cd;
            List<FacturacionCliente> Lista = new List<FacturacionCliente>();

            FacturacionCliente agenda = new FacturacionCliente();
            agenda.id_emp = session.Id_Emp;
            agenda.id_Cd = Convert.ToInt32(ddlSucursalRepPerdidaCliente.Value);
            agenda.fecha = Convert.ToDateTime(FechaCliente.Text);

            CN_MonitorfacturacionCliente CN = new CN_MonitorfacturacionCliente();
            CN.ConsultarMonitorAgenda(agenda, ref Lista, session.Emp_Cnx);

            Session["MonitorFacturacionCliente"] = Lista;

            if (Lista.Count > 0)
            {
                Cliente.Visible = true;
                GrdFacturacionCliente.DataSource = Lista;
                GrdFacturacionCliente.DataBind();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "CallFunctionGrafica", "CargarGraficas()", true);
            }
            else
            {
                Cliente.Visible = false;
            }
        }

        #endregion

        #region Proceso 

        private string CalcularTiempo(Int32 Minutos)
        {
            if (Minutos != 0)
            {
                Int32 horas = (Minutos / 60);
                Int32 minutos = Minutos - (horas * 60);
                return horas.ToString() + ":" + minutos.ToString().PadLeft(2, '0');
            }
            else
            {
                return "0:00";
            }
        }

        protected void BtnCrearCita_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunctionNew", "NuevaCita()", true);
        }

        protected void BSAgenda_AppointmentViewInfoCustomizing(object sender, DevExpress.Web.ASPxScheduler.AppointmentViewInfoCustomizingEventArgs e)
        {
            //e.ViewInfo.AppointmentStyle.CssClass = "dx-custom-style";
        }

        protected void BSAgenda_PopupMenuShowing(object sender, DevExpress.Web.Bootstrap.BootstrapSchedulerPopupMenuShowingEventArgs e)
        {
            e.Menu.Items.Clear();
        }

        protected void DllRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Id_CD = session.Id_Cd;
            CargarUsuario(Id_CD);

            rsc.Visible = false;
            Asesor.Visible = false;
            ReporteAgenda.Visible = false;
        }

        protected void BtnCliente_ServerClick(object sender, EventArgs e)
        {
            CNRSCAgenda CN = new CNRSCAgenda();
            AgendaRsc datos = new AgendaRsc();
            List<Agenda> List = new List<Agenda>();
            GridViewDataItemTemplateContainer c = ((System.Web.UI.HtmlControls.HtmlButton)sender).NamingContainer as GridViewDataItemTemplateContainer;
            string id = c.Grid.GetRowValues(c.VisibleIndex, "ID").ToString().Trim();
            string id_cte = c.Grid.GetRowValues(c.VisibleIndex, "id_cte").ToString().Trim();
            string fechainicial = c.Grid.GetRowValues(c.VisibleIndex, "inicioEjecucion").ToString().Trim();
            if (Convert.ToInt32(id_cte) != -1)
            {
                int Id_CD = session.Id_Cd;
                List<eClienteBuscar> lst = new List<eClienteBuscar>();
                eClenteBuscar_Params Pms = new eClenteBuscar_Params();
                Pms.Id_Emp = session.Id_Emp;
                Pms.Id_Cd = session.Id_Cd;
                Pms.Id_Rik = -1;
                Pms.Anio = Convert.ToDateTime(fechainicial).Year;
                Pms.Mes = Convert.ToDateTime(fechainicial).Month;
                Pms.Id_Cte = Convert.ToInt32(id_cte);
                Pms.Tipo = 1;
                Pms.CampoOrden = 10;
                Pms.OrdenDir = 0;
                CN.spRepCumplimientoVI_Dinamico(Pms, session.Emp_Cnx, ref lst);

                if (lst.Count > 0)
                {
                    Session["CumpliVIDetalle"] = lst;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirDetalles", "ActividadCLiente()", true);
                }
            }
        }

        protected void btnCordenadas_ServerClick(object sender, EventArgs e)
        {
            CNRSCAgenda CN = new CNRSCAgenda();
            Geolocalizacion geo = new Geolocalizacion();
            List<Geolocalizacion> List = new List<Geolocalizacion>();
            GridViewDataItemTemplateContainer c = ((System.Web.UI.HtmlControls.HtmlButton)sender).NamingContainer as GridViewDataItemTemplateContainer;
            string id = c.Grid.GetRowValues(c.VisibleIndex, "ID").ToString().Trim();


            geo.id_emp = session.Id_Emp;
            geo.ID_Cd = session.Id_Cd;
            geo.Id_Agenda = Convert.ToInt32(id);

            CN.ConsultaragendaGeolocalizacion(geo, ref List, session.Emp_Cnx);
            if (List.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirDetalles", "modalmapa('" + geo.id_emp + "', '" + geo.ID_Cd + "' ,'" + id + "')", true);
            }
        }

        protected void BtnReporteCaptacion_ServerClick(object sender, EventArgs e)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            CN_CapPedidoVtaInst CN = new CN_CapPedidoVtaInst();
            PedidoVtaInst pedido = new PedidoVtaInst();
            List<PedidoVtaInst> List = new List<PedidoVtaInst>();

            pedido.Id_Emp = Sesion.Id_Emp;
            pedido.Id_Cd = Convert.ToInt32(DllReporteCaptacionPedido.Value);
            pedido.FechaInicial = Convert.ToDateTime(FechaCaptacionInical.Value);
            pedido.FechaFinal = Convert.ToDateTime(FechaCaptacionInical.Value);
            pedido.Filtro_tipoPed = ddlTipoPedido.Value == null ? (string)null : ddlTipoPedido.Value.ToString() == "Todos" ? (string)null : ddlTipoPedido.Value.ToString();

            CN.ReporteCaptacionPedido(pedido, session.Emp_Cnx, ref List);

            if (List.Count > 0)
            {
                Session["MonitorReporteCaptacion"] = List;
                GrdCaptacionPedidos.DataSource = List;
                GrdCaptacionPedidos.DataBind();
                CaptacionPedido.Visible = true;


                ScriptManager.RegisterStartupScript(this, this.GetType(), "CallFunctionGrafica", "CargarGraficas()", true);
            }
            else
            {
                CaptacionPedido.Visible = false;
            }
        }

        #endregion

        #region metodosweb
        [WebMethod]
        public static string MonitorAgenda()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];

                if (HttpContext.Current.Session["MonitorAagenda"] != null)
                {
                    List<AgendaRsc> ListaMonitor = new List<AgendaRsc>();
                    ListaMonitor = (List<AgendaRsc>)HttpContext.Current.Session["MonitorAagenda"];

                    string datosProgramadas = "0";
                    string datosRealizadas = "0";
                    string datosRecalendarizadas = "0";
                    string datosCanceladas = "0";
                    string datosTotal = "0";

                    foreach (AgendaRsc agenda in ListaMonitor)
                    {
                        if (agenda.Descripcion.ToString() == "Programadas")
                        {
                            datosProgramadas = agenda.Cantidad.ToString();
                        }
                        if (agenda.Descripcion.ToString() == "Realizadas")
                        {
                            datosRealizadas = agenda.Cantidad.ToString();
                        }
                        if (agenda.Descripcion.ToString() == "Recalendarizadas")
                        {
                            datosRecalendarizadas = agenda.Cantidad.ToString();
                        }
                        if (agenda.Descripcion.ToString() == "Canceladas")
                        {
                            datosCanceladas = agenda.Cantidad.ToString();
                        }
                        if (agenda.Descripcion.ToString() == "Total")
                        {
                            datosTotal = agenda.Cantidad.ToString();
                        }
                    }

                    return JsonConvert.SerializeObject(new { id = 5, datosProgramadas = datosProgramadas, datosRealizadas = datosRealizadas, datosRecalendarizadas = datosRecalendarizadas, datosCanceladas = datosCanceladas, datosTotal = datosTotal });
                }
                else
                {
                    return JsonConvert.SerializeObject(new { id = 2 });
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = ex.Message });
            }
        }

        [WebMethod]
        public static string ReporteFacturacion()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
                if (Sesion == null)
                {
                    return JsonConvert.SerializeObject(new { id = 0 });
                }
                if (HttpContext.Current.Session["MonitorFacturacionCliente"] != null)
                {

                    List<FacturacionCliente> FacturacionLista = new List<FacturacionCliente>();
                    FacturacionLista = (List<FacturacionCliente>)HttpContext.Current.Session["MonitorFacturacionCliente"];

                    List<FacturacionCliente> results = (from p in FacturacionLista
                                                        group p.mes by p.mes into g
                                                        orderby g.Key
                                                        select new FacturacionCliente { mes = g.Key, cantidad = g.Count() }).ToList();

                    string titulo = "";
                    string datos = "";

                    foreach (FacturacionCliente cliente in results)
                    {
                        string NombreMEs = "";

                        if (cliente.mes == 1)
                        {
                            NombreMEs = "Enero";
                        }
                        if (cliente.mes == 2)
                        {
                            NombreMEs = "Febrero";
                        }
                        if (cliente.mes == 3)
                        {
                            NombreMEs = "Marzo";
                        }
                        if (cliente.mes == 4)
                        {
                            NombreMEs = "Abril";
                        }
                        if (cliente.mes == 5)
                        {
                            NombreMEs = "Mayo";
                        }
                        if (cliente.mes == 6)
                        {
                            NombreMEs = "Junio";
                        }
                        if (cliente.mes == 7)
                        {
                            NombreMEs = "Julio";
                        }
                        if (cliente.mes == 8)
                        {
                            NombreMEs = "Agosto";
                        }
                        if (cliente.mes == 9)
                        {
                            NombreMEs = "Septiembre";
                        }
                        if (cliente.mes == 10)
                        {
                            NombreMEs = "Octubre";
                        }
                        if (cliente.mes == 11)
                        {
                            NombreMEs = "Noviembre";
                        }
                        if (cliente.mes == 12)
                        {
                            NombreMEs = "Diciembre";
                        }

                        if (titulo == "")
                        {
                            titulo = NombreMEs;
                        }
                        else
                        {
                            titulo = titulo + "," + NombreMEs;
                        }


                        if (datos == "")
                        {
                            datos = cliente.cantidad.ToString();
                        }
                        else
                        {
                            datos = datos + "," + cliente.cantidad.ToString();
                        }
                    }

                    return JsonConvert.SerializeObject(new { id = 5, titulo = titulo, datos = datos });
                }
                else
                {
                    return JsonConvert.SerializeObject(new { id = 2 });
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = ex.Message });
            }
        }

        [WebMethod]
        public static string ReporteCaptacion()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
                if (Sesion == null)
                {
                    return JsonConvert.SerializeObject(new { id = 0 });
                }
                if (HttpContext.Current.Session["MonitorReporteCaptacion"] != null)
                {

                    List<PedidoVtaInst> List = new List<PedidoVtaInst>();
                    List = (List<PedidoVtaInst>)HttpContext.Current.Session["MonitorReporteCaptacion"];

                    string titulo = "";
                    string datos = "";

                    foreach (PedidoVtaInst Pedido in List)
                    {
                        if (Pedido.Filtro_Frecuencia != "Total")
                        {
                            if (titulo == "")
                            {
                                titulo = Pedido.Filtro_Frecuencia;
                            }
                            else
                            {
                                titulo = titulo + "," + Pedido.Filtro_Frecuencia;
                            }


                            if (datos == "")
                            {
                                datos = Pedido.cantidad.ToString();
                            }
                            else
                            {
                                datos = datos + "," + Pedido.cantidad.ToString();
                            }
                        }
                    }
                    return JsonConvert.SerializeObject(new { id = 5, titulo = titulo, datos = datos });
                }
                else
                {
                    return JsonConvert.SerializeObject(new { id = 2 });
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = ex.Message });
            }
        }

        [WebMethod]
        public static string ReporteCumplimientoServValor()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];

                if (HttpContext.Current.Session["MonitorAagendaServicioValor"] != null)
                {
                    List<AgendaRsc> ListaMonitor = new List<AgendaRsc>();
                    ListaMonitor = (List<AgendaRsc>)HttpContext.Current.Session["MonitorAagendaServicioValor"];

                    string datosProgramadas = "0";
                    string datosRealizadas = "0";
                    string datosRecalendarizadas = "0";
                    string datosCanceladas = "0";
                    string datosTotal = "0";

                    foreach (AgendaRsc agenda in ListaMonitor)
                    {
                        if (agenda.Descripcion.ToString() == "Programadas")
                        {
                            datosProgramadas = agenda.Cantidad.ToString();
                        }
                        if (agenda.Descripcion.ToString() == "Realizadas")
                        {
                            datosRealizadas = agenda.Cantidad.ToString();
                        }
                        if (agenda.Descripcion.ToString() == "Recalendarizadas")
                        {
                            datosRecalendarizadas = agenda.Cantidad.ToString();
                        }
                        if (agenda.Descripcion.ToString() == "Canceladas")
                        {
                            datosCanceladas = agenda.Cantidad.ToString();
                        }
                        if (agenda.Descripcion.ToString() == "Total")
                        {
                            datosTotal = agenda.Cantidad.ToString();
                        }
                    }
                    double pendientes = 0;
                    pendientes = Convert.ToDouble(datosTotal) - Convert.ToDouble(datosRealizadas) - Convert.ToDouble(datosCanceladas);

                    string titulo = "";
                    string datos = "";

                    titulo = "Servicio Pendiente a Atender";
                    titulo = titulo + "," + "Servicios Realizados";
                    datos = pendientes.ToString();
                    datos = datos + "," + datosRealizadas;

                    return JsonConvert.SerializeObject(new { id = 5, titulo = titulo, datos = datos });
                }
                else
                {
                    return JsonConvert.SerializeObject(new { id = 2 });
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = ex.Message });
            }
        }


        [WebMethod]
        public static string ReporteServicioValor()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];

                if (HttpContext.Current.Session["MonitorAagendaServicioValor"] != null)
                {
                    List<AgendaRsc> ListaMonitor = new List<AgendaRsc>();
                    ListaMonitor = (List<AgendaRsc>)HttpContext.Current.Session["MonitorAagendaServicioValor"];

                    string datosCapMHA = "";
                    string datosQuimico = "";
                    string datosAudMha = "";
                    string datosLimpieza = "";
                    string datosProductos = "";

                    string titulo = "Programadas,Realizadas,Recalendarizadas,Canceladas";

                    List<AgendaRsc> programadas = (from tlist in ListaMonitor
                                                   where tlist.Descripcion == "Programadas"
                                                   select tlist).ToList();

                    List<AgendaRsc> Realizadas = (from tlist in ListaMonitor
                                                  where tlist.Descripcion == "Realizadas"
                                                  select tlist).ToList();


                    List<AgendaRsc> Canceladas = (from tlist in ListaMonitor
                                                  where tlist.Descripcion == "Canceladas"
                                                  select tlist).ToList();

                    List<AgendaRsc> Recalendarizadas = (from tlist in ListaMonitor
                                                        where tlist.Descripcion == "Recalendarizadas"
                                                        select tlist).ToList();

                    int PCapMha = 0;
                    int PQuimicos = 0;
                    int PAudMha = 0;
                    int PLimpiza = 0;
                    int PProductos = 0;

                    foreach (AgendaRsc agenda in programadas)
                    {
                        if (agenda.Actividad == "Capacitación MHA")
                        {
                            PCapMha = agenda.Cantidad;
                        }
                        if (agenda.Actividad == "Capacitación químicos")
                        {
                            PQuimicos = agenda.Cantidad;
                        }
                        if (agenda.Actividad == "Auditoria MHA")
                        {
                            PAudMha = agenda.Cantidad;
                        }
                        if (agenda.Actividad == "Auditoria limpieza")
                        {
                            PLimpiza = agenda.Cantidad;
                        }
                        if (agenda.Actividad == "Asesoría productos key, limpieza y MHA")
                        {
                            PProductos = agenda.Cantidad;
                        }
                    }

                    int RCapMha = 0;
                    int RQuimicos = 0;
                    int RAudMha = 0;
                    int RLimpiza = 0;
                    int RProductos = 0;

                    foreach (AgendaRsc agenda in Realizadas)
                    {
                        if (agenda.Actividad == "Capacitación MHA")
                        {
                            RCapMha = agenda.Cantidad;
                        }
                        if (agenda.Actividad == "Capacitación químicos")
                        {
                            RQuimicos = agenda.Cantidad;
                        }
                        if (agenda.Actividad == "Auditoria MHA")
                        {
                            RAudMha = agenda.Cantidad;
                        }
                        if (agenda.Actividad == "Auditoria limpieza")
                        {
                            RLimpiza = agenda.Cantidad;
                        }
                        if (agenda.Actividad == "Asesoría productos key, limpieza y MHA")
                        {
                            RProductos = agenda.Cantidad;
                        }
                    }


                    int CCapMha = 0;
                    int CQuimicos = 0;
                    int CAudMha = 0;
                    int CLimpiza = 0;
                    int CProductos = 0;

                    foreach (AgendaRsc agenda in Canceladas)
                    {
                        if (agenda.Actividad == "Capacitación MHA")
                        {
                            CCapMha = agenda.Cantidad;
                        }
                        if (agenda.Actividad == "Capacitación químicos")
                        {
                            CQuimicos = agenda.Cantidad;
                        }
                        if (agenda.Actividad == "Auditoria MHA")
                        {
                            CAudMha = agenda.Cantidad;
                        }
                        if (agenda.Actividad == "Auditoria limpieza")
                        {
                            CLimpiza = agenda.Cantidad;
                        }
                        if (agenda.Actividad == "Asesoría productos key, limpieza y MHA")
                        {
                            CProductos = agenda.Cantidad;
                        }
                    }


                    int ReCapMha = 0;
                    int ReQuimicos = 0;
                    int ReAudMha = 0;
                    int ReLimpiza = 0;
                    int ReProductos = 0;

                    foreach (AgendaRsc agenda in Recalendarizadas)
                    {
                        if (agenda.Actividad == "Capacitación MHA")
                        {
                            ReCapMha = agenda.Cantidad;
                        }
                        if (agenda.Actividad == "Capacitación químicos")
                        {
                            ReQuimicos = agenda.Cantidad;
                        }
                        if (agenda.Actividad == "Auditoria MHA")
                        {
                            ReAudMha = agenda.Cantidad;
                        }
                        if (agenda.Actividad == "Auditoria limpieza")
                        {
                            ReLimpiza = agenda.Cantidad;
                        }
                        if (agenda.Actividad == "Asesoría productos key, limpieza y MHA")
                        {
                            ReProductos = agenda.Cantidad;
                        }
                    }

                    datosCapMHA = PCapMha.ToString() + "," + RCapMha.ToString() + "," + ReCapMha.ToString() + "," + CCapMha.ToString();
                    datosQuimico = PQuimicos.ToString() + "," + RQuimicos.ToString() + "," + ReQuimicos.ToString() + "," + CQuimicos.ToString();
                    datosAudMha = PAudMha.ToString() + "," + RAudMha.ToString() + "," + ReAudMha.ToString() + "," + CAudMha.ToString();
                    datosLimpieza = PLimpiza.ToString() + "," + RLimpiza.ToString() + "," + ReLimpiza.ToString() + "," + CLimpiza.ToString();
                    datosProductos = PProductos.ToString() + "," + RProductos.ToString() + "," + ReProductos.ToString() + "," + CProductos.ToString();
                    return JsonConvert.SerializeObject(new { id = 5, titulo = titulo, datosCapMHA = datosCapMHA, datosQuimico = datosQuimico, datosAudMha = datosAudMha, datosLimpieza = datosLimpieza, datosProductos = datosProductos });
                }
                else
                {
                    return JsonConvert.SerializeObject(new { id = 2 });
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = ex.Message });
            }
        }

        #endregion

        protected void btnServicioValor_ServerClick(object sender, EventArgs e)
        {
            int Id_CD = session.Id_Cd;
            List<AgendaRsc> ListaMonitor = new List<AgendaRsc>();
            List<AgendaRsc> ListaMonitordetalle = new List<AgendaRsc>();
            AgendaRsc agenda = new AgendaRsc();
            agenda.Id_Emp = session.Id_Emp;
            agenda.Id_Cd = Convert.ToInt32(dllSucursalSerivioValor.Value);
            agenda.Id_Usu = Convert.ToInt32(DllUsuarioServicioValor.Value.ToString());
            agenda.FechaInicio = Convert.ToDateTime(TxtFechaInicialServicioValor.Text);
            agenda.fechaFinal = Convert.ToDateTime(TxtFechaFinalServicioValor.Text);

            CNRSCAgenda CN = new CNRSCAgenda();
            CN.monitorActServicioValor(agenda, ref ListaMonitor, session.Emp_Cnx);
            CN.monitorActServicioValordet(agenda, ref ListaMonitordetalle, session.Emp_Cnx);



            Session["monitorActServicioValordet"] = ListaMonitordetalle;
            Session["MonitorAagendaServicioValor"] = ListaMonitor;
            if (ListaMonitor.Count() > 0)
            {
                divServicioValor.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CallFunctionGrafica", "CargarGraficas()", true);

            }
            else
            {
                divServicioValor.Visible = false;
            }
        }

        protected void btnConsultaTracking_ServerClick(object sender, EventArgs e)
        {
            List<AgendaRsc> Lista = new List<AgendaRsc>();
            List<AgendaRsc> ListaMonitor = new List<AgendaRsc>();
            AgendaRsc agenda = new AgendaRsc();
            if (RBFecha.SelectedValue != "1")
            {
                DateTime fecha1 = DateTime.Parse(txtFecha.Value.ToString());

                agenda.mes = fecha1.Month;
                agenda.anio = fecha1.Year;
            }
            int Id_CD = session.Id_Cd;
            agenda.FechaInicio = Convert.ToDateTime(txtFechaInicialTracking.Text);
            agenda.fechaFinal = Convert.ToDateTime(txtFechaFinalTracking.Text);
            agenda.Id_Emp = session.Id_Emp;
            agenda.Id_Cd = Convert.ToInt32(DllSucursalTracking.Value);
            agenda.Id_Usu = Convert.ToInt32(dllUsuarioTracking.Value.ToString());
            agenda.id_tu = Convert.ToInt32(DllRolTracking.Value.ToString());


            CNRSCAgenda CN = new CNRSCAgenda();
            CN.ConsultarMonitorAgendaDetalle(agenda, ref ListaMonitor, session.Emp_Cnx);

            CN.ConsultaTrackingActividad(agenda, session.Emp_Cnx, ref Lista);
            cargarActividadesGral(ListaMonitor);
            Session["Monitortracking"] = Lista;

            grdTracking.DataSource = Lista;
            grdTracking.DataBind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CallFunctionGrafica", "CargarGraficas()", true);
        }

        protected void DllRolTracking_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Id_CD = session.Id_Cd;
            CargarUsuarioTracking(Id_CD);
        }

        protected void BtnCumplVI_ServerClick(object sender, EventArgs e)
        {
            int Id_CD = session.Id_Cd;
            List<eClienteBuscar> lst = new List<eClienteBuscar>();
            List<eClienteBuscar> lst2 = new List<eClienteBuscar>();
            eClenteBuscar_Params Pms = new eClenteBuscar_Params();
            Pms.Id_Emp = session.Id_Emp;
            Pms.Id_Cd = Convert.ToInt32(DllSucuralCumplVI.Value);
            Pms.Id_Rik = Convert.ToInt32(DllUsuarioCumplVi.Value.ToString());
            Pms.Anio = Convert.ToDateTime(txtFechaCumplVi.Text).Year;
            Pms.Mes = Convert.ToDateTime(txtFechaCumplVi.Text).Month;
            Pms.Tipo = 1;
            Pms.CampoOrden = 10;
            Pms.OrdenDir = 0;


            CNRSCAgenda CN = new CNRSCAgenda();
            CN.spRepCumplimientoVI_Dinamico(Pms, session.Emp_Cnx, ref lst);
            if (lst != null)
            {
                if (lst.Count > 0)
                {

                    eClienteBuscar registro = new eClienteBuscar();
                    registro.descripcionTipoCuenta = "C. Local";
                    registro.CantidadTipoCuenta = lst.Count(x => x.TipoCuenta == 0);
                    lst2.Add(registro);

                    registro = new eClienteBuscar();
                    registro.descripcionTipoCuenta = "C. Nacional";
                    registro.CantidadTipoCuenta = lst.Count(x => x.TipoCuenta == 1);
                    lst2.Add(registro);

                    registro = new eClienteBuscar();
                    registro.descripcionTipoCuenta = "C. Cordinada";
                    registro.CantidadTipoCuenta = lst.Count(x => x.TipoCuenta == 2);
                    lst2.Add(registro);

                    Session["RepCumplViReg"] = lst2;
                    GrdCuentaVI.DataSource = lst2;
                    GrdCuentaVI.DataBind();

                    Session["RepCumplVi"] = lst;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CallFunctionGrafica", "CargarGraficas()", true);
                    RepCumpl.Visible = true;
                }
                else
                {
                    RepCumpl.Visible = false;
                }
            }
        }

        protected void DllRolCumplVI_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Id_CD = session.Id_Cd;
            CargarUsuarioCumplVi(Id_CD);
        }

        [WebMethod]
        public static string ReporteCumpliVI()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];

                if (HttpContext.Current.Session["RepCumplVi"] != null)
                {
                    List<eClienteBuscar> ListaMonitor = new List<eClienteBuscar>();
                    ListaMonitor = (List<eClienteBuscar>)HttpContext.Current.Session["RepCumplVi"];

                    string datosVI = "0";
                    string DatosFacturacion = "0";
                    string PorcCumpli = "0";
                    string VentaMesTotal = "0";
                    datosVI = ListaMonitor.Sum(x => x.VtaInst).ToString("F2");
                    DatosFacturacion = ListaMonitor.Sum(x => x.VtaMes).ToString("F2");
                    PorcCumpli = Convert.ToDouble(ListaMonitor.Average(x => x.PorcMes) * 100).ToString("F0");
                    VentaMesTotal = ListaMonitor.Sum(x => x.VtaMesTot).ToString("F2");
                    return JsonConvert.SerializeObject(new { id = 5, datosVI = datosVI, DatosFacturacion = DatosFacturacion, PorcCumpli = PorcCumpli, VentaMesTotal = VentaMesTotal });
                }
                else
                {
                    return JsonConvert.SerializeObject(new { id = 2 });
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = ex.Message });
            }
        }




        [WebMethod]
        public static string ActServicioValordet(string Estatus)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];

                if (HttpContext.Current.Session["monitorActServicioValordet"] != null)
                {
                    List<AgendaRsc> ListaMonitor = new List<AgendaRsc>();
                    ListaMonitor = (List<AgendaRsc>)HttpContext.Current.Session["monitorActServicioValordet"];

                    List<AgendaRsc> qry = new List<AgendaRsc>();
                    List<AgendaRsc> qry2 = new List<AgendaRsc>();
                    if (Estatus == "0")
                    {
                        qry = (from tlist in ListaMonitor
                               where tlist.inicioEjecucion == ""
                               && tlist.fechaModificacion == ""
                               && tlist.fechaEliminacion == ""
                               select tlist).ToList();

                        qry2 = (from tlist in qry
                                group tlist.Actividad by tlist.Actividad into g
                                orderby g.Key
                                select new AgendaRsc { Actividad = g.Key, Cantidad = g.Count() }).ToList();
                    }
                    else if (Estatus == "1")
                    {
                        qry = (from tlist in ListaMonitor
                               where tlist.inicioEjecucion != ""
                               select tlist).ToList();

                        qry2 = (from tlist in qry
                                group tlist.Actividad by tlist.Actividad into g
                                orderby g.Key
                                select new AgendaRsc { Actividad = g.Key, Cantidad = g.Count() }).ToList();
                    }
                    else if (Estatus == "2")
                    {
                        qry = (from tlist in ListaMonitor
                               where tlist.inicioEjecucion == ""
                               && tlist.fechaModificacion != ""
                               && tlist.fechaEliminacion == ""
                               select tlist).ToList();

                        qry2 = (from tlist in qry
                                group tlist.Actividad by tlist.Actividad into g
                                orderby g.Key
                                select new AgendaRsc { Actividad = g.Key, Cantidad = g.Count() }).ToList();
                    }
                    else if (Estatus == "3")
                    {
                        qry = (from tlist in ListaMonitor
                               where tlist.inicioEjecucion == ""
                               && tlist.fechaEliminacion != ""
                               select tlist).ToList();

                        qry2 = (from tlist in qry
                                group tlist.Actividad by tlist.Actividad into g
                                orderby g.Key
                                select new AgendaRsc { Actividad = g.Key, Cantidad = g.Count() }).ToList();
                    }

                    string actividad = "";
                    string Cantidad = "";

                    foreach (AgendaRsc agenda in qry2)
                    {
                        if (actividad == "")
                        {
                            actividad = agenda.Actividad;
                        }
                        else
                        {
                            actividad = actividad + "," + agenda.Actividad;
                        }

                        if (Cantidad == "")
                        {
                            Cantidad = agenda.Cantidad.ToString();
                        }
                        else
                        {
                            Cantidad = Cantidad + "," + agenda.Cantidad.ToString();
                        }
                    }

                    return JsonConvert.SerializeObject(new { id = 5, Actividad = actividad, Cantidad = Cantidad });
                }
                else
                {
                    return JsonConvert.SerializeObject(new { id = 2 });
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = ex.Message });
            }
        }

        private void cargarActividadesGral(List<AgendaRsc> lista)
        {

            List<actividades> Actividades = new List<actividades>();
            actividades Act = new actividades();

            foreach (AgendaRsc agenda in lista)
            {
                if (agenda.Descripcion == "Programadas a Tiempo")
                {
                    Act.tiempo = agenda.Cantidad.ToString();
                }
                if (agenda.Descripcion == "Programadas Vencidas")
                {
                    Act.vencidas = agenda.Cantidad.ToString();
                }
                if (agenda.Descripcion == "Realizadas")
                {
                    Act.terminadas = agenda.Cantidad.ToString();
                }
                if (agenda.Descripcion == "Ejecucion")
                {
                    Act.ejecucion = agenda.Cantidad.ToString();
                }
                if (agenda.Descripcion == "Canceladas")
                {
                    Act.bajas = agenda.Cantidad.ToString();
                }
                if (agenda.Descripcion == "Total")
                {
                    Act.total = agenda.Cantidad.ToString();
                }
                if (agenda.Descripcion == "Recalendarizadas")
                {
                    Act.Reprogramadas = agenda.Cantidad.ToString();
                }


            }
            Actividades.Add(Act);
            Session["MonitorGraltracking"] = Actividades;
            grdmonitorgeneral.DataSource = Actividades;
            grdmonitorgeneral.DataBind();
        }

        protected void btnExcel_ServerClick(object sender, EventArgs e)
        {
            //grdTracking.ExportXlsToResponse(new XlsExportOptionsEx { ExportType = ExportType.WYSIWYG });


            // Ensure that the data-aware export mode is enabled.
            DevExpress.Export.ExportSettings.DefaultExportType = ExportType.DataAware;
            // Create a new object defining how a document is exported to the XLSX format.
            XlsxExportOptionsEx options = new XlsxExportOptionsEx();
            // Subscribe to the CustomizeSheetHeader event. 
            options.CustomizeSheetHeader += options_CustomizeSheetHeader;
            // Export the grid data to the XLSX format.
            grdTracking.DataColumns["estatus"].Visible = true;
            grdTracking.DataColumns["TipoUsuario"].Visible = true;
            grdTracking.DataColumns["inicioEjecucion"].Visible = true;
            grdTracking.DataColumns["finalEjecucion"].Visible = true;

            string file = "ReporteTracking" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";
            grdTracking.ExportXlsxToResponse(file, options);
            // Open the created document.
            System.Diagnostics.Process.Start(file);

            grdTracking.DataColumns["estatus"].Visible = false;
            grdTracking.DataColumns["TipoUsuario"].Visible = false;
            grdTracking.DataColumns["inicioEjecucion"].Visible = false;
            grdTracking.DataColumns["finalEjecucion"].Visible = false;


        }

        static CellObject CreateCell(object value, XlFormattingObject formatCell)
        {
            return new CellObject { Value = value, Formatting = formatCell };
        }

        // Specify a cell's alignment and font settings. 
        static XlFormattingObject CreateXlFormattingObject(bool bold, double size)
        {
            var cellFormat = new XlFormattingObject
            {
                Font = new XlCellFont
                {
                    Bold = bold,
                    Size = size
                },
                Alignment = new XlCellAlignment
                {
                    RelativeIndent = 10,
                    HorizontalAlignment = XlHorizontalAlignment.Center,
                    VerticalAlignment = XlVerticalAlignment.Center
                }
            };
            return cellFormat;
        }

        void Options_CustomizeSheetHeader(DevExpress.Export.ContextEventArgs e)
        {
            // Create a new row. 

            List<actividades> Lista = (List<actividades>)Session["MonitorGraltracking"];

            if (Lista != null)
            {
                // Create a new row.
                CellObject row = new CellObject();
                // Specify row values.
                row.Value = "Estatus de Actividades";
                // Specify row formatting.
                XlFormattingObject rowFormatting = new XlFormattingObject();
                rowFormatting.Font = new XlCellFont { Bold = true, Size = 12 };
                rowFormatting.Alignment = new DevExpress.Export.Xl.XlCellAlignment { HorizontalAlignment = DevExpress.Export.Xl.XlHorizontalAlignment.Center, VerticalAlignment = DevExpress.Export.Xl.XlVerticalAlignment.Top };
                row.Formatting = rowFormatting;
                // Add the created row to the output document.
                e.ExportContext.AddRow(new[] { row });
                // Add an empty row to the output document.
                e.ExportContext.AddRow();
                // Merge cells of two new rows. 
                e.ExportContext.MergeCells(new DevExpress.Export.Xl.XlCellRange(new DevExpress.Export.Xl.XlCellPosition(0, 0), new DevExpress.Export.Xl.XlCellPosition(6, 0)));


                var cell = CreateXlFormattingObject(true, 14);
                var CellLocation = CreateCell("En Tiempo", cell);
                var CellLocation2 = CreateCell("En Ejecucion", cell);
                var CellLocation3 = CreateCell("Terminadas", cell);
                var CellLocation4 = CreateCell("Total", cell);
                var CellLocation5 = CreateCell("Bajas", cell);
                var CellLocation7 = CreateCell("Reprogramadas", cell);
                var CellLocation6 = CreateCell("Vencidas", cell);
                CellLocation.Formatting.BackColor = System.Drawing.Color.CornflowerBlue;
                CellLocation2.Formatting.BackColor = System.Drawing.Color.CornflowerBlue;
                CellLocation3.Formatting.BackColor = System.Drawing.Color.CornflowerBlue;
                CellLocation4.Formatting.BackColor = System.Drawing.Color.CornflowerBlue;
                CellLocation5.Formatting.BackColor = System.Drawing.Color.CornflowerBlue;
                CellLocation6.Formatting.BackColor = System.Drawing.Color.CornflowerBlue;
                CellLocation7.Formatting.BackColor = System.Drawing.Color.CornflowerBlue;
                e.ExportContext.AddRow(new[] { CellLocation, CellLocation2, CellLocation3, CellLocation5, CellLocation7, CellLocation6, CellLocation4 });

                double total = 0;
                double tiempo = 0;
                double terminadas = 0;
                double ejecucion = 0;
                double bajas = 0;
                double vencidas = 0;
                double reprogramadas = 0;

                CellLocation.Formatting.BackColor = System.Drawing.Color.White;
                CellLocation2.Formatting.BackColor = System.Drawing.Color.White;
                CellLocation3.Formatting.BackColor = System.Drawing.Color.White;
                CellLocation4.Formatting.BackColor = System.Drawing.Color.White;
                CellLocation5.Formatting.BackColor = System.Drawing.Color.White;
                CellLocation6.Formatting.BackColor = System.Drawing.Color.White;
                CellLocation7.Formatting.BackColor = System.Drawing.Color.White;
                foreach (actividades agenda in Lista)
                {

                    total = total + Convert.ToDouble(agenda.total);
                    tiempo = tiempo + Convert.ToDouble(agenda.tiempo);
                    terminadas = terminadas + Convert.ToDouble(agenda.terminadas);
                    ejecucion = ejecucion + Convert.ToDouble(agenda.ejecucion);
                    bajas = bajas + Convert.ToDouble(agenda.bajas);
                    vencidas = vencidas + Convert.ToDouble(agenda.vencidas);
                    reprogramadas = reprogramadas + Convert.ToDouble(agenda.Reprogramadas);

                    CellLocation = CreateCell(agenda.tiempo, cell);
                    CellLocation2 = CreateCell(agenda.ejecucion, cell);
                    CellLocation3 = CreateCell(agenda.terminadas, cell);
                    CellLocation4 = CreateCell(agenda.total, cell);
                    CellLocation5 = CreateCell(agenda.bajas, cell);
                    CellLocation6 = CreateCell(agenda.vencidas, cell);
                    CellLocation7 = CreateCell(agenda.Reprogramadas, cell);
                    e.ExportContext.AddRow(new[] { CellLocation, CellLocation2, CellLocation3, CellLocation5, CellLocation7, CellLocation6, CellLocation4 });
                }
                double Porctotal = total / total;
                double portiempo = tiempo == 0 ? 0 : tiempo / total;
                double porterminadas = terminadas == 0 ? 0 : terminadas / total;
                double porcejecucion = ejecucion == 0 ? 0 : ejecucion / total;
                double porcbaja = bajas == 0 ? 0 : bajas / total;
                double porcvencidad = vencidas == 0 ? 0 : vencidas / total;
                double porcreprogramas = reprogramadas == 0 ? 0 : reprogramadas / total;

                CellLocation = CreateCell(portiempo.ToString("P0"), cell);
                CellLocation2 = CreateCell(porcejecucion.ToString("P0"), cell);
                CellLocation3 = CreateCell(porterminadas.ToString("P0"), cell);
                CellLocation4 = CreateCell(Porctotal.ToString("P0"), cell);
                CellLocation5 = CreateCell(porcbaja.ToString("P0"), cell);
                CellLocation6 = CreateCell(porcvencidad.ToString("P0"), cell);
                CellLocation7 = CreateCell(porcreprogramas.ToString("P0"), cell);
                e.ExportContext.AddRow(new[] { CellLocation, CellLocation2, CellLocation3, CellLocation5, CellLocation7, CellLocation6, CellLocation4 });

                e.ExportContext.AddRow();
                e.ExportContext.AddRow();
            }
        }

        double tiempo = 0;
        double ejecucion = 0;
        double terminadas = 0;
        double bajas = 0;
        double Reprogramadas = 0;
        double vencidas = 0;
        double TotalPerc = 0;



        protected void grdmonitorgeneral_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if (Session["MonitorGraltracking"] != null)
            {
                List<actividades> Lista = (List<actividades>)Session["MonitorGraltracking"];
                if (Lista.First().total == null)
                {
                    TotalPerc = 0;
                }
                else
                {
                    TotalPerc = Convert.ToDouble(Lista.First().total.ToString());
                }
                if (e.IsTotalSummary)
                {
                    if ((e.Item as ASPxSummaryItem).Tag == "tiempo")
                    {
                        if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                        {
                            tiempo = 0;
                        }
                        else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                        {
                            if (e.GetValue("tiempo") != null)
                            {
                                tiempo += Convert.ToDouble(e.GetValue("tiempo").ToString().Trim());
                            }
                        }
                        else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                        {
                            if (tiempo == 0)
                            {
                                e.TotalValue = 0;
                            }
                            else
                            {
                                e.TotalValue = tiempo > 0 ? tiempo / TotalPerc : 0;
                            }
                        }
                    }
                    if ((e.Item as ASPxSummaryItem).Tag == "ejecucion")
                    {
                        if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                        {
                            ejecucion = 0;
                        }
                        else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                        {
                            if (e.GetValue("ejecucion") != null)
                            {
                                ejecucion += Convert.ToDouble(e.GetValue("ejecucion").ToString().Trim());
                            }
                        }
                        else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                        {
                            if (ejecucion == 0)
                            {
                                e.TotalValue = 0;
                            }
                            else
                            {
                                e.TotalValue = ejecucion > 0 ? ejecucion / TotalPerc : 0;
                            }
                        }
                    }
                    if ((e.Item as ASPxSummaryItem).Tag == "terminadas")
                    {
                        if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                        {
                            terminadas = 0;
                        }
                        else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                        {
                            if (e.GetValue("terminadas") != null)
                            {
                                terminadas += Convert.ToDouble(e.GetValue("terminadas").ToString().Trim());
                            }
                        }
                        else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                        {
                            if (terminadas == 0)
                            {
                                e.TotalValue = 0;
                            }
                            else
                            {
                                e.TotalValue = terminadas > 0 ? terminadas / TotalPerc : 0;
                            }
                        }
                    }
                    if ((e.Item as ASPxSummaryItem).Tag == "bajas")
                    {
                        if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                        {
                            bajas = 0;
                        }
                        else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                        {
                            if (e.GetValue("bajas") != null)
                            {
                                bajas += Convert.ToDouble(e.GetValue("bajas").ToString().Trim());
                            }
                        }
                        else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                        {
                            if (bajas == 0)
                            {
                                e.TotalValue = 0;
                            }
                            else
                            {
                                e.TotalValue = bajas > 0 ? bajas / TotalPerc : 0;
                            }
                        }
                    }
                    if ((e.Item as ASPxSummaryItem).Tag == "Reprogramadas")
                    {
                        if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                        {
                            Reprogramadas = 0;
                        }
                        else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                        {
                            if (e.GetValue("Reprogramadas") != null)
                            {
                                Reprogramadas += Convert.ToDouble(e.GetValue("Reprogramadas").ToString().Trim());
                            }
                        }
                        else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                        {
                            if (Reprogramadas == 0)
                            {
                                e.TotalValue = 0;
                            }
                            else
                            {
                                e.TotalValue = Reprogramadas > 0 ? Reprogramadas / TotalPerc : 0;
                            }
                        }
                    }
                    if ((e.Item as ASPxSummaryItem).Tag == "vencidas")
                    {
                        if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                        {
                            vencidas = 0;
                        }
                        else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                        {
                            if (e.GetValue("vencidas") != null)
                            {
                                vencidas += Convert.ToDouble(e.GetValue("vencidas").ToString().Trim());
                            }
                        }
                        else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                        {
                            if (vencidas == 0)
                            {
                                e.TotalValue = 0;
                            }
                            else
                            {
                                e.TotalValue = vencidas > 0 ? vencidas / TotalPerc : 0;
                            }
                        }
                    }
                    if ((e.Item as ASPxSummaryItem).Tag == "total")
                    {
                        if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                        {
                            TotalPerc = 0;
                        }
                        else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                        {
                            if (e.GetValue("total") != null)
                            {
                                TotalPerc += Convert.ToDouble(e.GetValue("total").ToString().Trim());
                            }
                        }
                        else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                        {
                            if (vencidas == 0)
                            {
                                e.TotalValue = 0;
                            }
                            else
                            {
                                e.TotalValue = TotalPerc > 0 ? TotalPerc / TotalPerc : 0;
                            }
                        }
                    }
                }
            }
        }

        protected void btnreporteCaptacion_ServerClick1(object sender, EventArgs e)
        {
            GrdCaptacionPedidos.ExportXlsToResponse(new XlsExportOptionsEx { ExportType = ExportType.WYSIWYG });
        }

        protected void GrdCaptacionPedidos_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if (e.IsTotalSummary)
            {
                if ((e.Item as ASPxSummaryItem).Tag == "Filtro_Frecuencia")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        e.TotalValue = "Total:";
                    }
                }
            }
        }


        private void Mostrar()
        {
            #region Captura de valores
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            string nombreEmpresa = sesion.Emp_Nombre;
            string nombreSucursal = sesion.Cd_Nombre;
            DateTime Fechalocal = DateTime.Now;
            int error = 0;
            VenEstadisticaVentas ventas = new VenEstadisticaVentas();
            ventas.Id_Cd = sesion.Id_Cd_Ver;
            ventas.id_emp = sesion.Id_Emp;
            //radioButton Filtro-- 1
            if (RBVenta.SelectedValue == "4")
            {
                ventas.Filtro = 0;
                ventas.SFiltro = RBVenta.SelectedItem.Text;
            }
            if (RBVenta.SelectedValue == "3")
            {
                ventas.Filtro = 1;
                ventas.SFiltro = RBVenta.SelectedItem.Text;
            }
            if (RBVenta.SelectedValue == "1")
            {
                ventas.Filtro = 2;
                ventas.SFiltro = RBVenta.SelectedItem.Text;
            }
            if (RBVenta.SelectedValue == "2")
            {
                ventas.Filtro = 3;
                ventas.SFiltro = RBVenta.SelectedItem.Text;
            }

            ventas.id_tu = Convert.ToInt32(dllrolVenta.Value.ToString());


            //combo Año
            int año = -1;
            int añocurso = Convert.ToDateTime(cmbAnio.Value.ToString()).Year;
            int.TryParse(añocurso.ToString(), out año);
            if (año > 0)
            {
                ventas.Anio = año;
                ventas.SAnio = añocurso.ToString();
            }

            // valor pesos
            ventas.Mostrar = 1;

            //checkBox Cliente   
            ventas.Nivel = 0;
            ventas.Nivel2 = 0;

            #endregion

            #region datos de filtros
            if (ventas.FiltroSem == 1)
            {//Ventas por semana  --4

                if (ventas.Filtro == 0)
                {//Representante
                    if (ventas.Nivel == 0 && ventas.Nivel2 == 0)
                    {//ni clientes,sin producto -- a - b
                        switch (ventas.Mostrar)
                        {
                            case 1://pesos - x 
                                ventas.Reporte = 16;
                                break;
                            case 2://unidades - y 
                                ventas.Reporte = 17;
                                break;
                        }


                        ventas.Encabezado = "Núm.";
                        ventas.Encabezado1 = "Representante";
                    }
                    if (ventas.Nivel == 1 && ventas.Nivel2 == 0)
                    {//ni clientes,sin producto -- a - b
                        switch (ventas.Mostrar)
                        {
                            case 1://pesos - x 
                                ventas.Reporte = 18;
                                break;
                            case 2://unidades - y 
                                ventas.Reporte = 19;
                                break;
                        }


                        ventas.Encabezado = "Núm.";
                        ventas.Encabezado1 = "Cliente";
                    }

                    if (ventas.Nivel == 1 && ventas.Nivel2 == 1)
                    {//ni clientes,sin producto -- a - b
                        switch (ventas.Mostrar)
                        {
                            case 1://pesos - x 
                                ventas.Reporte = 20;
                                break;
                            case 2://unidades - y 
                                ventas.Reporte = 21;
                                break;
                        }


                        ventas.Encabezado = "Núm.";
                        ventas.Encabezado1 = "Producto";
                    }


                }
                if (ventas.Filtro == 1)
                {//Territorio      -- 1          
                    if (ventas.Nivel == 0 && ventas.Nivel2 == 0)
                    {//ni clientes,sin producto -- a - b
                        switch (ventas.Mostrar)
                        {
                            case 1://pesos - x 
                                ventas.Reporte = 1;
                                break;
                            case 2://unidades - y 
                                ventas.Reporte = 2;
                                break;
                            case 3: //ambos
                                ventas.Reporte = 15;
                                break;
                        }

                        ventas.Encabezado = "Núm.";
                        ventas.Encabezado1 = "Territorio";
                    }
                    if (ventas.Nivel == 1 && ventas.Nivel2 == 0)
                    {//clientes - a
                        switch (ventas.Mostrar)
                        {
                            case 1://pesos - x 
                                ventas.Reporte = 3;
                                break;
                            case 2://unidades - y 
                                ventas.Reporte = 4;
                                break;
                            case 3: //ambos
                                ventas.Reporte = 16;
                                break;
                        }
                        ventas.Encabezado = "Núm.";
                        ventas.Encabezado1 = "Cliente";
                    }

                    if (ventas.Nivel == 0 && ventas.Nivel2 == 1)
                    {//productos - b
                        switch (ventas.Mostrar)
                        {
                            case 1://pesos - x 
                                ventas.Reporte = 5;
                                break;
                            case 2://unidades - y 
                                ventas.Reporte = 6;
                                break;
                            case 3: //ambos
                                ventas.Reporte = 17;
                                break;
                        }
                        ventas.Encabezado = "Núm.";
                        ventas.Encabezado1 = "Producto";
                    }
                    if (ventas.Nivel == 1 && ventas.Nivel2 == 1)
                    {//clientes - a , productos - b
                        switch (ventas.Mostrar)
                        {
                            case 1://pesos - x 
                                ventas.Reporte = 7;
                                break;
                            case 2://unidades - y 
                                ventas.Reporte = 8;
                                break;
                            case 3: //ambos
                                ventas.Reporte = 18;
                                break;
                        }
                        ventas.Encabezado = "Núm.";
                        ventas.Encabezado1 = "Producto";
                    }
                }

                if (ventas.Filtro == 2)
                {//Cliente      -- 2
                    if (ventas.Nivel == 0 && ventas.Nivel2 == 0)
                    {//ni clientes,sin producto -- a - b
                        switch (ventas.Mostrar)
                        {
                            case 1://pesos - x 
                                ventas.Reporte = 9;
                                break;
                            case 2://unidades - y 
                                ventas.Reporte = 10;
                                break;
                            case 3: //ambos
                                ventas.Reporte = 19;
                                break;
                        }
                        ventas.Encabezado = "Núm.";
                        ventas.Encabezado1 = "Cliente";
                    }
                    if (ventas.Nivel == 0 && ventas.Nivel2 == 1)
                    {//productos - b
                        switch (ventas.Mostrar)
                        {
                            case 1://pesos - x 
                                ventas.Reporte = 11;
                                break;
                            case 2://unidades - y 
                                ventas.Reporte = 12;
                                break;
                            case 3: //ambos
                                ventas.Reporte = 20;
                                break;
                        }
                        ventas.Encabezado = "Núm.";
                        ventas.Encabezado1 = "Producto";
                    }
                }

                if (ventas.Filtro == 3)
                {//Productos -- 3
                    if (ventas.Nivel == 0 && ventas.Nivel2 == 0)
                    {//ni clientes,sin producto -- a - b
                        switch (ventas.Mostrar)
                        {
                            case 1://pesos - x 
                                ventas.Reporte = 13;
                                break;
                            case 2://unidades - y 
                                ventas.Reporte = 14;
                                break;
                            case 3: //ambos
                                ventas.Reporte = 21;
                                break;
                        }
                        ventas.Encabezado = "Núm.";
                        ventas.Encabezado1 = "Producto";
                    }
                }
            }
            else
            {
                if (ventas.Filtro == 0)
                {//Representante 
                    if (ventas.Nivel == 0 && ventas.Nivel2 == 0)
                    {//ni clientes,sin producto -- a - b
                        switch (ventas.Mostrar)
                        {
                            case 1://pesos - x 
                                ventas.Reporte = 16;
                                break;
                            case 2://unidades - y 
                                ventas.Reporte = 17;
                                break;
                        }


                        ventas.Encabezado = "Núm.";
                        ventas.Encabezado1 = "Representante";
                    }
                    if (ventas.Nivel == 1 && ventas.Nivel2 == 0)
                    {//ni clientes,sin producto -- a - b
                        switch (ventas.Mostrar)
                        {
                            case 1://pesos - x 
                                ventas.Reporte = 18;
                                break;
                            case 2://unidades - y 
                                ventas.Reporte = 19;
                                break;
                        }
                        ventas.Encabezado = "Núm.";
                        ventas.Encabezado1 = "Cliente";
                    }

                    if (ventas.Nivel == 1 && ventas.Nivel2 == 1)
                    {//ni clientes,sin producto -- a - b
                        switch (ventas.Mostrar)
                        {
                            case 1://pesos - x 
                                ventas.Reporte = 20;
                                break;
                            case 2://unidades - y 
                                ventas.Reporte = 21;
                                break;
                        }


                        ventas.Encabezado = "Núm.";
                        ventas.Encabezado1 = "Producto";
                    }


                }

                if (ventas.Filtro == 1)
                {//Territorio      -- 1          
                    if (ventas.Nivel == 0 && ventas.Nivel2 == 0)
                    {//ni clientes,sin producto -- a - b
                        switch (ventas.Mostrar)
                        {
                            case 1://pesos - x 
                                ventas.Reporte = 1;
                                break;
                            case 2://unidades - y 
                                ventas.Reporte = 2;
                                break;
                            case 3: //ambos
                                ventas.Reporte = 15;
                                break;
                        }

                        ventas.Encabezado = "Núm.";
                        ventas.Encabezado1 = "Territorio";
                    }
                    if (ventas.Nivel == 1 && ventas.Nivel2 == 0)
                    {//clientes - a
                        switch (ventas.Mostrar)
                        {
                            case 1://pesos - x 
                                ventas.Reporte = 3;
                                break;
                            case 2://unidades - y 
                                ventas.Reporte = 4;
                                break;
                            case 3: //ambos
                                ventas.Reporte = 16;
                                break;
                        }
                        ventas.Encabezado = "Núm.";
                        ventas.Encabezado1 = "Cliente";
                    }

                    if (ventas.Nivel == 0 && ventas.Nivel2 == 1)
                    {//productos - b
                        switch (ventas.Mostrar)
                        {
                            case 1://pesos - x 
                                ventas.Reporte = 5;
                                break;
                            case 2://unidades - y 
                                ventas.Reporte = 6;
                                break;
                            case 3: //ambos
                                ventas.Reporte = 17;
                                break;
                        }
                        ventas.Encabezado = "Núm.";
                        ventas.Encabezado1 = "Producto";
                    }
                    if (ventas.Nivel == 1 && ventas.Nivel2 == 1)
                    {//clientes - a , productos - b
                        switch (ventas.Mostrar)
                        {
                            case 1://pesos - x 
                                ventas.Reporte = 7;
                                break;
                            case 2://unidades - y 
                                ventas.Reporte = 8;
                                break;
                            case 3: //ambos
                                ventas.Reporte = 18;
                                break;
                        }
                        ventas.Encabezado = "Núm.";
                        ventas.Encabezado1 = "Producto";
                    }
                }

                if (ventas.Filtro == 2)
                {//Cliente      -- 2
                    if (ventas.Nivel == 0 && ventas.Nivel2 == 0)
                    {//ni clientes,sin producto -- a - b
                        switch (ventas.Mostrar)
                        {
                            case 1://pesos - x 
                                ventas.Reporte = 9;
                                break;
                            case 2://unidades - y 
                                ventas.Reporte = 10;
                                break;
                            case 3: //ambos
                                ventas.Reporte = 19;
                                break;
                        }
                        ventas.Encabezado = "Núm.";
                        ventas.Encabezado1 = "Cliente";
                    }
                    if (ventas.Nivel == 0 && ventas.Nivel2 == 1)
                    {//productos - b
                        switch (ventas.Mostrar)
                        {
                            case 1://pesos - x 
                                ventas.Reporte = 11;
                                break;
                            case 2://unidades - y 
                                ventas.Reporte = 12;
                                break;
                            case 3: //ambos
                                ventas.Reporte = 20;
                                break;
                        }
                        ventas.Encabezado = "Núm.";
                        ventas.Encabezado1 = "Producto";
                    }
                }

                if (ventas.Filtro == 3)
                {//Productos -- 3
                    if (ventas.Nivel == 0 && ventas.Nivel2 == 0)
                    {//ni clientes,sin producto -- a - b
                        switch (ventas.Mostrar)
                        {
                            case 1://pesos - x 
                                ventas.Reporte = 13;
                                break;
                            case 2://unidades - y 
                                ventas.Reporte = 14;
                                break;
                            case 3: //ambos
                                ventas.Reporte = 21;
                                break;
                        }
                        ventas.Encabezado = "Núm.";
                        ventas.Encabezado1 = "Producto";
                    }
                }
            }
            #endregion 

            CNRSCAgenda cn = new CNRSCAgenda();
            List<VenEstadisticaVentas> List = new List<VenEstadisticaVentas>();
            cn.VentaServicioAnual(ventas, ref List, sesion.Emp_Cnx);

            GridProducto.Style["display"] = "none";
            GridCliente.Style["display"] = "none";
            GridTerritorio.Style["display"] = "none";
            GridAscRsc.Style["display"] = "none";

            Session["RepVentas"] = List;
            Session["idRepVentas"] = 1;
            if (ventas.Reporte == 9 || ventas.Reporte == 10 || ventas.Reporte == 19)
            {
                Session["idRepVentas"] = 1;
                GridCliente.Style["display"] = "";
                GrdCliente.DataSource = List;
                GrdCliente.DataBind();
            }

            if (ventas.Reporte == 13 || ventas.Reporte == 14 || ventas.Reporte == 21)
            {
                Session["idRepVentas"] = 2;
                GridProducto.Style["display"] = "";
                grdProducto.DataSource = List;
                grdProducto.DataBind();
            }

            if (ventas.Reporte == 1 || ventas.Reporte == 2 || ventas.Reporte == 15)
            {
                Session["idRepVentas"] = 3;
                GridTerritorio.Style["display"] = "";
                GrdTerritorio.DataSource = List;
                GrdTerritorio.DataBind();
            }

            if (ventas.Reporte == 16 || ventas.Reporte == 17)
            {
                Session["idRepVentas"] = 4;
                GridAscRsc.Style["display"] = "";
                GrdAscRsc.DataSource = List;
                GrdAscRsc.DataBind();
            }
        }

        private void boton(string cadena, ref int error)
        {
            if (!string.IsNullOrEmpty(cadena))
            {
                string[] split = cadena.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                string[] split2;
                foreach (string a in split)
                {
                    if (a.Contains("-"))
                    {
                        split2 = a.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                        if (split2.Length != 2)
                        {
                            info("El rango " + a.ToString() + " no es válido");
                            error = 1;
                        }
                        if (split2.Length == 2)
                            if (Convert.ToInt32(split2[0]) > Convert.ToInt32(split2[1]))
                            {
                                info("El rango " + a.ToString() + " no es válido");
                                error = 1;
                            }
                    }
                }
            }
        }

        protected void btnReporteVentas_ServerClick(object sender, EventArgs e)
        {

            try
            {
                if (!string.IsNullOrEmpty(cmbAnio.Value.ToString()))
                    if (cmbAnio.Value.ToString() != "-1")
                    {
                        Mostrar();
                    }
                    else
                    {
                        info("Ingresar un año válido");
                    }
                else
                {
                    info("Ingresar un año válido");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnReportePedido_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirReporteVI", "AbrirReporteVI()", true);

        }

        protected void BtnImprimirReporteVentas_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (Session["idRepVentas"] != null)
                {
                    if (Session["idRepVentas"] != null)
                    {

                        int id = Convert.ToInt32(Session["idRepVentas"].ToString());

                        // Ensure that the data-aware export mode is enabled.
                        DevExpress.Export.ExportSettings.DefaultExportType = ExportType.DataAware;
                        // Create a new object defining how a document is exported to the XLSX format.
                        XlsExportOptionsEx options = new XlsExportOptionsEx();
                        // Subscribe to the CustomizeSheetHeader event. 
                        options.CustomizeSheetHeader += options_CustomizeSheetHeaderserv;

                        // Export the grid data to the XLSX format.
                        string file = "Rep_Est_VentaServicio_" + RBVenta.SelectedItem.Text.ToString() + ".xlsx";

                        if (id == 1)
                        {
                            GrdCliente.ExportXlsToResponse(file, options);
                        }

                        if (id == 2)
                        {
                            grdProducto.ExportXlsToResponse(file, options);
                        }

                        if (id == 3)
                        {
                            GrdTerritorio.ExportXlsToResponse(file, options);
                        }

                        if (id == 4)
                        {
                            GrdAscRsc.ExportXlsToResponse(file, options);
                        }

                        System.Diagnostics.Process.Start(file);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void options_CustomizeSheetHeaderserv(DevExpress.Export.ContextEventArgs e)
        {
            // Create a new row.
            CellObject row = new CellObject();
            // Specify row values.
            row.Value = "KEY QUÍMICA, S.A. de C.V.";
            // Specify row formatting.
            XlFormattingObject rowFormatting = new XlFormattingObject();
            rowFormatting.Font = new XlCellFont { Bold = true, Size = 12 };
            rowFormatting.Alignment = new DevExpress.Export.Xl.XlCellAlignment { HorizontalAlignment = DevExpress.Export.Xl.XlHorizontalAlignment.Left, VerticalAlignment = DevExpress.Export.Xl.XlVerticalAlignment.Top };
            row.Formatting = rowFormatting;
            // Add the created row to the output document.
            e.ExportContext.AddRow(new[] { row });
            // Merge cells of two new rows. 
            e.ExportContext.MergeCells(new DevExpress.Export.Xl.XlCellRange(new DevExpress.Export.Xl.XlCellPosition(0, 0), new DevExpress.Export.Xl.XlCellPosition(3, 0)));
            // Create a new row.

            // Create a new row.
            row = new CellObject();
            // Specify row values.
            row.Value = session.Cd_Nombre.ToString();
            // Specify row formatting.
            rowFormatting = new XlFormattingObject();
            rowFormatting.Font = new XlCellFont { Bold = true, Size = 12 };
            rowFormatting.Alignment = new DevExpress.Export.Xl.XlCellAlignment { HorizontalAlignment = DevExpress.Export.Xl.XlHorizontalAlignment.Left, VerticalAlignment = DevExpress.Export.Xl.XlVerticalAlignment.Top };
            row.Formatting = rowFormatting;
            // Add the created row to the output document.
            e.ExportContext.AddRow(new[] { row });
            // Merge cells of two new rows. 
            e.ExportContext.MergeCells(new DevExpress.Export.Xl.XlCellRange(new DevExpress.Export.Xl.XlCellPosition(0, 0), new DevExpress.Export.Xl.XlCellPosition(3, 0)));

            // Create a new row.
            row = new CellObject();
            // Specify row values.
            row.Value = "Estadística de ventas de Servicio";
            // Specify row formatting.
            rowFormatting = new XlFormattingObject();
            rowFormatting.Font = new XlCellFont { Bold = true, Size = 12 };
            rowFormatting.Alignment = new DevExpress.Export.Xl.XlCellAlignment { HorizontalAlignment = DevExpress.Export.Xl.XlHorizontalAlignment.Left, VerticalAlignment = DevExpress.Export.Xl.XlVerticalAlignment.Top };
            row.Formatting = rowFormatting;
            // Add the created row to the output document.
            e.ExportContext.AddRow(new[] { row });
            // Merge cells of two new rows. 
            e.ExportContext.MergeCells(new DevExpress.Export.Xl.XlCellRange(new DevExpress.Export.Xl.XlCellPosition(0, 0), new DevExpress.Export.Xl.XlCellPosition(3, 0)));
            // Create a new row.

            // Create a new row.
            row = new CellObject();
            // Specify row values.
            row.Value = "Usuario:" + " " + session.U_Nombre.ToString();
            // Specify row formatting.
            rowFormatting = new XlFormattingObject();
            rowFormatting.Font = new XlCellFont { Bold = true, Size = 12 };
            rowFormatting.Alignment = new DevExpress.Export.Xl.XlCellAlignment { HorizontalAlignment = DevExpress.Export.Xl.XlHorizontalAlignment.Left, VerticalAlignment = DevExpress.Export.Xl.XlVerticalAlignment.Top };
            row.Formatting = rowFormatting;
            // Add the created row to the output document.
            e.ExportContext.AddRow(new[] { row });
            // Merge cells of two new rows. 
            e.ExportContext.MergeCells(new DevExpress.Export.Xl.XlCellRange(new DevExpress.Export.Xl.XlCellPosition(0, 1), new DevExpress.Export.Xl.XlCellPosition(3, 1)));
            // Create a new row.

            // Create a new row.
            row = new CellObject();
            // Specify row values.
            row.Value = "Fecha:" + " " + DateTime.Now.ToString("dd/MM/yyyy  HH:mm:ss");
            // Specify row formatting.
            rowFormatting = new XlFormattingObject();
            rowFormatting.Font = new XlCellFont { Bold = true, Size = 12 };
            rowFormatting.Alignment = new DevExpress.Export.Xl.XlCellAlignment { HorizontalAlignment = DevExpress.Export.Xl.XlHorizontalAlignment.Left, VerticalAlignment = DevExpress.Export.Xl.XlVerticalAlignment.Top };
            row.Formatting = rowFormatting;
            // Add the created row to the output document.
            e.ExportContext.AddRow(new[] { row });
            // Merge cells of two new rows. 
            e.ExportContext.MergeCells(new DevExpress.Export.Xl.XlCellRange(new DevExpress.Export.Xl.XlCellPosition(0, 2), new DevExpress.Export.Xl.XlCellPosition(3, 2)));
            // Create a new row.


            // Create a new row.
            row = new CellObject();
            // Specify row values.
            row.Value = "Ordenar Por:" + " " + RBVenta.SelectedItem.Text.ToString();
            // Specify row formatting.
            rowFormatting = new XlFormattingObject();
            rowFormatting.Font = new XlCellFont { Bold = true, Size = 12 };
            rowFormatting.Alignment = new DevExpress.Export.Xl.XlCellAlignment { HorizontalAlignment = DevExpress.Export.Xl.XlHorizontalAlignment.Left, VerticalAlignment = DevExpress.Export.Xl.XlVerticalAlignment.Top };
            row.Formatting = rowFormatting;
            // Add the created row to the output document.
            e.ExportContext.AddRow(new[] { row });
            // Merge cells of two new rows. 
            e.ExportContext.MergeCells(new DevExpress.Export.Xl.XlCellRange(new DevExpress.Export.Xl.XlCellPosition(0, 3), new DevExpress.Export.Xl.XlCellPosition(3, 3)));
            // Create a new row.

            // Create a new row.
            row = new CellObject();
            // Specify row values.
            row.Value = "Rol:" + " " + dllrolVenta.Text.ToString();
            // Specify row formatting.
            rowFormatting = new XlFormattingObject();
            rowFormatting.Font = new XlCellFont { Bold = true, Size = 12 };
            rowFormatting.Alignment = new DevExpress.Export.Xl.XlCellAlignment { HorizontalAlignment = DevExpress.Export.Xl.XlHorizontalAlignment.Left, VerticalAlignment = DevExpress.Export.Xl.XlVerticalAlignment.Top };
            row.Formatting = rowFormatting;
            // Add the created row to the output document.
            e.ExportContext.AddRow(new[] { row });
            // Merge cells of two new rows. 
            e.ExportContext.MergeCells(new DevExpress.Export.Xl.XlCellRange(new DevExpress.Export.Xl.XlCellPosition(0, 4), new DevExpress.Export.Xl.XlCellPosition(3, 4)));
            // Create a new row.


            // Create a new row.
            row = new CellObject();
            // Specify row values.
            row.Value = "Año:" + " " + cmbAnio.Text.ToString();
            // Specify row formatting.
            rowFormatting = new XlFormattingObject();
            rowFormatting.Font = new XlCellFont { Bold = true, Size = 12 };
            rowFormatting.Alignment = new DevExpress.Export.Xl.XlCellAlignment { HorizontalAlignment = DevExpress.Export.Xl.XlHorizontalAlignment.Left, VerticalAlignment = DevExpress.Export.Xl.XlVerticalAlignment.Top };
            row.Formatting = rowFormatting;
            // Add the created row to the output document.
            e.ExportContext.AddRow(new[] { row });
            // Merge cells of two new rows. 
            e.ExportContext.MergeCells(new DevExpress.Export.Xl.XlCellRange(new DevExpress.Export.Xl.XlCellPosition(0, 5), new DevExpress.Export.Xl.XlCellPosition(3, 5)));
            // Create a new row. 

            e.ExportContext.AddRow();
        }


        void options_CustomizeSheetHeader(DevExpress.Export.ContextEventArgs e)
        {
            // Create a new row. 

            List<actividades> Lista = (List<actividades>)Session["MonitorGraltracking"];

            if (Lista != null)
            {
                // Create a new row.
                CellObject row = new CellObject();
                // Specify row values.
                row.Value = "Estatus de Actividades";
                // Specify row formatting.
                XlFormattingObject rowFormatting = new XlFormattingObject();
                rowFormatting.Font = new XlCellFont { Bold = true, Size = 14 };
                rowFormatting.Alignment = new DevExpress.Export.Xl.XlCellAlignment { HorizontalAlignment = DevExpress.Export.Xl.XlHorizontalAlignment.Center, VerticalAlignment = DevExpress.Export.Xl.XlVerticalAlignment.Top };
                row.Formatting = rowFormatting;
                // Add the created row to the output document.
                e.ExportContext.AddRow(new[] { row });
                // Add an empty row to the output document.
                e.ExportContext.AddRow();
                // Merge cells of two new rows. 
                e.ExportContext.MergeCells(new DevExpress.Export.Xl.XlCellRange(new DevExpress.Export.Xl.XlCellPosition(0, 0), new DevExpress.Export.Xl.XlCellPosition(6, 0)));


                var cell = CreateXlFormattingObject(true, 14);
                var CellLocation = CreateCell("En Tiempo", cell);
                var CellLocation2 = CreateCell("En Ejecucion", cell);
                var CellLocation3 = CreateCell("Terminadas", cell);
                var CellLocation4 = CreateCell("Total", cell);
                var CellLocation5 = CreateCell("Bajas", cell);
                var CellLocation7 = CreateCell("Reprogramadas", cell);
                var CellLocation6 = CreateCell("Vencidas", cell);
                CellLocation.Formatting.BackColor = System.Drawing.Color.CornflowerBlue;
                CellLocation2.Formatting.BackColor = System.Drawing.Color.CornflowerBlue;
                CellLocation3.Formatting.BackColor = System.Drawing.Color.CornflowerBlue;
                CellLocation4.Formatting.BackColor = System.Drawing.Color.CornflowerBlue;
                CellLocation5.Formatting.BackColor = System.Drawing.Color.CornflowerBlue;
                CellLocation6.Formatting.BackColor = System.Drawing.Color.CornflowerBlue;
                CellLocation7.Formatting.BackColor = System.Drawing.Color.CornflowerBlue;
                e.ExportContext.AddRow(new[] { CellLocation, CellLocation2, CellLocation3, CellLocation5, CellLocation7, CellLocation6, CellLocation4 });

                double total = 0;
                double tiempo = 0;
                double terminadas = 0;
                double ejecucion = 0;
                double bajas = 0;
                double vencidas = 0;
                double reprogramadas = 0;

                CellLocation.Formatting.BackColor = System.Drawing.Color.White;
                CellLocation2.Formatting.BackColor = System.Drawing.Color.White;
                CellLocation3.Formatting.BackColor = System.Drawing.Color.White;
                CellLocation4.Formatting.BackColor = System.Drawing.Color.White;
                CellLocation5.Formatting.BackColor = System.Drawing.Color.White;
                CellLocation6.Formatting.BackColor = System.Drawing.Color.White;
                CellLocation7.Formatting.BackColor = System.Drawing.Color.White;
                foreach (actividades agenda in Lista)
                {

                    total = total + Convert.ToDouble(agenda.total);
                    tiempo = tiempo + Convert.ToDouble(agenda.tiempo);
                    terminadas = terminadas + Convert.ToDouble(agenda.terminadas);
                    ejecucion = ejecucion + Convert.ToDouble(agenda.ejecucion);
                    bajas = bajas + Convert.ToDouble(agenda.bajas);
                    vencidas = vencidas + Convert.ToDouble(agenda.vencidas);
                    reprogramadas = reprogramadas + Convert.ToDouble(agenda.Reprogramadas);

                    CellLocation = CreateCell(agenda.tiempo, cell);
                    CellLocation2 = CreateCell(agenda.ejecucion, cell);
                    CellLocation3 = CreateCell(agenda.terminadas, cell);
                    CellLocation4 = CreateCell(agenda.total, cell);
                    CellLocation5 = CreateCell(agenda.bajas, cell);
                    CellLocation6 = CreateCell(agenda.vencidas, cell);
                    CellLocation7 = CreateCell(agenda.Reprogramadas, cell);
                    e.ExportContext.AddRow(new[] { CellLocation, CellLocation2, CellLocation3, CellLocation5, CellLocation7, CellLocation6, CellLocation4 });
                }
                double Porctotal = total / total;
                double portiempo = tiempo == 0 ? 0 : tiempo / total;
                double porterminadas = terminadas == 0 ? 0 : terminadas / total;
                double porcejecucion = ejecucion == 0 ? 0 : ejecucion / total;
                double porcbaja = bajas == 0 ? 0 : bajas / total;
                double porcvencidad = vencidas == 0 ? 0 : vencidas / total;
                double porcreprogramas = reprogramadas == 0 ? 0 : reprogramadas / total;

                CellLocation = CreateCell(portiempo.ToString("P0"), cell);
                CellLocation2 = CreateCell(porcejecucion.ToString("P0"), cell);
                CellLocation3 = CreateCell(porterminadas.ToString("P0"), cell);
                CellLocation4 = CreateCell(Porctotal.ToString("P0"), cell);
                CellLocation5 = CreateCell(porcbaja.ToString("P0"), cell);
                CellLocation6 = CreateCell(porcvencidad.ToString("P0"), cell);
                CellLocation7 = CreateCell(porcreprogramas.ToString("P0"), cell);
                e.ExportContext.AddRow(new[] { CellLocation, CellLocation2, CellLocation3, CellLocation5, CellLocation7, CellLocation6, CellLocation4 });

                e.ExportContext.AddRow();
                e.ExportContext.AddRow();
            }
        }

        protected void btnPerdidaClienteexcel_ServerClick(object sender, EventArgs e)
        {
            GrdFacturacionCliente.ExportXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
        }

        protected void RBFecha_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RBFecha.SelectedValue.ToString() == "1")
            {
                txtFechaInicialTracking.Enabled = true;
                txtFechaFinalTracking.Enabled = true;
                txtFecha.Enabled = false;

                txtFechaInicialTracking.ReadOnly = false;
                txtFechaFinalTracking.ReadOnly = false;
                txtFecha.ReadOnly = true;
            }
            else
            {
                txtFechaInicialTracking.Enabled = false;
                txtFechaFinalTracking.Enabled = false;
                txtFecha.Enabled = true;

                txtFechaInicialTracking.ReadOnly = true;
                txtFechaFinalTracking.ReadOnly = true;
                txtFecha.ReadOnly = false;
            }
        }
    }


    public class actividades
    {
        public string Reprogramadas { get; set; }
        public string tiempo { get; set; }
        public string ejecucion { get; set; }
        public string terminadas { get; set; }
        public string total { get; set; }
        public string bajas { get; set; }
        public string vencidas { get; set; }
    }
}