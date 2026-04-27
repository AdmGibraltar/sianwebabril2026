using System;
using System.Collections.Generic;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocios
{
    public class CN_UsuariosFacturaCancelada
    {
        /// <summary>
        /// Consulta el catálogo de sucursales
        /// </summary>
        /// <param name="strConnection">Cadena de conexión</param>
        /// <returns>Lista de sucursales</returns>
        public List<entUsuarioFacturaCancelada> CatalogoSucursales(string strConnection)
        {
            try
            {
                CD_UsuariosFacturaCancelada cdUsuarios = new CD_UsuariosFacturaCancelada();
                return cdUsuarios.CatalogoSucursales(strConnection);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en CN_UsuariosFacturaCancelada.CatalogoSucursales: " + ex.Message);
            }
        }

        /// <summary>
        /// Consulta los usuarios por sucursal
        /// </summary>
        /// <param name="strConnection">Cadena de conexión</param>
        /// <param name="intIdCd">ID de la sucursal</param>
        /// <param name="lstUsuariosActivos">Lista de usuarios activos (salida)</param>
        /// <param name="lstUsuariosNoActivos">Lista de usuarios no activos (salida)</param>
        public void CatalogoUsuarios(string strConnection, ref entUsuarioFacturaCancelada entUsuario)
        {
            try
            {
                CD_UsuariosFacturaCancelada cdUsuarios = new CD_UsuariosFacturaCancelada();
                cdUsuarios.CatalogoUsuarios(strConnection, ref entUsuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en CN_UsuariosFacturaCancelada.CatalogoUsuarios: " + ex.Message);
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
            try
            {
                CD_UsuariosFacturaCancelada cdUsuarios = new CD_UsuariosFacturaCancelada();
                cdUsuarios.AgregarUsuario(strConnection, entCorreos);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en CN_UsuariosFacturaCancelada.AgregarUsuario: " + ex.Message);
            }
        }



    }
}