using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    [Serializable]
    public class ClienteIntermediarioPago
    {
        public int Id_Emp { get; set; }
        public int Id_Cd { get; set; }
        public int Id_Cte { get; set; }
        public int Id_IntPag { get; set; }
        public string IntPag_Nombre { get; set; }
        public string IntPag_RFC { get; set; }
        public string IntPag_Razon { get; set; }
        public string IntPag_Correo { get; set; }
        public bool IntPag_Estatus { get; set; }
        public string IntPag_EstatusStr { get; set; }
        public string IntPag_Calle { get; set; }
        public string IntPag_Numero { get; set; }
        public string IntPag_CP { get; set; }
        public string IntPag_Colonia { get; set; }
        public string IntPag_Municipio { get; set; }
        public string IntPag_Estado { get; set; }
    }
}
