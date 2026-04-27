using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_ConfiguraOCE
    {
        public void LlenaListadoConfiguraOCE(string Conexion, string sp, int Id_CDI, ref List<ConfiguraOCE> Lista)
        {
            try
            {
                CD_ConfiguraOCE CD = new CD_ConfiguraOCE();
                CD.LlenaListadoConfiguraOCE(Conexion, sp, Id_CDI, ref Lista);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //  
        public void ActualizaConfiguraOCE(string Conexion, string sp, ConfiguraOCE configura, ref int i)
        {
            try
            {
                CD_ConfiguraOCE CD = new CD_ConfiguraOCE();
                CD.ActualizaConfiguraOCE(Conexion, sp, configura, ref i);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
