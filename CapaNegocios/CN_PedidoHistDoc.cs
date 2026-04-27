using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_PedidoHistDoc
    {
        public void InsertarPedidoHistorialDocumentoEnviado(PedidoHistDoc pedido, string Conexion, ref int verificador)
        {
            CD_PedidoHistDoc PedidoHist = new CD_PedidoHistDoc();
            PedidoHist.InsertarPedidoHistorialDocumentoEnviado(pedido, Conexion, ref verificador);
        }

        public void ConsultarPedidoHistorialDocumentoEnviado(PedidoHistDoc pedido, string Conexion, ref List<PedidoHistDoc> ListaPedido)
        {
            CD_PedidoHistDoc PedidoHist = new CD_PedidoHistDoc();
            PedidoHist.ConsultarPedidoHistorialDocumentoEnviado(pedido, Conexion, ref  ListaPedido);
        }

        public void Insertar_RegistroPedidoNuExp(PedidoHistDoc pedido, string Conexion, ref int verificador)
        {
            CD_PedidoHistDoc PedidoHist = new CD_PedidoHistDoc();
            PedidoHist.Insertar_RegistroPedidoNuExp(pedido, Conexion, ref verificador);
        }

        public void Consulta_RegistroPedidoNuExp(PedidoHistDoc pedido, string Conexion, ref List<PedidoHistDoc> ListaPedido)
        {
            CD_PedidoHistDoc PedidoHist = new CD_PedidoHistDoc();
            PedidoHist.Consulta_RegistroPedidoNuExp(pedido, Conexion, ref  ListaPedido);
        }
    }
}
