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
    public class CN_CapAcys2_ServicioValorTipo
    {

        public CN_CapAcys2_ServicioValorTipo()
        {
        }
                
        public List<eCapAcys2_TipoServicio> Consultar_ServicioValorTipo(int Id_Emp, int Id_Cd, int IdTipoServicio, string Conexion)
        {
            CD_CapAcys_ServicioValorTipo CD = new CD_CapAcys_ServicioValorTipo();
            return  CD.Consultar_ServicioValor(Id_Emp, Id_Cd,  IdTipoServicio,Conexion);
        }

        //

    }
}
