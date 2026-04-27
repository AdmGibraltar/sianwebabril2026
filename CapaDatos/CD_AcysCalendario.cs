using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_AcysCalendario
    {
        public void spAcuerdosExistentesPorCliente(AcysCalendario Datos, ref List<AcysCalendario> Lista, string Conexion)
        {

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                AcysCalendario Registros;
                string[] Parametros = {
                                      "@Id_Emp",
                                      "@Id_Cd",
                                      "@Id_Acs",
                                      "@Anio",
                                      "@Producto"
                                      };

                object[] Valores = {
                                   Datos.Id_Emp,
                                   Datos.Id_Cd,
                                   Datos.Id_Acs,
                                   Datos.Acs_Anio,
                                   Datos.Id_Prd == 0? (object)null:  Datos.Id_Prd
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapAcys_ConsultaCalendario", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    Registros = new AcysCalendario();

                    Registros.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    Registros.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    Registros.Id_Acs = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Acs")));
                    Registros.Id_Prd = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    Registros.Id_Ter = Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    Registros.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    Registros.Acs_Anio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Anio")));
                    Registros.Acs_Semana = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Semana")));
                    Registros.Acs_Cantidad = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Acs_Cantidad")));
                    Registros.Acs_Precio = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Acs_Precio")));
                    Registros.Cte_NomComercial = Convert.ToString(dr.GetValue(dr.GetOrdinal("Cte_NomComercial")));
                    Registros.Prd_Descripcion = Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_Descripcion")));
                    Registros.Fecha_Inicial = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Sem_FechaIni")));
                    Registros.Frecuencia = Convert.ToString(dr.GetValue(dr.GetOrdinal("Acs_FrecuenciaTipo")));


                    Lista.Add(Registros);
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