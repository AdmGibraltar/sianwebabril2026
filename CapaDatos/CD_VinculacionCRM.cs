using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_VinculacionCRM
    {
        public int ActualizarTerritorioOpo(int Id_Cd, int Id_Cte, int Id_Terr, int IdRik, string conexion)
        {
            SqlDataReader dr = null;
            var resultUpdate = 0;
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);

            try
            {
                //CapaDatos.StartTrans();
                string[] Parametros = {
                                        "@Id_Cd",
                                        "@Id_Cte",
                                        "@Id_Terr",
                                        "@Id_Usu",

                                      };
                object[] Valores = {
                                        Id_Cd,
                                        Id_Cte ,
                                        Id_Terr,
                                        IdRik,
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spVinculacionCRM_ActualizarOpoxTerr", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    dr.Read();
                    var result = dr.GetValue(dr.GetOrdinal("Result"));
                    resultUpdate = result == null ? 0 : Convert.ToInt32(result);

                }
                sqlcmd.Dispose();
            }
            catch (Exception ex)
            {
                resultUpdate = 0;
                throw ex;
            }

            return resultUpdate;
        }
    }
}