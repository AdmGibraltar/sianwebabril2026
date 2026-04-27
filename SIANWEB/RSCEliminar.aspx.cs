using CapaEntidad;
using CapaNegocios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIANWEB
{

    public partial class RSCEliminar : System.Web.UI.Page
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

        private void ShowMessageExito(string Message, string type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessageExito('" + Message + "','" + type + "');", true);
        }

        private void ShowMessage(string Message, string type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }
        private void sucess(string mensaje)
        {
            ShowMessageExito(mensaje, "Success");
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

                }
            }
        }

        protected void btnMotivo_ServerClick(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                EliminarActividad(Convert.ToInt32(Request.QueryString["id"]));
            }
        }


        public void EliminarActividad(int id)
        {
            try
            {
                if (txtMotivo.Text == "")
                {
                    info("Favor de agregar un motivo de cancelación");
                    return;
                }
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CNRSCAgenda agenda = new CNRSCAgenda();
                AgendaRsc Registro = new AgendaRsc();
                List<Agenda> list = new List<Agenda>();
                List<Agenda> Listabaja = new List<Agenda>();
                Registro.Id_Emp = Sesion.Id_Emp;
                Registro.Id_Cd = Sesion.Id_Cd;
                Registro.ID = id;
                Registro.Comentarios = txtMotivo.Text;

                List<Agenda> listaAgenda = new List<Agenda>();
                agenda.ConsultarAgendaDetallada(Registro, ref listaAgenda, Sesion.Emp_Cnx);
                if (listaAgenda.Count > 0)
                {
                    if (listaAgenda.First().verificador == 0)
                    {
                        if (listaAgenda.First().inicioEjecucion != null)
                        {
                            info("La actividad no puede ser Eliminada, la actividad esta en curso o finalizada");
                            return;
                        }
                        if (Convert.ToDateTime(listaAgenda.First().fechaFinal).Date < DateTime.Now.Date.AddDays(-7))
                        {
                            info("La actividad ya no puede ser Eliminada");
                            return;
                        }
                        else
                        {
                            agenda.BajaAgendaDetallada(Registro, ref Listabaja, Sesion.Emp_Cnx);
                            if (Listabaja.First().ID == 0)
                            {
                                sucess("Se elimino la actividad correctamente");
                            }
                            else
                            {
                                info("Error, no se elimino la actividad correctamente");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                warning("Error en dar de baja la actividad - " + ex.Message);
            }
        }

    }
}