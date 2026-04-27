using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Rep_VenEstadisticaVentas
    {
        #region Parametros
        string[] Parametros ={
                              "@Id_Emp"
                             ,"@Id_Cd"
                             ,"@Fecha"
                             ,"@Territorio"
                             ,"@Cliente"
                             ,"@Producto"
                             ,"@Tipo"
                             ,"@NivelCliente"
                             ,"@NivelProducto"
                            };

        #endregion

        public void ConsultaVentaSem_Territorio(VentaSemanal semanal, string Conexion, ref List<VentaSemanal> List, int Id_Emp, int Id_Cd,
                                                string Fecha, string Territorio, string Cliente, string Producto, string Categoria,
                                                int Tipo, int NivelCliente, int NivelProducto, int Mov_80)
        {
            try
            {
                SqlDataReader dr = null;
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros ={
                              "@Id_Emp"
                             ,"@Id_Cd"
                             ,"@Fecha"
                             ,"@Territorio"
                             ,"@Cliente"
                             ,"@Producto"
                             ,"@Categoria"
                             ,"@Tipo"
                             ,"@NivelCliente"
                             ,"@NivelProducto"
                             ,"@Mov_80"
                            };
                object[] Valores ={
                                   Id_Emp
                                  ,Id_Cd
                                  ,Fecha
                                  ,Territorio
                                  ,Cliente
                                  ,Producto
                                  ,Categoria
                                  ,Tipo
                                  ,NivelCliente
                                  ,NivelProducto
                                  ,Mov_80
                                  };

                string strQry = "exec spRepVentasSemanal_v2 " + String.Join(", ", Valores.ToArray());

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRepVentasSemanal_v2", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    semanal = new VentaSemanal();
                    semanal.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    semanal.Nom_Ter = dr.GetValue(dr.GetOrdinal("Nom_Ter")).ToString();
                    semanal.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    semanal.Nom_Cte = dr.GetValue(dr.GetOrdinal("Nom_Cte")).ToString();
                    semanal.Id_prd = (long)dr["Id_Prd"];
                    semanal.Nom_Prd = dr.GetValue(dr.GetOrdinal("Nom_Prd")).ToString();
                    if (Tipo == 0)
                    {
                        semanal.Unidades = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Unidades")));
                        semanal.Importe = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("Importe")));
                    }
                    if (Tipo == 1)
                        semanal.Importe = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("Importe")));
                    if (Tipo == 2)
                        semanal.Unidades = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Unidades")));

                    semanal.Semana = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Sem")));
                    semanal.Anio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Anio")));
                    semanal.Mes = dr.GetValue(dr.GetOrdinal("Mes")).ToString();
                    List.Add(semanal);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaVentaAnual(string Conexion, int Id_Emp, int Id_Cd, VenEstadisticaVentas entFiltroVentas, ref DataTable dataTable, ref List<VenEstadisticaVentas> lstEstVenta)
        {
            try
            {
                VenEstadisticaVentas entEstVenta = new VenEstadisticaVentas();
                SqlDataReader dr = null;
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros ={
                              "@Id_Emp"
                             ,"@Id_Cd"
                             ,"@Anio"
                             ,"@Territorio"
                             ,"@Cliente"
                             ,"@Producto"
                             ,"@Tipo"
                             ,"@NivelCliente"
                             ,"@NivelProducto"
                             ,"@Reporte"
                             ,"@Id_U"
                             ,"@CategoriaProducto"
                             ,"@FechaInicio", "@FechaFin"
                            };
                object[] Valores ={
                                   Id_Emp
                                  ,Id_Cd
                                  ,entFiltroVentas.Anio
                                  ,entFiltroVentas.Territorio
                                  ,entFiltroVentas.Cliente
                                  ,entFiltroVentas.Producto
                                  ,entFiltroVentas.Mostrar
                                  ,entFiltroVentas.Nivel
                                  ,entFiltroVentas.Nivel2
                                  ,entFiltroVentas.Reporte
                                  ,entFiltroVentas.id_usu
                                  ,entFiltroVentas.categoria
                                  ,entFiltroVentas.FechaInicio.HasValue ? (object)entFiltroVentas.FechaInicio.Value : DBNull.Value
                                  ,entFiltroVentas.FechaFin.HasValue ? (object)entFiltroVentas.FechaFin.Value : DBNull.Value
                                  };

                string strQry = "exec spVentasAnuales_v2 " + String.Join(", ", Valores.ToArray());

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spVentasAnuales_v2", ref dr, Parametros, Valores);

                //dataTable.Load(dr);
                while (dr.Read())
                {
                    entEstVenta = new VenEstadisticaVentas();


                    switch (entFiltroVentas.Reporte)
                    {
                        case 1:
                        case 2:
                            entEstVenta.id_ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                            entEstVenta.nombre_terr = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                            break;
                        case 3:
                        case 4:
                            entEstVenta.id_ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                            entEstVenta.nombre_terr = dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();
                            entEstVenta.id_cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                            entEstVenta.Cliente = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                            break;
                        case 5:
                        case 6:
                            entEstVenta.id_ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                            entEstVenta.nombre_terr = dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();
                            entEstVenta.id_prd = (long)dr["Id"];
                            entEstVenta.Producto = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                            break;
                        case 7:
                        case 8:
                            entEstVenta.id_ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                            entEstVenta.nombre_terr = dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();
                            entEstVenta.id_cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                            entEstVenta.Cliente = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                            entEstVenta.id_prd = (long)dr["Id"];
                            entEstVenta.Producto = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                            break;
                        case 9:
                        case 10:
                            entEstVenta.id_cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                            entEstVenta.Cliente = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                            break;
                        case 11:
                        case 12:
                            entEstVenta.id_cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                            entEstVenta.Cliente = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                            entEstVenta.id_prd = (long)dr["Id"];
                            entEstVenta.Producto = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                            break;
                        case 13:
                        case 14:
                            entEstVenta.id_prd = (long)dr["Id"];
                            entEstVenta.Producto = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                            break;
                        case 16:
                        case 17:
                            entEstVenta.id_rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                            entEstVenta.nombre_rik = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                            break;
                        case 18:
                            entEstVenta.id_rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                            entEstVenta.nombre_rik = dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();
                            entEstVenta.id_cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                            entEstVenta.Cliente = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                            break;
                        case 19:
                            entEstVenta.id_rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                            entEstVenta.nombre_rik = dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();
                            entEstVenta.id_cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                            entEstVenta.Cliente = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                            break;
                        case 20:
                        case 21:
                            entEstVenta.id_rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                            entEstVenta.nombre_rik = dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();
                            entEstVenta.id_cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                            entEstVenta.Cliente = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                            entEstVenta.id_prd = (long)dr["Id"];
                            entEstVenta.Producto = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                            break;
                        case 22:
                        case 23:
                            entEstVenta.id_rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                            entEstVenta.nombre_rik = dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();
                            entEstVenta.id_prd = (long)dr["Id"];
                            entEstVenta.Producto = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                            break;
                        default:

                            break;
                    }


                    entEstVenta.total = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("Total")));
                    entEstVenta.mes1 = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("mes1")));
                    entEstVenta.mes2 = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("mes2")));
                    entEstVenta.mes3 = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("mes3")));
                    entEstVenta.mes4 = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("mes4")));
                    entEstVenta.mes5 = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("mes5")));
                    entEstVenta.mes6 = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("mes6")));
                    entEstVenta.mes7 = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("mes7")));
                    entEstVenta.mes8 = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("mes8")));
                    entEstVenta.mes9 = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("mes9")));
                    entEstVenta.mes10 = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("mes10")));
                    entEstVenta.mes11 = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("mes11")));
                    entEstVenta.mes12 = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("mes12")));

                    lstEstVenta.Add(entEstVenta);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaVentaPeriodo(string Conexion, int Id_Emp, int Id_Cd, VenEstadisticaVentas entFiltroVentas, ref DataTable dataTable, ref List<VenEstadisticaVentas> lstEstVenta)
        {
            try
            {
                VenEstadisticaVentas entEstVenta = new VenEstadisticaVentas();
                SqlDataReader dr = null;
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                string[] Parametros ={
                              "@Id_Emp"
                             ,"@Id_Cd"
                             ,"@Anio"
                             ,"@Territorio"
                             ,"@Cliente"
                             ,"@Producto"
                             ,"@Tipo"
                             ,"@NivelCliente"
                             ,"@NivelProducto"
                             ,"@Reporte"
                             ,"@Id_U"
                             ,"@CategoriaProducto"
                             ,"@FechaInicio", "@FechaFin"
                            };
                object[] Valores ={
                                   Id_Emp
                                  ,Id_Cd
                                  ,entFiltroVentas.Anio
                                  ,entFiltroVentas.Territorio
                                  ,entFiltroVentas.Cliente
                                  ,entFiltroVentas.Producto
                                  ,entFiltroVentas.Mostrar
                                  ,entFiltroVentas.Nivel
                                  ,entFiltroVentas.Nivel2
                                  ,entFiltroVentas.Reporte
                                  ,entFiltroVentas.id_usu
                                  ,entFiltroVentas.categoria
                                  ,entFiltroVentas.FechaInicio.HasValue ? (object)entFiltroVentas.FechaInicio.Value : DBNull.Value
                                  ,entFiltroVentas.FechaFin.HasValue ? (object)entFiltroVentas.FechaFin.Value : DBNull.Value
                                  };

                string strQry = "exec spVentasPorPeriodo " + String.Join(", ", Valores.ToArray());

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spVentasPorPeriodo", ref dr, Parametros, Valores);

                //dataTable.Load(dr);
                while (dr.Read())
                {
                    entEstVenta = new VenEstadisticaVentas();


                    switch (entFiltroVentas.Reporte)
                    {
                        case 1:
                        case 2:
                            entEstVenta.id_ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                            entEstVenta.nombre_terr = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                            break;
                        case 3:
                        case 4:
                            entEstVenta.id_ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                            entEstVenta.nombre_terr = dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();
                            entEstVenta.id_cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                            entEstVenta.Cliente = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                            break;
                        case 5:
                        case 6:
                            entEstVenta.id_ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                            entEstVenta.nombre_terr = dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();
                            entEstVenta.id_prd = (long)dr["Id"];
                            entEstVenta.Producto = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                            break;
                        case 7:
                        case 8:
                            entEstVenta.id_ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                            entEstVenta.nombre_terr = dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();
                            entEstVenta.id_cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                            entEstVenta.Cliente = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                            entEstVenta.id_prd = (long)dr["Id"];
                            entEstVenta.Producto = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                            break;
                        case 9:
                        case 10:
                            entEstVenta.id_cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                            entEstVenta.Cliente = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                            break;
                        case 11:
                        case 12:
                            entEstVenta.id_cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                            entEstVenta.Cliente = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                            entEstVenta.id_prd = (long)dr["Id"];
                            entEstVenta.Producto = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                            break;
                        case 13:
                        case 14:
                            entEstVenta.id_prd = (long)dr["Id"];
                            entEstVenta.Producto = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                            break;
                        case 16:
                        case 17:
                            entEstVenta.id_rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                            entEstVenta.nombre_rik = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                            break;
                        case 18:
                            entEstVenta.id_rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                            entEstVenta.nombre_rik = dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();
                            entEstVenta.id_cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id")));
                            entEstVenta.Cliente = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                            break;
                        case 19:
                            entEstVenta.id_rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                            entEstVenta.nombre_rik = dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();
                            entEstVenta.id_cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                            entEstVenta.Cliente = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                            break;
                        case 20:
                        case 21:
                            entEstVenta.id_rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                            entEstVenta.nombre_rik = dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();
                            entEstVenta.id_cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                            entEstVenta.Cliente = dr.GetValue(dr.GetOrdinal("Cte_NomComercial")).ToString();
                            entEstVenta.id_prd = (long)dr["Id"];
                            entEstVenta.Producto = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                            break;
                        case 22:
                        case 23:
                            entEstVenta.id_rik = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                            entEstVenta.nombre_rik = dr.GetValue(dr.GetOrdinal("Ter_Nombre")).ToString();
                            entEstVenta.id_prd = (long)dr["Id"];
                            entEstVenta.Producto = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                            break;
                        default:

                            break;
                    }


                    entEstVenta.total = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("Total")));

                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        string columnName = dr.GetName(i);

                        if (!columnName.Equals("Id", StringComparison.OrdinalIgnoreCase) &&
    !columnName.Equals("Nombre", StringComparison.OrdinalIgnoreCase) && !columnName.Equals("Ter_Nombre", StringComparison.OrdinalIgnoreCase) &&
    !columnName.Equals("Id_Ter", StringComparison.OrdinalIgnoreCase) && !columnName.Equals("ID_Cd", StringComparison.OrdinalIgnoreCase) &&
    !columnName.Equals("Id_Cte", StringComparison.OrdinalIgnoreCase) && !columnName.Equals("Cte_NomComercial", StringComparison.OrdinalIgnoreCase) &&
    !columnName.Equals("Id_Prd", StringComparison.OrdinalIgnoreCase) && !columnName.Equals("Prd_Descripcion", StringComparison.OrdinalIgnoreCase) &&
    !columnName.Equals("Total", StringComparison.OrdinalIgnoreCase))
                        {
                            try
                            {
                                long valorMes = 0;

                                if (dr[i] != DBNull.Value)
                                {
                                    decimal valorDecimal = Convert.ToDecimal(dr[i]);
                                    valorMes = Convert.ToInt64(valorDecimal);
                                }

                                entEstVenta.Meses[columnName] = valorMes;
                            }
                            catch (Exception ex)
                            {
                                throw new Exception($"Error al convertir la columna '{columnName}' con valor '{dr[i]}'", ex);
                            }
                        }
                    }

                    lstEstVenta.Add(entEstVenta);
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