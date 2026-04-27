using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class CatConvCategoria
    {
        public int Id_Cat { get; set; }
        public string Cat_Nombre { get; set; }
        public string Cat_DescCorta { get; set; }
        public int Cat_Consecutivo { get; set; }
        public int Cat_CapturaUsuario { get; set; }
        public int Cat_Activo { get; set; }
        public int Id_UCreo { get; set; }
        public DateTime? Cat_FechaCreo { get; set; }
    }
}
