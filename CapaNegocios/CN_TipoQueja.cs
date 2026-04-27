using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class CN_TipoQueja
    {
        public void ConsultaTipoQueja_Buscar(eTipoQueja tipoqueja, ref List<eTipoQueja> listaTipoQueja, string Conexion)
        {
            try
            {
                CapaDatos.CD_TipoQueja cdTipoQueja = new CapaDatos.CD_TipoQueja();
                cdTipoQueja.ConsultaTipoQueja_Buscar(tipoqueja, ref listaTipoQueja, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}