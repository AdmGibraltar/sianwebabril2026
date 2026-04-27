using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class CN_MonitorfacturacionCliente
    {
        public void ConsultarMonitorAgenda(FacturacionCliente agenda, ref List<FacturacionCliente> List, string Conexion)
        {
            CD_MonitorfacturacionCliente CD = new CD_MonitorfacturacionCliente();
            CD.ConsultarMonitorAgenda(agenda, ref List, Conexion);
        }
    }
}