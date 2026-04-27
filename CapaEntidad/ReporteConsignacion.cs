using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class ReporteConsignacion
    {
        public DateTime fecha { get; set; }
        public int Id_Emp { get; set; }
        public string CD_Nom { get; set; }
        public int Id_Cd { get; set; }
        public int Id_Cte { get; set; }
        public string Cte_NomComercial { get; set; }
        public Int64 Id_Prd { get; set; }
        public string Prd_Descripcion { get; set; }
        public string Prd_Presentacion { get; set; }
        public string Id_Uni { get; set; }
        public int Prd_InvFinal { get; set; }
        public double Prd_PrecioAAA { get; set; }
        public double ImporteInventario { get; set; }
        public double Antepenultimo { get; set; }
        public double Penultimo { get; set; }
        public double Ultimo { get; set; }
        public double Promedio { get; set; }
        public double CostoPromedio { get; set; }
        public double Rotacion { get; set; }
        public double Vigente { get; set; }
        public double Vencido { get; set; }
        public double RetiraroFacturar { get; set; }
    }
}