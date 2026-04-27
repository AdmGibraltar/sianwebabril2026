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
using CapaModelo_CC.CuentasCoorporativas;
using System.Data.SqlClient;


namespace SIANWEB
{
    public partial class ProPedidoVIOC : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Trim().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Trim().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Trim().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Trim().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Trim().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Trim().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Trim().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Trim().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        private Int32 clickedRowIndex = -1;

        public DataTable dt
        {
            get
            {
                return (DataTable)ViewState["ProdVIOC"];
            }
            set
            {
                ViewState["ProdVIOC"] = value;

            }
        }

        public ArrayList al
        {
            get
            {
                return (ArrayList)Session["Borrados" + Session.SessionID];
            }
            set { Session["Borrados" + Session.SessionID] = value; }
        }
        double iva_cd
        {
            get
            {
                double? _iva_cd = (double?)Session["iva_cd" + Session.SessionID + Page.Request.Url.ToString().Trim().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]];
                return _iva_cd == null ? 0 : (double)_iva_cd;
            }
            set
            {
                Session["iva_cd" + Session.SessionID + Page.Request.Url.ToString().Trim().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value;
            }
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
            if (Session["ProdVIOC"] != null)
            {
                GetListDet();
                DataTable dtTemp = (DataTable)Session["ProdVIOC"];

                rg1.DataSource = dtTemp;
                rg1.DataBind();
            }

            if (Session["AAA"] != null)
            {
                List<PedidoDet> det = (List<PedidoDet>)Session["AAA"];

                grdAAA.DataSource = det;
                grdAAA.DataBind();
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
                    if (!Page.IsPostBack)
                    {
                        _nuevoPedidoSinProgramar = false;
                        Session["Id_PedVIOC" + Session.SessionID] = null;
                        Session["dtPedidoVI" + Session.SessionID] = null;
                        Session["BorradosVIOC" + Session.SessionID] = null;
                        Session["AAA"] = null;

                        //Edsg28052015
                        Session["ProductosNoAcysVIOC"] = null;
                        Session.Add("ProductosNoAcysVIOC", new List<Int64>());




                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirContrasenas", "AbrirContrasenas()", true);
                            return;
                        }
                        if (Session["BorradosVIOC" + Session.SessionID] == null)
                        {
                            Session["BorradosVIOC" + Session.SessionID] = new ArrayList();
                        }
                        Inicializar();
                        al = new ArrayList();
                    }
                }
            }
            catch (Exception ex)
            {
                Alerta(ex.Message.ToString().Trim());
            }
        }

        protected void txtProducto_Load(object sender, EventArgs e)
        {
            producto = sender;
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
                    if (string.IsNullOrEmpty(txtClave.Text))
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
                pedido_vta.ConsultarAAAEspecial(Sesion.Id_Emp, Sesion.Id_Cd_Ver, Convert.ToDouble(txtIdCte.Value.ToString().Trim()), Id_prd, ref verificador, Sesion.Emp_Cnx);

                if (verificador > 0)
                {
                    Alerta("El producto cuenta con precio AAA especial");
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
                    if (int.Parse(cantidad) == 0)
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
                ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtImporte")).Value = Prd_Cantidad * Prd_Precio;

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
                string mensaje;

                GetListDet();
                DataTable dt = (DataTable)Session["ProdVIOC"];

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


                foreach (DataRow dataR in dt.Rows)
                {
                    if (dt.Select("Id_Prd = " + dataR["Id_Prd"].ToString().Trim()).Length > 1)
                    {
                        Alerta("El producto " + dataR["Id_Prd"].ToString().Trim() + " no puede ser captado mas de una vez en el mismo pedido");
                        return;
                    }
                }

                Session["dtPedidoVI" + Session.SessionID] = dt;

                string ConexionCentral = ConfigurationManager.AppSettings["strConnectionCentral"].ToString().Trim();
                ConvenioDet conv;
                ConvenioDet convdet;
                CN_Convenio cn_conv;
                List<string> Productos = new List<string>();

                List<ConvenioDet> lconveniosdet = new List<ConvenioDet>();
                ConvenioDet lconvdet = new ConvenioDet();

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
                        if (Convert.ToInt64(dt.Rows[x]["Id_PrdOld"].ToString()) != Convert.ToInt64(dt.Rows[x]["Id_Prd"].ToString()))
                        {
                            if (Convert.ToInt64(dt.Rows[x]["Id_Prd"]) <= 999999999999)
                            {
                                //double PrecioIngresado = Convert.ToDouble(item2.OwnerTableView.DataKeyValues[item2.ItemIndex]["Prd_Importe"]);
                                //double impore = (item2.FindControl("Prd_Importe") as RadNumericTextBox).Value.HasValue ? (double)(item2.FindControl("Prd_Importe") as RadNumericTextBox).Value : 0;
                                cn_conv.Convenio_ConsultaPrecio(conv, ref convdet, ConexionCentral);



                                int agregar = 0;
                                Producto producto = new Producto();
                                //obtener datos de producto
                                CN_CatProducto clsProducto = new CN_CatProducto();
                                producto.Id_Cte = Convert.ToInt32(Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1"));
                                int productoNuevo = 0;
                                clsProducto.ConsultaProductos(ref producto, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, Convert.ToInt64(dt.Rows[x]["Id_Prd"]), ref productoNuevo, 0);

                                if (Convert.ToDouble(PrecioIngresado) < producto.Prd_AAA)
                                {
                                    if (prodAAA != "")
                                    {
                                        prodAAA = prodAAA + ", " + Convert.ToInt64(dt.Rows[x]["Id_Prd"]).ToString();
                                    }
                                    else
                                    {
                                        prodAAA = Convert.ToInt64(dt.Rows[x]["Id_Prd"]).ToString();
                                    }

                                }
                            }// si prod es menor a 999999999999
                        }
                    }  //Termina ciclo  convenios  
                }  // si tiene artículos 


                if (Request.QueryString["IdAutorizacion"] != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {

                        if (dr["Ped_Asignar"].ToString().Trim() == "")
                            dr["Ped_Asignar"] = 0;

                        if (Convert.ToInt32(dr["Ped_Asignar"]) > 0)
                        {
                            Alerta("Este pedido se encuentra asignado, si desea realizar cambios, favor de desasignar el pedido.");
                            return;
                        }
                        CN_CatProducto cn_catproducto = new CN_CatProducto();
                        Producto pr = new Producto();
                        List<int> actuales = new List<int>();
                        cn_catproducto.ConsultaProducto_Disponible(session.Id_Emp, session.Id_Cd_Ver, dr["Id_Prd"].ToString().Trim(), ref actuales, session.Emp_Cnx);

                        if (Request.QueryString["IdAutorizacion"] != null)
                        {
                            if ((Convert.ToInt32(dr["Prd_Cantidad"]) - (Convert.ToInt32(dr["Ped_Asignar"] == DBNull.Value ? 0 : dr["Ped_Asignar"]))) > actuales[2])
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "inventario", "AbrirVentana_InvIns('" + rdFechaF.Text.ToString().Trim().Replace("/", "") + "', '" + this.TxtPed_ReqAcys.Text + "', '" + txtClave.Text + "')", true);

                                return;
                            }
                        }
                    }

                    Guardar();
                }
                else
                {
                    if (prodAAA == "")
                    {
                        foreach (DataRow dr in dt.Rows)
                        {

                            if (dr["Ped_Asignar"].ToString().Trim() == "")
                                dr["Ped_Asignar"] = 0;

                            if (Convert.ToInt32(dr["Ped_Asignar"]) > 0)
                            {
                                Alerta("Este pedido se encuentra asignado, si desea realizar cambios, favor de desasignar el pedido.");
                                return;
                            }
                            CN_CatProducto cn_catproducto = new CN_CatProducto();
                            Producto pr = new Producto();
                            List<int> actuales = new List<int>();
                            cn_catproducto.ConsultaProducto_Disponible(session.Id_Emp, session.Id_Cd_Ver, dr["Id_Prd"].ToString().Trim(), ref actuales, session.Emp_Cnx);

                            if ((Convert.ToInt32(dr["Prd_Cantidad"]) - (Convert.ToInt32(dr["Ped_Asignar"] == DBNull.Value ? 0 : dr["Ped_Asignar"]))) > actuales[2])
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "inventario", "AbrirVentana_InvIns('" + rdFechaF.Text.ToString().Trim().Replace("/", "") + "', '" + this.TxtPed_ReqAcys.Text + "', '" + txtClave.Text + "')", true);
                                return;
                            }
                        }

                        Guardar();
                    }
                    else
                    {
                        List<PedidoDet> det = new List<PedidoDet>();
                        PedidoDet detalle;
                        for (int x = 0; x < dt.Rows.Count; x++)
                        {
                            if (dt.Rows[x]["Id_Prd"].ToString() != dt.Rows[x]["Id_PrdOld"].ToString())
                            {
                                detalle = new PedidoDet();
                                detalle.Id_PrdOri = Convert.ToInt64(dt.Rows[x]["Id_PrdOld"].ToString());
                                detalle.Id_Prd = Convert.ToInt64(dt.Rows[x]["Id_Prd"].ToString());
                                detalle.Importe = Convert.ToDouble(dt.Rows[x]["Prd_Precio"].ToString());
                                detalle.AAA = Convert.ToDouble(dt.Rows[x]["Prd_PrecioLista"].ToString());
                                det.Add(detalle);
                            }
                        }
                        Session["AAA"] = det;
                        grdAAA.DataSource = det;
                        grdAAA.DataBind();
                        RadGrid1.DataSource = GetListDetRevPrecios();
                        RadGrid1.DataBind();
                        mensajeSoliitud("El precio de venta de los siguiente productos es menor al precio AAA: <Br> ");
                        updAAA.Update();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataTable GetListDetRevPrecios()
        {
            DataTable dt = (DataTable)Session["ProdVIOC"];

            DataTable dtTemplistprod = new DataTable();
            dtTemplistprod.Columns.Add("Id_Prd", System.Type.GetType("System.Int64"));
            dtTemplistprod.Columns.Add("Prd_Descripcion", System.Type.GetType("System.String"));
            dtTemplistprod.Columns.Add("Prd_Cantidad", System.Type.GetType("System.Int32"));
            dtTemplistprod.Columns.Add("Prd_Asignado", System.Type.GetType("System.Int32"));
            dtTemplistprod.Columns.Add("Prd_InvFinal", System.Type.GetType("System.Int32"));
            dtTemplistprod.Columns.Add("Prd_Disponible", System.Type.GetType("System.Int32"));

            CN_CatProducto cn_catproducto = new CN_CatProducto();
            Producto pr = new Producto();
            List<int> actuales;

            foreach (DataRow dataR in dt.Rows)
            {

                actuales = new List<int>();
                cn_catproducto.ConsultaProducto_Disponible(session.Id_Emp, session.Id_Cd_Ver, dataR["Id_Prd"].ToString(), ref actuales, session.Emp_Cnx);
                if (Convert.ToInt32(dataR["Prd_Cantidad"]) > Convert.ToInt32(actuales[2]))
                {
                    dtTemplistprod.Rows.Add(new object[] { dataR["Id_Prd"], dataR["Prd_Descripcion"], dataR["Prd_Cantidad"], actuales[1], actuales[0], actuales[2] });
                }
            }
            return dtTemplistprod;
        }
        private void Guardar()
        {
            try
            {
                GetListDet();
                DataTable dt = (DataTable)Session["ProdVIOC"];

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
                pedido.Ped_Importe = txtSubtotal.Value != "" ? Convert.ToDouble(txtSubtotal.Value.ToString().Trim()) : 0;
                pedido.Ped_Subtotal = txtSubtotal.Value != "" ? Convert.ToDouble(txtSubtotal.Value.ToString().Trim()) : 0;
                pedido.Ped_Iva = txtIva.Value != "" ? Convert.ToDouble(txtIva.Value.ToString().Trim()) : 0;
                pedido.Ped_Total = txtTotal.Value != "" ? Convert.ToDouble(txtTotal.Value.ToString().Trim()) : 0;

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
                pedido.Ped_Tipo = txtClave.Value == null || txtClave.Value == "" ? 4 : 3;

                // Edsg Proyecto Internet
                if (rdModInternet.Checked) pedido.Ped_Tipo = 5;
                pedido.FechaFacAcys = Convert.ToDateTime(rdFechaF.Value);
                pedido.PedAcys = TxtPed_PedAcys.Text.Trim();
                pedido.ReqAcys = TxtPed_ReqAcys.Text.Trim();
                pedido.OcAcys = TxtPed_OCAcys.Text.Trim();
                pedido.IdOC = TxtPed_ReqAcys.Text.Trim();
                pedido.UsoCFDI = ddUsoCfdi.Value?.ToString();


                CN_CapPedidoVtaInst clsCapPedido = new CN_CapPedidoVtaInst();

                //JFCV convenios, validar el precio minimo y maximo 
                #region inicio validar precios convenio
                // *ERGC: se desahabilito ya que lo precios ya esta pactado con el cliente a nivel cuenta naciional

                //string ConexionCentral = ConfigurationManager.AppSettings["strConnectionCentral"].ToString().Trim();
                //ConvenioDet conv;
                //ConvenioDet convdet;
                //CN_Convenio cn_conv;
                //List<string> Productos = new List<string>();

                //List<ConvenioDet> lconveniosdet = new List<ConvenioDet>();
                //ConvenioDet lconvdet = new ConvenioDet(); 

                //string prodAAA = "";
                //if (dt.Rows.Count > 0)
                //{
                //    for (int x = 0; x < dt.Rows.Count; x++)
                //    {

                //        conv = new ConvenioDet();
                //        convdet = new ConvenioDet();
                //        cn_conv = new CN_Convenio();
                //        conv.Id_Emp = session.Id_Emp;
                //        conv.Id_Cd = session.Id_Cd_Ver;
                //        conv.Id_Cte = Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1");
                //        conv.Id_Prd = Convert.ToInt64(dt.Rows[x]["Id_Prd"]);
                //        double PrecioIngresado = Convert.ToDouble(dt.Rows[x]["Prd_Precio"]);
                //        //double impore =  dt.Rows[x]["Prd_Precio"].ToString().Trim()!= DBNull.Value  ? (double)(dt.Rows[x]["Prd_Precio"]) : 0;

                //        if (Convert.ToInt64(dt.Rows[x]["Id_PrdOld"].ToString()) == Convert.ToInt64(dt.Rows[x]["Id_Prd"].ToString()))
                //        { 
                //            if (Convert.ToInt64(dt.Rows[x]["Id_Prd"]) <= 999999999999)
                //            {
                //                //double PrecioIngresado = Convert.ToDouble(item2.OwnerTableView.DataKeyValues[item2.ItemIndex]["Prd_Importe"]);
                //                //double impore = (item2.FindControl("Prd_Importe") as RadNumericTextBox).Value.HasValue ? (double)(item2.FindControl("Prd_Importe") as RadNumericTextBox).Value : 0;
                //                cn_conv.Convenio_ConsultaPrecio(conv, ref convdet, ConexionCentral);



                //                int agregar = 0;

                //                if (convdet != null && convdet.PCD_PrecioAAAEsp > 0)
                //                {
                //                    if (convdet.PCD_PrecioVtaMin != 0 || convdet.PCD_PrecioVtaMax != 0)
                //                    {
                //                        #region pvtamin y pvtamax  dif de cero 
                //                        if (Math.Round(PrecioIngresado, 3) < convdet.PCD_PrecioVtaMin)
                //                        {
                //                            if (Convert.ToDouble(PrecioIngresado) < convdet.PCD_PrecioAAAEsp)
                //                            {
                //                                if (prodAAA != "")
                //                                {
                //                                    prodAAA = prodAAA + ", " + Convert.ToInt64(dt.Rows[x]["Id_Prd"]).ToString();
                //                                }
                //                                else
                //                                {
                //                                    prodAAA = Convert.ToInt64(dt.Rows[x]["Id_Prd"]).ToString();
                //                                }
                //                            }
                //                        }
                //                        else if (convdet.PCD_PrecioVtaMin == 0 && convdet.PCD_PrecioVtaMax != 0)
                //                        {
                //                            // vta minima es igual a cero y vta max dif 0 
                //                            // si precio es mayor a vta max manda aviso 
                //                            if (Math.Round(PrecioIngresado, 3) > convdet.PCD_PrecioVtaMax)
                //                            {
                //                                agregar = 1;
                //                            }
                //                        }
                //                        else if (Math.Round(PrecioIngresado, 3) > convdet.PCD_PrecioVtaMax)
                //                        {
                //                            agregar = 1;
                //                        }

                //                        if (agregar == 1)
                //                        {
                //                            Productos.Add(convdet.Id_Prd.ToString().Trim());

                //                            lconvdet = new ConvenioDet();
                //                            lconvdet.PC_Nombre = convdet.PC_Nombre;
                //                            lconvdet.Id_PC = convdet.Id_PC;
                //                            lconvdet.Id_Prd = convdet.Id_Prd;
                //                            lconvdet.PCD_PrecioVtaMax = convdet.PCD_PrecioVtaMax;
                //                            lconvdet.PCD_PrecioVtaMin = convdet.PCD_PrecioVtaMin;
                //                            lconvdet.PCD_PrecioVentaConvenio = PrecioIngresado;
                //                            lconveniosdet.Add(lconvdet);
                //                        }
                //                        #endregion
                //                    }  // si pvtamin y pvtamax son cero
                //                }
                //                else
                //                {
                //                    Producto producto = new Producto();
                //                    //obtener datos de producto
                //                    CN_CatProducto clsProducto = new CN_CatProducto();
                //                    producto.Id_Cte = Convert.ToInt32(Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1"));
                //                    int productoNuevo = 0;
                //                    clsProducto.ConsultaProductos(ref producto, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, Convert.ToInt64(dt.Rows[x]["Id_Prd"]), ref productoNuevo, 0);

                //                    if (Convert.ToDouble(PrecioIngresado) < producto.Prd_AAA)
                //                    {
                //                        if (prodAAA != "")
                //                        {
                //                            prodAAA = prodAAA + ", " + Convert.ToInt64(dt.Rows[x]["Id_Prd"]).ToString();
                //                        }
                //                        else
                //                        {
                //                            prodAAA = Convert.ToInt64(dt.Rows[x]["Id_Prd"]).ToString();
                //                        }

                //                    }
                //                }
                //            }// si prod es menor a 999999999999
                //        } 
                //    }  //Termina ciclo  convenios  
                //}  // si tiene artículos  


                //if (prodAAA != "")
                //{
                //    Alerta("El precio de venta de los siguiente produtos no puede ser menor al Precio AAA del producto: " + prodAAA);
                //    return;
                //}

                //Session["ProdsConv" + Session.SessionID] = null;
                //Session["Id_FacPrec" + Session.SessionID] = null;
                //Session["lConvPrecios" + Session.SessionID] = null;

                //if (Productos.Count > 0 && lconveniosdet.Count > 0)
                //{
                //    Session["ProdsConv" + Session.SessionID] = Productos;
                //    Session["lConvPrecios" + Session.SessionID] = lconveniosdet;
                //    Session["Id_FacPrec" + Session.SessionID] = txtFolio.Value.ToString().Trim();
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirVentana_AlertaPrecios", "AbrirVentana_AlertaPrecios()", true);

                //    return;
                //}



                #endregion inicio validar precios conevnio 


                if (HF_ID.Value == "" || Request.QueryString["IdAutorizacion"] != null)
                {

                    if (!_PermisoGuardar)
                    {
                        Alerta("No tiene permisos para grabar");

                        return;
                    }

                    Int64 _prd = 0;
                    foreach (DataRow rows in dt.AsEnumerable())
                    {
                        _prd = Convert.ToInt64((rows[1]));
                        PedidoVtaInst pvi = new PedidoVtaInst();
                        pvi.Id_Emp = session.Id_Emp;
                        pvi.Id_Cd = session.Id_Cd_Ver;
                        pvi.Id_Cte = Convert.ToInt32(txtIdCte.Value);
                        pvi.Id_Ter = Convert.ToInt32(txtIdTer.Value);
                        pvi.Id_Acs = Convert.ToInt32(txtClave.Value);
                        pvi.Acs_Semana = Convert.ToInt32(txtSemana.Value == "" ? "0" : txtSemana.Value.ToString().Trim());
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

                            clsCapPedido.InsertarOrdenCompraCentral(session.Id_U, session.Id_Cd, pedido, dt, session.Emp_Cnx, ref verificador, Id_TG, acys.Id_AcsVersion);
                        }
                        else
                        {
                            clsCapPedido.InsertarOrdenCompraCentral(session.Id_U, session.Id_Cd, pedido, dt, session.Emp_Cnx, ref verificador, Id_TG, null);
                        }
                    }
                    else
                    {
                        clsCapPedido.InsertarOrdenCompraCentral(session.Id_U, session.Id_Cd, pedido, dt, session.Emp_Cnx, ref verificador, Id_TG, null);


                    }

                    if (verificador >= 1)
                    {
                        Session["Id_Ped" + Session.SessionID] = verificador;
                        mensajeExito("Se realizo la captación del pedido con el folio: " + verificador);
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
                        Alerta("No tiene Permisos para Actualizar el Pedido");
                        return;
                    }


                    pedido.Id_Ped = Convert.ToInt32(HF_ID.Value);
                    int captado = 0;
                    captado = Convert.ToInt32(txtFolio.Value);

                    clsCapPedido.ModificarCN(session.Id_U, session.Id_Cd, pedido, dt, session.Emp_Cnx, captado, ref verificador, al, Id_TG);

                    if (verificador >= 1)
                    {
                        Session["Id_Ped" + Session.SessionID] = verificador;
                        mensajeExito("Se actualizo el Pedido: " + verificador);

                        btncaptacion_Guardar.Enabled = false;
                    }
                    else
                    {
                        Alerta("Ocurrió un error al Intentar Actualizar el Pedido");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GuardarSolicitud()
        {
            try
            {
                GetListDet();
                DataTable dt = (DataTable)Session["ProdVIOC"];

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
                pedido.Ped_Importe = txtSubtotal.Value != "" ? Convert.ToDouble(txtSubtotal.Value.ToString().Trim()) : 0;
                pedido.Ped_Subtotal = txtSubtotal.Value != "" ? Convert.ToDouble(txtSubtotal.Value.ToString().Trim()) : 0;
                pedido.Ped_Iva = txtIva.Value != "" ? Convert.ToDouble(txtIva.Value.ToString().Trim()) : 0;
                pedido.Ped_Total = txtTotal.Value != "" ? Convert.ToDouble(txtTotal.Value.ToString().Trim()) : 0;

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
                pedido.Ped_Tipo = txtClave.Value == null || txtClave.Value == "" ? 4 : 3;

                // Edsg Proyecto Internet
                if (rdModInternet.Checked) pedido.Ped_Tipo = 5;
                pedido.FechaFacAcys = Convert.ToDateTime(rdFechaF.Value);
                pedido.PedAcys = TxtPed_PedAcys.Text.Trim();
                pedido.ReqAcys = TxtPed_ReqAcys.Text.Trim();
                pedido.OcAcys = TxtPed_OCAcys.Text.Trim();
                pedido.IdOC = TxtPed_ReqAcys.Text.Trim();

                CN_CapPedidoVtaInst clsCapPedido = new CN_CapPedidoVtaInst();

                if (HF_ID.Value == "" || Request.QueryString["IdAutorizacion"] != null)
                {
                    if (!_PermisoGuardar)
                    {
                        Alerta("No tiene permisos para grabar");
                        return;
                    }

                    Int64 _prd = 0;

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

                            clsCapPedido.InsertarOrdenCompraCentralSolicitud(session.Id_U, session.Id_Cd, pedido, dt, session.Emp_Cnx, ref verificador, Id_TG, acys.Id_AcsVersion);
                        }
                        else
                        {
                            clsCapPedido.InsertarOrdenCompraCentralSolicitud(session.Id_U, session.Id_Cd, pedido, dt, session.Emp_Cnx, ref verificador, Id_TG, null);
                        }
                    }
                    else
                    {
                        clsCapPedido.InsertarOrdenCompraCentralSolicitud(session.Id_U, session.Id_Cd, pedido, dt, session.Emp_Cnx, ref verificador, Id_TG, null);


                    }

                    if (verificador >= 1)
                    {
                        Session["Id_Ped" + Session.SessionID] = verificador;

                        string Id_Cte = txtIdCte.Value;
                        int Id_Rik = session.Id_U;
                        string mensaje = "";

                        EnviarCorreo(Id_Cte, Id_Rik, session.Emp_Cnx, ref mensaje);
                        if (mensaje == "")
                        {
                            mensajeExito("Se realizo la Solicitud con el folio: " + verificador);
                        }
                        else
                        {
                            mensajeExito(mensaje);
                        }
                    }
                    else
                    {
                        Alerta("Ocurrió un error al intentar guardar la solicitud de autorización");
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
                    int captado = 0;
                    if (Request.QueryString["IdVI"] != null)
                        captado = Convert.ToInt32(txtFolio.Text);

                    clsCapPedido.Modificar(pedido, dt, session.Emp_Cnx, captado, ref verificador, al);

                    if (verificador >= 1)
                    {
                        Session["Id_Ped" + Session.SessionID] = verificador;
                        mensajeExito("Se realizo la captación del pedido con el folio: " + verificador);
                    }
                    else
                    {
                        Alerta("Ocurrió un error al intentar guardar el pedido");

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        private void Inicializar()
        {
            CN_CatCentroDistribucion cd = new CN_CatCentroDistribucion();
            CentroDistribucion centroDistribucion = new CentroDistribucion();
            double iva = iva_cd;
            cd.ConsultarIva(session.Id_Emp, session.Id_Cd_Ver, ref iva, session.Emp_Cnx);
            iva_cd = iva;
            UltimosPeriodos();
            LlenarComboUsoCfdi();
            if (Request.QueryString["idP"] != null)
            {
                txtFolio.Text = Request.QueryString["idP"].ToString().Trim();
                CargarPedido();
            }
            else
            {
                if (Request.QueryString["IdAutorizacion"] != null)
                {
                    txtFolio.Text = Request.QueryString["IdAutorizacion"].ToString().Trim();
                    CargarPedidoAutorizado();
                }
                else if (Request.QueryString["IdOC"] != null)
                {
                    CN_CatCNac_OrdenCompra cn = new CN_CatCNac_OrdenCompra();
                    int idOC = Int32.Parse(Request.QueryString["IdOC"]);
                    var res = cn.ConsultaPedidoOC_Captacion(idOC);

                    txtIdCte.Value = res.Id_Cte.ToString().Trim();
                    txtFolio.Text = MaximoId();
                    TxtPed_ReqAcys.Text = res.NumPedido;
                    TxtPed_ReqAcys.Enabled = false;
                    txtNotas.Text = res.Observaciones;

                    CargarCliente();
                    CN_CapPedido cnOC = new CN_CapPedido();
                    List<OrdenCompra_Detalle> lista = new List<OrdenCompra_Detalle>();
                    cnOC.ConsultarPedidoOrden_Detalle(res.NumPedido, res.Id_Cte, res.Id_Cd, session.Emp_Cnx, ref lista);
                    dt = null;

                    GetListDet();
                    DataTable dtTemp = dt;

                    foreach (OrdenCompra_Detalle detalle in lista)
                    {
                        dtTemp.Rows.Add(new object[] {
                     detalle.Id_PrdOld ,
                    detalle.Id_Prd ,
                    detalle.Prd_Descripcion ,
                    detalle.Prd_Presentacion ,
                    detalle.Prd_Unidad  ,
                    detalle.mes1 ,
                    detalle.mes2 ,
                    detalle.mes3 ,
                    detalle.Prd_Cantidad  ,
                    detalle.Prd_Precio ,
                    detalle.Acs_PrecioAcys ,
                    detalle.Prd_Importe,
                    detalle.Acs_Documento,
                    detalle.Acs_Fecha,
                    detalle.Acs_Mod,
                    detalle.Acs_Dia ,
                    detalle.Acs_DiaStr ,
                    detalle.Acs_Frecuencia,
                    detalle.Prd_RemFact,
                    detalle.Ped_Asignar,
                    detalle.Id_TG,
                    detalle.Id_Acs,
                    detalle.ACS_ReqOC ,
                    detalle.Prd_PrecioLista ,
                    detalle.Tipo_producto,
                    detalle.Prd_Cantidadold ,
                    detalle.Prd_Activo
                });
                    }
                    //cn_capPedidoInternet.ConsultarPedido_Detalle(session.Emp_Cnx, num_pedido, ref dtTemp);
                    dt = dtTemp;


                    CN_CatProducto cn_catproducto = new CN_CatProducto();
                    Producto pr;
                    foreach (DataRow dr in dt.Rows)
                    {
                        pr = new Producto();
                        pr.Id_Emp = session.Id_Emp;
                        pr.Id_Cd = session.Id_Cd_Ver;
                        pr.Id_Prd = Convert.ToInt64(dr["Id_prd"]);

                        cn_catproducto.ConsultarVentas(ref pr, Convert.ToInt32(res.Id_Cte.ToString().Trim()), session.Emp_Cnx);

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
                            if (Convert.ToDouble(dt.Rows[x]["Prd_PrecioLista"]) == 0)
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


                                    Producto producto = new Producto();
                                    //obtener datos de producto
                                    CN_CatProducto clsProducto = new CN_CatProducto();
                                    producto.Id_Cte = Convert.ToInt32(Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1"));
                                    int productoNuevo = 0;
                                    clsProducto.ConsultaProductos(ref producto, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, Convert.ToInt64(dt.Rows[x]["Id_Prd"]), ref productoNuevo, 0);

                                    if (0 < producto.Prd_AAA)
                                    {
                                        dt.Rows[x]["Prd_PrecioLista"] = producto.Prd_AAA;
                                    }

                                }
                            }
                        }
                    }

                    Session["ProdVIOC"] = dtTemp;
                    rg1.DataSource = dtTemp;
                    rg1.DataBind();
                    dt = dtTemp;
                    Consultar_IVA_Cliente();
                    CalcularTotales();
                }
                else
                {
                    txtFolio.Text = MaximoId();

                    if (Request.QueryString["id"] != "-1")
                    {
                        txtClave.Text = Request.QueryString["id"];
                        CargarAcuerdo();
                        if (string.IsNullOrEmpty(txtClave.Text))
                            productoNuevo = 1;
                    }
                }
            }

            int intIdCte = Convert.ToInt32(txtIdCte.Value.ToString().Trim());
            ConsultarClienteFechaEntrega(session.Emp_Cnx, intIdCte, session.Id_Cd);

            _PermisoGuardar = Convert.ToBoolean(Request.QueryString["PermisoGuardar"]);
            _PermisoModificar = Convert.ToBoolean(Request.QueryString["PermisoModificar"]);
            _PermisoEliminar = Convert.ToBoolean(Request.QueryString["PermisoEliminar"]);
            _PermisoImprimir = Convert.ToBoolean(Request.QueryString["PermisoImprimir"]);
            ValidarPermisos();

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
        private void UltimosPeriodos()
        {
            try
            {
                Funciones funciones = new Funciones();


            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                    cn_capPedidoVI.Consultar(ref pedido, session.Emp_Cnx, ref verificador, ref cc);

                    if (verificador == 1)
                    {

                        txtClienteNom.Text = pedido.Cte_Nom;
                        txtIdCte.Value = pedido.Id_Cte.ToString().Trim();
                        txtRikNom.Value = pedido.Rik_Nombre;
                        txtIdRik.Value = pedido.Id_Rik.ToString().Trim();

                        txtTerritorioNom.Text = pedido.Ter_Nombre;
                        txtIdTer.Value = pedido.Id_Ter.ToString().Trim();

                        //txtContactoNom.Text = pedido.Acs_Contacto;
                        //txtContactoTel.Text = pedido.Acs_Telefono;
                        //txtContactoMail.Text = pedido.Acs_email;
                        //txtContactoPuesto.Text = pedido.Acs_Puesto;

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

                        TxtVersion.Text = pedido.Id_AcsVersion.ToString().Trim();
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
                        else if (pedido.Acs_Modalidad == "C")
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
                    //imgBuscar.Visible = true;

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


                            Producto producto = new Producto();
                            //obtener datos de producto
                            CN_CatProducto clsProducto = new CN_CatProducto();
                            producto.Id_Cte = Convert.ToInt32(Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1"));
                            int productoNuevo = 0;
                            clsProducto.ConsultaProductos(ref producto, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, Convert.ToInt64(dt.Rows[x]["Id_Prd"]), ref productoNuevo, 0);

                            if (0 < producto.Prd_AAA)
                            {
                                dt.Rows[x]["Prd_PrecioLista"] = producto.Prd_AAA;
                            }
                        }

                    }
                }





                string _idTGStr = Request.QueryString["Id_TG"];
                int _idTG = 0;
                if (_idTGStr != null)
                {
                    if (int.TryParse(_idTGStr, out _idTG))
                    {

                    }
                }

                Session["ProdVIOC"] = dt;
                rg1.DataSource = dt;
                rg1.DataBind();

                foreach (DataRow i in dt.Rows)
                {
                    imp += Convert.ToDouble(i["Prd_Importe"]);

                }
                txtSubtotal.Value = imp.ToString().Trim();


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


                txtIva.Value = (Convert.ToDouble(HD_IVAfacturacion.Value.ToString()) * iva_cd / 100).ToString("F2").Trim();

                txtTotal.Value = txtSubtotal.Value + txtIva.Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool _nuevoPedidoSinProgramar
        {
            get
            {
                return (bool)ViewState["_nuevoPedidoSinProgramar"];
            }
            set
            {
                ViewState["_nuevoPedidoSinProgramar"] = value;


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


        private void CargarPedidoInternet(int num_pedido)
        {
            try
            {
                //  HF_ID.Value = txtFolio.Text;
                Sesion sesion = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                Pedido_Internet pedido = new Pedido_Internet();
                ClienteDirEntrega dirEntrega = new ClienteDirEntrega();

                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;
                pedido.Num_Pedido = num_pedido;

                CN_CapPedido_Internet cn_capPedidoInternet = new CN_CapPedido_Internet();
                cn_capPedidoInternet.ConsultarPedido_Datos(session.Emp_Cnx, ref pedido, ref dirEntrega, 0, 0);

                txtClienteNom.Text = pedido.Nom_Cliente;
                txtIdCte.Value = pedido.Id_Cte.ToString().Trim();
                TxtPed_ReqAcys.Text = pedido.Num_Pedido.ToString().Trim();

                CargarTerritorios();

                //txtRikNom.Text = pedido.Rik_Nombre;
                //txtIdRik.Value = pedido.Id_Rik;
                //txtTerritorioNom.Text = pedido.Ter_Nombre;
                //txtIdTer.Value = pedido.Id_Ter;

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


                txtRHoraam1.Value = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + dirEntrega.Cte_HoraAm1).ToString().Trim(); ;
                txtRHoraam2.Value = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + dirEntrega.Cte_HoraAm2).ToString().Trim(); ;
                txtRHorapm1.Value = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + dirEntrega.Cte_HoraPm1).ToString().Trim(); ;
                txtRHorapm2.Value = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + dirEntrega.Cte_HoraPm2).ToString().Trim(); ;

                rdModInternet.Checked = true;



                txtSubtotal.Value = pedido.Subtotal.ToString().Trim();
                txtIva.Value = pedido.IVA.ToString().Trim();
                txtTotal.Value = pedido.Total.ToString().Trim();

                //Edsg Desactiva los campos
                TxtPed_ReqAcys.Enabled = false;
                txtNotas.Enabled = false;
                ImgBuscarDireccionEntrega.Enabled = false;



                GetListDet();

                DataTable dtTemp = dt;
                cn_capPedidoInternet.ConsultarPedido_Detalle(session.Emp_Cnx, num_pedido, 0, ref dtTemp);
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
                Session["ProdVIOC"] = dt;
                rg1.DataSource = dt;
                rg1.DataBind();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarPedidoAutorizado()
        {
            try
            {
                HF_ID.Value = txtFolio.Text;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                session = (Sesion)Session["Sesion" + Session.SessionID];
                Pedido pedido = new Pedido();
                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;
                pedido.Id_Ped = Convert.ToInt32(txtFolio.Text);

                CN_CapPedido cn_capPedido = new CN_CapPedido();
                cn_capPedido.ConsultaPedidoAutorizacion(ref pedido, session.Emp_Cnx);

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

                //CheckBox1.Checked = pedido.Ped_ReqOrden;
                //txtReqOrd.Text = pedido.Ped_OrdenCompra;


                txtFecha.Value = pedido.Ped_AcysAnio.ToString().Trim();
                txtSemana.Value = pedido.Ped_AcysSemana.ToString().Trim();
                txtClave.Value = pedido.Id_Acs;



                txtSubtotal.Value = pedido.Ped_Subtotal.ToString().Trim();
                txtIva.Value = pedido.Ped_Iva.ToString().Trim();
                txtTotal.Value = pedido.Ped_Total.ToString().Trim();

                rdFechaE.Value = pedido.Ped_FechaEntrega;

                if (pedido.FechaFacAcys.Year != 1)
                {
                    rdFechaF.Value = pedido.FechaFacAcys;
                }
                if (rdFechaF.Value == null)
                {
                    rdFechaF.Value = DateTime.Now;
                }
                TxtPed_PedAcys.Text = pedido.Pedido_del;
                TxtPed_ReqAcys.Text = pedido.Requisicion;
                TxtPed_OCAcys.Text = pedido.Ped_OrdenCompra;


                pedido.Ped_Tipo = 3;
                pedido.Ped_Captacion = true;

                CargarInfoAcys(pedido.Id_Acs, pedido.Ped_AcysSemana, pedido.Ped_AcysAnio);

                GetListDet();
                DataTable dtTemp = dt;
                cn_capPedido.ConsultaPedidoDetAutorizacion(pedido, ref dtTemp, session.Emp_Cnx);
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
                        if (Convert.ToDouble(dt.Rows[x]["Prd_PrecioLista"]) == 0)
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


                                Producto producto = new Producto();
                                //obtener datos de producto
                                CN_CatProducto clsProducto = new CN_CatProducto();
                                producto.Id_Cte = Convert.ToInt32(Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1"));
                                int productoNuevo = 0;
                                clsProducto.ConsultaProductos(ref producto, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, Convert.ToInt64(dt.Rows[x]["Id_Prd"]), ref productoNuevo, 0);

                                if (0 < producto.Prd_AAA)
                                {
                                    dt.Rows[x]["Prd_PrecioLista"] = producto.Prd_AAA;
                                }

                            }
                        }
                        if (Convert.ToInt64(dt.Rows[x]["Id_Prd"]) == Convert.ToInt64(dt.Rows[x]["Id_PrdOld"]))
                        {
                            dt.Rows[x]["Tipo_producto"] = "O";
                        }
                    }
                }




                Session["ProdVIOC"] = dt;
                rg1.DataSource = dt;
                rg1.DataBind();

                string _idTGStr = Request.QueryString["Id_TG"];
                int _idTG = 0;
                if (_idTGStr != null)
                {

                }

                Funciones funcion = new Funciones();
                CN_CatSemana CnSemana = new CN_CatSemana();
                Semana semana = new Semana();
                semana.Id_Emp = session.Id_Emp;
                semana.Id_Cd = session.Id_Cd_Ver;
                semana.Sem_FechaAct = funcion.GetLocalDateTime(session.Minutos);
                int verificador = -1;
                CnSemana.ConsultaSemana(ref semana, session.Emp_Cnx, ref verificador);

                if (verificador > -1)
                {
                    HF_FechaActual.Value = rdFechaF.Value.ToString().Trim();
                    HF_InicioSemana.Value = semana.Sem_FechaIni.ToString().Trim();
                    HF_FinSemana.Value = semana.Sem_FechaFin.ToString().Trim();
                }
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
                HF_ID.Value = txtFolio.Text;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                session = (Sesion)Session["Sesion" + Session.SessionID];
                Pedido pedido = new Pedido();
                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;
                pedido.Id_Ped = Convert.ToInt32(txtFolio.Text);

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

                //CheckBox1.Checked = pedido.Ped_ReqOrden;
                //txtReqOrd.Text = pedido.Ped_OrdenCompra;


                txtFecha.Value = pedido.Ped_AcysAnio.ToString().Trim();
                txtSemana.Value = pedido.Ped_AcysSemana.ToString().Trim();
                txtClave.Value = pedido.Id_Acs;



                txtSubtotal.Value = pedido.Ped_Subtotal.ToString().Trim();
                txtIva.Value = pedido.Ped_Iva.ToString().Trim();
                txtTotal.Value = pedido.Ped_Total.ToString().Trim();

                rdFechaE.Value = pedido.Ped_FechaEntrega;

                if (pedido.FechaFacAcys.Year != 1)
                {
                    rdFechaF.Value = pedido.FechaFacAcys;
                }
                TxtPed_PedAcys.Text = pedido.Pedido_del;
                TxtPed_ReqAcys.Text = pedido.Requisicion;
                TxtPed_OCAcys.Text = pedido.Ped_OrdenCompra;
                ddUsoCfdi.Value = pedido.UsoCFDI?.ToString().Trim();

                pedido.Ped_Tipo = 3;
                pedido.Ped_Captacion = true;

                CargarInfoAcys(pedido.Id_Acs, pedido.Ped_AcysSemana, pedido.Ped_AcysAnio);
                List<OrdenCompra_Detalle> Lista = new List<OrdenCompra_Detalle>();

                cn_capPedido.ConsultaPedidoDetCN(pedido, ref Lista, session.Emp_Cnx);


                string ConexionCentral = ConfigurationManager.AppSettings["strConnectionCentral"].ToString().Trim();
                ConvenioDet conv;
                ConvenioDet convdet;
                CN_Convenio cn_conv;
                List<string> Productos = new List<string>();
                List<ConvenioDet> lconveniosdet = new List<ConvenioDet>();
                ConvenioDet lconvdet = new ConvenioDet();

                foreach (OrdenCompra_Detalle dr in Lista)
                {
                    conv = new ConvenioDet();
                    convdet = new ConvenioDet();
                    cn_conv = new CN_Convenio();
                    conv.Id_Emp = session.Id_Emp;
                    conv.Id_Cd = session.Id_Cd_Ver;
                    conv.Id_Cte = Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1");
                    conv.Id_Prd = Convert.ToInt64(dr.Id_Prd);
                    double PrecioIngresado = Convert.ToDouble(dr.Prd_Precio);

                    if (Convert.ToInt64(dr.Id_Prd) <= 999999999999)
                    {
                        cn_conv.Convenio_ConsultaPrecio(conv, ref convdet, ConexionCentral);


                        Producto producto = new Producto();
                        //obtener datos de producto
                        CN_CatProducto clsProducto = new CN_CatProducto();
                        producto.Id_Cte = Convert.ToInt32(Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1"));
                        int productoNuevo = 0;
                        clsProducto.ConsultaProductos(ref producto, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, Convert.ToInt64(dr.Id_Prd), ref productoNuevo, 0);

                        if (0 < producto.Prd_AAA)
                        {
                            dr.Prd_PrecioLista = Convert.ToDouble(producto.Prd_AAA);
                        }
                    }
                }

                dt = null;

                GetListDet();
                DataTable dtTemp = dt;

                foreach (OrdenCompra_Detalle detalle in Lista)
                {
                    dtTemp.Rows.Add(new object[] {
                     detalle.Id_PrdOld ,
                    detalle.Id_Prd ,
                    detalle.Prd_Descripcion ,
                    detalle.Prd_Presentacion ,
                    detalle.Prd_Unidad  ,
                    detalle.mes1 ,
                    detalle.mes2 ,
                    detalle.mes3 ,
                    detalle.Prd_Cantidad  ,
                    detalle.Prd_Precio ,
                    detalle.Acs_PrecioAcys ,
                    detalle.Prd_Importe,
                    detalle.Acs_Documento,
                    detalle.Acs_Fecha,
                    detalle.Acs_Mod,
                    detalle.Acs_Dia ,
                    detalle.Acs_DiaStr ,
                    detalle.Acs_Frecuencia,
                    detalle.Prd_RemFact,
                    detalle.Ped_Asignar,
                    detalle.Id_TG,
                    detalle.Id_Acs,
                    detalle.ACS_ReqOC ,
                    detalle.Prd_PrecioLista ,
                    detalle.Tipo_producto,
                    detalle.Prd_Cantidadold ,
                });
                }

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


                            Producto producto = new Producto();
                            //obtener datos de producto
                            CN_CatProducto clsProducto = new CN_CatProducto();
                            producto.Id_Cte = Convert.ToInt32(Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1"));
                            int productoNuevo = 0;
                            clsProducto.ConsultaProductos(ref producto, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, Convert.ToInt64(dt.Rows[x]["Id_Prd"]), ref productoNuevo, 0);

                            if (0 < producto.Prd_AAA)
                            {
                                dt.Rows[x]["Prd_PrecioLista"] = producto.Prd_AAA;
                            }
                        }

                    }
                }

                Session["ProdVIOC"] = dt;
                rg1.DataSource = dt;
                rg1.DataBind();

                string _idTGStr = Request.QueryString["Id_TG"];
                int _idTG = 0;
                if (_idTGStr != null)
                {

                }

                Funciones funcion = new Funciones();
                CN_CatSemana CnSemana = new CN_CatSemana();
                Semana semana = new Semana();
                semana.Id_Emp = session.Id_Emp;
                semana.Id_Cd = session.Id_Cd_Ver;
                semana.Sem_FechaAct = funcion.GetLocalDateTime(session.Minutos);
                int verificador = -1;
                CnSemana.ConsultaSemana(ref semana, session.Emp_Cnx, ref verificador);

                if (verificador > -1)
                {
                    if (rdFechaF.Value != null)
                    {
                        HF_FechaActual.Value = rdFechaF.Value.ToString().Trim();
                    }
                    HF_InicioSemana.Value = semana.Sem_FechaIni.ToString().Trim();
                    HF_FinSemana.Value = semana.Sem_FechaFin.ToString().Trim();
                }
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
                cn_capPedidoVI.Consultar(ref pedido, session.Emp_Cnx, ref verificador, ref cc);

                txtCalle.Text = cc.Cte_Calle;
                txtNo.Text = cc.Cte_Numero;
                txtCp.Value = cc.Cte_Cp;
                txtMunicipio.Text = cc.Cte_Municipio;
                txtEstado.Text = cc.Cte_Estado;
                txtColonia.Text = cc.Cte_Colonia;

                TxtVersion.Text = pedido.Id_AcsVersion.ToString().Trim();
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
                else if (pedido.Acs_Modalidad == "C")
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
                if (txtTerritorioNom.SelectedItem.Value != "")
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
                //rdFecha.Value = funciones.GetLocalDateTime(Sesion.Minutos);
                //dpFecha2.Value = funciones.GetLocalDateTime(Sesion.Minutos).AddDays(1);
                //rdFecha.Focus();
                //txtClave.Text = MaximoId();
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


                    Session["ProdVIOC"] = dtTemp_Resto;
                    rg1.DataSource = dtTemp_Resto;
                    rg1.DataBind();
                }
            }
            catch (Exception ex)
            {
                Alerta(ex.Message.ToString().Trim());
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
                dt.Columns.Add("Tipo_producto", System.Type.GetType("System.String"));
                dt.Columns.Add("Prd_Cantidadold", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Prd_Activo", System.Type.GetType("System.Int32"));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void btncaptacion_Guardar_Click(object sender, EventArgs e)
        {
            PreGuardar();
        }

        protected void btnGuardar_ServerClick(object sender, EventArgs e)
        {
            Guardar();
        }

        protected void btnImprimir_ServerClick(object sender, EventArgs e)
        {
            Imprimir(Convert.ToInt32(Session["Id_Ped" + Session.SessionID]));
        }

        protected void btnDireccion_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Direccion", "AbrirBuscarDireccionEntrega()", true);
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
                        Int64 idProducto = Convert.ToInt64(Session["Id_Buscar" + Session.SessionID].ToString().Trim());
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

        protected void cmbProductoDet_TextChanged(Int64 idProducto)
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
                if (Session["ProdVIOC"] != null)
                {
                    dtTemp_Resto = (DataTable)Session["ProdVIOC"];
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
            });
                Session["ProdVIOC"] = dt;
                rg1.DataSource = dt;
                rg1.DataBind();
            }
            catch (Exception ex)
            {
                Alerta(ex.Message);
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


        #endregion
        #region gridFunciones

        protected void rg1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            string id_prd = "0";
            DataRowView row = (DataRowView)grid.GetRow(clickedRowIndex);
            if (Request.QueryString["IdAutorizacion"] != null)
            {
                grid.CancelEdit();
                return;
            }
            foreach (DataColumn column in row.DataView.Table.Columns)
            {
                if (column.ColumnName.ToString() == "Id_PrdOld")
                {
                    e.NewValues[column.ColumnName] = row[column.ColumnName];
                    id_prd = row[column.ColumnName].ToString();
                }
                if (column.ColumnName.ToString() == "Id_Prd")
                {
                    if (row[column.ColumnName].ToString() != id_prd)
                    {
                        grid.CancelEdit();
                        break;
                    }
                }
                if (column.ColumnName.ToString() == "Prd_Cantidad")
                {
                    if (Convert.ToInt32(row[column.ColumnName].ToString()) <= 0)
                    {
                        grid.CancelEdit();
                        break;
                    }
                }
                if (column.ColumnName.ToString() == "ACS_ReqOC")
                {
                    e.NewValues[column.ColumnName] = row[column.ColumnName];
                }
                if (column.ColumnName.ToString() == "Acs_Doc")
                {
                    e.NewValues[column.ColumnName] = row[column.ColumnName];
                }
                if (column.ColumnName.ToString() == "Prd_Precio")
                {
                    e.NewValues[column.ColumnName] = row[column.ColumnName];
                }
                if (column.ColumnName.ToString() == "Tipo_producto")
                {
                    e.NewValues[column.ColumnName] = "S";
                }


            }
        }

        protected void rg1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            String[] options = e.Parameters.Split('|');

            if (options.Length == 2)
            {
                if (options[0] == "New")
                    clickedRowIndex = Convert.ToInt32(options[1]);
            }

            grid.AddNewRow();
        }

        protected void lnkNew_Load(object sender, EventArgs e)
        {
            ASPxHyperLink link = sender as ASPxHyperLink;
            GridViewDataItemTemplateContainer container = link.NamingContainer as GridViewDataItemTemplateContainer;

            link.ClientInstanceName = String.Format("lnkNew_{0}", container.VisibleIndex);
            link.ClientSideEvents.Click = String.Format("function (s, e) {{ grid.PerformCallback(\"New|\" + {0}); }}", container.VisibleIndex);
        }

        protected void rg1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            GetListDet();
            DataTable dtTemp = (DataTable)Session["ProdVIOC"];

            ASPxGridView gridView = (ASPxGridView)sender;
            decimal cantidad = 0;
            decimal importe = 0;
            decimal total = 0;
            string Id_PrdOld = "0";
            int insertbool = 1;
            DataRow row = dtTemp.NewRow();
            IDictionaryEnumerator enumerator = e.NewValues.GetEnumerator();
            enumerator.Reset();
            while (enumerator.MoveNext())
            {
                if (enumerator.Key.ToString().Trim() != "Count")
                {
                    if (enumerator.Key.ToString() == "Id_PrdOld")
                    {
                        Id_PrdOld = enumerator.Value.ToString();
                    }
                    if (enumerator.Key.ToString() == "Prd_Cantidad")
                    {
                        cantidad = decimal.Parse(enumerator.Value.ToString());
                        row[enumerator.Key.ToString().Trim()] = enumerator.Value == null ? DBNull.Value : enumerator.Value;
                    }
                    else if (enumerator.Key.ToString() == "Prd_Precio")
                    {
                        importe = decimal.Parse(enumerator.Value.ToString());
                        row[enumerator.Key.ToString().Trim()] = enumerator.Value == null ? DBNull.Value : enumerator.Value;
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
                            row[enumerator.Key.ToString().Trim()] = enumerator.Value == null ? DBNull.Value : enumerator.Value;
                        }
                    }
                    else
                    {
                        row[enumerator.Key.ToString().Trim()] = enumerator.Value == null ? DBNull.Value : enumerator.Value;
                    }
                }
            }


            foreach (DataRow dr in dtTemp.Rows) // search whole table
            {
                if (dr["Id_PrdOld"].ToString() == Id_PrdOld)
                {
                    if (dr["Id_Prd"].ToString() == dr["Id_PrdOld"].ToString())
                    {

                        if (Convert.ToInt32(dr["Prd_Cantidad"].ToString()) < cantidad)
                        {
                            insertbool = 0;
                            break;
                        }
                        else
                        {
                            dr["Prd_Cantidad"] = Convert.ToInt32(dr["Prd_Cantidad"].ToString()) - cantidad;
                        }
                        if (dr["Prd_Cantidad"].ToString() == "0")
                        {
                            dr["Tipo_producto"] = "B";
                        }
                        else
                        {
                            dr["Tipo_producto"] = "P";
                        }

                    }
                }
            }
            if (insertbool == 1)
            {
                gridView.CancelEdit();
                e.Cancel = true;
                dtTemp.Rows.Add(row);
                Session["ProdVIOC"] = dtTemp;
                rg1.DataSource = dtTemp;
                rg1.DataBind();
                //Consultar_IVA_Cliente();
                //calcularsubtotal();
            }
        }

        protected void rg1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            DataTable dtTemp = (DataTable)Session["ProdVIOC"];
            ASPxGridView gridView = (ASPxGridView)sender;

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
                    if (Convert.ToDouble(row[enumerator.Key.ToString().Trim()].ToString()) > Convert.ToDouble(enumerator.Value))
                    {
                        row[enumerator.Key.ToString().Trim()] = enumerator.Value;
                    }
                    else
                    {
                        Alerta("Prueba de Cantidad");
                        break;
                    }
                }
                else if (enumerator.Key.ToString() == "Prd_Precio")
                {
                    importe = decimal.Parse(enumerator.Value.ToString());
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
                else if (enumerator.Key.ToString() == "Tipo_producto")
                {
                    if (row[enumerator.Key.ToString().Trim()].ToString() == "B")
                    {
                        Alerta("Prueba de TipoProducto");
                        break;
                    }
                }
                else
                {
                    row[enumerator.Key.ToString().Trim()] = enumerator.Value;
                }
            }

            gridView.CancelEdit();
            e.Cancel = true;

            Session["ProdVIOC"] = dtTemp;


            rg1.DataSource = dtTemp;
            rg1.DataBind();
            //Consultar_IVA_Cliente();
            //calcularsubtotal();
        }

        protected void rg1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            GetListDet();
            DataTable dtTemp = (DataTable)Session["ProdVIOC"];

            ASPxGridView gridView = (ASPxGridView)sender;

            string i = e.Keys["Id_Prd"].ToString();

            e.Cancel = true;
            int delete = -1;
            string id_prdOld = "0";
            string cantidad = "0";
            foreach (DataRow dr in dtTemp.Rows) // search whole table
            {
                if (dr["Id_Prd"].ToString() == i)
                {
                    if (dr["Id_Prd"].ToString() == dr["Id_PrdOld"].ToString())
                    {
                        ((ASPxGridView)sender).JSProperties["cpIsdelete"] = true;
                        break;
                    }
                    else
                    {
                        cantidad = dr["Prd_Cantidad"].ToString();
                        id_prdOld = dr["Id_PrdOld"].ToString();
                        delete = 1;
                        dr.Delete();

                        break;
                    }
                }
            }

            if (delete != -1)
            {
                foreach (DataRow dr in dtTemp.Rows) // search whole table
                {
                    if (dr["Id_PrdOld"].ToString() == id_prdOld)
                    {
                        if (dr["Id_Prd"].ToString() == dr["Id_PrdOld"].ToString())
                        {

                            dr["Prd_Cantidad"] = (Convert.ToInt32(dr["Prd_Cantidad"].ToString()) + Convert.ToInt32(cantidad)).ToString();
                            if (dr["Prd_Cantidad"].ToString() == dr["Prd_Cantidadold"].ToString())
                            {
                                dr["Tipo_producto"] = "O";
                            }
                            else
                            {
                                dr["Tipo_producto"] = "P";
                            }
                        }
                    }
                }
            }

            gridView.CancelEdit();
            e.Cancel = true;

            Session["ProdVIOC"] = dtTemp;

            rg1.DataSource = dtTemp;
            rg1.DataBind();
            //Consultar_IVA_Cliente();
            //calcularsubtotal();
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

        private void mensajeSoliitud(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "modalMensajesolicitud('" + mensaje + "')", true);
        }

        #endregion
        #region webMethod

        [WebMethod]
        public static string cmbProductoDetRestos(string IdProd, string idterr, string idCte, string IdRik, string clave, string IdCd, string IdEmp, string EmpCnx, string pedidoProg)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];


            IdEmp = Sesion.Id_Emp.ToString();
            IdCd = Sesion.Id_Cd_Ver.ToString();

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
                pedidoProg = pedidoProg == "" ? "0" : pedidoProg;
                pedidoProg = pedidoProg == "0" ? "false" : pedidoProg;
                pedidoProg = pedidoProg == "1" ? "true" : pedidoProg;


                DataTable dt = (DataTable)HttpContext.Current.Session["ProdVIOC"];


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

                if (bool.Parse(pedidoProg) && cnCa.ExisteProductoEnGarantia(Convert.ToInt32(IdEmp), Convert.ToInt32(IdCd), Convert.ToInt64(IdProd), Convert.ToInt32(idterr), Convert.ToInt32(idCte), Convert.ToInt32(IdRik), Sesion.Emp_Cnx))
                {
                    return JsonConvert.SerializeObject(new { id = 4 });
                }

                if (string.IsNullOrEmpty(clave))
                {
                    productoNuevo = 1;
                }
                pr.Id_Cte = !string.IsNullOrEmpty(idCte) ? Convert.ToInt32(idCte) : 0;
                cn_catproducto.ConsultaProductos(ref pr, Sesion.Emp_Cnx, Convert.ToInt32(IdEmp), Convert.ToInt32(IdCd), Convert.ToInt64(IdProd), ref productoNuevo, 2);

                cn_catproducto.ConsultarVentas(ref pr2, Convert.ToInt32(idCte), Sesion.Emp_Cnx);

                return JsonConvert.SerializeObject(new { id = 0, Presentacion = pr.Prd_Presentacion, PrdUni = pr.Prd_UniNs, Cant = 0, Precio = pr.Prd_Precio, imp = pr.Prd_Precio, PRecioLista = pr.Prd_AAA, Descripcion = pr.Prd_Descripcion, mes1 = pr2.ventaMes[0].ToString().Trim(), mes2 = pr2.ventaMes[1].ToString().Trim(), mes3 = pr2.ventaMes[2].ToString().Trim(), Prd_Activo = pr.Prd_Activo, Prd_InvFinal = pr.Prd_InvFinal });
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
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];


                IdEmp = Sesion.Id_Emp.ToString();
                IdCd = Sesion.Id_Cd_Ver.ToString();
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
                catproducto.ConsultaProducto_Disponible(Convert.ToInt32(IdEmp), Convert.ToInt32(IdCd), Id_prd, ref Actuales, Sesion.Emp_Cnx);


                CN_CapPedidoVtaInst pedido_vta = new CN_CapPedidoVtaInst();
                int verificador = 0;
                if (!string.IsNullOrEmpty(idCte))
                    pedido_vta.ConsultarAAAEspecial(Convert.ToInt32(IdEmp), Convert.ToInt32(IdCd), Convert.ToInt32(idCte), Id_prd, ref verificador, Sesion.Emp_Cnx);
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
            catch (Exception ex)
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
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name });

            }
        }

        [WebMethod(EnableSession = true)]
        public static string CalcularTotalVisible(string IdCd, string IdEmp, string EmpCnx)
        {
            try
            {
                double imp = 0;

                DataTable dt = (DataTable)HttpContext.Current.Session["ProdVIOC"];
                DataTable dt_Restos = (DataTable)HttpContext.Current.Session["Restos"];

                if (dt != null)
                {
                    foreach (DataRow i in dt.Rows)
                    {
                        if (i["Tipo_producto"].ToString() != "B")
                        {
                            imp += Convert.ToDouble(i["Prd_Importe"] == DBNull.Value ? 0 : i["Prd_Importe"]);
                        }
                    }
                }


                imp.ToString().Trim();

                double iva_cd = 16;
                CN_CatCentroDistribucion cn = new CN_CatCentroDistribucion();
                //cn.ConsultarIva(int.Parse(IdEmp), int.Parse(IdCd), ref iva_cd, EmpCnx);
                string IVASistema = (imp * iva_cd / 100).ToString("F2").Trim();
                string totalImporte = imp > 0 ? (imp + Convert.ToDouble(IVASistema)).ToString("F2").Trim() : "0";

                return JsonConvert.SerializeObject(new { id = 1, subtotal = imp.ToString("F2"), iva = IVASistema, total = totalImporte });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = "Error al realizar el el total de importe" });
            }

        }


        #endregion

        protected void ButtonSolicitud_ServerClick(object sender, EventArgs e)
        {

            GuardarSolicitud();

        }

        private void EnviarCorreo(string Cliente, int Usuario, string conexion, ref string mensage)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                PedidoVtaInst pedido = new PedidoVtaInst();
                List<UsuarioCorreo> ListaCorreoUsuario = new List<UsuarioCorreo>();



                CN_enviarCorreo Envia = new CN_enviarCorreo();
                Envia.ConsultarCorreoUsuario(session.Id_Emp, session.Id_Cd, conexion, ref ListaCorreoUsuario);
                string fecha = DateTime.Now.ToString("dd/MM/yyyy");
                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Cd = session.Id_Cd;
                configuracion.Id_Emp = session.Id_Emp;
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                cn_configuracion.Consulta(ref configuracion, conexion);
                StringBuilder cuerpo_correo = new StringBuilder();

                cuerpo_correo.Append("<div align='center'>");
                cuerpo_correo.Append("<table><tr><td>");
                cuerpo_correo.Append("<td></td>");
                cuerpo_correo.Append("</tr><tr><td colspan='2'><br><br><br></td>");
                cuerpo_correo.Append("</tr><tr>");
                cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='2'>");

                cuerpo_correo.Append("Buen día, Se les comunica por este medio. <br><br> ");

                cuerpo_correo.Append("Se ha realizado la siguiente solicitud de autorización en captura de OC Centralizada. <br><br>");
                cuerpo_correo.Append("CDI:" + sesion.Id_Cd + " <br>");
                cuerpo_correo.Append("Usuario: " + txtRikNom.Value + " <br>");
                cuerpo_correo.Append("Cliente: " + txtClienteNom.Text + " <br>");
                cuerpo_correo.Append("OC Centralizada: " + TxtPed_ReqAcys.Text + " <br><br>");


                cuerpo_correo.Append("Producto de venta original: <br>");
                cuerpo_correo.Append("Precio de venta: <br>");
                cuerpo_correo.Append("Producto de venta sustituto. <br>");
                cuerpo_correo.Append("Precio AAA: <br>");




                cuerpo_correo.Append("<table border='0' cellspacing='0' cellpadding='0' width='100 % ' style='width: 100.0 %; border-collapse:collapse; border-spacing:0; max-width:100 % '>");
                cuerpo_correo.Append("<thead>");
                cuerpo_correo.Append("<tr>");

                cuerpo_correo.Append("<td valign='bottom' style='padding: 7.5pt 7.5pt 7.5pt 7.5pt'>");
                cuerpo_correo.Append("Producto de venta original");
                cuerpo_correo.Append("</td>");

                cuerpo_correo.Append("<td valign='bottom' style='padding: 7.5pt 7.5pt 7.5pt 7.5pt'>");
                cuerpo_correo.Append("Producto de venta sustituto");
                cuerpo_correo.Append("</td>");

                cuerpo_correo.Append("<td valign='bottom' style='padding: 7.5pt 7.5pt 7.5pt 7.5pt'>");
                cuerpo_correo.Append("Precio de venta");
                cuerpo_correo.Append("</td>");


                cuerpo_correo.Append("<td valign='bottom' style='padding: 7.5pt 7.5pt 7.5pt 7.5pt'>");
                cuerpo_correo.Append("Precio AAA");
                cuerpo_correo.Append("</td>");

                cuerpo_correo.Append("</TR>");
                cuerpo_correo.Append("</thead>");

                for (int x = 0; x < dt.Rows.Count; x++)
                {

                    if (dt.Rows[x]["Id_Prd"].ToString() != dt.Rows[x]["Id_PrdOld"].ToString())
                    {
                        cuerpo_correo.Append("<TR>");

                        cuerpo_correo.Append("<td valign='top' style='border: none; border-top:solid #cccccc 1.0pt;padding:7.5pt 7.5pt 7.5pt 7.5pt'>");
                        cuerpo_correo.Append(dt.Rows[x]["Id_PrdOld"].ToString());
                        cuerpo_correo.Append("</td>");

                        cuerpo_correo.Append("<td valign='top' style='border: none; border-top:solid #cccccc 1.0pt;padding:7.5pt 7.5pt 7.5pt 7.5pt'>");
                        cuerpo_correo.Append(dt.Rows[x]["Id_Prd"].ToString());
                        cuerpo_correo.Append("</td>");

                        cuerpo_correo.Append("<td valign='top' style='border: none; border-top:solid #cccccc 1.0pt;padding:7.5pt 7.5pt 7.5pt 7.5pt'>");
                        cuerpo_correo.Append(Convert.ToDecimal(dt.Rows[x]["Prd_Precio"].ToString()));
                        cuerpo_correo.Append("</td>");


                        cuerpo_correo.Append("<td valign='top' style='border: none; border-top:solid #cccccc 1.0pt;padding:7.5pt 7.5pt 7.5pt 7.5pt'>");
                        cuerpo_correo.Append(Convert.ToInt32(dt.Rows[x]["Prd_PrecioLista"].ToString()));
                        cuerpo_correo.Append("</td>");

                        cuerpo_correo.Append("</TR>");
                    }
                }




                //cuerpo_correo.Append("Cordinardor de la cuenta: Prueba <br>");
                cuerpo_correo.Append("Sistema automático de reporte de captación de pedido. <br>  Fecha de realización: " + fecha + "<br>");
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");
                cuerpo_correo.Append("<center><br>");
                cuerpo_correo.Append("</td></tr></table></div>");

                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);

                MailMessage m = new MailMessage();
                m.From = new MailAddress(configuracion.Mail_Remitente, "solicitud autorizacion producto AAA orden de compra: Prueba");
                m.Subject = "Reporte ";
                //foreach (UsuarioCorreo correo in ListaCorreoUsuario)
                //{
                //    m.To.Add(new MailAddress(correo.U_Correo));
                //} 
                m.To.Add(new MailAddress("erikrgc@hotmail.com"));
                m.IsBodyHtml = true;

                string body = cuerpo_correo.ToString().Trim();
                AlternateView vistaHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);

                m.AlternateViews.Add(vistaHtml);
                try
                {
                    sm.Send(m);
                }
                catch (Exception ex)
                {
                    mensage = "Fallo en enviar el correo electronico";
                }

            }
            catch (Exception)
            {
                mensage = "Fallo en la configuración del correo";
            }
        }

        protected void rg1_HtmlDataCellPrepared(object sender, BootstrapGridViewTableDataCellEventArgs e)
        {
            bool inactivos = false;
            if (e.DataColumn.FieldName == "Id_Prd")
            {
                // Obtén el valor de la fila actual para Prd_ACtivo
                //var prdActivoValue = Convert.ToInt32(e.GetValue("Prd_Activo"));
                var prdActivoValue = Convert.ToInt32(e.GetValue("Prd_Activo") as int? ?? 0);

                // Verifica la condición
                if (prdActivoValue == 2)
                {
                    // Cambia el color de fondo de la celda a rojo
                    e.Cell.BackColor = System.Drawing.Color.Red;
                    e.Cell.ForeColor = System.Drawing.Color.White;
                    inactivos = true;
                }
            }

            if (inactivos)
            {
                Alerta("Hay productos inactivos en el detalle de captación de pedidos");
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
    }
}