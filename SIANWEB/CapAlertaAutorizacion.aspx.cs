using CapaEntidad;
using CapaNegocios;
using DevExpress.Export;
using DevExpress.Utils;
using DevExpress.Web;
using DevExpress.Web.Bootstrap;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIANWEB
{
    public partial class CapAlertaAutorizacion : System.Web.UI.Page
    {

        #region Variables
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private Sesion sesion { get { return (Sesion)Session["Sesion" + Session.SessionID]; } set { Session["Sesion" + Session.SessionID] = value; } }


        private string Emp_CnxCentral
        {
            get { return ConfigurationManager.AppSettings.Get("strConnectionCentral"); }
        }

        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {

            gridBuscar.SettingsBehavior.AllowSelectByRowClick = true;

        }

        protected void Page_Load(object sender, EventArgs e)
        {


            try
            {
                Sesion Sesion = new Sesion();
                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                else
                {

                    if (Session["listaSolicitudes" + Session.SessionID] != null)
                    {
                        gridBuscar.DataSource = Session["listaSolicitudes" + Session.SessionID];
                        gridBuscar.DataBind();
                    }

                    if (!IsPostBack)
                    {
                        if (sesion != null)
                        {
                            Session["listaSolicitudes" + Session.SessionID] = null;

                            inicializar();
                            string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];

                            cmbBuscarRepresentante.Items.Add("--Todos--", "-1");
                            cmbBuscarRepresentante.Value = "-1";

                            CmbTipoSolicitud.Items.Add("--Todos--", "-1");


                            CmbTipoSolicitud.Items.Add("CRM", "1");
                            CmbTipoSolicitud.Items.Add("ACYS", "2");
                            CmbTipoSolicitud.Items.Add("Remisión", "3");
                            CmbTipoSolicitud.Items.Add("Pedido", "4");
                            CmbTipoSolicitud.Items.Add("Factura", "5");
                            CmbTipoSolicitud.Items.Add("GPMa", "6");
                            CmbTipoSolicitud.Value = "-1";
                            CargarRik();


                        }
                    }

                }
            }
            catch (Exception ex)
            {
                warning(ex + " " + new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }


        }
        private void inicializar()
        {
            ValidarSesion();
            //CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();

            //CN_Comun.DevLlenaCombo(2, sesion.Id_Emp, sesion.Id_U, Emp_CnxCentral, "SP_CatCDI_combo2", ref CmbSucursal);
            //CMBSucursalRepresentante.Items.AddRange(CmbSucursal.Items.Cast<Object>().ToArray());

            CargarDatos();

        }

        protected void btnBuscarInformacion_ServerClick(object sender, EventArgs e)
        {
            ValidarSesion();
            CargarDatos();
        }

        protected void btnDescargarInformacion_ServerClick(object sender, EventArgs e)
        {

            if (Session["listaSolicitudes" + Session.SessionID] != null)
            {
                gridBuscar.DataSource = Session["listaSolicitudes" + Session.SessionID];
                gridBuscar.DataBind();

                GridViewExporter1.WriteXlsToResponse(new XlsExportOptionsEx { ExportType = ExportType.WYSIWYG });

            }
            else
            {
                mensaje("Sin información de solicitudes pendientes");
            }

        }


        protected void btnAutorizarTodo(object sender, EventArgs e)
        {

            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            List<object> selectedValues;

            string motivoautorizar = HF_MotivoJustificacion.Value.ToString();
            // string combo = cmbAutorizacion.Value.ToString();



            List<string> fieldNames = new List<string>();
            foreach (GridViewColumn column in gridBuscar.Columns)
                if (column is GridViewDataColumn)
                    fieldNames.Add(((GridViewDataColumn)column).FieldName);
            selectedValues = gridBuscar.GetSelectedFieldValues(fieldNames.ToArray());

            string resultado = "";

            for (int i = 0; i < gridBuscar.VisibleRowCount; i++)
            {
                ASPxCheckBox chk = gridBuscar.FindRowCellTemplateControl(i, null, "checkValue") as ASPxCheckBox;
                if (chk != null)
                {
                    if (chk.Checked)
                    {

                        int id_folioautorizacion = Convert.ToInt32(gridBuscar.GetRowValues(i, "IdAutorizacionPrecio").ToString());
                        double precio_vta = Convert.ToDouble(gridBuscar.GetRowValues(i, "Precio_Vta").ToString());
                        int req_aut = Convert.ToInt32(gridBuscar.GetRowValues(i, "Req_Aut_Director").ToString());
                        int id_Cd = Convert.ToInt32(gridBuscar.GetRowValues(i, "Id_Cd").ToString());
                        int usuarioautoriza = Sesion.Id_U;
                        DateTime fechav = new DateTime();
                        fechav = DateTime.Now.AddYears(1);
                        fechav = Convert.ToDateTime(gridBuscar.GetRowValues(i, "FechaVigencia").ToString());


                        resultado = AutorizarSolicitud(id_folioautorizacion, usuarioautoriza, motivoautorizar, precio_vta, fechav, id_Cd, req_aut);

                    }
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "CloseWindowmensaje('" + resultado + "');", true);


        }


        public string AutorizarSolicitud(int folio, int usuarioautoriza, string justificacion, double precio, DateTime fechav, int id_cd, int reqaut)
        {
            try
            {


                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];

                int Verificador = 0;


                CN_AlertaAutorizacion cn_alerta = new CN_AlertaAutorizacion();

                AlertaAutorizacion alerta = new AlertaAutorizacion();


                if (folio != 0)
                {
                    alerta.IdAutorizacionPrecio = Convert.ToInt32(folio);
                    alerta.IdUsuarioGteAutorizo = usuarioautoriza;
                    alerta.Justificacion = justificacion;
                    alerta.Estatus = 2;
                    alerta.FechaVigencia = fechav;
                    alerta.Id_Cd = id_cd;
                    alerta.Precio_Vta = precio;
                    alerta.Req_Aut_Director = reqaut;
                    cn_alerta.AutorizarSolicitudGerente(alerta, ref Verificador, Conexion);

                }


                string correousuario = "";
                string nombrecliente = "";
                string prd_Descripcion = "";
                string correodireccion = "";
                Double precio_MinimoRik = 0;
                Double precioObjetivo = 0;
                Int64 id_Prd = 0;
                int req_aut = 0;


                cn_alerta.ConsultaAlertaCorreo(alerta, ref correousuario, ref correodireccion, ref nombrecliente, ref prd_Descripcion, ref precio_MinimoRik, ref precioObjetivo, ref id_Prd, ref req_aut, Conexion);

                alerta.Req_Aut_Director = req_aut;

                if (correousuario != "")
                {
                    alerta.Cte_NomComercial = nombrecliente;
                    alerta.Id_Prd = id_Prd;
                    alerta.Prd_Descripcion = prd_Descripcion;
                    alerta.Precio_MinimoRik = precio_MinimoRik;
                    alerta.PrecioObjetivo = precioObjetivo;
                    alerta.Id_Emp = 1;
                    if (correodireccion != "" && alerta.Req_Aut_Director == 1)
                    {
                        correousuario = correousuario + ";" + correodireccion;
                    }

                    var mc = new CapAlertaAutorizacionEditar();

                    string url = mc.session.URL + "/";

                    EnviarCorreo(alerta, correousuario, 1, url);
                }

                return "Se Autorizo el folio correctamente.";

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        protected void btnRechazarTodo(object sender, EventArgs e)
        {

            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            List<object> selectedValues;
 
            string motivoautorizar = HF_MotivoJustificacion.Value.ToString(); //Justificarechazo
            string idmotivorechazo = HF_IdMotivoRechazo.Value.ToString();
            string motivorechazo = HF_MotivoRechazo.Value.ToString();


            /*var Facturass = gridBuscar.GetSelectedFieldValues("GridBuscar_DXSelBtn")*/
            //motivoautorizar = cmbAutorizacion.SelectedItem.Text.ToString() + "  " +  motivoautorizar;
            //string  valor = cmbAutorizacion.Value.ToString();

            //List<string> fieldNames = new List<string>();
            //foreach (GridViewColumn column in gridBuscar.Columns)
            //    if (column is GridViewDataColumn)
            //        fieldNames.Add(((GridViewDataColumn)column).FieldName);
            //selectedValues = gridBuscar.GetSelectedFieldValues(fieldNames.ToArray());

            string resultado = "";
            if (gridBuscar.VisibleRowCount > 0)
            {
                for (int i = 0; i < gridBuscar.VisibleRowCount; i++)
                {
                    ASPxCheckBox chk = gridBuscar.FindRowCellTemplateControl(i, null, "checkValue") as ASPxCheckBox;
                    if (chk != null)
                    {
                        if (chk.Checked)
                        {

                            int id_folioautorizacion = Convert.ToInt32(gridBuscar.GetRowValues(i, "IdAutorizacionPrecio").ToString());
                            double precio_vta = Convert.ToDouble(gridBuscar.GetRowValues(i, "Precio_Vta").ToString());
                            int req_aut = Convert.ToInt32(gridBuscar.GetRowValues(i, "Req_Aut_Director").ToString()); 
                            int id_Cd = Convert.ToInt32(gridBuscar.GetRowValues(i, "Id_Cd").ToString());
                            int usuarioautoriza = Sesion.Id_U;
                            DateTime fechav = new DateTime();
                            fechav = DateTime.Now.AddYears(1);

                            resultado = RechazarSolicitud(id_folioautorizacion, usuarioautoriza, motivoautorizar, idmotivorechazo, motivorechazo, precio_vta, fechav, id_Cd);

                        }
                    }
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "CloseWindowmensaje('" + resultado + "');", true);


        }


        public string RechazarSolicitud(int folio, int usuarioautoriza, string justificacion, string idmotivorechazo, string motivorechazo, double precio_vta, DateTime fechav, int id_cd)
        {
            try
            {
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
                string WebURLtempPDF = string.Concat(System.Configuration.ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), "");

                int Verificador = 0;
                CN_AlertaAutorizacion cn_leads = new CN_AlertaAutorizacion();
                AlertaAutorizacion alerta = new AlertaAutorizacion();
                //conv.PC_NoConvenioKey = this.HFTipoOp.Value == "0" ? int.Parse(this.TxtKeyConvenio.Text) : 0;

                //conv.PC_FechaInicio = this.TxtPC_FechaInicioConv.SelectedDate.Value != null ? this.TxtPC_FechaInicioConv.SelectedDate.Value : DateTime.Now;
 

                if (folio != 0)
                {
                    alerta.IdAutorizacionPrecio = Convert.ToInt32(folio);
                    alerta.IdUsuarioGteAutorizo = usuarioautoriza;
                    alerta.Justificacion = justificacion;
                    alerta.Estatus = 4;
                    alerta.Id_Emp = 1;
                    alerta.Id_Cd = id_cd;
                    //rechazar enviar idmotivo y motivo
                    alerta.Id_MotivoRechazo = Convert.ToInt32(idmotivorechazo);
                    alerta.MotivoRechazo = motivorechazo;
                    //cancelar solicitud
                    cn_leads.AutorizarSolicitudGerente(alerta, ref Verificador, Conexion);
                }

                string correousuario = "";
                string nombrecliente = "";
                string prd_Descripcion = "";
                string coreodireccion = "";
                Double precio_MinimoRik = 0;
                Double precioObjetivo = 0;
                Int64 id_Prd = 0;
                int req_aut = 0;
                //este tipo de autorización es para que envie correo al gerente 
                //alerta.TipoAutorizacion = 4;

                cn_leads.ConsultaAlertaCorreo(alerta, ref correousuario, ref coreodireccion, ref nombrecliente, ref prd_Descripcion, ref precio_MinimoRik, ref precioObjetivo, ref id_Prd, ref req_aut, Conexion);

                alerta.Req_Aut_Director = req_aut;

                if (correousuario != "")
                {
                    alerta.Cte_NomComercial = nombrecliente;
                    alerta.Id_Prd = id_Prd;
                    alerta.Prd_Descripcion = prd_Descripcion;
                    alerta.Precio_MinimoRik = precio_MinimoRik;
                    alerta.PrecioObjetivo = precioObjetivo;
                    alerta.Id_Emp = 1;

                    var mc = new CapAlertaAutorizacionEditar();

                    string url = (HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath + "/").Replace("//", "/").Replace("http:/", "http://");


                    EnviarCorreo(alerta, correousuario, 0, url);
                }

                if (coreodireccion != "" && alerta.Req_Aut_Director == 1)
                {
                    //EnviarCorreoDireccion();
                }


                return "Se Rechazo el folio # " + folio;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public static string EnviarCorreo(AlertaAutorizacion alertaautorizacion, string correo, int autoriza, string url)
        {
            try
            {
                //obtener la conección de sianweb para poder obtener la configuración 
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnection"];



                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Cd = alertaautorizacion.Id_Cd;
                configuracion.Id_Emp = alertaautorizacion.Id_Emp;
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                cn_configuracion.Consulta(ref configuracion, Conexion);
                StringBuilder cuerpo_correo = new StringBuilder();
                cuerpo_correo.Append("<html><div align='center'>");
                cuerpo_correo.Append("<table><tr><td>");
                cuerpo_correo.Append("<IMG SRC=\"cid:companylogo\" ALIGN='left'></td>");
                cuerpo_correo.Append("<td></td>");
                cuerpo_correo.Append("</tr><tr><td colspan='2'><br><br><br></td>");
                cuerpo_correo.Append("</tr><tr>");
                cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='2'>");

                if (alertaautorizacion.Req_Aut_Director == 1)
                {
                    cuerpo_correo.Append("Solicitud de precios especiales # " + alertaautorizacion.IdAutorizacionPrecio.ToString());
                    if (autoriza == 1)
                    {
                        cuerpo_correo.Append(" Ha sido Auorizada por el Gerente</td></tr><tr><td> Y requiere ser autorizada por Dirección.</td> </tr>");
                    }
                    else
                    {
                        cuerpo_correo.Append(" Ha sido Rechazada por el Gerente</td></tr><tr><td> Motivo de Rechazo: " + alertaautorizacion.Justificacion + "</td> </tr>");
                    }
                }
                else
                {
                    cuerpo_correo.Append("Su Solicitud de precios especiales # " + alertaautorizacion.IdAutorizacionPrecio.ToString());
                    if (autoriza == 1)
                    {
                        cuerpo_correo.Append(" Ha sido Auorizada por el Gerente</td></tr><tr><td> &nbsp;</td> </tr>");
                    }
                    else
                    {
                        cuerpo_correo.Append(" Ha sido Rechazada por el Gerente</td></tr><tr><td> Motivo de Rechazo: " + alertaautorizacion.Justificacion + "</td> </tr>");
                    }
                }


                cuerpo_correo.Append("<tr>");
                cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='2'>");

                cuerpo_correo.Append("</td></tr> <tr><td> &nbsp;</td> </tr>");
                cuerpo_correo.Append("<tr>");
                cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='2'>");

                cuerpo_correo.Append("</td></tr> <tr><td> &nbsp;</td> </tr>");
                cuerpo_correo.Append("<tr>");
                cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='2'>");
                //cuerpo_correo.Append("Autorizada por : " + session.U_Nombre);
                cuerpo_correo.Append("</td></tr>");
                cuerpo_correo.Append("<tr><td colspan='2'>");
                cuerpo_correo.Append("<center><br>");
                cuerpo_correo.Append("</td></tr></table></div>");

                cuerpo_correo.Append("<div align='center'>");
                cuerpo_correo.Append("<table><tr>");
                cuerpo_correo.Append("<td>Producto</td><td>Descripción</td><td>Precio Vta<br>Autorizado</td><td>Precio<br>Objetivo</td><td>Precio <br>Min rik</td>");
                cuerpo_correo.Append("</tr><tr><td>" + alertaautorizacion.Id_Prd.ToString() + "</td><td>" + alertaautorizacion.Prd_Descripcion.ToString() + "</td>");
                cuerpo_correo.Append("<td> " + String.Format("{0:###,###,##0.00}", alertaautorizacion.Precio_Vta) + " </td > <td> " + String.Format("{0:###,###,##0.00}", alertaautorizacion.PrecioObjetivo) + " </td><td> " + String.Format("{0:###,###,##0.00}", alertaautorizacion.Precio_MinimoRik) + " </td> ");
                cuerpo_correo.Append("</tr><tr><td colspan='2'><br><br><br></td>");
                cuerpo_correo.Append("</tr><tr>");
                cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='2'>");
                if (alertaautorizacion.Req_Aut_Director == 1)
                {
                    cuerpo_correo.Append("Favor de dar seguimiento a esta solictud del cliente: " + alertaautorizacion.Cte_NomComercial);
                }
                else
                {
                    cuerpo_correo.Append("Favor de dar seguimiento al documento que tenga pendiente de este producto con el cliente: " + alertaautorizacion.Cte_NomComercial);
                }
                cuerpo_correo.Append("</td></tr>");





                cuerpo_correo.Append("<tr><td><br><center>");
                cuerpo_correo.Append("<a href='" + url + "CapAlertaConsulta.aspx'" + ">");
                cuerpo_correo.Append("Consulta solicitudes de autorización de precios</a></font></center>");
                cuerpo_correo.Append("</td></tr></table></div>");
                cuerpo_correo.Append(" </html>");


                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new System.Net.NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                sm.EnableSsl = true;
                MailMessage m = new MailMessage();
                m.From = new MailAddress(configuracion.Mail_Remitente);
                char[] splitchar = { ';' };
                //correo = "francisco.cepeda@Gibraltar.com.mx";
                string[] Correos = correo.Split(splitchar);


                //m.Bcc.Add(new MailAddress("francisco.cepeda@gibraltar.com.mx"));

                foreach (string mail in Correos)
                {

                    m.To.Add(new MailAddress(mail));
                }

                if (alertaautorizacion.Req_Aut_Director == 1)
                {
                    if (autoriza == 1)
                    {
                        m.Subject = "Solicitud de aplicación de precios especiales # " + alertaautorizacion.IdAutorizacionPrecio.ToString() + " , pendiente de autorizar";
                    }
                    else
                    {
                        m.Subject = "Solicitud de aplicación de precios especiales # " + alertaautorizacion.IdAutorizacionPrecio.ToString() + " , ha sido Rechazada";
                    }

                }
                else
                {
                    if (autoriza == 1)
                    {
                        m.Subject = "Solicitud de aplicación de precios especiales # " + alertaautorizacion.IdAutorizacionPrecio.ToString() + " , ha sido Autorizada";
                    }
                    else
                    {
                        m.Subject = "Solicitud de aplicación de precios especiales # " + alertaautorizacion.IdAutorizacionPrecio.ToString() + " , ha sido Rechazada";
                    }
                }
                m.IsBodyHtml = true;
                string body = cuerpo_correo.ToString();
                AlternateView vistaHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                //Esto queda dentro de un try por si llegan a cambiar la imagen el correo como quiera se mande
                ////try
                ////{
                ////    LinkedResource logo = new LinkedResource(MapPath(@"Imagenes/logo.jpg"), MediaTypeNames.Image.Jpeg)
                ////    {
                ////        ContentId = "companylogo"
                ////    };
                ////    vistaHtml.LinkedResources.Add(logo);
                ////}
                ////catch (Exception)
                ////{
                ////}

                m.AlternateViews.Add(vistaHtml);
                try
                {
                    sm.Send(m);

                }
                catch (Exception)
                {

                    return "Error al enviar el correo. Favor de revisar la configuración del sistema";

                }

                return "Se envío el correo de forma correcta ";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }






        protected void ASPxCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            BootstrapGridView grid = sender as BootstrapGridView;

            List<object> selectedValues;

            //List<string> fieldNames = new List<string>();
            //foreach (GridViewColumn column in gridBuscar.Columns)
            //    if (column is GridViewDataColumn)
            //        fieldNames.Add(((GridViewDataColumn)column).FieldName);
            //selectedValues = gridBuscar.GetSelectedFieldValues(fieldNames.ToArray());


            ASPxCheckBox checkBox = sender as ASPxCheckBox;

            //this.ASPxListBox3.Items.Clear();

            //for (int i = 0; i < gridBuscar.VisibleRowCount; i++)
            //{
            //    ASPxCheckBox chk = gridBuscar.FindRowCellTemplateControl(i, null, "checkValue") as ASPxCheckBox;
            //    if (chk.Checked)
            //    {
            //        //if (i == 0)
            //        //{
            //            ASPxListBox3.Items.Add( gridBuscar.GetRowValues(i, "IdAutorizacionPrecio").ToString());
            //        //}
            //    }
            //}


        }

        [DefaultValue(false)]
        public bool AlwaysShowCheckboxesInCheckColumns { get; set; }
        [DefaultValue(false)]
        public bool ProcessSelectionChangedOnServer { get; set; }



        /// <summary>
        /// Funcion que valida la sesion
        /// </summary>
        private void ValidarSesion()
        {
            try
            {
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                    return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
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


        public void CargarDatos()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            Session["listaSolicitudes" + Session.SessionID] = null;

            try
            {
                List<AlertaAutorizacion> listaSolicitudes = new List<AlertaAutorizacion>();
                AlertaAutorizacion CapAlerta = new AlertaAutorizacion();



                string FechaInicialD = "01/01/2000";
                string FechaFinalD = "01/01/2100";


                CapAlerta.Id_Emp = sesion.Id_Emp;
                CapAlerta.Id_Cd = sesion.Id_Cd_Ver;
                DateTime FechaInicial2 = DateTime.Parse(FechaInicialD);
                DateTime FechaFinal2 = DateTime.Parse(FechaFinalD).AddMonths(1).AddDays(-1);
                CapAlerta.FechaInicial = FechaInicial2;
                CapAlerta.FechaFinal = FechaFinal2;
                CapAlerta.IdRepresentante = -1; // Convert.ToInt32(cmbBuscarRepresentante.Value.ToString());
                CapAlerta.IdUsuarioGteAutorizo = -1; //Convert.ToInt32(cmbBuscarRepresentante.Value.ToString());
                if (CMBSucursalRepresentante.Value == null)
                {
                    CapAlerta.IdRepresentante = -1;
                }
                else
                {
                    CapAlerta.IdRepresentante = Convert.ToInt32(CMBSucursalRepresentante.Value.ToString());
                }

                if (CmbTipoSolicitud.Value == null)
                {
                    CapAlerta.TipoAutorizacion = -1;
                }
                else
                {
                    CapAlerta.TipoAutorizacion = Convert.ToInt32(CmbTipoSolicitud.Value.ToString());
                }
                CapAlerta.Estatus = 1; // solicitado


                if (txtProducto.Text.ToString() != "")
                {
                    CapAlerta.Id_Prd = int.Parse(txtProducto.Value.ToString());
                    //CapAlerta.Id_Cte = int.Parse(txtIdCte.Value.ToString());
                }
                else
                {
                    CapAlerta.Id_Prd = -1;
                }
                if (txtIdCte.Text.ToString() != "")
                {
                    CapAlerta.Id_Cte = int.Parse(txtIdCte.Value.ToString());
                }
                else
                {
                    CapAlerta.Id_Cte = -1;
                }

                CN_AlertaAutorizacion cn_leads = new CN_AlertaAutorizacion();
                cn_leads.ConsultaAlertaAutorizacionLista(CapAlerta, ref listaSolicitudes, "PAUTGTE", Emp_CnxCentral);

                //listaSolicitudesquery = listaSolicitudesquery.OrderBy(x => x.Mes).ToList();

                Session["listaSolicitudes" + Session.SessionID] = listaSolicitudes;
                gridBuscar.DataSource = listaSolicitudes;
                gridBuscar.DataBind();
                //UPdBusacarinfo.Update();
                //UpdatePanel6.Update();

            }
            catch (Exception ex)
            {
                mensaje("Error al obtener los datos:  " + ex.Message);
            }
        }


        //[System.Web.Services.WebMethod(EnableSession = true)]
        //public static string AutorizarFolio(string parametro, string justificacion)
        //{
        //    string test = HttpContext.Current.Session.SessionID;

        //    HttpContext.Current.Session["idusuario"] = "61";
        //    String usuario = (String)HttpContext.Current.Session["idusuario"];
        //    int usuarioautoriza = Convert.ToInt32(usuario);
             
        //    String resultado = AutorizarSolicitud(parametro, usuarioautoriza, justificacion);
        //    return resultado;
        //}



        public static string AutorizarSolicitud(string folio, int usuarioautoriza, string justificacion, double precio, DateTime fechav, int id_cd, int reqaut)
        {
            try
            {


                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];

                int Verificador = 0;


                CN_AlertaAutorizacion cn_leads = new CN_AlertaAutorizacion();

                AlertaAutorizacion alerta = new AlertaAutorizacion();

                //conv.PC_NoConvenioKey = this.HFTipoOp.Value == "0" ? int.Parse(this.TxtKeyConvenio.Text) : 0;

                //conv.PC_FechaInicio = this.TxtPC_FechaInicioConv.SelectedDate.Value != null ? this.TxtPC_FechaInicioConv.SelectedDate.Value : DateTime.Now;




                if (folio != "0")
                {
                    alerta.IdAutorizacionPrecio = Convert.ToInt32(folio);
                    alerta.IdUsuarioGteAutorizo = usuarioautoriza;
                    alerta.Justificacion = justificacion;
                    alerta.Estatus = 2;
                    alerta.FechaVigencia = fechav;
                    alerta.Id_Cd = id_cd;
                    alerta.Precio_Vta = precio;
                    alerta.Req_Aut_Director = reqaut;
                    cn_leads.AutorizarSolicitudGerente(alerta, ref Verificador, Conexion);


                    string correousuario = "";
                    string nombrecliente = "";
                    string prd_Descripcion = "";
                    string correodireccion = "";
                    Double precio_MinimoRik = 0;
                    Double precioObjetivo = 0;
                    Int64 id_Prd = 0;
                    int req_aut = 0;


                    cn_leads.ConsultaAlertaCorreo(alerta, ref correousuario, ref correodireccion, ref nombrecliente, ref prd_Descripcion, ref precio_MinimoRik, ref precioObjetivo, ref id_Prd, ref req_aut, Conexion);

                    alerta.Req_Aut_Director = req_aut;

                    if (correousuario != "")
                    {
                        alerta.Cte_NomComercial = nombrecliente;
                        alerta.Id_Prd = id_Prd;
                        alerta.Prd_Descripcion = prd_Descripcion;
                        alerta.Precio_MinimoRik = precio_MinimoRik;
                        alerta.PrecioObjetivo = precioObjetivo;
                        alerta.Id_Emp = 1;
                        if (correodireccion != "" && alerta.Req_Aut_Director == 1)
                        {
                            correousuario = correousuario + ";" + correodireccion;
                        }

                        var mc = new CapAlertaAutorizacionEditar();

                        string url = (HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath + "/").Replace("//", "/").Replace("http:/", "http://");

                        EnviarCorreo(alerta, correousuario, 1, url);
                    }
                    //Alerta("La información ha sido guardada.");

                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "Regresar", "Mensajecerrarpantalla('La información ha sido guardada')", true);

                    //
                }
                return "Se Autorizo el folio correctamente.";

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters == "Delete")
            {
                ///REVISAR TOD JFCV 3 mayo
                //Session["listaSolicitudes" + Session.SessionID] = listaSolicitudes;
                //gridBuscar.DataSource = listaSolicitudes;
                //gridBuscar.DataBind();

                //table = GetTable();
                //List<Object> selectItems = grid.GetSelectedFieldValues("ID");
                //foreach (object selectItemId in selectItems)
                //{
                //    table.Rows.Remove(table.Rows.Find(selectItemId));
                //}
                //grid.DataBind();
                //grid.Selection.UnselectAll();
            }
        }

        protected void gridView_CustomCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (e.ButtonID != "Acept") return;
            //copiedValues = new Hashtable();
            //foreach (string fieldName in copiedFields)
            //    copiedValues[fieldName] = grid.GetRowValues(e.VisibleIndex, fieldName);
            //grid.AddNewRow();
            if (e.ButtonID != "Cancel") return;

            //if (e.Parameters == "Delete")
            //{
            //    table = GetTable();
            //    List<Object> selectItems = grid.GetSelectedFieldValues("ID");
            //    foreach (object selectItemId in selectItems)
            //    {
            //        table.Rows.Remove(table.Rows.Find(selectItemId));
            //    }
            //    grid.DataBind();
            //    grid.Selection.UnselectAll();
            //}


        }


        protected void gridBuscar_CustomButtonInitialize(object sender, DevExpress.Web.Bootstrap.BootstrapGridViewCustomButtonEventArgs e)
        {
            var grid = sender as DevExpress.Web.Bootstrap.BootstrapGridView;
            if (e.VisibleIndex < 0) return; // Evita errores en filas de encabezado, etc.

            string tipo = grid.GetRowValues(e.VisibleIndex, "Tipo") as string;

            if (e.ButtonID == "ShowEditWindow")
            {
                e.Visible = string.Equals(tipo, "GPMA", StringComparison.OrdinalIgnoreCase) ? DefaultBoolean.False : DefaultBoolean.True;
            }
            else if (e.ButtonID == "ShowEditWindowGPMA")
            {
                e.Visible = string.Equals(tipo, "GPMA", StringComparison.OrdinalIgnoreCase) ? DefaultBoolean.True : DefaultBoolean.False;
            }
        }

        private void CargarRik()
        {
            try
            {

                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();

                CMBSucursalRepresentante.Items.Clear();
                CN_Comun.DevLlenaCombo(2, sesion.Id_Emp, sesion.Id_Cd, sesion.Emp_Cnx, "spCatRik_Combo_Todos", ref CMBSucursalRepresentante);

                CMBSucursalRepresentante.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //protected void grid_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        //{
        //    if (e.ButtonID != "Acept") return;
        //    //copiedValues = new Hashtable();
        //    //foreach (string fieldName in copiedFields)
        //    //    copiedValues[fieldName] = grid.GetRowValues(e.VisibleIndex, fieldName);
        //    //grid.AddNewRow();
        //    if (e.ButtonID != "Cancel") return;

        //}

        #region Mensajes


        private void warning(string mensaje)
        {
            ShowMessage(mensaje, "Warning");
        }
        private void ShowMessage(string Message, string type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }

        /// <summary>
        /// Abre el modal de mensaje
        /// </summary>
        /// <param name="mensaje"></param>
        private void mensaje(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "modalMensaje('" + mensaje + "')", true);
        }

        /// <summary>
        /// Abre el modal de mensaje si se requiere con pregunta
        /// </summary>
        /// <param name="mensaje"></param>
        private void mensajeDecision(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modalQuestion", "modalQuestion('" + mensaje + "')", true);
        }

        protected void WCompararRepresentantes_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "comparar", "comparar()", false);
            //UPdBusacarinfo.Update();
        }



        #endregion


        protected void btnCoreccion(object sender, EventArgs e)
        {
            if (Session["Respuesta" + Session.SessionID] != null)
            {
                if (Convert.ToBoolean(Session["Respuesta" + Session.SessionID]))
                {
                    //Guardar();
                }
            }

        }


    }
}