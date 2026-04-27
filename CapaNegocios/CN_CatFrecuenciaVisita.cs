using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
namespace CapaNegocios
{
    public class CN_CatFrecuenciaVisita
    {

        public List<eCatFrecuenciaVisita> Listado(string Conexion)
        {
            CD_CatFrecuenciaVisita CD = new CD_CatFrecuenciaVisita();
            return CD.Listado(Conexion);
        }

        //
    }
}
