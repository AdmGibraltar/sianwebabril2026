using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocios;
using System.Net.Mail;
using System.Net;
using Telerik.Web.UI;
using System.Text;
using System.Net.Mime;
using System.Data;
using System.Collections;
using System.Xml;

namespace SIANWEB
{
    public partial class CapSolicitud : System.Web.UI.Page
    {

        #region Variables
        Telerik.Web.UI.RadComboBox RadCombobox;

        int hId_cliente = 0;
        int Esconsulta = 0;

        public int HId_cliente
        {
            get { return hId_cliente; }
            set { hId_cliente = value; }
        }

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

        public List<Producto> lstProductos
        {
            get { return (List<Producto>)Session["lstProductos" + Session.SessionID]; }
            set { Session["lstProductos" + Session.SessionID] = value; }
        }

        public DataTable objdtListaProd { get; set; }
        protected DataTable objdtTablaProd { get { if (ViewState["objdtTablaProd"] != null) { return (DataTable)ViewState["objdtTablaProd"]; } else { return objdtListaProd; } } set { ViewState["objdtTablaProd"] = value; } }

        public DataTable objdtListaDoc { get; set; }
        protected DataTable objdtTablaDoc { get { if (ViewState["objdtTablaDoc"] != null) { return (DataTable)ViewState["objdtTablaDoc"]; } else { return objdtListaDoc; } } set { ViewState["objdtTablaDoc"] = value; } }

        #endregion

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
                        int Id_Solicitud = Convert.ToInt32(Page.Request.QueryString["Id_Solicitud"]);
                        Inicializar(Id_Solicitud);
                        Tabla();

                    }
                }
            }
            catch (Exception ex)
            {
                RAM1.Alert("Error, " + ex.Message);
            }
        }

        private void Inicializar(int Id_Solicitud)
        {
            try
            {
                txtFecha.Text = DateTime.Now.ToShortDateString();
                CargarEstado();
                CargarAccion();
                CargarTServicio();
                CargarOcurrio();
                CargarPrioridad();
                CargarTQueja();
                CargarCC();
                RadEditorComentarios.ToolsFile = "BasicTools.xml";
                RadEditorRechazo.ToolsFile = "BasicTools.xml";
                InicializarTablaProductos();
                InicializarTablaDocumentos();
                Getlist();
                RadEditorComentarios.Content = string.Empty;
                RadEditorComentarios.Enabled = true;

                if (Id_Solicitud > 0)
                {
                    Esconsulta = 1;
                    LlenaFormularioSolicitud(Id_Solicitud);
                }
            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }
        }

        private void Getlist()
        {
            List<Producto> lstProductos = new List<Producto>();
            rgProductos.DataSource = this.lstProductos = new List<Producto>();
            rgProductos.DataBind();
        }

        private void LlenaFormularioSolicitud(int Id_Solicitud)
        {
            try
            {
                Sesion Sesion = new CapaEntidad.Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Solicitud solicitud = new Solicitud();
                solicitud.Id_Solicitud = Id_Solicitud;
                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Cd = Sesion.Id_Cd_Ver;
                configuracion.Id_Emp = Sesion.Id_Emp;
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                cn_configuracion.Consulta(ref configuracion, sesion.Emp_Cnx);
                List<Documento> lstDocumentos = new List<Documento>();
                List<Producto> lstProductos = new List<Producto>();
                CN_Solicitud CN = new CN_Solicitud();
                CN.ConsultaSolicitud(Sesion, ref solicitud, ref lstProductos, ref lstDocumentos);

                //Datos Cliente
                txtIdQueja.Text = solicitud.Id_Solicitud.ToString();
                Cmbsucursal.Text = sesion.Cd_Nombre;
                txtcli_Nombre.Text = solicitud.Cte_Nom;
                txtcorreo.Text = solicitud.Correo;
                txtId_Cliente.Text = solicitud.Id_Cliente.ToString();
                txtIdQueja.Enabled = false;

                //Datos del Tecnico
                txtInvestigador.Text = solicitud.Nom_Investigador;
                CmbCC.Text = solicitud.ConCopia;

                //General
                CmbEstado.SelectedIndex = solicitud.Id_Estado;
                CmbPrioridad.SelectedIndex = solicitud.Id_Prioridad;
                Cmbtservicio.SelectedIndex = solicitud.Id_tServicio;
                CmbAccion.SelectedIndex = solicitud.Id_Accion;

                //Categorización
                CmbTipoQueja.SelectedIndex = solicitud.Id_Categoria;

                //Descripción y Comentarios
                RadEditorComentarios.Content = solicitud.Comentarios;

                txtFechaEvento.Text = solicitud.FechaEvento.ToShortDateString();
                CargarMotivos(solicitud.Id_Categoria);
                SeleccionarMotivos(solicitud.Motivos);

                txtOtros.Text = solicitud.OtroMotivo;
                txtDescripcion.Value = solicitud.Descripcion;
                cmbOcurrio.SelectedValue = solicitud.DondeOcurrio;

                RadEditorRechazo.Content = solicitud.MotRechazo;

                if (solicitud.Id_Estado == 5)
                    RadEditorRechazo.Visible = true;
                else
                    RadEditorRechazo.Visible = false;


                rgProductos.DataSource = lstProductos.Where(Producto => Producto.Mantener == true);
                rgProductos.DataBind();

                CargarProductos(lstProductos);

                //Se cargan los documentos relacionados
                gvFiles.DataSource = lstDocumentos.Where(Documento => Documento.TipoDoc != "Solucion");
                gvFiles.DataBind();

                gvSolucion.DataSource = lstDocumentos.Where(Documento => Documento.TipoDoc == "Solucion");
                gvSolucion.DataBind();

                CargarDocumentos(lstDocumentos);

                DeshabilitaCampos();
                CmbAccion.Enabled = false;
                RadToolBar1.Enabled = false;
                RadEditorComentarios.Enabled = false;
                RadEditorRechazo.Enabled = false;
                CmbEstado.Enabled = false;
                rgProductos.Enabled = false;


                lblSolicitud.Text = Id_Solicitud.ToString();
            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }
        }

        private void HabilitaCampos()
        {
            try
            {
                txtInvestigador.Enabled = true;
                CmbCC.Enabled = true;
                CmbEstado.Enabled = true;
                CmbAccion.Enabled = true;
                Cmbtservicio.Enabled = true;
                CmbTipoQueja.Enabled = true;
                RadEditorComentarios.Enabled = true;
            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }
        }

        private void DeshabilitaCampos()
        {
            try
            {

                txtIdQueja.Enabled = false;
                txtInvestigador.Enabled = false;
                CmbCC.Enabled = false;
                CmbPrioridad.Enabled = false;
                cmbOcurrio.Enabled = false;
                CmbEstado.Enabled = false;
                CmbAccion.Enabled = false;
                Cmbtservicio.Enabled = false;
                CmbTipoQueja.Enabled = false;
                RadEditorComentarios.Enabled = false;
                RadEditorRechazo.Enabled = false;

            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
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

                RlbMotivos.DataSource = dsMotivos.Tables[0];
                RlbMotivos.DataTextField = "Descripcion";
                RlbMotivos.DataValueField = "Id";
                RlbMotivos.DataBind();

                foreach (RadListBoxItem item in RlbMotivos.Items)
                {
                    item.Checked = false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SeleccionarMotivos(string Motivos)
        {
            String value = Motivos;
            Char delimiter = ',';
            string motivosseleccionados = "";
            String[] seleccion = value.Split(delimiter);
            foreach (string valor in seleccion)
            {
                if (valor != "")
                {
                    foreach (RadListBoxItem item in RlbMotivos.Items)
                    {
                        if (item.Value == valor)
                        {
                            motivosseleccionados = motivosseleccionados + item.Value + " - " + item.Text + " \n";

                        }
                        if (item.Value == "-1")
                        {
                            motivosseleccionados = motivosseleccionados + 46 + " - " + "No Aplica" + " \n";
                        }
                    }
                }
                txtMotivos.Value = motivosseleccionados;
            }
        }

        private void CargarDocumentos(List<Documento> lstDocumentos)
        {
            try
            {
                foreach (Documento doc in lstDocumentos)
                {
                    ArrayList ArrayDoc = new ArrayList();
                    ArrayDoc.Add(doc.Id_Doc);
                    ArrayDoc.Add(doc.Doc_Nombre);
                    ArrayDoc.Add(doc.Formato);
                    ArrayDoc.Add(doc.Tamano);

                    objdtTablaDoc.Rows.Add(ArrayDoc.ToArray());
                }
            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }
        }

        private void CargarTQueja()
        {
            try
            {
                Sesion Sesion = new CapaEntidad.Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                CapaNegocios.CN__Comun comun = new CapaNegocios.CN__Comun();
                comun.LlenaCombo(Sesion.Emp_Cnx, "spCatTipoQuejas_Combo", ref CmbTipoQueja);
                if (Lista.Count > 0)
                {
                    CmbTipoQueja.DataSource = Lista;
                    CmbTipoQueja.DataValueField = "Id";
                    CmbTipoQueja.DataTextField = "Descripcion";
                    CmbTipoQueja.DataBind();
                }
            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
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

        private void CargarPrioridad()
        {
            try
            {
                Sesion Sesion = new CapaEntidad.Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<Comun> Lista = new List<Comun>();
                CapaNegocios.CN__Comun comun = new CN__Comun();
                comun.LlenaCombo(sesion.Emp_Cnx, "spCatPrioridad_Combo", ref CmbPrioridad);
                if (Lista.Count > 0)
                {
                    CmbPrioridad.DataSource = Lista;
                    CmbPrioridad.DataValueField = "Id";
                    CmbPrioridad.DataTextField = "Descripcion";
                    CmbPrioridad.DataBind();
                }
            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }
        }

        private void CargarTServicio()
        {
            try
            {
                Sesion Sesion = new CapaEntidad.Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                CapaNegocios.CN__Comun comun = new CapaNegocios.CN__Comun();
                comun.LlenaCombo(Sesion.Emp_Cnx, "spCattiposervicio_Combo", ref Cmbtservicio);
                if (Lista.Count > 0)
                {
                    Cmbtservicio.DataSource = Lista;
                    Cmbtservicio.DataValueField = "Id";
                    Cmbtservicio.DataTextField = "Descripcion";
                    Cmbtservicio.DataBind();
                }
            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }
        }

        private void CargarAccion()
        {
            try
            {
                Sesion Sesion = new CapaEntidad.Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                CapaNegocios.CN__Comun comun = new CapaNegocios.CN__Comun();
                comun.LlenaCombo(sesion.Emp_Cnx, "spCatAcciones_Combo", ref CmbAccion);
                if (Lista.Count > 0)
                {
                    CmbAccion.DataSource = Lista;
                    CmbAccion.DataValueField = "Id";
                    CmbAccion.DataTextField = "Descripcion";
                    CmbAccion.DataBind();
                }
            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }
        }

        private void CargarEstado()
        {
            try
            {
                Sesion Sesion = new CapaEntidad.Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                CapaNegocios.CN__Comun comun = new CapaNegocios.CN__Comun();
                comun.LlenaCombo(sesion.Emp_Cnx, "spCatEstado_Combo", ref CmbEstado);
                if (Lista.Count > 0)
                {
                    CmbEstado.DataSource = Lista;
                    CmbEstado.DataValueField = "Id";
                    CmbEstado.DataTextField = "Descripcion";
                    CmbEstado.DataBind();
                }
            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }
        }

        private void CargarCC()
        {
            try
            {
                Sesion Sesion = new CapaEntidad.Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                CapaNegocios.CN__Comun comun = new CapaNegocios.CN__Comun();
                comun.LlenaCombo(sesion.Emp_Cnx, "spCatUsuarioCC_Combo", ref CmbCC);
                if (Lista.Count > 0)
                {
                    CmbCC.DataSource = Lista;
                    CmbCC.DataValueField = "Id";
                    CmbCC.DataTextField = "Descripcion";
                    CmbCC.DataBind();
                }
            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }
        }

        private void LimpiarDatosClientes()
        {
            txtIdQueja.Text = string.Empty;
            txtcli_Nombre.Text = string.Empty;
            txtcorreo.Text = string.Empty;
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            Inicializar(0);
            RadEditorComentarios.Content = string.Empty;
            txtIdQueja.Enabled = true;
            gvFiles.DataSource = null;
            gvFiles.DataBind();
            gvSolucion.DataSource = null;
            gvSolucion.DataBind();
        }


        private int ValidarCampos()
        {
            if (txtIdQueja.Text == "")
            {
                Alerta("Favor de escribir un ID_Queja, este campo es obligatorio.");
                return 0;
            }
            if (txtInvestigador.Text == null)
            {
                Alerta("Debe seleccionar investigador, este campo es obligatorio.");
                return 0;
            }
            if (CmbTipoQueja.SelectedIndex == 0)
            {
                Alerta("Debe seleccionar tipo de queja, este campo es obligatorio.");
                return 0;
            }

            if (CmbEstado.SelectedIndex == 0)
            {
                Alerta("Debe seleccionar un estado, este campo es obligatorio.");
                return 0;
            }
            if (Cmbtservicio.SelectedIndex == 0)
            {
                Alerta("Debe seleccionar un tipo de servicio, este campo es obligatorio.");
                return 0;
            }

            if (CmbAccion.SelectedIndex == 1)
            {

                for (int x = 0; x < objdtTablaProd.Rows.Count; x++)
                {
                    int Id_Doc = Convert.ToInt32(objdtTablaProd.Rows[x][16]);
                    if (Id_Doc == 0)
                    {
                        Alerta("Debe ingresar el Id documento en la tabla de productos para todos los registros, este campo es obligatorio.");
                        return 0;
                    }
                }
            }

            return 1;
        }

        private void RecalculasTotalAAA(DataTable objdtTablaProd)
        {
            Double TotalAAA = 0;
            Double TotalEstandar = 0;
            Double Costo = 0;
            Double Estandar = 0;
            int Cantidad = 0;

            foreach (DataRow Row in objdtTablaProd.Rows)
            {
                if (Convert.ToBoolean(Row["Mantener"]) == true)
                {
                    Cantidad = Convert.ToInt32(Row["Cantidad"]);
                    //Costo AAA
                    Costo = Convert.ToDouble(Row["Costo"]);
                    TotalAAA = TotalAAA + (Cantidad * Costo);
                    //Precio Estandar
                    Estandar = Convert.ToDouble(Row["Costoestandar"]);
                    TotalEstandar = TotalEstandar + (Cantidad * Estandar);
                }
            }
            txtTotalAAA.Text = TotalAAA.ToString();
            txtTotalEstandar.Text = TotalEstandar.ToString();
        }

        private void CargarProductos(List<Producto> lstproducto)
        {
            try
            {
                Double TotalAAA = 0;
                Double TotalEstandar = 0;
                foreach (Producto prd in lstproducto)
                {
                    ArrayList ArrayProd = new ArrayList();
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
                    ArrayProd.Add(prd.Mantener);
                    ArrayProd.Add(prd.Transferencia);
                    ArrayProd.Add(prd.Rem_Trans);
                    ArrayProd.Add(prd.Costoestandar);
                    ArrayProd.Add(prd.NCredito);

                    TotalAAA = TotalAAA + (prd.Cantidad * prd.Costo);
                    TotalEstandar = TotalEstandar + (prd.Cantidad * prd.Costoestandar);

                    objdtTablaProd.Rows.Add(ArrayProd.ToArray());
                }
                txtTotalAAA.Text = TotalAAA.ToString();
                txtTotalEstandar.Text = TotalEstandar.ToString();
            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }
        }

        private void EliminarProducto(Int64 id_Prd)
        {
            List<Producto> lista = this.lstProductos;

            //buscar producto de orden de compra en la lista
            for (int i = 0; i < lista.Count; i++)
            {
                Producto orden = lista[i];
                if (orden.Id_Prd == id_Prd)
                {
                    lista.RemoveAt(i);
                    break;
                }
            }
            this.lstProductos = lista;
        }

        protected void RadToolBar1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;

                switch (btn.CommandName)
                {
                    case "Nuevo":
                        Nuevo();
                        break;
                    case "Guardar":
                        // Guardar();
                        break;
                    case "Listado":
                        break;
                }
            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }
        }

        private void Nuevo()
        {
            try
            {
                LimpiarDatosClientes();
                //LimpiarFormulario();
            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }
        }

        private void InicializarTablaProductos()
        {
            try
            {
                objdtListaProd = new DataTable();
                objdtListaProd.Columns.Add("Id_Prd");
                objdtListaProd.Columns.Add("Descripcion");
                objdtListaProd.Columns.Add("Presentacion");
                objdtListaProd.Columns.Add("Cantidad");
                objdtListaProd.Columns.Add("Lote");
                objdtListaProd.Columns.Add("Fabricacion");
                objdtListaProd.Columns.Add("Caducidad");
                objdtListaProd.Columns.Add("Marca");
                objdtListaProd.Columns.Add("Costo");
                objdtListaProd.Columns.Add("Num_Fac");
                objdtListaProd.Columns.Add("Nom_Prov");
                objdtListaProd.Columns.Add("Id_motivo");
                objdtListaProd.Columns.Add("Motivo");
                objdtListaProd.Columns.Add("Id_procede");
                objdtListaProd.Columns.Add("Procede");
                objdtListaProd.Columns.Add("Transferencia");
                objdtListaProd.Columns.Add("Rem_Trans");
                objdtListaProd.Columns.Add("CostoEstandar");
                objdtListaProd.Columns.Add("NCredito");
                objdtTablaProd = objdtListaProd;
            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }
        }

        private void InicializarTablaDocumentos()
        {
            try
            {
                objdtListaDoc = new DataTable();
                objdtListaDoc.Columns.Add("Id_Doc");
                objdtListaDoc.Columns.Add("Doc_Nombre");
                objdtListaDoc.Columns.Add("Tipo");
                objdtListaDoc.Columns.Add("Tamaño");

                objdtTablaDoc = objdtListaDoc;
            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }
        }

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

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    case "RebindGrid":
                        rgProductos.Rebind();
                        break;
                }
            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }
        }

        protected void rgProductos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {

                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    rgProductos.DataSource = objdtTablaProd;
                }
            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }
        }

        protected void rgProductos_InsertCommand(object sender, GridCommandEventArgs e)
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
                producto.Prd_UniEmp = Convert.ToInt32((insertedItem["Prd_UniEmp"].FindControl("txtPrd_UniEmp") as RadNumericTextBox).Text);

                producto.Lote = (insertedItem["Lote"].FindControl("txtLote") as RadTextBox).Text;
                producto.Fabricacion = Convert.ToDateTime((insertedItem["Fabricacion"].Controls[0] as RadDatePicker).SelectedDate.ToString());
                producto.Caducidad = Convert.ToDateTime((insertedItem["Caducidad"].Controls[0] as RadDatePicker).SelectedDate.ToString());

                producto.Marca = (insertedItem["Marca"].FindControl("txtMarca") as RadTextBox).Text;
                producto.Costo = float.Parse((insertedItem["Costo"].FindControl("txtCosto") as RadNumericTextBox).Text);
                producto.Num_Fac = Convert.ToInt32((insertedItem["Facorem"].FindControl("txtFacorem") as RadNumericTextBox).Text);
                producto.Nom_Prov = (insertedItem["Proveedor"].FindControl("txtProveedor") as RadTextBox).Text;

                //this.ListaProductos_AgregarProducto(producto);    

                ArrayList ArrayProd = new ArrayList();
                ArrayProd.Add(producto.Id_Prd);
                ArrayProd.Add(producto.Descripcion);
                ArrayProd.Add(producto.Presentacion);
                ArrayProd.Add(producto.Cantidad);
                ArrayProd.Add(producto.Prd_UniEmp);
                ArrayProd.Add(producto.Lote);
                ArrayProd.Add(producto.Fabricacion);
                ArrayProd.Add(producto.Caducidad);
                ArrayProd.Add(producto.Marca);
                ArrayProd.Add(producto.Costo);
                ArrayProd.Add(producto.Num_Fac);
                ArrayProd.Add(producto.Nom_Prov);
                ArrayProd.Add(producto.Mantener);
                ArrayProd.Add(producto.Transferencia);
                ArrayProd.Add(producto.Rem_Trans);
                ArrayProd.Add(producto.Costoestandar);
                ArrayProd.Add(producto.NCredito);
                objdtTablaProd.Rows.Add(ArrayProd.ToArray());
            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }
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
                producto.Prd_UniEmp = Convert.ToInt32((insertedItem["Prd_UniEmp"].FindControl("txtPrd_UniEmp") as RadNumericTextBox).Text);
                producto.Lote = (insertedItem["Lote"].FindControl("txtLote") as RadTextBox).Text;
                producto.Fabricacion = Convert.ToDateTime((insertedItem["Fabricacion"].Controls[0] as RadDatePicker).SelectedDate.ToString());
                producto.Caducidad = Convert.ToDateTime((insertedItem["Caducidad"].Controls[0] as RadDatePicker).SelectedDate.ToString());
                producto.Marca = (insertedItem["Marca"].FindControl("txtMarca") as RadTextBox).Text;
                producto.Costo = float.Parse((insertedItem["Costo"].FindControl("txtCosto") as RadNumericTextBox).Text);
                producto.Num_Fac = Convert.ToInt32((insertedItem["Facorem"].FindControl("txtFacorem") as RadNumericTextBox).Text);
                producto.Nom_Prov = (insertedItem["Proveedor"].FindControl("txtProveedor") as RadTextBox).Text;

                //Campos nuevo en control de cambio
                //04-02-2018
                producto.Mantener = Convert.ToBoolean((insertedItem["Mantener"].FindControl("cmbMantener") as RadComboBox).Text);
                producto.Transferencia = Convert.ToInt32((insertedItem["Transferencia"].FindControl("txtTransferencia") as RadNumericTextBox).Text);
                producto.Rem_Trans = Convert.ToInt32((insertedItem["Rem_Trans"].FindControl("txtRem_Trans") as RadNumericTextBox).Text);
                producto.Costoestandar = float.Parse((insertedItem["Costoestandar"].FindControl("txtCostoestandar") as RadNumericTextBox).Text);
                producto.NCredito = Convert.ToInt32((insertedItem["NCredito"].FindControl("txtNCredito") as RadNumericTextBox).Text);

                DataRow[] Ar_dr;

                Ar_dr = objdtTablaProd.Select("Id_Prd='" + Convert.ToInt64(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id_Prd"]) + "'");
                if (Ar_dr.Length > 0)
                {
                    Ar_dr[0].BeginEdit();
                    Ar_dr[0]["Id_Prd"] = producto.Id_Prd;
                    Ar_dr[0]["Descripcion"] = producto.Descripcion;
                    Ar_dr[0]["Presentacion"] = producto.Presentacion;
                    Ar_dr[0]["Cantidad"] = producto.Cantidad;
                    Ar_dr[0]["Prd_UniEmp"] = producto.Prd_UniEmp;
                    Ar_dr[0]["Lote"] = producto.Lote;
                    Ar_dr[0]["Fabricacion"] = producto.Fabricacion.ToShortDateString();
                    Ar_dr[0]["Caducidad"] = producto.Caducidad.ToShortDateString();
                    Ar_dr[0]["Marca"] = producto.Marca;
                    Ar_dr[0]["Costo"] = producto.Costo;
                    Ar_dr[0]["Num_Fac"] = producto.Num_Fac;
                    Ar_dr[0]["Nom_Prov"] = producto.Nom_Prov;
                    Ar_dr[0]["Mantener"] = producto.Mantener;
                    Ar_dr[0]["Transferencia"] = producto.Transferencia;
                    Ar_dr[0]["Rem_Trans"] = producto.Rem_Trans;
                    Ar_dr[0]["costoestandar"] = producto.Costoestandar;
                    Ar_dr[0]["Ncredito"] = producto.NCredito;

                    objdtTablaProd.AcceptChanges();
                }
                rgProductos.Rebind();
            }
            catch (Exception ex)
            {
                Alerta("Error, " + ex.Message);
            }
        }

        protected void rgProductos_DeleteCommand1(object sender, GridCommandEventArgs e)
        {
        }

        protected void rgProductos_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
        }

        protected void rgProductos_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }

        protected void rgProductos_ItemCreated(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
            {
                GridEditableItem editItem = (GridEditableItem)e.Item;
            }
        }

        protected void rgProductos_ItemCommand(object sender, GridCommandEventArgs e)
        {
        }

        #region Correos

        private string FormatMultipleEmailAddresses(string emailAddresses)
        {
            var delimiters = new[] { ',', ';' };

            var addresses = emailAddresses.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            return string.Join(",", addresses);
        }

        private void EnviaCorreoCxC(Solicitud solicitud)
        { }

        private void EnviarCorreoInvestigador(Solicitud solicitud)
        { }

        private void EnviarCorreoDevolucionCedis(Solicitud solicitud)
        { }

        private void EnviarCorreoCliente(Solicitud solicitud, int Actualiza)
        { }

        private void EnviarCorreoJefePlaneacion(Solicitud solicitud, int Id_U, int Id_Estado)
        { }

        #endregion

        protected void RadAsyncUpload_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            string path = Server.MapPath("~/App_Data/");
            e.File.SaveAs(path + e.File.GetName());
        }

        protected void RadTabStrip1_TabClick(object sender, RadTabStripEventArgs e)
        {
            if (((Telerik.Web.UI.RadTabStrip)(sender)).SelectedTab.Value == "Productos")
            {
            }
            if (((Telerik.Web.UI.RadTabStrip)(sender)).SelectedTab.Value == "Adjuntos")
            {
                if (objdtTablaProd.Rows.Count == 0)
                {
                    Alerta("Antes de cargar archivos, debe agregar los productos.");
                    return;

                }
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

        protected void gvSolucion_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                gvSolucion.DataSource = objdtListaDoc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvSolucion_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                gvSolucion.DataSource = objdtListaDoc;
                RecalculasTotalAAA(objdtTablaProd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvSolucion_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            try
            {
                gvSolucion.DataSource = objdtListaDoc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvFiles_ItemCommand(object sender, GridCommandEventArgs e)
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
                Response.Flush();
                Response.End();
            }
        }

        protected void gvFiles_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                gvFiles.DataSource = objdtListaDoc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvFiles_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            try
            {
                gvFiles.DataSource = objdtListaDoc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}