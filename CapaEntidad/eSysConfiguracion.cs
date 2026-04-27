using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    // 20AGO-2021 RFH
    public class eSysConfiguracion
    {
        public int Id_Emp { get; set; }
        public int Id_Cd { get; set; }
        public int Id_Conf { get; set; }
        public string Conf_Valor { get; set; }
        public string Conf_Descripcion { get; set; }

    }
}