using CapaDatos;
using CapaEntidad;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class CN_facturacionDoble
    {
        public void ConsultarRep_FacturaDoble(FacturacionDoble Registro, ref List<FacturacionDoble> Lista, string Conexion)
        {
            CD_facturacionDoble CD = new CD_facturacionDoble();
            CD.ConsultarRep_FacturaDoble(Registro, ref Lista, Conexion);
        }
    }
}