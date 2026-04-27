using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Chofer
    {
        public int Id_Emp { get; set; }
        public int Id_Cd { get; set; }
        public int Folio { get; set; }
        public string Rfc { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NumLicencia { get; set; }
        public string Chofer_Calle { get; set; }
        public string Chofer_Numero { get; set; }
        public string Chofer_Estado { get; set; }
        public string Chofer_Cp { get; set; }
        public int Chofer_Estatus { get; set; }
    }
}