using CapaDatos;
using CapaEntidad;
using CapaNegocios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIANWEB.DataSets.CatCliente;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using SIANWEB.Utilerias;
using DevExpress.Web.ASPxHtmlEditor.Internal;
using static SIANWEB.GestionPrecios.GestionIncrementoClientesDetalle;

namespace SIANWEB
{
    public partial class CatClientesV3 : System.Web.UI.Page
    {
        public static Sesion MySesion { get; set; }
        static string Emp_CnxCen
        {
            get { return ConfigurationManager.AppSettings.Get("strConnectionCentral"); }
        }
        static string Emp_CnxCob
        {
            get { return ConfigurationManager.AppSettings.Get("strConnectionCobranza"); }
        }
        static bool PermisoGuardar
        {
            get { return HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID] != null && (bool)HttpContext.Current.Session["PermisoGuardar" + HttpContext.Current.Session.SessionID + HttpContext.Current.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; }
            set { HttpContext.Current.Session["PermisoGuardar" + HttpContext.Current.Session.SessionID + HttpContext.Current.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; }
        }
        static bool PermisoModificar
        {
            get { return HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID] != null && (bool)HttpContext.Current.Session["PermisoModificar" + HttpContext.Current.Session.SessionID + HttpContext.Current.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; }
            set { HttpContext.Current.Session["PermisoModificar" + HttpContext.Current.Session.SessionID + HttpContext.Current.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; }
        }
        static string PageTitle { get; set; }
        static string NomComercial
        {
            get { return HttpContext.Current.Session["_nomComercial"].ToString(); }
            set { HttpContext.Current.Session["_nomComercial"] = value; }
        }
        static CatClienteDS DataSet
        {
            get { return (CatClienteDS)HttpContext.Current.Session["cat_ctedet_ds" + HttpContext.Current.Session.SessionID + HttpContext.Current.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; }
            set { HttpContext.Current.Session["cat_ctedet_ds" + HttpContext.Current.Session.SessionID + HttpContext.Current.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; }
        }

        /* se guardan los territorios autorizados */
        static DataTable DtTablaTerritorios
        {
            get { return (DataTable)HttpContext.Current.Session["DtTableTerritorios"]; }
            set { HttpContext.Current.Session["DtTableTerritorios"] = value; }
        }

        /* se guardan los territoriosAnt */
        static DataTable DtTablaTerritoriosAnt
        {
            get { return (DataTable)HttpContext.Current.Session["DtTableTerritoriosAnt"]; }
            set { HttpContext.Current.Session["DtTableTerritoriosAnt"] = value; }
        }

        static DataTable DtTablaTerritorios_ViewModel
        {
            get { return (DataTable)HttpContext.Current.Session["DtTableTerritorios_viewmodel"]; }
            set { HttpContext.Current.Session["DtTableTerritorios_viewmodel"] = value; }
        }

        static DataTable DtTableDireccionEntrega
        {
            get { return (DataTable)HttpContext.Current.Session["DtTableDireccionEntrega"]; }
            set { HttpContext.Current.Session["DtTableDireccionEntrega"] = value; }
        }
        static List<int> lstTerActivo
        {
            get { return (List<int>)HttpContext.Current.Session["_lstTerActivo"]; }
            set { HttpContext.Current.Session["_lstTerActivo"] = value; }
        }
        static List<int> lstTerInactivo
        {
            get { return (List<int>)HttpContext.Current.Session["_lstTerInactivo"]; }
            set { HttpContext.Current.Session["_lstTerInactivo"] = value; }
        }










        protected void Page_Load(object sender, EventArgs e)
        {
            MySesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (MySesion == null)
            {
                var pag = Page.Request.Url
                    .ToString()
                    .Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                Response.Redirect("login.aspx", false);
                return;
            }

            if (!Page.IsPostBack)
            {
                ValidarPermisos();
                DataSet = new CatClienteDS();
                DtTablaTerritorios_ViewModel = InicializarTablaTerritorios_ViewModel();
                DtTablaTerritorios = InicializarTablaTerritorios(true);
                DtTablaTerritoriosAnt = InicializarTablaTerritorios(false);
                DtTableDireccionEntrega = InicializarDtTableDireccionEntrega();
                lstTerActivo = new List<int>();
                lstTerInactivo = new List<int>();
            }
        }









        #region
        public class ResponseJson_SelectFormat
        {
            public string Descripcion { get; set; }
            public int Id { get; set; }
            public string StringId { get; set; }
        }

        public class ResponseJson
        {
            public string Mensaje { get; set; }
            public bool Estatus { get; set; } = false;
        }

        public class ResponseJsonTerritorios : ResponseJson
        {
            public bool EnviarComentarios { get; set; }
        }
        #endregion







        #region inicializar

        public class Configuracion
        {
            public string Title { get; set; }
            public bool PermisosGuardar { get; set; }
            public bool PermisoModificar { get; set; }
            public bool PermisoModificarTerritorios { get; set; }
            public bool MostrarTabBennets { get; set; }
        }

        public class DatosDelCliente
        {
            public int IdCliente { get; set; }
            public string FechaUltimaModificacion { get; set; }
            public string Usuario { get; set; }
            public DatosGenerales DatosGenerales { get; set; }
            public List<DireccionEntrega> DireccionEntrega { get; set; }
            public List<Territorios> Territorios { get; set; }
            public Cobranza Cobranza { get; set; }
            public CatalogoAdicional CatalogoAdicionalBennets { get; set; }
            public string Mensaje { get; set; }
            public bool TerritoriosPendientesPorAceptar { get; set; }
        }

        [WebMethod]
        public static Configuracion ObtenerConfiguracionInicial()
        {
            return new Configuracion
            {
                Title = PageTitle,
                PermisosGuardar = PermisoGuardar,
                PermisoModificar = PermisoModificar,
                //PermisoModificarTerritorios = MySesion.Id_TU == 23 // jefe de operaciones
                PermisoModificarTerritorios = MySesion.Id_TU == 3,
                //PermisoModificarTerritorios = true,
                MostrarTabBennets = MySesion.Id_Cd == 34120
            };
        }

        [WebMethod]
        public static ResponseJson CambiarCentroDistribucion(string centroDistribucion, int idCentroDistribucion)
        {
            var result = new ResponseJson();
            if (!OpenSession())
            {
                result.Mensaje = "connClose";
                return result;
            }

            MySesion.Id_Cd_Ver = idCentroDistribucion;
            MySesion.Cd_Nombre = centroDistribucion;
            CN_CatCalendario cn_catcalendario = new CN_CatCalendario();
            Calendario calendario = new Calendario();
            cn_catcalendario.ConsultaCalendarioActual(ref calendario, MySesion);
            MySesion.CalendarioIni = calendario.Cal_FechaIni;
            MySesion.CalendarioFin = calendario.Cal_FechaFin;
            result.Estatus = true;
            return result;
        }

        [WebMethod]
        public static List<ResponseJson_SelectFormat> ObtenerCentros()
        {
            var result = new List<ResponseJson_SelectFormat>();
            try
            {
                var list = new List<Comun>();
                int multiOfi = MySesion.U_MultiOfi ? 1 : 2;
                new CD__Comun().LlenaCombo(multiOfi, MySesion.Id_Emp, MySesion.Id_U, "spCatCentroDistribucion_Combo", MySesion.Emp_Cnx, ref list);

                if (list.Count > 0)
                {
                    result = list.Select(x => new ResponseJson_SelectFormat()
                    {
                        Descripcion = x.Descripcion,
                        Id = x.Id
                    }).ToList();
                }
            }
            catch { }
            return result;
        }

        [WebMethod]
        public static List<ResponseJson_SelectFormat> ObtenerClientes()
        {
            var result = new List<ResponseJson_SelectFormat>();
            try
            {
                var list = new List<Comun>();
                new CD__Comun().LlenaCombo(
                    2,
                    MySesion.Id_Emp,
                    MySesion.Id_Cd_Ver,
                    null,
                    MySesion.Id_Rik == -1 ? null : (int?)MySesion.Id_Rik,
                    "spCatCliente_Combo",
                    MySesion.Emp_Cnx,
                    ref list
                );

                if (list.Count > 0)
                {
                    result = list.Select(x => new ResponseJson_SelectFormat()
                    {
                        Descripcion = x.Descripcion,
                        Id = x.Id
                    }).ToList();
                }
            }
            catch { }
            return result;
        }

        [WebMethod]
        public static List<ResponseJson_SelectFormat> ObtenerUEN()
        {
            var result = new List<ResponseJson_SelectFormat>();
            try
            {
                var list = new CD_CatUen().SelCatUen(MySesion.Id_Emp, 0, MySesion.Emp_Cnx);
                list.Insert(0, new Uen
                {
                    Id_Uen = -1,
                    Descripcion = "-- Seleccionar --"
                });

                result = list.Select(x => new ResponseJson_SelectFormat()
                {
                    Descripcion = x.Descripcion,
                    Id = x.Id_Uen
                }).ToList();
            }
            catch { }
            return result;
        }

        [WebMethod]
        public static List<ResponseJson_SelectFormat> ObtenerUsosCfdi()
        {
            var result = new List<ResponseJson_SelectFormat>();
            using (var cn = new SqlConnection(MySesion.Emp_Cnx))
            {
                var usosCfdi = new List<string>();
                try
                {
                    cn.Open();
                    using (var cmd = new SqlCommand("SELECT Id, Descripcion FROM siancentral.siancentral.dbo.usocfdi", cn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            int i = 0;
                            while (reader.Read())
                            {
                                i++;
                                result.Add(new ResponseJson_SelectFormat
                                {
                                    Id = i,
                                    StringId = reader["Id"].ToString(),
                                    Descripcion = reader["Descripcion"].ToString(),
                                });
                            }
                        }
                    }
                }
                catch { }
                return result;
            }
        }

        [WebMethod]
        public static List<ResponseJson_SelectFormat> ObtenerSegmentos(int idUen)
        {
            var result = new List<ResponseJson_SelectFormat>();
            try
            {
                var list = new CD_CatSegmentos().SelCatSegmento(MySesion.Id_Emp, idUen, MySesion.Emp_Cnx);
                list.Insert(0, new eSegmento
                {
                    Id_Seg = -1,
                    Seg_Descripcion = "-- Seleccionar --"
                });

                result = list.Select(x => new ResponseJson_SelectFormat()
                {
                    Descripcion = x.Seg_Descripcion,
                    Id = x.Id_Seg
                }).ToList();
            }
            catch { }
            return result;
        }

        [WebMethod]
        public static List<ResponseJson_SelectFormat> ObtenerPaises()
        {
            var result = new List<ResponseJson_SelectFormat>();
            try
            {
                var list = new List<Comun>();
                new CD__Comun().LlenaCombo(
                    1,
                    MySesion.Id_Emp,
                    MySesion.Id_Cd_Ver,
                    2,
                    "spCatPaises_Combo",
                    MySesion.Emp_Cnx,
                    ref list
                );

                if (list.Count > 0)
                {
                    result = list.Select(x => new ResponseJson_SelectFormat()
                    {
                        Descripcion = x.Descripcion,
                        Id = x.Id
                    }).ToList();
                }
            }
            catch { }
            return result;
        }

        [WebMethod]
        public static List<ResponseJson_SelectFormat> ObtenerEstados(int idPais)
        {
            var result = new List<ResponseJson_SelectFormat>();
            try
            {
                var list = new List<Comun>();
                new CD__Comun().LlenaCombo(
                    idPais,
                    "spCatEstados_Combo",
                    MySesion.Emp_Cnx,
                    ref list
                );

                if (list.Count > 0)
                {
                    result = list.Select(x => new ResponseJson_SelectFormat()
                    {
                        Descripcion = x.Descripcion,
                        Id = x.Id
                    }).ToList();
                }
            }
            catch { }
            return result;
        }

        [WebMethod]
        public static DatosDelCliente ObtenerDatosDelCliente(int idCliente)
        {
            HttpContext.Current.Session["autorizoelcambiotipocliente"] = null;
            HttpContext.Current.Session["autorizo!"] = null;
            var result = new DatosDelCliente();
            if (!OpenSession())
            {
                result.Mensaje = "connClose";
                return result;
            }

            try
            {
                DtTablaTerritorios.Rows.Clear();
                DtTablaTerritoriosAnt.Rows.Clear();
                DtTablaTerritorios_ViewModel.Rows.Clear();
                lstTerActivo = new List<int>();
                lstTerInactivo = new List<int>();

                var cte = new Clientes
                {
                    Id_Emp = MySesion.Id_Emp,
                    Id_Cd = MySesion.Id_Cd_Ver,
                    Id_Cte = idCliente,
                    Ignora_Inactivo = true
                };

                var catCliente = new CN_CatCliente();
                catCliente.ConsultaClientes(ref cte, MySesion.Emp_Cnx);
                result.IdCliente = cte.Id_Cte.Value;
                result.FechaUltimaModificacion = cte.Cte_Modfecha.ToShortDateString();
                result.Usuario = cte.U_Nombre;


                //DATOS GENERALES --->
                NomComercial = cte.Cte_NomComercial;
                result.DatosGenerales = new DatosGenerales
                {
                    Activo = cte.Estatus,
                    Sucursal = cte.Cte_EsSucursal,
                    RazonSocial = cte.Cte_NomComercial,
                    NombreComercial = cte.Cte_NomCorto,
                    Uen = cte.Id_Uen.Value > 0 ? cte.Id_Uen.Value : -1,
                    Segmento = cte.Id_Seg.Value > 0 ? cte.Id_Seg.Value : -1,
                    TipoCliente = cte.Id_TCte,
                    CuentaCorporativa = cte.Id_Corp,
                    Contacto = cte.Cte_Contacto,
                    Email = cte.Cte_Email,
                    Calle = cte.Cte_FacCalle,
                    Colonia = cte.Cte_FacColonia,
                    Numero = cte.Cte_FacNumero,
                    CP = cte.Cte_FacCp,
                    Estado = cte.Cte_FacEstado,
                    Municipio = cte.Cte_FacMunicipio,
                    Telefonos = cte.Cte_FacTel,
                    RFC = cte.Cte_FacRfc,
                    RegimentFiscal = cte.Cte_RegimenFiscal,
                    AsignacionDePedido = cte.Cte_AsignacionPed,
                    Vinculado = cte.Vinculado == "S",
                    EstadoId = cte.Id_CteEstado,
                    PaisId = cte.Id_CtePais
                };
                //<-----

                //DIRECCION ENTREGA ---->
                ObtenerDireccionEntregas_ClienteSeleccionado(cte.Id_Cte.Value);
                result.DireccionEntrega = ObtenerDtTableDireccionEntrega();
                //<------

                //TERRITORIOS --->
                ObtenerTerritorios_ClienteSeleccionado(cte.Id_Cte.Value);
                result.Territorios = ObtenerDtTableTerritorios();
                result.TerritoriosPendientesPorAceptar = !ChecarTerritoriosPendientes(idCliente);
                //<-----

                catCliente.ConsultarClienteFormaPago(ref cte, MySesion.Emp_Cnx);
                //COBRANZA ----->
                result.Cobranza = new Cobranza
                {
                    Credito = cte.Cte_Credito,
                    PermiteFacturar = cte.Cte_Facturacion,
                    CreditoSuspendido = cte.Cte_CreditoSuspendido,
                    HabilitarCreditoSuspendido = ValidarCreditoSuspendido(idCliente),
                    Moneda = cte.Id_Mon,
                    LimiteCredito = cte.Cte_LimCobr,
                    CondicionPagoDias = cte.Cte_CondPago,
                    CondicionPagoAutorizo = cte.UPlazo,
                    CorreoEdoCuenta1 = cte.Cte_CorreoEdoCuenta1,
                    CorreoEdoCuenta2 = cte.Cte_CorreoEdoCuenta2,
                    CorreoEdoCuenta3 = cte.Cte_CorreoEdoCuenta3,
                    RHoraAm1 = cte.Cte_RHoraam1,
                    RHoraAm2 = cte.Cte_RHoraam2,
                    //RHoraPm1 = cte.Cte_RHorapm1,
                    //RHoraPm2 = cte.Cte_RHorapm2,
                    SemanaRevision1 = cte.Cte_SemRev,
                    SemanaRevision2 = cte.Cte_SemRev2,
                    RLunes = cte.Cte_RLunes,
                    RMartes = cte.Cte_RMartes,
                    RMiercoles = cte.Cte_RMiercoles,
                    RJueves = cte.Cte_RJueves,
                    RViernes = cte.Cte_RViernes,
                    RSabado = cte.Cte_RSabado,
                    RDomingo = cte.Cte_RDomingo,
                    PHoraAm1 = cte.Cte_PHoraam1,
                    PHoraAm2 = cte.Cte_PHoraam2,
                    //PHoraPm1 = cte.Cte_PHorapm1,
                    //PHoraPm2 = cte.Cte_PHorapm2,
                    SemanaPago = cte.Cte_SemCob,
                    PLunes = cte.Cte_CPLunes,
                    PMartes = cte.Cte_CPMartes,
                    PMiercoles = cte.Cte_CPMiercoles,
                    PJueves = cte.Cte_CPJueves,
                    PViernes = cte.Cte_CPViernes,
                    PSabado = cte.Cte_CPSabado,
                    PDomingo = cte.Cte_CPDomingo,
                    SemanaRecepcion = cte.Cte_SemRec,
                    RecLunes = cte.Cte_RecLunes,
                    RecMartes = cte.Cte_RecMartes,
                    RecMiercoles = cte.Cte_RecMiercoles,
                    RecJueves = cte.Cte_RecJueves,
                    RecViernes = cte.Cte_RecViernes,
                    RecSabado = cte.Cte_RecSabado,
                    RecDomingo = cte.Cte_RecDomingo,
                    TelefonoCobranza1 = cte.Cte_TelCobranza1,
                    TelefonoCobranza2 = cte.Cte_TelCobranza2,
                    DesgloseIva = cte.Cte_DesgIva,
                    RetencionIva = cte.Cte_RetIva,
                    PorcientoRetencion = cte.PorcientoRetencion,
                    Documento = cte.Cte_Documentos.Replace("'", ""),
                    IvaCliente = cte.BPorcientoIVA,
                    PorcentajeIvaCliente = cte.PorcientoIVA,
                    UsoCFDI = string.IsNullOrEmpty(cte.Cte_UsoCFDI) ? "G01" : cte.Cte_UsoCFDI,
                    MetodoPago = string.IsNullOrEmpty(cte.Cte_MetodoPago) ? "PPD" : cte.Cte_MetodoPago,
                    PagoUsoCFDI = string.IsNullOrEmpty(cte.Cte_PagoUsoCFDI) ? "P01" : cte.Cte_PagoUsoCFDI,
                    PagoMetodoPago = string.IsNullOrEmpty(cte.Cte_PagoMetodoPago) ? "3" : cte.Cte_PagoMetodoPago,
                    PagoBanco = cte.Cte_PagoIdBan,
                    PagoNumeroCuenta = cte.Cte_PagoNumeroCuenta,
                    PagoCorreos = cte.Cte_PagoCorreos,
                    NCUsoCFDI = string.IsNullOrEmpty(cte.Cte_NCUsoCFDI) ? "P01" : cte.Cte_NCUsoCFDI,
                    NCFormaPago = string.IsNullOrEmpty(cte.Cte_NCFormaPago) ? "3" : cte.Cte_NCFormaPago,
                    NCMetodoPago = string.IsNullOrEmpty(cte.Cte_NCMetodoPago) ? "PPD" : cte.Cte_NCMetodoPago,
                    Banco = cte.Id_Ban,
                    NumeroCuenta = cte.Cte_NumeroCuenta,
                    ReferenciaTecleada = cte.Cte_ReferenciaTecleada,
                    Portal = cte.Cte_Portal,
                    UDigitos = cte.Cte_UDigitos,
                    Comisiones = cte.Cte_Comisiones,
                    OrdenCompra = cte.Cte_ReqOrdenCompra,
                    NotaCreditoFacturar = cte.BPorcNotaCredito,
                    PorcentajeNotaCreditoFacturar = cte.PorcientoNotaCredito,
                    VersionCFDI = cte.Cte_VersionCFDI,
                    RemisionElectronica = cte.Cte_RemisionElectronica,
                    Serie = cte.Id_Cfe,
                    SerieNC = cte.Cte_SerieNCre,
                    SerieNCargo = cte.Cte_SerieNCa,
                    Adenda = cte.Id_Ade,
                    FormasDePago = cte.FormasPago == null ? new int[] { } : cte.FormasPago.Select(x => x.Id_Fpa).ToArray(),
                    RevPago = cte.RevPago
                };

                string h = "";
                string rpm1 = cte.Cte_RHorapm1;
                if (!string.IsNullOrEmpty(rpm1))
                {
                    h = (rpm1.ToLower().Contains("p.m.") || rpm1.ToLower().Contains("a.m.")) ? rpm1.Substring(0, 5) : rpm1;
                    var pm1 = DateTime.Parse(h);
                    if (pm1.Hour < 12) result.Cobranza.RHoraPm1 = $"{24 + (pm1.Hour - 12)}:{pm1.ToString("mm")}";
                    else result.Cobranza.RHoraPm1 = rpm1;
                }

                string rpm2 = cte.Cte_RHorapm2;
                if (!string.IsNullOrEmpty(rpm2))
                {
                    h = (rpm2.ToLower().Contains("p.m.") || rpm2.ToLower().Contains("a.m.")) ? rpm2.Substring(0, 5) : rpm2;
                    var pm2 = DateTime.Parse(h);
                    if (pm2.Hour < 12) result.Cobranza.RHoraPm2 = $"{24 + (pm2.Hour - 12)}:{pm2.ToString("mm")}";
                    else result.Cobranza.RHoraPm2 = rpm2;
                }

                string ppm1 = cte.Cte_PHorapm1;
                if (!string.IsNullOrEmpty(ppm1))
                {
                    h = (ppm1.ToLower().Contains("p.m.") || ppm1.ToLower().Contains("a.m.")) ? ppm1.Substring(0, 5) : ppm1;
                    var pm1 = DateTime.Parse(h);
                    if (pm1.Hour < 12) result.Cobranza.PHoraPm1 = $"{24 + (pm1.Hour - 12)}:{pm1.ToString("mm")}";
                    else result.Cobranza.PHoraPm1 = ppm1;
                }

                string ppm2 = cte.Cte_PHorapm2;
                if (!string.IsNullOrEmpty(ppm2))
                {
                    h = (ppm2.ToLower().Contains("p.m.") || ppm2.ToLower().Contains("a.m.")) ? ppm2.Substring(0, 5) : ppm2;
                    var pm2 = DateTime.Parse(h);
                    if (pm2.Hour < 12) result.Cobranza.PHoraPm2 = $"{24 + (pm2.Hour - 12)}:{pm2.ToString("mm")}";
                    else result.Cobranza.PHoraPm2 = ppm2;
                }
                //<----------


                //DIRECCION ENTREGA ---->
                if (MySesion.Id_Cd == 34120)
                {
                    result.CatalogoAdicionalBennets = ConsultarSeleccionCatalogoAdicional(cte.Id_Cte.Value);
                }
                //<------

                var usu = new Usuario
                {
                    Id_Emp = MySesion.Id_Emp,
                    Id_Cd = MySesion.Id_Cd,
                    Id_U = MySesion.Id_U
                };
                new CN_CatUsuario().ConsultaUsuarios(ref usu, MySesion.Emp_Cnx);

                int dias = cte.Cte_DiasVencidos;
                //result.Cobranza.EnableCreditoSuspendido = usu.U_SusCredito && usu.U_DiasVencimiento >= dias;
                result.Cobranza.EnableCreditoSuspendido = ValidarCheckCobranza(cte.Id_Cte.Value);
            }
            catch (Exception ex) { string x = ex.Message; }

            return result;
        }

        private static bool ValidarCheckCobranza(int id_cte)
        {
            int verificador = 0;
            new CN_CatUsuario().ConsultaModificarCredito(id_cte, MySesion, ref verificador);

            if (verificador == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        [WebMethod]
        public static ResponseJson InicializarValores()
        {
            var result = new ResponseJson();
            if (!OpenSession())
            {
                result.Mensaje = "connClose";
                return result;
            }

            DtTableDireccionEntrega.Rows.Clear();
            DtTableDireccionEntrega.AcceptChanges();
            DtTablaTerritorios.Rows.Clear();
            DtTablaTerritorios.AcceptChanges();
            DtTablaTerritoriosAnt.Rows.Clear();
            DtTablaTerritoriosAnt.AcceptChanges();
            DtTablaTerritorios_ViewModel.Rows.Clear();
            DtTablaTerritorios_ViewModel.AcceptChanges();
            HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID + "autorizacionVinculacion"] = null;
            result.Estatus = true;
            return result;
        }

        void ValidarPermisos()
        {
            try
            {
                var pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                string url = (new FileInfo(Page.Request.Url.AbsolutePath)).Name + (pag.Length > 1 ? ("?" + pag[1]) : "");

                pagina.Url = url;

                new CN_Pagina().PaginaConsultar(ref pagina, MySesion.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                PageTitle = pagina.Descripcion;

                var permiso = new Permiso
                {
                    Id_U = MySesion.Id_U,
                    Id_Cd = MySesion.Id_Cd,
                    Sm_cve = pagina.Clave
                };
                //Esta clave depende de la pantalla
                new CD_PermisosU().ValidaPermisosUsuario(ref permiso, MySesion.Emp_Cnx);

                if (permiso.PAccesar)
                {
                    PermisoGuardar = permiso.PGrabar;
                    PermisoModificar = permiso.PModificar;
                }
                else Response.Redirect("Inicio.aspx");
            }
            catch { }
        }

        static bool ValidarCreditoSuspendido(int idCliente)
        {
            try
            {
                int verificador = 0;
                new CN_CatUsuario().ConsultaModificarCredito(idCliente, MySesion, ref verificador);

                return verificador == 1;
            }
            catch { }
            return false;
        }

        static bool OpenSession()
        {
            return MySesion != null && DtTablaTerritorios != null && DtTablaTerritoriosAnt != null
                && DtTablaTerritorios_ViewModel != null && DtTableDireccionEntrega != null;
        }
        #endregion







        #region datos generales
        public class DatosGenerales
        {
            public bool Activo { get; set; }
            public bool Sucursal { get; set; }
            public string RazonSocial { get; set; }
            public string NombreComercial { get; set; }
            public int Uen { get; set; }
            public int Segmento { get; set; }
            public int TipoCliente { get; set; }
            public int CuentaCorporativa { get; set; }
            public string Contacto { get; set; }
            public string Email { get; set; }
            public string Calle { get; set; }
            public string Colonia { get; set; }
            public string Numero { get; set; }
            public string CP { get; set; }
            public string Estado { get; set; }
            public string Municipio { get; set; }
            public string Telefonos { get; set; }
            public string RFC { get; set; }
            public int RegimentFiscal { get; set; }
            public int AsignacionDePedido { get; set; }
            public bool Vinculado { get; set; }
            public int PaisId { get; set; }
            public int EstadoId { get; set; }
        }
        public class MaximoId
        {
            public string Id { get; set; }
        }

        [WebMethod]
        public static MaximoId ObtenerMaximoId()
        {
            var result = new MaximoId();
            try
            {
                result.Id = new CN__Comun().Maximo(MySesion.Id_Emp, MySesion.Id_Cd_Ver, "CatCliente", "Id_Cte", MySesion.Emp_Cnx, "spCatLocal_Maximo");
            }
            catch { }
            return result;
        }

        [WebMethod]
        public static List<ResponseJson_SelectFormat> ObtenerTiposDeCliente()
        {
            var result = new List<ResponseJson_SelectFormat>();
            try
            {
                var list = new List<Comun>();
                new CD__Comun().LlenaCombo(1, MySesion.Id_Emp, MySesion.Id_Cd_Ver, MySesion.VersionTerritorio ? "spCatTCliente_ComboVTerritorio" : "spCatTCliente_Combo", MySesion.Emp_Cnx, ref list);

                if (list.Count > 0)
                {
                    result = list.Select(x => new ResponseJson_SelectFormat()
                    {
                        Descripcion = x.Descripcion,
                        Id = x.Id
                    }).ToList();
                }
            }
            catch { }
            return result;
        }

        [WebMethod]
        public static List<ResponseJson_SelectFormat> ObtenerCuentasCorporativas()
        {
            var result = new List<ResponseJson_SelectFormat>();
            try
            {
                var list = new List<Comun>();
                new CD__Comun().LlenaCombo(MySesion.Id_Emp, "spCatCuentaCorp_Combo", Emp_CnxCen, ref list);

                if (list.Count > 0)
                {
                    result = list.Select(x => new ResponseJson_SelectFormat()
                    {
                        Descripcion = x.Descripcion,
                        Id = x.Id
                    }).ToList();
                }
            }
            catch { }
            return result;
        }

        [WebMethod]
        public static List<ResponseJson_SelectFormat> ObtenerRegimenFiscal()
        {
            var result = new List<ResponseJson_SelectFormat>();
            try
            {
                var list = new List<Comun>();
                new CD__Comun().LlenaCombo("Sp_CatRegimenFiscal_Combo", MySesion.Emp_Cnx, ref list);

                if (list.Count > 0)
                {
                    result = list.Select(x => new ResponseJson_SelectFormat()
                    {
                        Descripcion = x.Descripcion,
                        Id = x.Id
                    }).ToList();
                }
            }
            catch { }
            return result;
        }

        [WebMethod]
        public static List<ResponseJson_SelectFormat> ObtenerAsignacionDePedido()
        {
            return new List<ResponseJson_SelectFormat>()
            {
                new ResponseJson_SelectFormat
                {
                    Id = -1,
                    Descripcion = "-- Seleccionar --"
                },
                new ResponseJson_SelectFormat
                {
                    Id = 0,
                    Descripcion = "Dependiendo de existencia"
                },
                new ResponseJson_SelectFormat
                {
                    Id = 1,
                    Descripcion = "Sólo partidas completas"
                }
            };
        }

        /// <summary>
        /// se ejecuta cuando es false el checkbox activo
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static ResponseJson DeshabilitarCliente(int clienteId)
        {
            var result = new ResponseJson();
            if (!OpenSession())
            {
                result.Mensaje = "connClose";
                return result;
            }

            try
            {
                bool verificador = false;
                var catalogo = new Catalogo
                {
                    Id_Emp = MySesion.Id_Emp,
                    Id_Cd = MySesion.Id_Cd_Ver,
                    Id = clienteId,
                    Tabla = "CatCliente",
                    Columna = "Id_Cte"
                };

                new CN__Comun().Deshabilitar(catalogo, MySesion.Emp_Cnx, ref verificador);
                result.Estatus = verificador;
                result.Mensaje = !verificador ? "El registro está siendo utilizado por otro componente" : "";
            }
            catch { }
            return result;
        }

        /// <summary>
        /// se ejecuta cuando se selecciona tipo de cliente
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static ResponseJson ConsultaTipoDeCliente(int tipoDeCliente)
        {
            var result = new ResponseJson();
            if (!OpenSession())
            {
                result.Mensaje = "connClose";
                return result;
            }
            try
            {
                var list = new List<Comun>();
                var cte = new Clientes
                {
                    Id_Emp = MySesion.Id_Emp,
                    Id_Cd = MySesion.Id_Cd_Ver,
                    Id_TCte = tipoDeCliente
                };

                new CN_CatCliente().ConsultaClienteTipo(cte, MySesion.Emp_Cnx, ref list);
                if (list.Count > 0)
                {
                    bool conCuentaCorporativa = list[0].ValorBool;
                    /*
                     * conCuentaCorporativa
                     * false = inahabilitamos el select de cuentas corporativos
                     */
                    result.Estatus = conCuentaCorporativa;
                }
            }
            catch { }
            return result;
        }
        #endregion






        #region direccion entrega
        public class DireccionEntrega
        {
            public int Id { get; set; }
            public string Calle { get; set; }
            public string Numero { get; set; }
            public string CP { get; set; }
            public string Colonia { get; set; }
            public string Municipio { get; set; }
            public string Estado { get; set; }
            public string Sector { get; set; }
            public string Telefono { get; set; }
            public string Fax { get; set; }
            public string HoraAm1 { get; set; }
            public string HoraAm2 { get; set; }
            public string HoraPm1 { get; set; }
            public string HoraPm2 { get; set; }
            public int RutaId { get; set; }
            public string Ruta { get; set; }
            public bool Edit { get; set; }
            public int EstadoId { get; set; }
            public string Pais { get; set; }
            public int PaisId { get; set; }
        }

        [WebMethod]
        public static List<ResponseJson_SelectFormat> ObtenerRutas()
        {
            var result = new List<ResponseJson_SelectFormat>();
            try
            {
                var list = new List<Comun>();
                new CD__Comun().LlenaCombo(1, MySesion.Id_Emp, MySesion.Id_Cd_Ver, 1, "spCatRutas_Combo", MySesion.Emp_Cnx, ref list);

                if (list.Count > 0)
                {
                    result = list.Select(x => new ResponseJson_SelectFormat()
                    {
                        Descripcion = x.Descripcion,
                        Id = x.Id
                    }).ToList();
                }
            }
            catch { }
            return result;
        }

        [WebMethod]
        public static ResponseJson AgregarDireccionEntrega(DireccionEntrega req)
        {
            var result = new ResponseJson();
            if (!OpenSession())
            {
                result.Mensaje = "connClose";
                return result;
            }

            try
            {
                DtTableDireccionEntrega.Rows.Add(
                    1,
                    DtTableDireccionEntrega.Rows.Count,
                    req.Calle,
                    req.Numero,
                    req.CP,
                    req.Colonia,
                    req.Municipio,
                    req.Estado,
                    req.Sector,
                    req.Telefono,
                    req.Fax,
                    req.HoraAm1,
                    req.HoraAm2,
                    req.HoraPm1,
                    req.HoraPm2,
                    req.Ruta,
                    req.RutaId,
                    req.PaisId,
                    req.EstadoId,
                    req.Pais,
                    req.Estado
                );

                result.Estatus = true;
            }
            catch (Exception ex)
            {
                result.Mensaje = ex.Message;
            }

            return result;
        }

        [WebMethod]
        public static List<DireccionEntrega> ObtenerDtTableDireccionEntrega()
        {
            var result = new List<DireccionEntrega>();
            try
            {
                result = DtTableDireccionEntrega.Rows.OfType<DataRow>().Select(x => new DireccionEntrega()
                {
                    Id = x.Field<int>("Id_CteDirEntrega"),
                    Calle = x.Field<string>("Cte_Calle"),
                    Numero = x.Field<string>("Cte_Numero"),
                    CP = x.Field<string>("Cte_Cp"),
                    Colonia = x.Field<string>("Cte_Colonia"),
                    Municipio = x.Field<string>("Cte_Municipio"),
                    //Estado = x.Field<string>("Cte_Estado"),
                    Estado = x.Field<string>("Nom_Estado"),
                    Sector = x.Field<string>("Cte_Sector"),
                    Telefono = x.Field<string>("Cte_Telefono"),
                    Fax = x.Field<string>("Cte_Fax"),
                    HoraAm1 = x.Field<string>("Cte_HoraAm1"),
                    HoraAm2 = x.Field<string>("Cte_HoraAm2"),
                    HoraPm1 = x.Field<string>("Cte_HoraPm1"),
                    HoraPm2 = x.Field<string>("Cte_HoraPm2"),
                    Ruta = x.Field<string>("Ruta_Entrega"),
                    RutaId = x.Field<int>("Ruta_EntregaId_Rut"),
                    Edit = false,
                    EstadoId = x.Field<int>("Id_EEstado"),
                    PaisId = x.Field<int>("Id_EPais"),
                    Pais = x.Field<string>("Nom_Pais")
                }).ToList();
            }
            catch { }
            return result;
        }

        [WebMethod]
        public static ResponseJson EliminarDireccionEntrega(int idDireccionEntrega)
        {
            var result = new ResponseJson();
            if (!OpenSession())
            {
                result.Mensaje = "connClose";
                return result;
            }

            try
            {
                var row = DtTableDireccionEntrega.Select("Id_CteDirEntrega='" + idDireccionEntrega + "'");
                row[0].Delete();
                DtTableDireccionEntrega.AcceptChanges();

                result.Estatus = true;
            }
            catch { }
            return result;
        }

        [WebMethod]
        public static ResponseJson ActualizarDireccionEntrega(DireccionEntrega req)
        {
            var result = new ResponseJson();
            if (!OpenSession())
            {
                result.Mensaje = "connClose";
                return result;
            }

            try
            {
                var row = DtTableDireccionEntrega.Select("Id_CteDirEntrega='" + req.Id + "'")[0];
                row["Cte_Calle"] = req.Calle;
                row["Cte_Numero"] = req.Numero;
                row["Cte_Cp"] = req.CP;
                row["Cte_Colonia"] = req.Colonia;
                row["Cte_Municipio"] = req.Municipio;
                row["Cte_Estado"] = req.Estado;
                row["Cte_Sector"] = req.Sector;
                row["Cte_Telefono"] = req.Telefono;
                row["Cte_Fax"] = req.Fax;
                row["Cte_HoraAm1"] = req.HoraAm1;
                row["Cte_HoraAm2"] = req.HoraAm2;
                row["Cte_HoraPm1"] = req.HoraPm1;
                row["Cte_HoraPm2"] = req.HoraPm2;
                row["Ruta_EntregaId_Rut"] = req.RutaId;
                row["Ruta_Entrega"] = req.Ruta;
                row["Id_EEstado"] = req.EstadoId;
                row["Id_EPais"] = req.PaisId;
                row["Nom_Pais"] = req.Pais;
                row["Nom_Estado"] = req.Estado;
                row.AcceptChanges();

                result.Estatus = true;
            }
            catch { }
            return result;
        }

        static void ObtenerDireccionEntregas_ClienteSeleccionado(int clienteId)
        {
            try
            {
                var dt = InicializarDtTableDireccionEntrega();
                var clienteDirEntrega = new ClienteDirEntrega
                {
                    Id_Emp = MySesion.Id_Emp,
                    Id_Cd = MySesion.Id_Cd_Ver,
                    Id_Cte = clienteId
                };
                new CN_CatCliente().ConsultaClienteDirEntrega(clienteDirEntrega, MySesion.Emp_Cnx, ref dt, MySesion.VersionTerritorio);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if ((bool)(row["Cte_HoraPm1"]?.ToString()?.EndsWith("p."))) row["Cte_HoraPm1"] = row["Cte_HoraPm1"].ToString() + " m.";
                        if ((bool)(row["Cte_HoraPm2"]?.ToString()?.EndsWith("p."))) row["Cte_HoraPm2"] = row["Cte_HoraPm2"].ToString() + " m.";
                        if ((bool)(row["Cte_HoraAm1"]?.ToString()?.EndsWith("a."))) row["Cte_HoraAm1"] = row["Cte_HoraAm1"].ToString() + " m.";
                        if ((bool)(row["Cte_HoraAm2"]?.ToString()?.EndsWith("a."))) row["Cte_HoraAm2"] = row["Cte_HoraAm2"].ToString() + " m.";

                        string hpm1 = row["Cte_HoraPm1"]?.ToString();
                        if (!string.IsNullOrEmpty(hpm1))
                        {
                            string h = (hpm1.ToLower().Contains("p.m.") || hpm1.ToLower().Contains("a.m.")) ? hpm1.Substring(0, 5) : hpm1;
                            var pm1 = DateTime.Parse(h);
                            if (pm1.Hour < 12) row["Cte_HoraPm1"] = $"{24 + (pm1.Hour - 12)}:{pm1.ToString("mm")} p. m.";
                        }

                        string hpm2 = row["Cte_HoraPm2"]?.ToString();
                        if (!string.IsNullOrEmpty(hpm2))
                        {
                            string h = (hpm2.ToLower().Contains("p.m.") || hpm2.ToLower().Contains("a.m.")) ? hpm2.Substring(0, 5) : hpm2;
                            var pm2 = DateTime.Parse(h);
                            if (pm2.Hour < 12) row["Cte_HoraPm2"] = $"{24 + (pm2.Hour - 12)}:{pm2.ToString("mm")} p. m.";
                        }
                    }
                }
                DtTableDireccionEntrega = dt;
            }
            catch { }
        }

        static DataTable InicializarDtTableDireccionEntrega()
        {
            var dt = new DataTable();
            dt.Columns.Add("Id_Cte", Type.GetType("System.Int32"));
            dt.Columns.Add("Id_CteDirEntrega", Type.GetType("System.Int32"));
            dt.Columns.Add("Cte_Calle", Type.GetType("System.String"));
            dt.Columns.Add("Cte_Numero", Type.GetType("System.String"));
            dt.Columns.Add("Cte_Cp", Type.GetType("System.String"));
            dt.Columns.Add("Cte_Colonia", Type.GetType("System.String"));
            dt.Columns.Add("Cte_Municipio", Type.GetType("System.String"));
            dt.Columns.Add("Cte_Estado", Type.GetType("System.String"));
            dt.Columns.Add("Cte_Sector", Type.GetType("System.String"));
            dt.Columns.Add("Cte_Telefono", Type.GetType("System.String"));
            dt.Columns.Add("Cte_Fax", Type.GetType("System.String"));
            dt.Columns.Add("Cte_HoraAm1", Type.GetType("System.String"));
            dt.Columns.Add("Cte_HoraAm2", Type.GetType("System.String"));
            dt.Columns.Add("Cte_HoraPm1", Type.GetType("System.String"));
            dt.Columns.Add("Cte_HoraPm2", Type.GetType("System.String"));
            dt.Columns.Add("Ruta_Entrega", Type.GetType("System.String"));
            dt.Columns.Add("Ruta_EntregaId_Rut", Type.GetType("System.Int32"));
            dt.Columns.Add("Id_EPais", Type.GetType("System.Int32"));
            dt.Columns.Add("Id_EEstado", Type.GetType("System.Int32"));
            dt.Columns.Add("Nom_Pais", Type.GetType("System.String"));
            dt.Columns.Add("Nom_Estado", Type.GetType("System.String"));
            return dt;
        }

        #endregion






        #region territorios
        public class Territorios
        {
            public int Id { get; set; }
            public bool Activo { get; set; }
            public int IdTerritorio { get; set; }
            public string Territorio { get; set; }
            //public int? IdUen { get; set; }
            //public string Uen { get; set; }
            //public int? IdSegmento { get; set; }
            //public string Segmento { get; set; }
            public string IdRik { get; set; }
            public string Rik { get; set; }
            public int IdTerServ { get; set; }
            public string TerServ { get; set; }
            public int IdRikServ { get; set; }
            public string RikServ { get; set; }
            public int IdTerServTecnico { get; set; }
            public string TerServTecnico { get; set; }
            public int IdRikServTecnico { get; set; }
            public string RikServTecnico { get; set; }
            public string FechaSolicitud { get; set; }
            public string FechaAutorizado { get; set; }
            public string FechaRechazado { get; set; }
            public bool Edit { get; set; }
            public bool TerritorioPadre { get; set; }
        }

        public class TerritorioReq : Territorios
        {
            public int IdCliente { get; set; }
            public string Cliente { get; set; }

        }

        public class RepresentantePorTerritorio
        {
            public int Id { get; set; }
            public string Representante { get; set; }
            public string Mensaje { get; set; }
        }


        [WebMethod]
        public static List<ResponseJson_SelectFormat> ObtenerTerritoriosServicio()
        {
            var result = new List<ResponseJson_SelectFormat>();

            try
            {
                var list = new List<Comun>();
                new CD__Comun().LlenaCombo("spCatTerritorioServ_Combo", MySesion.Emp_Cnx, ref list);

                if (list.Count > 0)
                {
                    result = list.Select(x => new ResponseJson_SelectFormat()
                    {
                        Descripcion = x.Descripcion,
                        Id = x.Id
                    }).ToList();
                }
            }
            catch { }
            return result;
        }

        [WebMethod]
        public static List<ResponseJson_SelectFormat> ObtenerTerritorios()
        {
            var result = new List<ResponseJson_SelectFormat>();
            try
            {
                var list = new List<Comun>();
                new CD__Comun().LlenaCombo(MySesion.Id_Emp, MySesion.Id_Cd_Ver, "spCatTerritorio_Combo", MySesion.Emp_Cnx, ref list);

                if (list.Count > 0)
                {
                    result = list.Select(x => new ResponseJson_SelectFormat()
                    {
                        Descripcion = x.Descripcion,
                        Id = x.Id
                    }).ToList();
                }
            }
            catch { }
            return result;
        }

        //[WebMethod]
        //public static List<ResponseJson_SelectFormat> ObtenerTipoGarantia()
        //{
        //    var result = new List<ResponseJson_SelectFormat>();

        //    try
        //    {
        //        var list = new CN_TipoGarantia(MySesion).ObtenerTodas();
        //        result = list.Select(x => new ResponseJson_SelectFormat()
        //        {
        //            Descripcion = x.TG_Nombre,
        //            Id = x.Id_TG
        //        }).ToList();
        //    }
        //    catch { }
        //    return result;
        //}

        [WebMethod]
        public static ResponseJsonTerritorios AgregarTerritorio(TerritorioReq req)
        {
            var result = new ResponseJsonTerritorios();
            if (!OpenSession())
            {
                result.Mensaje = "connClose";
                return result;
            }

            try
            {
                if (!ChecarTerritoriosPendientes(req.IdCliente))
                {
                    result.Mensaje = "El cliente tiene solicitudes de territorios pendientes de autorizar o rechazar hasta no resolver esto no puede volver hacer un cambio en la configuración de este cliente en territorios.";
                    return result;
                }

                string fechaSolicitud = null;
                int respuesta = 0;
                new CN_CatCliente().ValidaSucursal(MySesion, Emp_CnxCob, ref respuesta);

                if (MySesion.Id_Cd_Ver == respuesta && req.IdTerritorio.ToString().Substring(0, 1) != "6")
                {
                    fechaSolicitud = DateTime.Now.ToShortDateString();

                    int id_CteDet = DtTablaTerritorios_ViewModel.Rows.Count;
                    var row = DtTablaTerritorios_ViewModel.Rows.Add(
                        id_CteDet,
                        req.IdTerritorio,
                        req.Territorio,
                        //req.IdUen,
                        //req.Uen,
                        //req.IdSegmento,
                        //req.Segmento,
                        req.IdRik,
                        req.Rik,
                        req.IdTerServ,
                        req.TerServ,
                        req.IdRikServ,
                        req.RikServ,
                        req.IdTerServTecnico,
                        req.TerServTecnico,
                        req.IdRikServTecnico,
                        req.RikServTecnico,
                        fechaSolicitud,
                        null,
                        null,
                        req.Activo,
                        req.TerritorioPadre,
                        true,
                        false,
                        false
                    );


                    DataSet.CatClienteDet.AddCatClienteDetRow(id_CteDet.ToString(), "0", "0");
                    DataSet.CatClienteDet.AcceptChanges();

                    CargarTerritoriosAutorizacionAnt(null, "", true);
                    CargarTerritoriosAutorizados(req, true, true, 1);
                    result.Estatus = true;
                    result.EnviarComentarios = EnviarComentarios(req.IdTerritorio);
                }
                else
                {
                    int id_CteDet = DtTablaTerritorios_ViewModel.Rows.Count;
                    var row = DtTablaTerritorios_ViewModel.Rows.Add(
                        id_CteDet,
                        req.IdTerritorio,
                        req.Territorio,
                        //req.IdUen,
                        //req.Uen,
                        //req.IdSegmento,
                        //req.Segmento,
                        req.IdRik,
                        req.Rik,
                        req.IdTerServ,
                        req.TerServ,
                        req.IdRikServ,
                        req.RikServ,
                        req.IdTerServTecnico,
                        req.TerServTecnico,
                        req.IdRikServTecnico,
                        req.RikServTecnico,
                        fechaSolicitud,
                        null,
                        null,
                        req.Activo,
                        req.TerritorioPadre,
                        true,
                        false,
                        false
                    );


                    DataSet.CatClienteDet.AddCatClienteDetRow(id_CteDet.ToString(), "0", "0");
                    DataSet.CatClienteDet.AcceptChanges();

                    result.Estatus = true;
                    result.EnviarComentarios = false;
                }
            }
            catch (Exception ex)
            {
                result.Mensaje = ex.Message;
            }

            return result;
        }

        [WebMethod]
        public static RepresentantePorTerritorio ObtenerRepresentantePorTerritorio(int idTerritorio)
        {
            var result = new RepresentantePorTerritorio();
            if (!OpenSession())
            {
                result.Mensaje = "connClose";
                return result;
            }

            try
            {
                var terServ = new CapaEntidad.Territorios
                {
                    Id_Emp = MySesion.Id_Emp,
                    Id_Cd = MySesion.Id_Cd_Ver,
                    Id_Ter = idTerritorio
                };
                Representantes rik = null;
                new CN_CatRepresentantes().ConsultarRepresentantePorTerritorio(terServ, MySesion.Emp_Cnx, ref rik);
                if (rik != null)
                {
                    result.Id = rik.Id_Rik;
                    result.Representante = rik.Nombre;
                }
            }
            catch { }
            return result;
        }

        [WebMethod]
        public static List<Territorios> ObtenerDtTableTerritorios()
        {
            var result = new List<Territorios>();
            try
            {
                result = DtTablaTerritorios_ViewModel.Rows.OfType<DataRow>()
                    .Where(x => !x.Field<bool>("Eliminado"))
                    .Select(x => new Territorios()
                    {
                        Id = x.Field<int>("Id_CteDet"),
                        Activo = x.Field<bool>("Cte_Activo"),
                        IdTerritorio = x.Field<int>("Id_Ter"),
                        Territorio = x.Field<string>("Ter_Nombre"),
                        IdRik = x.Field<string>("Id_Rik"),
                        Rik = x.Field<string>("Rik"),
                        IdTerServ = x.Field<int>("Id_TerServ"),
                        TerServ = x.Field<string>("TerServ"),
                        IdRikServ = x.Field<int>("Id_RIKServ"),
                        RikServ = x.Field<string>("RikServ"),
                        IdTerServTecnico = x.Field<int>("Id_TerServTecnico"),
                        TerServTecnico = x.Field<string>("TerServTecnico"),
                        IdRikServTecnico = x.Field<int>("Id_RIKServTecnico"),
                        RikServTecnico = x.Field<string>("RikServTecnico"),
                        FechaSolicitud = x.Field<string>("Fec_Solicitud"),
                        FechaAutorizado = x.Field<string>("Fec_Autorizado"),
                        FechaRechazado = x.Field<string>("Fec_Rechazado"),
                        Edit = false,
                        TerritorioPadre = x.Field<bool>("TerritorioPadre")
                    }).ToList();
            }
            catch { }
            return result;
        }

        [WebMethod]
        public static ResponseJson ActualizarTerritorio(TerritorioReq req)
        {
            var result = new ResponseJsonTerritorios();
            if (!OpenSession())
            {
                result.Mensaje = "connClose";
                return result;
            }

            try
            {
                if (!ChecarTerritoriosPendientes(req.IdCliente))
                {
                    result.Mensaje = "El cliente tiene solicitudes de territorios pendientes de autorizar o rechazar hasta no resolver esto no puede volver hacer un cambio en la configuración de este cliente en territorios.";
                    return result;
                }
                else
                {
                    int respuesta = 0;
                    new CN_CatCliente().ValidaSucursal(MySesion, Emp_CnxCob, ref respuesta);

                    if (MySesion.Id_Cd_Ver == respuesta && req.IdTerritorio.ToString().Substring(0, 1) != "6")
                    {
                        CargarTerritoriosAutorizacionAnt(req, "edit", false);
                        CargarTerritoriosAutorizados(req, true, false, 1);

                        var row = DtTablaTerritorios_ViewModel.Select("Id_CteDet='" + req.Id + "'")[0];
                        row["Id_Ter"] = req.IdTerritorio;
                        row["Ter_Nombre"] = req.Territorio;
                        row["Id_Rik"] = req.IdRik;
                        row["rik"] = req.Rik;
                        row["Id_TerServ"] = req.IdTerServ;
                        row["TerServ"] = req.TerServ;
                        row["Id_RIKServ"] = req.IdRikServ;
                        row["RikServ"] = req.RikServ;
                        row["Id_TerServTecnico"] = req.IdTerServTecnico;
                        row["TerServTecnico"] = req.TerServTecnico;
                        row["Id_RIKServTecnico"] = req.IdRikServTecnico;
                        row["RikServTecnico"] = req.RikServTecnico;
                        row.AcceptChanges();

                        result.Estatus = true;
                        result.EnviarComentarios = true;
                    }
                    else
                    {
                        var row = DtTablaTerritorios_ViewModel.Select("Id_CteDet='" + req.Id + "'")[0];
                        int id_CteDet = DtTablaTerritorios_ViewModel.Rows.Count;
                        string fechaSolicitud;

                        //if ((bool)row["TerritorioPadre"] == true && req.IdTerritorio != (int)row["Id_Ter"])
                        if (req.IdTerritorio != (int)row["Id_Ter"])
                        {
                            fechaSolicitud = DateTime.Now.ToShortDateString();

                            TerritorioReq entTernuevo = new TerritorioReq();
                            entTernuevo.Id = id_CteDet;
                            entTernuevo.Activo = false;
                            entTernuevo.IdTerritorio = (int)row["Id_Ter"];
                            entTernuevo.Territorio = row["Ter_Nombre"].ToString();
                            entTernuevo.IdRik = row["Id_Rik"].ToString();
                            entTernuevo.Rik = row["Rik"].ToString();
                            entTernuevo.IdTerServ = (int)row["Id_TerServ"];
                            entTernuevo.TerServ = row["TerServ"].ToString();
                            entTernuevo.IdRikServ = (int)row["Id_RIKServ"];
                            entTernuevo.RikServ = row["RikServ"].ToString();
                            entTernuevo.IdTerServTecnico = row["Id_TerServTecnico"] != DBNull.Value ? (int)row["Id_TerServTecnico"] : -1;
                            entTernuevo.TerServTecnico = row["TerServTecnico"]?.ToString() ?? "";
                            entTernuevo.IdRikServTecnico = row["Id_RIKServTecnico"] != DBNull.Value ? (int)row["Id_RIKServTecnico"] : -1;
                            entTernuevo.RikServTecnico = row["RikServTecnico"]?.ToString() ?? "";
                            entTernuevo.FechaSolicitud = row["Fec_Solicitud"].ToString();
                            entTernuevo.FechaAutorizado = row["Fec_Autorizado"].ToString();
                            entTernuevo.FechaRechazado = row["Fec_Rechazado"].ToString();
                            entTernuevo.Edit = false;
                            entTernuevo.TerritorioPadre = (bool)row["TerritorioPadre"];
                            var objAdd = AgregarTerritorio(entTernuevo);

                        }

                        if (req.IdTerServ != (int)row["Id_TerServ"] ||
                           req.IdTerritorio != (int)row["Id_Ter"] ||
                           req.IdTerServTecnico != (row["Id_TerServTecnico"] != DBNull.Value ? (int)row["Id_TerServTecnico"] : -1))
                        {
                            row["Id_Ter"] = req.IdTerritorio;
                            row["Ter_Nombre"] = req.Territorio;
                            row["Id_Rik"] = req.IdRik;
                            row["rik"] = req.Rik;
                            row["Id_TerServ"] = req.IdTerServ;
                            row["TerServ"] = req.TerServ;
                            row["Id_RIKServ"] = req.IdRikServ;
                            row["RikServ"] = req.RikServ;
                            row["Id_TerServTecnico"] = req.IdTerServTecnico;
                            row["TerServTecnico"] = req.TerServTecnico;
                            row["Id_RIKServTecnico"] = req.IdRikServTecnico;
                            row["RikServTecnico"] = req.RikServTecnico;
                            row["Actualizado"] = true;
                            row.AcceptChanges();
                        }

                        result.Estatus = true;
                        result.EnviarComentarios = false;
                        return result;
                    }
                }
            }
            catch { }
            return result;
        }

        [WebMethod]
        public static ResponseJson EliminarTerritorio(int idCliente, int idTerritorio, bool nuevoClienteYTerritorioPadre)
        {
            var result = new ResponseJson();
            if (!OpenSession())
            {
                result.Mensaje = "connClose";
                return result;
            }

            try
            {
                if (nuevoClienteYTerritorioPadre)
                {
                    /* si es cliente nuevo y eliminar el territorio padre no hay
                     * nececidad de validar si puede eliminarlo, ya que aún no hay registros en la bd
                     */
                    DtTablaTerritorios.Rows.Clear();
                    DtTablaTerritorios.AcceptChanges();
                    DtTablaTerritoriosAnt.Rows.Clear();
                    DtTablaTerritoriosAnt.AcceptChanges();
                    DtTablaTerritorios_ViewModel.Rows.Clear();
                    DtTablaTerritorios_ViewModel.AcceptChanges();
                    result.Estatus = true;
                }
                else
                {
                    if (PermisosParaEliminar(idCliente, idTerritorio))
                    {
                        var row = DtTablaTerritorios_ViewModel.Select("Id_Ter='" + idTerritorio + "'");
                        row[0]["Eliminado"] = true;
                        DtTablaTerritorios_ViewModel.AcceptChanges();

                        result.Estatus = true;
                    }
                    else { result.Mensaje = "El territorio no puede ser eliminado debido a que actualmente está en uso por otros sistemas"; }
                }
            }
            catch { }
            return result;
        }

        [WebMethod]
        public static ResponseJson AgregarComentarioTerritorio()
        {
            string comentario = HttpContext.Current.Session["Comentarios" + HttpContext.Current.Session.SessionID].ToString();
            string idTer = HttpContext.Current.Session["Territorio" + HttpContext.Current.Session.SessionID].ToString();

            var row = DtTablaTerritorios.Select("Id_Ter='" + idTer + "'")[0];
            row["comentarios"] = comentario;
            row.AcceptChanges();

            return new ResponseJson { Estatus = true };
        }

        [WebMethod]
        public static ResponseJson CambiarStatusActivoTerritorio(TerritorioReq req, bool status)
        {
            var result = new ResponseJsonTerritorios();
            if (!OpenSession())
            {
                result.Mensaje = "connClose";
                return result;
            }

            try
            {
                var row = DtTablaTerritorios_ViewModel.Select("Id_Ter='" + req.IdTerritorio + "'")[0];
                row["Fec_Solicitud"] = DateTime.Now.ToShortDateString();
                row["Fec_Autorizado"] = DBNull.Value;
                row["Fec_Rechazado"] = DBNull.Value;
                row["Cte_Activo"] = status;
                row["Actualizado"] = true;
                row.AcceptChanges();

                int respuesta = 0;
                new CN_CatCliente().ValidaSucursal(MySesion, Emp_CnxCob, ref respuesta);
                if (req.IdTerritorio.ToString().Substring(0, 1) != "6" && MySesion.Id_Cd_Ver == respuesta)
                {
                    CargarTerritoriosAutorizacionAnt(req, "activo", status);
                    CargarTerritoriosAutorizados(req, status, false, 1);
                    result.Estatus = true;
                    result.EnviarComentarios = true;
                }
                else
                {
                    result.Estatus = true;
                    result.EnviarComentarios = false;
                }

                int id_CteDet = (int)row["Id_CteDet"];
                if (status)
                {
                    lstTerActivo.Add(id_CteDet);
                    if (lstTerInactivo.Contains(id_CteDet))
                        lstTerInactivo.Remove(id_CteDet);
                }
                else
                {
                    lstTerInactivo.Add(id_CteDet);
                    if (lstTerActivo.Contains(id_CteDet))
                        lstTerActivo.Remove(id_CteDet);
                }

            }
            catch { }
            return result;
        }

        static void ObtenerTerritorios_ClienteSeleccionado(int clienteId)
        {
            try
            {
                var cc = new CN_CatCliente();
                var dt = InicializarTablaTerritorios_ViewModel();
                var clientedet = new ClienteDet
                {
                    Id_Emp = MySesion.Id_Emp,
                    Id_Cd = MySesion.Id_Cd_Ver,
                    Id_Cte = clienteId
                };
                cc.ConsultaClienteDet(clientedet, MySesion.Emp_Cnx, ref dt, MySesion.VersionTerritorio);

                if (dt != null && dt.Rows.Count > 0 && !dt.AsEnumerable().Any(x => x.Field<bool>("TerritorioPadre")))
                {
                    //no se han asignado territorio padre
                    DefaultTerritorioPadre(clienteId);
                    dt = InicializarTablaTerritorios_ViewModel();
                    cc.ConsultaClienteDet(clientedet, MySesion.Emp_Cnx, ref dt, MySesion.VersionTerritorio);
                }

                DtTablaTerritorios_ViewModel = dt;
                foreach (DataRow row in dt.Rows) DataSet.CatClienteDet.AddCatClienteDetRow(row["Id_CteDet"].ToString(), "0", "0");
            }
            catch { }
        }

        static DataTable InicializarTablaTerritorios_ViewModel()
        {
            var dt = new DataTable();
            dt.Columns.Add("Id_CteDet", Type.GetType("System.Int32"));
            dt.Columns.Add("Id_Ter", Type.GetType("System.Int32"));
            dt.Columns.Add("Ter_Nombre", Type.GetType("System.String"));
            dt.Columns.Add("Id_Rik", Type.GetType("System.String"));
            dt.Columns.Add("Rik", Type.GetType("System.String"));
            dt.Columns.Add("Id_TerServ", Type.GetType("System.Int32"));
            dt.Columns.Add("TerServ", Type.GetType("System.String"));
            dt.Columns.Add("Id_RIKServ", Type.GetType("System.Int32"));
            dt.Columns.Add("RikServ", Type.GetType("System.String"));
            dt.Columns.Add("Id_TerServTecnico", Type.GetType("System.Int32"));
            dt.Columns.Add("TerServTecnico", Type.GetType("System.String"));
            dt.Columns.Add("Id_RIKServTecnico", Type.GetType("System.Int32"));
            dt.Columns.Add("RikServTecnico", Type.GetType("System.String"));
            dt.Columns.Add("Fec_Solicitud", Type.GetType("System.String"));
            dt.Columns.Add("Fec_Autorizado", Type.GetType("System.String"));
            dt.Columns.Add("Fec_Rechazado", Type.GetType("System.String"));
            dt.Columns.Add("Cte_Activo", Type.GetType("System.Boolean"));
            dt.Columns.Add("TerritorioPadre", Type.GetType("System.Boolean"));
            dt.Columns.Add("Nuevo", Type.GetType("System.Boolean")); //identifica si el territorio en la tabla ya existe en la bd
            dt.Columns.Add("Eliminado", Type.GetType("System.Boolean"));
            dt.Columns.Add("Actualizado", Type.GetType("System.Boolean"));

            return dt;
        }

        static DataTable InicializarTablaTerritorios(bool territorio)
        {
            var dt = new DataTable();
            dt.Columns.Add("Id_Emp");
            dt.Columns.Add("Id_Cd");
            dt.Columns.Add("Id_Cte");
            dt.Columns.Add("Nom_Cliente");
            dt.Columns.Add("Id_Ter");
            dt.Columns.Add("Nom_Territorio");
            dt.Columns.Add("Dimension");
            dt.Columns.Add("Pesos");
            dt.Columns.Add("Potencial");
            dt.Columns.Add("manodeobra");
            dt.Columns.Add("gastosTerritorio");
            dt.Columns.Add("fletespagadosporcliente");
            dt.Columns.Add("porcentaje");
            dt.Columns.Add("activo");
            dt.Columns.Add("nuevo");
            dt.Columns.Add("Id_Solicitud");
            dt.Columns.Add("Id_CteDet");
            dt.Columns.Add("Id_Seg");
            dt.Columns.Add("Id_TerServ");
            if (territorio)
            {
                dt.Columns.Add("Solicita");
                dt.Columns.Add("CorreoSolicitante");
                dt.Columns.Add("comentarios");
                dt.Columns.Add("Impacta");
            }

            return dt;
        }

        static void CargarTerritoriosAutorizacionAnt(TerritorioReq terr, string tipo, bool estatus)
        {
            if (terr == null)
            {
                DtTablaTerritoriosAnt.Rows.Add(
                    MySesion.Id_Emp,
                    MySesion.Id_Cd_Ver,
                    0,
                    "No aplica",
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                );
                return;
            }

            bool activo;
            if (tipo != "edit")
            {
                if (!terr.Activo && tipo == "activo") activo = true;
                else activo = false;
            }
            else activo = terr.Activo;

            DtTablaTerritoriosAnt.Rows.Add(
                MySesion.Id_Emp,
                MySesion.Id_Cd_Ver,
                terr.IdCliente,
                terr.Cliente,
                terr.IdTerritorio,
                terr.Territorio,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                activo,
                false,
                0, //id solicitud
                terr.Id,
                -1, //id segmento
                terr.IdTerServ
            );
        }

        static void CargarTerritoriosAutorizados(TerritorioReq terr, bool activo, bool nuevo, int impacta)
        {
            DtTablaTerritorios.Rows.Add(
                MySesion.Id_Emp,
                MySesion.Id_Cd_Ver,
                terr.IdCliente,
                terr.Cliente,
                terr.IdTerritorio,
                terr.Territorio,
                0, //dimension
                0, //pesos
                0, //potencial
                0, //mano de obra
                0, //gastos territorio
                0, //fletes pagados por el cliente
                0, // porcentaje
                activo,
                nuevo,
                0, //id solicitud
                terr.Id,
                -1, //id segmento
                terr.IdTerServ,
                MySesion.U_Nombre, //solicita
                MySesion.U_Correo, // correo solicitante
                "", //comentario
                impacta
            );
        }

        static bool ChecarTerritoriosPendientes(int idCliente)
        {
            int existe = 0;
            new CN_CatCliente().ConsultarSolicitudesPdtesClienteTerr(MySesion.Id_Emp, MySesion.Id_Cd_Ver, idCliente, Emp_CnxCen, ref existe);
            if (existe == 1)
            {

                return false;
            }
            return true;
        }

        static bool EnviarComentarios(int idTerr)
        {
            bool send = false;
            try
            {
                int respuesta = 0;
                new CN_CatCliente().ValidaSucursal(MySesion, Emp_CnxCob, ref respuesta);
                if (MySesion.Id_Cd_Ver == respuesta)
                {
                    send = true;
                }
            }
            catch { }
            return send;
        }

        static bool PermisosParaEliminar(int idCliente, int idTerr)
        {
            bool permiso = false;
            using (var conn = new SqlConnection(MySesion.Emp_Cnx))
            {
                using (var cmd = new SqlCommand("spPermisosParaEliminarTerritorio", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id_ter", SqlDbType.Int).Value = idTerr;
                    cmd.Parameters.Add("@id_cd", SqlDbType.Int).Value = MySesion.Id_Cd;
                    cmd.Parameters.Add("@id_cte", SqlDbType.Int).Value = idCliente;

                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            permiso = reader.GetInt32(0) == 0;
                        }
                    }
                }
            }

            return permiso;
        }

        static void DefaultTerritorioPadre(int idCliente)
        {
            using (var conn = new SqlConnection(MySesion.Emp_Cnx))
            {
                using (var cmd = new SqlCommand("habilitarTerritorioPadre", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id_cd", SqlDbType.Int).Value = MySesion.Id_Cd;
                    cmd.Parameters.Add("@id_cte", SqlDbType.Int).Value = idCliente;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion






        #region cobranza
        public class Cobranza
        {
            public bool Credito { get; set; }
            public bool PermiteFacturar { get; set; }
            public bool CreditoSuspendido { get; set; }
            public bool EnableCreditoSuspendido { get; set; }
            public bool HabilitarCreditoSuspendido { get; set; }
            public int Moneda { get; set; }
            public double LimiteCredito { get; set; }
            public int CondicionPagoDias { get; set; }
            public string CondicionPagoAutorizo { get; set; }
            public string CorreoEdoCuenta1 { get; set; }
            public string CorreoEdoCuenta2 { get; set; }
            public string CorreoEdoCuenta3 { get; set; }
            public string RHoraAm1 { get; set; }
            public string RHoraAm2 { get; set; }
            public string RHoraPm1 { get; set; }
            public string RHoraPm2 { get; set; }
            public int SemanaRevision1 { get; set; }
            public int SemanaRevision2 { get; set; }
            public bool RLunes { get; set; }
            public bool RMartes { get; set; }
            public bool RMiercoles { get; set; }
            public bool RJueves { get; set; }
            public bool RViernes { get; set; }
            public bool RSabado { get; set; }
            public bool RDomingo { get; set; }
            public string PHoraAm1 { get; set; }
            public string PHoraAm2 { get; set; }
            public string PHoraPm1 { get; set; }
            public string PHoraPm2 { get; set; }
            public int SemanaPago { get; set; }
            public bool PLunes { get; set; }
            public bool PMartes { get; set; }
            public bool PMiercoles { get; set; }
            public bool PJueves { get; set; }
            public bool PViernes { get; set; }
            public bool PSabado { get; set; }
            public bool PDomingo { get; set; }
            public int SemanaRecepcion { get; set; }
            public bool RecLunes { get; set; }
            public bool RecMartes { get; set; }
            public bool RecMiercoles { get; set; }
            public bool RecJueves { get; set; }
            public bool RecViernes { get; set; }
            public bool RecSabado { get; set; }
            public bool RecDomingo { get; set; }
            public string TelefonoCobranza1 { get; set; }
            public string TelefonoCobranza2 { get; set; }
            public string Documento { get; set; }
            public bool DesgloseIva { get; set; }
            public bool RetencionIva { get; set; }
            public double? PorcientoRetencion { get; set; }
            public bool IvaCliente { get; set; }
            public int? PorcentajeIvaCliente { get; set; }
            public int Banco { get; set; }
            public string NumeroCuenta { get; set; }
            public string ReferenciaTecleada { get; set; }
            public string Portal { get; set; }
            public string UDigitos { get; set; }
            public string UsoCFDI { get; set; }
            public string MetodoPago { get; set; }
            public string PagoUsoCFDI { get; set; }
            public string PagoMetodoPago { get; set; }
            public string PagoNumeroCuenta { get; set; }
            public int PagoBanco { get; set; }
            public string PagoCorreos { get; set; }
            public string NCUsoCFDI { get; set; }
            public string NCFormaPago { get; set; }
            public string NCMetodoPago { get; set; }
            public int[] FormasDePago { get; set; }
            public bool OrdenCompra { get; set; }
            public bool Comisiones { get; set; }
            public bool NotaCreditoFacturar { get; set; }
            public double? PorcentajeNotaCreditoFacturar { get; set; }
            public decimal VersionCFDI { get; set; }
            public int RemisionElectronica { get; set; }
            public int Serie { get; set; }
            public int SerieNC { get; set; }
            public int SerieNCargo { get; set; }
            public int Adenda { get; set; }
            public string RevPago { get; set; }
        }

        [WebMethod]
        public static List<ResponseJson_SelectFormat> ObtenerFormasDePago()
        {
            var result = new List<ResponseJson_SelectFormat>();

            try
            {
                var list = new List<Comun>();
                new CD__Comun().LlenaCombo(MySesion.Id_Emp, -1, "spCatFormaPago_Combo", MySesion.Emp_Cnx, ref list);
                result = list.Select(x => new ResponseJson_SelectFormat()
                {
                    Descripcion = x.Descripcion,
                    Id = x.Id
                }).ToList();
            }
            catch { }
            return result;
        }

        [WebMethod]
        public static List<ResponseJson_SelectFormat> ObtenerTMoneda()
        {
            var result = new List<ResponseJson_SelectFormat>();

            try
            {
                var list = new List<Comun>();
                new CD__Comun().LlenaCombo(1, MySesion.Id_Emp, "spCatTmoneda_Combo", MySesion.Emp_Cnx, ref list);
                result = list.Select(x => new ResponseJson_SelectFormat()
                {
                    Descripcion = x.Descripcion,
                    Id = x.Id
                }).ToList();
            }
            catch { }
            return result;
        }

        [WebMethod]
        public static List<ResponseJson_SelectFormat> ObtenerVersionCFDI()
        {
            var result = new List<ResponseJson_SelectFormat>();

            try
            {
                var list = new List<Comun>();
                new CD__Comun().LlenaCombo("Sp_VersionCFDI_Combo", MySesion.Emp_Cnx, ref list);
                result = list.Select(x => new ResponseJson_SelectFormat()
                {
                    Descripcion = x.Descripcion,
                    Id = x.Id
                }).ToList();
            }
            catch { }
            return result;
        }

        [WebMethod]
        public static List<ResponseJson_SelectFormat> ObtenerBancos()
        {
            var result = new List<ResponseJson_SelectFormat>();

            try
            {
                var list = new List<Comun>();
                new CD__Comun().LlenaCombo(1, MySesion.Id_Emp, MySesion.Id_Cd_Ver, "spCatBanco_Combo", MySesion.Emp_Cnx, ref list);
                result = list.Select(x => new ResponseJson_SelectFormat()
                {
                    Descripcion = x.Descripcion,
                    Id = x.Id
                }).ToList();
            }
            catch { }
            return result;
        }

        [WebMethod]
        public static List<ResponseJson_SelectFormat> ObtenerMetodosPago()
        {
            return new List<ResponseJson_SelectFormat>()
            {
                new ResponseJson_SelectFormat
                {
                    StringId = "-1",
                    Descripcion = "-- Seleccionar --"
                },
                new ResponseJson_SelectFormat
                {
                    StringId = "PUE",
                    Descripcion = "Pago en una sola exhibición"
                },
                new ResponseJson_SelectFormat
                {
                    StringId = "PPD",
                    Descripcion = "Pago en parcialidades o diferido"
                }
            };
        }

        [WebMethod]
        public static List<ResponseJson_SelectFormat> ObtenerAdenda()
        {
            var result = new List<ResponseJson_SelectFormat>();

            try
            {
                var list = new List<Comun>();
                new CD__Comun().LlenaCombo(1, MySesion.Id_Emp, MySesion.Id_Cd_Ver, "spCatAdenda_Combo", MySesion.Emp_Cnx, ref list);
                result = list.Select(x => new ResponseJson_SelectFormat()
                {
                    Descripcion = x.Descripcion,
                    Id = x.Id
                }).ToList();
            }
            catch { }
            return result;
        }

        [WebMethod]
        public static List<ResponseJson_SelectFormat> ObtenerSerieNotaCargo()
        {
            var result = new List<ResponseJson_SelectFormat>();

            try
            {
                var list = new List<Comun>();
                new CD__Comun().LlenaCombo(1, MySesion.Id_Emp, MySesion.Id_Cd_Ver, 2, "spCatConsFactEle_Combo", MySesion.Emp_Cnx, ref list);
                result = list.Select(x => new ResponseJson_SelectFormat()
                {
                    Descripcion = x.Descripcion,
                    Id = x.Id
                }).ToList();
            }
            catch { }
            return result;
        }

        [WebMethod]
        public static List<ResponseJson_SelectFormat> ObtenerSerieNotaCredito()
        {
            var result = new List<ResponseJson_SelectFormat>();

            try
            {
                var list = new List<Comun>();
                new CD__Comun().LlenaCombo(1, MySesion.Id_Emp, MySesion.Id_Cd_Ver, 3, "spCatConsFactEle_Combo", MySesion.Emp_Cnx, ref list);
                result = list.Select(x => new ResponseJson_SelectFormat()
                {
                    Descripcion = x.Descripcion,
                    Id = x.Id
                }).ToList();
            }
            catch { }
            return result;
        }

        [WebMethod]
        public static List<ResponseJson_SelectFormat> ObtenerSerieConsecutivo()
        {
            var result = new List<ResponseJson_SelectFormat>();

            try
            {
                var list = new List<Comun>();
                new CD__Comun().LlenaCombo(1, MySesion.Id_Emp, MySesion.Id_Cd_Ver, 1, "spCatConsFactEle_Combo", MySesion.Emp_Cnx, ref list);
                result = list.Select(x => new ResponseJson_SelectFormat()
                {
                    Descripcion = x.Descripcion,
                    Id = x.Id
                }).ToList();
            }
            catch { }
            return result;
        }

        [WebMethod]
        public static List<ResponseJson_SelectFormat> ObtenerRemisionesElectronicas()
        {
            var result = new List<ResponseJson_SelectFormat>();

            try
            {
                var list = new List<Comun>();
                new CD__Comun().LlenaCombo("Sp_CatCuentaContableNacional_Combo", MySesion.Emp_Cnx, ref list);
                result = list.Select(x => new ResponseJson_SelectFormat()
                {
                    Descripcion = x.Descripcion,
                    Id = x.Id
                }).ToList();
            }
            catch { }
            return result;
        }

        [WebMethod]
        public static List<ResponseJson_SelectFormat> ObtenerDiasCondicionesDePago()
        {
            var result = new List<ResponseJson_SelectFormat>();

            try
            {
                var list = new List<Comun>();
                new CD__Comun().LlenaCombo(1, MySesion.Id_Emp, MySesion.Id_Cd_Ver, 1, "spCatDias_Combo", MySesion.Emp_Cnx, ref list);
                result = list.Select(x => new ResponseJson_SelectFormat()
                {
                    Descripcion = x.Descripcion,
                    Id = x.Id
                }).ToList();
            }
            catch { }
            return result;
        }
        #endregion





        #region bennets

        public class CatalogoAdicionalBennets
        {
            public string LabelText { get; set; }
            public int IdCatGenerico { get; set; }
            public List<ResponseJson_SelectFormat> CatGenericoDetalle { get; set; }
        }

        public class CatalogoAdicional
        {
            public string Sucursal { get; set; }
            public int IdCatGenerico { get; set; }
            public List<CatalogoAdicionalOptions> Cat { get; set; }
            public class CatalogoAdicionalOptions
            {
                public int IdCatGenerico { get; set; }
                public int IdCatGenericoDetalle { get; set; }
            }
        }

        [WebMethod]
        public static List<CatalogoAdicionalBennets> ObtenerCamposDeUsuario_Bennets()
        {
            var lstCampos = new List<Comun>();
            var lstCatalogo = new List<Comun>();
            var lstCatalogoDetalle = new List<Comun>();
            var result = new List<CatalogoAdicionalBennets>();

            new CN_CatCliente().ConsultarCatalogoDinamico(MySesion.Id_Emp, MySesion.Id_Cd, MySesion.Emp_Cnx, ref lstCatalogo, ref lstCatalogoDetalle, ref lstCampos);

            if (lstCatalogo != null && lstCatalogo.Count > 0 && lstCatalogoDetalle != null && lstCatalogoDetalle.Count > 0 && lstCampos != null && lstCampos.Count > 0)
            {
                result.Add(new CatalogoAdicionalBennets
                {
                    LabelText = lstCampos[0].Descripcion,
                    IdCatGenerico = lstCampos[0].Id,
                });

                for (int i = 0; i < lstCatalogo.Count(); i++)
                {
                    result.Add(new CatalogoAdicionalBennets
                    {
                        LabelText = lstCatalogo[i].Descripcion,
                        IdCatGenerico = lstCatalogo[i].Id,
                        CatGenericoDetalle = new List<ResponseJson_SelectFormat>(
                            lstCatalogoDetalle
                            .Where(x => x.Id == lstCatalogo[i].Id)
                            .Select(s => new ResponseJson_SelectFormat
                            {
                                Descripcion = s.Descripcion,
                                Id = s.ValorInt
                            })
                            .ToList()
                        )
                    });
                }
            }
            return result;
        }

        static CatalogoAdicional ConsultarSeleccionCatalogoAdicional(int idCliente)
        {
            var result = new CatalogoAdicional();
            var lstCatalogoSeleccion = new List<Comun>();

            new CN_CatCliente().ConsultarSeleccionCatalogoAdicional(MySesion.Emp_Cnx, MySesion.Id_Cd, idCliente, ref lstCatalogoSeleccion);
            if (lstCatalogoSeleccion != null && lstCatalogoSeleccion.Count > 0)
            {
                result.IdCatGenerico = lstCatalogoSeleccion[0].Id;
                result.Sucursal = lstCatalogoSeleccion[0].ValorStr;
                result.Cat = new List<CatalogoAdicional.CatalogoAdicionalOptions>
                {
                    new CatalogoAdicional.CatalogoAdicionalOptions
                    {
                         IdCatGenerico = lstCatalogoSeleccion[1].Id,
                         IdCatGenericoDetalle = lstCatalogoSeleccion[1].ValorInt
                    },
                    new CatalogoAdicional.CatalogoAdicionalOptions
                    {
                         IdCatGenerico = lstCatalogoSeleccion[2].Id,
                         IdCatGenericoDetalle = lstCatalogoSeleccion[2].ValorInt
                    },
                    new CatalogoAdicional.CatalogoAdicionalOptions
                    {
                         IdCatGenerico = lstCatalogoSeleccion[3].Id,
                         IdCatGenericoDetalle = lstCatalogoSeleccion[3].ValorInt
                    },
                    new CatalogoAdicional.CatalogoAdicionalOptions
                    {
                         IdCatGenerico = lstCatalogoSeleccion[4].Id,
                         IdCatGenericoDetalle = lstCatalogoSeleccion[4].ValorInt
                    },
                    new CatalogoAdicional.CatalogoAdicionalOptions
                    {
                         IdCatGenerico = lstCatalogoSeleccion[5].Id,
                         IdCatGenericoDetalle = lstCatalogoSeleccion[5].ValorInt
                    }
                };
            }
            return result;
        }

        static void GuardarSeleccionCatalogoAdicional(int idCliente, CatalogoAdicional catalogo)
        {
            int[] lstParametro = new int[13];

            lstParametro[0] = idCliente;
            lstParametro[1] = catalogo.Cat[0].IdCatGenerico;
            lstParametro[2] = catalogo.Cat[0].IdCatGenericoDetalle;
            lstParametro[3] = catalogo.Cat[1].IdCatGenerico;
            lstParametro[4] = catalogo.Cat[1].IdCatGenericoDetalle;
            lstParametro[5] = catalogo.Cat[2].IdCatGenerico;
            lstParametro[6] = catalogo.Cat[2].IdCatGenericoDetalle;
            lstParametro[7] = catalogo.Cat[3].IdCatGenerico;
            lstParametro[8] = catalogo.Cat[3].IdCatGenericoDetalle;
            lstParametro[9] = catalogo.Cat[4].IdCatGenerico;
            lstParametro[10] = catalogo.Cat[4].IdCatGenericoDetalle;

            new CN_CatCliente().GuardarSeleccionCatalogoAdicional(MySesion.Emp_Cnx, MySesion.Id_Cd, lstParametro, catalogo.IdCatGenerico, catalogo.Sucursal);
        }
        #endregion




        #region guardar cliente
        public class ClienteModel
        {
            public bool Nuevo { get; set; }
            public int IdCliente { get; set; }
            public DatosGenerales DatosGenerales { get; set; }
            public Cobranza Cobranza { get; set; }
            public CatalogoAdicional CatalogoAdicionalBennets { get; set; }
        }

        [WebMethod]
        public static ResponseJson GuardarCliente(ClienteModel model)
        {
            var result = new ResponseJson();
            if (!OpenSession())
            {
                result.Mensaje = "connClose";
                return result;
            }

            int TotalTerritorioPadre = DtTablaTerritorios_ViewModel.AsEnumerable().Where(x => (x.Field<bool>("TerritorioPadre") == true) && (x.Field<bool>("Cte_Activo") == true)).Count();

            if (TotalTerritorioPadre > 1)
            {
                result.Mensaje = "Seleccione solo un territorio padre";
                return result;
            }

            if (model.Nuevo && !PermisoGuardar)
            {
                result.Mensaje = "No tiene permisos para guardar";
                return result;
            }
            else if (!PermisoModificar)
            {
                result.Mensaje = "No tiene permisos para modificar";
                return result;
            }

            if (HttpContext.Current.Session["autorizoelcambiotipocliente"] == null && HttpContext.Current.Session["autorizo!"] == null)
            {
                if (!ValidarDatos_PreGuardar(model, ref result)) return result;
            }

            var context = HttpContext.Current.Session;
            int autorizaVinculacion = 0;
            if (context["Sesion" + context.SessionID + "autorizacionVinculacion"] != null)
                autorizaVinculacion = int.Parse(context["Sesion" + context.SessionID + "autorizacionVinculacion"].ToString());
            bool cambioTipoCliente = false;
            if (context["Sesion" + context.SessionID + "cambioTipoCliente"] != null)
                cambioTipoCliente = context["Sesion" + context.SessionID + "cambioTipoCliente"].ToString().ToUpper() == "TRUE";

            if ((autorizaVinculacion == 0 && cambioTipoCliente && !model.Nuevo) || (autorizaVinculacion == 0 && model.Nuevo))
            {
                result.Mensaje = "openWindowVinculacionCliente";
                return result;
            }

            try
            {
                var cliente = new Clientes
                {
                    Id_Emp = MySesion.Id_Emp,
                    Id_Cd = MySesion.Id_Cd_Ver,
                    Id_UCd = MySesion.Id_Cd,
                    Id_U = MySesion.Id_U,
                    Id_UMod = MySesion.Id_U,
                    //datos generales
                    Id_Cte = model.IdCliente,
                    Estatus = model.DatosGenerales.Activo,
                    Cte_NomComercial = model.DatosGenerales.RazonSocial,
                    Id_TCte = model.DatosGenerales.TipoCliente,
                    Id_Corp = model.DatosGenerales.CuentaCorporativa,
                    Id_Uen = model.DatosGenerales.Uen,
                    Id_Seg = model.DatosGenerales.Segmento,
                    Cte_NomCorto = model.DatosGenerales.NombreComercial,
                    Cte_Calle = "",
                    Cte_Numero = "",
                    Cte_Cp = "",
                    Cte_Colonia = "",
                    Cte_Municipio = "",
                    Cte_DRfc = "",
                    Cte_Estado = "",
                    Cte_Telefono = "",
                    Cte_Fax = "",
                    Cte_Referencia = "",
                    Cte_Contacto = model.DatosGenerales.Contacto,
                    Cte_Email = model.DatosGenerales.Email,
                    Cte_FacCalle = model.DatosGenerales.Calle,
                    Cte_FacNumero = model.DatosGenerales.Numero,
                    Cte_FacCp = model.DatosGenerales.CP,
                    Cte_FacColonia = model.DatosGenerales.Colonia,
                    Cte_FacMunicipio = model.DatosGenerales.Municipio,
                    Cte_FacEstado = model.DatosGenerales.Estado,
                    Cte_FacTel = model.DatosGenerales.Telefonos,
                    Cte_FacRfc = model.DatosGenerales.RFC,
                    Cte_RegimenFiscal = model.DatosGenerales.RegimentFiscal,
                    Cte_AsignacionPed = model.DatosGenerales.AsignacionDePedido,
                    Id_CtePais = model.DatosGenerales.PaisId,
                    Id_CteEstado = model.DatosGenerales.EstadoId,
                    //cobranza
                    Cte_Credito = model.Cobranza.Credito,
                    Cte_Facturacion = model.Cobranza.PermiteFacturar,
                    Cte_CreditoSuspendido = model.Cobranza.CreditoSuspendido,
                    Id_Mon = model.Cobranza.Moneda,
                    Cte_LimCobr = model.Cobranza.LimiteCredito,
                    Cte_CondPago = model.Cobranza.CondicionPagoDias,
                    UPlazo = model.Cobranza.CondicionPagoAutorizo,
                    Cte_CorreoEdoCuenta1 = model.Cobranza.CorreoEdoCuenta1,
                    Cte_CorreoEdoCuenta2 = model.Cobranza.CorreoEdoCuenta2,
                    Cte_CorreoEdoCuenta3 = model.Cobranza.CorreoEdoCuenta3,

                    Cte_RHoraam1 = model.Cobranza.RHoraAm1,
                    Cte_RHoraam2 = model.Cobranza.RHoraAm2,
                    Cte_RHorapm1 = model.Cobranza.RHoraPm1,
                    Cte_RHorapm2 = model.Cobranza.RHoraPm2,
                    Cte_SemRev = model.Cobranza.SemanaRevision1,
                    Cte_SemRev2 = model.Cobranza.SemanaRevision2,
                    Cte_RLunes = model.Cobranza.RLunes,
                    Cte_RMartes = model.Cobranza.RMartes,
                    Cte_RMiercoles = model.Cobranza.RMiercoles,
                    Cte_RJueves = model.Cobranza.RJueves,
                    Cte_RViernes = model.Cobranza.RViernes,
                    Cte_RSabado = model.Cobranza.RSabado,
                    Cte_RDomingo = model.Cobranza.RDomingo,

                    Cte_PHoraam1 = model.Cobranza.PHoraAm1,
                    Cte_PHoraam2 = model.Cobranza.PHoraAm2,
                    Cte_PHorapm1 = model.Cobranza.PHoraPm1,
                    Cte_PHorapm2 = model.Cobranza.PHoraPm2,
                    Cte_SemCob = model.Cobranza.SemanaPago,
                    Cte_CPLunes = model.Cobranza.PLunes,
                    Cte_CPMartes = model.Cobranza.PMartes,
                    Cte_CPMiercoles = model.Cobranza.PMiercoles,
                    Cte_CPJueves = model.Cobranza.PJueves,
                    Cte_CPViernes = model.Cobranza.PViernes,
                    Cte_CPSabado = model.Cobranza.PSabado,
                    Cte_CPDomingo = model.Cobranza.PDomingo,

                    Cte_SemRec = model.Cobranza.SemanaRecepcion,
                    Cte_RecLunes = model.Cobranza.RecLunes,
                    Cte_RecMartes = model.Cobranza.RecMartes,
                    Cte_RecMiercoles = model.Cobranza.RecMiercoles,
                    Cte_RecJueves = model.Cobranza.RecJueves,
                    Cte_RecViernes = model.Cobranza.RecViernes,
                    Cte_RecSabado = model.Cobranza.RecSabado,
                    Cte_RecDomingo = model.Cobranza.RecDomingo,

                    Cte_TelCobranza1 = model.Cobranza.TelefonoCobranza1,
                    Cte_TelCobranza2 = model.Cobranza.TelefonoCobranza2,
                    Cte_Documentos = model.Cobranza.Documento,
                    Cte_DesgIva = model.Cobranza.DesgloseIva,
                    Cte_RetIva = model.Cobranza.RetencionIva,
                    PorcientoRetencion = model.Cobranza.PorcientoRetencion,
                    BPorcientoIVA = model.Cobranza.IvaCliente,
                    PorcientoIVA = model.Cobranza.PorcentajeIvaCliente,

                    Id_Ban = model.Cobranza.Banco,
                    Cte_NumeroCuenta = model.Cobranza.NumeroCuenta,
                    Cte_ReferenciaTecleada = model.Cobranza.ReferenciaTecleada,
                    Cte_Portal = model.Cobranza.Portal,
                    Cte_UDigitos = model.Cobranza.UDigitos,

                    Cte_UsoCFDI = model.Cobranza.UsoCFDI,
                    Cte_MetodoPago = model.Cobranza.MetodoPago,

                    Cte_PagoUsoCFDI = model.Cobranza.PagoUsoCFDI,
                    Cte_PagoMetodoPago = model.Cobranza.PagoMetodoPago,
                    Cte_PagoIdBan = model.Cobranza.PagoBanco,
                    Cte_PagoNumeroCuenta = model.Cobranza.PagoNumeroCuenta,
                    Cte_PagoCorreos = model.Cobranza.PagoCorreos,

                    Cte_NCUsoCFDI = model.Cobranza.NCUsoCFDI,
                    Cte_NCFormaPago = model.Cobranza.NCFormaPago,
                    Cte_NCMetodoPago = model.Cobranza.NCMetodoPago,
                    Cte_Factoraje = false,
                    Cte_Efectivo = false,
                    Cte_Cheque = false,
                    Cte_Transferencia = false,
                    Cte_ReqOrdenCompra = model.Cobranza.OrdenCompra,
                    Cte_Comisiones = model.Cobranza.Comisiones,
                    BPorcNotaCredito = model.Cobranza.NotaCreditoFacturar,
                    PorcientoNotaCredito = model.Cobranza.PorcentajeNotaCreditoFacturar,
                    Cte_VersionCFDI = model.Cobranza.VersionCFDI,
                    Cte_RemisionElectronica = model.Cobranza.RemisionElectronica,
                    Id_Cfe = model.Cobranza.Serie,
                    Cte_SerieNCre = model.Cobranza.SerieNC,
                    Cte_SerieNCa = model.Cobranza.SerieNCargo,
                    Id_Ade = model.Cobranza.Adenda,
                    Db = (new SqlConnectionStringBuilder(MySesion.Emp_Cnx)).InitialCatalog,
                    Db_Cobranza = (new SqlConnectionStringBuilder(Emp_CnxCob)).InitialCatalog,
                    Cte_AutorizaPlazo_IdCd = null,
                    Cte_AutorizaPlazo_IdU = null,
                    RevPago = model.Cobranza.RevPago
                };

                cliente.Cte_NumCuentaContNacional = ObtenerNumCuentaContNacional(model.Cobranza.RemisionElectronica);

                var formaPagos = new List<FormaPago>();
                if (model.Cobranza.FormasDePago.Length > 0)
                {
                    foreach (var item in model.Cobranza.FormasDePago)
                        formaPagos.Add(new FormaPago { Id_Fpa = item });
                }
                cliente.FormasPago = formaPagos;


                var catCliente = new CN_CatCliente();
                int verificador = 0;

                if (model.Nuevo) catCliente.InsertarClientes(cliente, MySesion.Emp_Cnx, ref verificador, MySesion.VersionTerritorio);
                else catCliente.ModificarClientes(cliente, MySesion.Emp_Cnx, ref verificador, MySesion.VersionTerritorio);

                if (MySesion.Id_Cd == 34120) GuardarSeleccionCatalogoAdicional(model.IdCliente, model.CatalogoAdicionalBennets);

                if (verificador != 1 && model.Nuevo)
                {
                    result.Mensaje = "La clave ya existe";
                    return result;
                }
                else if (verificador != 1 && !model.Nuevo)
                {
                    result.Mensaje = "Ocurrio un error al actualizar los datos del cliente";
                    return result;
                }

                //catCliente.ValidaSucursal(MySesion, Emp_CnxCob, ref respuesta); pendiente validar con rafa

                if (model.Nuevo) catCliente.InsertarCteFormaPago(cliente, MySesion.Emp_Cnx, ref verificador);

                int respuesta = 0;
                new CN_CatCliente().ValidaSucursal(MySesion, Emp_CnxCob, ref respuesta);

                //clientesDet
                var _dtVentasDirectas = DtTablaTerritorios_ViewModel.AsEnumerable()
                            .Where(x => !x.Field<int>("Id_Ter").ToString().StartsWith("6") && x.Field<bool>("Nuevo"));
                var dtVentasDirectas = _dtVentasDirectas.Any() ? _dtVentasDirectas.CopyToDataTable() : DtTablaTerritorios_ViewModel.Clone();

                var _clientesDet = DtTablaTerritorios_ViewModel.AsEnumerable()
                    .Where(x => x.Field<bool>("Nuevo"));
                var clientesDet = _clientesDet.Any() ? _clientesDet.CopyToDataTable() : DtTablaTerritorios_ViewModel.Clone();


                var _territoriosEliminados = DtTablaTerritorios_ViewModel.AsEnumerable()
                        .Where(x => x.Field<bool>("Eliminado"));
                var territoriosEliminados = _territoriosEliminados.Any() ? _territoriosEliminados.CopyToDataTable() : DtTablaTerritorios_ViewModel.Clone();
                if (territoriosEliminados.Rows.Count > 0)
                {
                    foreach (DataRow row in territoriosEliminados.Rows)
                    {
                        EliminarClienteDet(model.IdCliente, int.Parse(row["Id_Ter"].ToString()));
                    }
                }

                var _territoriosActualizados = DtTablaTerritorios_ViewModel.AsEnumerable()
                    .Where(x => x.Field<bool>("Actualizado"));
                var territoriosAcutalizados = _territoriosActualizados.Any() ? _territoriosActualizados.CopyToDataTable() : DtTablaTerritorios_ViewModel.Clone();
                if (territoriosAcutalizados.Rows.Count > 0)
                {
                    var now = DateTime.Now;
                    foreach (DataRow row in territoriosAcutalizados.Rows)
                    {
                        ActualizarClienteDet(
                          model.IdCliente,
                          int.Parse(row["Id_CteDet"].ToString()),
                          int.Parse(row["Id_Ter"].ToString()),
                          now,
                          bool.Parse(row["Cte_Activo"].ToString()),
                          int.Parse(row["Id_TerServ"].ToString()),
                          row["Id_TerServTecnico"] != DBNull.Value ? int.Parse(row["Id_TerServTecnico"].ToString()) : -1  // NUEVO
                      );
                    }
                }

                //Territorio si no existe insertarlo
                if (dtVentasDirectas.Rows.Count > 0)
                    catCliente.InsertarClientesDet(cliente, dtVentasDirectas, MySesion.Emp_Cnx, MySesion.VersionTerritorio);
                else if (clientesDet.Rows.Count > 0)
                    catCliente.InsertarClientesDet(cliente, clientesDet, MySesion.Emp_Cnx, MySesion.VersionTerritorio);

                // Cambiar territorio activo
                if (lstTerActivo.Count == 1)
                {
                    if (lstTerInactivo.Count > 0)
                    {
                        foreach (var item in lstTerInactivo)
                        {
                            var clienteDet = new ClienteDet
                            {
                                Id_Emp = cliente.Id_Emp,
                                Id_Cd = cliente.Id_Cd,
                                Id_Cte = (int)cliente.Id_Cte,
                                Id_CteDet = item
                            };
                            catCliente.ActualizaTerritorioActivo(MySesion.Emp_Cnx, clienteDet, lstTerActivo[0], MySesion.Id_U);
                        }
                    }
                }

                catCliente.InsertarClientesDirEntrega(cliente, DtTableDireccionEntrega, MySesion.Emp_Cnx, MySesion.VersionTerritorio);
                if (DtTablaTerritorios.Rows.Count > 0 && MySesion.Id_Cd_Ver == respuesta)
                {
                    GuardarCliente_Territorio(model.IdCliente);
                }

                if (!model.Nuevo)
                {
                    var conv = ObtenerDatosConvenio(model.IdCliente);
                    if (conv != null && conv.Id_PC > 0)
                    {
                        if (cliente.Cte_NomComercial != NomComercial) EnviaMailAviso(Convert.ToInt32(cliente.Id_Cte), NomComercial, cliente.Cte_NomComercial, conv);
                    }
                }

                if (autorizaVinculacion == 2 && model.Nuevo) EnviaEmailViculacion(model.DatosGenerales.RFC, model.DatosGenerales.RazonSocial);
                if (autorizaVinculacion == 2 && !model.Nuevo && (territoriosEliminados.Rows.Count > 0 || territoriosAcutalizados.Rows.Count > 0))
                    EnviaEmailViculacion(model.DatosGenerales.RFC, model.DatosGenerales.RazonSocial);

                result.Estatus = true;
            }
            catch (Exception ex) { result.Mensaje = ex.Message; }

            return result;
        }

        static bool ValidarDatos_PreGuardar(ClienteModel model, ref ResponseJson json)
        {
            //HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID + "autorizacionVinculacion"] = 0;

            var centroDistribuciones = new List<CentroDistribucion>();
            new CN_CatCentroDistribucion().ConsultaCentrosPropios(ref centroDistribuciones, MySesion.Id_Emp, Emp_CnxCen);

            bool cambioCondicionesDePago = false;
            bool cambioTipoCliente = false;
            int condicionPagoDias = model.Cobranza.CondicionPagoDias;
            bool permiteGuardar = true;

            if (!model.Nuevo && centroDistribuciones.Count(x => x.Id_Cd == MySesion.Id_Cd_Ver) > 0)
            {
                var cte = new Clientes
                {
                    Id_Emp = MySesion.Id_Emp,
                    Id_Cd = MySesion.Id_Cd_Ver,
                    Id_Cte = model.IdCliente,
                    Ignora_Inactivo = true
                };
                var catCliente = new CN_CatCliente();
                catCliente.ConsultaClientes(ref cte, MySesion.Emp_Cnx);


                if (cte.Cte_CondPago != condicionPagoDias)
                {
                    permiteGuardar = false;
                    cambioCondicionesDePago = true;
                }

                if (cte.Id_TCte != model.DatosGenerales.TipoCliente)
                {
                    permiteGuardar = false;
                    cambioTipoCliente = true;
                    HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID + "cambioTipoCliente"] = "true";
                }
                else
                {
                    HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID + "cambioTipoCliente"] = "false";
                }
            }
            else cambioCondicionesDePago = true;


            string id_tu = "";
            var listAcciones = new List<Acciones>();
            var listPeriodoGracia = new List<PeriodoGracia>();
            var listAlertas = new List<Alertas>();
            var configCobranza = new CN_ConfiguracionCobranza();
            string db = new SqlConnectionStringBuilder(MySesion.Emp_Cnx).InitialCatalog;
            var reglas = new Reglas();

            configCobranza.Consultar(ref listPeriodoGracia, ref listAcciones, ref listAlertas, MySesion.Id_Emp, db, ref reglas, Emp_CnxCob);

            if (condicionPagoDias >= (double?)reglas.Val1 && condicionPagoDias <= (double?)reglas.Val2)
                id_tu = $",{reglas.Id_Tu1},{reglas.Id_Tu2},{reglas.Id_Tu3},";
            if (condicionPagoDias >= (double?)reglas.Val3 && condicionPagoDias <= (double?)reglas.Val4)
                id_tu = $",{reglas.Id_Tu3},{reglas.Id_Tu2},";
            if (condicionPagoDias >= (double?)reglas.Val5 && condicionPagoDias <= (double?)reglas.Val6)
                id_tu = $",{reglas.Id_Tu3},";

            if (permiteGuardar && !model.Nuevo) id_tu = "";


            if (id_tu != "" && centroDistribuciones.Count(x => x.Id_Cd == MySesion.Id_Cd_Ver) > 0)
            {
                HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID + "Id_Tu"] = id_tu;

                var clsTipoCliente = new CN_CatTipoCliente();
                var tipoCliente = new TipoCliente
                {
                    Id_TCte = model.DatosGenerales.TipoCliente
                };
                int verificador = 1;
                clsTipoCliente.ConsultaAutorizadores(tipoCliente, MySesion.Emp_Cnx, ref verificador);

                if (tipoCliente.TCte_Autorizadores == ",")
                {
                    HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID + "cambioTipoCliente"] = "false";
                }

                HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID + "TCte_Autorizadores"] = tipoCliente.TCte_Autorizadores;

                if (cambioCondicionesDePago && condicionPagoDias > 30)
                {
                    json.Mensaje = "openWindowAutorizacionCliente";
                    return false;
                }

                if (cambioTipoCliente)
                {
                    json.Mensaje = "openWindowAutorizacionTipoCliente";
                    return false;
                }
            }

            return true;
        }

        static void GuardarCliente_Territorio(int idCliente)
        {
            var sw = new StringWriter();
            DtTablaTerritorios.TableName = "objdtTablaTerritorios";
            DtTablaTerritorios.WriteXml(sw, XmlWriteMode.WriteSchema, false);
            string xmlTerritorios = sw.ToString();

            var swAnt = new StringWriter();
            DtTablaTerritoriosAnt.TableName = "objdtTablaTerritoriosAnt";
            DtTablaTerritoriosAnt.WriteXml(swAnt, XmlWriteMode.WriteSchema, false);
            string xmlTerritoriosAnt = swAnt.ToString();

            var ws = new wsTerritorio.Service1();
            var result = ws.GuardaAutClienteTerritorio(xmlTerritorios, xmlTerritoriosAnt);

            EnviaEmailAutorizacionTerritorios(idCliente, result);
        }

        static string EnviaEmailAutorizacionTerritorios(int idCliente, DataTable dt)
        {
            string result = "";
            try
            {
                var configuracion = new ConfiguracionGlobal
                {
                    Id_Cd = MySesion.Id_Cd_Ver,
                    Id_Emp = MySesion.Id_Emp
                };
                new CN_Configuracion().Consulta(ref configuracion, MySesion.Emp_Cnx);

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    var cuerpo_correo = new StringBuilder();
                    cuerpo_correo.Append("<div align='center'>");
                    cuerpo_correo.Append("<table><tr><td>");
                    cuerpo_correo.Append("<IMG SRC=\"cid:companylogo\" ALIGN='left'></td>");
                    cuerpo_correo.Append("<td></td>");
                    cuerpo_correo.Append("</tr><tr><td colspan='2'><br><br><br></td>");
                    cuerpo_correo.Append("</tr><tr>");
                    cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='2'>");
                    cuerpo_correo.Append("Solicitud # " + dt.Rows[x]["Id_Solicitud"]);
                    cuerpo_correo.Append("</td></tr><tr>");
                    cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='2'>");
                    cuerpo_correo.Append("Se ha solicitado cambio en el Territorio del cliente # " + dt.Rows[x]["Id_Cte"]);
                    cuerpo_correo.Append("</td></tr><tr><td><b><font face='Tahoma' size='2'>");
                    cuerpo_correo.Append("Centro de distribución:  " + MySesion.Id_Cd_Ver + " - " + MySesion.Cd_Nombre);
                    cuerpo_correo.Append("</td></tr><tr><td><b><font face='Tahoma' size='2'>");
                    cuerpo_correo.Append("Solicitó : " + MySesion.Id_U + " - " + MySesion.U_Nombre);
                    cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");
                    string[] url = HttpContext.Current.Request.Url.ToString().Split(new char[] { '/' });

                    cuerpo_correo.Append("<center><br><br>");
                    cuerpo_correo.Append("Solicitud de cambios en Cliente-Territorio del catalogo de clientes</a></font></center>");
                    cuerpo_correo.Append("</td></tr></table></div>");
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                    SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto))
                    {
                        Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña),
                        EnableSsl = true
                    };

                    MailMessage m = new MailMessage
                    {
                        From = new MailAddress(configuracion.Mail_Remitente)
                    };
                    m.To.Add(new MailAddress(configuracion.Mail_Autorizaterritorios));

                    m.Subject = "Solicitud de autorización para cambios en los territorios del cliente #" + idCliente + " del centro " + MySesion.Id_Cd_Ver;
                    m.IsBodyHtml = true;
                    string body = cuerpo_correo.ToString();
                    AlternateView vistaHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                    //Esto queda dentro de un try por si llegan a cambiar la imagen el correo como quiera se mande
                    try
                    {
                        LinkedResource logo = new LinkedResource(HttpContext.Current.Server.MapPath(@"Imagenes/logo.jpg"), MediaTypeNames.Image.Jpeg)
                        {
                            ContentId = "companylogo"
                        };
                        vistaHtml.LinkedResources.Add(logo);
                    }
                    catch { }

                    m.AlternateViews.Add(vistaHtml);
                    try { sm.Send(m); }
                    catch (Exception)
                    {
                        result = "Error al enviar el correo. Favor de revisar la configuración del sistema";
                    }
                }
            }
            catch { }
            return result;
        }

        static int ObtenerNumCuentaContNacional(int idCuenta)
        {
            int value = 0;
            if (idCuenta > 0)
            {
                switch (idCuenta)
                {
                    case 4:
                        value = 6500;
                        break;
                    case 6:
                        value = 6501;
                        break;
                    case 7:
                        value = 6502;
                        break;
                    case 8:
                        value = 6503;
                        break;
                    case 9:
                        value = 6504;
                        break;
                    case 11:
                        value = 6505;
                        break;
                }
            }
            return value;
        }

        static ConvenioDet ObtenerDatosConvenio(int idCliente)
        {
            var convdet = new ConvenioDet();

            try
            {
                var conv = new ConvenioDet()
                {
                    Id_Emp = MySesion.Id_Emp,
                    Id_Cd = MySesion.Id_Cd_Ver,
                    Id_Cte = idCliente
                };
                new CN_Convenio().Convenio_ConsultaClienteVinculado(conv, ref convdet, Emp_CnxCen);
            }
            catch { }
            return convdet;
        }

        static void EnviaMailAviso(int cte, string nombreactual, string nombrenuevo, ConvenioDet conv)
        {
            string Asunto = "Cambios en nombre de cliente";
            int adicionales = 0;  //Gerente JO
            int anexos = 0;
            int administradores = 1;
            int detalle = 0;
            int destinatarios = 0;  //RIKS

            int tipo_Habilitar = 1; //Ver


            StringBuilder cuerpo_correo = new StringBuilder();

            cuerpo_correo.Append("<div align='left'>");

            cuerpo_correo.Append("<table style='font-family: Verdana; font-size: 8pt'>");
            cuerpo_correo.Append("<tr> <td > * El nombre (Razón Social) del siguiente cliente ha cambiado.   </td>  </tr>");
            cuerpo_correo.Append("<tr> <td > * Validar si con esta modificación sigue perteneciendo al convenio. </td>  </tr>");
            cuerpo_correo.Append("<tr> <td >  &nbsp; </td>  </tr>");
            cuerpo_correo.Append("</table>");

            cuerpo_correo.Append("<Table  border='1'><tr><td><b>No. Cliente </b></td><td><b>Nombre de cliente ACTUAL:</b></td> <td><b>Nombre de cliente NUEVO: </b></td> <td><b>No. Convenio Key: </b></td><td><b>No. Convenio proveedor: </b></td><td><b>Nombre de convenio: </b></td><td><b>Concesionario: </b></td></tr>");
            cuerpo_correo.Append("<tr> <td >  " + cte.ToString() + "   </td>   ");
            cuerpo_correo.Append("<td >   " + nombreactual + "  </td> ");
            cuerpo_correo.Append("<td >   " + nombrenuevo + "  </td> ");
            cuerpo_correo.Append("<td >   " + conv.Id_PC.ToString() + "  </td> ");
            cuerpo_correo.Append("<td >   " + conv.PC_NoConvenio + "  </td> ");
            cuerpo_correo.Append("<td >   " + conv.PC_Nombre + "  </td> ");
            cuerpo_correo.Append("<td >   " + conv.Prd_Descripcion + "  </td>  </tr>");
            cuerpo_correo.Append("</table>");


            EnviaMailConvenio enviarcorreo = new EnviaMailConvenio(conv.Id_PC, Asunto, cuerpo_correo, "", 1, destinatarios, adicionales, anexos, administradores, 0, MySesion, tipo_Habilitar, detalle, "");
            enviarcorreo.EnviaMail();

        }

        static void EliminarClienteDet(int idCliente, int idTerr)
        {
            try
            {
                using (var conn = new SqlConnection(MySesion.Emp_Cnx))
                {
                    using (var cmd = new SqlCommand("spCatClienteDet_Eliminar", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_cd", SqlDbType.Int).Value = MySesion.Id_Cd;
                        cmd.Parameters.Add("@id_cte", SqlDbType.Int).Value = idCliente;
                        cmd.Parameters.Add("@id_ter", SqlDbType.Int).Value = idTerr;

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }

        static void ActualizarClienteDet(int idCliente, int idClienteDet, int idTerr, DateTime fecSolicitud, bool status, int idTerServ, int idTerServTecnico = -1)
        {
            try
            {
                using (var conn = new SqlConnection(MySesion.Emp_Cnx))
                {
                    using (var cmd = new SqlCommand("spCatClienteDet_ActualizarStatus", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_cd", SqlDbType.Int).Value = MySesion.Id_Cd;
                        cmd.Parameters.Add("@id_cte", SqlDbType.Int).Value = idCliente;
                        cmd.Parameters.Add("@id_cteDet", SqlDbType.Int).Value = idClienteDet;
                        cmd.Parameters.Add("@id_ter", SqlDbType.Int).Value = idTerr;
                        cmd.Parameters.Add("@fec_Solicitud", SqlDbType.DateTime).Value = fecSolicitud;
                        cmd.Parameters.Add("@status", SqlDbType.Bit).Value = status;
                        cmd.Parameters.Add("@id_terServ", SqlDbType.Int).Value = idTerServ;
                        cmd.Parameters.Add("@id_terServTecnico", SqlDbType.Int).Value = idTerServTecnico;

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }

        static void EnviaEmailViculacion(string rfc, string nombreCliente)
        {
            try
            {
                var configuracion = new ConfiguracionGlobal
                {
                    Id_Cd = MySesion.Id_Cd_Ver,
                    Id_Emp = MySesion.Id_Emp
                };

                new CN_Configuracion().Consulta(ref configuracion, MySesion.Emp_Cnx);
                StringBuilder cuerpo_correo = new StringBuilder();

                cuerpo_correo.Append("<div align='center'>");
                cuerpo_correo.Append(" <table>");
                cuerpo_correo.Append("  <tr>");
                cuerpo_correo.Append("   <td><img src=\"cid:companylogo\"></td>");
                cuerpo_correo.Append("   <td valign='middle' style='text-decoration: underline'><b><font face= 'Tahoma' size = '2'>Se ha creado un nuevo cliente corporativo con el RFC " + rfc + " Razón social " + nombreCliente + " en la sucursal " + MySesion.Id_Cd_Ver + " " + MySesion.Cd_Nombre + ". <br/> Favor de agregarlo a la estructura para la sucursal proceda a la vinculación  </font></b></td>");
                cuerpo_correo.Append("  </tr>");
                cuerpo_correo.Append("  <tr>");
                cuerpo_correo.Append("   <td colspan='2'><br><br><br></td>");
                cuerpo_correo.Append("  </tr>");
                cuerpo_correo.Append("  <tr>");
                cuerpo_correo.Append("   <td colspan='2'><b><font face= 'Tahoma' size = '2'></font></b></td>");
                cuerpo_correo.Append("  </tr>");
                cuerpo_correo.Append("  <tr>");
                cuerpo_correo.Append("   <td colspan='2'><br><br></td>");
                cuerpo_correo.Append("  </tr>");
                cuerpo_correo.Append("  <tr>");
                cuerpo_correo.Append("</font></b></td>");
                cuerpo_correo.Append("  </tr>");
                cuerpo_correo.Append("  <tr>");
                cuerpo_correo.Append("   <td colspan='2'><br><br></td>");
                cuerpo_correo.Append("  </tr>");
                cuerpo_correo.Append("  <tr>");
                cuerpo_correo.Append(" </table>");
                cuerpo_correo.Append("</div>");
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto))
                {
                    Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña),
                    EnableSsl = true
                };
                MailMessage m = new MailMessage
                {
                    From = new MailAddress(configuracion.Mail_Remitente)
                };

                var configMail = new CN_Configuracion().Obtener(MySesion, 52);
                if (configMail.Conf_Valor == "")
                {
                    return;
                }

                foreach (var address in configMail.Conf_Valor.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    m.To.Add(new MailAddress(address));
                }

                m.Subject = "Incluir cliente en estructura";
                m.IsBodyHtml = true;
                string body = cuerpo_correo.ToString();
                AlternateView vistaHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                //Esto queda dentro de un try por si llegan a cambiar la imagen el correo como quiera se mande
                try
                {
                    var logo = new LinkedResource(HttpContext.Current.Server.MapPath(@"Imagenes/logo.jpg"), MediaTypeNames.Image.Jpeg)
                    {
                        ContentId = "companylogo"
                    };
                    vistaHtml.LinkedResources.Add(logo);
                }
                catch { }
                m.AlternateViews.Add(vistaHtml);
                sm.Send(m);
            }
            catch { }
        }

        [WebMethod]
        public static List<ResponseJson_SelectFormat> ObtenerTerritoriosServicioTecnico()
        {
            var result = new List<ResponseJson_SelectFormat>();

            try
            {
                var list = new List<Comun>();
                new CD__Comun().LlenaCombo("spCatTerritorioServTecnico_Combo", MySesion.Emp_Cnx, ref list);

                if (list.Count > 0)
                {
                    result = list.Select(x => new ResponseJson_SelectFormat()
                    {
                        Descripcion = x.Descripcion,
                        Id = x.Id
                    }).ToList();
                }
            }
            catch { }
            return result;
        }
        #endregion
    }
}