using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using CapaNegocios;
using CapaEntidad;
using Telerik.Web.UI;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Data.OleDb;
using CapaDatos;

namespace SIANWEB
{
    public partial class CapPagosTimbresDet : System.Web.UI.Page
    {
        #region Variables
        public string NombreArchivo;
        public string NombreHojaExcel;

        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        private DataTable dtDetHist
        {
            get
            {
                return (DataTable)Session["dtDetPagosdet" + Session.SessionID];
            }
            set
            {
                Session["dtDetPagosdet" + Session.SessionID] = value;
            }
        }

        private class Timbre
        {
            public CapaEntidad.Clientes Cliente { get; set; }
            public List<CapaEntidad.Factura> Facturas { get; set; }
        }
        #endregion
        private string Emp_CnxCob
        {
            get { return ConfigurationManager.AppSettings.Get("strConnectionCobranza"); }
        }

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (Sesion == null)
                    CerrarVentana();
                else
                {
                    if (!Page.IsPostBack)
                    {
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            return;
                        }
                        Session["PosBackPagos" + Session.SessionID] = "1";
                        Inicializar();

                        double ancho = 0;
                        foreach (GridColumn gc in RgDetcompl.Columns)
                        {
                            if (gc.Display)
                            {
                                ancho = ancho + gc.HeaderStyle.Width.Value;
                            }
                        }
                        RgDetcompl.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        RgDetcompl.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));

                        ancho = 0;

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }



        protected void RgDet_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {

                RgDetcompl.DataSource = dtDetHist;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void RgDet_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                int Id_PagtimbDet = -1;
                GridItem item = (GridItem)e.Item;
                if (item.Cells[7].Text != "&nbsp;")
                {
                    Int32.TryParse(item.Cells[7].Text, out Id_PagtimbDet);
                }

                PagoDetComplemento pagoDetComplemento = new PagoDetComplemento();
                object pagoPDF = null;
                pagoDetComplemento.Id_Emp = sesion.Id_Emp;
                pagoDetComplemento.Id_Cd = sesion.Id_Cd_Ver;
                pagoDetComplemento.Id_Pag = Convert.ToInt32(HF_ID.Value);
                pagoDetComplemento.Id_Cte = Convert.ToInt32(HF_CTE.Value);
                pagoDetComplemento.Id_PagDet = Convert.ToInt32(HF_RegPga.Value);
                pagoDetComplemento.Id_PagDetTimb = Id_PagtimbDet;

                new CN_CapPago().ConsultaComplementoPagoConsultaDetlista(ref pagoDetComplemento, ref pagoPDF, sesion.Emp_Cnx);

                if (pagoDetComplemento == null || pagoDetComplemento.Cancelacion_XML == null)
                {
                    item.FindControl("xmlCancelacion").Visible = false;
                }
                else
                {
                    item.FindControl("xmlCancelacion").Visible = true;
                    Label estatus = item.FindControl("lblestatus") as Label;
                    estatus.Text = "Cancelado";

                }
                if (pagoDetComplemento == null || pagoDetComplemento.Pago_Xml == null)
                {
                    item.FindControl("ImageButton2").Visible = false;
                    item.FindControl("ImageButton3").Visible = false;
                    item.FindControl("gbcEnviar").Visible = false;
                }

            }
        }

        protected void RgDet_ItemCommand(object source, GridCommandEventArgs e)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            int Id_Emp;
            int Id_Cd;
            int Id_Fac;
            int Id_Cte;
            int Id_PagDet;
            int Id_PagtimbDet = -1;
            try
            {
                ErrorManager();
                Int32 item = default(Int32);
                if (e.Item == null) return;
                item = e.Item.ItemIndex;
                GridItem gi = e.Item;
                if (item >= 0)
                {

                    Int32.TryParse(gi.Cells[6].Text, out Id_Fac);

                    if (gi.Cells[7].Text != "")
                    {
                        Int32.TryParse(gi.Cells[7].Text, out Id_PagtimbDet);
                    }

                    DateTime fechaPeriodoInicio = sesion.CalendarioIni;
                    DateTime fechaPeriodoFinal = sesion.CalendarioFin;

                    Id_Emp = sesion.Id_Emp;
                    Id_Cd = sesion.Id_Cd_Ver;
                    Id_Cte = Convert.ToInt32(HF_CTE.Value);
                    Id_PagDet = Convert.ToInt32(HF_RegPga.Value);
                    Id_Fac = Convert.ToInt32(HF_IDFac.Value);

                    switch (e.CommandName)
                    {
                        case "PDF":

                            this.descargarPDF(Id_Cte, Id_PagDet, Id_PagtimbDet);

                            break;
                        case "XML":
                            descargarXML(Id_Cte, Id_PagDet, Id_PagtimbDet);
                            break;
                        case "xmlCancelacion":
                            descargarXMLCancelacion(Id_Cte, Id_PagDet, Id_PagtimbDet);
                            break;
                        case "Enviar":
                            RAM1.ResponseScripts.Add(string.Concat(@"AbrirVentana_EnviarPagos('" + Id_Emp + "','" + Id_Cd + "','" + Id_Cte + "','" + HF_ID.Value + "','" + Id_Fac + "','" + Id_PagDet + "','" + HF_Serie.Value + "', '" + Id_PagtimbDet + "')"));
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void RgDet_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                RgDetcompl.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void RgDet_ItemCreated(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridEditableItem && e.Item.IsInEditMode)
                {

                    GridEditableItem item = e.Item as GridEditableItem;
                    (item.FindControl("rgReferencia") as RadTextBox).Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                //
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        #endregion

        #region Funciones    

        private string Serialize(object o)
        {
            var xs = new XmlSerializer(o.GetType());
            var xml = new StringWriter();
            xs.Serialize(xml, o);

            return xml.ToString();
        }

        private void cargarPago()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                CN_CapPago cn_cappago = new CN_CapPago();
                PagoDetComplemento pagoDetComplemento = new PagoDetComplemento();
                List<PagoDetComplemento> ListaDocumentos = new List<PagoDetComplemento>();

                object pagoPDF = null;
                pagoDetComplemento.Id_Emp = Sesion.Id_Emp;
                pagoDetComplemento.Id_Cd = Sesion.Id_Cd_Ver;
                pagoDetComplemento.Id_Pag = Convert.ToInt32(HF_ID.Value);
                pagoDetComplemento.Id_Cte = Convert.ToInt32(HF_CTE.Value);
                pagoDetComplemento.Id_PagDet = Convert.ToInt32(HF_RegPga.Value);
                new CN_CapPago().ConsultaDetalleComplementoPagoListaDocs(pagoDetComplemento, ref ListaDocumentos, ref pagoPDF, Sesion.Emp_Cnx);

                for (int x = 0; x < ListaDocumentos.Count; x++)
                {
                    dtDetHist.Rows.Add(
                        ListaDocumentos[x].Id_Pag,
                        ListaDocumentos[x].Id_Cte,
                        ListaDocumentos[x].Id_PagComp,
                        ListaDocumentos[x].Pago_Serie,
                        ListaDocumentos[x].Pago_FolioFiscal,
                        ListaDocumentos[x].Id_PagDetTimb,
                        ListaDocumentos[x].Id_PagDet,
                        ListaDocumentos[x].FechaCreacionXML,
                        "Activo"
                        );
                }
                RgDetcompl.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void Inicializar()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            Funciones funcion = new Funciones();

            GetListDet();
            if ((Request.QueryString["Id"] != null && Request.QueryString["Id"] != "-1") &&
                (Request.QueryString["idCte"] != null && Request.QueryString["idCte"] != "-1") &&
                (Request.QueryString["idPagDte"] != null && Request.QueryString["idPagDte"] != "-1"))
            {
                HF_IDFac.Value = Request.QueryString["IdFac"];
                HF_Serie.Value = Request.QueryString["serie"];
                HF_ID.Value = Request.QueryString["Id"];
                HF_CTE.Value = Request.QueryString["idCte"];
                HF_RegPga.Value = Request.QueryString["idPagDte"];
                cargarPago();
            }
            HF_PageName.Value = Guid.NewGuid().ToString().Replace("-", "");
            ViewState["PageName"] = HF_PageName.Value;
            _PermisoImprimir = Convert.ToBoolean(Request.QueryString["PermisoImprimir"]);
            HF_Timbrado.Value = "0";

        }

        private void CerrarVentana()
        {
            try
            {
                string funcion;
                if (this.HiddenRebind.Value == "0")
                    funcion = "CloseWindow()";
                else
                    funcion = "CloseAndRebind()";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GetListDet()
        {
            try
            {
                dtDetHist = new DataTable();
                dtDetHist.Columns.Add("Id_Pag", System.Type.GetType("System.Int32"));
                dtDetHist.Columns.Add("Id_Cte", System.Type.GetType("System.Int32"));
                dtDetHist.Columns.Add("Id_PagComp", System.Type.GetType("System.Int32"));
                dtDetHist.Columns.Add("Pago_Serie", System.Type.GetType("System.String"));
                dtDetHist.Columns.Add("Pago_FolioFiscal", System.Type.GetType("System.String"));
                dtDetHist.Columns.Add("Id_PagDetTimb", System.Type.GetType("System.Int32"));
                dtDetHist.Columns.Add("Id_PagDet", System.Type.GetType("System.Int32"));
                dtDetHist.Columns.Add("FechaCreacionXML", System.Type.GetType("System.DateTime"));
                dtDetHist.Columns.Add("Estatus", System.Type.GetType("System.String"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DeshabilitarDetalle()
        {
            try
            {

                foreach (GridDataItem item in RgDetcompl.MasterTableView.Items)
                {
                    ImageButton ImgEliminar = (ImageButton)item["Accion"].Controls[1];
                    ImgEliminar.Visible = false;

                }

                foreach (GridHeaderItem headerItem in this.RgDetcompl.MasterTableView.GetItems(GridItemType.Header))
                {

                    RadNumericTextBox rgCte = (RadNumericTextBox)headerItem["Id_Cte"].Controls[1];
                    RadTextBox rgtxtReferencia = (RadTextBox)headerItem["Pag_Referencia"].Controls[1]; ;
                    RadTextBox rgtxtFolioFiscal = rgCte.Parent.FindControl("rgFolioFiscal") as RadTextBox;
                    RadTextBox rgSerie = rgCte.Parent.FindControl("rgSerie") as RadTextBox;
                    RadNumericTextBox rgCdi = rgSerie.Parent.FindControl("rgCdi") as RadNumericTextBox;
                    RadComboBox rgcmbNum = rgCte.Parent.FindControl("rgcmbNum") as RadComboBox;
                    RadComboBox rgcmbMov = rgCte.Parent.FindControl("rgcmbMov") as RadComboBox;
                    RadNumericTextBox rgTerr = rgtxtReferencia.Parent.FindControl("rgTerr") as RadNumericTextBox;
                    RadDatePicker rdFecha = rgtxtReferencia.Parent.FindControl("rdFecha") as RadDatePicker;
                    RadTextBox rgCteNombre = rgtxtReferencia.Parent.FindControl("rgCteNombre") as RadTextBox;
                    RadTextBox rgEstatus = rgtxtReferencia.Parent.FindControl("rgEstatus") as RadTextBox;
                    RadNumericTextBox rgImporte = rgtxtReferencia.Parent.FindControl("rgImporte") as RadNumericTextBox;
                    RadNumericTextBox rgPagado = rgtxtReferencia.Parent.FindControl("rgPagado") as RadNumericTextBox;
                    RadNumericTextBox rgtxtImporte = rgtxtReferencia.Parent.FindControl("rgtxtImporte") as RadNumericTextBox;
                    RadTextBox rgCheque = rgSerie.Parent.FindControl("rgCheque") as RadTextBox;
                    ImageButton ImgAgregar = rgSerie.Parent.FindControl("BtnAgregar") as ImageButton;


                    rgCte.Visible = false;
                    rgtxtReferencia.Visible = false;
                    rgtxtFolioFiscal.Visible = false;
                    rgSerie.Visible = false;
                    rgCdi.Visible = false;
                    rgcmbNum.Visible = false;
                    rgcmbMov.Visible = false;
                    rgTerr.Visible = false;
                    rdFecha.Visible = false;
                    rgCteNombre.Visible = false;
                    rgEstatus.Visible = false;
                    rgImporte.Visible = false;
                    rgPagado.Visible = false;
                    rgtxtImporte.Visible = false;
                    rgCheque.Visible = false;
                    ImgAgregar.Visible = false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion

        #region imprimir

        private void descargarXML(int Id_Cte, int Id_PagDet, int Id_PagDetTimb)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            PagoDetComplemento pagoDetComplemento = new PagoDetComplemento();
            object pagoPDF = null;
            pagoDetComplemento.Id_Emp = sesion.Id_Emp;
            pagoDetComplemento.Id_Cd = sesion.Id_Cd_Ver;
            pagoDetComplemento.Id_Pag = Convert.ToInt32(HF_ID.Value);
            pagoDetComplemento.Id_Cte = Id_Cte;
            pagoDetComplemento.Id_PagDet = Id_PagDet;
            pagoDetComplemento.Id_PagDetTimb = Id_PagDetTimb;
            new CN_CapPago().ConsultaComplementoPagoConsultaDetlista(ref pagoDetComplemento, ref pagoPDF, sesion.Emp_Cnx);

            if (pagoDetComplemento == null)
            {
                Alerta("El pago no ha sido timbrado");
            }
            else
            {
                if (pagoDetComplemento.Pago_Xml == null)
                    Alerta("El xml no fue generado al momento de timbrar. Favor de timbrar de nuevo");
                else
                {
                    string ruta = null;
                    string rutaCN = null;

                    System.IO.StreamWriter sw = null;
                    ruta = Server.MapPath("Reportes") + "\\archivoXml" + sesion.Id_U.ToString() + "Pago" + pagoDetComplemento.Id_PagComp.ToString() + ".txt";
                    rutaCN = Server.MapPath("Reportes") + "\\archivoXmlCN" + sesion.Id_U.ToString() + "Pago" + pagoDetComplemento.Id_PagComp.ToString() + ".txt";

                    if (File.Exists(ruta))
                        File.Delete(ruta);
                    if (File.Exists(Server.MapPath("Reportes") + "\\archivoXml" + sesion.Id_U.ToString() + "Pago" + pagoDetComplemento.Id_PagComp.ToString() + ".xml"))
                        File.Delete(Server.MapPath("Reportes") + "\\archivoXml" + sesion.Id_U.ToString() + "Pago" + pagoDetComplemento.Id_PagComp.ToString() + ".xml");
                    Encoding utf8WithoutBom = new System.Text.UTF8Encoding(false);
                    sw = new System.IO.StreamWriter(ruta, false, utf8WithoutBom);

                    sw.WriteLine(pagoDetComplemento.Pago_Xml.ToString());
                    sw.Close();
                    File.Move(ruta, Server.MapPath("Reportes") + "\\archivoXml" + sesion.Id_U.ToString() + "Pago" + pagoDetComplemento.Id_PagComp.ToString() + ".xml");

                    if (File.Exists(Server.MapPath("Reportes") + "\\archivoXmlCN" + sesion.Id_U.ToString() + "Pago" + pagoDetComplemento.Id_PagComp.ToString() + ".xml"))
                    {
                        File.Delete(Server.MapPath("Reportes") + "\\archivoXmlCN" + sesion.Id_U.ToString() + "Pago" + pagoDetComplemento.Id_PagComp.ToString() + ".xml");
                    }

                    sw = new System.IO.StreamWriter(rutaCN, false, utf8WithoutBom);
                    sw.WriteLine(pagoDetComplemento.Pago_Xml.ToString());
                    sw.Close();

                    File.Move(rutaCN, Server.MapPath("Reportes") + "\\archivoXmlCN" + sesion.Id_U.ToString() + "Pago" + pagoDetComplemento.Id_PagComp.ToString() + ".xml");

                    RAM1.ResponseScripts.Add(string.Concat(@"abrirArchivoCN('Reportes\\archivoXml" + sesion.Id_U.ToString() + "Pago", pagoDetComplemento.Id_PagComp.ToString(), ".xml'", ",", @"'Reportes\\archivoXmlCN" + sesion.Id_U.ToString() + "Pago", pagoDetComplemento.Id_PagComp.ToString(), ".xml')"));

                }

            }
        }

        private void descargarXMLCancelacion(int Id_Cte, int Id_PagDet, int Id_PagDetTimb)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            PagoDetComplemento pagoDetComplemento = new PagoDetComplemento();
            object pagoPDF = null;
            pagoDetComplemento.Id_Emp = sesion.Id_Emp;
            pagoDetComplemento.Id_Cd = sesion.Id_Cd_Ver;
            pagoDetComplemento.Id_Pag = Convert.ToInt32(HF_ID.Value);
            pagoDetComplemento.Id_Cte = Id_Cte;
            pagoDetComplemento.Id_PagDet = Id_PagDet;
            pagoDetComplemento.Id_PagDetTimb = Id_PagDetTimb;
            new CN_CapPago().ConsultaComplementoPagoConsultaDetlista(ref pagoDetComplemento, ref pagoPDF, sesion.Emp_Cnx);

            if (pagoDetComplemento == null)
            {
                Alerta("El pago no ha sido timbrado");
            }
            else
            {
                if (pagoDetComplemento.Cancelacion_XML == null)
                    Alerta("El xml no fue generado al momento de cancelar. Favor de cancelar de nuevo");
                else
                {
                    string ruta = null;

                    System.IO.StreamWriter sw = null;
                    ruta = Server.MapPath("Reportes") + "\\archivoXml" + sesion.Id_U.ToString() + "CancelacionPago" + pagoDetComplemento.Id_PagComp.ToString() + ".txt";

                    if (File.Exists(ruta))
                        File.Delete(ruta);
                    if (File.Exists(Server.MapPath("Reportes") + "\\archivoXml" + sesion.Id_U.ToString() + "CancelacionPago" + pagoDetComplemento.Id_PagComp.ToString() + ".xml"))
                        File.Delete(Server.MapPath("Reportes") + "\\archivoXml" + sesion.Id_U.ToString() + "CancelacionPago" + pagoDetComplemento.Id_PagComp.ToString() + ".xml");
                    Encoding utf8WithoutBom = new System.Text.UTF8Encoding(false);
                    sw = new System.IO.StreamWriter(ruta, false, utf8WithoutBom);
                    sw.WriteLine(pagoDetComplemento.Cancelacion_XML.ToString());
                    sw.Close();
                    File.Move(ruta, Server.MapPath("Reportes") + "\\archivoXml" + sesion.Id_U.ToString() + "CancelacionPago" + pagoDetComplemento.Id_PagComp.ToString() + ".xml");

                    RAM1.ResponseScripts.Add(string.Concat(@"abrirArchivo('Reportes\\archivoXml" + sesion.Id_U.ToString() + "CancelacionPago", pagoDetComplemento.Id_PagComp.ToString(), ".xml')"));//, ",", @"'Reportes\\archivoXmlCN" + sesion.Id_U.ToString() + "Fac", pagoDetComplemento.Id_PagComp.ToString(), ".xml')"));

                }

            }
        }

        private void descargarPDF(int Id_Cte, int Id_PagDet, int Id_PagDetTimb)
        {

            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            try
            {

                PagoDetComplemento pagoDetComplemento = new PagoDetComplemento();
                object pagoPDF = null;
                pagoDetComplemento.Id_Emp = sesion.Id_Emp;
                pagoDetComplemento.Id_Cd = sesion.Id_Cd_Ver;
                pagoDetComplemento.Id_Pag = Convert.ToInt32(HF_ID.Value);
                pagoDetComplemento.Id_Cte = Id_Cte;
                pagoDetComplemento.Id_PagDet = Id_PagDet;
                pagoDetComplemento.Id_PagDetTimb = Id_PagDetTimb;
                new CN_CapPago().ConsultaComplementoPagoConsultaDetlista(ref pagoDetComplemento, ref pagoPDF, sesion.Emp_Cnx);

                if (pagoDetComplemento == null)
                {
                    Alerta("El pago no ha sido timbrado");
                }
                else
                {
                    if (pagoPDF == null)
                        Alerta("El pdf no fue generado al momento de timbrar. Favor de timbrar de nuevo");
                    else
                    {
                        byte[] archivoPdf = (byte[])pagoPDF;
                        byte[] archivoPdfCN = pagoPDF != System.DBNull.Value ? (byte[])pagoPDF : new byte[0];
                        if (archivoPdf.Length > 0)
                        {
                            string tempPDFname = "PAGO";
                            tempPDFname = tempPDFname + pagoDetComplemento.Id_Emp.ToString() + "_" + pagoDetComplemento.Id_Cd.ToString() + "_" + pagoDetComplemento.Id_PagComp.ToString() + ".pdf";
                            string URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                            string WebURLtempPDF = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFname);
                            this.ByteToTempPDF(URLtempPDF, archivoPdf);
                            RAM1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDFVarias('", WebURLtempPDF, "')"));
                        }
                    }
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

        #endregion imprimir

        #region ErrorManager
        private void AlertaFocus(string mensaje, string rtb)
        {
            try
            {
                //RAM1.ResponseScripts.Add("AlertaFocus('" + mensaje + "','" + rtb + "');");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
        private void Alerta(string mensaje, int largo = 330, int ancho = 150)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', " + largo.ToString() + "," + ancho.ToString() + ");");
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