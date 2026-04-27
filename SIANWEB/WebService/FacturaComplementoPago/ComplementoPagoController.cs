using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaEntidad;
using CapaNegocios;
using System.Net.Http;
using SIANWEB.WebAPI.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;


namespace SIANWEB.WebService
{
    [AllowAnonymous]
    public class ComplementoPagoController : ApiController
    {

        [HttpPost]
        public HttpResponseMessage Pagoprueba(string strMensaje)
        {
            Debug.WriteLine("Entro post" + strMensaje);

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, strMensaje);
        }

        [HttpPost]
        public HttpResponseMessage ConsultarPago(int IntPago)
        {
            object objResult = new object();
            CN_CapPago cnPago = new CN_CapPago();
            FacturaLite entFacturaStatus = new FacturaLite();
            //int intResultado = 0;
            string strMsjResultado = string.Empty;
            string strMsjMsjAlert = string.Empty;
            string strMsjScript = string.Empty;

            try
            {
                if (IntPago != 0)
                {
                    //var sesion = JsonConvert.DeserializeObject<Sesion>(entFacturaStatus.strSesion);
                    entFacturaStatus.id_Pag = IntPago;

                    cnPago.ConsultaPagoSegundoPlano(Sesion.Emp_Cnx, ref entFacturaStatus, ref strMsjResultado);

                    //intResultado = entFacturaStatus.Activo;

                    if (!string.IsNullOrEmpty(entFacturaStatus.strRstAlerta))
                    {
                        strMsjMsjAlert = entFacturaStatus.strRstAlerta;
                    }

                    if (!string.IsNullOrEmpty(entFacturaStatus.strRstScript))
                    {
                        strMsjScript = entFacturaStatus.strRstScript;
                    }


                    objResult = new
                    {
                        intActivo = entFacturaStatus.Activo,
                        intNotificado = entFacturaStatus.Notificado,
                        strRespuesta = strMsjResultado,
                        srlScript = strMsjScript,
                        rslAlert = strMsjMsjAlert
                    };
                }
                else
                {
                    objResult = new
                    {
                        intActivo = 0,
                        intNotificado = 0,
                        strRespuesta = strMsjResultado,
                        srlScript = "",
                        rslAlert = ""
                    };
                }
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, objResult);
            }
            catch (Exception ex)
            {
                strMsjResultado = ex.ToString();

                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, strMsjResultado);
            }


        }
        [HttpPost]
        public HttpResponseMessage PagoSegundoPlano([FromBody] List<FacturaLite> lstFacturasPago)
        {
            Debug.WriteLine("entro post");
            string strAlerta = string.Empty;
            string strResponseScripts = string.Empty;
            // error return Request.CreateErrorResponse((HttpStatusCode)510, "No se regresó información de la base de datos");
            try
            {
                var sesion = JsonConvert.DeserializeObject<Sesion>(lstFacturasPago.Select(x => x.strSesion).FirstOrDefault());

                Task.Run(async () =>
                {
                    Debug.WriteLine("entro a tarea segundo plano");
                    //Thread.Sleep(20000);
                    //await Task.Delay(5000);
                    CapPagosTimbres capPagosTimbres = new CapPagosTimbres();
                    capPagosTimbres.PrepararPago(sesion, lstFacturasPago, true, ref strAlerta, ref strResponseScripts);
                });
                string strMensje = "Se están generando los complementos de pago correspondientes en segundo plano debido a la cantidad de documentos seleccionados, al finalizar el proceso la página se actualizará en automático, esto puede tomar un tiempo.";

                return Request.CreateResponse(System.Net.HttpStatusCode.OK, strMensje);
            }
            catch (Exception ex)
            {
                CN_CapPago cnPago = new CN_CapPago();
                FacturaLite entFacturaLite = lstFacturasPago.First();
                entFacturaLite.strRstAlerta = ex.Message;
                entFacturaLite.strRstScript = ex.StackTrace.ToString();
                cnPago.DesactivaPagoSegundoPlano("Data Source=13.84.160.245;Initial catalog=sianwebmty;User ID=sa;Password=K3yQuimica10803!", entFacturaLite);
                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, ex);
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