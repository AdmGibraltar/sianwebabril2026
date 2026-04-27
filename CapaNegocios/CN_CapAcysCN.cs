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
    // 14ENE-2020 RFH 

    public class CN_CapAcysCN
    {
        public eAcysCN_Permisos spAcysCN_Permisos(
            ref int Estatus, ref string Mensaje, int Sucursal, int Id_Cte, string Conexion)
        {
            CD_CapAcysCN CD = new CD_CapAcysCN();
            return CD.spAcysCN_Permisos(ref Estatus, ref Mensaje, Sucursal, Id_Cte, Conexion);
        }

        public List<eAcysDet2> spAcysCN_Productos(
            ref int Estatus, ref string Mensaje,
            int Sucursal, int Id_Cte, int Id_Acs, string Conexion)
        {
            CD_CapAcysCN CD = new CD_CapAcysCN();
            return CD.spAcysCN_Productos(ref Estatus, ref Mensaje, Sucursal, Id_Cte,Id_Acs, Conexion);
        }

        //

    }
}