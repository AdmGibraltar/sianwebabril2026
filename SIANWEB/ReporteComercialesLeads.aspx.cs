
using CapaEntidad;
using CapaNegocios;
using ClosedXML.Excel;
using DevExpress.Export;
using DevExpress.XtraPrinting;
using Newtonsoft.Json;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace SIANWEB
{
    public partial class ReporteComercialesLeads : System.Web.UI.Page
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

        #region Eventos 

        /// <summary>
        /// Precarga la informacion de los grid view  de:
        /// Presupuesto
        /// Multiplicador
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {

            if (Session["SMultiplicador"] != null)
            {

                List<CatMultiplicador> listaMultiplicador = new List<CatMultiplicador>();
                listaMultiplicador = (List<CatMultiplicador>)Session["SMultiplicador"];

            }
            if (Session["listaMultiplicador"] != null)
            {
                List<CatMultiplicador> lista = new List<CatMultiplicador>();
                lista = (List<CatMultiplicador>)Session["listaMultiplicador"];
                gridBuscar.DataSource = lista;
                gridBuscar.DataBind();
                UPdBusacarinfo.Update();
                UpdatePanel6.Update();
            }
        }

        /// <summary>
        /// Inicio de la funciones de la pantalla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                ValidarPermisos();
                Session["Sucursal"] = null;
                Session["Representante"] = null;
                Session["SPresupuesto"] = null;
                Session["SMultiplicador"] = null;
                Session["listaMultiplicador"] = null;
                Session["nombreRik"] = null;

                gridBuscar.DataSource = null;
                gridBuscar.DataBind();
                UpdatePanel6.Update();

                if (sesion != null)
                {
                    inicializar();

                    string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];

                    CapaNegocios.CN__Comun CN = new CapaNegocios.CN__Comun();

                    CN.DevLlenaCombo(-1, Conexion, "spLeads_ConsultaMedioComunicacion", ref CmbMedioContacto);
                    CmbMedioContacto.SelectedIndex = 0;

                    CN.DevLlenaCombo(-1, Conexion, "spLeads_ConsultaGiroEmpresa", ref CmbGiroEmpresa);
                    CmbGiroEmpresa.SelectedIndex = 0;

                    CN.DevLlenaCombo(-1, Conexion, "spLeads_ConsultaEstatus", ref CmbEstatus);
                    CmbEstatus.SelectedIndex = 0;

                    gridBuscar.Columns["NumeroEmpleados"].Visible = false;
                    gridBuscar.Columns["Estado"].Visible = false;
                    gridBuscar.Columns["Ciudad"].Visible = false;


                }
            }
            Session["activeMenu"] = 3;
        }






        protected void BtnCompararRepresentante_ServerClick(object sender, EventArgs e)
        {



        }


        protected void btndescragrBuscarInformacion_ServerClick(object sender, EventArgs e)
        {

            try
            {
                gridBuscar.Columns["NombreContacto"].Visible = true;
                gridBuscar.Columns["NumeroEmpleados"].Visible = true;
                gridBuscar.Columns["Estado"].Visible = true;
                gridBuscar.Columns["Ciudad"].Visible = true;
                gridBuscar.Columns["Correo"].Visible = true;
                gridBuscar.Columns["Telefono"].Visible = true;
                gridBuscar.Columns["ProductoInteres"].Visible = true;
                gridBuscar.Columns["Comentarios"].Visible = true;
                gridBuscar.Columns["MotivoCanceladoGerente"].Visible = true;
                gridBuscar.Columns["MotivoCanceladoRik"].Visible = true;
                CargarDatos();

                gridBuscar.ExportXlsToResponse(new XlsExportOptionsEx { ExportType = ExportType.WYSIWYG });

                gridBuscar.Columns["NombreContacto"].Visible = false;
                gridBuscar.Columns["NumeroEmpleados"].Visible = false;
                gridBuscar.Columns["Estado"].Visible = false;
                gridBuscar.Columns["Ciudad"].Visible = false;
                gridBuscar.Columns["Correo"].Visible = false;
                gridBuscar.Columns["Telefono"].Visible = false;
                gridBuscar.Columns["ProductoInteres"].Visible = false;
                gridBuscar.Columns["Comentarios"].Visible = false;
                gridBuscar.Columns["MotivoCanceladoGerente"].Visible = false;
                gridBuscar.Columns["MotivoCanceladoRik"].Visible = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                Debug.WriteLine(ex.InnerException.ToString());
            }


        }

        #region eventos tiemporespuesta

        protected void CmbSucursalRepresentante_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id_Cd = Convert.ToInt32(CMBSucursalRepresentante.Value.ToString());
            cargarRIkTR(id_Cd);
            updpanel3.Update();
        }

        /// <summary>
        /// evento que se llama en la seccion de comparar representante
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void wizClaimInfo_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {

            if (WCompararRepresentantes.WizardSteps[e.CurrentStepIndex].Title == "1")
            {
                DateTime fecha1 = DateTime.Parse(TXTAnioInicialTR.Value.ToString());
                DateTime fecha2 = DateTime.Parse(TXTAnioFinalTR.Value.ToString()).AddMonths(1).AddDays(-1);
                fecharikvsrikinicial.Value = fecha1.ToString("dd/MM/yyyy");
                fecharikvsrikfinal.Value = fecha2.ToString("dd/MM/yyyy");
            }


            if (WCompararRepresentantes.WizardSteps[e.CurrentStepIndex].Title == "2")
            {
                if (CMBSucursalRepresentante.Value.ToString() == "-1")
                {
                    e.Cancel = true;
                }

            }

            if (WCompararRepresentantes.WizardSteps[e.CurrentStepIndex].Title == "3")
            {
                if (CMBSucursalRepresentante.Value.ToString() == "-1")
                {
                    e.Cancel = true;
                }
                else
                {
                    Session["Sucursal"] = CMBSucursalRepresentante.Value.ToString();
                    if (TipoRikGerente2.SelectedItem.Index == 1)
                    {
                        SelecciontipoGerente.Value = "1";
                        SucursalRepresentante.Value = CMBSucursalRepresentante.Value.ToString();
                        cargarGerente(Convert.ToInt32(CMBSucursalRepresentante.Value.ToString()));
                        RBLrepresentante2.Visible = false;
                        RBLGerente2.Visible = true;
                        RBLGerente2.Enabled = false;
                    }
                    else
                    {
                        SucursalRepresentante.Value = CMBSucursalRepresentante.Value.ToString();
                        SelecciontipoGerente.Value = "0";
                        RBLrepresentante2.Visible = true;
                        RBLGerente2.Visible = false;
                    }
                }
            }


            if (WCompararRepresentantes.WizardSteps[e.CurrentStepIndex].Title == "4")
            {
                txtrepre.Value = "";
                string representante = "";

                List<int> myList = new List<int>();

                if (TipoRikGerente2.SelectedItem.Index == 0) //Representante
                {

                    for (var i = 0; i < RBLrepresentante2.Items.Count; i++)
                    {
                        if (RBLrepresentante2.Items[i].Selected)
                        {
                            int value = Convert.ToInt32(RBLrepresentante2.Items[i].Value.ToString());
                            myList.Add(value);
                            if (representante == "")
                            {
                                representante = value.ToString();
                            }
                            else
                            {
                                representante = representante + "," + value.ToString();
                            }
                        }
                    }

                    if (myList.Count == 0)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        txtrepre.Value = representante;
                        Session["Representante"] = representante;
                        SelecciontipoGerente.Value = representante;
                    }


                }

                if (TipoRikGerente2.SelectedItem.Index == 1) //gerente
                {



                    if (RBLGerente2.SelectedIndex == 0)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        txtrepre.Value = RBLGerente2.SelectedItem.Text;
                        Session["Gerente"] = txtrepre.Value;
                    }


                }


            }

            WCompararRepresentantes.PreRender += new EventHandler(WCompararRepresentantes_PreRender);
            updpanel3.Update();
        }

        /// <summary>
        /// funcion para que se visualize los steps
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void WCompararRepresentantes_PreRender(object sender, EventArgs e)
        {
            Repeater SideBarList = WCompararRepresentantes.FindControl("HeaderContainer").FindControl("SideBarList") as Repeater;
            SideBarList.DataSource = WCompararRepresentantes.WizardSteps;
            SideBarList.DataBind();
            updpanel3.Update();
        }

        protected void WCompararRepresentantes_PreviousButtonClick(object sender, WizardNavigationEventArgs e)
        {
            WCompararRepresentantes.PreRender += new EventHandler(WCompararRepresentantes_PreRender);
            updpanel3.Update();
        }

        /// <summary>
        /// eventos para el wizard botones
        /// </summary>
        /// <param name="wizardStep"></param>
        /// <returns></returns>
        protected string GetClassForWizardStep(object wizardStep)
        {
            WizardStep step = wizardStep as WizardStep;

            if (step == null)
            {
                return "";
            }
            int stepIndex = WCompararRepresentantes.WizardSteps.IndexOf(step);

            if (stepIndex < WCompararRepresentantes.ActiveStepIndex)
            {
                return "prevStep";
            }
            else if (stepIndex > WCompararRepresentantes.ActiveStepIndex)
            {
                return "nextStep";
            }
            else
            {
                return "currentStep";
            }
        }


        /// <summary>
        /// Carga la informacion del rik y los agrupa por id
        /// </summary>
        /// <param name="id_Cd"></param>
        private void cargarRIkTR(int id_Cd)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_Rik Rik = new CN_Rik();
            Rik RegistroRik = new Rik();
            RegistroRik.Id_Emp = Sesion.Id_Emp;
            RegistroRik.Id_Cd = id_Cd;
            RegistroRik.Id_Rik = -1;

            List<Rik> list_Riks = new List<Rik>();

            Rik.ConsultaRIkCrm(RegistroRik, ref list_Riks, Emp_CnxCentral);

            List<Rik> query = (from m in list_Riks
                               group m.nombre_Rik by m.Id_Rik into g
                               select new Rik { id = g.Key, nombre = g.First() }).ToList();

            Session["nombreRik"] = query;
            RBLrepresentante2.DataSource = query;
            RBLrepresentante2.ValueField = "id";
            RBLrepresentante2.TextField = "nombre";
            RBLrepresentante2.DataBind();

        }


        /// <summary>
        /// Carga la informacion del rik y los agrupa por id
        /// </summary>
        /// <param name="id_Cd"></param>
        private void cargarGerente(int id_Cd)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_Leads cnlead = new CN_Leads();
            Usuario usuario = new Usuario();
            usuario.Id_Emp = Sesion.Id_Emp;
            usuario.Id_Cd = id_Cd;
            usuario.Id_Rik = -1;
            ///usuario.Id_TU = 3;
            ///Para que traiga los gerenets es el 3 pero para gerentes de la tabla de gerentes es 8 
            usuario.Id_TU = 8;

            List<Usuario> list_Riks = new List<Usuario>();

            cnlead.ConsultaGerente(usuario, Emp_CnxCentral, ref list_Riks);

            List<Usuario> query = (from m in list_Riks
                                   group m.U_Nombre by m.Id_U into g
                                   select new Usuario { Id_U = g.Key, U_Nombre = g.First() }).ToList();

            Session["nombreGerente"] = query;
            RBLGerente2.DataSource = query;
            RBLGerente2.ValueField = "Id_U";
            RBLGerente2.TextField = "U_Nombre";
            RBLGerente2.DataBind();
            RBLGerente2.SelectedIndex = 1;

        }


        #endregion eventos tiemporespuesta

        protected void btnBuscarInformacion_ServerClick(object sender, EventArgs e)
        {
            ValidarSesion();
            CargarDatos();
        }

        public void CargarDatos()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            string mesAnioInicial = txtfechaInicialBuscarInformacion.Text;
            string mesAniofinal = txtfechaFinalBuscarInformacion.Text;

            List<Leads> listaLeads = new List<Leads>();
            Leads CapLeads = new Leads();

            if (mesAnioInicial != "" && mesAniofinal != "")
            {

                string FechaInicialD = ("01" + "/" + mesAnioInicial.Split('/')[0] + "/" + mesAnioInicial.Split('/')[1]);
                string FechaFinalD = ("01" + "/" + mesAniofinal.Split('/')[0] + "/" + mesAniofinal.Split('/')[1]);

                if (Convert.ToDateTime(FechaInicialD) > Convert.ToDateTime(FechaFinalD))
                {
                    mensaje("La fecha inicial es mayor a la fecha final de la sección de Buscar Información.");
                    return;
                }
                CapLeads.Id_Emp = sesion.Id_Emp;
                CapLeads.Id_Cd = Sesion.Id_Cd_Ver;
                DateTime FechaInicial2 = DateTime.Parse(FechaInicialD);
                DateTime FechaFinal2 = DateTime.Parse(FechaFinalD).AddMonths(1).AddDays(-1);
                CapLeads.FechaInicial = FechaInicial2;
                CapLeads.FechaFinal = FechaFinal2;

                CapLeads.IdMedioComunicacion = int.Parse(CmbMedioContacto.Value.ToString());
                CapLeads.IdGiroEmpresa = int.Parse(CmbGiroEmpresa.Value.ToString());

                //JFCV 15 abr 2021
                CapLeads.Activo = int.Parse(CmbEstatus.Value.ToString());
                if (CapLeads.Activo == -1)
                {
                    CapLeads.Activo = 10;  //para que traiga todos los estatus 
                }

                CN_Leads cn_leads = new CN_Leads();
                List<Leads> listLeads = new List<Leads>();
                cn_leads.ConsultaListaLeads(CapLeads, ref listaLeads, Emp_CnxCentral);

                Session["listaLeads"] = listaLeads;
                gridBuscar.DataSource = listaLeads;
                gridBuscar.DataBind();
                UPdBusacarinfo.Update();
                UpdatePanel6.Update();
            }
        }




        #endregion

        #region Funciones


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

        /// <summary>
        /// Funcion que inicializa toda la informacion inicial de la pantalla y los campos
        /// </summary>
        private void inicializar()
        {
            CargarTrimestre();
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();

            CN_Comun.DevLlenaCombo(2, sesion.Id_Emp, sesion.Id_U, Emp_CnxCentral, "SP_CatCDI_combo2", ref CmbSucursal);

            CMBSucursalRepresentante.Items.AddRange(CmbSucursal.Items.Cast<Object>().ToArray());

            CmbSucursal.Value = sesion.Id_Cd_Ver.ToString();
            CMBSucursalRepresentante.Value = sesion.Id_Cd_Ver.ToString();
            TXTAnioInicialOT.Value = DateTime.Now;
            TXTAnioFinalOT.Value = DateTime.Now;

            txtfechaFinalBuscarInformacion.Value = DateTime.Now;
            txtfechaInicialBuscarInformacion.Value = DateTime.Now;

            txtFechaFinalPVV.Value = DateTime.Now;
            txtFechaInicialPVV.Value = DateTime.Now;

            cmbBuscarRepresentante.Items.Add("--Todos--", "-1");
            cmbBuscarRepresentante.Value = "-1";

            CmbSucursal.ReadOnly = true;
            CMBSucursalRepresentante.ReadOnly = true;
            TXTAnioFinalEF.Value = DateTime.Now;
            TXTAnioInicialEF.Value = DateTime.Now;
            Session["sucursales"] = CmbSucursal.Items.Cast<Object>().ToArray();
            updpanel3.Update();
            TXTAnioFinalTR.Value = DateTime.Now;
            TXTAnioInicialTR.Value = DateTime.Now;

            int id_Cd2 = Convert.ToInt32(CmbSucursal.Value.ToString());
            cargarRIkTR(id_Cd2);
            RBLRepresentante.DataSource = null;
            cargarRIkBuscarInformacion(id_Cd2);
            UpdatePanel43.Update();
            updpanel3.Update();

        }


        protected void CmbSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id_Cd = Convert.ToInt32(CmbSucursal.Value.ToString());
            cargarRIkBuscarInformacion(id_Cd);
            UpdatePanel43.Update();
        }


        private void cargarRIkBuscarInformacion(int id_Cd)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_Rik Rik = new CN_Rik();
            Rik RegistroRik = new Rik();
            RegistroRik.Id_Emp = Sesion.Id_Emp;
            RegistroRik.Id_Cd = id_Cd;
            RegistroRik.Id_Rik = -1;
            List<Rik> list_Riks = new List<Rik>();

            Rik.ConsultaRIkCrm(RegistroRik, ref list_Riks, Emp_CnxCentral);



            List<Rik> query = (from m in list_Riks
                               where m.nombre_Rik != ""
                               group m.nombre_Rik by m.Id_Rik into g
                               select new Rik { id = g.Key, nombre = g.First() }).ToList();

            Session["nombreRik"] = query;
            RBLRepresentante.DataSource = query;
            RBLRepresentante.ValueField = "id";
            RBLRepresentante.TextField = "nombre";
            RBLRepresentante.DataBind();


        }




        /// <summary>
        /// Cargar la información del trimestre
        /// </summary>
        private void CargarTrimestre()
        {
            List<Mes> ListaMes = new List<Mes>();
            Mes RegistroMes;

            int MesActual = DateTime.Now.Month;

            RegistroMes = new Mes();
            RegistroMes.Id_mes = "-1";
            RegistroMes.NombreMes = "--Seleccionar--";
            ListaMes.Add(RegistroMes);

            RegistroMes = new Mes();
            RegistroMes.Id_mes = "1";
            RegistroMes.NombreMes = "trimestre 1";
            ListaMes.Add(RegistroMes);

            RegistroMes = new Mes();
            RegistroMes.Id_mes = "2";
            RegistroMes.NombreMes = "trimestre 2";
            ListaMes.Add(RegistroMes);

            RegistroMes = new Mes();
            RegistroMes.Id_mes = "3";
            RegistroMes.NombreMes = "trimestre 3";
            ListaMes.Add(RegistroMes);

            RegistroMes = new Mes();
            RegistroMes.Id_mes = "4";
            RegistroMes.NombreMes = "trimestre 4";
            ListaMes.Add(RegistroMes);

        }


        /// <summary>
        /// Carga la informacion del rik y los agrupa por id
        /// </summary>
        /// <param name="id_Cd"></param>
        private void cargarRIk(int id_Cd)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_Rik Rik = new CN_Rik();
            Rik RegistroRik = new Rik();
            RegistroRik.Id_Emp = Sesion.Id_Emp;
            RegistroRik.Id_Cd = id_Cd;
            RegistroRik.Id_Rik = -1;
            List<Rik> list_Riks = new List<Rik>();

            Rik.ConsultaRIk(RegistroRik, ref list_Riks, Emp_CnxCentral);

            var query = (from m in list_Riks
                         group m.nombre_Rik by m.Id_Rik into g
                         select new { id = g.Key, nombre = g.First() }).ToList();


        }


        private void ObtenerMEs(ref string mes, int NumeroMes)
        {
            if (NumeroMes == 1)
            {
                mes = "Enero";
            }
            if (NumeroMes == 2)
            {
                mes = "Febrero";
            }
            if (NumeroMes == 3)
            {
                mes = "Marzo";
            }
            if (NumeroMes == 4)
            {
                mes = "Abril";
            }
            if (NumeroMes == 5)
            {
                mes = "Mayo";
            }
            if (NumeroMes == 6)
            {
                mes = "Junio";
            }
            if (NumeroMes == 7)
            {
                mes = "Julio";
            }
            if (NumeroMes == 8)
            {
                mes = "Agosto";
            }
            if (NumeroMes == 9)
            {
                mes = "Septiembre";
            }
            if (NumeroMes == 10)
            {
                mes = "Octubre";
            }
            if (NumeroMes == 11)
            {
                mes = "Noviembre";
            }
            if (NumeroMes == 12)
            {
                mes = "Diciembre";
            }
        }


        #endregion

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




        private void cargarRIkDescargarInformacion(int id_Cd)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_Rik Rik = new CN_Rik();
            Rik RegistroRik = new Rik();
            RegistroRik.Id_Emp = Sesion.Id_Emp;
            RegistroRik.Id_Cd = id_Cd;
            RegistroRik.Id_Rik = -1;
            List<Rik> list_Riks = new List<Rik>();

            Rik.ConsultaRIk(RegistroRik, ref list_Riks, Emp_CnxCentral);



            var query = (from m in list_Riks
                         group m.nombre_Rik by m.Id_Rik into g
                         select new { id = g.Key, nombre = g.First() }).ToList();



        }


        #endregion

        #region webMethod



        [WebMethod]
        public static string ObservarTotales(string mesAnioInicial, string mesAniofinal, string seleccion, string Sucursal)
        {
            try
            {
                string Emp_CnxCentral = ConfigurationManager.AppSettings.Get("strConnectionCentral");
                CN_Leads Cn_Lead = new CN_Leads();
                Leads Presupuesto = new Leads();
                List<Leads> listaPresupuesto = new List<Leads>();
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];

                if (Sesion == null)
                {
                    return JsonConvert.SerializeObject(new { id = 0 });
                }
                int Tiposeleccion = Convert.ToInt32(seleccion);

                //if (int.Parse(Sucursal) == -1)
                //{
                //    return JsonConvert.SerializeObject(new { id = 1 });
                //}
                if (mesAnioInicial != "" && mesAniofinal != "")
                {
                    if (DateTime.Parse(mesAnioInicial) > DateTime.Parse(mesAniofinal))
                    {
                        return JsonConvert.SerializeObject(new { id = 4 });
                    }
                    //if (Tiposeleccion == 1) //Presupuesto
                    //{
                    DateTime FechaInicial2 = DateTime.Parse(mesAnioInicial);
                    DateTime FechaFinal2 = DateTime.Parse(mesAniofinal).AddMonths(1).AddDays(-1);


                    Presupuesto.FechaInicial = FechaInicial2;
                    Presupuesto.FechaFinal = FechaFinal2;
                    Presupuesto.TipoFiltro = Tiposeleccion;
                    Presupuesto.Id_Emp = Sesion.Id_Emp;
                    Presupuesto.Id_Cd = Sesion.Id_Cd;

                    Cn_Lead.ConsultaLeadsObservarTotales(Presupuesto, ref listaPresupuesto, Emp_CnxCentral);
                    int total = 0;
                    string titulo = "";
                    string datos = "";
                    foreach (Leads presupuesto in listaPresupuesto)
                    {
                        total = total + Convert.ToInt32(presupuesto.ValorCantidad);
                        if (titulo == "")
                        {
                            titulo = presupuesto.Descripcion;
                            datos = presupuesto.ValorCantidad.ToString();
                        }
                        else
                        {
                            titulo = titulo + "," + presupuesto.Descripcion;
                            datos = datos + "," + presupuesto.ValorCantidad.ToString();
                        }

                    }
                    //string totalstr = "Total: " + total.ToString("c");
                    string totalstr = "Total: " + total.ToString();
                    return JsonConvert.SerializeObject(new { id = 3, titulo = titulo, datos = datos, total = totalstr });
                    //}
                    //else //Venta
                    //{
                    //    DateTime fecha = DateTime.Parse(mesAnioInicial);
                    //    DateTime fechaFinal = DateTime.Parse(mesAniofinal).AddMonths(1).AddDays(-1);
                    //    Presupuesto.Id_Emp = Sesion.Id_Emp;
                    //    Presupuesto.Id_Cd = int.Parse(Sucursal);
                    //    Presupuesto.MesInicial = fecha.Month;
                    //    Presupuesto.AnioInicial = fecha.Year;
                    //    Presupuesto.MesFinal = fechaFinal.Month;
                    //    Presupuesto.AnioFinal = fechaFinal.Year;

                    //    CN_Pres.ConsultaUtilidadRIk(Presupuesto, ref listaPresupuesto, Emp_CnxCentral);
                    //    double total = 0;
                    //    string titulo = "";
                    //    string datos = "";
                    //    foreach (CatPresupuesto presupuesto in listaPresupuesto)
                    //    {
                    //        total = total + presupuesto.venta;
                    //        if (titulo == "")
                    //        {
                    //            titulo = presupuesto.ter_nombre;
                    //            datos = presupuesto.venta.ToString("F2");
                    //        }
                    //        else
                    //        {
                    //            titulo = titulo + "," + presupuesto.ter_nombre;
                    //            datos = datos + "," + presupuesto.venta.ToString("F2");
                    //        }
                    //    }
                    //string totalstr = "Total: " + total.ToString("c");
                    //return JsonConvert.SerializeObject(new { id = 3, titulo = titulo, datos = datos, total = totalstr });
                    //}
                }
                else
                {
                    return JsonConvert.SerializeObject(new { id = 2 });
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = ex.Message });
            }
        }

        [WebMethod]
        public static string IntegrarResultados(string mesAnioInicial, string mesAniofinal, string seleccion, string Sucursal)
        {
            try
            {
                string Emp_CnxCentral = ConfigurationManager.AppSettings.Get("strConnectionCentral");
                CN_Leads Cn_Lead = new CN_Leads();
                Leads Presupuesto = new Leads();
                List<Leads> listaPresupuesto = new List<Leads>();
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];

                if (Sesion == null)
                {
                    return JsonConvert.SerializeObject(new { id = 0 });
                }
                int Tiposeleccion = Convert.ToInt32(seleccion);

                //if (int.Parse(Sucursal) == -1)
                //{
                //    return JsonConvert.SerializeObject(new { id = 1 });
                //}
                if (mesAnioInicial != "" && mesAniofinal != "")
                {
                    if (DateTime.Parse(mesAnioInicial) > DateTime.Parse(mesAniofinal))
                    {
                        return JsonConvert.SerializeObject(new { id = 4 });
                    }
                    if (Tiposeleccion == 1) //Presupuesto
                    {
                        DateTime FechaInicial2 = DateTime.Parse(mesAnioInicial);
                        DateTime FechaFinal2 = DateTime.Parse(mesAniofinal).AddMonths(1).AddDays(-1);

                        Presupuesto.Id_Emp = Sesion.Id_Emp;
                        Presupuesto.Id_Cd = int.Parse(Sucursal);
                        Presupuesto.FechaInicial = FechaInicial2;
                        Presupuesto.FechaFinal = FechaFinal2;
                        Presupuesto.TipoFiltro = Tiposeleccion;



                        Cn_Lead.ConsultaLeadsIntegrarResultados(Presupuesto, ref listaPresupuesto, Emp_CnxCentral);
                        int total = 0;
                        string titulo = "";
                        string datos = "";
                        foreach (Leads presupuesto in listaPresupuesto)
                        {
                            total = total + Convert.ToInt32(presupuesto.ValorCantidad);
                            if (titulo == "")
                            {
                                titulo = presupuesto.Descripcion;
                                datos = presupuesto.ValorCantidad.ToString();
                            }
                            else
                            {
                                titulo = titulo + "," + presupuesto.Descripcion;
                                datos = datos + "," + presupuesto.ValorCantidad.ToString();
                            }

                        }
                        //string totalstr = "Total: " + total.ToString("c");
                        string totalstr = "Total: " + total.ToString();
                        return JsonConvert.SerializeObject(new { id = 3, titulo = titulo, datos = datos, total = totalstr });
                    }
                    else //Venta
                    {
                        DateTime fecha = DateTime.Parse(mesAnioInicial);
                        DateTime fechaFinal = DateTime.Parse(mesAniofinal).AddMonths(1).AddDays(-1);
                        Presupuesto.Id_Emp = Sesion.Id_Emp;
                        Presupuesto.Id_Cd = int.Parse(Sucursal);
                        Presupuesto.FechaInicial = fecha;
                        Presupuesto.FechaFinal = fechaFinal;
                        Presupuesto.TipoFiltro = Tiposeleccion;


                        Cn_Lead.ConsultaLeadsIntegrarResultados(Presupuesto, ref listaPresupuesto, Emp_CnxCentral);
                        double total = 0;
                        string titulo = "";
                        string datos = "";
                        foreach (Leads presupuesto in listaPresupuesto)
                        {
                            total = total + presupuesto.ValorMonto;
                            if (titulo == "")
                            {
                                titulo = presupuesto.Descripcion;
                                datos = presupuesto.ValorMonto.ToString("F2");
                            }
                            else
                            {
                                titulo = titulo + "," + presupuesto.Descripcion;
                                datos = datos + "," + presupuesto.ValorMonto.ToString("F2");
                            }
                        }
                        string totalstr = "Total: " + total.ToString("c");
                        return JsonConvert.SerializeObject(new { id = 3, titulo = titulo, datos = datos, total = totalstr });
                    }
                }
                else
                {
                    return JsonConvert.SerializeObject(new { id = 2 });
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = ex.Message });
            }
        }



        [WebMethod]
        public static string CompararRepresentantes(string parametro)
        {
            try
            {

                string Emp_CnxCentral = ConfigurationManager.AppSettings.Get("strConnectionCentral");
                CN_Presupuesto CN_Presupuesto = new CN_Presupuesto();
                CatPresupuesto Presupuesto = new CatPresupuesto();
                CN_Leads CN_Leads = new CN_Leads();
                List<CatPresupuesto> listaPresupuesto = new List<CatPresupuesto>();

                Sesion Sesion = new Sesion();
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
                if (Sesion == null)
                {
                    return JsonConvert.SerializeObject(new { id = 0 });
                }
                string sucursal = HttpContext.Current.Session["Sucursal"].ToString();
                //string rik = HttpContext.Current.Session["Representante"].ToString();
                string rik = parametro;



                string titulo = "";
                string datos = "";

                List<int> myList = new List<int>();

                if (int.Parse(sucursal) == -1)
                {
                    return JsonConvert.SerializeObject(new { id = 1 });
                }
                else
                {
                    if (rik != "")
                    {
                        string[] id_rik = rik.Split(',');
                        foreach (var value in id_rik)
                        {
                            myList.Add(Convert.ToInt32(value));
                        }
                    }

                    if (myList.Count == 0)
                    {
                        return JsonConvert.SerializeObject(new { id = 2 });
                    }
                    else
                    {
                        Presupuesto.Id_Emp = Sesion.Id_Emp;
                        Presupuesto.Id_Cd = int.Parse(sucursal);

                        CN_Leads.ConsultarProyectosAsig(Presupuesto, ref listaPresupuesto, Emp_CnxCentral);

                        List<CatPresupuesto> lista = (from tlist in listaPresupuesto
                                                      where myList.Contains(tlist.Id_Rik)
                                                      select tlist).ToList();


                        foreach (CatPresupuesto presupuesto in lista)
                        {
                            if (titulo == "")
                            {
                                titulo = presupuesto.NombreRik;
                                datos = presupuesto.cantidad.ToString();
                            }
                            else
                            {
                                titulo = titulo + "," + presupuesto.NombreRik;
                                datos = datos + "," + presupuesto.cantidad.ToString();
                            }
                        }
                        return JsonConvert.SerializeObject(new { id = 8, titulo = titulo, datos = datos });
                    }
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = ex.Message });
            }
        }

        [WebMethod]
        public static string btnBucaPVV_ServerClick(string mesAnioInicial, string mesAniofinal, string sucursal, string idRik)
        {
            try
            {
                string Emp_CnxCentral = ConfigurationManager.AppSettings.Get("strConnectionCentral");
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
                if (Sesion == null)
                {
                    return JsonConvert.SerializeObject(new { id = 0 });
                }
                CN_Presupuesto cdpresupuesto = new CN_Presupuesto();
                CatPresupuesto Presupuesto = new CatPresupuesto();
                List<CatPresupuesto> listaPresupuesto = new List<CatPresupuesto>();
                CN_Presupuesto presupuesto = new CN_Presupuesto();
                List<CatPresupuesto> listaPresupuesto2 = new List<CatPresupuesto>();

                Presupuesto.Id_Emp = Sesion.Id_Emp;
                Presupuesto.Id_Cd = int.Parse(sucursal);
                if (Convert.ToInt32(sucursal) == -1)
                {

                    return JsonConvert.SerializeObject(new { id = 1 });
                }
                else if (mesAnioInicial == "" || mesAniofinal == "")
                {
                    return JsonConvert.SerializeObject(new { id = 3 });
                }
                else if (Convert.ToInt32(idRik) == -1)
                {
                    return JsonConvert.SerializeObject(new { id = 2 });
                }
                else
                {

                    Presupuesto.MesInicial = DateTime.Parse(mesAnioInicial).Month;
                    Presupuesto.AnioInicial = DateTime.Parse(mesAnioInicial).Year;
                    Presupuesto.MesFinal = DateTime.Parse(mesAniofinal).Month;
                    Presupuesto.AnioFinal = DateTime.Parse(mesAniofinal).Year;

                    DateTime FechaInicial2 = DateTime.Parse(mesAnioInicial);
                    DateTime FechaFinal2 = DateTime.Parse(mesAniofinal).AddMonths(1).AddDays(-1);
                    Presupuesto.FechaInicial = FechaInicial2;
                    Presupuesto.fechafinal = FechaFinal2;
                    Presupuesto.Id_Rik = Convert.ToInt32(idRik);
                    Presupuesto.Id_u = Convert.ToInt32(idRik);

                    cdpresupuesto.ConsultaUtilidadRIkxmes(Presupuesto, ref listaPresupuesto, Emp_CnxCentral);
                    presupuesto.ConsultaPresupuestoMesualPvvRIk(Presupuesto, ref listaPresupuesto2, Emp_CnxCentral);


                    DateTime AnioInicial = DateTime.Parse(mesAnioInicial);
                    DateTime AnioFinal = DateTime.Parse(mesAniofinal);
                    if (AnioInicial > AnioFinal)
                    {
                        return JsonConvert.SerializeObject(new { id = 4 });
                    }

                    string titulo = "";

                    string datos = "";
                    string datos2 = "";

                    for (var i = AnioInicial; i <= AnioFinal;)
                    {

                        List<CatPresupuesto> lista2 = (from tlist in listaPresupuesto
                                                       where tlist.Id_Rik == Convert.ToInt32(idRik) &&
                                                       tlist.fecha == AnioInicial
                                                       select tlist).ToList();

                        List<CatPresupuesto> lista = (from tlist in listaPresupuesto2
                                                      where tlist.Id_Rik == Convert.ToInt32(idRik) &&
                                                      tlist.fecha == AnioInicial
                                                      select tlist).ToList();

                        int NumeroMes = AnioInicial.Month;
                        string mes = "";

                        if (NumeroMes == 1)
                        {
                            mes = "Enero";
                        }
                        if (NumeroMes == 2)
                        {
                            mes = "Febrero";
                        }
                        if (NumeroMes == 3)
                        {
                            mes = "Marzo";
                        }
                        if (NumeroMes == 4)
                        {
                            mes = "Abril";
                        }
                        if (NumeroMes == 5)
                        {
                            mes = "Mayo";
                        }
                        if (NumeroMes == 6)
                        {
                            mes = "Junio";
                        }
                        if (NumeroMes == 7)
                        {
                            mes = "Julio";
                        }
                        if (NumeroMes == 8)
                        {
                            mes = "Agosto";
                        }
                        if (NumeroMes == 9)
                        {
                            mes = "Septiembre";
                        }
                        if (NumeroMes == 10)
                        {
                            mes = "Octubre";
                        }
                        if (NumeroMes == 11)
                        {
                            mes = "Noviembre";
                        }
                        if (NumeroMes == 12)
                        {
                            mes = "Diciembre";
                        }

                        if (titulo == "")
                        {
                            titulo = mes + " " + AnioInicial.Year.ToString();
                        }
                        else
                        {
                            titulo = titulo + "," + mes + " " + AnioInicial.Year.ToString();
                        }

                        if (lista2.Count() == 0)
                        {
                            if (datos == "")
                            {
                                datos = "0";
                            }
                            else
                            {
                                datos = datos + "," + "0";
                            }
                        }
                        else
                        {
                            foreach (CatPresupuesto registro in lista2)
                            {
                                if (datos == "")
                                {
                                    datos = registro.venta.ToString("F2");
                                }
                                else
                                {
                                    datos = datos + "," + registro.venta.ToString("F2");
                                }
                            }
                        }

                        if (lista.Count() == 0)
                        {
                            if (datos2 == "")
                            {
                                datos2 = "0";
                            }
                            else
                            {
                                datos2 = datos2 + "," + "0";
                            }
                        }
                        else
                        {
                            foreach (CatPresupuesto registro2 in lista)
                            {
                                if (datos2 == "")
                                {
                                    datos2 = registro2.TotalPresupuesto.ToString("F2");
                                }
                                else
                                {
                                    datos2 = datos2 + "," + registro2.TotalPresupuesto.ToString("F2");
                                }
                            }
                        }

                        AnioInicial = AnioInicial.AddMonths(1);
                        i = AnioInicial;
                    }

                    return JsonConvert.SerializeObject(new { id = 5, titulo = titulo, datos = datos, datos2 = datos2 });
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = ex.Message });
            }
        }



        [WebMethod]
        public static string btnIntegrarLeads_ServerClick(string mesAnioInicial, string mesAniofinal, string Seleccion)
        {
            try
            {
                string Emp_CnxCentral = ConfigurationManager.AppSettings.Get("strConnectionCentral");
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
                if (Sesion == null)
                {
                    return JsonConvert.SerializeObject(new { id = 0 });
                }
                CN_Presupuesto cdpresupuesto = new CN_Presupuesto();
                CatPresupuesto Presupuesto = new CatPresupuesto();
                List<CatPresupuesto> listaPresupuesto = new List<CatPresupuesto>();
                CN_Leads presupuesto = new CN_Leads();
                List<CatPresupuesto> listaPresupuesto2 = new List<CatPresupuesto>();
                CN_ReporteCRM CN = new CN_ReporteCRM();

                string sucursales = Sesion.Id_Cd_Ver.ToString();

                DateTime AnioInicial = DateTime.Parse(mesAnioInicial);
                DateTime AnioFinal = DateTime.Parse(mesAniofinal);
                if (AnioInicial > AnioFinal)
                {
                    return JsonConvert.SerializeObject(new { id = 4 });
                }

                string titulo = "";

                string datos = "";
                string datos2 = "";
                string datos3 = "";
                string datos4 = "";
                string datos5 = "";
                string datos6 = "";
                string datos7 = "";
                string datos8 = "";
                string datos9 = "";
                string titsucursal = "";

                List<CatPresupuesto> lista = new List<CatPresupuesto>();
                List<CatPresupuesto> lista2 = new List<CatPresupuesto>();
                List<CatPresupuesto> lista3 = new List<CatPresupuesto>();
                List<CatPresupuesto> lista4 = new List<CatPresupuesto>();
                List<CatPresupuesto> lista5 = new List<CatPresupuesto>();
                List<CatPresupuesto> lista6 = new List<CatPresupuesto>();
                List<CatPresupuesto> lista7 = new List<CatPresupuesto>();
                List<CatPresupuesto> lista8 = new List<CatPresupuesto>();
                List<CatPresupuesto> lista9 = new List<CatPresupuesto>();

                int contador = 0;

                if (Seleccion == "0") //si es venta hace esto
                {

                    List<int> ListaSucursales = new List<int>();
                    string[] id_rik = sucursales.Split(',');
                    foreach (var value in id_rik)
                    {
                        ListaSucursales.Add(Convert.ToInt32(value));
                    }

                    foreach (int id in ListaSucursales)
                    {

                        string nombreCdi = "";

                        CN.consultaCDI2(Sesion.Id_Emp, id, ref nombreCdi, Emp_CnxCentral);

                        if (nombreCdi == "")
                        {
                            nombreCdi = " - ";
                        }

                        if (titsucursal == "")
                        {
                            titsucursal = nombreCdi;
                        }
                        else
                        {
                            titsucursal = titsucursal + "," + nombreCdi;
                        }

                        Presupuesto.Id_Emp = Sesion.Id_Emp;
                        Presupuesto.Id_Cd = id;
                        //Valido si eligio al menos una sucursal , fechas y el Rik 
                        if (Convert.ToInt32(id) == -1)
                        {

                            return JsonConvert.SerializeObject(new { id = 1 });
                        }
                        else if (mesAnioInicial == "" || mesAniofinal == "")
                        {
                            return JsonConvert.SerializeObject(new { id = 3 });
                        }
                        else
                        {

                            Presupuesto.MesInicial = DateTime.Parse(mesAnioInicial).Month;
                            Presupuesto.AnioInicial = DateTime.Parse(mesAnioInicial).Year;
                            Presupuesto.MesFinal = DateTime.Parse(mesAniofinal).Month;
                            Presupuesto.AnioFinal = DateTime.Parse(mesAniofinal).Year;

                            DateTime FechaInicial2 = DateTime.Parse(mesAnioInicial);
                            DateTime FechaFinal2 = DateTime.Parse(mesAniofinal).AddMonths(1).AddDays(-1);
                            Presupuesto.FechaInicial = FechaInicial2;
                            Presupuesto.fechafinal = FechaFinal2;
                            Presupuesto.Id_Rik = 0;
                            Presupuesto.Id_u = 0;
                            listaPresupuesto2.Clear();
                            //cdpresupuesto.ConsultaUtilidadRIkxmes(Presupuesto, ref listaPresupuesto, Emp_CnxCentral);
                            presupuesto.ConsultaIntegrarLeads(Presupuesto, 0, ref listaPresupuesto2, Emp_CnxCentral);


                            AnioInicial = DateTime.Parse(mesAnioInicial);
                            AnioFinal = DateTime.Parse(mesAniofinal);

                            contador++;
                            for (var i = AnioInicial; i <= AnioFinal;)
                            {

                                int NumeroMes = AnioInicial.Month;
                                string mes = "";


                                if (contador == 1)
                                {

                                    if (NumeroMes == 1)
                                    {
                                        mes = "Enero";
                                    }
                                    if (NumeroMes == 2)
                                    {
                                        mes = "Febrero";
                                    }
                                    if (NumeroMes == 3)
                                    {
                                        mes = "Marzo";
                                    }
                                    if (NumeroMes == 4)
                                    {
                                        mes = "Abril";
                                    }
                                    if (NumeroMes == 5)
                                    {
                                        mes = "Mayo";
                                    }
                                    if (NumeroMes == 6)
                                    {
                                        mes = "Junio";
                                    }
                                    if (NumeroMes == 7)
                                    {
                                        mes = "Julio";
                                    }
                                    if (NumeroMes == 8)
                                    {
                                        mes = "Agosto";
                                    }
                                    if (NumeroMes == 9)
                                    {
                                        mes = "Septiembre";
                                    }
                                    if (NumeroMes == 10)
                                    {
                                        mes = "Octubre";
                                    }
                                    if (NumeroMes == 11)
                                    {
                                        mes = "Noviembre";
                                    }
                                    if (NumeroMes == 12)
                                    {
                                        mes = "Diciembre";
                                    }
                                    if (titulo == "")
                                    {
                                        titulo = mes + " " + AnioInicial.Year.ToString();
                                    }
                                    else
                                    {
                                        titulo = titulo + "," + mes + " " + AnioInicial.Year.ToString();
                                    }


                                    lista2 = (from tlist in listaPresupuesto2
                                              where tlist.fecha == AnioInicial
                                              select tlist).ToList();


                                    if (lista2.Count() == 0)
                                    {
                                        if (datos == "")
                                        {
                                            datos = "0";
                                        }
                                        else
                                        {
                                            datos = datos + "," + "0";
                                        }
                                    }
                                    else
                                    {
                                        foreach (CatPresupuesto registro in lista2)
                                        {
                                            if (datos == "")
                                            {
                                                datos = registro.cantidad.ToString();
                                            }
                                            else
                                            {
                                                datos = datos + "," + registro.cantidad.ToString();
                                            }
                                        }
                                    }

                                }
                                if (contador == 2)
                                {
                                    lista = (from tlist in listaPresupuesto2
                                             where tlist.fecha == AnioInicial
                                             select tlist).ToList();

                                    if (lista.Count() == 0)
                                    {
                                        if (datos2 == "")
                                        {
                                            datos2 = "0";
                                        }
                                        else
                                        {
                                            datos2 = datos2 + "," + "0";
                                        }
                                    }
                                    else
                                    {
                                        foreach (CatPresupuesto registro2 in lista)
                                        {
                                            if (datos2 == "")
                                            {
                                                datos2 = registro2.TotalPresupuesto.ToString();
                                            }
                                            else
                                            {
                                                datos2 = datos2 + "," + registro2.TotalPresupuesto.ToString();
                                            }
                                        }
                                    }


                                }
                                if (contador == 3)
                                {
                                    lista3 = (from tlist in listaPresupuesto2
                                              where tlist.fecha == AnioInicial
                                              select tlist).ToList();


                                    if (lista3.Count() == 0)
                                    {
                                        if (datos3 == "")
                                        {
                                            datos3 = "0";
                                        }
                                        else
                                        {
                                            datos3 = datos3 + "," + "0";
                                        }
                                    }
                                    else
                                    {
                                        foreach (CatPresupuesto registro in lista3)
                                        {
                                            if (datos3 == "")
                                            {
                                                datos3 = registro.cantidad.ToString();
                                            }
                                            else
                                            {
                                                datos3 = datos3 + "," + registro.cantidad.ToString();
                                            }
                                        }
                                    }


                                }

                                if (contador == 4)
                                {
                                    lista4 = (from tlist in listaPresupuesto2
                                              where tlist.fecha == AnioInicial
                                              select tlist).ToList();

                                    if (lista4.Count() == 0)
                                    {
                                        if (datos4 == "")
                                        {
                                            datos4 = "0";
                                        }
                                        else
                                        {
                                            datos4 = datos4 + "," + "0";
                                        }
                                    }
                                    else
                                    {
                                        foreach (CatPresupuesto registro in lista4)
                                        {
                                            if (datos4 == "")
                                            {
                                                datos4 = registro.cantidad.ToString();
                                            }
                                            else
                                            {
                                                datos4 = datos4 + "," + registro.cantidad.ToString();
                                            }
                                        }
                                    }


                                }
                                if (contador == 5)
                                {
                                    lista5 = (from tlist in listaPresupuesto2
                                              where tlist.fecha == AnioInicial
                                              select tlist).ToList();

                                    if (lista5.Count() == 0)
                                    {
                                        if (datos5 == "")
                                        {
                                            datos5 = "0";
                                        }
                                        else
                                        {
                                            datos5 = datos5 + "," + "0";
                                        }
                                    }
                                    else
                                    {
                                        foreach (CatPresupuesto registro in lista5)
                                        {
                                            if (datos5 == "")
                                            {
                                                datos5 = registro.cantidad.ToString();
                                            }
                                            else
                                            {
                                                datos5 = datos5 + "," + registro.cantidad.ToString();
                                            }
                                        }
                                    }

                                }
                                if (contador == 6)
                                {
                                    lista6 = (from tlist in listaPresupuesto2
                                              where tlist.fecha == AnioInicial
                                              select tlist).ToList();

                                    if (lista6.Count() == 0)
                                    {
                                        if (datos6 == "")
                                        {
                                            datos6 = "0";
                                        }
                                        else
                                        {
                                            datos6 = datos6 + "," + "0";
                                        }
                                    }
                                    else
                                    {
                                        foreach (CatPresupuesto registro in lista6)
                                        {
                                            if (datos6 == "")
                                            {
                                                datos6 = registro.cantidad.ToString();
                                            }
                                            else
                                            {
                                                datos6 = datos6 + "," + registro.cantidad.ToString();
                                            }
                                        }
                                    }
                                }
                                if (contador == 7)
                                {
                                    lista7 = (from tlist in listaPresupuesto2
                                              where tlist.fecha == AnioInicial
                                              select tlist).ToList();

                                    if (lista7.Count() == 0)
                                    {
                                        if (datos7 == "")
                                        {
                                            datos7 = "0";
                                        }
                                        else
                                        {
                                            datos7 = datos7 + "," + "0";
                                        }
                                    }
                                    else
                                    {
                                        foreach (CatPresupuesto registro in lista7)
                                        {
                                            if (datos7 == "")
                                            {
                                                datos7 = registro.cantidad.ToString();
                                            }
                                            else
                                            {
                                                datos7 = datos7 + "," + registro.cantidad.ToString();
                                            }
                                        }
                                    }
                                }
                                if (contador == 8)
                                {
                                    lista8 = (from tlist in listaPresupuesto2
                                              where tlist.fecha == AnioInicial
                                              select tlist).ToList();

                                    if (lista8.Count() == 0)
                                    {
                                        if (datos8 == "")
                                        {
                                            datos8 = "0";
                                        }
                                        else
                                        {
                                            datos8 = datos8 + "," + "0";
                                        }
                                    }
                                    else
                                    {
                                        foreach (CatPresupuesto registro in lista8)
                                        {
                                            if (datos8 == "")
                                            {
                                                datos8 = registro.cantidad.ToString();
                                            }
                                            else
                                            {
                                                datos8 = datos8 + "," + registro.cantidad.ToString();
                                            }
                                        }
                                    }

                                }
                                if (contador == 9)
                                {
                                    lista9 = (from tlist in listaPresupuesto2
                                              where tlist.fecha == AnioInicial
                                              select tlist).ToList();

                                    if (lista9.Count() == 0)
                                    {
                                        if (datos9 == "")
                                        {
                                            datos9 = "0";
                                        }
                                        else
                                        {
                                            datos9 = datos9 + "," + "0";
                                        }
                                    }
                                    else
                                    {
                                        foreach (CatPresupuesto registro in lista9)
                                        {
                                            if (datos9 == "")
                                            {
                                                datos9 = registro.cantidad.ToString();
                                            }
                                            else
                                            {
                                                datos9 = datos9 + "," + registro.cantidad.ToString();
                                            }
                                        }
                                    }
                                }






                                AnioInicial = AnioInicial.AddMonths(1);
                                i = AnioInicial;
                            }
                            //return aqui estaba

                        }
                    }

                }
                else
                {

                    //Ventas
                    List<int> ListaSucursales = new List<int>();
                    string[] id_rik = sucursales.Split(',');
                    foreach (var value in id_rik)
                    {
                        ListaSucursales.Add(Convert.ToInt32(value));
                    }

                    foreach (int id in ListaSucursales)
                    {

                        string nombreCdi = "";

                        CN.consultaCDI2(Sesion.Id_Emp, id, ref nombreCdi, Emp_CnxCentral);

                        if (nombreCdi == "")
                        {
                            nombreCdi = " - ";
                        }
                        if (titsucursal == "")
                        {
                            titsucursal = nombreCdi;
                        }
                        else
                        {
                            titsucursal = titsucursal + "," + nombreCdi;
                        }


                        Presupuesto.Id_Emp = Sesion.Id_Emp;
                        Presupuesto.Id_Cd = id;
                        //Valido si eligio al menos una sucursal , fechas y el Rik 
                        if (Convert.ToInt32(id) == -1)
                        {

                            return JsonConvert.SerializeObject(new { id = 1 });
                        }
                        else if (mesAnioInicial == "" || mesAniofinal == "")
                        {
                            return JsonConvert.SerializeObject(new { id = 3 });
                        }
                        else
                        {

                            Presupuesto.MesInicial = DateTime.Parse(mesAnioInicial).Month;
                            Presupuesto.AnioInicial = DateTime.Parse(mesAnioInicial).Year;
                            Presupuesto.MesFinal = DateTime.Parse(mesAniofinal).Month;
                            Presupuesto.AnioFinal = DateTime.Parse(mesAniofinal).Year;

                            DateTime FechaInicial2 = DateTime.Parse(mesAnioInicial);
                            DateTime FechaFinal2 = DateTime.Parse(mesAniofinal).AddMonths(1).AddDays(-1);
                            Presupuesto.FechaInicial = FechaInicial2;
                            Presupuesto.fechafinal = FechaFinal2;
                            Presupuesto.Id_Rik = 0;
                            Presupuesto.Id_u = 0;
                            listaPresupuesto2.Clear();
                            //cdpresupuesto.ConsultaUtilidadRIkxmes(Presupuesto, ref listaPresupuesto, Emp_CnxCentral);
                            presupuesto.ConsultaIntegrarLeads(Presupuesto, 1, ref listaPresupuesto2, Emp_CnxCentral);


                            AnioInicial = DateTime.Parse(mesAnioInicial);
                            AnioFinal = DateTime.Parse(mesAniofinal);

                            contador++;
                            for (var i = AnioInicial; i <= AnioFinal;)
                            {

                                int NumeroMes = AnioInicial.Month;
                                string mes = "";


                                if (contador == 1)
                                {
                                    if (NumeroMes == 1)
                                    {
                                        mes = "Enero";
                                    }
                                    if (NumeroMes == 2)
                                    {
                                        mes = "Febrero";
                                    }
                                    if (NumeroMes == 3)
                                    {
                                        mes = "Marzo";
                                    }
                                    if (NumeroMes == 4)
                                    {
                                        mes = "Abril";
                                    }
                                    if (NumeroMes == 5)
                                    {
                                        mes = "Mayo";
                                    }
                                    if (NumeroMes == 6)
                                    {
                                        mes = "Junio";
                                    }
                                    if (NumeroMes == 7)
                                    {
                                        mes = "Julio";
                                    }
                                    if (NumeroMes == 8)
                                    {
                                        mes = "Agosto";
                                    }
                                    if (NumeroMes == 9)
                                    {
                                        mes = "Septiembre";
                                    }
                                    if (NumeroMes == 10)
                                    {
                                        mes = "Octubre";
                                    }
                                    if (NumeroMes == 11)
                                    {
                                        mes = "Noviembre";
                                    }
                                    if (NumeroMes == 12)
                                    {
                                        mes = "Diciembre";
                                    }
                                    if (titulo == "")
                                    {
                                        titulo = mes + " " + AnioInicial.Year.ToString();
                                    }
                                    else
                                    {
                                        titulo = titulo + "," + mes + " " + AnioInicial.Year.ToString();
                                    }


                                    lista2 = (from tlist in listaPresupuesto2
                                              where tlist.fecha == AnioInicial
                                              select tlist).ToList();


                                    if (lista2.Count() == 0)
                                    {
                                        if (datos == "")
                                        {
                                            datos = "0";
                                        }
                                        else
                                        {
                                            datos = datos + "," + "0";
                                        }
                                    }
                                    else
                                    {
                                        foreach (CatPresupuesto registro in lista2)
                                        {
                                            if (datos == "")
                                            {
                                                datos = registro.venta.ToString("F2");
                                            }
                                            else
                                            {
                                                datos = datos + "," + registro.venta.ToString("F2");
                                            }
                                        }
                                    }

                                }
                                if (contador == 2)
                                {
                                    lista = (from tlist in listaPresupuesto2
                                             where tlist.fecha == AnioInicial
                                             select tlist).ToList();

                                    if (lista.Count() == 0)
                                    {
                                        if (datos2 == "")
                                        {
                                            datos2 = "0";
                                        }
                                        else
                                        {
                                            datos2 = datos2 + "," + "0";
                                        }
                                    }
                                    else
                                    {
                                        foreach (CatPresupuesto registro2 in lista)
                                        {
                                            if (datos2 == "")
                                            {
                                                datos2 = registro2.venta.ToString("F2");
                                            }
                                            else
                                            {
                                                datos2 = datos2 + "," + registro2.venta.ToString("F2");
                                            }
                                        }
                                    }


                                }
                                if (contador == 3)
                                {
                                    lista3 = (from tlist in listaPresupuesto2
                                              where tlist.fecha == AnioInicial
                                              select tlist).ToList();


                                    if (lista3.Count() == 0)
                                    {
                                        if (datos3 == "")
                                        {
                                            datos3 = "0";
                                        }
                                        else
                                        {
                                            datos3 = datos3 + "," + "0";
                                        }
                                    }
                                    else
                                    {
                                        foreach (CatPresupuesto registro in lista3)
                                        {
                                            if (datos3 == "")
                                            {
                                                datos3 = registro.venta.ToString("F2");
                                            }
                                            else
                                            {
                                                datos3 = datos3 + "," + registro.venta.ToString("F2");
                                            }
                                        }
                                    }


                                }
                                if (contador == 4)
                                {
                                    lista4 = (from tlist in listaPresupuesto2
                                              where tlist.fecha == AnioInicial
                                              select tlist).ToList();

                                    if (lista4.Count() == 0)
                                    {
                                        if (datos4 == "")
                                        {
                                            datos4 = "0";
                                        }
                                        else
                                        {
                                            datos4 = datos4 + "," + "0";
                                        }
                                    }
                                    else
                                    {
                                        foreach (CatPresupuesto registro in lista4)
                                        {
                                            if (datos4 == "")
                                            {
                                                datos4 = registro.venta.ToString("F2");
                                            }
                                            else
                                            {
                                                datos4 = datos4 + "," + registro.venta.ToString("F2");
                                            }
                                        }
                                    }


                                }
                                if (contador == 5)
                                {
                                    lista5 = (from tlist in listaPresupuesto2
                                              where tlist.fecha == AnioInicial
                                              select tlist).ToList();

                                    if (lista5.Count() == 0)
                                    {
                                        if (datos5 == "")
                                        {
                                            datos5 = "0";
                                        }
                                        else
                                        {
                                            datos5 = datos5 + "," + "0";
                                        }
                                    }
                                    else
                                    {
                                        foreach (CatPresupuesto registro in lista5)
                                        {
                                            if (datos5 == "")
                                            {
                                                datos5 = registro.venta.ToString("F2");
                                            }
                                            else
                                            {
                                                datos5 = datos5 + "," + registro.venta.ToString("F2");
                                            }
                                        }
                                    }

                                }
                                if (contador == 6)
                                {
                                    lista6 = (from tlist in listaPresupuesto2
                                              where tlist.fecha == AnioInicial
                                              select tlist).ToList();

                                    if (lista6.Count() == 0)
                                    {
                                        if (datos6 == "")
                                        {
                                            datos6 = "0";
                                        }
                                        else
                                        {
                                            datos6 = datos6 + "," + "0";
                                        }
                                    }
                                    else
                                    {
                                        foreach (CatPresupuesto registro in lista6)
                                        {
                                            if (datos6 == "")
                                            {
                                                datos6 = registro.venta.ToString("F2");
                                            }
                                            else
                                            {
                                                datos6 = datos6 + "," + registro.venta.ToString("F2");
                                            }
                                        }
                                    }
                                }
                                if (contador == 7)
                                {
                                    lista7 = (from tlist in listaPresupuesto2
                                              where tlist.fecha == AnioInicial
                                              select tlist).ToList();

                                    if (lista7.Count() == 0)
                                    {
                                        if (datos7 == "")
                                        {
                                            datos7 = "0";
                                        }
                                        else
                                        {
                                            datos7 = datos7 + "," + "0";
                                        }
                                    }
                                    else
                                    {
                                        foreach (CatPresupuesto registro in lista7)
                                        {
                                            if (datos7 == "")
                                            {
                                                datos7 = registro.venta.ToString("F2");
                                            }
                                            else
                                            {
                                                datos7 = datos7 + "," + registro.venta.ToString("F2");
                                            }
                                        }
                                    }
                                }
                                if (contador == 8)
                                {
                                    lista8 = (from tlist in listaPresupuesto2
                                              where tlist.fecha == AnioInicial
                                              select tlist).ToList();

                                    if (lista8.Count() == 0)
                                    {
                                        if (datos8 == "")
                                        {
                                            datos8 = "0";
                                        }
                                        else
                                        {
                                            datos8 = datos8 + "," + "0";
                                        }
                                    }
                                    else
                                    {
                                        foreach (CatPresupuesto registro in lista8)
                                        {
                                            if (datos8 == "")
                                            {
                                                datos8 = registro.venta.ToString("F2");
                                            }
                                            else
                                            {
                                                datos8 = datos8 + "," + registro.venta.ToString("F2");
                                            }
                                        }
                                    }

                                }
                                if (contador == 9)
                                {
                                    lista9 = (from tlist in listaPresupuesto2
                                              where tlist.fecha == AnioInicial
                                              select tlist).ToList();

                                    if (lista9.Count() == 0)
                                    {
                                        if (datos9 == "")
                                        {
                                            datos9 = "0";
                                        }
                                        else
                                        {
                                            datos9 = datos9 + "," + "0";
                                        }
                                    }
                                    else
                                    {
                                        foreach (CatPresupuesto registro in lista9)
                                        {
                                            if (datos9 == "")
                                            {
                                                datos9 = registro.venta.ToString("F2");
                                            }
                                            else
                                            {
                                                datos9 = datos9 + "," + registro.venta.ToString("F2");
                                            }
                                        }
                                    }
                                }






                                AnioInicial = AnioInicial.AddMonths(1);
                                i = AnioInicial;
                            }
                            //return aqui estaba

                        }
                    }
                    //regresa el valor de los datos de las ventas
                    return JsonConvert.SerializeObject(new { id = 5, titulo = titulo, datos = datos, datos2 = datos2, datos3 = datos3, datos4 = datos4, datos5 = datos5, datos6 = datos6, datos7 = datos7, datos8 = datos8, datos9 = datos9, titsucursal = titsucursal, cs = contador });


                }

                if (Seleccion == "0")
                    return JsonConvert.SerializeObject(new { id = 5, titulo = titulo, datos = datos, datos2 = datos2, datos3 = datos3, datos4 = datos4, datos5 = datos5, datos6 = datos6, datos7 = datos7, datos8 = datos8, datos9 = datos9, titsucursal = titsucursal, cs = contador });
                else
                    return JsonConvert.SerializeObject(new { id = 6, titulo = titulo, datos = datos, datos2 = datos2, datos3 = datos3, datos4 = datos4, datos5 = datos5, datos6 = datos6, datos7 = datos7, datos8 = datos8, datos9 = datos9, titsucursal = titsucursal, cs = contador });


            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = ex.Message });
            }
        }

        //[ScriptMethod]
        [WebMethod(EnableSession = true)]
        public static string Efectividad(string Seleccion, string sucursal, string rik, string mesAnioInicial, string mesAniofinal)
        {
            try
            {
                string Emp_CnxCentral = ConfigurationManager.AppSettings.Get("strConnectionCentral");
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];

                CN_ReporteCRM CN = new CN_ReporteCRM();
                List<ReporteCRM> listaReporteCRM = new List<ReporteCRM>();

                ReporteCRM RegistroReporteCRM = new ReporteCRM();

                if (sucursal == "-1")
                {
                    return JsonConvert.SerializeObject(new { id = 1 });
                }
                if (mesAnioInicial == "" && mesAniofinal == "")
                {
                    return JsonConvert.SerializeObject(new { id = 2 });
                }
                if (DateTime.Parse(mesAnioInicial) > DateTime.Parse(mesAniofinal))
                {
                    return JsonConvert.SerializeObject(new { id = 4 });
                }

                List<ReporteCRM> listaReportepie = new List<ReporteCRM>();


                RegistroReporteCRM.Id_Emp = Sesion.Id_Emp;
                RegistroReporteCRM.Id_Cd = Convert.ToInt32(sucursal);

                RegistroReporteCRM.Id_Rik = rik;
                RegistroReporteCRM.TipoVenta = Convert.ToInt32(Seleccion);
                RegistroReporteCRM.fechainicio = DateTime.Parse(mesAnioInicial);
                RegistroReporteCRM.fechafinal = DateTime.Parse(mesAniofinal);

                CN.ConsultaReporteGraficaCantidad(RegistroReporteCRM, ref listaReportepie, Emp_CnxCentral);

                string titulo2 = "";
                string datos2 = "";
                foreach (ReporteCRM presupuesto in listaReportepie)
                {
                    if (titulo2 == "")
                    {
                        titulo2 = presupuesto.Nombre;
                        datos2 = presupuesto.Total.ToString("F2");
                    }
                    else
                    {
                        titulo2 = titulo2 + "," + presupuesto.Nombre;
                        datos2 = datos2 + "," + presupuesto.Total.ToString("F2");
                    }
                }



                CN.ConsultaReporteGrafica(RegistroReporteCRM, ref listaReporteCRM, Emp_CnxCentral);



                string titulo = "";
                string datos = "";
                foreach (ReporteCRM presupuesto in listaReporteCRM)
                {
                    if (titulo == "")
                    {
                        titulo = presupuesto.Nombre;
                        datos = presupuesto.Total.ToString("F2");
                    }
                    else
                    {
                        titulo = titulo + "," + presupuesto.Nombre;
                        datos = datos + "," + presupuesto.Total.ToString("F2");
                    }
                }
                return JsonConvert.SerializeObject(new { id = 3, titulo = titulo, datos = datos, titulo2 = titulo2, datos2 = datos2 });



            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = ex.Message });
            }
        }

        //Efectividad cuando elige sucursales 
        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static string EfectividadSucursales(string Seleccion, string sucursales, string mesAnioInicial, string mesAniofinal, string representantes)
        {
            try
            {
                string Emp_CnxCentral = ConfigurationManager.AppSettings.Get("strConnectionCentral");
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];

                CN_ReporteCRM CN = new CN_ReporteCRM();
                CN_Leads CNLEADS = new CN_Leads();

                ReporteCRM RegistroReporteCRM = new ReporteCRM();

                decimal totalEnDesarrollo = 0;
                decimal totalEnCierre = 0;
                decimal totalCancelados = 0;

                if (sucursales == " ")
                {
                    return JsonConvert.SerializeObject(new { id = 1 });
                }
                else if (mesAnioInicial == "" && mesAniofinal == "")
                {
                    return JsonConvert.SerializeObject(new { id = 2 });
                }
                else if (DateTime.Parse(mesAnioInicial) > DateTime.Parse(mesAniofinal))
                {
                    return JsonConvert.SerializeObject(new { id = 3 });
                }
                else
                {


                    List<int> ListaSucursaless = new List<int>();
                    string[] id_sucursal = sucursales.Split(',');
                    if (id_sucursal.Count() == 1)
                    {
                        if (representantes == "")
                        {
                            return JsonConvert.SerializeObject(new { id = 6 });
                        }

                        return EfectividadRepresentantes(Seleccion, sucursales, representantes, mesAnioInicial, mesAniofinal);
                    }


                    string nombre = "";
                    string datosanalisis = "";
                    string datosanPresentación = "";
                    string datosanNegociación = "";
                    string datosanCierre = "";
                    string datosCancelación = "";

                    //gráfica de totales Pie
                    string titulo2 = "";
                    string datos2 = "";

                    List<ReporteCRM> listaReportepieTotal = new List<ReporteCRM>();

                    if (Seleccion == "0")
                    {

                        //gráfica de pie con los totales 19 feb 2021


                        List<int> ListaSucursales = new List<int>();
                        string[] id_rik = sucursales.Split(',');
                        foreach (var value in id_rik)
                        {
                            ListaSucursales.Add(Convert.ToInt32(value));
                        }




                        foreach (int id in ListaSucursales)
                        {
                            string nombreCdi = "";

                            CN.consultaCDI2(Sesion.Id_Emp, id, ref nombreCdi, Emp_CnxCentral);

                            List<ReporteCRM> listaReporteCRM = new List<ReporteCRM>();

                            List<ReporteCRM> listaReporteCRMAnalisis = new List<ReporteCRM>();
                            List<ReporteCRM> listaReporteCRMPresentacion = new List<ReporteCRM>();
                            List<ReporteCRM> listaReporteCRMNegociacion = new List<ReporteCRM>();
                            List<ReporteCRM> listaReporteCRMCierre = new List<ReporteCRM>();
                            List<ReporteCRM> listaReporteCRMCancelacion = new List<ReporteCRM>();

                            RegistroReporteCRM.Id_Emp = Sesion.Id_Emp;
                            RegistroReporteCRM.Id_Cd = Convert.ToInt32(id);

                            RegistroReporteCRM.Id_Rik = null;
                            RegistroReporteCRM.TipoVenta = Convert.ToInt32("0");
                            RegistroReporteCRM.fechainicio = DateTime.Parse(mesAnioInicial);
                            RegistroReporteCRM.fechafinal = DateTime.Parse(mesAniofinal);
                            RegistroReporteCRM.id_proyect = 1;

                            CNLEADS.ConsultaReporteEfectividadGrafBarrasCantidad(RegistroReporteCRM, ref listaReporteCRMAnalisis, ref listaReporteCRMPresentacion,
           ref listaReporteCRMNegociacion, ref listaReporteCRMCierre, ref listaReporteCRMCancelacion, Emp_CnxCentral);

                            if (nombre == "")
                            {
                                nombre = nombreCdi;
                            }
                            else
                            {
                                nombre = nombre + "," + nombreCdi;
                            }

                            if (listaReporteCRMAnalisis.Count() == 0)
                            {
                                if (datosanalisis == "")
                                {
                                    datosanalisis = "0"; ;
                                }
                                else
                                {
                                    datosanalisis = datosanalisis + "," + "0";
                                }
                            }
                            else
                            {
                                foreach (ReporteCRM ana in listaReporteCRMAnalisis)
                                {
                                    if (datosanalisis == "")
                                    {
                                        datosanalisis = ana.Total.ToString("F2");
                                    }
                                    else
                                    {
                                        datosanalisis = datosanalisis + "," + ana.Total.ToString("F2");
                                    }
                                }
                            }

                            if (listaReporteCRMPresentacion.Count() == 0)
                            {
                                if (datosanPresentación == "")
                                {
                                    datosanPresentación = "0"; ;
                                }
                                else
                                {
                                    datosanPresentación = datosanPresentación + "," + "0";
                                }
                            }
                            else
                            {
                                foreach (ReporteCRM ana in listaReporteCRMPresentacion)
                                {
                                    if (datosanPresentación == "")
                                    {
                                        datosanPresentación = ana.Total.ToString("F2");
                                    }
                                    else
                                    {
                                        datosanPresentación = datosanPresentación + "," + ana.Total.ToString("F2");
                                    }
                                }
                            }

                            if (listaReporteCRMNegociacion.Count() == 0)
                            {
                                if (datosanNegociación == "")
                                {
                                    datosanNegociación = "0"; ;
                                }
                                else
                                {
                                    datosanNegociación = datosanNegociación + "," + "0";
                                }
                            }
                            else
                            {
                                foreach (ReporteCRM ana in listaReporteCRMNegociacion)
                                {
                                    if (datosanNegociación == "")
                                    {
                                        datosanNegociación = ana.Total.ToString("F2");
                                    }
                                    else
                                    {
                                        datosanNegociación = datosanNegociación + "," + ana.Total.ToString("F2");
                                    }
                                }
                            }

                            if (listaReporteCRMCierre.Count() == 0)
                            {
                                if (datosanCierre == "")
                                {
                                    datosanCierre = "0"; ;
                                }
                                else
                                {
                                    datosanCierre = datosanCierre + "," + "0";
                                }
                            }
                            else
                            {
                                foreach (ReporteCRM ana in listaReporteCRMCierre)
                                {
                                    if (datosanCierre == "")
                                    {
                                        datosanCierre = ana.Total.ToString("F2");
                                    }
                                    else
                                    {
                                        datosanCierre = datosanCierre + "," + ana.Total.ToString("F2");
                                    }
                                }
                            }

                            if (listaReporteCRMCancelacion.Count() == 0)
                            {
                                if (datosCancelación == "")
                                {
                                    datosCancelación = "0"; ;
                                }
                                else
                                {
                                    datosCancelación = datosCancelación + "," + "0";
                                }
                            }
                            else
                            {
                                foreach (ReporteCRM ana in listaReporteCRMCancelacion)
                                {
                                    if (datosCancelación == "")
                                    {
                                        datosCancelación = ana.Total.ToString("F2");
                                    }
                                    else
                                    {
                                        datosCancelación = datosCancelación + "," + ana.Total.ToString("F2");
                                    }
                                }
                            }


                            //gráfica de pie con los totales INICIO

                            CN_Leads CNPIE = new CN_Leads();
                            List<ReporteCRM> listaReporteCRMPIE = new List<ReporteCRM>();

                            ReporteCRM RegistroReporteCRMPIE = new ReporteCRM();


                            if (mesAnioInicial == "" && mesAniofinal == "")
                            {
                                return JsonConvert.SerializeObject(new { id = 2 });
                            }
                            if (DateTime.Parse(mesAnioInicial) > DateTime.Parse(mesAniofinal))
                            {
                                return JsonConvert.SerializeObject(new { id = 4 });
                            }

                            List<ReporteCRM> listaReportepie = new List<ReporteCRM>();

                            //JFCV revisar esta parte porque debo mandarle el listado de sucursales 
                            // o meterlo dentro del for each para que lea cada uno de ellos 
                            RegistroReporteCRMPIE.Id_Emp = Sesion.Id_Emp;
                            RegistroReporteCRMPIE.Id_Cd = Convert.ToInt32(id);

                            RegistroReporteCRMPIE.Id_Rik = null;
                            RegistroReporteCRMPIE.TipoVenta = Convert.ToInt32(Seleccion);
                            RegistroReporteCRMPIE.fechainicio = DateTime.Parse(mesAnioInicial);
                            RegistroReporteCRMPIE.fechafinal = DateTime.Parse(mesAniofinal);
                            RegistroReporteCRMPIE.id_proyect = 1;  //este campo es para que totalice por sucursal


                            CNPIE.ConsultaReporteGraficaCantidad(RegistroReporteCRMPIE, ref listaReportepie, Emp_CnxCentral);

                            //si es la primera vez que entra al ciclo de sucursales entonces 
                            // pietotal no tiene nada asi que inserto cada reistro que traiga el query
                            if (listaReportepieTotal.Count == 0)
                            {
                                foreach (ReporteCRM presupuesto in listaReportepie)
                                {
                                    listaReportepieTotal.Add(presupuesto);
                                }

                            }
                            else
                            {
                                //Si ya es la segunda o mas sucursal entonces le suma 
                                foreach (ReporteCRM presupuesto in listaReportepie)
                                {
                                    foreach (ReporteCRM presupuestotot in listaReportepieTotal)
                                    {
                                        if (presupuesto.Nombre == presupuestotot.Nombre)
                                        {
                                            presupuestotot.Total = presupuestotot.Total + presupuesto.Total;
                                        }
                                    }

                                }
                            }

                            //gráfica de pie con los totales FIN

                        }

                        //gráfica de pie con los totales Inicio
                        foreach (ReporteCRM presupuesto in listaReportepieTotal)
                        {

                            if (titulo2 == "")
                            {

                                titulo2 = presupuesto.Nombre;
                                datos2 = presupuesto.Total.ToString("F2");
                            }
                            else
                            {
                                titulo2 = titulo2 + "," + presupuesto.Nombre;
                                datos2 = datos2 + "," + presupuesto.Total.ToString("F2");
                            }

                        }

                        //gráfica de pie con los totales FIN


                        return JsonConvert.SerializeObject(new
                        {
                            id = 4,
                            Nombre = nombre,
                            Analisis = datosanalisis,
                            presentacion = datosanPresentación,
                            Negociación = datosanNegociación,
                            Cierre = datosanCierre,
                            Cancelación = datosCancelación,
                            titulo2 = titulo2,
                            datos2 = datos2
                        });

                    }
                    else
                    {
                        //gráfica por monto
                        //gráfica de pie con los totales 19 feb 2021


                        List<int> ListaSucursales = new List<int>();
                        string[] id_rik = sucursales.Split(',');
                        foreach (var value in id_rik)
                        {
                            ListaSucursales.Add(Convert.ToInt32(value));
                        }


                        foreach (int id in ListaSucursales)
                        {
                            string nombreCdi = "";

                            CN.consultaCDI2(Sesion.Id_Emp, id, ref nombreCdi, Emp_CnxCentral);

                            List<ReporteCRM> listaReporteCRM = new List<ReporteCRM>();

                            List<ReporteCRM> listaReporteCRMAnalisis = new List<ReporteCRM>();
                            List<ReporteCRM> listaReporteCRMPresentacion = new List<ReporteCRM>();
                            List<ReporteCRM> listaReporteCRMNegociacion = new List<ReporteCRM>();
                            List<ReporteCRM> listaReporteCRMCierre = new List<ReporteCRM>();
                            List<ReporteCRM> listaReporteCRMCancelacion = new List<ReporteCRM>();


                            RegistroReporteCRM.Id_Emp = Sesion.Id_Emp;
                            RegistroReporteCRM.Id_Cd = Convert.ToInt32(id);

                            RegistroReporteCRM.Id_Rik = null;
                            RegistroReporteCRM.TipoVenta = Convert.ToInt32("0");
                            RegistroReporteCRM.fechainicio = DateTime.Parse(mesAnioInicial);
                            RegistroReporteCRM.fechafinal = DateTime.Parse(mesAniofinal);

                            CNLEADS.ConsultaReporteGraficaEfectividadMonto(RegistroReporteCRM, ref listaReporteCRMAnalisis, ref listaReporteCRMPresentacion,
           ref listaReporteCRMNegociacion, ref listaReporteCRMCierre, ref listaReporteCRMCancelacion, Emp_CnxCentral);

                            if (nombre == "")
                            {
                                nombre = nombreCdi;
                            }
                            else
                            {
                                nombre = nombre + "," + nombreCdi;
                            }


                            if (listaReporteCRMAnalisis.Count() == 0)
                            {
                                if (datosanalisis == "")
                                {
                                    datosanalisis = "0"; ;
                                }
                                else
                                {
                                    datosanalisis = datosanalisis + "," + "0";
                                }
                            }
                            else
                            {
                                foreach (ReporteCRM ana in listaReporteCRMAnalisis)
                                {
                                    if (datosanalisis == "")
                                    {
                                        datosanalisis = ana.Total.ToString("F2");
                                    }
                                    else
                                    {
                                        datosanalisis = datosanalisis + "," + ana.Total.ToString("F2");
                                    }
                                    totalEnDesarrollo = totalEnDesarrollo + ana.Total;
                                }
                            }

                            if (listaReporteCRMPresentacion.Count() == 0)
                            {
                                if (datosanPresentación == "")
                                {
                                    datosanPresentación = "0"; ;
                                }
                                else
                                {
                                    datosanPresentación = datosanPresentación + "," + "0";
                                }
                            }
                            else
                            {
                                foreach (ReporteCRM ana in listaReporteCRMPresentacion)
                                {
                                    if (datosanPresentación == "")
                                    {
                                        datosanPresentación = ana.Total.ToString("F2");
                                    }
                                    else
                                    {
                                        datosanPresentación = datosanPresentación + "," + ana.Total.ToString("F2");
                                    }
                                    totalEnCierre = totalEnCierre + ana.Total;
                                }
                            }

                            if (listaReporteCRMNegociacion.Count() == 0)
                            {
                                if (datosanNegociación == "")
                                {
                                    datosanNegociación = "0"; ;
                                }
                                else
                                {
                                    datosanNegociación = datosanNegociación + "," + "0";
                                }
                            }
                            else
                            {
                                foreach (ReporteCRM ana in listaReporteCRMNegociacion)
                                {
                                    if (datosanNegociación == "")
                                    {
                                        datosanNegociación = ana.Total.ToString("F2");
                                    }
                                    else
                                    {
                                        datosanNegociación = datosanNegociación + "," + ana.Total.ToString("F2");
                                    }
                                    totalCancelados = totalCancelados + ana.Total;
                                }
                            }

                            if (listaReporteCRMCierre.Count() == 0)
                            {
                                if (datosanCierre == "")
                                {
                                    datosanCierre = "0"; ;
                                }
                                else
                                {
                                    datosanCierre = datosanCierre + "," + "0";
                                }
                            }
                            else
                            {
                                foreach (ReporteCRM ana in listaReporteCRMCierre)
                                {
                                    if (datosanCierre == "")
                                    {
                                        datosanCierre = ana.Total.ToString("F2");
                                    }
                                    else
                                    {
                                        datosanCierre = datosanCierre + "," + ana.Total.ToString("F2");
                                    }
                                }
                            }

                            if (listaReporteCRMCancelacion.Count() == 0)
                            {
                                if (datosCancelación == "")
                                {
                                    datosCancelación = "0"; ;
                                }
                                else
                                {
                                    datosCancelación = datosCancelación + "," + "0";
                                }
                            }
                            else
                            {
                                foreach (ReporteCRM ana in listaReporteCRMCancelacion)
                                {
                                    if (datosCancelación == "")
                                    {
                                        datosCancelación = ana.Total.ToString("F2");
                                    }
                                    else
                                    {
                                        datosCancelación = datosCancelación + "," + ana.Total.ToString("F2");
                                    }
                                }
                            }



                            //gráfica de pie con los totales INICIO

                            CN_Leads CNPIE = new CN_Leads();
                            List<ReporteCRM> listaReporteCRMPIE = new List<ReporteCRM>();

                            ReporteCRM RegistroReporteCRMPIE = new ReporteCRM();


                            if (mesAnioInicial == "" && mesAniofinal == "")
                            {
                                return JsonConvert.SerializeObject(new { id = 2 });
                            }
                            if (DateTime.Parse(mesAnioInicial) > DateTime.Parse(mesAniofinal))
                            {
                                return JsonConvert.SerializeObject(new { id = 4 });
                            }

                            List<ReporteCRM> listaReportepie = new List<ReporteCRM>();

                            RegistroReporteCRMPIE.Id_Emp = Sesion.Id_Emp;
                            RegistroReporteCRMPIE.Id_Cd = Convert.ToInt32(id);

                            RegistroReporteCRMPIE.Id_Rik = null;
                            RegistroReporteCRMPIE.TipoVenta = Convert.ToInt32(Seleccion);
                            RegistroReporteCRMPIE.fechainicio = DateTime.Parse(mesAnioInicial);
                            RegistroReporteCRMPIE.fechafinal = DateTime.Parse(mesAniofinal);
                            RegistroReporteCRMPIE.id_proyect = 1;  //este campo es para que totalice por sucursal


                            CNPIE.ConsultaReporteGraficaCantidad(RegistroReporteCRMPIE, ref listaReportepie, Emp_CnxCentral);

                            //si es la primera vez que entra al ciclo de sucursales entonces 
                            // pietotal no tiene nada asi que inserto cada reistro que traiga el query
                            if (listaReportepieTotal.Count == 0)
                            {
                                foreach (ReporteCRM presupuesto in listaReportepie)
                                {
                                    listaReportepieTotal.Add(presupuesto);
                                }

                            }
                            else
                            {
                                //Si ya es la segunda o mas sucursal entonces le suma 
                                foreach (ReporteCRM presupuesto in listaReportepie)
                                {
                                    foreach (ReporteCRM presupuestotot in listaReportepieTotal)
                                    {
                                        if (presupuesto.Nombre == presupuestotot.Nombre)
                                        {
                                            presupuestotot.Total = presupuestotot.Total + presupuesto.Total;
                                        }
                                    }

                                }
                            }

                            //gráfica de pie con los totales FIN

                        } //fin foreach sucursales monto 

                        //gráfica de pie con los totales Inicio
                        foreach (ReporteCRM presupuesto in listaReportepieTotal)
                        {

                            if (titulo2 == "")
                            {

                                titulo2 = presupuesto.Nombre;
                                datos2 = presupuesto.Total.ToString("F2");
                            }
                            else
                            {
                                titulo2 = titulo2 + "," + presupuesto.Nombre;
                                datos2 = datos2 + "," + presupuesto.Total.ToString("F2");
                            }

                        }
                        titulo2 = "En Desarrollo, En Cierre, Cancelados";
                        datos2 = totalEnDesarrollo.ToString("F2");
                        datos2 = datos2 + "," + totalEnCierre.ToString("F2");
                        //datos2 =  datos2 + "," + totalCancelados.ToString("F2");
                        datos2 = datos2 + ",0 ";  //LosFormatter cancelados  no puedo sumarlos

                        //gráfica de pie con los totales FIN

                        return JsonConvert.SerializeObject(new
                        {
                            id = 5,
                            Nombre = nombre,
                            Analisis = datosanalisis,
                            presentacion = datosanPresentación,
                            Negociación = datosanNegociación,
                            Cierre = datosanCierre,
                            Cancelación = datosCancelación,
                            titulo2 = titulo2,
                            datos2 = datos2
                        });

                    }  //Else selección diferente a cero ( fin de monto


                }  //Fin
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = ex.Message });
            }
        }

        /// <summary>
        /// Este web method es para calcular los datos de la gráfica de efectividad por sucursal.
        /// </summary>
        /// <param name="Seleccion"></param>
        /// <param name="mesAnioInicial"></param>
        /// <param name="mesAniofinal"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static string EfectividadRepresentantes(string Seleccion, string sucursal, string riks, string mesAnioInicial, string mesAniofinal)
        {
            try
            {

                string rik = riks;
                string Emp_CnxCentral = ConfigurationManager.AppSettings.Get("strConnectionCentral");
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];



                ReporteCRM RegistroReporteCRM = new ReporteCRM();
                CN_Leads CNLEADSPIE = new CN_Leads();


                List<int> myList = new List<int>();
                RegistroReporteCRM.Id_Emp = Sesion.Id_Emp;
                RegistroReporteCRM.Id_Cd = Convert.ToInt32(sucursal);


                if (rik != "")
                {

                    string[] id_rik = rik.Split(',');
                    foreach (var value in id_rik)
                    {
                        myList.Add(Convert.ToInt32(value));
                    }
                }

                if (mesAnioInicial == "" && mesAniofinal == "")
                {
                    return JsonConvert.SerializeObject(new { id = 2 });
                }
                if (DateTime.Parse(mesAnioInicial) > DateTime.Parse(mesAniofinal))
                {
                    return JsonConvert.SerializeObject(new { id = 3 });
                }


                string nombre = "";
                string datosanalisis = "";
                string datosanPresentación = "";
                string datosanNegociación = "";
                string datosanCierre = "";
                string datosCancelación = "";
                string titulo2 = "";
                string datos2 = "";

                decimal totalEnDesarrollo = 0;
                decimal totalEnCierre = 0;
                decimal totalCancelados = 0;


                //gráfica de PIE
                List<ReporteCRM> listaReportepieTotal = new List<ReporteCRM>();


                if (Convert.ToInt32(Seleccion) == 0)
                {

                    foreach (int nombrerik in myList)
                    {
                        List<ReporteCRM> listaReporteCRM = new List<ReporteCRM>();

                        List<ReporteCRM> listaReporteCRMAnalisis = new List<ReporteCRM>();
                        List<ReporteCRM> listaReporteCRMPresentacion = new List<ReporteCRM>();
                        List<ReporteCRM> listaReporteCRMNegociacion = new List<ReporteCRM>();
                        List<Rik> RegistroRik = (List<Rik>)HttpContext.Current.Session["nombreRik"];

                        Rik list = (from tlist in RegistroRik
                                    where tlist.id == nombrerik
                                    select tlist).FirstOrDefault();

                        RegistroReporteCRM.Id_Rik = nombrerik.ToString();
                        RegistroReporteCRM.fechainicio = DateTime.Parse(mesAnioInicial);
                        RegistroReporteCRM.fechafinal = DateTime.Parse(mesAniofinal);

                        RegistroReporteCRM.Id_Emp = Sesion.Id_Emp;
                        RegistroReporteCRM.Id_Cd = Convert.ToInt32(sucursal);
                        RegistroReporteCRM.TipoVenta = Convert.ToInt32(Seleccion);

                        //id_proyect Si vale 1 es que va a totalizar 
                        RegistroReporteCRM.id_proyect = 0;


                        CNLEADSPIE.ConsultaGraficaEfectividadCantidad(RegistroReporteCRM, ref listaReporteCRMAnalisis, ref listaReporteCRMPresentacion,
                  ref listaReporteCRMNegociacion, Emp_CnxCentral);


                        ///traerme los datos par ala gráfica de barras
                        #region gráfica de barras
                        if (nombre == "")
                        {
                            nombre = list.nombre.Substring(0, 17);
                        }
                        else
                        {
                            nombre = nombre + "," + list.nombre.Substring(0, 17);
                        }

                        if (listaReporteCRMAnalisis.Count() == 0)
                        {
                            if (datosanalisis == "")
                            {
                                datosanalisis = "0"; ;
                            }
                            else
                            {
                                datosanalisis = datosanalisis + "," + "0";
                            }
                        }
                        else
                        {
                            foreach (ReporteCRM ana in listaReporteCRMAnalisis)
                            {
                                if (datosanalisis == "")
                                {
                                    datosanalisis = ana.Total.ToString("F2");
                                }
                                else
                                {
                                    datosanalisis = datosanalisis + "," + ana.Total.ToString("F2");
                                }
                                totalEnDesarrollo = totalEnDesarrollo + ana.Total;
                            }
                        }

                        if (listaReporteCRMPresentacion.Count() == 0)
                        {
                            if (datosanPresentación == "")
                            {
                                datosanPresentación = "0"; ;
                            }
                            else
                            {
                                datosanPresentación = datosanPresentación + "," + "0";
                            }
                        }
                        else
                        {
                            foreach (ReporteCRM ana in listaReporteCRMPresentacion)
                            {
                                if (datosanPresentación == "")
                                {
                                    datosanPresentación = ana.Total.ToString("F2");
                                }
                                else
                                {
                                    datosanPresentación = datosanPresentación + "," + ana.Total.ToString("F2");
                                }
                                totalEnCierre = totalEnCierre + ana.Total;
                            }
                        }

                        if (listaReporteCRMNegociacion.Count() == 0)
                        {
                            if (datosanNegociación == "")
                            {
                                datosanNegociación = "0"; ;
                            }
                            else
                            {
                                datosanNegociación = datosanNegociación + "," + "0";
                            }
                        }
                        else
                        {
                            foreach (ReporteCRM ana in listaReporteCRMNegociacion)
                            {
                                if (datosanNegociación == "")
                                {
                                    datosanNegociación = ana.Total.ToString("F2");
                                }
                                else
                                {
                                    datosanNegociación = datosanNegociación + "," + ana.Total.ToString("F2");
                                }
                                totalCancelados = totalCancelados + ana.Total;
                            }
                        }


                        #endregion gráfica de barras

                        //gráfica de pie con los totales INICIO


                        List<ReporteCRM> listaReporteCRMPIE = new List<ReporteCRM>();

                        ReporteCRM RegistroReporteCRMPIE = new ReporteCRM();
                        List<ReporteCRM> listaReportepie = new List<ReporteCRM>();

                        //JFCV revisar esta parte porque debo mandarle el listado de sucursales 
                        // o meterlo dentro del for each para que lea cada uno de ellos 
                        RegistroReporteCRMPIE.Id_Emp = Sesion.Id_Emp;
                        RegistroReporteCRMPIE.Id_Cd = Convert.ToInt32(sucursal);

                        RegistroReporteCRMPIE.Id_Rik = nombrerik.ToString();
                        RegistroReporteCRMPIE.TipoVenta = Convert.ToInt32(Seleccion);
                        RegistroReporteCRMPIE.fechainicio = DateTime.Parse(mesAnioInicial);
                        RegistroReporteCRMPIE.fechafinal = DateTime.Parse(mesAniofinal);

                        //id_proyect Si vale 1 es que va a totalizar 
                        RegistroReporteCRMPIE.id_proyect = 0;

                        CNLEADSPIE.ConsultaReporteGraficaCantidad(RegistroReporteCRMPIE, ref listaReportepie, Emp_CnxCentral);

                        //si es la primera vez que entra al ciclo de sucursales entonces 
                        // pietotal no tiene nada asi que inserto cada reistro que traiga el query
                        if (listaReportepieTotal.Count == 0)
                        {
                            foreach (ReporteCRM presupuesto in listaReportepie)
                            {
                                listaReportepieTotal.Add(presupuesto);
                            }

                        }
                        else
                        {
                            //Si ya es la segunda o mas sucursal entonces le suma 
                            foreach (ReporteCRM presupuesto in listaReportepie)
                            {
                                foreach (ReporteCRM presupuestotot in listaReportepieTotal)
                                {
                                    if (presupuesto.Nombre == presupuestotot.Nombre)
                                    {
                                        presupuestotot.Total = presupuestotot.Total + presupuesto.Total;
                                    }
                                }

                            }
                        }

                        //gráfica de pie con los totales FIN

                    } //fin foreach sucursales monto 

                    //gráfica de pie con los totales Inicio
                    foreach (ReporteCRM presupuesto in listaReportepieTotal)
                    {

                        if (titulo2 == "")
                        {

                            titulo2 = presupuesto.Nombre;
                            datos2 = presupuesto.Total.ToString("F2");
                        }
                        else
                        {
                            titulo2 = titulo2 + "," + presupuesto.Nombre;
                            datos2 = datos2 + "," + presupuesto.Total.ToString("F2");
                        }

                    }

                    titulo2 = "En Desarrollo, En Cierre, Cancelados";
                    datos2 = totalEnDesarrollo.ToString("F2");
                    datos2 = datos2 + "," + totalEnCierre.ToString("F2");
                    datos2 = datos2 + "," + totalCancelados.ToString("F2");




                    return JsonConvert.SerializeObject(new
                    {
                        id = 4,
                        Nombre = nombre,
                        Analisis = datosanalisis,
                        presentacion = datosanPresentación,
                        Negociación = datosanNegociación,
                        Cierre = datosanCierre,
                        Cancelación = datosCancelación,
                        titulo2 = titulo2,
                        datos2 = datos2
                    });

                }
                else
                {

                    foreach (int nombrerik in myList)
                    {

                        List<ReporteCRM> listaReporteCRM = new List<ReporteCRM>();

                        List<ReporteCRM> listaReporteCRMAnalisis = new List<ReporteCRM>();
                        List<ReporteCRM> listaReporteCRMPresentacion = new List<ReporteCRM>();
                        List<ReporteCRM> listaReporteCRMNegociacion = new List<ReporteCRM>();
                        List<ReporteCRM> listaReporteCRMCierre = new List<ReporteCRM>();
                        List<ReporteCRM> listaReporteCRMCancelacion = new List<ReporteCRM>();
                        List<Rik> RegistroRik = (List<Rik>)HttpContext.Current.Session["nombreRik"];

                        Rik list = (from tlist in RegistroRik
                                    where tlist.id == nombrerik
                                    select tlist).FirstOrDefault();

                        RegistroReporteCRM.Id_Rik = nombrerik.ToString();
                        RegistroReporteCRM.fechainicio = DateTime.Parse(mesAnioInicial);
                        RegistroReporteCRM.fechafinal = DateTime.Parse(mesAniofinal);

                        CNLEADSPIE.ConsultaReporteGraficaEfectividadMonto(RegistroReporteCRM, ref listaReporteCRMAnalisis, ref listaReporteCRMPresentacion,
                  ref listaReporteCRMNegociacion, ref listaReporteCRMCierre, ref listaReporteCRMCancelacion, Emp_CnxCentral);



                        if (nombre == "")
                        {
                            nombre = list.nombre.Substring(0, 17);
                        }
                        else
                        {
                            nombre = nombre + "," + list.nombre.Substring(0, 17);
                        }

                        if (listaReporteCRMAnalisis.Count() == 0)
                        {
                            if (datosanalisis == "")
                            {
                                datosanalisis = "0"; ;
                            }
                            else
                            {
                                datosanalisis = datosanalisis + "," + "0";
                            }
                        }
                        else
                        {
                            foreach (ReporteCRM ana in listaReporteCRMAnalisis)
                            {
                                if (datosanalisis == "")
                                {
                                    datosanalisis = ana.Total.ToString("F2");
                                }
                                else
                                {
                                    datosanalisis = datosanalisis + "," + ana.Total.ToString("F2");
                                }
                                totalEnDesarrollo = totalEnDesarrollo + ana.Total;
                            }
                        }

                        if (listaReporteCRMPresentacion.Count() == 0)
                        {
                            if (datosanPresentación == "")
                            {
                                datosanPresentación = "0"; ;
                            }
                            else
                            {
                                datosanPresentación = datosanPresentación + "," + "0";
                            }
                        }
                        else
                        {
                            foreach (ReporteCRM ana in listaReporteCRMPresentacion)
                            {
                                if (datosanPresentación == "")
                                {
                                    datosanPresentación = ana.Total.ToString("F2");
                                }
                                else
                                {
                                    datosanPresentación = datosanPresentación + "," + ana.Total.ToString("F2");
                                }
                                totalEnCierre = totalEnCierre + ana.Total;
                            }
                        }

                        if (listaReporteCRMNegociacion.Count() == 0)
                        {
                            if (datosanNegociación == "")
                            {
                                datosanNegociación = "0"; ;
                            }
                            else
                            {
                                datosanNegociación = datosanNegociación + "," + "0";
                            }
                        }
                        else
                        {
                            foreach (ReporteCRM ana in listaReporteCRMNegociacion)
                            {
                                if (datosanNegociación == "")
                                {
                                    datosanNegociación = ana.Total.ToString("F2");
                                }
                                else
                                {
                                    datosanNegociación = datosanNegociación + "," + ana.Total.ToString("F2");
                                }
                                totalCancelados = totalCancelados + ana.Total;
                            }
                        }

                        if (listaReporteCRMCierre.Count() == 0)
                        {
                            if (datosanCierre == "")
                            {
                                datosanCierre = "0"; ;
                            }
                            else
                            {
                                datosanCierre = datosanCierre + "," + "0";
                            }
                        }
                        else
                        {
                            foreach (ReporteCRM ana in listaReporteCRMCierre)
                            {
                                if (datosanCierre == "")
                                {
                                    datosanCierre = ana.Total.ToString("F2");
                                }
                                else
                                {
                                    datosanCierre = datosanCierre + "," + ana.Total.ToString("F2");
                                }
                            }
                        }

                        if (listaReporteCRMCancelacion.Count() == 0)
                        {
                            if (datosCancelación == "")
                            {
                                datosCancelación = "0"; ;
                            }
                            else
                            {
                                datosCancelación = datosCancelación + "," + "0";
                            }
                        }
                        else
                        {
                            foreach (ReporteCRM ana in listaReporteCRMCancelacion)
                            {
                                if (datosCancelación == "")
                                {
                                    datosCancelación = ana.Total.ToString("F2");
                                }
                                else
                                {
                                    datosCancelación = datosCancelación + "," + ana.Total.ToString("F2");
                                }
                            }
                        }
                        //gráfica de pie con los totales INICIO


                        List<ReporteCRM> listaReporteCRMPIE = new List<ReporteCRM>();

                        ReporteCRM RegistroReporteCRMPIE = new ReporteCRM();
                        List<ReporteCRM> listaReportepie = new List<ReporteCRM>();

                        //JFCV revisar esta parte porque debo mandarle el listado de sucursales 
                        // o meterlo dentro del for each para que lea cada uno de ellos 
                        RegistroReporteCRMPIE.Id_Emp = Sesion.Id_Emp;
                        RegistroReporteCRMPIE.Id_Cd = Convert.ToInt32(sucursal);

                        RegistroReporteCRMPIE.Id_Rik = nombrerik.ToString();
                        RegistroReporteCRMPIE.TipoVenta = Convert.ToInt32(Seleccion);
                        RegistroReporteCRMPIE.fechainicio = DateTime.Parse(mesAnioInicial);
                        RegistroReporteCRMPIE.fechafinal = DateTime.Parse(mesAniofinal);

                        CNLEADSPIE.ConsultaReporteGraficaCantidad(RegistroReporteCRMPIE, ref listaReportepie, Emp_CnxCentral);

                        //si es la primera vez que entra al ciclo de sucursales entonces 
                        // pietotal no tiene nada asi que inserto cada reistro que traiga el query
                        if (listaReportepieTotal.Count == 0)
                        {
                            foreach (ReporteCRM presupuesto in listaReportepie)
                            {
                                listaReportepieTotal.Add(presupuesto);
                            }

                        }
                        else
                        {
                            //Si ya es la segunda o mas sucursal entonces le suma 
                            foreach (ReporteCRM presupuesto in listaReportepie)
                            {
                                foreach (ReporteCRM presupuestotot in listaReportepieTotal)
                                {
                                    if (presupuesto.Nombre == presupuestotot.Nombre)
                                    {
                                        presupuestotot.Total = presupuestotot.Total + presupuesto.Total;
                                    }
                                }

                            }
                        }

                        //gráfica de pie con los totales FIN

                    } //fin foreach sucursales monto 

                    //gráfica de pie con los totales Inicio
                    foreach (ReporteCRM presupuesto in listaReportepieTotal)
                    {

                        if (titulo2 == "")
                        {

                            titulo2 = presupuesto.Nombre;
                            datos2 = presupuesto.Total.ToString("F2");
                        }
                        else
                        {
                            titulo2 = titulo2 + "," + presupuesto.Nombre;
                            datos2 = datos2 + "," + presupuesto.Total.ToString("F2");
                        }

                    }

                    titulo2 = "En Desarrollo, En Cierre, Cancelados";
                    datos2 = totalEnDesarrollo.ToString("F2");
                    datos2 = datos2 + "," + totalEnCierre.ToString("F2");
                    //datos2 =  datos2 + "," + totalCancelados.ToString("F2");
                    datos2 = datos2 + ",0 ";  //LosFormatter cancelados  no puedo sumarlos


                    return JsonConvert.SerializeObject(new
                    {
                        id = 5,
                        Nombre = nombre,
                        Analisis = datosanalisis,
                        presentacion = datosanPresentación,
                        Negociación = datosanNegociación,
                        Cierre = datosanCierre,
                        Cancelación = datosCancelación,
                        titulo2 = titulo2,
                        datos2 = datos2
                    });


                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = ex.Message });
            }
        }



        //Efectividad cuando elige sucursales 
        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static string TiempoRespuesta(string Seleccion, string sucursales, string mesAnioInicial, string mesAniofinal, string representantes, string gerentes)
        {
            try
            {
                string Emp_CnxCentral = ConfigurationManager.AppSettings.Get("strConnectionCentral");
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];

                CN_ReporteCRM CN = new CN_ReporteCRM();

                CN_Leads CNLEADS = new CN_Leads();


                ReporteCRM RegistroReporteCRM = new ReporteCRM();


                if (sucursales == " ")
                {
                    return JsonConvert.SerializeObject(new { id = 1 });
                }
                else if (mesAnioInicial == "" && mesAniofinal == "")
                {
                    return JsonConvert.SerializeObject(new { id = 2 });
                }
                else if (DateTime.Parse(mesAnioInicial) > DateTime.Parse(mesAniofinal))
                {
                    return JsonConvert.SerializeObject(new { id = 3 });
                }
                else
                {


                    List<int> ListaSucursaless = new List<int>();
                    string[] id_sucursal = sucursales.Split(',');


                    if (Seleccion == "0")
                    {
                        if (representantes == "")
                        {
                            return JsonConvert.SerializeObject(new { id = 6 });
                        }
                        return TiempoRespuestaRepresentantes(Seleccion, sucursales, representantes, mesAnioInicial, mesAniofinal);
                    }


                    string nombre = "";
                    string datosanalisis = "";
                    string datosantiempolimite = "";
                    string datosfuera = "";
                    string datosanCierre = "";
                    string datosCancelación = "";

                    //gráfica de totales Pie
                    string titulo2 = "";
                    string datos2 = "";

                    List<ReporteCRM> listaReportepieTotal = new List<ReporteCRM>();

                    //if (Seleccion == "0")
                    //{

                    //gráfica de pie con los totales 19 feb 2021

                    List<int> ListaSucursales = new List<int>();
                    string[] id_rik = sucursales.Split(',');
                    foreach (var value in id_rik)
                    {
                        ListaSucursales.Add(Convert.ToInt32(value));
                    }


                    foreach (int id in ListaSucursales)
                    {
                        string nombreGerente = gerentes;

                        //CN.consultaCDI2(Sesion.Id_Emp, id, ref nombreCdi, Emp_CnxCentral);
                        CN_Leads cnlead = new CN_Leads();
                        Usuario usuario = new Usuario();
                        usuario.Id_Emp = Sesion.Id_Emp;
                        usuario.Id_Cd = Convert.ToInt32(id);
                        usuario.Id_Rik = -1;
                        ///Para que traiga los gerenets es el 3 pero para gerentes de la tabla de gerentes es 8 
                        usuario.Id_TU = 8;

                        List<Usuario> list_Gers = new List<Usuario>();

                        cnlead.ConsultaGerente(usuario, Emp_CnxCentral, ref list_Gers);

                        foreach (Usuario gtes in list_Gers)
                        {
                            nombreGerente = gtes.U_Nombre;
                        }

                        List<ReporteCRM> listaReporteCRM = new List<ReporteCRM>();

                        List<ReporteCRM> listaReporteTiempoEstandar = new List<ReporteCRM>();
                        List<ReporteCRM> listaReporteTiempoLimite = new List<ReporteCRM>();
                        List<ReporteCRM> listaReporteFuera = new List<ReporteCRM>();


                        RegistroReporteCRM.Id_Emp = Sesion.Id_Emp;
                        RegistroReporteCRM.Id_Cd = Convert.ToInt32(id);

                        RegistroReporteCRM.Id_Rik = null;
                        RegistroReporteCRM.TipoVenta = Convert.ToInt32(Seleccion);
                        RegistroReporteCRM.fechainicio = DateTime.Parse(mesAnioInicial);
                        RegistroReporteCRM.fechafinal = DateTime.Parse(mesAniofinal);

                        CNLEADS.ReporteLeads_TiempoRespuestaBarra(RegistroReporteCRM, ref listaReporteTiempoEstandar, ref listaReporteTiempoLimite,
       ref listaReporteFuera, Emp_CnxCentral);



                        if (nombre == "")
                        {
                            nombre = nombreGerente;
                        }
                        else
                        {
                            nombre = nombre + "," + nombreGerente;
                        }

                        if (listaReporteTiempoEstandar.Count() == 0)
                        {
                            if (datosanalisis == "")
                            {
                                datosanalisis = "0"; ;
                            }
                            else
                            {
                                datosanalisis = datosanalisis + "," + "0";
                            }
                        }
                        else
                        {
                            foreach (ReporteCRM ana in listaReporteTiempoEstandar)
                            {
                                if (datosanalisis == "")
                                {
                                    datosanalisis = ana.Total.ToString("F2");
                                }
                                else
                                {
                                    datosanalisis = datosanalisis + "," + ana.Total.ToString("F2");
                                }
                            }
                        }

                        if (listaReporteTiempoLimite.Count() == 0)
                        {
                            if (datosantiempolimite == "")
                            {
                                datosantiempolimite = "0"; ;
                            }
                            else
                            {
                                datosantiempolimite = datosantiempolimite + "," + "0";
                            }
                        }
                        else
                        {
                            foreach (ReporteCRM ana in listaReporteTiempoLimite)
                            {
                                if (datosantiempolimite == "")
                                {
                                    datosantiempolimite = ana.Total.ToString("F2");
                                }
                                else
                                {
                                    datosantiempolimite = datosantiempolimite + "," + ana.Total.ToString("F2");
                                }
                            }
                        }

                        if (listaReporteFuera.Count() == 0)
                        {
                            if (datosfuera == "")
                            {
                                datosfuera = "0"; ;
                            }
                            else
                            {
                                datosfuera = datosfuera + "," + "0";
                            }
                        }
                        else
                        {
                            foreach (ReporteCRM ana in listaReporteFuera)
                            {
                                if (datosfuera == "")
                                {
                                    datosfuera = ana.Total.ToString("F2");
                                }
                                else
                                {
                                    datosfuera = datosfuera + "," + ana.Total.ToString("F2");
                                }
                            }
                        }

                        //gráfica de pie con los totales INICIO

                        CN_Leads CNPIE = new CN_Leads();
                        List<ReporteCRM> listaReporteCRMPIE = new List<ReporteCRM>();

                        ReporteCRM RegistroReporteCRMPIE = new ReporteCRM();


                        if (mesAnioInicial == "" && mesAniofinal == "")
                        {
                            return JsonConvert.SerializeObject(new { id = 2 });
                        }
                        if (DateTime.Parse(mesAnioInicial) > DateTime.Parse(mesAniofinal))
                        {
                            return JsonConvert.SerializeObject(new { id = 4 });
                        }

                        List<ReporteCRM> listaReportepie = new List<ReporteCRM>();

                        //JFCV revisar esta parte porque debo mandarle el listado de sucursales 
                        // o meterlo dentro del for each para que lea cada uno de ellos 
                        RegistroReporteCRMPIE.Id_Emp = Sesion.Id_Emp;
                        RegistroReporteCRMPIE.Id_Cd = Convert.ToInt32(id);

                        RegistroReporteCRMPIE.Id_Rik = null;
                        RegistroReporteCRMPIE.TipoVenta = Convert.ToInt32(Seleccion);
                        RegistroReporteCRMPIE.fechainicio = DateTime.Parse(mesAnioInicial);
                        RegistroReporteCRMPIE.fechafinal = DateTime.Parse(mesAniofinal);

                        CNPIE.ReporteLeads_TiempoRespuesta(RegistroReporteCRMPIE, ref listaReportepie, Emp_CnxCentral);

                        //si es la primera vez que entra al ciclo de sucursales entonces 
                        // pietotal no tiene nada asi que inserto cada reistro que traiga el query
                        if (listaReportepieTotal.Count == 0)
                        {
                            foreach (ReporteCRM presupuesto in listaReportepie)
                            {
                                listaReportepieTotal.Add(presupuesto);
                            }

                        }
                        else
                        {
                            //Si ya es la segunda o mas sucursal entonces le suma 
                            foreach (ReporteCRM presupuesto in listaReportepie)
                            {
                                foreach (ReporteCRM presupuestotot in listaReportepieTotal)
                                {
                                    if (presupuesto.Nombre == presupuestotot.Nombre)
                                    {
                                        presupuestotot.Total = presupuestotot.Total + presupuesto.Total;
                                    }
                                }

                            }
                        }

                        //gráfica de pie con los totales FIN

                    }

                    //gráfica de pie con los totales Inicio
                    foreach (ReporteCRM presupuesto in listaReportepieTotal)
                    {

                        if (titulo2 == "")
                        {

                            titulo2 = presupuesto.Nombre;
                            datos2 = presupuesto.Total.ToString("F2");
                        }
                        else
                        {
                            titulo2 = titulo2 + "," + presupuesto.Nombre;
                            datos2 = datos2 + "," + presupuesto.Total.ToString("F2");
                        }

                    }

                    //gráfica de pie con los totales FIN

                    return JsonConvert.SerializeObject(new
                    {
                        id = 4,
                        Nombre = nombre,
                        Analisis = datosanalisis,
                        presentacion = datosantiempolimite,
                        Negociación = datosfuera,
                        Cierre = datosanCierre,
                        Cancelación = datosCancelación,
                        titulo2 = titulo2,
                        datos2 = datos2
                    });

                    //}

                }  //Fin
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = ex.Message });
            }
        }




        /// <summary>
        /// Este web method es para calcular los datos de la gráfica de efectividad por sucursal.
        /// </summary>
        /// <param name="Seleccion"></param>
        /// <param name="mesAnioInicial"></param>
        /// <param name="mesAniofinal"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static string TiempoRespuestaRepresentantes(string Seleccion, string sucursal, string riks, string mesAnioInicial, string mesAniofinal)
        {
            try
            {

                string rik = riks;
                string Emp_CnxCentral = ConfigurationManager.AppSettings.Get("strConnectionCentral");
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];

                CN_ReporteCRM CN = new CN_ReporteCRM();
                CN_Leads CNLEADS = new CN_Leads();

                ReporteCRM RegistroReporteCRM = new ReporteCRM();

                List<int> myList = new List<int>();
                RegistroReporteCRM.Id_Emp = Sesion.Id_Emp;
                RegistroReporteCRM.Id_Cd = Convert.ToInt32(sucursal);


                if (rik != "")
                {

                    string[] id_rik = rik.Split(',');
                    foreach (var value in id_rik)
                    {
                        myList.Add(Convert.ToInt32(value));
                    }
                }



                string nombre = "";
                string datosatiempo = "";
                string datostiempolimite = "";
                string datosfuera = "";

                string titulo2 = "";
                string datos2 = "";

                //gráfica de PIE
                List<ReporteCRM> listaReportepieTotal = new List<ReporteCRM>();




                foreach (int nombrerik in myList)
                {
                    List<ReporteCRM> listaReporteCRM = new List<ReporteCRM>();

                    List<ReporteCRM> listaReporteTiempoEstandar = new List<ReporteCRM>();
                    List<ReporteCRM> listaReporteTiempoLimite = new List<ReporteCRM>();
                    List<ReporteCRM> listaReporteFuera = new List<ReporteCRM>();

                    List<Rik> RegistroRik = (List<Rik>)HttpContext.Current.Session["nombreRik"];

                    Rik list = (from tlist in RegistroRik
                                where tlist.id == nombrerik
                                select tlist).FirstOrDefault();
                    RegistroReporteCRM.Id_Emp = Sesion.Id_Emp;
                    RegistroReporteCRM.Id_Cd = Convert.ToInt32(sucursal);
                    RegistroReporteCRM.Id_Rik = nombrerik.ToString();
                    RegistroReporteCRM.fechainicio = DateTime.Parse(mesAnioInicial);
                    RegistroReporteCRM.fechafinal = DateTime.Parse(mesAniofinal);

                    CNLEADS.ReporteLeads_TiempoRespuestaBarra(RegistroReporteCRM, ref listaReporteTiempoEstandar, ref listaReporteTiempoLimite,
       ref listaReporteFuera, Emp_CnxCentral);



                    if (nombre == "")
                    {
                        nombre = list.nombre.Substring(0, 17);
                    }
                    else
                    {
                        nombre = nombre + "," + list.nombre.Substring(0, 17);
                    }

                    if (listaReporteTiempoEstandar.Count() == 0)
                    {
                        if (datosatiempo == "")
                        {
                            datosatiempo = "0"; ;
                        }
                        else
                        {
                            datosatiempo = datosatiempo + "," + "0";
                        }
                    }
                    else
                    {
                        foreach (ReporteCRM ana in listaReporteTiempoEstandar)
                        {
                            if (datosatiempo == "")
                            {
                                datosatiempo = ana.Total.ToString("F2");
                            }
                            else
                            {
                                datosatiempo = datosatiempo + "," + ana.Total.ToString("F2");
                            }
                        }
                    }

                    if (listaReporteTiempoLimite.Count() == 0)
                    {
                        if (datostiempolimite == "")
                        {
                            datostiempolimite = "0"; ;
                        }
                        else
                        {
                            datostiempolimite = datostiempolimite + "," + "0";
                        }
                    }
                    else
                    {
                        foreach (ReporteCRM ana in listaReporteTiempoLimite)
                        {
                            if (datostiempolimite == "")
                            {
                                datostiempolimite = ana.Total.ToString("F2");
                            }
                            else
                            {
                                datostiempolimite = datostiempolimite + "," + ana.Total.ToString("F2");
                            }
                        }
                    }

                    if (listaReporteFuera.Count() == 0)
                    {
                        if (datosfuera == "")
                        {
                            datosfuera = "0"; ;
                        }
                        else
                        {
                            datosfuera = datosfuera + "," + "0";
                        }
                    }
                    else
                    {
                        foreach (ReporteCRM ana in listaReporteFuera)
                        {
                            if (datosfuera == "")
                            {
                                datosfuera = ana.Total.ToString("F2");
                            }
                            else
                            {
                                datosfuera = datosfuera + "," + ana.Total.ToString("F2");
                            }
                        }
                    }


                    //gráfica de pie con los totales INICIO

                    CN_Leads CNPIE = new CN_Leads();
                    List<ReporteCRM> listaReporteCRMPIE = new List<ReporteCRM>();

                    ReporteCRM RegistroReporteCRMPIE = new ReporteCRM();
                    List<ReporteCRM> listaReportepie = new List<ReporteCRM>();

                    //JFCV revisar esta parte porque debo mandarle el listado de sucursales 
                    // o meterlo dentro del for each para que lea cada uno de ellos 
                    RegistroReporteCRMPIE.Id_Emp = Sesion.Id_Emp;
                    RegistroReporteCRMPIE.Id_Cd = Convert.ToInt32(sucursal);

                    RegistroReporteCRMPIE.Id_Rik = nombrerik.ToString();
                    RegistroReporteCRMPIE.TipoVenta = Convert.ToInt32(Seleccion);
                    RegistroReporteCRMPIE.fechainicio = DateTime.Parse(mesAnioInicial);
                    RegistroReporteCRMPIE.fechafinal = DateTime.Parse(mesAniofinal);

                    CNPIE.ReporteLeads_TiempoRespuesta(RegistroReporteCRMPIE, ref listaReportepie, Emp_CnxCentral);

                    //si es la primera vez que entra al ciclo de sucursales entonces 
                    // pietotal no tiene nada asi que inserto cada reistro que traiga el query
                    if (listaReportepieTotal.Count == 0)
                    {
                        foreach (ReporteCRM presupuesto in listaReportepie)
                        {
                            listaReportepieTotal.Add(presupuesto);
                        }

                    }
                    else
                    {
                        //Si ya es la segunda o mas sucursal entonces le suma 
                        foreach (ReporteCRM presupuesto in listaReportepie)
                        {
                            foreach (ReporteCRM presupuestotot in listaReportepieTotal)
                            {
                                if (presupuesto.Nombre == presupuestotot.Nombre)
                                {
                                    presupuestotot.Total = presupuestotot.Total + presupuesto.Total;
                                }
                            }

                        }
                    }

                    //gráfica de pie con los totales FIN

                } //fin foreach riks 

                //gráfica de pie con los totales Inicio
                foreach (ReporteCRM presupuesto in listaReportepieTotal)
                {

                    if (titulo2 == "")
                    {

                        titulo2 = presupuesto.Nombre;
                        datos2 = presupuesto.Total.ToString("F2");
                    }
                    else
                    {
                        titulo2 = titulo2 + "," + presupuesto.Nombre;
                        datos2 = datos2 + "," + presupuesto.Total.ToString("F2");
                    }

                }
                titulo2 = "Tiempo Estandard,Tiempo Límite,Fuera de Tiempo";

                return JsonConvert.SerializeObject(new
                {
                    id = 4,
                    Nombre = nombre,
                    Analisis = datosatiempo,
                    presentacion = datostiempolimite,
                    Negociación = datosfuera,
                    Cierre = "",
                    Cancelación = "",
                    titulo2 = titulo2,
                    datos2 = datos2
                });

            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = ex.Message });
            }
        }



        #endregion
    }
}