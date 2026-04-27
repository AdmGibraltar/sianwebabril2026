using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_enviarCorreo
    {
        /// <summary>
        /// Funcion que consulta la información del correo
        /// </summary>
        /// <param name="Id_Emp"></param>
        /// <param name="Id_Cd"></param>
        /// <param name="Conexion"></param>
        /// <param name="ListaCorreoUsuario"></param>
        public void ConsultarCorreoUsuario(int Id_Emp, int Id_Cd, string Conexion, ref List<UsuarioCorreo> ListaCorreoUsuario)
        {
            CD_EnviarCorreo Cd_Envia = new CD_EnviarCorreo();
            Cd_Envia.ConsultarCorreoUsuario(Id_Emp, Id_Cd, Conexion, ref ListaCorreoUsuario);
        }

        public void ConsultarCorreoUserXPefril(int Id_Emp, int Id_Cd, string Perfil, string Conexion, ref List<UsuarioCorreo> ListaCorreoUsuario)
        {
            CD_EnviarCorreo Cd_Envia = new CD_EnviarCorreo();
            Cd_Envia.ConsultarCorreoUserXPefril(Id_Emp, Id_Cd, Perfil, Conexion, ref ListaCorreoUsuario);
        }

    }

}