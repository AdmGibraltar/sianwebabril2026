using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaEntidad;
using CapaNegocios;
using System.Net.Http;
using System.Threading.Tasks;
using SIANWEB.Core.Web.API;

namespace SIANWEB.WebService
{
    public class CatAplicacionController
        : BaseWebAPIController
    {
        //EF
        [HttpGet]
        public HttpResponseMessage Get(int idEmp, int idSol)
        {
            CN_CatAplicacion cnCatAplicacion = new CN_CatAplicacion();
            try
            {
                var result = cnCatAplicacion.ObtenerPorEmpresaYSolucion(Sesion.Id_Emp, idSol, Sesion);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }

        //EF
        [HttpGet]
        public HttpResponseMessage Get(int idSol, int idSeg, int idOp)
        {
            CN_CatAplicacion cnCatAplicacion = new CN_CatAplicacion();
            try
            {
                var result = cnCatAplicacion.ObtenerPorEmpresaSolucionSegmento(Sesion.Id_Emp, Sesion.Id_Cd, idSol, idSeg, idOp, Sesion);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }

        }

        //EF
        [HttpGet]
        public HttpResponseMessage Get(int idUen, int idSeg, int idArea, int idSol, int idOp, int idCte)
        {
            CN_CatAplicacion cnCatAplicacion = new CN_CatAplicacion();
            if (idOp == 0)
            {
                try
                {
                    var aplicacionesDisponibles = cnCatAplicacion.ObtenerTodasLasAplicacionesDisponibles(Sesion, idUen, idSeg, idArea, idSol, idCte);
                    return Request.CreateResponse(System.Net.HttpStatusCode.OK, aplicacionesDisponibles);
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
                }
            }
            else
            {
                try
                {
                    var aplicacionesDisponibles = cnCatAplicacion.ObtenerTodasLasAplicacionesDisponibles(Sesion, idUen, idSeg, idArea, idSol, idOp, idCte);
                    return Request.CreateResponse(System.Net.HttpStatusCode.OK, aplicacionesDisponibles);
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
                }
            }
        }

        // 26 JUL 2021 RFH
        [HttpGet]
        public eResponse<List<Aplicacion>> spObtenerTodasLasAplicacionesDisponibles
            (int Id_Uen, int Id_Seg, int Id_Area, int Id_Sol, int Id_Op, int Id_Cte)
        {
            eResponse<List<Aplicacion>> result = new eResponse<List<Aplicacion>>();
            List<Aplicacion> Lst = new List<Aplicacion>();
            CN_CatAplicacion CN_CatApl = new CN_CatAplicacion();

            try
            {
                Lst = CN_CatApl.spObtenerTodasLasAplicacionesDisponibles(
                    Sesion.Id_Emp, Sesion.Id_Cd, Id_Uen, Id_Seg, Id_Area, Id_Sol, Id_Op, Id_Cte, Sesion.Emp_Cnx);
                result.Datos = Lst;
                if (Lst == null)
                {
                    result.Estado = -1;
                }
                else
                {
                    result.Estado = 1;
                }

            }
            catch (Exception ex)
            {
                Lst = null;
                result.Estado = -1;
            }

            return result;
        }

    }
}