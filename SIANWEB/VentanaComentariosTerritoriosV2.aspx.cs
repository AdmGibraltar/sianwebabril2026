using System;
using System.Web.UI;
using System.Web;
using System.Web.Services;

namespace SIANWEB
{
    public partial class VentanaComentariosTerritoriosV2 : Page
    {
        static string Id_Ter
        {
            get { return HttpContext.Current.Session["vmtv2_terId"].ToString(); }
            set { HttpContext.Current.Session["vmtv2_terId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["vmtv2_terId"] = null;
            Id_Ter = Request.QueryString["Id_Ter"];
        }

        public class ResponseJson
        {
            public bool Status { get; set; }
        }

        [WebMethod]
        public static ResponseJson GuardarComentario(string comentario)
        {
            HttpContext.Current.Session["Comentarios" + HttpContext.Current.Session.SessionID] = comentario;
            HttpContext.Current.Session["Territorio" + HttpContext.Current.Session.SessionID] = Id_Ter;
            return new ResponseJson
            {
                Status = true
            };
        }
    }
}