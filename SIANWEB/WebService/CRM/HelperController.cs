using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaEntidad;
using CapaNegocios;
using CapaDatos;
using CapaModelo;
using SIANWEB.WebAPI.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Configuration;

namespace SIANWEB.WebService.PortalRIK.GestionPromocion.Propuestas
{
    public class HelperController : ApiController
    {
        [HttpGet]
        public eResponse<List<Comun>> ObtenerTipoCliente(string diff1)
        {
            var result = new eResponse<List<Comun>>();

            try
            {
                var list = new List<Comun>();
                new CD__Comun().LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, "spCatTCliente_Combo", Sesion.Emp_Cnx, ref list);
                result.Datos = list;
                result.Estado = 1;
            }
            catch { }
            return result;
        }

        [HttpGet]
        public eResponse<List<Comun>> ObtenerCuentasCorporativas(string diff2)
        {
            var result = new eResponse<List<Comun>>();

            try
            {
                var list = new List<Comun>();
                string conexion = ConfigurationManager.AppSettings.Get("strConnectionCentral");
                new CD__Comun().LlenaCombo(Sesion.Id_Emp, "spCatCuentaCorp_Combo", conexion, ref list);
                result.Datos = list;
                result.Estado = 1;
            }
            catch { }
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