using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class eCL_ResponsableAutorizador
    {
        public int Id_MotivoCL { get; set; }
        public string Concepto { get; set; }
        public string Correo { get; set; }
        public string Responsable { get; set; }

        public int ResponsableIdUsuario { get; set; }

    }
}