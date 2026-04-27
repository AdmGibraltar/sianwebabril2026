using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using CapaEntidad;
using CapaNegocios;
using Telerik.Web.UI;
using System.Text.RegularExpressions;

namespace SIANWEB
{
    public partial class CapCartaPorte : System.Web.UI.Page
    {
        #region Variables 

        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        public static string param { get; set; }

        public static Int32 Tipo { get; set; }
        public static Int32 Id_Doc { get; set; }
        public static Int32 Id_Cte { get; set; }

        public DataTable objdtListaProd { get; set; }
        protected DataTable objdtTablaProd { get { if (ViewState["objdtTablaProd"] != null) { return (DataTable)ViewState["objdtTablaProd"]; } else { return objdtListaProd; } } set { ViewState["objdtTablaProd"] = value; } }

        RadComboBox cmbColonia;

        #endregion

        #region Eventos 
        protected void Page_Load(object sender, EventArgs e)
        {

            Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
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
                    Id_Doc = Convert.ToInt32(Page.Request.QueryString["Id_Doc"]);
                    Id_Cte = Convert.ToInt32(Page.Request.QueryString["Id_Cte"]);
                    txtDocumento.Text = Id_Doc.ToString();
                    txtCodigoPostal.Text = Page.Request.QueryString["CodigoPostal"].Trim();
                    txtColonia.Text = Page.Request.QueryString["Colonia"];
                    txtTipoDoc.Text = Page.Request.QueryString["TipoDoc"];
                    Inicializacion(Id_Doc, txtTipoDoc.Text);
                    // PreCargarInfo(Id_Doc, Id_Cte);
                }
            }
        }


        protected void chkDistancia_CheckedChanged(object sender, EventArgs e)
        {
            if (txtKm.Text == "")
            {
                Alerta("Favor de poporcionar un valor valido para la distancia");
                chkDistancia.Checked = false;
                return;
            }
            if (chkDistancia.Checked == true)
            {
                foreach (DataRow row in objdtTablaProd.Rows)
                {
                    row[4] = txtKm.Text;
                }
            }
            else
            {
                foreach (DataRow row in objdtTablaProd.Rows)
                {
                    row[4] = 1;
                }
            }
            objdtTablaProd.AcceptChanges();
            rgProductos.DataSource = this.objdtTablaProd;
            rgProductos.DataBind();
        }

        protected void chkCodigoPostal_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCodigoPostal.Checked == true)
            {
                foreach (DataRow row in objdtTablaProd.Rows)
                {
                    row[7] = txtCodigoPostal.Text;
                    row[8] = txtColonia.Text;
                }
            }
            else
            {
                foreach (DataRow row in objdtTablaProd.Rows)
                {
                    row[7] = string.Empty;
                    row[8] = string.Empty;
                }
            }
            objdtTablaProd.AcceptChanges();
            rgProductos.DataSource = this.objdtTablaProd;
            rgProductos.DataBind();
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {


                func_cerrarventana(param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;
                switch (btn.CommandName)
                {
                    case "save":
                        this.GeneraCartaPorte();
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void rgProductos_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgProductos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgProductos.DataSource = this.objdtTablaProd;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        protected void rgProductos_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            try
            {
                rgProductos.DataSource = this.objdtTablaProd;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgProductos_ItemDataBound(object sender, GridItemEventArgs e)
        {
            Sesion Sesion = new CapaEntidad.Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
            {
                GridEditableItem editItem = (GridEditableItem)e.Item;
                CargarDestinos(editItem);
                CargarMaterialPeligroso(editItem);
                CargarEmbalaje(editItem);


            }
        }

        protected void rgProductos_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem insertedItem = (GridEditableItem)e.Item;
                CartaPorte carta = new CartaPorte();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                GridDataItem item = (GridDataItem)e.Item;

                if ((insertedItem["Id_RemDet"].Controls[0] as TextBox).Text == "")
                {
                    carta.Id_RemDet = 0;
                }
                else
                {
                    carta.Id_RemDet = int.Parse((insertedItem["Id_RemDet"].Controls[0] as TextBox).Text);
                }
                carta.Id_Prd = Convert.ToInt64((insertedItem["Id_Prd"].FindControl("txtId_Prd") as RadNumericTextBox).Text);
                carta.Descripcion = (insertedItem["Descripcion"].FindControl("txtDescripcion") as RadTextBox).Text;

                carta.Cantidad = Convert.ToInt32((insertedItem["Cantidad"].FindControl("txtCantidad") as RadNumericTextBox).Text);
                if (double.Parse((insertedItem["Distancia"].FindControl("txtDistancia") as RadNumericTextBox).Text) <= 0)
                {
                    Alerta("El campo distancia debe contener un valor mayor a cero.");
                    e.Canceled = true;
                    return;
                }
                else
                {
                    carta.Distancia = double.Parse((insertedItem["Distancia"].FindControl("txtDistancia") as RadNumericTextBox).Text);
                }
                carta.Origen = (insertedItem["Origen"].FindControl("txtOrigen") as RadTextBox).Text;
                carta.Destino = (insertedItem["Destino"].FindControl("cmbDestino") as RadComboBox).SelectedValue;

                if ((insertedItem["CodigoPostal"].FindControl("txtCodigoPostal") as RadNumericTextBox).Text == "" || Convert.ToInt64((insertedItem["CodigoPostal"].FindControl("txtCodigoPostal") as RadNumericTextBox).Text) == 0)
                {
                    Alerta("El campo Código postal es obligatorio, para continuar favor de proporcionar uno valido, ejemplo: 64500.");
                    e.Canceled = true;
                    return;
                }
                else
                {
                    carta.CodigoPostal = (insertedItem["CodigoPostal"].FindControl("txtCodigoPostal") as RadNumericTextBox).Text;
                }


                carta.Colonia = (insertedItem["Colonia"].FindControl("cmbColonia") as RadComboBox).Text;

                carta.Fecha = (insertedItem["Fecha"].Controls[0] as RadDatePicker).SelectedDate.Value;

                CheckBox cb = (CheckBox)item["EsPeligroso"].Controls[0];
                carta.EsPeligroso = cb.Checked;

                if (carta.EsPeligroso == true)
                {

                    carta.ClaveMatPeligroso = (insertedItem["ClaveMatPeligroso"].FindControl("cmbClaveMatPeligroso") as RadComboBox).SelectedItem.Value;
                    carta.Embalaje = (insertedItem["Embalaje"].FindControl("cmbEmbalaje") as RadComboBox).Text;
                    carta.ClaveEmbalaje = (insertedItem["Embalaje"].FindControl("cmbEmbalaje") as RadComboBox).SelectedValue;
                }
                else
                {
                    carta.ClaveMatPeligroso = "";
                    carta.ClaveEmbalaje = "";
                    carta.Embalaje = "";

                }

                if (double.Parse((insertedItem["Kg"].FindControl("txtKg") as RadNumericTextBox).Text) <= 0)
                {
                    Alerta("El campo Peso Kg debe contener un valor mayor a cero.");
                    e.Canceled = true;
                    return;
                }
                else
                {
                    carta.Kg = double.Parse((insertedItem["Kg"].FindControl("txtKg") as RadNumericTextBox).Text);
                }


                DataRow[] Ar_dr;

                Ar_dr = objdtTablaProd.Select("Id_Prd='" + Convert.ToInt64(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Prd"]) + "'");
                if (Ar_dr.Length > 0)
                {
                    Ar_dr[0].BeginEdit();
                    Ar_dr[0]["Id_Prd"] = carta.Id_Prd;
                    Ar_dr[0]["Descripcion"] = carta.Descripcion;
                    Ar_dr[0]["Cantidad"] = carta.Cantidad;
                    Ar_dr[0]["Distancia"] = carta.Distancia;
                    Ar_dr[0]["Origen"] = carta.Origen;
                    Ar_dr[0]["Destino"] = carta.Destino;
                    Ar_dr[0]["CodigoPostal"] = carta.CodigoPostal;
                    Ar_dr[0]["Colonia"] = carta.Colonia;
                    Ar_dr[0]["Fecha"] = carta.Fecha.ToString("yyyy-MM-ddTHH:mm:ss");
                    Ar_dr[0]["Kg"] = carta.Kg;
                    Ar_dr[0]["EsPeligroso"] = carta.EsPeligroso;
                    Ar_dr[0]["ClaveMatPeligroso"] = carta.ClaveMatPeligroso;
                    Ar_dr[0]["ClaveEmbalaje"] = carta.ClaveEmbalaje;
                    Ar_dr[0]["Embalaje"] = carta.Embalaje;

                    objdtTablaProd.AcceptChanges();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void rgProductos_EditCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void CmbNombre_TextChanged(object sender, EventArgs e)
        {

        }

        protected void BuscarChofer_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                RAM1.ResponseScripts.Add("BuscarChofer(true);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void BuscarVehiculo_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                RAM1.ResponseScripts.Add("BuscarVehiculo(true);");
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
                ErrorManager();
                string cmd = e.Argument.ToString();
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CapRemision CN = new CN_CapRemision();
                switch (cmd)
                {
                    case "chofer":
                        Chofer chofer = new Chofer();
                        chofer.Folio = int.Parse(Session["Id_Buscar" + Session.SessionID].ToString());
                        chofer.Id_Emp = session.Id_Emp;
                        chofer.Id_Cd = session.Id_Cd;
                        CN.ConsultaChofer(ref chofer, session.Emp_Cnx);

                        txtNombre.Text = chofer.Nombre;
                        txtRfc.Text = chofer.Rfc;
                        txtLicencia.Text = chofer.NumLicencia;
                        Session.Remove("Id_Buscar" + Session.SessionID);
                        break;

                    case "vehiculo":
                        Vehiculo veh = new Vehiculo();
                        veh.PlacaVehiculo = Session["Id_Buscar" + Session.SessionID].ToString();
                        veh.Id_Emp = session.Id_Emp;
                        veh.Id_Cd = session.Id_Cd;

                        CN.ConsultaVehiculo(ref veh, session.Emp_Cnx);

                        txtVehiculo.Text = veh.Nom_Vehiculo;
                        txtPlaca.Text = veh.PlacaVehiculo;
                        txtModelo.Text = veh.ModeloVehiculo;
                        txtTipoRemolque.Text = veh.TipoRemolque;
                        txtPlacaRemolque.Text = veh.PlacaRemolque;
                        txtIdConfVehiculo.Text = veh.Id_ConfigVehiculo;
                        txtConfVehiculo.Text = veh.ConfiguracionVehiculo;
                        txtIdPermisoSCT.Text = veh.IdPermisoSCT;
                        txtPermisoSCT.Text = veh.PermisoSCT;
                        txtPermiso.Text = veh.NumPermiso;
                        txtAseguradora.Text = veh.Aseguradora;
                        txtPoliza.Text = veh.Poliza;

                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void cmbConfiguracion_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }

        protected void rgProductos_Load(object sender, EventArgs e)
        {
            rgProductos.InsertCommand += rgProductos_InsertCommand;
        }

        protected void rgProductos_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem item = e.Item as GridEditableItem;
                CheckBox cb = (CheckBox)item["EsPeligroso"].Controls[0];
                cb.AutoPostBack = true;
                cb.CheckedChanged += new System.EventHandler(cb_CheckedChanged);

                //Cargar Combo Colonias 

                cmbColonia = (RadComboBox)item.FindControl("cmbColonia");
            }
        }

        protected void cmbcodigopostal_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {

        }

        void cb_CheckedChanged(object sender, System.EventArgs e)
        {
            CheckBox check = (CheckBox)sender;
            GridEditableItem eitem = (GridEditableItem)check.NamingContainer;

            if (check.Checked == false)
            {

            }

        }

        protected void rgProductos_Init(object sender, EventArgs e)
        {
            rgProductos.InsertCommand += rgProductos_InsertCommand;

        }

        protected void rgProductos_InsertCommand(object sender, GridCommandEventArgs e)
        {
            object commandArguments = e.CommandArgument;
            bool canceled = e.Canceled;
            string commandName = e.CommandName;
            object source = e.CommandSource;
            GridItem item = e.Item;
        }

        protected void cmbPermisoSCT_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }

        protected void cmbClaveMatPeligroso_ClientSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }

        protected void cmbClaveMatPeligroso_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {

        }

        protected void cmbColonia_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {

        }

        protected void OnItemDataBoundHandler(object sender, GridItemEventArgs e)
        {

        }

        protected void cmbClaveMatPeligroso_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            //Find GridEditableItems when in Edit mode
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                //Get reference to item (i.e. Row)
                var item = e.Item as GridEditableItem;

                //Get reference to auto-generated RadCombobox in
                //specific column (in this case, a column called Title)
                var rcb = item["cmbClaveMatPeligroso"].Controls[0] as RadComboBox;
                if (rcb == null)
                    return;

                //Customize the RadCombobox properities
                rcb.OnClientSelectedIndexChanged = "onselectedindexchanged";
            }
        }

        protected void txtCodigoPostal_TextChanged(object sender, EventArgs e)
        {
            int cp = 0;
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];

            if (sender is RadNumericTextBox)
            {
                RadNumericTextBox tb = (RadNumericTextBox)sender;
                cp = int.Parse(tb.Text);
            }

            //RadComboBox cmbColonia = (RadComboBox)FindControl("cmbColonia");
            DataSet dsColonias = new DataSet();

            new CN_CapRemision().ConsultaColonias(sesion, ref dsColonias, cp);

            cmbColonia.DataSource = dsColonias.Tables[0];
            cmbColonia.DataTextField = "Descripcion";
            cmbColonia.DataValueField = "Id";
            cmbColonia.DataBind();


        }

        #endregion

        #region Funciones



        private void PreCargarInfo(int Id_Doc, int Id_Cte)
        {
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];
            DataSet dtDatos = new DataSet();
            new CN_CapRemision().ConsultaPrecarga(sesion, Id_Cte, ref dtDatos);

            foreach (DataRow Row in dtDatos.Tables[0].Rows)
            {
                //Carga Datos Vehiculo
                txtVehiculo.Text = Row[9].ToString();
                txtPlaca.Text = Row[10].ToString();
                txtModelo.Text = Row[11].ToString();
                txtTipoRemolque.Text = Row[12].ToString();
                txtPlacaRemolque.Text = Row[13].ToString();

                txtIdConfVehiculo.Text = Row[6].ToString();
                txtConfVehiculo.Text = Row[7].ToString();
                txtIdPermisoSCT.Text = Row[14].ToString();
                txtPermisoSCT.Text = Row[15].ToString();
                txtPermiso.Text = Row[16].ToString();
                txtAseguradora.Text = Row[18].ToString();
                txtPoliza.Text = Row[17].ToString();

                //Carga Datos Chofer
                txtNombre.Text = Row[3].ToString() + " " + Row[4].ToString() + " " + Row[5].ToString();
                txtRfc.Text = Row[2].ToString();
                txtLicencia.Text = Row[5].ToString();

                //Distancia 
                txtKm.Text = Row[19].ToString();
            }
        }

        private void Inicializacion(int Id_Rem, string Tipodoc)
        {
            InicializarTablaProductos(Id_Rem, Tipodoc);
        }

        //Se validan los campos del formulario para evitar que vayan campos vacios 
        private int Validaciones()
        {
            if (!Regex.IsMatch(txtRfc.Text, "[A-z]{4}[0-9]{6}[A-z0-9]{3}"))
            {
                Alerta($"{txtRfc.Text} no es un RFC válido, favor de tomar encuenta el siguiente formato, 4 Letras seguidas de 6 números y al final 3 caracteres (números o letras).");
                return 0;
            }

            if (txtPlaca.Text == "")
            {
                Alerta("El campo placa es requerido, favor de proporcionar la información para continuar.");
                txtPlaca.Focus();
                return 0;
            }
            if (txtModelo.Text == "")
            {
                Alerta("El campo modelo es requerido, favor de proporcionar la información para continuar.");
                txtModelo.Focus();
                return 0;
            }
            if (txtPermiso.Text == "")
            {
                Alerta("El campo permiso es requerido, favor de proporcionar la información para continuar.");
                txtPermiso.Focus();
                return 0;
            }
            if (txtNombre.Text == "")
            {
                Alerta("El campo nombre es requerido, favor de proporcionar la información para continuar.");
                txtNombre.Focus();
                return 0;
            }
            if (txtRfc.Text == "")
            {
                Alerta("El campo rfc es requerido, favor de proporcionar la información para continuar.");
                txtRfc.Focus();
                return 0;
            }
            if (txtLicencia.Text == "")
            {
                Alerta("El campo licencia es requerido, favor de proporcionar la información para continuar.");
                txtLicencia.Focus();
                return 0;
            }
            if (txtKm.Text == "")
            {
                Alerta("El campo km. recorridos es requerido, favor de proporcionar la información para continuar.");
                txtKm.Focus();
                return 0;
            }
            if (txtAseguradora.Text == "")
            {
                Alerta("El campo aseguradora es requerido, favor de proporcionar la información para continuar.");
                txtAseguradora.Focus();
                return 0;
            }
            if (txtPoliza.Text == "")
            {
                Alerta("El campo poliza es requerido, favor de proporcionar la información para continuar.");
                txtPoliza.Focus();
                return 0;
            }

            for (int x = 0; x < objdtTablaProd.Rows.Count; x++)
            {
                int y = x + 1;
                if (Convert.ToInt32(objdtTablaProd.Rows[x]["Distancia"]) <= 0)
                {
                    Alerta("El campo distancia de la partida " + y + " no puede estar en 0 km, favor de proporcionar la información para continuar.");
                    return 0;
                }
                if (objdtTablaProd.Rows[x]["CodigoPostal"].ToString() == string.Empty)
                {
                    Alerta("El campo código postal de la partida " + y + " no puede estar en 0 o vacio, favor de proporcionar la información para continuar.");
                    return 0;
                }
                if (objdtTablaProd.Rows[x]["Colonia"].ToString() == "")
                {
                    Alerta("El campo Colonia de la partida " + y + " no puede estar vacio, favor de proporcionar la información para continuar.");
                    return 0;
                }
            }

            return 1;
        }

        private void GeneraCartaPorte()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Validaciones() == 0)
                {
                    return;
                }
                else
                {

                    StringBuilder XML_Enviar = new StringBuilder();
                    StringBuilder XML_Detalle = new StringBuilder();
                    StringBuilder XML_Mercancia = new StringBuilder();
                    StringBuilder XML_Origen = new StringBuilder();
                    StringBuilder XML_Destino = new StringBuilder();
                    CN_CapRemision CN = new CN_CapRemision();
                    Remision remision = new Remision();
                    DireccionCFDI DirSAT = new DireccionCFDI();
                    CartaPorte carta = new CartaPorte();
                    Factura factura = new Factura();

                    object CFDI_Xml = "NULL";
                    object CFDI_Pdf = "NULL";
                    int Id_CFDI = 0;
                    int CFDI_Estatus = 0;
                    carta.Id_Emp = sesion.Id_Emp;
                    carta.Id_Cd = sesion.Id_Cd;
                    carta.Id_Doc = int.Parse(txtDocumento.Text);
                    string TieneCartaPorte = "Sí";

                    CN.Consulta_CFDISAT(carta, sesion.Emp_Cnx, ref CFDI_Pdf, ref CFDI_Xml, ref Id_CFDI, ref CFDI_Estatus);

                    if (Id_CFDI == 0 || CFDI_Estatus == 0)
                    {
                        RemisionDet RemDet = new RemisionDet();
                        ClienteDirEntrega Entrega = new ClienteDirEntrega();
                        DataSet dsDirecciones = new DataSet();

                        if (txtTipoDoc.Text == "Remision")
                        {
                            //Consulta Remision 
                            new CN_CapRemision().ConsultarEncabezadoImprimir(sesion, Id_Doc, ref remision, 0);

                            //Consulta Detalle de remision
                            List<RemisionDet> detalles = new List<RemisionDet>();
                            new CN_CapRemision().ConsultaRemisionDetalle(sesion, carta, ref detalles);
                        }

                        if (txtTipoDoc.Text == "Factura")
                        {
                            //Consulta Factura 
                            bool valor = false;
                            factura.Id_Emp = sesion.Id_Emp;
                            factura.Id_Cd = sesion.Id_Cd_Ver;
                            factura.Id_Fac = int.Parse(carta.Id_Doc.ToString());
                            new CN_CapFactura().ConsultaFacturaEncabezado(ref factura, sesion.Emp_Cnx, ref valor);

                            //Consulta Detalle de Factura
                            List<FacturaDet> detalles = new List<FacturaDet>();
                            new CN_CapRemision().ConsultaFacturaDetalle(sesion, carta, ref detalles);
                        }

                        //Consulta datos de clientes
                        Clientes Cliente = new Clientes();
                        if (txtTipoDoc.Text == "Remision")
                        {
                            carta.Id_Cte = remision.Id_Cte;
                        }
                        else
                        {
                            carta.Id_Cte = factura.Id_Cte;
                        }

                        carta.CodigoPostal = txtCodigoPostal.Text;
                        carta.Colonia = txtColonia.Text;
                        new CN_CapRemision().ConsultaClienteCDFI(sesion, carta, ref Cliente);


                        //Consulta datos de direcciones de entrega
                        int Tipo = 1; //Regresa tanto direcciones de origen y Destino
                        new CN_CapRemision().ConsultarDireccionesEntrega(sesion, carta, ref dsDirecciones, Tipo);

                        //Consulta datos de clientes
                        string Serie = "PRUEBA";
                        new CN_CapRemision().ConsultaFolioCDFI(sesion, ref Id_CFDI, ref Serie);

                        foreach (DataRow RowDatos in objdtTablaProd.Rows)
                        {
                            XML_Detalle.Append(" <Concepto");
                            string EsPel = "No";
                            if (RowDatos[11].ToString() == "True")
                            {
                                EsPel = "Sí";
                            }
                            else
                            {
                                EsPel = "No";
                            }
                            XML_Detalle.Append(" cveMatPeligroso=\"" + RowDatos[12].ToString() + "\"");
                            XML_Detalle.Append(" esPeligroso=\"" + EsPel + "\"");
                            XML_Detalle.Append(" cveEmbalaje=\"" + RowDatos[13].ToString() + "\"");
                            XML_Detalle.Append(" descEmbalaje=\"" + RowDatos[14].ToString() + "\"");
                            XML_Detalle.Append(" pesoKg=\"" + RowDatos[10].ToString() + "\"");

                            XML_Detalle.Append(" ClaveProdServ=\"" + RowDatos[15].ToString() + "\"");
                            XML_Detalle.Append(" ClaveUnidad=\"" + RowDatos[16].ToString() + "\"");
                            XML_Detalle.Append(" UnidadFiscal=\"" + RowDatos[17].ToString() + "\"");
                            XML_Detalle.Append(" cantidad=\"" + RowDatos[3].ToString() + "\"");
                            XML_Detalle.Append(" noIdentificacion=\"" + RowDatos[1].ToString().Replace("'", "") + "\"");
                            XML_Detalle.Append(" descripcion=\"" + RowDatos[2].ToString() + "\"");
                            XML_Detalle.Append(" valorUnitario=\"" + RowDatos[19].ToString() + "\"");
                            XML_Detalle.Append(" importe=\"" + RowDatos[20].ToString() + "\" />");
                        }

                        foreach (DataRow Row in objdtTablaProd.Rows)
                        {
                            XML_Mercancia.Append(" <Mercancia");
                            XML_Mercancia.Append(" IdProd=\"" + Row[1].ToString() + "\"");
                            XML_Mercancia.Append(" Cantidad=\"" + Row[3].ToString() + "\"");
                            XML_Mercancia.Append(" IdOrigen=\"" + Row[5].ToString() + "\"");
                            XML_Mercancia.Append(" IdDestino=\"" + Row[6].ToString() + "\" />");
                        }

                        foreach (DataRow Row in dsDirecciones.Tables[0].Select())
                        {

                            if (Row[1].ToString() == "Origen")
                            {
                                XML_Destino.Append(" <" + Row[1].ToString() + "");
                                XML_Destino.Append(" IdOrigen=\"" + Row[2].ToString() + "\"");
                                XML_Destino.Append(" Salida=\"" + DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") + "\"");
                                XML_Destino.Append(" Nombre=\"" + Row[4].ToString() + "\"");
                                XML_Destino.Append(" RFC=\"" + Regex.Replace(Row[5].ToString(), @"\s", "") + "\" >");
                                XML_Destino.Append(" <Domicilio ");
                                XML_Destino.Append(" Calle=\"" + Row[6].ToString().Trim() + "\"");
                                XML_Destino.Append(" NoExt=\"" + Row[7].ToString().Trim() + "\"");
                                XML_Destino.Append(" NoInt=\"" + Row[7].ToString().Trim() + "\"");

                                CN.ConsultaDireccionesSAT(sesion.Emp_Cnx, Row[13].ToString(), Row[8].ToString(), ref DirSAT);
                                if (DirSAT.Nom_Asentamiento == null || DirSAT.c_Colonia == null)
                                {
                                    Alerta("Información incorrecta, favor de validar los datos de dirección del catalogo del cliente antes de generar la carta porte.");
                                    return;
                                }

                                XML_Destino.Append(" Col=\"" + DirSAT.c_Colonia + "\"");
                                XML_Destino.Append(" Referencia=\"" + "" + "\"");
                                XML_Destino.Append(" Mun=\"" + DirSAT.c_Municipio + "\"");
                                XML_Destino.Append(" Loc=\"" + DirSAT.c_Localidad + "\"");
                                XML_Destino.Append(" Estado=\"" + DirSAT.c_Estado + "\"");
                                XML_Destino.Append(" CP=\"" + string.Format("{00000}", Row[13].ToString().Trim()) + "\" />");
                                XML_Destino.Append(" </" + Row[1].ToString().Trim() + ">");
                            }
                            if (Row[1].ToString() == "Destino")
                            {
                                foreach (DataRow Row2 in objdtTablaProd.Rows)
                                {
                                    if (Row[2].ToString() == Row2[6].ToString())
                                    {
                                        XML_Destino.Append(" <" + Row[1].ToString() + "");
                                        XML_Destino.Append(" distRecorrida=\"" + Row2[4].ToString() + "\"");
                                        XML_Destino.Append(" IdDestino=\"" + Row2[6].ToString() + "\"");
                                        XML_Destino.Append(" Llegada=\"" + Row2[9].ToString() + "\"");
                                        XML_Destino.Append(" Nombre=\"" + Row[4].ToString() + "\"");
                                        XML_Destino.Append(" RFC=\"" + Row[5].ToString() + "\" >");

                                        CN.ConsultaDireccionesSAT(sesion.Emp_Cnx, Row2[7].ToString(), Row2[8].ToString(), ref DirSAT);

                                        if (DirSAT.Nom_Asentamiento == null || DirSAT.c_Colonia == null)
                                        {
                                            Alerta("El campo Colonia " + Row[8].ToString().Trim() + " no es valido, favor de proporcionar la colonia correcta");
                                            return;
                                        }

                                        XML_Destino.Append(" <Domicilio ");
                                        XML_Destino.Append(" Calle=\"" + Row[6].ToString().Trim() + "\"");
                                        XML_Destino.Append(" NoExt=\"" + Row[7].ToString().Trim() + "\"");
                                        XML_Destino.Append(" NoInt=\"" + Row[7].ToString().Trim() + "\"");
                                        XML_Destino.Append(" Col=\"" + DirSAT.c_Colonia + "\"");
                                        XML_Destino.Append(" Referencia=\"" + "" + "\"");
                                        XML_Destino.Append(" Mun=\"" + DirSAT.c_Municipio + "\"");
                                        XML_Destino.Append(" Loc=\"" + DirSAT.c_Localidad + "\"");
                                        XML_Destino.Append(" Estado=\"" + DirSAT.c_Estado + "\"");
                                        XML_Destino.Append(" CP=\"" + string.Format("{00000}", DirSAT.c_CodigoPostal.Trim()) + "\"");
                                        XML_Destino.Append(" Pais=\"" + DirSAT.Pais + "\" />");
                                        XML_Destino.Append(" </" + Row[1].ToString().Trim() + ">");
                                    }
                                }
                            }
                        }


                        string TipoMoneda = "";
                        if (Cliente.Id_Mon == 2)
                            TipoMoneda = "pesos";
                        else
                            TipoMoneda = "dolares";

                        double SubTotal = 0;
                        double Total = 0;
                        string Nota = "";
                        string UCorreo = "";
                        int Id_Cte = 0;

                        if (txtTipoDoc.Text == "Remision")
                        {
                            SubTotal = remision.Rem_Subtotal;
                            Total = remision.Rem_Total;
                            Nota = remision.Rem_Nota;
                            UCorreo = remision.UCorreo;
                            Id_Cte = remision.Id_Cte;
                        }
                        else
                        {
                            SubTotal = Convert.ToDouble(factura.Fac_SubTotal);
                            Total = Convert.ToDouble(factura.Fac_ImporteIva + factura.Fac_SubTotal);
                            Nota = factura.Fac_Notas;
                            UCorreo = factura.Cte_Email;
                            Id_Cte = factura.Id_Cte;
                        }
                        #region construirXML

                        XML_Enviar.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                        XML_Enviar.Append("<Comprobante");
                        XML_Enviar.Append(" serie=\"" + Serie.ToString() + "\"");
                        XML_Enviar.Append(" folio =\"" + Id_CFDI.ToString() + "\"");
                        XML_Enviar.Append(" UUII=\"" + "" + "\"");
                        XML_Enviar.Append(" fecha=\"" + DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") + "\""); //
                        XML_Enviar.Append(" formaDePago=\"" + Cliente.Cte_MetodoPago + "\"");
                        XML_Enviar.Append(" subTotal=\"" + SubTotal + "\"");
                        XML_Enviar.Append(" total=\"" + Total + "\"");
                        XML_Enviar.Append(" tipoDeComprobante=\"" + "ingreso" + "\"");
                        XML_Enviar.Append(" Sustituye=\"" + "" + "\"");
                        XML_Enviar.Append(" tipoMovimiento=\"" + "TRASLADO" + "\"");
                        XML_Enviar.Append(" tipoMoneda=\"" + TipoMoneda + "\"");
                        XML_Enviar.Append(" tipoCambio=\"" + "" + "\"");
                        XML_Enviar.Append(" movimientoCancelar=\"" + "" + "\"");
                        XML_Enviar.Append(" ConceptoDescuento1=\"" + "descto" + "\"");
                        XML_Enviar.Append(" TasaDescuento1=\"" + "0" + "\"");
                        XML_Enviar.Append(" ConceptoDescuento2=\"" + "descto" + "\"");
                        XML_Enviar.Append(" TasaDescuento2=\"" + "0" + "\"");
                        XML_Enviar.Append(" Notas=\"" + Nota + "\"");
                        XML_Enviar.Append(" Correo=\"" + UCorreo + "\"");
                        XML_Enviar.Append(" CliNum=\"" + Id_Cte + "\"");
                        XML_Enviar.Append(" MetodoPago=\"" + Cliente.Cte_PagoMetodoPago + "\"");
                        XML_Enviar.Append(" CuentaBancaria=\"" + "" + "\"");
                        XML_Enviar.Append(" Referencia=\"" + "" + "\"");
                        XML_Enviar.Append(" ComprobanteVersion=\"" + "3.3" + "\" >");

                        //Emisor 
                        XML_Enviar.Append("<Emisor");
                        XML_Enviar.Append(" rfc=\"" + "KQU6911016X5" + "\"");
                        XML_Enviar.Append(" numero=\"" + sesion.Id_Cd_Ver + "\" />");

                        //Receptor
                        XML_Enviar.Append("<Receptor");
                        XML_Enviar.Append(" rfc=\"" + Cliente.Cte_FacRfc + "\"");
                        XML_Enviar.Append(" nombre=\"" + Cliente.Cte_NomComercial + "\"");
                        XML_Enviar.Append(" UsoCFDI=\"" + Cliente.Cte_UsoCFDI + "\" >");
                        //Domicilio

                        //Validacion de direccion de Cliente
                        //
                        if (Cliente.Cte_FacColonia == "" || Cliente.Cte_FacColonia == null)
                        {
                            Alerta("Favor de revisar en el catalogo de cliente que el codigo postal y la colonia sean correctos.");
                            return;
                        }

                        XML_Enviar.Append("<Domicilio");
                        XML_Enviar.Append(" calle=\"" + Cliente.Cte_FacCalle + "\"");
                        XML_Enviar.Append(" noExterior=\"" + Cliente.Cte_FacNumero.Trim() + "\"");
                        XML_Enviar.Append(" colonia=\"" + Cliente.Cte_FacColonia + "\"");
                        XML_Enviar.Append(" municipio=\"" + Cliente.Cte_FacMunicipio + "\"");
                        XML_Enviar.Append(" estado=\"" + Cliente.Cte_Estado + "\"");
                        XML_Enviar.Append(" pais=\"" + Cliente.Cte_Pais + "\"");
                        XML_Enviar.Append(" codigoPostal=\"" + string.Format("{00000}", Cliente.Cte_FacCp.Trim()) + "\" />");
                        XML_Enviar.Append("</Receptor>");

                        //Conceptos
                        XML_Enviar.Append("<Conceptos>");
                        XML_Enviar.Append(XML_Detalle);
                        XML_Enviar.Append("</Conceptos>");

                        XML_Enviar.Append("<Impuestos totalImpuestosTraslados=\"" + "0" + "\" >");
                        XML_Enviar.Append("<Traslados>");
                        XML_Enviar.Append("<Traslado ");
                        XML_Enviar.Append(" impuesto=\"" + "IVA" + "\"");
                        XML_Enviar.Append(" tasa=\"" + "0" + "\"");
                        XML_Enviar.Append(" importe=\"" + "0" + "\" />");

                        XML_Enviar.Append("</Traslados>");
                        XML_Enviar.Append("</Impuestos>");

                        #region CartaPorte
                        XML_Enviar.Append("<Traslado");
                        XML_Enviar.Append(" CartaPorte=\"" + TieneCartaPorte + "\" />");
                        //Carta Porte
                        XML_Enviar.Append("<CartaPorte>");

                        XML_Enviar.Append("<Ubicaciones ");
                        XML_Enviar.Append(" KmRecorridos=\"" + txtKm.Text.ToString() + "\" >");
                        XML_Enviar.Append(XML_Destino);
                        XML_Enviar.Append("</Ubicaciones>");

                        XML_Enviar.Append("<Mercancias>");
                        XML_Enviar.Append(XML_Mercancia);
                        XML_Enviar.Append("</Mercancias>");

                        ////Autotransporte
                        XML_Enviar.Append("<Autotransporte");
                        XML_Enviar.Append(" PermSCT=\"" + txtIdPermisoSCT.Text + "\"");
                        XML_Enviar.Append(" NumPerm=\"" + txtPermiso.Text + "\"");
                        XML_Enviar.Append(" Aseguradora=\"" + txtAseguradora.Text + "\"");
                        XML_Enviar.Append(" Poliza=\"" + txtPoliza.Text + "\"");
                        XML_Enviar.Append(" CofiguracionVhe=\"" + txtIdConfVehiculo.Text.Trim() + "\"");
                        XML_Enviar.Append(" PlacaVhe=\"" + txtPlaca.Text + "\"");
                        XML_Enviar.Append(" Modelo=\"" + txtModelo.Text + "\"");
                        XML_Enviar.Append(" TipoRemolque=\"" + txtTipoRemolque.Text + "\"");
                        XML_Enviar.Append(" Placa=\"" + txtPlacaRemolque.Text + "\" />");

                        //Figura Transporte
                        XML_Enviar.Append("<FiguraTransporte");
                        XML_Enviar.Append(" CveTranporte=\"" + "01" + "\" >");

                        XML_Enviar.Append("<Operador ");
                        XML_Enviar.Append(" RFC=\"" + txtRfc.Text + "\"");
                        XML_Enviar.Append(" NumLicencia=\"" + txtLicencia.Text + "\"");
                        XML_Enviar.Append(" Nombre=\"" + txtNombre.Text + "\" />");


                        XML_Enviar.Append("</FiguraTransporte>");
                        XML_Enviar.Append("</CartaPorte>");
                        #endregion

                        XML_Enviar.Append("</Comprobante>");

                        #endregion construirXML

                        // --------------------------------------
                        // Consulta centro de distribución
                        // --------------------------------------
                        CentroDistribucion Cd = new CentroDistribucion();
                        new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref Cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);
                        // --------------------------------------
                        // cargar xml que se envia a SAT
                        // --------------------------------------
                        XmlDocument xml = new XmlDocument();
                        xml.LoadXml(XML_Enviar.ToString());

                        // --------------------------------------//
                        // --------------------------------------//
                        //         LLENAR DATOS DEL XML        --//
                        // --------------------------------------//
                        // --------------------------------------//
                        #region Llenar datos a Enviar

                        CN_CatTipoMoneda cn_moneda = new CN_CatTipoMoneda();
                        TipoMoneda tm = new TipoMoneda();
                        tm.Id_Emp = sesion.Id_Emp;
                        tm.Id_Mon = 2;
                        cn_moneda.ConsultaTipoMonedaIndividual(ref tm, sesion.Emp_Cnx);

                        //encabezado
                        XmlNode Comprobante = xml.SelectSingleNode("Comprobante");
                        Comprobante.Attributes["serie"].Value = Serie.ToString();
                        Comprobante.Attributes["folio"].Value = Id_CFDI.ToString();
                        Comprobante.Attributes["fecha"].Value = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"); // string.Format("{0:yyyy-MM-ddTHH:mm:ss}", remision.Rem_FechaHr);
                        Comprobante.Attributes["subTotal"].Value = remision.Rem_Subtotal.ToString();
                        Comprobante.Attributes["total"].Value = remision.Rem_Total.ToString();
                        Comprobante.Attributes["tipoDeComprobante"].Value = "ingreso";
                        Comprobante.Attributes["tipoMovimiento"].Value = "TRASLADO";
                        Comprobante.Attributes["tipoMoneda"].Value = tm.Mon_Abrev;
                        Comprobante.Attributes["tipoCambio"].Value = tm.Mon_TipCambio.ToString();
                        Comprobante.Attributes["CliNum"].Value = remision.Id_Cte.ToString();
                        Comprobante.Attributes["Notas"].Value = remision.Rem_Nota;

                        Comprobante.Attributes["MetodoPago"].Value = "99"; //.Substring(1, 2 - Cliente.Cte_PagoMetodoPago.Trim().Length);

                        Comprobante.Attributes["CuentaBancaria"].Value = "0";
                        Clientes cliente = new Clientes();
                        cliente.Id_Emp = sesion.Id_Emp;
                        cliente.Id_Cd = sesion.Id_Cd_Ver;
                        cliente.Id_Cte = Convert.ToInt32(remision.Id_Cte);
                        new CN_CatCliente().ConsultaClientes(ref cliente, sesion.Emp_Cnx);

                        //receptor
                        XmlNode Receptor = Comprobante.SelectSingleNode("Receptor");
                        Receptor.Attributes["rfc"].Value = cliente.Cte_FacRfc;
                        Receptor.Attributes["nombre"].Value = cliente.Cte_NomComercial.ToString();
                        Receptor.Attributes["UsoCFDI"].Value = cliente.Cte_UsoCFDI;

                        //
                        XmlNode Emisor = Comprobante.SelectSingleNode("Emisor");
                        Emisor.Attributes["rfc"].Value = Regex.Replace(Cd.Cd_Rfc, @"\s", "");
                        Emisor.Attributes["numero"].Value = sesion.Id_Cd_Ver.ToString();
                        //Domicilio
                        XmlNode Domicilio = Receptor.SelectSingleNode("Domicilio");
                        Domicilio.Attributes["calle"].Value = cliente.Cte_FacCalle;
                        Domicilio.Attributes["noExterior"].Value = cliente.Cte_FacNumero;
                        Domicilio.Attributes["colonia"].Value = Cliente.Cte_FacColonia;
                        Domicilio.Attributes["municipio"].Value = Cliente.Cte_FacMunicipio;
                        Domicilio.Attributes["estado"].Value = Cliente.Cte_Estado;
                        Domicilio.Attributes["pais"].Value = Cliente.Cte_Pais;
                        Domicilio.Attributes["codigoPostal"].Value = string.Format("{00000}", Cliente.Cte_FacCp.Trim());

                        // ---------------------
                        // Conceptos --> partidas = producto
                        // Detalle --> productoDetalle
                        // ---------------------              
                        XmlNode Conceptos = Comprobante.SelectSingleNode("Conceptos");
                        XmlNode producto = Conceptos.SelectSingleNode("Concepto");
                        XmlNode Impuestos = Comprobante.SelectSingleNode("Impuestos");

                        #endregion
                        // --------------------------------------
                        // convertir XML a string 
                        // --------------------------------------

                        StringWriter sw = new StringWriter();
                        XmlTextWriter tx = new XmlTextWriter(sw);
                        xml.WriteTo(tx);
                        string xmlString = sw.ToString();

                        // ------------------------------------------------------
                        // ENVIAR XML al servicio de la aplicacion de KEY
                        // ------------------------------------------------------
                        XmlDocument xmlSAT = new XmlDocument();

                        WebReference.Service1 sianFacturacionElectronica = new WebReference.Service1();
                        sianFacturacionElectronica.Url = ConfigurationManager.AppSettings["WS_CFDIImpresion"].ToString();
                        object sianFacturacionElectronicaResult = sianFacturacionElectronica.ObtieneCFD(xmlString);

                        // ------------------------------------------------------
                        string stringPDF = string.Empty;
                        string selloSAT = string.Empty;
                        string folioFiscal = string.Empty;
                        string errorNum = string.Empty;
                        string errorText = string.Empty;

                        xmlSAT.LoadXml(sianFacturacionElectronicaResult.ToString());
                        int TSAT = 1;
                        foreach (XmlNode nodo in xmlSAT.DocumentElement)
                        {
                            if (nodo.Name == "cfdi:Complemento")
                            {
                                TSAT = 2;
                                foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                                {
                                    if (Nodo_nivel2.Name == "tfd:TimbreFiscalDigital")
                                    {
                                        selloSAT = Nodo_nivel2.Attributes["SelloSAT"].Value;
                                        folioFiscal = Nodo_nivel2.Attributes["UUID"].Value;
                                    }
                                }
                            }
                            if (nodo.Name == "AddendaKey")
                            {
                                foreach (XmlNode Nodo_nivel3 in nodo.ChildNodes)
                                {
                                    if (Nodo_nivel3.Name == "PDF")
                                        stringPDF = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                    if (Nodo_nivel3.Name == "ERROR")
                                    {
                                        errorNum = Nodo_nivel3.Attributes["Numero"].Value;
                                        errorText = Nodo_nivel3.Attributes["Texto"].Value;
                                    }
                                }
                            }
                        }

                        //Se guarda el registro aunque se regrese con error para poder tener el folio de seguimiento diponible
                        //RBM Agosto 2023
                        carta.Id_Emp = sesion.Id_Emp;
                        carta.Id_Cd = sesion.Id_Cd_Ver;
                        carta.Id_Rem = Id_Doc;
                        carta.Id_CFDI = Id_CFDI;
                        carta.CFDI_Fecha = DateTime.Now;
                        carta.CFDI_Sello = selloSAT;
                        carta.CFDI_FolioFiscal = folioFiscal;
                        carta.CFDI_Cancelacion = "NULL";
                        System.Data.SqlTypes.SqlXml sqlXml = new System.Data.SqlTypes.SqlXml(new XmlTextReader(xmlSAT.OuterXml, XmlNodeType.Document, null));
                        carta.CFDI_Xml = sqlXml;
                        carta.CFDI_Pdf = this.Base64ToByte(stringPDF);
                        int verificador = 0;
                        if (errorNum != "0")
                            carta.CFDI_Estatus = "0";
                        else
                            carta.CFDI_Estatus = "1";

                        //Datos referencia para llenado automatico
                        carta.Id_Cte = Id_Cte;
                        carta.PlacaVehiculo = txtPlaca.Text;
                        carta.RfcChofer = txtRfc.Text;
                        carta.Distancia = int.Parse(txtKm.Text);
                        carta.CodigoPostal = txtCodigoPostal.Text;
                        carta.Colonia = txtColonia.Text;

                        new CN_CapRemision().InsertarCFDITrasladoSAT(carta, sesion.Emp_Cnx, ref verificador);
                        //Fin

                        if (errorNum != "0")
                        {
                            this.Alerta(string.Concat("El servicio de KEY ha devuelto el siguiente error:<br/>", errorText.Replace("'", "\"")));
                            return;
                        }
                        else
                        {
                            RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow('", "La carta porte se ha generado correctamente.", "')"));
                        }
                    }
                    else
                    {
                        Alerta("Este Documento ya cuenta con un CFDI de traslado, se puede descargar en la opción de PDF o XML.");
                    }
                }
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
        private void InicializarTablaProductos(int Id_Doc, string TipoDoc)
        {
            try
            {
                objdtListaProd = new DataTable();
                objdtListaProd.Columns.Add("Id_Det");
                objdtListaProd.Columns.Add("Id_Prd");
                objdtListaProd.Columns.Add("Descripcion");
                objdtListaProd.Columns.Add("Cantidad");
                objdtListaProd.Columns.Add("Distancia");
                objdtListaProd.Columns.Add("Origen");
                objdtListaProd.Columns.Add("Destino");
                objdtListaProd.Columns.Add("Codigopostal");
                objdtListaProd.Columns.Add("Colonia");
                objdtListaProd.Columns.Add("Fecha");
                objdtListaProd.Columns.Add("Kg");
                objdtListaProd.Columns.Add("EsPeligroso");
                objdtListaProd.Columns.Add("ClaveMatPeligroso");
                objdtListaProd.Columns.Add("ClaveEmbalaje");
                objdtListaProd.Columns.Add("Embalaje");

                //Se agregan
                objdtListaProd.Columns.Add("ClaveProdServ");
                objdtListaProd.Columns.Add("ClaveUnidad");
                objdtListaProd.Columns.Add("UnidadFiscal");
                objdtListaProd.Columns.Add("noIdentificacion");
                objdtListaProd.Columns.Add("valorUnitario");
                objdtListaProd.Columns.Add("importe");

                objdtTablaProd = objdtListaProd;
                Getlist(Id_Doc, TipoDoc);
            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }
        }

        private void Getlist(int Id_Doc, string TipoDoc)
        {
            Sesion Sesion = new CapaEntidad.Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CartaPorte carta = new CartaPorte();
            carta.Id_Emp = Sesion.Id_Emp;
            carta.Id_Cd = Sesion.Id_Cd;
            carta.Id_Doc = Id_Doc;
            carta.Id_Cte = Id_Cte;

            DataSet dsDirecciones = new DataSet();
            new CN_CapRemision().ConsultarDireccionesEntrega(Sesion, carta, ref dsDirecciones, 1);

            if (TipoDoc == "Remision")
            {
                List<RemisionDet> detalles = new List<RemisionDet>();
                new CN_CapRemision().ConsultaRemisionDetalle(Sesion, carta, ref detalles);
                CargarProductos(detalles);
                rgProductos.DataSource = objdtTablaProd;
            }
            else
            {
                List<FacturaDet> detalles = new List<FacturaDet>();
                new CN_CapRemision().ConsultaFacturaDetalle(Sesion, carta, ref detalles);
                CargarProductos(detalles);
                rgProductos.DataSource = objdtTablaProd;
            }
        }

        private void CargarProductos(List<FacturaDet> detalles)
        {
            try
            {
                foreach (FacturaDet det in detalles)
                {
                    ArrayList ArrayProd = new ArrayList();
                    ArrayProd.Add(det.Id_FacDet);
                    ArrayProd.Add(det.Id_Prd);
                    ArrayProd.Add(det.Prd_Descripcion.Replace("\"", ""));
                    ArrayProd.Add(det.Fac_Cant);
                    ArrayProd.Add(1.0);
                    ArrayProd.Add("OR000001");
                    ArrayProd.Add("DE000001");
                    ArrayProd.Add("");
                    ArrayProd.Add("");
                    ArrayProd.Add(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"));
                    ArrayProd.Add(det.Fac_Cant * det.Prd_Peso);
                    ArrayProd.Add(det.EsPeligroso);
                    ArrayProd.Add("");
                    ArrayProd.Add("");
                    ArrayProd.Add("");
                    //nuevos
                    ArrayProd.Add(det.Producto.Prd_ClaveProdServ);
                    ArrayProd.Add(det.Producto.Prd_ClaveUnidad);
                    ArrayProd.Add(det.Prd_UniNe);
                    ArrayProd.Add(det.Id_Prd);
                    ArrayProd.Add(det.Fac_Precio);
                    ArrayProd.Add(det.Fac_Importe);

                    objdtTablaProd.Rows.Add(ArrayProd.ToArray());
                }

            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }
        }

        private void CargarProductos(List<RemisionDet> detalles)
        {
            try
            {
                foreach (RemisionDet det in detalles)
                {
                    ArrayList ArrayProd = new ArrayList();
                    ArrayProd.Add(det.Id_RemDet);
                    ArrayProd.Add(det.Id_Prd);
                    ArrayProd.Add(det.Prd_Descripcion.Replace("\"", ""));
                    ArrayProd.Add(det.Rem_Cant);
                    ArrayProd.Add(1.0);
                    ArrayProd.Add("OR000001");
                    ArrayProd.Add("DE000001");
                    ArrayProd.Add("");
                    ArrayProd.Add("");
                    ArrayProd.Add(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"));
                    ArrayProd.Add(det.Rem_Cant * det.Prd_Peso);
                    ArrayProd.Add(det.EsPeligroso);
                    ArrayProd.Add("");
                    ArrayProd.Add("");
                    ArrayProd.Add("");
                    //nuevos
                    ArrayProd.Add(det.Producto.Prd_ClaveProdServ);
                    ArrayProd.Add(det.Producto.Prd_ClaveUnidad);
                    ArrayProd.Add(det.Prd_UniNe);
                    ArrayProd.Add(det.Id_Prd);
                    ArrayProd.Add(det.Rem_Precio);
                    ArrayProd.Add(det.Rem_Importe);

                    objdtTablaProd.Rows.Add(ArrayProd.ToArray());
                }

            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }
        }

        protected void func_cerrarventana(string param)
        {
            string funcion = "CloseAndRebind('" + param + "')";
            string script = "<script>" + funcion + "</script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
        }

        private void CargarDestinos(GridEditableItem editItem)
        {
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];
            RadComboBox cmbDestino = (RadComboBox)editItem.FindControl("cmbDestino");
            RadComboBox cmbColonia = (RadComboBox)editItem.FindControl("cmbColonia");

            Remision remision = new Remision();
            CartaPorte carta = new CartaPorte();
            carta.Id_Emp = sesion.Id_Emp;
            carta.Id_Cd = sesion.Id_Cd;
            carta.Id_Cte = Id_Cte;
            DataTable DtResultado = new DataTable();
            new CN_CapRemision().ConsultarEncabezadoImprimir(sesion, int.Parse(txtDocumento.Text), ref remision, 0);
            DataSet dsDirecciones = new DataSet();
            new CN_CapRemision().ConsultarDireccionesEntrega(sesion, carta, ref dsDirecciones, 2);

            if (dsDirecciones.Tables[0].Rows.Count > 0)
            {
                cmbDestino.DataSource = dsDirecciones.Tables[0];
                cmbDestino.DataTextField = "Calle";
                cmbDestino.DataValueField = "EntSal";
                cmbDestino.DataBind();
            }
        }

        private void CargarMaterialPeligroso(GridEditableItem editItem)
        {
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];
            RadComboBox cmbClaveMatPeligroso = (RadComboBox)editItem.FindControl("cmbClaveMatPeligroso");
            DataSet dsMaterialPeligroso = new DataSet();
            new CN_CapRemision().ConsultaMaterialPeligroso(sesion, ref dsMaterialPeligroso);

            cmbClaveMatPeligroso.DataSource = dsMaterialPeligroso.Tables[0];
            cmbClaveMatPeligroso.DataTextField = "Descripcion";
            cmbClaveMatPeligroso.DataValueField = "Id";
            cmbClaveMatPeligroso.DataBind();

        }

        private void CargarEmbalaje(GridEditableItem editItem)
        {
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];
            RadComboBox cmbEmbalaje = (RadComboBox)editItem.FindControl("cmbEmbalaje");
            DataSet dsEmbalaje = new DataSet();
            new CN_CapRemision().ConsultaEmbalaje(sesion, ref dsEmbalaje);

            cmbEmbalaje.DataSource = dsEmbalaje.Tables[0];
            cmbEmbalaje.DataTextField = "Descripcion";
            cmbEmbalaje.DataValueField = "Id";
            cmbEmbalaje.DataBind();

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
                Alerta(Message);
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
                Alerta("Error: [" + NombreFuncion + "] " + eme.Message.ToString());
                //this.lblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();

            }
            catch (Exception ex)
            {
                Alerta("Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString());
                //this.lblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }

        #endregion

    }
}