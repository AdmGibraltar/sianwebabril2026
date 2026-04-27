using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaDatos;
using CapaEntidad;
using CapaNegocios;
using System.Net.Http;
using SIANWEB.Core.Web.API;
using CapaModelo;
using System.Net;

namespace SIANWEB.WebService.GPM
{
    public class GestionPreciosController
        : BaseWebAPIController
    {

        //JFCV
        public class ProductoValidacion
        {
            public int Id_Emp { get; set; }
            public int Id_Cd { get; set; }
            public int Id_Cte { get; set; }
            public Int64 Id_Prd { get; set; }
            public int Id_Ter { get; set; }
            public double PrecioNegociadoProy { get; set; }
            public double UnidadesProyectadas { get; set; }
            public string NomProducto { get; set; }
            public string NomComercial { get; set; }
            public double PrecioObjetivoProy { get; set; }
            public string Comentarios { get; set; }
            public int Id_Rik { get; set; }
            public string NomEstatus { get; set; }
            public int id_ReporteGp { get; set; }
        }

        public class ProductosValidacionRequest
        {
            public List<ProductoValidacion> productos { get; set; }
        }

        /// <summary>
        /// Obtiene la entrada del producto con Id_Prd igual a id.
        /// </summary>
        /// <param name="id">Identificador del producto</param>
        /// <returns>CatProducto</returns>
        [HttpGet]
        public HttpResponseMessage Get(Int64 id)
        {
            CatProducto catProducto = null;
            try
            {
                using (IBusinessTransaction ibt = CN_FabricaTransaccionNegocios.Default(Sesion))
                {
                    ibt.Begin();
                    CN_CatProducto cnCatProducto = new CN_CatProducto();
                    catProducto = cnCatProducto.ObtenerPorId(Sesion, id, ibt);
                    return Request.CreateResponse(System.Net.HttpStatusCode.OK, catProducto);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("Se ha producido un error al obtener el producto {0}", id), ex);
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, string.Format("Se ha producido un error al obtener el producto {0}", id), ex);
            }
        }

        [HttpGet]

        public eResponse<Producto> Get(int Id_Cte, Int64 Id_Prd)
        {
            eResponse<Producto> result = new eResponse<Producto>();
            result.Estado = 0;
            result.Mensaje = "";
            result.Datos = null;

            Producto P = new Producto();
            try
            {
                CN_CatProducto cnProducto = new CN_CatProducto();
                P.Id_Cte = Id_Cte;
                cnProducto.Consulta_Producto(ref P, Sesion.Emp_Cnx, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Id_Cd_Ver, Id_Prd, 0);
                result.Datos = P;
                result.Estado = 1;
            }
            catch (Exception ex)
            {
                result.Estado = -1;
                result.Datos = null;
                result.Mensaje = ex.Message.ToString();
            }
            return result;
        }

        public eResponse<GestionIncrementoPrecios> Get(int Id_Cte, Int64 Id_Prd, string Id_Tamaño, int id_rik)
        {
            eResponse<GestionIncrementoPrecios> result = new eResponse<GestionIncrementoPrecios>();
            result.Estado = 0;
            result.Mensaje = "";
            result.Datos = null;


            Producto P = new Producto();
            GestionIncrementoPrecios GPMA = new GestionIncrementoPrecios();
            try
            {
                CN_CatProducto cnProducto = new CN_CatProducto();
                P.Id_Cte = Id_Cte;
                cnProducto.Consulta_ProductoPrecioObjetivo(ref P, ref GPMA, Sesion.Emp_Cnx, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Id_Cd_Ver, Id_Prd, 0, Id_Cte, Id_Tamaño, id_rik);
                result.Datos = GPMA;
                result.Estado = 1;
            }
            catch (Exception ex)
            {
                result.Estado = -1;
                result.Datos = null;
                result.Mensaje = ex.Message.ToString();
            }
            return result;
        }

        //JFCV alertadeprecios validar si el producto requiere autorización 
        [HttpGet]
        public eResponse<AlertaAutorizacion> AlertaPrecioValidaPrecioGPMA(int Id_Emp, int Id_Cd, int Id_Cte, Int64 Id_Prd, double Precio_Vta, int Id_Rik, int Id_Ter, int NR, int cantidad, string prd_descripcion, string cte_nomcomercial, double precioObjetivoProy, string Comentarios)
        {
            eResponse<AlertaAutorizacion> result = new eResponse<AlertaAutorizacion>();
            result.Estado = 0;
            result.Mensaje = "";
            result.Datos = null;
            // Id_Emp = Sesion.Id_Emp;
            //acys.Id_Cd = Sesion.Id_Cd_Ver;
            try
            {

                #region validar precios que requieran autorización

                //JFCV Validar si tiene precios inferiores a los precios minimos
                AlertaAutorizacion alertaaut;
                AlertaAutorizacion alertaautdet;
                CN_AlertaAutorizacion cn_alertaautorizacion;

                List<string> ProductosAlerta = new List<string>();
                //descomentarice esta para guardar en una variable de sesion los productos que no pasaron la validación
                List<AlertaAutorizacion> lalertasautdet = new List<AlertaAutorizacion>();
                AlertaAutorizacion pasoalertaautdet = new AlertaAutorizacion();

                //JFCV 11 agosto del 2022 
                //leer configuración para ver si se valida o no el precio 
                // configuración 956 conf de ACYS  
                // si el valor es 0 no valida y si es 1 si va a validar 
                int GLOBAL_ValidaPrecioMinimoRik = 0;
                CapaEntidad.eSysConfiguracion SysC = new CapaEntidad.eSysConfiguracion();
                try
                {
                    CN_SysConfigruacion CN = new CN_SysConfigruacion();
                    SysC = CN.spSysConfiguracionById(Id_Emp, Sesion.Id_Cd, 956, Sesion.Emp_Cnx);
                    int iTmp = 0;
                    int.TryParse(SysC.Conf_Valor, out iTmp);
                    GLOBAL_ValidaPrecioMinimoRik = iTmp;
                }
                catch (Exception ex)
                {
                    GLOBAL_ValidaPrecioMinimoRik = 0;
                }

                if (GLOBAL_ValidaPrecioMinimoRik == 1)
                {
                    #region validar precios que requieran autorización
                    if (NR == 0)
                    {
                        System.Web.HttpContext.Current.Session["lAurizacionPrecios" + HttpContext.Current.Session.SessionID] = lalertasautdet;
                        System.Web.HttpContext.Current.Session["ProdsAutorizacion" + HttpContext.Current.Session.SessionID] = ProductosAlerta;
                        System.Web.HttpContext.Current.Session["Id_FacPrec" + HttpContext.Current.Session.SessionID] = "99999";
                    }

                    alertaaut = new AlertaAutorizacion();
                    alertaautdet = new AlertaAutorizacion();
                    cn_alertaautorizacion = new CN_AlertaAutorizacion();
                    alertaaut.Id_Emp = Sesion.Id_Emp;
                    alertaaut.Id_Cd = Sesion.Id_Cd_Ver;
                    alertaaut.Id_Cte = Id_Cte;
                    alertaaut.Id_Prd = Id_Prd;
                    alertaaut.IdRepresentante = Sesion.Id_Rik;
                    alertaaut.Id_Ter = Id_Ter;
                    double Preciodefactura = Precio_Vta;
                    alertaaut.Precio_Vta = Preciodefactura;
                    cn_alertaautorizacion.AlertaPrecioConsultaPrecioGPMA(alertaaut, ref alertaautdet, Sesion.Emp_Cnx);

                    pasoalertaautdet = new AlertaAutorizacion();
                    //si no recibe registros es que ya se cuenta con una autorización de este precio 
                    if (alertaautdet.Id_Prd == 0)
                    {
                        result.Datos = null;
                        result.Estado = 1;
                    }
                    else
                    {
                        if (alertaautdet != null && alertaautdet.Precio_MinimoRik > 0)
                        {
                            //PO que sea el que recibe de parametro
                            alertaautdet.PrecioObjetivo = precioObjetivoProy;

                            if (Math.Round(Preciodefactura, 3) < alertaautdet.PrecioObjetivo)
                            {

                                pasoalertaautdet.Id_Emp = Sesion.Id_Emp;
                                pasoalertaautdet.Id_Cd = Sesion.Id_Cd_Ver;
                                pasoalertaautdet.Id_Cte = Id_Cte;
                                pasoalertaautdet.IdRepresentante = Sesion.Id_Rik;
                                pasoalertaautdet.Id_Ter = Id_Ter;
                                pasoalertaautdet.IdAutorizacionAnterior = alertaautdet.IdAutorizacionAnterior;
                                pasoalertaautdet.Id_Prd = alertaautdet.Id_Prd;
                                pasoalertaautdet.Precio_MinimoRik = Math.Round(Convert.ToDouble(alertaautdet.Precio_MinimoRik), 3);
                                pasoalertaautdet.Precio_MinimoGte = Math.Round(Convert.ToDouble(alertaautdet.Precio_MinimoGte), 3);
                                pasoalertaautdet.Precio_Vta = Preciodefactura;
                                pasoalertaautdet.PrecioLista = alertaautdet.PrecioLista;
                                pasoalertaautdet.Cte_NomComercial = cte_nomcomercial; // alertaaut.Cte_NomComercial;
                                pasoalertaautdet.Prd_Descripcion = prd_descripcion; // alertaautdet.Prd_Descripcion;
                                pasoalertaautdet.PrecioLista = alertaautdet.PrecioLista;
                                pasoalertaautdet.Cantidad = cantidad;
                                pasoalertaautdet.Precio_AAA = alertaautdet.Precio_AAA;
                                pasoalertaautdet.Utilidad = alertaautdet.Utilidad;
                                pasoalertaautdet.Porc_Utilidad = alertaautdet.Utilidad / Preciodefactura;
                                pasoalertaautdet.Importe = Preciodefactura * pasoalertaautdet.Cantidad;
                                pasoalertaautdet.Importe_Utilidad = pasoalertaautdet.Utilidad * pasoalertaautdet.Cantidad;
                                pasoalertaautdet.Justificacion = Comentarios;

                                pasoalertaautdet.TipoAutorizacion = 6; //GPMA
                                pasoalertaautdet.FechaVigencia = DateTime.Now.AddMonths(12);
                                pasoalertaautdet.Id_Cpr = alertaautdet.Id_Cpr;
                                pasoalertaautdet.JustificacionMemo = Comentarios;

                                pasoalertaautdet.Id_Tamaño = alertaautdet.Id_Tamaño;
                                pasoalertaautdet.PrecioObjetivo = alertaautdet.PrecioObjetivo;
                                //JFCV 21mayo precio objetivo que sea el que recibio de parametro
                                pasoalertaautdet.PrecioObjetivo = precioObjetivoProy;

                                lalertasautdet = (List<AlertaAutorizacion>)System.Web.HttpContext.Current.Session["lAurizacionPrecios" + HttpContext.Current.Session.SessionID];
                                ProductosAlerta = (List<string>)System.Web.HttpContext.Current.Session["ProdsAutorizacion" + HttpContext.Current.Session.SessionID];
                                if (lalertasautdet == null)
                                {
                                    lalertasautdet = new List<AlertaAutorizacion>();
                                    ProductosAlerta = new List<String>();
                                }
                                else
                                {
                                    if (lalertasautdet.Count >= NR)
                                    {
                                        lalertasautdet = new List<AlertaAutorizacion>();
                                        ProductosAlerta = new List<String>();
                                    }
                                }

                                lalertasautdet.Add(pasoalertaautdet);

                                System.Web.HttpContext.Current.Session["lAurizacionPrecios" + HttpContext.Current.Session.SessionID] = lalertasautdet;
                                ProductosAlerta.Add(alertaautdet.Id_Prd.ToString().Trim());
                                System.Web.HttpContext.Current.Session["ProdsAutorizacion" + HttpContext.Current.Session.SessionID] = ProductosAlerta;
                                System.Web.HttpContext.Current.Session["Id_FacPrec" + HttpContext.Current.Session.SessionID] = "99999";

                                result.Datos = pasoalertaautdet;
                                result.Estado = 1;

                            }
                            else
                            {

                                result.Datos = null;
                                result.Estado = 1;
                            }

                        }
                        else
                        {
                            result.Datos = null;
                            result.Estado = 1;
                        }


                    }
                    #endregion
                }
                else
                {
                    result.Datos = null;
                    result.Estado = 1;
                }

                #endregion validar precios que requieran autorización
            }
            catch (Exception ex)
            {
                result.Estado = -1;
                result.Datos = null;
                result.Mensaje = ex.Message.ToString();
            }
            return result;
        }

        [HttpPost]
        public HttpResponseMessage AlertaPrecioValidaPrecioGPMA_Masivo([FromBody] ProductosValidacionRequest request)
        {
            var resultados = new List<AlertaAutorizacion>();


            var productos = request?.productos;
            if (productos == null)
                return Request.CreateResponse(HttpStatusCode.OK, resultados);


            #region validar precios que requieran autorización

            //JFCV Validar si tiene precios inferiores a los precios minimos
            AlertaAutorizacion alertaaut;
            AlertaAutorizacion alertaautdet;
            CN_AlertaAutorizacion cn_alertaautorizacion;

            List<string> ProductosAlerta = new List<string>();
            //descomentarice esta para guardar en una variable de sesion los productos que no pasaron la validación
            List<AlertaAutorizacion> lalertasautdet = new List<AlertaAutorizacion>();
            AlertaAutorizacion pasoalertaautdet = new AlertaAutorizacion();

            //JFCV 11 agosto del 2022 
            //leer configuración para ver si se valida o no el precio 
            // configuración 956 conf de ACYS  
            // si el valor es 0 no valida y si es 1 si va a validar 
            int GLOBAL_ValidaPrecioMinimoRik = 0;
            CapaEntidad.eSysConfiguracion SysC = new CapaEntidad.eSysConfiguracion();
            try
            {
                CN_SysConfigruacion CN = new CN_SysConfigruacion();
                SysC = CN.spSysConfiguracionById(Sesion.Id_Emp, Sesion.Id_Cd, 958, Sesion.Emp_Cnx);
                int iTmp = 0;
                int.TryParse(SysC.Conf_Valor, out iTmp);
                GLOBAL_ValidaPrecioMinimoRik = iTmp;
            }
            catch (Exception ex)
            {
                GLOBAL_ValidaPrecioMinimoRik = 0;
            }


            System.Web.HttpContext.Current.Session["lAurizacionPrecios" + HttpContext.Current.Session.SessionID] = lalertasautdet;
            System.Web.HttpContext.Current.Session["ProdsAutorizacion" + HttpContext.Current.Session.SessionID] = ProductosAlerta;
            System.Web.HttpContext.Current.Session["Id_FacPrec" + HttpContext.Current.Session.SessionID] = "99999";
            int NR = 1500;

            eResponse<AlertaAutorizacion> result = new eResponse<AlertaAutorizacion>();
            result.Estado = 0;
            result.Mensaje = "";
            result.Datos = null;

            foreach (var prod in productos)
            {
                // Id_Emp = Sesion.Id_Emp;
                //acys.Id_Cd = Sesion.Id_Cd_Ver;
                try
                {


                    if (GLOBAL_ValidaPrecioMinimoRik == 1)
                    {
                        #region validar precios que requieran autorización
                        //if (NR == 0)
                        //{
                        //    System.Web.HttpContext.Current.Session["lAurizacionPrecios" + HttpContext.Current.Session.SessionID] = lalertasautdet;
                        //    System.Web.HttpContext.Current.Session["ProdsAutorizacion" + HttpContext.Current.Session.SessionID] = ProductosAlerta;
                        //    System.Web.HttpContext.Current.Session["Id_FacPrec" + HttpContext.Current.Session.SessionID] = "99999";
                        //}

                        alertaaut = new AlertaAutorizacion();
                        alertaautdet = new AlertaAutorizacion();
                        cn_alertaautorizacion = new CN_AlertaAutorizacion();
                        alertaaut.Id_Emp = Sesion.Id_Emp;
                        alertaaut.Id_Cd = Sesion.Id_Cd_Ver;
                        alertaaut.Id_Cte = prod.Id_Cte;
                        alertaaut.Id_Prd = prod.Id_Prd;
                        alertaaut.IdRepresentante = Sesion.Id_Rik;
                        alertaaut.IdRepresentante = prod.Id_Rik;
                        alertaaut.Id_Ter = prod.Id_Ter;
                        double Preciodefactura = prod.PrecioNegociadoProy;
                        alertaaut.Precio_Vta = Preciodefactura;
                        cn_alertaautorizacion.AlertaPrecioConsultaPrecioGPMA(alertaaut, ref alertaautdet, Sesion.Emp_Cnx);

                        pasoalertaautdet = new AlertaAutorizacion();
                        //si no recibe registros es que ya se cuenta con una autorización de este precio 
                        if (alertaautdet.Id_Prd == 0)
                        {
                            result.Datos = null;
                            result.Estado = 1;
                        }
                        else
                        {
                            if (alertaautdet != null && alertaautdet.Precio_MinimoRik > 0)
                            {
                                //PO que sea el que recibe de parametro
                                alertaautdet.PrecioObjetivo = prod.PrecioObjetivoProy;

                                if (Math.Round(Preciodefactura, 3) < alertaautdet.PrecioObjetivo)
                                {
                                    pasoalertaautdet.Id_Emp = Sesion.Id_Emp;
                                    pasoalertaautdet.Id_Cd = Sesion.Id_Cd_Ver;
                                    pasoalertaautdet.Id_Cte = prod.Id_Cte;
                                    pasoalertaautdet.IdRepresentante = prod.Id_Rik;
                                    pasoalertaautdet.Id_Ter = prod.Id_Ter;
                                    pasoalertaautdet.IdAutorizacionAnterior = alertaautdet.IdAutorizacionAnterior;
                                    pasoalertaautdet.Id_Prd = alertaautdet.Id_Prd;
                                    pasoalertaautdet.Precio_MinimoRik = Math.Round(Convert.ToDouble(alertaautdet.Precio_MinimoRik), 3);
                                    pasoalertaautdet.Precio_MinimoGte = Math.Round(Convert.ToDouble(alertaautdet.Precio_MinimoGte), 3);
                                    pasoalertaautdet.Precio_Vta = Preciodefactura;
                                    pasoalertaautdet.PrecioLista = alertaautdet.PrecioLista;
                                    pasoalertaautdet.Cte_NomComercial = prod.NomComercial; // alertaaut.Cte_NomComercial;
                                    pasoalertaautdet.Prd_Descripcion = prod.NomProducto; // alertaautdet.Prd_Descripcion;
                                    pasoalertaautdet.PrecioLista = alertaautdet.PrecioLista;
                                    pasoalertaautdet.Cantidad = prod.UnidadesProyectadas;
                                    pasoalertaautdet.Precio_AAA = alertaautdet.Precio_AAA;
                                    pasoalertaautdet.Utilidad = alertaautdet.Utilidad;
                                    pasoalertaautdet.Porc_Utilidad = alertaautdet.Utilidad / Preciodefactura;
                                    pasoalertaautdet.Importe = Preciodefactura * pasoalertaautdet.Cantidad;
                                    pasoalertaautdet.Importe_Utilidad = pasoalertaautdet.Utilidad * pasoalertaautdet.Cantidad;
                                    pasoalertaautdet.Justificacion = "";

                                    pasoalertaautdet.TipoAutorizacion = 6; //GPMA
                                    pasoalertaautdet.FechaVigencia = DateTime.Now.AddMonths(12);
                                    pasoalertaautdet.Id_Cpr = alertaautdet.Id_Cpr;
                                    pasoalertaautdet.JustificacionMemo = prod.Comentarios;

                                    pasoalertaautdet.Id_Tamaño = alertaautdet.Id_Tamaño;
                                    pasoalertaautdet.PrecioObjetivo = alertaautdet.PrecioObjetivo;
                                    //JFCV 21mayo precio objetivo que sea el que recibio de parametro
                                    pasoalertaautdet.PrecioObjetivo = prod.PrecioObjetivoProy;

                                    lalertasautdet = (List<AlertaAutorizacion>)System.Web.HttpContext.Current.Session["lAurizacionPrecios" + HttpContext.Current.Session.SessionID];
                                    ProductosAlerta = (List<string>)System.Web.HttpContext.Current.Session["ProdsAutorizacion" + HttpContext.Current.Session.SessionID];
                                    if (lalertasautdet == null)
                                    {
                                        lalertasautdet = new List<AlertaAutorizacion>();
                                        ProductosAlerta = new List<String>();
                                    }
                                    else
                                    {
                                        if (lalertasautdet.Count >= NR)
                                        {
                                            lalertasautdet = new List<AlertaAutorizacion>();
                                            ProductosAlerta = new List<String>();
                                        }
                                    }

                                    lalertasautdet.Add(pasoalertaautdet);

                                    System.Web.HttpContext.Current.Session["lAurizacionPrecios" + HttpContext.Current.Session.SessionID] = lalertasautdet;
                                    ProductosAlerta.Add(alertaautdet.Id_Prd.ToString().Trim());
                                    System.Web.HttpContext.Current.Session["ProdsAutorizacion" + HttpContext.Current.Session.SessionID] = ProductosAlerta;
                                    System.Web.HttpContext.Current.Session["Id_FacPrec" + HttpContext.Current.Session.SessionID] = prod.id_ReporteGp;

                                    result.Datos = pasoalertaautdet;
                                    result.Estado = 1;

                                }
                                else
                                {

                                    result.Datos = null;
                                    result.Estado = 1;
                                }

                            }
                            else
                            {
                                result.Datos = null;
                                result.Estado = 1;
                            }


                        }
                        #endregion
                    }
                    else
                    {
                        result.Datos = null;
                        result.Estado = 1;
                    }

                    #endregion validar precios que requieran autorización
                }
                catch (Exception ex)
                {
                    result.Estado = -1;
                    result.Datos = null;
                    result.Mensaje = ex.Message.ToString();
                }

            }
            // Agrega el resultado si hay datos
            if (result.Datos != null)
            {
                resultados.Add(result.Datos);
            }

            return Request.CreateResponse(HttpStatusCode.OK, resultados);
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

        //
    }
}