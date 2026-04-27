using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_ReporteRemisiones
    {
        public void Rep_RemisionesVencidasCN(ReporteRemisiones Datos, ref List<ReporteRemisiones> ReporteRemisiones, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                ReporteRemisiones registros;

                string[] Parametros = { "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Ter",
                                        "@Id_Cte",
                                        "@Vencido",
                                        "@FechaIni",
                                        "@FechaFin",
                                        "@TipoRemision"};

                object[] Valores = { Datos.Id_Emp,
                                    Datos.Id_Cd,
                                     (object)null,
                                     (object)null,
                                    Datos.Vencido,
                                    Datos.FechaIni,
                                    Datos.FechaFin,
                                    Datos.TipoRemision };

                // ------------------------------------
                // Consultar Gestion de Rentabilidad
                // ------------------------------------
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRep_RemisionesVencidasCN", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    registros = new ReporteRemisiones();


                    registros.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    registros.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    registros.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    registros.Cte_NomComercial = Convert.ToString(dr.GetValue(dr.GetOrdinal("Cte_NomComercial")));
                    registros.nombreMatriz = Convert.ToString(dr.GetValue(dr.GetOrdinal("nombreMatriz")));
                    registros.id_matriz = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_matriz")));
                    registros.Ter_Nombre = Convert.ToString(dr.GetValue(dr.GetOrdinal("Ter_Nombre")));
                    registros.Id_Rem = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rem")));
                    registros.Rem_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Rem_Fecha")));
                    registros.Id_Prd = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("Id_Prd")));

                    registros.Prd_Descripcion = Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_Descripcion")));
                    registros.Rem_Cant = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Rem_Cant")));
                    registros.vencido = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("vencido")));
                    registros.Rem_Dev = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Rem_Dev")));
                    registros.Prd_Pesos = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Prd_Pesos")));
                    registros.SaldoUnidades = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("SaldoUnidades")));


                    ReporteRemisiones.Add(registros);
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Rep_RemisionesVencidas(ReporteRemisiones Datos, ref List<ReporteRemisiones> ReporteRemisiones, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                ReporteRemisiones registros;

                string[] Parametros = { "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Ter",
                                        "@Id_Cte",
                                        "@Vencido",
                                        "@FechaIni",
                                        "@FechaFin",
                                        "@TipoRemision"};

                object[] Valores = { Datos.Id_Emp,
                                    Datos.Id_Cd,
                                     (object)null,
                                     (object)null,
                                    Datos.Vencido,
                                    Datos.FechaIni,
                                    Datos.FechaFin,
                                    Datos.TipoRemision };

                // ------------------------------------
                // Consultar Gestion de Rentabilidad
                // ------------------------------------
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRep_RemisionesVencidas", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    registros = new ReporteRemisiones();
                    registros.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    registros.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    registros.Cte_NomComercial = Convert.ToString(dr.GetValue(dr.GetOrdinal("Cte_NomComercial")));
                    registros.Ter_Nombre = Convert.ToString(dr.GetValue(dr.GetOrdinal("Ter_Nombre")));
                    registros.Id_Rem = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rem")));
                    registros.Rem_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Rem_Fecha")));
                    registros.Id_Prd = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("Id_Prd")));

                    registros.Prd_Descripcion = Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_Descripcion")));
                    registros.Rem_Cant = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Rem_Cant")));
                    registros.vencido = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("vencido")));
                    registros.Rem_Dev = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Rem_Dev")));
                    registros.Prd_Pesos = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Prd_Pesos")));
                    registros.SaldoUnidades = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("SaldoUnidades")));


                    ReporteRemisiones.Add(registros);
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Rep_RemisionesFacturas(ReporteRemisiones Datos, ref List<ReporteRemisiones> ReporteRemisiones, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                ReporteRemisiones registros;

                string[] Parametros = { "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Ter",
                                        "@Id_Cte",
                                        "@Vencido",
                                        "@FechaIni",
                                        "@FechaFin",
                                        "@TipoRemision"};

                object[] Valores = { Datos.Id_Emp,
                                    Datos.Id_Cd,
                                     (object)null,
                                     (object)null,
                                    Datos.Vencido,
                                    Datos.FechaIni,
                                    Datos.FechaFin,
                                    Datos.TipoRemision };

                // ------------------------------------
                // Consultar Gestion de Rentabilidad
                // ------------------------------------
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRep_RemisionesVencidasFacturas", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    registros = new ReporteRemisiones();


                    registros.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    registros.Id_Ter = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Ter")));
                    registros.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    registros.Cte_NomComercial = Convert.ToString(dr.GetValue(dr.GetOrdinal("Cte_NomComercial")));
                    registros.nombreMatriz = Convert.ToString(dr.GetValue(dr.GetOrdinal("nombreMatriz")));
                    registros.id_matriz = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_matriz")));
                    registros.Ter_Nombre = Convert.ToString(dr.GetValue(dr.GetOrdinal("Ter_Nombre")));
                    registros.Id_Rem = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Rem")));
                    registros.Rem_Fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("Rem_Fecha")));
                    registros.Id_Prd = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    registros.Folio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CFD_Folio")));
                    registros.Prd_Descripcion = Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_Descripcion")));
                    registros.Rem_Cant = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Rem_Cant")));
                    registros.vencido = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("vencido")));
                    registros.Rem_Dev = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Rem_Dev")));
                    registros.Prd_Pesos = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Prd_Pesos")));
                    registros.SaldoUnidades = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("SaldoUnidades")));


                    ReporteRemisiones.Add(registros);
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Rep_Consigancion(ReporteConsignacion Datos, ref List<ReporteConsignacion> ReporteRemisiones, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                ReporteConsignacion registros;

                string[] Parametros = { "@Id_Emp",
                                        "@Id_Cd",
                                        "@Fecha",
                                        "@Id_PrdStr",
                                        "@Id_CteStr"};

                object[] Valores = { Datos.Id_Emp,
                                    Datos.Id_Cd,
                                    Datos.fecha,
                                    (object)null,
                                    (object)null,};

                // ------------------------------------
                // Consultar Gestion de Rentabilidad
                // ------------------------------------
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRepAnalisisRemision_Actual", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    registros = new ReporteConsignacion();


                    registros.Prd_Descripcion = Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_Descripcion")));
                    registros.CD_Nom = Convert.ToString(dr.GetValue(dr.GetOrdinal("CD_Nom")));
                    registros.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                    registros.Id_Cte = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cte")));
                    registros.Cte_NomComercial = Convert.ToString(dr.GetValue(dr.GetOrdinal("Cte_NomComercial")));
                    registros.Id_Prd = Convert.ToInt64(dr.GetValue(dr.GetOrdinal("Id_Prd")));

                    registros.RetiraroFacturar = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("RetiraroFacturar")));
                    registros.Vencido = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vencido")));
                    registros.Vigente = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vigente")));
                    registros.Rotacion = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Rotacion")));
                    registros.CostoPromedio = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("CostoPromedio")));
                    registros.Promedio = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Promedio")));
                    registros.Ultimo = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Ultimo")));
                    registros.Penultimo = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Penultimo")));
                    registros.Antepenultimo = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Antepenultimo")));

                    registros.ImporteInventario = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("ImporteInventario")));
                    registros.Prd_InvFinal = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Prd_InvFinal")));
                    registros.Id_Uni = Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_Uni")));
                    registros.Prd_PrecioAAA = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Prd_PrecioAAA")));
                    registros.Prd_Presentacion = Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_Presentacion")));


                    ReporteRemisiones.Add(registros);
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