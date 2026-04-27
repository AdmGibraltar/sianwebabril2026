using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaModelo;
using CapaNegocios;
using CapaEntidad;
using System.Net.Http;
using SIANWEB.Core.Web.API;

//JUN02-2020 RFH Actualizado

namespace SIANWEB.WebService.PortalRIK
{
    /// <summary>
    /// Controlador web del api para el catálogo CatProducto de SianWeb.
    /// </summary>
    public class CatProductoController 
        : BaseWebAPIController
    {
        /// <summary>
        /// Obtiene la entrada del producto con Id_Prd igual a id.
        /// </summary>
        /// <param name="id">Identificador del producto</param>
        /// <returns>CatProducto</returns>
        [HttpGet]
        public HttpResponseMessage Get(Int64 id)
        {
            CatProducto catProducto = null;
            try
            {
                using (IBusinessTransaction ibt = CN_FabricaTransaccionNegocios.Default(Sesion))
                {
                    ibt.Begin();
                    CN_CatProducto cnCatProducto = new CN_CatProducto();
                    catProducto = cnCatProducto.ObtenerPorId(Sesion, id, ibt);
                    return Request.CreateResponse(System.Net.HttpStatusCode.OK, catProducto);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("Se ha producido un error al obtener el producto {0}", id), ex);
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, string.Format("Se ha producido un error al obtener el producto {0}", id),  ex);
            }
        }

        // JUN01-2020 RFH
        // CRM Consulta Producto / Si importar la Aplicacion
        
        [HttpGet]
        public eResponse<CapaEntidad.Producto> ConsultaProductoById(long Id_Prd)
        {
            eResponse<CapaEntidad.Producto> result = new eResponse<CapaEntidad.Producto>();
            CapaEntidad.Producto Prd = new CapaEntidad.Producto();
            result.Estado = 0;
            result.Mensaje = "";

            try
            {
                CN_CatProducto CN = new CN_CatProducto();
                CN.ConsultaProducto(ref Prd, Sesion.Emp_Cnx, 
                    Sesion.Id_Emp, Sesion.Id_Cd, Sesion.Id_Cd_Ver, Id_Prd, false);

                result.Datos = Prd;
                if (Prd.Id_Prd > 0 && Prd.Prd_Descripcion.Length > 0)
                {
                    result.Estado = 1;
                }
                else
                {
                    result.Estado = 2;
                }
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


    }
}