using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using System.Data;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_FaltanteOportunidad
    {
        public void ConsultaFaltanteOportunidad(string Conexion, FaltanteOportunidad Faltante, ref DataSet dsProductos)
        {
            try
            {
                CD_FaltanteOportunidad CD = new CD_FaltanteOportunidad();
                CD.ConsultaFaltanteOportunidad(Conexion, Faltante, ref dsProductos);
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
                CD_FaltanteOportunidad CD = new CD_FaltanteOportunidad();
                CD.LlenaCombo(Id_Grupo, Conexion, sp, ref Lista);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}