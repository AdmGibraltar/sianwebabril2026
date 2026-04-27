using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class eReportUtilidadVenta
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

        public string NoReg { get; set; }

        public string Tipo { get; set; }
        public string UUID { get; set; }
        public int NumeroDocumento { get; set; }
        public int Id_Cliente { get; set; }

        public string Cliente { get; set; }
        public string Cancelado { get; set; }
        public string Grupo { get; set; }
        public string EstatusDoc { get; set; }
        public string Descript { get; set; }

        public string FechaContabilizacion { get; set; }

        public double TotalDoc { get; set; }
        public double ImpuestoTotal { get; set; }

        public double DescuentoTotal { get; set; }

        public double SubTotal { get; set; }
        public string ItemCode { get; set; }
        public string Dscription { get; set; }

        public int Quantity { get; set; }
        public double Price { get; set; }
        public double LineTotal { get; set; }
        public string Fac_CteRfc { get; set; }

        public string FechaCan { get; set; }

        public int Id_Terr { get; set; }
        public string Territorio { get; set; }

    }
}