using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class CN_SaldoMenor
    {
        public void ConsultarSaldo(SaldoMenor Datos, ref List<SaldoMenor> lista, string Conexion)
        {
            CD_SaldoMenor cd = new CD_SaldoMenor();
            cd.ConsultarSaldo(Datos, ref lista, Conexion);
        }
    }
}