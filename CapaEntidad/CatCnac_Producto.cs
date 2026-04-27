using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class CatCnac_Producto
    {
        public long Id_Prd { get; set; }
        public int Id_TG { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }
        public double Subtotal { get; set; }
        public int Frecuencia { get; set; }
        public bool Lun { get; set; }
        public bool Mar { get; set; }
        public bool Mie { get; set; }
        public bool Jue { get; set; }
        public bool Vie { get; set; }
        public bool Sab { get; set; }
        public string Documento { get; set; }
        public int Id_ACYS { get; set; }
        public string Descripcion { get; set; }
        public string Unidad { get; set; }
        public string Presentacion { get; set; }
        public int RequiereOC { get; set; }
        public int Acs_FrecuenciaTipo { get; set; }
    }
}