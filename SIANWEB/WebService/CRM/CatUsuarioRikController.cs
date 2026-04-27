using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaEntidad;
using CapaNegocios;

namespace SIANWEB.WebService
{
    public class CatUsuarioRikController: ApiController
    {
        [HttpGet]
        public  List<eUsuarioRik> Get(int IdGerente, int IdRik)
        {
            List<eUsuarioRik> lst = new List<eUsuarioRik>();
            CN_UsuarioRik cn = new CN_UsuarioRik();
            lst = cn.Lista(Sesion.Id_Emp, Sesion.Id_Cd, Sesion.Emp_Cnx);
            return lst;
        }

        // NOV22-2019 RFH
        // Utilizado en territorios

        [HttpGet]
        public eResponse<List<eUsuarioRik>> Get_Lista_Combo(int Id_Uen, int TipoRik)
        {
            eResponse<List<eUsuarioRik>> result = new eResponse<List<eUsuarioRik>>();
            List<eUsuarioRik> lst = new List<eUsuarioRik>();
            result.Estado = 0;            
            try {                                
                CN_UsuarioRik cn = new CN_UsuarioRik();

                //lst = cn.Lista_Combo(Sesion.Id_Emp, Sesion.Id_Cd, Sesion.Id_TU, Id_Uen, TipoRik, Sesion.Emp_Cnx);

                int Id_TU = 0;                
                if (Sesion.Id_TU == 2)
                {
                    Id_TU = -1;
                }
                else
                {
                    Id_TU = Sesion.Id_TU;
                }

                lst = cn.Lista_Combo(Sesion.Id_Emp, Sesion.Id_Cd, Id_TU, Id_Uen, TipoRik, Sesion.Emp_Cnx);
                
                
                result.Estado=1;
                result.Datos = lst;
            } catch (Exception ex) {
                result.Estado=-1;
                result.Datos = lst;
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
        //


    }
}