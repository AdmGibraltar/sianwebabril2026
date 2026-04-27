using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_ClienteIntermediarioPago
    {
        CD_ClienteIntermediarioPago CD;

        /// <summary>
        /// Funcion que consulta la información del cliente sobre intermediarios de pago.
        /// </summary>
        /// <param name="inter"></param>
        /// <param name="Conexion"></param>
        /// <param name="List"></param>
        public void ConsultaClienteIntermediarioFinanciero(ClienteIntermediarioPago inter, string Conexion, ref System.Data.DataTable dt)
        {
            CD = new CD_ClienteIntermediarioPago();
            CD.ConsultaClienteIntermediarioFinanciero(inter, Conexion, ref dt);
        }

        /// <summary>
        /// Función que tare la informacion del intermediario de pago.
        /// </summary>
        /// <param name="inter"></param>
        /// <param name="Conexion"></param>
        /// <param name="List"></param>
        public void ConsultaIntermediarioFinanciero(ref ClienteIntermediarioPago inter, string Conexion)
        {
            CD = new CD_ClienteIntermediarioPago();
            CD.ConsultaIntermediarioFinanciero(ref inter, Conexion);
        }

        /// <summary>
        /// Función que insertar o actualiza la información
        /// </summary>
        /// <param name="inter"></param>
        /// <param name="dt"></param>
        /// <param name="Conexion"></param>
        public void InsertarClienteIntermediarioFinanciero(ClienteIntermediarioPago inter, System.Data.DataTable dt, string Conexion)
        {
            CD = new CD_ClienteIntermediarioPago();
            CD.InsertarClienteIntermediarioFinanciero(inter, dt, Conexion);
        }
    }
}
