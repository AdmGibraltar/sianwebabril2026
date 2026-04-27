using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Text;
using System.Net;
using System.Collections;
using Telerik.Web.UI.GridExcelBuilder;
using System.IO;
using System.Diagnostics;
using System.Configuration;
using System.Xml;
using System.Text.RegularExpressions;

namespace SIANWEB
{
    public partial class CapNotaCredito_Lista : System.Web.UI.Page
    {
        #region Variables

        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        public List<NotaCredito> listNotaCredito;

        #endregion
        #region Propiedades
        public int PermisoGuardar { get { return _PermisoGuardar == true ? 1 : 0; } }
        public int PermisoModificar { get { return _PermisoModificar == true ? 1 : 0; } }
        public int PermisoEliminar { get { return _PermisoEliminar == true ? 1 : 0; } }
        public int PermisoImprimir { get { return _PermisoImprimir == true ? 1 : 0; } }
        #endregion
        #region Eventos
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
                    }
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }
        }
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    case "RebindGrid":
                        rgNotaCredito.Rebind();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }
        protected void cmbCentrosDist_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN__Comun comun = new CN__Comun();

                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                if (sesion.CalendarioIni >= txtFecha1.MinDate && sesion.CalendarioIni <= txtFecha1.MaxDate)
                {
                    txtFecha1.DbSelectedDate = sesion.CalendarioIni;
                }
                if (sesion.CalendarioFin >= txtFecha2.MinDate && sesion.CalendarioFin <= txtFecha2.MaxDate)
                {
                    txtFecha2.DbSelectedDate = sesion.CalendarioFin;
                }
                Session["Sesion" + Session.SessionID] = sesion;
                rgNotaCredito.Rebind();
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Cmb_CentroDistribucion_IndexChanging_error"));
            }
        }
        protected void rgNotaCredito_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                { //Llenar Grid
                    rgNotaCredito.DataSource = this.GetList();
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgNotaCredito_NeedDataSource"));
            }
        }
        protected void rgNotaCredito_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgNotaCredito.Rebind();
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "radGrid_PageIndexChanged"));
            }
        }
        protected void rgNotaCredito_ItemCommand(object source, GridCommandEventArgs e)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            string mensajeError = string.Empty;
            try
            {
                ErrorManager();
                Int32 item = default(Int32);
                if (e.Item == null) return;
                item = e.Item.ItemIndex;
                if (item >= 0)
                {
                    int Id_Emp = Convert.ToInt32(rgNotaCredito.Items[item]["Id_Emp"].Text);
                    int Id_Cd = Convert.ToInt32(rgNotaCredito.Items[item]["Id_Cd"].Text);
                    int Id_Ncr = Convert.ToInt32(rgNotaCredito.Items[item]["Id_Ncr"].Text);
                    int Id_Tm = Convert.ToInt32(rgNotaCredito.Items[item]["Id_Tm"].Text);
                    string estatus = rgNotaCredito.Items[item]["Ncr_EstatusStr"].Text;
                    string tipo = rgNotaCredito.Items[item]["Ncr_TipoStr"].Text;
                    string Id_NcrSerie = rgNotaCredito.Items[item]["Id_NcrSerie"].Text;
                    bool tienePDF = Convert.ToBoolean(rgNotaCredito.Items[item]["PDF"].Text);
                    bool tieneXML = Convert.ToBoolean(rgNotaCredito.Items[item]["NcrXML"].Text);
                    string[] datePart = rgNotaCredito.Items[item]["Ncr_Fecha"].Text.Split(new char[] { '/' });
                    DateTime fechaNotaCredito = new DateTime(Convert.ToInt32(datePart[2]), Convert.ToInt32(datePart[1]), Convert.ToInt32(datePart[0]));
                    string strMsjError = string.Empty;
                    string strScript = string.Empty;

                    switch (e.CommandName.ToString())
                    {
                        case "Eliminar":
                            mensajeError = "CapNotaCredito_delete_error";
                            if (estatus.Contains("Baja"))
                                this.DisplayMensajeAlerta("CapNotaCredito_EsBaja");
                            else
                            {

                                if ((estatus.Contains("Impreso") || estatus.Contains("Capturado")))
                                {
                                    if (Id_Tm == 5)
                                        this.DisplayMensajeAlerta("CapNotaCredito_Movimiento5");
                                    else
                                    {
                                        if (Id_Tm == 4 && tipo == "Factura")
                                            this.DisplayMensajeAlerta("CapNotaCredito_Movimiento4");
                                        else
                                        {
                                            if (_PermisoEliminar)
                                            {
                                                this.CancelarNotaCredito(Id_Emp, Id_Cd, Id_Ncr, Id_NcrSerie, ref strMsjError, ref strScript);
                                                if (!string.IsNullOrEmpty(strMsjError))
                                                {
                                                    this.Alerta(strMsjError);
                                                }
                                                else
                                                {
                                                    this.DisplayMensajeAlerta("CapNotaCredito_delete_ok");
                                                }

                                                if (!string.IsNullOrEmpty(strScript))
                                                {
                                                    RAM1.ResponseScripts.Add(strScript);
                                                }

                                                this.rgNotaCredito.Rebind();
                                            }
                                            else
                                                this.DisplayMensajeAlerta("PermisoEliminarDenegado");
                                        }
                                    }
                                }
                                else
                                    this.DisplayMensajeAlerta("CapNotaCredito_EstatusIncorrecto");
                            }
                            break;
                        case "Modificar":
                            string notaModificable = "1";
                            if (estatus.Contains("Capturado") && (fechaNotaCredito >= sesion.CalendarioIni && fechaNotaCredito <= sesion.CalendarioFin))
                                notaModificable = "1";
                            else
                                notaModificable = "0";

                            if (Id_Tm == 5 || (Id_Tm == 4 && tipo == "Factura"))
                                RAM1.ResponseScripts.Add("OpenAlert('Movimiento generado en forma automática. Imposible modificar este documento','" + Id_Emp + "','" + Id_Cd + "','" + Id_Ncr + "','" + Id_NcrSerie + "','0')");
                            else
                            {
                                if (_PermisoModificar)
                                    RAM1.ResponseScripts.Add(string.Concat(@"AbrirVentana_NotaCredito_Edicion('", Id_Emp, "','", Id_Cd, "','", Id_Ncr, "','", Id_NcrSerie, "','", notaModificable, "')"));
                                else
                                    RAM1.ResponseScripts.Add("OpenAlert('Operación denegada, no tiene permisos para modificar notas de crédito','" + Id_Emp + "','" + Id_Cd + "','" + Id_Ncr + "'," + Id_NcrSerie + "','0')");
                            }
                            break;
                        case "Imprimir":
                            mensajeError = "CapNotaCredito_print_error";
                            if ((estatus.Contains("Impreso") || estatus.Contains("Capturado")))
                            {
                                if (_PermisoImprimir)
                                {

                                    int Verificador = 0;
                                    ValidarEstatusFactura(Id_Emp, Id_Cd, Id_Ncr, Id_NcrSerie, ref Verificador);

                                    if (Verificador == -1)
                                    {
                                        this.ImprimirNotaCredito(Id_Emp, Id_Cd, Id_Ncr, Id_NcrSerie, ref strMsjError, ref strScript, "NOTA DE CREDITO", "", tienePDF);

                                        if (!string.IsNullOrEmpty(strMsjError))
                                        {
                                            Alerta(strMsjError);
                                        }

                                        if (!string.IsNullOrEmpty(strScript))
                                        {
                                            RAM1.ResponseScripts.Add(strScript);
                                        }
                                    }
                                    else if (Verificador == -2)
                                    {
                                        Alerta("No se puede imprimir la nota de crédito, ya que la factura de la que se género se encuentra en estatus <b>BAJA</b>");

                                    }
                                    else if (Verificador == -3)
                                    {
                                        Alerta("No se puede imprimir la nota de crédito, ya que la factura de la que se género se encuentra en estatus <b>CAPTURADO</b> </br></br>  Imprima la factura para poder imprimir la nota de crédito");
                                    }

                                }
                                else
                                {
                                    this.DisplayMensajeAlerta("PermisoImprimirDenegado");
                                }
                            }
                            else
                                this.DisplayMensajeAlerta("CapNotaCredito_EstatusIncorrecto");
                            break;
                        case "PDF":
                            if (tienePDF)
                                descargarPDF(Id_Ncr, Id_NcrSerie);
                            else
                                Alerta("Esta nota de crédito aún no cuenta con un archivo PDF");
                            break;
                        case "XML":
                            if (tieneXML)
                                descargarXML(Id_Ncr, Id_NcrSerie);
                            else
                                Alerta("Esta nota de crédito aún no cuenta con un archivo XML");
                            break;
                    }
                }
                //para los botones de exportar
                if (e.CommandName.ToString().ToUpper().Contains("EXPORTTO"))
                {
                    rgNotaCredito.MasterTableView.Columns.FindByUniqueName("Editar").Visible = false;
                    rgNotaCredito.MasterTableView.Columns.FindByUniqueName("Eliminar").Visible = false;
                    rgNotaCredito.MasterTableView.Columns.FindByUniqueName("Imprimir").Visible = false;
                    rgNotaCredito.MasterTableView.Columns.FindByUniqueName("PDF").Visible = false;
                    rgNotaCredito.MasterTableView.Columns.FindByUniqueName("NcrXML").Visible = false;

                    if (e.CommandName.ToString().ToUpper().Contains("PDF"))
                    {
                        rgNotaCredito.MasterTableView.Columns.FindByUniqueName("Cte_NomComercial").HeaderStyle.Width = Unit.Pixel(200);
                        rgNotaCredito.MasterTableView.Columns.FindByUniqueName("Ncr_Fecha").HeaderStyle.Width = Unit.Pixel(70);
                    }
                }
                if (e.CommandName.ToString().ToUpper().Contains("SORT"))
                {
                    ErrorManager();
                    this.rgNotaCredito.Rebind();
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, mensajeError));
            }
        }

        private void descargarXML(int Id_Ncr, string Id_NcrSerie)
        {
            NotaCredito notaCredito = new NotaCredito();
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            notaCredito.Id_Emp = Sesion.Id_Emp;
            notaCredito.Id_Cd = Sesion.Id_Cd_Ver;
            notaCredito.Id_Ncr = Id_Ncr;
            notaCredito.Id_NcrSerie = Id_NcrSerie;
            CN_CapNotaCredito notaCredito2 = new CN_CapNotaCredito();
            notaCredito2.ArchivoPdf_Xml(ref notaCredito, Sesion.Emp_Cnx);
            string ruta = null;
            string rutaCN = null;
            System.IO.StreamWriter sw = null;
            ruta = Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "Ncr" + Id_Ncr.ToString() + ".txt";
            rutaCN = Server.MapPath("Reportes") + "\\archivoXmlCN" + Sesion.Id_U.ToString() + "Ncr" + Id_Ncr.ToString() + ".txt";

            if (File.Exists(ruta))
                File.Delete(ruta);
            if (File.Exists(Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "Ncr" + Id_Ncr.ToString() + ".xml"))
                File.Delete(Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "Ncr" + Id_Ncr.ToString() + ".xml");
            sw = new System.IO.StreamWriter(ruta, false, Encoding.UTF8);
            sw.WriteLine(notaCredito.Ncr_Xml.ToString());
            sw.Close();
            File.Move(ruta, Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "Ncr" + Id_Ncr.ToString() + ".xml");


            if ((notaCredito.Ncr_XmlCN != null) && (notaCredito.Ncr_XmlCN != string.Empty))
            {
                if (File.Exists(rutaCN))
                {
                    File.Delete(rutaCN);
                }

                if (File.Exists(Server.MapPath("Reportes") + "\\archivoXmlCN" + Sesion.Id_U.ToString() + "Ncr" + Id_Ncr.ToString() + ".xml"))
                {
                    File.Delete(Server.MapPath("Reportes") + "\\archivoXmlCN" + Sesion.Id_U.ToString() + "Ncr" + Id_Ncr.ToString() + ".xml");
                }

                sw = new System.IO.StreamWriter(rutaCN, false, Encoding.UTF8);
                sw.WriteLine(notaCredito.Ncr_XmlCN.ToString());
                sw.Close();

                File.Move(rutaCN, Server.MapPath("Reportes") + "\\archivoXmlCN" + Sesion.Id_U.ToString() + "Ncr" + Id_Ncr.ToString() + ".xml");

                RAM1.ResponseScripts.Add(string.Concat(@"abrirArchivoCN('Reportes\\archivoXml" + Sesion.Id_U.ToString() + "Ncr", Id_Ncr.ToString(), ".xml'", ",", @"'Reportes\\archivoXmlCN" + Sesion.Id_U.ToString() + "Ncr", Id_Ncr.ToString(), ".xml')"));
            }
            else
            {
                RAM1.ResponseScripts.Add(string.Concat(@"abrirArchivo('Reportes\\archivoXml" + Sesion.Id_U.ToString() + "Ncr", Id_Ncr.ToString(), ".xml')"));
            }
        }

        private void ConsultarPDF(int Id_Ncr, string Id_NcrSerie, ref string strScript, ref string strMsjError)
        {
            try
            {
                // ------------------------------
                // Abrir PDF de Nota de Credito
                // ------------------------------
                object resultado = null;
                object resultadoCN = null;
                NotaCredito notaCredito = new NotaCredito();
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                notaCredito.Id_Emp = Sesion.Id_Emp;
                notaCredito.Id_Cd = Sesion.Id_Cd_Ver;
                notaCredito.Id_Ncr = Id_Ncr;
                notaCredito.Id_NcrSerie = Id_NcrSerie;
                CN_CapNotaCredito nota = new CN_CapNotaCredito();
                nota.ConsultarNotaCreditoSAT(ref notaCredito, Sesion.Emp_Cnx, ref resultado, ref resultadoCN);
                byte[] archivoPdf = (byte[])resultado;
                byte[] archivoPdfCN = resultadoCN != System.DBNull.Value ? (byte[])resultadoCN : new byte[0];
                if (archivoPdf.Length > 0)
                {
                    string tempPDFname = string.Concat("NOTACREDITO_"
                            , Sesion.Id_Emp.ToString()
                            , "_", Sesion.Id_Cd.ToString()
                            , "_", Id_Ncr.ToString()
                            , ".pdf");
                    string URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                    string WebURLtempPDF = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFname);
                    this.ByteToTempPDF(URLtempPDF, archivoPdf);
                    // ------------------------------------------------------------------------------------------------
                    // Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                    // ------------------------------------------------------------------------------------------------


                    if (archivoPdfCN.Length > 0)
                    {
                        string tempPDFCNname = string.Concat("NOTACREDITOCN_", Sesion.Id_Emp.ToString(), "_", Sesion.Id_Cd.ToString(), "_", Id_Ncr.ToString(), ".pdf");
                        string URLtempPDFCN = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFCNname));
                        string WebURLtempPDFCN = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFCNname);


                        this.ByteToTempPDF(URLtempPDFCN, archivoPdfCN);

                        strScript = string.Concat(@"AbrirNotaCreditoPDFCN('" + WebURLtempPDF + "','" + WebURLtempPDFCN + "')");
                    }
                    else
                    {
                        // ------------------------------------------------------------------------------------------------
                        // Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                        // ------------------------------------------------------------------------------------------------
                        strScript = string.Concat(@"AbrirNotaCreditoPDF('", WebURLtempPDF, "','')");
                    }
                }
            }
            catch (Exception ex)
            {
                strMsjError = ex.Message;
            }
        }

        private void descargarPDF(int Id_Ncr, string Id_NcrSerie)
        {
            try
            {
                // ------------------------------
                // Abrir PDF de Nota de Cargo
                // ------------------------------
                object resultado = null;
                object resultadoCN = null;
                NotaCredito notaCredito = new NotaCredito();
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                notaCredito.Id_Emp = Sesion.Id_Emp;
                notaCredito.Id_Cd = Sesion.Id_Cd_Ver;
                notaCredito.Id_Ncr = Id_Ncr;
                notaCredito.Id_NcrSerie = Id_NcrSerie;
                CN_CapNotaCredito nota = new CN_CapNotaCredito();
                nota.ConsultarNotaCreditoSAT(ref notaCredito, Sesion.Emp_Cnx, ref resultado, ref resultadoCN);
                byte[] archivoPdf = (byte[])resultado;
                byte[] archivoPdfCN = resultadoCN != System.DBNull.Value ? (byte[])resultadoCN : new byte[0];
                if (archivoPdf.Length > 0)
                {
                    string tempPDFname = string.Concat("NOTACREDITO_"
                            , Sesion.Id_Emp.ToString()
                            , "_", Sesion.Id_Cd.ToString()
                            , "_", Id_Ncr.ToString()
                            , ".pdf");
                    string URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                    string WebURLtempPDF = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFname);
                    this.ByteToTempPDF(URLtempPDF, archivoPdf);
                    // ------------------------------------------------------------------------------------------------
                    // Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                    // ------------------------------------------------------------------------------------------------


                    if (archivoPdfCN.Length > 0)
                    {
                        string tempPDFCNname = string.Concat("NOTACREDITOCN_", Sesion.Id_Emp.ToString(), "_", Sesion.Id_Cd.ToString(), "_", Id_Ncr.ToString(), ".pdf");
                        string URLtempPDFCN = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFCNname));
                        string WebURLtempPDFCN = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFCNname);


                        this.ByteToTempPDF(URLtempPDFCN, archivoPdfCN);

                        RAM1.ResponseScripts.Add(string.Concat(@"AbrirNotaCreditoPDFCN('" + WebURLtempPDF + "','" + WebURLtempPDFCN + "')"));
                    }
                    else
                    {
                        // ------------------------------------------------------------------------------------------------
                        // Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                        // ------------------------------------------------------------------------------------------------
                        RAM1.ResponseScripts.Add(string.Concat(@"AbrirNotaCreditoPDF('", WebURLtempPDF, "','')"));
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(ex.Message);
            }
        }
        protected void rgNotaCredito_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                GridItem cmdItem = rgNotaCredito.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                cmdItem.FindControl("AddNewRecordButton").Parent.Visible = false;
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(ex.Message);
            }
        }
        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                rgNotaCredito.Rebind();
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "btnBuscar_error"));
            }
        }
        //protected void ImgExportar_Click(object sender, ImageClickEventArgs e)
        //{
        //    try
        //    {

        //        //rgFactura.ExportSettings.Excel.Format = (GridExcelExportFormat)Enum.Parse(typeof(GridExcelExportFormat),"ExcelML");
        //        //rgFactura.ExportSettings.IgnorePaging = true;
        //        //rgFactura.ExportSettings.ExportOnlyData = true;
        //        //rgFactura.ExportSettings.OpenInNewWindow = true;
        //        //rgFactura.ExportSettings.FileName = "Listado facturas";
        //        //rgFactura.MasterTableView.ExportToExcel();
        //        GenerarExcel();
        //    }
        //    catch (Exception ex)
        //    {

        //        ErrorManager(ex, "ImgExportar_Click");
        //    }

        //}
        #endregion
        #region Funciones
        private List<NotaCredito> GetList()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                listNotaCredito = new List<NotaCredito>();
                NotaCredito notaCredito = new NotaCredito();
                notaCredito.Id_Emp = sesion.Id_Emp;
                notaCredito.Id_Cd = sesion.Id_Cd_Ver;

                int? objectInt = null;
                DateTime? objectDateTime = null;

                new CN_CapNotaCredito().ConsultaNotaCredito_Buscar(notaCredito, ref listNotaCredito, sesion.Emp_Cnx
                    , txtNombreCliente.Text
                    , this.txtCliente1.Text == string.Empty ? objectInt : Convert.ToInt32(this.txtCliente1.Text)
                    , this.txtCliente2.Text == string.Empty ? objectInt : Convert.ToInt32(this.txtCliente2.Text)
                    , this.txtFecha1.SelectedDate == null ? objectDateTime : Convert.ToDateTime(this.txtFecha1.SelectedDate)
                    , this.txtFecha2.SelectedDate == null ? objectDateTime : Convert.ToDateTime(this.txtFecha2.SelectedDate)
                    , this.cmbEstatus.SelectedValue == "-1" ? string.Empty : this.cmbEstatus.SelectedValue
                    , this.txtNotaCredito1.Text == string.Empty ? objectInt : Convert.ToInt32(this.txtNotaCredito1.Text)
                    , this.txtNotaCredito2.Text == string.Empty ? objectInt : Convert.ToInt32(this.txtNotaCredito2.Text)
                    , sesion.Propia ? sesion.Id_U : objectInt);
                return listNotaCredito;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CancelarNotaCredito(int Id_Emp, int Id_Cd, int Id_Ncr, string Id_NcrSerie, ref string strMsjError, ref string strScript)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            NotaCredito notaCredito = new NotaCredito();
            List<NotaCreditoDet> listaNotaCreditoDet = new List<NotaCreditoDet>();
            CN_CapNotaCredito cn_notaCredito = new CN_CapNotaCredito();
            notaCredito.Id_Emp = Id_Emp;
            notaCredito.Id_Cd = Id_Cd;
            notaCredito.Id_Ncr = Id_Ncr;
            notaCredito.Id_NcrSerie = Id_NcrSerie;
            cn_notaCredito.ConsultarNotaCredito(ref notaCredito, sesion.Emp_Cnx, ref listaNotaCreditoDet);
            notaCredito.ListaNotaCredito = listaNotaCreditoDet;

            if (!(notaCredito.Ncr_Fecha.Date >= sesion.CalendarioIni.Date && notaCredito.Ncr_Fecha.Date <= sesion.CalendarioFin.Date))
            {
                //guardar bitacora cancelacion
                strScript = string.Concat(@"AbrirVentana_CancelarNotaCreditoVencido('", Id_Emp, "','", Id_Cd, "','", Id_Ncr, "','", Id_NcrSerie, "','", notaCredito.Ncr_Estatus, "')");
                return;
            }

            int TSATCANCELACION = 1;
            string RFC = string.Empty;
            string UUID = string.Empty;
            if (notaCredito.Ncr_Xml != null)
            {
                XmlDocument xmlBD = new XmlDocument();
                xmlBD.LoadXml(notaCredito.Ncr_Xml.ToString());
                foreach (XmlNode nodo in xmlBD.ChildNodes)
                {
                    if (nodo.Name == "Comprobante")
                    {
                        TSATCANCELACION = 1;
                    }
                    else if (nodo.Name == "cfdi:Comprobante")
                    {
                        TSATCANCELACION = 2;
                        foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                        {
                            if (Nodo_nivel2.Name == "cfdi:Complemento")
                            {
                                XmlNode Nodo_nivel3;
                                Nodo_nivel3 = Nodo_nivel2.FirstChild;
                                UUID = Nodo_nivel3.Attributes["UUID"].Value;
                            }
                            if (Nodo_nivel2.Name == "cfdi:Emisor")
                            {
                                RFC = Nodo_nivel2.Attributes["Rfc"].Value;
                            }
                        }
                    }
                }
            }
            if (TSATCANCELACION == 2)
            {
                string valorResultadoCancelacion = "0";
                WS_CFDICancelacion.Service1 ws = new WS_CFDICancelacion.Service1();
                ws.Url = ConfigurationManager.AppSettings["WS_CFDICancelacion"].ToString();
                String respuestaCancelacion = ws.CancelacionWS("" + RFC + "," + UUID + "");
                XmlDocument XmlCancelacion = new XmlDocument();
                XmlCancelacion.LoadXml(respuestaCancelacion);
                foreach (XmlNode nodo in XmlCancelacion.ChildNodes)
                {
                    if (nodo.Name == "Acuse")
                    {
                        foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                        {
                            if (Nodo_nivel2.Name == "Folios")
                            {
                                foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                {
                                    if (Nodo_nivel3.Name == "EstatusUUID")
                                    {
                                        valorResultadoCancelacion = Nodo_nivel3.InnerText;
                                    }
                                }
                            }
                        }
                    }
                }
                string valorResultadoCancelacionTexto = string.Empty;
                switch (valorResultadoCancelacion)
                {
                    case "202":
                        valorResultadoCancelacionTexto = "Documento Previamente Cancelado";
                        break;
                    case "203":
                        valorResultadoCancelacionTexto = "Documento No corresponda al emisor";
                        break;
                    case "204":
                        valorResultadoCancelacionTexto = "Documento No Aplicable para cancelación";
                        break;
                    case "205":
                        valorResultadoCancelacionTexto = "Documento No Existe emisión";
                        break;
                    default:
                        valorResultadoCancelacionTexto = "No se hizo conexión con el servicio de cancelación";
                        break;
                }
                if (valorResultadoCancelacion != "201")
                {
                    strMsjError = valorResultadoCancelacionTexto;
                    return;
                }
            }



            int verificador = 0;
            notaCredito.Id_Emp = Id_Emp;
            notaCredito.Id_Cd = Id_Cd;
            notaCredito.Id_Ncr = Id_Ncr;
            notaCredito.Id_NcrSerie = Id_NcrSerie;
            new CN_CapNotaCredito().EliminarNotaCredito(ref notaCredito, sesion.Emp_Cnx, verificador);

            ImprimirNotaCredito(Id_Emp, Id_Cd, Id_Ncr, Id_NcrSerie, ref strMsjError, ref strScript, "CANCELACION", string.Concat("Canc. NC-", Id_Ncr.ToString()), false);

        }

        public void ImprimirNotaCreditoAutomatica(int Id_Emp, int Id_Cd, int Id_Ncr, ref string strMsjError, ref string strScript)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_CapNotaCredito cn_notaCredito = new CN_CapNotaCredito();
            NotaCredito notaCreditoRef = new NotaCredito();
            notaCreditoRef.Id_Emp = Id_Emp;
            notaCreditoRef.Id_Cd = Id_Cd;
            notaCreditoRef.Id_Ncr = Id_Ncr;
            // consulta para obtener el Id_NcrSerie
            cn_notaCredito.ConsultarNotaCreditoOptenerNcrSerie(ref notaCreditoRef, sesion.Emp_Cnx);

            ImprimirNotaCredito(notaCreditoRef.Id_Emp, notaCreditoRef.Id_Cd, notaCreditoRef.Id_Ncr, notaCreditoRef.Id_NcrSerie, ref strMsjError, ref strScript, "NOTA DE CREDITO", "", false);
        }

        private void ImprimirNotaCredito(int Id_Emp, int Id_Cd, int Id_Ncr, string Id_NcrSerie, ref string strMsjError, ref string strScript, string movimiento, string agregado_nota_cancelacion, bool tienePDF = false)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                int verificador = 0;
                // --------------------------------------------------------------------
                // Consulta detalle de factura para generar lista de productos
                // --------------------------------------------------------------------
                List<NotaCreditoDet> listaNotaCreditoDet = new List<NotaCreditoDet>();
                CN_CapNotaCredito cn_notaCredito = new CN_CapNotaCredito();
                NotaCredito notaCredito = new NotaCredito();
                notaCredito.Id_Emp = Id_Emp;
                notaCredito.Id_Cd = Id_Cd;
                notaCredito.Id_Ncr = Id_Ncr;
                notaCredito.Id_NcrSerie = Id_NcrSerie;

                NotaCredito NotaCreditoNacional = new NotaCredito();
                NotaCreditoNacional.Id_Emp = Id_Emp;
                NotaCreditoNacional.Id_Cd = Id_Cd;
                NotaCreditoNacional.Id_Ncr = Id_Ncr;

                cn_notaCredito.ConsultarNotaCredito(ref notaCredito, sesion.Emp_Cnx, ref listaNotaCreditoDet);
                cn_notaCredito.ConsultaNotaCreditoNacional(ref NotaCreditoNacional, sesion.Emp_Cnx);
                bool esNcrNacional = NotaCreditoNacional != null ? true : false;
                // Validar si la Remisión es Válida o no en base a la suma de los montos en las partidas de la remisión y la remisión especial.
                bool bDocumentoValido = false;
                new CN_CapNotaCredito().ValidaMontosImpresion(notaCredito, sesion.Id_Cd_Ver, sesion.Id_Emp, 3, sesion.Emp_Cnx, ref bDocumentoValido);

                if (bDocumentoValido)
                {

                    List<AdendaDet> listCabT = new List<AdendaDet>();
                    List<AdendaDet> listDetT = new List<AdendaDet>();
                    List<AdendaDet> listCabNacionalT = new List<AdendaDet>();
                    List<AdendaDet> listDetNacionalT = new List<AdendaDet>();
                    new CN_CapNotaCredito().ConsultarAdenda(Id_Emp, Id_Cd, Id_Ncr, Id_NcrSerie, "5", "6", ref listCabT, ref listDetT, sesion.Emp_Cnx);
                    new CN_CapFactura().ConsultarAdendaNacional(Id_Emp, Id_Cd, Id_Ncr, "1", "2", ref listCabNacionalT, ref listDetNacionalT, sesion.Emp_Cnx);

                    // -------------------------------------------------------------------------------------------
                    // Consulta productos de nota especial de la tabla 'CapFacturaEspecialDet' si esque la nota especial existe
                    // esto es si es una actualización de nota --> si el parametro Folio trae un Id de nota
                    // -------------------------------------------------------------------------------------------
                    List<NotaCreditoDet> listaProdNotaEspecialFinal = new List<NotaCreditoDet>();
                    new CN_CapNotaCredito().ConsultaNotaCreditoEspecialDetalle(ref listaProdNotaEspecialFinal, sesion.Emp_Cnx, Id_Emp
                        , Id_Cd
                        , Id_Ncr
                        , Id_NcrSerie
                        , (int)notaCredito.Id_Cte);
                    // -------------------------------------------------------------------------------------------

                    #region variable XML a enviar
                    StringBuilder XML_Enviar = new StringBuilder();
                    XML_Enviar.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                    XML_Enviar.Append("<Comprobante");
                    XML_Enviar.Append(" serie=\"\"");
                    XML_Enviar.Append(" folio=\"\"");
                    XML_Enviar.Append(" fecha=\"\"");
                    XML_Enviar.Append(" formaDePago=\"\"");
                    XML_Enviar.Append(" subTotal=\"\"");
                    XML_Enviar.Append(" total=\"\"");
                    XML_Enviar.Append(" tipoDeComprobante=\"\"");
                    XML_Enviar.Append(" tipoMovimiento=\"\""); //FACTURA,NOTA DE CREDITO, NOTA DE CEDITO ,CANCELACION FACTURA,CANCELACION
                    XML_Enviar.Append(" tipoMoneda=\"\""); //MN= MONEDA NACIONAL, MA = MONEDA AMERICANA depende del catalogo del SIAN
                    XML_Enviar.Append(" tipoCambio=\"\""); //IMPORTE VIGENTE DEL CAMBIO DEPENDIENDO DEL TIPO DE MONEDA
                    XML_Enviar.Append(" leyendaFacturaEspecial=\"\""); //LEYENDA DE FACTURA ESPECIAL: LOS DATOS DEL DETALLE REAL DE LA FACTURA PERO DELIMITADOS POR /
                    XML_Enviar.Append(" movimientoacancelar=\"\""); //SI ES CANCELACION FACTURA HAY QUE INDICAR QUE FACTURA ESTA CANCELANDO APLICA LO MISMO PARA LA NOTA DE CREDITO
                    XML_Enviar.Append(" CliNum=\"\"");
                    XML_Enviar.Append(" MetodoPago=\"\"");
                    XML_Enviar.Append(" Notas=\"\"");
                    XML_Enviar.Append(" Motivo=\"\"");
                    XML_Enviar.Append(" Referencia=\"\"");
                    XML_Enviar.Append(" TipoReferencia=\"\"");
                    XML_Enviar.Append(" Correo=\"\"");
                    XML_Enviar.Append(" ComprobanteVersion=\"\"");
                    XML_Enviar.Append(">");
                    XML_Enviar.Append("<CFDIRelacionados");
                    XML_Enviar.Append(" TipoRelacion=\"\">");
                    XML_Enviar.Append("<CFDIRelacionado");
                    XML_Enviar.Append(" UUID=\"\" />");
                    XML_Enviar.Append("</CFDIRelacionados>");
                    XML_Enviar.Append(" <Emisor");
                    XML_Enviar.Append(" rfc=\"\"");
                    XML_Enviar.Append(" numero=\"\" />");
                    XML_Enviar.Append(" <Receptor");
                    XML_Enviar.Append(" rfc=\"\"");
                    XML_Enviar.Append(" nombre=\"\"");
                    XML_Enviar.Append(" UsoCFDI=\"\"");
                    XML_Enviar.Append(" Regimen=\"\">");
                    XML_Enviar.Append(" <Domicilio");
                    XML_Enviar.Append(" calle=\"\"");
                    XML_Enviar.Append(" noExterior=\"\"");
                    XML_Enviar.Append(" colonia=\"\"");
                    XML_Enviar.Append(" municipio=\"\"");
                    XML_Enviar.Append(" estado=\"\"");
                    XML_Enviar.Append(" pais=\"\"");
                    XML_Enviar.Append(" codigoPostal=\"\" />");
                    XML_Enviar.Append(" </Receptor>");
                    XML_Enviar.Append(" <Conceptos>");
                    XML_Enviar.Append(" <Concepto");
                    XML_Enviar.Append(" ClaveProdServ=\"\"");
                    XML_Enviar.Append(" ClaveUnidad=\"\"");
                    XML_Enviar.Append(" cantidad=\"\"");
                    XML_Enviar.Append(" noIdentificacion=\"\"");
                    XML_Enviar.Append(" descripcion=\"\"");
                    XML_Enviar.Append(" valorUnitario=\"\"");
                    XML_Enviar.Append(" importe=\"\" />");
                    XML_Enviar.Append(" </Conceptos>");

                    XML_Enviar.Append(" <Impuestos");
                    XML_Enviar.Append(" totalImpuestosTrasladados=\"\">");
                    XML_Enviar.Append(" <Traslados>");
                    XML_Enviar.Append(" <Traslado");
                    XML_Enviar.Append(" impuesto=\"\"");
                    XML_Enviar.Append(" tasa=\"\"");
                    XML_Enviar.Append(" importe=\"\" />");
                    XML_Enviar.Append(" </Traslados>");
                    XML_Enviar.Append(" </Impuestos>");

                    XML_Enviar.Append(" <Addenda>");
                    //ADENDA CABECERA
                    XML_Enviar.Append(" <cabecera");
                    XML_Enviar.Append(" Pedido=\"\"");
                    XML_Enviar.Append(" Requisicion=\"\"");
                    XML_Enviar.Append(" consignarRenglon1=\"\"");
                    XML_Enviar.Append(" consignarRenglon2=\"\"");
                    XML_Enviar.Append(" consignarRenglon3=\"\"");
                    XML_Enviar.Append(" consignarRenglon4=\"\"");
                    XML_Enviar.Append(" consignarRenglon5=\"\"");
                    XML_Enviar.Append(" Conducto=\"\"");
                    XML_Enviar.Append(" CondicionesPago=\"\"");
                    XML_Enviar.Append(" NumeroGuia=\"\"");
                    XML_Enviar.Append(" ControlPedido=\"\"");
                    XML_Enviar.Append(" OrdenEmbarque=\"\"");
                    XML_Enviar.Append(" Zona=\"\"");
                    XML_Enviar.Append(" Territorio=\"\"");
                    XML_Enviar.Append(" Agente=\"\"");
                    XML_Enviar.Append(" NumeroDocumentoAduanero=\"\"");
                    XML_Enviar.Append(" Formulo=\"\"");
                    XML_Enviar.Append(" Autorizo=\"\"");
                    XML_Enviar.Append(" NombreAddenda=\"\"");
                    foreach (AdendaDet det in listCabT)
                    {
                        XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                    }
                    XML_Enviar.Append("/>");

                    //ADENDA DETALLE
                    if (listaProdNotaEspecialFinal.Count > 0)
                    {
                        foreach (NotaCreditoDet notaCreditoDet in listaProdNotaEspecialFinal)
                        {
                            XML_Enviar.Append(" <Detalle");
                            XML_Enviar.Append(" noProducto=\"" + notaCreditoDet.Producto.Id_PrdEsp + "\"");
                            XML_Enviar.Append(" UnidadMedida=\"" + notaCreditoDet.Producto.Prd_Presentacion.Trim() + " " + notaCreditoDet.Producto.Prd_UniNs + "\"");
                            foreach (AdendaDet det in listDetT)
                            {
                                if (notaCreditoDet.Id_Prd == det.Id_Prd)
                                {
                                    XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                                }
                            }
                            XML_Enviar.Append("/>");
                        }
                    }
                    else
                    {
                        if (notaCredito.Ncr_Tipo == 1 && notaCredito.Id_Tm == 4)
                        {
                            XML_Enviar.Append(" <Detalle");
                            XML_Enviar.Append(" noProducto=\"\"");
                            XML_Enviar.Append(" UnidadMedida=\"\"");
                            XML_Enviar.Append("/>");
                        }
                        else
                        {
                            if (notaCredito.ListaNotaCredito.Count > 0)
                            {
                                foreach (NotaCreditoDet notaCreditoDet in notaCredito.ListaNotaCredito)
                                {
                                    XML_Enviar.Append(" <Detalle");
                                    XML_Enviar.Append(" noProducto=\"" + notaCreditoDet.Id_Prd + "\"");
                                    XML_Enviar.Append(" UnidadMedida=\"" + notaCreditoDet.Prd_Presentacion.Trim() + " " + notaCreditoDet.Prd_UniNs + "\"");
                                    foreach (AdendaDet det in listDetT)
                                    {
                                        if (notaCreditoDet.Id_Prd == det.Id_Prd)
                                        {
                                            XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                                        }
                                    }
                                    XML_Enviar.Append("/>");
                                }
                            }
                            else
                            {
                                XML_Enviar.Append(" <Detalle");
                                XML_Enviar.Append(" noProducto=\"\"");
                                XML_Enviar.Append(" UnidadMedida=\"\"");
                                XML_Enviar.Append("/>");
                            }
                        }
                    }
                    XML_Enviar.Append(" </Addenda>");
                    if (NotaCreditoNacional != null)
                    {
                        if (movimiento != "CANCELACION")
                        {
                            //COMPROBANTE NACIONAL
                            XML_Enviar.Append(" <ComprobanteCN");
                            XML_Enviar.Append(" CliNum=\"\"");
                            XML_Enviar.Append(">");
                            XML_Enviar.Append(" <Conceptos>");
                            XML_Enviar.Append(" <Concepto");
                            XML_Enviar.Append(" ClaveProdServ=\"\"");
                            XML_Enviar.Append(" ClaveUnidad=\"\"");
                            XML_Enviar.Append(" cantidad=\"\"");
                            XML_Enviar.Append(" noIdentificacion=\"\"");
                            XML_Enviar.Append(" descripcion=\"\"");
                            XML_Enviar.Append(" valorUnitario=\"\"");
                            XML_Enviar.Append(" importe=\"\" />");
                            XML_Enviar.Append(" </Conceptos>");

                            //ADENDA NACIONAL
                            XML_Enviar.Append(" <AddendaCN>");

                            //ADENDA NACIONAL CABECERA
                            XML_Enviar.Append(" <CabeceraCN");
                            XML_Enviar.Append(" Pedido=\"\"");
                            XML_Enviar.Append(" Requisicion=\"\"");
                            XML_Enviar.Append(" consignarRenglon1=\"\"");
                            XML_Enviar.Append(" consignarRenglon2=\"\"");
                            XML_Enviar.Append(" consignarRenglon3=\"\"");
                            XML_Enviar.Append(" consignarRenglon4=\"\"");
                            XML_Enviar.Append(" consignarRenglon5=\"\"");
                            XML_Enviar.Append(" Conducto=\"\"");
                            XML_Enviar.Append(" CondicionesPago=\"\"");
                            XML_Enviar.Append(" NumeroGuia=\"\"");
                            XML_Enviar.Append(" ControlPedido=\"\"");
                            XML_Enviar.Append(" OrdenEmbarque=\"\"");
                            XML_Enviar.Append(" Zona=\"\"");
                            XML_Enviar.Append(" Territorio=\"\"");
                            XML_Enviar.Append(" Agente=\"\"");
                            XML_Enviar.Append(" NumeroDocumentoAduanero=\"\"");
                            XML_Enviar.Append(" Formulo=\"\"");
                            XML_Enviar.Append(" Autorizo=\"\"");

                            XML_Enviar.Append(" NombreAddenda=\"\"");
                            foreach (AdendaDet det in listCabNacionalT)
                            {
                                XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                            }
                            XML_Enviar.Append("/>");


                            //ADENDA NACIONAL DETALLE

                            //NUEVO METODO PARA IMPRIMIR DETALLES DE ADENDA
                            //NUEVO METODO PARA IMPRIMIR DETALLES DE ADENDA
                            foreach (NotaCreditoDet fd in listaNotaCreditoDet)
                            {
                                XML_Enviar.Append(" <Detalle");
                                XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                                XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                                XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");

                                string primerNodo = "";
                                int primerfila = 0;
                                foreach (AdendaDet det in listDetNacionalT)
                                {

                                    if (fd.Id_Prd == det.Id_Prd)
                                    {
                                        if (primerfila == 0)
                                        { // COPIAMOS EL NOMBRE DEL PRIMER NODO PARA COMPARAR CUANDO INICIE UNA NUEVA ADENDA
                                            primerNodo = det.Nodo;
                                        }
                                        if (primerfila > 0 && det.Nodo == primerNodo)
                                        {
                                            XML_Enviar.Append("/>");//CERRAMOS LA ADENDA
                                                                    // ABRIMOS UNA NUEVA ADENDA
                                            XML_Enviar.Append(" <Detalle");
                                            XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                                            XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                                            XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");
                                        }

                                        XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                                        primerfila++;
                                    }
                                }

                                XML_Enviar.Append("/>");
                            }

                            XML_Enviar.Append(" </AddendaCN>");

                            XML_Enviar.Append(" </ComprobanteCN>");
                        }
                        else
                        {
                            XML_Enviar.Append("<ComprobanteCN UUID=\"" + notaCredito.Ncr_FolioFiscalCN + "\" Folio=\"" + notaCredito.Ncr_FolioCN.ToString() + "\" Serie=\"" + notaCredito.Ncr_ReferenciaSerie.ToString() + "\" />");
                            NotaCreditoNacional = null;
                        }
                    }
                    XML_Enviar.Append(" </Comprobante>");
                    #endregion

                    #region Codigo pruebas

                    //PruebaServicio.Service1 servicio = new PruebaServicio.Service1();
                    //float suma = servicio.Suma(Convert.ToSingle(txtNumero1.Text), Convert.ToSingle(txtNumero2.Text));
                    //this.Alerta(suma.ToString());

                    //Uri objURI = new Uri("");
                    //WebRequest objWebRequest = WebRequest.Create(objURI);
                    //WebResponse objWebResponse = objWebRequest.GetResponse();
                    //Stream objStream = objWebResponse.GetResponseStream();
                    //StreamReader objStreamReader = new StreamReader(objStream);
                    //string responseText = objStreamReader.ReadToEnd();

                    #endregion

                    // --------------------------------------
                    // Consulta centro de distribución
                    // --------------------------------------
                    CentroDistribucion Cd = new CentroDistribucion();
                    new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref Cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);


                    //if (notaCredito.Ncr_Sello != string.Empty)
                    //{ //Abre el XML y carga el PDF de la nota de credito
                    //    object resultado = null;
                    //    cn_notaCredito.ConsultarNotaCreditoSAT(ref notaCredito, sesion.Emp_Cnx, ref resultado);

                    //    byte[] archivoPdf = (byte[])resultado;
                    //    if (archivoPdf.Length > 0)
                    //    {
                    //        string tempPDFname = string.Concat("NOTACREDITO_"
                    //                , notaCredito.Id_Emp.ToString()
                    //                , "_", notaCredito.Id_Cd.ToString()
                    //                , "_", notaCredito.Id_U.ToString()
                    //                , ".pdf");
                    //        string URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                    //        string WebURLtempPDF = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFname);

                    //        this.ByteToTempPDF(URLtempPDF, archivoPdf);
                    //        // ------------------------------------------------------------------------------------------------
                    //        // Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                    //        // ------------------------------------------------------------------------------------------------
                    //        RAM1.ResponseScripts.Add(string.Concat(@"AbrirNotaCreditoPDF('", WebURLtempPDF, "')"));
                    //    }
                    //    else
                    //    {
                    //        this.DisplayMensajeAlerta("TempPDFNoData");
                    //    }
                    //}
                    //else
                    // {
                    // --------------------------------------
                    // cargar xml de factura que se envia a SAT
                    // --------------------------------------
                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(XML_Enviar.ToString());

                    // --------------------------------------//
                    // --------------------------------------//
                    //         LLENAR DATOS DEL XML        --//
                    // --------------------------------------//
                    // --------------------------------------//
                    CN_CatTipoMoneda cn_moneda = new CN_CatTipoMoneda();
                    TipoMoneda tm = new TipoMoneda();
                    tm.Id_Emp = sesion.Id_Emp;
                    tm.Id_Mon = 2;
                    cn_moneda.ConsultaTipoMonedaIndividual(ref tm, sesion.Emp_Cnx);

                    string numeroPedido = string.Empty;
                    string Fac_FolioFiscal = string.Empty;
                    if (notaCredito.Ncr_Movimiento != null)
                    {
                        bool encontrado = false;
                        if (Convert.ToInt32(notaCredito.Ncr_Movimiento) == 1)
                        {
                            Factura factura = new Factura();
                            factura.Id_Emp = Id_Emp;
                            factura.Id_Cd = Id_Cd;
                            factura.Id_Fac = Convert.ToInt32(notaCredito.Ncr_Referencia);
                            new CN_CapFactura().ConsultaFacturaEncabezado(ref factura, sesion.Emp_Cnx, ref encontrado);
                            if (encontrado)
                            {
                                numeroPedido = factura.Fac_PedNum == null ? string.Empty : factura.Fac_PedNum.ToString();
                                Fac_FolioFiscal = factura.Fac_FolioFiscal == null ? string.Empty : factura.Fac_FolioFiscal.ToString();
                            }
                        }
                        else
                        {
                            NotaCargo notacargo = new NotaCargo();
                            notacargo.Id_Emp = Id_Emp;
                            notacargo.Id_Cd = Id_Cd;
                            notacargo.Id_Nca = Convert.ToInt32(notaCredito.Ncr_Referencia);
                            notacargo.Id_NcaSerie = notaCredito.Ncr_ReferenciaSerie.Trim() + notaCredito.Ncr_Referencia.ToString().Trim();
                            new CN_CapNotaCargo().ConsultaNotaCargo(ref notacargo, sesion.Emp_Cnx);
                            Fac_FolioFiscal = notacargo.Nca_FolioFiscal == null ? string.Empty : notacargo.Nca_FolioFiscal.ToString();
                        }
                    }
                    #region Llenar datos Nota de credito a Enviar

                    //consultar datos del cliente de la nota de credito
                    Clientes cliente = new Clientes();
                    cliente.Id_Emp = sesion.Id_Emp;
                    cliente.Id_Cd = sesion.Id_Cd_Ver;
                    cliente.Id_Cte = Convert.ToInt32(notaCredito.Id_Cte);
                    new CN_CatCliente().ConsultaClientes(ref cliente, sesion.Emp_Cnx);

                    //encabezado
                    XmlNode Comprobante = xml.SelectSingleNode("Comprobante");
                    Comprobante.Attributes["serie"].Value = notaCredito.Id_NcrSerie.Replace(notaCredito.Id_Ncr.ToString(), ""); // "PRUEBA";
                    //Comprobante.Attributes["serie"].Value = "PRUEBA"; 
                    Comprobante.Attributes["folio"].Value = notaCredito.Id_Ncr.ToString();
                    Comprobante.Attributes["fecha"].Value = string.Format("{0:yyyy-MM-ddTHH:mm:ss}", notaCredito.Ncr_FechaHr);
                    Comprobante.Attributes["formaDePago"].Value = notaCredito.Ncr_MetodoPago.ToString(); //"PAGO EN UNA SOLA EXHIBICION"; <-- segun el documento no lleva
                    Comprobante.Attributes["subTotal"].Value = notaCredito.Ncr_Subtotal == null ? "0" : notaCredito.Ncr_Subtotal.ToString();
                    Comprobante.Attributes["total"].Value = (Convert.ToDouble(notaCredito.Ncr_Subtotal) + Convert.ToDouble(notaCredito.Ncr_Iva)).ToString();
                    Comprobante.Attributes["tipoDeComprobante"].Value = "egreso";
                    Comprobante.Attributes["tipoMovimiento"].Value = movimiento;
                    Comprobante.Attributes["tipoMoneda"].Value = tm.Mon_Abrev;
                    Comprobante.Attributes["tipoCambio"].Value = tm.Mon_TipCambio.ToString();
                    Comprobante.Attributes["leyendaFacturaEspecial"].Value = ""; //
                    Comprobante.Attributes["movimientoacancelar"].Value = ""; //
                    Comprobante.Attributes["CliNum"].Value = notaCredito.Id_Cte.ToString();
                    Comprobante.Attributes["MetodoPago"].Value = "00".Substring(1, 2 - notaCredito.Ncr_FormaPago.Trim().Length) + notaCredito.Ncr_FormaPago.Trim();

                    Comprobante.Attributes["Motivo"].Value = notaCredito.Tm_Nombre;
                    Comprobante.Attributes["Referencia"].Value = notaCredito.Ncr_Referencia.ToString();
                    Comprobante.Attributes["TipoReferencia"].Value = notaCredito.Ncr_Movimiento == 1 ? "Factura" : "Nota de cargo";
                    Comprobante.Attributes["ComprobanteVersion"].Value = cliente.Cte_VersionCFDI.ToString();

                    StringBuilder NotaCompleta = new StringBuilder();

                    NotaCompleta.Append(notaCredito.Ncr_Notas + "//");
                    NotaCompleta.Append(agregado_nota_cancelacion);
                    Comprobante.Attributes["Notas"].Value = NotaCompleta.ToString();

                    //Comprobante.Attributes["ComprobanteVersion"].Value = "3.3";
                    //consultar datos relacionados
                    XmlNode Relacionados = Comprobante.SelectSingleNode("CFDIRelacionados");
                    Relacionados.Attributes["TipoRelacion"].Value = notaCredito.Id_Tm == 5 ? "03" : notaCredito.Ncr_TipoRelacion;

                    XmlNode Relacionado = Relacionados.SelectSingleNode("CFDIRelacionado");
                    Relacionado.Attributes["UUID"].Value = Fac_FolioFiscal;
                    ////consultar datos del cliente de la nota de credito
                    //Clientes cliente = new Clientes();
                    //cliente.Id_Emp = sesion.Id_Emp;
                    //cliente.Id_Cd = sesion.Id_Cd_Ver;
                    //cliente.Id_Cte = Convert.ToInt32(notaCredito.Id_Cte);
                    //new CN_CatCliente().ConsultaClientes(ref cliente, sesion.Emp_Cnx);

                    Comprobante.Attributes["Correo"].Value = cliente.Cte_Email;
                    //receptor
                    XmlNode Receptor = Comprobante.SelectSingleNode("Receptor");
                    Receptor.Attributes["rfc"].Value = cliente.Cte_FacRfc;
                    Receptor.Attributes["nombre"].Value = cliente.Cte_NomComercial;
                    Receptor.Attributes["UsoCFDI"].Value = notaCredito.Ncr_UsoCFDI;
                    Receptor.Attributes["Regimen"].Value = cliente.Cte_RegimenFiscal.ToString();

                    XmlNode Emisor = Comprobante.SelectSingleNode("Emisor");
                    Emisor.Attributes["rfc"].Value = Cd.Cd_Rfc;
                    //Emisor.Attributes["numero"].Value = Cd.Cd_Numero;
                    Emisor.Attributes["numero"].Value = Cd.Id_Cd.ToString();
                    //Domicilio
                    XmlNode Domicilio = Receptor.SelectSingleNode("Domicilio");
                    Domicilio.Attributes["calle"].Value = cliente.Cte_FacCalle;
                    Domicilio.Attributes["noExterior"].Value = cliente.Cte_FacNumero;
                    Domicilio.Attributes["colonia"].Value = cliente.Cte_FacColonia;
                    Domicilio.Attributes["municipio"].Value = cliente.Cte_FacMunicipio;
                    Domicilio.Attributes["estado"].Value = cliente.Cte_FacEstado;
                    Domicilio.Attributes["pais"].Value = "México";
                    Domicilio.Attributes["codigoPostal"].Value = cliente.Cte_FacCp;

                    // ---------------------
                    // Conceptos --> partidas = producto
                    // Detalle --> productoDetalle
                    // ---------------------          
                    XmlNode Conceptos = Comprobante.SelectSingleNode("Conceptos");
                    XmlNode producto = Conceptos.SelectSingleNode("Concepto");

                    XmlNode Addenda = Comprobante.SelectSingleNode("Addenda");

                    XmlNode ComprobanteCN = Comprobante.SelectNodes("ComprobanteCN").Count > 0 ? Comprobante.SelectSingleNode("ComprobanteCN") : null;
                    XmlNode AddendaCN = ComprobanteCN != null ? ComprobanteCN.SelectSingleNode("AddendaCN") : null;
                    XmlNode ConceptosCN = ComprobanteCN != null ? ComprobanteCN.SelectSingleNode("Conceptos") : null;
                    XmlNode productoCN = ConceptosCN != null ? ConceptosCN.SelectSingleNode("Concepto") : null;

                    if (NotaCreditoNacional != null)
                    {
                        ComprobanteCN.Attributes["CliNum"].Value = NotaCreditoNacional != null ? NotaCreditoNacional.Id_Cte.ToString() : "0";
                    }


                    //Si existe una nota especial, en los nodos de conceptos del producto se pone
                    //los productos de la nota especial
                    //si no, se pone los datos de productos de la nota original
                    if (listaProdNotaEspecialFinal.Count > 0)
                    {
                        foreach (NotaCreditoDet notaDet in listaProdNotaEspecialFinal)
                        {
                            XmlNode prd = producto.Clone();
                            prd.Attributes["noIdentificacion"].Value = notaDet.Producto.Id_PrdEsp;
                            prd.Attributes["descripcion"].Value = notaDet.Producto.Prd_DescripcionEspecial.Replace("\"", "");
                            prd.Attributes["importe"].Value = notaDet.Ncr_Importe.ToString();
                            prd.Attributes["ClaveProdServ"].Value = "01010101";
                            prd.Attributes["ClaveUnidad"].Value = "H87";
                            producto.ParentNode.AppendChild(prd);
                        }
                    }
                    else
                    {
                        if (notaCredito.Ncr_Tipo == 1 && (notaCredito.Id_Tm == 4 || notaCredito.Id_Tm == 5))
                        {
                            XmlNode prd = producto.Clone();
                            prd.Attributes["noIdentificacion"].Value = "";
                            prd.Attributes["descripcion"].Value = notaCredito.Tm_Nombre.Replace("\"", "");
                            prd.Attributes["importe"].Value = notaCredito.Ncr_Subtotal.ToString();
                            prd.Attributes["ClaveProdServ"].Value = "01010101";
                            prd.Attributes["ClaveUnidad"].Value = "H87";
                            producto.ParentNode.AppendChild(prd);
                        }
                        else
                        {
                            if (notaCredito.ListaNotaCredito.Count > 0)
                            {
                                foreach (NotaCreditoDet notaCreditoDet in notaCredito.ListaNotaCredito)
                                {
                                    XmlNode prd = producto.Clone();
                                    prd.Attributes["noIdentificacion"].Value = notaCreditoDet.Id_Prd.ToString();
                                    prd.Attributes["descripcion"].Value = notaCreditoDet.Prd_Nombre.Replace("\"", "");
                                    prd.Attributes["cantidad"].Value = notaCreditoDet.Ncr_Cant.ToString();
                                    prd.Attributes["valorUnitario"].Value = notaCreditoDet.Ncr_Precio.ToString();
                                    prd.Attributes["importe"].Value = notaCreditoDet.Ncr_Importe.ToString();
                                    prd.Attributes["ClaveProdServ"].Value = notaCreditoDet.Ncr_ClaveProdServ.ToString();
                                    prd.Attributes["ClaveUnidad"].Value = notaCreditoDet.Ncr_ClaveUnidad.ToString();
                                    producto.ParentNode.AppendChild(prd);

                                    if (NotaCreditoNacional != null)
                                    {
                                        XmlNode prdCN = productoCN.Clone();
                                        prdCN.Attributes["noIdentificacion"].Value = notaCreditoDet.Id_Prd.ToString();
                                        prdCN.Attributes["descripcion"].Value = notaCreditoDet.Prd_Nombre.Replace("\"", "");
                                        prdCN.Attributes["cantidad"].Value = notaCreditoDet.Ncr_Cant.ToString();
                                        prdCN.Attributes["valorUnitario"].Value = notaCreditoDet.Ncr_Precio.ToString();
                                        prdCN.Attributes["importe"].Value = notaCreditoDet.Ncr_Importe.ToString();
                                        prdCN.Attributes["ClaveProdServ"].Value = notaCreditoDet.Ncr_ClaveProdServ.ToString();
                                        prdCN.Attributes["ClaveUnidad"].Value = notaCreditoDet.Ncr_ClaveUnidad.ToString();
                                        productoCN.ParentNode.AppendChild(prdCN);
                                    }
                                }
                            }
                            else
                            {
                                XmlNode prd = producto.Clone();
                                prd.Attributes["noIdentificacion"].Value = "";
                                prd.Attributes["descripcion"].Value = notaCredito.Tm_Nombre.Replace("\"", "");
                                prd.Attributes["cantidad"].Value = "1";
                                prd.Attributes["valorUnitario"].Value = notaCredito.Ncr_Subtotal.ToString();
                                prd.Attributes["importe"].Value = notaCredito.Ncr_Subtotal.ToString();
                                prd.Attributes["ClaveProdServ"].Value = "84111506";
                                prd.Attributes["ClaveUnidad"].Value = "ACT";
                                producto.ParentNode.AppendChild(prd);
                            }
                        }
                    }
                    producto.ParentNode.RemoveChild(producto);

                    if (NotaCreditoNacional != null)
                    {
                        productoCN.ParentNode.RemoveChild(xml.SelectNodes("//ComprobanteCN//Conceptos//Concepto").Item(0));
                    }
                    //Impuestos
                    XmlNode Impuestos = Comprobante.SelectSingleNode("Impuestos");
                    Impuestos.Attributes["totalImpuestosTrasladados"].Value = notaCredito.Ncr_Iva == null ? "0" : notaCredito.Ncr_Iva.ToString();

                    //Traslado (impuestos desgloce)
                    XmlNode Traslados = Impuestos.SelectSingleNode("Traslados");
                    XmlNode Traslado = Traslados.SelectSingleNode("Traslado");
                    Traslado.Attributes["impuesto"].Value = "IVA";
                    //Traslado.Attributes["tasa"].Value = Cd.Cd_IvaPedidosFacturacion.ToString();
                    Traslado.Attributes["tasa"].Value = (notaCredito.Ncr_Iva / notaCredito.Ncr_Subtotal * 100).Value.ToString("0.00");
                    Traslado.Attributes["importe"].Value = notaCredito.Ncr_Iva == null ? "0" : notaCredito.Ncr_Iva.ToString();

                    //Addenda
                    XmlNode cabecera = Addenda.SelectSingleNode("cabecera");
                    //si el movimiento de la nota de crédito es 1 (factura), consulta la factura para sacar el pedido
                    //string numeroPedido = string.Empty;                    if (notaCredito.Ncr_Movimiento != null)
                    {
                        if (Convert.ToInt32(notaCredito.Ncr_Movimiento) == 1)
                        {
                            bool encontrado = false;
                            Factura factura = new Factura();
                            factura.Id_Emp = Id_Emp;
                            factura.Id_Cd = Id_Cd;
                            factura.Id_Fac = Convert.ToInt32(notaCredito.Ncr_Referencia);
                            new CN_CapFactura().ConsultaFacturaEncabezado(ref factura, sesion.Emp_Cnx, ref encontrado);
                            if (encontrado)
                            {
                                numeroPedido = factura.Fac_PedNum == null ? string.Empty : factura.Fac_PedNum.ToString();
                            }
                        }
                    }
                    cabecera.Attributes["Pedido"].Value = numeroPedido;

                    //datos de cabecera
                    cabecera.Attributes["Zona"].Value = notaCredito.Id_Cd.ToString(); //Cd.Cd_Descripcion;
                    cabecera.Attributes["Territorio"].Value = notaCredito.Id_Ter.ToString();// notaCredito.Ter_Nombre == null ? string.Empty : notaCredito.Ter_Nombre;
                    cabecera.Attributes["Agente"].Value = notaCredito.Id_Rik == null ? string.Empty : notaCredito.Id_Rik.ToString();
                    cabecera.Attributes["Formulo"].Value = Cd.Cd_CobranzaPersonaFormula;
                    cabecera.Attributes["Autorizo"].Value = Cd.Cd_CobranzaPersonaAutoriza;
                    cabecera.Attributes["Requisicion"].Value = "";
                    cabecera.Attributes["consignarRenglon1"].Value = cliente.Cte_NomComercial;
                    cabecera.Attributes["consignarRenglon2"].Value = string.Concat(cliente.Cte_Calle, " ", cliente.Cte_Numero);
                    cabecera.Attributes["consignarRenglon3"].Value = string.Concat(cliente.Cte_Colonia, " ", cliente.Cte_Municipio);
                    cabecera.Attributes["consignarRenglon4"].Value = cliente.Cte_Estado;
                    cabecera.Attributes["consignarRenglon5"].Value = "México";
                    cabecera.Attributes["Conducto"].Value = "";
                    cabecera.Attributes["CondicionesPago"].Value = "";
                    cabecera.Attributes["NumeroGuia"].Value = "";
                    cabecera.Attributes["ControlPedido"].Value = "";
                    cabecera.Attributes["OrdenEmbarque"].Value = "";
                    cabecera.Attributes["NumeroDocumentoAduanero"].Value = "";
                    if (listCabT.Count > 0 || listDetT.Count > 0)
                    {
                        cabecera.Attributes["NombreAddenda"].Value = cliente.Ade_Nombre;
                    }

                    //Addenda Nacional
                    if (NotaCreditoNacional != null)
                    {
                        XmlNode cabeceraCN = AddendaCN.SelectSingleNode("CabeceraCN");
                        cabeceraCN.Attributes["Zona"].Value = notaCredito.Id_Cd.ToString(); //Cd.Cd_Descripcion;
                        cabeceraCN.Attributes["Territorio"].Value = notaCredito.Id_Ter.ToString();// notaCredito.Ter_Nombre == null ? string.Empty : notaCredito.Ter_Nombre;
                        cabeceraCN.Attributes["Agente"].Value = notaCredito.Id_Rik == null ? string.Empty : notaCredito.Id_Rik.ToString();
                        cabeceraCN.Attributes["Formulo"].Value = Cd.Cd_CobranzaPersonaFormula;
                        cabeceraCN.Attributes["Autorizo"].Value = Cd.Cd_CobranzaPersonaAutoriza;
                        cabeceraCN.Attributes["Requisicion"].Value = "";
                        cabeceraCN.Attributes["consignarRenglon1"].Value = cliente.Cte_NomComercial;
                        cabeceraCN.Attributes["consignarRenglon2"].Value = string.Concat(NotaCreditoNacional.Fac_CteCalle.Replace("\"", ""), " ", NotaCreditoNacional.Fac_CteNumero);
                        cabeceraCN.Attributes["consignarRenglon3"].Value = NotaCreditoNacional.Fac_CteColonia;
                        cabeceraCN.Attributes["consignarRenglon4"].Value = string.Concat(NotaCreditoNacional.Fac_CteMunicipio, " ", NotaCreditoNacional.Fac_CteEstado, " ", NotaCreditoNacional.Fac_CteCp).Replace('É', 'E');
                        cabeceraCN.Attributes["consignarRenglon5"].Value = "México";
                        cabeceraCN.Attributes["Conducto"].Value = "";
                        cabeceraCN.Attributes["CondicionesPago"].Value = "";
                        cabeceraCN.Attributes["NumeroGuia"].Value = "";
                        cabeceraCN.Attributes["ControlPedido"].Value = "";
                        cabeceraCN.Attributes["OrdenEmbarque"].Value = "";
                        cabeceraCN.Attributes["NumeroDocumentoAduanero"].Value = "";
                        cabeceraCN.Attributes["NombreAddenda"].Value = NotaCreditoNacional.Fac_CteAdeNombre;//cliente.Ade_Nombre;                           
                    }

                    #endregion

                    // --------------------------------------
                    // convertir XML a string !!!!!!!!!!!!!!!!!!!!!!!
                    // --------------------------------------
                    StringWriter sw = new StringWriter();
                    XmlTextWriter tx = new XmlTextWriter(sw);
                    xml.WriteTo(tx);
                    string xmlString = sw.ToString();

                    // ------------------------------------------------------
                    // ENVIAR XML al servicio de la aplicacion de KEY
                    // ------------------------------------------------------
                    XmlDocument xmlSAT = new XmlDocument();
                    //sian_cfd.Service1 sianFacturacionElectronica = new sian_cfd.Service1();

                    WebReference.Service1 sianFacturacionElectronica = new WebReference.Service1();
                    sianFacturacionElectronica.Url = ConfigurationManager.AppSettings["WS_CFDIImpresion"].ToString();
                    object sianFacturacionElectronicaResult = sianFacturacionElectronica.ObtieneCFD(xmlString);


                    if (movimiento == "CANCELACION")
                    {
                        string XmLRegex = string.Empty;
                        XmLRegex = Regex.Replace(sianFacturacionElectronicaResult.ToString(), @"(?s)(?<=<cfdi:Addenda>).+?(?=</cfdi:Addenda>)", "");
                        XmLRegex = XmLRegex.Replace("<cfdi:Addenda>", "");
                        XmLRegex = XmLRegex.Replace("</cfdi:Addenda>", "");
                        xmlSAT.LoadXml(XmLRegex);
                    }
                    else
                    {
                        xmlSAT.LoadXml(sianFacturacionElectronicaResult.ToString());
                        //quitaxmlSAT.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><Comprobante><AddendaKey><PDF ArchivoPDF=\"JVBERi0xLjcgCiXi48/TIAoxIDAgb2JqIAo8PCAKL1R5cGUgL0NhdGFsb2cgCi9QYWdlcyAyIDAgUiAKL1BhZ2VNb2RlIC9Vc2VOb25lIAovVmlld2VyUHJlZmVyZW5jZXMgPDwgCi9GaXRXaW5kb3cgdHJ1ZSAKL1BhZ2VMYXlvdXQgL1NpbmdsZVBhZ2UgCi9Ob25GdWxsU2NyZWVuUGFnZU1vZGUgL1VzZU5vbmUgCj4+IAo+PiAKZW5kb2JqIAo1IDAgb2JqIAo8PCAKL0xlbmd0aCA1Mzc2IAovRmlsdGVyIFsgL0ZsYXRlRGVjb2RlIF0gCj4+IApzdHJlYW0KeJzFO8lSG0u2e74iF68jIDClzKrKGhzRi5JUiAJJJaokpvaLG0IIEGiwJQSYuL/Yq/sVvejFi/6Bd3Ko4aCS7dt2dNthJ1l58kx5phxghMJfBv9c3ySjmexSsrzbUT8krR1mGyY5kP9zzzIccuA6olmOd8535jsmOd5h5GWHU2a4HAB9wyM+/OBygwugVGBwYQAaxonl2QaFYY/DRzlsU9ew83HmuQK6GFazTWpCwz0KSA+sHDUF6tQwmUmo4VAP/vdMkjaAq5cdBmwywOsIvmZ5lzpkKmeKvwAqmLdMmhHxATTrWp6hgG0b6AJhkwkcs6JvWoalIDy/NCw7+Ri3uaCcjWZdkxselwCCnim+2IbglPtcqFl1JQLfkTM0e7rLHU8TKFQkhlWPc9+weDYssIMizHxY9eSo1I/kTupJMlN8KOF3mBA6w+A6rtaOws8z8Xyn6ErpHF8s7IFju5pC9sH1BKWShEI1Li9E3KDh2JqG1JDqFsMCoZdp0Ob5hwLCdSTJnEvdV2ss7SPnUvddxzTKGBw7xyB50P0Sj7aj1JQxqfolVZfY9BCPJtg+0lT+wXVzIuAZVgkk67tutlpg1wanwiNcbpHVaIdzG3xSmzNYLvzMXEETXOh2p97fYToKZICwCmA6fUBOxF9AUfOJI9y2f7uzS6JOL0764R7pP2wSCwGdxYSXKnomY4bDt1HMQDcp7hLSDNNGEvUaUdytNdpBGsIX0o5TUo/CbpiSP0gnTBpBt/H3QPICIcSEFQB9Q/zp32gXz7j3Ffe9sBk1Y9JdGHKOCWELLIUK+N0kPB1EadSI/ujmAFUCilgnXASW1XdE5NrUJPVllIOYY76T6zBsHAVCkvBC8CKIbdekir3gSo6InFzirCCn4UwGAQAr8SzsSnkD8h/8A4t1Fn4fbKvYNoXlyuVmnqfyx4bYGVyF3I24m0atbpAQbRuWJ0IJhH+RQTaMQ5u2WhJQV9jdzl0WrWW4NSHN2ZXMZWBg/6zCmbQ5CkaFEcTCoMEoekEr/p7eglbY1e53YIE8QgVSgi1SCRKDRj+WM7jgB/xOxHCYUBsSGWIEHHVrzKyBR3gKUsQV8ChXYt5lzkfmfmS2osuZJ0Q4gCDJ5XhZqHrUqccklVbeOJMTLOnoQiPviFrgKq6WRQcDChoVFDtRJ06DlDDm6sWA5VMYdtOg2w9IJ0iigETddJBInaSEOrZFc+ic+cYgGBz1w07cIM0o7SdRPyaHYTNMgvYH0vnH62S0yCa5wubEHJCCMsao1zzmKlao6qOkEuqZ1ObUtSAUSBAdgXwlAcjpSP8vdBID4bZivBvkfApVSzajQTNokrMIVgsk+9446QedYNCOekG6AVoS6sAyRXQ9MJXSJV/CaqX6lbI8U0itoiCILL/Z0oDUN4s6lIE2lGGIEk/DUqpJ+LKaYZ4KeHiNj4eT2Zh01/N/EduySmupF2cxXcwnQ3IznkLYXT6NNyA6i/nTeLkcfyVdo20QB3RO369bDtz/+GnXY5/2iGdRYIoK0MPiGwcjo569MS05bHw8OR0QxwcFMOJc8E2Q8R0IMieHk9VoOP0IKtkAeXl5MR7HX43RYmbMXuUwhIwvkPp9VR7LEkCkPS8P5aKMLgdzDWo6howb1LBEhir9r/zbk2YI6j0JL8np4O+dqBGQ1FBeZ5wZGfFTFJVEVSPKb1FlA/JdpBnuOr4WKfc0pE6PevQ9gFwNvBj5WL5wH0i3/X4wW/fmu3XPAUp2A05G4qdxlpNBccrGvCzmNJoR6cQQA5IkvFQ2KQMJ/O9A4M8ilA6Jh4vlTBgc+Ty8W3z0fdJbLKF7O5lPlt/Jxqqs4bI+3VLUKLiqmqbxRzNqxdsTn+lLu9ClmiXdtpqILSL4ltJp0I1EmAA7UI4scXraMXc7IsUF38i+rlXI+a1yMQOs4ACqsr7gYXsJxewfqkhFat9WIPaSEBJnJiMvywga6G+v3zIn0EvpfaNAzUG3MJBC2glE+i5cnVNLCGSBFxOfSdlket70dCEdDHNp7zOUvH2ZhcBSO5chWDu1TLfwZwgnptxCCXibQcSHrSLo02Owhd+pTWZ3FKrnhYTkYP7SioArCB2eo2qpLQxlwMx6z1LZ1Q7B187CJAWxCdhoZaBhsP2VIQ6qVoe/x5WnhsFqIdG1dCw9AD1DPWM5RRrJU6gMNN8oGlmxy7GoLPaqzRaE9KqXFFVNsojVGwvjnXF9y7RdKgonxYdjqahfyYgGrHIfQbuXxM00TM6+uw2R8UZsRGTA23AhS3mpKZZ/WxkadVtgyyowHcjDjzyjHQaN/iAJsrJP6gew+aLkvkGLmi8UiOTqckhuWaiuO2SmCqIEdnGwhwTXrcfp6SAkQZjEadw2TUpaiXJbC1RmZdVHu6/qDrGo6guYnTYFSxQ8+qOdf7Wpcg/5WThLVqeIc498vyf0r3GrWJuRS7K6Sdq8+mq7kLU9ZipWVMzwtY5AmkGY9uP0I8l+hDqdwnpFZ1ClDVdDIhRHs60zgZVy7GzTqrZ1YCKq1LI8p1J59bgNJXU9OBYZvpuSbthKAuLTC2bSRqdXA+WftDLtSRNXjIeBLvwNlmsk1x7LSzkClVKuPqsoB8vfsfoY0ajLyguDat2ZzNUu/rO6Y4A54+e98lxKt2jvLOi2DNIZQNncjxoxuIBe+e9ZmldlaYVFIUszodLeoqo/aWem/0t0ZUGgNatV5TkurdRUM4QiSm6nQF3tfjRI45T04vZZTPyT1knU/yETM6tMjEE+1BsAZGKW2H84P2tioDbKf4naHAis1hb39PXIe7W1BoFQWRIfx+IAJBVG1j/wP4EMvSD5tPdDWqsMayJ7VPglh4Se25pfaOJP6MyBgMap/Ut05hl8S0TzfR/mwfJU6q07CAHhcVCPu7AdJyJF1NtBF/YyoLir3EXzraj+xsW5x7fCmbC1qnBW+v7O1oQ0GntZdUeeW6E7sR0Wf3+Nm0KydbZFNEark2mjHSfC1ASGC+Jz2IO1tydP2y1ZWR7QoUgpFGZChbEZ0yyXb8ueoIw/Gda0KD/tn/RbGYBVx7UsA+QhLerGxAYJ0h/UmlOlNai8HXdTa57n5ZEXac3+0yqjvyZr2qbBtugMqprqpNlPgl4YNOOE9M+jFNICCcTJFRgQbRk/5Jq80jVFcWlXRDRuGm51+vxhv5RKc36R0mAR7S15AHRW7ZZ1cVHRCftBW5zH9GqduCd24gzKs0bnJ5TmiDKfZ/Zn51b5gbosG5Cnzea/qzT2a5zThgBrb7M0l1Y7J2zl41YoLK0ddKJuQDq1TiDOZ7cqzHYLJTBemTfFYSzd0Bj74HhWKaT9hMZ+kZmZjm+Y3vb4b34jngGOJGx1pZPWa5A0vR8tbCsrNI5yQ0ln1KHVOvP+WwENlOZsDWisukgL00ZcDwhobtAOEtI7axC7HiSJuPPpDC7JIeSEH3JRqzKuQYy1K8pb23QNXdD/bFz7NbWaeDngbtsWeNvMLY3FbZXcPFGHfNpt1DilNK9s3dwmGseZvlz+TWtzin0UtjaTe1s8VOMuK+yiflGhMG4yzviv8lBx1KI8VB/ny3MdDhHGIb48WVN3U1XH+QrUNPUxT/koLb5ejZfPw9FkMR+vMvSnldM9fY1YurypwZ9OAGk5CtoENmwQCCAOiEUiSdiJ5Plc5zIh3PV9r1aT+MV7HXF2BEzLZaeW4crXDqZ82iO74hVB+cBMjTm+PhQs3z0Qkg7q/RiyncQuj6mYOKWyfX1KJd4w0PfPP8ziw7T44HD52iOf8b6fTbh9j2O2gQMJYFEXRrhtbByA8eyYtbsQtwHpeDkZy3soZVBO6dSvMV4+TW4no+HNQkI00ma2YLkyIfv4vKRN1a9WpysOSN8f+5b0KixRGuFfCrOzuTwrPHBFMSeqE3PLHVIOyOVFk7joKRZpw8R8cQ7JfeGBannzQ9A0bLdjKF/aJA1UlPeUnuXBdh+fz4o7lPHqaSguVYbLf5DZ+NPufLH6tEcW19PJ3eLT7nAmuxqCTOajxfxmIqx/OJ2N509jGJoOyWJ5M56L9VA3W1F2s2XkVxoUOFCrYpwZBoHluPnnUA1ZZhZfyoKo+rYIGmW+BSUV8sSZ58bUcD5eqost5uYV/rv5UzJUEDyLYrUx4dli/t9C4S/yce22GIXY4uuImyVnhB0UMhrOnyY3wxvB6f+o+OcUMdH84Lmw/1cB3cyC66dd1c83MM3FaExmkylZjO4Xowmoe7Eiq/ET/DAkX8l8PX4ek+zA27VlTFNK7o1XAOq7NSYu+tRVh+3nyRBWNJwDjyvpQ1/WY2EF48wK5gtyux6vxmS0vp6ADy3I5/X8aa0X/IOAB4/KbUPRl9g1dWk84HWrkt08jZeAckVmi+XwabGcyMHl8O1f0mx+++23v4D1zVdAhdwPhUmCEqeTL2tQ4mgCQE+LJxhSxExeuujOlanH/CKFDbOoYHpc3DT4/3Eby6zE30SQG5G8p2TS+R1f3iaqW0QqIp28G2AQG8x8VNErD/vyeWI2LGwSRi09anLx6iUbBWODMTsb86xiHvWlARWjjHryjs6l8r2bWuFi2HJsGRXVVYnSe9Vs06mUSVxv5KObMnH5yisftwRwMfw+azH90kpnFyjAt13hQI0hC4t/J7momVvzSxZwVb2RiS8DP/PlDaOrBXhfcOSwno79lZc0ofDRm8VoLfxwAS5L1nPwofFn4VkQEqSfTGaiK2/K13MibuqqM4i+7ONcnJdso/hjAagq2IhaX96juvp1C9SdsC+Sf2xqMtejZnZ8DJHLEu/yjOyJgIxkQP9vu8meeCK421WNo5p71Vyr5gSN2aqZq+ZFNafoY0c1K9X4aGyqmolqPqvmTTUPqHeumrZqLMTnUjWBap5V46HeFaKXIiZeVTNG87QmjlTjognnVfMGaLpWVqyahWr6iBctNEMfuWp6CGStmgbCGSHIO9UMkdAPaAHqiIkYoT5DkF+RRAECeUNWoNXzhKjrlW4hClqfx6r5gpbjCmmih8jOEPM1hOwW4aSIQR9Rf0VGO0Iq8JHR3iA70xI1kXlfIpAuauZIomtkIUfIZSJENkAL3kH+cFmsimlLCtBMVNNTzb5qPNU8qeZZNYlqbNU4qolVc4OQLVXTRxMOVcNVM1YNU81MNZdo7FY1L6gZIl5aqrlSTaAaXzVd1Rwhso+IQYoYXKumjgS7Rwxqsi5SlkYWokbz+ZBN/1/SPxa1NM+S0M2qwY7co6CWDO4OeWBa09q5GTxeXi/6NDpppAHt8sFba3V4/pqY9XR+Nw1ury/GL+uTfa//JU6j9v3V0Fs797XZmdWcpO1lvAy/Dk72Y9ZrNh+uTnzz7jTwBqf7kb+c1Wqe710/1IK79Vm/HjanNLh+7h7V+HD59mV981xv1ujXo3jwdfLY7t399a+ljZCI7yLHMfG4yzfliwX5LrPiUYutkpNrusUOnNF8p8U+uK54ol9kthJ+mW/1K55t+C35jAL2WqbOt+/wo/p840EelU9VXPnrMnh73xveTSAlM7K4JeqeybTk7wG4nn6B+o4Q+yBf+ehrB/FEl4tiR1Ziql6Upzy3xfsseVbQDvtJ+a3pxms5vQuMWhFsIeXzwqwQqNjvM4535syTv/9Q7N7zD3qrnk141y02+4iI+I0djHG2gTHFOGcbONF+3FO/17FRwe0eLqaThX7suU1ay1EnSxq1a0lT2PKWDMzMrqY0Ht3LImtUFIH6SAtWXP1W1fbnNp5tMc/l7oHFneDA9jg/8AMaQtc2PZM3LdcKdBllFldUqISybY8xR1/QusKdFJB4DA6Gf0DdPnM+WuZHixdOqKpkwZD6SZ0juI489NAP0Zh+iLZx3OBAyei/P25oBM2wG5A4AVPrSlNrg4V2eu2wE3b7sTS9MOlHh1FDHHQVFlmcT8gX3D7fqP1+V2nmd5SMDdTTYx5KZBYC0WO6YOKod4AmcJSTAwRiI2QcNQcobQco+YffIKRxmgi1iUCaaJ6LegFSgYnIYtkP0EcTfaQIdR9Bak18RGTNqo8c8aI5S5EKaBWffpXQundatdI/uB8o6gqdk+co+Z+ijx3VrFDW12PTvXI98lk1bygn6965atqqsVDtsER1hS6DPNS7QvRSxMTrXrmc8VAJcYQqiTniBc8boOnXe+XqZLFXLrSukNAMfdT1Tw+B6IqngXBGCPJur1zOPCMN6gWoIyZihPoMQX5FEgUIRC/HCVLPE6KuV7qFKGh9HqvmC1qOK6SJHiI7Q8zXELJbhJMiBn1E/RUZ7QipwEdGe4PsTEvUROZ9uVEl/u2/sjkoKGhIvZPa3ytHJ71X01vEBJHVDq43RjcImd5k99GEw71yJNGi6BAyQwzqMb1le0HNEPHSQlrSsvtIkUeI7CNikCIG9f6vjgS7Rwxqsi5SlkYWokbz+YCmay3pTW8DqQCfHhwhifBWVu+176rUqi3f3CuHuilygHMEoic8ZuZZjkA49Ggf0bFDO7GOKylCpiF1hNVhaYBcTDu4dvdDxJl2uATxWUeEdDC9Q/Jp6rdIhou9cqDVXrhGMuANsZZWR5kYkdWyawfX+0YdgYYIi6bgIEi9ADosnaE10mFiguhpQkvEC96L4niLJdITcGxsouYByaCn+0jzWsmnSMkeInuKyEYIyxIJXUONhyC9zR30n9jO/r61BKfE8vTvVe5eHrfn43OTX8z96fP1zfTlvvscTLyGc39ybgdJP766GPTu+/bj+DoZtG5fro5mq8vhS2e4/2VKI+vr2b19ejmn3de7s/WU7r+Np28Di8YP5mjmxH7K3wYPgVMf3jw6aZSGs4fBtD2L52+dyJpdv9F1On1ymr3RanzFOq/hsdVOndZVeum8vtanV9fntLm6unfM/flDY3Ty9vz0lty+WWn0GL1duidZQZ4r57TVd8L1jXfqXezPe42b1zVweRmuTuqf94+D69Hs8e5y1fg8/WIdPsfs/nH+YgbHfMopj33mrhrrMJjeea3oeXT51H22g/1mhx4dBfMXfrl/MvSTy6/Py/Mv085F97xx9Oqd3vWcs7v2Z2fOIr8/armn+5PruntOW18e7cfV+Npr9s3jy4fQej7Vpwv6epPZ+qlAeZ/VGT8t5OE86YnfGiO9XvNA/ETGc/J5uBxNhlNxSTdeEQCa3I6XE72Lg93S/wMu3J25ZW5kc3RyZWFtIAplbmRvYmogCjIwIDAgb2JqIAo8PCAKL0xlbmd0aCA1NzYgCi9GaWx0ZXIgWyAvRmxhdGVEZWNvZGUgXSAKPj4gCnN0cmVhbQp4nGVUTYvbMBS8+1fouKUFO3rW0xZMYJtecugHDf0BjiQHQ2Ibxznsv69Gs10oDUS2xzN+M3qS6sPx63EaN1P/XOdwSpsZximu6T4/1pDMOV3GyeysiWPY3p7KGG79YuosPr3et3Q7TsNsuq6qf+WX9219NU8v5ffxZR3766cv8zV+MPWPNaZ1nC7m6ffhlJ9Pj2W5pluaNtOY/d7ENFT14Vu/fO9vydT/faIQdm8O5pjuSx/S2k+XZDp93psuyt6kKf77rnJCyXngc2ebzC1D0+RL1bWf830ZmiZfMhABRAIxAw5sR4krkgGMgYwBDAuGJcMCaAG0BFpIHCSOEgcAL1syWjCcQCKUCBi4bwm0BUgAEoGUAQsLlj4sfLQBjEBGALADsCOwAwCTLZ22cIpp49A0EVUCkgfGD4ivKKCsoqhiYcHSh4UPhwKOVRyqOAR1TOtKWkVZZVkFgJaVIQPPkODeEXAFANtR4orEQ+Ip8QDOAM4EzpCgi46tdGilR1DPtB5pFbmU4bSEg2uldYV1RUuUfVH0RRFDmUWRxWMleC4Hj+Wg8KQ0pjAmfb4vQ9PkSwbgSWhMYEyRSxlOEU7xUsnQwsD8KidZMckCk0KnAqcCC0IfAh+CLgpbKWilILkwviC+wKTQqcCpYq0oF4xiwXgk94zvS3x8T/lRLR+Fa6F1gXWLubGcIFsmCGxPiYek7E9lb/Olytv0737EjsV58r7zw2Nd86FQDp1yHmDjj1N6P5eWecE+x7/6A0c2I6plbmRzdHJlYW0gCmVuZG9iaiAKMjMgMCBvYmogCjw8IAovTGVuZ3RoIDE3Njg4IAovTGVuZ3RoMSAyNDcyOCAKL0ZpbHRlciBbIC9GbGF0ZURlY29kZSBdIAo+PiAKc3RyZWFtCnicrbwJeFRF1jBcVXfvfV/T6dvppLN0QkLSIQQiuYEQgQiELabRSNgXFRIWQVSIyo4KjMriMkRHAXGhSQQSFon7Mq8D44o6jswMijrympkX0UHS/Z263UGY5f2e7/n/G6pO3ao6tZxz6tQ5VbdBGCGkR62IQXVjJxQW15fcPBohZiLkjpl+69TmhsYRr0H6DxDOT79tsfzzkN98CuVfIyRMntU8+1bx68M5CGmrEeI+nn3L7bPqT+46jlCmglBFw5yZU2cE/1H8N6ifBvgD5kCGZYUESeYpiDLn3Lp4GTE48hHCJ2kftyyYPvXusQ+MQIg8B+/Nt05d1izK7G1Q/y14l+dPvXXmF22lXoSCJsApbF6waHEiD32MkJm2LzcvnNn8t1FfOuC9AsJ7kIdhXvTRIRbBmFE6MkGOoUfuyeop6SnrGdQzsqe+J9oztWduT3PP4p53e95PJBBSy/v1DOgp76nqqe1pgPLpPbf0LEyWG8+YjOiKx9idgjXGSoTVpAdlohDKQQPRGEpJNBlNS1W2IwPKR2HUDxWgElSIsqGeCZlhhBokIRdKQ24YoQ8ZEQd8wYhHDiQgLRKRE3mh3XRUjDJQENrOQgNQLipCMspDNujPj0pRf56ilKMo2YuOkHJ0HwTE70VbuHr0MPcm2sr+BeWn0g8BHAVwKoTtpDzRC2WPQxrRemob5Wge5I0H+CB9h/wH4X0bLQdcxNUnEpA/Dd53QLqX8aG1gg+tYBehkVBvIsAqgBtpGsbzNn0H6KLtQd4WeH8SQj2E0RBKIFxPy6COH9KT6Du0FwVYBPV/RWUIHUbf4SJcRCaTV5jFTDcrs1u5N/gZwgxhs/CSeKdUJF0n3SKd0bCa97RDtZu1F3Wd+nr9RcNp443G35n2mGOWHGs/63O2R2xv2W+xf+cY4njRccG5zJXp+sld777Pk+Y57J2YJvqeS89XecmVlFQucd0wxVjxg+gWVRY++ZcKH4UnlkzNvnjxUq8JiZlQV0rxnsr3B+Qo8E/kHuFKQAi9Scj8Hs0iFpEjWoEl9GHRPz0TRw+TkQzS9z73fnwcLhGG4HZVipEwJD4GDTOhixcvLjehX3pKPWVqThn6O6pAm0BmCMhUIZoEeBXkFRgJ4Q6jNDXsRmlsCKQMJc70hfjcxBlaRiH5Fhr3JUPqaUfPoY9xDpZRB74IUvgTduP+aCSsqB9BUvehXvQwyN9EtBVbQAod0OtIzEKdMLoPP5q4LfENugZ492TiEL4nsRfKN6E30E8wgj+yGEY8BupPQjPRN8yXKJp4BOR8LUj7YDQeO9BU9BH8/QBjeBA9hF7CdyZ+gl5t6B5orwJVoarEy4lLIP33sZu5U9IBtAUdwXxiemIurJIMtIGEEx8lvoD1FUW/Qc/BmMK4mx2BAuhmtBptx27mDUg9jJ5CcawjjcwwDjQXzKsezUdL0Qa0F72DLbiOO8X1JO5InAWqWmHVTUVz0Te4FI8mT7O6xJDEp+gG1IXegvnSv272BnY3d0O8MvF44hVY64ewBh/FL3PF3AO9dyeeSLwA6zyE+gNFxkA/09C96GX0Nvob+jtZmViJRqAJ0PPr2IdlHAKKf0TcZAVZwbwP2qIKNcJol6CdKAYcOYyOoGNAm8/QafQltmEvHoWn4S3470RHZpATzKPMi8wHLGafAXoHQVPkocXoaXQQ/Rd6F53AHLRfhOvwPLwAb8OP49MkRr4jP7Iiey/7M9vLheKn4z8nxiR+AH3kQdeh5Wgl0PY3qAO9iH6HPgQp+x90AZvwQDwHP4Fj+DT+jkgkg4wlzWQreZo8z4xhtjAvs6XsUPZm9l32U24Nt1GYKsQv7Yo/GH8+/vvEocTvqR6G9kOoBih6N0jF0+g4eh9a/wR9jv5M5QfaH4wn45ugl0V4HX4IP49fx7/H38IskfqXQQaTauh1AVkIdLqHPEgegt5PwN9J8in5nPyV/MBwTAYzgGlhnmBiTCdzkvmKNbEhth/bnx3LTmYTwJli7lpuAreHe5Z7hevhK/gZfDP/tXCPsEr8r9683j/GUXxOPBbvANkVQZKWAyV+jZ4EuX8RePAOUPR3MOLT6DxwwYMDOBvGXY5rcC0eja/HN+KZ+B68Fv8Kb8eP4ifxCzADmAMRYOxhUkUmkKlkJllF1pL7yYvwd5i8TT4ip8g5GLmTCTJhpj8zkpnM3MDMhzksZlYwq4CyW5i9zAnmfeYs8zVzDrjmZNPZJexydge7m32R/T13HXcr/D3JHee6ud9zl7hLPOE9fBpfyM/j9/B/FnhhgFAnrBc+EP5HbMZpOA9GLl+pTogb1mA62Uts7Ep8DjJ8mIUdaQsKAx8mwKr4H1TJxIEvBloOY7MTN2ulmLzCxgB/MT6CSvHraCVPGNBM7GnUjv9ATrOvkmvQh7gJu9ndzHzuHRJAz4I22kyOkiN4KHqRVJB68hiD8Jd4D/oS5H0ZegjfjBehZ/E5PAjfhcvwSvQBcTAT8CpUkXiSsFjCI3EPghGgu9kZ6KZ/1qhXP7gcLJlv4r9m9eydoJ860Vbg6HPoC/wMuoi5xHeg3RjQRlNBy9wH8r4aUa3XCOtsJaxHN2iQW/gT6EXMg14t44ewy1EP+gf6hjsMEjUUNOnZ+Fz21+xfEmWJAlhhsMrQHlh3c9C1sGK+BCk5Bu/07UZY6RrQJcWwquvANpiB7gKttyURSzyWuDdxe2IB+i3gXsT5+CJugxXRCRgV6C3424Q+wRthHV77v8/zPz3xGagbfYtdOAsXw3o4x93Gbeb2ci9yL3Hv8v2B2qvQoyDRfwZp1sAMpqPfo2/Rj1gE3rjBaonAeAfC2BvQLSTKHEPDsAc1w5rNAT0+NDWTRdDKPUC9x2A9H4O10QN64kb0EjqFCXbCjKZD/yK0Uwt0ngK1dwEH78UdkDMDtHYe+ivM24AHksXQnwItbQWt1Q1j+gP6CqidUMeVD3qhGtdDWz+i69EM6GEAqsP7gQMHUTlo1mrmv4DemdiEhuIM/BTgNcEKNYBVVc79BROUHx+TGEjmMsdgj0lAfhvsXl50DW6BURhhHr3Ijsei0vh4GMP7mGFj+D11FDvIzMRaZmn8FvRb9AzwRGFvE8DyVaomKpVDrqkYPKh8YFlppKS4f1Fhv4L8cF5uTnYoKzOYEZD96b40r8ftcjrsNqvFbDIa9DqtRhIFnmMZglH+8GBNkxwLNcXYUHDEiAL6HpwKGVOvyGiKyZBVc3WdmNykVpOvrqlAzVn/VFNJ1lQu18QmuQJVFOTLw4Ny7N3qoNyJJ49rgPT91cGoHDunpker6c1qWg/pQAAQ5OGuOdVyDDfJw2M1t83ZMLypGprbr9UMCw6bqSnIR/s1WkhqIRVzBpv3Y+cQrCaIc/ig/QSJehhUzBOsHh5zB6vpCGJM1vCpM2J14xqGV3sDgWhBfgwPmx6cFkPBoTFjWK2ChqndxPhhMUHtRp5LZ4M2yvvzuzfc12lC05rCuhnBGVNvbIgxU6O0D3MY+q2OOZefcf3yCo1bhjWsvbLUy2wY7por09cNG9bKse5xDVeWBmgcjUIbgEuyapo21EDX9wERayfI0BtZHW2I4dXQpUxnQmeVnN/M4HCa0zRPjknBocE5G+Y1AWs8G2Jo/O2Bdo9H6UqcRp7h8oaJDcFArNIbjE6tTttvQxvG397hVmT31SUF+ftN5iRh9xuMqYROf2Vi5uUyNaVWp6na8Zcpi+mIgiNBIGLydBlG0hCEOQ2k0cyBaMP0gVANnigGrNgM4MjcmDSsaYNpEM2n+DEuyxSUN/yAQAKC5767OmdqKofPMv2AaJLKyWVRg/K+dCwcjuXlURERhgFPYYxD1PfSgvzbOsmAYLNJBgDkQ3VA26nRQYVA/kCAMnhjp4KmwUusdVxD8l1G07ztSCkMR2OkiZZ095XYJ9GS1r6Sy+hNQZDkF1XD2R4TQ5f/GU0O6/A5g2LY8b8Uz0yW104I1o6b3CAP39CUom3txKvekuUDL5elUjHrsAbGS1Ip4mXUUhDKGy9Xpi8NuhibBf94VahnxBgQSjUDyzUxU9OIZBzVBAL/EadTEK9A6kz0UCwV/IKWGmVsUPjq98FXvV81Ot0GBsbLhkjtxMkbNmiuKqsBBbRhQ01QrtnQtGFqZ6J1WlA2BTd0kd1k94bm4U19DO1MHN7ojdXcF4VJzMGDQFgJGro/iNeN26/gdRMmN3SBpyOvm9jQTjAZ1jQ0uj8Tyhq6wFRR1FxyOZe+yfQN1WIQ9HYiqkXeLgWhVrWUVTPU9+mdGKl5Yl8eRtM7STLPpObBU4BSZwcokU3PGP712T+xtUrPPIf2QQCHC2IZQhsEBinMcx2CvljpBGixqbDdES7uSnRDYlCJml/wUHHrUeZZ2AZLIPvZ9kk0+9kOpbpYhSWDk7CwvwrbxWSxYCv2V3kArRACQcZUaiyETRB2QjgOgYcBPYu+gJCAwDB7mCfba/zQwtPQkLHKxjwNs1YgPgEhAYGB0T8Nc3kafZ/KYWFUv+mQdLT736hYXuY3gGWE2AShFcI+CCcgcGgBxDshJCAwkAITGQJhnmSeaDf5TVUa5tdoJQTCPIKMGCM/tL69w6TSZkeH0VqsVJmYh1EdBIJizGjUDYFAs1sAbQsiUL22vaC/SsLaDo2h2AT1N8KgN8JANkKXbRBj9V2BQOtv7LA6aPP3thvNKt4d7UWRZKLD5CquAyosQ5iZycwHJ8kPxvV8MEH8zHSAPoDTmBlIr45T6TCailuhv0qoXgm2Zi4UVzEOsOD8TDXjAeuBVlvSbkj2s6Q9J68YZjyMcalVjIwejCc/IzJCe7FfPsIoKvHXdUhaOr517SZ78TFmNSOAc+tnWqGW0288xmiAsxp1JhM7JH3x5iodMxGmORHI4ocxYqDyfLWh+e3QUJWZGc6kgcPnZ25mfOB8+pkaJl2Fu5knwM3yM493hNL83UeYB1WsX9FGofshSdEa0qE3FHdXScwQKI0xDwADHlA739wRGgimaojJQUUQCNB4JaRWqkK/AVIbgGsbgFMbgFMbYFAbQPoQsx5K1kOdQmY5amaWos0QdkKaipW9HQjapSYyc4q7GDfjAsKYjgApMeR6OiQDHZmr3WJVq7k6dIbiymPMIpDzRdCmwizucLqKFxxh8tSp5He4vBShuR3E9Ri4TiprANFBWXKMSQNCUML4mPR2uz9W5Yd3Ksh+hMk75CQlEnmffEjZTb1HFf42Bd9Nwd8lYaKbnEwuCvIehaer0siX0NgU8jnaCSlCjpBXUREgfEo66SjIJ6QLVQI8Be8zAHYBLAF4uD3wlr+TdHYAgLE/2q530MmSV9vDhamEPyuVcHpTCYujuCqLvEJeRmnQxMcAMwG+TLpRBsDjAF0Au8F+fgvgAVKKBgN8MQVfI0epiJND5CBY8n7S0W6gQ4i1CxTsa+cpeKEdJd/qCv1HyQvkWeSBqs+3hzyQu6cjlOk3HoH2MPjai9t9fkuVhjyBG/B5qNQGdj5AZCFPtpfRRja3H5X9XWQz2ay4ypQspUDZxRRlFRUU7WLkLLlALpN3yVUm8gAokJ0E1i/ZCHEZkglIDwQFwmayvp0ti1X1wpzovAhqhbhNTTVB3KymwOdEpsulPWqqkqxGYyEQaGMFhJUQWiHcDf7dZrIcwh0Q7oRwl5qzGMISCEtBmzQDRjNgNANGs4rRDBjNgNEMGM0qRrPa+xIIFKMJMJoAowkwmlSMJsBoAowmwGhSMeh4mwCjScWoA4w6wKgDjDoVow4w6gCjDjDqVIw6wKgDjDoVQwEMBTAUwFBUDAUwFMBQAENRMRTAUABDUTGKAKMIMIoAo0jFKAKMIsAoAowiFaMIMIoAo0jFkAFDBgwZMGQVQwYMGTBkwJBVDBkwZMCQVQwTYJgAwwQYJhXDBBgmwDABhknFMKn8WQKBYpwGjNOAcRowTqsYpwHjNGCcBozTKsZpwDgNGKfJ0v3MyarXAeUkoJwElJMqyklAOQkoJwHlpIpyElBOAsrJ1NQXq8QgIDYrIKyE0AqB4nYDbjfgdgNut4rbrYrXEggUNwYYMcCIAUZMxYgBRgwwYoARUzFigBEDjJiK0QYYbYDRBhhtKkYbYLQBRhtgtKkYbargLoFAMf7fhfL/mTXkbtwgwl5LWnGuClei71S4Ap1S4V1ovwrvRLtUeAe6R4XLUZkKl6KQCqE9FS5GfhG3+8uMVQ5QAWMhTIGwAMJOCPsgHIcgqKkTEL6AkCClSgZrFMYKO4V9wnGB2yecFoiRH8vv5Pfxx3luH3+aJ3KVl+hVPQqqBW1S45UQfw8BNhGIK9VUJYlAvxHQs6XwFyERxXxO/j4Pn8jDx/Pwvjy8KQ9XSeRazKqaTkZl4F/7cYOiCw3xn4JQFsoeAprpgYPfOf3toQH+Tnw0CXKVMMDvIOyHsAvCPRDKIBRDKICQBcGv5uVB/QYlI9XkUQjZEAIQZNoFcjjAQLSYRaWL6PGujtf1SKL9ZOcA3pH27CIAne3ZYwEcas+e5q+S8EGUTa0ifAA49yzAfe3+M1D8fBI81+4/AmBPuz8CoLE9ux+AG9qz3/VX6fEk5Gcp6sQUnADzpnB8u78eqo1r9+cCCLdnh2jtPOgoC0pzcQM6AzArhZWZ7CnY7h8MIKPdX05riyibMh7zqEAdHgeBQqYDBvR9F25gsaL1n/M/6P8O0P8KhAXx+ETuZAGcyOrE9YrGf7Tg11C5yt9epaH1YX/Yn4IxCg/4d2Wt9z8KbeGsg/4d/n7+Bwo6Rci+H8a9Xu2i3X8P+ILPKlZ/q7/Iv7jgjH+Rf5R/qn+8vzEL8tv9N/qP0mGiKG4gzx7010GDI2EWWe3+a7M61SHW+G/3K/5sf7l8lNIXDUy2W1ZwlFIAFSd7zwf65mV1UhmfVNaJzUqe0CNsFm4QhgqDhaCQIaQLPsEmWkSTaBB1okYURV5kRSIi0daZOK2E6d2MjVevaHiWxqyaNhEaE6Re3RAsEjQKxaxMLamdMBTXxrqno9ppcuzChGAn1oCrxQWH4pilFtVOHBobGK7tFBLjY2Xh2phQd0PDfowfiEJujKwDR2ZiQydO0KzVXnqmsR+j1fd7uxDG7tX3R6PI5bit0lVpGWIur6n+N1FTKg7/8riuTPpiW2snNMT2+qKxYppI+KK1sbvpiUcXMRL98OouYqAg2tDFNhPj8PE0n22ujkK1M2o1kGYDVEPZFEA1cSiSaTXQJ0NpNeBRsl4I0KFegAKop9GjkFovpNGr9VhM6+0/JQ+v3i/Lap0shE6pdU5loSvqgMQAbvX+UEitFZRxA62FG4KyOrBctSG/H6oU+NUqGOw6tSE/VjuLFf5SJStVpfRylVK1Lwb/UsefrGPL6atjy4E64f+Pz8yhYdzRf8mKV+khUlNw+EwITbGNt81xxVqnyfL+FUtSp0uhpmnT51A4dWZsSXBmdWxFsFre3//Vf1P8Ki3uH6zej14dPrFh/6vKzOr2/kr/4cGp1dGOyoqGqqv6Wn+5r4aKf9NYBW2sgfZVWfVviqtocSXtq4r2VUX7qlQq1b6Gz6VyX9ewX0RDo8NuTMIOotWADDd5A9GhDlPzECrQXYMDrhXewyzCe5A2HI3pgkNjegi0qKCqoIoWwTqjRQZ6Upgqcq0YHPAexntSRSbINgeHoj7SIlqpNlY6rjYWmDC5gYpKTJn673m2iD5qsQsNn1sN/+B9sRrg78qaaNG/fRb/u2fJkiWLaLQkvAih2ljehNrYgHEwEkGArpqqo5DXry+PYdS8/ZI0vDPRDYVhGAReTLujqTAOAwUVDXhdAmnj2wRCXYXFHR5f8YJjsIOvhAB+HFnaXqi6z2RpR0YW9V8WdxSWJiG4qxS2ewLF0ENHGaBSmJWEirkAEpuzNhdsLmvLaitoK+Mh9+AuyPTvoltpe+EuBi0OL+ojBCQXR4HYMCza3xPtaT614zaaCIej4UVYpde/Ehv3Ef0yYRelWl2kNr+4jyHJ/EWpRoATyd6X9KEtSSGphUtUpGQjybfL0S8PvKnHOiKalbpfZ5AZ9d21s5A2p9I8pDLo6RBL7/wzwJlLpgkyoEmpNAP501NpFtKrU2ke0rur1CdctXDu1Fvyhy64Zcb/PYNed1/+C0NYiOaiqegWlI+GogUAZ6DxaCaajZZAeiqU/t/r//9RA/YyIAgHfzA/AQ19keA4L3SSSsWKODbOII3AxjFyizwXJ8xRHEISjmEXcoVNFyp6K8aYzleM7q1AlZA2XYKof1HAHDBnQQQ7J7okM92XFA79jGS2G/pC0cRZ7iz3PjIiL3pSmbSN2yZu1203sCIWDKJRcGW7lklLLcJS8zL7Gna9uF63xrDast62zr7Ouc61xqMTLKJN8NgtHpvHZfcI1gK95C4QGEf2Pg1GGpNG1jCaTnKfIhf5FF+Tr9nX6mvz8bKvx0d8puw2hI1gKhTBVKFSR9qKV11hmEFjy+hzo02NLRdoAlWeqzzXvwg3NragRmukbMCAsgElMjKbUEBG2GYpKR5QGgkFM/josOLnZ6/vwNV4dXxF/Fi8K74C9/9q//6/fH7o0Gnywentze3hQfH58Ufij8cX4E14zj/iiUTi0k8/U4k8wu4hrUAHBg1TrFi22CNYTs+M1DEYMSaGMJ04qkiQ/hIMD0w6ydRDeD5ys1/dqQ549PlzjSY60sYWGGk4bLUG8JENeHD8HLsHh+MfIg0MFd0HHb3IHYY+FnQhDrRBcSTCUa0QzFKhUmlzRhCncHVcK3ea4/xcE9fM9XBsK4ehUwaJhPkEpCOGTiOmG/UgsMWL0El4Y9F8tv/OFO0WpgSgsoJSrWUhrMQSEIH7cA53+GINjGMLLDg3jENHXIpWy4TEkJZhGcx0JloVKW1QRCMPGhyRwPjqSEHlqbR+kAsRL4mav0jfaVhW0misJI01SX5NkOSzslSomU3msDOleZqlZBn7lLRXc0A6rLkgXdQ4drKbpZ2aN6S3NR+TU+xH0ieas+Rr9kvpW41+qbRMcy+5j71Xuk+zmQgN2plkHjtbmqO5jdzOCtWklq2WajXXi9dLDRrBpSk0RMggNiIN1lQaBIboWF6SNHbiYZ2SsJ8nwyY2KH7CMhqJ0wlCMW/QFSOVf2KdqI9oaaTO0qDVR0TFkB3R0giyHlNMNKEVgd8sJoIGlFZlBZDQbHGWJ7VZIy48Z/rgHM3wdiYGKwXQi8yKklTMsDaGYWGr1RQzBJIEmmF0LCE6jUaSBNFvwIZOrO+gd5OHyUCV9Tc0JlnunDAxwhULirBSxOKxlcCFY1pZqwMJG6hYgNcKVEQKVELFfh3W0Wb0/ZfAYj/fci4cNlX8t6nC4zb1tvS2VHhcpt5wGDJMZ1pg8ABh/DDatVy/8Nq7Xlvbz0VBOApSURuzTgCrSkyc3q+VBw6MwuKijyoroP9bGkFgMKaKAweweQs+gjVYwEfj5+Kfx/8S/yN3+JKL+fpiDXvPzytoAD3yMOjin0CmqB5ZqmTxXJety8Vcy+HZ3EccsZiz9AYD8pqyQI6NSAT1IGCBir6kjQhUPzj8vqKUfuB8JqOMqWgT0AobO9L6T0hKNpXr0aaWC2FVK1RQFWcpL1R1HGpswXTQstPhsNt4XuCDQTcB1UB1Q3Yo+DD+DBvGr9g7bduYeW+//OS+24bdNKK0jTvsCHy+b23nXLO992P2lXhTv2lVdXP0Guh4K+jgozAfOwqgn5R7yo0jjdcL87TzdHul3Ya24EHDKUnDi7zGKTo0Aww1hhqjIJoks81gM9pMAwwDjNcalxhuN72v0S6Tlrlv862T1rnX+HjJYZN0RsMEwxLDKsNDht8YOIOs19n0ep1RZ9c7HVlWkw032dpsxGZDcoCSCwhnRyII0FElG+lNeqL/wJvdxsf4bv4kz/Jrm4NYDhYFSTBgv5JqGf2n/0I1VZeeO99IyaaKhUq5xhaA2GwpL1/bL9xouMv0GjaXI3in9MQtjZSgxSo9BYfDaQ0w/UgwaDb/QtXgVrLgrx+2vvJy013zOuK//mjhxJtmVXz24byKsSMyXzzLHR77zj1Pf5w2cM2z8T/jymejgd7HmDGZDUNH3aDjqM7NB5npAhoLaKESLJSK2CKuTmqWWqXNksBjjmSxDBGQKDmdHnYlh7lOXKBoeEHGRYgeldNXM2OoI82klWwmLHGLvc8l51w7rmE/UQZGK0bD/HsrIBo+s/pMSmYqVL0Iwl0asIOAfxEfzd4fH8O+8tNPPw85Su8RqCz3wLi0aLNyjcixgpjFW/wcLuL2cYTjJIalfNFIWVokCnwtQ0ZokBZrPbK+SK/oGT0rXckI3ZXiO8bUeAES5yvOJ7doGsyUE9Swo4qh3VcOeqG13aOC/VaqfKJQieFMdNzAERhzMjzMVl76hpzulZkS7vBP8SM/xlt+pMe26CGQ3akwfhNsrSuVkhwuR3OtcyY7U8flOcudIxxRxxwHV+4c4F3r3cFt1XJ+cxZ40lZLltEkuv9lZVpbA1gOFAVIwGyRkWwqMhETnZd89bJsvLwuk7NKrUlroBiWpMVuE3j6FwR1UlJcNoSAAFEJeoj4DjXd3dlUUDZr9L3Tnup9H+d8fmfZiCkVFbdMGHKAO5wWeiV+9ncH7m2bXpvnZ1+5VGqw1L++d+/BWRYDgk0Oo1GJr9i/w76dj08q13SZO30Hc97IZwWrYHdanXZXeCY3M2cxv0y/OOcT3UdBXVQzyTApIxqco5tlmR2YmzM7f6lvjW9rQGcJ0v0u3R+hUJnp9kTGZYwLvpzxcpBtyWgJ3p1xd/BPGX8K8mFNnj4zIzNYro8EazW1+uqMYcF5+pnB2/XLM9brN2Ts0uzW78mwShpJz2fwQbfGrXdkCBlBjZ7FznqX4pYjC1x4gWuni7gOk5nIC3uAzlPu92JvgY1BIzDdFEZ65EgRVnAdbsKbcRvYd91YxP/NKp5yE4vZgjzJ9X3CiZ2K1Rlx1grZIU8/f3abKQacqcXfm5Oy5y54L8Wg2gkN+xGshtFUA4wxXQAYXgic6m0Jn28Mn0nCheEzsFck9wBVGDOAHl7fEKDHyRT8S7u1PAPIAwDe3m630LeTitFSrpct5Ro1GGne14pBB3n6co2LBmv5VR5JNLVR2wdpBulLM0qBjiP1wzJqgrs0z2RoUGO0T3SyHI6kcZet/pVGBoDhxzq5EFh7Am+3OR2sqp3YoIxGYdmzc+2mLddcF+n676a1K79/BtuwU4ifst51190jC/MH4tiJJfcl0PH4t/GP8OdpW9bdPi4y0mvpN7j+9heaX53193f0LdNLM8ojWYWzbj22ccUfbsaY6qipYB/fBPLlQR8pY9ZI623rHTvRdv5N6QPmA+0PjJQl5ehy9Lm2XMcSbom0hhNB9Jwges5cksdkcUIOt4PbJr3NvK7lKvFYMOPGmxA+TW03RBeZ2RVRoUYPEE9WnK4CVjQoBkvEUDvFiMcasVGxuyLGTpyjZFgKNIzxe0M9+h6pTXmK0nCaPbtNwEbBLxQJDF2vHd4VEy4b0aq6AXZTZp8PN7acCVNIE42qjoc9H3Mq/VRz2ulIEpc3myjl2UrsHxp/97v4H+Lr8HIcwfo9M4rjn3mevu03v32r7ba9xHtDzzdgSE/G8/HDO2+K1Sxc9W38Yvzb77ai8EKg3XbQQ0HQQxL+nWKQGF50M06RtYAVCxYX6rBoK5nOpClEoZIHVg5TLIA/IYiMSIjASGBDgf3EsNQKYqkVxBbzJ+hOQDYqbkVbp23SMs3aVi1p03Zriawt0oLtJaUapVAxTJgQkYpVddytehkbOzTUfkoprnCYqi4Q+gupN3WPUPdElNwX4QGrKSmyXYgB5SCBpSjKSbux+5BELUmIVK8ZRHeYWqv1oLZUbNWWqhO7xtMvIk6AiGMcTDGjMGwNs1rcLLaJ7eIZhn+NOSF+KjIyUyhGmMHiWPFXzE6xjdknxpjjopbqYkUqKY0QBSKBaid9YXGEyDQSbKWQs02RAv0iZCJEau2adBneIBKJILgI4xTySbYwmJQIY4gi3EjqBclGvMJoMlx4RHhW+C35hHxNzgr/INpskiOMEpYJ64TnCE99h4W/HMX0mYdRpFqHZupRmrdjmTRga/zj3v1gFBYw71+sYY5eqkY2J8KJ3sRZMlj1qQYqPoTxSMLYCKFf54EXi/9KPBzzV/ChHrwl6UMlVZPKAGqygjnSv0jAJZjBN78f3+LmvrtoQ2wxyNXjsD8/zb0ALvI1iqdOoL4RC2sNiSznEQhz5ebL9++6cvONU/6O7k25RtQrCtgfxznkNPfCzyN/ROpxMuKOqzaJBld1ISFxSpHKyiN8DkRJVuSURnglR2XFKaUukA1lEOWiPDYPNtpC3UBUxlXq5qF5ZCYzi5sjztZ8zRhH8ZiIEgaXQWIFCcMIBRtCAi+xrMzxNo7jRY3i8Q3R0C60Hl9EkwWrhGfpHYdi4AXCsSxGog5MIZjXVEXrx+on561AnU6SqUh+CReB5USkwyQTsVBDkmGduLU3pazA0b1u8KZBKbh6x4At9FWfITgaHBpqQ4PoX+0nCGBvrH0tJfgvShFVxJO+g3ZCbSx93GRV0OPtIqs5nIgDpS7t59mB9ImC7ZgUmkCAgT8csDIMdzz+Umvvwdvjb5DBuDzvnTfw6HgHyMwGIveeRkoNyAS1uacB7a1IBtvwlFK5NA/PMSzL+4q9AP5mwC7xOfmBLIfFbx9rJ0X2fXZit9uCGVkWqyjbqAnjzW7mW3nC1+Zk76POUtKK0VErJlDUT+lX16+pX3O/1n6b+7X1E+V+Rf1IP1sG2DTWIiuxUnEp+Lc2TdJu/mdjzQ5Wmq/cDp2AsWa/ylij+iNpR6eoZaSHjhqZOlqImkTpJGlUJ10VLmkU9VnVDFjdyZcQGNijXnh27eQFU9ZsbnzitlHxL+N6nPPK83nXXV87Kv/3e7GlLTx0gnL7O9xh3407psx+Lpx9dOWMYy16kbBvxJ/npOuvrZ4kcb1d8WWSrnHM0BvzUCKRPPMRykmInr5hARG8D89CPsVITH68z4/9AhoRJtjfRSIoF4Ux7BelFG8eLJBCFY9J4iUoXuZBLCgWe3J1aF1pEQHfMyKMsXAZPRwulW2wtsYnvmYfYYcgPXKjbcqIr/FZ8Ufrj3b2TfI1uIhuzi2RqKneWu+IuraR7fx2cZuuU/qQfMb9QfpQB0Pmv9abdou/Jf/Fvyq+oeOWiOv5VSJjpsesGq0TgGJjBVu54GnyNnuJ1xBAbk9D1WXl0nLh8mESaoG9r2VYgyLNNc2yzHLMdbG4MUo3RGvEMqCkGNltKJiRGcqyOS4fL43f0PvY33Ak/vZ3v4r/uAHLW+fPf/jh+fO3koz7ML8h/ub3f4u/uiqx59d79rQ9tmcPtR0ehOg5TH99lqnYwe/XkBA97aLaiQW9N/u2vqWJKkefU21784P0Ry/xs+hDnXo2dyufDvrTjnLADLhrjW9t4BH0iO0xx2NOfpnpLudSeY1mjWGdaZ1tvVfkfVKWx2vz2QLurJudy5G4GOGoMAeU+e2e29NvlzcI683rPWvkHcIj2q3mZ4SDjjccHznMZd4G81xhrmY5ul3gGXwduhHdgthMR0Z2dqZDQAxPQmkFRia7k1x3IDQ2o0AidFUZzRHSiScoRuYDSQqF/O5sUrsvD1tSK86SPPHLU/Ka8przWvPa8ng5ryeP5IGVqsNGnV9XpGPosuzI/ecTP1hvZ3phkaHK8+fCpt44sAvTIxe6nMpVxqmeaWOWwylQq5Dvs12QGVyMrAEpZtmpAVMWyi5zcP1vbb11mGI4tHlf/IX43aAxR+IavKI0J364vPz0gQN/+tNzSvnkxgm/Ojym3+9tQeGOSvwAnoNn403xlviOlzbPV4a9dEf850u98UTCPjjwTDEi9ZS3sIYyQVe50QZloCAKkmByig7pWvFaSbheqjdtNW0zb7c/6thtOuT42P4lf4HX6nU60FFCllXSaWX9CXooBGZMhuKt8zZ5mWZvq5fI3iJvm7fby3oxAb3kLnJ3uxk31UyeK4wWVTMlLZYKKtBUk6tHfdaAGYxih+qggzCZDCSYQd2s0gdxjta66c4VrR6cU3T3qRfe+2SFzQfK96tjAyffOnvrC0z4Ujz+06dbo1MfnbTiApXdbbDHrqK2G/jmleAD81yWIItF4nHxC5EtBOOFiCJKOsISeMGV/FjQu+MZ0CXEk7TDrvaCNf/OC26s+OUYRz2M+BcvdxtzrncwmdH7GPVwn/6pdwsdG90n8lT//DdKlsRyGoZImizWso/BDIN4joNBCKII3jknyvwJ6s+qhNbX6Zv0TLO+VU+oq96m79azeqKVr7IOdf+R0MkNEyI6WnULYFR/nUluART8k7/eN5nLf1vB1qjGOfFTvUe5w73HSdXFGnJ378rkLQg3gc4JD1aeMLNezTh2soZ9ltslPCs9pf0MfyDwq7Xb8UPMI9w24RHpIe0e/BQjebBdyMEhIYrrhdXMBm6DJEXwYIG4NTJbqKlmr9PcoFnF3qfZwu7UtLEfsH/U6MvYgZoH2Uc1b7Jva06ygoZIvFZgRF7LMiKHgJsckkSGkQnYiPDCa7Uy4sBW4YCuDAEjRtIiDqySQ7xitUf4WmqhdIgePXMYH6WXYgcgl9Rqqd2iTTmiOmqEuClNXdT1PAfJC8kUKrxM1KutDyM8SdPjAPiV2RGWeudJ+M4hyRzRDICoz+RWvcWWlha0sD/Ggb5/+If4IHBNQmAnXR8fCG+Pxo/ED5Neciyeiz/uHdhrwD/H6TkU/X0yNxFoz2NDB2KwSM9fLOXqQfFEz6BIt/gR/oh8wn7CcfRQehm3DW8lO9jt3E5RZJCWLxTpwXeTuBQLbuTgc1GIH4mu5a8HGxIIJmNkA1eSZy5bekwnmaZoeZBOFvwbTLjDZCr9hpM60loWr2Rb2S/Y0yzLdmKtolnJtDJfMKcZlsrXAagBQn4YaxGhNl4RTNMtXGHjgT/feL6xMew6d/kE+NzVlP3Fk+nuMCV9mANg201kMGpUqamqV7pto8ZAkpRE23seV+FFoBQH9f4Pd/jnV9lrLtYA9UFup4EePAZ7lAz2Ws3A9Nr0euE28TbdanGVbrVzlVfinbzX4rR4c8w5rhxPTro4QnsDO1GarJ3H3sEudy32HDQcNL2pf8P0semsycCk8TL1hxW/p9wPrYN6wY60Al6yUJfYUjvWiq3UH7ZSfzjPARsTuBWyewpkZ1vqiV+WGVA/GUUZJMOd3abBRo1fU5S8geoIrNh5lV+snoueP9ei2r9J/xjc43Nmag23hCv6jkFhvykNmMFHzsgkpRFL2S9HELDJ0MunslKmkqxojO888FV873PdXfe/h824JD/+qf/Z1le+/Ppo45FhxPtjb+fk9S/j2e9/iWdMGfnlO2W33HXh7/Gf4z+PjBxuoTptB8jgq+r636J4RB5bLBoNrDeGBQNR0kiihpNESSN24kNKWODBRU4KlAYESqORwFXQMBIjaqE2lSpMkFYrCiLbSWa0cyNEAIpFUA8oyWWN13c8ecU5sZsSxpU8MLi8Mt1AoOQenLxMcIVZ8M3UhEgToqlCfI2hcUVqyUqyVr1oeaddzKarlK5RMLbcIT5b2sxu59vYGNvNCqv4PezX7AWOVe+aysYnPfZMSGTx12gWM2uYHcwO6RHNXuYw8zajeZk5yVzSMNdohjJkIXgYONzSGFX1MJ/4mp4m8PTkyWrUVrJFegdEOlslK2st9JThZIfRnYQGZxJCDRVCJRWm6rUbrJXoypvzaNJPTK4EwbwDNHg9fqD3FKmJ3x2/tQfU+BKysff1S3eT2A/x4XRNJHqBlw3ASxG93gUL+yVliLa0W4NZhuUYgeVYhkumZYJBx2I1R+YF6gtgVmB4UWAxsF5iEacRQbN3ktmgL6j+BX2CRM0R4oS2eeI8IAiijEgncR7Cl1k6q0OiCsEE25bbdMZl6k2CM6gyqRFMF85Q/8RSTs/6neXm1A2ReCVPBeAm8PU1alWgcN/szcTRexbfgKN4PJ7U+yWZy4zrPUqqLz3fu0NYBTK8FiGmDOx7E9qj5GzjsGTAE7hZ3BKOKbQ0GOYYmi2sRqLWH9mkS+hIpW6sjoAVuFTJFQSMYBfnNTlIMklFUrPESp6Vlp0WMsWy0rLPctLCWkwohBn6TaGWkFbcBl6/21zZhdOQKr4tsFefM6mmRcuFRvfoM8hFTQt6I7uwvFidR2MLqo05J9CPVKg3VjwQDP4ANTMGgMUP1iSsaN6M2+JnMTfs5uqm6PXXXjN4fCEb2nZzdekP/ar2xv8Gc1yBEL8d5piNB3ehXBDXRrOmkuN5nZ136CJMRIy4IsFqMlwc7qoO6mSmMHeC1JTbmrsz9yl+t7BLd4A/oIvlnsw9nWtAuYW5dVBwPPeLXD5X8aRFKuG9VS3khAAreHwO1bcRAtS3SWcFk9mc7U1LC2VrYEMxmkIWszK5tMmMF5gxeEE1itHjDfnSIG9BGm5Kw2mQ92IWGIAYyNaOULZqukuVFCoDYNzZUDVbqYJQASEzO5KtDLomUph9IvuLbMaY7c9uzWZQtpxdlJ3IZrPdOX+p6LtiTh0KpYh+oREUJgBYlBWpq+ekqFVUJu33Pm94ISxZ0KlhKxC9pNjhHKDGDjsssEi2ygA1GepLrsDMxu5ZW4tqnrxxyZM5vvhZX/a4wXP6xc+mVw6omlMQP8uGtjwzcdKkiVNurN7eGyVTft2vYsTGrXFCah6dnF+zakfvJVgTI8Hv7Ac8C6Ji3KLMETxiGudzeEZ5R6SNzPrM9IVZGuCucV8fmuWeHVoT+pX7Qc8uT5f3Tc9bXh3P6+0O3u3I5nPtUfdSsobs4g/wb/C645FPTMSXWdzfnK/PVML9IplKRg5Ebl9kQealTJJZ46NULjIYI9f4MPKZfDHfP3ysz5ePS5ACudQTJGhSQEkzVwYUrwkilydCOX2AFXR6TT5lFpSpEIpVCDXyqSwoNm16/5CYK+Xoo37dTh3x63BCh3WKwRHRecZGcKQJZPUBah6U5AamOPEXTjzWOcW5wMk43SVzq/rYCJq+5VwjVfrh5NsZ1aMAzgIPewHQ+wHVOA8nVXt7oQ+3RM/1GRGZie5DXl9kYuaMTNIYjtJTH2A0YzAlL9kos1twtsplh52xOZwB1WfjwS2hp/llsHmq7hqmxyB26mxD1oBSPDMRfu/E0c5axpsV/1ZrEpgRTzU+daz+0V+9fl3dgtqJ+KYB32aWNVRfN7zEpCV/7vfIQ9H1h+Kd962+Lq3MLdbUtK+bfH9tWpacNm744Ph7lmJXdsXg+uJQWeZMz04g+kSQh6nqXXUa2qwUWKJ8VBO11DvqXdG07cIO6SdJak5vTSeDmIhukD3iHsVU60bZq907JMlGFySn9VAmGLSCwQhk1jhzDfoQXWOK0Yg8m9Jxuikgun0NFVccPVSMPtdb8RVK6iT1CIL6sbAp6ufyczVzLbMcs1xz0/jGaCBQSukDTi0YF05zAINHd/kMgp0a/7lq/+RDYDm80n4PdvdaCquXT123avaMtY/dEMXZWMQG7H6ImC41771u/tNPHXqCzrcK5psN8m9Dafg3XciU+Emp0ZbDzqrfatrD7dYckY7oOz2iaMMjyLV8jWZs+h79Qf6g503NW7qPNKd0Pwk/6vVpxjS7Ary2KwZzxGg/bj9hZ+yqQkmvVCFsrPZOcr+iMxosdYYmAzG4LPQ266DbG8ElFvWawycnrzsycpMwXJCErjQVKkZYGG3UGzLBsKdYLPRLOlZrcVFyZ2oFFMCF9sBY8Jw9helT0hek70xn040BUdEbI0DwlFyHr7r3OEe/5LO5lBxbpUtJN0IEi8lFV526u1f2qhaEBQYBNdQDDKhkSS06Ctv7qsKCUZWeioCgANQaLXdSEOuQNEPU16pApfoJXfQMXQuNavcGBahkoJ0aaPcGBYiV9F7Uo1dQpxXYXKL68GCFY9hPgjK47SZUUoyYgOrRW5O3Xk5yEbsGfLMv/tfVc7Ht/XPYwvcqzD1Th07OZpbV31hRgfH4wkeeOLDlc5CFcPzN+LG7No7AtyxfOWzYInq+vTF+C7tN3aPT0CNKv4HWEVZiiTDl+nJrxFvNjNSPtFZ7/+GV6vn6y2vigvAPrwg7jkcVfoGuAcWh1ZqMBmdA9DSDvJtzDQZjyGRSF4G2GbVSt89XmeQH2NsVsEmYzvQdwakrIHkED6uAroFZ/Kwr1wA9I1V3ZXq0A7oimy6DX1bBRsyXvDCvC5P4pa6GTWNhw3Y8MGvaPWumz17Hhh6rmxH/Y7w3fiH+Sc2k3m+Yro5nH+/Y/aS67uPj2CZ1HyjEY5RpS31rfcSi0zf3X6Nv7c/KOEiCTBEuISWMgoeRYcwNxqgtmlWfWw9sutn4k/knq2WwvsQxOKckv1Zf7ajNqc7v0fU6NQ+A5tXq9No8nT7b4HDaC/Q6p4N1ZVLpP6BKvyrkBrMqIB1aXRLm5CWFP5iVhP0jyUUg2b2q+p7CUXr7jdkUGDQFlOxau+By83m52pDHRWktud0ez6b+uD9QvlPRoJLMgMVddFnznE/pHrpLn+kjfO/51Pbdp8WROji183ZJF1FF95dDNxqoPXjFqal+rnGubW7W7NxZ4bmFKr+cnMPZp71LQX2lhNcJrpPNQIIyqHvrFRy8HVeJvpz6+WVZVv2K7o/umobx8ddbsTCk+cim+N//fOneptkPrJsz896a7IH29ICjf/CmR587sOlDrMWe5x++dO3Rw/Mquh4wkHufefyJXz/d9jjw9m1wov7MhtRvJvspXmYgbCkDwdzcByYzH8IyV8QRbp/47rNJyxjEseJC6jDYSs17CG/Tk1fsZvQUXvof9RxW84CqP79ibVw3coLknFQaBptrzTO1y8X14jPcM+Iuwy7rAdTFHDB0ml+0vo7eMXdbzRFrvTaqn2Ieb22y8m5uqWOH83PTFzZujpV+Xia4LH5voZd4FYsj4t1l5EwBOUAClPcmyAnsKpLwWOkLqUdipE48tqMNtvFOHDhgYmWWsLSaF6qxu+x616mxFmzxZOFTaGn6KZ078/1XruD8BfV7jN5wy3nQhi3nwi2ws7dcYY3B9tzSd8teBkwD/xZdXnM4eY6qMq0RmzQTh1+/3Dxv5/M/Y+ndL3B6/KPvn/uA3HTX+DGzmyeOW4AnpE+oa7t0B9Z+9AU2x3fHl8Tnxx87xKSt23rHfQ+sboWVjpELFNBX3PvIgTqV4gEszmNlk2yOsq0uTmSPu4jdYSY2i8NssBqRyWClv/GwSaJRi6doE1qipfKv4bHZ6MAJB3aoNjH9KUgP/WWI1aaRSirFsWKdyIg5pkLzFDMxd2JW0RusIWKbgtoc3Q7ioPsRiLjD7VzWReYmXYcw+A70S9lLjRXnG91Jx4GeRkKohKi8mB5GpawZa4n6FULKXbDbS+xBkJyg67HyHUuWLQoNG3JN6Xvvxc8+xobq1qyakPmaqXxc7eeXDjEjqe5V7wPU7z8FtLQLSfSLT7DCFalOIq1STOqWTkrfS5xfapJWSm2QwTG8gMA7BCtDUb/zZFAjgclyvMBqiBDCqjBIgcwI6xYrk4s+fPnrz0p1Cr8cRMIEFob7hP3BpLCzBzEbv/TzKDb086eXGpPfhrJR0JEOtFNxCVandbI4R2Q7WQyejalarDZ+Y+J4SnqfWTDoeZ1WCwYQwSEHUuTMyD7wfqER0E0wLkdGZmSzq81Fml09LvK9C7s02pDOQH0RvV6nbveA0qbDPaA/3c7U+MG3uMKVWxi+oGb8cv562XYKXOkomFWHIp3Y2Wj8bOa48pGLw9SP2/h+4yNj/ST9uZkD61a1x/2wPbw4bM6qO9B7cRjmk+CvhmBdS6hekW4md5CNhIH1hXM7pqjfGNx0SJQ4jHQSOoIbgH+YNCp6DrF+WIcxlmXdmsN4N25DfecnF/qcn/ON59TDIxgiL5QOyCwrYULxs4/8fj4mRWfY4Obhicy31yDTeWizPnGWNcAYDEhGDyq1yzTrNLvxXoF+ZXhIeksS681RR9RT759tnuOY45ntF8tJOT9AGqAfSUbyw6Ua/W7pt+Rt/jXpNf0n5DP+A+kDvdnkkl1EZUEW6AnXLlHvNxYaiZGqG+MuxPlOjWUx68mwndK6A5c1Rp++SOmJ5O7ciIudDrNJSFqkZQOcGWCom01JlTHAbAqFSPGHyzZtXvrhR/GLEJfUOXyRsSVJwHVvfzE+Jd50cCseiXfhXx/c+k3VxFvj8LysVE28hd7bv1wFfBgNNPACDXLRJ0rxWvvbdnJH2sY0sot5htttO8gc5g7aPnV97hYdNny/434nCWj0oEWdVkfArzfpNJ04U9GN1WNFv0lP9HrQDJgoRr+10EqsdMrWXV4O9Gf9FfqzWNWf2fqYrltHdDqH6dRK/yb/Tv8+/3E/5z8tnBqbiTM9Yccp51JQre68f1GrwGF6E6xq1zNqpF4Mq75u8hvK5L/G5HkquuJjJrDiyhypfVDIGkJAmTjsNvUymN45jsYm/cJx1y9dOH5ArX/hsoaRI2Zp473eW1+9/cRds99fsS3+1Xtvxi/i1YE581c1z7vT/iUz9/pRDTOa8lfvvGHVLeteXuQ9uvrleA/9AX0JrGUd0NWHK5UpB1wHwal9h33TddJ10n3SIw7zDksb5qt3P8o+7NrL7koTeY+Mcvgyzwh2mGuYe5hHzHRlujM9jCPE1rPrXI95H0t7zLc3ba9PtFB/Vvb1993mW+Xb7PvIJ6rOrsNmj/iISWf0mUCa1Y/TFXqrAuYF0Bt1kic6CNYZ6U8Yg35dIVCeske3y8pJpxwOPJZqDr/xlGkpcaf3UTxJ8AqqElLEBoMZ1BrQFizmcPIexgeGi7mcjqHdqALFYCpnRVM5J5oBmsuv/vBMK3ndsPtaMf0f1lJ8ogff9LPRY8ibOI3SIPgSp1PfMsAqMAcGWFTjJsW2AZng0tI7fJ4FXay7lG1q++6l8KCZ0YY5YvxrNxbf+OSna0eXxC9c68Bc/OeHsPTZ/srrJ900c94daV+/8+0L0zumVZ2vC6HmIzDp64FPLm4X0qI19LzwtNLfaI5otB7tIHagZgRXr92rfUn7rvYTrSagxVpGQH5toZYUaiu1Y7WMltJQe5i0IQY/d4geJwqiTuzEDR2F6sVXk2IgYxnMePQYuXUpslZQmo4+39irqivTOZWkOCWvjXSTsPOEOAMWS9n1zMtLL9yN438Tzr3BPoG5/1oSHxW3voKLyLJ/9N1xq3vaBPX7VSWX7mhcHUdauRjXzZ3kvk/+kGEl1wYZXPLjH8KEMOrbu5Cb/Ze9K7Vbpa7EU79b8Ce+Jlu4x5EbvavkyggMdk2ucZBhlCFqFNx25GIcduS0WG3YaSE27GIkQSPowEzGihE525wxJ9MEoNvJOMEwaLdj6sh0IDv9dQ148zqtVKgpROAbTAHNRE2HHBcTclom2SttO237bEyTrdW22XbS1mPjkM1kk21FNtbm9ixr6ztNqY2VTaiNDVa/jLEluunnx5eSXx+bzqt2xTn1VzlQ9QyVupKUXdGIwYiwUfVQ5uRTF7bmYGlJaZaZLO/WZqdlj3JNu/O65eVa6e67sYcNnY5PvCec5v00r2Tc8P4P4xOn338qvh7oMwn8/ErYu93oT8q4BmPUEnXMMc61zHXc5brdvY1s071hesP1sekj1zf8N+I31m/sP/HWgdaB9lGWUY4aV1Q3VycMspQ5ylzMUm6pcS23xrjevcey29FlOeiQDKqf7o0Y1GsoW8RQoqc57vSIPvVFgP4wZoG1ixWLWYsUqIoUqIdKNoP5ehiD4QtFslPANBcHUKGeJvRJd94rBGxXfaxB3ffw+XNhejHTeCac/G4RYPK2DxyR1BWMap+WcakjEzBfHWz/+F8N08fOvWvlzXWzgMnh8+9+E/8rdpx75UvyXfGEiVv2HnvshgWFL72CwX7CAs7aTW2z62EPykvZ+MeUwfO0S8S14jb3bm63+Ixhr7XLcNB8zNptPmHV27kB5mrTcscB8p7ppE04gk4AumrUm7wyqBUq1ulJw17vDxSCYa+oRn2lhBUw8BIpo35f0qhXMvxsIexKStKi55KW/FiwiDxZrlOWKyz61Mbzny36xpRDnaSMAPaQRd1SUnS50oNmjfEezcRh0TtMcx+L/Rz/6cQf43/Gef+9+7PeJ1aMGzMHLPpmdkL6xLq23jvj5z/4U7wHR/F6/CCeceTSN+sfXr5x0+qViEPRxOs8vfvT0v+tB4XRYNSujPGnLUjDaR/60m0+X7pPl8bb0mV/JD+tyBc8M/CHojO+cK50xvSD64w/ncHoGtM15Br6UVsI94Rw6EawIPNxTz7Ov9Eo+2Uid2JJ8SEe9/CYv9G2DyWNxRvHoin0hKFi9LC+m8/Glgs0NKYS6jdujdRmpCQrPAORumOo9x40or94UL8wtEYGJFU5rLwyW9KJLVUPowUeB3EJ/l/Ko88/tON5Gj4LuPML3LLsLsh3B3DFSUb/Tvz481u3/VLoCkAhxGzd6y8dewPCW5v6Z2b237SpKCuz/8WzvPbn215/6aXX33jppTfVrE1qMRLofz9dBOvaBOs6j7yidPNmPihmO83O4HbLdtu27IfzJMFWYyOWI/ouw5uBL4M/6S9k8Ln6SfqZ+oe12yy7M7p0QlVQyawOzc6YEVprWWtbk3FvplQWGs7XaEfpxxprAkMzhIzM7FCZrjRAv8cuzRR4DWeWAi59ti4jIyMoZGYo+Yt0y2y322/LXZK3zr4q7xH7w3kvZrwY1LfiTc77XDvynsmL5fPOgEMJBCMOJc0f8TvwF+CmlYiBuqxNWSRLcfkiWZ589UdPsEfU5eOifFwIrE4PFJmwqQRUQup+IvlltKYyuUPQDw/d4WWdlNOXYBdXz6xTvoH6dR69jTiHUnt7KY8xjx04lDEgUBOYiKPOGXiu8wLWYCdhPYEMkmPV60iOZwpYwDU52joP9tRYBfDz4B898ugLjS1e+gn8b+kpDXjmKsxQfyKQSd9Pd/gzk+9uj/queCFxsx4PyKjJ2K5/KOO1jA8y+ECGTs+yHpQ6B0Il9ESow1lQiVMHhup7RlZE/erf50mLIJz87p9twq24BzMIm9RfAbBqTasDasJ2Nhr0zRS2B9QGTMGhQNOOEqcC7ToVaNSplJZFnPROwqlk5UIE7RqdfvX4n3VO8ijglxk9uM6T8JDU5NUfAqgP/UK8sYV+K74w+ZokRsqASh0awNOYtLwyE28rktZSacyBCOjw3UF9uc6mK6fJdh39LcC3+7XlKHWdGr183pBcQ9mZ6lf99A7oyo/6nanlVoQ9lvnTby3LstlHxp+7YcWnX376QU78R/OUhgVFcloIvxxtOP/9J724MDx+Uk5aoWy3mWuH1O/YcPSBjf2HDPU7gun2tFmjatf86r2Y+qvjX1HfFmwVB+ipsBH7cTk97DMNxUPNf8T/wJLAObhM0mCeY+YwJlab2WJlbAQbVXeXESSNxmbXOBDSakKipLq7Ek5IWPpP7i6yhRz2y36uHffYsf1qP/ffOLlJN/dcebnZqV6kiH3npOakg3vV/ZgZP7vu2NTHxvriZ+Vx19TML4mf5Q73frlzRPO6Tb1bSP/dk0ur16/p/Q4m/X8Ad/FAgmVuZHN0cmVhbSAKZW5kb2JqIAoyNSAwIG9iaiAKPDwgCi9MZW5ndGggMjQyIAovRmlsdGVyIFsgL0ZsYXRlRGVjb2RlIF0gCj4+IApzdHJlYW0KeJxtUMtqxDAMvPsrdNzSgu3dXgIh0GYvOfRBQz/AsZVgSGzjOIf9+1pOulCowRIaSaOReNtdO2cT8M/odY8JRutMxNVvUSMMOFkH8gzG6nRExepFBeC5ub+tCZfOjR7qmvGvnFxTvMHphd71sVWzHaJ9evWzeQD+EQ1G6yY4fbd9jvsthBkXdAkENA0YHBlv31R4VwsC/4eklMhDhTe4BqUxKjch1BfZZFM1gM78zbHnvWMY97C+nKmUjBDZZUAQIHZAEFDI5A5IAqqDnYCqYXnILx3No4vclestxrxUOVvZh2Rbh/fLBh9IJX32A3QseX9lbmRzdHJlYW0gCmVuZG9iaiAKMjggMCBvYmogCjw8IAovTGVuZ3RoIDYxMDcgCi9MZW5ndGgxIDEyMDY4IAovRmlsdGVyIFsgL0ZsYXRlRGVjb2RlIF0gCj4+IApzdHJlYW0KeJzlmHlclFX3wM99ntmH2ZBlcFgGx0FwYEBQFDUZ2UTJBWFsxhVkEd80CETTImnVSLO0fdH2jcqHUQvN0spWs9VWW2zPkqTFFg3md+5zBkKr36fP+3k/7++P38CZ7z3nnnvufc45c1mAAYABWkGEGdPL0jNHnV3rBWB1aJ1WtbSy4fDl3ctx/AHK2Krly+zpk0dNxvnfAVSa2oZFS+/4Ju8hAI0CQDto0ZKVtb2rmlIAkmMB1Jq6msrqWV2x36N/Bq7PrkODYb2YjHon6kPrli47T3ECzkfdinrYkvqqSsUYlQf1oVxfWnleQ9hP0e2o8/3t51Qurfn99Y0rAZIOoq2wob5pWdAGawAsfL29obGm4fnuZW7UxwBE3oo2hs/FX2GgALSDCslA6M7szgkGAboziKb9aFXJngKuwJwojDj2QQ5cC3k42swuEOKF3eKlfA6Uuz6pvfaamAWm8cchRiMve/zbC17mfHWZ9tDJ93vWa4+qn0VfLcYLvRRGYTcoQaO8SZmFB4sniq/BLgE0IJjUWlGhEAUFnPaaWma3g52fVAW9wPapNwtJ8pMB6brbT75/4nLtUflkA1+MzsrsYIYnQI0nMUM6VGAyqjA3PA+g3gzQu2nAmovw61Zohx2wC56Cl+BN+JHpcM1lsAc+g2/gBziJYdUsksWylNNP+u+/ei9RLgWDuBcfKRogeCJ4pPeB4BE8vHGAZRNq0YqkPyzB8GDX6bbeTb2dva+o9GCW15qF/WjtZl3BE0Iu14PZXBfW8LG8olu9uXdr75ZTjtMAjdAM58FKWIXd2QIXwmq4BC7HTlsLV2AuVuP4SlgH6+Eq2ABXwzWwETZhp1wH18MNcCPcBDfDLZjH22AzbAnNcX0zfl0vz/KZO+FeeAAeQt4Fd8M9cB/cj/qDmP2H4BG0kYX0h9FyO9yB1nvRyr24bSt+SdABAdgG27FmpPdpnbAXHoXHkDuxmo/DbuyCJ7GOe7GyT8s2bunT/96T3p+BffAsPAfPwwvwInbGfngZDsAr8Oq/NfNsv4Vrr8Hr8Ab22kF4C96Gd+A9OAQfwcdwGD7Frjv6p/l30eN99Pkw5PUJen0BR9CzCz3Jj3w+kGe/liMcxLWH4XOmgeNMgJMQxBGv3vVyhW6S68irx6tzt5xnXo+tqPMK3ddfm4cxxw9jPbnGxzeHqvEI+nZgBvvy99dZeyVUHcr3bvThueAzB0K5eD5UCR7nyf61++W5gLzu6f6of2SUnvCtAdn5YEAOv4Av5cxQ9mj2j+xxj8/Rh2eZxzg1t5/iWso+X8vtA9fwufdRP4K3w1HMNOe3ciW+ha/6x1+F5rvgOzgGx+X3bvge75Mf4SfUf0ZLN2p/tp5u+QW/foXf4ARW8HfoGaD1nDbTg9djEG8rxgQmQu8foz+ssiiYkqnwTtMwLdOxMGZgRmZiZrScOqPvn7H8aSbsL+a0siWcDWIReF9GMysbzGx4b8axeJbAEtmQAXMx/TN2nHGwocwZmouSV8b0r01Aj+gBviksg63Adxdzs3Qcj2Aj2Sg2muWgJQ31TNTH4lyGzDyYAQthCZxQfi28jPEj8Fbp8BQtmD9v7pzZfp+3vGxm6Yzp06aeWTJlcvGkosKC/LyJntwJZ4wfNzZnzOjsUenutNTkJOdQx5AEa4TFbDLodVqNWqXEH1wMUgsdRRV2KalCUiQ5iovTuO6oREPlAEOFZEdT0ak+kr1CdrOf6ulBz9rTPD3k6en3ZGb7eBiflmovdNilAwUOeyebXerD8foCh98udcnjqfJYkSQrBlQSE3GFvdBaV2CXWIW9UCpaXtdWWFGA8Tr0unxHfo0uLRU6dHoc6nEkJTsaOljyBCYPhOTCsR34Y9vAt5VEZ2FltTSj1FdYYEtM9Ms2yJdjSap8SS3Hsi/mZ4Yr7R2pe9vWdZphYYUrrNpRXTnXJ4mVuKhNLGxrWyNZXFKKo0BKWfW5FR+5Rkp1FBRKLgcGK5nZvwGTlE6zw952HPDwjq6jp1oqQxaV03wc+JA/Yn+acL5vDHg2PCE+X2IiP8uVnR5YiIrUWuoj3Q4LbQHwpLv8klDBZ/b2zUR6+Uxr30z/8gpHIi9VYUXoe3mdVWpdaE9LxezL3078xnm7JCZVLKyq46ysaXMUFFDeyn2SpwAHnsrQsxZ2ZKSjf2UFPsRinoZSn5TuaJAiHHnkgAY7r8HiMp+8JLRMisiXoKIqtEpKLyzg57IXtlUU0AF5LEepbydkBQ93jLTbtmXBSPDzc0hR+ViUpMI2X3WtlFBhq8b+rLX7bImSx4/p8zt8NX5eJYdZSjmM2yXKO8qr8NlO8+5z5k+udmrsPsEm+nm10GAvwjdH3nicMGO5ZJVXNG+83cds0OeGu4Q8+OiUOKiIzvxiPiXypfnFtkR/Ir3+lyPZQmdSOiXNgFhmNPSfifb526ORNz9Qir2wpmDAAU8JqgwdMBTtr88p8FyENsYVGl7O4r4p0YmfXLQJGEY28Spa7RLMsPscNQ6/A3vIM8PHn43nWq5vSZmjpHS2T652qEvKT9FofgxpEiTidJ8i5GMPFrlsfWWV9Umy3q8WnzY9uW/a3qZxlJS18eCOUECw4ycIH1qVNLnyyjHhI/GjWYS3m6Oo0mE324vaKjuDrQvbOjyetobCirqxPIZjcnWbo8w33iafdaavxbaKbxUOJaykPC8tFe+evA4HW1va4WFry2b7duLvsva15b6AwIT8ijx/x1Cc8+20A3hkq8Ct3MgVO1d4pJmoaGR/204PQKs8q5ANsl7VyUC2afpsDKo6BbKZ+2wC2hRk88g2/sIiWeswxXjdFtqreXku8Ne1Vfj5hwuisJT4zSTmmACS4JjQwQRVmKRz1ORJekcet+dyey7ZVdyuxsbAn4WYHH4ntVU48J7ChvKBjVErijykvTMYLPclHrB1+ROx1eaizPZJWhfe/UrnFPSbxKUCzZOk1qpKfg7w+vhatXNylR/bti8gukyWtBhBG4qAHkXyGt6OuKgKa4MFlNe3oiK1+iW/i2/qW+yX29ksQbFjLJadYiqT+Ebp/rZwR6b82cSPgs65hkOLZ4MyH1lsqOJmfkqSOgxPXuXAqaoKO2ZbAVVl2Op0l+psZKnBK1GRVCOLzhaaBP5YolNv0ElaNwbEbz7Wu/lHUulU+/10eFlbE3LAvc2SHk+UNCCVoQWYHZyazM+C32vwqNz1KR6mtBNmOs7Dm4UfWo6kxmnJ4JxciZc/rdejxTGmb7GG3xH6UIx9ZFXzJw/DvIvO8s7gfY6ViQNeaakO/sOBNybYdmJjg7/tdIM0x5WWqjndapDNbW0aw18voHxpDP1EI4T+bwBBK/6tB3/62xo6tGKn8FsgPi6hU/g1EO9C/BKIT0X8TDhO+InmfiTtB8L3hG7CMcJ35NlFOErGbwnfEI4QviZ8RfiS8AXh80C8FvEZaZ8SPgnEhSMOB+JiEB8H4tIRHxE+JHxAOEQu75P2HuFdwjuEtwlvEQ4S3iS8QXid8BrhVcIrdIgDhJcJ+wkv0bYvkucLhOcJzxGeJewjPEN4mvAUYS9hD8V8kvAEGXcTHifsIuwkdBIeIzxK2EHYTthGCBA6ArGZCImwNRCbhXiE8DDhIUI74cFA7AjEA4T7ad19hHsJ9xDuJtxFuJOW30G4nbCFsJlwG+FWCn0L4WZafhPhRsINhOsJ19G6awmbCBsJ1xCuJmwgXEWh19PydYQrCW2EKwhracEawuWEywiXEi4hXBywjURcRGglrCZcSGghXEA4n7CKsJJwHmEFYTmhmbCM0ERoJJxLaCDUBwaPQpxDWEpYQjib8C/CYkIdYRGhllBDqCZUERYSKgkVhAWE+YR5hLmEOYTZBH8gZjTCRziLMIvgJZQTyggzCaWEGYTphGmEqYQzCSWEKYTJhGLCJEIRoZBQQMgn5BEmEjyEXMIEwhmE8YRxhLGEnIA1BzGGMJqQTRhFGEnIImQSRhAyZIgsYHWjlk5GNyGNkEpwEYYTUgjJhGGEJIIzED0OMZTgCETzhh4SiB6LSCSjnZBAiCfEEWIJNsJgQgzBSogmRBEiaYcI2mEQGcMJFoKZYCIYCQZCGEFP0BG0FFNDUJNRRVASFASRIBAYAWSwIKGX0EP4nXCScILwG+FXwi/ytuxn+YnYcTL+RPiR8APhe0I34RjhO0IX4SjhW8I3hCOErwlf0X5fBqIciC8InweisMHYZ4RPA1FjEJ8QDgei8hEfB6IKEB8RPiR8EIgqRBwKRBUh3ie8R3iXQr9DeJuCvUXBDhLeJLxBwV6nda8RXiW8QjhAeJmwn9a9RKFfJLxAh3+e8Bzt92wgKg+xjxY8Qxs9Tad+ioLtJewhPEl4grCb8DhhF4XeSaE7KfRjFPpRwg7CdtpoGyFA6KBtJcJWwiMU+mHCQ4R2woOEBwKReO+y+wORExH3Ee4NRE5F3BOInIa4OxA5HXFXIHIm4s5ApAdxB7ncTi5byGUzudxGc7eS5y2k3UyeNxFupAU3EK4PRM5AXEfLryVsImykI11DnleT5wbCVYHIUsR68lxHuJLQFojwIa4IRPgRawMRcxFrAhHzEJcHIqYgLgtEzEFcSnOXkOfF5HKRZyuy21SYcMxYnHA4bFrC0yhPoexF2aOflRBA6UCRULaiPILyMMpDKO0oD6I8gHI/yn0o96Lcg3I3yl0od6LcgXI7yhaUzbq6hJtRbkK5EeUGlOtRrkO5FmUTykaUa1Cu1tYlbEC5CmU9yjqUiVrhd+EEzIIE4SSyDhLY6sAg/nG8MBDOW2sZoSlg4a3VSDiX0ECoJ5xDWEpYQjib8C/CeMK4gJljLCGHMIYwmpBNGEUYScgiZAZMvE9HEDII4QQLwUwwEYwEQwCL0snCCHqCjqAlaAjqgIGXWuWZg/wOpQvlKMq3KN+gHMFyfozyEcqHKB+gHEJ5H+U9LMu7KO+gPInyBMpulMdRdqHchqW4FaWTtVKmVwUsvOVXUnLOI6wgLCc0E/IJeZSHiQQPIZcwgXAGPXIkIYIwiGOnKIpCwJNw95OiANtR9qGIItBZzieUUdVn0slKCTMI0wnTCFMJZxJKCFMIkwnFhEmEIkIhoYAwhJBIh7cTEgjxhDhCLMFGGEyIIVjpMaMJUZ5bkD0ov6OcRDmB8hsW+FeUX1B+RjmO8hPKj1jVH1C+R/kK5UuUL1A+R/kM5VOUT7C6B1BeRtmP8hLKiygvoDyP8hzKsyj7UJ5B6UR5DCv+KMoOlO0o21Bu4dUXeijHLYQLCIsDFvxViNURFlFaagk1hGpCFWEhoZJQQVhAmE+YR5hLmEOYTfATfISzCLMIXkI5IZ3gplSnEVIJLsJwQgohmTCMkERwUm2GEhwEJUFBEAkCgdEnEjx3IoMovShfY2LfRnkL5SDKmyhvoLyO8hrKqyivYKJ3olwmOhMuFd0JlzB3wsXFrd6L2lu9q4tbvBe2t3j1LeNaSlpEfYsNcX5Le8uhFtUFxau857ev8ipWRawSdCuLV3jPa1/h1a9gYcuLm73lzZ83/9QsRjSXN1c3L2u+tvkgGtR3N29v3tcsdgb3esKbx4wram2+ulmIwHkBmpmJmxOb9caiZcWN3qb2Rq+icWSjMO6nRna4kQkZjWxGY0WjgF7bGocmF3HvUY1Rg4vMjRmNnkbx3OJ6b0N7vXd6fX396vot9XvqlavrN9QLW3EkeOq1hqJzipd6P17KYLcQBDPKXiEYEHX1jwu9wOCY0OsJsrMxAf/CRCx2L/LWtS/y1rqrvTXt1d4q90JvpbvCu8A9zzu/fZ53rnu2d077bK/f7fOehf6z3OVeb3u5t8xd6p3ZXuqd7p7mnYb2qe4S75ntJd4p7mLv5PZi74xiNsld5C0UsxPwJwjE43dDfGt8d7xCXxHXECc0xB2O644TG2K7Y4XVNmYavHrwhsGiCd8EeotJiNkQsyVma4zSJA/EsIbw1nChwdJqETIsHstrlsMWBVhutwimDaYtpq0mcbppgemYKWhSbDWxrcY9xleN4nTjAmO9UTQZuS6aPUb3iCKTIcHgmZRuEMenG3IN0w3iBgPzGNyZRR7D0GFFuWHTwxaEiVvCmCcsKaXomC6oEzw6nDimDWqFoJaByOyMATMjRA2vEYtMKMJ+3BbFlAx/tegoL3O5SjrVwZklkmbGHImtlZxl/N1TOltSrZXAO3uOr4Oxq/wdTMgvlyL4P5Zl/bL16yEvrkSKK/NJt8f5S6RWHHj4IIgDiOuIgjy/a35Tc1PTMleTC99Q5jehZVkzfstg+I5sXsZnljUBurj+5sU9mjiaZaem5gXNGAMn0Nwkm7k2X3b5uxj/1dffPsl/48X+Lzf///2yLpgPABqoDf3PVoRB/f+/VeB4UGiswlES/y+vQouWJMgJjQUwwoLQWET70tBYgeNNobEKxzsn8leBK79yyeKFjYtT8+qXVP8zE0zs/yoAF+RDJSyBxbAQGvE9FfKgHvVqmAk1sAiacVyJM/9szX/SC19KgN4m8ZDSiM+txgxNhWlQvhsM7DaIhrFs//aCAk2a+klUBbCz/Zh1xm7zDFIIBpst1zFKtU4stUzOVa8TyiG356MPn8O3A+E56QdY+oddb3eZe56z5KR3HewakcEsiRZZIoyCWq1SOYa4hVHDkrKzsjInCKNGJjmGGAXZNjJ79AQxKzNeECP6LBMErjPx0O/TxcKeocLKxHFlI5TM5YxOGKTRiAnxBmeW3VQy1ZGdPFip0KhEpUY9LDvP4V0xZcgrOuuw2LhhVh0yLhbZ87TSeOIHpfHkWYqCk7uFr3N8E4aqVhr0glKruS05PnLoiNgzSgwmg9Joix4cq9ZYjLrhxZU9Nw12Rut00c7BsU4ey9kzDnvKh823W+mECSycX95en8eoSGUKF9OOZdocpvfgz4NHTcJU8LCoTuG7R7Oc+AU5u4TvQB/8xqPjU3pmEvXD8Re7Ry1jcuz2HFtn8OttejaVc7vexM604ZzHkBWlcpeZc6zcksN/LfOoysGaO3hqlyszNzwnp8vlwlTzAUtfMH+eq+tATk56uhkzP88F8+fNY/Pm2bBuA0+HpzKJ/8mdscq4dWg3v5PXLzFUwNFY1FEj3cIfZZ6g4kWNlNshkZd7t1Jn1vVYo+wRWpU5JuJw/ky3JTJlwvBxcwrdBq1BoxRVupj8hcs9NTdUj7Ce2dZ4A+vVWcJUZ8elDNZrolMdielOR2R3UdOCGUMTx6XGxDsTwmLTh0QnRFusToc1a05Lce6q9e3n3hIWk4LdnBM8Ir6NtRuEH5DnqXo7IS1UrzQWsUu4DiB4kIoEmCmwP44mHdj+nKVyj9FU5rSi1Smnycl/N/YoZ4XS5MpFYfg5kAvDK2LzhOMeWjvT6pgQwQQe/6/S/48CY1R8YVg/O+0joxzpVvCMR0bEoz5BIb6dee6OSy57pDYl69wdF1+2tTa59xddZELqmCHjpqaFR6VPGTlsfFr8ILWw7pYT0vw57b/cevNJmQ/MXV9X7ArPaXzw3LYdZ7tiMs+svhD7/1rsf0kZDW7YEep/gzaFaZOZZhhj4SyD/6qqxfx5MpgIKZ3Cxm3xVr2lM/jRDjRaBoXjHzcerWNmisnM9Eoz/zOhv7fwoTJze/AecR3Yl9VzYETGAuxj3lfYxtaUZJaC+wzYiu/wT+LxFp1HcU5v0awsC95LSUlyuzrpDoq0yDeRpNIbtT3ZGqNepcTR969Fx1lUgsYYxqKUJuuwhKR0q+ZNrUmvrI4dxm8J+cbRi1Oa9ErL8CRrQpRRs12hFJmoDtOefFNvHYb9l4f9twf7LwOv43f7+q8guPcx3m8FLG5Ep3DNNjAaoTN4eEATjusUVj7m9Mjd4MG/obbZ7ersvo7Jxj+xPNrhZVZ52ionQN3XLNgreDN38UbcxxsxB2+HTLkZd+B+JhEvq26PDlvtj+AeLY9uRts/3oL2cNFVwBsSG9Ct6GtIftmPDl3+ZFIoQ7cC9Wi2uMecNr1l2yqXt2hElE7UGrRhqbkzRpQvKx4iuC8uX3T17JSxjQ82zF5TOdFp6j1pzSjOSC9IixqUkpc+dpHw9PQH7ty41BMWHhGZPDQxOUptDDeOr718Spwru3bj3Mq7VuQNn1a/dnPm2VfPGpo4fuaIUaWjBjsgOvV/AF3YT0BlbmRzdHJlYW0gCmVuZG9iaiAKMzAgMCBvYmogCjw8IAovTGVuZ3RoIDIyNyAKL0ZpbHRlciBbIC9GbGF0ZURlY29kZSBdIAo+PiAKc3RyZWFtCnicVVBNi8MgEL37K+bYpQdNF3qSwOJectgPGvYHGB2D0KhMzKH/ftW0hQq+8fF843O4Gj6H4DPwX4pmxAzOB0u4xo0MwoSzD9CdwHqT76yhWXQCXszjbc24DMFFkJLxSxHXTDc4fNSljkpf/UT+DfgPWSQfZjj8qbHwcUvpiguGDAL6Hiw6xtWXTt96QeCv/qZ297ejxTVpg6TDjCBPogfpuh4w2FeNve+Oye203dpBiFKYPLtybiBEKWxv1kCIUljp+XDX9vXbz4xmIyrx22xa8prSB3yOL8VUQ9XN/gEdAnQqZW5kc3RyZWFtIAplbmRvYmogCjMzIDAgb2JqIAo8PCAKL0xlbmd0aCA2Nzc5IAovTGVuZ3RoMSAxNDgwMCAKL0ZpbHRlciBbIC9GbGF0ZURlY29kZSBdIAo+PiAKc3RyZWFtCnic1Zt5QJTV/v/PeWZgBoZhhlVwgBkcwQURF1Q0k5FNEFERpmZwYxfNhVjcUdJKQ23fF7PVipaHsRJbrWxfLNvubTG9t7p1y7K9LOX7fp7PfMz83dsfv+8f93sH3vN+fT5nmfOc5zmHQ4mQQgir6BQGMXNGRdao0ZkNNiHkSmSn1y2paT66+fAc8AfQxLrlbS71hn0HhFAihAhJamxesOTHH8vApsFChPVfsHhV49bJSU8JMeZ8IYzTmhpq6geOab8N/XnQfmwTEtb7DFMQv4B4YNOStpXx/eRkxAmIExYvq6sxfmusRDwCceSSmpXN0YPS0xB/idi1tGZJgy3mlvVCZLchF9m8rLWtzyEuFMJeoJU3tzQ0n/OAcgJxPbq3IydxXdorQhiF1qcZX4oQR11Hlx39pq/vd7J9YrvKFi2kCL6MkVC2uM5wve7TjTmiSu7Xi26V2XKrvFZJV9L1T8BUPHJ49OOffzDfNvEHkWjWKz36xdpXNN/fFvb+r8eObwn70vQwwjDt04OfkCEvFSHCHHJdyGh0k0JueF1cqGiDtIUoimI0KEZx2quswuUSHoz7m1BxQsh9pu08DorDd/x67NiOsC9/v5bgK1TPhEqXsItnhAkjsYss0SBE1KWYK6NWatouxIkr/tBqplgkWvF8dGKet4krxJPifVErNoKuEzvEneJuoYqnxIvi3dPH+b95nVgVskREGHZjzDFC9B3rO3LiTqg3JPKUzBWIYoyu3zN99r6vTst9deKKPvuJ3tBoEa63tSoHkP1OHu87puRqcd9YLVY2gW16i29M2088cGLnaXNQLqrEbDFHzBXVogbXXy+axELMzDlisVgilurRUpQtwHsjovmoVYdaGv9ea5lohlpEm2gXy/HVDG4NRlrZuXrcLlbga6VYJVaLNWKt6Ai+r9Aza1GyWo9XQuvEetyZ88QGndgps1GcLy7AXdskNouL/jS66CR1iS1iK+7zxeKSf8vb/hBdiq/LxOV4Hq4UV4mrxbV4Lm4QN56WvUbPXy+2i5vxzGhlVyFzs05a6WPiOfGQuF88IB7W57IOs0YzwvPSqM9hM+ZgLa5w4ykjpvlbcXK21uHatWvrCl7pSuQ3nNJieXAetZobUZN6ofug9dJx2kxcimsg/v2KKLpKv/7fs6fOyp9leT5uPGVmbtAjjU7P/ju+WtyEFXgL3rVZ1ehWMNHNOp+a336y7g49vk3cLu7AvdipEztl7gTvFHdhbd8jusW9+PqdTyXy+8V9+p1TRY8IiF3iQdzJh8Vu0avn/6zsX+V3BfOBk5k94hHxKJ6QJ8Re7DRP44szjyP3ZDC7T89R/DT2t316LYqeE89jh3pJvCxeEfvFs4he099fQPS6OCDeFO9KK+gN8Tnej4vXQz4WkWKytrFjnm8U88Q8z5T6+fPmzpld5fd5Kytmlc+cMb1sWunUkuIpRYUF+XmTPbmTzpx4xoTxOePGjskanjlscHraQPcAZ0JslN1mtYSHmU2hIdjNpRhW6C6qdqnp1aox3V1cnKnF7hokak5JVKsupIr+WEd1VevVXH+s6UHNxtNqeqim52RNaXdNFBMzh7kK3S711QK3q1dWlfvA2wrcfpd6ROcynY3pemBFkJqKFq7ChKYClyqrXYVq0fKmrsLqAvTXYwnPd+c3hGcOEz3hFqAFpA52N/fIwZOkDsrgwgk9+Flm1T5WNaQV1tSrM8t9hQWO1FS/nhP5el9qaL5q0vtyLdTGLLa4eobt7draaxe11RkR9e76mjk+1VCDRl2Gwq6uTWpUhjrEXaAOWf1xAi65QR3mLihUM9zorHTWyQ+Qakia3e3q+kFg8O4jX/4xUxPMhKbZfxAaapd4cppQziwwNowQ15eaqo1lS69H1CJQO8t9FLtErSMgPFkZflWp1kr2ckmcVyvp5JKTzavdqdqtKqwOfi9vSlA7a12ZwzD7+ncavlHuUg3p1bV1TZrXNHS5Cwpo3ip9qqcA4KkJXmthz4gs1K+pxkUs1Kah3KdmuZvVWHceVUDCpd2DhRU+vUmwmRqbr4rqumArNauwQBuXq7CruoAGqPXlLvftEaP7DvVkuxy7Rots4dfGocbn46akF3b56htVZ7WjHs9no8vnSFU9fkyf3+1r8Gt3yW1XhxzCx6Xqn6i3wrWdVpsra1duSjO7fIrD4NfuFhKuIry58yaiwI7bpYfaHc2b6PJJh+Bq+JRgDY3+0A8CQ1p+sVZk0JrmFztS/an0+pMhOYJjCklTzaf0ZUfi5Jjoc/7t0Ki2NqAhrsKGglMG+IdOQ4IDDPb2r8epaHMR/GC0MGu3s5iLDGlYucgp6EZPaXcxwaWKmS6fu8Htd+MZ8sz0ademzbV+f0sr3KXlVT79bgefkso/RFSeQ5EqUlHMgZKPZ7Aow8G3VY+n6PHJsPi04hIudnWZ3aUVXVrn7mCHwoUVhIsOTS+p2ZITnY2lWYTdzV1U43bZXUVdNb19nbVdPR5PV3NhddMErQ93SX2Xu8I30aGPdZavw7Fa+6hoUSpLK/Myh2Hvyetxy83lPR65uaLKtwe/ELg2V/oCilTyq/P8PQNR5tvjEsKjZxUtqyW1wKUFWk+zEJj1+o49HiE69VKjntDjul4p9JyZc1LU9SqUs3NOQc5IOY+e0164SQlNmGJst4Wueu32rPU3dVX7tcUl4nEr8S1V6Z4kVMU9qUcqoRFquLshT7W487R8rpbPpXyoljfhwZDxEpOj7Uld1W7sU3igfMIh6VE0aF26evv6Kn2przqO+FPxqM2BqnxqWAb2/pC0qag3RVM10lPUzroabRzC69PamtJK6vx4bLlDVClRw9BDWLAH1CjS22iPIxrV4d7gBurtOxGonX7Vn6F9qG+hX3+c7aoodk/Abac+Q9K1D8ryd0W7R+lrE0shPG2TZmEYm6jwUcaBEB/mp0kyRWDkdW4U1VW7MNtGUVeBR5320nAHZRqwJRrTG3SFO4KFQrssQ5rFGq6GDUeH+NbYMlxbkiFpJr+fBq9Hm4IV8Nl21YIRpZ8ylcEGmB0UlWhjwfcmDFWr+pTWTXmvmOVeiZ1FG7TekwnFqjWtpAabP7W3IOPO4cZmbY+wBPvYR1mTduURmHdDWmVv3073qtRTXpnD3NoPB+3BFI49eLCFv+v0hDo7I3OY+fSsVU93dZmt/7oBzZfZetK1pKsQPzVQkX7HFn0J2u/i/+8rEGZw9SrnPxiWIKcCNjJsYDiPoZNhPcM6hg6GtQxrGFYzrGJYybCCYTlDO0MbQyvDuQzNDMsYljIsYVjMcA7DIoaFDE0MCxgaGRoY6hnqGGoZahiqGeYzzGOYyzCHYTZDFYOfwcdwNsNZDF6GSoYKhlkM5QwzGWYwTGcoY5jGUMowlaGEoZhhCkMRQyFDAUM+Qx7DZAYPQy7DJIYzGSYynMEwgWE8Qw7DOIaxDGMYshlGM4xiGMkwgiGLYThDJsMwhgyGoQxDGAYzDGJIZ0hjGMjgZhjAkMrgYnAypDAkMyQxOBj6MyQyJDD0Y4hniGOIZYhhiGaIYrAz2BgiGawMEQwWhnCGMAYzg4khlCGEwchgYFAYJIMIguxjOMFwnOE3hl8ZjjH8wvAzw08MPzL8wPA9w3cM3zJ8w3CU4WuGrxiOMHzJ8AXDPxk+Z/iM4R8MnzJ8wvAxw98Z/sZwmOEQw0cMBxk+ZPiA4X2G9xj+yvAXhncZ3mF4m+EthjcZDjC8wfA6w36G1xheZXiF4WWGlxheZHiB4XmG5xieZdjH8AzD0wxPMexleJLhCYbHGR5jeJThEYY9DL0MuxkeZniI4UGGXQwBhh4GleEBhvsZ7mO4l6Gb4R6GuxnuYtjJcCfDHQy3M9zGcCvDLQw7GG5m2M5wE8ONDDcwXM9wHcO1DNcwXM1wFcOVDFcwXM5wGcOlDJcwXMywjWErwxaGLoaLGDYzbGK4kOECBj72SD72SD72SD72SD72SD72SD72SD72SD72SD72SD72SD72SD72SD72SD72SD72SD72SD72yBYGPv9IPv9IPv9IPv9IPv9IPv9IPv9IPv9IPv9IPv9IPv9IPv9IPv9IPv9IPv9IPv9IPv9IPv9IPv9IPv9IPv9IPv9IPv9IPv9IPv9IPv9IPv9IPv9IPv9IPv9IPv9IPv9IPvZIPvZIPvZIPu1IPu1IPu1IPu1IPu1IPu1IPu1IPu1IPu3I/F0a4NQcSJnkxJk5kBIH20DReYGUCbBOitaTrQukRMA6KFpLtoZsNdmqQPJk2MpAcj5sBdlysnYqa6OolayFkucGkvNgzWTLyJZSlSVki8nOCSQVwhaRLSRrIltA1hhIKoA1UFRPVkdWS1ZDVk02n2wetZtL0Ryy2WRVZH4yH9nZZGeReckqySrIZpGVk80km0E2nayMbBpZKdnUgKMEVkJWHHBMhU0hKwo4SmGFAcc0WAFZPlkelU2mdh6yXGo3iexMsolU8wyyCdR8PFkO2TiysWRjqLNsstHUyyiykWQjqLMssuHULpNsGFkG2VCyIWSDyQZR1+lkadTnQDI32QDqOpXMRe2cZClkyWRJZA6y/oH+02GJZAmB/jNg/cjiKRlHFkvJGLJosigqs5PZKBlJZiWLoDILWThZGJWZyUxkoYHEmbCQQGI5zEhmoKRCkSQTusk+shN6FXmcot/IfiU7RmW/UPQz2U9kP5L9EEiohH0fSKiAfUfRt2TfkB2lsq8p+orsCNmXVPYF2T8p+TnZZ2T/IPuUqnxC0ccU/Z2iv5EdJjtEZR+RHaTkh2QfkL1P9h5V+StFfyF7N9DvbNg7gX5nwd4me4uSb5IdIHuD7HWqsp/sNUq+SvYK2ctkL1GVF8leoOTzZM+RPUu2j+wZqvk0RU+R7SV7ksqeIHucko+RPUr2CNkesl6quZuih8keInuQbFcgPhcWCMTPhvWQqWQPkN1Pdh/ZvWTdZPcE4rFfy7upl7vIdlLZnWR3kN1OdhvZrWS3kO0gu5k620693ER2I5XdQHY92XVk11KDayi6muwqsiup7Arq5XKyy6jsUrJLyC4m20a2lWpuoaiL7CKyzWSbyC4MxNXALgjE1cLOJ9sYiGuEbSA7LxDnhXUG4rAZy/WBuLGwdWQd1HwttVtDtjoQVw9bRc1Xkq0gW07WTtZG1kpdt1Dzc8maA3F1sGXU2VKquYRsMdk5ZIvIFlK7JrIFNLJGat5AVk8168hqyWrIqsnmk82ji55LI5tDNpsuuoq69tMH+cjOpuGeRR/kpV4qySrIZpGVB2I9sJmBWO0TZgRitcd7eiB2I6wsEJsJm0ZVSsmmBmJxLpAlFBWTTaFkUSB2HawwELsJVhCIXQ/LD8R2wvIC0UWwyWQeslyySYFo/HyXZ1I0MRDlh51BNiEQpT0a48lyAlFTYOMCUT7Y2EBUFWwMlWWTjQ5EDYONopojA1HahY0IRGlrM4tsODXPpE8YRpZBnQ0lG0KdDSYbRJZOlhaI0mZpIJmb+hxAfaZSZy7qxUmWQu2SyZLIHGT9yRID9rmwhIB9HqxfwD4fFk8WRxZLFkMWTQ2iqIGdkjaySDIrWQTVtFDNcEqGkZnJTGShVDOEahopaSBTyCSZ8PTZap2aTtjqnMdt9c7fwL9Cx6BfkPsZuZ+gH6EfoO+R/w76FmXfID4KfQ19BR1B/kvoC5T9E/Hn0GfQP6BPIxc4P4lscn4M/R36G3QYuUPwj6CD0IeIP4C/D70H/RX6i/Uc57vWkc534G9bFzvfsqY734QOgN+wZjhfh/ZDr6H8VeResS5xvgx+Cfwi+AXrIufz1oXO56xNzmetC5z70PYZ9Pc09BTk6duL9yehJ6DHI851PhbR4nw0otX5SESbcw/UC+1G/mHoIZQ9iLJdyAWgHkiFHrCsct5vWe28z7LWea+lw9ltWee8B7obugvaCd0J3WHJdN4Ovw26FW1uge+wnOO8GbwdfBN0I/gG9HU9+roOfV2L3DXQ1dBV0JXQFdDlaHcZ+rs0fLrzkvAZzovDFzi3hd/h3Bq+03mBIc15viHHuVHmODd4O73ndXd613s7vOu6O7yWDmnpcHSUdqzp6O54v8MTHRq+1rvau6Z7tXeVd4V3ZfcK7yPKhaJRucAz0bu8u91rbI9tb2s3fN8uu9tlQbsc0S4V0W5vd7UbItq8Ld7W7havaJnZ0tmithjPUFsOtSiiRYb39u3d1eJIKYJ71rZY7UXnepd5m7uXeZc2LvEuwgAX5izwNnUv8Dbm1Hsbuuu9dTm13pqcau/8nLneed1zvXNyqryzu6u8/hyf92zUPyun0uvtrvRW5JR7Z3WXe2fkTPdOR74sp9Q7rbvUOzWn2FvSXeydklPkLcTFiyR7kivJYNcGMD0JIxEOmTfC4XEcchx1GIVDdex1GKJt/Z39lSG2RJk/I1EuS1yfeEmiwZawP0HxJAwZVmTrt7/fR/2+7meM8fQbMrxIxNvjXfGGOO3a4ssqi3TPLSAfOUa/1rJ4d3qRLU7a4pxxSqEzToqoQ1FHowxxT9r32xWbTdpsfTbFY0N1W6QzUtHe+iINnsiR44psVqdV0d76rIZ4jxUZrcdBETMri2wWp0Xx5lpmWBSPJTe/yGPJHFEkDNIlpZB2mMGsjULGOYuwrnfFyxCJn+c9lRUZGaW9ZjGrVDXPnK3KzWpahfbuKa9SQzerwls129cj5cX+HqnkV6qx2v/R1eMLtm0TecmlanKFT92R7C9VOwEeDfoAIrknXuT5M+a1trdmZLTNw9u81rYM/RuRbNeiDC2pfbe2Ida+2vVYZPzpi6rB5rfi1cbJtj9v9X/9Jf/TA/jvf/UI7R8hTO5Tzhf1ykZoA3Qe1Amth9ZBHdBaaA20GloFrYRWQMuhdqgNaoXOhZqhZdBSaAm0GDoHWgQthJqgBVAj1ADVQ3VQLVQDVUPzoXnQXGgONBuqgvyQDzobOgvyQpVQBTQLKodmQjOg6VAZNA0qhaZCJVAxNAUqggqhAigfyoMmQx4oF5oEnQlNhM6AJkDjoRxoHDQWGgNlQ6OhUdBIaASUBQ2HMqFhUAY0FBoCDYYGQelQGjQQckMDoFTIBTmhFCgZSoIcUH8oEUqA+kHxUBwUC8VA0VAUZIdsUCRkhSIgCxQOhUFmyASFQiGQcXIf3g2QAklIiHqJnDwBHYd+g36FjkG/QD9DP0E/Qj9A30PfQd9C30BHoa+hr6Aj0JfQF9A/oc+hz6B/QJ9Cn0AfQ3+H/gYdhg5BH0EHoQ+hD6D3ofegv0J/gd6F3oHeht6C3oQOQG9Ar0P7odegV6FXoJehl6AXoReg56HnoGehfdAz0NPQU9Be6EnoCehx6DHoUegRaA/UC+2GHoYegh6EdkEBqAdSoQeg+6H7oHuhbuge6G7oLmgndCd0B3Q7dBt0K3QLtAO6GdoO3QTdCN0AXQ9dB10LXQNdDV0FXQldAV0OXQZdCl0CXQxtg7ZCW6Au6CJoM7QJuhC6QNRP7pRY/xLrX2L9S6x/ifUvsf4l1r/E+pdY/xLrX2L9S6x/ifUvsf4l1r/E+pdY/xLrX7ZA2AMk9gCJPUBiD5DYAyT2AIk9QGIPkNgDJPYAiT1AYg+Q2AMk9gCJPUBiD5DYAyT2AIk9QGIPkNgDJPYAiT1AYg+Q2AMk9gCJPUBiD5DYAyT2AIk9QGIPkFj/EutfYv1LrH2JtS+x9iXWvsTal1j7EmtfYu1LrH2Jtf+f3of/y1/+//QA/stfCfPnCe3vcBqDf59iEJFCBNkIjgxyKChJ+5dFxjBkksTQICvIFwXZgHxlkI3g5iCHgrdO1l75Gfk1ixfWtiz8s0hMPvmVLzKgGrFYLBS1ogXvs0SDWCDakalB/Gc1/3/LhAgR4kSr4UBIJMZvEuNFmZguZj8mrFji8WKCfOihuIICc6bpCSxfRbiwAZiFlPkem1Gx7u7fP9e9e0zoNkNUSa/MfDDXtA0/2nKPHzz+Wtbxg0eix2cdkVkfHj542P7Na1Hjs0YffuvwyBEyKjVKV2ykYjLFhroHDFfGDEofO3r0qEnKmOx094BIRc9ljx03yTB6VIpiiOXMJEWLpeHAb1WGGcdDlXXu3LNGh6T0t8VaQ0OUpITozIlp9orZaROHJ5sMplBDiNk0eFzegNLFhQPeM0Ulx8UnR5vN0cnxcclRpuPvh0Qe+zYk8td84+JfrzSEnjEnd6Dh2nCzYgwN7U1JSBx6RmrJWbYYu9ESY4+KN5uioyIGF8w5fmFcktZHUlwc9XW8TH+Crus7ZthheEmMEld6bNF2iyyL0d5ys+XQmN6+o7vsSGhuC7pV9x93Rej+2S6tQa/82eNIsdhR12K3am+obQlHFUsCyi0o3y08CEWKvVeGesIzpw5NHFiSOC1kmsjNzY0en4vpzso4rD/qdrLxI0fMxS8taTyFUdnDAaGmqN8TNKlxcWPH6m7YYY52JSS6os0Jw0tGTFpbgDAxwRVjCo1O6hefbDdNu6asas201ESzxWw04k2xlc0rGOjzHt/CGcP5ZkuYwRBmMa/wzjiz8aJqYYnR/vbMsNvQP6RUDBKDxbhd4c5+Ax6V2t/3WeVcT0y/tP7vhXts/YrDne9Fv26QhsHFIvfg3FFH3so4Mkpm4dGJHo+LkfoDY8QlxJ18KvRxjx41Vqd4aejf5E+bMGLwoPT4wS6jNTHGHm8NrZ87dnruONfAtDh3WsJvn4YMWNQckzIgJWbCCJMlVDGERVnrlg8ZOXRMtt1iO7FGKO6+PjFdu6chpTg/6n+7JnDQxXgVUdV3xHAQ9zoGV3KXJyl3iBwcLYdEyXSrTI+Q6WaZbpJDDXKIIlO0W4vbmRK89fBD2q2Hf6Xd+pTgrU/pVcI9KVnhMjw2AdVjtRsf60LF2GjUitXufuwjOFeKvr27baKsGQNK1P5LmG2qu1cqPSFlIveIdvPnBm9+1lxy3Hx+ydMWkUl/ECKVuFht7iYphoMTWu9rWXbH0rHjW+9thY+73zFp0YyShQWpjtxFM4oXFbjkJ0v3XFiat+7BFvhU+NqSDbXjs+dvKJu6oWZ89rwN/wPAoXrFZW5kc3RyZWFtIAplbmRvYmogCjM1IDAgb2JqIAo8PCAKL0xlbmd0aCA3NDIgCi9GaWx0ZXIgWyAvRmxhdGVEZWNvZGUgXSAKPj4gCnN0cmVhbQp4nFWVy27bSBBF9/oKLjPIQlSrHjYgGMg4Gy8yE4wxH0BJLUNATAm0vMjfp0/fJJgxYJK6Xc2qOrfUWj8+fX6az7dh/XW5HJ7rbTid5+NS3y7vy6EO+/pynodNGY7nw+3np349vE7XYd02P39/u9XXp/l0GXa71fqftvh2W74PHz7x9+fHT8t5+vbHsP57OdblPL8MH/59fG6fn9+v12/1tc63YRweHoZjPa3Wj1+m61/Tax3W/93d1zY/816O9e06HeoyzS912OXhYdidtg9DnY//X1vlvbbsT/q8244ttl/Gsd2akAgpIZtQTu25X8ax3VrEhoiNIjYIBaFIKAh3CHcS7hACISQEwoQwSZgQDMEkGMIWYSthi+AILsER7hHuJdw3wY7tuV/Gsd2awKIpwnoEbZh6MXpxEriyOFmMvkzNGc0V2BQBKgCySkRVROUdVO0q3SndQeHi4fBwqnaV7pRu8DVBNiAbi6YI6xHUZCrMemHgNDE1mBoZTWmNtAZfE2QDsjEH/dKEA73wXCQUhAqbKkAVQEmCVJYkS7AYiggigppChQWFBThDTAOmRuem9o32DZ9NZhtmFzwqMqpglFO1q3SndAe4i7pDPbAk5EvgS7A99I7gHQG9EMIAYfKcEhIhMC3kXOAc3xFdxvHUe4FNCFAAKOAbghxATvpKNZc0lyymIrJHACtFLCFWyFiUtpC2wLcIcgFyIbpoS+lzuofYXsT28KBzV/tO+8521zu8G8U0hUYqGKmkr1RzSXPJYioiiQgShLJEz0IJrjqcOhzTXM45zjnbXe/w/g5QhHhE54GLKSsTKxMXU1YmVlZMq3Ku4lyBXhHCAsKgr1BzQXNJ56n2s7tPglCW6FkoMlVp9kqZSNdYOmNZmISicSh9HIgObYk+lqAo4lHgsaXIrSrdUmk/WVPz0W6rdsD+Okk5azn/fx/Xh/dlaSd5/5HohzhH9nmuv39HrpcrJzT/qx/b7n3aZW5kc3RyZWFtIAplbmRvYmogCjM4IDAgb2JqIAo8PCAKL0xlbmd0aCAyNTk0OCAKL0xlbmd0aDEgMzc3ODggCi9GaWx0ZXIgWyAvRmxhdGVEZWNvZGUgXSAKPj4gCnN0cmVhbQp4nKS8CXxTRf4APjPvzP1yNEmTNHlpmrQ0QKEtlEKlj6McYqGctkilyH3JUUBBlKLcoqK7orKugLqKB0toC5bCLoisB8rCet/gLiq6W+26LKtAk/93JkmBdff/39/nn3Rmvm9m8t7MfO/vzCvCCCETakAcqho1tqBwvDRnAEK8A2pHTp0/ZeEL9jmZAH+KEFamLlui7vR98Fdo34WQOG7Gwpnz31pesw0h/WW4vnXmvOUzxD/l1iPkH4zQ0upZ06dMM0zJ6ocQB79HvWdBha1I54XfT4PrnFnzl9w+9bXATrj33yANm7dg6pRd4vqboX8xtD8zf8rtC4XdwmnovwGu1VunzJ++ZPTlexDKugGuf1y4oH5JIh9tRcg6j7YvXDx9Yfj9ysNwvRkhw++hDsO86MeIeMxD6UEK1Ojb1fZwe5f2vu3XtU9sn9y+on1l+zftbe3/SCQQgrac9rz2KLQNgLYpV9osFxBSLJDsCFnaIL1n6WupsVyH6Bq6U4mOfBq68hmKKtEolI26oN5IQgKSkQ6ZYcUtyIBEpIfxGFEGykVZKIB8SEVe5Ec2lIdKURBZkQvZURT1RZnIAXf3oHw0EPVDGhqAuqJBKIIK4dnXozI0DIXRSFSOeqJuqABmWIGGoyJ0HYymOxqM+qMc1AuNEKEzX49GkOfR51AOJqXoVSjHQ5oCMG07DakM0lhIlVB3AEoPpAmpfuPF59GjUH9YLEVThQlokjAh0UF/K7yGHuf/gnpAuZMmaJ8P7U8CPAPS05CegPZdcI+t0P95Og5AiAW/hjZAWQV9t0JZAmMbALAO0hPQ53p43noo74FEy8HQn8JDoN9auE8O/CYIZStcr6SJ3geeXU3vAf0GQns5PFuEcgPUiTCGLtJ9KBPa1kEdndt6+P3voByeesYwuLZDWx5fn/hCykK34dcST/IvosU0EQc6BFh1ogu4D/GQSVxfbgv3Ot9buEHcKt0txeQ63Uh9V8MQ46emkaZzlv6KA77fWgfabrfr7W87ih1vZ0zIuOic4Hzc+aHzvFt0N2RO9EQ82zy/997uvZw1w39XYKRaqI5U3wo+k52ffSh0LkfMGZKzLbw/Esvr3aV7lHTr031NQXXBjz3dhXKRu+hisdprWu/eJaTkxT47+rSW3t/X03dk33V9X2TUD4gpKsrNdV0/2VL2T9krM5J88i+5+bQ8uXRK6OKejpkKko1wqWP92YfbgLdQWhW2CUVwG2+y5P6EZhCbLBCDyBP64dG/fcZVDlKRBhz0D+Gd+GhcJPXHjclxSP3jI9EgBV3cc3GFgq48KfUZw2rGoB+Aln8NfEKALwrQBKDJF/ksGAkRWlEmJI/wLOAuQjkt8TWkc7SMz06co+20JN/CfVpSCaFdaDeejXajw+gobodf7UEHUDN6HfhqMHocrUS/ROuBCydCzUZ4+hh40mD0S5yZaIan7wQ+2olOQN8b0V2oFTmxO/ENWoXWcu/Ar9YCD2cDH1ahBeg+fENiKZoE3HMPKkE3oFvRQtyQqE7cn3go8TT6DTrAvZ7oAH73oKnwPZH4Tvgw8Slw6iT0MHoMncYP6fYBT98IUvgA92u0GG3janmcmJm4CCMIottgDDxIkRP4CInC3aejr7Ebr+QGwV2eSsQSx6CXD9WiWWgbasW98FASFCYlKhMngEq7odvhro+hRrQfvi3od+hjbBTaE08n2kGmdAUpsQrW44/4CBfvWB0vp/QCq9QFpM9wmNfv0WvoFA7hl8kCwSgUCpqwIvEuSKKeaDyM9ln45Vf4X+Qu+K7iXuWHJAaCZFuLHqSrjf6AvsAeXIBH4QmkC1lAnuAWg+zrCr/tCfJxNqz3o3D3z3EU7ydGcpJ7in+BvyRmxc8kzICRCPoV0MHL2AQzVXE9vhu/j/9CBpHJ5Ffkz9wv+ef4t6UpMOub0Xx0H3oB/QvbcB88Gt+EZ+GVeD1+ED+GT+BT+BwZQMaRueR7bha3iPsdPxC+INv4e4R1wr3iuXh1/Fj8T/F/JQoT69BooIfVMPqH0RMwswPoJPoIvqfRn7GADdgMXxUH8Xh8B3zvwvfhJ/Eu/Bxuhqecwn/G3+Af8D/xJQKkS0TiJUGSDd8QWUxuI78kj5OT8D1F/kZ+4lxcNhflenFlXA23AEa1HiTIFm4f9wXv4U/yCVjnQmGrsF3YJbwgHBXaRaN0t4zkty4/1ZHf8XkcxTfEt8Yb482JL0BvZAJN+UBzlMHop8B3DuB7K1DcHvQONsLaeXA+7o9vgJWZjOfgRfh2WMk1eBv+DRv7b/EhWKUP8PcwZhPxsTF3J73IQDIKvjeT6WQR2UIeIs3kfXKRkzgDZ+EyuHxuKFfLTeeWcMu5rVyMe4v7jPszd4G7DN8Er+cDfDYf4aP8UH4yv5R/gv+a/1qYJLwpfCnqxfniOrFF/LvUW+ovVUmjpVrpAWm/9K5cB9T5CtqHXrpaKOAz3GqugtuH7idFfCb5I/kj0PNkNI2rJECpZBfeQO7EzSRHuF3sR/rhkaidj8Bav0q2kwukH1eJR+CxaA7pmbyb6OCfh6KMfwW18Ydgbn+EO98uGvFd5HvRiBoxonoM/4HrwUe5N9HH3Gks8TvRJ7weu3AbeZarAir4Hd9fqEZB7nH0W24RvhPtIxVg/VySNwMdj8Sgq9A4XIh/5BKIIyOBikq4v6B70FzyIWoDPt6AHsHT+JnoflSEV6Kv0TPAFV2EW8V8MQO/QWbzm4gdNyPCPwezK8U5mBMcaA2u5baJ35OP0FJ0ktejz7kXYfQnyW+5Sr5dGINnAQfcidahRYnVaLlQzb+NZyIOT0Bh/gxIt5VcIR+EchVIlUkg0/YDd7eCHBjAVUKNGyjnBqCL8SAhtsH3UZATPFDQbODxG0GK/RE1i+NIC5opmDFIHZDGb8bHoImJZ9BjiZno1sRDqBvIg/WJlXDHXehL9ADahdfG70ALwYb5CHj7BmEIOSkMSXQjm8hHZCzZei1+YbXD2I2+he9v4aK/cBBt4j9AY1F5YnPiPaDuPJCwj6FbwMI5C7P8Dp4wjDuCiuIjyd7EEG4hzPc0Gp14NhHAejQrMQ/srEPoN5KApkhRwHEMvw3zvQNNJ2MSS7jp8dmwDg/AKmiwWktB/mzUBo0fN0Ar739dWb++pX1KehUXFfbsUdC9W9dofpe83Eg4J5QdVAP+LJ/Xk+l2OTMcdptVsZhNRoNeJ0uiwHMEo64VoSF1aixSF+MjoWHDutHr0BSomHJVRV1Mhaoh1/aJqXWsm3ptTw16zvi3nlqyp9bZEytqGSrr1lWtCKmxE4NDagueOLoa4PsGh2rUWBuDKxm8hcEmgINB+IFa4Z41WI3hOrUiNmTZrE0VdYPhdnsN+kGhQdP13bqivXoDgAaAYq7Qwr3Y1R8zgLgq+u4lSDbBoGKe0OCKWGZoMB1BjAtXTJkWqxpdXTHYGwzWdOsaw4Omhm6JodDAmCXKuqBB7DExcVBMYo9RZ9PZoHvVvV2PbNrcoqBb6qLGaaFpUyZVx7gpNfQZ1ig8d3DMteKs+8ol3Nw2qHr91a1eblOFe7ZKLzdtWq/Gdoyuvro1SPOaGrgH/JaEh9RtGgKP3gyLOGKsCk8ja2uqY3gtPFKlM6GzSs5veqiC1tTNUWO60MDQrE1z6gA1nk0xNGZ5sNHj0Q4kziBPhbppXHUoGCv3hmqmDPbtdaBNY5Y3ZWpq5rUt3bruVazJhd1rtqQAo+lqYHpnG4NYdwqNGNO5spiOKDQcCCKmTlVhJNUhmFMfmk3vgzZN7QPd4FOD4VexaYCR2THdoLpNSl9aT38fE8JKSN30TwQUEGr727U1U1I1Ylj5J6IgpZNOUoP2NByLRmP5+ZREpEGAUxhjf3bdq1vXZS0kFFqoqFDA8qEqWNspNX0LYPmDQYrge1s0dAtcxBpGVyevVXSLtxFpBdGaGKmjLUfSLRnjaUtDuqXz53UhoORmZjJmxORI559FcdorZvWNYef/S/P0ZPuIsaERoydWqxWb6lJrO2LcNVfJ9j6dbSkoZh9UzXlJCiJejrUCUU7q7Ewvqo0xPgx/IiPqaS2SDFTJarA6JKbUDUvmNfpg8H/8UUuinf6KFVd+lhpmrG/02ut+11xfMzzjJg4GDKpyxLiJmzbpr2kDUks+cHiqAIpH46qD6qAYGg+cGYa/lsSRPjTVeGMaLNkg2gHoL1mVurymozcF18CHUme3rkNA0G3aNCSkDtlUt2lKS6LhlpCqhDYdIEfJ0U0LK+rShNOSaL3XGxuyuQbWahbuC0xB0MC9Ibxh9F4Nbxg7sfoA+BLqhnHVjQSTQXUDa/bmQFv1ARUhjdUSWksr6YVKL9AIDJNsJDLr7z2gIdTAWnlWwa6ntmDE6uR0HUZTW0iyTknXEajjk3Uaq6MfKmMGjau+mnoYS9Z0o25VUuslcmnM4uefA2gcl9cUcQdOHeK6oDOQCNelMZoVOMDlclmN/QJaCxdqsmUUWgZ041QYUwHLVcgXQNoD6TAkHk3m/FCvQL4KUgOkPZAOQzoFSYSB+FmrCmkBpO2QztAWLovzNaoBZUAulwm/zYQ5WjgX+h5SAhKHApAXQBoFaTKkByBthySyfrRmAaRVkA5DamctGudqfKgIxu5qvJcVTXPmFbLLKcnLSbXssunGmmRZOTpZDh6e7NY32a1ncbK6+8Bkmds1WdrChQ201JsKjwxwck6YpBMGvhByTI4hC8Zg5uzgMlAMEuHEVI3G2ZpyIoXbD3M8whzhMLglgcQRDjearIUD9CRBvkc2FCDfkbZkC2lrMlsLtw+4nvwZ7YF0GBJH/gzfL8gXaBU5Q9cc8nJI2yEdhnQS0veQRHIGvqfh+zn5HFnIZ6gAUjmkyZC2QzoM6XtIEvkMcoV8SuUXyylcDomQTyFXyCcwrU8gt5CPAfqYfAxDe6expLTwAAOiBSkgEE4BLm8KsDkLW8jbjT91AYqKAKaBog5y2ag/KuKyG8M9Ay2cu7FsdqCF/KVJjQZ2DOhB3kUxSARG8i48+V2kQqqCVAdpISQRoPcBeh81QNoCaQekGCSgMsgVSCo5DuktSO+jHpA0SFWQZHKqER7TQk42RgYGBjjBwH8NnO0AOUFeZ+Vb5FVWvkn+wMo3oPRDeZy82ugPoAEGaEfwGwVKBcoCaBfIy005tkBigJUchrULQF4AqRzSKEiTIT0ASSSHSXbjtIANbnIQHZcR9GxE37DyGfSkjLQ5AS0yCAhQpVmk73UAQbZd3R4hWmTrY3BJs8j9DwFEs8iazQDRLLJiNUA0i8xbBhDNItPmAESzyMTJANEsMmocQJC1kCdeyskNlIyai9UBFnIbrNJtsEq3wSrdhnjwH+GLfuLp2H7VmJ8PK7ZNi3bJDzS04oZDuGEMbngSN0zHDXfhhtW4oQw33IwborjBhxv8uEHDDQdxH1iKBqw1X3NZqrlxw3HcsBs31OOGCG4I44Yc3KDiEq2FBBuHF7GighVNAyjTQXldf5A+FhKEFQ0CzQdBJhyG/CSkBLvSoJOaneyc6adldlN+efK6e9/CBQOGkVfgh68AGl5BpyHxgKBXgIxegZu8AjewQF4OaTKkI5C+h5SAJELvbBj4Ayy3QF4AqRzSZEirIH0PSWTD+R4SQQtSQ9zDBlaQGvQoekVegS910IMkqGUpPiWqDOMe8GGLH4/yJ/ykBDmdIJFtVtnagk37/2X68V8mpBugI/eTB2ioFtziZPlA409ZgRb8aGPkYGBABn4E+XmgOlyKIjgMZR9Uz657IZ9My2LkIy9AWdjomwA/szRGugZasZn+an/gJ9/ZwDe+FgLgOd/BwAdqC48bA+9BzQv7A+/6NgbeKGiRoeZQpAVD0aqyrgd8fQK7j7Ouq6FhW2PgLlrsD9zpGxqY62MN05MNN9fDlWYJjIlMDAyD+w323RLQ6uGe+wPlvpsDZclevehv9gd6wBCiSTAfBtvFxx4a8rMbji9pwbO0rtJWqVoaBd58odRVCkoBKUvySg7ZJiuyWTbKelmWRZmXiYxkR0vijBalkT+HyAKANCSLEc9ghdCcJEOFBMsEXL6YnRtBRowdiEfEjkxFI25RYxfGhlqwHqwZITQQx2wj0IhxA2N9oiNapMSYWEl0REyquql6L8b310BtjGwALT6uugUnaNVaL/UbDiCMrWvv89Iyb+19NTXI7VxW7i639beWDhn8H7K6VB698nFfA2fFto4YWx17PqsmVkiBRFbNiNgvqGNxAP+A2ysGH8B/p0VN9QGuP/6hYgyt5/oPrqkZ0YInsH5IxX+HfkAxf2f9ZFDMtB9SZX+y37ZkvzD8Hvrl0AL66XQozPqFdTrWj8e03976nIrBe3NyWB+XiupZn3qXenWf42HoEw6zPs4GdJz1Oe5soH1i/VkXnw+6+H2sC/YgH+viwx7WZcKVLgWpLhs7u2xkT+LwlT6+ZB/TmXQf0xnoE/1fP9MHRqO4qV/N1EnUKasLVUyHVBe7d9ksd6zhFlXdO7Um5a1F6m6ZOouWU6bHakLTB8emhgare/tN+g/Nk2hzv9DgvWhSxbjqvZO06YMb+2n9KkJTBtc0Da0qLrnmWRs7n1Vc9R9uVkVvVkyfNbTkPzSX0Oah9Fkl9Fkl9FlDtaHsWYjReFX1XhkNrAEfgJVNxKAHeq3zBmsGOpWF/Rnx9gu67/K2grWyCxnAJTKCe22CRJu6Deg2gDYBT9EmM/W8U03uu/oFva14V6pJgWpraCCKLllavxS5K2YPTv7VwweqliylC57Mo/X/7QNtFeBED65fgtCIWP7YEbFysHb3ShLU1tEpxfqm6wyGCrD9k5XdobIvreS4zo60rozW6XSpjj/H/9JUOYhyQQM52IQ1P16C6mu4mH/EOAKiYFzKxWkFW4qqh/oamGA9juL69D1Sw45GUfIa0Tmn05KlKSi1FktSZfKX8JP69JJ0fuhiRTtXbAnckH5kNCO1t8EhI0rvc/AAG1OwCJCbegI83W9xo5wUTJAZlaVgDuqvT8E8wLekYBHgFQPoZ2B0wOLZU+b9NxgN6PwCpiFfjGajKWgeGoOmo5loKUBToO6/9fq/1hO20QRfGKeEUNAatIYhA+mOLqvckcuagC4hlT9C50/QiMQ53s/3RxkoC4/VXAHkyyDjuVqhVjfeMJ2bKyzQTTfIGS2Js80mkzjeCoA2hkJZPprn2j4SLjouePietr6ZPX0DbJWeAb7RtkmZY3xTbPM9U3y3i7dnXCAX3ApyYovJ5apy1jkXglvgs2xRdihEUXivTy+xHUOcONKsKGQ8BsLTFEURxysY44ftPt7g0kwtiU9hBGQ8AN+xoQDwbbPRyIAjmi43vzhmwiZPAK6awpFiWmoD/KHiHgEccBYpOZKWk18ckMpBQ3KSajCQ8ZLbaITcR28rmQ0GEWB6Q8lJ7y9l+otL3NGRyvkkidVGKzvOjlQWRaMXFtHryjZU3tZRG42eLW+zlRbUlnUsKsNWW2mprbRnD1yLoCWKFy3GLlEMZSOrgooKkdUhBZ3OosLeOBjJjYSyRe7m1q7fHfgm/j12fPoeNuPL5/SNa6du7viYjDb2mbBx5XN4guupZhzAHDbivPjn8Z8UdU/rLPzwukGznmEU/Tn/PBklHAVcTwc9mni9yZVZTOgaQMnRNYgAsAo3kNOYW8CtAm+QW4AWYDIKVxFQ8ZwCztZ6zOMWUtdI1nEtZOw+lMl/9CybeWXH+Q5U3lFbRmcEH5iS3V7Efb72b5/yz2N3/BxSeKChwUBDuUBDJpSJ5+7PcNPls7ckzjE8WQDQ6imUyRpskj7TOFQcJk8Qa+SZ4mxZLlb62vo6e7krlBG2Ec4K9yRhkm6MUmurdY5xzxfm66Yp823zndPct+EMnSiYbuLGCeP0NxnncdOF6fp5Rr3Lx0tWn8HgyPFqFJdejWEX6EOzWiwUzwzDSqq2vZmhmgIMzxSwWFIAwz9dNntOuLiHhJGkSCoQTM/TXuyl9cMpSQFszkFGM9wc2ejNkZGSEvLRRyBGSshMb46MlI6Rk94WaXDLACqHBevpoaSlXKjtFGBtQFi1F2qvVEQpeZW3wcIvqkWLQNZqurHCWN0twi06HtfWMLlpV0p6A1VlOBiJ2R2UsnoVU7oa/PTGP3yCnXf89d7T8bYDjevXNTatXd9I7Dj3/mXxLzpO/PVu7Memt958609/ePM4oyNw3vAKPgJ0NGcfwSLSC5TVFbp+wlOEC4NQUWHkVBBQJgXgb80mCwPoqjHgYjObf0visqawGWexlXiK//ZtOtsUH5V1lH0VReXlQFRF1iLrq8185NLHLh/cfHzia94qHEEKyKLxewkN3Wh6j58XHH6TyaWjFEXHQwEtky67zoqMtIYuMEUCQ0UBPOQEZCdSC+jdK/78TucZygH4ihEBAN9pmXS0cEuFYS45A4XNIH3LK/dsFtVMxQdzbySq4feJM8gJyQbJAob1Lby4nmwwbLC8YRZ0ksFNKuw3ZFyfOcg7zj4pA2Sjd6401zDVPi9jbmaddzm5TVxmWGFZLz4qbVXecH9M3hffN3xi8XQOt16nBYHodBjpFB3RbQlY6xElRTPUqoiGvrb4X7s3SU9tkC26Qju1i1At6kM/GFJNjV2xAcU4nbYMhQDN5EbsCiUaqwJEI4nj576zY1njkoFz3tn57vIHDzy3cuVzz9218vpa8g4IiOtenNwUT3wcj8df2f3oS/jX8Ue+b8ez8JzvZq+j9DMFdKJTeBZkwELNfMyEefgjMq/jTKiFHNR6EMzrjKZ6jiN0MqPIZLKAcMRjket1f0Wj8GQ8mXDlUCzAq+BhmeYW/NDee5kQql1UVnm+baQCYresUmmjlNNRVmotpcKWsYe9VzBDRJwohXrbbCVTuH2b420jelsOcHf/YyN/cffmh+O2+KWWT3bjb/FrjzMrIPE1KRXeAVofewBxic8bHaUgMj/XVEfpIxwm3HZuD0jFZQg7oDehp5b03DlEzuEW/Nw+mGfTCljsMuV8Gx1NWXnZeqF7tPZO5Rhd8Gg0Axdh/NyWeHWm8LeL9A7oNCjaS0DXerRHUznNZC2ey68iD5DHZP5FHoMlIRBOJ2Ajwcf1DLN6im+EaTwRHLU0w6XEGfIxYjczYgcK1DIpKafpldGuxyhoJkuxkKaSHgJWBU0gQqahFZfhtYgu7FkglE6ujNbS1e0og9V1wcqmVVgwZBVFqVfv3iVF5FLzgHfGPfLngiX8Hf1XBn479PhkOrcyWA8J5ubHr6X4TGdVTG67nSnm881WKwO+03RUEpr8DsFP2ddFO/j9tNXvM0OL30hH7qe0YiR6l0sNKFZC1AAgueDdEzQ/gQra6EjLaX6skDI26Xyg0WZjtsF5TWexkvRzzmgGm52M9ztoHb13I9yaihEqpwD4m8ZW8T89jfI6fR59GnuY1ruf0E88KBwWD0qvyW/4pOHGGuM481zjNPMK2wr7Rtsh25eeL73tHuNhw0t24lV8SpbiV8TfJ9qRBIJBhlIH2PL49Yosisd9HofP55F9HqA42ePjTH6lhTzdNMqKrS3YvY/OALHlsGBi1Ne73oHVpqyDD5LVIIoV3EczWveVM0ZaRXjSSnJQAD+wNykIgDYvRCmJIsot1FA5a7VRzEK23tw9agZyTTIQSkuHPqgW1y6uqQlnBCMlgPHeKV0i5famMoNqGQn+eOlyCXGFn9r2/a7H7rj7cXzA/uOf3rkw7NmjT07y7949oGzqkbuOfTlj7i8e32Q/+dG3u6ufP/T0hik9mQ0+FuR7JtCKC4VQD4KS6Gs2Iq+/O6VUoAkyvnt3W9AvCnl+m8mvY1qVSuv9TNpHLUldTZhRwZBIAdZocXO0kbIKl+7FUXpgioLLyTDS7hnsjhlMUWRckerXqgyK/bbS0k7N8RIbiJgeiJgcyFmmQSjA6lLPp3UcVYDZtJI+lv4ygxF3BpvplfmlHwbPwgWpAaQTJbiSXk7cxTncOTzylfGbHoKuB74T3YlX8kvkRYbFxqWmFa570Sa8mV8nrzasMa4z3ed6y/qq3ZYNdN/oUz20UNUCWnRTI5QZ/F1UI/K7kRGGsaM7vmql6w/rsK6FzNSUaL1FU0FiWDCyKBZiacEP7i9018c4DLbhzMac+oxO1ZOhZZCMLT07Vc/52jYqrRmQmlstmxy1ikGg9El9gMoWoUU1NTgS6VWcIi3JCTkCKwZqrjZkOIfzygWes3DeV4ePfDt3/vr74hc++ih+4cFb1s2dtXbjjJkb+g7fMnb1rt13r3qW83Z5dM6Oj0/vmPFIl67HNhxKIIyPPPAyHjdrzT2Tp65fczlRuWXUMw13P7+L+j+VQJMZQJNZKB+PSgmUgAUs7slgc3vz/Bo4FSYQJl4h2+8w6f0YhZVOslL8LmYhuRRKVi5GVq4UMZ1494TyhzSKYWWO1VKcdpubiQdLWsbgzMHqRNs4dS43TZomz7FNU5fIS31r5XW+9+V3nVZJZd4VFf4UCDFRRqGgmrJZzzTnqiE1SBusdJRVJgLj9OJ3JlOlQWZpuvSYQWv10WxoX7heYXaEghF4VwRm0f4SJU5lS1d9K+4DErxUc5a7JrsWuFa5eBezWV3MFXK1kJymaBLPoCnaqGxheE6imckWOscUjqmQptqjBkvMy6FSg4oQW8pQtTKz1YmvRi13qcnddfjcCQPG30IGHJrZ3HHbqTVfxM/+euO53Z91lIy6f+Tip5+8Y8Xz/FjznB6VPfp/9+nUuvi/3t7UdhcegVfi517edfTyZ7XP17Q88eiePRlbAK8HQFyuY/ashPpoKi8gUdIRsYznyrDI60lZAbXEqYbdKe98FGYGlEttC9DpKS8OSNbeqyiDg3TgxIkTXM2JE5efPXEC7u0BnacIraDPTTii9bZVG2cZtxmfM75hFG7gbjD9kudsINORUeQkQW/gJPAOTKbjHO/gOB7sIWI08RJ3kBxEMjjrOzQ94nnogo7r+RYy4yVB0GtZgWJ9WvPrk0YqA5Kur74Fl2gmScsOFUsNwV7SFgv1+IBAHMWIKEQFwypJOWQ8Scsnsg9Mqs1MO/yNOrMUk8yzLVO+UpjeV86XXSizpth1ffcoDyrCYrEAUlmkxwR2kq0UVOy7mqGolMvuVsrxWVll9BY1gGzqoTiMmqHU2FBVatQipcZsH5TdSpl1URO0BnthMPczQlbOisnWjjXk17949dXmeC88+Tfc/svX/ya+E5TYwx1zASMTEl+BLXkERXFNiiMNmW7m2rl9iIUHoka4wF1CepPFaPHr9V0y/D7e38UndDGFTEZ3JkY2lfGkKkWoqqDdIwWMLQvoF9lKYcKAayp+X1VetZUqx6KFNFEmzRNMTlOFaZ2Jr7DeaF3m5cY45ylzHNOcS03LHetMmxwbvb8x6QWVY2xpMJrMvIThuZgqcLo9fRDTY5sm3As8owze3UqeRpnAkrkwSgGGabLVT1YXqERlDrHaINVHGGdGMIooEQIjPv8SbYls6eYG5m3MfAdTBkUUx1ds/q4pEzkpezt5MpoSvh2MKduUJHcyxc/4k3ImXlRjL7nCfVLJFUZMafurZXJkQnPg4bmr9jx5Z9ENDpuhvmXdnNmbHc3Bb397+/G5M6bdvSV+7v2XE/ge92PrY3ev3Ol4gtx+59S716xR9702s3Ha5Me7+393/5H4P7+61w1LQ328oPAMSJsJSdzSqMURQC5FqM+s92dk+GzU7DFYeN7vM5nBBXfTUA+VswxgdMD0JOAxJV87jgHuKOq62JiqtbB8hGd51qasrfZn7a8Y3zd+4pV1drc538Ppegg9DK1gk3GAP8Wuz7DZ7cfNFofZ7jBbTIBEzU4Hopl3mInZbNEycGpQL1l4/A5FMFhoIFNgeNbJygJllfKAwiuARjdDoxsjt+Im7jQa3VtU2yHcC1nww0ATfRrN+/4TOgPXovMKQmvLWKApFWayQgLCPbte7h4VgENR0gtienUReB/XIBawaQ9mBLlklEAC4RsZ/7uMx+bd3bx7842b8567n3zU8dKoNQ8ewfKS+86/3oEblE33HntyW+Oocif5+4vxZZPiF/702oONZ5D7sZR/Tu03P+jK36ZsI0MAphu2wmQvMOOHzpoJKTd1TfKoEnFbGTaszEOxuq1do4Y8v8UcMI8yc2azA1VhzJjapIA3gOmqZlPjl6L2WLS2kKmWQiaVAOtUuypUt372h04P4KpBXKETLZ8RipVZfP/lqdc+698eVXD1g7Tivp4bnFroJueNoRncPOd8z8zQCs+d/s2ee/3bnM95Dnm+dX6lXlDt1zmfcO52cn27TBNJLqWxENCYO6iKap5/lHkyJSgffSR+pyqpqJvpIAKtuBQZQE9bryWhLV2p9m6mytvaaXdZNSuxbom+djXvU0ppu5pO0soY1S7CtTUpquhPehXnUh0MJQIut1lZGCCCGbFkMLtr4W7nyilj76zqjXsfnL//MpZefaDtjhV/f/LFj8mbv1lye+NzK+/ciccqK269YdWHC43uCXOx/OFprGyL/yX+Q/zreNNvD3PFv9p/7PHNe/ZQ//BRhEQL6EqFnE3zu5y4oBkoVmSzycpUFKg1AATqJuZRyGijzYLFyOnAAZd1BjOSdURvEKk+NCgUsYaWxMX9tJdBQVRBpnzkHxn+aQiq+ZpgEDXty48cUU6dOkIlYRSEBab7DungUICGhMXxIss5lvMsF1gutyR+0EIUIkbWQ6S0TMzMLWAmvt6YCh3+yKgffvCjFmBCHBx7VW8rtrBMMHIImw1IljFh+pzejQHsJgfJBGSDtZqgmRB7EGIPQunbIuooRM8XgOZmkYey5GRqk7O5akfFq61CxCI7iFfmlxnXGV+HpTQONw63cF34sKmruZq7iV9mut283iQbiCCXmnqbR5ERHJimcqVpoFn/KHmM2yptlXdxz0qijVjM5h4CcQgCkYGXewgygLJxjGUM1jAhsqzTG8A8NZsViqc6W4ON2FrJLpB2PRsFVW7BPTW9UadXNeMqAza0wiTN2AAtpAUbwGHHSLUsVDD4vxNeUoU6oUHghBayq8nar8YdzQTLDGSgu6NMafNkKlQeejovztYidzksg3LV16O0tdGIzPo7j63v7qYFOLkjYoaxI2L+0ROrf4eMiUtAg+8jknifBsXwiJgR2vKgjVo5P+4162ltyuh5d3+w1Nw1yAyf/SWl5sISBu7rBrUp4yZas5jGaGspnxVZg9jp6l2Cg9aQFYew9VGcg2/q4cwEOwcLB+MT9sSrhdZLPzw4rOpX3OWLQ/g3L/Xiz1xSWQz2MGSrgVc49NY+TJBMaACnqc91LJDTVFScLLv1SJZ5XZJlKJwss/zJ0u1JBn7yTUqxKmwR9ggcp2KEHkA7UAzxBaBuqtBp1I4EmwqVWxAnJK1Hyi/uzuBuio++a07FmS5ozKBCjB/Qk/z7NVfFdGGxGhvAz6qtWbS4rKMzik3NymSU9/BRofXiEHpKL/G18JnwLjIjLzquVXks2KE4HF6X18vzCu8wuAxe/jnXfvOrZs7lcnuJmqVZR9lHuTRPtVCtu1EZb51sn+ia7J7gudF7r+sxomT6Oc7mN+gyIqqEJWrs0SlI6ZFL6fg03Q9IR/7PpyP/F7UgY1tPQxbOskSoOyBexXaZvqmTUltAtZVtI5Xa9Hwrr4mw1tYusisoWMiDh0P4UHYOKUlu9xQTkKtoKt6Ae7+Jh7zQHN9/+GS8ddfrOOuDT7B3+TcP/jH+ATmO5+NfH43/5tPT8R37XscTfx//V/wkLsbeJmz4RfxLGNIk0Lt/Fd5BPUiGljuVm8rXc0t4Ppzbiyv1DeKGSzdkVQQG5wzJHcvVSJOybszbaDeDx/gDW4icNBBOA5E0kJsGQlR5m5Kdk0A4DUTSQC6lgCEUyjNFckgOlxvubSkODQ5XFExUJ4TGh+cZ5pjmmmc4pruXG1aYVljuVJbm1IfXcZsMG02bLPcpa3PuCT9k2mrZmuFPydxuwYjNG/HoIl1wBKEuHhtf2DOCpgORmLot9270Em/Yaermzw3jsOAUqNPNZKzg76bz+50cw0AUdHUtpFRRy+zcgrbkF9ztcI7ZZBCCviy/V5ZEniMiDudkQ50I7nE3j0Yp4wEP9rQ5UTdmhDDNo2AVV+E6vBBvwSJo4Jhm70YfSR8NI75eF0FdcBdKUGYzGd+FDs1Ef9fFUwhzwhEbZSDaZGPLZ2LAj5qF9gGnH1Ca2XPqTcnoduVZtv1DA9yMsFJONdjt0bM0O09nZHXRGZZiAGtAmtUuurJPBBRoL/ET0NzU5suN5OSysAoz4l1SJKnYXU5w6J1J/zsnMukl0+TX71zw/NiqSf3i80bPnnnXD7986qd1Qqtl93OxnaV98EfVDSvWXfr1a/F/PIY/UG6978aB9YMrZoZcU6IlT01f8PK02W+tNt97/+qbRhUVzc3rt2/Z0pP1S74BGQsitQMhoQZkmAQifuZ+bLYoTMP/0JwCknqM0CWruaI9BZYXKD2UmfIsXZ2ygduivCG8Kh5R2hWDLNTgCaRKmWWIKf8w/sP0D7OON/Im3swZ9DqB58H/kkVJMgIsi0YJRF3nYiNVMjqgiXAcrcugdZzKGx3wK50fdJhf5MQWslDTIdn4jUZAmbViAwgyAxCCiqZL3Jgq/iR/mue28JhvwVgzVBmPSKeN3BYjNtJrxSKdlMgqqUEi0i8s73+QjCRkQoI/d1tSTbWBlirztJWfBbUFf1Q7RcF2p+opmgrEloLnvV45dsx87Nh6IVmCdLmitpp5CydLrYl28Bt+7MP01uJFKUEbwkU4xAU5e5CL5IoSR4r+RKo/e6HjVzs/wn9/bEi2r4gKX3woPphMxFsP3HbfvQiWBqPHAVcBwJUO37nXZmAbD/aMYtltdLLY6TkQjgDJsHiqJMMyykTiOFnHE6KTZJ5TRVFIawqBGkhMhQhJ260l8S/NwwyoWtWAVUOVoc6w0NBgEAyyTsVsl8MED2NHplM650Ja51xI65yLSTsR8Uwmwy2b09aQpmfmUK2+31VqKFobLQPLAJY+GdigYQ0wkijj0NXlmU2QFD503+fMS0ZrsaxCBjYTsBXwEj3U0ixrQ0rp1vn+IaWyVpgEC0ul7Ey2S7Q/E8DCJEhrQ8m9I0OoVDI7INnp9fn9dgCzkmAWgBkU/HFvRmmaa68y1sBoKMKgI8FaePw1jrS+djkOJsJqfhWYBw2XGpK46oEQ38r4aqNmEogfRBlirz3pWkh9k5qkzpdEFZMCGqrFeB9O7R+d0wxsgeXU6v6Q3q79c3qZL6eXNZ40Mukd5f2PXb1nC0QN63m29itqYSW34Hr2oEEdcC+txB7P4jfFvYJp9+6L/0AGEUT4TqCt3TBeN8rGl7WgzWDGtt6+iYEZ8vwAr2PbVDLLJZaD5jnCxsU2jShgTAOGNAAy9M9NNk8xlO1N2bnFVnqdlVuspEpLqoT2D5uyIsl26K+kStquDQcgbL7ed7061jDJN9+3WHe7ebllrX6D5RHTc5YWyznz1xYF7HrVanFYrRarxaizeUnQ49SLNrrPJLh1OqfLk+l3URJmW8YuFwpm+0F2ILfbYjHL/oj5cTG9WS2myVmkwaJsOg9RpDMWa9WchTkNOVxOtlvF/xsfiP+VD0L9dl11VgUQVkkZgaEv86yb7gJRxFG00d0NaCsrLWD7QcntIKFz9/KqD0pZdppe1iylFqWv1daXkitexGxjM1C9J7PUCnxhg2TWfKVKtgNSAFInoVOLOB1Ocjld9hDXneRGQiErVCd3mII7yaZjb604/k5l3vgbEuePjr/1xm7BEV/gnWu3jnzkqXgPoXXU68sffz8rnDNyaXwR7rlmcx+D1LGUKypZPnQW23veCTZlNtCaA32k6SOWar5afkPmnRRBTpAxxXw/eQh/vbzM8oxwziIZEbG2kIPNos4RIWn5RTrlF2FmLg2gaj7m8NWqTqw6q5yEnlxqcHJOU0TVY31aXOrVVJA2iTZ9Gm36TrTp+ZSTl0SbvhNt+toMKr6uoC1a21apgDXJ8Ja0MdnRmyiqxUXWlG3ZC0RFMnRu5euOTotfeveP8YsLjw7dfef7+4XWy3s/i19+6n5s+oYbdbnx8L5bjrJdbDQf1ugA2JBhbNc8Xoc3g9Tl4ptlO7ZxOTkoaHORMAIaprNS6SgxFl1+Mxf0izqMI7nhHBW0AFFz61hw+WznoqWjzB83p5btvOZlvvHihlycm8WWigV+9JmRlMlDrZzKFHECoYKYppHKzq23MnadjFmyw1SDlmuD+ZDX5/Fl+sDxjijhjEggIof5SCjsNmUFkdNiD0Jnh12V4CpbCAexz+AKYocVMr8uGEQ5HGQoJXgpJ3QSef5qFhDtFbaKydUttuUUFfJgOnUnYDqJElhPNh4Wu8TK3UDmPxA/tePD+PbmJlz1yXaMH4rsCd6yf8Hao7cF+6zH5MG72vuT8hdxx5nF9QfwzR++j+ubZ7b8ssfChsrRa0Zt2H4s/mPDlBJsBXw8CfKc/p8EA3pYywBjVJYlCXE8FSN6nd8Asp2iIkuxFUvjuOtVvWoieo+J1/1/ioorh3FSMqI9LSOM/W66+jwbFRFgdNZWnj8bTe4SJ6UD1ZvUaaOSPZme5HMuP8FFL7/HrRFad8fLX4ybdlOamgE0tQxoKgu9s28qmZNFyedcc0pGndMmU0hFhaapaCFaktWA1mRtQduEF7jfmA5wzabXTKfQ2ax/ZFnNtixrVhaXL+ZZ831qYKhpguPGjAmZs4S5WXfY7rVt4x4zb/Ptwk+TXdb3zHbgco/iUDw8VbuNeaWMZLvllSoWhHmv3W/kvH5QMBHL9SiiYow9AVdElbHMwilypj/t0TGH7sIVg9vK4uOwLtSjAzqhJ/eupoiULU2AHCjv8c1Hr4u/8mVb/INf7cGDjn6Ku/Y7XHT0F8/9ZdL8r9Y99WdCen5/6WV869tf4vF7z7zZbcdDT8a/f/Bg/JtNh5Aowdo9Dfoxm+H/QxpmOKJ5QJbwnF+n36E/pSd6gRCDLAuyKkki9WVTuiQlpEQmpJhKcbO4FabIFmsbTNhEDGpK8ycF1P+gV+Sf6xVnimZUE1ZNVaY600ITT4VV7aJOa4sd5Ur7xJ3WVmltATMOcJT6/UFq10D+9FFy8ejRDlFo7XiGTLw4hDR1VLbFwU54AtZhIqyDBajoS61ADeBBMnhrwAZWxW9BMiBPhwNs80fHJqLTszNg7Ewf3ZGnRj24EZ5AlvI/88bP9ae/36Rrj6il4Nq2TtZggqg355VkEfDCy7yY6fa4iWjQG/UmPSdmOB1Ou5MTvRxIHZsZMrfsC2Kn3hpETO7kwwekDVWIoAmdNFhgJqEw6MGSlO8WCj6Bf3ph4l01S+pHrnjwxNr4Xlz64G96VlQ+Mm/k7vhbQmtG1g23xE8eezYef25K4e7ePSu+eearf+XT9wt3gTxZS215dJ8WZfLkAQl3ihQQJ4+D9DYQ4jH8DzIkRRbGFFnEfyZK9P0m/czcYNx0NmkbsgOx/yZHdnGfXf6SxDqqqAzpu7tjhkz/BdJWGHc+jFtA8zUjJsABApKZOUue1SwS4f5npF74GVLFnyH1q9okOpNBqWDG1qPkbfCN/rH7LJVnz8c/x/egE0iPRu7Tc0h6QWzBVVoEc2WEYD0uQ3oCtnUZEvtIfUehyWgBWoV2wMh3GFI7yOfPUv+O7tdQcwt8+LbkGZuiXkXUAMrt3btk/4mqGwtLe3MnTiy6N1KZOeWmRCJ5Bls4SiL0bBqm/2/nHwghr2YmZ4LtQRLUo2FRgoMHCP3HVUBJtb0QPBcDzyDu73wEKfjT1D5phgXTfwekE4lo0iO9xc0OyxTQ45bl5UzCeV+y2LAF7DWRei1VmaUTLVv5rTKIWMsR4Yh4RHrTorNozlIPZ9dlmDxKL9zXsBrfb5ALbDfyNVKNodr8CH5U/6jhJdJifN1w3PyW8jH3nu5Ppk+UL/U2Wyq0YzAim9XiNikiFVLnNDOFLOAXmJBeT0QmKKjTC3xBC682QxQ5SdbpsCiCG89xBgtY4SYTtlhMigEjHTEZOKOiFy3EoldeRa/qiBJGOrBsdBwxvQpSL2zkHEYjp9fpOI6I4FMYjUg/yoZtw013GbP1limi7i5N34K9L2lildjA3P5Bmlnl7iLZo2Ath1tXHkudnPJkdtR2eMB3/1I538YoJmk705wd9UtFlmtT22+lFst6+dh6s3IsmUMhmZWyMrmsJrl53mx2Z5UamJeYVWrMdpVykOh1Y7BUYWI6oxRnB0t1YEKnZWsNqKEo1UO1NUUYF7loULkEoBCXiy14TfyxL57q7usabvog/iC+97OP+8a/IXk4/tPQHgOLLsWNHX/E19fEa4GSNgBFlbGYsoROaDfretNjUKN0W3Q7dDHdEd1pXbtOQrqAbqGuQbc9VXVGl9DpAzoMtMgTTidyd2EkCiKvF6WwgPjt/A4+xh/hz/DiEb6dJ4hX+VNwxfM0vEMZlacyxEUDYDxPuZPX06fyDratQsNm9Cwgn/Y3eXpeSk+Zlx8pD626yp8HbbO4jB1OKCtPHstIHSWqXbwoyg5nABdvaG5u5v968uSlDHqQmPJxVeIc18b3Rx48McUXxeZVFmwxYBr6Xghrwdt8Bsnt4w3YnCHJdKASO7wjsd3C1FFxutUSPfHuq8l43LHaQproXuBQnREHfIPsg1xj7WNddfY616/Ir7htpqeVpz1G2ZSpn0Nmc3OEpcaFpgbTM8Z9uv36fUaj07jO+BfCmbMnWxZYVlk4C24hz2vLe7B4fB0Mawsw9RnUDiLcYjGgK2P0wdBzzDJdKnO2F+aXY4gGMGCHhqNgRbFGlxMPY2a7h3bDw30ZOSclTF93IKlXG/S0k2Rj4e6e3uJjqWWuXdSWXO7axanX5NmGfJ+atsXno22L01a5tbRAqT0Lfyz6iGsX1aRfbChmJ4o7I43UNuLK9mZ9/9uP4/9a/M3G3Z8G9mSumrjh+afXzLkfr3W9dBJnYf2LmKzes9M7d94r77x/9G56NpDqgW+ATq3sJJb9AOJBtQ9lBMMPCU0IzQjV69boxNmepcJCXb3hHuEeg5jr1HHu3Hy/M0uns9v8+flduqCk7RDw+61IdkdEIyVHEEFfaUXMgmKn9UWRWU/M6hFZ5FF0MJd8XDhi9NFfgEY3JUMQWgbtZfR0zfL//zC9o/+uiVi0F1y8tAZt+3cLnC459YGSxzboWdyrHGnIwXLAwcKk4QB2A7SV0N1dCm8lkV1v1s+YufaBGxte3hz/Bb5udZ/rRwy5+4n4J3j+zZFBE/uOe3hzfLfQWnNg+s3PFOUeapi5t64nN8bqnFE5fEGXSzskY5+5Q8Ys70l5qSTxNTeF4eU5TZlOZopLyFJxg2mDVdQxb7HZQKmuBXs0A++36HQRvV6OGJI8zmI3yQMbhqQVwYCki0BrNHbIzFCr2rFq1+xV9jo7b8cRxI7sJNfy2/RafppayxG2/emQX5uScpWjNKoKpNoWLaf0mToC0Zt5ypRKI/32SAunDp+Td7Tm5btfPoF3uHetHFR/F/fD5cyW43M+p/McANw4h8wHSuyqZS4kCzlSiStB7YcQ8QgLacSeX3hf8ti08hUqqGyDBwEf0LPnA0gX3LJvHzKZoJsO7Nkh9GwY7p+SPTYBI5nZYHok6GQBE6HgsxPKZyesRUVULzMLwavlFAg4H+VxYX2BsYexzrhR3qjbYjxibDcaVGOVkfDgD5DUoQsdNoKHCLcsL0/pzxxQfKosOMBlALmgEsFBiKCDR32j6pGsmy7j6URm23x5pVUybpC3yHANAsREtLzSyQQ/QLYTQmiNVRWqBNJDqBO2CEeEdkEQWsiGJkPdruRe7CJ6bpwmt5I8/+7JbHMnz8CnQto0op0MXDtAnDQii74l8fdGnQ3TQnbQ/f7kQVC69ZoH3XozqYPYPyVhW2r08GAQFyV3UoswGdDx+tv4zu6B7G5486sdR4XWSx80LLz9dr7LxSEo6UPwHbDmJuRGjVrX6da5DjJCGeG4SbnJwRuMfovZjFzupB1si8iMt2Ulta+filfIHtWD4c/jNv1fzeOfs3vm1eye4vdFtRdSQZ00lzMvCabKHAI/OJYkGGRhsbQvQLo8VDnvoZrv4m/EN+A7Dj1Re0PPNfGNQqvZNn3//IPxjo4XObx51aR7MkzoTgXW4XqwI32g9/JQCemmddWZdPmZJk9+F1N+fqmpd0aJt2/+8PxaU23+HNPs/Loem0zrumxz/srznCkjL82VuezdGgo9k/l83v7Mg3nHMk/mvZ3xWZ482In9dLmsdLY225Vtm17UDR1PoYAr4I52zS8u5Uu7DueHdZ0g10RnyLOjy4zrjW8YfzL9FLWWFJsxrxTkFLsKgw735C4LupAuvgJzufkB83ZzwixsN+8xf2/mzOy9HnNajpiZJKYazMzOEplFetbIbPZxLlCi+90PO3w+CdFOHoaLilx9oY8zdJmiTEEiQ1s4mENjfakt0b8l3egcZp3k0HgW1fg5NNJM555DxY2BPi6HPSgnHSPPaSE3aeZcjZ7TUyM9InsiQikVgtSCiLQk3t/PgJ6lbHuDvilWeqSU7CjFpS46tgH0jq6wO7sg57B4UiQBsRwMdTNTTMz8ENkbiSI7qi4y8hLZa2Qii6CJPftc9bpG2/m2KMi/KDuR2xlALOuIfvklVdpno+kjgOn+i5JRtfRRQPaCYpSdGUOLwkxK0m3DEvbtVZybPD7Wn7B9RGcGaHZXKMKJkpkklTx04sqmHZiz59DQ+mG95n48ExdVbFi1PCvmvvXUxg3PVyk6V/Yhn+uWYwsmFc6fPevJSNY944e8sHbk6pEOs8mTE9bf2u26mkXuRfeO0KZc3/329ktrr+uDP8vzKXmVBcPqbhp13W1m+v901yMkRoCmQ/g1Gic5nz5dcz793taHWqXBVBzmz/JndV+4vlSF94QLKnHJakjn9qrgDYT8PjGDhnIlLIY8mYr+VBhvCe8Ik7DL5TGHt1ixladkYWXOkpURB0Wj1cFOjrG3VShCrIQixMre6LMysrCmed+a3te0tuBazegOb/FiL7udt/N2XnY7Lz3jZKW387KosJeZZV6qC9n5bq+R3tibpjcvvZ8TkaJQGJ9CmFqIhL5COAr0FP1NFr0zYgeikJIWSvR9uJRouswsbcYYDvaWEDOzU+8mZuaEW/DtTUFqckdHdpIQM7orO84qV9VcITC46BhZMX3wV4vANiwrKwNpVkkVgdV1dbzWbHTYIw6j1Yttpgwvpk5rdHXqpVh6CphtT7vY2X9ryFqctB0ZRGOuGdb1OwufmbPskcBdx594vik0qf/CXzZXT7thdV8+8vDIybdUt+7Z35FLfj1vct+Hn+54hDTefnvVtgc7PqJ6/B6w/c/Q/0mLRx1AHvpmcIarmKh2J93+adeKbI7iqB3nyHanEdudBhHprSAoUJEz7HZpRb2LPRqlBVcey210eV2d4sLFxAVjZQPb+HEwhqYRB4Y8F0Oei56UMNF1TrjwERd2jfRQcZBR3Ls45mn3kIWeHZ6YJ+HhPcawjr3rDI9tp2/3qbpT4HvxurQbRQHNSsegY0+mp6wYzV/QnOz1RcJCX4RxwsjMa1wneiCQiYerzwqAeDhLhUN5WWnq9TnAlYcHT9tiImIysMWJCm/0IpNs9SKKt/z81aiWblcGe1EZAZqJmVWuKyYWV77yvZufGqUYmg3WW0ePvr9f8+PNw+aP6lVPHupouq/n0NFjH9hASsE1K0/yM/cV8LMT36nZBU60k11Ki/IX7mt7O3fBLvIUSWXA0MsV/Khyyn3GnXDzquwwO5w2nwAc7DTpTWajOcfN8ORmr/oaGLYMDrpSBootKzM52ZoZslmPTpwZGM7g+qckzgx6upIGuqZMpRkoESQMGP4MI90UPR6KOHe7myx073DH3EfcvJsjRRnOMGZGgdWasvlT9kLK8UXs6Yg5vp371xfpG3wAMbwhPrnZSg+dUApA7BXbU+AC8mikS7kGcWArnC/7OTqZ4cvCruVtVvAVkgh1iladXtZLekBlxCqavdiit6WYkAYgaWBhEePClGy/igXXP7n0s7qdVYq+OX/usPpn+cgjeyoWVhbe2VFP1t06f8BDb3UcQl6cSCTf+WYxKy4VszqfjllltWeRLJ7FrLKujlnx1Ky6B3qXsHjEX/YLLBjBDq2V9EkeXivulSx79EyW2cnDbVoYuNgiBITtwmmBHwVZu8AFhIVCg5AQeFg4PXtFOXUntqYZRb2KtyN8BBxqcmVh06zFkJH1M0SxCAWSU0IzvR2eSKQtvFScAo3kr41TUJnJsMQOvGF2FU3KOus9zezoG4xiCMim00kfF4/XntYT3hQ2FZsGm4Rejl6+G8k4/RjHWN9MMk2YrpvqqPMdCbwrvGf/LPNL+5eO711/zfwy60wgEXAGAlFPmbPMM8KzMLAlIHUnOabuzr6kl2kEqTANcQz33aifYJpp+lL82nkRnzcrOIMzGxQL8voMkhXpM0DeuYvoeWlLWFFOWbFi1ax11gYrH2DclAy2W21MJzKDj+lFkelEFna3sn0Ipg3NTBumz69SPagNZIpwiS3nsHRSOi0lJD79Xxf8V/3XBX8y3MJCE8ykk5jlRv/rQtXVu1SLKts6rtI9i1hYtYxJsTKamJdMrRn2nnxKRvVKBSVg9fHVL5P1mX5s1XtL57x7T93WgqYO9cWly36z647bd657YvOlp7ZjbtPoAcR8cQixvXX85Vc/fusY1SdrAXGvgryyoje0fgV2rPA4xBfzg/ix/Ax+CS/qrLJO1pnsVp0JcTI2+KiZgfS6vC0ylrPBsbWTbGuSNJOC/mo2v4oaU4I+RY1i+kX6K/qbSfzUuUQWC0IjbUOPXRsso/JdqT2/mEbhqUQoTb/Og5Q31pvZQdjaxXQHIqmBXRIz8oDt1z7Zf3b5TTf3Hziw380OPx/ZuWhY32dzh5bXLe54N3n+JCfxA8kXHkMuul+lp8dOI8VMew0AoCETI2w06TGHnIouatGLTiAyi5KNsrHJFjbihCRX6CrqpIVSg7RF4pGkSjukmHREOiWJ7Khm6szmeeZsSXRfmQXlknZVCkid4rzI1oSe69QM7P82iKmT18n/NyG1kjmg/XvvnfFvDMqC8x1lytnzZYx6OoB2wCIuKlLeSB59CLuSxGMN9SqylrB3ldg+O1E8N5TdMq/rmjVN+/bZo3n+nduV/tOfJFM3Y2le/L7NHb+o7OqhtBKMj+a+A9vDg/+V8v2z9A4LZ+B8mRabaBDtms2iGjSjmorLZxZEPZ953CfAKKUFQxqzzb1NFh+20IDxfF9pnmOCZY+e00yahVjUvB7FCs0ko87mNLltuYZcY66pt7G3qZf5Mashz5ZnH+assdXYazJm22bbZ2csF5eZlltXOFZkrDVtsm62bbZvdDyq32U4pBy0tjq+1X/t+KepQ/nJkfD509F7p93g8/KWwZY1Fs6S2Tn8JFElD1nSmEOJxWJUrDabHnGZDrs9bNM74MJitFiNYYMeRKneTt1Eg0hvgHyKjxT4DvuIr4WU77PAWmiOFjJOM5TbNBuZbDtsI7YWPHC/BWejCq+eNrHV0lRjD+MoI1dlTBiJEXo0FVhgbUh5s1ddCTiGxeugZ+08bnbUzq2cP5tJ321v87iVNgYhN0V3Onov0yiF0N3NXoGm4fv1LFZ/bETMPHZEzD16YvVBZEycQ4bEOdwnGZBgx10cic/3l5Tqs0tKwRk9ty+j1Jo65FJDVTICPwrX1thzk65RCY1b2Gn4wl6E6WvToexVjn5dy4a5rBHBEJ9/9LNodiD6l+b4vAE5PVZOKI7PfE7Jy/HOtWTxeR2PLV29chmZe+n1PQNrxlK6aoVsPToBOjOsuQndhipLbj7tQTzdB9rBs/2nC7Xs1a/kdlPriRMn6G9XxkeTOuEdpKDrNH2uBSPFJoF+a8FFTWi7WYZSs0rbzTfT/wSjchz3ovXXm9m9Oi60KReS/9+AsgeOEGsxTKsIZgMSQ8H49MN/rJx4aPXy3OtCoN/jow/hH7H5u487Lp2q2bT14O/igbh6zfOna8Y8kqcQnV7ByKajI9Bv5zCUzWg7d7M5/fq2Of0Whzn5X4cY8DfNotcDy5gDZmJ+0ZYaI13+fxunPYSs4LzCt4iaNgrpWA1mT/Z1uStWH5pYeTI+Gp/BXxw6sHXTxLcvdXz8XfyHuLzZltyHFMbQ/Xgc1/wcYFnW9c3V9xJ764fqb+TWcR9w0jL9R9xHYFRRacz0YJ6wmd8kPM9/Kwt6Hvfi3+eJjsZCdLZgMafSDFi4yVhqo7VNcC2nSp6WWaw80mRz0vrPtesy4Znh8HWyLjPzOlGUdHqdrBc4nlcFvUMQ4EpWJdEhSaJejwTCYyIZZCTrOWLAiG8hfTVLDwHvEGLCEeGMwAvXy7TO0EPCKkjdGOjfFrJOMxrU/2sU+4crB0h20W3OtE/YQYMQdP8AOKusLOkNQrKVFlA+M6f5jL5HCiRXJpdh4C/gMS8L9/GJD/vUpA9j8on2JqOVrle75gJAVMzWYlkxK8U6CukVk1Kc+ldhNVfedaFn06y6bFi3rpmlPE3Z3lKBHsx0AuhMbnEabKVytqOU1xyldJn3hQHsPJ3GbohYHGRxLWNiZrLhIIY/ybr1KPkQSx2PkbsTqONCu9Da0YV80PHby4+Sr76N8/MGgE1Xnbhf+E54l/5/fDxLe3hyZHuEZLpLMojBxwfoqSVHwBES84Vurmikn1Dm6hu5QbjBNTxSK4wPVUcWCHdwK4TN3GbhYbSNexq9wL2H3nN+ib50fen2+IQoyhf6CXyt8JB7a+S9CB925keKnaWR4e7hvopARWhEZIJcbR2fMdE3MWtC4Eb1xuzZwoyMuZE7Ivf77o984v5/CrsS8Kiqs33Oufsyc5dZ78wkM5NkJssEsi8TY3NVFgExgIAQkooKCsFCAxFEUGNrBZcC4tNK64JWa7FqgRAgIlbqWvWn0Eq1Yl3aIiKV1qdF/qLM5D/n3JkQaPv8hHvuuXfmZib3nu873/J+7/lj0lKC0Dc4dKg/nAakdLc5nGaD3mAF18KxiPGXMUJZMujnAB9nPCEOkQPAlRQWagwSSwoFKZT00LnKk6+n8ORj8548k5InH4whHTtBBo1nIgrFKvoqUEU8GcOOFMWoKHSeVqzy82sqJudGFMXhUCBODvsVSAMDz876G105ZA5YSqpsepYmSL6plB+BziFgCny2MVdyQem2mpKl7JdrlqYfefjxV1/P7t26DY59g5RhLM4c3fKtp1d+dt972T/D8B8XdM6Z/3BXak161Zx9sPPwe3Denl9lf3p4Z/aj71d1PQTT/VC+P/tuFr85+5vSC6xcjH8C1hUetNMuT5rQgn4FlZvlnmbYxDSLzVKzq8XdYDZ5ZNND1IBJGndO9l25/UidYN9AlEIsrzZWwBUKSrLlQplS4U6ajWyL2KKQ33ipOJ3tEjuVDvd083o4n+0WFykL3fPNG9mbRVJ1scJc4bmTvVu4W/4BOyjuNl9j3xDfZf8gvud+x/yUPSYecx81K3nKJKNit1X3k1YRSQux1t1BOmcz+D6vHpSNfAaf9HSawRdlhP49g9/FO/l7mr3XdY9G0ve67jJMj2c4g++RFcjryCPJHk9sOH/vio1M3iMPTd6LVT7oCwRCMdWm8/5Vu2PyBnmfzMiDcHDnVbl0xaAt8wO2PkU/oDN4UrvKlmPA8vpeipN0RerykyFrcqYr+Il1outEF+7QyNX5Gf3JI2vFaEKfpvQds2DkzsnuvzKbQmOd2P1w2R21ERSib6w0JHDYYDhtkhR/OO1xdiyhKgljZRROE8O5P5KmzHXRSNpjR9IM3lxuf6DVY/oDeA7APYbFPQobGI0FoshMK2pB/EIICuKtikx6iPRUTwCf8wTwOdJDuHdOhCAFR+o6SHK3BFCQz63AYkhqBqCEmrLqp1C+orjmElj6diaDUl9k10fjNb7sBnQG/TK79sa2KVfC72UmnzmNlFENUwqzhB0WXIxt3uPYPyI1wUvsuYrCeSuVhPcyZayXlwqsgkol6a0sTiuN3onKOO9MYZayQPlK/tLnHl1cWfqN4m+UXla6ofLRSqEx3ljeVjlOGRcfWz49Pr18oXBt/NryuZV9lYdLj8X/Vvz3UiPg532DaPtAWcQj0Ny5HgPVNHPeB/Zhj0oAg+gWu5aLRDR5bFFElf2+ukSdnAgGDwagHrADcwN9AbYSD200o5I6uQHq5AaGndwAdXIJ6QU9ezwX5jNzYT7HyQ2QMMBEGu/r1WACFEVLXtQOaB9pQxob1dq0dmw0U35BLUT0pFZE6WkoltgpS9eop6tZqcreeP3I2Ctxdk9i9/Zcfzdz5BRxWI7kinuPOHZOD+jqCRAYGbU3S52aXuL1BhrySc+RPCrXbVVqL+m9ZW3QDZdve/+Lxb/9/t6bn5z//qO/PP6jJ29ZveXZm2/aMis0NVE7r6Np2z2w9YNNEN67qe9M978O3PQ0U/HbfS/+z8uvvUzsybahY8x2/LyrmYC9ii3yFrVIE6UxJTOL5hetltZJd5Q86Xm68iXGJQVCwUD1pMp3AlwYzUBIr4VysFPslDrlTqVT7XR1i91St9ytdKvdroHkQKlGSpdKyhtLOuTZyrzkvLLe4t6SvpL75YfUjWUPVP6g+gn5KfXx0ifKdiRfTfrL8pNOUb5TnO+U5DtlTuFA7j2kU5zvlOQ7BUS8zMJ0h1iaUGU2FEv6WGV0QYgMryKrksZDrDar3brK2modsHjNilpLrI8sNmqtt5D1AraUfFgMKHul7SVv10lJrA4PQgSgDmmOfIfXX++wWmJbBsLRnQU3FKCCiE9gnYQTBcwczYNijtoe4taykdFKNARDJZbtCdbXksuryBi1gk5LRpVFee+sGLnSipGrLJopsih60xpEc/qFkgp86c5I+mAFrCCfQq6oyFcnVDizOU86xylPSEWIflS8tKJ+bu2+WtRW21eLagkTZwkIOnkGagzGnLuMZtAO+QIxSotDvkSsRKMIGo1+PS1GBYAE3GLU7aVV0zlRKPoIQJLOQMCqydFtYinIGwR40/Fu6eW5RFcq1TMCM5Fy8CkpQtLVQxNdJO5GiubIbhhDHnCionbpqMJirJyShm7qHp3hi1yxMJDKhDDkRuGm0IsP4+7iMCgqdqliuRyGZaWSzKfYMIjqBSR+6iDHaUOVakXq9ttvByOEleCSuoYJGLAPMhoLJUF3nleMh39IvpdKaVu/dteq1Tc1JO5/7UftFzVX3HfFLS90GNvUZQtXd/v9VeE7Xnxg5sLXbjnwHrwwsmjp/DEXFgcTtRNuv3z8yrJo6tJV1wendU5rKo4UeOSSuotWd3ZsvvIZSuiNbZTdbBKYbEG+7t4kao5GU5yUKJ8rUzhE6VFYWilEekZMdV7YN+B2wFx46JGeYdNj2WAgULEXCHlNBrJLpXAa1YCIlVlDzmXNnMCNQYg69uvv7NcP0RJ8ineg6cezk1PY9mteWMGWy2iiMcdYZzBGzKG5ydEfsfmOQfwDKRqv1yMFpcSC+sLeHS2pZ3lV8vBhyTI5FrC8Iilu0dSBh/EKETGsFLhLQEKoEFPuetAgtIgXuMcw43lbmCxOUi7RxhsTzTnaNHORME+83lzJ3yz0is/xe7Rd5pf811KZYpSBMlepu0wrNau8zaDJXCHeKW5iHlB/BregLcqT6k6wi9/j/jX7Dv+edIw9pn1qnuS/kiIKrfJRaavzTtKaDnna5kMtYdmtsSYwREFMCFrCbSYAcAuMC6oJbB28YzeRacOFErCCIr1c0OvhZcVIyiljOjtN7jRuMFYbdxuyIbMMgORxOA/m7K3uytFZnKxySkn1I+THkQ78P2x7GY5DvCBwkiyLiqrKumFgaZ20gwMmlukJ9nWy5o69bAjY/TRMM8UJ2BsV3Pg5J1xur8vlFvHcmZJFL74c+6X4uwIvpMzxgsmKmqG6XfTrmdiqI1UOCEHe1AgmQ/ae0l2QQMv7XIxrEP4MW2/tMlwi3yYjeRDNsKV2Ay4xbjNIwc4MW9E5OJfG/BkOv3knPOU5dR1VGdbkk11dQSz3+D9Bb3YFj55j45Efh/2OEjsZtF1zjt137g6PSmLrERgn2UifbJO2Ra+YNYDN1RjaO/Qx1vkfA/fQwQFQrcWwrffxMMPZ7Enb6q+gTBcHtwuEhwOfiGPPt456vuLQx9uFmHPWzJV3knKqg7uwqsS/G3sEB/uFavIb+0Ez2uN80vAvH74uQK8zsBshx9gYaB4ZrXIPHdplpkGlSd2+7R7i8M7OaSmSmyDiRxkL4rDOQ8NUngCJVRUzpQyclH1+z1NtbN1Tz21uuHDX1uzA80+Vv8smMw8eMd5EizOb3tqPrvv6MFq988wByluwFgDmXyR2gq62w7wTzedn8h0So7n+yZ3iGSkPw3MK+eR8R8p3aLUpnVhmMCtkZPIxD/WMvthhOnI+gPcmR0/EHcG/A5/hWZZj+SZpPMsl+FHyLHkFc6N8mPkLLzzJw2I+KSTENN8stbnaXbPZ2fwsYbZ0C7uS+5H0Gv87LK9H+M+E/+VPiz5TljmGYRGJukgiPpBELI401sKwbMKJv8gSPiCgK5bDro6oKEBmB6FmSxxLsTBFIjmKxyjmUndoBTa4oEtJACwTcEN+oiN5+5p/y9s7+SeT5p/MEUF+S3X9KT7+upEZekKIqtOK8Z5TtGI8dba+DztlgTQJv7Ajw5yCLraKrQxtc4rHNUmCUekOBklBFymA7erJjR1blioL0pJYUNBK4if9BSSMcqg/Rnfb4zk+L8p30QNyfg+P3Zk4LZTt95Pdh/06Db7gHT1S6W67kufLgLngjfkBC0WvH3+a19tKG1Iu2R8kF3++Pey8nbAdd+V6PU54hrotgrF2AP78s2w3fPHD7GO3cXvO7IXbsssz81D05uyc/BxIsHgq7N4lSi0MewEeOZ/uMANkaH1qu3GHtXDDkIZASXYE6aj7g30B7rBluDGxGy5WyFVudgFcwC9QPuRZ4uDyoiDxPB4QkqySRH1MVrD6U3iGlxianidnmRiCXqLtVIWHDDYHlUFk2VjLYp8XAdE9iIK2pErTbLmP6Du403YpihoDzLR2tJ56tzttCWtSbz5WR/IdwzTHpH7XidSh4C6XO+fxUiOJVOuSLBndHSWAs1bcp1wx0CTUbjQIztHycxoOJ0XnOm6wTsF6JULKzUVVUtk9QycBM3SSqh7KOwBpPahEY254Y8lTtc7qFvIvbpx1Lg10Qeatz2F8ytiLvwkjf87sRt9iJmfHrV69bAPcemZH5n4gtuLnVA4Au40+J3W76SbmhobH5KVwvHipxOA5ScrhHm23CtwuqBTieYQr5BFoy7S2ZV7JBSBST7P4LkPISjKLZThZEK8vk+FpGcoxyOIZiZXLlEg9JA15xjvwniXP2kPO4ku4QoFHilyoAlF+Hu4ky6HgBxAGQrVoi0icqLYpUAm5IeD4qcBykfpoQpF+EssdKX1pnXwSO2tH9DPDSN5WI03NRFoz0EOmldw08gpcOpsyF1MhkFBRPA2DcRIV+HCnlUZFlpNXqGuAjU2kwhoKcV85+vuUS8/8hg2deWM2s2WAeXrexGefPSNc/yyBpgIgLCe11vB9O1kOkka5mQymQaORNhuDE8B4Y4I5PjgLXGnMMq8M6pvETVr+htbpMGSlfPVcvTqGG6NO8k3npqtzfPO4eeoiXy/Xq67yaZyPcBaZIhA1RHGs55kWYbuQYakJgZ+KjA0IyeXWNNXrMU2fPxAM+gaHWrEtEYyRvWoaZG93+EQpRqyFmGMtBDlRLPQFvT5f0FQlqdBn4q5pqJoW0w0sAIYpqWLQx2mGrgKEvxLHBHVNw9paxBKGgqZpGEAMBQIh/SIJTgUxPJimAh/ebMDBqbtipBTQsgbhPdudQukuEhkKBTMZbC84YaH/aDHkSg7yBA3/v8lAI0Wv5HsjGzhpm4alyyCYWFMmhGkOAjaBT1acRcDmqIrc+MwO1ebs3Ky+tCueyyjhnekklnKhG/hIdtXrH5WEmmUYOP679uLIqKMvZxc/n32rVAh4s29gxdj2wA/+WsJ8mAllP//nPQPML74ax3bdG5s//uvHWbLQz51Dx1jCA0F41/vsByGnaiVcAzeW49qi26IoGi2K1EUujhC0Ad/iIdCDy/yXhbrELtcsrcv/zVC3eINrgbbYvzi0L/qeejhw2Pqz5/PA59ZfKF7BinFVWpW3mmvTbO4ybQp3HXe44Ev2K13VfW4WS3GY5MxlX8StBEsOKlBXbGWu0qewDhBBoUhQJZhDkZ/Ko8i/yKPIHSp3hfgINMBM3NAqCu3phUZdLqHuYG3qmARC+/BcDB+F2+AXkI3CNtgOGUiwHrm1J844sBBItSykkzM0yeQMKVgBOjl73nkrTcrDIK28oCUE0Coc33ROSXcOT6dn8JkjZ2M7lG2UhLvbziIY8BtBT7wYz3DYYcQOog6Ki0oZb2AEA+6onw0s3X7N1h47+48X9i5C9TPuW/7MT29c/gy3J/Pl+vb1by7L/j37zsPwhy/OuGf/Wwdf288RtsIQts+OYV9QhsdzeekAJwJZxH7CMCS9hLJ4VKVGItMpMH13AwdBkZGWSaTEZaQlv4kVKGnQ4NDxHXgPc3uZaFOpMF4PynAjE5NOKkrUAz9u8NFh+9ay0fUghhtNLQdlUlJOgwb5UjBenglnotniLOk6eB1aKC6UbgIr4Aq0UrxJWiGvgWvQncxdwlrxbulhsEm6T34G/ER+AewWtstvgFflw+D38ufgL/LX4KRcif8cOQj8chlIyk1yO8DGDGeb/nrOVlz1eX5ygscnLhIxQ/FMQzxNQI1Wci/IOZNiTPFdoWcRx6kKAdJ+kML3Bm/7U/tToGoYuN8kY8WXkGSvJMmAGfZ/sMEIZEc98QKe8gHkqvAEVyTati31SUgahOGdNnZmEHZmwrYUQzYsUpyVEpwStK5Q8MSRrpzPMqydjPS55GazqS8Ne87hbnDw8/kENPxF9oZfHklEg6nPn8suxob8Hdcvmb4crf36MPCSGtw12YVsnP0GMEEhvMZep+qj9Av1STrbFtsWQ9FYuVpcUOurLbi44NuxDTGxJdASnhiYGJ4tzlE7A53hbnGRulD/VmBReF/sbe8HwQ9Cbxce8R4p/Dg2FPMXsyk95WtgW/Rx7ES9Q/9E+WtBVlcMN+OPUKiMH4s9cFslB2Woy7Y8F9tCbIzikGJ2Lj5x1LF75GCeViHvRAyz5zkqgI64Yso10As9daiOONP/WdrzQq6PEHL9HCE/db6QU6g7NB0hj2Ihh+dIeV7IzxdxJ6WVHinhnjwA1u/zUvrFUoMZEapd80TLxgVrD3bf+NGqjvWjjSeX3/T0z3qXbc8u5F64e+rUe4c2PZ79+p7LWjJfM0/sf+Wt37/15rtgaAi8gB3u0RSPR1Zy4wH6CIDa3VCwc5RndEEYyingxuJYJUABdlyaglA4i9DDX7khvA8wYMLQKNbDXwQqQA1ohofsxOpCyFZXNjZWjYvPiE+p6mrsZq6tuplZEV9WtapxTbyval2jXoP1w24lXRiLldRXkpxWZay4vltsrvCrTTF/RXVcAT61qaY6DnxN8erqN9Umr6o2VavxJjZQyw+iJ3ZN4SB3AkYIW24YPbsjEH07NQgbbNnr8/cFAl4OVAzCpn4o1+KzA+VvQ1eE0KiG0ab+hmVJWkZppquTdrIvySQH0TRbq/AHAtFoLNbcXFtbXo6vvt/2A5/Xm0rV1CiKLJPSuz5wkACxkIr1wuhlhMsV6XvgOsDDZltr49q527j1HMtZ6ddzq1Fgf6vnBKXDOfuTOYeAEL/ocGOfJCY4MShOkig+/e/Q8BPKFYLH0ii/cvCSlfbEeJPqKUkUJ4oSDG8m3ZpLQzw2/tphXRluKj2j20G1ipvGRHM7jMeamutLa9tBXe0oI4VfTnm8VVoNfkuNqoCzUcrzopUVtxMsBPb1YBfXMBqVNpFApJGkhGFNjU0NdT5yQkjiAZkLUAo+hidFfQGPU6EOs2ueWiXv9DVc1r2kd2bX2jk7uh/uXB7co8+ftbZyenf6by90L1x5/aruhXddfd/bA8aVL91bdN+YuQq60HdR9c9v2Ldiijlzpjb5mqcj3T1m5nSRJ9G9ccbzX0m7+DJ9bVfn6kTG73po2TUrqsBjt+WwwoQjwwuvfg748RD2Beop5zLFryfYBmYss8fF0lMteHgHREM1vAzW71qEE7yKrCYkCueV4D4J+qlW8VPcsEQRwxJFDEvDiOEcyjpE3kdR1jT9I1HEsDSM8pbk3NIxp3ZR+PXllGgmQFDC/i/86Nv+R/3b/EN+1o+8/x3391/gwuJ5cGH/CLgwckB/vvMrZHOcV6mTIycAZxGYVse8GEYGu3m3kHDzahi6RC0Pyydlxg5Zg6OTRsLwB27dt/wXkwZuXDTl+63YxPjHxq4nHspchR5bs+qKdbdknsdzx6XZhczHeO7QQQQ22usUlEIVwQvQJLRS5dt8bdYka0Pho4Vcvac+3FY4xjMmfIXnivC1nmvDcwv7Cg/xvzeP8p+px4N6OSpSU740alAnoHFqB1qI3lPfD/7F/5l1NHwGaZB1eUMRRXDz3giLJ4yAuw4Q9KoGdc3W5mp9GltIE3uFdNbQaGJPG07saTSxp/lzmYisU8Gl+cmT1fI5EPr2Nhqj7TX+Hb1aQtGrNKcn0Jye4HeCPM5qYQWF52bz/gNyNUOQh+dPCKAHGrlK2sZc+u4czGplxQMzXsj+fcnbt77a85NM/Jmblj25dfmNj2cXIvGCy+FoKDya/e6T6766hHl2//6XXz/0zuug6wk81jx4Pujj3gYB6LILvRLUrCqr2rKtb1sPqg+5nnKJIVeZa5u1z2ItMkTLQtH6AtHFqFpEhj6U8npYBltIm73QO+Sx2UCCxabNRuhArGtyEOtUJFq/AUDLphkp20XGsROsKKOBiiI6sitz4Yp/5MrmvLnRfTyPbT1KZ28y3ul6BODxoLUX7gFxcArKIB/TyN9GGt3AOu2EfuJElxPaIOubpA1nhHt1g5cEXuQRr0tmGBg8HuekioGovp4usLSOoDuJuhvGv/p8BOnZv3mzJ/Td5Zd1hptrp405cID58b09i+rHXWk+LI+be829Z8hKgmVYHx3CdrQbbrVd5iB6Q0QmrHXCSb+xJdyB3yikEaSX7Im4U47KpCo9DdPyBDgOjRMnSO16J5yOposd0hT9BngtulbsllbBXnGVdA/8nniXdBqeRGFLTMJyMSWlxZ+K70KBMATs1n31qNIkIYJDdrGZhqhFkpEoywmIsLGJIKFARFdzKYHn5atdwFkJh+YOUm4ZDUJtQBQFjn8ezQEACAROTO2CItejbgjctnuuu8/9hZujdXYl5CV3L5BvhXArgO1gCRgiqyDS52Zpem+cUCWQcZ6LRGZI50iKPhc9Q1Jyrfonba2ZT6hrkzNXdfcrOdJl/BhSTgBwZzlMiiRF6tw9kdxLfPTSbnIXya10KJt6ZtO4E4mcf9iv0TiJszu2O5yWRH/4QhIR7g+kqZKW/WnkxVvIf5ZMoa4B8sVOKKWxLu4rQ08sm5VtZ+ZlfrVkZTf860ZG5DeuyHxzlfQgNp6G/oTt4buzf8V/c8hWYRupAAYWe8lFtPw3X/3L4N8WZZ/KLvzOd8hyiyuG3uQ3Y3lTQACEQSmow6aXvMHaEEILxFA4TFaJ1oKWNxi0gmGfZoVqUuZetBlIcD5Q0WZbYUKWxcBwMJgoI+ej+PxotLk/oUT2oh9jRQ1ADfrxjqJnGnhy7MPHGv6VEsHy3Vh/ZQeFQ1ImcIfPKHNiOJZxInOWOmENLdJ1TI5psK68MBUFdbGaKByVxL2qEtxzIS0KAqwvCg0Z9zwi7lUUlEVhbRw3laWjo6C6GDduqEahn8ONrphR4BVwM5wLHcac3A67PPk1XgjReJ4iACs3hoRx/8trKx754d07d9/5ve0wfcnsjovH4I0p2njmT/CTRx7AL6zBL7SQk2Nnd7AdD//x1Rf3vPEafLX3we8v6/3xumVfLeOl0/8L1z3yPnnhdfhK74P39pIXABr6SXYqbKH1Jyb4vT2W5RLcBWwddyfHBUSOE1gWsZwHQJeCGK/KGpwiEPYLhRcihrYBa8NAAM8AroQsb1BgVGlT2hWGUJHbTbT4yKEmp/6EQivxlEIapaBFg4pI4xPUqVQsj/fZ8yvvqDiRWmFaXwfaJp9wANcOHM4JQtXVrdFFh4vILepaUtTlMJTcglOsRe83tuOgs6IDKdQirP93DmQXFDVGmxoH6i56YAL72W9/e3rVj9wTNrKdXz/6yuR5JFa+FJxgW9hdePw221GwWEKnRWYxJ/DSYpmVT3NwcRtqRwhZKhlr1A4+2XqiVT/S2gqqTpLypJrqBKGDNBzCHwSzPXD9z+H6bM8JuHEL2W/JLsb2wlL4GP4cnvKRjLdLOR6yggQSDEwwSEiwLJ+oRnAzOoA/60UOhCRoic4n5gQPO1WkBKSVrtNCg3JYqZDPxRvbcqaZ+TXZmG9uyTy4hfxde/Hznk3Xeqt9DjDwVjuIXfPHEMqv8QZIYPAxCqAmfxV5ANi0/xu5+TXV2FMz9j6cnSp851+3/h+8k3jsZW5kc3RyZWFtIAplbmRvYmogCjIgMCBvYmogCjw8IAovVHlwZSAvUGFnZXMgCi9LaWRzIFsgOCAwIFIgXSAKL0NvdW50IDEgCi9NZWRpYUJveCAzIDAgUiAKL0Nyb3BCb3ggNCAwIFIgCj4+IAplbmRvYmogCjMgMCBvYmogClsgMCAwIDYxMiA3OTIgXSAKZW5kb2JqIAo0IDAgb2JqIApbIDAgMCA2MTIgNzkyIF0gCmVuZG9iaiAKNiAwIG9iaiAKPDwgCi9Qcm9jU2V0IDcgMCBSIAovRm9udCA8PCAKLzkgOSAwIFIgIAovYSAxMCAwIFIgIAovZSAxNCAwIFIgIAovZiAxNSAwIFIgIAo+PiAKL1hPYmplY3QgPDwgCi9pbWcwIDExIDAgUiAgCi9pbWcxIDE2IDAgUiAgCj4+IAo+PiAKZW5kb2JqIAo3IDAgb2JqIApbIC9QREYgL1RleHQgL0ltYWdlQiAvSW1hZ2VDIC9JbWFnZUkgIF0gCmVuZG9iaiAKOCAwIG9iaiAKPDwgCi9UeXBlIC9QYWdlIAovUGFyZW50IDIgMCBSIAovUmVzb3VyY2VzIDYgMCBSIAovQ29udGVudHMgWyA1IDAgUiBdIAo+PiAKZW5kb2JqIAo5IDAgb2JqIAo8PCAKL1R5cGUgL0ZvbnQgCi9TdWJ0eXBlIC9UcnVlVHlwZSAKL0Jhc2VGb250IC9BQUFBQUErQXJpYWwsQm9sZCAKL0ZpcnN0Q2hhciAzMiAKL0xhc3RDaGFyIDIxMSAKL1dpZHRocyAxOSAwIFIgCi9Gb250RGVzY3JpcHRvciAyMSAwIFIgCi9Ub1VuaWNvZGUgMjAgMCBSIAo+PiAKZW5kb2JqIAoxMCAwIG9iaiAKPDwgCi9UeXBlIC9Gb250IAovU3VidHlwZSAvVHJ1ZVR5cGUgCi9CYXNlRm9udCAvQUFBQUFCK0FyaWFsIAovRmlyc3RDaGFyIDMyIAovTGFzdENoYXIgMjQzIAovV2lkdGhzIDM0IDAgUiAKL0ZvbnREZXNjcmlwdG9yIDM2IDAgUiAKL1RvVW5pY29kZSAzNSAwIFIgCj4+IAplbmRvYmogCjExIDAgb2JqIAo8PCAKL1R5cGUgL1hPYmplY3QgCi9TdWJ0eXBlIC9JbWFnZSAKL05hbWUgL2ltZzAgCi9MZW5ndGggNzMzMSAKL0ZpbHRlciBbIC9GbGF0ZURlY29kZSBdIAovV2lkdGggNTM4IAovSGVpZ2h0IDE3MiAKL0JpdHNQZXJDb21wb25lbnQgOCAKL0NvbG9yU3BhY2UgMTIgMCBSIAo+PiAKc3RyZWFtCnic7V3rTyrJtv/Q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CjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjAgCjI3OCAKMCAKMCAKMCAKMCAKMCAKNzc4IApdIAplbmRvYmogCjIxIDAgb2JqIAo8PCAKL1R5cGUgL0ZvbnREZXNjcmlwdG9yIAovQXNjZW50IDkwNSAKL0NhcEhlaWdodCA1MDAgCi9EZXNjZW50IC0yMTIgCi9GbGFncyA0IAovRm9udEJCb3ggMjIgMCBSIAovRm9udE5hbWUgL0FBQUFBQStBcmlhbCxCb2xkIAovSXRhbGljQW5nbGUgMAovU3RlbVYgMCAKL1N0ZW1IIDAgCi9BdmdXaWR0aCA0NzkgCi9Gb250RmlsZTIgMjMgMCBSIAovTGVhZGluZyAwIAovTWF4V2lkdGggMjYyOCAKL01pc3NpbmdXaWR0aCA0NzkgCi9YSGVpZ2h0IDAgCj4+IAplbmRvYmogCjIyIDAgb2JqIApbIC02MjggLTM3NiAyMDAwIDEwMTggXSAKZW5kb2JqIAoyNCAwIG9iaiAKWyAKNTA3IAo1MDcgCjUwNyAKMCAKMCAKMCAKMCAKMCAKMCAKNTA3IApdIAplbmRvYmogCjI2IDAgb2JqIAo8PCAKL1R5cGUgL0ZvbnREZXNjcmlwdG9yIAovQXNjZW50IDk1MiAKL0NhcEhlaWdodCA1MDAgCi9EZXNjZW50IC0yNjkgCi9GbGFncyA0IAovRm9udEJCb3ggMjcgMCBSIAovRm9udE5hbWUgL0FBQUFBRCtDYWxpYnJpLEJvbGQgCi9JdGFsaWNBbmdsZSAwCi9TdGVtViAwIAovU3RlbUggMCAKL0F2Z1dpZHRoIDUzNiAKL0ZvbnRGaWxlMiAyOCAwIFIgCi9MZWFkaW5nIDAgCi9NYXhXaWR0aCAxNzU5IAovTWlzc2luZ1dpZHRoIDUzNiAKL1hIZWlnaHQgMCAKPj4gCmVuZG9iaiAKMjcgMCBvYmogClsgLTUxOSAtMzA2IDEyNDAgOTcxIF0gCmVuZG9iaiAKMjkgMCBvYmogClsgCjIyNiAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKNTI3IAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAowIAo1MjUgCl0gCmVuZG9iaiAKMzEgMCBvYmogCjw8IAovVHlwZSAvRm9udERlc2NyaXB0b3IgCi9Bc2NlbnQgOTUyIAovQ2FwSGVpZ2h0IDUwMCAKL0Rlc2NlbnQgLTI2OSAKL0ZsYWdzIDQgCi9Gb250QkJveCAzMiAwIFIgCi9Gb250TmFtZSAvQUFBQUFDK0NhbGlicmkgCi9JdGFsaWNBbmdsZSAwCi9TdGVtViAwIAovU3RlbUggMCAKL0F2Z1dpZHRoIDUyMSAKL0ZvbnRGaWxlMiAzMyAwIFIgCi9MZWFkaW5nIDAgCi9NYXhXaWR0aCAxNzQzIAovTWlzc2luZ1dpZHRoIDUyMSAKL1hIZWlnaHQgMCAKPj4gCmVuZG9iaiAKMzIgMCBvYmogClsgLTUwMyAtMzA3IDEyNDAgOTY0IF0gCmVuZG9iaiAKMzQgMCBvYmogClsgCjI3OCAKMCAKMCAKMCAKNTU2IAo4ODkgCjAgCjAgCjMzMyAKMzMzIAowIAo1ODQgCjI3OCAKMzMzIAoyNzggCjI3OCAKNTU2IAo1NTYgCjU1NiAKNTU2IAo1NTYgCjU1NiAKNTU2IAo1NTYgCjU1NiAKNTU2IAoyNzggCjAgCjAgCjU4NCAKMCAKMCAKMCAKNjY3IAo2NjcgCjcyMiAKNzIyIAo2NjcgCjYxMSAKNzc4IAo3MjIgCjI3OCAKNTAwIAo2NjcgCjU1NiAKODMzIAo3MjIgCjc3OCAKNjY3IAo3NzggCjcyMiAKNjY3IAo2MTEgCjcyMiAKNjY3IAo5NDQgCjY2NyAKNjY3IAo2MTEgCjAgCjAgCjAgCjAgCjU1NiAKMCAKNTU2IAo1NTYgCjUwMCAKNTU2IAo1NTYgCjI3OCAKNTU2IAo1NTYgCjIyMiAKMjIyIAo1MDAgCjIyMiAKODMzIAo1NTYgCjU1NiAKNTU2IAo1NTYgCjMzMyAKNTAwIAoyNzggCjU1NiAKNTAwIAo3MjIgCjUwMCAKNTAwIAo1MDAgCjAgCjI2MCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKMCAKNTU2IAowIAowIAowIAoyNzggCjAgCjAgCjAgCjAgCjAgCjU1NiAKXSAKZW5kb2JqIAozNiAwIG9iaiAKPDwgCi9UeXBlIC9Gb250RGVzY3JpcHRvciAKL0FzY2VudCA5MDUgCi9DYXBIZWlnaHQgNTAwIAovRGVzY2VudCAtMjEyIAovRmxhZ3MgNCAKL0ZvbnRCQm94IDM3IDAgUiAKL0ZvbnROYW1lIC9BQUFBQUIrQXJpYWwgCi9JdGFsaWNBbmdsZSAwCi9TdGVtViAwIAovU3RlbUggMCAKL0F2Z1dpZHRoIDQ0MSAKL0ZvbnRGaWxlMiAzOCAwIFIgCi9MZWFkaW5nIDAgCi9NYXhXaWR0aCAyNjY1IAovTWlzc2luZ1dpZHRoIDQ0MSAKL1hIZWlnaHQgMCAKPj4gCmVuZG9iaiAKMzcgMCBvYmogClsgLTY2NSAtMzI1IDIwMDAgMTAwNiBdIAplbmRvYmogCjM5IDAgb2JqIAooUG93ZXJlZCBCeSBDcnlzdGFsKSAKZW5kb2JqIAo0MCAwIG9iaiAKKENyeXN0YWwgUmVwb3J0cykgCmVuZG9iaiAKNDEgMCBvYmogCjw8IAovUHJvZHVjZXIgKFBvd2VyZWQgQnkgQ3J5c3RhbCkgIAovQ3JlYXRvciAoQ3J5c3RhbCBSZXBvcnRzKSAgCj4+IAplbmRvYmogCnhyZWYgCjAgNDIgCjAwMDAwMDAwMDAgNjU1MzUgZiAKMDAwMDAwMDAxNyAwMDAwMCBuIAowMDAwMDY0Njk2IDAwMDAwIG4gCjAwMDAwNjQ3OTUgMDAwMDAgbiAKMDAwMDA2NDgyOSAwMDAwMCBuIAowMDAwMDAwMTk0IDAwMDAwIG4gCjAwMDAwNjQ4NjMgMDAwMDAgbiAKMDAwMDA2NTAxMiAwMDAwMCBuIAowMDAwMDY1MDcwIDAwMDAwIG4gCjAwMDAwNjUxNjIgMDAwMDAgbiAKMDAwMDA2NTMzOSAwMDAwMCBuIAowMDAwMDY1NTEyIDAwMDAwIG4gCjAwMDAwNzMwMzkgMDAwMDAgbiAKMDAwMDA3MzA5MyAwMDAwMCBuIAowMDAwMDczNTU4IDAwMDAwIG4gCjAwMDAwNzM3MzMgMDAwMDAgbiAKMDAwMDA3MzkxMiAwMDAwMCBuIAowMDAwMDc5NTExIDAwMDAwIG4gCjAwMDAwNzk1NjUgMDAwMDAgbiAKMDAwMDA3OTcwNCAwMDAwMCBuIAowMDAwMDA1NjUzIDAwMDAwIG4gCjAwMDAwODAzNzQgMDAwMDAgbiAKMDAwMDA4MDY1MyAwMDAwMCBuIAowMDAwMDA2MzEyIDAwMDAwIG4gCjAwMDAwODA2OTYgMDAwMDAgbiAKMDAwMDAyNDEwMSAwMDAwMCBuIAowMDAwMDgwNzU4IDAwMDAwIG4gCjAwMDAwODEwMzkgMDAwMDAgbiAKMDAwMDAyNDQyNiAwMDAwMCBuIAowMDAwMDgxMDgxIDAwMDAwIG4gCjAwMDAwMzA2MzMgMDAwMDAgbiAKMDAwMDA4MTc0MSAwMDAwMCBuIAowMDAwMDgyMDE3IDAwMDAwIG4gCjAwMDAwMzA5NDMgMDAwMDAgbiAKMDAwMDA4MjA1OSAwMDAwMCBuIAowMDAwMDM3ODIyIDAwMDAwIG4gCjAwMDAwODI4NzcgMDAwMDAgbiAKMDAwMDA4MzE1MSAwMDAwMCBuIAowMDAwMDM4NjQ3IDAwMDAwIG4gCjAwMDAwODMxOTQgMDAwMDAgbiAKMDAwMDA4MzIzNCAwMDAwMCBuIAowMDAwMDgzMjcxIDAwMDAwIG4gCnRyYWlsZXIgCjw8IAovU2l6ZSA0MiAKL1Jvb3QgMSAwIFIgCi9JbmZvIDQxIDAgUiAKPj4gCnN0YXJ0eHJlZiAKODMzNTkgCiUlRU9GDQo=\" Id_Pag=\"\"/><ERROR Texto =\"\" Numero=\"0\"/></AddendaKey></Comprobante>");
                    }

                    //*********************************************//
                    //* Procesar XML recibido de servicio de SAT  *//
                    //*********************************************//
                    string stringPDF = string.Empty;
                    string stringPDFCN = string.Empty;
                    string selloSAT = string.Empty;
                    string selloSATCN = string.Empty;
                    string folioFiscal = string.Empty;
                    string folioFiscalCN = string.Empty;
                    string errorNum = string.Empty;
                    string errorText = string.Empty;
                    string errorNumCN = string.Empty;
                    string errorTextCN = string.Empty;
                    string VersionCFDI = string.Empty;
                    string subTotalSAT = string.Empty;
                    string DescuentoSAT = string.Empty;
                    string ivaSAT = string.Empty;

                    int TSAT = 1;

                    foreach (XmlNode nodoSistemaFacturacion in xmlSAT.ChildNodes)
                    {
                        if (nodoSistemaFacturacion.Name == "Comprobante")
                        {
                            TSAT = 1;
                            selloSAT = nodoSistemaFacturacion.Attributes["sello"].Value;
                            VersionCFDI = nodoSistemaFacturacion.Attributes["Version"].Value;
                            foreach (XmlNode Nodo_nivel2 in nodoSistemaFacturacion.ChildNodes)
                            {
                                if (Nodo_nivel2.Name == "AddendaKey")
                                {
                                    foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                    {
                                        if (Nodo_nivel3.Name == "PDF")
                                            stringPDF = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                        if (Nodo_nivel3.Name == "ERROR")
                                        {
                                            errorNum = Nodo_nivel3.Attributes["Numero"].Value;
                                            errorText = Nodo_nivel3.Attributes["Texto"].Value;
                                        }
                                    }

                                    nodoSistemaFacturacion.RemoveChild(Nodo_nivel2);
                                }


                            }
                        }
                        else if (nodoSistemaFacturacion.Name == "cfdi:Comprobante")
                        {
                            TSAT = 2;
                            VersionCFDI = nodoSistemaFacturacion.Attributes["Version"].Value;
                            ///Revisar                          subTotalSAT = nodoSistemaFacturacion.Attributes["SubTotal"].Value;
                            ///Revisar                          XmlNamespaceManager nsm = new XmlNamespaceManager(xmlSAT.NameTable);
                            ///Revisar                          nsm.AddNamespace("cfdi", "http://www.sat.gob.mx/cfd/3");
                            ///Revisar                          XmlNode nodeComprobante = xmlSAT.SelectSingleNode("//cfdi:Comprobante", nsm);
                            ///Revisar                          XmlNode nodeImpuestos = nodeComprobante.SelectSingleNode("cfdi:Impuestos", nsm);
                            ///Revisar                          ivaSAT = nodeImpuestos.Attributes["TotalImpuestosTrasladados"].Value;
                            ///



                            subTotalSAT = nodoSistemaFacturacion.Attributes["SubTotal"].Value;

                            try
                            {
                                DescuentoSAT = nodoSistemaFacturacion.Attributes["Descuento"].Value;
                            }
                            catch (Exception e)
                            {
                                DescuentoSAT = "0";
                            }



                            XmlNamespaceManager nsm = new XmlNamespaceManager(xmlSAT.NameTable);
                            nsm.AddNamespace("cfdi", "http://www.sat.gob.mx/cfd/3");

                            XmlNode nodeComprobante = xmlSAT.SelectSingleNode("//cfdi:Comprobante", nsm);

                            if (nodeComprobante == null)
                            {
                                foreach (XmlNode Nodo_nivel2 in nodoSistemaFacturacion.ChildNodes)
                                {
                                    if (Nodo_nivel2.Name == "cfdi:Impuestos")
                                    {
                                        foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.Attributes)
                                        {
                                            if (Nodo_nivel3.Name == "TotalImpuestosTrasladados")
                                            {
                                                ivaSAT = Nodo_nivel3.Value;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                XmlNode nodeImpuestos = nodeComprobante.SelectSingleNode("cfdi:Impuestos", nsm);
                                ivaSAT = nodeImpuestos.Attributes["TotalImpuestosTrasladados"].Value;
                            }

                            foreach (XmlNode Nodo_nivel2 in nodoSistemaFacturacion.ChildNodes)
                            {
                                if (Nodo_nivel2.Name == "AddendaKey")
                                {
                                    foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                    {
                                        if (Nodo_nivel3.Name == "PDF")
                                            stringPDF = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                        if (Nodo_nivel3.Name == "ERROR")
                                        {
                                            errorNum = Nodo_nivel3.Attributes["Numero"].Value;
                                            errorText = Nodo_nivel3.Attributes["Texto"].Value;
                                        }
                                    }

                                    nodoSistemaFacturacion.RemoveChild(Nodo_nivel2);
                                }

                                if (Nodo_nivel2.Name == "cfdi:Complemento")
                                {
                                    foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                    {
                                        if (Nodo_nivel3.Name == "tfd:TimbreFiscalDigital")
                                        {
                                            //selloSAT = Nodo_nivel3.Attributes["SelloSAT"].Value;


                                            if (VersionCFDI == "3.2")
                                            {
                                                selloSAT = Nodo_nivel3.Attributes["selloSAT"].Value;
                                            }
                                            else
                                            {
                                                selloSAT = Nodo_nivel3.Attributes["SelloSAT"].Value;
                                            }


                                            folioFiscal = Nodo_nivel3.Attributes["UUID"].Value;
                                        }
                                    }

                                }

                            }

                        }
                        if (nodoSistemaFacturacion.Name == "SistemaFacturacion")
                        {
                            foreach (XmlNode nodoXmlSAT in nodoSistemaFacturacion.ChildNodes)
                            {
                                if (nodoXmlSAT.Name == "ComprobanteCDIK")
                                {
                                    foreach (XmlNode nodo in nodoXmlSAT.ChildNodes)
                                    {
                                        if (nodo.Name == "Comprobante")
                                        {
                                            subTotalSAT = nodo.Attributes["SubTotal"].Value;
                                            VersionCFDI = nodo.Attributes["Version"].Value;
                                            TSAT = 1;
                                            selloSAT = nodo.Attributes["sello"].Value;

                                            foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                                            {

                                                if (Nodo_nivel2.Name == "cfdi:Impuestos")
                                                {
                                                    foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.Attributes)
                                                    {
                                                        if (Nodo_nivel3.Name == "TotalImpuestosTrasladados")
                                                        {
                                                            ivaSAT = Nodo_nivel3.Value;
                                                        }
                                                    }
                                                }



                                                if (Nodo_nivel2.Name == "AddendaKey")
                                                {
                                                    foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                                    {
                                                        if (Nodo_nivel3.Name == "PDF")
                                                            stringPDF = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                                        if (Nodo_nivel3.Name == "ERROR")
                                                        {
                                                            errorNum = Nodo_nivel3.Attributes["Numero"].Value;
                                                            errorText = Nodo_nivel3.Attributes["Texto"].Value;
                                                        }
                                                    }

                                                    nodo.RemoveChild(Nodo_nivel2);
                                                }
                                            }
                                        }
                                        else if (nodo.Name == "cfdi:Comprobante")
                                        {
                                            TSAT = 2;
                                            VersionCFDI = nodo.Attributes["Version"].Value;
                                            subTotalSAT = nodo.Attributes["SubTotal"].Value;


                                            foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                                            {


                                                if (Nodo_nivel2.Name == "cfdi:Impuestos")
                                                {
                                                    foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.Attributes)
                                                    {
                                                        if (Nodo_nivel3.Name == "TotalImpuestosTrasladados")
                                                        {
                                                            ivaSAT = Nodo_nivel3.Value;
                                                        }
                                                    }
                                                }

                                                if (Nodo_nivel2.Name == "AddendaKey")
                                                {
                                                    foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                                    {
                                                        if (Nodo_nivel3.Name == "PDF")
                                                            stringPDF = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                                        if (Nodo_nivel3.Name == "ERROR")
                                                        {
                                                            errorNum = Nodo_nivel3.Attributes["Numero"].Value;
                                                            errorText = Nodo_nivel3.Attributes["Texto"].Value;
                                                        }
                                                    }

                                                    nodo.RemoveChild(Nodo_nivel2);
                                                }

                                                if (Nodo_nivel2.Name == "cfdi:Complemento")
                                                {

                                                    foreach (XmlNode xNode in Nodo_nivel2)
                                                    {

                                                        if (xNode.Attributes != null
                                                           && xNode.Attributes["SelloSAT"] != null)
                                                        {
                                                            selloSAT = xNode.Attributes["SelloSAT"].Value;
                                                            folioFiscal = xNode.Attributes["UUID"].Value;
                                                            break;
                                                        }
                                                    }
                                                }

                                            }

                                        }
                                    }
                                }
                                else
                                {
                                    if (nodoXmlSAT.Name == "ComprobanteKSL")
                                    {



                                        foreach (XmlNode nodo in nodoXmlSAT.ChildNodes)
                                        {
                                            if (nodo.Name == "Comprobante")
                                            {
                                                TSAT = 1;
                                                selloSATCN = nodo.Attributes["sello"].Value;
                                                VersionCFDI = nodo.Attributes["Version"].Value;

                                                foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                                                {
                                                    if (Nodo_nivel2.Name == "AddendaKey")
                                                    {
                                                        foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                                        {
                                                            if (Nodo_nivel3.Name == "PDF")
                                                                stringPDFCN = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                                            if (Nodo_nivel3.Name == "ERROR")
                                                            {
                                                                errorNumCN = Nodo_nivel3.Attributes["Numero"].Value;
                                                                errorTextCN = Nodo_nivel3.Attributes["Texto"].Value;
                                                            }
                                                        }

                                                        nodo.RemoveChild(Nodo_nivel2);
                                                    }


                                                }
                                            }
                                            else if (nodo.Name == "cfdi:Comprobante")
                                            {
                                                VersionCFDI = nodo.Attributes["Version"].Value;
                                                TSAT = 2;
                                                foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                                                {
                                                    if (Nodo_nivel2.Name == "AddendaKey")
                                                    {
                                                        foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                                        {
                                                            if (Nodo_nivel3.Name == "PDF")
                                                                stringPDFCN = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                                            if (Nodo_nivel3.Name == "ERROR")
                                                            {
                                                                errorNumCN = Nodo_nivel3.Attributes["Numero"].Value;
                                                                errorTextCN = Nodo_nivel3.Attributes["Texto"].Value;
                                                            }
                                                        }

                                                        nodo.RemoveChild(Nodo_nivel2);
                                                    }

                                                    if (Nodo_nivel2.Name == "cfdi:Complemento")
                                                    {
                                                        XmlNode Nodo_nivel3;
                                                        Nodo_nivel3 = Nodo_nivel2.FirstChild;
                                                        //selloSATCN = Nodo_nivel3.Attributes["SelloSAT"].Value;


                                                        if (VersionCFDI == "3.2")
                                                        {
                                                            selloSATCN = Nodo_nivel3.Attributes["selloSAT"].Value;
                                                        }
                                                        else
                                                        {
                                                            selloSATCN = Nodo_nivel3.Attributes["SelloSAT"].Value;
                                                        }
                                                        folioFiscalCN = Nodo_nivel3.Attributes["UUID"].Value;
                                                    }

                                                }

                                            }
                                        }

                                    }
                                }
                            }
                        }

                    }

                    if (TSAT == 2 && tienePDF)
                    {
                        ConsultarPDF(Id_Ncr, Id_NcrSerie, ref strScript, ref strMsjError);
                        return;
                    }

                    if (errorNum != "0")
                    {
                        //this.Alerta(string.Concat("El servicio de KEY ha devuelto el siguiente error:<br/>", errorText.Replace("'", "\"")));
                        // JorgeRmzF: 23/09/2022, se cambio la referencia al objeto de Frontend por una variable de retorno
                        strMsjError = string.Concat("El servicio de KEY ha devuelto el siguiente error:<br/>", errorText.Replace("'", "\""));
                    }
                    else
                    {
                        if ((NotaCreditoNacional != null) && (errorNumCN != "0"))
                        {
                            strMsjError = string.Concat(errorTextCN.Replace("'", "\""));
                        }
                        else
                        {
                            //notaCredito.Ncr_Sello = selloSAT;
                            notaCredito.Ncr_Sello = string.IsNullOrEmpty(selloSAT) ? notaCredito.Ncr_Sello : selloSAT;
                            notaCredito.Ncr_SelloCN = selloSATCN;
                            //notaCredito.Ncr_FolioFiscal = folioFiscal;
                            notaCredito.Ncr_FolioFiscal = string.IsNullOrEmpty(folioFiscal) ? notaCredito.Ncr_FolioFiscal : folioFiscal;

                            System.Data.SqlTypes.SqlXml sqlXml;
                            System.Data.SqlTypes.SqlXml sqlXmlCN;

                            if (xmlSAT.SelectNodes("SistemaFacturacion").Count > 0)
                            {
                                //sqlXml = sqlXml.Value.Replace("ComprobanteCDIK", "").;
                                sqlXml = new System.Data.SqlTypes.SqlXml(new XmlTextReader(xmlSAT.SelectSingleNode("//SistemaFacturacion//ComprobanteCDIK").OuterXml.Replace("<ComprobanteCDIK>", "").Replace("</ComprobanteCDIK>", ""), XmlNodeType.Document, null));
                                sqlXmlCN = new System.Data.SqlTypes.SqlXml(new XmlTextReader(xmlSAT.SelectSingleNode("//SistemaFacturacion//ComprobanteKSL").OuterXml.Replace("<ComprobanteKSL>", "").Replace("</ComprobanteKSL>", ""), XmlNodeType.Document, null));
                                notaCredito.Ncr_FolioCN = int.Parse(xmlSAT.SelectSingleNode("//SistemaFacturacion//ComprobanteKSL").FirstChild.Attributes["Folio"].Value == string.Empty ? "0" : xmlSAT.SelectSingleNode("//SistemaFacturacion//ComprobanteKSL").FirstChild.Attributes["Folio"].Value).ToString();
                            }
                            else
                            {
                                sqlXml = new System.Data.SqlTypes.SqlXml(new XmlTextReader(xmlSAT.OuterXml, XmlNodeType.Document, null));
                                sqlXmlCN = null;
                                notaCredito.Ncr_FolioCN = null;
                            }

                            if (movimiento != "CANCELACION")
                            {
                                notaCredito.Ncr_Xml = sqlXml;
                                notaCredito.Ncr_XmlCN = sqlXmlCN;
                                notaCredito.Ncr_FolioFiscal = folioFiscal;
                                notaCredito.Ncr_FolioFiscalCN = folioFiscalCN;
                            }

                            notaCredito.Ncr_Pdf = this.Base64ToByte(stringPDF);
                            notaCredito.Ncr_PdfCN = this.Base64ToByte(stringPDFCN);


                            // ---------------------------------------------------------------------------------------------
                            // Guarda la factrura en el directorio 
                            // ---------------------------------------------------------------------------------------------
                            // ---------------------------------------------------------------------------------------------
                            // Se actualiza el estatus de la factura a Impreso (I)
                            // ---------------------------------------------------------------------------------------------
                            verificador = 0;
                            if (movimiento != "CANCELACION")
                            {
                                notaCredito.Ncr_Estatus = "I";
                            }
                            else
                            {
                                notaCredito.Ncr_Estatus = "B";
                            }
                            new CN_CapNotaCredito().ModificarNotaCreditoSAT(notaCredito, sesion.Emp_Cnx, ref verificador);

                            // ------------------------------
                            // Abrir PDF de Nota de Credito
                            // ------------------------------
                            string tempPDFname = string.Concat("NOTACREDITO_"
                                    , notaCredito.Id_Emp.ToString()
                                    , "_", notaCredito.Id_Cd.ToString()
                                    , "_", notaCredito.Id_U.ToString()
                                    , ".pdf");
                            string URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                            string WebURLtempPDF = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFname);

                            string tempPDFCNname = string.Concat("NOTACREDITO_", notaCredito.Id_Emp.ToString(), "_", notaCredito.Id_Cd.ToString(), "_", notaCredito.Id_Ncr.ToString(), ".pdf");
                            string URLtempPDFCN = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFCNname));
                            string WebURLtempPDFCN = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFCNname);

                            if (!string.IsNullOrEmpty(stringPDF))
                                this.ByteToTempPDF(URLtempPDF, this.Base64ToByte(stringPDF));

                            #region xml

                            NotaCredito Nota = new NotaCredito();
                            Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                            Nota.Id_Emp = Sesion.Id_Emp;
                            Nota.Id_Cd = Sesion.Id_Cd_Ver;
                            Nota.Id_Ncr = Id_Ncr;
                            Nota.Id_NcrSerie = Id_NcrSerie;

                            CN_CapNotaCredito NC = new CN_CapNotaCredito();
                            NC.ArchivoPdf_Xml(ref Nota, Sesion.Emp_Cnx);
                            string ruta = null;
                            string rutaCN = null;
                            System.IO.StreamWriter sw2 = null;
                            ruta = Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "NC" + Id_Ncr.ToString() + ".txt";
                            rutaCN = Server.MapPath("Reportes") + "\\archivoXmlCN" + Sesion.Id_U.ToString() + "NC" + Id_Ncr.ToString() + ".txt";

                            if (File.Exists(ruta))
                                File.Delete(ruta);
                            if (File.Exists(Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "NC" + Id_Ncr.ToString() + ".xml"))
                                File.Delete(Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "NC" + Id_Ncr.ToString() + ".xml");
                            sw2 = new System.IO.StreamWriter(ruta, false, Encoding.UTF8);
                            sw2.WriteLine(Nota.Ncr_Xml.ToString());
                            sw2.Close();
                            File.Move(ruta, Server.MapPath("Reportes") + "\\archivoXml" + Sesion.Id_U.ToString() + "NC" + Id_Ncr.ToString() + ".xml");

                            if ((Nota.Ncr_XmlCN != null) && (Nota.Ncr_XmlCN != string.Empty))
                            {
                                if (File.Exists(rutaCN))
                                {
                                    File.Delete(rutaCN);
                                }

                                if (File.Exists(Server.MapPath("Reportes") + "\\archivoXmlCN" + Sesion.Id_U.ToString() + "NC" + Id_Ncr.ToString() + ".xml"))
                                {
                                    File.Delete(Server.MapPath("Reportes") + "\\archivoXmlCN" + Sesion.Id_U.ToString() + "NC" + Id_Ncr.ToString() + ".xml");
                                }

                                sw2 = new System.IO.StreamWriter(rutaCN, false, Encoding.UTF8);
                                sw2.WriteLine(Nota.Ncr_XmlCN.ToString());
                                sw2.Close();

                                File.Move(rutaCN, Server.MapPath("Reportes") + "\\archivoXmlCN" + Sesion.Id_U.ToString() + "NC" + Id_Ncr.ToString() + ".xml");

                            }


                            #endregion


                            if (esNcrNacional)
                            {
                                if (!string.IsNullOrEmpty(stringPDFCN))
                                    this.ByteToTempPDF(URLtempPDFCN, this.Base64ToByte(stringPDFCN));
                                // ------------------------------------------------------------------------------------------------
                                // Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                                // ------------------------------------------------------------------------------------------------
                                strScript += string.Concat(@"AbrirNotaCreditoPDFCN('" + WebURLtempPDF + "','" + WebURLtempPDFCN + "')");
                            }
                            else
                            {
                                // if (notaCredito.Id_Tm != 5) 
                                strScript += string.Concat(@"AbrirNotaCreditoPDFCN('" + WebURLtempPDF + "','')");
                            }

                        }
                    }
                }
                else
                {
                    //RAM1.ResponseScripts.Add("OpenAlert('Los montos de la Nota de Crédito y la Nota de Crédito Especial no coinciden','" + Id_Emp + "','" + Id_Cd + "','" + Id_Ncr + "','" + Id_NcrSerie + "','" + 1 + "')");
                    // JorgeRmzF: 23/09/2022, se cambio la referencia al objeto de Frontend por una variable de retorno
                    strScript = "OpenAlert('Los montos de la Nota de Crédito y la Nota de Crédito Especial no coinciden','" + Id_Emp + "','" + Id_Cd + "','" + Id_Ncr + "','" + Id_NcrSerie + "','" + 1 + "')";

                    //RAM1.ResponseScripts.Add(string.Concat(@"AbrirVentana_NotaCargo_Edicion('", Id_Emp, "','", Id_Cd, "','", Id_NcaSerie, "','", Id_Nca, "','", "1", "')"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private byte[] Base64ToByte(string data)
        {
            byte[] filebytes = null;
            try
            {
                if (!string.IsNullOrEmpty(data))
                {
                    filebytes = Convert.FromBase64String(data);
                }
                return filebytes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ByteToTempPDF(string tempPath, byte[] filebytes)
        {
            if (File.Exists(tempPath))
            {
                File.Delete(tempPath);
            }
            FileStream fs = new FileStream(tempPath,
                FileMode.CreateNew,
                FileAccess.Write,
                FileShare.None);
            fs.Write(filebytes, 0, filebytes.Length);
            fs.Close();
        }
        private void ShowTempPDF(string tempPath_archivoPDF)
        {
            Process proc = new Process();
            proc.StartInfo = new ProcessStartInfo(tempPath_archivoPDF);
            proc.Start();
            while (!proc.HasExited)
            {
                System.Threading.Thread.Sleep(200);
            }
        }
        private void Inicializar()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            this.CargarCentros();
            this.cmbEstatus.Sort = RadComboBoxSort.Ascending;
            this.cmbEstatus.SortItems();


            /*
            //Cargar grid de ordenes de compra
            if (sesion.CalendarioIni >= txtFecha1.MinDate && sesion.CalendarioIni <= txtFecha1.MaxDate)
            {
                txtFecha1.DbSelectedDate = sesion.CalendarioIni;
            }
            if (sesion.CalendarioFin >= txtFecha2.MinDate && sesion.CalendarioFin <= txtFecha2.MaxDate)
            {
                txtFecha2.DbSelectedDate = sesion.CalendarioFin;
            }*/



            if (DateTime.Now > txtFecha2.SelectedDate)
            {
                txtFecha1.DbSelectedDate = sesion.CalendarioFin;
                txtFecha2.DbSelectedDate = sesion.CalendarioFin;
                //txtFecha1.DbSelectedDate = DateTime.Now;
                //txtFecha2.DbSelectedDate = DateTime.Now;
            }
            else
            {
                txtFecha1.DbSelectedDate = DateTime.Now;
                txtFecha2.DbSelectedDate = DateTime.Now;
            }


            double ancho = 0;
            foreach (GridColumn gc in rgNotaCredito.Columns)
            {
                if (gc.Display)
                {
                    ancho = ancho + gc.HeaderStyle.Width.Value;
                }
            }
            rgNotaCredito.Width = Unit.Pixel(Convert.ToInt32(ancho));
            rgNotaCredito.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
            rgNotaCredito.Rebind();
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

                    if (Permiso.PGrabar == false)
                        this.RadToolBar1.FindItemByValue("new").Visible = false;
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
                if (mensaje.Contains("TempPDFNoData"))
                Alerta("El archivo PDF no contiene datos.");
            else
                    if (mensaje.Contains("CapNotaCredito_Movimiento5"))
                Alerta("Esta nota de crédito tiene el movimiento 5. Se debe cancelar desde las devoluciones parciales, movimiento generado en forma automática; para cancelar este movimiento es necesario cancelar la devolución");
            else
                        if (mensaje.Contains("CapNotaCredito_Movimiento4"))
                Alerta("Esta nota de crédito tiene el movimiento 4. Se debe cancelar desde la factura, movimiento generado en forma automática; para cancelar este movimiento es necesario cancelar la factura");
            else
                            if (mensaje.Contains("PermisoEliminarDenegado"))
                Alerta("Operación denegada, no tiene permisos para dar de baja notas de crédito");
            else
                                if (mensaje.Contains("PermisoImprimirDenegado"))
                Alerta("Operación denegada, no tiene permisos para imprimir notas de crédito");
            else
                                    if (mensaje.Contains("CapNotaCredito_Modificar_Denegado"))
                Alerta("Imposible modificar el documento");
            else
                                        if (mensaje.Contains("CapNotaCredito_delete_ok"))
                Alerta("La nota de crédito se ha dado de baja (estatus \"B\") correctamente");
            else
                                            if (mensaje.Contains("CapNotaCredito_EstatusIncorrecto"))
                Alerta("No se puede realizar la operación. El estatus es incorrecto");
            else
                                                if (mensaje.Contains("CapNotaCredito_EsBaja"))
                Alerta("La nota de crédito ya está dada de baja");
            else
                                                    if (mensaje.Contains("CapNotaCredito_delete_error"))
                Alerta("Error al momento de dar de baja la nota de crédito");
            else
                                                        if (mensaje.Contains("CapNotaCredito_print_error"))
                Alerta(string.Concat("Error al imprimir la nota de crédito. ", mensaje.Replace("'", "\"")));
            else
                                                            if (mensaje.Contains("RAM1_AjaxRequest"))
                Alerta("Error al momento de actualizar el grid de notas de crédito");
            else
                Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
        }
        private void ValidarEstatusFactura(int Id_Emp, int Id_Cd, int Id_Ncr, string Id_NcrSerie, ref int Verificador)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CapNotaCredito cn_nc = new CN_CapNotaCredito();
                cn_nc.ValidarEstatusFactura(Id_Emp, Id_Cd, Id_Ncr, Id_NcrSerie, sesion.Emp_Cnx, ref Verificador);

            }
            catch (Exception ex)
            {

                throw ex;
            }
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