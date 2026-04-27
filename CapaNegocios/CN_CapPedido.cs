
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using CapaEntidad;
using CapaDatos;
using System.Data;

namespace CapaNegocios
{
    public class CN_CapPedido
    {

        public void InsertarPedido(CapaEntidad.Pedido pedido, DataTable dt, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.InsertarPedido(pedido, dt, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPedido(ref Pedido pedido, string Conexion)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ConsultaPedido(ref pedido, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void PedidosPendientes_ConsultaReporte(Pedido pedido, ref DataTable dt, string Conexion)
        {
            try
            {
                CD_CapPedido cd_ped = new CD_CapPedido();
                cd_ped.PedidosPendientes_ConsultaReporte(pedido, ref dt, Conexion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool ConsultaPedidoFacturacion(ref Pedido pedido, string Conexion)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                return claseCapaDatos.ConsultaPedidoFacturacion(ref pedido, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ConsultaPedidoOC(ref Pedido pedido, string Conexion)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                return claseCapaDatos.ConsultaPedidoOC(ref pedido, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool ConsultaPedidoTieneUnidadesRemisionadas(ref Pedido pedido, string Conexion)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                return claseCapaDatos.ConsultaPedidoTieneUnidadesRemisionadas(ref pedido, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarTotalPedidosCentroDist(ref int verificador, int Id_Cd, string Conexion)
        {
            try
            {
                new CD_CapPedido().ConsultarTotalPedidosCentroDist(ref verificador, Id_Cd, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarPedido(Pedido pedido, DataTable dt, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ModificarPedido(pedido, dt, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPedidoDet(Pedido pedido, ref DataTable dt, string Conexion)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ConsultaPedidoDet(pedido, Conexion, ref dt);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPedidoDet(Pedido pedido, ref List<OrdenCompra_Detalle> lista, string Conexion)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ConsultaPedidoDet(pedido, Conexion, ref lista);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPedidoDetCN(Pedido pedido, ref List<OrdenCompra_Detalle> lista, string Conexion)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ConsultaPedidoDetCN(pedido, Conexion, ref lista);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultarPedidoOrden_Detalle(string idOC, int Id_Cte, int Id_Cd, string Conexion, ref List<OrdenCompra_Detalle> Lista)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ConsultarPedidoOrden_Detalle(idOC, Id_Cte, Id_Cd, Conexion, ref Lista);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPedidoDetDisp(Pedido pedido, ref DataTable dt, int? facturando, string Conexion)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ConsultaPedidoDetDisp(pedido, Conexion, facturando, ref dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Baja(Pedido ped, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.Baja(ped, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void logCancelarPedido(Pedido ped, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.logCancelarPedido(ped, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Imprimir(Pedido ped, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.Imprimir(ped, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPedido(Pedido pedido, string Conexion, ref List<Pedido> List)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ConsultaPedido(pedido, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPedidoAsig_Admin(Pedido pedido, string Conexion, ref List<Pedido> List)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ConsultaPedidoAsig_Admin(pedido, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaPedidoAsig_Admin_FechaEntrega_Picking_SinIdentificar(Pedido pedido, string Conexion, ref List<Pedido> List)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ConsultaPedidoAsig_Admin_FechaEntrega_Picking_SinIdentificar(pedido, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPedidoAsig_Admin_FechaEntrega_Picking(Pedido pedido, string Conexion, ref List<Pedido> List)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ConsultaPedidoAsig_Admin_FechaEntrega_Picking(pedido, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPedidoAsig_Admin_FechaEntrega(Pedido pedido, string Conexion, ref List<Pedido> List)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ConsultaPedidoAsig_Admin_FechaEntrega(pedido, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPedidoAsig_Admin_FechaEntregaPorProducto_Picking(Pedido pedido, string Conexion, ref List<PedidoDet> List)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ConsultaPedidoAsig_Admin_FechaEntregaPorProducto_Picking(pedido, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPedidoAsig_Admin_FechaEntregaPorProducto(Pedido pedido, string Conexion, ref List<PedidoDet> List)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ConsultaPedidoAsig_Admin_FechaEntregaPorProducto(pedido, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaPedidoAsig_Picking(Pedido pedido, string Conexion, ref List<PedidoDet> List)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ConsultaPedidoAsig_Picking(pedido, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaPedidoAsig(Pedido pedido, string Conexion, ref List<PedidoDet> List)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ConsultaPedidoAsig(pedido, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizaPedidoDireccionEntrega(Pedido pedido, string Conexion, ref Pedido List)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ActualizaPedidoDireccionEntrega(pedido, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CambiarRutaEntregaPedido(Pedido pedido, string Conexion, ref Pedido List)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.CambiarRutaEntregaPedido(pedido, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ActualizaPedidoFechaEntrega(Pedido pedido, string Conexion, ref Pedido List)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ActualizaPedidoFechaEntrega(pedido, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPedidoDireccionEntrega(Pedido pedido, string Conexion, ref Pedido List)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ConsultaPedidoDireccionEntrega(pedido, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPedidoAsig_Picking_Pedido(Pedido pedido, string Conexion, ref List<PedidoDet> List)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ConsultaPedidoAsig_Picking_Ped(pedido, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //ConsultaPedidoAsig_Picking_Ped


        public void AsignarPrdXPed(Pedido pedido, List<PedidoDet> list, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.AsignarPrdXPed(pedido, list, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //AsignarPrdXPed_Picking


        public void AsignarPrdXPed_Picking(Pedido pedido, List<PedidoDet> list, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.AsignarPrdXPed_Picking(pedido, list, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPedidoAutorizacion_Lista(Pedido pedido, string Conexion, ref List<Pedido> List)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ConsultaPedidoAutorizacion_Lista(pedido, Conexion, ref List);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Autorizar(Pedido pedido, string Conexion, ref int verificador)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.Autorizar(pedido, Conexion, ref verificador);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarEncabezado_RepFacPedidosPendientes(Sesion sesion, ref string Emp_Nombre, ref string Cd_Nombre, ref string U_Nombre)
        {
            try
            {
                new CD_CapPedido().ConsultarEncabezado_RepFacPedidosPendientes(sesion, ref Emp_Nombre, ref Cd_Nombre, ref U_Nombre);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPedidoDisp(Pedido pedido, int prd, string Conexion, ref int disponible_pedido)
        {
            try
            {
                new CD_CapPedido().ConsultaPedidoDisp(pedido, prd, Conexion, ref disponible_pedido);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPedidoCancelacion(PedidoDet pedido, string Conexion, ref List<PedidoDet> list)
        {
            try
            {
                new CD_CapPedido().ConsultaPedidoCancelacion(pedido, Conexion, ref list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BajaParcial(Pedido pedido, List<PedidoDet> list, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapPedido().BajaParcial(pedido, list, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConfirmarPedido(PedidoDet pedido, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapPedido().ConfirmarPedido(pedido, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CancelarPedido(PedidoDet pedido, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapPedido().CancelarPedido(pedido, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaAsignPedxPrd_Picking_Producto(PedidoDet prdDet, string Conexion, ref List<ProductoDet> List)
        {
            try
            {
                new CD_CapPedido().ConsultaAsignPedxPrd_Picking_Producto(prdDet, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AsignarRuta(int id_Ped, string sector, string ruta, int secuencia, string Conexion, ref int verificador)
        {
            try
            {
                new CD_CapPedido().AsignarRuta(id_Ped, sector, ruta, secuencia, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPedido2(ref Pedido pedido, string Conexion)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ConsultaPedido2(ref pedido, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaCaptacionPedidoDet(Pedido pedido, ref DataTable dt, ref DataTable dtRestos, string Conexion)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ConsultaCaptacionPedidoDet2(pedido, Conexion, ref dt, ref dtRestos);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void ConsultaCaptacionPedidoDetadmin(Pedido pedido, ref DataTable dt, string Conexion)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ConsultaCaptacionPedidoDetadmin(pedido, Conexion, ref dt);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void ConsultaCaptacionPedidoDet(Pedido pedido, ref List<PedidoDet> ped, string Conexion)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ConsultaCaptacionPedidoDet(pedido, ref ped, Conexion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void spCapFacturaDetalle_Consultar(Factura pedido, ref List<PedidoDet> ped, string Conexion)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.spCapFacturaDetalle_Consultar(pedido, ref ped, Conexion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void ConsultarProductoParaVI2(Pedido pedido, string Conexion, ref List<PedidoDet> list)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ConsultarProductoParaVI2(pedido, Conexion, ref list);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaProductoRepetidosDet2(Pedido pedido, string Conexion, ref DataTable dt)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ConsultaProductoRepetidosDet2(pedido, Conexion, ref dt);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultarProductoParaVI(Pedido pedido, string Conexion, ref List<PedidoDet> list)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ConsultarProductoParaVI2(pedido, Conexion, ref list);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaPedidoAutorizacion(ref Pedido pedido, string Conexion)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ConsultaPedidoAutorizacion(ref pedido, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPedidoDetAutorizacion(Pedido pedido, ref DataTable dt, string Conexion)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ConsultaPedidoDetAutorizacion(pedido, Conexion, ref dt);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarCancelacioPedido(Pedido Datos, ref List<Pedido> lista, string Conexion)
        {
            CD_CapPedido cd = new CD_CapPedido();
            cd.ConsultarCancelacioPedido(Datos, ref lista, Conexion);
        }

        public void ConsultaPedidoAutorizado(ref Pedido pedido, string Conexion)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ConsultaPedidoAutorizado(ref pedido, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaCaptacionPedidoAutorizado(Pedido pedido, ref DataTable dt, ref DataTable dtRestos, string Conexion)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ConsultaCaptacionPedidoAutorizado(pedido, Conexion, ref dt, ref dtRestos);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaproPedidoDetAut(Pedido pedido, ref DataTable dt, ref DataTable dtRestos, string Conexion)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ConsultaproPedidoDetAut(pedido, Conexion, ref dt, ref dtRestos);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaproPedidoAut(ref Pedido pedido, string Conexion)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ConsultaproPedidoAut(ref pedido, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void spCappedidoDetalle_Consultar(PedidoVtaInst pedido, ref List<PedidoDet> ped, string Conexion)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.spCapFacturaDetalle_Consultar(pedido, ref ped, Conexion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarClienteFechaEntrega(string strConexion, int intIdCte, int IntIdCd, ref string strFechaEntrega, ref int intDiasEntrega, ref DateTime fechaEntrega, ref int intEsModificable)
        {
            try
            {
                CD_CapPedido claseCapaDatos = new CD_CapPedido();
                claseCapaDatos.ConsultarClienteFechaEntrega(strConexion, intIdCte, IntIdCd, ref strFechaEntrega, ref intDiasEntrega, ref fechaEntrega, ref intEsModificable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}