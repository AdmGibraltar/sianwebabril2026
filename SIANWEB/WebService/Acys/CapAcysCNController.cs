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

namespace SIANWEB.WebService
{
    public class CapAcysCNController : ApiController
    {
        [HttpGet]
        public eResponse<eAcysCN_Permisos> spAcysCN_Permisos(
            int Id_Cte, int Id_Acs, int AcsVersion, int Param1)
        {
            eResponse<eAcysCN_Permisos> result = new eResponse<eAcysCN_Permisos>();
            result.Estado = 0;
            result.Mensaje = "";

            eAcysCN_Permisos APer = new eAcysCN_Permisos();

            try
            {
                int Estado = 0;
                string Mensaje = "";

                CN_CapAcysCN CN_AcysCN = new CN_CapAcysCN();
                APer = CN_AcysCN.spAcysCN_Permisos(
                    ref Estado, ref Mensaje, Sesion.Id_Cd, Id_Cte, Sesion.Emp_Cnx);

                result.Estado = 1;
                result.Datos = APer;
            }
            catch (Exception ex)
            {
                result.Estado = -1;
                result.Datos = null;
                result.Mensaje = ex.ToString();
            }
            return result;
        }

        [HttpGet]
        public eResponse<List<eAcysDet2>> spAcysCN_Productos(
            int Id_Cte, int Id_Acs, int AcsVersion, int Param2)
        {
            eResponse<List<eAcysDet2>> result = new eResponse<List<eAcysDet2>>();
            result.Estado = 0;
            result.Mensaje = "";

            List<eAcysDet2> Lst = new List<eAcysDet2>();

            try
            {
                int Estado = 0;
                string Mensaje = "";

                CN_CapAcysCN CN_AcysCN = new CN_CapAcysCN();
                Lst = CN_AcysCN.spAcysCN_Productos(
                    ref Estado, ref Mensaje, Sesion.Id_Cd, Id_Cte,Id_Acs, Sesion.Emp_Cnx);

                result.Estado = Estado;
                result.Datos = Lst;
            }
            catch (Exception ex)
            {
                result.Estado = -1;
                result.Datos = null;
                result.Mensaje = ex.ToString();
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