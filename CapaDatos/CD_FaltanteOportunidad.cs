using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data;
using System.Data.SqlClient;
namespace CapaDatos
{
    public class CD_FaltanteOportunidad
    {
        public void ConsultaFaltanteOportunidad(string Conexion, FaltanteOportunidad Faltante, ref DataSet dsResultado)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = new string[]{
                                                    "@Id_Cd",
                                                    "@Cierre"
                                                   };

                object[] Valores = new object[] { 
                                                   Faltante.Id_Cd,
                                                   Faltante.TipoReporte
                                                };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spImportaFaltantesOportVenta", ref dsResultado, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LlenaCombo(int Id_Grupo, string Conexion, string sp, ref List<Comun> Lista)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Grupo" };

                object[] Valores = { Id_Grupo };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(sp, ref dr, Parametros, Valores);

                Comun Comun = default(Comun);
                while (dr.Read())
                {
                    Comun = new Comun();
                    Comun.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    Comun.Descripcion = dr.GetString(dr.GetOrdinal("Descripcion"));
                    Lista.Add(Comun);
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}