using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Text;
using System.Net;
using System.IO;
using System.Collections;
using Telerik.Reporting.Processing;
using System.Net.Mail;
using System.Net.Mime;

namespace SIANWEB
{
    public partial class CapGestionPreciosP : System.Web.UI.Page
    {
        #region Variables

        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private Sesion sesion
        {
            get
            {
                return (Sesion)Session["Sesion" + Session.SessionID];
            }
            set
            {
                Session["Sesion" + Session.SessionID] = value;
            }
        }
        public Convenio convenio
        {
            get { return (Convenio)Session["Convenio" + Session.SessionID]; }
            set { Session["Convenio" + Session.SessionID] = value; }
        }
        #endregion Variables

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
                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_Cve.Value = randObj.Next().ToString();
                        this.TblEncabezado.Visible = false;
                        this.ValidarPermisos();
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        this.CargarCentros();
                        CargarCategoria();
                        //CargarPermisosEspeciales();
                        rgUtilizados.Rebind();
                        rgNoUtilizados.Rebind();

                        Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                        //Convenio conv = (Convenio)Session["Convenio" + Session.SessionID];
                        //if (conv.Pue_Admin1 != sesion.Id_U && conv.Pue_Admin2 != sesion.Id_U)
                        //{
                        //    this.RadToolBar1.Items[1].Visible = false;
                        //}

                        //TODO leer cuantas solicitudes están pendientes y actualizarlas 
                        this.RadToolBar1.Items[2].Text = LeerSolicitudespendientes();

                    }
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }
        }
        #region Eventos
        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
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
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);

                rgUtilizados.Rebind();
                rgNoUtilizados.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                string cmd = e.Argument.ToString();
                switch (cmd)
                {
                    case "RebindGrid":
                        rgUtilizados.Rebind();
                        rgNoUtilizados.Rebind();
                        break;
                    case "VerConvenio":
                        RAM1.ResponseScripts.Add("return OpenWindow('" + HFId_PC.Value + "','" + HFTipoOP.Value + " ')");

                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }
        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
                if (!_PermisoImprimir)
                {
                    this.Alerta("No tiene permisos para ver el reporte");
                    return;
                }
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;


                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Convenio conv = (Convenio)Session["Convenio" + Session.SessionID];


                if (Page.IsValid)
                {
                    if (btn.CommandName == "new")
                    {
                        //if (sesion.Id_U == 1 || conv.Pue_Admin1 == sesion.Id_U || conv.Pue_Admin2 == sesion.Id_U)
                        //{

                        RAM1.ResponseScripts.Add("return OpenWindow('0','0')");
                        //}
                        //else
                        //{
                        //    Alerta("No tiene permiso para agregar convenios");
                        //    return;
                        //}
                    }
                    else if (btn.CommandName == "imprimir")
                    {
                        Imprimir();
                    }
                    else if (btn.CommandName == "expclientes")
                    {
                        ExportarClientes();
                    }
                    else if (btn.CommandName == "expconvenios")
                    {
                        ExportarConvenios();
                    }
                    else if (btn.CommandName == "expConsecionarios")
                    {
                        Exportarconcesionarioshabilitados();
                    }
                    else if (btn.CommandName == "ayuda")
                    {
                        //if (sesion.Id_U == 1 || conv.Pue_Admin1 == sesion.Id_U || conv.Pue_Admin2 == sesion.Id_U)
                        //{

                        RAM1.ResponseScripts.Add("return OpenWindowConvenioInformacion('0')"); //le envío 0 para que sea de solo consulta y no puedan subir los archivos
                        //}
                        //else
                        //{
                        //    Alerta("No tiene permiso para esta opción");
                        //    return;
                        //}
                    }
                    else if (btn.CommandName == "atiendesol")
                    {
                        //if (sesion.Id_U == 1 || conv.Pue_Admin1 == sesion.Id_U || conv.Pue_Admin2 == sesion.Id_U)
                        //{

                        RAM1.ResponseScripts.Add("return OpenWindowSolicitudVincular('0','0')");
                        //}
                        //else
                        //{
                        //    Alerta("No tiene permiso para esta opción");
                        //    return;
                        //}
                    } else if (btn.CommandName == "expbonificaciones")
                    {
                        //if (sesion.Id_U == 1 || conv.Pue_Admin1 == sesion.Id_U || conv.Pue_Admin2 == sesion.Id_U)
                        //{

                            RAM1.ResponseScripts.Add("return OpenWindowBonificacion()");
                        //}
                        //else
                        //{
                        //    Alerta("No tiene permiso para esta opción");
                        //    return;
                        //}
                    }

                    else if (btn.CommandName == "configuracion")
                    {
                        //if (sesion.Id_U == 1 || conv.Pue_Admin1 == sesion.Id_U || conv.Pue_Admin2 == sesion.Id_U)
                        //{

                        RAM1.ResponseScripts.Add("return OpenWindowConfig('0','0')");
                        //}
                        //else
                        //{
                        //    Alerta("Esta funcionalidad es de uso exclusivo para los\nAdministradores del módulo Convenios, si requiere acceso\nfavor de contactar al departamento de Mercadotecnia.");
                        //    return;
                        //}

                    }

                    //expbonificaciones, ayuda
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void CmbCentro_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (sesion == null)
            {
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
            }
            CN__Comun comun = new CN__Comun();
            comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
        }
        protected void BtnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //if (this.CmbTipoB.SelectedValue == "-1")
                //{
                //    Alerta("Seleccione el tipo de búsqueda");
                //    return;
                //}
                //if (this.CmbVencido.SelectedValue == "-1")
                //{
                //    Alerta("Seleccion el criterio de vencido");
                //    return;
                //}
                //if (this.CmbCategoria.SelectedValue == "-1")
                //{
                //    Alerta("Seleccion la categoría");
                //    return;
                //}

                //if (TxtValorCategoria.Text == string.Empty)
                //{
                //    Alerta("Ingrese el valor a filtrar");
                //    return;
                //}

                rgUtilizados.Rebind();
                rgNoUtilizados.Rebind();
            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void rgUtilizado_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Convenio conv = (Convenio)Session["Convenio" + Session.SessionID];
                GridItem item = e.Item;
                int Id_PC = 0;
                string PC_NoConvenio;
                string PC_Nombre;
                string Id_CatStr;

                switch (e.CommandName)
                {
                    case "Baja":

                        if (conv.Pue_Admin1 == sesion.Id_U || conv.Pue_Admin2 == sesion.Id_U)
                        {

                            if (this.rgUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Estatus"].Text == "Cancelado")
                            {
                                Alerta("Imposible cancelar el convenio, ya que ya se encuentra cancelado.");
                                return;
                            }

                            Id_PC = int.Parse(this.rgUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Id_PC"].Text);
                            PC_NoConvenio = this.rgUtilizados.MasterTableView.Items[e.Item.ItemIndex]["PC_NoConvenio"].Text;
                            PC_Nombre = this.rgUtilizados.MasterTableView.Items[e.Item.ItemIndex]["PC_Nombre"].Text;
                            Id_CatStr = this.rgUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Cat_DescCorta"].Text;
                            RAM1.ResponseScripts.Add("return OpenWindowDet('" + Id_PC + "','" + PC_NoConvenio + "','" + PC_Nombre + "','" + Id_CatStr + "')");


                        }
                        else
                        {
                            Alerta("No tiene permisos para administrar convenios");
                            return;
                        }
                        break;

                    case "Editar":

                        Id_PC = int.Parse(this.rgUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Id_PC"].Text);
                        RAM1.ResponseScripts.Add("return OpenWindow('" + Id_PC + "','0')");

                        //if (conv.Pue_Admin1 == sesion.Id_U || conv.Pue_Admin2 == sesion.Id_U)
                        //{
                        //    Id_PC = int.Parse(this.rgUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Id_PC"].Text);
                        //    HFTipoOP.Value = "0";
                        //    if (this.rgUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Estatus"].Text == "Cancelado")
                        //    {
                        //        HFId_PC.Value = Id_PC.ToString();
                        //        RAM1.ResponseScripts.Add("return  callConfirm('Esta por editar un convenio cancelado, ¿Desea continuar?')");

                        //        break;
                        //    }
                        //    else
                        //    {
                        //        RAM1.ResponseScripts.Add("return OpenWindow('" + Id_PC + "','0')");
                        //    }
                        //}
                        //else
                        //{
                        //    Alerta("No tiene permisos para administrar convenios");
                        //    return;
                        //}
                        break;

                    case "Sustituir":
                        if (conv.Pue_Admin1 == sesion.Id_U || conv.Pue_Admin2 == sesion.Id_U)
                        {
                            Id_PC = int.Parse(this.rgUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Id_PC"].Text);
                            HFTipoOP.Value = "1";
                            if (this.rgUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Estatus"].Text == "Cancelado")
                            {
                                HFId_PC.Value = Id_PC.ToString();
                                RAM1.ResponseScripts.Add("return  callConfirm('Esta por sustituir un convenio cancelado, ¿Desea continuar?')");
                            }
                            else
                            {

                                // Id_PC = int.Parse(this.rgUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Id_PC"].Text);
                                RAM1.ResponseScripts.Add("return OpenWindow('" + Id_PC + "','1')");
                            }
                        }
                        else
                        {
                            Alerta("No tiene permisos para administrar convenios");
                            return;
                        }
                        break;
                    case "VincularSuc":
                        if (conv.Pue_Admin1 == sesion.Id_U || conv.Pue_Admin2 == sesion.Id_U)
                        {
                            if (this.rgUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Estatus"].Text == "Cancelado")
                            {
                                Alerta("Imposible vincular el convenio, ya que ya se encuentra cancelado.");
                                return;
                            }

                            Id_PC = int.Parse(this.rgUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Id_PC"].Text);
                            PC_NoConvenio = this.rgUtilizados.MasterTableView.Items[e.Item.ItemIndex]["PC_NoConvenio"].Text;
                            PC_Nombre = this.rgUtilizados.MasterTableView.Items[e.Item.ItemIndex]["PC_Nombre"].Text;
                            Id_CatStr = this.rgUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Cat_DescCorta"].Text;
                            RAM1.ResponseScripts.Add("return OpenWindowVincularSuc('" + Id_PC + "','" + PC_NoConvenio + "','" + PC_Nombre + "','" + Id_CatStr + "')");
                        }
                        else
                        {
                            Alerta("No tiene permisos para administrar convenios");
                            return;
                        }
                        break;
                    case "VincularCte":
                        if (this.rgUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Estatus"].Text == "Cancelado")
                        {
                            Alerta("Imposible generar solicitud, ya que ya se encuentra cancelado.");
                            return;
                        }

                        if (this.rgUtilizados.MasterTableView.Items[e.Item.ItemIndex]["PCD_Gral"].Text == "True")
                        {
                            Alerta("No es necesario vincular clientes a este convenio, esto debido a que fue autorizado para aplicarse de manera general a todos los clientes del Centro de Distribución.");
                            return;

                        }

                        if (this.rgUtilizados.MasterTableView.Items[e.Item.ItemIndex]["PCD_Usar"].Text == "False")
                        {
                            Alerta("La sucursal no esta autorizada para utilizar este convenio");
                            return;

                        }
                        

                        Id_PC = int.Parse(this.rgUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Id_PC"].Text);
                        PC_NoConvenio = this.rgUtilizados.MasterTableView.Items[e.Item.ItemIndex]["PC_NoConvenio"].Text;
                        PC_Nombre = this.rgUtilizados.MasterTableView.Items[e.Item.ItemIndex]["PC_Nombre"].Text;
                        Id_CatStr = this.rgUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Cat_DescCorta"].Text;
                        RAM1.ResponseScripts.Add("return OpenWindowSolicitud('" + Id_PC + "','" + PC_NoConvenio + "','" + PC_Nombre + "','" + Id_CatStr + "','0')");

                        break;

                    case "Correo":

                        //if (item.Cells[3].Text.ToString().ToLower() == "i")
                        //{
                        Id_PC = int.Parse(this.rgUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Id_PC"].Text);
                        PC_NoConvenio = this.rgUtilizados.MasterTableView.Items[e.Item.ItemIndex]["PC_NoConvenio"].Text;
                        PC_Nombre = this.rgUtilizados.MasterTableView.Items[e.Item.ItemIndex]["PC_Nombre"].Text;
                        Id_CatStr = this.rgUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Cat_DescCorta"].Text;
                        RAM1.ResponseScripts.Add("return OpenWindowCorreos('" + Id_PC + "','" + PC_NoConvenio + "','" + PC_Nombre + "','" + Id_CatStr + "')");

                        break;
                        //}
                        //else
                        //{
                        //    Alerta("La solicitud se encuentra en estatus no válido para su envio");
                        //}
                        break;


                    case "Imprimir":
                        Id_PC = int.Parse(this.rgUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Id_PC"].Text);

                        Imprimir(Id_PC);
                        break;
                    case "Vinculados":
                        int Id_Cd;
                        //if (conv.Pue_Admin1 == sesion.Id_U || conv.Pue_Admin2 == sesion.Id_U)
                        //{
                        //    Id_Cd = -1;
                        //}
                        //else
                        //{
                        Id_Cd = sesion.Id_Cd_Ver;
                        //}

                        Id_PC = int.Parse(this.rgUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Id_PC"].Text);
                        PC_NoConvenio = this.rgUtilizados.MasterTableView.Items[e.Item.ItemIndex]["PC_NoConvenio"].Text;
                        PC_Nombre = this.rgUtilizados.MasterTableView.Items[e.Item.ItemIndex]["PC_Nombre"].Text;
                        Id_CatStr = this.rgUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Cat_DescCorta"].Text;

                        RAM1.ResponseScripts.Add("return OpenWindowVinculados('" + Id_PC + "','" + PC_NoConvenio + "','" + PC_Nombre + "','" + Id_CatStr + "','" + Id_Cd + "')");
                        break;

                    case "Enviar":
                        if (item.Cells[3].Text.ToString().ToLower() == "i")
                        {
                            EnviaEmail(Convert.ToInt32(item.Cells[2].Text));
                        }
                        else
                        {
                            Alerta("La solicitud se encuentra en estatus no válido para su envio");
                        }
                        break;

                    case "historial":
                        ////if (conv.Pue_Admin1 == sesion.Id_U || conv.Pue_Admin2 == sesion.Id_U)
                        ////{
                        Id_PC = int.Parse(this.rgUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Id_PC"].Text);
                        HFTipoOP.Value = "0";
                        //if (this.rgUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Estatus"].Text == "Cancelado")
                        //{
                        //    HFId_PC.Value = Id_PC.ToString();
                        //    RAM1.ResponseScripts.Add("return  callConfirm('Esta por editar un convenio cancelado, ¿Desea continuar?')");

                        //    break;
                        //}
                        //else
                        //{
                        RAM1.ResponseScripts.Add("return OpenWindowHistorial('" + Id_PC + "','0')");
                        //}
                        //}
                        //else
                        //{
                        //    Alerta("No tiene permisos para administrar convenios");
                        //    return;
                        //}
                        break;


                }

            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        private void EnviaEmail(int Convenio)
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                int verificador = -1;
                Acys conveniod = new Acys();
                conveniod.Id_Emp = session.Id_Emp;
                conveniod.Id_Cd = session.Id_Cd_Ver;
                conveniod.Id_Acs = Convenio;

                /* CN_Capconveniod cn_conveniod = new CN_Capconveniod();
                 cn_conveniod.ConsultaEnvio(ref conveniod, session.Emp_Cnx, ref verificador);*/

                //if (CambiarEstatus(Id_conveniod, "S") != 1)
                //{
                //    Alerta("Ocurrió un error al intentar realizar la solicitud");
                //    return;
                //}

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
                cuerpo_correo.Append("Se ha colocado una solicitud de autorización de acuerdo comercial con el número  " + Convenio);
                /*if (conveniod.Acs_Sustituye != null)
                    cuerpo_correo.Append(" que sustituye a la solicitud #" + conveniod.Acs_Sustituye);*/

                cuerpo_correo.Append(", de la sucursal " + session.Id_Cd_Ver);
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");
                string[] url = Request.Url.ToString().Split(new char[] { '/' });

                cuerpo_correo.Append("<center><br>");
                cuerpo_correo.Append("<a href='" + Request.Url.ToString().Replace(url[url.Length - 1], "") + "Capconveniod.aspx?Id=" + Convenio + "&Accion=2&PermisoGuardar=1&PermisoModificar=1&PermisoEliminar=1&PermisoImprimir=1'" + ">");
                cuerpo_correo.Append("Solicitud de autorización de acuerdos comerciales</a></font></center>");
                cuerpo_correo.Append("</td></tr></table></div>");
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                sm.EnableSsl = true;
                MailMessage m = new MailMessage();
                m.From = new MailAddress(configuracion.Mail_Remitente);
                //m.To.Add(new MailAddress(configuracion.Mail_conveniod));
                m.Subject = "Solicitud de autorización de Acuerdo Comercial #" + Convenio + " del centro " + session.Id_Cd_Ver;
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
                    //CambiarEstatus(Id_conveniod, "C");
                    Alerta("Error al enviar el correo. Favor de revisar la configuración del sistema");
                    return;
                }
                Alerta("Solicitud enviada correctamente");
                rgNoUtilizados.Rebind();
                rgUtilizados.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        protected void rgNoUtilizado_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Convenio conv = (Convenio)Session["Convenio" + Session.SessionID];
                GridItem item = e.Item;
                int Id_PC = 0;
                string PC_NoConvenio;
                string PC_Nombre;
                string Id_CatStr;
                switch (e.CommandName)
                {

                    case "Baja":
                        if (conv.Pue_Admin1 == sesion.Id_U || conv.Pue_Admin2 == sesion.Id_U)
                        {

                            if (this.rgNoUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Estatus"].Text == "Cancelado")
                            {
                                Alerta("Imposible cancelar el convenio, ya que ya se encuentra cancelado");
                                return;
                            }



                            Id_PC = int.Parse(this.rgNoUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Id_PC"].Text);
                            PC_NoConvenio = this.rgNoUtilizados.MasterTableView.Items[e.Item.ItemIndex]["PC_NoConvenio"].Text;
                            PC_Nombre = this.rgNoUtilizados.MasterTableView.Items[e.Item.ItemIndex]["PC_Nombre"].Text;
                            Id_CatStr = this.rgNoUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Cat_DescCorta"].Text;



                            RAM1.ResponseScripts.Add("return OpenWindowDet('" + Id_PC + "','" + PC_NoConvenio + "','" + PC_Nombre + "','" + Id_CatStr + "')");
                        }
                        else
                        {
                            Alerta("No tiene permisos para administrar convenios");
                            return;

                        }
                        break;
                    case "Editar":

                        Id_PC = int.Parse(this.rgNoUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Id_PC"].Text);
                        RAM1.ResponseScripts.Add("return OpenWindow('" + Id_PC + "','0')");

                        //if (conv.Pue_Admin1 == sesion.Id_U || conv.Pue_Admin2 == sesion.Id_U)
                        //{
                        //    Id_PC = int.Parse(this.rgNoUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Id_PC"].Text);
                        //    HFTipoOP.Value = "0";

                        //    if (this.rgNoUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Estatus"].Text == "Cancelado")
                        //    {
                        //        HFId_PC.Value = Id_PC.ToString();
                        //        RAM1.ResponseScripts.Add("return  callConfirm('Esta por editar un convenio cancelado, ¿Desea continuar?')");

                        //        break;
                        //    }
                        //    else
                        //    {
                        //        RAM1.ResponseScripts.Add("return OpenWindow('" + Id_PC + "','0')");
                        //    }
                        //}
                        //else
                        //{
                        //     Alerta("No tiene permisos para administrar convenios");
                        //    return;

                        //}
                        break;
                    case "Sustituir":
                        if (conv.Pue_Admin1 == sesion.Id_U || conv.Pue_Admin2 == sesion.Id_U)
                        {
                            Id_PC = int.Parse(this.rgNoUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Id_PC"].Text);
                            HFTipoOP.Value = "1";

                            if (this.rgNoUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Estatus"].Text == "Cancelado")
                            {
                                HFId_PC.Value = Id_PC.ToString();
                                RAM1.ResponseScripts.Add("return  callConfirm('Esta por sustituir un convenio cancelado, ¿Desea continuar?')");
                            }
                            else
                            {

                                Id_PC = int.Parse(this.rgNoUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Id_PC"].Text);
                                RAM1.ResponseScripts.Add("return OpenWindow('" + Id_PC + "','1')");
                            }
                        }
                        else
                        {
                            Alerta("No tiene permisos para administrar convenios");
                            return;
                        }
                        break;
                    case "VincularSuc":
                        if (conv.Pue_Admin1 == sesion.Id_U || conv.Pue_Admin2 == sesion.Id_U)
                        {
                            if (this.rgNoUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Estatus"].Text == "Cancelado")
                            {
                                Alerta("Imposible vincular el convenio, ya que ya se encuentra cancelado.");
                                return;
                            }

                            Id_PC = int.Parse(this.rgNoUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Id_PC"].Text);
                            PC_NoConvenio = this.rgNoUtilizados.MasterTableView.Items[e.Item.ItemIndex]["PC_NoConvenio"].Text;
                            PC_Nombre = this.rgNoUtilizados.MasterTableView.Items[e.Item.ItemIndex]["PC_Nombre"].Text;
                            Id_CatStr = this.rgNoUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Cat_DescCorta"].Text;
                            RAM1.ResponseScripts.Add("return OpenWindowVincularSuc('" + Id_PC + "','" + PC_NoConvenio + "','" + PC_Nombre + "','" + Id_CatStr + "')");
                        }
                        else
                        {
                            Alerta("No tiene permisos para administrar convenios");
                            return;
                        }
                        break;
                    case "VincularCte":

                        if (this.rgNoUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Estatus"].Text == "Cancelado")
                        {
                            Alerta("Imposible generar solicitud, ya que ya se encuentra cancelado.");
                            return;
                        }

                        if (this.rgNoUtilizados.MasterTableView.Items[e.Item.ItemIndex]["PCD_Usar"].Text == "False")
                        {
                            Alerta("La sucursal no esta autorizada para utilizar este convenio");
                            return;

                        }
                        if (this.rgNoUtilizados.MasterTableView.Items[e.Item.ItemIndex]["PCD_Gral"].Text == "True")
                        {
                            Alerta("La presente solicitud no se puede procesar debido a que el cliente ya se encuentra vinculado al convenio.");
                            return;

                        }



                        Id_PC = int.Parse(this.rgNoUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Id_PC"].Text);
                        PC_NoConvenio = this.rgNoUtilizados.MasterTableView.Items[e.Item.ItemIndex]["PC_NoConvenio"].Text;
                        PC_Nombre = this.rgNoUtilizados.MasterTableView.Items[e.Item.ItemIndex]["PC_Nombre"].Text;
                        Id_CatStr = this.rgNoUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Cat_DescCorta"].Text;
                        RAM1.ResponseScripts.Add("return OpenWindowSolicitud('" + Id_PC + "','" + PC_NoConvenio + "','" + PC_Nombre + "','" + Id_CatStr + "','0')");

                        break;
                    case "Imprimir":
                        Id_PC = int.Parse(this.rgNoUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Id_PC"].Text);

                        Imprimir(Id_PC);
                        break;
                    case "Vinculados":
                        int Id_Cd;
                        //if (conv.Pue_Admin1 == sesion.Id_U || conv.Pue_Admin2 == sesion.Id_U)
                        //{
                        //    Id_Cd = -1;
                        //}
                        //else
                        //{
                        Id_Cd = sesion.Id_Cd_Ver;
                        //}

                        Id_PC = int.Parse(this.rgNoUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Id_PC"].Text);
                        PC_NoConvenio = this.rgNoUtilizados.MasterTableView.Items[e.Item.ItemIndex]["PC_NoConvenio"].Text;
                        PC_Nombre = this.rgNoUtilizados.MasterTableView.Items[e.Item.ItemIndex]["PC_Nombre"].Text;
                        Id_CatStr = this.rgNoUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Cat_DescCorta"].Text;
                        RAM1.ResponseScripts.Add("return OpenWindowVinculados('" + Id_PC + "','" + PC_NoConvenio + "','" + PC_Nombre + "','" + Id_CatStr + "','" + Id_Cd + "')");
                        break;


                    case "Enviar":
                        if (item.Cells[3].Text.ToString().ToLower() == "i")
                        {
                            EnviaEmail(Convert.ToInt32(item.Cells[2].Text));
                        }
                        else
                        {
                            Alerta("La solicitud se encuentra en estatus no válido para su envio");
                        }
                        break;

                    case "historial":
                        //if (conv.Pue_Admin1 == sesion.Id_U || conv.Pue_Admin2 == sesion.Id_U)
                        //{
                            Id_PC = int.Parse(this.rgNoUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Id_PC"].Text);
                            HFTipoOP.Value = "0";
                            //if (this.rgUtilizados.MasterTableView.Items[e.Item.ItemIndex]["Estatus"].Text == "Cancelado")
                            //{
                            //    HFId_PC.Value = Id_PC.ToString();
                            //    RAM1.ResponseScripts.Add("return  callConfirm('Esta por editar un convenio cancelado, ¿Desea continuar?')");

                            //    break;
                            //}
                            //else
                            //{
                            RAM1.ResponseScripts.Add("return OpenWindowHistorial('" + Id_PC + "','0')");
                            //}
                        //}
                        //else
                        //{
                        //    Alerta("No tiene permisos para administrar convenios");
                        //    return;
                        //}
                        break;



                }

            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgUtilizado_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            try
            {
                rgUtilizados.Rebind();

            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
            //rgPedido.Rebind();
        }
        protected void rgNoUtilizado_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            try
            {
                rgNoUtilizados.Rebind();

            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
            //rgPedido.Rebind();
        }
        protected void rgUtilizado_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    this.rgUtilizados.DataSource = ListUtilizados();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgNoUtilizado_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    this.rgNoUtilizados.DataSource = ListNoUtilizados();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgUtilizado_ItemDataBound(object source, GridItemEventArgs e)
        {
            try
            {

                //if (e.Item is GridDataItem)
                //{
                //    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                //    Convenio conv = (Convenio)Session["Convenio" + Session.SessionID];
                //    GridDataItem item = (GridDataItem)e.Item;
                //    if (conv.Pue_Admin1 != sesion.Id_U && conv.Pue_Admin2 != sesion.Id_U && sesion.Id_U != 1)
                //    {
                //        item["VincularCte"].Visible = true;
                //    }
                //    else
                //    {
                //        item["Editar"].Visible = true;
                //        item["sustituir"].Visible = true;
                //        item["VincularSuc"].Visible = true;
                //        item["Cancelar"].Visible = true;


                //    }

                //}

                //if (e.Item is GridHeaderItem)
                //{
                //Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                //Convenio conv = (Convenio)Session["Convenio" + Session.SessionID];
                //GridHeaderItem item = (GridHeaderItem)e.Item;
                //    if (conv.Pue_Admin1 != sesion.Id_U && conv.Pue_Admin2 != sesion.Id_U && sesion.Id_U != 1)
                //    {
                //        item["VincularCte"].Visible = true;
                //    }
                //    else
                //    {
                //        item["Editar"].Visible = true;
                //        item["sustituir"].Visible = true;
                //        item["VincularSuc"].Visible = true;
                //        item["Cancelar"].Visible = true;


                //    }
                //}
            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgNoUtilizado_ItemDataBound(object source, GridItemEventArgs e)
        {
            try
            {

                //if (e.Item is GridDataItem)
                //{
                //    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                //    Convenio conv = (Convenio)Session["Convenio" + Session.SessionID];
                //    GridDataItem item = (GridDataItem)e.Item;
                //    if (conv.Pue_Admin1 != sesion.Id_U && conv.Pue_Admin2 != sesion.Id_U  && sesion.Id_U != 1)
                //    {
                //        item["VincularCte"].Visible = true;
                //    }
                //    else
                //    {
                //        item["Editar"].Visible = true;
                //        item["sustituir"].Visible = true;
                //        item["VincularSuc"].Visible = true;
                //        item["Cancelar"].Visible = true;


                //    }

                //}

                //if (e.Item is GridHeaderItem)
                //{
                //    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                //    Convenio conv = (Convenio)Session["Convenio" + Session.SessionID];
                //    GridHeaderItem item = (GridHeaderItem)e.Item;
                //    if (conv.Pue_Admin1 != sesion.Id_U && conv.Pue_Admin2 != sesion.Id_U && sesion.Id_U != 1)
                //    {
                //        item["VincularCte"].Visible = true;
                //    }
                //    else
                //    {
                //        item["Editar"].Visible = true;
                //        item["sustituir"].Visible = true;
                //        item["VincularSuc"].Visible = true;
                //        item["Cancelar"].Visible = true;


                //    }
                //}
            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        #endregion Eventos
        #region Funciones
        private void ValidarPermisos()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Pagina pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                if (pag.Length > 1)
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                else
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;
                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, Sesion.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;

                Permiso Permiso = new Permiso();
                Permiso.Id_U = Sesion.Id_U;
                Permiso.Id_Cd = Sesion.Id_Cd;
                Permiso.Sm_cve = pagina.Clave;

                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, Sesion.Emp_Cnx);

                if (Permiso.PAccesar == true)
                    _PermisoImprimir = Permiso.PImprimir;
            }
            catch (Exception ex)
            {
                throw ex;
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
                    CN_Comun.LlenaCombo(2, Sesion.Id_Emp, Sesion.Id_U, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.Visible = false;
                    this.TblEncabezado.Rows[0].Cells[2].InnerText = " " + CmbCentro.FindItemByValue(Sesion.Id_Cd_Ver.ToString()).Text;
                }
                else
                {
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, sesion.Id_Cd_Ver, sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = Sesion.Id_Cd_Ver.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarCategoria()
        {
            try
            {
                CN__Comun cn_comun = new CN__Comun();
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];

                cn_comun.LlenaCombo(Conexion, "spCatConvCategoria_Combo", ref CmbCategoria);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private List<Convenio> ListUtilizados()
        {
            try
            {
                Convenio conv = new Convenio();
                CN_Convenio cn_conv = new CN_Convenio();
                List<Convenio> ListUtil = new List<Convenio>();
                List<Convenio> ListNoUtil = new List<Convenio>();
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                conv.Filtro_TipoFiltro = int.Parse(this.CmbTipoB.SelectedValue);
                conv.Filtro_Vencido = int.Parse(this.CmbVencido.SelectedValue);
                conv.Filtro_Id_Cat = int.Parse(this.CmbCategoria.SelectedValue);
                conv.Filtro_Valor = TxtValorCategoria.Text.Trim() == "" ? "-1" : TxtValorCategoria.Text.Trim();
                conv.Filtro_Id_Cd = sesion.Id_Cd_Ver;

                conv.Filtro_Id_Cd = sesion.Id_Cd_Ver;
                cn_conv.ConsultaListaConvenios(conv, ref ListUtil, ref ListNoUtil, Conexion);

                return ListUtil;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        private List<Convenio> ListNoUtilizados()
        {
            try
            {
                Convenio conv = new Convenio();
                CN_Convenio cn_conv = new CN_Convenio();
                List<Convenio> ListUtil = new List<Convenio>();
                List<Convenio> ListNoUtil = new List<Convenio>();
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                conv.Filtro_TipoFiltro = int.Parse(this.CmbTipoB.SelectedValue);
                conv.Filtro_Vencido = int.Parse(this.CmbVencido.SelectedValue);
                conv.Filtro_Id_Cat = int.Parse(this.CmbCategoria.SelectedValue);
                conv.Filtro_Valor = TxtValorCategoria.Text.Trim() == "" ? "-1" : TxtValorCategoria.Text.Trim();
                conv.Filtro_Id_Cd = sesion.Id_Cd_Ver;

                cn_conv.ConsultaListaConvenios(conv, ref ListUtil, ref ListNoUtil, Conexion);

                return ListNoUtil;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        //private void CargarPermisosEspeciales()
        //{
        //    try
        //    {
        //        Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
        //        Convenio conv = new Convenio();
        //        CN_Convenio cn_conv = new CN_Convenio();

        //        cn_conv.ConsultaUsuariosEspeciales(ref conv, sesion.Emp_Cnx);

        //        convenio = conv;

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }

        //}
        private void Imprimir(int Id_PC)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ArrayList ALValorParametrosInternos = new ArrayList();

                ALValorParametrosInternos.Add(sesion.Emp_Cnx);
                ALValorParametrosInternos.Add(Id_PC);


                Type instance = null;

                instance = typeof(LibreriaReportes.RepPrecioConvenio);


                Session["InternParameter_Values" + Session.SessionID + HF_Cve.Value] = ALValorParametrosInternos;
                Session["assembly" + Session.SessionID + HF_Cve.Value] = instance.AssemblyQualifiedName;
                RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_Cve.Value + "');");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void Imprimir()
        {
            try
            {
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Convenio conv = (Convenio)Session["Convenio" + Session.SessionID];
                ArrayList ALValorParametrosInternos = new ArrayList();

                ALValorParametrosInternos.Add(Conexion);
                ALValorParametrosInternos.Add(this.CmbTipoB.SelectedValue);
                ALValorParametrosInternos.Add(this.CmbVencido.SelectedValue);
                ALValorParametrosInternos.Add(this.CmbCategoria.SelectedValue);
                ALValorParametrosInternos.Add(TxtValorCategoria.Text.Trim() == "" ? "0" : TxtValorCategoria.Text.Trim());
                if (conv.Pue_Admin1 == sesion.Id_U || conv.Pue_Admin2 == sesion.Id_U)
                {
                    ALValorParametrosInternos.Add(-1);
                }
                else
                {
                    ALValorParametrosInternos.Add(sesion.Id_Cd_Ver);
                }

                Type instance = null;

                instance = typeof(LibreriaReportes.RepPrecioConvenioListaDet);


                Session["InternParameter_Values" + Session.SessionID + HF_Cve.Value] = ALValorParametrosInternos;
                Session["assembly" + Session.SessionID + HF_Cve.Value] = instance.AssemblyQualifiedName;
                RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_Cve.Value + "');");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void ExportarClientes()
        {
            try
            {

                StringBuilder tabla = new StringBuilder();
                tabla.Append("<html><head><meta http-equiv='Content-Type' content='text/html; charset=ISO-8859-1'></head><body><table style='width:700px'>");
                EscribeDetalleCtes(ref tabla);
                tabla.Append("</table></body></html>");
                CN__Comun cn_comun = new CN__Comun();
                cn_comun.ExportarExcel("ClientesVinculados", tabla.ToString());

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void EscribeDetalleCtes(ref StringBuilder Tabla)
        {
            try
            {
                String width;

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_Convenio cn_conv = new CN_Convenio();
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
                DataTable dt = null;

                cn_conv.Convenion_ConsultaVinculadosTodos(sesion.Id_Cd_Ver, ref dt, Conexion);

                Tabla.Append("<tr>");

                //lectura y escritura de columnas
                for (int i = 0; i < dt.Columns.Count; i++)
                {

                    if (dt.Columns[i].ColumnName == "Id_PC")
                    {
                        width = (i == 0) ? "70px" : "90px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Convenio Key");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Pc_NoConvenio")
                    {
                        width = (i == 0) ? "70px" : "90px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Convenio proveedor");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Pc_Nombre")
                    {
                        width = (i == 0) ? "150px" : "180px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Nombre de convenio");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "desc_Estatus")
                    {
                        width = (i == 0) ? "150px" : "180px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Estatus");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Cat_DescCorta")
                    {
                        width = (i == 0) ? "50px" : "70px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Categoría");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Cat_Nombre")
                    {
                        width = (i == 0) ? "120px" : "150px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Nombre de categoría");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Id_Cte")
                    {
                        width = (i == 0) ? "50px" : "70px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Núm. Cte.");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Sol_CteNombre")
                    {
                        width = (i == 0) ? "150px" : "180px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Nombre de cliente");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Id_Cd")
                    {
                        width = (i == 0) ? "50px" : "70px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("No. de concesionario");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Cd_Nombre")
                    {
                        width = (i == 0) ? "150px" : "180px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Nombre de concesionario");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "OrigenConvenioString")
                    {
                        width = (i == 0) ? "150px" : "180px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Origen de vinculación");
                        Tabla.Append("</th>");
                    }

                }
                Tabla.Append("</tr>");
                // lectura y escritura de filas
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    Tabla.Append("<tr>");
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {

                        if (dt.Columns[i].ColumnName == "Id_PC")
                        {
                            Tabla.Append("<td   style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Pc_NoConvenio")
                        {
                            Tabla.Append("<td   style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Pc_Nombre")
                        {
                            Tabla.Append("<td   style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "desc_Estatus")
                        {
                            Tabla.Append("<td   style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Cat_DescCorta")
                        {
                            Tabla.Append("<td   style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Cat_Nombre")
                        {
                            Tabla.Append("<td   style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Id_Cte")
                        {
                            Tabla.Append("<td   style='text-align:right'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Sol_CteNombre")
                        {
                            Tabla.Append("<td   style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }

                        else if (dt.Columns[i].ColumnName == "Id_Cd")
                        {
                            Tabla.Append("<td   style='text-align:right'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Cd_Nombre")
                        {
                            Tabla.Append("<td   style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "OrigenConvenioString")
                        {
                            Tabla.Append("<td   style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }


                    }
                }
                Tabla.Append("</tr>");
                Tabla.Append("<tr>");
                Tabla.Append("<td>");
                Tabla.Append("&nbsp; &nbsp;</td>");
                Tabla.Append("</tr>");



            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        private void ExportarConvenios()
        {
            try
            {

                StringBuilder tabla = new StringBuilder();
                tabla.Append("<html><head><meta http-equiv='Content-Type' content='text/html; charset=ISO-8859-1'></head><body><table style='width:700px'>");
                EscribeDetalleConv(ref tabla);
                tabla.Append("</table></body></html>");
                CN__Comun cn_comun = new CN__Comun();
                cn_comun.ExportarExcel("ListadoConvenios", tabla.ToString());

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void EscribeDetalleConv(ref StringBuilder Tabla)
        {
            try
            {
                String width;

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_Convenio cn_conv = new CN_Convenio();
                Convenio conv = new Convenio();
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
                DataTable dt = null;

                conv.Filtro_TipoFiltro = -1;
                conv.Filtro_Vencido = -1;
                conv.Filtro_Id_Cat = -1;
                conv.Filtro_Valor = "-1";
                conv.Filtro_Id_Cd = sesion.Id_Cd_Ver;

                cn_conv.Convenio_ConsultaListaDet(conv, ref dt, Conexion);

                Tabla.Append("<tr>");


                //lectura y escritura de columnas
                for (int i = 0; i < dt.Columns.Count; i++)
                {

                    if (dt.Columns[i].ColumnName == "Id_Pc")
                    {
                        width = (i == 0) ? "100px" : "120px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Convenio Key");
                        Tabla.Append("</th>");
                    }
                    else
                        if (dt.Columns[i].ColumnName == "PC_NoConvenio")
                        {
                            width = (i == 0) ? "100px" : "120px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("Convenio proveedor");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "PC_Nombre")
                        {
                            width = (i == 0) ? "180px" : "210px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("Nombre de convenio");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "Cat_DescCorta")
                        {
                            width = (i == 0) ? "50px" : "70px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("Categoría");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "Cat_Nombre")
                        {
                            width = (i == 0) ? "120px" : "150px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("Nombre de categoría");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "Estatus")
                        {
                            width = (i == 0) ? "50px" : "70px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("Estatus");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "Id_Prd")
                        {
                            width = (i == 0) ? "50px" : "70px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("Clave Key");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "PCD_ClaveProv")
                        {
                            width = (i == 0) ? "50px" : "70px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("Clave prov.");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "Prd_Descripcion")
                        {
                            width = (i == 0) ? "350px" : "380px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("Descripción de Producto");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "PCD_PrecioVtaMin")
                        {
                            width = (i == 0) ? "70px" : "90px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("P. Venta Min.");
                            Tabla.Append("</th>");
                        }

                        else if (dt.Columns[i].ColumnName == "PCD_PrecioVtaMax")
                        {
                            width = (i == 0) ? "70px" : "90px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("P. Venta Max.");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "PCD_PrecioAAAEspA")
                        {
                            width = (i == 0) ? "110px" : "130px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("<b> Anterior / PAAA Esp.");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "PCD_FechaInicioA")
                        {
                            width = (i == 0) ? "110px" : "130px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("<b> Anterior / Fecha inicio");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "PCD_FechaFinA")
                        {
                            width = (i == 0) ? "100px" : "120px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("<b> Anterior / Fecha fin");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "PCD_PrecioAAAEsp")
                        {
                            width = (i == 0) ? "90px" : "110px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("<b> Actual / PAAA Esp. ");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "PCD_FechaInicio")
                        {
                            width = (i == 0) ? "100px" : "120px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("<b> Actual / Fecha inicio ");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "PCD_FechaFin")
                        {
                            width = (i == 0) ? "110px" : "130px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("<b> Actual / Fecha fin");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "PCD_PrecioAAAEspC")
                        {
                            width = (i == 0) ? "100px" : "120px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("<b> Futuro / PAAA Esp. ");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "PCD_FechaInicioC")
                        {
                            width = (i == 0) ? "110px" : "130px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("<b> Futuro / Fecha inicio ");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "PCD_FechaFinC")
                        {
                            width = (i == 0) ? "90px" : "110px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("<b> Futuro / Fecha fin");
                            Tabla.Append("</th>");
                        }
                        //else if (dt.Columns[i].ColumnName == "PCD_PrecioPAAAEspProv")
                        //{
                        //    width = (i == 0) ? "110px" : "130px";
                        //    Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        //    Tabla.Append("Costo especial ");
                        //    Tabla.Append("</th>");
                        //}
                        else if (dt.Columns[i].ColumnName == "PCD_PrecioVentaConvenio")
                        {
                            width = (i == 0) ? "90px" : "110px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("P.Venta Convenio");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "PCD_Margen")
                        {
                            width = (i == 0) ? "90px" : "110px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("Margen ");
                            Tabla.Append("</th>");
                        }
                }
                Tabla.Append("</tr>");
                // lectura y escritura de filas
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    Tabla.Append("<tr>");
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {

                        if (dt.Columns[i].ColumnName == "Id_Pc")
                        {
                            Tabla.Append("<td   style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else
                            if (dt.Columns[i].ColumnName == "PC_NoConvenio")
                            {
                                Tabla.Append("<td   style='text-align:left'>");
                                Tabla.Append(dt.Rows[j][i].ToString());
                                Tabla.Append("</td>");
                            }
                            else if (dt.Columns[i].ColumnName == "PC_Nombre")
                            {
                                Tabla.Append("<td   style='text-align:left'>");
                                Tabla.Append(dt.Rows[j][i].ToString());
                                Tabla.Append("</td>");
                            }
                            else if (dt.Columns[i].ColumnName == "Cat_DescCorta")
                            {
                                Tabla.Append("<td   style='text-align:center'>");
                                Tabla.Append(dt.Rows[j][i].ToString());
                                Tabla.Append("</td>");
                            }
                            else if (dt.Columns[i].ColumnName == "Cat_Nombre")
                            {
                                Tabla.Append("<td   style='text-align:left'>");
                                Tabla.Append(dt.Rows[j][i].ToString());
                                Tabla.Append("</td>");
                            }
                            else if (dt.Columns[i].ColumnName == "Estatus")
                            {
                                Tabla.Append("<td   style='text-align:center'>");
                                Tabla.Append(dt.Rows[j][i].ToString());
                                Tabla.Append("</td>");
                            }
                            else if (dt.Columns[i].ColumnName == "Id_Prd")
                            {
                                Tabla.Append("<td   style='text-align:right'>");
                                Tabla.Append(dt.Rows[j][i].ToString());
                                Tabla.Append("</td>");
                            }
                            else if (dt.Columns[i].ColumnName == "PCD_ClaveProv")
                            {
                                Tabla.Append("<td   style='text-align:right'>");
                                Tabla.Append(dt.Rows[j][i].ToString());
                                Tabla.Append("</td>");
                            }
                            else if (dt.Columns[i].ColumnName == "Prd_Descripcion")
                            {
                                Tabla.Append("<td   style='text-align:left'>");
                                Tabla.Append(dt.Rows[j][i].ToString());
                                Tabla.Append("</td>");
                            }

                            else if (dt.Columns[i].ColumnName == "PCD_PrecioVtaMin")
                            {
                                if (dt.Rows[j][i].ToString() != "")
                                {
                                    double valor = double.Parse(dt.Rows[j][i].ToString());
                                    Tabla.Append("<td   style='text-align:right'>");
                                    Tabla.Append(valor.ToString("C2"));
                                    Tabla.Append("</td>");
                                }
                                else
                                {
                                    Tabla.Append("<td   style='text-align:right'>");
                                    Tabla.Append(dt.Rows[j][i].ToString());
                                    Tabla.Append("</td>");
                                }
                            }
                            else if (dt.Columns[i].ColumnName == "PCD_PrecioVtaMax")
                            {
                                if (dt.Rows[j][i].ToString() != "")
                                {
                                    double valor = double.Parse(dt.Rows[j][i].ToString());
                                    Tabla.Append("<td   style='text-align:right'>");
                                    Tabla.Append(valor.ToString("C2"));
                                    Tabla.Append("</td>");
                                }
                                else
                                {
                                    Tabla.Append("<td   style='text-align:right'>");
                                    Tabla.Append(dt.Rows[j][i].ToString());
                                    Tabla.Append("</td>");
                                }
                            }


                            else if (dt.Columns[i].ColumnName == "PCD_PrecioAAAEspA")
                            {
                                if (dt.Rows[j][i].ToString() != "")
                                {
                                    double valor = double.Parse(dt.Rows[j][i].ToString());
                                    Tabla.Append("<td   style='text-align:right'>");
                                    Tabla.Append(valor.ToString("C2"));
                                    Tabla.Append("</td>");
                                }
                                else
                                {
                                    Tabla.Append("<td   style='text-align:right'>");
                                    Tabla.Append(dt.Rows[j][i].ToString());
                                    Tabla.Append("</td>");
                                }
                            }
                            else if (dt.Columns[i].ColumnName == "PCD_FechaInicioA")
                            {
                                if (dt.Rows[j][i].ToString() != "")
                                {
                                    DateTime valor = DateTime.Parse(dt.Rows[j][i].ToString());
                                    Tabla.Append("<td   style='text-align:center'>");
                                    Tabla.Append(valor.ToShortDateString());
                                    Tabla.Append("</td>");
                                }
                                else
                                {
                                    Tabla.Append("<td   style='text-align:center'>");
                                    Tabla.Append(dt.Rows[j][i].ToString());
                                    Tabla.Append("</td>");
                                }

                            }
                            else if (dt.Columns[i].ColumnName == "PCD_FechaFinA")
                            {
                                if (dt.Rows[j][i].ToString() != "")
                                {
                                    DateTime valor = DateTime.Parse(dt.Rows[j][i].ToString());
                                    Tabla.Append("<td   style='text-align:center'>");
                                    Tabla.Append(valor.ToShortDateString());
                                    Tabla.Append("</td>");
                                }
                                else
                                {
                                    Tabla.Append("<td   style='text-align:center'>");
                                    Tabla.Append(dt.Rows[j][i].ToString());
                                    Tabla.Append("</td>");
                                }

                            }
                            else if (dt.Columns[i].ColumnName == "PCD_PrecioAAAEsp")
                            {
                                if (dt.Rows[j][i].ToString() != "")
                                {
                                    double valor = double.Parse(dt.Rows[j][i].ToString());
                                    Tabla.Append("<td   style='text-align:right'>");
                                    Tabla.Append(valor.ToString("C2"));
                                    Tabla.Append("</td>");
                                }
                                else
                                {
                                    Tabla.Append("<td   style='text-align:right'>");
                                    Tabla.Append(dt.Rows[j][i].ToString());
                                    Tabla.Append("</td>");
                                }
                            }
                            else if (dt.Columns[i].ColumnName == "PCD_FechaInicio")
                            {
                                if (dt.Rows[j][i].ToString() != "")
                                {
                                    DateTime valor = DateTime.Parse(dt.Rows[j][i].ToString());
                                    Tabla.Append("<td   style='text-align:center'>");
                                    Tabla.Append(valor.ToShortDateString());
                                    Tabla.Append("</td>");
                                }
                                else
                                {
                                    Tabla.Append("<td   style='text-align:center'>");
                                    Tabla.Append(dt.Rows[j][i].ToString());
                                    Tabla.Append("</td>");
                                }

                            }
                            else if (dt.Columns[i].ColumnName == "PCD_FechaFin")
                            {
                                if (dt.Rows[j][i].ToString() != "")
                                {
                                    DateTime valor = DateTime.Parse(dt.Rows[j][i].ToString());
                                    Tabla.Append("<td   style='text-align:center'>");
                                    Tabla.Append(valor.ToShortDateString());
                                    Tabla.Append("</td>");
                                }
                                else
                                {
                                    Tabla.Append("<td   style='text-align:center'>");
                                    Tabla.Append(dt.Rows[j][i].ToString());
                                    Tabla.Append("</td>");
                                }

                            }

                            else if (dt.Columns[i].ColumnName == "PCD_PrecioAAAEspC")
                            {
                                if (dt.Rows[j][i].ToString() != "")
                                {
                                    double valor = double.Parse(dt.Rows[j][i].ToString());
                                    Tabla.Append("<td   style='text-align:right'>");
                                    Tabla.Append(valor.ToString("C2"));
                                    Tabla.Append("</td>");
                                }
                                else
                                {
                                    Tabla.Append("<td   style='text-align:right'>");
                                    Tabla.Append(dt.Rows[j][i].ToString());
                                    Tabla.Append("</td>");
                                }
                            }
                            else if (dt.Columns[i].ColumnName == "PCD_FechaInicioC")
                            {
                                if (dt.Rows[j][i].ToString() != "")
                                {
                                    DateTime valor = DateTime.Parse(dt.Rows[j][i].ToString());
                                    Tabla.Append("<td   style='text-align:center'>");
                                    Tabla.Append(valor.ToShortDateString());
                                    Tabla.Append("</td>");
                                }
                                else
                                {
                                    Tabla.Append("<td   style='text-align:center'>");
                                    Tabla.Append(dt.Rows[j][i].ToString());
                                    Tabla.Append("</td>");
                                }

                            }
                            else if (dt.Columns[i].ColumnName == "PCD_FechaFinC")
                            {
                                if (dt.Rows[j][i].ToString() != "")
                                {
                                    DateTime valor = DateTime.Parse(dt.Rows[j][i].ToString());
                                    Tabla.Append("<td   style='text-align:center'>");
                                    Tabla.Append(valor.ToShortDateString());
                                    Tabla.Append("</td>");
                                }
                                else
                                {
                                    Tabla.Append("<td   style='text-align:center'>");
                                    Tabla.Append(dt.Rows[j][i].ToString());
                                    Tabla.Append("</td>");
                                }

                            }

                            //else if (dt.Columns[i].ColumnName == "PCD_PrecioPAAAEspProv")
                            //{
                            //    if (dt.Rows[j][i].ToString() != "")
                            //    {
                            //        double valor = double.Parse(dt.Rows[j][i].ToString());
                            //        Tabla.Append("<td   style='text-align:right'>");
                            //        Tabla.Append(valor.ToString("C2"));
                            //        Tabla.Append("</td>");
                            //    }
                            //    else
                            //    {
                            //        Tabla.Append("<td   style='text-align:right'>");
                            //        Tabla.Append(dt.Rows[j][i].ToString());
                            //        Tabla.Append("</td>");
                            //    }
                            //}
                            else if (dt.Columns[i].ColumnName == "PCD_PrecioVentaConvenio")
                            {
                                if (dt.Rows[j][i].ToString() != "")
                                {
                                    double valor = double.Parse(dt.Rows[j][i].ToString());
                                    Tabla.Append("<td   style='text-align:right'>");
                                    Tabla.Append(valor.ToString("C2"));
                                    Tabla.Append("</td>");
                                }
                                else
                                {
                                    Tabla.Append("<td   style='text-align:right'>");
                                    Tabla.Append(dt.Rows[j][i].ToString());
                                    Tabla.Append("</td>");
                                }
                            }
                            else if (dt.Columns[i].ColumnName == "PCD_Margen")
                            {
                                if (dt.Rows[j][i].ToString() != "")
                                {
                                    double valor = double.Parse(dt.Rows[j][i].ToString());
                                    Tabla.Append("<td   style='text-align:right'>");
                                    Tabla.Append(valor.ToString("P"));
                                    Tabla.Append("</td>");
                                }
                                else
                                {
                                    Tabla.Append("<td   style='text-align:right'>");
                                    Tabla.Append(dt.Rows[j][i].ToString());
                                    Tabla.Append("</td>");
                                }
                            }





                    }
                }
                Tabla.Append("</tr>");
                Tabla.Append("<tr>");
                Tabla.Append("<td>");
                Tabla.Append("&nbsp; &nbsp;</td>");
                Tabla.Append("</tr>");



            }
            catch (Exception ex)
            {

                throw ex;
            }

        }



        private void Exportarconcesionarioshabilitados()
        {
            //011 exporta a excell los concecionarios habilitados
            try
            {

                StringBuilder tabla = new StringBuilder();
                tabla.Append("<html><head><meta http-equiv='Content-Type' content='text/html; charset=ISO-8859-1'></head><body><table style='width:700px'>");
                EscribeDetalleConcesionarios(ref tabla);
                tabla.Append("</table></body></html>");
                CN__Comun cn_comun = new CN__Comun();
                cn_comun.ExportarExcel("ListadoConsecionarios", tabla.ToString());

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void EscribeDetalleConcesionarios(ref StringBuilder Tabla)
        {

            //escrice el detalle de los consecionarios habilitados 
            try
            {
                String width;

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_Convenio cn_conv = new CN_Convenio();
                Convenio conv = new Convenio();
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
                DataTable dt = null;

                conv.Filtro_TipoFiltro = -1;
                conv.Filtro_Vencido = -1;
                conv.Filtro_Id_Cat = -1;
                conv.Filtro_Valor = "-1";
                conv.Filtro_Id_Cd = -1;

                cn_conv.Convenio_Concesionarioshabilitados(conv, ref dt, Conexion);


                Tabla.Append("<tr><td><div style ='color:Blue' >KEY QUIMICA SA DE CV </div></td><td>/ Concesionarios habilitados por convenio ");

                Tabla.Append("</td></tr>");

                Tabla.Append("<tr>");


                //lectura y escritura de columnas
                for (int i = 0; i < dt.Columns.Count; i++)
                {

                    if (dt.Columns[i].ColumnName == "Id_PC")
                    {
                        width = (i == 0) ? "120px" : "90px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Convenio Key");
                        Tabla.Append("</th>");
                    }
                    else
                        if (dt.Columns[i].ColumnName == "Pc_NoConvenio")
                        {
                            width = (i == 0) ? "120px" : "90px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("Convenio proveedor");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "PC_Nombre")
                        {
                            width = (i == 0) ? "180px" : "210px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("Nombre de convenio");
                            Tabla.Append("</th>");
                        }
                        //else if (dt.Columns[i].ColumnName == "Cat_DescCorta")
                        //{
                        //    width = (i == 0) ? "50px" : "70px";
                        //    Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        //    Tabla.Append("Categoría");
                        //    Tabla.Append("</th>");
                        //}
                        else if (dt.Columns[i].ColumnName == "Cat_Nombre")
                        {
                            width = (i == 0) ? "120px" : "150px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("Categoría");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "Estatus")
                        {
                            width = (i == 0) ? "50px" : "70px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("Estatus");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "Cd_Nombre")
                        {
                            width = (i == 0) ? "120px" : "150px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("Concesionario");
                            Tabla.Append("</th>");
                        }

                        else if (dt.Columns[i].ColumnName == "PCD_Ver")
                        {
                            width = (i == 0) ? "50px" : "70px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("Ver");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "PCD_Usar")
                        {
                            width = (i == 0) ? "50px" : "70px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("Usar");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "PCD_Gral")
                        {
                            width = (i == 0) ? "80px" : "70px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("General");
                            Tabla.Append("</th>");
                        }

                }
                Tabla.Append("</tr>");
                // lectura y escritura de filas
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    Tabla.Append("<tr>");
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {

                        if (dt.Columns[i].ColumnName == "Id_PC")
                        {
                            Tabla.Append("<td   style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else
                            if (dt.Columns[i].ColumnName == "Pc_NoConvenio")
                            {
                                Tabla.Append("<td   style='text-align:left'>");
                                Tabla.Append(dt.Rows[j][i].ToString());
                                Tabla.Append("</td>");
                            }
                            else if (dt.Columns[i].ColumnName == "PC_Nombre")
                            {
                                Tabla.Append("<td   style='text-align:left'>");
                                Tabla.Append(dt.Rows[j][i].ToString());
                                Tabla.Append("</td>");
                            }
                            //else if (dt.Columns[i].ColumnName == "Cat_DescCorta")
                            //{
                            //    Tabla.Append("<td   style='text-align:center'>");
                            //    Tabla.Append(dt.Rows[j][i].ToString());
                            //    Tabla.Append("</td>");
                            //}
                            else if (dt.Columns[i].ColumnName == "Cat_Nombre")
                            {
                                Tabla.Append("<td   style='text-align:left'>");
                                Tabla.Append(dt.Rows[j][i].ToString());
                                Tabla.Append("</td>");
                            }
                            else if (dt.Columns[i].ColumnName == "Estatus")
                            {
                                Tabla.Append("<td   style='text-align:center'>");
                                Tabla.Append(dt.Rows[j][i].ToString());
                                Tabla.Append("</td>");
                            }
                            else if (dt.Columns[i].ColumnName == "Cd_Nombre")
                            {
                                Tabla.Append("<td   style='text-align:left'>");
                                Tabla.Append(dt.Rows[j][i].ToString());
                                Tabla.Append("</td>");
                            }


                            else if (dt.Columns[i].ColumnName == "PCD_ClaveProv")
                            {
                                Tabla.Append("<td   style='text-align:right'>");
                                Tabla.Append(dt.Rows[j][i].ToString());
                                Tabla.Append("</td>");
                            }
                            else if (dt.Columns[i].ColumnName == "Prd_Descripcion")
                            {
                                Tabla.Append("<td   style='text-align:left'>");
                                Tabla.Append(dt.Rows[j][i].ToString());
                                Tabla.Append("</td>");
                            }
                            else if (dt.Columns[i].ColumnName == "PCD_Ver")
                            {
                                Tabla.Append("<td   style='text-align:center'>");
                                Tabla.Append(dt.Rows[j][i].ToString());
                                Tabla.Append("</td>");
                            }
                            else if (dt.Columns[i].ColumnName == "PCD_Usar")
                            {
                                Tabla.Append("<td   style='text-align:center'>");
                                Tabla.Append(dt.Rows[j][i].ToString());
                                Tabla.Append("</td>");
                            }
                            else if (dt.Columns[i].ColumnName == "PCD_Gral")
                            {
                                Tabla.Append("<td   style='text-align:center'>");
                                Tabla.Append(dt.Rows[j][i].ToString());
                                Tabla.Append("</td>");
                            }

                    }
                }
                Tabla.Append("</tr>");
                Tabla.Append("<tr>");
                Tabla.Append("<td>");
                Tabla.Append("&nbsp; &nbsp;</td>");
                Tabla.Append("</tr>");



            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private string LeerSolicitudespendientes()
        {
            try
            {
                ///Leer cuantas solicitudes están pendientes de autorizarse y mostrarlas en el toolbar
                ///Estatus es pendiente 
                List<SolConvenio> List = new List<SolConvenio>();
                CN_SolConvenio cn_conv = new CN_SolConvenio();
                SolConvenio conv = new SolConvenio();
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Convenio c = (Convenio)Session["Convenio" + Session.SessionID];

                conv.FiltroSol_Estatus = "P";

                //JFCV TODO que traiga la información en base al tipo de consulta 

                conv.Filtro_TipoFiltro = -1;

                conv.Filtro_Valor = "-1";
                conv.Filtro_Id_Cat = "-1";



                //if (c.Pue_Admin1 == sesion.Id_U || c.Pue_Admin2 == sesion.Id_U)
                //{
                //    conv.FiltroId_CD = (int?)null;

                //}
                //else
                //{
                conv.FiltroId_CD = sesion.Id_Cd_Ver;
                //}
                cn_conv.ProPrecioConv_SolicitudLista(conv, ref List, Conexion);

                return List.Count().ToString();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        #endregion Funciones
        #region ErrorManager

        private void Alerta(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }

        private void Alerta2(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 600, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
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

        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta("Error al cargar la página");
            else
                if (mensaje.Contains("Impresion_error"))
                    Alerta("Error al momento de imprimir");
                else
                    Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
        }
        #endregion
    }
}