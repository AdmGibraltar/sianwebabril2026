using CapaEntidad;
using CapaNegocios;
using DevExpress.Export.Xl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Reporting.Processing;
using Telerik.Web.UI;

namespace SIANWEB
{
    public partial class Reporte_LogFactura : System.Web.UI.Page
    {
        #region Variables
        public string strEmp = System.Configuration.ConfigurationManager.AppSettings["VGEmpresa"];
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        #endregion
        #region Propiedades

        public int PermisoGuardar { get { return _PermisoGuardar == true ? 1 : 0; } }
        public int PermisoModificar { get { return _PermisoModificar == true ? 1 : 0; } }
        public int PermisoEliminar { get { return _PermisoEliminar == true ? 1 : 0; } }
        public int PermisoImprimir { get { return _PermisoImprimir == true ? 1 : 0; } }



        private string Emp_CnxCob
        {
            get { return ConfigurationManager.AppSettings.Get("strConnectionCobranza"); }
        }

        #endregion
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
                    Response.Redirect("login.aspx", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        this.ValidarPermisos();
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        this.Inicializar();

                        txtFecha1.DbSelectedDate = Sesion.CalendarioIni;
                        txtFecha2.DbSelectedDate = Sesion.CalendarioFin;

                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();
                    }
                }
            }
            catch (Exception ex)
            { //ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }
        }
        #region Eventos
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    case "RebindGrid":
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {

            if (txtFecha1.SelectedDate > txtFecha2.SelectedDate)
            {
                Alerta("La fecha de inicio debe ser menor a la fecha final");
                return;
            }

            string accionError = string.Empty;
            string mensajeError = string.Empty;
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;

                switch (btn.CommandName)
                {
                    case "mostrar":
                        mensajeError = "Impresion_error";
                        this.ImprimirExcel();
                        break;
                    case "excel":
                        mensajeError = "Impresion_error";
                        this.ImprimirExcel();
                        break;
                }
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, mensajeError);
                this.DisplayMensajeAlerta(mensaje);
            }
        }

        protected void cmbCentrosDist_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
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
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Cmb_CentroDistribucion_IndexChanging_error"));
            }
        }
        #endregion
        #region Funciones
        private void Imprimir(bool a_pantalla)
        {
            try
            {
                int VGEmpresa = 0;
                Int32.TryParse(strEmp, out VGEmpresa);

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ArrayList ALValorParametrosInternos = new ArrayList();

                string dbcobranza = (new SqlConnectionStringBuilder(Emp_CnxCob)).InitialCatalog;

                ALValorParametrosInternos.Add(sesion.Emp_Nombre);
                ALValorParametrosInternos.Add(sesion.Cd_Nombre);
                ALValorParametrosInternos.Add(sesion.U_Nombre);
                ALValorParametrosInternos.Add(DateTime.Now.ToString("dd/MM/yyyy"));
                ALValorParametrosInternos.Add(txtFecha1.SelectedDate == null ? "Todas" : Convert.ToDateTime(txtFecha1.SelectedDate).ToString("dd/MM/yyyy"));
                ALValorParametrosInternos.Add(txtFecha2.SelectedDate == null ? "Todas" : Convert.ToDateTime(txtFecha2.SelectedDate).ToString("dd/MM/yyyy"));


                Type instance = null;
                if (a_pantalla)
                {
                    if (VGEmpresa == sesion.Id_Emp)
                    {
                        instance = typeof(LibreriaReportes.Rep_FacRegistroFacturacionBts);
                    }
                    else
                    {
                        instance = typeof(LibreriaReportes.Rep_FacRegistroFacturacion);
                    }
                }
                else
                {
                    if (VGEmpresa == sesion.Id_Emp)
                    {
                        instance = typeof(LibreriaReportes.ExpRep_FacRegistroFacturacionBts);
                    }
                    else
                    {
                        instance = typeof(LibreriaReportes.ExpRep_FacRegistroFacturacion);
                    }
                }


                //NOTA: El estatus de impresión, lo pone cuando el reporte se carga
                if (_PermisoImprimir)
                    if (a_pantalla)
                    {
                        Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = ALValorParametrosInternos;
                        Session["assembly" + Session.SessionID + HF_ClvPag.Value] = instance.AssemblyQualifiedName;
                        RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_ClvPag.Value + "');");
                    }
                    else
                    {
                        ImprimirXLS(ALValorParametrosInternos, instance);
                    }
                else
                    Alerta("No tiene permiso para imprimir");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ImprimirXLS(ArrayList ALValorParametrosInternos, Type instance)
        {
            try
            {
                Telerik.Reporting.Report report1 = (Telerik.Reporting.Report)Activator.CreateInstance(instance);
                for (int i = 0; i <= ALValorParametrosInternos.Count - 1; i++)
                {
                    report1.ReportParameters[i].AllowNull = true;
                    report1.ReportParameters[i].Value = ALValorParametrosInternos[i];
                }
                ReportProcessor reportProcessor = new ReportProcessor();
                RenderingResult result = reportProcessor.RenderReport("XLS", report1, null);
                string ruta = Server.MapPath("Reportes") + "\\" + instance.Name + ".xls";
                if (File.Exists(ruta))
                    File.Delete(ruta);
                FileStream fs = new FileStream(ruta, FileMode.Create);
                fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);
                fs.Flush();
                fs.Close();
                RAM1.ResponseScripts.Add("startDownload('" + instance.Name + ".xls');");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Inicializar()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            this.CargarCentros();


            this.CargarConsecutivos();
        }

        private void ValidarPermisos()
        {
            try
            {
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];

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
                {
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;
                }
                else
                    Response.Redirect("Inicio.aspx");
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
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, Sesion.Id_Cd_Ver, Sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = Sesion.Id_Cd_Ver.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarConsecutivos()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                int tipo = 1;//facturas
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, tipo, Sesion.Emp_Cnx, "spCatConsFactEle_Combo", ref cmbSerie);
            }
            catch (Exception ex)
            {
                throw ex;
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

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string Fecha = txtFecha1.SelectedDate == null ? "" : Convert.ToDateTime(txtFecha1.SelectedDate).ToString("yyyy-MM-dd");
            string Fecha2 = txtFecha2.SelectedDate == null ? "" : Convert.ToDateTime(txtFecha2.SelectedDate).ToString("yyyy-MM-dd");
            string serie = cmbSerie.Text.Trim();
            if (serie == "-- Seleccionar --")
            {
                AlertaFocus("Seleccione la serie", cmbSerie.ClientID);
            }
            else if (Fecha == "")
            {
                AlertaFocus("Seleccione la fecha inicial", cmbSerie.ClientID);
            }
            else if (Fecha2 == "")
            {
                AlertaFocus("Seleccione la fecha final", cmbSerie.ClientID);
            }
            else
            {
                rgLogDocumento.Rebind();
            }
            //GetLogList();

        }

        private List<eReporte_LogFactura> GetLogList()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                string Fecha = txtFecha1.SelectedDate == null ? "" : Convert.ToDateTime(txtFecha1.SelectedDate).ToString("yyyy-MM-dd");
                string Fecha2 = txtFecha2.SelectedDate == null ? "" : Convert.ToDateTime(txtFecha2.SelectedDate).ToString("yyyy-MM-dd");

                List<eReporte_LogFactura> List = new List<eReporte_LogFactura>();
                CN_Reporte_LogFactura clsCatMeta = new CN_Reporte_LogFactura();

                eReporte_LogFactura osearch = new eReporte_LogFactura();
                osearch.Id_Emp = sesion.Id_Emp;
                osearch.Id_Cd = sesion.Id_Cd_Ver;
                osearch.FechaIni = Fecha;
                osearch.FechaFin = Fecha2;
                osearch.Id_Cte = txtCliente.Text.Trim() != "" ? int.Parse(txtCliente.Text) : 0;
                osearch.Fac_FolioFiscal = cmbSerie.Text;
                osearch.Id_Fac = txtFactura.Text.Trim() != "" ? int.Parse(txtFactura.Text) : 0;
                clsCatMeta.ListaLog(osearch, sesion.Emp_Cnx, ref List);
                Session["ListaFacturas" + Session.SessionID] = List;

                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void rgLogDocumento_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    rgLogDocumento.DataSource = GetLogList();
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }

        protected void rgLogDocumento_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                rgLogDocumento.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        private void ImprimirExcel()
        {
            try
            {
                //List<eReporte_LogFactura> List=GetLogList();

                List<eReporte_LogFactura> List = Session["ListaFacturas" + Session.SessionID] as List<eReporte_LogFactura>;

                // Create an exporter instance.  
                IXlExporter exporter = XlExport.CreateExporter(XlDocumentFormat.Xlsx);

                var mappath = HttpContext.Current.Server.MapPath("~/Reportes/RerpoteCaptacionPedidos" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx");
                // Create the FileStream object with the specified file path.  

                if (File.Exists(mappath))
                    File.Delete(mappath);

                #region excel
                using (FileStream stream = new FileStream(mappath, FileMode.Create, FileAccess.ReadWrite))
                {
                    // Create a new document and begin to write it to the specified stream.  
                    using (IXlDocument document = exporter.CreateDocument(stream))
                    {
                        // Add a new worksheet to the document.  
                        using (IXlSheet sheet = document.CreateSheet())
                        {
                            // Specify the worksheet name. 
                            sheet.Name = "Facturas-Rastreo";

                            using (IXlColumn column = sheet.CreateColumn())
                            {
                                column.WidthInPixels = 250;
                            }

                            // Create the first column and set its width.  
                            using (IXlColumn column = sheet.CreateColumn())
                            {
                                column.WidthInPixels = 250;
                            }

                            // Create the second column and set its width. 
                            using (IXlColumn column = sheet.CreateColumn())
                            {
                                column.WidthInPixels = 250;
                            }

                            // Create the third column and set the specific number format for its cells. 
                            using (IXlColumn column = sheet.CreateColumn())
                            {
                                column.WidthInPixels = 250;
                            }


                            using (IXlColumn column = sheet.CreateColumn()) //cantidad
                            {
                                column.WidthInPixels = 250;
                            }

                            using (IXlColumn column = sheet.CreateColumn()) //codigo
                            {
                                column.WidthInPixels = 250;
                            }

                            using (IXlColumn column = sheet.CreateColumn()) //id contacto
                            {
                                column.WidthInPixels = 250;
                            }

                            using (IXlColumn column = sheet.CreateColumn()) //Nombrecontacto
                            {
                                column.WidthInPixels = 250;
                            }

                            using (IXlColumn column = sheet.CreateColumn()) //Telefono
                            {
                                column.WidthInPixels = 250;
                            }

                            using (IXlColumn column = sheet.CreateColumn()) // Email_contacto
                            {
                                column.WidthInPixels = 250;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) // direccion
                            {
                                column.WidthInPixels = 250;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) // Latitud
                            {
                                column.WidthInPixels = 250;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) // Longitud
                            {
                                column.WidthInPixels = 250;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) // Fecha min entrega
                            {
                                column.WidthInPixels = 250;
                            }

                            // Specify cell font attributes. 
                            XlCellFormatting cellFormatting = new XlCellFormatting();
                            cellFormatting.Font = new XlFont();
                            cellFormatting.Font.Name = "Century Gothic";
                            cellFormatting.Font.SchemeStyle = XlFontSchemeStyles.None;

                            // Specify formatting settings for the header row. 
                            XlCellFormatting headerRowFormatting = new XlCellFormatting();
                            headerRowFormatting.CopyFrom(cellFormatting);
                            headerRowFormatting.Font.Bold = true;
                            headerRowFormatting.Fill = XlFill.SolidFill(XlColor.FromTheme(XlThemeColor.Accent2, 0.0));


                            // Create the header row. 
                            using (IXlRow row = sheet.CreateRow())
                            {
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Cliente";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Usuario Fac.";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Factura";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Fecha Fac.";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }

                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Usuario Cancelación";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Fecha Cancelación";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Relacionó a Almacen";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Documento";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Fecha Relación a Almacen";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Confirmó a Almacen";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Documento";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }

                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Fecha Confirmada a Almacen";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Embarcó";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Documento";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Fecha Embarque";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }

                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Entregó";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Documento";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }

                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Fecha Entrega";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }

                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Regreso a Almacen";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }


                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Documento";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }

                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Fecha Regreso Almacen";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }

                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Relación a Cobranza";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }

                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Documento";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }

                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Fecha Relación Cobranza";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }

                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Confirmada en Cobranza";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }

                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Documento";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }

                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Fecha Confirmada Cobranza";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }

                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Enviado a Revisión";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }

                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Documento";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }

                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Fecha Envio a Revisión";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }

                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Confirmó Revisión";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Documento";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }

                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Fecha Confirmación Revisión";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }

                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Envió a Cobro";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Documento";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }

                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Fecha Enviada a Cobro";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }

                                //using (IXlCell cell = row.CreateCell())
                                //{
                                //    cell.Value = "Embarcó (Factura/Remisión)";
                                //    cell.ApplyFormatting(headerRowFormatting);
                                //}
                                //using (IXlCell cell = row.CreateCell())
                                //{
                                //    cell.Value = "Documento";
                                //    cell.ApplyFormatting(headerRowFormatting);
                                //}

                                //using (IXlCell cell = row.CreateCell())
                                //{
                                //    cell.Value = "Fecha Embarque";
                                //    cell.ApplyFormatting(headerRowFormatting);
                                //}



                            }

                            // Generate data for the sales report.  
                            for (int i = 0; i < List.Count; i++)
                            {
                                using (IXlRow row = sheet.CreateRow())
                                {
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Cliente;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].UsuarioFactura;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].DocFactura;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].FechaFactura;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].UsuarioCancela;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].FechaCancela;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].UsuarioRelacionAlmacen;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].DocRelacionAlmacen;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].FechaRelacionAlmacen;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].UsuarioConfirmadaAlmacen;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].DocConfirmadaAlmacen;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].FechaConfirmadaAlmacen;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    //using (IXlCell cell = row.CreateCell())
                                    //{
                                    //    cell.Value = List[i].UsuarioEmbarque;
                                    //    cell.ApplyFormatting(cellFormatting);
                                    //}

                                    //using (IXlCell cell = row.CreateCell())
                                    //{
                                    //    // Apply the custom number format to the date value. 
                                    //    // Display days as Sunday–Saturday, months as January–December, days as 1–31 and years as 1900–9999. 
                                    //    cell.Value = List[i].DocEmbarque; 
                                    //    cell.ApplyFormatting(cellFormatting);
                                    //}

                                    //using (IXlCell cell = row.CreateCell())
                                    //{
                                    //    cell.Value = List[i].FechaEmbarque;
                                    //    cell.ApplyFormatting(cellFormatting);
                                    //}

                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].UsuarioEmbarqueV2;
                                        cell.ApplyFormatting(cellFormatting);
                                    }

                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].DocEmbarqueV2;
                                        cell.ApplyFormatting(cellFormatting);
                                    }

                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].FechaEmbarqueV2;
                                        cell.ApplyFormatting(cellFormatting);
                                    }

                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].UsuarioEntregada;
                                        cell.ApplyFormatting(cellFormatting);
                                    }

                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].DocEntregada;
                                        cell.ApplyFormatting(cellFormatting);
                                    }

                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].FechaEntregada;
                                        cell.ApplyFormatting(cellFormatting);
                                    }


                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].UsuarioRegresoAlmacen;
                                        cell.ApplyFormatting(cellFormatting);
                                    }

                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].DocRegresoAlmacen;
                                        cell.ApplyFormatting(cellFormatting);
                                    }

                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].FechaRegresoAlmacen;
                                        cell.ApplyFormatting(cellFormatting);
                                    }

                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].UsuarioRelacionCobranza;
                                        cell.ApplyFormatting(cellFormatting);
                                    }

                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].DocRelacionCobranza;
                                        cell.ApplyFormatting(cellFormatting);
                                    }

                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].FechaRelacionCobranza;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    //
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].UsuarioConfirmadaCobranza;
                                        cell.ApplyFormatting(cellFormatting);
                                    }

                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].DocConfirmadaCobranza;
                                        cell.ApplyFormatting(cellFormatting);
                                    }

                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].FechaConfirmadaCobranza;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    //
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].UsuarioEnviadaRevision;
                                        cell.ApplyFormatting(cellFormatting);
                                    }

                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].DocEnviadaRevision;
                                        cell.ApplyFormatting(cellFormatting);
                                    }

                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].FechaEnviadaRevision;
                                        cell.ApplyFormatting(cellFormatting);
                                    }

                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].UsuarioConfirmadaRevision;
                                        cell.ApplyFormatting(cellFormatting);
                                    }

                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].DocConfirmadaRevision;
                                        cell.ApplyFormatting(cellFormatting);
                                    }

                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].FechaConfirmadaRevision;
                                        cell.ApplyFormatting(cellFormatting);
                                    }

                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].UsuarioEnviadaCobro;
                                        cell.ApplyFormatting(cellFormatting);
                                    }

                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].DocEnviadaCobro;
                                        cell.ApplyFormatting(cellFormatting);
                                    }


                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].FechaEnviadaCobro;
                                        cell.ApplyFormatting(cellFormatting);
                                    }

                                }
                            }

                            // Enable AutoFilter for the created cell range. 
                            sheet.AutoFilterRange = sheet.DataRange;
                            // Specify formatting settings for the total row. 
                            XlCellFormatting totalRowFormatting = new XlCellFormatting();
                            totalRowFormatting.CopyFrom(cellFormatting);
                            totalRowFormatting.Font.Bold = true;
                            totalRowFormatting.Fill = XlFill.SolidFill(XlColor.FromTheme(XlThemeColor.Accent5, 0.6));


                        }
                    }
                }
                #endregion excel

                Console.WriteLine(mappath);
                // Open the XLSX document using the default application. 
                ////System.Diagnostics.Process.Start(mappath);

                if (File.Exists(mappath))
                {
                    string ruta2 = null;
                    ruta2 = Server.MapPath("Reportes") + "\\ReporteRutasEmbarque.xlsx";
                    if (File.Exists(ruta2))
                    {
                        File.Delete(ruta2);
                    }
                    File.Move(mappath, Server.MapPath("Reportes") + "\\ReporteRutasEmbarque.xlsx");
                    Response.Redirect("Reportes\\ReporteRutasEmbarque.xlsx", false);
                }



            }
            catch (Exception ex)
            {
                ErrorManager(ex.Message);
            }
        }

        protected void txtCliente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCliente.Value.ToString().Trim() == "")
                {
                    txtClienteNombre.Text = "";
                    txtCliente.Text = "";
                    return;
                }
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Clientes cliente = new Clientes();
                cliente.Id_Emp = sesion.Id_Emp;
                cliente.Id_Cd = sesion.Id_Cd_Ver;
                cliente.Id_Cte = Convert.ToInt32(txtCliente.Value);
                cliente.Id_Rik = sesion.Id_Rik;
                try
                {
                    new CN_CatCliente().ConsultaClientes(ref cliente, sesion.Emp_Cnx);
                    txtClienteNombre.Text = cliente.Cte_NomComercial;

                    //CargarComboTerritorios();
                }
                catch (Exception ex)
                {
                    AlertaFocus(ex.Message, txtCliente.ClientID);
                    txtClienteNombre.Text = "";
                    txtCliente.Text = "";

                    return;
                }

                //CargarComboTerritorios();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void AlertaFocus(string mensaje, string rtb)
        {
            try
            {
                RAM1.ResponseScripts.Add("AlertaFocus('" + mensaje + "','" + rtb + "');");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
    }
}