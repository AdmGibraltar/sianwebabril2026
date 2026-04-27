using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Text;

using System.Globalization;
using System.Configuration;
using System.IO;

using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Data;

using System.Data.SqlClient;
using CapaDatos;

namespace SIANWEB
{
    public partial class ComprasLocales : System.Web.UI.Page
    {
        public string strIdPrd = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        Pagina pagina = new Pagina();
                        string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                        if (pag.Length > 1)
                            pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                        else
                            pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;
                        CN_Pagina CapaNegocio = new CN_Pagina();
                        CapaNegocio.PaginaConsultar(ref pagina, Sesion.Emp_Cnx);
                        Session["Head" + Session.SessionID] = pagina.Path;

                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            // RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        //  CargarCentros();
                        CargarValoresCombo();
                        Inicializar();
                        InicializaSolicitud();
                        IniciaConsulta();
                        if (Request.QueryString["SolCompra"] != null)
                        {
                            //  Alerta("llego con una solicitud en especifico");
                            this.cmbCategorias.SelectedIndex = 5;

                            this.divActivacion.Visible = false;
                            this.divAbasto.Visible = false;
                            this.divSolicitudCliente.Visible = false;
                            this.divConsultaSolicitud.Visible = true;
                            this.btnRegresarCons.Visible = true;

                            this.txtBuscaXSolCom.Text = Request.QueryString["SolCompra"].ToString();

                            BuscaSolicitudesDirecta(Convert.ToInt32(Request.QueryString["SolCompra"].ToString()));

                            CompraLocal cl = new CompraLocal();

                            int Id_SolDet = 0;
                            Id_SolDet = Convert.ToInt32(Request.QueryString["SolCompra"].ToString());

                            cl.Id_Emp = Sesion.Id_Emp;
                            cl.Id_Cd = Sesion.Id_Cd_Ver;
                            cl.Id_Comp = Id_SolDet;

                            CN_ProCompraLocal cn_ListadoDetalleCompraLocal = new CN_ProCompraLocal();
                            DataTable DetalleSolicitud = new DataTable();
                            DetalleSolicitud.Columns.Add("Solicitud");
                            DetalleSolicitud.Columns.Add("Num");
                            DetalleSolicitud.Columns.Add("Descripcion");
                            DetalleSolicitud.Columns.Add("Costo");
                            DetalleSolicitud.Columns.Add("Estatus");
                            DetalleSolicitud.Columns.Add("EstatusStr");
                            DetalleSolicitud.Columns.Add("TipoSol");

                            cn_ListadoDetalleCompraLocal.ConsultarSolicitud(cl, ref DetalleSolicitud, Sesion.Emp_Cnx);

                            rgDetalleSolicitud.DataSource = DetalleSolicitud;
                            rgDetalleSolicitud.Rebind();
                            rgDetalleSolicitud.Visible = true;

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        #region Variables

        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        private DataTable dt { get { return (DataTable)Session["dt" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["dt" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        public bool NoProveedorFijado;

        private List<ProductoPrecios> listSource
        {
            get { return (List<ProductoPrecios>)Session["listSource"]; }
            set { Session["listSource"] = value; }
        }



        public long IdProducto
        {
            get { return Convert.ToInt32(Session["_IdProducto"]); }
            set { Session["_IdProducto"] = value; }
        }

        public string Valor
        {
            get
            {
                return "";  //   MaximoId();
            }
            set { }
        }

        #endregion

        #region Funciones

        private void CargarCentros()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();

                if (Sesion.U_MultiOfi == false)
                {
                    CN_Comun.LlenaCombo(2, Sesion.Id_Emp, Sesion.Id_U, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref cmbCentrosDist);
                    this.dvCmbCentros.Visible = false;
                    this.cmbCentrosDist.SelectedValue = Sesion.Id_Cd_Ver.ToString();
                    this.txtCentrosDist.Value = Sesion.Id_Cd_Ver.ToString();
                    this.LabelCDI81.Text = this.LabelCDI81.Text + " <b>" + cmbCentrosDist.FindItemByValue(Sesion.Id_Cd_Ver.ToString()).Text + "</b>";
                    //  this.TblEncabezado.Rows[0].Cells[2].InnerText = " " + cmbCentrosDist.FindItemByValue(Sesion.Id_Cd_Ver.ToString()).Text;
                }
                else
                {
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, Sesion.Id_Cd_Ver, Sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref cmbCentrosDist);
                    this.cmbCentrosDist.SelectedValue = Sesion.Id_Cd_Ver.ToString();
                    this.txtCentrosDist.Value = Sesion.Id_Cd_Ver.ToString();
                    this.dvCmbCentros.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AgregaCausaDesabasto()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            if (this.txtCatCausaDes.Text != "")
            {
                CN_ComprasLocales cn_Listadocompralocal = new CN_ComprasLocales();
                string causa = txtCatCausaDes.Text;

                cn_Listadocompralocal.CatDesAgrega(causa, Sesion.Emp_Cnx);
            }
            ListadoCatAbasto();
        }

        public object ClonarPrecioProducto(object obj)
        {
            object objResult = null;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, obj);
                ms.Position = 0;
                objResult = bf.Deserialize(ms);
            }
            return objResult;
        }

        private void CargarValoresCombo()
        {
            try
            {

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(sesion.Emp_Cnx, "spCatMotivoCompraLocal_Combo", ref cmbCategorias);
                cmbCategorias.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));

                /*
                cmbCategorias.Items.Add(new RadComboBoxItem("-- Seleccionar --", "-1"));
                cmbCategorias.Items.Add(new RadComboBoxItem("Activación de Código por Desabasto", "01"));
                cmbCategorias.Items.Add(new RadComboBoxItem("Código Central con Abasto Local", "02"));
                cmbCategorias.Items.Add(new RadComboBoxItem("Solicitud del Cliente", "03"));
                cmbCategorias.Items.Add(new RadComboBoxItem("-------------------------------------", "-1"));
                cmbCategorias.Items.Add(new RadComboBoxItem("Consultar Solicitud", "04"));
                cmbCategorias.Items.Add(new RadComboBoxItem("Catalogo Causas de Desabasto", "05"));
                */
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Submit(object sender, EventArgs e)
        {
            string customerName = Request.Form[txtSearch.UniqueID];
            string customerId = Request.Form[hfCustomerId.UniqueID];
            Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
            if (customerId != string.Empty && customerId != "-1")
            {
                int id_Cd_Prod = session.Id_Cd_Ver;
                HabilitaCamposComprasLocales(true);


                this.LlenaComboListadoSATDesabasto();

                this.LlenarFormularioProducto(Convert.ToInt32(customerId), id_Cd_Prod);

                this.LlenarProdcutosHermanos(Convert.ToInt32(customerId), ref txtProductosMismoPadre);

                this.LlenarListaPedidos(0, Convert.ToInt32(customerId));  /// , ref lstbPedidos);

                //  this.LlenarListaPedidos(0, Convert.ToInt32(customerId), ref ListBox2);

                // No hay pedidos referentes al producto, se ocultan los controles
                //      quedan ocultos los controles ya que solo se despliegan cuando se seleccionan las dos opciones del combo
                /*
                if (this.chklstPedidos.Items.Count == 0 )   //  (lstbPedidos.Items.Count == 0 )
                {
                    divPedidosRefAbasto.Visible = false;
                }
                else
                {
                    divPedidosRefAbasto.Visible = true;
                }
                */
                this.divPedidosRefAbasto.Visible = false;

                this.lbl_Val_UnidadesDisponibles.Text = "";
                this.lblNumSolicitud.Text = "";
                this.hiddenId.Value = customerId;
                TextId_Prd.Enabled = false;

                txtCodigoUsadoProd.Text = this.MaximoId(Convert.ToInt32(TextId_Prd.Text), this.txtProveedor.Text);
                txtCodProd.Text = txtCodigoUsadoProd.Text;
                lblTituloProducto.Text = string.Concat(txtCodProd.Text, " - ", TextPrd_Descrpcion.Text);
                txtCodigoUsadoProd.Enabled = false;
                txtCodProd.Enabled = false;

                // consulta disponibilidad del producto y lo pone en lbl_Val_UnidadesDisponibles
                //  this.Revisadisponibilidad(Convert.ToInt32(customerId));

            }
            if (customerId == string.Empty)        //  || e.Value == "-1"
            {
                this.LimpiarCampos();
                //    cmbProductosLista.Text = "-- Seleccionar --";
            }

        }

        protected void SubmitProductoLocal(object sender, EventArgs e)
        {
            string prductoName = Request.Form[txtProductoLocal.UniqueID];
            string prductoId = Request.Form[hfProductoLocal.UniqueID];

            Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
            if (prductoId != string.Empty && prductoId != "-1")
            {
                int id_Cd_Prod = session.Id_Cd_Ver;
                //  HabilitaCamposComprasLocales2(true);

                this.LlenaComboListadoSATAbasto();
                this.LlenarFormularioProductoAbasto_funcion(Convert.ToInt64(prductoId), id_Cd_Prod);

                this.LlenarProdcutosHermanos(Convert.ToInt64(prductoId), ref txtProductosMismoPadreAbasto);

                if (this.lblcmbFam.Text == "-- Seleccionar --")
                //  if (this.hddFamiliaAbasto.Value == "")
                {
                    DisplayMensajeAlerta("SeleccionaProductoCL_familia_error");
                    prductoId = "";
                    cmbProductosHabiliCompraLocal.SelectedIndex = 0;
                    LimpiarCampos2();
                }
                else
                {
                    this.hiddenId.Value = prductoId;
                    this.hidRpoveedorOriginal.Value = this.txtProveeAba.Text;
                    TextId_Prd.Enabled = false;
                }
            }
            /*
            if (prductoId == string.Empty)
            {
                this.LimpiarCampos2();
            }
            */
        }

        protected void SubmitProductoSoli(object sender, EventArgs e)
        {
            string prductoName = this.RadComboBoxProduct.Text;
            string prductoId = this.RadComboBoxProduct.SelectedValue.ToString();

            if (this.txtIdProductoCteF4F.Visible)
            {
                prductoId = Request.Form[hdfProductoCteF4F.UniqueID];
            }

            Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
            if (prductoId != string.Empty && prductoId != "-1")
            {
                int id_Cd_Prod = session.Id_Cd_Ver;
                //  InicializaSolicitud();
                HabilitaCamposSolicitud(true);
                this.LlenarFormularioProductoSoliCliente(Convert.ToInt32(prductoId), id_Cd_Prod);
                this.hiddenId.Value = prductoId;
                PestanasCte.Visible = true;

                //  TextId_Prd.Enabled = false;
            }
        }

        protected void BuscaCombinado(object sender, EventArgs e)
        {
            string provName = Request.Form[txtBuscaXProvee.UniqueID];
            string provId = Request.Form[hdtxtBuscaProv.UniqueID];

            string prodName = Request.Form[txtBuscaXCodProd.UniqueID];
            string prodId = Request.Form[hdtxtBuscaCodi.UniqueID];

            string SolId = Request.Form[txtBuscaXSolCom.UniqueID];

            if (provName != string.Empty || prodName != string.Empty || SolId != string.Empty)
            {
                this.BuscaSolicitudesCombo(SolId == "" ? 0 : Convert.ToInt32(SolId), prodName == "" ? 0 : Convert.ToInt32(prodId), provName == "" ? 0 : Convert.ToInt32(provId));


            }
        }

        protected void RegresaDeConsulta(object sender, EventArgs e)
        {
            Response.Redirect("ComprasLocalesConsulta.aspx");
        }


        protected void BuscaSoliXProve(object sender, EventArgs e)
        {
            string provName = Request.Form[txtBuscaXProvee.UniqueID];
            string provId = Request.Form[hdtxtBuscaProv.UniqueID];

            if (provId != string.Empty && provId != "-1")
            {
                this.BuscaSolicitudesPorProveedor(Convert.ToInt32(provId));
            }
        }

        protected void BuscaSoliXProdu(object sender, EventArgs e)
        {
            string prodName = Request.Form[txtBuscaXCodProd.UniqueID];
            string prodId = Request.Form[hdtxtBuscaCodi.UniqueID];

            if (prodId != string.Empty && prodId != "-1")
            {
                this.ConsultaSolicitudXProducto(Convert.ToInt32(prodId));
            }
        }

        protected void BuscaSolixSoli(object sender, EventArgs e)
        {
            string SolId = Request.Form[txtBuscaXSolCom.UniqueID];

            if (SolId != string.Empty && SolId != "0")
            {
                this.BuscaSolicitudesDirecta(Convert.ToInt32(SolId));
            }
        }

        private void Creardt()
        {
            try
            {
                dt = new DataTable();

                dt.Columns.Add("Num");
                dt.Columns.Add("Descripcion");
                dt.Columns.Add("Costo");
                dt.Columns.Add("Estatus");
                dt.Columns.Add("EstatusStr");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ChecarProductoYaSolicitado(long Id_Prd, ref int ok)
        {
            try
            {

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                // Convert.ToInt64(txtCodigoUsadoProd.Text);

                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Producto" };
                object[] Valores = { Id_Prd };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCompraLocalPedidosProducto_ChecaDuplicado", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    ListItem item = new ListItem();

                    ok = Convert.ToInt32(dr["Duplicado"].ToString());

                }
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Guardar(ref Producto prod, ref int ok)
        {
            try
            {
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatProducto clsCatProducto = new CN_CatProducto();
                int verificador = -1;
                Producto producto = this.LlenatObjetoProducto();

                foreach (GridDataItem dataItem in rgPrecios.MasterTableView.Items)
                {
                    if (Convert.ToInt32(dataItem["Id_Pre"].Text) == 1 && Convert.ToBoolean(dataItem["Prd_Actual"].Text) == true)
                    {
                        if (Convert.ToDouble((((Label)dataItem.FindControl("lblPrd_Pesos")).Text)) == 0)
                        {
                            Alerta("El precio AAA actual no puede ser 0");
                            return;
                        }
                    }
                    if (Convert.ToInt32(dataItem["Id_Pre"].Text) == 2 && Convert.ToBoolean(dataItem["Prd_Actual"].Text) == true)
                    {
                        if (Convert.ToDouble((((Label)dataItem.FindControl("lblPrd_Pesos")).Text)) == 0)
                        {
                            Alerta("El precio LISTA actual no puede ser 0");
                            return;
                        }
                    }
                    if (Convert.ToInt32(dataItem["Id_Pre"].Text) == 3 && Convert.ToBoolean(dataItem["Prd_Actual"].Text) == true)
                    {
                        if (Convert.ToDouble((((Label)dataItem.FindControl("lblPrd_Pesos")).Text)) == 0)
                        {
                            Alerta("El precio COSTO actual no puede ser 0");
                            return;
                        }
                    }

                }




                producto.Id_Prd = Convert.ToInt64(txtCodigoUsadoProd.Text);

                if ((producto.Prd_Descripcion != string.Empty) && (producto.Id_Prd.ToString() != string.Empty))
                {
                    lblTituloProducto.Text = string.Concat(producto.Id_Prd.ToString(), " - ", producto.Prd_Descripcion);
                }
                if (hiddenId.Value != string.Empty)
                {
                    //if (!_PermisoGuardar)
                    //{
                    //    DisplayMensajeAlerta("PermisoGuardarNo");
                    //    return;
                    //}
                    int Id_PrdOriginal = Convert.ToInt32(TextId_Prd.Text);
                    // graba en la localizacion alterna de producto/precios
                    clsCatProducto.InsertarProducto_CL(producto, session.Emp_Cnx, ref verificador);

                    //  this.LimpiarCampos();
                    this.IdProducto = producto.Id_Prd;
                    Funciones funciones = new Funciones();


                    /// Graba la Solicitud en si, cada solicitud va con un producto por vuelta.
                    CompraLocal cl = new CompraLocal();
                    cl.Id_Emp = session.Id_Emp;
                    cl.Id_Cd = session.Id_Cd_Ver;
                    cl.FechaSol = funciones.GetLocalDateTime(session.Minutos);
                    cl.Comp_Solicito = session.Id_U;
                    verificador = 0;
                    CN_ProCompraLocal cn_procompralocal = new CN_ProCompraLocal();

                    /// se llena un dataset con el dato del producto nuevo para enviar a generar la solicitud
                    Creardt();
                    /// // 1 prudcto = 1 solicutd
                    foreach (ProductoPrecios productoPrecios in producto.ListaProductoPrecios)
                    {
                        if (productoPrecios.Pre_Descripcion == "COSTO" && productoPrecios.Prd_Actual == true)
                        {
                            dt.Rows.Add(new object[] { producto.Id_Prd, producto.Prd_Descripcion, Convert.ToDouble(productoPrecios.Prd_Pesos).ToString("#,##0.00"), 0, "Sin autorizar" });
                        }
                    }

                    cn_procompralocal.InsertarSolicitud(cl, dt, session.Emp_Cnx, ref verificador);

                    hfNumSolicitud.Value = verificador.ToString();

                    prod = producto;

                    // guarda una copia del original vs el clon para 
                    CN_ComprasLocales clLocales = new CN_ComprasLocales();
                    clLocales.ProductosClonados(verificador, Id_PrdOriginal, producto.Id_Prd, session.Emp_Cnx);
                    ok = 1;


                    /// Guarda los datos de SAT
                    string CveUnidad = cmbUnidadMedidaSATDesabasto.SelectedValue;
                    string CveProducto = this.hfCveProdServSATAbasto.Value;     //  cmbProdServicioSATDesabasto.SelectedValue;
                    clLocales.GrabaDatosProductoSAT(verificador, producto.Id_Prd, CveUnidad, CveProducto, session.Emp_Cnx);
                    //  this.DisplayMensajeAlerta("CatProducto_insert_ok");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void HabilitaEnviar()
        {
            string providerName = Request.Form[txtSearchProv.UniqueID];
            string providerId = Request.Form[hfProviderId.UniqueID];

        }

        private Producto LlenatObjetoProductoAbasto()
        {
            Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
            Producto producto = new Producto();

            producto.Id_Emp = session.Id_Emp;
            producto.Id_Cd = session.Id_Cd_Ver;
            producto.Id_Prd = Convert.ToInt64(this.lblId_Prd.Text);
            producto.Id_Ptp = lblTipoProducto.Text == string.Empty ? 0 : Convert.ToInt32(lblTipoProducto.Text);
            producto.Prd_Descripcion = lblPrd_Descrpcion.Text;
            producto.Prd_Presentacion = lblPresentacion.Text;
            producto.Prd_Nuevo = chkProductoNuevoAbasto.Checked;
            producto.Prd_Mes = lbltxtAmortizacion.Text == string.Empty ? 0 : Convert.ToInt32(lbltxtAmortizacion.Text);
            producto.Id_Spo = lblId_Spo.Text == string.Empty ? 0 : Convert.ToInt32(lblId_Spo.Text);
            producto.Prd_AgrupadoSpo = lblAgrupadoSpo.Text == string.Empty ? 0 : Convert.ToInt32(lblAgrupadoSpo.Text);
            producto.Id_Cpr = lblCategoria.Text == string.Empty ? 0 : Convert.ToInt32(lblCategoria.Text);
            producto.Id_Fam = lblFam.Text == string.Empty ? 0 : Convert.ToInt32(lblFam.Text);
            producto.Id_Sub = lblSubFam.Text != string.Empty ? Convert.ToInt32(lblSubFam.Text) : 0;
            producto.Id_Pvd = txtProveeAba.Text == string.Empty ? 0 : Convert.ToInt32(txtProveeAba.Text);
            producto.Prd_UniNe = lblcmbUentrada.Text;
            producto.Prd_FactorConv = lbltxtFactorConversion.Text == string.Empty ? 0 : Convert.ToSingle(lbltxtFactorConversion.Text);
            producto.Prd_UniNs = lblcmbUsalida.Text;
            producto.Prd_UniEmp = lbltxtUempaque.Text == string.Empty ? 0 : Convert.ToSingle(lbltxtUempaque.Text);
            producto.Prd_InvSeg = lbltxtInvSeguridad.Text == string.Empty ? 0 : Convert.ToInt32(lbltxtInvSeguridad.Text);
            producto.Prd_TEntre = lbltxtTentrega.Text == string.Empty ? 0 : Convert.ToInt32(lbltxtTentrega.Text);
            producto.Prd_PlanAbasto = lbltxtPlanAbasto.Text;
            producto.Prd_Minimo = lbltxtMinCompra.Text == string.Empty ? 0 : Convert.ToInt32(lbltxtMinCompra.Text);
            producto.Prd_Mes = lbltxtAmortizacion.Text == string.Empty ? 0 : Convert.ToInt32(lbltxtAmortizacion.Text);
            producto.Prd_TTrans = lbltxtTtransporte.Text == string.Empty ? 0 : Convert.ToInt32(lbltxtTtransporte.Text);
            producto.Prd_Ubicacion = lbltxtUbicacion.Text;
            producto.Prd_PesConTecnico = lbltxtPesos.Text == string.Empty ? 0 : Convert.ToDouble(lbltxtPesos.Text);
            producto.Prd_MaxExistencia = lbltxtExistencia.Text == string.Empty ? 0 : Convert.ToInt32(lbltxtExistencia.Text);
            producto.Prd_Asignado = lbltxtAsignado.Text == string.Empty ? 0 : Convert.ToInt32(lbltxtAsignado.Text);
            producto.Prd_InvInicial = lbltxtInicial.Text == string.Empty ? 0 : Convert.ToInt32(lbltxtInicial.Text);
            producto.Prd_InvFinal = lbltxtFinal.Text == string.Empty ? 0 : Convert.ToInt32(lbltxtFinal.Text);
            producto.Prd_Ordenado = lbltxtOrdenado.Text == string.Empty ? 0 : Convert.ToInt32(lbltxtOrdenado.Text);
            producto.Prd_Fisico = lbltxtFisico.Text == string.Empty ? 0 : Convert.ToInt32(lbltxtFisico.Text);
            producto.Prd_CLNomFab = lbltxtFnombre.Text;
            producto.Prd_CLIdFab = lbltxtFcodigo.Text;
            producto.Prd_CLDesFab = lbltxtFdescripcion.Text;
            producto.Prd_CLPreFab = lbltxtFpresentacion.Text;
            producto.Prd_CLNomPro = txtPnombreAbasto.Text;
            producto.Prd_CLIdPro = lbltxtPcodigo.Text;
            producto.Prd_CLDesPro = lbltxtPdescripcion.Text;
            producto.Prd_CLPrePro = lbltxtPpresentacion.Text;
            producto.Prd_Transito = lbltxtTransito.Text == string.Empty ? 0 : Convert.ToInt32(lbltxtTransito.Text);
            producto.Prd_Colo = true;
            producto.Prd_Unico = Convert.ToInt32(hidProductoOriginal.Value); //  lblId_Prd.Text == string.Empty ? 0 : Convert.ToInt32(lblId_Prd.Text);
            producto.Prd_CptSv = string.Empty;
            producto.Prd_FecAlta = DateTime.Now;
            producto.Prd_Activo = chkActivoAbasto.Checked ? 1 : 2;

            producto.ListaProductoPrecios = this.listSource;

            return producto;
        }

        private Producto LlenatObjetoProducto()
        {
            Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
            Producto producto = new Producto();

            producto.Id_Emp = session.Id_Emp;
            producto.Id_Cd = session.Id_Cd_Ver;
            producto.Id_Prd = Convert.ToInt64(TextId_Prd.Text);
            producto.Id_Spo = TextId_Spo.Text == string.Empty ? 0 : Convert.ToInt32(TextId_Spo.Text);
            producto.Id_Ptp = txtTipoProducto.Text == string.Empty ? 0 : Convert.ToInt32(txtTipoProducto.Text);
            producto.Id_Cpr = txtCategoria.Text == string.Empty ? 0 : Convert.ToInt32(txtCategoria.Text);
            producto.Id_Fam = txtFam.Value.HasValue ? Convert.ToInt32(txtFam.Text) : 0;
            producto.Id_Sub = txtSubFam.Value.HasValue ? Convert.ToInt32(txtSubFam.Text) : 0;
            producto.Id_Pvd = txtProveedor.Text == string.Empty ? 0 : Convert.ToInt32(txtProveedor.Text);
            producto.Prd_Descripcion = TextPrd_Descrpcion.Text;
            producto.Prd_Presentacion = txtPresentacion.Text;
            producto.Prd_InvInicial = txtInicial.Text == string.Empty ? 0 : Convert.ToInt32(txtInicial.Text);
            producto.Prd_InvFinal = txtFinal.Text == string.Empty ? 0 : Convert.ToInt32(txtFinal.Text);
            producto.Prd_AgrupadoSpo = txtAgrupadoSpo.Text == string.Empty ? 0 : Convert.ToInt32(txtAgrupadoSpo.Text);
            producto.Prd_FactorConv = txtFactorConversion.Text == string.Empty ? 0 : Convert.ToSingle(txtFactorConversion.Text);
            producto.Prd_AparatoSisProp = chkSistProp.Checked;
            producto.Prd_Fisico = txtFisico.Text == string.Empty ? 0 : Convert.ToInt32(txtFisico.Text);
            producto.Prd_Ordenado = txtOrdenado.Text == string.Empty ? 0 : Convert.ToInt32(txtOrdenado.Text);
            producto.Prd_Asignado = txtAsignado.Text == string.Empty ? 0 : Convert.ToInt32(txtAsignado.Text);
            producto.Prd_InvSeg = txtInvSeguridad.Text == string.Empty ? 0 : Convert.ToInt32(txtInvSeguridad.Text);
            producto.Prd_TTrans = txtTtransporte.Text == string.Empty ? 0 : Convert.ToInt32(txtTtransporte.Text);
            producto.Prd_TEntre = txtTentrega.Text == string.Empty ? 0 : Convert.ToInt32(txtTentrega.Text);
            producto.Prd_Transito = txtTransito.Text == string.Empty ? 0 : Convert.ToInt32(txtTransito.Text);
            producto.Prd_UniNe = cmbUentrada.SelectedValue.ToString().Trim() == "-1" ? string.Empty : cmbUentrada.SelectedValue;
            producto.Prd_UniNs = cmbUsalida.SelectedValue.ToString().Trim() == "-1" ? string.Empty : cmbUsalida.SelectedValue;
            producto.Prd_Unico = TextId_Prd.Text == string.Empty ? 0 : Convert.ToInt32(TextId_Prd.Text);
            producto.Prd_UniEmp = txtUempaque.Text == string.Empty ? 0 : Convert.ToSingle(txtUempaque.Text);
            producto.Prd_Colo = chkComprasLocales.Checked;
            producto.Prd_Ren = txtRentabilidad.Text.Length > 0 ? txtRentabilidad.Text[0] : ' ';
            producto.Prd_Mes = txtAmortizacion.Text == string.Empty ? 0 : Convert.ToInt32(txtAmortizacion.Text);
            producto.Prd_CLNomFab = txtFnombre.Text;
            producto.Prd_CLIdFab = txtFcodigo.Text;
            producto.Prd_CLDesFab = txtFdescripcion.Text;
            producto.Prd_CLPreFab = txtFpresentacion.Text;
            producto.Prd_CLNomPro = txtPnombre.Text;
            producto.Prd_CLIdPro = txtPcodigo.Text;
            producto.Prd_CLDesPro = txtPdescripcion.Text;
            producto.Prd_CLPrePro = txtPpresentacion.Text;
            producto.Prd_MaxExistencia = txtExistencia.Text == string.Empty ? 0 : Convert.ToInt32(txtExistencia.Text);
            producto.Prd_Ubicacion = txtUbicacion.Text;
            producto.Prd_Contribucion = txtContribucion.Text == string.Empty ? 0 : Convert.ToSingle(txtContribucion.Text);
            producto.Prd_PorUtilidades = txtPorUtilidades.Text == string.Empty ? 0 : Convert.ToSingle(txtPorUtilidades.Text);
            producto.Prd_Nuevo = chkProductoNuevo.Checked;
            producto.Prd_PesConTecnico = txtPesos.Text == string.Empty ? 0 : Convert.ToDouble(txtPesos.Text);
            producto.Prd_CptSv = string.Empty;
            producto.Prd_Activo = chkActivoAbasto.Checked ? 1 : 2;
            producto.Prd_FecAlta = DateTime.Now;
            producto.Prd_Minimo = txtMinCompra.Text == string.Empty ? 0 : Convert.ToInt32(txtMinCompra.Text);
            producto.Prd_PlanAbasto = txtPlanAbasto.Text;




            producto.ListaProductoPrecios = this.listSource;

            return producto;
        }

        private Producto LlenatObjetoProductoSolicitud()
        {
            Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
            Producto producto = new Producto();

            producto.Id_Emp = session.Id_Emp;
            producto.Id_Cd = session.Id_Cd_Ver;
            producto.Id_Prd = Convert.ToInt64(this.txtCodProdCte.Value);
            producto.Id_Ptp = txtTipoProductoCte.Text == string.Empty ? 0 : Convert.ToInt32(txtTipoProductoCte.Text);
            producto.Prd_Descripcion = TextPrd_DescrpcionCte.Text;
            producto.Prd_Presentacion = txtPresentacionCte.Text;
            producto.Prd_Activo = chkActivoAbasto.Checked ? 1 : 2;
            producto.Prd_Nuevo = chkProdNuevoCte.Checked;
            //  rdpVigencia.SelectedDate;       ver donde se guarda este campo de inicio de vigencia

            producto.Id_Spo = TextId_SpoCte.Text == string.Empty ? 0 : Convert.ToInt32(TextId_SpoCte.Text);
            producto.Id_Cpr = txtCategoriaCte.Text == string.Empty ? 0 : Convert.ToInt32(txtCategoriaCte.Text);
            producto.Id_Fam = txtFamCte.Value.HasValue ? Convert.ToInt32(txtFamCte.Text) : 0;
            producto.Id_Sub = txtSubFamCte.Value.HasValue ? Convert.ToInt32(txtSubFamCte.Text) : 0;
            producto.Id_Pvd = txtProveedorCte.Text == string.Empty ? 0 : Convert.ToInt32(txtProveedorCte.Text);
            producto.Prd_UniNe = cmbUentradaCte.SelectedValue.ToString().Trim() == "-1" ? string.Empty : cmbUentradaCte.SelectedValue;
            producto.Prd_FactorConv = txtFactorConversionCte.Text == string.Empty ? 0 : Convert.ToSingle(txtFactorConversionCte.Text);
            producto.Prd_UniNs = cmbUsalidaCte.SelectedValue.ToString().Trim() == "-1" ? string.Empty : cmbUsalidaCte.SelectedValue;
            producto.Prd_UniEmp = txtUempaqueCte.Text == string.Empty ? 0 : Convert.ToSingle(txtUempaqueCte.Text);

            producto.Prd_InvSeg = txtInvSeguridadCte.Text == string.Empty ? 0 : Convert.ToInt32(txtInvSeguridadCte.Text);
            producto.Prd_AparatoSisProp = chkSistPropCte.Checked;
            producto.Prd_TEntre = txtTentregaCte.Text == string.Empty ? 0 : Convert.ToInt32(txtTentregaCte.Text);
            producto.Prd_PlanAbasto = txtPlanAbastoCte.Text;
            producto.Prd_Minimo = txtMinCompraCte.Text == string.Empty ? 0 : Convert.ToInt32(txtMinCompraCte.Text);
            producto.Prd_TTrans = txtTtransporteCte.Text == string.Empty ? 0 : Convert.ToInt32(txtTtransporteCte.Text);
            producto.Prd_Colo = chkComprasLocalesCte.Checked;
            producto.Prd_Mes = txtAmortizacionCte.Text == string.Empty ? 0 : Convert.ToInt32(txtAmortizacionCte.Text);
            producto.Prd_PesConTecnico = txtPesosCte.Text == string.Empty ? 0 : Convert.ToDouble(txtPesosCte.Text);
            producto.Prd_MaxExistencia = txtExistenciaCte.Text == string.Empty ? 0 : Convert.ToInt32(txtExistenciaCte.Text);
            producto.Prd_Ubicacion = txtUbicacionCte.Text;
            //  txtMotivoSolicita   Ver donde se va aguardar este campo

            producto.Prd_Transito = txtTransitoCte.Text == string.Empty ? 0 : Convert.ToInt32(txtTransitoCte.Text);
            producto.Prd_Asignado = txtAsignadoCte.Text == string.Empty ? 0 : Convert.ToInt32(txtAsignadoCte.Text);
            producto.Prd_InvInicial = txtInicialCte.Text == string.Empty ? 0 : Convert.ToInt32(txtInicialCte.Text);
            producto.Prd_Ordenado = txtOrdenadoCte.Text == string.Empty ? 0 : Convert.ToInt32(txtOrdenadoCte.Text);
            producto.Prd_InvFinal = txtFinalCte.Text == string.Empty ? 0 : Convert.ToInt32(txtFinalCte.Text);
            producto.Prd_Transito = txtTransitoCte.Text == string.Empty ? 0 : Convert.ToInt32(txtTransitoCte.Text);
            producto.Prd_Fisico = txtFisicoCte.Text == string.Empty ? 0 : Convert.ToInt32(txtFisicoCte.Text);

            producto.Prd_CLNomFab = txtFnombreCte.Text;
            producto.Prd_CLIdFab = txtFcodigoCte.Text;
            producto.Prd_CLDesFab = txtFdescripcionCte.Text;
            producto.Prd_CLPreFab = txtFpresentacionCte.Text;
            producto.Prd_CLNomPro = txtPnombreCte.Text;
            producto.Prd_CLIdPro = txtPcodigoCte.Text;
            producto.Prd_CLDesPro = txtPdescripcionCte.Text;
            producto.Prd_CLPrePro = txtPpresentacionCte.Text;

            producto.Prd_AgrupadoSpo = 0;
            producto.Prd_Unico = Convert.ToInt32(TextId_PrdCte.Text);    //  .Substring(10, TextId_PrdCte.Text.Length - 10) );
            producto.Prd_Ren = ' ';
            producto.Prd_Contribucion = 0;
            producto.Prd_PorUtilidades = 0;
            producto.Prd_CptSv = string.Empty;

            producto.Prd_FecAlta = DateTime.Now;        //guardar aqui la vigencia???

            producto.ListaProductoPrecios = this.listSource;

            return producto;
        }

        private Producto LlenatObjetoProductoSolicitudConsulta()
        {
            Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
            Producto producto = new Producto();

            producto.Id_Emp = session.Id_Emp;
            producto.Id_Cd = session.Id_Cd_Ver;
            producto.Id_Prd = Convert.ToInt64(this.TextId_PrdCons.Text);
            producto.Id_Ptp = txtTipoProductoCons.Text == string.Empty ? 0 : Convert.ToInt32(txtTipoProductoCons.Text);
            producto.Prd_Descripcion = TextPrd_DescrpcionCons.Text;
            producto.Prd_Presentacion = txtPresentacionCons.Text;
            producto.Prd_Activo = chkActivoAbasto.Checked ? 1 : 2;
            producto.Prd_Nuevo = chkProdNuevoCons.Checked;

            producto.Id_Spo = TextId_SpoCons.Text == string.Empty ? 0 : Convert.ToInt32(TextId_SpoCons.Text);
            producto.Id_Cpr = txtCategoriaCons.Text == string.Empty ? 0 : Convert.ToInt32(txtCategoriaCons.Text);
            producto.Id_Fam = txtFamCons.Value.HasValue ? Convert.ToInt32(txtFamCons.Text) : 0;
            producto.Id_Sub = txtSubFamCons.Value.HasValue ? Convert.ToInt32(txtSubFamCons.Text) : 0;
            producto.Id_Pvd = txtProveedorCons.Text == string.Empty ? 0 : Convert.ToInt32(txtProveedorCons.Text);
            producto.Prd_UniNe = cmbUentradaCons.SelectedValue.ToString().Trim() == "-1" ? string.Empty : cmbUentradaCons.SelectedValue;
            producto.Prd_FactorConv = txtFactorConversionCons.Text == string.Empty ? 0 : Convert.ToSingle(txtFactorConversionCons.Text);
            producto.Prd_UniNs = cmbUsalidaCons.SelectedValue.ToString().Trim() == "-1" ? string.Empty : cmbUsalidaCons.SelectedValue;
            producto.Prd_UniEmp = txtUempaqueCons.Text == string.Empty ? 0 : Convert.ToSingle(txtUempaqueCons.Text);

            producto.Prd_AgrupadoSpo = 0;
            producto.Prd_Unico = Convert.ToInt32(TextId_PrdCons.Text.Substring(10, TextId_PrdCons.Text.Length - 10));
            producto.Prd_Ren = ' ';
            producto.Prd_Contribucion = 0;
            producto.Prd_PorUtilidades = 0;
            producto.Prd_CptSv = string.Empty;

            producto.Prd_FecAlta = DateTime.Now;

            producto.ListaProductoPrecios = this.listSource;

            return producto;
        }

        private void IniciaConsulta()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            this.LlenarComboProductoTipo(ref cmbTipoProductoCons);
            this.LlenarComboSisPropietario(ref cmbSisPropCons);
            this.LlenarComboProductoCategoria(ref cmbCategoriaCons);
            this.LlenarComboProductoFamiliaCte(ref cmbFamCons);
            this.LlenarComboProveedores(0, ref cmbProveedorCons);
            this.LlenarComboUnidades(ref cmbUentradaCons);
            this.LlenarComboUnidades(ref cmbUsalidaCons);

            System.Text.StringBuilder str = new System.Text.StringBuilder();
            List<SubFamProducto> listaSF = new List<SubFamProducto>();
            SubFamProducto subFamilia = new SubFamProducto();
            new CN_CatSubFamProducto().ConsultaSubFamProductoCte(subFamilia, sesion.Emp_Cnx, sesion.Id_Emp, ref listaSF);
            str.Append(string.Concat("arregloSubFamiliasCons = new Array(3);"));
            str.Append(string.Concat("arregloSubFamiliasCons[0] = new Array(", listaSF.Count, ");"));
            str.Append(string.Concat("arregloSubFamiliasCons[1] = new Array(", listaSF.Count, ");"));
            str.Append(string.Concat("arregloSubFamiliasCons[2] = new Array(", listaSF.Count, ");"));
            for (int i = 0; i < listaSF.Count; i++)
            {
                subFamilia = listaSF[i];
                str.Append(string.Concat("arregloSubFamiliasCons[0][", i.ToString(), "] = '", subFamilia.Id_Fam, "';"));
                str.Append(string.Concat("arregloSubFamiliasCons[1][", i.ToString(), "] = '", subFamilia.Id_Sub, "';"));
                str.Append(string.Concat("arregloSubFamiliasCons[2][", i.ToString(), "] = '", subFamilia.Sub_Descripcion, "';"));
            }

            if (Session["IdCategoria" + Session.SessionID] != null)
            {
                int? index = cmbCategoriaCons.FindItemIndexByValue(Session["IdCategoria" + Session.SessionID].ToString());
                cmbCategoriaCons.SelectedIndex = index != null ? (int)index : 0;
                cmbCategoriaCons.Text = cmbCategoriaCons.Items[cmbCategoriaCons.SelectedIndex].Text;
                if (cmbCategoriaCons.SelectedIndex > 0)
                {
                    cmbCategoriaCons.Text = Session["IdCategoria" + Session.SessionID].ToString();
                }
            }

            RAM1.ResponseScripts.Add(string.Concat(@"", str.ToString()));
        }

        private void InicializaSolicitud()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            this.LlenarComboCausaDesabasto(ref cmbCausaDesabasto);
            cmbCausaDesabasto.SelectedIndex = cmbCausaDesabasto.FindItemIndexByValue("-1");
            this.LlenarComboProductoTipo(ref cmbTipoProductoCte);
            this.LlenarComboSisPropietario(ref cmbSisPropCte);
            this.LlenarComboProductoCategoria(ref cmbCategoriaCte);
            this.LlenarComboProductoFamiliaCte(ref cmbFamCte);
            this.LlenarComboProveedores(0, ref cmbProveedorCte);
            this.LlenarComboUnidades(ref cmbUentradaCte);
            this.LlenarComboUnidades(ref cmbUsalidaCte);

            lstbPedidos.Items.Clear();


            this.listSource = new List<ProductoPrecios>();

            System.Text.StringBuilder str = new System.Text.StringBuilder();
            List<SubFamProducto> listaSF = new List<SubFamProducto>();
            SubFamProducto subFamilia = new SubFamProducto();
            new CN_CatSubFamProducto().ConsultaSubFamProductoCte(subFamilia, sesion.Emp_Cnx, sesion.Id_Emp, ref listaSF);
            str.Append(string.Concat("arregloSubFamiliasCte = new Array(3);"));
            str.Append(string.Concat("arregloSubFamiliasCte[0] = new Array(", listaSF.Count, ");"));
            str.Append(string.Concat("arregloSubFamiliasCte[1] = new Array(", listaSF.Count, ");"));
            str.Append(string.Concat("arregloSubFamiliasCte[2] = new Array(", listaSF.Count, ");"));
            for (int i = 0; i < listaSF.Count; i++)
            {
                subFamilia = listaSF[i];
                str.Append(string.Concat("arregloSubFamiliasCte[0][", i.ToString(), "] = '", subFamilia.Id_Fam, "';"));
                str.Append(string.Concat("arregloSubFamiliasCte[1][", i.ToString(), "] = '", subFamilia.Id_Sub, "';"));
                str.Append(string.Concat("arregloSubFamiliasCte[2][", i.ToString(), "] = '", subFamilia.Sub_Descripcion, "';"));
            }

            if (Session["IdCategoria" + Session.SessionID] != null)
            {
                int? index = cmbCategoriaCte.FindItemIndexByValue(Session["IdCategoria" + Session.SessionID].ToString());
                cmbCategoriaCte.SelectedIndex = index != null ? (int)index : 0;
                cmbCategoriaCte.Text = cmbCategoriaCte.Items[cmbCategoriaCte.SelectedIndex].Text;
                if (cmbCategoriaCte.SelectedIndex > 0)
                {
                    txtCategoriaCte.Text = Session["IdCategoria" + Session.SessionID].ToString();
                }
            }
            RAM1.ResponseScripts.Add(string.Concat(@"", str.ToString()));
        }

        private void Inicializar()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            this.CargarCentros();
            this.LlenarComboProductoTipo(ref this.cmbTipoProducto);
            this.LlenarComboRentabilidad(ref this.cmbRentabilidad);
            this.LlenarComboSisPropietario(ref this.cmbSisProp);
            this.LlenarComboProductoCategoria(ref this.cmbCategoria);
            this.LlenarComboProductoFamiliaCte(ref this.cmbFam);
            this.LlenarComboProveedores(0, ref this.cmbProveedor);
            this.LlenarComboUnidades(ref this.cmbUentrada);
            this.LlenarComboUnidades(ref this.cmbUsalida);

            this.hiddenId.Value = string.Empty;

            System.Text.StringBuilder str = new System.Text.StringBuilder();
            List<SubFamProducto> listaSF = new List<SubFamProducto>();
            SubFamProducto subFamilia = new SubFamProducto();
            new CN_CatSubFamProducto().ConsultaSubFamProductoCte(subFamilia, sesion.Emp_Cnx, sesion.Id_Emp, ref listaSF);
            str.Append(string.Concat("arregloSubFamilias = new Array(3);"));
            str.Append(string.Concat("arregloSubFamilias[0] = new Array(", listaSF.Count, ");"));
            str.Append(string.Concat("arregloSubFamilias[1] = new Array(", listaSF.Count, ");"));
            str.Append(string.Concat("arregloSubFamilias[2] = new Array(", listaSF.Count, ");"));
            for (int i = 0; i < listaSF.Count; i++)
            {
                subFamilia = listaSF[i];
                str.Append(string.Concat("arregloSubFamilias[0][", i.ToString(), "] = '", subFamilia.Id_Fam, "';"));
                str.Append(string.Concat("arregloSubFamilias[1][", i.ToString(), "] = '", subFamilia.Id_Sub, "';"));
                str.Append(string.Concat("arregloSubFamilias[2][", i.ToString(), "] = '", subFamilia.Sub_Descripcion, "';"));
            }
            this.listSource = new List<ProductoPrecios>();

            this.NuevoProducto();
            if (Session["IdLocal" + Session.SessionID] != null)
            {
                this.TextId_Prd.Text = Session["IdLocal" + Session.SessionID].ToString();
                TextId_Prd.Enabled = false;
                chkComprasLocales.Checked = true;
                Session["IdLocal" + Session.SessionID] = null;

                if (Session["IdCategoria" + Session.SessionID] != null)
                {
                    int? index = cmbCategoria.FindItemIndexByValue(Session["IdCategoria" + Session.SessionID].ToString());
                    cmbCategoria.SelectedIndex = index != null ? (int)index : 0;
                    cmbCategoria.Text = cmbCategoria.Items[cmbCategoria.SelectedIndex].Text;
                    if (cmbCategoria.SelectedIndex > 0)
                    {
                        txtCategoria.Text = Session["IdCategoria" + Session.SessionID].ToString();
                    }
                }

                // Como es alta de producto de compra local
                // se habilitan controles de la pestaña de compras locales
                this.HabilitaCamposComprasLocales(true);
            }
            else
            { // Como es alta de producto normal
                // se deshabilitan controles de la pestaña de compras locales (solo se habilitan cuando se generó previamente un código para producto local)
                this.HabilitaCamposComprasLocales(false);
            }

            lblTituloProducto.Text = string.Empty;

            if ((txtCodProd.Text != string.Empty) && (TextPrd_Descrpcion.Text != string.Empty))
            {
                lblTituloProducto.Text = string.Concat(txtCodProd.Text, " - ", TextPrd_Descrpcion.Text);
            }

        }

        private void LlenarProdcutosHermanos(long Producccto, ref TextBox txttboxx)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            string listadito = "";

            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Producto" };
                object[] Valores = { Producccto };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCompraLocalProductosHermanos_Lista", ref dr, Parametros, Valores);

                if (!dr.HasRows)
                {
                    txttboxx.Text = "";
                }
                else
                    while (dr.Read())
                    {
                        listadito = listadito + dr["Id_Producto"].ToString() + ";";
                    }

                txttboxx.Text = listadito;

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void LlenarListaPedidos(int Solicitud, long Producto)   //  , ref ListBox listado
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            string Values = "";
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Solicitud", "@Id_Producto" };
                object[] Valores = { Solicitud, Producto };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCompraLocalPedidosProducto_Lista", ref dr, Parametros, Valores);

                if (!dr.HasRows)
                {
                    //  listado.Items.Clear();
                    chklstPedidos.Items.Clear();
                    chklstPedidosCons.Items.Clear();
                }
                else
                    while (dr.Read())
                    {
                        ListItem item = new ListItem();
                        item.Text = dr["Id_Pedido"].ToString() + " | " + dr["Ped_FechaEntrega"].ToString();
                        item.Value = dr["Id_Pedido"].ToString();

                        //  listado.Items.Add(item);
                        chklstPedidos.Items.Add(item);
                        chklstPedidosCons.Items.Add(item);

                        Values = Values + item.Value + ";";
                    }
                //  listatxt.Text = Values;
                //  listado.DataBind();

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void SeleccionaPedidodeListaPedidos(int Solicitud, long Producto, ref ListBox listado)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            string Values = "";
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Solicitud" };
                object[] Valores = { Solicitud };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCompraLocalPedidoReferencia_Consultar", ref dr, Parametros, Valores);

                if (!dr.HasRows)
                {
                    listado.Items.Clear();
                }
                else
                {
                    if (chklstPedidosCons.Items.Count != 0)
                    {
                        while (dr.Read())
                        {
                            //  listado.SelectedValue = dr["PedidoReferencia"].ToString();
                            Values = dr["PedidoReferencia"].ToString();
                        }


                        string[] Value1 = Values.Split(',');
                        foreach (string Value0 in Value1)
                        {
                            foreach (ListItem itemCh in this.chklstPedidosCons.Items)
                            {
                                if (itemCh.Value == Value0)
                                {
                                    itemCh.Selected = true;
                                }
                            }
                        }
                    }
                }

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GrabaPedidodeListaPedidos(int Solicitud, long Producto, TextBox listado)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            string Values = listado.Text;
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(sesion.Emp_Cnx);

                string[] Parametros = { "@Id_Solicitud", "@Id_Producto", "@PedidoReferencia" };
                object[] Valores = { Solicitud, Producto, Values };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spCompraLocalPedidoReferencia_Grabar", ref dr, Parametros, Valores);

                CapaDatos.LimpiarSqlcommand(ref sqlcmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void LlenarComboCausaDesabasto(ref RadComboBox combo)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(sesion.Emp_Cnx, "spCompraLocalCausaDesabasto_Combo", ref combo);
            combo.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
        }

        private void LlenarComboProductoTipo(ref RadComboBox combo)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Emp_Cnx, "spCatTipoProducto_Combo", ref combo);
        }

        private void LlenarComboSisPropietario(ref RadComboBox combo)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Emp_Cnx, "spCatSisPropietarios_Combo", ref combo);
        }

        private void LlenarComboRentabilidad(ref RadComboBox combo)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Emp_Cnx, "spCatRentabilidad_Combo", ref combo);
            combo.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
        }

        private void LlenarComboProductoCategoria(ref RadComboBox combo)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Emp_Cnx, "spCatProductoCategoria_Combo", ref combo);
            combo.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
        }

        private void LlenarComboProductoFamilia(ref RadComboBox combo)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Emp_Cnx, "spCatAplicacion_Combo", ref combo);
            combo.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
        }

        private void LlenarComboProductoFamiliaCte(ref RadComboBox combo)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Emp_Cnx, "spCatAplicacion_Combo", ref combo);
            combo.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
        }

        private void LlenarComboProveedores(int pEmp, ref RadComboBox combo)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            if (pEmp == 0)
            {
                pEmp = sesion.Id_Emp;
            }
            /// revisar aqui si se puede mandar el 2do parametro como 1 para q traiga todos los proveedores
            CN_Comun.LlenaCombo(1, pEmp, sesion.Emp_Cnx, "spProveedores_ComboCompraLocal", ref combo);
        }

        private void LlenarComboUnidades(ref RadComboBox combo)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(1, sesion.Id_Emp, sesion.Emp_Cnx, "spCatUnidad_Combo", ref combo, true);
            combo.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
        }

        protected void ActualizaTItuloAba(object sender, EventArgs e)
        {
            if (this.hidRpoveedorOriginal.Value != this.txtProveeAba.Text)
            {
                if (hidProductoOriginal.Value == "0")
                {
                    hidProductoOriginal.Value = lblId_Prd.Text;
                }

                lblId_Prd.Text = this.MaximoId(Convert.ToInt32(hidProductoOriginal.Value), this.txtProveeAba.Text);      //   this.Valor;
                lblCodProd.Text = lblId_Prd.Text;
                lblTituloProducto.Text = string.Concat(lblId_Prd.Text, " - ", lblPrd_Descrpcion.Text);
                NoProveedorFijado = true;
            }
        }

        protected void EnviarSolicitudAbasto(object sender, EventArgs e)
        {
            /*
            foreach (GridDataItem dataItem in rgPrecios.MasterTableView.Items)
            {
                if (Convert.ToInt32(dataItem["Id_Pre"].Text) == 1 && Convert.ToBoolean(dataItem["Prd_Actual"].Text) == true)
                {
                    if (Convert.ToDouble((((Label)dataItem.FindControl("lblPrd_Pesos")).Text)) == 0)
                    {
                        Alerta("El precio AAA actual no puede ser 0");
                        return;
                    }
                }
                if (Convert.ToInt32(dataItem["Id_Pre"].Text) == 2 && Convert.ToBoolean(dataItem["Prd_Actual"].Text) == true)
                {
                    if (Convert.ToDouble((((Label)dataItem.FindControl("lblPrd_Pesos")).Text)) == 0)
                    {
                        Alerta("El precio LISTA actual no puede ser 0");
                        return;
                    }
                }
                if (Convert.ToInt32(dataItem["Id_Pre"].Text) == 3 && Convert.ToBoolean(dataItem["Prd_Actual"].Text) == true)
                {
                    if (Convert.ToDouble((((Label)dataItem.FindControl("lblPrd_Pesos")).Text)) == 0)
                    {
                        Alerta("El precio COSTO actual no puede ser 0");
                        return;
                    }
                }

            }
            */

            string providerName = this.cmbProveedorAba.Text;  // Request.Form[txtSearchProvAbasto.UniqueID];
            string providerId = Request.Form[hfProviderAbastoId.UniqueID];
            string productName = Request.Form[txtPnombreAbasto.UniqueID];

            try
            {

                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                if (session == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }

                CN_CatProducto clsCatProducto = new CN_CatProducto();
                CN_ComprasLocales clLocales = new CN_ComprasLocales();
                int verificador = -1;
                int grabo = 0;
                Producto producto = this.LlenatObjetoProductoAbasto();
                Producto productoOriginal = new Producto();

                int famk = 0;
                if (this.hddFamiliaAbasto.Value != "")
                {
                    famk = Convert.ToInt32(this.hddFamiliaAbasto.Value);
                }


                string Fmilia = "";
                Fmilia = this.lblcmbFam.Text;
                string SbFmilia = "";
                SbFmilia = this.lblcmbSubFam.Text;

                // Ajuste del codigo del producto pues cambiaron de proveedor
                if (this.hidRpoveedorOriginal.Value != this.txtProveeAba.Text)
                {
                    if (NoProveedorFijado == false)
                    {
                        if (hidProductoOriginal.Value == "0")
                        {
                            hidProductoOriginal.Value = lblId_Prd.Text;
                        }

                        lblId_Prd.Text = this.MaximoId(Convert.ToInt32(hidProductoOriginal.Value), this.txtProveeAba.Text);      //   this.Valor;
                        lblCodProd.Text = lblId_Prd.Text;
                        lblTituloProducto.Text = string.Concat(lblId_Prd.Text, " - ", lblPrd_Descrpcion.Text);
                        // se graba el nuevoproducto
                    }
                    producto.Id_Prd = Convert.ToInt64(lblId_Prd.Text);
                    producto.Prd_Unico = Convert.ToInt32(hidProductoOriginal.Value);


                    int ok = 0;

                    ChecarProductoYaSolicitado(producto.Id_Prd, ref ok);
                    if (ok != 0)
                    {
                        Alerta("Ya se encuentra una solicitud pendiente de autorizar para ese producto.");
                        return;
                    }

                    clsCatProducto.InsertarProducto_CL(producto, session.Emp_Cnx, ref verificador);

                    productoOriginal = ConsultarPorducto(Convert.ToInt32(hidProductoOriginal.Value), session.Id_Cd_Ver);
                    productoOriginal.ListaProductoPrecios = this.ConsultarPorductoPrecios(Convert.ToInt32(hidProductoOriginal.Value));

                    clLocales.ProductosClonados(verificador, Convert.ToInt32(hidProductoOriginal.Value), Convert.ToInt64(lblId_Prd.Text), session.Emp_Cnx);

                }

                foreach (GridDataItem dataItem in this.rgPreciosAbasto.MasterTableView.Items)
                {
                    if (Convert.ToInt32(dataItem["Id_Pre"].Text) == 1 && Convert.ToBoolean(dataItem["Prd_Actual"].Text) == true)
                    {
                        if (Convert.ToDouble((((Label)dataItem.FindControl("lblPrd_PesosAba")).Text)) == 0)
                        {
                            Alerta("El precio AAA actual no puede ser 0");
                            return;
                        }
                    }
                }
                // producto.Id_Prd = Convert.ToInt32(lblId_Prd.Text);

                if ((producto.Prd_Descripcion != string.Empty) && (producto.Id_Prd.ToString() != string.Empty))
                {
                    lblTituloProducto.Text = string.Concat(producto.Id_Prd.ToString(), " - ", producto.Prd_Descripcion);
                }


                /// Graba la Solicitud en si, cada solicitud va con un producto por vuelta.
                Funciones funciones = new Funciones();
                CompraLocal cl = new CompraLocal();
                cl.Id_Emp = session.Id_Emp;
                cl.Id_Cd = session.Id_Cd_Ver;
                cl.FechaSol = funciones.GetLocalDateTime(session.Minutos);
                cl.Comp_Solicito = session.Id_U;
                verificador = 0;
                CN_ProCompraLocal cn_procompralocal = new CN_ProCompraLocal();

                /// se llena un dataset con el dato del producto nuevo para enviar a generar la solicitud
                Creardt();
                /// // 1 producto = 1 solicitud
                foreach (ProductoPrecios productoPrecios in producto.ListaProductoPrecios)
                {
                    if (productoPrecios.Pre_Descripcion == "COSTO" && productoPrecios.Prd_Actual == true)
                    {
                        dt.Rows.Add(new object[] { producto.Id_Prd, producto.Prd_Descripcion, Convert.ToDouble(productoPrecios.Prd_Pesos).ToString("#,##0.00"), 0, "Sin autorizar" });
                    }
                }

                cn_procompralocal.InsertarSolicitud(cl, dt, session.Emp_Cnx, ref verificador);

                hfNumSolicitudAbasto.Value = verificador.ToString();

                CN_ComprasLocales comLoc = new CN_ComprasLocales();
                comLoc.GrabaTipoCompraLocal(verificador, 2, session.Emp_Cnx, ref grabo);

                /// Guarda los datos de SAT
                string CveUnidad = cmbUnidadMedidaSATAbasto.SelectedValue;
                string CveProducto = this.hfCveProdServSAT.Value;       //  cmbProdServicioSATAbasto.SelectedValue;
                clLocales.GrabaDatosProductoSAT(verificador, producto.Id_Prd, CveUnidad, CveProducto, session.Emp_Cnx);

                //// Envia el correo

                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                verificador = -1;
                EntradaVirtual pe = new EntradaVirtual();

                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Cd = Sesion.Id_Cd_Ver;
                configuracion.Id_Emp = Sesion.Id_Emp;
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                cn_configuracion.Consulta(ref configuracion, Sesion.Emp_Cnx);

                lblNumSolicitudAbasto.Text = "Solicitud " + hfNumSolicitudAbasto.Value.ToString() + " Generada!!";

                StringBuilder cuerpo_correo = new StringBuilder();
                cuerpo_correo.Append("<div align='center'>");
                cuerpo_correo.Append("<table width='800px'><tr><td>");
                cuerpo_correo.Append("<IMG SRC=\"cid:companylogo\" ALIGN='left'></td>");
                cuerpo_correo.Append("</tr><tr><td colspan='2'><br></td>");
                cuerpo_correo.Append("</tr><tr>");
                cuerpo_correo.Append("<td colspan='2' style='font-family:Calibri,sans-serif;mso-bidi-font-family:Calibri; color:black'><br><br><b>");
                cuerpo_correo.Append("Se ha colocado una solicitud de compra local con el folio <b>" + hfNumSolicitudAbasto.Value + "</b><br>");
                cuerpo_correo.Append("para el proveedor <b>" + providerName + "</b><br>");
                cuerpo_correo.Append("del producto <b>" + producto.Prd_Descripcion + "</b><br>");
                cuerpo_correo.Append("para el centro de distribucion <b>" + session.Id_Cd + " - " + session.Cd_Nombre + "</b><br>");
                cuerpo_correo.Append("generada por <b>" + session.Id_Emp + " - " + session.Emp_Nombre + "</b>");
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");


                cuerpo_correo.Append("<table width='90%' style='font-family:Calibri,sans-serif;mso-bidi-font-family:Calibri;color:black' border=0 cellpadding=1>");
                cuerpo_correo.Append("<tr><td>Código del producto:</td><td colspan='4'>" + producto.Id_Prd + "</td></tr>");
                cuerpo_correo.Append("<tr><td>Descripción del producto:</td><td colspan='4'>" + producto.Prd_Descripcion + "</td></tr>");
                //  cuerpo_correo.Append("<tr><td>Presentación:</td><td>" + producto.Prd_Presentacion + "</td><td colspan='3'>&nbsp;</td></tr>");
                cuerpo_correo.Append("<tr><td>Tipo de producto:</td><td colspan='3'>" + lblcmbTipoProducto.Text + "</td><td>&nbsp;</td></tr>");

                cuerpo_correo.Append("<tr><td>Familia del producto:</td><td colspan='4'>" + Fmilia + "</td></tr>");
                cuerpo_correo.Append("<tr><td>SubFamilia del producto:</td><td colspan='4'>" + SbFmilia + "</td></tr>");

                cuerpo_correo.Append("<tr><td>Precios:</td><td colspan='4'>&nbsp;</td></tr><tr><td colspan='4' align=center>");

                string tablaprecios = "";
                string color = "";
                int reng = 1;
                string AAAO = "";
                string PublicoO = "";

                //                string tablaprecios = "<table style='font-family: verdana; font-size: 8pt;' cellspacing=0 border=1 cellpadding=0 width=250px>";
                //                tablaprecios = tablaprecios + "<tr><td style='background-color:#d9d9d9' ><b>Tipo de Precio</b></td><td style='background-color:#d9d9d9' align=right><b>Precio</b></td></tr>";

                if (this.hidRpoveedorOriginal.Value != this.txtProveeAba.Text)
                {
                    tablaprecios = "<table style='font-family: verdana; font-size: 8pt;' cellspacing=0 border=1 cellpadding=0 width=250px>";
                    tablaprecios = tablaprecios + "<tr><td style='background-color:#d9d9d9' ><b>Codigos:&nbsp;</b></td><td style='background-color:#d9d9d9' ><b>";
                    tablaprecios = tablaprecios + producto.Id_Prd + "&nbsp;</b></td><td style='background-color:#d9d9d9' align=right><b>";
                    tablaprecios = tablaprecios + productoOriginal.Id_Prd + "&nbsp;</b></td></tr>";

                    // saca los datos de los precios originales..
                    foreach (ProductoPrecios productoPrecios2 in productoOriginal.ListaProductoPrecios)
                    {
                        /// Evaluar el tipo para eliminar el Costo Original
                        if ((productoPrecios2.Pre_Descripcion == "AAA") && (productoPrecios2.Prd_Actual == true))
                        {
                            AAAO = productoPrecios2.Prd_Pesos.ToString("#,##0.00");
                        }

                        if ((productoPrecios2.Pre_Descripcion == "PUBLICO") && (productoPrecios2.Prd_Actual == true))
                        {
                            PublicoO = productoPrecios2.Prd_Pesos.ToString("#,##0.00");
                        }
                    }

                    foreach (ProductoPrecios productoPrecios in producto.ListaProductoPrecios)
                    {
                        if (productoPrecios.Prd_Actual == true)
                        {
                            if (reng == 0)
                            {
                                reng = 1;
                                color = "style='background-color:#d9d9d9'";
                            }
                            else
                            {
                                reng = 0;
                                color = "";
                            }

                            tablaprecios = tablaprecios + "<tr><td " + color + ">" + productoPrecios.Pre_Descripcion + "</td>";
                            tablaprecios = tablaprecios + "<td align=right " + color + ">" + Convert.ToDouble(productoPrecios.Prd_Pesos).ToString("#,##0.00") + "</td>";

                            if (productoPrecios.Pre_Descripcion == "COSTO")
                            {
                                tablaprecios = tablaprecios + "<td align=right " + color + ">" + "█████████" + "</td>";
                            }

                            if (productoPrecios.Pre_Descripcion == "AAA")
                            {
                                tablaprecios = tablaprecios + "<td align=right " + color + ">" + Convert.ToDouble(AAAO).ToString("#,##0.00") + "</td>";
                            }

                            if (productoPrecios.Pre_Descripcion == "PUBLICO")
                            {
                                tablaprecios = tablaprecios + "<td align=right " + color + ">" + Convert.ToDouble(PublicoO).ToString("#,##0.00") + "</td>";
                            }


                            tablaprecios = tablaprecios + "</tr>";
                        }
                    }
                    tablaprecios = tablaprecios + "</table>";
                }
                else
                {

                    foreach (ProductoPrecios productoPrecios in producto.ListaProductoPrecios)
                    {
                        if (productoPrecios.Prd_Actual == true)
                        {
                            if (reng == 0)
                            {
                                reng = 1;
                                color = "style='background-color:#d9d9d9'";
                            }
                            else
                            {
                                reng = 0;
                                color = "";
                            }

                            tablaprecios = tablaprecios + "<tr><td " + color + ">" + productoPrecios.Pre_Descripcion + "</td>";
                            tablaprecios = tablaprecios + "<td align=right " + color + ">" + Convert.ToDouble(productoPrecios.Prd_Pesos).ToString("#,##0.00") + "</td></tr>";
                        }
                    }
                    tablaprecios = tablaprecios + "</table>";
                }

                string[] url = Request.Url.ToString().Split(new char[] { '/' });

                cuerpo_correo.Append(tablaprecios);
                cuerpo_correo.Append("</td></tr>");
                cuerpo_correo.Append("</table>");
                cuerpo_correo.Append("</td></tr><tr><td colspan='2' align=center>");
                cuerpo_correo.Append("<br>");
                cuerpo_correo.Append("<a style='font-family: verdana; font-size: 8pt;' href='" + Request.Url.ToString().Replace(url[url.Length - 1], "") + "ProCompraLocal_Autorizacion.aspx?id1=" + hfNumSolicitudAbasto.Value + "&Id2=" + session.Id_Emp + "&Id3=" + session.Id_Cd_Ver + "'>");

                cuerpo_correo.Append("Solicitud de Compra Local</a>");
                cuerpo_correo.Append("</td></tr></table></div>");
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                sm.EnableSsl = true;
                MailMessage m = new MailMessage();
                string[] eVirtual = configuracion.Mail_EVirtual.Split(',');
                m.From = new MailAddress(configuracion.Mail_Remitente);

                //m.To.Add(new MailAddress("rafael.garcia@gibraltar.com.mx"));

                string correo = "";

                //  this.CorreosAutorizadorxMotivo(ref correo);
                this.CorreosAutorizadorxMotivoxApp(ref correo, famk);
                string[] eVirtual2 = correo.Split(',');
                reng = 0;

                foreach (string core in eVirtual2)
                {
                    if (core != " ")
                    {
                        if (reng == 0)
                        {
                            m.To.Add(new MailAddress(core));
                            reng = 1;
                        }
                        else
                        {
                            m.CC.Add(new MailAddress(core));
                        }

                    }
                }

                m.Bcc.Add(new MailAddress("rafael.garcia@gibraltar.com.mx"));


                m.Subject = "Solicitud de compra local " + hfNumSolicitudAbasto.Value;
                m.IsBodyHtml = true;
                string body = cuerpo_correo.ToString();

                this.RespaldoCorreo(hfNumSolicitudAbasto.Value, body, correo);

                AlternateView vistaHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                //Esto queda dentro de un try por si llegan a cambiar la imagen el correo como quiera se mande
                try
                {
                    LinkedResource logo = new LinkedResource(MapPath(@"Imagenes/logo.jpg"), MediaTypeNames.Image.Jpeg);
                    logo.ContentId = "companylogo";
                    vistaHtml.LinkedResources.Add(logo);
                }
                catch (Exception)
                {
                }

                m.AlternateViews.Add(vistaHtml);
                try
                {
                    sm.Send(m);
                }
                catch (Exception exx)
                {
                    Alerta("Error al enviar el correo. Favor de revisar la configuración del sistema. " + exx.Message);
                    LimpiarCampos2();
                    return;
                }
                Alerta("Solicitud enviada correctamente");
                LimpiarCampos2();
            }
            catch (Exception ex)
            {
                this.ValidaSolicitud(hfNumSolicitudAbasto.Value);
                throw ex;
            }

        }

        protected void EnviarSolicitudCliente(object sender, EventArgs e)
        {


            /*

            foreach (GridDataItem dataItem in rgPrecios.MasterTableView.Items)
            {
                if (Convert.ToInt32(dataItem["Id_Pre"].Text) == 1 && Convert.ToBoolean(dataItem["Prd_Actual"].Text) == true)
                {
                    if (Convert.ToDouble((((Label)dataItem.FindControl("lblPrd_Pesos")).Text)) == 0)
                    {
                        Alerta("El precio AAA actual no puede ser 0");
                        return;
                    }
                }
                if (Convert.ToInt32(dataItem["Id_Pre"].Text) == 2 && Convert.ToBoolean(dataItem["Prd_Actual"].Text) == true)
                {
                    if (Convert.ToDouble((((Label)dataItem.FindControl("lblPrd_Pesos")).Text)) == 0)
                    {
                        Alerta("El precio LISTA actual no puede ser 0");
                        return;
                    }
                }
                if (Convert.ToInt32(dataItem["Id_Pre"].Text) == 3 && Convert.ToBoolean(dataItem["Prd_Actual"].Text) == true)
                {
                    if (Convert.ToDouble((((Label)dataItem.FindControl("lblPrd_Pesos")).Text)) == 0)
                    {
                        Alerta("El precio COSTO actual no puede ser 0");
                        return;
                    }
                }

            }

            */

            string providerName = cmbProveedorCte.Text;        //    Request.Form[txtSearchProvCte.UniqueID];
            string providerId = Request.Form[hfProviderIdCte.UniqueID];
            string productName = Request.Form[txtPnombreCte.UniqueID];

            try
            {
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                if (session == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }

                CN_CatProducto clsCatProducto = new CN_CatProducto();
                int verificador = -1;

                string Fmilia = "";
                if (cmbFamCte.Items.Count > 0)
                {
                    Fmilia = this.cmbFamCte.SelectedItem.Text;
                }

                string SbFmilia = "";

                if (cmbSubFamCte.Items.Count > 0)
                {
                    SbFmilia = this.cmbSubFamCte.SelectedItem.Text;
                }
                Producto producto = this.LlenatObjetoProductoSolicitud();

                if ((producto.Prd_Descripcion != string.Empty) && (producto.Id_Prd.ToString() != string.Empty))
                {
                    lblTituloProducto.Text = string.Concat(producto.Id_Prd.ToString(), " - ", producto.Prd_Descripcion);
                }

                // graba los cambios que se haigan hecho sobre el producto
                //if (cmbAplicacionSoli.SelectedValue == "0")
                //{
                productName = producto.Prd_Descripcion;
                clsCatProducto.InsertarProducto_CL(producto, session.Emp_Cnx, ref verificador);
                //}
                //else
                //{
                //    clsCatProducto.ModificarProducto_CL(producto, session.Emp_Cnx, ref verificador);
                //}
                /// Graba la Solicitud en si, cada solicitud va con un producto por vuelta.
                Funciones funciones = new Funciones();
                CompraLocal cl = new CompraLocal();
                cl.Id_Emp = session.Id_Emp;
                cl.Id_Cd = session.Id_Cd_Ver;
                cl.FechaSol = funciones.GetLocalDateTime(session.Minutos);
                cl.Comp_Solicito = session.Id_U;
                verificador = 0;
                CN_ProCompraLocal cn_procompralocal = new CN_ProCompraLocal();

                /// se llena un dataset con el dato del producto nuevo para enviar a generar la solicitud
                Creardt();
                /// // 1 producto = 1 solicitud
                foreach (ProductoPrecios productoPrecios in producto.ListaProductoPrecios)
                {
                    if (productoPrecios.Pre_Descripcion == "COSTO" && productoPrecios.Prd_Actual == true)
                    {
                        dt.Rows.Add(new object[] { producto.Id_Prd, producto.Prd_Descripcion, Convert.ToDouble(productoPrecios.Prd_Pesos).ToString("#,##0.00"), 0, "Sin autorizar" });
                    }
                }

                cn_procompralocal.InsertarSolicitud(cl, dt, session.Emp_Cnx, ref verificador);

                hfNumSolicitudCte.Value = verificador.ToString();

                /// graba los comentarios y la fecha de vigencia

                string FVigencia = this.rdpVigencia.ValidationDate.ToString();
                string Comentarios = this.txtMotivoSolicita.Text;
                int grabo = 0;
                CN_ComprasLocales comLoc = new CN_ComprasLocales();

                comLoc.GrabaComentariosCliente(Comentarios, FVigencia, 3, verificador, session.Emp_Cnx, ref grabo);

                /// Guarda los datos de SAT
                string CveUnidad = cmbUnidadMedidaSATCte.SelectedValue;
                string CveProducto = this.hfCveProdServSATCte.Value;    //  cmbProdServicioSATCte.SelectedValue;
                comLoc.GrabaDatosProductoSAT(verificador, producto.Id_Prd, CveUnidad, CveProducto, session.Emp_Cnx);


                /// Graba el grid de clientes exclusivos
                int cliente = 0;

                string[] cadenaclientes = ddlElements.Value.Split(';');

                foreach (string s in cadenaclientes)
                {
                    cliente = Convert.ToInt32(s);
                    comLoc.GrabaClientesExclusivos(producto.Id_Prd, cliente, verificador, session.Emp_Cnx, ref grabo);
                }

                /*
                foreach (GridDataItem dataItem in rgListaCtes.MasterTableView.Items)
                {
                    if (Convert.ToString(dataItem["Id_ClienteListado"].Text) != "")
                    {
                        cliente = Convert.ToInt32(dataItem["Id_ClienteListado"].Text);
                        comLoc.GrabaClientesExclusivos(producto.Id_Prd, cliente, verificador, session.Emp_Cnx, ref grabo);
                    }
                }
                */
                //// Envia el correo

                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                verificador = -1;
                EntradaVirtual pe = new EntradaVirtual();

                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Cd = Sesion.Id_Cd_Ver;
                configuracion.Id_Emp = Sesion.Id_Emp;
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                cn_configuracion.Consulta(ref configuracion, Sesion.Emp_Cnx);

                lblNumSolicitudCte.Text = "Solicitud " + hfNumSolicitudCte.Value.ToString() + " Generada!!";
                //  txtMotivoSolicita  rdpVigencia

                StringBuilder cuerpo_correo = new StringBuilder();
                cuerpo_correo.Append("<div align='center'>");
                cuerpo_correo.Append("<table width='800px'><tr><td>");
                cuerpo_correo.Append("<IMG SRC=\"cid:companylogo\" ALIGN='left'></td>");
                cuerpo_correo.Append("</tr><tr><td colspan='2'><br></td>");
                cuerpo_correo.Append("</tr><tr>");
                cuerpo_correo.Append("<td colspan='2' style='font-family:Calibri,sans-serif;mso-bidi-font-family:Calibri; color:black'><br><br><b>");
                cuerpo_correo.Append("Se ha colocado una solicitud de compra local con el folio <b>" + hfNumSolicitudCte.Value + "</b><br>");
                cuerpo_correo.Append("para el proveedor <b>" + providerName + "</b><br>");
                cuerpo_correo.Append("del producto <b>" + producto.Prd_Descripcion + "</b><br>");
                cuerpo_correo.Append("para el centro de distribucion <b>" + session.Id_Cd + " - " + session.Cd_Nombre + "</b><br>");
                cuerpo_correo.Append("generada por <b>" + session.Id_Emp + " - " + session.Emp_Nombre + "</b>");
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");


                cuerpo_correo.Append("<table width='90%' style='font-family:Calibri,sans-serif;mso-bidi-font-family:Calibri;color:black' border=0 cellpadding=1>");
                cuerpo_correo.Append("<tr><td>Código del producto:</td><td colspan='4'>" + producto.Id_Prd + "</td></tr>");
                cuerpo_correo.Append("<tr><td>Descripción del producto:</td><td colspan='4'>" + producto.Prd_Descripcion + "</td></tr>");
                //  cuerpo_correo.Append("<tr><td>Presentación:</td><td>" + producto.Prd_Presentacion + "</td><td colspan='3'>&nbsp;</td></tr>");
                cuerpo_correo.Append("<tr><td>Tipo de producto:</td><td colspan='3'>" + cmbTipoProductoCte.Text + "</td><td>&nbsp;</td></tr>");

                cuerpo_correo.Append("<tr><td>Familia del producto:</td><td colspan='4'>" + Fmilia + "</td></tr>");
                cuerpo_correo.Append("<tr><td>SubFamilia del producto:</td><td colspan='4'>" + SbFmilia + "</td></tr>");

                cuerpo_correo.Append("<tr><td heigth=30px>&nbsp;</td></tr>");
                if (rdpVigencia.SelectedDate != null)
                {
                    cuerpo_correo.Append("<tr><td>Vigencia de Uso del Codigo:</td><td colspan='3'>" + rdpVigencia.SelectedDate.Value + " </td><td>&nbsp;</td></tr>");
                }
                cuerpo_correo.Append("<tr><td>Motivo de la solicitud:</td><td colspan='3'>" + txtMotivoSolicita.Text.Replace("\r\n", "<br>") + "</td><td>&nbsp;</td></tr>");


                cuerpo_correo.Append("<tr><td>Precios:</td><td colspan='4'>&nbsp;</td></tr><tr><td colspan='4' align=center>");
                string tablaprecios = "<table style='font-family: verdana; font-size: 8pt;' cellspacing=0 border=1 cellpadding=0 width=250px>";
                tablaprecios = tablaprecios + "<tr><td style='background-color:#d9d9d9' ><b>Tipo de Precio</b></td><td align=right style='background-color:#d9d9d9'><b>Precio</b></td></tr>";


                /// // 1 prudcto = 1 solicutd
                string color = "";
                int reng = 1;
                foreach (ProductoPrecios productoPrecios in producto.ListaProductoPrecios)
                {
                    if (productoPrecios.Prd_Actual == true)
                    {
                        if (reng == 0)
                        {
                            reng = 1;
                            color = "style='background-color:#d9d9d9'";
                        }
                        else
                        {
                            reng = 0;
                            color = "";
                        }

                        tablaprecios = tablaprecios + "<tr><td " + color + ">" + productoPrecios.Pre_Descripcion + "</td>";
                        tablaprecios = tablaprecios + "<td align=right " + color + ">" + Convert.ToDouble(productoPrecios.Prd_Pesos).ToString("#,##0.00") + "</td></tr>";
                    }
                }
                tablaprecios = tablaprecios + "</table>";

                string[] url = Request.Url.ToString().Split(new char[] { '/' });

                cuerpo_correo.Append(tablaprecios);
                cuerpo_correo.Append("</td></tr>");

                string TablaClientes = "";
                ///anexa los clientes exlucivos
                if (cliente > 0)
                {
                    TablaClientes = "<tr><td colspan='4'> Exclusivo para los clientes:&nbsp;</td></tr><tr><td colspan='4' align=center>";
                    TablaClientes = TablaClientes + "<table style='font-family: verdana; font-size: 8pt;' cellspacing=0 border=1 cellpadding=0 width=250px>";
                    //  TablaClientes = TablaClientes + "<tr><td style='background-color:#d9d9d9' ></td></tr>";

                    color = "";
                    reng = 1;

                    string[] cadenaclientesFull = ddlElementsFull.Value.Split(';');

                    foreach (string ccte in cadenaclientesFull)
                    {
                        if (reng == 0)
                        {
                            reng = 1;
                            color = "style='background-color:#d9d9d9'";
                        }
                        else
                        {
                            reng = 0;
                            color = "";
                        }

                        TablaClientes = TablaClientes + "<tr><td " + color + ">" + ccte + "</td></tr>";

                    }
                    TablaClientes = TablaClientes + "</table>";

                    cuerpo_correo.Append(TablaClientes);
                }

                this.listaClientes.Items.Clear();

                cuerpo_correo.Append("</table>");
                cuerpo_correo.Append("</td></tr><tr><td colspan='2' align=center>");
                cuerpo_correo.Append("<br>");
                cuerpo_correo.Append("<a style='font-family: verdana; font-size: 8pt;' href='" + Request.Url.ToString().Replace(url[url.Length - 1], "") + "ProCompraLocal_Autorizacion.aspx?id1=" + hfNumSolicitudCte.Value + "&Id2=" + session.Id_Emp + "&Id3=" + session.Id_Cd_Ver + "'>");
                //    + "AutorizaComprasLocales.aspx?Solicitud=" + hfNumSolicitudCte.Value + "'>");
                //  ?Id_Folio=" + Id_Env + "&accion=" + 5 + "&PermisoGuardar=" +
                //  permisoGuardar + "&PermisoModificar=" + permisoModificar + "&PermisoEliminar=" + permisoEliminar + "&PermisoImprimir="
                //  + permisoImprimir + "&Id1=" + pe.Env_Unique + "&Id2=" + Sesion.Id_Emp + "&Id3=" + Sesion.Id_Cd_Ver + "&Id4=1" + "'>");
                cuerpo_correo.Append("Solicitud de Compra Local</a>");
                cuerpo_correo.Append("</td></tr></table></div>");
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                sm.EnableSsl = true;
                MailMessage m = new MailMessage();
                m.From = new MailAddress(configuracion.Mail_Remitente);

                // aqui se llama a la funcionq ue me regresa los correos segun el motivo/aplicacion
                //  m.To.Add(new MailAddress("rafael.garcia@gibraltar.com.mx"));
                // m.To.Add(new MailAddress("rafael.garcia@gibraltar.com.mx"));
                string correo = "";
                int famk = 0;

                if (this.cmbAplicacionSoli.SelectedValue != "")
                {
                    famk = Convert.ToInt32(this.cmbAplicacionSoli.SelectedValue);
                }
                this.CorreosAutorizadorxMotivoxApp(ref correo, famk);

                string[] eVirtual = correo.Split(',');
                reng = 0;

                foreach (string core in eVirtual)
                {
                    if (core != " ")
                    {
                        if (reng == 0)
                        {
                            m.To.Add(new MailAddress(core));
                            reng = 1;
                        }
                        else
                        {
                            m.CC.Add(new MailAddress(core));
                        }

                    }
                }

                m.Bcc.Add(new MailAddress("rafael.garcia@gibraltar.com.mx"));


                m.Subject = "Solicitud de compra local " + hfNumSolicitudCte.Value;
                m.IsBodyHtml = true;
                string body = cuerpo_correo.ToString();

                this.RespaldoCorreo(hfNumSolicitudCte.Value, body, correo);

                AlternateView vistaHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                //Esto queda dentro de un try por si llegan a cambiar la imagen el correo como quiera se mande
                try
                {
                    LinkedResource logo = new LinkedResource(MapPath(@"Imagenes/logo.jpg"), MediaTypeNames.Image.Jpeg);
                    logo.ContentId = "companylogo";
                    vistaHtml.LinkedResources.Add(logo);
                }
                catch (Exception)
                {
                }

                m.AlternateViews.Add(vistaHtml);
                try
                {
                    sm.Send(m);
                }
                catch (Exception exx)
                {
                    Alerta("Error al enviar el correo. Favor de revisar la configuración del sistema. " + exx.Message + exx.Data.ToString());
                    LimpiarCampos3();
                    return;
                }
                Alerta("Solicitud enviada correctamente");
                LimpiarCampos3();
            }
            catch (Exception ex)
            {
                this.ValidaSolicitud(hfNumSolicitudCte.Value);
                throw ex;
            }
        }

        protected void EnviarSolicitud(object sender, EventArgs e)
        {
            /*
            foreach (GridDataItem dataItem in rgPrecios.MasterTableView.Items)
            {
                if (Convert.ToInt32(dataItem["Id_Pre"].Text) == 1 && Convert.ToBoolean(dataItem["Prd_Actual"].Text) == true)
                {
                    if (Convert.ToDouble((((Label)dataItem.FindControl("lblPrd_Pesos")).Text)) == 0)
                    {
                        Alerta("El precio AAA actual no puede ser 0");
                        return;
                    }
                }
                if (Convert.ToInt32(dataItem["Id_Pre"].Text) == 2 && Convert.ToBoolean(dataItem["Prd_Actual"].Text) == true)
                {
                    if (Convert.ToDouble((((Label)dataItem.FindControl("lblPrd_Pesos")).Text)) == 0)
                    {
                        Alerta("El precio LISTA actual no puede ser 0");
                        return;
                    }
                }
                if (Convert.ToInt32(dataItem["Id_Pre"].Text) == 3 && Convert.ToBoolean(dataItem["Prd_Actual"].Text) == true)
                {
                    if (Convert.ToDouble((((Label)dataItem.FindControl("lblPrd_Pesos")).Text)) == 0)
                    {
                        Alerta("El precio COSTO actual no puede ser 0");
                        return;
                    }
                }

            }
            */

            string providerName = this.cmbProveedor.Text;    //    Request.Form[txtSearchProv.UniqueID];
            string providerId = Request.Form[hfProviderId.UniqueID];
            string productName = Request.Form[txtSearch.UniqueID];
            int famk = 0;

            try
            {
                // Ajuste del codigo del producto

                if (this.txtFam.Text != "")
                { famk = Convert.ToInt32(this.txtFam.Text); }

                txtCodigoUsadoProd.Text = this.MaximoId(Convert.ToInt32(TextId_Prd.Text), this.txtProveedor.Text);      //   this.Valor;
                txtCodProd.Text = txtCodigoUsadoProd.Text;
                lblTituloProducto.Text = string.Concat(txtCodProd.Text, " - ", TextPrd_Descrpcion.Text);

                string Fmilia = "";
                if (cmbFam.Items.Count > 0)
                {
                    Fmilia = this.cmbFam.SelectedItem.Text;
                }
                string SbFmilia = ""; //    validar q tenga datops decimal subfamilia

                if (cmbSubFam.Items.Count > 0)
                {
                    SbFmilia = this.cmbSubFam.SelectedItem.Text;
                }
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }

                Producto productoOriginal = new Producto();

                productoOriginal = ConsultarPorducto(Convert.ToInt32(this.TextId_Prd.Text), Sesion.Id_Cd_Ver);
                productoOriginal.ListaProductoPrecios = this.ConsultarPorductoPrecios(Convert.ToInt32(this.TextId_Prd.Text));

                Producto prodcto = new Producto();
                int ok = 0;

                ChecarProductoYaSolicitado(Convert.ToInt64(txtCodigoUsadoProd.Text), ref ok);
                if (ok != 0)
                {
                    Alerta("Ya se encuentra una solicitud pendiente de autorizar para ese producto.");
                    return;
                }

                Guardar(ref prodcto, ref ok);

                if (ok != 1)
                {
                    return;
                }
                int grabo = 0;
                int verificador = -1;

                EntradaVirtual pe = new EntradaVirtual();
                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Cd = Sesion.Id_Cd_Ver;
                configuracion.Id_Emp = Sesion.Id_Emp;
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                CN_ComprasLocales comLoc = new CN_ComprasLocales();

                verificador = Convert.ToInt32(hfNumSolicitud.Value);
                comLoc.GrabaTipoCompraLocal(verificador, 1, Sesion.Emp_Cnx, ref grabo);

                grabo = 0;

                string Comentarios = this.cmbCausaDesabasto.Text;
                string PedidoRef = "";

                comLoc.GrabaSoloComentariosCliente(Comentarios, verificador, Sesion.Emp_Cnx, ref grabo);
                if (this.divPedidosRefAbasto.Visible == true)
                {

                    /// aqui verifica si tiene pedidos seleccionados
                    foreach (ListItem itemCh in this.chklstPedidos.Items)
                    {
                        if (itemCh.Selected == true)
                        {
                            PedidoRef = PedidoRef + itemCh.Value.ToString() + ',';
                        }
                    }

                    if (PedidoRef == "")
                    {
                        Alerta("DEbe de seleccionar al menos un pedido desabastecido.");
                        return;
                    }
                    else
                    {
                        txtValuesPedidos.Text = PedidoRef;
                        GrabaPedidodeListaPedidos(verificador, prodcto.Id_Prd, txtValuesPedidos);
                        PedidoRef = txtValuesPedidos.Text;
                    }
                }
                cn_configuracion.Consulta(ref configuracion, Sesion.Emp_Cnx);

                lblNumSolicitud.Text = "Solicitud " + hfNumSolicitud.Value.ToString() + " Generada!!";

                StringBuilder cuerpo_correo = new StringBuilder();
                cuerpo_correo.Append("<div align='center'>");
                cuerpo_correo.Append("<table width='700px'><tr><td>");
                cuerpo_correo.Append("<IMG SRC=\"cid:companylogo\" ALIGN='left'></td>");
                cuerpo_correo.Append("</tr><tr><td colspan='2'><br></td>");
                cuerpo_correo.Append("</tr><tr>");
                cuerpo_correo.Append("<td colspan='2' style='font-family:Calibri,sans-serif;mso-bidi-font-family:Calibri; color:black'><br><br><b>");
                cuerpo_correo.Append("Solicitud de compra <b>" + hfNumSolicitud.Value + "</b><br>");
                cuerpo_correo.Append("para el proveedor " + providerName + "<br>");
                cuerpo_correo.Append("del producto " + productName);
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");


                cuerpo_correo.Append("<table width='90%' style='font-family:Calibri,sans-serif;mso-bidi-font-family:Calibri;color:black' ");
                cuerpo_correo.Append("border=0 cellpadding=1><tr><td>Código del producto:</td><td>" + productoOriginal.Id_Prd + "</td>");
                cuerpo_correo.Append("<td>&nbsp;</td><td>Código Compra Local</td><td>" + prodcto.Id_Prd + "</td></tr>");
                cuerpo_correo.Append("<tr><td>Descripción del producto:</td><td colspan='4'>" + Request.Form[TextPrd_Descrpcion.UniqueID] + "</td></tr>");
                //  cuerpo_correo.Append("<tr><td>Presentación:</td><td>" + Request.Form[txtPresentacion.UniqueID] + "</td><td colspan='3'>&nbsp;</td></tr>");
                cuerpo_correo.Append("<tr><td>Tipo de producto:</td><td colspan='3'>" + cmbTipoProducto.Text + "</td><td>&nbsp;</td></tr>");

                cuerpo_correo.Append("<tr><td>Familia del producto:</td><td colspan='4'>" + Fmilia + "</td></tr>");
                cuerpo_correo.Append("<tr><td>SubFamilia del producto:</td><td colspan='4'>" + SbFmilia + "</td></tr>");

                if (PedidoRef != "")
                {
                    cuerpo_correo.Append("<tr><td>Pedido Referencia del desabasto:</td><td colspan='3'>" + PedidoRef + "</td><td>&nbsp;</td></tr>");
                }

                cuerpo_correo.Append("<tr><td>Motivo del desabasto:</td><td colspan='3'>" + Comentarios.Replace("\r\n", "<br>") + "</td><td>&nbsp;</td></tr>");

                cuerpo_correo.Append("<tr><td>Precios:</td><td colspan='4'>&nbsp;</td></tr><tr><td colspan='4' align=center>");

                //                  string tablaprecios = "<table style='font-family: verdana; font-size: 8pt;' cellspacing=0 border=1 cellpadding=0 width=250px>";
                //                  tablaprecios = tablaprecios + "<tr><td style='background-color:#d9d9d9' ><b>Tipo de Precio</b></td><td style='background-color:#d9d9d9' align=right><b>Precio</b></td></tr>"; 

                string tablaprecios = "<table style='font-family: verdana; font-size: 8pt;' cellspacing=0 border=1 cellpadding=0 width=250px>";
                tablaprecios = tablaprecios + "<tr><td style='background-color:#d9d9d9' ><b>Codigos:&nbsp;</b></td><td style='background-color:#d9d9d9' ><b>";
                tablaprecios = tablaprecios + prodcto.Id_Prd + "&nbsp;</b></td><td style='background-color:#d9d9d9' align=right><b>";
                tablaprecios = tablaprecios + productoOriginal.Id_Prd + "&nbsp;</b></td></tr>";

                // saca los datos de los precios originales..
                string AAAO = "";
                string PublicoO = "";
                foreach (ProductoPrecios productoPrecios2 in productoOriginal.ListaProductoPrecios)
                {
                    /// Evaluar el tipo para eliminar el Costo Original
                    if ((productoPrecios2.Pre_Descripcion == "AAA") && (productoPrecios2.Prd_Actual == true))
                    {
                        AAAO = productoPrecios2.Prd_Pesos.ToString("#,##0.00");
                    }

                    if ((productoPrecios2.Pre_Descripcion == "PUBLICO") && (productoPrecios2.Prd_Actual == true))
                    {
                        PublicoO = productoPrecios2.Prd_Pesos.ToString("#,##0.00");
                    }
                }

                /// // 1 prudcto = 1 solicutd
                string color = "";
                int reng = 1;
                foreach (ProductoPrecios productoPrecios in prodcto.ListaProductoPrecios)
                {
                    if (productoPrecios.Prd_Actual == true)
                    {
                        if (reng == 0)
                        {
                            reng = 1;
                            color = "style='background-color:#d9d9d9'";
                        }
                        else
                        {
                            reng = 0;
                            color = "";
                        }

                        tablaprecios = tablaprecios + "<tr><td " + color + ">" + productoPrecios.Pre_Descripcion + "</td>";
                        tablaprecios = tablaprecios + "<td align=right " + color + ">" + Convert.ToDouble(productoPrecios.Prd_Pesos).ToString("#,##0.00") + "</td>";

                        if (productoPrecios.Pre_Descripcion == "COSTO")
                        {
                            tablaprecios = tablaprecios + "<td align=right " + color + ">" + "█████████" + "</td>";
                        }

                        if (productoPrecios.Pre_Descripcion == "AAA")
                        {
                            tablaprecios = tablaprecios + "<td align=right " + color + ">" + Convert.ToDouble(AAAO).ToString("#,##0.00") + "</td>";
                        }

                        if (productoPrecios.Pre_Descripcion == "PUBLICO")
                        {
                            tablaprecios = tablaprecios + "<td align=right " + color + ">" + Convert.ToDouble(PublicoO).ToString("#,##0.00") + "</td>";
                        }


                        tablaprecios = tablaprecios + "</tr>";
                    }
                }
                tablaprecios = tablaprecios + "</table>";
                cuerpo_correo.Append(tablaprecios);
                cuerpo_correo.Append("</td></tr>");
                cuerpo_correo.Append("</table>");
                cuerpo_correo.Append("</td></tr><tr><td colspan=4>&nbsp;</td></tr>");

                //  cuerpo_correo.Append("<tr><td  style='font-family:Calibri,sans-serif;mso-bidi-font-family:Calibri; color:black'>Costos del producto base:</td><td colspan='4'>&nbsp;</td></tr><tr><td colspan='4' align=center>");

                //genera tabla con los proecios del producto original

                /*
                                string tablaprecios2 = "<table style='font-family: verdana; font-size: 8pt;' cellspacing=0 border=1 cellpadding=0 width=250px>";
                                tablaprecios2 = tablaprecios2 + "<tr><td style='background-color:#d9d9d9' >&nbsp;</td><td style='background-color:#d9d9d9' ><b>";
                                tablaprecios2 = tablaprecios2 + this.TextId_Prd.Text + "</b></td><td style='background-color:#d9d9d9' align=right><b>"
                                tablaprecios2 = tablaprecios2 + this.txtCodProd.Text + "</b></td></tr>";

                                /// // 1 prudcto = 1 solicutd
                                color = "";
                                reng = 1;
                                foreach (ProductoPrecios productoPrecios2 in productoOriginal.ListaProductoPrecios)
                                {
                                    /// Evaluar el tipo para eliminar el Costo Original
                                    if (productoPrecios2.Pre_Descripcion != "COSTO")
                                    {
                                        if (productoPrecios2.Prd_Actual == true)
                                        {
                                            if (reng == 0)
                                            {
                                                reng = 1;
                                                color = "style='background-color:#d9d9d9'";
                                            }
                                            else
                                            {
                                                reng = 0;
                                                color = "";
                                            }

                                            tablaprecios2 = tablaprecios2 + "<tr><td " + color + ">" + productoPrecios2.Pre_Descripcion + "</td>";
                                            tablaprecios2 = tablaprecios2 + "<td align=right " + color + ">" + Convert.ToDouble(productoPrecios2.Prd_Pesos).ToString("#,##0.00") + "</td></tr>";
                                        }
                                    }
                                }
                                tablaprecios2 = tablaprecios2 + "</table>";
                                cuerpo_correo.Append(tablaprecios2);
                                cuerpo_correo.Append("</td></tr>");
                */

                cuerpo_correo.Append("<tr><td colspan='2' align=center>");

                string[] url = Request.Url.ToString().Split(new char[] { '/' });

                cuerpo_correo.Append("<br>");
                cuerpo_correo.Append("<a style='font-family: verdana; font-size: 8pt;' href='" + Request.Url.ToString().Replace(url[url.Length - 1], "") + "ProCompraLocal_Autorizacion.aspx?id1=" + hfNumSolicitud.Value + "&Id2=" + Sesion.Id_Emp + "&Id3=" + Sesion.Id_Cd_Ver + "'>");

                cuerpo_correo.Append("Solicitud de Compra Local</a>");
                cuerpo_correo.Append("</td></tr></table></div>");
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                sm.EnableSsl = true;
                MailMessage m = new MailMessage();
                //  string[] eVirtual = configuracion.Mail_EVirtual.Split(',');
                m.From = new MailAddress(configuracion.Mail_Remitente);

                // aqui se llama a la funcionq ue me regresa los correos segun el motivo/aplicacion
                // m.To.Add(new MailAddress("rafael.garcia@gibraltar.com.mx"));

                string correo = "";


                this.CorreosAutorizadorxMotivo(ref correo);
                //  this.CorreosAutorizadorxMotivoxApp(ref correo, Aplik);
                string[] eVirtual = correo.Split(',');
                reng = 0;

                foreach (string core in eVirtual)
                {
                    if (core != " ")
                    {
                        if (reng == 0)
                        {
                            m.To.Add(new MailAddress(core));
                            reng = 1;
                        }
                        else
                        {
                            m.CC.Add(new MailAddress(core));
                        }

                    }
                }
                m.Bcc.Add(new MailAddress("rafael.garcia@gibraltar.com.mx"));

                m.Subject = "Solicitud de compra local " + hfNumSolicitud.Value + " del centro " + Sesion.Id_Cd + " - " + Sesion.Cd_Nombre;
                m.IsBodyHtml = true;
                string body = cuerpo_correo.ToString();

                this.RespaldoCorreo(hfNumSolicitud.Value, body, correo);

                AlternateView vistaHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);


                //Esto queda dentro de un try por si llegan a cambiar la imagen el correo como quiera se mande

                try
                {
                    LinkedResource logo = new LinkedResource(MapPath(@"Imagenes/logo.jpg"), MediaTypeNames.Image.Jpeg);
                    logo.ContentId = "companylogo";
                    vistaHtml.LinkedResources.Add(logo);
                }
                catch (Exception)
                {
                }
                m.AlternateViews.Add(vistaHtml);

                try
                {
                    sm.Send(m);

                    //  cliente.Send(mmsg);
                }
                catch (System.Net.Mail.SmtpException exx)
                {
                    Alerta("Error al enviar el correo. Favor de revisar la configuración del sistema: " + "||" + exx.Message);
                    this.listSource = this.ConsultarPorductoPrecios(0);
                    this.LimpiarCampos();
                    rgPrecios.Rebind();
                    return;
                }
                Alerta("Solicitud enviada correctamente");
                this.listSource = this.ConsultarPorductoPrecios(0);
                this.LimpiarCampos();
                rgPrecios.Rebind();
            }
            catch (Exception ex)
            {
                this.ValidaSolicitud(hfNumSolicitud.Value);
                throw ex;
            }

        }

        private void ListadoSolicitudes()
        {

            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CompraLocal cl = new CompraLocal();
            cl.Id_Emp = Sesion.Id_Emp;
            cl.Id_Cd = Sesion.Id_Cd_Ver;
            cl.Id_Comp = -1;

            CN_ComprasLocales cn_Listadocompralocal = new CN_ComprasLocales();
            List<CompraLocal> v = new List<CompraLocal>();

            cn_Listadocompralocal.ConsultarSolicitudes(cl, Sesion.Emp_Cnx, ref v);
            rgCompraLocal.DataSource = v;
            rgCompraLocal.Rebind();
        }

        private string MaximoId(int idProd, string Prov)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_ComprasLocales cn_CompraLocal = new CN_ComprasLocales();

                string Ca = "0";
                /*
                if (cmbCategorias.SelectedValue == "01")
                {
                    idProd = Convert.ToInt32(TextId_Prd.Text);
                    Prov = txtProveedor.Text;
                }
                
                if (cmbCategorias.SelectedValue == "03")
                {
                    idProd = 0;
                    
                    Prov = this.txtProveedorCte.Text;
                }
                */
                Ca = cmbCategorias.SelectedValue;

                return cn_CompraLocal.NuevoCodigoProducto(sesion.Id_Emp, sesion.Id_Cd_Ver, Ca, Prov, idProd, sesion.Emp_Cnx);

                //   CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                //   return CN_Comun.Maximo(sesion.Id_Emp, "CatProducto", "Id_Prd", sesion.Emp_Cnx, "spCatCentral_Maximo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool Deshabilitar()
        {
            try
            {
                bool verificador = false;
                if (hiddenId.Value != "") //Hidden Field BANDERA
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                    Catalogo ct = new Catalogo();
                    ct.Id_Emp = sesion.Id_Emp; //Si no ocupa empresa su catalogo se pone -1
                    ct.Id_Cd = sesion.Id_Cd_Ver; //Si no ocupa Centro de distribución se pone -1
                    ct.Id = Convert.ToInt32(hiddenId.Value); //Si no ocupa Id se pone -1
                    ct.Tabla = "CatProducto"; //Nombre de la tabla del catalogo
                    ct.Columna = "Id_Prd"; //Nombre de la columna del ID del catalogo
                    CN_Comun.Deshabilitar(ct, sesion.Emp_Cnx, ref verificador);
                }
                return verificador;
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
                if (mensaje.Contains("rgPrecios_FechasRango_incorrecto"))
                Alerta("Favor de capturar un rango de fechas correcto");
            else
                    if (mensaje.Contains("rgPrecios_FechasRango_PeridoNuevoAnterior"))
                Alerta("El periodo nuevo no debe ser menor al periodo actual");
            else
                        if (mensaje.Contains("rgPrecios_FechasRango_DiasEntrePeriodo"))
                Alerta("Rango de fechas no válido. Hay días entre el periodo anterior y el periodo nuevo. La fecha de inicio del periodo actual debe ser el siguiente día después de la fecha final del periodo anterior");
            else
                            if (mensaje.Contains("rgPrecios_FechasRango_empalmado"))
                Alerta("Rango de fechas empalmado.");
            else
                                if (mensaje.Contains("cmbProductosLista_ItemsDataBound"))
                Alerta("Error al llenar la lista de productos, combo cmbProductos");
            else
                                    if (mensaje.Contains("cmbProductosLista_UpdateCount"))
                Alerta("No se pudo actualizar el n&uacute;mero de registros de la lista de productos");
            else
                                        if (mensaje.Contains("cmbProductosLista_ItemsRequested"))
                Alerta("No se pudo actualizar la lista de productos");
            else
                                            if (mensaje.Contains("CatProducto_fechaEmpalmada_error"))
                Alerta("Los datos del precio de producto no se guardaron.<br/> Rango de fechas empalmado");
            else
                                                if (mensaje.Contains("rgPrecios_ItemDataBound"))
                Alerta("Error al colorear los precions actuales en el grid de precios de producto");
            else
                                                    if (mensaje.Contains("rgPrecios_NeedDataSource"))
                Alerta("Error al cargar el Grid de tipos de costos");
            else
                                                        if (mensaje.Contains("rgPrecios_ItemCommand"))
                Alerta("Error al ejecutar un evento (rgPrecios_ItemCommand) al cargar el Grid de tipo de costos");
            else
                                                            if (mensaje.Contains("rgPrecios_Actualizar_ok"))
                Alerta("El precio del producto fue actualizado correctamente");
            else
                                                                if (mensaje.Contains("rgPrecios_Actualizar_error"))
                Alerta("Error al actualizar el precio del producto");
            else
                                                                    if (mensaje.Contains("Cmb_CentroDistribucion_IndexChanging_error"))
                Alerta("Error al cambiar de centro de distribución");
            else
                                                                        if (mensaje.Contains("radGrid_PageIndexChanged"))
                Alerta("Error al cambiar de página");
            else
                                                                            if (mensaje.Contains("PermisoGuardarNo"))
                Alerta("No tiene permisos para grabar");
            else
                                                                                if (mensaje.Contains("CatProductoIdRepetida_error"))
                Alerta("La clave ya existe");
            else
                                                                                    if (mensaje.Contains("CatProductoDescripcionRepetida_error"))
                Alerta("La descripción ya existe");
            else
                                                                                        if (mensaje.Contains("PermisoModificarNo"))
                Alerta("No tiene permisos para actualizar");
            else
                                                                                            if (mensaje.Contains("ProductoBuscarNoExiste"))
                Alerta(string.Concat("El producto con clave ", TextId_Prd.Text, " no se ha encontrado"));
            else
                                                                                                if (mensaje.Contains("CatProducto_insert_ok"))
                Alerta("Los datos se guardaron correctamente");
            else
                                                                                                    if (mensaje.Contains("CatProducto_insert_error"))
                Alerta("No se pudo guardar los datos del tipo de precio");
            else
                                                                                                        if (mensaje.Contains("CatProducto_update_ok"))
                Alerta("Los datos se modificaron correctamente");
            else
                                                                                                            if (mensaje.Contains("CatProducto_update_error"))
                Alerta("No se pudo actualizar los datos del tipo de precio");
            else
                                                                                                                if (mensaje.Contains("SeleccionaProductoCL_familia_error"))
                Alerta("El producto pertenece a una familia que no tiene definido un autorizador.");
            else Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
        }

        private void LimpiarCampos()
        {
            lblTituloProducto.Text = string.Empty;
            txtSearch.Text = "";
            hfCustomerId.Value = "";
            //  lblNumSolicitud.Text = "";
            hfNumSolicitud.Value = "";
            TextId_Prd.Text = string.Empty;
            txtCodigoUsadoProd.Text = string.Empty;
            txtPorUtilidades.Text = string.Empty;
            chkActivo.Checked = true;
            chkProductoNuevo.Checked = false;
            txtCodProd.Text = string.Empty;
            TextPrd_Descrpcion.Text = string.Empty;
            txtPresentacion.Text = string.Empty;
            txtTipoProducto.Text = string.Empty;
            cmbTipoProducto.SelectedIndex = cmbTipoProducto.FindItemIndexByValue("-1");
            TextId_Spo.Text = string.Empty;
            cmbSisProp.SelectedIndex = cmbSisProp.FindItemIndexByValue("-1");
            txtAgrupadoSpo.Text = string.Empty;
            txtCategoria.Text = string.Empty;
            cmbCategoria.SelectedIndex = cmbCategoria.FindItemIndexByValue("-1");

            txtSubFam.Text = string.Empty;
            cmbSubFam.SelectedIndex = cmbSubFam.FindItemIndexByValue("-1");
            txtProveedor.Text = string.Empty;
            cmbProveedor.SelectedIndex = cmbProveedor.FindItemIndexByValue("-1");
            cmbUentrada.SelectedIndex = cmbUentrada.FindItemIndexByValue("-1");
            txtFactorConversion.Text = string.Empty;
            cmbUsalida.SelectedIndex = cmbUsalida.FindItemIndexByValue("-1");
            txtUempaque.Text = string.Empty;

            txtFam.Text = string.Empty;
            cmbFam.SelectedIndex = cmbFam.FindItemIndexByValue("-1");

            txtInvSeguridad.Text = string.Empty;
            chkSistProp.Checked = false;
            txtTentrega.Text = string.Empty;
            txtTtransporte.Text = string.Empty;
            txtRentabilidad.Text = string.Empty;
            cmbRentabilidad.SelectedIndex = 0;
            chkComprasLocales.Checked = false;
            txtAmortizacion.Text = string.Empty;
            txtPesos.Text = string.Empty;
            txtExistencia.Text = string.Empty;
            txtUbicacion.Text = string.Empty;
            txtContribucion.Text = string.Empty;

            txtAsignado.Text = string.Empty;
            txtInicial.Text = string.Empty;
            txtOrdenado.Text = string.Empty;
            txtFinal.Text = string.Empty;
            txtTransito.Text = string.Empty;
            txtFisico.Text = string.Empty;
            txtPlanAbasto.Text = string.Empty;
            txtMinCompra.Text = string.Empty;


            this.listSource = this.ConsultarPorductoPrecios(0);
            rgPrecios.Rebind();

            txtFnombre.Text = string.Empty;
            txtFcodigo.Text = string.Empty;
            txtFdescripcion.Text = string.Empty;
            txtFpresentacion.Text = string.Empty;
            txtPnombre.Text = string.Empty;

            txtSearchProv.Text = string.Empty;

            txtPcodigo.Text = string.Empty;
            txtPdescripcion.Text = string.Empty;
            txtPpresentacion.Text = string.Empty;

            cmbCausaDesabasto.SelectedIndex = cmbCausaDesabasto.FindItemIndexByValue("-1");
            lstbPedidos.Items.Clear();
            chklstPedidos.Items.Clear();        /// addon EMC 2019
            hddPedidoAbasto.Value = "";
            lbl_Val_UnidadesDisponibles.Text = "";

            this.hfCveProdServSATAbasto.Value = string.Empty;

            this.cmbProdServicioSATDesabasto.SelectedIndex = cmbProdServicioSATDesabasto.FindItemIndexByValue("-1");
            this.cmbUnidadMedidaSATDesabasto.SelectedIndex = cmbUnidadMedidaSATDesabasto.FindItemIndexByValue("-1");
        }

        private void LimpiarCampos2()
        {
            this.lblTituloProducto.Text = string.Empty;
            //   this.lblNumSolicitudAbasto.Text = "";
            this.txtProductoLocal.Text = string.Empty;
            hfProductoLocal.Value = string.Empty;
            hfNumSolicitudAbasto.Value = string.Empty;
            this.lblId_Prd.Text = string.Empty;
            this.lblPorUtilidades.Text = string.Empty;
            this.chkActivoAbasto.Checked = false;
            this.chkProductoNuevoAbasto.Checked = false;
            this.lblCodProd.Text = string.Empty;
            this.lblPrd_Descrpcion.Text = string.Empty;
            this.lblPrd_DescrpcionAbasto.Value = string.Empty;
            this.lblPresentacion.Text = string.Empty;
            this.lblTipoProducto.Text = string.Empty;
            this.lblcmbTipoProducto.Text = string.Empty;
            this.lblId_Spo.Text = string.Empty;
            this.lblcmbSisProp.Text = string.Empty;
            this.lblAgrupadoSpo.Text = string.Empty;
            this.lblCategoria.Text = string.Empty;
            this.lblcmbCategoria.Text = string.Empty;

            this.lblSubFam.Text = string.Empty;
            this.lblcmbSubFam.Text = string.Empty;
            this.lblProveedor.Text = string.Empty;
            this.lblcmbProveedor.Text = string.Empty;
            this.lblcmbUentrada.Text = string.Empty;
            this.lbltxtFactorConversion.Text = string.Empty;
            this.lblcmbUsalida.Text = string.Empty;
            this.lbltxtUempaque.Text = string.Empty;

            this.lblFam.Text = string.Empty;
            this.lblcmbFam.Text = string.Empty;

            this.lbltxtInvSeguridad.Text = string.Empty;
            ///            this.chkschkSistProp.Checked = false;
            this.lbltxtTentrega.Text = string.Empty;
            this.lbltxtTtransporte.Text = string.Empty;
            this.lbltxtRentabilidad.Text = string.Empty;
            this.lbllblRentabilidad.Text = string.Empty;
            this.chkComprasLocalesAbasto.Checked = false;
            this.lbltxtAmortizacion.Text = string.Empty;
            this.lbltxtPesos.Text = string.Empty;
            this.lbltxtExistencia.Text = string.Empty;
            this.lbltxtUbicacion.Text = string.Empty;
            this.lbltxtContribucion.Text = string.Empty;

            this.lbltxtAsignado.Text = string.Empty;
            this.lbltxtInicial.Text = string.Empty;
            this.lbltxtOrdenado.Text = string.Empty;
            this.lbltxtFinal.Text = string.Empty;
            this.lbltxtTransito.Text = string.Empty;
            this.lbltxtFisico.Text = string.Empty;
            this.lbltxtPlanAbasto.Text = string.Empty;
            this.lbltxtMinCompra.Text = string.Empty;

            this.listSource = this.ConsultarPorductoPrecios(0);
            this.rgPreciosAbasto.Rebind();

            this.lbltxtFnombre.Text = string.Empty;
            this.lbltxtFcodigo.Text = string.Empty;
            this.lbltxtFdescripcion.Text = string.Empty;
            this.lbltxtFpresentacion.Text = string.Empty;
            this.txtPnombreAbasto.Text = string.Empty;

            this.txtSearchProvAbasto.Text = string.Empty;

            this.lbltxtPcodigo.Text = string.Empty;
            this.lbltxtPdescripcion.Text = string.Empty;
            this.lbltxtPpresentacion.Text = string.Empty;

            txtProveeAba.Text = string.Empty;
            this.cmbProveedorAba.SelectedIndex = cmbProveedorAba.FindItemIndexByValue("-1");

            this.hfCveProdServSAT.Value = string.Empty;

            this.cmbProdServicioSATAbasto.SelectedIndex = cmbProdServicioSATAbasto.FindItemIndexByValue("-1");
            this.cmbUnidadMedidaSATAbasto.SelectedIndex = cmbUnidadMedidaSATAbasto.FindItemIndexByValue("-1");
        }

        private void LimpiarCampos3()
        {
            this.lblTituloProducto.Text = string.Empty;
            this.txtSearchProvCte.Text = "";
            this.hfProviderIdCte.Value = "";
            this.hfNumSolicitudCte.Value = "";
            this.txtIdProductoCteF4F.Text = "";
            this.hdfProductoCteF4F.Value = "";
            this.TextId_PrdCte.Text = "";
            this.chkActivoCte.Checked = true;
            this.chkProdNuevoCte.Checked = false;
            this.txtCodProdCte.Value = string.Empty;
            this.TextPrd_DescrpcionCte.Text = string.Empty;
            this.txtPresentacionCte.Text = string.Empty;
            this.txtTipoProductoCte.Text = string.Empty;
            this.cmbTipoProductoCte.SelectedIndex = this.cmbTipoProductoCte.FindItemIndexByValue("-1");
            this.TextId_SpoCte.Text = string.Empty;
            this.cmbSisPropCte.SelectedIndex = this.cmbSisPropCte.FindItemIndexByValue("-1");
            this.txtCategoriaCte.Text = string.Empty;
            this.cmbCategoriaCte.SelectedIndex = this.cmbCategoriaCte.FindItemIndexByValue("-1");

            this.txtSubFamCte.Text = string.Empty;
            this.cmbSubFamCte.SelectedIndex = this.cmbSubFamCte.FindItemIndexByValue("-1");
            this.txtProveedorCte.Text = string.Empty;
            this.cmbProveedorCte.SelectedIndex = this.cmbProveedorCte.FindItemIndexByValue("-1");
            this.cmbUentradaCte.SelectedIndex = this.cmbUentradaCte.FindItemIndexByValue("-1");
            this.txtFactorConversionCte.Text = string.Empty;
            this.cmbUsalidaCte.SelectedIndex = this.cmbUsalidaCte.FindItemIndexByValue("-1");
            this.txtUempaqueCte.Text = string.Empty;

            this.txtFamCte.Text = string.Empty;
            this.cmbFamCte.SelectedIndex = this.cmbFamCte.FindItemIndexByValue("-1");

            this.txtInvSeguridadCte.Text = string.Empty;
            this.chkSistPropCte.Checked = false;
            this.txtTentregaCte.Text = string.Empty;
            this.txtTtransporteCte.Text = string.Empty;
            this.chkComprasLocalesCte.Checked = false;
            this.txtAmortizacionCte.Text = string.Empty;
            this.txtPesosCte.Text = string.Empty;
            this.txtExistenciaCte.Text = string.Empty;
            this.txtUbicacionCte.Text = string.Empty;

            this.txtAsignadoCte.Text = string.Empty;
            this.txtInicialCte.Text = string.Empty;
            this.txtOrdenadoCte.Text = string.Empty;
            this.txtFinalCte.Text = string.Empty;
            this.txtTransitoCte.Text = string.Empty;
            this.txtFisicoCte.Text = string.Empty;
            this.txtPlanAbastoCte.Text = string.Empty;
            this.txtMinCompraCte.Text = string.Empty;

            this.listSource = this.ConsultarPorductoPrecios(0);
            this.rgPreciosCte.Rebind();

            this.txtFnombreCte.Text = string.Empty;
            this.txtFcodigoCte.Text = string.Empty;
            this.txtFdescripcionCte.Text = string.Empty;
            this.txtPnombreCte.Text = string.Empty;

            this.txtSearchProvCte.Text = string.Empty;

            this.txtPcodigoCte.Text = string.Empty;
            this.txtPdescripcionCte.Text = string.Empty;
            this.txtPpresentacionCte.Text = string.Empty;

            this.txtMotivoSolicita.Text = string.Empty;
            this.rdpVigencia.Clear();
            this.cmbAplicacionSoli.SelectedIndex = this.cmbAplicacionSoli.FindItemIndexByValue("-1");
            this.cmdSubFamiliaSoli.SelectedIndex = this.cmdSubFamiliaSoli.FindItemIndexByValue("-1");
            this.RadComboBoxProduct.SelectedIndex = this.RadComboBoxProduct.FindItemIndexByValue("-1");
            this.RadComboBoxProduct.ClearSelection();
            this.cmdSubFamiliaSoli.Enabled = false;
            this.RadComboBoxProduct.Enabled = false;

            this.listaClientes.Items.Clear();
            this.ddlElements.Value = string.Empty;
            this.ddlElementsFull.Value = string.Empty;

            this.hfCveProdServSATCte.Value = string.Empty;

            this.cmbUnidadMedidaSATCte.SelectedIndex = cmbUnidadMedidaSATCte.FindItemIndexByValue("-1");
            this.cmbProdServicioSATCte.SelectedIndex = cmbProdServicioSATCte.FindItemIndexByValue("-1");
        }

        private void LimpiarCampos4()
        {
            this.lblTituloProducto.Text = string.Empty;
            this.TextId_PrdCons.Text = "";
            this.txtCodProdCons.Text = "";
            this.chkActivoCons.Checked = true;
            this.TextPrd_DescrpcionCons.Text = string.Empty;
            this.chkProdNuevoCons.Checked = false;
            this.txtPresentacionCons.Text = string.Empty;
            this.txtTipoProductoCons.Text = string.Empty;

            this.cmbTipoProductoCons.SelectedIndex = this.cmbTipoProductoCons.FindItemIndexByValue("-1");
            this.TextId_SpoCons.Text = string.Empty;
            this.cmbSisPropCons.SelectedIndex = this.cmbSisPropCons.FindItemIndexByValue("-1");
            this.txtCategoriaCons.Text = string.Empty;
            this.cmbCategoriaCons.SelectedIndex = this.cmbCategoriaCons.FindItemIndexByValue("-1");

            this.txtFamCons.Text = string.Empty;
            this.cmbFamCons.SelectedIndex = this.cmbFamCons.FindItemIndexByValue("-1");
            this.txtSubFamCons.Text = string.Empty;
            this.cmbFamCons.SelectedIndex = this.cmbFamCons.FindItemIndexByValue("-1");
            this.txtProveedorCons.Text = string.Empty;
            this.cmbProveedorCons.SelectedIndex = this.cmbProveedorCons.FindItemIndexByValue("-1");
            this.cmbUentradaCons.SelectedIndex = this.cmbUentradaCons.FindItemIndexByValue("-1");
            this.txtFactorConversionCons.Text = string.Empty;
            this.cmbUsalidaCons.SelectedIndex = this.cmbUsalidaCons.FindItemIndexByValue("-1");
            this.txtUempaqueCons.Text = string.Empty;

            this.txtMotivoSolicitaCons.Text = string.Empty;
            this.rdpVigenciaCons.Clear();

            this.listSource = this.ConsultarPorductoPrecios(0);
            this.rgPreciosCons.Rebind();

            this.lstClientesAutorizadosCons.Items.Clear();
            this.ddlElementsCons.Value = string.Empty;
            this.ddlElementsFullCons.Value = string.Empty;

            this.txtPcodigoCte.Text = string.Empty;
            this.txtPdescripcionCte.Text = string.Empty;
            this.txtPpresentacionCte.Text = string.Empty;

            this.hfNumSolicitudCons.Value = string.Empty;
            RadTabStripPrincipalCons.Tabs[2].Visible = true;
            this.cmbCausaDesabastoCons.SelectedIndex = this.cmbCausaDesabastoCons.FindItemIndexByValue("-1");

        }

        private void LlenarFormularioProducto(int id_Producto, int id_Cd_Prod)
        {
            try
            {
                Producto producto = ConsultarPorducto(id_Producto, id_Cd_Prod);
                TextId_Prd.Text = producto.Id_Prd.ToString();
                txtCodProd.Text = producto.Prd_Unico == 0 ? string.Empty : producto.Prd_Unico.ToString();
                chkActivo.Checked = chkActivoAbasto.Checked;
                chkProductoNuevo.Checked = producto.Prd_Nuevo;
                TextPrd_Descrpcion.Text = producto.Prd_Descripcion;
                txtPresentacion.Text = producto.Prd_Presentacion;

                TextId_Spo.Text = string.Empty;
                cmbSisProp.Text = "";
                cmbSisProp.ClearSelection();
                if (producto.Id_Spo != 0)
                {
                    TextId_Spo.Text = producto.Id_Spo.ToString();
                    cmbSisProp.SelectedValue = producto.Id_Spo.ToString();
                }

                txtTipoProducto.Text = producto.Id_Ptp.ToString();
                cmbTipoProducto.SelectedValue = producto.Id_Ptp.ToString();
                txtCategoria.Text = string.Empty;
                cmbCategoria.Text = "";
                cmbCategoria.ClearSelection();
                if (producto.Id_Cpr != 0)
                {
                    txtCategoria.Text = producto.Id_Cpr.ToString();
                    cmbCategoria.SelectedValue = producto.Id_Cpr.ToString();
                }
                txtFam.Text = string.Empty;
                cmbFam.Text = "";
                cmbFam.ClearSelection();
                if (producto.Id_Fam != 0)
                {
                    txtFam.Text = producto.Id_Fam.ToString();
                    cmbFam.SelectedValue = producto.Id_Fam.ToString();
                }
                txtSubFam.Text = string.Empty;
                cmbSubFam.Text = "";
                cmbSubFam.ClearSelection();
                cmbSubFam.Items.Clear();
                if (producto.Id_Sub != 0)
                {
                    this.LlenarComboProductoSubFamilia(producto.Id_Fam, ref cmbSubFam);
                    txtSubFam.Text = producto.Id_Sub.ToString();
                    cmbSubFam.SelectedValue = producto.Id_Sub.ToString();
                }
                txtProveedor.Text = ""; //   producto.Id_Pvd == 100 ? string.Empty : producto.Id_Pvd.ToString(); //  producto.Id_Pvd.ToString();
                cmbProveedor.SelectedValue = "-1";  //  producto.Id_Pvd == 100 ? "-1" : producto.Id_Pvd.ToString();    /// producto.Id_Pvd.ToString();
                cmbUentrada.SelectedValue = producto.Prd_UniNe;
                txtFactorConversion.Text = producto.Prd_FactorConv.ToString();
                cmbUsalida.SelectedValue = producto.Prd_UniNs;
                txtUempaque.Text = producto.Prd_UniEmp.ToString();

                txtAgrupadoSpo.Text = producto.Prd_AgrupadoSpo == 0 ? string.Empty : producto.Prd_AgrupadoSpo.ToString();
                txtContribucion.Text = producto.Prd_Contribucion.ToString();
                txtPorUtilidades.Text = producto.Prd_PorUtilidades.ToString();

                txtInvSeguridad.Text = producto.Prd_InvSeg.ToString();
                chkSistProp.Checked = (bool)producto.Prd_AparatoSisProp;
                txtTentrega.Text = producto.Prd_TEntre.ToString();
                txtTtransporte.Text = producto.Prd_TTrans.ToString();
                cmbRentabilidad.SelectedIndex = cmbRentabilidad.FindItemIndexByText(producto.Prd_Ren.ToString(), true);
                txtRentabilidad.Text = cmbRentabilidad.SelectedValue;
                chkComprasLocales.Checked = producto.Prd_Colo;
                txtAmortizacion.Text = producto.Prd_Mes.ToString();
                txtPesos.Text = producto.Prd_PesConTecnico.ToString();
                txtExistencia.Text = producto.Prd_MaxExistencia.ToString();
                txtUbicacion.Text = producto.Prd_Ubicacion;

                txtAsignado.Text = producto.Prd_Asignado.ToString();
                txtInicial.Text = producto.Prd_InvInicial.ToString();
                txtOrdenado.Text = producto.Prd_Ordenado.ToString();
                txtFinal.Text = producto.Prd_InvFinal.ToString();
                txtTransito.Text = producto.Prd_Transito.ToString();
                txtFisico.Text = producto.Prd_Fisico.ToString();
                txtMinCompra.Text = producto.Prd_Minimo.ToString();
                txtPlanAbasto.Text = producto.Prd_PlanAbasto;


                try
                {


                    cmbProdServicioSATDesabasto.SelectedIndex = cmbProdServicioSATDesabasto.FindItemIndexByValue(producto.Prd_ClaveProdServ.ToString());
                    cmbProdServicioSATDesabasto.Text = cmbProdServicioSATDesabasto.FindItemByValue(producto.Prd_ClaveProdServ.ToString()).Text;


                    txtSearchProdServSAT.Value = cmbProdServicioSATDesabasto.FindItemByValue(producto.Prd_ClaveProdServ.ToString()).Text;
                    hfCveProdServSAT.Value = producto.Prd_ClaveProdServ.ToString();


                    cmbUnidadMedidaSATDesabasto.SelectedIndex = cmbUnidadMedidaSATDesabasto.FindItemIndexByValue(producto.Prd_ClaveUnidad.ToString());
                    cmbUnidadMedidaSATDesabasto.Text = cmbUnidadMedidaSATDesabasto.FindItemByValue(producto.Prd_ClaveUnidad.ToString()).Text;

                }
                catch
                {
                    cmbProdServicioSATDesabasto.SelectedIndex = cmbProdServicioSATDesabasto.FindItemIndexByValue("01010101");
                    cmbProdServicioSATDesabasto.Text = cmbProdServicioSATDesabasto.FindItemByValue("01010101").Text;


                    txtSearchProdServSAT.Value = cmbProdServicioSATDesabasto.FindItemByValue("01010101").Text;
                    hfCveProdServSAT.Value = "01010101";


                    cmbUnidadMedidaSATDesabasto.SelectedIndex = cmbUnidadMedidaSATDesabasto.FindItemIndexByValue("H87");
                    cmbUnidadMedidaSATDesabasto.Text = cmbUnidadMedidaSATDesabasto.FindItemByValue("H87").Text;

                    throw;
                }

                txtSearchProdServSAT.Disabled = false;
                cmbProdServicioSATDesabasto.Enabled = true;
                cmbUnidadMedidaSATDesabasto.Enabled = true;

                strIdPrd = id_Producto.ToString();
                this.listSource = this.ConsultarPorductoPrecios(id_Producto);


                // -- fixed para poner en 0 el costo actual y el AAA

                for (int i = 0; i < this.listSource.Count; i++)
                {
                    if (listSource[i].Prd_Actual)
                    {
                        if (listSource[i].Pre_Descripcion == "COSTO")
                        {
                            hddPrecioCostoCodigo.Value = listSource[i].Prd_Pesos.ToString();
                            listSource[i].Prd_Pesos = 0;
                        }

                        if (listSource[i].Pre_Descripcion == "AAA")
                        {
                            hddPrecioAAACodigo.Value = listSource[i].Prd_Pesos.ToString();
                            //  listSource[i].Prd_Pesos = 0;
                        }
                    }

                    //if (listSource[i].Prd_Actual == false)
                    //{
                    //    if (listSource[i].Pre_Descripcion == "COSTO")
                    //    {
                    //        //asdasdas//

                    //    }
                    //}
                }

                txtFnombre.Text = producto.Prd_CLNomFab;
                txtFcodigo.Text = producto.Prd_CLIdFab;
                txtFdescripcion.Text = producto.Prd_CLDesFab;
                txtFpresentacion.Text = producto.Prd_CLPreFab;
                txtPnombre.Text = producto.Prd_CLNomPro;

                txtPnombre.Text = producto.Prd_CLNomPro;

                txtSearchProv.Text = producto.Prd_CLNomPro;

                txtPcodigo.Text = producto.Prd_CLIdPro;
                txtPdescripcion.Text = producto.Prd_CLDesPro;
                txtPpresentacion.Text = producto.Prd_CLPrePro;

                //  string.Empty : (producto.Prd_InvFinal - producto.Prd_Asignado).ToString();
                int iDisponible = (producto.Prd_InvFinal - producto.Prd_Asignado);

                this.lbl_Val_UnidadesDisponibles.Text = "";

                if (iDisponible > 0)
                {
                    this.lbl_Val_UnidadesDisponibles.Text = "** Existen " + iDisponible.ToString() + " unidades disponibles del producto.";
                }

                //**********************************//
                //* Consultar precios de productos *//
                //**********************************//
                this.IdProducto = id_Producto;
                rgPrecios.Enabled = true;
                rgPrecios.Rebind();
                // Si es consulta de producto de compra local
                // se habilitan controles de la pestaña de compras locales
                //   if (producto.Prd_Colo)
                this.HabilitaCamposComprasLocales(true);
                //   else
                //       this.HabilitaCamposComprasLocales(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void HabilitaCamposSolicitud(bool habilitar)
        {
            txtFnombreCte.Enabled = habilitar;
            txtFcodigoCte.Enabled = habilitar;
            txtFdescripcionCte.Enabled = habilitar;
            txtFpresentacionCte.Enabled = habilitar;
            txtPnombreCte.Enabled = habilitar;
            //  txtSearchProv.Enabled = habilitar;
            txtPcodigoCte.Enabled = habilitar;
            txtPdescripcionCte.Enabled = habilitar;
            txtPpresentacionCte.Enabled = habilitar;
        }

        private void HabilitaCamposComprasLocales(bool habilitar)
        {
            txtFnombre.Enabled = habilitar;
            txtFcodigo.Enabled = habilitar;
            txtFdescripcion.Enabled = habilitar;
            txtFpresentacion.Enabled = habilitar;
            txtPnombre.Enabled = habilitar;
            //  txtSearchProv.Enabled = habilitar;
            txtPcodigo.Enabled = habilitar;
            txtPdescripcion.Enabled = habilitar;
            txtPpresentacion.Enabled = habilitar;
        }

        private void LlenarComboProductoSubFamilia(int familia, ref RadComboBox comb)
        {
            /*
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(1, sesion.Id_Emp, familia, sesion.Emp_Cnx, "spAppSubFam_Combo", ref comb);
            //  this.cmbSubFam.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
            comb.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
            */
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, sesion.Id_Emp, familia, sesion.Emp_Cnx, "spAppSubFam_Combo", ref comb);
                comb.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LlenarComboSubFamilias(string Aplica, ref RadComboBox combo)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            //  CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            //  CN_Comun.LlenaCombo(Convert.ToInt32(Aplica), sesion.Emp_Cnx, "spAplicacionSubFam_Combo", ref cmdSubFamiliaSoli);

            string SP = "spAplicacionSubFam_Combo ";
            string Conexion = sesion.Emp_Cnx;
            System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
            try
            {
                SqlDataReader dr = null;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Conexion);

                string[] Parametros = { "@Id1" };
                object[] Valores = { Convert.ToInt32(Aplica)
                                   };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spAplicacionSubFam_Combo", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    Comun com = new Comun();
                    com.Id = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IdSubFam"))); // dr.GetInt32(dr.GetValue(dr.GetOrdinal("IdSubFam").ToString()));
                    com.Descripcion = dr.GetValue(dr.GetOrdinal("DescripcionSubFam")).ToString();        //  dr.GetValue(dr.GetOrdinal("DescripcionSubFam").ToString())
                    Lista.Add(com);
                }
                dr.Close();
                CapaDatos.LimpiarSqlcommand(ref sqlcmd);

                if (Lista.Count > 0)
                {
                    combo.DataSource = Lista;
                    combo.DataValueField = "Id";
                    combo.DataTextField = "Descripcion";
                    combo.DataBind();
                }
                else
                {
                    combo.Items.Insert(combo.Items.Count(), new RadComboBoxItem("-|- Otros -|-", "0"));
                }
                combo.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LlenarComboAplicacion()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(sesion.Emp_Cnx, "spCatAplicacion_Combo", ref cmbAplicacionSoli);
                cmbAplicacionSoli.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
                //  cmbAplicacionSoli.Items.Insert(cmbAplicacionSoli.Items.Count(), new RadComboBoxItem("-|- Otros -|-", "0"));
                cmdSubFamiliaSoli.Items.Clear();
                cmdSubFamiliaSoli.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private void NuevoProducto()
        {   //rgPrecios.
            this.listSource = this.ConsultarPorductoPrecios(0);
            rgPrecios.DataSource = this.listSource;
            rgPrecios.DataBind();

            txtSubFam.Text = string.Empty;
            cmbSubFam.Text = "";
            cmbSubFam.ClearSelection();
            cmbSubFam.Items.Clear();
            this.hiddenId.Value = string.Empty;

            //Nuevo producto:
            //deshabilta controles de pestaña de compras locales
            this.HabilitaCamposComprasLocales(false);
            chkComprasLocales.Checked = false;
            lblTituloProducto.Text = string.Empty;
            TextId_Prd.Enabled = true;
            TextId_Prd.Focus();
        }

        private Producto ConsultarPorducto(long id_Producto, int id_Cd_Prod)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatProducto clsCatProducto = new CN_CatProducto();
                Producto producto = new Producto();
                clsCatProducto.ConsultaProducto_CL(ref producto, sesion.Emp_Cnx, sesion.Id_Emp, id_Cd_Prod, sesion.Id_Cd_Ver, id_Producto, true);
                return producto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<ProductoPrecios> ConsultarPorductoPrecios(long id_Producto)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                List<ProductoPrecios> list = new List<ProductoPrecios>();
                ProductoPrecios producto = new ProductoPrecios();
                producto.Id_Emp = sesion.Id_Emp;
                producto.Id_Cd = sesion.Id_Cd_Ver;
                producto.Id_Prd = id_Producto;

                //   new CN_ProductoPrecios().ConsultaListaProductoPrecios(producto, sesion.Emp_Cnx, ref list);
                new CN_ProductoPrecios().ConsultaListaProductoPreciosCL(producto, sesion.Emp_Cnx, ref list);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LlenaComboListadoSATDesabasto()
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.AppSettings["strConnection"].ToString();
                SqlCommand cmd = new SqlCommand();

                cmbUnidadMedidaSATDesabasto.Text = "";
                cmbUnidadMedidaSATDesabasto.ClearSelection();
                cmbUnidadMedidaSATDesabasto.Items.Clear();
                cmbUnidadMedidaSATDesabasto.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));

                cmd.CommandText = "spSATUnidadesMedida ";
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        RadComboBoxItem item = new RadComboBoxItem();
                        item.Value = sdr["Cve"].ToString();
                        item.Text = sdr["Descripcion"].ToString();
                        item.Attributes.Add("Cve", sdr["Cve"].ToString());
                        item.Attributes.Add("Descripcion", sdr["Descripcion"].ToString());
                        cmbUnidadMedidaSATDesabasto.Items.Add(item);

                        item.DataBind();
                    }
                }

                conn.Close();

                SqlConnection conn2 = new SqlConnection();
                conn2.ConnectionString = ConfigurationManager.AppSettings["strConnection"].ToString();
                SqlCommand cmd2 = new SqlCommand();

                cmbProdServicioSATDesabasto.Text = "";
                cmbProdServicioSATDesabasto.ClearSelection();
                cmbProdServicioSATDesabasto.Items.Clear();
                cmbProdServicioSATDesabasto.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));

                cmd2.CommandText = "spSATProductosYServicios ";
                cmd2.Connection = conn2;
                conn2.Open();
                using (SqlDataReader sdr2 = cmd2.ExecuteReader())
                {
                    while (sdr2.Read())
                    {
                        RadComboBoxItem item2 = new RadComboBoxItem();
                        item2.Value = sdr2["Cve"].ToString();
                        item2.Text = sdr2["Descripcion"].ToString();
                        item2.Attributes.Add("Cve", sdr2["Cve"].ToString());
                        item2.Attributes.Add("Descripcion", sdr2["Descripcion"].ToString());
                        cmbProdServicioSATDesabasto.Items.Add(item2);
                        item2.DataBind();
                    }
                }

                conn2.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void LlenaComboListadoSATCte()
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.AppSettings["strConnection"].ToString();
                SqlCommand cmd = new SqlCommand();

                cmbUnidadMedidaSATCte.Text = "";
                cmbUnidadMedidaSATCte.ClearSelection();
                cmbUnidadMedidaSATCte.Items.Clear();
                cmbUnidadMedidaSATCte.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));

                cmd.CommandText = "spSATUnidadesMedida ";
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        RadComboBoxItem item = new RadComboBoxItem();
                        item.Value = sdr["Cve"].ToString();
                        item.Text = sdr["Descripcion"].ToString();
                        item.Attributes.Add("Cve", sdr["Cve"].ToString());
                        item.Attributes.Add("Descripcion", sdr["Descripcion"].ToString());
                        cmbUnidadMedidaSATCte.Items.Add(item);

                        item.DataBind();
                    }
                }

                conn.Close();

                SqlConnection conn2 = new SqlConnection();
                conn2.ConnectionString = ConfigurationManager.AppSettings["strConnection"].ToString();
                SqlCommand cmd2 = new SqlCommand();

                cmbProdServicioSATCte.Text = "";
                cmbProdServicioSATCte.ClearSelection();
                cmbProdServicioSATCte.Items.Clear();
                cmbProdServicioSATCte.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));

                cmd2.CommandText = "spSATProductosYServicios ";
                cmd2.Connection = conn2;
                conn2.Open();
                using (SqlDataReader sd2r = cmd2.ExecuteReader())
                {
                    while (sd2r.Read())
                    {
                        RadComboBoxItem item2 = new RadComboBoxItem();
                        item2.Value = sd2r["Cve"].ToString();
                        item2.Text = sd2r["Descripcion"].ToString();
                        item2.Attributes.Add("Cve", sd2r["Cve"].ToString());
                        item2.Attributes.Add("Descripcion", sd2r["Descripcion"].ToString());
                        cmbProdServicioSATCte.Items.Add(item2);
                        item2.DataBind();
                    }
                }

                conn2.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void LlenaComboListadoSATAbasto()
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.AppSettings["strConnection"].ToString();
                SqlCommand cmd = new SqlCommand();

                cmbUnidadMedidaSATAbasto.Text = "";
                cmbUnidadMedidaSATAbasto.ClearSelection();
                cmbUnidadMedidaSATAbasto.Items.Clear();
                cmbUnidadMedidaSATAbasto.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));

                cmd.CommandText = "spSATUnidadesMedida ";
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        RadComboBoxItem item = new RadComboBoxItem();
                        item.Value = sdr["Cve"].ToString();
                        item.Text = sdr["Descripcion"].ToString();
                        item.Attributes.Add("Cve", sdr["Cve"].ToString());
                        item.Attributes.Add("Descripcion", sdr["Descripcion"].ToString());
                        cmbUnidadMedidaSATAbasto.Items.Add(item);

                        item.DataBind();
                    }
                }

                conn.Close();

                SqlConnection conn2 = new SqlConnection();
                conn2.ConnectionString = ConfigurationManager.AppSettings["strConnection"].ToString();
                SqlCommand cmd2 = new SqlCommand();

                cmbProdServicioSATAbasto.Text = "";
                cmbProdServicioSATAbasto.ClearSelection();
                cmbProdServicioSATAbasto.Items.Clear();
                cmbProdServicioSATAbasto.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
                cmd2.CommandText = "spSATProductosYServicios ";
                cmd2.Connection = conn2;
                conn2.Open();
                using (SqlDataReader sd2r = cmd2.ExecuteReader())
                {
                    while (sd2r.Read())
                    {
                        RadComboBoxItem item2 = new RadComboBoxItem();
                        item2.Value = sd2r["Cve"].ToString();
                        item2.Text = sd2r["Descripcion"].ToString();
                        item2.Attributes.Add("Cve", sd2r["Cve"].ToString());
                        item2.Attributes.Add("Descripcion", sd2r["Descripcion"].ToString());
                        cmbProdServicioSATAbasto.Items.Add(item2);
                        item2.DataBind();
                    }
                }
                conn2.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void LlenaComboListadoProductosCL()
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.AppSettings["strConnection"].ToString();
                SqlCommand cmd = new SqlCommand();

                cmbProductosHabiliCompraLocal.Text = "";
                cmbProductosHabiliCompraLocal.ClearSelection();
                cmbProductosHabiliCompraLocal.Items.Clear();

                cmd.CommandText = "spAABuscaProductosCompraLocalTodos ";
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        //  providers.Add(string.Format("{0}-{1}", sdr["Id_Prd"], sdr["Producto"]));

                        RadComboBoxItem item = new RadComboBoxItem();


                        item.Value = sdr["Id_Prd"].ToString();
                        item.Text = sdr["Producto"].ToString();

                        item.Attributes.Add("Ids", sdr["Id_Prd"].ToString());
                        item.Attributes.Add("Producto", sdr["Producto"].ToString());

                        cmbProductosHabiliCompraLocal.Items.Add(item);

                        item.DataBind();
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void LlenarFormularioProductoAbasto_funcion(long id_Producto, int id_Cd_Prod)
        {
            try
            {
                Producto producto = ConsultarPorducto(id_Producto, id_Cd_Prod);
                lblId_Prd.Text = producto.Id_Prd.ToString();
                lblCodProd.Text = producto.Prd_Unico == 0 ? string.Empty : producto.Prd_Unico.ToString();
                chkActivoAbasto.Checked = producto.Prd_Activo == 1 ? true : false;

                lblPrd_Descrpcion.Text = producto.Prd_Descripcion;
                lblPrd_DescrpcionAbasto.Value = producto.Prd_Descripcion;
                lblTituloProducto.Text = string.Concat(producto.Id_Prd.ToString(), " - ", producto.Prd_Descripcion);
                lblPresentacion.Text = producto.Prd_Presentacion;

                lblId_Spo.Text = string.Empty;
                lblId_Spo.Text = "";
                lblcmbSisProp.Text = "";
                if (producto.Id_Spo != 0)
                {
                    lblId_Spo.Text = producto.Id_Spo.ToString();
                    cmbGeneAbasto.Items.Clear();
                    LlenarComboSisPropietario(ref cmbGeneAbasto);
                    cmbGeneAbasto.SelectedValue = producto.Id_Spo.ToString();
                    lblcmbSisProp.Text = cmbGeneAbasto.Items[cmbGeneAbasto.SelectedIndex].Text;
                }

                lblTipoProducto.Text = producto.Id_Ptp.ToString();
                cmbGeneAbasto.Items.Clear();
                ///  Se tenia de manera incorrecta esta referncia, pues no se trata de un combo de sistemas propietarios, sino de tipo de producto
                //  LlenarComboSisPropietario(ref cmbGeneAbasto);
                LlenarComboProductoTipo(ref cmbGeneAbasto);
                cmbGeneAbasto.SelectedValue = producto.Id_Ptp.ToString();
                lblcmbTipoProducto.Text = cmbGeneAbasto.Items[cmbGeneAbasto.SelectedIndex].Text;

                lblCategoria.Text = string.Empty;
                lblcmbCategoria.Text = "";
                if (producto.Id_Cpr != 0)
                {
                    lblCategoria.Text = producto.Id_Cpr.ToString();
                    cmbGeneAbasto.Items.Clear();
                    LlenarComboProductoCategoria(ref cmbGeneAbasto);
                    cmbGeneAbasto.SelectedValue = producto.Id_Cpr.ToString();
                    lblcmbCategoria.Text = cmbGeneAbasto.Items[cmbGeneAbasto.SelectedIndex].Text;
                }
                lblFam.Text = string.Empty;
                lblcmbFam.Text = "";
                hddFamiliaAbasto.Value = "";
                if (producto.Id_Fam != 0)
                {
                    lblFam.Text = producto.Id_Fam.ToString();
                    hddFamiliaAbasto.Value = producto.Id_Fam.ToString();
                    cmbGeneAbasto.Items.Clear();
                    LlenarComboProductoFamilia(ref cmbGeneAbasto);
                    cmbGeneAbasto.SelectedValue = producto.Id_Fam.ToString();
                    lblcmbFam.Text = cmbGeneAbasto.Items[cmbGeneAbasto.SelectedIndex].Text;
                }
                lblSubFam.Text = string.Empty;
                lblcmbSubFam.Text = "";
                cmbGeneAbastoSubFam.Items.Clear();
                if (producto.Id_Sub != 0)
                {
                    //  this.LlenarComboProductoSubFamilia(producto.Id_Fam);
                    lblSubFam.Text = producto.Id_Sub.ToString();
                    LlenarComboProductoSubFamilia(Convert.ToInt32(lblFam.Text), ref cmbGeneAbastoSubFam);
                    cmbGeneAbastoSubFam.SelectedValue = producto.Id_Sub.ToString();
                    lblcmbSubFam.Text = cmbGeneAbastoSubFam.Items[cmbGeneAbastoSubFam.SelectedIndex].Text;
                }


                lblProveedor.Text = ""; //  producto.Id_Pvd == 100 ? string.Empty : producto.Id_Pvd.ToString();     /// producto.Id_Pvd.ToString();
                txtProveeAba.Text = "";     // producto.Id_Pvd == 100 ? "-1" : producto.Id_Pvd.ToString();     /// producto.Id_Pvd.ToString();
                cmbProveedorAba.Items.Clear();
                LlenarComboProveedores(1, ref cmbProveedorAba);
                // cmbProveedorAba.SelectedValue = "-1"; // producto.Id_Pvd == 100 ? "-1" : producto.Id_Pvd.ToString();     /// producto.Id_Pvd.ToString();
                lblcmbProveedor.Text = cmbProveedorAba.Items[cmbProveedorAba.SelectedIndex].Text;


                lblcmbUentrada.Text = producto.Prd_UniNe.ToString();
                lbltxtFactorConversion.Text = producto.Prd_FactorConv.ToString();
                lblcmbUsalida.Text = producto.Prd_UniNs;
                lbltxtUempaque.Text = producto.Prd_UniEmp.ToString();

                hidProductoOriginal.Value = producto.Prd_Unico.ToString();

                txtAgrupadoSpo.Text = producto.Prd_AgrupadoSpo == 0 ? string.Empty : producto.Prd_AgrupadoSpo.ToString();
                //  lbltxtContribucion.Text = producto.Prd_Contribucion.ToString();
                //  lbltxtPorUtilidades.Text = producto.Prd_PorUtilidades.ToString();
                /// Panel 2
                lbltxtInvSeguridad.Text = producto.Prd_InvSeg.ToString();
                chkPropietarioAbasto.Checked = (bool)producto.Prd_AparatoSisProp;
                lbltxtTentrega.Text = producto.Prd_TEntre.ToString();
                lbltxtTtransporte.Text = producto.Prd_TTrans.ToString();

                //  lbllblRentabilidad.SelectedIndex = lbllblRentabilidad.FindItemIndexByText(producto.Prd_Ren.ToString(), true);
                //  lbltxtRentabilidad.Text = producto.Prd_Ren.ToString();

                lbltxtMinCompra.Text = producto.Prd_Minimo.ToString();
                //  chkComprasLocalesAbasto.Checked = producto.Prd_Colo;
                lbltxtAmortizacion.Text = producto.Prd_Mes.ToString();
                lbltxtPesos.Text = producto.Prd_PesConTecnico.ToString();
                lbltxtExistencia.Text = producto.Prd_MaxExistencia.ToString();
                lbltxtUbicacion.Text = producto.Prd_Ubicacion;

                lbltxtAsignado.Text = producto.Prd_Asignado.ToString();
                lbltxtInicial.Text = producto.Prd_InvInicial.ToString();
                lbltxtOrdenado.Text = producto.Prd_Ordenado.ToString();
                lbltxtFinal.Text = producto.Prd_InvFinal.ToString();
                lbltxtTransito.Text = producto.Prd_Transito.ToString();
                lbltxtFisico.Text = producto.Prd_Fisico.ToString();

                lbltxtPlanAbasto.Text = producto.Prd_PlanAbasto;
                strIdPrd = id_Producto.ToString();
                /// Panel 4
                this.listSource = this.ConsultarPorductoPrecios(id_Producto);

                // -- fixed para poner en 0 el costo actual y el AAA
                for (int i = 0; i < this.listSource.Count; i++)
                {
                    if (listSource[i].Prd_Actual)
                    {
                        if (listSource[i].Pre_Descripcion == "COSTO")
                        {
                            hddPrecioCostoOriginal.Value = listSource[i].Prd_Pesos.ToString();
                            //  listSource[i].Prd_Pesos = 0;
                        }

                        if (listSource[i].Pre_Descripcion == "AAA")
                        {
                            hddPrecioAAAOriginal.Value = listSource[i].Prd_Pesos.ToString();
                            //listSource[i].Prd_Pesos = 0;
                        }
                    }
                }

                lbltxtFnombre.Text = producto.Prd_CLNomFab;
                lbltxtFcodigo.Text = producto.Prd_CLIdFab;
                lbltxtFdescripcion.Text = producto.Prd_CLDesFab;
                lbltxtFpresentacion.Text = producto.Prd_CLPreFab;

                txtPnombreAbasto.Text = producto.Prd_CLNomPro;

                txtSearchProvAbasto.Text = producto.Prd_CLNomPro;

                lbltxtPcodigo.Text = producto.Prd_CLIdPro;
                lbltxtPdescripcion.Text = producto.Prd_CLDesPro;
                lbltxtPpresentacion.Text = producto.Prd_CLPrePro;



                try
                {


                    cmbProdServicioSATAbasto.SelectedIndex = cmbProdServicioSATAbasto.FindItemIndexByValue(producto.Prd_ClaveProdServ.ToString());
                    cmbProdServicioSATAbasto.Text = cmbProdServicioSATAbasto.FindItemByValue(producto.Prd_ClaveProdServ.ToString()).Text;

                    txtSearchProdServSATAbasto.Value = cmbProdServicioSATAbasto.FindItemByValue(producto.Prd_ClaveProdServ.ToString()).Text;
                    hfCveProdServSATAbasto.Value = producto.Prd_ClaveProdServ.ToString();


                    cmbUnidadMedidaSATAbasto.SelectedIndex = cmbUnidadMedidaSATAbasto.FindItemIndexByValue(producto.Prd_ClaveUnidad.ToString());
                    cmbUnidadMedidaSATAbasto.Text = cmbUnidadMedidaSATAbasto.FindItemByValue(producto.Prd_ClaveUnidad.ToString()).Text;

                }
                catch
                {
                    cmbProdServicioSATAbasto.SelectedIndex = cmbProdServicioSATAbasto.FindItemIndexByValue("01010101");
                    cmbProdServicioSATAbasto.Text = cmbProdServicioSATAbasto.FindItemByValue("01010101").Text;

                    txtSearchProdServSATAbasto.Value = cmbProdServicioSATAbasto.FindItemByValue("01010101").Text;
                    hfCveProdServSATAbasto.Value = "01010101";


                    cmbUnidadMedidaSATAbasto.SelectedIndex = cmbUnidadMedidaSATAbasto.FindItemIndexByValue("H87");
                    cmbUnidadMedidaSATAbasto.Text = cmbUnidadMedidaSATAbasto.FindItemByValue("H87").Text;

                    throw;
                }

                cmbProdServicioSATAbasto.Enabled = true;
                cmbUnidadMedidaSATAbasto.Enabled = true;
                txtSearchProdServSATAbasto.Disabled = false;


                //**********************************//
                //* Consultar precios de productos *//
                //**********************************//
                this.IdProducto = id_Producto;
                rgPreciosAbasto.Enabled = true;
                rgPreciosAbasto.Rebind();
                // Si es consulta de producto de compra local
                // se habilitan controles de la pestaña de compras locales
                //   if (producto.Prd_Colo)
                //                      rgPreciosAbasto.Enabled = false;
                //  this.HabilitaCamposComprasLocales(true);
                //   else
                //       this.HabilitaCamposComprasLocales(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LlenarFormularioProductoSoliCliente(int id_Producto, int id_Cd_Prod)
        {
            try
            {

                Producto producto = ConsultarPorducto(id_Producto, id_Cd_Prod);
                TextId_PrdCte.Text = producto.Id_Prd.ToString();
                txtCodProdCte.Value = producto.Prd_Unico == 0 ? string.Empty : producto.Prd_Unico.ToString();
                chkActivoCte.Checked = producto.Prd_Activo == 1 ? true : false;

                TextPrd_DescrpcionCte.Text = producto.Prd_Descripcion;
                //  lblTituloProducto.Text = string.Concat(producto.Id_Prd.ToString(), " - ", producto.Prd_Descripcion);
                txtPresentacionCte.Text = producto.Prd_Presentacion;

                TextId_SpoCte.Text = string.Empty;

                txtTipoProductoCte.Text = producto.Id_Ptp.ToString();
                cmbTipoProductoCte.SelectedValue = producto.Id_Ptp.ToString();
                txtCategoriaCte.Text = string.Empty;
                cmbCategoriaCte.Text = "";
                cmbCategoriaCte.ClearSelection();
                if (producto.Id_Cpr != 0)
                {
                    txtCategoriaCte.Text = producto.Id_Cpr.ToString();
                    cmbCategoriaCte.SelectedValue = producto.Id_Cpr.ToString();
                }
                txtFamCte.Text = string.Empty;
                cmbFamCte.Text = "";
                cmbFamCte.ClearSelection();
                if (producto.Id_Fam != 0)
                {
                    txtFamCte.Text = producto.Id_Fam.ToString();
                    cmbFamCte.SelectedValue = producto.Id_Fam.ToString();
                }
                txtSubFamCte.Text = string.Empty;
                cmbSubFamCte.Text = "";
                cmbSubFamCte.ClearSelection();
                cmbSubFamCte.Items.Clear();
                if (producto.Id_Sub != 0)
                {
                    this.LlenarComboProductoSubFamilia(producto.Id_Fam, ref cmbSubFamCte);
                    txtSubFamCte.Text = producto.Id_Sub.ToString();
                    cmbSubFamCte.SelectedValue = producto.Id_Sub.ToString();
                }
                txtProveedorCte.Text = ""; //        producto.Id_Pvd == 100 ? string.Empty : producto.Id_Pvd.ToString();
                cmbProveedorCte.SelectedValue = "-1";       // producto.Id_Pvd == 100 ? "-1" : producto.Id_Pvd.ToString(); //  producto.Id_Pvd.ToString();
                cmbUentradaCte.SelectedValue = producto.Prd_UniNe;
                //  txtFactorConversionCte.Text = producto.Prd_FactorConv.ToString();
                cmbUsalidaCte.SelectedValue = producto.Prd_UniNs;
                txtUempaqueCte.Text = producto.Prd_UniEmp.ToString();

                /// txtAgrupadoSpoCte.Text = producto.Prd_AgrupadoSpo == 0 ? string.Empty : producto.Prd_AgrupadoSpo.ToString();
                //  txtContribucionCte.Text = producto.Prd_Contribucion.ToString();
                //  txtPorUtilidadesCte.Text = producto.Prd_PorUtilidades.ToString();

                txtInvSeguridadCte.Text = producto.Prd_InvSeg.ToString();
                if (producto.Prd_AparatoSisProp != null)
                {
                    chkSistPropCte.Checked = (bool)producto.Prd_AparatoSisProp;
                }
                txtTentregaCte.Text = producto.Prd_TEntre.ToString();
                txtTtransporteCte.Text = producto.Prd_TTrans.ToString();
                //  cmbRentabilidadCte.SelectedIndex = cmbRentabilidadCte.FindItemIndexByText(producto.Prd_Ren.ToString(), true);
                //  txtRentabilidadCte.Text = cmbRentabilidadCte.SelectedValue;
                chkComprasLocalesCte.Checked = producto.Prd_Colo;
                txtAmortizacionCte.Text = producto.Prd_Mes.ToString();
                txtPesosCte.Text = producto.Prd_PesConTecnico.ToString();
                txtExistenciaCte.Text = producto.Prd_MaxExistencia.ToString();
                txtUbicacionCte.Text = producto.Prd_Ubicacion;

                txtAsignadoCte.Text = producto.Prd_Asignado.ToString();
                txtInicialCte.Text = producto.Prd_InvInicial.ToString();
                txtOrdenadoCte.Text = producto.Prd_Ordenado.ToString();
                txtFinalCte.Text = producto.Prd_InvFinal.ToString();
                txtTransitoCte.Text = producto.Prd_Transito.ToString();
                txtFisicoCte.Text = producto.Prd_Fisico.ToString();
                txtMinCompraCte.Text = producto.Prd_Minimo.ToString();
                txtPlanAbastoCte.Text = producto.Prd_PlanAbasto;
                //  no hay historia del producto puesto que es una solicitud expresa del cliente 
                //  strIdPrd = id_Producto.ToString();
                this.listSource = this.ConsultarPorductoPrecios(id_Producto);

                // -- fixed para poner en 0 el costo actual y el AAA
                for (int i = 0; i < this.listSource.Count; i++)
                {
                    if (listSource[i].Prd_Actual)
                    {
                        if (listSource[i].Pre_Descripcion == "COSTO")
                        {
                            listSource[i].Prd_Pesos = 0;
                        }

                        if (listSource[i].Pre_Descripcion == "AAA")
                        {
                            listSource[i].Prd_Pesos = 0;
                        }
                    }
                }

                txtFnombreCte.Text = producto.Prd_CLNomFab;
                txtFcodigoCte.Text = producto.Prd_CLIdFab;
                txtFdescripcionCte.Text = producto.Prd_CLDesFab;
                txtFpresentacionCte.Text = producto.Prd_CLPreFab;
                txtPnombreCte.Text = producto.Prd_CLNomPro;

                txtPnombreCte.Text = producto.Prd_CLNomPro;

                txtSearchProvCte.Text = producto.Prd_CLNomPro;

                txtPcodigoCte.Text = producto.Prd_CLIdPro;
                txtPdescripcionCte.Text = producto.Prd_CLDesPro;
                txtPpresentacionCte.Text = producto.Prd_CLPrePro;

                //**********************************/
                //* Consultar precios de productos *//
                //**********************************//

                //  rgPreciosCte.Enabled = false;
                rgPreciosCte.Rebind();

                listaClientes.DataBind();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LlenaComboProducto()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.AppSettings["strConnection"].ToString();
            SqlCommand cmd = new SqlCommand();
            string txtcmd;
            string prodName;
            string SubFa;
            RadComboBoxProduct.Text = "";
            RadComboBoxProduct.ClearSelection();
            RadComboBoxProduct.Items.Clear();
            SubFa = cmdSubFamiliaSoli.SelectedValue.ToString();
            cmd.CommandText = "spAABuscaProductoSubFam ' ', " + SubFa;
            cmd.Connection = conn;
            conn.Open();
            using (SqlDataReader sdr = cmd.ExecuteReader())
            {
                while (sdr.Read())
                {
                    //  providers.Add(string.Format("{0}-{1}", sdr["Id_Prd"], sdr["Producto"]));

                    RadComboBoxItem item = new RadComboBoxItem();

                    item.Text = sdr["Descripcion"].ToString();
                    item.Value = sdr["Id_Prd"].ToString();

                    item.Attributes.Add("Ids", sdr["Id_Prd"].ToString());
                    item.Attributes.Add("Producto", sdr["Descripcion"].ToString());

                    RadComboBoxProduct.Items.Add(item);

                    item.DataBind();
                }
            }
            conn.Close();
        }

        private void ComentariosYVigenciaSolicitudConsylta(int Solicitud, int tipo)
        {
            string Values = "";
            string textos = "";

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.AppSettings["strConnection"].ToString();
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "spComentariosCompraLocal_Consulta " + Solicitud.ToString();
            cmd.Connection = conn;
            conn.Open();
            using (SqlDataReader sdr = cmd.ExecuteReader())
            {
                while (sdr.Read())
                {
                    this.txtMotivoSolicitaCons.Text = sdr["Comentarios"].ToString();
                    if (tipo == 1)
                    { cmbCausaDesabastoCons.SelectedValue = cmbCausaDesabastoCons.FindItemByText(sdr["Comentarios"].ToString()).Value; }

                    //  cmbCausaDesabastoCons.SelectedItem = sdr["Comentarios"].ToString();

                    if (sdr["Vigencia"].ToString() != "01/01/1900 12:00:00 a.m.")
                    {
                        if (sdr["Vigencia"].ToString() != "")
                        {
                            this.rdpVigenciaCons.SelectedDate = Convert.ToDateTime(sdr["Vigencia"].ToString());
                            rdpVigenciaCons.Enabled = false;
                        }
                    }
                }
            }
            conn.Close();
        }

        private void LlenaListaClientes(int Solicitud)
        {
            string Values = "";
            string textos = "";
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.AppSettings["strConnection"].ToString();
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "ConsultaProductoExXSolicitud " + Solicitud.ToString();
            cmd.Connection = conn;
            conn.Open();
            using (SqlDataReader sdr = cmd.ExecuteReader())
            {
                while (sdr.Read())
                {
                    ListItem item = new ListItem();

                    item.Text = sdr["Id_Cliente"].ToString() + " | " + sdr["Cliente"].ToString();
                    item.Value = sdr["Id_Cliente"].ToString();

                    this.lstClientesAutorizadosCons.Items.Add(item);

                    Values = Values + item.Value + ";";
                    textos = textos + item.Text + ";";

                }
            }
            this.ddlElementsCons.Value = Values;
            this.ddlElementsFullCons.Value = textos;
            this.hddListaClientesOriginal.Value = textos;
            this.lstClientesAutorizadosCons.DataBind();
            conn.Close();
        }

        private void BuscaSolicitudesCombo(int SolComId, int ProduId, int ProveId)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CompraLocal cl = new CompraLocal();

            CN_ComprasLocales cn_Listadocompralocal = new CN_ComprasLocales();
            List<CompraLocal> v = new List<CompraLocal>();

            rgDetalleSolicitud.DataBind();

            cn_Listadocompralocal.ConsultaSolicitudCombo(SolComId, ProduId, ProveId, Sesion.Emp_Cnx, ref v);
            rgCompraLocal.DataSource = v;
            rgCompraLocal.Rebind();
        }

        private void BuscaSolicitudesPorProveedor(int ProveId)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CompraLocal cl = new CompraLocal();

            CN_ComprasLocales cn_Listadocompralocal = new CN_ComprasLocales();
            List<CompraLocal> v = new List<CompraLocal>();

            rgDetalleSolicitud.DataBind();

            cn_Listadocompralocal.ConsultaSolicitudXProveedor(ProveId, Sesion.Emp_Cnx, ref v);
            rgCompraLocal.DataSource = v;
            rgCompraLocal.Rebind();
        }

        private void ConsultaSolicitudXProducto(int ProduId)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CompraLocal cl = new CompraLocal();

            CN_ComprasLocales cn_Listadocompralocal = new CN_ComprasLocales();
            List<CompraLocal> v = new List<CompraLocal>();

            rgDetalleSolicitud.DataBind();

            cn_Listadocompralocal.ConsultaSolicitudXProducto(ProduId, Sesion.Emp_Cnx, ref v);
            rgCompraLocal.DataSource = v;
            rgCompraLocal.Rebind();
        }

        private void BuscaSolicitudesDirecta(int SolComId)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CompraLocal cl = new CompraLocal();

            CN_ComprasLocales cn_Listadocompralocal = new CN_ComprasLocales();
            List<CompraLocal> v = new List<CompraLocal>();

            rgDetalleSolicitud.DataBind();

            cn_Listadocompralocal.ConsultaSolicitudDirecta(SolComId, Sesion.Emp_Cnx, ref v);
            rgCompraLocal.DataSource = v;
            rgCompraLocal.Rebind();
        }

        private void ListadoCatAbasto()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            CN_ComprasLocales cn_Listadocompralocal = new CN_ComprasLocales();

            DataTable CatDesabasto = new DataTable();
            CatDesabasto.Columns.Add("Id_Causa");
            CatDesabasto.Columns.Add("Desc_CausaDesAbasto");
            CatDesabasto.Columns.Add("Activo");

            cn_Listadocompralocal.CatDesConsulta(ref CatDesabasto, Sesion.Emp_Cnx);
            rgCausasDesabasto.DataSource = CatDesabasto;
            rgCausasDesabasto.Rebind();
        }

        private void ListadoMotivosCL()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            CN_ComprasLocales cn_Listadocompralocal = new CN_ComprasLocales();

            DataTable CatMotivo = new DataTable();
            CatMotivo.Columns.Add("Id_MotivoCL");
            CatMotivo.Columns.Add("Desc_MotivoCL");
            CatMotivo.Columns.Add("PorcentajeAAA");
            CatMotivo.Columns.Add("Aplica");

            cn_Listadocompralocal.CatMotivoCL(ref CatMotivo, Sesion.Emp_Cnx);
            rgMotivosCL.DataSource = CatMotivo;
            rgMotivosCL.Rebind();
        }

        private void ListadoCorreosAutorizador()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            CN_Comun.LlenaCombo(Sesion.Emp_Cnx, "spCatAplicacion_Combo", ref cmbAplicacionMotCL);
            cmbAplicacionMotCL.Items.Insert(0, new RadComboBoxItem("Todas", "-1"));
            cmbAplicacionMotCL.Items.Insert(cmbAplicacionMotCL.Items.Count(), new RadComboBoxItem("-|- Otros -|-", "0"));

            CN_Comun.LlenaCombo(Sesion.Emp_Cnx, "spCatMotivoCompraLocalAplica_Combo", ref cmbMotivoCL);
            cmbMotivoCL.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
            cmbMotivoCL.Items.Insert(cmbMotivoCL.Items.Count(), new RadComboBoxItem("-|- Todos -|-", "0"));

            CN_Comun.LlenaCombo(2, Sesion.Id_Emp, Sesion.Id_U, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref cmbCDIMotivoCL);


            CN_ComprasLocales cn_Listadocompralocal = new CN_ComprasLocales();

            DataTable CatCorreos = new DataTable();
            CatCorreos.Columns.Add("Id_Emp");
            CatCorreos.Columns.Add("Id_Cd");
            CatCorreos.Columns.Add("Id_Conf");
            CatCorreos.Columns.Add("Id_MotivoCL");
            CatCorreos.Columns.Add("Secuencia");
            CatCorreos.Columns.Add("Desc_MotivoCL");
            CatCorreos.Columns.Add("Id_Aplicacion");
            CatCorreos.Columns.Add("Aplicacion");
            CatCorreos.Columns.Add("Concepto");
            CatCorreos.Columns.Add("Correo");

            cn_Listadocompralocal.CatCorreosCL(ref CatCorreos, Sesion.Id_Emp, Sesion.Id_Cd, Sesion.Emp_Cnx);
            rgCorreosAutorizadores.DataSource = CatCorreos;
            rgCorreosAutorizadores.Rebind();
        }

        private void LimpiaCausas()
        {
            this.txtCatCausaDes.Text = "";
        }

        private void LlenarFormularioProductoSoliConsulta(long id_Producto, int id_Cd_Prod)
        {
            try
            {

                Producto producto = ConsultarPorducto(id_Producto, id_Cd_Prod);
                this.TextId_PrdCons.Text = producto.Id_Prd.ToString();
                txtCodProdCons.Text = producto.Prd_Unico == 0 ? string.Empty : producto.Prd_Unico.ToString();
                chkActivoCons.Checked = producto.Prd_Activo == 1 ? true : false;

                TextPrd_DescrpcionCons.Text = producto.Prd_Descripcion;
                //  lblTituloProducto.Text = string.Concat(producto.Id_Prd.ToString(), " - ", producto.Prd_Descripcion);
                txtPresentacionCons.Text = producto.Prd_Presentacion;

                txtTipoProductoCons.Text = producto.Id_Ptp.ToString();
                cmbTipoProductoCons.SelectedValue = producto.Id_Ptp.ToString();
                txtCategoriaCons.Text = string.Empty;
                cmbCategoriaCons.Text = "";
                cmbCategoriaCons.ClearSelection();
                if (producto.Id_Cpr != 0)
                {
                    txtCategoriaCons.Text = producto.Id_Cpr.ToString();
                    cmbCategoriaCons.SelectedValue = producto.Id_Cpr.ToString();
                }
                txtFamCons.Text = string.Empty;
                cmbFamCons.Text = "";
                cmbFamCons.ClearSelection();
                if (producto.Id_Fam != 0)
                {
                    txtFamCons.Text = producto.Id_Fam.ToString();
                    cmbFamCons.SelectedValue = producto.Id_Fam.ToString();
                }
                txtSubFamCons.Text = string.Empty;
                cmbSubFamCons.Text = "";
                cmbSubFamCons.ClearSelection();
                cmbSubFamCons.Items.Clear();
                if (producto.Id_Sub != 0)
                {
                    this.LlenarComboProductoSubFamilia(producto.Id_Fam, ref cmbSubFamCons);
                    txtSubFamCons.Text = producto.Id_Sub.ToString();
                    cmbSubFamCons.SelectedValue = producto.Id_Sub.ToString();
                }
                txtProveedorCons.Text = producto.Id_Pvd == 100 ? string.Empty : producto.Id_Pvd.ToString(); //producto.Id_Pvd.ToString();
                cmbProveedorCons.SelectedValue = producto.Id_Pvd == 100 ? "-1" : producto.Id_Pvd.ToString(); // producto.Id_Pvd.ToString();
                cmbUentradaCons.SelectedValue = producto.Prd_UniNe;
                //  txtFactorConversionCte.Text = producto.Prd_FactorConv.ToString();
                cmbUsalidaCons.SelectedValue = producto.Prd_UniNs;
                txtUempaqueCons.Text = producto.Prd_UniEmp.ToString();
                strIdPrd = id_Producto.ToString();
                this.listSource = this.ConsultarPorductoPrecios(id_Producto);

                // -- fixed para poner en 0 el costo actual y el AAA
                /*
                for (int i = 0; i < this.listSource.Count; i++)
                {
                    if (listSource[i].Prd_Actual)
                    {
                        if (listSource[i].Pre_Descripcion == "COSTO")
                        {
                            listSource[i].Prd_Pesos = 0;
                        }

                        if (listSource[i].Pre_Descripcion == "AAA")
                        {
                            listSource[i].Prd_Pesos = 0;
                        }
                    }
                }
                */

                this.rgPreciosCons.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void CancelarSolicitudConsulta(object sender, EventArgs e)
        {
            this.divConsulta.Visible = false;
            titSolicitud.Text = "";
            this.rgCompraLocal.Visible = true;
            TextId_PrdCons.Text = string.Empty;
            txtCodProdCons.Text = string.Empty;
            chkActivoCons.Checked = false;
            TextPrd_DescrpcionCons.Text = string.Empty;
            txtPresentacionCons.Text = string.Empty;
            txtTipoProductoCons.Text = string.Empty;
            cmbTipoProductoCons.ClearSelection();
            txtCategoriaCons.Text = string.Empty;
            cmbCategoriaCons.Text = "";
            cmbCategoriaCons.ClearSelection();
            txtFamCons.Text = string.Empty;
            cmbFamCons.Text = "";
            cmbFamCons.ClearSelection();
            txtSubFamCons.Text = string.Empty;
            cmbSubFamCons.Text = "";
            cmbSubFamCons.ClearSelection();
            cmbSubFamCons.Items.Clear();

            txtProveedorCons.Text = string.Empty;
            cmbProveedorCons.Text = "";
            cmbProveedorCons.ClearSelection();
            cmbUentradaCons.Text = "";
            cmbUentradaCons.ClearSelection();
            cmbUsalidaCons.Text = "";
            cmbUsalidaCons.ClearSelection();
            txtUempaqueCons.Text = string.Empty;

            txtMotivoSolicitaCons.Text = string.Empty;
            //  rdpVigenciaCons.SelectedDate = "";

            rgPreciosCons.DataSource = null;
            rgPreciosCons.Rebind();

            this.lstClientesAutorizadosCons.Items.Clear();

            this.rgCompraLocal.Rebind();
            LimpiarCampos4();
        }

        protected void GrabaSolicitudConsulta(object sender, EventArgs e)
        {
            string productName = this.TextPrd_DescrpcionCons.Text;
            string providerName = this.cmbProveedorCons.Text;

            try
            {
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatProducto clsCatProducto = new CN_CatProducto();
                int verificador = -1;
                int TipoSoli = Convert.ToInt32(hfTipooSolicitudCons.Value);

                // genera un nuevo codigo para el nuevo producto
                Producto producto = this.LlenatObjetoProductoSolicitudConsulta();

                // graba los cambios que se haigan hecho sobre el producto
                clsCatProducto.ModificarProducto_CL(producto, session.Emp_Cnx, ref verificador);

                /// Graba la Solicitud en si, cada solicitud va con un producto por vuelta.
                Funciones funciones = new Funciones();
                CompraLocal cl = new CompraLocal();
                cl.Id_Emp = session.Id_Emp;
                cl.Id_Cd = session.Id_Cd_Ver;
                cl.FechaSol = funciones.GetLocalDateTime(session.Minutos);
                cl.Id_Comp = Convert.ToInt32(hfNumSolicitudCons.Value);
                cl.Comp_Solicito = session.Id_U;
                verificador = 0;
                CN_ProCompraLocal cn_procompralocal = new CN_ProCompraLocal();

                /// se llena un dataset con el dato del producto nuevo para enviar a generar la solicitud
                Creardt();
                /// // 1 producto = 1 solicitud
                foreach (ProductoPrecios productoPrecios in producto.ListaProductoPrecios)
                {
                    if (productoPrecios.Pre_Descripcion == "COSTO" && productoPrecios.Prd_Actual == true)
                    {
                        dt.Rows.Add(new object[] { producto.Id_Prd, producto.Prd_Descripcion, Convert.ToDouble(productoPrecios.Prd_Pesos).ToString("#,##0.00"), 0, "Sin autorizar" });
                    }
                }

                cn_procompralocal.ModificarSolicitud(cl, dt, session.Emp_Cnx, ref verificador);

                verificador = Convert.ToInt32(hfNumSolicitudCons.Value);

                /// graba los comentarios y la fecha de vigencia

                string FVigencia = this.rdpVigenciaCons.ValidationDate.ToString();
                if (TipoSoli == 1)
                {
                    this.txtMotivoSolicitaCons.Text = this.cmbCausaDesabastoCons.Text;
                }

                string Comentarios = this.txtMotivoSolicitaCons.Text;
                int grabo = 0;
                CN_ComprasLocales comLoc = new CN_ComprasLocales();

                comLoc.GrabaComentariosCliente(Comentarios, FVigencia, TipoSoli, verificador, session.Emp_Cnx, ref grabo);



                if (this.divPedidosRefCons.Visible == true)
                {

                    string PedidoRef = "";
                    /// aqui verifica si tiene pedidos seleccionados
                    foreach (ListItem itemCh in this.chklstPedidosCons.Items)
                    {
                        if (itemCh.Selected == true)
                        {
                            PedidoRef = PedidoRef + itemCh.Value.ToString() + ',';
                        }
                    }

                    if (PedidoRef == "")
                    {
                        Alerta("DEbe de seleccionar al menos un pedido desabastecido.");
                        return;
                    }
                    else
                    {
                        // OJO AQUI LA ORIGINAL CAMBIO, PARA CAMBIAR LA CONSULTA CUANDO SEA SOLICITADO

                        txtValuesPedidos.Text = PedidoRef;
                        GrabaPedidodeListaPedidos(verificador, producto.Id_Prd, this.txtValuesPedidos);    // this.lstbPedidosCons);
                        PedidoRef = txtValuesPedidos.Text;
                    }
                }


                /// Graba el grid de clientes exclusivos
                int cliente = 0;
                if (ddlElementsCons.Value != "")
                {

                    comLoc.EliminaClientesExclusivos(verificador, session.Emp_Cnx, ref grabo);

                    string[] cadenaclientes = ddlElementsCons.Value.Split(';');

                    foreach (string s in cadenaclientes)
                    {
                        if (s != "")
                        {
                            cliente = Convert.ToInt32(s);
                            comLoc.GrabaClientesExclusivos(producto.Id_Prd, cliente, verificador, session.Emp_Cnx, ref grabo);
                        }
                    }
                }

                /// Regresa el producto al Standby, lo saca de CatProducto
                comLoc.DesAutorizaSolicitud(verificador, session.Emp_Cnx, ref grabo);

                //// Envia el correo

                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                verificador = -1;
                EntradaVirtual pe = new EntradaVirtual();

                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Cd = Sesion.Id_Cd_Ver;
                configuracion.Id_Emp = Sesion.Id_Emp;
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                cn_configuracion.Consulta(ref configuracion, Sesion.Emp_Cnx);

                titSolicitud.Text = "Solicitud " + hfNumSolicitudCons.Value.ToString() + " Modificada!!";
                //  txtMotivoSolicita  rdpVigencia

                StringBuilder cuerpo_correo = new StringBuilder();
                cuerpo_correo.Append("<div align='center'>");
                cuerpo_correo.Append("<table width='800px'><tr><td>");
                cuerpo_correo.Append("<IMG SRC=\"cid:companylogo\" ALIGN='left'></td>");
                cuerpo_correo.Append("</tr><tr><td colspan='2'><br></td>");
                cuerpo_correo.Append("</tr><tr>");
                cuerpo_correo.Append("<td colspan='2' style='font-family:Calibri,sans-serif;mso-bidi-font-family:Calibri; color:black'><br><br><b>");
                cuerpo_correo.Append("Se ha <b>modificado</b> una solicitud de compra local con el folio <b>" + hfNumSolicitudCons.Value + "</b><br>");
                cuerpo_correo.Append("para el proveedor <b>" + providerName + "</b><br>");
                cuerpo_correo.Append("del producto <b>" + producto.Prd_Descripcion + "</b><br>");
                cuerpo_correo.Append("para el centro de distribucion <b>" + session.Id_Cd + " - " + session.Cd_Nombre + "</b><br>");
                cuerpo_correo.Append("modificada por <b>" + session.Id_Emp + " - " + session.Emp_Nombre + "</b>");
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");

                cuerpo_correo.Append("<table width='90%' style='font-family:Calibri,sans-serif;mso-bidi-font-family:Calibri;color:black' border=0 cellpadding=1>");
                cuerpo_correo.Append("<tr><td>Solicito:</td><td colspan='4'>" + session.U_Nombre + " Correo: " + session.U_Correo + "</td></tr>");
                cuerpo_correo.Append("<tr><td>Código del producto:</td><td colspan='4'>" + producto.Id_Prd + "</td></tr>");
                cuerpo_correo.Append("<tr><td>Descripción del producto:</td><td colspan='4'>" + producto.Prd_Descripcion + "</td></tr>");
                //  cuerpo_correo.Append("<tr><td>Presentación:</td><td>" + producto.Prd_Presentacion + "</td><td colspan='3'>&nbsp;</td></tr>");
                cuerpo_correo.Append("<tr><td>Tipo de producto:</td><td colspan='3'>" + cmbTipoProductoCons.Text + "</td><td>&nbsp;</td></tr>");
                cuerpo_correo.Append("<tr><td heigth=30px>&nbsp;</td></tr>");
                if (rdpVigenciaCons.SelectedDate != null)
                {
                    cuerpo_correo.Append("<tr><td>Vigencia de Uso del Codigo:</td><td colspan='3'>" + rdpVigenciaCons.SelectedDate.Value + " </td><td>&nbsp;</td></tr>");
                }
                cuerpo_correo.Append("<tr><td>Motivo de la solicitud:</td><td colspan='3'>" + txtMotivoSolicitaCons.Text.Replace("\r\n", "<br>") + "</td><td>&nbsp;</td></tr>");

                cuerpo_correo.Append("<tr><td>Precios:</td><td colspan='4'>&nbsp;</td></tr><tr><td colspan='4' align=center>");
                string tablaprecios = "<table style='font-family: verdana; font-size: 8pt;' cellspacing=0 border=1 cellpadding=0 width=250px>";
                tablaprecios = tablaprecios + "<tr><td style='background-color:#d9d9d9' ><b>Tipo de Precio</b></td><td align=right style='background-color:#d9d9d9'><b>Precio</b></td></tr>";


                /// // 1 prudcto = 1 solicutd
                string color = "";
                string nuevo = "";
                string nuevo2 = "";
                int reng = 1;
                foreach (ProductoPrecios productoPrecios in producto.ListaProductoPrecios)
                {
                    if (productoPrecios.Prd_Actual == true)
                    {
                        if (reng == 0)
                        {
                            reng = 1;
                            color = "style='background-color:#d9d9d9'";
                        }
                        else
                        {
                            reng = 0;
                            color = "";
                        }

                        tablaprecios = tablaprecios + "<tr><td " + color + ">" + productoPrecios.Pre_Descripcion + "</td>";
                        tablaprecios = tablaprecios + "<td align=right " + color + ">" + Convert.ToDouble(productoPrecios.Prd_Pesos).ToString("#,##0.00") + "</td></tr>";
                    }
                }
                tablaprecios = tablaprecios + "</table>";

                string[] url = Request.Url.ToString().Split(new char[] { '/' });

                cuerpo_correo.Append(tablaprecios);
                cuerpo_correo.Append("</td></tr>");

                string TablaClientes = "";
                ///anexa los clientes exlucivos
                if (cliente > 0)
                {
                    TablaClientes = "<tr><td colspan='4'> Exclusivo para los clientes:&nbsp;</td></tr><tr><td colspan='4' align=center>";
                    TablaClientes = TablaClientes + "<table style='font-family: verdana; font-size: 8pt;' cellspacing=0 border=1 cellpadding=0 width=250px>";
                    //  TablaClientes = TablaClientes + "<tr><td style='background-color:#d9d9d9' ></td></tr>";

                    color = "";
                    reng = 1;

                    string[] cadenaclientesFull = ddlElementsFullCons.Value.Split(';');

                    foreach (string ccte in cadenaclientesFull)
                    {
                        if (ccte != "")
                        {
                            // tener una lista original y comparar el cliente vs esa lista, si no esta, se habilita la imagen, si exites se deja igual

                            string[] cadenaOriginalFull = hddListaClientesOriginal.Value.Split(';');

                            foreach (string ccteOri in cadenaOriginalFull)
                            {
                                if (ccteOri != "")
                                {
                                    if (ccte == ccteOri)
                                    {
                                        nuevo = "";
                                        nuevo2 = "";
                                        break;
                                    }
                                    else
                                    {
                                        nuevo = "<span style='color:#4747d1'><b>";
                                        nuevo2 = "</b></span>";
                                    }
                                }
                            }

                            if (reng == 0)
                            {
                                reng = 1;
                                color = "style='background-color:#d9d9d9'";
                            }
                            else
                            {
                                reng = 0;
                                color = "";
                            }

                            TablaClientes = TablaClientes + "<tr><td " + color + ">" + nuevo + ccte + nuevo2 + "</td></tr>";
                            nuevo = "";
                            nuevo2 = "";
                        }
                    }
                    TablaClientes = TablaClientes + "</table>";

                    cuerpo_correo.Append(TablaClientes);
                }
                this.lstClientesAutorizadosCons.Items.Clear();

                cuerpo_correo.Append("</table>");
                cuerpo_correo.Append("</td></tr><tr><td colspan='2' align=center>");
                cuerpo_correo.Append("<br>");
                cuerpo_correo.Append("<a style='font-family: verdana; font-size: 8pt;' href='" + Request.Url.ToString().Replace(url[url.Length - 1], "") + "ProCompraLocal_Autorizacion.aspx?id1=" + hfNumSolicitudCons.Value + "&Id2=" + session.Id_Emp + "&Id3=" + session.Id_Cd_Ver + "'>");
                //    + "AutorizaComprasLocales.aspx?Solicitud=" + hfNumSolicitudCte.Value + "'>");
                //  ?Id_Folio=" + Id_Env + "&accion=" + 5 + "&PermisoGuardar=" +
                //  permisoGuardar + "&PermisoModificar=" + permisoModificar + "&PermisoEliminar=" + permisoEliminar + "&PermisoImprimir="
                //  + permisoImprimir + "&Id1=" + pe.Env_Unique + "&Id2=" + Sesion.Id_Emp + "&Id3=" + Sesion.Id_Cd_Ver + "&Id4=1" + "'>");
                cuerpo_correo.Append("Solicitud de Compra Local</a>");
                cuerpo_correo.Append("</td></tr></table></div>");
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                sm.EnableSsl = true;
                MailMessage m = new MailMessage();
                string[] eVirtual = configuracion.Mail_EVirtual.Split(',');
                m.From = new MailAddress(configuracion.Mail_Remitente);

                //  m.To.Add(new MailAddress("rafael.garcia@gibraltar.com.mx"));

                string correo = "";
                this.CorreosAutorizadorxMotivo(ref correo);
                if (correo.Length != 0)
                {
                    string[] eVirtual2 = correo.Split(',');
                    reng = 0;

                    foreach (string core in eVirtual2)
                    {
                        if (core.Length > 2)
                        {
                            if (reng == 0)
                            {
                                m.To.Add(new MailAddress("luis.mendez@bsdenterprise.com"));     //core
                                reng = 1;
                            }
                            else
                            {
                                m.CC.Add(new MailAddress(core));
                            }

                        }
                    }
                }
                else
                {
                    m.CC.Add(new MailAddress("luis.mendez@bsdenterprise.com"));
                    //  m.CC.Add(new MailAddress("rafael.mejia@gibraltar.com.mx"));
                }

                //  m.To.Add(new MailAddress(eVirtual[0]));
                //JFCV 13 Abr 2016 Validar si se tiene mas de un correo antes de agregarlo
                //if (eVirtual.Count() > 1)
                //{
                //    if (eVirtual[1] != "")
                //    {
                //        m.CC.Add(new MailAddress(eVirtual[1]));
                //    }
                //}
                //    m.CC.Add(new MailAddress("rafael.mejia@gibraltar.com.mx"));


                m.Subject = "Modificacion a la solicitud de Compra Local " + hfNumSolicitudCons.Value;
                m.IsBodyHtml = true;
                string body = cuerpo_correo.ToString();

                this.RespaldoCorreo(hfNumSolicitudCons.Value, body, correo);

                AlternateView vistaHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                //Esto queda dentro de un try por si llegan a cambiar la imagen el correo como quiera se mande
                try
                {
                    LinkedResource logo = new LinkedResource(MapPath(@"Imagenes/logo.jpg"), MediaTypeNames.Image.Jpeg);
                    logo.ContentId = "companylogo";
                    vistaHtml.LinkedResources.Add(logo);
                }
                catch (Exception)
                {
                }

                m.AlternateViews.Add(vistaHtml);
                try
                {
                    sm.Send(m);
                }
                catch (Exception)
                {
                    Alerta("Error al enviar el correo. Favor de revisar la configuración del sistema");
                    LimpiarCampos4();
                    return;
                }
                Alerta("Solicitud enviada correctamente");
                LimpiarCampos4();
            }
            catch (Exception ex)
            {
                this.ValidaSolicitud(hfNumSolicitudCons.Value);
                throw ex;
            }
        }

        private void DeshabilitaConsulta()
        {
            TextId_PrdCons.Enabled = false;
            txtCodProdCons.Enabled = false;
            chkActivoCons.Enabled = false;
            chkProdNuevoCons.Enabled = false;

            TextId_SpoCons.Enabled = false;
            cmbSisPropCons.Enabled = false;

            TextPrd_DescrpcionCons.Enabled = false;
            txtPresentacionCons.Enabled = false;

            txtTipoProductoCons.Enabled = false;
            cmbTipoProductoCons.Enabled = false;
            txtCategoriaCons.Enabled = false;
            cmbCategoriaCons.Enabled = false;

            txtFamCons.Enabled = false;
            cmbFamCons.Enabled = false;
            txtSubFamCons.Enabled = false;
            cmbSubFamCons.Enabled = false;

            txtProveedorCons.Enabled = false;
            cmbProveedorCons.Enabled = false;
            cmbUentradaCons.Enabled = false;
            cmbUsalidaCons.Enabled = false;
            txtUempaqueCons.Enabled = false;
            txtFactorConversionCons.Enabled = false;

            txtMotivoSolicitaCons.Enabled = false;
            cmbCausaDesabastoCons.Enabled = false;
            rdpVigenciaCons.Enabled = false;
        }

        private void HabilitaConsulta()
        {
            TextId_PrdCons.Enabled = false;
            txtCodProdCons.Enabled = false;
            chkActivoCons.Enabled = false;
            chkProdNuevoCons.Enabled = false;

            TextPrd_DescrpcionCons.Enabled = false;
            txtPresentacionCons.Enabled = true;
            TextId_SpoCons.Enabled = true;
            cmbSisPropCons.Enabled = true;

            txtTipoProductoCons.Enabled = true;
            cmbTipoProductoCons.Enabled = true;
            txtCategoriaCons.Enabled = true;
            cmbCategoriaCons.Enabled = true;

            txtFamCons.Enabled = true;
            cmbFamCons.Enabled = true;
            txtSubFamCons.Enabled = true;
            cmbSubFamCons.Enabled = true;

            txtProveedorCons.Enabled = false;
            cmbProveedorCons.Enabled = false;
            cmbUentradaCons.Enabled = true;
            cmbUsalidaCons.Enabled = true;
            txtUempaqueCons.Enabled = true;
            txtFactorConversionCons.Enabled = true;

            txtMotivoSolicitaCons.Enabled = true;
            cmbCausaDesabastoCons.Enabled = true;
            rdpVigenciaCons.Enabled = true;
        }

        private void NuevaSemilla()
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.AppSettings["strConnection"].ToString();
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "CodigoHomologado_Maximo_Consulta ";
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        TextId_PrdCte.Text = sdr["Producto"].ToString();

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void PorcentajeAAA(int Motivo)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(ConfigurationManager.AppSettings["strConnection"].ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataReader dr = null;

                string[] Parametros = { "@Id_MotivoCL" };
                object[] Valores = { Motivo };
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("PorcentajeAAA_Consulta", ref dr, Parametros, Valores);

                while (dr.Read())
                {
                    this.hdfAAA.Value = dr["AAA"].ToString();

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CorreosAutorizadorxMotivo(ref string Mails)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_ComprasLocales cn_Listadocompralocal = new CN_ComprasLocales();

            int Motivo2 = Convert.ToInt32(this.cmbCategorias.SelectedValue);
            cn_Listadocompralocal.CorreosAutorizadorxMotivo(ref Mails, Motivo2, Sesion.Id_Emp, Sesion.Id_Cd, Sesion.Emp_Cnx);

        }

        private void RespaldoCorreo(string Solicitud, string BodyMail, string Rem)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_ComprasLocales cn_Listadocompralocal = new CN_ComprasLocales();

            cn_Listadocompralocal.RespaldoDeCorreo(Solicitud, BodyMail, Rem, Sesion.Id_Emp, Sesion.Id_Cd, Sesion.Emp_Cnx);
        }

        private void ValidaSolicitud(string Solicitud)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_ComprasLocales cn_Listadocompralocal = new CN_ComprasLocales();

            cn_Listadocompralocal.ValidaSolicitud(Solicitud, Sesion.Id_Cd, Sesion.Emp_Cnx);
        }


        private void CorreosAutorizadorxMotivoxApp(ref string Mails, int Aplikacion)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_ComprasLocales cn_Listadocompralocal = new CN_ComprasLocales();

            int Motivo2 = Convert.ToInt32(this.cmbCategorias.SelectedValue);
            int aplica = Aplikacion;
            cn_Listadocompralocal.CorreosAutorizadorxMotivoxApp(ref Mails, Motivo2, aplica, Sesion.Id_Emp, Sesion.Id_Cd, Sesion.Emp_Cnx);

        }

        #endregion

        #region Eventos

        protected void cmbMotivoCL_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {

                if (cmbMotivoCL.SelectedValue == "3")
                {
                    cmbAplicacionMotCL.Enabled = true;
                }
                else
                {
                    cmbAplicacionMotCL.Enabled = false;
                }


            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void btnNuevoCatCorreo_Click(object sender, ImageClickEventArgs e)
        {
            this.hddEmpresa.Value = "";
            this.cmbCDIMotivoCL.Items.Clear();
            this.cmbMotivoCL.Items.Clear();
            this.txtAutoriza1.Text = "";
            this.cmbAplicacionMotCL.Items.Clear();
            this.hddSecuencia.Value = "0";

            this.ListadoCorreosAutorizador();
        }

        protected void btnAgregaCatCorreo_Click(object sender, ImageClickEventArgs e)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            CN_ComprasLocales cn_Listadocompralocal = new CN_ComprasLocales();
            int CDI = Convert.ToInt32(cmbCDIMotivoCL.SelectedValue);
            string Empresa = Sesion.Id_Emp.ToString();
            int Motivo = Convert.ToInt32(cmbMotivoCL.SelectedValue);
            string correo = txtAutoriza1.Text;
            int aplicacion = Convert.ToInt32(cmbAplicacionMotCL.SelectedValue);
            if (this.hddSecuencia.Value == "")
            {
                this.hddSecuencia.Value = "0";
            }
            int Sec = Convert.ToInt32(hddSecuencia.Value);

            cn_Listadocompralocal.CatCorreoGraba(Empresa, CDI, 100, Motivo, correo, aplicacion, Sec, Sesion.Emp_Cnx);

            this.hddEmpresa.Value = "";
            this.cmbCDIMotivoCL.Items.Clear();
            this.cmbMotivoCL.Items.Clear();
            this.txtAutoriza1.Text = "";
            this.cmbAplicacionMotCL.Items.Clear();
            this.hddSecuencia.Value = "0";

            this.ListadoCorreosAutorizador();
        }

        protected void btnCancelarCatCorreo_Click(object sender, ImageClickEventArgs e)
        {
            this.hddEmpresa.Value = "";
            this.cmbCDIMotivoCL.Items.Clear();
            this.cmbMotivoCL.Items.Clear();
            this.txtAutoriza1.Text = "";
            this.cmbAplicacionMotCL.Items.Clear();
            this.hddSecuencia.Value = "0";

            this.ListadoCorreosAutorizador();
        }

        protected void btnAgregaCMotivo_Click(object sender, ImageClickEventArgs e)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            CN_ComprasLocales cn_Listadocompralocal = new CN_ComprasLocales();
            string descripcion = txtDescMotivoCL.Text;
            string Id = hddIdMotivoCL.Value;
            string AAA = txtAAA.Text;
            bool aplica = chkAplica.Checked;

            cn_Listadocompralocal.CatMotivoGraba(Id, descripcion, AAA, aplica, Sesion.Emp_Cnx);

            this.txtDescMotivoCL.Text = "";
            this.hddIdMotivoCL.Value = "";
            this.txtAAA.Text = "";
            this.chkAplica.Checked = false;
            this.ListadoMotivosCL();
        }

        protected void btnCancelarCMotivo_Click(object sender, ImageClickEventArgs e)
        {
            this.txtDescMotivoCL.Text = "";
            this.hddIdMotivoCL.Value = "";
            this.txtAAA.Text = "";
            this.chkAplica.Checked = false;

            this.ListadoMotivosCL();
        }

        protected void btnAgregaCDes_Click(object sender, ImageClickEventArgs e)
        {
            if (this.txtCatCausaDes.Text != "")
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                CN_ComprasLocales cn_Listadocompralocal = new CN_ComprasLocales();
                string causa = txtCatCausaDes.Text;

                cn_Listadocompralocal.CatDesAgrega(causa, Sesion.Emp_Cnx);

                this.txtCatCausaDes.Text = "";
            }
            ListadoCatAbasto();
        }

        protected void btnCancelarCDes_Click(object sender, ImageClickEventArgs e)
        {
            this.txtCatCausaDes.Text = "";

            ListadoCatAbasto();
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {

                int max = 0;
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Session["IdCategoria" + Session.SessionID] = cmbCategorias.SelectedValue;

                if (cmbCategorias.SelectedValue == "1")
                {
                    this.divActivacion.Visible = true;
                    this.divAbasto.Visible = false;
                    this.divSolicitudCliente.Visible = false;
                    this.divConsultaSolicitud.Visible = false;
                    this.divCatalogoDesabasto.Visible = false;
                    this.divMotivosCompraLocal.Visible = false;
                    this.divConfiguraCorreos.Visible = false;

                    HabilitaCamposComprasLocales(false);

                    this.lblNumSolicitud.Text = "";
                    this.lblNumSolicitudCte.Text = "";
                    this.lblNumSolicitudAbasto.Text = "";

                    this.PorcentajeAAA(Convert.ToInt32(this.cmbCategorias.SelectedIndex));

                    //this.LlenaComboListadoSATDesabasto();
                }


                if (cmbCategorias.SelectedValue == "2")
                {
                    this.divActivacion.Visible = false;
                    this.divAbasto.Visible = true;
                    this.divSolicitudCliente.Visible = false;
                    this.divConsultaSolicitud.Visible = false;
                    this.divCatalogoDesabasto.Visible = false;
                    this.divMotivosCompraLocal.Visible = false;
                    this.divConfiguraCorreos.Visible = false;

                    this.LlenaComboListadoProductosCL();
                    cmbProductosHabiliCompraLocal.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));

                    this.PorcentajeAAA(Convert.ToInt32(this.cmbCategorias.SelectedIndex));

                    //this.LlenaComboListadoSATAbasto();
                }


                if (cmbCategorias.SelectedValue == "3")
                {
                    this.divActivacion.Visible = false;
                    this.divAbasto.Visible = false;
                    this.divSolicitudCliente.Visible = true;
                    this.divConsultaSolicitud.Visible = false;
                    this.divCatalogoDesabasto.Visible = false;
                    this.divMotivosCompraLocal.Visible = false;
                    this.divConfiguraCorreos.Visible = false;

                    this.lblNumSolicitud.Text = "";
                    this.lblNumSolicitudCte.Text = "";
                    this.lblNumSolicitudAbasto.Text = "";

                    this.PorcentajeAAA(Convert.ToInt32(this.cmbCategorias.SelectedIndex));

                    this.LlenaComboListadoSATCte();
                }


                if (cmbCategorias.SelectedValue == "4")
                {
                    this.divActivacion.Visible = false;
                    this.divAbasto.Visible = false;
                    this.divSolicitudCliente.Visible = false;
                    this.divConsultaSolicitud.Visible = true;
                    this.divCatalogoDesabasto.Visible = false;
                    this.divMotivosCompraLocal.Visible = false;
                    this.divConfiguraCorreos.Visible = false;

                    ListadoSolicitudes();

                    this.lblNumSolicitud.Text = "";
                    this.lblNumSolicitudCte.Text = "";
                    this.lblNumSolicitudAbasto.Text = "";
                }

                if (cmbCategorias.SelectedValue == "5")
                {
                    this.divActivacion.Visible = false;
                    this.divAbasto.Visible = false;
                    this.divSolicitudCliente.Visible = false;
                    this.divConsultaSolicitud.Visible = false;
                    this.divCatalogoDesabasto.Visible = true;
                    this.divMotivosCompraLocal.Visible = false;
                    this.divConfiguraCorreos.Visible = false;

                    ListadoCatAbasto();

                    this.lblNumSolicitud.Text = "";
                    this.lblNumSolicitudCte.Text = "";
                    this.lblNumSolicitudAbasto.Text = "";
                }

                if (cmbCategorias.SelectedValue == "6")
                {
                    this.divActivacion.Visible = false;
                    this.divAbasto.Visible = false;
                    this.divSolicitudCliente.Visible = false;
                    this.divConsultaSolicitud.Visible = false;
                    this.divCatalogoDesabasto.Visible = false;
                    this.divMotivosCompraLocal.Visible = true;
                    this.divConfiguraCorreos.Visible = false;

                    ListadoMotivosCL();

                    this.lblNumSolicitud.Text = "";
                    this.lblNumSolicitudCte.Text = "";
                    this.lblNumSolicitudAbasto.Text = "";
                }

                if (cmbCategorias.SelectedValue == "7")
                {
                    this.divActivacion.Visible = false;
                    this.divAbasto.Visible = false;
                    this.divSolicitudCliente.Visible = false;
                    this.divConsultaSolicitud.Visible = false;
                    this.divCatalogoDesabasto.Visible = false;
                    this.divMotivosCompraLocal.Visible = false;
                    this.divConfiguraCorreos.Visible = true;

                    ListadoCorreosAutorizador();

                    this.lblNumSolicitud.Text = "";
                    this.lblNumSolicitudCte.Text = "";
                    this.lblNumSolicitudAbasto.Text = "";
                }

                if (cmbCategorias.SelectedValue == "-1")
                {
                    this.divActivacion.Visible = false;
                    this.divAbasto.Visible = false;
                    this.divSolicitudCliente.Visible = false;
                    this.divConsultaSolicitud.Visible = false;
                    this.divCatalogoDesabasto.Visible = false;
                    this.divMotivosCompraLocal.Visible = false;
                    this.divConfiguraCorreos.Visible = false;

                    this.lblNumSolicitud.Text = "";
                    this.lblNumSolicitudCte.Text = "";
                    this.lblNumSolicitudAbasto.Text = "";
                }

                // Response.Redirect("CatProductos.aspx?id=1", false);

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void chkActivo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                if (!((CheckBox)sender).Checked) // && hiddenId.Value != "")
                {
                    if (!Deshabilitar())
                    {
                        this.DisplayMensajeAlerta("El registro está siendo utilizado por otro componente");
                        ((CheckBox)sender).Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void grdPrecios_PreRender(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                foreach (GridDataItem item in rgPrecios.MasterTableView.Items)
                {
                    if (Convert.ToBoolean(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Prd_Actual"]))
                    {   //si es precio actual, se colorea de azul el Row                    
                        foreach (TableCell cell in item.Cells)
                        {
                            cell.CssClass = "styleCellRowAGridPrecios";
                        }
                        if (item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Pre"].ToString() != "3")
                        {
                            item["EditCommandColumn"].Controls[0].Visible = false;
                        }
                    }
                    else //Se quita la capacidad de edición del precio
                    {
                        item["EditCommandColumn"].Controls[0].Visible = false;
                        if (item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Pre"].ToString() == "3")
                        {
                            item["Prd_Pesos"].Controls[0].Visible = false;
                            item["Prd_Pesos"].BackColor = System.Drawing.Color.Black;
                        }
                    }

                    if (item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Pre"].ToString() == "1")
                    {
                        item["EditCommandColumn"].Controls[0].Visible = false;
                    }

                }

            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(ex.Message);
            }
        }

        protected void grdPrecios_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                GridEditFormItem editedItem = e.Item as GridEditFormItem;

                ProductoPrecios productoPrecio = new ProductoPrecios();
                productoPrecio.Id_Emp = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Id_Emp"]);
                productoPrecio.Id_Cd = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Id_Cd"]);
                productoPrecio.Id_Prd = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Id_Prd"]);
                productoPrecio.Id_Pre = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Id_Pre"]);

                productoPrecio.Prd_Actual = Convert.ToBoolean(((Literal)editedItem["Prd_Actual"].Controls[1]).Text);
                productoPrecio.Prd_FechaInicio = Convert.ToDateTime((editedItem["Prd_FechaInicio"].FindControl("datePickerFechaInicio") as RadDatePicker).SelectedDate);
                productoPrecio.Prd_FechaFin = Convert.ToDateTime((editedItem["Prd_FechaFin"].FindControl("datePickerFechaFin") as RadDatePicker).SelectedDate);
                productoPrecio.Prd_PreDescripcion = (editedItem["Prd_PreDescripcion"].FindControl("txtPrd_PreDescripcion") as RadTextBox).Text.Trim();
                productoPrecio.Pre_Descripcion = (editedItem["Pre_Descripcion"].FindControl("lblTipoPrecioEdit") as Label).Text.Trim();
                productoPrecio.Prd_Pesos = Convert.ToSingle((editedItem["Prd_Pesos"].FindControl("txtPrd_Pesos") as RadNumericTextBox).Text);

                //Checar que es un rango de fechas correcto para SQL Server
                if (Convert.ToDateTime(productoPrecio.Prd_FechaFin).CompareTo(new DateTime(1753, 1, 1)) < 0 || Convert.ToDateTime(productoPrecio.Prd_FechaInicio).CompareTo(new DateTime(1753, 1, 1)) < 0)
                    throw new Exception("rgPrecios_FechasRango_incorrecto");

                List<ProductoPrecios> listaProdPre = new List<ProductoPrecios>();
                for (int i = 0; i < this.listSource.Count; i++)
                    listaProdPre.Add((ProductoPrecios)this.ClonarPrecioProducto(this.listSource[i]));

                for (int i = 0; i < this.listSource.Count; i++)
                {
                    listaProdPre[i].Prd_FechaInicio = null;
                    listaProdPre[i].Prd_FechaFin = null;
                }
                //this.ValidarPeriodosPrecioProducto(ref productoPrecio, listaProdPre);

                /// Guarda el AAA anterior
                /*
                if ((editedItem["Pre_Descripcion"].FindControl("lblTipoPrecioEdit") as Label).Text.Trim() == "COSTO")
                {
                    int posicionAnterior = 0;

                    foreach (ProductoPrecios p in this.listSource)
                    {
                        if (p.Prd_Actual != false && p.Pre_Descripcion != "AAA")
                        {
                            posicionAnterior = posicionAnterior+1;
                        }

                        if (p.Prd_Actual == true && p.Pre_Descripcion == "AAA")
                        {
                            List<ProductoPrecios> listaPPAAA = new List<ProductoPrecios>(this.listSource);
                            //  int posicionFila = rgPrecios.CurrentPageIndex * rgPrecios.PageSize + e.Item.ItemIndex;
                            listaPPAAA[posicionAnterior] = (ProductoPrecios)this.ClonarPrecioProducto(p);
                            listaPPAAA[posicionAnterior].Prd_Actual = false;
                            //  listaPP[posicionFila] = (ProductoPrecios)this.ClonarPrecioProducto(p);
                            this.listSource = listaPPAAA;
                            break;
                        }
                    }
                }
                */

                /// aqui hay que buscar si se modifico AAA 
                double pAAA = 1 + (Convert.ToDouble(this.hdfAAA.Value) / 100);

                List<ProductoPrecios> listaProdPr = new List<ProductoPrecios>(this.listSource);
                if ((editedItem["Pre_Descripcion"].FindControl("lblTipoPrecioEdit") as Label).Text.Trim() == "COSTO")
                {
                    float nAAA = Convert.ToSingle((editedItem["Prd_Pesos"].FindControl("txtPrd_Pesos") as RadNumericTextBox).Text);

                    DateTime FecInicial = Convert.ToDateTime((editedItem["Prd_FechaInicio"].FindControl("datePickerFechaInicio") as RadDatePicker).SelectedDate.ToString());
                    DateTime FecFinal = Convert.ToDateTime((editedItem["Prd_FechaFin"].FindControl("datePickerFechaFin") as RadDatePicker).SelectedDate.ToString());

                    if (Math.Abs((FecFinal.Month - FecInicial.Month) + 12 * (FecFinal.Year - FecInicial.Year)) > 12)
                    {
                        throw new Exception("La diferencia entre las fechas No puede ser mayor a 1 año.");
                        return;
                    }

                    for (int ii = 1; ii < this.listSource.Count; ii++)
                    {
                        if (listaProdPr[ii].Pre_Descripcion == "AAA")
                        {
                            listaProdPr[ii].Prd_Pesos = Convert.ToSingle(nAAA * pAAA);  //1.10
                            listaProdPr[ii].Prd_FechaInicio = FecInicial;
                            listaProdPr[ii].Prd_FechaFin = FecFinal;
                            this.listSource = listaProdPr;
                            //  break;
                        }

                        if (listaProdPr[ii].Pre_Descripcion == "PUBLICO")
                        {
                            //  listaProdPr[ii].Prd_Pesos = listaProdPr[ii].Prd_Pesos;
                            listaProdPr[ii].Prd_FechaInicio = FecInicial;
                            listaProdPr[ii].Prd_FechaFin = FecFinal;
                            this.listSource = listaProdPr;
                            //  break;
                        }
                    }
                }


                //Agregar precio a la lista actual  

                foreach (ProductoPrecios p in this.listSource)
                {
                    if (p.Id_Pre == productoPrecio.Id_Pre && p.Prd_Actual == productoPrecio.Prd_Actual && p.Prd_Actual == true)
                    {
                        List<ProductoPrecios> listaPP = new List<ProductoPrecios>(this.listSource);
                        int posicionFila = rgPrecios.CurrentPageIndex * rgPrecios.PageSize + e.Item.ItemIndex;
                        //listaPP[posicionFila - listaPP.Count / 2] = (ProductoPrecios)this.ClonarPrecioProducto(p);
                        //listaPP[posicionFila - listaPP.Count / 2].Prd_Actual = false;
                        listaPP[posicionFila] = (ProductoPrecios)this.ClonarPrecioProducto(productoPrecio);
                        //if (productoPrecio.Prd_Actual)
                        //    this.ValidarPeriodosPrecioProducto(ref productoPrecio, listSource);
                        this.listSource = listaPP;
                        break;
                    }
                }


                rgPrecios.Rebind();

            }
            catch (Exception ex)
            {  //ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                this.DisplayMensajeAlerta(ex.Message);
            }
        }

        protected void grdPrecios_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    rgPrecios.DataSource = this.listSource;
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(ex.Message);
            }
        }

        protected void grdPrecios_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                ErrorManager();
                if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
                {
                    GridEditFormItem editItem = (GridEditFormItem)e.Item;

                    string datePickerFechaInicio = ((RadDatePicker)editItem.FindControl("datePickerFechaInicio")).ClientID.ToString();
                    string datePickerFechaFin = ((RadDatePicker)editItem.FindControl("datePickerFechaFin")).ClientID.ToString();
                    string txtPrd_Pesos = ((RadNumericTextBox)editItem.FindControl("txtPrd_Pesos")).ClientID.ToString();

                    string jsControles = string.Concat(
                        "datePickerFechaInicioClientId='", datePickerFechaInicio, "';"
                        , "datePickerFechaFinClientId='", datePickerFechaFin, "';"
                        , "txtPrd_PesosClientId='", txtPrd_Pesos, "';"
                        );

                    ImageButton insertbtn = (ImageButton)editItem.FindControl("PerformInsertButton");
                    if (insertbtn != null)
                    {
                        // jsControles = string.Concat(
                        //     jsControles
                        //     , "return ValidaFormGridPrecioProductos(\"insertar\");");

                        insertbtn.Attributes.Add("onclick", jsControles);
                    }

                    ImageButton updatebtn = (ImageButton)editItem.FindControl("UpdateButton");
                    if (updatebtn != null)
                    {
                        //jsControles = string.Concat(
                        //    jsControles
                        //    , "return ValidaFormGridPrecioProductos(\"actualizar\");");

                        updatebtn.Attributes.Add("onclick", jsControles);
                    }
                }
            }
            catch (Exception ex)
            {   //RadGrid1.Controls.Add(new LiteralControl("No se pudo agregar el Usuario. " + ex.Message));
                //ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                this.DisplayMensajeAlerta(ex.Message);
            }
        }

        protected void grdPrecios_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgPrecios.Rebind();
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "radGrid_PageIndexChanged"));
            }
        }

        protected void rgCompraLocal_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    Sesion Sesion = new Sesion();
                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    CompraLocal cl = new CompraLocal();
                    cl.Id_Emp = Sesion.Id_Emp;
                    cl.Id_Cd = Sesion.Id_Cd_Ver;
                    cl.Id_Comp = -1;

                    CN_ComprasLocales cn_Listadocompralocal = new CN_ComprasLocales();
                    List<CompraLocal> v = new List<CompraLocal>();

                    cn_Listadocompralocal.ConsultarSolicitudes(cl, Sesion.Emp_Cnx, ref v);
                    rgCompraLocal.DataSource = v;
                    //  rgCompraLocal.DataSource = this.listSource;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }

        protected void rgCompraLocal_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgCompraLocal.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgDetalleSolicitud_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditSol")
                {
                    Sesion Sesion = new Sesion();
                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                    CN_ComprasLocales cl = new CN_ComprasLocales();
                    // Solicitud
                    int Id_SolDet = 0;
                    int TipoSol = 0;
                    Id_SolDet = Convert.ToInt32(((Label)e.Item.FindControl("lblSolicitud")).Text);
                    TipoSol = Convert.ToInt32(((Label)e.Item.FindControl("lblTipoSolicitud")).Text);

                    this.PorcentajeAAA(TipoSol);

                    titSolicitud.Text = "Solicitud " + Id_SolDet.ToString();
                    hfTipooSolicitudCons.Value = TipoSol.ToString();

                    IniciaConsulta();

                    /// aqui sigue consultar la soliitud en especifito y traer todos los datos del producto
                    int Id_Emp = Sesion.Id_Emp;
                    int Id_Cd = Sesion.Id_Cd_Ver;

                    long ElProducto = 0;

                    //  Consulta la solictud a detalle
                    // saca el id del producto
                    cl.ConsultarSolicitud(Id_SolDet, Sesion.Emp_Cnx, ref ElProducto);
                    LlenarFormularioProductoSoliConsulta(ElProducto, Id_Cd);

                    if (TipoSol == 3)
                    {
                        this.cmbCategorias.SelectedIndex = cmbCategorias.FindItemIndexByValue("3");
                        this.cmbCategorias.Enabled = false;
                        RadTabStripPrincipalCons.Tabs[2].Visible = true;
                        HabilitaConsulta();
                        lblMotivoSolicitud.Visible = true;
                        txtMotivoSolicitaCons.Visible = true;
                        lblDiceVigencia.Visible = true;
                        rdpVigenciaCons.Visible = true;
                        lblCausaDEsabastoCons.Visible = false;
                        cmbCausaDesabastoCons.Visible = false;
                        divPedidosRefCons.Visible = false;
                    }

                    if (TipoSol == 2)
                    {
                        this.cmbCategorias.SelectedIndex = cmbCategorias.FindItemIndexByValue("2");
                        this.cmbCategorias.Enabled = false;
                        RadTabStripPrincipalCons.Tabs[2].Visible = false;
                        // se habilitan los precios, el resto de los datos no se habilita
                        lblMotivoSolicitud.Visible = false;
                        txtMotivoSolicitaCons.Visible = false;
                        lblDiceVigencia.Visible = false;
                        rdpVigenciaCons.Visible = false;
                        lblCausaDEsabastoCons.Visible = false;
                        cmbCausaDesabastoCons.Visible = false;
                        DeshabilitaConsulta();
                        divPedidosRefCons.Visible = false;
                    }


                    if (TipoSol == 1)
                    {

                        this.cmbCategorias.SelectedIndex = cmbCategorias.FindItemIndexByValue("1");
                        this.cmbCategorias.Enabled = false;
                        RadTabStripPrincipalCons.Tabs[2].Visible = false;
                        // se habilitan el combo de motivo desabasto  y los precios, el resto de los datos no se habilita
                        lblMotivoSolicitud.Visible = false;
                        txtMotivoSolicitaCons.Visible = false;
                        lblDiceVigencia.Visible = false;
                        rdpVigenciaCons.Visible = false;
                        lblCausaDEsabastoCons.Visible = true;
                        cmbCausaDesabastoCons.Visible = true;
                        cmbCausaDesabastoCons.Items.Clear();
                        this.LlenarComboCausaDesabasto(ref cmbCausaDesabastoCons);
                        // sacar el producto Original
                        //  txtCodProdCons.Text


                        this.LlenarListaPedidos(Id_SolDet, Convert.ToInt64(txtCodProdCons.Text));    //, ref lstbPedidosCons);
                        if (chklstPedidosCons.Items.Count == 0)
                        {
                            divPedidosRefCons.Visible = false;
                        }
                        else
                        {
                            divPedidosRefCons.Visible = true;
                            this.SeleccionaPedidodeListaPedidos(Convert.ToInt32(Id_SolDet), Convert.ToInt64(txtCodProdCons.Text), ref lstbPedidosCons);
                        }

                        DeshabilitaConsulta();
                        cmbCausaDesabastoCons.Enabled = true;
                    }

                    LlenaListaClientes(Id_SolDet);

                    ComentariosYVigenciaSolicitudConsylta(Id_SolDet, TipoSol);
                    hfNumSolicitudCons.Value = Id_SolDet.ToString();
                    //activo el nuevo set de pestañas (3)
                    divConsulta.Visible = true;

                    rgDetalleSolicitud.DataSource = null;
                    rgDetalleSolicitud.Rebind();
                    rgDetalleSolicitud.Visible = false;

                    rgCompraLocal.DataSource = null;
                    rgCompraLocal.Rebind();
                    rgCompraLocal.Visible = false;
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgDetalleSolicitud_SortCommand(object source, Telerik.Web.UI.GridSortCommandEventArgs e)
        {
            rgDetalleSolicitud.Visible = true;
        }

        protected void rgCompraLocal_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Detail")
                {
                    Sesion Sesion = new Sesion();
                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                    CompraLocal cl = new CompraLocal();

                    int Id_SolDet = 0;
                    Id_SolDet = Convert.ToInt32(((Label)e.Item.FindControl("lblcve")).Text);

                    cl.Id_Emp = Sesion.Id_Emp;
                    cl.Id_Cd = Sesion.Id_Cd_Ver;
                    cl.Id_Comp = Id_SolDet;

                    CN_ProCompraLocal cn_ListadoDetalleCompraLocal = new CN_ProCompraLocal();
                    DataTable DetalleSolicitud = new DataTable();
                    DetalleSolicitud.Columns.Add("Solicitud");
                    DetalleSolicitud.Columns.Add("Num");
                    DetalleSolicitud.Columns.Add("Descripcion");
                    DetalleSolicitud.Columns.Add("Costo");
                    DetalleSolicitud.Columns.Add("Estatus");
                    DetalleSolicitud.Columns.Add("EstatusStr");
                    DetalleSolicitud.Columns.Add("TipoSol");

                    cn_ListadoDetalleCompraLocal.ConsultarSolicitud(cl, ref DetalleSolicitud, Sesion.Emp_Cnx);

                    rgDetalleSolicitud.DataSource = DetalleSolicitud;
                    rgDetalleSolicitud.Rebind();
                    rgDetalleSolicitud.Visible = true;

                }

                if (e.CommandName == "Autorizar")
                {
                    Sesion Sesion = new Sesion();
                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                    int Id_SolDet = 0;
                    Id_SolDet = Convert.ToInt32(((Label)e.Item.FindControl("lblcve")).Text);

                    // redireccionar a la pagina de autorizar esa solicitud
                    string[] url = Request.Url.ToString().Split(new char[] { '/' });

                    string url2 = Request.Url.ToString().Replace(url[url.Length - 1], "") + "ProCompraLocal_Autorizacion.aspx?id1=" + Id_SolDet.ToString() + "&Id2=" + Sesion.Id_Emp + "&Id3=" + Sesion.Id_Cd_Ver;

                    Response.Redirect(url2, false);
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        protected void cmbUnidadMedidaSATDesabasto_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                if (cmbCategorias.SelectedValue == "1" || cmbCategorias.SelectedValue == "2")
                {
                    Alerta("Es importante considerar que se esta realizando el cambio de las Unidades de Medida del SAT al estándar de KEY");
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        protected void cmbProdServicioSATDesabasto_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                if (cmbCategorias.SelectedValue == "1" || cmbCategorias.SelectedValue == "2")
                {
                    Alerta("Es importante considerar que se esta realizando el cambio del Producto/Servicios del SAT al estándar de KEY");
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }



        protected void cmbCategorias_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {

                int max = 0;
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Session["IdCategoria" + Session.SessionID] = cmbCategorias.SelectedValue;

                lblTituloProducto.Text = string.Empty;

                if (cmbCategorias.SelectedValue == "1")
                {
                    this.divActivacion.Visible = true;
                    this.divAbasto.Visible = false;
                    this.divSolicitudCliente.Visible = false;
                    this.divConsultaSolicitud.Visible = false;
                    this.divCatalogoDesabasto.Visible = false;
                    this.divMotivosCompraLocal.Visible = false;
                    this.divConfiguraCorreos.Visible = false;

                    this.lblNumSolicitud.Text = "";
                    this.lblNumSolicitudCte.Text = "";
                    this.lblNumSolicitudAbasto.Text = "";

                    this.PorcentajeAAA(Convert.ToInt32(this.cmbCategorias.SelectedIndex));

                    this.LlenaComboListadoSATDesabasto();
                }

                if (cmbCategorias.SelectedValue == "2")
                {
                    this.divActivacion.Visible = false;
                    this.divAbasto.Visible = true;
                    this.divSolicitudCliente.Visible = false;
                    this.divConsultaSolicitud.Visible = false;
                    this.divCatalogoDesabasto.Visible = false;
                    this.divMotivosCompraLocal.Visible = false;
                    this.divConfiguraCorreos.Visible = false;

                    this.LlenaComboListadoProductosCL();
                    cmbProductosHabiliCompraLocal.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));

                    this.lblNumSolicitud.Text = "";
                    this.lblNumSolicitudCte.Text = "";
                    this.lblNumSolicitudAbasto.Text = "";

                    this.PorcentajeAAA(Convert.ToInt32(this.cmbCategorias.SelectedIndex));

                    this.LlenaComboListadoSATAbasto();
                }

                if (cmbCategorias.SelectedValue == "3")
                {
                    this.divActivacion.Visible = false;
                    this.divAbasto.Visible = false;
                    this.divSolicitudCliente.Visible = true;
                    this.divConsultaSolicitud.Visible = false;
                    this.divCatalogoDesabasto.Visible = false;
                    this.divMotivosCompraLocal.Visible = false;
                    this.divConfiguraCorreos.Visible = false;

                    LlenarComboAplicacion();

                    NoProveedorFijado = false;

                    this.lblNumSolicitud.Text = "";
                    this.lblNumSolicitudCte.Text = "";
                    this.lblNumSolicitudAbasto.Text = "";

                    this.PorcentajeAAA(Convert.ToInt32(this.cmbCategorias.SelectedIndex));

                    this.LlenaComboListadoSATCte();
                    //  se le da vigencia de 1 año a la fecha de la generacion de la solicitud
                    rdpVigencia.DbSelectedDate = DateTime.Now.AddYears(1);
                    rdpVigencia.Enabled = false;
                }

                if (cmbCategorias.SelectedValue == "4")
                {
                    this.divActivacion.Visible = false;
                    this.divAbasto.Visible = false;
                    this.divSolicitudCliente.Visible = false;
                    this.divConsultaSolicitud.Visible = true;
                    this.divCatalogoDesabasto.Visible = false;
                    this.divMotivosCompraLocal.Visible = false;
                    this.divConfiguraCorreos.Visible = false;

                    rgDetalleSolicitud.Visible = false;
                    rgCompraLocal.Visible = true;
                    divConsulta.Visible = false;

                    ListadoSolicitudes();

                    this.TextPrd_DescrpcionCons.Enabled = false;
                    this.txtProveedorCons.Enabled = false;
                    this.cmbProveedorCons.Enabled = false;

                    this.lblNumSolicitud.Text = "";
                    this.lblNumSolicitudCte.Text = "";
                    this.lblNumSolicitudAbasto.Text = "";
                }

                if (cmbCategorias.SelectedValue == "5")
                {
                    this.divActivacion.Visible = false;
                    this.divAbasto.Visible = false;
                    this.divSolicitudCliente.Visible = false;
                    this.divConsultaSolicitud.Visible = false;
                    this.divCatalogoDesabasto.Visible = true;
                    this.divMotivosCompraLocal.Visible = false;
                    this.divConfiguraCorreos.Visible = false;

                    ListadoCatAbasto();

                    this.lblNumSolicitud.Text = "";
                    this.lblNumSolicitudCte.Text = "";
                    this.lblNumSolicitudAbasto.Text = "";
                }

                if (cmbCategorias.SelectedValue == "6")
                {
                    this.divActivacion.Visible = false;
                    this.divAbasto.Visible = false;
                    this.divSolicitudCliente.Visible = false;
                    this.divConsultaSolicitud.Visible = false;
                    this.divCatalogoDesabasto.Visible = false;
                    this.divMotivosCompraLocal.Visible = true;
                    this.divConfiguraCorreos.Visible = false;
                    ListadoMotivosCL();

                    this.lblNumSolicitud.Text = "";
                    this.lblNumSolicitudCte.Text = "";
                    this.lblNumSolicitudAbasto.Text = "";
                }

                if (cmbCategorias.SelectedValue == "7")
                {
                    this.divActivacion.Visible = false;
                    this.divAbasto.Visible = false;
                    this.divSolicitudCliente.Visible = false;
                    this.divConsultaSolicitud.Visible = false;
                    this.divCatalogoDesabasto.Visible = false;
                    this.divMotivosCompraLocal.Visible = false;
                    this.divConfiguraCorreos.Visible = true;

                    ListadoCorreosAutorizador();

                    this.lblNumSolicitud.Text = "";
                    this.lblNumSolicitudCte.Text = "";
                    this.lblNumSolicitudAbasto.Text = "";
                }

                if (cmbCategorias.SelectedValue == "-1")
                {
                    this.divActivacion.Visible = false;
                    this.divAbasto.Visible = false;
                    this.divSolicitudCliente.Visible = false;
                    this.divConsultaSolicitud.Visible = false;
                    this.divCatalogoDesabasto.Visible = false;
                    this.divMotivosCompraLocal.Visible = false;
                    this.divConfiguraCorreos.Visible = false;

                    this.lblNumSolicitud.Text = "";
                    this.lblNumSolicitudCte.Text = "";
                    this.lblNumSolicitudAbasto.Text = "";
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void cmbCausaDesabasto_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                this.divPedidosRefAbasto.Visible = false;
                if (cmbCausaDesabasto.SelectedValue == "1")
                {
                    if (this.chklstPedidos.Items.Count == 0)
                    {
                        cmbCausaDesabasto.SelectedValue = "-1";
                        Alerta("No Se Encontraron Ordenes De Compra De Los Ultimos 3 Meses Con El Producto.   Favor de Seleccionar Otra Causa del Desabasto.");
                    }
                    else
                    {
                        this.divPedidosRefAbasto.Visible = true;
                    }
                }
                if (cmbCausaDesabasto.SelectedValue == "2")
                {
                    if (this.chklstPedidos.Items.Count == 0)
                    {
                        cmbCausaDesabasto.SelectedValue = "-1";
                        Alerta("No Se Encontraron Ordenes De Compra De Los Ultimos 3 Meses Con El Producto.   Favor de Seleccionar Otra Causa del Desabasto.");
                    }
                    else
                    {
                        this.divPedidosRefAbasto.Visible = true;
                    }
                }
                /*
                if (cmbCausaDesabasto.SelectedValue == "3")
                {
                    this.divPedidosRefAbasto.Visible = false;
                }
                if (cmbCausaDesabasto.SelectedValue == "4")
                {
                    this.divPedidosRefAbasto.Visible = false;
                }
                */
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void cmbCausaDesabastoCons_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {

                this.divPedidosRefCons.Visible = false;
                if (cmbCausaDesabastoCons.SelectedValue == "1")
                {
                    if (this.chklstPedidosCons.Items.Count == 0)
                    {
                        cmbCausaDesabastoCons.SelectedValue = "-1";
                        Alerta("No Se Encontraron Ordenes De Compra De Los Ultimos 3 Meses Con El Producto.   Favor de Seleccionar Otra Causa del Desabasto.");
                    }
                    else
                    {
                        this.divPedidosRefCons.Visible = true;
                    }
                }
                if (cmbCausaDesabastoCons.SelectedValue == "2")
                {
                    if (this.chklstPedidosCons.Items.Count == 0)
                    {
                        cmbCausaDesabastoCons.SelectedValue = "-1";
                        Alerta("No Se Encontraron Ordenes De Compra De Los Ultimos 3 Meses Con El Producto.   Favor de Seleccionar Otra Causa del Desabasto.");
                    }
                    else
                    {
                        this.divPedidosRefCons.Visible = true;
                    }
                }
                /*
                if (cmbCausaDesabasto.SelectedValue == "3")
                {
                    this.divPedidosRefAbasto.Visible = false;
                }
                if (cmbCausaDesabasto.SelectedValue == "4")
                {
                    this.divPedidosRefAbasto.Visible = false;
                }
                */
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void cmbCentrosDist_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null) { string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries); Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false); }
                CN__Comun comun = new CN__Comun(); comun.CambiarCdVer(cmbCentrosDist.SelectedItem, ref sesion);

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgPreciosCte_PreRender(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                foreach (GridDataItem item in rgPreciosCte.MasterTableView.Items)
                {
                    if (Convert.ToBoolean(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Prd_Actual"]))
                    {   //si es precio actual, se colorea de azul el Row                    
                        foreach (TableCell cell in item.Cells)
                        {
                            cell.CssClass = "styleCellRowAGridPrecios";
                        }
                        if (item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Pre"].ToString() == "1")
                        {
                            item["EditCommandColumn"].Controls[0].Visible = false;
                        }
                    }
                    else //Se quita la capacidad de edición del precio
                    {
                        item["EditCommandColumn"].Controls[0].Visible = false;
                    }

                    if (item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Pre"].ToString() == "1")
                    {
                        item["EditCommandColumn"].Controls[0].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(ex.Message);
            }
        }

        protected void rgPreciosCte_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                GridEditFormItem editedItem = e.Item as GridEditFormItem;

                ProductoPrecios productoPrecio = new ProductoPrecios();
                productoPrecio.Id_Emp = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Id_Emp"]);
                productoPrecio.Id_Cd = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Id_Cd"]);
                productoPrecio.Id_Prd = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Id_Prd"]);
                productoPrecio.Id_Pre = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Id_Pre"]);

                productoPrecio.Prd_Actual = Convert.ToBoolean(((Literal)editedItem["Prd_Actual"].Controls[1]).Text);
                productoPrecio.Prd_FechaInicio = Convert.ToDateTime((editedItem["Prd_FechaInicio"].FindControl("datePickerFechaInicioCte") as RadDatePicker).SelectedDate);
                productoPrecio.Prd_FechaFin = Convert.ToDateTime((editedItem["Prd_FechaFin"].FindControl("datePickerFechaFinCte") as RadDatePicker).SelectedDate);
                productoPrecio.Prd_PreDescripcion = (editedItem["Prd_PreDescripcion"].FindControl("txtPrd_PreDescripcionCte") as RadTextBox).Text.Trim();
                productoPrecio.Pre_Descripcion = (editedItem["Pre_Descripcion"].FindControl("lblTipoPrecioEditCte") as Label).Text.Trim();
                productoPrecio.Prd_Pesos = Convert.ToSingle((editedItem["Prd_Pesos"].FindControl("txtPrd_PesosCte") as RadNumericTextBox).Text);

                //Checar que es un rango de fechas correcto para SQL Server
                if (Convert.ToDateTime(productoPrecio.Prd_FechaFin).CompareTo(new DateTime(1753, 1, 1)) < 0 || Convert.ToDateTime(productoPrecio.Prd_FechaInicio).CompareTo(new DateTime(1753, 1, 1)) < 0)
                    throw new Exception("rgPrecios_FechasRango_incorrecto");

                List<ProductoPrecios> listaProdPre = new List<ProductoPrecios>();
                for (int i = 0; i < this.listSource.Count; i++)
                    listaProdPre.Add((ProductoPrecios)this.ClonarPrecioProducto(this.listSource[i]));

                for (int i = 0; i < this.listSource.Count; i++)
                {
                    listaProdPre[i].Prd_FechaInicio = null;
                    listaProdPre[i].Prd_FechaFin = null;
                }
                //this.ValidarPeriodosPrecioProducto(ref productoPrecio, listaProdPre);

                /// Guarda el AAA anterior
                /*
                if ((editedItem["Pre_Descripcion"].FindControl("lblTipoPrecioEditCte") as Label).Text.Trim() == "COSTO")
                {
                    int posicionAnterior = 0;

                    foreach (ProductoPrecios p in this.listSource)
                    {
                        if (p.Prd_Actual != false && p.Pre_Descripcion != "AAA")
                        {
                            posicionAnterior = posicionAnterior + 1;
                        }

                        if (p.Prd_Actual == true && p.Pre_Descripcion == "AAA")
                        {
                            List<ProductoPrecios> listaPPAAA = new List<ProductoPrecios>(this.listSource);
                            //  int posicionFila = rgPrecios.CurrentPageIndex * rgPrecios.PageSize + e.Item.ItemIndex;
                            listaPPAAA[posicionAnterior] = (ProductoPrecios)this.ClonarPrecioProducto(p);
                            listaPPAAA[posicionAnterior].Prd_Actual = false;
                            //  listaPP[posicionFila] = (ProductoPrecios)this.ClonarPrecioProducto(p);
                            this.listSource = listaPPAAA;
                            break;
                        }
                    }
                }
                */

                /// aqui hay que buscar si se modifico AAA 
                double pAAA = 1 + (Convert.ToDouble(this.hdfAAA.Value) / 100);

                List<ProductoPrecios> listaProdPr = new List<ProductoPrecios>(this.listSource);
                if ((editedItem["Pre_Descripcion"].FindControl("lblTipoPrecioEditCte") as Label).Text.Trim() == "COSTO")
                {
                    float nAAA = Convert.ToSingle((editedItem["Prd_Pesos"].FindControl("txtPrd_PesosCte") as RadNumericTextBox).Text);

                    DateTime FecInicial = Convert.ToDateTime((editedItem["Prd_FechaInicio"].FindControl("datePickerFechaInicioCte") as RadDatePicker).SelectedDate.ToString());
                    DateTime FecFinal = Convert.ToDateTime((editedItem["Prd_FechaFin"].FindControl("datePickerFechaFinCte") as RadDatePicker).SelectedDate.ToString());

                    for (int ii = 1; ii < this.listSource.Count; ii++)
                    {
                        if (listaProdPr[ii].Pre_Descripcion == "AAA")
                        {
                            listaProdPr[ii].Prd_Pesos = Convert.ToSingle(nAAA * pAAA);
                            listaProdPr[ii].Prd_FechaInicio = FecInicial;
                            listaProdPr[ii].Prd_FechaFin = FecFinal;
                            this.listSource = listaProdPr;
                            //  break;
                        }
                        if (listaProdPr[ii].Pre_Descripcion == "PUBLICO")
                        {
                            //  listaProdPr[ii].Prd_Pesos = Convert.ToSingle(nAAA * pAAA);
                            listaProdPr[ii].Prd_FechaInicio = FecInicial;
                            listaProdPr[ii].Prd_FechaFin = FecFinal;
                            this.listSource = listaProdPr;
                            //  break;
                        }
                    }
                }


                //Agregar precio a la lista actual  
                foreach (ProductoPrecios p in this.listSource)
                {
                    if (p.Id_Pre == productoPrecio.Id_Pre && p.Prd_Actual == productoPrecio.Prd_Actual && p.Prd_Actual == true)
                    {
                        List<ProductoPrecios> listaPP = new List<ProductoPrecios>(this.listSource);
                        int posicionFila = rgPreciosCte.CurrentPageIndex * rgPreciosCte.PageSize + e.Item.ItemIndex;
                        //  listaPP[posicionFila - listaPP.Count / 2] = (ProductoPrecios)this.ClonarPrecioProducto(p);
                        //  listaPP[posicionFila - listaPP.Count / 2].Prd_Actual = false;
                        listaPP[posicionFila] = (ProductoPrecios)this.ClonarPrecioProducto(productoPrecio);
                        //if (productoPrecio.Prd_Actual)
                        //    this.ValidarPeriodosPrecioProducto(ref productoPrecio, listSource);
                        this.listSource = listaPP;
                        break;
                    }
                }

                rgPreciosCte.Rebind();

            }
            catch (Exception ex)
            {  //ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                this.DisplayMensajeAlerta(ex.Message);
            }
        }

        protected void rgPreciosCte_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    rgPreciosCte.DataSource = this.listSource;
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(ex.Message);
            }
        }

        protected void rgPreciosCte_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                ErrorManager();
                if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
                {
                    GridEditFormItem editItem = (GridEditFormItem)e.Item;

                    string datePickerFechaInicio = ((RadDatePicker)editItem.FindControl("datePickerFechaInicioCte")).ClientID.ToString();
                    string datePickerFechaFin = ((RadDatePicker)editItem.FindControl("datePickerFechaFinCte")).ClientID.ToString();
                    string txtPrd_Pesos = ((RadNumericTextBox)editItem.FindControl("txtPrd_PesosCte")).ClientID.ToString();

                    string jsControles = string.Concat(
                        "datePickerFechaInicioClientIdCte='", datePickerFechaInicio, "';"
                        , "datePickerFechaFinClientIdCte='", datePickerFechaFin, "';"
                        , "txtPrd_PesosClientIdCte='", txtPrd_Pesos, "';"
                        );

                    ImageButton insertbtn = (ImageButton)editItem.FindControl("PerformInsertButton");
                    if (insertbtn != null)
                    {
                        //jsControles = string.Concat(
                        //    jsControles
                        //    , "return ValidaFormGridPrecioProductos(\"insertar\");");

                        insertbtn.Attributes.Add("onclick", jsControles);
                    }

                    ImageButton updatebtn = (ImageButton)editItem.FindControl("UpdateButton");
                    if (updatebtn != null)
                    {
                        //jsControles = string.Concat(
                        //    jsControles
                        //    , "return ValidaFormGridPrecioProductos(\"actualizar\");");

                        updatebtn.Attributes.Add("onclick", jsControles);
                    }
                }
            }
            catch (Exception ex)
            {   //RadGrid1.Controls.Add(new LiteralControl("No se pudo agregar el Usuario. " + ex.Message));
                //ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                this.DisplayMensajeAlerta(ex.Message);
            }
        }

        protected void rgPreciosCte_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgPreciosCte.Rebind();
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "radGrid_PageIndexChanged"));
            }
        }

        protected void cmdSubFamiliaSoli_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {

                if (cmdSubFamiliaSoli.SelectedValue != "-1")
                {
                    if (cmdSubFamiliaSoli.SelectedValue == "47")    //  nueva categoria de OTROS
                    {
                        //  this.txtIdProductoSol.Enabled = true;
                        this.hdSubFamiliaSoli.Value = cmdSubFamiliaSoli.SelectedValue;
                        //  LlenaComboProducto();
                        InicializaSolicitud();
                        RadComboBoxProduct.Enabled = true;

                        Sesion Sesion = new Sesion();
                        Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                        HabilitaCamposSolicitud(true);
                        this.LlenarFormularioProductoSoliCliente(0, Sesion.Id_Cd_Ver);
                        this.NuevaSemilla();
                    }
                    else
                    {
                        LlenarComboSubFamilias(cmbAplicacionSoli.SelectedValue, ref cmbSubFamCte);
                        txtSubFamCte.Text = cmdSubFamiliaSoli.SelectedValue.ToString();
                        //  cmbSubFamCte.Items.Insert(cmbSubFamCte.Items.Count(), new RadComboBoxItem("OTROS", "0"));
                        cmbSubFamCte.SelectedValue = cmdSubFamiliaSoli.SelectedValue;
                        cmbSubFamCte.Enabled = false;
                        txtFamCte.Enabled = false;
                        txtSubFamCte.Enabled = false;
                    }
                    PestanasCte.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void cmbAplicacionSoli_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {

                int max = 0;
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (cmbAplicacionSoli.SelectedValue != "-1")
                {
                    if (cmbAplicacionSoli.SelectedValue == "1047")
                    {
                        //  lblCodigoProductoCteF4F.Visible = true;
                        //  txtIdProductoCteF4F.Visible = true;

                        HabilitaCamposSolicitud(true);
                        InicializaSolicitud();

                        this.LlenarFormularioProductoSoliCliente(0, Sesion.Id_Cd_Ver);
                        this.NuevaSemilla();

                        //  TextId_PrdCte.Text = this.MaximoId(0, this.txtProveedorCte.Text);   //  this.Valor;
                        //  txtCodProdCte.Text = TextId_PrdCte.Text;
                        PestanasCte.Visible = true;
                        cmdSubFamiliaSoli.Items.Insert(cmdSubFamiliaSoli.Items.Count(), new RadComboBoxItem("OTROS", "0"));
                        cmdSubFamiliaSoli.SelectedValue = "0";  //"-|- Otros -|-";
                        cmdSubFamiliaSoli.Enabled = false;

                        txtSubFamCte.Text = "0";
                        cmbSubFamCte.Items.Insert(cmbSubFamCte.Items.Count(), new RadComboBoxItem("OTROS", "0"));
                        cmbSubFamCte.SelectedValue = "0";
                        cmbSubFamCte.Enabled = false;
                        txtSubFamCte.Enabled = false;
                        cmbFamCte.Items.Insert(cmbFamCte.Items.Count(), new RadComboBoxItem("OTROS", "0"));
                        cmbFamCte.SelectedValue = "0";
                        txtFamCte.Text = "0";
                        cmbFamCte.Enabled = false;
                        txtFamCte.Enabled = false;

                        this.cmbCategoriaCte.SelectedValue = this.cmbCategoriaCte.FindItemByText("OTROS", true).Value;
                        this.cmbCategoriaCte.Enabled = false;
                        this.txtCategoriaCte.Text = "0";
                    }
                    else
                    {
                        HabilitaCamposSolicitud(true);
                        InicializaSolicitud();
                        this.LlenarFormularioProductoSoliCliente(0, Sesion.Id_Cd_Ver);
                        this.NuevaSemilla();

                        this.cmdSubFamiliaSoli.Items.Clear();
                        this.LlenarComboSubFamilias(cmbAplicacionSoli.SelectedValue, ref cmdSubFamiliaSoli);
                        this.cmdSubFamiliaSoli.Enabled = true;

                        this.cmbSubFamCte.Items.Clear();
                        this.LlenarComboSubFamilias(cmbAplicacionSoli.SelectedValue, ref cmbSubFamCte);
                        this.cmbSubFamCte.Enabled = true;

                        cmbFamCte.SelectedValue = cmbAplicacionSoli.SelectedValue;
                        txtFamCte.Text = cmbAplicacionSoli.SelectedValue.ToString();
                        cmbFamCte.Enabled = false;
                        txtFamCte.Enabled = false;

                        if (cmbAplicacionSoli.SelectedValue == "47")
                        {
                            txtSubFamCte.Text = "0";
                            txtFamCte.Text = "0";
                            this.cmbCategoriaCte.SelectedValue = this.cmbCategoriaCte.FindItemByText("OTROS", true).Value;
                            this.cmbCategoriaCte.Enabled = false;
                            this.txtCategoriaCte.Text = "0";
                        }

                    }
                }


            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void grdPreciosAbasto_PreRender(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                foreach (GridDataItem item in rgPreciosAbasto.MasterTableView.Items)
                {
                    if (Convert.ToBoolean(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Prd_Actual"]))
                    {   //si es precio actual, se colorea de azul el Row                    
                        foreach (TableCell cell in item.Cells)
                        {
                            cell.CssClass = "styleCellRowAGridPrecios";
                        }
                        if (item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Pre"].ToString() != "3")
                        {
                            item["EditCommandColumn"].Controls[0].Visible = false;
                        }
                    }
                    else //Se quita la capacidad de edición del precio
                    {
                        item["EditCommandColumn"].Controls[0].Visible = false;
                    }

                    if (item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Pre"].ToString() == "1")
                    {
                        item["EditCommandColumn"].Controls[0].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(ex.Message);
            }
        }

        protected void grdPreciosAbasto_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                GridEditFormItem editedItem = e.Item as GridEditFormItem;

                ProductoPrecios productoPrecio = new ProductoPrecios();
                productoPrecio.Id_Emp = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Id_Emp"]);
                productoPrecio.Id_Cd = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Id_Cd"]);
                productoPrecio.Id_Prd = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Id_Prd"]);
                productoPrecio.Id_Pre = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Id_Pre"]);

                productoPrecio.Prd_Actual = Convert.ToBoolean(((Literal)editedItem["Prd_Actual"].Controls[1]).Text);
                productoPrecio.Prd_FechaInicio = Convert.ToDateTime((editedItem["Prd_FechaInicio"].FindControl("datePickerFechaInicioAba") as RadDatePicker).SelectedDate);
                productoPrecio.Prd_FechaFin = Convert.ToDateTime((editedItem["Prd_FechaFin"].FindControl("datePickerFechaFinAba") as RadDatePicker).SelectedDate);
                productoPrecio.Prd_PreDescripcion = (editedItem["Prd_PreDescripcion"].FindControl("txtPrd_PreDescripcionAba") as RadTextBox).Text.Trim();
                productoPrecio.Pre_Descripcion = (editedItem["Pre_Descripcion"].FindControl("lblTipoPrecioEditAba") as Label).Text.Trim();
                productoPrecio.Prd_Pesos = Convert.ToSingle((editedItem["Prd_Pesos"].FindControl("txtPrd_PesosAba") as RadNumericTextBox).Text);

                //Checar que es un rango de fechas correcto para SQL Server
                if (Convert.ToDateTime(productoPrecio.Prd_FechaFin).CompareTo(new DateTime(1753, 1, 1)) < 0 || Convert.ToDateTime(productoPrecio.Prd_FechaInicio).CompareTo(new DateTime(1753, 1, 1)) < 0)
                    throw new Exception("rgPrecios_FechasRango_incorrecto");

                List<ProductoPrecios> listaProdPre = new List<ProductoPrecios>();
                for (int i = 0; i < this.listSource.Count; i++)
                    listaProdPre.Add((ProductoPrecios)this.ClonarPrecioProducto(this.listSource[i]));

                for (int i = 0; i < this.listSource.Count; i++)
                {
                    listaProdPre[i].Prd_FechaInicio = null;
                    listaProdPre[i].Prd_FechaFin = null;
                }

                /// Guarda el AAA anterior
                /*
                if ((editedItem["Pre_Descripcion"].FindControl("lblTipoPrecioEditAba") as Label).Text.Trim() == "COSTO")
                {
                    int posicionAnterior = 0;

                    foreach (ProductoPrecios p in this.listSource)
                    {
                        if (p.Prd_Actual != false && p.Pre_Descripcion != "AAA")
                        {
                            posicionAnterior = posicionAnterior + 1;
                        }

                        if (p.Prd_Actual == true && p.Pre_Descripcion == "AAA")
                        {
                            List<ProductoPrecios> listaPPAAA = new List<ProductoPrecios>(this.listSource);
                            listaPPAAA[posicionAnterior] = (ProductoPrecios)this.ClonarPrecioProducto(p);
                            listaPPAAA[posicionAnterior].Prd_Actual = false;
                            this.listSource = listaPPAAA;
                            break;
                        }
                    }
                }
                */
                /// aqui hay que buscar si se modifico AAA 
                double pAAA = 1 + (Convert.ToDouble(this.hdfAAA.Value) / 100);

                //  validacion de los precios
                float precioAAA = 0;

                List<ProductoPrecios> listaProdPr = new List<ProductoPrecios>(this.listSource);
                if ((editedItem["Pre_Descripcion"].FindControl("lblTipoPrecioEditAba") as Label).Text.Trim() == "COSTO")
                {
                    float nAAA = Convert.ToSingle((editedItem["Prd_Pesos"].FindControl("txtPrd_PesosAba") as RadNumericTextBox).Text);
                    precioAAA = Convert.ToSingle(nAAA * pAAA);

                    if (precioAAA > Convert.ToSingle(hddPrecioAAAOriginal.Value))     //  hddPrecioCostoOriginal
                    {
                        throw new Exception("El Nuevo AAA No puede Estar por encima del AAA del Codigo Padre.");
                        //  "El costo AAA del producto en CEDIS es menor.""El costo AAA del producto en CEDIS es menor."
                        return;
                    }

                    DateTime FecInicial = Convert.ToDateTime((editedItem["Prd_FechaInicio"].FindControl("datePickerFechaInicioAba") as RadDatePicker).SelectedDate.ToString());
                    DateTime FecFinal = Convert.ToDateTime((editedItem["Prd_FechaFin"].FindControl("datePickerFechaFinAba") as RadDatePicker).SelectedDate.ToString());

                    //if (Math.Abs((FecFinal.Month - FecInicial.Month) + 12 * (FecFinal.Year - FecInicial.Year)) > 12)
                    //{
                    //    throw new Exception("La diferencia entre las fechas No puede ser mayor a 1 año.");
                    //    return;
                    //}

                    for (int ii = 1; ii < this.listSource.Count; ii++)
                    {
                        if (listaProdPr[ii].Pre_Descripcion == "AAA")
                        {
                            listaProdPr[ii].Prd_Pesos = Convert.ToSingle(nAAA * pAAA);  //  1.10
                            listaProdPr[ii].Prd_FechaInicio = FecInicial;
                            listaProdPr[ii].Prd_FechaFin = FecFinal;
                            this.listSource = listaProdPr;
                            //  break;
                        }

                        if (listaProdPr[ii].Pre_Descripcion == "PUBLICO")
                        {
                            //  listaProdPr[ii].Prd_Pesos = Convert.ToSingle(nAAA * pAAA);  //  1.10
                            listaProdPr[ii].Prd_FechaInicio = FecInicial;
                            listaProdPr[ii].Prd_FechaFin = FecFinal;
                            this.listSource = listaProdPr;
                            //  break;
                        }
                    }

                }

                //Agregar precio a la lista actual  
                foreach (ProductoPrecios p in this.listSource)
                {
                    if (p.Id_Pre == productoPrecio.Id_Pre && p.Prd_Actual == productoPrecio.Prd_Actual && p.Prd_Actual == true)
                    {
                        List<ProductoPrecios> listaPP = new List<ProductoPrecios>(this.listSource);
                        int posicionFila = rgPrecios.CurrentPageIndex * rgPrecios.PageSize + e.Item.ItemIndex;
                        //  listaPP[posicionFila - listaPP.Count / 2] = (ProductoPrecios)this.ClonarPrecioProducto(p);
                        //  listaPP[posicionFila - listaPP.Count / 2].Prd_Actual = false;
                        listaPP[posicionFila] = (ProductoPrecios)this.ClonarPrecioProducto(productoPrecio);
                        this.listSource = listaPP;
                        break;
                    }
                }

                rgPrecios.Rebind();
            }
            catch (Exception ex)
            {  //ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                this.DisplayMensajeAlerta(ex.Message);
            }
        }

        protected void grdPreciosAbasto_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    rgPreciosAbasto.DataSource = this.listSource;
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(ex.Message);
            }
        }

        protected void grdPreciosAbasto_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                ErrorManager();
                if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
                {
                    GridEditFormItem editItem = (GridEditFormItem)e.Item;

                    string datePickerFechaInicio = ((RadDatePicker)editItem.FindControl("datePickerFechaInicioAba")).ClientID.ToString();
                    string datePickerFechaFin = ((RadDatePicker)editItem.FindControl("datePickerFechaFinAba")).ClientID.ToString();
                    string txtPrd_Pesos = ((RadNumericTextBox)editItem.FindControl("txtPrd_PesosAba")).ClientID.ToString();

                    string jsControles = string.Concat(
                        "datePickerFechaInicioClientIdAba='", datePickerFechaInicio, "';"
                        , "datePickerFechaFinClientIdAba='", datePickerFechaFin, "';"
                        , "txtPrd_PesosClientIdAba='", txtPrd_Pesos, "';"
                        );

                    ImageButton insertbtn = (ImageButton)editItem.FindControl("PerformInsertButton");
                    if (insertbtn != null)
                    {
                        //jsControles = string.Concat(
                        //    jsControles
                        //    , "return ValidaFormGridPrecioProductos(\"insertar\");");

                        insertbtn.Attributes.Add("onclick", jsControles);
                    }

                    ImageButton updatebtn = (ImageButton)editItem.FindControl("UpdateButton");
                    if (updatebtn != null)
                    {
                        //jsControles = string.Concat(
                        //    jsControles
                        //    , "return ValidaFormGridPrecioProductos(\"actualizar\");");

                        updatebtn.Attributes.Add("onclick", jsControles);
                    }
                }
            }
            catch (Exception ex)
            {   //RadGrid1.Controls.Add(new LiteralControl("No se pudo agregar el Usuario. " + ex.Message));
                //ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                this.DisplayMensajeAlerta(ex.Message);
            }
        }

        protected void grdPreciosAbasto_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgPreciosAbasto.Rebind();
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "radGrid_PageIndexChanged"));
            }
        }

        protected void rgCausasDesabasto_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    Sesion Sesion = new Sesion();
                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                    CN_ComprasLocales cn_Listadocompralocal = new CN_ComprasLocales();

                    DataTable CatDesabasto = new DataTable();
                    CatDesabasto.Columns.Add("Id_Causa");
                    CatDesabasto.Columns.Add("Desc_CausaDesAbasto");
                    CatDesabasto.Columns.Add("Activo");

                    cn_Listadocompralocal.CatDesConsulta(ref CatDesabasto, Sesion.Emp_Cnx);
                    rgCausasDesabasto.DataSource = CatDesabasto;
                    rgCausasDesabasto.Rebind();

                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }

        protected void rgCausasDesabasto_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgCausasDesabasto.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgCausasDesabasto_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {

                int Id_CausaD = 0;
                Id_CausaD = Convert.ToInt32(((Label)e.Item.FindControl("lblCausaDes")).Text);

                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                CN_ComprasLocales cn_Listadocompralocal = new CN_ComprasLocales();
                if (e.CommandName == "Desactivar")
                {
                    cn_Listadocompralocal.CatDesDesactiva(Id_CausaD, Sesion.Emp_Cnx);
                }
                if (e.CommandName == "Eliminar")
                {
                    cn_Listadocompralocal.CatDesElimina(Id_CausaD, Sesion.Emp_Cnx);
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgMotivosCL_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    Sesion Sesion = new Sesion();
                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                    CN_ComprasLocales cn_Listadocompralocal = new CN_ComprasLocales();

                    DataTable CatMotivo = new DataTable();
                    CatMotivo.Columns.Add("Id_MotivoCL");
                    CatMotivo.Columns.Add("Desc_MotivoCL");
                    CatMotivo.Columns.Add("PorcentajeAAA");
                    CatMotivo.Columns.Add("Aplica");

                    cn_Listadocompralocal.CatMotivoCL(ref CatMotivo, Sesion.Emp_Cnx);
                    rgMotivosCL.DataSource = CatMotivo;
                    rgMotivosCL.Rebind();

                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }

        protected void rgMotivosCL_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgMotivosCL.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgMotivosCL_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {

                int Id_MotivoCL = 0;
                Id_MotivoCL = Convert.ToInt32(((Label)e.Item.FindControl("lblMotivoCL")).Text);

                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                CN_ComprasLocales cn_Listadocompralocal = new CN_ComprasLocales();

                if (e.CommandName == "Editar")
                {
                    //  cn_Listadocompralocal.CatDesElimina(Id_MotivoCL, Sesion.Emp_Cnx);

                    GridEditFormItem item = e.Item as GridEditFormItem;
                    this.hddIdMotivoCL.Value = Convert.ToString(Id_MotivoCL);

                    this.txtDescMotivoCL.Text = Convert.ToString(((Label)e.Item.FindControl("lblDescMotivoCL")).Text);
                    this.chkAplica.Checked = false;
                    if (0 != Convert.ToDouble(((Label)e.Item.FindControl("lblPorcentajeAAA")).Text))
                    {
                        this.chkAplica.Checked = true;
                    }
                    this.txtAAA.Text = Convert.ToString(((Label)e.Item.FindControl("lblPorcentajeAAA")).Text);
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgPreciosCons_PreRender(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                foreach (GridDataItem item in rgPreciosCons.MasterTableView.Items)
                {
                    if (Convert.ToBoolean(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Prd_Actual"]))
                    {   //si es precio actual, se colorea de azul el Row                    
                        foreach (TableCell cell in item.Cells)
                        {
                            cell.CssClass = "styleCellRowAGridPrecios";
                        }
                        if (item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Pre"].ToString() != "3")
                        {
                            item["EditCommandColumn"].Controls[0].Visible = false;
                        }
                    }
                    else //Se quita la capacidad de edición del precio
                    {
                        item["EditCommandColumn"].Controls[0].Visible = false;
                    }

                    if (item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Pre"].ToString() == "1")
                    {
                        item["EditCommandColumn"].Controls[0].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(ex.Message);
            }
        }

        protected void rgPreciosCons_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                GridEditFormItem editedItem = e.Item as GridEditFormItem;

                ProductoPrecios productoPrecio = new ProductoPrecios();
                productoPrecio.Id_Emp = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Id_Emp"]);
                productoPrecio.Id_Cd = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Id_Cd"]);
                productoPrecio.Id_Prd = Convert.ToInt64(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Id_Prd"]);
                productoPrecio.Id_Pre = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Id_Pre"]);

                productoPrecio.Prd_Actual = Convert.ToBoolean(((Literal)editedItem["Prd_Actual"].Controls[1]).Text);
                productoPrecio.Prd_FechaInicio = Convert.ToDateTime((editedItem["Prd_FechaInicio"].FindControl("datePickerFechaInicioCons") as RadDatePicker).SelectedDate);
                productoPrecio.Prd_FechaFin = Convert.ToDateTime((editedItem["Prd_FechaFin"].FindControl("datePickerFechaFinCons") as RadDatePicker).SelectedDate);
                productoPrecio.Prd_PreDescripcion = (editedItem["Prd_PreDescripcion"].FindControl("txtPrd_PreDescripcionCons") as RadTextBox).Text.Trim();
                productoPrecio.Pre_Descripcion = (editedItem["Pre_Descripcion"].FindControl("lblTipoPrecioEditCons") as Label).Text.Trim();
                productoPrecio.Prd_Pesos = Convert.ToSingle((editedItem["Prd_Pesos"].FindControl("txtPrd_PesosCons") as RadNumericTextBox).Text);

                //Checar que es un rango de fechas correcto para SQL Server
                if (Convert.ToDateTime(productoPrecio.Prd_FechaFin).CompareTo(new DateTime(1753, 1, 1)) < 0 || Convert.ToDateTime(productoPrecio.Prd_FechaInicio).CompareTo(new DateTime(1753, 1, 1)) < 0)
                    throw new Exception("rgPrecios_FechasRango_incorrecto");

                List<ProductoPrecios> listaProdPre = new List<ProductoPrecios>();
                for (int i = 0; i < this.listSource.Count; i++)
                    listaProdPre.Add((ProductoPrecios)this.ClonarPrecioProducto(this.listSource[i]));

                for (int i = 0; i < this.listSource.Count; i++)
                {
                    listaProdPre[i].Prd_FechaInicio = null;
                    listaProdPre[i].Prd_FechaFin = null;
                }
                //this.ValidarPeriodosPrecioProducto(ref productoPrecio, listaProdPre);

                /// Guarda el AAA anterior

                if ((editedItem["Pre_Descripcion"].FindControl("lblTipoPrecioEditCons") as Label).Text.Trim() == "COSTO")
                {
                    int posicionAnterior = 0;

                    foreach (ProductoPrecios p in this.listSource)
                    {
                        if (p.Prd_Actual != false && p.Pre_Descripcion != "AAA")
                        {
                            posicionAnterior = posicionAnterior + 1;
                        }

                        if (p.Prd_Actual == true && p.Pre_Descripcion == "AAA")
                        {
                            List<ProductoPrecios> listaPPAAA = new List<ProductoPrecios>(this.listSource);
                            //  int posicionFila = rgPrecios.CurrentPageIndex * rgPrecios.PageSize + e.Item.ItemIndex;
                            listaPPAAA[posicionAnterior] = (ProductoPrecios)this.ClonarPrecioProducto(p);
                            listaPPAAA[posicionAnterior].Prd_Actual = false;
                            //  listaPP[posicionFila] = (ProductoPrecios)this.ClonarPrecioProducto(p);
                            this.listSource = listaPPAAA;
                            break;
                        }
                    }
                }


                /// aqui hay que buscar si se modifico AAA 
                double pAAA = 1 + (Convert.ToDouble(this.hdfAAA.Value) / 100);

                List<ProductoPrecios> listaProdPr = new List<ProductoPrecios>(this.listSource);
                if ((editedItem["Pre_Descripcion"].FindControl("lblTipoPrecioEditCons") as Label).Text.Trim() == "COSTO")
                {
                    float nAAA = Convert.ToSingle((editedItem["Prd_Pesos"].FindControl("txtPrd_PesosCons") as RadNumericTextBox).Text);

                    DateTime FecInicial = Convert.ToDateTime((editedItem["Prd_FechaInicio"].FindControl("datePickerFechaInicioCons") as RadDatePicker).SelectedDate.ToString());
                    DateTime FecFinal = Convert.ToDateTime((editedItem["Prd_FechaFin"].FindControl("datePickerFechaFinCons") as RadDatePicker).SelectedDate.ToString());

                    if (Math.Abs((FecFinal.Month - FecInicial.Month) + 12 * (FecFinal.Year - FecInicial.Year)) > 12)
                    {
                        throw new Exception("La diferencia entre las fechas No puede ser mayor a 1 año.");
                        return;
                    }

                    for (int ii = 1; ii < this.listSource.Count; ii++)
                    {
                        if (listaProdPr[ii].Pre_Descripcion == "AAA")
                        {
                            listaProdPr[ii].Prd_Pesos = Convert.ToSingle(nAAA * pAAA);  // 1.10
                            listaProdPr[ii].Prd_FechaInicio = FecInicial;
                            listaProdPr[ii].Prd_FechaFin = FecFinal;
                            this.listSource = listaProdPr;
                            break;
                        }
                    }
                }


                //Agregar precio a la lista actual  
                foreach (ProductoPrecios p in this.listSource)
                {
                    if (p.Id_Pre == productoPrecio.Id_Pre && p.Prd_Actual == productoPrecio.Prd_Actual && p.Prd_Actual == true)
                    {
                        List<ProductoPrecios> listaPP = new List<ProductoPrecios>(this.listSource);
                        int posicionFila = rgPreciosCons.CurrentPageIndex * rgPreciosCons.PageSize + e.Item.ItemIndex;
                        //listaPP[posicionFila - listaPP.Count / 2] = (ProductoPrecios)this.ClonarPrecioProducto(p);
                        //listaPP[posicionFila - listaPP.Count / 2].Prd_Actual = false;
                        listaPP[posicionFila] = (ProductoPrecios)this.ClonarPrecioProducto(productoPrecio);
                        //if (productoPrecio.Prd_Actual)
                        //    this.ValidarPeriodosPrecioProducto(ref productoPrecio, listSource);
                        this.listSource = listaPP;
                        break;
                    }
                }
                rgPreciosCons.Rebind();
            }
            catch (Exception ex)
            {  //ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                this.DisplayMensajeAlerta(ex.Message);
            }
        }

        protected void rgPreciosCons_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    rgPreciosCons.DataSource = this.listSource;
            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(ex.Message);
            }
        }

        protected void rgPreciosCons_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                ErrorManager();
                if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
                {
                    GridEditFormItem editItem = (GridEditFormItem)e.Item;

                    string datePickerFechaInicio = ((RadDatePicker)editItem.FindControl("datePickerFechaInicioCons")).ClientID.ToString();
                    string datePickerFechaFin = ((RadDatePicker)editItem.FindControl("datePickerFechaFinCons")).ClientID.ToString();
                    string txtPrd_Pesos = ((RadNumericTextBox)editItem.FindControl("txtPrd_PesosCons")).ClientID.ToString();

                    string jsControles = string.Concat(
                        "datePickerFechaInicioClientIdCons='", datePickerFechaInicio, "';"
                        , "datePickerFechaFinClientIdCons='", datePickerFechaFin, "';"
                        , "txtPrd_PesosClientIdCons='", txtPrd_Pesos, "';"
                        );

                    ImageButton insertbtn = (ImageButton)editItem.FindControl("PerformInsertButton");
                    if (insertbtn != null)
                    {
                        //jsControles = string.Concat(
                        //    jsControles
                        //    , "return ValidaFormGridPrecioProductos(\"insertar\");");

                        insertbtn.Attributes.Add("onclick", jsControles);
                    }

                    ImageButton updatebtn = (ImageButton)editItem.FindControl("UpdateButton");
                    if (updatebtn != null)
                    {
                        //jsControles = string.Concat(
                        //    jsControles
                        //    , "return ValidaFormGridPrecioProductos(\"actualizar\");");

                        updatebtn.Attributes.Add("onclick", jsControles);
                    }
                }
            }
            catch (Exception ex)
            {   //RadGrid1.Controls.Add(new LiteralControl("No se pudo agregar el Usuario. " + ex.Message));
                //ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                this.DisplayMensajeAlerta(ex.Message);
            }
        }

        protected void rgPreciosCons_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgPreciosCons.Rebind();
            }
            catch (Exception ex)
            {
                DisplayMensajeAlerta(string.Concat(ex.Message, "rgPreciosCons_PageIndexChanged"));
            }
        }

        protected void rgCorreosAutorizadores_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    Sesion Sesion = new Sesion();
                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                    CN_ComprasLocales cn_Listadocompralocal = new CN_ComprasLocales();


                    DataTable CatCorreos = new DataTable();
                    CatCorreos.Columns.Add("Id_Emp");
                    CatCorreos.Columns.Add("Id_Cd");
                    CatCorreos.Columns.Add("Id_Conf");
                    CatCorreos.Columns.Add("Id_MotivoCL");
                    CatCorreos.Columns.Add("Secuencia");
                    CatCorreos.Columns.Add("Desc_MotivoCL");
                    CatCorreos.Columns.Add("Id_Aplicacion");
                    CatCorreos.Columns.Add("Aplicacion");
                    CatCorreos.Columns.Add("Concepto");
                    CatCorreos.Columns.Add("Correo");

                    cn_Listadocompralocal.CatCorreosCL(ref CatCorreos, Sesion.Id_Emp, Sesion.Id_Cd, Sesion.Emp_Cnx);
                    rgCorreosAutorizadores.DataSource = CatCorreos;
                    rgCorreosAutorizadores.Rebind();

                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }

        protected void rgCorreosAutorizadores_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgCorreosAutorizadores.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void rgCorreosAutorizadores_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                int Id_Emp = 0;
                Id_Emp = Convert.ToInt32(((Label)e.Item.FindControl("lblId_Emp")).Text);
                int Id_CD = 0;
                Id_CD = Convert.ToInt32(((Label)e.Item.FindControl("lblId_Cd")).Text);
                int Id_Config = 0;
                Id_Config = Convert.ToInt32(((Label)e.Item.FindControl("lblId_Conf")).Text);
                string Motivo = ((Label)e.Item.FindControl("lblDesc_MotivoCL")).Text;
                int Id_Motivo;
                Id_Motivo = Convert.ToInt32(((Label)e.Item.FindControl("lblId_Motivo")).Text);
                int Secuencia;
                Secuencia = Convert.ToInt32(((Label)e.Item.FindControl("lblSecuencia")).Text);
                string Aplicaccion = ((Label)e.Item.FindControl("lblAplicacion")).Text;
                int Id_Aplicacion;
                Id_Aplicacion = Convert.ToInt32(((Label)e.Item.FindControl("lblId_Aplicacion")).Text);
                //  string concepto = ((Label)e.Item.FindControl("lblConcepto")).Text;
                string correo = ((Label)e.Item.FindControl("lblCorreo")).Text;


                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                CN_ComprasLocales cn_Listadocompralocal = new CN_ComprasLocales();

                if (e.CommandName == "Editar")
                {
                    GridEditFormItem item = e.Item as GridEditFormItem;

                    //this.hddIdMotivoCL.Value = Convert.ToString( lblId_Conf);
                    this.hddEmpresa.Value = Convert.ToInt32(Id_Emp).ToString();
                    this.cmbCDIMotivoCL.SelectedValue = cmbCDIMotivoCL.FindItemByValue(Id_CD.ToString()).Value;
                    this.cmbMotivoCL.SelectedValue = cmbMotivoCL.FindItemByText(Motivo.ToString()).Value;
                    this.txtAutoriza1.Text = correo;
                    this.cmbAplicacionMotCL.SelectedValue = cmbAplicacionMotCL.FindItemByText(Aplicaccion.ToString()).Value;
                    this.hddSecuencia.Value = Secuencia.ToString();
                    this.cmbCDIMotivoCL.Enabled = false;
                    this.cmbMotivoCL.Enabled = false;
                    this.cmbAplicacionMotCL.Enabled = false;
                }

                if (e.CommandName == "Eliminar")
                {
                    GridEditFormItem item = e.Item as GridEditFormItem;

                    cn_Listadocompralocal.CatEliminaCorreoNotifica(Id_Emp, Id_CD, Id_Motivo, Id_Aplicacion, Secuencia, Sesion.Emp_Cnx);

                    this.ListadoCorreosAutorizador();
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        #endregion

        #region EventosWeb

        [WebMethod]
        public static string[] GetProductoServicioSAT(string ProdServCve)
        {
            try
            {
                List<string> providers = new List<string>();

                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.AppSettings["strConnection"].ToString();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string txtcmd;
                        txtcmd = "spAABuscaProdServcicioSAT '" + ProdServCve + "'";
                        cmd.CommandText = txtcmd;
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                providers.Add(string.Format("{0}-{1}", sdr["IdCve"], sdr["ProdServicio"]));
                            }
                        }
                        conn.Close();
                    }
                }
                return providers.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public static string[] GetClienteName(string clienteName)
        {
            try
            {
                List<string> providers = new List<string>();

                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.AppSettings["strConnection"].ToString();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string txtcmd;
                        txtcmd = "spAABuscaCliente '" + clienteName + "'";
                        cmd.CommandText = txtcmd;
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                providers.Add(string.Format("{0}-{1}", sdr["Id_Cliente"], sdr["Cliente"]));
                            }
                        }
                        conn.Close();
                    }
                }
                return providers.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public static string[] GetProductName(string prodName)
        {
            try
            {
                List<string> products = new List<string>();

                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.AppSettings["strConnection"].ToString();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string txtcmd;
                        txtcmd = "spAABuscaProducto '" + prodName + "'";
                        cmd.CommandText = txtcmd;
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                products.Add(string.Format("{0}-{1}", sdr["Id_Prd"], sdr["Producto"]));
                            }
                        }
                        conn.Close();
                    }
                }


                return products.ToArray();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public static string[] GetAllProductName(string prodName)
        {
            try
            {
                List<string> products = new List<string>();

                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.AppSettings["strConnection"].ToString();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string txtcmd;
                        txtcmd = "spAABuscaProducto '" + prodName + "'";
                        cmd.CommandText = txtcmd;
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                products.Add(string.Format("{0}-{1}", sdr["Id_Prd"], sdr["Producto"]));
                            }
                        }
                        conn.Close();
                    }
                }


                return products.ToArray();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public static string[] GetProvidertName(string provName)
        {
            try
            {
                List<string> providers = new List<string>();

                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.AppSettings["strConnection"].ToString();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string txtcmd;
                        txtcmd = "spAABuscaProveedor '" + provName + "'";
                        cmd.CommandText = txtcmd;
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                providers.Add(string.Format("{0}-{1}", sdr["Id_Pvd"], sdr["Proveedor"]));
                            }
                        }
                        conn.Close();
                    }
                }
                return providers.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public static string[] GetProductoLocal(string prodName)
        {
            try
            {
                List<string> providers = new List<string>();

                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.AppSettings["strConnection"].ToString();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string txtcmd;
                        txtcmd = "spAABuscaProductoLocal '" + prodName + "'";
                        cmd.CommandText = txtcmd;
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                providers.Add(string.Format("{0}-{1}", sdr["Id_Prd"], sdr["Producto"]));
                            }
                        }
                        conn.Close();
                    }
                }
                return providers.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public static string[] GetProveedorAbasto(string provName)
        {
            try
            {
                List<string> providers = new List<string>();

                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.AppSettings["strConnection"].ToString();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string txtcmd;
                        txtcmd = "spAABuscaProveedor '" + provName + "'";
                        cmd.CommandText = txtcmd;
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                providers.Add(string.Format("{0}-{1}", sdr["Id_Pvd"], sdr["Proveedor"]));
                            }
                        }
                        conn.Close();
                    }
                }
                return providers.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public static string[] GetProveedorCte(string provName)
        {
            try
            {
                List<string> providers = new List<string>();

                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.AppSettings["strConnection"].ToString();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string txtcmd;
                        txtcmd = "spAABuscaProveedor '" + provName + "'";
                        cmd.CommandText = txtcmd;
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                providers.Add(string.Format("{0}-{1}", sdr["Id_Pvd"], sdr["Proveedor"]));
                            }
                        }
                        conn.Close();
                    }
                }
                return providers.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public static string[] GetProveedorSoli(string provName)
        {
            try
            {
                List<string> providers = new List<string>();

                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.AppSettings["strConnection"].ToString();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string txtcmd;
                        txtcmd = "spAABuscaProveedor '" + provName + "'";
                        cmd.CommandText = txtcmd;
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                providers.Add(string.Format("{0}-{1}", sdr["Id_Pvd"], sdr["Proveedor"]));
                            }
                        }
                        conn.Close();
                    }
                }
                return providers.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public static string GetCodigoDuplicado(string EvalCode)
        {
            try
            {
                string respuesta = "0";
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.AppSettings["strConnection"].ToString();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string txtcmd;
                        txtcmd = "spAAABuscaCodigoLocal '" + EvalCode + "'";
                        cmd.CommandText = txtcmd;
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                if (sdr["Duplicado"] == "1")
                                {
                                    respuesta = "1";
                                    throw new Exception("La diferencia entre las fechas No puede ser mayor a 1 año.");
                                    return respuesta;
                                }
                            }
                        }
                        conn.Close();
                    }
                }
                return respuesta;
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
