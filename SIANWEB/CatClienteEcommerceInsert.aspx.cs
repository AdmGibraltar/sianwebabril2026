using CapaEntidad;
using CapaNegocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using System.Configuration;

namespace SIANWEB
{
    public partial class CatClienteEcommerceInsert : System.Web.UI.Page
    {
        #region Variables
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private Sesion sesion { get { return (Sesion)Session["Sesion" + Session.SessionID]; } set { Session["Sesion" + Session.SessionID] = value; } }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["portalpass"] = null;
                inicializar();
            }
        }


        private void inicializar()
        {
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            if (sesion.Id_Rik == -1)
            {
                CN_Comun.DevLlenaCombo(1, sesion.Id_Emp, sesion.Id_Cd_Ver, 2, null, sesion.Emp_Cnx, "spCatCliente_Combo", ref CmbCliente);
            }
            else
            {
                CN_Comun.DevLlenaCombo(1, sesion.Id_Emp, sesion.Id_Cd_Ver, 2, sesion.Id_Rik, sesion.Emp_Cnx, "spCatCliente_Combo", ref CmbCliente);
            }
            if (Request.QueryString["id"] != null)
            {
                consultar(Convert.ToInt32(Request.QueryString["id"].ToString()));
            }


        }

        public void consultar(int id)
        {
            List<Portakey> List = new List<Portakey>();
            Portakey Registro = new Portakey();
            Registro.Id_Emp = sesion.Id_Emp;
            Registro.id_Cd = sesion.Id_Cd;
            Registro.Id_Portal = id;
            CN_ClienteEcommerce cn = new CN_ClienteEcommerce();
            cn.ConsultaDatosPortal(Registro, ref List, sesion.Emp_Cnx);

            if (List.Count > 0)
            { 
                CmbCliente.Value = List.First().id_cte.ToString();
                txtUsuario.Text = List.First().Correo;
                TxtNombre.Text = List.First().nombre;
                txtApellido.Text = List.First().Apellidos;
                Session["portalpass"] = List.First().Correo;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (TxtNombre.Text.Trim().Length == 0 || txtApellido.Text.Trim().Length == 0)
                {
                    mensaje("Favor de Capturar Todos los Datos.");
                    return;
                }
                string mensajeInfo = "";

                CN_ClienteEcommerce Cn = new CN_ClienteEcommerce();
                ClienteECommerce Admon = new ClienteECommerce();
                List<ClienteECommerce> lista = new List<ClienteECommerce>();
                List<ClienteECommerce> Correo = new List<ClienteECommerce>();
                List<ClienteECommerce> segmento = new List<ClienteECommerce>();
                string ConexionCentral = ConfigurationManager.AppSettings["strConnectionCentral"].ToString().Trim();

                string password = "";
                Admon.Id_Emp = sesion.Id_Emp;
                Admon.Id_Cd = sesion.Id_Cd;
                Admon.Id_Usu = sesion.Id_Rik;
                Admon.Id_Cte = Convert.ToInt32(CmbCliente.Value.ToString());
                Admon.Nombre = txtUsuario.Text;
                Admon.Contrasena = password;
                Admon.NombreCliente = CmbCliente.Text;
                Admon.NombreUsu = TxtNombre.Text.Trim();
                Admon.Apellido = txtApellido.Text.Trim();
                List<ClienteECommerce> Lista = new List<ClienteECommerce>();



                Cn.ConsultadatosoCliente(Admon, ref Lista, sesion.Emp_Cnx);
                if (Lista.Count > 0)
                {
                    foreach (ClienteECommerce Datos in Lista)
                    {
                        if (Datos.Cte_FacRfc == "" || Datos.Cte_FacRfc == null)
                        {
                            if (mensajeInfo == "")
                            {
                                mensajeInfo = "Falta información en el catalogo de cliente de: RFC";
                            }
                            else
                            {
                                mensajeInfo = mensajeInfo + ", RFC";
                            }
                        }
                        if (Datos.calle == "" || Datos.calle == null)
                        {
                            if (mensajeInfo == "")
                            {
                                mensajeInfo = "Falta información en el catalogo de cliente de: Calle";
                            }
                            else
                            {
                                mensajeInfo = mensajeInfo + ", Calle";
                            }
                        }
                        if (Datos.Cte_Numero == "" || Datos.Cte_Numero == null)
                        {
                            if (mensajeInfo == "")
                            {
                                mensajeInfo = "Falta información en el catalogo de cliente de: Número";
                            }
                            else
                            {
                                mensajeInfo = mensajeInfo + ", Número";
                            }
                        }
                        if (Datos.Cte_Municipio == "" || Datos.Cte_Municipio == null)
                        {
                            if (mensajeInfo == "")
                            {
                                mensajeInfo = "Falta información en el catalogo de cliente de: Municipio";
                            }
                            else
                            {
                                mensajeInfo = mensajeInfo + ", Municipio";
                            }
                        }
                        if (Datos.Cte_Estado == "" || Datos.Cte_Estado == null)
                        {
                            if (mensajeInfo == "")
                            {
                                mensajeInfo = "Falta información en el catalogo de cliente de: Estado";
                            }
                            else
                            {
                                mensajeInfo = mensajeInfo + ", Estado";
                            }
                        }
                        if (Datos.Cte_CP == null)
                        {
                            if (mensajeInfo == "")
                            {
                                mensajeInfo = "Falta información en el catalogo de cliente de: Código Postal";
                            }
                            else
                            {
                                mensajeInfo = mensajeInfo + ", Código Postal";
                            }
                        }
                        if (Datos.Cte_Telefono == "" || Datos.Cte_Telefono == null)
                        {
                            if (mensajeInfo == "")
                            {
                                mensajeInfo = "Falta información en el catalogo de cliente de: Teléfono";
                            }
                            else
                            {
                                mensajeInfo = mensajeInfo + ", Teléfono";
                            }
                        }
                        if (Datos.DirEntregacte_calle == "" || Datos.DirEntregacte_calle == null)
                        {
                            if (mensajeInfo == "")
                            {
                                mensajeInfo = "Falta información en el catalogo de cliente de: Calle dirección de entrega";
                            }
                            else
                            {
                                mensajeInfo = mensajeInfo + ", dirección de entrega";
                            }
                        }
                        if (Datos.DirEntregacte_numero == "" || Datos.DirEntregacte_numero == null)
                        {
                            if (mensajeInfo == "")
                            {
                                mensajeInfo = "Falta información en el catalogo de cliente de: Número dirección de entrega";
                            }
                            else
                            {
                                mensajeInfo = mensajeInfo + ", Número dirección de entrega";
                            }
                        }
                        if (Datos.DirEntregaCte_municipio == "" || Datos.DirEntregaCte_municipio == null)
                        {
                            if (mensajeInfo == "")
                            {
                                mensajeInfo = "Falta información en el catalogo de cliente de: Municipio dirección de entrega";
                            }
                            else
                            {
                                mensajeInfo = mensajeInfo + ", Municipio dirección de entrega";
                            }
                        }
                        if (Datos.DirEntregaCte_Estado == "" || Datos.DirEntregaCte_Estado == null)
                        {
                            if (mensajeInfo == "")
                            {
                                mensajeInfo = "Falta información en el catalogo de cliente de: Estado dirección de entrega";
                            }
                            else
                            {
                                mensajeInfo = mensajeInfo + ", Estado dirección de entrega";
                            }
                        }
                        if (Datos.DirEntregacte_Cp == null)
                        {
                            if (mensajeInfo == "")
                            {
                                mensajeInfo = "Falta información en el catalogo de cliente de: Código Postal dirección de entrega";
                            }
                            else
                            {
                                mensajeInfo = mensajeInfo + ", Código Postal dirección de entrega";
                            }
                        }
                        if (Datos.DirEntregacte_telefono == "" || Datos.DirEntregacte_telefono == null)
                        {
                            if (mensajeInfo == "")
                            {
                                mensajeInfo = "Falta información en el catalogo de cliente de: Teléfono dirección de entrega";
                            }
                            else
                            {
                                mensajeInfo = mensajeInfo + ", Teléfono dirección de entrega";
                            }
                        }

                        if (Datos.nombre == "" || Datos.nombre == null || Datos.nombre2 == "" || Datos.nombre2 == null)
                        {
                            if (mensajeInfo == "")
                            {
                                mensajeInfo = "Falta información en el catalogo de cliente de: Contacto (debera de incluir nombre completo y apellido paterno)";
                            }
                            else
                            {
                                mensajeInfo = mensajeInfo + ",  Contacto (debera de incluir nombre completo y apellido paterno)";
                            }
                        }

                    }
                }

                List<ClienteECommerce> ListaCredito = new List<ClienteECommerce>();

                Cn.ConsultadatosoClienteCredito(Admon, ref ListaCredito, sesion.Emp_Cnx);

                if (ListaCredito.Count > 0)
                {
                    if (ListaCredito.First().Credito == 0 || ListaCredito.First().limite == 0)
                    {
                        if (mensajeInfo == "")
                        {
                            mensajeInfo = "El usuario no puede capturarse, Requiere tener credito activo y/o límite de credito mayor que cero.";
                        }
                        else
                        {
                            mensajeInfo = mensajeInfo + " <br> El usuario no puede capturarse, Requiere tener credito activo y/o límite de credito mayor que cero.";
                        }
                    }
                }
                else
                {
                    if (mensajeInfo == "")
                    {
                        mensajeInfo = "El usuario no puede capturarse, Requiere tener credito activo y/o límite de credito mayor que cero.";
                    }
                    else
                    {
                        mensajeInfo = mensajeInfo + " <br> El usuario no puede capturarse, Requiere tener credito activo y/o límite de credito mayor que cero.";
                    }
                }

                if (mensajeInfo != "")
                {
                    mensaje(mensajeInfo);
                    return;
                }

                PedidoVtaInst pedido = new PedidoVtaInst();
                List<PedidoVtaInst> listaAcys = new List<CapaEntidad.PedidoVtaInst>();
                CN_CapPedidoVtaInst vtaInst = new CN_CapPedidoVtaInst();
                pedido.Id_Emp = sesion.Id_Emp;
                pedido.Id_Cd = sesion.Id_Cd_Ver;
                pedido.Id_Cte = Convert.ToInt32(CmbCliente.Value.ToString());

                CN_CapPedido cn_capPedido = new CN_CapPedido();
                vtaInst.ConsultaClienteAcys(pedido, ref listaAcys, sesion.Emp_Cnx);

                if (listaAcys.Count == 0) 
                {
                    var acuerdo = "El Cliente no cuenta con un ACyS Autorizado para mostrar en el Portal Key";
                    mensaje(acuerdo);
                    return;
                }

                CN_PortalKey Cnportal = new CN_PortalKey();
                List<Portakey> Listaportal = new List<Portakey>();
                Portakey Registro = new Portakey();
                Registro.Tipo = 1;
                if (Request.QueryString["id"] != null)
                {
                    Registro.Id_Portal = Convert.ToInt32(Request.QueryString["id"].ToString());
                }
                Registro.Id_Emp = sesion.Id_Emp;
                Registro.id_Cd = sesion.Id_Cd;
                Registro.nombre = TxtNombre.Text;
                Registro.Apellidos = txtApellido.Text;
                Registro.id_cte = Convert.ToInt32(CmbCliente.Value.ToString());
                Registro.Correo = txtUsuario.Text;

                Cnportal.ConsultarCorreo(Registro, ref Listaportal, sesion.Emp_Cnx);
                if (Listaportal.Count > 0)
                {
                    mensaje("El usuario no puede capturarse, el correo ya esta siendo utilizado por el cliente para el acceso al portal.");
                    return;
                }


                if (Request.QueryString["id"] != null)
                {
                    Cn.ModificarLista(Admon, sesion.Emp_Cnx, ref password);

                    if (password != "")
                    {
                        //EnviarCorreo(Admon.Id_Cte, Admon.Nombre, sesion.Emp_Cnx, txtUsuario.Text, password, Admon.Id_Usu, Admon.NombreCliente);
                        mensajeExito("Se guardo la información correctamente");
                    }
                }
                else
                {
                    List<Portakey> List = new List<Portakey>();
                    CN_PortalKey Portal = new CN_PortalKey();

                    Portakey Datos = new Portakey();

                    Datos.Id_Emp = sesion.Id_Emp;
                    Datos.Id_Portal = -1;
                    Datos.id_cte = Convert.ToInt32(CmbCliente.Value.ToString());
                    Datos.id_Cd = sesion.Id_Cd_Ver;
                    Datos.Id_Direccion = -1;
                    Datos.Tipo = 1;

                    Portal.ConsultaClienteCapturados(Datos, ref List, sesion.Emp_Cnx);
                    if (List.Count() > 0)
                    {
                        mensaje("No se Puede Capturar el Cliente, El cliente ya esta registrado");
                        return;
                    }


                    Cn.InsertarLista(Admon, sesion.Emp_Cnx, ref password);

                    mensajeExito("Se guardo la Información Correctamente, el Cliente podra Acceder a Partir del día de Siguente a partir de las 8 A.M.");

                    //if (password != "")
                    //{
                    //    EnviarCorreo(Admon.Id_Cte, Admon.Nombre, sesion.Emp_Cnx, txtUsuario.Text, password, Admon.Id_Usu, Admon.NombreCliente); 
                    //}
                }
            }
            catch (Exception ex)
            {
                mensaje(ex.Message);
            }
        }

        private void EnviarCorreo(int id_cte, string Usuario, string conexion, string Email, string password = "", int Id_Usu = 0, string razonSocial = "")
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                PedidoVtaInst pedido = new PedidoVtaInst();
                List<UsuarioCorreo> ListaCorreoUsuario = new List<UsuarioCorreo>();

                CN_ClienteEcommerce Cn = new CN_ClienteEcommerce();
                int verificador = 0;
                Cn.InsertarJobMail(id_cte, Id_Usu, password, Email, conexion, ref verificador);

                CN_enviarCorreo Envia = new CN_enviarCorreo();
                Envia.ConsultarCorreoUserXPefril(sesion.Id_Emp, sesion.Id_Cd, "Gerente de Sucursal", conexion, ref ListaCorreoUsuario);

                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Cd = sesion.Id_Cd;
                configuracion.Id_Emp = sesion.Id_Emp;
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                cn_configuracion.Consulta(ref configuracion, conexion);
                StringBuilder cuerpo_correo = getHtmlEmailGerente(sesion.Id_Cd, Usuario, sesion.U_Nombre, razonSocial);

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);

                sm.EnableSsl = true;
                MailMessage m = new MailMessage();
                //m.From = new MailAddress(configuracion.Mail_Portal);
                m.From = new MailAddress(configuracion.Mail_Remitente);

                foreach (UsuarioCorreo correo in ListaCorreoUsuario)
                {
                    m.To.Add(new MailAddress(correo.U_Correo));
                }
                //m.To.Add(new MailAddress("igarrido@axsistec.com"));
                m.Subject = "Alta de usuario al Portal Key";
                m.IsBodyHtml = true;
                string body = cuerpo_correo.ToString();
                AlternateView vistaHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);

                //Esto queda dentro de un try por si llegan a cambiar la imagen el correo como quiera se mande
                try
                {
                    LinkedResource logo = new LinkedResource(MapPath(@"Imagenes/LogoNuevo.jpg"), MediaTypeNames.Image.Jpeg);
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
                    mensaje("Fallo en enviar el correo electronico, " + ex.Message);
                }

            }
            catch (Exception ex)
            {
                mensaje("Fallo en la configuración del sistema");
            }
        }

        public StringBuilder getHtmlEmailGerente(int id_cte = 0, string Usuario = "", string Representante = "", string razonSocial = "")
        {

            StringBuilder cuerpo_correo = new StringBuilder();
            cuerpo_correo.Append("<!DOCTYPE html>");
            cuerpo_correo.Append("<html lang='en'>");
            cuerpo_correo.Append("<head><meta charset = 'UTF-8'><meta http - equiv = 'X-UA-Compatible' content = 'IE=edge' ><title> EMAIl </title></head>");
            cuerpo_correo.Append("<body>");


            cuerpo_correo.Append("<style type='text / css'>");

            cuerpo_correo.Append("url('https://www.portalkey.com.mx/static/version1619652248/frontend/Morwi/key/es_MX/css/email-fonts.css';);html{font-size:62.5%;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;font-size-adjust:100%}body{color:#545454;font-family:'Effra Lt','Helvetica Neue',Helvetica,Arial,sans-serif;font-style:normal;font-weight:400;line-height:1.42857143;font-size:14px}p{margin-top:0;margin-bottom:10px}abbr[title]{border-bottom:1px dotted #ccc;cursor:help}b,strong{font-weight:700}em,i{font-style:italic}mark{background:#f6f6f6;color:#002c3d}small,.small{font-size:12px}hr{border:0;border-top:1px solid #ccc;margin-bottom:20px;margin-top:20px}sub,sup{font-size:71.42857143000001%;line-height:0;position:relative;vertical-align:baseline}sup{top:-.5em}sub{bottom:-.25em}dfn{font-style:italic}h1{font-family:'Effra Rg','Helvetica Neue',Helvetica,Arial,sans-serif;font-weight:300;line-height:1.1;font-size:26px;margin-top:0;margin-bottom:20px}h2{font-family:'Effra Rg','Helvetica Neue',Helvetica,Arial,sans-serif;font-weight:300;line-height:1.1;font-size:26px;margin-top:25px;margin-bottom:20px}h3{font-family:'Effra Rg','Helvetica Neue',Helvetica,Arial,sans-serif;font-weight:300;line-height:1.1;font-size:18px;margin-top:20px;margin-bottom:10px}h4{font-family:'Effra Rg','Helvetica Neue',Helvetica,Arial,sans-serif;font-weight:700;line-height:1.1;font-size:14px;margin-top:20px;margin-bottom:20px}h5{font-family:'Effra Rg','Helvetica Neue',Helvetica,Arial,sans-serif;font-weight:700;line-height:1.1;font-size:12px;margin-top:20px;margin-bottom:20px}h6{font-family:'Effra Rg','Helvetica Neue',Helvetica,Arial,sans-serif;font-weight:700;line-height:1.1;font-size:10px;margin-top:20px;margin-bottom:20px}h1 small,h2 small,h3 small,h4 small,h5 small,h6 small,h1 .small,h2 .small,h3 .small,h4 .small,h5 .small,h6 .small{color:#00aeef;font-family:'Effra Lt','Helvetica Neue',Helvetica,Arial,sans-serif;font-style:normal;font-weight:400;line-height:1}a,.alink{color:#4dc6f4;text-decoration:none}a:visited,.alink:visited{color:#4dc6f4;text-decoration:none}a:hover,.alink:hover{color:#4dc6f4;text-decoration:underline}a:active,.alink:active{color:#00aeef;text-decoration:underline}ul,ol{margin-top:0;margin-bottom:25px}ul>li,ol>li{margin-top:0;margin-bottom:10px}ul ul,ol ul,ul ol,ol ol{margin-bottom:0}dl{margin-bottom:20px;margin-top:0}dt{font-weight:700;margin-bottom:5px;margin-top:0}dd{margin-bottom:10px;margin-top:0;margin-left:0}code,kbd,pre,samp{font-family:Menlo,Monaco,Consolas,'Courier New',monospace}code{background:#f6f6f6;color:#007caa;padding:2px 4px;font-size:12px;white-space:nowrap}kbd{background:#f6f6f6;color:#007caa;padding:2px 4px;font-size:12px}pre{background:#f6f6f6;border:1px solid #ccc;color:#007caa;line-height:1.42857143;margin:0 0 10px;padding:10px;font-size:12px;display:block;word-wrap:break-word}pre code{background-color:transparent;border-radius:0;color:inherit;font-size:inherit;padding:0;white-space:pre-wrap}blockquote{border-left:0 solid #ccc;margin:0 0 20px 40px;padding:0;color:#545454;font-family:'Effra Lt','Helvetica Neue',Helvetica,Arial,sans-serif;font-style:italic;font-weight:400;line-height:1.42857143;font-size:14px}blockquote p:last-child,blockquote ul:last-child,blockquote ol:last-child{margin-bottom:0}blockquote footer,blockquote small,blockquote .small{color:#00aeef;line-height:1.42857143;font-size:10px;display:block}blockquote footer:before,blockquote small:before,blockquote .small:before{ }blockquote cite{font-style:normal}blockquote:before,blockquote:after{content:''}q{quotes:none}q:before,q:after{content:'';content:none}cite{font-style:normal}.shipment-track th{text-align:left}.shipment-track>tbody>tr>th,.shipment-track>tfoot>tr>th,.shipment-track>tbody>tr>td,.shipment-track>tfoot>tr>td{vertical-align:top}.shipment-track>thead>tr>th,.shipment-track>thead>tr>td{vertical-align:bottom}.shipment-track>thead>tr>th,.shipment-track>tbody>tr>th,.shipment-track>tfoot>tr>th,.shipment-track>thead>tr>td,.shipment-track>tbody>tr>td,.shipment-track>tfoot>tr>td{padding:0 10px}.email-items th{text-align:left}.email-items>tbody>tr>th,.email-items>tfoot>tr>th,.email-items>tbody>tr>td,.email-items>tfoot>tr>td{vertical-align:top}.email-items>thead>tr>th,.email-items>thead>tr>td{vertical-align:bottom}.email-items>thead>tr>th,.email-items>tbody>tr>th,.email-items>tfoot>tr>th,.email-items>thead>tr>td,.email-items>tbody>tr>td,.email-items>tfoot>tr>td{padding:0 10px}@media only screen and (max-width:639px){html,body{background-color:#fff;width:100% !important}.main{max-width:100% !important;min-width:240px;width:auto !important}.rma-items td,.rma-items th{font-size:12px !important;padding:5px !important}}@media only screen and (max-width:479px){.header,.main-content,.footer{padding:25px 10px !important}.footer td{display:block;width:auto !important}.email-features>tbody>tr>td{clear:both;display:block;padding-top:20px;width:auto !important}.email-summary h1{font-size:24px !important}.order-details .address-details,.order-details .method-info{display:block;padding:10px 0 !important;width:auto !important}.order-details .address-details h3,.order-details .method-info h3{margin-bottom:5px !important;margin-top:0 !important}.button .inner-wrapper{width:100% !important}.button .inner-wrapper td a{font-size:16px}}body,table,td,a{-ms-text-size-adjust:100%;-webkit-text-size-adjust:100%}img{-ms-interpolation-mode:bicubic}table,td{mso-table-lspace:0pt;mso-table-rspace:0pt}a:visited{color:#4dc6f4 !important;text-decoration:none !important}a:hover{color:#4dc6f4 !important;text-decoration:underline !important}a:active{color:#00aeef !important;text-decoration:underline !important}.no-link a,.address-details a{color:#545454 !important;cursor:default !important;text-decoration:none !important}.button .inner-wrapper td:hover{background-color:#005192 !important}.button .inner-wrapper a:active,.button .inner-wrapper td:active{background-color:#005192 !important}.button a:active,.button a:hover,.button a:visited{border:1px solid #005192;color:#fff !important;text-decoration:none !important}.email-items{overflow-x:auto;overflow-y:hidden;width:100%;-ms-overflow-style:-ms-autohiding-scrollbar;-webkit-overflow-scrolling:touch}");
            cuerpo_correo.Append("</style>");
            cuerpo_correo.Append("<style type='text / css'> url('https://www.portalkey.com.mx/static/version1619652248/frontend/Morwi/key/es_MX/css/email-fonts.css';);a:visited{color:#4dc6f4;text-decoration:none}a:hover{color:#4dc6f4;text-decoration:underline}a:active{color:#00aeef;text-decoration:underline}.footer small a:active,.footer small a:visited,.footer small a:hover{color:#ffffff !important}</style>");



            cuerpo_correo.Append("<table class='wrapper' width='100 % ' style='border - collapse: collapse; margin: 0 auto; '><tbody><tr>");
            cuerpo_correo.Append("<td  style='font - family: 'Effra Lt', 'Helvetica Neue', Helvetica, Arial, sans - serif; vertical - align: top; padding - bottom: 30px; width: 100 %; '>");
            cuerpo_correo.Append("<table class='main'  style='border - collapse: collapse; margin: 0 auto; text - align: left; width: 660px; '>");
            cuerpo_correo.Append("<tbody><tr>");
            cuerpo_correo.Append("<td class='header' style='font - family: 'Effra Lt', 'Helvetica Neue', Helvetica, Arial, sans - serif; vertical - align: top; padding: 25px; background - color: #fff;'>");
            cuerpo_correo.Append("<a class='logo'; style='color: #4dc6f4; text-decoration: none;'>");
            cuerpo_correo.Append("<img width='200' SRC=\"cid:companylogo\"; alt='Key Química' border='0' style='border: 0; height: auto; line-height: 100%; outline: none; text-decoration: none;'></a>");
            cuerpo_correo.Append("</td>");
            cuerpo_correo.Append("</tr>");


            cuerpo_correo.Append("<tr>");
            cuerpo_correo.Append("<td class='main - content' style='font - family: 'Effra Lt', 'Helvetica Neue', Helvetica, Arial, sans - serif; vertical - align: top; background - color: #fff; padding: 25px;'>");


            cuerpo_correo.Append("<p style='margin - top: 0; margin - bottom: 10px; '> ");
            cuerpo_correo.Append("<p class='greeting company - greeting' style='margin - top: 0; margin - bottom: 25px; '><strong style='font - weight: 700; '><br>Buen día, Estimado/a. </strong></p> <br><br> " + Representante + " dio de alta un usuario para el Portal Key.<br>");

            cuerpo_correo.Append("El usuario fue autorizado de manera automática, esta notificación es informativa y no es necesario tomar ninguna acción. <br>");

            cuerpo_correo.Append("<br>Los datos del cliente al que se le proporcionó un usuario son los siguientes: <br/><br/>");
            cuerpo_correo.Append("Número de cliente:" + id_cte + ". <br>");
            cuerpo_correo.Append("Razón social:" + razonSocial + ". <br>");
            cuerpo_correo.Append("Nombre de Usuario:" + Usuario + ". <br><br>");

            cuerpo_correo.Append("Sistema automático. favor de no responder a este correo. </br>  Fecha de realización: " + DateTime.Now.ToString("dd/MM/yyyy") + "<br>");

            cuerpo_correo.Append("</p>");
            cuerpo_correo.Append(" </ td > ");
            cuerpo_correo.Append("</ tr > ");
            cuerpo_correo.Append("</table></td></tr>");
            cuerpo_correo.Append("</tbody></table>");
            cuerpo_correo.Append("</body></html> ");

            return cuerpo_correo;
        }

        public static string Encriptar(string _cadenaAencriptar)
        {
            string result = string.Empty;
            byte[] encryted =
            System.Text.Encoding.Unicode.GetBytes(_cadenaAencriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        /// Esta función "desencripta" la cadena que le envíamos en el parámentro de entrada.
        public static string DesEncriptar(string _cadenaAdesencriptar)
        {
            string result = string.Empty;
            byte[] decryted =
            Convert.FromBase64String(_cadenaAdesencriptar);
            //result = 
            System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }


        #region mensaje

        private void mensaje(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "modalMensaje('" + mensaje + "')", true);
        }

        private void mensajeExito(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "modalMensajeExito('" + mensaje + "')", true);
        }
        #endregion
    }
}