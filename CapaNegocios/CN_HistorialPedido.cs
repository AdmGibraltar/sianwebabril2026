using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_HistorialPedido
    {
        public void RastreoPedido_Captacion(HistorialPedidos pedido, string Conexion, ref List<HistorialPedidos> List)
        {
            CD_HistorialPedido Hist = new CD_HistorialPedido();
            Hist.RastreoPedido_Captacion(pedido, Conexion, ref List);
        }
    }
}