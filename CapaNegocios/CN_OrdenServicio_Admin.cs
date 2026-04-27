using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocios
{
    public class CN_OrdenServicio_Admin
    {
        public List<Comun> CatalogoEstatus(string conexion)
        {
            return new CD_OrdenServicio_Admin().CatalogoEstatus(conexion);
        }

        public List<entOrdenServicio> BuscarOrdenesServicios(string conexion, entOrdenServicioFiltros filtros)
        {
            return new CD_OrdenServicio_Admin().BuscarOrdenesServicios(conexion, filtros);
        }

        public void BuscarOrdenServicioMonitor(string conexion, entOrdenServicioFiltros filtros, ref List<entOrdenServicioMonitor> lstOrdenProductos, ref List<entOrdenServicioMonitor> lstOrdenCliente, ref entOrdenServicoIndicadores entIndicadores)
        {
            new CD_OrdenServicio_Admin().BuscarOrdenServicioMonitor(conexion, filtros, ref lstOrdenProductos, ref lstOrdenCliente, ref  entIndicadores);
        }

        public List<entOrdenServicio> ExportarOrdenServicioMonitor(string conexion, entOrdenServicioFiltros filtros)
        {
            return new CD_OrdenServicio_Admin().ExportarOrdenServicioMonitor(conexion, filtros);
        }

        public void CatalogoUsuarios(string conexion, int intidEmp, int intIdCd, ref List<Comun> lstRol, ref List<Comun> lstUsuarios)
        {
            new CD_OrdenServicio_Admin().CatalogoUsuarios(conexion, intidEmp, intIdCd, ref lstRol, ref lstUsuarios);
        }

        public void CatalogoServicios(string conexion, int intidEmp, int intIdCd, ref List<entOrdenServicioProductos> lstServicio)
        {
            new CD_OrdenServicio_Admin().CatalogoServicios(conexion, intidEmp,  intIdCd ,ref lstServicio);
        }

        public void GuardarOrdenServicio(string conexion, entOrdenServicioDetalle det, List<entOrdenServicioProductos> lstProductos, List<int> lstProductosAEliminar, List<entRolUsuario> lstRolUsuario, List<int> lstIdRolUsuarioEliminar, ref string mensaje)
        {
            new CD_OrdenServicio_Admin().GuardarOrdenServicio(conexion, det, lstProductos, lstProductosAEliminar, lstRolUsuario, lstIdRolUsuarioEliminar, ref mensaje);
        }

        public void ConsultarOrdenServicio(string strConexionLocal, string strConexionCentral, ref entOrdenServicioDetalle det, ref List<entOrdenServicioProductos> lstProductos, ref entOrdenServicioClienteDireccion dir, ref List<Comun> lstRol, ref List<entOrdenServicioProductos> lstServicio, ref List<Comun> lstTerritorio, ref List<Comun> lstUsuario, ref List<entRolUsuario> lstRolUsuario, ref List<Comun> lstCambioCompromiso)
        {
            new CD_OrdenServicio_Admin().ConsultarOrdenServicio(strConexionLocal, strConexionCentral, ref det, ref lstProductos, ref dir, ref lstRol, ref lstServicio, ref lstTerritorio, ref lstUsuario, ref lstRolUsuario, ref lstCambioCompromiso);

        }

        public void ConsultarCliente(string conexion, int intIdCliente, string strNombreCliente, ref string strMensaje, ref List<entOrdenServicioClienteDireccion> lstDireccion, ref List<Comun> lstTerritorio, ref List<Comun> lstUsuario)
        {
            new CD_OrdenServicio_Admin().ConsultarCliente(conexion, intIdCliente, strNombreCliente, ref strMensaje, ref lstDireccion, ref lstTerritorio, ref lstUsuario);
        }

        public List<Comun> ConsultarCatMotivoIncompleto(string conexion, int Id_Emp, int Id_Cd)
        {
            return new CD_OrdenServicio_Admin().ConsultarCatMotivoIncompleto(conexion, Id_Emp, Id_Cd);
        }

        public List<Comun> ConsultarCatMotivoCambioCompromiso(string conexion, int Id_Emp, int Id_Cd)
        {
            return new CD_OrdenServicio_Admin().ConsultarCatMotivoCambioCompromiso(conexion, Id_Emp, Id_Cd);
        }
        
        public void GuardarConfirmacion(string conexion, int Id_Emp, int Id_Cd, int intIdOrdenServicio,int intTipoConfirmacion, int intMotivoIncompleto, int intIdU, ref string mensaje)
        {
            new CD_OrdenServicio_Admin().GuardarConfirmacion(conexion, Id_Emp, Id_Cd, intIdOrdenServicio, intTipoConfirmacion, intMotivoIncompleto, intIdU, ref mensaje);
        }

        public List<Comun> ConsultarCatMotivoEliminacion(string conexion, int Id_Emp, int Id_Cd)
        {
            return new CD_OrdenServicio_Admin().ConsultarCatMotivoEliminacion(conexion, Id_Emp, Id_Cd);
        }

        public void EliminarOrdenServicio(string conexion, int Id_Emp, int Id_Cd, int Id_Usuario, int intIdOrdenServicio, int intMotivoEliminacion, ref string mensaje)
        {
            new CD_OrdenServicio_Admin().EliminarOrdenServicio(conexion, Id_Emp, Id_Cd, Id_Usuario, intIdOrdenServicio, intMotivoEliminacion, ref mensaje);
        }

        public void ConsultaProducto(string conexion, int Id_Emp, int Id_Cd, long Id_Prd, ref entOrdenServicioProductos producto, ref string strMensaje)
        {
            new CD_OrdenServicio_Admin().ConsultaProducto(conexion, Id_Emp, Id_Cd, Id_Prd, ref producto, ref strMensaje);
        }

        public void ConsultarRepresentantesServicio(string conexion, int Id_Emp, int Id_Cd, int intIdCte, int intIdTer, ref List<Comun> lstRepresentantes)
        {
            new CD_OrdenServicio_Admin().ConsultarRepresentantesServicio(conexion, Id_Emp, Id_Cd, intIdCte, intIdTer, ref lstRepresentantes);
        }
    }
}
