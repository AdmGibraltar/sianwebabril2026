using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaModelo;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_CapValProyectoParams
    {

        /*
         * Esta funcion no la utiliza ningun instancia por lo pronto
         * Se creo para reemplazar en metodo EF. Jun5-2019 RFH.
         */

        public int CapValProyectoParams_InsertUpdate(int Id_Emp, int Id_Cd, eCapValProyectoParams Data,string conexion)
        {
            CD_CapValProyectoParams CD = new CD_CapValProyectoParams();
            return CD.CapValProyectoParams_InsertUpdate(Id_Emp, Id_Cd, Data, conexion);            
        }

        //

    }
}
