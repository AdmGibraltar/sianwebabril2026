using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaModelo;
using CapaNegocios;
using CapaEntidad;

namespace SIANWEB.WebService.PortalRIK.GestionPromocion
{
    public class CatUENController : ApiController
    {
        [HttpGet]
        public IEnumerable<CatUEN> Get(int idEmp, int idCd, int idRik)
        {
            CN_CatUen cnCatUen = new CN_CatUen();
            var resultado = cnCatUen.ObtenerUENsDeRepresentante(Sesion.Id_Emp, Sesion.Id_Cd, Sesion.Id_Rik, Sesion);
            return resultado;
        }

        // 13JUN-2019 RFH

        [HttpGet]
        public eResponse<List<Uen>> SelCatUen(int Id_Emp, int Id_Uen)
        {
            eResponse<List<Uen>> result = new eResponse<List<Uen>>();
            result.Estado = 0;

            List<Uen> lst = new List<Uen>();
            try
            {
                CN_CatUen CN = new CN_CatUen();
                lst = CN.SelCatUen(Sesion.Id_Emp, Id_Uen, Sesion.Emp_Cnx);

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