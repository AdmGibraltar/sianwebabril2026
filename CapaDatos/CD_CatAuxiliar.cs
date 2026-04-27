using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_CatAuxiliar
    {
        public void ConsultaAuxiliar(CatAuxiliares Auxiliar, string Conexion, ref List<CatAuxiliares> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd" };
                object[] Valores = { Auxiliar.Id_Emp, Auxiliar.Id_Cd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatAuxiliar_Consulta", ref dr, Parametros, Valores);


                while (dr.Read())
                {
                    Auxiliar = new CatAuxiliares();
                    Auxiliar.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    Auxiliar.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    Auxiliar.Id_Aux = (int)dr.GetValue(dr.GetOrdinal("Id_Aux"));
                    Auxiliar.Aux_Nombre = (string)dr.GetValue(dr.GetOrdinal("Aux_Nombre"));
                    Auxiliar.Estatus = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("Aux_Activo")));
                    if (Convert.ToBoolean(Auxiliar.Estatus))
                    {
                        Auxiliar.EstatusStr = "Activa";
                    }
                    else
                    {
                        Auxiliar.EstatusStr = "Inactiva";
                    }
                    List.Add(Auxiliar);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertarAuxiliar(CatAuxiliares Auxiliar, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Aux",
                                        "@Aux_Nombre",
                                        "@Aux_Activo"
                                      };
                object[] Valores = {
                                        Auxiliar.Id_Emp,
                                        Auxiliar.Id_Cd,
                                        Auxiliar.Id_Aux,
                                        Auxiliar.Aux_Nombre,
                                        Auxiliar.Estatus
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatAuxiliar_Insertar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarAuxiliar(CatAuxiliares Auxiliar, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Aux",
                                        "@Aux_Nombre",
                                        "@Aux_Activo"
                                      };
                object[] Valores = {
                                        Auxiliar.Id_Emp,
                                        Auxiliar.Id_Cd,
                                        Auxiliar.Id_Aux,
                                        Auxiliar.Aux_Nombre,
                                        Auxiliar.Estatus
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatAuxiliar_Modificar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
