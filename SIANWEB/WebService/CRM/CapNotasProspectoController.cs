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

namespace SIANWEB.WebService.PortalRIK.GestionPromocion
{
    public class CapNotasProspectoController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Post([FromBody]CapNotasProspecto capNotasProspecto)
        {
            try
            {
                CN_CapNotasProspecto cnCapNotasProspecto = new CN_CapNotasProspecto();
                var resultado = cnCapNotasProspecto.Crear(Sesion, capNotasProspecto.Id_Cliente, capNotasProspecto.CatNotaSerializable.Texto);
                
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, resultado);
            }
            catch (Exception ex)
            {
                //Falta manejar los estados de error: pérdida de conexión a base de datos, error general(desbordamiento de pila, etc.), identificador de cliente inválido, etc.
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete([FromBody]CapNotasProspecto capNotasProspecto)
        {
            try
            {
                CN_CapNotasProspecto cnCapNotasProspecto = new CN_CapNotasProspecto();
                cnCapNotasProspecto.Eliminar(Sesion, capNotasProspecto);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                //Falta manejar los estados de error: pérdida de conexión a base de datos, error general(desbordamiento de pila, etc.), identificador de cliente inválido, etc.
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPut]
        public HttpResponseMessage Put([FromBody]CapNotasProspecto capNotasProspecto)
        {
            try
            {
                CN_CapNotasProspecto cnCapNotasProspecto = new CN_CapNotasProspecto();
                capNotasProspecto.CatNota = capNotasProspecto.CatNotaSerializable;
                cnCapNotasProspecto.Actualizar(Sesion, capNotasProspecto);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                //Falta manejar los estados de error: pérdida de conexión a base de datos, error general(desbordamiento de pila, etc.), identificador de cliente inválido, etc.
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        public HttpResponseMessage Get(int idCte)
        {
            try
            {
                CN_CapNotasProspecto cnCapNotasProspecto = new CN_CapNotasProspecto();
                var result = cnCapNotasProspecto.ObtenerPorProspecto(Sesion, idCte);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                //Falta manejar los estados de error: pérdida de conexión a base de datos, error general(desbordamiento de pila, etc.), identificador de cliente inválido, etc.
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
            
        }

        // 21 Ene 2019 RFH
        // CRM3 - Regresa el listado de notas del cliente.

        [HttpGet]
        public eResponse<List<eNotasProspecto>> Get(int Id_Cte, int Id_Notas)
        {
            eResponse<List<eNotasProspecto>> result = new eResponse<List<eNotasProspecto>>();
            List<eNotasProspecto> lst = new List<eNotasProspecto>();
            try
            {
                CN_CapNotasProspecto cnCapNotasProspecto = new CN_CapNotasProspecto();
                lst = cnCapNotasProspecto.CrmNotasProspecto(Sesion.Id_Emp,Sesion.Id_Cd,Id_Cte,Sesion);

                result.Datos = lst;
                result.Estado = 1;                
            }
            catch (Exception ex)
            {                
                result.Estado = -1;
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