using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class CN_Report_ProductoInactivo
    {
        public List<eReport_ProductoInactivo> sp_ProductoInactivo_Proyectos(eReport_ProductoInactivo Pms)
        {
            CD_Report_ProductoInactivo CC = new CD_Report_ProductoInactivo();
            return CC.sp_ProductoInactivo_Proyectos(Pms);
        }

        public List<eReport_ProductoInactivo> sp_ProductoInactivo_Acys(eReport_ProductoInactivo Pms)
        {
            CD_Report_ProductoInactivo CC = new CD_Report_ProductoInactivo();
            return CC.sp_ProductoInactivo_Acys(Pms);
        }

    }
}