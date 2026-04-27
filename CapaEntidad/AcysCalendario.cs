using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class AcysCalendario
    {
        public int Id_Emp { get; set; }
        public int Id_Cd { get; set; }
        public int Id_Acs { get; set; }
        public long Id_Prd { get; set; }
        public int Id_Cte { get; set; }
        public string Id_Ter { get; set; }
        public int Acs_Anio { get; set; }
        public int Acs_Semana { get; set; }
        public int Acs_Cantidad { get; set; }
        public double Acs_Precio { get; set; }
        public string Cte_NomComercial { get; set; }
        public string Prd_Descripcion { get; set; }
        public DateTime Fecha_Inicial { get; set; }
        public string Frecuencia { get; set; }
    }
}