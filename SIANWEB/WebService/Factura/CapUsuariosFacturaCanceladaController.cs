using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using CapaEntidad;
using CapaNegocios;

namespace SIANWEB.WebService
{
    public class CapUsuariosFacturaCanceladaController : ApiController
    {


        /// <summary>
        /// Consulta los usuarios por sucursal
        /// </summary>
        /// <param name="intIdCd">ID de la sucursal</param>
        /// <returns>HttpResponseMessage con las listas de usuarios activos y no activos</returns>
        [HttpGet]
        public HttpResponseMessage CatalogoUsuarios()
        {
            entUsuarioFacturaCancelada entCorreos = new entUsuarioFacturaCancelada();


            try
            {
                // Verificar si la sesión es nula
                if (Sesion == null)
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, "Sesión no encontrada.");
                }
                string strConnectionCentral = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];

                CN_UsuariosFacturaCancelada cnUsuarios = new CN_UsuariosFacturaCancelada();
                entCorreos.intIdCd = Sesion.Id_Cd;
                cnUsuarios.CatalogoUsuarios(strConnectionCentral, ref entCorreos);

                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    entCorreos
                });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Agrega un usuario al catálogo de usuarios de facturas canceladas
        /// </summary>
        /// <param name="intIdCd">ID de la sucursal</param>
        /// <param name="intIdU">ID del usuario</param>
        /// <returns>HttpResponseMessage con el resultado de la operación</returns>
        [HttpGet]
        public HttpResponseMessage AgregarUsuario(string strCorreo)
        {
            entUsuarioFacturaCancelada entCorreos = new entUsuarioFacturaCancelada();

            try
            {
                // Verificar si la sesión es nula
                if (Sesion == null)
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, "Sesión no encontrada.");
                }

                CN_UsuariosFacturaCancelada cnUsuarios = new CN_UsuariosFacturaCancelada();

                string strConnectionCentral = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];

                // Agregar usuario
                entCorreos.strCorreo = strCorreo;
                entCorreos.intIdCd = Sesion.Id_Cd;
                cnUsuarios.AgregarUsuario(strConnectionCentral, entCorreos);

                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    success = true
                });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error: " + ex.Message);
            }
        }


        /// <summary>
        /// Obtiene la sesión actual.
        /// </summary>
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