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
    public partial class ComprasLocalesConsulta : System.Web.UI.Page
    {
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

                        CargarValoresCombo();
                        ListadoSolicitudes();

                        this.divConsultaSolicitud.Visible = true;
                        if (Request.QueryString["SolCompra"] != null)
                        {
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
                            DetalleSolicitud.Columns.Add("Num");
                            DetalleSolicitud.Columns.Add("Descripcion");
                            DetalleSolicitud.Columns.Add("Costo");
                            DetalleSolicitud.Columns.Add("Estatus");
                            DetalleSolicitud.Columns.Add("EstatusStr");

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

        private List<ProductoPrecios> listSource
        {
            get { return (List<ProductoPrecios>)Session["listSource"]; }
            set { Session["listSource"] = value; }
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

        private void CargarValoresCombo()
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

        protected void BuscaCombinado(object sender, EventArgs e)
        {
            string provName = Request.Form[txtBuscaXProvee.UniqueID];
            string provId = Request.Form[hdtxtBuscaProv.UniqueID];

            string prodName = Request.Form[txtBuscaXCodProd.UniqueID];
            string prodId = Request.Form[hdtxtBuscaCodi.UniqueID];

            string SolId = Request.Form[txtBuscaXSolCom.UniqueID];

            if (provName != string.Empty || prodName != string.Empty || SolId != string.Empty)
            {
                this.BuscaSolicitudesCombo(SolId == "" ? 0 : Convert.ToInt32(SolId), prodName == "" ? 0 : Convert.ToInt64(prodId), provName == "" ? 0 : Convert.ToInt32(provId));
            }
            else
            {
                if (provName == string.Empty || prodName == string.Empty || SolId == string.Empty)
                {
                    ListadoSolicitudes();
                }                   
            }

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

        private void BuscaSolicitudesCombo(int SolComId, long ProduId, int ProveId)
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

            rgDetalleSolicitud.Visible = false;
        }
        
        #endregion

        #region Eventos

        protected void cmbCentrosDist_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null) { string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries); Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false); } CN__Comun comun = new CN__Comun(); comun.CambiarCdVer(cmbCentrosDist.SelectedItem, ref sesion);

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
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
                Sesion Sesion = new Sesion();
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
                if (e.CommandName == "Actualizar")
                {
                    Sesion Sesion = new Sesion();
                    Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                    int Id_SolDet = 0;
                    Id_SolDet = Convert.ToInt32(((Label)e.Item.FindControl("lblcve")).Text);

                    // redireccionar a la pagina de autorizar esa solicitud
                    string[] url = Request.Url.ToString().Split(new char[] { '/' });

                    string url2 = Request.Url.ToString().Replace(url[url.Length - 1], "") + "ComprasLocales.aspx?SolCompra=" + Id_SolDet.ToString();

                    Response.Redirect(url2, false);
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        #endregion


        #region ErrorManager


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
                                                                                                                Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
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

        #region EventosWeb

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
                        txtcmd = "spAABuscaProductoTodos '" + prodName + "'";
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

        #endregion


    }
}