using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDatos;
using CapaNegocios;
using Telerik.Web.UI;
using System.Configuration;
using CapaEntidad;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using System.Text;
using DevExpress.Web;
using DevExpress.Web.Bootstrap;

namespace SIANWEB
{
    public partial class CargaSolicitudOC : System.Web.UI.Page
    {

        #region Variables

        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        private Sesion sesion { get { return (Sesion)Session["Sesion" + Session.SessionID]; } set { Session["Sesion" + Session.SessionID] = value; } }



        private string Emp_CnxCentral
        {
            get { return ConfigurationManager.AppSettings.Get("strConnectionCentral"); }
        }
        #endregion

        #region CargaInicial

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["ProdSOli"] != null)
            {

                List<PedidoVtaInst> dtTemp = (List<PedidoVtaInst>)Session["ProdSOli"];

                gridviewOrderCompra.DataSource = dtTemp;
                gridviewOrderCompra.DataBind();
            }

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
                    if (!Page.IsPostBack)
                    {
                        Session["ProdSOli"] = null;
                        Inicializar();
                    }
                }
            }
            catch (Exception ex)
            {
                warning(ex + " " + new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        #endregion 

        #region Funciones

        private void Inicializar()
        {
            try
            {

                Sesion sesion = (Sesion)this.Session["Sesion" + this.Session.SessionID];
                CargarDatos();
                CargarEstatus();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void CargarDatos()
        {
            try
            {
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.DevLlenaCombo(2, sesion.Id_Emp, sesion.Id_U, Emp_CnxCentral, "SP_CatCDI_combo2", ref CMBSucursal);

                CMBSucursal.Value = sesion.Id_Cd_Ver.ToString();
                CMBSucursal.ReadOnly = true;

                PedidoVtaInst Ped = new PedidoVtaInst();
                List<PedidoVtaInst> Solicitud = new List<PedidoVtaInst>();
                CN_CapPedidoVtaInst CN = new CN_CapPedidoVtaInst();

                CMBFechaInicial.Value = sesion.CalendarioIni;
                CMBFechaFinal.Value = sesion.CalendarioFin;

                Ped.FechaInicial = sesion.CalendarioIni;
                Ped.FechaFinal = sesion.CalendarioFin;
                Ped.Estatus = "P";

                CN.ConsultaSolicitud(Ped, sesion.Emp_Cnx, ref Solicitud);

                Session["ProdSOli"] = Solicitud;
                gridviewOrderCompra.DataSource = Solicitud;
                gridviewOrderCompra.DataBind();
            }
            catch (Exception ex)
            {
                warning(ex.Message.ToString());
            }
        }

        private void CargarEstatus()
        {
            try
            {
                this.CMBEstatus.Items.Add("--Todos--", (object)"T");
                this.CMBEstatus.Items.Add("Pendiente", (object)"P");
                this.CMBEstatus.Items.Add("Autorizado", (object)"A");
                this.CMBEstatus.Items.Add("Rechazado", (object)"R");
                this.CMBEstatus.Value = (object)"T";
            }
            catch (Exception ex)
            {
                warning(ex.Message.ToString());
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrid();
        }

        public void CargarGrid()
        {
            PedidoVtaInst Ped = new PedidoVtaInst();
            List<PedidoVtaInst> Solicitud = new List<PedidoVtaInst>();
            CN_CapPedidoVtaInst CN = new CN_CapPedidoVtaInst();

            Ped.FechaInicial = Convert.ToDateTime(CMBFechaInicial.Value);
            Ped.FechaFinal = Convert.ToDateTime(CMBFechaFinal.Value);
            Ped.Estatus = CMBEstatus.SelectedItem.Value.ToString(); ;

            CN.ConsultaSolicitud(Ped, sesion.Emp_Cnx, ref Solicitud);

            Session["ProdSOli"] = Solicitud;
            gridviewOrderCompra.DataSource = Solicitud;
            gridviewOrderCompra.DataBind();
        }

        protected void BtnRechazar_ServerClick(object sender, EventArgs e)
        {
            try
            {
                GridViewDataItemTemplateContainer c = ((System.Web.UI.HtmlControls.HtmlButton)sender).NamingContainer as GridViewDataItemTemplateContainer;
                CN_CapPedidoVtaInst CN = new CN_CapPedidoVtaInst();

                int verificador = 0;
                string id_Sol = c.Grid.GetRowValues(c.VisibleIndex, "id_Sol").ToString().Trim();
                string Estatus = c.Grid.GetRowValues(c.VisibleIndex, "estatusSTR").ToString().Trim();


                if (Estatus != "Pendiente")
                {
                    info("No se puede rechazar, la solicitud esta en un estatus diferente a Pendiente");
                    return;
                }
                PedidoVtaInst pedido = new PedidoVtaInst();
                pedido.id_Sol = Convert.ToInt32(id_Sol);
                pedido.Estatus = "R";

                CN.ActualizarSolicitud(pedido, sesion.Emp_Cnx, ref verificador);
                if (verificador == 0)
                {
                    CargarGrid();

                    string mensage = "";
                    Sesion session = new Sesion();
                    session = (Sesion)Session["Sesion" + Session.SessionID];
                    EnviarCorreoRechazado(session.Emp_Cnx, ref mensage);

                    sucess("Se rechazo la solicitud con el folio:" + id_Sol);
                }
            }
            catch (Exception ex)
            {
                warning("Error al monento de rechazar la solicitud");
            }
        }

        protected void btnAcceptar_ServerClick(object sender, EventArgs e)
        {
            try
            {
                GridViewDataItemTemplateContainer c = ((System.Web.UI.HtmlControls.HtmlButton)sender).NamingContainer as GridViewDataItemTemplateContainer;
                CN_CapPedidoVtaInst CN = new CN_CapPedidoVtaInst();

                int verificador = 0;
                string id_Sol = c.Grid.GetRowValues(c.VisibleIndex, "id_Sol").ToString().Trim();
                string Estatus = c.Grid.GetRowValues(c.VisibleIndex, "estatusSTR").ToString().Trim();

                if (Estatus != "Pendiente")
                {
                    info("No se puede autorizar, La solicitud esta en un estatus diferente a Pendiente");
                    return;
                }

                PedidoVtaInst pedido = new PedidoVtaInst();
                pedido.id_Sol = Convert.ToInt32(id_Sol);
                pedido.Estatus = "A";

                CN.ActualizarSolicitud(pedido, sesion.Emp_Cnx, ref verificador);

                if (verificador == 0)
                {
                    CargarGrid();

                    string mensage = "";
                    Sesion session = new Sesion();
                    session = (Sesion)Session["Sesion" + Session.SessionID];
                    EnviarCorreoAceptado(session.Emp_Cnx, ref mensage);

                    sucess("Se autorizo la solicitud con el folio:" + id_Sol);
                }
            }
            catch (Exception ex)
            {
                warning("Error al monento de rechazar la solicitud");
            }
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

        protected void gridpedidoVIProductoOrdenCompra_BeforePerformDataSelect(object sender, EventArgs e)
        {
            var prod = (sender as BootstrapGridView).GetMasterRowKeyValue();

            if (Session["campos"] == null)
            {

                string[] array = prod.ToString().Trim().Split('|');
                // array[0] = valor del campo id_sol 
                // array[1] = valor del campo id_cte

                if (Session["campos"] == null)
                {
                    string id_sol = array[0].ToString().Trim();
                    string id_cte = array[1].ToString().Trim();

                    BootstrapGridView grid = sender as BootstrapGridView;

                    PedidoDet Ped = new PedidoDet();
                    List<PedidoDet> Solicitud = new List<PedidoDet>();
                    CN_CapPedidoVtaInst CN = new CN_CapPedidoVtaInst();

                    Ped.id_sol = Convert.ToInt32(id_sol);

                    CN.ConsultaSolicitudDet(Ped, sesion.Emp_Cnx, ref Solicitud);


                    string ConexionCentral = ConfigurationManager.AppSettings["strConnectionCentral"].ToString().Trim();
                    ConvenioDet conv;
                    ConvenioDet convdet;
                    CN_Convenio cn_conv;
                    List<string> Productos = new List<string>();

                    List<ConvenioDet> lconveniosdet = new List<ConvenioDet>();
                    ConvenioDet lconvdet = new ConvenioDet();


                    if (Solicitud.Count > 0)
                    {
                        foreach (PedidoDet detalle in Solicitud)
                        {

                            conv = new ConvenioDet();
                            convdet = new ConvenioDet();
                            cn_conv = new CN_Convenio();
                            conv.Id_Emp = sesion.Id_Emp;
                            conv.Id_Cd = sesion.Id_Cd_Ver;
                            conv.Id_Cte = Convert.ToInt32(id_cte != "" ? id_cte : "-1");
                            conv.Id_Prd = Convert.ToInt64(detalle.Id_Prd);
                            double PrecioIngresado = Convert.ToDouble(detalle.Importe);

                            if (Convert.ToInt64(detalle.Id_Prd) <= 999999999999)
                            {
                                cn_conv.Convenio_ConsultaPrecio(conv, ref convdet, ConexionCentral);

                                if (convdet != null && convdet.PCD_PrecioAAAEsp > 0)
                                {
                                    detalle.AAA = Convert.ToDouble(convdet.PCD_PrecioAAAEsp.ToString());
                                }
                                else
                                {
                                    Producto producto = new Producto();
                                    //obtener datos de producto
                                    CN_CatProducto clsProducto = new CN_CatProducto();
                                    producto.Id_Cte = Convert.ToInt32(Convert.ToInt32(id_cte != "" ? id_cte : "-1"));
                                    int productoNuevo = 0;
                                    clsProducto.ConsultaProductos(ref producto, sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd_Ver, Convert.ToInt64(detalle.Id_Prd), ref productoNuevo, 0);

                                    if (0 < producto.Prd_AAA)
                                    {
                                        detalle.AAA = Convert.ToDouble(producto.Prd_AAA.ToString());
                                    }
                                }
                            }
                        }
                    }

                    List<PedidoDet> Solicitud2 = (from tlist in Solicitud
                                                  where tlist.AAA > tlist.Importe
                                                  select tlist).ToList();


                    Session["campos"] = Solicitud2;
                    grid.DataSource = Solicitud2;
                    grid.DataBind();
                }

            }
            else
            {
                Session["campos"] = null;
            }
        }


        #region Correos


        private void EnviarCorreoRechazado(string conexion, ref string mensage)
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Cd = session.Id_Cd_Ver;
                configuracion.Id_Emp = session.Id_Emp;
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                cn_configuracion.Consulta(ref configuracion, session.Emp_Cnx);
                StringBuilder cuerpo_correo = new StringBuilder();

                cuerpo_correo.Append("<div align='center'>");
                cuerpo_correo.Append("<table><tr><td>");
                cuerpo_correo.Append("<td></td>");
                cuerpo_correo.Append("</tr><tr><td colspan='2'><br><br><br></td>");
                cuerpo_correo.Append("</tr><tr>");
                cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='2'>");

                cuerpo_correo.Append("Buen día, Se les comunica por este medio. <br><br> ");

                cuerpo_correo.Append("Se ha RECHAZADO la siguiente solicitud de captura de OC Centralizada:  <br><br>");
                cuerpo_correo.Append("CDI: Monterrey <br>");
                cuerpo_correo.Append("Usuario: Prueba <br>");
                cuerpo_correo.Append("Cliente: VIPS <br>");
                cuerpo_correo.Append("OC Centralizada: Prueba <br><br>");


                cuerpo_correo.Append("Producto de venta original: <br>");
                cuerpo_correo.Append("Precio de venta: <br>");
                cuerpo_correo.Append("Producto de venta sustituto. <br>");
                cuerpo_correo.Append("Precio AAA: <br>");

                cuerpo_correo.Append("Cordinardor de la cuenta: Prueba <br>");
                cuerpo_correo.Append("Sistema automático de reporte de captación de pedido. <br>  Fecha de realización: " + DateTime.Now.ToString("dd/MM/yyyy") + "<br>");
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");
                cuerpo_correo.Append("<center><br>");
                cuerpo_correo.Append("</td></tr></table></div>");



                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                sm.EnableSsl = true;
                sm.UseDefaultCredentials = false;


                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11;

                MailMessage m = new MailMessage();
                m.From = new MailAddress(configuracion.Mail_Remitente, "solicitud autorizacion producto AAA orden de compra: Prueba");
                m.Subject = "Reporte ";

                //foreach (UsuarioCorreo correo in ListaCorreoUsuario)
                //{
                //    m.To.Add(new MailAddress(correo.U_Correo));
                //} 
                m.To.Add(new MailAddress("erikrgc@hotmail.com"));
                m.IsBodyHtml = true;

                string body = cuerpo_correo.ToString().Trim();
                AlternateView vistaHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);

                m.AlternateViews.Add(vistaHtml);
                try
                {
                    sm.Send(m);
                }
                catch (Exception ex)
                {
                    mensage = "Fallo en enviar el correo electronico";
                }

            }
            catch (Exception)
            {
                mensage = "Fallo en la configuración del correo";
            }
        }



        private void EnviarCorreoAceptado(string conexion, ref string mensage)
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Cd = session.Id_Cd_Ver;
                configuracion.Id_Emp = session.Id_Emp;
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                cn_configuracion.Consulta(ref configuracion, session.Emp_Cnx);
                StringBuilder cuerpo_correo = new StringBuilder();

                cuerpo_correo.Append("<div align='center'>");
                cuerpo_correo.Append("<table><tr><td>");
                cuerpo_correo.Append("<td></td>");
                cuerpo_correo.Append("</tr><tr><td colspan='2'><br><br><br></td>");
                cuerpo_correo.Append("</tr><tr>");
                cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='2'>");

                cuerpo_correo.Append("Buen día, Se les comunica por este medio. <br><br> ");

                cuerpo_correo.Append("Se ha AUTORIZADO la siguiente solicitud de captura de OC Centralizada: . <br><br>");
                cuerpo_correo.Append("CDI: Monterrey <br>");
                cuerpo_correo.Append("Usuario: Prueba <br>");
                cuerpo_correo.Append("Cliente: VIPS <br>");
                cuerpo_correo.Append("OC Centralizada: Prueba <br><br>");


                cuerpo_correo.Append("Producto de venta original: <br>");
                cuerpo_correo.Append("Precio de venta: <br>");
                cuerpo_correo.Append("Producto de venta sustituto. <br>");
                cuerpo_correo.Append("Precio AAA: <br>");

                cuerpo_correo.Append("Cordinardor de la cuenta: Prueba <br>");
                cuerpo_correo.Append("Sistema automático de reporte de captación de pedido. <br>  Fecha de realización: " + DateTime.Now.ToString("dd/MM/yyyy") + "<br>");
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");
                cuerpo_correo.Append("<center><br>");
                cuerpo_correo.Append("</td></tr></table></div>");

                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);

                MailMessage m = new MailMessage();
                m.From = new MailAddress(configuracion.Mail_Remitente, "solicitud autorizacion producto AAA orden de compra: Prueba");
                m.Subject = "Reporte ";
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //foreach (UsuarioCorreo correo in ListaCorreoUsuario)
                //{
                //    m.To.Add(new MailAddress(correo.U_Correo));
                //} 
                m.To.Add(new MailAddress("erikrgc@hotmail.com"));
                m.IsBodyHtml = true;

                string body = cuerpo_correo.ToString().Trim();
                AlternateView vistaHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);

                m.AlternateViews.Add(vistaHtml);
                try
                {
                    sm.Send(m);
                }
                catch (Exception ex)
                {
                    mensage = "Fallo en enviar el correo electronico";
                }
            }
            catch (Exception)
            {
                mensage = "Fallo en la configuración del correo";
            }
        }

        #endregion 
    }
}