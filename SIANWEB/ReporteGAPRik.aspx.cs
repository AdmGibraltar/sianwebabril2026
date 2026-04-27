using CapaEntidad;
using CapaNegocios;
using DevExpress.Export;
using DevExpress.Web;
using DevExpress.Web.Bootstrap;
using DevExpress.XtraPrinting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Web.UI;

namespace SIANWEB
{
    public partial class ReporteGAPRik : System.Web.UI.Page
    {

        public DataTable dt
        {
            get
            {
                return (DataTable)ViewState["dtReporteConsultaGAPRik"];
            }
            set
            {
                ViewState["dtReporteConsultaGAPRik"] = value;

            }
        }


        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private Sesion sesion { get { return (Sesion)Session["Sesion" + Session.SessionID]; } set { Session["Sesion" + Session.SessionID] = value; } }



        protected void page_init(object sender, EventArgs e)
        {
            if (Session["dtReporteConsultaGAPRik"] != null)
            {

                //List<PreciadorSegmentoTamaño> ListaPreciadorSegmentoTamaño = new List<PreciadorSegmentoTamaño>();
                //ListaPreciadorSegmentoTamaño = (List<PreciadorSegmentoTamaño>)Session["dtPreciadorClienteSegmento"];
                //grid.DataSource = ListaPreciadorSegmentoTamaño;
                //grid.DataBind();
                List<ReporteGAP> listreporteGAP = new List<ReporteGAP>();

                dt = (DataTable)Session["dtReporteConsultaGAPRik"];
                grdServicio.DataSource = dt;
                grdServicio.DataBind();

            }

        }



        protected void Page_Load(object sender, EventArgs e)
        {
            // GridViewFeaturesHelper.SetupGlobalGridViewBehavior(grid);



            if (!IsPostBack)
            {
                grdServicio.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "Ventas").DisplayFormat = "Total : {0:N2}";
                grdServicio.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "VentasPO").DisplayFormat = "Total : {0:N2}";
                grdServicio.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "GAPIngresos_Monto").DisplayFormat = "Total : {0:N2}";

                // Agregar el total personalizado para la columna de GapIngresosPorcentaje
                ASPxSummaryItem customSummaryItem = new ASPxSummaryItem
                {
                    SummaryType = DevExpress.Data.SummaryItemType.Custom,
                    FieldName = "GAPIngresos_Porc",
                    ShowInColumn = "GAPIngresos_Porc", // Asegúrate de que coincida con la columna de porcentaje
                    DisplayFormat = "Total: {0:N2}%"
                };
                grdServicio.TotalSummary.Add(customSummaryItem);

                // Suscribir el evento CustomSummaryCalculate
                grdServicio.CustomSummaryCalculate += grdServicio_CustomSummaryCalculate;


                if (Session["dtReporteConsultaGAPRik"] != null)
                {
                    Session["dtReporteConsultaGAPRik"] = null;
                }


                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion != null)
                {

                    inicializar();
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }

               


                ////cmbTipoReporte.SelectedIndex = 0;
                //CmbSucursal.SelectedIndex = 0;
            }
        }


        protected void grdServicio_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {

            if (e.Item is ASPxSummaryItem summaryItem && summaryItem.FieldName == "GAPIngresos_Porc"
        && e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
            {
                // Obtener los totales de Ventas y GapIngresos
                var totalVentasPO = Convert.ToDecimal(grdServicio.GetTotalSummaryValue(grdServicio.TotalSummary[1]) ?? 0);
                var totalGapIngresos = Convert.ToDecimal(grdServicio.GetTotalSummaryValue(grdServicio.TotalSummary[2]) ?? 0);

                if (totalVentasPO != 0)
                {
                    var porcentaje = (totalGapIngresos / totalVentasPO) * 100;
                    e.TotalValue = porcentaje;
                }
                else
                {
                    e.TotalValue = 0; // Evitar división por cero
                }
            }

        }


        private void inicializar()
        {

            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionSIANCentral"];

            //CN_Comun.DevLlenaCombo(6, Conexion, "spCatCDI_ComboExt", ref CmbSucursal);
            CN_Comun.DevLlenaCombo(1, sesion.Id_Emp, 100, Conexion, "spCatCalendarioAnhio2_Combo", ref this.CmbAnio);
            CN_Comun.DevLlenaCombo(1, sesion.Id_Emp, 100, sesion.CalendarioIni.Year, Conexion, "spCatCalendarioMes_Combo", ref this.CmbMes);

            this.CmbAnio.Value = DateTime.Now.Year.ToString();
           
            if (DateTime.Now.Month == 1)
            {
                this.CmbAnio.Value = (DateTime.Now.Year - 1).ToString();
                this.CmbMes.Value = "12";
            }
            else
            {
                this.CmbMes.Value = (DateTime.Now.Month - 1).ToString();
            }
            ;
            CargarRepresentantes();

            cmbRik.Value = "-1";

            //CmbSucursal.Items.Add("--Todas--", "-1");

            //CmbSucursal.Value = "-1";
            CmbTamaño.Items.Add("--Todos--", "-1");
            CmbTamaño.Items.Add("A", "A");
            CmbTamaño.Items.Add("B", "B");
            CmbTamaño.Items.Add("C", "C");
            CmbTamaño.Items.Add("D", "D");
            CmbTamaño.Items.Add("E", "E");
            CmbTamaño.Value = "-1";


            //si el usuario es rik no cargar el idrik de los parametros 
            //se queda el el del rik ( en cargarepresentantes lo asigne ) 
            if (sesion.Id_TU != 2)
            {
                if ((Request.QueryString["Id"] != null && Request.QueryString["Id"] != "-1") &&
               (Request.QueryString["Año"] != null && Request.QueryString["Año"] != "-1") &&
               (Request.QueryString["Mes"] != null && Request.QueryString["Mes"] != "-1"))
                {
                    txtRik.Text = Request.QueryString["Id"];
                    //cmbRik.SelectedItem.Value = txtRik.Text;
                    cmbRik.Value = txtRik.Text;
                    int año = 0;
                    año = Convert.ToInt32(Request.QueryString["año"]);
                    CmbAnio.SelectedItem.Value = año;

                    int mes = 0;
                    mes = Convert.ToInt32(Request.QueryString["mes"]);
                    //CmbMes.SelectedItem.Value = mes;
                    this.CmbMes.SelectedIndex = mes;
                    cargarDatos();
                }
            }
        }

        #region crear archivo de excell

        protected void btnGenerarEx(object sender, EventArgs e)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            switch (DemoExportFormat.Xlsx)
            {
                //case DemoExportFormat.Pdf:
                //    gridExport.WritePdfToResponse();
                //    break;

                case DemoExportFormat.Xlsx:
                    gridExport.WriteXlsxToResponse(new XlsxExportOptionsEx { ExportType = ExportType.WYSIWYG });
                    break;
                    //case DemoExportFormat.Rtf:
                    //    gridExport.WriteRtfToResponse();
                    //    break;
                    //case DemoExportFormat.Csv:
                    //    gridExport.WriteCsvToResponse(new CsvExportOptionsEx() { ExportType = ExportType.WYSIWYG });
                    //    break;
            }



            //if (bcmTipoReporte.Value.ToString() == "1")
            //{
            //    fechaInicial = fechaInicial.AddMonths(-3);
            //    fechafinal = fechafinal.AddDays(-1);
            //}

            //if (bcmTipoReporte.Value.ToString() == "2")
            //{
            //    fechaInicial = DateTime.Parse(txtfechaIniciaGR.Value.ToString());
            //    fechafinal = DateTime.Parse(txtfechafinalGR.Value.ToString());
            //}

            //if (fechaInicial > fechafinal)
            //{
            //    mensaje("La fecha inicial es mayor a la fecha final de la sección de observar totales.");
            //    return;
            //}

            //traer datos de la BD 


            ///Si se van a generar lo detalles de cada rik ejecuto esta función 
            ///
            //if (cmbTipoReporte.Value.ToString() != "-1")
            //{
            //    btnGenerarReportesRiks();
            //}
            //else
            //{

            //    try
            //    {
            //        SistemaCompensacionGetXML confsistcompensacion = new SistemaCompensacionGetXML();
            //        List<PreciadorSegmentoCliente> List = new List<PreciadorSegmentoCliente>();


            //        CN_CatCompensacion cn = new CN_CatCompensacion();

            //        confsistcompensacion.Id_Emp = Sesion.Id_Emp;
            //        confsistcompensacion.Anio = Convert.IsDBNull(CmbAnio.Value) ? 2023 : Convert.ToInt32(CmbAnio.Value.ToString());
            //        confsistcompensacion.Mes = Convert.IsDBNull(CmbMes.Value) ? 8 : Convert.ToInt32(CmbMes.Value.ToString());
            //        confsistcompensacion.Id_Cd = Convert.IsDBNull(CmbSucursal.Value) ? -1 : Convert.ToInt32(CmbSucursal.Value.ToString());
            //        //confsistcompensacion.Id_TipoRepresentante = Convert.IsDBNull(CmbSegmento.Value) ? -1 : Convert.ToInt32(CmbSegmento.Value.ToString());
            //        confsistcompensacion.Id_Sistema = 23;
            //        confsistcompensacion.Id_TipoRepresentante = Convert.ToInt32(CmbTipoRepresentante.Value.ToString());
            //        confsistcompensacion.Id_TipoRepresentante = 3;

            //        if (txtRik.Text == "")
            //        {
            //            confsistcompensacion.Id_Rik = -1; //Convert.IsDBNull(txtRik.Text) ? -1 : Convert.ToInt32(txtRik.Text);
            //        }
            //        else
            //        {
            //            confsistcompensacion.Id_Rik = Convert.ToInt32(txtRik.Text);
            //        }

            //        string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];


            //        DataSet dsEstructuraSegmento = new DataSet();
            //        List<ReporteComisiones> lstr = new List<ReporteComisiones>();

            //        cn.ConsultaListaDiferenciado(confsistcompensacion, ref dsEstructuraSegmento, Conexion);


            //        string nombre = "";
            //        int i = 6;  // = renglón 
            //        int c = 1;
            //        using (var workbook = new XLWorkbook())
            //        {

            //            //nombre = dt.Rows[0].Field<string>("Cd_Nombre");
            //            nombre = "Resumen";
            //            var HojaExcel = workbook.Worksheets.Add(nombre);

            //            DataTable dtResumen = new DataTable();
            //            DataTable dtClientes = new DataTable();
            //            DataTable dtFacturas = new DataTable();
            //            DataTable dtProductos = new DataTable();

            //            dtResumen = dsEstructuraSegmento.Tables[0];
            //            dtClientes = dsEstructuraSegmento.Tables[1];
            //            dtFacturas = dsEstructuraSegmento.Tables[2];
            //            dtProductos = dsEstructuraSegmento.Tables[3];
            //            nombre = "inicial";

            //            #region encabezados
            //            HojaExcel.Cell("A1").Value = "Reporte de comisiones Diferenciadas";
            //            HojaExcel.Cell("A2").Value = "Año";
            //            HojaExcel.Cell("B2").Value = confsistcompensacion.Anio;
            //            HojaExcel.Cell("A3").Value = "Mes";
            //            HojaExcel.Cell("B3").Value = confsistcompensacion.Mes;

            //            HojaExcel.Range("A5", "AH5").Style.Font.Bold = true;
            //            HojaExcel.Range("A5", "AH5").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            //            HojaExcel.Range("A5", "AH5").Style.Font.FontColor = XLColor.White;

            //            HojaExcel.Cell("A5").Value = "Num \r\nSucursal";
            //            HojaExcel.Cell("A5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaExcel.Cell("A5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaExcel.Cell("A5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaExcel.Cell("A5").Style.Font.Bold = true;
            //            HojaExcel.Cell("A5").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

            //            HojaExcel.Cell("B5").Value = "Nombre\r\nSucursal";
            //            HojaExcel.Cell("B5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaExcel.Cell("B5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaExcel.Cell("B5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


            //            HojaExcel.Cell("C5").Value = "Num \r\nRik";
            //            HojaExcel.Cell("C5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaExcel.Cell("C5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaExcel.Cell("C5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            //            HojaExcel.Cell("D5").Value = "Nombre \r\n del Rik";
            //            HojaExcel.Cell("D5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaExcel.Cell("D5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaExcel.Cell("D5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            //            HojaExcel.Cell("E5").Value = "Total Venta \r\nCobrada";
            //            HojaExcel.Cell("E5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaExcel.Cell("E5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaExcel.Cell("E5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


            //            HojaExcel.Cell("F5").Value = "Utilidad \r\n Prima";
            //            HojaExcel.Cell("F5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaExcel.Cell("F5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaExcel.Cell("F5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            //            HojaExcel.Cell("G5").Value = "Utilidad \r\n Bruta";
            //            HojaExcel.Cell("G5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaExcel.Cell("G5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaExcel.Cell("G5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            //            HojaExcel.Cell("H5").Value = "Comisión Base";
            //            HojaExcel.Cell("H5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaExcel.Cell("H5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaExcel.Cell("H5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


            //            HojaExcel.Cell("I5").Value = "Multiplicador";
            //            HojaExcel.Cell("I5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaExcel.Cell("I5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaExcel.Cell("I5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            //            HojaExcel.Cell("J5").Value = "Comisión base \r\ncon multiplicador";
            //            HojaExcel.Cell("J5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaExcel.Cell("J5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaExcel.Cell("J5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaExcel.Cell("J5").Style.Font.FontColor = XLColor.White;

            //            HojaExcel.Cell("K5").Value = "Gasto\r\nAdministrativo";
            //            HojaExcel.Cell("K5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaExcel.Cell("K5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaExcel.Cell("K5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaExcel.Cell("K5").Style.Font.FontColor = XLColor.White;

            //            HojaExcel.Cell("L5").Value = "Comisión\r\n Neta";
            //            HojaExcel.Cell("L5").Style.Fill.BackgroundColor = XLColor.Black;
            //            HojaExcel.Cell("L5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaExcel.Cell("L5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaExcel.Cell("L5").Style.Font.FontColor = XLColor.White;

            //            //HojaExcel.Cell("M5").Value = "Comisión\r\nFinal";
            //            //HojaExcel.Cell("M5").Style.Fill.BackgroundColor = XLColor.Black;
            //            //HojaExcel.Cell("M5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            //HojaExcel.Cell("M5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            //HojaExcel.Cell("M5").Style.Font.FontColor = XLColor.White;

            //            #endregion encabezados


            //            foreach (DataRow lista2 in dtResumen.Rows)
            //            {

            //                //aqui iban los comentarios  

            //                #region agrego los datos 
            //                //Renglón i  comienzo en 6 esa es mi primer fila de datos

            //                for (int ir = 1; ir <= lista2.ItemArray.Length; ir++)
            //                {
            //                    HojaExcel.Cell(i, ir).Value = lista2[ir - 1].ToString();
            //                    if (ir > 4)
            //                    {
            //                        HojaExcel.Cell(i, ir).Style.NumberFormat.Format = "####,##0.00";
            //                        HojaExcel.Cell(i, ir).DataType = XLDataType.Number;
            //                    }

            //                }

            //                #endregion datos


            //                i++;

            //            }
            //            HojaExcel.Columns().AdjustToContents();


            //            nombre = "Clientes";
            //            var HojaCliente = workbook.Worksheets.Add(nombre);

            //            #region clientes

            //            i = 6;  // = renglón 
            //            c = 1;


            //            #region encabezados

            //            HojaCliente.Range("A5", "AH5").Style.Font.Bold = true;
            //            HojaCliente.Range("A5", "AH5").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            //            HojaCliente.Range("A5", "AH5").Style.Font.FontColor = XLColor.White;

            //            HojaCliente.Cell("A5").Value = "Num \r\nSucursal";
            //            HojaCliente.Cell("A5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaCliente.Cell("A5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaCliente.Cell("A5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaCliente.Cell("A5").Style.Font.Bold = true;
            //            HojaCliente.Cell("A5").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

            //            HojaCliente.Cell("B5").Value = "Nombre\r\nSucursal";
            //            HojaCliente.Cell("B5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaCliente.Cell("B5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaCliente.Cell("B5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            //            HojaCliente.Cell("C5").Value = "Num \r\nCliente";
            //            HojaCliente.Cell("C5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaCliente.Cell("C5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaCliente.Cell("C5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            //            HojaCliente.Cell("D5").Value = "Nombre \r\n del Cliente";
            //            HojaCliente.Cell("D5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaCliente.Cell("D5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaCliente.Cell("D5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            //            HojaCliente.Cell("E5").Value = "Tamaño";
            //            HojaCliente.Cell("E5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaCliente.Cell("E5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaCliente.Cell("E5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            //            HojaCliente.Cell("F5").Value = "Num \r\nRik";
            //            HojaCliente.Cell("F5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaCliente.Cell("F5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaCliente.Cell("F5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            //            //HojaCliente.Cell("D5").Value = "Nombre /\r\n del Rik";
            //            //HojaCliente.Cell("D5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            //HojaCliente.Cell("D5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            //HojaCliente.Cell("D5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            //            HojaCliente.Cell("G5").Value = "Total Venta \r\nCobrada";
            //            HojaCliente.Cell("G5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaCliente.Cell("G5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaCliente.Cell("G5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


            //            HojaCliente.Cell("H5").Value = "Utilidad \r\n Prima";
            //            HojaCliente.Cell("H5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaCliente.Cell("H5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaCliente.Cell("H5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


            //            HojaCliente.Cell("I5").Value = "Amortización";
            //            HojaCliente.Cell("I5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaCliente.Cell("I5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaCliente.Cell("I5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            //            HojaCliente.Cell("J5").Value = "Gasto\r\nAdministrativo";
            //            HojaCliente.Cell("J5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaCliente.Cell("J5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaCliente.Cell("J5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaCliente.Cell("J5").Style.Font.FontColor = XLColor.White;

            //            HojaCliente.Cell("K5").Value = "Utilidad \r\n Bruta";
            //            HojaCliente.Cell("K5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaCliente.Cell("K5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaCliente.Cell("K5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaCliente.Cell("K5").Style.Font.FontColor = XLColor.White;

            //            HojaCliente.Cell("L5").Value = "Ajuste por\r\n Cobranza";
            //            HojaCliente.Cell("L5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaCliente.Cell("L5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaCliente.Cell("L5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaCliente.Cell("L5").Style.Font.FontColor = XLColor.White;

            //            HojaCliente.Cell("M5").Value = "UB\r\nAjustada";
            //            HojaCliente.Cell("M5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaCliente.Cell("M5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaCliente.Cell("M5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaCliente.Cell("M5").Style.Font.FontColor = XLColor.White;

            //            HojaCliente.Cell("N5").Value = "Comisión \r\n UP";
            //            HojaCliente.Cell("N5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaCliente.Cell("N5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaCliente.Cell("N5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaCliente.Cell("N5").Style.Font.FontColor = XLColor.White;

            //            HojaCliente.Cell("O5").Value = "Comisión \r\n UB "; // + confsistcompensacion.Anio.ToString();
            //            HojaCliente.Cell("O5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaCliente.Cell("O5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaCliente.Cell("O5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaCliente.Cell("O5").Style.Font.FontColor = XLColor.White;

            //            HojaCliente.Cell("P5").Value = "Utilidad \r\n Remanente";
            //            HojaCliente.Cell("P5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaCliente.Cell("P5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaCliente.Cell("P5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaCliente.Cell("P5").Style.Font.FontColor = XLColor.White;

            //            HojaCliente.Cell("Q5").Value = "Comisión\r\n Base"; // + confsistcompensacion.Anio.ToString();
            //            HojaCliente.Cell("Q5").Style.Fill.BackgroundColor = XLColor.Black;
            //            HojaCliente.Cell("Q5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaCliente.Cell("Q5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaCliente.Cell("Q5").Style.Font.FontColor = XLColor.White;

            //            HojaCliente.Cell("R5").Value = "Año";
            //            HojaCliente.Cell("R5").Style.Fill.BackgroundColor = XLColor.MediumOrchid;
            //            HojaCliente.Cell("R5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaCliente.Cell("R5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaCliente.Cell("R5").Style.Font.FontColor = XLColor.White;

            //            HojaCliente.Cell("S5").Value = "Mes";
            //            HojaCliente.Cell("S5").Style.Fill.BackgroundColor = XLColor.MediumOrchid;
            //            HojaCliente.Cell("S5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaCliente.Cell("S5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaCliente.Cell("S5").Style.Font.FontColor = XLColor.White;

            //            #endregion encabezados

            //            foreach (DataRow lista2 in dtClientes.Rows)
            //            {


            //                #region agrego los datos 
            //                for (int ir = 1; ir <= lista2.ItemArray.Length; ir++)
            //                {
            //                    HojaCliente.Cell(i, ir).Value = lista2[ir - 1].ToString();

            //                    if (ir > 7 && ir < 17)
            //                    {
            //                        if (ir != (int)13)
            //                        {
            //                            HojaCliente.Cell(i, ir - 1).Style.NumberFormat.Format = "####,##0.00";
            //                            HojaCliente.Cell(i, ir - 1).DataType = XLDataType.Number;

            //                        }
            //                    }
            //                    if (ir == 18)
            //                    {
            //                        HojaCliente.Cell(i, ir - 1).Style.NumberFormat.Format = "####,##0.00";
            //                        HojaCliente.Cell(i, ir - 1).DataType = XLDataType.Number;
            //                    }



            //                }
            //                #endregion datos

            //                i++;

            //            }

            //            #endregion clientes
            //            HojaCliente.Columns().AdjustToContents();


            //            #region facturas 


            //            nombre = "Facturas";
            //            var HojaFactura = workbook.Worksheets.Add(nombre);

            //            i = 6;  // = renglón 
            //            c = 1;


            //            #region encabezados

            //            HojaFactura.Range("A5", "AH5").Style.Font.Bold = true;
            //            HojaFactura.Range("A5", "AH5").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            //            HojaFactura.Range("A5", "AH5").Style.Font.FontColor = XLColor.White;

            //            HojaFactura.Cell("A5").Value = "Num \r\nSucursal";
            //            HojaFactura.Cell("A5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaFactura.Cell("A5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaFactura.Cell("A5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaFactura.Cell("A5").Style.Font.Bold = true;

            //            HojaFactura.Cell("B5").Value = "Nombre\r\nSucursal";
            //            HojaFactura.Cell("B5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaFactura.Cell("B5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaFactura.Cell("B5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            //            HojaFactura.Cell("C5").Value = "Referencia de Pago";
            //            HojaFactura.Cell("C5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaFactura.Cell("C5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaFactura.Cell("C5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaFactura.Cell("C5").Style.Font.Bold = true;

            //            HojaFactura.Cell("D5").Value = "Num \r\nCliente";
            //            HojaFactura.Cell("D5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaFactura.Cell("D5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaFactura.Cell("D5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            //            HojaFactura.Cell("E5").Value = "Nombre \r\n del Cliente";
            //            HojaFactura.Cell("E5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaFactura.Cell("E5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaFactura.Cell("E5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            //            HojaFactura.Cell("F5").Value = "Tamaño";
            //            HojaFactura.Cell("F5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaFactura.Cell("F5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaFactura.Cell("F5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            //            //HojaFactura.Cell("D5").Value = "Nombre /\r\n del Rik";
            //            //HojaFactura.Cell("D5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            //HojaFactura.Cell("D5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            //HojaFactura.Cell("D5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaFactura.Cell("G5").Value = "Territorio";
            //            HojaFactura.Cell("G5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaFactura.Cell("G5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaFactura.Cell("G5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            //            HojaFactura.Cell("H5").Value = "Num \r\nRik";
            //            HojaFactura.Cell("H5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaFactura.Cell("H5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaFactura.Cell("H5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            //            HojaFactura.Cell("I5").Value = "Fecha \r\n Factura";
            //            HojaFactura.Cell("I5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaFactura.Cell("I5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaFactura.Cell("I5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            //            HojaFactura.Cell("J5").Value = "Fecha \r\n Vencimiento";
            //            HojaFactura.Cell("J5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaFactura.Cell("J5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaFactura.Cell("J5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            //            HojaFactura.Cell("K5").Value = "Fecha de\r\n Pago";
            //            HojaFactura.Cell("K5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaFactura.Cell("K5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaFactura.Cell("K5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaFactura.Cell("K5").Style.Font.FontColor = XLColor.White;


            //            HojaFactura.Cell("L5").Value = "Días";
            //            HojaFactura.Cell("L5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaFactura.Cell("L5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaFactura.Cell("L5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaFactura.Cell("L5").Style.Font.FontColor = XLColor.White;


            //            HojaFactura.Cell("M5").Value = "Venta \r\nCobrada";
            //            HojaFactura.Cell("M5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaFactura.Cell("M5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaFactura.Cell("M5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaFactura.Cell("M5").Style.Font.FontColor = XLColor.White;

            //            HojaFactura.Cell("N5").Value = "Utilidad \r\n Prima";
            //            HojaFactura.Cell("N5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaFactura.Cell("N5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaFactura.Cell("N5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaFactura.Cell("N5").Style.Font.FontColor = XLColor.White;

            //            HojaFactura.Cell("O5").Value = "Multiplo Aj \r\n Cobranza ";
            //            HojaFactura.Cell("O5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaFactura.Cell("O5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaFactura.Cell("O5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaFactura.Cell("O5").Style.Font.FontColor = XLColor.White;

            //            HojaFactura.Cell("P5").Value = "Ajuste \r\n Cobranza  ";
            //            HojaFactura.Cell("P5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaFactura.Cell("P5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaFactura.Cell("P5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaFactura.Cell("P5").Style.Font.FontColor = XLColor.White;

            //            HojaFactura.Cell("Q5").Value = "Comisión\r\n UP Ajustada";
            //            HojaFactura.Cell("Q5").Style.Fill.BackgroundColor = XLColor.Black;
            //            HojaFactura.Cell("Q5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaFactura.Cell("Q5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaFactura.Cell("Q5").Style.Font.FontColor = XLColor.White;

            //            HojaFactura.Cell("R5").Value = "Año";
            //            HojaFactura.Cell("R5").Style.Fill.BackgroundColor = XLColor.MediumOrchid;
            //            HojaFactura.Cell("R5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaFactura.Cell("R5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaFactura.Cell("R5").Style.Font.FontColor = XLColor.White;

            //            HojaFactura.Cell("S5").Value = "Mes";
            //            HojaFactura.Cell("S5").Style.Fill.BackgroundColor = XLColor.MediumOrchid;
            //            HojaFactura.Cell("S5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaFactura.Cell("S5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaFactura.Cell("S5").Style.Font.FontColor = XLColor.White;

            //            #endregion encabezados

            //            foreach (DataRow lista2 in dtFacturas.Rows)
            //            {


            //                #region agrego los datos 

            //                for (int ir = 1; ir <= lista2.ItemArray.Length; ir++)
            //                {
            //                    HojaFactura.Cell(i, ir).Value = lista2[ir - 1].ToString();

            //                    //if ( (Type)lista2[ir - 1].GetType() = ;

            //                    if (ir == 14 || ir == 15 || ir == 17 || ir == 18)
            //                    {

            //                        HojaFactura.Cell(i, ir - 1).Style.NumberFormat.Format = "####,##0.00";
            //                        HojaFactura.Cell(i, ir - 1).DataType = XLDataType.Number;
            //                    }




            //                }
            //                #endregion datos

            //                i++;

            //            }

            //            HojaFactura.Columns().AdjustToContents();


            //            #endregion facturas

            //            #region Productos 
            //            nombre = "Productos";
            //            var HojaProducto = workbook.Worksheets.Add(nombre);

            //            i = 6;  // = renglón 
            //            c = 1;
            //            #region encabezados

            //            HojaProducto.Range("A5", "AH5").Style.Font.Bold = true;
            //            HojaProducto.Range("A5", "AH5").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            //            HojaProducto.Range("A5", "AH5").Style.Font.FontColor = XLColor.White;

            //            HojaProducto.Cell("A5").Value = "Num \r\nSucursal";
            //            HojaProducto.Cell("A5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaProducto.Cell("A5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaProducto.Cell("A5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaProducto.Cell("A5").Style.Font.Bold = true;

            //            HojaProducto.Cell("B5").Value = "Nombre\r\nSucursal";
            //            HojaProducto.Cell("B5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaProducto.Cell("B5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaProducto.Cell("B5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            //            HojaProducto.Cell("C5").Value = "Referencia de Pago";
            //            HojaProducto.Cell("C5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaProducto.Cell("C5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaProducto.Cell("C5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaProducto.Cell("C5").Style.Font.Bold = true;

            //            HojaProducto.Cell("D5").Value = "Num \r\nCliente";
            //            HojaProducto.Cell("D5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaProducto.Cell("D5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaProducto.Cell("D5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            //            HojaProducto.Cell("E5").Value = "Nombre \r\n del Cliente";
            //            HojaProducto.Cell("E5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaProducto.Cell("E5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaProducto.Cell("E5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            //            HojaProducto.Cell("F5").Value = "Tamaño";
            //            HojaProducto.Cell("F5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaProducto.Cell("F5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaProducto.Cell("F5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            //            HojaProducto.Cell("G5").Value = "Territorio";
            //            HojaProducto.Cell("G5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaProducto.Cell("G5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaProducto.Cell("G5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            //            HojaProducto.Cell("H5").Value = "Num \r\nRik";
            //            HojaProducto.Cell("H5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaProducto.Cell("H5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaProducto.Cell("H5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            //            HojaProducto.Cell("I5").Value = "Cantidad \r\n Facturada";
            //            HojaProducto.Cell("I5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaProducto.Cell("I5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaProducto.Cell("I5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            //            HojaProducto.Cell("J5").Value = "Producto";
            //            HojaProducto.Cell("J5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaProducto.Cell("J5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaProducto.Cell("J5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            //            HojaProducto.Cell("K5").Value = "Descripción";
            //            HojaProducto.Cell("K5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaProducto.Cell("K5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaProducto.Cell("K5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaProducto.Cell("K5").Style.Font.FontColor = XLColor.White;


            //            HojaProducto.Cell("L5").Value = "Convenio \r\nProveedor";
            //            HojaProducto.Cell("L5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaProducto.Cell("L5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaProducto.Cell("L5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaProducto.Cell("L5").Style.Font.FontColor = XLColor.White;


            //            HojaProducto.Cell("M5").Value = "Convenio \r\nKey";
            //            HojaProducto.Cell("M5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaProducto.Cell("M5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaProducto.Cell("M5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaProducto.Cell("M5").Style.Font.FontColor = XLColor.White;

            //            HojaProducto.Cell("N5").Value = "Clasificación";
            //            HojaProducto.Cell("N5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaProducto.Cell("N5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaProducto.Cell("N5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaProducto.Cell("N5").Style.Font.FontColor = XLColor.White;

            //            HojaProducto.Cell("O5").Value = "Nivel de \r\n Precio ";
            //            HojaProducto.Cell("O5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaProducto.Cell("O5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaProducto.Cell("O5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaProducto.Cell("O5").Style.Font.FontColor = XLColor.White;

            //            HojaProducto.Cell("P5").Value = "Precio de \r\n Venta  ";
            //            HojaProducto.Cell("P5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaProducto.Cell("P5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaProducto.Cell("P5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaProducto.Cell("P5").Style.Font.FontColor = XLColor.White;

            //            HojaProducto.Cell("Q5").Value = "Utilidad\r\n Prima";
            //            HojaProducto.Cell("Q5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaProducto.Cell("Q5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaProducto.Cell("Q5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaProducto.Cell("Q5").Style.Font.FontColor = XLColor.White;

            //            HojaProducto.Cell("R5").Value = "Porc.\r\n Comisión";
            //            HojaProducto.Cell("R5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
            //            HojaProducto.Cell("R5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaProducto.Cell("R5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaProducto.Cell("R5").Style.Font.FontColor = XLColor.White;

            //            HojaProducto.Cell("S5").Value = "Comisión\r\n UP";
            //            HojaProducto.Cell("S5").Style.Fill.BackgroundColor = XLColor.Black;
            //            HojaProducto.Cell("S5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaProducto.Cell("S5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaProducto.Cell("S5").Style.Font.FontColor = XLColor.White;

            //            HojaProducto.Cell("T5").Value = "Año";
            //            HojaProducto.Cell("T5").Style.Fill.BackgroundColor = XLColor.MediumOrchid;
            //            HojaProducto.Cell("T5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaProducto.Cell("T5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaProducto.Cell("T5").Style.Font.FontColor = XLColor.White;

            //            HojaProducto.Cell("U5").Value = "Mes";
            //            HojaProducto.Cell("U5").Style.Fill.BackgroundColor = XLColor.MediumOrchid;
            //            HojaProducto.Cell("U5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaProducto.Cell("U5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaProducto.Cell("U5").Style.Font.FontColor = XLColor.White;

            //            HojaProducto.Cell("V5").Value = "PrecioObjetivo";
            //            HojaProducto.Cell("V5").Style.Fill.BackgroundColor = XLColor.AshGrey;
            //            HojaProducto.Cell("V5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaProducto.Cell("V5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaProducto.Cell("V5").Style.Font.FontColor = XLColor.Black;

            //            HojaProducto.Cell("W5").Value = "Precio\r\n MinimoRik";
            //            HojaProducto.Cell("W5").Style.Fill.BackgroundColor = XLColor.AshGrey;
            //            HojaProducto.Cell("W5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaProducto.Cell("W5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaProducto.Cell("W5").Style.Font.FontColor = XLColor.Black;

            //            HojaProducto.Cell("X5").Value = "Precio\r\n MinimoGte";
            //            HojaProducto.Cell("X5").Style.Fill.BackgroundColor = XLColor.AshGrey;
            //            HojaProducto.Cell("X5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaProducto.Cell("X5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaProducto.Cell("X5").Style.Font.FontColor = XLColor.Black;

            //            HojaProducto.Cell("Y5").Value = "Precio\r\n Lista";
            //            HojaProducto.Cell("Y5").Style.Fill.BackgroundColor = XLColor.AshGrey;
            //            HojaProducto.Cell("Y5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaProducto.Cell("Y5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaProducto.Cell("Y5").Style.Font.FontColor = XLColor.Black;

            //            HojaProducto.Cell("Z5").Value = "Precio\r\n Minimo";
            //            HojaProducto.Cell("Z5").Style.Fill.BackgroundColor = XLColor.AshGrey;
            //            HojaProducto.Cell("Z5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaProducto.Cell("Z5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaProducto.Cell("Z5").Style.Font.FontColor = XLColor.Black;

            //            HojaProducto.Cell("AA5").Value = "Precio\r\n AAA";
            //            HojaProducto.Cell("AA5").Style.Fill.BackgroundColor = XLColor.AshGrey;
            //            HojaProducto.Cell("AA5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            //            HojaProducto.Cell("AA5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //            HojaProducto.Cell("AA5").Style.Font.FontColor = XLColor.Black;
            //            #endregion encabezados

            //            foreach (DataRow lista2 in dtProductos.Rows)
            //            {


            //                #region agrego los datos 

            //                for (int ir = 1; ir <= lista2.ItemArray.Length; ir++)
            //                {
            //                    HojaProducto.Cell(i, ir).Value = lista2[ir - 1].ToString();

            //                    //if ( (Type)lista2[ir - 1].GetType() = ;

            //                    if (ir == 17 || ir == 18 || ir == 20 || ir > 22)
            //                    {

            //                        HojaProducto.Cell(i, ir - 1).Style.NumberFormat.Format = "####,##0.00";
            //                        HojaProducto.Cell(i, ir - 1).DataType = XLDataType.Number;
            //                    }




            //                }
            //                #endregion datos

            //                i++;

            //            }

            //            HojaProducto.Columns().AdjustToContents();


            //            #endregion facturas

            //            string rutaguardado = Server.MapPath("~/Reportes/") + "RepComisionDif_" + confsistcompensacion.Anio + confsistcompensacion.Mes + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";


            //            if (File.Exists(rutaguardado))
            //            {
            //                File.Delete(rutaguardado);
            //            }

            //            workbook.SaveAs(rutaguardado);

            //            workbook.SaveAs(rutaguardado);

            //            string Outgoingfile = "RepComisionDif_" + confsistcompensacion.Anio + confsistcompensacion.Mes + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";
            //            string ruta = Server.MapPath("~/Reportes/") + "RepComisionDif_" + confsistcompensacion.Anio + confsistcompensacion.Mes + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";
            //            // Prepare the response
            //            HttpResponse httpResponse = Response;
            //            httpResponse.Clear();
            //            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //            httpResponse.AddHeader("Content-Disposition", "attachment; filename=" + Outgoingfile);



            //            // Flush the workbook to the Response.OutputStream
            //            using (MemoryStream memoryStream = new MemoryStream())
            //            {
            //                workbook.SaveAs(memoryStream);
            //                memoryStream.WriteTo(httpResponse.OutputStream);
            //                memoryStream.Close();
            //            }

            //            httpResponse.End();
            //        }

            //    }
            //    catch (Exception ex)
            //    {
            //        throw ex;
            //    }
            //}
        }

        public enum DemoExportFormat
        {
            Pdf = 0,
            Xls = 1,
            Xlsx = 2,
            Rtf = 3,
            Csv = 4
        }


        protected void btnConsultarEx(object sender, EventArgs e)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (Convert.ToInt32(CmbAnio.Value.ToString()) > DateTime.Now.Year)
            {
                mensaje("El año seleccionado no puede ser mayor al año actual");
                return;
            }
            else if (Convert.ToInt32(CmbAnio.Value.ToString()) == DateTime.Now.Year && int.Parse(this.CmbMes.Value.ToString()) > DateTime.Now.Month)
            {
                mensaje("El periodo seleccionado no puede ser mayor al periodo actual");
                return;

            }
            cargarDatos();

        }
        #endregion Crear Archivo de excel

        protected void btnGenerarReportesRiks()
        {

            if (Convert.ToInt32(CmbAnio.Value.ToString()) > DateTime.Now.Year)
            {
                mensaje("El año seleccionado no puede ser mayor al año actual");
                return;
            }
            else if (Convert.ToInt32(CmbAnio.Value.ToString()) == DateTime.Now.Year && int.Parse(this.CmbMes.Value.ToString()) > DateTime.Now.Month)
            {
                mensaje("El periodo seleccionado no puede ser mayor al periodo actual");
                return;

            }
            #region generar archivo excel 
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            //String Ceros = "000";
            //String CerosA = "";

            try
            {

                //if (bcmTipoReporte.Value.ToString() == "1")
                //{
                //    fechaInicial = fechaInicial.AddMonths(-3);
                //    fechafinal = fechafinal.AddDays(-1);
                //}

                //if (bcmTipoReporte.Value.ToString() == "2")
                //{
                //    fechaInicial = DateTime.Parse(txtfechaIniciaGR.Value.ToString());
                //    fechafinal = DateTime.Parse(txtfechafinalGR.Value.ToString());
                //}

                //if (fechaInicial > fechafinal)
                //{
                //    mensaje("La fecha inicial es mayor a la fecha final de la sección de observar totales.");
                //    return;
                //}

                //traer datos de la BD 

                SistemaCompensacionGetXML confsistcompensacion = new SistemaCompensacionGetXML();

                //CN_CatCompensacion cn = new CN_CatCompensacion();

                //confsistcompensacion.Id_Emp = sesion.Id_Emp;
                //confsistcompensacion.Anio = Convert.IsDBNull(CmbAnio.Value) ? 2023 : Convert.ToInt32(CmbAnio.Value.ToString());
                //confsistcompensacion.Mes = Convert.IsDBNull(CmbMes.Value) ? 10 : Convert.ToInt32(CmbMes.Value.ToString());
                //confsistcompensacion.Id_Cd = Convert.IsDBNull(CmbSucursal.Value) ? -1 : Convert.ToInt32(CmbSucursal.Value.ToString());
                ////confsistcompensacion.Id_TipoRepresentante = Convert.IsDBNull(CmbSegmento.Value) ? -1 : Convert.ToInt32(CmbSegmento.Value.ToString());
                //confsistcompensacion.Id_Sistema = 23;
                //confsistcompensacion.Id_TipoRepresentante = Convert.ToInt32(CmbTipoRepresentante.Value.ToString());
                //confsistcompensacion.Id_TipoRepresentante = 3;

                //if (txtRik.Text == "")
                //{
                //    confsistcompensacion.Id_Rik = -1; //Convert.IsDBNull(txtRik.Text) ? -1 : Convert.ToInt32(txtRik.Text);
                //}
                //else
                //{
                //    confsistcompensacion.Id_Rik = Convert.ToInt32(txtRik.Text);
                //}

                //string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];


                //DataSet dsEstructuraSegmento = new DataSet();
                //List<ReporteComisiones> lstr = new List<ReporteComisiones>();

                //cn.ConsultaListaDiferenciado(confsistcompensacion, ref dsEstructuraSegmento, Conexion);

                //DataTable dtResumen = new DataTable();
                //DataTable dtClientes = new DataTable();
                //DataTable dtFacturas = new DataTable();
                //DataTable dtProductos = new DataTable();

                //dtResumen = dsEstructuraSegmento.Tables[0];
                //dtClientes = dsEstructuraSegmento.Tables[1];
                //dtFacturas = dsEstructuraSegmento.Tables[2];
                //dtProductos = dsEstructuraSegmento.Tables[3];

                //foreach (DataRow listariks in dtResumen.Rows)
                //{

                //    // aqui hago el ciclo  


                //    //Renglón i  comienzo en 6 esa es mi primer fila de datos

                //    string id_rik = listariks["Rik"].ToString();
                //    int id_cd = Convert.ToInt32(listariks["CDI"].ToString());

                //    if (id_rik.Trim().Length <= 3)
                //    {
                //        CerosA = Ceros.ToString().Substring(1, 3 - id_rik.Trim().Length);
                //    }


                //    string nombre = "";
                //    int i = 6;  // = renglón 

                //    using (var workbook = new XLWorkbook())
                //    {

                //        //nombre = dt.Rows[0].Field<string>("Cd_Nombre");
                //        nombre = "Resumen";
                //        var HojaExcel = workbook.Worksheets.Add(nombre);



                //        nombre = "inicial";

                //        #region encabezados
                //        HojaExcel.Cell("A1").Value = "Reporte de comisiones Diferenciadas";
                //        HojaExcel.Cell("A2").Value = "Año";
                //        HojaExcel.Cell("B2").Value = confsistcompensacion.Anio;
                //        HojaExcel.Cell("A3").Value = "Mes";
                //        HojaExcel.Cell("B3").Value = confsistcompensacion.Mes;

                //        HojaExcel.Range("A5", "AH5").Style.Font.Bold = true;
                //        HojaExcel.Range("A5", "AH5").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                //        HojaExcel.Range("A5", "AH5").Style.Font.FontColor = XLColor.White;

                //        HojaExcel.Cell("A5").Value = "Num \r\nSucursal";
                //        HojaExcel.Cell("A5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaExcel.Cell("A5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaExcel.Cell("A5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        HojaExcel.Cell("A5").Style.Font.Bold = true;
                //        HojaExcel.Cell("A5").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                //        HojaExcel.Cell("B5").Value = "Nombre\r\nSucursal";
                //        HojaExcel.Cell("B5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaExcel.Cell("B5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaExcel.Cell("B5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


                //        HojaExcel.Cell("C5").Value = "Num \r\nRik";
                //        HojaExcel.Cell("C5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaExcel.Cell("C5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaExcel.Cell("C5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                //        HojaExcel.Cell("D5").Value = "Nombre \r\n del Rik";
                //        HojaExcel.Cell("D5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaExcel.Cell("D5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaExcel.Cell("D5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                //        HojaExcel.Cell("E5").Value = "Total Venta \r\nCobrada";
                //        HojaExcel.Cell("E5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaExcel.Cell("E5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaExcel.Cell("E5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


                //        HojaExcel.Cell("F5").Value = "Utilidad \r\n Prima";
                //        HojaExcel.Cell("F5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaExcel.Cell("F5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaExcel.Cell("F5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                //        HojaExcel.Cell("G5").Value = "Utilidad \r\n Bruta";
                //        HojaExcel.Cell("G5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaExcel.Cell("G5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaExcel.Cell("G5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                //        HojaExcel.Cell("H5").Value = "Comisión Base";
                //        HojaExcel.Cell("H5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaExcel.Cell("H5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaExcel.Cell("H5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


                //        HojaExcel.Cell("I5").Value = "Multiplicador";
                //        HojaExcel.Cell("I5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaExcel.Cell("I5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaExcel.Cell("I5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                //        HojaExcel.Cell("J5").Value = "Comisión base \r\ncon multiplicador";
                //        HojaExcel.Cell("J5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaExcel.Cell("J5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaExcel.Cell("J5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        HojaExcel.Cell("J5").Style.Font.FontColor = XLColor.White;

                //        HojaExcel.Cell("K5").Value = "Gasto\r\nAdministrativo";
                //        HojaExcel.Cell("K5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaExcel.Cell("K5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaExcel.Cell("K5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        HojaExcel.Cell("K5").Style.Font.FontColor = XLColor.White;

                //        HojaExcel.Cell("L5").Value = "Comisión\r\n Neta";
                //        HojaExcel.Cell("L5").Style.Fill.BackgroundColor = XLColor.Black;
                //        HojaExcel.Cell("L5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaExcel.Cell("L5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        HojaExcel.Cell("L5").Style.Font.FontColor = XLColor.White;
                //        #endregion encabezados
                //        //table.Select($"Filename = '{filename}'");

                //        foreach (DataRow lista2 in dtResumen.Select($"Rik= '{ id_rik} ' AND CDI = '{ id_cd.ToString()} ' "))
                //        {

                //            //aqui iban los comentarios  

                //            #region agrego los datos 
                //            //Renglón i  comienzo en 6 esa es mi primer fila de datos

                //            for (int ir = 1; ir <= lista2.ItemArray.Length; ir++)
                //            {
                //                HojaExcel.Cell(i, ir).Value = lista2[ir - 1].ToString();
                //                if (ir > 4)
                //                {
                //                    HojaExcel.Cell(i, ir).Style.NumberFormat.Format = "####,##0.00";
                //                    HojaExcel.Cell(i, ir).DataType = XLDataType.Number;
                //                }

                //            }

                //            #endregion datos


                //            i++;

                //        }
                //        HojaExcel.Columns().AdjustToContents();


                //        nombre = "Clientes";
                //        var HojaCliente = workbook.Worksheets.Add(nombre);

                //        #region clientes

                //        i = 6;  // = renglón 



                //        #region encabezados clientes

                //        HojaCliente.Range("A5", "AH5").Style.Font.Bold = true;
                //        HojaCliente.Range("A5", "AH5").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                //        HojaCliente.Range("A5", "AH5").Style.Font.FontColor = XLColor.White;

                //        HojaCliente.Cell("A5").Value = "Num \r\nSucursal";
                //        HojaCliente.Cell("A5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaCliente.Cell("A5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaCliente.Cell("A5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        HojaCliente.Cell("A5").Style.Font.Bold = true;
                //        HojaCliente.Cell("A5").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                //        HojaCliente.Cell("B5").Value = "Nombre\r\nSucursal";
                //        HojaCliente.Cell("B5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaCliente.Cell("B5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaCliente.Cell("B5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                //        HojaCliente.Cell("C5").Value = "Num \r\nCliente";
                //        HojaCliente.Cell("C5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaCliente.Cell("C5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaCliente.Cell("C5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                //        HojaCliente.Cell("D5").Value = "Nombre \r\n del Cliente";
                //        HojaCliente.Cell("D5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaCliente.Cell("D5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaCliente.Cell("D5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                //        HojaCliente.Cell("E5").Value = "Tamaño";
                //        HojaCliente.Cell("E5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaCliente.Cell("E5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaCliente.Cell("E5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                //        HojaCliente.Cell("F5").Value = "Num \r\nRik";
                //        HojaCliente.Cell("F5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaCliente.Cell("F5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaCliente.Cell("F5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                //        //HojaCliente.Cell("D5").Value = "Nombre /\r\n del Rik";
                //        //HojaCliente.Cell("D5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        //HojaCliente.Cell("D5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        //HojaCliente.Cell("D5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                //        HojaCliente.Cell("G5").Value = "Total Venta \r\nCobrada";
                //        HojaCliente.Cell("G5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaCliente.Cell("G5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaCliente.Cell("G5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


                //        HojaCliente.Cell("H5").Value = "Utilidad \r\n Prima";
                //        HojaCliente.Cell("H5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaCliente.Cell("H5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaCliente.Cell("H5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


                //        HojaCliente.Cell("I5").Value = "Amortización";
                //        HojaCliente.Cell("I5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaCliente.Cell("I5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaCliente.Cell("I5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                //        HojaCliente.Cell("J5").Value = "Gasto\r\nAdministrativo";
                //        HojaCliente.Cell("J5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaCliente.Cell("J5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaCliente.Cell("J5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        HojaCliente.Cell("J5").Style.Font.FontColor = XLColor.White;

                //        HojaCliente.Cell("K5").Value = "Utilidad \r\n Bruta";
                //        HojaCliente.Cell("K5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaCliente.Cell("K5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaCliente.Cell("K5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        HojaCliente.Cell("K5").Style.Font.FontColor = XLColor.White;

                //        HojaCliente.Cell("L5").Value = "Ajuste por\r\n Cobranza";
                //        HojaCliente.Cell("L5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaCliente.Cell("L5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaCliente.Cell("L5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        HojaCliente.Cell("L5").Style.Font.FontColor = XLColor.White;

                //        HojaCliente.Cell("M5").Value = "UB\r\nAjustada";
                //        HojaCliente.Cell("M5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaCliente.Cell("M5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaCliente.Cell("M5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        HojaCliente.Cell("M5").Style.Font.FontColor = XLColor.White;

                //        HojaCliente.Cell("N5").Value = "Comisión \r\n UP";
                //        HojaCliente.Cell("N5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaCliente.Cell("N5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaCliente.Cell("N5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        HojaCliente.Cell("N5").Style.Font.FontColor = XLColor.White;

                //        HojaCliente.Cell("O5").Value = "Comisión \r\n UB "; // + confsistcompensacion.Anio.ToString();
                //        HojaCliente.Cell("O5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaCliente.Cell("O5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaCliente.Cell("O5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        HojaCliente.Cell("O5").Style.Font.FontColor = XLColor.White;

                //        HojaCliente.Cell("P5").Value = "Utilidad \r\n Remanente";
                //        HojaCliente.Cell("P5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaCliente.Cell("P5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaCliente.Cell("P5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        HojaCliente.Cell("P5").Style.Font.FontColor = XLColor.White;

                //        HojaCliente.Cell("Q5").Value = "Comisión\r\n Base"; // + confsistcompensacion.Anio.ToString();
                //        HojaCliente.Cell("Q5").Style.Fill.BackgroundColor = XLColor.Black;
                //        HojaCliente.Cell("Q5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaCliente.Cell("Q5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        HojaCliente.Cell("Q5").Style.Font.FontColor = XLColor.White;

                //        HojaCliente.Cell("R5").Value = "Año";
                //        HojaCliente.Cell("R5").Style.Fill.BackgroundColor = XLColor.MediumOrchid;
                //        HojaCliente.Cell("R5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaCliente.Cell("R5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        HojaCliente.Cell("R5").Style.Font.FontColor = XLColor.White;

                //        HojaCliente.Cell("S5").Value = "Mes";
                //        HojaCliente.Cell("S5").Style.Fill.BackgroundColor = XLColor.MediumOrchid;
                //        HojaCliente.Cell("S5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaCliente.Cell("S5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        HojaCliente.Cell("S5").Style.Font.FontColor = XLColor.White;

                //        #endregion encabezados

                //        //foreach (DataRow lista2 in dtClientes.Select("Id_Rik==" + id_rik))
                //        foreach (DataRow lista2 in dtClientes.Select($"Id_Rik= '{ id_rik} ' AND Id_Cd = '{ id_cd.ToString()} ' "))

                //        {


                //            #region agrego los datos 
                //            for (int ir = 1; ir <= lista2.ItemArray.Length; ir++)
                //            {
                //                HojaCliente.Cell(i, ir).Value = lista2[ir - 1].ToString();

                //                if (ir > 7 && ir < 17)
                //                {
                //                    if (ir != (int)13)
                //                    {
                //                        HojaCliente.Cell(i, ir - 1).Style.NumberFormat.Format = "####,##0.00";
                //                        HojaCliente.Cell(i, ir - 1).DataType = XLDataType.Number;

                //                    }
                //                }
                //                if (ir == 18)
                //                {
                //                    HojaCliente.Cell(i, ir - 1).Style.NumberFormat.Format = "####,##0.00";
                //                    HojaCliente.Cell(i, ir - 1).DataType = XLDataType.Number;
                //                }



                //            }
                //            #endregion datos

                //            i++;

                //        }

                //        #endregion clientes
                //        HojaCliente.Columns().AdjustToContents();


                //        #region facturas 


                //        nombre = "Facturas";
                //        var HojaFactura = workbook.Worksheets.Add(nombre);

                //        i = 6;  // = renglón 



                //        #region encabezados facturas

                //        HojaFactura.Range("A5", "AH5").Style.Font.Bold = true;
                //        HojaFactura.Range("A5", "AH5").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                //        HojaFactura.Range("A5", "AH5").Style.Font.FontColor = XLColor.White;

                //        HojaFactura.Cell("A5").Value = "Num \r\nSucursal";
                //        HojaFactura.Cell("A5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaFactura.Cell("A5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaFactura.Cell("A5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        HojaFactura.Cell("A5").Style.Font.Bold = true;

                //        HojaFactura.Cell("B5").Value = "Nombre\r\nSucursal";
                //        HojaFactura.Cell("B5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaFactura.Cell("B5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaFactura.Cell("B5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                //        HojaFactura.Cell("C5").Value = "Referencia de Pago";
                //        HojaFactura.Cell("C5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaFactura.Cell("C5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaFactura.Cell("C5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        HojaFactura.Cell("C5").Style.Font.Bold = true;

                //        HojaFactura.Cell("D5").Value = "Num \r\nCliente";
                //        HojaFactura.Cell("D5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaFactura.Cell("D5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaFactura.Cell("D5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                //        HojaFactura.Cell("E5").Value = "Nombre \r\n del Cliente";
                //        HojaFactura.Cell("E5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaFactura.Cell("E5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaFactura.Cell("E5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                //        HojaFactura.Cell("F5").Value = "Tamaño";
                //        HojaFactura.Cell("F5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaFactura.Cell("F5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaFactura.Cell("F5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                //        //HojaFactura.Cell("D5").Value = "Nombre /\r\n del Rik";
                //        //HojaFactura.Cell("D5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        //HojaFactura.Cell("D5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        //HojaFactura.Cell("D5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        HojaFactura.Cell("G5").Value = "Territorio";
                //        HojaFactura.Cell("G5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaFactura.Cell("G5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaFactura.Cell("G5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                //        HojaFactura.Cell("H5").Value = "Num \r\nRik";
                //        HojaFactura.Cell("H5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaFactura.Cell("H5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaFactura.Cell("H5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                //        HojaFactura.Cell("I5").Value = "Fecha \r\n Factura";
                //        HojaFactura.Cell("I5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaFactura.Cell("I5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaFactura.Cell("I5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                //        HojaFactura.Cell("J5").Value = "Fecha \r\n Vencimiento";
                //        HojaFactura.Cell("J5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaFactura.Cell("J5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaFactura.Cell("J5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                //        HojaFactura.Cell("K5").Value = "Fecha de\r\n Pago";
                //        HojaFactura.Cell("K5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaFactura.Cell("K5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaFactura.Cell("K5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        HojaFactura.Cell("K5").Style.Font.FontColor = XLColor.White;


                //        HojaFactura.Cell("L5").Value = "Días";
                //        HojaFactura.Cell("L5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaFactura.Cell("L5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaFactura.Cell("L5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        HojaFactura.Cell("L5").Style.Font.FontColor = XLColor.White;


                //        HojaFactura.Cell("M5").Value = "Venta \r\nCobrada";
                //        HojaFactura.Cell("M5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaFactura.Cell("M5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaFactura.Cell("M5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        HojaFactura.Cell("M5").Style.Font.FontColor = XLColor.White;

                //        HojaFactura.Cell("N5").Value = "Utilidad \r\n Prima";
                //        HojaFactura.Cell("N5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaFactura.Cell("N5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaFactura.Cell("N5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        HojaFactura.Cell("N5").Style.Font.FontColor = XLColor.White;

                //        HojaFactura.Cell("O5").Value = "Multiplo Aj \r\n Cobranza ";
                //        HojaFactura.Cell("O5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaFactura.Cell("O5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaFactura.Cell("O5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        HojaFactura.Cell("O5").Style.Font.FontColor = XLColor.White;

                //        HojaFactura.Cell("P5").Value = "Ajuste \r\n Cobranza  ";
                //        HojaFactura.Cell("P5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaFactura.Cell("P5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaFactura.Cell("P5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        HojaFactura.Cell("P5").Style.Font.FontColor = XLColor.White;

                //        HojaFactura.Cell("Q5").Value = "Comisión\r\n UP Ajustada";
                //        HojaFactura.Cell("Q5").Style.Fill.BackgroundColor = XLColor.Black;
                //        HojaFactura.Cell("Q5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaFactura.Cell("Q5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        HojaFactura.Cell("Q5").Style.Font.FontColor = XLColor.White;

                //        HojaFactura.Cell("R5").Value = "Año";
                //        HojaFactura.Cell("R5").Style.Fill.BackgroundColor = XLColor.MediumOrchid;
                //        HojaFactura.Cell("R5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaFactura.Cell("R5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        HojaFactura.Cell("R5").Style.Font.FontColor = XLColor.White;

                //        HojaFactura.Cell("S5").Value = "Mes";
                //        HojaFactura.Cell("S5").Style.Fill.BackgroundColor = XLColor.MediumOrchid;
                //        HojaFactura.Cell("S5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaFactura.Cell("S5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        HojaFactura.Cell("S5").Style.Font.FontColor = XLColor.White;

                //        #endregion encabezados
                //        foreach (DataRow lista2 in dtFacturas.Select($"Id_Rik= '{ id_rik} ' AND Id_Cd = '{ id_cd.ToString()} ' "))
                //        {


                //            #region agrego los datos 

                //            for (int ir = 1; ir <= lista2.ItemArray.Length; ir++)
                //            {
                //                HojaFactura.Cell(i, ir).Value = lista2[ir - 1].ToString();

                //                //if ( (Type)lista2[ir - 1].GetType() = ;

                //                if (ir == 14 || ir == 15 || ir == 17 || ir == 18)
                //                {

                //                    HojaFactura.Cell(i, ir - 1).Style.NumberFormat.Format = "####,##0.00";
                //                    HojaFactura.Cell(i, ir - 1).DataType = XLDataType.Number;
                //                }




                //            }
                //            #endregion datos

                //            i++;

                //        }

                //        HojaFactura.Columns().AdjustToContents();


                //        #endregion facturas

                //        #region Productos 
                //        nombre = "Productos";
                //        var HojaProducto = workbook.Worksheets.Add(nombre);

                //        i = 6;  // = renglón 

                //        #region encabezados productos

                //        HojaProducto.Range("A5", "AH5").Style.Font.Bold = true;
                //        HojaProducto.Range("A5", "AH5").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                //        HojaProducto.Range("A5", "AH5").Style.Font.FontColor = XLColor.White;

                //        HojaProducto.Cell("A5").Value = "Num \r\nSucursal";
                //        HojaProducto.Cell("A5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaProducto.Cell("A5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaProducto.Cell("A5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        HojaProducto.Cell("A5").Style.Font.Bold = true;

                //        HojaProducto.Cell("B5").Value = "Nombre\r\nSucursal";
                //        HojaProducto.Cell("B5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaProducto.Cell("B5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaProducto.Cell("B5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                //        HojaProducto.Cell("C5").Value = "Referencia de Pago";
                //        HojaProducto.Cell("C5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaProducto.Cell("C5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaProducto.Cell("C5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        HojaProducto.Cell("C5").Style.Font.Bold = true;

                //        HojaProducto.Cell("D5").Value = "Num \r\nCliente";
                //        HojaProducto.Cell("D5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaProducto.Cell("D5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaProducto.Cell("D5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                //        HojaProducto.Cell("E5").Value = "Nombre \r\n del Cliente";
                //        HojaProducto.Cell("E5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaProducto.Cell("E5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaProducto.Cell("E5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                //        HojaProducto.Cell("F5").Value = "Tamaño";
                //        HojaProducto.Cell("F5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaProducto.Cell("F5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaProducto.Cell("F5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                //        HojaProducto.Cell("G5").Value = "Territorio";
                //        HojaProducto.Cell("G5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaProducto.Cell("G5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaProducto.Cell("G5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                //        HojaProducto.Cell("H5").Value = "Num \r\nRik";
                //        HojaProducto.Cell("H5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaProducto.Cell("H5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaProducto.Cell("H5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                //        HojaProducto.Cell("I5").Value = "Cantidad \r\n Facturada";
                //        HojaProducto.Cell("I5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaProducto.Cell("I5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaProducto.Cell("I5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                //        HojaProducto.Cell("J5").Value = "Producto";
                //        HojaProducto.Cell("J5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaProducto.Cell("J5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaProducto.Cell("J5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                //        HojaProducto.Cell("K5").Value = "Descripción";
                //        HojaProducto.Cell("K5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaProducto.Cell("K5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaProducto.Cell("K5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        HojaProducto.Cell("K5").Style.Font.FontColor = XLColor.White;


                //        HojaProducto.Cell("L5").Value = "Convenio \r\nProveedor";
                //        HojaProducto.Cell("L5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaProducto.Cell("L5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaProducto.Cell("L5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        HojaProducto.Cell("L5").Style.Font.FontColor = XLColor.White;


                //        HojaProducto.Cell("M5").Value = "Convenio \r\nKey";
                //        HojaProducto.Cell("M5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaProducto.Cell("M5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaProducto.Cell("M5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        HojaProducto.Cell("M5").Style.Font.FontColor = XLColor.White;

                //        HojaProducto.Cell("N5").Value = "Clasificación";
                //        HojaProducto.Cell("N5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaProducto.Cell("N5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaProducto.Cell("N5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        HojaProducto.Cell("N5").Style.Font.FontColor = XLColor.White;

                //        HojaProducto.Cell("O5").Value = "Nivel de \r\n Precio ";
                //        HojaProducto.Cell("O5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaProducto.Cell("O5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaProducto.Cell("O5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        HojaProducto.Cell("O5").Style.Font.FontColor = XLColor.White;

                //        HojaProducto.Cell("P5").Value = "Precio de \r\n Venta  ";
                //        HojaProducto.Cell("P5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaProducto.Cell("P5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaProducto.Cell("P5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        HojaProducto.Cell("P5").Style.Font.FontColor = XLColor.White;

                //        HojaProducto.Cell("Q5").Value = "Utilidad\r\n Prima";
                //        HojaProducto.Cell("Q5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaProducto.Cell("Q5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaProducto.Cell("Q5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        HojaProducto.Cell("Q5").Style.Font.FontColor = XLColor.White;

                //        HojaProducto.Cell("R5").Value = "Porc.\r\n Comisión";
                //        HojaProducto.Cell("R5").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                //        HojaProducto.Cell("R5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaProducto.Cell("R5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        HojaProducto.Cell("R5").Style.Font.FontColor = XLColor.White;

                //        HojaProducto.Cell("S5").Value = "Comisión\r\n UP";
                //        HojaProducto.Cell("S5").Style.Fill.BackgroundColor = XLColor.Black;
                //        HojaProducto.Cell("S5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaProducto.Cell("S5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        HojaProducto.Cell("S5").Style.Font.FontColor = XLColor.White;

                //        HojaProducto.Cell("T5").Value = "Año";
                //        HojaProducto.Cell("T5").Style.Fill.BackgroundColor = XLColor.MediumOrchid;
                //        HojaProducto.Cell("T5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaProducto.Cell("T5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        HojaProducto.Cell("T5").Style.Font.FontColor = XLColor.White;

                //        HojaProducto.Cell("U5").Value = "Mes";
                //        HojaProducto.Cell("U5").Style.Fill.BackgroundColor = XLColor.MediumOrchid;
                //        HojaProducto.Cell("U5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        HojaProducto.Cell("U5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        HojaProducto.Cell("U5").Style.Font.FontColor = XLColor.White;

                //        //////HojaProducto.Cell("V5").Value = "PrecioObjetivo";
                //        //////HojaProducto.Cell("V5").Style.Fill.BackgroundColor = XLColor.AshGrey;
                //        //////HojaProducto.Cell("V5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        //////HojaProducto.Cell("V5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        //////HojaProducto.Cell("V5").Style.Font.FontColor = XLColor.Black;

                //        //////HojaProducto.Cell("W5").Value = "Precio\r\n MinimoRik";
                //        //////HojaProducto.Cell("W5").Style.Fill.BackgroundColor = XLColor.AshGrey;
                //        //////HojaProducto.Cell("W5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        //////HojaProducto.Cell("W5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        //////HojaProducto.Cell("W5").Style.Font.FontColor = XLColor.Black;

                //        //////HojaProducto.Cell("X5").Value = "Precio\r\n MinimoGte";
                //        //////HojaProducto.Cell("X5").Style.Fill.BackgroundColor = XLColor.AshGrey;
                //        //////HojaProducto.Cell("X5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        //////HojaProducto.Cell("X5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        //////HojaProducto.Cell("X5").Style.Font.FontColor = XLColor.Black;

                //        //////HojaProducto.Cell("Y5").Value = "Precio\r\n Lista";
                //        //////HojaProducto.Cell("Y5").Style.Fill.BackgroundColor = XLColor.AshGrey;
                //        //////HojaProducto.Cell("Y5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        //////HojaProducto.Cell("Y5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        //////HojaProducto.Cell("Y5").Style.Font.FontColor = XLColor.Black;

                //        //////HojaProducto.Cell("Z5").Value = "Precio\r\n Minimo";
                //        //////HojaProducto.Cell("Z5").Style.Fill.BackgroundColor = XLColor.AshGrey;
                //        //////HojaProducto.Cell("Z5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        //////HojaProducto.Cell("Z5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        //////HojaProducto.Cell("Z5").Style.Font.FontColor = XLColor.Black;

                //        //////HojaProducto.Cell("AA5").Value = "Precio\r\n AAA";
                //        //////HojaProducto.Cell("AA5").Style.Fill.BackgroundColor = XLColor.AshGrey;
                //        //////HojaProducto.Cell("AA5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //        //////HojaProducto.Cell("AA5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //        //////HojaProducto.Cell("AA5").Style.Font.FontColor = XLColor.Black;
                //        #endregion encabezados

                //        foreach (DataRow lista2 in dtProductos.Select($"Id_Rik= '{ id_rik} ' AND Id_Cd = '{ id_cd.ToString()} ' "))
                //        {


                //            #region agrego los datos 

                //            for (int ir = 1; ir <= lista2.ItemArray.Length - 6; ir++)
                //            {
                //                HojaProducto.Cell(i, ir).Value = lista2[ir - 1].ToString();

                //                //if ( (Type)lista2[ir - 1].GetType() = ;

                //                if (ir == 17 || ir == 18 || ir == 20 || ir > 22)
                //                {

                //                    HojaProducto.Cell(i, ir - 1).Style.NumberFormat.Format = "####,##0.00";
                //                    HojaProducto.Cell(i, ir - 1).DataType = XLDataType.Number;
                //                }




                //            }
                //            #endregion datos

                //            i++;

                //        }

                //        HojaProducto.Columns().AdjustToContents();


                //        #endregion facturas
                //        string mess = confsistcompensacion.Mes.ToString();
                //        mess = mess.PadLeft(2, '0');

                //        string rutaguardado = Server.MapPath("~/Reportes/") + "RepComisionDif_" + confsistcompensacion.Anio + confsistcompensacion.Mes + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";

                //        rutaguardado = Server.MapPath("~/Reportes/") + id_cd.ToString() + CerosA + id_rik + "_" + mess + confsistcompensacion.Anio + "D.xlsx";


                //        if (File.Exists(rutaguardado))
                //        {
                //            File.Delete(rutaguardado);
                //        }

                //        workbook.SaveAs(rutaguardado);

                //        //  workbook.SaveAs(rutaguardado);

                //        // comentar esto para que funcione bien 
                //        ////jfcv 17 nov agregar sucursal jfcv
                //        // string Outgoingfile = id_cd.ToString() + id_rik + "_" + mess + confsistcompensacion.Anio + "D.xlsx";
                //        // string ruta = Server.MapPath("~/Reportes/") + id_cd.ToString() + id_rik + "_" + mess + confsistcompensacion.Anio + "D.xlsx";
                //        // // Prepare the response
                //        // HttpResponse httpResponse = Response;
                //        // httpResponse.Clear();
                //        // httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //        // httpResponse.AddHeader("Content-Disposition", "attachment; filename=" + Outgoingfile);



                //        // // Flush the workbook to the Response.OutputStream
                //        // using (MemoryStream memoryStream = new MemoryStream())
                //        // {
                //        //     workbook.SaveAs(memoryStream);
                //        //     memoryStream.WriteTo(httpResponse.OutputStream);
                //        //     memoryStream.Close();
                //        // }

                //        // httpResponse.End();
                //    }

                //} //fin del foreach de riks 
            }
            catch (Exception ex)
            {
                throw ex;
            }


            #endregion

        }


        private void CargarRepresentantes()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                CN__Comun cn_comun = new CN__Comun();
                //cn_comun.LlenaCombo(2, sesion.Id_Cd_Ver, sesion.Emp_Cnx, "spCatRik_ComboTodos", ref this.cmbRik);
                cn_comun.DevLlenaCombo(2, sesion.Id_Cd_Ver, sesion.Emp_Cnx, "spCatRik_ComboTodos", ref this.cmbRik);


                if (sesion.Id_TU == 2)
                {
                    this.txtRik.Text = sesion.Id_Rik.ToString();
                    this.cmbRik.Value = sesion.Id_Rik.ToString();
                    this.txtRik.Enabled = false;
                    this.cmbRik.Enabled = false;

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        private void CargaCdi()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN__Comun cn_comun = new CN__Comun();
                //cn_comun.DevLlenaCombo(0, sesion.Emp_Cnx, "spCatCDI_ComboINV", ref CmbSucursal);
            }
            catch (Exception ex)
            {
                mensaje(ex.Message);
            }
        }


        public void cargarDatos()
        {

            List<ReporteGAP> listreporteGAP = new List<ReporteGAP>();
            ReporteGAP reporteGAP = new ReporteGAP();
            string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_ReporteGAP cn_leads = new CN_ReporteGAP();

            reporteGAP.Id_Cd = sesion.Id_Cd_Ver;//Convert.ToInt32(CmbSucursal.Value.ToString());
            reporteGAP.IdReporteGAP = -1;

            reporteGAP.Activo = 1;

            reporteGAP.Id_Emp = sesion.Id_Emp;
            if (txtCliente.Text == "")
            {
                reporteGAP.Id_Cte = -1;
            }
            else
            {
                reporteGAP.Id_Cte = Convert.ToInt32(this.txtCliente.Text);
            }



            if (this.txtRik.Text == "")
            {
                reporteGAP.Id_Rik = -1;
            }
            else
            {
                reporteGAP.Id_Rik = Convert.ToInt32(this.txtRik.Text);
            }

            reporteGAP.Id_Rik = sesion.Id_TU == 2 ? Convert.ToInt32(sesion.Id_Rik.ToString()) : Convert.ToInt32(this.cmbRik.SelectedItem.Value);


            if (this.txtProducto.Text == "")
            {
                reporteGAP.Id_Prd = -1;
            }
            else
            {
                reporteGAP.Id_Prd = Convert.ToInt64(this.txtProducto.Text);
            }
            //if (this.txt.Text == "")
            //{
            reporteGAP.Id_Tamaño = "T";
            reporteGAP.Id_Tamaño = this.CmbTamaño.SelectedItem.Value.ToString();

            //}
            //else
            //{
            //    reporteGAP.Id_Prd = Convert.ToInt64(this.txtProducto.Text);
            //}

            reporteGAP.Id_Cd = sesion.Id_Cd_Ver; // Convert.ToInt32(this.CmbSucursal.SelectedItem.Value);


            //lead.FechaInicial = DateTime.Parse(txtfechaInicialGR.Value.ToString());
            //lead.FechaFinal = DateTime.Parse(txtfechafinalGR.Value.ToString());


            //conv.Filtro_Vencido = int.Parse(this.CmbVencido.SelectedValue);
            //conv.Filtro_Id_Cat = int.Parse(this.CmbCategoria.SelectedValue);
            //conv.Filtro_Valor = TxtValorCategoria.Text.Trim() == "" ? "-1" : TxtValorCategoria.Text.Trim();
            //conv.Filtro_Id_Cd = sesion.Id_Cd_Ver;
            reporteGAP.Mes = Convert.ToInt32(this.CmbMes.SelectedItem.Value);
            reporteGAP.Año = Convert.ToInt32(this.CmbAnio.SelectedItem.Value);

            CN_ReporteGAP.ConsultaListaReporteGAPRik(reporteGAP, ref listreporteGAP, Conexion);

            this.grdServicio.DataSource = listreporteGAP;
            this.grdServicio.DataBind();
            DataTable dataTable = ConvertToDataTable(listreporteGAP);
            dt = dataTable;
            Session["dtReporteConsultaGAPRik"] = dt;



        }
        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }
                table.Rows.Add(row);
            }

            return table;
        }


        #region eventos grid 

        protected void grid_CellEditorInitialize(object sender, BootstrapGridViewEditorEventArgs e)
        {
            BootstrapGridView grdServicio = (BootstrapGridView)sender;

        }


        protected void rg1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            DataTable dtTemp = (DataTable)Session["dtReporteConsultaGAPRik"];

            ASPxGridView gridView = (ASPxGridView)sender;

            int i = grdServicio.FindVisibleIndexByKeyValue(e.Keys["IdReporteGAP"]);

            e.Cancel = true;
            DataRow row = null;
            row = dtTemp.Rows[i];


            IDictionaryEnumerator enumerator = e.NewValues.GetEnumerator();
            enumerator.Reset();
            while (enumerator.MoveNext())
            {
                row[enumerator.Key.ToString()] = enumerator.Value;
            }

            ////CN_PreciadorSegmentoTamaño segmentostamaño = new CN_PreciadorSegmentoTamaño();
            ////PreciadorSegmentoCliente segmentoCliente = new PreciadorSegmentoCliente();
            ////segmentoCliente.Id_PreciadorCte = Convert.ToInt32(e.Keys["IdReporteGAP"].ToString());
            ////segmentoCliente.Id_Tamaño = e.NewValues["Id_Tamaño"].ToString();
            ////segmentoCliente.Comentario = e.NewValues["Comentario"].ToString();
            ////string verificador = "";
            ////segmentostamaño.ActualizarSegmentoCte(sesion.Id_Emp, sesion.Id_U, segmentoCliente, sesion.Emp_Cnx, ref verificador);

            ////// SaveUserLayoutToDatabase("userID", "GridLayout", ASPxGridView1.SaveClientLayout());

            ////gridView.CancelEdit();
            ////e.Cancel = true;



            Session["dtReporteConsultaGAPRik"] = dtTemp;


            grdServicio.DataSource = dtTemp;
            grdServicio.DataBind();

        }

        protected void btnRechazar_Click(object sender, EventArgs e)
        {

            GridViewDataItemTemplateContainer c = ((System.Web.UI.HtmlControls.HtmlButton)sender).NamingContainer as GridViewDataItemTemplateContainer;
            string Id_PreciadorSegmento = c.Grid.GetRowValues(c.VisibleIndex, "IdReporteGAP").ToString().Trim();
            //string Num_pedido = c.Grid.GetRowValues(c.VisibleIndex, "pedido").ToString().Trim();
            //string Semana = c.Grid.GetRowValues(c.VisibleIndex, "Acs_Semana").ToString().Trim();
            //string Anio = c.Grid.GetRowValues(c.VisibleIndex, "Acs_Anio").ToString().Trim();

            //RechazarPedidoVI(int.Parse(ID_Acs), int.Parse(Semana), int.Parse(Anio), Num_pedido);

            //TabName.Value = Request.Form[TabName.UniqueID];
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "mantaintabs('" + TabName.Value + "')", true);
        }

        protected void rg1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            DataTable dtTemp = (DataTable)Session["dtReporteConsultaGAPRik"];



            ASPxGridView gridView = (ASPxGridView)sender;
            int i = grdServicio.FindVisibleIndexByKeyValue(e.Keys["IdReporteGAP"]);
            e.Cancel = true;



            ((BootstrapGridView)sender).JSProperties["cpIsUpdated"] = 1;


            dtTemp.Rows[i].Delete();


            Session["dtReporteConsultaGAPRik"] = dtTemp;

            grdServicio.DataSource = dtTemp;
            grdServicio.DataBind();


        }
        protected void rg1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {

            DataTable dtTemp = (DataTable)Session["NuevoPrdocutoAAcys"];

            ASPxGridView gridView = (ASPxGridView)sender;

            DataRow row = dtTemp.NewRow();
            IDictionaryEnumerator enumerator = e.NewValues.GetEnumerator();
            enumerator.Reset();
            while (enumerator.MoveNext())
            {
                if (enumerator.Key.ToString() != "Count")
                {
                    row[enumerator.Key.ToString()] = enumerator.Value == null ? DBNull.Value : enumerator.Value;
                }
            }

            gridView.CancelEdit();
            e.Cancel = true;
            dtTemp.Rows.Add(row);
            Session["dtReporteConsultaGAPRik"] = dtTemp;


            grdServicio.DataSource = dtTemp;
            grdServicio.DataBind();
        }



        protected void ddlEditMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewEditingMode mode = (GridViewEditingMode)Enum.Parse(typeof(GridViewEditingMode), ddlEditMode.Text);
            grdServicio.SettingsEditing.Mode = mode;
            var commandColumn = grdServicio.Columns[0] as GridViewCommandColumn;
            commandColumn.Visible = !object.Equals(mode, GridViewEditingMode.Batch);
        }
        protected void grid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            int index = -1;
            if (int.TryParse(e.Parameters, out index))
                grdServicio.SettingsEditing.Mode = (GridViewEditingMode)index;
        }
 

        #endregion

        #region Mensajes

        /// <summary>
        /// Abre el modal de mensaje
        /// </summary>
        /// <param name="mensaje"></param>
        private void mensaje(string mensaje)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "modalMensaje('" + mensaje + "')", true);
            ////ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "modalMensajeAlerta('" + mensaje + "')", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "close", "CloseWindowmensaje('" + mensaje + "');", true);

        }


        #endregion


    }
}






