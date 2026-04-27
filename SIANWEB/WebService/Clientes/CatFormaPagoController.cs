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
    // 19AGO-2021 RFH
    public class CatFormaPagoController : ApiController
    {
        [HttpGet]
        public eResponse<List<CapaEntidad.CatFormaPago>> CatClienteFPago_Consultar(int Id_Cte)
        {
            eResponse<List<CapaEntidad.CatFormaPago>> result = new eResponse<List<CapaEntidad.CatFormaPago>>();
            result.Estado = 0;
            result.Mensaje = "";
            CatFormaPago APer = new CatFormaPago();
            List<CapaEntidad.CatFormaPago> lst = new List<CapaEntidad.CatFormaPago>();
            try
            {
                CN_CatCliente CN_Cte = new CN_CatCliente();
                lst = CN_Cte.CatClienteFPago_Consultar(Sesion.Id_Emp, Sesion.Id_Cd, Id_Cte, Sesion.Emp_Cnx);
                result.Estado = 1;
                if (lst == null)
                {
                    result.Estado = -1;
                }
                result.Datos = lst;
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