using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class ProductoPermiso
    {
        public int Id_Emp { get; set; }
        public int Id_Cd { get; set; }
        public string Sucursal { get; set; }
        public Int64 Id_Prd { get; set; }
        public string Nombre_Producto { get; set; }
        public bool Activo { get; set; }

    }
}