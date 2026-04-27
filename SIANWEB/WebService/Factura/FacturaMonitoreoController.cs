using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaEntidad;
using CapaNegocios;
using System.Net.Http;
using System.Diagnostics;
using System.Configuration;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SIANWEB.WebService
{
    public class FacturaMonitoreoController : ApiController
    {

        [HttpGet]
        public HttpResponseMessage prueba(string strMensaje)
        {
            Debug.WriteLine("Entro post" + strMensaje);

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, strMensaje);
        }

        [HttpPost]
        public HttpResponseMessage ConsultarFacturas()
        {
            object objResult = new object();
            CN_CapFactura cnFactura = new CN_CapFactura();
            FacturaLite entFacturaStatus = new FacturaLite();
            //int intResultado = 0;
            string strMsjResultado = string.Empty;
            List<FacturaLite> lstFacturaLite = new List<FacturaLite>();
            try
            {
                string strPath = ConfigurationManager.AppSettings["AccesoPortal"];
                entFacturaStatus.id_Emp = Sesion.Id_Emp;
                entFacturaStatus.id_Cd = Sesion.Id_Cd;
                cnFactura.ConsultaFacturaSegundoPlano(Sesion.Emp_Cnx, Sesion.Id_U, strPath, ref entFacturaStatus, ref strMsjResultado, ref lstFacturaLite);

                objResult = new
                {
                    intActivo = entFacturaStatus.Activo,
                    strRespuesta = strMsjResultado,
                    dataResultado = lstFacturaLite.ToList()
                };

                return Request.CreateResponse(System.Net.HttpStatusCode.OK, objResult);
            }
            catch (Exception ex)
            {
                strMsjResultado = ex.ToString();

                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, strMsjResultado);
            }


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