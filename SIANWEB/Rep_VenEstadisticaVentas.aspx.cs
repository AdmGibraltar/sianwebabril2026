using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using Telerik.Web.UI;
using CapaEntidad;
using CapaDatos;
using CapaNegocios;
using System.IO;
using System.Runtime.Remoting;
using System.Text;
using Telerik.Reporting.Processing;
using ClosedXML.Excel;
using System.Globalization;
using DevExpress.XtraCharts.Native;

namespace SIANWEB
{
    public partial class Rep_VenEstadisticaVentas : System.Web.UI.Page
    {
        #region Variables
        public string strEmp = System.Configuration.ConfigurationManager.AppSettings["VGEmpresa"];
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private int tipo = 0, cliente = 0, producto = 0;
        public int Mov_80 = 0;
        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        Inicializar();

                        Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                        if (sesion != null)
                        {
                            hdnFechaInicio.Value = sesion.CalendarioIni.ToString("MM-yyyy");
                            hdnFechaFin.Value = sesion.CalendarioFin.ToString("MM-yyyy");
                            HiddenModoFecha.Value = "anio";
                        }
                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();

                        int VGEmpresa = 0;
                        Int32.TryParse(strEmp, out VGEmpresa);
                        if (VGEmpresa == Sesion.Id_Emp)
                        {
                            Ambos.Visible = true;
                            TrMes.Visible = true;
                        }
                        else
                        {
                            Ambos.Visible = false;
                            TrMes.Visible = false;
                        }
                    }
                }
                //if (IsPostBack)
                //{
                //    string modo = HiddenModoFecha.Value;

                //    rowAnio.Visible = (modo == "anio");
                //    rowPeriodo.Visible = (modo == "periodo");
                //}
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RadToolBar1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            string accionError = string.Empty;
            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (!string.IsNullOrEmpty(cmbAño.SelectedValue))
                    if (cmbAño.SelectedValue != "-1")
                        switch (btn.CommandName)
                        {
                            case "Mostrar":
                                Mostrar(true);
                                break;
                            case "excel":
                                Mostrar(false);
                                break;
                        }
                    else
                        Alerta("Ingresar un año válido");
                else
                    Alerta("Ingresar un año válido");
            }
            catch (Exception ex)
            {
                Alerta("Error: " + ex.Message);
                //throw ex;
            }
        }

        protected void rbRepresentante_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRepresentante.Checked)
            {
                //txtProducto.Enabled = true;
                Filtro_Producto.Visible = true;
                //txtTerritorio.Enabled = true;
                Filtro_Territorio.Visible = true;
                txtCliente.Text = string.Empty;
                //txtCliente.Enabled = false;
                Filtro_Cliente.Visible = false;
                //cbCliente.Enabled = true;
                //cbProducto.Enabled = true;
                if (!ckbSemanal.Checked)
                {
                    Nivel_Cliente.Visible = true;
                    Nivel_Producto.Visible = true;
                    Nivel.Visible = true;
                }
            }
        }

        protected void rbTerritorio_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTerritorio.Checked && !ckbSemanal.Checked)
            {
                //txtProducto.Enabled = true;
                Filtro_Producto.Visible = true;
                //txtTerritorio.Enabled = true;
                Filtro_Territorio.Visible = true;
                txtCliente.Text = string.Empty;
                //txtCliente.Enabled = false;
                Filtro_Cliente.Visible = false;
                //cbCliente.Enabled = true;
                Nivel_Cliente.Visible = true;
                //cbProducto.Enabled = true;
                Nivel_Producto.Visible = true;
                Nivel.Visible = true;
            }
        }
        protected void rbCliente_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCliente.Checked && !ckbSemanal.Checked)
            {
                //txtCliente.Enabled = true;
                Filtro_Cliente.Visible = true;
                cbCliente.Checked = false;
                //cbCliente.Enabled = false;
                Nivel_Cliente.Visible = false;
                //cbProducto.Enabled = true;
                Nivel_Producto.Visible = true;
                txtTerritorio.Text = string.Empty;
                //txtTerritorio.Enabled = false;
                Filtro_Territorio.Visible = false;
                txtProducto.Text = string.Empty;
                //txtProducto.Enabled = false;
                Filtro_Producto.Visible = false;
                Nivel.Visible = true;

            }
        }
        protected void rbProducto_CheckedChanged(object sender, EventArgs e)
        {
            if (rbProducto.Checked)
            {
                txtTerritorio.Text = string.Empty;
                //txtTerritorio.Enabled = false;
                Filtro_Territorio.Visible = false;
                txtCliente.Text = string.Empty;
                //txtCliente.Enabled = false;
                Filtro_Cliente.Visible = false;
                //txtProducto.Enabled = true;
                Filtro_Producto.Visible = true;
                cbCliente.Checked = false;
                //cbCliente.Enabled = false;
                Nivel_Cliente.Visible = false;
                cbProducto.Checked = false;
                //cbProducto.Enabled = false;
                Nivel_Producto.Visible = false;
                Nivel.Visible = false;
            }
        }
        //ReporteVentasSemana -- Codigo para Reporte semanal de ventas 
        protected void CkbSemanal_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbSemanal.Checked)
            {
                //Ocultamos los filtros para seleccionar nivel
                Nivel.Visible = false;
                Nivel_Cliente.Visible = false;
                Nivel_Producto.Visible = false;
                //Ocultamos los filtros para seleccionar Ordenar por
                rbCliente.Visible = false;
                rbProducto.Visible = false;
                rbRepresentante.Visible = false;
                Ambos.Visible = true;
                rbCliente.Checked = false;
                rbProducto.Checked = false;
                rbRepresentante.Checked = false;
                //Ocultar controles                
                Filtro_Cliente.Visible = false;
                Filtro_Territorio.Visible = false;
                Filtro_Producto.Visible = false;

                ChkMov80.Visible = true;
                rbTerritorio.Checked = true;

            }
            else
            {
                //Mostramos los filtros para seleccionar nivel
                Nivel.Visible = true;
                Nivel_Cliente.Visible = true;
                Nivel_Producto.Visible = true;
                //Mostramos los filtros para seleccionar Ordenar por
                rbCliente.Visible = true;
                rbProducto.Visible = true;
                rbRepresentante.Visible = true;
                Ambos.Visible = false;
                //Mostrar controles                
                Filtro_Territorio.Visible = true;
                Filtro_Producto.Visible = true;

                ChkMov80.Visible = false;



            }
        }
        protected void ChkMov80_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ChkMov80.Checked)
                    Mov_80 = 1;
                else
                    Mov_80 = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void cmbCentrosDist_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
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
            CargarAño();
        }
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {

        }
        #endregion

        #region Funciones
        private void Inicializar()
        {
            try
            {
                CargarCentros();
                CargarAño();
                CargarCategoria();
                ChkMov80.Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarCategoria()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                bool[] claveString = new bool[1];
                claveString[0] = true;

                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatProductoCategoriaCU_Combo", ref cmbCategoria, claveString);
                cmbCategoria.SelectedIndex = 0;
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
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

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
                }
                else
                    Response.Redirect("Inicio.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarAño()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(2, Sesion.Id_Emp, Sesion.Id_Cd_Ver, Sesion.Emp_Cnx, "spCatCalendarioAnhio2_Combo", ref cmbAño);

                if (cmbAño.FindItemIndexByValue(Sesion.CalendarioFin.Year.ToString()) != null)
                {
                    cmbAño.SelectedIndex = cmbAño.FindItemIndexByValue(Sesion.CalendarioFin.Year.ToString());
                }
                else
                {
                    cmbAño.SelectedIndex = cmbAño.Items.Count - 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarAnios()
        {
            //int anio = Convert.ToInt32(DateTime.Now.Year);
            //RadComboBoxItem myItem = new RadComboBoxItem();
            //int i = 0;
            //for (int x = anio; x > anio - 3; x = x - 1)
            //{
            //    cmbAnios.Items.Insert(i, new Telerik.Web.UI.RadComboBoxItem { Text = x.ToString(), Value = x.ToString() });               
            //}
            //  cmbAnios.SelectedIndex = 2;  
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
        private void Mostrar(bool a_pantalla)
        {
            try
            {
                #region Captura de valores
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                string nombreEmpresa = sesion.Emp_Nombre;
                string nombreSucursal = sesion.Cd_Nombre;
                DateTime Fechalocal = DateTime.Now;
                int error = 0;
                int porsemana = 0;
                VenEstadisticaVentas ventas = new VenEstadisticaVentas();
                ventas.Id_Cd = sesion.Id_Cd_Ver;
                //radioButton Filtro-- 1
                if (rbRepresentante.Checked == true)
                {
                    ventas.Filtro = 0;
                    ventas.SFiltro = rbRepresentante.Text;
                }
                if (rbTerritorio.Checked == true)
                {
                    ventas.Filtro = 1;
                    ventas.SFiltro = rbTerritorio.Text;
                }
                if (rbCliente.Checked == true)
                {
                    ventas.Filtro = 2;
                    ventas.SFiltro = rbCliente.Text;
                }
                if (rbProducto.Checked == true)
                {
                    ventas.Filtro = 3;
                    ventas.SFiltro = rbProducto.Text;
                }
                //txtTerritorio
                if (!string.IsNullOrEmpty(txtTerritorio.Text))
                {
                    boton(txtTerritorio.Text, ref error);
                    ventas.Territorio = txtTerritorio.Text;
                    ventas.STerritorio = txtTerritorio.Text;
                }
                else
                    ventas.STerritorio = "Todos";

                //txtClientes
                if (!string.IsNullOrEmpty(txtCliente.Text))
                {
                    boton(txtCliente.Text, ref error);
                    ventas.Cliente = txtCliente.Text;
                    ventas.SCliente = txtCliente.Text;
                }
                else
                    ventas.SCliente = "Todos";

                //txtProducto
                if (!string.IsNullOrEmpty(txtProducto.Text))
                {
                    boton(txtProducto.Text, ref error);
                    ventas.Producto = txtProducto.Text;
                    ventas.SProducto = txtProducto.Text;
                }
                else
                    ventas.SProducto = "Todos";

                ventas.sCategoria = cmbCategoria.Text;
                ventas.categoria = cmbCategoria.SelectedValue;

                //combo Año
                int año = -1;
                int.TryParse(cmbAño.SelectedValue, out año);
                if (año > 0)
                {
                    ventas.Anio = año;
                    ventas.SAnio = cmbAño.SelectedItem.Text;
                }
                //radioButton Mostrar
                if (rbPesos.Checked)
                {// X
                    ventas.Mostrar = 1;
                    ventas.SMostrar = rbPesos.Text;
                }
                if (rbUnidades.Checked)
                {// Y
                    ventas.Mostrar = 2;
                    ventas.SMostrar = rbUnidades.Text;
                }
                if (rbAmbos.Checked)
                {// Y
                    ventas.Mostrar = 3;
                    ventas.SMostrar = rbAmbos.Text;
                }
                //checkBox Cliente   
                ventas.Nivel = 0;
                ventas.Nivel2 = 0;
                if (cbCliente.Checked)
                {//A
                    ventas.Nivel = 1;
                    ventas.SNivel = cbCliente.Text;
                }
                else
                    ventas.SNivel = "Todos";

                //checkBox Producto
                if (cbProducto.Checked)
                {//B
                    ventas.Nivel2 = 1;
                    if (ventas.SNivel == "Todos")
                        ventas.SNivel = cbProducto.Text;
                    else
                        ventas.SNivel += " y " + cbProducto.Text;
                }
                else
                    if (string.IsNullOrEmpty(ventas.SNivel))
                    ventas.SNivel = "Todos";

                //ReporteVentasSemana -- Codigo para Reporte semanal de ventas 
                if (ckbSemanal.Checked)
                {
                    ventas.FiltroSem = 1;
                    ventas.SAnio = cmbAño.Text;
                }

                #endregion
                #region valoresParametros
                #region datos de filtros
                if (ventas.FiltroSem == 1)
                {//Ventas por semana  --4
                    porsemana = 1;
                    if (ventas.Filtro == 0)
                    {//Representante
                        if (ventas.Nivel == 0 && ventas.Nivel2 == 0)
                        {//ni clientes,sin producto -- a - b
                            switch (ventas.Mostrar)
                            {
                                case 1://pesos - x 
                                    ventas.Reporte = 16;
                                    break;
                                case 2://unidades - y 
                                    ventas.Reporte = 17;
                                    break;
                            }


                            ventas.Encabezado = "Núm.";
                            ventas.Encabezado1 = "Representante";
                        }
                        if (ventas.Nivel == 1 && ventas.Nivel2 == 0)
                        {//ni clientes,sin producto -- a - b
                            switch (ventas.Mostrar)
                            {
                                case 1://pesos - x 
                                    ventas.Reporte = 18;
                                    break;
                                case 2://unidades - y 
                                    ventas.Reporte = 19;
                                    break;
                            }


                            ventas.Encabezado = "Núm.";
                            ventas.Encabezado1 = "Cliente";
                        }

                        if (ventas.Nivel == 1 && ventas.Nivel2 == 1)
                        {//ni clientes,sin producto -- a - b
                            switch (ventas.Mostrar)
                            {
                                case 1://pesos - x 
                                    ventas.Reporte = 20;
                                    break;
                                case 2://unidades - y 
                                    ventas.Reporte = 21;
                                    break;
                            }


                            ventas.Encabezado = "Núm.";
                            ventas.Encabezado1 = "Producto";
                        }


                    }
                    if (ventas.Filtro == 1)
                    {//Territorio      -- 1          
                        if (ventas.Nivel == 0 && ventas.Nivel2 == 0)
                        {//ni clientes,sin producto -- a - b
                            switch (ventas.Mostrar)
                            {
                                case 1://pesos - x 
                                    ventas.Reporte = 1;
                                    break;
                                case 2://unidades - y 
                                    ventas.Reporte = 2;
                                    break;
                                case 3: //ambos
                                    ventas.Reporte = 15;
                                    break;
                            }

                            ventas.Encabezado = "Núm.";
                            ventas.Encabezado1 = "Territorio";
                        }
                        if (ventas.Nivel == 1 && ventas.Nivel2 == 0)
                        {//clientes - a
                            switch (ventas.Mostrar)
                            {
                                case 1://pesos - x 
                                    ventas.Reporte = 3;
                                    break;
                                case 2://unidades - y 
                                    ventas.Reporte = 4;
                                    break;
                                case 3: //ambos
                                    ventas.Reporte = 16;
                                    break;
                            }
                            ventas.Encabezado = "Núm.";
                            ventas.Encabezado1 = "Cliente";
                        }

                        if (ventas.Nivel == 0 && ventas.Nivel2 == 1)
                        {//productos - b
                            switch (ventas.Mostrar)
                            {
                                case 1://pesos - x 
                                    ventas.Reporte = 5;
                                    break;
                                case 2://unidades - y 
                                    ventas.Reporte = 6;
                                    break;
                                case 3: //ambos
                                    ventas.Reporte = 17;
                                    break;
                            }
                            ventas.Encabezado = "Núm.";
                            ventas.Encabezado1 = "Producto";
                        }
                        if (ventas.Nivel == 1 && ventas.Nivel2 == 1)
                        {//clientes - a , productos - b
                            switch (ventas.Mostrar)
                            {
                                case 1://pesos - x 
                                    ventas.Reporte = 7;
                                    break;
                                case 2://unidades - y 
                                    ventas.Reporte = 8;
                                    break;
                                case 3: //ambos
                                    ventas.Reporte = 18;
                                    break;
                            }
                            ventas.Encabezado = "Núm.";
                            ventas.Encabezado1 = "Producto";
                        }
                    }

                    if (ventas.Filtro == 2)
                    {//Cliente      -- 2
                        if (ventas.Nivel == 0 && ventas.Nivel2 == 0)
                        {//ni clientes,sin producto -- a - b
                            switch (ventas.Mostrar)
                            {
                                case 1://pesos - x 
                                    ventas.Reporte = 9;
                                    break;
                                case 2://unidades - y 
                                    ventas.Reporte = 10;
                                    break;
                                case 3: //ambos
                                    ventas.Reporte = 19;
                                    break;
                            }
                            ventas.Encabezado = "Núm.";
                            ventas.Encabezado1 = "Cliente";
                        }
                        if (ventas.Nivel == 0 && ventas.Nivel2 == 1)
                        {//productos - b
                            switch (ventas.Mostrar)
                            {
                                case 1://pesos - x 
                                    ventas.Reporte = 11;
                                    break;
                                case 2://unidades - y 
                                    ventas.Reporte = 12;
                                    break;
                                case 3: //ambos
                                    ventas.Reporte = 20;
                                    break;
                            }
                            ventas.Encabezado = "Núm.";
                            ventas.Encabezado1 = "Producto";
                        }
                    }

                    if (ventas.Filtro == 3)
                    {//Productos -- 3
                        if (ventas.Nivel == 0 && ventas.Nivel2 == 0)
                        {//ni clientes,sin producto -- a - b
                            switch (ventas.Mostrar)
                            {
                                case 1://pesos - x 
                                    ventas.Reporte = 13;
                                    break;
                                case 2://unidades - y 
                                    ventas.Reporte = 14;
                                    break;
                                case 3: //ambos
                                    ventas.Reporte = 21;
                                    break;
                            }
                            ventas.Encabezado = "Núm.";
                            ventas.Encabezado1 = "Producto";
                        }
                    }
                }
                else
                {
                    if (ventas.Filtro == 0)
                    {//Representante


                        if (ventas.Nivel == 0 && ventas.Nivel2 == 0)
                        {//ni clientes,sin producto -- a - b
                            switch (ventas.Mostrar)
                            {
                                case 1://pesos - x 
                                    ventas.Reporte = 16;
                                    break;
                                case 2://unidades - y 
                                    ventas.Reporte = 17;
                                    break;
                            }


                            ventas.Encabezado = "Núm.";
                            ventas.Encabezado1 = "Representante";
                        }
                        if (ventas.Nivel == 0 && ventas.Nivel2 == 1)
                        {//ni clientes,sin producto -- a - b
                            switch (ventas.Mostrar)
                            {
                                case 1://pesos - x 
                                    ventas.Reporte = 22;
                                    break;
                                case 2://unidades - y 
                                    ventas.Reporte = 23;
                                    break;
                            }


                            ventas.Encabezado = "Núm.";
                            ventas.Encabezado1 = "Representante";
                        }
                        if (ventas.Nivel == 1 && ventas.Nivel2 == 0)
                        {//ni clientes,sin producto -- a - b
                            switch (ventas.Mostrar)
                            {
                                case 1://pesos - x 
                                    ventas.Reporte = 18;
                                    break;
                                case 2://unidades - y 
                                    ventas.Reporte = 19;
                                    break;
                            }


                            ventas.Encabezado = "Núm.";
                            ventas.Encabezado1 = "Cliente";
                        }

                        if (ventas.Nivel == 1 && ventas.Nivel2 == 1)
                        {//ni clientes,sin producto -- a - b
                            switch (ventas.Mostrar)
                            {
                                case 1://pesos - x 
                                    ventas.Reporte = 20;
                                    break;
                                case 2://unidades - y 
                                    ventas.Reporte = 21;
                                    break;
                            }


                            ventas.Encabezado = "Núm.";
                            ventas.Encabezado1 = "Producto";
                        }


                    }

                    if (ventas.Filtro == 1)
                    {//Territorio      -- 1          
                        if (ventas.Nivel == 0 && ventas.Nivel2 == 0)
                        {//ni clientes,sin producto -- a - b
                            switch (ventas.Mostrar)
                            {
                                case 1://pesos - x 
                                    ventas.Reporte = 1;
                                    break;
                                case 2://unidades - y 
                                    ventas.Reporte = 2;
                                    break;
                                case 3: //ambos
                                    ventas.Reporte = 15;
                                    break;
                            }

                            ventas.Encabezado = "Núm.";
                            ventas.Encabezado1 = "Territorio";
                        }
                        if (ventas.Nivel == 1 && ventas.Nivel2 == 0)
                        {//clientes - a
                            switch (ventas.Mostrar)
                            {
                                case 1://pesos - x 
                                    ventas.Reporte = 3;
                                    break;
                                case 2://unidades - y 
                                    ventas.Reporte = 4;
                                    break;
                                case 3: //ambos
                                    ventas.Reporte = 16;
                                    break;
                            }
                            ventas.Encabezado = "Núm.";
                            ventas.Encabezado1 = "Cliente";
                        }

                        if (ventas.Nivel == 0 && ventas.Nivel2 == 1)
                        {//productos - b
                            switch (ventas.Mostrar)
                            {
                                case 1://pesos - x 
                                    ventas.Reporte = 5;
                                    break;
                                case 2://unidades - y 
                                    ventas.Reporte = 6;
                                    break;
                                case 3: //ambos
                                    ventas.Reporte = 17;
                                    break;
                            }
                            ventas.Encabezado = "Núm.";
                            ventas.Encabezado1 = "Producto";
                        }
                        if (ventas.Nivel == 1 && ventas.Nivel2 == 1)
                        {//clientes - a , productos - b
                            switch (ventas.Mostrar)
                            {
                                case 1://pesos - x 
                                    ventas.Reporte = 7;
                                    break;
                                case 2://unidades - y 
                                    ventas.Reporte = 8;
                                    break;
                                case 3: //ambos
                                    ventas.Reporte = 18;
                                    break;
                            }
                            ventas.Encabezado = "Núm.";
                            ventas.Encabezado1 = "Producto";
                        }
                    }

                    if (ventas.Filtro == 2)
                    {//Cliente      -- 2
                        if (ventas.Nivel == 0 && ventas.Nivel2 == 0)
                        {//ni clientes,sin producto -- a - b
                            switch (ventas.Mostrar)
                            {
                                case 1://pesos - x 
                                    ventas.Reporte = 9;
                                    break;
                                case 2://unidades - y 
                                    ventas.Reporte = 10;
                                    break;
                                case 3: //ambos
                                    ventas.Reporte = 19;
                                    break;
                            }
                            ventas.Encabezado = "Núm.";
                            ventas.Encabezado1 = "Cliente";
                        }
                        if (ventas.Nivel == 0 && ventas.Nivel2 == 1)
                        {//productos - b
                            switch (ventas.Mostrar)
                            {
                                case 1://pesos - x 
                                    ventas.Reporte = 11;
                                    break;
                                case 2://unidades - y 
                                    ventas.Reporte = 12;
                                    break;
                                case 3: //ambos
                                    ventas.Reporte = 20;
                                    break;
                            }
                            ventas.Encabezado = "Núm.";
                            ventas.Encabezado1 = "Producto";
                        }
                    }

                    if (ventas.Filtro == 3)
                    {//Productos -- 3
                        if (ventas.Nivel == 0 && ventas.Nivel2 == 0)
                        {//ni clientes,sin producto -- a - b
                            switch (ventas.Mostrar)
                            {
                                case 1://pesos - x 
                                    ventas.Reporte = 13;
                                    break;
                                case 2://unidades - y 
                                    ventas.Reporte = 14;
                                    break;
                                case 3: //ambos
                                    ventas.Reporte = 21;
                                    break;
                            }
                            ventas.Encabezado = "Núm.";
                            ventas.Encabezado1 = "Producto";
                        }
                    }
                }
                #endregion

                ArrayList ALValorParametrosInternos = new ArrayList();
                ALValorParametrosInternos.Add(ventas.Filtro);
                ALValorParametrosInternos.Add(ventas.SFiltro);
                ALValorParametrosInternos.Add(ventas.Sucursal);
                ALValorParametrosInternos.Add(ventas.SSucursal);
                ALValorParametrosInternos.Add(ventas.Territorio);
                ALValorParametrosInternos.Add(ventas.STerritorio);
                ALValorParametrosInternos.Add(ventas.Cliente);
                ALValorParametrosInternos.Add(ventas.SCliente);
                ALValorParametrosInternos.Add(ventas.Producto);
                ALValorParametrosInternos.Add(ventas.SProducto);
                ALValorParametrosInternos.Add(ventas.Anio);
                ALValorParametrosInternos.Add(ventas.SAnio);
                ALValorParametrosInternos.Add(ventas.Mostrar);
                ALValorParametrosInternos.Add(ventas.SMostrar);
                ALValorParametrosInternos.Add(ventas.Nivel);
                ALValorParametrosInternos.Add(ventas.SNivel);
                ALValorParametrosInternos.Add(ventas.Nivel2);
                ALValorParametrosInternos.Add(ventas.Reporte);
                ALValorParametrosInternos.Add(ventas.Encabezado);
                ALValorParametrosInternos.Add(ventas.Encabezado1);
                ALValorParametrosInternos.Add(sesion.Id_U);
                //parametros para el cuerpo del reporte
                ALValorParametrosInternos.Add(sesion.Id_Emp);
                ALValorParametrosInternos.Add(ventas.Id_Cd);
                ALValorParametrosInternos.Add(sesion.U_Nombre);
                ALValorParametrosInternos.Add(Fechalocal);
                ALValorParametrosInternos.Add(nombreEmpresa);
                ALValorParametrosInternos.Add(nombreSucursal);
                if (!rbAmbos.Checked)
                    ALValorParametrosInternos.Add(strEmp);
                //conexion
                ALValorParametrosInternos.Add(sesion.Emp_Cnx);
                ALValorParametrosInternos.Add(cmbMes.SelectedValue);
                ALValorParametrosInternos.Add(ventas.categoria);
                ALValorParametrosInternos.Add(ventas.sCategoria);

                ventas.id_usu = sesion.Id_U;
                ventas.nombre_Comercial = nombreEmpresa;
                ventas.Sucursal = nombreSucursal;
                #endregion
                Type instance = null;
                if (porsemana == 1)
                {
                    if (a_pantalla)
                        Alerta("No se puede mostrar este reporte, solo se descarga a excel.");
                    else
                        GenerarExcelVentaSemanal(ventas);
                }
                else
                {
                    if (a_pantalla)
                    {
                        if (rbAmbos.Checked)
                        {
                            instance = typeof(LibreriaReportes.Rep_VenEstadisticaVentasAmbos);
                        }
                        else
                        {

                            if (ventas.Filtro == 0)
                            {
                                instance = typeof(LibreriaReportes.Rep_VenEstadisticaVentasRep);
                            }
                            else
                            {
                                // quitar los ultimos deos item a ALValorParametrosInternos para que no se envien los parametros de categoria
                                ALValorParametrosInternos.RemoveAt(ALValorParametrosInternos.Count - 1);
                                ALValorParametrosInternos.RemoveAt(ALValorParametrosInternos.Count - 1);

                                instance = typeof(LibreriaReportes.Rep_VenEstadisticaVentas);
                            }
                        }
                    }
                    else
                    {
                        if (rbAmbos.Checked)
                        {
                            instance = typeof(LibreriaReportes.ExpRep_VenEstadisticaVentasAmbos);
                        }
                        else
                        {
                            GenerarExcelVentaAnuales(ventas);
                            error = 1; // Condicional para que no genere el reporte en telerik
                        }
                    }

                    //NOTA: El estatus de impresión, lo pone cuando el reporte se carga
                    if (error == 0)
                        if (_PermisoImprimir)
                        {
                            if (a_pantalla)
                            {
                                Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = null;
                                Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = ALValorParametrosInternos;
                                Session["assembly" + Session.SessionID + HF_ClvPag.Value] = instance.AssemblyQualifiedName;
                                RAM1.ResponseScripts.Add("AbrirReporteCve('" + HF_ClvPag.Value + "');");

                                /*
                                Session["InternParameter_Values" + Session.SessionID] = ALValorParametrosInternos;
                                Session["assembly" + Session.SessionID] = instance.AssemblyQualifiedName;
                                RAM1.ResponseScripts.Add("AbrirReporte(" + HF_ClvPag.Value + ");");
                                */
                            }
                            else
                            {
                                ImprimirXLS(ALValorParametrosInternos, instance);
                            }
                        }
                        else
                            Alerta("No tiene permiso para imprimir");
                }
            }
            catch (Exception ex)
            {
                Alerta("Error: " + ex.Message);
                //throw ex;
            }
        }
        private void ImprimirXLS(ArrayList ALValorParametrosInternos, Type instance)
        {
            try
            {
                Telerik.Reporting.Report report1 = (Telerik.Reporting.Report)Activator.CreateInstance(instance);
                for (int i = 0; i <= ALValorParametrosInternos.Count - 1; i++)
                {
                    report1.ReportParameters[i].AllowNull = true;
                    report1.ReportParameters[i].Value = ALValorParametrosInternos[i];
                }
                ReportProcessor reportProcessor = new ReportProcessor();
                RenderingResult result = reportProcessor.RenderReport("XLS", report1, null);
                string ruta = Server.MapPath("Reportes") + "\\" + instance.Name + "_" + report1.ReportParameters[1].Value + ".xls";

                if (File.Exists(ruta))
                    File.Delete(ruta);

                FileStream fs = new FileStream(ruta, FileMode.Create);
                fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);

                fs.Flush();
                fs.Close();

                RAM1.ResponseScripts.Add("startDownload('" + instance.Name + "_" + report1.ReportParameters[1].Value + ".xls');");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void boton(string cadena, ref int error)
        {
            if (!string.IsNullOrEmpty(cadena))
            {
                string[] split = cadena.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                string[] split2;
                foreach (string a in split)
                {
                    if (a.Contains("-"))
                    {
                        split2 = a.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                        if (split2.Length != 2)
                        {
                            Alerta("El rango " + a.ToString() + " no es válido");
                            error = 1;
                        }
                        if (split2.Length == 2)
                            if (Convert.ToInt32(split2[0]) > Convert.ToInt32(split2[1]))
                            {
                                Alerta("El rango " + a.ToString() + " no es válido");
                                error = 1;
                            }
                    }
                }
            }
        }

        private List<VentaSemanal> GetList()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<VentaSemanal> lisVentasSem = new List<VentaSemanal>();
                VentaSemanal semanal = new VentaSemanal();

                if (rbPesos.Checked)
                    tipo = 1;
                if (rbUnidades.Checked)
                    tipo = 2;
                if (rbAmbos.Checked)
                    tipo = 0;

                if (ChkMov80.Checked)
                    Mov_80 = 1;
                else
                    Mov_80 = 0;

                new CN_Rep_VenEstadisticaVentas().ConsultaVentaSem_Territorio(semanal, sesion.Emp_Cnx, ref lisVentasSem, sesion.Id_Emp, sesion.Id_Cd_Ver
                                                                              , cmbAño.Text
                                                                              , this.txtTerritorio.Text
                                                                              , this.txtCliente.Text
                                                                              , this.txtProducto.Text
                                                                              , this.cmbCategoria.SelectedValue
                                                                              , tipo
                                                                              , cliente
                                                                              , producto
                                                                              , Mov_80
                                                                              );

                return lisVentasSem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void GenerarExcel()
        {
            try
            {
                StringBuilder tabla = new StringBuilder();
                Funcion fn = new Funcion();
                tabla.Append("<html><head><meta http-equiv='Content-Type' content='text/html; charset=ISO-8859-1'></head><body><table style='width:1200px'>");

                EscribeDetalle(ref tabla);
                tabla.Append("</table></body></html>");
                fn.ExportarExcel("ReporteVentas_Semanal", tabla.ToString());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void EscribeEncabezado(ref StringBuilder Tabla)
        {
            try
            {
                string sclientes = "", sproductos = "";
                if (txtCliente.Text != "")
                    sclientes = txtCliente.Text;
                else
                    sclientes = "Todos";
                if (txtProducto.Text != "")
                    sproductos = txtProducto.Text;
                else
                    sproductos = "Todos";


                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Tabla.Append("<tr>");
                Tabla.Append("<td  colspan='20' style='width:400px; text-align:Left; font-weight:bold'> Reporte de Ventas Semanal  </td>");
                Tabla.Append("</tr>");
                Tabla.Append("<tr>");
                Tabla.Append("<td  colspan='20' style='width:400px; text-align:Left; font-weight:bold'>" + sesion.Cd_Nombre + "</td>");
                Tabla.Append("</tr>");
                Tabla.Append("<tr>");
                Tabla.Append("<td colspan='1'  style='width:40px; text-align:Left;'> Período: </td>");
                Tabla.Append("<td  colspan='20' style='width:300px; text-align:Left; font-weight:bold'> Enero " + cmbAño.Text + " a  Diciembre " + cmbAño.Text + "</td>");
                Tabla.Append("</tr>");
                Tabla.Append("<tr>");
                Tabla.Append("<td  style='width:40px; text-align:Left; '> Cliente: </td>");
                Tabla.Append("<td  colspan='20' style='width:300px; text-align:Left; font-weight:bold'>" + sclientes + "</td>");
                Tabla.Append("</tr>");
                Tabla.Append("<tr>");
                Tabla.Append("<td style='width:40px; text-align:Left; '> Producto: </td>");
                Tabla.Append("<td colspan='20' style='width:300px; text-align:Left; font-weight:bold'> " + sproductos + "</td>");
                Tabla.Append("</tr>");
                Tabla.Append("<tr>");
                Tabla.Append("<td>&nbsp;</td>");
                Tabla.Append("</tr>");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void EscribeDetalle(ref StringBuilder Tabla)
        {
            try
            {
                List<VentaSemanal> List = new List<VentaSemanal>();
                List = GetList();
                DataTable dt = new DataTable();
                dt = Funcion.Convertidor<VentaSemanal>.ListaToDatatable(List);
                EscribeEncabezado(ref Tabla);
                Tabla.Append("<tr>");

                //lectura y escritura de columnas
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "Id_Ter")
                    {
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:70; background: #D3D3D3;'>");
                        Tabla.Append("#Territorio");
                        Tabla.Append("</th>");
                    }

                    if (dt.Columns[i].ColumnName == "Nom_Ter")
                    {
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:200; background: #D3D3D3;'>");
                        Tabla.Append("Territorio");
                        Tabla.Append("</th>");
                    }

                    if (dt.Columns[i].ColumnName == "Id_Cte")
                    {
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:90; background:#D3D3D3;'>");
                        Tabla.Append("Num");
                        Tabla.Append("</th>");
                    }

                    if (dt.Columns[i].ColumnName == "Nom_Cte")
                    {
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:340; background:#D3D3D3;'>");
                        Tabla.Append("Cliente");
                        Tabla.Append("</th>");
                    }

                    if (dt.Columns[i].ColumnName == "Id_prd")
                    {
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:90; background:#D3D3D3;'>");
                        Tabla.Append("Código");
                        Tabla.Append("</th>");
                    }

                    if (dt.Columns[i].ColumnName == "Nom_Prd")
                    {
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:340; background:#D3D3D3;'>");
                        Tabla.Append("Producto");
                        Tabla.Append("</th>");
                    }

                    if (dt.Columns[i].ColumnName == "Unidades" && tipo == 2)
                    {
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:60; background:#D3D3D3; '>");
                        Tabla.Append("Unidades");
                        Tabla.Append("</th>");
                    }

                    if (dt.Columns[i].ColumnName == "Importe" && tipo == 1)
                    {
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:90; background:#D3D3D3; '>");
                        Tabla.Append("Importe");
                        Tabla.Append("</th>");
                    }

                    if (dt.Columns[i].ColumnName == "Unidades" && tipo == 0)
                    {
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:60; background:#D3D3D3; '>");
                        Tabla.Append("Unidades");
                        Tabla.Append("</th>");
                    }

                    if (dt.Columns[i].ColumnName == "Importe" && tipo == 0)
                    {
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:90; background:#D3D3D3; '>");
                        Tabla.Append("Importe");
                        Tabla.Append("</th>");
                    }

                    if (dt.Columns[i].ColumnName == "Anio")
                    {
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width: 60; background:#FF00FF;'>");
                        Tabla.Append("Año");
                        Tabla.Append("</th>");
                    }

                    if (dt.Columns[i].ColumnName == "Semana")
                    {
                        //width = (i == 0) ? "70px" : "90px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width: 60; background:#FF00FF;'>");
                        Tabla.Append("Semana");
                        Tabla.Append("</th>");
                    }
                    if (dt.Columns[i].ColumnName == "Mes")
                    {
                        //width = (i == 0) ? "70px" : "90px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width: 90; background:#FF00FF;'>");
                        Tabla.Append("Mes");
                        Tabla.Append("</th>");
                    }


                }
                Tabla.Append("</tr>");
                // lectura y escritura de filas
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    Tabla.Append("<tr>");
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (dt.Columns[i].ColumnName == "Id_Ter")
                        {
                            Tabla.Append("<td style='text-align:center'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        if (dt.Columns[i].ColumnName == "Nom_Ter")
                        {
                            Tabla.Append("<td style='text-align:center'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        if (dt.Columns[i].ColumnName == "Id_Cte")
                        {
                            Tabla.Append("<td style='text-align:center'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        if (dt.Columns[i].ColumnName == "Nom_Cte")
                        {
                            Tabla.Append("<td style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        if (dt.Columns[i].ColumnName == "Id_prd")
                        {
                            Tabla.Append("<td style='text-align:center'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        if (dt.Columns[i].ColumnName == "Nom_Prd")
                        {
                            Tabla.Append("<td style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }

                        if (dt.Columns[i].ColumnName == "Unidades" && tipo == 2)
                        {
                            Tabla.Append("<td style='text-align:center'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }

                        if (dt.Columns[i].ColumnName == "Importe" && tipo == 1)
                        {
                            Tabla.Append("<td style='text-align:center'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }

                        if (dt.Columns[i].ColumnName == "Unidades" && tipo == 0)
                        {
                            Tabla.Append("<td style='text-align:center'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }

                        if (dt.Columns[i].ColumnName == "Importe" && tipo == 0)
                        {
                            Tabla.Append("<td style='text-align:center'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }


                        if (dt.Columns[i].ColumnName == "Anio")
                        {
                            Tabla.Append("<td style='text-align:center'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }

                        if (dt.Columns[i].ColumnName == "Semana")
                        {
                            Tabla.Append("<td style='text-align:center'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }

                        if (dt.Columns[i].ColumnName == "Mes")
                        {
                            Tabla.Append("<td style='text-align:center'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                    }
                    Tabla.Append("</tr>");
                }

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

        private void GenerarExcelVentaSemanal(VenEstadisticaVentas ventas)
        {
            try
            {

                string strNombreArchivo = "ReporteVentas_" + DateTime.Now.ToString("yymmddhhMM");
                List<VentaSemanal> lstVentaSemanal = new List<VentaSemanal>();
                var wb = new XLWorkbook();

                var ws = wb.Worksheets.Add("Reporte");

                lstVentaSemanal = GetList();

                FormatoReporteSemanal(ventas, lstVentaSemanal, ref ws);

                string path = System.Web.HttpContext.Current.Server.MapPath("Reportes");
                string newFile = "Rep_VentaSemanal_" + ventas.id_usu + ".xlsx";
                string savefile = System.IO.Path.Combine(path, newFile);

                if (File.Exists(savefile))
                {
                    File.Delete(savefile);
                }

                wb.SaveAs(savefile);


                RAM1.ResponseScripts.Add("startDownload('" + newFile + "');");

            }
            catch (Exception ex)
            {
                this.ErrorManager(ex, "GenerarExcelVentaSemanal");
                throw ex;
            }
        }

        private void GenerarExcelVentaAnuales(VenEstadisticaVentas ventas)
        {
            try
            {

                string modoFecha = HiddenModoFecha.Value;
                string fechaInicio1 = hdnFechaInicio.Value;
                string fechaFin2 = hdnFechaFin.Value;
                if (modoFecha == "periodo")
                {
                    if (DateTime.TryParse(fechaInicio1, out var fechaInicio))
                    {
                        ventas.FechaInicio = new DateTime(fechaInicio.Year, fechaInicio.Month, 1);
                    }

                    if (DateTime.TryParse(fechaFin2, out var fechaFin))
                    {
                        // Fin del mes
                        ventas.FechaFin = new DateTime(fechaFin.Year, fechaFin.Month, 1).AddMonths(1).AddDays(-1);
                    }
                }






                List<VenEstadisticaVentas> lstEstVenta = new List<VenEstadisticaVentas>();
                CN_Rep_VenEstadisticaVentas cnEstadisticaVentas = new CN_Rep_VenEstadisticaVentas();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                System.Data.DataTable dataTable = new System.Data.DataTable();
                List<VentaSemanal> lstVentaSemanal = new List<VentaSemanal>();
                var wb = new XLWorkbook();

                var ws = wb.Worksheets.Add("Reporte");

                if (modoFecha == "anio")
                {
                    cnEstadisticaVentas.ConsultaVentaAnual(sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd, ventas, ref dataTable, ref lstEstVenta);
                }
                else
                {
                    cnEstadisticaVentas.ConsultaVentaPeriodo(sesion.Emp_Cnx, sesion.Id_Emp, sesion.Id_Cd, ventas, ref dataTable, ref lstEstVenta);

                }
                if (modoFecha == "anio")
                {
                    FormatoReporteAnual(ventas, sesion.U_Nombre, lstEstVenta, ref ws);
                }
                else
                {
                    FormatoReportePeriodo(ventas, sesion.U_Nombre, lstEstVenta, ref ws);

                }

                //FormatoReporteAnual(ventas, sesion.U_Nombre, lstEstVenta, ref ws);

                string path = System.Web.HttpContext.Current.Server.MapPath("Reportes");
                string newFile = "Rep_VentaAnual_" + ventas.id_usu + ".xlsx";
                string savefile = System.IO.Path.Combine(path, newFile);

                if (File.Exists(savefile))
                {
                    File.Delete(savefile);
                }

                wb.SaveAs(savefile);
                RAM1.ResponseScripts.Add("startDownload('" + newFile + "');");
            }
            catch (Exception ex)
            {
                this.ErrorManager(ex, "GenerarExcelVentaAnuales");
                throw ex;
            }
        }

        // 
        private void FormatoReporteAnual(VenEstadisticaVentas entFiltro, string strUsuario, List<VenEstadisticaVentas> lstEstVenta, ref IXLWorksheet wss)
        {
            #region Encabezado

            string strUltimaColumna = string.Empty;
            string strDescripcionFiltro1 = string.Empty;
            string strValorFiltro1 = string.Empty;
            string strDescripcionFiltro2 = string.Empty;
            string strValorFiltro2 = string.Empty;

            string modoFecha = HiddenModoFecha.Value;
            //string fechaInicio1 = hdnFechaInicio.Value;
            //string fechaFin2 = hdnFechaFin.Value;

            wss.Style.Font.FontSize = 8;
            wss.Range("F1:I1").Merge();
            wss.Cell("F1").SetValue(entFiltro.nombre_Comercial);
            wss.Cell("F1").Style.Font.Bold = true;
            wss.Range("F2:I2").Merge();
            wss.Cell("F2").SetValue(entFiltro.Sucursal);
            wss.Range("F3:I3").Merge();
            wss.Cell("F3").SetValue("Estadística de ventas");
            wss.Range("F1:F3").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            wss.Range("F1:F3").Style.Font.FontSize = 11;

            wss.Range("M1:O1").Merge();
            wss.Cell("M1").SetValue(strUsuario);
            wss.Range("M2:O2").Merge();
            wss.Cell("M2").SetValue(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            wss.Range("M1:M2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            wss.Range("A1:B1").Merge();
            wss.Cell("A1").SetValue("Order por:");
            wss.Cell("C1").SetValue(entFiltro.SFiltro);
            wss.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            wss.Range("A2:B2").Merge();
            wss.Cell("A2").SetValue("Mostrar en:");
            wss.Cell("C2").SetValue(entFiltro.SMostrar);
            wss.Cell("A2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            wss.Range("A5:B5").Merge();
            wss.Cell("A5").SetValue("Producto:");
            wss.Cell("C5").SetValue(entFiltro.SProducto);
            wss.Cell("A5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            wss.Range("A6:B6").Merge();
            wss.Cell("A6").SetValue("Categoría:");
            wss.Cell("C6").SetValue(entFiltro.sCategoria);
            wss.Cell("A6").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            wss.Range("A7:B7").Merge();
            wss.Cell("A7").SetValue("Año:");
            wss.Cell("C7").SetValue(entFiltro.Anio.ToString());

            wss.Cell("A7").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            wss.Cell("C7").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            wss.Range("A1:A7").Style.Font.Bold = true;




            wss.Range("A3:B3").Merge();
            wss.Cell("A3").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            wss.Range("A4:B4").Merge();
            wss.Cell("A4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            strDescripcionFiltro1 = "Nivel:";
            strValorFiltro1 = entFiltro.SNivel;
            strDescripcionFiltro2 = "Territorio:";
            strValorFiltro2 = entFiltro.STerritorio;

            switch (entFiltro.Reporte)
            {
                case 1:
                case 2:
                    wss.Cell("A9").Value = "Num. Ter ";
                    wss.Cell("B9").Value = "Territorio";
                    wss.Cell("C9").Value = "Enero";
                    wss.Cell("D9").Value = "Febrero";
                    wss.Cell("E9").Value = "Marzo";
                    wss.Cell("F9").Value = "Abril";
                    wss.Cell("G9").Value = "Mayo";
                    wss.Cell("H9").Value = "Junio";
                    wss.Cell("I9").Value = "Julio";
                    wss.Cell("J9").Value = "Agosto";
                    wss.Cell("K9").Value = "Septiembre";
                    wss.Cell("L9").Value = "Octubre";
                    wss.Cell("M9").Value = "Noviembre";
                    wss.Cell("N9").Value = "Diciembre";
                    wss.Cell("O9").Value = "Total";
                    strUltimaColumna = "O";
                    wss.Column("B").Width = 25;

                    break;
                case 3:
                case 4:
                    wss.Cell("A9").Value = "Num. Ter ";
                    wss.Cell("B9").Value = "Territorio";
                    wss.Cell("C9").Value = "Num. Cte";
                    wss.Cell("D9").Value = "Cliente";
                    wss.Cell("E9").Value = "Enero";
                    wss.Cell("F9").Value = "Febrero";
                    wss.Cell("G9").Value = "Marzo";
                    wss.Cell("H9").Value = "Abril";
                    wss.Cell("I9").Value = "Mayo";
                    wss.Cell("J9").Value = "Junio";
                    wss.Cell("K9").Value = "Julio";
                    wss.Cell("L9").Value = "Agosto";
                    wss.Cell("M9").Value = "Septiembre";
                    wss.Cell("N9").Value = "Octubre";
                    wss.Cell("O9").Value = "Noviembre";
                    wss.Cell("P9").Value = "Diciembre";
                    wss.Cell("Q9").Value = "Total";
                    strUltimaColumna = "Q";
                    wss.Column("B").Width = 25;
                    wss.Column("D").Width = 35;
                    break;
                case 5:
                case 6:
                    wss.Cell("A9").Value = "Num. Ter ";
                    wss.Cell("B9").Value = "Territorio";
                    wss.Cell("C9").Value = "Código";
                    wss.Cell("D9").Value = "Producto";
                    wss.Cell("E9").Value = "Enero";
                    wss.Cell("F9").Value = "Febrero";
                    wss.Cell("G9").Value = "Marzo";
                    wss.Cell("H9").Value = "Abril";
                    wss.Cell("I9").Value = "Mayo";
                    wss.Cell("J9").Value = "Junio";
                    wss.Cell("K9").Value = "Julio";
                    wss.Cell("L9").Value = "Agosto";
                    wss.Cell("M9").Value = "Septiembre";
                    wss.Cell("N9").Value = "Octubre";
                    wss.Cell("O9").Value = "Noviembre";
                    wss.Cell("P9").Value = "Diciembre";
                    wss.Cell("Q9").Value = "Total";
                    strUltimaColumna = "Q";
                    wss.Column("B").Width = 25;
                    wss.Column("D").Width = 35;
                    break;
                case 7:
                case 8:
                    wss.Cell("A9").Value = "Num. Ter ";
                    wss.Cell("B9").Value = "Territorio";
                    wss.Cell("C9").Value = "Num. Cte";
                    wss.Cell("D9").Value = "Cliente";
                    wss.Cell("E9").Value = "Código";
                    wss.Cell("F9").Value = "Producto";
                    wss.Cell("G9").Value = "Enero";
                    wss.Cell("H9").Value = "Febrero";
                    wss.Cell("I9").Value = "Marzo";
                    wss.Cell("J9").Value = "Abril";
                    wss.Cell("K9").Value = "Mayo";
                    wss.Cell("L9").Value = "Junio";
                    wss.Cell("M9").Value = "Julio";
                    wss.Cell("N9").Value = "Agosto";
                    wss.Cell("O9").Value = "Septiembre";
                    wss.Cell("P9").Value = "Octubre";
                    wss.Cell("Q9").Value = "Noviembre";
                    wss.Cell("R9").Value = "Diciembre";
                    wss.Cell("S9").Value = "Total";
                    strUltimaColumna = "S";
                    wss.Column("B").Width = 25;
                    wss.Column("D").Width = 35;
                    wss.Column("F").Width = 45;
                    break;
                case 9:
                case 10:
                    wss.Cell("A9").Value = "Num. Cte";
                    wss.Cell("B9").Value = "Cliente";
                    wss.Cell("C9").Value = "Enero";
                    wss.Cell("D9").Value = "Febrero";
                    wss.Cell("E9").Value = "Marzo";
                    wss.Cell("F9").Value = "Abril";
                    wss.Cell("G9").Value = "Mayo";
                    wss.Cell("H9").Value = "Junio";
                    wss.Cell("I9").Value = "Julio";
                    wss.Cell("J9").Value = "Agosto";
                    wss.Cell("K9").Value = "Septiembre";
                    wss.Cell("L9").Value = "Octubre";
                    wss.Cell("M9").Value = "Noviembre";
                    wss.Cell("N9").Value = "Diciembre";
                    wss.Cell("O9").Value = "Total";
                    strUltimaColumna = "O";
                    wss.Column("B").Width = 25;
                    strDescripcionFiltro2 = "Cliente:";
                    strValorFiltro2 = entFiltro.SCliente;
                    break;
                case 11:
                case 12:
                    wss.Cell("A9").Value = "Num. Cte ";
                    wss.Cell("B9").Value = "Cliente";
                    wss.Cell("C9").Value = "Código";
                    wss.Cell("D9").Value = "Producto";
                    wss.Cell("E9").Value = "Enero";
                    wss.Cell("F9").Value = "Febrero";
                    wss.Cell("G9").Value = "Marzo";
                    wss.Cell("H9").Value = "Abril";
                    wss.Cell("I9").Value = "Mayo";
                    wss.Cell("J9").Value = "Junio";
                    wss.Cell("K9").Value = "Julio";
                    wss.Cell("L9").Value = "Agosto";
                    wss.Cell("M9").Value = "Septiembre";
                    wss.Cell("N9").Value = "Octubre";
                    wss.Cell("O9").Value = "Noviembre";
                    wss.Cell("P9").Value = "Diciembre";
                    wss.Cell("Q9").Value = "Total";
                    strUltimaColumna = "Q";
                    wss.Column("B").Width = 25;
                    wss.Column("D").Width = 35;
                    strDescripcionFiltro2 = "Cliente:";
                    strValorFiltro2 = entFiltro.SCliente;
                    break;
                case 13:
                case 14:
                    wss.Cell("A9").Value = "Código ";
                    wss.Cell("B9").Value = "Producto";
                    wss.Cell("C9").Value = "Enero";
                    wss.Cell("D9").Value = "Febrero";
                    wss.Cell("E9").Value = "Marzo";
                    wss.Cell("F9").Value = "Abril";
                    wss.Cell("G9").Value = "Mayo";
                    wss.Cell("H9").Value = "Junio";
                    wss.Cell("I9").Value = "Julio";
                    wss.Cell("J9").Value = "Agosto";
                    wss.Cell("K9").Value = "Septiembre";
                    wss.Cell("L9").Value = "Octubre";
                    wss.Cell("M9").Value = "Noviembre";
                    wss.Cell("N9").Value = "Diciembre";
                    wss.Cell("O9").Value = "Total";
                    strUltimaColumna = "O";
                    wss.Column("B").Width = 25;
                    strDescripcionFiltro1 = string.Empty;
                    strValorFiltro1 = string.Empty;
                    strDescripcionFiltro2 = string.Empty;
                    strValorFiltro2 = string.Empty;
                    break;
                case 16:
                case 17:
                    wss.Cell("A9").Value = "Num. Rik ";
                    wss.Cell("B9").Value = "Representante";
                    wss.Cell("C9").Value = "Enero";
                    wss.Cell("D9").Value = "Febrero";
                    wss.Cell("E9").Value = "Marzo";
                    wss.Cell("F9").Value = "Abril";
                    wss.Cell("G9").Value = "Mayo";
                    wss.Cell("H9").Value = "Junio";
                    wss.Cell("I9").Value = "Julio";
                    wss.Cell("J9").Value = "Agosto";
                    wss.Cell("K9").Value = "Septiembre";
                    wss.Cell("L9").Value = "Octubre";
                    wss.Cell("M9").Value = "Noviembre";
                    wss.Cell("N9").Value = "Diciembre";
                    wss.Cell("O9").Value = "Total";
                    strUltimaColumna = "O";
                    wss.Column("B").Width = 25;
                    break;
                case 18:
                case 19:
                    wss.Cell("A9").Value = "Num. Rik ";
                    wss.Cell("B9").Value = "Representante";
                    wss.Cell("C9").Value = "Num. Cte";
                    wss.Cell("D9").Value = "Cliente";
                    wss.Cell("E9").Value = "Enero";
                    wss.Cell("F9").Value = "Febrero";
                    wss.Cell("G9").Value = "Marzo";
                    wss.Cell("H9").Value = "Abril";
                    wss.Cell("I9").Value = "Mayo";
                    wss.Cell("J9").Value = "Junio";
                    wss.Cell("K9").Value = "Julio";
                    wss.Cell("L9").Value = "Agosto";
                    wss.Cell("M9").Value = "Septiembre";
                    wss.Cell("N9").Value = "Octubre";
                    wss.Cell("O9").Value = "Noviembre";
                    wss.Cell("P9").Value = "Diciembre";
                    wss.Cell("Q9").Value = "Total";
                    strUltimaColumna = "Q";
                    wss.Column("B").Width = 25;
                    wss.Column("D").Width = 35;
                    break;
                case 20:
                case 21:
                    wss.Cell("A9").Value = "Num. Rep ";
                    wss.Cell("B9").Value = "Representante";
                    wss.Cell("C9").Value = "Num. Cte";
                    wss.Cell("D9").Value = "Cliente";
                    wss.Cell("E9").Value = "Código";
                    wss.Cell("F9").Value = "Producto";
                    wss.Cell("G9").Value = "Enero";
                    wss.Cell("H9").Value = "Febrero";
                    wss.Cell("I9").Value = "Marzo";
                    wss.Cell("J9").Value = "Abril";
                    wss.Cell("K9").Value = "Mayo";
                    wss.Cell("L9").Value = "Junio";
                    wss.Cell("M9").Value = "Julio";
                    wss.Cell("N9").Value = "Agosto";
                    wss.Cell("O9").Value = "Septiembre";
                    wss.Cell("P9").Value = "Octubre";
                    wss.Cell("Q9").Value = "Noviembre";
                    wss.Cell("R9").Value = "Diciembre";
                    wss.Cell("S9").Value = "Total";
                    strUltimaColumna = "S";
                    wss.Column("B").Width = 25;
                    wss.Column("D").Width = 35;
                    wss.Column("F").Width = 45;
                    break;
                case 22:
                case 23:
                    wss.Cell("A9").Value = "Num. Rik ";
                    wss.Cell("B9").Value = "Representante";
                    wss.Cell("C9").Value = "Código";
                    wss.Cell("D9").Value = "Producto";
                    wss.Cell("E9").Value = "Enero";
                    wss.Cell("F9").Value = "Febrero";
                    wss.Cell("G9").Value = "Marzo";
                    wss.Cell("H9").Value = "Abril";
                    wss.Cell("I9").Value = "Mayo";
                    wss.Cell("J9").Value = "Junio";
                    wss.Cell("K9").Value = "Julio";
                    wss.Cell("L9").Value = "Agosto";
                    wss.Cell("M9").Value = "Septiembre";
                    wss.Cell("N9").Value = "Octubre";
                    wss.Cell("O9").Value = "Noviembre";
                    wss.Cell("P9").Value = "Diciembre";
                    wss.Cell("Q9").Value = "Total";
                    strUltimaColumna = "Q";
                    wss.Column("B").Width = 25;
                    wss.Column("D").Width = 35;
                    break;
                default:
                    strUltimaColumna = "S";
                    break;
            }

            wss.Range("A9:" + strUltimaColumna + "9").Style.Font.Bold = true;
            wss.Range("A9:" + strUltimaColumna + "9").Style.Font.FontColor = XLColor.White;
            wss.Range("A9:" + strUltimaColumna + "9").Style.Fill.BackgroundColor = XLColor.SteelBlue;

            wss.Range("A9:" + strUltimaColumna + "9").SetAutoFilter();


            if (!string.IsNullOrEmpty(strDescripcionFiltro1))
            {
                wss.Cell("A3").SetValue(strDescripcionFiltro1);
                wss.Cell("C3").SetValue(strValorFiltro1);
            }

            if (!string.IsNullOrEmpty(strDescripcionFiltro2))
            {
                wss.Cell("A4").SetValue(strDescripcionFiltro2);
                wss.Cell("C4").SetValue(strValorFiltro2);
            }





            #endregion

            #region Datos

            int intTotalFilas = lstEstVenta.Count;
            int intFilaInicial = 10;

            string strFilaInicial = intFilaInicial.ToString();
            int j = intFilaInicial;
            string strCeldas = string.Empty;

            for (int i = 0; i < intTotalFilas; i++, j++)
            {

                switch (entFiltro.Reporte)
                {
                    case 1:
                    case 2:
                        wss.Cell("A" + j).Value = lstEstVenta[i].id_ter;
                        wss.Cell("B" + j).Value = lstEstVenta[i].nombre_terr;
                        wss.Cell("C" + j).Value = lstEstVenta[i].mes1;
                        wss.Cell("D" + j).Value = lstEstVenta[i].mes2;
                        wss.Cell("E" + j).Value = lstEstVenta[i].mes3;
                        wss.Cell("F" + j).Value = lstEstVenta[i].mes4;
                        wss.Cell("G" + j).Value = lstEstVenta[i].mes5;
                        wss.Cell("H" + j).Value = lstEstVenta[i].mes6;
                        wss.Cell("I" + j).Value = lstEstVenta[i].mes7;
                        wss.Cell("J" + j).Value = lstEstVenta[i].mes8;
                        wss.Cell("K" + j).Value = lstEstVenta[i].mes9;
                        wss.Cell("L" + j).Value = lstEstVenta[i].mes10;
                        wss.Cell("M" + j).Value = lstEstVenta[i].mes11;
                        wss.Cell("N" + j).Value = lstEstVenta[i].mes12;
                        wss.Cell("O" + j).Value = lstEstVenta[i].total;
                        break;
                    case 3:
                    case 4:
                        wss.Cell("A" + j).Value = lstEstVenta[i].id_ter;
                        wss.Cell("B" + j).Value = lstEstVenta[i].nombre_terr;
                        wss.Cell("C" + j).Value = lstEstVenta[i].id_cte;
                        wss.Cell("D" + j).Value = lstEstVenta[i].Cliente;
                        wss.Cell("E" + j).Value = lstEstVenta[i].mes1;
                        wss.Cell("F" + j).Value = lstEstVenta[i].mes2;
                        wss.Cell("G" + j).Value = lstEstVenta[i].mes3;
                        wss.Cell("H" + j).Value = lstEstVenta[i].mes4;
                        wss.Cell("I" + j).Value = lstEstVenta[i].mes5;
                        wss.Cell("J" + j).Value = lstEstVenta[i].mes6;
                        wss.Cell("K" + j).Value = lstEstVenta[i].mes7;
                        wss.Cell("L" + j).Value = lstEstVenta[i].mes8;
                        wss.Cell("M" + j).Value = lstEstVenta[i].mes9;
                        wss.Cell("N" + j).Value = lstEstVenta[i].mes10;
                        wss.Cell("O" + j).Value = lstEstVenta[i].mes11;
                        wss.Cell("P" + j).Value = lstEstVenta[i].mes12;
                        wss.Cell("Q" + j).Value = lstEstVenta[i].total;
                        break;
                    case 5:
                    case 6:
                        wss.Cell("A" + j).Value = lstEstVenta[i].id_ter;
                        wss.Cell("B" + j).Value = lstEstVenta[i].nombre_terr;
                        wss.Cell("C" + j).Value = lstEstVenta[i].id_prd;
                        wss.Cell("D" + j).Value = lstEstVenta[i].Producto;
                        wss.Cell("E" + j).Value = lstEstVenta[i].mes1;
                        wss.Cell("F" + j).Value = lstEstVenta[i].mes2;
                        wss.Cell("G" + j).Value = lstEstVenta[i].mes3;
                        wss.Cell("H" + j).Value = lstEstVenta[i].mes4;
                        wss.Cell("I" + j).Value = lstEstVenta[i].mes5;
                        wss.Cell("J" + j).Value = lstEstVenta[i].mes6;
                        wss.Cell("K" + j).Value = lstEstVenta[i].mes7;
                        wss.Cell("L" + j).Value = lstEstVenta[i].mes8;
                        wss.Cell("M" + j).Value = lstEstVenta[i].mes9;
                        wss.Cell("N" + j).Value = lstEstVenta[i].mes10;
                        wss.Cell("O" + j).Value = lstEstVenta[i].mes11;
                        wss.Cell("P" + j).Value = lstEstVenta[i].mes12;
                        wss.Cell("Q" + j).Value = lstEstVenta[i].total;
                        break;
                    case 7:
                    case 8:
                        wss.Cell("A" + j).Value = lstEstVenta[i].id_ter;
                        wss.Cell("B" + j).Value = lstEstVenta[i].nombre_terr;
                        wss.Cell("C" + j).Value = lstEstVenta[i].id_cte;
                        wss.Cell("D" + j).Value = lstEstVenta[i].Cliente;
                        wss.Cell("E" + j).Value = lstEstVenta[i].id_prd;
                        wss.Cell("F" + j).Value = lstEstVenta[i].Producto;
                        wss.Cell("G" + j).Value = lstEstVenta[i].mes1;
                        wss.Cell("H" + j).Value = lstEstVenta[i].mes2;
                        wss.Cell("I" + j).Value = lstEstVenta[i].mes3;
                        wss.Cell("J" + j).Value = lstEstVenta[i].mes4;
                        wss.Cell("K" + j).Value = lstEstVenta[i].mes5;
                        wss.Cell("L" + j).Value = lstEstVenta[i].mes6;
                        wss.Cell("M" + j).Value = lstEstVenta[i].mes7;
                        wss.Cell("N" + j).Value = lstEstVenta[i].mes8;
                        wss.Cell("O" + j).Value = lstEstVenta[i].mes9;
                        wss.Cell("P" + j).Value = lstEstVenta[i].mes10;
                        wss.Cell("Q" + j).Value = lstEstVenta[i].mes11;
                        wss.Cell("R" + j).Value = lstEstVenta[i].mes12;
                        wss.Cell("S" + j).Value = lstEstVenta[i].total;
                        break;
                    case 9:
                    case 10:
                        wss.Cell("A" + j).Value = lstEstVenta[i].id_cte;
                        wss.Cell("B" + j).Value = lstEstVenta[i].Cliente;
                        wss.Cell("C" + j).Value = lstEstVenta[i].mes1;
                        wss.Cell("D" + j).Value = lstEstVenta[i].mes2;
                        wss.Cell("E" + j).Value = lstEstVenta[i].mes3;
                        wss.Cell("F" + j).Value = lstEstVenta[i].mes4;
                        wss.Cell("G" + j).Value = lstEstVenta[i].mes5;
                        wss.Cell("H" + j).Value = lstEstVenta[i].mes6;
                        wss.Cell("I" + j).Value = lstEstVenta[i].mes7;
                        wss.Cell("J" + j).Value = lstEstVenta[i].mes8;
                        wss.Cell("K" + j).Value = lstEstVenta[i].mes9;
                        wss.Cell("L" + j).Value = lstEstVenta[i].mes10;
                        wss.Cell("M" + j).Value = lstEstVenta[i].mes11;
                        wss.Cell("N" + j).Value = lstEstVenta[i].mes12;
                        wss.Cell("O" + j).Value = lstEstVenta[i].total;
                        break;
                    case 11:
                    case 12:
                        wss.Cell("A" + j).Value = lstEstVenta[i].id_cte;
                        wss.Cell("B" + j).Value = lstEstVenta[i].Cliente;
                        wss.Cell("C" + j).Value = lstEstVenta[i].id_prd;
                        wss.Cell("D" + j).Value = lstEstVenta[i].Producto;
                        wss.Cell("E" + j).Value = lstEstVenta[i].mes1;
                        wss.Cell("F" + j).Value = lstEstVenta[i].mes2;
                        wss.Cell("G" + j).Value = lstEstVenta[i].mes3;
                        wss.Cell("H" + j).Value = lstEstVenta[i].mes4;
                        wss.Cell("I" + j).Value = lstEstVenta[i].mes5;
                        wss.Cell("J" + j).Value = lstEstVenta[i].mes6;
                        wss.Cell("K" + j).Value = lstEstVenta[i].mes7;
                        wss.Cell("L" + j).Value = lstEstVenta[i].mes8;
                        wss.Cell("M" + j).Value = lstEstVenta[i].mes9;
                        wss.Cell("N" + j).Value = lstEstVenta[i].mes10;
                        wss.Cell("O" + j).Value = lstEstVenta[i].mes11;
                        wss.Cell("P" + j).Value = lstEstVenta[i].mes12;
                        wss.Cell("Q" + j).Value = lstEstVenta[i].total;

                        break;
                    case 13:
                    case 14:
                        wss.Cell("A" + j).Value = lstEstVenta[i].id_prd;
                        wss.Cell("B" + j).Value = lstEstVenta[i].Producto;
                        wss.Cell("C" + j).Value = lstEstVenta[i].mes1;
                        wss.Cell("D" + j).Value = lstEstVenta[i].mes2;
                        wss.Cell("E" + j).Value = lstEstVenta[i].mes3;
                        wss.Cell("F" + j).Value = lstEstVenta[i].mes4;
                        wss.Cell("G" + j).Value = lstEstVenta[i].mes5;
                        wss.Cell("H" + j).Value = lstEstVenta[i].mes6;
                        wss.Cell("I" + j).Value = lstEstVenta[i].mes7;
                        wss.Cell("J" + j).Value = lstEstVenta[i].mes8;
                        wss.Cell("K" + j).Value = lstEstVenta[i].mes9;
                        wss.Cell("L" + j).Value = lstEstVenta[i].mes10;
                        wss.Cell("M" + j).Value = lstEstVenta[i].mes11;
                        wss.Cell("N" + j).Value = lstEstVenta[i].mes12;
                        wss.Cell("O" + j).Value = lstEstVenta[i].total;

                        break;
                    case 16:
                    case 17:
                        wss.Cell("A" + j).Value = lstEstVenta[i].id_rik;
                        wss.Cell("B" + j).Value = lstEstVenta[i].nombre_rik;
                        wss.Cell("C" + j).Value = lstEstVenta[i].mes1;
                        wss.Cell("D" + j).Value = lstEstVenta[i].mes2;
                        wss.Cell("E" + j).Value = lstEstVenta[i].mes3;
                        wss.Cell("F" + j).Value = lstEstVenta[i].mes4;
                        wss.Cell("G" + j).Value = lstEstVenta[i].mes5;
                        wss.Cell("H" + j).Value = lstEstVenta[i].mes6;
                        wss.Cell("I" + j).Value = lstEstVenta[i].mes7;
                        wss.Cell("J" + j).Value = lstEstVenta[i].mes8;
                        wss.Cell("K" + j).Value = lstEstVenta[i].mes9;
                        wss.Cell("L" + j).Value = lstEstVenta[i].mes10;
                        wss.Cell("M" + j).Value = lstEstVenta[i].mes11;
                        wss.Cell("N" + j).Value = lstEstVenta[i].mes12;
                        wss.Cell("O" + j).Value = lstEstVenta[i].total;

                        break;
                    case 18:
                    case 19:
                        wss.Cell("A" + j).Value = lstEstVenta[i].id_rik;
                        wss.Cell("B" + j).Value = lstEstVenta[i].nombre_rik;
                        wss.Cell("C" + j).Value = lstEstVenta[i].id_cte;
                        wss.Cell("D" + j).Value = lstEstVenta[i].Cliente;
                        wss.Cell("E" + j).Value = lstEstVenta[i].mes1;
                        wss.Cell("F" + j).Value = lstEstVenta[i].mes2;
                        wss.Cell("G" + j).Value = lstEstVenta[i].mes3;
                        wss.Cell("H" + j).Value = lstEstVenta[i].mes4;
                        wss.Cell("I" + j).Value = lstEstVenta[i].mes5;
                        wss.Cell("J" + j).Value = lstEstVenta[i].mes6;
                        wss.Cell("K" + j).Value = lstEstVenta[i].mes7;
                        wss.Cell("L" + j).Value = lstEstVenta[i].mes8;
                        wss.Cell("M" + j).Value = lstEstVenta[i].mes9;
                        wss.Cell("N" + j).Value = lstEstVenta[i].mes10;
                        wss.Cell("O" + j).Value = lstEstVenta[i].mes11;
                        wss.Cell("P" + j).Value = lstEstVenta[i].mes12;
                        wss.Cell("Q" + j).Value = lstEstVenta[i].total;

                        break;
                    case 20:
                    case 21:
                        wss.Cell("A" + j).Value = lstEstVenta[i].id_rik;
                        wss.Cell("B" + j).Value = lstEstVenta[i].nombre_rik;
                        wss.Cell("C" + j).Value = lstEstVenta[i].id_cte;
                        wss.Cell("D" + j).Value = lstEstVenta[i].Cliente;
                        wss.Cell("E" + j).Value = lstEstVenta[i].id_prd;
                        wss.Cell("F" + j).Value = lstEstVenta[i].Producto;
                        wss.Cell("G" + j).Value = lstEstVenta[i].mes1;
                        wss.Cell("H" + j).Value = lstEstVenta[i].mes2;
                        wss.Cell("I" + j).Value = lstEstVenta[i].mes3;
                        wss.Cell("J" + j).Value = lstEstVenta[i].mes4;
                        wss.Cell("K" + j).Value = lstEstVenta[i].mes5;
                        wss.Cell("L" + j).Value = lstEstVenta[i].mes6;
                        wss.Cell("M" + j).Value = lstEstVenta[i].mes7;
                        wss.Cell("N" + j).Value = lstEstVenta[i].mes8;
                        wss.Cell("O" + j).Value = lstEstVenta[i].mes9;
                        wss.Cell("P" + j).Value = lstEstVenta[i].mes10;
                        wss.Cell("Q" + j).Value = lstEstVenta[i].mes11;
                        wss.Cell("R" + j).Value = lstEstVenta[i].mes12;
                        wss.Cell("S" + j).Value = lstEstVenta[i].total;
                        break;
                    case 22:
                    case 23:
                        wss.Cell("A" + j).Value = lstEstVenta[i].id_rik;
                        wss.Cell("B" + j).Value = lstEstVenta[i].nombre_rik;
                        wss.Cell("C" + j).Value = lstEstVenta[i].id_prd;
                        wss.Cell("D" + j).Value = lstEstVenta[i].Producto;
                        wss.Cell("E" + j).Value = lstEstVenta[i].mes1;
                        wss.Cell("F" + j).Value = lstEstVenta[i].mes2;
                        wss.Cell("G" + j).Value = lstEstVenta[i].mes3;
                        wss.Cell("H" + j).Value = lstEstVenta[i].mes4;
                        wss.Cell("I" + j).Value = lstEstVenta[i].mes5;
                        wss.Cell("J" + j).Value = lstEstVenta[i].mes6;
                        wss.Cell("K" + j).Value = lstEstVenta[i].mes7;
                        wss.Cell("L" + j).Value = lstEstVenta[i].mes8;
                        wss.Cell("M" + j).Value = lstEstVenta[i].mes9;
                        wss.Cell("N" + j).Value = lstEstVenta[i].mes10;
                        wss.Cell("O" + j).Value = lstEstVenta[i].mes11;
                        wss.Cell("P" + j).Value = lstEstVenta[i].mes12;
                        wss.Cell("Q" + j).Value = lstEstVenta[i].total;

                        break;
                    default:

                        break;
                }

            }

            string strUltimaFila = j.ToString();
            string strUltimaFilaFormula = (j - 1).ToString();

            wss.Cell("G" + strUltimaFila).FormulaA1 = "=SUM(G" + strFilaInicial + ":G" + strUltimaFilaFormula + ")";
            wss.Cell("H" + strUltimaFila).FormulaA1 = "=SUM(H" + strFilaInicial + ":H" + strUltimaFilaFormula + ")";
            wss.Cell("I" + strUltimaFila).FormulaA1 = "=SUM(I" + strFilaInicial + ":I" + strUltimaFilaFormula + ")";
            wss.Cell("J" + strUltimaFila).FormulaA1 = "=SUM(J" + strFilaInicial + ":J" + strUltimaFilaFormula + ")";
            wss.Cell("K" + strUltimaFila).FormulaA1 = "=SUM(K" + strFilaInicial + ":K" + strUltimaFilaFormula + ")";
            wss.Cell("L" + strUltimaFila).FormulaA1 = "=SUM(L" + strFilaInicial + ":L" + strUltimaFilaFormula + ")";
            wss.Cell("M" + strUltimaFila).FormulaA1 = "=SUM(M" + strFilaInicial + ":M" + strUltimaFilaFormula + ")";
            wss.Cell("N" + strUltimaFila).FormulaA1 = "=SUM(N" + strFilaInicial + ":N" + strUltimaFilaFormula + ")";
            wss.Cell("O" + strUltimaFila).FormulaA1 = "=SUM(O" + strFilaInicial + ":O" + strUltimaFilaFormula + ")";
            if (strUltimaColumna == "O")
            {
                wss.Cell("B" + strUltimaFila).Value = "Total General";

                wss.Cell("C" + strUltimaFila).FormulaA1 = "=SUM(C" + strFilaInicial + ":C" + strUltimaFilaFormula + ")";
                wss.Cell("D" + strUltimaFila).FormulaA1 = "=SUM(D" + strFilaInicial + ":D" + strUltimaFilaFormula + ")";
                wss.Cell("E" + strUltimaFila).FormulaA1 = "=SUM(E" + strFilaInicial + ":E" + strUltimaFilaFormula + ")";
                wss.Cell("F" + strUltimaFila).FormulaA1 = "=SUM(F" + strFilaInicial + ":F" + strUltimaFilaFormula + ")";

                strCeldas = "C" + strFilaInicial + ":" + strUltimaColumna + strUltimaFila;
            }
            else if (strUltimaColumna == "Q")
            {
                wss.Cell("D" + strUltimaFila).Value = "Total General";

                wss.Cell("E" + strUltimaFila).FormulaA1 = "=SUM(E" + strFilaInicial + ":E" + strUltimaFilaFormula + ")";
                wss.Cell("F" + strUltimaFila).FormulaA1 = "=SUM(F" + strFilaInicial + ":F" + strUltimaFilaFormula + ")";
                wss.Cell("P" + strUltimaFila).FormulaA1 = "=SUM(P" + strFilaInicial + ":P" + strUltimaFilaFormula + ")";
                wss.Cell("Q" + strUltimaFila).FormulaA1 = "=SUM(Q" + strFilaInicial + ":Q" + strUltimaFilaFormula + ")";

                strCeldas = "E" + strFilaInicial + ":" + strUltimaColumna + strUltimaFila;
            }
            else if (strUltimaColumna == "S")
            {
                wss.Cell("F" + strUltimaFila).Value = "Total General";

                wss.Cell("Q" + strUltimaFila).FormulaA1 = "=SUM(Q" + strFilaInicial + ":Q" + strUltimaFilaFormula + ")";
                wss.Cell("R" + strUltimaFila).FormulaA1 = "=SUM(R" + strFilaInicial + ":R" + strUltimaFilaFormula + ")";
                wss.Cell("S" + strUltimaFila).FormulaA1 = "=SUM(S" + strFilaInicial + ":S" + strUltimaFilaFormula + ")";

                strCeldas = "G" + strFilaInicial + ":" + strUltimaColumna + strUltimaFila;
            }

            if (entFiltro.Mostrar == 1)
            {
                wss.Range(strCeldas).Style.NumberFormat.Format = "$#,##0";
            }

            wss.Range(strCeldas).DataType = XLDataType.Number;
            wss.Range("A" + strUltimaFila + ":" + strUltimaColumna + strUltimaFila).Style.Font.Bold = true;

            wss.Range("A" + strFilaInicial + ":" + strUltimaColumna + strUltimaFila).Style
                       .Border.SetTopBorder(XLBorderStyleValues.Thin)
                       .Border.SetRightBorder(XLBorderStyleValues.Thin)
                       .Border.SetBottomBorder(XLBorderStyleValues.Thin)
                       .Border.SetLeftBorder(XLBorderStyleValues.Thin);

            #endregion

        }

        private void FormatoReporteSemanal(VenEstadisticaVentas entFiltro, List<VentaSemanal> lstVentaSemanal, ref IXLWorksheet wss)
        {
            #region Encabezado

            string strUltimaColumna = string.Empty;
            bool isColumnaImporte = false;
            wss.Style.Font.FontSize = 8;
            wss.Range("A1:B1").Merge();
            wss.Cell("A1").SetValue("Reporte Venta Semanal");
            wss.Range("A2:B2").Merge();
            wss.Cell("A2").SetValue(entFiltro.Sucursal);


            wss.Cell("J1").SetValue("Fecha Reporte: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            wss.Cell("J1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;


            wss.Range("A3:B3").Merge();
            wss.Cell("A3").SetValue("Periodo:");
            wss.Cell("A3").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            wss.Cell("C3").SetValue("Enero a Diciembre  " + entFiltro.SAnio);
            wss.Range("A4:B4").Merge();
            wss.Cell("A4").SetValue("Cliente:");
            wss.Cell("A4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            wss.Cell("C4").SetValue(entFiltro.SCliente);
            wss.Range("A5:B5").Merge();
            wss.Cell("A5").SetValue("Producto:");
            wss.Cell("A5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            wss.Cell("C5").SetValue(entFiltro.SProducto);
            wss.Range("A6:B6").Merge();
            wss.Cell("A6").SetValue("Categoría:");
            wss.Cell("A6").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            wss.Cell("C6").SetValue(entFiltro.sCategoria);

            if (ChkMov80.Checked)
            {
                wss.Cell("C7").SetValue("Considerar ventas con remisiones de tipo Mov 80");
            }

            wss.Range("A1:A6").Style.Font.Bold = true;
            wss.Range("A1:C6").Style.Font.FontSize = 10;

            wss.Cell("A8").Value = "# Territorio";
            wss.Cell("B8").Value = "Territorio";
            wss.Cell("C8").Value = "Num";
            wss.Cell("D8").Value = "Cliente";
            wss.Cell("E8").Value = "Código";
            wss.Cell("F8").Value = "Producto";

            if (entFiltro.Mostrar == 1)
            {
                wss.Cell("G8").Value = "Importe";
                wss.Cell("H8").Value = "Año";
                wss.Cell("I8").Value = "Semana";
                wss.Cell("J8").Value = "Mes";
                strUltimaColumna = "J8";
                isColumnaImporte = true;
            }
            else if (entFiltro.Mostrar == 2)
            {
                wss.Cell("G8").Value = "Unidades";
                wss.Cell("H8").Value = "Año";
                wss.Cell("I8").Value = "Semana";
                wss.Cell("J8").Value = "Mes";
                strUltimaColumna = "J8";
            }
            else
            {
                wss.Cell("G8").Value = "Importe";
                wss.Cell("H8").Value = "Unidades";
                wss.Cell("I8").Value = "Año";
                wss.Cell("J8").Value = "Semana";
                wss.Cell("k8").Value = "Mes";
                strUltimaColumna = "K8";
                isColumnaImporte = true;

            }

            wss.Columns("A:S").Width = 10;
            wss.Column("B").Width = 25;
            wss.Column("d").Width = 25;
            wss.Column("F").Width = 45;

            wss.Range("A8:" + strUltimaColumna).Style.Font.Bold = true;
            wss.Range("A8:" + strUltimaColumna).Style.Font.FontColor = XLColor.White;
            wss.Range("A8:" + strUltimaColumna).Style.Fill.BackgroundColor = XLColor.SteelBlue;

            wss.Range("A8:" + strUltimaColumna).SetAutoFilter();


            #endregion

            #region Datos
            int j = 9;
            for (int i = 0; i < lstVentaSemanal.Count; i++, j++)
            {
                wss.Cell("A" + j).Value = lstVentaSemanal[i].Id_Ter;
                wss.Cell("B" + j).Value = lstVentaSemanal[i].Nom_Ter;
                wss.Cell("C" + j).Value = lstVentaSemanal[i].Id_Cte;
                wss.Cell("D" + j).Value = lstVentaSemanal[i].Nom_Cte;
                wss.Cell("E" + j).Value = lstVentaSemanal[i].Id_prd;
                wss.Cell("F" + j).Value = lstVentaSemanal[i].Nom_Prd;

                if (entFiltro.Mostrar == 1)
                {
                    wss.Cell("G" + j).Value = lstVentaSemanal[i].Importe;
                    wss.Cell("H" + j).Value = lstVentaSemanal[i].Anio;
                    wss.Cell("I" + j).Value = lstVentaSemanal[i].Semana;
                    wss.Cell("J" + j).Value = lstVentaSemanal[i].Mes;
                }
                else if (entFiltro.Mostrar == 2)
                {
                    wss.Cell("G" + j).Value = lstVentaSemanal[i].Unidades;
                    wss.Cell("H" + j).Value = lstVentaSemanal[i].Anio;
                    wss.Cell("I" + j).Value = lstVentaSemanal[i].Semana;
                    wss.Cell("J" + j).Value = lstVentaSemanal[i].Mes;
                }
                else
                {
                    wss.Cell("G" + j).Value = lstVentaSemanal[i].Importe;
                    wss.Cell("H" + j).Value = lstVentaSemanal[i].Unidades;
                    wss.Cell("I" + j).Value = lstVentaSemanal[i].Anio;
                    wss.Cell("J" + j).Value = lstVentaSemanal[i].Semana;
                    wss.Cell("k" + j).Value = lstVentaSemanal[i].Mes;
                }
            }

            if (isColumnaImporte)
            {
                wss.Range("G9:G" + j.ToString()).DataType = XLDataType.Number;
                wss.Range("G9:G" + j.ToString()).Style.NumberFormat.Format = "$#,##0";
            }

            if (entFiltro.Mostrar == 3)
            {
                wss.Range("A8:K" + j.ToString()).Style
                       .Border.SetTopBorder(XLBorderStyleValues.Thin)
                       .Border.SetRightBorder(XLBorderStyleValues.Thin)
                       .Border.SetBottomBorder(XLBorderStyleValues.Thin)
                       .Border.SetLeftBorder(XLBorderStyleValues.Thin);
            }
            else
            {
                wss.Range("A8:J" + j.ToString()).Style
                       .Border.SetTopBorder(XLBorderStyleValues.Thin)
                       .Border.SetRightBorder(XLBorderStyleValues.Thin)
                       .Border.SetBottomBorder(XLBorderStyleValues.Thin)
                       .Border.SetLeftBorder(XLBorderStyleValues.Thin);
            }

            #endregion

        }

        public string GetExcelColumnName(int columnNumber)
        {
            string columnName = "";
            while (columnNumber > 0)
            {
                int modulo = (columnNumber - 1) % 26;
                columnName = Convert.ToChar(65 + modulo) + columnName;
                columnNumber = (columnNumber - modulo) / 26;
            }
            return columnName;
        }
        private void FormatoReportePeriodo(VenEstadisticaVentas entFiltro, string strUsuario, List<VenEstadisticaVentas> lstEstVenta, ref IXLWorksheet wss)
        {
            #region Encabezado

            string strUltimaColumna = string.Empty;
            string strDescripcionFiltro1 = string.Empty;
            string strValorFiltro1 = string.Empty;
            string strDescripcionFiltro2 = string.Empty;
            string strValorFiltro2 = string.Empty;

            string modoFecha = HiddenModoFecha.Value;
            //string fechaInicio1 = hdnFechaInicio.Value;
            //string fechaFin2 = hdnFechaFin.Value;

            int colIndexEncabezado = 0;

            wss.Style.Font.FontSize = 8;
            wss.Range("F1:I1").Merge();
            wss.Cell("F1").SetValue(entFiltro.nombre_Comercial);
            wss.Cell("F1").Style.Font.Bold = true;
            wss.Range("F2:I2").Merge();
            wss.Cell("F2").SetValue(entFiltro.Sucursal);
            wss.Range("F3:I3").Merge();
            wss.Cell("F3").SetValue("Estadística de ventas");
            wss.Range("F1:F3").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            wss.Range("F1:F3").Style.Font.FontSize = 11;

            wss.Range("M1:O1").Merge();
            wss.Cell("M1").SetValue(strUsuario);
            wss.Range("M2:O2").Merge();
            wss.Cell("M2").SetValue(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            wss.Range("M1:M2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            wss.Range("A1:B1").Merge();
            wss.Cell("A1").SetValue("Order por:");
            wss.Cell("C1").SetValue(entFiltro.SFiltro);
            wss.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            wss.Range("A2:B2").Merge();
            wss.Cell("A2").SetValue("Mostrar en:");
            wss.Cell("C2").SetValue(entFiltro.SMostrar);
            wss.Cell("A2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            wss.Range("A5:B5").Merge();
            wss.Cell("A5").SetValue("Producto:");
            wss.Cell("C5").SetValue(entFiltro.SProducto);
            wss.Cell("A5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            wss.Range("A6:B6").Merge();
            wss.Cell("A6").SetValue("Categoría:");
            wss.Cell("C6").SetValue(entFiltro.sCategoria);
            wss.Cell("A6").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            wss.Range("A7:B7").Merge();
            wss.Cell("A7").SetValue("Año:");
            //BRB
            if (modoFecha == "periodo")
            {
                wss.Cell("A7").SetValue("Periodo:");
                wss.Cell("C7").SetValue($"{entFiltro.FechaInicio:MMM/yyyy} a {entFiltro.FechaFin:MMM/yyyy}");
            }
            else
            {
                wss.Cell("A7").SetValue("Año:");
                wss.Cell("C7").SetValue(entFiltro.Anio.ToString());
            }
            //BRB
            wss.Cell("A7").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            wss.Cell("C7").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            wss.Range("A1:A7").Style.Font.Bold = true;




            wss.Range("A3:B3").Merge();
            wss.Cell("A3").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            wss.Range("A4:B4").Merge();
            wss.Cell("A4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            strDescripcionFiltro1 = "Nivel:";
            strValorFiltro1 = entFiltro.SNivel;
            strDescripcionFiltro2 = "Territorio:";
            strValorFiltro2 = entFiltro.STerritorio;



            switch (entFiltro.Reporte)
            {
                case 1:
                case 2:
                    wss.Cell("A9").Value = "Num. Ter ";
                    wss.Cell("B9").Value = "Territorio";
                    int colIndex = 3;
                    colIndexEncabezado = colIndex;
                    foreach (var clave in lstEstVenta[0].Meses.Keys)
                    {
                        if (clave.Length >= 6)
                        {
                            string mesStr = clave.Substring(0, 3);
                            string anioStr = clave.Substring(3);

                            if (int.TryParse(anioStr, out int anio))
                            {
                                DateTime fecha = DateTime.ParseExact(mesStr, "MMM", CultureInfo.InvariantCulture);
                                DateTime fechaCompleta = new DateTime(anio, fecha.Month, 1);
                                string nombreMes = fechaCompleta.ToString("MMMM", new CultureInfo("es-MX"));
                                nombreMes = char.ToUpper(nombreMes[0]) + nombreMes.Substring(1);
                                wss.Cell(9, colIndex).Value = nombreMes;
                            }
                            else
                            {
                                wss.Cell(9, colIndex).Value = clave;
                            }

                            colIndex++;
                        }
                    }

                    wss.Cell(9, colIndex).Value = "Total";
                    strUltimaColumna = GetExcelColumnName(colIndex);
                    wss.Column("B").Width = 25;

                    break;
                case 3:
                case 4:
                    wss.Cell("A9").Value = "Num. Ter ";
                    wss.Cell("B9").Value = "Territorio";
                    wss.Cell("C9").Value = "Num. Cte";
                    wss.Cell("D9").Value = "Cliente";
                    colIndex = 5;
                    colIndexEncabezado = colIndex;
                    foreach (var clave in lstEstVenta[0].Meses.Keys)
                    {
                        if (clave.Length >= 6)
                        {
                            string mesStr = clave.Substring(0, 3);
                            string anioStr = clave.Substring(3);

                            if (int.TryParse(anioStr, out int anio))
                            {
                                DateTime fecha = DateTime.ParseExact(mesStr, "MMM", CultureInfo.InvariantCulture);
                                DateTime fechaCompleta = new DateTime(anio, fecha.Month, 1);
                                string nombreMes = fechaCompleta.ToString("MMMM", new CultureInfo("es-MX"));
                                nombreMes = char.ToUpper(nombreMes[0]) + nombreMes.Substring(1);
                                wss.Cell(9, colIndex).Value = nombreMes;
                            }
                            else
                            {
                                wss.Cell(9, colIndex).Value = clave;
                            }

                            colIndex++;
                        }
                    }

                    wss.Cell(9, colIndex).Value = "Total";
                    strUltimaColumna = GetExcelColumnName(colIndex);
                    wss.Column("B").Width = 25;
                    wss.Column("D").Width = 35;
                    break;
                case 5:
                case 6:
                    wss.Cell("A9").Value = "Num. Ter ";
                    wss.Cell("B9").Value = "Territorio";
                    wss.Cell("C9").Value = "Código";
                    wss.Cell("D9").Value = "Producto";
                    colIndex = 5;
                    colIndexEncabezado = colIndex;
                    foreach (var clave in lstEstVenta[0].Meses.Keys)
                    {
                        if (clave.Length >= 6)
                        {
                            string mesStr = clave.Substring(0, 3);
                            string anioStr = clave.Substring(3);

                            if (int.TryParse(anioStr, out int anio))
                            {
                                DateTime fecha = DateTime.ParseExact(mesStr, "MMM", CultureInfo.InvariantCulture);
                                DateTime fechaCompleta = new DateTime(anio, fecha.Month, 1);
                                string nombreMes = fechaCompleta.ToString("MMMM", new CultureInfo("es-MX"));
                                nombreMes = char.ToUpper(nombreMes[0]) + nombreMes.Substring(1);
                                wss.Cell(9, colIndex).Value = nombreMes;
                            }
                            else
                            {
                                wss.Cell(9, colIndex).Value = clave;
                            }

                            colIndex++;
                        }
                    }

                    wss.Cell(9, colIndex).Value = "Total";
                    strUltimaColumna = GetExcelColumnName(colIndex);
                    wss.Column("B").Width = 25;
                    wss.Column("D").Width = 35;
                    break;
                case 7:
                case 8:
                    wss.Cell("A9").Value = "Num. Ter ";
                    wss.Cell("B9").Value = "Territorio";
                    wss.Cell("C9").Value = "Num. Cte";
                    wss.Cell("D9").Value = "Cliente";
                    wss.Cell("E9").Value = "Código";
                    wss.Cell("F9").Value = "Producto";
                    colIndex = 7;
                    colIndexEncabezado = colIndex;
                    foreach (var clave in lstEstVenta[0].Meses.Keys)
                    {
                        if (clave.Length >= 6)
                        {
                            string mesStr = clave.Substring(0, 3);
                            string anioStr = clave.Substring(3);

                            if (int.TryParse(anioStr, out int anio))
                            {
                                DateTime fecha = DateTime.ParseExact(mesStr, "MMM", CultureInfo.InvariantCulture);
                                DateTime fechaCompleta = new DateTime(anio, fecha.Month, 1);
                                string nombreMes = fechaCompleta.ToString("MMMM", new CultureInfo("es-MX"));
                                nombreMes = char.ToUpper(nombreMes[0]) + nombreMes.Substring(1);
                                wss.Cell(9, colIndex).Value = nombreMes;
                            }
                            else
                            {
                                wss.Cell(9, colIndex).Value = clave;
                            }

                            colIndex++;
                        }
                    }

                    wss.Cell(9, colIndex).Value = "Total";
                    strUltimaColumna = GetExcelColumnName(colIndex);
                    wss.Column("B").Width = 25;
                    wss.Column("D").Width = 35;
                    wss.Column("F").Width = 45;
                    break;
                case 9:
                case 10:
                    wss.Cell("A9").Value = "Num. Cte";
                    wss.Cell("B9").Value = "Cliente";
                    strDescripcionFiltro2 = "Cliente:";
                    strValorFiltro2 = entFiltro.SCliente;
                    colIndex = 3;
                    colIndexEncabezado = colIndex;
                    foreach (var clave in lstEstVenta[0].Meses.Keys)
                    {
                        if (clave.Length >= 6)
                        {
                            string mesStr = clave.Substring(0, 3);
                            string anioStr = clave.Substring(3);

                            if (int.TryParse(anioStr, out int anio))
                            {
                                DateTime fecha = DateTime.ParseExact(mesStr, "MMM", CultureInfo.InvariantCulture);
                                DateTime fechaCompleta = new DateTime(anio, fecha.Month, 1);
                                string nombreMes = fechaCompleta.ToString("MMMM", new CultureInfo("es-MX"));
                                nombreMes = char.ToUpper(nombreMes[0]) + nombreMes.Substring(1);
                                wss.Cell(9, colIndex).Value = nombreMes;
                            }
                            else
                            {
                                wss.Cell(9, colIndex).Value = clave;
                            }

                            colIndex++;
                        }
                    }

                    wss.Cell(9, colIndex).Value = "Total";
                    strUltimaColumna = GetExcelColumnName(colIndex);
                    wss.Column("B").Width = 25;
                    break;
                case 11:
                case 12:
                    wss.Cell("A9").Value = "Num. Cte ";
                    wss.Cell("B9").Value = "Cliente";
                    wss.Cell("C9").Value = "Código";
                    wss.Cell("D9").Value = "Producto";
                    colIndex = 5;
                    colIndexEncabezado = colIndex;
                    foreach (var clave in lstEstVenta[0].Meses.Keys)
                    {
                        if (clave.Length >= 6)
                        {
                            string mesStr = clave.Substring(0, 3);
                            string anioStr = clave.Substring(3);

                            if (int.TryParse(anioStr, out int anio))
                            {
                                DateTime fecha = DateTime.ParseExact(mesStr, "MMM", CultureInfo.InvariantCulture);
                                DateTime fechaCompleta = new DateTime(anio, fecha.Month, 1);
                                string nombreMes = fechaCompleta.ToString("MMMM", new CultureInfo("es-MX"));
                                nombreMes = char.ToUpper(nombreMes[0]) + nombreMes.Substring(1);
                                wss.Cell(9, colIndex).Value = nombreMes;
                            }
                            else
                            {
                                wss.Cell(9, colIndex).Value = clave;
                            }

                            colIndex++;
                        }
                    }

                    wss.Cell(9, colIndex).Value = "Total";
                    strUltimaColumna = GetExcelColumnName(colIndex);
                    wss.Column("B").Width = 25;
                    wss.Column("D").Width = 35;
                    strDescripcionFiltro2 = "Cliente:";
                    strValorFiltro2 = entFiltro.SCliente;
                    break;
                case 13:
                case 14:
                    wss.Cell("A9").Value = "Código ";
                    wss.Cell("B9").Value = "Producto";
                    strDescripcionFiltro1 = string.Empty;
                    strValorFiltro1 = string.Empty;
                    strDescripcionFiltro2 = string.Empty;
                    strValorFiltro2 = string.Empty;
                    colIndex = 3;
                    colIndexEncabezado = colIndex;
                    foreach (var clave in lstEstVenta[0].Meses.Keys)
                    {
                        if (clave.Length >= 6)
                        {
                            string mesStr = clave.Substring(0, 3);
                            string anioStr = clave.Substring(3);

                            if (int.TryParse(anioStr, out int anio))
                            {
                                DateTime fecha = DateTime.ParseExact(mesStr, "MMM", CultureInfo.InvariantCulture);
                                DateTime fechaCompleta = new DateTime(anio, fecha.Month, 1);
                                string nombreMes = fechaCompleta.ToString("MMMM", new CultureInfo("es-MX"));
                                nombreMes = char.ToUpper(nombreMes[0]) + nombreMes.Substring(1);
                                wss.Cell(9, colIndex).Value = nombreMes;
                            }
                            else
                            {
                                wss.Cell(9, colIndex).Value = clave;
                            }

                            colIndex++;
                        }
                    }

                    wss.Cell(9, colIndex).Value = "Total";
                    strUltimaColumna = GetExcelColumnName(colIndex);
                    wss.Column("B").Width = 25;
                    break;
                case 16:
                case 17:
                    wss.Cell("A9").Value = "Num. Rik ";
                    wss.Cell("B9").Value = "Representante";
                    colIndex = 3;
                    colIndexEncabezado = colIndex;
                    foreach (var clave in lstEstVenta[0].Meses.Keys)
                    {
                        if (clave.Length >= 6)
                        {
                            string mesStr = clave.Substring(0, 3);
                            string anioStr = clave.Substring(3);

                            if (int.TryParse(anioStr, out int anio))
                            {
                                DateTime fecha = DateTime.ParseExact(mesStr, "MMM", CultureInfo.InvariantCulture);
                                DateTime fechaCompleta = new DateTime(anio, fecha.Month, 1);
                                string nombreMes = fechaCompleta.ToString("MMMM", new CultureInfo("es-MX"));
                                nombreMes = char.ToUpper(nombreMes[0]) + nombreMes.Substring(1);
                                wss.Cell(9, colIndex).Value = nombreMes;
                            }
                            else
                            {
                                wss.Cell(9, colIndex).Value = clave;
                            }

                            colIndex++;
                        }
                    }

                    wss.Cell(9, colIndex).Value = "Total";
                    strUltimaColumna = GetExcelColumnName(colIndex);
                    wss.Column("B").Width = 25;
                    break;
                case 18:
                case 19:
                    wss.Cell("A9").Value = "Num. Rik ";
                    wss.Cell("B9").Value = "Representante";
                    wss.Cell("C9").Value = "Num. Cte";
                    wss.Cell("D9").Value = "Cliente";
                    colIndex = 5;
                    colIndexEncabezado = colIndex;
                    foreach (var clave in lstEstVenta[0].Meses.Keys)
                    {
                        if (clave.Length >= 6)
                        {
                            string mesStr = clave.Substring(0, 3);
                            string anioStr = clave.Substring(3);

                            if (int.TryParse(anioStr, out int anio))
                            {
                                DateTime fecha = DateTime.ParseExact(mesStr, "MMM", CultureInfo.InvariantCulture);
                                DateTime fechaCompleta = new DateTime(anio, fecha.Month, 1);
                                string nombreMes = fechaCompleta.ToString("MMMM", new CultureInfo("es-MX"));
                                nombreMes = char.ToUpper(nombreMes[0]) + nombreMes.Substring(1);
                                wss.Cell(9, colIndex).Value = nombreMes;
                            }
                            else
                            {
                                wss.Cell(9, colIndex).Value = clave;
                            }

                            colIndex++;
                        }
                    }

                    wss.Cell(9, colIndex).Value = "Total";
                    strUltimaColumna = GetExcelColumnName(colIndex);
                    wss.Column("B").Width = 25;
                    wss.Column("D").Width = 35;
                    break;
                case 20:
                case 21:
                    wss.Cell("A9").Value = "Num. Rep ";
                    wss.Cell("B9").Value = "Representante";
                    wss.Cell("C9").Value = "Num. Cte";
                    wss.Cell("D9").Value = "Cliente";
                    wss.Cell("E9").Value = "Código";
                    wss.Cell("F9").Value = "Producto";
                    colIndex = 7;
                    colIndexEncabezado = colIndex;
                    foreach (var clave in lstEstVenta[0].Meses.Keys)
                    {
                        if (clave.Length >= 6)
                        {
                            string mesStr = clave.Substring(0, 3);
                            string anioStr = clave.Substring(3);

                            if (int.TryParse(anioStr, out int anio))
                            {
                                DateTime fecha = DateTime.ParseExact(mesStr, "MMM", CultureInfo.InvariantCulture);
                                DateTime fechaCompleta = new DateTime(anio, fecha.Month, 1);
                                string nombreMes = fechaCompleta.ToString("MMMM", new CultureInfo("es-MX"));
                                nombreMes = char.ToUpper(nombreMes[0]) + nombreMes.Substring(1);
                                wss.Cell(9, colIndex).Value = nombreMes;
                            }
                            else
                            {
                                wss.Cell(9, colIndex).Value = clave;
                            }

                            colIndex++;
                        }
                    }

                    wss.Cell(9, colIndex).Value = "Total";
                    strUltimaColumna = GetExcelColumnName(colIndex);
                    wss.Column("B").Width = 25;
                    wss.Column("D").Width = 35;
                    wss.Column("F").Width = 45;
                    break;
                case 22:
                case 23:
                    wss.Cell("A9").Value = "Num. Rik ";
                    wss.Cell("B9").Value = "Representante";
                    wss.Cell("C9").Value = "Código";
                    wss.Cell("D9").Value = "Producto";
                    colIndex = 5;
                    colIndexEncabezado = colIndex;
                    foreach (var clave in lstEstVenta[0].Meses.Keys)
                    {
                        if (clave.Length >= 6)
                        {
                            string mesStr = clave.Substring(0, 3);
                            string anioStr = clave.Substring(3);

                            if (int.TryParse(anioStr, out int anio))
                            {
                                DateTime fecha = DateTime.ParseExact(mesStr, "MMM", CultureInfo.InvariantCulture);
                                DateTime fechaCompleta = new DateTime(anio, fecha.Month, 1);
                                string nombreMes = fechaCompleta.ToString("MMMM", new CultureInfo("es-MX"));
                                nombreMes = char.ToUpper(nombreMes[0]) + nombreMes.Substring(1);
                                wss.Cell(9, colIndex).Value = nombreMes;
                            }
                            else
                            {
                                wss.Cell(9, colIndex).Value = clave;
                            }

                            colIndex++;
                        }
                    }

                    wss.Cell(9, colIndex).Value = "Total";
                    strUltimaColumna = GetExcelColumnName(colIndex);
                    wss.Column("B").Width = 25;
                    wss.Column("D").Width = 35;
                    break;
                default:
                    strUltimaColumna = "S";
                    break;
            }

            wss.Range("A9:" + strUltimaColumna + "9").Style.Font.Bold = true;
            wss.Range("A9:" + strUltimaColumna + "9").Style.Font.FontColor = XLColor.White;
            wss.Range("A9:" + strUltimaColumna + "9").Style.Fill.BackgroundColor = XLColor.SteelBlue;
            wss.Range("A9:" + strUltimaColumna + "9").SetAutoFilter();


            if (!string.IsNullOrEmpty(strDescripcionFiltro1))
            {
                wss.Cell("A3").SetValue(strDescripcionFiltro1);
                wss.Cell("C3").SetValue(strValorFiltro1);
            }

            if (!string.IsNullOrEmpty(strDescripcionFiltro2))
            {
                wss.Cell("A4").SetValue(strDescripcionFiltro2);
                wss.Cell("C4").SetValue(strValorFiltro2);
            }

            //BRB-----------------------
            if (modoFecha == "periodo" && entFiltro.FechaInicio != null && entFiltro.FechaFin != null)
            {
                var colores = new List<XLColor>
                {
                    XLColor.FromArgb(189, 215, 238),
                    XLColor.FromArgb(221, 235, 247),
                    XLColor.FromArgb(155, 194, 230),
                };

                var clavesMeses = lstEstVenta[0].Meses.Keys.ToList();
                clavesMeses.Sort((a, b) =>
                {
                    DateTime fa = new DateTime(int.Parse(a.Substring(3)), DateTime.ParseExact(a.Substring(0, 3), "MMM", CultureInfo.InvariantCulture).Month, 1);
                    DateTime fb = new DateTime(int.Parse(b.Substring(3)), DateTime.ParseExact(b.Substring(0, 3), "MMM", CultureInfo.InvariantCulture).Month, 1);
                    return fa.CompareTo(fb);
                });

                var agrupadoPorAnio = new Dictionary<int, List<int>>();
                int colIndex = colIndexEncabezado;

                foreach (var clave in clavesMeses)
                {
                    string mesStr = clave.Substring(0, 3);
                    string anioStr = clave.Substring(3);

                    int anio = int.Parse(anioStr);
                    if (!agrupadoPorAnio.ContainsKey(anio))
                        agrupadoPorAnio[anio] = new List<int>();

                    agrupadoPorAnio[anio].Add(colIndex);
                    colIndex++;
                }

                int colorIndex = 0;
                foreach (var anio in agrupadoPorAnio.Keys.OrderBy(a => a))
                {
                    var columnas = agrupadoPorAnio[anio];
                    int colInicio = columnas.First();
                    int colFin = columnas.Last();
                    var color = colores[colorIndex % colores.Count];
                    wss.Range(8, colInicio, 8, colFin).Merge();
                    wss.Cell(8, colInicio).Value = anio.ToString();
                    wss.Cell(8, colInicio).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    wss.Cell(8, colInicio).Style.Fill.BackgroundColor = color;
                    wss.Row(8).Style.Font.Bold = true;
                    colorIndex++;
                }
            }
            //BRB-----------------------------



            #endregion

            #region Datos

            int intTotalFilas = lstEstVenta.Count;
            int intFilaInicial = 10;

            string strFilaInicial = intFilaInicial.ToString();
            int j = intFilaInicial;
            string strCeldas = string.Empty;



            switch (entFiltro.Reporte)
            {
                case 1:
                case 2:
                    for (int i2 = 0; i2 < lstEstVenta.Count; i2++, j++)
                    {
                        var venta = lstEstVenta[i2];
                        wss.Cell("A" + j).Value = venta.id_ter;
                        wss.Cell("B" + j).Value = venta.nombre_terr;

                        int colIndex = colIndexEncabezado;

                        foreach (var mes in venta.Meses)
                        {
                            wss.Cell(j, colIndex).Value = mes.Value;
                            colIndex++;
                        }
                        wss.Cell(j, colIndex).Value = venta.total;
                    }
                    break;
                case 3:
                case 4:
                    for (int i2 = 0; i2 < lstEstVenta.Count; i2++, j++)
                    {
                        var venta = lstEstVenta[i2];
                        wss.Cell("A" + j).Value = venta.id_ter;
                        wss.Cell("B" + j).Value = venta.nombre_terr;
                        wss.Cell("C" + j).Value = venta.id_cte;
                        wss.Cell("D" + j).Value = venta.Cliente;
                        int colIndex = colIndexEncabezado;

                        foreach (var mes in venta.Meses)
                        {
                            wss.Cell(j, colIndex).Value = mes.Value;
                            colIndex++;
                        }
                        wss.Cell(j, colIndex).Value = venta.total;
                    }
                    break;
                case 5:
                case 6:
                    for (int i2 = 0; i2 < lstEstVenta.Count; i2++, j++)
                    {
                        var venta = lstEstVenta[i2];
                        wss.Cell("A" + j).Value = venta.id_ter;
                        wss.Cell("B" + j).Value = venta.nombre_terr;
                        wss.Cell("C" + j).Value = venta.id_prd;
                        wss.Cell("D" + j).Value = venta.Producto;
                        int colIndex = colIndexEncabezado;

                        foreach (var mes in venta.Meses)
                        {
                            wss.Cell(j, colIndex).Value = mes.Value;
                            colIndex++;
                        }
                        wss.Cell(j, colIndex).Value = venta.total;
                    }
                    break;
                case 7:
                case 8:
                    for (int i2 = 0; i2 < lstEstVenta.Count; i2++, j++)
                    {
                        var venta = lstEstVenta[i2];
                        wss.Cell("A" + j).Value = venta.id_ter;
                        wss.Cell("B" + j).Value = venta.nombre_terr;
                        wss.Cell("C" + j).Value = venta.id_cte;
                        wss.Cell("D" + j).Value = venta.Cliente;
                        wss.Cell("E" + j).Value = venta.id_prd;
                        wss.Cell("F" + j).Value = venta.Producto;
                        int colIndex = colIndexEncabezado;

                        foreach (var mes in venta.Meses)
                        {
                            wss.Cell(j, colIndex).Value = mes.Value;
                            colIndex++;
                        }
                        wss.Cell(j, colIndex).Value = venta.total;
                    }
                    break;
                case 9:
                case 10:
                    for (int i2 = 0; i2 < lstEstVenta.Count; i2++, j++)
                    {
                        var venta = lstEstVenta[i2];
                        wss.Cell("A" + j).Value = venta.id_cte;
                        wss.Cell("B" + j).Value = venta.Cliente;

                        int colIndex = colIndexEncabezado;

                        foreach (var mes in venta.Meses)
                        {
                            wss.Cell(j, colIndex).Value = mes.Value;
                            colIndex++;
                        }
                        wss.Cell(j, colIndex).Value = venta.total;
                    }
                    break;
                case 11:
                case 12:
                    for (int i2 = 0; i2 < lstEstVenta.Count; i2++, j++)
                    {
                        var venta = lstEstVenta[i2];
                        wss.Cell("A" + j).Value = venta.id_cte;
                        wss.Cell("B" + j).Value = venta.Cliente;
                        wss.Cell("C" + j).Value = venta.id_prd;
                        wss.Cell("D" + j).Value = venta.Producto;
                        int colIndex = colIndexEncabezado;

                        foreach (var mes in venta.Meses)
                        {
                            wss.Cell(j, colIndex).Value = mes.Value;
                            colIndex++;
                        }
                        wss.Cell(j, colIndex).Value = venta.total;
                    }

                    break;
                case 13:
                case 14:
                    for (int i2 = 0; i2 < lstEstVenta.Count; i2++, j++)
                    {
                        var venta = lstEstVenta[i2];
                        wss.Cell("A" + j).Value = venta.id_prd;
                        wss.Cell("B" + j).Value = venta.Producto;

                        int colIndex = colIndexEncabezado;

                        foreach (var mes in venta.Meses)
                        {
                            wss.Cell(j, colIndex).Value = mes.Value;
                            colIndex++;
                        }
                        wss.Cell(j, colIndex).Value = venta.total;
                    }

                    break;
                case 16:
                case 17:
                    for (int i2 = 0; i2 < lstEstVenta.Count; i2++, j++)
                    {
                        var venta = lstEstVenta[i2];
                        wss.Cell("A" + j).Value = venta.id_rik;
                        wss.Cell("B" + j).Value = venta.nombre_rik;

                        int colIndex = colIndexEncabezado;

                        foreach (var mes in venta.Meses)
                        {
                            wss.Cell(j, colIndex).Value = mes.Value;
                            colIndex++;
                        }
                        wss.Cell(j, colIndex).Value = venta.total;
                    }

                    break;
                case 18:
                case 19:
                    for (int i2 = 0; i2 < lstEstVenta.Count; i2++, j++)
                    {
                        var venta = lstEstVenta[i2];
                        wss.Cell("A" + j).Value = venta.id_rik;
                        wss.Cell("B" + j).Value = venta.nombre_rik;
                        wss.Cell("C" + j).Value = venta.id_cte;
                        wss.Cell("D" + j).Value = venta.Cliente;
                        int colIndex = colIndexEncabezado;

                        foreach (var mes in venta.Meses)
                        {
                            wss.Cell(j, colIndex).Value = mes.Value;
                            colIndex++;
                        }
                        wss.Cell(j, colIndex).Value = venta.total;
                    }

                    break;
                case 20:
                case 21:
                    for (int i2 = 0; i2 < lstEstVenta.Count; i2++, j++)
                    {
                        var venta = lstEstVenta[i2];
                        wss.Cell("A" + j).Value = venta.id_rik;
                        wss.Cell("B" + j).Value = venta.nombre_rik;
                        wss.Cell("C" + j).Value = venta.id_cte;
                        wss.Cell("D" + j).Value = venta.Cliente;
                        wss.Cell("E" + j).Value = venta.id_prd;
                        wss.Cell("F" + j).Value = venta.Producto;
                        int colIndex = colIndexEncabezado;

                        foreach (var mes in venta.Meses)
                        {
                            wss.Cell(j, colIndex).Value = mes.Value;
                            colIndex++;
                        }
                        wss.Cell(j, colIndex).Value = venta.total;
                    }
                    break;
                case 22:
                case 23:
                    for (int i2 = 0; i2 < lstEstVenta.Count; i2++, j++)
                    {
                        var venta = lstEstVenta[i2];
                        wss.Cell("A" + j).Value = venta.id_rik;
                        wss.Cell("B" + j).Value = venta.nombre_rik;
                        wss.Cell("C" + j).Value = venta.id_prd;
                        wss.Cell("D" + j).Value = venta.Producto;
                        int colIndex = colIndexEncabezado;

                        foreach (var mes in venta.Meses)
                        {
                            wss.Cell(j, colIndex).Value = mes.Value;
                            colIndex++;
                        }
                        wss.Cell(j, colIndex).Value = venta.total;
                    }

                    break;
                default:

                    break;
            }



            string strUltimaFila = j.ToString();
            string strUltimaFilaFormula = (j - 1).ToString();

            int colIndexInicioMeses = colIndexEncabezado;
            int colIndexFinalMeses = colIndexInicioMeses + lstEstVenta[0].Meses.Count - 1;
            int colIndexTotal = colIndexFinalMeses + 1;

            for (int c = colIndexInicioMeses; c <= colIndexFinalMeses; c++)
            {
                string colLetra = GetExcelColumnName(c);
                wss.Cell(colLetra + strUltimaFila).FormulaA1 = $"=SUM({colLetra}{strFilaInicial}:{colLetra}{strUltimaFilaFormula})";
            }

            string colLetraTotal = GetExcelColumnName(colIndexTotal);
            wss.Cell(colLetraTotal + strUltimaFila).FormulaA1 = $"=SUM({colLetraTotal}{strFilaInicial}:{colLetraTotal}{strUltimaFilaFormula})";

            wss.Cell("B" + strUltimaFila).Value = "Total General";

            strUltimaColumna = colLetraTotal;
            string colLetraInicio = GetExcelColumnName(colIndexEncabezado);
            strCeldas = $"{colLetraInicio}{strFilaInicial}:{strUltimaColumna}{strUltimaFila}";

            if (entFiltro.Mostrar == 1)
            {
                wss.Range(strCeldas).Style.NumberFormat.Format = "$#,##0";
            }

            wss.Range(strCeldas).DataType = XLDataType.Number;
            wss.Range("A" + strUltimaFila + ":" + strUltimaColumna + strUltimaFila).Style.Font.Bold = true;
            wss.Range("A" + strFilaInicial + ":" + strUltimaColumna + strUltimaFila).Style
               .Border.SetTopBorder(XLBorderStyleValues.Thin)
               .Border.SetRightBorder(XLBorderStyleValues.Thin)
               .Border.SetBottomBorder(XLBorderStyleValues.Thin)
               .Border.SetLeftBorder(XLBorderStyleValues.Thin);

            #endregion

        }
    }
}