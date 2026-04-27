using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using System.Data;
using CapaModelo;

namespace CapaNegocios
{
    public class CN_CatalogoUnicoAplicaciones
    {
        public int InsertUpdate(ref eAcys2 acys, ref string MensajeError, string Conexion)
        {
            CD_CapAcys2Orden CD = new CD_CapAcys2Orden();
            return CD.InsertUpdate(ref acys, ref MensajeError, Conexion);
        }

    }
}