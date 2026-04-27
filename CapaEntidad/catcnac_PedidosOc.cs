using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class catcnac_PedidosOc
    {
        public int Id { get; set; }
        public string Id_OC { get; set; }
        public string Remision { get; set; }
        public int Id_Emp { get; set; }
        public int Id_Cd { get; set; }
        public int Id_Cte { get; set; }
        public double VentaInstalada { get; set; }
        public string Vigencia { get; set; }
        public string Estatus { get; set; }
        public string Cte_NomComercial { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaAlta { get; set; }
        public int Id_Ter { get; set; }
        public int Id_Ped { get; set; }

        public int ID_sol { get; set; }
        public string EstatusSOl { get; set; }

        public string EstatusStr { get; set; }
        public string EstatusSOlStr { get; set; }

    }
}