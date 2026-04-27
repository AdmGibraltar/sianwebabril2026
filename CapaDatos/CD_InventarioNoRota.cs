using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_InventarioNoRota
    {
        //Metodos Rota y No Rota Actual
        public void ConsultaInventarioNoRotaDetalle(string Conexion, ref List<InventariosNoRota> List, RepExcesos Exceso)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {
                                         "@Id_Emp",
                                         "@Id_Cd",
                                         "@Id_Cd_Filtro",
                                         "@Id_Pvd",
                                         "@Dias",
                                         "@Id_Ptp",
                                         "@Salida",
                                         "@Id_U",
                                         "@Rota",
                                         "@Dias_Ver",
                                         "@Id_Pvd_Ver"
                                      };
                object[] Valores = {
                                        Exceso.Id_Emp,
                                        Exceso.Id_Cd,
                                        Exceso.Id_Cd == -1? (int?)null: Exceso.Id_Cd,
                                        Exceso.Proveedor == -1? (int?)null: Exceso.Proveedor,
                                        Exceso.Dias,
                                        Exceso.Tproducto == -1? (int?)null: Exceso.Tproducto,
                                        Exceso.Salida,
                                        Exceso.Id_U,
                                        Exceso.Rota == -1? (int?)null: Exceso.Rota,
                                        Exceso.DiasVer== -1? (int?)null: Exceso.DiasVer  ,
                                        Exceso.ProveedorVer
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("sp_Reporte_InvExceso", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    InventariosNoRota Inv = new InventariosNoRota();
                    Inv.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    Inv.Sucursal = dr.GetValue(dr.GetOrdinal("Sucursal")).ToString();
                    Inv.Proveedor = dr.GetValue(dr.GetOrdinal("Id_Pvd")).ToString();
                    Inv.Codigo = (Int64)dr.GetValue(dr.GetOrdinal("Id_Prd"));
                    Inv.Descripcion = dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                    Inv.Monto = (double)dr.GetValue(dr.GetOrdinal("Costo"));
                    Inv.Cantidad = (int)dr.GetValue(dr.GetOrdinal("Exceso"));
                    Inv.Disponible = (int)dr.GetValue(dr.GetOrdinal("Prd_Disponible"));
                    Inv.Estatus = dr.GetValue(dr.GetOrdinal("Estatus")).ToString();
                    Inv.Categoria = dr.GetValue(dr.GetOrdinal("Categoria")).ToString();
                    List.Add(Inv);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        //Metodos Rota y No Rota Cierre
        public void ConsultaInventarioNoRotaDetalleCierre(string Conexion, ref List<InventariosNoRota> ListNoRotaCierre, RepExcesos Exceso)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros = {

                                      };
                object[] Valores = {

                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRep_InvExceso_MesCerrado", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    InventariosNoRota Inv = new InventariosNoRota();
                    Inv.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                    Inv.Sucursal = dr.GetValue(dr.GetOrdinal("Sucursal")).ToString();
                    Inv.Proveedor = dr.GetValue(dr.GetOrdinal("Id_Pvd")).ToString();
                    Inv.Codigo = (Int64)dr.GetValue(dr.GetOrdinal("Id_Prd"));
                    Inv.Descripcion = dr.GetValue(dr.GetOrdinal("Prd_Descripcion")).ToString();
                    Inv.Monto = (double)dr.GetValue(dr.GetOrdinal("Costo"));
                    Inv.Cantidad = (int)dr.GetValue(dr.GetOrdinal("Exceso"));
                    Inv.Disponible = (int)dr.GetValue(dr.GetOrdinal("Prd_Disponible"));
                    Inv.Estatus = dr.GetValue(dr.GetOrdinal("Estatus")).ToString();
                    Inv.Categoria = dr.GetValue(dr.GetOrdinal("Categoria")).ToString();
                    Inv.Rota = (bool)dr.GetValue(dr.GetOrdinal("Rota"));
                    ListNoRotaCierre.Add(Inv);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LlenaCombo(int Id_Grupo, string Conexion, string sp, ref List<Comun> Lista)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Grupo" };

                object[] Valores = { Id_Grupo };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(sp, ref dr, Parametros, Valores);

                Comun Comun = default(Comun);
                while (dr.Read())
                {
                    Comun = new Comun();
                    Comun.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    Comun.Descripcion = dr.GetString(dr.GetOrdinal("Descripcion"));
                    Lista.Add(Comun);
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