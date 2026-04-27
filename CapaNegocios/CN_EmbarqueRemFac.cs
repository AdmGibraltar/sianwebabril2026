using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class CN_EmbarqueRemFac
    {
        public void ConsultaProRemFacEntrega(int Id_Emp, int Id_Cd, string Conexion, EmbarqueFacRem remisionfiltro, ref List<EmbarqueFacRem> List)
        {
            try
            {
                CD_EmbarqueFacRem Remision = new CD_EmbarqueFacRem();
                Remision.ConsultaProRemFacEntrega(Id_Emp, Id_Cd, Conexion, remisionfiltro, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaEmbarqueFacRem(int Id_Emp, int Id_Cd, string Conexion, EmbarqueFacRem remisionfiltro, ref List<EmbarqueFacRem> List)
        {
            try
            {
                CD_EmbarqueFacRem Remision = new CD_EmbarqueFacRem();
                Remision.ConsultaEmbarqueFacRem(Id_Emp, Id_Cd, Conexion, remisionfiltro, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}