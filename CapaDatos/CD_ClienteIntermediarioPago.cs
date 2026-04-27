using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_ClienteIntermediarioPago
    {
        /// <summary>
        /// Función que consulta la información en la tabla CatIntermediarioFinanciero
        /// </summary>
        /// <param name="banco"></param>
        /// <param name="Conexion"></param>
        /// <param name="List"></param>
        public void ConsultaClienteIntermediarioFinanciero(ClienteIntermediarioPago inter, string Conexion, ref System.Data.DataTable dt)
        {
            try
            {
                int estatus = 0;
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte" };
                object[] Valores = { inter.Id_Emp, inter.Id_Cd, inter.Id_Cte };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatClienteIntermediarioPago_Consulta", ref dr, Parametros, Valores);

        
                while (dr.Read())
                {

                    estatus = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IntPag_Estatus")));
    
                        dt.Rows.Add(new object[] {
                        dr.GetValue(dr.GetOrdinal("Id_IntPag")),
                        dr.GetValue(dr.GetOrdinal("IntPag_Nombre")),
                        dr.GetValue(dr.GetOrdinal("IntPag_RFC")),
                        dr.GetValue(dr.GetOrdinal("IntPag_Razon")),
                        dr.GetValue(dr.GetOrdinal("IntPag_Correo")), 
                        estatus == 0? false : true, 
                        dr.GetValue(dr.GetOrdinal("IntPag_Calle")),             
                        dr.GetValue(dr.GetOrdinal("IntPag_Numero")),
                        dr.GetValue(dr.GetOrdinal("IntPag_CP")),
                        dr.GetValue(dr.GetOrdinal("IntPag_Colonia")),
                        dr.GetValue(dr.GetOrdinal("IntPag_Municipio")),
                        dr.GetValue(dr.GetOrdinal("IntPag_Estado")),
                       
                    });
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Función que consulta la información del intermediario
        /// </summary>
        /// <param name="banco"></param>
        /// <param name="Conexion"></param>
        /// <param name="List"></param>
        public void ConsultaIntermediarioFinanciero(ref ClienteIntermediarioPago inter, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@Id_IntPag" };
                object[] Valores = { inter.Id_Emp, inter.Id_Cd, inter.Id_Cte, inter.Id_IntPag };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spClienteIntermediarioPago _Consulta", ref dr, Parametros, Valores);

                ClienteIntermediarioPago intermediario;
               if (dr.Read() != null)
                {
                    intermediario = new ClienteIntermediarioPago();

                    inter.IntPag_Nombre = (string)dr.GetValue(dr.GetOrdinal("IntPag_Nombre"));
                    inter.IntPag_RFC = (string)dr.GetValue(dr.GetOrdinal("IntPag_RFC"));
                    inter.IntPag_Razon = dr.GetValue(dr.GetOrdinal("IntPag_Razon")).ToString();
                    inter.IntPag_Correo = (string)dr.GetValue(dr.GetOrdinal("IntPag_Correo"));
                    inter.IntPag_Calle = Convert.ToString(dr.GetValue(dr.GetOrdinal("IntPag_Calle")));

                    inter.IntPag_Numero = (string)dr.GetValue(dr.GetOrdinal("IntPag_Numero"));
                    inter.IntPag_CP = (string)dr.GetValue(dr.GetOrdinal("IntPag_CP"));
                    inter.IntPag_Colonia = dr.GetValue(dr.GetOrdinal("IntPag_Colonia")).ToString();
                    inter.IntPag_Municipio = (string)dr.GetValue(dr.GetOrdinal("IntPag_Municipio"));
                    inter.IntPag_Estado = Convert.ToString(dr.GetValue(dr.GetOrdinal("IntPag_Estado"))); 
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Función que guarda o actualiza la información.
        /// </summary>
        /// <param name="inter"></param>
        /// <param name="dt"></param>
        /// <param name="Conexion"></param>
        public void InsertarClienteIntermediarioFinanciero(ClienteIntermediarioPago inter, System.Data.DataTable dt, string Conexion)
        {

            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            int verificador = 0;
            try
            {

                CapaDatos.StartTrans();
                string[] Parametros = { 
                        "@Id_Emp" , 
                        "@Id_Cd", 
                        "@Id_Cte", 
                        "@Id_IntPag", 
                        "@IntPag_Nombre", 
                        "@IntPag_RFC", 
                        "@IntPag_Razon", 
                        "@IntPag_Correo", 
                        "@IntPag_Estatus", 
                        "@IntPag_Calle", 
                        "@IntPag_Numero", 
                        "@IntPag_CP", 
                        "@IntPag_Colonia", 
                        "@IntPag_Municipio", 
                        "@IntPag_Estado"  
                                      };

                object[] Valores = null;
                SqlCommand sqlcmd = null;

                if (dt.Rows.Count > 0)
                {
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        Valores = new object[] { 
                                inter.Id_Emp,
                                inter.Id_Cd,
                                inter.Id_Cte,  
                                dt.Rows[x]["Id_IntPag"],
                                dt.Rows[x]["IntPag_Nombre"],
                                dt.Rows[x]["IntPag_RFC"],
                                dt.Rows[x]["IntPag_Razon"],
                                dt.Rows[x]["IntPag_Correo"],
                                dt.Rows[x]["IntPag_Estatus"],
                                dt.Rows[x]["IntPag_Calle"],
                                dt.Rows[x]["IntPag_Numero"],
                                dt.Rows[x]["IntPag_CP"],
                                dt.Rows[x]["IntPag_Colonia"],
                                dt.Rows[x]["IntPag_Municipio"],
                                dt.Rows[x]["IntPag_Estado"],
                                dt.Rows[x]["IntPag_Estatus"]                                       
                                   };
                        sqlcmd = CapaDatos.GenerarSqlCommand("Spcatclienteintermediariopago_insertar", ref verificador, Parametros, Valores);
                    }
                }               
                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }

        }
    }
}
