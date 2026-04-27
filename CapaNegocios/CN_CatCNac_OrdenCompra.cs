
using CapaModelo_CC.CuentasCoorporativas;
using CapaDatos;
using System.Collections.Generic;
using System;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_CatCNac_OrdenCompra
    {

        public void ConsultarPedidosOC(int cliNum_Ini, int cliNum_Fin, int id_Ter_Ini, int id_Ter_Fin, int anio_Ini, int anio_Fin, string nomCliente, string Estatus, ref List<catcnac_PedidosOc> lista, string Conexion)
        {
            CD_CatCNac_OrdenCompra Corden = new CD_CatCNac_OrdenCompra();
            Corden.ConsultarPedidosOC(cliNum_Ini, cliNum_Fin, id_Ter_Ini, id_Ter_Fin, anio_Ini, anio_Fin, nomCliente, Estatus, ref lista, Conexion);
        }


        public spCatCNac_ConsultaPedido_OC_Result ConsultaPedidoOC_Captacion(int idOC)
        {
            CD_CatCNac_OrdenCompra Corden = new CD_CatCNac_OrdenCompra();
            return Corden.ConsultaPedidoOC_Captacion(idOC);
        }


        public List<spCatCNac_OrdenCompra_Detalle_Result> ConsultarPedidoOrden_Detalle(string idOC, int Id_Cte, int Id_Cd)
        {
            CD_CatCNac_OrdenCompra Corden = new CD_CatCNac_OrdenCompra();
            return Corden.ConsultarPedidoOrden_Detalle(idOC, Id_Cte, Id_Cd);
        }


        public List<spCatCNac_Remisiones80Ped_Result> ConsultarRemisionesPedido(int idOC)
        {
            CD_CatCNac_OrdenCompra Corden = new CD_CatCNac_OrdenCompra();
            return Corden.ConsultarRemisionesPedido(idOC);
        }



        public spCatCNac_ValidaPedidoMov80_Result ValidaRemMov80(int id_ped)
        {
            CD_CatCNac_OrdenCompra Corden = new CD_CatCNac_OrdenCompra();
            return Corden.ValidaRemMov80(id_ped);
        }

        public spCatCNac_ValidaClienteOC80_Result ValidaClienteOC80(int id_emp, int id_cd, int id_ped)
        {
            CD_CatCNac_OrdenCompra Corden = new CD_CatCNac_OrdenCompra();
            return Corden.ValidaClienteOC80(id_emp, id_cd, id_ped);
        }

    }


}