using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    [Serializable()]
    public class PagoSegundoPlano
    {
        public int Id_PagoSegundoPlano { get; set; }

        public DateTime Fecha_Inicia { get; set; }
        public DateTime Fecha_Fin { get; set; }
        public int Id_Pago { get; set; }
        public int MetodoPago { get; set; }
        public int id_Emp { get; set; }
        public int id_Fac { get; set; }
        public int id_Cd { get; set; }
        public int id_cte { get; set; }
        public string rfc { get; set; }
        public string serie { get; set; }
        public int Activo { get; set; }

    }
}