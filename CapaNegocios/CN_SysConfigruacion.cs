using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_SysConfigruacion
    {
        // 20AGO-2021 RFH
        public eSysConfiguracion spSysConfiguracionById(int Id_Emp, int Id_Cd, int Id_Conf, string Conexion)
        {
            CD_SysConfiguracion CD = new CD_SysConfiguracion();
            return CD.spSysConfiguracionById(Id_Emp, Id_Cd, Id_Conf, Conexion);
        }

    }
}