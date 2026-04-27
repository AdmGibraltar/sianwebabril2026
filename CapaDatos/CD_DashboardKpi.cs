using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;
using CapaModelo;
using System.Data.SqlTypes;
using System.Data.Common;
using System.Data;

namespace CapaDatos
{
    public class CD_DashboardKpi
    {
        public void ConsultarKpiDiario(string strConexion, int intIdEmp, int intIdCd, int intIdU, ref entDashboardKpiDiario entDsKpi, ref List<entDashboardKpiDiarioRik> lstDsKpiRik)
        {
            try
            {


                entDashboardKpiDiarioRik entDsKpiRik = new entDashboardKpiDiarioRik();

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(strConexion);
                NpsQueja_Detalle entNpsQuejaDetalle = new NpsQueja_Detalle();
                SqlDataReader dr = null;
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd"
                };
                object[] Valores = {
                                       intIdEmp,
                                       intIdCd
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("ConsultarDashboardKpiDiario", ref dr, Parametros, Valores);

                while (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (ExisteColumna(dr, "NombreCD"))
                        {
                            entDsKpi.NombreCD = dr.GetString(dr.GetOrdinal("NombreCD"));
                            entDsKpi.utilidadPPTOPorcentaje = dr.GetDecimal(dr.GetOrdinal("utilidadPPTOPorcentaje"));
                            entDsKpi.utilidadDiaPorcentaje = dr.GetDecimal(dr.GetOrdinal("utilidadDiaPorcentaje"));
                            entDsKpi.utilidadPptoMoneda = dr.GetDecimal(dr.GetOrdinal("utilidadPptoMoneda"));
                            entDsKpi.utilidadDiaMoneda = dr.GetDecimal(dr.GetOrdinal("utilidadDiaMoneda"));
                            entDsKpi.fechaDashBoard = dr.GetDateTime(dr.GetOrdinal("fechaDashBoard"));

                            entDsKpi.presupuestoGnrl = dr.GetDecimal(dr.GetOrdinal("presupuestoGnrl"));
                            entDsKpi.presupuestoGnrlRestante = dr.GetDecimal(dr.GetOrdinal("presupuestoGnrlRestante"));
                            entDsKpi.cumplimientoGnrl = dr.GetDecimal(dr.GetOrdinal("cumplimientoGnrl"));
                            entDsKpi.remisionesVigentes = dr.GetDecimal(dr.GetOrdinal("remisionesVigentes"));
                            entDsKpi.remisionesVencidas = dr.GetDecimal(dr.GetOrdinal("remisionesVencidas"));

                            entDsKpi.remisionesPxFGnrl = dr.GetDecimal(dr.GetOrdinal("remisionesPxFGnrl"));

                            entDsKpi.ttlCarteraCobranza = dr.GetDecimal(dr.GetOrdinal("ttlCarteraCobranza"));
                            entDsKpi.ttlCarteraTiempo = dr.GetDecimal(dr.GetOrdinal("ttlCarteraTiempo"));
                            entDsKpi.ttlCarteraMenos30dias = dr.GetDecimal(dr.GetOrdinal("ttlCarteraMenos30dias"));
                            entDsKpi.ttlCarteraMas30dias = dr.GetDecimal(dr.GetOrdinal("ttlCarteraMas30dias"));

                            entDsKpi.ImporteBaja = dr.GetDecimal(dr.GetOrdinal("ImporteBaja"));
                            entDsKpi.ImporteRefacturado = dr.GetDecimal(dr.GetOrdinal("ImporteRefacturado"));
                            entDsKpi.ImporteFacturas = dr.GetDecimal(dr.GetOrdinal("ImporteFacturas"));
                            entDsKpi.ImporteGeneral = dr.GetDecimal(dr.GetOrdinal("ImporteGeneral"));

                            entDsKpi.NumBaja = dr.GetInt32(dr.GetOrdinal("NumBajas"));
                            entDsKpi.NumRefacturado = dr.GetInt32(dr.GetOrdinal("NumRefacturado"));
                            entDsKpi.NumFacturas = dr.GetInt32(dr.GetOrdinal("NumFacturas"));

                        }
                        if (ExisteColumna(dr, "NombreRik"))
                        {
                            entDsKpiRik = new entDashboardKpiDiarioRik();
                            entDsKpiRik.NombreRik = dr.GetString(dr.GetOrdinal("NombreRik"));
                            entDsKpiRik.cumplimientoRik = dr.GetDecimal(dr.GetOrdinal("cumplimientoRik"));
                            entDsKpiRik.presupuestoRik = dr.GetDecimal(dr.GetOrdinal("presupuestoRik"));
                            entDsKpiRik.crecimientoRik = dr.GetDecimal(dr.GetOrdinal("crecimientoRik"));
                            entDsKpiRik.cteActivoRik = dr.GetInt32(dr.GetOrdinal("cteActivoRik"));
                            entDsKpiRik.carteraTiempoRik = dr.GetDecimal(dr.GetOrdinal("carteraTiempoRik"));
                            entDsKpiRik.carteraVencidaRik = dr.GetDecimal(dr.GetOrdinal("carteraVencidaRik"));
                            entDsKpiRik.remisionesTiempoRik = dr.GetDecimal(dr.GetOrdinal("remisionesTiempoRik"));
                            entDsKpiRik.remisionesVencidaRik = dr.GetDecimal(dr.GetOrdinal("remisionesVencidaRik"));

                            entDsKpiRik.UBPprcemtaje = dr.GetDecimal(dr.GetOrdinal("UBPprcemtaje"));

                            if (!entDsKpiRik.NombreRik.ToUpper().Contains("TOTAL"))
                            {
                                lstDsKpiRik.Add(entDsKpiRik);
                            }

                            /*
                           
                            */
                        }

                    }
                    dr.NextResult();
                }



                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultarKpiDiarioDetalle(string strConexion, int intIdEmp, int intIdCd, int intIdU, ref entDashboardKpiDiario entDsKpi, ref List<entDashboardKpiDiarioRik> lstDsKpiRik, ref entDashboardKpiDetalle entDsKpiDetalle)
        {
            try
            {
                List<entDashboardKpiEquipo> lstDashboardKpiEquipo = new List<entDashboardKpiEquipo>();
                List<entDashboardKpiDetalleRik> lstDashboardKpiDetalleRik = new List<entDashboardKpiDetalleRik>();
                List<entDashboardKpiDetalleAsc> lstDashboardKpiDetalleAsc = new List<entDashboardKpiDetalleAsc>();
                List<entDashboardKpiEquipoRik> lstDashboardKpiEquipoRik = new List<entDashboardKpiEquipoRik>();

                entDashboardKpiEquipo entKpiEquipo = new entDashboardKpiEquipo();
                entDashboardKpiDetalleRik entKpiDetalleRik = new entDashboardKpiDetalleRik();

                entDashboardKpiDiarioRik entDsKpiRik = new entDashboardKpiDiarioRik();
                entDashboardKpiDetalleAsc entKpiDetalleAsc = new entDashboardKpiDetalleAsc();


                string strIntegrante;
                string strEquipoNombre;

                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(strConexion);
                NpsQueja_Detalle entNpsQuejaDetalle = new NpsQueja_Detalle();
                SqlDataReader dr = null;
                string[] Parametros = {
                                        "@Id_Emp",
                                        "@Id_Cd"
                };
                object[] Valores = {
                                       intIdEmp,
                                       intIdCd
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("ConsultarDashboardKpiDiario", ref dr, Parametros, Valores);

                while (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (ExisteColumna(dr, "NombreCD"))
                        {
                            entDsKpi.NombreCD = dr.GetString(dr.GetOrdinal("NombreCD"));
                            entDsKpi.utilidadPPTOPorcentaje = dr.GetDecimal(dr.GetOrdinal("utilidadPPTOPorcentaje"));
                            entDsKpi.utilidadDiaPorcentaje = dr.GetDecimal(dr.GetOrdinal("utilidadDiaPorcentaje"));
                            entDsKpi.utilidadPptoMoneda = dr.GetDecimal(dr.GetOrdinal("utilidadPptoMoneda"));
                            entDsKpi.utilidadDiaMoneda = dr.GetDecimal(dr.GetOrdinal("utilidadDiaMoneda"));
                            entDsKpi.fechaDashBoard = dr.GetDateTime(dr.GetOrdinal("fechaDashBoard"));

                            entDsKpi.presupuestoGnrl = dr.GetDecimal(dr.GetOrdinal("presupuestoGnrl"));
                            entDsKpi.presupuestoGnrlRestante = dr.GetDecimal(dr.GetOrdinal("presupuestoGnrlRestante"));
                            entDsKpi.cumplimientoGnrl = dr.GetDecimal(dr.GetOrdinal("cumplimientoGnrl"));
                            entDsKpi.remisionesVigentes = dr.GetDecimal(dr.GetOrdinal("remisionesVigentes"));
                            entDsKpi.remisionesVencidas = dr.GetDecimal(dr.GetOrdinal("remisionesVencidas"));

                            entDsKpi.remisionesPxFGnrl = dr.GetDecimal(dr.GetOrdinal("remisionesPxFGnrl"));

                            entDsKpi.ttlCarteraCobranza = dr.GetDecimal(dr.GetOrdinal("ttlCarteraCobranza"));
                            entDsKpi.ttlCarteraTiempo = dr.GetDecimal(dr.GetOrdinal("ttlCarteraTiempo"));
                            entDsKpi.ttlCarteraMenos30dias = dr.GetDecimal(dr.GetOrdinal("ttlCarteraMenos30dias"));
                            entDsKpi.ttlCarteraMas30dias = dr.GetDecimal(dr.GetOrdinal("ttlCarteraMas30dias"));

                            entDsKpiDetalle.ventaPptoDiario = dr.GetDecimal(dr.GetOrdinal("ventaPptoDiario"));
                            entDsKpiDetalle.ventaPptoAcumulado = dr.GetDecimal(dr.GetOrdinal("ventaPptoAcumulado"));
                            entDsKpiDetalle.ventaDiaHoy = dr.GetDecimal(dr.GetOrdinal("ventaDiaHoy"));
                            entDsKpiDetalle.ventaComplementoAcumulado = dr.GetDecimal(dr.GetOrdinal("ventaComplementoAcumulado"));
                            entDsKpiDetalle.contribucionPptoDiario = dr.GetDecimal(dr.GetOrdinal("contribucionPptoDiario"));
                            entDsKpiDetalle.contribucionPptoAcumulado = dr.GetDecimal(dr.GetOrdinal("contribucionPptoAcumulado"));
                            entDsKpiDetalle.contribucionDiaHoy = dr.GetDecimal(dr.GetOrdinal("contribucionDiaHoy"));
                            entDsKpiDetalle.contribucionComplementoAcumulado = dr.GetDecimal(dr.GetOrdinal("contribucionComplementoAcumulado"));
                            entDsKpiDetalle.contribucionRealPromedio = dr.GetDecimal(dr.GetOrdinal("contribucionRealPromedio"));
                            entDsKpiDetalle.ventaRealPromedio = dr.GetDecimal(dr.GetOrdinal("ventaRealPromedio"));
                            entDsKpiDetalle.famQuimicoPpto = dr.GetDecimal(dr.GetOrdinal("famQuimicoPpto"));
                            entDsKpiDetalle.famQuimicoVenta = dr.GetDecimal(dr.GetOrdinal("famQuimicoVenta"));
                            entDsKpiDetalle.famQuimicoCumplimiento = dr.GetDecimal(dr.GetOrdinal("famQuimicoCumplimiento"));
                            entDsKpiDetalle.famPapelPpto = dr.GetDecimal(dr.GetOrdinal("famPapelPpto"));
                            entDsKpiDetalle.famPapelVenta = dr.GetDecimal(dr.GetOrdinal("famPapelVenta"));
                            entDsKpiDetalle.famPapelCumplimiento = dr.GetDecimal(dr.GetOrdinal("famPapelCumplimiento"));
                            entDsKpiDetalle.famOtroPpto = dr.GetDecimal(dr.GetOrdinal("famOtroPpto"));
                            entDsKpiDetalle.famOtroVenta = dr.GetDecimal(dr.GetOrdinal("famOtroVenta"));
                            entDsKpiDetalle.famOtroCumplimiento = dr.GetDecimal(dr.GetOrdinal("famOtroCumplimiento"));

                        }
                        else if (ExisteColumna(dr, "NombreRik"))
                        {
                            entDsKpiRik = new entDashboardKpiDiarioRik();
                            entKpiDetalleRik = new entDashboardKpiDetalleRik();

                            entDsKpiRik.NombreRik = dr.GetString(dr.GetOrdinal("NombreRik"));
                            entDsKpiRik.cumplimientoRik = dr.GetDecimal(dr.GetOrdinal("cumplimientoRik"));
                            entDsKpiRik.presupuestoRik = dr.GetDecimal(dr.GetOrdinal("presupuestoRik"));
                            entDsKpiRik.crecimientoRik = dr.GetDecimal(dr.GetOrdinal("crecimientoRik"));
                            entDsKpiRik.cteActivoRik = dr.GetInt32(dr.GetOrdinal("cteActivoRik"));
                            entDsKpiRik.carteraTiempoRik = dr.GetDecimal(dr.GetOrdinal("carteraTiempoRik"));
                            entDsKpiRik.carteraVencidaRik = dr.GetDecimal(dr.GetOrdinal("carteraVencidaRik"));
                            entDsKpiRik.remisionesTiempoRik = dr.GetDecimal(dr.GetOrdinal("remisionesTiempoRik"));
                            entDsKpiRik.remisionesVencidaRik = dr.GetDecimal(dr.GetOrdinal("remisionesVencidaRik"));

                            entDsKpiRik.UBPprcemtaje = dr.GetDecimal(dr.GetOrdinal("UBPprcemtaje"));

                            if (!entDsKpiRik.NombreRik.ToUpper().Contains("TOTAL"))
                            {
                                lstDsKpiRik.Add(entDsKpiRik);
                            }


                            entKpiDetalleRik.NombreRik = dr.GetString(dr.GetOrdinal("NombreRik"));
                            entKpiDetalleRik.presupuestoMensual = dr.GetDecimal(dr.GetOrdinal("presupuestoMensual"));
                            entKpiDetalleRik.presupuestoQuimico = dr.GetDecimal(dr.GetOrdinal("presupuestoQuimico"));
                            entKpiDetalleRik.cumplimientoQuimico = dr.GetDecimal(dr.GetOrdinal("cumplimientoQuimico"));
                            entKpiDetalleRik.margenGeneralQuimico = dr.GetDecimal(dr.GetOrdinal("margenGeneralQuimico"));
                            entKpiDetalleRik.presupuestoPapel = dr.GetDecimal(dr.GetOrdinal("presupuestoPapel"));
                            entKpiDetalleRik.cumplimientoPapel = dr.GetDecimal(dr.GetOrdinal("cumplimientoPapel"));
                            entKpiDetalleRik.margenGeneralPapel = dr.GetDecimal(dr.GetOrdinal("margenGeneralPapel"));
                            entKpiDetalleRik.presupuestoOtro = dr.GetDecimal(dr.GetOrdinal("presupuestoOtro"));
                            entKpiDetalleRik.cumplimientoOtro = dr.GetDecimal(dr.GetOrdinal("cumplimientoOtro"));
                            entKpiDetalleRik.margenGeneralOtro = dr.GetDecimal(dr.GetOrdinal("margenGeneralOtro"));
                            entKpiDetalleRik.ventaNuevaPresupuesto = dr.GetDecimal(dr.GetOrdinal("ventaNuevaPresupuesto"));
                            entKpiDetalleRik.ventaNueva = dr.GetDecimal(dr.GetOrdinal("ventaNueva"));
                            entKpiDetalleRik.clientesNuevos = dr.GetInt32(dr.GetOrdinal("clientesNuevos"));

                            lstDashboardKpiDetalleRik.Add(entKpiDetalleRik);
                            /*}
                             */


                        }
                        else if (ExisteColumna(dr, "equipoNombre"))
                        {

                            if (ExisteColumna(dr, "equipoIntegrante"))
                            {
                                strIntegrante = dr.GetString(dr.GetOrdinal("equipoIntegrante"));
                                strEquipoNombre = dr.GetString(dr.GetOrdinal("equipoNombre"));
                                lstDashboardKpiEquipoRik.Add(new entDashboardKpiEquipoRik
                                {
                                    equipoIntegrante = strIntegrante,
                                    equipoNombre = strEquipoNombre
                                });
                            }
                            else
                            {
                                entKpiEquipo = new entDashboardKpiEquipo();
                                entKpiEquipo.equipoNombre = dr.GetString(dr.GetOrdinal("equipoNombre"));
                                entKpiEquipo.equipoPresupuesto = dr.GetDecimal(dr.GetOrdinal("equipoPresupuesto"));
                                entKpiEquipo.equipoCumplimiento = dr.GetDecimal(dr.GetOrdinal("equipoCumplimiento"));
                                entKpiEquipo.equipoClientes = dr.GetInt32(dr.GetOrdinal("equipoClientes"));
                                entKpiEquipo.equipoCarteraTiempo = dr.GetDecimal(dr.GetOrdinal("equipoCarteraTiempo"));
                                entKpiEquipo.equipoCarteraVencido = dr.GetDecimal(dr.GetOrdinal("equipoCarteraVencido"));

                                lstDashboardKpiEquipo.Add(entKpiEquipo);
                            }

                            /*
                             * 
                            */
                        }
                        else if (ExisteColumna(dr, "ascNombre"))
                        {
                            entKpiDetalleAsc = new entDashboardKpiDetalleAsc();

                            entKpiDetalleAsc.ascNombre = dr.GetString(dr.GetOrdinal("ascNombre"));
                            entKpiDetalleAsc.ascPresupuesto = dr.GetDecimal(dr.GetOrdinal("ascPresupuesto"));
                            entKpiDetalleAsc.ascCumplimiento = dr.GetDecimal(dr.GetOrdinal("ascCumplimiento"));
                            entKpiDetalleAsc.ascVisitasProgramadas = dr.GetInt32(dr.GetOrdinal("ascVisitasProgramadas"));
                            entKpiDetalleAsc.ascVisitasReal = dr.GetInt32(dr.GetOrdinal("ascVisitasReal"));

                            lstDashboardKpiDetalleAsc.Add(entKpiDetalleAsc);
                            /*
                             
                            */
                        }

                    }
                    dr.NextResult();
                }

                entDsKpiDetalle.lstDashboardKpiEquipo = lstDashboardKpiEquipo;
                entDsKpiDetalle.lstDashboardKpiDetalleRik = lstDashboardKpiDetalleRik;
                entDsKpiDetalle.lstDashboardKpiDetalleAsc = lstDashboardKpiDetalleAsc;
                entDsKpiDetalle.lstDashboardKpiEquipoRik = lstDashboardKpiEquipoRik;


                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool ExisteColumna(SqlDataReader Reader, string ColumnName)
        {
            foreach (DataRow row in Reader.GetSchemaTable().Rows)
            {
                if (row["ColumnName"].ToString() == ColumnName)
                    return true;
            } //Still here? Column not found. 
            return false;
        }

    }
}