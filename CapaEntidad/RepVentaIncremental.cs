using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    [Serializable]
    public class RepVentaIncremental
    {
        public int idCd { get; set; }
        public int CalAnio { get; set; }
        public int idTer { get; set; }
        public int id_cte { get; set; }
        public string nombreComercial { get; set; }
        public string gVentaInst { get; set; }
        public string categoria { get; set; }
        public long idProd { get; set; }
        public int id_rik { get; set; }
        public string riknombre { get; set; }
        public string mesStr { get; set; }
        public string CdStr { get; set; }
        public int anioStr { get; set; }
        public string productoStr { get; set; }
        public string productoPres { get; set; }
        public decimal PrimerValor { get; set; }
        public decimal SegundoValor { get; set; }
        public decimal TercerValor { get; set; }
        public decimal PorCumplimiento { get; set; }
        public decimal PorMultiplicador { get; set; }
        public decimal mes1 { get; set; }
        public decimal mes2 { get; set; }
        public decimal mes3 { get; set; }
        public decimal objetivo1 { get; set; }
        public decimal objetivo2 { get; set; }
        public decimal objetivo3 { get; set; }
        public decimal totalObjetivo { get; set; }
        public decimal acuObjetivo1 { get; set; }
        public decimal acuObjetivo2 { get; set; }
        public decimal acuObjetivo3 { get; set; }
        public decimal totaAcuObjetivo { get; set; }
        public decimal ventaVI { get; set; }
        public decimal ventaVI1Mes { get; set; }
        public decimal ventaVI2Mes { get; set; }
        public decimal ventaVI3Mes { get; set; }
        public decimal ventaNoVI { get; set; }
        public decimal venta1NoVI { get; set; }
        public decimal venta2NoVI { get; set; }
        public decimal venta3NoVI { get; set; }
        public decimal ventaViInc { get; set; }
        public decimal ventaMes1ViInc { get; set; }
        public decimal ventaMes2ViInc { get; set; }
        public decimal ventaMes3ViInc { get; set; }


        //Para obtener los meses y años para venta incremental
        public int mesRep1 { get; set; }
        public int mesRep2 { get; set; }
        public int mesRep3 { get; set; }
        public string mesRepstr1 { get; set; }
        public string mesRepstr2 { get; set; }
        public string mesRepstr3 { get; set; }

        public int anioRep1 { get; set; }
        public int anioRep2 { get; set; }
        public int anioRep3 { get; set; }
        public string anioRepStr1 { get; set; }
        public string anioRepStr2 { get; set; }
        public string anioRepStr3 { get; set; }
    }
}