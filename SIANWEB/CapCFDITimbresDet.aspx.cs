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
    public partial class CapCFDITimbresDet : System.Web.UI.Page
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
             }
        }

        protected void RgDet_ItemCommand(object source, GridCommandEventArgs e)
        {
           
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

        private void Inicializar()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            Funciones funcion = new Funciones();
            if (Sesion == null)
            {
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (!Page.IsPostBack)
                {

                    CartaPorte carta = new CartaPorte();
                    carta.Id_Emp = Convert.ToInt32(Page.Request.QueryString["Id_Emp"]);
                    carta.Id_Cd = Convert.ToInt32(Page.Request.QueryString["Id_Cd"]);
                    carta.Id_Doc = Convert.ToInt32(Page.Request.QueryString["Id_Doc"]);
                    carta.TipoDoc = Page.Request.QueryString["TipoDoc"];


                    GetListDet(carta);
                    if (carta.Id_CFDI == 0)
                    {
                        Alerta("No existe historial para este docuemnto. ");
                        return;
                    }
                }
            }
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

        private void GetListDet(CartaPorte carta)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            try
            {
                CN_CapRemision CN = new CN_CapRemision();
                DataSet dtCfdi = new DataSet();
                new CN_CapRemision().ConsultaCDFI(sesion, ref carta, ref dtCfdi);
                RgDetcompl.DataSource = dtCfdi.Tables[0].Select("Cfdi_Estatus = '0' ");
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