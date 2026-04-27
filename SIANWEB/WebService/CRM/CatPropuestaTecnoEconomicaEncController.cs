using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaEntidad;
using CapaNegocios;
using CapaModelo;
using CapaDatos;
using SIANWEB.WebAPI.Models;
using System.Net.Http;
using SIANWEB.Core.Web.API;
using System.Threading.Tasks;

namespace SIANWEB.WebService
{
    public class CatPropuestaTecnoEconomicaEncController: ApiController
    {

        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        // Encavezado
		// 5 Oct 2018 se Actualiza
        [HttpGet]
        public CapaEntidad.eCapValProyecto Get(
            int CRM_Usuario_Rik, int Enc, int Id_Op, int Id_Cte, int Id_Val)
        {
            int Rik = 0;
            if (CRM_Usuario_Rik > 0)
            {
                Rik = CRM_Usuario_Rik;
            }
            else
            {
                Rik = Sesion.Id_Rik;
            }
            CapaEntidad.eCapValProyecto Obj = new CapaEntidad.eCapValProyecto();
            CN_CrmPropuestaEconomica cnPE = new CN_CrmPropuestaEconomica();
            Obj = cnPE.spCRMCapValProyecto(Sesion.Id_Emp, Sesion.Id_Cd, Id_Cte, Rik, Id_Op, Id_Val, Sesion);
            return Obj;
        }

        // Ver 2
        // JUN11-2020 RFH 
        [HttpGet]
        public eResponse<CapaEntidad.eCapValProyecto> spCRMCapValProyecto(
            int CRM_Usuario_Rik, int Id_Op, int Id_Cte, int Id_Val)
        {

            eResponse<CapaEntidad.eCapValProyecto> result = new eResponse<CapaEntidad.eCapValProyecto>();
            CapaEntidad.eCapValProyecto Obj = new eCapValProyecto();
            result.Estado = 0;
            result.Mensaje = "Consulta CapValProyecto";
            result.Datos = null;

            int Rik = 0;

            try
            {                
                if (CRM_Usuario_Rik > 0)
                {
                    Rik = CRM_Usuario_Rik;
                }
                else
                {
                    Rik = Sesion.Id_Rik;
                }

                CN_CrmPropuestaEconomica CN = new CN_CrmPropuestaEconomica();
                Obj = CN.spCRMCapValProyecto(Sesion.Id_Emp, Sesion.Id_Cd, Id_Cte, Rik, Id_Op, Id_Val, Sesion);
                
                result.Estado = 1;
                result.Datos = Obj;
            }
            catch (Exception ex)
            {
                result.Estado = -1;
                result.Datos = null;
                result.Mensaje = ex.Message;
            }
            return result;
        }
        
        // 7 Ene 2019 RFH
        [HttpGet]
        public eResponse<int> GuardarCamposDeReporte(int Id_op, 
            string tbNombreAtencion, string tbRepresentanteClienteNombre, string tbNombreRik, 
            string tbDireccion1, string tbDireccion2)
        {
            eResponse<int> result = new eResponse<int>();
            result.Estado = 0;
            result.Mensaje = "";
            try
            {
                CN_CapRepValuacionParams CN = new CN_CapRepValuacionParams();
                result.Datos = CN.SpCrmOportunidades_UpdateRepParams(Sesion.Id_Emp, Sesion.Id_Cd, Id_op, 
                    tbNombreAtencion, tbRepresentanteClienteNombre, tbNombreRik, 
                    tbDireccion1, tbDireccion2, Sesion.Emp_Cnx);
                result.Estado = 1;
            }
            catch (Exception ex)
            {
                result.Datos = 0;
                result.Estado = -1;
                result.Mensaje = ex.ToString();
            }
            return result;
        }

        // MAY22 2020 RFH        
        /*
        [HttpPut]
        public eResponse<int> GuardarCamposDeReporte_Ver2(eCRM_ValuacionCamposAdicionales E) 
        {
            eResponse<int> result = new eResponse<int>();
            result.Estado = 0;
            result.Mensaje = "";
            try
            {
                CN_CapRepValuacionParams CN = new CN_CapRepValuacionParams();
                E.Id_Emp = Sesion.Id_Emp;
                E.Id_Cd = Sesion.Id_Cd;
                result.Datos = CN.SpCrmOportunidades_UpdateRepParams_Ver2(E, Sesion.Emp_Cnx);
                result.Estado = 1;
            }
            catch (Exception ex)
            {
                result.Datos = 0;
                result.Estado = -1;
                result.Mensaje = ex.ToString();
            }
            return result;
        }
        */

        // JUN30-2020
        // CRM Variables de Reporetes Update

        [HttpPut]
        public eResponse<int> GuardarCamposDeReporte_Ver3(eCRM_ValuacionCamposAdicionales_Ver2 E)
        {
            eResponse<int> result = new eResponse<int>();
            result.Estado = 0;
            result.Mensaje = "";
            try
            {
                CN_CapRepValuacionParams CN = new CN_CapRepValuacionParams();
                E.Id_Emp = Sesion.Id_Emp;
                E.Id_Cd = Sesion.Id_Cd;
                result.Datos = CN.SpCrmOportunidades_UpdateRepParams_Ver3(E, Sesion.Emp_Cnx);
                result.Estado = 1;
            }
            catch (Exception ex)
            {
                result.Datos = 0;
                result.Estado = -1;
                result.Mensaje = ex.ToString();
            }
            return result;
        }
                        
        // 7 Ene 2019 RFH
        [HttpGet]
        public eResponse<eRepValuacionParams> GetCamposDeReporte(int Id_op)
        {
            eResponse<eRepValuacionParams> result = new eResponse<eRepValuacionParams>();
            eRepValuacionParams RVP = new eRepValuacionParams();
            try
            {                
                CN_CapRepValuacionParams CN = new CN_CapRepValuacionParams();
                RVP = CN.SpCrmOportunidades_RepValuacionProp(Sesion.Id_Emp, Sesion.Id_Cd, Id_op, Sesion.Emp_Cnx);
                result.Datos = RVP;
                result.Estado = 1;
            }
            catch (Exception ex)
            {
                result.Datos = null;
                result.Estado = -1;
            }
            return result;
        }

        // MAY26-2020 RFH
        /*
        [HttpGet]
        public eResponse<eRepValuacionParams> GetCamposDeReporte_Ver2(int Id_op, int Param1)
        {
            eResponse<eRepValuacionParams> result = new eResponse<eRepValuacionParams>();
            eRepValuacionParams RVP = new eRepValuacionParams();
            try
            {
                CN_CapRepValuacionParams CN = new CN_CapRepValuacionParams();
                RVP = CN.SpCrmOportunidades_RepValuacionProp_Ver2(Sesion.Id_Emp, Sesion.Id_Cd, Id_op, Sesion.Emp_Cnx);
                result.Datos = RVP;
                result.Estado = 1;
            }
            catch (Exception ex)
            {
                result.Datos = null;
                result.Estado = -1;
            }
            return result;
        }
        */

        // JUN30 -2020

        [HttpGet]
        public eResponse<eCRM_ValuacionCamposAdicionales_Ver2> GetCamposDeReporte_Ver2(int Id_op, int Param1)
        {
            eResponse<eCRM_ValuacionCamposAdicionales_Ver2> result = new eResponse<eCRM_ValuacionCamposAdicionales_Ver2>();            
            try
            {
                CN_CapRepValuacionParams CN = new CN_CapRepValuacionParams();
                result.Datos = CN.SpCrmOportunidades_RepValuacionProp_Ver2(Sesion.Id_Emp, Sesion.Id_Cd, Id_op, Sesion.Emp_Cnx);
                
                result.Estado = 1;
            }
            catch (Exception ex)
            {
                result.Datos = null;
                result.Estado = -1;
            }
            return result;
        }


        //
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