
using CapaEntidad;
using CapaDatos;
using System.Collections.Generic;
using System;

namespace CapaNegocios
{
    public class CN_DashboardKpi
    {


        public void ConsultarKpiDiario(string strConexion, int intIdEmp, int intIdCd, int intIdU, ref entDashboardKpiDiario entDsKpi, ref List<entDashboardKpiDiarioRik> lstDsKpiRik)
        {
            try
            {
                CD_DashboardKpi cdDashboardKpi = new CD_DashboardKpi();
                cdDashboardKpi.ConsultarKpiDiario(strConexion, intIdEmp, intIdCd, intIdU, ref entDsKpi, ref lstDsKpiRik);
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
                CD_DashboardKpi cdDashboardKpi = new CD_DashboardKpi();
                cdDashboardKpi.ConsultarKpiDiarioDetalle(strConexion, intIdEmp, intIdCd, intIdU, ref entDsKpi, ref lstDsKpiRik, ref entDsKpiDetalle);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}