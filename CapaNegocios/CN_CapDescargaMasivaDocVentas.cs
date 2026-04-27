using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad;
using CapaDatos;
using System.Data;
using CapaModelo;

namespace CapaNegocios
{

    public class CN_CapDescargaMasivaDocVentas
    {
        public void ConsultaFacturas(string Conexion, int Id_Emp, int Id_Cd, int intIdCte, string strPeriodo, bool boolPDF, bool boolXML, ref List<entDocVentas> lstDocVenta)
        {
            try
            {
                CD_CapDescargaMasivaDocVentas capaDatos = new CD_CapDescargaMasivaDocVentas();
                capaDatos.ConsultaFactura(Conexion, Id_Emp, Id_Cd, intIdCte, strPeriodo, boolPDF, boolXML, ref lstDocVenta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaNotaCredito(string Conexion, int Id_Emp, int Id_Cd, int intIdCte, string strPeriodo, bool boolPDF, bool boolXML, ref List<entDocVentas> lstDocVenta)
        {
            try
            {
                CD_CapDescargaMasivaDocVentas capaDatos = new CD_CapDescargaMasivaDocVentas();
                capaDatos.ConsultaNotaCredito(Conexion, Id_Emp, Id_Cd, intIdCte, strPeriodo, boolPDF, boolXML, ref lstDocVenta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // 
        public void ConsultaNotaCargo(string Conexion, int Id_Emp, int Id_Cd, int intIdCte, string strPeriodo, bool boolPDF, bool boolXML, ref List<entDocVentas> lstDocVenta)
        {
            try
            {
                CD_CapDescargaMasivaDocVentas capaDatos = new CD_CapDescargaMasivaDocVentas();
                capaDatos.ConsultaNotaCargo(Conexion, Id_Emp, Id_Cd, intIdCte, strPeriodo, boolPDF, boolXML, ref lstDocVenta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaComplementoPago(string Conexion, int Id_Emp, int Id_Cd, int intIdCte, string strPeriodo, bool boolPDF, bool boolXML, ref List<entDocVentas> lstDocVenta)
        {
            try
            {
                CD_CapDescargaMasivaDocVentas capaDatos = new CD_CapDescargaMasivaDocVentas();
                capaDatos.ConsultaComplementoPago(Conexion, Id_Emp, Id_Cd, intIdCte, strPeriodo, boolPDF, boolXML, ref lstDocVenta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //

    }
}