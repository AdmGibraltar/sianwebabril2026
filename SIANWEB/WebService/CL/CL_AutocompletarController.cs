using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaEntidad;
using CapaNegocios;
using CapaModelo;

namespace SIANWEB.WebService
{
    public class CL_AutocompletarController : ApiController
    {

        [HttpGet]
        public eResponse<List<CapaEntidad.eListaGenerica>> GetAutocomplete_ProductName(string Termino)
        {
            eResponse<List<CapaEntidad.eListaGenerica>> result = new eResponse<List<CapaEntidad.eListaGenerica>>();
            List<CapaEntidad.eListaGenerica> lst = new List<CapaEntidad.eListaGenerica>();

            result.Estado = 0;
            result.Mensaje = "";

            try
            {
                CN_Compras_Locales CN = new CN_Compras_Locales();
                lst = CN.Get_ListadoCausaDesabasto(0, Sesion.Emp_Cnx);

                result.Datos = lst;
                result.Estado = 1;
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