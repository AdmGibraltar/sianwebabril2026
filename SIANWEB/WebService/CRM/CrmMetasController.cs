using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaEntidad;
using CapaNegocios;
using CapaModelo;
using CapaDatos;
using SIANWEB.WebAPI.Models;
using System.Net.Http;
using SIANWEB.Core.Web.API;
using System.Threading.Tasks;

namespace SIANWEB.WebService.PortalRIK.GestionPromocion
{
    public class CrmMetasController : BaseWebAPIController
    {

        //
        // Abr17-2020 RFH Metas en CRM Por Rik 
        //

        [HttpGet]
        public eResponse<CapaEntidad.Meta> GetMetas_PorRik(int Id_Rik)
        {
            eResponse<CapaEntidad.Meta> result = new eResponse<CapaEntidad.Meta>();
            result.Estado = 0;
            CapaEntidad.Meta meta = new CapaEntidad.Meta();
            try
            {
                CN_CatMeta CN = new CN_CatMeta();
                meta.Id_Emp = Sesion.Id_Emp;
                meta.Id_Cd = Sesion.Id_Cd;
                //CN.Consultar_PorRik(Id_Rik, ref meta, Sesion.Emp_Cnx);
                //CD_CatMeta claseCapaDatos = new CD_CatMeta();
                //claseCapaDatos.Consultar(Id_Rik, ref meta, Sesion.Emp_Cnx);            
                result.Estado = 1;
                result.Datos = meta;
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