using CapaEntidad;
using CapaNegocios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SIANWEB
{
    public partial class CatVehiculo : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        public string Valor
        {
            get
            {
                return MaximoId();
            }
            set { }
        }

        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        this.ValidarPermisos();
                        if (sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        this.rgVehiculos.Rebind();
                        this.CargarCentros();
                        Carga_ConfigAutotransporte();
                        Carga_TipoPermiso();
                        Carga_TipoRemolque();
                        this.hiddenActualiza.Value = string.Empty;
                        this.txtIdVehiculo.Text = this.Valor;
                        this.txtNombre.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Alerta(string.Concat(ex.Message, "Page_Load_error"));
            }
        }
        protected void cmbCentrosDist_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
            }
            catch (Exception ex)
            {
                Alerta(string.Concat(ex.Message, "Cmb_CentroDistribucion_IndexChanging_error"));
            }
        }
        protected void rgVehiculos_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                if (e.CommandName.ToString() == "Modificar")
                {
                    Int32 item = default(Int32);
                    item = e.Item.ItemIndex;
                    if (item >= 0)
                    {
                        hiddenActualiza.Value = rgVehiculos.Items[item]["Folio"].Text;
                        txtIdVehiculo.Text = rgVehiculos.Items[item]["Folio"].Text;
                        txtIdVehiculo.Enabled = false;
                        txtNombre.Text = rgVehiculos.Items[item]["Nom_Vehiculo"].Text;
                        txtId_ConfigVehiculo.Text = rgVehiculos.Items[item]["Id_ConfigVehiculo"].Text;
                        cmbConfiguracion.SelectedItem.Text = rgVehiculos.Items[item]["ConfiguracionVehiculo"].Text;
                        txtPlaca.Text = rgVehiculos.Items[item]["PlacaVehiculo"].Text;
                        txtModelo.Text = rgVehiculos.Items[item]["ModeloVehiculo"].Text;

                        cmbTipoRemolque.SelectedItem.Text = Convert.IsDBNull(rgVehiculos.Items[item]["TipoRemolque"].Text) ? "" : rgVehiculos.Items[item]["TipoRemolque"].Text;
                        if (rgVehiculos.Items[item]["TipoRemolque"].Text == "&nbsp;")
                        {
                            Carga_TipoRemolque();
                        }

                        txtPlacaRemolque.Text = Convert.IsDBNull(rgVehiculos.Items[item]["PlacaRemolque"].Text) ? "" : rgVehiculos.Items[item]["PlacaRemolque"].Text;
                        if (rgVehiculos.Items[item]["PlacaRemolque"].Text == "&nbsp;")
                        {
                            txtPlacaRemolque.Text = "";
                        }
                        txtIdPermisoSCT.Text = rgVehiculos.Items[item]["IdPermisoSCT"].Text;
                        cmbPermisoSCT.SelectedItem.Text = rgVehiculos.Items[item]["PermisoSCT"].Text;
                        txtNumPermiso.Text = rgVehiculos.Items[item]["NumPermiso"].Text;
                        txtPoliza.Text = rgVehiculos.Items[item]["Poliza"].Text;
                        txtAseguradora.Text = rgVehiculos.Items[item]["Aseguradora"].Text;
                    }
                }
            }
            catch (Exception ex)
            {
                Alerta(string.Concat(ex.Message, "rgFamilia_ItemCommand"));
            }
        }
        protected void rgVehiculos_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                    rgVehiculos.DataSource = GetList();
            }
            catch (Exception ex)
            {
                Alerta(string.Concat(ex.Message, "rgVehiculos_NeedDataSource"));
            }
        }

        protected void rgVehiculos_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                this.rgVehiculos.Rebind();
            }
            catch (Exception ex)
            {
                Alerta(string.Concat(ex.Message, "rgVehiculos_PageIndexChanged"));
            }
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            string accionError = string.Empty;
            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;
                switch (btn.CommandName)
                {
                    case "new":
                        LimpiarCampos();
                        this.txtIdVehiculo.Text = this.Valor;
                        break;

                    case "save":
                        this.Guardar();
                        break;
                }
            }
            catch (Exception ex)
            {
                string mensaje = string.Concat(ex.Message, hiddenActualiza.Value == string.Empty ? "FamProducto_insert_error" : "FamProducto_update_error");
                Alerta(mensaje);
            }
        }

        protected void cmbConfiguracion_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cmbConfiguracion.SelectedValue == "-1")
            {
                txtId_ConfigVehiculo.Text = "";
            }
            else
            {
                txtId_ConfigVehiculo.Text = cmbConfiguracion.SelectedValue;
            }
        }

        protected void cmbPermisoSCT_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cmbPermisoSCT.SelectedValue == "-1")
            {
                txtIdPermisoSCT.Text = "";
            }
            else
            {
                txtIdPermisoSCT.Text = cmbPermisoSCT.SelectedValue;
            }
        }


        #endregion

        #region Funciones

        private string MaximoId()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(sesion.Id_Emp, "CatVehiculo", "Folio", sesion.Emp_Cnx, "spCatCentral_Maximo");
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
                if (hiddenActualiza.Value != "") //Hidden Field BANDERA
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                    Catalogo ct = new Catalogo();
                    ct.Id_Emp = sesion.Id_Emp; //Si no ocupa empresa su catalogo se pone -1
                    ct.Id_Cd = -1; //Si no ocupa Centro de distribución se pone -1
                    ct.Id = Convert.ToInt32(hiddenActualiza.Value); //Si no ocupa Id se pone -1
                    ct.Tabla = "CatVehiculo"; //Nombre de la tabla del catalogo
                    ct.Columna = "Id_Vehiculo"; //Nombre de la columna del ID del catalogo
                    CN_Comun.Deshabilitar(ct, sesion.Emp_Cnx, ref verificador);
                }
                return verificador;
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
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Pagina pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                if (pag.Length > 1)
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                else
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;
                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, Sesion.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;
                Permiso Permiso = new Permiso();
                Permiso.Id_U = Sesion.Id_U;
                Permiso.Id_Cd = Sesion.Id_Cd;
                Permiso.Sm_cve = pagina.Clave;
                //Esta clave depende de la pantalla
                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, Sesion.Emp_Cnx);

                if (Permiso.PAccesar == true)
                {
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;

                    if (Permiso.PGrabar == false)
                        this.RadToolBar1.Items[6].Visible = false;
                    if (Permiso.PGrabar == false && Permiso.PModificar == false)
                        this.RadToolBar1.Items[5].Visible = false;
                    //Regresar
                    this.RadToolBar1.Items[4].Visible = false;
                    //Eliminar
                    this.RadToolBar1.Items[3].Visible = false;
                    //Imprimir
                    this.RadToolBar1.Items[2].Visible = false;
                    //Correo
                    this.RadToolBar1.Items[1].Visible = false;
                }
                else
                    Response.Redirect("Inicio.aspx");

                CN_Ctrl ctrl = new CN_Ctrl();
                ctrl.ValidarCtrl(Sesion, pagina.Clave, divPrincipal);
                ctrl.ListaCtrls(Sesion.Emp_Cnx, pagina.Clave, divPrincipal.Controls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Guardar()
        {
            try
            {
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                Vehiculo vehiculo = new Vehiculo();

                vehiculo.Folio = txtIdVehiculo.Text.Trim() == string.Empty ? 0 : Convert.ToInt32(txtIdVehiculo.Text);
                vehiculo.Id_Emp = session.Id_Emp;
                vehiculo.Id_Cd = session.Id_Cd_Ver;

                if (txtNombre.Text == "")
                {
                    Alerta("El campo Nombre de vehículo es obligatorio, favor de proporcionar la información para continuar.");
                    txtNombre.Focus();
                    return;
                }
                else
                {
                    vehiculo.Nom_Vehiculo = txtNombre.Text;
                }

                if (txtPlaca.Text == "")
                {
                    Alerta("El campo Placa vehículo es obligatorio, favor de proporcionar la información para continuar.");
                    txtPlaca.Focus();
                    return;
                }
                else
                {
                    vehiculo.PlacaVehiculo = txtPlaca.Text;
                }

                if (txtModelo.Text == "" || txtNombre.Text == "0000")
                {
                    Alerta("El campo Modelo vehículo es obligatorio, favor de proporcionar la información para continuar, ejemplo: año a 4 digitos 2020.");
                    txtModelo.Focus();
                    return;
                }
                else
                {
                    vehiculo.ModeloVehiculo = txtModelo.Text;
                }

                if (txtId_ConfigVehiculo.Text == "")
                {
                    Alerta("El campo clave conf. es obligatorio, favor de proporcionar la información para continuar.");
                    txtId_ConfigVehiculo.Focus();
                    return;
                }
                else { vehiculo.Id_ConfigVehiculo = txtId_ConfigVehiculo.Text; }

                if (cmbConfiguracion.SelectedItem.Text == "")
                {
                    Alerta("El campo clave configuración vehículo es obligatorio, favor de proporcionar la información para continuar.");
                    cmbConfiguracion.Focus();
                    return;
                }
                else { vehiculo.ConfiguracionVehiculo = cmbConfiguracion.SelectedItem.Text; }

                if (txtIdPermisoSCT.Text == "")
                {
                    Alerta("El campo clave permiso es obligatorio, favor de proporcionar la información para continuar.");
                    txtIdPermisoSCT.Focus();
                    return;
                }
                else { vehiculo.IdPermisoSCT = txtIdPermisoSCT.Text; }

                if (cmbPermisoSCT.SelectedItem.Text == "")
                {
                    Alerta("El campo Permiso SCT es obligatorio, favor de proporcionar la información para continuar.");
                    cmbPermisoSCT.Focus();
                    return;
                }
                else { vehiculo.PermisoSCT = cmbPermisoSCT.SelectedItem.Text; }

                if (txtNumPermiso.Text == "")
                {
                    Alerta("El campo número permiso es obligatorio, favor de proporcionar la información para continuar.");
                    txtNumPermiso.Focus();
                    return;
                }
                else { vehiculo.NumPermiso = txtNumPermiso.Text; }

                if (txtPoliza.Text == "")
                {
                    Alerta("El campo poliza es obligatorio, favor de proporcionar la información para continuar.");
                    txtPoliza.Focus();
                    return;
                }
                else { vehiculo.Poliza = txtPoliza.Text; }

                if (txtAseguradora.Text == "")
                {
                    Alerta("El campo aseguradora es obligatorio, favor de proporcionar la información para continuar.");
                    txtAseguradora.Focus();
                    return;
                }
                else { vehiculo.Aseguradora = txtAseguradora.Text; }

                CN_CapRemision CN = new CN_CapRemision();
                int verificador = -1;

                CN.InsertarVehiculo(vehiculo, session.Emp_Cnx, ref verificador);
                if (verificador > 0)
                {
                    this.LimpiarCampos();
                    txtIdVehiculo.Enabled = false;
                    txtIdVehiculo.Text = this.Valor;
                    txtNombre.Focus();
                    Alerta("Los datos se guardaron correctamente.");

                    rgVehiculos.Rebind();
                }
                else
                {
                    Alerta("Ha ocurrido un error, favor de intentarlo de nuevo.");
                    LimpiarCampos();
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LimpiarCampos()
        {
            try
            {
                txtNombre.Text = string.Empty;
                txtModelo.Text = string.Empty;
                txtPlaca.Text = string.Empty;
                Carga_TipoRemolque();
                txtPlacaRemolque.Text = string.Empty;
                Carga_ConfigAutotransporte();
                txtId_ConfigVehiculo.Text = string.Empty;
                Carga_TipoPermiso();
                txtIdPermisoSCT.Text = string.Empty;
                txtNumPermiso.Text = string.Empty;
                txtPoliza.Text = string.Empty;
                txtAseguradora.Text = string.Empty;

                txtIdVehiculo.Enabled = false;
                txtNombre.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<Vehiculo> GetList()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<Vehiculo> listVehiculos = new List<Vehiculo>();
                CN_CapRemision CN = new CN_CapRemision();
                Vehiculo Vehiculo = new Vehiculo();
                Vehiculo.Id_Emp = sesion.Id_Emp;
                Vehiculo.Id_Cd = sesion.Id_Cd;
                CN.ConsultaVehiculos(Vehiculo, sesion.Emp_Cnx, ref listVehiculos);
                return listVehiculos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarCentros()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();

                if (Sesion.U_MultiOfi == false)
                {
                    CN_Comun.LlenaCombo(2, Sesion.Id_Emp, Sesion.Id_U, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.Visible = false;
                    this.TblEncabezado.Rows[0].Cells[2].InnerText = " " + CmbCentro.FindItemByValue(Sesion.Id_Cd_Ver.ToString()).Text;
                }
                else
                {
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, Sesion.Id_Cd_Ver, Sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = Sesion.Id_Cd_Ver.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Carga_TipoPermiso()
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                DataSet dsPermisoSCT = new DataSet();
                new CN_CapRemision().ConsultaPermisoSCT(sesion, ref dsPermisoSCT);

                cmbPermisoSCT.DataSource = dsPermisoSCT.Tables[0];
                cmbPermisoSCT.DataTextField = "Descripcion";
                cmbPermisoSCT.DataValueField = "Id";
                cmbPermisoSCT.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Carga_ConfigAutotransporte()
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                DataSet dsConfigAutotransporte = new DataSet();
                new CN_CapRemision().ConsultaConfigAutotransporte(sesion, ref dsConfigAutotransporte);

                cmbConfiguracion.DataSource = dsConfigAutotransporte.Tables[0];
                cmbConfiguracion.DataTextField = "Descripcion";
                cmbConfiguracion.DataValueField = "Id";
                cmbConfiguracion.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Carga_TipoRemolque()
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                DataSet dsTipoRemolque = new DataSet();
                new CN_CapRemision().ConsultaTipoRemolque(sesion, ref dsTipoRemolque);

                cmbTipoRemolque.DataSource = dsTipoRemolque.Tables[0];
                cmbTipoRemolque.DataTextField = "Descripcion";
                cmbTipoRemolque.DataValueField = "Id";
                cmbTipoRemolque.DataBind();
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