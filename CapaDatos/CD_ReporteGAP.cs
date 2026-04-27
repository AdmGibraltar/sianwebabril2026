using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using CapaEntidad;


namespace CapaDatos
{
    public class CD_ReporteGAP
    {
        public void ConsultaListaReporteGAP(ReporteGAP reporteGAP, ref List<ReporteGAP> listreporteGAP, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Zona",
                                        "@Id_Cd",
                                        "@Id_Cte",
                                        "@Id_Tamaño",
                                        "@Id_Prd",
                                        "@IdEstatus",
                                        "@Id_Rik",
                                        "@Id_TipoReporte",
                                        "@Mes",
                                        "@Año" };
                object[] Valores = {
                                       reporteGAP.Id_Emp,
                                       reporteGAP.Id_Zona,
                                       reporteGAP.Id_Cd,
                                       reporteGAP.Id_Cte,
                                       reporteGAP.Id_Tamaño,
                                       reporteGAP.Id_Prd,
                                       reporteGAP.Activo,
                                       reporteGAP.Id_Rik,
                                       reporteGAP.Id_TipoReporte,
                                       reporteGAP.Mes,
                                       reporteGAP.Año
                                   };
                //      leads.IdGiroEmpresa,

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spReporteGAP_ConsultaLista", ref dr, Parametros, Valores);

                ReporteGAP c;
                while (dr.Read())
                {
                    c = new ReporteGAP();
                    c.Id_Emp = Convert.ToInt32(dr["Id_Emp"]);
                    c.Id_Zona = Convert.ToInt32(dr["Id_Zona"]);
                    c.Id_Cd = Convert.ToInt32(dr["Id_Cd"]);
                    c.NomCDI = dr["NomCDI"].ToString().Trim();
                    c.NomZona = dr["NomZona"].ToString().Trim();
                    c.IdReporteGAP = Convert.ToInt32(dr["IdReporteGAP"]);

                    c.Id_Rik = dr["Id_Rik"].ToString() == "" ? 0 : Convert.ToInt32(dr["Id_Rik"]);
                    c.Nombre_Rik = dr["Nombre_Rik"].ToString() == "" ? "" : Convert.ToString(dr["Nombre_Rik"]);

                    c.Activo = Convert.ToInt32(dr["Activo"]);
                    c.Id_Cte = Convert.ToInt32(dr["Id_Cte"]);
                    c.Cte_NomComercial = dr["Cte_NomComercial"].ToString().Trim();
                    c.Id_Tamaño = dr["Id_Tamaño"].ToString().Trim();
                    c.Id_Categoria = Convert.ToInt32(dr["Id_Categoria"]);
                    c.NomCategoria = dr["NomCategoria"].ToString().Trim();
                    c.Id_Prd = Convert.ToInt64(dr["Id_Prd"]);
                    c.NomProducto = dr["NomProducto"].ToString().Trim();
                    c.Unidades = Convert.ToInt32(dr["Unidades"]);
                    c.Ventas = dr["Ventas"].ToString() == "" ? 0 : Convert.ToDouble(dr["Ventas"]);
                    c.VentasPO = dr["VentasPO"].ToString() == "" ? 0 : Convert.ToDouble(dr["VentasPO"]);

                    c.GAPIngresos_Monto = dr["GAPIngresos_Monto"].ToString() == "" ? 0 : Convert.ToDouble(dr["GAPIngresos_Monto"]);
                    c.GAPIngresos_Porc = dr["GAPIngresos_Porc"].ToString() == "" ? 0 : Convert.ToDouble(dr["GAPIngresos_Porc"]);
                    c.POvsPV = dr["POvsPV"].ToString() == "" ? 0 : Convert.ToDouble(dr["POvsPV"]);

                    c.PrecioVenta = dr["PrecioVenta"].ToString() == "" ? 0 : Convert.ToDouble(dr["PrecioVenta"]);
                    c.PrecioObjetivo = dr["PrecioObjetivo"].ToString() == "" ? 0 : Convert.ToDouble(dr["PrecioObjetivo"]);
                    c.MgRed_Monto = dr["MgRed_Monto"].ToString() == "" ? 0 : Convert.ToDouble(dr["MgRed_Monto"]);
                    c.MgRedPO_Monto = dr["MgRedPO_Monto"].ToString() == "" ? 0 : Convert.ToDouble(dr["MgRedPO_Monto"]);
                    c.MgRed_Porc = dr["MgRed_Porc"].ToString() == "" ? 0 : Convert.ToDouble(dr["MgRed_Porc"]);

                    c.MgRedPO_Porc = dr["MgRedPO_Porc"].ToString() == "" ? 0 : Convert.ToDouble(dr["MgRedPO_Porc"]);
                    c.GAPMgRed_Monto = dr["GAPMgRed_Monto"].ToString() == "" ? 0 : Convert.ToDouble(dr["GAPMgRed_Monto"]);
                    c.GAPMgRed_Porc = dr["GAPMgRed_Porc"].ToString() == "" ? 0 : Convert.ToDouble(dr["GAPMgRed_Porc"]);

                    c.IdUsuario = dr["IdUsuario"].ToString() == "" ? 0 : Convert.ToInt32(dr["IdUsuario"]);
                    c.Mes = dr["Mes"].ToString() == "" ? 0 : Convert.ToInt32(dr["Mes"]);
                    c.Año = dr["Año"].ToString() == "" ? 0 : Convert.ToInt32(dr["Año"]);
                    c.NomEstatus = dr["NomEstatus"].ToString() == "" ? "" : Convert.ToString(dr["NomEstatus"]);
                    if (!dr.IsDBNull(dr.GetOrdinal("FechaAlta")))
                    {
                        c.FechaAlta = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaAlta")));
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("FechaUltMod")))
                    {
                        c.FechaUltMod = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaUltMod")));
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("FechaInicial")))
                    {
                        c.FechaInicial = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaInicial")));
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("FechaFinal")))
                    {
                        c.FechaFinal = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaFinal")));
                    }




                    listreporteGAP.Add(c);

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ConsultaListaReporteGAPRiks(ReporteGAP reporteGAP, ref List<ReporteGAP> listreporteGAP, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Zona",
                                        "@Id_Cd",
                                        "@Id_Cte",
                                        "@Id_Tamaño",
                                        "@Id_Prd",
                                        "@IdEstatus",
                                        "@Id_Rik",
                                        "@Id_TipoReporte",
                                        "@Mes",
                                        "@Año" };
                object[] Valores = {
                                       reporteGAP.Id_Emp,
                                       reporteGAP.Id_Zona,
                                       reporteGAP.Id_Cd,
                                       reporteGAP.Id_Cte,
                                       reporteGAP.Id_Tamaño,
                                       reporteGAP.Id_Prd,
                                       reporteGAP.Activo,
                                       reporteGAP.Id_Rik,
                                       1,
                                       reporteGAP.Mes,
                                       reporteGAP.Año
                                   };
                //      leads.IdGiroEmpresa,

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spReporteGAP_ConsultaLista", ref dr, Parametros, Valores);

                ReporteGAP c;
                while (dr.Read())
                {
                    c = new ReporteGAP();
                    c.Id_Emp = Convert.ToInt32(dr["Id_Emp"]);
                    c.Id_Zona = Convert.ToInt32(dr["Id_Zona"]);
                    c.Id_Cd = Convert.ToInt32(dr["Id_Cd"]);
                    c.NomCDI = dr["NomCDI"].ToString().Trim();
                    c.NomZona = dr["NomZona"].ToString().Trim();
                    c.IdReporteGAP = Convert.ToInt32(dr["IdReporteGAP"]);

                    c.Id_Rik = dr["Id_Rik"].ToString() == "" ? 0 : Convert.ToInt32(dr["Id_Rik"]);
                    c.Nombre_Rik = dr["Nombre_Rik"].ToString() == "" ? "" : Convert.ToString(dr["Nombre_Rik"]);

                    c.Activo = Convert.ToInt32(dr["Activo"]);


                    c.Unidades = Convert.ToInt32(dr["Unidades"]);
                    c.Ventas = dr["Ventas"].ToString() == "" ? 0 : Convert.ToDouble(dr["Ventas"]);
                    c.VentasPO = dr["VentasPO"].ToString() == "" ? 0 : Convert.ToDouble(dr["VentasPO"]);

                    c.GAPIngresos_Monto = dr["GAPIngresos_Monto"].ToString() == "" ? 0 : Convert.ToDouble(dr["GAPIngresos_Monto"]);
                    c.GAPIngresos_Porc = dr["GAPIngresos_Porc"].ToString() == "" ? 0 : Convert.ToDouble(dr["GAPIngresos_Porc"]);
                    c.POvsPV = dr["POvsPV"].ToString() == "" ? 0 : Convert.ToDouble(dr["POvsPV"]);
                    c.MgRed_Monto = dr["MgRed_Monto"].ToString() == "" ? 0 : Convert.ToDouble(dr["MgRed_Monto"]);
                    c.MgRedPO_Monto = dr["MgRedPO_Monto"].ToString() == "" ? 0 : Convert.ToDouble(dr["MgRedPO_Monto"]);
                    c.GAPMgRed_Monto = dr["GAPMgRed_Monto"].ToString() == "" ? 0 : Convert.ToDouble(dr["GAPMgRed_Monto"]);
                    c.GAPMgRed_Porc = dr["GAPMgRed_Porc"].ToString() == "" ? 0 : Convert.ToDouble(dr["GAPMgRed_Porc"]);
                    c.MgRedPO_Porc = dr["MgRedPO_Porc"].ToString() == "" ? 0 : Convert.ToDouble(dr["MgRedPO_Porc"]);


                    c.MgRed_Porc = dr["MgRed_Porc"].ToString() == "" ? 0 : Convert.ToDouble(dr["MgRed_Porc"]);





                    c.Mes = dr["Mes"].ToString() == "" ? 0 : Convert.ToInt32(dr["Mes"]);
                    c.Año = dr["Año"].ToString() == "" ? 0 : Convert.ToInt32(dr["Año"]);
                    c.NomEstatus = dr["NomEstatus"].ToString() == "" ? "" : Convert.ToString(dr["NomEstatus"]);




                    listreporteGAP.Add(c);

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ConsultaListaReporteGAPRik(ReporteGAP reporteGAP, ref List<ReporteGAP> listreporteGAP, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Zona",
                                        "@Id_Cd",
                                        "@Id_Cte",
                                        "@Id_Tamaño",
                                        "@Id_Prd",
                                        "@IdEstatus",
                                        "@Id_Rik",
                                        "@Id_TipoReporte",
                                        "@Mes",
                                        "@Año" };
                object[] Valores = {
                                       reporteGAP.Id_Emp,
                                       reporteGAP.Id_Zona,
                                       reporteGAP.Id_Cd,
                                       reporteGAP.Id_Cte,
                                       reporteGAP.Id_Tamaño,
                                       reporteGAP.Id_Prd,
                                       reporteGAP.Activo,
                                       reporteGAP.Id_Rik,
                                       2,
                                       reporteGAP.Mes,
                                       reporteGAP.Año
                                   };
                //      leads.IdGiroEmpresa,

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spReporteGAP_ConsultaLista", ref dr, Parametros, Valores);

                ReporteGAP c;
                while (dr.Read())
                {
                    c = new ReporteGAP();
                    c.Id_Emp = Convert.ToInt32(dr["Id_Emp"]);
                    c.Id_Zona = Convert.ToInt32(dr["Id_Zona"]);
                    c.Id_Cd = Convert.ToInt32(dr["Id_Cd"]);
                    c.NomCDI = dr["NomCDI"].ToString().Trim();
                    c.NomZona = dr["NomZona"].ToString().Trim();
                    c.IdReporteGAP = Convert.ToInt32(dr["IdReporteGAP"]);

                    c.Id_Rik = dr["Id_Rik"].ToString() == "" ? 0 : Convert.ToInt32(dr["Id_Rik"]);
                    c.Nombre_Rik = dr["Nombre_Rik"].ToString() == "" ? "" : Convert.ToString(dr["Nombre_Rik"]);

                    c.Activo = Convert.ToInt32(dr["Activo"]);
                    c.Id_Cte = dr["Id_Cte"].ToString() == "" ? 0 : Convert.ToInt32(dr["Id_Cte"]);
                    c.Cte_NomComercial = dr["Cte_NomComercial"].ToString() == "" ? "" : Convert.ToString(dr["Cte_NomComercial"]);
                    c.Id_Tamaño = dr["Id_Tamaño"].ToString() == "" ? "" : Convert.ToString(dr["Id_Tamaño"]);



                    c.Unidades = Convert.ToInt32(dr["Unidades"]);
                    c.Ventas = dr["Ventas"].ToString() == "" ? 0 : Convert.ToDouble(dr["Ventas"]);
                    c.VentasPO = dr["VentasPO"].ToString() == "" ? 0 : Convert.ToDouble(dr["VentasPO"]);

                    c.GAPIngresos_Monto = dr["GAPIngresos_Monto"].ToString() == "" ? 0 : Convert.ToDouble(dr["GAPIngresos_Monto"]);
                    c.GAPIngresos_Porc = dr["GAPIngresos_Porc"].ToString() == "" ? 0 : Convert.ToDouble(dr["GAPIngresos_Porc"]);
                    c.POvsPV = dr["POvsPV"].ToString() == "" ? 0 : Convert.ToDouble(dr["POvsPV"]);
                    c.MgRed_Monto = dr["MgRed_Monto"].ToString() == "" ? 0 : Convert.ToDouble(dr["MgRed_Monto"]);
                    c.MgRedPO_Monto = dr["MgRedPO_Monto"].ToString() == "" ? 0 : Convert.ToDouble(dr["MgRedPO_Monto"]);
                    c.GAPMgRed_Monto = dr["GAPMgRed_Monto"].ToString() == "" ? 0 : Convert.ToDouble(dr["GAPMgRed_Monto"]);
                    c.GAPMgRed_Porc = dr["GAPMgRed_Porc"].ToString() == "" ? 0 : Convert.ToDouble(dr["GAPMgRed_Porc"]);
                    c.MgRedPO_Porc = dr["MgRedPO_Porc"].ToString() == "" ? 0 : Convert.ToDouble(dr["MgRedPO_Porc"]);


                    c.MgRed_Porc = dr["MgRed_Porc"].ToString() == "" ? 0 : Convert.ToDouble(dr["MgRed_Porc"]);





                    c.Mes = dr["Mes"].ToString() == "" ? 0 : Convert.ToInt32(dr["Mes"]);
                    c.Año = dr["Año"].ToString() == "" ? 0 : Convert.ToInt32(dr["Año"]);
                    c.NomEstatus = dr["NomEstatus"].ToString() == "" ? "" : Convert.ToString(dr["NomEstatus"]);
                    c.TipoCuenta = dr["TipoCuenta"].ToString() == "" ? "" : Convert.ToString(dr["TipoCuenta"]);

                    listreporteGAP.Add(c);

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ConsultaLeads(int Id_Leads, ref Leads leads, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;
                string[] Parametros = { "@IdLeads" };
                object[] Valores = { Id_Leads };

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spLeads_Consulta", ref dr, Parametros, Valores);

                if (dr.Read())
                {

                    leads.IdLeads = Convert.ToInt32(dr["IdLeads"]);
                    leads.NombreEmpresa = dr["NombreEmpresa"].ToString();
                    leads.IdGiroEmpresa = Convert.ToInt32(dr["IdGiroEmpresa"]);
                    leads.ProductoInteres = dr["ProductoInteres"].ToString();
                    leads.Id_Cd = Convert.ToInt32(dr["Id_Cd"]);
                    leads.FechaAlta = Convert.ToDateTime(dr["FechaAlta"]);
                    leads.FechaUltMod = Convert.ToDateTime(dr["FechaUltMod"]);
                    leads.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
                    leads.NomCDI = dr["NomCDI"].ToString();
                    //leads.IdRepresentante = Convert.ToInt32(dr["IdRepresentante"]);
                    leads.FechaAsignaRep = dr["FechaAsignaRep"].ToString() == "" ? (DateTime?)null : Convert.ToDateTime(dr["FechaAsignaRep"]);
                    leads.Activo = Convert.ToInt32(dr["Activo"]);
                    leads.IdMedioComunicacion = Convert.ToInt32(dr["IdMedioComunicacion"]);
                    leads.Ciudad = dr["Ciudad"].ToString();
                    leads.Comentarios = dr["Comentarios"].ToString() == "" ? "" : Convert.ToString(dr["Comentarios"]);
                    leads.Telefono = dr["Telefono"].ToString();
                    leads.Correo = dr["Correo"].ToString();
                    leads.NombreContacto = dr["NombreContacto"].ToString();

                }

                cd_datos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public void LlenaCombo(string Conexion, string sp, ref List<Comun> Lista)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { };

                object[] Valores = { };

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

        public void ConsultaLeadsObservarTotales(Leads RegistroPresupuesto, ref List<Leads> list_Presupuesto, string Conexion)
        {
            try
            {
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                Leads nuevoPresupuesto;

                string[] Parametros = {
                                         "@Id_Emp",
                                         "@Id_Tipo",
                                         "@FechaInicial",
                                         "@fechaFinal"
                };
                object[] Valores = {
                                       RegistroPresupuesto.Id_Emp,
                                       RegistroPresupuesto.TipoFiltro,
                                       RegistroPresupuesto.FechaInicial,
                                       RegistroPresupuesto.FechaFinal
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                //Encabezado del PAGO
                sqlcmd = CapaDatos.GenerarSqlCommand("SpConsultarLeadsReporteComercial", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    nuevoPresupuesto = new Leads();
                    nuevoPresupuesto.Id = dr.IsDBNull(dr.GetOrdinal("Id")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id"));
                    nuevoPresupuesto.Descripcion = dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? "" : dr.GetString(dr.GetOrdinal("Descripcion"));
                    nuevoPresupuesto.ValorCantidad = dr.IsDBNull(dr.GetOrdinal("Valor")) ? 0 : dr.GetInt32(dr.GetOrdinal("Valor"));
                    list_Presupuesto.Add(nuevoPresupuesto);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ConsultaLeadsIntegrarResultados(Leads RegistroPresupuesto, ref List<Leads> list_Presupuesto, string Conexion)
        {
            try
            {
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                Leads nuevoPresupuesto;

                string[] Parametros = {
                                         "@Id_Emp",
                                         "@Id_Tipo",
                                         "@Id_Cd",
                                         "@FechaInicial",
                                         "@fechaFinal"
                };
                object[] Valores = {
                                       RegistroPresupuesto.Id_Emp,
                                       RegistroPresupuesto.Id_Cd,
                                       RegistroPresupuesto.TipoFiltro,
                                       RegistroPresupuesto.FechaInicial,
                                       RegistroPresupuesto.FechaFinal
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                //Encabezado del PAGO
                sqlcmd = CapaDatos.GenerarSqlCommand("SpConsultarLeadsIntegrarResultados", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    nuevoPresupuesto = new Leads();
                    nuevoPresupuesto.Id = dr.IsDBNull(dr.GetOrdinal("Id")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id"));
                    nuevoPresupuesto.Descripcion = dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? "" : dr.GetString(dr.GetOrdinal("Descripcion"));
                    if (RegistroPresupuesto.TipoFiltro == 1)
                    {
                        nuevoPresupuesto.ValorMonto = 0;
                        nuevoPresupuesto.ValorCantidad = dr.IsDBNull(dr.GetOrdinal("Valor")) ? 0 : dr.GetInt32(dr.GetOrdinal("Valor"));
                    }
                    else
                    {
                        nuevoPresupuesto.ValorMonto = dr.IsDBNull(dr.GetOrdinal("Valor")) ? 0 : dr.GetInt32(dr.GetOrdinal("Valor"));
                        nuevoPresupuesto.ValorCantidad = 0;
                    }

                    list_Presupuesto.Add(nuevoPresupuesto);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ConsultarProyectosAsig(CatPresupuesto RegistroPresupuesto, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            try
            {
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                CatPresupuesto nuevoPresupuesto;

                string[] Parametros = {
                                         "@Id_Emp",
                                         "@Id_Cd"
                };
                object[] Valores = {
                                       RegistroPresupuesto.Id_Emp,
                                       RegistroPresupuesto.Id_Cd
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                sqlcmd = CapaDatos.GenerarSqlCommand("SpLeadsConsultarProyectosAsig", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    nuevoPresupuesto = new CatPresupuesto();
                    nuevoPresupuesto.Id_Rik = dr.IsDBNull(dr.GetOrdinal("id_rik")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_rik"));
                    nuevoPresupuesto.NombreRik = dr.IsDBNull(dr.GetOrdinal("nombre_rik")) ? "" : dr.GetString(dr.GetOrdinal("nombre_rik"));
                    nuevoPresupuesto.cantidad = dr.IsDBNull(dr.GetOrdinal("TOTALPRESUPUESTO")) ? 0 : dr.GetInt32(dr.GetOrdinal("TOTALPRESUPUESTO"));
                    list_Presupuesto.Add(nuevoPresupuesto);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //Gráfica de Integrar Leads 

        public void ConsultaIntegrarLeads(CatPresupuesto RegistroPresupuesto, int seleccion, ref List<CatPresupuesto> list_Presupuesto, string Conexion)
        {
            try
            {
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                CatPresupuesto nuevoPresupuesto;

                string[] Parametros = {
                                         "@Id_Emp",
                                         "@Id_Cd",
                                         "@Id_Seleccion",
                                         "@FechaInicial",
                                         "@fechaFinal"
                };
                object[] Valores = {
                                       RegistroPresupuesto.Id_Emp,
                                       RegistroPresupuesto.Id_Cd,
                                       seleccion,
                                       RegistroPresupuesto.FechaInicial,
                                       RegistroPresupuesto.fechafinal
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                //Encabezado del PAGO
                sqlcmd = CapaDatos.GenerarSqlCommand("SpConsultarLeadsIntegrar", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    nuevoPresupuesto = new CatPresupuesto();
                    nuevoPresupuesto.Id_Rik = dr.IsDBNull(dr.GetOrdinal("id_rik")) ? 0 : dr.GetInt32(dr.GetOrdinal("id_rik"));
                    nuevoPresupuesto.NombreRik = dr.IsDBNull(dr.GetOrdinal("nombre_rik")) ? "" : dr.GetString(dr.GetOrdinal("nombre_rik"));
                    nuevoPresupuesto.Anio = dr.IsDBNull(dr.GetOrdinal("anio")) ? 0 : dr.GetInt32(dr.GetOrdinal("anio"));
                    nuevoPresupuesto.Mes = dr.IsDBNull(dr.GetOrdinal("mes")) ? 0 : dr.GetInt32(dr.GetOrdinal("mes"));
                    nuevoPresupuesto.fecha = DateTime.Parse("01-" + (nuevoPresupuesto.Mes < 10 ? "0" + nuevoPresupuesto.Mes.ToString() : nuevoPresupuesto.Mes.ToString()) + "-" + nuevoPresupuesto.Anio);
                    if (seleccion == 0)
                    {
                        nuevoPresupuesto.cantidad = dr.IsDBNull(dr.GetOrdinal("TOTALPRESUPUESTO")) ? 0 : dr.GetInt32(dr.GetOrdinal("TOTALPRESUPUESTO"));
                        nuevoPresupuesto.TotalPresupuesto = Convert.ToDecimal(nuevoPresupuesto.cantidad);
                        nuevoPresupuesto.venta = 0;
                    }
                    else
                    {
                        nuevoPresupuesto.cantidad = 0; //dr.IsDBNull(dr.GetOrdinal("TOTALPRESUPUESTO")) ? 0 : dr.GetInt32(dr.GetOrdinal("TOTALPRESUPUESTO"));
                        nuevoPresupuesto.venta = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("TOTALPRESUPUESTO"))) ? -1 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("TOTALPRESUPUESTO")));
                        nuevoPresupuesto.TotalPresupuesto = dr.IsDBNull(dr.GetOrdinal("TOTALPRESUPUESTO")) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("TOTALPRESUPUESTO")));

                    }



                    list_Presupuesto.Add(nuevoPresupuesto);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        //reporte para gráfica de Efectividad ( barra de PIE ) 
        public void ConsultaReporteGraficaCantidad(ReporteCRM RegistroReporteCRM, ref List<ReporteCRM> listaReporteCRM, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {  "@Id_Emp",
                                         "@Id_Cd",
                                         "@Id_Rik",
                                         "@TipoVenta",
                                         "@FECHA_INICIAL",
                                         "@FECHA_FINAL",
                                         "@Totalizar"};
                object[] Valores = {   RegistroReporteCRM.Id_Emp,
                                       RegistroReporteCRM.Id_Cd,
                                       RegistroReporteCRM.Id_Rik,
                                       RegistroReporteCRM.TipoVenta,
                                       RegistroReporteCRM.fechainicio,
                                       RegistroReporteCRM.fechafinal,
                                       RegistroReporteCRM.id_proyect //ESTE ES EL VALOR DE TOTALIZAR 1 SI TOTALIZA 0 NO LO HACE
    };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRM_ReporteLeadsCENTRAL_GraficaCantidad", ref dr, Parametros, Valores);
                if (dr.Read())
                {

                    RegistroReporteCRM = new ReporteCRM();

                    RegistroReporteCRM.Nombre = "En Desarrollo";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("endesarrollo"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("endesarrollo")));
                    listaReporteCRM.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();

                    RegistroReporteCRM.Nombre = "En Cierre";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("cierre"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("cierre")));
                    listaReporteCRM.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();
                    RegistroReporteCRM.Nombre = "Cancelados";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cancelados"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Cancelados")));
                    listaReporteCRM.Add(RegistroReporteCRM);

                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //reporte para gráfica de Tiempo de respuesta ( barra de PIE ) 
        public void ReporteLeads_TiempoRespuesta(ReporteCRM RegistroReporteCRM, ref List<ReporteCRM> listaReporteCRM, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {  "@Id_Emp",
                                         "@Id_Cd",
                                         "@Id_Rik",
                                         "@TipoVenta",
                                         "@FECHA_INICIAL",
                                         "@FECHA_FINAL"};
                object[] Valores = {   RegistroReporteCRM.Id_Emp,
                                       RegistroReporteCRM.Id_Cd,
                                       RegistroReporteCRM.Id_Rik,
                                       RegistroReporteCRM.TipoVenta,
                                       RegistroReporteCRM.fechainicio,
                                       RegistroReporteCRM.fechafinal
    };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spReporteLeads_TiempoRespuesta", ref dr, Parametros, Valores);
                if (dr.Read())
                {

                    RegistroReporteCRM = new ReporteCRM();

                    RegistroReporteCRM.Nombre = "Tiempo Estandard";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("tiempoestandard"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("tiempoestandard")));
                    listaReporteCRM.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();

                    RegistroReporteCRM.Nombre = "Tiempo Límite";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("tiempolimite"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("tiempolimite")));
                    listaReporteCRM.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();
                    RegistroReporteCRM.Nombre = "Fuera de Tiempo";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("fueradetiempo"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("fueradetiempo")));
                    listaReporteCRM.Add(RegistroReporteCRM);

                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ReporteLeads_TiempoRespuestaBarra(ReporteCRM RegistroReporteCRM, ref List<ReporteCRM> listaReporteTiempoEstandar, ref List<ReporteCRM> listaReporteTiempoLimite,
         ref List<ReporteCRM> listaReporteFuera, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {  "@Id_Emp",
                                         "@Id_Cd",
                                         "@Id_Rik",
                                         "@TipoVenta",
                                         "@FECHA_INICIAL",
                                         "@FECHA_FINAL"};
                object[] Valores = {   RegistroReporteCRM.Id_Emp,
                                       RegistroReporteCRM.Id_Cd,
                                       RegistroReporteCRM.Id_Rik,
                                       RegistroReporteCRM.TipoVenta,
                                        RegistroReporteCRM.fechainicio,
                                       RegistroReporteCRM.fechafinal
    };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spReporteLeads_TiempoRespuesta", ref dr, Parametros, Valores);
                if (dr.Read())
                {

                    RegistroReporteCRM = new ReporteCRM();

                    RegistroReporteCRM.Nombre = "Tiempo Estandard";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("tiempoestandard"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("tiempoestandard")));
                    listaReporteTiempoEstandar.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();

                    RegistroReporteCRM.Nombre = "Tiempo Límite";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("tiempolimite"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("tiempolimite")));
                    listaReporteTiempoLimite.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();

                    RegistroReporteCRM.Nombre = "Fuera de Tiempo";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("fueradetiempo"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("fueradetiempo")));
                    listaReporteFuera.Add(RegistroReporteCRM);


                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaGerente(Usuario Usuario, string Conexion, ref List<Usuario> list)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_U", "@Id_TU" };
                object[] Valores = { Usuario.Id_Emp, Usuario.Id_Cd, Usuario.Id_Id_U, Usuario.Id_TU };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("SpLEadsUsuariosCDI_Consulta", ref dr, Parametros, Valores);

                Usuario VarUsuario = default(Usuario);
                while (dr.Read())
                {
                    VarUsuario = new Usuario();
                    VarUsuario.Id_U = (int)dr.GetValue(dr.GetOrdinal("Id_U"));
                    VarUsuario.U_Nombre = (string)dr.GetValue(dr.GetOrdinal("U_Nombre"));
                    VarUsuario.U_Correo = (string)dr.GetValue(dr.GetOrdinal("U_Correo"));

                    if (Usuario.Id_TU != 8)
                    {
                        VarUsuario.U_FNac = dr.IsDBNull(dr.GetOrdinal("U_FNac")) ? Convert.ToDateTime("01/01/0001") : (DateTime)dr.GetValue(dr.GetOrdinal("U_FNac"));
                        VarUsuario.U_Activo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("U_Activo")));
                        VarUsuario.Id_TU = (int)dr.GetValue(dr.GetOrdinal("Id_TU"));
                        VarUsuario.U_VerTodo = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("U_VerTodo")));
                        VarUsuario.U_MultiCentro = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("U_MultiOfi")));
                        VarUsuario.Tu_Descripcion = (string)dr.GetValue(dr.GetOrdinal("Tu_Descripcion"));
                        VarUsuario.U_ActivoStr = (string)dr.GetValue(dr.GetOrdinal("Activo_String"));
                        VarUsuario.U_Correo = (string)dr.GetValue(dr.GetOrdinal("U_Correo"));
                        VarUsuario.Cu_User = (string)dr.GetValue(dr.GetOrdinal("Cu_User"));
                        VarUsuario.Id_Cd = (int)dr.GetValue(dr.GetOrdinal("Id_Cd"));
                        VarUsuario.Id_Id_U = (int)dr.GetValue(dr.GetOrdinal("Id_Id_U"));
                        VarUsuario.Ofi_Descripcion = (string)dr.GetValue(dr.GetOrdinal("Cd_Nombre"));
                        VarUsuario.Id_Rik = (int)dr.GetValue(dr.GetOrdinal("Id_Rik"));
                        VarUsuario.U_SusCredito = Convert.ToBoolean(dr.GetValue(dr.GetOrdinal("U_SusCredito")));
                        VarUsuario.U_DiasVencimiento = dr.IsDBNull(dr.GetOrdinal("U_DiasVencimiento")) ? (Double?)null : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("U_DiasVencimiento")));
                    }
                    list.Add(VarUsuario);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //se ejecuta desde sucursales y es para la gráfica de PIE por cantidad
        public void ConsultaGraficaEfectividadCantidad(ReporteCRM RegistroReporteCRM, ref List<ReporteCRM> listaReporteCRMAnalisis, ref List<ReporteCRM> listaReporteCRMPresentacion,
          ref List<ReporteCRM> listaReporteCRMNegociacion, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {  "@Id_Emp",
                                         "@Id_Cd",
                                         "@Id_Rik",
                                         "@TipoVenta",
                                         "@FECHA_INICIAL",
                                         "@FECHA_FINAL",
                                         "@Totalizar"};
                object[] Valores = {   RegistroReporteCRM.Id_Emp,
                                       RegistroReporteCRM.Id_Cd,
                                       RegistroReporteCRM.Id_Rik,
                                       RegistroReporteCRM.TipoVenta,
                                       RegistroReporteCRM.fechainicio,
                                       RegistroReporteCRM.fechafinal,
                                       RegistroReporteCRM.id_proyect  //Me regresa totalizado cuando es 1
    };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRM_ReporteLeadsCENTRAL_GraficaCantidad", ref dr, Parametros, Valores);


                while (dr.Read())
                {

                    RegistroReporteCRM = new ReporteCRM();
                    RegistroReporteCRM.nombre_rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("nombre"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("nombre")));
                    RegistroReporteCRM.Nombre = "Analisis";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("endesarrollo"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("endesarrollo")));
                    listaReporteCRMAnalisis.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();
                    RegistroReporteCRM.nombre_rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("nombre"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("nombre")));
                    RegistroReporteCRM.Nombre = "Presentación";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("cierre"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("cierre")));
                    listaReporteCRMPresentacion.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();
                    RegistroReporteCRM.nombre_rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("nombre"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("nombre")));
                    RegistroReporteCRM.Nombre = "Negociación";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cancelados"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Cancelados")));
                    listaReporteCRMNegociacion.Add(RegistroReporteCRM);

                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultaReporteGraficaEfectividadMonto(ReporteCRM RegistroReporteCRM, ref List<ReporteCRM> listaReporteCRMAnalisis, ref List<ReporteCRM> listaReporteCRMPresentacion,
           ref List<ReporteCRM> listaReporteCRMNegociacion, ref List<ReporteCRM> listaReporteCRMCierre, ref List<ReporteCRM> listaReporteCRMCancelacion, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {  "@Id_Emp",
                                         "@Id_Cd",
                                         "@Id_Rik",
                                         "@TipoVenta",
                                         "@FECHA_INICIAL",
                                         "@FECHA_FINAL"
                };
                object[] Valores = {   RegistroReporteCRM.Id_Emp,
                                       RegistroReporteCRM.Id_Cd,
                                       RegistroReporteCRM.Id_Rik != ""?      RegistroReporteCRM.Id_Rik : null,
                                       0 ,
                                       RegistroReporteCRM.fechainicio,
                                       RegistroReporteCRM.fechafinal};


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spLeads_ReporteEfectividadCentral_Monto", ref dr, Parametros, Valores);
                while (dr.Read())
                {

                    RegistroReporteCRM = new ReporteCRM();
                    RegistroReporteCRM.nombre_rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("nombre"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("nombre")));
                    RegistroReporteCRM.Nombre = "Analisis";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("analisis"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("analisis")));
                    listaReporteCRMAnalisis.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();
                    RegistroReporteCRM.nombre_rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("nombre"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("nombre")));
                    RegistroReporteCRM.Nombre = "Presentación";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Presentacion"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Presentacion")));
                    listaReporteCRMPresentacion.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();
                    RegistroReporteCRM.nombre_rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("nombre"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("nombre")));
                    RegistroReporteCRM.Nombre = "Negociación";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Negociacion"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Negociacion")));
                    listaReporteCRMNegociacion.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();
                    RegistroReporteCRM.nombre_rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("nombre"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("nombre")));
                    RegistroReporteCRM.Nombre = "Cierre";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cierre"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Cierre")));
                    listaReporteCRMCierre.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();
                    RegistroReporteCRM.nombre_rik = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("nombre"))) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("nombre")));
                    RegistroReporteCRM.Nombre = "Cancelacion";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Cancelacion"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("Cancelacion")));
                    listaReporteCRMCancelacion.Add(RegistroReporteCRM);

                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //esta función es para la gráfica de barras de efectividad por cantidad 
        // y si quiere totalizar lleva 1 
        public void ConsultaReporteEfectividadGrafBarrasCantidad(ReporteCRM RegistroReporteCRM, ref List<ReporteCRM> listaReporteCRMAnalisis, ref List<ReporteCRM> listaReporteCRMPresentacion,
          ref List<ReporteCRM> listaReporteCRMNegociacion, ref List<ReporteCRM> listaReporteCRMCierre, ref List<ReporteCRM> listaReporteCRMCancelacion, string Conexion)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {  "@Id_Emp",
                                         "@Id_Cd",
                                         "@Id_Rik",
                                         "@TipoVenta",
                                         "@FECHA_INICIAL",
                                         "@FECHA_FINAL",
                                         "@Totalizar"};
                object[] Valores = {   RegistroReporteCRM.Id_Emp,
                                       RegistroReporteCRM.Id_Cd,
                                       RegistroReporteCRM.Id_Rik,
                                       RegistroReporteCRM.TipoVenta,
                                       RegistroReporteCRM.fechainicio,
                                       RegistroReporteCRM.fechafinal,
                                       RegistroReporteCRM.id_proyect  //Me regresa totalizado cuando es 1
    };


                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCRM_ReporteLeadsCENTRAL_GraficaCantidad", ref dr, Parametros, Valores);

                if (dr.Read())
                {

                    RegistroReporteCRM = new ReporteCRM();

                    RegistroReporteCRM.Nombre = "Analisis";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("endesarrollo"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("endesarrollo")));
                    listaReporteCRMAnalisis.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();


                    RegistroReporteCRM.Nombre = "Presentación";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("cierre"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("cierre")));
                    listaReporteCRMPresentacion.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();

                    RegistroReporteCRM.Nombre = "Negociación";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("cancelados"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("cancelados")));
                    listaReporteCRMNegociacion.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();
                    RegistroReporteCRM.Nombre = "Cierre";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("cierre"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("cierre")));
                    listaReporteCRMCierre.Add(RegistroReporteCRM);

                    RegistroReporteCRM = new ReporteCRM();
                    RegistroReporteCRM.Nombre = "Cancelacion";
                    RegistroReporteCRM.Total = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("cierre"))) ? 0 : Convert.ToDecimal(dr.GetValue(dr.GetOrdinal("cierre")));
                    listaReporteCRMCancelacion.Add(RegistroReporteCRM);
                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void ConsultaListaGestionIncrementoClientes(GestionIncrementoPrecios reporteGAP, ref List<GestionIncrementoPrecios> listreporteGAP, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = {
                                        "@Id_Emp",

                                        "@Id_Cd",
                                        "@Id_Cte",
                                        "@Id_Tamaño",
                                        "@Id_Prd",
                                        "@IdEstatus",
                                        "@Id_Rik",
                                        "@Id_TipoReporte"};
                object[] Valores = {
                                       reporteGAP.Id_Emp,

                                       reporteGAP.Id_Cd,
                                       reporteGAP.Id_Cte,
                                       reporteGAP.Id_Tamaño,
                                       reporteGAP.Id_Prd,
                                       reporteGAP.Activo,
                                       reporteGAP.Id_Rik,
                                       reporteGAP.Id_TipoReporte
                                   };
                //      leads.IdGiroEmpresa,

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spGestionPreciosIncremento_ConsultaLista", ref dr, Parametros, Valores);

                GestionIncrementoPrecios c;


                while (dr.Read())
                {
                    c = new GestionIncrementoPrecios();
                    c.Id_Emp = Convert.ToInt32(dr["Id_Emp"]);
                    c.Id_Cd = Convert.ToInt32(dr["Id_Cd"]);
                    c.NomCDI = dr["NomCDI"].ToString().Trim();
                    c.Id_Rik = dr["Id_Rik"].ToString() == "" ? 0 : Convert.ToInt32(dr["Id_Rik"]);
                    c.Nombre_Rik = dr["Nombre_Rik"].ToString() == "" ? "" : Convert.ToString(dr["Nombre_Rik"]);

                    c.Activo = Convert.ToInt32(dr["Activo"]);
                    c.Id_Cte = dr["Id_Cte"].ToString() == "" ? 0 : Convert.ToInt32(dr["Id_Cte"]);
                    c.Cte_NomComercial = dr["Cte_NomComercial"].ToString() == "" ? "" : Convert.ToString(dr["Cte_NomComercial"]);
                    c.Id_Tamaño = dr["Id_Tamaño"].ToString() == "" ? "" : Convert.ToString(dr["Id_Tamaño"]);

                    c.Unidades = dr["Unidades"].ToString() == "" ? 0 : Convert.ToDouble(dr["Unidades"]);

                    c.Ventas = dr["Ventas"].ToString() == "" ? 0 : Convert.ToDouble(dr["Ventas"]);

                    c.VentasPA = dr["VentasPA"].ToString() == "" ? 0 : Convert.ToDouble(dr["VentasPA"]);
                    c.Var_VentaMonto = dr["Var_VentaMonto"].ToString() == "" ? 0 : Convert.ToDouble(dr["Var_VentaMonto"]);
                    c.Var_VentaPorc = dr["Var_VentaPorc"].ToString() == "" ? 0 : Convert.ToDouble(dr["Var_VentaPorc"]);
                    c.MgRed_MontoActual = dr["MgRed_MontoActual"].ToString() == "" ? 0 : Convert.ToDouble(dr["MgRed_MontoActual"]);
                    c.MgRed_Proyectada = dr["MgRed_Proyectada"].ToString() == "" ? 0 : Convert.ToDouble(dr["MgRed_Proyectada"]);
                    c.VarMgRed_Monto = dr["VarMgRed_Monto"].ToString() == "" ? 0 : Convert.ToDouble(dr["VarMgRed_Monto"]);


                    c.VarMgRed_Porc = dr["VarMgRed_Porc"].ToString() == "" ? 0 : Convert.ToDouble(dr["VarMgRed_Porc"]);
                    c.NomEstatus = dr["NomEstatus"].ToString() == "" ? "" : Convert.ToString(dr["NomEstatus"]);


                    //c.GAPIngresos_Monto = dr["GAPIngresos_Monto"].ToString() == "" ? 0 : Convert.ToDouble(dr["GAPIngresos_Monto"]);
                    //c.GAPIngresos_Porc = dr["GAPIngresos_Porc"].ToString() == "" ? 0 : Convert.ToDouble(dr["GAPIngresos_Porc"]);
                    //c.POvsPV = dr["POvsPV"].ToString() == "" ? 0 : Convert.ToDouble(dr["POvsPV"]);
                    //c.MgRedPO_Monto = dr["MgRedPO_Monto"].ToString() == "" ? 0 : Convert.ToDouble(dr["MgRedPO_Monto"]);


                    c.Id_Matriz = dr["Id_Matriz"].ToString() == "" ? 0 : Convert.ToInt32(dr["Id_Matriz"]);
                    c.NombreMatriz = dr["NombreMatriz"].ToString() == "" ? "" : Convert.ToString(dr["NombreMatriz"]);

                    c.TipoCuenta = dr["TipoCuenta"].ToString() == "" ? "" : Convert.ToString(dr["TipoCuenta"]);
                    c.MgRed_PorcActual = dr["MgRed_PorcActual"].ToString() == "" ? 0 : Convert.ToDouble(dr["MgRed_PorcActual"]);
                    c.MgRed_PorcProy = dr["MgRed_PorcProy"].ToString() == "" ? 0 : Convert.ToDouble(dr["MgRed_PorcProy"]);
                    //c.VarPpbRed_Porc = c.MgRed_Porc - c.MgRed_PorcProy;
                    c.MgRed_PorcActual = dr["MgRed_PorcActual"].ToString() == "" ? 0 : Convert.ToDouble(dr["MgRed_PorcActual"]);
                    c.VarPpbRed_Porc = c.MgRed_PorcProy - c.MgRed_PorcActual;
                    c.IdReporteGP = dr["Id_ReporteGP"].ToString() == "" ? 0 : Convert.ToInt32(dr["Id_ReporteGP"]);
                    c.TieneIncremento = Convert.ToInt32(dr["TieneIncremento"]);

                    listreporteGAP.Add(c);

                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public void ConsultaListaGPMa_Consultadet(GestionIncrementoPrecios reporteGAP, ref List<GestionIncrementoPrecios> listreporteGAP, string Conexion)
        {
            try
            {
                CD_Datos cd_datos = new CD_Datos(Conexion);
                SqlDataReader dr = null;

                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Id_Cte",
                                        "@Id_Tamaño",
                                        "@Id_Prd",
                                        "@IdEstatus",
                                        "@Id_Rik",
                                        "@Id_ReporteGP",
                                        "@Id_VersionGPMA"};
                object[] Valores = {
                                       reporteGAP.Id_Emp,
                                       reporteGAP.Id_Cd,
                                       reporteGAP.Id_Cte,
                                       reporteGAP.Id_Tamaño,
                                       reporteGAP.Id_Prd,
                                       reporteGAP.Activo,
                                       reporteGAP.Id_Rik,
                                       reporteGAP.IdReporteGP,
                                       2
                                   };
                //      leads.IdGiroEmpresa,

                SqlCommand sqlcmd = cd_datos.GenerarSqlCommand("spCapReporteGPMa_ConsultaDet", ref dr, Parametros, Valores);

                GestionIncrementoPrecios c;


                while (dr.Read())
                {
                    c = new GestionIncrementoPrecios();
                    c.Id_Emp = Convert.ToInt32(dr["Id_Emp"]);
                    c.Id_Cd = Convert.ToInt32(dr["Id_Cd"]);
                    c.NomCDI = dr["NomCDI"].ToString().Trim();

                    c.Id_Rik = dr["Id_Rik"].ToString() == "" ? 0 : Convert.ToInt32(dr["Id_Rik"]);
                    c.Nombre_Rik = dr["Nombre_Rik"].ToString() == "" ? "" : Convert.ToString(dr["Nombre_Rik"]);

                    c.Activo = Convert.ToInt32(dr["Activo"]);
                    c.Id_Cte = dr["Id_Cte"].ToString() == "" ? 0 : Convert.ToInt32(dr["Id_Cte"]);
                    c.Cte_NomComercial = dr["Cte_NomComercial"].ToString() == "" ? "" : Convert.ToString(dr["Cte_NomComercial"]);
                    c.Id_Tamaño = dr["Id_Tamaño"].ToString() == "" ? "" : Convert.ToString(dr["Id_Tamaño"]);

                    c.Id_Categoria = dr["Id_Categoria"].ToString() == "" ? 0 : Convert.ToInt32(dr["Id_Categoria"]);
                    c.NomCategoria = dr["NomCategoria"].ToString() == "" ? "" : Convert.ToString(dr["NomCategoria"]);

                    c.Id_Prd = dr["Id_Prd"].ToString() == "" ? 0 : Convert.ToInt32(dr["Id_Prd"]);
                    c.NomProducto = dr["NomProducto"].ToString() == "" ? "" : Convert.ToString(dr["NomProducto"]);

                    // c.PrecioListaActual = dr["PrecioListaActual"].ToString() == "" ? 0 : Convert.ToDouble(dr["PrecioListaActual"]);
                    c.PrecioVenta = dr["PrecioVenta"].ToString() == "" ? 0 : Convert.ToDouble(dr["PrecioVenta"]);
                    c.TipoCuenta = dr["TipoCuenta"].ToString() == "" ? "" : Convert.ToString(dr["TipoCuenta"]);
                    c.Id_Matriz = dr["Id_Matriz"].ToString() == "" ? 0 : Convert.ToInt32(dr["Id_Matriz"]);
                    c.NombreMatriz = dr["NombreMatriz"].ToString() == "" ? "" : Convert.ToString(dr["NombreMatriz"]);
                    c.EjecutivoCuenta = dr["EjecutivoCuenta"].ToString() == "" ? "" : Convert.ToString(dr["EjecutivoCuenta"]);

                    c.Pc_NoConvenio = dr["Pc_NoConvenio"].ToString() == "" ? "" : Convert.ToString(dr["Pc_NoConvenio"]);
                    c.PC_Nombre = dr["PC_Nombre"].ToString() == "" ? "" : Convert.ToString(dr["PC_Nombre"]);
                    c.NombreCategoriaConvenio = dr["NombreCategoriaConvenio"].ToString() == "" ? "" : Convert.ToString(dr["NombreCategoriaConvenio"]);
                    c.Id_Pc = dr["Id_Pc"].ToString() == "" ? 0 : Convert.ToInt32(dr["Id_Pc"]);
                    c.CategoriaConv = dr["CategoriaConv"].ToString() == "" ? "" : Convert.ToString(dr["CategoriaConv"]);


                    //Situación actual
                    c.Unidades = Convert.ToDouble(dr["Unidades"]);
                    c.Ventas = dr["Ventas"].ToString() == "" ? 0 : Convert.ToDouble(dr["Ventas"]);
                    c.MgRed_MensualPesos = dr["MgRed_MensualPesos"].ToString() == "" ? 0 : Convert.ToDouble(dr["MgRed_MensualPesos"]);
                    c.MgRed_MensualPorc = dr["MgRed_MensualPorc"].ToString() == "" ? 0 : Convert.ToDouble(dr["MgRed_MensualPorc"]);

                    //Escenario PROYECTADO
                    c.PrecioObjetivoProy = dr["PrecioObjetivoProy"].ToString() == "" ? 0 : Convert.ToDouble(dr["PrecioObjetivoProy"]);
                    c.PrecioListaProy = dr["PrecioListaProy"].ToString() == "" ? 0 : Convert.ToDouble(dr["PrecioListaProy"]);
                    c.PrecioMinRikProy = dr["PrecioMinRikProy"].ToString() == "" ? 0 : Convert.ToDouble(dr["PrecioMinRikProy"]);
                    c.PrecioGteProy = dr["PrecioGteProy"].ToString() == "" ? 0 : Convert.ToDouble(dr["PrecioGteProy"]);
                    c.PrecioNegociadoProy = dr["PrecioNegociadoProy"].ToString() == "" ? 0 : Convert.ToDouble(dr["PrecioNegociadoProy"]);
                    c.PorcIncrementoProy = dr["PorcIncrementoProy"].ToString() == "" ? 0 : Convert.ToDouble(dr["PorcIncrementoProy"]);
                    c.DescuentoSobrePlistaProy = dr["DescuentoSobrePlistaProy"].ToString() == "" ? 0 : Convert.ToDouble(dr["DescuentoSobrePlistaProy"]);
                    c.VentaProy = dr["VentaProy"].ToString() == "" ? 0 : Convert.ToDouble(dr["VentaProy"]);
                    c.MgRed_PesosProy = dr["MgRed_PesosProy"].ToString() == "" ? 0 : Convert.ToDouble(dr["MgRed_PesosProy"]);
                    c.MgRed_PorcProy = dr["MgRed_PorcProy"].ToString() == "" ? 0 : Convert.ToDouble(dr["MgRed_PorcProy"]);
                    c.Comentarios = dr["Comentarios"].ToString() == "" ? "" : Convert.ToString(dr["Comentarios"]);
                    c.NomEstatus = dr["NomEstatus"].ToString() == "" ? "" : Convert.ToString(dr["NomEstatus"]);
                    c.CostoAAAActual = dr["CostoAAAActual"].ToString() == "" ? 0 : Convert.ToDouble(dr["CostoAAAActual"]);
                    c.CostoAAAAFuturo = dr["CostoAAAAFuturo"].ToString() == "" ? 0 : Convert.ToDouble(dr["CostoAAAAFuturo"]);
                    c.UnidadesProyectadas = dr["unidadesProyectadas"].ToString() == "" ? 0 : Convert.ToDouble(dr["unidadesProyectadas"]);
                    c.MgRed_PorcActual = dr["MgRed_PorcActual"].ToString() == "" ? 0 : Convert.ToDouble(dr["MgRed_PorcActual"]);
                    //c.VarPpbRed_Porc = c.MgRed_PorcProy- c.MgRed_PorcActual;
                    c.VarPpbRed_Porc = dr["VarPpbRed_Porc"].ToString() == "" ? 0 : Convert.ToDouble(dr["VarPpbRed_Porc"]);
                    c.IdReporteGP = dr["Id_ReporteGP"].ToString() == "" ? 0 : Convert.ToInt32(dr["Id_ReporteGP"]);
                    c.Id_Estatus = dr["Id_Estatus"].ToString() == "" ? 0 : Convert.ToInt32(dr["Id_Estatus"]);
                    c.TieneIncremento = Convert.ToInt32(dr["TieneIncremento"]);
                    listreporteGAP.Add(c);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public object ConsultaPropuesta(int id_cte, int id_rik, int id_cd, string Conexion, ref int Id_reportegp)
        {
            Id_reportegp = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion))
                {

                    conn.Open();
                    //string query = "Select  top 1 gp.Id_reporteGP, c.id_cte,c.id_cte ,gp.NomEstatus ,c.Cte_Email , gp.Id_Estatus,gp.DiasVigencia, gp.FechaInicioIncremento, Telefono from sianwebcentral.dbo.ReporteGP  gp left join catcliente c on c.id_cte = GP.Id_Cte and gp.Id_cd = c.Id_Cd where gp.id_cte = @Id_Cte and gp.id_cd = @Id_Cd  AND gp.Id_Rik = @Id_Rik order by Id_reporteGP desc  ";
                    string query = @"
                SELECT TOP 1 
                     isnull(gp.Id_reporteGP,0) Id_reporteGP ,  
                    c.Id_Cte, 
                     isnull(e.GPMA_Estatus,'Analizando') 	as   NomEstatus ,
                    ISNULL(c.Cte_Email,'') Cte_Email, 
                    gp.Id_Estatus, 
                    ISNULL(gp.DiasVigencia,15) as DiasVigencia, 
                    ISNULL(gp.FechaInicioIncremento,getdate() ) as FechaInicioIncremento, 
                     ISNULL(gp.Telefono ,'') as Telefono,
                     CONVERT(DATE, CONVERT(VARCHAR(8), CAST(valor AS BIGINT)), 112) AS FechaIncrementoLimite
                 FROM  catcliente c 
                 LEFT JOIN sianwebcentral.dbo.CapReporteGP gp
                    ON c.Id_Cte = gp.Id_Cte AND gp.Id_Cd = c.Id_Cd
                 LEFT JOIN CatReporteGPMAEstatus e on ISNULL(gp.Id_estatus,0) = e.Id_estatus 
                 LEFT JOIN siancentral.siancentral.dbo.CatPreciadorConfiguracion  pc ON pc.Id_Emp = gp.Id_emp and Id_Configuracion = 9

                 WHERE c.Id_Cte = @Id_Cte 
                     AND c.Id_Cd = @Id_Cd
                     AND ISNULL(gp.Id_Rik,-1) in (  @Id_Rik,-1) 
                 ORDER BY gp.Id_reporteGP DESC ";



                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {

                        cmd.Parameters.AddWithValue("@Id_Cte", id_cte);
                        cmd.Parameters.AddWithValue("@Id_Rik", id_rik);
                        cmd.Parameters.AddWithValue("@Id_Cd", id_cd);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                Id_reportegp = Convert.ToInt32(reader["Id_reporteGP"].ToString());

                                return new
                                {
                                    Cte_Email = Convert.ToString(reader["Cte_Email"]),
                                    Telefono = reader["Telefono"].ToString(),
                                    FechaIncremento = Convert.ToDateTime(reader["FechaInicioIncremento"]).ToString("yyyy-MM-dd"),
                                    Id_Estatus = reader["Id_Estatus"].ToString(),
                                    NomEstatus = reader["NomEstatus"].ToString(),
                                    Dias = reader["DiasVigencia"].ToString(),
                                    Id_reporteGP = reader["Id_reporteGP"].ToString(),
                                    FechaIncrementoLimite = reader["FechaIncrementoLimite"].ToString() == "" ? "" : Convert.ToDateTime(reader["FechaIncrementoLimite"]).ToString("yyyy-MM-dd")
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new
                {
                    Error = ex.Message,
                    Stack = ex.StackTrace
                };
            }
            return new
            {
                Cte_Email = "",
                Telefono = "",
                FechaIncremento = Convert.ToDateTime(DateTime.Now.AddMonths(3)).ToString("yyyy-MM-dd"),
                Id_Estatus = 0,
                NomEstatus = "Analizando",
                Dias = 15,
                Id_reporteGP = Id_reportegp,
                FechaIncrementoLimite = Convert.ToDateTime(DateTime.Now.AddMonths(3)).ToString("yyyy-MM-dd")
            };
        }


        public void InsertarGestionIncrementoanterior(GestionIncrementoPrecios encabezado, List<GestionIncrementoPrecios> list, string Conexion, ref int Id_ReporteGP)
        {
            CapaDatos.CD_Datos CapaDatos = default(CD_Datos);
            SqlCommand sqlcmd = default(SqlCommand);

            try
            {

                int verificador = 0;
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                CapaDatos.StartTrans();
                string[] Parametros = {
                        "@Id_Emp",
                        "@Id_Cd",
                        "@Id_Rik",
                        "@Id_Cte",
                        "@Id_Matriz",
                        "@NombreMatriz",
                        "@TipoCuenta",
                        "@Id_Tamaño",
                        "@Id_Estatus",
                        "@NomEstatus",
                        "@Id_ReporteGP",
                        "@DiasVigencia",
                        "@FechaInicioIncremento",
                        "@Telefono",
                        "@PorcentualIncremental"

                };
                object[] Valores = {
                    encabezado.Id_Emp,
                    encabezado.Id_Cd,
                    encabezado.Id_Rik,
                    encabezado.Id_Cte,
                    encabezado.Id_Matriz,
                    encabezado.NombreMatriz,
                    encabezado.TipoCuenta,
                    encabezado.Id_Tamaño,
                    2, // Id_Estatus default
                    encabezado.NomEstatus ,
                    Id_ReporteGP,
                    encabezado.DiasVigencia,
                    encabezado.FechaInicioIncremento,
                    encabezado.Telefono,
                    encabezado.PorcentualIncremental
                };


                sqlcmd = CapaDatos.GenerarSqlCommand("ReporteGP_Insertar", ref verificador, Parametros, Valores);


                sqlcmd.Dispose();
                Id_ReporteGP = verificador;



                Parametros = new string[] {
                                "@Id_ReporteGP",
                                "@Id_Emp",
                                "@Id_Cd",
                                "@Id_Rik",
                                "@Nombre_Rik",
                                "@Activo",
                                "@Id_Cte",
                                "@Cte_NomComercial",
                                "@Id_Tamaño",
                                "@Id_Categoria",
                                "@NomCategoria",
                                "@Id_Prd",
                                "@NomProducto",
                                "@PrecioListaActual",
                                "@PrecioVentaPromedioActual",
                                "@CostoAAAActual",
                                "@Unidades",
                                "@PrecioVenta",
                                "@Ventas",
                                "@MgRed_MensualPesos",
                                "@MgRed_MensualPorc",
                                "@PrecioObjetivoProy",
                                "@PrecioListaProy",
                                "@PrecioMinRikProy",
                                "@PrecioGteProy",
                                "@PrecioNegociadoProy",
                                "@PorcIncrementoProy",
                                "@DescuentoSobrePlistaProy",
                                "@VentaProy",
                                "@MgRed_PesosProy",
                                "@MgRed_PorcProy",
                                "@Comentarios",
                                "@IdUsuario",
                                "@NomEstatus",
                                "@UnidadesProyectadas",
                                "@CostoAAAAFuturo"

                    };
                for (int x = 0; x < list.Count; x++)
                {
                    if (verificador < 0)
                    {
                        break;
                    }
                    else
                    {
                        verificador = -1;
                    }

                    Valores = new object[] {
                        Id_ReporteGP,
                            encabezado.Id_Emp,
                            encabezado.Id_Cd ,

                             encabezado.Id_Rik,
                             encabezado.Nombre_Rik,
                             1,
                             encabezado.Id_Cte,
                             encabezado.Cte_NomComercial,
                             encabezado.Id_Tamaño,
                             list[x].Id_Categoria,
                             list[x].NomCategoria,
                             list[x].Id_Prd,
                             list[x].NomProducto,
                             list[x].PrecioListaActual,
                             list[x].PrecioVentaPromedioActual,
                             list[x].CostoAAAActual,
                             list[x].Unidades,
                             list[x].PrecioVenta,
                             list[x].Ventas,
                             list[x].MgRed_MensualPesos,
                             list[x].MgRed_MensualPorc,
                             list[x].PrecioObjetivoProy,
                             list[x].PrecioListaProy,
                             list[x].PrecioMinRikProy,
                             list[x].PrecioGteProy,
                             list[x].PrecioNegociadoProy,
                             list[x].PorcIncrementoProy,
                             list[x].DescuentoSobrePlistaProy,
                             list[x].VentaProy,
                             list[x].MgRed_PesosProy,
                             list[x].MgRed_PorcProy,
                             list[x].Comentarios,
                             list[x].IdUsuario,
                             list[x].NomEstatus,
                             list[x].UnidadesProyectadas,
                             list[x].CostoAAAAFuturo
                        };


                    sqlcmd = CapaDatos.GenerarSqlCommand("ReporteGPDetalle_Insertar", ref verificador, Parametros, Valores);
                    sqlcmd.Dispose();
                }


                if (verificador < 0)
                {
                    CapaDatos.RollBackTrans();
                }
                else
                {
                    CapaDatos.CommitTrans();
                }
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw new Exception("Excepción levantada al correr CD_ReporteGAP.Insertar", ex);
            }
            finally
            {
                //CapaDatos.CommitTrans();
            }
        }

        public void InsertarGestionIncremento(GestionIncrementoPrecios encabezado, List<GestionIncrementoPrecios> list, string Conexion, ref int Id_ReporteGP)
        {
            CapaDatos.CD_Datos CapaDatos = default(CD_Datos);
            SqlCommand sqlcmd = default(SqlCommand);

            try
            {

                int verificador = 0;
                string mensaje = string.Empty;

                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                CapaDatos.StartTrans();
                string[] Parametros = {
    "@Id_Emp",
    "@Id_Cd",
    "@Id_Rik",
    "@Id_Cte",
    "@Id_Matriz",
    "@NombreMatriz",
    "@TipoCuenta",
    "@Id_Tamaño",
    "@Id_Estatus",
    "@NomEstatus",
    "@Id_ReporteGP",
    "@DiasVigencia",
    "@FechaInicioIncremento",
    "@Telefono",
    "@PorcentualIncremental",
    "@Mensaje",
    "@IdUsuario"
};
                object[] Valores = {
    encabezado.Id_Emp ?? 0,
    encabezado.Id_Cd ?? 0,
    encabezado.Id_Rik ?? 0,
    encabezado.Id_Cte ?? 0,
    encabezado.Id_Matriz ?? 0,
    encabezado.NombreMatriz,
    encabezado.TipoCuenta,
    encabezado.Id_Tamaño,
    2,
    encabezado.NomEstatus,
    Id_ReporteGP,
    encabezado.DiasVigencia,
    encabezado.FechaInicioIncremento,
    encabezado.Telefono,
    encabezado.PorcentualIncremental,
    "",
    encabezado.IdUsuario
};




                sqlcmd = CapaDatos.GenerarSqlCommand("spCapReporteGP_Insertar", ref verificador, Parametros, Valores);

                sqlcmd.ExecuteNonQuery();

                if (verificador < 0)
                {
                    CapaDatos.RollBackTrans();
                    throw new Exception("Error al insertar encabezado: " + mensaje);
                }


                sqlcmd.Dispose();
                Id_ReporteGP = verificador;



                string[] ParametrosDetalle =  {
                          "@Id_ReporteGP",
                          "@Id_Emp",
                          "@Id_Cd",
                          "@Id_Rik",
                          "@Id_Cte",
                          "@Id_Categoria",
                          "@Id_Prd",
                          "@PrecioListaActual",
                          "@PrecioVentaPromedioActual",
                          "@CostoAAAActual",
                          "@Unidades",
                          "@PrecioVenta",
                          "@PrecioObjetivoProy",
                          "@PrecioListaProy",
                          "@PrecioMinRikProy",
                          "@PrecioGteProy",
                          "@PrecioNegociadoProy",
                          "@PorcIncrementoProy",
                          "@UnidadesProyectadas",
                          "@DescuentoSobrePlistaProy",
                          "@Comentarios",
                          "@IdUsuario",
                          "@Id_Estatus",
                          "@NomEstatus",
                          "@CostoAAAAFuturo"
                       };
                for (int x = 0; x < list.Count; x++)
                {
                    if (verificador < 0)
                    {
                        break;
                    }
                    else
                    {
                        verificador = -1;
                    }

                    object[] ValoresDetalle =  {
                  Id_ReporteGP,
                      encabezado.Id_Emp,
                      encabezado.Id_Cd ,

                       encabezado.Id_Rik,
                       encabezado.Id_Cte,
                       list[x].Id_Categoria,
                       list[x].Id_Prd,
                       list[x].PrecioListaActual,
                       list[x].PrecioVentaPromedioActual,
                       list[x].CostoAAAActual,
                       list[x].Unidades,
                       list[x].PrecioVenta,
                       list[x].PrecioObjetivoProy,
                       list[x].PrecioListaProy,
                       list[x].PrecioMinRikProy,
                       list[x].PrecioGteProy,
                       list[x].PrecioNegociadoProy,
                       list[x].PorcIncrementoProy,
                       list[x].UnidadesProyectadas,
                       list[x].DescuentoSobrePlistaProy,
                       list[x].Comentarios,
                       encabezado.IdUsuario,
                       list[x].Id_Estatus,
                       list[x].NomEstatus,
                       list[x].CostoAAAAFuturo
                  };


                    sqlcmd = CapaDatos.GenerarSqlCommand("spCapReporteGPDetalle_Insertar", ref verificador, ParametrosDetalle, ValoresDetalle);
                    sqlcmd.ExecuteNonQuery();
                    sqlcmd.Dispose();

                    if (verificador < 0)
                    {
                        CapaDatos.RollBackTrans();
                        throw new Exception($"Error al insertar detalle {x + 1}.");
                    }

                }
                CapaDatos.CommitTrans();
            }
            catch (Exception ex)
            {
                CapaDatos.RollBackTrans();
                throw new Exception("Excepción levantada al correr CD_ReporteGAP.Insertar: " + ex.Message, ex);
            }
            finally
            {
                //CapaDatos.CommitTrans();
            }
        }

        public void CerrarGestionIncremento(GestionIncrementoPrecios encabezado, string Conexion, ref int verificador)
        {
            CapaDatos.CD_Datos CapaDatos = default(CD_Datos);
            SqlCommand sqlcmd = default(SqlCommand);

            try
            {

                int Id_ReporteGP = 0;
                CapaDatos = new CapaDatos.CD_Datos(Conexion);
                CapaDatos.StartTrans();
                string[] Parametros = {
                        "@Id_Emp",
                        "@Id_Cd",
                        "@Id_Rik",
                        "@Id_Cte",
                        "@Id_Estatus",
                        "@Id_ReporteGP",
                        "@Id_Usuario"

                };
                object[] Valores = {
                    encabezado.Id_Emp,
                    encabezado.Id_Cd,
                    encabezado.Id_Rik,
                    encabezado.Id_Cte,
                    encabezado.Id_Estatus,
                    encabezado.IdReporteGP,
                    encabezado.IdUsuario
                };


                sqlcmd = CapaDatos.GenerarSqlCommand("spCapReporteGP_CambiarEstatus", ref verificador, Parametros, Valores);


                sqlcmd.Dispose();
                Id_ReporteGP = verificador;



                if (verificador < 0)
                {
                    CapaDatos.RollBackTrans();
                    throw new Exception("Error al cambiar el estatus");
                }
                else
                {
                    CapaDatos.CommitTrans();
                }
            }
            catch (Exception ex)
            {
                if (CapaDatos != null) CapaDatos.RollBackTrans();
                throw new Exception("Excepción levantada al correr CD_ReporteGAP.Cerrar: " + ex.Message, ex);
            }
            finally
            {
                if (sqlcmd != null) sqlcmd.Dispose();
            }
        }


        public void ReporteRentabilidad_ConsultarTotales(int Id_Emp, int Id_Cd_Ver, int Id_Cte, int? Id_Ter, string periodo, string ventas, ref DataTable dt, string id_reportegp, string Conexion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = {
                                          "@Id_Emp"
                                          ,"@Id_Cd_Ver"
                                          ,"@Id_Cte"
                                          ,"@Id_Ter"
                                          ,"@periodo"
                                          ,"@ventas"
                                          ,"@Id_ReporteGP"
                                      };
                object[] Valores = {
                                       Id_Emp
                                       ,Id_Cd_Ver
                                       ,Id_Cte
                                       ,Id_Ter
                                       ,periodo
                                       ,ventas
                                       ,id_reportegp
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRep_VenRentabilidad_Ventas_GPMaFuturo", "tabla", ref dt, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}