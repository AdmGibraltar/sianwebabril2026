using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_ConfiguracionDias
    {
        public void CatConfiguracionDias_Insertar(ConfiguracionDias cf, ref int Verificador, string Conexion)
        {
            try
            {
                CD_ConfiguracionDias cd_cf = new CD_ConfiguracionDias();
                cd_cf.CatConfiguracionDias_Insertar(cf, ref Verificador, Conexion);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public void CatConfiguracionDias_Lista(ref List<ConfiguracionDias> List, string Conexion)
        {
            try
            {
                CD_ConfiguracionDias cd_cf = new CD_ConfiguracionDias();
                cd_cf.CatConfiguracionDias_Lista(ref List, Conexion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void CatConfiguracionDias_Eliminar(int Id_DF, ref int Verificador, string Conexion)
        {
            try
            {
                CD_ConfiguracionDias cd_cf = new CD_ConfiguracionDias();
                cd_cf.CatConfiguracionDias_Eliminar(Id_DF, ref Verificador, Conexion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void RepEntradasCRM_Consulta(int TipoCd, int Anio, int Mes, int Tipo, int Id_U, ref DataTable dt)
        {
            try
            {
                CD_ConfiguracionDias cd_cd = new CD_ConfiguracionDias();
                cd_cd.RepEntradasCRM_Consulta(TipoCd, Anio, Mes, Tipo, Id_U, ref dt);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
