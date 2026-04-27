using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_EnviarCorreo
    {
        /// <summary>
        /// Funcion que consulta la informacion de usuario para obtener el correo teniendo en cuenta los siguiente tipo de usuario
        /// 3.- gerentte sucursal
        /// 10 .- Cobranza
        /// 17.- jefe servicio cliente
        /// </summary>
        /// <param name="Id_Emp"></param>
        /// <param name="Id_Cd"></param>
        /// <param name="Conexion"></param>
        /// <param name="CorreoUsuario"></param>
        public void ConsultarCorreoUsuario(int Id_Emp, int Id_Cd, string Conexion, ref List<UsuarioCorreo> ListaCorreoUsuario)
        {
            SqlDataReader dr = null;
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

            string[] Parametros = { "@Id_Emp",
                                      "@Id_Cd"
                                       };
            object[] Valores = {  Id_Emp,
                                   Id_Cd
                                 };

            SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spEnviarCorreoRepPedido", ref dr, Parametros, Valores);
            UsuarioCorreo CorreoUsuario;
            while (dr.Read())
            {
                CorreoUsuario = new UsuarioCorreo();
                CorreoUsuario.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                CorreoUsuario.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                CorreoUsuario.Id_U = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));
                CorreoUsuario.U_Nombre = Convert.ToString(dr.GetValue(dr.GetOrdinal("U_Nombre")));
                CorreoUsuario.U_Correo = Convert.ToString(dr.GetValue(dr.GetOrdinal("U_Correo")));

                ListaCorreoUsuario.Add(CorreoUsuario);

            }
            dr.Close();

            CapaDatos.LimpiarSqlcommand(ref sqlcmd);
        }


        public void ConsultarCorreoUserXPefril(int Id_Emp, int Id_Cd, string Perfil, string Conexion, ref List<UsuarioCorreo> ListaCorreoUsuario)
        {
            SqlDataReader dr = null;
            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

            string[] Parametros = { "@Id_Cd", "@Id_Emp", "@Perfil" };

            object[] Valores = { Id_Cd, Id_Emp, Perfil };

            SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("sp_CorreoAltaUser", ref dr, Parametros, Valores);
            UsuarioCorreo CorreoUsuario;
            while (dr.Read())
            {
                CorreoUsuario = new UsuarioCorreo();
                CorreoUsuario.Id_Emp = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Emp")));
                CorreoUsuario.Id_Cd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cd")));
                CorreoUsuario.Id_U = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_U")));
                CorreoUsuario.U_Nombre = Convert.ToString(dr.GetValue(dr.GetOrdinal("U_Nombre")));
                CorreoUsuario.U_Correo = Convert.ToString(dr.GetValue(dr.GetOrdinal("U_Correo")));

                ListaCorreoUsuario.Add(CorreoUsuario);

            }
            dr.Close();

            CapaDatos.LimpiarSqlcommand(ref sqlcmd);
        }

    }
}