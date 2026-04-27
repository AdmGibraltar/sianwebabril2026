using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaEntidad;
using CapaNegocios;
using CapaModelo;
using CapaDatos;
using SIANWEB.WebAPI.Models;
using System.Net.Http;
using SIANWEB.Core.Web.API;
using System.Threading.Tasks;
namespace SIANWEB.WebService.PortalRIK.GestionPromocion
{
    public class CrmRepresentanteController : BaseWebAPIController
    {
        [HttpGet]
        public IEnumerable<CrmLista> Get(int IdCD)
        {
            //
            // Se descontiuna el uso del parm. IdCD ya que se toma el actual.
            //

            List<CapaEntidad.CrmLista> lst = new List<CapaEntidad.CrmLista>();
            CN_CatRepresentantes CR = new CN_CatRepresentantes();

            CR.Consultar_RepresentantesCombo(Sesion.Id_Emp, Sesion.Id_U, ref lst, Sesion.Emp_Cnx);
            //CR.ConsultarRepresentantesCombo(Sesion.Id_Cd, ref lst, Sesion.Emp_Cnx);

            return lst;
        }

        // 02SEP-2020
        [HttpGet]
        public eResponse<List<eUsuarioRik>> Get_List(int Id_Cd)
        {
            eResponse<List<eUsuarioRik>> result = new eResponse<List<eUsuarioRik>>();
            CN_CatRepresentantes CR = new CN_CatRepresentantes();
            List<eUsuarioRik> Lst = new List<eUsuarioRik>();

            try
            {
                Lst = CR.Consultar_Representantes_ById_Cd(
                    Sesion.Id_Emp, Sesion.Id_Cd, Sesion.Emp_Cnx);

                result.Datos = Lst;
                result.Estado = 1;

            }
            catch (Exception ex)
            {
                result.Estado = -1;
                result.Mensaje = "Error";
            }

            return result;
        }

        // 22DIC-2020
        [HttpGet]
        public eResponse<List<eUsuarioRik>> spGetListByUser(int Id_Cd, int Id_U)
        {
            eResponse<List<eUsuarioRik>> result = new eResponse<List<eUsuarioRik>>();
            CN_CatRepresentantes CR = new CN_CatRepresentantes();
            List<eUsuarioRik> Lst = new List<eUsuarioRik>();

            try
            {
                Lst = CR.spGetListByUser(
                    Sesion.Id_Emp, Sesion.Id_Cd, Sesion.Id_U, Sesion.Emp_Cnx);

                result.Datos = Lst;
                result.Estado = 1;
            }
            catch (Exception ex)
            {
                result.Estado = -1;
                result.Mensaje = "Error";
            }
            return result;
        }

        // 09MAR-2021
        [HttpGet]
        public eResponse<List<eUsuarioRik>> spCatRik_ComboTodos_ver2(int IdParam1, int IdParam2, int Id_Param)
        {
            eResponse<List<eUsuarioRik>> result = new eResponse<List<eUsuarioRik>>();
            List<eUsuarioRik> Lst = new List<eUsuarioRik>();
            try
            {
                CN_CatRepresentantes CR = new CN_CatRepresentantes();
                // Segun el tipo de Usuario 
                Lst = CR.spCatRik_ComboTodos_ver2(Sesion.Id_Emp, Sesion.Id_Cd, Sesion.Id_U, Sesion.Emp_Cnx);

                result.Datos = Lst;
                result.Estado = 1;
            }
            catch (Exception ex)
            {
                result.Estado = -1;
                result.Mensaje = "Error";
            }
            return result;
        }

        // 12MAY-2021
        [HttpGet]
        public eResponse<List<eUsuarioRik>> spCatUsuario_Search(string Texto, int Id1, int Id2, int Id_Param)
        {
            eResponse<List<eUsuarioRik>> result = new eResponse<List<eUsuarioRik>>();
            List<eUsuarioRik> Lst = new List<eUsuarioRik>();
            try
            {
                CN_CatRepresentantes CR = new CN_CatRepresentantes();

                Lst = CR.spCatUsuario_Search(Sesion.Id_Emp, Sesion.Id_Cd, Sesion.Id_U, Texto, Sesion.Emp_Cnx);

                result.Datos = Lst;
                result.Estado = 1;
            }
            catch (Exception ex)
            {
                result.Estado = -1;
                result.Mensaje = "Error";
            }
            return result;
        }

        // 27MAY-2021
        [HttpGet]
        public eResponse<List<eUsuarioRik>> spCatUsuario_SearchByCte(
            int Id1, int Id2, int Id_Cte)
        {
            eResponse<List<eUsuarioRik>> result = new eResponse<List<eUsuarioRik>>();
            List<eUsuarioRik> Lst = new List<eUsuarioRik>();
            try
            {
                CN_CatRepresentantes CR = new CN_CatRepresentantes();

                Lst = CR.spCatUsuario_SearchByCte(Sesion.Id_Emp, Sesion.Id_Cd, Id_Cte, Sesion.Emp_Cnx);

                result.Datos = Lst;
                result.Estado = 1;
            }
            catch (Exception ex)
            {
                result.Estado = -1;
                result.Mensaje = "Error";
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