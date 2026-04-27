using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;
using System.Data;
using CapaModelo;

namespace CapaDatos
{
    public class CD_CapDescargaMasivaDocVentas
    {
        public void ConsultaFactura(string Conexion, int Id_Emp, int Id_Cd, int intIdCte, string strPeriodo, bool boolPDF, bool boolXML, ref List<entDocVentas> lstDocVenta)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@Periodo", "@boolPDF", "@boolXML" };
                object[] Valores = { Id_Emp, Id_Cd, intIdCte, strPeriodo, boolPDF, boolXML };
                entDocVentas docVentas = new entDocVentas();
                
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFactura_ConsultarDocumentos", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    docVentas = new entDocVentas();
                    docVentas.objDocPDF  = dr["Fac_Pdf"];
                    docVentas.objDocXML  = dr["Fac_xml"];
                    docVentas.strUUID = dr["Fac_UUID"].ToString();
                    docVentas.Id_Fac = (int)dr["Id_Fac"];
                    lstDocVenta.Add(docVentas);
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd",  "@Id_Cte", "@Periodo","@boolPDF", "@boolXML" };
                object[] Valores = { Id_Emp, Id_Cd, intIdCte, strPeriodo, boolPDF, boolXML };
                entDocVentas docVentas = new entDocVentas();

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapNotaCredito_ConsultarDocumentos", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    docVentas = new entDocVentas();
                    docVentas.objDocPDF = dr["Ncr_Pdf"];
                    docVentas.objDocXML = dr["Ncr_Xml"];
                    docVentas.strUUID = dr["Ncr_UUID"].ToString();
                    docVentas.Id_Ncr = (int)dr["Id_Ncr"];
                    lstDocVenta.Add(docVentas);
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaNotaCargo(string Conexion, int Id_Emp, int Id_Cd, int intIdCte, string strPeriodo, bool boolPDF, bool boolXML, ref List<entDocVentas> lstDocVenta)
        {
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@Periodo", "@boolPDF", "@boolXML" };
                object[] Valores = { Id_Emp, Id_Cd, intIdCte, strPeriodo, boolPDF, boolXML };
                entDocVentas docVentas = new entDocVentas();
               
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapNotaCargo_ConsultarDocumentos", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    docVentas = new entDocVentas();
                    docVentas.objDocPDF = dr["Nca_Pdf"];
                    docVentas.objDocXML = dr["Nca_Xml"];
                    docVentas.strUUID = dr["Nca_UUID"].ToString();
                    docVentas.Id_Nca = (int)dr["Id_Nca"];
                    lstDocVenta.Add(docVentas);
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
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
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id_Emp", "@Id_Cd", "@Id_Cte", "@Periodo", "@boolPDF", "@boolXML" };
                object[] Valores = { Id_Emp, Id_Cd, intIdCte, strPeriodo, boolPDF, boolXML };
                entDocVentas docVentas = new entDocVentas();

                
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapPagoDetComplemento_ConsultarDocumentos", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    docVentas = new entDocVentas();
                    docVentas.objDocPDF = dr["Pago_Pdf"];
                    docVentas.objDocXML = dr["Pago_Xml"];
                    //docVentas.strUUID = dr["Pago_UUID"].ToString();
                    docVentas.Id_PagDet = (int)dr["Id_PagDet"];
                    lstDocVenta.Add(docVentas);
                }
                dr.Close();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}