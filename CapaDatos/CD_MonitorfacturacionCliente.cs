using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_MonitorfacturacionCliente
    {
        public void ConsultarMonitorAgenda(FacturacionCliente agenda, ref List<FacturacionCliente> List, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@fecha"
                                      };
                object[] Valores = {
                                        agenda.id_emp,
                                        agenda.id_Cd,
                                        agenda.fecha
                                    };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spcat_Consultarfacturacion", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    agenda = new FacturacionCliente();
                    agenda.id_cte = dr.IsDBNull(dr.GetOrdinal("Id_Cte")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    agenda.nombreCliente = dr.IsDBNull(dr.GetOrdinal("Cte_NomComercial")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("Cte_NomComercial")));
                    agenda.Factura = dr.IsDBNull(dr.GetOrdinal("id_factura")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_factura")));
                    agenda.FechaFactura = dr.IsDBNull(dr.GetOrdinal("fecha_facturacion")) ? DateTime.Now : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("fecha_facturacion")));
                    agenda.mesFactura = dr.IsDBNull(dr.GetOrdinal("meses_facturacion")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("meses_facturacion")));
                    agenda.mes = dr.IsDBNull(dr.GetOrdinal("mes")) ? 0 : Convert.ToInt32(dr.GetValue(dr.GetOrdinal("mes")));
                    List.Add(agenda);
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