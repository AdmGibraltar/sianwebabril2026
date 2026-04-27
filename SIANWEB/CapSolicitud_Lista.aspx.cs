using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using CapaEntidad;
using CapaNegocios;
using System.Collections;


namespace SIANWEB
{
    public partial class CapSolicitud_Lista : System.Web.UI.Page
    {
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

        public List<Solicitud> lstSolicitud
        {
            get { return (List<Solicitud>)Session["lstSolicitud" + Session.SessionID]; }
            set { Session["lstSolicitud" + Session.SessionID] = value; }
        }

        public DataTable objdtLista { get; set; }

        protected DataTable objdtTabla { get { if (ViewState["objdtTabla"] != null) { return (DataTable)ViewState["objdtTabla"]; } else { return objdtLista; } } set { ViewState["objdtTabla"] = value; } }

        int id_Tu;

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
                        this.ValidarPermisos();
                        txtFecha1.SelectedDate = sesion.CalendarioIni;
                        txtFecha2.SelectedDate = sesion.CalendarioFin;
                        CargarCentros();
                        InicializarTablaProductos();
                        CargarEstatus();
                        CargarAccion();
                        CargarTServicio();
                        rgFolios.DataSource = this.GetList();
                    }
                }
            }
            catch (Exception ex)
            {
                RAM.Alert("Error, " + ex.Message);
            }
        }

        protected void cmbCentrosDist_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN__Comun comun = new CN__Comun();

                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                if (sesion.CalendarioIni >= txtFecha1.MinDate && sesion.CalendarioIni <= txtFecha1.MaxDate)
                {
                    txtFecha1.DbSelectedDate = sesion.CalendarioIni;
                }
                if (sesion.CalendarioFin >= txtFecha2.MinDate && sesion.CalendarioFin <= txtFecha2.MaxDate)
                {
                    txtFecha2.DbSelectedDate = sesion.CalendarioFin;
                }

                Session["Sesion" + Session.SessionID] = sesion;

                //txtCliente1.Text = string.Empty;
                //txtCliente2.Text = string.Empty;
                //this.CargarUsuarios();
                //rgFactura.Rebind();
            }
            catch (Exception ex)
            {
                //ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private void CargarCentros()
        {
            try
            {
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];

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

        private void ValidarPermisos()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Pagina pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                if (pag.Length > 1)
                {
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                }
                else
                {
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;
                }
                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, Sesion.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;
                Permiso Permiso = new Permiso();
                Permiso.Sm_cve = pagina.Clave;

            }
            catch (Exception ex)
            {
                throw ex;
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
                comun.LlenaCombo(Sesion.Emp_Cnx, "spCattiposervicio_Combo", ref cmbTipoServicio);
                if (Lista.Count > 0)
                {
                    cmbTipoServicio.DataSource = Lista;
                    cmbTipoServicio.DataValueField = "Id";
                    cmbTipoServicio.DataTextField = "Descripcion";
                    cmbTipoServicio.DataBind();
                }
            }
            catch (Exception ex)
            {
                RAM.Alert("Error, " + ex.Message);
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
                comun.LlenaCombo(Sesion.Emp_Cnx, "spCatAcciones_Combo", ref cmbAccionCierre);
                if (Lista.Count > 0)
                {
                    cmbAccionCierre.DataSource = Lista;
                    cmbAccionCierre.DataValueField = "Id";
                    cmbAccionCierre.DataTextField = "Descripcion";
                    cmbAccionCierre.DataBind();
                }
            }
            catch (Exception ex)
            {
                RAM.Alert("Error, " + ex.Message);
            }
        }

        private List<Solicitud> GetList()
        {
            try
            {
                Sesion Sesion = new CapaEntidad.Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<Solicitud> lstSolicitud = new List<Solicitud>();
                CN_Solicitud CN = new CN_Solicitud();

                int Estatus = 0;
                if (CmbEstado.SelectedIndex > 0)
                    Estatus = CmbEstado.SelectedIndex;

                int Solicitud = 0;
                if (txtIdSolicitud.Text != "")
                    Solicitud = int.Parse(txtIdSolicitud.Text);

                int Id_TipoServicio = 0;
                if (cmbTipoServicio.SelectedIndex > 0)
                    Id_TipoServicio = int.Parse(cmbTipoServicio.SelectedItem.Value);

                int Id_Accion = 0;
                if (cmbAccionCierre.SelectedIndex > 0)
                    Id_Accion = int.Parse(cmbAccionCierre.SelectedItem.Value);

                CN.ConsultaSolicitudes(Sesion, sesion.Id_TU, ref lstSolicitud,
                    Solicitud,
                    this.txtNomCliente.Text == string.Empty ? "" : this.txtNomCliente.Text,
                    this.txtFactura.Text == string.Empty ? 0 : Convert.ToInt32(this.txtFactura.Text),
                    this.txtFecha1.SelectedDate == null ? DateTime.MinValue : Convert.ToDateTime(this.txtFecha1.SelectedDate),
                    this.txtFecha2.SelectedDate == null ? DateTime.MinValue : Convert.ToDateTime(this.txtFecha2.SelectedDate),
                    Id_TipoServicio,
                    Id_Accion,
                    Estatus);

                rgFolios.Columns.FindByUniqueName("Eliminar").Display = false;

                return lstSolicitud;
            }
            catch (Exception ex)
            {
                RAM.Alert("Error, " + ex.Message);
                return lstSolicitud;
            }
        }

        private void CargarSolicitudes(List<Solicitud> lstSolicitud)
        {
            try
            {
                objdtTabla.Clear();
                foreach (Solicitud sol in lstSolicitud)
                {
                    ArrayList ArrayProd = new ArrayList();
                    ArrayProd.Add(sol.Id_Solicitud);
                    ArrayProd.Add(sol.Cte_Nom);
                    ArrayProd.Add(sol.Num_Factura);
                    ArrayProd.Add(sol.Descripcion);
                    ArrayProd.Add(sol.Tipo_Servicio);
                    ArrayProd.Add(sol.Estado);

                    objdtTabla.Rows.Add(ArrayProd.ToArray());
                }
            }
            catch (Exception ex)
            {
                RAM.Alert("Error, " + ex.Message);
            }
        }

        private void CargarEstatus()
        {
            try
            {
                Sesion Sesion = new CapaEntidad.Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<Comun> Lista = new List<Comun>();
                CapaNegocios.CN__Comun comun = new CN__Comun();
                comun.LlenaCombo(sesion.Emp_Cnx, "spCatEstado_Combo", ref CmbEstado);
                if (Lista.Count > 0)
                {
                    CmbEstado.DataSource = Lista;
                    CmbEstado.DataValueField = "Id";
                    CmbEstado.DataTextField = "Descripcion";
                    CmbEstado.DataBind();
                }
                if (sesion.Id_TU == 3)
                {
                    CmbEstado.SelectedIndex = 1;
                    CmbEstado.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                RAM.Alert("Error, " + ex.Message);
            }
        }

        protected void rgFolios_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {  //Llenar Grid
                    rgFolios.DataSource = GetList();
                    //rgFolios.DataBind();
                }
            }
            catch (Exception ex)
            {
                RAM.Alert("Error, " + ex.Message);
                //ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void CmbEstado_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion Sesion = new CapaEntidad.Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                int Estatus = CmbEstado.SelectedIndex;

                DataRow[] Ar_dr;
                List<Solicitud> lstSolicitud = new List<Solicitud>();
                Solicitud sol;
                Ar_dr = objdtTabla.Select("estado ='" + CmbEstado.Text + "'");
                if (CmbEstado.Text == "-- Todos --")
                {
                    Ar_dr = objdtTabla.Select();
                    for (int x = 0; x < Ar_dr.Length; x++)
                    {
                        sol = new Solicitud();
                        sol.Id_Solicitud = int.Parse(Ar_dr[x]["Id_Solicitud"].ToString());
                        sol.Cte_Nom = Ar_dr[x]["cNombre"].ToString();
                        sol.Num_Factura = int.Parse(Ar_dr[x]["Num_Factura"].ToString());
                        sol.Descripcion = Ar_dr[x]["Descripcion"].ToString();
                        sol.Tipo_Servicio = Ar_dr[x]["Tipo_Servicio"].ToString();
                        sol.Estado = Ar_dr[x]["estado"].ToString();
                        lstSolicitud.Add(sol);
                    }
                }
                else
                    for (int x = 0; x < Ar_dr.Length; x++)
                    {
                        sol = new Solicitud();
                        sol.Id_Solicitud = int.Parse(Ar_dr[x]["Id_Solicitud"].ToString());
                        sol.Cte_Nom = Ar_dr[x]["cNombre"].ToString();
                        sol.Num_Factura = int.Parse(Ar_dr[x]["Num_Factura"].ToString());
                        sol.Descripcion = Ar_dr[x]["Descripcion"].ToString();
                        sol.Tipo_Servicio = Ar_dr[x]["Tipo_Servicio"].ToString();
                        sol.Estado = Ar_dr[x]["estado"].ToString();
                        lstSolicitud.Add(sol);
                    }

                rgFolios.DataSource = lstSolicitud;
                rgFolios.DataBind();

            }
            catch (Exception ex)
            {
                RAM.Alert("Error, " + ex.Message);
            }
        }

        protected void rgFolios_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Int32 item = default(Int32);
                if (e.Item == null) return;
                item = e.Item.ItemIndex;

                if (item >= 0)
                {
                    Int32 Id_Solicitud = Convert.ToInt32(rgFolios.Items[item]["Id_Solicitud"].Text);
                    string Estatus = rgFolios.Items[item]["Estatus"].Text;

                    switch (e.CommandName.ToString())
                    {
                        case "Modificar":
                            RAM.ResponseScripts.Add(string.Concat(@"AbrirVentana_Solicitud('", Id_Solicitud, "')"));
                            break;
                        case "Eliminar":
                            Estatus = rgFolios.Items[item]["Estatus"].Text;
                            if (Estatus == "Cancelado" || Estatus == "Rechazado" || Estatus == "Cerrado")
                                RAM.Alert("Esta solicitud ya esta dada de baja.");
                            else
                                CancelarSolicitud(Id_Solicitud);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                RAM.Alert("Error, " + ex.Message);
            }
        }

        private void InicializarTablaProductos()
        {
            try
            {
                objdtLista = new DataTable();
                objdtLista.Columns.Add("Id_Solicitud");
                objdtLista.Columns.Add("cNombre");
                objdtLista.Columns.Add("Num_Factura");
                objdtLista.Columns.Add("Descripcion");
                objdtLista.Columns.Add("Tipo_Servicio");
                objdtLista.Columns.Add("estado");
                objdtTabla = objdtLista;
            }
            catch (Exception ex)
            {
                RAM.Alert("Error, " + ex.Message);
            }
        }


        private void CancelarSolicitud(int Id_Solicitud)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_Solicitud CN = new CN_Solicitud();
                Solicitud sol = new Solicitud();
                string respuesta = "";
                sol.Id_Solicitud = Id_Solicitud;
                CN.CancelaSolicitud(sesion, ref respuesta, ref sol);
                if (respuesta == "Cancelado")
                {
                    RAM.Alert("La solicitud fue cancelada con éxito");
                    rgFolios.DataSource = this.GetList();

                }
                else
                {
                    RAM.Alert("Ocurrio un error, favor de intentar nuevamente");
                    rgFolios.DataSource = this.GetList();
                }
            }
            catch (Exception ex)
            {
                RAM.Alert("Error, " + ex.Message);
            }
        }

        protected void rgFolios_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            try
            {
                rgFolios.DataSource = this.GetList();
            }
            catch (Exception ex)
            {
                RAM.Alert("Error, " + ex.Message);
            }
        }

        protected void RadToolBar1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;

                switch (btn.CommandName)
                {
                    case "Nuevo":
                        RAM.ResponseScripts.Add(string.Concat(@"AbrirVentana_Solicitud('", 0, "')"));
                        break;
                }

            }
            catch (Exception ex)
            {
                RAM.Alert("Error, " + ex.Message);
            }
        }

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void CmbEstado_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                rgFolios.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}