using CapaEntidad;
using CapaNegocios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SIANWEB.WebService.Integralidad
{
    public class ActualizaVPOIntegralidadController : ApiController
    {

        [HttpGet]
        public eResponse<eIntegralidadv2> spActualizaVPOMeta(
        int Id_Cte, int Id_Ter, float VPOMeta, int Mes, int Anio, int IdUen, int IdSegmento, float VPT, float Cantidad)
        {
            eResponse<eIntegralidadv2> result = new eResponse<eIntegralidadv2>();
            result.Estado = 0;

            eIntegralidadv2 oeIntegralidadv2Par = new eIntegralidadv2();
            oeIntegralidadv2Par.Id_Cte = Id_Cte;
            oeIntegralidadv2Par.Id_Ter = Id_Ter;
            oeIntegralidadv2Par.VPOMeta = VPOMeta;
            oeIntegralidadv2Par.Mes = Mes;
            oeIntegralidadv2Par.Anio = Anio;
            oeIntegralidadv2Par.Id_Cd = Sesion.Id_Cd;
            oeIntegralidadv2Par.Id_Seg = IdSegmento;
            oeIntegralidadv2Par.Id_Uen = IdUen;
            oeIntegralidadv2Par.VPT = VPT;
            oeIntegralidadv2Par.Cantidad = Cantidad;


            try
            {
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionSIANCentral"];
                CN_IntegralidadV2 CN = new CN_IntegralidadV2();
                result.Datos = CN.sp_IntegralidadMes_ActualizaVPOMetaV2(oeIntegralidadv2Par, Conexion);

                result.Estado = 1;
                result.Datos = null;
            }
            catch (Exception ex)
            {
                result.Mensaje = ex.Message;
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
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionSIANCentral"];
                Lst = CR.spConsultar_SegmentoUen(
                    Sesion.Id_Emp, Sesion.Id_Cd, Conexion, Tipo, Id_Uen);

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