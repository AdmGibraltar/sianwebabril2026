using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class FacturacionDoble
    {
        public int Id_Emp { get; set; }
        public int Id_Cd { get; set; }
        public int Id_Cte { get; set; }
        public int Id_Fac { get; set; }
        public int FolioAE { get; set; }

        public string Cte_NomComercial { get; set; }
        public string Fac_FolioFiscal { get; set; }
        public string Fac_FolioFiscalCN { get; set; }

        public DateTime Fac_Fecha { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }

        public double Fac_SubTotal { get; set; }
        public double Fac_ImporteIva { get; set; }
        public double Total { get; set; }

        public string Fac_Xml { get; set; }
        public string Fac_XMLCN { get; set; }
        public string estatus { get; set; }
        public string estatusCN { get; set; }

        public string Nombreestatus { get; set; }
        public string NombreestatusCN { get; set; }

        public object fac_pdf { get; set; }
        public object Fac_PdfCN { get; set; }
    }
}