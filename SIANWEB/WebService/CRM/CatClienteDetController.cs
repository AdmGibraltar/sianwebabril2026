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

namespace SIANWEB.WebService.PortalRIK
{
    public class CatClienteDetController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Post([FromBody] WebAPI.Models.Post.CatClienteDet data)
        {
            try
            {
                CN_CatClienteDet cnCatClienteDet = new CN_CatClienteDet(Sesion);
                var result = cnCatClienteDet.CrearNuevo(Sesion, data.IdCte, data.IdRik, data.IdTer, data.IdSeg, data.VPO);

                //IG ASIGNAR TERRITORIO
                CN_VinculacionCRM CN = new CN_VinculacionCRM();
                int resultUpdateTerr = CN.ActualizarTerritorioOpo(Sesion.Id_Cd, data.IdCte, data.IdTer, data.IdRik, Sesion.Emp_Cnx);

                return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// Devuelve los territorios asociados a un cliente.
        /// Todo * ENTITY FRAMEWORK *
        /// </summary>
        /// <param name="idCte">Identificador del cliente</param>
        /// <returns>IEnumerable(CatClienteDet). Conjunto de territorios asociados a un cliente.</returns>

        [HttpGet]
        public HttpResponseMessage Get(int idCte)
        {
            try
            {
                CN_CatClienteDet cnCatClienteDet = new CN_CatClienteDet(Sesion);
                var resultado = cnCatClienteDet.ObtenerPorCliente(Sesion, idCte);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, resultado);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int idCte, int idTer)
        {
            try
            {
                CN_CatClienteDet cnCatClienteDet = new CN_CatClienteDet(Sesion);
                cnCatClienteDet.Remover(Sesion, idCte, idTer);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }

        //
        // 14 Feb 2019
        // CRM 2 - Eliminar TERRITORIO
        //

        [HttpDelete]
        public eResponse<List<int>> ELIMINAR_TERRITORIOS_XCTE(int Id_Cte, int Id_Ter)
        {
            eResponse<List<int>> result = new eResponse<List<int>>();
            List<eTerritoriosCte> lst = new List<eTerritoriosCte>();
            result.Estado = 0;
            result.Mensaje = "";
            try
            {
                CN_CatClienteDet cnCatClienteDet = new CN_CatClienteDet(Sesion);
                cnCatClienteDet.Remover(Sesion, Id_Cte, Id_Ter);
                result.Estado = 1;
                result.Datos = null;
            }
            catch (Exception ex)
            {
                result.Estado = -1;
                result.Mensaje = ex.Message.ToString();
            }
            return result;
        }

        //
        // 12 Feb 2019
        // CRM 2 - CONSULTA TERRITORIOS POR CLIENTE
        //

        [HttpGet]
        public eResponse<List<eTerritoriosCte>> CONSULTAR_TERRITORIOS_XCTE(int Id_Cte, int Id_Rik)
        {
            Clientes C = new Clientes();
            eResponse<List<eTerritoriosCte>> result = new eResponse<List<eTerritoriosCte>>();
            List<eTerritoriosCte> lst = new List<eTerritoriosCte>();
            result.Estado = 0;
            result.Mensaje = "";
            try
            {
                CN_CatClienteDet cn = new CN_CatClienteDet(null);
                //lst = cn.ObtenerTerritoriosPorCte(Sesion.Id_Emp, Sesion.Id_Cd, Id_Cte, Sesion.Emp_Cnx);
                ///AQUI
                ///
                /*if (Sesion.Id_TU==3)
                {
                    lst = cn.ObtenerTerritoriosPorCteYRik(Sesion.Id_Emp, Sesion.Id_Cd, Id_Cte, 869, Sesion.Emp_Cnx);
                } else
                {*/
                lst = cn.ObtenerTerritoriosPorCteYRik(Sesion.Id_Emp, Sesion.Id_Cd, Id_Cte, Sesion.Id_Rik, Sesion.Emp_Cnx);
                //}
                result.Estado = 1;
                result.Datos = lst;
            }
            catch (Exception ex)
            {
                result.Estado = 0;
                result.Mensaje = ex.Message.ToString();
                result.Datos = null;
            }
            return result;
        }

        /// <summary>
        /// Devuelve los territorios asociados a un cliente.
        /// Todo * STORED PROCEDURE *        
        // 5 Nov 2018 RFH

        [HttpGet]
        public eResponse<List<eTerritoriosCte>> Buscar_TerritoriosCte(int Id_Cte)
        {
            Clientes C = new Clientes();
            eResponse<List<eTerritoriosCte>> result = new eResponse<List<eTerritoriosCte>>();
            List<eTerritoriosCte> lst = new List<eTerritoriosCte>();
            result.Estado = 0;
            result.Mensaje = "";
            try
            {
                CN_CatClienteDet cn = new CN_CatClienteDet(null);
                lst = cn.ObtenerTerritoriosPorCte(Sesion.Id_Emp, Sesion.Id_Cd, Id_Cte, Sesion.Emp_Cnx);

                result.Estado = 1;
                result.Datos = lst;
            }
            catch (Exception ex)
            {
                result.Estado = 0;
                result.Mensaje = ex.Message.ToString();
                result.Datos = null;
            }
            return result;
        }

        // CRM3 - 16 Ene 2019 RFH
        // Obtiene Listado de territorios del cliente union territorios del rik.

        [HttpGet]
        public eResponse<List<eTerritoriosCte>> Buscar_TerritoriosCteYRik(int Id_Rik, int Id_Cte, int Id_Crm)
        {
            Clientes C = new Clientes();
            eResponse<List<eTerritoriosCte>> result = new eResponse<List<eTerritoriosCte>>();
            List<eTerritoriosCte> lst = new List<eTerritoriosCte>();
            result.Estado = 0;
            result.Mensaje = "";
            try
            {
                CN_CatClienteDet cn = new CN_CatClienteDet(null);
                lst = cn.ObtenerTerritoriosPorCteYRik(Sesion.Id_Emp, Sesion.Id_Cd, Id_Cte, Id_Rik, Sesion.Emp_Cnx);

                result.Estado = 1;
                result.Datos = lst;
            }
            catch (Exception ex)
            {
                result.Estado = 0;
                result.Mensaje = ex.Message.ToString();
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
    }
}