using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class ReporteCRM
    {
        public int Id_Emp { get; set; }
        public int Id_Cd { get; set; }
        public int Id_U { get; set; }
        public string Id_Rik { get; set; }
        public int Periodo { get; set; }
        public int IntConsulta { get; set; }
        public float Monto1 { get; set; }
        public float FLOAT { get; set; }
        public bool Nuevo { get; set; }
        public int TipoVenta { get; set; }

        public decimal Analisis { get; set; }
        public decimal Presentacion { get; set; }
        public decimal NEgociacion { get; set; }
        public decimal Cierre { get; set; }
        public decimal Cancelacion { get; set; }

        public string Nombre { get; set; }
        public decimal Total { get; set; }

        public int rik { get; set; }
        public string nombre_rik { get; set; }

        public DateTime fechainicio { get; set; }
        public DateTime fechafinal { get; set; }

        public int id_proyect { get; set; }
        public DateTime? fechaCreacion { get; set; }
        public string uenDescripcion { get; set; }
        public DateTime? FECHAcierre { get; set; }
        public string nombre_cliente { get; set; }
        public string TipoVentaStr { get; set; }
    }
}
