using System;
using System.Web;
using System.Web.Services;

namespace SIANWEB
{
    public partial class AutorizacionVinculacionV2 : System.Web.UI.Page
    {
        public string RFC { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            RFC = Request.QueryString["rfc"];
        }

        public class ResponseJson
        {
            public bool Status { get; set; }
        }

        [WebMethod]
        public static ResponseJson GuardarValor(int valor)
        {
            HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID + "autorizacionVinculacion"] = valor;
            return new ResponseJson
            {
                Status = true
            };
        }
    }
}