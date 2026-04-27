
using CapaEntidad;
using CapaDatos;
using System.Collections.Generic;
using System;

namespace CapaNegocios
{
    public class CN_Nps
    {


        public void ConsultarNpsReporte(string strConexion, ref List<NpsQueja_Detalle> lstNpsQuejaDetalle, int intIdSucursal)
        {
            try
            {
                CD_Nps cdNps = new CD_Nps();
                cdNps.ConsultarNpsReporte(strConexion, ref lstNpsQuejaDetalle, intIdSucursal);
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
                CD_Nps cdNps = new CD_Nps();
                cdNps.ConsultarNpsFiltros(strConexion, ref lstNpsQuejaDetalle, entFiltro);
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
                CD_Nps cdNps = new CD_Nps();
                cdNps.ConsultarNpsAgrupado(strConexion, ref lstNpsQuejaDetalle, entFiltro);
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
                CD_Nps cdNps = new CD_Nps();
                cdNps.ConsultarNpsReporteDetalle(strConexion, ref lstNpsQuejaDetalle, entNpsFiltro);
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
                CD_Nps cdNps = new CD_Nps();
                cdNps.ConsultarCatalogo(strConexion, strSP, ref lstData);
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
                CD_Nps cdNps = new CD_Nps();
                cdNps.GuardarNps(strConexion, entNps, lstQueja);
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
                CD_Nps cdNps = new CD_Nps();
                cdNps.ConsultarRiks(strConexion, ref lstData, intIdSucursal);
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
                CD_Nps cdNps = new CD_Nps();
                cdNps.ConsultarCliente(strConexion, ref lstData, intIdSucursal, intIdRiK);
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
                CD_Nps cdNps = new CD_Nps();
                cdNps.ConsultarNps(strConexion, ref entNps, ref lstQueja, intIdSucursal, intIdNps);
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
                CD_Nps cdNps = new CD_Nps();
                cdNps.GuardarPlan(strConexion, intIdEmp, intIdSucursal, intIdUsuario, intIdNps, lstQueja, intConcluido, intPlan_Consecutivo, ref entResultado);
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
                CD_Nps cdNps = new CD_Nps();
                cdNps.ConsultarNpsEditar(strConexion, ref entNps, ref lstQueja, intIdSucursal, intNPS);

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
                CD_Nps cdNps = new CD_Nps();
                cdNps.IndicadorGeneralNps(strConexion, intIdEmp, intIdSucursal, entFiltros, ref lstIndicadorNps);

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
                CD_Nps cdNps = new CD_Nps();
                cdNps.IndicadorTrazabilidad(strConexion, intIdEmp, intIdSucursal, entFiltros, ref lstIndicadorTrazabilidad, ref lstIndicadorTrazabilidadCliente);

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
                CD_Nps cdNps = new CD_Nps();
                cdNps.IndicadorConversion(strConexion, intIdEmp, intIdSucursal, entFiltros, ref lstIndicadorConversion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}