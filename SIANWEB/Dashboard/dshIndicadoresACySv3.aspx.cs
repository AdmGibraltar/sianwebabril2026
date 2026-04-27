using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ClosedXML.Excel;
using CapaEntidad;
using CapaNegocios;
using System.Data;
using System.IO;
using System.Reflection;
using Telerik.Web.UI;
using CapaDatos;
using DevExpress.Web;
using DevExpress.Web.Internal;

namespace SIANWEB.Dashboard
{
    public partial class dshIndicadoresACySv3 : System.Web.UI.Page
    {
        public static string strRIKS;
        public static string strDatoRIKS;
        public static string strURLBack = "";

        public static string NumCapturado = "0";
        public static string NumSolicitadoG = "0";
        public static string NumSolicitadoJ = "0";
        public static string NumAutorizado = "0";
        public static string NumOtro = "0";

        public static string lblTotalGeneral = string.Empty;
        public static string lblCapturavsAutorizado = string.Empty;
        public static string lbltotalVentaInstalada = string.Empty;
        public static string lblCumplimientoACySAutorizados = string.Empty;
        public static string lblCumplimientoACySVenta = string.Empty;

        public static string lblNumCapturado = "0";
        public static string lblNumSolicitadoG = "0";
        public static string lblNumSolicitadoJ = "0";
        public static string lblNumAutorizado = "0";
        public static string lblNumOtro = "0";

        public static List<DashboardACyS_DetalleRIKS> ExcelDeRiks;
        public static List<Renglon> lstRiks;

        public static List<DashboardACyS_RIKS> ExcelBaseDeRiks;
        public static List<DashboardACyS_Resumen> ExcelResumen;
        public static List<DashboardACyS_Clientes> ExcelClientesCon;
        public static List<DashboardACyS_Clientes> ExcelClientesSin;
        public static List<DashboardACyS_DetalleACyS> ExcelDetalleACyS;
        public static List<DashboardACyS_RIKS> ListadoDeRIKs;
        public static List<DashboardACyS_Estatus> ExcelDeEstatus;

        public static List<DashboardACyS_Resumen> ExcelResumen_DtlRiks;
        public static List<DashboardACyS_RIKS> ExcelBaseDeRiks_DtlRiks;
        public static List<DashboardACyS_Clientes> ExcelClientesCon_DtlRiks;
        public static List<DashboardACyS_Clientes> ExcelClientesSin_DtlRiks;
        public static List<DashboardACyS_DetalleACyS> ExcelDetalleACyS_DtlRiks;

        protected void Page_Load(object sender, EventArgs e)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (Sesion == null)
            {
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                Response.Redirect("../Login.aspx", false);
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    CargarCentros();
                    llenaDashboard();
                    LlenarListaRIKs(ExcelDeRiks);
                }
                else
                {
                    //es evento
                }

            }
            Session["activeMenu"] = 6;
        }


        #region funciones

        void CargarCentros()
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_dshIndicadoresACyS clsdshIndicadoresACyS = new CN_dshIndicadoresACyS();
                List<Renglon> lista = new List<Renglon>();

                clsdshIndicadoresACyS.LlenaRenglones(sesion.Id_Emp, sesion.Id_Cd, sesion.Id_U, "spCatCentroDistribucion_Combo", sesion.Emp_Cnx,
                    ref lista);
                this.drpCDI.DataValueField = "idRng";
                this.drpCDI.DataTextField = "sValor";
                this.drpCDI.DataSource = lista.OrderBy(i => i.sValor);
                this.drpCDI.DataBind();

                this.drpCDI.SelectedValue = sesion.Id_Cd_Ver.ToString();
                if (lista.Count == 1)
                {
                    this.drpCDI.Enabled = false;
                    parrCDI.InnerText = "CDI: " + drpCDI.SelectedItem.Text + "";
                    drpCDI.Visible = false;
                }

                // JorgeRmzF [06-10-22]: filtros año y mes
                DateTime dateCurrent = DateTime.Now;
                int intYearInicial = 2022;
                for (int i = intYearInicial, intIndex = 0; i <= dateCurrent.Year; i++, intIndex++)
                {
                    this.drpAnio.Items.Insert(intIndex, new ListItem(i.ToString(), i.ToString()));
                }
                this.drpAnio.SelectedValue = dateCurrent.Year.ToString();


                //his.drpAnio.Text = dateCurrent.Year.ToString();

                this.drpMes.Items.Insert(0, new ListItem("Enero", "1"));
                this.drpMes.Items.Insert(0, new ListItem("Febrero", "2"));
                this.drpMes.Items.Insert(0, new ListItem("Marzo", "3"));
                this.drpMes.Items.Insert(0, new ListItem("Abril", "4"));
                this.drpMes.Items.Insert(0, new ListItem("Mayo", "5"));
                this.drpMes.Items.Insert(0, new ListItem("Junio", "6"));
                this.drpMes.Items.Insert(0, new ListItem("Julio", "7"));
                this.drpMes.Items.Insert(0, new ListItem("Agosto", "8"));
                this.drpMes.Items.Insert(0, new ListItem("Septiembre", "9"));
                this.drpMes.Items.Insert(0, new ListItem("Octubre", "10"));
                this.drpMes.Items.Insert(0, new ListItem("Noviembre", "11"));
                this.drpMes.Items.Insert(0, new ListItem("Diciembre", "12"));

                this.drpMes.SelectedValue = dateCurrent.Month.ToString();
                //this.drpMes.DataBind();

            }
            catch (Exception ex)
            {
                //throw ex;
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        void LlenarListaRIKs(List<DashboardACyS_DetalleRIKS> ListDetaleRIKs)
        {
            try
            {
                List<Renglon> listaRiks = new List<Renglon>();
                List<Renglon> listaRiksOrden = new List<Renglon>();
                //lstRiks = new List<Renglon>();

                foreach (var itemDetalleACyS in ListDetaleRIKs)
                {
                    if (!listaRiks.Any(x => x.sValor == itemDetalleACyS.Id_Rik.ToString()))
                    {
                        listaRiks.Add(new Renglon
                        {
                            sValor = itemDetalleACyS.Id_Rik.ToString(),
                            sDescripcion = itemDetalleACyS.Rik_Nombre
                        });
                    }

                }

                listaRiksOrden.Add(new Renglon
                {
                    sValor = "-1",
                    sDescripcion = " -- TODOS -- "
                });

                listaRiksOrden.AddRange(listaRiks.OrderBy(x => x.sDescripcion).ToList());

                CargarRIKs(listaRiksOrden);

            }
            catch (Exception ex)
            {
                //throw ex;
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        void CargarRIKs(List<Renglon> listaRiks)
        {
            try
            {

                drpGridRiks2.DataValueField = "sValor";
                drpGridRiks2.DataTextField = "sDescripcion";
                drpGridRiks2.DataSource = listaRiks;
                drpGridRiks2.DataBind();

                drpGridRiks2.SelectedValue = "-1";
            }
            catch (Exception ex)
            {
                //throw ex;
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        #endregion


        #region LlenarDashboard

        public void llenaDashboard()
        {
            try
            {
                List<DashboardACyS_Resumen> ListResumen = new List<DashboardACyS_Resumen>();
                List<DashboardACyS_Estatus> ListEstatus = new List<DashboardACyS_Estatus>();
                List<DashboardACyS_RIKS> ListRIKs = new List<DashboardACyS_RIKS>();
                List<DashboardACyS_DetalleRIKS> ListDetaleRIKs = new List<DashboardACyS_DetalleRIKS>();
                List<DashboardACyS_DetalleRIKS> ListDetaleRIKsOriginal = new List<DashboardACyS_DetalleRIKS>();

                List<DashboardACyS_Clientes> ClientesCon = new List<DashboardACyS_Clientes>();
                List<DashboardACyS_Clientes> ClientesSin = new List<DashboardACyS_Clientes>();
                List<DashboardACyS_DetalleACyS> DetalleAcyS = new List<DashboardACyS_DetalleACyS>();

                /// Obtiene Datos
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_dshIndicadoresACyS clsdshIndicadoresACyS = new CN_dshIndicadoresACyS();

                int iRik;
                string siRik = "";

                siRik = (drpGridRiks2.SelectedValue == "-1") ? "" : drpGridRiks2.SelectedValue;
                iRik = (string.IsNullOrEmpty(siRik)) ? -1 : 1;

                string sEstatus = this.drpEstatus.SelectedItem.Value;
                string strAnio = this.drpAnio.SelectedItem.Value;
                string strMes = this.drpMes.SelectedItem.Value;

                // IMPORTRANTE: validar año
                int intAnio = int.Parse(strAnio);
                int intAnioActual = DateTime.Now.Year;
                string strAnioHistorico = "2022"; //año inicia el historico de consulta
                int intMes = int.Parse(strMes);
                int intMesHistorico = 6;

                if (strAnio == strAnioHistorico)
                {
                    if (intMes < intMesHistorico)
                    {
                        this.drpMes.SelectedValue = intMesHistorico.ToString();
                        strMes = this.drpMes.SelectedItem.Value;
                        intMes = int.Parse(strMes);
                    }
                }

                if (intMes == 1)
                {
                    intMes = 12;
                    intAnio -= 1;
                }
                else
                {
                    intMes -= 1;
                }

                clsdshIndicadoresACyS.ConsultaDashboardACyS_v2(sesion.Id_Emp, sesion.Id_Cd, sesion.Id_U, iRik, siRik, sEstatus, intAnio.ToString(), intMes.ToString(), sesion.Emp_Cnx,
                    ref ListResumen, ref ListEstatus, ref ListRIKs, ref ListDetaleRIKs,
                    ref ClientesCon, ref ClientesSin, ref DetalleAcyS);
                GeneraResumen(ListResumen);
                LlenaGauges(ListEstatus);
                LlenaGraph(ListRIKs);

                ListadoDeRIKs = new List<DashboardACyS_RIKS>();
                ListadoDeRIKs = ListRIKs;

                ExcelDeEstatus = new List<DashboardACyS_Estatus>();
                ExcelDeEstatus = ListEstatus;

                ExcelDeRiks = new List<DashboardACyS_DetalleRIKS>();
                ExcelDeRiks = ListDetaleRIKs;

                ExcelResumen = new List<DashboardACyS_Resumen>();
                ExcelResumen = ListResumen;

                ExcelClientesCon = new List<DashboardACyS_Clientes>();
                ExcelClientesCon = ClientesCon;

                ExcelClientesSin = new List<DashboardACyS_Clientes>();
                ExcelClientesSin = ClientesSin;

                ExcelDetalleACyS = new List<DashboardACyS_DetalleACyS>();
                ExcelDetalleACyS = DetalleAcyS;


            }
            catch (Exception ex)
            {
                this.ErrorManager(ex, "llenaDashboard");
            }
        }

        void GeneraResumen(List<DashboardACyS_Resumen> ListaResumen)
        {
            lblTotalGeneral = string.Empty;
            lblCapturavsAutorizado = string.Empty;
            lblCumplimientoACySAutorizados = string.Empty;
            lbltotalVentaInstalada = string.Empty;
            lblCumplimientoACySVenta = string.Empty;

            foreach (DashboardACyS_Resumen rng in ListaResumen)
            {
                if (rng.Ordern == 1)
                {
                    lblTotalGeneral = rng.sValor;
                }
                if (rng.Ordern == 2)
                {
                    lblCapturavsAutorizado = rng.sValor;
                }
                if (rng.Ordern == 3)
                {
                    lblCumplimientoACySAutorizados = rng.sValor;
                }
                if (rng.Ordern == 4)
                {
                    lbltotalVentaInstalada = rng.sValor;
                }
                if (rng.Ordern == 5)
                {
                    lblCumplimientoACySVenta = rng.sValor;
                }
            }

        }

        void LlenaGauges(List<DashboardACyS_Estatus> ListaEstatus)
        {
            NumCapturado = "0";
            NumSolicitadoG = "0";
            NumSolicitadoJ = "0";
            NumAutorizado = "0";
            NumOtro = "0";
            int Total = 0;
            lblNumCapturado = "0";
            lblNumSolicitadoG = "0";
            lblNumSolicitadoJ = "0";
            lblNumAutorizado = "0";
            lblNumOtro = "0";

            foreach (DashboardACyS_Estatus rngg in ListaEstatus)
            {
                Total = Total + rngg.NACySValor;
                /*
                 * 'Autorizado'
                 * 'Cancelado'
                 * 'Capturado'
                 * 'Solicitado
                */
                if (rngg.AcsEstatusTexto == "Autorizado")
                {
                    NumAutorizado = rngg.NACySValor.ToString();
                    lblNumAutorizado = rngg.NACySValor.ToString();
                }

                if ((rngg.AcsEstatusTexto == "Cancelado") || (rngg.AcsEstatusTexto == "Solicitado"))
                {
                    NumOtro = rngg.NACySValor.ToString();
                    lblNumOtro = rngg.NACySValor.ToString();
                }

                if (rngg.AcsEstatusTexto == "Capturado")
                {
                    NumCapturado = rngg.NACySValor.ToString();
                    lblNumCapturado = rngg.NACySValor.ToString();
                }

                if (rngg.AcsEstatusTexto == "Solicitado G")
                {
                    NumSolicitadoG = rngg.NACySValor.ToString();
                    lblNumSolicitadoG = rngg.NACySValor.ToString();
                }

                if (rngg.AcsEstatusTexto == "Solicitado J")
                {
                    NumSolicitadoJ = rngg.NACySValor.ToString();
                    lblNumSolicitadoJ = rngg.NACySValor.ToString();
                }
            }
            /*
            NumCapturado = Math.Round((Convert.ToDouble(Convert.ToDouble(lblNumCapturado) / Convert.ToDouble(Total)) * 100), 0).ToString();
            NumSolicitadoG = Math.Round((Convert.ToDouble(Convert.ToDouble(lblNumSolicitadoG) / Convert.ToDouble(Total)) * 100), 0).ToString();
            NumSolicitadoJ = Math.Round((Convert.ToDouble(Convert.ToDouble(NumSolicitadoJ) / Convert.ToDouble(Total)) * 100), 0).ToString();
            NumAutorizado = Math.Round((Convert.ToDouble(Convert.ToDouble(lblNumAutorizado) / Convert.ToDouble(Total)) * 100), 0).ToString();
            NumOtro = Math.Round((Convert.ToDouble(Convert.ToDouble(lblNumOtro) / Convert.ToDouble(Total)) * 100), 0).ToString();
            */
            if (lblNumCapturado != "0")
            {
                NumCapturado = Math.Round((Convert.ToDouble(Convert.ToDouble(lblNumCapturado) / Convert.ToDouble(Total)) * 100), 0).ToString();
            }

            if (lblNumSolicitadoG != "0")
            {
                NumSolicitadoG = Math.Round((Convert.ToDouble(Convert.ToDouble(lblNumSolicitadoG) / Convert.ToDouble(Total)) * 100), 0).ToString();
            }

            if (lblNumSolicitadoJ != "0")
            {
                NumSolicitadoJ = Math.Round((Convert.ToDouble(Convert.ToDouble(NumSolicitadoJ) / Convert.ToDouble(Total)) * 100), 0).ToString();
            }

            if (lblNumAutorizado != "0")
            {
                NumAutorizado = Math.Round((Convert.ToDouble(Convert.ToDouble(lblNumAutorizado) / Convert.ToDouble(Total)) * 100), 0).ToString();
            }

            if (lblNumOtro != "0")
            {
                NumOtro = Math.Round((Convert.ToDouble(Convert.ToDouble(lblNumOtro) / Convert.ToDouble(Total)) * 100), 0).ToString();
            }

            lblNumAutorizado = lblNumAutorizado + "/ " + Total.ToString();
            lblNumOtro = lblNumOtro + "/ " + Total.ToString();
            lblNumCapturado = lblNumCapturado + "/ " + Total.ToString();
            lblNumSolicitadoJ = lblNumSolicitadoJ + "/ " + Total.ToString();
            lblNumSolicitadoG = lblNumSolicitadoG + "/ " + Total.ToString();
        }

        void LlenaGraph(List<DashboardACyS_RIKS> ListaRIKs)
        {
            strRIKS = "";
            strDatoRIKS = "";
            ListaRIKs = ListaRIKs.OrderBy(x => x.NACySValor).ToList();

            strDatoRIKS = string.Join(",", ListaRIKs.Select(x => x.NACySValor));
            strRIKS = string.Join(",", ListaRIKs.Select(x => string.Concat("\"", x.Rik_Nombre.Trim(), "\"")));

            string javaScript = "DrawTheChart(ChartData, ChartOptions, 'chart-01', 'HorizontalBar');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", javaScript, true);


        }

        private void ExportaAExcel()
        {
            int i = 0;
            string filenombre = "DetalleACySxRIK_" + DateTime.UtcNow.ToShortDateString().Replace("/", "") + "h" + DateTime.UtcNow.ToShortTimeString().Replace(".", "").Replace(":", "").Replace(" ", "");
            try
            {
                DataSet ds = new DataSet();
                ds = ToDataSet(ExcelDeRiks);

                ds.Tables[i].TableName = "DetalleACySDelRIK";

                using (XLWorkbook wb = new XLWorkbook())
                {
                    foreach (DataTable dt in ds.Tables)
                    {
                        wb.Worksheets.Add(dt);
                    }

                    //Export the Excel file.
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=" + filenombre + ".xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {
                this.ErrorManager(ex, "ExportaAExcel");
                throw ex;
            }
            finally
            {
            }
        }

        public DataSet ToDataSet<DashboardACyS_DetalleRIKS>(List<DashboardACyS_DetalleRIKS> items)
        {
            DataTable dataTable = new DataTable(typeof(DashboardACyS_DetalleRIKS).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(DashboardACyS_DetalleRIKS).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (DashboardACyS_DetalleRIKS item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dataTable);
            return ds;
        }

        void AvanceDetallado(string wsName, List<DashboardACyS_Resumen> ListaResumen,
            List<DashboardACyS_Clientes> ListaClientesCon, List<DashboardACyS_Clientes> ListaClientesSin,
            List<DashboardACyS_DetalleACyS> ListaDeDetalleACyS)
        {
            string filenombre = "ACySAvanceDetallado_" + DateTime.UtcNow.ToShortDateString().Replace("/", "") + "h" + DateTime.UtcNow.ToShortTimeString().Replace(".", "").Replace(":", "").Replace(" ", "");
            try
            {
                var wb = new XLWorkbook();

                var ws = wb.Worksheets.Add(wsName);
                FormatoAvanceDetallado("CDI: " + drpCDI.SelectedItem.Text + "", ListaResumen, ListaClientesCon, ListaClientesSin, ListaDeDetalleACyS, ref ws);

                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + filenombre + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                this.ErrorManager(ex, "AvanceDetallado");
                throw ex;
            }
        }

        void FormatoAvanceDetallado(string strTitulo, List<DashboardACyS_Resumen> ListaResumen,
            List<DashboardACyS_Clientes> ListaClientesCon, List<DashboardACyS_Clientes> ListaClientesSin,
            List<DashboardACyS_DetalleACyS> ListaDeDetalleACyS, ref IXLWorksheet wss)
        {
            int i = 0;
            try
            {
                #region Resumen

                wss.Range("A1:C1").Merge();
                wss.Range("A1").SetValue(strTitulo);
                wss.Cell("A1").Style.Font.Bold = true;

                wss.Range("H1:J1").Merge();
                wss.Range("H1").SetValue("Fecha Corte: " + DateTime.Now.ToString("dd/MM/yyyy"));
                wss.Range("H2:J2").Merge();
                wss.Range("H2").SetValue("Año consultado: " + this.drpAnio.SelectedValue);
                wss.Range("H3:J3").Merge();
                wss.Range("H3").SetValue("Mes consultado: " + this.drpMes.SelectedItem);

                wss.Range("B2:E2").Merge();
                foreach (DashboardACyS_Resumen rng in ListaResumen)
                {
                    wss.Range("B" + (i + 3) + ":D" + (i + 3)).Merge();
                    wss.Cell("B" + (i + 3)).Value = rng.Concepto.ToString();

                    if (rng.sValor.ToString().Contains("%"))
                    {
                        wss.Cell("E" + (i + 3)).Value = (float.Parse(rng.sValor.Replace("%", "")) / 100);
                        wss.Cell("E" + (i + 3)).Style.NumberFormat.Format = "0%";
                    }
                    else
                    {
                        wss.Cell("E" + (i + 3)).Value = rng.sValor;
                    }
                    i++;
                }

                // Formato
                wss.Range("E3:E7").Style.Font.Bold = true;
                wss.Range("E3:E7").Style.Font.FontSize = 14;
                wss.Range("E3:E7").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                #endregion

                #region Clientes8020

                // si se incluye segun el checkbox
                if (this.chkLista8020.Checked)
                {
                    i++;    // brinco un renglon
                    wss.Range("A" + (i + 3).ToString() + ":J" + (i + 3).ToString()).Merge();
                    wss.Range("A" + (i + 3).ToString()).SetValue("Clientes de Venta Instalada");
                    wss.Range("A" + (i + 3).ToString()).Style.Font.Bold = true;
                    wss.Range("A" + (i + 3).ToString()).Style.Font.FontColor = XLColor.White;
                    wss.Range("A" + (i + 3).ToString()).Style.Fill.BackgroundColor = XLColor.UltramarineBlue;
                    wss.Range("A" + (i + 3).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    wss.Range("A" + (i + 3).ToString() + ":J" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    i++;
                    wss.Range("A" + (i + 3).ToString() + ":E" + (i + 3).ToString()).Merge();
                    wss.Range("A" + (i + 3).ToString()).SetValue("Clientes CON ACyS Autorizado/Vigente");
                    wss.Range("A" + (i + 3).ToString()).Style.Font.Bold = true;
                    wss.Range("A" + (i + 3).ToString()).Style.Font.FontColor = XLColor.White;
                    wss.Range("A" + (i + 3).ToString()).Style.Fill.BackgroundColor = XLColor.UltramarineBlue;
                    wss.Range("A" + (i + 3).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    wss.Range("A" + (i + 3).ToString() + ":E" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    wss.Range("F" + (i + 3).ToString() + ":J" + (i + 3).ToString()).Merge();
                    wss.Range("F" + (i + 3).ToString()).SetValue("Clientes SIN ACyS Autorizado/Vigente");
                    wss.Range("F" + (i + 3).ToString()).Style.Font.Bold = true;
                    wss.Range("F" + (i + 3).ToString()).Style.Font.FontColor = XLColor.White;
                    wss.Range("F" + (i + 3).ToString()).Style.Fill.BackgroundColor = XLColor.UltramarineBlue;
                    wss.Range("F" + (i + 3).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    wss.Range("F" + (i + 3).ToString() + ":J" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    i++;    // brinco un renglon
                    string celditA = "";
                    int letra = 1;
                    int iCoon = i;
                    int iSiin = i;

                    foreach (DashboardACyS_Clientes rngCte in ListaClientesCon)
                    {
                        ///     ciclo de 5 columnas
                        if (letra == 1) { celditA = "A"; }
                        if (letra == 2) { celditA = "B"; }
                        if (letra == 3) { celditA = "C"; }
                        if (letra == 4) { celditA = "D"; }
                        if (letra == 5) { celditA = "E"; letra = 0; }

                        wss.Cell(celditA + (iCoon + 3)).Value = rngCte.iIdCliente.ToString();
                        wss.Cell(celditA + (iCoon + 3)).Style.Fill.BackgroundColor = XLColor.CadmiumYellow;
                        wss.Cell(celditA + (iCoon + 3)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wss.Cell(celditA + (iCoon + 3)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        if (letra == 0) { iCoon++; }
                        letra++;
                    }

                    letra = 1; celditA = "";
                    foreach (DashboardACyS_Clientes rngCte2 in ListaClientesSin)
                    {
                        ///     ciclo de 5 columnas
                        if (letra == 1) { celditA = "F"; }
                        if (letra == 2) { celditA = "G"; }
                        if (letra == 3) { celditA = "H"; }
                        if (letra == 4) { celditA = "I"; }
                        if (letra == 5) { celditA = "J"; letra = 0; }

                        wss.Cell(celditA + (iSiin + 3)).Value = rngCte2.iIdCliente.ToString();
                        wss.Cell(celditA + (iSiin + 3)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wss.Cell(celditA + (iSiin + 3)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        if (letra == 0) { iSiin++; }
                        letra++;
                    }

                    i = (iCoon >= iSiin) ? iCoon : iSiin;
                }

                i++;
                #endregion

                #region DetalleACyS

                // si se incluye segun el checkbox
                if (this.chkListaMatriz.Checked)
                {
                    i++;    // brinco un renglon
                    wss.Range("A" + (i + 3).ToString() + ":J" + (i + 3).ToString()).Merge();
                    wss.Range("A" + (i + 3).ToString()).SetValue("Matriz de ACyS vigentes total de clientes");
                    wss.Range("A" + (i + 3).ToString()).Style.Font.Bold = true;
                    wss.Range("A" + (i + 3).ToString()).Style.Font.FontColor = XLColor.White;
                    wss.Range("A" + (i + 3).ToString()).Style.Fill.BackgroundColor = XLColor.UltramarineBlue;
                    wss.Range("A" + (i + 3).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    wss.Range("A" + (i + 3).ToString() + ":J" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    i++;
                    wss.Cell("A" + (i + 3).ToString()).SetValue("Folio");
                    wss.Cell("A" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wss.Cell("B" + (i + 3).ToString()).SetValue("Estatus");
                    wss.Cell("B" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wss.Cell("C" + (i + 3).ToString()).SetValue("Num");
                    wss.Cell("C" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wss.Cell("D" + (i + 3).ToString()).SetValue("Cliente");
                    wss.Cell("D" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wss.Cell("E" + (i + 3).ToString()).SetValue("Terr.");
                    wss.Cell("E" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wss.Cell("F" + (i + 3).ToString()).SetValue("Rik");
                    wss.Cell("F" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wss.Cell("G" + (i + 3).ToString()).SetValue("Fecha");
                    wss.Cell("G" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wss.Cell("H" + (i + 3).ToString()).SetValue("Fecha Inicio");
                    wss.Cell("H" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wss.Cell("I" + (i + 3).ToString()).SetValue("Fecha Fin");
                    wss.Cell("I" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wss.Cell("J" + (i + 3).ToString()).SetValue("Vencido");
                    wss.Cell("J" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wss.Range("A" + (i + 3).ToString() + ":" + "J" + (i + 3).ToString()).Style.Font.Bold = true;
                    wss.Range("A" + (i + 3).ToString() + ":" + "J" + (i + 3).ToString()).Style.Font.Bold = true;
                    wss.Range("A" + (i + 3).ToString() + ":" + "J" + (i + 3).ToString()).Style.Font.FontColor = XLColor.White;
                    wss.Range("A" + (i + 3).ToString() + ":" + "J" + (i + 3).ToString()).Style.Fill.BackgroundColor = XLColor.UltramarineBlue;
                    wss.Range("A" + (i + 3).ToString() + ":" + "J" + (i + 3).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    i++;
                    //  Folio	Estatus	    NumCliente	Cliente     Territorio	RIK	FechaACyS	
                    //  FechaInicio	    FechaFin	TipoCuenta	Vencido
                    foreach (DashboardACyS_DetalleACyS Det in ListaDeDetalleACyS)
                    {
                        wss.Cell("A" + (i + 3).ToString()).SetValue(Det.Folio);
                        wss.Cell("A" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wss.Cell("B" + (i + 3).ToString()).SetValue(Det.Estatus);
                        wss.Cell("B" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wss.Cell("C" + (i + 3).ToString()).SetValue(Det.NumCliente);
                        wss.Cell("C" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wss.Cell("D" + (i + 3).ToString()).SetValue(Det.Cliente);
                        wss.Cell("D" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wss.Cell("E" + (i + 3).ToString()).SetValue(Det.Territorio);
                        wss.Cell("E" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wss.Cell("F" + (i + 3).ToString()).SetValue(Det.Rik);
                        wss.Cell("F" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wss.Cell("G" + (i + 3).ToString()).SetValue(Det.FechaACyS);
                        wss.Cell("G" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wss.Cell("H" + (i + 3).ToString()).SetValue(Det.FechaInicio);
                        wss.Cell("H" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wss.Cell("I" + (i + 3).ToString()).SetValue(Det.FechaFin);
                        wss.Cell("I" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wss.Cell("J" + (i + 3).ToString()).SetValue(Det.Vencido);
                        wss.Cell("J" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        i++;
                    }

                }

                #endregion

                wss.Columns().AdjustToContents();
            }
            catch (Exception ex)
            {
                this.ErrorManager(ex, "FormatoAvanceDetallado");
                throw ex;
            }
        }

        void CumplimientoCaptura(string wsName, List<DashboardACyS_RIKS> ListaRIKS, List<DashboardACyS_Resumen> ListaResumen,
            List<DashboardACyS_Estatus> ListaEstatus, List<DashboardACyS_DetalleACyS> ListaDeDetalleACyS)
        {
            string filenombre = "ACySCumplimientodDeCaptura_" + DateTime.UtcNow.ToShortDateString().Replace("/", "") + "h" + DateTime.UtcNow.ToShortTimeString().Replace(".", "").Replace(":", "").Replace(" ", "");
            try
            {
                var wb = new XLWorkbook();

                var ws = wb.Worksheets.Add(wsName);
                FormatoCumplimientoDeCaptura(ListaRIKS, ListaResumen, ListaEstatus, ListaDeDetalleACyS, ref ws);

                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + filenombre + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                this.ErrorManager(ex, "CumplimientoCaptura");
                throw ex;
            }
        }

        void FormatoCumplimientoDeCaptura(List<DashboardACyS_RIKS> ListaRIKS, List<DashboardACyS_Resumen> ListaResumen,
            List<DashboardACyS_Estatus> ListaEstatus, List<DashboardACyS_DetalleACyS> ListaDeDetalleACyS, ref IXLWorksheet wss)
        {
            int i = 0;
            try
            {
                #region Reporte

                wss.Range("A2:G2").Merge();
                wss.Range("A2").SetValue("Reporte de Cumplimiento en Captura de ACyS");
                wss.Range("A2:G2").Style.Font.Bold = true;
                wss.Range("A2:G2").Style.Font.FontSize = 14;
                wss.Range("A2:G2").Style.Font.FontColor = XLColor.White;
                wss.Range("A2:G2").Style.Fill.BackgroundColor = XLColor.LightSlateGray;

                wss.Range("A3:G3").Merge();
                wss.Range("A3").SetValue("al corte: " + DateTime.Now.ToString("dd/MM/yyyy"));
                wss.Range("A3:G3").Style.Fill.BackgroundColor = XLColor.LightSlateGray;

                wss.Range("H2").SetValue("Año: ");
                wss.Range("H3").SetValue("Mes: ");

                wss.Range("H2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                wss.Range("H3").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                wss.Range("I2").SetValue(drpAnio.SelectedItem.Text);
                wss.Range("I3").SetValue(drpMes.SelectedItem.Text);

                /// Columna de estatus
                wss.Range("B5:F5").Merge();
                wss.Range("B5").SetValue("Estatus");
                wss.Range("B5:F5").Style.Font.Bold = true;
                wss.Range("B5:F5").Style.Fill.BackgroundColor = XLColor.CadetBlue;
                wss.Range("B5:F5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wss.Range("B5:F5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                /// Columnas del listado
                //  Sucursal Capturado   Solicitado G    Solicitado J    Otro Autorizado  
                //  Total de Captura Total Capturado vs Autorizado Avance de Captura de 80/20
                wss.Cell("A6").SetValue("Sucursal");
                wss.Cell("A6").Style.Font.Bold = true;
                wss.Cell("A6").Style.Font.FontColor = XLColor.White;
                wss.Cell("A6").Style.Fill.BackgroundColor = XLColor.UltramarineBlue;
                wss.Cell("A6").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wss.Cell("A6").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                wss.Cell("B6").SetValue("Capturado");
                wss.Cell("B6").Style.Font.Bold = true;
                ///wss.Cell("B6").Style.Font.FontColor = XLColor.White;
                wss.Cell("B6").Style.Fill.BackgroundColor = XLColor.CadetBlue;
                wss.Cell("B6").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wss.Cell("B6").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                wss.Cell("C6").SetValue("Solicitado G");
                wss.Cell("C6").Style.Font.Bold = true;
                ///wss.Cell("C6").Style.Font.FontColor = XLColor.White;
                wss.Cell("C6").Style.Fill.BackgroundColor = XLColor.CadetBlue;
                wss.Cell("C6").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wss.Cell("C6").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                wss.Cell("D6").SetValue("Solicitado J");
                wss.Cell("D6").Style.Font.Bold = true;
                ///wss.Cell("D6").Style.Font.FontColor = XLColor.White;
                wss.Cell("D6").Style.Fill.BackgroundColor = XLColor.CadetBlue;
                wss.Cell("D6").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wss.Cell("D6").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                wss.Cell("E6").SetValue("Otro");
                wss.Cell("E6").Style.Font.Bold = true;
                ///wss.Cell("E6").Style.Font.FontColor = XLColor.White;
                wss.Cell("E6").Style.Fill.BackgroundColor = XLColor.CadetBlue;
                wss.Cell("E6").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wss.Cell("E6").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                wss.Cell("F6").SetValue("Autorizado");
                wss.Cell("F6").Style.Font.Bold = true;
                ///wss.Cell("F6").Style.Font.FontColor = XLColor.White;
                wss.Cell("F6").Style.Fill.BackgroundColor = XLColor.CadetBlue;
                wss.Cell("F6").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wss.Cell("F6").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                wss.Cell("G6").SetValue("Total ACyS Vigentes");
                wss.Cell("G6").Style.Font.Bold = true;
                wss.Cell("G6").Style.Font.FontColor = XLColor.White;
                wss.Cell("G6").Style.Fill.BackgroundColor = XLColor.UltramarineBlue;
                wss.Cell("G6").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wss.Cell("G6").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                wss.Cell("H6").SetValue("Cumplimiento Autorizados");
                wss.Cell("H6").Style.Font.Bold = true;
                //  wss.Cell("H6").Style.Font.FontColor = XLColor.White;
                wss.Cell("H6").Style.Fill.BackgroundColor = XLColor.CadmiumYellow;
                wss.Cell("H6").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wss.Cell("H6").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                wss.Cell("I6").SetValue("Cumplimiento ACyS Clientes de Venta Instalada");
                wss.Cell("I6").Style.Font.Bold = true;
                wss.Cell("I6").Style.Font.FontColor = XLColor.White;
                wss.Cell("I6").Style.Fill.BackgroundColor = XLColor.UltramarineBlue;
                wss.Cell("I6").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wss.Cell("I6").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                i++;
                wss.Cell("A" + (i + 6)).SetValue("CDI " + drpCDI.SelectedItem.Text);
                wss.Cell("A" + (i + 6)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                string celditA = "";
                int Total = 0;
                int Autorizados = 0;

                wss.Cell("B" + (i + 6).ToString()).Value = "0";
                wss.Cell("C" + (i + 6).ToString()).Value = "0";
                wss.Cell("D" + (i + 6).ToString()).Value = "0";
                wss.Cell("E" + (i + 6).ToString()).Value = "0";
                wss.Cell("F" + (i + 6).ToString()).Value = "0";

                foreach (DashboardACyS_Estatus rng in ListaEstatus)
                {
                    if (rng.AcsEstatusTexto == "Capturado") { celditA = "B"; }
                    if (rng.AcsEstatusTexto == "Solicitado G") { celditA = "C"; }
                    if (rng.AcsEstatusTexto == "Solicitado J") { celditA = "D"; }
                    if ((rng.AcsEstatusTexto == "Cancelado") || (rng.AcsEstatusTexto == "Solicitado")) { celditA = "E"; }
                    if (rng.AcsEstatusTexto == "Autorizado") { celditA = "F"; Autorizados = rng.NACySValor; }

                    Total = Total + rng.NACySValor;

                    wss.Cell(celditA + (i + 6).ToString()).Value = rng.NACySValor.ToString();
                    wss.Cell(celditA + (i + 6).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wss.Cell(celditA + (i + 6).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                }

                wss.Cell("G" + (i + 6).ToString()).Value = Total.ToString();
                wss.Cell("G" + (i + 6).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wss.Cell("G" + (i + 6).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                if (Total == 0)
                {
                    wss.Range("A" + (i + 6).ToString() + ":I" + (i + 6).ToString()).Style.Font.FontColor = XLColor.White;
                    wss.Range("A" + (i + 6).ToString() + ":I" + (i + 6).ToString()).Style.Font.Bold = true;
                    wss.Range("A" + (i + 6).ToString() + ":I" + (i + 6).ToString()).Style.Fill.BackgroundColor = XLColor.Red;
                    wss.Cell("H" + (i + 6).ToString()).SetValue("0%");
                    wss.Cell("H" + (i + 6).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wss.Cell("I" + (i + 6).ToString()).SetValue("0%");
                    wss.Cell("I" + (i + 6).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                }

                wss.Cell("H" + (i + 6).ToString()).SetValue(Math.Round((Convert.ToDouble(Convert.ToDouble(Autorizados) / Convert.ToDouble(Total)) * 100), 0).ToString() + "%");
                wss.Cell("H" + (i + 6).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wss.Cell("H" + (i + 6).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                DashboardACyS_Resumen TotalVs = new DashboardACyS_Resumen();
                TotalVs = ListaResumen.Where(a => a.Ordern == 5 && a.IdRik == 0).FirstOrDefault();

                wss.Cell("I" + (i + 6).ToString()).SetValue(TotalVs.sValor);
                wss.Cell("I" + (i + 6).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wss.Cell("I" + (i + 6).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                // si lo pide, se desglosa la informacion por RIK
                if (chkDetalleXRIKCumplimiento.Checked)
                {
                    int iiCaptura = 0;
                    int iiSolicitadoG = 0;
                    int iiSolicitadoJ = 0;
                    int iiAutorizado = 0;
                    int iiOtro = 0;
                    string strRIK = "";
                    DashboardACyS_RIKS RIIKK = new DashboardACyS_RIKS();
                    /// cada uno de los RIKS
                    foreach (var riik in ExcelBaseDeRiks_DtlRiks)
                    {
                        i++;
                        iiCaptura = ExcelDetalleACyS_DtlRiks.Where(ri => ri.Rik == riik.IdRik && ri.Estatus == "Capturado").Count();
                        iiSolicitadoG = ExcelDetalleACyS_DtlRiks.Where(ri => ri.Rik == riik.IdRik && ri.Estatus == "Solicitado G").Count();
                        iiSolicitadoJ = ExcelDetalleACyS_DtlRiks.Where(ri => ri.Rik == riik.IdRik && ri.Estatus == "Solicitado J").Count();
                        iiAutorizado = ExcelDetalleACyS_DtlRiks.Where(ri => ri.Rik == riik.IdRik && ri.Estatus == "Autorizado").Count();
                        iiOtro = ExcelDetalleACyS_DtlRiks.Where(ri => ri.Rik == riik.IdRik && (ri.Estatus == "Cancelado" || ri.Estatus == "Solicitado")).Count();

                        Total = ExcelResumen_DtlRiks.Where(ri => ri.IdRik == riik.IdRik && ri.Ordern == 1).Select(y => int.Parse(y.sValor)).FirstOrDefault();

                        wss.Cell("A" + (i + 6).ToString()).Value = "RIK " + riik.Rik_Nombre;
                        wss.Cell("A" + (i + 6).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wss.Cell("A" + (i + 6).ToString()).Style.Alignment.Indent = 2;
                        wss.Cell("A" + (i + 6).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                        wss.Cell("B" + (i + 6).ToString()).Value = iiCaptura.ToString();
                        wss.Cell("B" + (i + 6).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wss.Cell("B" + (i + 6).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                        wss.Cell("C" + (i + 6).ToString()).Value = iiSolicitadoG.ToString();
                        wss.Cell("C" + (i + 6).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wss.Cell("C" + (i + 6).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                        wss.Cell("D" + (i + 6).ToString()).Value = iiSolicitadoJ.ToString();
                        wss.Cell("D" + (i + 6).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wss.Cell("D" + (i + 6).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                        wss.Cell("E" + (i + 6).ToString()).Value = iiOtro.ToString();
                        wss.Cell("E" + (i + 6).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wss.Cell("E" + (i + 6).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                        wss.Cell("F" + (i + 6).ToString()).Value = iiAutorizado.ToString();
                        wss.Cell("F" + (i + 6).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wss.Cell("F" + (i + 6).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                        wss.Cell("G" + (i + 6).ToString()).Value = Total.ToString();
                        wss.Cell("G" + (i + 6).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wss.Cell("G" + (i + 6).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                        TotalVs = ExcelResumen_DtlRiks.Where(a => a.Ordern == 3 && a.IdRik == riik.IdRik).FirstOrDefault();
                        wss.Cell("H" + (i + 6).ToString()).SetValue(TotalVs.sValor);
                        wss.Cell("H" + (i + 6).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        wss.Cell("H" + (i + 6).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        TotalVs = ExcelResumen_DtlRiks.Where(a => a.Ordern == 5 && a.IdRik == riik.IdRik).FirstOrDefault();
                        if (TotalVs != null)
                        {
                            if (!string.IsNullOrEmpty(TotalVs.sValor))
                            {
                                wss.Cell("I" + (i + 6).ToString()).SetValue(TotalVs.sValor);
                            }
                            else
                            {
                                wss.Cell("I" + (i + 6).ToString()).SetValue(0);
                            }

                        }
                        else
                        {
                            wss.Cell("I" + (i + 6).ToString()).SetValue(0);
                        }

                        wss.Cell("I" + (i + 6).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        wss.Cell("I" + (i + 6).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    }
                }

                if (this.chkDesgloseEstatus.Checked == false)
                {
                    //  wss.Columns(2, 6).Hide();
                    wss.Columns(2, 6).Delete();
                }

                #endregion

                wss.Columns().AdjustToContents();
            }
            catch (Exception ex)
            {
                this.ErrorManager(ex, "FormatoCumplimientoDeCaptura");
                throw ex;
            }
        }

        void llenaDatosPorRik(bool boolGraficarRepresentante)
        {
            try
            {
                List<DashboardACyS_Resumen> ListResumen = new List<DashboardACyS_Resumen>();
                List<DashboardACyS_RIKS> ListbaseCliente = new List<DashboardACyS_RIKS>();
                List<DashboardACyS_Clientes> ClientesCon = new List<DashboardACyS_Clientes>();
                List<DashboardACyS_Clientes> ClientesSin = new List<DashboardACyS_Clientes>();
                List<DashboardACyS_DetalleACyS> DetalleAcyS = new List<DashboardACyS_DetalleACyS>();

                /// Obtiene Datos
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_dshIndicadoresACyS clsdshIndicadoresACyS = new CN_dshIndicadoresACyS();
                int iRik;
                string siRik;

                siRik = (drpGridRiks2.SelectedValue == "-1") ? "" : drpGridRiks2.SelectedValue;
                iRik = (string.IsNullOrEmpty(siRik)) ? -1 : 1;


                string sEstatus = this.drpEstatus.SelectedItem.Value;
                string strAnio = this.drpAnio.SelectedItem.Value;
                string strMes = this.drpMes.SelectedItem.Value;

                // IMPORTRANTE: validar año
                int intAnio = int.Parse(strAnio);
                int intAnioActual = DateTime.Now.Year;
                string strAnioHistorico = "2022"; //año inicia el historico de consulta
                int intMes = int.Parse(strMes);
                int intMesHistorico = 6;

                if (strAnio == strAnioHistorico)
                {
                    if (intMes < intMesHistorico)
                    {
                        this.drpMes.SelectedValue = intMesHistorico.ToString();
                        strMes = this.drpMes.SelectedItem.Value;
                    }
                }

                intMes = int.Parse(strMes);
                if (intMes == 1)
                {
                    intMes = 12;
                    intAnio -= 1;
                }
                else
                {
                    intMes -= 1;
                }
                clsdshIndicadoresACyS.ConsultaDashboardPorRIKACyS_v2(sesion.Id_Emp, sesion.Id_Cd, sesion.Id_U, iRik, siRik, sEstatus, intAnio.ToString(), intMes.ToString(), sesion.Emp_Cnx,
                    ref ListResumen, ref ListbaseCliente,
                    ref ClientesCon, ref ClientesSin, ref DetalleAcyS);

                ExcelResumen_DtlRiks = new List<DashboardACyS_Resumen>();
                ExcelResumen_DtlRiks = ListResumen;

                ExcelBaseDeRiks_DtlRiks = new List<DashboardACyS_RIKS>();
                ExcelBaseDeRiks_DtlRiks = ListbaseCliente;

                ExcelClientesCon_DtlRiks = new List<DashboardACyS_Clientes>();
                ExcelClientesCon_DtlRiks = ClientesCon;

                ExcelClientesSin_DtlRiks = new List<DashboardACyS_Clientes>();
                ExcelClientesSin_DtlRiks = ClientesSin;

                ExcelDetalleACyS_DtlRiks = new List<DashboardACyS_DetalleACyS>();
                ExcelDetalleACyS_DtlRiks = DetalleAcyS;

                List<DashboardACyS_Estatus> ListEstatus = new List<DashboardACyS_Estatus>();
                if (boolGraficarRepresentante)
                {
                    ListEstatus = DetalleAcyS.GroupBy(x => x.Estatus).Select(g => new DashboardACyS_Estatus { AcsEstatusTexto = g.Key, NACySValor = g.Count() }).ToList();
                    GeneraResumen(ListResumen);
                    LlenaGauges(ListEstatus);
                }


            }
            catch (Exception ex)
            {
                this.ErrorManager(ex, "llenaDashboard");
            }
        }

        void AvanceDetalladoPorRIKS(List<DashboardACyS_Resumen> ListaResumen,
            List<DashboardACyS_RIKS> ListaDeRIKS,
            List<DashboardACyS_Clientes> ListaClientesCon, List<DashboardACyS_Clientes> ListaClientesSin,
            List<DashboardACyS_DetalleACyS> ListaDeDetalleACyS, bool boolSoloRepresentante)
        {
            string filenombre = "ACySAvanceDetalladoPorRIKS_" + DateTime.UtcNow.ToShortDateString().Replace("/", "") + "h" + DateTime.UtcNow.ToShortTimeString().Replace(".", "").Replace(":", "").Replace(" ", "");
            string preNomHoja = String.Empty;
            try
            {

                var wb = new XLWorkbook();
                List<DashboardACyS_Resumen> ListaRikResumen = new List<DashboardACyS_Resumen>();
                List<DashboardACyS_Clientes> ListaRIKClientesCon;
                List<DashboardACyS_Clientes> ListaRIKClientesSin;
                List<DashboardACyS_DetalleACyS> ListaRIKDeDetalleACyS;
                IXLWorksheet ws;
                if (!boolSoloRepresentante)
                {
                    preNomHoja = "CDI_" + drpCDI.SelectedItem.Text;
                    if (preNomHoja.Length > 30)
                    {
                        preNomHoja = "CDI_" + ListaDeRIKS[0].IdCdi;
                    }
                    ws = wb.Worksheets.Add(preNomHoja);
                    FormatoAvanceDetallado("CDI: " + drpCDI.SelectedItem.Text, ListaResumen.Where(i => i.IdRik == 0).ToList(), ListaClientesCon, ListaClientesSin, ListaDeDetalleACyS, ref ws);
                }
                else
                {
                    preNomHoja = drpCDI.SelectedItem.Text + "_";
                    if (preNomHoja.Length > 30)
                    {
                        preNomHoja = +ListaDeRIKS[0].IdCdi + "_";
                    }
                }

                foreach (DashboardACyS_RIKS rik in ListaDeRIKS)
                {

                    ws = wb.Worksheets.Add(preNomHoja + "RIK_" + rik.IdRik);

                    ListaRikResumen = ExcelResumen_DtlRiks.Where(x => x.IdRik == rik.IdRik).ToList();
                    ListaRIKClientesCon = ExcelClientesCon_DtlRiks.Where(x => x.IdRik == rik.IdRik).ToList();
                    ListaRIKClientesSin = ExcelClientesSin_DtlRiks.Where(x => x.IdRik == rik.IdRik).ToList();
                    ListaRIKDeDetalleACyS = ExcelDetalleACyS_DtlRiks.Where(i => i.Rik == rik.IdRik).ToList();

                    FormatoAvanceDetallado(rik.Rik_Nombre, ListaRikResumen,
                        ListaRIKClientesCon,
                        ListaRIKClientesSin,
                        ListaRIKDeDetalleACyS, ref ws);
                }

                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + filenombre + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                this.ErrorManager(ex, "AvanceDetallado");
                throw ex;
            }
        }

        private void ValidarSesion()
        {

        }

        #endregion


        #region Eventos

        protected void drpCDI_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                llenaDashboard();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "drpCDI_SelectedIndexChanged");
            }
        }


        protected void drpEstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListItem iitem = this.drpEstatus.SelectedItem;
            if (iitem != null)
            {
                llenaDashboard();
            }
        }

        protected void btnExcelGrafica_Click(object sender, ImageClickEventArgs e)
        {
            llenaDashboard();
            if (ExcelDeRiks != null)
            {
                if (ExcelDeRiks.Count != 0)
                {
                    ExportaAExcel();
                }
            }
        }

        protected void imgRptAvance_Click(object sender, ImageClickEventArgs e)
        {
            /*
             bool boolSoloRepresentante = drpGridRiks2.SelectedValue == "-1" ? false : true;

             if (chkDetalleXRIK.Checked == false)
             {
                 if (boolSoloRepresentante)
                 {

                 }
                 else
                 {

                 }
             }
             else
             {
                 llenaDashboard();
                 llenaDatosPorRik(boolSoloRepresentante);

             }*/
            LlenarAvanceDetallado();
        }

        protected void imgRptCumplimiento_Click(object sender, ImageClickEventArgs e)
        {
            LLenarCumplimiento();
        }

        private void LLenarCumplimiento()
        {
            llenaDashboard();

            if (chkDetalleXRIKCumplimiento.Checked)
            {
                llenaDatosPorRik(false);
            }
            else
            {
                ExcelBaseDeRiks_DtlRiks = new List<DashboardACyS_RIKS>();
            }

            CumplimientoCaptura("Cumplimiento", ExcelBaseDeRiks_DtlRiks, ExcelResumen, ExcelDeEstatus, ExcelDetalleACyS);

        }

        protected void btnAvance_Click(object sender, EventArgs e)
        {
            LlenarAvanceDetallado();
        }

        private void LlenarAvanceDetallado()
        {
            bool boolSoloRepresentante = drpGridRiks2.SelectedValue == "-1" ? false : true;

            /// Es una corrida directa, solo la pagina de TOTAL
            if (this.chkDetalleXRIK.Checked == false)
            {
                if (boolSoloRepresentante)
                {
                    llenaDatosPorRik(boolSoloRepresentante);
                    List<DashboardACyS_Resumen> dashboardACyS_Resumen = new List<DashboardACyS_Resumen>();
                    dashboardACyS_Resumen = ExcelDetalleACyS.Select(x => new DashboardACyS_Resumen { IdCdi = x.IdCdi }).ToList();
                    AvanceDetalladoPorRIKS(ExcelResumen, ExcelBaseDeRiks_DtlRiks, ExcelClientesCon, ExcelClientesSin, ExcelDetalleACyS, boolSoloRepresentante);

                }
                else
                {
                    llenaDashboard();
                    AvanceDetallado("Total", ExcelResumen, ExcelClientesCon, ExcelClientesSin, ExcelDetalleACyS);
                }

            }
            else
            {
                /// Es una corrida separada por RIK
                llenaDashboard();
                llenaDatosPorRik(boolSoloRepresentante);
                AvanceDetalladoPorRIKS(ExcelResumen, ExcelBaseDeRiks_DtlRiks, ExcelClientesCon, ExcelClientesSin, ExcelDetalleACyS, boolSoloRepresentante);
            }
        }

        protected void btnCumplimiento_Click(object sender, EventArgs e)
        {
            LLenarCumplimiento();
        }

        protected void btnActualiza_Click(object sender, EventArgs e)
        {
            llenaDashboard();
        }



        #endregion

        #region ErrorManager

        private void Alerta(string mensaje)
        {
            try
            {
                ///     RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
                this.lblMensaje.Text = mensaje;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }

        private void ErrorManager()
        {
            try
            {
                this.lblMensaje.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ErrorManager(string Message)
        {
            try
            {
                this.lblMensaje.Text = Message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ErrorManager(Exception eme, string NombreFuncion)
        {
            try
            {
                this.lblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();

            }
            catch (Exception ex)
            {
                this.lblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }


        #endregion

        protected void ImgBtnGraficar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                GraficaIndicadores();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void drpMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                llenaDashboard();
                LlenarListaRIKs(ExcelDeRiks);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void drpGridRiks2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkDetalleXRIK.Checked == true)
            {
                chkDetalleXRIK.Checked = false;
            }

            GraficaIndicadores();
        }

        private void GraficaIndicadores()
        {
            try
            {

                bool boolGraficarRepresentante = false;
                // es una sucursal y selecciono un rik

                string siRik = drpGridRiks2.SelectedValue;
                if (siRik != "-1")
                {
                    boolGraficarRepresentante = true;
                }
                else
                {
                    boolGraficarRepresentante = false;
                }

                //llenaDashboard();

                if (boolGraficarRepresentante)
                {
                    llenaDatosPorRik(boolGraficarRepresentante);
                }



            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void chkDetalleXRIK_CheckedChanged(object sender, EventArgs e)
        {
            if (drpGridRiks2.SelectedValue != "-1")
            {
                drpGridRiks2.SelectedValue = "-1";
                LimpiarGrafica();
                llenaDashboard();
            }
        }

        private void LimpiarGrafica()
        {

            lblTotalGeneral = string.Empty;
            lblCapturavsAutorizado = string.Empty;
            lblCumplimientoACySAutorizados = string.Empty;
            lbltotalVentaInstalada = string.Empty;
            lblCumplimientoACySVenta = string.Empty;
            NumCapturado = "0";
            NumSolicitadoG = "0";
            NumSolicitadoJ = "0";
            NumAutorizado = "0";
            NumOtro = "0";
            lblNumCapturado = "0";
            lblNumSolicitadoG = "0";
            lblNumSolicitadoJ = "0";
            lblNumAutorizado = "0";
            lblNumOtro = "0";
        }
    }
}