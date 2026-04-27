using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class entDashboardKpiDiario
    {
        public string NombreCD { get; set; }
        public decimal utilidadPPTOPorcentaje { get; set; }
        public decimal utilidadDiaPorcentaje { get; set; }
        public decimal utilidadPptoMoneda { get; set; }
        public decimal utilidadDiaMoneda { get; set; }
        public DateTime fechaDashBoard { get; set; }
        public decimal gCumplimientoUBPorcenjeData1 { get; set; }
        public decimal gCumplimientoUBPorcenjeData2 { get; set; }
        public decimal gCumplimientoUBPorcenjeLabel1 { get; set; }
        public decimal gCumplimientoUBPorcenjeLabel2 { get; set; }
        public decimal gCumplimientoUBPesosData1 { get; set; }
        public decimal gCumplimientoUBPesosData2 { get; set; }
        public decimal gCumplimientoUBPesosLabel1 { get; set; }
        public decimal gCumplimientoUBPesosLabel2 { get; set; }
        public decimal presupuestoGnrl { get; set; }
        public decimal presupuestoGnrlRestante { get; set; }
        public decimal cumplimientoGnrl { get; set; }
        public decimal cumplimientoGnrlPorcentaje { get; set; }
        public decimal remisionesVigentes { get; set; }
        public decimal remisionesVencidas { get; set; }
        public decimal remisionesPxFGnrl { get; set; }
        public int gVigentesData1 { get; set; }
        public int gVigentesData2 { get; set; }
        public int gVigentesLabel1 { get; set; }
        public int gVigentesLabel2 { get; set; }
        public int gVencidasData1 { get; set; }
        public int gVencidasData2 { get; set; }
        public int gVencidasLabel1 { get; set; }
        public int gVencidasLabel2 { get; set; }
        public decimal ttlCarteraCobranza { get; set; }
        public decimal ttlCarteraTiempo { get; set; }
        public decimal ttlCarteraMenos30dias { get; set; }
        public decimal ttlCarteraMas30dias { get; set; }
        public int gTiempoData1 { get; set; }
        public int gTiempoData2 { get; set; }
        public string gTiempoLabel1 { get; set; }
        public int gTiempoLabel2 { get; set; }
        public int gMenos30Data1 { get; set; }
        public int gMenos30Data2 { get; set; }
        public int gMenos30Label1 { get; set; }
        public int gMenos30Label2 { get; set; }
        public int gMas30Data1 { get; set; }
        public int gMas30Data2 { get; set; }
        public int gMas30Label1 { get; set; }
        public int gMas30Label2 { get; set; }

        public decimal ImporteBaja { get; set; }
        public decimal ImporteRefacturado { get; set; }
        public decimal ImporteFacturas { get; set; }
        public decimal ImporteGeneral { get; set; }

        public int NumBaja { get; set; }
        public int NumRefacturado { get; set; }
        public int NumFacturas { get; set; }




    }

    public class entDashboardKpiDiarioRik
    {
        public string NombreRik { get; set; }
        public decimal cumplimientoRik { get; set; }
        public decimal presupuestoRik { get; set; }
        public decimal crecimientoRik { get; set; }
        public int cteActivoRik { get; set; }
        public decimal carteraTiempoRik { get; set; }
        public decimal carteraVencidaRik { get; set; }
        public decimal remisionesTiempoRik { get; set; }
        public decimal remisionesVencidaRik { get; set; }
        public decimal UBPprcemtaje { get; set; }


    }
}