using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class FacturaLite
    {
        public int id_Emp { get; set; }
        public int id_Fac { get; set; }
        public int id_Pag { get; set; }
        public int MetodoPago { get; set; }
        public int id_Cd { get; set; }
        public int id_cte { get; set; }
        public string rfc { get; set; }
        public string serie { get; set; }
        public string strSesion { get; set; }
        public string strRstAlerta { get; set; }
        public string strRstScript { get; set; }
        public string strUrlPdf { get; set; }
        public int Activo { get; set; }
        public int Notificado { get; set; }
        public int DescargarPDF { get; set; }
        public int Tipo_Mov { get; set; }

    }
}