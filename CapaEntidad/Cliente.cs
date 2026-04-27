using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Cliente
    {
        public int idCte { get; set; }
        public string nombre { get; set; }
        public int verificador { get; set; }
        public string RevPago { get; set; }
    }
}