using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaEntidad;
using CapaNegocios;
using CapaModelo;

namespace SIANWEB.WebService.Territorios
{
    public class TerritorioController : ApiController
    {

        //Extrae Indice de Territorios

        [HttpGet]
        public eResponse<List<CapaEntidad.Territorios>> Get(
            int PageNumber, int PageSize,
            string TextoBuscar, int TipoRepresentante, int Ter_Activo)
        {
            eResponse<List<CapaEntidad.Territorios>> result = new eResponse<List<CapaEntidad.Territorios>>();
            List<CapaEntidad.Territorios> lst = new List<CapaEntidad.Territorios>();
            result.Estado = 0;

            try
            {
                CN_CatTerritorios CN = new CN_CatTerritorios();
                CapaEntidad.Territorios E = new CapaEntidad.Territorios();

                E.Id_Emp = Sesion.Id_Emp;
                E.Id_Cd = Sesion.Id_Cd;

                CN.ConsultaTerritorios_Ver2(PageNumber, PageSize, E, TextoBuscar,
                    TipoRepresentante, Ter_Activo,
                    Sesion.Emp_Cnx, ref lst, Sesion.VersionTerritorio);

                result.Datos = lst;
                result.Estado = 1;
            }
            catch
            {
                result.Datos = null;
                result.Estado = 1;
            }

            return result;
        }

        // Consulta Territorio por Clave 

        [HttpGet]
        public eResponse<CapaEntidad.Territorios> Get(string Id_Ter, int Par1)
        {
            eResponse<CapaEntidad.Territorios> result = new eResponse<CapaEntidad.Territorios>();
            CapaEntidad.Territorios Terr = new CapaEntidad.Territorios();
            result.Estado = 0;

            try
            {
                CN_CatTerritorios CN = new CN_CatTerritorios();

                Terr = CN.ConsultaTerritorio(Sesion.Id_Emp, Sesion.Id_Cd, Id_Ter, Sesion.Emp_Cnx);

                result.Datos = Terr;
                result.Estado = 1;
            }
            catch
            {
                result.Datos = null;
                result.Estado = 1;
            }

            return result;
        }

        // DIC4-2019 
        // Consulta Territorio -> Clientes
        // o Clientes que aplicacn al territorio

        [HttpGet]
        public eResponse<List<CapaEntidad.eTerritoriosCteDetalle>> Consultar_TerritorioClientes(string Id_Ter, string ParametroBusqueda)
        {
            eResponse<List<CapaEntidad.eTerritoriosCteDetalle>> result = new eResponse<List<CapaEntidad.eTerritoriosCteDetalle>>();
            List<CapaEntidad.eTerritoriosCteDetalle> Terr = new List<CapaEntidad.eTerritoriosCteDetalle>();
            result.Estado = 0;
            try
            {
                CN_CatTerritorios CN = new CN_CatTerritorios();
                Terr = CN.ConsultaTerritoriosCliente_ById(
                    Sesion.Id_Emp, Sesion.Id_Cd, Id_Ter, ParametroBusqueda, Sesion.Emp_Cnx);
                result.Datos = Terr;
                result.Estado = 1;
            }
            catch
            {
                result.Datos = null;
                result.Estado = 1;
            }
            return result;
        }

        //

        [HttpGet]
        public eResponse<CapaEntidad.ModelAutorizacionTerritorios> ConsultaAutorizacionPendienteTerritorio(
            int ClaveTerritorio)
        {
            eResponse<CapaEntidad.ModelAutorizacionTerritorios> result = new eResponse<CapaEntidad.ModelAutorizacionTerritorios>();
            CapaEntidad.ModelAutorizacionTerritorios AutTer = new CapaEntidad.ModelAutorizacionTerritorios();
            result.Estado = 0;

            try
            {
                CapaDatos.CD_CatTerritorios cd_Territorios = new CapaDatos.CD_CatTerritorios();
                cd_Territorios.ConsultaAutorizacionPendienteTerritorio(ClaveTerritorio, ref AutTer, Sesion.Emp_Cnx);
                result.Datos = AutTer;
                result.Estado = 1;
            }
            catch
            {
                result.Datos = null;
                result.Estado = 1;
            }

            return result;
        }


        //

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
        public eResponse<List<CapaEntidad.CatTipoTerritorio>> Get_ListadoTipoTerritorio(int Movimiento)
        {
            eResponse<List<CatTipoTerritorio>> result = new eResponse<List<CatTipoTerritorio>>();
            List<CatTipoTerritorio> lst = new List<CatTipoTerritorio>();

            result.Estado = 0;
            result.Mensaje = "";

            try
            {
                CN_CatTerritorios CN = new CN_CatTerritorios();
                lst = CN.SelCatTipoTerritorio(Sesion.Id_Cd, Movimiento, Sesion.Emp_Cnx);

                result.Datos = lst;
                result.Estado = 1;
            }
            catch
            {
                result.Datos = null;
                result.Estado = -1;
                result.Mensaje = "Error";
            }
            return result;
        }

    }
}