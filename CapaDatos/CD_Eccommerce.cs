using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Eccommerce
    {
        public void ValidarPedidoFacturaEcommerce(Factura factura, string conexion, ref List<Factura> lista)
        {
            try
            {
                CD_Datos CDDatos = new CD_Datos(conexion);
                SqlDataReader sdr = null;

                string[] parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Fac_PedNum",
                                      };
                object[] valores = {
                                       factura.Id_Emp,
                                       factura.Id_Cd,
                                       factura.Fac_PedNum,
                                   };

                SqlCommand slqcmd = CDDatos.GenerarSqlCommand("spCapPed_validar"
                    , ref sdr, parametros, valores);

                while (sdr.Read())
                {
                    factura = new Factura();
                    factura.Id_Ped = sdr.IsDBNull(sdr.GetOrdinal("Id_Pedido")) ? 0 : Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Id_Pedido")));
                    factura.id_pedMag = sdr.IsDBNull(sdr.GetOrdinal("Id_PedidoMagento")) ? 0 : Convert.ToInt32(sdr.GetValue(sdr.GetOrdinal("Id_PedidoMagento")));
                    lista.Add(factura);
                }

                CDDatos.LimpiarSqlcommand(ref slqcmd);
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
                CD_Datos CDDatos = new CD_Datos(conexion);

                string[] parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Fac",
                                      };
                object[] valores = {
                                       factura.Id_Emp,
                                       factura.Id_Cd,
                                       factura.Id_Fac,
                                   };

                SqlCommand slqcmd = CDDatos.GenerarSqlCommand("spCapPed_BuscarPed", ref verificador, parametros, valores);
                CDDatos.LimpiarSqlcommand(ref slqcmd);
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
                CD_Datos CDDatos = new CD_Datos(conexion);
                string[] parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Fac_PedNum",
                                      };
                object[] valores = {
                                       remisiones.Id_Emp,
                                       remisiones.Id_Cd,
                                       remisiones.Id_Ped,
                                   };

                SqlCommand slqcmd = CDDatos.GenerarSqlCommand("spEccomercePedido_validar", ref verificador, parametros, valores);
                CDDatos.LimpiarSqlcommand(ref slqcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ValidarPedidoEcommerce(Remision remisiones, string conexion, ref int verificador)
        {
            try
            {
                CD_Datos CDDatos = new CD_Datos(conexion);
                string[] parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Fac_PedNum",
                                      };
                object[] valores = {
                                       remisiones.Id_Emp,
                                       remisiones.Id_Cd,
                                       remisiones.Id_Ped,
                                   };

                SqlCommand slqcmd = CDDatos.GenerarSqlCommand("spCapPed_validar", ref verificador, parametros, valores);
                CDDatos.LimpiarSqlcommand(ref slqcmd);
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
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Ped", "@tipo" };
                object[] Valores = { pedido.Id_Emp, pedido.Id_Cd, pedido.PedExterno, pedido.Ped_Tipo };
                //SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedido_Consultar", ref dr, Parametros, Valores);
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedido_BajaPortal", ref verificador, Parametros, Valores);


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

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
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Pedido", "@Id_Cd" };
                object[] Valores = { pedido, id_Cd };
                //SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedido_Consultar", ref dr, Parametros, Valores);
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SP_EmailConfirmacionEccomerce", ref verificador, Parametros, Valores);


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

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
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Pedido", "@Id_Cd" };
                object[] Valores = { pedido, id_Cd };
                //SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedido_Consultar", ref dr, Parametros, Valores);
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SP_EmailCancelarEccomerce", ref verificador, Parametros, Valores);


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

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
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Pedido", "@Id_Cd", "@id_Fac", "@id_Rem" };
                object[] Valores = { pedido, id_Cd,
                    id_fac == 0 ? (object)null: id_fac,
                    id_rem == 0 ? (object)null: id_rem};
                //SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedido_Consultar", ref dr, Parametros, Valores);
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SP_EmailenvioEccomerce", ref verificador, Parametros, Valores);


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

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
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Pedido", "@Id_Cd" };
                object[] Valores = { pedido, id_Cd,
                     id_fac == 0 ? (object)null: id_fac,
                     id_rem == 0 ? (object)null: id_rem};
                //SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedido_Consultar", ref dr, Parametros, Valores);
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SP_EmailEntregaEccomerce", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}