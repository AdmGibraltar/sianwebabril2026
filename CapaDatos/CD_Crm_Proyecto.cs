using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Crm_Proyecto
    {
        public void ObtenerTerrProyectos(Crm_Proyectos datos, ref List<Crm_Proyectos> lista, string conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(conexion);


                string[] parametros = {
                                            "@Id_Emp",
                                            "@Id_Cd",
                                            "@Id_Rik",
                                            "@Id_Cte"
                                      };

                object[] Valores = {
                                       datos.Id_Emp,
                                       datos.Id_Cd,
                                       datos.Id_Rik,
                                       datos.Id_Cte
                                   };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRM_ObtenerTerrProyectos", ref dr, parametros, Valores);
                Crm_Proyectos registros;
                while (dr.Read())
                {
                    registros = new Crm_Proyectos();
                    registros.Id_Emp = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Emp"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Id_Emp")));
                    registros.Id_Cd = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cd"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Id_Cd")));
                    registros.Id_Op = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Op"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Id_Op")));
                    registros.Id_Cte = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Cte"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Id_Cte")));
                    registros.Valuacion = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Valuacion"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Valuacion")));
                    registros.Id_Ter = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_Ter"))) ? -1 : dr.GetInt32((dr.GetOrdinal("Id_Ter")));
                    registros.Estatus = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Estatus"))) ? -1 : dr.GetInt32(dr.GetOrdinal("Estatus"));
                    registros.Id_CrmProspecto = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Id_CrmProspecto"))) ? -1 : dr.GetInt32(dr.GetOrdinal("Id_CrmProspecto"));
                    lista.Add(registros);
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