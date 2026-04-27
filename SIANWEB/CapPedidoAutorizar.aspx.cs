using CapaEntidad;
using CapaNegocios;
using DevExpress.Web;
using DevExpress.Web.Bootstrap;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIANWEB
{
    public partial class CapPedidoAutorizar : System.Web.UI.Page
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

                GrdAutPEdido.DataSource = dtTemp;
                GrdAutPEdido.DataBind();
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();

                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            //RAM1.ResponseScripts.Add("AbrirContrasenas(); return false");
                            return;
                        }

                        Session["ProdSOli"] = null;
                        CargarCentros();
                        CMBFechaInicial.Value = Sesion.CalendarioIni;
                        CMBFechaFinal.Value = Sesion.CalendarioFin;
                        Inicializar();
                    }
                }
            }
            catch (Exception ex)
            {
                warning(ex.Message.ToString() + "-" + new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        #endregion

        #region Funciones

        protected void CmbCentro_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }
                CN__Comun comun = new CN__Comun();
                comun.BoostrapCambiarCdVer(CmbCentro, ref sesion);
            }
            catch (Exception ex)
            {
                warning(ex.Message.ToString() + "-" + "CmbCentro_TextChanged");
            }
        }

        private void CargarCentros()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();


                if (Sesion.U_MultiOfi == false)
                {
                    CN_Comun.DevLlenaCombo(2, Sesion.Id_Emp, Sesion.Id_U, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    CmbCentro.ReadOnly = true;
                    CmbCentro.Value = (Sesion.Id_Cd_Ver.ToString());
                    CmbCentro.Enabled = false;

                }
                else
                {
                    CN_Comun.DevLlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, Sesion.Id_Cd_Ver, Sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.Value = Sesion.Id_Cd_Ver.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                {
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                }
                else
                {
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;
                }

                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, Sesion.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;
                Permiso Permiso = new Permiso();
                Permiso.Id_U = Sesion.Id_U;
                Permiso.Id_Cd = Sesion.Id_Cd;
                Permiso.Sm_cve = pagina.Clave;
                //Esta clave depende de la pantalla

                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, Sesion.Emp_Cnx);

                if (Permiso.PAccesar == true)
                {
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;

                    //if (Permiso.PGrabar == false)
                    //{
                    //    this.rtb1.Items[6].Visible = false;
                    //}
                    //if (Permiso.PGrabar == false && Permiso.PModificar == false)
                    //{
                    //    this.rtb1.Items[5].Visible = false;
                    //} 
                }
                else
                {
                    Response.Redirect("Inicio.aspx");
                }
                CN_Ctrl ctrl = new CN_Ctrl();
                ctrl.ValidarCtrl(Sesion, pagina.Clave, divPrincipal);
                ctrl.ListaCtrls(Sesion.Emp_Cnx, pagina.Clave, divPrincipal.Controls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Inicializar()
        {
            try
            {

                Sesion sesion = (Sesion)this.Session["Sesion" + this.Session.SessionID];
                CargarCliente();
                CargarDatos(); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CargarCliente()
        {
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            if (sesion.Id_Rik == -1)
            {
                CN_Comun.DevLlenaCombo(1, sesion.Id_Emp, sesion.Id_Cd_Ver, 2, null, sesion.Emp_Cnx, "spCatCliente_Combo", ref BCBCliente);
            }
            else
            {
                CN_Comun.DevLlenaCombo(1, sesion.Id_Emp, sesion.Id_Cd_Ver, 2, sesion.Id_Rik, sesion.Emp_Cnx, "spCatCliente_Combo", ref BCBCliente);
            }
            BCBCliente.Value = "-1";
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

                CargarGrid();
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

            Ped.Id_Emp = sesion.Id_Emp;
            Ped.Id_Cd = sesion.Id_Cd_Ver;
            Ped.FechaInicial = Convert.ToDateTime(CMBFechaInicial.Value);
            Ped.FechaFinal = Convert.ToDateTime(CMBFechaFinal.Value);
            Ped.Id_Cte = Convert.ToInt32(BCBCliente.Value.ToString()); 
            Ped.Estatus = "T";

            CN.ConsultaSolicitudPedidos(Ped, sesion.Emp_Cnx, ref Solicitud);

            Session["ProdSOli"] = Solicitud;
            GrdAutPEdido.DataSource = Solicitud;
            GrdAutPEdido.DataBind();
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
                pedido.Id_Emp = sesion.Id_Emp;
                pedido.Id_Cd = sesion.Id_Cd_Ver;
                pedido.Id_U = sesion.Id_U;
                pedido.id_Sol = Convert.ToInt32(id_Sol);
                pedido.Estatus = "R";

                CN.ActualizarSolicitudPedido(pedido, sesion.Emp_Cnx, ref verificador);
                if (verificador == 0)
                {
                    CargarGrid();

                    string mensage = "";
                    Sesion session = new Sesion();
                    session = (Sesion)Session["Sesion" + Session.SessionID];
                    EnviarCorreoRechazado(pedido.id_Sol, session.Emp_Cnx, ref mensage);

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
                pedido.Id_Emp = sesion.Id_Emp;
                pedido.Id_Cd = sesion.Id_Cd_Ver;
                pedido.Id_U = sesion.Id_U;
                pedido.id_Sol = Convert.ToInt32(id_Sol);
                pedido.Estatus = "A";

                CN.ActualizarSolicitudPedido(pedido, sesion.Emp_Cnx, ref verificador);

                if (verificador == 0)
                {
                    CargarGrid();

                    string mensage = "";
                    Sesion session = new Sesion();
                    session = (Sesion)Session["Sesion" + Session.SessionID];
                    EnviarCorreoAceptado(pedido.id_Sol, session.Emp_Cnx, ref mensage);

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


        private void EnviarCorreoRechazado(int id_sol, string conexion, ref string mensage)
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


                List<PedidoVtaInst> DatosPedido = (List<PedidoVtaInst>)Session["ProdSOli"];

                PedidoVtaInst Datos = (from tlist in DatosPedido
                                       where tlist.id_Sol == id_sol
                                       select tlist).FirstOrDefault();

                if (Datos != null)
                { 

                    cuerpo_correo.Append("<div>");
                    cuerpo_correo.Append("<table><tr><td>");
                    cuerpo_correo.Append("<td></td>");
                    cuerpo_correo.Append("</tr><tr><td colspan='2'><br><br><br></td>");
                    cuerpo_correo.Append("</tr><tr>");
                    cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='2'>");

                    cuerpo_correo.Append("Buen Día, Se les Comunica por este Medio. <br><br> ");

                    cuerpo_correo.Append("Se ha Rechazado la solicitud del Pedido: " + id_sol +  "<br><br>");
                    cuerpo_correo.Append("Sucursal: "+ CmbCentro.Text + " <br>");
                    cuerpo_correo.Append("Usuario: " + sesion.U_Nombre  +" <br>");
                    cuerpo_correo.Append("Cliente: " +Datos.Id_Cte + "- " +  Datos.Cte_Nom + " <br>");   
                  
                    cuerpo_correo.Append("Sistema Automático de Solicitud de Pedidos Clientes Crédito Bloqueado. <br>  Fecha de Realización: " + DateTime.Now.ToString("dd/MM/yyyy") + "<br>");
                    cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");
                    cuerpo_correo.Append("<br>");
                    cuerpo_correo.Append("</td></tr></table></div>");


                    SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                    sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);

                    MailMessage m = new MailMessage();
                    m.From = new MailAddress(configuracion.Mail_Remitente, "Rechazado la Solicitud del Pedido: " + id_sol);
                    m.Subject = "Pedido Rechazado: " + id_sol;
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    m.To.Add(new MailAddress(ConsultarEmail(Convert.ToInt32(Datos.Id_U))));

                    //m.To.Add(new MailAddress("servicios.informatica@gibraltar.com.mx"));
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
            }
            catch (Exception)
            {
                mensage = "Fallo en la configuración del correo";
            }
        }



        private void EnviarCorreoAceptado(int id_sol,  string conexion, ref string mensage)
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
                 

                List<PedidoVtaInst> DatosPedido = (List<PedidoVtaInst>)Session["ProdSOli"];

                PedidoVtaInst Datos = (from tlist in DatosPedido
                                       where tlist.id_Sol == id_sol
                                       select tlist).FirstOrDefault();

                if (Datos != null)
                {

                    cuerpo_correo.Append("<div>");
                    cuerpo_correo.Append("<table><tr><td>");
                    cuerpo_correo.Append("<td></td>");
                    cuerpo_correo.Append("</tr><tr><td colspan='2'><br><br><br></td>");
                    cuerpo_correo.Append("</tr><tr>");
                    cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='2'>");

                    cuerpo_correo.Append("Buen Día, Se les Comunica por este Medio. <br><br> ");

                    cuerpo_correo.Append("Se ha Autorizado la Solicitud del Pedido: " + id_sol + "<br><br>");
                    cuerpo_correo.Append("Sucursal: " + CmbCentro.Text + " <br>");
                    cuerpo_correo.Append("Usuario que Autorizo: " + sesion.U_Nombre + " <br>");
                    cuerpo_correo.Append("Cliente: " + Datos.Id_Cte + "- " + Datos.Cte_Nom + " <br>");

                    cuerpo_correo.Append("Sistema Automático de Solicitud de Pedidos Clientes Crédito Bloqueado. <br>  Fecha de Realización: " + DateTime.Now.ToString("dd/MM/yyyy") + "<br>");
                    cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");
                    cuerpo_correo.Append("<br>");
                    cuerpo_correo.Append("</td></tr></table></div>");

                    SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                    sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);

                    MailMessage m = new MailMessage();
                    m.From = new MailAddress(configuracion.Mail_Remitente, "Autorizacion del Pedido: " + id_sol);
                    m.Subject = "Autorizacion del Pedido: " + id_sol;
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    m.To.Add(new MailAddress(ConsultarEmail(Convert.ToInt32(Datos.Id_U))));

                    //m.To.Add(new MailAddress("servicios.informatica@gibraltar.com.mx"));
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
            }
            catch (Exception)
            {
                mensage = "Fallo en la configuración del correo";
            }
        }

        private string ConsultarEmail(int id_u)
        {
            Sesion session = new Sesion();
            session = (Sesion)Session["Sesion" + Session.SessionID];
            CN_CatUsuario cn_catusuario = new CN_CatUsuario();
            Usuario u = new Usuario();
            u.Id_Emp = session.Id_Emp;
            u.Id_Cd = session.Id_Cd_Ver;
            u.Id_U = id_u;
            string correo = "";
            cn_catusuario.ConsultaCorreoUsuario(u, session.Emp_Cnx, ref correo);
            return correo;
        }
        #endregion

    }
}