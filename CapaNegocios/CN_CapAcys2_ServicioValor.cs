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
    public class CN_CapAcys2_ServicioValor
    {
        public CN_CapAcys2_ServicioValor()
        {
        }

        //
        // 30 ago 2018 RFH 
        // Consulta Servicios por Id
        //
        public List<eCapAcys2_ServicioValor> Consultar_ServicioValor(int Id_Emp, int Id_Cd, int Id_Acs, int Tipo, string Conexion)
        {
            CD_CapAcys_ServicioValor CD = new CD_CapAcys_ServicioValor();
            return CD.Consultar_ServicioValor(Id_Emp, Id_Cd, Id_Acs, Tipo, Conexion);
        }
        //
    }
}
