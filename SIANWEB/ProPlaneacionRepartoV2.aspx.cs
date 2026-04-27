using CapaDatos;
using CapaEntidad;
using CapaNegocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIANWEB
{
    public partial class ProPlaneacionRepartoV2 : Page
    {
        public static Sesion MySesion { get; set; }
        public StringBuilder Expresion { get; set; }
        public List<PedidoDet> PedidoDets { get; set; }

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
            var q = Request.QueryString;

            fecha.Text = q["Fecha"];
            territorio.Text = q["Territorio"];
            credito.Text = q["Credito"];
            cliente.Text = q["Nom_Cte"];
            idCliente.Text = q["Id_Cte"];
            pedidoId.Value = q["Id"];

            PedidoDets = GetList();
        }


        List<PedidoDet> GetList()
        {
            var list = new List<PedidoDet>();
            var pedido = new Pedido
            {
                Id_Emp = MySesion.Id_Emp,
                Id_Cd = MySesion.Id_Cd_Ver,
                Id_Ped = int.TryParse(pedidoId.Value, out int pedId) ? pedId : 0
            };

            new CN_CapPedido().ConsultaPedidoAsig_Picking_Pedido(pedido, MySesion.Emp_Cnx, ref list);
            return list;
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            var pedidos = GetList();
            int inicial;
            int.TryParse(productoInicial.Value, out inicial);
            int final;
            int.TryParse(productoFinal.Value, out final);

            if (productoInicial.Value.Length > 0 && productoFinal.Value.Length > 0)
            {
                pedidos = pedidos.Where(x => x.Id_Prd >= inicial && x.Id_Prd <= final).ToList();
            }
            else if (productoInicial.Value.Length > 0)
            {
                pedidos = pedidos.Where(x => x.Id_Prd >= inicial).ToList();
            }
            else if (productoFinal.Value.Length > 0)
            {
                pedidos = pedidos.Where(x => x.Id_Prd <= final).ToList();
            }

            if (producto.Value.Length > 0)
            {
                pedidos = pedidos.Where(x => x.Prd_Desc.ToLower().Contains(producto.Value.ToLower())).ToList();
            }

            PedidoDets = pedidos;
        }

        public class ResponseJson
        {
            public string Message { get; set; }
            public bool Status { get; set; }
            public bool Rebind { get; set; }
        }

        public class AsignarModel
        {
            public long ProductoId { get; set; }
            public int CantAsignada { get; set; }
            public int CantPicking { get; set; }
            public int PzasNoConf { get; set; }
            public int PzasNoEnc { get; set; }
        }


        [WebMethod]
        public static ResponseJson Asignar(bool HF_Guardar, int pedidoId, int clienteId, List<AsignarModel> model)
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
                    var list = new List<PedidoDet>();
                    var pedido = new Pedido
                    {
                        Id_Emp = MySesion.Id_Emp,
                        Id_Cd = MySesion.Id_Cd_Ver,
                        Id_Ped = pedidoId,
                        Ped_Fecha = new Funciones().GetLocalDateTime(MySesion.Minutos),
                        Id_U = MySesion.Id_U,
                        Id_Cte = clienteId
                    };

                    new CN_CapPedido().ConsultaPedidoAsig_Picking_Pedido(pedido, MySesion.Emp_Cnx, ref list);
                    if (list.Count > 0 && list.Count == model.Count)
                    {
                        AsignarModel m;
                        foreach (var p in list)
                        {
                            m = model.First(x => x.ProductoId == p.Id_Prd);
                            int nuevaAsignada = m.CantAsignada - p.Prd_Asig;
                            if (nuevaAsignada < 0)
                                nuevaAsignada = 0;

                            p.Prd_Asig = nuevaAsignada;
                            p.Prd_NoConf = m.PzasNoConf;
                            p.Prd_NoEnc = m.PzasNoEnc;
                            p.Ped_Picking = m.CantPicking;
                        }

                        int verificador = 0;
                        new CN_CapPedido().AsignarPrdXPed_Picking(pedido, list, MySesion.Emp_Cnx, ref verificador);

                        switch (verificador)
                        {
                            case 1:
                                response.Message = "Se realizó la asignación correctamente";
                                response.Status = true;
                                break;
                            case 2:
                                response.Message = "No se cuenta con el inventario suficiente, no se realizo la asignación";
                                break;
                            case 3:
                                response.Message = "No se cuenta con el inventario suficiente, no se realizo la asignación";
                                break;
                            case 4:
                                response.Message = "Picking confirmado correctamente";
                                response.Status = true;
                                break;
                            default:
                                response.Message = "Ocurrió un error al intentar asignar";
                                break;
                        }

                        response.Rebind = true;
                    }
                    else
                    {
                        if (list.Count == 0) response.Message = "Error, no se encontraron productos del pedido";
                        else response.Message = "Error, los productos del pedido no coinciden con los productos enviados";
                    }
                }
                else
                {
                    response.Message = "hf_guardar=true";
                }
            }
            catch (Exception ex) { response.Message = ex.Message; }

            return response;
        }

        protected void Refresh_Click(object sender, EventArgs e)
        {
            PedidoDets = GetList();
        }
    }
}