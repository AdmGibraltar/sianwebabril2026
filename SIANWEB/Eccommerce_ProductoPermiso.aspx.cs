using CapaEntidad;
using CapaNegocios;
using DevExpress.Export;
using DevExpress.Web;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIANWEB
{
    public partial class Eccommerce_ProductoPermiso : System.Web.UI.Page
    {
        #region Variables 

        public Sesion session
        {
            get
            {
                return (Sesion)Session["Sesion" + Session.SessionID];
            }
            set
            {
                Session["session" + Session.SessionID] = value;

            }
        }

        private string Emp_CnxCentral
        {
            get { return ConfigurationManager.AppSettings.Get("strConnectionCentral"); }
        }
 
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
       
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
            if (Session["ProductoPermiso"] != null)
            {
                List<ProductoPermiso> Lista = (List<ProductoPermiso>)Session["ProductoPermiso"];
                GrdProductoPermiso.DataSource = Lista;
                GrdProductoPermiso.DataBind();
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
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();

                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            //RAM1.ResponseScripts.Add("AbrirContrasenas(); return false");
                            return;
                        }
                        Session["ProductoPermiso"] = null;
                        CargarCentros();
                        cargarDatos();

                    }
                }
            }
            catch (Exception ex)
            {
                warning(ex.Message + "- " + new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        public void cargarDatos()
        { 
            CN_ProductoPermiso CN = new CN_ProductoPermiso();
            ProductoPermiso Datos = new ProductoPermiso();
            List<ProductoPermiso> List = new List<ProductoPermiso>();


            Datos.Id_Cd = session.Id_Cd_Ver;
            CN.ProductoPermiso_Consulta(Datos, ref List, Emp_CnxCentral);
           
            Session["ProductoPermiso"] = List; 
            GrdProductoPermiso.DataSource = List;
            GrdProductoPermiso.DataBind(); 
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

                    //if (Permiso.PGrabar == false)
                    //{
                    //    this.rtb1.Items[6].Visible = false;
                    //}
                    //if (Permiso.PGrabar == false && Permiso.PModificar == false)
                    //{
                    //    this.rtb1.Items[5].Visible = false;
                    //} 
                }
                else
                {
                    Response.Redirect("Inicio.aspx");
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool ValidarSesion()
        {
            try
            {
                if (session == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void BtnPermiso_ServerClick(object sender, EventArgs e)
        {
            try
            {
                int verificador = 0;
                GridViewDataItemTemplateContainer c = ((System.Web.UI.HtmlControls.HtmlButton)sender).NamingContainer as GridViewDataItemTemplateContainer;
                string id_prd = c.Grid.GetRowValues(c.VisibleIndex, "Id_Prd").ToString().Trim();
                string Activo = c.Grid.GetRowValues(c.VisibleIndex, "Activo").ToString().Trim();

                CN_ProductoPermiso CN = new CN_ProductoPermiso();
                ProductoPermiso Datos = new ProductoPermiso();
                List<ProductoPermiso> List = new List<ProductoPermiso>();

                Datos.Id_Prd = Convert.ToInt64(id_prd);
                Datos.Activo = !Convert.ToBoolean(Activo);
                Datos.Id_Cd = session.Id_Cd_Ver;

                if (!_PermisoModificar)
                {
                    info("No tiene permisos para modificar");
                    return;
                }

                Datos.Id_Cd = session.Id_Cd_Ver;
                CN.ProductoPermiso_Actualizar(Datos, ref verificador, Emp_CnxCentral);

                cargarDatos();

                if (!Convert.ToBoolean(Activo))
                {
                    info("El código ha sido activado exitosamente y ahora será visible para los clientes dentro del Portal Key. El cambio se reflejará el día de mañana en el Portal Key.");
                }
                else
                {
                    info("El código ha sido desactivado exitosamente y dejará de ser visible para los clientes dentro del Portal Key. El cambio se reflejará el día de mañana en el Portal Key.");
                }
                
            }
            catch (Exception ex)
            {
                warning("Error al intentar guardar los cambios de los productos");
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


                cargarDatos();
            }
            catch (Exception ex)
            {
                warning(ex.Message.ToString() + "-" + "CmbCentro_TextChanged");
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

        protected void btnExcel_ServerClick(object sender, EventArgs e)
        {
            GrdProductoPermiso.Columns["btnCampo"].Visible = false;
             
            GrdProductoPermiso.ExportXlsToResponse(new XlsExportOptionsEx { ExportType = ExportType.WYSIWYG });

            GrdProductoPermiso.Columns["btnCampo"].Visible = true;
        }
    }
}