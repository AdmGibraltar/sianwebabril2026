using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    [Serializable]
    public class UsuarioCorreo
    {
        public int Id_Emp { get; set; }
        public int Id_Cd { get; set; }
        public int Id_U { get; set; }
        public string U_Nombre { get; set; }
        public string U_Correo { get; set; }


    }
}