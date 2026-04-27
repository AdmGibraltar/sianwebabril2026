using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class CN_Eccommerce
    {
        public void ValidarPedidoFacturaEcommerce(Factura factura, string conexion, ref List<Factura> lista)
        {
            try
            {
                CD_Eccommerce CD = new CD_Eccommerce();

                CD.ValidarPedidoFacturaEcommerce(factura, conexion, ref lista);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BuscarNumeroPedidoEcommerce(Factura factura, string conexion, ref int verificador)
        {
            try
            {
                CD_Eccommerce CD = new CD_Eccommerce();

                CD.BuscarNumeroPedidoEcommerce(factura, conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ValidarPedidoEcommerce(Remision Remisiones, string conexion, ref int verificador)
        {
            try
            {
                CD_Eccommerce CD = new CD_Eccommerce();

                CD.ValidarPedidoEcommerce(Remisiones, conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ValidarPrePedidoEcommerce(Remision remisiones, string conexion, ref int verificador)
        {
            try
            {
                CD_Eccommerce CD = new CD_Eccommerce();

                CD.ValidarPrePedidoEcommerce(remisiones, conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BajaPedido_Portal(PedidoVtaInst pedido, ref int verificador, string Conexion)
        {
            try
            {
                CD_Eccommerce CD = new CD_Eccommerce();

                CD.BajaPedido_Portal(pedido, ref verificador, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EnviarCorreoCaptacion(int pedido, int id_Cd, ref int verificador, string Conexion)
        {
            try
            {
                CD_Eccommerce CD = new CD_Eccommerce();

                CD.EnviarCorreoCaptacion(pedido, id_Cd, ref verificador, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EnviarCorreoCancelacion(int pedido, int id_Cd, ref int verificador, string Conexion)
        {
            try
            {
                CD_Eccommerce CD = new CD_Eccommerce();

                CD.EnviarCorreoCancelacion(pedido, id_Cd, ref verificador, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EnviarCorreoEnvio(int pedido, int id_Cd, int id_fac, int id_rem, ref int verificador, string Conexion)
        {
            try
            {
                CD_Eccommerce CD = new CD_Eccommerce();

                CD.EnviarCorreoEnvio(pedido, id_Cd, id_fac, id_rem, ref verificador, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EnviarCorreoEntrega(int pedido, int id_Cd, int id_fac, int id_rem, ref int verificador, string Conexion)
        {
            try
            {
                CD_Eccommerce CD = new CD_Eccommerce();

                CD.EnviarCorreoEntrega(pedido, id_Cd, id_fac, id_rem, ref verificador, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}