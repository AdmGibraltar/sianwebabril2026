using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class entDashboardKpiDetalle
    {
        public decimal ventaPptoDiario { get; set; }
        public decimal ventaPptoAcumulado { get; set; }
        public decimal ventaDiaHoy { get; set; }
        public decimal ventaComplementoAcumulado { get; set; }
        public decimal contribucionPptoDiario { get; set; }
        public decimal contribucionPptoAcumulado { get; set; }
        public decimal contribucionDiaHoy { get; set; }
        public decimal contribucionComplementoAcumulado { get; set; }
        public decimal contribucionRealPromedio { get; set; }
        public decimal ventaRealPromedio { get; set; }
        // Familia
        public decimal famQuimicoPpto { get; set; }
        public decimal famQuimicoVenta { get; set; }
        public decimal famQuimicoCumplimiento { get; set; }
        public decimal famPapelPpto { get; set; }
        public decimal famPapelVenta { get; set; }
        public decimal famPapelCumplimiento { get; set; }
        public decimal famOtroPpto { get; set; }
        public decimal famOtroVenta { get; set; }
        public decimal famOtroCumplimiento { get; set; }
        public List<entDashboardKpiEquipo> lstDashboardKpiEquipo { get; set; }
        public List<entDashboardKpiDetalleRik> lstDashboardKpiDetalleRik { get; set; }
        public List<entDashboardKpiDetalleAsc> lstDashboardKpiDetalleAsc { get; set; }
        public List<entDashboardKpiEquipoRik> lstDashboardKpiEquipoRik { get; set; }
    }

    public class entDashboardKpiEquipo
    {
        public string equipoNombre { get; set; }
        public decimal equipoPresupuesto { get; set; }
        public decimal equipoCumplimiento { get; set; }
        public int equipoClientes { get; set; }
        public decimal equipoCarteraTiempo { get; set; }
        public decimal equipoCarteraVencido { get; set; }
    }

    public class entDashboardKpiEquipoRik
    {
        public string equipoNombre { get; set; }
        public string equipoIntegrante { get; set; }
    }

    public class entDashboardKpiDetalleRik
    {
        public string NombreRik { get; set; }
        public decimal presupuestoMensual { get; set; }
        public decimal presupuestoQuimico { get; set; }
        public decimal cumplimientoQuimico { get; set; }
        public decimal margenGeneralQuimico { get; set; }
        public decimal presupuestoPapel { get; set; }
        public decimal cumplimientoPapel { get; set; }
        public decimal margenGeneralPapel { get; set; }
        public decimal presupuestoOtro { get; set; }
        public decimal cumplimientoOtro { get; set; }
        public decimal margenGeneralOtro { get; set; }
        //rik venta Nueva
        public decimal ventaNuevaPresupuesto { get; set; }
        public decimal ventaNueva { get; set; }
        public int clientesNuevos { get; set; }
    }

    public class entDashboardKpiDetalleAsc
    {
        public string ascNombre { get; set; }
        public decimal ascPresupuesto { get; set; }
        public decimal ascCumplimiento { get; set; }
        public decimal ascVisitasProgramadas { get; set; }
        public decimal ascVisitasReal { get; set; }
    }
}