using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_TipoQueja
    {
        public void ConsultaTipoQueja_Buscar(eTipoQueja tipoqueja, ref List<eTipoQueja> listaNotaCredito, string Conexion)
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
                                       tipoqueja.Id_tQueja
                                       ,1
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spTipoQueja_Buscar", ref dr, Parametros, Valores);
                listaNotaCredito = new List<eTipoQueja>();
                while (dr.Read())
                {
                    eTipoQueja oeTipoQueja = new eTipoQueja();
                    oeTipoQueja.Id_tQueja = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_tQueja")));
                    oeTipoQueja.Tipo_Queja = dr.IsDBNull(dr.GetOrdinal("Tipo_Queja")) ? string.Empty : dr.GetValue(dr.GetOrdinal("Tipo_Queja")).ToString();
                    oeTipoQueja.Estatus = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Estatus")));


                    listaNotaCredito.Add(oeTipoQueja);
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