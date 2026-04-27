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
    public partial class ProAsignPedxPrd_PickingV2 : System.Web.UI.Page
    {
        public static Sesion MySesion { get; set; }

        public bool MostrarBotonGuardar { get; set; }
        public static int HF_Ped { get; set; }
        public static string Ruta { get; set; }
        public static string Credito { get; set; }
        public static string Parcialidades { get; set; }
        public static string TipoPedido { get; set; }
        public string ProductoNombre { get; set; }
        public static List<ProductoDet> Pedidos { get; set; }
        private static List<PedidoDet> datosAgrupados { get; set; }
        public static bool ckAgrupador { get; set; }
        public static int IdCliente { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            MySesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (MySesion == null)
            {
                string script = "<script>closeThisWindow()</script>";
                ScriptManager.RegisterStartupScript(this, GetType(), "closeThisWindow()", script, false);
                return;
            }

            if (!Page.IsPostBack)
            {
                Inicializar();
                Pedidos = GetList();
            }
        }

        void Inicializar()
        {
            var q = Request.QueryString;
            int hf_ped;
            
            HF_Ped = int.TryParse(q["Id_Prd"], out hf_ped) ? hf_ped : 0;
            Ruta = q["Ruta"];
            Credito = q["Credito"];
            Parcialidades = q["Parcialidades"];
            TipoPedido = q["TipoPedido"];
            bool ckAgrupa = false;
            ckAgrupador = bool.TryParse(q["ckAgrupador"], out ckAgrupa) ? ckAgrupa : false;
            int rf_cliente;
            IdCliente = int.TryParse(q["id_cliente"], out rf_cliente) ? rf_cliente : 0;

            var producto = new Producto();

            new CN_CatProducto().ConsultaProducto(ref producto, MySesion.Emp_Cnx, MySesion.Id_Emp, MySesion.Id_Cd_Ver, HF_Ped, 0);
            ProductoNombre = producto.Prd_Descripcion;

            var permisoGuardar = Convert.ToBoolean(q["PermisoGuardar"]);
            var permisoModificar = Convert.ToBoolean(q["PermisoModificar"]);

            MostrarBotonGuardar = true;
            if (!permisoGuardar && !permisoModificar) MostrarBotonGuardar = false;
            datosAgrupados = (List<PedidoDet>)Session[Session.SessionID + "ListaResultados"];
        }

        

        static List<ProductoDet> GetList()
        {
          
            var result = new List<ProductoDet>();

            var nuevaLista = new List<ProductoDet>();

            try
            {
                if (ckAgrupador == true)
                {
                    var elementosCoincidentes = datosAgrupados.Where(x => x.Id_Prd == HF_Ped && x.Ruta == Ruta).ToList();
                    foreach (var item in elementosCoincidentes)
                    {
                        var pedidoDet = new PedidoDet
                        {
                            Id_Emp = MySesion.Id_Emp,
                            Id_Cd = MySesion.Id_Cd,
                            Id_Prd = item.Id_Prd,
                            Ruta = item.Ruta,
                            CreditoStr = item.CreditoStr,
                            Ped_PermiteParcialidades = item.Ped_PermiteParcialidades,
                            TipoPedido = item.TipoPedido,
                            Id_Cliente=IdCliente
                        };

                        new CN_CapPedido().ConsultaAsignPedxPrd_Picking_Producto(pedidoDet, MySesion.Emp_Cnx, ref result);

                    }
                    nuevaLista = result;
                }
                else
                {
                    var pedidoDet = new PedidoDet
                    {
                        Id_Emp = MySesion.Id_Emp,
                        Id_Cd = MySesion.Id_Cd,
                        Id_Prd = HF_Ped,
                        Ruta = Ruta,
                        CreditoStr = Credito,
                        Ped_PermiteParcialidades = Parcialidades,
                        TipoPedido = TipoPedido,
                        Id_Cliente = IdCliente
                    };

                    new CN_CapPedido().ConsultaAsignPedxPrd_Picking_Producto(pedidoDet, MySesion.Emp_Cnx, ref result);
                    nuevaLista.AddRange(result);
                }

            }
            catch { }

            return nuevaLista;
        }

        public class ResponseJson
        {
            public string Message { get; set; }
            public bool Status { get; set; }
            public bool Rebind { get; set; }
        }

        [WebMethod]
        public static ResponseJson ConfirmarPedido()
        {

            var response = new ResponseJson
            {
                Status = false,
                Rebind = false
            };

            try
            {
                MySesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
                if (MySesion == null)
                {
                    response.Message = "connection close";
                    return response;
                }

                int verificador = 0;
                //ivan G
                if (ckAgrupador == true)
                {
                    var elementosCoincidentes = datosAgrupados.Where(x => x.Id_Prd == HF_Ped && x.Ruta == Ruta).ToList();
                    foreach (var item in elementosCoincidentes)
                    {
                        var pedido = new PedidoDet
                        {
                            Id_Emp = MySesion.Id_Emp,
                            Id_Cd = MySesion.Id_Cd_Ver,
                            Id_Prd = item.Id_Prd,
                            Ruta = item.Ruta,
                            CreditoStr = item.CreditoStr,
                            Ped_PermiteParcialidades = item.Ped_PermiteParcialidades,
                            TipoPedido = item.TipoPedido
                        };

                        new CN_CapPedido().ConfirmarPedido(pedido, MySesion.Emp_Cnx, ref verificador);
                    }

                }
                else
                {
                    var pedido = new PedidoDet
                    {
                        Id_Emp = MySesion.Id_Emp,
                        Id_Cd = MySesion.Id_Cd_Ver,
                        Id_Prd = HF_Ped,
                        Ruta = Ruta,
                        CreditoStr = Credito,
                        Ped_PermiteParcialidades = Parcialidades,
                        TipoPedido = TipoPedido
                    };
                    new CN_CapPedido().ConfirmarPedido(pedido, MySesion.Emp_Cnx, ref verificador);
                }


                if (verificador == 0)
                {
                    response.Status = true;
                    response.Rebind = true;
                    response.Message = "Las cantidades fueron actualizadas correctamente";
                }
            }
            catch { }
            return response;
        }

        [WebMethod]
        public static ResponseJson CancelarPedido()
        {

            var response = new ResponseJson
            {
                Status = false,
                Rebind = true
            };

            try
            {
                MySesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
                if (MySesion == null)
                {
                    response.Message = "connection close";
                    return response;
                }

                int verificador = 0;
                //ivan g
                if (ckAgrupador == true)
                {
                    var elementosCoincidentes = datosAgrupados.Where(x => x.Id_Prd == HF_Ped && x.Ruta == Ruta).ToList();
                    foreach (var item in elementosCoincidentes)
                    {

                        var pedido = new PedidoDet
                        {
                            Id_Emp = MySesion.Id_Emp,
                            Id_Cd = MySesion.Id_Cd_Ver,
                            Id_Prd = HF_Ped,
                            Ruta = item.Ruta,
                            CreditoStr = item.CreditoStr,
                            Ped_PermiteParcialidades = item.Ped_PermiteParcialidades,
                            TipoPedido = item.TipoPedido
                        };
                        new CN_CapPedido().CancelarPedido(pedido, MySesion.Emp_Cnx, ref verificador);
                    }

                }
                else
                {
                    var pedido = new PedidoDet
                    {
                        Id_Emp = MySesion.Id_Emp,
                        Id_Cd = MySesion.Id_Cd_Ver,
                        Id_Prd = HF_Ped,
                        Ruta = Ruta,
                        CreditoStr = Credito,
                        Ped_PermiteParcialidades = Parcialidades,
                        TipoPedido = TipoPedido
                    };
                    new CN_CapPedido().CancelarPedido(pedido, MySesion.Emp_Cnx, ref verificador);
                }

                if (verificador == 0)
                {
                    response.Status = true;
                    response.Rebind = true;
                    response.Message = "Las cantidades fueron actualizadas correctamente";
                }
            }
            catch { }
            return response;
        }

        [WebMethod]
        public static ResponseJson Guardar(bool HF_Guardar, bool HiddenRebind, List<int> asign, List<int> picking)
        {
            var response = new ResponseJson
            {
                Status = false,
                Rebind = false
            };

            try
            {
                MySesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
                if (MySesion == null)
                {
                    response.Message = "connection close";
                    return response;
                }

                if (HF_Guardar)
                {
                    if (!HiddenRebind)
                    {
                        response.Message = "No ha realizado ninguna asignación";
                        return response;
                    }

                    var pedido = new Pedido
                    {
                        Id_Emp = MySesion.Id_Emp,
                        Id_Cd = MySesion.Id_Cd_Ver,
                        Ped_Fecha = new Funciones().GetLocalDateTime(MySesion.Minutos),
                        Id_U = MySesion.Id_U
                    };

                    var list = new List<PedidoDet>();
                    int i = 0;
                    foreach (var p in Pedidos)
                    {
                        list.Add(new PedidoDet
                        {
                            Id_Prd = HF_Ped,
                            Id_Ped = p.Id_Ped,
                            Id_PedDet = p.Id_PedDet,
                            Prd_Asig = asign[i] - p.Ped_Asignar,
                            Ped_Picking = picking[i]
                        });
                        i++;
                    }

                    int verificador = 0;
                    new CN_CapPedido().AsignarPrdXPed_Picking(pedido, list, MySesion.Emp_Cnx, ref verificador);

                    switch (verificador)
                    {
                        case 1:
                            response.Message = "Se realizó la asignación correctamente";
                            response.Status = true;
                            break;
                        case 2: response.Message = "No se cuenta con el inventario suficiente, no se realizo la asignación";
                            break;
                        case 3: response.Message = "No se pudo realizar la asignación, el pedido no cuenta con la cantidad por asignar";
                            break;
                        case 4:
                        default:
                            response.Message = "Picking confirmado correctamente";
                            response.Status = true;
                            break;
                    }

                    response.Rebind = true;
                }
                else
                {
                    response.Message = "hf_guardar=true";
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "inventario_insuficiente")
                {
                    response.Message = "No se cuenta con el inventario suficiente, no se realizo la asignación";
                    response.Rebind = true;
                }
                else if (ex.Message.Contains("deadlocked"))
                    response.Message = "El servidor esta tardando en responder, por favor de click en guardar nuevamente";
            }
            return response;
        }

        protected void RefreshTable_Click(object sender, EventArgs e)
        {
            Pedidos = GetList();
        }
    }
}