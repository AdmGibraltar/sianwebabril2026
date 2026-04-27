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
using CapaEntidad;

namespace SIANWEB.WebService
{
    public class CatTerritorioController
        : BaseWebAPIController
    {
        // Este metodo reemplaza al de abajo 
        // ya no utiliza EF
        // 26 Jun 2018 RFH
        [HttpGet]
        public Task<HttpResponseMessage> Get(int X, int Y)
        {

            List<eTerritorio> lst = new List<eTerritorio>();

            try
            {
                HttpContext current = HttpContext.Current;
                var t = Task<HttpResponseMessage>.Factory.StartNew(() =>
                {
                    HttpContext.Current = current;
                    try
                    {
                        using (IBusinessTransaction ibt = CN_FabricaTransaccionNegocios.Default(Sesion))
                        {
                            ibt.Begin();
                            CN_CatTerritorios cnCatTerritorios = new CN_CatTerritorios();
                            /*if (Sesion.Id_TU == 3)
                            {
                                lst = cnCatTerritorios.ObtenerTerritorios_PorRik(Sesion.Id_Emp, Sesion.Id_Cd, 869, Sesion);
                            } else { */
                            lst = cnCatTerritorios.ObtenerTerritorios_PorRik(Sesion.Id_Emp, Sesion.Id_Cd, Sesion.Id_Rik, Sesion);
                            // }
                            return Request.CreateResponse(System.Net.HttpStatusCode.OK, lst);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("CatTerritorioController::Get", ex);
                        return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                });
                return t;
            }
            catch (Exception ex)
            {
                return Task<HttpResponseMessage>.Factory.StartNew(() =>
                {
                    return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
                });
            }

        }


        // 1 Ene 2019
        [HttpGet]
        public eResponse<List<eTerritorio>> GetPorRik(int Par1, int Par2)
        {
            eResponse<List<eTerritorio>> result = new eResponse<List<eTerritorio>>();
            List<eTerritorio> lst = new List<eTerritorio>();
            result.Estado = 0;
            try
            {

                CN_CatTerritorios cnCatTerritorios = new CN_CatTerritorios();
                lst = cnCatTerritorios.ObtenerTerritorios_PorRik(Sesion.Id_Emp, Sesion.Id_Cd, Sesion.Id_Rik, Sesion);

                result.Datos = lst;
                result.Estado = 1;

            }
            catch (Exception ex)
            {
                result.Datos = null;
                result.Estado = -1;
            }

            return result;
        }

        // ABR28-2021
        [HttpGet]
        public eResponse<List<eTerritorio>> GetTerritoriosPorRik(int Id_Rik, int Par1, int Par2)
        {
            eResponse<List<eTerritorio>> result = new eResponse<List<eTerritorio>>();
            List<eTerritorio> lst = new List<eTerritorio>();
            result.Estado = 0;
            try
            {
                CN_CatTerritorios cnCatTerritorios = new CN_CatTerritorios();
                lst = cnCatTerritorios.ObtenerTerritorios_PorRik(Sesion.Id_Emp, Sesion.Id_Cd, Id_Rik, Sesion);
                result.Datos = lst;
                result.Estado = 1;
            }
            catch (Exception ex)
            {
                result.Datos = null;
                result.Estado = -1;
            }
            return result;
        }


        // 3MAY2021 RFH - CONSULTA TODOS LOS TERRITORIOS ACTIVOS
        [HttpGet]
        public eResponse<List<eTerritorio>> spGetTerritoriosAll(
            int Id_Rik, int ParTer)
        {
            eResponse<List<eTerritorio>> result = new eResponse<List<eTerritorio>>();
            List<eTerritorio> lst = new List<eTerritorio>();
            result.Estado = 0;
            try
            {
                CN_CatTerritorios cnCatTerritorios = new CN_CatTerritorios();
                lst = cnCatTerritorios.spGetTerritoriosAll(Sesion.Id_Emp, Sesion.Id_Cd, Id_Rik, Sesion);
                result.Datos = lst;
                result.Estado = 1;
            }
            catch (Exception ex)
            {
                result.Datos = null;
                result.Estado = -1;
            }
            return result;
        }

        // NOV26-2019 RFH 

        [HttpGet]
        public eResponse<string> Territorio_Maximo(string Prefix, int TipoRep, int TipoCliente, int IdUen, int IdSeg)
        {
            eResponse<string> result = new eResponse<string>();
            string Maximo = "";
            result.Estado = 0;
            try
            {
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                Maximo = CN_Comun.MaximoTerritorio(
                    Sesion.Id_Emp,
                    Sesion.Id_Cd_Ver, TipoRep, TipoCliente, IdUen, IdSeg,
                    Sesion.Emp_Cnx,
                    Sesion.VersionTerritorio ? "spCatTerritorio_MaximoV2" : "spCatTerritorio_Maximo",
                    Prefix
                );

                result.Datos = Maximo;
                result.Estado = 1;

            }
            catch (Exception ex)
            {
                result.Datos = null;
                result.Estado = -1;
            }

            return result;
        }

        //NOV26-2019 RFH

        [HttpGet]
        public eResponse<int> Territorio_CrearSolicitud(string ClaveTerritorio, string IdRepresentante, string Territorio, int Activo)
        {

            CapaDatos.CD_CatTerritorios cd_Territorios = new CapaDatos.CD_CatTerritorios();

            eResponse<int> result = new eResponse<int>();
            string Maximo = "";
            result.Estado = 0;
            try
            {
                if (Territorio == null)
                {
                    Territorio = "&nbsp";
                }

                int Respuesta = 0;
                CapaEntidad.ModelAutorizacionTerritorios NuevaSolicitud = new CapaEntidad.ModelAutorizacionTerritorios();
                NuevaSolicitud.IdUSolicita = Sesion.Id_U;
                NuevaSolicitud.ClaveTerritorio = long.Parse(ClaveTerritorio);
                NuevaSolicitud.IdRepresentante = int.Parse(IdRepresentante);
                NuevaSolicitud.Territorio = Territorio;
                if (Activo == 1)
                {
                    NuevaSolicitud.Activo = true;
                }
                else
                {
                    NuevaSolicitud.Activo = false;
                }
                NuevaSolicitud.IdAutorizacion = 0;

                cd_Territorios.GuardarAutorizacionTerritorios(NuevaSolicitud, ref Respuesta, Sesion.Emp_Cnx);

                result.Datos = Respuesta;
                result.Estado = 1;

            }
            catch (Exception ex)
            {
                result.Datos = 0;
                result.Estado = -1;
            }
            return result;
        }

        //DIC5-2019 RFH

        [HttpGet]
        public eResponse<List<CapaEntidad.eTerritoriosCteDetalle>> ConsultaTerritoriosCliente_ById(string ClaveTerritorio, string ParametroBusqueda)
        {
            eResponse<List<CapaEntidad.eTerritoriosCteDetalle>> result = new eResponse<List<CapaEntidad.eTerritoriosCteDetalle>>();

            CapaNegocios.CN_CatTerritorios CD_Ter = new CapaNegocios.CN_CatTerritorios();
            List<CapaEntidad.eTerritoriosCteDetalle> lst = new List<CapaEntidad.eTerritoriosCteDetalle>();

            string Maximo = "";
            result.Estado = 0;
            try
            {
                lst = CD_Ter.ConsultaTerritoriosCliente_ById(Sesion.Id_Emp, Sesion.Id_Cd, ClaveTerritorio, ParametroBusqueda, Sesion.Emp_Cnx);
                result.Datos = lst;
                result.Estado = 1;
            }
            catch (Exception ex)
            {
                result.Datos = null;
                result.Estado = -1;
            }
            return result;
        }

        // Catalogo Territorios 
        // Insert Update 

        [HttpGet]
        public eResponse<int> Territorio_InsertUpdate(
            int IdInsertUpdate,
            int Id_Ter,
            string Ter_Nombre,
            int Id_Uen,
            int Id_Rik,
            int Id_Seg,
            int Id_TipoCliente,
            string Id_Local,
            int Id_TipoRepresentante,
            int Ter_Activo,
            int Cve_Terr
        )
        {
            eResponse<int> result = new eResponse<int>();
            result.Estado = 0;

            CN_CatTerritorios clsCatSegmentos = new CN_CatTerritorios();
            int verificador = 0;

            try
            {

                CapaEntidad.Territorios Ter = new CapaEntidad.Territorios();

                if (Sesion.VersionTerritorio)
                {
                    /*
                        ahora el cliente es quien tiene la relacion con el uen
                        y segmento por lo que se remueve del territorio
                    */
                    Ter.Id_Emp = Sesion.Id_Emp;
                    Ter.Id_Cd = Sesion.Id_Cd;
                    Ter.Descripcion = Id_Local;
                    Ter.Id_Rik = Id_Rik;
                    Ter.Id_TipoCliente = Id_TipoCliente;
                    Ter.Id_Seg = -1;
                    Ter.Id_Uen = -1;
                    Ter.Id_TipoRepresentante = Id_TipoRepresentante;
                    Ter.Id_Local = Id_Local;
                    //Ter.Estatus = false;
                    //if (Ter_Activo == 1)
                    //{
                    //    Ter.Estatus = true;
                    //}
                    Ter.Id_Ter = Id_Ter;
                    Ter.Cve_Terr = Cve_Terr;
                }
                else
                {
                    Ter.Id_Emp = Sesion.Id_Emp;
                    Ter.Id_Cd = Sesion.Id_Cd;
                    Ter.Descripcion = Ter_Nombre;
                    Ter.Id_Rik = Id_Rik;
                    Ter.Id_TipoCliente = Id_TipoCliente;
                    Ter.Id_Seg = Id_Seg;
                    Ter.Id_Uen = Id_Uen;
                    Ter.Id_TipoRepresentante = Id_TipoRepresentante;
                    Ter.Id_Local = Id_Local;
                    Ter.Estatus = false;
                    if (Ter_Activo == 1)
                    {
                        Ter.Estatus = true;
                    }
                    Ter.Id_Ter = Id_Ter;
                    Ter.Cve_Terr = Cve_Terr;
                }

                // INSERT 
                if (IdInsertUpdate == 0)
                {
                    if (Sesion.VersionTerritorio) Ter.Estatus = true;
                    clsCatSegmentos.InsertarTerritorios(Ter, Sesion.Emp_Cnx, ref verificador);

                    if (verificador == 1)
                    {
                        result.Estado = 1; // Se guardo territorio nuevo 
                        //clsCatSegmentos.InsertarTerritoriosDet(Ter, dt, Sesion.Emp_Cnx, ref verificador);                                                
                    }
                    else
                    {
                        result.Estado = -1; // Error guardar territorio nuevo 
                    }
                }

                // UPDATE
                if (IdInsertUpdate == 1)
                {
                    if (Sesion.VersionTerritorio) Ter.Estatus = Ter_Activo == 1;
                    clsCatSegmentos.ModificarTerritorios(Ter, Sesion.Emp_Cnx, ref verificador);

                    if (verificador == 1)
                    {
                        result.Estado = 2; // Se guardo territorio nuevo 
                        //clsCatSegmentos.InsertarTerritoriosDet(Ter, dt, Sesion.Emp_Cnx, ref verificador);
                    }
                    else
                    {
                        result.Estado = -2; // Error guardar territorio nuevo 
                    }
                }

            }
            catch (Exception ex)
            {
                result.Datos = 0;
                result.Estado = -1;
            }
            return result;

        }

        /// <summary>
        /// 
        /// Consulta los territorios asociados al RIK. Utiliza el pase de sesión para determinar la empresa, 
        /// el centro de distribución y el representante.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task<HttpResponseMessage> Get()
        {
            try
            {
                HttpContext current = HttpContext.Current;
                var t = Task<HttpResponseMessage>.Factory.StartNew(() =>
                {
                    HttpContext.Current = current;
                    try
                    {
                        using (IBusinessTransaction ibt = CN_FabricaTransaccionNegocios.Default(Sesion))
                        {
                            ibt.Begin();
                            CN_CatTerritorios cnCatTerritorios = new CN_CatTerritorios();
                            var territorios = cnCatTerritorios.ObtenerTerritoriosPorRik(Sesion.Id_Emp, Sesion.Id_Cd, Sesion.Id_Rik, Sesion);
                            return Request.CreateResponse(System.Net.HttpStatusCode.OK, territorios.ToList());
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("CatTerritorioController::Get", ex);
                        return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                });
                return t;
            }
            catch (Exception ex)
            {
                return Task<HttpResponseMessage>.Factory.StartNew(() =>
                {
                    return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
                });
            }

        }

        /// <summary>
        /// Devuelve la colección de territorios asociados a un prospecto.
        /// </summary>
        /// <param name="idEmp"></param>
        /// <param name="idCd"></param>
        /// <param name="idProspecto"></param>
        /// <returns>Colección de territorios asociados a un prospecto</returns>
        [HttpGet]
        public HttpResponseMessage Get(int idCliente)
        {
            try
            {
                CN_CatClienteDet cnCatClienteDet = new CN_CatClienteDet(Sesion);
                var resutado = cnCatClienteDet.ObtenerPorCliente(Sesion, idCliente);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, resutado);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        public IEnumerable<CatTerritorio> Get(int idEmp, int idCd, int idRik)
        {
            CN_CatTerritorios cnCatTerritorios = new CN_CatTerritorios();
            return cnCatTerritorios.ObtenerTerritoriosPorRik(idEmp, idCd, idRik, Sesion);
        }

        [HttpGet]
        public IEnumerable<CatTerritorio> Get(int idEmp, int idCd, int idRik, int idSeg)
        {
            CN_CatTerritorios cnCatTerritorios = new CN_CatTerritorios();
            return cnCatTerritorios.ObtenerTerritoriosPorRik(idEmp, idCd, idRik, Sesion);
        }
    }
}