using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CapaEntidad;
using CapaDatos;
using System.Data;
using CapaModelo;

namespace CapaNegocios
{
    public class CN_Prospecto
    {

        public int Consultar(int Id_Emp, int Id_Cd, int Id_Rik, int Id_Cte, string Conexion)
        {
            int Result = 0;
            try
            {
                CD_CrmProspecto CRMp = new CD_CrmProspecto();
                Result = CRMp.ConsultarById(Id_Emp, Id_Cd, Id_Rik, Id_Cte, Conexion);
            }
            catch (Exception ex)
            {
                Result = -1;
            }
            return Result;
        }

        //
        public bool ValidarUenSegmento(int id_emp, int id_cd, int id_cte, string conexion)
        {
            try
            {
                return new CD_CrmProspecto().ConsultarUenYSegmento(id_emp, id_cd, id_cte, conexion);
            }
            catch { }
            return false;
        }
    }
}
