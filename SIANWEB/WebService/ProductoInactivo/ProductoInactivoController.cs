using CapaEntidad;
using CapaNegocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SIANWEB.WebService.ProductoInactivo
{
    public class ProductoInactivoController : ApiController
    {
        [HttpGet]
        public eResponse<List<eReport_ProductoInactivo>> sp_ProductoInactivo_Proyectos(
           //int PageNumber, int PageSize,
           int Anio, int Mes)
        {
            eResponse<List<eReport_ProductoInactivo>> result = new eResponse<List<eReport_ProductoInactivo>>();
            result.Estado = 0;
            List<eReport_ProductoInactivo> lst = new List<eReport_ProductoInactivo>();
            try
            {
                CN_Report_ProductoInactivo CC = new CN_Report_ProductoInactivo();
                eReport_ProductoInactivo Pms = new eReport_ProductoInactivo();


                Pms.Id_Emp = Sesion.Id_Emp;
                Pms.Id_Cd = Sesion.Id_Cd;
                Pms.Conexion = Sesion.Emp_Cnx;
                Pms.Anio = Anio;
                Pms.Mes = Mes;
                lst = CC.sp_ProductoInactivo_Proyectos(Pms);
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
        public eResponse<List<eReport_ProductoInactivo>> sp_ProductoInactivo_Acys(
        //int PageNumber, int PageSize,
        int Anio, int Mes, int tipo)
        {
            eResponse<List<eReport_ProductoInactivo>> result = new eResponse<List<eReport_ProductoInactivo>>();
            result.Estado = 0;
            List<eReport_ProductoInactivo> lst = new List<eReport_ProductoInactivo>();
            try
            {
                CN_Report_ProductoInactivo CC = new CN_Report_ProductoInactivo();
                eReport_ProductoInactivo Pms = new eReport_ProductoInactivo();

                Pms.Id_Emp = Sesion.Id_Emp;
                Pms.Id_Cd = Sesion.Id_Cd;
                Pms.Conexion = Sesion.Emp_Cnx;
                Pms.Anio = Anio;
                Pms.Mes = Mes;
                lst = CC.sp_ProductoInactivo_Acys(Pms);
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