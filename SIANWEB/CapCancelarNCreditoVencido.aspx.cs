using CapaEntidad;
using CapaNegocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SIANWEB
{
    public partial class CapCancelarNCreditoVencido : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                    CerrarVentana();
                }
                else
                {
                    if (!Page.IsPostBack)
                    { //obtener valores desde la URL
                        ViewState["Documento_Id"] = Convert.ToInt32(Page.Request.QueryString["Id_Ncr"]);
                        ViewState["Id_Cd"] = Convert.ToInt32(Page.Request.QueryString["Id_Cd"]);
                        ViewState["Id_Emp"] = Convert.ToInt32(Page.Request.QueryString["Id_Emp"]);
                        ViewState["Serie"] = Page.Request.QueryString["Id_NcrSerie"].ToString();
                        lblDocumento.Text = ViewState["Serie"].ToString();
                        if (!string.IsNullOrEmpty(Page.Request.QueryString["Num_Cliente"]))
                        {
                            ViewState["Num_Cliente"] = Convert.ToInt32(Page.Request.QueryString["Num_Cliente"]);

                        }
                        int Id_Nca = 0;
                        if (!string.IsNullOrEmpty(Page.Request.QueryString["Id_Nca"]))
                        {
                            Id_Nca = Convert.ToInt32(Page.Request.QueryString["Id_Nca"]);
                        }
                        ViewState["Id_Nca"] = Id_Nca;

                        if (!string.IsNullOrEmpty(Page.Request.QueryString["StatusDev"]))
                        {
                            ViewState["StatusDev"] = Page.Request.QueryString["StatusDev"].ToString();

                        }
                        if (!string.IsNullOrEmpty(Page.Request.QueryString["FechaDev"]))
                        {
                            string fechaDev = Page.Request.QueryString["FechaDev"];
                            ViewState["FechaDev"] = fechaDev;

                        }

                        if (!string.IsNullOrEmpty(Page.Request.QueryString["TipoDev"]))
                        {
                            ViewState["TipoDev"] = Convert.ToInt32(Page.Request.QueryString["TipoDev"]);

                        }

                        if (!string.IsNullOrEmpty(Page.Request.QueryString["Factura_dev"]))
                        {
                            ViewState["Factura_dev"] = Convert.ToInt32(Page.Request.QueryString["Factura_dev"]);

                        }

                        if (!string.IsNullOrEmpty(Page.Request.QueryString["Nca_Subtotal"]))
                        {
                            ViewState["Nca_Subtotal"] = Convert.ToDouble(Page.Request.QueryString["Nca_Subtotal"]);

                        }

                        if (!string.IsNullOrEmpty(Page.Request.QueryString["Nca_Iva"]))
                        {
                            ViewState["Nca_Iva"] = Convert.ToDouble(Page.Request.QueryString["Nca_Iva"]);

                        }

                        if (!string.IsNullOrEmpty(Page.Request.QueryString["Nca_Total"]))
                        {
                            ViewState["Nca_Total"] = Convert.ToDouble(Page.Request.QueryString["Nca_Total"]);

                        }
                        if (!string.IsNullOrEmpty(Page.Request.QueryString["serieDev"]))
                        {
                            string serieDev = Page.Request.QueryString["serieDev"];
                            ViewState["serieDev"] = serieDev;
                        }

                        string Ncr_Estatus = "";
                        if (!string.IsNullOrEmpty(Page.Request.QueryString["Ncr_Estatus"]))
                        {
                            Ncr_Estatus = Page.Request.QueryString["Ncr_Estatus"];

                        }
                        ViewState["Ncr_Estatus"] = Ncr_Estatus;

                        this.Inicializar();

                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void Inicializar()
        {
            cmbMotivo.Items.Clear();
            cmbMotivo.Items.Add(new Telerik.Web.UI.RadComboBoxItem("--Seleccione una opción--", "-1"));
            CargarMotivos();
            // Agregar manualmente 3 items al RadComboBox
            cmbMotivo.Items.Add(new Telerik.Web.UI.RadComboBoxItem("Otro", "0"));
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            string accionError = string.Empty;
            string mensajeError = string.Empty;
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;

                switch (btn.CommandName)
                {
                    case "save":
                        this.Guardar();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void Guardar()
        {
            try
            {

                if (Int32.Parse(ViewState["Documento_Id"].ToString()) <= 0)
                {
                    ErrorManager("No hay documento seleccionado");
                    return;
                }
                if (Int32.Parse(cmbMotivo.SelectedValue) < 0)
                {
                    ErrorManager("Seleccione el motivo");
                    return;
                }
                if (cmbMotivo.Text.ToString() == "Otro")
                {
                    if (txtOtroMotivo.Text.ToString().Trim() == "")
                    {
                        ErrorManager("Ingrese el motivo");
                        return;
                    }
                }

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                //ErrorManager("Ocurrio un error al guardar la información");
                eBitacoraCancelacionDocumento oeBitacoraCancelacionDocumento = new eBitacoraCancelacionDocumento();
                oeBitacoraCancelacionDocumento.TipoDocumento = "NotaCredito";
                oeBitacoraCancelacionDocumento.Id_Cd = Int32.Parse(ViewState["Id_Cd"].ToString());
                oeBitacoraCancelacionDocumento.Id_Emp = Int32.Parse(ViewState["Id_Emp"].ToString());
                oeBitacoraCancelacionDocumento.Documento_Id = Int32.Parse(ViewState["Documento_Id"].ToString());
                oeBitacoraCancelacionDocumento.Usuario_Id = sesion.Id_U;
                oeBitacoraCancelacionDocumento.SerieDocumento = ViewState["Serie"].ToString();
                oeBitacoraCancelacionDocumento.Motivo_Id = Int32.Parse(cmbMotivo.SelectedValue);
                oeBitacoraCancelacionDocumento.Motivo = cmbMotivo.Text.ToString() == "Otro" ? txtOtroMotivo.Text.ToString().Trim() : cmbMotivo.Text.ToString();
                if (!string.IsNullOrEmpty(ViewState["Ncr_Estatus"].ToString()))
                {
                    oeBitacoraCancelacionDocumento.Ncr_Estatus = ViewState["Ncr_Estatus"].ToString();
                }

                oeBitacoraCancelacionDocumento.Sucursal = sesion.Cd_Nombre;
                oeBitacoraCancelacionDocumento.CorreoSolicitud = sesion.U_Correo;

                if (!string.IsNullOrEmpty(ViewState["Id_Nca"].ToString()))
                {
                    int Id_Nca = Int32.Parse(ViewState["Id_Nca"].ToString());
                    if (Id_Nca > 0)
                    {
                        oeBitacoraCancelacionDocumento.Id_Nca = Id_Nca;
                        oeBitacoraCancelacionDocumento.Num_Cliente = Convert.ToInt32(ViewState["Num_Cliente"].ToString());
                        oeBitacoraCancelacionDocumento.StatusDev = ViewState["StatusDev"].ToString();
                        oeBitacoraCancelacionDocumento.FechaDev = Convert.ToDateTime(ViewState["FechaDev"].ToString());
                        oeBitacoraCancelacionDocumento.TipoDev = Convert.ToInt32(ViewState["TipoDev"].ToString());
                        oeBitacoraCancelacionDocumento.Factura_dev = Convert.ToInt32(ViewState["Factura_dev"].ToString());
                        oeBitacoraCancelacionDocumento.Nca_Subtotal = float.Parse(ViewState["Nca_Subtotal"].ToString());
                        oeBitacoraCancelacionDocumento.Nca_Iva = float.Parse(ViewState["Nca_Iva"].ToString());
                        oeBitacoraCancelacionDocumento.Nca_Total = float.Parse(ViewState["Nca_Total"].ToString());
                        oeBitacoraCancelacionDocumento.SerieDev = ViewState["serieDev"].ToString();
                    }
                }

                int verificador = 0;
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionSIANCentral"];
                new CN_BitacoraCancelacionDocumento().InsertarBitacora(ref oeBitacoraCancelacionDocumento, Conexion, ref verificador);
                if (verificador == -1)
                {

                    RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow('", "El documento esta pendiente por cancelar.", "')")); //cerrar ventana radWindow de factura
                }
                else
                {
                    EnviaEmail(Int32.Parse(ViewState["Documento_Id"].ToString()), ViewState["Serie"].ToString());
                }

            }
            catch (Exception ex)
            {
                ErrorManager("Ocurrio un error interno, al guardar la información");
                throw ex;
            }
        }

        private void CargarMotivos()
        {
            try
            {
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionSIANCentral"];
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Id_Cd_Ver, 3, Conexion, "spMotivos_Combo", ref cmbMotivo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        private void EnviaEmail(int idNotaCredito, string serie)
        {
            try
            {

                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                var empresa = session.Emp_Nombre;
                int verificador = -1;


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
                cuerpo_correo.Append("Se levantó la solicitud de cancelación de una nota de crédito, se requiere su autorización para su cancelación.");
                cuerpo_correo.Append("</td>");
                cuerpo_correo.Append("</tr>");

                cuerpo_correo.Append("<tr><td>Sucursal:" + session.Cd_Nombre + "</td></tr>");
                cuerpo_correo.Append("<tr><td>Empresa:" + empresa + "</td></tr>");
                cuerpo_correo.Append("<tr><td>Nota de Crédito:" + idNotaCredito + "</td></tr>");
                cuerpo_correo.Append("<tr><td> Nota de Crédito Serie:" + serie);
                cuerpo_correo.Append("<tr><td>Usuario Solicitud:" + session.U_Nombre + "</td></tr>");
                cuerpo_correo.Append("</td></tr></table></div>");
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                sm.EnableSsl = true;
                MailMessage m = new MailMessage();
                m.From = new MailAddress(configuracion.Mail_Remitente);


                m.To.Add(new MailAddress("igarrido@axsistec.com"));
                m.To.Add(new MailAddress("servicios.informatica@gibraltar.com.mx"));
                m.To.Add(new MailAddress("rafael.garcia@gibraltar.com.mx"));
                m.To.Add(new MailAddress("alejandro.lozano@key.com.mx"));
                //m.To.Add(new MailAddress("jonathan.garcia@key.com.mx"));
                m.To.Add(new MailAddress("raul.estrada@key.com.mx"));

                m.Subject = "CANCELACIÓN NOTA DE CRÉDITO MESES VENCIDOS";
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
                    RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow('", "Se generó solicitud de cancelación. La cancelación esta pendiente por ser autorizada", "')")); //cerrar ventana radWindow de factura

                }
                catch (Exception ex)
                {

                    ErrorManager("Error al enviar el correo. Favor de revisar la configuración del sistema\"");

                    return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            string mensajeError = string.Empty;
            try
            {
                ErrorManager();
                string cmd = e.Argument.ToString();
                switch (cmd)
                {
                    case "RebindGrid":

                        break;

                    case "GuardarMovimiento":

                        break;

                    case "cliente":
                        break;
                    case "panel":

                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private void CerrarVentana()
        {
            try
            {
                string funcion;
                funcion = "CloseAndRebind()";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
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

        protected void cmbMotivo_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cmbMotivo.Text.ToString() == "Otro")
            {
                trOtroMotivo.Style["display"] = "contents";
            }
            else
            {
                trOtroMotivo.Style["display"] = "none";

            }
        }
    }
}