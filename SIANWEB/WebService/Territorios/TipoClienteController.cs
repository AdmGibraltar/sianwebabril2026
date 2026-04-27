using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaEntidad;
using CapaNegocios;
using CapaModelo;

namespace SIANWEB.WebService.Territorios
{
    public class TipoClienteController : ApiController
    {
        [HttpGet]
        public eResponse<List<CapaEntidad.TipoCliente>> Get()
        {
            eResponse<List<CapaEntidad.TipoCliente>> result = new eResponse<List<CapaEntidad.TipoCliente>>();
            List<CapaEntidad.TipoCliente> lst = new List<CapaEntidad.TipoCliente>();
            result.Estado = 0;

            int verificador=0;

            try
            {
                CN_CatTipoCliente CN = new CN_CatTipoCliente();

                lst = CN.SelAll(Sesion.Id_Emp, Sesion.Emp_Cnx, ref verificador);

                result.Datos = lst;
                result.Estado = 1;
            }
            catch
            {
                result.Datos = null;
                result.Estado = 1;
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