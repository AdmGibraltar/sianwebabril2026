using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using CapaEntidad;
using System.Collections;
using CapaNegocios;
using CapaDatos;
using DevExpress.Web;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.BandedGrid;
using System.Web.Services;
using Newtonsoft.Json;
using DevExpress.Web.Bootstrap;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Text;
using System.Globalization;
using System.IO;
using System.Configuration;
using SIANWEB.API;
using CapaModelo_CC.CuentasCoorporativas;
using System.Data.SqlClient;


namespace SIANWEB
{
    public class UploadedFilesStorage2
    {
        public string Path { get; set; }
        public string Key { get; set; }
        public DateTime LastUsageTime { get; set; }

        public IList<UploadedFileInfo2> Files { get; set; }
    }

    public class UploadedFileInfo2
    {
        public string UniqueFileName { get; set; }
        public string OriginalFileName { get; set; }
        public string FilePath { get; set; }
        public string FileSize { get; set; }
    }

    public partial class ProPedidoVIInternet2 : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Trim().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Trim().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Trim().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Trim().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Trim().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Trim().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Trim().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Trim().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        protected string SubmissionID
        {
            get
            {
                return HiddenField.Get("SubmissionID").ToString().Trim();
            }
            set
            {
                HiddenField.Set("SubmissionID", value);
            }
        }
        UploadedFilesStorage2 UploadedFilesStorage2
        {
            get { return UploadControlHelper.GetUploadedFilesStorage2ByKey(SubmissionID); }
        }
        protected void Page_PreLoad(object sender, EventArgs e)
        {
            UploadControlHelper.RemoveOldStorages();
        }

        public DataTable dt
        {
            get
            {
                return (DataTable)Session["dtPedidoVI" + Session.SessionID];

            }
            set
            {
                Session["dtPedidoVI" + Session.SessionID] = value;

            }
        }

        public DataTable dtNuevaLista
        {
            get
            {
                return (DataTable)Session["dtPedidoVILista" + Session.SessionID];
            }
            set
            {
                Session["dtPedidoVILista" + Session.SessionID] = value;
            }
        }

        public ArrayList al
        {
            get
            {
                return (ArrayList)Session["BorradosInternet" + Session.SessionID];
            }
            set { Session["BorradosInternet" + Session.SessionID] = value; }
        }


        //private bool terr = false;
        private bool prod = false;
        public Sesion session
        {
            get
            {
                return (Sesion)Session["Sesion" + Session.SessionID];
            }
            set
            {
                Session["Sesion" + Session.SessionID] = value;

            }
        }
        private object _producto;
        public object producto
        {
            get { return _producto; }
            set { _producto = value; }
        }
        public int productoNuevo = 0;
        public int Id_TG
        {
            get
            {
                string _idTGStr = Request.QueryString["Id_TG"];
                int _idTG = 0;
                if (_idTGStr != null)
                {
                    if (int.TryParse(_idTGStr, out _idTG))
                    {
                        return _idTG;
                    }
                }
                return _idTG;
            }
        }
        #endregion
        #region Eventos
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                if (Session["dtPedidoVI"] != null)
                {

                    DataTable dtTemp = (DataTable)Session["dtPedidoVI"];

                    rg1.DataSource = dtTemp;
                    rg1.DataBind();
                }
            }


            var comboboxColumn = rg1.DataColumns["ACS_ReqOC"] as DevExpress.Web.Bootstrap.BootstrapGridViewComboBoxColumn;
            DevExpress.Web.Internal.ReflectionUtils.SetNonPublicInstancePropertyValue(comboboxColumn.PropertiesComboBox, "DataSecurityMode", DevExpress.Web.DataSecurityMode.Default);


            var comboboxColumndoc = rg1.DataColumns["Acs_Doc"] as DevExpress.Web.Bootstrap.BootstrapGridViewComboBoxColumn;
            DevExpress.Web.Internal.ReflectionUtils.SetNonPublicInstancePropertyValue(comboboxColumndoc.PropertiesComboBox, "DataSecurityMode", DevExpress.Web.DataSecurityMode.Default);

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];


                if (Sesion == null)
                {
                    CerrarVentana();
                }
                else
                {
                    txtPedCaptadorPor.Text = Sesion.U_Nombre;
                    if (this.IsPostBack)
                    {
                        TabName.Value = Request.Form[TabName.UniqueID];

                    }
                    if (!Page.IsPostBack)
                    {

                        SubmissionID = UploadControlHelper2.GenerateUploadedFilesStorageKey();
                        UploadControlHelper2.AddUploadedFilesStorage(SubmissionID);

                        _nuevoPedidoSinProgramar = false;
                        Session["Id_Ped" + Session.SessionID] = null;
                        Session["dtPedidoVI" + Session.SessionID] = null;
                        Session["BorradosInternet" + Session.SessionID] = null;
                        Session["Prod"] = null;
                        //Edsg28052015
                        Session["ProductosNoAcysInternet"] = null;
                        Session.Add("ProductosNoAcys", new List<Int64>());
                        Session["nuevaLista"] = null;

                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirContrasenas", "AbrirContrasenas()", true);
                            return;
                        }
                        if (Session["BorradosInternet" + Session.SessionID] == null)
                        {
                            Session["BorradosInternet" + Session.SessionID] = new ArrayList();
                        }
                        Inicializar();
                        al = new ArrayList();


                        HF_Emp_Cnx.Value = Sesion.Emp_Cnx;
                        HF_IdCd.Value = session.Id_Cd_Ver.ToString().Trim();
                        HF_IdEmp.Value = session.Id_Emp.ToString().Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                Alerta(ex.Message.ToString().Trim());
            }
        }

        void ValidateInputData()
        {
            bool isInvalid = UploadedFilesTokenBox.Tokens.Count == 0;
            if (isInvalid)
                throw new Exception("No se a cargado ningun documento.");
        }

        protected void ProcessSubmit(UploadedFileInfo2 fileInfo, ref string base64)
        {
            // process uploaded files here
            byte[] fileContent = File.ReadAllBytes(fileInfo.FilePath);
            base64 = Convert.ToBase64String(fileContent);
        }

        protected void ProcessSubmitURL(string fileInfo, ref string base64)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            // process uploaded files here
            using (var webClient = new WebClient())
            {
                byte[] fileContent = webClient.DownloadData(fileInfo);
                base64 = Convert.ToBase64String(fileContent);
            }
        }

        protected void DocumentsUploadControl_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            bool isSubmissionExpired = false;
            if (UploadedFilesStorage2 == null)
            {
                isSubmissionExpired = true;
                UploadControlHelper.AddUploadedFilesStorage2(SubmissionID);
            }
            UploadedFileInfo2 tempFileInfo = UploadControlHelper.AddUploadedFileInfo2(SubmissionID, e.UploadedFile.FileName);

            e.UploadedFile.SaveAs(tempFileInfo.FilePath);

            if (e.IsValid)
                e.CallbackData = tempFileInfo.UniqueFileName + "|" + isSubmissionExpired;
        }


        protected void cargarArchivo(ref string nombredoc, ref string extension, ref string archivobase64)
        {


            if (Convert.ToInt32(Request.QueryString["tipoPedido"]) != 0)
            {



                Uri uri = new Uri(Request.QueryString["UrlPedido"].ToString());

                nombredoc = System.IO.Path.GetFileName(uri.LocalPath).Split('.')[0];
                extension = System.IO.Path.GetExtension(uri.LocalPath);

                ProcessSubmitURL(Request.QueryString["UrlPedido"].ToString(), ref archivobase64);

            }
            else
            {
                ValidateInputData();

                List<UploadedFileInfo2> resultFileInfos = new List<UploadedFileInfo2>();

                bool allFilesExist = true;

                if (UploadedFilesStorage2 == null)
                    UploadedFilesTokenBox.Tokens = new TokenCollection();

                foreach (string fileName in UploadedFilesTokenBox.Tokens)
                {
                    UploadedFileInfo2 demoFileInfo = UploadControlHelper.GetDemoFileInfo(SubmissionID, fileName);
                    FileInfo fileInfo = new FileInfo(demoFileInfo.FilePath);
                    nombredoc = fileName;
                    extension = Path.GetExtension(fileName);

                    if (fileInfo.Exists)
                    {
                        ProcessSubmit(demoFileInfo, ref archivobase64);
                        demoFileInfo.FileSize = fileInfo.Length.ToString().Trim();
                        resultFileInfos.Add(demoFileInfo);
                    }
                    else
                        allFilesExist = false;
                }



                if (allFilesExist && resultFileInfos.Count > 0)
                {
                    UploadedFilesTokenBox.ErrorText = "Archivos cargados exitosamente.";
                    UploadedFilesTokenBox.IsValid = false;

                    UploadControlHelper.RemoveUploadedFilesStorage2(SubmissionID);
                    ASPxEdit.ClearEditorsInContainer(FormLayout, true);
                }
                else
                {
                    UploadedFilesTokenBox.ErrorText = "Arhivo no cargado. Revise la informacion del archivo.";
                    UploadedFilesTokenBox.IsValid = false;
                }
            }
        }

        protected void txtProducto_Load(object sender, EventArgs e)
        {
            producto = sender;
        }
        protected void rdFechaF_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            try
            {
                //if (HF_InicioSemana.Value == "" || HF_FinSemana.Value == "")
                //{
                //    Alerta("No está configurada la semana actual");
                //    return;
                //}
                //if (rdFechaF.SelectedDate < Convert.ToDateTime(HF_InicioSemana.Value) || rdFechaF.SelectedDate > Convert.ToDateTime(HF_FinSemana.Value))
                //{
                //    Alerta("La fecha no pertenece a la semana actual");
                //    rdFechaF.SelectedDate = Convert.ToDateTime(HF_FechaActual.Value);
                //}
            }
            catch (Exception ex)
            {
                Alerta(ex.Message.ToString().Trim());
            }
        }
        protected void chkMod_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chkbox = (sender as CheckBox);

                (chkbox.Parent.FindControl("cmbDia") as RadComboBox).Enabled = chkbox.Checked;
                (chkbox.Parent.FindControl("txtFrecuencia") as RadNumericTextBox).Enabled = chkbox.Checked;

            }
            catch (Exception ex)
            {
                Alerta(ex.Message.ToString().Trim());
            }
        }
        protected void imgAceptar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "popup()", true);
            }
            catch (Exception ex)
            {
                Alerta(ex.Message.ToString().Trim());
            }
        }
        protected void ImgBuscarDireccionEntrega_Click(object sender, ImageClickEventArgs e)
        {
            try
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "AbrirBuscarDireccionEntrega()", true);
            }
            catch (Exception ex)
            {
                Alerta(ex.Message.ToString().Trim());
            }
        }
        protected void rg1_ItemDataBound(object sender, GridItemEventArgs e)
        {

            if (Request.QueryString["IdOC"] != null)
            {
                //EDSG si es orden de compra no puede editar los productos
                if (e.Item is GridDataItem && !e.Item.IsInEditMode)
                {
                    GridDataItem gdi = (e.Item as GridDataItem);

                    gdi["DeleteColumn"].Enabled = false;
                    gdi["EditCommandColumn"].Enabled = false;

                }

                if (e.Item is GridCommandItem && !e.Item.IsInEditMode)
                {
                    LinkButton btAgregar = (LinkButton)e.Item.FindControl("LinkButton1");
                    LinkButton btEliminar = (LinkButton)e.Item.FindControl("LinkButton2");

                    btAgregar.Enabled = false;
                    btEliminar.Enabled = false;
                }

            }


            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode) && HF_ID.Value != "")
            {

                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CapPedido cn_pedido = new CN_CapPedido();
                Pedido ped = new Pedido();
                ped.Id_Emp = session.Id_Emp;
                ped.Id_Cd = session.Id_Cd_Ver;
                ped.Id_Ped = Convert.ToInt32(HF_ID.Value);
                cn_pedido.ConsultaPedido(ref ped, session.Emp_Cnx);

                string[] estatus = { "O", "I", "U", "A", "F", "R", "X", "E", "N", "D", "B" };

                if (estatus.Contains(ped.Estatus))
                {
                    GridEditableItem editItem = (GridEditableItem)e.Item;
                    ImageButton updatebtn = (ImageButton)editItem.FindControl("UpdateButton");
                    if (updatebtn != null)
                    {
                        ((RadNumericTextBox)editItem.FindControl("txtCantidad")).Enabled = false;
                        //((RadNumericTextBox)editItem.FindControl("txtPrecio")).Enabled = false;
                        //((RadNumericTextBox)editItem.FindControl("txtImporte")).Enabled = false;
                        ((CheckBox)editItem.FindControl("chkModTemp")).Enabled = false;
                    }
                }

                CN_CatTipoVenta cnTv = new CN_CatTipoVenta(session);

                DataRowView drv = (DataRowView)e.Item.DataItem;
                if (drv["Id_TG"] != null)
                {
                    if (!(drv["Id_TG"] is DBNull))
                    {
                        int idTG = (int)drv["Id_TG"];
                        if (idTG != 0)
                        {
                            RadComboBox rcb = e.Item.FindControl("cmbDocumento") as RadComboBox;
                            rcb.SelectedIndex = 1;
                            rcb.Enabled = false;
                        }
                    }
                }
            }
            //TODO: AGREGAR PARA PONER EL FOCUS
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem form = (GridEditableItem)e.Item;
                RadNumericTextBox dataField = (RadNumericTextBox)form["Id_Prod"].FindControl("txtProd");
                if (!dataField.Enabled)
                {
                    dataField = (RadNumericTextBox)form["Acys_Cantidad"].FindControl("txtCantidad");
                }
                dataField.Focus();


                CN_CatTipoVenta cnTv = new CN_CatTipoVenta(session);

                if (!(e.Item is GridDataInsertItem))
                {
                    DataRowView drv = (DataRowView)e.Item.DataItem;
                    if (drv["Id_TG"] != null)
                    {
                        if (!(drv["Id_TG"] is DBNull))
                        {
                            int idTG = (int)drv["Id_TG"];
                            if (idTG != 0)
                            {
                                RadComboBox rcb = e.Item.FindControl("cmbDocumento") as RadComboBox;
                                ((RadNumericTextBox)e.Item.FindControl("txtPrecio")).ReadOnly = true;
                                rcb.SelectedIndex = 1;
                                rcb.Enabled = false;
                            }
                        }
                    }
                }
            }

            if (e.Item is GridDataItem && !e.Item.IsInEditMode)
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                if (!_nuevoPedidoSinProgramar)
                {
                    if (drv["Id_TG"] != null)
                    {
                        if (!(drv["Id_TG"] is DBNull))
                        {
                            int idTG = (int)drv["Id_TG"];
                            if (idTG != 0)
                            {
                                GridDataItem gdi = (e.Item as GridDataItem);
                                if (gdi != null)
                                {
                                    CN_CapPedidoVtaInst cn_capPedidoVI = new CN_CapPedidoVtaInst();
                                    string idTGStr = Request.QueryString["Id_TG"];
                                    int? idTGNullable = null;
                                    idTG = 0;
                                    if (idTGStr != null)
                                    {
                                        if (int.TryParse(idTGStr, out idTG))
                                        {
                                            idTGNullable = idTG;
                                        }
                                    }
                                    List<PedidoVtaInst> List = new List<PedidoVtaInst>();
                                    DataTable dtTemp = dt.Clone();
                                    PedidoVtaInst pedido = new PedidoVtaInst();
                                    pedido.Id_Emp = session.Id_Emp;
                                    pedido.Id_Cd = session.Id_Cd_Ver;
                                    pedido.Id_Acs = (int)drv["Id_Acs"];
                                    pedido.Acs_Semana = Convert.ToInt32(txtSemana.Value);
                                    pedido.Acs_Anio = Convert.ToInt32(txtFecha.Value);

                                    cn_capPedidoVI.ConsultarDet(pedido, ref List, ref dtTemp, session.Emp_Cnx, idTGNullable);
                                    var productosExistentes = (from dr in dtTemp.AsEnumerable()
                                                               where (Int64)dr["Id_Prd"] == (Int64)drv["Id_Prd"]
                                                               select 1).ToList();
                                    if (productosExistentes.Count > 0)
                                        gdi["DeleteColumn"].Visible = false;

                                }
                            }
                        }
                    }
                }
            }
        }

        protected void rg1_Resto_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }

        //PRODUCTOS
        protected void cmbProducto_DataBound(object sender, EventArgs e)
        {
            RadComboBox comboBox = ((RadComboBox)sender);
            //comboBox.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
            string id = ((RadNumericTextBox)comboBox.Parent.Parent.FindControl("txtProd")).Text;
            if (id != "")
            {
                comboBox.SelectedIndex = comboBox.FindItemIndexByValue(id);
            }
        }
        protected void cmbProductoDet_TextChanged(object sender, EventArgs e)
        {

            RadNumericTextBox rdBox = (sender as RadNumericTextBox);
            CN_CatProducto cn_catproducto = new CN_CatProducto();
            Producto pr = new Producto();
            try
            {
                DataRow[] Ar_dr;
                Ar_dr = dt.Select("Id_prd='" + rdBox.Text + "'");

                Int64 idProd = 0;
                if (!Int64.TryParse(rdBox.Text, out idProd))
                {
                    Alerta("El formato del identificador del producto debe ser numérico. Por favor, capture un valor aceptable.");
                    return;
                }
                CN_CapAcys cnCa = new CN_CapAcys();
                if (txtIdTer.Value.ToString().Trim() == "")
                {
                    Alerta("Por favor, capture un territorio en la vista \"Datos Generales\"");
                    return;
                }
                if (txtIdCte.Value.ToString().Trim() == "")
                {
                    Alerta("Por favor, capture un territorio en la vista \"Datos Generales\"");
                    return;
                }
                if (txtIdRik.Value.ToString().Trim() == "")
                {
                    Alerta("Por favor, capture un representante de ventas en la vista \"Datos Generales\"");
                    return;
                }
                if (_nuevoPedidoSinProgramar && cnCa.ExisteProductoEnGarantia(session.Id_Emp, session.Id_Cd, idProd, Convert.ToInt32(txtIdTer.Value), Convert.ToInt32(txtIdCte.Value), Convert.ToInt32(txtIdRik.Value), session))
                {
                    Alerta("Solo se aceptan productos con modalidad de venta convencional que no sean parte del ACYS. Por favor, ingrese otro código de producto.");
                    return;
                }
                if (Ar_dr.Length > 0)
                {
                    rdBox.Text = "";
                    Alerta("Producto ya capturado");

                    return;
                }
                else
                {
                    if (txtClave.Value == null)
                        productoNuevo = 1;
                    pr.Id_Cte = !string.IsNullOrEmpty(txtIdCte.Value) ? Convert.ToInt32(txtIdCte.Value) : 0;
                    cn_catproducto.ConsultaProductos(ref pr, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, Convert.ToInt64(rdBox.Text), ref productoNuevo, 2);
                    (rdBox.Parent.FindControl("LabelPresent2") as Label).Text = pr.Prd_Presentacion;
                    (rdBox.Parent.FindControl("LabelUnidad2") as Label).Text = pr.Prd_UniNs;
                    (rdBox.Parent.FindControl("txtCantidad") as RadNumericTextBox).Value = 0;
                    (rdBox.Parent.FindControl("txtPrecio") as RadNumericTextBox).Text = pr.Prd_Precio;
                    (rdBox.Parent.FindControl("txtPrecioAcys") as RadNumericTextBox).Text = pr.Prd_Precio;
                    (rdBox.Parent.FindControl("txtImporte") as RadNumericTextBox).Text = pr.Prd_Precio;
                    (rdBox.Parent.FindControl("txtProductoNombre") as RadTextBox).Text = pr.Prd_Descripcion;

                    cn_catproducto.ConsultarVentas(ref pr, Convert.ToInt32(txtIdCte.Value), session.Emp_Cnx);
                    (rdBox.Parent.FindControl("Labelmes12") as Label).Text = pr.ventaMes[0].ToString().Trim();
                    (rdBox.Parent.FindControl("Labelmes22") as Label).Text = pr.ventaMes[1].ToString().Trim();
                    (rdBox.Parent.FindControl("Labelmes32") as Label).Text = pr.ventaMes[2].ToString().Trim();
                    (rdBox.Parent.FindControl("txtCantidad") as RadNumericTextBox).Focus();
                }
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
                (rdBox.Parent.FindControl("LabelPresent2") as Label).Text = "";
                (rdBox.Parent.FindControl("LabelUnidad2") as Label).Text = "";
                (rdBox.Parent.FindControl("txtCantidad") as RadNumericTextBox).Value = 0;
                (rdBox.Parent.FindControl("txtPrecio") as RadNumericTextBox).Text = "";
                (rdBox.Parent.FindControl("txtPrecioAcys") as RadNumericTextBox).Text = "";
                (rdBox.Parent.FindControl("txtImporte") as RadNumericTextBox).Text = "";
                (rdBox.Parent.FindControl("Labelmes12") as Label).Text = "";
                (rdBox.Parent.FindControl("Labelmes22") as Label).Text = "";
                (rdBox.Parent.FindControl("Labelmes32") as Label).Text = "";
            }
        }
        protected void cmbProducto_DataBinding(object sender, EventArgs e)
        {
            try
            {
                RadComboBox comboBox = ((RadComboBox)sender);// new RadComboBox() ;
                if (prod)
                {
                    return;
                }
                prod = true;

                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_Cd_Ver, 0, Sesion.Emp_Cnx, "spCatProducto_Combo", ref comboBox);
            }
            catch (Exception ex)
            {
                Alerta(ex.Message.ToString().Trim());
            }
        }

        //CLIENTE
        protected void txtIdCte_TextChanged(object sender, EventArgs e)
        {
            CargarCliente();

            //Edsg 29052015
            var pedido = new PedidoVtaInst();
            CN_CapPedidoVtaInst cn_capPedidoVI = new CN_CapPedidoVtaInst();

            pedido.Id_Emp = session.Id_Emp;
            pedido.Id_Cd = session.Id_Cd_Ver;
            pedido.Id_Acs = 0;
            pedido.Acs_Semana = 0;
            pedido.Acs_Anio = 0;

            if (txtIdCte.Value != "")
                pedido.Id_Cte = Int32.Parse(txtIdCte.Value);


            DataTable dtTemp_Resto = this.dt;
            List<PedidoVtaInst> List2 = new List<PedidoVtaInst>();
            cn_capPedidoVI.ConsultarDet_Resto(pedido, ref List2, ref dtTemp_Resto, session.Emp_Cnx, Id_TG);

            this.dt = dtTemp_Resto;



        }

        //TERRITORIO
        protected void txtTerritorioNom_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (txtTerritorioNom.Value.ToString().Trim() != "")
            {
                CN_CatTerritorios cn_terr = new CN_CatTerritorios();
                Territorios terr = new Territorios();
                terr.Id_Emp = session.Id_Emp;
                terr.Id_Cd = session.Id_Cd_Ver;
                terr.Id_Ter = Convert.ToInt32(txtTerritorioNom.Value);
                cn_terr.ConsultaTerritorios(ref terr, session.Emp_Cnx);
                txtRikNom.Value = terr.Rik_Nombre;
                txtIdRik.Value = terr.Id_Rik.ToString().Trim();
                txtIdTer.Value = txtTerritorioNom.Value.ToString().Trim();
            }
        }

        //GRID
        protected void rtb1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            try
            {

                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (btn.CommandName == "save")
                {
                    if (Page.IsValid)
                        PreGuardar();
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
                Alerta(ex.Message.ToString().Trim());
            }
        }

        protected void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            try
            {

                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                string Id_prd = ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtProd")).Text;
                CN_CapPedidoVtaInst pedido_vta = new CN_CapPedidoVtaInst();
                int verificador = 0;
                pedido_vta.ConsultarAAAEspecial(Sesion.Id_Emp, Sesion.Id_Cd_Ver, Convert.ToInt32(txtIdCte.Value.ToString().Trim()), Id_prd, ref verificador, Sesion.Emp_Cnx);

                if (verificador > 0)
                {
                    Alerta("El producto cuenta con precio AAA especial: " + ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtPrecio")).ClientID);
                }
                double cantidad = ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtCantidad")).Value.HasValue ? ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtCantidad")).Value.Value : 0;
                double precio = ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtPrecio")).Value.HasValue ? ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtPrecio")).Value.Value : 0;
                double importe = cantidad * precio;
                ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtImporte")).Text = importe.ToString("#,##0.00");
            }
            catch (Exception ex)
            {
                Alerta(ex.Message.ToString().Trim());
            }
        }
        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                RadNumericTextBox rdtn = (RadNumericTextBox)sender;

                string cantidad = ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtCantidad")).Text;
                string precio = ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtPrecio")).Text;
                int Prd_Cantidad = 0;
                double Prd_Precio = 0;

                if (cantidad != "")
                {
                    if (int.Parse(cantidad) <= 0)
                    {
                        Alerta("La cantidad debe ser mayor a 0");
                        return;
                    }
                }
                else
                {
                    Alerta("La cantidad debe ser mayor a 0");
                    rdtn.Value = 0;
                    return;
                }

                if (!string.IsNullOrEmpty(cantidad))
                    Prd_Cantidad = Convert.ToInt32(((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtCantidad")).Text);
                if (!string.IsNullOrEmpty(precio))
                    Prd_Precio = Convert.ToDouble(((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtPrecio")).Text);

                string Id_prd = ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtProd")).Text;
                ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtImporte")).DbValue = Prd_Cantidad * Prd_Precio;

                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                List<int> Actuales = new List<int>();
                CN_CatProducto catproducto = new CN_CatProducto();
                catproducto.ConsultaProducto_Disponible(Sesion.Id_Emp, Sesion.Id_Cd_Ver, Id_prd, ref Actuales, Sesion.Emp_Cnx);

                if (Actuales.Count > 0)
                {
                    if (Prd_Cantidad > Actuales[2])
                    {
                        Alerta("Inventario disponible insuficiente, <br>Inventario final: " + Actuales[0] + " <br>Asignado: " + Actuales[1] + " <br>Disponible: " + Actuales[2]);
                        return;
                    }
                    else
                    {
                        ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtPrecio")).Focus();
                    }
                }
                else
                {
                    ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtPrecio")).Focus();
                }
                CN_CapPedidoVtaInst pedido_vta = new CN_CapPedidoVtaInst();
                int verificador = 0;
                if (!string.IsNullOrEmpty(txtIdCte.Value))
                    pedido_vta.ConsultarAAAEspecial(Sesion.Id_Emp, Sesion.Id_Cd_Ver, Convert.ToInt32(txtIdCte.Value.ToString().Trim()), Id_prd, ref verificador, Sesion.Emp_Cnx);
                if (verificador > 0)
                {
                    Alerta("El producto cuenta con precio AAA especial");
                }
            }
            catch (Exception ex)
            {
                Alerta(ex.Message.ToString().Trim());
            }
        }
        #endregion
        #region Funciones
        private void PreGuardar()
        {
            try
            {

                if (this.rdFechaF.Value == null)
                {
                    Alerta("Debe capturar la fecha facturación");
                    return;
                }



                if (this.rdFechaE.Value == null)
                {
                    Alerta("Debe capturar la fecha compromiso de entrega");
                    return;
                }

                if (Convert.ToInt32(Request.QueryString["tipoPedido"]) == 0)
                {
                    if (ChkOrdCompra.Checked)
                    {
                        if (this.TxtPed_OCAcys.Value == null)
                        {
                            Alerta("Debe capturar la orden de compra");
                            return;
                        }

                        ValidateInputData();
                    }
                }


                foreach (DataRow dataR in dt.Rows)
                {
                    if (dt.Select("Id_Prd = " + dataR["Id_Prd"].ToString().Trim()).Length > 1)
                    {
                        Alerta("El producto " + dataR["Id_Prd"].ToString().Trim() + " no puede ser captado mas de una vez en el mismo pedido");
                        return;
                    }
                }

                //Edsg05062015 
                Session["dtPedidoVI" + Session.SessionID] = dt;

                foreach (DataRow dr in dt.Rows)
                {

                    if (dr["Ped_Asignar"].ToString().Trim() == "")
                        dr["Ped_Asignar"] = 0;

                    if (Convert.ToInt32(dr["Ped_Asignar"]) > 0)
                    {
                        Alerta("Este pedido se encuentra asignado, si desea realizar cambios, favor de desasignar el pedido");
                        return;
                    }
                    CN_CatProducto cn_catproducto = new CN_CatProducto();
                    Producto pr = new Producto();
                    List<int> actuales = new List<int>();
                    cn_catproducto.ConsultaProducto_Disponible(session.Id_Emp, session.Id_Cd_Ver, dr["Id_Prd"].ToString().Trim(), ref actuales, session.Emp_Cnx);

                    if ((Convert.ToInt32(dr["Prd_Cantidad"]) - (Convert.ToInt32(dr["Ped_Asignar"] == DBNull.Value ? 0 : dr["Ped_Asignar"]))) > actuales[2])
                    {
                        string clave = txtClave.Value == null ? "0" : txtClave.Value.ToString().Trim();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirVentana_InvIns", "AbrirVentana_InvIns('" + rdFechaF.Value.ToString().Trim().Replace("/", "") + "','" + this.TxtPed_ReqAcys.Text + "','" + clave + "')", true);
                        return;
                    }
                }

                Guardar();
            }
            catch (Exception ex)
            {

                Alerta(ex.Message);
            }
        }

        protected void ddlClienteNom_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtClave.Text = "";
                txtContactoNom.Text = "";
                txtContactoPuesto.Text = "";
                txtContactoTel.Text = "";
                txtContactoMail.Text = "";
                txtCalle.Text = "";
                txtNo.Text = "";
                txtColonia.Text = "";
                txtMunicipio.Text = "";
                txtEstado.Text = "";
                ChkOrdCompra.Checked = false;
                ChckFolio.Checked = false;
                ChckOrdReposicion.Checked = false;


                LblEOtro.Value = "";


                txtContactoNom.Enabled = true;
                txtContactoPuesto.Enabled = true;
                txtContactoTel.Enabled = true;
                txtContactoMail.Enabled = true;
                txtCalle.Enabled = true;
                txtNo.Enabled = true;
                txtColonia.Enabled = true;
                txtMunicipio.Enabled = true;
                txtEstado.Enabled = true;

                ChkOrdCompra.Disabled = false;
                ChckFolio.Disabled = false;
                ChckOrdReposicion.Disabled = false;

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                PedidoVtaInst pedido = new PedidoVtaInst();
                List<PedidoVtaInst> lista = new List<CapaEntidad.PedidoVtaInst>();
                CN_CapPedidoVtaInst vtaInst = new CN_CapPedidoVtaInst();
                pedido.Id_Emp = sesion.Id_Emp;
                pedido.Id_Cd = sesion.Id_Cd_Ver;
                pedido.Id_Cte = int.Parse(txtClienteNom.SelectedItem.Value.ToString().Trim());

                CN_CapPedido cn_capPedido = new CN_CapPedido();

                vtaInst.ConsultaClienteAcys(pedido, ref lista, sesion.Emp_Cnx);
                if (lista.Count == 0)//pedido no existe
                {
                    Alerta("El cliente no existe");
                }
                else if ((lista.OrderByDescending(x => x.Id_Acs).First().Id_Acs != 0) && (ConfigurationManager.AppSettings["ValidaAcys"].ToString() != "N"))
                {
                    var acuerdo = "El cliente ya cuenta con un acuerdo. </br> Numero de acuerdo: " + lista.OrderByDescending(x => x.Id_Acs).First().Id_Acs;
                    int idAcuerdo = lista.OrderByDescending(x => x.Id_Acs).First().Id_Acs;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Acys", "AbrirClienteAcys('" + idAcuerdo.ToString().Trim() + "' , '" + txtClienteNom.SelectedItem.Value.ToString().Trim() + "')", true);
                }
                else
                {
                    CargarCliente();

                    pedido = new PedidoVtaInst();
                    CN_CapPedidoVtaInst cn_capPedidoVI = new CN_CapPedidoVtaInst();

                    pedido.Id_Emp = session.Id_Emp;
                    pedido.Id_Cd = session.Id_Cd_Ver;
                    pedido.Id_Acs = 0;
                    pedido.Acs_Semana = 0;
                    pedido.Acs_Anio = 0;

                    if (txtIdCte.Value != "")
                        pedido.Id_Cte = Int32.Parse(txtIdCte.Value);


                    DataTable dtTemp_Resto = this.dt;
                    List<PedidoVtaInst> List2 = new List<PedidoVtaInst>();
                    cn_capPedidoVI.ConsultarDet_Resto(pedido, ref List2, ref dtTemp_Resto, session.Emp_Cnx, Id_TG);


                    Session["dtPedidoVI"] = dtTemp_Resto;
                    rg1.DataSource = dtTemp_Resto;
                    rg1.DataBind();
                }
            }
            catch (Exception ex)
            {
                Alerta(ex.Message.ToString().Trim());
            }
        }

        protected void btnDireccion_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Direccion", "AbrirBuscarDireccionEntrega()", true);
        }

        protected void rg1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView gridView = (ASPxGridView)sender;
            DataTable dtTemp = (DataTable)Session["dtPedidoVI"];
            if (Convert.ToInt32(Request.QueryString["tipoPedido"]) != 0)
            {


                int i = rg1.FindVisibleIndexByKeyValue(e.Keys["Id_Prd"]);

                e.Cancel = true;
                DataRow row = null;
                row = dtTemp.Rows[i];

                IDictionaryEnumerator enumerator = e.NewValues.GetEnumerator();
                decimal cantidad = 0;
                decimal importe = 0;
                decimal total = 0;
                enumerator.Reset();
                while (enumerator.MoveNext())
                {
                    if (enumerator.Key.ToString() == "Prd_Cantidad")
                    {
                        cantidad = decimal.Parse(enumerator.Value.ToString());
                        row[enumerator.Key.ToString().Trim()] = enumerator.Value;
                    }
                    else if (enumerator.Key.ToString() == "Prd_Precio")
                    {
                        importe = decimal.Parse(enumerator.Value.ToString());
                        row[enumerator.Key.ToString().Trim()] = enumerator.Value;
                    }
                    else if (enumerator.Key.ToString() == "Prd_Importe")
                    {
                        total = cantidad * importe;
                        if (decimal.Parse(enumerator.Value.ToString()) != total)
                        {
                            row[enumerator.Key.ToString().Trim()] = total.ToString();
                        }
                        else
                        {
                            row[enumerator.Key.ToString().Trim()] = enumerator.Value;
                        }
                    }
                    else
                    {
                        row[enumerator.Key.ToString().Trim()] = enumerator.Value;
                    }
                }


                gridView.CancelEdit();
                e.Cancel = true;

                Session["dtPedidoVI"] = dtTemp;


                rg1.DataSource = dtTemp;
                rg1.DataBind();

                calcularsubtotal();
            }
            else
            {

                gridView.CancelEdit();
                e.Cancel = true;

                gridView.CancelEdit();
                e.Cancel = true;

                Session["dtPedidoVI"] = dtTemp;
                Session["nuevaLista"] = dtNuevaLista;

                rg1.DataSource = dtTemp;
                rg1.DataBind();

                calcularsubtotal();
            }
        }

        protected void ddlDocEntrega_SelectedIndexChanged(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)Session["dtPedidoVI"];


            if (ddlDocEntrega.Value.ToString().Trim() != "-1")
            {
                if (dt != null)
                {
                    foreach (DataRow row in dt.AsEnumerable())
                    {
                        row.BeginEdit();
                        row["Acs_Doc"] = ddlDocEntrega.Value.ToString().Trim();
                        row.EndEdit();
                    }
                    Session["dtPedidoVI"] = dt;
                }


                Session["dtPedidoVI"] = dt;



                rg1.DataSource = dt;
                rg1.DataBind();

                calcularsubtotal();
            }

            TabName.Value = "tabDetalle";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "mantaintabs('" + TabName.Value + "')", true);
        }

        protected void rg1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {

            DataTable dtTemp = (DataTable)Session["dtPedidoVI"];

            ASPxGridView gridView = (ASPxGridView)sender;

            DataRow row = dtTemp.NewRow();
            IDictionaryEnumerator enumerator = e.NewValues.GetEnumerator();
            enumerator.Reset();
            while (enumerator.MoveNext())
            {
                if (enumerator.Key.ToString().Trim() != "Count")
                {
                    row[enumerator.Key.ToString().Trim()] = enumerator.Value == null ? DBNull.Value : enumerator.Value;
                }
            }

            gridView.CancelEdit();
            e.Cancel = true;
            dtTemp.Rows.Add(row);
            Session["dtPedidoVI"] = dtTemp;
            rg1.DataSource = dtTemp;
            rg1.DataBind();

            calcularsubtotal();
        }

        protected void rg1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            ASPxGridView gridView = (ASPxGridView)sender;
            DataTable dtTemp = (DataTable)Session["dtPedidoVI"];
            if (Convert.ToInt32(Request.QueryString["tipoPedido"]) != 0)
            {

                DataTable dtNuevaLista;
                if (Session["nuevaLista"] != null)
                {
                    dtNuevaLista = (DataTable)Session["nuevaLista"];
                }
                else
                {
                    dtNuevaLista = new DataTable();
                    dtNuevaLista.Columns.Add("Id_Prdold", System.Type.GetType("System.Int64"));
                    dtNuevaLista.Columns.Add("Id_Prd", System.Type.GetType("System.Int64"));
                    dtNuevaLista.Columns.Add("Prd_Cantidad", System.Type.GetType("System.Int64"));
                    dtNuevaLista.Columns.Add("Prd_Precio", System.Type.GetType("System.Int64"));
                    dtNuevaLista.Columns.Add("Estatus", System.Type.GetType("System.Int32"));
                    dtNuevaLista.Columns.Add("Tipo", System.Type.GetType("System.String"));
                }


                int i = rg1.FindVisibleIndexByKeyValue(e.Keys["Id_Prd"]);
                e.Cancel = true;

                DataRow dr;
                dr = dtNuevaLista.NewRow();
                dr["Id_Prdold"] = dtTemp.Rows[i][0];
                dr["Id_Prd"] = dtTemp.Rows[i][1];
                dr["Prd_Cantidad"] = 0;
                dr["Prd_Precio"] = dtTemp.Rows[i][9];
                dr["Estatus"] = 3;
                dr["Tipo"] = "VI";
                dtNuevaLista.Rows.Add(dr);

                ((BootstrapGridView)sender).JSProperties["cpIsUpdated"] = 1;


                dtTemp.Rows[i].Delete();


                Session["dtPedidoVI"] = dtTemp;
                Session["nuevaLista"] = dtNuevaLista;
                calcularsubtotal();
                rg1.DataSource = dtTemp;
                rg1.DataBind();

            }

            else
            {

                gridView.CancelEdit();
                e.Cancel = true;

                Session["dtPedidoVI"] = dtTemp;
                Session["nuevaLista"] = dtNuevaLista;

                rg1.DataSource = dtTemp;
                rg1.DataBind();

                calcularsubtotal();
            }
        }

        public void calcularsubtotal()
        {
            double imp = 0;

            DataTable dt = (DataTable)Session["dtPedidoVI"];

            if (dt != null)
            {
                foreach (DataRow i in dt.Rows)
                {
                    imp += Convert.ToDouble(i["Prd_Importe"] == DBNull.Value ? 0 : i["Prd_Importe"]);
                }
            }


            txtSubtotal.Value = imp.ToString("F2");

            double iva_cd = 0;
            CN_CatCentroDistribucion cn = new CN_CatCentroDistribucion();
            cn.ConsultarIva(session.Id_Emp, session.Id_Cd_Ver, ref iva_cd, session.Emp_Cnx);

            double iva = (Convert.ToDouble(HD_IVAfacturacion.Value.ToString()) * imp / 100);
            double total = double.Parse(txtSubtotal.Value) + iva;
            txtIva.Value = iva.ToString("F2");
            txtTotal.Value = total.ToString("F2");

        }

        protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            if (Session["dtPedidoVI"] != null)
            {
                DataTable dtTemp = (DataTable)Session["dtPedidoVI"];

                rg1.DataSource = dtTemp;
                rg1.DataBind();
            }
        }


        private void Guardar()
        {
            try
            {

                if (dt.Rows.Count == 0)
                {
                    Alerta("No ha agregado ningún producto al detalle");

                    return;
                }

                DataRow[] dr = dt.Select("Acs_Doc = ''");
                if (dr.Length > 0)
                {
                    Alerta("No se seleccionó documento de entrega para el producto <b>" + dr[0][1] + " - " + dr[0][2] + "</b>");

                    return;
                }

                int verificador = -1;
                Funciones funcion = new Funciones();
                PedidoVtaInst pedido = new PedidoVtaInst();
                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;
                pedido.Id_Cte = Convert.ToInt32(txtIdCte.Value);
                pedido.Ped_Fecha = funcion.GetLocalDateTime(session.Minutos);
                pedido.Id_Rik = Convert.ToInt32(txtIdRik.Value);

                if (txtIdTer.Value == "")
                {
                    Alerta("El Territorio es Requerido");
                    return;

                }
                pedido.Id_Ter = Convert.ToInt32(txtIdTer.Value);

                pedido.Pedido_del = TxtPed_PedAcys.Text.Trim();
                pedido.Requisicion = TxtPed_ReqAcys.Text.Trim();
                pedido.Ped_Solicito = txtContactoNom.Text;
                pedido.Ped_Flete = string.Empty;
                pedido.Ped_OrdenEntrega = TxtPed_OCAcys.Text.Trim();
                pedido.Ped_CondEntrega = 0;
                pedido.Ped_FechaEntrega = Convert.ToDateTime(rdFechaE.Value);
                pedido.Ped_Comentarios = txtNotas.Text;

                pedido.Ped_Observaciones = string.Empty;
                pedido.Ped_DescPorcen1 = 0;
                pedido.Ped_DescPorcen2 = 0;
                pedido.Ped_Desc1 = string.Empty;
                pedido.Ped_Desc2 = string.Empty;
                pedido.Ped_Importe = txtSubtotal.Value != "" ? Convert.ToDouble(txtSubtotal.Value.ToString()) : 0;
                pedido.Ped_Subtotal = txtSubtotal.Value != "" ? Convert.ToDouble(txtSubtotal.Value.ToString()) : 0;
                pedido.Ped_Iva = txtIva.Value != "" ? Convert.ToDouble(txtIva.Value.ToString()) : 0;
                pedido.Ped_Total = txtTotal.Value != "" ? Convert.ToDouble(txtTotal.Value.ToString()) : 0;

                pedido.Id_U = session.Id_U;
                pedido.Acs_Semana = Convert.ToInt32(txtSemana.Value == "" ? "0" : txtSemana.Value.ToString().Trim());
                pedido.Id_Acs = Convert.ToInt32(txtClave.Value == null ? "0" : txtClave.Value.ToString().Trim());
                pedido.Acs_Anio = Convert.ToInt32(txtFecha.Value == "" ? "0" : txtFecha.Value.ToString().Trim());
                pedido.Ped_SolicitoTel = txtContactoTel.Text;
                pedido.Ped_SolicitoEmail = txtContactoMail.Text;
                pedido.Ped_SolicitoPuesto = txtContactoPuesto.Text;
                pedido.Ped_ConsignadoCalle = txtCalle.Text;
                pedido.Ped_ConsignadoNo = txtNo.Text;
                pedido.Ped_ConsignadoCp = txtCp.Text;
                pedido.Ped_ConsignadoMunicipio = txtMunicipio.Text;
                pedido.Ped_ConsignadoEstado = txtEstado.Text;
                pedido.Ped_ConsignadoColonia = txtColonia.Text;
                pedido.Ped_ReqOrden = ChkOrdCompra.Checked;
                pedido.Ped_OrdenCompra = TxtPed_OCAcys.Text;
                pedido.Ped_AcysSemana = Convert.ToInt32(txtSemana.Value == "" ? "0" : txtSemana.Value.ToString().Trim());
                pedido.Ped_AcysAnio = Convert.ToInt32(txtFecha.Value == "" ? "0" : txtFecha.Value.ToString().Trim());
                pedido.Id_Acs = Convert.ToInt32(txtClave.Value);
                pedido.Estatus = "U";
                pedido.Ped_Tipo = txtClave.Value == null ? 4 : txtClave.Value.ToString() == "" ? 4 : 3;
                pedido.Ped_ReqOrden = ChkOrdCompra.Checked;

                // Edsg Proyecto Internet
                if (rdModInternet.Checked) pedido.Ped_Tipo = 5;
                pedido.FechaFacAcys = Convert.ToDateTime(rdFechaF.Value);
                pedido.PedAcys = TxtPed_PedAcys.Text.Trim();
                pedido.ReqAcys = TxtPed_ReqAcys.Text.Trim();
                pedido.OcAcys = TxtPed_OCAcys.Text.Trim();

                pedido.UsoCFDI = ddUsoCfdi.Value?.ToString();



                CN_CapPedidoVtaInst clsCapPedido = new CN_CapPedidoVtaInst();

                //JFCV convenios, validar el precio minimo y maximo 
                #region inicio validar precios convenio


                //JFCV  
                //leer configuración para ver si se valida o no el precio 
                // configuración 953 conf de Pedidos  
                // si el valor es 0 no valida y si es 1 si va a validar 
                int GLOBAL_ValidaPrecioMinimoRik = 0;
                CapaEntidad.eSysConfiguracion SysC = new CapaEntidad.eSysConfiguracion();
                try
                {
                    CN_SysConfigruacion CN = new CN_SysConfigruacion();
                    SysC = CN.spSysConfiguracionById(session.Id_Emp, session.Id_Cd, 953, session.Emp_Cnx);
                    int iTmp = 0;
                    int.TryParse(SysC.Conf_Valor, out iTmp);
                    GLOBAL_ValidaPrecioMinimoRik = iTmp;
                }
                catch (Exception ex)
                {
                    GLOBAL_ValidaPrecioMinimoRik = 0;
                }
                string ConexionCentral = ConfigurationManager.AppSettings["strConnectionCentral"].ToString().Trim();
                ConvenioDet conv;
                ConvenioDet convdet;
                CN_Convenio cn_conv;
                List<string> Productos = new List<string>();

                List<ConvenioDet> lconveniosdet = new List<ConvenioDet>();
                ConvenioDet lconvdet = new ConvenioDet();

                //JFCV Validar si tiene precios inferiores a los precios minimos
                AlertaAutorizacion alertaaut;
                AlertaAutorizacion alertaautdet;
                CN_AlertaAutorizacion cn_alertaautorizacion;

                List<string> ProductosAlerta = new List<string>();
                List<AlertaAutorizacion> lalertasautdet = new List<AlertaAutorizacion>();
                AlertaAutorizacion pasoalertaautdet = new AlertaAutorizacion();
                //reviso si la variable de sesion ya tiene un folio guardado
                //si no lo tiene es que es la primera vez que entra a grabar 
                //si ya lo tiene valido que sea el mismo que el que estoy 
                int procesar = 1;

                if (Session["Id_FacPrec" + Session.SessionID] != null)
                {
                    string folior = Session["Id_FacPrec" + Session.SessionID].ToString();
                    if (folior == txtFolio.Value.ToString().Trim())
                    {
                        procesar = 0;
                    }
                }

                //la primera vez que entra a esta rutina guarda en variable de sesion 
                //el folio si acaso tiene productos que requirieron de validación
                //entonces la segunda vez que entra ya esta ese dato guardado 
                //y si el folio es el mismo quiere decir que se reejecuto el grabar y que ya no debe validar.

                //JFCV fin

                string prodAAA = "";
                if (dt.Rows.Count > 0)
                {
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {

                        conv = new ConvenioDet();
                        convdet = new ConvenioDet();
                        cn_conv = new CN_Convenio();
                        conv.Id_Emp = session.Id_Emp;
                        conv.Id_Cd = session.Id_Cd_Ver;
                        conv.Id_Cte = Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1");
                        conv.Id_Prd = Convert.ToInt64(dt.Rows[x]["Id_Prd"]);
                        double PrecioIngresado = Convert.ToDouble(dt.Rows[x]["Prd_Precio"]);
                        //double impore =  dt.Rows[x]["Prd_Precio"].ToString().Trim()!= DBNull.Value  ? (double)(dt.Rows[x]["Prd_Precio"]) : 0;

                        if (Convert.ToInt64(dt.Rows[x]["Id_Prd"]) <= 999999999999)
                        {
                            //double PrecioIngresado = Convert.ToDouble(item2.OwnerTableView.DataKeyValues[item2.ItemIndex]["Prd_Importe"]);
                            //double impore = (item2.FindControl("Prd_Importe") as RadNumericTextBox).Value.HasValue ? (double)(item2.FindControl("Prd_Importe") as RadNumericTextBox).Value : 0;
                            cn_conv.Convenio_ConsultaPrecio(conv, ref convdet, ConexionCentral);



                            int agregar = 0;

                            if (convdet != null && convdet.PCD_PrecioAAAEsp > 0)
                            {
                                if (convdet.PCD_PrecioVtaMin != 0 || convdet.PCD_PrecioVtaMax != 0)
                                {

                                    #region pvtamin y pvtamax  dif de cero

                                    if (Math.Round(PrecioIngresado, 3) < convdet.PCD_PrecioVtaMin)
                                    {
                                        agregar = 1;
                                    }
                                    else if (convdet.PCD_PrecioVtaMin == 0 && convdet.PCD_PrecioVtaMax != 0)
                                    {
                                        // vta minima es igual a cero y vta max dif 0 
                                        // si precio es mayor a vta max manda aviso 
                                        if (Math.Round(PrecioIngresado, 3) > convdet.PCD_PrecioVtaMax)
                                        {
                                            agregar = 1;
                                        }
                                    }
                                    else if (Math.Round(PrecioIngresado, 3) < convdet.PCD_PrecioVtaMin || Math.Round(PrecioIngresado, 3) > convdet.PCD_PrecioVtaMax)
                                    {
                                        agregar = 1;
                                    }

                                    if (agregar == 1)
                                    {
                                        Productos.Add(convdet.Id_Prd.ToString().Trim());

                                        lconvdet = new ConvenioDet();
                                        lconvdet.PC_Nombre = convdet.PC_Nombre;
                                        lconvdet.Id_PC = convdet.Id_PC;
                                        lconvdet.Id_Prd = convdet.Id_Prd;
                                        lconvdet.PCD_PrecioVtaMax = convdet.PCD_PrecioVtaMax;
                                        lconvdet.PCD_PrecioVtaMin = convdet.PCD_PrecioVtaMin;
                                        lconvdet.PCD_PrecioVentaConvenio = PrecioIngresado;
                                        lconveniosdet.Add(lconvdet);
                                    }
                                    #endregion
                                }  // si pvtamin y pvtamax son cero
                            }
                            else
                            {
                                Producto producto = new Producto();
                                //obtener datos de producto
                                CN_CatProducto clsProducto = new CN_CatProducto();
                                producto.Id_Cte = Convert.ToInt32(Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1"));
                                int productoNuevo = 0;
                                clsProducto.ConsultaProductos(ref producto, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, Convert.ToInt64(dt.Rows[x]["Id_Prd"]), ref productoNuevo, 0);

                                if (Convert.ToDouble(PrecioIngresado) < producto.Prd_AAA)
                                {/*
                                    if (prodAAA != "")
                                    {
                                        prodAAA = prodAAA + ", " + Convert.ToInt64(dt.Rows[x]["Id_Prd"]).ToString();
                                    }
                                    else
                                    {
                                        prodAAA = Convert.ToInt64(dt.Rows[x]["Id_Prd"]).ToString();
                                    }
                            Juan Jo Lo que solicitamos es que la regla siga aplicando de manera general, pero en caso de que el cliente este ligado a algún convenio entonces si le permita facturar por debajo del precio aaa especial.
                            Autorizado por Dirección
                                    */
                                }
                            } // sin convdet es dif nulo y si precio aaa esp > 0

                            //JFCV Valido si el precio de venta es menor al minimo
                            #region validar precios que requieran autorización
                            if (GLOBAL_ValidaPrecioMinimoRik == 1)
                            {
                                alertaaut = new AlertaAutorizacion();
                                alertaautdet = new AlertaAutorizacion();
                                cn_alertaautorizacion = new CN_AlertaAutorizacion();
                                if (procesar == 1)
                                {
                                    alertaaut.Id_Emp = session.Id_Emp;
                                    alertaaut.Id_Cd = session.Id_Cd_Ver;
                                    alertaaut.Id_Cte = Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1");
                                    alertaaut.Cte_NomComercial = txtClienteNom.Text.Trim() != "" ? txtClienteNom.Text.Trim().Trim() : "";
                                    alertaaut.Id_Prd = Convert.ToInt64(dt.Rows[x]["Id_Prd"]);
                                    alertaaut.IdRepresentante = Convert.ToInt32(txtIdRik.Value);
                                    alertaaut.Id_Ter = Convert.ToInt32(txtIdTer.Value);  //Convert.ToInt32(ddlTerritorioNom.Value);
                                    double Preciodefactura = Convert.ToDouble(dt.Rows[x]["Prd_Precio"]);
                                    alertaaut.Precio_Vta = Preciodefactura;
                                    cn_alertaautorizacion.AlertaPrecioConsultaPrecio(alertaaut, ref alertaautdet, ConexionCentral);

                                    if (alertaautdet != null && alertaautdet.Precio_MinimoRik > 0)
                                    {

                                        //JFCV 2NOV precioObjetivo if (Math.Round(Preciodefactura, 3) < alertaautdet.Precio_MinimoRik)
                                        if (Math.Round(Preciodefactura, 3) < alertaautdet.PrecioObjetivo)
                                        {

                                            ProductosAlerta.Add(alertaautdet.Id_Prd.ToString().Trim());

                                            pasoalertaautdet = new AlertaAutorizacion();

                                            pasoalertaautdet.Id_Emp = session.Id_Emp;
                                            pasoalertaautdet.Id_Cd = session.Id_Cd_Ver;
                                            pasoalertaautdet.Id_Cte = Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1");
                                            pasoalertaautdet.IdRepresentante = Convert.ToInt32(txtIdRik.Value);
                                            pasoalertaautdet.Id_Ter = Convert.ToInt32(txtIdTer.Value);

                                            pasoalertaautdet.IdAutorizacionAnterior = alertaautdet.IdAutorizacionAnterior;
                                            pasoalertaautdet.Id_Prd = alertaautdet.Id_Prd;
                                            pasoalertaautdet.Precio_MinimoRik = Math.Round(Convert.ToDouble(alertaautdet.Precio_MinimoRik), 3);
                                            pasoalertaautdet.Precio_MinimoGte = Math.Round(Convert.ToDouble(alertaautdet.Precio_MinimoGte), 3);
                                            pasoalertaautdet.Precio_Vta = Preciodefactura;
                                            pasoalertaautdet.PrecioLista = alertaautdet.PrecioLista;
                                            pasoalertaautdet.Cte_NomComercial = alertaautdet.Cte_NomComercial;
                                            pasoalertaautdet.Prd_Descripcion = alertaautdet.Prd_Descripcion;
                                            pasoalertaautdet.PrecioLista = alertaautdet.PrecioLista;
                                            pasoalertaautdet.Cantidad = Convert.ToInt32(dt.Rows[x]["Prd_Cantidad"]);
                                            pasoalertaautdet.Precio_AAA = alertaautdet.Precio_AAA;
                                            pasoalertaautdet.Utilidad = alertaautdet.Utilidad;
                                            pasoalertaautdet.Porc_Utilidad = alertaautdet.Utilidad / Preciodefactura;
                                            pasoalertaautdet.Importe = Preciodefactura * pasoalertaautdet.Cantidad;
                                            pasoalertaautdet.Importe_Utilidad = pasoalertaautdet.Utilidad * pasoalertaautdet.Cantidad;
                                            pasoalertaautdet.Justificacion = "";
                                            pasoalertaautdet.Prd_Descripcion = Convert.ToString(dt.Rows[x]["Prd_Descripcion"]);
                                            pasoalertaautdet.Cte_NomComercial = txtClienteNom.Text.Trim() != "" ? txtClienteNom.Text.Trim() : "";
                                            pasoalertaautdet.TipoAutorizacion = 4; //Pedido
                                            pasoalertaautdet.FechaVigencia = DateTime.Now.AddMonths(12);
                                            pasoalertaautdet.Id_Cpr = alertaautdet.Id_Cpr;
                                            pasoalertaautdet.JustificacionMemo = "";
                                            //JFCV 2NOV precioObjetivo 
                                            pasoalertaautdet.Id_Tamaño = alertaautdet.Id_Tamaño;
                                            pasoalertaautdet.PrecioObjetivo = alertaautdet.PrecioObjetivo;
                                            lalertasautdet.Add(pasoalertaautdet);

                                        }

                                    }
                                    #endregion validar precios que requieran autorización
                                    //JFCV fin Valido si el precio de venta es menor al minimo



                                }// si prod es menor a 999999999999
                            }  //Termina ciclo  convenios  
                        }



                        if (prodAAA != "")
                        {
                            Alerta("El precio de venta de los siguiente produtos no puede ser menor al Precio AAA del producto: " + prodAAA);
                            return;
                        }

                        Session["ProdsConv" + Session.SessionID] = null;
                        Session["Id_FacPrec" + Session.SessionID] = null;
                        Session["lConvPrecios" + Session.SessionID] = null;

                        if (Productos.Count > 0 && lconveniosdet.Count > 0)
                        {
                            Session["ProdsConv" + Session.SessionID] = Productos;
                            Session["lConvPrecios" + Session.SessionID] = lconveniosdet;
                            Session["Id_FacPrec" + Session.SessionID] = txtFolio.Value.ToString().Trim();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirVentana_AlertaPrecios", "AbrirVentana_AlertaPrecios()", true);

                            return;
                        }

                        //JFCV alerta de precios autorización
                        //JFCV Validar si tiene precios inferiores a los precios minimos

                        Session["ProdsAutorizacion" + Session.SessionID] = null;
                        Session["Id_FacPrec" + Session.SessionID] = null;
                        Session["lAurizacionPrecios" + Session.SessionID] = null;

                        if (procesar == 1) //23sep22 agregue esta condición para que cuando este regrabando no se detenga por los precios alertas
                        {
                            if (ProductosAlerta.Count > 0 && lalertasautdet.Count > 0)
                            {
                                Session["ProdsAutorizacion" + Session.SessionID] = ProductosAlerta;
                                Session["lAurizacionPrecios" + Session.SessionID] = lalertasautdet;
                                Session["Id_FacPrec" + Session.SessionID] = txtFolio.Value.ToString().Trim();


                                //asi lo tego en facturas etc   RAM1.ResponseScripts.Add("Ventana_AutorizacionPrecios(); return false;");
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirVentana_AlertaAutorizacionPrecios", "AbrirVentana_AlertaAutorizacionPrecios()", true);

                                return;
                            }
                        }
                        //JFCV  FIn Validar si tiene precios inferiores a los precios minimos

                        #endregion inicio validar precios conevnio
                        //alertaprecios ultimo cambio  

                        Session["Id_FacPrec" + Session.SessionID] = null;
                    }
                }
                if (HF_ID.Value == "")
                {
                    if (!_PermisoGuardar)
                    {
                        Alerta("No tiene permisos para grabar");

                        return;
                    }

                    string nombre = "";
                    string extension = "";
                    string archivo = "";



                    if (ChkOrdCompra.Checked)
                    {
                        cargarArchivo(ref nombre, ref extension, ref archivo);

                    }



                    //string estatus = "";
                    //string mensaje = "";
                    dtNuevaLista = (DataTable)Session["nuevaLista"];

                    //APISKEY APis = new APISKEY();
                    //APis.ModificarPedido(pedido, dt, session.Emp_Cnx, ref estatus, ref mensaje);

                    //if (estatus == "0" || estatus == "2")
                    //{
                    //    Alerta(mensaje);
                    //    return;
                    //}

                    long _prd = 0;
                    foreach (DataRow rows in dt.AsEnumerable())
                    {
                        _prd = Convert.ToInt64((rows[1]));
                        PedidoVtaInst pvi = new PedidoVtaInst();
                        pvi.Id_Emp = session.Id_Emp;
                        pvi.Id_Cd = session.Id_Cd_Ver;
                        pvi.Id_Cte = Convert.ToInt32(txtIdCte.Value);
                        pvi.Id_Ter = Convert.ToInt32(txtIdTer.Value);
                        pvi.Id_Acs = Convert.ToInt32(txtClave.Value);
                        clsCapPedido.ConsultarPedidoExistente(pvi, _prd, session.Emp_Cnx, ref verificador);

                        if (verificador == 1)
                        {
                            Alerta("El producto " + _prd.ToString().Trim() + " ya ha sido captado por otro usuario");

                            return;
                        }
                    }

                    if (Id_TG != 0)
                    {
                        if (Request.QueryString["Id"] != "")
                        {
                            CapaEntidad.Acys acys = new Acys();
                            acys.Id_Emp = session.Id_Emp;
                            acys.Id_Cd = session.Id_Cd;
                            acys.Id_Acs = Convert.ToInt32(Request.QueryString["Id"]);
                            CN_CapAcys cnCapAcys = new CN_CapAcys();
                            cnCapAcys.ConsultaUltimaVersion(ref acys, session.Emp_Cnx);

                            clsCapPedido.Insertar(pedido, Convert.ToInt32(Request.QueryString["tipoPedido"]), dt, session.Emp_Cnx, ref verificador, Id_TG, acys.Id_AcsVersion);
                        }
                        else
                        {
                            clsCapPedido.Insertar(pedido, Convert.ToInt32(Request.QueryString["tipoPedido"]), dt, session.Emp_Cnx, ref verificador, Id_TG, null);
                        }
                    }
                    else
                    {
                        clsCapPedido.Insertar(pedido, Convert.ToInt32(Request.QueryString["tipoPedido"]), dt, session.Emp_Cnx, ref verificador, Id_TG, null);
                    }

                    if (verificador >= 1)
                    {


                        if (ChkOrdCompra.Checked)
                        {

                            pedido.Id_Ped = verificador;
                            pedido.Id_Acs = 0;
                            pedido.OrdenCompra = TxtPed_ReqAcys.Text;
                            pedido.Id_Cte = Convert.ToInt32(txtIdCte.Value);
                            pedido.nombreDocumento = nombre;
                            pedido.extension = extension;
                            pedido.archivo = archivo;
                            clsCapPedido.InsertarOrderCompra(pedido, session.Emp_Cnx, ref verificador, Id_TG, 0);
                        }


                        pedido.Id_Ped = verificador;
                        if (Session["PedexternoCP"] != null)
                        {
                            pedido.PedExterno = Convert.ToInt32(Session["PedexternoCP"].ToString());
                        }
                        pedido.Id_Cd = session.Id_Cd;
                        int captado = 0;
                        if (Request.QueryString["IdVI"] != null)
                            captado = Convert.ToInt32(verificador);

                        if (Convert.ToInt32(Request.QueryString["tipoPedido"]) != 0)
                        {

                            string estatus = "";
                            string mensaje = "";
                            dtNuevaLista = (DataTable)Session["nuevaLista"];

                            APISKEY APis = new APISKEY();
                            APis.ModificarPedido(pedido, dt, dtNuevaLista, session.Emp_Cnx, ref estatus, ref mensaje);


                            if (estatus == "0" || estatus == "2")
                            {

                                Session["Id_Ped" + Session.SessionID] = verificador;
                                mensajeExito("Se realizo la captación del pedido con el folio: " + verificador + ", </br>  Error al enviar la información al portal del cliente: " + mensaje);
                            }
                            else
                            {
                                Session["Id_Ped" + Session.SessionID] = verificador;
                                mensajeExito("Se realizo la captación del pedido con el folio: " + verificador);
                            }
                        }
                        else
                        {
                            Session["Id_Ped" + Session.SessionID] = verificador;
                            mensajeExito("Se realizo la captación del pedido con el folio: " + verificador);
                        }

                    }
                    else
                    {
                        Alerta("Ocurrió un error al intentar guardar el pedido");

                    }
                }
                else
                {
                    if (!_PermisoModificar)
                    {
                        Alerta("No tiene permisos para modificar");

                        return;
                    }
                    pedido.Id_Ped = Convert.ToInt32(HF_ID.Value);
                    if (Session["PedexternoCP"] != null)
                    {
                        pedido.PedExterno = Convert.ToInt32(Session["PedexternoCP"].ToString());
                    }
                    pedido.Id_Cd = session.Id_Cd;
                    int captado = 0;
                    if (Request.QueryString["IdVI"] != null)
                    {
                        captado = Convert.ToInt32(txtFolio.Value);
                    }

                    clsCapPedido.Modificar(pedido, dt, session.Emp_Cnx, captado, ref verificador, al);

                    if (verificador >= 1)
                    {
                        if (Convert.ToInt32(Request.QueryString["tipoPedido"]) != 0)
                        {

                            string estatus = "";
                            string mensaje = "";
                            dtNuevaLista = (DataTable)Session["nuevaLista"];

                            APISKEY APis = new APISKEY();
                            APis.ModificarPedido(pedido, dt, dtNuevaLista, session.Emp_Cnx, ref estatus, ref mensaje);




                            if (estatus == "0" || estatus == "2")
                            {
                                Session["Id_Ped" + Session.SessionID] = verificador;
                                mensajeExito("Se realizo la captación del pedido con el folio: " + verificador + ", </br>  Error al enviar la información al portal del cliente: " + mensaje);
                            }
                            else
                            {
                                Session["Id_Ped" + Session.SessionID] = verificador;
                                mensajeExito("Se realizo la captación del pedido con el folio: " + verificador);
                            }
                        }
                        else
                        {
                            Session["Id_Ped" + Session.SessionID] = verificador;
                            mensajeExito("Se realizo la captación del pedido con el folio: " + verificador);
                        }
                    }
                    else
                    {
                        Alerta("Ocurrió un error al intentar guardar el pedido");
                    }
                }
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
                throw ex;

            }
        }

        private void Consultar_IVA_Cliente()
        {
            string IVA_Cliente = "NO";
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (txtIdCte.Value.ToString() != string.Empty && txtIdCte.Value.ToString() != "-1")
            {
                Clientes cliente = new Clientes();
                cliente.Id_Emp = sesion.Id_Emp;
                cliente.Id_Cd = sesion.Id_Cd_Ver;
                cliente.Id_Rik = sesion.Id_Rik;
                cliente.Id_Cte = Convert.ToInt32(txtIdCte.Value.ToString());
                new CN_CatCliente().ConsultaClientes(ref cliente, sesion.Emp_Cnx);

                if (cliente.BPorcientoIVA == true)
                {
                    if (cliente.PorcientoIVA == 0 || cliente.PorcientoIVA == null)
                    {
                        Alerta("El porcentaje de IVA no está establecido, debe ser Mayor a Cero");
                        return;
                    }
                    else
                    {
                        HD_IVAfacturacion.Value = cliente.PorcientoIVA.ToString();
                        IVATEXTO.Text = "IVA : " + cliente.PorcientoIVA.ToString() + "%";
                        IVA_Cliente = "SI";
                    }
                }
            }

            if (IVA_Cliente == "NO")
            {
                // Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CentroDistribucion cd = new CentroDistribucion();
                new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);
                HD_IVAfacturacion.Value = cd.Cd_IvaPedidosFacturacion.ToString();
                IVATEXTO.Text = "IVA : " + cd.Cd_IvaPedidosFacturacion.ToString() + "%";
            }
        }

        private void Inicializar()
        {
            divupload.Visible = false;
            CN_CatCentroDistribucion cd = new CN_CatCentroDistribucion();
            CentroDistribucion centroDistribucion = new CentroDistribucion();
            txtFolio.Value = MaximoId();

            LlenarComboUsoCfdi();

            if (Request.QueryString["idP"] != null)
            {
                txtFolio.Value = Request.QueryString["idP"].ToString().Trim();
                CargarPedido();
            }
            else
            {//IdVI
                if (Request.QueryString["IdVI"] != null)
                {
                    if (Session["PedidoCaptado" + Session.SessionID] != null)
                        txtFolio.Value = Session["PedidoCaptado" + Session.SessionID].ToString().Trim();
                    CargarPedido();
                    Session["PedidoCaptado" + Session.SessionID] = null;
                }
                else
                    // Pedidos de internet
                    if (Request.QueryString["IdPeInt"] != null)
                {
                    //txtFolio.Value = Request.QueryString["IdPeInt"].ToString().Trim();


                    if (Request.QueryString["numPedido"] != null)
                    {
                        txtFolio.Value = Request.QueryString["numPedido"].ToString().Trim();

                        CargarPedido();
                    }
                    else
                    {
                        CargarPedidoInternet(Convert.ToInt32(Request.QueryString["IdPeInt"]), Convert.ToInt32(Request.QueryString["tipoPedido"]));
                    }
                }
                else
                {

                    if (Request.QueryString["IdOC"] != null)
                    {
                        CN_CatCNac_OrdenCompra cn = new CN_CatCNac_OrdenCompra();
                        int idOC = Int32.Parse(Request.QueryString["IdOC"]);
                        var res = cn.ConsultaPedidoOC_Captacion(idOC);

                        txtIdCte.Value = res.Id_Cte.ToString().Trim();
                        txtFolio.Value = MaximoId();
                        TxtPed_ReqAcys.Text = res.NumPedido;
                        TxtPed_ReqAcys.Enabled = false;
                        txtNotas.Text = res.Observaciones;

                        CargarCliente();

                        GetListDet();

                        DataTable dtTemp = dt;
                        //cn_capPedidoInternet.ConsultarPedido_Detalle(session.Emp_Cnx, num_pedido, ref dtTemp);
                        dt = dtTemp;

                        CN_CatCNac_OrdenCompra cnOC = new CN_CatCNac_OrdenCompra();
                        var resDet = cnOC.ConsultarPedidoOrden_Detalle(res.NumPedido, res.Id_Cd, res.Id_Cte);

                        foreach (spCatCNac_OrdenCompra_Detalle_Result item in resDet)
                        {
                            dt.Rows.Add(new object[] {
                                item.Id_Prd,
                                item.Id_Prd,
                                item.Prd_Descripcion,
                                item.Prd_Presentacion,
                                item.Id_Uni,
                                item.mes1,
                                item.mes2,
                                item.mes3,
                                item.Prd_Cantidad,
                                item.Prd_Precio,
                                item.Acs_PrecioAcys,
                                item.Prd_Importe,
                                Str(item.Acs_Documento),
                                item.Acs_Fecha,
                                item.Acs_Mod,
                                item.Acs_Dia ,
                                NombreDia(item.Acs_Dia),
                                item.Acs_Frecuencia,
                                item.Prd_RemFact,
                                item.Ped_Asignar
                                });
                        }

                        CN_CatProducto cn_catproducto = new CN_CatProducto();
                        Producto pr;
                        foreach (DataRow dr in dt.Rows)
                        {
                            pr = new Producto();
                            pr.Id_Emp = session.Id_Emp;
                            pr.Id_Cd = session.Id_Cd_Ver;
                            pr.Id_Prd = Convert.ToInt64(dr["Id_prd"]);

                            cn_catproducto.ConsultarVentas(ref pr, Convert.ToInt32(txtIdCte.Value), session.Emp_Cnx);

                            dr["mes1"] = pr.ventaMes[0].ToString().Trim();
                            dr["mes2"] = pr.ventaMes[1].ToString().Trim();
                            dr["mes3"] = pr.ventaMes[2].ToString().Trim();
                        }
                        Session["dtPedidoVI"] = dt;
                        rg1.DataSource = dt;
                        rg1.DataBind();



                    }
                    else
                    {

                        txtFolio.Value = MaximoId();

                        if (Request.QueryString["id"] != "-1")
                        {
                            txtClave.Value = Request.QueryString["id"];
                            CargarAcuerdo();
                            if (string.IsNullOrEmpty(txtClave.Value.ToString().Trim()))
                                productoNuevo = 1;
                        }
                    }
                }
            }

            ConsultarClienteFechaEntrega(session.Emp_Cnx, Convert.ToInt32(txtIdCte.Value), session.Id_Cd);
            if (ChkOrdCompra.Checked)
            {
                if ((txtClave.Text) == "")
                    productoNuevo = 1;
                if (Convert.ToInt32(Request.QueryString["tipoPedido"]) == 0)
                {
                    divupload.Visible = true;
                }
            }
            _PermisoGuardar = Convert.ToBoolean(Request.QueryString["PermisoGuardar"]);
            _PermisoModificar = Convert.ToBoolean(Request.QueryString["PermisoModificar"]);
            _PermisoEliminar = Convert.ToBoolean(Request.QueryString["PermisoEliminar"]);
            _PermisoImprimir = Convert.ToBoolean(Request.QueryString["PermisoImprimir"]);
            ValidarPermisos();
            Consultar_IVA_Cliente();
            CalcularTotales();

        }

        private List<eUsoCfdi> CargarUsoCfdi()
        {
            var result = new List<eUsoCfdi>();

            using (var cn = new SqlConnection(session.Emp_Cnx))
            {
                try
                {
                    cn.Open();

                    using (var cmd = new SqlCommand("SELECT Id, Descripcion FROM siancentral.siancentral.dbo.usocfdi", cn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new eUsoCfdi
                            {
                                Id = reader["Id"].ToString(),
                                Descripcion = reader["Descripcion"].ToString(),
                            });
                        }
                    }
                }
                catch
                {
                    // Manejo de error
                }
            }

            return result;
        }

        private void LlenarComboUsoCfdi()
        {
            var datos = CargarUsoCfdi();

            ddUsoCfdi.DataSource = datos;
            ddUsoCfdi.ValueField = "Id";
            ddUsoCfdi.TextField = "Descripcion";
            ddUsoCfdi.DataBind();
        }

        private object Str(object p)
        {
            switch (p.ToString().Trim().ToUpper())
            {
                case "F": return "Factura";
                case "R": return "Remisión";
                default: return "";
            }
        }

        private object NombreDia(object p)
        {
            switch (p.ToString().Trim().ToLower())
            {
                case "l": return "Lunes";
                case "m": return "Martes";
                case "mi": return "Miercoles";
                case "j": return "Jueves";
                case "v": return "Viernes";
                case "s": return "Sabado";
                default: return "";
            }
        }


        private string Nombre(int p)
        {
            switch (p)
            {
                case 1: return "Ene.";
                case 2: return "Feb.";
                case 3: return "Mar.";
                case 4: return "Abr.";
                case 5: return "May.";
                case 6: return "Jun.";
                case 7: return "Jul.";
                case 8: return "Ago.";
                case 9: return "Sep.";
                case 10: return "Oct.";
                case 11: return "Nov.";
                case 12: return "Dic.";
                default: return "";

            }
        }



        private void CargarAcuerdo()
        {
            try
            {
                int verificador = 0;
                double imp = 0;
                DateTime fechaf = default(DateTime);
                Funciones funcion = new Funciones();
                txtFecha.Value = Request.QueryString["Anio"].ToString().Trim();
                txtSemana.Value = Request.QueryString["Semana"].ToString().Trim();

                if (Request.QueryString["Id"] != "")
                {
                    PedidoVtaInst pedido = new PedidoVtaInst();
                    pedido.Id_Emp = session.Id_Emp;
                    pedido.Id_Cd = session.Id_Cd_Ver;
                    pedido.Id_Acs = Convert.ToInt32(Request.QueryString["Id"]);
                    Clientes cc = new Clientes();
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    sesion = (Sesion)Session["Sesion" + Session.SessionID];

                    CN_CapPedidoVtaInst cn_capPedidoVI = new CN_CapPedidoVtaInst();
                    cn_capPedidoVI.Consultar2(ref pedido, session.Emp_Cnx, ref verificador, ref cc);

                    if (verificador == 1)
                    {

                        txtClienteNom.Text = pedido.Cte_Nom;
                        txtIdCte.Value = pedido.Id_Cte.ToString().Trim();
                        txtRikNom.Value = pedido.Rik_Nombre;
                        txtIdRik.Value = pedido.Id_Rik.ToString().Trim();

                        txtTerritorioNom.Text = pedido.Ter_Nombre;
                        txtIdTer.Value = pedido.Id_Ter.ToString().Trim();

                        txtContactoNom.Enabled = false;
                        txtContactoTel.Enabled = false;
                        txtContactoMail.Enabled = false;
                        txtContactoPuesto.Enabled = false;

                        txtCalle.Text = cc.Cte_Calle;
                        txtNo.Text = cc.Cte_Numero;
                        txtCp.Value = cc.Cte_Cp;
                        txtMunicipio.Text = cc.Cte_Municipio;
                        txtEstado.Text = cc.Cte_Estado;
                        txtColonia.Text = cc.Cte_Colonia;

                        TxtVersion.Value = pedido.Id_AcsVersion.ToString().Trim();
                        txtContactoNom.Text = pedido.Acs_PedidoEncargadoEnviar;
                        txtContactoTel.Text = pedido.Acs_PedidoTelefono;
                        txtContactoMail.Text = pedido.Acs_PedidoEmail;
                        txtContactoPuesto.Text = pedido.Acs_PedidoPuesto;
                        this.ChkOrdCompra.Checked = pedido.Acs_ReqOrden;
                        this.ChckOrdReposicion.Checked = pedido.Acs_ReqDocReposicion;
                        this.ChckFolio.Checked = pedido.Acs_ReqDocFolio;
                        this.LblEOtro.Value = pedido.Acs_ReqDocOtro;

                        if (pedido.Acs_Modalidad == "A")
                        {
                            rdModFrencuenciaEstablecida.Checked = true;
                        }
                        else if (pedido.Acs_Modalidad == "B")
                        {
                            rdModOrdenAbierta.Checked = true;
                        }
                        else if (pedido.Acs_Modalidad == "F2")
                        {
                            rdModConsignacion.Checked = true;
                        }
                        this.txtContactoClientealmacen.Text = pedido.Acs_Contacto3;
                        if (pedido.Acs_Telefono3 == "0")
                        {
                            this.txtContactoClientealmacenTel.Text = "";
                        }
                        else
                        {
                            this.txtContactoClientealmacenTel.Text = pedido.Acs_Telefono3;
                        }
                        this.txtContactoClientealmacenEmail.Text = pedido.Acs_Email3;


                        //CheckBox1.Checked = pedido.Acs_ReqOrden;
                    }
                    else
                    {
                        Alerta("No se encontro");
                    }

                    pedido.Id_Emp = session.Id_Emp;
                    pedido.Id_Cd = session.Id_Cd_Ver;
                    pedido.Id_Acs = Convert.ToInt32(Request.QueryString["Id"]);
                    pedido.Acs_Semana = Convert.ToInt32(txtSemana.Value);
                    pedido.Acs_Anio = Convert.ToInt32(txtFecha.Value);

                    dt = null;

                    GetListDet();
                    DataTable dtTemp = dt;
                    List<PedidoVtaInst> List = new List<PedidoVtaInst>();
                    string idTGStr = Request.QueryString["Id_TG"];
                    int? idTGNullable = 0;
                    int idTG = 0;
                    if (idTGStr != null)
                    {
                        if (int.TryParse(idTGStr, out idTG))
                        {
                            idTGNullable = idTG;
                        }
                    }
                    cn_capPedidoVI.ConsultarDet(pedido, ref List, ref dtTemp, session.Emp_Cnx, idTGNullable);

                    rgAcuerdos.DataSource = List;
                    rgAcuerdos.DataBind();

                    DataTable dtTemp_Resto = this.dt;
                    List<PedidoVtaInst> List2 = new List<PedidoVtaInst>();
                    cn_capPedidoVI.ConsultarDet_Resto(pedido, ref List2, ref dtTemp_Resto, session.Emp_Cnx, Id_TG);



                    dt = dtTemp;


                    CN_CatProducto cn_catproducto = new CN_CatProducto();
                    Producto pr;
                    foreach (DataRow dr in dt.Rows)
                    {
                        pr = new Producto();
                        pr.Id_Emp = session.Id_Emp;
                        pr.Id_Cd = session.Id_Cd_Ver;
                        pr.Id_Prd = Convert.ToInt64(dr["Id_prd"]);

                        cn_catproducto.ConsultarVentas(ref pr, Convert.ToInt32(txtIdCte.Value), session.Emp_Cnx);

                        dr["mes1"] = pr.ventaMes[0].ToString().Trim();
                        dr["mes2"] = pr.ventaMes[1].ToString().Trim();
                        dr["mes3"] = pr.ventaMes[2].ToString().Trim();
                    }


                }
                else
                {
                    GetListDet();
                    fechaf = Convert.ToDateTime(funcion.GetLocalDateTime(session.Minutos).ToShortDateString()) > session.CalendarioFin ? session.CalendarioFin : Convert.ToDateTime(funcion.GetLocalDateTime(session.Minutos).ToShortDateString());
                    fechaf = fechaf.AddDays(1);


                    _nuevoPedidoSinProgramar = true;

                }


                string ConexionCentral = ConfigurationManager.AppSettings["strConnectionCentral"].ToString().Trim();
                ConvenioDet conv;
                ConvenioDet convdet;
                CN_Convenio cn_conv;
                List<string> Productos = new List<string>();

                List<ConvenioDet> lconveniosdet = new List<ConvenioDet>();
                ConvenioDet lconvdet = new ConvenioDet();


                if (dt.Rows.Count > 0)
                {
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {

                        conv = new ConvenioDet();
                        convdet = new ConvenioDet();
                        cn_conv = new CN_Convenio();
                        conv.Id_Emp = session.Id_Emp;
                        conv.Id_Cd = session.Id_Cd_Ver;
                        conv.Id_Cte = Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1");
                        conv.Id_Prd = Convert.ToInt64(dt.Rows[x]["Id_Prd"]);
                        double PrecioIngresado = Convert.ToDouble(dt.Rows[x]["Prd_Precio"]);

                        if (Convert.ToInt64(dt.Rows[x]["Id_Prd"]) <= 999999999999)
                        {
                            cn_conv.Convenio_ConsultaPrecio(conv, ref convdet, ConexionCentral);

                            //if (convdet != null && convdet.PCD_PrecioAAAEsp > 0)
                            //{
                            //    dt.Rows[x]["Prd_PrecioLista"] = convdet.PCD_PrecioAAAEsp;
                            //}
                            //else
                            //{
                            Producto producto = new Producto();
                            //obtener datos de producto
                            CN_CatProducto clsProducto = new CN_CatProducto();
                            producto.Id_Cte = Convert.ToInt32(Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1"));
                            int productoNuevo = 0;
                            clsProducto.ConsultaProductos(ref producto, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, Convert.ToInt64(dt.Rows[x]["Id_Prd"]), ref productoNuevo, 0);

                            if (0 < producto.Prd_PLista)
                            {
                                dt.Rows[x]["Prd_PrecioLista"] = producto.Prd_PLista;
                            }
                            //}
                        }
                    }
                }





                Session["Prod"] = dt;
                rg1.DataSource = dt;
                rg1.DataBind();

                foreach (DataRow i in dt.Rows)
                {
                    imp += Convert.ToDouble(i["Prd_Importe"]);

                }



                CN_CatSemana CnSemana = new CN_CatSemana();
                Semana semana = new Semana();
                semana.Id_Emp = session.Id_Emp;
                semana.Id_Cd = session.Id_Cd_Ver;
                semana.Sem_FechaAct = Convert.ToDateTime(funcion.GetLocalDateTime(session.Minutos).ToShortDateString());
                verificador = -1;
                CnSemana.ConsultaSemana(ref semana, session.Emp_Cnx, ref verificador);

                if (verificador > -1)
                {
                    HF_FechaActual.Value = rdFechaF.Value.ToString().Trim();
                    HF_InicioSemana.Value = semana.Sem_FechaIni.ToString().Trim();
                    HF_FinSemana.Value = semana.Sem_FechaFin.ToString().Trim();
                }

                double iva_cd = 0;
                CN_CatCentroDistribucion cn = new CN_CatCentroDistribucion();
                cn.ConsultarIva(session.Id_Emp, session.Id_Cd_Ver, ref iva_cd, session.Emp_Cnx);


                txtSubtotal.Value = imp.ToString("F2");
                txtIva.Value = (Convert.ToDouble(HD_IVAfacturacion.Value.ToString()) * iva_cd / 100).ToString("F2").Trim();
                txtTotal.Value = (Convert.ToDouble(txtSubtotal.Value) + Convert.ToDouble(txtIva.Value)).ToString("F2");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ConsultarClienteFechaEntrega(string strConexion, int intIdCte, int intIdCd)
        {

            int intDiasEntrega = 0;
            int intEsModificable = 1;
            DateTime fechaEntrega = new DateTime();
            string strFechaEntrega = string.Empty;

            CN_CapPedido cN_CapPedido = new CN_CapPedido();
            cN_CapPedido.ConsultarClienteFechaEntrega(strConexion, intIdCte, intIdCd, ref strFechaEntrega, ref intDiasEntrega, ref fechaEntrega, ref intEsModificable);
            if (!string.IsNullOrEmpty(strFechaEntrega))
            {
                rdFechaE.Value = Convert.ToDateTime(strFechaEntrega);
                lblDiasEntrega.Visible = true;
                lblDiasEntrega.Text = "Este cliente cuenta con un maximo de " + intDiasEntrega.ToString() + " dias para su entrega.";
                HF_FechaEntregaCompromiso.Value = fechaEntrega.ToString("yyyy-MM-dd");
                if (intEsModificable != 1)
                {
                    rdFechaE.Enabled = false;
                }
            }
        }

        private bool _nuevoPedidoSinProgramar
        {
            get
            {
                return (bool)Session["_nuevoPedidoSinProgramar"];
            }
            set
            {
                Session["_nuevoPedidoSinProgramar"] = value;
            }
        }


        private void CargarPedidoInternet(int num_pedido, int tipoPedido)
        {
            try
            {
                //  HF_ID.Value = txtFolio.Value;
                Sesion sesion = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                Pedido_Internet pedido = new Pedido_Internet();
                ClienteDirEntrega dirEntrega = new ClienteDirEntrega();

                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;
                pedido.Num_Pedido = num_pedido;
                pedido.tipoPedido = tipoPedido;

                CN_CapPedido_Internet cn_capPedidoInternet = new CN_CapPedido_Internet();
                cn_capPedidoInternet.ConsultarPedido_Datos(session.Emp_Cnx, ref pedido, ref dirEntrega, num_pedido, tipoPedido);

                txtClienteNom.Text = pedido.Nom_Cliente;
                txtIdCte.Value = pedido.Id_Cte.ToString().Trim();
                TxtPed_ReqAcys.Text = pedido.Num_Pedido.ToString().Trim();

                CargarTerritorios();

                txtClave.Text = pedido.id_Acs.ToString();

                txtContactoNom.Text = pedido.Nombre_Usuario;
                txtContactoTel.Text = pedido.Telefono_Usuario;
                txtContactoMail.Text = pedido.Cuenta_Usuario;
                txtContactoPuesto.Text = "Usuario Internet";

                txtCalle.Text = dirEntrega.Cte_Calle;
                txtNo.Text = dirEntrega.Cte_Numero;

                int res = 0;
                if (Int32.TryParse(dirEntrega.Cte_Cp, out res)) txtCp.Value = dirEntrega.Cte_Cp;

                txtMunicipio.Text = dirEntrega.Cte_Municipio;
                txtEstado.Text = dirEntrega.Cte_Estado;
                txtColonia.Text = dirEntrega.Cte_Colonia;
                txtNotas.Text = pedido.Observaciones;

                txtContactoClientealmacen.Text = pedido.Nombre_Usuario;
                if (Int32.TryParse(pedido.Telefono, out res)) txtContactoClientealmacenTel.Text = pedido.Telefono;
                txtContactoClientealmacenEmail.Text = pedido.Correo;
                txtContactoClientealmacen.Text = pedido.Nombre;

                txtRHoraam1.Value = dirEntrega.Cte_HoraAm1;
                txtRHoraam2.Value = dirEntrega.Cte_HoraAm2;
                txtRHorapm1.Value = dirEntrega.Cte_HoraPm1;
                txtRHorapm2.Value = dirEntrega.Cte_HoraPm2;

                TxtPed_OCAcys.Text = pedido.OrdenCompraSTR;

                ddUsoCfdi.Value = pedido.UsoCFDI?.ToString().Trim();
                rdModInternet.Checked = true;
                ChkOrdCompra.Checked = pedido.OrdenCompra;
                if (pedido.OrdenCompra == true)
                {
                    if (tipoPedido != 0)
                    {
                        //TxtPed_OCAcys.Text = pedido.OrdenCompraSTR;
                        TxtPed_OCAcys.Enabled = false;
                    }
                    else
                    {
                        TxtPed_OCAcys.Enabled = true;
                    }
                }
                else
                {
                    TxtPed_OCAcys.Enabled = false;

                }

                txtSubtotal.Value = pedido.Subtotal.ToString();
                txtIva.Value = pedido.IVA.ToString();
                txtTotal.Value = pedido.Total.ToString();

                //Edsg Desactiva los campos
                TxtPed_ReqAcys.Enabled = false;
                txtNotas.Enabled = false;
                ImgBuscarDireccionEntrega.Enabled = false;

                Session["PedexternoCP"] = pedido.Id_PedExt;


                GetListDet();

                DataTable dtTemp = dt;
                cn_capPedidoInternet.ConsultarPedido_Detalle(session.Emp_Cnx, num_pedido, tipoPedido, ref dtTemp);
                dt = dtTemp;

                CN_CatProducto cn_catproducto = new CN_CatProducto();
                Producto pr;
                foreach (DataRow dr in dt.Rows)
                {
                    pr = new Producto();
                    pr.Id_Emp = session.Id_Emp;
                    pr.Id_Cd = session.Id_Cd_Ver;
                    pr.Id_Prd = Convert.ToInt64(dr["Id_prd"]);

                    cn_catproducto.ConsultarVentas(ref pr, Convert.ToInt32(txtIdCte.Value), session.Emp_Cnx);

                    dr["mes1"] = pr.ventaMes[0].ToString().Trim();
                    dr["mes2"] = pr.ventaMes[1].ToString().Trim();
                    dr["mes3"] = pr.ventaMes[2].ToString().Trim();
                }



                string ConexionCentral = ConfigurationManager.AppSettings["strConnectionCentral"].ToString().Trim();
                ConvenioDet conv;
                ConvenioDet convdet;
                CN_Convenio cn_conv;
                List<string> Productos = new List<string>();

                List<ConvenioDet> lconveniosdet = new List<ConvenioDet>();
                ConvenioDet lconvdet = new ConvenioDet();


                if (dt.Rows.Count > 0)
                {
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {

                        conv = new ConvenioDet();
                        convdet = new ConvenioDet();
                        cn_conv = new CN_Convenio();
                        conv.Id_Emp = session.Id_Emp;
                        conv.Id_Cd = session.Id_Cd_Ver;
                        conv.Id_Cte = Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1");
                        conv.Id_Prd = Convert.ToInt64(dt.Rows[x]["Id_Prd"]);
                        double PrecioIngresado = Convert.ToDouble(dt.Rows[x]["Prd_Precio"]);

                        if (Convert.ToInt64(dt.Rows[x]["Id_Prd"]) <= 999999999999)
                        {
                            if (Convert.ToDouble(dt.Rows[x]["Prd_PrecioLista"]) == 0)
                            {
                                //cn_conv.Convenio_ConsultaPrecio(conv, ref convdet, ConexionCentral);

                                //if (convdet != null && convdet.PCD_PrecioAAAEsp > 0)
                                //{
                                //    dt.Rows[x]["Prd_PrecioLista"] = convdet.PCD_PrecioAAAEsp;
                                //}
                                //else
                                //{
                                Producto producto = new Producto();
                                //obtener datos de producto
                                CN_CatProducto clsProducto = new CN_CatProducto();
                                producto.Id_Cte = Convert.ToInt32(Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1"));
                                int productoNuevo = 0;
                                clsProducto.ConsultaProductos(ref producto, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, Convert.ToInt64(dt.Rows[x]["Id_Prd"]), ref productoNuevo, 0);

                                if (0 < producto.Prd_PLista)
                                {
                                    dt.Rows[x]["Prd_PrecioLista"] = producto.Prd_PLista;
                                }
                                //}
                            }
                        }
                    }
                }

                Session["dtPedidoVI"] = dt;
                rg1.DataSource = dt;
                rg1.DataBind();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarPedido()
        {
            try
            {
                HF_ID.Value = txtFolio.Value.ToString().Trim();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                session = (Sesion)Session["Sesion" + Session.SessionID];
                Pedido pedido = new Pedido();
                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;
                pedido.Id_Ped = Convert.ToInt32(txtFolio.Value);

                CN_CapPedido cn_capPedido = new CN_CapPedido();
                cn_capPedido.ConsultaPedido(ref pedido, session.Emp_Cnx);

                txtClienteNom.Text = pedido.Cte_NomComercial;
                txtIdCte.Value = pedido.Id_Cte.ToString().Trim();

                CargarTerritorios();

                txtRikNom.Value = pedido.Rik_Nombre;
                txtIdRik.Value = pedido.Id_Rik.ToString().Trim();
                txtTerritorioNom.Text = pedido.Ter_Nombre;
                txtIdTer.Value = pedido.Id_Ter.ToString().Trim();

                txtContactoNom.Text = pedido.Ped_Solicito;
                txtContactoTel.Text = pedido.Ped_SolicitoTel;
                txtContactoMail.Text = pedido.Ped_SolicitoEmail;
                txtContactoPuesto.Text = pedido.Ped_SolicitoPuesto;

                txtCalle.Text = pedido.Ped_ConsignadoCalle;
                txtNo.Text = pedido.Ped_ConsignadoNo;
                txtCp.Value = pedido.Ped_ConsignadoCp;
                txtMunicipio.Text = pedido.Ped_ConsignadoMunicipio;
                txtEstado.Text = pedido.Ped_ConsignadoEstado;
                txtColonia.Text = pedido.Ped_ConsignadoColonia;
                txtNotas.Text = pedido.Ped_Comentarios;

                txtFecha.Value = pedido.Ped_AcysAnio.ToString().Trim();
                txtSemana.Value = pedido.Ped_AcysSemana.ToString().Trim();
                txtClave.Value = pedido.Id_Acs;



                txtSubtotal.Value = pedido.Ped_Subtotal.ToString("f2");
                txtIva.Value = pedido.Ped_Iva.ToString("F2");
                txtTotal.Value = pedido.Ped_Total.ToString("F2");

                rdFechaE.Value = pedido.Ped_FechaEntrega;

                if (pedido.FechaFacAcys.Year != 1)
                {
                    rdFechaF.Value = pedido.FechaFacAcys;
                }
                TxtPed_PedAcys.Text = pedido.Pedido_del;
                TxtPed_ReqAcys.Text = pedido.Requisicion;
                TxtPed_OCAcys.Text = pedido.Ped_OrdenCompra;

                rdModInternet.Checked = true;
                ChkOrdCompra.Checked = pedido.Ped_ReqOrden;
                ddUsoCfdi.Value = pedido.UsoCFDI?.ToString().Trim();


                TxtPed_OCAcys.Enabled = false;

                pedido.Ped_Tipo = 3;
                pedido.Ped_Captacion = true;

                Session["PedexternoCP"] = pedido.PedidoExterno;


                GetListDet();
                DataTable dtTemp = dt;
                cn_capPedido.ConsultaPedidoDet(pedido, ref dtTemp, session.Emp_Cnx);
                dt = dtTemp;

                CN_CatProducto cn_catproducto = new CN_CatProducto();
                Producto pr;
                foreach (DataRow dr in dt.Rows)
                {
                    pr = new Producto();
                    pr.Id_Emp = session.Id_Emp;
                    pr.Id_Cd = session.Id_Cd_Ver;
                    pr.Id_Prd = Convert.ToInt64(dr["Id_prd"]);

                    cn_catproducto.ConsultarVentas(ref pr, Convert.ToInt32(txtIdCte.Value), session.Emp_Cnx);

                    dr["mes1"] = pr.ventaMes[0].ToString().Trim();
                    dr["mes2"] = pr.ventaMes[1].ToString().Trim();
                    dr["mes3"] = pr.ventaMes[2].ToString().Trim();
                }



                string ConexionCentral = ConfigurationManager.AppSettings["strConnectionCentral"].ToString().Trim();
                ConvenioDet conv;
                ConvenioDet convdet;
                CN_Convenio cn_conv;
                List<string> Productos = new List<string>();

                List<ConvenioDet> lconveniosdet = new List<ConvenioDet>();
                ConvenioDet lconvdet = new ConvenioDet();


                if (dt.Rows.Count > 0)
                {
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {

                        conv = new ConvenioDet();
                        convdet = new ConvenioDet();
                        cn_conv = new CN_Convenio();
                        conv.Id_Emp = session.Id_Emp;
                        conv.Id_Cd = session.Id_Cd_Ver;
                        conv.Id_Cte = Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1");
                        conv.Id_Prd = Convert.ToInt64(dt.Rows[x]["Id_Prd"]);
                        double PrecioIngresado = Convert.ToDouble(dt.Rows[x]["Prd_Precio"]);

                        if (Convert.ToInt64(dt.Rows[x]["Id_Prd"]) <= 999999999999)
                        {
                            if (Convert.ToDouble(dt.Rows[x]["Prd_PrecioLista"]) == 0)
                            {
                                //cn_conv.Convenio_ConsultaPrecio(conv, ref convdet, ConexionCentral);

                                //if (convdet != null && convdet.PCD_PrecioAAAEsp > 0)
                                //{
                                //    dt.Rows[x]["Prd_PrecioLista"] = convdet.PCD_PrecioAAAEsp;
                                //}
                                //else
                                //{
                                Producto producto = new Producto();
                                //obtener datos de producto
                                CN_CatProducto clsProducto = new CN_CatProducto();
                                producto.Id_Cte = Convert.ToInt32(Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1"));
                                int productoNuevo = 0;
                                clsProducto.ConsultaProductos(ref producto, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, Convert.ToInt64(dt.Rows[x]["Id_Prd"]), ref productoNuevo, 0);

                                if (0 < producto.Prd_PLista)
                                {
                                    dt.Rows[x]["Prd_PrecioLista"] = producto.Prd_PLista;
                                }
                                //}
                            }
                        }
                    }
                }

                Session["dtPedidoVI"] = dt;
                rg1.DataSource = dt;
                rg1.DataBind();



                Funciones funcion = new Funciones();
                CN_CatSemana CnSemana = new CN_CatSemana();
                Semana semana = new Semana();
                semana.Id_Emp = session.Id_Emp;
                semana.Id_Cd = session.Id_Cd_Ver;
                semana.Sem_FechaAct = funcion.GetLocalDateTime(session.Minutos);
                int verificador = -1;
                CnSemana.ConsultaSemana(ref semana, session.Emp_Cnx, ref verificador);

                //if (verificador > -1)
                //{
                //    HF_FechaActual.Value = rdFechaF.Value.ToString().Trim();
                //    HF_InicioSemana.Value = semana.Sem_FechaIni.ToString().Trim();
                //    HF_FinSemana.Value = semana.Sem_FechaFin.ToString().Trim();
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarInfoAcys(int Id_Acys, int AcysMes, int AcysAnio)
        {
            try
            {
                int verificador = 0;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                PedidoVtaInst pedido = new PedidoVtaInst();
                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;
                pedido.Id_Acs = Id_Acys;
                Clientes cc = new Clientes();
                CN_CapPedidoVtaInst cn_capPedidoVI = new CN_CapPedidoVtaInst();
                cn_capPedidoVI.Consultar2(ref pedido, session.Emp_Cnx, ref verificador, ref cc);

                txtCalle.Text = cc.Cte_Calle;
                txtNo.Text = cc.Cte_Numero;
                txtCp.Value = cc.Cte_Cp;
                txtMunicipio.Text = cc.Cte_Municipio;
                txtEstado.Text = cc.Cte_Estado;
                txtColonia.Text = cc.Cte_Colonia;

                TxtVersion.Value = pedido.Id_AcsVersion.ToString().Trim();
                txtContactoNom.Text = pedido.Acs_PedidoEncargadoEnviar;
                txtContactoTel.Text = pedido.Acs_PedidoTelefono;
                txtContactoMail.Text = pedido.Acs_PedidoEmail;
                txtContactoPuesto.Text = pedido.Acs_PedidoPuesto;
                this.ChkOrdCompra.Checked = pedido.Acs_ReqOrden;
                this.ChckOrdReposicion.Checked = pedido.Acs_ReqDocReposicion;
                this.ChckFolio.Checked = pedido.Acs_ReqDocFolio;
                this.LblEOtro.Value = pedido.Acs_ReqDocOtro;

                if (pedido.Acs_Modalidad == "A")
                {
                    rdModFrencuenciaEstablecida.Checked = true;
                }
                else if (pedido.Acs_Modalidad == "B")
                {
                    rdModOrdenAbierta.Checked = true;
                }
                else if (pedido.Acs_Modalidad == "F2")
                {
                    rdModConsignacion.Checked = true;
                }
                this.txtContactoClientealmacen.Text = pedido.Acs_Contacto3;
                if (pedido.Acs_Telefono3 == "0")
                {
                    this.txtContactoClientealmacenTel.Text = "";
                }
                else
                {
                    this.txtContactoClientealmacenTel.Text = pedido.Acs_Telefono3;
                }
                this.txtContactoClientealmacenEmail.Text = pedido.Acs_Email3;



                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;
                pedido.Id_Acs = Id_Acys;
                pedido.Acs_Semana = AcysMes;
                pedido.Acs_Anio = AcysAnio;
                List<PedidoVtaInst> List = new List<PedidoVtaInst>();
                cn_capPedidoVI.ConsultarDetAcys(pedido, ref List, session.Emp_Cnx);

                rgAcuerdos.DataSource = List;
                rgAcuerdos.DataBind();



            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void CargarTerritorios()
        {
            try
            {
                List<Territorios> Territorio = new List<Territorios>();
                CD_CatTerritorios CDterritorios = new CD_CatTerritorios();
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();

                CDterritorios.TerritorioCliente_Combo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, Convert.ToInt32(txtIdCte.Value.Length > 0 ? Int32.Parse(txtIdCte.Value) : -1), Sesion.Id_Rik < 0 ? (int?)null : Sesion.Id_Rik, null, Sesion.Emp_Cnx, ref Territorio);


                txtTerritorioNom.DataSource = Territorio;
                txtTerritorioNom.ValueField = "Id_Ter";
                txtTerritorioNom.TextField = "Descripcion";
                txtTerritorioNom.DataBind();



                if (txtTerritorioNom.Items.Count > 1)
                {
                    txtTerritorioNom.SelectedIndex = 1;
                    txtTerritorioNom.SelectedItem.Text = txtTerritorioNom.Items[1].Text;
                    txtIdTer.Value = txtTerritorioNom.Items[1].Value.ToString().Trim();
                }
                else
                {
                    if (Request.QueryString["id"] != "-1")
                    {
                        CDterritorios.TerritorioCliente_Combo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, Convert.ToInt32(txtIdCte.Value.Length > 0 ? Int32.Parse(txtIdCte.Value) : -1), Sesion.Id_Rik < 0 ? (int?)null : Sesion.Id_Rik, 0, Sesion.Emp_Cnx, ref Territorio);


                        txtTerritorioNom.DataSource = Territorio;
                        txtTerritorioNom.ValueField = "Id_Ter";
                        txtTerritorioNom.TextField = "Descripcion";
                        txtTerritorioNom.DataBind();


                        if (txtTerritorioNom.Items.Count > 1)
                        {
                            txtTerritorioNom.SelectedIndex = 1;
                            txtTerritorioNom.SelectedItem.Text = txtTerritorioNom.Items[1].Text;
                            txtIdTer.Value = txtTerritorioNom.Items[1].Value.ToString().Trim();
                        }
                    }
                    else
                    {
                        txtIdTer.Value = "";
                        txtTerritorioNom.Items.Clear();
                        txtTerritorioNom.Text = "";
                    }
                }
                if (txtTerritorioNom.SelectedItem.Value.ToString() != "")
                {
                    CN_CatTerritorios cn_terr = new CN_CatTerritorios();
                    Territorios terr = new Territorios();
                    terr.Id_Emp = session.Id_Emp;
                    terr.Id_Cd = session.Id_Cd_Ver;
                    terr.Id_Ter = Convert.ToInt32(txtTerritorioNom.SelectedItem.Value);
                    cn_terr.ConsultaTerritorios(ref terr, session.Emp_Cnx);

                    txtRikNom.Value = terr.Rik_Nombre;
                    txtIdRik.Value = terr.Id_Rik.ToString().Trim();
                }
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
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, "CapPedido", "Id_Ped", Sesion.Emp_Cnx, "spCatLocal_Maximo");
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
                //Sesion Sesion =  (Sesion)Session["Sesion" + Session.SessionID];  
                //Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                //Funciones funciones = new Funciones();
                //rdFecha.SelectedDate = funciones.GetLocalDateTime(Sesion.Minutos);
                //dpFecha2.SelectedDate = funciones.GetLocalDateTime(Sesion.Minutos).AddDays(1);
                //rdFecha.Focus();
                //txtClave.Value = MaximoId();
                //txtComentarios.Text = string.Empty;
                //txtConcepto.Text = string.Empty;
                //txtConcepto2.Text = string.Empty;
                //txtCondiciones.Text = string.Empty;
                //txtDescuento.Text = string.Empty;
                //txtDescuento2.Text = string.Empty;
                //txtFlete.Text = string.Empty;
                //txtImporte.Text = string.Empty;
                //txtIVA.Text = string.Empty;
                //txtNumCliente.Text = string.Empty;
                //txtObservaciones.Text = string.Empty;
                //txtOrden.Text = string.Empty;
                //txtPedidodel.Text = string.Empty;
                //txtRepresentanteID.Text = string.Empty;
                //txtRequisicion.Text = string.Empty;
                //txtSolicito.Text = string.Empty;
                //txtSub.Text = string.Empty;
                //txtTerritorio.Text = string.Empty;
                //txtTotal.Text = string.Empty;

                //cmbCliente.SelectedIndex = 0;
                //cmbCliente.Text = cmbCliente.Items[0].Text;

                //cmbRik.SelectedIndex = 0;
                //cmbRik.Text = cmbCliente.Items[0].Text;

                //cmbTerritorio.SelectedIndex = 0;
                //cmbTerritorio.Text = cmbCliente.Items[0].Text;

                //HF_ID.Value = "";

                //dt.Rows.Clear();
                //rgDetalles.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarProductos(RadComboBox sender)
        {
            try
            {
                RadComboBox productoDet = (sender.Parent.Parent.FindControl("cmbDescr") as RadComboBox);
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(session.Id_Emp, session.Id_Cd_Ver, Convert.ToInt32(sender.SelectedValue), session.Emp_Cnx, "spCatProdSeg_Combo", ref productoDet);
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
                //if (this.HiddenRebind.Value == "0")
                //{
                //    funcion = "CloseWindow()";
                //}
                //else
                //{
                funcion = "CloseAndRebind()";
                //}
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void Imprimir(int Id_Ped)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ArrayList ALValorParametrosInternos = new ArrayList();

                //Consulta centro de distribución
                CentroDistribucion Cd = new CentroDistribucion();
                new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref Cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);

                ALValorParametrosInternos.Add(sesion.Emp_Cnx);
                ALValorParametrosInternos.Add(sesion.Id_Emp);
                ALValorParametrosInternos.Add(sesion.Id_Cd_Ver);
                ALValorParametrosInternos.Add(Id_Ped);

                Type instance = null;
                instance = typeof(LibreriaReportes.PedidoImpresion);
                Session["InternParameter_Values" + Session.SessionID] = ALValorParametrosInternos;
                Session["assembly" + Session.SessionID] = instance.AssemblyQualifiedName;

                //NOTA: El estatus de impresión, lo pone cuando el reporte se carga

                ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirReportePadre", "AbrirReportePadre()", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void imprimir()
        {

            try
            {
                CapaDatos.Funciones funciones = new CapaDatos.Funciones();

                CapaDatos.Funciones fecha = new CapaDatos.Funciones();
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ArrayList ALValorParametrosInternos = new ArrayList();
                CapaDatos.Funciones CD = new CapaDatos.Funciones();

                ALValorParametrosInternos.Add(Sesion.Id_Emp);

                ALValorParametrosInternos.Add(Sesion.Emp_Cnx);

                Type instance = null;
                instance = typeof(LibreriaReportes.ReportePrueba);
                Session["InternParameter_Values" + Session.SessionID] = ALValorParametrosInternos;
                Session["assembly" + Session.SessionID] = instance.AssemblyQualifiedName;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirReportePadre", "AbrirReportePadre()", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ValidarPermisos()
        {
            try
            {
                if (_PermisoGuardar == false & _PermisoModificar == false)
                {
                    btnGuardar.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarCliente()
        {

            Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            bool cancelar = false;
            Clientes cte = new Clientes();
            cte.Id_Emp = Sesion.Id_Emp;
            cte.Id_Cd = Sesion.Id_Cd_Ver;
            cte.Id_Cte = txtIdCte.Value != "" ? Convert.ToInt32(txtIdCte.Value.ToString().Trim()) : -1;
            cte.Id_Rik = Sesion.Id_Rik;
            CN_CatCliente catcliente = new CN_CatCliente();
            try
            {
                catcliente.ConsultaClientes(ref cte, Sesion.Emp_Cnx);
                if (!cte.Cte_Facturacion)
                {
                    Alerta("CUIDADO: Este cliente se encuentra bloqueado por parte de cobranza; favor de aclarar su situación de créditos");
                    cancelar = true;
                }


                if (cte.Cte_CreditoSuspendido)
                {

                    Alerta("Este cliente tiene el crédito suspendido");
                    cancelar = true;
                }

            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
                cancelar = true;
            }


            CargarTerritorios();
            txtClienteNom.Text = cte.Cte_NomComercial;
            txtContactoNom.Text = cte.Cte_Contacto;
            txtContactoMail.Text = cte.Cte_Email;
            txtContactoTel.Text = cte.Cte_Telefono;
            txtCalle.Text = cte.Cte_Calle;
            txtColonia.Text = cte.Cte_Colonia;
            txtEstado.Text = cte.Cte_Estado;
            txtNo.Text = cte.Cte_Numero;
            ddUsoCfdi.Value = cte.Cte_UsoCFDI?.ToString().Trim();

            if (cte.Cte_Cp != null)
            {
                if (cte.Cte_Cp.Trim() != "")
                {
                    txtCp.Text = cte.Cte_Cp;
                }
            }
            txtMunicipio.Text = cte.Cte_Municipio;




            if (cancelar)
            {

                txtClienteNom.Text = "";
                txtIdCte.Value = "";
                txtIdTer.Value = "";
                txtIdRik.Value = "";
                txtTerritorioNom.Items.Clear();
                txtTerritorioNom.Text = "";
                txtIdRik.Value = "";
                txtRikNom.Value = "";

            }

        }
        //Grid
        private void GetListDet()
        {
            try
            {
                dt = new DataTable();
                DataColumn dc = new DataColumn();
                dt.Columns.Add("Id_PrdOld", System.Type.GetType("System.Int64"));
                dt.Columns.Add("Id_Prd", System.Type.GetType("System.Int64"));
                dt.Columns.Add("Prd_Descripcion", System.Type.GetType("System.String"));
                dt.Columns.Add("Prd_Presentacion", System.Type.GetType("System.String"));
                dt.Columns.Add("Prd_Unidad", System.Type.GetType("System.String"));

                dt.Columns.Add("Mes1", System.Type.GetType("System.Double"));
                dt.Columns.Add("Mes2", System.Type.GetType("System.Double"));
                dt.Columns.Add("Mes3", System.Type.GetType("System.Double"));

                dt.Columns.Add("Prd_Cantidad", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Prd_Precio", System.Type.GetType("System.Double"));
                dt.Columns.Add("Prd_PrecioAcys", System.Type.GetType("System.Double"));
                dt.Columns.Add("Prd_Importe", System.Type.GetType("System.Double"));
                dt.Columns.Add("Acs_Doc", System.Type.GetType("System.String"));
                dt.Columns.Add("Acs_FechaF", System.Type.GetType("System.DateTime"));
                dt.Columns.Add("Mod", System.Type.GetType("System.Boolean"));
                dt.Columns.Add("Acs_Dia", System.Type.GetType("System.String"));
                dt.Columns.Add("Acs_DiaStr", System.Type.GetType("System.String"));

                dt.Columns.Add("Acs_Frecuencia", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Prd_RemFact", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Ped_Asignar", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Id_TG", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Id_Acs", System.Type.GetType("System.Int32"));
                dt.Columns.Add("ACS_ReqOC", System.Type.GetType("System.String"));
                dt.Columns.Add("Prd_PrecioLista", System.Type.GetType("System.Double"));
                dt.Columns.Add("Prd_Activo", System.Type.GetType("System.Int32"));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Delete(GridCommandEventArgs e)
        {
            DataRow[] Ar_dr;
            GridItem gi = e.Item;

            long Id_Prd = Convert.ToInt64(((Label)gi.FindControl("lblProd")).Text);
            int Cantasignado = gi.OwnerTableView.DataKeyValues[gi.ItemIndex]["Ped_Asignar"] == DBNull.Value ? 0 : Convert.ToInt32(gi.OwnerTableView.DataKeyValues[gi.ItemIndex]["Ped_Asignar"]);
            if (Request.QueryString["IdVI"] != null && Cantasignado > 0)
            {
                Alerta("No es posible eliminar el producto captado");
                e.Canceled = true;
                return;
            }
            else
            {

                al.Add(Id_Prd);
                Ar_dr = dt.Select("Id_Prd='" + Id_Prd + "'");
                if (Ar_dr.Length > 0)
                {
                    var ProductosNoAcys = (List<Int64>)Session["ProductosNoAcys"];

                    if (!ProductosNoAcys.Contains(Id_Prd))
                        dt.ImportRow(Ar_dr[0]);



                    Ar_dr[0].Delete();
                    dt.AcceptChanges();
                }

                CalcularTotales();
            }
        }

        private void Delete(GridItem item, GridCommandEventArgs e)
        {
            DataRow[] Ar_dr;
            GridItem gi = item;

            long Id_Prd = Convert.ToInt64(((Label)gi.FindControl("lblProd")).Text);
            int Cantasignado = gi.OwnerTableView.DataKeyValues[gi.ItemIndex]["Ped_Asignar"] == DBNull.Value ? 0 : Convert.ToInt32(gi.OwnerTableView.DataKeyValues[gi.ItemIndex]["Ped_Asignar"]);
            if (Request.QueryString["IdVI"] != null && Cantasignado > 0)
            {
                Alerta("No es posible eliminar el producto captado");
                e.Canceled = true;
                return;
            }
            else
            {

                al.Add(Id_Prd);
                Ar_dr = dt.Select("Id_Prd='" + Id_Prd + "'");
                if (Ar_dr.Length > 0)
                {
                    var ProductosNoAcys = (List<Int64>)Session["ProductosNoAcys"];
                    if (!ProductosNoAcys.Contains(Id_Prd))
                        dt.ImportRow(Ar_dr[0]);



                    Ar_dr[0].Delete();
                    dt.AcceptChanges();
                }

                CalcularTotales();
            }
        }

        private void PerformInsert(GridCommandEventArgs e)
        {
            try
            {
                DataRow[] Ar_dr;
                GridItem gi = e.Item;

                if (((RadNumericTextBox)gi.FindControl("txtProd")).Value == 0 ||
                    ((RadNumericTextBox)gi.FindControl("txtPrecio")).Text == "" ||
                    ((RadNumericTextBox)gi.FindControl("txtCantidad")).Text == "")
                {
                    Alerta("Todos los campos son requeridos");
                    e.Canceled = true;
                    return;
                }

                if (int.Parse(((RadNumericTextBox)gi.FindControl("txtCantidad")).Text) == 0)
                {
                    Alerta("La cantidad debe ser mayor a 0");
                    e.Canceled = true;
                    return;

                }

                bool modificar = ((CheckBox)gi.FindControl("chkModTemp")).Checked;
                if (modificar)
                {
                    if (((RadNumericTextBox)gi.FindControl("txtFrecuencia")).Text == "")
                    {
                        Alerta("Todos los campos son requeridos");
                        e.Canceled = true;
                        return;
                    }
                }

                long Id_Prd = Convert.ToInt64(((RadNumericTextBox)gi.FindControl("txtProd")).Value);
                string Prd_Descripcion = ((RadTextBox)gi.FindControl("txtProductoNombre")).Text;
                double? Prd_Precio = ((RadNumericTextBox)gi.FindControl("txtPrecio")).Value;
                double? Prd_PrecioAcys = ((RadNumericTextBox)gi.FindControl("txtPrecioAcys")).Value;
                int Prd_Cantidad = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtCantidad")).Text);
                double Mes1 = Convert.ToDouble(((Label)gi.FindControl("Labelmes12")).Text.Replace("&nbsp;", "0"));
                double Mes2 = Convert.ToDouble(((Label)gi.FindControl("Labelmes22")).Text.Replace("&nbsp;", "0"));
                double Mes3 = Convert.ToDouble(((Label)gi.FindControl("Labelmes32")).Text.Replace("&nbsp;", "0"));
                string Prd_Presentacion = ((Label)gi.FindControl("LabelPresent2")).Text;
                string Prd_Unidad = ((Label)gi.FindControl("LabelUnidad2")).Text;
                string Acs_Doc = ((RadComboBox)gi.FindControl("cmbDocumento")).Text;
                int Ped_Asignar = 0;// Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtCantidad2")).Value.Value);

                double? Prd_Importe = Prd_Precio * Prd_Cantidad;

                string dia = ((RadComboBox)gi.FindControl("cmbDia")).SelectedValue;
                string diaStr = ((RadComboBox)gi.FindControl("cmbDia")).SelectedItem.Text;
                int frecuencia = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtFrecuencia")).Value);

                Ar_dr = dt.Select("Id_prd='" + Id_Prd + "'");
                if (Ar_dr.Length > 0)
                {
                    Alerta("Producto ya capturado");
                    e.Canceled = true;
                }
                else
                {
                    int verificador = -1;
                    CN_CapPedidoVtaInst cn_vi = new CN_CapPedidoVtaInst();
                    PedidoVtaInst pvi = new PedidoVtaInst();
                    pvi.Id_Emp = session.Id_Emp;
                    pvi.Id_Cd = session.Id_Cd_Ver;
                    pvi.Id_Acs = Convert.ToInt32(txtClave.Value);
                    pvi.Acs_Semana = Convert.ToInt32(txtSemana.Value);
                    cn_vi.ConsultarPedidoExistente(pvi, Id_Prd, session.Emp_Cnx, ref verificador);

                    if (verificador == 1)
                    {
                        Alerta("El producto " + Id_Prd + " ya ha sido captado por otro usuario ");
                        e.Canceled = true;
                        return;
                    }

                    dt.Rows.Add(new object[] {
                                -1,
                                Id_Prd,
                                Prd_Descripcion,
                                Prd_Presentacion,
                                Prd_Unidad,
                                Mes1,
                                Mes2,
                                Mes3,
                                Prd_Cantidad,
                                Prd_Precio,
                                Prd_PrecioAcys,
                                Prd_Importe,
                                Acs_Doc,
                                rdFechaF.Value,
                                modificar,
                                dia,
                                diaStr,
                                frecuencia,
                                0,
                                Ped_Asignar
                                 });

                    CalcularTotales();

                    al.Remove(Id_Prd);

                    //Edsg 
                    var ProductosNoAcys = (List<Int64>)Session["ProductosNoAcys"];
                    ProductosNoAcys.Add(Id_Prd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void Update(GridCommandEventArgs e)
        {
            try
            {
                DataRow[] Ar_dr;
                GridItem gi = e.Item;
                int? idTG = null;
                int idTGi = 0;
                idTG = (int?)(e.Item as GridDataItem).GetDataKeyValue("Id_TG");

                if (idTG != null)
                {
                    idTGi = idTG.Value;
                }

                if (int.Parse(((RadNumericTextBox)gi.FindControl("txtCantidad")).Text) == 0 && idTGi == 0)
                {
                    Alerta("La cantidad debe ser mayor a 0");
                    e.Canceled = true;
                    return;

                }

                if ((((RadNumericTextBox)gi.FindControl("txtProd")).Value == 0 ||
                    ((RadNumericTextBox)gi.FindControl("txtPrecio")).Text == "" ||
                    ((RadNumericTextBox)gi.FindControl("txtCantidad")).Text == "") && idTGi == 0)
                {
                    Alerta("Todos los campos son requeridos");
                    e.Canceled = true;
                    return;
                }

                bool modificar = ((CheckBox)gi.FindControl("chkModTemp")).Checked;
                if (modificar)
                {
                    if (((RadNumericTextBox)gi.FindControl("txtFrecuencia")).Text == "")
                    {
                        Alerta("Todos los campos son requeridos");
                        e.Canceled = true;
                        return;
                    }
                }

                long Id_Prd = Convert.ToInt64(((RadNumericTextBox)gi.FindControl("txtProd")).Value);
                string Prd_Descripcion = ((RadTextBox)gi.FindControl("txtProductoNombre")).Text;
                double Prd_Precio = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("txtPrecio")).Text);
                int Prd_Cantidad = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtCantidad")).Text);
                double Mes1 = Convert.ToDouble(((Label)gi.FindControl("Labelmes12")).Text.Replace("&nbsp;", "0"));
                double Mes2 = Convert.ToDouble(((Label)gi.FindControl("Labelmes22")).Text.Replace("&nbsp;", "0"));
                double Mes3 = Convert.ToDouble(((Label)gi.FindControl("Labelmes32")).Text.Replace("&nbsp;", "0"));
                string Prd_Presentacion = ((Label)gi.FindControl("LabelPresent2")).Text;
                string Prd_Unidad = ((Label)gi.FindControl("LabelUnidad2")).Text;
                string Acs_Doc = ((RadComboBox)gi.FindControl("cmbDocumento")).Text;

                double Prd_Importe = Prd_Precio * Prd_Cantidad;

                string dia = ((RadComboBox)gi.FindControl("cmbDia")).SelectedValue;
                string diaStr = ((RadComboBox)gi.FindControl("cmbDia")).SelectedItem.Text;
                int frecuencia = Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtFrecuencia")).Value);
                int Ped_Asignar = ((RadNumericTextBox)gi.FindControl("txtCantidad2")).Value.HasValue ? Convert.ToInt32(((RadNumericTextBox)gi.FindControl("txtCantidad2")).Value.Value) : 0;

                int verificador = -1;
                CN_CapPedidoVtaInst cn_vi = new CN_CapPedidoVtaInst();
                PedidoVtaInst pvi = new PedidoVtaInst();
                pvi.Id_Emp = session.Id_Emp;
                pvi.Id_Cd = session.Id_Cd_Ver;
                pvi.Id_Acs = Convert.ToInt32(txtClave.Value);
                pvi.Acs_Semana = Convert.ToInt32(txtSemana.Value);
                cn_vi.ConsultarPedidoExistente(pvi, Id_Prd, session.Emp_Cnx, ref verificador);

                if (verificador == 1 && HF_ID.Value == "")
                {
                    Alerta("El producto " + Id_Prd + " ya ha sido captado por otro usuario ");
                    e.Canceled = true;
                    return;
                }

                Ar_dr = dt.Select("Id_prd='" + Id_Prd + "'");

                if (Ar_dr.Length > 0)
                {
                    if (Request.QueryString["IdVI"] != null)
                    {
                        int asignado = Convert.ToInt32(gi.OwnerTableView.DataKeyValues[gi.ItemIndex]["Ped_Asignar"] == DBNull.Value ? 0 : gi.OwnerTableView.DataKeyValues[gi.ItemIndex]["Ped_Asignar"]);
                        if (asignado > Prd_Cantidad)
                        {
                            Alerta("La cantidad del producto no puede ser menor a la cantidad asignada.</br>Cantidad asignada: <b>" + asignado + "</b>");
                            e.Canceled = true;
                            return;
                        }
                    }

                    if (Convert.ToInt32(Ar_dr[0]["Prd_RemFact"]) > Prd_Cantidad)
                    {
                        Alerta("La cantidad del producto no puede ser menor a la cantidad remisionada y/o facturada.</br>Cantidad remisionada y/o facturada: <b>" + Ar_dr[0]["Prd_RemFact"] + "</b>");
                        e.Canceled = true;
                        return;
                    }

                    Ar_dr[0].BeginEdit();
                    Ar_dr[0]["Id_Prd"] = Id_Prd;
                    Ar_dr[0]["Prd_Descripcion"] = Prd_Descripcion;
                    Ar_dr[0]["Prd_Presentacion"] = Prd_Presentacion;
                    Ar_dr[0]["Prd_Unidad"] = Prd_Unidad;
                    Ar_dr[0]["Mes1"] = Mes1;
                    Ar_dr[0]["Mes2"] = Mes2;
                    Ar_dr[0]["Mes3"] = Mes3;
                    Ar_dr[0]["Prd_Cantidad"] = Prd_Cantidad;
                    Ar_dr[0]["Prd_Precio"] = Prd_Precio;
                    Ar_dr[0]["Prd_Importe"] = Prd_Importe;
                    Ar_dr[0]["Acs_Doc"] = Acs_Doc;
                    //Ar_dr[0]["Acs_FechaF"] = rdFechaF.SelectedDate;
                    Ar_dr[0]["Mod"] = modificar;
                    Ar_dr[0]["Acs_Dia"] = dia;
                    Ar_dr[0]["Acs_DiaStr"] = diaStr;
                    Ar_dr[0]["Acs_Frecuencia"] = frecuencia;
                    Ar_dr[0]["Ped_Asignar"] = Ped_Asignar;

                    Ar_dr[0].AcceptChanges();

                    CalcularTotales();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CalcularTotales()
        {
            try
            {
                double subtotal = 0;

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    subtotal += Convert.ToDouble(dt.Rows[x]["Prd_Importe"]);
                }

                double iva = subtotal * Convert.ToDouble(HD_IVAfacturacion.Value.ToString()) / 100;
                double total = subtotal + iva;

                txtSubtotal.Value = subtotal.ToString("F2"); ;
                txtIva.Value = iva.ToString("F2"); ;
                txtTotal.Value = total.ToString("F2"); ;
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
                    case "RadDateTimePicker":
                        (controles_contenidos[x] as RadDateTimePicker).Enabled = false;
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





        protected void btnActualizarDireccion_Click(object sender, EventArgs e)
        {
            cargardireccion();
        }

        public void cargardireccion()
        {
            if (Session["Id_Buscar" + Session.SessionID] != null)
            {

                if (HF_Param.Value == "precio")
                {
                    if (Session["Id_Buscar" + Session.SessionID] != null)
                    {
                        int idProducto = Convert.ToInt32(Session["Id_Buscar" + Session.SessionID].ToString().Trim());
                        cmbProductoDet_TextChanged(idProducto);
                    }
                }
                if (HF_Param.Value == "direccion")
                {
                    CN_CatCliente clsCliente = new CN_CatCliente();
                    ClienteDirEntrega cliente = new ClienteDirEntrega();
                    Sesion session2 = (Sesion)Session["Sesion" + Session.SessionID];
                    session2 = (Sesion)Session["Sesion" + Session.SessionID];
                    cliente.Id_Emp = session2.Id_Emp;
                    cliente.Id_Cd = session2.Id_Cd_Ver;

                    cliente.Id_CteDirEntrega = Int32.Parse(Session["Id_Buscar" + Session.SessionID].ToString().Trim()) - 1;
                    cliente.Id_Cte = Int32.Parse(Session["Descripcion_Buscar" + Session.SessionID].ToString().Trim());
                    clsCliente.ConsultaClienteDirEntrega(cliente, session2.Emp_Cnx);
                    txtCalle.Text = cliente.Cte_Calle;
                    txtNo.Text = cliente.Cte_Numero;
                    txtCp.Text = cliente.Cte_Cp.Trim();
                    txtColonia.Text = cliente.Cte_Colonia;
                    txtMunicipio.Text = cliente.Cte_Municipio;
                    txtEstado.Text = cliente.Cte_Estado;
                }
            }
        }


        protected void cmbProductoDet_TextChanged(int idProducto)
        {

            CN_CatProducto cn_catproducto = new CN_CatProducto();
            Producto pr = new Producto();
            try
            {
                DataRow[] Ar_dr;
                Ar_dr = dt.Select("Id_prd='" + idProducto + "'");



                CN_CapAcys cnCa = new CN_CapAcys();
                if (txtIdTer.Value == "")
                {
                    Alerta("Por favor, capture un territorio en la vista \"Datos Generales\"");
                    return;
                }
                if (txtIdCte.Value == "")
                {
                    Alerta("Por favor, capture un territorio en la vista \"Datos Generales\"");
                    return;
                }
                if (txtIdRik.Value == "")
                {
                    Alerta("Por favor, capture un representante de ventas en la vista \"Datos Generales\"");
                    return;
                }
                if (_nuevoPedidoSinProgramar && cnCa.ExisteProductoEnGarantia(session.Id_Emp, session.Id_Cd, idProducto, Convert.ToInt32(txtIdTer.Value), Convert.ToInt32(txtIdCte.Value), Convert.ToInt32(txtIdRik.Value), session))
                {
                    Alerta("Solo se aceptan productos con modalidad de venta convencional que no sean parte del ACYS. Por favor, ingrese otro código de producto.");
                    return;
                }
                if (Ar_dr.Length > 0)
                {

                    Alerta("Producto ya capturado");

                    return;
                }


                if (string.IsNullOrEmpty(txtClave.Text))
                    productoNuevo = 1;
                pr.Id_Cte = !string.IsNullOrEmpty(txtIdCte.Value) ? Convert.ToInt32(txtIdCte.Value) : 0;


                cn_catproducto.ConsultaProductos(ref pr, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, idProducto, ref productoNuevo, 2);

                DataTable dtTemp_Resto;
                if (Session["dtPedidoVI"] != null)
                {
                    dtTemp_Resto = (DataTable)Session["dtPedidoVI"];
                }
                else
                {
                    dtTemp_Resto = new DataTable();
                }


                dtTemp_Resto.Rows.Add(new object[] {
                    idProducto,
                    idProducto,
                    pr.Prd_Descripcion,
                    pr.Prd_Presentacion,
                    pr.Prd_UniNs,
                    -100,
                    -100,
                    -100,
                    0,
                    pr.Prd_Precio,
                    pr.Prd_Precio,
                    pr.Prd_Precio,
                    DBNull.Value,
                    DBNull.Value,
                    DBNull.Value,
                    DBNull.Value,
                    DBNull.Value,
                    DBNull.Value,
                    0,
                    DBNull.Value,
                    DBNull.Value,
                    DBNull.Value,
                    pr.Prd_Activo,
            });
                Session["dtPedidoVI"] = dt;
                rg1.DataSource = dt;
                rg1.DataBind();
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
            }
        }



        #endregion
        #region mensaje

        private void Alerta(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "modalMensaje('" + mensaje + "')", true);
        }

        private void mensajeExito(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "modalMensajeExito('" + mensaje + "')", true);
        }

        #region webMethod

        [WebMethod]
        public static string cmbProductoDetRestos(string IdProd, string idterr, string idCte, string IdRik, string clave, string IdCd, string IdEmp, string EmpCnx, string pedidoProg)
        {

            CN_CatProducto cn_catproducto = new CN_CatProducto();
            Producto pr = new Producto();
            Producto pr2 = new Producto();
            int productoNuevo = 0;
            try
            {

                CN_CapAcys cnCa = new CN_CapAcys();
                if (idterr == "")
                {
                    return JsonConvert.SerializeObject(new { id = 1 });

                }
                if (idCte == "")
                {
                    return JsonConvert.SerializeObject(new { id = 2 });
                }
                if (IdRik == "")
                {
                    return JsonConvert.SerializeObject(new { id = 3 });
                }
                if (pedidoProg.Length == 0)
                {
                    pedidoProg = "0";
                }
                pedidoProg = pedidoProg == "0" ? "false" : pedidoProg;
                pedidoProg = pedidoProg == "1" ? "true" : pedidoProg;


                DataTable dt = (DataTable)HttpContext.Current.Session["Prod"];
                DataTable dt_Restos = (DataTable)HttpContext.Current.Session["Restos"];

                if (dt != null)
                {
                    foreach (DataRow i in dt.Rows)
                    {
                        if (i["Id_Prd"].ToString().Trim() == IdProd)
                        {
                            return JsonConvert.SerializeObject(new { id = 5 });
                        }

                    }
                }

                if (dt_Restos != null)
                {
                    foreach (DataRow j in dt_Restos.Rows)
                    {

                        if (j["Id_Prd"].ToString().Trim() == IdProd)
                        {
                            return JsonConvert.SerializeObject(new { id = 5 });
                        }
                    }
                }

                if (bool.Parse(pedidoProg) && cnCa.ExisteProductoEnGarantia(Convert.ToInt32(IdEmp), Convert.ToInt32(IdCd), Convert.ToInt64(IdProd), Convert.ToInt32(idterr), Convert.ToInt32(idCte), Convert.ToInt32(IdRik), EmpCnx))
                {
                    return JsonConvert.SerializeObject(new { id = 4 });
                }

                if (string.IsNullOrEmpty(clave))
                {
                    productoNuevo = 1;
                }
                pr.Id_Cte = !string.IsNullOrEmpty(idCte) ? Convert.ToInt32(idCte) : 0;
                cn_catproducto.ConsultaProductos(ref pr, EmpCnx, Convert.ToInt32(IdEmp), Convert.ToInt32(IdCd), Convert.ToInt64(IdProd), ref productoNuevo, 2);
                if (pr.Prd_Activo == 2 && pr.Prd_InvFinal <= 0)
                {
                    return JsonConvert.SerializeObject(new { id = -1, men = "Producto inactivo, reemplazalo con otra alternativa/consulta con el area operativa/CEDIS" });
                }
                cn_catproducto.ConsultarVentas(ref pr2, Convert.ToInt32(idCte), EmpCnx);

                int verificador = 0;
                cn_catproducto.ValidarProductoEcommerce(Convert.ToInt64(IdProd), EmpCnx, ref verificador);

                if (verificador == 0)
                {
                    return JsonConvert.SerializeObject(new { id = 7 });
                }

                return JsonConvert.SerializeObject(new { id = 0, Presentacion = pr.Prd_Presentacion, PrdUni = pr.Prd_UniNs, Cant = 0, Precio = pr.Prd_Precio, imp = pr.Prd_Precio, PRecioLista = pr.Prd_PLista, Descripcion = pr.Prd_Descripcion, mes1 = pr2.ventaMes[0].ToString().Trim(), mes2 = pr2.ventaMes[1].ToString().Trim(), mes3 = pr2.ventaMes[2].ToString().Trim() });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = ex.Message.ToString().Trim() });

            }
        }

        [WebMethod]
        public static string txtCantidad_TextChanged(string cantidad, string precio, string idCte, string Id_prd, string IdCd, string IdEmp, string EmpCnx)
        {
            try
            {
                int Prd_Cantidad = 0;
                double Prd_Precio = 0;
                double importe = 0;

                if (cantidad != "")
                {
                    if (int.Parse(cantidad) <= 0)
                    {
                        return JsonConvert.SerializeObject(new { id = 1 });
                    }
                }
                else
                {
                    return JsonConvert.SerializeObject(new { id = 1 });
                }

                if (!string.IsNullOrEmpty(cantidad))
                    Prd_Cantidad = Convert.ToInt32(cantidad);
                if (!string.IsNullOrEmpty(precio))
                    Prd_Precio = Convert.ToDouble(precio);

                importe = Prd_Cantidad * Prd_Precio;

                List<int> Actuales = new List<int>();
                CN_CatProducto catproducto = new CN_CatProducto();
                catproducto.ConsultaProducto_Disponible(Convert.ToInt32(IdEmp), Convert.ToInt32(IdCd), Id_prd, ref Actuales, EmpCnx);


                CN_CapPedidoVtaInst pedido_vta = new CN_CapPedidoVtaInst();
                int verificador = 0;
                if (!string.IsNullOrEmpty(idCte))
                    pedido_vta.ConsultarAAAEspecial(Convert.ToInt32(IdEmp), Convert.ToInt32(IdCd), Convert.ToInt32(idCte), Id_prd, ref verificador, EmpCnx);
                if (verificador > 0)
                {
                    return JsonConvert.SerializeObject(new { id = 3 });
                }
                else
                {

                    if (Actuales.Count > 0)
                    {
                        if (Prd_Cantidad > Actuales[2])
                        {
                            return JsonConvert.SerializeObject(new { id = 2, final = Actuales[0], asignado = Actuales[1], disponible = Actuales[2], importe = importe });
                        }
                        else
                        {
                            return JsonConvert.SerializeObject(new { id = 0, importe = importe });

                        }
                    }
                    else
                    {
                        return JsonConvert.SerializeObject(new { id = 0, importe = importe });

                    }
                }
            }
            catch (Exception)
            {
                return JsonConvert.SerializeObject(new { id = -1, mensaje = new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name });
            }
        }

        [WebMethod]
        public static string txtPrecio_TextChanged(string cantidad, string precio, string idCte, string Id_prd, string IdCd, string IdEmp, string EmpCnx)
        {
            try
            {
                CN_CapPedidoVtaInst pedido_vta = new CN_CapPedidoVtaInst();
                int verificador = 0;
                pedido_vta.ConsultarAAAEspecial(Convert.ToInt32(IdEmp), Convert.ToInt32(IdCd), Convert.ToDouble(idCte), Id_prd, ref verificador, EmpCnx);

                if (verificador > 0)
                {
                    return JsonConvert.SerializeObject(new { id = 1 });
                }

                double importe = double.Parse(cantidad) * double.Parse(precio);
                return JsonConvert.SerializeObject(new { id = 0, importe = importe });
            }
            catch (Exception)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name });

            }
        }

        [WebMethod(EnableSession = true)]
        public static string CalcularTotalVisible(string IdCd, string IdEmp, string IVA, string EmpCnx)
        {
            try
            {
                double imp = 0;

                DataTable dt = (DataTable)HttpContext.Current.Session["dtPedidoVI"];


                if (dt != null)
                {
                    foreach (DataRow i in dt.Rows)
                    {
                        imp += Convert.ToDouble(i["Prd_Importe"] == DBNull.Value ? 0 : i["Prd_Importe"]);
                    }
                }

                imp.ToString("F2").Trim();


                string IVASistema = (imp * double.Parse(IVA) / 100).ToString("F2").Trim();
                string totalImporte = (Convert.ToDouble(imp) + Convert.ToDouble(IVASistema)).ToString("F2").Trim();

                return JsonConvert.SerializeObject(new { id = 1, subtotal = imp, iva = IVASistema, total = totalImporte });
            }
            catch (Exception)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = "Error al realizar el el total de importe" });
            }

        }

        #endregion

        #endregion

        protected void btncaptacion_Guardar_Click(object sender, EventArgs e)
        {
            PreGuardar();
        }

        protected void btnCoreccion(object sender, EventArgs e)
        {
            if (Session["Respuesta" + Session.SessionID] != null)
            {
                if (Convert.ToBoolean(Session["Respuesta" + Session.SessionID]))
                {
                    Guardar();
                }
            }

        }

        protected void btnGuardar_ServerClick(object sender, EventArgs e)
        {
            Guardar();
        }

        protected void Buttbtnimprimiron2_ServerClick(object sender, EventArgs e)
        {
            Imprimir(Convert.ToInt32(Session["Id_Ped" + Session.SessionID]));
        }


        protected void rg1_HtmlDataCellPrepared(object sender, BootstrapGridViewTableDataCellEventArgs e)
        {

            bool inactivos = false;
            if (e.DataColumn.FieldName == "Id_Prd")
            {
                // Obtén el valor de la fila actual para Prd_ACtivo
                var prdActivoValue = e.GetValue("Prd_Activo") != null ? Convert.ToInt32(e.GetValue("Prd_Activo")) : 1;

                // Verifica la condición
                if (prdActivoValue == 2)
                {
                    // Cambia el color de fondo de la celda a rojo
                    e.Cell.BackColor = System.Drawing.Color.Red;
                    e.Cell.ForeColor = System.Drawing.Color.White;
                    inactivos = true;
                }

                if (inactivos)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(System.Web.UI.Page), "Alerta", ProPedidoVI2.Mensajes("Hay productos inactivos en el detalle de captación de pedidos"), false);
                }
            }
        }
    }

    public static class UploadControlHelper
    {
        const int DisposeTimeout = 5;
        const string TempDirectory = "~/UploadControl/Temp/";
        static readonly object storageListLocker = new object();

        static HttpContext Context { get { return HttpContext.Current; } }
        static string RootDirectory { get { return Context.Request.MapPath(TempDirectory); } }

        static IList<UploadedFilesStorage2> UploadedFilesStorage2List;
        static IList<UploadedFilesStorage2> uploadedFilesStorage2List
        {
            get
            {
                return UploadedFilesStorage2List;
            }
        }

        static UploadControlHelper()
        {
            UploadedFilesStorage2List = new List<UploadedFilesStorage2>();
        }

        static string CreateTempDirectoryCore()
        {
            string uploadDirectory = Path.Combine(RootDirectory, Path.GetRandomFileName());
            Directory.CreateDirectory(uploadDirectory);

            return uploadDirectory;
        }
        public static UploadedFilesStorage2 GetUploadedFilesStorage2ByKey(string key)
        {
            lock (storageListLocker)
            {
                return GetUploadedFilesStorage2ByKeyUnsafe(key);
            }
        }
        static UploadedFilesStorage2 GetUploadedFilesStorage2ByKeyUnsafe(string key)
        {
            UploadedFilesStorage2 storage = UploadedFilesStorage2List.Where(i => i.Key == key).SingleOrDefault();
            if (storage != null)
                storage.LastUsageTime = DateTime.Now;
            return storage;
        }
        public static string GenerateUploadedFilesStorage2Key()
        {
            return Guid.NewGuid().ToString("N");
        }
        public static void AddUploadedFilesStorage2(string key)
        {
            lock (storageListLocker)
            {
                UploadedFilesStorage2 storage = new UploadedFilesStorage2
                {
                    Key = key,
                    Path = CreateTempDirectoryCore(),
                    LastUsageTime = DateTime.Now,
                    Files = new List<UploadedFileInfo2>()
                };
                UploadedFilesStorage2List.Add(storage);
            }
        }
        public static void RemoveUploadedFilesStorage2(string key)
        {
            lock (storageListLocker)
            {
                UploadedFilesStorage2 storage = GetUploadedFilesStorage2ByKeyUnsafe(key);
                if (storage != null)
                {
                    Directory.Delete(storage.Path, true);
                    UploadedFilesStorage2List.Remove(storage);
                }
            }
        }
        public static void RemoveOldStorages()
        {
            if (!Directory.Exists(RootDirectory))
                Directory.CreateDirectory(RootDirectory);

            lock (storageListLocker)
            {
                string[] existingDirectories = Directory.GetDirectories(RootDirectory);
                foreach (string directoryPath in existingDirectories)
                {
                    UploadedFilesStorage2 storage = UploadedFilesStorage2List.Where(i => i.Path == directoryPath).SingleOrDefault();
                    if (storage == null || (DateTime.Now - storage.LastUsageTime).TotalMinutes > DisposeTimeout)
                    {
                        Directory.Delete(directoryPath, true);
                        if (storage != null)
                            UploadedFilesStorage2List.Remove(storage);
                    }
                }
            }
        }
        public static UploadedFileInfo2 AddUploadedFileInfo2(string key, string originalFileName)
        {
            UploadedFilesStorage2 currentStorage = GetUploadedFilesStorage2ByKey(key);
            UploadedFileInfo2 fileInfo = new UploadedFileInfo2
            {
                FilePath = Path.Combine(currentStorage.Path, Path.GetRandomFileName()),
                OriginalFileName = originalFileName,
                UniqueFileName = GetUniqueFileName(currentStorage, originalFileName)
            };
            currentStorage.Files.Add(fileInfo);

            return fileInfo;
        }
        public static UploadedFileInfo2 GetDemoFileInfo(string key, string fileName)
        {
            UploadedFilesStorage2 currentStorage = GetUploadedFilesStorage2ByKey(key);
            return currentStorage.Files.Where(i => i.UniqueFileName == fileName).SingleOrDefault();
        }
        public static string GetUniqueFileName(UploadedFilesStorage2 currentStorage, string fileName)
        {
            string baseName = Path.GetFileNameWithoutExtension(fileName);
            string ext = Path.GetExtension(fileName);
            int index = 1;

            while (currentStorage.Files.Any(i => i.UniqueFileName == fileName))
                fileName = string.Format("{0} ({1}){2}", baseName, index++, ext);

            return fileName;
        }


    }
}