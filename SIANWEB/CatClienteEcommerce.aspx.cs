using CapaEntidad;
using CapaNegocios;
using DevExpress.Export;
using DevExpress.Utils;
using DevExpress.Web;
using DevExpress.Web.Bootstrap;
using DevExpress.XtraPrinting;
using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace SIANWEB
{
    public partial class CatClienteEcommerce : System.Web.UI.Page
    {

        #region Variables
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private Sesion sesion { get { return (Sesion)Session["Sesion" + Session.SessionID]; } set { Session["Sesion" + Session.SessionID] = value; } }



        private string Emp_CnxCentral
        {
            get { return ConfigurationManager.AppSettings.Get("strConnectionCentral"); }
        }



        #endregion

        #region Mensajes 


        private void ShowMessage(string Message, string type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }
        private void sucess(string mensaje)
        {
            ShowMessage(mensaje, "Success");
        }
        private void danger(string mensaje)
        {
            ShowMessage(mensaje, "Error");
        }
        private void warning(string mensaje)
        {
            ShowMessage(mensaje, "Warning");
        }
        private void info(string mensaje)
        {
            ShowMessage(mensaje, "Info");
        }

        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            List<Portakey> listaClienteECommerce = new List<Portakey>();

            if (Session["listaClienteECommerce"] != null)
            {

                listaClienteECommerce = (List<Portakey>)Session["listaClienteECommerce"];

                grdAdmon.DataSource = listaClienteECommerce;
                grdAdmon.DataBind();
            }

        }
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
                    if (!IsPostBack)
                    {
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            //RAM1.ResponseScripts.Add("AbrirContrasenas(); return false");
                            return;
                        }

                        ValidarPermisos();
                        Session["listaClienteECommerce"] = null;
                        grdAdmon.DataSource = null;
                        grdAdmon.DataBind();
                        CargarCentros();
                        CargarGrid();
                    }
                }
            }
            catch (Exception ex)
            {
                warning(ex.Message + "- " + new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
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
                    CN_Comun.DevLlenaCombo(2, Sesion.Id_Emp, Sesion.Id_U, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    CmbCentro.Visible = false;
                    CmbCentro.Value = (Sesion.Id_Cd_Ver.ToString());
                    TblEncabezado.Text = "Centro de distribución: " + CmbCentro.Text;

                }
                else
                {
                    CN_Comun.DevLlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, Sesion.Id_Cd_Ver, Sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.Value = Sesion.Id_Cd_Ver.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void CmbCentro_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }
                CN__Comun comun = new CN__Comun();
                comun.BoostrapCambiarCdVer(CmbCentro, ref sesion);


                CargarGrid();
            }
            catch (Exception ex)
            {
                warning(ex.Message.ToString() + "-" + "CmbCentro_TextChanged");
            }
        }


        private void ValidarPermisos()
        {
            try
            {
                if (sesion != null)
                {
                    Session["guardar"] = null;
                    Session["modificar"] = null;
                    Pagina pagina = new Pagina();
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                    if (pag.Length > 1)
                        pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                    else
                        pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;
                    CN_Pagina CapaNegocio = new CN_Pagina();
                    CapaNegocio.PaginaConsultar(ref pagina, sesion.Emp_Cnx);

                    Session["Head" + Session.SessionID] = pagina.Path;
                    this.Title = pagina.Descripcion;
                    Permiso Permiso = new Permiso();
                    Permiso.Id_U = sesion.Id_U;
                    Permiso.Id_Cd = sesion.Id_Cd;
                    Permiso.Sm_cve = pagina.Clave;
                    //Esta clave depende de la pantalla
                    CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                    CN_PermisosU.ValidaPermisosUsuario(ref Permiso, sesion.Emp_Cnx);
                    _PermisoGuardar = false;
                    _PermisoModificar = false;
                    if (Permiso.PAccesar == true)
                    {
                        _PermisoGuardar = Permiso.PGrabar;
                        _PermisoModificar = Permiso.PModificar;
                        Session["guardar"] = Permiso.PGrabar;
                        Session["modificar"] = Permiso.PModificar;
                    }
                    else
                    {
                        Response.Redirect("Inicio.aspx");
                    }

                }
                else
                {
                    Response.Redirect("login.aspx");
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ValidarSesion()
        {
            try
            {
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                    return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarGrid()
        {
            List<Portakey> List = new List<Portakey>();
            Portakey Registro = new Portakey();
            Registro.id_Cd = sesion.Id_Cd_Ver;
            Registro.Id_Emp = sesion.Id_Emp;

            CN_ClienteEcommerce cn = new CN_ClienteEcommerce();
            cn.ConsultaLista(Registro, ref List, sesion.Emp_Cnx);

            Session["listaClienteECommerce"] = List;
            grdAdmon.DataSource = List;
            grdAdmon.DataBind();
        }


        protected void btnEditar_ServerClick(object sender, EventArgs e)
        {
            BootstrapButton btn = sender as BootstrapButton;
            GridViewDataItemTemplateContainer container = btn.NamingContainer as GridViewDataItemTemplateContainer;
            int index = container.VisibleIndex;
            string Id = DataBinder.Eval(container.DataItem, "Id_Portal").ToString();
            string Tipo = DataBinder.Eval(container.DataItem, "Tipo").ToString();
            string Cdi = DataBinder.Eval(container.DataItem, "id_Cd").ToString();

            if (_PermisoModificar)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "EditarMAtriz", "Editar('" + Id + "', '" + Tipo + "', '" + Cdi + "')", true);
            }
            else
            {
                info("Sin permiso para modificar información");
            }
        }

        protected void btnInsert_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirModalDetalles", "AbrirModalDetalles()", true);

        }

        protected void btnExcel_ServerClick(object sender, EventArgs e)
        {
            grdAdmon.Columns["Editar"].Visible = false;
            grdAdmon.Columns["Region"].Visible = false;
            grdAdmon.Columns["Unidades"].Visible = false;
            grdAdmon.Columns["Permisos"].Visible = false;
            grdAdmon.Columns["Eliminar"].Visible = false;
           
            grdAdmon.ExportXlsToResponse(new XlsExportOptionsEx { ExportType = ExportType.WYSIWYG });

            grdAdmon.Columns["Editar"].Visible = true;
            grdAdmon.Columns["Region"].Visible = true;
            grdAdmon.Columns["Unidades"].Visible = true;
            grdAdmon.Columns["Permisos"].Visible = true;
            grdAdmon.Columns["Eliminar"].Visible = true;
        } 
       

        protected void BtnUsuarios_ServerClick(object sender, EventArgs e)
        {
            BootstrapButton btn = sender as BootstrapButton;
            GridViewDataItemTemplateContainer container = btn.NamingContainer as GridViewDataItemTemplateContainer;
            int index = container.VisibleIndex;
            string Id = DataBinder.Eval(container.DataItem, "Id_Portal").ToString();
            string Tipo = DataBinder.Eval(container.DataItem, "Tipo").ToString();
            string Cdi = DataBinder.Eval(container.DataItem, "id_Cd").ToString();

            if (Tipo == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirModalDetalles", "AbrirModalDetallesUpdate('" + Id + "')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "EditarUsuCli", "UsuarioClientes('" + Id + "', '" + Tipo + "', '" + Cdi + "')", true);
            }
        }

        protected void BtnRegion_ServerClick(object sender, EventArgs e)
        {
            BootstrapButton btn = sender as BootstrapButton;
            GridViewDataItemTemplateContainer container = btn.NamingContainer as GridViewDataItemTemplateContainer;
            int index = container.VisibleIndex;
            string Id = DataBinder.Eval(container.DataItem, "Id_Portal").ToString();
            string Tipo = DataBinder.Eval(container.DataItem, "Tipo").ToString();
            string Cdi = DataBinder.Eval(container.DataItem, "id_Cd").ToString();


            ScriptManager.RegisterStartupScript(this, this.GetType(), "EditarRegion", "Region('" + Id + "', '" + Tipo + "', '" + Cdi + "')", true);

        }

        protected void BtnPermisos_ServerClick(object sender, EventArgs e)
        {
            BootstrapButton btn = sender as BootstrapButton;
            GridViewDataItemTemplateContainer container = btn.NamingContainer as GridViewDataItemTemplateContainer;
            int index = container.VisibleIndex;
            string Id = DataBinder.Eval(container.DataItem, "Id_Portal").ToString();
            string Tipo = DataBinder.Eval(container.DataItem, "Tipo").ToString();
            string Cdi = DataBinder.Eval(container.DataItem, "id_Cd").ToString();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Permisos", "Permisos('" + Id + "', '" + Tipo + "', '" + Cdi + "')", true);

        }

        protected void Btneliminar_ServerClick(object sender, EventArgs e)
        {
            try
            {
                BootstrapButton btn = sender as BootstrapButton;
                GridViewDataItemTemplateContainer container = btn.NamingContainer as GridViewDataItemTemplateContainer;
                int index = container.VisibleIndex; 
                string Id = DataBinder.Eval(container.DataItem, "Id_Portal").ToString().Trim();
                string Tipo = DataBinder.Eval(container.DataItem, "Tipo").ToString().Trim();

                Session["PKId"] = Id;
                Session["PKTipo"] = Tipo;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Confirmacion", "Confirmacion('" + Id + "')", true);


 
                //int verificador = 0;
                //CN_PortalKey Cn = new CN_PortalKey();
                //Portakey Datos = new Portakey();
                //List<Portakey> lista = new List<Portakey>();

                //Datos.Id_Emp = sesion.Id_Emp;
                //Datos.id_Cd = sesion.Id_Cd;
                //Datos.Id_Portal = Convert.ToInt32(Id);
                //Datos.Tipo = Convert.ToInt32(Tipo);

                //Cn.EliminarMAtriz(Datos, ref verificador, Emp_CnxCentral);

                //if (verificador != 0)
                //{
                //    info("Se Elimino la Información Correctamente");
                //}
            }
            catch (Exception ex)
            {
                warning("Error al intentar Eliminar Datos.");
            }
        }

        protected void BtnNuevo_ServerClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "NuevaMAtriz", "Nuevo()", true);
        }

        protected void BtnRegion_Init(object sender, EventArgs e)
        {
            BootstrapButton btn = sender as BootstrapButton;
            GridViewDataItemTemplateContainer container = btn.NamingContainer as GridViewDataItemTemplateContainer;
            int index = container.VisibleIndex;
            string Matriz = DataBinder.Eval(container.DataItem, "Tipo").ToString();  
             
            if (Matriz.ToUpper() == "1" ||   Matriz.ToUpper() == "4")
            {
                btn.Visible = false;
            }
            else
            {
                btn.Visible = true;
            }
        }

        protected void btnEditar_Init(object sender, EventArgs e)
        {
            BootstrapButton btn = sender as BootstrapButton;
            GridViewDataItemTemplateContainer container = btn.NamingContainer as GridViewDataItemTemplateContainer;
            int index = container.VisibleIndex;
            string Matriz = DataBinder.Eval(container.DataItem, "Tipo").ToString();

            if (Matriz.ToUpper() == "1" ||  Matriz.ToUpper() == "4")
            {
                btn.Visible = false;
            }
            else
            {
                btn.Visible = true;
            }
        }

        protected void BtnUsuarios_Init(object sender, EventArgs e)
        {
            BootstrapButton btn = sender as BootstrapButton;
            GridViewDataItemTemplateContainer container = btn.NamingContainer as GridViewDataItemTemplateContainer;
            int index = container.VisibleIndex;
            string Matriz = DataBinder.Eval(container.DataItem, "Tipo").ToString();

           
        }

        protected void BtnPermisos_Init(object sender, EventArgs e)
        {
            BootstrapButton btn = sender as BootstrapButton;
            GridViewDataItemTemplateContainer container = btn.NamingContainer as GridViewDataItemTemplateContainer;
            int index = container.VisibleIndex;
            string Matriz = DataBinder.Eval(container.DataItem, "Tipo").ToString();

            if (Matriz.ToUpper() == "1")
            {
                btn.Visible = false;
            }
            else
            {
                btn.Visible = true;
            }
        }

        protected void BtnEliminar_Init(object sender, EventArgs e)
        {
            BootstrapButton btn = sender as BootstrapButton;
            GridViewDataItemTemplateContainer container = btn.NamingContainer as GridViewDataItemTemplateContainer;
            int index = container.VisibleIndex;
            string Matriz = DataBinder.Eval(container.DataItem, "Tipo").ToString();

            if (Matriz.ToUpper() == "3" || Matriz.ToUpper() == "4")
            {
                btn.Visible = false;
            }
            else
            {
                btn.Visible = true;
            }
        }


        protected void BtneliminarConfirmacion_Click(object sender, EventArgs e)
        {
            try
            {
                int Id = Convert.ToInt32(Session["PKId"].ToString());
                int tipo = Convert.ToInt32(Session["PKTipo"].ToString());
 
                int verificador = 0;
                CN_PortalKey Cn = new CN_PortalKey();
                Portakey Datos = new Portakey();
                List<Portakey> lista = new List<Portakey>();

                Datos.Id_Emp = sesion.Id_Emp;
                Datos.id_Cd = sesion.Id_Cd_Ver;
                Datos.Id_Portal = Convert.ToInt32(Id);
                Datos.Tipo = Convert.ToInt32(tipo);

                Cn.EliminarMAtriz(Datos, ref verificador, sesion.Emp_Cnx);

                if (verificador != 0)
                {
                    info("Se Elimino la Información Correctamente");
                    CargarGrid();
                }
            }
            catch (Exception ex)
            {
                warning("Error al intentar Eliminar Datos.");
            }
        }
    }
}