using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocios;
using Telerik.Web.UI;
using CapaModelo_CC.CuentasCoorporativas;
using SIANWEB.CuentasCorporativas;
using SIANWEB.Utilerias;
using CapaEntidad;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;

namespace SIANWEB
{
    public partial class Pro_CN_Vinculacion : System.Web.UI.Page
    {
        private Sesion sesion { get { return (Sesion)Session["Sesion" + Session.SessionID]; } set { Session["Sesion" + Session.SessionID] = value; } }

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

        protected void Page_Load(object sender, EventArgs e)
        {

            int idmatriz = Int32.Parse(Request.QueryString["IdMatriz"]);
            int id = Int32.Parse(Request.QueryString["Id"]);
            string nombre = Request.QueryString["Nombre"];

            Boolean EsDesvinc = false;
            if (Request.QueryString["DesVinc"] != null)
            {
                EsDesvinc = true;
            }

            var permisos = new Permisos(this.Page);
            permisos.ValidarSesion();

            if (!Page.IsPostBack)
            {
                cargardatos(idmatriz, id, nombre, EsDesvinc);
            }
        }

        public void cargardatos(int idmatriz, int id, string nombre, Boolean EsDesvinc)
        {
            txtNombreEstructura.Text = nombre;
            txtSucursalNombre.Text = sesion.Cd_Nombre;
            txtUsuario.Text = sesion.Cu_User;
            txtFechas.Text = DateTime.Now.ToShortDateString();

            CN_Cn_matriz Matriz = new CN_Cn_matriz();
            List<CN_Matriz> Lista = new List<CN_Matriz>();
            CN_Matriz datos = new CN_Matriz();
            datos.id_Cd = sesion.Id_Cd_Ver;
            datos.id_matriz = idmatriz;
            datos.id_Estructura = id;
            Matriz.ConsultarVinculacion(datos, ref Lista, sesion.Emp_Cnx);

            CN_CatCNac_Matriz cn = new CN_CatCNac_Matriz();

            if (Lista.Count > 0)
            {
                txtACYS.Text = Lista.First().AcysNombre;

                List<CN_Matriz> ListaUsuario = new List<CN_Matriz>();
                Matriz.ConsultarUsuario(datos, ref ListaUsuario, sesion.Emp_Cnx);
                cmbAsesorId.DataSource = ListaUsuario;
                cmbAsesorId.TextField = "nombre";
                cmbAsesorId.ValueField = "id";
                cmbAsesorId.DataBind();

                cmbAsesorId.Value = ListaUsuario.First().id.ToString(); ;

                cmbRemision_Cta_Nac.Enabled = Lista.First().mov80;

                if (Lista.First().mov80)
                {
                    List<CN_Matriz> ListaRemision = new List<CN_Matriz>();
                    Matriz.ConsultarRemisionesMov80(ref ListaRemision, sesion.Emp_Cnx);
                    cmbRemision_Cta_Nac.DataSource = ListaRemision;
                    cmbRemision_Cta_Nac.TextField = "nombre";
                    cmbRemision_Cta_Nac.ValueField = "id";
                    cmbRemision_Cta_Nac.DataBind();
                }

                List<CN_Matriz> ListaSolicitud = new List<CN_Matriz>();
                CN_Matriz datossol = new CN_Matriz();
                datossol.id_Cd = sesion.Id_Cd_Ver;
                datossol.id_matriz = idmatriz;
                datossol.id_Estructura = id;
                datossol.Desvinc = EsDesvinc;

                Matriz.ConsultarSolicitudes(datossol, ref ListaSolicitud, sesion.Emp_Cnx);

                if (ListaSolicitud.Count > 0)
                {

                    cmbAsesorId.Enabled = false;
                    txtClienteSIAN.Text = ListaSolicitud.First().clienteSian.ToString();
                    txtClienteSIAN.ReadOnly = true;

                    txtCalle.Text = ListaSolicitud.First().calle.ToString();
                    txtNumInterior.Text = ListaSolicitud.First().numinterior.ToString();
                    txtNumExterior.Text = ListaSolicitud.First().numExterior.ToString();
                    txtColonia.Text = ListaSolicitud.First().colonia.ToString();
                    txtMunicipio.Text = ListaSolicitud.First().municipio.ToString();
                    txtCP.Text = ListaSolicitud.First().Cp.ToString();
                    txtEstado.Text = ListaSolicitud.First().estado.ToString();
                    txtTelefonos.Text = ListaSolicitud.First().telefonos.ToString();
                    txtFAX.Text = ListaSolicitud.First().fax.ToString();
                    txtRFC.Text = ListaSolicitud.First().RFC.ToString();


                    txtCalle.ReadOnly = true;
                    txtNumInterior.ReadOnly = true;
                    txtNumExterior.ReadOnly = true;
                    txtColonia.ReadOnly = true;
                    txtMunicipio.ReadOnly = true;
                    txtCP.ReadOnly = true;
                    txtEstado.ReadOnly = true;
                    txtTelefonos.ReadOnly = true;
                    txtFAX.ReadOnly = true;
                    txtRFC.ReadOnly = true;

                    string Cte_NomComercial = "";
                    CN_CatCliente cn_cte = new CN_CatCliente();
                    int cliente = int.Parse(txtClienteSIAN.Text);

                    cn_cte.ClienteConsultaNombre(cliente, ref Cte_NomComercial, sesion);
                    txtRazonSocial.Text = Cte_NomComercial;
                    txtRazonSocial.ReadOnly = true;
                    cmbAsesorId.Value = ListaSolicitud.First().asesorid.ToString();
                    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();

                    CN_Comun.DevLlenaCombo(sesion.Id_Emp, sesion.Id_Cd_Ver, Convert.ToInt32(txtClienteSIAN.Text), sesion.Emp_Cnx, "spCatTerCte_Combo", ref cmbTerritorio);
                    cmbTerritorio.ValueField = "Id";
                    cmbTerritorio.TextField = "Id";
                    cmbTerritorio.DataBind();

                    cmbTerritorio.Value = ListaSolicitud.First().territorio.ToString();
                    cmbTerritorio.ReadOnly = true;

                    txtUsuario.Text = ListaSolicitud.First().usuario.ToString();

                    if (EsDesvinc)
                    {
                        txtComentarios.Text = "";
                        txtClienteSIAN.ReadOnly = true;
                        txtRazonSocial.ReadOnly = true;
                        cmbTerritorio.Enabled = false;
                    }
                }
                if (Request.QueryString["DesVinc"] == "1")
                {
                    lblTitulo.Text = "Desvinculación";
                }
            }
        }

        protected void ImgBuscarDireccionEntrega_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int idmatriz = Int32.Parse(Request.QueryString["IdMatriz"]);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "direccion", "AbrirBuscarDireccionEntrega('" + idmatriz + "')", true);
            }
            catch (Exception ex)
            {
                warning(ex.Message.ToString() + ": " + new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean EsDesvinc = false;
                if (Request.QueryString["DesVinc"] != null)
                {
                    EsDesvinc = true;
                }

                int idmatriz = Int32.Parse(Request.QueryString["IdMatriz"]);
                int id = Int32.Parse(Request.QueryString["Id"]);
                var objSession = ((CapaEntidad.Sesion)(Session["Sesion" + Session.SessionID]));

                if (txtClienteSIAN.Text == "")
                {
                    info("Favor de ingresar el numero del CLiente");
                    return;
                }

                if (cmbTerritorio.Value == null)
                {
                    info("Favor de ingresar el numero del Territorio");
                    return;
                }


                CN_Matriz registro = new CN_Matriz();

                registro.id_matriz = idmatriz;
                registro.id_Estructura = id;
                registro.sucursal = objSession.Id_Cd;
                registro.clienteSian = Convert.ToInt32(txtClienteSIAN.Text);
                registro.territorio = cmbTerritorio.Value.ToString();
                registro.razonsocial = txtRazonSocial.Text;
                if (cmbRemision_Cta_Nac.Value != null)
                {
                    registro.Rem_Cta_Nac = Convert.ToInt32(cmbRemision_Cta_Nac.Value.ToString());
                }
                registro.asesorid = Convert.ToInt32(cmbAsesorId.Value.ToString());
                registro.usuario = txtUsuario.Text;
                registro.Fecha = DateTime.Now;


                registro.sucursal = objSession.Id_Cd;

                if (EsDesvinc)
                {
                    registro.IdEstatus = 5;
                }
                else
                {
                    registro.IdEstatus = 1;
                }

                registro.id_Cd = objSession.Id_Cd;
                registro.id_emp = objSession.Id_Emp;

                registro.comentarios = txtComentarios.Text;

                int verificador = 0;
                CN_Cn_matriz Matriz = new CN_Cn_matriz();

                Matriz.InsertarSolicitudes(registro, ref verificador, objSession.Emp_Cnx);
                if (verificador != 0)
                {
                    registro.id = verificador;
                    registro.calle = txtCalle.Text.Trim();
                    registro.numinterior = txtNumInterior.Text.Trim();
                    registro.numExterior = txtNumExterior.Text.Trim();
                    registro.colonia = txtColonia.Text.Trim();
                    registro.municipio = txtMunicipio.Text.Trim();
                    registro.Cp = txtCP.Text.Trim();
                    registro.estado = txtEstado.Text.Trim();
                    registro.RFC = txtRFC.Text.Trim();
                    registro.telefono = txtTelefonos.Text.Trim();
                    registro.fax = txtFAX.Text.Trim();

                    Matriz.InsertarSolicitudesdirfiscal(registro, ref verificador, objSession.Emp_Cnx);

                    if (EsDesvinc)
                    {
                        info("Su solicitud de desvinculación ha sido procesada");
                    }
                    else
                    {
                        info("Su solicitud de vinculación ha sido procesada");
                    }
                    EnviaEmail(registro);
                }
                else
                {
                    info("Problema al guardar la solicitud, favor de revisar los datos");
                    return;
                }
            }
            catch (Exception ex)
            {
                warning("Problema al guardar la solicitud: " + ex.Message.ToString());
            }
        }

        protected void EnviaEmail(CN_Matriz solicitud)
        {
            try
            {
                CN_CatCNac_Solicitudes Csol = new CN_CatCNac_Solicitudes();
                var res = Csol.Consultar_Emails_Aut();

                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Cd = sesion.Id_Cd_Ver;
                configuracion.Id_Emp = sesion.Id_Emp;
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                cn_configuracion.Consulta(ref configuracion, sesion.Emp_Cnx);


                StringBuilder cuerpo_correo = new StringBuilder();
                cuerpo_correo.Append("<div align='center'>");
                cuerpo_correo.Append("<table><tr><td>");
                cuerpo_correo.Append("<IMG SRC=\"cid:companylogo\" ALIGN='left'></td>");
                cuerpo_correo.Append("<td></td>");
                cuerpo_correo.Append("</tr><tr><td colspan='2'><br><br><br></td>");
                cuerpo_correo.Append("</tr><tr>");
                cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='2'>");
                cuerpo_correo.Append("Se ha colocado una solicitud de vinculacion para el cliente " + solicitud.clienteSian.ToString());

                cuerpo_correo.Append(", de la sucursal " + solicitud.sucursal.ToString());
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
                catch (Exception ex)
                {
                    warning("Error al enviar el correo: " + ex.Message.ToString() + ". Favor de revisar la configuración del sistema");
                    return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return;
        }

        protected void txtClienteSIAN_ValueChanged(object sender, EventArgs e)
        {
            CN_CatCNac_Matriz cn = new CN_CatCNac_Matriz();

            try
            {
                var objSession = ((CapaEntidad.Sesion)(Session["Sesion" + Session.SessionID]));

                var cliente = cn.ConsultaCliente(Int32.Parse(txtClienteSIAN.Text), objSession.Id_Emp, objSession.Id_Cd);
                txtRazonSocial.Text = cliente.Cte_NomComercial;


                txtCalle.Text = cliente.Cte_FacCalle;
                txtNumInterior.Text = cliente.Cte_FacNumeroInterior;
                txtNumExterior.Text = cliente.Cte_FacNumero;
                txtColonia.Text = cliente.Cte_FacColonia;
                txtMunicipio.Text = cliente.Cte_FacMunicipio;
                txtEstado.Text = cliente.Cte_FacEstado;
                txtTelefonos.Text = cliente.Cte_FacTel;
                txtFAX.Text = cliente.Cte_Fax;
                txtRFC.Text = cliente.Cte_FacRfc;
                txtCP.Text = cliente.Cte_FacCp;

                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();


                CN_Comun.DevLlenaCombo(sesion.Id_Emp, sesion.Id_Cd_Ver, Convert.ToInt32(txtClienteSIAN.Text), sesion.Emp_Cnx, "spCatTerCte_Combo", ref cmbTerritorio);
                cmbTerritorio.ValueField = "Id";
                cmbTerritorio.TextField = "Id";
                cmbTerritorio.DataBind();

            }
            catch (Exception ex)
            {
                warning("Error a cargar los territorio: " + ex.Message.ToString()); ;
            }
        }
    }
}