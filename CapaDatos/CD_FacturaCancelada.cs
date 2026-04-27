using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CapaEntidad;
using CapaModelo;
using System.Data.SqlTypes;
using System.Data.Common;
using System.Data;

namespace CapaDatos
{

    public class CD_FacturaCancelada
    {
        public void ConsultarFacturasCanceladas(string strConexion, int idCd, ref List<entFacturaCancelada> lstTotalFacturaCancelada, ref List<entFacturaCancelada> lstFacturaCancelada)
        {
            try
            {
                int intTabla = 0;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(strConexion);
                SqlDataReader dr = null;
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCapFacturaCancelada_Consultar", ref dr, new string[] { "@Id_Cd" }, new object[] { idCd });

                while (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        intTabla = Convert.ToInt32(dr["intTabla"]);
                        if (intTabla == 1)
                        {
                            var factura = new entFacturaCancelada();

                            factura.strRfcReceptor = dr["strRfcReceptor"].ToString();
                            factura.strRazonSocial = dr["strRazonSocial"].ToString();
                            factura.strSerie = dr["strSerie"].ToString();
                            factura.intFolio = Convert.ToInt32(dr["intFolio"]);
                            factura.strFolioFiscal = dr["strFolioFiscal"].ToString();
                            if (!string.IsNullOrEmpty(dr["dtFechaEmision"].ToString()))
                            {
                                factura.dtFechaEmision = Convert.ToDateTime(dr["dtFechaEmision"]);
                            }
                            else
                            {
                                factura.dtFechaEmision = null;
                            }


                            factura.dtFechaSolCanc = Convert.ToDateTime(dr["dtFechaSolCanc"]);
                            factura.strTipoDocumento = dr["strTipoDocumento"].ToString();
                            factura.strEstadoSAT = dr["strEstadoSAT"].ToString();
                            factura.decSubtotal = Convert.ToDecimal(dr["decSubtotal"]);
                            factura.decIVA = Convert.ToDecimal(dr["decIVA"]);
                            factura.decTotal = Convert.ToDecimal(dr["decTotal"]);
                            factura.intFolioRelacionado = Convert.ToInt32(dr["intFolioRelacionado"]);
                            factura.strSerieRelacionada = dr["strSerieRelacionada"].ToString();
                            factura.strFolioFiscalRelacionado = dr["strFolioFiscalRelacionado"].ToString();
                            factura.strTipoDocumentoRelacionado = dr["strTipoDocumentoRelacionado"].ToString();
                            factura.boolEsTotal = false;
                            lstFacturaCancelada.Add(factura);
                        }
                        else
                        {
                            var factura = new entFacturaCancelada
                            {
                                strRfcReceptor = dr["strRfcReceptor"].ToString(),
                                strRazonSocial = dr["strRazonSocial"].ToString(),
                                //strSerie = dr["strSerie"].ToString(),
                                //intFolio = Convert.ToInt32(dr["intFolio"]),
                                //strFolioFiscal = dr["strFolioFiscal"].ToString(),
                                //dtFechaEmision = Convert.ToDateTime(dr["dtFechaEmision"]),
                                //dtFechaSolCanc = Convert.ToDateTime(dr["dtFechaSolCanc"]),
                                //strTipoDocumento = dr["strTipoDocumento"].ToString(),
                                //strEstadoSAT = dr["strEstadoSAT"].ToString(),
                                dtFechaEmision = null,
                                dtFechaSolCanc = null,
                                decSubtotal = Convert.ToDecimal(dr["decSubtotal"]),
                                decIVA = Convert.ToDecimal(dr["decIVA"]),
                                decTotal = Convert.ToDecimal(dr["decTotal"]),
                                //intFolioRelacionado = Convert.ToInt32(dr["intFolioRelacionado"]),
                                //strSerieRelacionada = dr["strSerieRelacionada"].ToString(),
                                //strFolioFiscalRelacionado = dr["strFolioFiscalRelacionado"].ToString(),
                                //strTipoDocumentoRelacionado = dr["strTipoDocumentoRelacionado"].ToString()
                                boolEsTotal = true
                            };
                            lstTotalFacturaCancelada.Add(factura);
                        }
                    }
                    dr.NextResult();
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