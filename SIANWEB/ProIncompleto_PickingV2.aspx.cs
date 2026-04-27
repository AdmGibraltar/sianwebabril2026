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
using System.Configuration;

namespace SIANWEB
{
    public partial class ProIncompleto_PickingV2 : System.Web.UI.Page
    {
        public static Sesion MySesion { get; set; }
        public static int HF_Ped { get; set; }
        public string ProductoNombre { get; set; }
        public string Presentacion { get; set; }
        public string Inventario { get; set; }
        public string Asignado { get; set; }
        public string Disponible { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            MySesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (MySesion == null)
            {
                string script = "<script>closeThisWindow()</script>";
                ScriptManager.RegisterStartupScript(this, GetType(), "closeThisWindow()", script, false);
                return;
            }

            if (!Page.IsPostBack) Inicializar();
        }

        void Inicializar()
        {
            var q = Request.QueryString;
            int hf_ped;
            HF_Ped = int.TryParse(q["Id_Prd"], out hf_ped) ? hf_ped : 0;

            Asignado = q["CantidadAsignada"];
            Disponible = q["CantidadDisponible"];

            int asignado;
            int disponible;
            int.TryParse(Asignado, out asignado);
            int.TryParse(Disponible, out disponible);

            Inventario = (asignado + disponible).ToString();

            var producto = new Producto();

            new CN_CatProducto().ConsultaProducto(ref producto, MySesion.Emp_Cnx, MySesion.Id_Emp, MySesion.Id_Cd_Ver, HF_Ped, 0);
            ProductoNombre = producto.Prd_Descripcion;
            Presentacion = producto.Prd_Presentacion + " " + producto.Prd_UniNe;
        }

        public class ConfirmarResponse
        {
            public string Message { get; set; }
            public bool Status { get; set; }
        }

        [WebMethod]
        public static ConfirmarResponse Confirmar(int noEncontrado, int noConforme)
        {
            var response = new ConfirmarResponse { Status = false };
            MySesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];

            if (MySesion == null)
            {
                response.Message = "connection close";
                return response;
            }

            string result = "";
            if (noEncontrado > 0 && noConforme > 0)
            {


                result = InsertaRemision(61, HF_Ped, noEncontrado);
                if (result != "") result += "\n";
                result += InsertaRemision(65, HF_Ped, noConforme);

                if (result == "")
                {
                    result = "Actualizado Producto No Encontrado y No Conforme";
                    response.Status = true;
                }
            }
            else if (noEncontrado > 0)
            {
                result = InsertaRemision(61, HF_Ped, noEncontrado);
                if (result == "")
                {
                    result = "Actualizado Producto No Encontrado";
                    response.Status = true;
                }

            }
            else if (noConforme > 0)
            {
                result = InsertaRemision(65, HF_Ped, noConforme);
                if (result == "")
                {
                    result = "Actualizado Producto No Conforme";
                    response.Status = true;
                }
            }
            response.Message = result;

            return response;
        }

        static string InsertaRemision(int id_tm, int id_prd, int cantidad)
        {
            string mensaje = "";

            var funcion = new Funciones();
            DateTime date = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy") + " " + funcion.GetLocalDateTime(MySesion.Minutos).ToString("HH:mm:ss"));
            var remision = new Remision
            {
                Id_Emp = MySesion.Id_Emp,
                Id_Cd = MySesion.Id_Cd_Ver,
                Id_Rem = -1,
                Rem_ManAut = 1,
                Rem_Tipo = "3",
                Rem_Fecha = date,
                Id_Tm = id_tm,
                Id_Ped = -1,
                Id_Cte = 100,
                Id_Ter = 50701011,
                Id_Rik = 100,
                Id_U = MySesion.Id_U,
                Rem_Calle = "",
                Rem_Numero = "",
                Rem_Cp = "",
                Rem_Colonia = "",
                Rem_Municipio = "",
                Rem_Estado = "",
                Rem_Rfc = "",
                Rem_Telefono = "",
                Rem_Contacto  = "",
                Rem_Conducto = "",
                Rem_Guia = "",
                Rem_FechaEntrega = date,
                Rem_Nota = "Movimiento generado desde el Picking en forma automática",
                Rem_Estatus = "C",
                Rem_OrdenCompra = "",
                Id_Vap = 0,
                Rem_CteCuentaContNacional = 0,
                Rem_CteCuentaNacional = 0
            };

            var detalles = new List<RemisionDet>
            {
                new RemisionDet
                {
                    Id_Emp = MySesion.Id_Emp,
                    Id_Cd = MySesion.Id_Cd_Ver,
                    Id_RemDet = 1,
                    Id_Ter = 50701011,
                    Id_Prd = id_prd,
                    Rem_Cant = cantidad,
                    Rem_Precio = ObtenerPrecioAAA(id_prd)
                }
            };

            remision.Rem_Subtotal = cantidad * detalles[0].Rem_Precio;
            remision.Rem_Total = (cantidad * detalles[0].Rem_Precio) * 1.16;
            remision.Rem_Iva = remision.Rem_Total - remision.Rem_Subtotal;

            try
            {
                int verificador = -1;
                int id_rem = -1;
                bool tipoMovimiento = false;
                
                new CN_CapRemision().GuardarRemision(remision, detalles, MySesion, ref verificador, false, false, ref id_rem, ref tipoMovimiento, ref mensaje,"", ConfigurationManager.AppSettings["PermitePrecios0Remision"].ToString());
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("saldo_insuficiente") || ex.Message.Contains("error"))
                    mensaje = ex.Message.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries)[0];
                else if (ex.Message.Contains("no cuenta con inventario suficiente"))
                    mensaje = ex.ToString();
            }

            return mensaje;
        }

        static float ObtenerPrecioAAA(long id_prd)
        {
            float precio = 0;
            try
            {
                int id_pre = 0;
                new CN_ProductoPrecios().ConsultaListaProductoPrecioAAA(ref precio, MySesion, id_prd, ref id_pre);
                
            }
            catch { }
            return precio;
        }
    }
}