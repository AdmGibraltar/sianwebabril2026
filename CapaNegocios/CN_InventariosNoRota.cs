using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_InventariosNoRota
    {
        public void ConsultaInventarioNoRotaDetalle(string Conexion, ref List<InventariosNoRota> List, RepExcesos Exceso)
        {
            try
            {
                CD_InventarioNoRota CD = new CD_InventarioNoRota();
                CD.ConsultaInventarioNoRotaDetalle(Conexion, ref List, Exceso);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaInventarioNoRotaDetalleCierre(string Conexion, ref List<InventariosNoRota> List, RepExcesos Exceso)
        {
            try
            {
                CD_InventarioNoRota CD = new CD_InventarioNoRota();
                CD.ConsultaInventarioNoRotaDetalleCierre(Conexion, ref List, Exceso);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LlenaCombo(int Id_Grupo, string Conexion, string sp, ref List<Comun> Lista)
        {
            try
            {
                CD_InventarioNoRota CD = new CD_InventarioNoRota();
                CD.LlenaCombo(Id_Grupo, Conexion, sp, ref Lista);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}