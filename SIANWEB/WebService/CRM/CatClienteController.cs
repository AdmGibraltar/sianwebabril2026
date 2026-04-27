using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaEntidad;
using CapaNegocios;
using System.Net.Http;
using System.Data.SqlClient;
using System.Configuration;

// 24 Sep 2018 RFH

namespace SIANWEB.WebService
{
    public class CatClienteController : ApiController
    {
        /// <summary>
        /// Operación de cosulta de clientes mediante un término de búsqueda para los elementos de listado con opción de búsqueda
        /// </summary>
        /// <param name="terminoDeBusqueda">String. Término de búsqueda.</param>
        /// <returns>List<Clientes> - Clientes coincidentes con el término de búsqueda.</returns>
        [HttpGet]
        public HttpResponseMessage Get(string terminoDeBusqueda, int idTer)
        {
            try
            {
                CN_CatCliente cnCatCte = new CN_CatCliente();
                Clientes pars = new Clientes();
                List<Clientes> result = new List<Clientes>();

                pars.Id_Emp = Sesion.Id_Emp;
                pars.Id_Cd = Sesion.Id_Cd;
                pars.Id_Rik = Sesion.Id_Rik;
                pars.Cte_NomComercial = terminoDeBusqueda;
                pars.Id_Terr = idTer;
                //¿Pasar la UEN como parámetro?

                cnCatCte.Lista(pars, Sesion.Emp_Cnx, ref result);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                //Manejar los casos adecuadamente
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// Obtiene el conjunto de clientes en los cuales el valor rfc coincide con el valor del campo Cte_Rfc
        /// </summary>
        /// <param name="rfc">Valor del RFC a buscar</param>
        /// <returns>Colección de clientes coincidenes con el valor del campo Cte_Rfc y el valor rfc</returns>
        [HttpGet]
        public HttpResponseMessage Get(string rfc)
        {
            try
            {
                CN_CatCliente cnCatCliente = new CN_CatCliente();
                //var clientes=cnCatCliente.ObtenerPorRFC(Sesion, rfc);
                var clientes = cnCatCliente.Obtener_PorRFC(Sesion, rfc);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, clientes);
            }
            catch (Exception ex) //manejo de error genérico
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }


        // Identifica si el IDCte esta en los prospectos 

        [HttpGet]
        public HttpResponseMessage Get(int IdCte)
        {
            try
            {
                int Id_Emp = Sesion.Id_Emp;
                int Id_Cd = Sesion.Id_Cd;
                int Id_Rik = Sesion.Id_Rik;
                int Id_Cte = IdCte;

                CN_Prospecto CNp = new CN_Prospecto();
                int IdCrmProspecto = CNp.Consultar(Id_Emp, Id_Cd, Id_Rik, Id_Cte, Sesion.Emp_Cnx);

                return Request.CreateResponse(System.Net.HttpStatusCode.OK, IdCrmProspecto);

            }
            catch (Exception ex) //manejo de error genérico
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }


        [HttpGet]
        public HttpResponseMessage Get(string nombreEmpresa, string sinUsar)
        {
            try
            {
                CN_CatCliente cnCatCliente = new CN_CatCliente();
                var clientes = cnCatCliente.ObtenerPorNombreComercial(Sesion, nombreEmpresa);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, clientes);
            }
            catch (Exception ex) //manejo de error genérico
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }

        // Busqueda de Cliente
        // Solo los de Rik Actual 
        // FEB28-2020 FRH 

        [HttpGet]
        public eResponse<List<eClienteBuscar>> BuscarPorRik(string TextoBuscar, int Id_Rik)
        {
            eResponse<List<eClienteBuscar>> result = new eResponse<List<eClienteBuscar>>();
            result.Estado = 0;

            List<eClienteBuscar> lst = new List<eClienteBuscar>();
            try
            {

                CN_CatCliente CC = new CN_CatCliente();
                lst = CC.ListarBusqueda_PorRik(Sesion.Id_Emp, Sesion.Id_Cd, 0, 0, 0, Sesion.Id_Rik, TextoBuscar, Sesion.Emp_Cnx);

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


        // Busqueda de Cliente
        // Regresa un listado 
        // 6 Sep 2018

        [HttpGet]
        public eResponse<List<eClienteBuscar>> Buscar(string TextoBuscar)
        {
            eResponse<List<eClienteBuscar>> result = new eResponse<List<eClienteBuscar>>();
            result.Estado = 0;

            List<eClienteBuscar> lst = new List<eClienteBuscar>();
            try
            {

                CN_CatCliente CC = new CN_CatCliente();
                lst = CC.ListarBusqueda(Sesion.Id_Emp, Sesion.Id_Cd, 0, 0, 0, Sesion.Id_Rik, TextoBuscar, Sesion.Emp_Cnx);

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

        // 24SEP-2021 RFH
        // Clientes por Rik CRM
        [HttpGet]
        public eResponse<List<eClienteBuscar>> spCrmInt_Clientes(int Id_Emp, int Id_Cd, int Id_Rik)
        {
            eResponse<List<eClienteBuscar>> result = new eResponse<List<eClienteBuscar>>();
            result.Estado = 0;

            List<eClienteBuscar> lst = new List<eClienteBuscar>();
            try
            {
                CN_CatCliente CC = new CN_CatCliente();
                lst = CC.spCrmInt_Clientes(Sesion.Id_Emp, Sesion.Id_Cd, Id_Rik, Sesion.Emp_Cnx);
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

        // Buscar Cliente por Id
        // 8 Sep 2018 RFH
        [HttpGet]
        public eResponse<eCliente> Buscar_CteById(int Id_Cte)
        {
            eCliente C = new eCliente();
            eResponse<eCliente> result = new eResponse<eCliente>();
            result.Estado = 0;
            try
            {
                CN_CatCliente CN = new CN_CatCliente();
                C = CN.Consultar_PorId_Cte(Sesion.Id_Emp, Sesion.Id_Cd, Id_Cte, Sesion.Emp_Cnx);

                if (C == null)
                {
                    result.Estado = -1;
                }
                else
                {
                    result.Estado = 1;
                }
                result.Datos = C;
            }
            catch (Exception ex)
            {
                result.Estado = 0;
                result.Datos = null;
            }

            return result;
        }

        // Buscar Cliente por Id
        // 10 Oct 2018 RFH
        // Metodo utilizado por ACyS 1
        [HttpGet]
        public eResponse<Clientes> Buscar_CteById_ACyS(int Id_Cte, int dVar1)
        {
            Clientes C = new Clientes();
            eResponse<Clientes> result = new eResponse<Clientes>();
            result.Estado = 0;
            result.Mensaje = "";
            try
            {
                CN_CatCliente CN = new CN_CatCliente();
                //C = CN.Consultar_PorId_Cte(Sesion.Id_Emp, Sesion.Id_Cd, Id_Cte, Sesion.Emp_Cnx);                                
                C.Id_Cte = Id_Cte;
                C.Id_Emp = Sesion.Id_Emp;
                C.Id_Cd = Sesion.Id_Cd_Ver;
                C.Id_Rik = Sesion.Id_Rik > 0 ? Sesion.Id_Rik : 0;
                CN.ConsultaClientes(ref C, Sesion.Emp_Cnx);

                result.Estado = 1;
                result.Datos = C;
            }
            catch (Exception ex)
            {
                result.Estado = 0;
                result.Mensaje = ex.Message.ToString();
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

        [HttpGet]
        public HttpResponseMessage UenYSegmentoConfigurado(int id_cliente)
        {
            try
            {
                bool val = new CN_Prospecto().ValidarUenSegmento(Sesion.Id_Emp, Sesion.Id_Cd, id_cliente, Sesion.Emp_Cnx);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, val);
            }
            catch
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, false);
            }
        }

        public class ReqCliente
        {
            public int IdCliente { get; set; }
            public int Uen { get; set; }
            public int Segmento { get; set; }
            public int TipoCliente { get; set; }
            public string Cte_NomComercial { get; set; }
            public string Cte_Email { get; set; }
        }

        [HttpPost]
        public HttpResponseMessage GuardarDatosGenerales([FromBody] ReqCliente req)
        {
            try
            {
                var cliente = new Clientes
                {
                    Id_Emp = Sesion.Id_Emp,
                    Id_Cd = Sesion.Id_Cd_Ver,
                    Id_UCd = Sesion.Id_Cd,
                    Id_U = Sesion.Id_U,
                    Id_UMod = Sesion.Id_U,
                    Id_Cte = req.IdCliente,
                    Estatus = false,
                    Id_TCte = req.TipoCliente,
                    Id_Uen = req.Uen,
                    Id_Seg = req.Segmento,
                    Cte_NomComercial = req.Cte_NomComercial,
                    Cte_NomCorto = req.Cte_NomComercial,
                    Cte_FacEstado = "",
                    Cte_FacColonia = "",
                    Cte_FacCalle = "",
                    Cte_FacCp = "",
                    Cte_FacMunicipio = "",
                    Cte_FacNumero = "",
                    Cte_FacTel = "",
                    Cte_FacRfc = "",
                    Cte_Calle = "",
                    Cte_Numero = "",
                    Cte_Cp = "",
                    Cte_Colonia = "",
                    Cte_Municipio = "",
                    Cte_Estado = "",
                    Cte_Telefono = "",
                    Cte_Fax = "",
                    Cte_DRfc = "",
                    Cte_Contacto = "",
                    Cte_Email = req.Cte_Email,
                    Cte_RHoraam1 = "",
                    Cte_RHoraam2 = "",
                    Cte_RHorapm1 = "",
                    Cte_RHorapm2 = "",
                    Cte_Referencia = "",
                    Db = (new SqlConnectionStringBuilder(Sesion.Emp_Cnx)).InitialCatalog,
                    Db_Cobranza = (new SqlConnectionStringBuilder(ConfigurationManager.AppSettings["strConnectionCobranza"])).InitialCatalog,
                    FormasPago = new List<FormaPago>()
                };

                int verificador = 0;
                new CN_CatCliente().ModificarClientes(cliente, Sesion.Emp_Cnx, ref verificador, Sesion.VersionTerritorio);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, verificador == 1);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, false);
            }
        }
    }
}