using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaEntidad;
using CapaDatos;
using Telerik.Web.UI;
using CapaNegocios;
using System.Text;
using SIANWEB.Core.UI;
using CapaModelo;
using System.Configuration;
using Newtonsoft.Json;
using System.Web.Services;

namespace SIANWEB.PortalRIK.Nps_Sian
{
    public partial class CapNps_Lista : BaseServerPage
    {
        public int Parametro_IdTU; // Tipo Usaurio 3.- Gerente         
        public int Parametro_IdRik; // Representante Institucional Key RIK , para recibir parametro 
        public string Parametro_Nombre; // Nombre Gerente       
        // Gerente
        public int CRM_Gerente_Id;
        public int CRM_Gerente_Rik;
        public string CRM_Gerente_Nombre;
        // Usuario 
        public int CRM_Usuario_Id;
        public int CRM_Usuario_Rik;
        public string CRM_Usuario_Nombre;

        protected void Page_Load(object sender, EventArgs e)
        {
            SIANWEB.MasterPage.PortalRIK mp = Master as SIANWEB.MasterPage.PortalRIK;
            mp.CurrentPath = new List<string>() { "Gestion de la Promoción", "Leads" }.ToArray();

            

            if (!IsPostBack)
            {
                Parametro_IdTU = session.Id_TU;
                // Es perfil gerente ?
                if (Parametro_IdTU == 3)
                {
                    // Si es Gerente
                    // Llena valores de gerente
                    CRM_Gerente_Id = session.Id_U;
                    CRM_Gerente_Rik = session.Id_Rik;
                    CRM_Gerente_Nombre = session.U_Nombre;
                    // Llena valores de usuario
                    CRM_Usuario_Id = session.Id_U;
                    CRM_Usuario_Rik = session.Id_Rik;
                    CRM_Usuario_Nombre = session.U_Nombre;
                    // Si viene el RIK en los parametos 
                    if (Parametro_IdRik > 0)
                    {
                        CRM_Usuario_Rik = Parametro_IdRik;
                        CRM_Usuario_Nombre = Parametro_Nombre;
                    }
                }
                else
                {
                    // No es Gerente
                    // 
                    CRM_Gerente_Id = 0;
                    CRM_Gerente_Rik = 0;
                    CRM_Gerente_Nombre = "";
                    //
                    CRM_Usuario_Id = session.Id_U;
                    CRM_Usuario_Rik = session.Id_Rik;
                    CRM_Usuario_Nombre = session.U_Nombre;

                }

                Session["activeMenu"] = 14;
            }
        }

        public Sesion session
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

        private static entRespuestaNps SesionExpirada()
        {
            entRespuestaNps ObjResultado = new entRespuestaNps();
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];

                if (Sesion == null)
                {
                    ObjResultado.intValor = -1;
                    ObjResultado.boolResultado = false;
                    ObjResultado.strMensaje = "Sesion caducada, ingrese de nuevo.";
                    ObjResultado.objResultado = new
                    {
                        redirect = "../../login.aspx"
                    };
                }
                else
                {
                    ObjResultado.boolResultado = true;
                }

                return ObjResultado;
            }
            catch (Exception ex)
            {
                ObjResultado.intValor = -1;
                ObjResultado.boolResultado = false;
                ObjResultado.strMensaje = "Redireccionar";
                ObjResultado.objResultado = new
                {
                    redirect = "login.aspx"
                };
                return ObjResultado;
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
            Sesion Sesion = new Sesion();
            entRespuestaNps entResultado = new entRespuestaNps();
            int intEsGerente = 0;
            try
            {
                entRespuestaNps entRspSesion = SesionExpirada();

                if (entRspSesion.boolResultado == false)
                {
                    return entRspSesion;
                }

                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
                string strCnx = Sesion.Emp_Cnx;

                cnNps.ConsultarCatalogo(strCnx, "spCatNps_ComboTema", ref lstDataTema);
                cnNps.ConsultarCatalogo(strCnx, "spCatNps_ComboEstatus", ref lstDataEstatus);
                cnNps.ConsultarCatalogo(strCnx, "spCatNps_ComboTipo", ref lstDataTipo);

                if (Sesion.Id_TU == 3)
                {
                    cnNps.ConsultarRiks(Sesion.Emp_Cnx, ref lstDataRik, Sesion.Id_Cd);
                    cnNps.ConsultarCliente(Sesion.Emp_Cnx, ref lstDataCliente, Sesion.Id_Cd, 0);
                    intEsGerente = 1;
                }
                else
                {
                    lstDataRik.Add( new ItemsTextValue
                    {
                        intValue = Sesion.Id_Rik,
                        strText = Sesion.U_Nombre
                    });
                    cnNps.ConsultarCliente(Sesion.Emp_Cnx, ref lstDataCliente, Sesion.Id_Cd, Sesion.Id_Rik);
                }

                entResultado.boolResultado = true;
                entResultado.objResultado = new
                {
                    dataRik = lstDataRik.Select(x => new { x.intValue, x.strText }).ToList(),
                    dataNpsTipo = lstDataTipo.Select(x => new { x.intValue, x.strText }).ToList(),
                    dataNpsTema = lstDataTema.Select(x => new { x.intValue, x.strText }).ToList(),
                    dataNpsEstatus = lstDataEstatus.Select(x => new { x.intValue, x.strText }).ToList(),
                    dataNpsCliente = lstDataCliente.Select(x => new { x.intValue, x.strText }).ToList(),
                    EsGerente= intEsGerente
                };

                return entResultado;
            }
            catch (Exception ex)
            {
                return MensajeError(ex.Message, ex.StackTrace.ToString());
            }
        }


        [WebMethod]
        public static object CargarReporteNps(string strIdRik, string strIdCliente, string strIdNpsTipo, string strIdEstatus, string strFechaInicial,
                                              string strFechaFinal)
        {            
            CN_Nps cnNps = new CN_Nps();
            DateTime dateTemporal;
            entRespuestaNps entResultado = new entRespuestaNps();
            List<NpsQueja_Detalle> lstNpsQuejaDetalle = new List<NpsQueja_Detalle>();
            Nps_Filtro_Busqueda entNpsFiltro = new Nps_Filtro_Busqueda();           
            Sesion Sesion = new Sesion();

            try
            {
                entRespuestaNps entRspSesion = SesionExpirada();

                if (entRspSesion.boolResultado == false)
                {
                    return entRspSesion;
                }

                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];

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
                entNpsFiltro.Id_Emp = Sesion.Id_Emp;
                entNpsFiltro.Id_Rik = int.Parse(strIdRik);
                entNpsFiltro.Id_Cte = int.Parse(strIdCliente);
                entNpsFiltro.Id_Nps_Tipo = int.Parse(strIdNpsTipo);
                entNpsFiltro.Id_Nps_Estatus = int.Parse(strIdEstatus);
                entNpsFiltro.FechaInicial = strFechaInicial;
                entNpsFiltro.FechaFinal = strFechaFinal;
                entNpsFiltro.strUEN = "";

                cnNps.ConsultarNpsAgrupado(Sesion.Emp_Cnx, ref lstNpsQuejaDetalle, entNpsFiltro);

                entResultado.boolResultado = true;
                entResultado.objResultado = new
                {
                    dataReporte = lstNpsQuejaDetalle
                };

                return entResultado;

            }
            catch (Exception ex)
            {
                return MensajeError(ex.Message, ex.StackTrace.ToString());
            }
        }

        private static entRespuestaNps MensajeError(string strErrorMensaje, string strErrordetalle)
        {
            entRespuestaNps entRspError = new entRespuestaNps
            {
                intValor = 0,
                boolResultado = false,
                strMensaje = "Ocurrio un error:" + strErrorMensaje,
                objResultado = strErrordetalle
            };

            return entRspError;
        }



        [WebMethod]
        public static object CargarReporteNpsDetallado(string strIdRik, string strIdCliente, string strIdNpsTipo, string strIdEstatus, string strFechaInicial,
                                              string strFechaFinal)
        {
            CN_Nps cnNps = new CN_Nps();
            object objResult = new object();
            Nps_Filtro_Busqueda entNpsFiltro = new Nps_Filtro_Busqueda();
            Sesion Sesion = new Sesion();
            DateTime dateTemporal;
            entRespuestaNps entResultado = new entRespuestaNps();
            try
            {
                entRespuestaNps entRspSesion = SesionExpirada();
                if (entRspSesion.boolResultado == false)
                {
                    return entRspSesion;
                }

                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];            

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
                entNpsFiltro.Id_Cte = int.Parse(strIdCliente);
                entNpsFiltro.Id_Nps_Tipo = int.Parse(strIdNpsTipo);
                entNpsFiltro.Id_Nps_Tema = 0;
                entNpsFiltro.Id_Nps_Estatus = int.Parse(strIdEstatus);
                entNpsFiltro.FechaInicial = strFechaInicial;
                entNpsFiltro.FechaFinal = strFechaFinal;
                entNpsFiltro.Cd_Nombre = Sesion.Cd_Nombre;
                entNpsFiltro.strUEN = "";

                List<NpsQueja_ReporteDetalle> lstNpsQuejaDetalle = new List<NpsQueja_ReporteDetalle>();

                cnNps.ConsultarNpsReporteDetalle(Sesion.Emp_Cnx, ref lstNpsQuejaDetalle, entNpsFiltro);

                entResultado.boolResultado = true;
                entResultado.objResultado = new
                {
                    dataReporte = lstNpsQuejaDetalle
                };

                return entResultado;

            }
            catch (Exception ex)
            {
                return MensajeError(ex.Message, ex.StackTrace.ToString());
            }
        }


        [WebMethod]
        public static object ConsultarCliente( string strIdRiK)
        {
            CN_Nps cnNps = new CN_Nps();

            object objResultado = new object();
            List<object> lstObjResultado = new List<object>();
            List<ItemsTextValue> lstData = new List<ItemsTextValue>();
            Sesion Sesion = new Sesion();
            entRespuestaNps entResultado = new entRespuestaNps();
            try
            {
                entRespuestaNps entRspSesion = SesionExpirada();

                if (entRspSesion.boolResultado == false)
                {
                    return entRspSesion;
                }
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];

                int intIdRiK = int.Parse(strIdRiK);

                cnNps.ConsultarCliente(Sesion.Emp_Cnx, ref lstData, Sesion.Id_Cd, intIdRiK);

                entResultado.boolResultado = true;
                entResultado.objResultado = new
                {
                    dataCliente = lstData.Select(x => new { x.intValue, x.strText }).ToList(),
                };  

                return entResultado;
            }
            catch (Exception ex)
            {
                return MensajeError(ex.Message, ex.StackTrace.ToString());
            }
        }

       
        [WebMethod]
        public static object SeleccionarNps(string strIdNps)
        {
            CN_Nps cnNps = new CN_Nps();
            Nps entNps = new Nps();
            object objResultado = new object();
            List<object> lstObjResultado = new List<object>();
            List<NpsQueja> lstQueja = new List<NpsQueja>();

            Sesion Sesion = new Sesion();
            entRespuestaNps entResultado = new entRespuestaNps();
            try
            {
                entRespuestaNps entRspSesion = SesionExpirada();

                if (entRspSesion.boolResultado == false)
                {
                    return entRspSesion;
                }
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];

                int intIdNps = int.Parse(strIdNps);

                cnNps.ConsultarNps(Sesion.Emp_Cnx, ref entNps, ref lstQueja, Sesion.Id_Cd, intIdNps);
                            
                entResultado.boolResultado = true;
                entResultado.objResultado = new
                {
                    dataNps = entNps,
                    dataQueja = lstQueja,
                };

                return entResultado;

            }
            catch (Exception ex)
            {
                return MensajeError(ex.Message, ex.StackTrace.ToString());
            }
        }

        [WebMethod]
        public static object GuardarPlan(string strIdNps, string strDataQueja, string strConcluido, string strPlanConsecutivo)
        {
            CN_Nps cnNps = new CN_Nps();

            Sesion Sesion = new Sesion();
            entRespuestaNps entResultado = new entRespuestaNps();
            try
            {
                entRespuestaNps entRspSesion = SesionExpirada();

                if (entRspSesion.boolResultado == false)
                {
                    return entRspSesion;
                }
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];

                int intIdNps = int.Parse(strIdNps);
                int intConcluido = int.Parse(strConcluido);
                int intPlan_Consecutivo = int.Parse(strPlanConsecutivo);
                
                var lstQueja = JsonConvert.DeserializeObject<List<NpsQueja>>(strDataQueja);

                cnNps.GuardarPlan(Sesion.Emp_Cnx, Sesion.Id_Emp, Sesion.Id_Cd, Sesion.Id_U, intIdNps, lstQueja, intConcluido, intPlan_Consecutivo, ref entResultado);

                entResultado.boolResultado = true;
                return entResultado;

            }
            catch (Exception ex)
            {
                return MensajeError(ex.Message, ex.StackTrace.ToString());
            }
        }

    }
}