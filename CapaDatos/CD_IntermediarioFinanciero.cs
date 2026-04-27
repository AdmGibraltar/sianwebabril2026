using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_IntermediarioFinanciero
    {
        /// <summary>
        /// Función que consulta la información en la tabla CatIntermediarioFinanciero
        /// </summary>
        /// <param name="banco"></param>
        /// <param name="Conexion"></param>
        /// <param name="List"></param>
        public void ConsultaIntermediarioFinanciero(IntermediarioFinanciero inter, string Conexion, ref List<IntermediarioFinanciero> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd" };
                object[] Valores = { inter.Id_Emp, inter.Id_Cd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatIntermediarioFinanciero_Consulta", ref dr, Parametros, Valores);

                IntermediarioFinanciero intermediario;
                while (dr.Read())
                {
                    intermediario = new IntermediarioFinanciero();
                    intermediario.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    intermediario.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    intermediario.Id_IF = (int)dr.GetValue(dr.GetOrdinal("Id_IF"));
                    intermediario.RFC = (string)dr.GetValue(dr.GetOrdinal("RFC"));
                    intermediario.Banco = (string)dr.GetValue(dr.GetOrdinal("Banco"));
                    intermediario.RazonSocial = dr.GetValue(dr.GetOrdinal("RazonSocial")).ToString();
                    intermediario.Correo = (string)dr.GetValue(dr.GetOrdinal("Correo"));
                    intermediario.Activo = Convert.ToString(dr.GetValue(dr.GetOrdinal("Activo")));

                    if (Convert.ToBoolean(intermediario.Activo))
                    {
                        intermediario.EstatusStr = "Activo";
                    }
                    else
                    {
                        intermediario.EstatusStr = "Inactivo";
                    }
                    List.Add(intermediario);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Función que consulta la información en la tabla CatIntermediarioFinanciero
        /// </summary>
        /// <param name="banco"></param>
        /// <param name="Conexion"></param>
        /// <param name="List"></param>
        public void  ConsultaIntermediarioBanco(IntermediarioFinanciero inter, string Conexion, ref List<IntermediarioFinanciero> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_If" };
                object[] Valores = { inter.Id_Emp, inter.Id_Cd, inter.Id_IF };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatIntermediarioFinanciero_ConsultaIntermediario", ref dr, Parametros, Valores);

                IntermediarioFinanciero intermediario;
                while (dr.Read())
                {
                    intermediario = new IntermediarioFinanciero();
                    intermediario.Id_Emp = (int)dr.GetValue(dr.GetOrdinal("Id_Emp"));
                    intermediario.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    intermediario.Id_IF = (int)dr.GetValue(dr.GetOrdinal("Id_IF"));
                    intermediario.RFC = (string)dr.GetValue(dr.GetOrdinal("RFC"));
                    intermediario.Banco = (string)dr.GetValue(dr.GetOrdinal("Banco"));
                    intermediario.RazonSocial = dr.GetValue(dr.GetOrdinal("RazonSocial")).ToString();
                    intermediario.Correo = (string)dr.GetValue(dr.GetOrdinal("Correo"));
                    intermediario.Activo = Convert.ToString(dr.GetValue(dr.GetOrdinal("Activo")));

                    if (Convert.ToBoolean(intermediario.Activo))
                    {
                        intermediario.EstatusStr = "Activo";
                    }
                    else
                    {
                        intermediario.EstatusStr = "Inactivo";
                    }
                    List.Add(intermediario);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Función que inserta la información en la tabla de CatIntermediarioFinanciero
        /// </summary>
        /// <param name="inter"></param>
        /// <param name="Conexion"></param>
        /// <param name="verificador"></param>
        public void InsertarIntermediarioFinanciero(IntermediarioFinanciero inter, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Id_IF", 
	                                    "@Banco", 
	                                    "@RFC", 
	                                    "@RazonSocial", 
	                                    "@Correo", 
	                                    "@Activo" 
                                      };
                object[] Valores = { 
                                        inter.Id_Emp,
                                        inter.Id_Cd,
                                        inter.Id_IF,
                                        inter.Banco,
                                        inter.RFC,
                                        inter.RazonSocial,
                                        inter.Correo,
                                        Convert.ToBoolean(inter.Activo)
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatIntermediarioFinanciero_Insertar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Función que actualiza la información en la tabla CatIntermediarioFinanciero
        /// </summary>
        /// <param name="inter"></param>
        /// <param name="Conexion"></param>
        /// <param name="verificador"></param>
        public void ModificarIntermediarioFinanciero(IntermediarioFinanciero inter, string Conexion, ref int verificador)
        {
            try
            {

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
                                        "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Id_IF", 
	                                    "@Banco", 
	                                    "@RFC", 
	                                    "@RazonSocial", 
	                                    "@Correo", 
	                                    "@Activo" 
                                      };
                object[] Valores = { 
                                        inter.Id_Emp,
                                        inter.Id_Cd,
                                        inter.Id_IF,
                                        inter.Banco,
                                        inter.RFC,
                                        inter.RazonSocial,
                                        inter.Correo,
                                        inter.Activo
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatIntermediarioFinanciero__Modificar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
