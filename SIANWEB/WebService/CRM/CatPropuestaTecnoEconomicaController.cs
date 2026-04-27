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
    public class CatPropuestaTecnoEconomicaController: ApiController
    {
        
        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
        // Detalle
        [HttpGet]
        public List<CapaEntidad.ePropuestaTecnoEconomicaDetalle> Get(
            int CRM_Usuario_Rik, int Id_Op, int Id_Cte, int Id_Val)
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
            List<CapaEntidad.ePropuestaTecnoEconomicaDetalle> lst = new List<CapaEntidad.ePropuestaTecnoEconomicaDetalle>();
            CN_CrmPropuestaEconomica cnPE = new CN_CrmPropuestaEconomica();
            lst = cnPE.CRM_ObtenerPropuestaEconomica(Sesion.Id_Emp, Sesion.Id_Cd, Id_Cte, Rik,Id_Op, Id_Val, Sesion);
            return lst;
        }

        // Detalle de Propuesta Tecno Economica - Ver 2
        [HttpGet]
        public eResponse<List<CapaEntidad.ePropuestaTecnoEconomicaDetalle>> CRM_ObtenerPropuestaEconomica_Ver2(
            int CRM_Usuario_Rik, int Id_Op, int Id_Cte, int Id_Val, int Id_Ptp)
        {
            eResponse<List<CapaEntidad.ePropuestaTecnoEconomicaDetalle>> result = new eResponse<List<CapaEntidad.ePropuestaTecnoEconomicaDetalle>>();
            result.Estado = 0;
            result.Mensaje = "Ejecucion de CRM_ObtenerPropuestaEconomica_Ver2";
            result.Datos = null;

            try
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
                List<CapaEntidad.ePropuestaTecnoEconomicaDetalle> lst = new List<CapaEntidad.ePropuestaTecnoEconomicaDetalle>();
                CN_CrmPropuestaEconomica cnPE = new CN_CrmPropuestaEconomica();
                lst = cnPE.CRM_ObtenerPropuestaEconomica_Ver2(
                    Sesion.Id_Emp, Sesion.Id_Cd, Id_Cte, Rik, Id_Op, Id_Val, Id_Ptp, Sesion);
                
                result.Estado = 1;
                result.Datos = lst;

            }
            catch (Exception ex)
            {
                result.Estado = -1;
                result.Datos = null;
                result.Mensaje = ex.Message;
            }
            return result;
        }

        // Detalle de Propuesta Tecno Economica - Ver 3 14AGO-2020 RFH
        [HttpGet]
        public eResponse<List<CapaEntidad.ePropuestaTecnoEconomicaDetalle>> CRM_ObtenerPropuestaEconomica_Ver3(
            int CRM_Usuario_Rik, int Id_Op, int Id_Cte, int Id_Val, int Id_Ptp, int ParamVer3)
        {
            eResponse<List<CapaEntidad.ePropuestaTecnoEconomicaDetalle>> result = new eResponse<List<CapaEntidad.ePropuestaTecnoEconomicaDetalle>>();
            result.Estado = 0;
            result.Mensaje = "Ejecucion de CRM_ObtenerPropuestaEconomica_Ver3";
            result.Datos = null;

            try
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
                List<CapaEntidad.ePropuestaTecnoEconomicaDetalle> lst = new List<CapaEntidad.ePropuestaTecnoEconomicaDetalle>();
                CN_CrmPropuestaEconomica cnPE = new CN_CrmPropuestaEconomica();
                lst = cnPE.CRM_ObtenerPropuestaEconomica_Ver3(
                    Sesion.Id_Emp, Sesion.Id_Cd, Id_Cte, Rik, Id_Op, Id_Val, Id_Ptp, Sesion);

                result.Estado = 1;
                result.Datos = lst;

            }
            catch (Exception ex)
            {
                result.Estado = -1;
                result.Datos = null;
                result.Mensaje = ex.Message;
            }
            return result;
        }


        // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\                        
        [HttpGet]
        public int Get(
            int Id_Op, int Id_Val, int Id_Cte, long Id_Prd, decimal Cantidad, 
            int AplDilucion, decimal DilucionA, decimal DilucionC, string CPT_ProductoActual, string CPT_SituacionActual, 
            string CPT_VentajasKey, string CPT_RecursoImagenProductoActual, string CPT_RecursoImagensolucionKey,
            decimal COP_CostoEnUso 
            )
        {

            eResponse<int> result = new eResponse<int>();            
            int Result = 0;

            try
            {   
             
                CN_CrmOportunidadesProductos OP = new CN_CrmOportunidadesProductos ();            
                Result = OP.Update_CrmOportunidadesProductos(
                Sesion.Id_Emp ,Sesion.Id_Cd,
                Id_Op, Id_Val, Id_Cte, Id_Prd, Cantidad, 
                AplDilucion, DilucionA, DilucionC,
                CPT_ProductoActual, CPT_SituacionActual, CPT_VentajasKey, CPT_RecursoImagenProductoActual, CPT_RecursoImagensolucionKey,
                COP_CostoEnUso,
                Sesion.Emp_Cnx);

                result.Estado = 1;
                result.Datos = Result;
            }
            catch (Exception ex)
            {
                result.Estado = -1;
                result.Datos = 0;
                result.Mensaje = ex.ToString();
            }
            
            return Result;
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