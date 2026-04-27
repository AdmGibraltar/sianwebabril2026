using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaEntidad;
using CapaNegocios;

namespace SIANWEB.WebService
{
    public class CatProductoConsultaController : ApiController
    {
        [HttpGet]

        public eResponse<Producto> Get(Int64 Id_Prd)
        {
            eResponse<Producto> result = new eResponse<Producto>();
            result.Estado = 0;
            result.Mensaje = "";
            result.Datos = null;

            Producto P = new Producto();
            try
            {
                CN_CatProducto cnProducto = new CN_CatProducto();
                cnProducto.Consulta_Producto(ref P, Sesion.Emp_Cnx, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Id_Cd_Ver, Id_Prd, 0);
                result.Datos = P;
                result.Estado = 1;
            }
            catch (Exception ex)
            {
                result.Estado = -1;
                result.Datos = null;
                result.Mensaje = ex.Message.ToString();
            }
            return result;
        }

        public eResponse<Producto> Get(int Id_Cte, Int64 Id_Prd)
        {
            eResponse<Producto> result = new eResponse<Producto>();
            result.Estado = 0;
            result.Mensaje = "";
            result.Datos = null;

            Producto P = new Producto();
            try
            {
                CN_CatProducto cnProducto = new CN_CatProducto();
                P.Id_Cte = Id_Cte;
                cnProducto.Consulta_Producto(ref P, Sesion.Emp_Cnx, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Id_Cd_Ver, Id_Prd, 0);
                result.Datos = P;
                result.Estado = 1;
            }
            catch (Exception ex)
            {
                result.Estado = -1;
                result.Datos = null;
                result.Mensaje = ex.Message.ToString();
            }
            return result;
        }


        /// <summary>
        /// Se refiera a una consulta de paginacion se especifica el tamaño del segmento y en no 
        /// * de segmento, regresa un listado.
        /// 25 JUN 2019 RFH
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageNumber"></param>
        /// <returns></returns>
        /// 
        /*
        [HttpGet]
        public eResponse<List<Producto>> Consulta_Segmento(int PageSize, int PageNumber)
        {
            eResponse<List<Producto>> result = new eResponse<List<Producto>>();
            result.Estado = 0;
            result.Mensaje = "";
            result.Datos = null;
            List<Producto> P = new List<Producto>();
            try
            {
                CN_CatProducto cnProducto = new CN_CatProducto();
                P = cnProducto.spCatProducto_Paginacion(Sesion.Id_Emp, Sesion.Id_Cd, PageSize, PageNumber, Sesion.Emp_Cnx);
                result.Datos = P;
                result.Estado = 1;
            }
            catch (Exception ex)
            {
                result.Estado = -1;
                result.Datos = null;
                result.Mensaje = ex.Message.ToString();
            }
            return result;
        }
        */

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