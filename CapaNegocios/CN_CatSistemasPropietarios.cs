using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CatSistemasPropietarios
    {
        public void ConsultaSistemasPropietarios(String Conexion, ref List<SistemasPropietarios> List)
        {
            try
            {
                CD_CatSistemasPropietarios claseCapaDatos = new CD_CatSistemasPropietarios();
                claseCapaDatos.ConsultaBanco(Conexion, ref List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LlenaCombo(string Conexion, string sp, ref List<Comun> Lista)
        {
            try
            {
                CD_CatSistemasPropietarios CD = new CD_CatSistemasPropietarios();
                CD.LlenaCombo(Conexion, sp, ref Lista);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LlenaCombo(int Id_Cat, string Conexion, string sp, ref List<Comun> Lista)
        {
            try
            {
                CD_CatSistemasPropietarios CD = new CD_CatSistemasPropietarios();
                CD.LlenaCombo(Id_Cat, Conexion, sp, ref Lista);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}