using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaEntidad;
using Telerik.Web.UI;
using CapaNegocios;
using System.Collections;
using CapaDatos;
using System.Web.Services;
using System.IO;
using System.Xml.Serialization;
using System.Configuration;
using System.Data.OleDb;
using System.Xml;
using System.Text;
using System.Net;
using System.Web.UI.HtmlControls;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Net.Http;
using Newtonsoft.Json;
using SIANWEB.Core.UI;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;

namespace SIANWEB
{
    public partial class CapPagosTimbres : BaseServerPage
    {
        #region Variables
        public string NombreArchivo;
        public string NombreHojaExcel;

        public string urlApp = ConfigurationManager.AppSettings["AccesoPortal"].ToString();
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private DataTable dtGral
        {
            get { return (DataTable)Session["dtGralPagos" + Session.SessionID + Request.Params["IdPagina"].ToString()]; }
            set { Session["dtGralPagos" + Session.SessionID + Request.Params["IdPagina"].ToString()] = value; }
        }

        private DataTable dtDetInicial
        {
            get
            {
                return (DataTable)Session["dtDetPagosInicial" + Session.SessionID + Request.Params["IdPagina"].ToString()];
            }
            set
            {
                Session["dtDetPagosInicial" + Session.SessionID + Request.Params["IdPagina"].ToString()] = value;
            }
        }

        private DataTable dtDet
        {
            get
            {
                return (DataTable)Session["dtDetPagos" + Session.SessionID + Request.Params["IdPagina"].ToString()];
            }
            set
            {
                Session["dtDetPagos" + Session.SessionID + Request.Params["IdPagina"].ToString()] = value;
            }
        }
        private bool banco
        {
            get
            {
                bool? _banco = (bool?)Session["banco" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]];
                return _banco ?? false;
            }
            set
            {
                Session["banco" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value;
            }
        }

        private class Timbre
        {
            public CapaEntidad.Clientes Cliente { get; set; }
            public List<CapaEntidad.Factura> Facturas { get; set; }
            public TipoMoneda TipoMoneda { get; set; }
            public Banco_Ficha PFicha { get; set; }
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
                //bool postback = (bool)Session["PostBackPagos" + Session.SessionID];
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (Sesion == null)
                    CerrarVentana();
                else
                {
                    //if (!Page.IsPostBack && Session["PosBackPagos" + Session.SessionID] == null)
                    if (!Page.IsPostBack)
                    {
                        if (Request.QueryString["CentroEjecutor"] != null)
                        {
                            HF_Centro.Value = Request.QueryString["CentroEjecutor"].ToString();
                        }

                        //Session["PostBackPagos" + Session.SessionID] = true;
                        //clientSideIsPostBack.Value = "S";
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            //RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        Session["PosBackPagos" + Session.SessionID] = "1";
                        Inicializar();
                        //   if (!((RadToolBarItem)rtb1.Items.FindItemByValue("save")).Visible)
                        //   {

                        //      DeshabilitarDetalle();
                        //  }

                        //double ancho = 0;
                        //foreach (GridColumn gc in RgDet.Columns)
                        //{
                        //    if (gc.Display)
                        //    {
                        //        ancho = ancho + gc.HeaderStyle.Width.Value;
                        //    }
                        //}
                        //RgDet.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        //RgDet.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));

                        //ancho = 0;



                    }

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
                string cmd = e.Argument.ToString();
                switch (cmd)
                {
                    case "RebindGrid":
                        RgDet.Rebind();
                        break;
                    case "ConfirmaTimbre":
                        // Llamar al mismo método que se llama desde el botón
                        Button2_Click(null, EventArgs.Empty);
                        break;
                    case "TimbrarDocumentos":
                        Timbrar_Click(null, EventArgs.Empty);
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }

        protected void rtb1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (btn.CommandName == "save")
                {
                    if (Page.IsValid)
                        Guardar();
                }
                else if (btn.CommandName == "new")
                {
                    Nuevo();
                }
                else if (btn.CommandName == "undo")
                {
                    CerrarVentana();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rtb1_ButtonClick");
            }
            finally
            {
                //RAM1.ResponseScripts.Add("HabilitarGuardar();");
            }
        }
        protected void Timbrar_Click(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                if (HF_Centro.Value != "")
                {
                    Alerta("No se puede realizar factura de un pago externo");
                }
                else
                {
                    if (HF_MP.Value != cboMetodoPago.SelectedValue)
                    {

                        if (cboMetodoPago.SelectedValue == "13" || cboMetodoPago.SelectedValue == "31")
                        {

                            HF_NMP.Value = "Transferencia electrónica de fondos";
                            string script = "radconfirm('La forma de pago seleccionado es subrogación o factoraje. <br>  Se realizare automaticamente el REP por Transferencia electrónica de fondos. <br> ¿Desea continuar?', confirmMetodoCallBackFn, 450, 150);";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "RadButton1", script, true);
                        }
                        else
                        {
                            HF_NMP.Value = cboMetodoPago.SelectedItem.Text;
                            string script = "radconfirm('¿Es correcto la forma de Pago que esta seleccionando?', confirmMetodoCallBackFn, 350, 150); HabilitarBoton()";

                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "RadButton1", script, true);
                        }
                    }
                    else
                    {
                        timbrardocumentos(cboMetodoPago.SelectedValue);
                    }
                }
            }
            catch (Exception ex)
            {

                ErrorManager(ex, "Timbrar_Click");
            }

        }
        protected void cmbBancos_DataBinding(object sender, EventArgs e)
        {
            try
            {
                if (banco)
                {
                    banco = false;
                    return;
                }
                banco = true;
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                RadComboBox ComboBox = sender as RadComboBox;
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatBanco_Combo", ref ComboBox);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RgGral_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    RgGral.DataSource = dtGral;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RgGral_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                RgGral.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RgGral_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":
                        InitInsert(e);
                        break;
                    case "PerformInsert":
                        PerformInsert(e);
                        CargarFichas();
                        break;
                    case "Update":
                        Update(e);
                        break;
                    case "Delete":
                        Delete(e);
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RgGral_ItemCreated(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridEditableItem && e.Item.IsInEditMode)
                {
                    GridEditableItem item = e.Item as GridEditableItem;
                    if (item.FindControl("rdFecha") != null)
                        (item.FindControl("rdFecha") as RadDatePicker).DateInput.Focus();
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
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    txtImporte.Text = "0.00";
                    RgDet.DataSource = dtDet;

                }
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
                int Id_Emp;
                int Id_Cd;
                int Id_Cte;
                int Id_PagDet;
                string Pag_Referencia;
                GridItem item = (GridItem)e.Item;
                Int32.TryParse(item.Cells[7].Text, out Id_Emp);
                Int32.TryParse(item.Cells[9].Text, out Id_Cd);
                Int32.TryParse(item.Cells[10].Text, out Id_Cte);
                Int32.TryParse(item.Cells[11].Text, out Id_PagDet);


                Label refe = item.FindControl("lblReferencia") as Label;
                Pag_Referencia = refe.Text;

                PagoDetComplemento pagoDetComplemento = new PagoDetComplemento();
                object pagoPDF = null;
                pagoDetComplemento.Id_Emp = Id_Emp;
                pagoDetComplemento.Id_Cd = sesion.Id_Cd_Ver;
                pagoDetComplemento.Id_Pag = Convert.ToInt32(HF_ID.Value);
                pagoDetComplemento.Id_Cte = Id_Cte;
                pagoDetComplemento.Id_PagDet = Id_PagDet;
                pagoDetComplemento.Id_PagComp = -1;
                new CN_CapPago().ConsultarPago(ref pagoDetComplemento, ref pagoPDF, sesion.Emp_Cnx);

                if (pagoDetComplemento == null || pagoDetComplemento.Cancelacion_XML == null)
                {
                    //item.FindControl("xmlCancelacion").Visible = false;
                }
                else
                {
                    //item.FindControl("xmlCancelacion").Visible = true;
                    item.FindControl("gbcCancelar").Visible = false;
                }
                if (pagoDetComplemento == null || pagoDetComplemento.Pago_Xml == null)
                {
                    //item.FindControl("ImageButton2").Visible = false;
                    //item.FindControl("ImageButton3").Visible = false;
                    //item.FindControl("gbcEnviar").Visible = false;
                    item.FindControl("gbcComprobante").Visible = false;
                    item.FindControl("gbcCancelar").Visible = false;

                    ((CheckBox)item.FindControl("ChkTimbrar")).Checked = true;
                    ((CheckBox)item.FindControl("ChkTimbrar")).Attributes.Add("onclick", "CheckTimbrar(this)");
                    string local = ((Label)item.FindControl("lblImportePag")).Text;
                    decimal importe = Convert.ToDecimal(txtImporte.Text) + Convert.ToDecimal(local);
                    txtImporte.Text = importe.ToString();

                }
                else
                {
                    if (pagoDetComplemento == null || pagoDetComplemento.Cancelacion_XML != null)
                    {
                        ((CheckBox)item.FindControl("ChkTimbrar")).Checked = true;
                        ((CheckBox)item.FindControl("ChkTimbrar")).Attributes.Add("onclick", "CheckTimbrar(this)");
                        string local = ((Label)item.FindControl("lblImportePag")).Text;
                        decimal importe = Convert.ToDecimal(txtImporte.Text) + Convert.ToDecimal(local);
                        txtImporte.Text = importe.ToString();
                    }
                    else
                    {
                        item.FindControl("gbcFactoraje").Visible = false;
                        item.FindControl("ChkTimbrar").Visible = false;
                    }
                }
                if (Convert.ToInt32(HF_FPI.Value) != 13 && Convert.ToInt32(HF_FPI.Value) != 31)
                {
                    item.FindControl("gbcFactoraje").Visible = false;
                }

                //Get the instance of the right type  
                GridDataItem dataBoundItem = e.Item as GridDataItem;

                ImageButton btnDelete = item.FindControl("gbcFactoraje") as ImageButton;
                btnDelete.Attributes.Add("onclick", "return " + "confirm('¿desea realizar el REP de la factura: " + Pag_Referencia + " por el método especial de pago ?')");
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
            string serie;
            try
            {
                ErrorManager();
                string strMensaje = string.Empty;
                Int32 item = default(Int32);
                if (e.Item == null) return;
                item = e.Item.ItemIndex;
                GridItem gi = e.Item;
                if (item >= 0)
                {

                    Int32.TryParse(gi.Cells[7].Text, out Id_Emp);
                    Int32.TryParse(gi.Cells[8].Text, out Id_Fac);
                    Int32.TryParse(gi.Cells[9].Text, out Id_Cd);
                    Int32.TryParse(gi.Cells[10].Text, out Id_Cte);
                    Int32.TryParse(gi.Cells[11].Text, out Id_PagDet);
                    DateTime fechaPeriodoInicio = sesion.CalendarioIni;
                    DateTime fechaPeriodoFinal = sesion.CalendarioFin;
                    serie = gi.Cells[13].Text;
                    switch (e.CommandName)
                    {
                        //case "PDF":
                        //    if (_PermisoImprimir)
                        //    {
                        //        this.descargarPDF(Id_Cte, Id_PagDet);
                        //    }
                        //    else
                        //    {
                        //        Alerta("Operación denegada, no tiene permisos para imprimir facturas");
                        //    }
                        //    break;
                        //case "XML":
                        //    descargarXML(Id_Cte, Id_PagDet);
                        //    break;
                        //case "xmlCancelacion":
                        //    descargarXMLCancelacion(Id_Cte, Id_PagDet);
                        //    break;
                        case "Cancelar":
                            // if (Convert.ToDateTime(HF_FechaPago.Value) < fechaPeriodoInicio || Convert.ToDateTime(HF_FechaPago.Value) > fechaPeriodoFinal)
                            //{
                            //    Alerta("El pago no se encuentra en el periodo actual");
                            //    e.Canceled = true;
                            //    return;
                            // }
                            if (HF_Centro.Value != "")
                            {
                                Alerta("No es posible cancelar un pago externo");
                            }
                            else
                            {
                                bool otraBD = sesion.Id_Cd_Ver != Id_Cd;

                                CN_CapPago cnPago = new CN_CapPago();
                                FacturaLite entFacturaLite = new FacturaLite();
                                entFacturaLite.id_Pag = Convert.ToInt32(HF_ID.Value);
                                cnPago.PagoSegundoPlanoConsultaEstatus(sesion.Emp_Cnx, ref entFacturaLite);
                                if (entFacturaLite.Activo == 1)
                                {
                                    Alerta(entFacturaLite.strRstAlerta);
                                    break;
                                }
                                this.cancelarComplemento(Id_PagDet, otraBD);
                            }
                            break;
                        //case "Enviar":
                        //    RAM1.ResponseScripts.Add(string.Concat(@"AbrirVentana_EnviarPagos('", Id_Emp, "','", Id_Cd, "','", Id_Cte, "','", HF_ID.Value, "','", Id_Fac, "','", Id_PagDet, "','", serie, "')"));
                        //    break;
                        case "Comprobante":
                            RAM1.ResponseScripts.Add(string.Concat(@"AbrirVentana_PagoTimbreDetalladoComplemento('" + HF_ID.Value + "','" + Id_Cte + "','" + Id_PagDet + "', '" + Id_Fac + "', '" + serie + "')"));

                            break;
                        case "Factoraje":
                            if (HF_Centro.Value != "")
                            {
                                Alerta("No se puede realizar factura de un pago externo");
                            }
                            else
                            {
                                //Si es 31 es por factoraje, si es por 13 es por subrogación
                                if (Convert.ToInt32(HF_FPI.Value) == 31)
                                {
                                    if (serie == "PUE")
                                    {
                                        Alerta("No se puede realizar factoraje con serie PUE. Favor de verificar que el metodo de pago del cliente sea correcto");
                                    }
                                    else
                                    {
                                        RAM1.ResponseScripts.Add(string.Concat(@"AbrirVentana_PagoFactoraje('" + HF_ID.Value + "','" + Id_Cte + "','" + Id_PagDet + "', '" + Id_Fac + "', '" + serie + "')"));
                                    }
                                }
                                else if (Convert.ToInt32(HF_FPI.Value) == 13)
                                {
                                    RAM1.ResponseScripts.Add(string.Concat(@"return AbrirVentana_PagoSubrogacion('" + HF_ID.Value + "','" + Id_Cte + "','" + Id_PagDet + "', '" + Id_Fac + "', '" + serie + "')"));

                                }
                            }
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
                RgDet.Rebind();
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
        protected void rgTxtReferencia_TextChanged(object source, EventArgs e)
        {
            try
            {
                Consulta();
                //Sesion sesion = new Sesion();
                //sesion = (Sesion)Session["Sesion" + Session.SessionID];
                //RadTextBox rgtxtReferencia = source as RadTextBox;
                //RadTextBox rgSerie = rgtxtReferencia.Parent.FindControl("rgSerie") as RadTextBox;
                //RadNumericTextBox rgCdi = rgSerie.Parent.FindControl("rgCdi") as RadNumericTextBox;
                //RadComboBox rgcmbMov = rgtxtReferencia.Parent.FindControl("rgcmbMov") as RadComboBox;
                //RadNumericTextBox rgTerr = rgtxtReferencia.Parent.FindControl("rgTerr") as RadNumericTextBox;
                //RadDatePicker rdFecha = rgtxtReferencia.Parent.FindControl("rdFecha") as RadDatePicker;
                //RadNumericTextBox rgCte = rgtxtReferencia.Parent.FindControl("rgCte") as RadNumericTextBox;
                //RadTextBox rgCteNombre = rgtxtReferencia.Parent.FindControl("rgCteNombre") as RadTextBox;
                //RadTextBox rgEstatus = rgtxtReferencia.Parent.FindControl("rgEstatus") as RadTextBox;
                //RadNumericTextBox rgImporte = rgtxtReferencia.Parent.FindControl("rgImporte") as RadNumericTextBox;
                //RadNumericTextBox rgPagado = rgtxtReferencia.Parent.FindControl("rgPagado") as RadNumericTextBox;
                //RadNumericTextBox rgtxtImporte = rgtxtReferencia.Parent.FindControl("rgtxtImporte") as RadNumericTextBox;
                //RadTextBox rgCheque = rgSerie.Parent.FindControl("rgCheque") as RadTextBox;
                //if (!rgtxtReferencia.AutoPostBack)
                //{
                //    return;
                //}

                ////if (rgtxtReferencia.Text == "")
                ////{
                ////    rgTerr.Text = "";
                ////    rdFecha.DbSelectedDate = null;
                //rgCte.Text = "";
                ////    rgCteNombre.Text = "";
                ////    rgtxtImporte.Text = "";
                ////    rgCheque.Text = "";
                ////    rgCdi.Text = "";
                ////    rgtxtReferencia.Focus();
                ////    return;
                ////}
                ////LimpiarDetalle(rgTerr, rdFecha, rgCte, rgCteNombre, rgEstatus, rgtxtImporte, rgPagado);
                //if (rgSerie.Text == "" || rgtxtReferencia.Text == "")
                //{
                //    rgSerie.Focus();
                //    return;
                //}
                //else
                //{
                //    rgCte.Focus();
                //}

                //if (rgCdi.Text == sesion.Id_Cd_Ver.ToString())
                //{
                //    rgTxtCliente_TextChanged(rgCte, null);
                //}
                ////rgtxtReferencia.Text = rgtxtReferencia.Text.ToUpper();

                ////string Referencia = rgtxtReferencia.Text;
                ////int Movimiento = Convert.ToInt32(rgcmbMov.SelectedValue);
                ////DataRow[] dr_ar = dtDet.Select("Doc_Referencia='" + Referencia + "' and Serie='" + rgSerie.Text + "'");
                ////if (dr_ar.Length == 0)
                ////{
                ////LimpiarDetalle(rgTerr, rdFecha, rgCte, rgCteNombre, rgEstatus, rgtxtImporte, rgPagado);
                ////if (Movimiento == 1)
                ////{
                ////    Factura ficha = new Factura();
                ////    ficha.Id_Emp = sesion.Id_Emp;
                ////    ficha.Id_Cd = Convert.ToInt32(rgCdi.Value);
                ////    ficha.Serie = rgSerie.Text;
                ////    ficha.Id_FacSerie = Referencia;
                ////    CN_CapPago cn_cappago = new CN_CapPago();
                ////    int verificador = 0;
                ////    cn_cappago.ConsultaPagoFicha(ref ficha, Emp_CnxCob, ref verificador);
                ////    if (verificador == 1)
                ////    {
                ////        if (ficha.Id_Cte.ToString() == rgCte.Text)
                ////        {
                ////            rgTerr.Text = ficha.Id_Ter.ToString();
                ////            rdFecha.SelectedDate = ficha.Fac_Fecha;
                ////            rgCte.Text = ficha.Id_Cte.ToString();
                ////            rgCteNombre.Text = ficha.Cte_NomComercial;
                ////            rgEstatus.Text = ficha.Fac_Estatus;
                ////            rgImporte.Text = ficha.Fac_Saldo.ToString();
                ////            rgPagado.Text = ficha.Fac_Pagado.ToString();
                ////            rgCdi.Text = ficha.Id_Cd.ToString();
                ////            rgtxtImporte.Text = (ficha.Fac_Saldo - ficha.Fac_Pagado).ToString();
                ////        }

                ////        if (rgCte.Text == "")
                ////        {
                ////            rgCte.Focus();
                ////        }
                ////    }
                ////    else
                ////    {
                ////        Alerta("Movimiento no existe; posiblemente sea un movimiento externo, revise la serie");
                ////        rgtxtReferencia.Text = "";
                ////    }
                ////}
                ////else if (Movimiento == 2)
                ////{
                ////    NotaCargo ficha = new NotaCargo();
                ////    ficha.Id_Emp = sesion.Id_Emp;
                ////    ficha.Serie = rgSerie.Text;
                ////    ficha.Id_Nca = Convert.ToInt32(Referencia);
                ////    CN_CapNotaCargo cn_capnotacargo = new CN_CapNotaCargo();
                ////    int verificador = 0;
                ////    cn_capnotacargo.ConsultaPagoFicha(ref ficha, sesion.Emp_Cnx, ref verificador);
                ////    if (verificador == 1)
                ////    {
                ////        rgTerr.Text = ficha.Id_Ter.ToString();
                ////        rdFecha.SelectedDate = ficha.Nca_Fecha;
                ////        rgCte.Text = ficha.Id_Cte.ToString();
                ////        rgCteNombre.Text = ficha.Cte_NomComercial;
                ////        rgEstatus.Text = ficha.Nca_Estatus;
                ////        rgImporte.Text = ficha.Importe.ToString();
                ////        rgPagado.Text = ficha.Nca_Pagado.ToString();
                ////        rgCdi.Text = ficha.Id_Cd.ToString();
                ////        rgtxtImporte.Text = ficha.Importe.ToString();
                ////    }
                ////    else
                ////    {
                ////        Alerta("Movimiento no existe; posiblemente sea un movimiento externo");
                ////        return;
                ////    }
                ////}
                ////if (rgEstatus.Text == "B")
                ////{
                ////    Alerta("Movimiento se encuentra cancelado; imposible aplicarle un pago");
                ////    LimpiarDetalle(rgTerr, rdFecha, rgCte, rgCteNombre, rgEstatus, rgtxtImporte, rgPagado);
                ////    rgtxtReferencia.Focus();
                ////    return;
                ////}
                ////else if (rgEstatus.Text == "C")
                ////{
                ////    Alerta("Movimiento se encuentra en status capturado; imposible aplicarle un pago");
                ////    LimpiarDetalle(rgTerr, rdFecha, rgCte, rgCteNombre, rgEstatus, rgtxtImporte, rgPagado);
                ////    rgtxtReferencia.Focus();
                ////    return;
                ////}
                ////if (Math.Round(Convert.ToDouble(rgImporte.Text) - Convert.ToDouble(rgPagado.Text), 2) <= 0)
                ////{
                ////    Alerta("Movimiento no tiene saldo positivo; imposible aplicarle un pago");
                ////    LimpiarDetalle(rgTerr, rdFecha, rgCte, rgCteNombre, rgEstatus, rgtxtImporte, rgPagado);
                ////    rgtxtReferencia.Focus();
                ////    return;
                ////}
                ////(rgtxtReferencia.Parent.FindControl("rgCheque") as RadTextBox).Focus();
                ////}
                ////else
                ////{
                ////    Alerta("Ya existe el registro");
                ////    rgtxtReferencia.Text = "";
                ////    rgtxtReferencia.Focus();
                ////    return;
                ////}
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void rgTxtCliente_TextChanged(object source, EventArgs e)
        {
            try
            {

                Consulta();

                //Sesion sesion = new Sesion();
                //sesion = (Sesion)Session["Sesion" + Session.SessionID];


                //RadNumericTextBox rgCte = source as RadNumericTextBox;
                //RadTextBox rgtxtReferencia = rgCte.Parent.FindControl("rgReferencia") as RadTextBox;
                //RadTextBox rgtxtFolioFiscal = rgCte.Parent.FindControl("rgFolioFiscal") as RadTextBox;
                //RadTextBox rgSerie = rgtxtReferencia.Parent.FindControl("rgSerie") as RadTextBox;
                //RadNumericTextBox rgCdi = rgSerie.Parent.FindControl("rgCdi") as RadNumericTextBox;
                //RadComboBox rgcmbMov = rgtxtReferencia.Parent.FindControl("rgcmbMov") as RadComboBox;
                //RadNumericTextBox rgTerr = rgtxtReferencia.Parent.FindControl("rgTerr") as RadNumericTextBox;
                //RadDatePicker rdFecha = rgtxtReferencia.Parent.FindControl("rdFecha") as RadDatePicker;
                //RadTextBox rgCteNombre = rgtxtReferencia.Parent.FindControl("rgCteNombre") as RadTextBox;
                //RadTextBox rgEstatus = rgtxtReferencia.Parent.FindControl("rgEstatus") as RadTextBox;
                //RadNumericTextBox rgImporte = rgtxtReferencia.Parent.FindControl("rgImporte") as RadNumericTextBox;
                //RadNumericTextBox rgPagado = rgtxtReferencia.Parent.FindControl("rgPagado") as RadNumericTextBox;
                //RadNumericTextBox rgtxtImporte = rgtxtReferencia.Parent.FindControl("rgtxtImporte") as RadNumericTextBox;
                //RadTextBox rgCheque = rgSerie.Parent.FindControl("rgCheque") as RadTextBox;

                //if (!rgCte.AutoPostBack && rgCdi.Text != sesion.Id_Cd_Ver.ToString())
                //{
                //    return;
                //}

                //rgTerr.Text = "";
                //rdFecha.DbSelectedDate = null;
                ////rgCte.Text = "";
                //rgCteNombre.Text = "";
                //rgtxtImporte.Text = "";
                //rgCheque.Text = "";

                //if (rgtxtReferencia.Text == "")
                //{
                //    rgTerr.Text = "";
                //    rdFecha.DbSelectedDate = null;
                //    rgCte.Text = "";
                //    rgCteNombre.Text = "";
                //    rgtxtImporte.Text = "";
                //    rgCheque.Text = "";
                //    //rgCdi.Text = "";
                //    rgtxtReferencia.Focus();
                //    return;
                //}

                //if (rgSerie.Text == "")
                //{
                //    rgSerie.Focus();
                //    return;
                //}
                //if (rgtxtReferencia.Text == "")
                //{
                //    rgtxtReferencia.Focus();
                //    return;
                //}
                //if (rgCte.Text == "" && Convert.ToInt32(rgCdi.Value) != sesion.Id_Cd_Ver)
                //{
                //    rgCte.Focus();
                //    return;
                //}

                //rgtxtReferencia.Text = rgtxtReferencia.Text.ToUpper();

                //string Referencia = rgtxtReferencia.Text;
                //int Movimiento = Convert.ToInt32(rgcmbMov.SelectedValue);
                //DataRow[] dr_ar = dtDet.Select("Doc_Referencia='" + Referencia + "' and Serie='" + rgSerie.Text + "'");
                //if (dr_ar.Length == 0)
                //{
                //    LimpiarDetalle(rgTerr, rdFecha, rgTerr, rgCteNombre, rgEstatus, rgtxtImporte, rgPagado,rgtxtFolioFiscal);
                //    if (Movimiento == 1)
                //    {
                //        Factura ficha = new Factura();
                //        ficha.Id_Emp = sesion.Id_Emp;
                //        ficha.Id_Cd = Convert.ToInt32(rgCdi.Value);
                //        ficha.Serie = rgSerie.Text;
                //        ficha.Id_FacSerie = Referencia;
                //        CN_CapPago cn_cappago = new CN_CapPago();
                //        int verificador = 0;
                //        cn_cappago.ConsultaPagoFicha(ref ficha, Emp_CnxCob, ref verificador);
                //        if (verificador == 1)
                //        {
                //            if (ficha.Id_Cte.ToString() == rgCte.Text || ficha.Id_Cd == sesion.Id_Cd_Ver)
                //            {
                //                rgTerr.Text = ficha.Id_Ter.ToString();
                //                rdFecha.SelectedDate = ficha.Fac_Fecha;
                //                rgCte.Text = ficha.Id_Cte.ToString();
                //                rgCteNombre.Text = ficha.Cte_NomComercial;
                //                rgEstatus.Text = ficha.Fac_Estatus;
                //                rgImporte.Text = ficha.Fac_Saldo.ToString();
                //                rgPagado.Text = ficha.Fac_Pagado.ToString();
                //                rgCdi.Text = ficha.Id_Cd.ToString();
                //                rgtxtImporte.Text = (ficha.Fac_Saldo - ficha.Fac_Pagado).ToString();
                //                rgtxtFolioFiscal.Text = ficha.Fac_FolioFiscal;
                //            }
                //            else
                //            {
                //                AlertaFocus("El documento no pertenece al cliente capturado", rgCte.ClientID);
                //                rgCte.Text = "";
                //                return;
                //            }
                //            if (rgCte.Text == "")
                //            {
                //                rgCte.Focus();
                //            }
                //        }
                //        else
                //        {
                //            Alerta("Movimiento no existe; posiblemente sea un movimiento externo, revise la serie");
                //            rgtxtReferencia.Text = "";
                //            rgtxtFolioFiscal.Text = "";
                //        }
                //    }
                //    else if (Movimiento == 2)
                //    {
                //        NotaCargo ficha = new NotaCargo();
                //        ficha.Id_Emp = sesion.Id_Emp;
                //        ficha.Id_Cd = Convert.ToInt32(rgCdi.Value);
                //        ficha.Serie = rgSerie.Text;
                //        ficha.Id_Nca = Convert.ToInt32(Referencia);
                //        CN_CapPago cn_cappago = new CN_CapPago();

                //        int verificador = 0;
                //        cn_cappago.ConsultaPagoNotaFicha(ref ficha, Emp_CnxCob, ref verificador);
                //        if (verificador == 1)
                //        {
                //            rgTerr.Text = ficha.Id_Ter.ToString();
                //            rdFecha.SelectedDate = ficha.Nca_Fecha;
                //            rgCte.Text = ficha.Id_Cte.ToString();
                //            rgCteNombre.Text = ficha.Cte_NomComercial;
                //            rgEstatus.Text = ficha.Nca_Estatus;
                //            rgImporte.Text = ficha.Importe.ToString();
                //            rgPagado.Text = ficha.Nca_Pagado.ToString();
                //            rgCdi.Text = ficha.Id_Cd.ToString();
                //            rgtxtImporte.Text = ficha.Importe.ToString();
                //            rgtxtFolioFiscal.Text = ficha.Nca_FolioFiscal;
                //        }
                //        else
                //        {
                //            Alerta("Movimiento no existe; posiblemente sea un movimiento externo");
                //            return;
                //        }
                //    }
                //    if (rgEstatus.Text == "B")
                //    {
                //        Alerta("Movimiento se encuentra cancelado; imposible aplicarle un pago");
                //        LimpiarDetalle(rgTerr, rdFecha, rgCte, rgCteNombre, rgEstatus, rgtxtImporte, rgPagado, rgtxtFolioFiscal);
                //        rgtxtReferencia.Focus();
                //        return;
                //    }
                //    else if (rgEstatus.Text == "C")
                //    {
                //        Alerta("Movimiento se encuentra en status capturado; imposible aplicarle un pago");
                //        LimpiarDetalle(rgTerr, rdFecha, rgCte, rgCteNombre, rgEstatus, rgtxtImporte, rgPagado, rgtxtFolioFiscal);
                //        rgtxtReferencia.Focus();
                //        return;
                //    }
                //    if (rgImporte.Text != "" && rgPagado.Text != "")
                //    {
                //        if (Math.Round(Convert.ToDouble(rgImporte.Text) - Convert.ToDouble(rgPagado.Text), 2) <= 0)
                //        {
                //            Alerta("Movimiento no tiene saldo positivo; imposible aplicarle un pago");
                //            LimpiarDetalle(rgTerr, rdFecha, rgCte, rgCteNombre, rgEstatus, rgtxtImporte, rgPagado, rgtxtFolioFiscal);
                //            rgtxtReferencia.Focus();
                //            return;
                //        }
                //    }
                //    (rgtxtReferencia.Parent.FindControl("rgCheque") as RadTextBox).Focus();
                //}
                //else
                //{
                //    Alerta("Ya existe el registro");
                //    rgtxtReferencia.Text = "";
                //    rgtxtReferencia.Focus();
                //    return;
                //}
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void rgTxtSerie_TextChanged(object source, EventArgs e)
        {
            try
            {
                ConsultaSerie2(source);
                Consulta();

                //Sesion sesion = new Sesion();
                //sesion = (Sesion)Session["Sesion" + Session.SessionID];
                //RadTextBox rgSerie = source as RadTextBox;
                //RadTextBox rgtxtReferencia = rgSerie.Parent.FindControl("rgReferencia") as RadTextBox;
                //RadComboBox rgcmbMov = rgSerie.Parent.FindControl("rgcmbMov") as RadComboBox;
                //RadNumericTextBox rgTerr = rgSerie.Parent.FindControl("rgTerr") as RadNumericTextBox;
                //RadNumericTextBox rgCdi = rgSerie.Parent.FindControl("rgCdi") as RadNumericTextBox;
                //RadDatePicker rdFecha = rgSerie.Parent.FindControl("rdFecha") as RadDatePicker;
                //RadNumericTextBox rgCte = rgSerie.Parent.FindControl("rgCte") as RadNumericTextBox;
                //RadTextBox rgCteNombre = rgSerie.Parent.FindControl("rgCteNombre") as RadTextBox;
                //RadTextBox rgEstatus = rgSerie.Parent.FindControl("rgEstatus") as RadTextBox;
                //RadNumericTextBox rgImporte = rgSerie.Parent.FindControl("rgImporte") as RadNumericTextBox;
                //RadNumericTextBox rgPagado = rgSerie.Parent.FindControl("rgPagado") as RadNumericTextBox;
                //RadNumericTextBox rgtxtImporte = rgSerie.Parent.FindControl("rgtxtImporte") as RadNumericTextBox;
                //RadTextBox rgCheque = rgSerie.Parent.FindControl("rgCheque") as RadTextBox;
                //RadTextBox rgFolioFiscal = rgSerie.Parent.FindControl("rgFolioFiscal") as RadTextBox;

                //rgSerie.Text = rgSerie.Text.ToUpper();
                //rgtxtReferencia.Text = "";
                //rgCdi.Text = "";
                //rgTerr.Text = "";
                //rdFecha.DbSelectedDate = null;
                //rgCte.Text = "";
                //rgCteNombre.Text = "";
                //rgtxtImporte.Text = "";
                //rgCheque.Text = "";
                //rgFolioFiscal.Text = "";

                //int verificador = 0;
                ////CN_ConsecutivoFE cfe = new CN_ConsecutivoFE();
                ////cfe.ConsultaConsecutivo(sesion.Id_Emp, Convert.ToInt32(rgcmbMov.SelectedValue), rgSerie.Text, sesion.Emp_Cnx, ref verificador);

                //Sesion session2 = new Sesion();
                //session2 = (Sesion)Session["Sesion" + Session.SessionID];
                //int Tipo_CDC = 0;
                //new CN_CatCliente().ConsultaTipoCDC(session2.Id_Cd_Ver, ref Tipo_CDC, session2.Emp_Cnx);            


                //CN_CapPago cn_cappago = new CN_CapPago();
                //DbCentro centro = new DbCentro();
                //cn_cappago.ConsultarCentro(sesion.Id_Emp, rgSerie.Text, ref centro, Emp_CnxCob, Tipo_CDC);
                //verificador = centro.Id_Cd;

                //if (verificador == 0)
                //{
                //    rgtxtReferencia.AutoPostBack = false;
                //    rgTerr.ReadOnly = false;
                //    rdFecha.DateInput.ReadOnly = false;
                //    rdFecha.DatePopupButton.Enabled = true;
                //    rgCte.ReadOnly = false;
                //    rgCte.AutoPostBack = false;
                //    rgCteNombre.ReadOnly = false;
                //    rgCdi.ReadOnly = false;
                //}
                //else
                //{
                //    if (Request.QueryString["Ext"] != null && centro.Db_CerradoExtemporaneo != null)
                //    {
                //        rgSerie.Text = "";
                //        rgtxtReferencia.Text = "";
                //        Alerta("La sucursal externa ya realizo el cierre extemporáneo de pagos");
                //        rgSerie.Focus();
                //        return;
                //    }
                //    else
                //    {
                //        rgtxtReferencia.AutoPostBack = true;
                //        rgTerr.ReadOnly = true;
                //        rdFecha.DateInput.ReadOnly = true;
                //        rdFecha.DatePopupButton.Enabled = false;
                //        rgCteNombre.ReadOnly = true;
                //        rgCdi.ReadOnly = true;
                //        rgCdi.DbValue = verificador;

                //        if (rgCdi.Text == sesion.Id_Cd_Ver.ToString())
                //        {
                //            rgCte.ReadOnly = true;

                //        }
                //        else
                //        {
                //            rgCte.AutoPostBack = true;
                //        }
                //    }
                //}

                //rgtxtReferencia.Focus();
                //return;

                //if (rgSerie.Text == "" || rgtxtReferencia.Text == "")
                //{

                //    rgtxtReferencia.Focus();
                //    return;
                //}



                //rgtxtReferencia.Text = rgtxtReferencia.Text.ToUpper();

                //string Referencia = rgtxtReferencia.Text;
                //int Movimiento = Convert.ToInt32(rgcmbMov.SelectedValue);
                //DataRow[] dr_ar = dtDet.Select("Doc_Referencia='" + Referencia + "' and Serie='" + rgSerie.Text + "'");
                //if (dr_ar.Length == 0)
                //{
                //    LimpiarDetalle(rgTerr, rdFecha, rgCte, rgCteNombre, rgEstatus, rgtxtImporte, rgPagado, rgFolioFiscal);
                //    if (Movimiento == 1)
                //    {
                //        Factura ficha = new Factura();
                //        ficha.Id_Emp = sesion.Id_Emp;
                //        ficha.Id_Cd = Convert.ToInt32(rgCdi.Value);
                //        ficha.Serie = rgSerie.Text;
                //        ficha.Id_FacSerie = Referencia;

                //        //CN_CapPago cn_cappago = new CN_CapPago();
                //        verificador = 0;
                //        cn_cappago.ConsultaPagoFicha(ref ficha, Emp_CnxCob, ref verificador);

                //        if (verificador == 1)
                //        {
                //            rgTerr.Text = ficha.Id_Ter.ToString();
                //            rdFecha.SelectedDate = ficha.Fac_Fecha;
                //            rgCte.Text = ficha.Id_Cte.ToString();
                //            rgCteNombre.Text = ficha.Cte_NomComercial;
                //            rgEstatus.Text = ficha.Fac_Estatus;
                //            rgImporte.Text = ficha.Fac_Saldo.ToString();
                //            rgPagado.Text = ficha.Fac_Pagado.ToString();
                //            rgtxtImporte.Text = (ficha.Fac_Saldo - ficha.Fac_Pagado).ToString();
                //        }
                //        else
                //        {
                //            Alerta("Movimiento no existe; posiblemente sea un movimiento externo");
                //            return;
                //        }
                //    }
                //    else if (Movimiento == 2)
                //    {
                //        NotaCargo ficha = new NotaCargo();
                //        ficha.Id_Emp = sesion.Id_Emp;
                //        ficha.Serie = rgSerie.Text;
                //        ficha.Id_Nca = Convert.ToInt32(Referencia);
                //        CN_CapNotaCargo cn_capnotacargo = new CN_CapNotaCargo();
                //        verificador = 0;
                //        cn_capnotacargo.ConsultaPagoFicha(ref ficha, sesion.Emp_Cnx, ref verificador);
                //        if (verificador == 1)
                //        {
                //            rgTerr.Text = ficha.Id_Ter.ToString();
                //            rdFecha.SelectedDate = ficha.Nca_Fecha;
                //            rgCte.Text = ficha.Id_Cte.ToString();
                //            rgCteNombre.Text = ficha.Cte_NomComercial;
                //            rgEstatus.Text = ficha.Nca_Estatus;
                //            rgImporte.Text = ficha.Importe.ToString();
                //            rgPagado.Text = ficha.Nca_Pagado.ToString();
                //            rgtxtImporte.Text = ficha.Importe.ToString();
                //        }
                //        else
                //        {
                //            Alerta("Movimiento no existe; posiblemente sea un movimiento externo");
                //            return;
                //        }
                //    }
                //    if (rgEstatus.Text == "B")
                //    {
                //        Alerta("Movimiento se encuentra cancelado; imposible aplicarle un pago");
                //        LimpiarDetalle(rgTerr, rdFecha, rgCte, rgCteNombre, rgEstatus, rgtxtImporte, rgPagado, rgFolioFiscal);
                //        rgtxtReferencia.Focus();
                //        return;
                //    }
                //    else if (rgEstatus.Text == "C")
                //    {
                //        Alerta("Movimiento se encuentra en status capturado; imposible aplicarle un pago");
                //        LimpiarDetalle(rgTerr, rdFecha, rgCte, rgCteNombre, rgEstatus, rgtxtImporte, rgPagado, rgFolioFiscal);
                //        rgtxtReferencia.Focus();
                //        return;
                //    }
                //    if (Convert.ToDouble(rgImporte.Text) - Convert.ToDouble(rgPagado.Text) <= 0)
                //    {
                //        Alerta("Movimiento no tiene saldo positivo; imposible aplicarle un pago");
                //        LimpiarDetalle(rgTerr, rdFecha, rgCte, rgCteNombre, rgEstatus, rgtxtImporte, rgPagado, rgFolioFiscal);
                //        rgtxtReferencia.Focus();
                //        return;
                //    }
                //    (rgtxtReferencia.Parent.FindControl("rgCheque") as RadTextBox).Focus();
                //}
                //else
                //{
                //    Alerta("Ya existe el registro");
                //    rgtxtReferencia.Text = "";
                //    rgtxtReferencia.Focus();
                //    return;
                //}
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        private static void LimpiarDetalle(RadNumericTextBox rgTerr, RadDatePicker rdFecha, RadNumericTextBox rgCte, RadTextBox rgCteNombre, RadTextBox rgEstatus, RadNumericTextBox rgImporte, RadNumericTextBox rgPagado, RadTextBox rgtxtFolioFiscal)
        {
            rgTerr.Text = string.Empty;
            rdFecha.SelectedDate = null;
            rgCte.Text = string.Empty;
            rgCteNombre.Text = string.Empty;
            rgEstatus.Text = string.Empty;
            rgImporte.Value = null;
            rgPagado.Value = null;
            //rgtxtFolioFiscal.Text = string.Empty;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                timbrardocumentos(cboMetodoPago.SelectedValue);
            }
            catch (Exception ex)
            {

                ErrorManager(ex, "Timbrar_Click");
            }

        }


        protected void cmbDocumento_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {

            try
            {

                ConsultaSerie(o);
                Consulta();
                //RadComboBox combo = o as RadComboBox;
                //RadTextBox rgSerie = combo.Parent.FindControl("rgSerie") as RadTextBox;
                //RadTextBox rgtxtReferencia = combo.Parent.FindControl("rgReferencia") as RadTextBox;
                //RadComboBox rgcmbMov = rgSerie.Parent.FindControl("rgcmbMov") as RadComboBox;
                //RadNumericTextBox rgTerr = rgSerie.Parent.FindControl("rgTerr") as RadNumericTextBox;
                //RadDatePicker rdFecha = rgSerie.Parent.FindControl("rdFecha") as RadDatePicker;
                //RadNumericTextBox rgCte = rgSerie.Parent.FindControl("rgCte") as RadNumericTextBox;
                //RadTextBox rgCteNombre = rgSerie.Parent.FindControl("rgCteNombre") as RadTextBox;
                //RadTextBox rgEstatus = rgSerie.Parent.FindControl("rgEstatus") as RadTextBox;
                //RadNumericTextBox rgImporte = rgSerie.Parent.FindControl("rgImporte") as RadNumericTextBox;
                //RadNumericTextBox rgPagado = rgSerie.Parent.FindControl("rgPagado") as RadNumericTextBox;
                //RadNumericTextBox rgtxtImporte = rgSerie.Parent.FindControl("rgtxtImporte") as RadNumericTextBox;
                //RadTextBox rgCheque = rgSerie.Parent.FindControl("rgCheque") as RadTextBox;
                //RadNumericTextBox Cdi = rgSerie.Parent.FindControl("rgCdi") as RadNumericTextBox;

                //Sesion session2 = new Sesion();
                //session2 = (Sesion)Session["Sesion" + Session.SessionID];
                //string Folio = null;
                //CN_CatCliente cn_cte = new CN_CatCliente();
                //cn_cte.ConsultaFolioFactEle(session2, int.Parse(combo.SelectedValue), ref Folio);
                //rgtxtReferencia.AutoPostBack = true;
                //rgSerie.Text = Folio;
                //Cdi.Value = session2.Id_Cd_Ver;
                //rgtxtReferencia.Text = "";
                //rgTerr.Text = "";
                //rdFecha.DbSelectedDate = null;
                //rgCte.Text = "";
                //rgCteNombre.Text = "";
                //rgtxtImporte.Text = "";
                //rgCheque.Text = "";
                //rgtxtReferencia.Focus();

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void txtBanco_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                RadNumericTextBox combo = (RadNumericTextBox)sender;
                Telerik.Web.UI.GridTableCell tabla = (Telerik.Web.UI.GridTableCell)combo.Parent;
                RadNumericTextBox txtIdBanco = (RadNumericTextBox)tabla.FindControl("rgtxtIdBanco");
                RadTextBox txtBanCuenta = (RadTextBox)tabla.FindControl("txtBanCuenta");
                CN_CapPago pagos = new CN_CapPago();
                string cuenta = "";
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                int banco = txtIdBanco.Value.HasValue ? Convert.ToInt32(txtIdBanco.Value.Value) : 0;
                pagos.ConsultaCuentaBanco(sesion, banco, ref cuenta);

                txtBanCuenta.Text = cuenta;
                ((RadNumericTextBox)tabla.FindControl("rgtxtImporte")).Focus();
            }
            catch (Exception)
            {

            }
        }
        protected void btnImportar_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Server.MapPath("~/App_Data/RadUploadTemp") + "\\" + NombreArchivo;
                //if (File.Exists(path))
                //{
                //    File.Delete(path);
                //}



                OleDbConnection con = default(OleDbConnection);

                string strConn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0 xml;HDR=YES;IMEX=1;\"";
                con = new OleDbConnection(strConn);
                con.Open();
                DataTable dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string hoja = dt.Rows[0].ItemArray[2].ToString().Replace("'", "");
                OleDbCommand cmd = new OleDbCommand("select * from [" + hoja + "]", con);
                OleDbDataAdapter dad = new OleDbDataAdapter();
                dad.SelectCommand = cmd;
                DataSet ds = new DataSet();
                try
                {
                    dad.Fill(ds);
                }
                catch (Exception ex)
                {
                    Alerta(ex.Message);

                }
                con.Close();


                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                String MovStr = "";
                Int32 Id_Terr = 0;
                Int32 Id_Cte = 0;
                String Doc_Estatus = "";
                Double Doc_Importe = 0;
                Double Doc_Pagado = 0;
                Double Total = 0;
                DateTime Doc_Fecha;
                String Cte_Nombre = "";
                String Fac_FolioFiscal = "";
                String TxtReferencia = "";
                int SeCargaron = 0;



                dtDet.Clear();

                int x = 0;
                foreach (DataRow row in ds.Tables[0].Rows)
                {



                    MovStr = "";
                    Id_Terr = 0;
                    Id_Cte = 0;
                    Doc_Estatus = "";
                    Doc_Importe = 0;
                    Doc_Pagado = 0;
                    Cte_Nombre = "";
                    Fac_FolioFiscal = "";
                    TxtReferencia = "";
                    Doc_Fecha = DateTime.Today;

                    //aqui
                    if (Convert.ToString(row[1]) == "1")
                    {

                        Factura ficha = new Factura();
                        ficha.Id_Emp = Sesion.Id_Emp;
                        ficha.Id_Cd = Sesion.Id_Cd_Ver;
                        ficha.Serie = Convert.ToString(row[2]);
                        ficha.Id_FacSerie = Convert.ToString(row[3]);
                        ficha.Fac_FolioFiscal = Convert.ToString(row[4]);
                        CN_CapPago cn_cappago = new CN_CapPago();
                        int verificador = 0;
                        cn_cappago.ConsultaPagoFicha(ref ficha, Emp_CnxCob, ref verificador);
                        if (verificador == 1)
                        {
                            MovStr = "Factura";
                            Id_Terr = Convert.ToInt32(ficha.Id_Ter);
                            Doc_Fecha = ficha.Fac_Fecha;
                            Id_Cte = ficha.Id_Cte;
                            Cte_Nombre = ficha.Cte_NomComercial;
                            Doc_Estatus = ficha.Fac_Estatus;
                            Doc_Importe = Convert.ToDouble(ficha.Fac_Saldo);
                            //Doc_Pagado = ficha.Fac_Pagado;
                            Doc_Pagado = Convert.ToDouble(ficha.Fac_Pagado);
                            Fac_FolioFiscal = ficha.Fac_FolioFiscal;
                            TxtReferencia = Convert.ToString(ficha.Id_Fac);
                        }
                        else
                        {
                            Alerta("Movimiento no existe; posiblemente sea un movimiento externo, revise la serie " + MovStr + " " + Convert.ToString(row[2]) + " " + Convert.ToString(row[3]));

                        }
                    }
                    else if (Convert.ToString(row[1]) == "2")
                    {
                        NotaCargo ficha = new NotaCargo();
                        ficha.Id_Emp = Sesion.Id_Emp;
                        ficha.Id_Cd = Sesion.Id_Cd_Ver;
                        ficha.Serie = Convert.ToString(row[2]);
                        ficha.Id_Nca = Convert.ToInt32(row[3]);
                        ficha.Nca_FolioFiscal = Convert.ToString(row[4]);
                        CN_CapPago cn_cappago = new CN_CapPago();

                        int verificador = 0;
                        cn_cappago.ConsultaPagoNotaFicha(ref ficha, Emp_CnxCob, ref verificador);
                        if (verificador == 1)
                        {
                            MovStr = "Nota de cargo";
                            Id_Terr = Convert.ToInt32(ficha.Id_Ter);
                            Doc_Fecha = ficha.Nca_Fecha;
                            Id_Cte = ficha.Id_Cte;
                            Cte_Nombre = ficha.Cte_NomComercial;
                            Doc_Estatus = ficha.Nca_Estatus;
                            Doc_Importe = Convert.ToDouble(ficha.Nca_Total);
                            //Doc_Pagado = ficha.Fac_Pagado;
                            Doc_Pagado = ficha.Nca_Pagado;
                            Fac_FolioFiscal = ficha.Nca_FolioFiscal;
                            TxtReferencia = Convert.ToString(ficha.Id_Nca);
                        }
                        else
                        {
                            Alerta("Movimiento no existe; posiblemente sea un movimiento externo " + MovStr + " " + Convert.ToString(row[2]) + " " + Convert.ToString(row[3]));
                            return;
                        }
                    }
                    if (Doc_Estatus == "B")
                    {
                        Alerta("Movimiento se encuentra cancelado; imposible aplicarle un pago " + MovStr + " " + Convert.ToString(row[2]) + " " + Convert.ToString(row[3]));
                        return;
                    }
                    else if (Doc_Estatus == "C")
                    {
                        Alerta("Movimiento se encuentra en status capturado; imposible aplicarle un pago " + MovStr + " " + Convert.ToString(row[2]) + " " + Convert.ToString(row[3]));
                        return;
                    }

                    if (Math.Round(Convert.ToDouble(Doc_Importe) - Convert.ToDouble(Doc_Pagado), 2) <= 0)
                    {
                        Alerta("Movimiento no tiene saldo positivo; imposible aplicarle un pago " + MovStr + " " + Convert.ToString(row[2]) + " " + Convert.ToString(row[3]));
                        return;
                    }


                    dtDet.Rows.Add(
                        x + 1,
                        Convert.ToString(row[1]),       /*Mov*/
                        MovStr,         /*MovStr*/
                        Convert.ToInt32(TxtReferencia),      /*Ref*/
                        /*Convert.ToString(row[4])*/Fac_FolioFiscal,  /*Fac_FolioFiscal*/
                        Id_Terr,      /*Id_Terr*/
                        Convert.ToString(row[2]),  /*Serie*/
                        Doc_Fecha,        /*Doc_Fecha*/
                        Id_Cte,       /*Id_Cte*/
                        Cte_Nombre,   /*Cte_Nombre aqui*/
                        Convert.ToString(row[1]),      /*Pag_Numero*/
                        Convert.ToString(row[5]),       /*Pag_Cheque*/
                        Convert.ToString(row[6]),       /*Importe*/
                        Doc_Estatus,      /*Doc_Estatus*/
                        Doc_Importe,      /*Doc_Importe*/
                        Doc_Pagado,       /*Doc_Pagado*/
                        Sesion.Id_Cd_Ver         /*Pag_Id_cd*/
                        );

                    Total = Total + Convert.ToDouble(row[6]);
                    SeCargaron++;
                }
                RgDet.Rebind();
                ///AQUI
                RgDet.DataSource = null;
                RgDet.Rebind();
                RgDet.DataSource = dtDet;
                Alerta("Se Importaron Registros : " + SeCargaron.ToString());
                //txtTotal.Text = Total.ToString();
                //txtImporte.Text = Total.ToString();
            }

            catch (Exception ex)
            {
                //                con.Close();
                Alerta(ex.Message.Replace("'", ""));
                //this.DisplayMensajeAlerta(ex.Message);
            }
        }

        protected void ImgAgregar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Agregar();

            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void ImgEliminar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Eliminar(sender);

            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        #endregion
        #region Funciones

        private void MetodosPagoTimbrar(int ID_Emp, int ID_CD, int Id_pag, string conexion, bool metodoSeleccionado, ref string MetodoPago)
        {
            List<PagoDetComplemento> complementos = new List<PagoDetComplemento>();
            List<PagoDet> list_pagos = new List<PagoDet>();
            CN_CatCliente clsCatClientes = new CN_CatCliente();
            CN_CapPago cn_cappago = new CN_CapPago();
            int Id_Pag = Convert.ToInt32(HF_ID.Value);
            int FormaPagoInter = 0;
            int FormaPagosubrogacion = 0;
            Pago pago = new Pago();
            pago.Id_Emp = ID_Emp;
            pago.Id_Cd = ID_CD;
            pago.Id_Pag = Id_pag;

            cn_cappago.ConsultaListaPagos(ref pago, ref list_pagos, conexion);

            if (list_pagos.Count > 0)
            {
                cn_cappago.ConsultaListaComplementosPago(ID_Emp, ID_CD, Id_pag, conexion, ref complementos);

                var facturas = (from tlist in list_pagos
                                join tlist2 in complementos on new { idemp = tlist.Pag_Id_Emp, idcd = tlist.Pag_Id_cd, tlist.Id_Pag } equals new { idemp = tlist2.Id_Emp, idcd = tlist2.Id_Cd, tlist2.Id_Pag } into lista
                                from tlist3 in lista.DefaultIfEmpty()
                                select new
                                {
                                    Cancelacion_XML = tlist3 == null ? null : tlist3.Cancelacion_XML,
                                    Pago_Xml = tlist3 == null ? null : tlist3.Pago_Xml
                                }).ToList();

                var facturaSinSello = (from tlist in facturas
                                       where (tlist.Cancelacion_XML != null && tlist.Pago_Xml != null) || tlist.Pago_Xml == null
                                       select tlist).ToList();

                HF_IdEmp.Value = ID_Emp.ToString();
                HF_IdCd.Value = ID_CD.ToString();

                Clientes cte = new Clientes();

                for (var i = 0; i < list_pagos.Count; i++)
                {
                    cte = new Clientes();
                    cte.Id_Emp = ID_Emp;
                    cte.Id_Cd = list_pagos[i].Pag_Id_cd;
                    cte.Id_Cte = list_pagos[i].Id_Cte;
                    if (list_pagos[i].Pag_Id_cd != ID_CD)
                    {
                        clsCatClientes.ConsultaClienteOtraBD(ref cte, list_pagos[i].Pag_Serie, conexion);
                    }
                    else
                    {
                        clsCatClientes.ConsultaClientes(ref cte, conexion);
                    }

                    MetodoPago = cte.Cte_PagoMetodoPago;
                }

                if (MetodoPago == null)
                {
                    MetodoPago = "3";
                }

                if (metodoSeleccionado)
                {
                    HF_FPI.Value = cboMetodoPago.SelectedValue;
                    if (cboMetodoPago.SelectedValue == "31")
                    {
                        FormaPagoInter++;
                    }
                    else if (cboMetodoPago.SelectedValue == "13")
                    {
                        FormaPagosubrogacion++;
                    }
                }
                else
                {
                    HF_FPI.Value = MetodoPago;
                    if (cte.Cte_MetodoPago == "31")
                    {
                        FormaPagoInter++;
                    }
                    else if (cte.Cte_MetodoPago == "13")
                    {
                        FormaPagosubrogacion++;
                    }
                }



                if (HF_Centro.Value != "")
                {
                    Alerta("No se puede realizar factura de un pago externo");
                }
                else
                {
                    if (FormaPagoInter > 0)
                    {
                        if (facturaSinSello.Count() > 0)
                        {
                            HF_IdCTE.Value = list_pagos.First().Id_Cte.ToString();
                            string script = "radconfirm('¿desea realizar un pago de factoraje para todas las facturas?', confirmCallBackFn, 350, 150);";
                            RAM1.ResponseScripts.Add(script);
                        }
                    }
                    else if (FormaPagosubrogacion > 0)
                    {
                        if (facturaSinSello.Count() > 0)
                        {
                            HF_IdCTE.Value = list_pagos.First().Id_Cte.ToString();
                            string script = "radconfirm('¿desea realizar un pago de subrogación para todas las facturas?', confirmCallBackFnSubrogacion, 350, 150);";
                            RAM1.ResponseScripts.Add(script);
                        }
                    }
                }
            }
        }

        private void Guardar()
        {

        }

        private void ModificarPagosExternos(List<int> centros, Pago pago, List<Banco_Ficha> list_fichas, List<PagoDet> list_pagos, int Id_PagOrigen)
        {
            //Pago pago, 

            Sesion session = new Sesion();
            session = (Sesion)Session["Sesion" + Session.SessionID];


            int Tipo_CDC = 0;
            new CN_CatCliente().ConsultaTipoCDC(session.Id_Cd_Ver, ref Tipo_CDC, session.Emp_Cnx);

            foreach (int c in centros)
            {
                List<PagoDet> cen = list_pagos.Where(PagoDet => PagoDet.Pag_Id_cd == c).ToList();
                if (cen.Count == 0)
                {
                    WS_PagosExternos.Service1 ws = new WS_PagosExternos.Service1();
                    ws.Url = ConfigurationManager.AppSettings["WS_PagosExternos"].ToString();
                    ws.CancelarPagoExterno(Serialize(pago), Emp_CnxCob, session.Emp_Cnx, c, Tipo_CDC);
                }
                else
                {
                    List<int> id_cd_distintos = new List<int>();
                    id_cd_distintos.Add(c);

                    ModificarPagosExternos(pago, list_fichas, list_pagos, Id_PagOrigen, id_cd_distintos);
                }
            }

            List<PagoDet> insertar = list_pagos.Where(PagoDet => !centros.Contains(PagoDet.Pag_Id_cd)).ToList();

            if (insertar.Count > 0)
            {
                List<int> id_cd_distintos = insertar.Select(PagoDet => PagoDet.Pag_Id_cd).Distinct().ToList();
                InsertarPagosExternos(pago, list_fichas, list_pagos, Id_PagOrigen, id_cd_distintos);
            }
        }
        private void InsertarPagosExternos(Pago pago, List<Banco_Ficha> list_fichas, List<PagoDet> list_pagos, int Id_PagOrigen, List<int> Id_CdExt)
        {
            Pago pago_cd = new Pago();
            List<Banco_Ficha> list_fichas_cd = new List<Banco_Ficha>();
            List<PagoDet> list_pagos_cd = new List<PagoDet>();

            Sesion session = new Sesion();
            session = (Sesion)Session["Sesion" + Session.SessionID];

            List<int> id_cd_distintos = new List<int>();

            if (Id_CdExt == null)
            {
                id_cd_distintos = list_pagos.Select(PagoDet => PagoDet.Pag_Id_cd).Distinct().ToList();
            }
            else
            {
                id_cd_distintos = Id_CdExt;
            }


            foreach (int id_cd in id_cd_distintos)
            {
                if (id_cd != session.Id_Cd_Ver)
                {
                    pago_cd = pago;
                    pago_cd.Id_Cd = id_cd;
                    pago_cd.Pag_Estatus = "I";

                    pago_cd.Id_CdOrigen = session.Id_Cd_Ver;
                    pago_cd.Id_UOrigen = session.Id_U;
                    pago_cd.U_Nombre = session.U_Nombre;
                    pago_cd.Id_PagOrigen = Id_PagOrigen;

                    list_pagos_cd = list_pagos.Where(PagoDet => PagoDet.Pag_Id_cd == id_cd).ToList();

                    double total_cd = list_pagos_cd.Sum(PagoDet => PagoDet.Importe);
                    pago_cd.Pag_Importe = total_cd;
                    pago_cd.Pag_Total = total_cd;

                    int[] fichas_distintas = list_pagos_cd.Select(PagoDet => PagoDet.Ficha).Distinct().ToArray();
                    list_fichas_cd.Clear();

                    int siguiente_ficha = 0;

                    foreach (int ficha in fichas_distintas)
                    {
                        siguiente_ficha++;

                        Banco_Ficha ficha_cd = null;
                        for (int x = 0; x < dtGral.Rows.Count; x++)
                        {
                            ficha_cd = new Banco_Ficha();
                            ficha_cd.Pag_Ficha = !string.IsNullOrEmpty(dtGral.Rows[x]["Pag_ficha"].ToString()) ? Convert.ToInt32(dtGral.Rows[x]["Pag_ficha"]) : 0;
                            ficha_cd.Pag_Fecha = !string.IsNullOrEmpty(dtGral.Rows[x]["Pag_Fecha"].ToString()) ? Convert.ToDateTime(dtGral.Rows[x]["Pag_Fecha"]) : DateTime.MinValue;

                            if (ficha_cd.Pag_Ficha == ficha)
                            {
                                break;
                            }
                        }

                        double importe_ficha = list_pagos_cd.Where(PagoDet => PagoDet.Ficha == ficha).Sum(PagoDet => PagoDet.Importe);
                        ficha_cd.Id_Ban = 99;
                        ficha_cd.Pag_Importe = importe_ficha;
                        ficha_cd.Pag_Ficha = siguiente_ficha;

                        list_fichas_cd.Add(ficha_cd);

                        foreach (PagoDet pd in list_pagos_cd.Where(PagoDet => PagoDet.Ficha == ficha).ToList())
                        {
                            pd.Ficha = siguiente_ficha;
                        }
                    }


                    int Tipo_CDC = 0;
                    new CN_CatCliente().ConsultaTipoCDC(session.Id_Cd_Ver, ref Tipo_CDC, session.Emp_Cnx);

                    WS_PagosExternos.Service1 ws = new WS_PagosExternos.Service1();
                    ws.Url = ConfigurationManager.AppSettings["WS_PagosExternos"].ToString();
                    // ws.GuardarPagoExterno(Serialize(pago_cd), Serialize(list_fichas_cd), Serialize(list_pagos_cd), Emp_CnxCob, session.Emp_Cnx, Tipo_CDC);
                }
            }
        }
        private void ModificarPagosExternos(Pago pago, List<Banco_Ficha> list_fichas_o, List<PagoDet> list_pagos_o, int Id_PagOrigen, List<int> Id_CdExt)
        {
            List<Banco_Ficha> list_fichas = ClonarFichas(list_fichas_o);
            List<PagoDet> list_pagos = ClonarPagos(list_pagos_o);

            Pago pago_cd = new Pago();
            List<Banco_Ficha> list_fichas_cd = new List<Banco_Ficha>();
            List<PagoDet> list_pagos_cd = new List<PagoDet>();

            Sesion session = new Sesion();
            session = (Sesion)Session["Sesion" + Session.SessionID];

            List<int> id_cd_distintos = new List<int>();

            if (Id_CdExt == null)
            {
                id_cd_distintos = list_pagos.Select(PagoDet => PagoDet.Pag_Id_cd).Distinct().ToList();
            }
            else
            {
                id_cd_distintos = Id_CdExt;
            }

            foreach (int id_cd in id_cd_distintos)
            {
                if (id_cd != session.Id_Cd_Ver)
                {

                    pago_cd = pago;
                    pago_cd.Id_Cd = id_cd;
                    pago_cd.Pag_Estatus = "I";

                    pago_cd.Id_CdOrigen = session.Id_Cd_Ver;
                    pago_cd.Id_UOrigen = session.Id_U;
                    pago_cd.U_Nombre = session.U_Nombre;
                    pago_cd.Id_PagOrigen = Id_PagOrigen;

                    list_pagos_cd = list_pagos.Where(PagoDet => PagoDet.Pag_Id_cd == id_cd).ToList();

                    double total_cd = list_pagos_cd.Sum(PagoDet => PagoDet.Importe);
                    pago_cd.Pag_Importe = total_cd;
                    pago_cd.Pag_Total = total_cd;

                    int[] fichas_distintas = list_pagos_cd.Select(PagoDet => PagoDet.Ficha).Distinct().ToArray();
                    list_fichas_cd.Clear();

                    int siguiente_ficha = 0;

                    foreach (int ficha in fichas_distintas)
                    {
                        siguiente_ficha++;

                        Banco_Ficha ficha_origen = list_fichas.Where(Banco_Ficha => Banco_Ficha.Pag_Ficha == ficha).ToList()[0];
                        double importe_ficha = list_pagos_cd.Where(PagoDet => PagoDet.Ficha == ficha).Sum(PagoDet => PagoDet.Importe);
                        ficha_origen.Id_Ban = 99;
                        ficha_origen.Pag_Ficha = siguiente_ficha;
                        ficha_origen.Pag_Importe = importe_ficha;

                        list_fichas_cd.Add(ficha_origen);

                        foreach (PagoDet pd in list_pagos_cd.Where(PagoDet => PagoDet.Ficha == ficha).ToList())
                        {
                            pd.Ficha = siguiente_ficha;
                        }
                    }


                    int Tipo_CDC = 0;
                    new CN_CatCliente().ConsultaTipoCDC(session.Id_Cd_Ver, ref Tipo_CDC, session.Emp_Cnx);

                    WS_PagosExternos.Service1 ws = new WS_PagosExternos.Service1();
                    ws.Url = ConfigurationManager.AppSettings["WS_PagosExternos"].ToString();
                    ws.ModificarPagoExterno(Serialize(pago_cd), Serialize(list_fichas_cd), Serialize(list_pagos_cd), Emp_CnxCob, session.Emp_Cnx, Tipo_CDC);
                }
            }
        }

        private void timbrardocumentos(string metodoPago)
        {
            bool proceso = true;
            bool segundoPlano = false;
            Sesion entSesion = new Sesion();
            List<FacturaLite> lstFacturaLite = new List<FacturaLite>();
            FacturaLite entFacturaLite = new FacturaLite();
            CN_CapPago cnPago = new CN_CapPago();
            PagoDetTimbrado entPagoDetTimbrado = new PagoDetTimbrado();
            string strMsjResultado = string.Empty;
            string strRslProceso = string.Empty;
            string strAlerta = string.Empty;
            string strResponseScripts = string.Empty;
            string strFnScript = string.Empty;
            try
            {
                entSesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (ViewState["proceso"] == null)
                {
                    ViewState["proceso"] = proceso;
                }

                if (Convert.ToBoolean(ViewState["proceso"].ToString()) == proceso)
                {
                    proceso = false;
                    ViewState["proceso"] = proceso;

                    if (!ViewState["PageName"].ToString().Equals(HF_PageName.Value))
                    {
                        Alerta("Error en la sesión");
                        return;
                    }

                    int id_cte;
                    Factura Factura = null;
                    NotaCargo entNotaCargo = null;
                    int id_Emp;
                    int id_Cd;
                    int id_Fac;
                    int Tipo_Mov;
                    int Id_PagDet;
                    string serie;
                    string rfc;
                    string strTipo_Mov;
                    int idPag = int.Parse(HF_ID.Value);
                    object objsesion = new object();
                    objsesion = new
                    {
                        entSesion.Emp_Cnx,
                        entSesion.Id_Cd_Ver,
                        entSesion.Id_Emp
                    };
                    string strSesion = JsonConvert.SerializeObject(objsesion);

                    for (int cont = 0; cont <= this.RgDet.Items.Count - 1; cont++)
                    {
                        if (!((CheckBox)this.RgDet.Items[cont].FindControl("ChkTimbrar")).Checked)
                            continue;
                        GridItem item = (GridItem)this.RgDet.Items[cont];
                        Int32.TryParse(item.Cells[7].Text, out id_Emp);
                        Int32.TryParse(item.Cells[8].Text, out id_Fac);
                        Int32.TryParse(item.Cells[9].Text, out id_Cd);
                        Int32.TryParse(item.Cells[10].Text, out id_cte);
                        Int32.TryParse(item.Cells[14].Text, out Tipo_Mov);
                        Int32.TryParse(item.Cells[11].Text, out Id_PagDet);

                        rfc = item.Cells[12].Text;
                        serie = item.Cells[13].Text;

                        entPagoDetTimbrado = new PagoDetTimbrado();
                        entPagoDetTimbrado.Id_Emp = id_Emp;
                        entPagoDetTimbrado.Id_Cd = id_Cd;
                        entPagoDetTimbrado.Id_Pag = idPag;
                        entPagoDetTimbrado.Id_Cte = id_cte;
                        entPagoDetTimbrado.Id_PagDet = Id_PagDet;
                        cnPago.ConsultaPagoDetTimbrado(entSesion.Emp_Cnx, ref entPagoDetTimbrado);
                        if (!string.IsNullOrEmpty(entPagoDetTimbrado.Pago_FolioFiscal))
                        {
                            this.Alerta("El pago de la factura " + id_Fac.ToString() + " ya fue timbrado, por favor actualice la pagina.");
                            return;
                        }

                        Factura = new Factura();
                        Factura.Id_Emp = id_Emp;
                        Factura.Id_Cd = id_Cd;
                        Factura.Id_Fac = id_Fac;
                        List<FacturaDet> ListaFacturaDet = new List<FacturaDet>();
                        if (id_Cd != entSesion.Id_Cd_Ver)
                        {
                            new CN_CapFactura().ConsultaFacturaOtraBDExiste(ref Factura, serie, entSesion.Emp_Cnx);
                        }
                        else
                        {
                            if (Tipo_Mov == 1)
                            {
                                new CN_CapFactura().ConsultaFacturaExiste(ref Factura, ref ListaFacturaDet, entSesion.Emp_Cnx);
                            }
                            else if (Tipo_Mov == 2)
                            {

                                // Validar si existe Nota de cargo
                                entNotaCargo = new NotaCargo();
                                entNotaCargo.Id_Emp = id_Emp;
                                entNotaCargo.Id_Cd = id_Cd;
                                entNotaCargo.Id_Nca = id_Fac;
                                new CN_CapNotaCargo().ConsultaNotaCargoMin(ref entNotaCargo, entSesion.Emp_Cnx);
                                if (entNotaCargo.Id_NcaSerie == null)
                                {
                                    this.Alerta("No se encuentra la Nota de cargo #" + id_Fac.ToString());
                                    return;
                                }
                                else
                                {
                                    Factura.Id_FacSerie = entNotaCargo.Id_NcaSerie;
                                }
                            }
                        }
                        if (Factura.Id_FacSerie == null)
                        {
                            this.Alerta("No se encuentra la factura #" + id_Fac.ToString());
                            return;
                        }

                        entFacturaLite = new FacturaLite();
                        entFacturaLite.id_Pag = idPag;
                        entFacturaLite.id_Fac = id_Fac;
                        entFacturaLite.id_cte = id_cte;
                        entFacturaLite.id_Emp = id_Emp;
                        entFacturaLite.id_Cd = id_Cd;
                        entFacturaLite.rfc = rfc;
                        entFacturaLite.serie = serie;
                        entFacturaLite.MetodoPago = int.Parse(metodoPago);
                        entFacturaLite.strSesion = strSesion;
                        entFacturaLite.Tipo_Mov = Tipo_Mov;
                        lstFacturaLite.Add(entFacturaLite);
                    }

                    if (lstFacturaLite.Count == 0)
                    {
                        strFnScript += "radalert('No se ha seleccionado facturas para timbrar', 330, 150);";
                    }
                    else
                    {
                        // valida el pago no este en pendientes de segundo plano
                        FacturaLite entFacturaValidar = new FacturaLite();
                        entFacturaValidar = lstFacturaLite.First();
                        cnPago.PagoSegundoPlanoConsultaEstatus(entSesion.Emp_Cnx, ref entFacturaValidar);
                        if (entFacturaValidar.Activo == 1)
                        {
                            strMsjResultado = entFacturaValidar.strRstAlerta;
                            strFnScript += "radalert('" + strMsjResultado + "', 330,150); ";
                        }

                        if (string.IsNullOrEmpty(strMsjResultado))
                        {
                            // mas de 5 facturas en el complemento de pago se realiza en segundo plano.
                            if (lstFacturaLite.Count >= 5)
                            {
                                // registra el log en segundo plano, para bloquear que se realice doble timbre o cancelacion
                                entFacturaLite = new FacturaLite();
                                entFacturaLite = lstFacturaLite.First();
                                cnPago.ActivaPagoSegundoPlano(entSesion.Emp_Cnx, ref entFacturaLite, ref strMsjResultado);
                                if (!string.IsNullOrEmpty(strMsjResultado))
                                {
                                    strFnScript += "radalert('" + strMsjResultado + "', 330,150); ";
                                }
                                else
                                {
                                    // envia las facturas a timbrara en segundo plano
                                    segundoPlano = true;
                                    object objPagos = new object();

                                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                    string URL = urlApp + "/Api/ComplementoPago/PagoSegundoPlano";
                                    HttpClient client = new HttpClient();
                                    client.BaseAddress = new Uri(URL);
                                    string strFacturasPago = JsonConvert.SerializeObject(lstFacturaLite);

                                    var data = new System.Net.Http.StringContent(strFacturasPago, Encoding.UTF8, "application/json");
                                    var response = client.PostAsync(URL, data).Result;
                                    if (response != null)
                                    {
                                        strRslProceso = response.Content.ReadAsStringAsync().Result;
                                        string strResult = response.StatusCode.ToString();
                                    }

                                    strFnScript += "ConsultaEstatusPago(2); ";
                                    if (!string.IsNullOrEmpty(strRslProceso))
                                    {
                                        strFnScript += "radalert('" + strRslProceso + "', 330,150); ";
                                    }
                                }
                            }
                            else
                            {
                                // porcesa las facturas en primer plano
                                PrepararPago(entSesion, lstFacturaLite, false, ref strAlerta, ref strResponseScripts);
                                if (!string.IsNullOrEmpty(strAlerta))
                                {
                                    strFnScript += "radalert('" + strAlerta + "', 330, 150); ";
                                }

                                if (!string.IsNullOrEmpty(strResponseScripts))
                                {
                                    strFnScript += string.Concat(" AbrirFacturaPDFVarias('", strResponseScripts, "'); ");
                                }

                            }
                        }
                    }

                    //RgDet.Rebind();
                    txtImporte.Text = "0";
                    HF_Timbrado.Value = "1";
                }
            }
            catch (Exception ex)
            {
                entFacturaLite = lstFacturaLite.First();
                entFacturaLite.strRstAlerta = ex.Message;
                entFacturaLite.strRstScript = ex.StackTrace.ToString();
                cnPago.DesactivaPagoSegundoPlano(entSesion.Emp_Cnx, entFacturaLite);
                proceso = true;
                ViewState["proceso"] = proceso;
                ErrorManager(ex, "Timbrar_Click");
            }
            finally
            {
                if (!segundoPlano)
                    RgDet.Rebind();
                proceso = true;
                ViewState["proceso"] = proceso;
                strFnScript += string.Concat(" HabilitarBoton(); ");
                RAM1.ResponseScripts.Add("setTimeout(function() {" + strFnScript + " }, 2000);");
            }
        }

        public void PrepararPago(Sesion entSesion, List<FacturaLite> ListaFacturaLite, bool isSegundoPlano, ref string strAlerta, ref string strResponseScripts)
        {
            Timbre Timbre = null;
            Factura entFactura = null;
            NotaCargo entNotaCargo = new NotaCargo();
            CN_CatTipoMoneda TipoMoneda = new CN_CatTipoMoneda();
            CN_CapPago Fichas = new CN_CapPago();
            List<TipoMoneda> ListaTipoMoneda = new List<CapaEntidad.TipoMoneda>();
            List<Banco_Ficha> ListaFichas = new List<Banco_Ficha>();
            List<PagoDet> list_pagos = new List<PagoDet>();
            List<PagoDetComplemento> List_DetComplemento = new List<PagoDetComplemento>();
            List<Timbre> Timbres = new List<Timbre>();
            CN_CapPago cnPago = new CN_CapPago();
            string strTipoMoneda = string.Empty;

            int Id_Mon_Nca = 0;
            try
            {
                foreach (var itemFacturaLite in ListaFacturaLite)
                {
                    entFactura = new Factura();
                    entFactura.Id_Fac = itemFacturaLite.id_Fac;
                    entFactura.Id_Emp = itemFacturaLite.id_Emp;
                    entFactura.Id_Cd = itemFacturaLite.id_Cd;
                    List<FacturaDet> ListaFacturaDet = new List<FacturaDet>();
                    strTipoMoneda = string.Empty;
                    if (itemFacturaLite.id_Cd != entSesion.Id_Cd_Ver)
                    {
                        new CN_CapFactura().ConsultaFacturaOtraBD(ref entFactura, itemFacturaLite.serie, entSesion.Emp_Cnx);
                    }
                    else
                    {
                        if (itemFacturaLite.Tipo_Mov == 1)
                        {
                            new CN_CapFactura().ConsultaFacturaMin(ref entFactura, entSesion.Emp_Cnx);
                        }
                        else if (itemFacturaLite.Tipo_Mov == 2)
                        {
                            // Validar si existe Nota de cargo                            
                            entNotaCargo = new NotaCargo();
                            entNotaCargo.Id_Emp = itemFacturaLite.id_Emp;
                            entNotaCargo.Id_Cd = itemFacturaLite.id_Cd;
                            entNotaCargo.Id_Nca = itemFacturaLite.id_Fac;
                            new CN_CapNotaCargo().ConsultaNotaCargoMin(ref entNotaCargo, entSesion.Emp_Cnx);
                            strTipoMoneda = BuscarTipoMoneda(entNotaCargo.Nca_Xml);
                            entFactura.Fac_SubTotal = entNotaCargo.Nca_Subtotal;
                            entFactura.Fac_ImporteIva = entNotaCargo.Nca_Iva;
                            entFactura.Fac_FPago = entNotaCargo.Nca_FPago;
                            entFactura.Fac_CteRfc = entNotaCargo.Nca_CteRfc;
                        }
                    }
                    Pago pago = new Pago();
                    pago.Id_Emp = entSesion.Id_Emp;
                    pago.Id_Cd = entSesion.Id_Cd_Ver;
                    pago.Id_Pag = itemFacturaLite.id_Pag;
                    TipoMoneda.ConsultaTipoMonedaXMLFacturacion(itemFacturaLite.id_Emp, entSesion.Emp_Cnx, ref ListaTipoMoneda);

                    Fichas.ConsultaPago2(ref pago, ref ListaFichas, ref list_pagos, entSesion.Emp_Cnx, ref List_DetComplemento);
                    // validar la Pag_Fecha debe ser mayor a 01-01-2010" 
                    if (ListaFichas.Any(x => x.Pag_Fecha < new DateTime(2010, 1, 1)))
                    {
                        strAlerta += "La fecha en la ficha de pago " + ListaFichas.Where(x => x.Pag_Fecha < new DateTime(2010, 1, 1)).Select(y => y.Pag_Ficha).First().ToString() + " debe ser mayor a 01-01-2010";
                        return;
                    }


                    TipoMoneda moneda = new TipoMoneda();
                    if (itemFacturaLite.Tipo_Mov == 2)
                    {
                        moneda = (from tlist in ListaTipoMoneda
                                  where tlist.Mon_codigo == strTipoMoneda
                                  select tlist).FirstOrDefault();
                    }
                    else
                    {
                        moneda = (from tlist in ListaTipoMoneda
                                  where tlist.Id_Mon == entFactura.Id_Mon
                                  select tlist).FirstOrDefault();
                    }

                    Banco_Ficha ficha = new Banco_Ficha();
                    ficha = (from plist in list_pagos
                             join flist in ListaFichas on plist.Pag_Numero equals flist.Pag_Ficha
                             where Convert.ToInt32(plist.Ref) == entFactura.Id_Fac
                             orderby flist.Pag_Ficha
                             select flist).FirstOrDefault();
                    //if (Timbres.Any(x => x.Cliente.Cte_FacRfc.Replace(" ", string.Empty) == rfc.Replace(" ", string.Empty) && x.TipoMoneda.Id_Mon == moneda.Id_Mon))
                    if (Timbres.Any(x => x.Facturas.Any(y => y.Fac_CteRfc.Replace(" ", string.Empty) == itemFacturaLite.rfc.Replace(" ", string.Empty)) && x.TipoMoneda.Id_Mon == moneda.Id_Mon && x.PFicha.Pag_NumOperacion == ficha.Pag_NumOperacion && x.PFicha.Pag_Fecha == ficha.Pag_Fecha))
                    {
                        //agregar factura de cliente existente
                        Timbre = Timbres.FirstOrDefault<Timbre>(x => x.Facturas.Any(y => y.Fac_CteRfc.Replace(" ", string.Empty) == itemFacturaLite.rfc.Replace(" ", string.Empty)) && x.TipoMoneda.Id_Mon == moneda.Id_Mon && x.PFicha.Pag_NumOperacion == ficha.Pag_NumOperacion && x.PFicha.Pag_Fecha == ficha.Pag_Fecha);
                        Timbre.Facturas.Add(entFactura);
                    }
                    else
                    {
                        //agregar cliente y factura
                        CapaEntidad.Clientes Cliente = new Clientes();
                        Cliente.Id_Emp = itemFacturaLite.id_Emp;
                        Cliente.Id_Cd = itemFacturaLite.id_Cd;
                        Cliente.Id_Cte = itemFacturaLite.id_cte;
                        Cliente.Ignora_Inactivo = true;
                        CN_CatCliente clsCatClientes = new CN_CatCliente();
                        if (itemFacturaLite.id_Cd != entSesion.Id_Cd_Ver)
                        {
                            clsCatClientes.ConsultaClienteOtraBD(ref Cliente, itemFacturaLite.serie, entSesion.Emp_Cnx);
                        }
                        else
                        {
                            clsCatClientes.ConsultaClientesBasico(ref Cliente, entSesion.Emp_Cnx, false);
                        }

                        Timbre = new Timbre();
                        Timbre.Cliente = Cliente;
                        Timbre.TipoMoneda = new TipoMoneda();
                        Timbre.TipoMoneda = moneda;
                        Timbre.Facturas = new List<CapaEntidad.Factura>();
                        Timbre.Facturas.Add(entFactura);
                        Timbre.PFicha = ficha;
                        Timbres.Add(Timbre);
                    }
                }

                this.ImprimirTimbres(entSesion, Timbres, ListaFacturaLite.Select(x => x.MetodoPago).FirstOrDefault(), ListaFacturaLite.Select(x => x.id_Pag).FirstOrDefault(), ref strAlerta, ref strResponseScripts, isSegundoPlano);

            }
            catch (Exception ex)
            {
                strAlerta += ex.Message + ex.StackTrace.ToString();
                throw ex;
            }
            finally
            {
                if (isSegundoPlano)
                {
                    //foreach (var itemFacturaLite in ListaFacturaLite)
                    //{
                    //string strAlerta, string strResponseScripts
                    FacturaLite entFacturaLite = ListaFacturaLite.First();
                    entFacturaLite.strRstAlerta = strAlerta;
                    entFacturaLite.strRstScript = strResponseScripts;
                    cnPago.DesactivaPagoSegundoPlano(entSesion.Emp_Cnx, entFacturaLite);
                    //break;
                    //}

                }
            }
        }

        private string BuscarTipoMoneda(object xml)
        {
            // Carga el XML en un objeto XDocument
            XDocument document = XDocument.Parse(xml.ToString());

            // Define el namespace para 'cfdi'
            XNamespace cfdi = "http://www.sat.gob.mx/cfd/4";

            // Obtiene el valor del atributo 'Moneda'
            string moneda = document.Root.Attribute("Moneda")?.Value;

            return moneda;
        }


        private List<PagoDet> ClonarPagos(List<PagoDet> list_pagos_o)
        {
            List<PagoDet> clon = new List<PagoDet>();
            PagoDet ficha_cd;

            foreach (PagoDet bf in list_pagos_o)
            {
                ficha_cd = new PagoDet();

                ficha_cd.Cheque = bf.Cheque;
                ficha_cd.Cte_Nombre = bf.Cte_Nombre;
                ficha_cd.Doc_Estatus = bf.Doc_Estatus;
                ficha_cd.Doc_Fecha = bf.Doc_Fecha;
                ficha_cd.Doc_Importe = bf.Doc_Importe;
                ficha_cd.Doc_Pagado = bf.Doc_Pagado;
                ficha_cd.Ficha = bf.Ficha;
                ficha_cd.Id_Cte = bf.Id_Cte;
                ficha_cd.Id_Terr = bf.Id_Terr;
                ficha_cd.Importe = bf.Importe;
                ficha_cd.Mov = bf.Mov;
                ficha_cd.MovStr = bf.MovStr;
                ficha_cd.Pag_Cheque = bf.Pag_Cheque;
                ficha_cd.Pag_Id_cd = bf.Pag_Id_cd;
                ficha_cd.Pag_Numero = bf.Pag_Numero;
                ficha_cd.Ref = bf.Ref;
                ficha_cd.Pag_Id_Ter = bf.Pag_Id_Ter;
                ficha_cd.Pag_Fac_Fecha = bf.Pag_Fac_Fecha;
                ficha_cd.Pag_Id_cte = bf.Pag_Id_cte;
                ficha_cd.Pag_Cte_Nombre = bf.Pag_Cte_Nombre;
                ficha_cd.Serie = bf.Serie;
                clon.Add(ficha_cd);
            }
            return clon;
        }
        private List<Banco_Ficha> ClonarFichas(List<Banco_Ficha> list_fichas_o)
        {
            List<Banco_Ficha> clon = new List<Banco_Ficha>();
            Banco_Ficha ficha_cd;

            foreach (Banco_Ficha bf in list_fichas_o)
            {
                ficha_cd = new Banco_Ficha();

                ficha_cd.Ban_Cuenta = bf.Ban_Cuenta;
                ficha_cd.Ban_Nombre = bf.Ban_Nombre;
                ficha_cd.Id_Ban = bf.Id_Ban;
                ficha_cd.Pag_Fecha = bf.Pag_Fecha;
                ficha_cd.Pag_Ficha = bf.Pag_Ficha;
                ficha_cd.Pag_Importe = bf.Pag_Importe;
                ficha_cd.Pag_NumOperacion = bf.Pag_NumOperacion;

                clon.Add(ficha_cd);
            }
            return clon;
        }
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
            string metodoPago = "";
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            Funciones funcion = new Funciones();
            CargarTipo();
            CargarMovimientos();
            CargarFormaPago();
            GetListGrl();
            GetListDet();

            //funcion que revisas si existe factura sin sello para mostrar el mensaje de factoraje.


            if (Request.QueryString["id"] != null && Request.QueryString["id"] != "-1")
            {
                txtClave.Text = Request.QueryString["id"];
                HF_ID.Value = Request.QueryString["id"];
                HF_FechaPago.Value = Request.QueryString["fechaPago"];

                MetodosPagoTimbrar(Convert.ToInt32(Sesion.Id_Emp), Convert.ToInt32(Sesion.Id_Cd_Ver), Convert.ToInt32(HF_ID.Value), Sesion.Emp_Cnx, false, ref metodoPago);

                cboMetodoPago.SelectedValue = metodoPago;
                HF_MP.Value = metodoPago;
                cargarPago();
                txtClave.Enabled = false;
                cmbTipo.Enabled = false;
                rdFechaPago.Enabled = false;
                cmbMovimiento.Enabled = false;
                txtMovimiento.Enabled = false;
                //RadTab tab1 = RadTabStrip1.Tabs.FindTabByText("Timbres");
                //tab1.Selected = true;
            }

            HF_PageName.Value = Guid.NewGuid().ToString().Replace("-", "");
            ViewState["PageName"] = HF_PageName.Value;
            _PermisoGuardar = Convert.ToBoolean(Request.QueryString["PermisoGuardar"]);
            _PermisoModificar = Convert.ToBoolean(Request.QueryString["PermisoModificar"]);
            _PermisoEliminar = Convert.ToBoolean(Request.QueryString["PermisoEliminar"]);
            _PermisoImprimir = Convert.ToBoolean(Request.QueryString["PermisoImprimir"]);
            ValidarPermisos();
            HF_Timbrado.Value = "0";
            if (Request.QueryString["Ext"] != null)
            {
                this.Title = "Pagos extemporaneos";

                CN_CapPago cn_cappago = new CN_CapPago();
                Pago pag = new Pago();
                pag.Id_Emp = Sesion.Id_Emp;
                pag.Id_Cd = Sesion.Id_Cd_Ver;
                int verificador = 0;
                cn_cappago.PermitirExtemporaneo(pag, Sesion.Emp_Cnx, ref verificador);
                if (!Convert.ToBoolean(verificador))
                {
                    //RAM1.ResponseScripts.Add("CloseAlert('Ya se ha efectuado el cierre extemporáneo de pagos, no es posible capturar el pago extemporáneo');");
                }
            }

        }

        private void CargarTipo()
        {
            try
            {

                cmbTipo.Items.Add(new RadComboBoxItem("-- Seleccionar --", "-1"));
                cmbTipo.Items.Add(new RadComboBoxItem("Factura", "0"));
                cmbTipo.Items.Add(new RadComboBoxItem("Pago", "1"));
                cmbTipo.Items.Add(new RadComboBoxItem("Nota de cargo", "2"));
                cmbTipo.Items.Add(new RadComboBoxItem("Nota de credito", "3"));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarMovimientos()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(2, 0, Sesion.Id_Emp, 1, Sesion.Emp_Cnx, "spCatMovimiento_Combo", ref cmbMovimiento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void cargarPago()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Pago pago = new Pago();
                List<Banco_Ficha> list_fichas = new List<Banco_Ficha>();
                List<PagoDet> list_pagos = new List<PagoDet>();
                List<PagoDetComplemento> List_DetComplemento = new List<PagoDetComplemento>();
                List<Comun> Lista = new List<Comun>();
                double importeTotal = 0;

                pago.Id_Emp = Sesion.Id_Emp;
                pago.Id_Cd = Sesion.Id_Cd_Ver;
                pago.Id_Pag = Convert.ToInt32(HF_ID.Value);
                CN_CapPago cn_cappago = new CN_CapPago();
                cn_cappago.ConsultaPago2(ref pago, ref list_fichas, ref list_pagos, Sesion.Emp_Cnx, ref List_DetComplemento);
                LlenarFormaMetodoPago(ref list_pagos);
                cmbTipo.SelectedIndex = cmbTipo.FindItemIndexByValue(pago.Tipo.ToString());
                rdFechaPago.DbSelectedDate = pago.Pag_Fecha.Year == 1 ? (DateTime?)null : pago.Pag_Fecha;
                cmbMovimiento.SelectedIndex = cmbMovimiento.FindItemIndexByValue(pago.Id_Tmov.ToString());
                txtMovimiento.Text = pago.Id_Tmov.ToString();

                for (int x = 0; x < list_fichas.Count; x++)
                {
                    dtGral.Rows.Add(
                        x + 1,
                        list_fichas[x].Pag_Ficha,
                        list_fichas[x].Pag_Fecha,
                        list_fichas[x].Id_Ban,
                        list_fichas[x].Ban_Nombre,
                        list_fichas[x].Pag_Importe,
                        list_fichas[x].Ban_Cuenta
                        );
                    Lista.Add(new Comun
                    {
                        Id = list_fichas[x].Id,
                        Descripcion = list_fichas[x].Descripcion
                    });
                }

                RgGral.Rebind();
                for (int x = 0; x < list_pagos.Count; x++)
                {
                    importeTotal += list_pagos[x].Importe;
                    dtDet.Rows.Add(
                        x + 1,
                        list_pagos[x].Mov,
                        list_pagos[x].MovStr,
                        list_pagos[x].Ref,
                        list_pagos[x].Fac_FolioFiscal,
                        list_pagos[x].FormaPago,
                        list_pagos[x].MetodoPago,
                        list_pagos[x].Id_Terr,
                        list_pagos[x].Serie,
                        list_pagos[x].Doc_Fecha,
                        list_pagos[x].Id_Cte,
                        list_pagos[x].Cte_Nombre,
                        list_pagos[x].Pag_Numero,
                        list_pagos[x].Pag_Cheque,
                        list_pagos[x].Importe,
                        list_pagos[x].Doc_Estatus,
                        list_pagos[x].Doc_Importe,
                        list_pagos[x].Doc_Pagado,
                        list_pagos[x].Pag_Id_cd,
                        Sesion.Id_Emp,
                        list_pagos[x].Id_PagDet,
                        list_pagos[x].Cte_Rfc.Replace(" ", string.Empty)
                        );
                }
                txtImporte.Value = 0;
                RgDet.Rebind();

                dtDetInicial = dtDet;

                //list_fichas Pag_ficha
                List<int> lstIntPagos = list_pagos.Select(x => x.Pag_Numero).Distinct().ToList();
                Lista = Lista.Where(x => lstIntPagos.Contains(x.Id)).Select(y => new Comun { Id = y.Id, Descripcion = y.Descripcion }).ToList();
                List<Comun> Lista2 = new List<Comun>();
                Lista2.Add(new Comun
                {
                    Id = -1,
                    Descripcion = "Todos"
                });
                Lista2.AddRange(Lista);

                cmbFicha.DataSource = Lista2;
                cmbFicha.DataValueField = "Id";
                cmbFicha.DataTextField = "Descripcion";
                cmbFicha.DataBind();

                cmbFicha.SelectedValue = "-1";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LlenarFormaMetodoPago(ref List<PagoDet> list_pagos)
        {
            string strFormaPago = string.Empty;
            string strMetodoPago = string.Empty;
            XmlDocument xmlFactura = new XmlDocument();

            for (int i = 0; i < list_pagos.Count; i++)
            {
                if (!string.IsNullOrEmpty(list_pagos[i].Fac_Xml))
                {
                    strFormaPago = string.Empty;
                    strMetodoPago = string.Empty;
                    xmlFactura.LoadXml(list_pagos[i].Fac_Xml);
                    foreach (XmlNode nodoSistemaFacturacion in xmlFactura.ChildNodes)
                    {
                        if (nodoSistemaFacturacion.Name == "cfdi:Comprobante")
                        {
                            strFormaPago = nodoSistemaFacturacion.Attributes["FormaPago"].Value;
                            strMetodoPago = nodoSistemaFacturacion.Attributes["MetodoPago"].Value;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(strFormaPago))
                    {
                        list_pagos[i].FormaPago = strFormaPago;
                    }
                    if (!string.IsNullOrEmpty(strMetodoPago))
                    {
                        list_pagos[i].MetodoPago = strMetodoPago;
                    }
                }
            }
        }

        private void LimpiarRegistro(RadComboBox rdBox)
        {
            ((RadNumericTextBox)rdBox.Parent.Parent.FindControl("txtProd")).Text = string.Empty;
            ((Label)rdBox.Parent.Parent.FindControl("lblPres2")).Text = string.Empty;
            ((Label)rdBox.Parent.Parent.FindControl("lblUnidad2")).Text = string.Empty;
            ((RadNumericTextBox)rdBox.Parent.Parent.FindControl("txtPrecio")).Text = "0";
            ((RadNumericTextBox)rdBox.Parent.Parent.FindControl("txtCantidad")).Text = "0";
            ((RadNumericTextBox)rdBox.Parent.Parent.FindControl("txtImporte")).Text = "0";
        }
        private string MaximoId()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, "CapPago", "Id_Pag", Sesion.Emp_Cnx, "spCatLocal_Maximo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Nuevo()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Funciones funcion = new Funciones();
                dtDet.Rows.Clear();
                dtGral.Rows.Clear();

                // txtImporte.Value = 0;
                // txtTotal.Value = 0;
                RadTabStrip1.Tabs[0].Selected = true;
                RadMultiPage1.PageViews[0].Selected = true;
                RgDet.Rebind();
                HF_ID.Value = "";
            }
            catch (Exception ex)
            {
                throw ex;
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
        private void CalcularImporteFichas()
        {
            double importe = 0;
            for (int x = 0; x < dtGral.Rows.Count; x++)
                importe += (Convert.ToDouble(dtGral.Rows[x]["Pag_Importe"]));
            // txtImporte.Text = importe.ToString();
        }
        private void CalcularTotalDetalle()
        {
            double importe = 0;
            for (int x = 0; x < dtDet.Rows.Count; x++)
                importe += (Convert.ToDouble(dtDet.Rows[x]["Pag_Importe"]));

            //txtTotal.Text = importe.ToString();
        }
        private void ValidarPermisos()
        {
            try
            {
                if (_PermisoGuardar == false)
                    this.rtb1.Items[6].Visible = false;
                if (_PermisoGuardar == false & _PermisoModificar == false)
                    this.rtb1.Items[5].Visible = false;
                //Regresar
                this.rtb1.Items[4].Visible = false;
                //Eliminar
                this.rtb1.Items[3].Visible = false;
                //Imprimir
                this.rtb1.Items[2].Visible = false;
                //Correo
                this.rtb1.Items[1].Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /*General*/
        private void GetListGrl()
        {
            try
            {
                dtGral = new DataTable();
                dtGral.Columns.Add("rgGralId", System.Type.GetType("System.Int32"));
                dtGral.Columns.Add("Pag_ficha", System.Type.GetType("System.Int32"));
                dtGral.Columns.Add("Pag_Fecha", System.Type.GetType("System.DateTime"));
                dtGral.Columns.Add("Pag_Banco", System.Type.GetType("System.String"));
                dtGral.Columns.Add("Pag_BancoStr", System.Type.GetType("System.String"));
                dtGral.Columns.Add("Pag_Importe", System.Type.GetType("System.Double"));
                dtGral.Columns.Add("Ban_Cuenta", System.Type.GetType("System.String"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Delete(GridCommandEventArgs e)
        {
            int rgGralId = 0;
            GridItem gi = null;
            DataRow[] Ar_dr;
            DataRow dr;
            gi = e.Item;
            rgGralId = Convert.ToInt32(((Label)gi.FindControl("lblGralId1")).Text);
            Ar_dr = dtDet.Select("Pag_Numero='" + rgGralId + "'");
            if (Ar_dr.Length > 0)
            {
                Alerta("No se puede eliminar, ya existen detalles de esta ficha");
                return;
            }
            Ar_dr = dtGral.Select("rgGralId='" + rgGralId + "'");
            if (Ar_dr.Length > 0)
            {
                Ar_dr[0].Delete();
                dtGral.AcceptChanges();
            }
            for (int x = 0; x < dtGral.Rows.Count; x++)
            {
                dr = dtGral.Rows[x];
                dr.BeginEdit();
                dr["rgGralId"] = x + 1;
                dr.AcceptChanges();
            }
            CalcularImporteFichas();
        }
        private void PerformInsert(GridCommandEventArgs e)
        {
            int rgGralId = 0;
            int ficha = 0;
            DateTime Fecha = default(DateTime);
            int Banco = 0;
            double Importe = 0;
            string BancoStr = "";
            string BancoCuenta = "";
            GridItem gi = e.Item;

            if (!((RadDatePicker)gi.FindControl("rdFecha")).SelectedDate.HasValue ||
                ((RadComboBox)gi.FindControl("cmbBancos")).SelectedValue == "-1" ||
                ((RadNumericTextBox)gi.FindControl("rgtxtImporte")).Text == "")
            {
                e.Canceled = true;
                this.Alerta("Todos los campos son requeridos");
                return;
            }
            ficha = dtGral.Rows.Count + 1;//Convert.ToInt32(((RadNumericTextBox)gi.FindControl("rgtxtFicha")).Text);
            Fecha = Convert.ToDateTime(((RadDatePicker)gi.FindControl("rdFecha")).SelectedDate);
            Banco = Convert.ToInt32(((RadComboBox)gi.FindControl("cmbBancos")).SelectedValue);
            BancoStr = ((RadComboBox)gi.FindControl("cmbBancos")).SelectedItem.Text;
            BancoCuenta = ((RadTextBox)gi.FindControl("txtBanCuenta")).Text;
            Importe = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("rgtxtImporte")).Text);
            rgGralId = dtGral.Rows.Count + 1;
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            DataTable dt_temp = dtGral;
            dt_temp.Rows.Add(new object[] { rgGralId, ficha, Fecha, Banco, BancoStr, Importe, BancoCuenta });
            dtGral = dt_temp;
            CalcularImporteFichas();

        }
        private void Update(GridCommandEventArgs e)
        {
            int rgGralId = 0;
            int ficha = 0;
            DateTime Fecha = default(DateTime);
            int Banco = 0;
            double Importe = 0;
            string BancoStr = "";
            string Ban_Cuenta = "";
            GridItem gi = e.Item;
            DataRow[] Ar_dr;
            if (!((RadDatePicker)gi.FindControl("rdFecha")).SelectedDate.HasValue ||
                ((RadComboBox)gi.FindControl("cmbBancos")).SelectedValue == "-1" ||
                ((RadNumericTextBox)gi.FindControl("rgtxtImporte")).Text == "")
            {
                e.Canceled = true;
                return;
            }
            ficha = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("rgtxtFicha")).Text);
            Fecha = Convert.ToDateTime(((RadDatePicker)gi.FindControl("rdFecha")).SelectedDate);
            Banco = Convert.ToInt32(((RadComboBox)gi.FindControl("cmbBancos")).SelectedValue);
            BancoStr = ((RadComboBox)gi.FindControl("cmbBancos")).SelectedItem.Text;
            Ban_Cuenta = ((RadTextBox)gi.FindControl("txtBanCuenta")).Text;// ((RadComboBox)gi.FindControl("txtBanCuenta")).SelectedItem.Text;
            Importe = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("rgtxtImporte")).Text);
            rgGralId = Convert.ToInt32(((Label)gi.FindControl("lblGralId2")).Text);

            Ar_dr = dtGral.Select("rgGralId='" + rgGralId + "'");
            if (Ar_dr.Length > 0)
            {
                Ar_dr[0].BeginEdit();
                Ar_dr[0]["Pag_ficha"] = ficha;
                Ar_dr[0]["Pag_Fecha"] = Fecha;
                Ar_dr[0]["Pag_Banco"] = Banco;
                Ar_dr[0]["Pag_BancoStr"] = BancoStr;
                Ar_dr[0]["Pag_Importe"] = Importe;
                Ar_dr[0]["Ban_Cuenta"] = Ban_Cuenta;
                Ar_dr[0].AcceptChanges();
            }
            CalcularImporteFichas();
        }
        private void InitInsert(GridCommandEventArgs e)
        {

        }
        /*Detalle*/
        private void GetListDet()
        {
            try
            {
                dtDet = new DataTable();
                dtDet.Columns.Add("RgDId", System.Type.GetType("System.Int32"));
                dtDet.Columns.Add("Pag_Movimiento", System.Type.GetType("System.Int32"));
                dtDet.Columns.Add("Pag_MovimientoStr", System.Type.GetType("System.String"));
                dtDet.Columns.Add("Doc_Referencia", System.Type.GetType("System.String"));
                dtDet.Columns.Add("Fac_FolioFiscal", System.Type.GetType("System.String"));
                dtDet.Columns.Add("FormaPago", System.Type.GetType("System.String"));
                dtDet.Columns.Add("MetodoPago", System.Type.GetType("System.String"));
                dtDet.Columns.Add("Id_Terr", System.Type.GetType("System.Int32"));
                dtDet.Columns.Add("Serie", System.Type.GetType("System.String"));
                dtDet.Columns.Add("Doc_Fecha", System.Type.GetType("System.DateTime"));
                dtDet.Columns.Add("Id_Cte", System.Type.GetType("System.Int32"));
                dtDet.Columns.Add("Cte_Nombre", System.Type.GetType("System.String"));
                dtDet.Columns.Add("Pag_Numero", System.Type.GetType("System.Int32"));
                dtDet.Columns.Add("Pag_Cheque", System.Type.GetType("System.String"));
                dtDet.Columns.Add("Pag_Importe", System.Type.GetType("System.Double"));
                dtDet.Columns.Add("Doc_Estatus", System.Type.GetType("System.String"));
                dtDet.Columns.Add("Doc_Importe", System.Type.GetType("System.Double"));
                dtDet.Columns.Add("Doc_Pagado", System.Type.GetType("System.Double"));
                dtDet.Columns.Add("Cdi", System.Type.GetType("System.Int32"));
                dtDet.Columns.Add("Id_Emp", System.Type.GetType("System.Int32"));
                dtDet.Columns.Add("Id_PagDet", System.Type.GetType("System.Int32"));
                dtDet.Columns.Add("Cte_Rfc", System.Type.GetType("System.String"));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void DeleteDet(GridCommandEventArgs e)
        {
            try
            {
                int RgDId = 0;
                GridItem gi = null;
                DataRow[] Ar_dr;
                DataRow dr;
                gi = e.Item;
                RgDId = Convert.ToInt32(((Label)gi.FindControl("lblDetlId1")).Text);
                Ar_dr = dtDet.Select("RgDId='" + RgDId + "'");
                if (Ar_dr.Length > 0)
                {
                    Ar_dr[0].Delete();
                    dtDet.AcceptChanges();
                }
                for (int x = 0; x < dtDet.Rows.Count; x++)
                {
                    dr = dtDet.Rows[x];
                    dr.BeginEdit();
                    dr["RgDId"] = x + 1;
                    dr.AcceptChanges();
                }
                CalcularTotalDetalle();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void PerformInsertDet(GridCommandEventArgs e)
        {
            try
            {
                int RgDId = 0;
                int Pag_Movimiento = 0;
                string Pag_MovimientoStr = "";
                DateTime Doc_Fecha = default(DateTime);
                string Doc_Referencia = "";
                string Fac_FolioFiscal = "";
                int Id_Terr = 0;
                string Serie = "";
                int Id_Cte = 0;
                string Cte_Nombre = "";
                int Pag_Numero = 0;
                string Pag_Cheque = "";
                double Pag_Importe = 0;
                string Doc_Estatus = "";
                double Doc_Importe = 0;
                double Doc_Pagado = 0;
                int Cdi = 0;
                GridItem gi = e.Item;

                if (((RadTextBox)gi.FindControl("rgSerie")).Text == "" ||
                    ((RadTextBox)gi.FindControl("rgReferencia")).Text == "" ||
                    !((Telerik.Web.UI.RadDatePicker)gi.FindControl("rdFecha")).SelectedDate.HasValue ||
                    ((Telerik.Web.UI.RadNumericTextBox)gi.FindControl("rgTerr")).Text == "" ||
                    ((Telerik.Web.UI.RadNumericTextBox)gi.FindControl("rgCdi")).Text == "" ||
                    ((Telerik.Web.UI.RadNumericTextBox)gi.FindControl("rgCte")).Text == "" ||
                    ((Telerik.Web.UI.RadTextBox)gi.FindControl("rgCteNombre")).Text == "" ||
                    ((Telerik.Web.UI.RadComboBox)gi.FindControl("rgcmbNum")).Text == "" ||
                    ((Telerik.Web.UI.RadNumericTextBox)gi.FindControl("rgtxtImporte")).Text == ""
                    )
                {
                    Alerta("Todos los campos son requeridos");
                    e.Canceled = true;
                    return;
                }
                if (((RadNumericTextBox)gi.FindControl("rgtxtImporte")).Value <= 0)
                {
                    Alerta("El importe debe ser mayor a cero");
                    e.Canceled = true;
                    return;
                }
                Serie = ((RadTextBox)gi.FindControl("rgSerie")).Text;
                Doc_Fecha = Convert.ToDateTime(((RadDatePicker)gi.FindControl("rdFecha")).SelectedDate);
                Doc_Referencia = ((RadTextBox)gi.FindControl("rgReferencia")).Text;
                Fac_FolioFiscal = ((RadTextBox)gi.FindControl("rgFolioFiscal")).Text;
                Pag_Movimiento = Convert.ToInt32(((RadComboBox)gi.FindControl("rgcmbMov")).SelectedValue);
                Pag_MovimientoStr = ((RadComboBox)gi.FindControl("rgcmbMov")).Text;
                Id_Terr = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("rgTerr")).Text);
                Id_Cte = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("rgCte")).Text);
                Cte_Nombre = ((RadTextBox)gi.FindControl("rgCteNombre")).Text;
                Pag_Numero = Convert.ToInt32(((RadComboBox)gi.FindControl("rgcmbNum")).SelectedItem.Text);
                Pag_Cheque = ((RadTextBox)gi.FindControl("rgCheque")).Text;
                Pag_Importe = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("rgtxtImporte")).Text);
                Doc_Estatus = ((RadTextBox)gi.FindControl("rgEstatus")).Text;
                Doc_Importe = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("rgImporte")).Value.HasValue ? ((RadNumericTextBox)gi.FindControl("rgImporte")).Value.Value : -1);
                Doc_Pagado = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("rgPagado")).Value.HasValue ? ((RadNumericTextBox)gi.FindControl("rgPagado")).Value.Value : -1);
                Cdi = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("rgCdi")).Text);

                /* if (Math.Round(Convert.ToDouble(txtImporte.Text), 2) < (Math.Round(Convert.ToDouble(txtTotal.Text) + Pag_Importe, 2)))
                 {
                     Alerta("El total del detalle excede al importe de las fichas");
                     e.Canceled = true;
                     return;
                 }
                 */
                RgDId = dtDet.Rows.Count + 1;
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                dtDet.Rows.Add(new object[] {
                RgDId,
                Pag_Movimiento,
                Pag_MovimientoStr,
                Doc_Referencia,
                Fac_FolioFiscal,
                Id_Terr,
                Serie,
                Doc_Fecha,
                Id_Cte,
                Cte_Nombre,
                Pag_Numero,
                Pag_Cheque,
                Pag_Importe,
                Doc_Estatus,
                Doc_Importe,
                Doc_Pagado,
                Cdi
            });
                if (Doc_Importe != -1 && Doc_Pagado != -1)
                {
                    double pagadoDoc = CalcularSaldoDocumento(Doc_Referencia, Doc_Importe - Doc_Pagado);
                    if (pagadoDoc > 0)
                        Alerta("Saldo del documento <b>" + pagadoDoc.ToString("$ #,##0.00") + "</b>");
                }
                CalcularTotalDetalle();
            }
            catch (Exception ex)
            {
                e.Canceled = true;
                throw ex;

            }
        }
        private double CalcularSaldoDocumento(string Doc_Referencia, double Doc_Saldo)
        {
            try
            {
                double saldo = 0;
                DataRow[] dr_Ar = dtDet.Select("Doc_Referencia='" + Doc_Referencia + "'");
                foreach (DataRow dr in dr_Ar)
                {
                    saldo += (Double)dr["Pag_Importe"];
                }
                saldo = Doc_Saldo - saldo;
                return saldo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void UpdateDet(GridCommandEventArgs e)
        {
            try
            {
                int RgDId = 0;
                int Pag_Movimiento = 0;
                string Pag_MovimientoStr = "";
                DateTime Doc_Fecha = default(DateTime);
                string Doc_Referencia = "";
                string Fac_FolioFiscal = "";
                int Id_Terr = 0;
                string Serie = "";
                int Id_Cte = 0;
                string Cte_Nombre = "";
                int Pag_Numero = 0;
                string Pag_Cheque = "";
                double Pag_Importe = 0;
                string Doc_Estatus = "";
                double Doc_Importe = 0;
                double Doc_Pagado = 0;
                int Cdi = 0;
                GridItem gi = e.Item;

                DataRow[] Ar_dr = null;
                if (((RadTextBox)gi.FindControl("rgSerie")).Text == "" ||
                    ((RadTextBox)gi.FindControl("rgReferencia")).Text == "" ||
                    ((Telerik.Web.UI.RadNumericTextBox)gi.FindControl("rgCdi")).Text == "" ||
                    !((RadDatePicker)gi.FindControl("rdFecha")).SelectedDate.HasValue ||
                    ((RadNumericTextBox)gi.FindControl("rgTerr")).Text == "" ||
                    ((RadNumericTextBox)gi.FindControl("rgCte")).Text == "" ||
                    ((RadTextBox)gi.FindControl("rgCteNombre")).Text == "" ||
                    ((RadComboBox)gi.FindControl("rgcmbNum")).Text == "" ||
                    ((RadNumericTextBox)gi.FindControl("rgtxtImporte")).Text == ""
                    )
                {
                    e.Canceled = true;
                    return;
                }

                Serie = ((RadTextBox)gi.FindControl("rgSerie")).Text;
                Doc_Fecha = Convert.ToDateTime(((RadDatePicker)gi.FindControl("rdFecha")).SelectedDate);
                Doc_Referencia = ((RadTextBox)gi.FindControl("rgReferencia")).Text;
                Fac_FolioFiscal = ((RadTextBox)gi.FindControl("rgFolioFiscal")).Text;
                Pag_Movimiento = Convert.ToInt32(((RadComboBox)gi.FindControl("rgcmbMov")).SelectedValue);
                Pag_MovimientoStr = ((RadComboBox)gi.FindControl("rgcmbMov")).Text;
                Id_Terr = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("rgTerr")).Text);
                Id_Cte = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("rgCte")).Text);
                Cte_Nombre = ((RadTextBox)gi.FindControl("rgCteNombre")).Text;
                Pag_Numero = Convert.ToInt32(((RadComboBox)gi.FindControl("rgcmbNum")).SelectedItem.Text);
                Pag_Cheque = ((RadTextBox)gi.FindControl("rgCheque")).Text;
                Pag_Importe = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("rgtxtImporte")).Text);
                Doc_Estatus = ((RadTextBox)gi.FindControl("rgEstatus")).Text;
                Doc_Importe = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("rgImporte")).Value.HasValue ? ((RadNumericTextBox)gi.FindControl("rgImporte")).Value.Value : -1);
                Doc_Pagado = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("rgPagado")).Value.HasValue ? ((RadNumericTextBox)gi.FindControl("rgPagado")).Value.Value : -1);
                Cdi = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("rgCdi")).Text);
                RgDId = Convert.ToInt32(((Label)gi.FindControl("lblDetlId2")).Text);

                if (RgDet.Items.Count > 0)
                {
                    int linea = gi.ItemIndex;
                    double totalSuma = 0;
                    for (int x = 0; x < dtDet.Rows.Count; x++)
                    {
                        if (Doc_Referencia != dtDet.Rows[x]["Doc_Referencia"].ToString() && Serie != dtDet.Rows[x]["Serie"].ToString())
                        {
                            totalSuma += (Convert.ToDouble(dtDet.Rows[x]["Pag_Importe"]));
                        }
                    }
                    /*if (Math.Round(Convert.ToDouble(txtImporte.Text), 2) < (totalSuma + Pag_Importe))
                    {
                        Alerta("El total del detalle excede al importe de las fichas");
                        e.Canceled = true;
                        return;
                    }*/
                }

                //DataRow[] Ar_Dr = null;                
                //double sum = 0;
                //for (int x = 0; x < dtGral.Rows.Count; x++)
                //{
                //    if (Pag_Numero == x)
                //    {
                //        sum = 0;
                //        Ar_Dr = dtDet.Select("Pag_Numero='" + Pag_Numero + "'");
                //        for (int y = 0; y < Ar_Dr.Length; y++)
                //        {
                //            sum += Convert.ToDouble(Ar_Dr[y]["Pag_Importe"]);
                //        }

                //        if (Convert.ToDouble(txtImporte.Text) < (sum))
                //        {
                //            Alerta("El total del detalle excede al importe de las fichas");
                //            e.Canceled = true;
                //            return;
                //        }
                //    }
                //}

                Ar_dr = dtDet.Select("RgDId='" + RgDId + "'");
                if (Ar_dr.Length > 0)
                {
                    Ar_dr[0].BeginEdit();
                    Ar_dr[0]["Serie"] = Serie;
                    Ar_dr[0]["Doc_Fecha"] = Doc_Fecha;
                    Ar_dr[0]["Doc_Referencia"] = Doc_Referencia;
                    Ar_dr[0]["Pag_Movimiento"] = Pag_Movimiento;
                    Ar_dr[0]["Pag_MovimientoStr"] = Pag_MovimientoStr;
                    Ar_dr[0]["Pag_MovimientoStr"] = Fac_FolioFiscal;
                    Ar_dr[0]["Id_Terr"] = Id_Terr;
                    Ar_dr[0]["Id_Cte"] = Id_Cte;
                    Ar_dr[0]["Cte_Nombre"] = Cte_Nombre;
                    Ar_dr[0]["Pag_Numero"] = Pag_Numero;
                    Ar_dr[0]["Pag_Cheque"] = Pag_Cheque;
                    Ar_dr[0]["Pag_Importe"] = Pag_Importe;
                    Ar_dr[0]["Doc_Estatus"] = Doc_Estatus;
                    Ar_dr[0]["Doc_Importe"] = Doc_Importe;
                    Ar_dr[0]["Doc_Pagado"] = Doc_Pagado;
                    Ar_dr[0]["Cdi"] = Cdi;
                    Ar_dr[0].AcceptChanges();
                }
                CalcularTotalDetalle();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void deshabilitarcontroles(System.Web.UI.ControlCollection controles_contenidos)
        {
            for (int x = 0; x < controles_contenidos.Count; x++)
            {
                string Type = controles_contenidos[x].GetType().FullName;

                if (Type.Contains("RadMultiPage") || Type.Contains("RadPageView") || Type.Contains("Panel"))
                {
                    deshabilitarcontroles(controles_contenidos[x].Controls);
                }

                switch (Type.Replace("Telerik.Web.UI.", ""))
                {
                    case "RadNumericTextBox":
                        (controles_contenidos[x] as RadNumericTextBox).Enabled = false;
                        break;
                    case "RadTextBox":
                        (controles_contenidos[x] as RadTextBox).Enabled = false;
                        break;
                    case "RadComboBox":
                        (controles_contenidos[x] as RadComboBox).Enabled = false;
                        break;
                    case "RadDatePicker":
                        (controles_contenidos[x] as RadDatePicker).Enabled = false;
                        break;
                }
                if (Type.Contains("CheckBox"))
                {
                    (controles_contenidos[x] as CheckBox).Enabled = false;
                }

                if (Type.Contains("ImageButton"))
                {
                    (controles_contenidos[x] as ImageButton).Enabled = false;
                }
            }
        }

        /*JMM: Se agregan desde el header item template
   /*Nuevo*/
        private void Consulta()
        {
            try
            {

                foreach (GridHeaderItem headerItem in this.RgDet.MasterTableView.GetItems(GridItemType.Header))
                {
                    Sesion sesion = new Sesion();
                    sesion = (Sesion)Session["Sesion" + Session.SessionID];
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


                    if (rgtxtReferencia.Text != "")
                    {
                        try
                        {
                            int.Parse(rgtxtReferencia.Text);
                        }
                        catch (Exception ex)
                        {
                            rgtxtReferencia.Text = "";
                            rgtxtFolioFiscal.Text = "";
                            rgtxtReferencia.Focus();
                            AlertaFocus("Referencia inválida", rgtxtReferencia.ClientID);


                        }
                    }

                    Label fichaNum;
                    rgcmbNum.Items.Clear();



                    if (!rgCte.AutoPostBack && rgCdi.Text != sesion.Id_Cd_Ver.ToString())
                    {
                        return;
                    }

                    rgTerr.Text = "";
                    rdFecha.DbSelectedDate = null;
                    //rgCte.Text = "";
                    rgCteNombre.Text = "";
                    rgtxtImporte.Text = "";
                    rgCheque.Text = "";

                    if (rgtxtReferencia.Text == "" && rgtxtFolioFiscal.Text == "")
                    {
                        rgtxtFolioFiscal.Text = "";
                        rgTerr.Text = "";
                        rdFecha.DbSelectedDate = null;
                        rgCte.Text = "";
                        rgCteNombre.Text = "";
                        rgtxtImporte.Text = "";
                        rgCheque.Text = "";
                        //rgCdi.Text = "";
                        rgtxtReferencia.Focus();
                        return;
                    }

                    if (rgSerie.Text == "")
                    {
                        rgSerie.Focus();
                        return;
                    }

                    /*                    if (rgtxtReferencia.Text == "")
                                        {
                                            rgtxtReferencia.Focus();
                                            return;
                                        }*/

                    if (rgCte.Text == "" && Convert.ToInt32(rgCdi.Value) != sesion.Id_Cd_Ver)
                    {
                        rgCte.Focus();
                        return;
                    }

                    rgtxtReferencia.Text = rgtxtReferencia.Text.ToUpper();

                    string Referencia = rgtxtReferencia.Text;
                    int Movimiento = Convert.ToInt32(rgcmbMov.SelectedValue);
                    DataRow[] dr_ar = dtDet.Select("Doc_Referencia='" + Referencia + "' and Serie='" + rgSerie.Text + "'");
                    if (dr_ar.Length == 0)
                    {
                        LimpiarDetalle(rgTerr, rdFecha, rgTerr, rgCteNombre, rgEstatus, rgtxtImporte, rgPagado, rgtxtFolioFiscal);
                        if (Movimiento == 1)
                        {
                            Factura ficha = new Factura();
                            ficha.Id_Emp = sesion.Id_Emp;
                            ficha.Id_Cd = Convert.ToInt32(rgCdi.Value);
                            ficha.Serie = rgSerie.Text;
                            ficha.Id_FacSerie = Referencia;
                            ficha.Fac_FolioFiscal = rgtxtFolioFiscal.Text;
                            CN_CapPago cn_cappago = new CN_CapPago();
                            int verificador = 0;
                            cn_cappago.ConsultaPagoFicha(ref ficha, Emp_CnxCob, ref verificador);
                            if (verificador == 1)
                            {
                                if (ficha.Id_Cte.ToString() == rgCte.Text || ficha.Id_Cd == sesion.Id_Cd_Ver)
                                {
                                    rgTerr.Text = ficha.Id_Ter.ToString();
                                    rdFecha.SelectedDate = ficha.Fac_Fecha;
                                    rgCte.Text = ficha.Id_Cte.ToString();
                                    rgCteNombre.Text = ficha.Cte_NomComercial;
                                    rgEstatus.Text = ficha.Fac_Estatus;
                                    rgImporte.Text = ficha.Fac_Saldo.ToString();
                                    rgPagado.Text = ficha.Fac_Pagado.ToString();
                                    rgCdi.Text = ficha.Id_Cd.ToString();
                                    rgtxtImporte.Text = (ficha.Fac_Saldo - ficha.Fac_Pagado).ToString();
                                    rgtxtFolioFiscal.Text = ficha.Fac_FolioFiscal;
                                    rgtxtReferencia.Text = Convert.ToString(ficha.Id_Fac);
                                }
                                else
                                {
                                    AlertaFocus("El documento no pertenece al cliente capturado", rgCte.ClientID);
                                    rgCte.Text = "";
                                    return;
                                }
                                if (rgCte.Text == "")
                                {
                                    rgCte.Focus();
                                }
                            }
                            else
                            {
                                Alerta("Movimiento no existe; posiblemente sea un movimiento externo, revise la serie");
                                rgtxtReferencia.Text = "";
                                rgtxtFolioFiscal.Text = "";
                            }
                        }
                        else if (Movimiento == 2)
                        {
                            NotaCargo ficha = new NotaCargo();
                            ficha.Id_Emp = sesion.Id_Emp;
                            ficha.Id_Cd = Convert.ToInt32(rgCdi.Value);
                            ficha.Serie = rgSerie.Text;
                            ficha.Id_Nca = Convert.ToInt32(Referencia);
                            CN_CapPago cn_cappago = new CN_CapPago();

                            int verificador = 0;
                            cn_cappago.ConsultaPagoNotaFicha(ref ficha, sesion.Emp_Cnx, ref verificador);
                            if (verificador == 1)
                            {
                                rgTerr.Text = ficha.Id_Ter.ToString();
                                rdFecha.SelectedDate = ficha.Nca_Fecha;
                                rgCte.Text = ficha.Id_Cte.ToString();
                                rgCteNombre.Text = ficha.Cte_NomComercial;
                                rgEstatus.Text = ficha.Nca_Estatus;
                                rgImporte.Text = ficha.Importe.ToString();
                                rgPagado.Text = ficha.Nca_Pagado.ToString();
                                rgCdi.Text = ficha.Id_Cd.ToString();
                                rgtxtImporte.Text = ficha.Importe.ToString();
                                rgtxtFolioFiscal.Text = ficha.Nca_FolioFiscal;
                                rgtxtReferencia.Text = ficha.Id_NcaSerie;
                            }
                            else
                            {
                                Alerta("Movimiento no existe; posiblemente sea un movimiento externo");
                                return;
                            }
                        }
                        if (rgEstatus.Text == "B")
                        {
                            Alerta("Movimiento se encuentra cancelado; imposible aplicarle un pago");
                            LimpiarDetalle(rgTerr, rdFecha, rgCte, rgCteNombre, rgEstatus, rgtxtImporte, rgPagado, rgtxtFolioFiscal);
                            rgtxtReferencia.Focus();
                            return;
                        }
                        else if (rgEstatus.Text == "C")
                        {
                            Alerta("Movimiento se encuentra en status capturado; imposible aplicarle un pago");
                            LimpiarDetalle(rgTerr, rdFecha, rgCte, rgCteNombre, rgEstatus, rgtxtImporte, rgPagado, rgtxtFolioFiscal);
                            rgtxtReferencia.Focus();
                            return;
                        }
                        if (rgImporte.Text != "" && rgPagado.Text != "")
                        {
                            if (Math.Round(Convert.ToDouble(rgImporte.Text) - Convert.ToDouble(rgPagado.Text), 2) <= 0)
                            {
                                Alerta("Movimiento no tiene saldo positivo; imposible aplicarle un pago");
                                LimpiarDetalle(rgTerr, rdFecha, rgCte, rgCteNombre, rgEstatus, rgtxtImporte, rgPagado, rgtxtFolioFiscal);
                                rgtxtReferencia.Focus();
                                return;
                            }
                        }
                        (rgtxtReferencia.Parent.FindControl("rgCheque") as RadTextBox).Focus();
                    }
                    else
                    {
                        Alerta("Ya existe el registro");
                        rgtxtReferencia.Text = "";
                        rgtxtReferencia.Focus();
                        return;
                    }


                }



            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void Agregar()
        {
            try
            {
                foreach (GridHeaderItem headerItem in this.RgDet.MasterTableView.GetItems(GridItemType.Header))
                {
                    RadNumericTextBox rgCte = (RadNumericTextBox)headerItem["Id_Cte"].Controls[1];
                    RadTextBox rgReferencia = ((RadTextBox)rgCte.Parent.FindControl("rgRefEnc"));
                    RadNumericTextBox rgImporte = ((RadNumericTextBox)rgCte.Parent.FindControl("rgImporte"));
                    int RgDId = 0;
                    int Pag_Movimiento = 0;
                    string Pag_MovimientoStr = "";
                    DateTime Doc_Fecha = default(DateTime);
                    string Doc_Referencia = "";
                    string Fac_FolioFiscal = "";
                    int Id_Terr = 0;
                    string Serie = "";
                    int Id_Cte = 0;
                    string Cte_Nombre = "";
                    int Pag_Numero = 0;
                    string Pag_Cheque = "";
                    double Pag_Importe = 0;
                    string Doc_Estatus = "";
                    double Doc_Importe = 0;
                    double Doc_Pagado = 0;
                    int Cdi = 0;

                    if (((RadTextBox)rgCte.Parent.FindControl("rgSerie")).Text == "" ||
                        ((RadTextBox)rgCte.Parent.FindControl("rgRefEnc")).Text == "" ||
                        !((Telerik.Web.UI.RadDatePicker)rgCte.Parent.FindControl("rdFecha")).SelectedDate.HasValue ||
                        ((Telerik.Web.UI.RadNumericTextBox)rgCte.Parent.FindControl("rgTerr")).Text == "" ||
                        ((Telerik.Web.UI.RadNumericTextBox)rgCte.Parent.FindControl("rgCdi")).Text == "" ||
                        ((Telerik.Web.UI.RadNumericTextBox)rgCte.Parent.FindControl("rgCte")).Text == "" ||
                        ((Telerik.Web.UI.RadTextBox)rgCte.Parent.FindControl("rgCteNombre")).Text == "" ||
                        ((Telerik.Web.UI.RadComboBox)rgCte.Parent.FindControl("rgcmbNum")).Text == "" ||
                        ((Telerik.Web.UI.RadNumericTextBox)rgCte.Parent.FindControl("rgtxtImporte")).Text == ""
                        )
                    {
                        Alerta("Todos los campos son requeridos");

                        return;
                    }
                    if (((RadNumericTextBox)rgCte.Parent.FindControl("rgtxtImporte")).Value <= 0)
                    {
                        Alerta("El importe debe ser mayor a cero");
                        return;
                    }


                    Serie = ((RadTextBox)rgCte.Parent.FindControl("rgSerie")).Text;
                    Doc_Fecha = Convert.ToDateTime(((RadDatePicker)rgCte.Parent.FindControl("rdFecha")).SelectedDate);
                    Doc_Referencia = ((RadTextBox)rgCte.Parent.FindControl("rgRefEnc")).Text;
                    Fac_FolioFiscal = ((RadTextBox)rgCte.Parent.FindControl("rgFolioFiscal")).Text;
                    Pag_Movimiento = Convert.ToInt32(((RadComboBox)rgCte.Parent.FindControl("rgcmbMov")).SelectedValue);
                    Pag_MovimientoStr = ((RadComboBox)rgCte.Parent.FindControl("rgcmbMov")).Text;
                    Id_Terr = Convert.ToInt32(((RadNumericTextBox)rgCte.Parent.FindControl("rgTerr")).Text);
                    Id_Cte = Convert.ToInt32(rgCte.Text);
                    Cte_Nombre = ((RadTextBox)rgCte.Parent.FindControl("rgCteNombre")).Text;
                    Pag_Numero = Convert.ToInt32(((RadComboBox)rgCte.Parent.FindControl("rgcmbNum")).SelectedItem.Text);
                    Pag_Cheque = ((RadTextBox)rgCte.Parent.FindControl("rgCheque")).Text;
                    Pag_Importe = Convert.ToDouble(((RadNumericTextBox)rgCte.Parent.FindControl("rgtxtImporte")).Text);
                    Doc_Estatus = ((RadTextBox)rgCte.Parent.FindControl("rgEstatus")).Text;
                    Doc_Importe = Convert.ToDouble(((RadNumericTextBox)rgCte.Parent.FindControl("rgImporte")).Value.HasValue ? ((RadNumericTextBox)rgCte.Parent.FindControl("rgImporte")).Value.Value : -1);
                    Doc_Pagado = Convert.ToDouble(((RadNumericTextBox)rgCte.Parent.FindControl("rgPagado")).Value.HasValue ? ((RadNumericTextBox)rgCte.Parent.FindControl("rgPagado")).Value.Value : -1);
                    Cdi = Convert.ToInt32(((RadNumericTextBox)rgCte.Parent.FindControl("rgCdi")).Text);

                    /*
                    if (Math.Round(Convert.ToDouble(txtImporte.Text), 2) < (Math.Round(Convert.ToDouble(txtTotal.Text) + Pag_Importe, 2)))
                    {
                        AlertaFocus("El total del detalle excede al importe de las fichas", rgImporte.ClientID);
                        return;
                    }
                    */
                    RgDId = dtDet.Rows.Count + 1;
                    Sesion Sesion = new Sesion();
                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                    dtDet.Rows.Add(new object[] {
                    RgDId,
                    Pag_Movimiento,
                    Pag_MovimientoStr,
                    Doc_Referencia,
                    Fac_FolioFiscal,
                    Id_Terr,
                    Serie,
                    Doc_Fecha,
                    Id_Cte,
                    Cte_Nombre,
                    Pag_Numero,
                    Pag_Cheque,
                    Pag_Importe,
                    Doc_Estatus,
                    Doc_Importe,
                    Doc_Pagado,
                    Cdi
                });
                    if (Doc_Importe != -1 && Doc_Pagado != -1)
                    {
                        double pagadoDoc = CalcularSaldoDocumento(Doc_Referencia, Doc_Importe - Doc_Pagado);
                        if (pagadoDoc > 0)
                            AlertaFocus("Saldo del documento <b>" + pagadoDoc.ToString("$ #,##0.00") + "</b>", rgReferencia.ClientID);
                    }
                    CalcularTotalDetalle();
                    RgDet.Rebind();
                    rgReferencia.Focus();

                }





            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void Eliminar(object sender)
        {
            try
            {
                ImageButton imgbtn = (ImageButton)sender;
                int RgDId = 0;
                DataRow[] Ar_dr;
                DataRow dr;
                RgDId = Convert.ToInt32(((Label)imgbtn.Parent.FindControl("lblDetlId1")).Text);
                Ar_dr = dtDet.Select("RgDId='" + RgDId + "'");
                if (Ar_dr.Length > 0)
                {
                    Ar_dr[0].Delete();
                    dtDet.AcceptChanges();
                }
                for (int x = 0; x < dtDet.Rows.Count; x++)
                {
                    dr = dtDet.Rows[x];
                    dr.BeginEdit();
                    dr["RgDId"] = x + 1;
                    dr.AcceptChanges();
                }
                CalcularTotalDetalle();
                RgDet.Rebind();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void ConsultaSerie(object sender)
        {
            try
            {

                RadComboBox rgcmbMov = (RadComboBox)sender;
                RadTextBox rgSerie = (RadTextBox)rgcmbMov.Parent.FindControl("rgSerie");
                RadTextBox rgReferencia = (RadTextBox)rgcmbMov.Parent.FindControl("rgRefEnc");
                RadNumericTextBox rgCdi = (RadNumericTextBox)rgcmbMov.Parent.FindControl("rgCdi");
                RadNumericTextBox rgTerr = rgReferencia.Parent.FindControl("rgTerr") as RadNumericTextBox;
                RadDatePicker rdFecha = rgReferencia.Parent.FindControl("rdFecha") as RadDatePicker;
                RadNumericTextBox rgCte = rgReferencia.Parent.FindControl("rgCte") as RadNumericTextBox;
                RadTextBox rgCteNombre = rgReferencia.Parent.FindControl("rgCteNombre") as RadTextBox;
                string folio = null;
                CN_CatCliente cn_cte = new CN_CatCliente();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                CN_CapPago cn_cappago = new CN_CapPago();

                DbCentro centro = new DbCentro();
                cn_cte.ConsultaFolioFactEle(sesion, int.Parse(rgcmbMov.SelectedValue), ref folio);

                rgReferencia.AutoPostBack = true;
                rgSerie.Text = folio;
                rgCdi.Value = sesion.Id_Cd_Ver;
                rgReferencia.Focus();





            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void ConsultaSerie2(object sender)
        {
            try
            {
                RadTextBox rgSerie = (RadTextBox)sender;
                RadComboBox rgcmbMov = (RadComboBox)rgSerie.Parent.FindControl("rgcmbMov");
                RadTextBox rgReferencia = (RadTextBox)rgcmbMov.Parent.FindControl("rgRefEnc");
                RadNumericTextBox rgCdi = (RadNumericTextBox)rgcmbMov.Parent.FindControl("rgCdi");
                RadNumericTextBox rgTerr = rgReferencia.Parent.FindControl("rgTerr") as RadNumericTextBox;
                RadDatePicker rdFecha = rgReferencia.Parent.FindControl("rdFecha") as RadDatePicker;
                RadNumericTextBox rgCte = rgReferencia.Parent.FindControl("rgCte") as RadNumericTextBox;
                RadTextBox rgCteNombre = rgReferencia.Parent.FindControl("rgCteNombre") as RadTextBox;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                int Tipo_CDC = 0;
                int verificador = 0;
                DbCentro centro = new DbCentro();
                CN_CapPago cn_cappago = new CN_CapPago();
                new CN_CatCliente().ConsultaTipoCDC(sesion.Id_Cd_Ver, ref Tipo_CDC, sesion.Emp_Cnx);
                cn_cappago.ConsultarCentro(sesion.Id_Emp, rgSerie.Text, ref centro, Emp_CnxCob, Tipo_CDC);
                verificador = centro.Id_Cd;
                if (verificador == 0)
                {
                    rgReferencia.AutoPostBack = false;
                    rgTerr.ReadOnly = false;
                    rdFecha.DateInput.ReadOnly = false;
                    rdFecha.DatePopupButton.Enabled = true;
                    rgCte.ReadOnly = false;
                    rgCte.AutoPostBack = false;
                    rgCteNombre.ReadOnly = false;
                    rgCdi.ReadOnly = false;
                }
                else
                {
                    if (Request.QueryString["Ext"] != null && centro.Db_CerradoExtemporaneo != null)
                    {
                        rgSerie.Text = "";
                        rgReferencia.Text = "";
                        Alerta("La sucursal externa ya realizo el cierre extemporáneo de pagos");
                        rgSerie.Focus();
                        return;
                    }
                    else
                    {
                        rgReferencia.AutoPostBack = true;
                        rgTerr.ReadOnly = true;
                        rdFecha.DateInput.ReadOnly = true;
                        rdFecha.DatePopupButton.Enabled = false;
                        rgCteNombre.ReadOnly = true;
                        rgCdi.ReadOnly = true;
                        rgCdi.Text = verificador.ToString(); ;

                        if (rgCdi.Text == sesion.Id_Cd_Ver.ToString())
                        {
                            rgCte.ReadOnly = true;

                        }
                        else
                        {
                            rgCte.AutoPostBack = true;
                        }
                    }
                }

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

                foreach (GridDataItem item in RgDet.MasterTableView.Items)
                {
                    ImageButton ImgEliminar = (ImageButton)item["Accion"].Controls[1];
                    ImgEliminar.Visible = false;

                }

                foreach (GridHeaderItem headerItem in this.RgDet.MasterTableView.GetItems(GridItemType.Header))
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
        private void CargarFichas()
        {
            try
            {
                foreach (GridHeaderItem headerItem in this.RgDet.MasterTableView.GetItems(GridItemType.Header))
                {
                    DataTable dt = (DataTable)Session["dtGralPagos" + Session.SessionID + Request.Params["IdPagina"].ToString()];
                    RadComboBox rgcmbNum = (RadComboBox)headerItem["Pag_Numero"].Controls[1];
                    string fichaNum;
                    rgcmbNum.Items.Clear();
                    //for (int x = 0; x < RgGral.Items.Count; x++)
                    //{
                    //    fichaNum = RgGral.Items[x]["Pag_ficha"].FindControl("lblFicha") as Label;
                    //    rgcmbNum.Items.Add(new RadComboBoxItem(fichaNum.Text, fichaNum.Text));
                    //}

                    foreach (DataRow dtRow in dt.Rows)
                    {
                        fichaNum = dtRow["Pag_ficha"].ToString();
                        rgcmbNum.Items.Add(new RadComboBoxItem(fichaNum, fichaNum));

                    }

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        private void CargarFormaPago()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Id_Emp, Sesion.Id_Cd, Sesion.Emp_Cnx, "spCatPagoFormaPago_Combo", ref cboMetodoPago);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void cboMetodoPago_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            List<PagoDet> list_pagos = new List<PagoDet>();
            CN_CatCliente clsCatClientes = new CN_CatCliente();
            CN_CapPago cn_cappago = new CN_CapPago();

            string MetodoPago = "";
            int ID_Emp = Convert.ToInt32(Sesion.Id_Emp);
            int Id_Cd = Convert.ToInt32(Sesion.Id_Cd_Ver);
            int Id_Pag = Convert.ToInt32(HF_ID.Value);

            try
            {
                ErrorManager();
                if (this.cboMetodoPago.SelectedValue != "-1")
                {
                    HF_FPI.Value = cboMetodoPago.SelectedValue;
                    txtImporte.Text = "0.00";

                    MetodosPagoTimbrar(ID_Emp, Id_Cd, Id_Pag, Sesion.Emp_Cnx, true, ref MetodoPago);

                    RgDet.Rebind();

                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }

        #region imprimir

        private void descargarXML(int Id_Cte, int Id_PagDet)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            PagoDetComplemento pagoDetComplemento = new PagoDetComplemento();
            object pagoPDF = null;
            pagoDetComplemento.Id_Emp = sesion.Id_Emp;
            pagoDetComplemento.Id_Cd = sesion.Id_Cd_Ver;
            pagoDetComplemento.Id_Pag = Convert.ToInt32(HF_ID.Value);
            pagoDetComplemento.Id_Cte = Id_Cte;
            pagoDetComplemento.Id_PagDet = Id_PagDet;
            pagoDetComplemento.Id_PagComp = -1;
            new CN_CapPago().ConsultarPago(ref pagoDetComplemento, ref pagoPDF, sesion.Emp_Cnx);

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
                    sw = new System.IO.StreamWriter(ruta, false, Encoding.UTF8);
                    sw.WriteLine(pagoDetComplemento.Pago_Xml.ToString());
                    sw.Close();
                    File.Move(ruta, Server.MapPath("Reportes") + "\\archivoXml" + sesion.Id_U.ToString() + "Pago" + pagoDetComplemento.Id_PagComp.ToString() + ".xml");

                    if (File.Exists(Server.MapPath("Reportes") + "\\archivoXmlCN" + sesion.Id_U.ToString() + "Pago" + pagoDetComplemento.Id_PagComp.ToString() + ".xml"))
                    {
                        File.Delete(Server.MapPath("Reportes") + "\\archivoXmlCN" + sesion.Id_U.ToString() + "Pago" + pagoDetComplemento.Id_PagComp.ToString() + ".xml");
                    }

                    sw = new System.IO.StreamWriter(rutaCN, false, Encoding.UTF8);
                    sw.WriteLine(pagoDetComplemento.Pago_Xml.ToString());
                    sw.Close();

                    File.Move(rutaCN, Server.MapPath("Reportes") + "\\archivoXmlCN" + sesion.Id_U.ToString() + "Pago" + pagoDetComplemento.Id_PagComp.ToString() + ".xml");

                    RAM1.ResponseScripts.Add(string.Concat(@"abrirArchivoCN('Reportes\\archivoXml" + sesion.Id_U.ToString() + "Pago", pagoDetComplemento.Id_PagComp.ToString(), ".xml'", ",", @"'Reportes\\archivoXmlCN" + sesion.Id_U.ToString() + "Pago", pagoDetComplemento.Id_PagComp.ToString(), ".xml')"));

                }

            }
        }

        private void descargarXMLCancelacion(int Id_Cte, int Id_PagDet)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            PagoDetComplemento pagoDetComplemento = new PagoDetComplemento();
            object pagoPDF = null;
            pagoDetComplemento.Id_Emp = sesion.Id_Emp;
            pagoDetComplemento.Id_Cd = sesion.Id_Cd_Ver;
            pagoDetComplemento.Id_Pag = Convert.ToInt32(HF_ID.Value);
            pagoDetComplemento.Id_Cte = Id_Cte;
            pagoDetComplemento.Id_PagDet = Id_PagDet;
            pagoDetComplemento.Id_PagComp = -1;
            new CN_CapPago().ConsultarPago(ref pagoDetComplemento, ref pagoPDF, sesion.Emp_Cnx);

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
                    sw = new System.IO.StreamWriter(ruta, false, Encoding.UTF8);
                    sw.WriteLine(pagoDetComplemento.Cancelacion_XML.ToString());
                    sw.Close();
                    File.Move(ruta, Server.MapPath("Reportes") + "\\archivoXml" + sesion.Id_U.ToString() + "CancelacionPago" + pagoDetComplemento.Id_PagComp.ToString() + ".xml");

                    RAM1.ResponseScripts.Add(string.Concat(@"abrirArchivo('Reportes\\archivoXml" + sesion.Id_U.ToString() + "CancelacionPago", pagoDetComplemento.Id_PagComp.ToString(), ".xml')"));//, ",", @"'Reportes\\archivoXmlCN" + sesion.Id_U.ToString() + "Fac", pagoDetComplemento.Id_PagComp.ToString(), ".xml')"));

                }

            }
        }

        private void descargarPDF(int Id_Cte, int Id_PagDet)
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
                pagoDetComplemento.Id_PagComp = -1;
                new CN_CapPago().ConsultarPago(ref pagoDetComplemento, ref pagoPDF, sesion.Emp_Cnx);

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

        private void cancelarComplemento(int id_PagDet, bool otraBD)
        {

            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            try
            {

                PagoDetComplemento pagoDetComplemento = new PagoDetComplemento();
                object pagoPDF = null;
                pagoDetComplemento.Id_Emp = sesion.Id_Emp;
                pagoDetComplemento.Id_Cd = sesion.Id_Cd_Ver;
                pagoDetComplemento.Id_Pag = Convert.ToInt32(HF_ID.Value);
                pagoDetComplemento.Id_PagDet = id_PagDet;
                pagoDetComplemento.Id_PagComp = -1;
                new CN_CapPago().ConsultarPago(ref pagoDetComplemento, ref pagoPDF, sesion.Emp_Cnx);

                if (pagoDetComplemento == null)
                {
                    Alerta("El pago no ha sido timbrado");
                }
                else
                {
                    CentroDistribucion Cd = new CentroDistribucion();
                    new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref Cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);
                    string folio = pagoDetComplemento.Pago_FolioFiscal;
                    string rfc = Cd.Cd_Rfc.Replace(" ", string.Empty);
                    WS_CFDICancelacion.Service1 cancelacion = new WS_CFDICancelacion.Service1();
                    cancelacion.Url = ConfigurationManager.AppSettings["WS_Cancelacion"].ToString();
                    string sianCancelacionPagoResult = cancelacion.CancelacionWS("" + rfc + "," + folio + "");
                    XmlDocument xmlSAT = new XmlDocument();
                    xmlSAT.LoadXml(sianCancelacionPagoResult);
                    System.Data.SqlTypes.SqlXml sqlXml = new System.Data.SqlTypes.SqlXml();
                    sqlXml = new System.Data.SqlTypes.SqlXml(new XmlTextReader(xmlSAT.OuterXml, XmlNodeType.Document, null));
                    List<PagoDetComplemento> complementos = new List<PagoDetComplemento>();
                    new CN_CapPago().ConsultaListaComplementosPago(pagoDetComplemento.Id_Emp,
                                                                    pagoDetComplemento.Id_Cd,
                                                                    pagoDetComplemento.Id_Pag,
                                                                    pagoDetComplemento.Id_PagComp,
                                                                    sesion.Emp_Cnx,
                                                                    ref complementos);
                    foreach (var cancelarComplemento in complementos)
                    {
                        if (cancelarComplemento.Id_PagDet == id_PagDet || cancelarComplemento.Pago_FolioFiscal == folio)
                        {
                            int verificadorPago = 0;

                            //new CN_CapPago().BajaComplementoPago(cancelarComplemento, otraBD, sesion.Emp_Cnx, ref verificadorPago);
                            //if (verificadorPago < 0)
                            //{
                            //    Alerta("No se pudo actualizar la información en el timbre");

                            //} 

                            cancelarComplemento.Cancelacion_XML = sqlXml;
                            cancelarComplemento.FechaCancelacion = DateTime.Now;
                            new CN_CapPago().ModificarComplementoPago(cancelarComplemento, sesion.Emp_Cnx, false, ref verificadorPago);
                            if (verificadorPago < 0)
                            {
                                Alerta("No se pudo actualizar la información en el timbre");

                            }
                        }
                    }
                    RgDet.Rebind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ImprimirTimbres(Sesion sesion, List<Timbre> Timbres, int MetodoPago, int Id_Pag, ref string strAlerta, ref string strResponseScripts, bool isSegundoPlano)
        {
            try
            {

                CentroDistribucion Cd = new CentroDistribucion();
                new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref Cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);
                Pago pago = new Pago();
                List<Banco_Ficha> list_fichas = new List<Banco_Ficha>();
                List<PagoDet> list_pagos = new List<PagoDet>();
                List<PagoDetComplemento> List_DetComplemento = new List<PagoDetComplemento>();
                PagoDetTimbrado entPagoDetTimbrado = new PagoDetTimbrado();
                bool isDuplicado = false;
                pago.Id_Emp = sesion.Id_Emp;
                pago.Id_Cd = sesion.Id_Cd_Ver;
                pago.Id_Pag = Id_Pag;
                string correo = "";
                new CN_CapPago().ConsultaPago2(ref pago, ref list_fichas, ref list_pagos, sesion.Emp_Cnx, ref List_DetComplemento);
                foreach (var Timbre in Timbres)
                {
                    //continue;
                    string nombrePDF = "PAGO_";
                    StringBuilder XML_EnviarPrueba = new StringBuilder();
                    StringBuilder XML_Enviar = new StringBuilder();
                    StringBuilder XML_Conceptos = new StringBuilder();

                    object pagoPDF = null;
                    int pag_ID = 0;
                    int verificador = -1;
                    double importes = 0;
                    object sianFacturacionElectronicaResult = null;
                    DateTime fechaPago = DateTime.Now;
                    string NumOperacion = string.Empty;

                    string serie = string.Empty;
                    int folio = 0;
                    // validar si las facturas ya fueron timbradas
                    isDuplicado = false;
                    foreach (var itemFactura in Timbre.Facturas)
                    {
                        entPagoDetTimbrado = new PagoDetTimbrado();
                        entPagoDetTimbrado.Id_Emp = pago.Id_Emp;
                        entPagoDetTimbrado.Id_Cd = pago.Id_Cd;
                        entPagoDetTimbrado.Id_Pag = pago.Id_Pag;
                        entPagoDetTimbrado.Id_Cte = itemFactura.Id_Cte;
                        entPagoDetTimbrado.Id_PagDet = list_pagos.Where(at => at.Ref.Replace(" ", string.Empty) == itemFactura.Id_Fac.ToString()).Select(row => row.Id_PagDet).FirstOrDefault();
                        new CN_CapPago().ConsultaPagoDetTimbrado(sesion.Emp_Cnx, ref entPagoDetTimbrado);
                        if (!string.IsNullOrEmpty(entPagoDetTimbrado.Pago_FolioFiscal))
                        {
                            isDuplicado = true;
                            continue;
                        }
                    }
                    if (isDuplicado)
                    {
                        // El pago ya fue timbrado, no se puede volver a timbrar. continuar con el siguiente pago.
                        continue;
                    }

                    new CN_CapPago().ConsultaSerieYFolio(pago.Id_Emp, pago.Id_Cd, ref serie, ref folio, sesion.Emp_Cnx);
                    //folio++;
                    //nombrePDF = nombrePDF + pago.Id_Emp.ToString() + "_" + pago.Id_Cd.ToString() + "_" + folio.ToString() + ".pdf";
                    #region construirXML
                    try
                    {
                        PagoDetComplemento pagoDetComplemento = new PagoDetComplemento();
                        List<PagoDetComplemento> ListaDocumentos = new List<PagoDetComplemento>();
                        List<PagoDetComplemento> listaFoliosRelacionados = new List<PagoDetComplemento>();
                        foreach (var Factura in Timbre.Facturas)
                        {
                            object complementoPagoPDF = null;
                            PagoDet PagoDet = list_pagos.Where(at => at.Ref.Replace(" ", string.Empty) == Factura.Id_Fac.ToString()).Select(row => row).FirstOrDefault();
                            pagoDetComplemento.Id_Emp = pago.Id_Emp;
                            pagoDetComplemento.Id_Cd = pago.Id_Cd;
                            pagoDetComplemento.Id_Pag = pago.Id_Pag;
                            pagoDetComplemento.Id_Cte = Timbre.Cliente.Id_Cte.Value;
                            pagoDetComplemento.Id_PagDet = PagoDet.Id_PagDet;
                            pagoDetComplemento.RFC = Timbre.Cliente.Cte_FacRfc;
                            fechaPago = list_fichas.Where(at => at.Pag_Ficha == PagoDet.Pag_Numero).Select(row => row.Pag_Fecha).First();
                            NumOperacion = list_fichas.Where(at => at.Pag_Ficha == PagoDet.Pag_Numero).Select(row => Convert.ToString(row.Pag_NumOperacion)).First();
                            new CN_CapPago().ConsultaComplementoPago(ref pagoDetComplemento, ref pagoPDF, sesion.Emp_Cnx);

                            if (pagoDetComplemento == null)
                            {
                                pagoDetComplemento = new PagoDetComplemento();
                                pagoDetComplemento.Id_Emp = pago.Id_Emp;
                                pagoDetComplemento.Id_Cd = pago.Id_Cd;
                                pagoDetComplemento.Id_Pag = pago.Id_Pag;
                                pagoDetComplemento.Id_Cte = Timbre.Cliente.Id_Cte.Value;
                                pagoDetComplemento.Id_PagDet = PagoDet.Id_PagDet;
                                pagoDetComplemento.Pago_Serie = serie;
                                pagoDetComplemento.Id_PagComp = folio;
                                pagoDetComplemento.RFC = Timbre.Cliente.Cte_FacRfc;
                                new CN_CapPago().InsertarComplementoPago(pagoDetComplemento, sesion.Emp_Cnx, ref verificador);

                                folio = verificador;
                            }
                            else
                            {
                                folio = pagoDetComplemento.Id_PagComp;
                            }


                            pag_ID = list_pagos.First().Id_Pag;

                            //Forma de pago y uso de cfdi
                            if (MetodoPago > 0)
                            {
                                pagoDetComplemento.Cte_Fpago = MetodoPago;
                                // SI alguno de estos estatus se estan utilizando y no se realiza por funcion de subrogacio o intermediario se utilizara
                                // forma de pago 3
                                // forma pago 13 - subrogacion
                                // forma pago 31 - intermediario pago
                                if (pagoDetComplemento.Cte_Fpago == 13 || pagoDetComplemento.Cte_Fpago == 31)
                                    pagoDetComplemento.Cte_Fpago = 3;
                            }
                            else
                            {
                                pagoDetComplemento.Cte_Fpago = Convert.ToInt32(Timbre.Facturas.First().Fac_FPago);
                                if (pagoDetComplemento.Cte_Fpago == 99)
                                    pagoDetComplemento.Cte_Fpago = 3;
                            }

                            if (Timbre.Cliente.Cte_PagoCorreos != null && Timbre.Cliente.Cte_PagoCorreos != "")
                            {
                                correo = Timbre.Cliente.Cte_PagoCorreos;
                            }
                            else
                            {
                                correo = Timbre.Cliente.Cte_Email;
                            }

                            pagoDetComplemento.Cte_UsoCFDI = Timbre.Cliente.Cte_PagoUsoCFDI;


                            //Conuslta para saber cuantas veces se ha realizado una factura
                            new CN_CapPago().ConsultaDetalleComplementoPagoListaDocs(pagoDetComplemento, ref ListaDocumentos, ref complementoPagoPDF, sesion.Emp_Cnx);
                            PagoDetComplemento registroFolio = new PagoDetComplemento();
                            foreach (PagoDetComplemento registroFolioRelacionados in ListaDocumentos)
                            {
                                if (registroFolioRelacionados.Pago_FolioFiscal != null && registroFolioRelacionados.Pago_FolioFiscal.Length > 0)
                                {
                                    listaFoliosRelacionados.Select(x => x.Pago_FolioFiscal == registroFolioRelacionados.Pago_FolioFiscal).ToList();
                                    if (listaFoliosRelacionados.Count == 0)
                                    {
                                        registroFolio.Pago_FolioFiscal = registroFolioRelacionados.Pago_FolioFiscal;
                                        listaFoliosRelacionados.Add(registroFolio);
                                    }
                                }
                            }


                            var PagoFactura = list_pagos.FirstOrDefault(x => x.Ref.Replace(" ", string.Empty) == Factura.Id_Fac.ToString());

                            double totalFacturado = Factura.Fac_SubTotal.Value + Factura.Fac_ImporteIva.Value;

                            var Pagado = List_DetComplemento.FirstOrDefault(x => x.pag_referencia.Replace(" ", string.Empty) == Factura.Id_Fac.ToString());

                            string ivaTrasladado = (Factura.Fac_ImporteIva.Value / Factura.Fac_SubTotal.Value * 100).ToString("0.00");

                            double saldo = 0;
                            double saldoant = 0;



                            //AOG
                            //if (totalFacturado <= Pago.Importe)
                            //{
                            //    saldoant = totalFacturado;
                            //    saldo = 0;
                            //}
                            //else
                            //{
                            //    saldoant = totalFacturado - (totalFacturado - Pago.Importe);
                            //    saldo = saldoant - Pago.Importe;
                            //}
                            int numParcialidad = 1;
                            try
                            {
                                if (Pagado != null)
                                {
                                    if (totalFacturado <= Pagado.Total_Pagado_Timbrado)
                                    {
                                        saldoant = totalFacturado;
                                        saldo = 0;
                                    }
                                    else
                                    {
                                        saldoant = totalFacturado - Pagado.Total_Pagado_Timbrado;
                                        numParcialidad = Pagado.Parcialidad_Timbrada + 1;
                                    }

                                }
                                else
                                {
                                    saldoant = totalFacturado;
                                    saldo = 0;
                                }

                            }
                            catch (Exception ex)
                            {
                                saldoant = totalFacturado;
                                saldo = 0;
                            }
                            //saldo = saldoant - PagoFactura.Importe;


                            if (Convert.ToDouble(saldoant.ToString("N2")) < PagoFactura.Importe)
                            {
                                PagoFactura.Importe = saldoant;
                            }

                            saldo = saldoant - PagoFactura.Importe;
                            importes += PagoFactura.Importe;
                            XML_Conceptos.Append(" <Concepto");
                            XML_Conceptos.Append(" ImpSaldoInsoluto=\"" + ((saldo < 0) ? string.Format("{0:0.00}", decimal.Zero) : string.Format("{0:0.00}", saldo)) + "\"");
                            XML_Conceptos.Append(" ImpPagado=\"" + (string.Format("{0:0.00}", PagoFactura.Importe)) + "\"");
                            XML_Conceptos.Append(" ImpSaldoAnt=\"" + string.Format("{0:0.00}", saldoant) + "\"");
                            XML_Conceptos.Append(" NumParcialidad=\"" + numParcialidad.ToString() + "\"");
                            XML_Conceptos.Append(" TipoCambioDR=\"" + Timbre.TipoMoneda.Mon_TipCambio + "\"");
                            XML_Conceptos.Append(" MonedaDR=\"" + Timbre.TipoMoneda.Mon_codigo + "\"");
                            XML_Conceptos.Append(" FPago=\"" + PagoFactura.Ref + "\"");
                            //XML_Enviar.Append(" SPago=\"" +  serie  + "\""); ;
                            XML_Conceptos.Append(" SPago=\"" + PagoFactura.Pag_Serie + "\"");
                            //XML_Conceptos.Append(" SPago=\"" + "PRUEBA" + "\"");
                            XML_Conceptos.Append(" MetodoDePagoDR=\"" + Timbre.Cliente.Cte_MetodoPago + "\"");
                            XML_Conceptos.Append(" UUID=\"" + PagoFactura.Fac_FolioFiscal + "\"");
                            XML_Conceptos.Append(" ImpuestoDR=\"" + ivaTrasladado + "\" />");
                        }

                        nombrePDF = nombrePDF + pago.Id_Emp.ToString() + "_" + pago.Id_Cd.ToString() + "_" + folio.ToString() + ".pdf";


                        XML_Enviar.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                        XML_Enviar.Append("<Comprobante");
                        XML_Enviar.Append(" ComprobanteVersion=\"" + Timbre.Cliente.Cte_VersionCFDI + "\"");
                        //XML_Enviar.Append(" MetodoPago=\"" + "03" + "\"");
                        XML_Enviar.Append(" MetodoPago=\"" + pagoDetComplemento.Cte_Fpago.ToString().PadLeft(2, '0') + "\"");
                        XML_Enviar.Append(" CliNum=\"" + Timbre.Cliente.Id_Cte.ToString() + "\"");
                        XML_Enviar.Append(" Correo=\"" + correo + "\"");
                        XML_Enviar.Append(" Notas=\"" + "//////" + "\"");
                        XML_Enviar.Append(" tipoCambio=\"" + Timbre.TipoMoneda.Mon_TipCambio + "\"");
                        XML_Enviar.Append(" tipoMoneda=\"" + Timbre.TipoMoneda.Mon_Abrev + "\"");
                        XML_Enviar.Append(" tipoMovimiento=\"" + "PAGO" + "\"");
                        XML_Enviar.Append(" tipoDeComprobante=\"" + "PAGO" + "\"");
                        XML_Enviar.Append(" total=\"" + importes.ToString() + "\"");
                        XML_Enviar.Append(" fecha=\"" + string.Format("{0:s}", DateTime.Now) + "\"");
                        //XML_Enviar.Append(" fecha=\"" + string.Format("{0:s}", fechaPago) + "\"");
                        XML_Enviar.Append(" folio=\"" + folio + "\"");
                        //XML_Enviar.Append(" folio=\"" + pag_ID.ToString() + "\"");
                        //XML_Enviar.Append(" serie=\"" + serie + "\" >");
                        XML_Enviar.Append(" Id_Pag=\"" + pago.Id_Pag.ToString() + "\"");
                        XML_Enviar.Append(" serie=\"" + serie + "\" >");
                        //XML_Enviar.Append(" serie=\"" + "PRUEBA" + "\" >");


                        //Si la factura ya tenia anteriormente factura se relaciona.
                        if (listaFoliosRelacionados.Count > 0)
                        {
                            XML_Enviar.Append(" <CfdiRelacionados");
                            XML_Enviar.Append(" TipoRelacion=\"" + "04" + "\" >");
                            foreach (PagoDetComplemento Complemento in listaFoliosRelacionados)
                            {
                                XML_Enviar.Append(" <CfdiRelacionado");
                                XML_Enviar.Append(" UUID=\"" + Complemento.Pago_FolioFiscal + "\" />");
                            }
                            XML_Enviar.Append("</CfdiRelacionados>");
                        }


                        XML_Enviar.Append(" <Emisor");
                        XML_Enviar.Append(" numero=\"" + Cd.Id_Cd + "\"");
                        XML_Enviar.Append(" rfc=\"" + Cd.Cd_Rfc + "\" />");

                        XML_Enviar.Append(" <Receptor");
                        XML_Enviar.Append(" rfc=\"" + HttpUtility.HtmlEncode(Timbre.Facturas.First().Fac_CteRfc) + "\"");
                        XML_Enviar.Append(" nombre=\"" + HttpUtility.HtmlEncode(Timbre.Cliente.Cte_NomComercial) + "\"");
                        //XML_Enviar.Append(" UsoCFDI=\"" + Timbre.Cliente.Cte_UsoCFDI + "\" >");
                        XML_Enviar.Append(" UsoCFDI=\"" + pagoDetComplemento.Cte_UsoCFDI + "\"");
                        XML_Enviar.Append(" Regimen=\"" + Timbre.Cliente.Cte_RegimenFiscal + "\" >");
                        XML_Enviar.Append(" <Domicilio");
                        XML_Enviar.Append(" codigoPostal=\"" + Timbre.Cliente.Cte_FacCp + "\"");
                        XML_Enviar.Append(" pais=\"" + "México" + "\"");
                        XML_Enviar.Append(" estado=\"" + HttpUtility.HtmlEncode(Timbre.Cliente.Cte_FacEstado) + "\"");
                        XML_Enviar.Append(" municipio=\"" + HttpUtility.HtmlEncode(Timbre.Cliente.Cte_FacMunicipio) + "\"");
                        XML_Enviar.Append(" colonia=\"" + HttpUtility.HtmlEncode(Timbre.Cliente.Cte_FacColonia) + "\"");
                        XML_Enviar.Append(" noInterior=\"" + "" + "\"");
                        XML_Enviar.Append(" noExterior=\"" + HttpUtility.HtmlEncode(Timbre.Cliente.Cte_FacNumero) + "\"");
                        XML_Enviar.Append(" calle=\"" + HttpUtility.HtmlEncode(Timbre.Cliente.Cte_FacCalle) + "\" />");
                        XML_Enviar.Append(" <DatosdePago");
                        if (!string.IsNullOrEmpty(Timbre.Cliente.Cte_PagoBanco))
                        {

                            XML_Enviar.Append(" NomBancoOrdExt=\"" + Timbre.Cliente.Cte_PagoBanco + "\"");
                            XML_Enviar.Append(" CtaOrdenante=\"" + Timbre.Cliente.Cte_PagoNumeroCuenta + "\"");
                            XML_Enviar.Append(" RfcEmisorCtaOrd=\"" + Timbre.Cliente.Cte_PagoBancoRfc + "\"");
                            //Banco banco = new Banco();
                            //CN_CatBanco clsCatBanco = new CN_CatBanco();
                            //banco.Empresa = sesion.Id_Emp;
                            //banco.Centro = sesion.Id_Cd_Ver;
                            //banco.Id = Timbre.Cliente.Cte_PagoIdBan;
                            //clsCatBanco.ConsultaBancoPorId(sesion.Id_Emp, sesion.Id_Cd_Ver, Timbre.Cliente.Cte_PagoIdBan, sesion.Emp_Cnx, ref banco);
                            //if (banco != null && !string.IsNullOrEmpty(banco.Rfc))
                            //{
                            //    XML_Enviar.Append(" RfcEmisorCtaOrd=\"" + banco.Rfc + "\"");
                            //}

                        }
                        if (NumOperacion == "0")
                        { NumOperacion = ""; }

                        XML_Enviar.Append(" FechaPago=\"" + string.Format("{0:s}", fechaPago) + "\" ");
                        XML_Enviar.Append(" NumOperacion=\"" + NumOperacion + "\" />");
                        XML_Enviar.Append(" </Receptor>");

                        XML_Enviar.Append(" <Conceptos>");
                        XML_Enviar.Append(XML_Conceptos);
                        XML_Enviar.Append("</Conceptos>");
                        XML_Enviar.Append("</Comprobante>");
                        #endregion construirXML 


                        #region XMLPrueba
                        //XML_EnviarPrueba.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                        //XML_EnviarPrueba.Append("<Comprobante ");
                        //XML_EnviarPrueba.Append("serie=\"PRUEBA\" ");
                        //XML_EnviarPrueba.Append("folio=\"24\" ");
                        //XML_EnviarPrueba.Append("fecha=\"2017-10-15T08:25:00\" ");
                        //XML_EnviarPrueba.Append("total=\"19246.02\" ");
                        //XML_EnviarPrueba.Append("tipoDeComprobante=\"PAGO\" ");
                        //XML_EnviarPrueba.Append("tipoMovimiento=\"PAGO\" ");
                        //XML_EnviarPrueba.Append("tipoMoneda=\"pesos\" ");
                        //XML_EnviarPrueba.Append("tipoCambio=\"1\" ");
                        //XML_EnviarPrueba.Append("Notas=\"//////\" ");
                        //XML_EnviarPrueba.Append("Correo=\"\" ");
                        //XML_EnviarPrueba.Append("CliNum=\"31496\" ");
                        //XML_EnviarPrueba.Append("MetodoPago=\"03\" ");
                        //XML_EnviarPrueba.Append("ComprobanteVersion=\"3.3\">");
                        //XML_EnviarPrueba.Append("<Emisor ");
                        //XML_EnviarPrueba.Append("rfc=\"KQU-691101-6X5\" ");
                        //XML_EnviarPrueba.Append("numero=\"410\" />");
                        //XML_EnviarPrueba.Append("<Receptor ");
                        //XML_EnviarPrueba.Append("rfc=\"DMV840927T86\" ");
                        //XML_EnviarPrueba.Append("nombre=\"DESARROLLO MARINA VALLARTA, S.A. DE C.V.\" ");
                        //XML_EnviarPrueba.Append("UsoCFDI=\"P01\">");
                        //XML_EnviarPrueba.Append("<Domicilio ");
                        //XML_EnviarPrueba.Append("calle=\"Av 5 de Febrero\" ");
                        //XML_EnviarPrueba.Append("noExterior=\"1311-A\" ");
                        //XML_EnviarPrueba.Append("noInterior=\"\" ");
                        //XML_EnviarPrueba.Append("colonia=\"San Pablo\" ");
                        //XML_EnviarPrueba.Append("municipio=\"Querètaro\" ");
                        //XML_EnviarPrueba.Append("estado=\"Querètaro\" ");
                        //XML_EnviarPrueba.Append("pais=\"México\" ");
                        //XML_EnviarPrueba.Append("codigoPostal=\"76130\" />");
                        //XML_EnviarPrueba.Append("</Receptor>");
                        //XML_EnviarPrueba.Append("<Conceptos>");
                        //XML_EnviarPrueba.Append("<Concepto ");
                        //XML_EnviarPrueba.Append("UUID=\"7C8316AE-C3D3-4BA5-84CB-2AE89F5D4CAD\" ");
                        //XML_EnviarPrueba.Append("SPago=\"PRUEBA\" ");
                        //XML_EnviarPrueba.Append("FPago=\"63711\" ");
                        //XML_EnviarPrueba.Append("MonedaDR=\"MXN\" ");
                        //XML_EnviarPrueba.Append("TipoCambioDR=\"1.00\" ");
                        //XML_EnviarPrueba.Append("NumParcialidad=\"1\" ");
                        //XML_EnviarPrueba.Append("ImpSaldoAnt=\"13246.02\" ");
                        //XML_EnviarPrueba.Append("ImpPagado=\"13246.02\" ");
                        //XML_EnviarPrueba.Append("ImpSaldoInsoluto=\"0.00\" />");
                        //XML_EnviarPrueba.Append("<Concepto ");
                        //XML_EnviarPrueba.Append("UUID=\"9396E202-8482-40A7-A99B-6128E88AE174\" ");
                        //XML_EnviarPrueba.Append("SPago=\"PRUEBA\" ");
                        //XML_EnviarPrueba.Append("FPago=\"63712\" ");
                        //XML_EnviarPrueba.Append("MonedaDR=\"MXN\" ");
                        //XML_EnviarPrueba.Append("TipoCambioDR=\"1.00\" ");
                        //XML_EnviarPrueba.Append("NumParcialidad=\"1\" ");
                        //XML_EnviarPrueba.Append("ImpSaldoAnt=\"13246.02\" ");
                        //XML_EnviarPrueba.Append("ImpPagado=\"6000.00\" ");
                        //XML_EnviarPrueba.Append("ImpSaldoInsoluto=\"7246.02\" />");
                        //XML_EnviarPrueba.Append("</Conceptos>");
                        //XML_EnviarPrueba.Append("</Comprobante>");
                        #endregion XMLPrueba

                        //break;

                        XmlDocument xml = new XmlDocument();
                        xml.LoadXml(XML_Enviar.ToString());

                        StringWriter sw = new StringWriter();
                        XmlTextWriter tx = new XmlTextWriter(sw);
                        xml.WriteTo(tx);
                        string xmlString = sw.ToString();

                        WebReference.Service1 sianFacturacionElectronica = new WebReference.Service1();
                        if (Timbre.Facturas.Count() > 40)
                        {
                            int tiempo = 6000 * Timbre.Facturas.Count();
                            sianFacturacionElectronica.Timeout = tiempo;
                        }

                        sianFacturacionElectronica.Url = ConfigurationManager.AppSettings["WS_CFDIImpresion"].ToString();
                        sianFacturacionElectronicaResult = sianFacturacionElectronica.ObtieneCFD(xmlString);
                    }
                    catch (Exception ex)
                    {
                        foreach (var Factura in Timbre.Facturas)
                        {
                            int ver = 0;
                            PagoDet PagoDet = list_pagos.Where(at => at.Ref.Replace(" ", string.Empty) == Factura.Id_Fac.ToString()).Select(row => row).FirstOrDefault();
                            new CN_CapPago().BorrarComplementoPago(sesion.Id_Emp, sesion.Id_Cd_Ver, PagoDet.Id_Pag, folio, PagoDet.Id_PagDet, sesion.Emp_Cnx, ref ver);
                        }

                        strAlerta += ex.Message;
                        throw ex;
                    }
                    XmlDocument xmlSAT = new XmlDocument();
                    xmlSAT.LoadXml(sianFacturacionElectronicaResult.ToString());

                    //*********************************************//
                    //* Procesar XML recibido de servicio de SAT  *//  aqui
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
                    int idPag = 0;


                    #region analizaXML
                    foreach (XmlNode nodoSistemaFacturacion in xmlSAT.ChildNodes)
                    {
                        if (nodoSistemaFacturacion.Name == "Comprobante")
                        {

                            selloSAT = nodoSistemaFacturacion.Attributes["sello"].Value;

                            foreach (XmlNode Nodo_nivel2 in nodoSistemaFacturacion.ChildNodes)
                            {
                                if (Nodo_nivel2.Name == "AddendaKey")
                                {
                                    foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                    {
                                        if (Nodo_nivel3.Name == "PDF")
                                        {
                                            stringPDF = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                            idPag = Convert.ToInt32(Nodo_nivel3.Attributes["Id_Pag"].Value);
                                        }
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

                            VersionCFDI = nodoSistemaFacturacion.Attributes["Version"].Value;
                            foreach (XmlNode Nodo_nivel2 in nodoSistemaFacturacion.ChildNodes)
                            {

                                if (Nodo_nivel2.Name == "AddendaKey")
                                {
                                    foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                    {
                                        if (Nodo_nivel3.Name == "PDF")
                                        {
                                            stringPDF = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                            if (!string.IsNullOrEmpty(Nodo_nivel3.Attributes["Id_Pag"].Value))
                                            {
                                                idPag = Convert.ToInt32(Nodo_nivel3.Attributes["Id_Pag"].Value);
                                            }

                                        }
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

                                            selloSAT = nodo.Attributes["sello"].Value;

                                            foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                                            {
                                                if (Nodo_nivel2.Name == "AddendaKey")
                                                {
                                                    foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                                    {
                                                        if (Nodo_nivel3.Name == "PDF")
                                                        {
                                                            stringPDF = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                                            idPag = Convert.ToInt32(Nodo_nivel3.Attributes["Id_Pag"].Value);
                                                        }
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

                                            VersionCFDI = nodo.Attributes["Version"].Value;
                                            foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                                            {


                                                if (Nodo_nivel2.Name == "AddendaKey")
                                                {
                                                    foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                                    {
                                                        if (Nodo_nivel3.Name == "PDF")
                                                        {
                                                            stringPDF = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                                            idPag = Convert.ToInt32(Nodo_nivel3.Attributes["Id_Pag"].Value);
                                                        }
                                                        if (Nodo_nivel3.Name == "ERROR")
                                                        {
                                                            errorNum = Nodo_nivel3.Attributes["Numero"].Value;
                                                            errorText = Nodo_nivel3.Attributes["Texto"].Value;
                                                        }
                                                        if (Nodo_nivel3.Name == "IDPAG")
                                                        {
                                                            idPag = Convert.ToInt32(Nodo_nivel3.Attributes["Numero"].Value);
                                                        }
                                                    }

                                                    nodo.RemoveChild(Nodo_nivel2);
                                                }

                                                if (Nodo_nivel2.Name == "cfdi:Complemento")
                                                {
                                                    XmlNode Nodo_nivel3;
                                                    Nodo_nivel3 = Nodo_nivel2.FirstChild;

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
                                else
                                {
                                    if (nodoXmlSAT.Name == "ComprobanteKSL")
                                    {
                                        foreach (XmlNode nodo in nodoXmlSAT.ChildNodes)
                                        {
                                            if (nodo.Name == "Comprobante")
                                            {

                                                selloSATCN = nodo.Attributes["sello"].Value;

                                                foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                                                {
                                                    if (Nodo_nivel2.Name == "AddendaKey")
                                                    {
                                                        foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                                        {
                                                            if (Nodo_nivel3.Name == "PDF")
                                                            {
                                                                stringPDFCN = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                                                idPag = Convert.ToInt32(Nodo_nivel3.Attributes["Id_Pag"].Value);
                                                            }
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
                                                foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                                                {

                                                    if (Nodo_nivel2.Name == "AddendaKey")
                                                    {
                                                        foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                                        {
                                                            if (Nodo_nivel3.Name == "PDF")
                                                            {
                                                                stringPDFCN = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                                                idPag = Convert.ToInt32(Nodo_nivel3.Attributes["Id_Pag"].Value);
                                                            }
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
                    #endregion analizaXML

                    #region imprimirResultado
                    if (errorNum != "0")
                    {
                        string strLstFolios = String.Join(",", Timbre.Facturas.Select(x => x.Id_Fac.ToString()).ToList());
                        string textoDecodificado = WebUtility.HtmlDecode(errorText);
                        textoDecodificado = textoDecodificado.Remove(0, 1).Replace("'", "\"").Replace("|", "<br>");
                        strAlerta += "Cliente:" + Timbre.Cliente.Cte_NomComercial + ", folios: " + strLstFolios + ", Error: " + textoDecodificado;
                        //this.Alerta(errorText.Remove(0, 1).Replace("'", "\"").Replace("|", "<br>"), 660, 150);
                        //Alerta(string mensaje, int largo = 330, int ancho=150)
                        /* factura.Fac_Sello = selloSAT;
                         System.Data.SqlTypes.SqlXml sqlXml
                             = new System.Data.SqlTypes.SqlXml(new XmlTextReader(xmlSAT.OuterXml, XmlNodeType.Document, null));
                         factura.Fac_Xml = sqlXml;
                         factura.Fac_Pdf = this.Base64ToByte(stringPDF);
                         verificador = 0;
                         new CN_CapFactura().ModificarFacturaSAT(factura, sesion.Emp_Cnx, ref verificador);*/
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(errorNumCN) && errorNumCN != "0")
                        {
                            string strLstFoliosNC = String.Join(",", Timbre.Facturas.Select(x => x.Id_Fac.ToString()).ToList());
                            strAlerta += "Cliente:" + Timbre.Cliente.Cte_NomComercial + ", folios: " + strLstFoliosNC + ", Error: " + errorTextCN.Remove(0, 1).Replace("'", "\"").Replace("|", "<br>");
                            //this.Alerta(errorTextCN.Remove(0, 1).Replace("'", "\"").Replace("|", "<br>"), 660, 150);
                        }
                        else
                        {
                            if (idPag == pago.Id_Pag)
                            {
                                List<PagoDetComplemento> complementos = new List<PagoDetComplemento>();
                                new CN_CapPago().ConsultaListaComplementosPago(pago.Id_Emp, pago.Id_Cd, pago.Id_Pag, Timbre.Cliente.Id_Cte.Value, folio, sesion.Emp_Cnx, ref complementos);

                                List<PagoDet> ListaPagoDet = new List<PagoDet>();
                                PagoDet PagoDet;
                                foreach (var Factura in Timbre.Facturas)
                                {
                                    PagoDet = new PagoDet();
                                    PagoDet = list_pagos.Where(at => at.Ref.Replace(" ", string.Empty) == Factura.Id_Fac.ToString()).Select(row => row).First();
                                    ListaPagoDet.Add(PagoDet);
                                }

                                var query = (from TComplementos in complementos
                                             join TListaPagoDet in ListaPagoDet on TComplementos.Id_PagDet equals TListaPagoDet.Id_PagDet
                                             where TComplementos.Id_PagComp == folio
                                             select TComplementos).ToList();

                                foreach (var item in query)
                                {
                                    item.Pago_FolioFiscal = folioFiscal;
                                    item.Pago_Pdf = this.Base64ToByte(stringPDF);
                                    if (!string.IsNullOrEmpty(Timbre.Cliente.Cte_PagoMetodoPago))
                                    {
                                        item.Cte_Fpago = Convert.ToInt32(Timbre.Cliente.Cte_PagoMetodoPago);
                                    }
                                    else
                                    {
                                        item.Cte_Fpago = Convert.ToInt32(Timbre.Facturas.First().Fac_FPago);
                                        if (item.Cte_Fpago == 99)
                                            item.Cte_Fpago = 3;
                                    }

                                    item.Cte_UsoCFDI = Timbre.Cliente.Cte_PagoUsoCFDI;
                                    System.Data.SqlTypes.SqlXml sqlXml;
                                    sqlXml = new System.Data.SqlTypes.SqlXml(new XmlTextReader(xmlSAT.OuterXml, XmlNodeType.Document, null));
                                    item.Pago_Xml = sqlXml;
                                    item.FechaCreacionXML = DateTime.Now;
                                    new CN_CapPago().ModificarComplementoPago(item, sesion.Emp_Cnx, true, ref verificador);
                                    if (verificador < 0)
                                    {
                                        strAlerta = "No se pudo actualizar la información en el timbre";
                                        // Alerta("No se pudo actualizar la información en el timbre");

                                    }
                                }
                            }
                            string tempPDFname = nombrePDF;
                            string URLtempPDF = String.Empty;
                            if (isSegundoPlano)
                            {
                                URLtempPDF = System.Web.Hosting.HostingEnvironment.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                            }
                            else
                            {
                                URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                            }
                            string WebURLtempPDF = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFname);
                            this.ByteToTempPDF(URLtempPDF, this.Base64ToByte(stringPDF));

                            strResponseScripts = WebURLtempPDF;
                        }

                    }
                    #endregion imprimirResultado
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

        #endregion

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

        protected void cmbFicha_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                RadComboBox rgcmbMov = (RadComboBox)o;
                int intPago_Ficha = int.Parse(rgcmbMov.SelectedValue);

                DataRow[] Ar_dr;
                DataTable dtFiltro = new DataTable();
                dtFiltro.Columns.Add("RgDId", System.Type.GetType("System.Int32"));
                dtFiltro.Columns.Add("Pag_Movimiento", System.Type.GetType("System.Int32"));
                dtFiltro.Columns.Add("Pag_MovimientoStr", System.Type.GetType("System.String"));
                dtFiltro.Columns.Add("Doc_Referencia", System.Type.GetType("System.String"));
                dtFiltro.Columns.Add("Fac_FolioFiscal", System.Type.GetType("System.String"));
                dtFiltro.Columns.Add("FormaPago", System.Type.GetType("System.String"));
                dtFiltro.Columns.Add("MetodoPago", System.Type.GetType("System.String"));
                dtFiltro.Columns.Add("Id_Terr", System.Type.GetType("System.Int32"));
                dtFiltro.Columns.Add("Serie", System.Type.GetType("System.String"));
                dtFiltro.Columns.Add("Doc_Fecha", System.Type.GetType("System.DateTime"));
                dtFiltro.Columns.Add("Id_Cte", System.Type.GetType("System.Int32"));
                dtFiltro.Columns.Add("Cte_Nombre", System.Type.GetType("System.String"));
                dtFiltro.Columns.Add("Pag_Numero", System.Type.GetType("System.Int32"));
                dtFiltro.Columns.Add("Pag_Cheque", System.Type.GetType("System.String"));
                dtFiltro.Columns.Add("Pag_Importe", System.Type.GetType("System.Double"));
                dtFiltro.Columns.Add("Doc_Estatus", System.Type.GetType("System.String"));
                dtFiltro.Columns.Add("Doc_Importe", System.Type.GetType("System.Double"));
                dtFiltro.Columns.Add("Doc_Pagado", System.Type.GetType("System.Double"));
                dtFiltro.Columns.Add("Cdi", System.Type.GetType("System.Int32"));
                dtFiltro.Columns.Add("Id_Emp", System.Type.GetType("System.Int32"));
                dtFiltro.Columns.Add("Id_PagDet", System.Type.GetType("System.Int32"));
                dtFiltro.Columns.Add("Cte_Rfc", System.Type.GetType("System.String"));
                int intFicha = 0;


                //Ar_dr = dtDetInicial.Select("Pag_Numero='" + intPago_Ficha + "'");
                //foreach (var item in Ar_dr)
                //{

                //    dtFiltro.Rows.Add(item);
                //}
                // var query = from order in dtDetInicial.AsEnumerable() where order.Field<int>("Pag_Numero") == intPago_Ficha ;

                for (int x = 0; x < dtDetInicial.Rows.Count; x++)
                {

                    intFicha = (int)dtDetInicial.Rows[x]["Pag_Numero"];
                    if (intFicha == intPago_Ficha || intPago_Ficha == -1)
                    {
                        DataRow resultRow = dtFiltro.NewRow();
                        DataRow dr = dtDetInicial.Rows[x];

                        resultRow["RgDId"] = x + 1;
                        resultRow["Pag_Movimiento"] = dr["Pag_Movimiento"];
                        resultRow["Pag_MovimientoStr"] = dr["Pag_MovimientoStr"];
                        resultRow["Doc_Referencia"] = dr["Doc_Referencia"];
                        resultRow["Fac_FolioFiscal"] = dr["Fac_FolioFiscal"];
                        resultRow["FormaPago"] = dr["FormaPago"];
                        resultRow["MetodoPago"] = dr["MetodoPago"];
                        resultRow["Id_Terr"] = dr["Id_Terr"];
                        resultRow["Serie"] = dr["Serie"];
                        resultRow["Doc_Fecha"] = dr["Doc_Fecha"];
                        resultRow["Id_Cte"] = dr["Id_Cte"];
                        resultRow["Cte_Nombre"] = dr["Cte_Nombre"];
                        resultRow["Pag_Numero"] = dr["Pag_Numero"];
                        resultRow["Pag_Cheque"] = dr["Pag_Cheque"];
                        resultRow["Pag_Importe"] = dr["Pag_Importe"];
                        resultRow["Doc_Estatus"] = dr["Doc_Estatus"];
                        resultRow["Doc_Importe"] = dr["Doc_Importe"];
                        resultRow["Doc_Pagado"] = dr["Doc_Pagado"];
                        resultRow["Cdi"] = dr["Cdi"];
                        resultRow["Id_Emp"] = dr["Id_Emp"];
                        resultRow["Id_PagDet"] = dr["Id_PagDet"];
                        resultRow["Cte_Rfc"] = dr["Cte_Rfc"];
                        dtFiltro.Rows.Add(resultRow);
                    }
                    //icha_cd.Pag_Ficha = !string.IsNullOrEmpty(dtGral.Rows[x]["Pag_ficha"].ToString()) ? Convert.ToInt32(dtGral.Rows[x]["Pag_ficha"]) : 0;

                    dtDet = dtFiltro;
                    RgDet.DataSource = dtDet;
                    RgDet.Rebind();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }

        }


    }
}