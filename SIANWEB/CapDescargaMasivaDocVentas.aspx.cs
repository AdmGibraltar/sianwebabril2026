using CapaEntidad;
using CapaNegocios;
using DevExpress.Export;
using DevExpress.Web;
using DevExpress.XtraPrinting;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace SIANWEB
{
    public partial class CapDescargaMasivaDocVentas : System.Web.UI.Page
    {
        #region Variables 

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

        private string Emp_CnxCentral
        {
            get { return ConfigurationManager.AppSettings.Get("strConnectionCentral"); }
        }

        #endregion 

        #region Mensajes 
        private void ShowMessage(string Message, string type)
        {
            string strFn = "setTimeout(function() { ShowMessage('" + Message + "','" + type + "'); }, 500);";
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), strFn, true);
        }
        private void sucess(string mensaje)
        {
            ShowMessage(mensaje, "Success");
        }
        private void danger(string mensaje)
        {
            ShowMessage(mensaje, "Error");
        }
        private void warning(string mensaje)
        {
            ShowMessage(mensaje, "Warning");
        }
        private void info(string mensaje)
        {
            ShowMessage(mensaje, "Info");
        }
        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (session == null)
                {
                    if (!Page.IsCallback)
                    {
                        string[] pag = Page.Request.Url.ToString().Trim().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                        Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                        Response.Redirect("login.aspx", false);
                    }
                }
                else
                {
                    if (!IsPostBack)
                    {
                        datePeriodo.SelectedDate = DateTime.Today;
                        CargarClientes(); //LOCAL

                    }
                }
            }
            catch (Exception ex)
            {
                warning(ex.Message + "- " + new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
            Session["activeMenu"] = 5;
        }

        private void CargarClientes() //LOCAL
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion != null)
                {
                    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                    CN_Comun.DevLlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatCliente_Combo", ref cmbCliente);
                    cmbCliente.Items[0].Text = "Todos";
                    cmbCliente.SelectedIndex = 0;
                    //cmbCliente.Items[-1].Text = "todos";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void BtnDescargar_ServerClick(object sender, EventArgs e)
        {
            try
            {
                //string strPeriodo = string.Empty;
                string strTipoDoc = string.Empty;
                string strFormato = string.Empty;
                int intTipoDoc;
                int intFormato;
                int intIdCte;
                List<object> Temp2 = new List<object>();
                List<object> Temp = new List<object>();
                List<int> lstIntTipoDoc = new List<int>();
                List<int> lstIntFormato = new List<int>();
                DateTime fechaPeriodo;
                string strUrlFacPdfZip = string.Empty;
                string strNameFacPdfZip = string.Empty;
                string strUrlFacXmlZip = string.Empty;
                string strNameFacXmlZip = string.Empty;

                string strUrlPagoPdfZip = string.Empty;
                string strNamePagoPdfZip = string.Empty;
                string strUrlPagoXmlZip = string.Empty;
                string strNamePagoXmlZip = string.Empty;

                string strUrlNCargoPdfZip = string.Empty;
                string strNameNCargoPdfZip = string.Empty;
                string strUrlNCargoXmlZip = string.Empty;
                string strNameNCargoXmlZip = string.Empty;

                string strUrlNCreditoPdfZip = string.Empty;
                string strNameNCreditoPdfZip = string.Empty;
                string strUrlNCreditoXmlZip = string.Empty;
                string strNameNCreditoXmlZip = string.Empty;
                string strMensaje = string.Empty;


                foreach (ListEditItem itemTipoDoc in listBoxTipoDoc.SelectedItems)
                {
                    Temp.Add(itemTipoDoc.Value);
                    lstIntTipoDoc.Add(int.Parse(itemTipoDoc.Value.ToString()));
                }

                foreach (ListEditItem li in ListBoxFormato.SelectedItems)
                {
                    Temp2.Add(li.Value);
                    lstIntFormato.Add(int.Parse(li.Value.ToString()));
                }

                if (datePeriodo.Value == null)
                {
                    warning("Seleccione el período");

                }
                else
                if (lstIntTipoDoc.Count == 0)
                {
                    warning("Seleccione el tipo de documento");

                }
                else if (lstIntFormato.Count == 0)
                {
                    warning("Seleccione el formato a descargar");

                }
                else
                {
                    intIdCte = int.Parse(cmbCliente.SelectedItem.Value.ToString());
                    fechaPeriodo = DateTime.Parse(datePeriodo.Value.ToString());

                    GetDocumentos(intIdCte, fechaPeriodo, lstIntTipoDoc, lstIntFormato,
                        ref strUrlFacPdfZip, ref strNameFacPdfZip, ref strUrlFacXmlZip, ref strNameFacXmlZip,
                        ref strUrlPagoPdfZip, ref strNamePagoPdfZip, ref strUrlPagoXmlZip, ref strNamePagoXmlZip,
                        ref strUrlNCargoPdfZip, ref strNameNCargoPdfZip, ref strUrlNCargoXmlZip, ref strNameNCargoXmlZip,
                        ref strUrlNCreditoPdfZip, ref strNameNCreditoPdfZip, ref strUrlNCreditoXmlZip, ref strNameNCreditoXmlZip, ref strMensaje);
                    string strFuncionJs = "DescargarZip('"
                                                    + strNameFacPdfZip + "','" + strUrlFacPdfZip + "','" + strNameFacXmlZip + "','" + strUrlFacXmlZip + "','"
                                                    + strNamePagoPdfZip + "','" + strUrlPagoPdfZip + "','" + strNamePagoXmlZip + "','" + strUrlPagoXmlZip + "','"
                                                    + strNameNCargoPdfZip + "','" + strUrlNCargoPdfZip + "','" + strNameNCargoXmlZip + "','" + strUrlNCargoXmlZip + "','"
                                                    + strNameNCreditoPdfZip + "','" + strUrlNCreditoPdfZip + "','" + strNameNCreditoXmlZip + "','" + strUrlNCreditoXmlZip + "','" + strMensaje + "');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), strFuncionJs, true);
                }


            }
            catch (Exception ex)
            {
                danger("Error al crear archivo: " + ex.Message);
            }
        }

        private enum enumTipoDocumento
        {
            ComplementoPago = 1, Factura = 2, NotaCargo = 3, NotaCredito = 4
        }

        private enum enumFormato
        {
            PDF = 1, XML = 2
        }

        private void GetDocumentos(int intIdCte, DateTime fechaPeriodo, List<int> lstIntTipoDoc, List<int> lstIntFormato,
            ref string strUrlFacPdfZip, ref string strNameFacPdfZip, ref string strUrlFacXmlZip, ref string strNameFacXmlZip,
            ref string strUrlPagoPdfZip, ref string strNamePagoPdfZip, ref string strUrlPagoXmlZip, ref string strNamePagoXmlZip,
            ref string strUrlNCargoPdfZip, ref string strNameNCargoPdfZip, ref string strUrlNCargoXmlZip, ref string strNameNCargoXmlZip,
            ref string strUrlNCreditoPdfZip, ref string strNameNCreditoPdfZip, ref string strUrlNCreditoXmlZip, ref string strNameNCreditoXmlZip,
            ref string strMensaje)
        {
            try
            {
                bool boolPDF = false;
                bool boolXML = false;
                string strFileZip = string.Empty;

                foreach (var itemIntFormato in lstIntFormato)
                {
                    switch (itemIntFormato)
                    {
                        case (int)enumFormato.PDF:
                            boolPDF = true;
                            break;
                        case (int)enumFormato.XML:
                            boolXML = true;
                            break;
                    }
                }

                foreach (var itemIntTipoDoc in lstIntTipoDoc)
                {
                    switch (itemIntTipoDoc)
                    {
                        case (int)enumTipoDocumento.ComplementoPago:
                            DescargarComplementoPago(intIdCte, fechaPeriodo, boolPDF, boolXML, ref strUrlPagoPdfZip, ref strNamePagoPdfZip, ref strUrlPagoXmlZip, ref strNamePagoXmlZip, ref strMensaje);
                            break;
                        case (int)enumTipoDocumento.Factura:
                            DescargarFactura(intIdCte, fechaPeriodo, boolPDF, boolXML, ref strUrlFacPdfZip, ref strNameFacPdfZip, ref strUrlFacXmlZip, ref strNameFacXmlZip, ref strMensaje);
                            break;
                        case (int)enumTipoDocumento.NotaCargo:
                            DescargarNotaCargo(intIdCte, fechaPeriodo, boolPDF, boolXML, ref strUrlNCargoPdfZip, ref strNameNCargoPdfZip, ref strUrlNCargoXmlZip, ref strNameNCargoXmlZip, ref strMensaje);
                            break;
                        case (int)enumTipoDocumento.NotaCredito:
                            DescargarNotaCredito(intIdCte, fechaPeriodo, boolPDF, boolXML, ref strUrlNCreditoPdfZip, ref strNameNCreditoPdfZip, ref strUrlNCreditoXmlZip, ref strNameNCreditoXmlZip, ref strMensaje);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DescargarNotaCredito(int intIdCte, DateTime fechaPeriodo, bool boolPDF, bool boolXML, ref string strUrlNCreditoPdfZip, ref string strNameNCreditoPdfZip, ref string strUrlNCreditoXmlZip, ref string strNameNCreditoXmlZip, ref string strMensaje)
        {
            try
            {
                List<entDocVentas> lstDocVenta = new List<entDocVentas>();
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                string tempPDFname = string.Empty;
                string tempXMLname = string.Empty;
                string URLtempPDF = string.Empty;
                string URLtempXML = string.Empty;
                string strServerUrlFileZip;
                string strServerUrlXmlZip;
                string strAnioMes = fechaPeriodo.ToString("yyyyMM");
                string strCarpetaAnioMes = string.Empty;
                string strPeriodo = fechaPeriodo.ToString("yyyy-MM-dd HH:mm:ss.fff");

                if (intIdCte != -1)
                {
                    strAnioMes = intIdCte.ToString() + "_" + strAnioMes;
                }
                byte[] archivoPdf;
                List<string> arrFilePDF = new List<string>();
                List<string> arrFileXML = new List<string>();

                CN_CapDescargaMasivaDocVentas cnDescargaDoc = new CN_CapDescargaMasivaDocVentas();
                cnDescargaDoc.ConsultaNotaCredito(Sesion.Emp_Cnx, Sesion.Id_Emp, Sesion.Id_Cd, intIdCte, strPeriodo, boolPDF, boolXML, ref lstDocVenta);

                if (boolPDF)
                {
                    strCarpetaAnioMes = "NCredito_pdf_" + strAnioMes;
                    CrearDirectorio(strCarpetaAnioMes);

                    foreach (var itemDocVenta in lstDocVenta)
                    {
                        tempPDFname = string.Concat("NCredito_"
                            , itemDocVenta.Id_Ncr.ToString()
                            , ".pdf");

                        URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), strCarpetaAnioMes, "//", tempPDFname));

                        if (File.Exists(URLtempPDF))
                        {
                            arrFilePDF.Add(URLtempPDF);
                        }
                        else
                        {
                            if (itemDocVenta.objDocPDF != System.DBNull.Value)
                            {
                                archivoPdf = (byte[])itemDocVenta.objDocPDF;
                                this.ByteToTempPDF(URLtempPDF, archivoPdf);
                                arrFilePDF.Add(URLtempPDF);
                            }
                        }
                    }

                    if (arrFilePDF.Count > 0)
                    {
                        using (ZipFile zip = new ZipFile())
                        {
                            //zip.AddFiles(arrFilePDF, "file");

                            strNameNCreditoPdfZip = string.Concat("NCredito_pdf_", strAnioMes, ".zip");
                            strServerUrlFileZip = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), strNameNCreditoPdfZip));
                            strUrlNCreditoPdfZip = string.Concat(ConfigurationManager.AppSettings["AccesoPortal"].ToString(), "/xmlSAT/", strNameNCreditoPdfZip);
                            // utf-8
                            zip.AddDirectory(Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), strCarpetaAnioMes)));
                            zip.Save(strServerUrlFileZip);
                        }
                    }
                    else
                    {
                        strMensaje += "No hay PDF de Notas de crédito en el período seleccionado.<br>";
                    }
                }

                if (boolXML)
                {
                    strCarpetaAnioMes = "NCredito_xml_" + strAnioMes;
                    CrearDirectorio(strCarpetaAnioMes);
                    foreach (var itemDocVenta in lstDocVenta)
                    {
                        tempXMLname = string.Concat("NCredito_"
                            , itemDocVenta.Id_Ncr.ToString()
                            , ".xml");

                        URLtempXML = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), strCarpetaAnioMes, "//", tempXMLname));

                        XmlDocument doc = new XmlDocument();

                        if (File.Exists(URLtempXML))
                        {
                            arrFileXML.Add(URLtempXML);
                        }
                        else
                        {
                            if (itemDocVenta.objDocXML != System.DBNull.Value)
                            {
                                doc.LoadXml(itemDocVenta.objDocXML.ToString());
                                doc.Save(URLtempXML);
                                arrFileXML.Add(URLtempXML);
                            }
                        }
                    }
                    if (arrFileXML.Count > 0)
                    {
                        using (ZipFile zip = new ZipFile())
                        {
                            //zip.AddFiles(arrFileXML, "file");
                            strNameNCreditoXmlZip = string.Concat("NCredito_xml_", strAnioMes, ".zip");
                            strServerUrlXmlZip = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), strNameNCreditoXmlZip));
                            strUrlNCreditoXmlZip = string.Concat(ConfigurationManager.AppSettings["AccesoPortal"].ToString(), "/xmlSAT/", strNameNCreditoXmlZip);
                            zip.AddDirectory(Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), strCarpetaAnioMes)));
                            zip.Save(strServerUrlXmlZip);

                        }
                    }
                    else
                    {
                        strMensaje += "No hay XML de Notas de crédito en el período seleccionado.<br>";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DescargarNotaCargo(int intIdCte, DateTime fechaPeriodo, bool boolPDF, bool boolXML, ref string strUrlNCargoPdfZip, ref string strNameNCargoPdfZip, ref string strUrlNCargoXmlZip, ref string strNameNCargoXmlZip, ref string strMensaje)
        {
            try
            {
                List<entDocVentas> lstDocVenta = new List<entDocVentas>();
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                string tempPDFname = string.Empty;
                string tempXMLname = string.Empty;
                string URLtempPDF = string.Empty;
                string URLtempXML = string.Empty;
                string strServerUrlFileZip;
                string strServerUrlXmlZip;
                string strAnioMes = fechaPeriodo.ToString("yyyyMM");
                string strCarpetaAnioMes = string.Empty;
                string strPeriodo = fechaPeriodo.ToString("yyyy-MM-dd HH:mm:ss.fff");

                byte[] archivoPdf;
                List<string> arrFilePDF = new List<string>();
                List<string> arrFileXML = new List<string>();

                CN_CapDescargaMasivaDocVentas cnDescargaDoc = new CN_CapDescargaMasivaDocVentas();
                cnDescargaDoc.ConsultaNotaCargo(Sesion.Emp_Cnx, Sesion.Id_Emp, Sesion.Id_Cd, intIdCte, strPeriodo, boolPDF, boolXML, ref lstDocVenta);

                if (intIdCte != -1)
                {
                    strAnioMes = intIdCte.ToString() + "_" + strAnioMes;
                }

                if (boolPDF)
                {
                    strCarpetaAnioMes = "NCargo_pdf_" + strAnioMes;
                    CrearDirectorio(strCarpetaAnioMes);
                    foreach (var itemDocVenta in lstDocVenta)
                    {
                        tempPDFname = string.Concat("NCargo_"
                            , itemDocVenta.Id_Nca.ToString()
                            , ".pdf");

                        URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), strCarpetaAnioMes, "//", tempPDFname));

                        if (File.Exists(URLtempPDF))
                        {
                            arrFilePDF.Add(URLtempPDF);
                        }
                        else
                        {
                            if (itemDocVenta.objDocPDF != System.DBNull.Value)
                            {
                                archivoPdf = (byte[])itemDocVenta.objDocPDF;
                                this.ByteToTempPDF(URLtempPDF, archivoPdf);
                                arrFilePDF.Add(URLtempPDF);
                            }
                        }
                    }

                    if (arrFilePDF.Count > 0)
                    {
                        using (ZipFile zip = new ZipFile())
                        {
                            //zip.AddFiles(arrFilePDF, "file");
                            strNameNCargoPdfZip = string.Concat("NCargo_pdf_", strAnioMes, ".zip");
                            strServerUrlFileZip = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), strNameNCargoPdfZip));
                            strUrlNCargoPdfZip = string.Concat(ConfigurationManager.AppSettings["AccesoPortal"].ToString(), "/xmlSAT/", strNameNCargoPdfZip);
                            zip.AddDirectory(Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), strCarpetaAnioMes)));
                            zip.Save(strServerUrlFileZip);
                        }
                    }
                    else
                    {
                        strMensaje += "No hay PDF de Notas de cargo en el período seleccionado.<br>";
                    }
                }

                if (boolXML)
                {
                    strCarpetaAnioMes = "NCargo_xml_" + strAnioMes;
                    CrearDirectorio(strCarpetaAnioMes);
                    foreach (var itemDocVenta in lstDocVenta)
                    {
                        tempXMLname = string.Concat("NCargo_"
                            , itemDocVenta.Id_Nca.ToString()
                            , ".xml");

                        URLtempXML = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), strCarpetaAnioMes, "//", tempXMLname));

                        XmlDocument doc = new XmlDocument();

                        if (File.Exists(URLtempXML))
                        {
                            arrFileXML.Add(URLtempXML);
                        }
                        else
                        {
                            if (itemDocVenta.objDocXML != System.DBNull.Value)
                            {
                                doc.LoadXml(itemDocVenta.objDocXML.ToString());
                                doc.Save(URLtempXML);
                                arrFileXML.Add(URLtempXML);
                            }
                        }
                    }
                    if (arrFileXML.Count > 0)
                    {
                        using (ZipFile zip = new ZipFile())
                        {
                            //zip.AddFiles(arrFileXML, "file");
                            strNameNCargoXmlZip = string.Concat("NCargo_xml_", strAnioMes, ".zip");
                            strServerUrlXmlZip = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), strNameNCargoXmlZip));
                            strUrlNCargoXmlZip = string.Concat(ConfigurationManager.AppSettings["AccesoPortal"].ToString(), "/xmlSAT/", strNameNCargoXmlZip);
                            zip.AddDirectory(Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), strCarpetaAnioMes)));
                            zip.Save(strServerUrlXmlZip);

                        }
                    }
                    else
                    {
                        strMensaje += "No hay XML de Notas de cargo en el período seleccionado.<br>";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DescargarFactura(int intIdCte, DateTime fechaPeriodo, bool boolPDF, bool boolXML, ref string strUrlPdfZip, ref string strNamePdfZip, ref string strUrlXmlZip, ref string strNameXmlZip, ref string strMensaje)
        {
            try
            {
                List<entDocVentas> lstDocVenta = new List<entDocVentas>();
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                string tempPDFname = string.Empty;
                string tempXMLname = string.Empty;
                string URLtempPDF = string.Empty;
                string URLtempXML = string.Empty;
                string strAnioMes = fechaPeriodo.ToString("yyyyMM");
                string strCarpetaAnioMes = string.Empty;
                string strServerUrlFileZip;
                string strServerUrlXmlZip;
                string strPeriodo = fechaPeriodo.ToString("yyyy-MM-dd HH:mm:ss.fff");

                byte[] archivoPdf;
                List<string> arrFilePDF = new List<string>();
                List<string> arrFileXML = new List<string>();

                CN_CapDescargaMasivaDocVentas cnDescargaDoc = new CN_CapDescargaMasivaDocVentas();
                cnDescargaDoc.ConsultaFacturas(Sesion.Emp_Cnx, Sesion.Id_Emp, Sesion.Id_Cd, intIdCte, strPeriodo, boolPDF, boolXML, ref lstDocVenta);

                if (intIdCte != -1)
                {
                    strAnioMes = intIdCte.ToString() + "_" + strAnioMes;
                }

                if (boolPDF)
                {
                    strCarpetaAnioMes = "Factura_pdf_" + strAnioMes;
                    CrearDirectorio(strCarpetaAnioMes);
                    foreach (var itemDocVenta in lstDocVenta)
                    {
                        tempPDFname = string.Concat("Factura_"
                            , itemDocVenta.Id_Fac.ToString()
                            , ".pdf");

                        URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), strCarpetaAnioMes, "//", tempPDFname));

                        if (File.Exists(URLtempPDF))
                        {
                            arrFilePDF.Add(URLtempPDF);
                        }
                        else
                        {
                            if (itemDocVenta.objDocPDF != System.DBNull.Value)
                            {
                                archivoPdf = (byte[])itemDocVenta.objDocPDF;
                                this.ByteToTempPDF(URLtempPDF, archivoPdf);
                                arrFilePDF.Add(URLtempPDF);
                            }
                        }
                    }

                    if (arrFilePDF.Count > 0)
                    {
                        using (ZipFile zip = new ZipFile())
                        {
                            //zip.AddFiles(arrFilePDF, "file");
                            strNamePdfZip = string.Concat("Factura_pdf_", strAnioMes, ".zip");
                            strServerUrlFileZip = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), strNamePdfZip));
                            strUrlPdfZip = string.Concat(ConfigurationManager.AppSettings["AccesoPortal"].ToString(), "/xmlSAT/", strNamePdfZip);
                            zip.AddDirectory(Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), strCarpetaAnioMes)));
                            zip.Save(strServerUrlFileZip);
                        }
                    }
                    else
                    {
                        strMensaje += "No hay PDF de Factura en el período seleccionado.<br>";
                    }
                }

                if (boolXML)
                {
                    strCarpetaAnioMes = "Factura_xml_" + strAnioMes;
                    CrearDirectorio(strCarpetaAnioMes);
                    foreach (var itemDocVenta in lstDocVenta)
                    {
                        tempXMLname = string.Concat("Factura_"
                            , itemDocVenta.Id_Fac.ToString()
                            , ".xml");

                        URLtempXML = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), strCarpetaAnioMes, "//", tempXMLname));

                        XmlDocument doc = new XmlDocument();

                        if (File.Exists(URLtempXML))
                        {
                            arrFileXML.Add(URLtempXML);
                        }
                        else
                        {
                            if (itemDocVenta.objDocXML != System.DBNull.Value)
                            {
                                doc.LoadXml(itemDocVenta.objDocXML.ToString());
                                doc.Save(URLtempXML);
                                arrFileXML.Add(URLtempXML);
                            }
                        }
                    }
                    if (arrFileXML.Count > 0)
                    {
                        using (ZipFile zip = new ZipFile())
                        {
                            //zip.AddFiles(arrFileXML, "file");
                            strNameXmlZip = string.Concat("Factura_xml_", strAnioMes, ".zip");
                            strServerUrlXmlZip = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), strNameXmlZip));
                            strUrlXmlZip = string.Concat(ConfigurationManager.AppSettings["AccesoPortal"].ToString(), "/xmlSAT/", strNameXmlZip);
                            zip.AddDirectory(Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), strCarpetaAnioMes)));
                            zip.Save(strServerUrlXmlZip);
                        }
                    }
                    else
                    {
                        strMensaje += "No hay XML de Factura en el período seleccionado.<br>";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        private void DescargarComplementoPago(int intIdCte, DateTime fechaPeriodo, bool boolPDF, bool boolXML, ref string strUrlPagoPdfZip, ref string strNamePagoPdfZip, ref string strUrlPagoXmlZip, ref string strNamePagoXmlZip, ref string strMensaje)
        {
            try
            {
                List<entDocVentas> lstDocVenta = new List<entDocVentas>();
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                string tempPDFname = string.Empty;
                string tempXMLname = string.Empty;
                string URLtempPDF = string.Empty;
                string URLtempXML = string.Empty;
                string strServerUrlFileZip;
                string strServerUrlXmlZip;
                string strDirectorio = string.Empty;
                string strAnioMes = fechaPeriodo.ToString("yyyyMM");
                string strCarpetaAnioMes = string.Empty;
                string strPeriodo = fechaPeriodo.ToString("yyyy-MM-dd HH:mm:ss.fff");

                byte[] archivoPdf;
                List<string> arrFilePDF = new List<string>();
                List<string> arrFileXML = new List<string>();

                CN_CapDescargaMasivaDocVentas cnDescargaDoc = new CN_CapDescargaMasivaDocVentas();
                cnDescargaDoc.ConsultaComplementoPago(Sesion.Emp_Cnx, Sesion.Id_Emp, Sesion.Id_Cd, intIdCte, strPeriodo, boolPDF, boolXML, ref lstDocVenta);

                if (intIdCte != -1)
                {
                    strAnioMes = intIdCte.ToString() + "_" + strAnioMes;
                }

                if (boolPDF)
                {
                    strCarpetaAnioMes = "Pago_pdf_" + strAnioMes;
                    CrearDirectorio(strCarpetaAnioMes);

                    foreach (var itemDocVenta in lstDocVenta)
                    {
                        tempPDFname = string.Concat("Pago_"
                            , itemDocVenta.Id_PagDet.ToString()
                            , ".pdf");

                        URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), strCarpetaAnioMes, "//", tempPDFname));

                        if (arrFilePDF.Any(x => x == URLtempPDF))
                        {
                            continue;
                        }

                        if (File.Exists(URLtempPDF))
                        {
                            arrFilePDF.Add(URLtempPDF);
                        }
                        else
                        {
                            if (itemDocVenta.objDocPDF != System.DBNull.Value)
                            {
                                archivoPdf = (byte[])itemDocVenta.objDocPDF;
                                this.ByteToTempPDF(URLtempPDF, archivoPdf);
                                arrFilePDF.Add(URLtempPDF);
                            }
                        }
                    }

                    if (arrFilePDF.Count > 0)
                    {
                        using (ZipFile zip = new ZipFile())
                        {
                            //zip.AddFiles(arrFilePDF, "file");
                            strNamePagoPdfZip = string.Concat("ComplementoPago_pdf_", strAnioMes, ".zip");
                            strServerUrlFileZip = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), strNamePagoPdfZip));
                            strUrlPagoPdfZip = string.Concat(ConfigurationManager.AppSettings["AccesoPortal"].ToString(), "/xmlSAT/", strNamePagoPdfZip);
                            zip.AddDirectory(Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), strCarpetaAnioMes)));
                            zip.Save(strServerUrlFileZip);
                        }
                    }
                    else
                    {
                        strMensaje += "No hay PDF de Complemento de pago en el período seleccionado.<br>";
                    }
                }

                if (boolXML)
                {
                    strCarpetaAnioMes = "Pago_xml_" + strAnioMes;
                    CrearDirectorio(strCarpetaAnioMes);

                    foreach (var itemDocVenta in lstDocVenta)
                    {
                        tempXMLname = string.Concat("Pago_"
                            , itemDocVenta.Id_PagDet.ToString()
                            , ".xml");

                        URLtempXML = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), strCarpetaAnioMes, "//", tempXMLname));

                        XmlDocument doc = new XmlDocument();

                        if (arrFileXML.Any(x => x == URLtempXML))
                        {
                            continue;
                        }
                        if (File.Exists(URLtempXML))
                        {
                            arrFileXML.Add(URLtempXML);
                        }
                        else
                        {
                            if (itemDocVenta.objDocXML != System.DBNull.Value)
                            {
                                doc.LoadXml(itemDocVenta.objDocXML.ToString());
                                doc.Save(URLtempXML);
                                arrFileXML.Add(URLtempXML);
                            }
                        }
                    }
                    if (arrFileXML.Count > 0)
                    {
                        using (ZipFile zip = new ZipFile())
                        {
                            //zip.AddFiles(arrFileXML, "file");
                            strNamePagoXmlZip = string.Concat("ComplementoPago_xml_", strAnioMes, ".zip");
                            strServerUrlXmlZip = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), strNamePagoXmlZip));
                            strUrlPagoXmlZip = string.Concat(ConfigurationManager.AppSettings["AccesoPortal"].ToString(), "/xmlSAT/", strNamePagoXmlZip);
                            zip.AddDirectory(Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), strCarpetaAnioMes)));
                            zip.Save(strServerUrlXmlZip);

                        }
                    }
                    else
                    {
                        strMensaje += "No hay XML de Complemento de pago en el período seleccionado.<br>";
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CrearDirectorio(string strCarpeta)
        {
            string strDirectorio = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), strCarpeta));
            if (Directory.Exists(strDirectorio))
            {
                Directory.Delete(strDirectorio, true);
            }
            Directory.CreateDirectory(strDirectorio);
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


    }
}