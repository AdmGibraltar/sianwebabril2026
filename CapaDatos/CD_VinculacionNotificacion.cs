using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_VinculacionNotificacion
    {
        public int Consultar_CountNoti(int Id_Cd, int Id_Rik, string Conexion)
        {
            int contador = 0;
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Cd", "@Id_Rik" };
                object[] Valores = { Id_Cd, Id_Rik };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spConsultar_VinculacionCentralCRM", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    contador++;
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                contador = 0;
            }
            return contador;
        }
    }
}