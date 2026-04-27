using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class eReportUtilidadBruta
    {
        public int Id_Emp { get; set; }
        public int Id_Cd { get; set; }
        public int Cd_Tipo { get; set; }
        public int Id_Tipo_Documento { get; set; }
        public string Tipo_Documento { get; set; }

        public int Id_Factura { get; set; }
        public string Sucursal { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }

        public double CalculadoCon { get; set; }
        public double Venta { get; set; }
        public double Costo { get; set; }
        public double UtilidadBruta { get; set; }
        public double PorcUBReal { get; set; }
        public double PorcUBPlaneada { get; set; }
        public double VarianzaUBrutaPuntos { get; set; }
        public double ImpactoPesos { get; set; }
        public DateTime Fecha_Consulta { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Conexion { get; set; }

        public int Id_Prd { get; set; }
        public string Prd_Nombre { get; set; }
    }
}