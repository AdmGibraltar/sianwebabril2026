using CapaEntidad;
using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class CN_Rik
    {
        public void ConsultaRIk(Rik RegistroRik, ref List<Rik> list_Riks, string Conexion)
        {
            CD_Rik CD = new CD_Rik();
            CD.ConsultaRIk(RegistroRik, ref list_Riks, Conexion);
        }

        public void ConsultaRIkCrm(Rik RegistroRik, ref List<Rik> list_Riks, string Conexion)
        {
            CD_Rik CD = new CD_Rik();
            CD.ConsultaRIkCrm(RegistroRik, ref list_Riks, Conexion);
        }
    }
}
