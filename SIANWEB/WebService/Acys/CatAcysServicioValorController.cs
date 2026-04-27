using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaEntidad;
using CapaNegocios;

namespace SIANWEB.WebService
{
    public class CatAcysServicioValorController : ApiController
    {

        [HttpGet]
        public List<eCapAcys2_ServicioValor> Get(int IdAcys, int AcsVersion)
        {
            List<eCapAcys2_ServicioValor> Lst = new List<eCapAcys2_ServicioValor>();
            try            
            {
                CN_CapAcys2_ServicioValor cn_capacys = new CN_CapAcys2_ServicioValor();
                Lst = cn_capacys.Consultar_ServicioValor(Sesion.Id_Emp, Sesion.Id_Cd, IdAcys, AcsVersion, Sesion.Emp_Cnx);
            }
            catch (Exception ex)
            {
                Lst = null;
            }
            return Lst;
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