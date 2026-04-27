
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Campania
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }

    public class Campania_Det
    {
        public int Id { get; set; }
        public int Campania_Id { get; set; }

        public Int64 Id_Prd { get; set; }

        public string Descripcion { get; set; }

    }


}