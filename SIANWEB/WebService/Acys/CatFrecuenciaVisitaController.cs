using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaEntidad;
using CapaNegocios;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using Telerik.Web.UI;

//29 Nov 2018 RFH

namespace SIANWEB.WebService
{
    public class CatFrecuenciaVisitaController : ApiController
    {
        // 29 Nov 2018 RFH Carga listado de registros
        
        [HttpGet]
        public eResponse<List<eCatFrecuenciaVisita>> Listado(int Id)
        {
            eResponse<List<eCatFrecuenciaVisita>> result = new eResponse<List<eCatFrecuenciaVisita>>();
            result.Estado = 0;

            List<eCatFrecuenciaVisita> lst = new List<eCatFrecuenciaVisita>();
            try
            {
                CN_CatFrecuenciaVisita CN = new CN_CatFrecuenciaVisita();
                lst = CN.Listado(Sesion.Emp_Cnx);
                result.Estado = 1;
                result.Datos = lst;
            }
            catch (Exception ex)
            {
                result.Estado = -1;
                result.Datos = null;
            }
            return result;
        }

        /*
        [HttpGet]
        public eResponse<List<eCatFrecuenciaVisita>> Listado(int Id)
        {
            eResponse<List<eCatFrecuenciaVisita>> result = new eResponse<List<eCatFrecuenciaVisita>>();
            result.Estado = 0;

            List<eCatFrecuenciaVisita> lst = new List<eCatFrecuenciaVisita>();
            try
            {
                CN_CatFrecuenciaVisita CN = new CN_CatFrecuenciaVisita();
                lst = CN.Listado(Sesion.Emp_Cnx);
                result.Estado = 1;
                result.Datos = lst;
            }
            catch (Exception ex)
            {
                result.Estado = -1;
                result.Datos = null;
            }
            return result;
        }
        */

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

        //
    }

}