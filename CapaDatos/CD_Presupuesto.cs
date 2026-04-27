using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Presupuesto
    {
        public void AgregarPresupuestoRIk(CatPresupuesto RegistroPresupuesto, ref int verificador, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {  "@Id_Emp",
                                         "@Sucursal",
                                         "@Id_Rik",
                                         "@Nombre_rik",
                                         "@Mes",
                                         "@Anio",
                                         "@Presupuesto",
                                         "@permiso"
                };
                object[] Valores = { RegistroPresupuesto.Id_Emp ,
                                       RegistroPresupuesto.Sucursal ,
                                       RegistroPresupuesto.Id_Rik,
                                       RegistroPresupuesto.NombreRik,
                                       RegistroPresupuesto.Mes ,
                                       RegistroPresupuesto.Anio ,
                                       RegistroPresupuesto.Presupuestostr,
                                        RegistroPresupuesto.permiso};

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SpCatPresupuesto_Insertar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AgregarCategoriaPresupuestoRIk(CatPresupuesto RegistroPresupuesto, ref int verificador, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {  "@Id_Emp",
                                         "@id_Cd",
                                         "@TipoCuena",
                                         "@Categoria",
                                         "@Id_Rik",
                                         "@Nombre_rik",
                                         "@Mes",
                                         "@Anio",
                                         "@Presupuesto",
                                         "@utilidadBruta",
                                         "@percutilidadPrima"
                };
                object[] Valores = {   RegistroPresupuesto.Id_Emp ,
                                       RegistroPresupuesto.Id_Cd ,
                                       RegistroPresupuesto.TipoCuenta,
                                       RegistroPresupuesto.Categoria,
                                       RegistroPresupuesto.Id_Rik,
                                       RegistroPresupuesto.NombreRik,
                                       RegistroPresupuesto.Mes ,
                                       RegistroPresupuesto.Anio ,
                                       RegistroPresupuesto.Presupuesto.ToString(),
                                       RegistroPresupuesto.utilidadBruta.ToString(),
                                       RegistroPresupuesto.percutilidadPrima.ToString()};

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SpCatPresupuestoCategoria_Insertar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AgregarVNRRIk(CatPresupuesto RegistroPresupuesto, ref int verificador, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                         "@Sucursal",
                                         "@Id_Rik",
                                         "@Nombre_rik",
                                         "@VNR"
                };
                object[] Valores = {
                                       RegistroPresupuesto.Sucursal ,
                                       RegistroPresupuesto.Id_Rik,
                                       RegistroPresupuesto.NombreRik,
                                       RegistroPresupuesto.cargavnr };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SpCatVNR_Insertar", ref verificador, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaPresupuestoRIk(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            try
            {
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                CatPresupuesto nuevoPresupuesto;

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@FechaInicial",
                                          "@fechafinal"
                };
                object[] Valores = {
                                       RegistroPresupuesto.Id_Emp,
                                       RegistroPresupuesto.Id_Cd,
                                       RegistroPresupuesto.FechaInicial,
                                       RegistroPresupuesto.fechafinal
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                //Encabezado del PAGO
                sqlcmd = CapaDatos.GenerarSqlCommand("SpCatPresupuesto_Consultar", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    nuevoPresupuesto = new CatPresupuesto();

                    nuevoPresupuesto.Id_Emp = dr.IsDBNull(dr.GetOrdinal("id_emp")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_emp"));
                    nuevoPresupuesto.Id_Cd = dr.IsDBNull(dr.GetOrdinal("id_cd")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_cd"));
                    nuevoPresupuesto.Sucursal = dr.IsDBNull(dr.GetOrdinal("Sucursal")) ? "" : dr.GetString(dr.GetOrdinal("Sucursal"));
                    nuevoPresupuesto.Id_Rik = dr.IsDBNull(dr.GetOrdinal("id_rik")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_rik"));
                    nuevoPresupuesto.NombreRik = dr.IsDBNull(dr.GetOrdinal("nombre_rik")) ? "" : dr.GetString(dr.GetOrdinal("nombre_rik"));
                    nuevoPresupuesto.Mes = dr.IsDBNull(dr.GetOrdinal("mes")) ? 0 : dr.GetInt32(dr.GetOrdinal("mes"));
                    nuevoPresupuesto.Anio = dr.IsDBNull(dr.GetOrdinal("anio")) ? 0 : dr.GetInt32(dr.GetOrdinal("anio"));
                    nuevoPresupuesto.Presupuesto = dr.IsDBNull(dr.GetOrdinal("presupuesto")) ? 0 : Convert.ToDouble(dr.GetDecimal(dr.GetOrdinal("presupuesto")));
                    list_Presupuesto.Add(nuevoPresupuesto);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ConsultaPresupuestoMesualRIk(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            try
            {
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                CatPresupuesto nuevoPresupuesto;

                string[] Parametros = {
                                         "@Id_Emp",
                                         "@Id_Cd",
                                         "@FechaInicial",
                                         "@fechaFinal"
                };
                object[] Valores = {
                                       RegistroPresupuesto.Id_Emp,
                                       RegistroPresupuesto.Id_Cd,
                                       RegistroPresupuesto.FechaInicial,
                                       RegistroPresupuesto.fechafinal
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                //Encabezado del PAGO
                sqlcmd = CapaDatos.GenerarSqlCommand("SpConsultarPresupuestoMensualRik", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    nuevoPresupuesto = new CatPresupuesto();
                    nuevoPresupuesto.Id_Rik = dr.IsDBNull(dr.GetOrdinal("id_rik")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_rik"));
                    nuevoPresupuesto.NombreRik = dr.IsDBNull(dr.GetOrdinal("nombre_rik")) ? "" : dr.GetString(dr.GetOrdinal("nombre_rik"));
                    nuevoPresupuesto.TotalPresupuesto = dr.IsDBNull(dr.GetOrdinal("TOTALPRESUPUESTO")) ? 0 : dr.GetDecimal(dr.GetOrdinal("TOTALPRESUPUESTO"));
                    list_Presupuesto.Add(nuevoPresupuesto);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ConsultaPresupuestoMesualPvvRIk(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            try
            {
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                CatPresupuesto nuevoPresupuesto;

                string[] Parametros = {
                                         "@Id_Emp",
                                         "@Id_Cd",
                                         "@FechaInicial",
                                         "@fechaFinal"
                };
                object[] Valores = {
                                       RegistroPresupuesto.Id_Emp,
                                       RegistroPresupuesto.Id_Cd,
                                       RegistroPresupuesto.FechaInicial,
                                       RegistroPresupuesto.fechafinal
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                //Encabezado del PAGO
                sqlcmd = CapaDatos.GenerarSqlCommand("SpConsultarPresupuestoMensualpvvRik", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    nuevoPresupuesto = new CatPresupuesto();
                    nuevoPresupuesto.Id_Rik = dr.IsDBNull(dr.GetOrdinal("id_rik")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_rik"));
                    nuevoPresupuesto.NombreRik = dr.IsDBNull(dr.GetOrdinal("nombre_rik")) ? "" : dr.GetString(dr.GetOrdinal("nombre_rik"));
                    nuevoPresupuesto.Anio = dr.IsDBNull(dr.GetOrdinal("anio")) ? 0 : dr.GetInt32(dr.GetOrdinal("anio"));
                    nuevoPresupuesto.Mes = dr.IsDBNull(dr.GetOrdinal("mes")) ? 0 : dr.GetInt32(dr.GetOrdinal("mes"));
                    nuevoPresupuesto.fecha = DateTime.Parse("01-" + (nuevoPresupuesto.Mes < 10 ? "0" + nuevoPresupuesto.Mes.ToString() : nuevoPresupuesto.Mes.ToString()) + "-" + nuevoPresupuesto.Anio);
                    nuevoPresupuesto.TotalPresupuesto = dr.IsDBNull(dr.GetOrdinal("TOTALPRESUPUESTO")) ? 0 : dr.GetDecimal(dr.GetOrdinal("TOTALPRESUPUESTO"));
                    list_Presupuesto.Add(nuevoPresupuesto);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ConsultaVentaTotal(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            try
            {
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                CatPresupuesto nuevoPresupuesto;

                string[] Parametros = {

                                         "@Id_Cd",
                                         "@AnioInicial",
                                         "@AnioFinal"
                };
                object[] Valores = {
                                       RegistroPresupuesto.Id_Cd,
                                       RegistroPresupuesto.AnioInicial,
                                        RegistroPresupuesto.AnioFinal,
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                //Encabezado del PAGO
                sqlcmd = CapaDatos.GenerarSqlCommand("sp_ConsultarVentatotal", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    nuevoPresupuesto = new CatPresupuesto();
                    nuevoPresupuesto.Id_Emp = dr.IsDBNull(dr.GetOrdinal("idEmp")) ? 0 : dr.GetInt32(dr.GetOrdinal("idEmp"));
                    nuevoPresupuesto.Id_Cd = dr.IsDBNull(dr.GetOrdinal("idcd")) ? 0 : dr.GetInt32(dr.GetOrdinal("idcd"));
                    nuevoPresupuesto.Anio = dr.IsDBNull(dr.GetOrdinal("anio")) ? 0 : dr.GetInt32(dr.GetOrdinal("anio"));
                    nuevoPresupuesto.Mes = dr.IsDBNull(dr.GetOrdinal("mes")) ? 0 : dr.GetInt32(dr.GetOrdinal("mes"));
                    nuevoPresupuesto.fecha = DateTime.Parse("01-" + (nuevoPresupuesto.Mes < 10 ? "0" + nuevoPresupuesto.Mes.ToString() : nuevoPresupuesto.Mes.ToString()) + "-" + nuevoPresupuesto.Anio);
                    nuevoPresupuesto.TotalPresupuesto = dr.IsDBNull(dr.GetOrdinal("venta")) ? 0 : dr.GetDecimal(dr.GetOrdinal("venta"));
                    list_Presupuesto.Add(nuevoPresupuesto);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ConsultaVentaTotalTrimestral(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            try
            {
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                CatPresupuesto nuevoPresupuesto;

                string[] Parametros = {

                                         "@Id_Cd",
                                         "@AnioInicial",
                                         "@AnioFinal"
                };
                object[] Valores = {
                                       RegistroPresupuesto.Id_Cd,
                                       RegistroPresupuesto.AnioInicial,
                                        RegistroPresupuesto.AnioFinal,
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                //Encabezado del PAGO
                sqlcmd = CapaDatos.GenerarSqlCommand("sp_ConsultarVentatotalTrimestrak", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    nuevoPresupuesto = new CatPresupuesto();
                    nuevoPresupuesto.Id_Emp = dr.IsDBNull(dr.GetOrdinal("idEmp")) ? 0 : dr.GetInt32(dr.GetOrdinal("idEmp"));
                    nuevoPresupuesto.Id_Cd = dr.IsDBNull(dr.GetOrdinal("idcd")) ? 0 : dr.GetInt32(dr.GetOrdinal("idcd"));
                    nuevoPresupuesto.Anio = dr.IsDBNull(dr.GetOrdinal("anio")) ? 0 : dr.GetInt32(dr.GetOrdinal("anio"));
                    nuevoPresupuesto.Mes = dr.IsDBNull(dr.GetOrdinal("mes")) ? 0 : dr.GetInt32(dr.GetOrdinal("mes"));
                    nuevoPresupuesto.fecha = DateTime.Parse("01-" + (nuevoPresupuesto.Mes < 10 ? "0" + nuevoPresupuesto.Mes.ToString() : nuevoPresupuesto.Mes.ToString()) + "-" + nuevoPresupuesto.Anio);
                    nuevoPresupuesto.TotalPresupuesto = dr.IsDBNull(dr.GetOrdinal("venta")) ? 0 : dr.GetDecimal(dr.GetOrdinal("venta"));
                    list_Presupuesto.Add(nuevoPresupuesto);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ConsultaRemisionTotal(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            try
            {
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                CatPresupuesto nuevoPresupuesto;

                string[] Parametros = {

                                         "@Id_Cd",
                                         "@FechaInicial",
                                         "@FechaFinal"
                };
                object[] Valores = {
                                       RegistroPresupuesto.Id_Cd,
                                       RegistroPresupuesto.FechaInicial,
                                        RegistroPresupuesto.fechafinal,
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                //Encabezado del PAGO
                sqlcmd = CapaDatos.GenerarSqlCommand("sp_VentatotalReporte", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    nuevoPresupuesto = new CatPresupuesto();
                    nuevoPresupuesto.Id_Cd = dr.IsDBNull(dr.GetOrdinal("idcd")) ? 0 : dr.GetInt32(dr.GetOrdinal("idcd"));
                    nuevoPresupuesto.Anio = dr.IsDBNull(dr.GetOrdinal("anio")) ? 0 : dr.GetInt32(dr.GetOrdinal("anio"));
                    nuevoPresupuesto.Mes = dr.IsDBNull(dr.GetOrdinal("mes")) ? 0 : dr.GetInt32(dr.GetOrdinal("mes"));
                    nuevoPresupuesto.fecha = DateTime.Parse("01-" + (nuevoPresupuesto.Mes < 10 ? "0" + nuevoPresupuesto.Mes.ToString() : nuevoPresupuesto.Mes.ToString()) + "-" + nuevoPresupuesto.Anio);
                    nuevoPresupuesto.TotalPresupuesto = dr.IsDBNull(dr.GetOrdinal("Venta")) ? 0 : dr.GetDecimal(dr.GetOrdinal("Venta"));
                    nuevoPresupuesto.venta = dr.IsDBNull(dr.GetOrdinal("Venta")) ? 0 : Convert.ToDouble(dr.GetDecimal(dr.GetOrdinal("Venta")));
                    nuevoPresupuesto.utilidadBruta = dr.IsDBNull(dr.GetOrdinal("VentaUb")) ? 0 : Convert.ToDouble(dr.GetDecimal(dr.GetOrdinal("VentaUb")));
                    nuevoPresupuesto.UB = dr.IsDBNull(dr.GetOrdinal("UtilidadBruta")) ? 0 : dr.GetDecimal(dr.GetOrdinal("UtilidadBruta"));
                    nuevoPresupuesto.FechaInicial = RegistroPresupuesto.FechaInicial;
                    nuevoPresupuesto.fechafinal = RegistroPresupuesto.fechafinal;
                    list_Presupuesto.Add(nuevoPresupuesto);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ConsultRemisionTotalTrimestral(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            try
            {
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                CatPresupuesto nuevoPresupuesto;

                string[] Parametros = {

                                         "@Id_Cd",
                                         "@FechaInicial",
                                         "@FechaFinal"
                };
                object[] Valores = {
                                       RegistroPresupuesto.Id_Cd,
                                       RegistroPresupuesto.FechaInicial,
                                       RegistroPresupuesto.fechafinal,
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                //Encabezado del PAGO
                sqlcmd = CapaDatos.GenerarSqlCommand("sp_VentatotalReporteTrimestral", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    nuevoPresupuesto = new CatPresupuesto();
                    nuevoPresupuesto.Id_Cd = dr.IsDBNull(dr.GetOrdinal("idcd")) ? 0 : dr.GetInt32(dr.GetOrdinal("idcd"));
                    nuevoPresupuesto.Anio = dr.IsDBNull(dr.GetOrdinal("anio")) ? 0 : dr.GetInt32(dr.GetOrdinal("anio"));
                    nuevoPresupuesto.Mes = dr.IsDBNull(dr.GetOrdinal("mes")) ? 0 : dr.GetInt32(dr.GetOrdinal("mes"));
                    nuevoPresupuesto.fecha = DateTime.Parse("01-" + (nuevoPresupuesto.Mes < 10 ? "0" + nuevoPresupuesto.Mes.ToString() : nuevoPresupuesto.Mes.ToString()) + "-" + nuevoPresupuesto.Anio);
                    nuevoPresupuesto.TotalPresupuesto = dr.IsDBNull(dr.GetOrdinal("venta")) ? 0 : dr.GetDecimal(dr.GetOrdinal("venta"));
                    list_Presupuesto.Add(nuevoPresupuesto);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public void ConsultaUtilidadRIk(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            try
            {
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                CatPresupuesto nuevoPresupuesto;

                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@mesInicial",
                                        "@anioInicial",
                                        "@mesFinal",
                                        "@anioFinal"
                };

                object[] Valores = {
                                       RegistroPresupuesto.Id_Emp,
                                       RegistroPresupuesto.Id_Cd,
                                       RegistroPresupuesto.MesInicial,
                                       RegistroPresupuesto.AnioInicial,
                                       RegistroPresupuesto.MesFinal,
                                       RegistroPresupuesto.AnioFinal,
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                //Encabezado del PAGO
                sqlcmd = CapaDatos.GenerarSqlCommand("sp_ReportePresupueto", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    nuevoPresupuesto = new CatPresupuesto();
                    nuevoPresupuesto.Id_Emp = dr.IsDBNull(dr.GetOrdinal("Id_Emp")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Emp"));
                    nuevoPresupuesto.Id_Cd = dr.IsDBNull(dr.GetOrdinal("Id_Cd")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                    nuevoPresupuesto.id_ter = dr.IsDBNull(dr.GetOrdinal("id_ter")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_ter"));
                    nuevoPresupuesto.ter_nombre = dr.IsDBNull(dr.GetOrdinal("ter_nombre")) ? "" : dr.GetString(dr.GetOrdinal("ter_nombre"));
                    nuevoPresupuesto.cantidad = dr.IsDBNull(dr.GetOrdinal("cantidad")) ? 0 : dr.GetDouble(dr.GetOrdinal("cantidad"));
                    nuevoPresupuesto.PrecioDistribuidor = dr.IsDBNull(dr.GetOrdinal("PrecioDistribuidor")) ? 0 : dr.GetDouble(dr.GetOrdinal("PrecioDistribuidor"));
                    nuevoPresupuesto.PrecioLista = dr.IsDBNull(dr.GetOrdinal("PrecioLista")) ? 0 : dr.GetDouble(dr.GetOrdinal("PrecioLista"));
                    nuevoPresupuesto.PrecioVenta = dr.IsDBNull(dr.GetOrdinal("PrecioVenta")) ? 0 : dr.GetDouble(dr.GetOrdinal("PrecioVenta"));
                    nuevoPresupuesto.venta = dr.IsDBNull(dr.GetOrdinal("venta")) ? 0 : dr.GetDouble(dr.GetOrdinal("venta"));
                    nuevoPresupuesto.Costo = dr.IsDBNull(dr.GetOrdinal("Costo")) ? 0 : dr.GetDouble(dr.GetOrdinal("Costo"));
                    nuevoPresupuesto.utilidadBruta = dr.IsDBNull(dr.GetOrdinal("utilidadBruta")) ? 0 : dr.GetDouble(dr.GetOrdinal("utilidadBruta"));
                    nuevoPresupuesto.porcubreal = dr.IsDBNull(dr.GetOrdinal("porcubreal")) ? 0 : dr.GetDouble(dr.GetOrdinal("porcubreal"));
                    nuevoPresupuesto.porcubplaneada = dr.IsDBNull(dr.GetOrdinal("porcubplaneada")) ? 0 : dr.GetDouble(dr.GetOrdinal("porcubplaneada"));
                    nuevoPresupuesto.varianzaubrutapuntos = dr.IsDBNull(dr.GetOrdinal("varianzaubrutapuntos")) ? 0 : dr.GetDouble(dr.GetOrdinal("varianzaubrutapuntos"));
                    nuevoPresupuesto.impactopesos = dr.IsDBNull(dr.GetOrdinal("impactopesos")) ? 0 : dr.GetDouble(dr.GetOrdinal("impactopesos"));
                    nuevoPresupuesto.importecostopublico = dr.IsDBNull(dr.GetOrdinal("importecostopublico")) ? 0 : dr.GetDouble(dr.GetOrdinal("importecostopublico"));
                    list_Presupuesto.Add(nuevoPresupuesto);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ConsultaUtilidadRIkxmes(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            try
            {
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                CatPresupuesto nuevoPresupuesto;

                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@mesInicial",
                                        "@anioInicial",
                                        "@mesFinal",
                                        "@anioFinal",
                                        "@Id_u",
                                        "@Id_Rik"
                };

                object[] Valores = {
                                       RegistroPresupuesto.Id_Emp,
                                       RegistroPresupuesto.Id_Cd,
                                       RegistroPresupuesto.MesInicial,
                                       RegistroPresupuesto.AnioInicial,
                                       RegistroPresupuesto.MesFinal,
                                       RegistroPresupuesto.AnioFinal,
                                       RegistroPresupuesto.Id_u == -1?    (object) null :  RegistroPresupuesto.Id_u,
                                       RegistroPresupuesto.Id_Rik == -1?    (object) null :  RegistroPresupuesto.Id_Rik
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                //Encabezado del PAGO
                sqlcmd = CapaDatos.GenerarSqlCommand("sp_ReportePresupuetoxmes", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    nuevoPresupuesto = new CatPresupuesto();
                    nuevoPresupuesto.Id_Emp = dr.IsDBNull(dr.GetOrdinal("Id_Emp")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Emp"));
                    nuevoPresupuesto.Id_Cd = dr.IsDBNull(dr.GetOrdinal("Id_Cd")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                    nuevoPresupuesto.id_ter = dr.IsDBNull(dr.GetOrdinal("id_ter")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_ter"));
                    nuevoPresupuesto.ter_nombre = dr.IsDBNull(dr.GetOrdinal("ter_nombre")) ? "" : dr.GetString(dr.GetOrdinal("ter_nombre"));
                    nuevoPresupuesto.cantidad = dr.IsDBNull(dr.GetOrdinal("cantidad")) ? 0 : dr.GetDouble(dr.GetOrdinal("cantidad"));
                    nuevoPresupuesto.PrecioDistribuidor = dr.IsDBNull(dr.GetOrdinal("PrecioDistribuidor")) ? 0 : dr.GetDouble(dr.GetOrdinal("PrecioDistribuidor"));
                    nuevoPresupuesto.PrecioLista = dr.IsDBNull(dr.GetOrdinal("PrecioLista")) ? 0 : dr.GetDouble(dr.GetOrdinal("PrecioLista"));
                    nuevoPresupuesto.PrecioVenta = dr.IsDBNull(dr.GetOrdinal("PrecioVenta")) ? 0 : dr.GetDouble(dr.GetOrdinal("PrecioVenta"));
                    nuevoPresupuesto.venta = dr.IsDBNull(dr.GetOrdinal("venta")) ? 0 : dr.GetDouble(dr.GetOrdinal("venta"));
                    nuevoPresupuesto.Costo = dr.IsDBNull(dr.GetOrdinal("Costo")) ? 0 : dr.GetDouble(dr.GetOrdinal("Costo"));
                    nuevoPresupuesto.utilidadBruta = dr.IsDBNull(dr.GetOrdinal("utilidadBruta")) ? 0 : dr.GetDouble(dr.GetOrdinal("utilidadBruta"));
                    nuevoPresupuesto.porcubreal = dr.IsDBNull(dr.GetOrdinal("porcubreal")) ? 0 : dr.GetDouble(dr.GetOrdinal("porcubreal"));
                    nuevoPresupuesto.porcubplaneada = dr.IsDBNull(dr.GetOrdinal("porcubplaneada")) ? 0 : dr.GetDouble(dr.GetOrdinal("porcubplaneada"));
                    nuevoPresupuesto.varianzaubrutapuntos = dr.IsDBNull(dr.GetOrdinal("varianzaubrutapuntos")) ? 0 : dr.GetDouble(dr.GetOrdinal("varianzaubrutapuntos"));
                    nuevoPresupuesto.impactopesos = dr.IsDBNull(dr.GetOrdinal("impactopesos")) ? 0 : dr.GetDouble(dr.GetOrdinal("impactopesos"));
                    nuevoPresupuesto.importecostopublico = dr.IsDBNull(dr.GetOrdinal("importecostopublico")) ? 0 : dr.GetDouble(dr.GetOrdinal("importecostopublico"));
                    nuevoPresupuesto.Mes = dr.IsDBNull(dr.GetOrdinal("mes")) ? 0 : dr.GetInt32(dr.GetOrdinal("mes"));
                    nuevoPresupuesto.Anio = dr.IsDBNull(dr.GetOrdinal("anio")) ? 0 : dr.GetInt32(dr.GetOrdinal("anio"));
                    nuevoPresupuesto.fecha = DateTime.Parse("01-" + (nuevoPresupuesto.Mes < 10 ? "0" + nuevoPresupuesto.Mes.ToString() : nuevoPresupuesto.Mes.ToString()) + "-" + nuevoPresupuesto.Anio);
                    nuevoPresupuesto.Id_Rik = dr.IsDBNull(dr.GetOrdinal("id_rik")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_rik"));
                    list_Presupuesto.Add(nuevoPresupuesto);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public void ConsultaUtilidadRIkxmesCliente(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            try
            {
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                CatPresupuesto nuevoPresupuesto;

                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@FechaInicial",
                                        "@fechaFinal",
                                        "@Id_u",
                                        "@Id_Rik"
                };

                object[] Valores = {
                                       RegistroPresupuesto.Id_Emp,
                                       RegistroPresupuesto.Id_Cd,
                                       RegistroPresupuesto.FechaInicial,
                                       RegistroPresupuesto.fechafinal,
                                       RegistroPresupuesto.Id_u == -1?    (object) null :  RegistroPresupuesto.Id_u,
                                       RegistroPresupuesto.Id_Rik == -1?    (object) null :  RegistroPresupuesto.Id_Rik
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                //Encabezado del PAGO
                sqlcmd = CapaDatos.GenerarSqlCommand("sp_ReportePresupuetoxmesCte2", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    nuevoPresupuesto = new CatPresupuesto();
                    nuevoPresupuesto.Id_Emp = dr.IsDBNull(dr.GetOrdinal("Id_Emp")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Emp"));
                    nuevoPresupuesto.Id_Cd = dr.IsDBNull(dr.GetOrdinal("Id_Cd")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                    nuevoPresupuesto.id_cte = dr.IsDBNull(dr.GetOrdinal("id_cte")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_cte"));
                    nuevoPresupuesto.cte_nomcomercial = dr.IsDBNull(dr.GetOrdinal("cte_nomcomercial")) ? "" : dr.GetString(dr.GetOrdinal("cte_nomcomercial"));
                    nuevoPresupuesto.id_ter = dr.IsDBNull(dr.GetOrdinal("id_ter")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_ter"));
                    nuevoPresupuesto.ter_nombre = dr.IsDBNull(dr.GetOrdinal("ter_nombre")) ? "" : dr.GetString(dr.GetOrdinal("ter_nombre"));
                    nuevoPresupuesto.cantidad = dr.IsDBNull(dr.GetOrdinal("cantidad")) ? 0 : dr.GetDouble(dr.GetOrdinal("cantidad"));
                    nuevoPresupuesto.PrecioDistribuidor = dr.IsDBNull(dr.GetOrdinal("PrecioDistribuidor")) ? 0 : dr.GetDouble(dr.GetOrdinal("PrecioDistribuidor"));
                    nuevoPresupuesto.PrecioLista = dr.IsDBNull(dr.GetOrdinal("PrecioLista")) ? 0 : dr.GetDouble(dr.GetOrdinal("PrecioLista"));
                    nuevoPresupuesto.PrecioVenta = dr.IsDBNull(dr.GetOrdinal("PrecioVenta")) ? 0 : dr.GetDouble(dr.GetOrdinal("PrecioVenta"));
                    nuevoPresupuesto.venta = dr.IsDBNull(dr.GetOrdinal("venta")) ? 0 : dr.GetDouble(dr.GetOrdinal("venta"));
                    nuevoPresupuesto.Costo = dr.IsDBNull(dr.GetOrdinal("Costo")) ? 0 : dr.GetDouble(dr.GetOrdinal("Costo"));
                    nuevoPresupuesto.utilidadBruta = dr.IsDBNull(dr.GetOrdinal("utilidadBruta")) ? 0 : dr.GetDouble(dr.GetOrdinal("utilidadBruta"));
                    nuevoPresupuesto.porcubreal = dr.IsDBNull(dr.GetOrdinal("porcubreal")) ? 0 : dr.GetDouble(dr.GetOrdinal("porcubreal"));
                    nuevoPresupuesto.porcubplaneada = dr.IsDBNull(dr.GetOrdinal("porcubplaneada")) ? 0 : dr.GetDouble(dr.GetOrdinal("porcubplaneada"));
                    nuevoPresupuesto.varianzaubrutapuntos = dr.IsDBNull(dr.GetOrdinal("varianzaubrutapuntos")) ? 0 : dr.GetDouble(dr.GetOrdinal("varianzaubrutapuntos"));
                    nuevoPresupuesto.impactopesos = dr.IsDBNull(dr.GetOrdinal("impactopesos")) ? 0 : dr.GetDouble(dr.GetOrdinal("impactopesos"));
                    nuevoPresupuesto.importecostopublico = dr.IsDBNull(dr.GetOrdinal("importecostopublico")) ? 0 : dr.GetDouble(dr.GetOrdinal("importecostopublico"));
                    nuevoPresupuesto.Mes = dr.IsDBNull(dr.GetOrdinal("mes")) ? 0 : dr.GetInt32(dr.GetOrdinal("mes"));
                    nuevoPresupuesto.Anio = dr.IsDBNull(dr.GetOrdinal("anio")) ? 0 : dr.GetInt32(dr.GetOrdinal("anio"));
                    nuevoPresupuesto.fecha = DateTime.Parse("01-" + (nuevoPresupuesto.Mes < 10 ? "0" + nuevoPresupuesto.Mes.ToString() : nuevoPresupuesto.Mes.ToString()) + "-" + nuevoPresupuesto.Anio);
                    nuevoPresupuesto.Id_Rik = dr.IsDBNull(dr.GetOrdinal("id_rik")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_rik"));
                    nuevoPresupuesto.id_matriz = dr.IsDBNull(dr.GetOrdinal("Id")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id"));
                    nuevoPresupuesto.Matriz = dr.IsDBNull(dr.GetOrdinal("Nombre")) ? "" : dr.GetString(dr.GetOrdinal("Nombre"));

                    nuevoPresupuesto.FechaInicial = RegistroPresupuesto.FechaInicial;
                    nuevoPresupuesto.fechafinal = RegistroPresupuesto.fechafinal;
                    list_Presupuesto.Add(nuevoPresupuesto);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public void PresupuestoMensualRik(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            try
            {
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                CatPresupuesto nuevoPresupuesto;

                string[] Parametros = {
                                         "@Id_Emp",
                                         "@Id_Cd",
                                         "@FechaInicial",
                                         "@fechaFinal"
                };
                object[] Valores = {
                                       RegistroPresupuesto.Id_Emp,
                                       RegistroPresupuesto.Id_Cd,
                                       RegistroPresupuesto.FechaInicial,
                                       RegistroPresupuesto.fechafinal
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                //Encabezado del PAGO
                sqlcmd = CapaDatos.GenerarSqlCommand("SpPresupuestoMensualRik", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    nuevoPresupuesto = new CatPresupuesto();
                    nuevoPresupuesto.Id_Rik = dr.IsDBNull(dr.GetOrdinal("id_rik")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_rik"));
                    nuevoPresupuesto.NombreRik = dr.IsDBNull(dr.GetOrdinal("nombre_rik")) ? "" : dr.GetString(dr.GetOrdinal("nombre_rik"));
                    nuevoPresupuesto.Anio = dr.IsDBNull(dr.GetOrdinal("anio")) ? 0 : dr.GetInt32(dr.GetOrdinal("anio"));
                    nuevoPresupuesto.Mes = dr.IsDBNull(dr.GetOrdinal("mes")) ? 0 : dr.GetInt32(dr.GetOrdinal("mes"));
                    nuevoPresupuesto.fecha = DateTime.Parse("01-" + (nuevoPresupuesto.Mes < 10 ? "0" + nuevoPresupuesto.Mes.ToString() : nuevoPresupuesto.Mes.ToString()) + "-" + nuevoPresupuesto.Anio);
                    nuevoPresupuesto.TotalPresupuesto = dr.IsDBNull(dr.GetOrdinal("TOTALPRESUPUESTO")) ? 0 : dr.GetDecimal(dr.GetOrdinal("TOTALPRESUPUESTO"));
                    nuevoPresupuesto.utilidadBruta = dr.IsDBNull(dr.GetOrdinal("up")) ? 0 : dr.GetDouble(dr.GetOrdinal("up"));
                    nuevoPresupuesto.porcubplaneada = dr.IsDBNull(dr.GetOrdinal("up_porc")) ? 0 : dr.GetDouble(dr.GetOrdinal("up_porc"));
                    nuevoPresupuesto.VNRPorc = Convert.ToDouble(dr.IsDBNull(dr.GetOrdinal("MetaVNR")) ? 0 : dr.GetDecimal(dr.GetOrdinal("MetaVNR")));
                    nuevoPresupuesto.FechaInicial = RegistroPresupuesto.FechaInicial;
                    nuevoPresupuesto.fechafinal = RegistroPresupuesto.fechafinal;
                    list_Presupuesto.Add(nuevoPresupuesto);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public void PresupuestoMensualCategoria(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            try
            {
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                CatPresupuesto nuevoPresupuesto;

                string[] Parametros = {
                                         "@Id_Emp",
                                         "@Id_Cd",
                                         "@FechaInicial",
                                         "@fechaFinal"
                };
                object[] Valores = {
                                       RegistroPresupuesto.Id_Emp,
                                       RegistroPresupuesto.Id_Cd,
                                       RegistroPresupuesto.FechaInicial,
                                       RegistroPresupuesto.fechafinal
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                //Encabezado del PAGO
                sqlcmd = CapaDatos.GenerarSqlCommand("SpPresupuestoCategoriaCentral", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    nuevoPresupuesto = new CatPresupuesto();
                    nuevoPresupuesto.Id_Rik = dr.IsDBNull(dr.GetOrdinal("id_rik")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_rik"));
                    nuevoPresupuesto.NombreRik = dr.IsDBNull(dr.GetOrdinal("nombre_rik")) ? "" : dr.GetString(dr.GetOrdinal("nombre_rik"));
                    nuevoPresupuesto.TipoCuenta = dr.IsDBNull(dr.GetOrdinal("Tipocuenta")) ? "" : dr.GetString(dr.GetOrdinal("Tipocuenta"));
                    nuevoPresupuesto.Categoria = dr.IsDBNull(dr.GetOrdinal("categoria")) ? "" : dr.GetString(dr.GetOrdinal("categoria"));
                    nuevoPresupuesto.Anio = dr.IsDBNull(dr.GetOrdinal("anio")) ? 0 : dr.GetInt32(dr.GetOrdinal("anio"));
                    nuevoPresupuesto.Mes = dr.IsDBNull(dr.GetOrdinal("mes")) ? 0 : dr.GetInt32(dr.GetOrdinal("mes"));
                    nuevoPresupuesto.fecha = DateTime.Parse("01-" + (nuevoPresupuesto.Mes < 10 ? "0" + nuevoPresupuesto.Mes.ToString() : nuevoPresupuesto.Mes.ToString()) + "-" + nuevoPresupuesto.Anio);
                    nuevoPresupuesto.TotalPresupuesto = dr.IsDBNull(dr.GetOrdinal("TOTALPRESUPUESTO")) ? 0 : dr.GetDecimal(dr.GetOrdinal("TOTALPRESUPUESTO"));
                    nuevoPresupuesto.up = dr.IsDBNull(dr.GetOrdinal("up")) ? 0 : dr.GetDecimal(dr.GetOrdinal("up"));
                    nuevoPresupuesto.pup = dr.IsDBNull(dr.GetOrdinal("pup")) ? 0 : dr.GetDecimal(dr.GetOrdinal("pup"));
                    nuevoPresupuesto.Id_Cpr = dr.IsDBNull(dr.GetOrdinal("id_cpr")) ? 6 : dr.GetInt32(dr.GetOrdinal("id_cpr"));
                    nuevoPresupuesto.FechaInicial = RegistroPresupuesto.FechaInicial;
                    nuevoPresupuesto.fechafinal = RegistroPresupuesto.fechafinal;
                    list_Presupuesto.Add(nuevoPresupuesto);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ConsultaventanoRentable(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            try
            {
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                CatPresupuesto nuevoPresupuesto;

                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Rik",
                                        "@mesInicial",
                                        "@anioInicial",
                                        "@mesFinal",
                                        "@anioFinal"
                };

                object[] Valores = {
                                       RegistroPresupuesto.Id_Emp,
                                       RegistroPresupuesto.Id_Cd,
                                       RegistroPresupuesto.Id_Rik == -1? (object) null :  RegistroPresupuesto.Id_Rik,
                                       RegistroPresupuesto.MesFinal,
                                       RegistroPresupuesto.AnioInicial,
                                       RegistroPresupuesto.MesFinal,
                                       RegistroPresupuesto.AnioFinal,

                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                //Encabezado del PAGO
                sqlcmd = CapaDatos.GenerarSqlCommand("SP_ReporteVentaNORentablerikCentral", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    nuevoPresupuesto = new CatPresupuesto();
                    nuevoPresupuesto.UtilidadPrima = dr.IsDBNull(dr.GetOrdinal("UtilidadPrima")) ? 0 : dr.GetDouble(dr.GetOrdinal("UtilidadPrima"));
                    nuevoPresupuesto.VNRPor = dr.IsDBNull(dr.GetOrdinal("VentaPorc")) ? 0 : dr.GetDouble(dr.GetOrdinal("VentaPorc"));
                    nuevoPresupuesto.VNR = dr.IsDBNull(dr.GetOrdinal("VentaNORentable")) ? 0 : dr.GetDouble(dr.GetOrdinal("VentaNORentable"));

                    list_Presupuesto.Add(nuevoPresupuesto);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ConsultaUtilidadcategoria(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            try
            {
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                CatPresupuesto nuevoPresupuesto;

                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@mesInicial",
                                        "@anioInicial",
                                        "@mesFinal",
                                        "@anioFinal",
                                        "@Id_u",
                                        "@Id_TerStr",
                                        "@Id_CteStr",
                                        "Id_Rik"

                };

                object[] Valores = {
                                       RegistroPresupuesto.Id_Emp,
                                       RegistroPresupuesto.Id_Cd,
                                       RegistroPresupuesto.MesInicial,
                                       RegistroPresupuesto.AnioInicial,
                                       RegistroPresupuesto.MesFinal,
                                       RegistroPresupuesto.AnioFinal,
                                       RegistroPresupuesto.Id_u == -1?    (object) null :  RegistroPresupuesto.Id_u,
                                       RegistroPresupuesto.id_ter == 0?    (object) null :  RegistroPresupuesto.id_ter.ToString(),
                                       RegistroPresupuesto.id_cte == 0?    (object) null :  RegistroPresupuesto.id_cte.ToString(),
                                       RegistroPresupuesto.Id_Rik == -1?     (object) null :  RegistroPresupuesto.Id_Rik.ToString()
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                //Encabezado del PAGO
                sqlcmd = CapaDatos.GenerarSqlCommand("sp_ReporteVentaCategoriaCentral", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    nuevoPresupuesto = new CatPresupuesto();
                    nuevoPresupuesto.Id_Cd = dr.IsDBNull(dr.GetOrdinal("Id_Cd")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                    nuevoPresupuesto.cantidad = dr.IsDBNull(dr.GetOrdinal("cantidad")) ? 0 : dr.GetDouble(dr.GetOrdinal("cantidad"));
                    nuevoPresupuesto.PrecioDistribuidor = dr.IsDBNull(dr.GetOrdinal("PrecioDistribuidor")) ? 0 : dr.GetDouble(dr.GetOrdinal("PrecioDistribuidor"));
                    nuevoPresupuesto.PrecioLista = dr.IsDBNull(dr.GetOrdinal("PrecioLista")) ? 0 : dr.GetDouble(dr.GetOrdinal("PrecioLista"));
                    nuevoPresupuesto.PrecioVenta = dr.IsDBNull(dr.GetOrdinal("PrecioVenta")) ? 0 : dr.GetDouble(dr.GetOrdinal("PrecioVenta"));
                    nuevoPresupuesto.venta = dr.IsDBNull(dr.GetOrdinal("venta")) ? 0 : dr.GetDouble(dr.GetOrdinal("venta"));
                    nuevoPresupuesto.Costo = dr.IsDBNull(dr.GetOrdinal("Costo")) ? 0 : dr.GetDouble(dr.GetOrdinal("Costo"));
                    nuevoPresupuesto.utilidadBruta = dr.IsDBNull(dr.GetOrdinal("utilidadBruta")) ? 0 : dr.GetDouble(dr.GetOrdinal("utilidadBruta"));
                    nuevoPresupuesto.porcubreal = dr.IsDBNull(dr.GetOrdinal("porcubreal")) ? 0 : dr.GetDouble(dr.GetOrdinal("porcubreal")) / 100;
                    nuevoPresupuesto.porcubplaneada = dr.IsDBNull(dr.GetOrdinal("porcubplaneada")) ? 0 : dr.GetDouble(dr.GetOrdinal("porcubplaneada")) / 100;
                    nuevoPresupuesto.varianzaubrutapuntos = dr.IsDBNull(dr.GetOrdinal("varianzaubrutapuntos")) ? 0 : dr.GetDouble(dr.GetOrdinal("varianzaubrutapuntos"));
                    nuevoPresupuesto.impactopesos = dr.IsDBNull(dr.GetOrdinal("impactopesos")) ? 0 : dr.GetDouble(dr.GetOrdinal("impactopesos"));
                    nuevoPresupuesto.importecostopublico = dr.IsDBNull(dr.GetOrdinal("importecostopublico")) ? 0 : dr.GetDouble(dr.GetOrdinal("importecostopublico"));

                    nuevoPresupuesto.Id_Cpr = dr.IsDBNull(dr.GetOrdinal("Id_Cpr")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cpr"));
                    nuevoPresupuesto.Cpr_Descripcion = dr.IsDBNull(dr.GetOrdinal("Cpr_Descripcion")) ? "" : dr.GetString(dr.GetOrdinal("Cpr_Descripcion"));

                    nuevoPresupuesto.id_matriz = dr.IsDBNull(dr.GetOrdinal("id")) ? 0 : dr.GetInt32(dr.GetOrdinal("id"));
                    nuevoPresupuesto.Matriz = dr.IsDBNull(dr.GetOrdinal("nombre")) ? "" : dr.GetString(dr.GetOrdinal("nombre"));

                    list_Presupuesto.Add(nuevoPresupuesto);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Consultaventanodetalle(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            try
            {
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                CatPresupuesto nuevoPresupuesto;

                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Rik",
                                        "@Id_Ter",
                                        "@Id_Cte",
                                        "@mesInicial",
                                        "@anioInicial",
                                        "@mesFinal",
                                        "@anioFinal"
                };

                object[] Valores = {
                                       RegistroPresupuesto.Id_Emp,
                                       RegistroPresupuesto.Id_Cd,
                                       RegistroPresupuesto.Id_Rik == -1? (object) null :  RegistroPresupuesto.Id_Rik,
                                       RegistroPresupuesto.id_ter == 0? (object) null :  RegistroPresupuesto.id_ter,
                                       RegistroPresupuesto.id_cte == 0? (object) null :  RegistroPresupuesto.id_cte,
                                       RegistroPresupuesto.MesFinal,
                                       RegistroPresupuesto.AnioInicial,
                                       RegistroPresupuesto.MesFinal,
                                       RegistroPresupuesto.AnioFinal,
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                //Encabezado del PAGO
                sqlcmd = CapaDatos.GenerarSqlCommand("SP_ReporteVentaNORentableDetalleCentral", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    nuevoPresupuesto = new CatPresupuesto();
                    nuevoPresupuesto.id_cte = dr.IsDBNull(dr.GetOrdinal("Id_Cte")) ? 0 : Convert.ToInt32(dr.GetDouble(dr.GetOrdinal("Id_Cte")));
                    nuevoPresupuesto.id_ter = dr.IsDBNull(dr.GetOrdinal("Id_Ter")) ? 0 : Convert.ToInt32(dr.GetDouble(dr.GetOrdinal("Id_Ter")));
                    nuevoPresupuesto.VNRPor = dr.IsDBNull(dr.GetOrdinal("VentaPorc")) ? 0 : dr.GetDouble(dr.GetOrdinal("VentaPorc"));
                    nuevoPresupuesto.VNR = dr.IsDBNull(dr.GetOrdinal("VentaNORentable")) ? 0 : dr.GetDouble(dr.GetOrdinal("VentaNORentable"));
                    list_Presupuesto.Add(nuevoPresupuesto);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Consultamatriz(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            try
            {
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                CatPresupuesto nuevoPresupuesto;

                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Cte",
                };

                object[] Valores = {
                                       RegistroPresupuesto.Id_Emp,
                                       RegistroPresupuesto.Id_Cd,
                                       RegistroPresupuesto.id_cte == -1? (object) null :  RegistroPresupuesto.id_cte,
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                //Encabezado del PAGO
                sqlcmd = CapaDatos.GenerarSqlCommand("sp_ConsultaClienteAcuerdoCentral", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    nuevoPresupuesto = new CatPresupuesto();
                    nuevoPresupuesto.id_matriz = dr.IsDBNull(dr.GetOrdinal("Id")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id"));
                    nuevoPresupuesto.Matriz = dr.IsDBNull(dr.GetOrdinal("Nombre")) ? "N/A" : dr.GetString(dr.GetOrdinal("Nombre"));

                    list_Presupuesto.Add(nuevoPresupuesto);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Consultaventanoacysdetalle(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            try
            {
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                CatPresupuesto nuevoPresupuesto;

                string[] Parametros = {
                                        "@Id_Cd",
                                        "@AnioInicial",
                                        "@MesInicial",
                                        "@AnioFinal",
                                        "@MesFinal",
                                        "@Id_Ter",
                                        "@Id_Cte",
                                        "@Id_Prd",
                                        "@Id_Emp",
                                        "@Cpr_Descripcion"
                };

                object[] Valores = {

                                       RegistroPresupuesto.Id_Cd,
                                       RegistroPresupuesto.AnioInicial,
                                       RegistroPresupuesto.MesInicial,
                                       RegistroPresupuesto.AnioFinal,
                                       RegistroPresupuesto.MesFinal,
                                       RegistroPresupuesto.id_ter == 0? (object) null :  RegistroPresupuesto.id_ter,
                                       RegistroPresupuesto.id_cte == 0? (object) null :  RegistroPresupuesto.id_cte,
                                       RegistroPresupuesto.id_prd ==  0? (object) null :  RegistroPresupuesto.id_prd,
                                       RegistroPresupuesto.Id_Emp,
                                       RegistroPresupuesto.Cpr_Descripcion == "COMPRAS LOCALES"? "Compras" :  RegistroPresupuesto.Cpr_Descripcion == "DOSIF / DESP"? "DOSIf" : RegistroPresupuesto.Cpr_Descripcion
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                //Encabezado del PAGO
                sqlcmd = CapaDatos.GenerarSqlCommand("spRepCumplimientoVI_GestionUtilidadCentral", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    nuevoPresupuesto = new CatPresupuesto();
                    nuevoPresupuesto.id_cte = dr.IsDBNull(dr.GetOrdinal("Id_Cte")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cte"));
                    nuevoPresupuesto.id_prd = dr.IsDBNull(dr.GetOrdinal("Id_Prd")) ? 0 : dr.GetInt64(dr.GetOrdinal("Id_Prd"));
                    nuevoPresupuesto.id_ter = dr.IsDBNull(dr.GetOrdinal("id_Ter")) ? 0 : Convert.ToInt32(dr.GetString(dr.GetOrdinal("id_Ter")));
                    nuevoPresupuesto.AcysVenta = dr.IsDBNull(dr.GetOrdinal("Venta")) ? 0 : dr.GetDouble(dr.GetOrdinal("Venta"));
                    nuevoPresupuesto.AcysUP = dr.IsDBNull(dr.GetOrdinal("ubreal")) ? 0 : dr.GetDouble(dr.GetOrdinal("ubreal"));
                    nuevoPresupuesto.Cpr_Descripcion = dr.IsDBNull(dr.GetOrdinal("categoria")) ? "N/A" : dr.GetString(dr.GetOrdinal("categoria"));

                    list_Presupuesto.Add(nuevoPresupuesto);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ConsultaUtilidadProducto(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            try
            {
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                CatPresupuesto nuevoPresupuesto;

                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@mesInicial",
                                        "@anioInicial",
                                        "@mesFinal",
                                        "@anioFinal",
                                        "@Id_u",
                                        "@Id_TerStr",
                                        "@Id_CteStr",
                                        "@Cpr_Descripcion"
                };

                object[] Valores = {
                                       RegistroPresupuesto.Id_Emp,
                                       RegistroPresupuesto.Id_Cd,
                                       RegistroPresupuesto.MesInicial,
                                       RegistroPresupuesto.AnioInicial,
                                       RegistroPresupuesto.MesFinal,
                                       RegistroPresupuesto.AnioFinal,
                                       RegistroPresupuesto.Id_u == -1?    (object) null :  RegistroPresupuesto.Id_u,
                                       RegistroPresupuesto.id_ter == 0?    (object) null :  RegistroPresupuesto.id_ter.ToString(),
                                       RegistroPresupuesto.id_cte == 0?    (object) null :  RegistroPresupuesto.id_cte.ToString(),
                                         RegistroPresupuesto.Cpr_Descripcion == "COMPRAS LOCALES"? "Compras" :  RegistroPresupuesto.Cpr_Descripcion == "DOSIF / DESP"? "DOSIf" : RegistroPresupuesto.Cpr_Descripcion
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                //Encabezado del PAGO
                sqlcmd = CapaDatos.GenerarSqlCommand("sp_ReporteVentaProductoCentral", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    nuevoPresupuesto = new CatPresupuesto();
                    nuevoPresupuesto.Id_Cd = dr.IsDBNull(dr.GetOrdinal("Id_Cd")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                    nuevoPresupuesto.cantidad = dr.IsDBNull(dr.GetOrdinal("cantidad")) ? 0 : dr.GetDouble(dr.GetOrdinal("cantidad"));
                    nuevoPresupuesto.PrecioDistribuidor = dr.IsDBNull(dr.GetOrdinal("PrecioDistribuidor")) ? 0 : dr.GetDouble(dr.GetOrdinal("PrecioDistribuidor"));
                    nuevoPresupuesto.PrecioLista = dr.IsDBNull(dr.GetOrdinal("PrecioLista")) ? 0 : dr.GetDouble(dr.GetOrdinal("PrecioLista"));
                    nuevoPresupuesto.PrecioVenta = dr.IsDBNull(dr.GetOrdinal("PrecioVenta")) ? 0 : dr.GetDouble(dr.GetOrdinal("PrecioVenta"));
                    nuevoPresupuesto.venta = dr.IsDBNull(dr.GetOrdinal("venta")) ? 0 : dr.GetDouble(dr.GetOrdinal("venta"));
                    nuevoPresupuesto.Costo = dr.IsDBNull(dr.GetOrdinal("Costo")) ? 0 : dr.GetDouble(dr.GetOrdinal("Costo"));
                    nuevoPresupuesto.utilidadBruta = dr.IsDBNull(dr.GetOrdinal("utilidadBruta")) ? 0 : dr.GetDouble(dr.GetOrdinal("utilidadBruta"));
                    nuevoPresupuesto.porcubreal = dr.IsDBNull(dr.GetOrdinal("porcubreal")) ? 0 : dr.GetDouble(dr.GetOrdinal("porcubreal")) / 100;
                    nuevoPresupuesto.porcubplaneada = dr.IsDBNull(dr.GetOrdinal("porcubplaneada")) ? 0 : dr.GetDouble(dr.GetOrdinal("porcubplaneada")) / 100;
                    nuevoPresupuesto.varianzaubrutapuntos = dr.IsDBNull(dr.GetOrdinal("varianzaubrutapuntos")) ? 0 : dr.GetDouble(dr.GetOrdinal("varianzaubrutapuntos"));
                    nuevoPresupuesto.impactopesos = dr.IsDBNull(dr.GetOrdinal("impactopesos")) ? 0 : dr.GetDouble(dr.GetOrdinal("impactopesos"));
                    nuevoPresupuesto.importecostopublico = dr.IsDBNull(dr.GetOrdinal("importecostopublico")) ? 0 : dr.GetDouble(dr.GetOrdinal("importecostopublico"));

                    nuevoPresupuesto.id_prd = dr.IsDBNull(dr.GetOrdinal("id_prd")) ? 0 : dr.GetInt64(dr.GetOrdinal("id_prd"));
                    nuevoPresupuesto.prd_nombre = dr.IsDBNull(dr.GetOrdinal("prd_nombre")) ? "" : dr.GetString(dr.GetOrdinal("prd_nombre"));

                    list_Presupuesto.Add(nuevoPresupuesto);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaUtilidadCliente(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            try
            {
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                CatPresupuesto nuevoPresupuesto;

                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@mesInicial",
                                        "@anioInicial",
                                        "@mesFinal",
                                        "@anioFinal",
                                        "@Id_u",
                                        "@Terr",
                                        "@id_Cte"

                };

                object[] Valores = {
                                       RegistroPresupuesto.Id_Emp,
                                       RegistroPresupuesto.Id_Cd,
                                       RegistroPresupuesto.MesInicial,
                                       RegistroPresupuesto.AnioInicial,
                                       RegistroPresupuesto.MesFinal,
                                       RegistroPresupuesto.AnioFinal,
                                       RegistroPresupuesto.Id_u == -1?    (object) null :  RegistroPresupuesto.Id_u,
                                       RegistroPresupuesto.id_ter  == -1?    (object) null :  RegistroPresupuesto.id_ter,
                                       RegistroPresupuesto.id_cte  == -1?    (object) null :  RegistroPresupuesto.id_cte,

            };

                SqlCommand sqlcmd = default(SqlCommand);

                //Encabezado del PAGO
                sqlcmd = CapaDatos.GenerarSqlCommand("sp_ReportePresupuetoCte", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    nuevoPresupuesto = new CatPresupuesto();
                    nuevoPresupuesto.Id_Emp = dr.IsDBNull(dr.GetOrdinal("Id_Emp")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Emp"));
                    nuevoPresupuesto.Id_Cd = dr.IsDBNull(dr.GetOrdinal("Id_Cd")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                    nuevoPresupuesto.id_cte = dr.IsDBNull(dr.GetOrdinal("id_cte")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_cte"));
                    nuevoPresupuesto.cte_nomcomercial = dr.IsDBNull(dr.GetOrdinal("cte_nomcomercial")) ? "" : dr.GetString(dr.GetOrdinal("cte_nomcomercial"));
                    nuevoPresupuesto.id_ter = dr.IsDBNull(dr.GetOrdinal("id_ter")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_ter"));
                    nuevoPresupuesto.ter_nombre = dr.IsDBNull(dr.GetOrdinal("ter_nombre")) ? "" : dr.GetString(dr.GetOrdinal("ter_nombre"));
                    nuevoPresupuesto.cantidad = dr.IsDBNull(dr.GetOrdinal("cantidad")) ? 0 : dr.GetDouble(dr.GetOrdinal("cantidad"));
                    nuevoPresupuesto.PrecioDistribuidor = dr.IsDBNull(dr.GetOrdinal("PrecioDistribuidor")) ? 0 : dr.GetDouble(dr.GetOrdinal("PrecioDistribuidor"));
                    nuevoPresupuesto.PrecioLista = dr.IsDBNull(dr.GetOrdinal("PrecioLista")) ? 0 : dr.GetDouble(dr.GetOrdinal("PrecioLista"));
                    nuevoPresupuesto.PrecioVenta = dr.IsDBNull(dr.GetOrdinal("PrecioVenta")) ? 0 : dr.GetDouble(dr.GetOrdinal("PrecioVenta"));
                    nuevoPresupuesto.venta = dr.IsDBNull(dr.GetOrdinal("venta")) ? 0 : dr.GetDouble(dr.GetOrdinal("venta"));
                    nuevoPresupuesto.Costo = dr.IsDBNull(dr.GetOrdinal("Costo")) ? 0 : dr.GetDouble(dr.GetOrdinal("Costo"));
                    nuevoPresupuesto.utilidadBruta = dr.IsDBNull(dr.GetOrdinal("utilidadBruta")) ? 0 : dr.GetDouble(dr.GetOrdinal("utilidadBruta"));
                    nuevoPresupuesto.porcubreal = dr.IsDBNull(dr.GetOrdinal("porcubreal")) ? 0 : dr.GetDouble(dr.GetOrdinal("porcubreal"));
                    nuevoPresupuesto.porcubplaneada = dr.IsDBNull(dr.GetOrdinal("porcubplaneada")) ? 0 : dr.GetDouble(dr.GetOrdinal("porcubplaneada"));
                    nuevoPresupuesto.varianzaubrutapuntos = dr.IsDBNull(dr.GetOrdinal("varianzaubrutapuntos")) ? 0 : dr.GetDouble(dr.GetOrdinal("varianzaubrutapuntos"));
                    nuevoPresupuesto.impactopesos = dr.IsDBNull(dr.GetOrdinal("impactopesos")) ? 0 : dr.GetDouble(dr.GetOrdinal("impactopesos"));
                    nuevoPresupuesto.importecostopublico = dr.IsDBNull(dr.GetOrdinal("importecostopublico")) ? 0 : dr.GetDouble(dr.GetOrdinal("importecostopublico"));
                    nuevoPresupuesto.Mes = dr.IsDBNull(dr.GetOrdinal("mes")) ? 0 : dr.GetInt32(dr.GetOrdinal("mes"));
                    nuevoPresupuesto.Anio = dr.IsDBNull(dr.GetOrdinal("anio")) ? 0 : dr.GetInt32(dr.GetOrdinal("anio"));
                    nuevoPresupuesto.fecha = DateTime.Parse("01-" + (nuevoPresupuesto.Mes < 10 ? "0" + nuevoPresupuesto.Mes.ToString() : nuevoPresupuesto.Mes.ToString()) + "-" + nuevoPresupuesto.Anio);
                    nuevoPresupuesto.Id_Rik = dr.IsDBNull(dr.GetOrdinal("id_rik")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_rik"));
                    nuevoPresupuesto.FechaInicial = RegistroPresupuesto.FechaInicial;
                    nuevoPresupuesto.fechafinal = RegistroPresupuesto.fechafinal;
                    list_Presupuesto.Add(nuevoPresupuesto);
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