using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using CapaModelo;

namespace CapaNegocios
{
    public class CN_UsuarioRik
    {
        public List<eUsuarioRik> Lista(int Id_Emp, int Id_Cd, string Conexion)
        {
            CD_UsuarioRik cdURik = new CD_UsuarioRik();
            return cdURik.Lista(Id_Emp, Id_Cd,Conexion);
        }

        // NOV22-2019 RFH

        public List<eUsuarioRik> Lista_Combo(int Id_Emp, int Id_Cd, int Id_Tu, int Id_Uen, int TipoRik, string Conexion)
        {
            CD_UsuarioRik cdURik = new CD_UsuarioRik();
            return cdURik.Lista_Combo(Id_Emp, Id_Cd, Id_Tu, Id_Uen, TipoRik, Conexion);
        }

        //
    }
}
