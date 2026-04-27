using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class ResultReporteFacturacionCDC
    {
        ///     Id_Emp Id_Cd   Id_Cte Cte_NomComercial    Id_Fac 
        ///     ImporteConIVA   DiasPago FecFactura  FecVenceFactura 
        ///     Fac_Pagado  Fac_EstatusStr Fac_PedNum  Pag_Importe OrdenCompra

        public int Id_Emp { get; set; }
        public int Id_Cd { get; set; }
        public int Id_Cte { get; set; }
        public string Cte_NomComercial { get; set; }
        public int Id_Fac { get; set; }
        public decimal ImporteConIVA { get; set; }
        public int DiasPago { get; set; }
        public DateTime FecFactura { get; set; }
        public DateTime FecVenceFactura { get; set; }
        public decimal Fac_Pagado { get; set; }
        public string Fac_EstatusStr { get; set; }
        public string Fac_PedNum { get; set; }
        public decimal Pag_Importe { get; set; }
        public string OrdenCompra { get; set; }

        //RBM Oct 2024
        //Se agregan campos al reporte
        public int Id_Rik { get; set; }
        public int Id_Ter { get; set; }
        public string Rik_Nombre { get; set; }
        public string Fac_Estatus { get; set; }
        public string FPago { get; set; }
        public string PagoMetodo { get; set; }
        public int CondPago { get; set; }
        public string Analista { get; set; }
        public string Grupo { get; set; }
        public string DiasRevision { get; set; }
        public string HorarioRevision { get; set; }
        public string DocAdicionales { get; set; }
        public string CampoOtro { get; set; }
        public string DiasPagos { get; set; }
        public string HorarioPago { get; set; }
        public string ContraEntrega { get; set; }
        public string DiasRecepcion { get; set; }
        public string HorarioEntrega { get; set; }
        public string PersonaRecibe { get; set; }
        public string CitaEntrega { get; set; }
        public string AreaRecibo { get; set; }
        public string Estacionamiento { get; set; }
        public string DocEntrega { get; set; }
        public string DocRecepcion { get; set; }

    }
}