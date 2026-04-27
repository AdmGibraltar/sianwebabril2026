using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class CN_VinculacionCRM
    {
        public int ActualizarTerritorioOpo(int Id_Cd, int Id_Cte, int Id_Terr, int IdRik, string conexion)
        {
            CD_VinculacionCRM oCD_VinculacionCRM = new CD_VinculacionCRM();
            return oCD_VinculacionCRM.ActualizarTerritorioOpo(Id_Cd, Id_Cte, Id_Terr, IdRik, conexion);
        }
    }
}