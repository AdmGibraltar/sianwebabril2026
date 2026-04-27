using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class CN_LineaTransporte
    {
        public void ConsultaLineaTransporte_Buscar(eLineaTransporte lineaTransporte, ref List<eLineaTransporte> listaLineaTransporte, string Conexion)
        {
            try
            {
                CapaDatos.CD_LineaTransporte cdLineaTransporte = new CapaDatos.CD_LineaTransporte();
                cdLineaTransporte.ConsultaLineaTransporte_Buscar(lineaTransporte, ref listaLineaTransporte, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}