using CapaEntidad;
using CapaDatos;
using System.Collections.Generic;
using System;

namespace CapaNegocios
{
    public class CN_FacturaCancelada
    {
        public void ConsultarFacturasCanceladas(string conexion, int idCd, ref List<entFacturaCancelada> lstTotalFacturaCancelada, ref List<entFacturaCancelada> lstFacturaCancelada)
        {
            try
            {
                new CD_FacturaCancelada().ConsultarFacturasCanceladas(conexion, idCd, ref lstTotalFacturaCancelada, ref lstFacturaCancelada);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}