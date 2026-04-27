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
using System.Threading.Tasks;
using SIANWEB.Core.Web.API;

namespace SIANWEB.WebService.PortalRIK.GestionPromocion
{
    /// <summary>
    /// Controlador del repositorio CrmOportunidadesProductos
    /// </summary>
    public class CrmOportunidadesProductosController
        : BaseWebAPIController
    {
        /// <summary>
        /// Obtiene el LISTADO de PRODUCTOS de un PROYECTO
        /// </summary>
        /// <param name="Id_CrmOportunidad">Identificador del proyecto</param>
        /// <param name="Id_Cte">Identificador del cliente</param>
        /// <returns>Task[HttpResponseMessage]</returns>
        [HttpGet]
        public Task<HttpResponseMessage> Get(int Id_CrmOportunidad, int Id_Cte)
        {
            //Se guarda el contexto de la llamada para capturarlos por referencia en las expresiones lambda.
            var currentContext = HttpContext.Current;
            var request = Request;
            try
            {
                //Se crea la tarea
                var t = Task<HttpResponseMessage>.Factory.StartNew(() =>
                {
                    //Se reestablece el contexto
                    HttpContext.Current = currentContext;
                    Request = request;
                    CN_CrmOportunidadesProductos cnCrmOportunidadesProductos = new CN_CrmOportunidadesProductos();
                    try
                    {
                        using (var ibt = CN_FabricaTransaccionNegocios.Default(Sesion))
                        {
                            ibt.Begin();
                            var result = cnCrmOportunidadesProductos.ObtenerProductosPorOportunidad(
                                Sesion, Id_CrmOportunidad, Id_Cte, ibt);

                            return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
                        }
                    }
                    catch (Exception ex)
                    {
                        return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                });
                return t;
            }
            catch (Exception ex)
            {
                //Se manda a bitácora el evento de la excepción
                Logger.Error("CrmOportunidadesProductosController::Get", ex);
                return Task<HttpResponseMessage>.Factory.StartNew(() => 
                {
                    Request = request;
                    return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex); 
                });
            }
        }

        //JUN06-2020 RFH 

        [HttpGet]
        public eResponse<List<eCrmOportunidadesProducto>> Get_ListadoProductosByProyecto(
            int Id_Op, int Id_Cte)
        {
            eResponse<List<eCrmOportunidadesProducto>> result = new eResponse<List<eCrmOportunidadesProducto>>();
            List<eCrmOportunidadesProducto> lst = new List<eCrmOportunidadesProducto>();
            result.Estado = 0;
           
            try
            {
                CN_CrmOportunidadesProductos CN = new CN_CrmOportunidadesProductos();

                lst = CN.ObtenerProductosPorOportunidad_Ver2(
                    Sesion.Id_Emp, Sesion.Id_Cd, Id_Op, Id_Cte, Sesion.Emp_Cnx);   
                    
                result.Estado = 1;
                result.Datos = lst;
            }
            catch (Exception ex)
            {
                result.Estado = -1;
                result.Datos = null;
                result.Mensaje = ex.ToString();
            }
            return result;
        }

        //
        // CRM - Insertar Producto en Listado de Productos de Proyecto EF
        //

        [HttpPost]
        public HttpResponseMessage Post([FromBody]CrmOportunidadesProducto model)
        {
            CN_CrmOportunidadesProductos cnCrmOportunidadesProductos = new CN_CrmOportunidadesProductos();
            model.Id_Emp = Sesion.Id_Emp;
            model.Id_Cd = Sesion.Id_Cd;
            model.Id_Rik = Sesion.Id_Rik;

            if (model.Id_Prd == 0)
            {                
                throw new Exception("Falta la clave de producto");
            }

            CrmOportunidadesProducto result = null;
            try
            {
                int iResult = 0;
                result = cnCrmOportunidadesProductos.Crear(Sesion, model, ref iResult);

                if (iResult == 3)
                {
                    return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
                }

                if (iResult == 2)
                {
                    return Request.CreateResponse(System.Net.HttpStatusCode.Conflict, result);
                }

                if (iResult == 1)
                {
                    return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, result);
                }

                return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }

        }

        //
        // JUN04-2020 
        // CRM - Insertar Producto en Listado de Productos de Proyecto SP
        //

        [HttpPut]
        public eResponse<int> CrmOportunidadesProductos_InsertUpdate(eCrmOportunidadesProducto P)
        {
            eResponse<int> result = new eResponse<int>();
            int iStatus = 0;
            int AfecteRows = 0;
            try
            {
                CN_CrmOportunidadesProductos CN_OP = new CN_CrmOportunidadesProductos();
                P.Id_Emp = Sesion.Id_Emp;
                P.Id_Cd = Sesion.Id_Cd;
                P.Id_Rik = Sesion.Id_Rik;                
                //Los otros campos pasan directo de la entidad

                if (P.Accion == 1)
                {
                    // Insert Update 
                    AfecteRows = CN_OP.CrmOportunidadesProductos_InsertUpdate(P, 1, Sesion.Emp_Cnx);
                }
                if (P.Accion == 2)
                {
                    // Update
                    AfecteRows = CN_OP.CrmOportunidadesProductos_InsertUpdate(P, 2, Sesion.Emp_Cnx);
                }

                result.Estado = AfecteRows;
            }
            catch (Exception ex)
            {
                result.Estado = -1;
                result.Datos = 0;
            }

            return result;
        }

        //
        // JUN10-2020 
        // CRM - UPDATE PRODUCTO
        //

        /*
        [HttpPut]
        public eResponse<int> CrmOportunidadesProductos_Update(eCrmOportunidadesProducto P)
        {
            eResponse<int> result = new eResponse<int>();
            int iStatus = 0;
            int AfecteRows = 0;
            try
            {
                CN_CrmOportunidadesProductos CN_OP = new CN_CrmOportunidadesProductos();
                P.Id_Emp = Sesion.Id_Emp;
                P.Id_Cd = Sesion.Id_Cd;
                P.Id_Rik = Sesion.Id_Rik;

                //Los otros campos pasan directo de la entidad

                AfecteRows = CN_OP.CrmOportunidadesProductos_InsertUpdate(P, 2, Sesion.Emp_Cnx);

                result.Estado = AfecteRows;
            }
            catch (Exception ex)
            {
                result.Estado = -1;
                result.Datos = 0;
            }
            return result;
        }
        */
        

        /*
        [HttpPut]
        public HttpResponseMessage Put([FromBody]CrmOportunidadesProducto model)
        {
            CN_CrmOportunidadesProductos cnCrmOportunidadesProductos = new CN_CrmOportunidadesProductos();
            model.Id_Emp = Sesion.Id_Emp;
            model.Id_Cd = Sesion.Id_Cd;
            model.Id_Rik = Sesion.Id_Rik;
            try
            {
                cnCrmOportunidadesProductos.Actualizar(Sesion, model);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }
        */

        [HttpDelete]
        public HttpResponseMessage Delete(int idCte, int idOp, int idPrd)
        {
            int iDeleteResult = -1;

            try
            {
                CN_CrmOportunidadesProductos cnCrmOportunidadesProductos = new CN_CrmOportunidadesProductos();
                
                //cnCrmOportunidadesProductos.Eliminar(Sesion, idCte, idOp, idPrd);
                // 19 Sep 2018 RFH 
                // Eliminar con SP
                iDeleteResult = cnCrmOportunidadesProductos.SP_Eliminar(Sesion, idCte, idOp, idPrd);
                if (iDeleteResult == 1)
                {
                    // Al borrar un prducto regresa el estatus a inicio.
                    eCapValProyecto ecVP = new eCapValProyecto();
                    ecVP.Id_Emp = Sesion.Id_Emp;
                    ecVP.Id_Cd = Sesion.Id_Cd;
                    ecVP.Id_Op = idOp;
                    ecVP.Id_Vap = 0;
                    ecVP.Id_Cte = 0;
                    ecVP.Estatus = 2;
                    ecVP.Vap_Estatus = "A"; // Cambia estatus a incio de captura 
                    ecVP.Vap_Estatus2 = 3;
                    ecVP.Id_Rik = 0;
                    ecVP.Id_Ter = 0;
                    ecVP.Vap_Fecha = "";
                    ecVP.Id_U = 0;
                    ecVP.Vap_Nota = "";
                    ecVP.Vap_UtilidadRemanente = 0;
                    ecVP.Vap_ValorPresenteNeto = 0;
                    ecVP.MotivoParaAutorizacion = "";
                    ecVP.Tipo = 2; // Actualiza Solo Estatus

                    CN_CapValProyecto cVP = new CN_CapValProyecto();
                    int iRes = cVP.CRM2_InsertUpdate_Tipo(ecVP, Sesion.Emp_Cnx);               
                }
                    
                if (iDeleteResult == 1)
                {
                    return Request.CreateResponse(System.Net.HttpStatusCode.OK);
                } else if (iDeleteResult == 0)
                {
                    return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError);
                }
                return Request.CreateResponse(System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
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