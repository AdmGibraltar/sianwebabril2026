
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
    public partial class ReporteGAPGerente : System.Web.UI.Page
        {

            public DataTable dt
            {
                get
                {
                    return (DataTable)ViewState["dtReporteConsultaGAP"];
                }
                set
                {
                    ViewState["dtReporteConsultaGAP"] = value;

                }
            }


            public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
            public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
            private Sesion sesion { get { return (Sesion)Session["Sesion" + Session.SessionID]; } set { Session["Sesion" + Session.SessionID] = value; } }



            protected void page_init(object sender, EventArgs e)
            {
                if (Session["dtReporteConsultaGAP"] != null)
                {

                    //List<PreciadorSegmentoTamaño> ListaPreciadorSegmentoTamaño = new List<PreciadorSegmentoTamaño>();
                    //ListaPreciadorSegmentoTamaño = (List<PreciadorSegmentoTamaño>)Session["dtPreciadorClienteSegmento"];
                    //grid.DataSource = ListaPreciadorSegmentoTamaño;
                    //grid.DataBind();
                    List<ReporteGAP> listreporteGAP = new List<ReporteGAP>();

                    dt = (DataTable)Session["dtReporteConsultaGAP"];
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


                if (Session["dtReporteConsultaGAP"] != null)
                    {
                        Session["dtReporteConsultaGAP"] = null;
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

                CargaCdi();
                CargarRepresentantes();
         
                cmbRik.Value = "-1";

            //CmbSucursal.Items.Add("--Todas--", "-1");

            //CmbSucursal.Value = "-1";

            cargarDatos();



              
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



                    SistemaCompensacionGetXML confsistcompensacion = new SistemaCompensacionGetXML();
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
                cn_comun.DevLlenaCombo(2,  sesion.Id_Cd_Ver,sesion.Emp_Cnx, "spCatRik_ComboTodos", ref this.cmbRik);


                if (sesion.Id_TU == 2)
                {
                    this.cmbRik.Value = sesion.Id_Rik.ToString();
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

            

             
            reporteGAP.Id_Rik = -1;
              

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

                CN_ReporteGAP.ConsultaListaReporteGAPRiks(reporteGAP, ref listreporteGAP, Conexion);

                this.grdServicio.DataSource = listreporteGAP;
                this.grdServicio.DataBind();
                DataTable dataTable = ConvertToDataTable(listreporteGAP);
                dt = dataTable;
                Session["dtReporteConsultaGAP"] = dt;



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
                DataTable dtTemp = (DataTable)Session["dtReporteConsultaGAP"];

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



                Session["dtReporteConsultaGAP"] = dtTemp;


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
                DataTable dtTemp = (DataTable)Session["dtReporteConsultaGAP"];



                ASPxGridView gridView = (ASPxGridView)sender;
                int i = grdServicio.FindVisibleIndexByKeyValue(e.Keys["IdReporteGAP"]);
                e.Cancel = true;



                ((BootstrapGridView)sender).JSProperties["cpIsUpdated"] = 1;


                dtTemp.Rows[i].Delete();


                Session["dtReporteConsultaGAP"] = dtTemp;

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
                Session["dtReporteConsultaGAP"] = dtTemp;


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





