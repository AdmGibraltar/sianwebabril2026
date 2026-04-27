using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using System.Data;
using CapaModelo;


/*
 * 24 sep 2018 RFH
 */

namespace CapaNegocios
{
    public class CN_ValuacionProyectoDetalle
    {
        public List<ValuacionProyectoDetalle> Detalle(int Id_Emp, int Id_Cd, int Id_Op, string Conexion)
        {

            CD_ValuacionProyectoDetalle CD = new CD_ValuacionProyectoDetalle();
            return CD.SelByOp(Id_Emp, Id_Cd, Id_Op, Conexion);

        }
        //
    }
}
