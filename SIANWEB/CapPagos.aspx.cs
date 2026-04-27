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
using System.Web.UI.HtmlControls;

namespace SIANWEB
{
    public partial class CapPagos : System.Web.UI.Page
    {
        #region Variables
        public string NombreArchivo;
        public string NombreHojaExcel;
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private DataTable dtGral
        {
            //Se quita variable sesion para evitar que se borren las partidas de los pagos
            // 07/11/2016 
            get { return (DataTable)Session["dtGralPagos" + Request.Params["IdPagina"].ToString()]; }
            set { Session["dtGralPagos" + Request.Params["IdPagina"].ToString()] = value; }
        }

        public string FechaEnable
        {
            get
            {
                if (rdFechaPago.Enabled)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
        }
        private DataTable dtDet
        {
            //Se quita variable sesion para evitar que se borren las partidas de los pagos
            // 07/11/2016
            get
            {
                return (DataTable)Session["dtDetPagos" + Request.Params["IdPagina"].ToString()];
            }
            set
            {
                Session["dtDetPagos" + Request.Params["IdPagina"].ToString()] = value;
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

                        //Session["PostBackPagos" + Session.SessionID] = true;
                        //clientSideIsPostBack.Value = "S";
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        Session["PosBackPagos" + Session.SessionID] = "1";
                        Inicializar();

                        if (!((RadToolBarItem)rtb1.Items.FindItemByValue("save")).Visible)
                        {
                            deshabilitarcontroles(Generales.Controls);

                            GridCommandItem cmdItem = (GridCommandItem)RgDet.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                            cmdItem.FindControl("AddNewRecordButton").Parent.Visible = false;
                            RgDet.MasterTableView.Columns[RgDet.MasterTableView.Columns.Count - 1].Display = false;
                            RgDet.MasterTableView.Columns[RgDet.MasterTableView.Columns.Count - 2].Display = false;

                            GridCommandItem cmdItem2 = (GridCommandItem)RgGral.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                            cmdItem2.FindControl("AddNewRecordButton").Parent.Visible = false;
                            RgGral.MasterTableView.Columns[RgGral.MasterTableView.Columns.Count - 1].Display = false;
                            RgGral.MasterTableView.Columns[RgGral.MasterTableView.Columns.Count - 2].Display = false;
                            RgGral.MasterTableView.Columns[RgGral.MasterTableView.Columns.Count - 4].HeaderStyle.Width = Unit.Pixel(502);
                        }

                        double ancho = 0;
                        foreach (GridColumn gc in RgDet.Columns)
                        {
                            if (gc.Display)
                            {
                                ancho = ancho + gc.HeaderStyle.Width.Value;
                            }
                        }
                        RgDet.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        RgDet.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));

                        ancho = 0;
                        foreach (GridColumn gc in RgGral.Columns)
                        {
                            if (gc.Display)
                            {
                                ancho = ancho + gc.HeaderStyle.Width.Value;
                            }
                        }
                        RgGral.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        RgGral.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));
                    }
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
                RAM1.ResponseScripts.Add("HabilitarGuardar();");
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
                int Id_mov = int.Parse(txtMovimiento.Text);
                CN_Comun.LlenaCombo(Id_mov, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatBanco_Combo", ref ComboBox);
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
        protected void RgGral_ItemCreated(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridEditableItem && e.Item.IsInEditMode)
                {
                    GridEditableItem item = e.Item as GridEditableItem;

                    CN_CapPago pagos = new CN_CapPago();
                    Sesion sesion = new Sesion();
                    sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    int Id_Cd = 0;
                    pagos.ValidaSucursal(sesion, ref Id_Cd);
                    if (Id_Cd > 0)
                    {
                        if (txtMovimiento.Text == "1")
                        {
                            RadNumericTextBox txtIdBanco = item.FindControl("rgtxtIdBanco") as RadNumericTextBox;
                            RadComboBox cmbBanco = item.FindControl("cmbBancos") as RadComboBox;

                            txtIdBanco.ReadOnly = true;
                            cmbBanco.Enabled = false;

                            if (item.FindControl("txtPag_RefBancaria") != null)
                                (item.FindControl("txtPag_RefBancaria") as RadTextBox).Focus();
                        }
                        if (txtMovimiento.Text == "42")
                        {
                            if (item.FindControl("rdFecha") != null)
                                (item.FindControl("rdFecha") as RadDatePicker).DateInput.Focus();
                        }
                    }
                    else
                    {
                        if (item.FindControl("rdFecha") != null)
                            (item.FindControl("rdFecha") as RadDatePicker).DateInput.Focus();
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
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    RgDet.DataSource = dtDet;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RgDet_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":
                        break;
                    case "PerformInsert":
                        PerformInsertDet(e);
                        break;
                    case "Update":
                        UpdateDet(e);
                        break;
                    case "Delete":
                        DeleteDet(e);
                        break;
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
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                RadTextBox rgtxtReferencia = source as RadTextBox;
                RadTextBox rgSerie = rgtxtReferencia.Parent.FindControl("rgSerie") as RadTextBox;
                RadNumericTextBox rgCdi = rgSerie.Parent.FindControl("rgCdi") as RadNumericTextBox;
                RadComboBox rgcmbMov = rgtxtReferencia.Parent.FindControl("rgcmbMov") as RadComboBox;
                RadNumericTextBox rgTerr = rgtxtReferencia.Parent.FindControl("rgTerr") as RadNumericTextBox;
                RadDatePicker rdFecha = rgtxtReferencia.Parent.FindControl("rdFecha") as RadDatePicker;
                RadNumericTextBox rgCte = rgtxtReferencia.Parent.FindControl("rgCte") as RadNumericTextBox;
                RadTextBox rgCteNombre = rgtxtReferencia.Parent.FindControl("rgCteNombre") as RadTextBox;
                RadTextBox rgEstatus = rgtxtReferencia.Parent.FindControl("rgEstatus") as RadTextBox;
                RadNumericTextBox rgImporte = rgtxtReferencia.Parent.FindControl("rgImporte") as RadNumericTextBox;
                RadNumericTextBox rgPagado = rgtxtReferencia.Parent.FindControl("rgPagado") as RadNumericTextBox;
                RadNumericTextBox rgtxtImporte = rgtxtReferencia.Parent.FindControl("rgtxtImporte") as RadNumericTextBox;
                RadTextBox rgCheque = rgSerie.Parent.FindControl("rgCheque") as RadTextBox;




                if (!rgtxtReferencia.AutoPostBack)
                {
                    return;
                }

                rgCte.Text = "";

                if (rgSerie.Text == "" || rgtxtReferencia.Text == "")
                {
                    rgSerie.Focus();
                    return;
                }
                else
                {
                    rgCte.Focus();
                }

                if (rgCdi.Text == sesion.Id_Cd_Ver.ToString())
                {
                    rgTxtCliente_TextChanged(rgCte, null);
                }
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
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];


                RadNumericTextBox rgCte = source as RadNumericTextBox;
                RadTextBox rgtxtReferencia = rgCte.Parent.FindControl("rgReferencia") as RadTextBox;
                RadTextBox rgtxtFolioFiscal = rgCte.Parent.FindControl("rgFolioFiscal") as RadTextBox;

                RadTextBox rgSerie = rgtxtReferencia.Parent.FindControl("rgSerie") as RadTextBox;
                RadNumericTextBox rgCdi = rgSerie.Parent.FindControl("rgCdi") as RadNumericTextBox;
                RadComboBox rgcmbMov = rgtxtReferencia.Parent.FindControl("rgcmbMov") as RadComboBox;
                RadNumericTextBox rgTerr = rgtxtReferencia.Parent.FindControl("rgTerr") as RadNumericTextBox;
                RadDatePicker rdFecha = rgtxtReferencia.Parent.FindControl("rdFecha") as RadDatePicker;
                RadTextBox rgCteNombre = rgtxtReferencia.Parent.FindControl("rgCteNombre") as RadTextBox;
                RadTextBox rgEstatus = rgtxtReferencia.Parent.FindControl("rgEstatus") as RadTextBox;
                RadNumericTextBox rgImporte = rgtxtReferencia.Parent.FindControl("rgImporte") as RadNumericTextBox;
                RadNumericTextBox rgPagado = rgtxtReferencia.Parent.FindControl("rgPagado") as RadNumericTextBox;
                RadNumericTextBox rgtxtImporte = rgtxtReferencia.Parent.FindControl("rgtxtImporte") as RadNumericTextBox;
                RadNumericTextBox RadNumericTextBox1 = rgtxtReferencia.Parent.FindControl("RadNumericTextBox1") as RadNumericTextBox;
                RadTextBox rgCheque = rgSerie.Parent.FindControl("rgCheque") as RadTextBox;

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
                RadNumericTextBox1.Text = "";

                if (rgtxtReferencia.Text == "")
                {
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
                if (rgtxtReferencia.Text == "")
                {
                    rgtxtReferencia.Focus();
                    return;
                }
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
                                RadNumericTextBox1.Text = (Math.Round(ficha.Fac_Saldo.Value - ficha.Fac_Pagado.Value, 2)).ToString();
                                rgtxtFolioFiscal.Text = ficha.Fac_FolioFiscal;
                            }
                            else
                            {
                                AlertaFocus("El documento no pertenece al cliente capturado", rgCte.ClientID);
                                rgCte.Text = "";
                                return;
                            }

                            /*if (rdFechaPago.SelectedDate.Value < rdFecha.SelectedDate)
                            {
                               AlertaFocus("Imposible hacer un pago a un documento con fecha mayor a la fecha del pago", rgSerie.ClientID);
                               LimpiarDetalle(rgTerr, rdFecha, rgCte, rgCteNombre, rgEstatus, rgtxtImporte, rgPagado, rgtxtFolioFiscal);
                               rgtxtReferencia.Focus();
                               return;
                           }*/

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
                        cn_cappago.ConsultaPagoNotaFicha(ref ficha, Emp_CnxCob, ref verificador);
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
                            RadNumericTextBox1.Text = ficha.Importe.ToString();
                            rgtxtFolioFiscal.Text = ficha.Nca_FolioFiscal;
                        }
                        else
                        {
                            Alerta("Movimiento no existe; posiblemente sea un movimiento externo");
                            return;
                        }
                        /*if (rdFechaPago.SelectedDate.Value < rdFecha.SelectedDate)
                        {
                            AlertaFocus("Imposible hacer un pago a un documento con fecha mayor a la fecha del pago", rgSerie.ClientID);
                            LimpiarDetalle(rgTerr, rdFecha, rgCte, rgCteNombre, rgEstatus, rgtxtImporte, rgPagado, rgtxtFolioFiscal);
                            rgtxtReferencia.Focus();
                            return;
                        }*/
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
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
        protected void rgTxtSerie_TextChanged(object source, EventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                RadTextBox rgSerie = source as RadTextBox;
                RadTextBox rgtxtReferencia = rgSerie.Parent.FindControl("rgReferencia") as RadTextBox;
                RadComboBox rgcmbMov = rgSerie.Parent.FindControl("rgcmbMov") as RadComboBox;
                RadNumericTextBox rgTerr = rgSerie.Parent.FindControl("rgTerr") as RadNumericTextBox;
                RadNumericTextBox rgCdi = rgSerie.Parent.FindControl("rgCdi") as RadNumericTextBox;
                RadDatePicker rdFecha = rgSerie.Parent.FindControl("rdFecha") as RadDatePicker;
                RadNumericTextBox rgCte = rgSerie.Parent.FindControl("rgCte") as RadNumericTextBox;
                RadTextBox rgCteNombre = rgSerie.Parent.FindControl("rgCteNombre") as RadTextBox;
                RadTextBox rgEstatus = rgSerie.Parent.FindControl("rgEstatus") as RadTextBox;
                RadNumericTextBox rgImporte = rgSerie.Parent.FindControl("rgImporte") as RadNumericTextBox;
                RadNumericTextBox rgPagado = rgSerie.Parent.FindControl("rgPagado") as RadNumericTextBox;
                RadNumericTextBox rgtxtImporte = rgSerie.Parent.FindControl("rgtxtImporte") as RadNumericTextBox;
                RadTextBox rgCheque = rgSerie.Parent.FindControl("rgCheque") as RadTextBox;
                RadTextBox rgFolioFiscal = rgSerie.Parent.FindControl("rgFolioFiscal") as RadTextBox;

                rgSerie.Text = rgSerie.Text.ToUpper();
                rgtxtReferencia.Text = "";
                rgCdi.Text = "";
                rgTerr.Text = "";
                rdFecha.DbSelectedDate = null;
                rgCte.Text = "";
                rgCteNombre.Text = "";
                rgtxtImporte.Text = "";
                rgCheque.Text = "";
                rgFolioFiscal.Text = "";

                int verificador = 0;

                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                int Tipo_CDC = 0;
                new CN_CatCliente().ConsultaTipoCDC(session2.Id_Cd_Ver, ref Tipo_CDC, session2.Emp_Cnx);


                CN_CapPago cn_cappago = new CN_CapPago();
                DbCentro centro = new DbCentro();
                cn_cappago.ConsultarCentro(sesion.Id_Emp, rgSerie.Text, ref centro, Emp_CnxCob, Tipo_CDC);
                verificador = centro.Id_Cd;

                if (verificador == 0)
                {
                    rgtxtReferencia.AutoPostBack = false;
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
                        rgtxtReferencia.Text = "";
                        Alerta("La sucursal externa ya realizo el cierre extemporáneo de pagos");
                        rgSerie.Focus();
                        return;
                    }
                    else
                    {
                        rgtxtReferencia.AutoPostBack = true;
                        rgTerr.ReadOnly = true;
                        rdFecha.DateInput.ReadOnly = true;
                        rdFecha.DatePopupButton.Enabled = false;
                        rgCteNombre.ReadOnly = true;
                        rgCdi.ReadOnly = true;
                        rgCdi.DbValue = verificador;

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

                rgtxtReferencia.Focus();
                return;

                if (rgSerie.Text == "" || rgtxtReferencia.Text == "")
                {

                    rgtxtReferencia.Focus();
                    return;
                }



                rgtxtReferencia.Text = rgtxtReferencia.Text.ToUpper();

                string Referencia = rgtxtReferencia.Text;
                int Movimiento = Convert.ToInt32(rgcmbMov.SelectedValue);
                DataRow[] dr_ar = dtDet.Select("Doc_Referencia='" + Referencia + "' and Serie='" + rgSerie.Text + "'");
                if (dr_ar.Length == 0)
                {
                    LimpiarDetalle(rgTerr, rdFecha, rgCte, rgCteNombre, rgEstatus, rgtxtImporte, rgPagado, rgFolioFiscal);
                    if (Movimiento == 1)
                    {
                        Factura ficha = new Factura();
                        ficha.Id_Emp = sesion.Id_Emp;
                        ficha.Id_Cd = Convert.ToInt32(rgCdi.Value);
                        ficha.Serie = rgSerie.Text;
                        ficha.Id_FacSerie = Referencia;

                        //CN_CapPago cn_cappago = new CN_CapPago();
                        verificador = 0;
                        cn_cappago.ConsultaPagoFicha(ref ficha, Emp_CnxCob, ref verificador);

                        if (verificador == 1)
                        {
                            rgTerr.Text = ficha.Id_Ter.ToString();
                            rdFecha.SelectedDate = ficha.Fac_Fecha;
                            rgCte.Text = ficha.Id_Cte.ToString();
                            rgCteNombre.Text = ficha.Cte_NomComercial;
                            rgEstatus.Text = ficha.Fac_Estatus;
                            rgImporte.Text = ficha.Fac_Saldo.ToString();
                            rgPagado.Text = ficha.Fac_Pagado.ToString();
                            rgtxtImporte.Text = (ficha.Fac_Saldo - ficha.Fac_Pagado).ToString();
                        }
                        else
                        {
                            Alerta("Movimiento no existe; posiblemente sea un movimiento externo");
                            return;
                        }
                    }
                    else if (Movimiento == 2)
                    {
                        NotaCargo ficha = new NotaCargo();
                        ficha.Id_Emp = sesion.Id_Emp;
                        ficha.Serie = rgSerie.Text;
                        ficha.Id_Nca = Convert.ToInt32(Referencia);
                        CN_CapNotaCargo cn_capnotacargo = new CN_CapNotaCargo();
                        verificador = 0;
                        cn_capnotacargo.ConsultaPagoFicha(ref ficha, sesion.Emp_Cnx, ref verificador);
                        if (verificador == 1)
                        {
                            rgTerr.Text = ficha.Id_Ter.ToString();
                            rdFecha.SelectedDate = ficha.Nca_Fecha;
                            rgCte.Text = ficha.Id_Cte.ToString();
                            rgCteNombre.Text = ficha.Cte_NomComercial;
                            rgEstatus.Text = ficha.Nca_Estatus;
                            rgImporte.Text = ficha.Importe.ToString();
                            rgPagado.Text = ficha.Nca_Pagado.ToString();
                            rgtxtImporte.Text = ficha.Importe.ToString();
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
                        LimpiarDetalle(rgTerr, rdFecha, rgCte, rgCteNombre, rgEstatus, rgtxtImporte, rgPagado, rgFolioFiscal);
                        rgtxtReferencia.Focus();
                        return;
                    }
                    else if (rgEstatus.Text == "C")
                    {
                        Alerta("Movimiento se encuentra en status capturado; imposible aplicarle un pago");
                        LimpiarDetalle(rgTerr, rdFecha, rgCte, rgCteNombre, rgEstatus, rgtxtImporte, rgPagado, rgFolioFiscal);
                        rgtxtReferencia.Focus();
                        return;
                    }
                    if (Convert.ToDouble(rgImporte.Text) - Convert.ToDouble(rgPagado.Text) <= 0)
                    {
                        Alerta("Movimiento no tiene saldo positivo; imposible aplicarle un pago");
                        LimpiarDetalle(rgTerr, rdFecha, rgCte, rgCteNombre, rgEstatus, rgtxtImporte, rgPagado, rgFolioFiscal);
                        rgtxtReferencia.Focus();
                        return;
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
            rgtxtFolioFiscal.Text = string.Empty;
        }
        protected void rgcmbMov_DataBinding(object source, EventArgs e)
        {
            try
            {
                RadComboBox rgcmbMov = source as RadComboBox;
                Label fichaNum;
                for (int x = 0; x < RgGral.Items.Count; x++)
                {
                    fichaNum = RgGral.Items[x]["Pag_ficha"].FindControl("lblFicha") as Label;
                    rgcmbMov.Items.Add(new RadComboBoxItem(fichaNum.Text, fichaNum.Text));
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbDocumento_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {

            try
            {
                RadComboBox combo = o as RadComboBox;
                RadTextBox rgSerie = combo.Parent.FindControl("rgSerie") as RadTextBox;
                RadTextBox rgtxtReferencia = combo.Parent.FindControl("rgReferencia") as RadTextBox;
                RadComboBox rgcmbMov = rgSerie.Parent.FindControl("rgcmbMov") as RadComboBox;
                RadNumericTextBox rgTerr = rgSerie.Parent.FindControl("rgTerr") as RadNumericTextBox;
                RadDatePicker rdFecha = rgSerie.Parent.FindControl("rdFecha") as RadDatePicker;
                RadNumericTextBox rgCte = rgSerie.Parent.FindControl("rgCte") as RadNumericTextBox;
                RadTextBox rgCteNombre = rgSerie.Parent.FindControl("rgCteNombre") as RadTextBox;
                RadTextBox rgEstatus = rgSerie.Parent.FindControl("rgEstatus") as RadTextBox;
                RadNumericTextBox rgImporte = rgSerie.Parent.FindControl("rgImporte") as RadNumericTextBox;
                RadNumericTextBox rgPagado = rgSerie.Parent.FindControl("rgPagado") as RadNumericTextBox;
                RadNumericTextBox rgtxtImporte = rgSerie.Parent.FindControl("rgtxtImporte") as RadNumericTextBox;
                RadTextBox rgCheque = rgSerie.Parent.FindControl("rgCheque") as RadTextBox;
                RadNumericTextBox Cdi = rgSerie.Parent.FindControl("rgCdi") as RadNumericTextBox;

                Sesion session2 = new Sesion();
                session2 = (Sesion)Session["Sesion" + Session.SessionID];
                string Folio = null;
                CN_CatCliente cn_cte = new CN_CatCliente();
                cn_cte.ConsultaFolioFactEle(session2, int.Parse(combo.SelectedValue), ref Folio);
                rgtxtReferencia.AutoPostBack = true;
                rgSerie.Text = Folio;
                Cdi.Value = session2.Id_Cd_Ver;
                rgtxtReferencia.Text = "";
                rgTerr.Text = "";
                rdFecha.DbSelectedDate = null;
                rgCte.Text = "";
                rgCteNombre.Text = "";
                rgtxtImporte.Text = "";
                rgCheque.Text = "";
                rgtxtReferencia.Focus();

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RgDet_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
            {
                GridEditableItem editItem = (GridEditableItem)e.Item;
                string Doc_Estatus = ((RadTextBox)editItem.FindControl("rgEstatus")).Text;
                if (Doc_Estatus == "")
                {
                    RadTextBox rgtxtReferencia = editItem.FindControl("rgReferencia") as RadTextBox;
                    RadTextBox rgFolioFiscal = editItem.FindControl("rgFolioFiscal") as RadTextBox;
                    RadComboBox rgcmbMov = editItem.FindControl("rgcmbMov") as RadComboBox;
                    RadNumericTextBox rgTerr = editItem.FindControl("rgTerr") as RadNumericTextBox;
                    RadNumericTextBox rgCdi = editItem.FindControl("rgCdi") as RadNumericTextBox;
                    RadDatePicker rdFecha = editItem.FindControl("rdFecha") as RadDatePicker;
                    RadNumericTextBox rgCte = editItem.FindControl("rgCte") as RadNumericTextBox;
                    RadTextBox rgCteNombre = editItem.FindControl("rgCteNombre") as RadTextBox;
                    RadTextBox rgEstatus = editItem.FindControl("rgEstatus") as RadTextBox;
                    RadNumericTextBox rgImporte = editItem.FindControl("rgImporte") as RadNumericTextBox;
                    RadNumericTextBox rgPagado = editItem.FindControl("rgPagado") as RadNumericTextBox;
                    RadNumericTextBox rgtxtImporte = editItem.FindControl("rgtxtImporte") as RadNumericTextBox;
                    RadTextBox rgCheque = editItem.FindControl("rgCheque") as RadTextBox;


                    rgtxtReferencia.AutoPostBack = false;
                    rgTerr.ReadOnly = false;
                    rdFecha.DateInput.ReadOnly = false;
                    rdFecha.DatePopupButton.Enabled = true;
                    rgCte.ReadOnly = false;
                    rgCteNombre.ReadOnly = false;
                    rgCdi.ReadOnly = false;
                    rgFolioFiscal.ReadOnly = true;
                    Sesion session2 = new Sesion();
                    session2 = (Sesion)Session["Sesion" + Session.SessionID];
                    string Folio = null;
                    CN_CatCliente cn_cte = new CN_CatCliente();
                    RadComboBox rdx = (editItem.FindControl("rgcmbMov") as RadComboBox);
                    RadTextBox Serie = (editItem.FindControl("rgSerie") as RadTextBox);
                    RadNumericTextBox Cdi = (editItem.FindControl("rgCdi") as RadNumericTextBox);

                    cn_cte.ConsultaFolioFactEle(session2, int.Parse(rdx.SelectedValue), ref Folio);
                    rgtxtReferencia.AutoPostBack = true;
                    Serie.Text = Folio;
                    Cdi.Value = session2.Id_Cd_Ver;

                    rgtxtReferencia.Focus();

                }
            }
        }
        /*        protected void txtPag_RefBancaria_TextChanged(object sender, EventArgs e)
                {
                    RadTextBox combo = (RadTextBox)sender;
                    Telerik.Web.UI.GridTableCell tabla = (Telerik.Web.UI.GridTableCell)combo.Parent;
                    RadNumericTextBox txtIdBanco = (RadNumericTextBox)tabla.FindControl("rgtxtIdBanco");
                    RadTextBox txtPag_RefBancaria = (RadTextBox)tabla.FindControl("txtPag_RefBancaria");

                    if (txtPag_RefBancaria.Text != "")
                    {
                        CN_CapPago pagos = new CN_CapPago();
                        Sesion sesion = new Sesion();
                        sesion = (Sesion)Session["Sesion" + Session.SessionID];
                        int banco = txtIdBanco.Value.HasValue ? Convert.ToInt32(txtIdBanco.Value.Value) : 0;
                        string RefBancaria = txtPag_RefBancaria.Text;
                        string ValidaReferencia = "";
                        pagos.ValidaReferenciaBancaria(sesion, banco, RefBancaria, ref ValidaReferencia);
                        if (ValidaReferencia != "") { 
                            Alerta(ValidaReferencia);
                            ((RadTextBox)tabla.FindControl("txtPag_RefBancaria")).Focus();
                            return;
                        } else
                        {
                            //((RadNumericTextBox)tabla.FindControl("rgtxtImporte")).Focus();
                            return;
                        }
                    }
                    else {

                        ((RadTextBox)tabla.FindControl("txtPag_RefBancaria")).Focus();
                        return;
                    }
                }

                */
        protected void txtBanco_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                RadNumericTextBox combo = (RadNumericTextBox)sender;
                Telerik.Web.UI.GridTableCell tabla = (Telerik.Web.UI.GridTableCell)combo.Parent;
                RadNumericTextBox txtIdBanco = (RadNumericTextBox)tabla.FindControl("rgtxtIdBanco");
                RadTextBox txtBanCuenta = (RadTextBox)tabla.FindControl("txtBanCuenta");
                RadTextBox txtNumOperacion = (RadTextBox)tabla.FindControl("txtNumOperacion");
                RadTextBox txtPag_RefBancaria = (RadTextBox)tabla.FindControl("txtPag_RefBancaria");
                CN_CapPago pagos = new CN_CapPago();
                string cuenta = "";
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                int banco = txtIdBanco.Value.HasValue ? Convert.ToInt32(txtIdBanco.Value.Value) : 0;
                pagos.ConsultaCuentaBanco(sesion, banco, ref cuenta);

                txtBanCuenta.Text = cuenta;
                txtPag_RefBancaria.Text = "";

                ((RadNumericTextBox)tabla.FindControl("txtNumOperacion")).Focus();
            }
            catch (Exception)
            {

            }
        }
        protected void RAM1_AjaxRequest1(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    case "panel":
                        Unit altura = (Unit)(Convert.ToDouble(HiddenHeight.Value) - 150);
                        RadPageViewDetalles.Height = altura;
                        RadPane1.Height = altura;
                        RadPane1.Width = RadPageViewDGenerales.Width;
                        RadSplitter1.Height = altura;
                        RadPageViewDGenerales.Height = altura;
                        RadSplitter2.Height = altura;
                        RadPane2.Height = altura;
                        RadPane2.Width = RadPageViewDGenerales.Width;
                        break;
                    case "Recargar":
                        Recargar();
                        break;
                    case "cancel":
                        Cancelar(txtMovimiento.Text);
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void Customvalidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                args.IsValid = (RadUpload1.InvalidFiles.Count == 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void RadAsyncUpload1_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            try
            {
                Label fileName = new Label();
                fileName.Text = e.File.FileName;
                NombreArchivo = fileName.Text;
                NombreHojaExcel = e.File.GetNameWithoutExtension().ToString();
                if (e.IsValid)
                {
                    ValidFiles.Visible = true;
                    ValidFiles.Controls.Add(fileName);
                }
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }

        protected void btnImportar_Click(object sender, EventArgs e)
        {
            string BloquearImporteMayorPago = ConfigurationManager.AppSettings["BloquearImporteMayorPago"].ToString();
            /*
             Excel row-column ref
            [0] = Numero de ficha
            [1] = Fecha de pago
            [2] = Numero de banco
            [3] = Numero de operacion
            [4] = Referencia Bancaria
            [5] = Importe
            [6] = Tipo de movimiento
            [7] = Serie
            [8] = Referencia
            [9] = Cheque
            [10] = Importe detalle
             */
            try
            {
                string path = Server.MapPath("~/App_Data/RadUploadTemp") + "\\" + NombreArchivo;

                if (File.Exists(path)) File.Delete(path);
                foreach (UploadedFile f in RadUpload1.UploadedFiles) f.SaveAs(path, true);

                string strConnection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0 xml;HDR=YES;IMEX=1;\"";
                OleDbConnection con = new OleDbConnection(strConnection);
                con.Open();

                DataTable dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                OleDbCommand cmd = new OleDbCommand("select * from [" + dt.Rows[0].ItemArray[2].ToString().Replace("'", "") + "]", con);

                OleDbDataAdapter dad = new OleDbDataAdapter
                {
                    SelectCommand = cmd
                };
                DataSet ds = new DataSet();
                dad.Fill(ds);
                con.Close();


                DataTable dtExcel = ds.Tables[0].Rows
                    .Cast<DataRow>()
                    .Where(row => !row.ItemArray.All(field => field is DBNull || string.IsNullOrWhiteSpace(field as string)))
                    .CopyToDataTable();


                RgGral.Rebind();
                RgGral.DataSource = null;
                dtDet.Clear();
                dtGral.Clear();

                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];

                var fichas = dtExcel.Rows.Cast<DataRow>()
                    .OrderBy(x => x[0])
                    .GroupBy(x => x[0])
                    .Select(g => g.First())
                    .ToList();

                //validaciones
                for (int i = 0; i < fichas.Count; i++)
                {
                    if ((i + 1) != int.Parse(fichas[i][0].ToString()))
                    {
                        Alerta("El número de fichas debe ser consecutivo empezando desde el #1");
                        return;
                    }
                }

                foreach (DataRow row in fichas)
                {
                    int Banco = 0;
                    string RefBancaria = "";
                    int NumeroFicha = 0;

                    var rowsByFicha = dtExcel.Rows.Cast<DataRow>()
                        .Where(x => x[0].ToString() == row[0].ToString())
                        .ToList();

                    if (rowsByFicha
                        .Select(s => s[1].ToString())
                        .Distinct()
                        .Count() > 1)
                    {
                        Alerta("No puede haber fechas diferentes para la ficha #" + row[0]);
                        return;
                    }

                    if (ConfigurationManager.AppSettings["ReferenciaBancaria"].ToString() == "S")
                    {

                        if (row[4].ToString().Trim() != "")
                        {
                            Banco = Convert.ToInt32(row[2].ToString().Trim());
                            RefBancaria = row[4].ToString().Trim();
                            NumeroFicha = Convert.ToInt32(row[0].ToString().Trim());
                            CN_CapPago pagos = new CN_CapPago();
                            sesion = (Sesion)Session["Sesion" + Session.SessionID];
                            string ValidaReferencia = "";
                            pagos.ValidaReferenciaBancaria(sesion, Banco, RefBancaria, ref ValidaReferencia);
                            if (ValidaReferencia != "")
                            {
                                Alerta(ValidaReferencia);
                                return;
                            }
                            //ReferenciaBancaria
                            if (ConfigurationManager.AppSettings["ReferenciaBancaria"].ToString() == "S")
                            {
                                var Ar_drRevisa = dtExcel.Rows.Cast<DataRow>()
                                .Where(x => x[4].ToString() == RefBancaria && Convert.ToInt32(x[2].ToString()) == Banco
                                && Convert.ToInt32(x[0].ToString()) != NumeroFicha);

                                if (Ar_drRevisa.Select(s => s[4].ToString())
                                                 .Distinct()
                                                 .Count() > 0)
                                {
                                    Alerta("No se puede Repetir la Referencia Bancaria con el mismo Banco.");
                                    return;
                                }
                            }
                        }
                        else
                        {
                            this.Alerta("Favor de capturar la Referencia Bancaria");
                            return;
                        }
                    }

                    if (rowsByFicha
                        .Select(s => s[5].ToString())
                        .Distinct()
                        .Count() > 1)
                    {
                        Alerta("No puede haber importes diferentes para la ficha #" + row[0]);
                        return;
                    }
                }
                //fin validaciones



                var bankList = new List<Comun>();
                string bankId;
                string bank;
                string RefId;
                string Id_Mov;
                string account = "";
                new CD__Comun().LlenaCombo(1, sesion.Id_Emp, sesion.Id_Cd_Ver, "spCatBanco_Combo", sesion.Emp_Cnx, ref bankList);
                var capPago = new CN_CapPago();
                double importeTotal = 0;

                int countFichas = 1;
                foreach (DataRow row in fichas)
                {
                    bankId = row[2].ToString();
                    RefId = row[4].ToString();
                    Id_Mov = row[6].ToString();

                    if (!bankList.Any(x => x.Id.ToString() == bankId))
                    {
                        Alerta("El id de banco #" + bankId + " no existe");
                        return;
                    }

                    capPago.ConsultaCuentaBanco(sesion, int.Parse(bankId), ref account);
                    bank = bankList
                        .First(x => x.Id.ToString() == bankId)
                        .Descripcion;

                    // RBM Mayo 2025
                    // Se valida el banco contra la referencia proporcionada
                    CN_CapPago pagos = new CN_CapPago();
                    int Id_Cd = 0;
                    int validador = 0;
                    pagos.ValidaSucursal(sesion, ref Id_Cd);
                    if (Id_Cd > 0)
                    {
                        if (Id_Mov != txtMovimiento.Text)
                        {
                            Alerta("No se puede cargar pagos de tipo 42 al tipo de pago 1, favor de cambiar el tipo de pago seleccionado.");
                            return;
                        }

                        if (bankId != "42")
                        {
                            validarRefBanco(int.Parse(bankId), account, RefId, ref validador);

                            if (validador == 0)
                            {
                                Alerta("La referencia de banco #" + RefId + ", no corresponde al id del banco #" + bankId);
                                return;
                            }
                        }
                    }


                    dtGral.Rows.Add(countFichas, row[0], row[1], row[2], bank, row[5], account, row[3], row[4]);
                    importeTotal += double.Parse(row[5].ToString());
                    countFichas++;
                }

                RgGral.Rebind();
                RgGral.DataSource = dtGral;
                txtImporte.Text = importeTotal.ToString();


                string tipoMovimiento;
                Factura ficha_f;
                NotaCargo ficha_nc;
                var catCliente = new CN_CatCliente();
                DbCentro centro;
                int verificadorCd = 0;
                int tipo_cdc = 0;
                string movStr = "";
                string _ref = "";
                string fac_folioFiscal = "";
                int idTer = 0;
                DateTime doc_fecha;
                int id_cte = 0;
                string cte_nombre = "";
                string docEstatus = "";
                double docImporte = 0;
                double docPagado = 0;
                double total = 0;
                double importeReal = 0;

                int countDet = 1;
                foreach (DataRow row in dtExcel.Rows)
                {

                    movStr = "";
                    idTer = 0;
                    id_cte = 0;
                    docEstatus = "";
                    docImporte = 0;
                    docPagado = 0;
                    cte_nombre = "";
                    fac_folioFiscal = "";
                    _ref = "";
                    verificadorCd = 0;
                    doc_fecha = DateTime.Today;
                    tipo_cdc = 0;
                    tipoMovimiento = row[6].ToString();
                    double importeExcel = double.Parse(row[10].ToString());
                    importeReal = 0;

                    catCliente.ConsultaTipoCDC(sesion.Id_Cd_Ver, ref tipo_cdc, sesion.Emp_Cnx);
                    centro = new DbCentro();
                    capPago.ConsultarCentro(sesion.Id_Emp, row[7].ToString(), ref centro, Emp_CnxCob, tipo_cdc);
                    verificadorCd = centro.Id_Cd;

                    var Ar_drRevisadet = dtExcel.Rows.Cast<DataRow>()
                     .Where(x => x[7].ToString() == row[7].ToString() && Convert.ToInt32(x[8].ToString()) == Convert.ToInt32(row[8].ToString()));

                    if (Ar_drRevisadet.Select(s => s[8].ToString())
                                     .Count() > 1)
                    {
                        Alerta("No se puede Repetir la Serie y Referencia del Movimiento, Favor de revisar el Documento.");
                        return;
                    }

                    if (tipoMovimiento == "1")
                    {
                        ficha_f = new Factura
                        {
                            Id_Emp = sesion.Id_Emp,
                            Id_Cd = verificadorCd,
                            Serie = row[7].ToString(),
                            Id_FacSerie = row[8].ToString()
                        };

                        int verificador = 0;
                        capPago.ConsultaPagoFicha(ref ficha_f, Emp_CnxCob, ref verificador);
                        if (verificador == 1)
                        {
                            movStr = "Factura";
                            importeReal = ficha_f.Fac_Saldo.Value - ficha_f.Fac_Pagado.Value;
                            if (importeExcel > importeReal && BloquearImporteMayorPago == "1")
                            {
                                Alerta("Movimiento no puede ser mayor a " + importeReal + "; imposible aplicarle un pago " + movStr + " " + row[7] + " " + row[8]);
                                return;
                            }
                            _ref = ficha_f.Id_FacSerie;
                            fac_folioFiscal = ficha_f.Fac_FolioFiscal;
                            idTer = ficha_f.Id_Ter.Value;
                            doc_fecha = ficha_f.Fac_Fecha;
                            id_cte = ficha_f.Id_Cte;
                            cte_nombre = ficha_f.Cte_NomComercial;
                            docEstatus = ficha_f.Fac_Estatus;
                            docImporte = ficha_f.Fac_Saldo.Value;
                            docPagado = ficha_f.Fac_Pagado.Value;
                        }
                        else
                        {
                            Alerta("No se localizó el registro(" + row[7].ToString() + "" + row[8].ToString() + "). </br> Favor de verificar la informacion.");
                            return;
                        }
                    }
                    else if (tipoMovimiento == "2")
                    {
                        ficha_nc = new NotaCargo
                        {
                            Id_Emp = sesion.Id_Emp,
                            Id_Cd = verificadorCd,
                            Serie = row[7].ToString(),
                            Id_Nca = int.Parse(row[8].ToString())
                        };

                        int verificador = 0;
                        capPago.ConsultaPagoNotaFicha(ref ficha_nc, Emp_CnxCob, ref verificador);
                        if (verificador == 1)
                        {
                            movStr = "Nota de cargo";
                            importeReal = ficha_nc.Importe;
                            if (importeExcel > importeReal && BloquearImporteMayorPago == "1")
                            {
                                Alerta("Movimiento no puede ser mayor a " + ficha_nc.Importe + "; imposible aplicarle un pago " + movStr + " " + row[7] + " " + row[8]);
                                return;
                            }
                            idTer = ficha_nc.Id_Ter;
                            doc_fecha = ficha_nc.Nca_Fecha;
                            id_cte = ficha_nc.Id_Cte;
                            cte_nombre = ficha_nc.Cte_NomComercial;
                            docEstatus = ficha_nc.Nca_Estatus;
                            //docImporte = ficha_nc.Nca_Total;
                            docImporte = ficha_nc.Importe; //preguntar a Rafa
                            docPagado = ficha_nc.Nca_Pagado;
                            fac_folioFiscal = ficha_nc.Nca_FolioFiscal;
                            _ref = ficha_nc.Id_Nca.ToString();
                        }
                    }



                    if (docEstatus == "B")
                    {
                        Alerta("Movimiento se encuentra cancelado; imposible aplicarle un pago " + movStr + " " + row[7] + " " + row[8]);
                        return;
                    }
                    else if (docEstatus == "C")
                    {
                        Alerta("Movimiento se encuentra en estatus capturado; imposible aplicarle un pago " + movStr + " " + row[7] + " " + row[8]);
                        return;
                    }

                    if ((Math.Round(docImporte) - docPagado) <= 0)
                    {
                        Alerta("Movimiento no tiene saldo positivo; imposible aplicarle un pago " + movStr + " " + row[7] + " " + row[8]);
                        return;
                    }



                    if (doc_fecha.Date > rdFechaPago.SelectedDate.Value.Date && ConfigurationManager.AppSettings["Pagos_ValidarFechaMinimaFactura"].ToString() == "true")
                    {
                        Alerta("La fecha de creación del documento #" + _ref + " es mayor a la fecha de pago. Favor de quitar del listado para continuar.");
                        return;
                    }
                    else
                    {
                        dtDet.Rows.Add(
                            countDet,
                            row[6].ToString(),
                            movStr,
                            _ref,
                            fac_folioFiscal,
                            idTer,
                            row[7].ToString(),
                            doc_fecha,
                            id_cte,
                            cte_nombre,
                            row[0].ToString(),
                            row[9].ToString(),
                            row[10].ToString(),
                            docEstatus,
                            docImporte,
                            docPagado,
                            verificadorCd, importeReal);

                        total += double.Parse(row[10].ToString());

                        countDet++;
                    }
                }

                RgDet.Rebind();
                RgDet.DataSource = dtDet;

                Alerta("Se importaron los registros");
                txtTotal.Text = total.ToString();
            }
            catch (Exception ex) { Alerta(ex.Message.Replace("'", "")); }
        }

        private void validarRefBanco(int BankId, string Cuenta, string RefId, ref int validador)
        {
            string sCuenta = "";
            string ReferenciaCuenta = "";
            sCuenta = Cuenta.Replace("-", "");
            string Referencia = RefId.Substring(0, 3);

            ReferenciaCuenta = sCuenta.Substring(sCuenta.Length - 3, 3);
            if (Referencia == ReferenciaCuenta)
            {
                validador = 1;
            }
            else
            {
                validador = 0;
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

        protected void txtPag_RefBancaria_TextChanged(object sender, EventArgs e)
        {
            CN_CapPago pagos = new CN_CapPago();
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];
            int Id_Cd = 0;

            pagos.ValidaSucursal(sesion, ref Id_Cd);
            if (Id_Cd > 0)
            {
                if (txtMovimiento.Text == "42")
                {
                    return;
                }
                else
                {
                    RadTextBox combo = (RadTextBox)sender;
                    RadTextBox rgtxtReferencia = sender as RadTextBox;
                    Telerik.Web.UI.GridTableCell tabla = (Telerik.Web.UI.GridTableCell)combo.Parent;
                    RadNumericTextBox txtIdBanco = (RadNumericTextBox)tabla.FindControl("rgtxtIdBanco");
                    RadTextBox txtBanCuenta = (RadTextBox)tabla.FindControl("txtBanCuenta");
                    RadComboBox cmbBanco = (RadComboBox)tabla.FindControl("cmbBancos");
                    RadDatePicker rdFecha = (RadDatePicker)tabla.FindControl("rdFecha");
                    RadNumericTextBox txtImporte = (RadNumericTextBox)tabla.FindControl("rgtxtImporte");
                    RadTextBox txtNumOperacion = (RadTextBox)tabla.FindControl("txtNumOperacion");
                    string Referencia = rgtxtReferencia.Text;
                    string cuenta = "";
                    int Id_Ban = 0;
                    pagos.ConsultaReferenciaBanco(sesion, Referencia, ref Id_Ban, ref cuenta);
                    if (Id_Ban == 0)
                    {
                        Alerta("No existe banco para la referencia bancaria proporcionada, intente de nuevo.");
                        rgtxtReferencia.Text = "";
                        txtBanCuenta.Text = "";
                        txtIdBanco.Text = "";
                        cmbBanco.SelectedValue = "-1";
                        rgtxtReferencia.Focus();
                        return;
                    }

                    txtBanCuenta.Text = cuenta;
                    pagos.spCatBanco_Combo(sesion, Id_Ban, ref cuenta);
                    txtIdBanco.Text = Id_Ban.ToString();
                    cmbBanco.SelectedValue = Id_Ban.ToString();
                    cmbBanco.Text = cuenta;
                    rdFecha.Focus();
                }
            }
        }
        protected void cmbMovimiento_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            CN_CapPago pagos = new CN_CapPago();
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];
            int Id_Cd = 0;


            pagos.ValidaSucursal(sesion, ref Id_Cd);
            if (Id_Cd > 0)
            {
                if (RgGral.Items.Count > 0)
                {
                    RadConfirm("Si cambia el tipo de movimiento, se eliminarán todos los datos cargados. ¿Desea continuar con el cambio de movimiento?");
                }
            }
        }
        protected void cmbMovimiento_DataBinding(object sender, EventArgs e)
        {
            try
            {
                RadComboBox rgcmbMov = sender as RadComboBox;
                Label fichaNum;
                for (int x = 0; x < RgGral.Items.Count; x++)
                {
                    fichaNum = RgGral.Items[x]["Pag_ficha"].FindControl("lblFicha") as Label;
                    rgcmbMov.Items.Add(new RadComboBoxItem(fichaNum.Text, fichaNum.Text));
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        #endregion
        #region Funciones
        private void Guardar()
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                RadPageViewDGenerales.Focus();


                int verificador = 0;

                if (Request.QueryString["Ext"] != null)
                {
                    CN_CapPago cn_cappago = new CN_CapPago();
                    Pago pag = new Pago();
                    pag.Id_Emp = session.Id_Emp;
                    pag.Id_Cd = session.Id_Cd_Ver;

                    cn_cappago.PermitirExtemporaneo(pag, session.Emp_Cnx, ref verificador);

                    if (!Convert.ToBoolean(verificador))
                    {
                        Alerta("Ya se ha efectuado el cierre extemporáneo de pagos, no es posible guardar el pago extemporáneo");
                        return;
                    }
                }

                if (dtGral.Rows.Count < 1)
                {
                    Alerta("Favor de capturar al menos un pago");
                    return;
                }
                if (dtDet.Rows.Count < 1)
                {
                    Alerta("Favor de capturar el detalle");
                    return;
                }

                //valida fecha de ficha o sea menor a la fecha de factura   
                if (ConfigurationManager.AppSettings["Pagos_ValidarFechaMinimaFactura"].ToString() == "true")
                {
                    for (int i = 0; i < dtGral.Rows.Count; i++)
                    {
                        string numOperacion = dtGral.Rows[i]["Pag_ficha"].ToString();

                        if (string.IsNullOrEmpty(numOperacion))
                            continue;

                        DateTime fechaFicha = Convert.ToDateTime(dtGral.Rows[i]["Pag_Fecha"]);

                        for (int j = 0; j < dtDet.Rows.Count; j++)
                        {
                            string numFactura = dtDet.Rows[j]["Pag_Numero"].ToString();


                            if (numFactura != numOperacion)
                                continue;

                            DateTime fechaFactura =
                                Convert.ToDateTime(dtDet.Rows[j]["Doc_Fecha"]);
                            string referencia =
                                    dtDet.Rows[j]["Doc_Referencia"].ToString();
                            if (fechaFactura.Date > fechaFicha.Date)
                            {


                                string mensaje =
                                    $"No se puede guardar. La ficha con No.: {numOperacion} " +
                                    $"tiene fecha {fechaFicha:dd/MM/yyyy} y existe la factura {referencia} " +
                                    $"con fecha {fechaFactura:dd/MM/yyyy}.";

                                Alerta(mensaje);

                                return;
                            }

                            if (fechaFactura.Date > rdFechaPago.SelectedDate.Value.Date)
                            {

                                string mensaje =
                                    $"No se puede guardar. El pago " +
                                    $"tiene fecha {rdFechaPago.SelectedDate.Value.Date:dd/MM/yyyy} y existe la factura {referencia} " +
                                    $"con fecha {fechaFactura:dd/MM/yyyy}.";

                                Alerta(mensaje);

                                return;
                            }
                        }
                    }
                }


                CalcularImporteFichas();
                CalcularTotalDetalle();

                Pago pago = new Pago();
                pago.Id_Emp = session.Id_Emp;
                pago.Id_Cd = session.Id_Cd_Ver;
                pago.Tipo = Convert.ToInt32(cmbTipo.SelectedValue);
                pago.Pag_Fecha = rdFechaPago.SelectedDate.Value;
                pago.Id_Tmov = !string.IsNullOrEmpty(cmbMovimiento.SelectedValue) ? Convert.ToInt32(cmbMovimiento.SelectedValue) : 0;
                pago.Pag_Importe = !string.IsNullOrEmpty(txtImporte.Text) ? Convert.ToDouble(txtImporte.Text) : 0;
                pago.Pag_Total = !string.IsNullOrEmpty(txtTotal.Text) ? Convert.ToDouble(txtTotal.Text) : 0;
                pago.Id_U = session.Id_U;
                pago.Pag_Estatus = "I";   //Se cambia a impreso
                pago.Pag_Extemporaneo = Request.QueryString["Ext"] != null ? true : false;

                List<Banco_Ficha> list_fichas = new List<Banco_Ficha>();
                List<Banco_Ficha> list_fichasExterno = new List<Banco_Ficha>();
                Banco_Ficha ficha = default(Banco_Ficha);
                for (int x = 0; x < dtGral.Rows.Count; x++)
                {
                    ficha = new Banco_Ficha();
                    ficha.Pag_Ficha = !string.IsNullOrEmpty(dtGral.Rows[x]["Pag_ficha"].ToString()) ? Convert.ToInt32(dtGral.Rows[x]["Pag_ficha"]) : 0;
                    ficha.Id_Ban = !string.IsNullOrEmpty(dtGral.Rows[x]["Pag_Banco"].ToString()) ? Convert.ToInt32(dtGral.Rows[x]["Pag_Banco"]) : 0;
                    ficha.Pag_Fecha = !string.IsNullOrEmpty(dtGral.Rows[x]["Pag_Fecha"].ToString()) ? Convert.ToDateTime(dtGral.Rows[x]["Pag_Fecha"]) : DateTime.MinValue;
                    ficha.Pag_Importe = !string.IsNullOrEmpty(dtGral.Rows[x]["Pag_Importe"].ToString()) ? Convert.ToDouble(dtGral.Rows[x]["Pag_Importe"]) : 0;
                    ficha.Pag_NumOperacion = !string.IsNullOrEmpty(dtGral.Rows[x]["Pag_NumOperacion"].ToString()) ? Convert.ToString(dtGral.Rows[x]["Pag_NumOperacion"]) : "";
                    ficha.Pag_RefBancaria = !string.IsNullOrEmpty(dtGral.Rows[x]["Pag_RefBancaria"].ToString()) ? Convert.ToString(dtGral.Rows[x]["Pag_RefBancaria"]) : "";

                    list_fichas.Add(ficha);
                    list_fichasExterno.Add(ficha);
                }
                List<PagoDet> list_pagos = new List<PagoDet>();
                List<PagoDet> list_pagosExterno = new List<PagoDet>();
                PagoDet pagodet = new PagoDet();
                for (int x = 0; x < dtDet.Rows.Count; x++)
                {
                    pagodet = new PagoDet();
                    pagodet.Mov = !string.IsNullOrEmpty(dtDet.Rows[x]["Pag_Movimiento"].ToString()) ? Convert.ToInt32(dtDet.Rows[x]["Pag_Movimiento"]) : 0;
                    pagodet.Serie = dtDet.Rows[x]["Serie"];
                    pagodet.Ref = dtDet.Rows[x]["Doc_Referencia"].ToString();
                    pagodet.Fac_FolioFiscal = dtDet.Rows[x]["Fac_FolioFiscal"].ToString();
                    pagodet.Ficha = !string.IsNullOrEmpty(dtDet.Rows[x]["Pag_Numero"].ToString()) ? Convert.ToInt32(dtDet.Rows[x]["Pag_Numero"]) : 0;
                    pagodet.Cheque = dtDet.Rows[x]["Pag_Cheque"].ToString();
                    pagodet.Importe = !string.IsNullOrEmpty(dtDet.Rows[x]["Pag_Importe"].ToString()) ? Convert.ToDouble(dtDet.Rows[x]["Pag_Importe"]) : 0;
                    pagodet.Pag_Id_cd = Convert.ToInt32(dtDet.Rows[x]["Cdi"]);
                    pagodet.Pag_Id_Ter = Convert.ToInt32(dtDet.Rows[x]["Id_Terr"]);
                    pagodet.Pag_Fac_Fecha = Convert.ToDateTime(dtDet.Rows[x]["Doc_Fecha"]);
                    pagodet.Pag_Id_cte = Convert.ToInt32(dtDet.Rows[x]["Id_Cte"]);
                    pagodet.Pag_Cte_Nombre = dtDet.Rows[x]["Cte_Nombre"].ToString();
                    list_pagos.Add(pagodet);
                    list_pagosExterno.Add(pagodet);
                }

                double total_ficha;
                foreach (Banco_Ficha bf in list_fichas)
                {
                    total_ficha = 0;
                    foreach (PagoDet pd in list_pagos)
                    {
                        if (pd.Ficha == bf.Pag_Ficha)
                        {
                            total_ficha += pd.Importe;
                        }
                    }
                    //if ((total_ficha - bf.Pag_Importe) < -0.01 || (total_ficha - bf.Pag_Importe) > 0.01)
                    if ((Math.Round(total_ficha, 2) - Math.Round(bf.Pag_Importe, 2)) != 0)
                    {
                        //RAM1.ResponseScripts.Add("AbrirVentana_PagoDif();");
                        string idPagina = Request.Params["IdPagina"];
                        RAM1.ResponseScripts.Add($"AbrirVentana_PagoDif('{idPagina}');");
                        return;
                    }
                }

                //if (txtImporte.Value - txtTotal.Value < -0.01 || txtImporte.Value - txtTotal.Value > 0.01)
                if (Math.Round(txtImporte.Value.Value, 2) != Math.Round(txtTotal.Value.Value, 2))
                {
                    Alerta("No puede haber diferencia en importe de fichas y Total en detalle, favor de revisarlos antes de continuar");
                    return;
                }

                CN_CapPago clsCapPago = new CN_CapPago();
                verificador = -1;
                if (HF_ID.Value == "")
                {
                    if (!_PermisoGuardar)
                    {
                        Alerta("No tiene permisos para grabar");
                        return;
                    }

                    clsCapPago.InsertarPago(pago, list_fichas, list_pagos, session.Emp_Cnx, ref verificador);
                    if (verificador > 0)
                    {
                        InsertarPagosExternos(pago, list_fichasExterno, list_pagosExterno, verificador, null);
                        RAM1.ResponseScripts.Add("CloseAlert('Se genero el pago <b>#" + verificador + "</b>');");
                    }
                    else
                    {
                        Alerta("Ocurrió un error al intentar guardar el pago");
                    }
                }
                else
                {
                    if (!_PermisoModificar)
                    {
                        Alerta("No tiene permisos para modificar");
                        return;
                    }
                    pago.Id_Pag = Convert.ToInt32(HF_ID.Value);

                    List<int> centros = new List<int>();
                    clsCapPago.ModificarPago(pago, list_fichas, list_pagos, session.Emp_Cnx, ref verificador, ref centros);
                    if (verificador == 1)
                    {
                        ModificarPagosExternos(centros, pago, list_fichasExterno, list_pagosExterno, pago.Id_Pag);
                        HiddenRebind.Value = "1";
                        RAM1.ResponseScripts.Add("CloseAlert('El pago se guardo correctamente en estatus IMPRESO');");
                    }
                    else
                        Alerta("Ocurrió un error al intentar guardar los cambios");
                }
                //if(verificador > 0)
                //    RAM1.ResponseScripts.Add("CloseAlert('Se genero el pago <b>#" + verificador + "</b>');");
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                    ws.GuardarPagoExterno(Serialize(pago_cd), Serialize(list_fichas_cd), Serialize(list_pagos_cd), Emp_CnxCob, session.Emp_Cnx, Tipo_CDC);
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
                ficha_cd.Pag_RefBancaria = bf.Pag_RefBancaria;
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
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            Funciones funcion = new Funciones();
            rdFechaPago.SelectedDate = funcion.GetLocalDateTime(Sesion.Minutos);
            rdFechaPago.Focus();
            BloquearFechaFutura();
            CargarTipo();
            CargarMovimientos();
            cmbTipo.SelectedIndex = cmbTipo.FindItemIndexByValue("1");
            cmbTipo.Enabled = false;
            GetListGrl();
            GetListDet();
            if (Request.QueryString["id"] != null && Request.QueryString["id"] != "-1")
            {
                txtClave.Text = Request.QueryString["id"];
                HF_ID.Value = Request.QueryString["id"];
                cargarPago();
            }
            else
            {
                txtClave.Text = MaximoId();
                RgGral.Rebind();
                RgDet.Rebind();
            }
            _PermisoGuardar = Convert.ToBoolean(Request.QueryString["PermisoGuardar"]);
            _PermisoModificar = Convert.ToBoolean(Request.QueryString["PermisoModificar"]);
            _PermisoEliminar = Convert.ToBoolean(Request.QueryString["PermisoEliminar"]);
            _PermisoImprimir = Convert.ToBoolean(Request.QueryString["PermisoImprimir"]);
            ValidarPermisos();

            if (Request.QueryString["Ext"] != null)
            {
                this.Title = "Pagos extemporaneos";
                rdFechaPago.Enabled = false;
                rdFechaPago.DbSelectedDate = Sesion.CalendarioIni.AddDays(-1);
                CN_CapPago cn_cappago = new CN_CapPago();
                Pago pag = new Pago();
                pag.Id_Emp = Sesion.Id_Emp;
                pag.Id_Cd = Sesion.Id_Cd_Ver;
                int verificador = 0;
                cn_cappago.PermitirExtemporaneo(pag, Sesion.Emp_Cnx, ref verificador);
                if (!Convert.ToBoolean(verificador))
                {
                    RAM1.ResponseScripts.Add("CloseAlert('Ya se ha efectuado el cierre extemporáneo de pagos, no es posible capturar el pago extemporáneo');");
                }
            }
        }

        private void BloquearFechaFutura()
        {

            if (ConfigurationManager.AppSettings["Pagos_BloquearInputFechaFutura"].ToString() == "true")
            {
                rdFechaPago.MaxDate = DateTime.Today;
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

                pago.Id_Emp = Sesion.Id_Emp;
                pago.Id_Cd = Sesion.Id_Cd_Ver;
                pago.Id_Pag = Convert.ToInt32(HF_ID.Value);
                CN_CapPago cn_cappago = new CN_CapPago();
                cn_cappago.ConsultaPago(ref pago, ref list_fichas, ref list_pagos, Sesion.Emp_Cnx);

                cmbTipo.SelectedIndex = cmbTipo.FindItemIndexByValue(pago.Tipo.ToString());
                rdFechaPago.DbSelectedDate = pago.Pag_Fecha.Year == 1 ? (DateTime?)null : pago.Pag_Fecha;
                cmbMovimiento.SelectedIndex = cmbMovimiento.FindItemIndexByValue(pago.Id_Tmov.ToString());
                txtMovimiento.Text = pago.Id_Tmov.ToString();
                txtImporte.Text = pago.Pag_Importe.ToString();
                txtTotal.Text = pago.Pag_Total.ToString();

                for (int x = 0; x < list_fichas.Count; x++)
                {
                    dtGral.Rows.Add(
                        x + 1,
                        list_fichas[x].Pag_Ficha,
                        list_fichas[x].Pag_Fecha,
                        list_fichas[x].Id_Ban,
                        list_fichas[x].Ban_Nombre,
                        list_fichas[x].Pag_Importe,
                        list_fichas[x].Ban_Cuenta,
                        list_fichas[x].Pag_NumOperacion,
                        list_fichas[x].Pag_RefBancaria
                        );
                }
                RgGral.Rebind();
                for (int x = 0; x < list_pagos.Count; x++)
                {
                    dtDet.Rows.Add(
                        x + 1,
                        list_pagos[x].Mov,
                        list_pagos[x].MovStr,
                        list_pagos[x].Ref,
                        list_pagos[x].Fac_FolioFiscal,
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
                        list_pagos[x].Importe
                        );
                }
                RgDet.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
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
                rdFechaPago.SelectedDate = funcion.GetLocalDateTime(Sesion.Minutos);
                dtDet.Rows.Clear();
                dtGral.Rows.Clear();
                cmbMovimiento.SelectedIndex = 0;
                cmbMovimiento.Text = cmbMovimiento.Items[0].Text;
                txtMovimiento.Value = null;
                txtImporte.Value = 0;
                txtTotal.Value = 0;
                RadTabStrip1.Tabs[0].Selected = true;
                RadMultiPage1.PageViews[0].Selected = true;
                RgDet.Rebind();
                RgGral.Rebind();
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
            txtImporte.Text = importe.ToString();
        }
        private void CalcularTotalDetalle()
        {
            double importe = 0;
            for (int x = 0; x < dtDet.Rows.Count; x++)
                importe += (Convert.ToDouble(dtDet.Rows[x]["Pag_Importe"]));

            txtTotal.Text = importe.ToString();
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
                dtGral.Columns.Add("Pag_NumOperacion", System.Type.GetType("System.String"));
                dtGral.Columns.Add("Pag_RefBancaria", System.Type.GetType("System.String"));
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
            string NumOperacion = "";
            String RefBancaria = "";
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
            RefBancaria = ((RadTextBox)gi.FindControl("txtPag_RefBancaria")).Text;
            Importe = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("rgtxtImporte")).Text);
            NumOperacion = ((RadTextBox)gi.FindControl("txtNumOperacion")).Text;


            if (ConfigurationManager.AppSettings["ReferenciaBancaria"].ToString() == "S")
            {
                if (Banco == 42) RefBancaria = "";

                if (RefBancaria != "")
                {
                    CN_CapPago pagos = new CN_CapPago();
                    Sesion sesion = new Sesion();
                    sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    string ValidaReferencia = "";
                    pagos.ValidaReferenciaBancaria(sesion, Banco, RefBancaria, ref ValidaReferencia);
                    if (ValidaReferencia != "")
                    {
                        e.Canceled = true;
                        Alerta(ValidaReferencia);
                        ((RadTextBox)gi.FindControl("txtPag_RefBancaria")).Focus();
                        return;
                    }

                    DataRow[] Ar_dr, Ar_drRevisa;

                    Ar_drRevisa = dtGral.Select("Pag_RefBancaria='" + RefBancaria + "' and Pag_Banco='" + Banco + "' and rgGralId <>'" + rgGralId + "'");
                    if (Ar_drRevisa.Length > 0)
                    {
                        e.Canceled = true;
                        this.Alerta("No se puede repetir la referencia Bancaria con el mismo Banco");
                        ((RadTextBox)gi.FindControl("txtPag_RefBancaria")).Focus();
                        return;
                    }
                }
                else
                {

                    if (Banco != 42)
                    {
                        e.Canceled = true;
                        this.Alerta("Favor de capturar la Referencia Bancaria");
                        ((RadTextBox)gi.FindControl("txtPag_RefBancaria")).Focus();
                        return;
                    }
                }
            }




            rgGralId = dtGral.Rows.Count + 1;
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            DataTable dt_temp = dtGral;
            dt_temp.Rows.Add(new object[] { rgGralId, ficha, Fecha, Banco, BancoStr, Importe, BancoCuenta, NumOperacion, RefBancaria });
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
            string Pag_NumOperacion = "";
            string RefBancaria = "";
            GridItem gi = e.Item;
            DataRow[] Ar_dr, Ar_drRevisa;
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
            RefBancaria = ((RadTextBox)gi.FindControl("txtPag_RefBancaria")).Text;
            Pag_NumOperacion = ((RadTextBox)gi.FindControl("txtNumOperacion")).Text;
            Importe = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("rgtxtImporte")).Text);
            rgGralId = Convert.ToInt32(((Label)gi.FindControl("lblGralId2")).Text);


            if (ConfigurationManager.AppSettings["ReferenciaBancaria"].ToString() == "S")
            {
                if (Banco == 42) RefBancaria = "";

                if (RefBancaria != "")
                {
                    CN_CapPago pagos = new CN_CapPago();
                    Sesion sesion = new Sesion();
                    sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    string ValidaReferencia = "";
                    pagos.ValidaReferenciaBancaria(sesion, Banco, RefBancaria, ref ValidaReferencia);
                    if (ValidaReferencia != "")
                    {
                        e.Canceled = true;
                        Alerta(ValidaReferencia);
                        ((RadTextBox)gi.FindControl("txtPag_RefBancaria")).Focus();
                        return;
                    }

                    Ar_drRevisa = dtGral.Select("Pag_RefBancaria='" + RefBancaria + "' and Pag_Banco='" + Banco + "'and rgGralId <>'" + rgGralId + "'");
                    if (Ar_drRevisa.Length > 0)
                    {
                        e.Canceled = true;
                        this.Alerta("No se puede repetir la referencia Bancaria con el mismo Banco");
                        ((RadTextBox)gi.FindControl("txtPag_RefBancaria")).Focus();
                        return;
                    }
                }
                else
                {

                    if (Banco != 42)
                    {
                        e.Canceled = true;
                        this.Alerta("Favor de capturar la Referencia Bancaria");
                        ((RadTextBox)gi.FindControl("txtPag_RefBancaria")).Focus();
                        return;
                    }


                }
            }



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
                Ar_dr[0]["Pag_NumOperacion"] = Pag_NumOperacion;
                Ar_dr[0]["Pag_RefBancaria"] = RefBancaria;
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
                dtDet.Columns.Add("Pag_ImporteReal", System.Type.GetType("System.Double"));
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
                string BloquearImporteMayorPago = ConfigurationManager.AppSettings["BloquearImporteMayorPago"].ToString();
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
                double importeReal = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("RadNumericTextBox1")).Text);
                Pag_Importe = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("rgtxtImporte")).Text);
                if (Pag_Importe > importeReal && BloquearImporteMayorPago == "1")
                {
                    Alerta("El importe no puede ser mayor a " + importeReal);
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

                if (rdFechaPago.SelectedDate.Value.Date < Doc_Fecha.Date && ConfigurationManager.AppSettings["Pagos_ValidarFechaMinimaFactura"].ToString() == "true")
                {
                    Alerta("La fecha del documento no puede ser mayor a la fecha del pago.");
                    e.Canceled = true;
                    return;
                }

                if (Math.Round(Convert.ToDouble(txtImporte.Text), 2) < (Math.Round(Convert.ToDouble(txtTotal.Text) + Pag_Importe, 2)))
                {
                    Alerta("El total del detalle excede al importe de las fichas");
                    e.Canceled = true;
                    return;
                }

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
                Cdi,importeReal
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
                string BloquearImporteMayorPago = ConfigurationManager.AppSettings["BloquearImporteMayorPago"].ToString();
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
                double Pag_ImporteReal = 0;
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
                Pag_ImporteReal = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("RadNumericTextBox1")).Value.HasValue ? ((RadNumericTextBox)gi.FindControl("RadNumericTextBox1")).Value.Value : -1);

                if (rdFechaPago.SelectedDate.Value.Date < Doc_Fecha.Date && ConfigurationManager.AppSettings["Pagos_ValidarFechaMinimaFactura"].ToString() == "true")
                {
                    Alerta("La fecha del documento no puede ser mayor a la fecha del pago.");
                    e.Canceled = true;
                    return;
                }

                if (Pag_Importe > Pag_ImporteReal && BloquearImporteMayorPago == "1")
                {
                    Alerta("El importe no puede ser mayor a " + Pag_ImporteReal);
                    e.Canceled = true;
                    return;
                }


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
                    if (Math.Round(Convert.ToDouble(txtImporte.Text), 2) < (totalSuma + Pag_Importe))
                    {
                        Alerta("El total del detalle excede al importe de las fichas");
                        e.Canceled = true;
                        return;
                    }
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
                    Ar_dr[0]["Pag_ImporteReal"] = Pag_ImporteReal;
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
                    for (int x = 0; x < RgGral.Items.Count; x++)
                    {
                        fichaNum = RgGral.Items[x]["Pag_ficha"].FindControl("lblFicha") as Label;
                        rgcmbNum.Items.Add(new RadComboBoxItem(fichaNum.Text, fichaNum.Text));
                    }


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
                    double Pag_ImporteReal = 0;

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
                    Pag_ImporteReal = Convert.ToDouble(((RadNumericTextBox)rgCte.Parent.FindControl("Pag_ImporteReal")).Text);

                    if (Math.Round(Convert.ToDouble(txtImporte.Text), 2) < (Math.Round(Convert.ToDouble(txtTotal.Text) + Pag_Importe, 2)))
                    {
                        AlertaFocus("El total del detalle excede al importe de las fichas", rgImporte.ClientID);
                        return;
                    }

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
                    Cdi,Pag_ImporteReal
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
                    DataTable dt = (DataTable)Session["dtGralPagos" + Request.Params["IdPagina"].ToString()];
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
        private void Recargar()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Funciones funcion = new Funciones();
                rdFechaPago.SelectedDate = funcion.GetLocalDateTime(Sesion.Minutos);
                dtDet.Rows.Clear();
                dtGral.Rows.Clear();
                txtImporte.Value = 0;
                txtTotal.Value = 0;
                RadTabStrip1.Tabs[0].Selected = true;
                RadMultiPage1.PageViews[0].Selected = true;
                RgDet.Rebind();
                RgGral.Rebind();
                HF_ID.Value = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Cancelar(string Id_Mov)
        {
            try
            {
                string Movimiento = "";
                if (Id_Mov == "42")
                {
                    Movimiento = "1";
                    txtMovimiento.Text = Movimiento;
                    cmbMovimiento.SelectedValue = Movimiento;

                }
                if (Id_Mov == "1")
                {
                    Movimiento = "42";
                    txtMovimiento.Text = Movimiento;
                    cmbMovimiento.SelectedValue = Movimiento;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region ErrorManager
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
        private void RadConfirm(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radconfirm('" + mensaje + "<br /><br />', confirmCallBackFn, 330, 150);");

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

        protected void rdFechaPago_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            if (ConfigurationManager.AppSettings["Pagos_ValidarFechaMinimaFactura"].ToString() == "false")
            {
                return;
            }

            if (!rdFechaPago.SelectedDate.HasValue)
                return;

            DateTime fechaSeleccionada = rdFechaPago.SelectedDate.Value;
            DateTime? fechaMaxGrid = null;


            //foreach (GridDataItem item in RgGral.Items)
            foreach (GridDataItem item in RgDet.Items)
            {
                Label lblFecha = item.FindControl("lblFecha") as Label;
                if (lblFecha != null && DateTime.TryParse(lblFecha.Text, out DateTime fechaRegistro))
                {
                    if (fechaMaxGrid == null || fechaRegistro > fechaMaxGrid)
                        fechaMaxGrid = fechaRegistro;
                }
            }

            if (fechaMaxGrid.HasValue && fechaSeleccionada < fechaMaxGrid.Value)
            {

                Alerta($"La fecha de pago no puede ser menor que la fecha registrada ({fechaMaxGrid.Value:dd/MM/yyyy}) en el detalle de facturas.");

                rdFechaPago.Clear();
            }
        }

        protected void RgGral_UpdateCommand(object source, GridCommandEventArgs e)
        {
            if (ConfigurationManager.AppSettings["Pagos_ValidarFechaMinimaFactura"].ToString() == "false")
            {
                return;
            }
            GridEditableItem item = (GridEditableItem)e.Item;

            RadDatePicker rdpFecha =
                (RadDatePicker)item.FindControl("rdFecha");

            RadNumericTextBox rgtxtFicha =
            (RadNumericTextBox)item.FindControl("rgtxtFicha");

            if (rdpFecha == null || rgtxtFicha == null)
                return;

            DateTime? nuevaFecha = rdpFecha.SelectedDate;

            if (!nuevaFecha.HasValue)
                return;



            string numOperacionFicha = rgtxtFicha.Text.Trim();

            foreach (GridDataItem facturaItem in RgDet.Items)
            {

                Label lblNumero =
                    (Label)facturaItem.FindControl("lblNumero");

                if (lblNumero == null)
                    continue;

                string numFactura = lblNumero.Text.Trim();


                if (numFactura != numOperacionFicha)
                    continue;

                Label lblFecha =
                    (Label)facturaItem.FindControl("lblFecha");

                Label lblReferencia =
                    (Label)facturaItem.FindControl("lblReferencia");

                if (lblFecha == null || lblReferencia == null)
                    continue;

                DateTime fechaFactura = DateTime.Parse(lblFecha.Text);


                string referencia = lblReferencia.Text;

                if (fechaFactura.Date > nuevaFecha.Value.Date)
                {
                    rdpFecha.Clear();

                    string mensaje =
                        $"La fecha de la ficha no puede ser menor que la factura {referencia} " +
                        $"(No. Ficha {numFactura}) con fecha {fechaFactura:dd/MM/yyyy}";

                    Alerta(mensaje);

                    e.Canceled = true;
                    return;
                }
                if (rdFechaPago != null && rdFechaPago.SelectedDate.HasValue)
                {
                    if (fechaFactura.Date > rdFechaPago.SelectedDate.Value.Date)
                    {
                        rdpFecha.Clear();

                        string mensaje =
                            $"La fecha de pago no puede ser menor que la factura {referencia} " +
                            $"(No. Ficha {numFactura}) con fecha {fechaFactura:dd/MM/yyyy}";

                        Alerta(mensaje);

                        e.Canceled = true;
                        return;
                    }
                }
            }
        }

        protected void RgGral_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (ConfigurationManager.AppSettings["Pagos_BloquearInputFechaFutura"].ToString() == "true")
            {
                if (e.Item is Telerik.Web.UI.GridEditableItem && e.Item.IsInEditMode)
                {
                    Telerik.Web.UI.GridEditableItem item = (Telerik.Web.UI.GridEditableItem)e.Item;

                    Telerik.Web.UI.RadDatePicker rdFecha =
                        (Telerik.Web.UI.RadDatePicker)item.FindControl("rdFecha");

                    if (rdFecha != null)
                    {
                        rdFecha.MaxDate = DateTime.Today;
                    }
                }
            }
        }
    }
}