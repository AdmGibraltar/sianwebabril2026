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
using DevExpress.Office.Utils;

namespace SIANWEB
{
    public partial class CapNps_Reportes : System.Web.UI.Page
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

              
               
            }
            Session["activeMenu"] = 7;
        }




       





        /// <summary>
        /// evento que se llama en la seccion de comparar representante
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       

      
  


  
    


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

        protected void WCompararRepresentantes_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "comparar", "comparar()", false);
            
        }
     
        #endregion

        #region webMethod



        [WebMethod]
        public static object IndicadorGeneralNps(string mesAnioInicial, string mesAniofinal, string strUEN, string strRik)
        {
            try
            {
                entRespuestaNps objResultado = new entRespuestaNps();
                object objData = new object();
                string Emp_CnxCentral = ConfigurationManager.AppSettings.Get("strConnectionCentral");
                CN_Nps cnNps = new CN_Nps();
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];

                if (Sesion == null)
                {
                    return JsonConvert.SerializeObject(new { id = 0 });
                }

                if (mesAnioInicial != "" && mesAniofinal != "")
                {
                    if (DateTime.Parse(mesAnioInicial) > DateTime.Parse(mesAniofinal))
                    {
                        return JsonConvert.SerializeObject(new { id = 4 });
                    }
                    DateTime dateInicial = DateTime.Parse(mesAnioInicial);
                    DateTime dateFinal = DateTime.Parse(mesAniofinal).AddMonths(1).AddDays(-1);
                    //object objDatos = new object();
                    List<Nps_IndicadorNps> lstIndicadorNps = new List<Nps_IndicadorNps>();
                    entNpsIndicadorFiltro entFiltros = new entNpsIndicadorFiltro();

                    //
                    entFiltros.dateInicial = dateInicial;
                    entFiltros.dateFinal = dateFinal;
                    entFiltros.intRik = int.Parse(strRik);
                    entFiltros.strUEN = strUEN;

                    cnNps.IndicadorGeneralNps(Sesion.Emp_Cnx, Sesion.Id_Emp, Sesion.Id_Cd, entFiltros, ref lstIndicadorNps);
                    string[] lstlabels = new string[] { };
                    int[] lstTotals= new int[] { };
                    int intMaxValue = 0;
                    int intGranTotal = 0;
                    if (lstIndicadorNps.Count >0)
                    {
                       lstlabels = lstIndicadorNps.Select(x => x.Nps_descripcion).ToArray();
                       lstTotals = lstIndicadorNps.Select(x => x.Total).ToArray();
                       intMaxValue = lstIndicadorNps.Max(x=> x.Total);
                        intGranTotal = lstIndicadorNps.Select(x => x.GranTotal).FirstOrDefault();
                    }


                    objData = new
                    {
                        dataResult = lstIndicadorNps,
                        dataLabels = lstlabels,
                        dataTotals = lstTotals,
                        maxValue = intMaxValue,
                        granTotal = intGranTotal
                    };

                    objResultado = new entRespuestaNps
                    {
                        boolResultado = true,
                        strMensaje = "",
                        objResultado = objData
                    };
                }
                else
                {
                    objResultado = new entRespuestaNps
                    {
                        boolResultado = false,
                        strMensaje = "Seleccione fecha valida",

                    };
                }
                return objResultado;


            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = ex.Message });
            }
        }



        [WebMethod]
        public static object IndicadorTrazabilidad(string mesAnioInicial, string mesAniofinal, string strEstatus, string strUEN, string strRik)
        {
            entRespuestaNps objResultado = new entRespuestaNps();
            try
            {
              
                object objData = new object();

                CN_Nps cnNps = new CN_Nps();
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];

                if (Sesion == null)
                {
                    return JsonConvert.SerializeObject(new { id = 0 });
                }

                if (mesAnioInicial != "" && mesAniofinal != "")
                {
                    if (DateTime.Parse(mesAnioInicial) > DateTime.Parse(mesAniofinal))
                    {
                        return JsonConvert.SerializeObject(new { id = 4 });
                    }
                    DateTime dateInicial = DateTime.Parse(mesAnioInicial);
                    DateTime dateFinal = DateTime.Parse(mesAniofinal).AddMonths(1).AddDays(-1);
                    
                    List<Nps_IndicadorTrazabilidad> lstIndicadorTrazabilidad = new List<Nps_IndicadorTrazabilidad>();
                    List<Nps_IndicadorTrazabilidadCliente> lstIndicadorTrazabilidadCliente = new List<Nps_IndicadorTrazabilidadCliente>();
                   
                    entNpsIndicadorFiltro entFiltros = new entNpsIndicadorFiltro();
                    entFiltros.dateInicial = dateInicial;
                    entFiltros.dateFinal = dateFinal;
                    entFiltros.strStatus = strEstatus;
                    entFiltros.intRik = int.Parse(strRik);
                    entFiltros.strUEN = strUEN;

                    cnNps.IndicadorTrazabilidad(
                        Sesion.Emp_Cnx, 
                        Sesion.Id_Emp, 
                        Sesion.Id_Cd,
                        entFiltros,
                        ref lstIndicadorTrazabilidad,
                        ref lstIndicadorTrazabilidadCliente);

                    string[] lstlabels = new string[] { };
                    int[] lstTotals = new int[] { };
                    int intMaxValue = 0;
                    int intGranTotal = 0;

                    if (lstIndicadorTrazabilidad.Count > 0)
                    {
                        lstlabels = lstIndicadorTrazabilidad.Select(x => x.Estatus_Descripcion).ToArray();
                        lstTotals = lstIndicadorTrazabilidad.Select(x => x.Total).ToArray();
                        intMaxValue = lstIndicadorTrazabilidad.Max(x => x.Total);
                        intGranTotal = lstIndicadorTrazabilidad.Select(x => x.GranTotal).FirstOrDefault();
                    }

                    objData = new
                    {
                        dataResult = lstIndicadorTrazabilidad,
                        dataDtlClientes = lstIndicadorTrazabilidadCliente,
                        dataLabels = lstlabels,
                        dataTotals = lstTotals,
                        maxValue = intMaxValue,
                        granTotal = intGranTotal
                    };

                    objResultado = new entRespuestaNps
                    {
                        boolResultado = true,
                        strMensaje = "",
                        objResultado = objData
                    };
                }
                else
                {
                    objResultado = new entRespuestaNps
                    {
                        boolResultado = false,
                        strMensaje = "Seleccione fecha valida",

                    };
                }
                return objResultado;


            }
            catch (Exception ex)
            {
                objResultado = new entRespuestaNps
                {
                    boolResultado = false,
                    strMensaje = "Error fallo la transacción: " + ex.Message,

                };
                return objResultado;
            }
        }

        
        [WebMethod]
        public static object IndicadorConversion(string mesAnioInicial, string mesAniofinal, string strUEN, string strRik)
        {
            try
            {
                entRespuestaNps objResultado = new entRespuestaNps();
                object objData = new object();

                CN_Nps cnNps = new CN_Nps();
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];

                if (Sesion == null)
                {
                    return JsonConvert.SerializeObject(new { id = 0 });
                }

                if (mesAnioInicial != "" && mesAniofinal != "")
                {
                    if (DateTime.Parse(mesAnioInicial) > DateTime.Parse(mesAniofinal))
                    {
                        return JsonConvert.SerializeObject(new { id = 4 });
                    }
                    DateTime dateInicial = DateTime.Parse(mesAnioInicial);
                    DateTime dateFinal = DateTime.Parse(mesAniofinal).AddMonths(1).AddDays(-1);

                    List<Nps_IndicadorConversion> lstIndicadorConversion = new List<Nps_IndicadorConversion>();
                    entNpsIndicadorFiltro entFiltros = new entNpsIndicadorFiltro();

                    //
                    entFiltros.dateInicial = dateInicial;
                    entFiltros.dateFinal = dateFinal;
                    entFiltros.intRik = int.Parse(strRik);
                    entFiltros.strUEN = strUEN;

                    cnNps.IndicadorConversion(Sesion.Emp_Cnx, Sesion.Id_Emp, Sesion.Id_Cd, entFiltros, ref lstIndicadorConversion);
                    
                    

                    objResultado = new entRespuestaNps
                    {
                        boolResultado = true,
                        strMensaje = "",
                        objResultado = lstIndicadorConversion
                    };
                }
                else
                {
                    objResultado = new entRespuestaNps
                    {
                        boolResultado = false,
                        strMensaje = "Seleccione fecha valida",

                    };
                }
                return objResultado;


            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = ex.Message });
            }
        }

        [WebMethod]
        public static object ConsultarCatalogos()
        {
            CN_Nps cnNps = new CN_Nps();

            object objResultado = new object();
            List<object> lstObjResultado = new List<object>();
            List<ItemsTextValue> lstDataTipo = new List<ItemsTextValue>();
            List<ItemsTextValue> lstDataTema = new List<ItemsTextValue>();
            List<ItemsTextValue> lstDataEstatus = new List<ItemsTextValue>();
            List<ItemsTextValue> lstDataRik = new List<ItemsTextValue>();
            List<ItemsTextValue> lstDataCliente = new List<ItemsTextValue>();
            List<ItemsTextValue> lstDataUEN = new List<ItemsTextValue>();
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
            string strCnx = Sesion.Emp_Cnx;
            int intEsGerente = 0;

            cnNps.ConsultarCatalogo(strCnx, "spCatNps_ComboTema", ref lstDataTema);
            cnNps.ConsultarCatalogo(strCnx, "spCatNps_ComboEstatus", ref lstDataEstatus);
            cnNps.ConsultarCatalogo(strCnx, "spCatNps_ComboTipo", ref lstDataTipo);
            cnNps.ConsultarCatalogo(strCnx, "spCatNps_ComboUEN", ref lstDataUEN);

            if (Sesion.Id_TU == 3)
            {
                cnNps.ConsultarRiks(Sesion.Emp_Cnx, ref lstDataRik, Sesion.Id_Cd);
                intEsGerente = 1;
            }
            else
            {
                lstDataRik.Add(new ItemsTextValue
                {
                    intValue = Sesion.Id_Rik,
                    strText = Sesion.U_Nombre
                });

            }

            objResultado = new
            {
                dataRik = lstDataRik.Select(x => new { x.intValue, x.strText }).ToList(),
                dataNpsTipo = lstDataTipo.Select(x => new { x.intValue, x.strText }).ToList(),
                dataNpsTema = lstDataTema.Select(x => new { x.intValue, x.strText }).ToList(),
                dataNpsEstatus = lstDataEstatus.Select(x => new { x.intValue, x.strText }).ToList(),
                dataNpsUEN = lstDataUEN.Select(x => new { x.intValue, x.strText }).ToList(),
                EsGerente = intEsGerente
            };

            return objResultado;
        }

        [WebMethod]
        public static object CargarReporteNps(string strIdRik, string strIdNpsTipo, string strIdEstatus, string strFechaInicial,
                                            string strFechaFinal,  string strUEN)
        {

            CN_Nps cnNps = new CN_Nps();
            object objResult = new object();

            Nps_Filtro_Busqueda entNpsFiltro = new Nps_Filtro_Busqueda();
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];

            DateTime dateTemporal;

            if (!string.IsNullOrEmpty(strFechaInicial))
            {
                dateTemporal = DateTime.Parse(strFechaInicial);
                strFechaInicial = dateTemporal.ToString("yyyy-MM-dd") + " 00:00:00.000";
            }
            else
            {
                strFechaInicial = "-1";
            }
            if (!string.IsNullOrEmpty(strFechaFinal))
            {
                dateTemporal = DateTime.Parse(strFechaFinal);
                strFechaFinal = dateTemporal.ToString("yyyy-MM-dd") + " 23:59:59.000";
            }
            else
            {
                strFechaFinal = "-1";
            }

            entNpsFiltro.Id_Cd = Sesion.Id_Cd;
            entNpsFiltro.Id_Rik = int.Parse(strIdRik);
            entNpsFiltro.Id_Cte = 0;
            entNpsFiltro.Id_Nps_Tipo = int.Parse(strIdNpsTipo);
            entNpsFiltro.Id_Nps_Estatus = int.Parse(strIdEstatus);
            entNpsFiltro.FechaInicial = strFechaInicial;
            entNpsFiltro.FechaFinal = strFechaFinal;
            entNpsFiltro.Cd_Nombre = Sesion.Cd_Nombre.ToString();
            entNpsFiltro.strUEN = strUEN;

            List<NpsQueja_Detalle> lstNpsQuejaDetalle = new List<NpsQueja_Detalle>();
            
            cnNps.ConsultarNpsAgrupado(Sesion.Emp_Cnx, ref lstNpsQuejaDetalle, entNpsFiltro);

            objResult = new
            {
                dataReporte = lstNpsQuejaDetalle
            };

            return objResult;
        }


        [WebMethod]
        public static object CargarReporteNpsDetallado(string strIdRik, string strIdNpsTipo, string strIdEstatus, string strFechaInicial,
                                           string strFechaFinal, string strUEN)
        {
            CN_Nps cnNps = new CN_Nps();
            object objResult = new object();

            Nps_Filtro_Busqueda entNpsFiltro = new Nps_Filtro_Busqueda();
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];

            DateTime dateTemporal;

            if (!string.IsNullOrEmpty(strFechaInicial))
            {
                dateTemporal = DateTime.Parse(strFechaInicial);
                strFechaInicial = dateTemporal.ToString("yyyy-MM-dd") + " 00:00:00.000";
            }
            else
            {
                strFechaInicial = "-1";
            }
            if (!string.IsNullOrEmpty(strFechaFinal))
            {
                dateTemporal = DateTime.Parse(strFechaFinal);
                strFechaFinal = dateTemporal.ToString("yyyy-MM-dd") + " 23:59:59.000";
            }
            else
            {
                strFechaFinal = "-1";
            }


            entNpsFiltro.Id_Cd = Sesion.Id_Cd;
            entNpsFiltro.Id_Rik = int.Parse(strIdRik);
            entNpsFiltro.Id_Cte = 0;
            entNpsFiltro.Id_Nps_Tipo = int.Parse(strIdNpsTipo);
            entNpsFiltro.Id_Nps_Tema = 0;
            entNpsFiltro.Id_Nps_Estatus = int.Parse(strIdEstatus);
            entNpsFiltro.FechaInicial = strFechaInicial;
            entNpsFiltro.FechaFinal = strFechaFinal;
            entNpsFiltro.Cd_Nombre = Sesion.Cd_Nombre.ToString();
            entNpsFiltro.strUEN = strUEN;

            List<NpsQueja_ReporteDetalle> lstNpsQuejaDetalle = new List<NpsQueja_ReporteDetalle>();

            cnNps.ConsultarNpsReporteDetalle(Sesion.Emp_Cnx, ref lstNpsQuejaDetalle, entNpsFiltro);

            objResult = new
            {
                dataReporte = lstNpsQuejaDetalle
            };

            return objResult;
        }


        #endregion
    }
}