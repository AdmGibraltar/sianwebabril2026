using CapaDatos;
using CapaEntidad;
using CapaNegocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIANWEB
{
    public partial class ProPlaneacionRepartoFechaEntregaRutaV2 : System.Web.UI.Page
    {
        public static Sesion MySesion { get; set; }
        public static int HF_Ped { get; set; }
        public string Fecha { get; set; }
        public static int Id_Cte { get; set; }
        public string Nom_Cte { get; set; }
        public string Credito { get; set; }
        public List<Comun> Rutas { get; set; }
        public Pedido Peddido { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            MySesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (MySesion == null)
            {
                string script = "<script>closeThisWindow()</script>";
                ScriptManager.RegisterStartupScript(this, GetType(), "closeThisWindow()", script, false);
                return;
            }

            if (!IsPostBack) Inicializar();
        }

        void Inicializar()
        {
            //CargarRutasEntrega();

            var q = Request.QueryString;
            int hf_ped;
            HF_Ped = int.TryParse(q["Id"], out hf_ped) ? hf_ped : 0;
            int id_cte;
            Id_Cte = int.TryParse(q["Id_Cte"], out id_cte) ? id_cte : 0;
            Nom_Cte = q["Nom_Cte"];
            Fecha = q["Fecha"];
            Credito = q["Credito"];

            var pedido = new Pedido
            {
                Id_Emp = MySesion.Id_Emp,
                Id_Cd = MySesion.Id_Cd_Ver,
                Id_Ped = HF_Ped
            };

            var list = new Pedido();
            new CN_CapPedido().ConsultaPedidoDireccionEntrega(pedido, MySesion.Emp_Cnx, ref list);
            Peddido = list;
        }



        public class ResponseAjax
        {
            public string Message { get; set; }
            public bool Status { get; set; }
        }

        [WebMethod]
        public static ResponseAjax ActualizaFechaEntrega(string fechae)
        {
            var response = new ResponseAjax { Status = false };
            MySesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
            
            if (MySesion == null)
            {
                response.Message = "connection close";
                return response;
            }

            var pedido = new Pedido
            {
                Id_Emp = MySesion.Id_Emp,
                Id_Cd = MySesion.Id_Cd_Ver,
                Id_Ped = HF_Ped,
                Id_Cte = Id_Cte,
                Ped_FechaEntrega = DateTime.Parse(fechae)
            };

            var list = new Pedido();
            new CN_CapPedido().ActualizaPedidoFechaEntrega(pedido, MySesion.Emp_Cnx, ref list);
            response.Message = "Fecha de entrega modificada correctamente";
            response.Status = true;
            return response;

            
        }
    }
}