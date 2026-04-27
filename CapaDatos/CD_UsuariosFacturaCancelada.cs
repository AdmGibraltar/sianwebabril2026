using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_UsuariosFacturaCancelada
    {
        /// <summary>
        /// Consulta el catálogo de sucursales
        /// </summary>
        /// <param name="strConnection">Cadena de conexión</param>
        /// <returns>Lista de sucursales</returns>
        public List<entUsuarioFacturaCancelada> CatalogoSucursales(string strConnection)
        {
            SqlDataReader dr = null;
            List<entUsuarioFacturaCancelada> lstSucursales = new List<entUsuarioFacturaCancelada>();
            try
            {
                CD_Datos cdDatos = new CD_Datos(strConnection);
                SqlCommand sqlcmd = cdDatos.GenerarSqlCommand("spCapFacturasCancelada_ConsultarSucursales", ref dr, new string[] { }, new object[] { });

                while (dr.Read())
                {
                    lstSucursales.Add(new entUsuarioFacturaCancelada
                    {
                        intIdCd = Convert.ToInt32(dr["intIdCd"]),
                        strCdNombre = dr["strCdNombre"].ToString()
                    });
                }

                dr.Close();
                cdDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                if (dr != null && !dr.IsClosed) dr.Close();
                throw new Exception("Error en CD_UsuariosFacturaCancelada.CatalogoSucursales: " + ex.Message);
            }
            return lstSucursales;
        }

        /// <summary>
        /// Consulta los usuarios por sucursal
        /// </summary>
        /// <param name="strConnection">Cadena de conexión</param>
        /// <param name="intIdCd">ID de la sucursal</param>
        /// <param name="lstUsuariosActivos">Lista de usuarios activos (salida)</param>
        /// <param name="lstUsuariosNoActivos">Lista de usuarios no activos (salida)</param>
        public void CatalogoUsuarios(string strConnection, ref entUsuarioFacturaCancelada entUsuarioCorreos)
        {
            SqlDataReader dr = null;
            try
            {
                CD_Datos cdDatos = new CD_Datos(strConnection);
                string[] Parametros = { "@Id_Cd" };
                object[] Valores = { entUsuarioCorreos.intIdCd };
                SqlCommand sqlcmd = cdDatos.GenerarSqlCommand("spCapFacturaCancelada_ConsultarUsuarios", ref dr, Parametros, Valores);

                if (dr.Read())
                {
                    entUsuarioCorreos.intIdCd = Convert.ToInt32(dr["intIdCd"]);
                    entUsuarioCorreos.strCdNombre = dr["strCdNombre"].ToString();
                    entUsuarioCorreos.strCorreo = dr["strCorreo"].ToString();
                }
                else
                {
                    entUsuarioCorreos.strCorreo = ""; // No se encontraron usuarios
                }

                dr.Close();
                cdDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                if (dr != null && !dr.IsClosed) dr.Close();
                throw new Exception("Error en CD_UsuariosFacturaCancelada.CatalogoUsuarios: " + ex.Message);
            }
        }

        /// <summary>
        /// Agrega un usuario al catálogo de usuarios de facturas canceladas
        /// </summary>
        /// <param name="strConnection">Cadena de conexión</param>
        /// <param name="intIdCd">ID de la sucursal</param>
        /// <param name="intIdUsuario">ID del usuario</param>
        public void AgregarUsuario(string strConnection, entUsuarioFacturaCancelada entCorreos)
        {
            SqlDataReader dr = null;
            try
            {
                CD_Datos cdDatos = new CD_Datos(strConnection);
                string[] Parametros = { "@intIdCd", "@strCorreo" };
                object[] Valores = { entCorreos.intIdCd, entCorreos.strCorreo };
                SqlCommand sqlcmd = cdDatos.GenerarSqlCommand("spCapFacturaCancelada_InsertarUsuario", ref dr, Parametros, Valores);
                dr.Close();
                cdDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                if (dr != null && !dr.IsClosed) dr.Close();
                throw new Exception("Error en CD_UsuariosFacturaCancelada.AgregarUsuario: " + ex.Message);
            }
        }
    }
}