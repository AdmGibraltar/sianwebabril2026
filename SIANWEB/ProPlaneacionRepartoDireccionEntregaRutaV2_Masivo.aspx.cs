using CapaDatos;
using CapaEntidad;
using CapaNegocios;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.UI;

namespace SIANWEB
{
    public class PedidoCambioRuta
    {
        public int Id_Ped { get; set; }
        public int Id_Cte { get; set; }
        public string Cte_NomComercial { get; set; }
        public string Ruta { get; set; }
    }
    public partial class ProPlaneacionRepartoDireccionEntregaRutaV2_Masivo : System.Web.UI.Page
    {
        public static Sesion MySesion { get; set; }
        public static int HF_Ped { get; set; }
        public string Fecha { get; set; }
        public static int Id_Cte { get; set; }
        public string Nom_Cte { get; set; }
        public string Credito { get; set; }
        public List<Comun> Rutas { get; set; }
        //public Pedido Peddido { get; set; }
        public List<PedidoCambioRuta> Pedidos { get; set; } = new List<PedidoCambioRuta>();
        public List<Comun> Auxiliar { get; set; }

        //[Serializable]
        //public class Pedido
        //{
        //    public int Id_Ped { get; set; }
        //    public int Id_Cte { get; set; }
        //    public string Cte_NomComercial { get; set; }
        //    public string Ruta { get; set; }
        //}
        [System.Web.Services.WebMethod]
        public static void GuardarPedidosEnSession(string pedidos)
        {
            // Deserializar manualmente la lista de pedidos
            var listaPedidos = JsonConvert.DeserializeObject<List<PedidoCambioRuta>>(pedidos);

            HttpContext.Current.Session["PedidosSeleccionados"] = listaPedidos;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            MySesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (MySesion == null)
            {
                string script = "<script>closeThisWindow()</script>";
                ScriptManager.RegisterStartupScript(this, GetType(), "closeThisWindow()", script, false);
                return;
            }
            CargarRutasEntrega();
            //if (!IsPostBack) Inicializar();
            if (!IsPostBack)
            {
                Pedidos = Session["PedidosSeleccionados"] as List<PedidoCambioRuta>;
                CargarAuxiliares();
                hdnClaveRuta.Value = Valor;
                //Inicializar();
            }
            if (IsPostBack)
            {
                string eventTarget = Request["__EVENTTARGET"];
                if (eventTarget == "guardarNuevaRuta")
                {
                    GuardarDesdeHiddenFields();
                }
            }
        }

        //void Inicializar()
        //{
        //    CargarRutasEntrega();



        //    var query = Request.QueryString;
        //    int i = 0;

        //    while (!string.IsNullOrEmpty(query[$"Ids[{i}].Id"]))
        //    {
        //        int idPed = int.TryParse(query[$"Ids[{i}].Id"], out var tmpIdPed) ? tmpIdPed : 0;
        //        int idCte = int.TryParse(query[$"Ids[{i}].Id_Cte"], out var tmpIdCte) ? tmpIdCte : 0;
        //        string nomCte = query[$"Ids[{i}].Nom_Cte"];
        //        string fecha = query[$"Ids[{i}].Fecha"];
        //        string credito = query[$"Ids[{i}].Credito"];
        //        string ruta = query[$"Ids[{i}].Ruta"];

        //        var pedidoBase = new Pedido
        //        {
        //            Id_Emp = MySesion.Id_Emp,
        //            Id_Cd = MySesion.Id_Cd_Ver,
        //            Id_Ped = idPed
        //        };

        //        var pedidoDetalle = new Pedido();
        //        new CN_CapPedido().ConsultaPedidoDireccionEntrega(pedidoBase, MySesion.Emp_Cnx, ref pedidoDetalle);

        //        // Rellenar valores extra del querystring (si no vienen de la BD)
        //        //pedidoDetalle.Ped_Fecha = fecha;
        //        pedidoDetalle.Id_Ped = idPed;
        //        pedidoDetalle.Cte_NomComercial = nomCte;
        //        pedidoDetalle.Id_Cte = idCte;
        //        pedidoDetalle.CreditoStr = credito;
        //        pedidoDetalle.Ruta = ruta;

        //        Pedidos.Add(pedidoDetalle);
        //        i++;
        //    }
        //}
        void CargarAuxiliares()
        {
            try
            {
                var cd = new CD__Comun();
                var auxiliar = new List<Comun>();
                cd.LlenaCombo(MySesion.Id_Emp, MySesion.Id_Cd_Ver, "spCatAuxiliar_Combo", MySesion.Emp_Cnx, ref auxiliar);
                Auxiliar = auxiliar;
            }
            catch { }
        }
        void CargarRutasEntrega()
        {
            var rutas = new List<Comun>();
            new CD__Comun().LlenaCombo(1, MySesion.Id_Emp, MySesion.Id_Cd_Ver, 1, "spCatRutas_Combo", MySesion.Emp_Cnx, ref rutas);
            Rutas = rutas;
        }

        private void GuardarDesdeHiddenFields()
        {
            string clave = HF_ClaveRuta.Value;
            string descripcion = HF_DescripcionRuta.Value;
            bool activa = HF_Activa.Value == "true";
            int auxiliar = Convert.ToInt32(HF_Auxiliar.Value);

            Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
        }
        [System.Web.Services.WebMethod]
        public static string GuardarRuta(string clave, string descripcion, int auxiliar)
        {
            Sesion session = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];

            RutaEntrega ruta = new RutaEntrega
            {
                Id = Convert.ToInt32(clave),
                Descripcion = descripcion,
                Id_Emp = session.Id_Emp,
                Id_Cd = session.Id_Cd_Ver,
                Estatus = true,
                Id_Aux = auxiliar,
                
            };

            CN_CatRutaEntrega clsCatRutaEntrega = new CN_CatRutaEntrega();
            int verificador = -1;
            clsCatRutaEntrega.InsertarRutaEntrega(ruta, session.Emp_Cnx, ref verificador);
            return "Guardado correctamente";
        }
        public class ResponseAjax
        {
            public string Message { get; set; }
            public bool Status { get; set; }
        }
        [WebMethod]
        public static ResponseAjax CambiarRutaEntrega(List<int> pedidosIds, int ruta)
        {
            var response = new ResponseAjax { Status = false };
            var MySesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];

            if (MySesion == null)
            {
                response.Message = "connection close";
                return response;
            }

            foreach (var idPedido in pedidosIds)
            {
                var pedido = new Pedido
                {
                    Id_Emp = MySesion.Id_Emp,
                    Id_Cd = MySesion.Id_Cd_Ver,
                    Id_Ped = idPedido,
                    Id_Cte = 0,
                    Id_Rut = ruta
                };

                var result = new Pedido();
                new CN_CapPedido().CambiarRutaEntregaPedido(pedido, MySesion.Emp_Cnx, ref result);
            }

            response.Message = "Ruta asignada correctamente a todos los pedidos";
            response.Status = true;
            return response;
        }
        public string Valor
        {
            get
            {
                return MaximoId();
            }
            set { }
        }
        private string MaximoId()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, "CatRutaEntrega", "Id_Rut", Sesion.Emp_Cnx, "spCatLocal_Maximo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}