using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocios;
using Telerik.Web.UI;
using CapaModelo_CC.CuentasCoorporativas;
using CapaEntidad;
using SIANWEB.Utilerias;
using System.Text;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Configuration; 

namespace SIANWEB
{
    public partial class Pro_CN_Vinculacion_Lista : System.Web.UI.Page
    {

        #region Variables 

        public Sesion session
        {
            get
            {
                return (Sesion)Session["session" + Session.SessionID];
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

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["vinculacionCN"] != null)
            {
                List<CN_Matriz> Lista = (List<CN_Matriz>)Session["vinculacionCN"];
                dgClienteMatriz.DataSource = Lista;
                dgClienteMatriz.DataBind();
            } 
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CN_CatCNac_Matriz cn = new CN_CatCNac_Matriz();

            var permisos = new Permisos(this.Page);
            permisos.ValidarSesion();


            if (!Page.IsPostBack)
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                cargarMatriz();

                int idMatriz = 0;
                if (cmbMatriz.Items.Count > 0)
                    idMatriz = Int32.Parse(cmbMatriz.Value.ToString());


                CN_Cn_matriz Matriz = new CN_Cn_matriz();
                List<CN_Matriz> Lista = new List<CN_Matriz>();
                CN_Matriz datos = new CN_Matriz();
                datos.id_Cd = Sesion.Id_Cd_Ver;
                datos.id_matriz = idMatriz;
                Matriz.ConsultarVinculacion(datos, ref Lista, Sesion.Emp_Cnx);

                Session["vinculacionCN"] = Lista;
                dgClienteMatriz.DataSource = Lista;
                dgClienteMatriz.DataBind();
            }
        }

        private void cargarMatriz()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_Cn_matriz Matriz = new CN_Cn_matriz();
            List<CN_Matriz> Lista = new List<CN_Matriz>();
            CN_Matriz datos = new CN_Matriz();
            datos.id_Cd = Sesion.Id_Cd_Ver;
            Matriz.ConsultarMatriz(datos, ref Lista, Sesion.Emp_Cnx);

            cmbMatriz.DataSource = Lista;
            cmbMatriz.ValueField = "id";
            cmbMatriz.TextField = "nombre";
            cmbMatriz.DataBind();

            cmbMatriz.Value = Lista.First().id.ToString();
        }



        protected void dgClienteMatriz_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var objSession = ((CapaEntidad.Sesion)(Session["session" + Session.SessionID]));

            if (e.CommandName == "Cancelar")
            {
                int idEst = Int32.Parse(e.CommandArgument.ToString());
                CN_CatCNac_Matriz cn = new CN_CatCNac_Matriz();
                cn.CancelarSolicitud(idEst);

                int idMatriz = Int32.Parse(cmbMatriz.Value.ToString());

                this.dgClienteMatriz.DataSource = cn.ConsultarEstructura(idMatriz, objSession.Id_Emp, objSession.Id_Cd);
                this.dgClienteMatriz.DataBind();
            }
            if (e.CommandName == "Reenviar")
            {

                int idSol = Int32.Parse(e.CommandArgument.ToString());
                CN_CatCNac_Matriz cn = new CN_CatCNac_Matriz();
                var solic = cn.ConsultarSolicitudes(idSol);

                EnviaEmail(solic);
               info("Se reenvió la solicitud");


            }

        }


      

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            CN_CatCNac_Matriz cn = new CN_CatCNac_Matriz();
            var sol = cn.ConsultarSolicitudes("ogc");
        }



        protected void EnviaEmail(CatCNac_Solicitudes solicitud)
        {
            try
            {

                CN_CatCNac_Solicitudes Csol = new CN_CatCNac_Solicitudes();
                var res = Csol.Consultar_Emails_Aut();


                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Cd = session.Id_Cd_Ver;
                configuracion.Id_Emp = session.Id_Emp;
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                cn_configuracion.Consulta(ref configuracion, session.Emp_Cnx);


                StringBuilder cuerpo_correo = new StringBuilder();
                cuerpo_correo.Append("<div align='center'>");
                cuerpo_correo.Append("<table><tr><td>");
                cuerpo_correo.Append("<IMG SRC=\"cid:companylogo\" ALIGN='left'></td>");
                cuerpo_correo.Append("<td></td>");
                cuerpo_correo.Append("</tr><tr><td colspan='2'><br><br><br></td>");
                cuerpo_correo.Append("</tr><tr>");
                cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='2'>");
                cuerpo_correo.Append("Se ha colocado una solicitud de vinculacion para el cliente " + solicitud.ClienteSIAN.ToString());
                /*if (acys.Acs_Sustituye != null)
                    cuerpo_correo.Append(" que sustituye a la solicitud #" + acys.Acs_Sustituye);*/

                cuerpo_correo.Append(", de la sucursal " + solicitud.Sucursal.ToString());
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");
                string[] url = Request.Url.ToString().Split(new char[] { '/' });
                cuerpo_correo.Append("</td></tr></table></div>");
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                sm.EnableSsl = true;
                MailMessage m = new MailMessage();
                m.From = new MailAddress(configuracion.Mail_Remitente);

                foreach (CatCNac_EmailsAutorizacion email in res)
                    m.To.Add(new MailAddress(email.Correo));


                m.Subject = "Solicitud de autorización de Vinculacion";
                m.IsBodyHtml = true;
                string body = cuerpo_correo.ToString();
                AlternateView vistaHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                //Esto queda dentro de un try por si llegan a cambiar la imagen el correo como quiera se mande
                try
                {
                    LinkedResource logo = new LinkedResource(MapPath(@"Imagenes/logo.jpg"), MediaTypeNames.Image.Jpeg);
                    logo.ContentId = "companylogo";
                    vistaHtml.LinkedResources.Add(logo);
                }
                catch (Exception)
                {
                }

                m.AlternateViews.Add(vistaHtml);
                try
                {
                    sm.Send(m);
                }
                catch (Exception)
                {
                   info("Error al enviar el correo. Favor de revisar la configuración del sistema");
                    return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return;
        }

        protected void cmbMatriz_SelectedIndexChanged1(object sender, EventArgs e)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

          
            int idMatriz = Int32.Parse(cmbMatriz.Value.ToString()); 

            CN_Cn_matriz Matriz = new CN_Cn_matriz();
            List<CN_Matriz> Lista = new List<CN_Matriz>();
            CN_Matriz datos = new CN_Matriz();
            datos.id_matriz = idMatriz;
            datos.id_Cd = Sesion.Id_Cd_Ver;
            Matriz.ConsultarVinculacion(datos, ref Lista, Sesion.Emp_Cnx);

            Session["vinculacionCN"] = Lista;
            dgClienteMatriz.DataSource = Lista;
            dgClienteMatriz.DataBind(); ;
        }
    }



}   