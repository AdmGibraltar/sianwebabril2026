using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class CN_Reporte_LogFactura
    {
        public void ListaLog(eReporte_LogFactura search, string Conexion, ref List<eReporte_LogFactura> List)
        {
            try
            {
                CD_Reporte_LogFactura claseCapaDatos = new CD_Reporte_LogFactura();
                claseCapaDatos.ListaLog(search, Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}