using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using System.Data;
using CapaModelo;
using System.Diagnostics;

//
// 22JUL-2019 RFH
// 

namespace CapaNegocios
{
    public class CN_CrmCapValProyecto
    {

        public List<eCrmProyecto> GetCrmProyecto(
            int Id_Emp, int Id_Cd, int Id_Rik, int Id_Cte, int Agrupado, string Conexion)
        {
            CD_CapValProyecto CD = new CD_CapValProyecto();
            return CD.GetCrmProyecto(Id_Emp, Id_Cd, Id_Rik, Id_Cte, Agrupado, Conexion);
        }
        //
    }
}