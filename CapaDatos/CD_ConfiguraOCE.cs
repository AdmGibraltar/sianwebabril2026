using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaEntidad;

namespace CapaDatos
{
    public class CD_ConfiguraOCE
    {
        public void LlenaListadoConfiguraOCE(string Conexion, string sp, int Id_CDI, ref List<ConfiguraOCE> Lista)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Cd", "@IdCveConfiguraOCE" };

                object[] Valores = { Id_CDI, 0 };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(sp, ref dr, Parametros, Valores);

                ConfiguraOCE rngLista = default(ConfiguraOCE);
                while (dr.Read())
                {
                    rngLista = new ConfiguraOCE();
                    rngLista.IdConfiguraOCE = dr.GetInt32(dr.GetOrdinal("IdConfiguraOCE"));
                    rngLista.Id_Cd = dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                    /// rngLista.Sucursal = dr.GetString(dr.GetOrdinal("Sucursal"));
                    rngLista.IdCveConfiguraOCE = dr.GetString(dr.GetOrdinal("IdCveConfiguraOCE"));
                    rngLista.ConceptoConfiguraOCE = dr.GetString(dr.GetOrdinal("ConceptoConfiguraOCE"));
                    rngLista.ValorConfiguraOCE = dr.GetString(dr.GetOrdinal("ValorConfiguraOCE"));
                    rngLista.iActivo = dr.GetString(dr.GetOrdinal("iActivo"));
                    rngLista.Condicion0 = dr.GetString(dr.GetOrdinal("Condicion0"));
                    rngLista.Param01 = dr.GetString(dr.GetOrdinal("Param01"));
                    rngLista.Param02 = dr.GetString(dr.GetOrdinal("Param02"));
                    rngLista.Factor1 = dr.GetString(dr.GetOrdinal("Factor1"));
                    rngLista.Factor2 = dr.GetString(dr.GetOrdinal("Factor2"));
                    rngLista.Multiplicador1 = dr.GetString(dr.GetOrdinal("Multiplicador1"));
                    rngLista.Factor3 = dr.GetString(dr.GetOrdinal("Factor3"));
                    rngLista.Multiplicador2 = dr.GetString(dr.GetOrdinal("Multiplicador2"));

                    Lista.Add(rngLista);
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ActualizaConfiguraOCE(string Conexion, string sp, ConfiguraOCE configura, ref int i)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@IdConfiguraOCE",
                                        "@Id_Cd",
                                        "@IdCveConfiguraOCE",
                                        "@ConceptoConfiguraOCE",
                                        "@ValorConfiguraOCE",
                                        "@iActivo",
                                        "@Condicion0",
                                        "@Param01",
                                        "@Param02",
                                        "@Factor1",
                                        "@Factor2",
                                        "@Multiplicador1",
                                        "@Factor3",
                                        "@Multiplicador2",
                                        "@Callback"
                                        };

                object[] Valores = { configura.IdConfiguraOCE,
                                    configura.Id_Cd,
                                    int.Parse(configura.IdCveConfiguraOCE),
                                    configura.ConceptoConfiguraOCE,
                                    string.IsNullOrEmpty(configura.ValorConfiguraOCE) ? -1 : decimal.Parse(configura.ValorConfiguraOCE),
                                    1,
                                    string.IsNullOrEmpty(configura.Condicion0) ? "-1" : configura.Condicion0,
                                    string.IsNullOrEmpty(configura.Param01) ? "-1" : configura.Param01,
                                    string.IsNullOrEmpty(configura.Param02) ? "-1" : configura.Param02,
                                    string.IsNullOrEmpty(configura.Factor1) ? -1 : int.Parse(configura.Factor1),
                                    string.IsNullOrEmpty(configura.Factor2) ? -1 : int.Parse(configura.Factor2),
                                    string.IsNullOrEmpty(configura.Multiplicador1) ? -1 : decimal.Parse(configura.Multiplicador1),
                                    string.IsNullOrEmpty(configura.Factor3) ? -1 : int.Parse(configura.Factor3),
                                    string.IsNullOrEmpty(configura.Multiplicador2) ? -1 : decimal.Parse(configura.Multiplicador2),
                                    1
                                    };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(sp, ref dr, Parametros, Valores);

                ConfiguraOCE rngLista = default(ConfiguraOCE);
                while (dr.Read())
                {
                    i = dr.GetInt32(dr.GetOrdinal("OK"));
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
