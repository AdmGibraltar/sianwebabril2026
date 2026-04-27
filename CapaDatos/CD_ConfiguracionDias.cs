using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_ConfiguracionDias
    {
        public void CatConfiguracionDias_Insertar(ConfiguracionDias cf, ref int Verificador, string Conexion)
        {
            try
            {

                CD_Datos cd_datos = new CD_Datos(Conexion);

                string[] Parametros = {
                                        "@Id_CD",
                                        "@Id_Rik",
                                        "@DF_FechaIni",
                                        "@DF_FechaFin",
                                        "@DF_RepNombre",
                                        "@DF_Nivel",
                                        "@DF_Tipo",
                                        "@DF_Comentario",
                                        "@Id_U"
                                      };
                object[] Valores = {
                                       cf.Id_Cd ,
                                       cf.Id_Rik,
                                       cf.DF_FechaIni,
                                       cf.DF_FechaFin,
                                       cf.DF_RepNombre,
                                       cf.DF_Nivel,
                                       cf.DF_Tipo,
                                       cf.DF_Comentario,
                                       cf.Id_U 
                                   };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCatDiasPermiso_Insertar", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void CatConfiguracionDias_Lista(ref List<ConfiguracionDias> List, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCatDiasPermiso_Lista", ref dr);

                ConfiguracionDias c;
                while (dr.Read())
                {
                    c = new ConfiguracionDias();
                    c.Id_DF = Convert.ToInt32(dr["Id_DF"]);
                    c.CD_Nombre = dr["CD_Nombre"].ToString();
                    c.DF_NivelStr = dr["DF_NivelStr"].ToString();
                    c.DF_RepNombre = dr["DF_RepNombre"].ToString();
                    c.DF_TipoStr = dr["DF_TipoStr"].ToString();
                    c.DF_FechaIni = Convert.ToDateTime(dr["DF_FechaIni"]);
                    c.DF_FechaFin = Convert.ToDateTime(dr["DF_FechaIni"]);
                    c.DF_Comentario = dr["DF_Comentario"].ToString();

                    List.Add(c);

                }
                dr.Close();

                cd_datos.LimpiarSqlcommand(ref sqlcmd);



            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void CatConfiguracionDias_Eliminar(int Id_DF, ref int Verificador, string Conexion)
        {
            try
            {

                CD_Datos cd_datos = new CD_Datos(Conexion);

                string[] Parametros = {
                                      "@Id_DF",
                                      
                                      };
                object[] Valores = {
                                      Id_DF
                                   };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCatDiasPermiso_Eliminar", ref Verificador, Parametros, Valores);

                cd_datos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void RepEntradasCRM_Consulta(int TipoCd, int Anio, int Mes, int Tipo, int Id_U, ref DataTable dt)
        {
            try
            {
                string conexion = "Data Source=40.84.229.61;Initial Catalog=sianwebcentralsep2017;User ID=sa;Password=4dmK3yQu1m";


                CD_Datos cd_datos = new CD_Datos(conexion);
                DataSet ds = new DataSet();

                string[] Parametros = {  "@TipoCD",
                                            "@CDI",
                                            "@Anio",
                                            "@Mes", 
                                            "@Tipo",
                                            "@Id_U"
                                          };
                object[] Valores = {
                                   TipoCd,
                                   -1,
                                   Anio ,
                                   Mes, 
                                   Tipo,
                                   Id_U};


                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spRepEntradasCrmPivot", ref ds, Parametros, Valores);
                dt = ds.Tables[0];

                cd_datos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
