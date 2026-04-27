using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Crm_Proyectos
    {
        public int Id_Emp { get; set; }
        public int Id_Cd { get; set; }
        public int Id_Rik { get; set; }
        public int Id_Cte { get; set; }

        public int Id_Op { get; set; }
        public int Valuacion { get; set; }
        public int Id_Ter { get; set; }
        public int Estatus { get; set; }
        public int Id_CrmProspecto { get; set; }
    }
}