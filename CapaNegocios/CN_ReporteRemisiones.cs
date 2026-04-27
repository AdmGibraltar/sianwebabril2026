using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_ReporteRemisiones
    {
        public void Rep_RemisionesVencidas(ReporteRemisiones Datos, ref List<ReporteRemisiones> ReporteRemisiones, string Conexion)
        {
            CD_ReporteRemisiones CD = new CD_ReporteRemisiones();
            CD.Rep_RemisionesVencidas(Datos, ref ReporteRemisiones, Conexion);
        }

        public void Rep_RemisionesVencidasCN(ReporteRemisiones Datos, ref List<ReporteRemisiones> ReporteRemisiones, string Conexion)
        {
            CD_ReporteRemisiones CD = new CD_ReporteRemisiones();
            CD.Rep_RemisionesVencidasCN(Datos, ref ReporteRemisiones, Conexion);
        }

        public void Rep_RemisionesFacturas(ReporteRemisiones Datos, ref List<ReporteRemisiones> ReporteRemisiones, string Conexion)
        {
            CD_ReporteRemisiones CD = new CD_ReporteRemisiones();
            CD.Rep_RemisionesFacturas(Datos, ref ReporteRemisiones, Conexion);
        }

        public void Rep_Consigancion(ReporteConsignacion Datos, ref List<ReporteConsignacion> ReporteRemisiones, string Conexion)
        {
            CD_ReporteRemisiones CD = new CD_ReporteRemisiones();
            CD.Rep_Consigancion(Datos, ref ReporteRemisiones, Conexion);
        }
    }
}