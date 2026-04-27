using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaDatos;
using Telerik.Web.UI;
using System.Data;
using CapaNegocios;
using System.Text;
using SIANWEB.Core.UI;
using CapaModelo;
using System.Configuration;
using System.Runtime.Remoting;
using Excel = Microsoft.Office.Interop.Excel;


namespace SIANWEB.CL
{
    public partial class CL_Autorizaciones : System.Web.UI.Page
    {
        public int Parametro_Id_Cd;
        public int Parametro_SolFolio;
        public int Id_PrdLocal;
        public int Id_VencidoLocal;
        public int Id_ProvLocal;

        

        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        //DataTable dt { get { return (DataTable)Session["dt" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["dt" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        static int mes_siguiente = 1;
        //ultima fecha es la fecha final del ultimo periodo encontrado en la base de datos
        static DateTime ultima_fecha;

        protected string sian;

        public Sesion session
        {
            get
            {
                return (Sesion)Session["Sesion" + Session.SessionID];
            }
            set
            {
                Session["session" + Session.SessionID] = value;
            }
        }

        // Tipo de Usuario 
        public int Usuario_Tipo;
        public int iCDI;
        public string sCDI_Nombre;
        public static string SucursalCadena;
        protected void Page_Load(object sender, EventArgs e)
        {
            Parametro_Id_Cd = 0;
            Parametro_SolFolio = 0;
            // Parametros en URL      
            try
            {
                if (ValidarSesion())
                {
                    if (!IsPostBack)
                    {
                        ValidarPermisos();
                        Usuario_Tipo = session.Id_TU;
                        sCDI_Nombre = session.Cd_Nombre;
                        iCDI = session.Id_Cd;
                        sian = ConfigurationManager.AppSettings["AccesoPortal"].ToString();

                        //this.txtCentrosDist.Value = sCDI_Nombre;
                        //cargarProveedorLocal();
                        if (iCDI > 0 && Parametro_SolFolio > 0)
                        {
                            Ejecutar_Function_JavaScript("IniciaParametros(" + Parametro_Id_Cd.ToString() + "," + Parametro_SolFolio.ToString() + ");");
                            return;
                        }
                    }
                    else
                    {
                        int x;
                        x = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void btnGraficas_Click(object sender, EventArgs e)
        {
            this.DisplayInExcel();
        }

        public class Account
        {
            public int ID { get; set; }
            public double Balance { get; set; }
        }

        public void DisplayInExcel()
        {
            int chbOpcion_1;
            int chbOpcion_2;
            int chbOpcion_3;
            int chbOpcion_4;
            int chbOpcion_5;
            int chbOpcion_6;
            int chbOpcion_7;
            int chbOpcion_8;
            int chbOpcion_9;
            int chbOpcion_10;
            int chbOpcion_11;
            int chbOpcion_12;
            int chbOpcion_13;
            string FechaInicial;
            string FechaFinal;
            string SFechaInicial;
            string SFechaFinal;

            try
            {
                #region Opciones             
                if (Request.Form["chbOpcion_1"] != null && Request.Form["chbOpcion_1"] == "on")
                {
                    chbOpcion_1 = 1;
                }
                else
                {
                    chbOpcion_1 = 0;
                }
                if (Request.Form["chbOpcion_2"] != null && Request.Form["chbOpcion_2"] == "on")
                {
                    chbOpcion_2 = 1;
                }
                else
                {
                    chbOpcion_2 = 0;
                }
                if (Request.Form["chbOpcion_3"] != null && Request.Form["chbOpcion_3"] == "on")
                {
                    chbOpcion_3 = 1;
                }
                else
                {
                    chbOpcion_3 = 0;
                }
                if (Request.Form["chbOpcion_4"] != null && Request.Form["chbOpcion_4"] == "on")
                {
                    chbOpcion_4 = 1;
                }
                else
                {
                    chbOpcion_4 = 0;
                }
                if (Request.Form["chbOpcion_5"] != null && Request.Form["chbOpcion_5"] == "on")
                {
                    chbOpcion_5 = 1;
                }
                else
                {
                    chbOpcion_5 = 0;
                }
                if (Request.Form["chbOpcion_6"] != null && Request.Form["chbOpcion_6"] == "on")
                {
                    chbOpcion_6 = 1;
                }
                else
                {
                    chbOpcion_6 = 0;
                }
                if (Request.Form["chbOpcion_7"] != null && Request.Form["chbOpcion_7"] == "on")
                {
                    chbOpcion_7 = 1;
                }
                else
                {
                    chbOpcion_7 = 0;
                }
                if (Request.Form["chbOpcion_8"] != null && Request.Form["chbOpcion_8"] == "on")
                {
                    chbOpcion_8 = 1;
                }
                else
                {
                    chbOpcion_8 = 0;
                }
                if (Request.Form["chbOpcion_9"] != null && Request.Form["chbOpcion_9"] == "on")
                {
                    chbOpcion_9 = 1;
                }
                else
                {
                    chbOpcion_9 = 0;
                }
                if (Request.Form["chbOpcion_10"] != null && Request.Form["chbOpcion_10"] == "on")
                {
                    chbOpcion_10 = 1;
                }
                else
                {
                    chbOpcion_10 = 0;
                }
                if (Request.Form["chbOpcion_11"] != null && Request.Form["chbOpcion_11"] == "on")
                {
                    chbOpcion_11 = 1;
                }
                else
                {
                    chbOpcion_11 = 0;
                }
                if (Request.Form["chbOpcion_12"] != null && Request.Form["chbOpcion_12"] == "on")
                {
                    chbOpcion_12 = 1;
                }
                else
                {
                    chbOpcion_12 = 0;
                }
                if (Request.Form["chbOpcion_13"] != null && Request.Form["chbOpcion_13"] == "on")
                {
                    chbOpcion_13 = 1;
                }
                else
                {
                    chbOpcion_13 = 0;
                }

                #endregion
            }
            catch (Exception e)
            {
                Alerta("Error 1.");
                return;
            }

            string Sucursales;

            try
            {
                FechaInicial = Request.Form["tbFechaInicial"];
                SFechaInicial = FechaInicial;
                FechaInicial = FechaInicial.Substring(6, 4) + "-" + FechaInicial.Substring(3, 2) + "-" + FechaInicial.Substring(0, 2);

                FechaFinal = Page.Request.Form["tbFechaFinal"].ToString();
                SFechaFinal = FechaFinal;
                FechaFinal = FechaFinal.Substring(6, 4) + "-" + FechaFinal.Substring(3, 2) + "-" + FechaFinal.Substring(0, 2);

                int FILTRO_NoSolicitud;
                Int64 FILTRO_CodigoPadreProducto;
                Int64 FILTRO_CodigoLocal;
                int FILTRO_EstadoAutorizacion;
                int FILTRO_EstadoVigencia;
                int FILTRO_EstadoVencimiento;
                string FILTRO_Sucursal;
                int FILTRO_TipoProducto;
                int FILTRO_FamiliaProductos;
                int FILTRO_MotivoCompra;

                FILTRO_NoSolicitud = 0;
                FILTRO_CodigoPadreProducto = 0;
                FILTRO_CodigoLocal = 0;
                FILTRO_EstadoAutorizacion = 0;
                FILTRO_EstadoVigencia = 0;
                FILTRO_EstadoVencimiento = 0;
                FILTRO_Sucursal = "";
                FILTRO_TipoProducto = 0;
                FILTRO_FamiliaProductos = 0;
                FILTRO_MotivoCompra = 0;

                int PageNo;
                int PageSize;
                int Id_Com;
                int Id_Estatus;

                PageNo = 0;
                PageSize = 0;
                Id_Com = 0;
                Id_Estatus = 0;
                Sucursales = "";

                //RBM
                //List<eCatCDI> lstSucursales = new List<eCatCDI>();
                //CN_CatCDI CN_CatCdi = new CN_CatCDI();
                //lstSucursales = CN_CatCdi.spCatCDI_ComboTodos_ver2(1, session.Emp_Cnx);

                // Sucursales / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / / 

                //RBN
                //int xControl = 0;
                //foreach (eCatCDI CDI in lstSucursales)
                //{
                //    if (Request.Form["chbSucursal_" + xControl.ToString()] != null && Request.Form["chbSucursal_" + xControl.ToString()] == "on")
                //    {
                //        if (Sucursales == "")
                //        {
                //            Sucursales = CDI.Id_Cd.ToString() + ",";
                //        }
                //        else
                //        {
                //            Sucursales = Sucursales + "," + CDI.Id_Cd.ToString();
                //        }
                //    }
                //    else
                //    {
                //    }
                //    xControl = xControl + 1;
                //}
                //if (Sucursales == "")
                //{
                //    Ejecutar_Function_JavaScript("CLIndex_Inicializar();");
                //    Alerta("Sebe seleccionar al menus una sucursal");
                //    return;
                //}
            }
            catch (Exception e)
            {
                Alerta("Error 2.");
                return;
            }

            int Verifica;
            Verifica = 0;

            // Consulta de informacion 
            CapaEntidad.eReporteMensual Res = new CapaEntidad.eReporteMensual();
            CapaEntidad.eReporteMensual ResTmp = new CapaEntidad.eReporteMensual();
            CN_Compras_Locales CN = new CN_Compras_Locales();

            try
            {
                //    //CapaEntidad.eReporteMensual ReporteMensual = new CapaEntidad.eReporteMensual();                
                //    ResTmp = CN.spGetCL_MultiplesConsultas(Sucursales, FechaInicial, FechaFinal, 1, session, ref Verifica);
                //    Res.MotivoCompra = ResTmp.MotivoCompra;

                //    ResTmp = CN.spGetCL_MultiplesConsultas(Sucursales, FechaInicial, FechaFinal, 2, session, ref Verifica);
                //    Res.PorSucursal = ResTmp.PorSucursal;

                //    ResTmp = CN.spGetCL_MultiplesConsultas(Sucursales, FechaInicial, FechaFinal, 3, session, ref Verifica);
                //    Res.PorTipoProducto = ResTmp.PorTipoProducto;

                //    ResTmp = CN.spGetCL_MultiplesConsultas(Sucursales, FechaInicial, FechaFinal, 4, session, ref Verifica);
                //    Res.MontoProveedorLocal = ResTmp.MontoProveedorLocal;

                //    ResTmp = CN.spGetCL_MultiplesConsultas(Sucursales, FechaInicial, FechaFinal, 5, session, ref Verifica);
                //    Res.MontoCompra_Aplicacion = ResTmp.MontoCompra_Aplicacion;

                //    ResTmp = CN.spGetCL_MultiplesConsultas(Sucursales, FechaInicial, FechaFinal, 6, session, ref Verifica);
                //    Res.AltasSkus = ResTmp.AltasSkus;
                //    Res.Monto = ResTmp.Monto;
                //    Res.NoProveedores = ResTmp.NoProveedores;
                //    Verifica = 1;
                //    if (Verifica <= 0)
                //    {
                //        Ejecutar_Function_JavaScript("CLIndex_Inicializar();");
                //        Alerta("No se encontro información para desplegar, Verifique los parametros de consulta que sean correctos.");
                //        return;
                //    }
            }
            catch (Exception e)
            {
                Alerta("Error 3.");
                return;
            }

            DateTime FechaHoy = DateTime.Now;
            string sFechaHoy;
            sFechaHoy = FechaHoy.ToString();
            sFechaHoy = sFechaHoy.Replace("/", "");
            sFechaHoy = sFechaHoy.Replace(":", "");
            sFechaHoy = sFechaHoy.Replace(".", "");
            sFechaHoy = sFechaHoy.Replace(" ", "_");

            Excel.Application excelApp = new Excel.Application();

            excelApp.Visible = false; // Make the object visible.
            object misValue = System.Reflection.Missing.Value;

            // Create a new, empty workbook and add it to the collection returned
            // by property Workbooks. The new workbook becomes the active workbook.
            // Add has an optional parameter for specifying a particular template.
            // Because no argument is sent in this example, Add creates a new workbook.
            //excelApp.Workbooks.Add();
            var xlWorkBook = excelApp.Workbooks.Add(misValue);

            // This example uses a single workSheet. The explicit type casting is
            // removed in a later procedure.
            Excel._Worksheet workSheet = (Excel.Worksheet)excelApp.ActiveSheet;

            try
            {

                // Establish column headings in cells A1 and B1.            
                workSheet.Cells[1, "A"] = "INDICADORES DE GESTION DE COMPRAS LOCALES";
                workSheet.Cells[1, "A"].Font.Size = 20;
                workSheet.Cells[1, "A"].Font.Bold = true;
                workSheet.Cells[2, "A"] = "Fecha solicitud:";
                workSheet.Cells[2, "A"].Font.Bold = true;
                workSheet.Cells[3, "A"] = "de:";
                workSheet.Cells[4, "A"] = "a:";
                workSheet.Cells[3, "B"] = SFechaInicial;
                Excel.Range RangeFI;
                RangeFI = workSheet.get_Range("B3", "B3");
                RangeFI.Interior.Color = System.Drawing.Color.Gray;
                workSheet.Cells[4, "B"] = SFechaFinal;
                Excel.Range RangeFF;
                RangeFF = workSheet.get_Range("B4", "B4");
                RangeFF.Interior.Color = System.Drawing.Color.Gray;
                workSheet.Cells[6, "A"] = "Sucursal:";
                workSheet.Cells[6, "A"].Font.Bold = true;
                workSheet.Cells[7, "B"] = Sucursales;
                Excel.Range RangeSuc;
                RangeSuc = workSheet.get_Range("B7", "B7");
                RangeSuc.Interior.Color = System.Drawing.Color.Gray;


                workSheet.Cells[9, "A"] = "Tipo de producto:";
                workSheet.Cells[9, "A"].Font.Bold = true;
                //workSheet.Cells[10, "A"] = "--------------------------";
                Excel.Range RangeTP;
                RangeTP = workSheet.get_Range("B10", "B10");
                RangeTP.Interior.Color = System.Drawing.Color.Gray;

                workSheet.Cells[14, "A"] = "Altas de SKUs";
                workSheet.Cells[14, "A"].Font.Size = 15;
                workSheet.Cells[14, "A"].Font.Bold = true;
                workSheet.Cells[15, "B"] = Res.AltasSkus.ToString();
                workSheet.Cells[15, "B"].Font.Size = 15;
                workSheet.Cells[15, "B"].Font.Bold = true;
                Excel.Range Rango1;
                Rango1 = workSheet.get_Range("A14", "B16");
                //Rango1.Borders.LineStyle = Excel.XlBorderWeight.xlThin;
                Excel.Borders border1 = Rango1.Borders;
                border1[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                border1[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
                border1[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                border1[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;

                workSheet.Cells[14, "D"] = "Monto de Compra Local";
                workSheet.Cells[14, "D"].Font.Size = 15;
                workSheet.Cells[14, "D"].Font.Bold = true;
                workSheet.Cells[15, "E"] = Res.Monto.ToString();
                workSheet.Cells[15, "E"].Font.Size = 15;
                workSheet.Cells[15, "E"].Font.Bold = true;
                Excel.Range Rango2;
                Rango2 = workSheet.get_Range("D14", "E16");
                //Rango1.Borders.LineStyle = Excel.XlBorderWeight.xlThin;
                Excel.Borders border2 = Rango2.Borders;
                border2[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                border2[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
                border2[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                border2[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;

                Excel.Range chartRangeMontoCL;
                chartRangeMontoCL = workSheet.get_Range("E15", "E15");
                chartRangeMontoCL.NumberFormat = "$ #,##0.00";

                workSheet.Cells[14, "G"] = "#Proveedores Locales";
                workSheet.Cells[14, "G"].Font.Size = 15;
                workSheet.Cells[14, "G"].Font.Bold = false;
                workSheet.Cells[15, "H"] = Res.NoProveedores.ToString();
                workSheet.Cells[15, "H"].Font.Size = 15;
                workSheet.Cells[15, "H"].Font.Bold = true;
                Excel.Range Rango3;
                Rango3 = workSheet.get_Range("G14", "H16");
                //Rango1.Borders.LineStyle = Excel.XlBorderWeight.xlThin;
                Excel.Borders border3 = Rango3.Borders;
                border3[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                border3[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
                border3[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                border3[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;

                // X, Y, Ancho , Alto    

                // Grafica 1 
                // Datos Grafica 1 - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
                workSheet.Cells[18, "A"] = "Monto por motivo de compra";
                workSheet.Cells[18, "A"].Font.Bold = true;
                foreach (eClRm_MotivoCompra XMotivoC in Res.MotivoCompra)
                {
                    workSheet.Cells[19, "A"] = "1";
                    workSheet.Cells[20, "A"] = "2";
                    workSheet.Cells[21, "A"] = "3";
                    workSheet.Cells[19, "B"] = XMotivoC.Importe1;
                    workSheet.Cells[20, "B"] = XMotivoC.Importe2;
                    workSheet.Cells[21, "B"] = XMotivoC.Importe3;
                }
                Excel.Range chartRangeX1;
                chartRangeX1 = workSheet.get_Range("B19", "B21");
                chartRangeX1.NumberFormat = "$ #,##0.00";

                Excel.Range chartRange1;
                Excel.ChartObjects xlCharts = (Excel.ChartObjects)workSheet.ChartObjects(Type.Missing);
                Excel.ChartObject myChart1 = (Excel.ChartObject)xlCharts.Add(150, 270, 250, 200);
                Excel.Chart chartPage = myChart1.Chart;

                chartRange1 = workSheet.get_Range("B19", "B21");
                chartPage.SetSourceData(chartRange1, misValue);
                chartPage.ChartType = Excel.XlChartType.xlPie;


                // Grafica 2 - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
                // Datos Grafica 2 
                workSheet.Cells[18, "H"] = "Monto por sucursal";
                workSheet.Cells[18, "H"].Font.Bold = true;
                int RowG2 = 19;
                foreach (eClRm_PorSucursal XSucursal in Res.PorSucursal)
                {
                    workSheet.Cells[RowG2, "H"] = XSucursal.NombreSucursal;
                    workSheet.Cells[RowG2, "I"] = XSucursal.Monto;
                    RowG2 = RowG2 + 1;
                }
                Excel.Range chartRangeX2;
                chartRangeX2 = workSheet.get_Range("I19", "I" + (RowG2 - 1).ToString());
                chartRangeX2.NumberFormat = "$ #,##0.00";

                Excel.Range chartRange2;
                Excel.ChartObjects xlCharts2 = (Excel.ChartObjects)workSheet.ChartObjects(Type.Missing);
                Excel.ChartObject myChart2 = (Excel.ChartObject)xlCharts2.Add(600, 270, 250, 200);
                Excel.Chart chartPage2 = myChart2.Chart;

                chartRange2 = workSheet.get_Range("H19", "I" + (RowG2 - 1).ToString());
                chartPage2.SetSourceData(chartRange2, misValue);
                chartPage2.ChartType = Excel.XlChartType.xlPie;

                // Grafica 3  - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
                workSheet.Cells[35, "A"] = "Monto compra local por Tipo";
                workSheet.Cells[35, "A"].Font.Bold = true;

                int RowG3 = 36;
                foreach (eClRm_PorTipoProducto XTP in Res.PorTipoProducto)
                {
                    workSheet.Cells[RowG3, "A"] = XTP.TipoProducto;
                    workSheet.Cells[RowG3, "B"] = XTP.Monto;
                    RowG3 = RowG3 + 1;
                }
                Excel.Range chartRangeX3;
                chartRangeX3 = workSheet.get_Range("B36", "B" + (RowG3 - 1).ToString());
                chartRangeX3.NumberFormat = "$ #,##0.00";

                Excel.Range chartRange3;
                Excel.ChartObjects xlCharts3 = (Excel.ChartObjects)workSheet.ChartObjects(Type.Missing);
                Excel.ChartObject myChart3 = (Excel.ChartObject)xlCharts3.Add(150, 500, 250, 200);
                Excel.Chart chartPage3 = myChart3.Chart;

                chartRange3 = workSheet.get_Range("A36", "B" + (RowG3 - 1).ToString());
                chartPage3.SetSourceData(chartRange3, misValue);
                chartPage3.ChartType = Excel.XlChartType.xlPie;

                // Grafica 4 - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
                workSheet.Cells[35, "H"] = "Monto por proveedor local";
                workSheet.Cells[35, "H"].Font.Bold = true;

                int RowG4 = 36;
                foreach (eClRm_MontoProveedorLocal MPL in Res.MontoProveedorLocal)
                {
                    workSheet.Cells[RowG4, "H"] = MPL.NombreProveedor;
                    workSheet.Cells[RowG4, "I"] = MPL.Monto;
                    RowG4 = RowG4 + 1;
                }
                Excel.Range chartRangeX4;
                chartRangeX4 = workSheet.get_Range("I36", "I" + (RowG4 - 1).ToString());
                chartRangeX4.NumberFormat = "$ #,##0.00";

                Excel.Range chartRange4;
                Excel.ChartObjects xlCharts4 = (Excel.ChartObjects)workSheet.ChartObjects(Type.Missing);
                Excel.ChartObject myChart4 = (Excel.ChartObject)xlCharts4.Add(600, 500, 250, 200);
                Excel.Chart chartPage4 = myChart4.Chart;

                chartRange4 = workSheet.get_Range("H36", "I" + (RowG4 - 1).ToString());
                chartPage4.SetSourceData(chartRange4, misValue);
                chartPage4.ChartType = Excel.XlChartType.xlPie;

                // Grafica 5 - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

                workSheet.Cells[52, "A"] = "Monto compra local por";
                workSheet.Cells[52, "A"].Font.Bold = true;
                workSheet.Cells[53, "A"] = "Aplicación";
                workSheet.Cells[53, "A"].Font.Bold = true;

                int RowG5 = 54;
                foreach (eClRm_MontoCompra_Aplicacion MPA in Res.MontoCompra_Aplicacion)
                {
                    workSheet.Cells[RowG5, "A"] = MPA.AplicacionNombre;
                    workSheet.Cells[RowG5, "B"] = MPA.Monto;
                    RowG5 = RowG5 + 1;
                }
                Excel.Range chartRangeX5;
                chartRangeX5 = workSheet.get_Range("B54", "B" + (RowG5 - 1).ToString());
                chartRangeX5.NumberFormat = "$ #,##0.00";

                Excel.Range chartRange5;
                Excel.ChartObjects xlCharts5 = (Excel.ChartObjects)workSheet.ChartObjects(Type.Missing);
                Excel.ChartObject myChart5 = (Excel.ChartObject)xlCharts5.Add(180, 750, 250, 200);
                Excel.Chart chartPage5 = myChart5.Chart;

                chartRange5 = workSheet.get_Range("A54", "B" + (RowG5 - 1).ToString());
                chartPage5.SetSourceData(chartRange5, misValue);
                chartPage5.ChartType = Excel.XlChartType.xlPie;

                string sFileName;
                sFileName = Server.MapPath("CLArchivos") + "\\ComprasLocales_ReporeGraficas_" + sFechaHoy + ".xls";

                xlWorkBook.SaveAs(sFileName, Excel.XlFileFormat.xlWorkbookNormal,
                    misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);

                xlWorkBook.Close(true, misValue, misValue);
                excelApp.Visible = true; // Make the object visible.
                excelApp.Quit();

                Alerta("Se ha generado el archivo: " + sFileName + " con formato excel, en la carpeta documentos.");

                //string SMP="";
                //SMP = Server.MapPath("C:\\ComprasLocales\\");

                //Response.Redirect(sFileName, false);
                /*
                string Ruta_Archivo = "";
                Ruta_Archivo = HttpContext.Current.Server.MapPath("");
                Alerta("Ruta: " + Ruta_Archivo);
                */
                /*
                Response.ContentType = "Application/x-msexcel";
                Response.AppendHeader("Content-Disposition", "attachment; filename=MyFile.pdf");
                //Response.TransmitFile(Server.MapPath("~/Files/MyFile.pdf"));
                Response.TransmitFile(Server.MapPath("CLArchivos") + "\\ComprasLocales_ReporeGraficas_" + sFechaHoy + ".xls");
                Response.End();
                */

                Response.Redirect("CLArchivos\\ComprasLocales_ReporeGraficas_" + sFechaHoy + ".xls", false);

                Ejecutar_Function_JavaScript("CLIndex_Inicializar();");

            }
            catch (Exception e)
            {
                Alerta("Error 4.");
                return;
            }
            return;
        }

        private void Alerta(string mensaje)
        {
            try
            {
                //RAM1.ResponseScripts.Add("radalert('" + mensaje + "</br></br>', 330, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }

        private void ValidarPermisos()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Pagina pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                if (pag.Length > 1)
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                else
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;

                pagina.Url = "CL/" + pagina.Url;

                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, Sesion.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;
            }
            catch (Exception ex)
            {
                this.Title = "Error";
            }
        }

        private bool ValidarSesion()
        {
            try
            {
                if (session == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                    return false;
                }
                return true;
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
                //this.lblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();
            }
            catch (Exception ex)
            {
                //this.lblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                string cmd = e.Argument.ToString();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }

        private void Ejecutar_Function_JavaScript(string Funcion_Nombre)
        {
            try
            {
                string funcion;
                funcion = Funcion_Nombre; // "CloseAndRebind()";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}