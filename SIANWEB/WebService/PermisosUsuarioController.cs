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
    public class PermisosUsuarioController: ApiController
    {
        //

        [HttpGet]
        public eResponse<CapaEntidad.Permiso> Get(string Pagina)
        {
            eResponse<CapaEntidad.Permiso> result = new eResponse<CapaEntidad.Permiso>();
            CapaEntidad.Permiso Per = new CapaEntidad.Permiso();
            result.Estado = 0;

            try
            {
                CN_PermisosU CN = new CN_PermisosU();
                
                Pagina pagina = new Pagina();
                pagina.Url = Pagina;                
                CN_Pagina CapaNegocio = new CN_Pagina();                
                CapaNegocio.PaginaConsultar(ref pagina, Sesion.Emp_Cnx);
                
                Per.Id_U = Sesion.Id_U;
                Per.Id_Cd = Sesion.Id_Cd;
                Per.Sm_cve = pagina.Clave;
                Per.Menu = "";

                CN.ValidaPermisosUsuario(ref Per, Sesion.Emp_Cnx);

                result.Datos = Per;
                result.Estado = 1;
            }
            catch
            {
                result.Datos = null;
                result.Estado = 1;
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