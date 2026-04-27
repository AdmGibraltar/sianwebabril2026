using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Configuration;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace SIANWEB
{
    public partial class Rep_FaltanteOportunidad : System.Web.UI.Page
    {
        #region Variables
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private Sesion sesion
        {
            get
            {
                return (Sesion)Session["Sesion" + Session.SessionID];
            }
            set
            {
                Session["Sesion" + Session.SessionID] = value;
            }
        }
        #endregion Variables

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        this.ValidarPermisos();
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            return;
                        }
                        this.CargarCentros();
                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_Cve.Value = randObj.Next().ToString();
                        this.TblEncabezado.Visible = false;

                    }
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }
        }

        #region Funciones
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
                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, Sesion.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;

                Permiso Permiso = new Permiso();
                Permiso.Id_U = Sesion.Id_U;
                Permiso.Id_Cd = Sesion.Id_Cd;
                Permiso.Sm_cve = pagina.Clave;
                //Esta clave depende de la pantalla

                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, Sesion.Emp_Cnx);

                if (Permiso.PAccesar == true)
                    _PermisoImprimir = Permiso.PImprimir;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarCentros()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();


                if (Sesion.U_MultiOfi == false)
                {
                    CN_Comun.LlenaCombo(2, Sesion.Id_Emp, Sesion.Id_U, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.Visible = false;
                    this.TblEncabezado.Rows[0].Cells[2].InnerText = " " + CmbCentro.FindItemByValue(Sesion.Id_Cd_Ver.ToString()).Text;
                }
                else
                {
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, sesion.Id_Cd_Ver, sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = Sesion.Id_Cd_Ver.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }

        private static ExcelWorksheet CreateSheet(ExcelPackage p, string sheetName, int index)
        {
            p.Workbook.Worksheets.Add(sheetName);
            ExcelWorksheet ws = p.Workbook.Worksheets[index];
            ws.Name = sheetName; //Setting Sheet's name
            ws.Cells.Style.Font.Size = 11; //Default font size for whole sheet
            ws.Cells.Style.Font.Name = "Calibri"; //Default Font name for whole sheet

            return ws;
        }

        private static ExcelWorksheet CreateSheet2(ExcelPackage p, string sheetName)
        {
            p.Workbook.Worksheets.Add(sheetName);
            ExcelWorksheet ws = p.Workbook.Worksheets[2];
            ws.Name = sheetName; //Setting Sheet's name
            ws.Cells.Style.Font.Size = 11; //Default font size for whole sheet
            ws.Cells.Style.Font.Name = "Calibri"; //Default Font name for whole sheet

            return ws;
        }

        private static void SetWorkbookProperties(ExcelPackage p)
        {
            //Here setting some document properties
            p.Workbook.Properties.Author = "Raúl Bórquez Martínez ";
            p.Workbook.Properties.Title = "Reportes Excesos de Inventarios que no Rotan";
        }

        private static void CreateHeader(ExcelWorksheet ws, ref int rowIndex, DataTable dt)
        {


            int colIndex = 1;
            foreach (DataColumn dc in dt.Columns) //Creating Headings
            {
                var cell = ws.Cells[rowIndex, colIndex];

                //Setting the background color of header cells to Gray
                var fill = cell.Style.Fill;
                fill.PatternType = ExcelFillStyle.Solid;
                fill.BackgroundColor.SetColor(Color.DarkGray);

                //Setting Top/left,right/bottom borders.
                var border = cell.Style.Border;
                border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;

                //Setting Value in cell
                cell.Value = dc.ColumnName;

                colIndex++;
            }
        }

        private static void CreateData(ExcelWorksheet ws, ref int rowIndex, DataTable dt)
        {
            int colIndex = 0;
            foreach (DataRow dr in dt.Rows) // Adding Data into rows
            {
                colIndex = 1;
                rowIndex++;

                foreach (DataColumn dc in dt.Columns)
                {
                    var cell = ws.Cells[rowIndex, colIndex];

                    //Setting Value in cell
                    if (dc.DataType == typeof(System.Double) && dc.ColumnName != "Cobertura (Días)")
                    {
                        if (dc.ColumnName == "Acumulado")
                        {
                            cell.Value = dr[dc.ColumnName];
                            cell[rowIndex, colIndex].Style.Numberformat.Format = "#0\\.00%";
                        }
                        else
                        {
                            if (dc.ColumnName == "Promedio de Venta (Unidades)")
                            {
                                cell.Value = dr[dc.ColumnName];
                            }
                            else
                            {
                                cell.Value = dr[dc.ColumnName];
                                cell[rowIndex, colIndex].Style.Numberformat.Format = "$#,##0.00";
                            }
                        }
                    }
                    else
                    {
                        cell.Value = dr[dc.ColumnName];
                    }

                    //Setting borders of cell
                    var border = cell.Style.Border;
                    border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                    colIndex++;
                }
            }
        }

        private static void CreateFooter(ExcelWorksheet ws, ref int rowIndex, DataTable dt)
        {
            int colIndex = 0;
            foreach (DataColumn dc in dt.Columns) //Creating Formula in footers
            {
                colIndex++;
                var cell = ws.Cells[rowIndex, colIndex];

                //Setting Sum Formula
                cell.Formula = "Sum(" + ws.Cells[4, colIndex].Address + ":" + ws.Cells[rowIndex - 1, colIndex].Address + ")";

                //Setting Background fill color to Gray
                cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cell.Style.Fill.BackgroundColor.SetColor(Color.DarkGray);
            }
        }

        #endregion Funciones

        private void CargarSucursales(int Id_Grupo)
        {
            try
            {
                //Sesion Sesion = new CapaEntidad.Sesion();
                //Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                //System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                //CN_FaltanteOportunidad CN = new CN_FaltanteOportunidad();
                //CN.LlenaCombo(Id_Grupo, Sesion.Emp_Cnx, "spCatSucursales_Combo", ref Lista);
                //if (Lista.Count > 0)
                //{
                //    Cmbsucursal.DataSource = Lista;
                //    Cmbsucursal.DataValueField = "Id";
                //    Cmbsucursal.DataTextField = "Descripcion";
                //    Cmbsucursal.DataBind();
                //}
            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }
        }

        #region Eventos
        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);


            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    case "RebindGrid":
                        //this.rgFacturaRuta.Rebind();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }
        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (Page.IsValid)
                {
                    if (btn.CommandName == "excel")
                    {
                        DescargaExcel();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private void DescargaExcel()
        {
            FaltanteOportunidad Faltante = new FaltanteOportunidad();

            Faltante.TipoReporte = RblTipoRep.SelectedValue;
            Faltante.TipoDesglose = RblTipoDesglose.SelectedItem.Text;
            Faltante.Desglose = RblTipoDesglose.SelectedValue;
            Faltante.Id_Cd = sesion.Id_Cd;

            List<FaltanteOportunidad> List = new List<FaltanteOportunidad>();
            CN_FaltanteOportunidad CN = new CN_FaltanteOportunidad();
            DataSet dsResultado = new DataSet();

            //Detalle
            CN.ConsultaFaltanteOportunidad(sesion.Emp_Cnx, Faltante, ref dsResultado);
            FaltanteOportunidadDetalle(dsResultado, Faltante);
        }

        private void FaltanteOportunidadDetalle(DataSet dsResultado, FaltanteOportunidad Faltante)
        {
            try
            {
                string ruta = null;
                Random rnd = new Random();
                int nro = rnd.Next(0, 8);
                string tipo = "FaltanteOportunidad";
                ruta = Server.MapPath("Reportes") + "\\Rep_" + tipo + nro + ".xlsx";

                if (File.Exists(ruta))
                    File.Delete(ruta);
                if (File.Exists(Server.MapPath("Reportes") + "\\Rep_" + tipo + nro + ".xls"))
                    File.Delete(Server.MapPath("Reportes") + "\\Rep_" + tipo + nro + ".xls");

                if ((dsResultado.Tables[0] != null))
                {
                    if (!(dsResultado.Tables[0].Rows.Count == 0))
                    {
                        using (ExcelPackage p = new ExcelPackage())
                        {
                            //set the workbook properties and add a default sheet in it
                            SetWorkbookProperties(p);
                            //Create a sheet
                            Funcion func = new Funcion();
                            List<object> lCDI = (from r in dsResultado.Tables[0].AsEnumerable()
                                                 select r["Id_Cd"]).Distinct().ToList();
                            Dictionary<int, double> lCDTOTAL = new Dictionary<int, double>();
                            int count = 1;
                            lCDI.Sort();

                            foreach (int a in lCDI)
                            {
                                string NomMes1 = "", NomMes2 = "", NomMes3 = "", NomMes4 = "", NomMes5 = "", NomMes6 = "";

                                foreach (DataRow rowMes in dsResultado.Tables[2].Rows)
                                {
                                    if (Convert.ToInt32(rowMes[0]) == 1)
                                        NomMes1 = rowMes[3] + " - " + rowMes[2];
                                    if (Convert.ToInt32(rowMes[0]) == 2)
                                        NomMes2 = rowMes[3] + " - " + rowMes[2];
                                    if (Convert.ToInt32(rowMes[0]) == 3)
                                        NomMes3 = rowMes[3] + " - " + rowMes[2];
                                    if (Convert.ToInt32(rowMes[0]) == 4)
                                        NomMes4 = rowMes[3] + " - " + rowMes[2];
                                    if (Convert.ToInt32(rowMes[0]) == 5)
                                        NomMes5 = rowMes[3] + " - " + rowMes[2];
                                    if (Convert.ToInt32(rowMes[0]) == 6)
                                        NomMes6 = rowMes[3] + " - " + rowMes[2];

                                }

                                ExcelWorksheet ws = CreateSheet(p, func.getCDIName(a), count);
                                DataTable dtDetalle = new DataTable();
                                DataColumn Sucursal = new DataColumn("Sucursal", Type.GetType("System.String"));
                                DataColumn Codigo = new DataColumn("Código", Type.GetType("System.Int32"));
                                DataColumn Descripcion = new DataColumn("Descripción", Type.GetType("System.String"));
                                DataColumn Estatus = new DataColumn("Estatus", Type.GetType("System.String"));
                                DataColumn Tipo = new DataColumn("Tipo", Type.GetType("System.String"));
                                DataColumn Mes6 = new DataColumn(NomMes6, Type.GetType("System.Int32"));
                                DataColumn Mes5 = new DataColumn(NomMes5, Type.GetType("System.Int32"));
                                DataColumn Mes4 = new DataColumn(NomMes4, Type.GetType("System.Int32"));
                                DataColumn Mes3 = new DataColumn(NomMes3, Type.GetType("System.Int32"));
                                DataColumn Mes2 = new DataColumn(NomMes2, Type.GetType("System.Int32"));
                                DataColumn Mes1 = new DataColumn(NomMes1, Type.GetType("System.Int32"));
                                DataColumn PromVenta = new DataColumn("Promedio de Venta (Unidades)", Type.GetType("System.Double"));
                                DataColumn InvDia = new DataColumn("Inventario al día (Unidades)", Type.GetType("System.Int32"));
                                DataColumn Cobertura = new DataColumn("Cobertura (Días)", Type.GetType("System.Double"));
                                DataColumn PrecioAAA = new DataColumn("Precio AAA (Monto)", Type.GetType("System.Double"));
                                DataColumn VentaPotPromAAA = new DataColumn("Venta Pot Prom AAA", Type.GetType("System.Double"));
                                DataColumn MontoInv = new DataColumn("Monto Inv", Type.GetType("System.Double"));
                                DataColumn Acumulado = new DataColumn("Acumulado", Type.GetType("System.Double"));
                                DataColumn CodigoVelocidad = new DataColumn("Código de  velocidad", Type.GetType("System.String"));
                                DataColumn Bandera = new DataColumn("Bandera", Type.GetType("System.Int32"));
                                DataColumn OpVentaTotal = new DataColumn("Op Venta Tot", Type.GetType("System.Double"));

                                dtDetalle.Columns.Add(Sucursal);
                                dtDetalle.Columns.Add(Codigo);
                                dtDetalle.Columns.Add(Descripcion);
                                dtDetalle.Columns.Add(Estatus);
                                dtDetalle.Columns.Add(Tipo);
                                dtDetalle.Columns.Add(Mes6);
                                dtDetalle.Columns.Add(Mes5);
                                dtDetalle.Columns.Add(Mes4);
                                dtDetalle.Columns.Add(Mes3);
                                dtDetalle.Columns.Add(Mes2);
                                dtDetalle.Columns.Add(Mes1);
                                dtDetalle.Columns.Add(PromVenta);
                                dtDetalle.Columns.Add(InvDia);
                                dtDetalle.Columns.Add(Cobertura);
                                dtDetalle.Columns.Add(PrecioAAA);
                                dtDetalle.Columns.Add(VentaPotPromAAA);
                                dtDetalle.Columns.Add(MontoInv);
                                dtDetalle.Columns.Add(Acumulado);
                                dtDetalle.Columns.Add(CodigoVelocidad);
                                dtDetalle.Columns.Add(Bandera);
                                dtDetalle.Columns.Add(OpVentaTotal);

                                double CalAcumulado = 0;
                                string Velocidad = "";
                                int vBandera = 0;
                                double vOpventa = 0.0, SumatoriaA = 0.0, SumatoriaB = 0.0, SumatoriaC = 0.0, SumatoriaD = 0.0, SumatoriaE = 0.0, VentaProm = 0.0,
                                SumatoriaPromA = 0.0, SumatoriaPromB = 0.0, SumatoriaPromC = 0.0, SumatoriaPromD = 0.0, SumatoriaPromE = 0.0;

                                int Prd_InvFinal = 0;
                                double InvDiario = 0, Cober = 0;

                                foreach (DataRow row in dsResultado.Tables[1].Select("Id_Cd = '" + a + "'"))
                                {
                                    InvDiario = 0;
                                    Cober = 0;
                                    Prd_InvFinal = 0;
                                    foreach (DataRow rowval in dsResultado.Tables[0].Select("Id_Prd = '" + Int64.Parse(row[2].ToString()) + "'"))
                                    {
                                        InvDiario = Convert.ToDouble(rowval["Prd_InvFinal"]) * Convert.ToDouble(row["PrecioAAA"]);
                                        Cober = (Convert.ToDouble(rowval["Prd_InvFinal"]) / Convert.ToDouble(row[12])) * 30;
                                        Prd_InvFinal = Convert.ToInt32(rowval["Prd_InvFinal"]);

                                        if (double.IsInfinity(Cober))
                                            Cober = 0;

                                    }

                                    if (Convert.ToDouble(row[19]) >= 0)
                                    {
                                        CalAcumulado = CalAcumulado + Convert.ToDouble(row[19]);
                                        if (CalAcumulado > 100)
                                            CalAcumulado = 100.00;
                                    }
                                    else
                                    {
                                        CalAcumulado = CalAcumulado + 0;
                                    }

                                    if (Convert.ToDouble(row[16]) == 0)
                                        Velocidad = "E";
                                    if (CalAcumulado <= 39.99)
                                        Velocidad = "A";
                                    if (CalAcumulado >= 40.00 && CalAcumulado <= 69.99)
                                        Velocidad = "B";
                                    if (CalAcumulado >= 70.00 && CalAcumulado <= 89.99)
                                        Velocidad = "C";

                                    if (CalAcumulado >= 90.00 && CalAcumulado <= 100.00)
                                        Velocidad = "D";

                                    if (Convert.ToDouble(Cober) < 7)
                                        vBandera = 1;
                                    else
                                        vBandera = 0;

                                    if (vBandera == 1)
                                        vOpventa = 1 * Convert.ToDouble(row[16]);
                                    else
                                        vOpventa = 0;

                                    if (Velocidad == "E")
                                        SumatoriaE = SumatoriaE + vOpventa;
                                    if (Velocidad == "D")
                                        SumatoriaD = SumatoriaD + vOpventa;
                                    if (Velocidad == "C")
                                        SumatoriaC = SumatoriaC + vOpventa;
                                    if (Velocidad == "B")
                                        SumatoriaB = SumatoriaB + vOpventa;
                                    if (Velocidad == "A")
                                        SumatoriaA = SumatoriaA + vOpventa;

                                    if (Velocidad == "E")
                                        SumatoriaPromE = SumatoriaPromE + Convert.ToDouble(row[16]);
                                    if (Velocidad == "D")
                                        SumatoriaPromD = SumatoriaPromD + Convert.ToDouble(row[16]);
                                    if (Velocidad == "C")
                                        SumatoriaPromC = SumatoriaPromC + Convert.ToDouble(row[16]);
                                    if (Velocidad == "B")
                                        SumatoriaPromB = SumatoriaPromB + Convert.ToDouble(row[16]);
                                    if (Velocidad == "A")
                                        SumatoriaPromA = SumatoriaPromA + Convert.ToDouble(row[16]);

                                    VentaProm = Convert.ToDouble(row[18]);

                                    DataRow drFila = null;
                                    drFila = dtDetalle.NewRow();
                                    drFila[0] = row[1];
                                    drFila[1] = row[2];
                                    drFila[2] = row[3];
                                    drFila[3] = row[4];
                                    drFila[4] = row[5];
                                    drFila[5] = row[6];
                                    drFila[6] = row[7];
                                    drFila[7] = row[8];
                                    drFila[8] = row[9];
                                    drFila[9] = row[10];
                                    drFila[10] = row[11];
                                    drFila[11] = row[12];
                                    drFila[12] = Prd_InvFinal;
                                    drFila[13] = Math.Round(Cober, 2);
                                    drFila[14] = row[15];
                                    drFila[15] = row[16];
                                    drFila[16] = InvDiario;
                                    drFila[17] = CalAcumulado;
                                    drFila[18] = Velocidad;
                                    drFila[19] = vBandera;
                                    drFila[20] = vOpventa;

                                    dtDetalle.Rows.Add(drFila);
                                    dtDetalle.AcceptChanges();
                                }


                                int rowIndex = 2;
                                Double TotalOpVenta = (SumatoriaA + SumatoriaB + SumatoriaC + SumatoriaD + SumatoriaE);
                                ws.Cells[2, 23].Value = "Código";
                                ws.Cells[2, 23].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                ws.Cells[2, 23].Style.Fill.BackgroundColor.SetColor(Color.DarkGray);
                                ws.Cells[3, 23].Value = "Código A";
                                ws.Cells[3, 24].Value = SumatoriaA;
                                ws.Cells[3, 24].Style.Numberformat.Format = "$#,##0.00";
                                ws.Cells[3, 25].Value = SumatoriaA / SumatoriaPromA;
                                ws.Cells[3, 25].Style.Numberformat.Format = "0%";

                                ws.Cells[4, 23].Value = "Código B";
                                ws.Cells[4, 24].Value = SumatoriaB;
                                ws.Cells[4, 24].Style.Numberformat.Format = "$#,##0.00";
                                ws.Cells[4, 25].Value = SumatoriaB / SumatoriaPromB;
                                ws.Cells[4, 25].Style.Numberformat.Format = "0%";

                                ws.Cells[5, 23].Value = "Código C";
                                ws.Cells[5, 24].Value = SumatoriaC;
                                ws.Cells[5, 24].Style.Numberformat.Format = "$#,##0.00";
                                ws.Cells[5, 25].Value = SumatoriaC / SumatoriaPromC;
                                ws.Cells[5, 25].Style.Numberformat.Format = "0%";

                                ws.Cells[6, 23].Value = "Código D";
                                ws.Cells[6, 24].Value = SumatoriaD;
                                ws.Cells[6, 24].Style.Numberformat.Format = "$#,##0.00";
                                ws.Cells[6, 25].Value = SumatoriaD / SumatoriaPromD;
                                ws.Cells[6, 25].Style.Numberformat.Format = "0%";

                                ws.Cells[7, 23].Value = "Código E";
                                ws.Cells[7, 24].Value = SumatoriaE;
                                ws.Cells[7, 24].Style.Numberformat.Format = "$#,##0.00";
                                ws.Cells[7, 25].Value = SumatoriaE / SumatoriaPromE;
                                ws.Cells[7, 25].Style.Numberformat.Format = "0%";

                                ws.Cells[8, 23].Value = "TOTAL OP VENTA";
                                ws.Cells[8, 23].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                ws.Cells[8, 23].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                ws.Cells[8, 24].Value = TotalOpVenta;
                                ws.Cells[8, 24].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                ws.Cells[8, 24].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                ws.Cells[8, 24].Style.Numberformat.Format = "$#,##0.00";

                                ws.Cells[8, 25].Value = TotalOpVenta / VentaProm;
                                ws.Cells[8, 25].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                ws.Cells[8, 25].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                ws.Cells[8, 25].Style.Numberformat.Format = "0%";

                                ws.Cells[9, 23].Value = "VENTA PROM";
                                ws.Cells[9, 23].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                ws.Cells[9, 23].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                ws.Cells[9, 24].Value = VentaProm;
                                ws.Cells[9, 24].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                ws.Cells[9, 24].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                ws.Cells[9, 24].Style.Numberformat.Format = "$#,##0.00";

                                ws.Cells[2, 24].Value = "Faltante Venta A";
                                ws.Cells[2, 24].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                ws.Cells[2, 24].Style.Fill.BackgroundColor.SetColor(Color.DarkGray);

                                ws.Cells[2, 25].Value = "% Op Venta Total";
                                ws.Cells[2, 25].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                ws.Cells[2, 25].Style.Fill.BackgroundColor.SetColor(Color.DarkGray);

                                CreateHeader(ws, ref rowIndex, dtDetalle);
                                CreateData(ws, ref rowIndex, dtDetalle);
                                count++;

                                ws.Column(1).AutoFit();
                                ws.Column(2).AutoFit();
                                ws.Column(3).AutoFit();
                                ws.Column(4).AutoFit();
                                ws.Column(5).AutoFit();
                                ws.Column(6).AutoFit();
                                ws.Column(7).AutoFit();
                                ws.Column(8).AutoFit();
                                ws.Column(9).AutoFit();
                                ws.Column(10).AutoFit();
                                ws.Column(11).AutoFit();
                                ws.Column(14).AutoFit();
                                ws.Column(15).AutoFit();
                                ws.Column(16).AutoFit();
                                ws.Column(17).AutoFit();
                                ws.Column(18).AutoFit();
                                ws.Column(20).AutoFit();
                                ws.Column(21).AutoFit();
                                ws.Column(22).AutoFit();
                                ws.Column(23).AutoFit();
                                ws.Column(24).AutoFit();
                                ws.Column(25).AutoFit();
                            }

                            Byte[] bin = p.GetAsByteArray();
                            File.WriteAllBytes(ruta, bin);


                            if (File.Exists(ruta))
                            {
                                string ruta2 = null;
                                ruta2 = Server.MapPath("Reportes") + "\\Rep_" + tipo + nro + ".xlsx";
                                Response.Redirect("Reportes\\Rep_" + tipo + nro + ".xlsx", false);
                            }
                        }
                    }
                    else
                    {
                        Alerta("No existen registros para los parametros seleccionados.");
                    }
                }
                else
                {
                    Alerta("No existen registros para los parametros seleccionados.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        protected void CmbCentro_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (sesion == null)
            {
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
            }
            CN__Comun comun = new CN__Comun();
            comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
            //nuevo();
        }
        protected void RblTipoRep_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void CmbGrupo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                //CargarSucursales(CmbGrupo.SelectedIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Eventos


        #region ErrorManager

        private void Alerta(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }

        private void Alerta2(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 600, 150);");
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

        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta("Error al cargar la página");
            else
                if (mensaje.Contains("Impresion_error"))
                    Alerta("Error al momento de imprimir");
                else
                    Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
        }
        #endregion
    }
}