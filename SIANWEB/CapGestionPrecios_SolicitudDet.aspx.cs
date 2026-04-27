using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaEntidad;
using Telerik.Web.UI;
using CapaNegocios;
using System.Text;
using System.Configuration;

namespace SIANWEB
{
    public partial class CapGestionPrecios_SolicitudDet : System.Web.UI.Page
    {
        #region Variables
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private List<SolConvenioDet> ListDet
        {
            get { return (List<SolConvenioDet>)Session["ListDet" + Session.SessionID]; }
            set { Session["ListDet" + Session.SessionID] = value; }
        }
        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
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
         
                        this.CargarCentros();
                        ListDet = null;
                        ListDet = new List<SolConvenioDet>();
                        HFId_Sol.Value = Page.Request.QueryString["Id_Sol"].ToString();

                        if (HFId_Sol.Value == "0")
                        {
                            HFId_PC.Value = Page.Request.QueryString["Id_PC"].ToString();
                            HFCapUsuario.Value = ObtenerCapturaUsuario(Page.Request.QueryString["Id_CatStr"].ToString());

                            LblPC_NoConvenioKey.Text = Page.Request.QueryString["Id_PC"].ToString();
                            LblPC_NoConvenio.Text = Page.Request.QueryString["PC_NoConvenio"].ToString();
                            LblPC_Nombre.Text = Page.Request.QueryString["PC_Nombre"].ToString();
                            LblId_CatStr.Text = Page.Request.QueryString["Id_CatStr"].ToString();
                            rgSolicitudDet.Rebind();
                            LblCd_Nombre.Text = Sesion.Id_Cd_Ver.ToString() + " - " + Sesion.Cd_Nombre;
                            LblU_Nombre.Text = Sesion.U_Nombre;
                            LblU_Correo.Text = Sesion.U_Correo;
                            LblSol_Fecha.Text = DateTime.Today.ToShortDateString();
                            LblId_Sol.Text = ObtenerConsecutivo().ToString();
                        }
                        else
                        {
                            LblId_Sol.Text = HFId_Sol.Value;
                            ConsultaEncabezado();
                            ConsultaDetalle();
 
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void CmbCentro_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion(); sesion = (Sesion)Session["Sesion" + Session.SessionID]; if (sesion == null) { string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries); Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false); } CN__Comun comun = new CN__Comun(); comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                //this.Nuevo();
       
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
            
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;

                    if (btn.CommandName == "save")
                    {
                        if (HFId_Sol.Value == "0")
                        {
                            Guardar();
                        }
                        else
                        {
                            Modificar();
                        }
                    }

                
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgSolicitudDet_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                   
                    rgSolicitudDet.DataSource = ListDet;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgSolicitudDet_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":
                        break;
                    case "PerformInsert":
                        Nuevo(e);
                        break;
                    case "Update":
                        Modificar(e);
                        break;
                    case "Delete":
                        Borrar(e);
                        break;
                }
  
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgSolicitudDet_InsertCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                SolConvenioDet c = new SolConvenioDet();

             
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgSolicitudDet_UpdateCommand(object source, GridCommandEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgSolicitudDet_DeleteCommand(object source, GridCommandEventArgs e)
        {
            try
            {
            //    GridEditableItem editedItem = e.Item as GridEditableItem;
            //    EntradaSalidaDetalle Es_Det = new EntradaSalidaDetalle();
            //    Es_Det.Id_EsDetStr = (editedItem["Id_EsDetStr"].FindControl("lblDet_Item") as Label).Text;
            //    list_Es.Remove(list_Es.Where(EntradaSalidaDetalle => EntradaSalidaDetalle.Id_EsDetStr == Es_Det.Id_EsDetStr).ToList()[0]);

            //    rgEntradaSalida.Rebind();
            //    CalcularTotales();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgSolicitudDet_ItemCreated(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridEditableItem && e.Item.IsInEditMode)
                {
                    GridEditableItem item = e.Item as GridEditableItem;
                    if (HFCapUsuario.Value == "False")
                    {
                        //(item.FindControl("TxtSol_UsuFinal") as RadNumericTextBox).Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
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
                   
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }
        protected void TxtId_Cte_TextChanged(object sender, EventArgs e)
        {
            try
            {
                RadNumericTextBox TxtIdCte = (RadNumericTextBox)sender;
                RadTextBox TxtCteNom = (RadTextBox)TxtIdCte.Parent.FindControl("TxtSol_CteNombre");
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatCliente cn_cte = new CN_CatCliente();
                string Cte_NomComercial = null;
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();

                if (TxtIdCte.Text != "")
                {
                    int cliente = int.Parse(TxtIdCte.Text);

                    cn_cte.ClienteConsultaNombre(cliente, ref Cte_NomComercial, sesion);

                    if (Cte_NomComercial == null)
                    {
                        Alertavalida("El cliente no existe");
                        TxtIdCte.Focus();
                        return;
                    }
                    TxtCteNom.Text = Cte_NomComercial;

                }
                else
                {
                    TxtIdCte.Focus();
                }
 
            }
            catch (Exception ex)
            {
                
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
 
        #endregion

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
                //Esta clave depende de la pantalla

                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, Sesion.Emp_Cnx);

                if (Permiso.PAccesar == true)
                {
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;

                    if (Permiso.PGrabar == false)
                        this.rtb1.Items[1].Visible = false;
                }
                else
                    Response.Redirect("Inicio.aspx");


                //if (ConsultarAutorizacionPrecio() == "True")
                //{

                //    this.rg1.Columns[11].Visible = true ;
                //}

                //else
                //{
                //    this.rg1.Columns[11].Visible = false;
                
                //}
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
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, Sesion.Id_Cd_Ver, Sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = Sesion.Id_Cd_Ver.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Nuevo(GridCommandEventArgs e)
        {
            try
            {
                GridItem gi = e.Item;

                if (HFCapUsuario.Value == "False")
                {
                
                    if (((RadNumericTextBox)gi.FindControl("TxtId_Cte")).Text == "" ||
                        ((RadTextBox)gi.FindControl("TxtSol_CteNombre")).Text == "")
                    {
                        Alertavalida("Todos los campos son requeridos");
                        e.Canceled = true;
                        return;
                    }
                }
                else
                {
                    if (((RadNumericTextBox)gi.FindControl("TxtId_Cte")).Text == "" ||
                         ((RadTextBox)gi.FindControl("TxtSol_CteNombre")).Text == "")
                    {
                        Alertavalida("Todos los campos son requeridos");
                        e.Canceled = true;
                        return;
                    }

                }



                SolConvenioDet s = new SolConvenioDet();
                s.Id_Cte = int.Parse(((RadNumericTextBox)gi.FindControl("TxtId_Cte")).Text);
                s.Sol_CteNombre = ((RadTextBox)gi.FindControl("TxtSol_CteNombre")).Text.Trim();

                s.Id_Unique = Guid.NewGuid().ToString();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                s.Id_Cd = sesion.Id_Cd_Ver;

                if (ListDet.Where(x => x.Id_Cte == s.Id_Cte).ToList().Count > 0)
                {
                    Alerta("La presente solicitud no se puede procesar debido a que el cliente  ya se encuentra vinculado al convenio.   <BR><BR>No. de cliente: <b>" + s.Id_Cte.ToString() + "</b> <BR>Nombre de cliente: <b>" + s.Sol_CteNombre + "</b>");
                    e.Canceled = true;
                    return;
                }

                ListDet.Add(s);

                rgSolicitudDet.Rebind();


            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void Borrar(GridCommandEventArgs e)
        {
            try
            {
                GridItem gi = e.Item;
                string Id_Unique;
                Id_Unique = ((Label)gi.FindControl("LblId_Unique")).Text.Trim();

                ListDet.Remove(ListDet.Where(x => x.Id_Unique == Id_Unique).ToList()[0]);
          
                rgSolicitudDet.Rebind();


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void Modificar(GridCommandEventArgs e)
        {
            try
            {
                GridItem gi = e.Item;

                if (HFCapUsuario.Value == "False")
                {

                    if (((RadNumericTextBox)gi.FindControl("TxtId_Cte")).Text == "" ||
                                    ((RadTextBox)gi.FindControl("TxtSol_CteNombre")).Text == "")
                    {
                        Alertavalida("Todos los campos son requeridos");
                        e.Canceled = true;
                        return;
                    }
                }
                else
                {
                    if (((RadNumericTextBox)gi.FindControl("TxtId_Cte")).Text == "" ||
                         ((RadTextBox)gi.FindControl("TxtSol_CteNombre")).Text == "")
                    {
                        Alertavalida("Todos los campos son requeridos");
                        e.Canceled = true;
                        return;
                    }

                }

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                SolConvenioDet s = new SolConvenioDet();
                s.Id_Cte = int.Parse(((RadNumericTextBox)gi.FindControl("TxtId_Cte")).Text);
                s.Sol_CteNombre = ((RadTextBox)gi.FindControl("TxtSol_CteNombre")).Text.Trim();

                s.Id_Unique = ((Label)gi.FindControl("LblId_UniqueE")).Text.Trim();
                s.Id_Cd = sesion.Id_Cd_Ver;

                //if (ListDet.Where(x => x.Id_Cte == s.Id_Cte).ToList().Count > 0)
                //{
                //    Alerta("El cliente ya fue agregado");
                //    e.Canceled = true;
                //    return;
                //}

                ListDet.Where(x => x.Id_Unique   == s.Id_Unique).ToList()[0].Id_Cte  = s.Id_Cte;
                ListDet.Where(x => x.Id_Unique == s.Id_Unique).ToList()[0].Sol_CteNombre = s.Sol_CteNombre;



                rgSolicitudDet.Rebind();


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private int ObtenerConsecutivo()
        {
            try
            {
                int Id_Sol = 0;
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
                CN_Convenio cn_conv = new CN_Convenio();

                cn_conv.ConsultaConsecutivoSolicitud(ref Id_Sol, Conexion);

                return Id_Sol;

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private string ObtenerCapturaUsuario(string Cat_DescCorta)
        {
            try
            {
                string Cat_CapturaUsuario = null;
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
                CN_Convenio cn_conv = new CN_Convenio();
                cn_conv.ConsultaCapturaUsuario(Cat_DescCorta, ref Cat_CapturaUsuario, Conexion);


                return Cat_CapturaUsuario;

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void Guardar()
        {
            try
            {
                if (ListDet.Count == 0)
                {
                    Alertavalida("Debe agregar al menos un cliente");
                    return;
                }

                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];  //40 siancentral
                string Conexion2 = System.Configuration.ConfigurationManager.AppSettings["strConnectionSIANCentral"]; //SIANwebCentral
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_Convenio cn_conv = new CN_Convenio();
                SolConvenio sol = new SolConvenio();
                int Verificador = 0;

                LlenarEncabezado(ref sol);
                //JFCV TODO agregar estos metodos
                List<SolConvenioDet> Listvalida = new List<SolConvenioDet>();
                Listvalida = ListDet;
                string mensajeerror = " ";

                cn_conv.ConvenioSolicitud_ValidaNoExistaClienteDet(sol, Listvalida, ref Verificador, ref mensajeerror, Conexion);

                if (mensajeerror != "")
                {

                    Alerta("La presente solicitud no se puede procesar debido a que el <BR> cliente ya se encuentra vinculado al convenio.<BR> " + mensajeerror);
                    return;
                }

                Verificador = 0;


                cn_conv.ConvenioSolicitud_Insertar(sol, ref Verificador, Conexion);


                string rutaarchivo = string.Concat(ConfigurationManager.AppSettings["WebURLCarpetaConvenios"].ToString(), "PoliticasGenerales.pdf");

                if (Verificador != 0)
                {
                    List<SolConvenioDet> List = new List<SolConvenioDet>();
                    List = ListDet;
                    int Id_Sol = Verificador;
                    cn_conv.ConvenioSolicitud_InsertarDet(Id_Sol, sol, List, ref Verificador, Conexion);

                    if (Verificador == -1)
                    {
                        try
                        {
                            //cn_conv.ConvenioSolicitud_EnviarCorreoCreoSol(Id_Sol, ref Verificador, sesion.Emp_Cnx);
                            EnviaMail_CorreoCreoSol(Id_Sol);

                        }
                        catch (Exception ex)
                        {
                            Alerta("Ocurrio un error al enviar el mensaje de correo");
                            throw ex;
                        }
                        finally
                        {
                            //AlertaCerrar("Se generó correctamente la solicitud de vinculación con el No de folio: " + Id_Sol.ToString() + " </BR>***Nota: Si la presente solicitud es autorizada, implicará la aceptación de los terminos de la <a href=http://40.84.229.61/siancentral_PRUEBA/CarpetaConvenios/PoliticasGenerales.pdf>Política de Convenios de PAAA Especiales.</a>");
                            AlertaCerrar("Se generó correctamente la solicitud de vinculación con el No de folio: " + Id_Sol.ToString() + " </BR>***Nota: Si la presente solicitud es autorizada, implicará la aceptación de los terminos de la <a href=" + rutaarchivo + ">Política de Convenios de PAAA Especiales.</a>");
                        }
                        AlertaCerrar("Se generó correctamente la solicitud de vinculación con el No de folio: " + Id_Sol.ToString() + " </BR>***Nota: Si la presente solicitud es autorizada, implicará la aceptación de los terminos de la <a href=" + rutaarchivo + ">Política de Convenios de PAAA Especiales.</a>");
                        //AlertaCerrar("Se generó correctamente la solicitud de vinculación con el No de folio: " + Id_Sol.ToString() + " </BR>***Nota: Si la presente solicitud es autorizada, implicará la aceptación de los terminos de la <a href=http://40.84.229.61/siancentral_PRUEBA/CarpetaConvenios/PoliticasGenerales.pdf>Política de Convenios de PAAA Especiales.</a>");
                        //AlertaCerrar("Se generó correctamente la solicitud de vinculación con el No de folio: " + Id_Sol.ToString() + " </BR>***Nota: Si la presente solicitud es autorizada, implicará la aceptación de los terminos de la <h3 style= color:blue; >Politica de Convenios de PAAA Especiales.</h3>");

                    }
                    else
                    {
                        Alertavalida("Error al tratar de guardar la solicitud");
                        return;
                    }
                }
                else
                {
                    Alerta("Error al tratar de guardar la solicitud");
                    return;
                }



            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        private void EnviaMail_CorreoCreoSol(int Id_Sol)
        {
            string Asunto = "";
            int adicionales = 0;  //Gerente JO
            int anexos = 0;
            int administradores = 1;
            int detalle = 0;
            int destinatarios = 0;  //RIKS 
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            int tipo_Habilitar = 1; //Ver
            Asunto = "Solicitud de vinculación No. " + Id_Sol.ToString() + " / " + LblPC_Nombre.Text;

            StringBuilder cuerpo_correo = new StringBuilder();

            cuerpo_correo.Append("<div align='left'>");

            cuerpo_correo.Append("<table style='font-family: Verdana; font-size: 8pt'>");
            cuerpo_correo.Append("<tr> <td > * Se ha generado la siguiente solicitud de vinculación.</td>  </tr>");
            cuerpo_correo.Append("<tr> <td > * Una vez atendida la solicitud, le envíaremos un correo indicando si el cliente fue AUTORIZADO ó RECHAZADO para agregarse al convenio.</td>  </tr>");
            cuerpo_correo.Append("<tr> <td >  &nbsp; </td>  </tr>");
            cuerpo_correo.Append("</table>");

            cuerpo_correo.Append("<table style='font-family: Verdana; font-size: 8pt'>");
            cuerpo_correo.Append("<tr><td style ='Font-Bold='True'>Convenio Key: <b>" + Convert.ToString(HFId_PC.Value));
            cuerpo_correo.Append(" </b></td> <td>  &nbsp; </td> <td> &nbsp;  </td>  </tr>");
            cuerpo_correo.Append("<tr><td style ='Font-Bold='True'>Convenio proveedor: <b>" + LblPC_NoConvenio.Text);
            cuerpo_correo.Append(" </b></td> <td>  &nbsp; </td> <td> &nbsp;  </td>  </tr>");
            cuerpo_correo.Append("<tr><td style ='Font-Bold='True'>Nombre de convenio: <b>" + LblPC_Nombre.Text);
            cuerpo_correo.Append(" </b></td> <td>  &nbsp; </td> <td> &nbsp;  </td>  </tr>");
            cuerpo_correo.Append("<tr><td style ='Font-Bold='True'>Categoría: <b>" + LblId_CatStr.Text);
            cuerpo_correo.Append(" </b></td> <td>  &nbsp; </td> <td> &nbsp;  </td>  </tr>");
            cuerpo_correo.Append("</table> <br>");
            cuerpo_correo.Append("<Table  border='1'><tr><td><b>No. Cliente </b></td><td><b>Nombre de cliente:</b></td> <td><b>Concesionario: </b></td> <td><b>Usuario que solicita: </b></td></tr>");


            foreach (SolConvenioDet s in ListDet)
            {
                cuerpo_correo.Append("<tr> <td >  " +  s.Id_Cte.ToString()  + "  </td>   ");
                cuerpo_correo.Append("<td >   " + s.Sol_CteNombre + "  </td> ");
                cuerpo_correo.Append("<td >   " + LblCd_Nombre.Text + "  </td> ");
                cuerpo_correo.Append("<td >   " + LblU_Nombre.Text + "  </td></tr> ");
            }
 
            cuerpo_correo.Append("</table>");



            EnviaMailConvenio enviarcorreo = new EnviaMailConvenio(int.Parse(HFId_PC.Value), Asunto, cuerpo_correo, "", 1, destinatarios, adicionales, anexos, administradores, 0, sesion, tipo_Habilitar, detalle, LblU_Correo.Text);
            enviarcorreo.EnviaMail();

        }


        private void LlenarEncabezado(ref SolConvenio sol)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                sol.Id_Sol = int.Parse(HFId_Sol.Value);
                sol.Id_Cd = sesion.Id_Cd_Ver;
                sol.Id_PC = HFId_Sol.Value == "0" ? int.Parse(HFId_PC.Value) : 0;
                sol.Id_U = sesion.Id_U;
                sol.Sol_UNombre = sesion.U_Nombre;
                sol.Sol_UCorreo = sesion.U_Correo;


            }
            catch (Exception ex)
            {
                
                throw ex;
            }
 
        }
        private void ConsultaEncabezado()
        {
            try
            {
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
                CN_Convenio cn_conv = new CN_Convenio();
                SolConvenio sol = new SolConvenio();

                cn_conv.ConvenioSolicitud_Consulta(int.Parse(HFId_Sol.Value), ref sol, Conexion);

                LblCd_Nombre.Text = sol.CD_Nombre;
                LblU_Nombre.Text = sol.Sol_UNombre;
                LblU_Correo.Text = sol.Sol_UCorreo;
                LblSol_Fecha.Text = sol.Sol_Fecha.ToShortDateString();
                LblPC_NoConvenio.Text = sol.PC_NoConvenio;
                LblPC_Nombre.Text = sol.PC_Nombre;
                LblId_CatStr.Text = sol.Cat_DescCorta;
                HFCapUsuario.Value = ObtenerCapturaUsuario(sol.Cat_DescCorta);

                LblPC_NoConvenioKey.Text = sol.Id_PC.ToString();

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void ConsultaDetalle()
        {
            try
            {
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
                CN_Convenio cn_conv = new CN_Convenio();
                List<SolConvenioDet> List = new List<SolConvenioDet>();

                cn_conv.ConvenioSolicitud_ConsultaDet(int.Parse(HFId_Sol.Value), ref List, Conexion);

                ListDet = List;
                rgSolicitudDet.Rebind();

            }
            catch (Exception ex)
            {
                
                throw ex;
            }

        }
        private void Modificar()
        {
            try
            {
                if (ListDet.Count == 0)
                {
                    Alertavalida("Debe agregar al menos un cliente");
                    return;
                }

                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
                CN_Convenio cn_conv = new CN_Convenio();
                SolConvenio sol = new SolConvenio();
                int Verificador = 0;

                LlenarEncabezado(ref sol);

                cn_conv.ConvenioSolicitud_Modificar(sol, ref Verificador, Conexion);

                string rutaarchivo = string.Concat(ConfigurationManager.AppSettings["WebURLCarpetaConvenios"].ToString(), "PoliticasGenerales.pdf");


                if (Verificador == -1)
                {
                    List<SolConvenioDet> List = new List<SolConvenioDet>();
                    List = ListDet;
                    int Id_Sol = int.Parse(HFId_Sol.Value);
                    cn_conv.ConvenioSolicitud_InsertarDet(Id_Sol, sol, List, ref Verificador, Conexion);

                    if (Verificador == -1)
                    {
                        //AlertaCerrar("Se modifico correctamente la solicitud de vinculación con el No de folio: " + Id_Sol.ToString() + " </BR>***Nota: Si la presente solicitud es autorizada, implicará la aceptación de los terminos de la <a href=http://40.84.229.61/siancentral_PRUEBA/CarpetaConvenios/PoliticasGenerales.pdf>Politica de Convenios de PAAA Especiales.</a>");
                        AlertaCerrar("Se modifico correctamente la solicitud de vinculación con el No de folio: " + Id_Sol.ToString() + " </BR>***Nota: Si la presente solicitud es autorizada, implicará la aceptación de los terminos de la <a href=" + rutaarchivo + ">Politica de Convenios de PAAA Especiales.</a>");
                    }
                    else
                    {
                        Alertavalida("Error al tratar de modificar la solicitud");
                        return;
                    }
                }
                else
                {
                    Alertavalida("Error al tratar de modificar la solicitud");
                    return;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion

        #region ErrorManager
        private void Alertavalida(string mensaje)
        {
            try
            {
                mensaje = mensaje.Replace(Convert.ToChar(10).ToString(), string.Empty);
                mensaje = mensaje.Replace(Convert.ToChar(13).ToString(), string.Empty);
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 200, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }

        private void Alerta(string mensaje)
        {
            try
            {
                mensaje = mensaje.Replace(Convert.ToChar(10).ToString(), string.Empty);
                mensaje = mensaje.Replace(Convert.ToChar(13).ToString(), string.Empty);
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 500, 200);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
        private void AlertaCerrar(string mensaje)
        {
            try
            {
                mensaje = mensaje.Replace(Convert.ToChar(10).ToString(), string.Empty);
                mensaje = mensaje.Replace(Convert.ToChar(13).ToString(), string.Empty);
                RAM1.ResponseScripts.Add("CloseWindowA('" + mensaje + "');");
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
        #endregion
    }
}