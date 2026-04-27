using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using System.Configuration;
using CapaNegocios;
using DevExpress.Web;
using System.Collections;
using System.Web.Services;
using Newtonsoft.Json;
using DevExpress.XtraPrinting;
using DevExpress.Export;


namespace SIANWEB
{
    public partial class CapAlertaConsulta : System.Web.UI.Page
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

                    if (Session["listaSolicitudescon" + Session.SessionID] != null)
                    {
                        gridBuscar.DataSource = Session["listaSolicitudescon" + Session.SessionID];
                        gridBuscar.DataBind();
                    }

                    if (!IsPostBack)
                    {
                        if (sesion != null)
                        {
                            Session["listaSolicitudescon" + Session.SessionID] = null;

                            inicializar();
                            string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];

                            cmbBuscarRepresentante.Items.Add("--Todos--", "-1");
                            cmbBuscarRepresentante.Value = "-1";

                            CmbTipoSolicitud.Items.Add("--Todos--", "-1");

                            cmbEstatus.Items.Add("--Todos--", "-1");
                            cmbEstatus.Items.Add("Solicitado", "1");
                            cmbEstatus.Items.Add("Autorizado", "2");
                            cmbEstatus.Items.Add("Cancelado", "3");
                            cmbEstatus.Items.Add("Rechazado", "4");
                            cmbEstatus.Value = "-1";

                            CmbTipoSolicitud.Items.Add("CRM", "1");
                            CmbTipoSolicitud.Items.Add("ACYS", "2");
                            CmbTipoSolicitud.Items.Add("Remisión", "3");
                            CmbTipoSolicitud.Items.Add("Pedido", "4");
                            CmbTipoSolicitud.Items.Add("Factura", "5");
                            CmbTipoSolicitud.Value = "-1";

                            if (sesion.Id_TU == 2)
                            {
                                cmbBuscarRepresentante.Items.Clear();
                                cmbBuscarRepresentante.Items.Add(sesion.U_Nombre, Convert.ToInt32(sesion.Id_Rik.ToString()));
                                cmbBuscarRepresentante.Value = "-1";
                                btnRegresar.Visible = false;

                            }
                            else
                            {
                                CargarRik();
                            }

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                mensaje(ex + " " + new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }



        }
        private void inicializar()
        {
            ValidarSesion();
            //CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();

            //CN_Comun.DevLlenaCombo(2, sesion.Id_Emp, sesion.Id_U, Emp_CnxCentral, "SP_CatCDI_combo2", ref CmbSucursal);
            //CMBSucursalRepresentante.Items.AddRange(CmbSucursal.Items.Cast<Object>().ToArray());

            CargarDatos();

            txtfechaInicialBuscarInformacion.Value = DateTime.Now;
            txtfechaFinalBuscarInformacion.Value = DateTime.Now;
        }

        protected void btnBuscarInformacion_ServerClick(object sender, EventArgs e)
        {
            ValidarSesion();
            CargarDatos();
        }

        protected void btnDescargarInformacion_ServerClick(object sender, EventArgs e)
        {

            if (Session["listaSolicitudescon" + Session.SessionID] != null)
            {
                gridBuscar.DataSource = Session["listaSolicitudescon" + Session.SessionID];
                gridBuscar.DataBind();

                GridViewExporter1.WriteXlsToResponse(new XlsExportOptionsEx { ExportType = ExportType.WYSIWYG });

            }
            else
            {
                mensaje("Sin información de solicitudes.");
            }


        }



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

            string mesAnioInicial = txtfechaInicialBuscarInformacion.Text;
            string mesAniofinal = txtfechaFinalBuscarInformacion.Text;

            Session["listaSolicitudescon" + Session.SessionID] = null;


            try
            {
                List<AlertaAutorizacion> listaSolicitudescon = new List<AlertaAutorizacion>();
                AlertaAutorizacion CapAlerta = new AlertaAutorizacion();


                if (mesAnioInicial != "" && mesAniofinal != "")
                {

                    string FechaInicialD = ("01" + "/" + mesAnioInicial.Split('/')[0] + "/" + mesAnioInicial.Split('/')[1]);
                    string FechaFinalD = ("01" + "/" + mesAniofinal.Split('/')[0] + "/" + mesAniofinal.Split('/')[1]);



                    if (Convert.ToDateTime(FechaInicialD) > Convert.ToDateTime(FechaFinalD))
                    {
                        mensaje("La fecha inicial es mayor a la fecha final de la sección de Buscar Información.");
                        return;
                    }
                    CapAlerta.Id_Emp = sesion.Id_Emp;
                    CapAlerta.Id_Cd = sesion.Id_Cd_Ver;
                    //DateTime FechaInicial2 = DateTime.Parse(FechaInicialD);
                    // DateTime FechaFinal2 = DateTime.Parse(FechaFinalD).AddMonths(1).AddDays(-1);
                    DateTime FechaInicial2 = DateTime.Parse(FechaInicialD);
                    DateTime FechaFinal2 = DateTime.Parse(FechaFinalD).AddMonths(1).AddDays(-1);
                    CapAlerta.FechaInicial = FechaInicial2;
                    CapAlerta.FechaFinal = FechaFinal2;
                    CapAlerta.IdRepresentante = -1; // Convert.ToInt32(cmbBuscarRepresentante.Value.ToString());
                    CapAlerta.IdUsuarioGteAutorizo = -1; //Convert.ToInt32(cmbBuscarRepresentante.Value.ToString());
                    CapAlerta.TipoAutorizacion = Convert.ToInt32(CmbTipoSolicitud.Value.ToString());
                    CapAlerta.Estatus = Convert.ToInt32(cmbEstatus.Value.ToString());
                    if (CMBSucursalRepresentante.Value == null)
                    {
                        CapAlerta.IdRepresentante = -1;
                    }
                    else
                    {
                        CapAlerta.IdRepresentante = Convert.ToInt32(CMBSucursalRepresentante.Value.ToString());
                    }
                    // si el usuario es un rik solo traer sus solicitudes 
                    CapAlerta.IdRepresentante = sesion.Id_TU == 2 ? Convert.ToInt32(sesion.Id_Rik.ToString()) : Convert.ToInt32(CMBSucursalRepresentante.Value.ToString());


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

                    CN_AlertaAutorizacion cn_auto = new CN_AlertaAutorizacion();
                    cn_auto.ConsultaAlertaAutorizacionLista(CapAlerta, ref listaSolicitudescon, "LISTADO", Emp_CnxCentral);

                    //listaLeadsquery = listaLeadsquery.OrderBy(x => x.Mes).ToList();


                    Session["listaSolicitudescon" + Session.SessionID] = listaSolicitudescon;
                    gridBuscar.DataSource = listaSolicitudescon;
                    gridBuscar.DataBind();
                    //UPdBusacarinfo.Update();
                    //UpdatePanel6.Update();
                }
            }
            catch (Exception ex)
            {
                mensaje("Error al obtener los datos:  " + ex.Message);
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



    }
}