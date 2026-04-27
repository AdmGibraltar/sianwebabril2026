using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocios;
using System.Data;
using Telerik.Web.UI;
using System.Data.OleDb;
using System.IO;
using System.Collections;
using System.Xml.Serialization;
using System.Text;
using System.Xml;
using System.ComponentModel;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using System.Text.RegularExpressions;
using System.Configuration;

namespace SIANWEB
{
    public partial class CapQueja : System.Web.UI.Page
    {
        #region Variables
        Telerik.Web.UI.RadComboBox RadCombobox;

        public Telerik.Web.UI.RadComboBox RadCombobox1
        {
            get { return RadCombobox; }
            set { RadCombobox = value; }
        }

        private Sesion sesion
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

        public DataTable objdtListaDoc { get; set; }
        protected DataTable objdtTablaDoc { get { if (ViewState["objdtTablaDoc"] != null) { return (DataTable)ViewState["objdtTablaDoc"]; } else { return objdtListaDoc; } } set { ViewState["objdtTablaDoc"] = value; } }

        public DataTable objdtListaProd { get; set; }
        protected DataTable objdtTablaProd { get { if (ViewState["objdtTablaProd"] != null) { return (DataTable)ViewState["objdtTablaProd"]; } else { return objdtListaProd; } } set { ViewState["objdtTablaProd"] = value; } }

        public string NombreArchivo;
        public string mensaje;

        public List<Producto> lstProductos
        {
            get { return (List<Producto>)Session["lstProductos" + Session.SessionID]; }
            set { Session["lstProductos" + Session.SessionID] = value; }
        }

        public List<Documento> lstDocumento
        {
            get { return (List<Documento>)Session["lstDocumento" + Session.SessionID]; }
            set { Session["lstDocumento" + Session.SessionID] = value; }
        }

        private int id_Emp;

        public int Id_Emp
        {
            get { return id_Emp; }
            set { id_Emp = value; }
        }
        private int id_Cd;

        public int Id_Cd
        {
            get { return id_Cd; }
            set { id_Cd = value; }
        }
        private int id_Queja;

        public int Id_Queja
        {
            get { return id_Queja; }
            set { id_Queja = value; }
        }

        private string respuesta;

        public string Respuesta
        {
            get { return respuesta; }
            set { respuesta = value; }
        }


        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {

                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("Login.aspx", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        int Tipo = 0;
                        Id_Emp = Convert.ToInt32(Page.Request.QueryString["Id_Emp"]);
                        Id_Cd = Convert.ToInt32(Page.Request.QueryString["Id_Cd"]);
                        Id_Queja = Convert.ToInt32(Page.Request.QueryString["Id_Queja"]);
                        Tipo = Convert.ToInt32(Page.Request.QueryString["Tipo"]);
                        Inicializar(Id_Queja, Id_Emp, Id_Cd, Tipo);
                        Tabla();
                        List<Documento> lstDocumento = new List<Documento>();
                        RadEditorResume.ToolsFile = "BasicTools.xml";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void cmbPrioridad_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                string cmd = e.Argument.ToString();
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                if (cmd == "Si")
                {
                    lblRes.Text = cmd;
                }
                else
                {
                    lblRes.Text = "No";
                }

                switch (e.Argument.ToString())
                {
                    case "RebindGrid":
                        rgProductos.Rebind();
                        break;
                    case "panel":
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void rgProductos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {

                    rgProductos.DataSource = this.objdtTablaProd;
                    if (rgProductos.Items.Count < 15)
                    {
                        if ((cmbtquejas.SelectedIndex == 1) || (cmbtquejas.SelectedIndex == 4))
                        {
                            rgProductos.MasterTableView.GetColumn("Prd_UniEmp").Display = false;
                            rgProductos.MasterTableView.GetColumn("Lote").Display = false;
                            rgProductos.MasterTableView.GetColumn("ConservarProd").Display = false;
                        }
                        if ((cmbtquejas.SelectedIndex == 2) || (cmbtquejas.SelectedIndex == 3))
                        {
                            rgProductos.MasterTableView.GetColumn("Prd_UniEmp").Display = false;
                            rgProductos.MasterTableView.GetColumn("Lote").Display = false;
                        }
                        if (cmbtquejas.SelectedIndex == 5 || cmbtquejas.SelectedIndex == 7)
                        {
                            rgProductos.MasterTableView.GetColumn("Prd_UniEmp").Display = false;
                            rgProductos.MasterTableView.GetColumn("ConservarProd").Display = false;
                        }
                        if (cmbtquejas.SelectedIndex == 6 || cmbtquejas.SelectedIndex == 8)
                        {
                            rgProductos.MasterTableView.GetColumn("ConservarProd").Display = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void rgProductos_InsertCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem InsertItem = e.Item as GridEditableItem;

                Producto producto = new Producto();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                GridDataItem item = (GridDataItem)e.Item;

                if (cmbtquejas.SelectedIndex == 4)
                {
                    if ((InsertItem["Id_PrdN"].FindControl("txtId_Prd") as RadNumericTextBox).Text == "" ||
                       ((InsertItem["Cantidad"].FindControl("txtCantidad") as RadNumericTextBox).Text == "" ||
                       ((InsertItem["Fabricacion"].Controls[0] as RadDatePicker).SelectedDate.ToString() == "" ||
                       ((InsertItem["Costo"].FindControl("txtCosto") as RadNumericTextBox).Text == "" ||
                       ((InsertItem["Num_Fac"].FindControl("txtNum_Fac") as RadNumericTextBox).Text == ""
                       )))))
                    {
                        Alerta("Favor de proporcionar todos los campos, ya que son obligatorios.");
                        e.Canceled = true;
                        return;

                    }
                }

                if (cmbtquejas.SelectedIndex == 5)
                {

                    if ((InsertItem["Id_PrdN"].FindControl("txtId_Prd") as RadNumericTextBox).Text == "" ||
                       ((InsertItem["Cantidad"].FindControl("txtCantidad") as RadNumericTextBox).Text == "" ||
                       ((InsertItem["Lote"].FindControl("txtLote") as RadTextBox).Text == "" ||
                       ((InsertItem["Fabricacion"].Controls[0] as RadDatePicker).SelectedDate.ToString() == "" ||
                       ((InsertItem["Caducidad"].Controls[0] as RadDatePicker).SelectedDate.ToString() == "" ||
                       ((InsertItem["Marca"].FindControl("txtMarca") as RadTextBox).Text == "" ||
                       ((InsertItem["Costo"].FindControl("txtCosto") as RadNumericTextBox).Text == "" ||
                       ((InsertItem["Num_Fac"].FindControl("txtNum_Fac") as RadNumericTextBox).Text == "" ||
                       ((InsertItem["Nom_Prov"].FindControl("txtNom_Prov") as RadTextBox).Text == ""
                       )))))))))
                    {
                        Alerta("Favor de proporcionar todos los campos, ya que son obligatorios.");
                        e.Canceled = true;
                        return;
                    }
                }

                if (cmbtquejas.SelectedIndex == 6)
                {
                    if ((InsertItem["Lote"].FindControl("txtLote") as RadTextBox).Text == "" ||
                       ((InsertItem["Fabricacion"].Controls[0] as RadDatePicker).SelectedDate.ToString() == "" ||
                       ((InsertItem["Caducidad"].Controls[0] as RadDatePicker).SelectedDate.ToString() == "")))
                    {
                        Alerta("Favor de proporcionar todos los campos, ya que son obligatorios.");
                        e.Canceled = true;
                        return;
                    }
                }

                if (cmbtquejas.SelectedIndex == 7)
                {
                    if ((InsertItem["Fabricacion"].Controls[0] as RadDatePicker).SelectedDate.ToString() == "" ||
                       ((InsertItem["Cantidad"].FindControl("txtCantidad") as RadNumericTextBox).Text == "" ||
                       ((InsertItem["Costo"].FindControl("txtCosto") as RadNumericTextBox).Text == "" ||
                       ((InsertItem["Num_Fac"].FindControl("txtNum_Fac") as RadNumericTextBox).Text == ""))))
                    {
                        Alerta("Favor de proporcionar todos los campos, ya que son obligatorios.");
                        e.Canceled = true;
                        return;
                    }
                }

                if (cmbtquejas.SelectedIndex == 8)
                {
                    if ((InsertItem["Lote"].FindControl("txtLote") as RadTextBox).Text == "" ||
                       ((InsertItem["Fabricacion"].Controls[0] as RadDatePicker).SelectedDate.ToString() == ""))
                    {
                        Alerta("Favor de proporcionar todos los campos, ya que son obligatorios.");
                        e.Canceled = true;
                        return;
                    }
                }

                producto.Id_Prd = Convert.ToInt64((InsertItem["Id_PrdN"].FindControl("txtId_Prd") as RadNumericTextBox).Text);
                producto.Descripcion = (InsertItem["Descripcion"].FindControl("txtPrd_Descripcion") as RadTextBox).Text;
                producto.Presentacion = (InsertItem["Presentacion"].FindControl("txtPrd_Presentacion") as RadTextBox).Text;
                producto.Cantidad = Convert.ToInt32((InsertItem["Cantidad"].FindControl("txtCantidad") as RadNumericTextBox).Text);

                if ((InsertItem["Prd_UniEmp"].FindControl("txtPrd_UniEmp") as RadNumericTextBox).Text == "")
                    producto.Prd_UniEmp = 0;
                else
                    producto.Prd_UniEmp = float.Parse((InsertItem["Prd_UniEmp"].FindControl("txtPrd_UniEmp") as RadNumericTextBox).Text);

                if ((InsertItem["Lote"].FindControl("txtLote") as RadTextBox).Text == "")
                    producto.Lote = null;
                else
                    producto.Lote = (InsertItem["Lote"].FindControl("txtLote") as RadTextBox).Text;

                if ((InsertItem["Fabricacion"].Controls[0] as RadDatePicker).SelectedDate.ToString() == "")
                    producto.Fabricacion = Convert.ToDateTime("2000/01/01 12:00:00.000");
                else
                    producto.Fabricacion = Convert.ToDateTime((InsertItem["Fabricacion"].Controls[0] as RadDatePicker).SelectedDate);

                if ((InsertItem["Caducidad"].Controls[0] as RadDatePicker).SelectedDate.ToString() == "")
                    producto.Caducidad = Convert.ToDateTime("2000/01/01 12:00:00.000");
                else
                    producto.Caducidad = Convert.ToDateTime((InsertItem["Caducidad"].Controls[0] as RadDatePicker).SelectedDate);

                if ((InsertItem["Marca"].FindControl("txtMarca") as RadTextBox).Text == "")
                    producto.Marca = "";
                else
                    producto.Marca = (InsertItem["Marca"].FindControl("txtMarca") as RadTextBox).Text;

                producto.Costo = float.Parse((InsertItem["Costo"].FindControl("txtCosto") as RadNumericTextBox).Text);

                if ((InsertItem["Num_Fac"].FindControl("txtNum_Fac") as RadNumericTextBox).Text == "")
                    producto.Num_Fac = 0;
                else
                    producto.Num_Fac = Convert.ToInt32((InsertItem["Num_Fac"].FindControl("txtNum_Fac") as RadNumericTextBox).Text);

                if ((InsertItem["Nom_Prov"].FindControl("txtNom_Prov") as RadTextBox).Text == "")
                    producto.Nom_Prov = null;
                else
                    producto.Nom_Prov = (InsertItem["Nom_Prov"].FindControl("txtNom_Prov") as RadTextBox).Text;

                if (Convert.ToBoolean((InsertItem["ConservarProd"].FindControl("cmbConservar") as RadComboBox).Text == ""))
                    producto.ConservarProd = true;
                else
                    if (Convert.ToBoolean((InsertItem["ConservarProd"].FindControl("cmbConservar") as RadComboBox).Text == "Si"))
                    producto.ConservarProd = true;
                else
                    producto.ConservarProd = false;

                //agregar producto de orden de compra a la lista
                //this.ListaProductos_AgregarProducto(producto);    

                wsCatalogoUnico(producto.Id_Prd, ref producto);

                if (producto.Descripcion == "")
                {
                    Alerta("Este producto probablemente este en estatus no valido.");
                    e.Canceled = true;
                    return;
                }

                string Motivos = MotivosSeleccionados(RadListBox1);
                if (Motivos == "43,")
                {
                    int a = producto.Cantidad % producto.MultiploVenta;
                    if (a > 0)
                    {
                        Alerta("Favor de proporcionar una cantidad considerando el multiplo de venta " + producto.MultiploVenta + ".");
                        e.Canceled = true;
                        return;
                    }
                }

                ArrayList ArrayProd = new ArrayList();
                ArrayProd.Add(sesion.Id_Emp);
                ArrayProd.Add(sesion.Id_Cd);
                ArrayProd.Add(producto.Id_Prd);
                ArrayProd.Add(producto.Descripcion);
                ArrayProd.Add(producto.Presentacion);
                ArrayProd.Add(producto.Cantidad);
                ArrayProd.Add(producto.Prd_UniEmp);
                ArrayProd.Add(producto.Lote);
                ArrayProd.Add(producto.Fabricacion.ToString("dd/MM/yyyy"));
                ArrayProd.Add(producto.Caducidad.ToString("dd/MM/yyyy"));
                ArrayProd.Add(producto.Marca);
                ArrayProd.Add(producto.Costo);
                ArrayProd.Add(producto.Num_Fac);
                ArrayProd.Add(producto.Nom_Prov);
                ArrayProd.Add(producto.ConservarProd);
                ArrayProd.Add(producto.Costoestandar);
                objdtTablaProd.Rows.Add(ArrayProd.ToArray());

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void wsCostoEstandar(Int64 Id_Prd, ref Producto prod)
        {
            wsQuejas.Service1 wsCatUnico = new wsQuejas.Service1();
            XmlDocument xmlProducto = new System.Xml.XmlDocument();
            string xmlResultado = "";

            xmlResultado = wsCatUnico.XMLSGCUP_Producto(Id_Prd);
            xmlProducto.LoadXml(xmlResultado);

            prod.Costoestandar = Convert.ToDouble(xmlProducto.SelectSingleNode("//COSTO_ESTANDAR").InnerText.ToString());
        }

        protected void rgProductos_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem insertedItem = (GridEditableItem)e.Item;
                Producto producto = new Producto();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                GridDataItem item = (GridDataItem)e.Item;

                producto.Id_Prd = Convert.ToInt64((insertedItem["Id_PrdN"].FindControl("txtId_Prd") as RadNumericTextBox).Text);
                producto.Descripcion = (insertedItem["Descripcion"].FindControl("txtPrd_Descripcion") as RadTextBox).Text;
                producto.Presentacion = (insertedItem["Presentacion"].FindControl("txtPrd_Presentacion") as RadTextBox).Text;
                producto.Cantidad = Convert.ToInt32((insertedItem["Cantidad"].FindControl("txtCantidad") as RadNumericTextBox).Text);
                producto.Lote = (insertedItem["Lote"].FindControl("txtLote") as RadTextBox).Text;
                producto.Fabricacion = Convert.ToDateTime((insertedItem["Fabricacion"].Controls[0] as RadDatePicker).SelectedDate.ToString());
                producto.Caducidad = Convert.ToDateTime((insertedItem["Caducidad"].Controls[0] as RadDatePicker).SelectedDate.ToString());
                producto.Marca = (insertedItem["Marca"].FindControl("txtMarca") as RadTextBox).Text;
                producto.Costo = float.Parse((insertedItem["Costo"].FindControl("txtCosto") as RadNumericTextBox).Text);
                producto.Num_Fac = Convert.ToInt32((insertedItem["Num_Fac"].FindControl("txtNum_Fac") as RadNumericTextBox).Text);
                producto.Nom_Prov = (insertedItem["Nom_Prov"].FindControl("txtNom_Prov") as RadTextBox).Text;

                if (Convert.ToBoolean((insertedItem["ConservarProd"].FindControl("cmbConservar") as RadComboBox).Text == ""))
                    producto.ConservarProd = true;
                else
                    if (Convert.ToBoolean((insertedItem["ConservarProd"].FindControl("cmbConservar") as RadComboBox).Text == "Si"))
                    producto.ConservarProd = true;
                else
                    producto.ConservarProd = false;

                DataRow[] Ar_dr;

                Ar_dr = objdtTablaProd.Select("Id_Prd='" + Convert.ToInt64(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Prd"]) + "'");
                if (Ar_dr.Length > 0)
                {
                    Ar_dr[0].BeginEdit();
                    Ar_dr[0]["Id_Prd"] = producto.Id_Prd;               //Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Prd"]);
                    Ar_dr[0]["Descripcion"] = producto.Descripcion;     //item.OwnerTableView.DataKeyValues[item.ItemIndex]["Descripcion"];
                    Ar_dr[0]["Presentacion"] = producto.Presentacion;   //item.OwnerTableView.DataKeyValues[item.ItemIndex]["Presentacion"];
                    Ar_dr[0]["Cantidad"] = producto.Cantidad;           //Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Cantidad"]);
                    Ar_dr[0]["Prd_UniEmp"] = producto.Prd_UniEmp;
                    Ar_dr[0]["Lote"] = producto.Lote;                   //item.OwnerTableView.DataKeyValues[item.ItemIndex]["Lote"];
                    Ar_dr[0]["Fabricacion"] = producto.Fabricacion.ToString("dd/MM/yyyy");     //Convert.ToDateTime(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Fabricacion"]);
                    Ar_dr[0]["Caducidad"] = producto.Caducidad.ToString("dd/MM/yyyy");         //Convert.ToDateTime(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Caducidad"]);
                    Ar_dr[0]["Marca"] = producto.Marca;                 //item.OwnerTableView.DataKeyValues[item.ItemIndex]["Marca"];
                    Ar_dr[0]["Costo"] = producto.Costo;                 //Convert.ToDouble(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Costo"]);
                    Ar_dr[0]["Num_Fac"] = producto.Num_Fac;             //Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Num_Fac"]);
                    Ar_dr[0]["Nom_Prov"] = producto.Nom_Prov;           //item.OwnerTableView.DataKeyValues[item.ItemIndex]["Nom_Prov"];
                    Ar_dr[0]["ConservarProd"] = producto.ConservarProd;


                    objdtTablaProd.AcceptChanges();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void rgProductos_DeleteCommand1(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridDataItem item = (GridDataItem)e.Item;
                CN_Queja CN = new CN_Queja();
                Int64 Id_Prd = Convert.ToInt64(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Prd"]);
                int Id_Queja = 0;
                if (lblIdQueja.Text != "")
                {
                    Id_Queja = int.Parse(lblIdQueja.Text);
                }
                if (id_Queja > 0)
                {
                    //actualizar producto de orden de compra a la lista
                    this.EliminarProducto(Id_Prd);
                    CN.EliminarProducto(sesion, Id_Queja, Id_Prd);
                    wsQuejas.Service1 ws = new wsQuejas.Service1();
                    ws.EliminaProducto(sesion.Id_Emp, sesion.Id_Cd_Ver, Id_Prd, Id_Queja);
                    Alerta("El producto se eliminó correctamente.");
                }
                else
                {
                    this.EliminarProducto(Id_Prd);
                    Alerta("El producto se eliminó correctamente.");
                }

            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, "No se pudo eliminar el registro.");
            }
        }

        private void EliminarProducto(Int64 id_Prd)
        {
            DataRow[] Ar_dr;

            Ar_dr = objdtTablaProd.Select("Id_Prd='" + id_Prd + "'");
            if (Ar_dr.Length > 0)
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Ar_dr[0].Delete();
                objdtTablaProd.AcceptChanges();
            }
            rgProductos.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = true;
            rgProductos.Rebind();
        }

        protected void rgProductos_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {

        }

        protected void rgProductos_ItemDataBound(object sender, GridItemEventArgs e)
        {
            Sesion Sesion = new CapaEntidad.Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
            {
                GridEditableItem editItem = (GridEditableItem)e.Item;
                CargarConservarProd(editItem);

            }
        }

        private void CargarConservarProd(GridEditableItem editItem)
        {
            if (rgProductos.Items.Count < 16)
            {
                RadComboBox cmbConservar = (RadComboBox)editItem.FindControl("cmbConservar");
                List<Comun> lstConservar = new List<Comun>();
                Comun com;
                com = new Comun();

                com.Id = 1;
                com.Descripcion = "Si";

                lstConservar.Add(com);
                com = new Comun();
                com.Id = 0;
                com.Descripcion = "No";
                lstConservar.Add(com);

                cmbConservar.DataSource = lstConservar;
                cmbConservar.DataValueField = "Id";
                cmbConservar.DataTextField = "Descripcion";
                cmbConservar.DataBind();
            }
        }


        protected void rgProductos_ItemCreated(object sender, GridItemEventArgs e)
        {

            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
            {
                GridEditableItem editItem = (GridEditableItem)e.Item;

                if (cmbtquejas.SelectedValue == "-1")
                {
                    Alerta("Favor de seleccionar tipo de  queja para continuar.");
                    rgProductos.DataSource = null;
                }
            }
        }

        protected void txtProducto_TextChanged(object sender, EventArgs e)
        {
            try
            {
                RadNumericTextBox combo = (RadNumericTextBox)sender;
                //obtiene la tabla contenedora de los controles de ediciÃ³n de registro del Grid
                Telerik.Web.UI.GridTableCell tabla = (Telerik.Web.UI.GridTableCell)combo.Parent;

                RadNumericTextBox txtId_Prd = (RadNumericTextBox)tabla.FindControl("txtId_Prd");
                RadNumericTextBox txtCantidad = (RadNumericTextBox)tabla.FindControl("txtCantidad");
                RadTextBox txtPrd_Descripcion = (RadTextBox)tabla.FindControl("txtPrd_Descripcion");
                RadTextBox txtPrd_Presentacion = (RadTextBox)tabla.FindControl("txtPrd_Presentacion");
                RadNumericTextBox txtPrd_UniEmp = (RadNumericTextBox)tabla.FindControl("txtPrd_UniEmp");
                RadNumericTextBox txtCosto = (RadNumericTextBox)tabla.FindControl("txtCosto");
                RadTextBox txtNom_Prov = (RadTextBox)tabla.FindControl("txtNom_Prov");

                //Se valida si existe el producto en la tabla
                DataRow[] Ar_dr;
                Int64 prd = Convert.ToInt64((txtId_Prd.Parent.Parent.FindControl("txtId_Prd") as RadNumericTextBox).Value);
                Ar_dr = objdtTablaProd.Select("Id_Prd='" + prd + "'");

                if (Ar_dr.Length > 0)
                {
                    //Se valida el tipo de queja que solicita lote 
                    if (cmbtquejas.SelectedValue != "5" && cmbtquejas.SelectedValue != "7" && cmbtquejas.SelectedValue != "8")
                    {
                        Alerta("Este tipo de queja no permite introducir mas de un registro del mismo código.");
                        txtPrd_Descripcion.Text = "";
                        txtPrd_Presentacion.Text = "";
                        txtCosto.Text = "";
                        txtNom_Prov.Text = "";
                        txtId_Prd.Text = "";
                        txtId_Prd.Focus();
                        return;
                    }
                }


                Producto producto = new Producto();
                producto.Id_Prd = Int64.Parse(txtId_Prd.Text);
                wsCatalogoUnico(producto.Id_Prd, ref producto);


                txtPrd_Descripcion.Text = producto == null ? string.Empty : producto.Descripcion;
                txtPrd_Presentacion.Text = producto == null ? string.Empty : producto.Prd_Presentacion;
                txtPrd_UniEmp.Text = producto == null ? string.Empty : producto.Prd_UniEmp.ToString();
                txtCosto.Text = producto == null ? string.Empty : producto.Prd_AAA.ToString();
                txtNom_Prov.Text = producto == null ? string.Empty : producto.Nom_Prov;
                txtCantidad.Focus();
            }
            catch (Exception)
            {
                Alerta("Este producto no fue encontrado o se encuentra en estatus de obsoleto, favor de capturar otro producto");
            }
        }

        private void wsCatalogoUnico(Int64 Id_Prd, ref Producto prod)
        {
            wsQuejas.Service1 wsCatUnico = new wsQuejas.Service1();
            XmlDocument xmlProducto = new System.Xml.XmlDocument();
            string xmlResultado = "";
            xmlResultado = wsCatUnico.XMLSGCUP_Producto(Id_Prd);
            xmlProducto.LoadXml(xmlResultado);

            prod.Id_Prd = Int64.Parse(xmlProducto.SelectSingleNode("//PRODUCTO").InnerText.ToString());
            prod.Descripcion = xmlProducto.SelectSingleNode("//NOMBRE_PRODUCTO").InnerText.ToString();
            prod.Prd_Presentacion = xmlProducto.SelectSingleNode("//PRESENTACION_DE_LA_UNIDAD").InnerText.ToString();
            prod.Prd_UniEmp = float.Parse(xmlProducto.SelectSingleNode("//UNIDAD_DE_VENTA").InnerText.ToString());
            prod.Prd_AAA = Convert.ToDouble(xmlProducto.SelectSingleNode("//PAAA_ACTUAL").InnerText.ToString());
            prod.Nom_Prov = xmlProducto.SelectSingleNode("//NOMBRE_PROVEEDOR").InnerText.ToString();
            prod.Costoestandar = Convert.ToDouble(xmlProducto.SelectSingleNode("//COSTO_ESTANDAR").InnerText.ToString());
            prod.MultiploVenta = Convert.ToInt32(xmlProducto.SelectSingleNode("//MULTIPLO_DE_VENTA").InnerText.ToString());
        }

        protected void txtProducto_Load(object sender, EventArgs e)
        {
            //objdtTablaProd = sender;
        }

        #endregion

        #region Funciones

        private void Tabla()
        {
            double ancho = 0;
            foreach (GridColumn gc in rgProductos.Columns)
            {
                if (gc.Display)
                {
                    ancho = ancho + gc.HeaderStyle.Width.Value;
                }
            }
            rgProductos.Width = Unit.Pixel(Convert.ToInt32(ancho));
        }

        private void Inicializar(int Id_Queja, int Id_Emp, int Id_Cd, int Tipo)
        {
            try
            {
                CargarTQuejasCentral(Id_Queja);
                CargarLineaTransporteCentral(Id_Queja);
                //CargarTQuejas();
                CargarPrioridad();
                CargarSucursal();
                CargarOcurrio();
                InicializarTablaProductos();
                InicializarTablaDocumentos();

                txtFecha.SelectedDate = DateTime.Now;
                rdpFechaEmbarque.SelectedDate = DateTime.Now;

                CamposTransporte(0);

                if (Id_Queja > 0)
                {
                    LlenaFormularioQueja(Id_Queja, Id_Emp, Id_Cd, Tipo);
                }
                else
                {
                    Getlist();
                    txtCliente.Text = sesion.U_Nombre;
                    txtIdCd.Text = sesion.Id_Cd_Ver.ToString();
                    RadEditorResume.Content = string.Empty;
                    txtembarque.Text = string.Empty;
                    txtflete.Text = string.Empty;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void InicializarTablaDocumentos()
        {
            try
            {
                objdtListaDoc = new DataTable();
                objdtListaDoc.Columns.Add("Id_Emp");
                objdtListaDoc.Columns.Add("Id_Cd");
                objdtListaDoc.Columns.Add("Id_Doc");
                objdtListaDoc.Columns.Add("Doc_Nombre");
                objdtListaDoc.Columns.Add("Formato");
                objdtListaDoc.Columns.Add("Tamano");
                objdtListaDoc.Columns.Add("TipoDoc");
                objdtListaDoc.Columns.Add("Archivo");
                objdtTablaDoc = objdtListaDoc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LlenaFormularioQueja(int Id_Queja, int Id_Emp, int Id_Cd)
        {
            try
            {
                objdtListaDoc = new DataTable();
                objdtListaDoc.Columns.Add("Id_Emp");
                objdtListaDoc.Columns.Add("Id_Cd");
                objdtListaDoc.Columns.Add("Id_Doc");
                objdtListaDoc.Columns.Add("Doc_Nombre");
                objdtListaDoc.Columns.Add("Formato");
                objdtListaDoc.Columns.Add("Tamano");
                objdtListaDoc.Columns.Add("TipoDoc");
                objdtListaDoc.Columns.Add("Archivo");
                objdtTablaDoc = objdtListaDoc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LlenaFormularioQueja(int Id_Queja, int Id_Emp, int Id_Cd, int Tipo)
        {
            try
            {
                lblEsConsulta.Text = "0";
                RadEditorResume.ToolsFile = "BasicTools.xml";
                Sesion Sesion = new CapaEntidad.Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Id_Queja > 0 && Tipo == 1)
                {
                    lblEsConsulta.Text = "1";
                }

                List<Producto> lstProducto = new List<Producto>();
                List<Documento> lstDocumentos = new List<Documento>();
                CN_Queja Negocio = new CN_Queja();
                Queja queja = new Queja();
                queja.Id_Queja = Id_Queja;
                queja.Id_Emp = Id_Emp;
                queja.Id_Cd = Id_Cd;
                Negocio.ConsultaQueja(Sesion, ref queja, ref lstProducto, ref lstDocumentos);

                //se cargan 
                //Datos Generales
                if (Tipo == 1)
                    lblIdQueja.Text = queja.Id_Queja.ToString();
                else
                    lblIdQueja.Text = "0";

                txtCliente.Text = queja.Nom_Cliente;
                txtIdCd.Text = queja.Id_Cd.ToString();
                txtIdcte_directo.Text = queja.Id_CteDirecto.ToString();
                txtNomcte_directo.Text = queja.Nom_CteDirecto;

                //Categorizacion
                cmbtquejas.SelectedIndex = queja.Id_TQueja;
                cmbPrioridad.SelectedIndex = queja.Id_Prioridad;

                //Descripcion de problema
                txtFecha.SelectedDate = queja.Fec_Evento;
                cmbOcurrio.SelectedIndex = queja.DondeOcurrio;
                txtOtroMotivo.Text = queja.OtroMotivo;

                //Descripcion detallada del problema
                RadEditorResume.Content = queja.Descripcion;
                txtembarque.Text = queja.Embarque;
                rdpFechaEmbarque.SelectedDate = queja.FechaEmbarque;
                txtflete.Text = queja.Guia_Flete;

                //Se llena datos de queja tipo transporte

                RadComboBoxItem item = CmbTransporte.Items.FindItemByText(queja.NomComTransporte);
                if (item != null)
                {
                    CmbTransporte.ClearSelection();
                    item.Selected = true;
                }
                else
                {
                    if (!String.IsNullOrEmpty(queja.NomComTransporte))
                    {
                        int totalItems = CmbTransporte.Items.Count;
                        CmbTransporte.Items.Insert(totalItems, new RadComboBoxItem(queja.NomComTransporte, totalItems.ToString()));
                        CmbTransporte.SelectedIndex = totalItems;
                    }
                }
                txtNomChofer.Text = queja.NomChofer;
                txtPlacas.Text = queja.Placas;
                txtFechaEmbarque.SelectedDate = queja.Fec_Embarque;

                //RBM Enero 2021
                //Se agrega dato remision creado
                //Inicio
                lblRemision.Visible = true;
                txtRemision.Text = queja.Id_Rem.ToString();
                txtRemision.Visible = true;

                //End

                CargarMotivos(queja.Id_TQueja);

                SeleccionaMotivos(queja.Motivos);

                //Se cargan los productos de la quejas
                rgProductos.DataSource = lstProducto;
                rgProductos.DataBind();

                CargarProductos(lstProducto);
                HabilitarCampos(Tipo);


                CargarDocumentos(lstDocumentos);

                if (lstDocumentos.Count > 0)
                    lstDocumento = lstDocumentos;
                else
                    lstDocumento = null;

                //Se cargan los documentos relacionados en cada tabla 
                gvFotos.DataSource = lstDocumentos.Where(Documento => Documento.TipoDoc == "Foto");
                gvFotos.DataBind();

                gvFactura.DataSource = lstDocumentos.Where(Documento => Documento.TipoDoc == "Factura");
                gvFactura.DataBind();

                gvAdicional.DataSource = lstDocumentos.Where(Documento => Documento.TipoDoc == "Adicional");
                gvAdicional.DataBind();
                CmbTransporte.Enabled = false;

                if (Tipo == 1)
                {
                    txtCliente.Enabled = false;
                    txtIdCd.Enabled = false;
                    cmbNomSucursal.Enabled = false;
                    cmbtquejas.Enabled = false;
                    txtIdcte_directo.Enabled = false;
                    txtNomcte_directo.Enabled = false;
                    cmbPrioridad.Enabled = false;
                    txtFecha.Enabled = false;
                    cmbOcurrio.Enabled = false;
                    txtOtroMotivo.Enabled = false;
                    RadEditorResume.Enabled = false;
                    rdpFechaEmbarque.Enabled = false;
                    txtembarque.Enabled = false;
                    txtflete.Enabled = false;
                    RadEditorResume.Enabled = true;
                }
                else
                {
                    txtIdcte_directo.Enabled = true;
                    txtNomcte_directo.Enabled = true;
                    RadListBox1.Enabled = true;
                }


                if (queja.Id_TQueja == 6 || queja.Id_TQueja == 4)
                {
                    CamposTransporte(1);
                }
                if (Tipo == 2)
                {
                    txtRemision.Text = "";
                    Alerta("Favor de asegurarse de realizar los cambios necesarios antes de guardar, ya que después solo podrá agregar documentos.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SeleccionaMotivos(string Motivos)
        {
            String value = Motivos;
            Char delimiter = ',';
            String[] seleccion = value.Split(delimiter);
            foreach (string valor in seleccion)
            {
                if (valor != "")
                {
                    foreach (RadListBoxItem item in RadListBox1.Items)
                    {
                        if (item.Value == valor || "42" == valor || "-1" == valor)
                        {
                            item.Checked = true;

                        }
                    }
                    RadListBox1.Enabled = false;
                }
            }
        }

        private void Getlist()
        {
            List<Producto> lstProductos = new List<Producto>();
            rgProductos.DataSource = this.lstProductos = new List<Producto>();
            rgProductos.DataBind();
        }

        private void CargarPrioridad()
        {
            try
            {
                Sesion Sesion = new CapaEntidad.Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<Comun> Lista = new List<Comun>();
                CapaNegocios.CN__Comun comun = new CN__Comun();
                comun.LlenaCombo(sesion.Emp_Cnx, "spCatPrioridad_Combo", ref cmbPrioridad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarOcurrio()
        {
            try
            {
                Sesion Sesion = new CapaEntidad.Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<Comun> Lista = new List<Comun>();
                CapaNegocios.CN__Comun comun = new CN__Comun();
                comun.LlenaCombo(sesion.Emp_Cnx, "spCatOcurrio_Combo", ref cmbOcurrio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarSucursal()
        {
            try
            {
                Sesion Sesion = new CapaEntidad.Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<Comun> Lista = new List<Comun>();
                CapaNegocios.CN__Comun comun = new CN__Comun();
                comun.LlenaCombo(Sesion.Id_Cd_Ver, sesion.Emp_Cnx, "spCatSucursal_Combo", ref cmbNomSucursal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarTQuejasCentral(int IdQueja)
        {
            try
            {
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionSIANCentral"];

                Sesion Sesion = new CapaEntidad.Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                CapaNegocios.CN__Comun comun = new CN__Comun();

                List<eTipoQueja> listtipoQueja = new List<eTipoQueja>();
                eTipoQueja eBitacoraCancelacion = new eTipoQueja();

                eBitacoraCancelacion.Estatus = 1;
                if (IdQueja > 0)
                {
                    eBitacoraCancelacion.Estatus = -1;


                }

                new CN_TipoQueja().ConsultaTipoQueja_Buscar(eBitacoraCancelacion, ref listtipoQueja, Conexion);

                if (listtipoQueja.Count > 0)
                {

                    cmbtquejas.DataSource = listtipoQueja;
                    cmbtquejas.DataValueField = "Id_tQueja";     // Propiedad que se usará como valor
                    cmbtquejas.DataTextField = "Tipo_Queja";     // Propiedad que se mostrará
                    cmbtquejas.DataBind();

                    // With this corrected line:
                    cmbtquejas.Items.Insert(0, new RadComboBoxItem("Seleccione una opción", "0"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarLineaTransporteCentral(int Id_Queja)
        {
            try
            {
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionSIANCentral"];

                Sesion Sesion = new CapaEntidad.Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                CapaNegocios.CN__Comun comun = new CapaNegocios.CN__Comun();

                List<eLineaTransporte> listtipoQueja = new List<eLineaTransporte>();
                eLineaTransporte eBitacoraCancelacion = new eLineaTransporte();

                eBitacoraCancelacion.Estatus = "A";
                //if (Id_Queja > 0)
                //{
                //    eBitacoraCancelacion.Estatus = "";
                //}

                new CN_LineaTransporte().ConsultaLineaTransporte_Buscar(eBitacoraCancelacion, ref listtipoQueja, Conexion);

                if (listtipoQueja.Count > 0)
                {

                    CmbTransporte.DataSource = listtipoQueja;
                    CmbTransporte.DataValueField = "Id";
                    CmbTransporte.DataTextField = "LineaTransporte";
                    CmbTransporte.DataBind();

                    // With this corrected line:
                    CmbTransporte.Items.Insert(0, new RadComboBoxItem("Seleccione una opción", "0"));
                }
            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }
        }

        private void CargarLineaTransporte()
        {
            try
            {
                Sesion Sesion = new CapaEntidad.Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                CapaNegocios.CN__Comun comun = new CapaNegocios.CN__Comun();
                comun.LlenaCombo(Sesion.Emp_Cnx, "spCatLineaTransporte_Combo", ref CmbTransporte);
                if (Lista.Count > 0)
                {
                    CmbTransporte.DataSource = Lista;
                    CmbTransporte.DataValueField = "Id";
                    CmbTransporte.DataTextField = "Descripcion";
                    CmbTransporte.DataBind();
                }
            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }
        }

        private void CargarTQuejas()
        {
            try
            {
                Sesion Sesion = new CapaEntidad.Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<Comun> Lista = new List<Comun>();
                CapaNegocios.CN__Comun comun = new CN__Comun();
                comun.LlenaCombo(sesion.Emp_Cnx, "spCatTipoQuejas_Combo", ref cmbtquejas);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Guardar(DataTable objdtTablaProd)
        {
            try
            {
                int respuesta = 0;
                Sesion Sesion = new CapaEntidad.Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                XmlSerializer serializar = new XmlSerializer(typeof(Queja));
                string UrlDoc = Server.MapPath("~/App_Data") + "\\" + "Lista.xml";
                //string mensaje;
                if (ValidarCampos() == 0)
                    return;
                else
                {
                    //Se carga la queja
                    Queja queja = new Queja();
                    Documento doc;
                    List<Documento> LstDocumentos = new List<Documento>();

                    queja.Id_Emp = sesion.Id_Emp;
                    queja.Id_Cd = sesion.Id_Cd_Ver;
                    queja.Id_Cliente = sesion.Id_U;
                    queja.Nom_Cliente = txtCliente.Text;
                    queja.Correo = Sesion.U_Correo;
                    if (txtIdcte_directo.Text == "")
                        queja.Id_CteDirecto = 0;
                    else
                        queja.Id_CteDirecto = int.Parse(txtIdcte_directo.Text);
                    queja.Nom_CteDirecto = txtNomcte_directo.Text;
                    queja.Id_TQueja = cmbtquejas.SelectedIndex;
                    queja.Id_Prioridad = 1;
                    //--------------------------------------------------------
                    queja.Fec_Evento = txtFecha.SelectedDate;
                    queja.DondeOcurrio = cmbOcurrio.SelectedIndex;
                    queja.OtroMotivo = txtOtroMotivo.Text;
                    queja.Motivos = MotivosSeleccionados(RadListBox1);
                    //-------------------------------------------------------
                    queja.Descripcion = RadEditorResume.Content;
                    queja.Embarque = txtembarque.Text;
                    queja.FechaEmbarque = rdpFechaEmbarque.SelectedDate;
                    queja.Guia_Flete = txtflete.Text;
                    //-------------------------------------------------------
                    queja.NomComTransporte = CmbTransporte.SelectedItem.Text;// txtCompañia.Text;
                    queja.Placas = txtPlacas.Text;
                    queja.NomChofer = txtNomChofer.Text;
                    queja.Fec_Embarque = txtFechaEmbarque.SelectedDate;

                    if (lblIdQueja.Text != "")
                    {
                        queja.Id_Queja = int.Parse(lblIdQueja.Text);
                    }
                    //Fotos del producto
                    foreach (UploadedFile f in UploadFotos.UploadedFiles)
                    {
                        doc = new Documento();
                        doc.Id_Emp = sesion.Id_Emp;
                        doc.Id_Cd = sesion.Id_Cd;
                        doc.Doc_Nombre = f.GetName();
                        doc.Formato = f.ContentType;
                        doc.Tamano = f.ContentLength;
                        doc.TipoDoc = "Foto";
                        doc.Url = Server.MapPath("~/Archivos") + "\\" + doc.Doc_Nombre;
                        byte[] FileInBytes = File.ReadAllBytes(doc.Url);
                        doc.Archivo = FileInBytes;
                        LstDocumentos.Add(doc);
                    }
                    //Factura
                    foreach (UploadedFile f in UploadFactura.UploadedFiles)
                    {
                        doc = new Documento();
                        doc.Id_Emp = sesion.Id_Emp;
                        doc.Id_Cd = sesion.Id_Cd;
                        doc.Doc_Nombre = f.GetName();
                        doc.Formato = f.ContentType;
                        doc.Tamano = f.ContentLength;
                        doc.TipoDoc = "Factura";
                        doc.Url = Server.MapPath("~/Archivos") + "\\" + doc.Doc_Nombre;
                        byte[] FileInBytes = File.ReadAllBytes(doc.Url);
                        doc.Archivo = FileInBytes;
                        LstDocumentos.Add(doc);
                    }
                    //Adicional
                    foreach (UploadedFile f in UploadAdicional.UploadedFiles)
                    {
                        doc = new Documento();
                        doc.Id_Emp = sesion.Id_Emp;
                        doc.Id_Cd = sesion.Id_Cd;
                        doc.Doc_Nombre = f.GetName();
                        doc.Formato = f.ContentType;
                        doc.Tamano = f.ContentLength;
                        doc.TipoDoc = "Adicional";
                        doc.Url = Server.MapPath("~/Archivos") + "\\" + doc.Doc_Nombre;
                        byte[] FileInBytes = File.ReadAllBytes(doc.Url);
                        doc.Archivo = FileInBytes;
                        LstDocumentos.Add(doc);
                    }

                    if (lblEsConsulta.Text == "0" && Page.Request.QueryString["Tipo"] == "2")
                    {
                        foreach (Documento Doc in lstDocumento)
                        {
                            LstDocumentos.Add(Doc);
                        }
                    }

                    string xmlqueja;
                    string xmlProductos;
                    string MensajeRem = "";

                    CN_Queja CN = new CN_Queja();
                    if (lblEsConsulta.Text == "1")
                    {
                        CN.ActualizaQueja(sesion, ref queja, objdtTablaProd, ref LstDocumentos, ref respuesta);
                        #region Convertir a XML
                        //Queja
                        StringBuilder sb = new StringBuilder();
                        TextWriter tw = new StringWriter(sb);
                        serializar.Serialize(tw, queja);
                        tw.Close();
                        xmlqueja = sb.ToString();

                        //Productos
                        System.IO.StringWriter wproductos = new System.IO.StringWriter();
                        objdtTablaProd.WriteXml(wproductos, XmlWriteMode.WriteSchema, false);
                        xmlProductos = wproductos.ToString();

                        #endregion
                        if (respuesta > 0)
                        {
                            wsQuejas.Service1 ws = new wsQuejas.Service1();
                            ws.ActualizaQueja(xmlqueja, xmlProductos, null);
                            EnviarCorreoInvestigador(queja.Id_Queja);
                            mensaje = "se ha actualizado correctamente la queja #" + respuesta;
                            RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow('", mensaje, "')"));

                            return;
                        }
                        else
                        {
                            mensaje = "ha ocurrido un error al actualizar la queja, favor de volver a intentar";
                            RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow('", mensaje, "')"));
                            return;
                        }
                    }
                    else
                    {
                        if (this.lblRes.Text == "Si")
                        {
                            //Se crea Remision del movimiento correspondinte (65, "Producto no Conforme") (78, "Remision de solicitud de devolución a cedis") (79, "Remision de diferencia de embarque cedis") 
                            //RBM Enero 2020
                            //Crea una remision de mov 65 cuando Tipo de queja no es 1 Faltante, 6 Servicio de transporte
                            //Inicio
                            #region Remisiones (65, 78, 79)
                            Remision remision = new Remision();
                            int Id_Rem = 0;
                            int Id_Tm = 0;
                            int verificador = 0;
                            int Id_Ter = 50701011;

                            //1.- Queja por faltante
                            //2.- Queja por sobrante 
                            //3.- Queja por producto cambiado
                            //4.- Queja por producto daÃ±ado durante el transporte
                            //5.- Calidad producto quÃ­mico
                            //6.- Queja de Servicio de Transporte
                            //7.- Calidad producto comercializado
                            //8.- Devolución de producto a Cedis
                            if (queja.Id_TQueja == 1 || queja.Id_TQueja == 3 || queja.Id_TQueja == 4 || queja.Id_TQueja == 5 || queja.Id_TQueja == 7 || queja.Id_TQueja == 8)
                            {
                                if (queja.Id_TQueja == 1 || queja.Id_TQueja == 3)
                                {
                                    Id_Tm = 79; //Remisión de solicitud de devolución a cedis.
                                }
                                if (queja.Id_TQueja == 4 || queja.Id_TQueja == 5 || queja.Id_TQueja == 7)
                                {
                                    Id_Tm = 65; //Remisión de producto no conforme. 
                                }
                                if (queja.Id_TQueja == 8)
                                {
                                    Id_Tm = 78; //Remisión de diferencia de embarque a cedis.
                                }

                                double SubTotalAAA = 0;
                                double Iva = 0;
                                double TotalAAA = 0;
                                remision.Id_Emp = sesion.Id_Emp;
                                remision.Id_Cd = sesion.Id_Cd_Ver;
                                //remision.Id_Rem = tipoDeMovimiento == 2 ? Id_Rem_Actualiza : -1;
                                remision.Rem_ManAut = 0; // Automatico
                                remision.Rem_Tipo = "3"; // 3=remision
                                remision.Rem_Fecha = DateTime.Now;
                                remision.Id_Tm = Id_Tm;
                                remision.Id_Ped = -1;
                                remision.Id_Cte = 100; //Cliente Key Quimica
                                remision.Id_Ter = Id_Ter;
                                //remision.Id_Rik = int.Parse(txtRepresentante.Text);
                                remision.Id_U = sesion.Id_U;
                                remision.Rem_Calle = "JAIME NUNO";
                                remision.Rem_Numero = "#433 OTE";
                                remision.Rem_Cp = "64500";
                                remision.Rem_Colonia = "COL. DEL NORTE MTY. N.L.";
                                remision.Rem_Municipio = "Monterrey";
                                remision.Rem_Estado = "Nuevo LeÃ³n";
                                remision.Rem_Rfc = "KQU6911016X5";
                                remision.Rem_Telefono = "3314200";
                                remision.Rem_Contacto = "MARIO ORTIZ";
                                remision.Rem_Conducto = "";
                                remision.Rem_Guia = "Pte";
                                remision.Rem_FechaEntrega = null;
                                remision.Rem_Nota = "Queja # " + queja.Id_Queja.ToString() + ", " + queja.TipoQueja;
                                remision.Rem_Subtotal = SubTotalAAA;// subtotal;
                                remision.Rem_Iva = Iva;// IVA;
                                remision.Rem_Total = TotalAAA;// total;
                                remision.Rem_Estatus = "I";
                                //remision.Rem_OrdenCompra = txtOrdenCompra.Text;
                                remision.Id_Vap = 0;
                                remision.Rem_CteCuentaNacional = 0;
                                remision.Rem_CteCuentaContNacional = 0;

                                int Cons = 0;
                                int Cantidad = 0;
                                List<RemisionDet> detalles = new List<RemisionDet>();
                                RemisionDet remdetalle = new RemisionDet();
                                foreach (DataRow row in objdtTablaProd.Rows)
                                {

                                    if (!detalles.Exists(x => x.Id_Prd == long.Parse(row["Id_Prd"].ToString())))
                                    {
                                        Cantidad = 0;
                                        foreach (DataRow row2 in objdtTablaProd.Select("Id_Prd='" + row["Id_Prd"] + "'"))
                                        {
                                            {
                                                Cantidad = Cantidad + int.Parse(row2["Cantidad"].ToString());
                                            }
                                        }
                                    }

                                    remdetalle = new RemisionDet();
                                    remdetalle.Id_Emp = sesion.Id_Emp;
                                    remdetalle.Id_Cd = sesion.Id_Cd_Ver;
                                    remdetalle.Id_RemDet = Cons;
                                    remdetalle.Id_Ter = Id_Ter;
                                    remdetalle.Id_Prd = Int64.Parse(row["Id_Prd"].ToString());
                                    remdetalle.Rem_Cant = Cantidad;
                                    remdetalle.Rem_Precio = double.Parse(row["Costo"].ToString());
                                    remdetalle.Ped_Pertenece = false;



                                    if (!detalles.Exists(x => x.Id_Prd == long.Parse(row["Id_Prd"].ToString())))
                                    {
                                        SubTotalAAA = SubTotalAAA + (remdetalle.Rem_Precio * remdetalle.Rem_Cant);
                                        detalles.Add(remdetalle);
                                        Cons = Cons + 1;
                                    }
                                }
                                remision.Rem_Subtotal = SubTotalAAA;
                                remision.Rem_Iva = SubTotalAAA * 0.16;
                                remision.Rem_Total = remision.Rem_Subtotal + remision.Rem_Iva;

                                bool tipoMovimento = false;
                                string mensajes = "";

                                new CN_CapRemision().GuardarRemision(remision, detalles, sesion, ref verificador, false, false, ref Id_Rem, ref tipoMovimento, ref mensajes, "", ConfigurationManager.AppSettings["PermitePrecios0Remision"].ToString());

                                if (mensajes != "")
                                {
                                    Alerta(mensajes + ", favor de cambiar la cantidad o bien eliminar la partida para poder generar la remisión y queja correctamente.");
                                    queja.Id_Rem = verificador;
                                    return;
                                }
                                else
                                {
                                    queja.Id_Rem = verificador;
                                    if (verificador > 0)
                                    {
                                        MensajeRem = " Se generó automáticamente la remisión #" + verificador + ", del tipo de movimiento " + Id_Tm + ".";
                                        //Alerta(MensajeRem);
                                    }
                                }

                            }
                            #endregion
                            //Fin
                        }
                        CN.GuardaQueja(sesion, queja, objdtTablaProd, LstDocumentos, ref respuesta);

                        #region Convertir a XML
                        //Queja
                        StringBuilder sb = new StringBuilder();
                        TextWriter tw = new StringWriter(sb);
                        serializar.Serialize(tw, queja);
                        tw.Close();
                        xmlqueja = sb.ToString();

                        //Productos
                        System.IO.StringWriter wproductos = new System.IO.StringWriter();
                        objdtTablaProd.WriteXml(wproductos, XmlWriteMode.WriteSchema, false);
                        xmlProductos = wproductos.ToString();


                        #endregion
                        if (respuesta > 0)
                        {
                            #region LLamar WebService
                            wsQuejas.Service1 ws = new wsQuejas.Service1();
                            string resp = "0";
                            resp = ws.GuardarQueja(xmlqueja, xmlProductos, null);

                            #endregion
                            mensaje = "Se ha creado la queja #" + resp + ". " + MensajeRem;
                            RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow('", mensaje, "')"));
                            return;
                        }
                        else
                        {
                            //new CN_CapRemision().ModificarRemision_Estatus(remision, sesion.Emp_Cnx, ref verificador);
                            mensaje = "ha ocurrido un error al crear la queja, favor de intentar de nuevo.";
                            RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow('", mensaje, "')"));

                            if (MensajeRem != "")
                            {
                                Alerta(MensajeRem);
                            }
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow('", ex.ToString(), "')"));
            }
        }


        private string FormatMultipleEmailAddresses(string emailAddresses)
        {
            var delimiters = new[] { ',', ';' };

            var addresses = emailAddresses.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            return string.Join(",", addresses);
        }

        private void EnviarCorreoInvestigador(int Id_Queja)
        {
            using (System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage())
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                List<Documento> lstDocumentos = new List<Documento>();
                List<Producto> lstProductos = new List<Producto>();
                CN_Solicitud Negocio = new CN_Solicitud();
                Solicitud solicitud = new Solicitud();
                solicitud.Id_Solicitud = Id_Queja;
                Negocio.ConsultaSolicitud(session, ref solicitud, ref lstProductos, ref lstDocumentos);
                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Emp = sesion.Id_Emp;
                configuracion.Id_Cd = sesion.Id_Cd_Ver;
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                cn_configuracion.Consulta(ref configuracion, session.Emp_Cnx);
                StringBuilder cuerpo_correo;
                cuerpo_correo = new StringBuilder();
                cuerpo_correo.Append("<div align='center'>");
                cuerpo_correo.Append("<table border ='0'> ");
                cuerpo_correo.Append("<tr><td valign='middle' colspan = '2' style='text-decoration: underline'><b><font face= 'Tahoma' size = '4'>Queja modificada por cliente</font></b></td></tr>");
                cuerpo_correo.Append("<tr><td colspan='2'><br><br><br></td></tr>");
                cuerpo_correo.Append("<tr><td colspan='2' align='left'><b><font face= 'Tahoma' size = '2'>La solicitud # </font></b><b><font face= 'Tahoma' size = '2' color='#777777'>" + Id_Queja + ".</font></b></td></tr>");
                cuerpo_correo.Append("<tr><td colspan='2' align='left'><b><font face= 'Tahoma' size = '2'>de la sucursal </font></b><b><font face= 'Tahoma' size = '2' color='#777777'>" + solicitud.Id_Cd + " - " + solicitud.Nom_Sucursal + ".</font></b></td></tr>");
                cuerpo_correo.Append("<tr><td align='left'><b><font face= 'Tahoma' size = '2'>Ha sido modificada por el cliente.</font></b></td></tr>");
                cuerpo_correo.Append("<tr><td align='left'><b><font face= 'Tahoma' size = '2'>Favor de revisar los productos o archivos agregados. </font></b></td></tr>");
                cuerpo_correo.Append("<tr><td colspan='2'><br><br></td></tr>");
                cuerpo_correo.Append("<tr><td align ='center' colspan='2'><b><font face= 'Tahoma' size = '2'></b></td></tr>");
                cuerpo_correo.Append("</table>");
                cuerpo_correo.Append("</div>");
                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                sm.EnableSsl = true;

                message.From = new MailAddress(configuracion.Mail_Remitente);
                string Correos;
                if (Id_Queja != 0 && solicitud.Inv_Correo != "")
                {
                    Correos = solicitud.Inv_Correo + "," + configuracion.Mail_AdministradorQuejas;
                }
                else
                {
                    Correos = configuracion.Mail_AdministradorQuejas;
                }
                // DirecciÃ³n de destino
                message.To.Add(FormatMultipleEmailAddresses(Correos));
                // Asunto 
                message.Subject = "Actualización de queja por cliente.";
                // Mensaje 
                message.IsBodyHtml = true;
                string body = cuerpo_correo.ToString();
                AlternateView vistaHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);

                // Se envÃ­a el mensaje y se informa al usuario
                string mensaje = string.Empty;
                message.AlternateViews.Add(vistaHtml);
                try
                {
                    sm.Send(message);
                    //Alerta("Correo enviado satisfactoriamente");
                }
                catch (Exception ex)
                {
                    sm.Send(message);
                    Alerta("Error al enviar el correo, favor de contactar a su administrador de correos.");
                }
            }
        }

        private string MotivosSeleccionados(RadListBox RadListBox1)
        {
            StringBuilder sb = new StringBuilder();

            IList<RadListBoxItem> collection = RadListBox1.CheckedItems;
            foreach (RadListBoxItem item in collection)
            {
                sb.Append(item.Value + ",");
            }
            return sb.ToString();
        }

        public DataTable ConvertToDataTable<Documento>(IList<Documento> data)
        {
            PropertyDescriptorCollection propiedades = TypeDescriptor.GetProperties(typeof(Documento));
            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in propiedades)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            foreach (Documento item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in propiedades)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }
                table.Rows.Add(row);
            }
            return table;

        }

        private int ValidarCampos()
        {
            try
            {
                //1.- Queja por faltante
                //2.- Queja por sobrante 
                //3.- Queja por producto cambiado
                //4.- Queja por producto daÃ±ado durante el transporte
                //5.- Calidad producto quÃ­mico
                //6.- Queja de Servicio de Transporte
                //7.- Calidad producto comercializado
                //8.- Devolución de producto a Cedis

                if (ValidaDatosGenerales() == 0)
                {
                    return 0;
                }

                if (objdtTablaProd.Rows.Count == 0 && cmbtquejas.SelectedIndex != 6)
                {
                    Alerta("Favor de proporcionar al menos un producto para la queja");
                    return 0;
                }

                if (UploadFotos.UploadedFiles.Count == 0 && lblEsConsulta.Text == "")
                {
                    if (cmbtquejas.SelectedIndex == 3 || cmbtquejas.SelectedIndex == 5 || cmbtquejas.SelectedIndex == 6 || cmbtquejas.SelectedIndex == 7)
                    {
                        Alerta("Para guardar una queja de este tipo, se debe ingresar al menos una foto de evidencia obligatoriamente.");
                        return 0;
                    }
                }

                if (UploadFactura.UploadedFiles.Count == 0 && lblEsConsulta.Text == "")
                {
                    Alerta("Para guardar una queja de este tipo, es obligatorio adjuntar la factura.");
                    return 0;
                }

                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CalcularDiasEmbarque(DateTime fechaini, DateTime fechafin)
        {
            fechaini = fechaini.Date;
            fechafin = fechafin.Date;
            if (fechaini > fechafin)
                Alerta("La fecha Seleccionada debe ser menor a " + fechafin);

            TimeSpan span = fechafin - fechaini;
            int businessDays = span.Days + 1;
            int fullWeekCount = businessDays / 7;
            // find out if there are weekends during the time exceedng the full weeks 
            if (businessDays > fullWeekCount * 7)
            {
                // we are here to find out if there is a 1-day or 2-days weekend 
                // in the time interval remaining after subtracting the complete weeks 
                int firstDayOfWeek = (int)fechaini.DayOfWeek;
                int lastDayOfWeek = (int)fechafin.DayOfWeek;
                if (lastDayOfWeek < firstDayOfWeek)
                    lastDayOfWeek += 7;
                if (firstDayOfWeek <= 6)
                {
                    if (lastDayOfWeek >= 7)// Both Saturday and Sunday are in the remaining time interval 
                        businessDays -= 2;
                    else if (lastDayOfWeek >= 6)// Only Saturday is in the remaining time interval 
                        businessDays -= 1;
                }
                else if (firstDayOfWeek <= 7 && lastDayOfWeek >= 7)// Only Sunday is in the remaining time interval 
                    businessDays -= 1;
            }

            // subtract the weekends during the full weeks in the interval 
            businessDays -= fullWeekCount + fullWeekCount;
            return businessDays;
        }

        private void CargarDocumentos(List<Documento> lstdocumentos)
        {
            try
            {
                foreach (Documento Doc in lstdocumentos)
                {
                    ArrayList ArrayDoc = new ArrayList();
                    ArrayDoc.Add(Doc.Id_Emp);
                    ArrayDoc.Add(Doc.Id_Cd);
                    ArrayDoc.Add(Doc.Id_Doc);
                    ArrayDoc.Add(Doc.Doc_Nombre);
                    ArrayDoc.Add(Doc.Formato);
                    ArrayDoc.Add(Doc.Tamano);
                    ArrayDoc.Add(Doc.TipoDoc);
                    ArrayDoc.Add(Doc.Archivo);
                    objdtTablaDoc.Rows.Add(ArrayDoc.ToArray());
                }
            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }
        }

        private void CargarProductos(List<Producto> lstproducto)
        {
            try
            {
                foreach (Producto prd in lstproducto)
                {
                    ArrayList ArrayProd = new ArrayList();
                    ArrayProd.Add(prd.Id_Emp);
                    ArrayProd.Add(prd.Id_Cd);
                    ArrayProd.Add(prd.Id_Prd);
                    ArrayProd.Add(prd.Descripcion);
                    ArrayProd.Add(prd.Presentacion);
                    ArrayProd.Add(prd.Cantidad);
                    ArrayProd.Add(prd.Prd_UniEmp);
                    ArrayProd.Add(prd.Lote);
                    ArrayProd.Add(prd.Fabricacion.ToShortDateString());
                    ArrayProd.Add(prd.Caducidad.ToShortDateString());
                    ArrayProd.Add(prd.Marca);
                    ArrayProd.Add(prd.Costo);
                    ArrayProd.Add(prd.Num_Fac);
                    ArrayProd.Add(prd.Nom_Prov);
                    ArrayProd.Add(prd.ConservarProd);
                    ArrayProd.Add(prd.Costoestandar);

                    objdtTablaProd.Rows.Add(ArrayProd.ToArray());
                }
            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }
        }

        private Producto GenerarlstProductos(GridEditableItem editedItem, ref Producto producto)
        {
            producto.Id_Prd = (int)(editedItem.FindControl("txtId_Prd") as RadNumericTextBox).Value;
            producto.Descripcion = (editedItem["Prd_Descripcion"].FindControl("txtPrd_Descripcion") as RadTextBox).Text;
            producto.Presentacion = (editedItem["Prd_Presentacion"].FindControl("txtPrd_Presentacion") as RadTextBox).Text;
            return producto;

        }

        private void InicializarTablaProductos()
        {
            try
            {
                objdtListaProd = new DataTable();
                objdtListaProd.Columns.Add("Id_Emp");
                objdtListaProd.Columns.Add("Id_Cd");
                objdtListaProd.Columns.Add("Id_Prd");
                objdtListaProd.Columns.Add("Descripcion");
                objdtListaProd.Columns.Add("Presentacion");
                objdtListaProd.Columns.Add("Cantidad");
                objdtListaProd.Columns.Add("Prd_UniEmp");
                objdtListaProd.Columns.Add("Lote");
                objdtListaProd.Columns.Add("Fabricacion");
                objdtListaProd.Columns.Add("Caducidad");
                objdtListaProd.Columns.Add("Marca");
                objdtListaProd.Columns.Add("Costo");
                objdtListaProd.Columns.Add("Num_Fac");
                objdtListaProd.Columns.Add("Nom_Prov");
                objdtListaProd.Columns.Add("ConservarProd");
                objdtListaProd.Columns.Add("Estandar");
                objdtTablaProd = objdtListaProd;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region "MÃ©todos para obtrener desde objetos los valores para los controles durante la inserciÃ³n/actualizaciÃ³n de un Grid editable"
        protected string ObtenerDescripcion(object prd)
        {
            if (prd is Producto) { return ((Producto)prd).Descripcion; } else { return string.Empty; }
        }
        protected string ObtenerPresentacion(object prd)
        {
            if (prd is Producto) { return ((Producto)prd).Presentacion; } else { return string.Empty; }
        }
        #endregion

        protected void rgProductos_ItemCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void RadUpload1_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            try
            {
                Label fileName = new Label();
                fileName.Text = e.File.FileName;
                NombreArchivo = e.File.GetNameWithoutExtension().ToString() + e.File.GetExtension();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;

                switch (btn.CommandName)
                {
                    case "new":
                        Nuevo();
                        break;
                    case "save":
                        Guardar(objdtTablaProd);
                        break;
                    case "Listado":
                        break;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Nuevo()
        {
            Inicializar(0, Id_Emp, Id_Cd, 2);
        }

        protected void cmbtquejas_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            CargarMotivos(cmbtquejas.SelectedIndex);
            HabilitarCampos(0);

        }

        private void HabilitarCampos(int Tipo)
        {
            rgProductos.MasterTableView.GetColumn("Prd_UniEmp").Display = true;
            rgProductos.MasterTableView.GetColumn("Lote").Display = true;
            rgProductos.MasterTableView.GetColumn("Fabricacion").Display = true;
            rgProductos.MasterTableView.GetColumn("Caducidad").Display = true;
            rgProductos.MasterTableView.GetColumn("Marca").Display = true;
            rgProductos.MasterTableView.GetColumn("Costo").Display = true;
            rgProductos.MasterTableView.GetColumn("Num_Fac").Display = true;
            rgProductos.MasterTableView.GetColumn("Nom_Prov").Display = true;
            rgProductos.MasterTableView.GetColumn("ConservarProd").Display = true;

            //1.- Queja por faltante
            //2.- Queja por sobrante 
            //3.- Queja por producto cambiado
            //4.- Queja por producto daÃ±ado durante el transporte
            //5.- Calidad producto quÃ­mico
            //6.- Queja de Servicio de Transporte
            //7.- Calidad producto comercializado
            //8.- DevoluciÃ³n de producto a Cedis

            if ((cmbtquejas.SelectedIndex == 1) || (cmbtquejas.SelectedIndex == 4))
            {
                rgProductos.MasterTableView.GetColumn("Prd_UniEmp").Display = false;
                rgProductos.MasterTableView.GetColumn("Lote").Display = false;
                rgProductos.MasterTableView.GetColumn("ConservarProd").Display = false;
            }

            if ((cmbtquejas.SelectedIndex == 2) || (cmbtquejas.SelectedIndex == 3))
            {
                rgProductos.MasterTableView.GetColumn("Prd_UniEmp").Display = false;
                rgProductos.MasterTableView.GetColumn("Lote").Display = false;
            }

            if (cmbtquejas.SelectedIndex == 5)
            {
                rgProductos.MasterTableView.GetColumn("Prd_UniEmp").Display = false;
                rgProductos.MasterTableView.GetColumn("ConservarProd").Display = false;

            }

            if (cmbtquejas.SelectedIndex == 7)
            {
                rgProductos.MasterTableView.GetColumn("Prd_UniEmp").Display = false;
                rgProductos.MasterTableView.GetColumn("ConservarProd").Display = false;
                if (Tipo == 0)//una queja nueva 
                {
                    RAM1.ResponseScripts.Add(string.Concat(@"FrmPreguntas('')"));
                }
            }

            if (cmbtquejas.SelectedIndex == 6 || cmbtquejas.SelectedIndex == 8)
            {
                rgProductos.MasterTableView.GetColumn("ConservarProd").Display = false;
            }


            if ((cmbtquejas.SelectedIndex == 1) || (cmbtquejas.SelectedIndex == 2) || (cmbtquejas.SelectedIndex == 3))
            {
                rgProductos.Enabled = true;
                CamposTransporte(2);

                foreach (RadListBoxItem item in RadListBox1.Items)
                {
                    item.Checked = true;
                }

            }
            if (cmbtquejas.SelectedIndex == 5 || cmbtquejas.SelectedIndex == 7 || cmbtquejas.SelectedIndex == 8)
            {
                rgProductos.Enabled = true;
                CamposTransporte(0);
            }
            if (cmbtquejas.SelectedIndex == 6)
            {
                CamposTransporte(1);
                rgProductos.Enabled = false;
            }
            if (cmbtquejas.SelectedIndex == 4)
            {
                CamposTransporte(1);
                rgProductos.Enabled = true;
            }
            if (cmbtquejas.SelectedIndex == 2)
            {
                this.lblRes.Text = "No";
            }
        }

        private void CamposTransporte(int activo)
        {
            if (activo == 1)
            {
                CmbTransporte.Visible = true;
                txtNomChofer.Visible = true;
                txtPlacas.Visible = true;
                txtFechaEmbarque.Visible = true;
                txtFechaEmbarque.SelectedDate = DateTime.Now;
                lblCompañia.Visible = true;
                lblNomChofer.Visible = true;
                lblPlacas.Visible = true;
                lblfechaembarque.Visible = true;

            }
            if (activo == 2)
            {
                CmbTransporte.Visible = true;
                txtNomChofer.Visible = false;
                txtPlacas.Visible = false;
                txtFechaEmbarque.Visible = false;
                lblCompañia.Visible = true;
                lblNomChofer.Visible = false;
                lblPlacas.Visible = false;
                lblfechaembarque.Visible = false;
            }
            else
            {
                if (activo == 0)
                {
                    CmbTransporte.Visible = false;
                    txtNomChofer.Visible = false;
                    txtPlacas.Visible = false;
                    txtFechaEmbarque.Visible = false;
                    lblCompañia.Visible = false;
                    lblNomChofer.Visible = false;
                    lblPlacas.Visible = false;
                    lblfechaembarque.Visible = false;
                }
            }
        }

        protected void func_cerrarventana(string param)
        {
            string funcion = "CloseAndRebind('" + param + "')";
            string script = "<script>" + funcion + "</script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
        }

        protected void gvFotos_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {

        }

        protected void gvFotos_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "DescargarArchivo")
            {
                GridDataItem item = e.Item as GridDataItem;
                Documento Doc = new Documento();
                Doc.Id_Emp = Int32.Parse(item.GetDataKeyValue("Id_Emp").ToString());
                Doc.Id_Cd = Int32.Parse(item.GetDataKeyValue("Id_Cd").ToString());
                Doc.Id_Doc = Int32.Parse(item.GetDataKeyValue("Id_Doc").ToString());

                CN_Documentos Negocio = new CN_Documentos();
                Negocio.ConsultaDocumento(sesion, ref Doc);
                Response.AddHeader("Content-type", Doc.Formato);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Doc.Doc_Nombre);
                Response.BinaryWrite(Doc.Archivo);
                Response.Flush();
                Response.End();

            }
        }

        protected void gvFotos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    gvFotos.DataSource = this.objdtTablaDoc.Select("TipoDoc ='Foto' ");
                }
            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }

        }

        protected void gvFactura_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {

        }

        protected void gvFactura_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "DescargarArchivo")
            {
                Sesion Sesion = new CapaEntidad.Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                GridDataItem item = e.Item as GridDataItem;
                Documento Doc = new Documento();
                Doc.Id_Emp = Int32.Parse(item.GetDataKeyValue("Id_Emp").ToString());
                Doc.Id_Cd = Int32.Parse(item.GetDataKeyValue("Id_Cd").ToString());
                Doc.Id_Doc = Int32.Parse(item.GetDataKeyValue("Id_Doc").ToString());

                CN_Documentos Negocio = new CN_Documentos();
                Negocio.ConsultaDocumento(sesion, ref Doc);

                Response.AddHeader("Content-type", Doc.Formato);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Doc.Doc_Nombre);
                Response.BinaryWrite(Doc.Archivo);
                Response.Flush();
                Response.End();
            }
        }

        protected void gvFactura_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    gvFactura.DataSource = objdtTablaDoc.Select("TipoDoc ='Factura' ");
                }
            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }

        }

        protected void gvAdicional_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {

        }

        protected void gvAdicional_ItemCommand(object sender, GridCommandEventArgs e)
        {

            if (e.CommandName == "DescargarArchivo")
            {
                Sesion Sesion = new CapaEntidad.Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                GridDataItem item = e.Item as GridDataItem;
                Documento Doc = new Documento();
                Doc.Id_Emp = Int32.Parse(item.GetDataKeyValue("Id_Emp").ToString());
                Doc.Id_Cd = Int32.Parse(item.GetDataKeyValue("Id_Cd").ToString());
                Doc.Id_Doc = Int32.Parse(item.GetDataKeyValue("Id_Doc").ToString());

                CN_Documentos Negocio = new CN_Documentos();
                Negocio.ConsultaDocumento(sesion, ref Doc);

                Response.AddHeader("Content-type", Doc.Formato);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Doc.Doc_Nombre);
                Response.BinaryWrite(Doc.Archivo);
                Response.Flush();
                Response.End();
            }
        }

        protected void gvAdicional_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    gvAdicional.DataSource = objdtTablaDoc.Select("TipoDoc = 'Adicional' ");
                }
            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }

        }

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
        private void AlertaCerrar(string mensaje)
        {
            try
            {
                mensaje = mensaje.Replace(Convert.ToChar(10).ToString(), string.Empty);
                mensaje = mensaje.Replace(Convert.ToChar(13).ToString(), string.Empty);
                RAM1.ResponseScripts.Add("CloseWindowA('" + mensaje + "');");
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
                mensaje = mensaje.Replace(Convert.ToChar(10).ToString(), string.Empty);
                mensaje = mensaje.Replace(Convert.ToChar(13).ToString(), string.Empty);
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

        protected void RadAsyncUpload_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            try
            {
                string path = Server.MapPath("~/Archivos/");
                e.File.SaveAs(path + e.File.GetName());
            }
            catch (Exception)
            {
                Alerta("Favor de agregar las imagenes de evidencia nuevamente.");
            }
        }

        protected void RAM_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    case "DestruirSesionQuejas":
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarMotivos(int Id_tQueja)
        {
            try
            {
                Sesion Sesion = new CapaEntidad.Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                DataSet dsMotivos = new DataSet();
                CN_Queja CN = new CN_Queja();
                CN.ConsultaMotivos(Sesion, ref dsMotivos, Id_tQueja);

                RadListBox1.DataSource = dsMotivos.Tables[0];
                RadListBox1.DataTextField = "Descripcion";
                RadListBox1.DataValueField = "Id";
                RadListBox1.DataBind();

                foreach (RadListBoxItem item in RadListBox1.Items)
                {
                    item.Checked = false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void cmbNomSucursal_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cmbNomSucursal.Text == "-- Cliente directo --")
            {
                txtIdcte_directo.Enabled = true;
                txtNomcte_directo.Enabled = true;
                txtIdCd.Text = string.Empty;
                txtIdcte_directo.Focus();
            }
            else
            {
                txtIdcte_directo.Text = String.Empty;
                txtNomcte_directo.Text = String.Empty;
                txtIdcte_directo.Enabled = false;
                txtNomcte_directo.Enabled = false;
                txtIdCd.Text = sesion.Id_Cd_Ver.ToString();
            }
        }


        protected void RadTabStrip1_TabClick(object sender, RadTabStripEventArgs e)
        {
            if (((Telerik.Web.UI.RadTabStrip)(sender)).SelectedTab.Value == "Productos")
            {
                if (ValidaDatosGenerales() == 0)
                {
                    return;
                }
                if (((Telerik.Web.UI.RadTabStrip)(sender)).SelectedTab.Value == "Productos")
                {
                    if (cmbtquejas.SelectedIndex == 0)
                    {
                        Alerta("Debe seleccionar un tipo de queja para seguir");
                        RadTabStrip1.Tabs[0].Selected = true;
                        RadPageViewDGenerales.Selected = true;
                        return;
                    }
                    if (cmbtquejas.SelectedIndex == 6)
                    {
                        Alerta("Este tipo de Queja no requiere captura de productos.");
                        RadTabStrip1.Tabs[2].Selected = true;
                        RadPageViewArchivos.Selected = true;
                        return;
                    }

                    if (txtFecha.IsEmpty)
                    {
                        Alerta("Debe seleccionar una fecha valida antes de continuar.");
                        RadTabStrip1.Tabs[0].Selected = true;
                        RadPageViewDGenerales.Selected = true;
                        return;
                    }
                    if (RadListBox1.CheckedItems.Count == 0)
                    {
                        Alerta("Debe seleccionar al menos un motivo para continuar");
                        RadTabStrip1.Tabs[0].Selected = true;
                        RadPageViewDGenerales.Selected = true;
                        return;
                    }
                }
            }
            if (((Telerik.Web.UI.RadTabStrip)(sender)).SelectedTab.Value == "Adjuntos")
            {
                if (ValidaDatosGenerales() == 0)
                {
                    return;
                }

                if (objdtTablaProd.Rows.Count == 0 && cmbtquejas.SelectedIndex != 6)
                {
                    Alerta("Antes de cargar archivos, debe agregar los productos.");
                    RadTabStrip1.Tabs[1].Selected = true;
                    RadPageViewProductos.Selected = true;
                    return;
                }
            }
        }

        private int ValidaDatosGenerales()
        {
            if (cmbtquejas.SelectedIndex == 0)
            {
                Alerta("El campo tipo de queja es obligatorio, favor de seleccionar un tipo de queja valido");
                RadTabStrip1.Tabs[0].Selected = true;
                RadPageViewDGenerales.Selected = true;
                return 0;
            }

            if (cmbOcurrio.SelectedValue == "-1")
            {
                Alerta("Favor de selecccionar una opcion del listado dondé ocurrió, este campo es obligatorio.");
                RadTabStrip1.Tabs[0].Selected = true;
                RadPageViewDGenerales.Selected = true;
                return 0;
            }

            if (txtembarque.Text == "")
            {
                if (cmbtquejas.SelectedIndex == 1 || cmbtquejas.SelectedIndex == 2 || cmbtquejas.SelectedIndex == 3 || cmbtquejas.SelectedIndex == 4 || cmbtquejas.SelectedIndex == 6)
                {
                    Alerta("Favor de proporcionar el número de Embarque, este campo es obligatorio.");
                    RadTabStrip1.Tabs[0].Selected = true;
                    RadPageViewDGenerales.Selected = true;
                    return 0;
                }
            }
            if (txtflete.Text == "")
            {
                if (cmbtquejas.SelectedIndex == 1 || cmbtquejas.SelectedIndex == 2 || cmbtquejas.SelectedIndex == 3 || cmbtquejas.SelectedIndex == 6)
                {
                    Alerta("Favor de proporcionar la guía de Flete, este campo es obligatorio.");
                    RadTabStrip1.Tabs[0].Selected = true;
                    RadPageViewDGenerales.Selected = true;
                    return 0;
                }
            }

            //RBM Junio 2023 Se agrega nuevo campo obigatorio para las quejas de tipo 1, 2 y 3
            //Inicio
            if (cmbtquejas.SelectedIndex == 1 || cmbtquejas.SelectedIndex == 2 || cmbtquejas.SelectedIndex == 3 || cmbtquejas.SelectedIndex == 4 || cmbtquejas.SelectedIndex == 6)
            {
                if (CmbTransporte.SelectedIndex == 0)
                {
                    Alerta("Favor de proporcionar la Línea de transporte, este campo es obligatorio para este tipo de queja.");
                    RadTabStrip1.Tabs[0].Selected = true;
                    RadPageViewDGenerales.Selected = true;
                    return 0;
                }
            }
            //Fin
            //RBM Junio 2023 Se cambian campos obigatorios para las quejas de tipo 4 -- Producto Dañado en transporte 
            //Inicio
            if (cmbtquejas.SelectedIndex == 4 || cmbtquejas.SelectedIndex == 6)
            {
                if (txtPlacas.Text == "")
                {
                    Alerta("Favor de proporcionar las placas del transporte, este campo es obligatorio para este tipo de queja.");
                    RadTabStrip1.Tabs[0].Selected = true;
                    RadPageViewDGenerales.Selected = true;
                    return 0;
                }
                if (txtFechaEmbarque.SelectedDate > DateTime.Now || txtFechaEmbarque.SelectedDate is null)
                {
                    Alerta("La fecha de embarque no puede estar en vavio o ser mayor a la fecha actual, Favor de proporcionar una fecha menor o igual al dia de hoy.");
                    RadTabStrip1.Tabs[0].Selected = true;
                    RadPageViewDGenerales.Selected = true;
                    return 0;
                }

                if (CmbTransporte.SelectedIndex == -1)
                {
                    Alerta("Seleccione una línea de transporte.");
                    return 0;
                }
            }
            //Fin
            return 1;
        }

        protected void RadListBox1_ItemCheck(object sender, RadListBoxItemEventArgs e)
        {
            if (e.Item.Text == "Otros")
            {
                txtOtroMotivo.Enabled = true;
                txtOtroMotivo.Focus();
            }

        }

        protected void rdpFechaEmbarque_SelectedDateChanged1(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            int DiasCalculados = 0;

            if (cmbtquejas.SelectedIndex == 0)
            {
                Alerta("Debes seleccionar tipo de queja para contiuar.");
                rdpFechaEmbarque.SelectedDate = DateTime.Now;
                return;
            }
            if (cmbtquejas.SelectedIndex <= 4 || cmbtquejas.SelectedIndex == 6)
            {
                DiasCalculados = CalcularDiasEmbarque(Convert.ToDateTime(rdpFechaEmbarque.SelectedDate), DateTime.Now);
                if (DiasCalculados > 3)
                {
                    Alerta("La fecha no puede ser menor a dos dias de la fecha Actual");
                    rdpFechaEmbarque.SelectedDate = DateTime.Now;
                    return;
                }
                if (DateTime.Now < Convert.ToDateTime(rdpFechaEmbarque.SelectedDate))
                {
                    Alerta("La fecha de embarque no puede ser mayor a la fecha Actual");
                    rdpFechaEmbarque.SelectedDate = DateTime.Now;
                    return;
                }
            }

        }

        protected void gvFotos_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                CN_Queja CN = new CN_Queja();
                GridDataItem item = (GridDataItem)e.Item;
                Int32 Id_Doc = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Doc"]);
                //actualizar producto de orden de compra a la lista
                this.EliminarDocumento(Id_Doc);
                CN.EliminarDocumento(sesion, Id_Doc);
                Alerta("El Documento se ha eliminado satisfactoriamente.");
                return;
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, "rgOrdCompra_delete_error");
            }
        }

        private void EliminarDocumento(int Id_Doc)
        {
            DataRow[] Ar_dr;

            Ar_dr = objdtTablaDoc.Select("Id_Doc = '" + Id_Doc + "'");
            if (Ar_dr.Length > 0)
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Ar_dr[0].Delete();
                objdtTablaDoc.AcceptChanges();
            }
        }

        protected void RadMultiPage1_PageViewCreated(object sender, RadMultiPageEventArgs e)
        {

        }

        protected void gvFactura_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            CN_Queja CN = new CN_Queja();
            GridDataItem item = (GridDataItem)e.Item;
            Int32 Id_Doc = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Doc"]);
            this.EliminarDocumento(Id_Doc);
            CN.EliminarDocumento(sesion, Id_Doc);
            Alerta("El Documento se ha eliminado satisfactoriamente.");
            return;
        }

        protected void gvAdicional_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            CN_Queja CN = new CN_Queja();
            GridDataItem item = (GridDataItem)e.Item;
            Int32 Id_Doc = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Doc"]);
            this.EliminarDocumento(Id_Doc);
            CN.EliminarDocumento(sesion, Id_Doc);
            Alerta("El Documento se ha eliminado satisfactoriamente.");
            return;
        }

        protected void rgProductos_RowDrop(object sender, GridDragDropEventArgs e)
        {

        }

        protected void txtLote_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                RadTextBox TxtLote = (sender as RadTextBox);
                Int64 prd = Convert.ToInt64((TxtLote.Parent.Parent.FindControl("txtId_Prd") as RadNumericTextBox).Value);
                string lote = TxtLote.Text;

                DataRow[] Ar_dr;
                //Se valida si existe el producto en la tabla
                Ar_dr = objdtTablaProd.Select("Id_Prd = '" + prd + "'");
                int x = 1;

                if (Ar_dr.Length > 0)
                {
                    //Se validan los registros existentes del producto
                    while (Ar_dr.Length >= x)
                    {
                        string Lote = Ar_dr[x - 1]["Lote"].ToString();
                        if (lote == Lote)
                        {
                            Alerta("Este lote ya existe en otro registro, favor de proporcionar otro lote o bien eliminar la partida.");
                            TxtLote.Text = "";
                            return;
                        }
                        x = x + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];

                RadNumericTextBox Txtcantidad = (sender as RadNumericTextBox);
                int cantidad = Txtcantidad.Value.HasValue ? (int)Txtcantidad.Value : 0;
                Int64 prd = Convert.ToInt64((Txtcantidad.Parent.Parent.FindControl("txtId_Prd") as RadNumericTextBox).Value);
                int disponible = 0;
                int invFinal = 0;
                int asignado = 0;
                int cant_Tabla = 0;
                new CN_CapEntradaSalida().ConsultarDisponible(session, prd, ref disponible, ref invFinal, ref asignado);

                DataRow[] Ar_dr;
                //Se valida si existe el producto en la tabla
                Ar_dr = objdtTablaProd.Select("Id_Prd='" + prd + "'");
                int x = 1;

                if (Ar_dr.Length > 0)
                {
                    //Se valida el tipo de queja que solicita lote 
                    if (cmbtquejas.SelectedValue == "5" || cmbtquejas.SelectedValue == "7" || cmbtquejas.SelectedValue == "8")
                    {
                        //Se validan los registros existentes del producto
                        while (Ar_dr.Length >= x)
                        {
                            cant_Tabla = int.Parse(Ar_dr[x - 1]["Cantidad"].ToString());
                            asignado = asignado + cant_Tabla;
                            disponible = disponible - cant_Tabla;
                            x = x + 1;
                        }
                    }
                    else
                    {
                        Alerta("Este tipo de queja no permite introducir mas de un registro del mismo código.");
                        return;
                    }
                }

                if (this.lblRes.Text == "Si")
                {
                    if (cantidad > disponible)
                    {
                        AlertaFocus("Producto <b>" + prd.ToString() + "</b> inventario disponible insuficiente,</br>inventario final: " + invFinal.ToString() + ",</br>asignado: " + asignado.ToString() + ",</br>disponible: " + (disponible).ToString(), Txtcantidad.ClientID);
                        Txtcantidad.Text = "";
                        return;
                    }
                    else
                    {
                        if (cmbtquejas.SelectedIndex == 1 || cmbtquejas.SelectedIndex == 2 || cmbtquejas.SelectedIndex == 3 || cmbtquejas.SelectedIndex == 4) //Fecha FabricaciÃ³n
                        {
                            (Txtcantidad.Parent.Parent.FindControl("txtLote") as RadTextBox).Focus();
                        }
                        if (cmbtquejas.SelectedIndex == 5 || cmbtquejas.SelectedIndex == 7 || cmbtquejas.SelectedIndex == 8)
                        {
                            (Txtcantidad.Parent.Parent.FindControl("txtLote") as RadTextBox).Focus();
                        }
                    }
                }

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected void rgProductos_PreRender(object sender, EventArgs e)
        {
            if (objdtTablaProd != null)
            {
                if (objdtTablaProd.Rows.Count >= 15)
                {
                    rgProductos.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = false;
                    rgProductos.Rebind();
                }

                if (lblEsConsulta.Text == "1")
                {
                    rgProductos.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = false;
                    rgProductos.Rebind();
                }
            }
        }

        protected void RadListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void RadListBox1_ItemCheck1(object sender, RadListBoxItemEventArgs e)
        {
            if (cmbtquejas.SelectedItem.Value == "8")
            {
                if (RadListBox1.CheckedItems.Count > 1)
                {
                    Alerta("Esta tipo de queja solo permite seleccionar un motivo, favor de seleccionarlo de nuevo.");
                    RadListBox1.ClearChecked();
                    return;
                }
            }
        }
    }
}