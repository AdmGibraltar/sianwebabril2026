using CapaEntidad;
using CapaNegocios;
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

using SIANWEB.Utilerias;
using ClosedXML.Excel;

namespace SIANWEB
{
    public partial class Rep_FacturacionCDC : System.Web.UI.Page
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
                        //  hlink.Text = "";
                        //  hlink.NavigateUrl = "";
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

        protected void RAM1_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
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

        protected void RadToolBar1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            if (txtFecha1.SelectedDate == null)
            {
                Alerta("Debe seleccionar una fecha de inicio.");
                return;
            }
            
            if (txtFecha2.SelectedDate == null)
            {
                Alerta("Debe seleccionar una fecha de final.");
                return;
            }


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
                    case "excel":
                        mensajeError = "Impresion_error";
                        ReporteCDC();
                        break;
                }
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, mensajeError);
                this.DisplayMensajeAlerta(mensaje);
            }
        }

        protected void CmbCentro_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
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

        private void Inicializar()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            this.CargarCentros();
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


        private void ReporteCDC()
        {
            //  try
            //  {
            int VGEmpresa = 0;
            Int32.TryParse(strEmp, out VGEmpresa);
            hlink.Text = "";
            hlink.NavigateUrl = "";

            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            CN_ReporteFacturacionCDC cN_Reporte = new CN_ReporteFacturacionCDC();

            ArrayList ALValorParametrosInternos = new ArrayList();

            string dbcobranza = (new SqlConnectionStringBuilder(Emp_CnxCob)).InitialCatalog;

            //NOTA: El estatus de impresión, lo pone cuando el reporte se carga
            /// if (!_PermisoImprimir) Alerta("No tiene permiso para imprimir");

            List<ResultReporteFacturacionCDC> listdo = new List<ResultReporteFacturacionCDC>();

            DateTime dtfecha1 = Convert.ToDateTime(txtFecha1.SelectedDate); //  .ToString("dd/MM/yyyy");
            DateTime dtfecha2 = Convert.ToDateTime(txtFecha2.SelectedDate); //  .ToString("dd/MM/yyyy");

            int rrRes = cN_Reporte.ReporteFacturacionCDC(sesion.Id_Emp, sesion.Id_Cd_Ver, dtfecha1, dtfecha2, sesion.Emp_Cnx, ref listdo);

            if (rrRes == 0)
            {
                ExportaExcel exportador = new ExportaExcel();

                int cont = 6;
                using (var workboook = new XLWorkbook())
                {
                    var HojaXcel = workboook.Worksheets.Add("Reporte");

                    HojaXcel.Range("A1").SetValue("Fecha Inicial:");
                    HojaXcel.Range("B1").SetValue(Convert.ToDateTime(txtFecha1.SelectedDate).ToString("dd/MM/yyyy"));
                    HojaXcel.Range("B1:K1").Merge();
                    HojaXcel.Range("A2").SetValue("Fecha Final:");
                    HojaXcel.Range("B2").SetValue(Convert.ToDateTime(txtFecha2.SelectedDate).ToString("dd/MM/yyyy"));
                    HojaXcel.Range("B2:K2").Merge();
                    HojaXcel.Range("A3").SetValue("Sucursal:");
                    HojaXcel.Range("B3").SetValue(sesion.Cd_Nombre);
                    HojaXcel.Range("B3:K3").Merge();

                    ///     Id_Emp Id_Cd   Id_Cte Cte_NomComercial    Id_Fac 
                    ///     ImporteConIVA   DiasPago FecFactura  FecVenceFactura 
                    ///     Fac_Pagado  Fac_EstatusStr Fac_PedNum  Pag_Importe OrdenCompra

                    //exportador.CeldaHeaderAgrupador("A5:B5", "Cliente", ref HojaXcel);
                    exportador.CeldaHeader("A5", "Cliente", ref HojaXcel);
                    exportador.CeldaHeader("B5", "Nombre Comercial", ref HojaXcel);
                    exportador.CeldaHeader("C5", "Factura", ref HojaXcel);
                    exportador.CeldaHeader("D5", "Territorio", ref HojaXcel);
                    exportador.CeldaHeader("E5", "Id Rik", ref HojaXcel);
                    exportador.CeldaHeader("F5", "Nombre Rik", ref HojaXcel);
                    exportador.CeldaHeader("G5", "Importe Factura", ref HojaXcel);
                    exportador.CeldaHeader("H5", "Fecha Factura", ref HojaXcel);
                    exportador.CeldaHeader("I5", "Fecha Vencimiento", ref HojaXcel);
                    exportador.CeldaHeader("J5", "Orden de Compra", ref HojaXcel);
                    exportador.CeldaHeader("K5", "Saldo Pagado", ref HojaXcel);
                    exportador.CeldaHeader("L5", "Saldo Pendiente", ref HojaXcel);
                    exportador.CeldaHeader("M5", "Estatus Factura", ref HojaXcel);
                    exportador.CeldaHeader("N5", "Método de Pago Factura", ref HojaXcel);
                    exportador.CeldaHeader("O5", "Forma de Pago Factura", ref HojaXcel);
                    exportador.CeldaHeader("P5", "Días de Crédito", ref HojaXcel);
                    exportador.CeldaHeader("Q5", "Analista de Cobranza", ref HojaXcel);
                    exportador.CeldaHeader("R5", "Grupo", ref HojaXcel);

                    //Seccion 2
                    exportador.CeldaHeader("S5", "Días de Revisión", ref HojaXcel);
                    exportador.CeldaHeader("T5", "Horarios de Revisión", ref HojaXcel);
                    exportador.CeldaHeader("U5", "Documentos Adicionales", ref HojaXcel);
                    exportador.CeldaHeader("V5", "Campo Otro", ref HojaXcel);
                    exportador.CeldaHeader("W5", "Días de Pago", ref HojaXcel);
                    exportador.CeldaHeader("X5", "Horarios de Pago", ref HojaXcel);
                    exportador.CeldaHeader("Y5", "Contra Pago/Visita Gestor", ref HojaXcel);

                    //Sección 3
                    exportador.CeldaHeader("Z5", "Días de Recepción", ref HojaXcel);
                    exportador.CeldaHeader("AA5", "Horarios de Entrega", ref HojaXcel);
                    exportador.CeldaHeader("AB5", "Persona que Recibe", ref HojaXcel);
                    exportador.CeldaHeader("AC5", "Cita para la Entrega", ref HojaXcel);
                    exportador.CeldaHeader("AD5", "Área de Recibo", ref HojaXcel);
                    exportador.CeldaHeader("AE5", "Estacionamiento", ref HojaXcel);
                    exportador.CeldaHeader("AF5", "Documento de Entrega", ref HojaXcel);
                    exportador.CeldaHeader("AG5", "Documento de Recepción", ref HojaXcel);




                    foreach (ResultReporteFacturacionCDC itemm in listdo)
                    {
                        exportador.CeldaDatos("A" + cont.ToString(), itemm.Id_Cte.ToString(), XLAlignmentHorizontalValues.Left, ref HojaXcel);
                        exportador.CeldaDatos("B" + cont.ToString(), itemm.Cte_NomComercial, XLAlignmentHorizontalValues.Left, ref HojaXcel);

                        exportador.CeldaDatoInt("C" + cont.ToString(), itemm.Id_Fac, XLAlignmentHorizontalValues.Right, false, false, ref HojaXcel);
                        exportador.CeldaDatoInt("D" + cont.ToString(), itemm.Id_Ter, XLAlignmentHorizontalValues.Right, false, false, ref HojaXcel);
                        exportador.CeldaDatoInt("E" + cont.ToString(), itemm.Id_Rik, XLAlignmentHorizontalValues.Right, false, false, ref HojaXcel);
                        exportador.CeldaDatos("F" + cont.ToString(), itemm.Rik_Nombre.ToString(), XLAlignmentHorizontalValues.Left, ref HojaXcel);

                        exportador.CeldaDatosFloat("G" + cont.ToString(), (float)itemm.ImporteConIVA, XLAlignmentHorizontalValues.Right, false, false, ref HojaXcel);
                        exportador.CeldaDatos("H" + cont.ToString(), itemm.FecFactura.ToString("dd/MM/yyyy"), XLAlignmentHorizontalValues.Center, ref HojaXcel);
                        exportador.CeldaDatos("I" + cont.ToString(), itemm.FecVenceFactura.ToString("dd/MM/yyyy"), XLAlignmentHorizontalValues.Center, ref HojaXcel);
                        exportador.CeldaDatos("J" + cont.ToString(), itemm.OrdenCompra, XLAlignmentHorizontalValues.Right, ref HojaXcel);

                        exportador.CeldaDatosFloat("K" + cont.ToString(), (float)itemm.Fac_Pagado, XLAlignmentHorizontalValues.Right, false, false, ref HojaXcel);
                        exportador.CeldaDatosFloat("L" + cont.ToString(), (float)itemm.Pag_Importe, XLAlignmentHorizontalValues.Right, false, false, ref HojaXcel);
                        exportador.CeldaDatos("M" + cont.ToString(), itemm.Fac_Estatus.ToString(), XLAlignmentHorizontalValues.Left, ref HojaXcel);
                        exportador.CeldaDatos("N" + cont.ToString(), itemm.FPago.ToString(), XLAlignmentHorizontalValues.Left, ref HojaXcel);
                        exportador.CeldaDatos("O" + cont.ToString(), itemm.PagoMetodo.ToString(), XLAlignmentHorizontalValues.Left, ref HojaXcel);
                        exportador.CeldaDatoInt("P" + cont.ToString(), itemm.DiasPago, XLAlignmentHorizontalValues.Right, false, false, ref HojaXcel);
                        exportador.CeldaDatos("Q" + cont.ToString(), itemm.Grupo.ToString(), XLAlignmentHorizontalValues.Left,  ref HojaXcel);
                        exportador.CeldaDatos("R" + cont.ToString(), itemm.Analista.ToString(), XLAlignmentHorizontalValues.Left, ref HojaXcel);
                        
                        //Sección 2                        
                        exportador.CeldaDatos("S" + cont.ToString(), itemm.DiasRevision.ToString(), XLAlignmentHorizontalValues.Left, ref HojaXcel);
                        exportador.CeldaDatos("T" + cont.ToString(), itemm.HorarioRevision.ToString(), XLAlignmentHorizontalValues.Left, ref HojaXcel);
                        exportador.CeldaDatos("U" + cont.ToString(), itemm.DocAdicionales.ToString(), XLAlignmentHorizontalValues.Left, ref HojaXcel);
                        exportador.CeldaDatos("V" + cont.ToString(), itemm.CampoOtro.ToString(), XLAlignmentHorizontalValues.Left,  ref HojaXcel);
                        exportador.CeldaDatos("W" + cont.ToString(), itemm.DiasPagos.ToString(), XLAlignmentHorizontalValues.Left, ref HojaXcel);
                        exportador.CeldaDatos("X" + cont.ToString(), itemm.HorarioPago.ToString(), XLAlignmentHorizontalValues.Left, ref HojaXcel);
                        exportador.CeldaDatos("Y" + cont.ToString(), itemm.ContraEntrega.ToString(), XLAlignmentHorizontalValues.Left, ref HojaXcel);

                        //Sección 3
                        exportador.CeldaDatos("Z" + cont.ToString(), itemm.DiasRecepcion.ToString(), XLAlignmentHorizontalValues.Left, ref HojaXcel);
                        exportador.CeldaDatos("AA" + cont.ToString(), itemm.HorarioEntrega.ToString(), XLAlignmentHorizontalValues.Left, ref HojaXcel);
                        exportador.CeldaDatos("AB" + cont.ToString(), itemm.PersonaRecibe.ToString(), XLAlignmentHorizontalValues.Left, ref HojaXcel);
                        exportador.CeldaDatos("AC" + cont.ToString(), itemm.CitaEntrega.ToString(), XLAlignmentHorizontalValues.Left, ref HojaXcel);
                        exportador.CeldaDatos("AD" + cont.ToString(), itemm.AreaRecibo.ToString(), XLAlignmentHorizontalValues.Left, ref HojaXcel);
                        exportador.CeldaDatos("AE" + cont.ToString(), itemm.Estacionamiento.ToString(), XLAlignmentHorizontalValues.Left, ref HojaXcel);
                        exportador.CeldaDatos("AF" + cont.ToString(), itemm.DocEntrega.ToString(), XLAlignmentHorizontalValues.Left, ref HojaXcel);
                        exportador.CeldaDatos("AG" + cont.ToString(), itemm.DocRecepcion.ToString(), XLAlignmentHorizontalValues.Left, ref HojaXcel);



                        cont = cont + 1;
                    }

                    foreach (var item in HojaXcel.ColumnsUsed())
                    {
                        item.AdjustToContents();
                    }



                    string archivooo = "ReporteFacturacionCDC_" + DateTime.Now.ToShortDateString().Replace("/", "") + "h" + DateTime.Now.ToShortTimeString().Replace(".", "").Replace(":", "").Replace(" ", "") + ".xlsx";




                    //  if (exportador.ServicioGrabaArchivo(workboook, Server.MapPath("Reportes/") + archivooo))
                    //  {
                        //try
                        //{
                        //    exportador.ServicioBajaArchivo(Server.MapPath("Reportes/"), archivooo);
                        //}
                        //catch (Exception ex)
                        //{
                        //    hlink.Text = archivooo;
                        //    hlink.NavigateUrl = Server.MapPath("Reportes/") + archivooo;
                        //    throw ex;
                        //}

                    //  }
                    _ = exportador.ServicioGrabaArchivo(workboook, Server.MapPath("Reportes/") + archivooo);
                    hlink.Text = archivooo;
                    hlink.NavigateUrl = "Reportes/" + archivooo;    /// Server.MapPath("Reportes/") + archivooo;

                    //  exportador.ServicioExportaExcelClosedXML(workboook, Response, Server.MapPath("Reportes/") + archivooo, archivooo);
                    /*
                    HttpResponse httpResponse = Response;
                    httpResponse.Clear();
                    httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    httpResponse.AddHeader("Content-Disposition", "attachment; filename=" + Server.MapPath("Reportes/") + archivooo);

                    // Flush the workbook to the Response.OutputStream
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        workboook.SaveAs(memoryStream);
                        memoryStream.WriteTo(httpResponse.OutputStream);
                        memoryStream.Close();
                    }
                    httpResponse.End();
                    */
                    ///     Server.MapPath("Reportes")
                    ///     
                }
            }



            /*
        }
        catch (Exception ex)
        {
            throw ex;
        }
        */
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



    }
}