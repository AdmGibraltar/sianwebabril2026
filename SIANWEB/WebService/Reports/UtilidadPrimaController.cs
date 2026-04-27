using CapaEntidad;
using CapaNegocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SIANWEB.WebService.Reports
{
    public class UtilidadPrimaController : ApiController
    {
        [HttpGet]
        public eResponse<List<eReportUtilidadBruta>> sp_ReportUtilidadPrimaDocumentoDetalle(
           //int PageNumber, int PageSize,
           int Anio, int Mes)
        {
            eResponse<List<eReportUtilidadBruta>> result = new eResponse<List<eReportUtilidadBruta>>();
            result.Estado = 0;
            List<eReportUtilidadBruta> lst = new List<eReportUtilidadBruta>();
            try
            {
                CN_UtilidadPrima CC = new CN_UtilidadPrima();
                eReportUtilidadBruta Pms = new eReportUtilidadBruta();

                Pms.PageNumber = 0;
                Pms.PageSize = 0;
                Pms.Id_Emp = Sesion.Id_Emp;
                Pms.Id_Cd = Sesion.Id_Cd;
                Pms.Conexion = Sesion.Emp_Cnx;
                Pms.Anio = Anio;
                Pms.Mes = Mes;
                lst = CC.sp_Report_UtilidadPrimaDocumentoDetalle(Pms);
                result.Estado = 1;
                result.Datos = lst;
            }
            catch (Exception ex)
            {
                result.Estado = 0;
                result.Datos = null;
            }
            return result;
        }


        [HttpGet]
        public eResponse<List<eReportUtilidadVenta>> sp_ReportUtilidadVenta(
        //int PageNumber, int PageSize,
        int Anio, int Mes, int tipo)
        {
            eResponse<List<eReportUtilidadVenta>> result = new eResponse<List<eReportUtilidadVenta>>();
            result.Estado = 0;
            List<eReportUtilidadVenta> lst = new List<eReportUtilidadVenta>();
            try
            {
                CN_UtilidadPrima CC = new CN_UtilidadPrima();
                eReportUtilidadVenta Pms = new eReportUtilidadVenta();

                Pms.PageNumber = 0;
                Pms.PageSize = 0;
                Pms.Id_Emp = Sesion.Id_Emp;
                Pms.Id_Cd = Sesion.Id_Cd;
                Pms.Conexion = Sesion.Emp_Cnx;
                Pms.Anio = Anio;
                Pms.Mes = Mes;
                lst = CC.sp_Report_UtilidadVentaDocumentoDetalle(Pms);
                result.Estado = 1;
                result.Datos = lst;
            }
            catch (Exception ex)
            {
                result.Estado = 0;
                result.Datos = null;
            }
            return result;
        }

        protected Sesion Sesion
        {
            get
            {
                if (HttpContext.Current.Session != null)
                {
                    return (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
                }
                return null;
            }
        }

    }
}