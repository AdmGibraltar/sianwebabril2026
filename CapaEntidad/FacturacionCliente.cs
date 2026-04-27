using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class FacturacionCliente
    {
        public int id_emp { get; set; }
        public int id_Cd { get; set; }
        public int id_cte { get; set; }
        public int Factura { get; set; }
        public int mesFactura { get; set; }
        public int mes { get; set; }
        public int cantidad { get; set; }

        public string nombreCliente { get; set; }


        public DateTime fecha { get; set; }
        public DateTime FechaFactura { get; set; }


    }
}