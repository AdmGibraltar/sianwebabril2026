using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class CN_ReporteFacturacionCDC
    {
        public int ReporteFacturacionCDC(int IdEmp, int IdCDI, DateTime fInicio, DateTime fFinal, string Conexion, ref List<ResultReporteFacturacionCDC> lista)
        {
            int iRes = 0;
            try
            {
                CD_ReporteFacturacionCDC claseRptCDC = new CD_ReporteFacturacionCDC();
                iRes = claseRptCDC.ReporteFacturacionCDC(IdEmp, IdCDI, fInicio, fFinal, Conexion, ref lista);
            }
            catch (Exception ex)
            {
                iRes = -1;
                throw ex;
            }
            return iRes;
        }
    }
}
