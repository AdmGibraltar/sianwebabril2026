using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class eReport_ProductoInactivo
    {
        public int Id_Emp { get; set; }
        public int Id_Cd { get; set; }
        public int Anio { get; set; }
        public int Mes { get; set; }
        public int Id_Op { get; set; }
        public int Id_Prd { get; set; }
        public string Prd_Descripcion { get; set; }
        public string Cliente { get; set; }
        public string TipoVenta { get; set; }
        public string EstatusStr { get; set; }
        public string Conexion { get; set; }
        public int Id_Acs { get; set; }
        public string TipoCuentaStr { get; set; }
        public string VigenciaStr { get; set; }

    }
}