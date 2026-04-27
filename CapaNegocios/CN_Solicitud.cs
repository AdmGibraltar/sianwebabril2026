using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using CapaEntidad;
using System.Data;
namespace CapaNegocios
{
    public class CN_Solicitud
    {
        public void ConsultaProductos(string Conexion, ref List<Producto> lstProductos)
        {
            try
            {
                CD_Solicitud CD = new CD_Solicitud();
                CD.ConsultaProductos(Conexion, ref lstProductos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GuardaSolicitud(Sesion sesion, Solicitud solicitud, ref int respuesta, DataTable objdtTabla)
        {
            try
            {
                CD_Solicitud CD = new CD_Solicitud();
                CD.GuardaSolicitud(sesion, solicitud, ref respuesta, objdtTabla);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaSolicitudes(Sesion sesion, int Id_Tu, ref List<Solicitud> lstSolicitud, int Id_Solicitud, string NomCliente, int Id_Factura, DateTime Fec_Inicio, DateTime Fec_Fin, int TipoServicio, int Id_Accion, int Estatus)
        {
            try
            {
                CD_Solicitud CD = new CD_Solicitud();
                CD.ConsultaSolicitudes(sesion, Id_Tu, ref lstSolicitud, Id_Solicitud, NomCliente, Id_Factura, Fec_Inicio, Fec_Fin, TipoServicio, Id_Accion, Estatus);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaSolicitud(Sesion Sesion, ref Solicitud solicitud, ref List<Producto> lstProducto, ref List<Documento> lstDocumentos)
        {
            try
            {
                CD_Solicitud CD = new CD_Solicitud();
                CD.ConsultaSolicitud(Sesion, solicitud, ref lstProducto, ref lstDocumentos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CancelaSolicitud(Sesion sesion, ref string respuesta, ref  Solicitud sol)
        {
            try
            {
                CD_Solicitud CD = new CD_Solicitud();
                CD.CancelaSolicitud(sesion, ref respuesta, ref sol);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
