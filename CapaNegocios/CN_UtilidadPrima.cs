using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class CN_UtilidadPrima
    {
        public List<eReportUtilidadBruta> sp_Report_UtilidadPrimaDocumentoDetalle(eReportUtilidadBruta Pms)
        {
            CD_UtilidadPrima CC = new CD_UtilidadPrima();
            return CC.sp_Report_UtilidadPrimaDocumentoDetalle(Pms);
        }
        public List<eReportUtilidadVenta> sp_Report_UtilidadVentaDocumentoDetalle(eReportUtilidadVenta Pms)
        {
            CD_UtilidadPrima CC = new CD_UtilidadPrima();
            return CC.sp_Report_UtilidadVentaDocumentoDetalle(Pms);
        }
    }
}