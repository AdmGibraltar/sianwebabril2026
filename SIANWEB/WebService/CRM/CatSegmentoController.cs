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
    public class CatSegmentoController : ApiController
    {
        [HttpGet]
        public IEnumerable<CatSegmento> Get(int idEmp, int idUen)
        {
            CN_CatSegmentos cnCatSegmento = new CN_CatSegmentos();
            var resultado = cnCatSegmento.ObtenerSegmentosPorUen(Sesion.Id_Emp, idUen, Sesion);
            return resultado;
        }

        // 34SEP-2021 RFH

        [HttpGet]
        public eResponse<List<eSegmento>> Consultar_SegmentoByRik(int Id_Cd, int Id_Rik)
        {
            eResponse<List<eSegmento>> result = new eResponse<List<eSegmento>>();
            result.Estado = 0;

            List<eSegmento> lst = new List<eSegmento>();
            try
            {
                CN_CatSegmentos CN = new CN_CatSegmentos();
                lst = CN.ConsultaSegmentos(Sesion.Id_Emp, Sesion.Id_Cd, Id_Rik, Sesion.Emp_Cnx);

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

        // 13JUN-2019 RFH

        [HttpGet]
        public eResponse<List<eSegmento>> SelCatSegmento(int Id_Emp, int Id_Uen)
        {
            eResponse<List<eSegmento>> result = new eResponse<List<eSegmento>>();
            result.Estado = 0;

            List<eSegmento> lst = new List<eSegmento>();
            try
            {
                CN_CatSegmentos CN = new CN_CatSegmentos();
                lst = CN.SelCatSegmento(Sesion.Id_Emp, Id_Uen, Sesion.Emp_Cnx);

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

        // NOV26-2019 RFH

        [HttpGet]
        public eResponse<List<Segmentos>> Consultar_Segmento(int Segmento)
        {
            eResponse<List<Segmentos>> result = new eResponse<List<Segmentos>>();
            result.Estado = 0;

            List<Segmentos> lst = new List<Segmentos>();
            try
            {
                CN_CatSegmentos CN = new CN_CatSegmentos();
                CN.ConsultaSegmentos(Sesion.Id_Emp, Segmento, Sesion.Emp_Cnx, ref lst);

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