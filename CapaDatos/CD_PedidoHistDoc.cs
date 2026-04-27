using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_PedidoHistDoc
    {
        public void InsertarPedidoHistorialDocumentoEnviado(PedidoHistDoc pedido, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
             
                string[] Parametros = { 
	                                    "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Fecha", 
                                        "@Semana",
	                                    "@DocExcel",
                                        "@Mes"
                                      };
                object[] Valores = { 
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.FechaRegistro,
                                        pedido.SemanaRegistro,
                                        pedido.DocExcel, 
                                        pedido.mes
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoHistDocEnv_Insertar", ref verificador, Parametros, Valores);

                
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
              
                throw ex;
            }
        }

        public void ConsultarPedidoHistorialDocumentoEnviado(PedidoHistDoc pedido, string Conexion, ref List<PedidoHistDoc> ListaPedido)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                SqlDataReader dr = null;
                PedidoHistDoc RegistroPedido;

                string[] Parametros = { 
	                                    "@Id_Emp", 
	                                    "@Id_Cd",  
                                        "@Semana"
	                                  
                                      };
                object[] Valores = { 
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,    
                                        pedido.SemanaRegistro                                      
                                   };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPedidoHistDocEnv_Consultar", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    RegistroPedido = new PedidoHistDoc();

                    RegistroPedido.FechaInicial = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("CAL_FECHAINI")));
                    RegistroPedido.FechaFinal = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("CAL_FECHAFIN")));
                    RegistroPedido.cantidadRegistros = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("cantidadRegistros")));
                    RegistroPedido.mes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("cal_Mes")));

                    ListaPedido.Add(RegistroPedido);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }

        }

        public void Insertar_RegistroPedidoNuExp(PedidoHistDoc pedido, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {

                string[] Parametros = { 
	                                    "@Id_Emp", 
	                                    "@Id_Cd", 
	                                    "@Id_U" 
                                      };
                object[] Valores = { 
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,
                                        pedido.Id_U
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapRegistroPedido_insertar", ref verificador, Parametros, Valores);


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Consulta_RegistroPedidoNuExp(PedidoHistDoc pedido, string Conexion, ref List<PedidoHistDoc> ListaPedido)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
            try
            {
                SqlDataReader dr = null;
                PedidoHistDoc RegistroPedido;

                string[] Parametros = { 
	                                    "@Id_Emp", 
	                                    "@Id_Cd",  
                                        "@Id_U"
	                                  
                                      };
                object[] Valores = { 
                                        pedido.Id_Emp,
                                        pedido.Id_Cd,    
                                        pedido.Id_U                                      
                                   };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapRegistroPedido_consulta", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    RegistroPedido = new PedidoHistDoc();
                    RegistroPedido.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                    RegistroPedido.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    RegistroPedido.Id_U = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));
                    RegistroPedido.CantVtaNuExp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CantVtaNuExp"))); 
                    ListaPedido.Add(RegistroPedido);
                }

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