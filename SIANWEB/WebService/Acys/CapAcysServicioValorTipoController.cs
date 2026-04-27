using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaEntidad;
using CapaNegocios;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using Telerik.Web.UI;

namespace SIANWEB.WebService
{
    public class CapAcysServicioValorTipoController : ApiController
    {

        [HttpGet]
        public eResponse<List<eCapAcys2_TipoServicio>> Get(int IdTipoServicio)
        {
            eResponse<List<eCapAcys2_TipoServicio>> result = new eResponse<List<eCapAcys2_TipoServicio>>();
            List<eCapAcys2_TipoServicio> lst = new List<eCapAcys2_TipoServicio>();
            result.Estado = 0;
            try
            {
                CN_CapAcys2_ServicioValorTipo CN = new CN_CapAcys2_ServicioValorTipo();
                lst = CN.Consultar_ServicioValorTipo(Sesion.Id_Emp, Sesion.Id_Cd, IdTipoServicio, Sesion.Emp_Cnx);
                result.Datos = lst;
                result.Estado = 1;                
            }
            catch (Exception ex)
            {
                result.Estado = 0;
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
        
        //

    }
}