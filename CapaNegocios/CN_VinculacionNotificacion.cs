using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class CN_VinculacionNotificacion
    {
        public int Consultar_CountNoti(int Id_Cd, int Id_Rik, string Conexion)
        {
            CD_VinculacionNotificacion CD = new CD_VinculacionNotificacion();
            return CD.Consultar_CountNoti(Id_Cd, Id_Rik, Conexion);
        }
    }
}