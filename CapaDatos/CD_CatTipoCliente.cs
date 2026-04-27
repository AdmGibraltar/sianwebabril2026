using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;


namespace CapaDatos
{
    public class CD_CatTipoCliente
    {
        // NOV19-2019 RFH

        public List<CapaEntidad.TipoCliente> SelAll(int Id_Emp, string Conexion, ref int verificador)
        {
            List<CapaEntidad.TipoCliente> lst = new List<CapaEntidad.TipoCliente>();
            
            try
            {
                SqlDataReader dr = null;

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { "@Id1", "@Id2" };
                object[] Valores = { 1, Id_Emp };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTipoCliente_Combo", ref dr, Parametros, Valores);
                
                while (dr.Read())
                {
                    CapaEntidad.TipoCliente obj = new TipoCliente();
                    obj.Id_TCte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                    obj.TCte_Descripcion = dr.GetValue(dr.GetOrdinal("Descripcion")).ToString();
                    lst.Add(obj);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {                
                lst = null;
            }            
            return lst;
        }


        public void ModificarTipoCliente(TipoCliente tipoCliente, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_TCte", 
                                        "@TCte_Descripcion", 
	                                    "@TCte_ConCuentaCorporativa", 
                                        "@TCte_Autorizadores"
                                      };
                object[] Valores = { 
                                        tipoCliente.Id_TCte
                                        ,tipoCliente.TCte_Descripcion
                                        ,tipoCliente.TCte_ConCuentaCorporativa
                                        ,tipoCliente.TCte_Autorizadores
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTipoCliente_Modificar", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarAutorizadores(TipoCliente tipoCliente, string Conexion, ref int verificador)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_TCte", 
                                        "@TCte_Autorizadores"
                                      };
                object[] Valores = { 
                                        tipoCliente.Id_TCte
                                        ,tipoCliente.TCte_Autorizadores
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTipoCliente_ModificarAutorizadores", ref verificador, Parametros, Valores);
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaAutorizadores(TipoCliente tipoCliente, string Conexion, ref int verificador)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = { 
	                                    "@Id_TCte", 
                                      };
                object[] Valores = { 
                                        tipoCliente.Id_TCte
                                   };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatTipoCliente_ConsultaAutorizadores", ref dr, Parametros, Valores);

                tipoCliente.TCte_Autorizadores = "";

                while (dr.Read())
                {
                    tipoCliente.TCte_Autorizadores += "," + dr["Id_U"].ToString();
                }

                tipoCliente.TCte_Autorizadores += ",";

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
