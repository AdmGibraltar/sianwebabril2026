using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaModelo;
using CapaNegocios;
using CapaEntidad;

namespace SIANWEB.WebService
{
    public class CatSolucionController : ApiController
    {
        [HttpGet]
        public IEnumerable<CapaModelo.CatSolucion> Get(int idEmp, int idArea)
        {
            CN_CatSolucion cnCatSolucion = new CN_CatSolucion();
            idEmp = Sesion.Id_Emp;
            var resultado = cnCatSolucion.ObtenerPorEmpresaYArea(idEmp, idArea, Sesion);
            return resultado;
        }

        // 2 AGO 2021 - RFH
        [HttpGet]
        public eResponse<List<CapaEntidad.CatSolucion>> spCatSolucionByArea(int Id_Emp, int Id_Area)
        {
            eResponse<List<CapaEntidad.CatSolucion>> result = new eResponse<List<CapaEntidad.CatSolucion>>();
            List<CapaEntidad.CatSolucion> Lst = new List<CapaEntidad.CatSolucion>();
            result.Estado = 0;

            CN_CatSolucion CN = new CN_CatSolucion();
            Lst = CN.spCatSolucionByArea(Id_Emp, Id_Area, Sesion.Emp_Cnx);
            //return resultado;
            try
            {
                result.Estado = 1;
                result.Datos = Lst;
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