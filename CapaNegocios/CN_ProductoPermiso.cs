using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_ProductoPermiso
    {

        public void ProductoPermiso_Consulta(ProductoPermiso Datos, ref List<ProductoPermiso> lista, string conexion)
        {
            CD_ProductoPermiso Cd = new CD_ProductoPermiso();
            Cd.ProductoPermiso_Consulta(Datos, ref lista, conexion);
        }

        public void ProductoPermiso_Actualizar(ProductoPermiso Datos, ref int verificador, string conexion)
        {
            CD_ProductoPermiso Cd = new CD_ProductoPermiso();
            Cd.ProductoPermiso_Actualizar(Datos, ref verificador, conexion);
        }
    }
}