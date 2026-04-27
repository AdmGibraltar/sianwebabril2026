using CapaEntidad;
using CapaNegocios;
using ClosedXML.Excel;
using DevExpress.Office.Utils;
using DocumentFormat.OpenXml.Vml.Office;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SIANWEB.WebService
{
    public class CapOrdenServicio_AdminController : ApiController
    {
        #region Propiedades y Métodos Auxiliares

        /// <summary>
        /// Obtiene la sesión actual del usuario
        /// </summary>
        private Sesion SesionActual
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

        private string strConexionCentral
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["strConnectionSIANCentral"];
            }
        }

        /// <summary>
        /// Valida que exista una sesión activa
        /// Si no existe, retorna un error 401 (Unauthorized)
        /// </summary>
        /// <returns>HttpResponseMessage con error si no hay sesión, null si la sesión es válida</returns>
        private HttpResponseMessage ValidarSesion()
        {
            if (SesionActual == null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new
                {
                    mensaje = "Sesión no encontrada",
                    error = "Su sesión ha terminado. Por favor, inicie sesión nuevamente."
                });
            }
            return null;
        }

        /// <summary>
        /// Parsea una cadena a DateTime nullable
        /// </summary>
        /// <param name="fecha">Cadena con la fecha</param>
        /// <returns>DateTime? con la fecha parseada o null si no es válida</returns>
        private DateTime? ParseDate(string fecha)
        {
            if (string.IsNullOrWhiteSpace(fecha))
                return null;

            DateTime resultado;
            if (DateTime.TryParse(fecha, out resultado))
                return resultado;

            return null;
        }

        /// <summary>
        /// Crea una respuesta exitosa con datos JSON
        /// </summary>
        /// <param name="data">Datos a retornar</param>
        /// <returns>HttpResponseMessage con status 200 OK</returns>
        private HttpResponseMessage RespuestaExitosa(object data)
        {
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        /// <summary>
        /// Crea una respuesta de error
        /// </summary>
        /// <param name="mensaje">Mensaje de error</param>
        /// <param name="statusCode">Código de estado HTTP (por defecto 500)</param>
        /// <returns>HttpResponseMessage con el error</returns>
        private HttpResponseMessage RespuestaError(string mensaje, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            return Request.CreateResponse(statusCode, new
            {
                error = mensaje,
                mensaje = mensaje
            });
        }

        #endregion

        #region Métodos API

        [HttpGet]
        public HttpResponseMessage ConsultarOrdenServicio(int intIdOrdenServicio)
        {
            try
            {
                // Validar sesión
                var errorSesion = ValidarSesion();
                if (errorSesion != null)
                {
                    return errorSesion;
                }

                entOrdenServicioDetalle det = new entOrdenServicioDetalle();
                List<entOrdenServicioProductos> lstProductos = new List<entOrdenServicioProductos>();
                entOrdenServicioClienteDireccion dir = new entOrdenServicioClienteDireccion(); 
                List<Comun> lstRol = new List<Comun>(),  lstTerritorio = new List<Comun>(), lstUsuario = new List<Comun>();
                List<entOrdenServicioProductos> lstServicio = new List<entOrdenServicioProductos>();
                List<entRolUsuario> lstRolUsuario = new List<entRolUsuario>();
                List<Comun> lstCambioCompromiso = new List<Comun>();  // NUEVO: Lista de motivos de cambio compromiso
                det.Id_Emp = SesionActual.Id_Emp;
                det.Id_Cd = SesionActual.Id_Cd;
                det.intIdOrdenServicio = intIdOrdenServicio;
                new CN_OrdenServicio_Admin().ConsultarOrdenServicio(SesionActual.Emp_Cnx, strConexionCentral, ref det, ref lstProductos, ref dir, ref lstRol, ref lstServicio, ref lstTerritorio, ref lstUsuario, ref lstRolUsuario, ref lstCambioCompromiso);
                return RespuestaExitosa(new { entOrdenServicioDetalle = det, entOrdenServicioClienteDireccion = dir, lstProductos, lstRol, lstServicio, lstTerritorio, lstUsuario, lstRolUsuario, lstCambioCompromiso });
            }
            catch (Exception ex)
            {
                return RespuestaError("Error en el servidor: " + ex.Message);
            }
        }
        
        
        [HttpGet]
        public HttpResponseMessage ConsultarCliente(string strNombreCliente, int intIdCliente)
        {
            try
            {
                // Validar sesión
                var errorSesion = ValidarSesion();
                if (errorSesion != null)
                {
                    return errorSesion;
                }

                List<entOrdenServicioClienteDireccion> lstDireccion = new List<entOrdenServicioClienteDireccion>();
                var lstTerritorio = new List<Comun>();
                var lstUsuario = new List<Comun>();
                string strMensaje = string.Empty;
                new CN_OrdenServicio_Admin().ConsultarCliente(SesionActual.Emp_Cnx, intIdCliente, strNombreCliente, ref strMensaje, ref lstDireccion, ref lstTerritorio,ref lstUsuario);
                return RespuestaExitosa(new { strMensaje,lstDireccion, lstTerritorio, lstUsuario, intIdTerSel = -1, intIdUsuarioSel = -1 });
            }
            catch (Exception ex)
            {
                return RespuestaError("Error en el servidor: " + ex.Message);
            }
        }


        [HttpGet]
        public HttpResponseMessage CatalogoEstatus(int intRef1, int intRef2, int intRef3)
        {
            try
            {
                // Validar sesión
                var errorSesion = ValidarSesion();
                if (errorSesion != null)
                {
                    return errorSesion;
                }
               
                var lst = new CN_OrdenServicio_Admin().CatalogoEstatus(strConexionCentral);
                return RespuestaExitosa(lst);
            }
            catch (Exception ex)
            {
                return RespuestaError("Error en el servidor: " + ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage CatalogosAltaOrdenServicio(int intRef1, int intRef2, int intRef3, int intRef4)
        {
            try
            {
                // Validar sesión
                var errorSesion = ValidarSesion();
                if (errorSesion != null)
                {
                    return errorSesion;
                }

                List<Comun> lstRol = null;
                List<Comun> lstUsuarios = null;
                List<entOrdenServicioProductos> lstServicio = null;
                
                new CN_OrdenServicio_Admin().CatalogoServicios(strConexionCentral, SesionActual.Id_Emp, SesionActual.Id_Cd,  ref lstServicio);
                new CN_OrdenServicio_Admin().CatalogoUsuarios(SesionActual.Emp_Cnx, SesionActual.Id_Emp, SesionActual.Id_Cd, ref lstRol, ref lstUsuarios);
                return RespuestaExitosa(new { lstRol, lstUsuarios, lstServicio });
            }
            catch (Exception ex)
            {
                return RespuestaError("Error en el servidor: " + ex.Message);
            }
        }

        // 8 parametros
        [HttpGet]
        public HttpResponseMessage BuscarOrdenesServicios(string strNombreCliente = null, int intIdCteInicial = 0, int intIdCteFinal = 0, int intIdEstatus = 0, string dateFechaInicial = null, string dateFechaFinal = null, int intIdOrdenServicioInicial = 0, int intIdOrdenServicioFinal = 0)
        {
            try
            {
                // Validar sesión
                var errorSesion = ValidarSesion();
                if (errorSesion != null)
                {
                    return errorSesion;
                }

                var filtros = new entOrdenServicioFiltros
                {
                    Id_Emp = SesionActual.Id_Emp,
                    Id_Cd = SesionActual.Id_Cd,
                    strNombreCliente = strNombreCliente,
                    intIdCteInicial = intIdCteInicial,
                    intIdCteFinal = (intIdCteFinal ==0 && intIdCteInicial>0)? intIdCteInicial: intIdCteFinal,
                    intIdEstatus = intIdEstatus,
                    dateFechaInicial = ParseDate(dateFechaInicial),
                    dateFechaFinal = ParseDate(dateFechaFinal),
                    intIdOrdenServicioInicial = intIdOrdenServicioInicial,
                    intIdOrdenServicioFinal = (intIdOrdenServicioFinal == 0 && intIdOrdenServicioInicial > 0) ? intIdOrdenServicioInicial : intIdOrdenServicioFinal,
                     
                };
                var lst = new CN_OrdenServicio_Admin().BuscarOrdenesServicios(SesionActual.Emp_Cnx, filtros);
                
                // Agregar el ID del usuario en sesión a cada registro para la validación en el cliente
                foreach (var orden in lst)
                {
                    orden.intUsuarioSesion = SesionActual.Id_U;
                }
                
                return RespuestaExitosa(lst);
            }
            catch (Exception ex)
            {
                return RespuestaError("Error en el servidor: " + ex.Message);
            }
        }


        // --- NUEVO: Buscar para monitor ---
        [HttpGet]
        public HttpResponseMessage BuscarOrdenServicioMonitor(string dateFechaInicial = null, string dateFechaFinal = null, int intIdOrdenServicioInicial = 0, int intIdOrdenServicioFinal = 0, int intIdEstatus = 0)
        {
            try
            {
                // Validar sesión
                var errorSesion = ValidarSesion();
                if (errorSesion != null)
                {
                    return errorSesion;
                }
                List<entOrdenServicioMonitor> lstOrdenProductos = new List<entOrdenServicioMonitor>();
                List<entOrdenServicioMonitor> lstOrdenCliente = new List<entOrdenServicioMonitor>();
                entOrdenServicoIndicadores entIndicadores = new entOrdenServicoIndicadores();
                var filtros = new entOrdenServicioFiltros
                {
                    Id_Emp = SesionActual.Id_Emp,
                    Id_Cd = SesionActual.Id_Cd,
                    dateFechaInicial = ParseDate(dateFechaInicial),
                    dateFechaFinal = ParseDate(dateFechaFinal),
                    intIdOrdenServicioInicial = intIdOrdenServicioInicial,
                    intIdOrdenServicioFinal = intIdOrdenServicioFinal,
                    intIdEstatus = intIdEstatus
                };
                new CN_OrdenServicio_Admin().BuscarOrdenServicioMonitor(SesionActual.Emp_Cnx, filtros, ref lstOrdenProductos, ref lstOrdenCliente, ref  entIndicadores);
                return RespuestaExitosa( new { lstOrdenProductos, lstOrdenCliente , entIndicadores });
            }
            catch (Exception ex)
            {
                return RespuestaError("Error en el servidor: " + ex.Message);
            }
        }
        // -------------------------------------------------

        public enum EstatusOrdenServicio
        {
            Captura = 1,
            Asignada = 2,
            Impreso = 3,
            Confirmado = 4,
            Cerrado = 5,
            Baja = 6
        }

        // Método para guardar orden de servicio with productos
        [HttpGet]
        public HttpResponseMessage GuardarOrdenServicio(int intIdOrdenServicio, bool isExtemporaneo, string dateCompromiso, int intMotivoCambioFecha, long intIdServicio, string strDescripcionServicio, int intIdCliente, int intIdTer, string strproductos, string strProductosAEliminar = "[]", string strRolUsuario = "[]", string strIdRolUsuarioEliminar = "[]")
        {
            try
            {
                // Validar sesión
                var errorSesion = ValidarSesion();
                if (errorSesion != null)
                {
                    return errorSesion;
                }

                var det = new entOrdenServicioDetalle
                {
                    Id_Emp = SesionActual.Id_Emp,
                    Id_Cd = SesionActual.Id_Cd,
                    intIdOrdenServicio = intIdOrdenServicio,
                    isExtemporaneo = isExtemporaneo,
                    dateCompromiso = ParseDate(dateCompromiso),
                    intMotivoCambioFecha = intMotivoCambioFecha,  // NUEVO: Agregar motivo de cambio de fecha
                    intIdServicio = intIdServicio,
                    strDescripcionServicio = strDescripcionServicio,
                    intIdCliente = intIdCliente,
                    intIdTer = intIdTer,
                    intIdUsuarioInserta = SesionActual.Id_U,
                    strUsuarioInserta = SesionActual.U_Nombre,
                    Id_Estatus = (int)EstatusOrdenServicio.Asignada
                };



                // Deserializar el string de productos a lista de objetos
                List<entOrdenServicioProductos> lstProductos = new List<entOrdenServicioProductos>();
                if (!string.IsNullOrWhiteSpace(strproductos))
                {
                    try
                    {
                        // Deserializar directamente a la entidad
                        lstProductos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<entOrdenServicioProductos>>(strproductos);
                        det.dcmSubTotal = lstProductos.Where(y=> y.TipoProducto == 1).Sum(x => x.Total);
                        det.dcmIva = 0;
                        det.dcmTotal = det.dcmSubTotal;
                    }
                    catch (Exception exJson)
                    {
                        return RespuestaError("Error al procesar productos: " + exJson.Message, HttpStatusCode.BadRequest);
                    }
                }
                
                // Deserializar productos a eliminar
                List<int> lstProductosAEliminar = new List<int>();
                if (!string.IsNullOrWhiteSpace(strProductosAEliminar))
                {
                    try
                    {
                        lstProductosAEliminar = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(strProductosAEliminar);
                    }
                    catch (Exception exJson)
                    {
                        return RespuestaError("Error al procesar productos a eliminar: " + exJson.Message, HttpStatusCode.BadRequest);
                    }
                }
                
                // Deserializar roles y usuarios
                List<entRolUsuario> lstRolUsuario = new List<entRolUsuario>();
                if (!string.IsNullOrWhiteSpace(strRolUsuario))
                {
                    try
                    {
                        lstRolUsuario = Newtonsoft.Json.JsonConvert.DeserializeObject<List<entRolUsuario>>(strRolUsuario);
                        // Asignar Id_Emp, Id_Cd a cada elemento
                        foreach (var ru in lstRolUsuario)
                        {
                            ru.intIdEmp = SesionActual.Id_Emp;
                            ru.intIdCd = SesionActual.Id_Cd;
                        }
                    }
                    catch (Exception exJson)
                    {
                        return RespuestaError("Error al procesar roles y usuarios: " + exJson.Message, HttpStatusCode.BadRequest);
                    }
                }
                
                // Deserializar roles y usuarios a eliminar
                List<int> lstIdRolUsuarioEliminar = new List<int>();
                if (!string.IsNullOrWhiteSpace(strIdRolUsuarioEliminar))
                {
                    try
                    {
                        lstIdRolUsuarioEliminar = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(strIdRolUsuarioEliminar);
                    }
                    catch (Exception exJson)
                    {
                        return RespuestaError("Error al procesar roles y usuarios a eliminar: " + exJson.Message, HttpStatusCode.BadRequest);
                    }
                }
                
                string mensaje = string.Empty;
                new CN_OrdenServicio_Admin().GuardarOrdenServicio(SesionActual.Emp_Cnx, det, lstProductos, lstProductosAEliminar, lstRolUsuario, lstIdRolUsuarioEliminar, ref mensaje);
                return RespuestaExitosa(new { mensaje });
            }
            catch (Exception ex)
            {
                return RespuestaError("Error en el servidor: " + ex.Message);
            }
        }
        

      // 9 parametros
        [HttpGet]
        public HttpResponseMessage DescargarExcelOrdenesServicio(string strNombreCliente = null, int intIdCteInicial = 0, int intIdCteFinal = 0, int intIdEstatus = 0, string strEstatus = null, string dateFechaInicial = null, string dateFechaFinal = null, int intIdOrdenServicioInicial = 0, int intIdOrdenServicioFinal = 0)
        {
            try
            {
                // Validar sesión
                var errorSesion = ValidarSesion();
                if (errorSesion != null)
                {
                    return errorSesion;
                }

                var filtros = new entOrdenServicioFiltros
                {
                    Id_Emp = SesionActual.Id_Emp,
                    Id_Cd = SesionActual.Id_Cd,
                    strNombreCliente = strNombreCliente,
                    intIdCteInicial = intIdCteInicial,
                    intIdCteFinal = (intIdCteFinal == 0 && intIdCteInicial > 0) ? intIdCteInicial : intIdCteFinal,
                    intIdEstatus = intIdEstatus,
                    strEstatus = strEstatus,
                    dateFechaInicial = ParseDate(dateFechaInicial),
                    dateFechaFinal = ParseDate(dateFechaFinal),
                    intIdOrdenServicioInicial = intIdOrdenServicioInicial,
                    intIdOrdenServicioFinal = (intIdOrdenServicioFinal == 0 && intIdOrdenServicioInicial > 0) ? intIdOrdenServicioInicial : intIdOrdenServicioFinal,
                };
                var lst = new CN_OrdenServicio_Admin().BuscarOrdenesServicios(SesionActual.Emp_Cnx, filtros);
                var wb = ConstruirExcel(filtros, lst);
                using (var ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    ms.Position = 0;
                    var result = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new ByteArrayContent(ms.ToArray())
                    };
                    result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                    result.Content.Headers.ContentDisposition.FileName = "OrdenesServicio.xlsx";
                    result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                    return result;
                }
            }
            catch (Exception ex)
            {
                return RespuestaError("Error en el servidor: " + ex.Message);
            }
        }

        // --- NUEVO: Exportar para Monitor con columnas extendidas ---
        [HttpGet]
        public HttpResponseMessage ExportarOrdenServicioMonitor(string dateFechaInicial = null, string dateFechaFinal = null, int intIdOrdenServicioInicial = 0, int intIdOrdenServicioFinal = 0, int intIdEstatus = 0, string strEstatus= null)
        {
            try
            {
                // Validar sesión
                var errorSesion = ValidarSesion();
                if (errorSesion != null)
                {
                    return errorSesion;
                }
               
                List<entOrdenServicioMonitor> lstOrdenProductos = new List<entOrdenServicioMonitor>();
                List<entOrdenServicioMonitor> lstOrdenCliente = new List<entOrdenServicioMonitor>();
                entOrdenServicoIndicadores entIndicadores = new entOrdenServicoIndicadores();
                var filtros = new entOrdenServicioFiltros
                {
                    Id_Emp = SesionActual.Id_Emp,
                    Id_Cd = SesionActual.Id_Cd,
                    dateFechaInicial = ParseDate(dateFechaInicial),
                    dateFechaFinal = ParseDate(dateFechaFinal),
                    intIdOrdenServicioInicial = intIdOrdenServicioInicial,
                    intIdOrdenServicioFinal = intIdOrdenServicioFinal,
                    intIdEstatus = intIdEstatus,
                    strEstatus = strEstatus
                };
                new CN_OrdenServicio_Admin().BuscarOrdenServicioMonitor(SesionActual.Emp_Cnx, filtros, ref lstOrdenProductos, ref lstOrdenCliente, ref entIndicadores);

                var wb = ConstruirExcelMonitor(filtros, lstOrdenProductos, lstOrdenCliente, entIndicadores);
                using (var ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    ms.Position = 0;
                    var result = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new ByteArrayContent(ms.ToArray())
                    };
                    result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                    result.Content.Headers.ContentDisposition.FileName = "MonitorOrdenServicio.xlsx";
                    result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                    return result;
                }
            }
            catch (Exception ex)
            {
                return RespuestaError("Error en el servidor: " + ex.Message);
            }
        }
        // -------------------------------------------------

        [HttpGet]
        public HttpResponseMessage ConsultarCatMotivoIncompleto(int intIdOrdenServicio)
        {
            try
            {
                // Validar sesión
                var errorSesion = ValidarSesion();
                if (errorSesion != null)
                {
                    return errorSesion;
                }

                var lst = new CN_OrdenServicio_Admin().ConsultarCatMotivoIncompleto(strConexionCentral, SesionActual.Id_Emp, SesionActual.Id_Cd);
                return RespuestaExitosa(lst);
            }
            catch (Exception ex)
            {
                return RespuestaError("Error en el servidor: " + ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage ConsultarCatMotivoCambioCompromiso(int intIdOrdenServicio)
        {
            try
            {
                // Validar sesión
                var errorSesion = ValidarSesion();
                if (errorSesion != null)
                {
                    return errorSesion;
                }

                var lst = new CN_OrdenServicio_Admin().ConsultarCatMotivoCambioCompromiso(strConexionCentral, SesionActual.Id_Emp, SesionActual.Id_Cd);
                return RespuestaExitosa(lst);
            }
            catch (Exception ex)
            {
                return RespuestaError("Error en el servidor: " + ex.Message);
            }
        }
        
        private enum catTipoConfirmacion
        {
            Completo=1,
            Incompleto=2
        }

        // Método para guardar confirmación
        [HttpGet]
        public HttpResponseMessage GuardarConfirmacion(int intIdOrdenServicio, bool isCompleto, int intMotivoIncompleto)
        {
            try
            {
                // Validar sesión
                var errorSesion = ValidarSesion();
                if (errorSesion != null)
                {
                    return errorSesion;
                }
                int intTipoConfirmacion = isCompleto ? (int)catTipoConfirmacion.Completo : (int)catTipoConfirmacion.Incompleto;

                string strMensaje = string.Empty;
                new CN_OrdenServicio_Admin().GuardarConfirmacion(
                    SesionActual.Emp_Cnx,
                    SesionActual.Id_Emp,
                    SesionActual.Id_Cd,
                    intIdOrdenServicio,
                    intTipoConfirmacion,
                    intMotivoIncompleto,
                    SesionActual.Id_U,
                    ref strMensaje
                );
                return RespuestaExitosa(new { strMensaje });
            }
            catch (Exception ex)
            {
                return RespuestaError("Error en el servidor: " + ex.Message);
            }
        }

        // Método para consultar catálogo de motivos de eliminación
        [HttpGet]
        public HttpResponseMessage ConsultarCatMotivoEliminacion()
        {
            try
            {
                // Validar sesión
                var errorSesion = ValidarSesion();
                if (errorSesion != null)
                {
                    return errorSesion;
                }

                var lista = new CN_OrdenServicio_Admin().ConsultarCatMotivoEliminacion(
                    strConexionCentral,
                    SesionActual.Id_Emp,
                    SesionActual.Id_Cd
                );
                return RespuestaExitosa(lista);
            }
            catch (Exception ex)
            {
                return RespuestaError("Error en el servidor: " + ex.Message);
            }
        }

        // Método para eliminar orden de servicio (eliminación lógica)
        [HttpGet]
        public HttpResponseMessage EliminarOrdenServicio(int intIdOrdenServicio, int intMotivoEliminacion)
        {
            try
            {
                // Validar sesión
                var errorSesion = ValidarSesion();
                if (errorSesion != null)
                {
                    return errorSesion;
                }

                string mensaje = string.Empty;
                new CN_OrdenServicio_Admin().EliminarOrdenServicio(
                    SesionActual.Emp_Cnx,
                    SesionActual.Id_Emp,
                    SesionActual.Id_Cd,
                    SesionActual.Id_U,
                    intIdOrdenServicio,
                    intMotivoEliminacion,
                    ref mensaje
                );
                return RespuestaExitosa(new { mensaje });
            }
            catch (Exception ex)
            {
                return RespuestaError("Error en el servidor: " + ex.Message);
            }
        }

        private ClosedXML.Excel.XLWorkbook ConstruirExcel(entOrdenServicioFiltros filtros, List<entOrdenServicio> data )
        {
            var wb = new ClosedXML.Excel.XLWorkbook();
            var ws = wb.Worksheets.Add("OrdenesServicio");
            int r = 1;
            
            // Título del reporte en negritas y tamaño 16, combinado de A1 a H1
            ws.Cell(r, 1).Value = "Reporte de Órdenes de Servicio";
            ws.Range(r, 1, r, 8).Merge(); // Combinar celdas A1 a H1
            ws.Cell(r, 1).Style.Font.Bold = true;
            ws.Cell(r, 1).Style.Font.FontSize = 16;
            ws.Cell(r, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            r++;
            
            // Fecha creación, combinar A2 y B2
            ws.Cell(r, 1).Value = "Fecha creación: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            ws.Range(r, 1, r, 2).Merge(); // Combinar celdas A2 y B2
            r++;
            
            ws.Cell(r++, 1).Value = "Filtros:";
            
            // Filtros con valores en negritas, descripción alineada a la derecha
            ws.Cell(r, 1).Value = "Nombre Cliente:"; 
            ws.Cell(r, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            ws.Cell(r, 2).Value = filtros.strNombreCliente;
            ws.Cell(r, 2).Style.Font.Bold = true;
            r++;
            
            ws.Cell(r, 1).Value = "Cliente Inicial:"; 
            ws.Cell(r, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            ws.Cell(r, 2).Value = filtros.intIdCteInicial == 0 ? "" : filtros.intIdCteInicial.ToString();
            ws.Cell(r, 2).Style.Font.Bold = true;
            r++;
            
            ws.Cell(r, 1).Value = "Cliente Final:"; 
            ws.Cell(r, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            ws.Cell(r, 2).Value = filtros.intIdCteFinal == 0 ? "" : filtros.intIdCteFinal.ToString();
            ws.Cell(r, 2).Style.Font.Bold = true;
            r++;
            
            ws.Cell(r, 1).Value = "Estatus:"; 
            ws.Cell(r, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            ws.Cell(r, 2).Value = filtros.strEstatus;
            ws.Cell(r, 2).Style.Font.Bold = true;
            r++;
            
            ws.Cell(r, 1).Value = "Fecha Inicial:"; 
            ws.Cell(r, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            ws.Cell(r, 2).Value = filtros.dateFechaInicial;
            ws.Cell(r, 2).Style.Font.Bold = true;
            r++;
            
            ws.Cell(r, 1).Value = "Fecha Final:"; 
            ws.Cell(r, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            ws.Cell(r, 2).Value = filtros.dateFechaFinal;
            ws.Cell(r, 2).Style.Font.Bold = true;
            r++;
            
            ws.Cell(r, 1).Value = "Orden Servicio Inicial:"; 
            ws.Cell(r, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            ws.Cell(r, 2).Value = filtros.intIdOrdenServicioInicial == 0 ? "" : filtros.intIdOrdenServicioInicial.ToString();
            ws.Cell(r, 2).Style.Font.Bold = true;
            r++;
            
            ws.Cell(r, 1).Value = "Orden Servicio Final:"; 
            ws.Cell(r, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            ws.Cell(r, 2).Value = filtros.intIdOrdenServicioFinal == 0 ? "" : filtros.intIdOrdenServicioFinal.ToString();
            ws.Cell(r, 2).Style.Font.Bold = true;
            r++;
            
            r++; // Fila en blanco
            int headerRow = r;
            string[] headers = {"Cve Servicio", "Número", "Fecha", "Núm. cte.", "Cliente", "Subtotal", "IVA", "Total"};
            
            // Encabezados con fondo azul claro y activar filtrado
            for(int c = 0; c < headers.Length; c++)
            {
                ws.Cell(r, c + 1).Value = headers[c];
                ws.Cell(r, c + 1).Style.Fill.BackgroundColor = XLColor.LightBlue;
                ws.Cell(r, c + 1).Style.Font.Bold = true;
            }
            
            // Activar autofiltro en la fila de encabezados
            ws.Range(r, 1, r, headers.Length).SetAutoFilter();
            
            r++;
            int firstDataRow = r;
            
            // Datos con formato de moneda para Subtotal, IVA y Total
            foreach(var o in data)
            {
                ws.Cell(r, 1).Value = o.strCveServicio;
                ws.Cell(r, 2).Value = o.intIdOrdenServicio;
                ws.Cell(r, 3).Value = o.dateFecha;
                ws.Cell(r, 4).Value = o.intIdCte;
                ws.Cell(r, 5).Value = o.strNombreComercial;
                
                // Formato moneda para Subtotal, IVA y Total
                ws.Cell(r, 6).Value = o.dcmSubtotal;
                ws.Cell(r, 6).Style.NumberFormat.Format = "$#,##0.00";
                
                ws.Cell(r, 7).Value = o.dcmIva;
                ws.Cell(r, 7).Style.NumberFormat.Format = "$#,##0.00";
                
                ws.Cell(r, 8).Value = o.dcmTotal;
                ws.Cell(r, 8).Style.NumberFormat.Format = "$#,##0.00";
                
                r++;
            }
            
            // Aplicar bordes a toda la tabla (encabezado + datos)
            if (data.Count > 0)
            {
                ws.Range(headerRow, 1, r - 1, headers.Length).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ws.Range(headerRow, 1, r - 1, headers.Length).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            }
            
            ws.Columns().AdjustToContents();
            return wb;
        }

        // Excel extendido para monitor
        private ClosedXML.Excel.XLWorkbook ConstruirExcelMonitor(entOrdenServicioFiltros filtros, List<entOrdenServicioMonitor> lstOrdenProductos, List<entOrdenServicioMonitor> lstOrdenCliente, entOrdenServicoIndicadores entIndicadores)
        {
            var wb = new ClosedXML.Excel.XLWorkbook();
            var wsClt = wb.Worksheets.Add("Monitor Cliente");
            var wsPrd = wb.Worksheets.Add("Monitor Cliente Det Pro.");
            
            // Construir hoja de Cliente
            int r = 1;
            
            // Título del reporte en negritas y tamaño 16, combinado de A1 a J1
            wsClt.Cell(r, 1).Value = "Reporte Monitoreo Orden Servicio";
            wsClt.Range(r, 1, r, 16).Merge(); // Combinar celdas A1 a P1
            wsClt.Cell(r, 1).Style.Font.Bold = true;
            wsClt.Cell(r, 1).Style.Font.FontSize = 16;
            wsClt.Cell(r, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            r++;
            
            // Fecha creación, combinar A2 y B2
            wsClt.Cell(r, 1).Value = "Fecha creación: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            wsClt.Range(r, 1, r, 2).Merge(); // Combinar celdas A2 y B2
            r++;
            
            // Tabla de indicadores en G3:J5
            // Fila 3 - Encabezados
            wsClt.Cell(3, 9).Value = "Confirmados"; // G3
            wsClt.Cell(3, 10).Value = "Órdenes de servicios"; // H3
            wsClt.Cell(3, 11).Value = "Montos"; // I3
            wsClt.Cell(3, 12).Value = "%"; // J3
            
            // Aplicar formato LightBlue y negritas a G3:J3
            for (int c = 9; c <= 12; c++)
            {
                wsClt.Cell(3, c).Style.Fill.BackgroundColor = XLColor.LightBlue;
                wsClt.Cell(3, c).Style.Font.Bold = true;
            }
            
            // Fila 4 - Ticket completos
            wsClt.Cell(4, 9).Value = "Ticket completos"; // i4
            wsClt.Cell(4, 10).Value = entIndicadores.intCompleto; // j4
            wsClt.Cell(4, 11).Value = entIndicadores.dcmCompletoMonto; // k4
            wsClt.Cell(4, 11).Style.NumberFormat.Format = "$#,##0.00";
            wsClt.Cell(4, 12).Value = entIndicadores.dcmCompletoPorcentaje; // l4
            wsClt.Cell(4, 12).Style.NumberFormat.Format = "0.00%";
            
            // Fila 5 - Ticket incompletos
            wsClt.Cell(5, 9).Value = "Ticket incompletos"; // G5
            wsClt.Cell(5, 10).Value = entIndicadores.intIncompleto; // H5
            wsClt.Cell(5, 11).Value = entIndicadores.dcmIncompletoMonto; // I5
            wsClt.Cell(5, 11).Style.NumberFormat.Format = "$#,##0.00";
            wsClt.Cell(5, 12).Value = entIndicadores.dcmIncompletoPorcentaje; // J5
            wsClt.Cell(5, 12).Style.NumberFormat.Format = "0.00%";
            
            // Aplicar bordes a la tabla de indicadores
            wsClt.Range(3, 9, 5, 12).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            wsClt.Range(3, 9, 5, 12).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            
            wsClt.Cell(r++, 1).Value = "Filtros:";
            
            // Filtros con valores en negritas, descripción alineada a la derecha
            wsClt.Cell(r, 1).Value = "Estatus:"; 
            wsClt.Cell(r, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            wsClt.Cell(r, 2).Value = filtros.strEstatus;
            wsClt.Cell(r, 2).Style.Font.Bold = true;
            r++;
            
            wsClt.Cell(r, 1).Value = "Fecha Inicial:"; 
            wsClt.Cell(r, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            wsClt.Cell(r, 2).Value = filtros.dateFechaInicial;
            wsClt.Cell(r, 2).Style.Font.Bold = true;
            r++;
            
            wsClt.Cell(r, 1).Value = "Fecha Final:"; 
            wsClt.Cell(r, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            wsClt.Cell(r, 2).Value = filtros.dateFechaFinal;
            wsClt.Cell(r, 2).Style.Font.Bold = true;
            r++;
            
            wsClt.Cell(r, 1).Value = "Orden Servicio Inicial:"; 
            wsClt.Cell(r, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            wsClt.Cell(r, 2).Value = filtros.intIdOrdenServicioInicial == 0 ? "" : filtros.intIdOrdenServicioInicial.ToString();
            wsClt.Cell(r, 2).Style.Font.Bold = true;
            r++;
            
            wsClt.Cell(r, 1).Value = "Orden Servicio Final:"; 
            wsClt.Cell(r, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            wsClt.Cell(r, 2).Value = filtros.intIdOrdenServicioFinal == 0 ? "" : filtros.intIdOrdenServicioFinal.ToString();
            wsClt.Cell(r, 2).Style.Font.Bold = true;
            r++;
            
            r++; // Fila en blanco
            int headerRow = r;
            string[] headersCliente = {"Id cliente","Cliente","Orden de servicio","Estatus","Tipo confirmación","Matriz cuenta nacional","Usuario Creador","Usuario Asignado","Motivo Incompleto","Motivo Cambio Fecha Compromiso", "Fecha captura", "Fecha compromiso", "Fecha confirmacion","Tiempo respuesta","Unidades","Monto"};
            
            // Encabezados con fondo azul claro
            for(int c=0;c< headersCliente.Length;c++)
            {
                wsClt.Cell(r, c+1).Value = headersCliente[c];
                wsClt.Cell(r, c + 1).Style.Fill.BackgroundColor = XLColor.LightBlue;
                wsClt.Cell(r, c + 1).Style.Font.Bold = true;
            }
            
            // Activar autofiltro en la fila de encabezados
            wsClt.Range(r, 1, r, headersCliente.Length).SetAutoFilter();
            
            r++;
            int firstDataRow = r;
            
            foreach(var itemCliente in lstOrdenCliente)
            {
                wsClt.Cell(r,1).Value = itemCliente.intIdCte;
                wsClt.Cell(r,2).Value = itemCliente.strCliente;
                wsClt.Cell(r,3).Value = itemCliente.intIdSrv;
                wsClt.Cell(r,4).Value = itemCliente.strEstatus; 
                wsClt.Cell(r,5).Value = itemCliente.strTipoConfirmacion;
                wsClt.Cell(r,6).Value = itemCliente.strMatriz;
                wsClt.Cell(r,7).Value = itemCliente.strUsuarioCreador;
                wsClt.Cell(r,8).Value = itemCliente.strUsuarioAsignado;
                wsClt.Cell(r,9).Value = itemCliente.strMotivoIncompleto;
                wsClt.Cell(r,10).Value = itemCliente.strMotivoCambioFecha;
                wsClt.Cell(r,11).Value = itemCliente.dateCaptura; 
                wsClt.Cell(r,12).Value = itemCliente.dateCompromiso; 
                wsClt.Cell(r,13).Value = itemCliente.dateConfirmacion;               
                wsClt.Cell(r,14).Value = itemCliente.intTiempoConfirmacion;
                wsClt.Cell(r,15).Value = itemCliente.intUnidades_Cliente; 
                wsClt.Cell(r,16).Value = itemCliente.dcmTotal_Cliente;
                wsClt.Cell(r,16).Style.NumberFormat.Format = "$#,##0.00";
                r++;
            }
            
            // Aplicar bordes a toda la tabla (encabezado + datos)
            if (lstOrdenCliente.Count > 0)
            {
                wsClt.Range(headerRow, 1, r - 1, headersCliente.Length).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsClt.Range(headerRow, 1, r - 1, headersCliente.Length).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            }

            wsClt.Columns().AdjustToContents();
            
            // Construir hoja de Productos
            r = 1;
            
            // Título del reporte en negritas y tamaño 16, combinado de A1 a L1
            wsPrd.Cell(r, 1).Value = "Reporte Monitoreo Orden Servicio - Detalle Productos";
            wsPrd.Range(r, 1, r, 18).Merge(); // Combinar celdas A1 a R1
            wsPrd.Cell(r, 1).Style.Font.Bold = true;
            wsPrd.Cell(r, 1).Style.Font.FontSize = 16;
            wsPrd.Cell(r, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            r++;
            
            // Fecha creación, combinar A2 y B2
            wsPrd.Cell(r, 1).Value = "Fecha creación: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            wsPrd.Range(r, 1, r, 2).Merge(); // Combinar celdas A2 y B2
            r++;
            
            wsPrd.Cell(r++, 1).Value = "Filtros:";
            
            // Filtros con valores en negritas, descripción alineada a la derecha
            wsPrd.Cell(r, 1).Value = "Estatus:"; 
            wsPrd.Cell(r, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            wsPrd.Cell(r, 2).Value = filtros.strEstatus;
            wsPrd.Cell(r, 2).Style.Font.Bold = true;
            r++;
            
            wsPrd.Cell(r, 1).Value = "Fecha Inicial:"; 
            wsPrd.Cell(r, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            wsPrd.Cell(r, 2).Value = filtros.dateFechaInicial;
            wsPrd.Cell(r, 2).Style.Font.Bold = true;
            r++;
            
            wsPrd.Cell(r, 1).Value = "Fecha Final:"; 
            wsPrd.Cell(r, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            wsPrd.Cell(r, 2).Value = filtros.dateFechaFinal;
            wsPrd.Cell(r, 2).Style.Font.Bold = true;
            r++;
            
            wsPrd.Cell(r, 1).Value = "Orden Servicio Inicial:"; 
            wsPrd.Cell(r, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            wsPrd.Cell(r, 2).Value = filtros.intIdOrdenServicioInicial == 0 ? "" : filtros.intIdOrdenServicioInicial.ToString();
            wsPrd.Cell(r, 2).Style.Font.Bold = true;
            r++;
            
            wsPrd.Cell(r, 1).Value = "Orden Servicio Final:"; 
            wsPrd.Cell(r, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            wsPrd.Cell(r, 2).Value = filtros.intIdOrdenServicioFinal == 0 ? "" : filtros.intIdOrdenServicioFinal.ToString();
            wsPrd.Cell(r, 2).Style.Font.Bold = true;
            r++;
            
            r++; // Fila en blanco
            headerRow = r;
            string[] headersProducto = {"Id cliente","Cliente","Orden de servicio","Estatus","Tipo confirmación","Matriz cuenta nacional","Usuario Creador","Usuario Asignado","Motivo Incompleto","Motivo Cambio Fecha Compromiso", "Fecha captura", "Fecha compromiso", "Fecha confirmacion", "Tiempo respuesta","Código producto","Producto","Unidades","Monto"};
            
            // Encabezados con fondo azul claro
            for (int c = 0; c < headersProducto.Length; c++)
            {
                wsPrd.Cell(r, c + 1).Value = headersProducto[c];
                wsPrd.Cell(r, c + 1).Style.Fill.BackgroundColor = XLColor.LightBlue;
                wsPrd.Cell(r, c + 1).Style.Font.Bold = true;
            }
            
            // Activar autofiltro en la fila de encabezados
            wsPrd.Range(r, 1, r, headersProducto.Length).SetAutoFilter();
            
            r++;
            firstDataRow = r;
            
            foreach (var itemProducto in lstOrdenProductos)
            {
                wsPrd.Cell(r, 1).Value = itemProducto.intIdCte;
                wsPrd.Cell(r, 2).Value = itemProducto.strCliente;
                wsPrd.Cell(r, 3).Value = itemProducto.intIdSrv;
                wsPrd.Cell(r, 4).Value = itemProducto.strEstatus;
                wsPrd.Cell(r, 5).Value = itemProducto.strTipoConfirmacion;
                wsPrd.Cell(r, 6).Value = itemProducto.strMatriz;
                wsPrd.Cell(r, 7).Value = itemProducto.strUsuarioCreador;
                wsPrd.Cell(r, 8).Value = itemProducto.strUsuarioAsignado;
                wsPrd.Cell(r, 9).Value = itemProducto.strMotivoIncompleto;
                wsPrd.Cell(r, 10).Value = itemProducto.strMotivoCambioFecha;
                wsPrd.Cell(r, 11).Value = itemProducto.dateCaptura;
                wsPrd.Cell(r, 12).Value = itemProducto.dateCompromiso;
                wsPrd.Cell(r, 13).Value = itemProducto.dateConfirmacion;
                wsPrd.Cell(r, 14).Value = itemProducto.intTiempoConfirmacion;
                wsPrd.Cell(r, 15).Value = itemProducto.intIdPrd;
                wsPrd.Cell(r, 16).Value = itemProducto.strPrdDescripcion;
                wsPrd.Cell(r, 17).Value = itemProducto.intUnidades_Producto;
                wsPrd.Cell(r, 18).Value = itemProducto.dcmTotal_Producto;
                wsPrd.Cell(r, 18).Style.NumberFormat.Format = "$#,##0.00";
                r++;
            }
            
            // Aplicar bordes a toda la tabla (encabezado + datos)
            if (lstOrdenProductos.Count > 0)
            {
                wsPrd.Range(headerRow, 1, r - 1, headersProducto.Length).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsPrd.Range(headerRow, 1, r - 1, headersProducto.Length).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            }
            
            wsPrd.Columns().AdjustToContents();
            
            return wb;
        }

        /// <summary>
        /// Consulta un producto por su ID desde la base de datos
        /// </summary>
        /// <param name="intIdProducto">ID del producto a consultar</param>
        /// <returns>Datos del producto o mensaje de error</returns>
        [HttpGet]
        public HttpResponseMessage ConsultarIdProducto(long intIdProducto)
        {
            try
            {
                // Validar sesión
                var errorSesion = ValidarSesion();
                if (errorSesion != null)
                {
                    return errorSesion;
                }
                string strMensaje = string.Empty;
                entOrdenServicioProductos producto = new entOrdenServicioProductos();

                new CN_OrdenServicio_Admin().ConsultaProducto(
                   SesionActual.Emp_Cnx,
                   SesionActual.Id_Emp,
                   SesionActual.Id_Cd, intIdProducto, ref producto, ref strMensaje
               );

                // Consultar producto usando el método existente
               

                // Verificar si se encontró el producto
                if (producto.Id_Prd > 0)
                {
                    return RespuestaExitosa(new
                    {
                        Id = producto.Id_Prd,
                        Descripcion = producto.Descripcion,
                        Costo = producto.Costo,
                        TipoProducto = producto.TipoProducto
                    });
                }
                else
                {
                    return RespuestaExitosa(new { mensaje = strMensaje });
                }
            }
            catch (Exception ex)
            {
                return RespuestaError("Error al consultar el producto: " + ex.Message);
            }
        }

        /// <summary>
        /// Consulta representantes de servicio por cliente y territorio
        /// </summary>
        /// <param name="intIdCte">ID del cliente</param>
        /// <param name="intIdTer">ID del territorio</param>
        /// <returns>Lista de representantes de servicio</returns>
        [HttpGet]
        public HttpResponseMessage ConsultarRepresentantesServicio(int intIdCte, int intIdTer)
        {
            try
            {
                // Validar sesión
                var errorSesion = ValidarSesion();
                if (errorSesion != null)
                {
                    return errorSesion;
                }

                List<Comun> lstRepresentantes = new List<Comun>();
                new CN_OrdenServicio_Admin().ConsultarRepresentantesServicio(
                    SesionActual.Emp_Cnx,
                    SesionActual.Id_Emp,
                    SesionActual.Id_Cd,
                    intIdCte,
                    intIdTer,
                    ref lstRepresentantes
                );

                return RespuestaExitosa(lstRepresentantes);
            }
            catch (Exception ex)
            {
                return RespuestaError("Error al consultar representantes de servicio: " + ex.Message);
            }
        }

        #endregion
    }
}
