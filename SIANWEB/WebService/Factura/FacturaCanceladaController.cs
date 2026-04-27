using CapaEntidad;
using CapaNegocios;
using ClosedXML.Excel;
using DevExpress.XtraCharts.Native;
using DevExpress.XtraRichEdit.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace SIANWEB.WebService
{
    public class FacturaCanceladaController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage ConsultarFacturasCanceladas(string str1)
        {
            try
            {
                var lstTotalFacturaCancelada = new List<entFacturaCancelada>();
                var lstFacturaCancelada = new List<entFacturaCancelada>();
                // Se obtiene la cadena de conexión Emp_Cnx y el Id_Cd de la variable de sesion...
                string strEmpCnx = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
                int intIdCd = Sesion.Id_Cd;

                new CN_FacturaCancelada().ConsultarFacturasCanceladas(strEmpCnx, intIdCd, ref lstTotalFacturaCancelada, ref lstFacturaCancelada);

                return Request.CreateResponse(HttpStatusCode.OK, new { lstTotalFacturaCancelada, lstFacturaCancelada });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage DescargarReporte(string str1, string str2)
        {
            try
            {
                var lstTotalFacturaCancelada = new List<entFacturaCancelada>();
                var lstFacturaCancelada = new List<entFacturaCancelada>();
                string strEmpCnx = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
                int intIdCd = Sesion.Id_Cd;

                new CN_FacturaCancelada().ConsultarFacturasCanceladas(strEmpCnx, intIdCd, ref lstTotalFacturaCancelada, ref lstFacturaCancelada);

                using (var workbook = new ClosedXML.Excel.XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Facturas Canceladas");
                    worksheet.Cell(3, 1).Value = "RFC Receptor";
                    worksheet.Cell(3, 2).Value = "Razón Social";
                    worksheet.Cell(3, 3).Value = "Serie";
                    worksheet.Cell(3, 4).Value = "Folio";
                    worksheet.Cell(3, 5).Value = "Folio Fiscal";
                    worksheet.Cell(3, 6).Value = "Fecha Emisión";
                    worksheet.Cell(3, 7).Value = "Fecha Solicitud Cancelación";
                    worksheet.Cell(3, 8).Value = "Tipo Documento";
                    worksheet.Cell(3, 9).Value = "Estado SAT";
                    worksheet.Cell(3, 10).Value = "Subtotal";
                    worksheet.Cell(3, 11).Value = "IVA";
                    worksheet.Cell(3, 12).Value = "Total";
                    worksheet.Cell(3, 13).Value = "Folio Relacionado";
                    worksheet.Cell(3, 14).Value = "Serie Relacionada";
                    worksheet.Cell(3, 15).Value = "Folio Fiscal Relacionado";
                    worksheet.Cell(3, 16).Value = "Tipo Documento Relacionado";

                    worksheet.Range(3, 1, 3, 16).SetAutoFilter(true);
                    // Estilo para los encabezados
                    worksheet.Range(3, 1, 3, 16).Style
                        .Font.SetBold(true)
                        .Fill.SetBackgroundColor(XLColor.LightBlue);



                    // Llenar datos...
                    int row = 4;
                    // Siguiente paso el ordena la lista lstTotalFacturaCancelada descendente a la propiedad decTotal..
                    lstTotalFacturaCancelada = lstTotalFacturaCancelada.OrderByDescending(x => x.decTotal).ToList();
                    foreach (var item in lstTotalFacturaCancelada)
                    {
                        worksheet.Cell(row, 1).Value = item.strRfcReceptor;
                        worksheet.Cell(row, 2).Value = item.strRazonSocial;
                        worksheet.Cell(row, 3).Value = item.strSerie;
                        if (item.intFolio > 0)
                        {
                            worksheet.Cell(row, 4).Value = item.intFolio;
                        }

                        worksheet.Cell(row, 5).Value = item.strFolioFiscal;
                        worksheet.Cell(row, 6).Value = "";
                        worksheet.Cell(row, 7).Value = "";
                        worksheet.Cell(row, 8).Value = item.strTipoDocumento;
                        worksheet.Cell(row, 9).Value = item.strEstadoSAT;
                        worksheet.Cell(row, 10).Value = item.decSubtotal;
                        worksheet.Cell(row, 11).Value = item.decIVA;
                        worksheet.Cell(row, 12).Value = item.decTotal;
                        // Se agrega la propiedad intFolioRelacionado para que se llene en la hoja de excel
                        if (item.intFolioRelacionado > 0)
                        {
                            worksheet.Cell(row, 13).Value = item.intFolioRelacionado;
                        }

                        worksheet.Cell(row, 14).Value = item.strSerieRelacionada;
                        worksheet.Cell(row, 15).Value = item.strFolioFiscalRelacionado;
                        worksheet.Cell(row, 16).Value = item.strTipoDocumentoRelacionado;

                        worksheet.Cell(row, 10).Style.NumberFormat.Format = "$#,##0.00";
                        worksheet.Cell(row, 11).Style.NumberFormat.Format = "$#,##0.00";
                        worksheet.Cell(row, 12).Style.NumberFormat.Format = "$#,##0.00";
                        worksheet.Range(row, 1, row, 16).Style
                           .Font.SetBold(true)
                           .Fill.SetBackgroundColor(XLColor.LightGray);

                        row++;
                        // en el segundo foreach usa la lista lstFacturaCancelada con la condición de seleccionar las facturas que tengan el mismo strRFCReceptor del ítem de lstTotalFacturaCancelada los seleccionados llenan las siguientes líneas...
                        foreach (var item2 in lstFacturaCancelada.Where(x => x.strRfcReceptor == item.strRfcReceptor))
                        {
                            worksheet.Cell(row, 1).Value = item2.strRfcReceptor;
                            worksheet.Cell(row, 2).Value = item2.strRazonSocial;
                            worksheet.Cell(row, 3).Value = item2.strSerie;
                            worksheet.Cell(row, 4).Value = item2.intFolio;
                            worksheet.Cell(row, 5).Value = item2.strFolioFiscal;
                            worksheet.Cell(row, 6).Value = item2.dtFechaEmision;
                            worksheet.Cell(row, 7).Value = item2.dtFechaSolCanc;
                            worksheet.Cell(row, 8).Value = item2.strTipoDocumento;
                            worksheet.Cell(row, 9).Value = item2.strEstadoSAT;
                            worksheet.Cell(row, 10).Value = item2.decSubtotal;
                            worksheet.Cell(row, 11).Value = item2.decIVA;
                            worksheet.Cell(row, 12).Value = item2.decTotal;
                            // Se agrega la propiedad intFolioRelacionado para que se llene en la hoja de excel
                            worksheet.Cell(row, 13).Value = item2.intFolioRelacionado;
                            worksheet.Cell(row, 14).Value = item2.strSerieRelacionada;
                            worksheet.Cell(row, 15).Value = item2.strFolioFiscalRelacionado;
                            worksheet.Cell(row, 16).Value = item2.strTipoDocumentoRelacionado;

                            worksheet.Cell(row, 10).Style.NumberFormat.Format = "$#,##0.00";
                            worksheet.Cell(row, 11).Style.NumberFormat.Format = "$#,##0.00";
                            worksheet.Cell(row, 12).Style.NumberFormat.Format = "$#,##0.00";
                            row++;
                        }
                    }

                    worksheet.Range(3, 1, row, 16).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(3, 1, row, 16).Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                    // ajustar columnas...
                    worksheet.Columns().AdjustToContents();


                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var result = new HttpResponseMessage(HttpStatusCode.OK)
                        {
                            Content = new ByteArrayContent(stream.ToArray())
                        };
                        result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                        {
                            FileName = "FacturasCanceladas.xlsx"
                        };
                        result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        protected Sesion Sesion
        {
            get
            {
                if (HttpContext.Current.Session != null)
                {
                    return (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
                }
                return null;
            }
        }
    }
}