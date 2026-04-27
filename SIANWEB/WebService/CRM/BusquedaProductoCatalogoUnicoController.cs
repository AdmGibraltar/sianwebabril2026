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
    public class BusquedaProductoCatalogoUnicoController
        : BaseWebAPIController
    {
        /// <summary>
        /// Regresa el producto del catálogo único dado que este exista en el mapa de oferta para la aplicación asociada al proyecto idOp.
        /// </summary>
        /// <param name="idCte">Identificador del cliente</param>
        /// <param name="idOp">Identificador del proyecto que tiene asociado la aplicacion que contiene al producto idPrd</param>
        /// <param name="idPrd">Identificador del producto</param>
        /// <returns>Task[HttpResponseMessage]</returns>
        [HttpGet]
        public Task<HttpResponseMessage> Get(int idCte, int idOp, Int64 idPrd)
        {
            try
            {
                HttpContext current = HttpContext.Current;
                var t = Task<HttpResponseMessage>.Factory.StartNew(() =>
                    {
                        HttpContext.Current = current;
                        CapAplicacionProducto ret = null;
                        try
                        {
                            using (IBusinessTransaction ibt = CN_FabricaTransaccionNegocios.Default(Sesion))
                            {
                                ibt.Begin();
                                CN_CapAplicacionProducto cnCapAplicacionProducto = new CN_CapAplicacionProducto();
                                var productoCU = cnCapAplicacionProducto.ObtenerPorProyectoYProducto(Sesion, idCte, idOp, idPrd, ibt);
                                if (productoCU.Count() > 0)
                                {
                                    var forceProducto=productoCU.First().CatProductoSerializable;
                                    return Request.CreateResponse(System.Net.HttpStatusCode.OK, productoCU.First());
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.Error("BusquedaProductoCatalogoUnicoController::Get->inside task", ex);
                            return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                        return Request.CreateResponse(System.Net.HttpStatusCode.OK, ret);
                        
                    });
                return t;
            }
            catch (Exception ex)
            {
                Logger.Error("BusquedaProductoCatalogoUnicoController::Get", ex);
                return Task<HttpResponseMessage>.Factory.StartNew(() =>
                    {
                        
                        return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
                    });
            }
        }
        
        // JUN03-2020 RFH
        // Remplaza la funcion arriba
        // CRM - Proyecto - Busqueda Producto 

        [HttpGet]
        public eResponse<CapaEntidad.Producto> CRM_BusquedaProducto(
            int Id_Cte, int Id_Op, long Id_Prd, int Tipo)
        {
            eResponse<CapaEntidad.Producto> result = new eResponse<CapaEntidad.Producto>();
            CapaEntidad.Producto Prd = new CapaEntidad.Producto();
            result.Estado = 0;
            result.Mensaje = "";

            try
            {
                CN_CatProducto CN = new CN_CatProducto();

                Prd = CN.CRM_BusquedaProducto(
                Sesion.Id_Emp, Sesion.Id_Cd, Id_Cte, Id_Op, Id_Prd, Tipo, Sesion.Emp_Cnx);

                if (Prd != null)
                {
                    result.Estado = 1;
                }
                else 
                {
                    result.Estado =2; // No encontrado
                }
                result.Datos = Prd;                
            }
            catch
            {
                result.Datos = null;
                result.Estado = -1;
                result.Mensaje = "Error";
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