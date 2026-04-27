using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;
using CapaModelo;
using System.Data.SqlTypes;

namespace CapaDatos
{
    public class CD_Nps
    {

        public void ConsultarSucursales(string strConexion, ref List<ItemsTextValue> lstData)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(strConexion);
                ItemsTextValue item = new ItemsTextValue();
                SqlDataReader dr = null;

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatNps_ComboSucursal", ref dr);

                while (dr.Read())
                {
                    item = new ItemsTextValue();
                    item.intValue = dr.GetInt32(dr.GetOrdinal("Id"));
                    item.strText = dr.GetString(dr.GetOrdinal("Descripcion"));
                    lstData.Add(item);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarCatalogo(string strConexion, string strSP, ref List<ItemsTextValue> lstData)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(strConexion);
                ItemsTextValue item = new ItemsTextValue();
                SqlDataReader dr = null;

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand(strSP, ref dr);

                while (dr.Read())
                {
                    item = new ItemsTextValue();
                    item.intValue = dr.GetInt32(dr.GetOrdinal("Id"));
                    item.strText = dr.GetString(dr.GetOrdinal("Descripcion"));
                    lstData.Add(item);
                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarNpsReporte(string strConexion, ref List<NpsQueja_Detalle> lstNpsQuejaDetalle, int intIdSucursal)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(strConexion);
                NpsQueja_Detalle entNpsQuejaDetalle = new NpsQueja_Detalle();
                SqlDataReader dr = null; string[] Parametros = {
                                        "@Id_Cd"
                                      };
                object[] Valores = {
                                       intIdSucursal
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatNps_Reporte", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        entNpsQuejaDetalle = new NpsQueja_Detalle();

                        entNpsQuejaDetalle.Id_Nps_Queja = dr.GetInt32(dr.GetOrdinal("Id_Nps_Queja"));
                        entNpsQuejaDetalle.Id_Nps = dr.GetInt32(dr.GetOrdinal("Id_Nps"));
                        entNpsQuejaDetalle.Folio_Entrevista = dr.GetString(dr.GetOrdinal("Folio_Entrevista"));
                        entNpsQuejaDetalle.Fecha_Entrevista = dr.GetString(dr.GetOrdinal("Fecha_Entrevista"));
                        entNpsQuejaDetalle.Nps_Descripcion = dr.GetString(dr.GetOrdinal("Nps_Descripcion"));
                        entNpsQuejaDetalle.Nps_Tema = dr.GetString(dr.GetOrdinal("Nps_Tema"));
                        entNpsQuejaDetalle.Cd_Nombre = dr.GetString(dr.GetOrdinal("Cd_Nombre"));
                        entNpsQuejaDetalle.Id_Cd = dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                        entNpsQuejaDetalle.Nps_Plan = dr.GetString(dr.GetOrdinal("Nps_Plan"));
                        entNpsQuejaDetalle.Nps_Estatus = dr.GetString(dr.GetOrdinal("Nps_Estatus"));
                        entNpsQuejaDetalle.Id_Nps_Estatus = dr.GetInt32(dr.GetOrdinal("Id_Nps_EstatusActual"));
                        lstNpsQuejaDetalle.Add(entNpsQuejaDetalle);

                    }
                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarNpsFiltros(string strConexion, ref List<NpsQueja_Detalle> lstNpsQuejaDetalle, Nps_Filtro_Busqueda entFiltro)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(strConexion);
                NpsQueja_Detalle entNpsQuejaDetalle = new NpsQueja_Detalle();
                SqlDataReader dr = null; string[] Parametros = {
                                        "@Id_Cd",
                                        "@Id_Rik",
                                        "@Id_Cte",
                                        "@Id_Nps_Tipo",
                                        "@Id_Nps_Estatus",
                                        "@FechaInicial",
                                        "@FechaFinal",
                                      };
                object[] Valores = {
                                       entFiltro.Id_Cd,
                                       entFiltro.Id_Rik,
                                       entFiltro.Id_Cte,
                                       entFiltro.Id_Nps_Tipo,
                                       entFiltro.Id_Nps_Estatus,
                                       entFiltro.FechaInicial,
                                       entFiltro.FechaFinal
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatNps_ReporteLocal", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        entNpsQuejaDetalle = new NpsQueja_Detalle();

                        entNpsQuejaDetalle.Id_Nps_Queja = dr.GetInt32(dr.GetOrdinal("Id_Nps_Queja"));
                        entNpsQuejaDetalle.Id_Nps = dr.GetInt32(dr.GetOrdinal("Id_Nps"));
                        entNpsQuejaDetalle.Nps_Estatus = dr.GetString(dr.GetOrdinal("Nps_Estatus"));
                        entNpsQuejaDetalle.Id_Nps_Estatus = dr.GetInt32(dr.GetOrdinal("Id_Nps_EstatusActual"));
                        entNpsQuejaDetalle.Fecha_Entrevista = dr.GetString(dr.GetOrdinal("Fecha_Entrevista"));
                        entNpsQuejaDetalle.Folio_Entrevista = dr.GetString(dr.GetOrdinal("Folio_Entrevista"));
                        entNpsQuejaDetalle.Cd_Nombre = entFiltro.Cd_Nombre;
                        entNpsQuejaDetalle.Id_Rik = dr.GetInt32(dr.GetOrdinal("Id_Rik"));
                        entNpsQuejaDetalle.Rik_Nombre = dr.GetString(dr.GetOrdinal("Rik_Nombre"));
                        entNpsQuejaDetalle.Id_Cte = dr.GetInt32(dr.GetOrdinal("Id_Cte"));
                        entNpsQuejaDetalle.Cte_Nomcomercial = dr.GetString(dr.GetOrdinal("Cte_Nomcomercial"));

                        entNpsQuejaDetalle.Nps_Descripcion = dr.GetString(dr.GetOrdinal("Nps_Descripcion"));
                        entNpsQuejaDetalle.Nps_Tema = dr.GetString(dr.GetOrdinal("Nps_Tema"));
                        entNpsQuejaDetalle.Id_Nps_Plan = dr.GetInt32(dr.GetOrdinal("Id_Nps_Plan"));
                        entNpsQuejaDetalle.Nps_Plan = dr.GetString(dr.GetOrdinal("Nps_PlanAccion"));

                        lstNpsQuejaDetalle.Add(entNpsQuejaDetalle);

                    }
                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarNpsAgrupado(string strConexion, ref List<NpsQueja_Detalle> lstNpsQuejaDetalle, Nps_Filtro_Busqueda entFiltro)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(strConexion);
                NpsQueja_Detalle entNpsQuejaDetalle = new NpsQueja_Detalle();
                SqlDataReader dr = null; string[] Parametros = {
                                        "@Id_Cd",
                                        "@Cd_Nombre",
                                        "@Id_Rik",
                                        "@Id_Cte",
                                        "@Id_Nps_Tipo",
                                        "@Id_Nps_Tema",
                                        "@Id_Nps_Estatus",
                                        "@FechaInicial",
                                        "@FechaFinal",
                                        "@Lst_UEN"
                                      };
                object[] Valores = {
                                       entFiltro.Id_Cd,
                                       "",
                                       entFiltro.Id_Rik,
                                       entFiltro.Id_Cte,

                                       entFiltro.Id_Nps_Tipo,
                                       -1,
                                       entFiltro.Id_Nps_Estatus,
                                       entFiltro.FechaInicial,
                                       entFiltro.FechaFinal,
                                       entFiltro.strUEN
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatNps_ReporteLocal", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        entNpsQuejaDetalle = new NpsQueja_Detalle();

                        entNpsQuejaDetalle.Id_Nps = dr.GetInt32(dr.GetOrdinal("Id_Nps"));
                        entNpsQuejaDetalle.Nps_Estatus = dr.GetString(dr.GetOrdinal("Nps_Estatus"));
                        entNpsQuejaDetalle.Id_Nps_Estatus = dr.GetInt32(dr.GetOrdinal("Id_Nps_Estatus"));
                        entNpsQuejaDetalle.Fecha_Entrevista = dr.GetString(dr.GetOrdinal("Fecha_Entrevista"));
                        entNpsQuejaDetalle.Folio_Entrevista = dr.GetString(dr.GetOrdinal("Folio_Entrevista"));
                        entNpsQuejaDetalle.Cd_Nombre = entFiltro.Cd_Nombre;
                        entNpsQuejaDetalle.Id_Rik = dr.GetInt32(dr.GetOrdinal("Id_Rik"));
                        entNpsQuejaDetalle.Rik_Nombre = dr.GetString(dr.GetOrdinal("Rik_Nombre"));
                        entNpsQuejaDetalle.Id_Cte = dr.GetInt32(dr.GetOrdinal("Id_Cte"));
                        entNpsQuejaDetalle.Cte_Nomcomercial = dr.GetString(dr.GetOrdinal("Cte_Nomcomercial"));
                        entNpsQuejaDetalle.Nps_Descripcion = dr.GetString(dr.GetOrdinal("Nps_Descripcion"));
                        entNpsQuejaDetalle.Nps_Tema = dr.GetString(dr.GetOrdinal("Nps_Tema"));

                        lstNpsQuejaDetalle.Add(entNpsQuejaDetalle);

                    }
                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarNpsReporteDetalle(string strConexion, ref List<NpsQueja_ReporteDetalle> lstNpsQuejaDetalle, Nps_Filtro_Busqueda entNpsFiltro)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(strConexion);
                NpsQueja_ReporteDetalle entNpsQuejaDetalle = new NpsQueja_ReporteDetalle();
                SqlDataReader dr = null; string[] Parametros = {
                                        "@Id_Cd",
                                        "@Cd_Nombre",
                                        "@Id_Rik",
                                        "@Id_Cte",
                                        "@Id_Nps_Estatus",
                                        "@Id_Nps_Tipo",
                                        "@Id_Nps_Tema",
                                        "@FechaInicial",
                                        "@FechaFinal",
                                        "@Buscar_Texto",
                                        "@Lst_UEN",
                                      };
                object[] Valores = {
                                        entNpsFiltro.Id_Cd,
                                        entNpsFiltro.Cd_Nombre,
                                        entNpsFiltro.Id_Rik,
                                        entNpsFiltro.Id_Cte,
                                        entNpsFiltro.Id_Nps_Estatus,
                                        entNpsFiltro.Id_Nps_Tipo,
                                        entNpsFiltro.Id_Nps_Tema,
                                        entNpsFiltro.FechaInicial,
                                        entNpsFiltro.FechaFinal,
                                        "-1",
                                        entNpsFiltro.strUEN
                                    };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatNps_Reporte_Detallado", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        entNpsQuejaDetalle = new NpsQueja_ReporteDetalle();

                        entNpsQuejaDetalle.Envio_Correo = dr.GetString(dr.GetOrdinal("Envio_Correo"));
                        entNpsQuejaDetalle.Nps_Estatus = dr.GetString(dr.GetOrdinal("Nps_Estatus"));
                        entNpsQuejaDetalle.Folio_Entrevista = dr.GetString(dr.GetOrdinal("Folio_Entrevista"));
                        entNpsQuejaDetalle.Fecha_Entrevista = dr.GetString(dr.GetOrdinal("Fecha_Entrevista"));
                        entNpsQuejaDetalle.Cte_Nomcomercial = dr.GetString(dr.GetOrdinal("Cte_Nomcomercial"));
                        entNpsQuejaDetalle.Cd_Nombre = dr.GetString(dr.GetOrdinal("Cd_Nombre"));
                        //Id_Nps_Tipo_Inicial
                        entNpsQuejaDetalle.Nps_Tipo_Inicial = dr.GetString(dr.GetOrdinal("Nps_Tipo_Inicial"));
                        entNpsQuejaDetalle.Nps_Valor_Inicial = dr["Nps_Valor_Inicial"].ToString();

                        entNpsQuejaDetalle.Rik_Nombre = dr.GetString(dr.GetOrdinal("Rik_Nombre"));
                        entNpsQuejaDetalle.Entrevistado = dr.GetString(dr.GetOrdinal("Entrevistado"));
                        entNpsQuejaDetalle.Puesto = dr.GetString(dr.GetOrdinal("Puesto"));
                        entNpsQuejaDetalle.Nps_Tema = dr.GetString(dr.GetOrdinal("Nps_Tema"));
                        entNpsQuejaDetalle.Nps_Queja = dr.GetString(dr.GetOrdinal("Nps_Queja"));

                        entNpsQuejaDetalle.Fecha_Asignado = dr.GetString(dr.GetOrdinal("Fecha_Asignado"));

                        entNpsQuejaDetalle.Nps_PlanAccion = dr.GetString(dr.GetOrdinal("Nps_PlanAccion"));
                        entNpsQuejaDetalle.Fecha_Compromiso = dr.GetString(dr.GetOrdinal("Fecha_Compromiso"));
                        entNpsQuejaDetalle.Fecha_EnDesarrollo = dr.GetString(dr.GetOrdinal("Fecha_EnDesarrollo"));
                        entNpsQuejaDetalle.Fecha_Atendido = dr.GetString(dr.GetOrdinal("Fecha_Atendido"));
                        entNpsQuejaDetalle.Fecha_Reenviado = dr.GetString(dr.GetOrdinal("Fecha_Reenviado"));

                        entNpsQuejaDetalle.Nps_PlanAccion_Segundo = dr.GetString(dr.GetOrdinal("Nps_PlanAccion_Segundo"));
                        entNpsQuejaDetalle.Fecha_Compromiso_Segundo = dr.GetString(dr.GetOrdinal("Fecha_Compromiso_Segundo"));
                        entNpsQuejaDetalle.Fecha_EnDesarrollo_Segundo = dr.GetString(dr.GetOrdinal("Fecha_EnDesarrollo_Segundo"));
                        entNpsQuejaDetalle.Fecha_Atendido_Segundo = dr.GetString(dr.GetOrdinal("Fecha_Atendido_Segundo"));
                        entNpsQuejaDetalle.Fecha_CierreCiclo = dr.GetString(dr.GetOrdinal("Fecha_CierreCiclo"));
                        //Id_Nps_Tipo_Segundo
                        entNpsQuejaDetalle.Nps_Tipo_Segundo = dr.GetString(dr.GetOrdinal("Nps_Tipo_Segundo"));
                        entNpsQuejaDetalle.Nps_Valor_Segundo = dr["Nps_Valor_Segundo"].ToString();
                        //Id_Nps_Tipo_Final
                        entNpsQuejaDetalle.Nps_Valor_Final = dr["Nps_Valor_Final"].ToString();
                        entNpsQuejaDetalle.Nps_Tipo_Final = dr["Nps_Tipo_Final"].ToString();
                        entNpsQuejaDetalle.Comentario = dr["Comentario"].ToString();

                        lstNpsQuejaDetalle.Add(entNpsQuejaDetalle);
                    }
                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarRiks(string strConexion, ref List<ItemsTextValue> lstData, int intIdSucursal)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(strConexion);
                ItemsTextValue item = new ItemsTextValue();
                SqlDataReader dr = null; string[] Parametros = {
                                        "@Id_Cd"
                                      };
                object[] Valores = {
                                       intIdSucursal
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatNps_ComboRIKFiltro", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        item = new ItemsTextValue();
                        item.intValue = dr.GetInt32(dr.GetOrdinal("Id"));
                        item.strText = dr.GetString(dr.GetOrdinal("Descripcion"));
                        if (item.intValue != 0)
                        {
                            lstData.Add(item);
                        }
                    }
                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarCliente(string strConexion, ref List<ItemsTextValue> lstData, int intIdSucursal, int intIdRiK)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(strConexion);
                ItemsTextValue item = new ItemsTextValue();
                SqlDataReader dr = null; string[] Parametros = {
                                        //"@Id_Cd",
                                        "@IdRik"
                                      };
                object[] Valores = {
                                        //intIdSucursal,
                                       intIdRiK,
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatNps_ComboClientesFiltro", ref dr, Parametros, Valores);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        item = new ItemsTextValue();
                        item.intValue = dr.GetInt32(dr.GetOrdinal("Id"));
                        item.strText = dr.GetString(dr.GetOrdinal("Descripcion"));
                        if (item.intValue != 0)
                        {
                            lstData.Add(item);
                        }
                    }
                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarNps(string strConexion, ref Nps entNps, ref List<NpsQueja> lstQueja, int intIdSucursal, int intIdNps)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(strConexion);
                NpsQueja entQueja = new NpsQueja();
                SqlDataReader dr = null;
                string[] Parametros = {
                                        "@Id_Cd",
                                        "@Id_Nps",
                                        "@Cd_Nombre"
                                      };
                object[] Valores = {
                                       intIdSucursal,
                                       intIdNps,
                                       ""
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatNps_Consulta", ref dr, Parametros, Valores);
                bool boolPrimerLinea = true;
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (boolPrimerLinea)
                        {
                            entNps.Id_Cd = dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                            entNps.Id_Nps = dr.GetInt32(dr.GetOrdinal("Id_Nps"));
                            entNps.Id_Rik = dr.GetInt32(dr.GetOrdinal("Id_Rik"));
                            entNps.Id_Cte = dr.GetInt32(dr.GetOrdinal("Id_Cte"));

                            entNps.Folio_Entrevista = dr.GetString(dr.GetOrdinal("Folio_Entrevista"));
                            entNps.Fecha_Entrevista = (DateTime)dr["Fecha_Entrevista"];
                            entNps.strFecha_Entrevista = entNps.Fecha_Entrevista.ToString("yyyy-MM-ddTHH:mm:ss");
                            entNps.Entrevistado = dr.GetString(dr.GetOrdinal("Entrevistado"));
                            entNps.Puesto = dr.GetString(dr.GetOrdinal("Puesto"));
                            entNps.Cd_Nombre = dr.GetString(dr.GetOrdinal("Cd_Nombre"));
                            entNps.Rik_Nombre = dr.GetString(dr.GetOrdinal("Rik_Nombre"));
                            entNps.Cte_NomComercial = dr.GetString(dr.GetOrdinal("Cte_NomComercial"));
                            entNps.Id_Nps_Estatus = dr.GetInt32(dr.GetOrdinal("Id_Nps_Estatus"));
                            entNps.Nps_Estatus = dr.GetString(dr.GetOrdinal("Nps_Estatus"));

                            entNps.Nps_Valor_Inicial = dr.GetInt32(dr.GetOrdinal("Nps_Valor_Inicial"));
                            entNps.Nps_Tipo_Inicial = dr.GetString(dr.GetOrdinal("Nps_Tipo_Inicial"));
                            entNps.Id_Nps_Tipo_Inicial = dr.GetInt32(dr.GetOrdinal("Id_Nps_Tipo_Inicial"));

                            entNps.Nps_Valor_Segundo = dr.GetInt32(dr.GetOrdinal("Nps_Valor_Secundario"));
                            entNps.Nps_Tipo_Segundo = dr.GetString(dr.GetOrdinal("Nps_Tipo_Secundario"));
                            entNps.Id_Nps_Tipo_Segundo = dr.GetInt32(dr.GetOrdinal("Id_Nps_Tipo_Secundario"));

                            entNps.Nps_Valor_Final = dr.GetInt32(dr.GetOrdinal("Nps_Valor_Final"));
                            entNps.Nps_Tipo_Final = dr.GetString(dr.GetOrdinal("Nps_Tipo_Final"));
                            entNps.Id_Nps_Tipo_Final = dr.GetInt32(dr.GetOrdinal("Id_Nps_Tipo_Final"));

                            boolPrimerLinea = false;
                        }

                        entQueja = new NpsQueja();

                        entQueja.Id_Nps_Queja = dr.GetInt32(dr.GetOrdinal("Id_Nps_Queja"));
                        entQueja.Id_Nps_Tema = dr.GetInt32(dr.GetOrdinal("Id_Nps_Tema"));
                        entQueja.Nps_Tema = dr.GetString(dr.GetOrdinal("Nps_Tema"));
                        entQueja.Otro_Tema = dr.GetString(dr.GetOrdinal("Otro_Tema"));
                        if (entQueja.Id_Nps_Tema == 10)
                        {
                            entQueja.Nps_Tema += ": " + entQueja.Otro_Tema;
                        }
                        entQueja.Nps_Queja = dr.GetString(dr.GetOrdinal("Nps_Queja"));
                        entQueja.Nps_Protocolo = dr.GetString(dr.GetOrdinal("Nps_Protocolo"));
                        entQueja.Nps_PlanAccion = dr.GetString(dr.GetOrdinal("Nps_PlanAccion"));
                        entQueja.strFecha_Compromiso = dr.GetString(dr.GetOrdinal("Fecha_Compromiso"));
                        if (!string.IsNullOrEmpty(entQueja.strFecha_Compromiso))
                        {
                            entQueja.Fecha_Compromiso = DateTime.Parse(entQueja.strFecha_Compromiso);
                            entQueja.strFecha_Compromiso = entQueja.Fecha_Compromiso.ToString("yyyy-MM-ddTHH:mm:ss");
                        }
                        entQueja.Nps_PlanAccion_Segundo = dr.GetString(dr.GetOrdinal("Nps_PlanAccion_Segundo"));
                        entQueja.strFecha_Compromiso_Segundo = dr.GetString(dr.GetOrdinal("Fecha_Compromiso_Segundo"));
                        if (!string.IsNullOrEmpty(entQueja.strFecha_Compromiso_Segundo))
                        {
                            entQueja.Fecha_Compromiso_Segundo = DateTime.Parse(entQueja.strFecha_Compromiso_Segundo);
                            entQueja.strFecha_Compromiso_Segundo = entQueja.Fecha_Compromiso_Segundo.ToString("yyyy-MM-ddTHH:mm:ss");
                        }

                        lstQueja.Add(entQueja);

                    }
                }

                /* 
                  entQueja.strFecha_Compromiso = dr.GetString(dr.GetOrdinal("Fecha_Compromiso"));

                        if (!string.IsNullOrEmpty(entQueja.strFecha_Compromiso))
                        {
                            entQueja.Fecha_Compromiso = DateTime.Parse(entQueja.strFecha_Compromiso);
                            entQueja.strFecha_Compromiso = entQueja.Fecha_Compromiso.ToString("yyyy-MM-ddTHH:mm:ss");
                        }
                */
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GuardarPlan(string strConexion, int intIdEmp, int intIdSucursal, int intIdUsuario, int intIdNps, List<NpsQueja> lstQueja, int intConcluido, int intPlan_Consecutivo, ref entRespuestaNps entResultado)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(strConexion);
                SqlDataReader dr = null;
                DateTime dateCompromiso;
                SqlCommand sqlcmd = new SqlCommand();

                foreach (var itemQueja in lstQueja)
                {
                    dr = null;

                    dateCompromiso = DateTime.Parse(itemQueja.strFecha_Compromiso);


                    string[] Parametros = {
                                        "@Id_Cd",
                                        "@Id_Emp",
                                        "@Id_U",
                                        "@Id_Nps",
                                        "@Id_Nps_Queja",
                                        "@strPlan",
                                        "@FechaCompromiso",
                                        "@Concluido",
                                        "@Plan_Consecutivo"
                                      };

                    object[] Valores = {
                                       intIdSucursal,
                                       intIdEmp,
                                       intIdUsuario,
                                       intIdNps,
                                       itemQueja.Id_Nps_Queja,
                                       itemQueja.Nps_PlanAccion,
                                       dateCompromiso.ToString("yyyy-MM-dd"),
                                       intConcluido,
                                       intPlan_Consecutivo
                                   };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spCatNps_GuardarPlan", ref dr, Parametros, Valores);
                    if (dr.HasRows)
                    {
                        if (dr.Read())
                        {
                            entResultado.intValor = dr.GetInt32(dr.GetOrdinal("Resultado"));
                            entResultado.strMensaje = dr.GetString(dr.GetOrdinal("Mensaje"));
                        }
                    }

                    dr.Close();

                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ConsultarNpsEditar(string strConexion, ref Nps entNps, ref List<NpsQueja> lstQueja, int intIdSucursal, int intNPS)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(strConexion);
                NpsQueja entQueja = new NpsQueja();
                //List<NpsQueja> lstQueja = new List<NpsQueja>();

                //Nps entNps = new Nps();

                SqlDataReader dr = null; string[] Parametros = {
                                        "@Id_Cd",
                                        "@Id_Nps",
                                        "@Cd_Nombre"
                                      };
                object[] Valores = {
                                        intIdSucursal,
                                        intNPS,
                                        ""
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatNps_Consulta", ref dr, Parametros, Valores);
                bool boolPrimerLinea = true;
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (boolPrimerLinea)
                        {
                            entNps.Id_Cd = dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                            entNps.Id_Nps = dr.GetInt32(dr.GetOrdinal("Id_Nps"));
                            entNps.Id_Rik = dr.GetInt32(dr.GetOrdinal("Id_Rik"));
                            entNps.Id_Cte = dr.GetInt32(dr.GetOrdinal("Id_Cte"));
                            entNps.Id_Nps_Tipo = dr.GetInt32(dr.GetOrdinal("Id_Nps_Tipo"));
                            entNps.Nps_Valor = dr.GetInt32(dr.GetOrdinal("Nps_Valor"));

                            entNps.Folio_Entrevista = dr.GetString(dr.GetOrdinal("Folio_Entrevista"));
                            entNps.Fecha_Entrevista = (DateTime)dr["Fecha_Entrevista"];
                            entNps.Entrevistado = dr.GetString(dr.GetOrdinal("Entrevistado"));
                            entNps.Puesto = dr.GetString(dr.GetOrdinal("Puesto"));

                            boolPrimerLinea = false;
                        }

                        entQueja = new NpsQueja();



                        entQueja.Id_Nps_Queja = dr.GetInt32(dr.GetOrdinal("Id_Nps_Queja"));
                        entQueja.Id_Nps_Tema = dr.GetInt32(dr.GetOrdinal("Id_Nps_Tema"));
                        entQueja.Otro_Tema = dr.GetString(dr.GetOrdinal("Otro_Tema"));
                        entQueja.Nps_Valor = dr.GetInt32(dr.GetOrdinal("Nps_Valor"));
                        entQueja.Id_Nps_Tipo = dr.GetInt32(dr.GetOrdinal("Id_Nps_Tipo"));
                        entQueja.Nps_Queja = dr.GetString(dr.GetOrdinal("Nps_Queja"));
                        entQueja.Nps_Protocolo = dr.GetString(dr.GetOrdinal("Nps_Protocolo"));
                        entQueja.Id_Nps_EstatusActual = dr.GetInt32(dr.GetOrdinal("Id_Nps_EstatusActual"));
                        //entQueja.Nps_Estatus

                        lstQueja.Add(entQueja);

                    }
                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GuardarNps(string strConexion, Nps entNps, List<NpsQueja> lstQueja)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(strConexion);

                SqlDataReader dr = null;
                SqlDataReader dr2 = null;
                string[] Parametros = {
                                        "@Id_Cd",
                                        "@Id_Rik",
                                        "@Id_Clt",
                                        "@Id_Nps_Tipo",
                                        "@Nps_Valor",
                                        "@Folio_Entrevista",
                                        "@Fecha_Entrevista",
                                        "@Entrevistado",
                                        "@Puesto",
                                        "@Id_U_Ins"

                                      };
                object[] Valores = {
                    entNps.Id_Cd,
                    entNps.Id_Rik,
                    entNps.Id_Cte,
                    entNps.Id_Nps_Tipo,
                    entNps.Nps_Valor,
                    entNps.Folio_Entrevista,
                    entNps.Fecha_Entrevista,
                    entNps.Entrevistado,
                    entNps.Puesto,
                    entNps.Id_U_Ins
                };
                int intIdNps = 0;
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatNps_Insertar", ref dr, Parametros, Valores);
                if (dr.Read())
                {
                    if (!string.IsNullOrEmpty(dr["Id_Nps"].ToString()))
                    {
                        intIdNps = Convert.ToInt32(dr["Id_Nps"]);
                    }


                }

                if (intIdNps != 0)
                {

                }
                else
                {
                    intIdNps = 4;
                }

                dr.Close();
                foreach (var itemQueja in lstQueja)
                {
                    dr2 = null;
                    string[] Parametros2 = {
                            "@Id_Cd",
                            "@Id_Nps",
                            "@Id_Nps_Tema",
                            "@Otro_Tema",
                            "@Nps_Valor",
                            "@Id_Nps_Tipo",
                            "@Nps_Queja",
                            "@Nps_Protocolo",
                            "@Nps_Valor_Final",
                            "@Id_Nps_EstatusActual"
                        };

                    object[] Valores2 = {
                            entNps.Id_Cd,
                            intIdNps,
                            itemQueja.Id_Nps_Tema,
                             "",
                            itemQueja.Nps_Valor,
                            itemQueja.Id_Nps_Tipo,
                            itemQueja.Nps_Queja,
                            itemQueja.Nps_Protocolo,
                            itemQueja.Nps_Valor_Final,
                            1,
                        };

                    sqlcmd = CapaDatos.GenerarSqlCommand("spCatNpsQueja_Insertar", ref dr2, Parametros2, Valores2);
                    dr2.Close();
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void IndicadorGeneralNps(string strConexion, int intIdEmp, int intIdSucursal, entNpsIndicadorFiltro entFiltros, ref List<Nps_IndicadorNps> lstIndicadorNps)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(strConexion);
                Nps_IndicadorNps itemIndicadorNps = new Nps_IndicadorNps();
                //List<Nps_IndicadorNps> lstIndicadorNps = new List<Nps_IndicadorNps>();
                SqlDataReader dr = null;
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Lst_UEN",
                                        "@intRik",
                                        "@Anio_Iniciar",
                                        "@Mes_Inicial",
                                        "@Anio_Final",
                                        "@Mes_Final"

                                      };
                object[] Valores = {
                                        intIdEmp,
                                        intIdSucursal,
                                        entFiltros.strUEN,
                                        entFiltros.intRik,
                                        entFiltros.dateInicial.Year.ToString(),
                                        entFiltros.dateInicial.Month.ToString("d2"),
                                        entFiltros.dateFinal.Year.ToString(),
                                        entFiltros.dateFinal.Month.ToString("d2"),
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatNps_IndicadorNps", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    itemIndicadorNps = new Nps_IndicadorNps();
                    itemIndicadorNps.Id_Nps_Tipo = dr.GetInt32(dr.GetOrdinal("Id_Nps_Tipo"));
                    itemIndicadorNps.Nps_descripcion = dr["Nps_descripcion"].ToString();
                    itemIndicadorNps.Total = dr.GetInt32(dr.GetOrdinal("Total"));
                    itemIndicadorNps.GranTotal = int.Parse(dr["GranTotal"].ToString());
                    itemIndicadorNps.Porcentaje = decimal.Parse(dr["Porcentaje"].ToString());

                    lstIndicadorNps.Add(itemIndicadorNps);
                }


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void IndicadorTrazabilidad(string strConexion, int intIdEmp, int intIdSucursal, entNpsIndicadorFiltro entFiltros,
             ref List<Nps_IndicadorTrazabilidad> lstIndicadorTrazabilidad, ref List<Nps_IndicadorTrazabilidadCliente> lstIndicadorTrazabilidadCliente)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(strConexion);
                Nps_IndicadorTrazabilidad entTrazabilidad = new Nps_IndicadorTrazabilidad();
                Nps_IndicadorTrazabilidadCliente entTrazabilidadCliente = new Nps_IndicadorTrazabilidadCliente();

                SqlDataReader dr = null;
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Cd_Nombre",
                                        "@Lst_Estatus",
                                        "@Lst_UEN",
                                        "@intRik",
                                        "@Anio_Iniciar",
                                        "@Mes_Inicial",
                                        "@Anio_Final",
                                        "@Mes_Final"

                                      };
                object[] Valores = {
                                        intIdEmp,
                                        intIdSucursal,
                                        "",
                                        entFiltros.strStatus,
                                        entFiltros.strUEN,
                                        entFiltros.intRik,
                                        entFiltros.dateInicial.Year.ToString(),
                                        entFiltros.dateInicial.Month.ToString("d2"),
                                        entFiltros.dateFinal.Year.ToString(),
                                        entFiltros.dateFinal.Month.ToString("d2"),
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatNps_Trazabilidad", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    entTrazabilidad = new Nps_IndicadorTrazabilidad();

                    entTrazabilidad.Id_Nps_Estatus = dr.GetInt32(dr.GetOrdinal("Id_Nps_Estatus"));
                    entTrazabilidad.Estatus_Descripcion = dr["Estatus_Descripcion"].ToString();
                    entTrazabilidad.Total = dr.GetInt32(dr.GetOrdinal("Total"));
                    entTrazabilidad.GranTotal = int.Parse(dr["GranTotal"].ToString());
                    entTrazabilidad.Porcentaje = decimal.Parse(dr["Porcentaje"].ToString());

                    lstIndicadorTrazabilidad.Add(entTrazabilidad);
                }

                dr.NextResult();
                while (dr.Read())
                {
                    entTrazabilidadCliente = new Nps_IndicadorTrazabilidadCliente();

                    entTrazabilidadCliente.Cte_NomComercial = dr["Cte_NomComercial"].ToString();
                    entTrazabilidadCliente.Total_Estatus1 = dr.GetInt32(dr.GetOrdinal("Total_Estatus1"));
                    entTrazabilidadCliente.Total_Estatus2 = dr.GetInt32(dr.GetOrdinal("Total_Estatus2"));
                    entTrazabilidadCliente.Total_Estatus3 = dr.GetInt32(dr.GetOrdinal("Total_Estatus3"));
                    entTrazabilidadCliente.Total_Estatus5 = dr.GetInt32(dr.GetOrdinal("Total_Estatus5"));
                    entTrazabilidadCliente.Total_Estatus4 = dr.GetInt32(dr.GetOrdinal("Total_Estatus4"));
                    entTrazabilidadCliente.Porcentaje_Estatus1 = decimal.Parse(dr["Porcentaje_Estatus1"].ToString());
                    entTrazabilidadCliente.Porcentaje_Estatus2 = decimal.Parse(dr["Porcentaje_Estatus2"].ToString());
                    entTrazabilidadCliente.Porcentaje_Estatus3 = decimal.Parse(dr["Porcentaje_Estatus3"].ToString());
                    entTrazabilidadCliente.Porcentaje_Estatus5 = decimal.Parse(dr["Porcentaje_Estatus5"].ToString());
                    entTrazabilidadCliente.Porcentaje_Estatus4 = decimal.Parse(dr["Porcentaje_Estatus4"].ToString());


                    lstIndicadorTrazabilidadCliente.Add(entTrazabilidadCliente);
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void IndicadorConversion(string strConexion, int intIdEmp, int intIdSucursal, entNpsIndicadorFiltro entFiltros, ref List<Nps_IndicadorConversion> lstIndicadorConversion)
        {
            try
            {
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(strConexion);
                Nps_IndicadorConversion entConversion = new Nps_IndicadorConversion();

                SqlDataReader dr = null;
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd",
                                        "@Cd_Nombre",
                                        "@Lst_UEN",
                                        "@intRik",
                                        "@Anio_Iniciar",
                                        "@Mes_Inicial",
                                        "@Anio_Final",
                                        "@Mes_Final"
                                      };
                object[] Valores = {
                                        intIdEmp,
                                        intIdSucursal,
                                        "",
                                        entFiltros.strUEN,
                                        entFiltros.intRik,
                                        entFiltros.dateInicial.Year.ToString(),
                                        entFiltros.dateInicial.Month.ToString("d2"),
                                        entFiltros.dateFinal.Year.ToString(),
                                        entFiltros.dateFinal.Month.ToString("d2"),
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCatNps_Conversion", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    entConversion = new Nps_IndicadorConversion();
                    entConversion.Cte_NomComercial = dr["Cte_NomComercial"].ToString();
                    entConversion.Id_Nps_Tipo_Inicial = dr.GetInt32(dr.GetOrdinal("Id_Nps_Tipo_Inicial"));
                    entConversion.Tipo_Inicial = dr["Tipo_Inicial"].ToString();
                    entConversion.Valor_Inicial = dr.GetInt32(dr.GetOrdinal("Valor_Inicial"));
                    entConversion.Id_Nps_Tipo_Final = dr.GetInt32(dr.GetOrdinal("Id_Nps_Tipo_Final"));
                    entConversion.Tipo_Final = dr["Tipo_Final"].ToString();
                    entConversion.Valor_Final = dr.GetInt32(dr.GetOrdinal("Valor_Final"));
                    entConversion.Valor_Conversion = dr.GetInt32(dr.GetOrdinal("Valor_Conversion"));
                    entConversion.Porcentaje_Conversion = dr.GetInt32(dr.GetOrdinal("Porcentaje_Conversion"));

                    lstIndicadorConversion.Add(entConversion);
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