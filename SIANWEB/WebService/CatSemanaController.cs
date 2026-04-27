using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CapaEntidad;
using CapaNegocios;
using System.Net.Http;

// 4 Dic 2018

namespace SIANWEB.WebService
{
    public class CatSemanaController : ApiController
    {

        [HttpGet]
        public eResponse<Semana> ConsultaSemanaActual(string Fecha)
        {
            eResponse<Semana> result = new eResponse<Semana>();
            result.Estado = 0;
            result.Mensaje = "";

            try
            {
                int verificador = 0;
                Semana S = new Semana();
                S.Id_Emp = Sesion.Id_Emp;
                S.Id_Cd = Sesion.Id_Cd;
                S.Sem_FechaAct = Convert.ToDateTime(Fecha);
            
                CN_CatSemana CN = new CN_CatSemana();
                CN.ConsultaSemanaActual(ref S, Sesion.Emp_Cnx, ref verificador);
                result.Datos = S;
                result.Estado = 1;                
            }
            catch (Exception ex) //manejo de error genérico
            {
                result.Estado = -1;
                result.Mensaje = ex.ToString();                
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