using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class eBitacoraCancelacionDocumento
    {
        public int Id { get; set; }
        public int Id_Emp { get; set; }
        public int Id_Cd { get; set; }

        public int Motivo_Id { get; set; }
        public int Documento_Id { get; set; }
        public int Usuario_Id { get; set; }
        public string TipoDocumento { get; set; }
        public string SerieDocumento { get; set; }
        public string Fecha { get; set; }
        public string Motivo { get; set; }
        public string Ncr_Estatus { get; set; }
        public string Sucursal { get; set; }
        public string CorreoSolicitud { get; set; }


        public DateTime FechaDocumento { get; set; }
        public string UsuarioCancelacion { get; set; }
        public DateTime? FechaIni { get; set; }
        public DateTime? FechaFin { get; set; }
        public int EstatusBitacora { get; set; }
        public int Id_Nca { get; set; }
        public int Num_Cliente { get; set; }
        public string StatusDev { get; set; }
        public string SerieDev { get; set; }
        public DateTime FechaDev { get; set; }
        public int TipoDev { get; set; }
        public int Factura_dev { get; set; }
        public float Nca_Subtotal { get; set; }
        public float Nca_Iva { get; set; }
        public float Nca_Total { get; set; }

    }
}