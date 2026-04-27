using ClosedXML.Excel;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace SIANWEB.Utilerias
{
    public class ExportaExcel
    {
        public void ServicioExportaExcelClosedXML(XLWorkbook woooorkbook, HttpResponse httpResponse, string rutaguardado, string Outgoingfile)
        {
            try
            {
                /// string rutaguardado = Server.MapPath("~/Reportes/") + Outgoingfile;

                if (File.Exists(rutaguardado))
                {
                    File.Delete(rutaguardado);
                }
                
                /// woooorkbook.SaveAs(rutaguardado);

                woooorkbook.SaveAs(rutaguardado);

                // Prepare the response
                //  HttpResponse httpResponse = Response;
                httpResponse.Clear();
                httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                httpResponse.AddHeader("Content-Disposition", "attachment; filename=" + Outgoingfile);

                // Flush the workbook to the Response.OutputStream
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    woooorkbook.SaveAs(memoryStream);
                    memoryStream.WriteTo(httpResponse.OutputStream);
                    memoryStream.Close();
                }
                httpResponse.End();
            }
            catch (Exception exc) { }
            finally
            {
                try
                {
                    //stop processing the script and return the current result
                    HttpContext.Current.Response.End();
                }
                catch (Exception ex) { }
                finally
                {
                    //Sends the response buffer
                    HttpContext.Current.Response.Flush();
                    // Prevents any other content from being sent to the browser
                    HttpContext.Current.Response.SuppressContent = true;
                    //Directs the thread to finish, bypassing additional processing
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                    //Suspends the current thread
                    System.Threading.Thread.Sleep(1);
                }
            }
        }

        public void ServicioExportaExcelSyncF(IWorkbook Libro, HttpResponse RRResponse, string xfilenaame)
        {
            try
            {
                /// Server.MapPath("~/Reportes/") +
                //  string xfilenaame = "ReporteCRMImpulsosQuimicos_" + DateTime.Now.ToShortDateString().Replace("/", "") + "h" + DateTime.Now.ToShortTimeString().Replace(".", "").Replace(":", "").Replace(" ", "") + ".xlsx";
                Libro.SaveAs(xfilenaame, RRResponse, ExcelDownloadType.Open, ExcelHttpContentType.Excel2016);
            }
            catch (Exception ex)
            {
                throw ex;
                RRResponse.End();
                /*
                HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.  
                */
            }
        }

        public bool ServicioGrabaArchivo(XLWorkbook woooorkbook, string RutaArchivo)
        {
            bool salida = false;
            try
            {
                if (File.Exists(RutaArchivo))
                {
                    File.Delete(RutaArchivo);
                }
                woooorkbook.SaveAs(RutaArchivo);
                salida = true;
                return salida;
            }
            catch (Exception exc)
            {
                return salida;
            }
        }

        public void ServicioBajaArchivo(string rutaarchivo, string xFullFileNaame)
        {
            try
            {
                WebClient req = new WebClient();
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();

                response.Buffer = true;
                response.AddHeader("Content-Disposition", "attachment;filename=\"" + xFullFileNaame + "\"");
                byte[] data = req.DownloadData(rutaarchivo + xFullFileNaame);
                response.BinaryWrite(data);
                response.End();

                /*
                response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                response.AddHeader("Content-Disposition", "attachment;filename=\"" + xFullFileNaame + "\"");
                response.TransmitFile(xFullFileNaame);
                response.Flush();
                */
            }
            catch (Exception exc)
            {
                ////    return salida;
            }
        }


        #region ExcelSF

        public void CeldaHeaderSF(string rango, string etiquetea, ref IWorksheet wsss)
        {
            wsss.Range[rango].Text = etiquetea;
            wsss.Range[rango].CellStyle.Font.Bold = true;
            wsss.Range[rango].CellStyle.Font.Color = ExcelKnownColors.White;

            wsss.Range[rango].CellStyle.Color = Color.FromArgb(51, 133, 255); ///    ("#3385ff");
            wsss.Range[rango].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
            wsss.Range[rango].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
            /// wsss.Range(rango).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
            //Apply borders
            wsss.Range[rango].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Medium;
            wsss.Range[rango].CellStyle.Borders[ExcelBordersIndex.EdgeTop].Color = ExcelKnownColors.Black;
            wsss.Range[rango].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Medium;
            wsss.Range[rango].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].Color = ExcelKnownColors.Black;
            wsss.Range[rango].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Medium;
            wsss.Range[rango].CellStyle.Borders[ExcelBordersIndex.EdgeRight].Color = ExcelKnownColors.Black;
            wsss.Range[rango].CellStyle.Borders[ExcelBordersIndex.EdgeLeft].LineStyle = ExcelLineStyle.Medium;
            wsss.Range[rango].CellStyle.Borders[ExcelBordersIndex.EdgeLeft].Color = ExcelKnownColors.Black;
        }

        public void CeldaHeaderAgrupadorSF(string rango, string etiquetea, ref IWorksheet wsss)
        {
            wsss.Range[rango].Text = etiquetea;
            wsss.Range[rango].Merge();
            wsss.Range[rango].CellStyle.Font.Bold = true;
            wsss.Range[rango].CellStyle.Font.Color = ExcelKnownColors.White;
            wsss.Range[rango].CellStyle.Color = Color.FromArgb(51, 133, 255); ///    ("#3385ff");
            wsss.Range[rango].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
            wsss.Range[rango].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;

            //Apply borders
            wsss.Range[rango].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Medium;
            wsss.Range[rango].CellStyle.Borders[ExcelBordersIndex.EdgeTop].Color = ExcelKnownColors.Black;
            wsss.Range[rango].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Medium;
            wsss.Range[rango].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].Color = ExcelKnownColors.Black;
            wsss.Range[rango].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Medium;
            wsss.Range[rango].CellStyle.Borders[ExcelBordersIndex.EdgeRight].Color = ExcelKnownColors.Black;
            wsss.Range[rango].CellStyle.Borders[ExcelBordersIndex.EdgeLeft].LineStyle = ExcelLineStyle.Medium;
            wsss.Range[rango].CellStyle.Borders[ExcelBordersIndex.EdgeLeft].Color = ExcelKnownColors.Black;
        }

        public void CeldaDatosSF(string rango, string etiquetea, ExcelHAlign HAlign, bool isBold, bool isItalic, ref IWorksheet wsss)
        {
            wsss.Range[rango].Text = etiquetea;
            wsss.Range[rango].CellStyle.HorizontalAlignment = HAlign;
            wsss.Range[rango].CellStyle.VerticalAlignment = ExcelVAlign.VAlignTop;
            wsss.Range[rango].CellStyle.Font.Bold = isBold;
            wsss.Range[rango].CellStyle.Font.Italic = isItalic;
            //Apply borders
            wsss.Range[rango].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
            wsss.Range[rango].CellStyle.Borders[ExcelBordersIndex.EdgeTop].Color = ExcelKnownColors.Black;
            wsss.Range[rango].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Medium;
            wsss.Range[rango].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].Color = ExcelKnownColors.Black;
            wsss.Range[rango].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Medium;
            wsss.Range[rango].CellStyle.Borders[ExcelBordersIndex.EdgeRight].Color = ExcelKnownColors.Black;
            wsss.Range[rango].CellStyle.Borders[ExcelBordersIndex.EdgeLeft].LineStyle = ExcelLineStyle.Medium;
            wsss.Range[rango].CellStyle.Borders[ExcelBordersIndex.EdgeLeft].Color = ExcelKnownColors.Black;
        }

        public void CeldaDatoIntSF(string rango, int intNumero, ExcelHAlign HAlign, bool isBold, bool isItalic, ref IWorksheet wsss)
        {
            wsss.Range[rango].Number = intNumero;
            wsss.Range[rango].CellStyle.HorizontalAlignment = HAlign;
            wsss.Range[rango].CellStyle.Font.Bold = isBold;
            wsss.Range[rango].CellStyle.Font.Italic = isItalic;
            //Apply borders
            wsss.Range[rango].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
            wsss.Range[rango].CellStyle.Borders[ExcelBordersIndex.EdgeTop].Color = ExcelKnownColors.Black;
            wsss.Range[rango].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Medium;
            wsss.Range[rango].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].Color = ExcelKnownColors.Black;
            wsss.Range[rango].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Medium;
            wsss.Range[rango].CellStyle.Borders[ExcelBordersIndex.EdgeRight].Color = ExcelKnownColors.Black;
            wsss.Range[rango].CellStyle.Borders[ExcelBordersIndex.EdgeLeft].LineStyle = ExcelLineStyle.Medium;
            wsss.Range[rango].CellStyle.Borders[ExcelBordersIndex.EdgeLeft].Color = ExcelKnownColors.Black;

        }

        public void CeldaDatosFloatSF(string rango, float FloatNumero, ExcelHAlign HAlign, bool isBold, bool isItalic, ref IWorksheet wsss)
        {
            wsss.Range[rango].Number = FloatNumero;
            wsss.Range[rango].NumberFormat = "0.00";
            wsss.Range[rango].CellStyle.NumberFormat = "0.00";
            wsss.Range[rango].CellStyle.HorizontalAlignment = HAlign;
            wsss.Range[rango].CellStyle.Font.Bold = isBold;
            wsss.Range[rango].CellStyle.Font.Italic = isItalic;
            //Apply borders
            wsss.Range[rango].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
            wsss.Range[rango].CellStyle.Borders[ExcelBordersIndex.EdgeTop].Color = ExcelKnownColors.Black;
            wsss.Range[rango].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Medium;
            wsss.Range[rango].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].Color = ExcelKnownColors.Black;
            wsss.Range[rango].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Medium;
            wsss.Range[rango].CellStyle.Borders[ExcelBordersIndex.EdgeRight].Color = ExcelKnownColors.Black;
            wsss.Range[rango].CellStyle.Borders[ExcelBordersIndex.EdgeLeft].LineStyle = ExcelLineStyle.Medium;
            wsss.Range[rango].CellStyle.Borders[ExcelBordersIndex.EdgeLeft].Color = ExcelKnownColors.Black;
        }

        #endregion ExcelSF


        #region ExcelOld

        public void CeldaHeader(string rango, string etiquetea, ref IXLWorksheet wsss)
        {
            wsss.Range(rango).SetValue(etiquetea);
            wsss.Range(rango).Style.Font.Bold = true;
            wsss.Range(rango).Style.Font.FontColor = XLColor.White;
            wsss.Range(rango).Style.Fill.BackgroundColor = XLColor.FromHtml("#3385ff");
            wsss.Range(rango).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            wsss.Range(rango).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            wsss.Range(rango).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
        }

        public void CeldaHeaderAgrupador(string rango, string etiquetea, ref IXLWorksheet wsss)
        {
            wsss.Range(rango).SetValue(etiquetea);
            wsss.Range(rango).Merge();
            wsss.Range(rango).Style.Font.Bold = true;
            wsss.Range(rango).Style.Font.FontColor = XLColor.White;
            wsss.Range(rango).Style.Fill.BackgroundColor = XLColor.FromHtml("#3385ff");
            wsss.Range(rango).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            wsss.Range(rango).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            wsss.Range(rango).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
        }

        public void CeldaDatos(string rango, string etiquetea, XLAlignmentHorizontalValues HAlign, ref IXLWorksheet wsss)
        {
            wsss.Cell(rango).SetValue(etiquetea);
            wsss.Cell(rango).Style.Alignment.Horizontal = HAlign;
            wsss.Cell(rango).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
            wsss.Cell(rango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        }

        public void CeldaDatosBold(string rango, string etiquetea, XLAlignmentHorizontalValues HAlign, ref IXLWorksheet wsss)
        {
            wsss.Range(rango).SetValue(etiquetea);
            wsss.Range(rango).Merge();
            wsss.Range(rango).Style.Font.Bold = true;
            wsss.Range(rango).Style.Alignment.Horizontal = HAlign;
            wsss.Range(rango).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
            wsss.Range(rango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        }

        public void CeldaDatosItalic(string rango, string etiquetea, XLAlignmentHorizontalValues HAlign, ref IXLWorksheet wsss)
        {
            wsss.Range(rango).SetValue(etiquetea);
            wsss.Range(rango).Merge();
            wsss.Range(rango).Style.Font.Italic = true;
            wsss.Range(rango).Style.Alignment.Horizontal = HAlign;
            wsss.Range(rango).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
            wsss.Range(rango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        }

        public void CeldaDatoInt(string rango, int intNumero, XLAlignmentHorizontalValues HAlign, bool isBold, bool isItalic, ref IXLWorksheet wsss)
        {

            wsss.Cell(rango).Value = intNumero;
            wsss.Cell(rango).Style.Font.Bold = isBold;
            wsss.Cell(rango).Style.Font.Italic = isItalic;
            wsss.Cell(rango).Style.Alignment.Horizontal = HAlign;
            wsss.Cell(rango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

        }

        public void CeldaDatosFloat(string rango, float FloatNumero, XLAlignmentHorizontalValues HAlign, bool isBold, bool isItalic, ref IXLWorksheet wsss)
        {

            wsss.Cell(rango).Value = FloatNumero;
            wsss.Cell(rango).Style.Font.Bold = isBold;
            wsss.Cell(rango).Style.Font.Italic = isItalic;
            wsss.Cell(rango).Style.NumberFormat.Format = @"[$$-en-US]#,##0.00_);[Red]([$$-en-US]#,##0.00)";     /// "$0.00";   ///  @"[$$-en-US]#,##0.00_);[Red]([$$-en-US]#,##0.00)";
            wsss.Cell(rango).DataType = XLDataType.Number; // Use XLDataType.Number in 2018 and after
            wsss.Cell(rango).Style.Alignment.Horizontal = HAlign;
            wsss.Cell(rango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

        }


        #endregion ExcelOld

    }
}