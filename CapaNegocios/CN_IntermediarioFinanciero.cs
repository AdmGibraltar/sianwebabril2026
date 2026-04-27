using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_IntermediarioFinanciero
    {
        /// <summary>
        /// Función que consulta la información en la tabla CatIntermediarioFinanciero
        /// </summary>
        /// <param name="banco"></param>
        /// <param name="Conexion"></param>
        /// <param name="List"></param>
        public void ConsultaIntermediarioFinanciero(IntermediarioFinanciero inter, string Conexion, ref List<IntermediarioFinanciero> List)
        {
            try
            {
                CD_IntermediarioFinanciero Intermediario = new CD_IntermediarioFinanciero();
                Intermediario.ConsultaIntermediarioFinanciero(inter, Conexion, ref  List);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaIntermediarioBanco(IntermediarioFinanciero inter, string Conexion, ref List<IntermediarioFinanciero> List)
        {
            CD_IntermediarioFinanciero Intermediario = new CD_IntermediarioFinanciero();
            Intermediario.ConsultaIntermediarioBanco(inter, Conexion, ref  List);
        }


        /// <summary>
        /// Función que inserta la información en la tabla de CatIntermediarioFinanciero
        /// </summary>
        /// <param name="inter"></param>
        /// <param name="Conexion"></param>
        /// <param name="verificador"></param>
        public void InsertarIntermediarioFinanciero(IntermediarioFinanciero inter, string Conexion, ref int verificador)
        {
            try
            {
                CD_IntermediarioFinanciero Intermediario = new CD_IntermediarioFinanciero();
                Intermediario.InsertarIntermediarioFinanciero(inter, Conexion, ref  verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Función que actualiza la información en la tabla CatIntermediarioFinanciero
        /// </summary>
        /// <param name="inter"></param>
        /// <param name="Conexion"></param>
        /// <param name="verificador"></param>
        public void ModificarIntermediarioFinanciero(IntermediarioFinanciero inter, string Conexion, ref int verificador)
        {
            try
            {
                CD_IntermediarioFinanciero Intermediario = new CD_IntermediarioFinanciero();
                Intermediario.ModificarIntermediarioFinanciero(inter, Conexion, ref  verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
