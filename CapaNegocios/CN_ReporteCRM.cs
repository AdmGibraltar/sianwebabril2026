using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class CN_ReporteCRM
    {
        public void ConsultaReporteGrafica(ReporteCRM RegistroReporteCRM, ref List<ReporteCRM> listaReporteCRM, string Conexion)
        {
            CD_ReporteCRM cd = new CD_ReporteCRM();
            cd.ConsultaReporteGrafica(RegistroReporteCRM, ref listaReporteCRM, Conexion);

        }

        public void ConsultaReporteGraficaCantidad(ReporteCRM RegistroReporteCRM, ref List<ReporteCRM> listaReporteCRM, string Conexion)
        {
            CD_ReporteCRM cd = new CD_ReporteCRM();
            cd.ConsultaReporteGraficaCantidad(RegistroReporteCRM, ref listaReporteCRM, Conexion);

        }


        public void ConsultaReporteGraficaMontoProyecto(ReporteCRM RegistroReporteCRM, ref List<ReporteCRM> listaReporteCRMAnalisis, ref List<ReporteCRM> listaReporteCRMPresentacion,
            ref List<ReporteCRM> listaReporteCRMNegociacion, ref List<ReporteCRM> listaReporteCRMCierre, ref List<ReporteCRM> listaReporteCRMCancelacion, string Conexion)
        {
            CD_ReporteCRM cd = new CD_ReporteCRM();
            cd.ConsultaReporteGraficaMontoProyecto(RegistroReporteCRM, ref listaReporteCRMAnalisis, ref listaReporteCRMPresentacion,
           ref listaReporteCRMNegociacion, ref listaReporteCRMCierre, ref listaReporteCRMCancelacion, Conexion);

        }

        public void ConsultaReporteExcelMontoProyecto(ReporteCRM RegistroReporteCRM, ref List<ReporteCRM> listaReporteCRM, string Conexion)
        {
            CD_ReporteCRM cd = new CD_ReporteCRM();
            cd.ConsultaReporteExcelMontoProyecto(RegistroReporteCRM, ref listaReporteCRM, Conexion);

        }

        public void ConsultaReporteGraficaCantidadProyecto(ReporteCRM RegistroReporteCRM, ref List<ReporteCRM> listaReporteCRMAnalisis, ref List<ReporteCRM> listaReporteCRMPresentacion,
            ref List<ReporteCRM> listaReporteCRMNegociacion, ref List<ReporteCRM> listaReporteCRMCierre, ref List<ReporteCRM> listaReporteCRMCancelacion, string Conexion)
        {
            CD_ReporteCRM cd = new CD_ReporteCRM();
            cd.ConsultaReporteGraficaCantidadProyecto(RegistroReporteCRM, ref listaReporteCRMAnalisis, ref listaReporteCRMPresentacion,
           ref listaReporteCRMNegociacion, ref listaReporteCRMCierre, ref listaReporteCRMCancelacion, Conexion);
        }


        public void ConsultaReporteGraficaCantidad(ReporteCRM RegistroReporteCRM, ref List<ReporteCRM> listaReporteCRMAnalisis, ref List<ReporteCRM> listaReporteCRMPresentacion,
           ref List<ReporteCRM> listaReporteCRMNegociacion, ref List<ReporteCRM> listaReporteCRMCierre, ref List<ReporteCRM> listaReporteCRMCancelacion, string Conexion)
        {
            CD_ReporteCRM cd = new CD_ReporteCRM();
            cd.ConsultaReporteGraficaCantidad(RegistroReporteCRM, ref listaReporteCRMAnalisis, ref listaReporteCRMPresentacion,
           ref listaReporteCRMNegociacion, ref listaReporteCRMCierre, ref listaReporteCRMCancelacion, Conexion);
        }

        public void ConsultaReporteGrafica(ReporteCRM RegistroReporteCRM, ref List<ReporteCRM> listaReporteCRMAnalisis, ref List<ReporteCRM> listaReporteCRMPresentacion,
           ref List<ReporteCRM> listaReporteCRMNegociacion, ref List<ReporteCRM> listaReporteCRMCierre, ref List<ReporteCRM> listaReporteCRMCancelacion, string Conexion)
        {
            CD_ReporteCRM cd = new CD_ReporteCRM();
            cd.ConsultaReporteGrafica(RegistroReporteCRM, ref listaReporteCRMAnalisis, ref listaReporteCRMPresentacion,
           ref listaReporteCRMNegociacion, ref listaReporteCRMCierre, ref listaReporteCRMCancelacion, Conexion);
        }

        public void consultaCDI(int id_emp, int cdi, ref string nombre, string Conexion)
        {
            CD_ReporteCRM cd = new CD_ReporteCRM();
            cd.consultaCDI(id_emp, cdi, ref nombre, Conexion);
        }

        public void consultaCDI2(int id_emp, int cdi, ref string nombre, string Conexion)
        {
            CD_ReporteCRM cd = new CD_ReporteCRM();
            cd.consultaCDI2(id_emp, cdi, ref nombre, Conexion);
        }

        public void ConsultaReporteExcelImpulsosQuimicos(ReporteCRMQuimicos RegistroReporteCRM, ref List<ReporteCRMQuimicos> listaReporteCRM, ref List<ReporteCRMQuimicos> listaCRMQuimicosxSucursal
    , ref List<ReporteCRMQuimicos> listaCRMQuimicosxSucursalxRIK, ref List<ReporteCRMQuimicos> listaData, string Conexion)
        {
            CD_ReporteCRM cd = new CD_ReporteCRM();
            cd.ConsultaReporteExcelImpulsosQuimicos(RegistroReporteCRM, ref listaReporteCRM, ref listaCRMQuimicosxSucursal, ref listaCRMQuimicosxSucursalxRIK, ref listaData, Conexion);

        }
    }
}