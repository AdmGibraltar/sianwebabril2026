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
    [AllowAnonymous]
    public class FacturaTimbreTaskController : ApiController
    {

        [HttpGet]
        public HttpResponseMessage prueba(string strMensaje)
        {
            Debug.WriteLine("Entro post" + strMensaje);

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, strMensaje);
        }

        [HttpPost]
        public HttpResponseMessage TimbrarFacturas([FromBody] List<string> lstDatos)
        {
            string strFacturaPendiente = string.Empty;
            string strFnScript = string.Empty;
            string strFnScriptSP = string.Empty;
            string strUrlPdf = string.Empty;
            string strUrlPdfNC = string.Empty;
            string strErrorMensaje = string.Empty;
            string strErrorDetalle = string.Empty;
            int idGrupoFac = 0;

            var lstFactura = JsonConvert.DeserializeObject<List<Factura>>(lstDatos[0]);
            Sesion entSesion = new Sesion();

            entSesion.Emp_Cnx = lstDatos[1];
            entSesion.Id_Cd_Ver = int.Parse(lstDatos[2]);
            entSesion.Id_Emp = int.Parse(lstDatos[3]);
            entSesion.Id_U = int.Parse(lstDatos[4]);
            idGrupoFac = int.Parse(lstDatos[5]);

            CN_CapFactura cnFactura = new CN_CapFactura();
            SIANWEB.CapFactura_Lista cpFactura = new SIANWEB.CapFactura_Lista();
            Task.Run(async () =>
            {
                foreach (var itemFactura in lstFactura)
                {
                    strFnScriptSP = string.Empty;
                    strUrlPdf = string.Empty;
                    strUrlPdfNC = string.Empty;
                    strErrorMensaje = string.Empty;
                    strErrorDetalle = string.Empty;

                    try
                    {
                        cpFactura.ImprimirFacturaTask(entSesion, itemFactura.Id_Emp, itemFactura.Id_Cd, itemFactura.Id_Fac, itemFactura.Fac_PedNum.ToString(), "FACTURA", "", ref strFnScriptSP, ref strUrlPdf, ref strUrlPdfNC, (bool)itemFactura.PDF, true);
                    }
                    catch (Exception ex)
                    {
                        strErrorMensaje = ex.Message;
                        strErrorDetalle = ex.StackTrace.ToString();
                    }
                    finally
                    {
                        // guardar log final
                        FacturaLite entFacturaLite = new FacturaLite();
                        entFacturaLite.id_Emp = itemFactura.Id_Emp;
                        entFacturaLite.id_Cd = itemFactura.Id_Cd;
                        entFacturaLite.id_Fac = itemFactura.Id_Fac;
                        entFacturaLite.strRstScript = strFnScriptSP;
                        cnFactura.DesactivaFacturaSegundoPlano(entSesion.Emp_Cnx, ref entFacturaLite, strUrlPdf, strErrorMensaje, strErrorDetalle, idGrupoFac);
                    }
                }
            });

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, "ok");
        }

    }
}