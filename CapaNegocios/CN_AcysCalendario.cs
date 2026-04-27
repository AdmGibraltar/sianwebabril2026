using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_AcysCalendario
    {
        public void spAcuerdosExistentesPorCliente(AcysCalendario Datos, ref List<AcysCalendario> Lista, string Conexion)
        {
            CD_AcysCalendario CD = new CD_AcysCalendario();
            CD.spAcuerdosExistentesPorCliente(Datos, ref Lista, Conexion);
        }
    }
}