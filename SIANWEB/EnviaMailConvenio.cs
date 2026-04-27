using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CapaEntidad;
using CapaNegocios;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;

namespace SIANWEB
{
    public class EnviaMailConvenio
    {

        #region Variables

        

        public string NombreArchivopdf;

        //public abstract MapPath(string virtualPath);

        private String Asunto;
        private StringBuilder CuerpoCorreo;
        private string Fin;
        private int Id_PC;

        private int tipocorreo;        // lo uso para obtener algunas consideraciones del correo a enviar 
        private int destinatarios;     // si es 1 incluyo a los correos de los riks 
        private int adicionales;      // si es 1 incluyo a los correos de los Gerentes y JOs
        private int anexos;            // si es 1 incluyo a los correos de los que capturaron en anexos en la pantalla correo ( 021 ) 
        private int administradores;   // si es 1 incluyo a los correos de los Administradores
        private int DatosConvenio;     // Si es 1 incluyo en el cuerpo los datos del convenio 
        private Sesion sesion; 
        private int Tipo_Habilitar ;   // si es 1 traer usuarios adicionales con opcion ver, 2 usar y 3 Gral nulo es todos 
        private int Detalle;
        
       
        public Convenio convenio
        {
            get { return convenio ; }
            set { convenio= value; }

        }
     
        string slblNombreArchivoPDF  = "";
        string slblContenidoPDF  = "";

        string sTxtPC_NoConvenio  = "";
        string sTxtPC_Nombre  = "";
        string sLblId_CatStr = "";
        string txtCuerpoMail = "";
        string agregarcorreo= "";

        #endregion

        public EnviaMailConvenio(int Id_PC, String Asunto, StringBuilder CuerpoCorreo, String Fin, int tipocorreo, int destinatarios, int adicionales, int anexos, int administradores, int DatosConvenio, Sesion sesion , int tipo_Habilitar,int detalle,  string pagregarcorreo)
        {
            this.Asunto = Asunto;
            this.CuerpoCorreo = CuerpoCorreo;
            this.Fin = Fin;
            this.tipocorreo = tipocorreo;
            this.destinatarios = destinatarios;
            this.adicionales = adicionales;
            this.anexos = anexos;
            this.administradores = administradores;
            this.DatosConvenio = DatosConvenio;
            this.Id_PC = Id_PC;
            this.sesion = sesion;
            this.Tipo_Habilitar = tipo_Habilitar;
            this.Detalle = detalle;
            this.agregarcorreo = pagregarcorreo;
        }

        public void EnviaMail()
        {

            try
            {


                // Si es tipo 0 envía el correo con el formato de requerimiento 016 Habilitar consecionario 
                if (tipocorreo == 1)
                {
                    ArmarEmail();
                }




            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void ArmarEmail()
        {
           
                Sesion session = new Sesion();
                session = sesion;


                //conveniod.Id_Emp = session.Id_Emp;
                //conveniod.Id_Cd = session.Id_Cd_Ver;

                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];

                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Cd = session.Id_Cd_Ver;
                configuracion.Id_Emp = session.Id_Emp;
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                cn_configuracion.Consulta(ref configuracion, session.Emp_Cnx);

                StringBuilder cuerpo_correo = new StringBuilder();
                cuerpo_correo = CuerpoCorreo;  //me traigo el cuerpo de correo recibido



                CN_Convenio cn_conv = new CN_Convenio();
                Convenio conv = new Convenio();
                List<ConvenioDet> List = new List<ConvenioDet>();


                try
                {

                    cn_conv.ConsultaConvenio(Id_PC, ref conv, Conexion);

                    slblNombreArchivoPDF = conv.PC_NombreArchivoPDF;
                    slblContenidoPDF = conv.PC_ArchivoPDF;

                    sTxtPC_NoConvenio = conv.PC_NoConvenio;
                    sTxtPC_Nombre = conv.PC_Nombre;
                    sLblId_CatStr = conv.Cat_DescCorta;

                    cn_conv.ConsultaConvenioDet(Id_PC, ref List, Conexion);


                }
                catch (Exception ex)
                {

                    throw ex;
                }


                if (DatosConvenio == 1)
                {




                    cuerpo_correo.Append("<table style='font-family: Verdana; font-size: 8pt'>");
                    cuerpo_correo.Append("<tr><td style ='Font-Bold='True'>Convenio Key:" + Id_PC.ToString());
                    cuerpo_correo.Append(" </td> <td>  &nbsp; </td> <td> &nbsp;  </td>  </tr>");

                    cuerpo_correo.Append("<tr><td style ='Font-Bold='True'>Convenio proveedor:" + sTxtPC_NoConvenio);
                    cuerpo_correo.Append(" </td> <td>  &nbsp; </td> <td> &nbsp;  </td>  </tr>");
                    cuerpo_correo.Append("<tr><td style ='Font-Bold='True'>Nombre de convenio:" + sTxtPC_Nombre);
                    cuerpo_correo.Append(" </td> <td>  &nbsp; </td> <td> &nbsp;  </td>  </tr>");
                    cuerpo_correo.Append("<tr><td style ='Font-Bold='True'>Categoría:" + sLblId_CatStr);
                    cuerpo_correo.Append(" </td> <td>  &nbsp; </td> <td> &nbsp;  </td>  </tr>");
                    cuerpo_correo.Append("</table>");



                }


                //try
                //{
                //cuerpo_correo.Append("<div align='left'>");

                cuerpo_correo.Append("<table style='font-family: Verdana; font-size: 8pt'>");

                if (Fin != "")
                {
                    cuerpo_correo.Append(" <tr> <td > " + Fin + "  </td>  </tr>");
                }
                cuerpo_correo.Append(" <tr> <td >  &nbsp;  </td>  </tr>");
                cuerpo_correo.Append("<tr><td colspan='2'>Si requiere alguna aclaración favor de contactar al área de Precios / Key Quimica SA de CV");
                cuerpo_correo.Append(" </td>    </tr> </table></div>");

                //string pathDestino = Server.MapPath("~/CarpetaConvenios") + "\\" + lblNombreArchivoPDF.Text;


                //if (lblNombreArchivoPDF.Text != "")
                //{

                //    cuerpo_correo.Append("<tr><td>");
                //    cuerpo_correo.Append(" </td> <td > Accesar a : <a href='{href}'>Rastreo de Solicitud</a> </td>  </tr>");

                //}




                //// string strUrl = (HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath + "/").Replace("//", "/").Replace("http:/", "http://");
                //string strUrl = "http://http://40.84.229.61/siancentral/CarpetaConvenios/";
                ////if (configuracion.WebURLCarpetaConvenios != "" && configuracion.WebURLCarpetaConvenios != null)
                ////{
                ////    strUrl = configuracion.RutaSistemaGastos + "/";
                ////}
                txtCuerpoMail = cuerpo_correo.ToString();

            //if (lblNombreArchivoPDF.Text != "")
            //{
            //    txtCuerpoMail = txtCuerpoMail.Replace("{href}", strUrl + lblNombreArchivoPDF.Text);


            //cuerpo_correo.Append("<table><tr><td>");
            //cuerpo_correo.Append("<IMG SRC=\"cid:companylogo\" ALIGN='left'></td>");
            //cuerpo_correo.Append("<td></td>");
            //cuerpo_correo.Append("</tr><tr><td colspan='2'><br><br><br></td>");
            //cuerpo_correo.Append("</tr><tr>");
            //cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='2'>");
            //cuerpo_correo.Append("Se ha colocado una solicitud de autorización de acuerdo comercial con el número  " + Convenio);

            //cuerpo_correo.Append(", de la sucursal " + session.Id_Cd_Ver);
            //cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");
            //string[] url = Request.Url.ToString().Split(new char[] { '/' });

            //cuerpo_correo.Append("<center><br>");
            //cuerpo_correo.Append("<a href='" + Request.Url.ToString().Replace(url[url.Length - 1], "") + "Capconveniod.aspx?Id=" + Convenio + "&Accion=2&PermisoGuardar=1&PermisoModificar=1&PermisoEliminar=1&PermisoImprimir=1'" + ">");
            //cuerpo_correo.Append("Solicitud de autorización de acuerdos comerciales</a></font></center>");
            //cuerpo_correo.Append("</td></tr></table></div>");
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                sm.EnableSsl = true;
                MailMessage m = new MailMessage();
                m.From = new MailAddress(configuracion.Mail_Remitente);

                //agregar destinatarios 
            
                if (agregarcorreo != "")
                {
                    m.Bcc.Add(new MailAddress(agregarcorreo));
                }

                if (administradores == 1)
                {

                    //List<ProPrecioConv_Usuarios> lista = this.ListaAdministradores;
                    List<ProPrecioConv_Usuarios> lista = new List<ProPrecioConv_Usuarios>();
                    lista = ObtenerAdministradores();

                    //buscar el registro de Categoria en la lista para ver si ya existe
                    for (int i = 0; i < lista.Count; i++)
                    {
                        ProPrecioConv_Usuarios usuAdministrador = lista[i];
                        if (usuAdministrador.Usu_correo != "")
                        {
                            m.Bcc.Add(new MailAddress(usuAdministrador.Usu_correo));
                        }

                    }
                }

                if (anexos == 1)
                {
                    //List<ProPrecioConv_Usuarios> listaanexos = this.ListaCorreosAnexos;
                    List<ProPrecioConv_Usuarios> listaanexos = new List<ProPrecioConv_Usuarios>();
                    listaanexos = ObtenerCorreosAnexos(Convert.ToInt32(Id_PC.ToString()));

                    for (int i = 0; i < listaanexos.Count; i++)
                    {
                        ProPrecioConv_Usuarios usuanexo = listaanexos[i];
                        if (usuanexo.Usu_correo != "")
                        {
                            m.Bcc.Add(new MailAddress(usuanexo.Usu_correo));
                        }

                    }
                }

                if (adicionales == 1)
                {
                    //List<ProPrecioConv_Usuarios> listaadicionales = this.ListaCorreosAdicionales;
                    List<ProPrecioConv_Usuarios> listaadicionales = new List<ProPrecioConv_Usuarios>();
                    listaadicionales = ObtenerCorreosAdicionales();

                    for (int i = 0; i < listaadicionales.Count; i++)
                    {
                        ProPrecioConv_Usuarios usuadicional = listaadicionales[i];
                        if (usuadicional.Usu_correo != "")
                        {
                            m.Bcc.Add(new MailAddress(usuadicional.Usu_correo));
                        }

                    }
                }

                if (destinatarios == 1)
                {
                    //ObtenerUsuarioBon(); //obtener los usuarios    
                    //List<ConvenioCorreos> listadestinatarios = this.ListaDestinatario;
                    List<ConvenioCorreos> listadestinatarios = new List<ConvenioCorreos>();
                    listadestinatarios = ObtenerDestinatarios();

                    

                    for (int i = 0; i < listadestinatarios.Count; i++)
                    {
                        ConvenioCorreos usudestinatarios = listadestinatarios[i];
                        if (usudestinatarios.U_correo != "")
                        {
                            m.Bcc.Add(new MailAddress(usudestinatarios.U_correo));
                        }

                    }
                }
 

                m.Subject = Asunto;   
                m.IsBodyHtml = true;
                string body = txtCuerpoMail;
                AlternateView vistaHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                //Esto queda dentro de un try por si llegan a cambiar la imagen el correo como quiera se mande
              
                //try
                //{
                //    LinkedResource logo = new LinkedResource(MapPath(@"Imagenes/logo.jpg"), MediaTypeNames.Image.Jpeg);
                //    logo.ContentId = "companylogo";
                //    vistaHtml.LinkedResources.Add(logo);
                //}
                //catch (Exception)
                //{
                //}

                m.AlternateViews.Add(vistaHtml);
                try
                {
                    sm.Send(m);
                }
                catch (Exception)
                {

                    //Alerta("Error al enviar el correo. Favor de revisar la configuración del sistema");
                    return;
                }
                //Alerta("Correo enviado correctamente");
            }
            //}

            //catch (Exception ex)
            //{
            //    throw ex;
            //}





        private List<ConvenioCorreos> ObtenerDestinatarios()  //Obtener usuario destinatarios
        {
            try
            {
                CN_Convenio centro = new CN_Convenio();
                List<ConvenioCorreos> listaUsuarioDestinatario = new List<ConvenioCorreos>();
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];

                centro.ConsultarConvenioCorreos(Id_PC, 1, ref listaUsuarioDestinatario, Conexion);
                return listaUsuarioDestinatario;
                
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private List<ProPrecioConv_Usuarios> ObtenerAdministradores()
        {
            try
            {
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionSIANCentral"];
                CN_ConfiguracionDiasConvenio centro = new CN_ConfiguracionDiasConvenio();
                List<ProPrecioConv_Usuarios> listaadministradores = new List<ProPrecioConv_Usuarios>();
                //el tipo de usuario administrador es el 1 
                centro.ConsultarConfiguracion_Administrador(ref listaadministradores, sesion.Id_Cd_Ver, sesion.Id_Emp, 1, Conexion);
                //ListaAdministradores = listaadministradores;
                return listaadministradores;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private List<ProPrecioConv_Usuarios> ObtenerCorreosAnexos(int Id_PC)
        {
            try
            {
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];

                CN_ConfiguracionDiasConvenio centro = new CN_ConfiguracionDiasConvenio();
                List<ProPrecioConv_Usuarios> listaCorreosAnexos = new List<ProPrecioConv_Usuarios>();
                centro.ConsultarCorreosAnexos(ref listaCorreosAnexos, sesion.Id_Cd_Ver, sesion.Id_Emp, Id_PC, Conexion);
                //ListaCorreosAnexos = listaCorreosAnexos;
                return listaCorreosAnexos;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        private List<ProPrecioConv_Usuarios> ObtenerCorreosAdicionales()
        {
            try
            {
                 
                CN_ConfiguracionDiasConvenio centro = new CN_ConfiguracionDiasConvenio();

                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];

                List<ProPrecioConv_Usuarios> listaadicionales = new List<ProPrecioConv_Usuarios>();
                centro.ConsultarCorreosAdicionales(ref listaadicionales, sesion.Id_Cd_Ver, sesion.Id_Emp, Id_PC, Tipo_Habilitar, Conexion);
                //ListaCorreosAdicionales = listaadicionales;
                return listaadicionales;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        

    }
}