using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using System.Data;
using System.IO;
using CapaNegocios;
using ClosedXML.Excel;
using System.Configuration;
using System.Web.Services;
using Newtonsoft.Json;
using DevExpress.XtraPrinting;
using DevExpress.Export;
using Spire.Xls;

namespace SIANWEB
{
    public partial class ReportesCompasLocales : System.Web.UI.Page
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

        public static List<Comun> ListaMotivos = new List<Comun>();
        public static List<Comun> ListaAplicaciones = new List<Comun>();
        public static List<Comun> ListaTipoProducto = new List<Comun>();
        public static List<Comun> ListaProductoCentral = new List<Comun>();
        public static List<Comun> ListaProveedorCentral = new List<Comun>();
        public static List<Comun> ListaProveedorLocal = new List<Comun>();

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            if (!Page.IsPostBack)
            {

                ValidarPermisos();

                inicializar();
                CargarComboMotivoCompra();
                CargarComboTipoProducto();
                var cont = ListaAplicaciones.Count();
                CargarComboAplicacion();
                CargarComboProductoCentral();
                CargarProveedorCentral();
                CargarProveedorLocal();

            }
            //else
            //{ 
            //    CargarComboMotivoCompra();
            //    CargarComboTipoProducto();
            //    CargarComboAplicacion();
            //    CargarComboProductoCentral();
            //    CargarProveedorCentral();
            //    CargarProveedorLocal();
            //}
            Session["activeMenu"] = 4;
        }
        
        private void CargarProveedorLocal()
        {
            try
            {
                Sesion Sesion = new CapaEntidad.Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<Comun> Lista = new List<Comun>();
                CN_ComprasLocales CN = new CN_ComprasLocales();

                CN.LlenaCombo(0, Sesion.Emp_Cnx, "SP_CargaProveedorLocalCL", ref Lista);
                Lista = Lista.Where(x => x.Id != -1).ToList();
                ListaProveedorLocal = Lista;
                //cmbProveedorLocalRep.DataSource = Lista.OrderBy(i => i.Descripcion);
                //cmbProveedorLocalRep.DataBind();
                //cmbProveedorLocalRep.Enabled = true;
            }
            catch (Exception ex)
            {
                //Alerta("Error, " + ex.Message);
            }
        }
        private void CargarComboTipoProducto()
        {
            try
            {
                Sesion Sesion = new CapaEntidad.Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<Comun> Lista = new List<Comun>();
                CN_ComprasLocales CN = new CN_ComprasLocales();

                CN.LlenaCombo(0, Sesion.Emp_Cnx, "SP_CargaTipoProductoCL", ref Lista);
                Lista = Lista.Where(x => x.Id != -1).ToList();
                ListaTipoProducto = Lista;
                //cmbTipoProductoRep.DataSource = Lista.OrderBy(i => i.Descripcion);
                //cmbTipoProductoRep.DataBind();
                //cmbTipoProductoRep.Enabled = true;
            }
            catch (Exception ex)
            {
                //Alerta("Error, " + ex.Message);
            }
        }
        private void CargarProveedorCentral()
        {
            try
            {
                Sesion Sesion = new CapaEntidad.Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<Comun> Lista = new List<Comun>();
                CN_ComprasLocales CN = new CN_ComprasLocales();

                CN.LlenaCombo(0, Sesion.Emp_Cnx, "SP_CargaProveedorCentralCL", ref Lista);
                Lista = Lista.Where(x => x.Id != -1).ToList();
                ListaProveedorCentral = Lista;
                //cmbProveedorCentralRep.DataSource = Lista.OrderBy(i => i.Descripcion);
                //cmbProveedorCentralRep.DataBind();
                //cmbProveedorCentralRep.Enabled = true;
            }
            catch (Exception ex)
            {
                //Alerta("Error, " + ex.Message);
            }
        }
        private void CargarComboProductoCentral()
        {
            try
            {
                Sesion Sesion = new CapaEntidad.Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<Comun> Lista = new List<Comun>();
                CN_ComprasLocales CN = new CN_ComprasLocales();

                CN.LlenaCombo(0, Sesion.Emp_Cnx, "SP_CargaProveedorCentralCLRep", ref Lista);
                Lista = Lista.Where(x => x.Id != -1).ToList();
                ListaProductoCentral = Lista;
                //cmbProductoCentralRep.DataSource = Lista.OrderBy(i => i.Descripcion);
                //cmbProductoCentralRep.DataBind();
                //cmbProductoCentralRep.Enabled = true;
            }
            catch (Exception ex)
            {
                //Alerta("Error, " + ex.Message);
            }
        }
        private void CargarComboAplicacion()
        {
            try
            {
                Sesion Sesion = new CapaEntidad.Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<Comun> Lista = new List<Comun>();
                CN_ComprasLocales CN = new CN_ComprasLocales();

                CN.LlenaCombo(0, Sesion.Emp_Cnx, "SP_CargaProductoAplicacionCLReporte", ref Lista);
                Lista = Lista.Where(x => x.Id != -1).ToList();
                ListaAplicaciones = Lista;
                //cmbAplicacionRep.DataSource = Lista.OrderBy(i => i.Descripcion);
                //cmbAplicacionRep.DataBind();
                //cmbAplicacionRep.Enabled = true;
            }
            catch (Exception ex)
            {
                //Alerta("Error, " + ex.Message);
            }
        }
        private void CargarComboMotivoCompra()
        {
            try
            {
                Sesion Sesion = new CapaEntidad.Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<Comun> Lista = new List<Comun>();
                CN_ComprasLocales CN = new CN_ComprasLocales();

                CN.LlenaCombo(0, Sesion.Emp_Cnx, "SP_CargaMotivosCL", ref Lista);
                Lista = Lista.Where(x => x.Id != -1).ToList();
                ListaMotivos = Lista;
                //cmbMotivoCompraRep.DataSource = Lista.OrderBy(i => i.Id);
                //cmbMotivoCompraRep.DataBind();
                //cmbMotivoCompraRep.Enabled = true;
            }
            catch (Exception ex)
            {
                //Alerta("Error, " + ex.Message);
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
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            
            //Se cargan tablas multiseleccion
            //CN_ComprasLocales CN = new CN_ComprasLocales();

            //var dtMotivos = CN.ComboMotivos();
            //cmbMotivoRBM.DataSource = dtMotivos;
            //cmbMotivoRBM.DataBind();


            //Se cargan los Combos del primer reporte
            //
            CN_Comun.DevLlenaCombo(sesion.Id_Cd_Ver, sesion.Emp_Cnx, "SP_CargaGrupo", ref cmbGrupoM);
            CN_Comun.DevLlenaCombo(sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Id_U, sesion.Emp_Cnx, "SP_CargaSucursal", ref cmbSucursalM);
            CN_Comun.DevLlenaCombo(sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Id_U, sesion.Emp_Cnx, "SP_CargaMotivosCL", ref cmbMotivoM);
            CN_Comun.DevLlenaCombo(sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Id_U, sesion.Emp_Cnx, "SP_CargaTipoProductoCL", ref cmbTipoProducto);
            CN_Comun.DevLlenaCombo(sesion.Emp_Cnx, "SP_CargaProveedorLocalCL", ref cmbProveedorLocalPL);
            CN_Comun.DevLlenaCombo(sesion.Emp_Cnx, "SP_CargaProveedorCentralCL", ref cmbProveedorCentralPC); 
            CN_Comun.DevLlenaCombo(sesion.Emp_Cnx, "SP_CargaProductoAplicacionCL", ref cmbAplicacionProductoA);
            CN_Comun.DevLlenaCombo64(sesion.Emp_Cnx, "SP_CargaProductoCentralCL", ref cmbProductoCentralPC);

            //Se asigna grupo de las sucursales 
            cmbGrupos.Value = "1";
            cmbGrupoM.Value = "1";
            cmbGrupoTP.Value = "1";
            cmbGrupoPL.Value = "1";
            cmbGrupoPC.Value = "1";
            cmbGrupoA.Value = "1";
            cmbGrupoAAA.Value = "1";
            cmbGrupoDes.Value = "1";

            //Se asignan valores a combos repetidos
            cmbGrupos.Items.AddRange(cmbGrupoM.Items.Cast<Object>().ToArray());
            cmbGrupoTP.Items.AddRange(cmbGrupoM.Items.Cast<Object>().ToArray());
            cmbGrupoPL.Items.AddRange(cmbGrupoM.Items.Cast<Object>().ToArray());
            cmbGrupoPC.Items.AddRange(cmbGrupoM.Items.Cast<Object>().ToArray());
            cmbGrupoA.Items.AddRange(cmbGrupoM.Items.Cast<Object>().ToArray());
            cmbGrupoAAA.Items.AddRange(cmbGrupoM.Items.Cast<Object>().ToArray());
            cmbGrupoDes.Items.AddRange(cmbGrupoM.Items.Cast<Object>().ToArray());


            cmbSucursals.Items.AddRange(cmbSucursalM.Items.Cast<Object>().ToArray());
            cmbSucursalTP.Items.AddRange(cmbSucursalM.Items.Cast<Object>().ToArray());
            cmbSucursalPL.Items.AddRange(cmbSucursalM.Items.Cast<Object>().ToArray());
            cmbSucursalPC.Items.AddRange(cmbSucursalM.Items.Cast<Object>().ToArray());
            cmbSucursalA.Items.AddRange(cmbSucursalM.Items.Cast<Object>().ToArray());
            cmbSucursalAAA.Items.AddRange(cmbSucursalM.Items.Cast<Object>().ToArray());
            cmbSucursalDes.Items.AddRange(cmbSucursalM.Items.Cast<Object>().ToArray());


            cmbMotivoss.Items.AddRange(cmbMotivoM.Items.Cast<Object>().ToArray());
            cmbMotivoTP.Items.AddRange(cmbMotivoM.Items.Cast<Object>().ToArray());
            cmbMotivoPL.Items.AddRange(cmbMotivoM.Items.Cast<Object>().ToArray());
            cmbMotivoPC.Items.AddRange(cmbMotivoM.Items.Cast<Object>().ToArray());
            cmbMotivoA.Items.AddRange(cmbMotivoM.Items.Cast<Object>().ToArray());
            cmbMotivoDes.Items.AddRange(cmbMotivoM.Items.Cast<Object>().ToArray());

            cmbTipoProductoPL.Items.AddRange(cmbTipoProducto.Items.Cast<Object>().ToArray());
            cmbTipoProductoA.Items.AddRange(cmbTipoProducto.Items.Cast<Object>().ToArray());
            cmbTipoProductoPC.Items.AddRange(cmbTipoProducto.Items.Cast<Object>().ToArray());
            cmbTipoProductoDes.Items.AddRange(cmbTipoProducto.Items.Cast<Object>().ToArray());

            cmbProdutoCentralPL.Items.AddRange(cmbProductoCentralPC.Items.Cast<Object>().ToArray());
            cmbProductoCentralDes.Items.AddRange(cmbProductoCentralPC.Items.Cast<Object>().ToArray());
            cmbProveedorCentralDes.Items.AddRange(cmbProveedorCentralPC.Items.Cast<Object>().ToArray());
            cmbProveedorLocalDes.Items.AddRange(cmbProveedorLocalPL.Items.Cast<Object>().ToArray());
            cmbAplicacionDes.Items.AddRange(cmbAplicacionProductoA.Items.Cast<Object>().ToArray());

            //Se selecciona la sucursal actual  
            cmbSucursalM.Value = sesion.Id_Cd_Ver.ToString();
            cmbSucursals.Value = sesion.Id_Cd_Ver.ToString();
            cmbSucursalTP.Value = sesion.Id_Cd_Ver.ToString();
            cmbSucursalPL.Value = sesion.Id_Cd_Ver.ToString();
            cmbSucursalPC.Value = sesion.Id_Cd_Ver.ToString();
            cmbSucursalA.Value = sesion.Id_Cd_Ver.ToString();
            cmbSucursalAAA.Value = sesion.Id_Cd_Ver.ToString();
            cmbSucursalDes.Value = sesion.Id_Cd_Ver.ToString();

            //Se inicializa el combo de Motivo
            cmbMotivoM.Value = "0";
            cmbMotivoss.Value = "0";
            cmbMotivoTP.Value = "0";
            cmbMotivoPL.Value = "0";
            cmbMotivoPC.Value = "0";
            cmbMotivoA.Value = "0";
            cmbMotivoAAA.Value = "0";
            cmbMotivoDes.Value = "0";

            //Se inicializa el combo de Tipo producto
            cmbTipoProducto.Value = "0";
            cmbTipoProductoA.Value = "0";
            cmbTipoProductoPC.Value = "0";
            cmbTipoProductoPL.Value = "0";
            cmbTipoProductoDes.Value = "0";

            cmbProductoCentralDes.Value = "0";
            cmbProdutoCentralPL.Value = "0";
            cmbProductoCentralPC.Value = "0";
            
            //se inicializa Proveedor Local
            cmbProveedorLocalPL.Value = "0";
            cmbProveedorLocalDes.Value = "0";
            
            //se inicializa Proveedor Local
            cmbProveedorCentralPC.Value = "0";
            cmbProveedorCentralDes.Value = "0";
            
            //se inicializa Tipo de precio
            cmbTipoPrecioAAA.Value = "0";
            
            //se inicializa Prodcuto Aplicación
            cmbAplicacionProductoA.Value = "0";
            cmbAplicacionDes.Value = "0";

            //Se deshabilitan las opciones que no pueden ser modificadas
            cmbGrupoM.ReadOnly = true;
            cmbGrupos.ReadOnly = true;
            cmbGrupoTP.ReadOnly = true;
            cmbGrupoDes.ReadOnly = true;

            cmbSucursalM.ReadOnly = true;
            cmbSucursals.ReadOnly = true;
            cmbSucursalTP.ReadOnly = true;
            cmbSucursalDes.ReadOnly = true;

        }
        void LimpiarValores()
        {
            //strRIKS = "";
            //strDatoRIKS = "";
            //strURLBack = "";

            //NumCapturado = "0";
            //NumSolicitadoG = "0";
            //NumSolicitadoJ = "0";
            //NumAutorizado = "0";
            //NumOtro = "0";

            //lblTotalGeneral = "";
            //lblCapturavsAutorizado = "";
            //lblCumplimientoACySAutorizados = "";
            //lbltotalVentaInstalada = "";
            //lblCumplimientoACySVenta = "";


            //lblNumCapturado = "0";
            //lblNumSolicitadoG = "0";
            //lblNumSolicitadoJ = "0";
            //lblNumAutorizado = "0";
            //lblNumOtro = "0";

            //ExcelBaseDeRiks = new List<DashboardACyS_RIKS>();
            //ExcelDeRiks = new List<DashboardACyS_DetalleRIKS>();
            //ExcelResumen = new List<DashboardACyS_Resumen>();
            //ExcelClientesCon = new List<DashboardACyS_Clientes>();
            //ExcelClientesSin = new List<DashboardACyS_Clientes>();
            //ExcelDetalleACyS = new List<DashboardACyS_DetalleACyS>();
            //ListadoDeRIKs = new List<DashboardACyS_RIKS>();
            //ExcelDeEstatus = new List<DashboardACyS_Estatus>();

            //ExcelResumen_DtlRiks = new List<DashboardACyS_Resumen>();
            //ExcelBaseDeRiks_DtlRiks = new List<DashboardACyS_RIKS>();
            //ExcelClientesCon_DtlRiks = new List<DashboardACyS_Clientes>();
            //ExcelClientesSin_DtlRiks = new List<DashboardACyS_Clientes>();
            //ExcelDetalleACyS_DtlRiks = new List<DashboardACyS_DetalleACyS>();

            //ListaSucursales = new List<Comun>();
        }

        //protected void GridAplicaciones_ValueChanged(object sender, EventArgs e)
        //{
        //    CargarComboAplicacion();
        //}

        protected void cmbMotivo_SelectedIndexChanged(object sender, EventArgs e)
        {       
        }

        protected void cmbSucursalM_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        [WebMethod]
        public static string btnCLporMotivo_ServerClick(string mesAnioInicial, string mesAniofinal, string Id_Cd, string Id_Motivo, string Mostrar)
        {
            try
            {
                Sesion Sesion = new Sesion(); 
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
                if (Sesion == null)
                {
                    return JsonConvert.SerializeObject(new { id = 0 });
                }

                CN_Compras_Locales CN_CL = new CN_Compras_Locales();
                CompraLocal CL = new CompraLocal();

                List<CompraLocal> listaComprasLocales = new List<CompraLocal>();
                List<object> listaFinal = new List<object>();
                DataSet dtDatos = new DataSet();

                CL.Id_Emp = Sesion.Id_Emp;
                CL.Id_Cd = Sesion.Id_Cd_Ver;
                CL.Id_Motivo = int.Parse(Id_Motivo);
                DateTime FechaIni = Convert.ToDateTime(mesAnioInicial);
                DateTime FechaFin = Convert.ToDateTime(mesAniofinal);
                DateTime AnioInicial = DateTime.Parse(mesAnioInicial);
                DateTime AnioFinal = DateTime.Parse(mesAniofinal);
                if (AnioInicial > AnioFinal)
                {
                    return JsonConvert.SerializeObject(new { id = 4 });
                }

                CN_CL.ConsultaComprasLocalesXMotivo(CL, ref listaComprasLocales, FechaIni, FechaFin, Sesion.Emp_Cnx);

               var lista = listaComprasLocales.GroupBy(x => x.Cd_Nombre)
                             .Select(g => new {
                                 Cd_Nombre = g.Key,
                                 unoP = g.Where(c=> c.Id_Motivo == 1).Sum(c => c.Pesos),
                                 unoC = g.Where(c => c.Id_Motivo == 1).Sum(c => c.Unidades),
                                 dosP = g.Where(c => c.Id_Motivo == 2).Sum(c => c.Pesos),
                                 dosC = g.Where(c => c.Id_Motivo == 2).Sum(c => c.Unidades),
                                 tresP = g.Where(c => c.Id_Motivo == 3).Sum(c => c.Pesos),
                                 tresC = g.Where(c => c.Id_Motivo == 3).Sum(c => c.Unidades),
                                 Porcentaje = "100%"
                             });

                string titulo = "";
                decimal total = 0;
                string datos = "";
                string totalstr = "";
 
                if (Mostrar == "0")
                {
                    foreach (CompraLocal Compralocal in listaComprasLocales)
                    {
                        total = total + Compralocal.Pesos;
                        if (titulo == "")
                        {
                            titulo = Compralocal.Motivo;
                            datos = Compralocal.Pesos.ToString("F2");
                        }
                        else
                        {
                            titulo = titulo + "," + Compralocal.Motivo;
                            datos = datos + "," + Compralocal.Pesos.ToString("F2");
                        }
                    }

                    totalstr = "Total: " + total.ToString("c");
                }
                else 
                {
                    foreach (CompraLocal Compralocal in listaComprasLocales)
                    {
                        total = total + Compralocal.Unidades;
                        if (titulo == "")
                        {
                            titulo = Compralocal.Motivo;
                            datos = Compralocal.Unidades.ToString();
                        }
                        else
                        {
                            titulo = titulo + "," + Compralocal.Motivo;
                            datos = datos + "," + Compralocal.Unidades.ToString();
                        }
                    }

                    totalstr = "Total: " + total.ToString();
                }
                return JsonConvert.SerializeObject(new { id = 3, titulo = titulo, datos = datos, total = totalstr, datos2 = lista });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = ex.Message });
            }
        }

        [WebMethod]
        public static string btnCLporSucursal_ServerClick(string mesAnioInicial, string mesAniofinal, string Id_Cd, string Id_Motivo, string Mostrar)
        {
            try
            {
                //string Emp_CnxCentral = ConfigurationManager.AppSettings.Get("strConnectionCentral");
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
                if (Sesion == null)
                {
                    return JsonConvert.SerializeObject(new { id = 0 });
                }

                CN_Compras_Locales CN_CL = new CN_Compras_Locales();
                CompraLocal CL = new CompraLocal();

                List<CompraLocal> listaComprasLocales = new List<CompraLocal>();

                CL.Id_Emp = Sesion.Id_Emp;
                CL.Id_Cd = Sesion.Id_Cd_Ver;
                CL.Id_Motivo = int.Parse(Id_Motivo);
                DateTime FechaIni = Convert.ToDateTime(mesAnioInicial);
                DateTime FechaFin = Convert.ToDateTime(mesAniofinal);
                DateTime AnioInicial = DateTime.Parse(mesAnioInicial);
                DateTime AnioFinal = DateTime.Parse(mesAniofinal);
                if (AnioInicial > AnioFinal)
                {
                    return JsonConvert.SerializeObject(new { id = 4 });
                }

                CN_CL.ConsultaComprasLocalesXSucursal(CL, ref listaComprasLocales, FechaIni, FechaFin, Sesion.Emp_Cnx);

                string titulo = "";
                decimal total = 0;
                string datos = "";
                string totalstr = "";

                if (Mostrar == "0")
                {
                    foreach (CompraLocal Compralocal in listaComprasLocales)
                    {
                        total = total + Compralocal.Pesos;
                        if (titulo == "")
                        {
                            titulo = Compralocal.Cd_Nombre;
                            datos = Compralocal.Pesos.ToString("F2");
                        }
                        else
                        {
                            titulo = titulo + "," + Compralocal.Cd_Nombre;
                            datos = datos + "," + Compralocal.Pesos.ToString("F2");
                        }
                    }

                     totalstr = "Total: " + total.ToString("c");
                }
                else {
                    foreach (CompraLocal Compralocal in listaComprasLocales)
                    {
                        total = total + Compralocal.Unidades;
                        if (titulo == "")
                        {
                            titulo = Compralocal.Cd_Nombre;
                            datos = Compralocal.Unidades.ToString();
                        }
                        else
                        {
                            titulo = titulo + "," + Compralocal.Cd_Nombre;
                            datos = datos + "," + Compralocal.Unidades.ToString();
                        }
                    }
                     totalstr = "Total: " + total.ToString();

                }
                return JsonConvert.SerializeObject(new { id = 3, titulo = titulo, datos = datos, total = totalstr, datos2 = listaComprasLocales });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = ex.Message });
            }
        }
        [WebMethod]
        public static string btnCLTipoProducto_ServerClick(string mesAnioInicial, string mesAniofinal, string Id_Cd, string Id_Motivo, string TipoProducto, string Mostrar)
        {
            try
            {
                //string Emp_CnxCentral = ConfigurationManager.AppSettings.Get("strConnectionCentral");
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
                if (Sesion == null)
                {
                    return JsonConvert.SerializeObject(new { id = 0 });
                }

                CN_Compras_Locales CN_CL = new CN_Compras_Locales();
                CompraLocal CL = new CompraLocal();

                List<CompraLocal> listaComprasLocales = new List<CompraLocal>();

                CL.Id_Emp = Sesion.Id_Emp;
                CL.Id_Cd = Sesion.Id_Cd_Ver;
                CL.Id_Motivo = int.Parse(Id_Motivo);
                CL.IdTipoProd = TipoProducto.ToString();
                DateTime FechaIni = Convert.ToDateTime(mesAnioInicial);
                DateTime FechaFin = Convert.ToDateTime(mesAniofinal);
                DateTime AnioInicial = DateTime.Parse(mesAnioInicial);
                DateTime AnioFinal = DateTime.Parse(mesAniofinal);
                if (AnioInicial > AnioFinal)
                {
                    return JsonConvert.SerializeObject(new { id = 4 });
                }

                CN_CL.ConsultaComprasLocalesXTipoProduto(CL, ref listaComprasLocales, FechaIni, FechaFin, Sesion.Emp_Cnx);

                var lista = listaComprasLocales.GroupBy(x => x.Cd_Nombre)
              .Select(g => new {
                  Cd_Nombre = g.Key,
                  Quimicos = g.Where(c => c.IdTipoProd == "1").Sum(c => c.Pesos),
                  Otros = g.Where(c => c.IdTipoProd == "2").Sum(c => c.Pesos),
                  Papel = g.Where(c => c.IdTipoProd == "3").Sum(c => c.Pesos),
                  Dosif = g.Where(c => c.IdTipoProd == "4").Sum(c => c.Pesos),
                  Suplementos = g.Where(c => c.IdTipoProd == "null").Sum(c => c.Pesos),
                  Porcentaje = "100%"
              });


                string titulo = "";
                decimal total = 0;
                string datos = "";
                string totalstr = "";

                if (Mostrar == "0")
                {
                    foreach (CompraLocal Compralocal in listaComprasLocales)
                    {
                        total = total + Compralocal.Pesos;
                        if (titulo == "")
                        {
                            titulo = Compralocal.TipoProd;
                            datos = Compralocal.Pesos.ToString("F2");
                        }
                        else
                        {
                            titulo = titulo + "," + Compralocal.TipoProd;
                            datos = datos + "," + Compralocal.Pesos.ToString("F2");
                        }
                    }

                    totalstr = "Total: " + total.ToString("c");
                }
                else
                {
                    foreach (CompraLocal Compralocal in listaComprasLocales)
                    {
                        total = total + Compralocal.Unidades;
                        if (titulo == "")
                        {
                            titulo = Compralocal.TipoProd;
                            datos = Compralocal.Unidades.ToString();
                        }
                        else
                        {
                            titulo = titulo + "," + Compralocal.TipoProd;
                            datos = datos + "," + Compralocal.Unidades.ToString();
                        }
                    }
                    totalstr = "Total: " + total.ToString();

                }
                return JsonConvert.SerializeObject(new { id = 3, titulo = titulo, datos = datos, total = totalstr, datos2 = lista });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = ex.Message });
            }
        }
        [WebMethod]
        public static string btnCLProvLocal_ServerClick(string mesAnioInicial, string mesAniofinal, string Id_Cd, string Id_Motivo,  string ProveedorLocal, string TipoProducto, string ProductoCentral, string Mostrar)
        {
            try
            {
                //string Emp_CnxCentral = ConfigurationManager.AppSettings.Get("strConnectionCentral");
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
                if (Sesion == null)
                {
                    return JsonConvert.SerializeObject(new { id = 0 });
                }

                CN_Compras_Locales CN_CL = new CN_Compras_Locales();
                CompraLocal CL = new CompraLocal();

                List<CompraLocal> listaComprasLocales = new List<CompraLocal>();

                CL.Id_Emp = Sesion.Id_Emp;
                CL.Id_Cd = Sesion.Id_Cd_Ver;
                CL.Id_Motivo = int.Parse(Id_Motivo); 
                CL.TipoProd = TipoProducto.ToString();
                CL.ProveedorLocal = ProveedorLocal.ToString();
                CL.CodigoPadre = ProductoCentral.ToString();
                DateTime FechaIni = Convert.ToDateTime(mesAnioInicial);
                DateTime FechaFin = Convert.ToDateTime(mesAniofinal);
                DateTime AnioInicial = DateTime.Parse(mesAnioInicial);
                DateTime AnioFinal = DateTime.Parse(mesAniofinal);
                if (AnioInicial > AnioFinal)
                {
                    return JsonConvert.SerializeObject(new { id = 4 });
                }

                DataSet dtResultado = new DataSet();
                //CN_CL.ConsultaComprasLocalesXProvLocal(CL, ref dtResultado, FechaIni, FechaFin, Sesion.Emp_Cnx);
                CN_CL.ConsultaComprasLocalesXProvLocal(CL, ref listaComprasLocales, FechaIni, FechaFin, Sesion.Emp_Cnx);

                string titulo = "";
                decimal total = 0;
                string datos = "";
                //string datos2 = "";
                string totalstr = "";
                int unidades = 0;

                //if (Mostrar == "0")
                //{
                //    foreach (DataRow Row in dtResultado.Tables[0].Rows)
                //    {
                //        total = total + decimal.Parse(Row["Pesos"].ToString());
                //        if (titulo == "")
                //        {
                //            titulo = Row["ProveedorLocal"].ToString() + " - " + Row["Descripcion"].ToString();
                //            datos = Row["Pesos"].ToString();
                //        }
                //        else
                //        {
                //            titulo = titulo + "," + Row["ProveedorLocal"] + " - " + Row["Descripcion"].ToString();
                //            datos = datos + "," + Row["Pesos"].ToString();
                //        }
                //    }

                //    totalstr = "Total: " + total.ToString("c");
                //}
                //else
                //{
                //    foreach (DataRow Row in dtResultado.Tables[0].Rows)
                //    {
                //        total = total + int.Parse(Row["Unidades"].ToString());
                //        if (titulo == "")
                //        {
                //            titulo = Row["ProveedorLocal"].ToString();
                //            datos = Row["Unidades"].ToString();
                //        }
                //        else
                //        {
                //            titulo = titulo + "," + Row["ProveedorLocal"];
                //            datos = datos + "," + Row["Unidades"].ToString();
                //        }
                //    }
                //    totalstr = "Total: " + total.ToString();

                //}
                if (Mostrar == "0")
                {
                    unidades = 0;
                    foreach (CompraLocal Compralocal in listaComprasLocales)
                    {
                        total = total + Compralocal.Pesos;
                        if (titulo == "")
                        {
                            titulo = Compralocal.IdProveedorLocal + " - " + Compralocal.ProveedorLocal;
                            datos = Compralocal.Pesos.ToString("F2");
                        }
                        else
                        {
                            titulo = titulo + "," + Compralocal.ProveedorLocal;
                            datos = datos + "," + Compralocal.Pesos.ToString("F2");
                        }
                    }

                    totalstr = total.ToString();
                }
                else
                {
                    foreach (CompraLocal Compralocal in listaComprasLocales)
                    {
                        unidades = 1;
                        total = total + Compralocal.Unidades;
                        if (titulo == "")
                        {
                            titulo = Compralocal.IdProveedorLocal + '-' + Compralocal.ProveedorLocal;
                            datos = Compralocal.Unidades.ToString();
                        }
                        else
                        {
                            titulo = titulo + "," + Compralocal.ProveedorLocal;
                            datos = datos + "," + Compralocal.Unidades.ToString();
                        }
                    }
                    totalstr = total.ToString();


                }
                return JsonConvert.SerializeObject(new { id = 3, titulo = titulo, datos = datos, total = totalstr, unidades = unidades, datos2 = listaComprasLocales });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = ex.Message });
            }
        }
        [WebMethod]
        public static string btnCLProvCentral_ServerClick(string mesAnioInicial, string mesAniofinal, string Id_Cd, string Id_Motivo, string ProveedorCentral, string TipoProducto, string ProductoCentral, string Mostrar)
        {
            try
            {
                //string Emp_CnxCentral = ConfigurationManager.AppSettings.Get("strConnectionCentral");
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
                if (Sesion == null)
                {
                    return JsonConvert.SerializeObject(new { id = 0 });
                }

                CN_Compras_Locales CN_CL = new CN_Compras_Locales();
                CompraLocal CL = new CompraLocal();

                List<CompraLocal> listaComprasLocales = new List<CompraLocal>();

                CL.Id_Emp = Sesion.Id_Emp;
                CL.Id_Cd = Sesion.Id_Cd_Ver;
                CL.Id_Motivo = int.Parse(Id_Motivo);
                CL.TipoProd = TipoProducto.ToString();
                CL.ProveedorCentral = ProveedorCentral;
                CL.CodigoPadre = ProductoCentral; 
                DateTime FechaIni = Convert.ToDateTime(mesAnioInicial);
                DateTime FechaFin = Convert.ToDateTime(mesAniofinal);
                DateTime AnioInicial = DateTime.Parse(mesAnioInicial);
                DateTime AnioFinal = DateTime.Parse(mesAniofinal);
                if (AnioInicial > AnioFinal)
                {
                    return JsonConvert.SerializeObject(new { id = 4 });
                }

                CN_CL.ConsultaComprasLocalesXProvCentral(CL, ref listaComprasLocales, FechaIni, FechaFin, Sesion.Emp_Cnx);

                string titulo = "";
                decimal total = 0;
                string datos = "";
                string totalstr = "";
                int unidades = 0;

                if (Mostrar == "0")
                {
                    unidades = 0;
                    foreach (CompraLocal Compralocal in listaComprasLocales)
                    {
                        total = total + Compralocal.Pesos;
                        if (titulo == "")
                        {
                            titulo = Compralocal.IdProveedorCentral + " - " + Compralocal.ProveedorCentral;
                            datos = Compralocal.Pesos.ToString("F2");
                        }
                        else
                        {
                            titulo = titulo + "," + Compralocal.IdProveedorCentral + " - " + Compralocal.ProveedorCentral;
                            datos = datos + "," + Compralocal.Pesos.ToString("F2");
                        }
                    }

                    totalstr = total.ToString();
                }
                else
                {
                    unidades = 1;
                    foreach (CompraLocal Compralocal in listaComprasLocales)
                    {
                        total = total + Compralocal.Unidades;
                        if (titulo == "")
                        {
                            titulo = Compralocal.IdProveedorCentral + " - " + Compralocal.ProveedorCentral;
                            datos = Compralocal.Unidades.ToString();
                        }
                        else
                        {
                            titulo = titulo + "," + Compralocal.IdProveedorCentral + "-" + Compralocal.ProveedorCentral;
                            datos = datos + "," + Compralocal.Unidades.ToString();
                        }
                    }
                    totalstr = total.ToString();

                }
                return JsonConvert.SerializeObject(new { id = 3, titulo = titulo, datos = datos, total = totalstr, unidades= unidades, datos2 = listaComprasLocales });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = ex.Message });
            }
        }
        [WebMethod]
        public static string btnCLAplicacion_ServerClick(string mesAnioInicial, string mesAniofinal, string Id_Cd, string Id_Motivo, string TipoProducto, string Aplicacion, string Mostrar)
        {
            try
            {
                //string Emp_CnxCentral = ConfigurationManager.AppSettings.Get("strConnectionCentral");
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
                if (Sesion == null)
                {
                    return JsonConvert.SerializeObject(new { id = 0 });
                }

                CN_Compras_Locales CN_CL = new CN_Compras_Locales();
                CompraLocal CL = new CompraLocal();

                List<CompraLocal> listaComprasLocales = new List<CompraLocal>();

                CL.Id_Emp = Sesion.Id_Emp;
                CL.Id_Cd = Sesion.Id_Cd_Ver;
                CL.Id_Motivo = int.Parse(Id_Motivo);
                CL.TipoProd = TipoProducto.ToString();
                CL.Aplicacion = Aplicacion.ToString();
                DateTime FechaIni = Convert.ToDateTime(mesAnioInicial);
                DateTime FechaFin = Convert.ToDateTime(mesAniofinal);
                DateTime AnioInicial = DateTime.Parse(mesAnioInicial);
                DateTime AnioFinal = DateTime.Parse(mesAniofinal);
                if (AnioInicial > AnioFinal)
                {
                    return JsonConvert.SerializeObject(new { id = 4 });
                }

                CN_CL.ConsultaComprasLocalesXAplicacion(CL, ref listaComprasLocales, FechaIni, FechaFin, Sesion.Emp_Cnx);

                string titulo = "";
                decimal total = 0;
                string datos = "";
                string totalstr = "";
                int unidades = 0;

                if (Mostrar == "0")
                {
                    unidades = 0;
                    foreach (CompraLocal Compralocal in listaComprasLocales)
                    {
                        total = total + Compralocal.Pesos;
                        if (titulo == "")
                        {
                            titulo = Compralocal.Aplicacion;
                            datos = Compralocal.Pesos.ToString("F2");
                        }
                        else
                        {
                            titulo = titulo + "," + Compralocal.Aplicacion;
                            datos = datos + "," + Compralocal.Pesos.ToString("F2");
                        }
                    }

                    totalstr = total.ToString();
                }
                else
                {
                    unidades = 1;
                    foreach (CompraLocal Compralocal in listaComprasLocales)
                    {
                        total = total + Compralocal.Unidades;
                        if (titulo == "")
                        {
                            titulo = Compralocal.Aplicacion;
                            datos = Compralocal.Unidades.ToString();
                        }
                        else
                        {
                            titulo = titulo + "," + Compralocal.Aplicacion;
                            datos = datos + "," + Compralocal.Unidades.ToString();
                        }
                    }
                    totalstr = total.ToString();

                }
                return JsonConvert.SerializeObject(new { id = 3, titulo = titulo, datos = datos, total = totalstr, unidades= unidades, datos2 = listaComprasLocales});
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = ex.Message });
            }
        }
        [WebMethod]
        public static string BtnCLPrecioAAA_ServerClick(string mesAnioInicial, string mesAniofinal, string Id_Cd, string Id_Motivo, string TipoPrecio)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
                if (Sesion == null)
                {
                    return JsonConvert.SerializeObject(new { id = 0 });
                }

                CN_Compras_Locales CN_CL = new CN_Compras_Locales();
                CompraLocal CL = new CompraLocal();

                List<CompraLocal> listaComprasLocales = new List<CompraLocal>();

                CL.Id_Emp = Sesion.Id_Emp;
                CL.Id_Cd = Sesion.Id_Cd_Ver;
                CL.Id_Motivo = int.Parse(Id_Motivo);
                DateTime FechaIni = Convert.ToDateTime(mesAnioInicial);
                DateTime FechaFin = Convert.ToDateTime(mesAniofinal);
                DateTime AnioInicial = DateTime.Parse(mesAnioInicial);
                DateTime AnioFinal = DateTime.Parse(mesAniofinal);
                CL.TipoAAA = int.Parse(TipoPrecio);
                if (AnioInicial > AnioFinal)
                {
                    return JsonConvert.SerializeObject(new { id = 4 });
                }

                CN_CL.ConsultaComprasLocalesXTipoAAA(CL, ref listaComprasLocales, FechaIni, FechaFin, Sesion.Emp_Cnx);

                string titulo = "";
                decimal total = 0;
                string datos = "";
                string totalstr = "";
                string Mostrar = "0";
                int ContMayor = 0;
                decimal TotalMayor = 0;
                int ContMenor = 0;
                decimal TotalMenor = 0;

                if (Mostrar == "0")
                {
                    foreach (CompraLocal Compralocal in listaComprasLocales)
                    {
                        if(CL.TipoAAA == 1) 
                        {
                            titulo = "Precio Mayor";
                            ContMayor += 1;
                            TotalMayor += Compralocal.Pesos;
                        }
                        else if(CL.TipoAAA == 2)
                        {
                            titulo = "Precio Menor";
                            ContMayor += 1;
                            TotalMayor += Compralocal.Pesos;
                        }
                        else
                        {
                            if (Compralocal.TipoAAA == 1) 
                            {
                                ContMayor += 1;
                                TotalMayor += Compralocal.Pesos;
                            }
                            if (Compralocal.TipoAAA == 2)
                            {
                                ContMenor += 1;
                                TotalMenor += Compralocal.Pesos;
                            }
                        }

                        if (CL.TipoAAA == 0)
                        {
                            titulo = "Precio Mayor" + " - " + ContMayor.ToString() + " Códigos" + ", " + "Precio Menor" + " - " + ContMenor.ToString() + " Códigos";
                            datos = TotalMayor.ToString("F2") + "," + TotalMenor.ToString("F2");
                        }
                        else
                        {
                            titulo = titulo + " - " + ContMayor.ToString() + " Códigos";
                            datos = TotalMayor.ToString("F2");
                        }
                    }

                    totalstr = "Total: " + total.ToString("c");
                }
                else
                {
                    foreach (CompraLocal Compralocal in listaComprasLocales)
                    {
                        total = total + Compralocal.Unidades;
                        if (titulo == "")
                        {
                            titulo = Compralocal.Aplicacion;
                            datos = Compralocal.Unidades.ToString();
                        }
                        else
                        {
                            titulo = titulo + "," + Compralocal.Aplicacion;
                            datos = datos + "," + Compralocal.Unidades.ToString();
                        }
                    }
                    totalstr = "Total: " + total.ToString();

                }
                return JsonConvert.SerializeObject(new { id = 3, titulo = titulo, datos = datos, total = totalstr, datos2 = listaComprasLocales });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = ex.Message });
            }
        }


        protected void BtnExcelCLPrecioAAA_ServerClick(object sender, EventArgs e)
        {
            //string Emp_CnxCentral = ConfigurationManager.AppSettings.Get("strConnectionCentral");
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
            CN_Compras_Locales CN_CL = new CN_Compras_Locales();
            CompraLocal CL = new CompraLocal();

            //string mesAnioInicial = ""; 
            //string mesAniofinal = "";
            //string Id_Cd = "";
            //string Id_Motivo = "";
            //string TipoPrecio = "";
            //string Mostrar = "";


            List<CompraLocal> listaComprasLocales = new List<CompraLocal>();

            CL.Id_Emp = Sesion.Id_Emp;
            CL.Id_Cd = Sesion.Id_Cd_Ver;
            //DateTime FechaInicio = FechaInicialAAA.Date;
            //DateTime FechaFin = FechaFinalAAA.Date;
            CL.Id_Motivo = int.Parse(cmbMotivoAAA.Value.ToString());
            CL.TipoAAA = int.Parse(cmbTipoPrecioAAA.Value.ToString());

            DataSet dsDatos = new DataSet();

            //CN_CL.ConsultaComprasLocalesXPrecioAAA(CL, ref dsDatos, FechaInicio, FechaFin, Sesion.Emp_Cnx);

            using (var workbook = new XLWorkbook())
            {
                var HojaExcel = workbook.Worksheets.Add("Reporte");

                HojaExcel.Cell(1, 1).Value = "No.Solicitud";
                HojaExcel.Cell(1, 1).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 2).Value = "# Sucursal";
                HojaExcel.Cell(1, 2).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 3).Value = "Sucursal";
                HojaExcel.Cell(1, 3).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 4).Value = "Solicitante";
                HojaExcel.Cell(1, 4).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                //HojaExcel.Cell(1, 5).Value = "Estatus";
                //HojaExcel.Cell(1, 5).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                //HojaExcel.Cell(1, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 5).Value = "# Motivo CL";
                HojaExcel.Cell(1, 5).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 6).Value = "Motivo CL";
                HojaExcel.Cell(1, 6).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 7).Value = "Fecha";
                HojaExcel.Cell(1, 7).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 8).Value = "Producto Padre";
                HojaExcel.Cell(1, 8).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 9).Value = "Producto CL";
                HojaExcel.Cell(1, 9).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 9).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 10).Value = "Descripción Prodcuto CL";
                HojaExcel.Cell(1, 10).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 10).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 11).Value = "Fecha Vencimiento CL";
                HojaExcel.Cell(1, 11).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 11).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 12).Value = "# Motivo";
                HojaExcel.Cell(1, 12).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 12).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 13).Value = "Motivo";
                HojaExcel.Cell(1, 13).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 13).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 14).Value = "# Proveedor Central";
                HojaExcel.Cell(1, 14).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 14).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 15).Value = "Nombre Proveedor Central";
                HojaExcel.Cell(1, 15).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 15).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 16).Value = "# Proveedor Local";
                HojaExcel.Cell(1, 16).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 16).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 17).Value = "Nombre Proveedor Local";
                HojaExcel.Cell(1, 17).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 17).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 18).Value = "Id Tipo Producto";
                HojaExcel.Cell(1, 18).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 18).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 19).Value = "Tipo de Producto";
                HojaExcel.Cell(1, 19).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 19).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 20).Value = "Aplicación";
                HojaExcel.Cell(1, 20).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 20).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 21).Value = "SubFamilia";
                HojaExcel.Cell(1, 21).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 21).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 22).Value = "Presentación";
                HojaExcel.Cell(1, 22).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 22).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 23).Value = "Unidad";
                HojaExcel.Cell(1, 23).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 23).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 24).Value = "Costo";
                HojaExcel.Cell(1, 24).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 24).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 25).Value = "PAAA código compra local";
                HojaExcel.Cell(1, 25).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 25).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 26).Value = "PAAA código key";
                HojaExcel.Cell(1, 26).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 26).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 27).Value = "Precio lista";
                HojaExcel.Cell(1, 27).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 27).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 28).Value = "Autorizador";
                HojaExcel.Cell(1, 28).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 28).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 29).Value = "Autorizador Nombre";
                HojaExcel.Cell(1, 29).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 29).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                //HojaExcel.Cell(1, 30).Value = "Fecha Venta";
                //HojaExcel.Cell(1, 30).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                //HojaExcel.Cell(1, 30).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 30).Value = "Venta Total";
                HojaExcel.Cell(1, 30).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 30).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 31).Value = "Cantidad (Unidades)";
                HojaExcel.Cell(1, 31).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 31).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                int renglon = 2;

                foreach (DataRow Row in dsDatos.Tables[0].Rows)
                {
                    HojaExcel.Cell(renglon, 1).Value = Row["Id_Comp"];
                    HojaExcel.Cell(renglon, 2).Value = Row["Id_Cd"];
                    HojaExcel.Cell(renglon, 3).Value = Row["Alm_Nombre"].ToString();
                    HojaExcel.Cell(renglon, 4).Value = Row["Solicitante"].ToString();
                    HojaExcel.Cell(renglon, 5).Value = Row["TipoSolicitud"];
                    HojaExcel.Cell(renglon, 6).Value = Row["Motivo"];
                    HojaExcel.Cell(renglon, 7).Value = Row["FechaSolicitud"];
                    HojaExcel.Cell(renglon, 8).Value = Row["Id_PrdOriginal"];
                    HojaExcel.Cell(renglon, 9).Value = Row["Id_Prd"];
                    HojaExcel.Cell(renglon, 10).Value = Row["Prd_Descripcion"];
                    HojaExcel.Cell(renglon, 11).Value = Row["Vigencia"];
                    HojaExcel.Cell(renglon, 12).Value = Row["IdCausaDesabasto"];
                    HojaExcel.Cell(renglon, 13).Value = Row["Comentarios"];
                    HojaExcel.Cell(renglon, 14).Value = Row["Id_Proveedor"];
                    HojaExcel.Cell(renglon, 15).Value = Row["ProveedorCentral"];

                    HojaExcel.Cell(renglon, 16).Value = Row["IdProveedor"];
                    HojaExcel.Cell(renglon, 17).Value = Row["Prd_NomPrvLocal"];
                    HojaExcel.Cell(renglon, 18).Value = Row["IdTipoProducto"];
                    HojaExcel.Cell(renglon, 19).Value = Row["TipoProducto"];
                    HojaExcel.Cell(renglon, 20).Value = Row["NomFamilia"];
                    HojaExcel.Cell(renglon, 21).Value = Row["NomSubFamilia"];

                    HojaExcel.Cell(renglon, 21).Value = Row["Prd_Presentacion"];
                    HojaExcel.Cell(renglon, 22).Value = "Unidad";
                    HojaExcel.Cell(renglon, 23).Value = Row["Costo"];

                    HojaExcel.Cell(renglon, 24).Value = Row["Costo"];
                    HojaExcel.Cell(renglon, 25).Value = Row["AAACL"];
                    HojaExcel.Cell(renglon, 26).Value = Row["AAAKEY"];
                    HojaExcel.Cell(renglon, 27).Value = Row["PrecioLista"];
                    HojaExcel.Cell(renglon, 28).Value = Row["IdUsuarioAutorizador"];
                    HojaExcel.Cell(renglon, 29).Value = Row["NomAutorizador"];
                    HojaExcel.Cell(renglon, 30).Value = Row["Pesos"];
                    HojaExcel.Cell(renglon, 31).Value = Row["Unidades"];

                    renglon = renglon + 1;

                }

                HojaExcel.Columns().AdjustToContents();

                string rutaguardado = Server.MapPath("~/Reportes/") + "ReporteComprasLocalesAAA_" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";


                if (File.Exists(rutaguardado))
                {
                    File.Delete(rutaguardado);
                }

                workbook.SaveAs(rutaguardado);



                string Outgoingfile = Server.MapPath("~/Reportes/") + "ReporteComprasLocalesAAA_" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";
                string ruta = Server.MapPath("~/Reportes/") + "ReporteComprasLocalesAAA_" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";
                // Prepare the response
                HttpResponse httpResponse = Response;
                httpResponse.Clear();
                httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                httpResponse.AddHeader("Content-Disposition", "attachment; filename=" + Outgoingfile);



                // Flush the workbook to the Response.OutputStream
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    workbook.SaveAs(memoryStream);
                    memoryStream.WriteTo(httpResponse.OutputStream);
                    memoryStream.Close();
                }

                httpResponse.End();
            }
        }

        protected void BtnDescargar_ServerClick(object sender, EventArgs e)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            DateTime FechaInicial = txtFechaInicialDescargar.Date;
            DateTime FechaFinal = txtFechaFinalDescargar.Date;
            int MotivoCL = int.Parse(cmbMotivoDes.Value.ToString());
            int TipoProductoCL = int.Parse(cmbTipoProductoDes.Value.ToString());
            int AplicacionCL = int.Parse(cmbAplicacionDes.Value.ToString());
            int ProductoCentralCL = int.Parse(cmbProductoCentralDes.Value.ToString());
            string ProveedorCentralCL = cmbProveedorCentralDes.Value.ToString();
            int ProveedorLocalCL = int.Parse(cmbProveedorLocalDes.Value.ToString());

            CompraLocal CL = new CompraLocal();
            List<eComprasLocales> listaComprasLocales = new List<eComprasLocales>();
            CN_Compras_Locales CN = new CN_Compras_Locales();
            DataSet dsDatos = new DataSet();

            CN.DescargaReporteCL(Sesion.Id_Emp, Sesion.Id_Cd_Ver, FechaInicial, FechaFinal, MotivoCL, TipoProductoCL, AplicacionCL, ProductoCentralCL, ProveedorCentralCL, ProveedorLocalCL, ref dsDatos, Sesion.Emp_Cnx);

            //gvDatosM.DataSource = listaComprasLocales;


            using (var workbook = new XLWorkbook())
            {
                var HojaExcel = workbook.Worksheets.Add("Reporte");

                HojaExcel.Cell(1, 1).Value = "No.Solicitud";
                HojaExcel.Cell(1, 1).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 2).Value = "# Sucursal";
                HojaExcel.Cell(1, 2).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 3).Value = "Sucursal";
                HojaExcel.Cell(1, 3).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 4).Value = "Solicitante";
                HojaExcel.Cell(1, 4).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 5).Value = "# Motivo CL";
                HojaExcel.Cell(1, 5).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 6).Value = "Motivo CL";
                HojaExcel.Cell(1, 6).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 7).Value = "Fecha";
                HojaExcel.Cell(1, 7).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 8).Value = "Producto Padre";
                HojaExcel.Cell(1, 8).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 9).Value = "Producto CL";
                HojaExcel.Cell(1, 9).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 9).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 10).Value = "Descripción Producto CL";
                HojaExcel.Cell(1, 10).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 10).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 11).Value = "Fecha Vencimiento CL";
                HojaExcel.Cell(1, 11).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 11).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 12).Value = "# Motivo";
                HojaExcel.Cell(1, 12).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 12).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 13).Value = "Motivo";
                HojaExcel.Cell(1, 13).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 13).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 14).Value = "# Proveedor Central";
                HojaExcel.Cell(1, 14).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 14).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 15).Value = "Nombre Proveedor Central";
                HojaExcel.Cell(1, 15).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 15).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 16).Value = "# Proveedor Local";
                HojaExcel.Cell(1, 16).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 16).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 17).Value = "Nombre Proveedor Local";
                HojaExcel.Cell(1, 17).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 17).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 18).Value = "Id Tipo Producto";
                HojaExcel.Cell(1, 18).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 18).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 19).Value = "Tipo de Producto";
                HojaExcel.Cell(1, 19).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 19).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 20).Value = "Aplicación";
                HojaExcel.Cell(1, 20).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 20).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 21).Value = "SubFamilia";
                HojaExcel.Cell(1, 21).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 21).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 22).Value = "Presentación";
                HojaExcel.Cell(1, 22).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 22).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 23).Value = "Unidad";
                HojaExcel.Cell(1, 23).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 23).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 24).Value = "Costo";
                HojaExcel.Cell(1, 24).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 24).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 25).Value = "PAAA código compra local";
                HojaExcel.Cell(1, 25).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 25).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 26).Value = "PAAA código key";
                HojaExcel.Cell(1, 26).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 26).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 27).Value = "Precio lista";
                HojaExcel.Cell(1, 27).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 27).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 28).Value = "Autorizador";
                HojaExcel.Cell(1, 28).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 28).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 29).Value = "Autorizador Nombre";
                HojaExcel.Cell(1, 29).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 29).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 30).Value = "Venta Total";
                HojaExcel.Cell(1, 30).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 30).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 31).Value = "Cantidad (Unidades)";
                HojaExcel.Cell(1, 31).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 31).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 32).Value = "Mes";
                HojaExcel.Cell(1, 32).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 32).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell(1, 33).Value = "Año";
                HojaExcel.Cell(1, 33).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                HojaExcel.Cell(1, 33).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                int renglon = 2;

                foreach (DataRow Row in dsDatos.Tables[0].Rows)
                {
                    HojaExcel.Cell(renglon,  1).Value = Row["Id_Comp"];
                    HojaExcel.Cell(renglon,  2).Value = Row["Id_Cd"];
                    HojaExcel.Cell(renglon,  3).Value = Row["Alm_Nombre"].ToString();
                    HojaExcel.Cell(renglon,  4).Value = Row["Solicitante"].ToString();
                    HojaExcel.Cell(renglon,  5).Value = Row["TipoSolicitud"];
                    HojaExcel.Cell(renglon,  6).Value = Row["Motivo"];
                    HojaExcel.Cell(renglon,  7).Value = Row["FechaSolicitud"];
                    HojaExcel.Cell(renglon,  8).Value = Row["Id_PrdOriginal"];
                    HojaExcel.Cell(renglon, 9).Value =  Row["Id_Prd"];
                    HojaExcel.Cell(renglon, 9).Style.NumberFormat.Format = "#";
                    HojaExcel.Cell(renglon, 10).Value = Row["Prd_Descripcion"];
                    HojaExcel.Cell(renglon, 11).Value = Row["Vigencia"];
                    HojaExcel.Cell(renglon, 12).Value = Row["IdCausaDesabasto"];
                    HojaExcel.Cell(renglon, 13).Value = Row["Comentarios"];
                    HojaExcel.Cell(renglon, 14).Value = Row["Id_Proveedor"];
                    HojaExcel.Cell(renglon, 15).Value = Row["ProveedorCentral"];
                    HojaExcel.Cell(renglon, 16).Value = Row["IdProveedor"];
                    HojaExcel.Cell(renglon, 17).Value = Row["Prd_NomPrvLocal"];
                    HojaExcel.Cell(renglon, 18).Value = Row["IdTipoProducto"];
                    HojaExcel.Cell(renglon, 19).Value = Row["TipoProducto"];
                    HojaExcel.Cell(renglon, 20).Value = Row["NomFamilia"];
                    HojaExcel.Cell(renglon, 21).Value = Row["NomSubFamilia"];
                    HojaExcel.Cell(renglon, 22).Value = Row["Prd_Presentacion"];
                    HojaExcel.Cell(renglon, 23).Value = Row["Prd_UniEmp"];
                    HojaExcel.Cell(renglon, 24).Value = "$" + Row["Costo"];
                    HojaExcel.Cell(renglon, 25).Value = "$" + Row["AAACL"];
                    HojaExcel.Cell(renglon, 26).Value = "$" + Row["AAAKEY"];
                    HojaExcel.Cell(renglon, 27).Value = "$" + Row["PrecioLista"];
                    HojaExcel.Cell(renglon, 28).Value = Row["IdUsuarioAutorizador"];
                    HojaExcel.Cell(renglon, 29).Value = Row["NomAutorizador"];
                    HojaExcel.Cell(renglon, 30).Value = "$" + Row["Pesos"];
                    HojaExcel.Cell(renglon, 31).Value = Row["Unidades"];
                    HojaExcel.Cell(renglon, 32).Value = Row["Mes"];
                    HojaExcel.Cell(renglon, 33).Value = Row["Año"];
                    renglon = renglon + 1;
                }

                HojaExcel.Columns().AdjustToContents();
                string rutaguardado = Server.MapPath("~/Reportes/") + "ReporteComprasLocales_" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";


                if (File.Exists(rutaguardado))
                {
                    File.Delete(rutaguardado);
                }

                workbook.SaveAs(rutaguardado);
                string Outgoingfile = "ReporteComprasLocales_" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";
                string ruta = Server.MapPath("~/Reportes/") + "ReporteComprasLocales_" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";
                // Prepare the response
                HttpResponse httpResponse = Response;
                httpResponse.Clear();
                httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                httpResponse.AddHeader("Content-Disposition", "attachment; filename=" + Outgoingfile);



                // Flush the workbook to the Response.OutputStream
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    workbook.SaveAs(memoryStream);
                    memoryStream.WriteTo(httpResponse.OutputStream);
                    memoryStream.Close();
                }

                httpResponse.End();
            }
        }

    }
}