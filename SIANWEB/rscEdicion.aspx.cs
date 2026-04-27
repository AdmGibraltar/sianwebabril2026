using CapaEntidad;
using CapaNegocios;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIANWEB
{


    public partial class rscEdicion : System.Web.UI.Page
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

        #region mensaje

        private void mensajeAlerta(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "modalMensaje('" + mensaje + "')", true);
        }

        private void mensajeExito(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "modalMensajeExito('" + mensaje + "')", true);
        }

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    cargarActividad(Convert.ToInt32(Request.QueryString["id"]));
                }

                if (Request.QueryString["redirect"] != null)
                {
                    Session["redirectrsc"] = Request.QueryString["redirect"].ToString();
                    if (Session["redirectrsc"].ToString() == "1")
                    {
                        info("no se puede iniciar la actividad, ya existe otra actividad en curso, la actividad sera redirigida a la actividad activa");
                        Session["redirectrsc"] = "2";
                    }
                }
            }
        }

        public void cargarActividad(int id)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CNRSCAgenda agenda = new CNRSCAgenda();
                AgendaRsc Registro = new AgendaRsc();

                List<Agenda> list = new List<Agenda>();

                Registro.Id_Emp = Sesion.Id_Emp;
                Registro.Id_Cd = Sesion.Id_Cd;
                Registro.ID = id;

                agenda.ConsultarAgendaDetallada(Registro, ref list, Sesion.Emp_Cnx);

                if (list.Count > 0)
                {
                    lblActividad.InnerText = list.First().nombre.Split('-')[1];
                    lblCliente.InnerText = list.First().nombre.Split('-')[2];
                    lblHorario.InnerText = DateTime.Parse(list.First().FechaInicio.ToString()).ToString("HH:mm") + " - " + DateTime.Parse(list.First().fechaFinal.ToString()).ToString("HH:mm");
                    lblRol.InnerText = list.First().TipoUsuario;
                    lblUsuario.InnerText = list.First().usuario;
                    lblBracket.InnerText = list.First().tipo;
                    lblid.InnerText = id.ToString();
                }

                if (session.Id_TU > 3)
                {
                    BtnIniciar.Visible = true;
                    BtnTerminar.Visible = true;
                }
            }
            catch (Exception ex)
            {
                warning("Error en carga datos - " + ex.Message);
            }
        }



        public void IniciarActividad(int id)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CNRSCAgenda agenda = new CNRSCAgenda();
                AgendaRsc Registro = new AgendaRsc();
                List<Agenda> ListaACtividadInicioFin = new List<Agenda>();
                List<Agenda> ListaIniciar = new List<Agenda>();
                List<Agenda> ListaTerminar = new List<Agenda>();
                List<Agenda> Listavalidacion = new List<Agenda>();
                List<Agenda> list = new List<Agenda>();

                Registro.Id_Emp = Sesion.Id_Emp;
                Registro.Id_Cd = Sesion.Id_Cd;
                Registro.ID = id;
                Registro.Id_Usu = Sesion.Id_U;
                Registro.FechaInicio = DateTime.Now;

                List<Agenda> listaAgenda = new List<Agenda>();
                agenda.ConsultarAgendaDetallada(Registro, ref listaAgenda, Sesion.Emp_Cnx);
                if (listaAgenda.Count > 0)
                {
                    if (listaAgenda.First().verificador == 0)
                    {
                        if (Convert.ToDateTime(listaAgenda.First().fechaFinal).Date < DateTime.Now.Date.AddDays(-1))
                        {
                            info("No se Puede Iniciar la Actividad, Favor de Reagendar");
                            return;
                        }
                        if (Convert.ToDateTime(listaAgenda.First().fechaFinal).Date > DateTime.Now.Date)
                        {
                            info("No se Puede Iniciar la Actividad, Favor de Reagendar");
                            return;
                        }
                    }
                    else
                    {
                        info("Error para Iniciar la Actividad");
                        return;
                    }
                }
                agenda.ConsultarACtividadInicioFin(Registro, ref ListaACtividadInicioFin, Sesion.Emp_Cnx);
                if (ListaACtividadInicioFin.Count > 0)
                {
                    if (ListaACtividadInicioFin.First().FechaInicio.Date == Convert.ToDateTime("1/1/0001").Date)
                    {
                        agenda.ConsultarACtividadActivas(Registro, ref Listavalidacion, Sesion.Emp_Cnx);

                        if (Listavalidacion.Count > 0)
                        {
                            if (Listavalidacion.First().FechaInicio.Date != Convert.ToDateTime("1/1/0001").Date)
                            {
                                int idActividad = Listavalidacion.First().ID;
                                cargarActividad(idActividad);
                                info("Existe una Actividad en Curso con fecha: " + Listavalidacion.First().FechaInicio.ToString("dd/MM/yyyy") + ". El Sistema se Redirigio hacia la Actividad en Curso");
                                return;
                            }
                        }
                        agenda.AgendaInicioActividades(Registro, ref ListaIniciar, Sesion.Emp_Cnx);
                        info("Actividad Iniciada");
                        return;
                    }
                    else if (ListaACtividadInicioFin.First().fechaFinal.Date == Convert.ToDateTime("1/1/0001").Date)
                    {
                        info("Actividad en Curso");
                        return;
                    }
                    else
                    {
                        info("Actividad Finalizada");
                    }
                }
                else
                {
                    info("Error al Consultar la Actividad");
                }
            }
            catch (Exception ex)
            {
                warning("Error al Iniciar la Actividad - " + ex.Message);
            }
        }

        public void TerminarActividad(int id)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CNRSCAgenda agenda = new CNRSCAgenda();
                AgendaRsc Registro = new AgendaRsc();
                List<Agenda> ListaACtividadInicioFin = new List<Agenda>();
                List<Agenda> ListaIniciar = new List<Agenda>();
                List<Agenda> ListaTerminar = new List<Agenda>();
                List<Agenda> Listavalidacion = new List<Agenda>();
                List<Agenda> list = new List<Agenda>();

                Registro.Id_Emp = Sesion.Id_Emp;
                Registro.Id_Cd = Sesion.Id_Cd;
                Registro.ID = id;
                Registro.Id_Usu = Sesion.Id_U;
                Registro.fechaFinal = DateTime.Now;

                agenda.ConsultarACtividadInicioFin(Registro, ref ListaACtividadInicioFin, Sesion.Emp_Cnx);
                if (ListaACtividadInicioFin.Count > 0)
                {
                    if (ListaACtividadInicioFin.First().FechaInicio.Date == Convert.ToDateTime("1/1/0001").Date)
                    {
                        info("Actividad no ha sido iniciada");
                        return;
                    }
                    else if (ListaACtividadInicioFin.First().fechaFinal.Date == Convert.ToDateTime("1/1/0001").Date)
                    {
                        agenda.AgendaFinalizarActividades(Registro, ref ListaIniciar, Sesion.Emp_Cnx);

                        info("Actividad finalizada");
                        return;
                    }
                    else
                    {
                        info("Esta actividad ya fue finalizada");
                    }
                }
                else
                {
                    info("Error al realizar la consulta de actividad");
                }
            }
            catch (Exception ex)
            {
                warning("Error al iniciar la actividad - " + ex.Message);
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblid.InnerText != null)
                {
                    string id = lblid.InnerText;
                    string _PermisoGuardar = "true";
                    string _PermisoModificar = "true";

                    Sesion Sesion = new Sesion();
                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    CNRSCAgenda agenda = new CNRSCAgenda();
                    AgendaRsc Registro = new AgendaRsc();
                    List<Agenda> list = new List<Agenda>();
                    List<Agenda> Listabaja = new List<Agenda>();
                    Registro.Id_Emp = Sesion.Id_Emp;
                    Registro.Id_Cd = Sesion.Id_Cd;
                    Registro.ID = Convert.ToInt32(id);
                    Registro.Comentarios = "Baja de actividad - sianweb";

                    List<Agenda> listaAgenda = new List<Agenda>();
                    agenda.ConsultarAgendaDetallada(Registro, ref listaAgenda, Sesion.Emp_Cnx);
                    if (listaAgenda.Count > 0)
                    {
                        if (listaAgenda.First().verificador == 0)
                        {
                            if (listaAgenda.First().inicioEjecucion != null)
                            {
                                info("La actividad no puede ser actualizada, la actividad esta en curso o finalizada");
                                return;
                            }
                            if (Convert.ToDateTime(listaAgenda.First().fechaFinal).Date < DateTime.Now.Date.AddDays(-7))
                            {
                                info("La actividad ya no puede ser actualizada");
                                return;
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Closeactividad", "CloseEdicion('" + id + "','" + _PermisoGuardar + "','" + _PermisoModificar + "')", true);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                warning("Error al realizar la actualizacion");
            }
        }

        protected void btnAceptarEliminar_Click(object sender, EventArgs e)
        {
            if (lblid.InnerText != null)
            {
                string id = lblid.InnerText;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "MotivoCancelacion", "ModalMotivo('" + id + "')", true);
            }
        }

        protected void BtnTerminar_ServerClick(object sender, EventArgs e)
        {
            if (lblid.InnerText != null)
            {
                TerminarActividad(Convert.ToInt32(lblid.InnerText));
            }
        }

        protected void BtnIniciar_ServerClick(object sender, EventArgs e)
        {
            if (lblid.InnerText != null)
            {
                IniciarActividad(Convert.ToInt32(lblid.InnerText));
            }
        }
    }
}