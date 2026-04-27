using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaEntidad;
using CapaNegocios;
using System.Net.Http;

namespace SIANWEB.WebService
{
    public class FacturaController: ApiController
    {

        // ABR16-2020 RFH
        // PROBABLEMENTE NO SE SE UTILICE 

        /*[HttpGet]
        public eResponse<int> Consulta_ValidacionFacturas()
        {
            eResponse<int> result = new eResponse<int>();
            result.Estado = 0;
            result.Mensaje = "";
            try
            {
                eDocumentosSinTimbar DST = new eDocumentosSinTimbar();

                int verificador = 0;                
                CN_CapFactura CN = new CN_CapFactura();
                DST = CN.Consulta_ValidacionFacturas(
                    Sesion.Id_Emp,
                    Sesion.Id_Cd,
                    Sesion.Emp_Cnx);

                result.Datos = verificador;
                result.Estado = 1;
            }
            catch (Exception ex) //manejo de error genérico
            {
                result.Estado = -1;
                result.Mensaje = ex.ToString();
            }
            return result;
        }*/

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