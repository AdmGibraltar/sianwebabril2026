using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;

using System.Data;

namespace CapaDatos
{
    public class CD_ConfiguracionDiasConvenio
    {


        public void ConsultarConfiguracion_DiasAlerta(ref List<ConfiguracionDiasConvenio> Listadiasalerta, int Id_Cd, int Id_Emp, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { };

                object[] Valores = { };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPrecioConv_DiasAlerta_Consultar", ref dr, Parametros, Valores);
                ConfiguracionDiasConvenio diasalerta = new ConfiguracionDiasConvenio();
                while (dr.Read())
                {
                    diasalerta = new ConfiguracionDiasConvenio();
                    diasalerta.Id_Alerta = dr.GetInt32(dr.GetOrdinal("Id_Alerta"));
                    diasalerta.Dia_NomAlerta = dr["Dia_NomAlerta"].ToString();
                    diasalerta.Dia_DiasAlerta = dr.GetInt32(dr.GetOrdinal("Dia_DiasAlerta"));
                    Listadiasalerta.Add(diasalerta);
                }

                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarConfiguracion_DiasAlerta(ref ConfiguracionDiasConvenio diasalerta, Sesion sesion, int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = {
			         "@Id_Alerta"
                    ,"@Dia_NomAlerta"
                    ,"@Dia_DiasAlerta"                    
                };
                object[] Valores = {
                     diasalerta.Id_Alerta
                    ,diasalerta.Dia_NomAlerta
                    ,diasalerta.Dia_DiasAlerta
		        };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPrecioConv_DiasAlerta_ModificarCobranza", ref verificador, Parametros, Valores);

                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void AgregarConfiguracion_DiasAlerta(List<ConfiguracionDiasConvenio> centroDistribucion, int Id_Cd, Sesion sesion)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
            try
            {
                CapaDatos.StartTrans();
                EliminarConfiguracion_DiasAlerta(sesion.Id_Emp, Id_Cd, sesion.Emp_Cnx);
                string[] Parametros = {
			        "@Dia_NomAlerta"
                    ,"@Dia_DiasAlerta" 
                };
                object[] Valores;
                SqlCommand sqlcmd = default(SqlCommand);
                int verificador = 0;

                for (int x = 0; x < centroDistribucion.Count; x++)
                {
                    Valores = new object[] {
                                            centroDistribucion[x].Dia_NomAlerta
                                            ,centroDistribucion[x].Dia_DiasAlerta  
		                                    };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spProPrecioConv_DiasAlerta_Agregar", ref verificador, Parametros, Valores);
                }
                CapaDatos.CommitTrans();
                // CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void EliminarConfiguracion_DiasAlerta(int Id_Emp, int Id_Cd, string Emp_Cnx)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Emp_Cnx);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = { };
                object[] Valores = { };
                int verificador = 0;
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPrecioConv_DiasAlerta_Eliminar", ref verificador, Parametros, Valores);

                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        //categoria 
        public void ConsultarConfiguracion_Categoria(ref List<CatConvCategoria> Listadiasalerta, int Id_Cd, int Id_Emp, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { };

                object[] Valores = { };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatConvCategoria_Consultar", ref dr, Parametros, Valores);
                CatConvCategoria diasalerta = new CatConvCategoria();
                while (dr.Read())
                {
                    diasalerta = new CatConvCategoria();
                    diasalerta.Id_Cat = (int)dr.GetValue(dr.GetOrdinal("Id_Cat"));
                    diasalerta.Cat_DescCorta = (string)dr.GetValue(dr.GetOrdinal("Cat_DescCorta"));
                    diasalerta.Cat_Nombre = (string)dr.GetValue(dr.GetOrdinal("Cat_Nombre"));

                    diasalerta.Cat_Consecutivo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cat_Consecutivo")));
                    diasalerta.Cat_CapturaUsuario = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cat_Consecutivo")));
                    diasalerta.Cat_Activo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cat_Activo")));

                    diasalerta.Id_UCreo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_UCreo")));
                    diasalerta.Cat_FechaCreo = (DateTime)dr.GetValue(dr.GetOrdinal("Cat_fechaCreo"));

                    Listadiasalerta.Add(diasalerta);
                }

                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarConfiguracion_Categoria(ref CatConvCategoria categoria, Sesion sesion, int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = {
			         "@Id_Cat"
                    ,"@Cat_Nombre"
                    ,"@Cat_DescCorta"  
                    ,"@Cat_Consecutivo"  
                    ,"@Cat_CapturaUsuario"  
                    ,"@Cat_Activo"  
                    ,"@Id_UCreo"
                    ,"@Cat_FechaCreo"
                };
                object[] Valores = {
                     categoria.Id_Cat
                    ,categoria.Cat_Nombre
                    ,categoria.Cat_DescCorta
                    ,categoria.Cat_Consecutivo
                    ,categoria.Cat_CapturaUsuario
                    ,categoria.Cat_Activo
                    ,categoria.Id_UCreo
                    ,categoria.Cat_FechaCreo

		        };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatConvCategoria_Modificar", ref verificador, Parametros, Valores);

                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }


        public void ModificaMasivoConfiguracion_Categoria(List<CatConvCategoria> centroDistribucion, int Id_Cd, Sesion sesion)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
            try
            {
                CapaDatos.StartTrans();
                EliminarConfiguracion_DiasAlerta(sesion.Id_Emp, Id_Cd, sesion.Emp_Cnx);
                string[] Parametros = {
			         "@Id_Cat"
                    ,"@Cat_Nombre"
                    ,"@Cat_DescCorta"  
                    ,"@Cat_Consecutivo"  
                    ,"@Cat_CapturaUsuario"  
                    ,"@Cat_Activo"  
                    ,"@Id_UCreo"
                    ,"@Cat_FechaCreo"
                };
                object[] Valores;
                SqlCommand sqlcmd = default(SqlCommand);
                int verificador = 0;

                for (int x = 0; x < centroDistribucion.Count; x++)
                {
                    Valores = new object[] {
                                             centroDistribucion[x].Id_Cat
                                            ,centroDistribucion[x].Cat_Nombre
                                            ,centroDistribucion[x].Cat_DescCorta
                                            ,centroDistribucion[x].Cat_Consecutivo
                                            ,centroDistribucion[x].Cat_CapturaUsuario
                                            ,centroDistribucion[x].Cat_Activo
                                            ,centroDistribucion[x].Id_UCreo
                                            ,centroDistribucion[x].Cat_FechaCreo
 
		                                    };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spCatConvCategoria_Modificar", ref verificador, Parametros, Valores);
                }
                CapaDatos.CommitTrans();
                // CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }


        public void AgregarConfiguracion_Categoria(CatConvCategoria categoria, int Id_Cd, Sesion sesion)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
            try
            {
                CapaDatos.StartTrans();
                //EliminarConfiguracion_DiasAlerta(sesion.Id_Emp, Id_Cd, sesion.Emp_Cnx);
                string[] Parametros = {
			         "@Cat_Nombre"
                    ,"@Cat_DescCorta"  
                    ,"@Cat_Consecutivo"  
                    ,"@Cat_CapturaUsuario"  
                    ,"@Cat_Activo"  
                    ,"@Id_UCreo"
                     
                };
                object[] Valores;
                SqlCommand sqlcmd = default(SqlCommand);
                int verificador = 0;

                Valores = new object[] {
                                             categoria.Cat_Nombre
                                            ,categoria.Cat_DescCorta
                                            ,categoria.Cat_Consecutivo
                                            ,categoria.Cat_CapturaUsuario
                                            ,categoria.Cat_Activo
                                            ,categoria.Id_UCreo
                       
		                                    };
                sqlcmd = CapaDatos.GenerarSqlCommand("spCatConvCategoria_Agregar", ref verificador, Parametros, Valores);

                CapaDatos.CommitTrans();
                // CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }


        public void EliminarConfiguracion_Categoria(int Id_Emp, int Id_Cd, int Id_Cat, int id_UCreo, string Emp_Cnx)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Emp_Cnx);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = { "@Id_Cat", "@Id_UCreo" };
                object[] Valores = { Id_Cat, id_UCreo };
                int verificador = 0;
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatConvCategoria_Eliminar", ref verificador, Parametros, Valores);

                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        ///
        ///  Administradores
        ///
        public void ConsultarConfiguracion_Administrador(ref List<ProPrecioConv_Usuarios> listaadministradores, int Id_Cd, int Id_Emp, int tipo_usuario, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Tipo_usuario" };

                object[] Valores = { Id_Emp, tipo_usuario };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPrecioConv_Usuarios_Consultar", ref dr, Parametros, Valores);
                ProPrecioConv_Usuarios admins = new ProPrecioConv_Usuarios();
                while (dr.Read())
                {
                    admins = new ProPrecioConv_Usuarios();
                    //diasalerta.Id_Cd = dr.GetInt32(dr.GetOrdinal("CDI"));
                    admins.Id_Emp = dr.GetInt32(dr.GetOrdinal("Id_Emp"));
                    admins.Id_Conusu = dr.GetInt32(dr.GetOrdinal("Id_Conusu"));
                    admins.Id_Usu = dr.GetInt32(dr.GetOrdinal("Id_Usu"));
                    admins.Tipo_usuario = dr.GetInt32(dr.GetOrdinal("Tipo_usuario"));
                    admins.Usu_correo = dr["Usu_correo"].ToString();
                    admins.U_Nombre = dr["U_Nombre"].ToString();


                    listaadministradores.Add(admins);
                }

                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AgregarConfiguracion_Administrador(List<ProPrecioConv_Usuarios> centroDistribucion, int Id_Cd, int tipo_usuario, Sesion sesion)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);
            try
            {
                CapaDatos.StartTrans();
                //solo elimina cuando es del tipo usuario 1 
                if (tipo_usuario == 1)
                {
                    EliminarConfiguracion_Administrador(sesion.Id_Emp, Id_Cd, tipo_usuario, sesion.Emp_Cnx);
                }
                string[] Parametros = {
			        "@Id_Emp"
                    ,"@Id_Usu" 
                    ,"@Tipo_usuario"
                };
                object[] Valores;
                SqlCommand sqlcmd = default(SqlCommand);
                int verificador = 0;

                for (int x = 0; x < centroDistribucion.Count; x++)
                {
                    Valores = new object[] {
                                            centroDistribucion[x].Id_Emp
                                            ,centroDistribucion[x].Id_Usu  
                                             ,tipo_usuario 
		                                    };
                    sqlcmd = CapaDatos.GenerarSqlCommand("spProPrecioConvUsuarios_Agregar", ref verificador, Parametros, Valores);
                }
                CapaDatos.CommitTrans();
                // CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }

        public void EliminarConfiguracion_Administrador(int Id_Emp, int Id_Cd, int tipo_usuario, string Emp_Cnx)
        {
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Emp_Cnx);
            try
            {
                CapaDatos.StartTrans();
                string[] Parametros = { "@Id_Emp", "@Tipo_Usuario" };

                object[] Valores = { Id_Emp, tipo_usuario };
                int verificador = 0;
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spProPrecioConv_Usuarios_Eliminar", ref verificador, Parametros, Valores);

                CapaDatos.CommitTrans();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw ex;
            }
        }


        ///
        ///  Correos Anexos
        ///
        public void ConsultarCorreosAnexos(ref List<ProPrecioConv_Usuarios> listaadministradores, int Id_Cd, int Id_Emp, int id_pc, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_PC" };

                object[] Valores = { Id_Emp, id_pc };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("ProPrecioConv_MensajeUsuarios_Consultar", ref dr, Parametros, Valores);
                ProPrecioConv_Usuarios admins = new ProPrecioConv_Usuarios();
                while (dr.Read())
                {
                    admins = new ProPrecioConv_Usuarios();
                    //diasalerta.Id_Cd = dr.GetInt32(dr.GetOrdinal("CDI"));
                    admins.Id_Emp = dr.GetInt32(dr.GetOrdinal("Id_Emp"));
                    admins.Id_Conusu = dr.GetInt32(dr.GetOrdinal("Id_Conusu"));
                    admins.Id_Usu = dr.GetInt32(dr.GetOrdinal("Id_Usu"));
                    admins.Tipo_usuario = dr.GetInt32(dr.GetOrdinal("Tipo_usuario"));
                    admins.Usu_correo = dr["Usu_correo"].ToString();
                    admins.U_Nombre = dr["U_Nombre"].ToString();


                    listaadministradores.Add(admins);
                }

                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///
        ///  Correos Adicionales
        ///
        public void ConsultarCorreosAdicionales(ref List<ProPrecioConv_Usuarios> listaadministradores, int Id_Cd, int Id_Emp, int id_pc, int Tipo_Habilitar, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_PC", "@Id_CD", "@Tipo_Habilitar" };

                object[] Valores = { Id_Emp, id_pc, Id_Cd, Tipo_Habilitar };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("ProPrecioConv_CorreosGerenteJO_Consultar", ref dr, Parametros, Valores);
                ProPrecioConv_Usuarios admins = new ProPrecioConv_Usuarios();
                while (dr.Read())
                {
                    admins = new ProPrecioConv_Usuarios();
                    //diasalerta.Id_Cd = dr.GetInt32(dr.GetOrdinal("CDI"));
                    admins.Id_Emp = dr.GetInt32(dr.GetOrdinal("Id_Emp"));
                    admins.Id_Conusu = dr.GetInt32(dr.GetOrdinal("Id_Conusu"));
                    admins.Id_Usu = dr.GetInt32(dr.GetOrdinal("Id_Usu"));
                    admins.Tipo_usuario = dr.GetInt32(dr.GetOrdinal("Tipo_usuario"));
                    admins.Usu_correo = dr["Usu_correo"].ToString();
                    admins.U_Nombre = dr["U_Nombre"].ToString();



                    listaadministradores.Add(admins);
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
