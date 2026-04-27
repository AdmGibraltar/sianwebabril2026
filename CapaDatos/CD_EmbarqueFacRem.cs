using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_EmbarqueFacRem
    {
        public void ConsultaProRemFacEntrega(int Id_Emp, int Id_Cd, string Conexion, EmbarqueFacRem remisionfiltro, ref List<EmbarqueFacRem> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Filtro_Nombre",
                                          "@Filtro_CteIni",
                                          "@Filtro_CteFin",
                                          "@Filtro_FecIni",
                                          "@Filtro_FecFin",
                                          "@TipoDoc",
                                          "@Id_Emb",
                                          "@Id_Doc"
                                      };
                object[] Valores = {
                                       Id_Emp,
                                       Id_Cd,
                                       remisionfiltro.Filtro_Nombre  == "" ? (object)null : remisionfiltro.Filtro_Nombre ,
                                       remisionfiltro.Filtro_Id_Cte  == "" ? (object)null : remisionfiltro.Filtro_Id_Cte ,
                                       remisionfiltro.Filtro_Id_Cte2  == "" ? (object)null : remisionfiltro.Filtro_Id_Cte2,
                                       remisionfiltro.Filtro_FecIni  == "" ? (object)null : remisionfiltro.Filtro_FecIni ,
                                       remisionfiltro.Filtro_FecFin  == "" ? (object)null : remisionfiltro.Filtro_FecFin,
                                       remisionfiltro.TipoDoc,
                                       remisionfiltro.Id_Emb,
                                       remisionfiltro.Id_Doc,
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spfacRemEntrega_Consulta", ref dr, Parametros, Valores);

                EmbarqueFacRem remisionesEntrega;
                while (dr.Read())
                {
                    remisionesEntrega = new EmbarqueFacRem();
                    remisionesEntrega.Id_Doc = (int)dr.GetValue(dr.GetOrdinal("Id_Doc"));
                    remisionesEntrega.Id_Emb = (int)dr.GetValue(dr.GetOrdinal("Id_Emb"));
                    
                    remisionesEntrega.TipoDoc = (string)dr.GetValue(dr.GetOrdinal("TipoDoc"));
                    remisionesEntrega.Estatus = (string)dr.GetValue(dr.GetOrdinal("Rem_Estatus"));
                    remisionesEntrega.Fecha = (DateTime)dr.GetValue(dr.GetOrdinal("Rem_Fecha"));
                    remisionesEntrega.Numero = (int)dr.GetValue(dr.GetOrdinal("Id_Doc"));
                    remisionesEntrega.Pedido = (int)dr.GetValue(dr.GetOrdinal("Id_Ped"));
                    remisionesEntrega.Cliente = (string)dr.GetValue(dr.GetOrdinal("Cte_NomComercial"));
                    remisionesEntrega.Num_Cliente = (int)dr.GetValue(dr.GetOrdinal("Id_Cte"));
                    List.Add(remisionesEntrega);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaEmbarqueFacRem(int Id_Emp, int Id_Cd, string Conexion, EmbarqueFacRem remisionfiltro, ref List<EmbarqueFacRem> List)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@Filtro_FecIni",
                                          "@Filtro_FecFin",
                                          "@Id_Emb"
                                      };
                object[] Valores = {
                                       Id_Emp,
                                       Id_Cd,
                                       remisionfiltro.Filtro_FecIni  == "" ? (object)null : remisionfiltro.Filtro_FecIni ,
                                       remisionfiltro.Filtro_FecFin  == "" ? (object)null : remisionfiltro.Filtro_FecFin,
                                       remisionfiltro.Id_Emb  == 0 ? (object)null : remisionfiltro.Id_Emb
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spEmbarqueEntrega_Consulta", ref dr, Parametros, Valores);

                EmbarqueFacRem remisionesEntrega;
                while (dr.Read())
                {
                    remisionesEntrega = new EmbarqueFacRem();
                    remisionesEntrega.Id_Emb = (int)dr.GetValue(dr.GetOrdinal("Id_Emb"));

                    remisionesEntrega.Estatus = (string)dr.GetValue(dr.GetOrdinal("Estatus"));
                    remisionesEntrega.Fecha = (DateTime)dr.GetValue(dr.GetOrdinal("Fecha"));
                    remisionesEntrega.Chofer = (string)dr.GetValue(dr.GetOrdinal("Chofer"));
                    remisionesEntrega.Camioneta = (string)dr.GetValue(dr.GetOrdinal("Camioneta"));
                    List.Add(remisionesEntrega);
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
