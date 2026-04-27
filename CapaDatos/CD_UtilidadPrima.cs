using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_UtilidadPrima
    {
        public List<eReportUtilidadBruta> sp_Report_UtilidadPrimaDocumentoDetalle(eReportUtilidadBruta Pms)
        {
            List<eReportUtilidadBruta> lst = new List<eReportUtilidadBruta>();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Pms.Conexion);

                string[] Parametros = {
                    //"@PageNumber" , "@PageSize",
                    "@id_emp",
                    "@id_cd",
                    "@cd_tipo",
                    "@month",
                    "@year","@insert"
                };

                object[] Valores = {
                    //Pms.PageNumber, Pms.PageSize
                    Pms.Id_Emp,
                    Pms.Id_Cd,
                    4,
                    Pms.Mes,
                    Pms.Anio,0
                };

                //SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRepCumplimientoVI_Dinamico_v2", ref dr, Parametros, Valores);                                                                 
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("sp_Report_UtilidadPrimaDocumentoDetalle", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    eReportUtilidadBruta obj = new eReportUtilidadBruta();
                    obj.Cd_Tipo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cd_Tipo")));
                    obj.Id_Tipo_Documento = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Tipo_Documento")));
                    obj.Tipo_Documento = Convert.ToString(dr.GetValue(dr.GetOrdinal("Tipo_Documento")));
                    obj.Id_Factura = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Factura")));
                    obj.Sucursal = Convert.ToString(dr.GetValue(dr.GetOrdinal("Sucursal")));
                    obj.Mes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Mes")));
                    obj.Anio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Anio")));
                    obj.CalculadoCon = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("CalculadoCon"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("CalculadoCon")));
                    obj.Venta = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Venta"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Venta")));
                    obj.Costo = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Costo"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Costo")));
                    obj.UtilidadBruta = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("UtilidadBruta"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("UtilidadBruta")));
                    obj.PorcUBReal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("PorcUBReal"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("PorcUBReal")));
                    obj.PorcUBPlaneada = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("PorcUBPlaneada"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("PorcUBPlaneada")));
                    obj.VarianzaUBrutaPuntos = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("VarianzaUBrutaPuntos"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("VarianzaUBrutaPuntos")));
                    obj.ImpactoPesos = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("ImpactoPesos"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("ImpactoPesos")));
                    obj.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    obj.Prd_Nombre = Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_Nombre")));
                    lst.Add(obj);
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                //throw ex;
                lst = null;
            }
            return lst;
        }

        public List<eReportUtilidadVenta> sp_Report_UtilidadVentaDocumentoDetalle(eReportUtilidadVenta Pms)
        {
            List<eReportUtilidadVenta> lst = new List<eReportUtilidadVenta>();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Pms.Conexion);

                string[] Parametros = {
                    //"@PageNumber" , "@PageSize",
                    "@id_emp",
                    "@id_cd",
                    "@cd_tipo",
                    "@month",
                    "@year","@insert"
                };

                object[] Valores = {
                    //Pms.PageNumber, Pms.PageSize
                    Pms.Id_Emp,
                    Pms.Id_Cd,
                    4,
                    Pms.Mes,
                    Pms.Anio,0
                };

                CentroDistribucion Cd = new CentroDistribucion();
                CD_CatCentroDistribucion cd = new CD_CatCentroDistribucion();


                cd.ConsultaCentroDistribucion(ref Cd, Pms.Id_Cd, Pms.Id_Emp, Pms.Conexion);

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("sp_Report_UtilidadVentaDocumentoDetalle", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    eReportUtilidadVenta obj = new eReportUtilidadVenta();
                    //obj.Cd_Tipo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cd_Tipo")));
                    //obj.Id_Tipo_Documento = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Tipo_Documento")));
                    obj.Tipo_Documento = Convert.ToString(dr.GetValue(dr.GetOrdinal("Tipo_Documento")));
                    obj.Id_Factura = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Factura")));
                    obj.Sucursal = Convert.ToString(dr.GetValue(dr.GetOrdinal("Sucursal")));
                    obj.Mes = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Mes")));
                    obj.Anio = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Anio")));

                    obj.Id_Prd = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    obj.Prd_Nombre = Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_Nombre")));

                    obj.Tipo = Convert.ToString(dr.GetValue(dr.GetOrdinal("Tipo_Documento")));
                    obj.UUID = Convert.ToString(dr.GetValue(dr.GetOrdinal("UUID")));
                    obj.NumeroDocumento = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Factura")));
                    obj.Id_Cliente = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Id_Cliente")));
                    obj.Cliente = Convert.ToString(dr.GetValue(dr.GetOrdinal("Cliente")));
                    obj.Cancelado = Convert.ToString(dr.GetValue(dr.GetOrdinal("Cancelado")));
                    obj.Grupo = Convert.ToString(dr.GetValue(dr.GetOrdinal("Grupo")));
                    obj.EstatusDoc = Convert.ToString(dr.GetValue(dr.GetOrdinal("EstatusDoc")));
                    obj.FechaContabilizacion = dr.IsDBNull(dr.GetOrdinal("FechaContabilizacion")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("FechaContabilizacion"))) == "01/01/1900 12:00:00 a. m." ? "" : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaContabilizacion"))).ToString("g");

                    obj.TotalDoc = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("TotalDoc"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("TotalDoc")));
                    obj.ImpuestoTotal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("ImpuestoTotal"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("ImpuestoTotal")));
                    obj.SubTotal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("SubTotal"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("SubTotal")));

                    obj.ItemCode = Convert.ToString(dr.GetValue(dr.GetOrdinal("ItemCode")));
                    obj.Dscription = Convert.ToString(dr.GetValue(dr.GetOrdinal("Dscription")));
                    obj.Quantity = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Quantity")));
                    obj.Price = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("Price"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Price")));
                    obj.LineTotal = Convert.IsDBNull(dr.GetValue(dr.GetOrdinal("LineTotal"))) ? 0 : Convert.ToDouble(dr.GetValue(dr.GetOrdinal("LineTotal")));
                    obj.Fac_CteRfc = Convert.ToString(dr.GetValue(dr.GetOrdinal("Fac_CteRfc")));
                    obj.FechaCan = dr.IsDBNull(dr.GetOrdinal("FechaCan")) ? "" : Convert.ToString(dr.GetValue(dr.GetOrdinal("FechaCan"))) == "01/01/1900 12:00:00 a. m." ? "" : Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaCan"))).ToString("g");

                    obj.Id_Terr = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("id_ter")));
                    obj.Territorio = Convert.ToString(dr.GetValue(dr.GetOrdinal("ter_nombre")));

                    if (obj.Tipo_Documento == "Nota Credito")
                    {
                        obj.TotalDoc = obj.TotalDoc > 0 ? obj.TotalDoc * -1 : 0;
                        obj.ImpuestoTotal = obj.ImpuestoTotal > 0 ? obj.ImpuestoTotal * -1 : 0;
                        obj.SubTotal = obj.SubTotal > 0 ? obj.SubTotal * -1 : 0;
                        obj.Price = obj.Price > 0 ? obj.Price * -1 : 0;
                        obj.LineTotal = obj.LineTotal > 0 ? obj.LineTotal * -1 : 0;
                    }

                    /*
                    if (obj.UUID != null)
                    {
                        if (obj.UUID != "" && obj.Fac_CteRfc != "")
                        {
                            ConsulltaEstatusCFDISAT.ServicioConsultaCFDI consultaCFDI = new ConsulltaEstatusCFDISAT.ServicioConsultaCFDI();
                            try
                            {
                                obj.Cancelado = consultaCFDI.ServicioConsultaCFDI_Consulta("?re=" + Cd.Cd_Rfc + "&rr=" + obj.Fac_CteRfc.Trim() + "&tt=" + (obj.SubTotal + obj.ImpuestoTotal).ToString("0.00") + "&id=" + obj.UUID.Trim());
                            }
                            catch (Exception e)
                            {

                            }

                        }
                        else
                        {
                            obj.Cancelado = "";
                        }
                    }*/

                    lst.Add(obj);
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                //throw ex;
                lst = null;
            }
            return lst;
        }
    }
}