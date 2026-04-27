using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaDatos;
using Telerik.Web.UI;
using System.Data;
using CapaNegocios;
using System.Text;
using SIANWEB.Core.UI;
using CapaModelo;
using System.Configuration;
using DevExpress.XtraScheduler.iCalendar;
using System.IO;
using DevExpress.Web.Bootstrap;
using DevExpress.XtraScheduler;

namespace SIANWEB.GestionRSC
{
    public partial class RSCAgenda : BaseServerPage
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
            if (Session["Agendarsc"] != null)
            {
                List<AgendaRsc> Lista = (List<AgendaRsc>)Session["Agendarsc"];
                BSAgenda.AppointmentDataSource = Lista;
                BSAgenda.DataBind();
            }
            if (Session["AgendaActividad"] != null)
            {
                List<AgendaRsc> Actividades = (List<AgendaRsc>)Session["AgendaActividad"];
                GRDACtividadesdeDia.DataSource = Actividades;
                GRDACtividadesdeDia.DataBind();
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
                    if (ValidarSesion())
                    {
                        if (!IsPostBack)
                        {
                            Session["Agendarsc"] = null;
                            Session["AgendaActividad"] = null;
                            BSAgenda.AppointmentDataSource = null;
                            BSAgenda.DataBind();

                            ValidarPermisos();
                            Llenardatos();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                warning(ex.Message + "- " + new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        #endregion

        #region Funciones

        private void Llenardatos()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            int Id_CD = session.Id_Cd;
            List<AgendaRsc> Lista = new List<AgendaRsc>();

            cargarTipoUsuario();
            CargarUsuario(Id_CD);
            cargarClientes();

            lblActividad.Text = "Actividad del Día: " + DateTime.Now.ToString("dd MMMM, yyyy");
            lblCorte.Text = "Fecha de Corte: " + DateTime.Now.ToString("HH:mm");
            Fecha.Value = session.CalendarioIni;
            FechaFinal.Value = session.CalendarioFin;
            cargarDatos();


        }

        private void cargarDatos()
        {
            int Id_CD = session.Id_Cd;
            List<AgendaRsc> Lista = new List<AgendaRsc>();

            AgendaRsc agenda = new AgendaRsc();
            agenda.Id_Emp = session.Id_Emp;
            agenda.Id_Cd = session.Id_Cd;
            agenda.Id_Usu = Convert.ToInt32(DllUSuario.Value.ToString());
            agenda.id_tu = Convert.ToInt32(DllRol.Value.ToString());
            agenda.id_cte = Convert.ToInt32(DllCLiente.Value.ToString());
            agenda.id_ActividadGral = -1;
            agenda.id_Actividad = -1;
            agenda.FechaInicio = Convert.ToDateTime(Fecha.Text);
            agenda.fechaFinal = Convert.ToDateTime(FechaFinal.Text);

            CNRSCAgenda CN = new CNRSCAgenda();
            CN.ConsultaPedidoRastreo(agenda, session.Emp_Cnx, ref Lista);
            Session["Agendarsc"] = Lista;
            BSAgenda.AppointmentDataSource = Lista;
            BSAgenda.DataBind();


            List<AgendaRsc> Actividades = new List<AgendaRsc>();
            AgendaRsc act = new AgendaRsc();

            var tlistvencidas = (from tlist in Lista
                                 where tlist.fechaFinal <= DateTime.Now
                                 && tlist.fechaFinal.Date == DateTime.Now.Date
                                 && tlist.inicioEjecucion == ""
                                 select new
                                 {
                                     actividad = tlist.Actividad,
                                     nombre = tlist.usuario,
                                     fechainicial = tlist.FechaInicio,
                                     fechafinal = tlist.fechaFinal
                                 }).ToList();

            act.Actividad = "Vencidas";
            act.Cantidad = tlistvencidas.Count;
            act.colorACtividad = "red.png";
            Actividades.Add(act);

            var tlistEjecucion = (from tlist in Lista
                                  where tlist.inicioEjecucion != ""
                                  && tlist.finalEjecucion == ""
                                     && tlist.fechaFinal.Date == DateTime.Now.Date
                                  select new
                                  {
                                      actividad = tlist.Actividad,
                                      nombre = tlist.usuario,
                                      fechainicial = tlist.FechaInicio,
                                      fechafinal = tlist.fechaFinal
                                  }).ToList();
            act = new AgendaRsc();
            act.Actividad = "En Ejecución";
            act.Cantidad = tlistEjecucion.Count;
            act.colorACtividad = "yellow.png";
            Actividades.Add(act);

            var tlistTiempo = (from tlist in Lista
                               where tlist.fechaFinal >= DateTime.Now
                                  && tlist.fechaFinal.Date == DateTime.Now.Date
                                  && tlist.inicioEjecucion == ""
                               select new
                               {
                                   actividad = tlist.Actividad,
                                   nombre = tlist.usuario,
                                   fechainicial = tlist.FechaInicio,
                                   fechafinal = tlist.fechaFinal
                               }).ToList();
            act = new AgendaRsc();
            act.Actividad = "En Tiempo";
            act.Cantidad = tlistTiempo.Count;
            act.colorACtividad = "green.png";
            Actividades.Add(act);


            var tlistCompletadas = (from tlist in Lista
                                    where tlist.inicioEjecucion != ""
                                    && tlist.finalEjecucion != ""
                                       && tlist.fechaFinal.Date == DateTime.Now.Date
                                    select new
                                    {
                                        actividad = tlist.Actividad,
                                        nombre = tlist.usuario,
                                        fechainicial = tlist.FechaInicio,
                                        fechafinal = tlist.fechaFinal
                                    }).ToList();
            act = new AgendaRsc();
            act.Actividad = "Terminadas";
            act.Cantidad = tlistCompletadas.Count;
            act.colorACtividad = "blue.png";
            Actividades.Add(act);



            Session["AgendaActividad"] = Actividades;
            GRDACtividadesdeDia.DataSource = Actividades;
            GRDACtividadesdeDia.DataBind();

            lblActividad.Text = "Actividad del Día: " + DateTime.Now.ToString("dd MMMM, yyyy");
            lblCorte.Text = "Fecha de Corte: " + DateTime.Now.ToString("HH:mm");
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

            if (Sesion.Id_TU != 15 && Sesion.Id_TU != 16 && Sesion.Id_TU != 31)
            {
                DllRol.Value = "-1";
                DllRol.ReadOnly = false;
                DllRol.Enabled = true;
            }
            else
            {
                if (Sesion.Id_TU == 31)
                {
                    DllRol.Value = "15";
                    DllRol.ReadOnly = true;
                    DllRol.Enabled = false;
                }
                else
                {
                    DllRol.Value = Sesion.Id_TU.ToString();
                    DllRol.ReadOnly = true;
                    DllRol.Enabled = false;
                }
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

            if (Sesion.Id_TU != 15 && Sesion.Id_TU != 16 && Sesion.Id_TU != 31)
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

        private void cargarClientes()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            AgendaRsc Agenda = new AgendaRsc();
            List<Cliente> lista = new List<Cliente>();
            CN_CapPedidoVtaInst vtaInst = new CN_CapPedidoVtaInst();
            Agenda.Id_Emp = sesion.Id_Emp;
            Agenda.Id_Cd = sesion.Id_Cd_Ver;
            Agenda.Id_Usu = Convert.ToInt32(DllUSuario.Value.ToString());
            CN_CapPedido cn_capPedido = new CN_CapPedido();
            vtaInst.ConsultarAgendaCliente(Agenda, ref lista, sesion.Emp_Cnx);

            var query = (from tlist in lista
                         group tlist by tlist.idCte into g
                         select new
                         {
                             Id_Cte = g.Key,
                             nombre = g.Select(x => x.nombre).FirstOrDefault()
                         }).ToList();

            DllCLiente.DataSource = query;
            DllCLiente.ValueField = "Id_Cte";
            DllCLiente.TextField = "nombre";
            DllCLiente.DataBind();

            DllCLiente.Value = "0";
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

        #endregion

        #region Proceso 

        protected void BtnCrearCita_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunctionNew", "NuevaCita()", true);
        }

        protected void btnCargarListado_ServerClick(object sender, EventArgs e)
        {
            cargarDatos();
        }

        protected void BSAgenda_AppointmentViewInfoCustomizing(object sender, DevExpress.Web.ASPxScheduler.AppointmentViewInfoCustomizingEventArgs e)
        {
            //e.ViewInfo.AppointmentStyle.CssClass = "dx-custom-style";
        }

        protected void BSAgenda_PopupMenuShowing(object sender, DevExpress.Web.Bootstrap.BootstrapSchedulerPopupMenuShowingEventArgs e)
        {
            e.Menu.Items.Clear();
        }

        protected void ButtonExport_Click(object sender, EventArgs e)
        {
            iCalendarExporter exporter = BSAgenda.SelectedAppointments.Count > 0 ?
       new iCalendarExporter(BSAgenda.Storage, BSAgenda.SelectedAppointments) :
       new iCalendarExporter(BSAgenda.Storage);
            PostCalendarFile(exporter);
        }

        protected void BSAgenda_PopupMenuShowing1(object sender, DevExpress.Web.Bootstrap.BootstrapSchedulerPopupMenuShowingEventArgs e)
        {
            BootstrapSchedulerPopupMenu menu = e.Menu;
            if (menu.MenuId.Equals(SchedulerMenuItemId.AppointmentMenu))
            {
                DevExpress.Web.MenuItem item = new BootstrapMenuItem("Export Appointment", "ExportAppointment");
                menu.Items.Insert(1, item);
                menu.ClientSideEvents.ItemClick = "function(s, e) { OnMenuClick(s,e); }";
            }
        }

        public void PostCalendarFile(iCalendarExporter exporter)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                exporter.Export(memoryStream);
                Stream outputStream = Response.OutputStream;
                memoryStream.WriteTo(outputStream);
                outputStream.Flush();
            }
            Response.ContentType = "text/calendar";
            Response.AddHeader("Content-Disposition", "attachment; filename=appointments.ics");
            Response.End();
        }

        protected void DllUSuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<AgendaRsc> Lista = new List<AgendaRsc>();
            AgendaRsc agenda = new AgendaRsc();
            agenda.Id_Emp = session.Id_Emp;
            agenda.Id_Cd = session.Id_Cd;

            agenda.Id_Usu = 1;
            if (Convert.ToInt32(DllUSuario.Value.ToString()) != -1)
            {
                agenda.Id_Usu = Convert.ToInt32(DllUSuario.Value.ToString());
            }
            CNRSCAgenda CN = new CNRSCAgenda();
            CN.ConsultaPedidoRastreo(agenda, session.Emp_Cnx, ref Lista);

            Session["Agendarsc"] = Lista;
            BSAgenda.AppointmentDataSource = Lista;
            BSAgenda.DataBind();
        }

        protected void DllRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Id_CD = session.Id_Cd;
            CargarUsuario(Id_CD);
        }

        protected void DllUSuario_SelectedIndexChanged1(object sender, EventArgs e)
        {
            cargarClientes();
        }
        #endregion

        protected void btnAgendar_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunctionNew", "Agendar()", true);
        }
    }
}