using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_facturacionDoble
    {
        public void ConsultarRep_FacturaDoble(FacturacionDoble Registro, ref List<FacturacionDoble> Lista, string Conexion)
        {
            try
            {
                CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);
                SqlDataReader dr = null;
                FacturacionDoble nuevo;

                string[] Parametros = {
                                          "@Id_Emp",
                                          "@Id_Cd",
                                          "@FechaInicial",
                                          "@FechaFinal"
                };
                object[] Valores = {
                                       Registro.Id_Emp,
                                       Registro.Id_Cd,
                                       Registro.FechaInicial,
                                       Registro.FechaFinal
                                   };

                SqlCommand sqlcmd = default(SqlCommand);

                //Encabezado del PAGO
                sqlcmd = CapaDatos.GenerarSqlCommand("spCapFactura_ConsultaFacturasDobles", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    nuevo = new FacturacionDoble();

                    nuevo.Id_Cd = dr.IsDBNull(dr.GetOrdinal("Id_Cd")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cd"));
                    nuevo.Id_Cte = dr.IsDBNull(dr.GetOrdinal("Id_Cte")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Cte"));
                    nuevo.Id_Fac = dr.IsDBNull(dr.GetOrdinal("Id_Fac")) ? 0 : dr.GetInt32(dr.GetOrdinal("Id_Fac"));
                    nuevo.Fac_Fecha = dr.GetDateTime(dr.GetOrdinal("Fac_Fecha"));
                    nuevo.Fac_SubTotal = dr.IsDBNull(dr.GetOrdinal("Fac_SubTotal")) ? 0 : dr.GetDouble(dr.GetOrdinal("Fac_SubTotal"));
                    nuevo.Fac_ImporteIva = dr.IsDBNull(dr.GetOrdinal("Fac_ImporteIva")) ? 0 : dr.GetDouble(dr.GetOrdinal("Fac_ImporteIva"));
                    nuevo.Cte_NomComercial = dr.IsDBNull(dr.GetOrdinal("Cte_NomComercial")) ? "" : dr.GetString(dr.GetOrdinal("Cte_NomComercial"));
                    nuevo.Total = dr.IsDBNull(dr.GetOrdinal("Total")) ? 0 : dr.GetDouble(dr.GetOrdinal("Total"));
                    nuevo.Fac_Xml = dr.IsDBNull(dr.GetOrdinal("Fac_Xml")) ? "" : dr.GetString(dr.GetOrdinal("Fac_Xml"));
                    nuevo.fac_pdf = dr["fac_pdf"];
                    nuevo.Fac_FolioFiscal = dr.IsDBNull(dr.GetOrdinal("Fac_FolioFiscal")) ? "" : dr.GetString(dr.GetOrdinal("Fac_FolioFiscal"));
                    nuevo.FolioAE = dr.IsDBNull(dr.GetOrdinal("FolioAE")) ? 0 : dr.GetInt32(dr.GetOrdinal("FolioAE"));
                    nuevo.Fac_PdfCN = dr["Fac_PdfCN"];
                    nuevo.Fac_XMLCN = dr.IsDBNull(dr.GetOrdinal("Fac_XMLCN")) ? "" : dr.GetString(dr.GetOrdinal("Fac_XMLCN"));
                    nuevo.estatus = dr.IsDBNull(dr.GetOrdinal("Estatus")) ? "i" : dr.GetString(dr.GetOrdinal("Estatus"));
                    nuevo.estatusCN = dr.IsDBNull(dr.GetOrdinal("EstatusCN")) ? "i" : dr.GetString(dr.GetOrdinal("EstatusCN"));
                    nuevo.Nombreestatus = NombreEstatus(nuevo.estatus);
                    nuevo.NombreestatusCN = nuevo.estatusCN.ToLower() != "b" ? "Activo" : "Baja";
                    nuevo.Fac_FolioFiscalCN = dr.IsDBNull(dr.GetOrdinal("Fac_FolioFiscalCN")) ? "" : dr.GetString(dr.GetOrdinal("Fac_FolioFiscalCN"));


                    Lista.Add(nuevo);
                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private string NombreEstatus(string p)
        {
            switch (p)
            {
                case "O": return "Confirmado";
                case "C": return "Capturado";
                case "I": return "Impreso";
                case "U": return "Autorizado";
                case "A": return "Asignado";
                case "F": return "Facturado";
                case "R": return "Remisionado";
                case "X": return "Facturado/Remisionado";
                case "E": return "Embarque";
                case "N": return "Entregado";
                case "D": return "Baja por administración";
                case "B": return "Baja por cliente";
                default: return "";
            }
        }
    }
}