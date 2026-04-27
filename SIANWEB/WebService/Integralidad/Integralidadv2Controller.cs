using CapaEntidad;
using CapaModelo;
using CapaNegocios;
using DevExpress.DataProcessing;
using DevExpress.Web.ASPxRichEdit.Forms;
using SIANWEB.PortalRIK.GestionPromocion;
using SIANWEB.WebAPI.Models.Post;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.Http;
using System.Web.ModelBinding;

namespace SIANWEB.WebService.Integralidad
{
    public class Integralidadv2Controller : ApiController
    {
        // INTEGRALIDAD Opcion 1
        //25SEP-2021 RFh

        [HttpGet]
        public eResponse<List<eIntegralidadv2>> spListCdiByZona()
        {
            eResponse<List<eIntegralidadv2>> result = new eResponse<List<eIntegralidadv2>>();
            result.Estado = 0;

            List<eIntegralidadv2> lst = new List<eIntegralidadv2>();
            eIntegralidadv2 oeIntegralidadv2Par = new eIntegralidadv2();
            oeIntegralidadv2Par.Id_Zona = 0;
            oeIntegralidadv2Par.Tipo = 4;
            oeIntegralidadv2Par.Id_Cd = Sesion.Id_Cd;
            try
            {
                CN_IntegralidadV2 CN = new CN_IntegralidadV2();
                lst = CN.spListCdiByZona(oeIntegralidadv2Par, Emp_CnxCentral);

                result.Estado = 1;
                result.Datos = lst;
            }
            catch (Exception ex)
            {
                result.Estado = 0;
                result.Datos = null;
            }
            return result;
        }

        [HttpGet]
        public eResponse<List<eIntegralidadv2>> sp_MapaAplicaciones_DetalleXMes(int Id_Cte, int Id_Rik, int Id_Seg, int MesIni, int AnioIni, int Id_Uen, int Tipo = 5)
        {
            eResponse<List<eIntegralidadv2>> result = new eResponse<List<eIntegralidadv2>>();
            result.Estado = 0;

            List<eIntegralidadv2> lst = new List<eIntegralidadv2>();
            eIntegralidadv2 oeIntegralidadv2Par = new eIntegralidadv2();
            oeIntegralidadv2Par.Id_Cd = Sesion.Id_Cd;
            oeIntegralidadv2Par.Id_Cte = Id_Cte;
            oeIntegralidadv2Par.Id_Rik = Id_Rik;
            oeIntegralidadv2Par.Id_Seg = Id_Seg;
            oeIntegralidadv2Par.Id_Uen = Id_Uen;
            oeIntegralidadv2Par.MesIni = MesIni;
            oeIntegralidadv2Par.AnioIni = AnioIni;
            //oeIntegralidadv2Par.MesFin = MesFin;
            //oeIntegralidadv2Par.AnioFin = AnioFin;
            oeIntegralidadv2Par.Tipo = Tipo;
            try
            {
                CN_IntegralidadV2 CN = new CN_IntegralidadV2();

                if (Tipo == 6)
                {

                    lst = CN.spListIntegralidadByTipoProducto(oeIntegralidadv2Par, Emp_CnxCentral);

                }
                else
                {
                    lst = CN.sp_MapaAplicaciones_DetalleXMes(oeIntegralidadv2Par, Emp_CnxCentral);
                }


                result.Estado = 1;
                result.Datos = lst;
            }
            catch (Exception ex)
            {
                result.Estado = 0;
                result.Datos = null;
            }
            return result;
        }
        [HttpGet]
        public eResponse<List<eIntegralidadv2>> spConsultar_SegmentoUen(int Id_Cd, int Tipo, int Id_Uen)
        {
            eResponse<List<eIntegralidadv2>> result = new eResponse<List<eIntegralidadv2>>();
            CN_IntegralidadV2 CR = new CN_IntegralidadV2();
            List<eIntegralidadv2> Lst = new List<eIntegralidadv2>();

            try
            {
                Lst = CR.spConsultar_SegmentoUen(
                    Sesion.Id_Emp, Sesion.Id_Cd, Emp_CnxCentral, Tipo, Id_Uen);

                result.Datos = Lst;
                result.Estado = 1;
            }
            catch (Exception ex)
            {
                result.Estado = -1;
                result.Datos = null;
                result.Mensaje = ex.ToString();
            }
            return result;
        }

        //[HttpPost]
        //public eResponse<List<CatCRM_IntegralidadMes>> spActualizaVPOMeta([FromBody] ActualizaVPOMetaRequest req)
        //{
        //    eResponse<List<CatCRM_IntegralidadMes>> result = new eResponse<List<CatCRM_IntegralidadMes>>();
        //    result.Estado = 0;

        //    eIntegralidadv2 oeIntegralidadv2Par = new eIntegralidadv2();
        //    oeIntegralidadv2Par.Id_Cte = req.Id_Cte;
        //    oeIntegralidadv2Par.Id_Ter = req.Id_Ter;
        //    oeIntegralidadv2Par.VPOMeta = req.VPOMeta;
        //    oeIntegralidadv2Par.Mes = req.Mes;
        //    oeIntegralidadv2Par.Anio = req.Anio;
        //    oeIntegralidadv2Par.Id_Cd = Sesion.Id_Cd;
        //    oeIntegralidadv2Par.Id_Seg = req.IdSegmento;
        //    oeIntegralidadv2Par.Id_Uen = req.IdUen;
        //    oeIntegralidadv2Par.VPT = req.VPT;
        //    oeIntegralidadv2Par.Cantidad = req.Cantidad;

        //    List<CatCRM_IntegralidadMes> lst = new List<CatCRM_IntegralidadMes>();

        //    try//}
        //    {
        //        string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionSIANCentral"];

        //        CN_IntegralidadV2 CN = new CN_IntegralidadV2();
        //        lst = CN.sp_IntegralidadMes_ActualizaVPOMetaV2(oeIntegralidadv2Par, Conexion);
        //        result.Mensaje = "pasa 2";
        //        result.Estado = 1;
        //        result.Datos = null;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Mensaje = ex.Message;
        //        result.Estado = 0;
        //        result.Datos = null;
        //    }
        //    return result;


        //[HttpGet]
        //public eResponse<eIntegralidadv2> spActualizaVPOMeta(
        //int Id_Cte, int Id_Ter, float VPOMeta, int Mes, int Anio, int IdUen, int IdSegmento, float VPT, float Cantidad)
        //{
        //    eResponse<eIntegralidadv2> result = new eResponse<eIntegralidadv2>();
        //    result.Estado = 0;

        //    eIntegralidadv2 oeIntegralidadv2Par = new eIntegralidadv2();
        //    oeIntegralidadv2Par.Id_Cte = Id_Cte;
        //    oeIntegralidadv2Par.Id_Ter = Id_Ter;
        //    oeIntegralidadv2Par.VPOMeta = VPOMeta;
        //    oeIntegralidadv2Par.Mes = Mes;
        //    oeIntegralidadv2Par.Anio = Anio;
        //    oeIntegralidadv2Par.Id_Cd = Sesion.Id_Cd;
        //    oeIntegralidadv2Par.Id_Seg = IdSegmento;
        //    oeIntegralidadv2Par.Id_Uen = IdUen;
        //    oeIntegralidadv2Par.VPT = VPT;
        //    oeIntegralidadv2Par.Cantidad = Cantidad;


        //    try
        //    {
        //        string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionSIANCentral"];
        //        CN_IntegralidadV2 CN = new CN_IntegralidadV2();
        //        result.Datos = CN.sp_IntegralidadMes_ActualizaVPOMetaV2(oeIntegralidadv2Par, Conexion);

        //        result.Estado = 1;
        //        result.Datos = null;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Mensaje = ex.Message;
        //        result.Estado = 0;
        //        result.Datos = null;
        //    }
        //    return result;
        //}
        protected Sesion Sesion
        {
            get
            {
                if (HttpContext.Current.Session != null)
                {
                    return (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
                }
                return null;
            }
        }

        private string Emp_CnxCentral
        {
            get { return ConfigurationManager.AppSettings.Get("strConnectionSIANCentral"); }
        }

        //

    }
}