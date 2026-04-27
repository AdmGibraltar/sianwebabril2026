using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_HistorialPedido
    {
        public void RastreoPedido_Captacion(HistorialPedidos pedido, string Conexion, ref List<HistorialPedidos> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { 
                                          "@FechaIni", 
                                          "@FechaFin", 
                                          "@strPedido", 
                                          "@iEstatus" 
                                      };
                object[] Valores = { 
                                    
                                       pedido.Filtro_FechaInicial == "" ? (object)null: pedido.Filtro_FechaInicial, 
                                       pedido.Filtro_FechaFinal == "" ? (object)null: pedido.Filtro_FechaFinal, 
                                       pedido.Filtro_strPedido == "" ? (object)null: pedido.Filtro_strPedido, 
                                       pedido.Filtro_Estatus  == "" ? (object)null: pedido.Filtro_Estatus, 
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spHistorialPedidos2", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    pedido = new HistorialPedidos();
                    pedido.Id_emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    pedido.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    pedido.Id_Cte = (int)dr.GetValue(dr.GetOrdinal("IdCte"));
                    pedido.NomCte = (string)dr.GetValue(dr.GetOrdinal("NomCte"));
                    pedido.RSC = (string)dr.GetValue(dr.GetOrdinal("RSC"));
                    pedido.Id_Ped = (int)dr.GetValue(dr.GetOrdinal("Id_Ped"));
                    pedido.Id_Fac = (int)dr.GetValue(dr.GetOrdinal("Id_Fac"));
                    pedido.Orden = (int)dr.GetValue(dr.GetOrdinal("Orden"));
                    pedido.TipoFecha = (string)dr.GetValue(dr.GetOrdinal("TipoFecha"));
                    pedido.Fecha = (DateTime)dr.GetValue(dr.GetOrdinal("Fecha"));
                    pedido.porcentaje = (double)dr.GetValue(dr.GetOrdinal("PorcentajeF")); 
                    List.Add(pedido);
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
