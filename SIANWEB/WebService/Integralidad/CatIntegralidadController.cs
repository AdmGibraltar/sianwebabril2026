using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaEntidad;
using CapaNegocios;
using System.Net.Http;
using CapaDatos;
using CapaModelo;


namespace SIANWEB.WebService
{
    public class CatIntegralidadController : ApiController
    {
        // INTEGRALIDAD Opcion 1
        //25SEP-2021 RFh

        [HttpGet]
        public eResponse<List<CatCRM_ListadoRIKS>> spCrmInt_IntegralidadMes_Por_RIK_Listado_Ver2(
            string Riks, string Ctes, string Segs, int Opcion2)
        {
            eResponse<List<CatCRM_ListadoRIKS>> result = new eResponse<List<CatCRM_ListadoRIKS>>();
            result.Estado = 0;

            List<CatCRM_ListadoRIKS> lst = new List<CatCRM_ListadoRIKS>();
            try
            {
                CN_CatCRMInt_Integralidad CN = new CN_CatCRMInt_Integralidad();
                lst = CN.ListadoRiks_Ver2(Sesion.Id_Emp, Sesion.Id_Cd, Riks, Ctes, Segs, Sesion.Emp_Cnx);

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
        public eResponse<List<CatCRM_ReporteIntegralidadGral>> CargarRepresentantes(
            string Riks, string Ctes, string Segs)
        {
            eResponse<List<CatCRM_ReporteIntegralidadGral>> result = new eResponse<List<CatCRM_ReporteIntegralidadGral>>();
            result.Estado = 0;

            List<CatCRM_ReporteIntegralidadGral> lst = new List<CatCRM_ReporteIntegralidadGral>();
            try
            {
                CN_CatCRMInt_Integralidad CN = new CN_CatCRMInt_Integralidad();

                lst = CN.ListadoIntegralidadRIK(Sesion.Id_Emp, Sesion.Id_Cd, Riks, Sesion.Emp_Cnx);

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

        // 21Oct-2021 Rfh
        [HttpGet]
        public eResponse<List<CatCRM_ReporteIntegralidadGral>> spCrmIntegralidad_PorRik(
            string RiksSeleccionados, int param1)
        {
            eResponse<List<CatCRM_ReporteIntegralidadGral>> result = new eResponse<List<CatCRM_ReporteIntegralidadGral>>();
            result.Estado = 0;

            List<CatCRM_ReporteIntegralidadGral> lst = new List<CatCRM_ReporteIntegralidadGral>();
            try
            {
                CN_CatCRMInt_Integralidad CN = new CN_CatCRMInt_Integralidad();

                lst = CN.spCrmIntegralidad_PorRik(Sesion.Id_Emp, Sesion.Id_Cd, RiksSeleccionados, Sesion.Emp_Cnx);

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


        /*
        [HttpGet]
        public eResponse<List<CatCRM_IntegralidadMes>> spCrmInt_IntegralidadMes(int Id_Cte, int Id_Rik, int Id_Ter, int Id_Seg)
        {
            eResponse<List<CatCRM_IntegralidadMes>> result = new eResponse<List<CatCRM_IntegralidadMes>>();
            result.Estado = 0;
            List<CatCRM_IntegralidadMes> lst = new List<CatCRM_IntegralidadMes>();
            try
            {
                CN_CatCRMInt_Integralidad CN = new CN_CatCRMInt_Integralidad();
                lst = CN.ListadoAplicaciones(Id_Cte, Id_Rik, Id_Ter, Id_Seg, Sesion.Emp_Cnx);
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
        */

        // INspCrmInt_IntegralidadMes_Por_RIK_Listado_Ver2TEGRALIDAD Opcion 2 - Detalle
        // 4OCT-2021 RFH Consulta Integralidad 

        [HttpGet]
        public eResponse<List<CatCRM_IntegralidadMes>> spCrmInt_IntegralidadMes_Ver2(
            int Id_Cte, int Id_Rik, int Id_Ter, int Id_Seg, int Par4)
        {
            eResponse<List<CatCRM_IntegralidadMes>> result = new eResponse<List<CatCRM_IntegralidadMes>>();
            result.Estado = 0;

            List<CatCRM_IntegralidadMes> lst = new List<CatCRM_IntegralidadMes>();

            try
            {
                CN_CatCRMInt_Integralidad CN = new CN_CatCRMInt_Integralidad();
                lst = CN.ListadoAplicaciones_Ver2(Id_Cte, Id_Rik, Id_Ter, Id_Seg, Sesion.Emp_Cnx);

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
        public eResponse<List<CatCRM_IntegralidadMes>> spCrmInt_Integralidad_ActualizaVPOMeta(
            int Id_Cte,  int Id_Ter ,float VPOMeta, int Par4)
        {
            eResponse<List<CatCRM_IntegralidadMes>> result = new eResponse<List<CatCRM_IntegralidadMes>>();
            result.Estado = 0;

            List<CatCRM_IntegralidadMes> lst = new List<CatCRM_IntegralidadMes>();

            try
            {
                CN_CatCRMInt_Integralidad CN = new CN_CatCRMInt_Integralidad();
                lst = CN.CNActualizaVPOMeta(Id_Cte,  Id_Ter , Sesion.Emp_Cnx, VPOMeta);

                result.Estado = 1;
                result.Datos = null;
            }
            catch (Exception ex)
            {
                result.Estado = 0;
                result.Datos = null;
            }
            return result;
        }

        // 4OCT-2021 RFH
        [HttpGet]
        public eResponse<List<eCrmInt_MapaAplicaciones_Detalle>> spCrmInt_MapaAplicaciones_Detalle_Ver2(
            int Id_Cte, int Id_Ter, int Id_Usu, int Id_Seg, int Cal_Anio, int Cal_Mes)
        {
            eResponse<List<eCrmInt_MapaAplicaciones_Detalle>> result = new eResponse<List<eCrmInt_MapaAplicaciones_Detalle>>();
            result.Estado = 0;
            List<eCrmInt_MapaAplicaciones_Detalle> lst = new List<eCrmInt_MapaAplicaciones_Detalle>();
            try
            {
                CN_CatCRMInt_Integralidad CN = new CN_CatCRMInt_Integralidad();
                lst = CN.spCrmInt_MapaAplicaciones_Detalle_Ver2(
                    Id_Cte, Id_Ter, Id_Usu, Id_Seg, Cal_Anio, Cal_Mes, Sesion.Emp_Cnx);

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

        //

    }
}