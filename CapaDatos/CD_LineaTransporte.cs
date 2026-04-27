using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_LineaTransporte
    {
        public void ConsultaLineaTransporte_Buscar(eLineaTransporte tipoqueja, ref List<eLineaTransporte> listaNotaCredito, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id"
                                          ,"@Estatus"
                                      };
                object[] Valores = {
                                       tipoqueja.Id
                                       ,tipoqueja.Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spLineaTransporte_Buscar", ref dr, Parametros, Valores);
                listaNotaCredito = new List<eLineaTransporte>();
                while (dr.Read())
                {
                    eLineaTransporte oeLineaTransporte = new eLineaTransporte();
                    oeLineaTransporte.Id = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    oeLineaTransporte.LineaTransporte = dr.IsDBNull(dr.GetOrdinal("LineaTransporte")) ? string.Empty : dr.GetValue(dr.GetOrdinal("LineaTransporte")).ToString();
                    oeLineaTransporte.Estatus = dr.IsDBNull(dr.GetOrdinal("LineaTransporte")) ? string.Empty : dr.GetValue(dr.GetOrdinal("Estatus")).ToString();


                    listaNotaCredito.Add(oeLineaTransporte);
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