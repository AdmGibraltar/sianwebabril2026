using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using System.Data;

using CapaEntidad;

namespace CapaDatos
{
    public class CD_CapPedido
    {
        public void ConsultarTotalPedidosCentroDist(ref int verificador, int Id_Cd, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Id_Cd" };
                object[] Valores = { Id_Cd };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoCantidadEnCd_Consultar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "Id_Ped" };
                object[] Valores = { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped };
                //SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedido_Consultar", ref dr, Parametros, Valores);
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedido_ConsultarV2", ref dr, Parametros, Valores);
                if (dr.HasRows)
                {
                    dr.Read();
                    pedido.Ped_Comentarios = dr.IsDBNull(dr.GetOrdinal("Ped_Comentarios")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Comentarios")).ToString();
                    pedido.Ped_DescPorcen1 = dr.IsDBNull(dr.GetOrdinal("Ped_Desc1")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_DescPorcen1")));
                    pedido.Ped_DescPorcen2 = dr.IsDBNull(dr.GetOrdinal("Ped_Desc2")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_DescPorcen2")));
                    string entrega = dr.GetValue(dr.GetOrdinal("Ped_CondEntrega")).ToString();
                    if (!string.IsNullOrEmpty(entrega))
                        pedido.Ped_CondEntrega = Convert.ToInt32(entrega);
                    else
                        pedido.Ped_CondEntrega = 0;
                    pedido.Ped_Desc1 = dr.IsDBNull(dr.GetOrdinal("Ped_Desc1")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Desc1")).ToString();
                    pedido.Ped_Desc2 = dr.IsDBNull(dr.GetOrdinal("Ped_Desc2")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Desc2")).ToString();
                    pedido.Ped_Flete = dr.IsDBNull(dr.GetOrdinal("Ped_Flete")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Flete")).ToString();
                    pedido.Ped_Importe = dr.IsDBNull(dr.GetOrdinal("Ped_Importe")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Importe")));
                    pedido.Ped_Iva = dr.IsDBNull(dr.GetOrdinal("Ped_Iva")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Iva")));
                    pedido.Id_Cte = dr.IsDBNull(dr.GetOrdinal("Id_Cte")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    pedido.Cte_NomComercial = dr.IsDBNull(dr.GetOrdinal("Cte_NomComercial")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    pedido.Ped_Observaciones = dr.IsDBNull(dr.GetOrdinal("Ped_Observaciones")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Observaciones")).ToString();
                    pedido.Ped_OrdenEntrega = dr.IsDBNull(dr.GetOrdinal("Ped_OrdenEntrega")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_OrdenEntrega")).ToString();
                    pedido.Pedido_del = dr.IsDBNull(dr.GetOrdinal("Ped_PedidoDel")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_PedidoDel")).ToString();
                    pedido.Id_Rik = dr.IsDBNull(dr.GetOrdinal("Id_Rik")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    pedido.Rik_Nombre = dr.IsDBNull(dr.GetOrdinal("Rik_Nombre")) ? "" : dr.GetValue(dr.GetOrdinal("Rik_Nombre")).ToString();
                    pedido.Requisicion = dr.IsDBNull(dr.GetOrdinal("Ped_Requisicion")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Requisicion")).ToString();
                    pedido.Ped_Solicito = dr.IsDBNull(dr.GetOrdinal("Ped_Solicito")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Solicito")).ToString();
                    pedido.Ped_Subtotal = dr.IsDBNull(dr.GetOrdinal("Ped_Subtotal")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Subtotal")));
                    pedido.Id_Ter = dr.IsDBNull(dr.GetOrdinal("Id_Ter")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    pedido.Ter_Nombre = dr.IsDBNull(dr.GetOrdinal("Ter_Nombre")) ? "" : dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();
                    pedido.Ped_Total = dr.IsDBNull(dr.GetOrdinal("Ped_Total")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Total")));
                    pedido.Ped_Fecha = dr.IsDBNull(dr.GetOrdinal("Ped_Fecha")) ? default(DateTime) : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ped_Fecha")));
                    pedido.Ped_FechaEntrega = dr.IsDBNull(dr.GetOrdinal("Ped_FechaEntrega")) ? default(DateTime) : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ped_FechaEntrega")));
                    pedido.Ped_FechFactAcys = dr.IsDBNull(dr.GetOrdinal("Ped_FechFactAcys")) ? default(DateTime) : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ped_FechFactAcys")));


                    pedido.Ped_Tipo = dr.IsDBNull(dr.GetOrdinal("Ped_Tipo")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Tipo")));
                    pedido.Ped_SolicitoTel = dr.IsDBNull(dr.GetOrdinal("Ped_SolicitoTel")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_SolicitoTel")).ToString();
                    pedido.Ped_SolicitoEmail = dr.IsDBNull(dr.GetOrdinal("Ped_SolicitoEmail")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_SolicitoEmail")).ToString();
                    pedido.Ped_SolicitoPuesto = dr.IsDBNull(dr.GetOrdinal("Ped_SolicitoPuesto")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_SolicitoPuesto")).ToString();
                    pedido.Ped_ConsignadoCalle = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoCalle")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoCalle")).ToString();
                    pedido.Ped_ConsignadoNo = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoNo")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoNo")).ToString();
                    pedido.Ped_ConsignadoCp = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoCp")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoCp")).ToString();
                    pedido.Ped_ConsignadoMunicipio = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoMunicipio")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoMunicipio")).ToString();
                    pedido.Ped_ConsignadoEstado = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoEstado")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoEstado")).ToString();
                    pedido.Ped_ConsignadoColonia = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoColonia")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoColonia")).ToString();

                    pedido.Ped_ReqOrden = dr.IsDBNull(dr.GetOrdinal("Ped_ReqOrden")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Ped_ReqOrden")));
                    pedido.Ped_OrdenCompra = dr.IsDBNull(dr.GetOrdinal("Ped_OrdenCompra")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_OrdenCompra")).ToString();
                    pedido.Ped_AcysSemana = dr.IsDBNull(dr.GetOrdinal("Ped_AcysSemana")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_AcysSemana")));
                    pedido.Ped_AcysAnio = dr.IsDBNull(dr.GetOrdinal("Ped_AcysAnio")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_AcysAnio")));
                    pedido.Id_Acs = dr.IsDBNull(dr.GetOrdinal("Id_Acs")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));
                    pedido.Estatus = dr.GetValue(dr.GetOrdinal("Ped_Estatus")).ToString();
                    pedido.ReqAcys = dr.IsDBNull(dr.GetOrdinal("Ped_ReqAcys")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ReqAcys")).ToString();


                    pedido.Acs_ReqDocReposicion = dr.IsDBNull(dr.GetOrdinal("ped_reqdocreposicion")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ped_reqdocreposicion")));
                    pedido.Acs_ReqDocFolio = dr.IsDBNull(dr.GetOrdinal("ped_reqdocfolio")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ped_reqdocfolio")));
                    pedido.Acs_ReqFacturaKey = dr.IsDBNull(dr.GetOrdinal("ped_reqFactKey")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ped_reqFactKey")));
                    pedido.Acs_ReqCopia = dr.IsDBNull(dr.GetOrdinal("ped_reqCopiaDoc")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ped_reqCopiaDoc")));
                    pedido.ACS_ReqRemision = dr.IsDBNull(dr.GetOrdinal("ped_reqRemision")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ped_reqRemision")));


                    pedido.Acs_ReqOrdencop = dr.IsDBNull(dr.GetOrdinal("Acs_ReqOrdencop")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ReqOrdencop")));
                    pedido.Acs_ReqDocReposicioncop = dr.IsDBNull(dr.GetOrdinal("Acs_ReqOrdenrep")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ReqOrdenrep")));
                    pedido.Acs_ReqDocFoliocop = dr.IsDBNull(dr.GetOrdinal("Acs_ReqDocFoliocop")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ReqDocFoliocop")));
                    pedido.Acs_ReqFacturaKeyCop = dr.IsDBNull(dr.GetOrdinal("acs_ReqFacturaKeyCop")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("acs_ReqFacturaKeyCop")));
                    pedido.Acs_ReqCopiaCop = dr.IsDBNull(dr.GetOrdinal("Acs_ReqCopiaCop")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ReqCopiaCop")));
                    pedido.ACS_ReqRemisionCop = dr.IsDBNull(dr.GetOrdinal("ACS_ReqRemisionCop")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_ReqRemisionCop")));
                    pedido.Ped_Tipo = dr.IsDBNull(dr.GetOrdinal("TipoPedido")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("TipoPedido")));
                    pedido.PedidoExterno = dr.IsDBNull(dr.GetOrdinal("PedidoPortal")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PedidoPortal")));



                    pedido.Acs_ReqDocOtro = dr["ped_reqotrodesc"].ToString();




                    pedido.acs_contacto2 = dr["Acs_Contacto2"].ToString();
                    pedido.acs_telefono2 = dr["Acs_Telefono2"].ToString();
                    pedido.acs_email2 = dr["Acs_Email2"].ToString();
                    pedido.acs_contacto3 = dr["Acs_Contacto3"].ToString();
                    pedido.acs_telefono3 = dr["Acs_Telefono3"].ToString();
                    pedido.acs_email3 = dr["Acs_Email3"].ToString();
                    pedido.acs_contacto4 = dr["Acs_Contacto4"].ToString();
                    pedido.acs_telefono4 = dr["Acs_Telefono4"].ToString();
                    pedido.acs_email4 = dr["Acs_Email4"].ToString();
                    pedido.acs_contacto5 = dr["Acs_Contacto5"].ToString();
                    pedido.acs_telefono5 = dr["Acs_Telefono5"].ToString();
                    pedido.acs_email5 = dr["Acs_Email5"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("Id_TG")))
                    {
                        pedido.Id_TG = (int)dr.GetValue(dr.GetOrdinal("Id_TG"));
                    }

                    if (dr.IsDBNull(dr.GetOrdinal("Id_Fac")))
                        pedido.Id_Fac = null;
                    else
                        pedido.Id_Fac = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Fac")));

                    if (!dr.IsDBNull(dr.GetOrdinal("UsoCFDI")))
                    {
                        pedido.UsoCFDI = dr["UsoCFDI"].ToString();
                    }
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

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
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                SqlDataReader dr = null;
                bool pedidoEncontrado = false;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "Id_Ped" };
                object[] Valores = { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedido_Consultar", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    pedidoEncontrado = true;
                    pedido.Ped_Comentarios = dr.IsDBNull(dr.GetOrdinal("Ped_Comentarios")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Comentarios")).ToString();
                    pedido.Ped_DescPorcen1 = dr.IsDBNull(dr.GetOrdinal("Ped_Desc1")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_DescPorcen1")));
                    pedido.Ped_DescPorcen2 = dr.IsDBNull(dr.GetOrdinal("Ped_Desc2")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_DescPorcen2")));
                    pedido.Ped_CondEntrega = dr.IsDBNull(dr.GetOrdinal("Ped_CondEntrega")) ? 0 : (string.IsNullOrEmpty(dr.GetValue(dr.GetOrdinal("Ped_CondEntrega")).ToString()) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_CondEntrega"))));
                    pedido.Ped_Desc1 = dr.IsDBNull(dr.GetOrdinal("Ped_Desc1")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Desc1")).ToString();
                    pedido.Ped_Desc2 = dr.IsDBNull(dr.GetOrdinal("Ped_Desc2")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Desc2")).ToString();
                    pedido.Ped_Flete = dr.IsDBNull(dr.GetOrdinal("Ped_Flete")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Flete")).ToString();
                    pedido.Ped_Importe = dr.IsDBNull(dr.GetOrdinal("Ped_Importe")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Importe")));
                    pedido.Ped_Iva = dr.IsDBNull(dr.GetOrdinal("Ped_Iva")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Iva")));
                    pedido.Id_Cte = dr.IsDBNull(dr.GetOrdinal("Id_Cte")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    pedido.Ped_Observaciones = dr.IsDBNull(dr.GetOrdinal("Ped_Observaciones")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Observaciones")).ToString();
                    pedido.Ped_OrdenEntrega = dr.IsDBNull(dr.GetOrdinal("Ped_OrdenEntrega")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_OrdenEntrega")).ToString();
                    pedido.Pedido_del = dr.IsDBNull(dr.GetOrdinal("Ped_PedidoDel")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_PedidoDel")).ToString();
                    pedido.Id_Rik = dr.IsDBNull(dr.GetOrdinal("Id_Rik")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    pedido.Requisicion = dr.IsDBNull(dr.GetOrdinal("Ped_Requisicion")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Requisicion")).ToString();
                    pedido.Ped_Solicito = dr.IsDBNull(dr.GetOrdinal("Ped_Solicito")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Solicito")).ToString();
                    pedido.Ped_Subtotal = dr.IsDBNull(dr.GetOrdinal("Ped_Subtotal")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Subtotal")));
                    pedido.Id_Ter = dr.IsDBNull(dr.GetOrdinal("Id_Ter")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    pedido.Ped_Total = dr.IsDBNull(dr.GetOrdinal("Ped_Total")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Total")));
                    pedido.Estatus = dr.IsDBNull(dr.GetOrdinal("Ped_Estatus")) ? string.Empty : dr.GetValue(dr.GetOrdinal("Ped_Estatus")).ToString();
                    pedido.Cant_Facturada = dr.IsDBNull(dr.GetOrdinal("cant_Facturada")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("cant_Facturada")));
                    pedido.Ped_OrdenCompra = dr.IsDBNull(dr.GetOrdinal("Ped_OrdenCompra")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_OrdenCompra")).ToString();

                    pedido.Ped_Tipo = dr.IsDBNull(dr.GetOrdinal("Ped_Tipo")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Tipo")));
                    if (dr.IsDBNull(dr.GetOrdinal("Id_Fac")))
                        pedido.Id_Fac = null;
                    else
                        pedido.Id_Fac = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Fac")));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                return pedidoEncontrado;
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
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                SqlDataReader dr = null;
                bool pedidoEncontrado = false;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Pedido" };
                object[] Valores = { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spPedidosVI_validaOC", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    pedidoEncontrado = true;
                    pedido.Id_Emp = dr.IsDBNull(dr.GetOrdinal("id_emp")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_emp"));
                    pedido.Id_Cte = dr.IsDBNull(dr.GetOrdinal("id_cte")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_cte"));
                    pedido.Id_Ped = dr.IsDBNull(dr.GetOrdinal("Id_Ped")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Ped"));
                    pedido.Requisicion = dr.IsDBNull(dr.GetOrdinal("Ped_requisicion")) ? "" : dr.GetString(dr.GetOrdinal("Ped_requisicion"));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                return pedidoEncontrado;
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
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "Id_Ped" };
                object[] Valores = { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoTieneUnidRemisionadas_Consultar", ref dr, Parametros, Valores);
                bool unidadesRem = false;
                if (dr.HasRows)
                {
                    dr.Read();
                    unidadesRem = dr.IsDBNull(dr.GetOrdinal("UniRem")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("UniRem")));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                return unidadesRem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaPedidoDet(Pedido pedido, string Conexion, ref DataTable dt)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "Id_Ped", "Ped_Captacion", "@Filtro_Doc", "@ConMinimo" };
                object[] Valores = { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped, pedido.Ped_Captacion, pedido.Filtro_Doc, -1 };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDet_Consultar", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    if (pedido.Ped_Captacion == true)
                    {
                        dt.Rows.Add(new object[] {
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Prd_Descripcion")),
                            dr.GetValue(dr.GetOrdinal("Prd_Presentacion")),
                            dr.GetValue(dr.GetOrdinal("Id_Uni")),
                            dr.GetValue(dr.GetOrdinal("mes1")),
                            dr.GetValue(dr.GetOrdinal("mes2")),
                            dr.GetValue(dr.GetOrdinal("mes3")),
                            dr.GetValue(dr.GetOrdinal("Prd_Cantidad")),
                            dr.GetValue(dr.GetOrdinal("Prd_Precio")),
                            dr.GetValue(dr.GetOrdinal("Acs_PrecioAcys")),
                            dr.GetValue(dr.GetOrdinal("Prd_Importe")),
                            Str(dr.GetValue(dr.GetOrdinal("Acs_Documento"))),
                            dr.GetValue(dr.GetOrdinal("Acs_Fecha")),
                            dr.GetValue(dr.GetOrdinal("Acs_Mod")) ,
                            dr.GetValue(dr.GetOrdinal("Acs_Dia")) ,
                            Nombre(dr.GetValue(dr.GetOrdinal("Acs_Dia"))),
                            dr.GetValue(dr.GetOrdinal("Acs_Frecuencia")),
                            dr.GetValue(dr.GetOrdinal("Prd_RemFact")),
                            dr.GetValue(dr.GetOrdinal("Ped_Asignar"))
                       });
                    }
                    else
                    {
                        dt.Rows.Add(new object[] {
                            dr.GetValue(dr.GetOrdinal("Id_PedDet")),
                            dr.GetValue(dr.GetOrdinal("Id_Ter")),
                            dr.GetValue(dr.GetOrdinal("Ter_Nombre")),
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Prd_Descripcion")),
                            dr.GetValue(dr.GetOrdinal("Prd_Presentacion")),
                            dr.GetValue(dr.GetOrdinal("Prd_Unidad")),
                            dr.GetValue(dr.GetOrdinal("Prd_Precio")),
                            dr.GetValue(dr.GetOrdinal("Prd_Cantidad")),
                            dr.GetValue(dr.GetOrdinal("Prd_Importe"))
                    });
                    }
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaPedidoDet(Pedido pedido, string Conexion, ref List<OrdenCompra_Detalle> Lista)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "Id_Ped", "Ped_Captacion", "@Filtro_Doc", "@ConMinimo" };
                object[] Valores = { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped, pedido.Ped_Captacion, pedido.Filtro_Doc, -1 };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDet_Consultar", ref dr, Parametros, Valores);
                OrdenCompra_Detalle detalle;
                while (dr.Read())
                {
                    detalle = new OrdenCompra_Detalle();


                    detalle.Id_PrdOld = dr.IsDBNull(dr.GetOrdinal("Id_Prd")) ? 0 : dr.GetInt64(dr.GetOrdinal("Id_Prd"));
                    detalle.Id_Prd = dr.IsDBNull(dr.GetOrdinal("Id_Prd")) ? 0 : dr.GetInt64(dr.GetOrdinal("Id_Prd"));
                    detalle.Prd_Descripcion = dr.IsDBNull(dr.GetOrdinal("Prd_Descripcion")) ? "" : dr.GetString(dr.GetOrdinal("Prd_Descripcion"));
                    detalle.Prd_Presentacion = dr.IsDBNull(dr.GetOrdinal("Prd_Presentacion")) ? "" : dr.GetString(dr.GetOrdinal("Prd_Presentacion"));
                    detalle.Prd_Unidad = dr.IsDBNull(dr.GetOrdinal("Id_Uni")) ? "" : dr.GetString(dr.GetOrdinal("Id_Uni"));
                    detalle.mes1 = dr.IsDBNull(dr.GetOrdinal("mes1")) ? 0 : dr.GetInt32(dr.GetOrdinal("mes1"));
                    detalle.mes2 = dr.IsDBNull(dr.GetOrdinal("mes2")) ? 0 : dr.GetInt32(dr.GetOrdinal("mes2"));
                    detalle.mes3 = dr.IsDBNull(dr.GetOrdinal("mes3")) ? 0 : dr.GetInt32(dr.GetOrdinal("mes3"));
                    detalle.Prd_Cantidad = dr.IsDBNull(dr.GetOrdinal("Prd_Cantidad")) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Cantidad"));
                    detalle.Prd_Precio = dr.IsDBNull(dr.GetOrdinal("Prd_Precio")) ? 0 : dr.GetDouble(dr.GetOrdinal("Prd_Precio"));
                    detalle.Acs_PrecioAcys = dr.IsDBNull(dr.GetOrdinal("Acs_PrecioAcys")) ? 0 : dr.GetDouble(dr.GetOrdinal("Acs_PrecioAcys"));
                    detalle.Prd_Importe = dr.IsDBNull(dr.GetOrdinal("Prd_Importe")) ? 0 : dr.GetDouble(dr.GetOrdinal("Prd_Importe"));
                    detalle.Acs_Documento = dr.IsDBNull(dr.GetOrdinal("Acs_Documento")) ? "" : dr.GetString(dr.GetOrdinal("Acs_Documento"));
                    detalle.Acs_Fecha = dr.GetDateTime(dr.GetOrdinal("Acs_Fecha"));
                    detalle.Acs_Mod = dr.IsDBNull(dr.GetOrdinal("Acs_Mod")) ? false : dr.GetBoolean(dr.GetOrdinal("Acs_Mod"));
                    detalle.Acs_Dia = dr.IsDBNull(dr.GetOrdinal("Acs_Dia")) ? "" : dr.GetString(dr.GetOrdinal("Acs_Dia"));
                    detalle.Acs_DiaStr = Nombredia(dr.IsDBNull(dr.GetOrdinal("Acs_Dia")) ? "" : dr.GetString(dr.GetOrdinal("Acs_Dia")));
                    detalle.Acs_Frecuencia = dr.IsDBNull(dr.GetOrdinal("Acs_Frecuencia")) ? 0 : dr.GetInt32(dr.GetOrdinal("Acs_Frecuencia"));
                    detalle.Prd_RemFact = dr.IsDBNull(dr.GetOrdinal("Prd_RemFact")) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_RemFact"));
                    detalle.Ped_Asignar = dr.IsDBNull(dr.GetOrdinal("Ped_Asignar")) ? 0 : dr.GetInt32(dr.GetOrdinal("Ped_Asignar"));
                    detalle.Id_TG = 0;
                    detalle.Id_Acs = 0;
                    detalle.ACS_ReqOC = "";
                    detalle.Prd_PrecioLista = 0;
                    detalle.Tipo_producto = "O";
                    detalle.Prd_Cantidadold = dr.IsDBNull(dr.GetOrdinal("Prd_Cantidad")) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Cantidad"));
                    Lista.Add(detalle);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPedidoDetCN(Pedido pedido, string Conexion, ref List<OrdenCompra_Detalle> Lista)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Ped", "@Ped_Captacion", "@Filtro_Doc", "@ConMinimo" };
                object[] Valores = { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped, pedido.Ped_Captacion, pedido.Filtro_Doc, -1 };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDet_ConsultarCN", ref dr, Parametros, Valores);
                OrdenCompra_Detalle detalle;
                while (dr.Read())
                {
                    detalle = new OrdenCompra_Detalle();


                    detalle.Id_PrdOld = dr.IsDBNull(dr.GetOrdinal("id_prdOld")) ? 0 : dr.GetInt64(dr.GetOrdinal("id_prdOld"));
                    detalle.Id_Prd = dr.IsDBNull(dr.GetOrdinal("Id_Prd")) ? 0 : dr.GetInt64(dr.GetOrdinal("Id_Prd"));
                    detalle.Prd_Descripcion = dr.IsDBNull(dr.GetOrdinal("Prd_Descripcion")) ? "" : dr.GetString(dr.GetOrdinal("Prd_Descripcion"));
                    detalle.Prd_Presentacion = dr.IsDBNull(dr.GetOrdinal("Prd_Presentacion")) ? "" : dr.GetString(dr.GetOrdinal("Prd_Presentacion"));
                    detalle.Prd_Unidad = dr.IsDBNull(dr.GetOrdinal("Id_Uni")) ? "" : dr.GetString(dr.GetOrdinal("Id_Uni"));
                    detalle.mes1 = dr.IsDBNull(dr.GetOrdinal("mes1")) ? 0 : dr.GetInt32(dr.GetOrdinal("mes1"));
                    detalle.mes2 = dr.IsDBNull(dr.GetOrdinal("mes2")) ? 0 : dr.GetInt32(dr.GetOrdinal("mes2"));
                    detalle.mes3 = dr.IsDBNull(dr.GetOrdinal("mes3")) ? 0 : dr.GetInt32(dr.GetOrdinal("mes3"));
                    detalle.Prd_Cantidad = dr.IsDBNull(dr.GetOrdinal("Prd_Cantidad")) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Cantidad"));
                    detalle.Prd_Cantidadold = dr.IsDBNull(dr.GetOrdinal("Prd_Cantidadold")) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Cantidadold"));
                    detalle.Prd_Precio = dr.IsDBNull(dr.GetOrdinal("Prd_Precio")) ? 0 : dr.GetDouble(dr.GetOrdinal("Prd_Precio"));
                    detalle.Acs_PrecioAcys = dr.IsDBNull(dr.GetOrdinal("Acs_PrecioAcys")) ? 0 : dr.GetDouble(dr.GetOrdinal("Acs_PrecioAcys"));
                    detalle.Prd_Importe = dr.IsDBNull(dr.GetOrdinal("Prd_Importe")) ? 0 : dr.GetDouble(dr.GetOrdinal("Prd_Importe"));
                    detalle.Acs_Documento = dr.IsDBNull(dr.GetOrdinal("Acs_Documento")) ? "" : dr.GetString(dr.GetOrdinal("Acs_Documento"));
                    detalle.Acs_Fecha = dr.GetDateTime(dr.GetOrdinal("Acs_Fecha"));
                    detalle.Acs_Mod = dr.IsDBNull(dr.GetOrdinal("Acs_Mod")) ? false : dr.GetBoolean(dr.GetOrdinal("Acs_Mod"));
                    detalle.Acs_Dia = dr.IsDBNull(dr.GetOrdinal("Acs_Dia")) ? "" : dr.GetString(dr.GetOrdinal("Acs_Dia"));
                    detalle.Acs_DiaStr = Nombredia(dr.IsDBNull(dr.GetOrdinal("Acs_Dia")) ? "" : dr.GetString(dr.GetOrdinal("Acs_Dia")));
                    detalle.Acs_Frecuencia = dr.IsDBNull(dr.GetOrdinal("Acs_Frecuencia")) ? 0 : dr.GetInt32(dr.GetOrdinal("Acs_Frecuencia"));
                    detalle.Prd_RemFact = dr.IsDBNull(dr.GetOrdinal("Prd_RemFact")) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_RemFact"));
                    detalle.Ped_Asignar = dr.IsDBNull(dr.GetOrdinal("Ped_Asignar")) ? 0 : dr.GetInt32(dr.GetOrdinal("Ped_Asignar"));
                    detalle.Id_TG = 0;
                    detalle.Id_Acs = 0;
                    detalle.ACS_ReqOC = "";
                    detalle.Prd_PrecioLista = 0;
                    if (detalle.Id_PrdOld == detalle.Id_Prd)
                    {
                        detalle.Tipo_producto = "O";
                        if (detalle.Prd_Cantidad == 0 && detalle.Prd_Cantidad != detalle.Prd_Cantidadold)
                        {
                            detalle.Tipo_producto = "B";
                        }
                        else if (detalle.Prd_Cantidad != 0 && detalle.Prd_Cantidad != detalle.Prd_Cantidadold)
                        {
                            detalle.Tipo_producto = "P";
                        }
                    }
                    else
                    {
                        detalle.Tipo_producto = "S";
                    }

                    Lista.Add(detalle);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

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
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@IdOC", "@Id_Cd", "@Id_Cte" };
                object[] Valores = { idOC, Id_Cd, Id_Cte };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatCNac_OrdenCompra_Detalle", ref dr, Parametros, Valores);
                OrdenCompra_Detalle detalle;
                while (dr.Read())
                {
                    detalle = new OrdenCompra_Detalle();

                    detalle.Id_PrdOld = dr.IsDBNull(dr.GetOrdinal("Id_Prd")) ? 0 : dr.GetInt64(dr.GetOrdinal("Id_Prd"));
                    detalle.Id_Prd = dr.IsDBNull(dr.GetOrdinal("Id_Prd")) ? 0 : dr.GetInt64(dr.GetOrdinal("Id_Prd"));
                    detalle.Prd_Activo = dr.IsDBNull(dr.GetOrdinal("Prd_Activo")) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Activo"));
                    detalle.Prd_Descripcion = dr.IsDBNull(dr.GetOrdinal("Prd_Descripcion")) ? "" : dr.GetString(dr.GetOrdinal("Prd_Descripcion"));
                    detalle.Prd_Presentacion = dr.IsDBNull(dr.GetOrdinal("Prd_Presentacion")) ? "" : dr.GetString(dr.GetOrdinal("Prd_Presentacion"));
                    detalle.Prd_Unidad = dr.IsDBNull(dr.GetOrdinal("Id_Uni")) ? "" : dr.GetString(dr.GetOrdinal("Id_Uni"));
                    detalle.mes1 = dr.IsDBNull(dr.GetOrdinal("mes1")) ? 0 : dr.GetInt32(dr.GetOrdinal("mes1"));
                    detalle.mes2 = dr.IsDBNull(dr.GetOrdinal("mes2")) ? 0 : dr.GetInt32(dr.GetOrdinal("mes2"));
                    detalle.mes3 = dr.IsDBNull(dr.GetOrdinal("mes3")) ? 0 : dr.GetInt32(dr.GetOrdinal("mes3"));
                    detalle.Prd_Cantidad = dr.IsDBNull(dr.GetOrdinal("Prd_Cantidad")) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Cantidad"));
                    detalle.Prd_Cantidadold = dr.IsDBNull(dr.GetOrdinal("Prd_Cantidad")) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_Cantidad"));
                    detalle.Prd_Precio = dr.IsDBNull(dr.GetOrdinal("Prd_Precio")) ? 0 : dr.GetDouble(dr.GetOrdinal("Prd_Precio"));
                    detalle.Acs_PrecioAcys = 0;
                    detalle.Prd_Importe = dr.IsDBNull(dr.GetOrdinal("Prd_Importe")) ? 0 : dr.GetDouble(dr.GetOrdinal("Prd_Importe"));
                    detalle.Acs_Documento = dr.IsDBNull(dr.GetOrdinal("Acs_Documento")) ? "" : dr.GetString(dr.GetOrdinal("Acs_Documento"));
                    detalle.Acs_Fecha = dr.GetDateTime(dr.GetOrdinal("Acs_Fecha"));
                    detalle.Acs_Mod = false;
                    detalle.Acs_Dia = dr.IsDBNull(dr.GetOrdinal("Acs_Dia")) ? "" : dr.GetString(dr.GetOrdinal("Acs_Dia"));
                    detalle.Acs_DiaStr = Nombredia(dr.IsDBNull(dr.GetOrdinal("Acs_Dia")) ? "" : dr.GetString(dr.GetOrdinal("Acs_Dia")));
                    detalle.Acs_Frecuencia = dr.IsDBNull(dr.GetOrdinal("Acs_Frecuencia")) ? 0 : dr.GetInt32(dr.GetOrdinal("Acs_Frecuencia"));
                    detalle.Prd_RemFact = dr.IsDBNull(dr.GetOrdinal("Prd_RemFact")) ? 0 : dr.GetInt32(dr.GetOrdinal("Prd_RemFact"));
                    detalle.Ped_Asignar = dr.IsDBNull(dr.GetOrdinal("Ped_Asignar")) ? 0 : dr.GetInt32(dr.GetOrdinal("Ped_Asignar"));
                    detalle.Id_TG = 0;
                    detalle.Id_Acs = 0;
                    detalle.ACS_ReqOC = "";
                    detalle.Prd_PrecioLista = dr.IsDBNull(dr.GetOrdinal("PrecioLista")) ? 0 : dr.GetDouble(dr.GetOrdinal("PrecioLista")); ;
                    if (detalle.Id_PrdOld == detalle.Id_Prd)
                    {
                        detalle.Tipo_producto = "O";
                        if (detalle.Prd_Cantidad == 0 && detalle.Prd_Cantidad != detalle.Prd_Cantidadold)
                        {
                            detalle.Tipo_producto = "B";
                        }
                        else if (detalle.Prd_Cantidad != 0 && detalle.Prd_Cantidad != detalle.Prd_Cantidadold)
                        {
                            detalle.Tipo_producto = "P";
                        }
                    }
                    else
                    {
                        detalle.Tipo_producto = "S";
                    }

                    Lista.Add(detalle);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        private object Str(object p)
        {
            switch (p.ToString().ToUpper())
            {
                case "F": return "Factura";
                case "R": return "Remisión";
                default: return "";
            }
        }
        public void ConsultaPedidoDetDisp(Pedido pedido, string Conexion, int? facturando, ref DataTable dt)
        {//rm
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                SqlDataReader dr = null;


                string StrTraePicking = "";
                if (pedido.TraePicking == null)
                {
                    StrTraePicking = "N";
                }
                else
                {
                    StrTraePicking = pedido.TraePicking;
                }


                string[] Parametros = { "@Id_Emp", "@Id_Cd", "Id_Ped", "@Ped_Captacion", "@Filtro_Doc", "@ConMinimo", "@Facturando", "@TraePicking" };
                object[] Valores = { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped, null, pedido.Filtro_Doc, null, facturando, StrTraePicking };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDet_Consultar", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    if (Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Disponible"))) > 0)
                    {
                        dt.Rows.Add(new object[] {
                                               dr.GetValue(dr.GetOrdinal("Id_PedDet")),
                                               dr.GetValue(dr.GetOrdinal("Id_Ter")),
                                               dr.GetValue(dr.GetOrdinal("Ter_Nombre")),
                                               dr.GetValue(dr.GetOrdinal("Id_Prd")),
                                               dr.GetValue(dr.GetOrdinal("Prd_Descripcion")),
                                               dr.GetValue(dr.GetOrdinal("Prd_Presentacion")),
                                               dr.GetValue(dr.GetOrdinal("Prd_Unidad")),
                                               dr.GetValue(dr.GetOrdinal("Prd_Precio")),
                                               dr.GetValue(dr.GetOrdinal("Disponible")),
                                               dr.GetValue(dr.GetOrdinal("Prd_Importe")),
                                               dr.GetValue(dr.GetOrdinal("Id_Rem"))
                        });
                    }
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private object Nombre(object p)
        {
            switch (p.ToString().ToLower())
            {
                case "l": return "Lunes";
                case "m": return "Martes";
                case "mi": return "Miercoles";
                case "j": return "Jueves";
                case "v": return "Viernes";
                case "s": return "Sabado";
                default: return "";
            }
        }


        private string Nombredia(string p)
        {
            switch (p.ToString().ToLower())
            {
                case "l": return "Lunes";
                case "m": return "Martes";
                case "mi": return "Miercoles";
                case "j": return "Jueves";
                case "v": return "Viernes";
                case "s": return "Sabado";
                default: return "";
            }
        }
        public void InsertarPedido(Pedido pedido, DataTable dt, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Cte",
                                        "@Ped_Fecha",
                                        "@Id_Rik",
                                        "@Id_Ter",
                                        "@Pedido_del",
                                        "@Requisicion",
                                        "@Ped_Solicito",
                                        "@Ped_Flete",
                                        "@Ped_OrdenEntrega",
                                        "@Ped_CondEntrega",
                                        "@Ped_FechaEntrega",
                                        "@Ped_Observaciones",
                                        "@Ped_DescPorcen1",
                                        "@Ped_DescPorcen2",
                                        "@Ped_Desc1",
                                        "@Ped_Desc2",
                                        "@Ped_Comentarios",
                                        "@Ped_Importe",
                                        "@Ped_Subtotal",
                                        "@Ped_Iva",
                                        "@Ped_Total",
                                        "@Ped_Estatus",
                                        "@Id_U",
                                        "@Ped_Tipo"
                                      };
                object[] Valores = {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Cte,
                                        pedido.Ped_Fecha,
                                        pedido.Id_Rik,
                                        pedido.Id_Ter,
                                        pedido.Pedido_del,
                                        pedido.Requisicion,
                                        pedido.Ped_Solicito,
                                        pedido.Ped_Flete,
                                        pedido.Ped_OrdenEntrega,
                                        pedido.Ped_CondEntrega,
                                        pedido.Ped_FechaEntrega,
                                        pedido.Ped_Observaciones,
                                        pedido.Ped_DescPorcen1,
                                        pedido.Ped_DescPorcen2,
                                        pedido.Ped_Desc1,
                                        pedido.Ped_Desc2,
                                        pedido.Ped_Comentarios,
                                        pedido.Ped_Importe,
                                        pedido.Ped_Subtotal,
                                        pedido.Ped_Iva,
                                        pedido.Ped_Total,
                                        pedido.Estatus,
                                        pedido.Id_U,
                                        pedido.Ped_Tipo
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedido_Insertar", ref verificador, Parametros, Valores);
                pedido.Id_Ped = verificador;
                if (verificador > -1)
                {
                    verificador = -1;
                    ModificarDet(pedido, dt, ref verificador, CapaDatos, ref Parametros, ref Valores, ref sqlcmd);
                    CapaDatos.CommitTrans();
                }
                else
                    CapaDatos.RollBackTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }
        public void ModificarPedido(Pedido pedido, DataTable dt, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = null;
                object[] Valores = null;
                SqlCommand sqlcmd = default(SqlCommand);
                Parametros = new string[] {
                                            "@Id_Emp",
                                            "@Id_Cd",
                                            "@Id_Ped",
                                            "@Ped_Fecha",
                                            "@Id_Cte",
                                            "@Id_Rik",
                                            "@Id_Ter",
                                            "@Pedido_del",
                                            "@Ped_Solicito",
                                            "@Requisicion",
                                            "@Ped_Flete",
                                            "@Ped_OrdenEntrega",
                                            "@Ped_CondEntrega",
                                            "@Ped_FechaEntrega",
                                            "@Ped_Observaciones",
                                            "@Ped_DescPorcen1",
                                            "@Ped_DescPorcen2",
                                            "@Ped_Desc1",
                                            "@Ped_Desc2",
                                            "@Ped_Comentarios",
                                            "@Ped_Importe",
                                            "@Ped_Subtotal",
                                            "@Ped_Iva",
                                            "@Ped_Total",
                                            "@Id_U"
                                      };
                Valores = new object[]{
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Ped,
                                        pedido.Ped_Fecha,
                                        pedido.Id_Cte,
                                        pedido.Id_Rik,
                                        pedido.Id_Ter,
                                        pedido.Pedido_del,
                                        pedido.Ped_Solicito,
                                        pedido.Requisicion,
                                        pedido.Ped_Flete,
                                        pedido.Ped_OrdenEntrega,
                                        pedido.Ped_CondEntrega,
                                        pedido.Ped_FechaEntrega,
                                        pedido.Ped_Observaciones,
                                        pedido.Ped_DescPorcen1,
                                        pedido.Ped_DescPorcen2,
                                        pedido.Ped_Desc1,
                                        pedido.Ped_Desc2,
                                        pedido.Ped_Comentarios,
                                        pedido.Ped_Importe,
                                        pedido.Ped_Subtotal,
                                        pedido.Ped_Iva,
                                        pedido.Ped_Total,
                                        pedido.Id_U
                                   };

                sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedido_Modificar", ref verificador, Parametros, Valores);
                if (verificador == 1)
                {
                    ModificarDet(pedido, dt, ref verificador, CapaDatos, ref Parametros, ref Valores, ref sqlcmd);
                    CapaDatos.CommitTrans();
                }
                else
                {
                    CapaDatos.RollBackTrans();
                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }
        private static void ModificarDet(Pedido pedido, DataTable dt, ref int verificador, CapaDatos.CD_Datos CapaDatos, ref string[] Parametros, ref object[] Valores, ref SqlCommand sqlcmd)
        {
            if (dt.Rows.Count == 0) return;

            Parametros = new string[]{
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Ped",
                                        "@Id_PedDet",
                                        "@Id_Ter",
                                        "@Id_Prd",
                                        "@Ped_Precio",
                                        "@Ped_Cantidad",
                                        "@Accion"
                                      };
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                Valores = new object[] {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Ped,
                                        x,
                                        dt.Rows[x]["Id_Ter"],
                                        dt.Rows[x]["Id_Prd"],
                                        dt.Rows[x]["Prd_Precio"],
                                        dt.Rows[x]["Prd_Cantidad"],
                                        x
                                   };
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDet_Insertar", ref verificador, Parametros, Valores);
            }
        }
        private string Tipo(string p)
        {
            switch (p)
            {
                case "1":
                    return "Sin distribución";
                case "2":
                    return "Con distribución";
                case "3":
                    return "Venta instalada";
                case "4":
                    return "Venta nueva";
                case "5":
                    return "Internet";
                default:
                    return "";
            }
        }
        private string Estatus(string p)
        {
            switch (p)
            {
                case "O": return "Confirmado";
                case "C": return "Capturado";
                case "I": return "Impreso";
                case "U": return "Autorizado";
                case "A": return "Asignado";
                case "F": return "Facturado";
                case "R": return "Remisionado";
                case "X": return "Facturado/Remisionado";
                case "E": return "Embarque";
                case "N": return "Entregado";
                case "D": return "Baja por administración";
                case "B": return "Baja por cliente";
                default: return "";
            }
        }
        public void Baja(Pedido ped, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Ped",
                                        "@Estatus"
                                      };
                object[] Valores = {
                                        ped.Id_Emp,
                                        ped.Id_Cd,
                                        ped.Id_Ped,
                                        ped.Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedido_Baja", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void logCancelarPedido(Pedido ped, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                string[] Parametros = {

                                        "@Id_Cd",
                                        "@Id_Ped",
                                        "@id_u",
                                        "@fecha",
                                        "@Modulo"
                                      };
                object[] Valores = {
                                        ped.Id_Cd,
                                        ped.Id_Ped,
                                        ped.Id_U,
                                        ped.Ped_Fecha,
                                        ped.Modulo
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("splogCancelarPedido", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Imprimir(Pedido ped, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Ped",
                                        "@Estatus"
                                      };
                object[] Valores = {
                                        ped.Id_Emp,
                                        ped.Id_Cd,
                                        ped.Id_Ped,
                                        ped.Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedido_Imprimir", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Filtro_Nombre",
                                          "@Filtro_CteIni",
                                          "@Filtro_CteFin",
                                          "@Filtro_Tipo",
                                          "@Filtro_FecIni",
                                          "@Filtro_FecFin",
                                          "@Filtro_Estatus",
                                          "@Filtro_PedIni",
                                          "@Filtro_PedFin",
                                          "@Filtro_usuario",
                                      };
                object[] Valores = {
                                       pedido.Id_Emp,
                                       pedido.Id_Cd,
                                       pedido.Filtro_Nombre == "" ? (object)null : pedido.Filtro_Nombre,
                                       pedido.Filtro_CteIni == ""? (object)null: pedido.Filtro_CteIni,
                                       pedido.Filtro_CteFin == ""? (object)null: pedido.Filtro_CteFin,
                                       pedido.Filtro_Tipo,
                                       pedido.Filtro_FecIni,
                                       pedido.Filtro_FecFin ,
                                       pedido.Filtro_Estatus,
                                       pedido.Filtro_PedIni,
                                       pedido.Filtro_PedFin,
                                       pedido.Filtro_usuario == "" || pedido.Filtro_usuario == "-1" ? (object)null : pedido.Filtro_usuario
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedido_Lista", ref dr, Parametros, Valores);

                Pedido p;
                while (dr.Read())
                {
                    p = new Pedido();
                    p.Ped_Tipo = dr.IsDBNull(dr.GetOrdinal("Ped_Tipo")) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Tipo")));
                    p.Ped_TipoStr = dr.IsDBNull(dr.GetOrdinal("Ped_Tipo")) ? "" : Tipo(dr.GetValue(dr.GetOrdinal("Ped_Tipo")).ToString());
                    p.Id_U = dr.IsDBNull(dr.GetOrdinal("Id_U")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));
                    p.U_Nombre = dr.GetValue(dr.GetOrdinal("U_Nombre")).ToString();
                    p.Estatus = dr.IsDBNull(dr.GetOrdinal("Ped_Estatus")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Estatus")).ToString();
                    p.EstatusStr = dr.IsDBNull(dr.GetOrdinal("Ped_Estatus")) ? "" : Estatus(dr.GetValue(dr.GetOrdinal("Ped_Estatus")).ToString());
                    p.Id_Ped = dr.IsDBNull(dr.GetOrdinal("Id_Ped")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ped")));
                    p.Ped_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ped_Fecha")));
                    p.Id_Cte = dr.IsDBNull(dr.GetOrdinal("Id_Cte")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    p.Cte_NomComercial = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    p.Ped_Subtotal = dr.IsDBNull(dr.GetOrdinal("Ped_Subtotal")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Subtotal")));
                    p.Ped_Iva = dr.IsDBNull(dr.GetOrdinal("Ped_Iva")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Iva")));
                    p.Ped_Total = dr.IsDBNull(dr.GetOrdinal("Ped_Total")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Total")));
                    p.Facturacion = dr.IsDBNull(dr.GetOrdinal("Cte_Facturacion")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_Facturacion")));
                    List.Add(p);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                string aplicaAutorizacion = System.Configuration.ConfigurationManager.AppSettings["AplicaAutorizacion"].ToString();
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Filtro_Nombre",
                                          "@Filtro_CteIni",
                                          "@Filtro_CteFin",

                                          "@Filtro_FecIni",
                                          "@Filtro_FecFin",

                                          "@Filtro_PedIni",
                                          "@Filtro_PedFin",

                                          "@Filtro_RutaIni",
                                          "@Filtro_RutaFin",

                                          "@Filtro_SectorIni",
                                          "@Filtro_SectorFin",

                                          "@Filtro_Credito",
                                          "@Filtro_Autorizados"

                                      };
                object[] Valores = {
                                       pedido.Id_Emp,
                                       pedido.Id_Cd,
                                       pedido.Filtro_Nombre  ,
                                       pedido.Filtro_CteIni == ""? (object)null: pedido.Filtro_CteIni,
                                       pedido.Filtro_CteFin== ""? (object)null: pedido.Filtro_CteFin,
                                       pedido.Filtro_FecIni,
                                       pedido.Filtro_FecFin,
                                       pedido.Filtro_PedIni,
                                       pedido.Filtro_PedFin,
                                       pedido.Filtro_RutaInicial,
                                       pedido.Filtro_RutaFinal,
                                       pedido.Filtro_SectorInicial,
                                       pedido.Filtro_SectorFinal,
                                       pedido.Filtro_Credito,
                                       aplicaAutorizacion
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProAsignPrdxPed_Lista", ref dr, Parametros, Valores);

                Pedido p;
                while (dr.Read())
                {
                    p = new Pedido();
                    p.Id_Ped = dr.IsDBNull(dr.GetOrdinal("Id_Ped")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ped")));
                    p.Ped_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ped_Fecha")));
                    p.Id_Cte = dr.IsDBNull(dr.GetOrdinal("Id_Cte")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    p.Cte_NomComercial = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    p.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    p.Credito = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_Credito")));
                    p.CreditoStr = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_Credito"))) ? "Si" : "No";
                    p.Ped_Cantidad = dr.IsDBNull(dr.GetOrdinal("Ped_Cantidad")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Cantidad")));
                    p.Ped_CantidadDisponible = dr.IsDBNull(dr.GetOrdinal("Ped_CantidadDisponible")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_CantidadDisponible")));
                    p.Ped_ImporteOrdenado = dr.IsDBNull(dr.GetOrdinal("Ped_ImporteOrdenado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_ImporteOrdenado")));
                    p.Ped_ImporteDisponible = dr.IsDBNull(dr.GetOrdinal("Ped_ImporteDisponible")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_ImporteDisponible")));
                    p.Ped_Asignado = dr.IsDBNull(dr.GetOrdinal("Ped_Asignado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Asignado")));
                    p.Ped_ImporteAsignado = dr.IsDBNull(dr.GetOrdinal("Ped_ImporteAsignado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_ImporteAsignado")));
                    p.Sector = dr["Sector"].ToString();
                    p.Ruta = dr["Ruta"].ToString();
                    p.Secuencia = dr.IsDBNull(dr.GetOrdinal("Secuencia")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Secuencia")));
                    p.Ped_PorcentajeCantidadDisponible = dr.IsDBNull(dr.GetOrdinal("Ped_PorcentajeCantidadDisponible")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_PorcentajeCantidadDisponible")));
                    p.Ped_PorcentajeImporteDisponible = dr.IsDBNull(dr.GetOrdinal("Ped_PorcentajeImporteDisponible")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_PorcentajeImporteDisponible")));
                    p.Ped_PorcentajeAsignado = dr.IsDBNull(dr.GetOrdinal("Ped_PorcentajeAsignado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_PorcentajeAsignado")));
                    p.Ped_PorcentajeImporteAsignado = dr.IsDBNull(dr.GetOrdinal("Ped_PorcentajeImporteAsignado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_PorcentajeImporteAsignado")));
                    p.Ped_FechaEntrega = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ped_FechaEntrega")));
                    List.Add(p);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Filtro_Nombre",
                                          "@Filtro_CteIni",
                                          "@Filtro_CteFin",

                                          "@Filtro_FecIni",
                                          "@Filtro_FecFin",

                                          "@Filtro_PedIni",
                                          "@Filtro_PedFin",

                                          "@Filtro_RutaIni",
                                          "@Filtro_RutaFin",

                                          "@Filtro_SectorIni",
                                          "@Filtro_SectorFin",

                                          "@Filtro_Credito",

                                          "@Filtro_FecEntregaIni",
                                          "@Filtro_FecEntregaFin",
                                          "@Filtro_Estatus"

                                      };
                object[] Valores = {
                                       pedido.Id_Emp,
                                       pedido.Id_Cd,
                                       pedido.Filtro_Nombre  ,
                                       pedido.Filtro_CteIni == ""? (object)null: pedido.Filtro_CteIni,
                                       pedido.Filtro_CteFin== ""? (object)null: pedido.Filtro_CteFin,
                                       pedido.Filtro_FecIni,
                                       pedido.Filtro_FecFin,
                                       pedido.Filtro_PedIni,
                                       pedido.Filtro_PedFin,
                                       pedido.Filtro_RutaInicial,
                                       pedido.Filtro_RutaFinal,
                                       pedido.Filtro_SectorInicial,
                                       pedido.Filtro_SectorFinal,
                                       pedido.Filtro_Credito,
                                       pedido.Filtro_FecEntregaIni,
                                       pedido.Filtro_FecEntregaFin,
                                       Convert.ToInt32(pedido.Filtro_Estatus)
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProAsignPrdxPed_Lista_FechaEntrega", ref dr, Parametros, Valores);

                Pedido p;
                while (dr.Read())
                {
                    p = new Pedido();
                    p.Id_Ped = dr.IsDBNull(dr.GetOrdinal("Id_Ped")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ped")));
                    p.Ped_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ped_Fecha")));
                    p.Id_Cte = dr.IsDBNull(dr.GetOrdinal("Id_Cte")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    p.Cte_NomComercial = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    p.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    p.Credito = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_Credito")));
                    p.CreditoStr = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_Credito"))) ? "Si" : "No";
                    p.Ped_Cantidad = dr.IsDBNull(dr.GetOrdinal("Ped_Cantidad")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Cantidad")));
                    p.Ped_CantidadDisponible = dr.IsDBNull(dr.GetOrdinal("Ped_CantidadDisponible")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_CantidadDisponible")));
                    p.Ped_ImporteOrdenado = dr.IsDBNull(dr.GetOrdinal("Ped_ImporteOrdenado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_ImporteOrdenado")));
                    p.Ped_ImporteDisponible = dr.IsDBNull(dr.GetOrdinal("Ped_ImporteDisponible")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_ImporteDisponible")));
                    p.Ped_Asignado = dr.IsDBNull(dr.GetOrdinal("Ped_Asignado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Asignado")));
                    p.Ped_ImporteAsignado = dr.IsDBNull(dr.GetOrdinal("Ped_ImporteAsignado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_ImporteAsignado")));
                    p.Sector = dr["Sector"].ToString();
                    p.Ruta = dr["Ruta"].ToString();
                    p.Secuencia = dr.IsDBNull(dr.GetOrdinal("Secuencia")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Secuencia")));
                    p.Ped_PorcentajeCantidadDisponible = dr.IsDBNull(dr.GetOrdinal("Ped_PorcentajeCantidadDisponible")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_PorcentajeCantidadDisponible")));
                    p.Ped_PorcentajeImporteDisponible = dr.IsDBNull(dr.GetOrdinal("Ped_PorcentajeImporteDisponible")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_PorcentajeImporteDisponible")));
                    p.Ped_PorcentajeAsignado = dr.IsDBNull(dr.GetOrdinal("Ped_PorcentajeAsignado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_PorcentajeAsignado")));
                    p.Ped_PorcentajeImporteAsignado = dr.IsDBNull(dr.GetOrdinal("Ped_PorcentajeImporteAsignado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_PorcentajeImporteAsignado")));
                    p.Ped_FechaEntrega = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ped_FechaEntrega")));
                    p.TipoPedido = dr["TipoPedido"].ToString();

                    List.Add(p);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Filtro_Nombre",
                                          "@Filtro_CteIni",
                                          "@Filtro_CteFin",

                                          "@Filtro_FecIni",
                                          "@Filtro_FecFin",

                                          "@Filtro_PedIni",
                                          "@Filtro_PedFin",

                                          "@Filtro_RutaIni",
                                          "@Filtro_RutaFin",

                                          "@Filtro_SectorIni",
                                          "@Filtro_SectorFin",

                                          "@Filtro_Credito",

                                          "@Filtro_FecEntregaIni",
                                          "@Filtro_FecEntregaFin",
                                          "@Filtro_Estatus",
                                          "@Rut_Descripcion",
                                          "@Id_Aux",
                                          "@Id_Cte"

                                      };
                object[] Valores = {
                                       pedido.Id_Emp,
                                       pedido.Id_Cd,
                                       pedido.Filtro_Nombre  ,
                                       pedido.Filtro_CteIni == ""? (object)null: pedido.Filtro_CteIni,
                                       pedido.Filtro_CteFin== ""? (object)null: pedido.Filtro_CteFin,
                                       pedido.Filtro_FecIni,
                                       pedido.Filtro_FecFin,
                                       pedido.Filtro_PedIni,
                                       pedido.Filtro_PedFin,
                                       pedido.Filtro_RutaInicial,
                                       pedido.Filtro_RutaFinal,
                                       pedido.Filtro_SectorInicial,
                                       pedido.Filtro_SectorFinal,
                                       pedido.Filtro_Credito,
                                       pedido.Filtro_FecEntregaIni,
                                       pedido.Filtro_FecEntregaFin,
                                       Convert.ToInt32(pedido.Filtro_Estatus),
                                       pedido.Ruta,
                                       pedido.Id_Aux,
                                       pedido.Id_Cte
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProAsignPrdxPed_Lista_FechaEntrega_Picking_Sin_Identificar", ref dr, Parametros, Valores);

                Pedido p;
                while (dr.Read())
                {
                    p = new Pedido();
                    p.Id_Ped = dr.IsDBNull(dr.GetOrdinal("Id_Ped")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ped")));
                    p.Ped_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ped_Fecha")));
                    p.Id_Cte = dr.IsDBNull(dr.GetOrdinal("Id_Cte")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    p.Cte_NomComercial = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    p.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    p.Credito = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_Credito")));
                    p.CreditoStr = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_Credito"))) ? "Si" : "No";
                    p.Ped_Cantidad = dr.IsDBNull(dr.GetOrdinal("Ped_Cantidad")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Cantidad")));
                    p.Ped_CantidadDisponible = dr.IsDBNull(dr.GetOrdinal("Ped_CantidadDisponible")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_CantidadDisponible")));
                    p.Ped_ImporteOrdenado = dr.IsDBNull(dr.GetOrdinal("Ped_ImporteOrdenado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_ImporteOrdenado")));
                    p.Ped_ImporteDisponible = dr.IsDBNull(dr.GetOrdinal("Ped_ImporteDisponible")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_ImporteDisponible")));
                    p.Ped_Asignado = dr.IsDBNull(dr.GetOrdinal("Ped_Asignado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Asignado")));
                    p.Ped_Picking = dr.IsDBNull(dr.GetOrdinal("Ped_picking")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_picking")));

                    p.Ped_Facturado = dr.IsDBNull(dr.GetOrdinal("Ped_Facturado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Facturado")));
                    p.Ped_Remisionado = dr.IsDBNull(dr.GetOrdinal("Ped_Remisionado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Remisionado")));
                    p.Ped_Pendiente = dr.IsDBNull(dr.GetOrdinal("Ped_Pendiente")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Pendiente")));

                    p.Ped_ImporteAsignado = dr.IsDBNull(dr.GetOrdinal("Ped_ImporteAsignado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_ImporteAsignado")));
                    p.Sector = dr["Sector"].ToString();
                    p.Ruta = dr["Ruta"].ToString();
                    p.Secuencia = dr.IsDBNull(dr.GetOrdinal("Secuencia")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Secuencia")));
                    p.Ped_PorcentajeCantidadDisponible = dr.IsDBNull(dr.GetOrdinal("Ped_PorcentajeCantidadDisponible")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_PorcentajeCantidadDisponible")));
                    p.Ped_PorcentajeImporteDisponible = dr.IsDBNull(dr.GetOrdinal("Ped_PorcentajeImporteDisponible")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_PorcentajeImporteDisponible")));
                    p.Ped_PorcentajeAsignado = dr.IsDBNull(dr.GetOrdinal("Ped_PorcentajeAsignado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_PorcentajeAsignado")));
                    p.Ped_PorcentajeImporteAsignado = dr.IsDBNull(dr.GetOrdinal("Ped_PorcentajeImporteAsignado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_PorcentajeImporteAsignado")));
                    p.Ped_FechaEntrega = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ped_FechaEntrega")));
                    p.TipoPedido = dr["TipoPedido"].ToString();
                    p.Ped_PermiteParcialidades = dr["Ped_PermiteParcialidades"].ToString();
                    List.Add(p);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Filtro_Nombre",
                                          "@Filtro_CteIni",
                                          "@Filtro_CteFin",

                                          "@Filtro_FecIni",
                                          "@Filtro_FecFin",

                                          "@Filtro_PedIni",
                                          "@Filtro_PedFin",

                                          "@Filtro_RutaIni",
                                          "@Filtro_RutaFin",

                                          "@Filtro_SectorIni",
                                          "@Filtro_SectorFin",

                                          "@Filtro_Credito",

                                          "@Filtro_FecEntregaIni",
                                          "@Filtro_FecEntregaFin",
                                          "@Filtro_Estatus",
                                          "@Rut_Descripcion",
                                          "@Id_Aux",
                                          "@Id_Cte"

                                      };
                object[] Valores = {
                                       pedido.Id_Emp,
                                       pedido.Id_Cd,
                                       pedido.Filtro_Nombre  ,
                                       pedido.Filtro_CteIni == ""? (object)null: pedido.Filtro_CteIni,
                                       pedido.Filtro_CteFin== ""? (object)null: pedido.Filtro_CteFin,
                                       pedido.Filtro_FecIni,
                                       pedido.Filtro_FecFin,
                                       pedido.Filtro_PedIni,
                                       pedido.Filtro_PedFin,
                                       pedido.Filtro_RutaInicial,
                                       pedido.Filtro_RutaFinal,
                                       pedido.Filtro_SectorInicial,
                                       pedido.Filtro_SectorFinal,
                                       pedido.Filtro_Credito,
                                       pedido.Filtro_FecEntregaIni,
                                       pedido.Filtro_FecEntregaFin,
                                       Convert.ToInt32(pedido.Filtro_Estatus),
                                       pedido.Ruta,
                                       pedido.Id_Aux,
                                       pedido.Id_Cte
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProAsignPrdxPed_Lista_FechaEntrega_Picking", ref dr, Parametros, Valores);

                Pedido p;
                while (dr.Read())
                {
                    p = new Pedido();
                    p.Id_Ped = dr.IsDBNull(dr.GetOrdinal("Id_Ped")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ped")));
                    p.Ped_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ped_Fecha")));
                    p.Id_Cte = dr.IsDBNull(dr.GetOrdinal("Id_Cte")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    p.Cte_NomComercial = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    p.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    p.Credito = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_Credito")));
                    p.CreditoStr = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_Credito"))) ? "Si" : "No";
                    p.Ped_Cantidad = dr.IsDBNull(dr.GetOrdinal("Ped_Cantidad")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Cantidad")));
                    p.Ped_CantidadDisponible = dr.IsDBNull(dr.GetOrdinal("Ped_CantidadDisponible")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_CantidadDisponible")));
                    p.Ped_ImporteOrdenado = dr.IsDBNull(dr.GetOrdinal("Ped_ImporteOrdenado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_ImporteOrdenado")));
                    p.Ped_ImporteDisponible = dr.IsDBNull(dr.GetOrdinal("Ped_ImporteDisponible")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_ImporteDisponible")));
                    p.Ped_Asignado = dr.IsDBNull(dr.GetOrdinal("Ped_Asignado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Asignado")));
                    p.Ped_Picking = dr.IsDBNull(dr.GetOrdinal("Ped_picking")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_picking")));

                    p.Ped_Facturado = dr.IsDBNull(dr.GetOrdinal("Ped_Facturado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Facturado")));
                    p.Ped_Remisionado = dr.IsDBNull(dr.GetOrdinal("Ped_Remisionado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Remisionado")));
                    p.Ped_Pendiente = dr.IsDBNull(dr.GetOrdinal("Ped_Pendiente")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Pendiente")));

                    p.Ped_ImporteAsignado = dr.IsDBNull(dr.GetOrdinal("Ped_ImporteAsignado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_ImporteAsignado")));
                    p.Sector = dr["Sector"].ToString();
                    p.Ruta = dr["Ruta"].ToString();
                    p.Secuencia = dr.IsDBNull(dr.GetOrdinal("Secuencia")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Secuencia")));
                    p.Ped_PorcentajeCantidadDisponible = dr.IsDBNull(dr.GetOrdinal("Ped_PorcentajeCantidadDisponible")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_PorcentajeCantidadDisponible")));
                    p.Ped_PorcentajeImporteDisponible = dr.IsDBNull(dr.GetOrdinal("Ped_PorcentajeImporteDisponible")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_PorcentajeImporteDisponible")));
                    p.Ped_PorcentajeAsignado = dr.IsDBNull(dr.GetOrdinal("Ped_PorcentajeAsignado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_PorcentajeAsignado")));
                    p.Ped_PorcentajeImporteAsignado = dr.IsDBNull(dr.GetOrdinal("Ped_PorcentajeImporteAsignado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_PorcentajeImporteAsignado")));
                    p.Ped_FechaEntrega = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ped_FechaEntrega")));
                    p.TipoPedido = dr["TipoPedido"].ToString();
                    p.Ped_PermiteParcialidades = dr["Ped_PermiteParcialidades"].ToString();
                    List.Add(p);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Filtro_Nombre",
                                          "@Filtro_CteIni",
                                          "@Filtro_CteFin",

                                          "@Filtro_FecIni",
                                          "@Filtro_FecFin",

                                          "@Filtro_PedIni",
                                          "@Filtro_PedFin",

                                          "@Filtro_RutaIni",
                                          "@Filtro_RutaFin",

                                          "@Filtro_SectorIni",
                                          "@Filtro_SectorFin",

                                          "@Filtro_Credito",

                                          "@Filtro_FecEntregaIni",
                                          "@Filtro_FecEntregaFin",
                                          "@Filtro_Estatus",
                                          "@Rut_Descripcion",
                                          "@Id_Aux",
                                          "@Id_Cte"

                                      };
                object[] Valores = {
                                       pedido.Id_Emp,
                                       pedido.Id_Cd,
                                       pedido.Filtro_Nombre  ,
                                       pedido.Filtro_CteIni == ""? (object)null: pedido.Filtro_CteIni,
                                       pedido.Filtro_CteFin== ""? (object)null: pedido.Filtro_CteFin,
                                       pedido.Filtro_FecIni,
                                       pedido.Filtro_FecFin,
                                       pedido.Filtro_PedIni,
                                       pedido.Filtro_PedFin,
                                       pedido.Filtro_RutaInicial,
                                       pedido.Filtro_RutaFinal,
                                       pedido.Filtro_SectorInicial,
                                       pedido.Filtro_SectorFinal,
                                       pedido.Filtro_Credito,
                                       pedido.Filtro_FecEntregaIni,
                                       pedido.Filtro_FecEntregaFin,
                                       Convert.ToInt32(pedido.Filtro_Estatus),
                                       pedido.Ruta,
                                       pedido.Id_Aux,
                                       pedido.Id_Cte
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProAsignPrdxPed_Lista_FechaEntrega_Picking_Producto", ref dr, Parametros, Valores);

                PedidoDet p;
                while (dr.Read())
                {
                    p = new PedidoDet();
                    p.Id_Prd = dr.IsDBNull(dr.GetOrdinal("Id_Prd")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    p.Ruta = dr["Ruta"].ToString();
                    p.Prd_Descripcion = dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                    p.Prd_Presentacion = dr.GetValue(dr.GetOrdinal("Prd_Presentacion")).ToString();
                    p.Prd_UniNe = dr.GetValue(dr.GetOrdinal("Prd_UniNe")).ToString();
                    p.Ped_Cantidad = dr.IsDBNull(dr.GetOrdinal("Ped_Cantidad")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Cantidad")));
                    p.Ped_CantidadDisponible = dr.IsDBNull(dr.GetOrdinal("Ped_CantidadDisponible")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_CantidadDisponible")));
                    p.Ped_ImporteOrdenado = dr.IsDBNull(dr.GetOrdinal("Ped_ImporteOrdenado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_ImporteOrdenado")));
                    p.Ped_ImporteDisponible = dr.IsDBNull(dr.GetOrdinal("Ped_ImporteDisponible")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_ImporteDisponible")));
                    p.Ped_Asignado = dr.IsDBNull(dr.GetOrdinal("Ped_Asignado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Asignado")));
                    p.Ped_ImporteAsignado = dr.IsDBNull(dr.GetOrdinal("Ped_ImporteAsignado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_ImporteAsignado")));
                    p.Ped_PorcentajeCantidadDisponible = dr.IsDBNull(dr.GetOrdinal("Ped_PorcentajeCantidadDisponible")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_PorcentajeCantidadDisponible")));
                    p.Ped_PorcentajeImporteDisponible = dr.IsDBNull(dr.GetOrdinal("Ped_PorcentajeImporteDisponible")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_PorcentajeImporteDisponible")));
                    p.Ped_PorcentajeAsignado = dr.IsDBNull(dr.GetOrdinal("Ped_PorcentajeAsignado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_PorcentajeAsignado")));
                    p.Ped_PorcentajeImporteAsignado = dr.IsDBNull(dr.GetOrdinal("Ped_PorcentajeImporteAsignado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_PorcentajeImporteAsignado")));
                    p.TipoPedido = dr["TipoPedido"].ToString();
                    p.Credito = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_Credito")));
                    p.CreditoStr = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_Credito"))) ? "Si" : "No";
                    p.Ped_PermiteParcialidades = dr["Ped_PermiteParcialidades"].ToString();

                    p.Ped_Picking = dr.IsDBNull(dr.GetOrdinal("Ped_picking")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_picking")));

                    p.Ped_Facturado = dr.IsDBNull(dr.GetOrdinal("Ped_Facturado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Facturado")));
                    p.Ped_Remisionado = dr.IsDBNull(dr.GetOrdinal("Ped_Remisionado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Remisionado")));
                    p.Ped_Pendiente = dr.IsDBNull(dr.GetOrdinal("Ped_Pendiente")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Pendiente")));
                    p.Prd_Disponible = dr.IsDBNull(dr.GetOrdinal("Disponible")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Disponible")));
                    List.Add(p);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Filtro_Nombre",
                                          "@Filtro_CteIni",
                                          "@Filtro_CteFin",

                                          "@Filtro_FecIni",
                                          "@Filtro_FecFin",

                                          "@Filtro_PedIni",
                                          "@Filtro_PedFin",

                                          "@Filtro_RutaIni",
                                          "@Filtro_RutaFin",

                                          "@Filtro_SectorIni",
                                          "@Filtro_SectorFin",

                                          "@Filtro_Credito",

                                          "@Filtro_FecEntregaIni",
                                          "@Filtro_FecEntregaFin",
                                          "@Filtro_Estatus"

                                      };
                object[] Valores = {
                                       pedido.Id_Emp,
                                       pedido.Id_Cd,
                                       pedido.Filtro_Nombre  ,
                                       pedido.Filtro_CteIni == ""? (object)null: pedido.Filtro_CteIni,
                                       pedido.Filtro_CteFin== ""? (object)null: pedido.Filtro_CteFin,
                                       pedido.Filtro_FecIni,
                                       pedido.Filtro_FecFin,
                                       pedido.Filtro_PedIni,
                                       pedido.Filtro_PedFin,
                                       pedido.Filtro_RutaInicial,
                                       pedido.Filtro_RutaFinal,
                                       pedido.Filtro_SectorInicial,
                                       pedido.Filtro_SectorFinal,
                                       pedido.Filtro_Credito,
                                       pedido.Filtro_FecEntregaIni,
                                       pedido.Filtro_FecEntregaFin,
                                       Convert.ToInt32(pedido.Filtro_Estatus)
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProAsignPrdxPed_Lista_FechaEntregaPorProducto", ref dr, Parametros, Valores);

                PedidoDet p;
                while (dr.Read())
                {
                    p = new PedidoDet();
                    p.Id_Prd = dr.IsDBNull(dr.GetOrdinal("Id_Prd")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    p.Prd_Descripcion = dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                    p.Prd_Presentacion = dr.GetValue(dr.GetOrdinal("Prd_Presentacion")).ToString();
                    p.Prd_UniNe = dr.GetValue(dr.GetOrdinal("Prd_UniNe")).ToString();
                    p.Ped_Cantidad = dr.IsDBNull(dr.GetOrdinal("Ped_Cantidad")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Cantidad")));
                    p.Ped_CantidadDisponible = dr.IsDBNull(dr.GetOrdinal("Ped_CantidadDisponible")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_CantidadDisponible")));
                    p.Ped_ImporteOrdenado = dr.IsDBNull(dr.GetOrdinal("Ped_ImporteOrdenado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_ImporteOrdenado")));
                    p.Ped_ImporteDisponible = dr.IsDBNull(dr.GetOrdinal("Ped_ImporteDisponible")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_ImporteDisponible")));
                    p.Ped_Asignado = dr.IsDBNull(dr.GetOrdinal("Ped_Asignado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Asignado")));
                    p.Ped_ImporteAsignado = dr.IsDBNull(dr.GetOrdinal("Ped_ImporteAsignado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_ImporteAsignado")));
                    p.Ped_PorcentajeCantidadDisponible = dr.IsDBNull(dr.GetOrdinal("Ped_PorcentajeCantidadDisponible")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_PorcentajeCantidadDisponible")));
                    p.Ped_PorcentajeImporteDisponible = dr.IsDBNull(dr.GetOrdinal("Ped_PorcentajeImporteDisponible")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_PorcentajeImporteDisponible")));
                    p.Ped_PorcentajeAsignado = dr.IsDBNull(dr.GetOrdinal("Ped_PorcentajeAsignado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_PorcentajeAsignado")));
                    p.Ped_PorcentajeImporteAsignado = dr.IsDBNull(dr.GetOrdinal("Ped_PorcentajeImporteAsignado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_PorcentajeImporteAsignado")));
                    p.TipoPedido = dr["TipoPedido"].ToString();
                    List.Add(p);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Ped"
                                      };
                object[] Valores = {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Ped
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProAsignPrdxPed_Consulta_Pickingr", ref dr, Parametros, Valores);

                PedidoDet p;
                while (dr.Read())
                {
                    p = new PedidoDet();
                    p.Id_Ter = dr.IsDBNull(dr.GetOrdinal("Id_Ter")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    p.Id_Prd = dr.IsDBNull(dr.GetOrdinal("Id_Prd")) ? 0 : Convert.ToInt64(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    p.Prd_Desc = dr.IsDBNull(dr.GetOrdinal("Prd_Descripcion")) ? "" : dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                    p.Prd_Ord = dr.IsDBNull(dr.GetOrdinal("Ped_Cantidad")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Cantidad")));
                    p.Prd_OrdDisp = dr.IsDBNull(dr.GetOrdinal("Prd_OrdDisp")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_OrdDisp")));

                    p.Ped_CantF = dr.IsDBNull(dr.GetOrdinal("Ped_CantF")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_CantF")));
                    p.Ped_CantR = dr.IsDBNull(dr.GetOrdinal("Ped_CantR")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_CantR")));
                    p.Prd_Asig = dr.IsDBNull(dr.GetOrdinal("Ped_Asignar")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Asignar")));
                    p.Prd_Faltante = dr.IsDBNull(dr.GetOrdinal("Prd_Faltante")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_Faltante")));
                    p.Prd_Existencia = dr.IsDBNull(dr.GetOrdinal("Prd_InvFinal")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_InvFinal")));
                    p.Prd_Disponible = dr.IsDBNull(dr.GetOrdinal("Prd_Disponible")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_Disponible")));
                    p.Prd_NoConf = dr.IsDBNull(dr.GetOrdinal("Prd_NoConf")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_NoConf")));
                    p.Prd_NoEnc = dr.IsDBNull(dr.GetOrdinal("Prd_NoEnc")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_NoEnc")));
                    p.Id_PedDet = dr.IsDBNull(dr.GetOrdinal("Id_PedDet")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_PedDet")));
                    p.Prd_PorcentajeAsignado = dr.IsDBNull(dr.GetOrdinal("Prd_PorcentajeAsignado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_PorcentajeAsignado")));
                    List.Add(p);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Ped",
                                          "@Ped_FechaEntrega"
                                      };
                object[] Valores = {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Ped,
                                        pedido.Ped_FechaEntrega

                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SP_ActualizaFechaEntregaPedido", ref dr, Parametros, Valores);

                if (dr.Read())
                {
                    List = new Pedido();



                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Ped",
                                          "@Id_Cte",
                                          "@Id_Rut"
                                      };
                object[] Valores = {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Ped,
                                        pedido.Id_Cte,
                                        pedido.Id_Rut
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SP_ActualizaDirEntregaPedido", ref dr, Parametros, Valores);

                if (dr.Read())
                {
                    List = new Pedido();



                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Ped",
                                          "@Id_Rut"
                                      };
                object[] Valores = {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Ped,
                                        pedido.Id_Rut
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SP_CambiarRutaEntregaPedido", ref dr, Parametros, Valores);

                if (dr.Read())
                {
                    List = new Pedido();



                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Ped"
                                      };
                object[] Valores = {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Ped
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SP_ConsultaDirEntregaPedido", ref dr, Parametros, Valores);

                if (dr.Read())
                {
                    List = new Pedido();

                    List.Ped_ConsignadoCalle = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoCalle")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoCalle")).ToString();
                    List.Ped_ConsignadoNo = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoNo")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoNo")).ToString();
                    List.Ped_ConsignadoCp = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoCp")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoCp")).ToString();
                    List.Ped_ConsignadoMunicipio = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoMunicipio")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoMunicipio")).ToString();
                    List.Ped_ConsignadoEstado = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoEstado")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoEstado")).ToString();
                    List.Ped_ConsignadoColonia = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoColonia")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoColonia")).ToString();
                    List.acs_telefono2 = dr.IsDBNull(dr.GetOrdinal("Cte_Telefono")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_Telefono")).ToString();
                    List.Ruta = dr.IsDBNull(dr.GetOrdinal("Ruta")) ? "" : dr.GetValue(dr.GetOrdinal("Ruta")).ToString();
                    List.Id_Rut = dr.IsDBNull(dr.GetOrdinal("Id_Rut")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rut")));

                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaPedidoAsig_Picking_Ped(Pedido pedido, string Conexion, ref List<PedidoDet> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Ped"
                                      };
                object[] Valores = {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Ped
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProAsignPrdxPed_Consultar_Picking_Pedido", ref dr, Parametros, Valores);

                PedidoDet p;
                while (dr.Read())
                {
                    p = new PedidoDet();
                    p.Id_Ter = dr.IsDBNull(dr.GetOrdinal("Id_Ter")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    p.Id_Prd = dr.IsDBNull(dr.GetOrdinal("Id_Prd")) ? 0 : Convert.ToInt64(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    p.Prd_Desc = dr.IsDBNull(dr.GetOrdinal("Prd_Descripcion")) ? "" : dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                    p.Prd_Ord = dr.IsDBNull(dr.GetOrdinal("Ped_Cantidad")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Cantidad")));
                    p.Prd_OrdDisp = dr.IsDBNull(dr.GetOrdinal("Prd_OrdDisp")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_OrdDisp")));

                    p.Ped_CantF = dr.IsDBNull(dr.GetOrdinal("Ped_CantF")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_CantF")));
                    p.Ped_CantR = dr.IsDBNull(dr.GetOrdinal("Ped_CantR")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_CantR")));
                    p.Prd_Asig = dr.IsDBNull(dr.GetOrdinal("Ped_Asignar")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Asignar")));
                    p.Prd_Faltante = dr.IsDBNull(dr.GetOrdinal("Prd_Faltante")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_Faltante")));
                    p.Prd_Existencia = dr.IsDBNull(dr.GetOrdinal("Prd_InvFinal")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_InvFinal")));
                    p.Prd_Disponible = dr.IsDBNull(dr.GetOrdinal("Prd_Disponible")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_Disponible")));
                    p.Prd_NoConf = dr.IsDBNull(dr.GetOrdinal("Prd_NoConf")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_NoConf")));
                    p.Prd_NoEnc = dr.IsDBNull(dr.GetOrdinal("Prd_NoEnc")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_NoEnc")));
                    p.Id_PedDet = dr.IsDBNull(dr.GetOrdinal("Id_PedDet")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_PedDet")));
                    p.Prd_PorcentajeAsignado = dr.IsDBNull(dr.GetOrdinal("Prd_PorcentajeAsignado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_PorcentajeAsignado")));
                    p.Ped_Picking = dr.IsDBNull(dr.GetOrdinal("Ped_picking")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_picking")));
                    List.Add(p);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string StrTraePicking = "";
                if (pedido.TraePicking == null)
                {
                    StrTraePicking = "N";
                }
                else
                {
                    StrTraePicking = pedido.TraePicking;
                }

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Ped",
                                          "@TraePicking"
                                      };
                object[] Valores = {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Ped,
                                        StrTraePicking
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProAsignPrdxPed_Consultar", ref dr, Parametros, Valores);

                PedidoDet p;
                while (dr.Read())
                {
                    p = new PedidoDet();
                    p.Id_Ter = dr.IsDBNull(dr.GetOrdinal("Id_Ter")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    p.Id_Prd = dr.IsDBNull(dr.GetOrdinal("Id_Prd")) ? 0 : Convert.ToInt64(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    p.Prd_Desc = dr.IsDBNull(dr.GetOrdinal("Prd_Descripcion")) ? "" : dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                    p.Prd_Ord = dr.IsDBNull(dr.GetOrdinal("Ped_Cantidad")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Cantidad")));
                    p.Prd_OrdDisp = dr.IsDBNull(dr.GetOrdinal("Prd_OrdDisp")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_OrdDisp")));

                    p.Ped_CantF = dr.IsDBNull(dr.GetOrdinal("Ped_CantF")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_CantF")));
                    p.Ped_CantR = dr.IsDBNull(dr.GetOrdinal("Ped_CantR")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_CantR")));
                    p.Prd_Asig = dr.IsDBNull(dr.GetOrdinal("Ped_Asignar")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Asignar")));
                    p.Prd_Faltante = dr.IsDBNull(dr.GetOrdinal("Prd_Faltante")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_Faltante")));
                    p.Prd_Existencia = dr.IsDBNull(dr.GetOrdinal("Prd_InvFinal")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_InvFinal")));
                    p.Prd_Disponible = dr.IsDBNull(dr.GetOrdinal("Prd_Disponible")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_Disponible")));
                    p.Prd_NoConf = dr.IsDBNull(dr.GetOrdinal("Prd_NoConf")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_NoConf")));
                    p.Prd_NoEnc = dr.IsDBNull(dr.GetOrdinal("Prd_NoEnc")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_NoEnc")));
                    p.Id_PedDet = dr.IsDBNull(dr.GetOrdinal("Id_PedDet")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_PedDet")));
                    p.Prd_PorcentajeAsignado = dr.IsDBNull(dr.GetOrdinal("Prd_PorcentajeAsignado")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_PorcentajeAsignado")));
                    p.Ped_Picking = dr.IsDBNull(dr.GetOrdinal("Ped_Picking")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Picking")));
                    List.Add(p);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void AsignarPrdXPed_Picking(Pedido pedido, List<PedidoDet> list, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                SqlCommand sqlcmd = default(SqlCommand);
                CapaDatos.StartTrans();


                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Ped",
                                          "@Id_Prd",
                                          "@Id_Asig",
                                          "@FecAsig",
                                          "@UsrAsig",
                                          "@Id_PedDet"
                                          ,"@Prd_NoConf"
                                          ,"@Prd_NoEnc"
                                          ,"@Ped_picking"
                                          ,"@Id_Cte"
                                          ,"@Id_Ter"
                                      };
                object[] Valores = null;

                for (int x = 0; x < list.Count; x++)
                {


                    if (pedido.Id_Ped != 0)
                    {
                        Valores = new object[] {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Ped,
                                        list[x].Id_Prd,
                                        list[x].Prd_Asig,
                                        pedido.Ped_Fecha,
                                        pedido.Id_U,
                                        list[x].Id_PedDet
                                        ,list[x].Prd_NoConf
                                        ,list[x].Prd_NoEnc
                                        ,list[x].Ped_Picking
                                        ,pedido.Id_Cte
                                        ,list[x].Id_Ter
                                   };
                    }
                    else
                    {
                        Valores = new object[] {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        list[x].Id_Ped,
                                        list[x].Id_Prd,
                                        list[x].Prd_Asig,
                                        pedido.Ped_Fecha,
                                        pedido.Id_U,
                                        list[x].Id_PedDet
                                        ,list[x].Prd_NoConf
                                        ,list[x].Prd_NoEnc
                                        ,list[x].Ped_Picking
                                        ,pedido.Id_Cte
                                        ,list[x].Id_Ter
                        };
                    }

                    sqlcmd = CapaDatos.GenerarSqlCommand("spProAsignPrdxPed_Picking", ref verificador, Parametros, Valores);
                    if (verificador == 2 || verificador == 3)
                    {
                        CapaDatos.RollBackTrans();
                        CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                        return;
                    }
                }
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }
        public void AsignarPrdXPed(Pedido pedido, List<PedidoDet> list, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                SqlCommand sqlcmd = default(SqlCommand);
                CapaDatos.StartTrans();


                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Ped",
                                          "@Id_Prd",
                                          "@Id_Asig",
                                          "@FecAsig",
                                          "@UsrAsig",
                                          "@Id_PedDet"
                                          ,"@Prd_NoConf"
                                          ,"@Prd_NoEnc"
                                      };
                object[] Valores = null;

                for (int x = 0; x < list.Count; x++)
                {
                    Valores = new object[] {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Ped,
                                        list[x].Id_Prd,
                                        list[x].Prd_Asig,
                                        pedido.Ped_Fecha,
                                        pedido.Id_U,
                                        list[x].Id_PedDet
                                        ,list[x].Prd_NoConf
                                        ,list[x].Prd_NoEnc
                                   };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spProAsignPrdxPed", ref verificador, Parametros, Valores);
                    if (verificador == 2 || verificador == 3)
                    {
                        CapaDatos.RollBackTrans();
                        CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                        return;
                    }
                }
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }
        public void AsignarRuta(int id_Ped, string sector, string ruta, int secuencia, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                SqlCommand sqlcmd = default(SqlCommand);
                CapaDatos.StartTrans();


                string[] Parametros = {
                                          "@Id_Ped",
                                          "@Sector",
                                          "@Ruta",
                                          "@Secuencia"
                                      };
                object[] Valores = new object[] {
                                        id_Ped,
                                        sector,
                                        ruta,
                                        secuencia
                                   };

                sqlcmd = CapaDatos.GenerarSqlCommand("spProAsignRuta", ref verificador, Parametros, Valores);
                if (verificador == 2 || verificador == 3)
                {
                    CapaDatos.RollBackTrans();
                    CapaDatos.LimpiarSqlcommand(ref sqlcmd);
                    return;

                }
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }
        public void ConsultaPedidoAutorizacion_Lista(Pedido pedido, string Conexion, ref List<Pedido> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Filtro_Nombre",
                                          "@Filtro_CteIni",
                                          "@Filtro_CteFin",
                                          "@Filtro_FecIni",
                                          "@Filtro_FecFin",
                                          "@Filtro_usuario",
                                      };
                object[] Valores = {
                                       pedido.Id_Emp,
                                       pedido.Id_Cd,
                                       pedido.Filtro_Nombre == ""? (object)null: pedido.Filtro_Nombre,
                                       pedido.Filtro_CteIni == ""? (object)null: pedido.Filtro_CteIni,
                                       pedido.Filtro_CteFin == ""? (object)null: pedido.Filtro_CteFin,
                                       pedido.Filtro_FecIni,
                                       pedido.Filtro_FecFin,
                                       pedido.Filtro_usuario == ""? (object)null: pedido.Filtro_usuario
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoAutorizacion_Lista", ref dr, Parametros, Valores);

                Pedido p;
                while (dr.Read())
                {
                    p = new Pedido();
                    p.Ped_Tipo = dr.IsDBNull(dr.GetOrdinal("Ped_Tipo")) ? -1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Tipo")));
                    p.Ped_TipoStr = dr.IsDBNull(dr.GetOrdinal("Ped_Tipo")) ? "" : Tipo(dr.GetValue(dr.GetOrdinal("Ped_Tipo")).ToString());
                    p.Id_U = dr.IsDBNull(dr.GetOrdinal("Id_U")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));
                    p.Estatus = dr.IsDBNull(dr.GetOrdinal("Ped_Estatus")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Estatus")).ToString();
                    p.EstatusStr = dr.IsDBNull(dr.GetOrdinal("Ped_Estatus")) ? "" : Estatus(dr.GetValue(dr.GetOrdinal("Ped_Estatus")).ToString());
                    p.Id_Ped = dr.IsDBNull(dr.GetOrdinal("Id_Ped")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ped")));
                    p.Ped_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ped_Fecha")));
                    p.Id_Cte = dr.IsDBNull(dr.GetOrdinal("Id_Cte")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    p.Cte_NomComercial = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    p.Ped_Subtotal = dr.IsDBNull(dr.GetOrdinal("Ped_Subtotal")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Subtotal")));
                    p.Ped_Iva = dr.IsDBNull(dr.GetOrdinal("Ped_Iva")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Iva")));
                    p.Ped_Total = dr.IsDBNull(dr.GetOrdinal("Ped_Total")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Total")));
                    p.Facturacion = dr.IsDBNull(dr.GetOrdinal("Cte_Facturacion")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_Facturacion")));
                    List.Add(p);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Autorizar(Pedido pedido, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Ped",
                                        "@Fecha",
                                        "@Id_U"
                                      };
                object[] Valores = {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Ped,
                                        pedido.Ped_FechaAut,
                                        pedido.Id_U
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedido_Autorizar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultarEncabezado_RepFacPedidosPendientes(Sesion sesion, ref string Emp_Nombre, ref string Cd_Nombre, ref string U_Nombre)
        {//rm
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);


                string[] parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_U",
                                      };
                string[] Valores = {
                                       sesion.Id_Emp.ToString(),
                                       sesion.Id_Cd_Ver.ToString(),
                                       sesion.Id_U.ToString(),
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRepFacPedidosPendientes_Encabezado", ref dr, parametros, Valores);
                while (dr.Read())
                {
                    Emp_Nombre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Emp_Nombre"))) ? "" : dr.GetString(dr.GetOrdinal("Emp_Nombre"));
                    Cd_Nombre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cd_Nombre"))) ? "" : dr.GetString(dr.GetOrdinal("Cd_Nombre"));
                    U_Nombre = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("U_Nombre"))) ? "" : dr.GetString(dr.GetOrdinal("U_Nombre"));
                    break;
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Ped",
                                          "@Id_Prd"
                                      };
                object[] Valores = {
                                      pedido.Id_Emp,
                                      pedido.Id_Cd,
                                      pedido.Id_Ped,
                                      prd
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDisponible_Consultar", ref disponible_pedido, parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);


                string[] parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Ped",
                                      };
                object[] Valores = {
                                       pedido.Id_Emp,
                                       pedido.Id_Cd,
                                       pedido.Id_Ped,
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedido_BajaParcialConsulta", ref dr, parametros, Valores);

                while (dr.Read())
                {
                    pedido = new PedidoDet();
                    pedido.Id_Prd = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    pedido.Prd_Desc = Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_Descripcion")));
                    pedido.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    pedido.Ter_Descripcion = Convert.ToString(dr.GetValue(dr.GetOrdinal("Ter_Nombre")));
                    pedido.Original = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Original")));
                    pedido.Cancelado = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cancelado")));
                    pedido.Pendiente = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Pendiente")));
                    pedido.Final = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Final")));
                    pedido.Prd_Asig = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Asignado")));
                    list.Add(pedido);
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConfirmarPedido(PedidoDet pedido, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                SqlCommand sqlcmd = default(SqlCommand);
                CapaDatos.StartTrans();



                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Prd",
                                          "@Ruta",
                                          "@CreditoStr",
                                          "@Ped_PermiteParcialidades",
                                          "@TipoPedido"
                                      };
                object[] Valores = null;

                Valores = new object[] {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Prd,
                                        pedido.Ruta,
                                        pedido.CreditoStr,
                                        pedido.Ped_PermiteParcialidades,
                                        pedido.TipoPedido
                                   };

                sqlcmd = CapaDatos.GenerarSqlCommand("spProAsignPrdxPed_Lista_Confirma_Picking_Producto", ref verificador, Parametros, Valores);




                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void ConsultaAsignPedxPrd_Picking_Producto(PedidoDet prdDet, string Conexion, ref List<ProductoDet> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Prd",
                                          "@Ruta",
                                          "@CreditoStr",
                                          "@Ped_PermiteParcialidades",
                                          "@TipoPedido",
                                          "@Id_Cte"
                                      };
                object[] Valores = {
                                        prdDet.Id_Emp,
                                        prdDet.Id_Cd,
                                        prdDet.Id_Prd,
                                        prdDet.Ruta,
                                        prdDet.CreditoStr,
                                        prdDet.Ped_PermiteParcialidades,
                                        prdDet.TipoPedido,
                                        prdDet.Id_Cliente
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProAsignPedxPrd_Consultar_Picking", ref dr, Parametros, Valores);

                ProductoDet p;
                while (dr.Read())
                {
                    p = new ProductoDet();
                    p.Id_Ped = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ped")));
                    p.Ped_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ped_Fecha")));
                    p.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    p.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    p.Cte_NomComercial = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    p.Cte_Credito = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_Credito")));
                    p.Cte_CreditoStr = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Cte_Credito"))) ? "Si" : "No";
                    p.Ped_Ord = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Ord")));
                    p.Ped_Disp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Disp")));
                    p.Ped_Asignar = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Asignar")));
                    p.Ped_Faltante = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Faltante")));
                    p.Prd_InvFinal = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_InvFinal")));
                    p.Prd_Disp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_Disp")));
                    p.Id_PedDet = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_PedDet")));
                    p.Ped_Picking = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Picking")));
                    List.Add(p);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CancelarPedido(PedidoDet pedido, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                SqlCommand sqlcmd = default(SqlCommand);
                CapaDatos.StartTrans();



                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Prd",
                                          "@Ruta",
                                          "@CreditoStr",
                                          "@Ped_PermiteParcialidades",
                                          "@TipoPedido"
                                      };
                object[] Valores = null;

                Valores = new object[] {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Prd,
                                        pedido.Ruta,
                                        pedido.CreditoStr,
                                        pedido.Ped_PermiteParcialidades,
                                        pedido.TipoPedido
                                   };

                sqlcmd = CapaDatos.GenerarSqlCommand("spProAsignPrdxPed_Lista_Cancelar_Picking_Producto", ref verificador, Parametros, Valores);




                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void BajaParcial(Pedido pedido, List<PedidoDet> list, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                SqlCommand sqlcmd = default(SqlCommand);
                CapaDatos.StartTrans();


                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Id_Ped",
                                          "@Id_Prd",
                                          "@Id_Ter",
                                          "@Cant_Cancelar"
                                      };
                object[] Valores = null;

                for (int x = 0; x < list.Count; x++)
                {
                    Valores = new object[] {
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_Ped,
                                        list[x].Id_Prd,
                                        list[x].Id_Ter,
                                        list[x].Cancelado
                                   };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedido_BajaParcial", ref verificador, Parametros, Valores);

                }
                int verificador2 = 0;
                Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Ped" };
                Valores = new object[] { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped };
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedido_BajaParcialTotales", ref verificador2, Parametros, Valores);

                verificador2 = 0;
                if (verificador == 1)
                {
                    Parametros = new string[] { "@Id_Emp", "@Id_Cd", "@Id_Ped" };
                    Valores = new object[] { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spProPedido_AsignacionAutomaticaTerr", ref verificador2, Parametros, Valores);
                }



                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void PedidosPendientes_ConsultaReporte(Pedido pedido, ref DataTable dt, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                DataSet ds = null;
                string[] Parametros = {
                            "@Id_Emp",
                            "@Id_Cd",
                            "@Territorios",
                            "@Clientes",
                            "@Productos",
                            "@Tipo",
                            "@FechaIni",
                            "@FechaFin",
                            "@Pedido"
                        };

                object[] Valores = {
                                       pedido.Id_Emp,
                                       pedido.Id_Cd,
                                       pedido.Territorios,
                                       pedido.Clientes,
                                       pedido.Productos,
                                       1,
                                       pedido.FechaIni == null ? (object) null: pedido.FechaIni,
                                       pedido.FechaFin  == null ? (object) null: pedido.FechaFin,
                                       pedido.Pedidos
                                   };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spRepFacPedidosPendientes_Detalle", ref ds, Parametros, Valores);
                dt = ds.Tables[0];

                cd_datos.LimpiarSqlcommand(ref sqlcmd);


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }





        public void spCapFacturaDetalle_Consultar(Factura pedido, ref List<PedidoDet> det, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Fac" };
                object[] Valores = { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Fac };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaDetalle_Consultar", ref dr, Parametros, Valores);
                PedidoDet detalle;
                while (dr.Read())
                {
                    detalle = new PedidoDet();
                    detalle.Id_Prd = dr.GetInt64(dr.GetOrdinal("Id_Prd"));
                    detalle.Ped_Cantidad = dr.GetInt32(dr.GetOrdinal("Fac_Cant"));
                    det.Add(detalle);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaCaptacionPedidoDet(Pedido pedido, ref List<PedidoDet> det, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "Id_Ped", "Ped_Captacion", "@Filtro_Doc", "@ConMinimo" };
                object[] Valores = { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped, pedido.Ped_Captacion, pedido.Filtro_Doc, -1 };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDet_Consultar", ref dr, Parametros, Valores);
                PedidoDet detalle;
                while (dr.Read())
                {
                    detalle = new PedidoDet();
                    detalle.Id_Prd = dr.GetInt64(dr.GetOrdinal("Id_Prd"));
                    detalle.Ped_Cantidad = dr.GetInt32(dr.GetOrdinal("Prd_Cantidad"));
                    det.Add(detalle);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaCaptacionPedidoDet(Pedido pedido, string Conexion, ref DataTable dt, ref DataTable dtRestos)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "Id_Ped", "Ped_Captacion", "@Filtro_Doc", "@ConMinimo" };
                object[] Valores = { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped, pedido.Ped_Captacion, pedido.Filtro_Doc, -1 };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDet_Consultar", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    if (pedido.Ped_Captacion == true)
                    {
                        if (dr.GetValue(dr.GetOrdinal("TipoVta")).ToString() == "NE")
                        {
                            dtRestos.Rows.Add(new object[] {
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Prd_Descripcion")),
                            dr.GetValue(dr.GetOrdinal("Prd_Presentacion")),
                            dr.GetValue(dr.GetOrdinal("Id_Uni")),
                            dr.GetValue(dr.GetOrdinal("mes1")),
                            dr.GetValue(dr.GetOrdinal("mes2")),
                            dr.GetValue(dr.GetOrdinal("mes3")),
                            dr.GetValue(dr.GetOrdinal("Prd_Cantidad")),
                            dr.GetValue(dr.GetOrdinal("Prd_Precio")),
                            dr.GetValue(dr.GetOrdinal("Acs_PrecioAcys")),
                            dr.GetValue(dr.GetOrdinal("Prd_Importe")),
                            Str(dr.GetValue(dr.GetOrdinal("Acs_Documento"))),
                            dr.GetValue(dr.GetOrdinal("Acs_Fecha")),
                            dr.GetValue(dr.GetOrdinal("Acs_Mod")) ,
                            dr.GetValue(dr.GetOrdinal("Acs_Dia")) ,
                            Nombre(dr.GetValue(dr.GetOrdinal("Acs_Dia"))),
                            dr.GetValue(dr.GetOrdinal("Acs_Frecuencia")),
                            dr.GetValue(dr.GetOrdinal("Prd_RemFact")),
                            dr.GetValue(dr.GetOrdinal("Ped_Asignar")),
                            dr.GetValue(dr.GetOrdinal("TipoVta"))
                       });
                        }
                        else
                        {
                            dt.Rows.Add(new object[] {
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Prd_Descripcion")),
                            dr.GetValue(dr.GetOrdinal("Prd_Presentacion")),
                            dr.GetValue(dr.GetOrdinal("Id_Uni")),
                            dr.GetValue(dr.GetOrdinal("mes1")),
                            dr.GetValue(dr.GetOrdinal("mes2")),
                            dr.GetValue(dr.GetOrdinal("mes3")),
                            dr.GetValue(dr.GetOrdinal("Prd_Cantidad")),
                            dr.GetValue(dr.GetOrdinal("Prd_Precio")),
                            dr.GetValue(dr.GetOrdinal("Acs_PrecioAcys")),
                            dr.GetValue(dr.GetOrdinal("Prd_Importe")),
                            Str(dr.GetValue(dr.GetOrdinal("Acs_Documento"))),
                            dr.GetValue(dr.GetOrdinal("Acs_Fecha")),
                            dr.GetValue(dr.GetOrdinal("Acs_Mod")) ,
                            dr.GetValue(dr.GetOrdinal("Acs_Dia")) ,
                            Nombre(dr.GetValue(dr.GetOrdinal("Acs_Dia"))),
                            dr.GetValue(dr.GetOrdinal("Acs_Frecuencia")),
                            dr.GetValue(dr.GetOrdinal("Prd_RemFact")),
                            dr.GetValue(dr.GetOrdinal("Ped_Asignar")),
                            dr.GetValue(dr.GetOrdinal("TipoVta"))
                              });
                        }
                    }
                    else
                    {
                        if (dr.GetValue(dr.GetOrdinal("TipoVta")).ToString() == "NE")
                        {
                            dtRestos.Rows.Add(new object[] {
                            dr.GetValue(dr.GetOrdinal("Id_PedDet")),
                            dr.GetValue(dr.GetOrdinal("Id_Ter")),
                            dr.GetValue(dr.GetOrdinal("Ter_Nombre")),
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Prd_Descripcion")),
                            dr.GetValue(dr.GetOrdinal("Prd_Presentacion")),
                            dr.GetValue(dr.GetOrdinal("Prd_Unidad")),
                            dr.GetValue(dr.GetOrdinal("Prd_Precio")),
                            dr.GetValue(dr.GetOrdinal("Prd_Cantidad")),
                            dr.GetValue(dr.GetOrdinal("Prd_Importe")),
                            dr.GetValue(dr.GetOrdinal("TipoVta"))
                    });
                        }
                        else
                        {
                            dt.Rows.Add(new object[] {
                            dr.GetValue(dr.GetOrdinal("Id_PedDet")),
                            dr.GetValue(dr.GetOrdinal("Id_Ter")),
                            dr.GetValue(dr.GetOrdinal("Ter_Nombre")),
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Prd_Descripcion")),
                            dr.GetValue(dr.GetOrdinal("Prd_Presentacion")),
                            dr.GetValue(dr.GetOrdinal("Prd_Unidad")),
                            dr.GetValue(dr.GetOrdinal("Prd_Precio")),
                            dr.GetValue(dr.GetOrdinal("Prd_Cantidad")),
                            dr.GetValue(dr.GetOrdinal("Prd_Importe")),
                            dr.GetValue(dr.GetOrdinal("TipoVta"))
                             });
                        }
                    }
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

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
                PedidoDet nuevopedido;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Acs" };
                object[] Valores = { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Acs };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDet_ConsultarProductoParaVI", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    nuevopedido = new PedidoDet();
                    nuevopedido.Id_Prd = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("id_prd")));
                    nuevopedido.TotalProd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("TotalProd")));
                    list.Add(nuevopedido);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaProductoRepetidosDet(Pedido pedido, string Conexion, ref DataTable dt)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_prd", "@Id_Acs" };
                object[] Valores = { pedido.Id_Emp, pedido.Id_Cd, pedido.listaproductos, pedido.Id_Acs, };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDet_ConsultarProducto", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    dt.Rows.Add(new object[] {

                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Prd_Descripcion")),
                            dr.GetValue(dr.GetOrdinal("Prd_Presentacion")),
                            dr.GetValue(dr.GetOrdinal("Id_Uni")),

                            dr.GetValue(dr.GetOrdinal("Prd_Cantidad")),
                            dr.GetValue(dr.GetOrdinal("Prd_Precio")),

                              });
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

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
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "Id_Ped" };
                object[] Valores = { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped };
                //SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedido_Consultar", ref dr, Parametros, Valores);
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedido_ConsultarV3", ref dr, Parametros, Valores);
                if (dr.HasRows)
                {
                    dr.Read();
                    pedido.Ped_Comentarios = dr.IsDBNull(dr.GetOrdinal("Ped_Comentarios")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Comentarios")).ToString();
                    pedido.Ped_DescPorcen1 = dr.IsDBNull(dr.GetOrdinal("Ped_Desc1")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_DescPorcen1")));
                    pedido.Ped_DescPorcen2 = dr.IsDBNull(dr.GetOrdinal("Ped_Desc2")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_DescPorcen2")));
                    string entrega = dr.GetValue(dr.GetOrdinal("Ped_CondEntrega")).ToString();
                    if (!string.IsNullOrEmpty(entrega))
                        pedido.Ped_CondEntrega = Convert.ToInt32(entrega);
                    else
                        pedido.Ped_CondEntrega = 0;
                    pedido.Ped_Desc1 = dr.IsDBNull(dr.GetOrdinal("Ped_Desc1")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Desc1")).ToString();
                    pedido.Ped_Desc2 = dr.IsDBNull(dr.GetOrdinal("Ped_Desc2")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Desc2")).ToString();
                    pedido.Ped_Flete = dr.IsDBNull(dr.GetOrdinal("Ped_Flete")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Flete")).ToString();
                    pedido.Ped_Importe = dr.IsDBNull(dr.GetOrdinal("Ped_Importe")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Importe")));
                    pedido.Ped_Iva = dr.IsDBNull(dr.GetOrdinal("Ped_Iva")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Iva")));
                    pedido.Id_Cte = dr.IsDBNull(dr.GetOrdinal("Id_Cte")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    pedido.Cte_NomComercial = dr.IsDBNull(dr.GetOrdinal("Cte_NomComercial")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    pedido.Ped_Observaciones = dr.IsDBNull(dr.GetOrdinal("Ped_Observaciones")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Observaciones")).ToString();
                    pedido.Ped_OrdenEntrega = dr.IsDBNull(dr.GetOrdinal("Ped_OrdenEntrega")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_OrdenEntrega")).ToString();
                    pedido.Pedido_del = dr.IsDBNull(dr.GetOrdinal("Ped_PedidoDel")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_PedidoDel")).ToString();
                    pedido.Id_Rik = dr.IsDBNull(dr.GetOrdinal("Id_Rik")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    pedido.Rik_Nombre = dr.IsDBNull(dr.GetOrdinal("Rik_Nombre")) ? "" : dr.GetValue(dr.GetOrdinal("Rik_Nombre")).ToString();
                    pedido.Requisicion = dr.IsDBNull(dr.GetOrdinal("Ped_Requisicion")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Requisicion")).ToString();
                    pedido.Ped_Solicito = dr.IsDBNull(dr.GetOrdinal("Ped_Solicito")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Solicito")).ToString();
                    pedido.Ped_Subtotal = dr.IsDBNull(dr.GetOrdinal("Ped_Subtotal")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Subtotal")));
                    pedido.Id_Ter = dr.IsDBNull(dr.GetOrdinal("Id_Ter")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    pedido.Ter_Nombre = dr.IsDBNull(dr.GetOrdinal("Ter_Nombre")) ? "" : dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();
                    pedido.Ped_Total = dr.IsDBNull(dr.GetOrdinal("Ped_Total")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Total")));
                    pedido.Ped_Fecha = dr.IsDBNull(dr.GetOrdinal("Ped_Fecha")) ? default(DateTime) : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ped_Fecha")));
                    pedido.Ped_FechaEntrega = dr.IsDBNull(dr.GetOrdinal("Ped_FechaEntrega")) ? default(DateTime) : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ped_FechaEntrega")));
                    pedido.Ped_Tipo = dr.IsDBNull(dr.GetOrdinal("Ped_Tipo")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Tipo")));
                    pedido.Ped_SolicitoTel = dr.IsDBNull(dr.GetOrdinal("Ped_SolicitoTel")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_SolicitoTel")).ToString();
                    pedido.Ped_SolicitoEmail = dr.IsDBNull(dr.GetOrdinal("Ped_SolicitoEmail")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_SolicitoEmail")).ToString();
                    pedido.Ped_SolicitoPuesto = dr.IsDBNull(dr.GetOrdinal("Ped_SolicitoPuesto")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_SolicitoPuesto")).ToString();
                    pedido.Ped_ConsignadoCalle = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoCalle")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoCalle")).ToString();
                    pedido.Ped_ConsignadoNo = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoNo")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoNo")).ToString();
                    pedido.Ped_ConsignadoCp = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoCp")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoCp")).ToString();
                    pedido.Ped_ConsignadoMunicipio = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoMunicipio")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoMunicipio")).ToString();
                    pedido.Ped_ConsignadoEstado = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoEstado")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoEstado")).ToString();
                    pedido.Ped_ConsignadoColonia = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoColonia")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoColonia")).ToString();
                    pedido.Ped_ReqOrden = dr.IsDBNull(dr.GetOrdinal("Ped_ReqOrden")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Ped_ReqOrden")));
                    pedido.Ped_OrdenCompra = dr.IsDBNull(dr.GetOrdinal("Ped_OrdenCompra")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_OrdenCompra")).ToString();
                    pedido.Ped_AcysSemana = dr.IsDBNull(dr.GetOrdinal("Ped_AcysSemana")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_AcysSemana")));
                    pedido.Ped_AcysAnio = dr.IsDBNull(dr.GetOrdinal("Ped_AcysAnio")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_AcysAnio")));
                    pedido.Id_Acs = dr.IsDBNull(dr.GetOrdinal("Id_Acs")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));
                    pedido.Estatus = dr.GetValue(dr.GetOrdinal("Ped_Estatus")).ToString();
                    pedido.ReqAcys = dr.IsDBNull(dr.GetOrdinal("Ped_ReqAcys")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ReqAcys")).ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("Id_TG")))
                    {
                        pedido.Id_TG = (int)dr.GetValue(dr.GetOrdinal("Id_TG"));
                    }

                    if (dr.IsDBNull(dr.GetOrdinal("Id_Fac")))
                        pedido.Id_Fac = null;
                    else
                        pedido.Id_Fac = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Fac")));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaCaptacionPedidoDet2(Pedido pedido, string Conexion, ref DataTable dt, ref DataTable dtRestos)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "Id_Ped", "Ped_Captacion", "@Filtro_Doc", "@ConMinimo" };
                object[] Valores = { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped, pedido.Ped_Captacion, pedido.Filtro_Doc, -1 };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDet_Consultar2", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    if (pedido.Ped_Captacion == true)
                    {
                        if (dr.GetValue(dr.GetOrdinal("TipoVta")).ToString() == "NE")
                        {
                            dtRestos.Rows.Add(new object[] {
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Prd_Descripcion")),
                            dr.GetValue(dr.GetOrdinal("Prd_Presentacion")),
                            dr.GetValue(dr.GetOrdinal("Id_Uni")),

                            dr.GetValue(dr.GetOrdinal("mes1")),
                            dr.GetValue(dr.GetOrdinal("mes2")),
                            dr.GetValue(dr.GetOrdinal("mes3")),

                            dr.GetValue(dr.GetOrdinal("Prd_Cantidad")),
                            dr.GetValue(dr.GetOrdinal("Prd_Precio")),
                            dr.IsDBNull(dr.GetOrdinal("Acs_PrecioAcys"))? 0 : dr.GetValue(dr.GetOrdinal("Acs_PrecioAcys")),
                            dr.GetValue(dr.GetOrdinal("Prd_Importe")),
                            Str(dr.GetValue(dr.GetOrdinal("Acs_Documento"))),

                            dr.IsDBNull(dr.GetOrdinal("Acs_Fecha"))? DateTime.Now : dr.GetValue(dr.GetOrdinal("Acs_Fecha")),
                            dr.GetValue(dr.GetOrdinal("Acs_Mod")) ,
                            dr.GetValue(dr.GetOrdinal("Acs_Dia")) ,
                            Nombre(dr.GetValue(dr.GetOrdinal("Acs_Dia"))),
                            dr.GetValue(dr.GetOrdinal("Acs_Frecuencia")),

                             0,
                             dr.GetValue(dr.GetOrdinal("Ped_Asignar")),
                             0,
                             0,
                             dr.IsDBNull(dr.GetOrdinal("ACS_ReqOC")) ? 0 : dr.GetValue(dr.GetOrdinal("ACS_ReqOC")),
                             dr.GetValue(dr.GetOrdinal("Acs_Precio")),
                             dr.GetValue(dr.GetOrdinal("Prd_Cantidad"))
                       });
                        }
                        else
                        {
                            dt.Rows.Add(new object[] {
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Prd_Descripcion")),
                            dr.GetValue(dr.GetOrdinal("Prd_Presentacion")),
                            dr.GetValue(dr.GetOrdinal("Id_Uni")),

                            dr.GetValue(dr.GetOrdinal("mes1")),
                            dr.GetValue(dr.GetOrdinal("mes2")),
                            dr.GetValue(dr.GetOrdinal("mes3")),

                            dr.GetValue(dr.GetOrdinal("Prd_Cantidad")),
                            dr.GetValue(dr.GetOrdinal("Prd_Precio")),
                            dr.IsDBNull(dr.GetOrdinal("Acs_PrecioAcys"))? 0 : dr.GetValue(dr.GetOrdinal("Acs_PrecioAcys")),
                            dr.GetValue(dr.GetOrdinal("Prd_Importe")),
                            Str(dr.GetValue(dr.GetOrdinal("Acs_Documento"))),

                            dr.IsDBNull(dr.GetOrdinal("Acs_Fecha"))? DateTime.Now : dr.GetValue(dr.GetOrdinal("Acs_Fecha")),
                            dr.GetValue(dr.GetOrdinal("Acs_Mod")) ,
                            dr.GetValue(dr.GetOrdinal("Acs_Dia")) ,
                            Nombre(dr.GetValue(dr.GetOrdinal("Acs_Dia"))),
                              dr.GetValue(dr.GetOrdinal("Acs_Frecuencia")),

                           0,
                           dr.GetValue(dr.GetOrdinal("Ped_Asignar")),
                           0,
                           0,
                           dr.IsDBNull(dr.GetOrdinal("ACS_ReqOC")) ? 0 : dr.GetValue(dr.GetOrdinal("ACS_ReqOC")),
                           dr.GetValue(dr.GetOrdinal("Acs_Precio")),
                           dr.GetValue(dr.GetOrdinal("Prd_Cantidad"))
                              });
                        }
                    }
                    else
                    {
                        if (dr.GetValue(dr.GetOrdinal("TipoVta")).ToString() == "NE")
                        {
                            dtRestos.Rows.Add(new object[] {
                            dr.GetValue(dr.GetOrdinal("Id_PedDet")),
                            dr.GetValue(dr.GetOrdinal("Id_Ter")),
                            dr.GetValue(dr.GetOrdinal("Ter_Nombre")),
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Prd_Descripcion")),
                            dr.GetValue(dr.GetOrdinal("Prd_Presentacion")),
                            dr.GetValue(dr.GetOrdinal("Prd_Unidad")),
                            dr.GetValue(dr.GetOrdinal("Prd_Precio")),
                            dr.GetValue(dr.GetOrdinal("Prd_Cantidad")),
                            dr.GetValue(dr.GetOrdinal("Prd_Importe")),
                            dr.GetValue(dr.GetOrdinal("TipoVta")),
                            0,
                            dr.IsDBNull(dr.GetOrdinal("ACS_ReqOC")) ? 0 : dr.GetValue(dr.GetOrdinal("ACS_ReqOC")),
                            dr.GetValue(dr.GetOrdinal("Acs_Precio")),
                            dr.GetValue(dr.GetOrdinal("Prd_Cantidad"))
                    });
                        }
                        else
                        {
                            dt.Rows.Add(new object[] {
                            dr.GetValue(dr.GetOrdinal("Id_PedDet")),
                            dr.GetValue(dr.GetOrdinal("Id_Ter")),
                            dr.GetValue(dr.GetOrdinal("Ter_Nombre")),
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Prd_Descripcion")),
                            dr.GetValue(dr.GetOrdinal("Prd_Presentacion")),
                            dr.GetValue(dr.GetOrdinal("Prd_Unidad")),
                            dr.GetValue(dr.GetOrdinal("Prd_Precio")),
                            dr.GetValue(dr.GetOrdinal("Prd_Cantidad")),
                            dr.GetValue(dr.GetOrdinal("Prd_Importe")),
                            dr.GetValue(dr.GetOrdinal("TipoVta")),
                            0,
                            dr.IsDBNull(dr.GetOrdinal("ACS_ReqOC")) ? 0 : dr.GetValue(dr.GetOrdinal("ACS_ReqOC")),
                            dr.GetValue(dr.GetOrdinal("Acs_Precio")),
                            dr.GetValue(dr.GetOrdinal("Prd_Cantidad"))

                             });
                        }
                    }
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void ConsultaCaptacionPedidoDetadmin(Pedido pedido, string Conexion, ref DataTable dt)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "Id_Ped", "Ped_Captacion", "@Filtro_Doc", "@ConMinimo" };
                object[] Valores = { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped, pedido.Ped_Captacion, pedido.Filtro_Doc, -1 };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDet_Consultar2", ref dr, Parametros, Valores);

                while (dr.Read())
                {

                    dt.Rows.Add(new object[] {
                            dr.IsDBNull(dr.GetOrdinal("Acs_Fecha"))? DateTime.Now : dr.GetValue(dr.GetOrdinal("Acs_Fecha")),
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Prd_Descripcion")),
                            dr.GetValue(dr.GetOrdinal("Prd_Presentacion")),
                            dr.GetValue(dr.GetOrdinal("Id_Uni")),
                            dr.GetValue(dr.GetOrdinal("Prd_Cantidad")),
                            dr.GetValue(dr.GetOrdinal("Prd_Precio")),
                            dr.GetValue(dr.GetOrdinal("Prd_Importe"))
                            });

                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

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
                PedidoDet nuevopedido;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Acs" };
                object[] Valores = { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Acs };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDet_ConsultarProductoParaVI2", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    nuevopedido = new PedidoDet();
                    nuevopedido.Id_Prd = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("id_prd")));
                    nuevopedido.TotalProd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("TotalProd")));
                    list.Add(nuevopedido);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

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
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_prd", "@Id_Acs" };
                object[] Valores = { pedido.Id_Emp, pedido.Id_Cd, pedido.listaproductos, pedido.Id_Acs, };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDet_ConsultarProducto2", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    dt.Rows.Add(new object[] {

                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Prd_Descripcion")),
                            dr.GetValue(dr.GetOrdinal("Prd_Presentacion")),
                            dr.GetValue(dr.GetOrdinal("Id_Uni")),

                            dr.GetValue(dr.GetOrdinal("Prd_Cantidad")),
                            dr.GetValue(dr.GetOrdinal("Prd_Precio")),

                              });
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

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
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "Id_Ped" };
                object[] Valores = { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped };
                //SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedido_Consultar", ref dr, Parametros, Valores);
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoSolicitud_ConsultarV2Autorizacion", ref dr, Parametros, Valores);
                if (dr.HasRows)
                {
                    dr.Read();
                    pedido.Ped_Comentarios = dr.IsDBNull(dr.GetOrdinal("Ped_Comentarios")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Comentarios")).ToString();
                    pedido.Ped_DescPorcen1 = dr.IsDBNull(dr.GetOrdinal("Ped_Desc1")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_DescPorcen1")));
                    pedido.Ped_DescPorcen2 = dr.IsDBNull(dr.GetOrdinal("Ped_Desc2")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_DescPorcen2")));
                    string entrega = dr.GetValue(dr.GetOrdinal("Ped_CondEntrega")).ToString();
                    if (!string.IsNullOrEmpty(entrega))
                        pedido.Ped_CondEntrega = Convert.ToInt32(entrega);
                    else
                        pedido.Ped_CondEntrega = 0;
                    pedido.Ped_Desc1 = dr.IsDBNull(dr.GetOrdinal("Ped_Desc1")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Desc1")).ToString();
                    pedido.Ped_Desc2 = dr.IsDBNull(dr.GetOrdinal("Ped_Desc2")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Desc2")).ToString();
                    pedido.Ped_Flete = dr.IsDBNull(dr.GetOrdinal("Ped_Flete")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Flete")).ToString();
                    pedido.Ped_Importe = dr.IsDBNull(dr.GetOrdinal("Ped_Importe")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Importe")));
                    pedido.Ped_Iva = dr.IsDBNull(dr.GetOrdinal("Ped_Iva")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Iva")));
                    pedido.Id_Cte = dr.IsDBNull(dr.GetOrdinal("Id_Cte")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    pedido.Cte_NomComercial = dr.IsDBNull(dr.GetOrdinal("Cte_NomComercial")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    pedido.Ped_Observaciones = dr.IsDBNull(dr.GetOrdinal("Ped_Observaciones")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Observaciones")).ToString();
                    pedido.Ped_OrdenEntrega = dr.IsDBNull(dr.GetOrdinal("Ped_OrdenEntrega")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_OrdenEntrega")).ToString();
                    pedido.Pedido_del = dr.IsDBNull(dr.GetOrdinal("Ped_PedidoDel")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_PedidoDel")).ToString();
                    pedido.Id_Rik = dr.IsDBNull(dr.GetOrdinal("Id_Rik")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    pedido.Rik_Nombre = dr.IsDBNull(dr.GetOrdinal("Rik_Nombre")) ? "" : dr.GetValue(dr.GetOrdinal("Rik_Nombre")).ToString();
                    pedido.Requisicion = dr.IsDBNull(dr.GetOrdinal("Ped_Requisicion")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Requisicion")).ToString();
                    pedido.Ped_Solicito = dr.IsDBNull(dr.GetOrdinal("Ped_Solicito")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Solicito")).ToString();
                    pedido.Ped_Subtotal = dr.IsDBNull(dr.GetOrdinal("Ped_Subtotal")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Subtotal")));
                    pedido.Id_Ter = dr.IsDBNull(dr.GetOrdinal("Id_Ter")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    pedido.Ter_Nombre = dr.IsDBNull(dr.GetOrdinal("Ter_Nombre")) ? "" : dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();
                    pedido.Ped_Total = dr.IsDBNull(dr.GetOrdinal("Ped_Total")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Total")));
                    pedido.Ped_Fecha = dr.IsDBNull(dr.GetOrdinal("Ped_Fecha")) ? default(DateTime) : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ped_Fecha")));
                    pedido.Ped_FechaEntrega = dr.IsDBNull(dr.GetOrdinal("Ped_FechaEntrega")) ? default(DateTime) : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ped_FechaEntrega")));
                    pedido.Ped_FechFactAcys = dr.IsDBNull(dr.GetOrdinal("Ped_FechFactAcys")) ? default(DateTime) : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ped_FechFactAcys")));


                    pedido.Ped_Tipo = dr.IsDBNull(dr.GetOrdinal("Ped_Tipo")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Tipo")));
                    pedido.Ped_SolicitoTel = dr.IsDBNull(dr.GetOrdinal("Ped_SolicitoTel")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_SolicitoTel")).ToString();
                    pedido.Ped_SolicitoEmail = dr.IsDBNull(dr.GetOrdinal("Ped_SolicitoEmail")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_SolicitoEmail")).ToString();
                    pedido.Ped_SolicitoPuesto = dr.IsDBNull(dr.GetOrdinal("Ped_SolicitoPuesto")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_SolicitoPuesto")).ToString();
                    pedido.Ped_ConsignadoCalle = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoCalle")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoCalle")).ToString();
                    pedido.Ped_ConsignadoNo = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoNo")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoNo")).ToString();
                    pedido.Ped_ConsignadoCp = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoCp")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoCp")).ToString();
                    pedido.Ped_ConsignadoMunicipio = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoMunicipio")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoMunicipio")).ToString();
                    pedido.Ped_ConsignadoEstado = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoEstado")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoEstado")).ToString();
                    pedido.Ped_ConsignadoColonia = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoColonia")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoColonia")).ToString();

                    pedido.Ped_ReqOrden = dr.IsDBNull(dr.GetOrdinal("Ped_ReqOrden")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Ped_ReqOrden")));
                    pedido.Ped_OrdenCompra = dr.IsDBNull(dr.GetOrdinal("Ped_OrdenCompra")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_OrdenCompra")).ToString();
                    pedido.Ped_AcysSemana = dr.IsDBNull(dr.GetOrdinal("Ped_AcysSemana")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_AcysSemana")));
                    pedido.Ped_AcysAnio = dr.IsDBNull(dr.GetOrdinal("Ped_AcysAnio")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_AcysAnio")));
                    pedido.Id_Acs = dr.IsDBNull(dr.GetOrdinal("Id_Acs")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));
                    pedido.Estatus = dr.GetValue(dr.GetOrdinal("Ped_Estatus")).ToString();
                    pedido.ReqAcys = dr.IsDBNull(dr.GetOrdinal("Ped_ReqAcys")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ReqAcys")).ToString();


                    pedido.Acs_ReqDocReposicion = dr.IsDBNull(dr.GetOrdinal("ped_reqdocreposicion")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ped_reqdocreposicion")));
                    pedido.Acs_ReqDocFolio = dr.IsDBNull(dr.GetOrdinal("ped_reqdocfolio")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ped_reqdocfolio")));
                    pedido.Acs_ReqFacturaKey = dr.IsDBNull(dr.GetOrdinal("ped_reqFactKey")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ped_reqFactKey")));
                    pedido.Acs_ReqCopia = dr.IsDBNull(dr.GetOrdinal("ped_reqCopiaDoc")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ped_reqCopiaDoc")));
                    pedido.ACS_ReqRemision = dr.IsDBNull(dr.GetOrdinal("ped_reqRemision")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ped_reqRemision")));


                    pedido.Acs_ReqOrdencop = dr.IsDBNull(dr.GetOrdinal("Acs_ReqOrdencop")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ReqOrdencop")));
                    pedido.Acs_ReqDocReposicioncop = dr.IsDBNull(dr.GetOrdinal("Acs_ReqOrdenrep")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ReqOrdenrep")));
                    pedido.Acs_ReqDocFoliocop = dr.IsDBNull(dr.GetOrdinal("Acs_ReqDocFoliocop")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ReqDocFoliocop")));
                    pedido.Acs_ReqFacturaKeyCop = dr.IsDBNull(dr.GetOrdinal("acs_ReqFacturaKeyCop")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("acs_ReqFacturaKeyCop")));
                    pedido.Acs_ReqCopiaCop = dr.IsDBNull(dr.GetOrdinal("Acs_ReqCopiaCop")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ReqCopiaCop")));
                    pedido.ACS_ReqRemisionCop = dr.IsDBNull(dr.GetOrdinal("ACS_ReqRemisionCop")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_ReqRemisionCop")));




                    pedido.Acs_ReqDocOtro = dr["ped_reqotrodesc"].ToString();
                    pedido.acs_contacto2 = dr["Acs_Contacto2"].ToString();
                    pedido.acs_telefono2 = dr["Acs_Telefono2"].ToString();
                    pedido.acs_email2 = dr["Acs_Email2"].ToString();
                    pedido.acs_contacto3 = dr["Acs_Contacto3"].ToString();
                    pedido.acs_telefono3 = dr["Acs_Telefono3"].ToString();
                    pedido.acs_email3 = dr["Acs_Email3"].ToString();
                    pedido.acs_contacto4 = dr["Acs_Contacto4"].ToString();
                    pedido.acs_telefono4 = dr["Acs_Telefono4"].ToString();
                    pedido.acs_email4 = dr["Acs_Email4"].ToString();
                    pedido.acs_contacto5 = dr["Acs_Contacto5"].ToString();
                    pedido.acs_telefono5 = dr["Acs_Telefono5"].ToString();
                    pedido.acs_email5 = dr["Acs_Email5"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("Id_TG")))
                    {
                        pedido.Id_TG = (int)dr.GetValue(dr.GetOrdinal("Id_TG"));
                    }

                    if (dr.IsDBNull(dr.GetOrdinal("Id_Fac")))
                        pedido.Id_Fac = null;
                    else
                        pedido.Id_Fac = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Fac")));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPedidoDetAutorizacion(Pedido pedido, string Conexion, ref DataTable dt)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "Id_Ped", "Ped_Captacion", "@Filtro_Doc", "@ConMinimo" };
                object[] Valores = { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped, pedido.Ped_Captacion, pedido.Filtro_Doc, -1 };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoSolicitudDetalleAutorizacion_Consultar", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    dt.Rows.Add(new object[] {
                                dr.GetValue(dr.GetOrdinal("Id_PrdOri")),
                                dr.GetValue(dr.GetOrdinal("Id_Prd")),
                                dr.GetValue(dr.GetOrdinal("Prd_Descripcion")),
                                dr.GetValue(dr.GetOrdinal("Prd_Presentacion")),
                                dr.GetValue(dr.GetOrdinal("Prd_Unidad")),
                                0,
                               0,
                               0,
                                dr.GetValue(dr.GetOrdinal("Prd_Cantidad")),
                                dr.GetValue(dr.GetOrdinal("Prd_Precio")),
                               0,
                                dr.GetValue(dr.GetOrdinal("Prd_Importe")),
                                "R",
                                DateTime.Now,
                                0,
                                0 ,
                                0,
                                0,
                                0,
                                0,
                                0,
                                0,
                                "",
                                0
                                ,"",0,dr.GetValue(dr.GetOrdinal("Prd_Activo")),
                           });
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarCancelacioPedido(Pedido Datos, ref List<Pedido> lista, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;
                Pedido registro;
                string[] Parametros = { "@Id_Emp",
                                        "@Id_Cd",
                                        "@FechaIni",
                                        "@FechaFin" };
                object[] Valores = { Datos.Id_Emp, Datos.Id_Cd, Datos.FechaIni, Datos.FechaFin };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("sp_RepCancelacionPed", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    registro = new Pedido();
                    registro.Id_Ped = dr.IsDBNull(dr.GetOrdinal("id_ped")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_ped"));
                    registro.Id_U = dr.IsDBNull(dr.GetOrdinal("id_u")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_u"));
                    registro.U_Nombre = dr.IsDBNull(dr.GetOrdinal("U_Nombre")) ? "" : dr.GetString(dr.GetOrdinal("U_Nombre"));
                    registro.FechaBaja = dr.GetDateTime(dr.GetOrdinal("fecha_baja"));
                    registro.Modulo = dr.IsDBNull(dr.GetOrdinal("Modulo")) ? "" : dr.GetString(dr.GetOrdinal("Modulo"));
                    registro.UsuarioDescripcion = dr.IsDBNull(dr.GetOrdinal("Tu_Descripcion")) ? "" : dr.GetString(dr.GetOrdinal("Tu_Descripcion"));


                    lista.Add(registro);
                }
                dr.Close();
                cd_datos.LimpiarSqlcommand(ref sqlcmd);


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ConsultaPedidoAutorizado(ref Pedido pedido, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "Id_Ped" };
                object[] Valores = { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped };
                //SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedido_Consultar", ref dr, Parametros, Valores);
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spSolPedido_Consultar", ref dr, Parametros, Valores);
                if (dr.HasRows)
                {
                    dr.Read();
                    pedido.Ped_Comentarios = dr.IsDBNull(dr.GetOrdinal("Ped_Comentarios")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Comentarios")).ToString();
                    pedido.Ped_DescPorcen1 = dr.IsDBNull(dr.GetOrdinal("Ped_Desc1")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_DescPorcen1")));
                    pedido.Ped_DescPorcen2 = dr.IsDBNull(dr.GetOrdinal("Ped_Desc2")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_DescPorcen2")));
                    string entrega = dr.GetValue(dr.GetOrdinal("Ped_CondEntrega")).ToString();
                    if (!string.IsNullOrEmpty(entrega))
                        pedido.Ped_CondEntrega = Convert.ToInt32(entrega);
                    else
                        pedido.Ped_CondEntrega = 0;
                    pedido.Ped_Desc1 = dr.IsDBNull(dr.GetOrdinal("Ped_Desc1")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Desc1")).ToString();
                    pedido.Ped_Desc2 = dr.IsDBNull(dr.GetOrdinal("Ped_Desc2")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Desc2")).ToString();
                    pedido.Ped_Flete = dr.IsDBNull(dr.GetOrdinal("Ped_Flete")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Flete")).ToString();
                    pedido.Ped_Importe = dr.IsDBNull(dr.GetOrdinal("Ped_Importe")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Importe")));
                    pedido.Ped_Iva = dr.IsDBNull(dr.GetOrdinal("Ped_Iva")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Iva")));
                    pedido.Id_Cte = dr.IsDBNull(dr.GetOrdinal("Id_Cte")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    pedido.Cte_NomComercial = dr.IsDBNull(dr.GetOrdinal("Cte_NomComercial")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    pedido.Ped_Observaciones = dr.IsDBNull(dr.GetOrdinal("Ped_Observaciones")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Observaciones")).ToString();
                    pedido.Ped_OrdenEntrega = dr.IsDBNull(dr.GetOrdinal("Ped_OrdenEntrega")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_OrdenEntrega")).ToString();
                    pedido.Pedido_del = dr.IsDBNull(dr.GetOrdinal("Ped_PedidoDel")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_PedidoDel")).ToString();
                    pedido.Id_Rik = dr.IsDBNull(dr.GetOrdinal("Id_Rik")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    pedido.Rik_Nombre = dr.IsDBNull(dr.GetOrdinal("Rik_Nombre")) ? "" : dr.GetValue(dr.GetOrdinal("Rik_Nombre")).ToString();
                    pedido.Requisicion = dr.IsDBNull(dr.GetOrdinal("Ped_Requisicion")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Requisicion")).ToString();
                    pedido.Ped_Solicito = dr.IsDBNull(dr.GetOrdinal("Ped_Solicito")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Solicito")).ToString();
                    pedido.Ped_Subtotal = dr.IsDBNull(dr.GetOrdinal("Ped_Subtotal")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Subtotal")));
                    pedido.Id_Ter = dr.IsDBNull(dr.GetOrdinal("Id_Ter")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    pedido.Ter_Nombre = dr.IsDBNull(dr.GetOrdinal("Ter_Nombre")) ? "" : dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();
                    pedido.Ped_Total = dr.IsDBNull(dr.GetOrdinal("Ped_Total")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Total")));
                    pedido.Ped_Fecha = dr.IsDBNull(dr.GetOrdinal("Ped_Fecha")) ? default(DateTime) : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ped_Fecha")));
                    pedido.Ped_FechaEntrega = dr.IsDBNull(dr.GetOrdinal("Ped_FechaEntrega")) ? default(DateTime) : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ped_FechaEntrega")));
                    pedido.Ped_FechFactAcys = dr.IsDBNull(dr.GetOrdinal("Ped_FechFactAcys")) ? default(DateTime) : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ped_FechFactAcys")));


                    pedido.Ped_Tipo = dr.IsDBNull(dr.GetOrdinal("Ped_Tipo")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Tipo")));
                    pedido.Ped_SolicitoTel = dr.IsDBNull(dr.GetOrdinal("Ped_SolicitoTel")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_SolicitoTel")).ToString();
                    pedido.Ped_SolicitoEmail = dr.IsDBNull(dr.GetOrdinal("Ped_SolicitoEmail")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_SolicitoEmail")).ToString();
                    pedido.Ped_SolicitoPuesto = dr.IsDBNull(dr.GetOrdinal("Ped_SolicitoPuesto")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_SolicitoPuesto")).ToString();
                    pedido.Ped_ConsignadoCalle = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoCalle")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoCalle")).ToString();
                    pedido.Ped_ConsignadoNo = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoNo")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoNo")).ToString();
                    pedido.Ped_ConsignadoCp = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoCp")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoCp")).ToString();
                    pedido.Ped_ConsignadoMunicipio = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoMunicipio")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoMunicipio")).ToString();
                    pedido.Ped_ConsignadoEstado = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoEstado")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoEstado")).ToString();
                    pedido.Ped_ConsignadoColonia = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoColonia")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoColonia")).ToString();

                    pedido.Ped_ReqOrden = dr.IsDBNull(dr.GetOrdinal("Ped_ReqOrden")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Ped_ReqOrden")));
                    pedido.Ped_OrdenCompra = dr.IsDBNull(dr.GetOrdinal("Ped_OrdenCompra")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_OrdenCompra")).ToString();
                    pedido.Ped_AcysSemana = dr.IsDBNull(dr.GetOrdinal("Ped_AcysSemana")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_AcysSemana")));
                    pedido.Ped_AcysAnio = dr.IsDBNull(dr.GetOrdinal("Ped_AcysAnio")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_AcysAnio")));
                    pedido.Id_Acs = dr.IsDBNull(dr.GetOrdinal("Id_Acs")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));
                    pedido.Estatus = dr.GetValue(dr.GetOrdinal("Ped_Estatus")).ToString();
                    pedido.ReqAcys = dr.IsDBNull(dr.GetOrdinal("Ped_ReqAcys")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ReqAcys")).ToString();


                    pedido.Acs_ReqDocReposicion = dr.IsDBNull(dr.GetOrdinal("ped_reqdocreposicion")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ped_reqdocreposicion")));
                    pedido.Acs_ReqDocFolio = dr.IsDBNull(dr.GetOrdinal("ped_reqdocfolio")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ped_reqdocfolio")));
                    pedido.Acs_ReqFacturaKey = dr.IsDBNull(dr.GetOrdinal("ped_reqFactKey")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ped_reqFactKey")));
                    pedido.Acs_ReqCopia = dr.IsDBNull(dr.GetOrdinal("ped_reqCopiaDoc")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ped_reqCopiaDoc")));
                    pedido.ACS_ReqRemision = dr.IsDBNull(dr.GetOrdinal("ped_reqRemision")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ped_reqRemision")));


                    pedido.Acs_ReqOrdencop = dr.IsDBNull(dr.GetOrdinal("Acs_ReqOrdencop")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ReqOrdencop")));
                    pedido.Acs_ReqDocReposicioncop = dr.IsDBNull(dr.GetOrdinal("Acs_ReqOrdenrep")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ReqOrdenrep")));
                    pedido.Acs_ReqDocFoliocop = dr.IsDBNull(dr.GetOrdinal("Acs_ReqDocFoliocop")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ReqDocFoliocop")));
                    pedido.Acs_ReqFacturaKeyCop = dr.IsDBNull(dr.GetOrdinal("acs_ReqFacturaKeyCop")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("acs_ReqFacturaKeyCop")));
                    pedido.Acs_ReqCopiaCop = dr.IsDBNull(dr.GetOrdinal("Acs_ReqCopiaCop")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ReqCopiaCop")));
                    pedido.ACS_ReqRemisionCop = dr.IsDBNull(dr.GetOrdinal("ACS_ReqRemisionCop")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_ReqRemisionCop")));
                    pedido.Ped_Tipo = dr.IsDBNull(dr.GetOrdinal("TipoPedido")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("TipoPedido")));
                    pedido.PedidoExterno = dr.IsDBNull(dr.GetOrdinal("PedidoPortal")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("PedidoPortal")));



                    pedido.Acs_ReqDocOtro = dr["ped_reqotrodesc"].ToString();




                    pedido.acs_contacto2 = dr["Acs_Contacto2"].ToString();
                    pedido.acs_telefono2 = dr["Acs_Telefono2"].ToString();
                    pedido.acs_email2 = dr["Acs_Email2"].ToString();
                    pedido.acs_contacto3 = dr["Acs_Contacto3"].ToString();
                    pedido.acs_telefono3 = dr["Acs_Telefono3"].ToString();
                    pedido.acs_email3 = dr["Acs_Email3"].ToString();
                    pedido.acs_contacto4 = dr["Acs_Contacto4"].ToString();
                    pedido.acs_telefono4 = dr["Acs_Telefono4"].ToString();
                    pedido.acs_email4 = dr["Acs_Email4"].ToString();
                    pedido.acs_contacto5 = dr["Acs_Contacto5"].ToString();
                    pedido.acs_telefono5 = dr["Acs_Telefono5"].ToString();
                    pedido.acs_email5 = dr["Acs_Email5"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("Id_TG")))
                    {
                        pedido.Id_TG = (int)dr.GetValue(dr.GetOrdinal("Id_TG"));
                    }

                    if (dr.IsDBNull(dr.GetOrdinal("Id_Fac")))
                        pedido.Id_Fac = null;
                    else
                        pedido.Id_Fac = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Fac")));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaCaptacionPedidoAutorizado(Pedido pedido, string Conexion, ref DataTable dt, ref DataTable dtRestos)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "Id_Ped", "Ped_Captacion", "@Filtro_Doc", "@ConMinimo" };
                object[] Valores = { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped, pedido.Ped_Captacion, pedido.Filtro_Doc, -1 };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spSolPedidoDet_Consultar", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    if (pedido.Ped_Captacion == true)
                    {
                        if (dr.GetValue(dr.GetOrdinal("TipoVta")).ToString() == "NE")
                        {
                            dtRestos.Rows.Add(new object[] {
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Prd_Descripcion")),
                            dr.GetValue(dr.GetOrdinal("Prd_Presentacion")),
                            dr.GetValue(dr.GetOrdinal("Id_Uni")),

                            dr.GetValue(dr.GetOrdinal("mes1")),
                            dr.GetValue(dr.GetOrdinal("mes2")),
                            dr.GetValue(dr.GetOrdinal("mes3")),

                            dr.GetValue(dr.GetOrdinal("Prd_Cantidad")),
                            dr.GetValue(dr.GetOrdinal("Prd_Precio")),
                            dr.IsDBNull(dr.GetOrdinal("Acs_PrecioAcys"))? 0 : dr.GetValue(dr.GetOrdinal("Acs_PrecioAcys")),
                            dr.GetValue(dr.GetOrdinal("Prd_Importe")),
                            Str(dr.GetValue(dr.GetOrdinal("Acs_Documento"))),

                            dr.IsDBNull(dr.GetOrdinal("Acs_Fecha"))? DateTime.Now : dr.GetValue(dr.GetOrdinal("Acs_Fecha")),
                            dr.GetValue(dr.GetOrdinal("Acs_Mod")) ,
                            dr.GetValue(dr.GetOrdinal("Acs_Dia")) ,
                            Nombre(dr.GetValue(dr.GetOrdinal("Acs_Dia"))),
                            dr.GetValue(dr.GetOrdinal("Acs_Frecuencia")),

                             0,
                             dr.GetValue(dr.GetOrdinal("Ped_Asignar")),
                             0,
                             0,
                             dr.IsDBNull(dr.GetOrdinal("ACS_ReqOC")) ? 0 : dr.GetValue(dr.GetOrdinal("ACS_ReqOC")),
                             0,
                             dr.GetValue(dr.GetOrdinal("Prd_Cantidad"))
                       });
                        }
                        else
                        {
                            dt.Rows.Add(new object[] {
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Prd_Descripcion")),
                            dr.GetValue(dr.GetOrdinal("Prd_Presentacion")),
                            dr.GetValue(dr.GetOrdinal("Id_Uni")),

                            dr.GetValue(dr.GetOrdinal("mes1")),
                            dr.GetValue(dr.GetOrdinal("mes2")),
                            dr.GetValue(dr.GetOrdinal("mes3")),

                            dr.GetValue(dr.GetOrdinal("Prd_Cantidad")),
                            dr.GetValue(dr.GetOrdinal("Prd_Precio")),
                            dr.IsDBNull(dr.GetOrdinal("Acs_PrecioAcys"))? 0 : dr.GetValue(dr.GetOrdinal("Acs_PrecioAcys")),
                            dr.GetValue(dr.GetOrdinal("Prd_Importe")),
                            Str(dr.GetValue(dr.GetOrdinal("Acs_Documento"))),

                            dr.IsDBNull(dr.GetOrdinal("Acs_Fecha"))? DateTime.Now : dr.GetValue(dr.GetOrdinal("Acs_Fecha")),
                            dr.GetValue(dr.GetOrdinal("Acs_Mod")) ,
                            dr.GetValue(dr.GetOrdinal("Acs_Dia")) ,
                            Nombre(dr.GetValue(dr.GetOrdinal("Acs_Dia"))),
                              dr.GetValue(dr.GetOrdinal("Acs_Frecuencia")),

                           0,
                           dr.GetValue(dr.GetOrdinal("Ped_Asignar")),
                           0,
                           0,
                             dr.IsDBNull(dr.GetOrdinal("ACS_ReqOC")) ? 0 : dr.GetValue(dr.GetOrdinal("ACS_ReqOC")),
                            0,
                                dr.GetValue(dr.GetOrdinal("Prd_Cantidad"))
                              });
                        }
                    }
                    else
                    {
                        if (dr.GetValue(dr.GetOrdinal("TipoVta")).ToString() == "NE")
                        {
                            dtRestos.Rows.Add(new object[] {
                            dr.GetValue(dr.GetOrdinal("Id_PedDet")),
                            dr.GetValue(dr.GetOrdinal("Id_Ter")),
                            dr.GetValue(dr.GetOrdinal("Ter_Nombre")),
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Prd_Descripcion")),
                            dr.GetValue(dr.GetOrdinal("Prd_Presentacion")),
                            dr.GetValue(dr.GetOrdinal("Prd_Unidad")),
                            dr.GetValue(dr.GetOrdinal("Prd_Precio")),
                            dr.GetValue(dr.GetOrdinal("Prd_Cantidad")),
                            dr.GetValue(dr.GetOrdinal("Prd_Importe")),
                            dr.GetValue(dr.GetOrdinal("TipoVta")),
                            0,
                             dr.IsDBNull(dr.GetOrdinal("ACS_ReqOC")) ? 0 : dr.GetValue(dr.GetOrdinal("ACS_ReqOC")),
                            0,
                            dr.GetValue(dr.GetOrdinal("Prd_Cantidad"))
                    });
                        }
                        else
                        {
                            dt.Rows.Add(new object[] {
                            dr.GetValue(dr.GetOrdinal("Id_PedDet")),
                            dr.GetValue(dr.GetOrdinal("Id_Ter")),
                            dr.GetValue(dr.GetOrdinal("Ter_Nombre")),
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Prd_Descripcion")),
                            dr.GetValue(dr.GetOrdinal("Prd_Presentacion")),
                            dr.GetValue(dr.GetOrdinal("Prd_Unidad")),
                            dr.GetValue(dr.GetOrdinal("Prd_Precio")),
                            dr.GetValue(dr.GetOrdinal("Prd_Cantidad")),
                            dr.GetValue(dr.GetOrdinal("Prd_Importe")),
                            dr.GetValue(dr.GetOrdinal("TipoVta")),
                            0,
                            dr.IsDBNull(dr.GetOrdinal("ACS_ReqOC")) ? 0 : dr.GetValue(dr.GetOrdinal("ACS_ReqOC")),
                            0,
                            dr.GetValue(dr.GetOrdinal("Prd_Cantidad"))

                             });
                        }
                    }
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

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
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "Id_Ped" };
                object[] Valores = { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped };
                //SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedido_Consultar", ref dr, Parametros, Valores);
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoSolicitud_ConsultarV2Autorizacion", ref dr, Parametros, Valores);
                if (dr.HasRows)
                {
                    dr.Read();
                    pedido.Ped_Comentarios = dr.IsDBNull(dr.GetOrdinal("Ped_Comentarios")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Comentarios")).ToString();
                    pedido.Ped_DescPorcen1 = dr.IsDBNull(dr.GetOrdinal("Ped_Desc1")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_DescPorcen1")));
                    pedido.Ped_DescPorcen2 = dr.IsDBNull(dr.GetOrdinal("Ped_Desc2")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_DescPorcen2")));
                    string entrega = dr.GetValue(dr.GetOrdinal("Ped_CondEntrega")).ToString();
                    if (!string.IsNullOrEmpty(entrega))
                        pedido.Ped_CondEntrega = Convert.ToInt32(entrega);
                    else
                        pedido.Ped_CondEntrega = 0;
                    pedido.Ped_Desc1 = dr.IsDBNull(dr.GetOrdinal("Ped_Desc1")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Desc1")).ToString();
                    pedido.Ped_Desc2 = dr.IsDBNull(dr.GetOrdinal("Ped_Desc2")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Desc2")).ToString();
                    pedido.Ped_Flete = dr.IsDBNull(dr.GetOrdinal("Ped_Flete")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Flete")).ToString();
                    pedido.Ped_Importe = dr.IsDBNull(dr.GetOrdinal("Ped_Importe")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Importe")));
                    pedido.Ped_Iva = dr.IsDBNull(dr.GetOrdinal("Ped_Iva")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Iva")));
                    pedido.Id_Cte = dr.IsDBNull(dr.GetOrdinal("Id_Cte")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    pedido.Cte_NomComercial = dr.IsDBNull(dr.GetOrdinal("Cte_NomComercial")) ? "" : dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                    pedido.Ped_Observaciones = dr.IsDBNull(dr.GetOrdinal("Ped_Observaciones")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Observaciones")).ToString();
                    pedido.Ped_OrdenEntrega = dr.IsDBNull(dr.GetOrdinal("Ped_OrdenEntrega")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_OrdenEntrega")).ToString();
                    pedido.Pedido_del = dr.IsDBNull(dr.GetOrdinal("Ped_PedidoDel")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_PedidoDel")).ToString();
                    pedido.Id_Rik = dr.IsDBNull(dr.GetOrdinal("Id_Rik")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rik")));
                    pedido.Rik_Nombre = dr.IsDBNull(dr.GetOrdinal("Rik_Nombre")) ? "" : dr.GetValue(dr.GetOrdinal("Rik_Nombre")).ToString();
                    pedido.Requisicion = dr.IsDBNull(dr.GetOrdinal("Ped_Requisicion")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Requisicion")).ToString();
                    pedido.Ped_Solicito = dr.IsDBNull(dr.GetOrdinal("Ped_Solicito")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_Solicito")).ToString();
                    pedido.Ped_Subtotal = dr.IsDBNull(dr.GetOrdinal("Ped_Subtotal")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Subtotal")));
                    pedido.Id_Ter = dr.IsDBNull(dr.GetOrdinal("Id_Ter")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    pedido.Ter_Nombre = dr.IsDBNull(dr.GetOrdinal("Ter_Nombre")) ? "" : dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();
                    pedido.Ped_Total = dr.IsDBNull(dr.GetOrdinal("Ped_Total")) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ped_Total")));
                    pedido.Ped_Fecha = dr.IsDBNull(dr.GetOrdinal("Ped_Fecha")) ? default(DateTime) : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ped_Fecha")));
                    pedido.Ped_FechaEntrega = dr.IsDBNull(dr.GetOrdinal("Ped_FechaEntrega")) ? default(DateTime) : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ped_FechaEntrega")));
                    pedido.Ped_FechFactAcys = dr.IsDBNull(dr.GetOrdinal("Ped_FechFactAcys")) ? default(DateTime) : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Ped_FechFactAcys")));


                    pedido.Ped_Tipo = dr.IsDBNull(dr.GetOrdinal("Ped_Tipo")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_Tipo")));
                    pedido.Ped_SolicitoTel = dr.IsDBNull(dr.GetOrdinal("Ped_SolicitoTel")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_SolicitoTel")).ToString();
                    pedido.Ped_SolicitoEmail = dr.IsDBNull(dr.GetOrdinal("Ped_SolicitoEmail")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_SolicitoEmail")).ToString();
                    pedido.Ped_SolicitoPuesto = dr.IsDBNull(dr.GetOrdinal("Ped_SolicitoPuesto")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_SolicitoPuesto")).ToString();
                    pedido.Ped_ConsignadoCalle = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoCalle")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoCalle")).ToString();
                    pedido.Ped_ConsignadoNo = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoNo")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoNo")).ToString();
                    pedido.Ped_ConsignadoCp = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoCp")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoCp")).ToString();
                    pedido.Ped_ConsignadoMunicipio = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoMunicipio")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoMunicipio")).ToString();
                    pedido.Ped_ConsignadoEstado = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoEstado")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoEstado")).ToString();
                    pedido.Ped_ConsignadoColonia = dr.IsDBNull(dr.GetOrdinal("Ped_ConsignadoColonia")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ConsignadoColonia")).ToString();

                    pedido.Ped_ReqOrden = dr.IsDBNull(dr.GetOrdinal("Ped_ReqOrden")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Ped_ReqOrden")));
                    pedido.Ped_OrdenCompra = dr.IsDBNull(dr.GetOrdinal("Ped_OrdenCompra")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_OrdenCompra")).ToString();
                    pedido.Ped_AcysSemana = dr.IsDBNull(dr.GetOrdinal("Ped_AcysSemana")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_AcysSemana")));
                    pedido.Ped_AcysAnio = dr.IsDBNull(dr.GetOrdinal("Ped_AcysAnio")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Ped_AcysAnio")));
                    pedido.Id_Acs = dr.IsDBNull(dr.GetOrdinal("Id_Acs")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));
                    pedido.Estatus = dr.GetValue(dr.GetOrdinal("Ped_Estatus")).ToString();
                    pedido.ReqAcys = dr.IsDBNull(dr.GetOrdinal("Ped_ReqAcys")) ? "" : dr.GetValue(dr.GetOrdinal("Ped_ReqAcys")).ToString();


                    pedido.Acs_ReqDocReposicion = dr.IsDBNull(dr.GetOrdinal("ped_reqdocreposicion")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ped_reqdocreposicion")));
                    pedido.Acs_ReqDocFolio = dr.IsDBNull(dr.GetOrdinal("ped_reqdocfolio")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ped_reqdocfolio")));
                    pedido.Acs_ReqFacturaKey = dr.IsDBNull(dr.GetOrdinal("ped_reqFactKey")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ped_reqFactKey")));
                    pedido.Acs_ReqCopia = dr.IsDBNull(dr.GetOrdinal("ped_reqCopiaDoc")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ped_reqCopiaDoc")));
                    pedido.ACS_ReqRemision = dr.IsDBNull(dr.GetOrdinal("ped_reqRemision")) ? false : Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("ped_reqRemision")));


                    pedido.Acs_ReqOrdencop = dr.IsDBNull(dr.GetOrdinal("Acs_ReqOrdencop")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ReqOrdencop")));
                    pedido.Acs_ReqDocReposicioncop = dr.IsDBNull(dr.GetOrdinal("Acs_ReqOrdenrep")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ReqOrdenrep")));
                    pedido.Acs_ReqDocFoliocop = dr.IsDBNull(dr.GetOrdinal("Acs_ReqDocFoliocop")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ReqDocFoliocop")));
                    pedido.Acs_ReqFacturaKeyCop = dr.IsDBNull(dr.GetOrdinal("acs_ReqFacturaKeyCop")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("acs_ReqFacturaKeyCop")));
                    pedido.Acs_ReqCopiaCop = dr.IsDBNull(dr.GetOrdinal("Acs_ReqCopiaCop")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_ReqCopiaCop")));
                    pedido.ACS_ReqRemisionCop = dr.IsDBNull(dr.GetOrdinal("ACS_ReqRemisionCop")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ACS_ReqRemisionCop")));




                    pedido.Acs_ReqDocOtro = dr["ped_reqotrodesc"].ToString();
                    pedido.acs_contacto2 = dr["Acs_Contacto2"].ToString();
                    pedido.acs_telefono2 = dr["Acs_Telefono2"].ToString();
                    pedido.acs_email2 = dr["Acs_Email2"].ToString();
                    pedido.acs_contacto3 = dr["Acs_Contacto3"].ToString();
                    pedido.acs_telefono3 = dr["Acs_Telefono3"].ToString();
                    pedido.acs_email3 = dr["Acs_Email3"].ToString();
                    pedido.acs_contacto4 = dr["Acs_Contacto4"].ToString();
                    pedido.acs_telefono4 = dr["Acs_Telefono4"].ToString();
                    pedido.acs_email4 = dr["Acs_Email4"].ToString();
                    pedido.acs_contacto5 = dr["Acs_Contacto5"].ToString();
                    pedido.acs_telefono5 = dr["Acs_Telefono5"].ToString();
                    pedido.acs_email5 = dr["Acs_Email5"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("Id_TG")))
                    {
                        pedido.Id_TG = (int)dr.GetValue(dr.GetOrdinal("Id_TG"));
                    }

                    if (dr.IsDBNull(dr.GetOrdinal("Id_Fac")))
                        pedido.Id_Fac = null;
                    else
                        pedido.Id_Fac = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Fac")));
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaproPedidoDetAut(Pedido pedido, string Conexion, ref DataTable dt, ref DataTable dtRestos)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "Id_Ped", "Ped_Captacion", "@Filtro_Doc", "@ConMinimo" };
                object[] Valores = { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped, pedido.Ped_Captacion, pedido.Filtro_Doc, -1 };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDet_Consultar2", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    if (pedido.Ped_Captacion == true)
                    {
                        if (dr.GetValue(dr.GetOrdinal("TipoVta")).ToString() == "NE")
                        {
                            dtRestos.Rows.Add(new object[] {
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Prd_Descripcion")),
                            dr.GetValue(dr.GetOrdinal("Prd_Presentacion")),
                            dr.GetValue(dr.GetOrdinal("Id_Uni")),

                            dr.GetValue(dr.GetOrdinal("mes1")),
                            dr.GetValue(dr.GetOrdinal("mes2")),
                            dr.GetValue(dr.GetOrdinal("mes3")),

                            dr.GetValue(dr.GetOrdinal("Prd_Cantidad")),
                            dr.GetValue(dr.GetOrdinal("Prd_Precio")),
                            dr.IsDBNull(dr.GetOrdinal("Acs_PrecioAcys"))? 0 : dr.GetValue(dr.GetOrdinal("Acs_PrecioAcys")),
                            dr.GetValue(dr.GetOrdinal("Prd_Importe")),
                            Str(dr.GetValue(dr.GetOrdinal("Acs_Documento"))),

                            dr.IsDBNull(dr.GetOrdinal("Acs_Fecha"))? DateTime.Now : dr.GetValue(dr.GetOrdinal("Acs_Fecha")),
                            dr.GetValue(dr.GetOrdinal("Acs_Mod")) ,
                            dr.GetValue(dr.GetOrdinal("Acs_Dia")) ,
                            Nombre(dr.GetValue(dr.GetOrdinal("Acs_Dia"))),
                            dr.GetValue(dr.GetOrdinal("Acs_Frecuencia")),

                             0,
                             dr.GetValue(dr.GetOrdinal("Ped_Asignar")),
                             0,
                             0,
                             dr.IsDBNull(dr.GetOrdinal("ACS_ReqOC")) ? 0 : dr.GetValue(dr.GetOrdinal("ACS_ReqOC")),
                             0,
                             dr.GetValue(dr.GetOrdinal("Prd_Cantidad"))
                       });
                        }
                        else
                        {
                            dt.Rows.Add(new object[] {
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Prd_Descripcion")),
                            dr.GetValue(dr.GetOrdinal("Prd_Presentacion")),
                            dr.GetValue(dr.GetOrdinal("Id_Uni")),

                            dr.GetValue(dr.GetOrdinal("mes1")),
                            dr.GetValue(dr.GetOrdinal("mes2")),
                            dr.GetValue(dr.GetOrdinal("mes3")),

                            dr.GetValue(dr.GetOrdinal("Prd_Cantidad")),
                            dr.GetValue(dr.GetOrdinal("Prd_Precio")),
                            dr.IsDBNull(dr.GetOrdinal("Acs_PrecioAcys"))? 0 : dr.GetValue(dr.GetOrdinal("Acs_PrecioAcys")),
                            dr.GetValue(dr.GetOrdinal("Prd_Importe")),
                            Str(dr.GetValue(dr.GetOrdinal("Acs_Documento"))),

                            dr.IsDBNull(dr.GetOrdinal("Acs_Fecha"))? DateTime.Now : dr.GetValue(dr.GetOrdinal("Acs_Fecha")),
                            dr.GetValue(dr.GetOrdinal("Acs_Mod")) ,
                            dr.GetValue(dr.GetOrdinal("Acs_Dia")) ,
                            Nombre(dr.GetValue(dr.GetOrdinal("Acs_Dia"))),
                              dr.GetValue(dr.GetOrdinal("Acs_Frecuencia")),

                           0,
                           dr.GetValue(dr.GetOrdinal("Ped_Asignar")),
                           0,
                           0,
                             dr.IsDBNull(dr.GetOrdinal("ACS_ReqOC")) ? 0 : dr.GetValue(dr.GetOrdinal("ACS_ReqOC")),
                            0,
                                dr.GetValue(dr.GetOrdinal("Prd_Cantidad"))
                              });
                        }
                    }
                    else
                    {
                        if (dr.GetValue(dr.GetOrdinal("TipoVta")).ToString() == "NE")
                        {
                            dtRestos.Rows.Add(new object[] {
                            dr.GetValue(dr.GetOrdinal("Id_PedDet")),
                            dr.GetValue(dr.GetOrdinal("Id_Ter")),
                            dr.GetValue(dr.GetOrdinal("Ter_Nombre")),
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Prd_Descripcion")),
                            dr.GetValue(dr.GetOrdinal("Prd_Presentacion")),
                            dr.GetValue(dr.GetOrdinal("Prd_Unidad")),
                            dr.GetValue(dr.GetOrdinal("Prd_Precio")),
                            dr.GetValue(dr.GetOrdinal("Prd_Cantidad")),
                            dr.GetValue(dr.GetOrdinal("Prd_Importe")),
                            dr.GetValue(dr.GetOrdinal("TipoVta")),
                            0,
                             dr.IsDBNull(dr.GetOrdinal("ACS_ReqOC")) ? 0 : dr.GetValue(dr.GetOrdinal("ACS_ReqOC")),
                            0,
                            dr.GetValue(dr.GetOrdinal("Prd_Cantidad"))
                    });
                        }
                        else
                        {
                            dt.Rows.Add(new object[] {
                            dr.GetValue(dr.GetOrdinal("Id_PedDet")),
                            dr.GetValue(dr.GetOrdinal("Id_Ter")),
                            dr.GetValue(dr.GetOrdinal("Ter_Nombre")),
                            dr.GetValue(dr.GetOrdinal("Id_Prd")),
                            dr.GetValue(dr.GetOrdinal("Prd_Descripcion")),
                            dr.GetValue(dr.GetOrdinal("Prd_Presentacion")),
                            dr.GetValue(dr.GetOrdinal("Prd_Unidad")),
                            dr.GetValue(dr.GetOrdinal("Prd_Precio")),
                            dr.GetValue(dr.GetOrdinal("Prd_Cantidad")),
                            dr.GetValue(dr.GetOrdinal("Prd_Importe")),
                            dr.GetValue(dr.GetOrdinal("TipoVta")),
                            0,
                            dr.IsDBNull(dr.GetOrdinal("ACS_ReqOC")) ? 0 : dr.GetValue(dr.GetOrdinal("ACS_ReqOC")),
                            0,
                            dr.GetValue(dr.GetOrdinal("Prd_Cantidad"))

                             });
                        }
                    }
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void spCapFacturaDetalle_Consultar(PedidoVtaInst pedido, ref List<PedidoDet> det, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Ped", "@TipoCancelacion" };
                object[] Valores = { pedido.Id_Emp, pedido.Id_Cd, pedido.Id_Ped, pedido.TipoCancelacion };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoDetalle_Consultar", ref dr, Parametros, Valores);
                PedidoDet detalle;
                while (dr.Read())
                {
                    detalle = new PedidoDet();
                    detalle.Id_Prd = dr.GetInt64(dr.GetOrdinal("Id_Prd"));
                    detalle.Ped_Cantidad = dr.GetInt32(dr.GetOrdinal("Ped_Cantidad"));
                    det.Add(detalle);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);


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
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(strConexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@Id_Cte", "@Id_Cd" };
                object[] Valores = { intIdCte, IntIdCd };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedido_ConsultarFechaEntrega", ref dr, Parametros, Valores);
                if (dr.HasRows)
                {
                    dr.Read();
                    strFechaEntrega = dr.GetValue(dr.GetOrdinal("Cte_FechaEntrega")).ToString();
                    intDiasEntrega = dr.IsDBNull(dr.GetOrdinal("Cte_DiasEntrega")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cte_DiasEntrega")));
                    fechaEntrega = dr.IsDBNull(dr.GetOrdinal("FechaEntregaLimite")) ? DateTime.Now : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaEntregaLimite")));
                    intEsModificable = dr.IsDBNull(dr.GetOrdinal("EsModificable")) ? 1 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("EsModificable")));
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}