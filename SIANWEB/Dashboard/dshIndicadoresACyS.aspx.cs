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

namespace SIANWEB.Dashboard
{
    public partial class dshIndicadoresACyS : System.Web.UI.Page
    {

        public string strRIKS = "";
        public string strDatoRIKS = "";
        public string strURLBack = "";

        public string NumCapturado = "0";
        public string NumSolicitadoG = "0";
        public string NumSolicitadoJ = "0";
        public string NumAutorizado = "0";
        public string NumOtro = "0";

        public string lblTotalGeneral = "";
        public string lblTotalClientes = "";
        public string lblTotalCaptura = "";
        public string lblAvanceCaptura = "";
        public string lblCapturavsAutorizado = "";

        public string lblNumCapturado = "0";
        public string lblNumSolicitadoG = "0";
        public string lblNumSolicitadoJ = "0";
        public string lblNumAutorizado = "0";
        public string lblNumOtro = "0";

        public List<DashboardACyS_RIKS> ExcelBaseDeRiks;
        public List<DashboardACyS_DetalleRIKS> ExcelDeRiks;
        public List<DashboardACyS_Resumen> ExcelResumen;
        public List<DashboardACyS_Clientes> ExcelClientesCon;
        public List<DashboardACyS_Clientes> ExcelClientesSin;
        public List<DashboardACyS_DetalleACyS> ExcelDetalleACyS;
        public List<DashboardACyS_RIKS> ListadoDeRIKs;

        public List<DashboardACyS_Estatus> ExcelDeEstatus;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
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
                        CargarRepresentantes();
                        llenaDashboard();
                    }
                }
                /*
                strURLBack = "ReporteComerciales.aspx";
                if ((int)Session["activeMenu"] == 1 )
                {
                    strURLBack = "ReporteComerciales.aspx";
                }
                if ((int)Session["activeMenu"] == 2)
                {
                    strURLBack = "ReporteComercialsCRM.aspx";
                }
                if ((int)Session["activeMenu"] == 3)
                {
                    strURLBack = "ReporteComercialesLeads.aspx";
                }
                if ((int)Session["activeMenu"] == 4)
                {
                    strURLBack = "ReporteComerciales_MonitoreoProducto.aspx";
                }
                */
                Session["activeMenu"] = 5;
            }
            catch (Exception ex)
            {
                this.ErrorManager(ex, "Page_Load");
            }
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
            }
            catch (Exception ex)
            {
                //throw ex;
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        void CargarRepresentantes()
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_dshIndicadoresACyS clsdshIndicadoresACyS = new CN_dshIndicadoresACyS();
                List<Renglon> lista = new List<Renglon>();

                clsdshIndicadoresACyS.LlenaRIKs(sesion.Id_Cd, "dshTodosLosRIKSACyS", sesion.Emp_Cnx,
                    ref lista);
                this.drpRIKs.DataValueField = "idRng";
                this.drpRIKs.DataTextField = "sValor";
                this.drpRIKs.DataSource = lista.OrderBy(i => i.sValor);
                this.drpRIKs.DataBind();

                this.lstchkRIKS.DataValueField = "idRng";
                this.lstchkRIKS.DataTextField = "sValor";
                this.lstchkRIKS.DataSource = lista.OrderBy(i => i.sValor);
                this.lstchkRIKS.DataBind();

                foreach(ListItem ch in lstchkRIKS.Items)
                {
                    ch.Selected = true;
                }

            }
            catch (Exception ex)
            {
                //throw ex;
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        #endregion


        #region LlenarDashboard

        void llenaDashboard()
        {
            try
            {
                List<DashboardACyS_Resumen> ListResumen = new List<DashboardACyS_Resumen>();
                List<DashboardACyS_Estatus> ListEstatus = new List<DashboardACyS_Estatus>();
                List<DashboardACyS_RIKS> ListRIKs = new List<DashboardACyS_RIKS>();
                List<DashboardACyS_DetalleRIKS> ListDetaleRIKs = new List<DashboardACyS_DetalleRIKS>();

                List<DashboardACyS_Clientes> ClientesCon = new List<DashboardACyS_Clientes>();
                List<DashboardACyS_Clientes> ClientesSin = new List<DashboardACyS_Clientes>();
                List<DashboardACyS_DetalleACyS> DetalleAcyS = new List<DashboardACyS_DetalleACyS>();

                /// Obtiene Datos
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_dshIndicadoresACyS clsdshIndicadoresACyS = new CN_dshIndicadoresACyS();
                int iRik = Convert.ToInt32(this.drpRIKs.SelectedItem.Value);
                iRik = (chkTodosRIKs.Checked ? -1 : 1);
                string siRik = "";

                foreach (ListItem chk in lstchkRIKS.Items)
                {
                    if (chk.Selected)
                    {
                        siRik = siRik + chk.Value.ToString() + ",";
                    }
                }

                string sEstatus = this.drpEstatus.SelectedItem.Value;

                clsdshIndicadoresACyS.ConsultaDashboardACyS(sesion.Id_Emp, sesion.Id_Cd, sesion.Id_U, iRik, siRik, sEstatus, sesion.Emp_Cnx, 
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
            lblTotalGeneral = "";
            lblTotalClientes = "";
            lblTotalCaptura= "";
            lblAvanceCaptura= "";
            lblCapturavsAutorizado= "";
            foreach(DashboardACyS_Resumen rng in ListaResumen)
            {
                if (rng.Ordern==1)
                {
                    lblTotalGeneral = rng.sValor;
                }
                if (rng.Ordern == 2)
                {
                    lblTotalClientes = rng.sValor;
                }
                if (rng.Ordern == 3)
                {
                    lblTotalCaptura = rng.sValor;
                }
                if (rng.Ordern == 4)
                {
                    lblAvanceCaptura = rng.sValor;
                }
                if (rng.Ordern == 5)
                {
                    lblCapturavsAutorizado = rng.sValor;
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

                if ( (rngg.AcsEstatusTexto == "Cancelado") || (rngg.AcsEstatusTexto == "Solicitado") )
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

            NumCapturado = Math.Round((Convert.ToDouble(Convert.ToDouble(lblNumCapturado) / Convert.ToDouble(Total)) * 100), 0).ToString();
            NumSolicitadoG = Math.Round((Convert.ToDouble(Convert.ToDouble(lblNumSolicitadoG) / Convert.ToDouble(Total)) * 100), 0).ToString();
            NumSolicitadoJ = Math.Round((Convert.ToDouble(Convert.ToDouble(NumSolicitadoJ) / Convert.ToDouble(Total)) * 100), 0).ToString();
            NumAutorizado = Math.Round((Convert.ToDouble(Convert.ToDouble(lblNumAutorizado) / Convert.ToDouble(Total)) * 100), 0).ToString();
            NumOtro = Math.Round((Convert.ToDouble(Convert.ToDouble(lblNumOtro) / Convert.ToDouble(Total)) * 100), 0).ToString();

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
            foreach(DashboardACyS_RIKS rik in ListaRIKs)
            {
                strDatoRIKS = strDatoRIKS + rik.NACySValor.ToString().Trim()  + ", ";
                strRIKS = strRIKS + " \"" + rik.Rik_Nombre.Trim() + "\", ";
            }

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
            List<DashboardACyS_DetalleACyS> ListaDeDetalleACyS, ref IXLWorksheet wss )
        {
            int i = 0;
            try
            {
                #region Resumen

                wss.Range("A1:C1").Merge();
                wss.Range("A1").SetValue(strTitulo);
                wss.Cell("A1").Style.Font.Bold = true;

                wss.Range("I1:J1").Merge();
                wss.Range("I1").SetValue("Fecha Corte: " + DateTime.Now.ToString("dd/MM/yyyy"));

                wss.Range("B2:E2").Merge();
                foreach (DashboardACyS_Resumen rng in ListaResumen)
                {
                    wss.Range("B" + (i + 3) + ":D" + (i + 3)).Merge();
                    wss.Cell("B" + (i + 3)).Value = rng.Concepto.ToString();
                    wss.Cell("E" + (i + 3)).Value = rng.sValor.ToString();
                    i++;
                }

                // Formato
                wss.Range("E2:E" + (i + 2).ToString()).Style.Font.Bold = true;
                wss.Cell("E" + (i + 1)).Style.Font.FontSize = 14;
                /// wss.Cell("E" + (i + 1)).Style.NumberFormat.Format = "{0:p0}";
                wss.Cell("E" + (i + 1)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                wss.Cell("E" + (i + 2)).Style.Font.FontSize = 14;
                /// wss.Cell("E" + (i + 2)).Style.NumberFormat.Format = "{0:p0}";
                wss.Cell("E" + (i + 2)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                #endregion

                #region Clientes8020

                // si se incluye segun el checkbox
                if (this.chkLista8020.Checked)
                {
                    i++;    // brinco un renglon
                    wss.Range("A" + (i + 3).ToString() + ":J" + (i + 3).ToString()).Merge();
                    wss.Range("A" + (i + 3).ToString()).SetValue("Clientes que Conforman el 80/20");
                    wss.Range("A" + (i + 3).ToString()).Style.Font.Bold = true;
                    wss.Range("A" + (i + 3).ToString()).Style.Font.FontColor = XLColor.White;
                    wss.Range("A" + (i + 3).ToString()).Style.Fill.BackgroundColor = XLColor.UltramarineBlue;
                    wss.Range("A" + (i + 3).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    wss.Range("A" + (i + 3).ToString() + ":J" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    i++;
                    wss.Range("A" + (i + 3).ToString() + ":E" + (i + 3).ToString()).Merge();
                    wss.Range("A" + (i + 3).ToString()).SetValue("Clientes CON ACyS");
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

                wss.Range("A2:J2").Merge();
                wss.Range("A2").SetValue("Reporte de Cumplimiento en Captura de ACyS");
                wss.Range("A2:J2").Style.Font.Bold = true;
                wss.Range("A2:J2").Style.Font.FontSize = 14;
                wss.Range("A2:J2").Style.Font.FontColor = XLColor.White;
                wss.Range("A2:J2").Style.Fill.BackgroundColor = XLColor.LightSlateGray;

                wss.Range("A3:J3").Merge();
                wss.Range("A3").SetValue("al corte: " + DateTime.Now.ToString("dd/MM/yyyy"));
                wss.Range("A3:J3").Style.Fill.BackgroundColor = XLColor.LightSlateGray;

                //  if (this.chkDesgloseEstatus.Checked)
                {
                    /// Columna de estatus
                    wss.Range("B5:F5").Merge();
                    wss.Range("B5").SetValue("Estatus");
                    wss.Range("B5:F5").Style.Font.Bold = true;
                    wss.Range("B5:F5").Style.Fill.BackgroundColor = XLColor.CadetBlue;
                    wss.Range("B5:F5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    wss.Range("B5:F5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                }
                /// Columnas del listado
                //  Sucursal Capturado   Solicitado G    Solicitado J    Otro Autorizado  
                //  Total de Captura Total Capturado vs Autorizado Avance de Captura de 80/20
                wss.Cell("A6").SetValue("Sucursal");
                wss.Cell("A6").Style.Font.Bold = true;
                wss.Cell("A6").Style.Font.FontColor = XLColor.White;
                wss.Cell("A6").Style.Fill.BackgroundColor = XLColor.UltramarineBlue;
                wss.Cell("A6").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wss.Cell("A6").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                /// if (this.chkDesgloseEstatus.Checked)
                {
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
                }

                wss.Cell("G6").SetValue("Total de Captura");
                wss.Cell("G6").Style.Font.Bold = true;
                wss.Cell("G6").Style.Font.FontColor = XLColor.White;
                wss.Cell("G6").Style.Fill.BackgroundColor = XLColor.UltramarineBlue;
                wss.Cell("G6").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wss.Cell("G6").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                wss.Cell("H6").SetValue("Total Capturado vs Autorizado");
                wss.Cell("H6").Style.Font.Bold = true;
                //  wss.Cell("H6").Style.Font.FontColor = XLColor.White;
                wss.Cell("H6").Style.Fill.BackgroundColor = XLColor.CadmiumYellow;
                wss.Cell("H6").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wss.Cell("H6").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                wss.Cell("I6").SetValue("Avance de Captura de 80/20");
                wss.Cell("I6").Style.Font.Bold = true;
                wss.Cell("I6").Style.Font.FontColor = XLColor.White;
                wss.Cell("I6").Style.Fill.BackgroundColor = XLColor.UltramarineBlue;
                wss.Cell("I6").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wss.Cell("I6").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                i++;
                wss.Cell("A" + (i + 6) ).SetValue("CDI " + drpCDI.SelectedItem.Text);
                wss.Cell("A" + (i + 6) ).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                
                string celditA = "";
                int Total = 0;
                int Autorizados = 0;
                foreach (DashboardACyS_Estatus rng in ListaEstatus)
                {
                    if (rng.AcsEstatusTexto == "Capturado") { celditA = "B"; }
                    if (rng.AcsEstatusTexto == "Solicitado G") { celditA = "C"; }
                    if (rng.AcsEstatusTexto == "Solicitado J") { celditA = "D"; }
                    if ((rng.AcsEstatusTexto == "Cancelado") || (rng.AcsEstatusTexto == "Solicitado")) { celditA = "E"; }
                    if (rng.AcsEstatusTexto == "Autorizado") { celditA = "F"; Autorizados = rng.NACySValor; }

                    Total = Total + rng.NACySValor;

                    //  if (this.chkDesgloseEstatus.Checked)
                    {
                        wss.Cell(celditA + (i + 6).ToString()).Value = rng.NACySValor.ToString();
                        wss.Cell(celditA + (i + 6).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wss.Cell(celditA + (i + 6).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    }

                }
                
                wss.Cell("G" + (i + 6).ToString()).Value = Total.ToString();
                wss.Cell("G" + (i + 6).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wss.Cell("G" + (i + 6).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                if (Total==0)
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
                    int iiSolicitadoG= 0;
                    int iiSolicitadoJ = 0;
                    int iiAutorizado = 0;
                    int iiOtro = 0;
                    string strRIK = "";
                    DashboardACyS_RIKS RIIKK = new DashboardACyS_RIKS();
                    /// cada uno de los RIKS
                    foreach (int riik in ListaDeDetalleACyS.Select(rr => rr.Rik).Distinct().ToList())
                    {
                        i++;
                        iiCaptura = ListaDeDetalleACyS.Where(ri => ri.Rik == riik && ri.Estatus == "Capturado").Count();
                        iiSolicitadoG = ListaDeDetalleACyS.Where(ri => ri.Rik == riik && ri.Estatus == "Solicitado G").Count();
                        iiSolicitadoJ = ListaDeDetalleACyS.Where(ri => ri.Rik == riik && ri.Estatus == "Solicitado J").Count();
                        iiAutorizado = ListaDeDetalleACyS.Where(ri => ri.Rik == riik && ri.Estatus == "Autorizado").Count();
                        iiOtro = ListaDeDetalleACyS.Where(ri => ri.Rik == riik && (ri.Estatus == "Cancelado" || ri.Estatus == "Solicitado")).Count();
                        strRIK = "";
                        Total = ListaDeDetalleACyS.Where(ri => ri.Rik == riik).Count();
                        RIIKK = ListaRIKS.Where(ri => ri.IdRik == riik).FirstOrDefault();
                        if (RIIKK != null)
                        {
                            strRIK = RIIKK.Rik_Nombre;
                        }

                        wss.Cell("A" + (i + 6).ToString()).Value = "RIK " + strRIK;
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

                        wss.Cell("H" + (i + 6).ToString()).SetValue(Math.Round((Convert.ToDouble(Convert.ToDouble(iiAutorizado) / Convert.ToDouble(Total)) * 100), 0).ToString() + "%");
                        wss.Cell("H" + (i + 6).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        wss.Cell("H" + (i + 6).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                        TotalVs = ListaResumen.Where(a => a.Ordern == 4 && a.IdRik == riik).FirstOrDefault();
                        wss.Cell("I" + (i + 6).ToString()).SetValue(TotalVs.sValor);
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

        void llenaDatosPorRik()
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
                int iRik = Convert.ToInt32(this.drpRIKs.SelectedItem.Value);
                iRik = (chkTodosRIKs.Checked ? -1 : 1);
                string siRik = "";

                foreach (ListItem chk in lstchkRIKS.Items)
                {
                    if (chk.Selected)
                    {
                        siRik = siRik + chk.Value.ToString() + ",";
                    }
                }

                string sEstatus = this.drpEstatus.SelectedItem.Value;

                clsdshIndicadoresACyS.ConsultaDashboardPorRIKACyS(sesion.Id_Emp, sesion.Id_Cd, sesion.Id_U, iRik, siRik, sEstatus, sesion.Emp_Cnx,
                    ref ListResumen, ref ListbaseCliente,
                    ref ClientesCon, ref ClientesSin, ref DetalleAcyS);

                ExcelResumen = new List<DashboardACyS_Resumen>();
                ExcelResumen = ListResumen;

                ExcelBaseDeRiks = new List<DashboardACyS_RIKS>();
                ExcelBaseDeRiks = ListbaseCliente;

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

        void AvanceDetalladoPorRIKS(List<DashboardACyS_Resumen> ListaResumen,
            List<DashboardACyS_RIKS> ListaDeRIKS,
            List<DashboardACyS_Clientes> ListaClientesCon, List<DashboardACyS_Clientes> ListaClientesSin,
            List<DashboardACyS_DetalleACyS> ListaDeDetalleACyS)
        {
            string filenombre = "ACySAvanceDetalladoPorRIKS_" + DateTime.UtcNow.ToShortDateString().Replace("/", "") + "h" + DateTime.UtcNow.ToShortTimeString().Replace(".", "").Replace(":", "").Replace(" ", "");
            try
            {
                var wb = new XLWorkbook();

                var ws = wb.Worksheets.Add("CDI_" + drpCDI.SelectedItem.Text);
                FormatoAvanceDetallado("CDI: " + drpCDI.SelectedItem.Text, ListaResumen.Where(i => i.IdRik == 0).ToList(), ListaClientesCon, ListaClientesSin, ListaDeDetalleACyS, ref ws);

                foreach(DashboardACyS_RIKS rik in ListaDeRIKS)
                {
                    ws = wb.Worksheets.Add("RIK_" + rik.IdRik);
                    FormatoAvanceDetallado(rik.Rik_Nombre ,ListaResumen.Where(i => i.IdRik == rik.IdRik).ToList(), 
                        ListaClientesCon.Where(i => i.IdRik == rik.IdRik).ToList(), 
                        ListaClientesSin.Where(i => i.IdRik == rik.IdRik).ToList(), 
                        ListaDeDetalleACyS.Where(i => i.Rik == rik.IdRik).ToList(), ref ws);
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


        #region Eventos

        protected void drpCDI_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }

                CN__Comun comun = new CN__Comun();
                /// comun.CambiarCdVer(, ref sesion);
                sesion.Id_Cd_Ver = Convert.ToInt32(drpCDI.SelectedItem.Value);
                sesion.Cd_Nombre = drpCDI.SelectedItem.Text;
                CN_CatCalendario cn_catcalendario = new CN_CatCalendario();
                Calendario calendario = new Calendario();
                cn_catcalendario.ConsultaCalendarioActual(ref calendario, sesion);
                sesion.CalendarioIni = calendario.Cal_FechaIni;
                sesion.CalendarioFin = calendario.Cal_FechaFin;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "drpCDI_SelectedIndexChanged");
            }
        }

        protected void drpRIKs_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListItem iitem = this.drpRIKs.SelectedItem;
            if (iitem != null)
            {
                llenaDashboard();
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
            if (chkDetalleXRIK.Checked == false)
            {
                /// Es una corrida directa, solo la pagina de TOTAL
                if (this.chkDetalleXRIK.Checked == false)
                {
                    llenaDashboard();
                    AvanceDetallado("Total", ExcelResumen, ExcelClientesCon, ExcelClientesSin, ExcelDetalleACyS);
                }
            }
            else
            {
                /// Es una corrida separada por RIK
                llenaDashboard();
                llenaDatosPorRik();
                AvanceDetalladoPorRIKS(ExcelResumen, ExcelBaseDeRiks, ExcelClientesCon, ExcelClientesSin, ExcelDetalleACyS);
            }
        }

        protected void imgRptCumplimiento_Click(object sender, ImageClickEventArgs e)
        {
            llenaDashboard();
            if (chkDetalleXRIKCumplimiento.Checked)
            {
                llenaDatosPorRik();
            }
            CumplimientoCaptura("Cumplimiento", ListadoDeRIKs, ExcelResumen, ExcelDeEstatus, ExcelDetalleACyS);
        }

        protected void btnAvance_Click(object sender, EventArgs e)
        {
            /// Es una corrida directa, solo la pagina de TOTAL
            if (this.chkDetalleXRIK.Checked == false)
            {
                llenaDashboard();
                AvanceDetallado("Total", ExcelResumen, ExcelClientesCon, ExcelClientesSin, ExcelDetalleACyS);
            }
            else
            {   
                /// Es una corrida separada por RIK
                llenaDashboard();
                llenaDatosPorRik();
                AvanceDetalladoPorRIKS(ExcelResumen, ExcelBaseDeRiks, ExcelClientesCon, ExcelClientesSin, ExcelDetalleACyS);
            }
        }

        protected void btnCumplimiento_Click(object sender, EventArgs e)
        {
            llenaDashboard();
            if (chkDetalleXRIKCumplimiento.Checked)
            {
                llenaDatosPorRik();
            }
            CumplimientoCaptura("Cumplimiento", ListadoDeRIKs, ExcelResumen, ExcelDeEstatus, ExcelDetalleACyS);
        }

        protected void btnActualiza_Click(object sender, EventArgs e)
        {
            llenaDashboard();
        }

        #endregion

    }
}