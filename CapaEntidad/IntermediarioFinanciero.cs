using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    [Serializable]
    public class IntermediarioFinanciero
    {
        public int Id_IF { get; set; }
        public int Id_Emp { get; set; }
        public int Id_Cd { get; set; }
        public string Banco { get; set; }
        public string RFC { get; set; }
        public string RazonSocial { get; set; }
        public string Correo { get; set; }
        public string EstatusStr { get; set; }
        public string Activo { get; set; }
    }
}
