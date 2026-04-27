using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaEntidad;
using CapaNegocios;
using CapaDatos;
using CapaModelo;
using SIANWEB.WebAPI.Models;
using System.Net.Http;
using Newtonsoft.Json;
using SIANWEB.Core.Web.API;
using System.Threading.Tasks;

namespace SIANWEB.WebService.PortalRIK.GestionPromocion.Proyectos
{
    public class CancelarProyectoController: ApiController
    {
        /*
        [HttpPut]
        public Task<HttpResponseMessage> Put([FromBody] CancelarProyectoPutModel model)
        {
            try
            {
                HttpContext current = HttpContext.Current;
                var t = Task<HttpResponseMessage>.Factory.StartNew(() =>
                {
                    HttpContext.Current = current;
                    try
                    {
                        using (IBusinessTransaction ibt = CN_FabricaTransaccionNegocios.Default(Sesion))
                        {
                            ibt.Begin();
                            CN_CrmOportunidad cnCrmOportunidad = new CN_CrmOportunidad();
                            cnCrmOportunidad.Cancelar(Sesion, model.IdCte, model.IdOp, model.IdCausa, ibt);
                            ibt.Commit();
                            return Request.CreateResponse(System.Net.HttpStatusCode.OK);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("CancelarProyectoController::Get->inside task", ex);
                        return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
                    }

                });
                return t;
            }
            catch (Exception ex)
            {
                Logger.Error("CancelarProyectoController::Get", ex);
                return Task<HttpResponseMessage>.Factory.StartNew(() =>
                {

                    return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
                });
            }
        }
        */


    /// <summary>
    /// CRM - Cancelar proyeto via ejecucion de SP
    /// </summary>
        [HttpGet]
        public eResponse<int> spCRM_CrmOportunidades_Cancelar(int Id_Op, int Id_Cte, int Id_Causa)
        {
            eResponse<int> result = new eResponse<int>();
            result.Estado = 0;
            int iResultado=0; 

            try
            {   
                CN_CrmOportunidad CN_O = new CN_CrmOportunidad();
                iResultado = CN_O.CrmOportunidades_Cancelar(
                    Sesion.Id_Emp, Sesion.Id_Cd, Id_Op, Id_Cte, Id_Causa, Sesion.Emp_Cnx);
                
                result.Estado = 1;
                result.Datos = iResultado;
            }
            catch (Exception ex)
            {
                result.Estado = 0;
                result.Datos = -1;
            }

            return result;
        }


        /// <summary>
        /// Sesión del usuario actual
        /// </summary>
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

    public class CancelarProyectoPutModel
    {
        public int IdCte
        {
            get;
            set;
        }

        public int IdOp
        {
            get;
            set;
        }

        public int IdCausa
        {
            get;
            set;
        }
    }
}