using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_AlertaAutorizacion
    {

        public void ConsultaAlertaAutorizacionLista(AlertaAutorizacion alertaautorizacion, ref List<AlertaAutorizacion> listalertaautorizaciones, string seleccion, string Conexion)
        {
            try
            {
                CD_AlertaAutorizacion cd_alertaautorizacion = new CD_AlertaAutorizacion();
                cd_alertaautorizacion.ConsultaAlertaAutorizacionLista(alertaautorizacion, ref listalertaautorizaciones, seleccion, Conexion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ConsultaAlertaAutorizacionRentabilidad(int id_emp, int idAutorizacionPrecio, int id_cd, ref List<EstadisticaRentabilidad> listalertaautorizaciones, string Conexion)
        {
            try
            {
                CD_AlertaAutorizacion cd_alertaautorizacion = new CD_AlertaAutorizacion();
                cd_alertaautorizacion.ConsultaAlertaAutorizacionRentabilidad(id_emp, idAutorizacionPrecio, id_cd, ref listalertaautorizaciones, Conexion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void AutorizarSolicitudGerente(AlertaAutorizacion conv, ref int Verificador, string Conexion)
        {
            try
            {
                CD_AlertaAutorizacion cd_conv = new CD_AlertaAutorizacion();
                cd_conv.AutorizarSolicitudGerente(conv, ref Verificador, Conexion);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void AlertaPrecioConsultaPrecio(AlertaAutorizacion alertaautorizacion, ref AlertaAutorizacion alertaautorizaciones, string Conexion)
        {
            try
            {
                CD_AlertaAutorizacion cd_alertaautorizacion = new CD_AlertaAutorizacion();
                cd_alertaautorizacion.AlertaPrecioConsultaPrecio(alertaautorizacion, ref alertaautorizaciones, Conexion);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void AlertaPrecioConsultaPrecioAcys(AlertaAutorizacion alertaautorizacion, ref AlertaAutorizacion alertaautorizaciones, string Conexion)
        {
            try
            {
                CD_AlertaAutorizacion cd_alertaautorizacion = new CD_AlertaAutorizacion();
                cd_alertaautorizacion.AlertaPrecioConsultaPrecioAcys(alertaautorizacion, ref alertaautorizaciones, Conexion);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public void AlertaPrecioConsultaPrecioGPMA(AlertaAutorizacion alertaautorizacion, ref AlertaAutorizacion alertaautorizaciones, string Conexion)
        {
            try
            {
                CD_AlertaAutorizacion cd_alertaautorizacion = new CD_AlertaAutorizacion();
                cd_alertaautorizacion.AlertaPrecioConsultaPrecioGPMA(alertaautorizacion, ref alertaautorizaciones, Conexion);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public void AlertaPrecioConsultaGPMaDetalle(AlertaAutorizacion alertaautorizacion, ref List<AlertaAutorizacion> listaProductos, string seleccion, string Conexion)
        {
            try
            {
                CD_AlertaAutorizacion cd_alertaAutorizacion = new CD_AlertaAutorizacion();
                cd_alertaAutorizacion.AlertaPrecioConsultaGPMaDetalle(alertaautorizacion, ref listaProductos, seleccion, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void CapAlertaAutorizacioInsertar(AlertaAutorizacion alertaautorizacion, string Conexion, ref int verificador)
        {
            try
            {
                CD_AlertaAutorizacion claseCapaDatos = new CD_AlertaAutorizacion();
                claseCapaDatos.CapAlertaAutorizacioInsertar(alertaautorizacion, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Precio el movimiento que puede ser 1 CRM, 2 ACYS , 3 Pedido, 4 Remisiones y 5 Factura
        /// </summary>
        public void ConsultaMotivos(int id_Movimiento, Sesion sesion, ref List<AlertaAutorizacion> alertaAutorizacion)
        {
            try
            {
                new CD_AlertaAutorizacion().ConsultaMotivos(id_Movimiento, sesion, ref alertaAutorizacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CapAlertaAutorizacioRentabilidadInsertar(AlertaAutorizacion alertaautorizacion, EstadisticaRentabilidad estrentabilidad, string Conexion, ref int verificador)
        {
            try
            {
                CD_AlertaAutorizacion claseCapaDatos = new CD_AlertaAutorizacion();
                claseCapaDatos.CapAlertaAutorizacioRentabilidadInsertar(alertaautorizacion, estrentabilidad, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Inserta el detalle de las autorizaciones GPMA Det
        /// </summary>
        /// <param name="alertaautorizacion"></param>
        /// <param name="Conexion"></param>
        /// <param name="verificador"></param>
        public void CapAlertaAutorizacioInsertarGPMa(AlertaAutorizacion alertaautorizacion, string Conexion, ref int idAutorizacionPrecio)
        {
            try
            {
                CD_AlertaAutorizacion claseCapaDatos = new CD_AlertaAutorizacion();
                claseCapaDatos.CapAlertaAutorizacioInsertarGPMa(alertaautorizacion, Conexion, ref idAutorizacionPrecio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Inserta el detalle de las autorizaciones GPMA Det
        /// </summary>
        /// <param name="alertaautorizacion"></param>
        /// <param name="Conexion"></param>
        /// <param name="verificador"></param>
        public void CapAlertaAutorizacioInsertarGPMaDet(AlertaAutorizacion alertaautorizacion, string Conexion, ref int verificador)
        {
            try
            {
                CD_AlertaAutorizacion claseCapaDatos = new CD_AlertaAutorizacion();
                claseCapaDatos.CapAlertaAutorizacioInsertarGPMaDet(alertaautorizacion, Conexion, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public void ConsultaAlertaCorreo(AlertaAutorizacion alertaautorizacion, ref string correousuario, ref string correodireccion, ref string nombrecliente, ref string prd_Descripcion, ref double precio_MinimoRik, ref double precioObjetivo, ref Int64 id_Prd, ref int req_aut, string Conexion)
        {
            try
            {
                CD_AlertaAutorizacion cd_alertaautorizacion = new CD_AlertaAutorizacion();
                cd_alertaautorizacion.ConsultaAlertaCorreo(alertaautorizacion, ref correousuario, ref correodireccion, ref nombrecliente, ref prd_Descripcion, ref precio_MinimoRik, ref precioObjetivo, ref id_Prd, ref req_aut, Conexion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public void ConsultaCuentaNacional(int id_cte, Sesion sesion, ref string tipocuenta, ref string nombrecliente, string Conexion)
        {
            try
            {
                new CD_AlertaAutorizacion().ConsultaCuentaNacional(id_cte, sesion, ref tipocuenta, ref nombrecliente, Conexion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}