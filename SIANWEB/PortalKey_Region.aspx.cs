using CapaDatos;
using CapaEntidad;
using CapaNegocios;
using DevExpress.Web;
using DevExpress.Web.Bootstrap;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIANWEB
{
    public partial class PortalKey_Region : System.Web.UI.Page
    {
        #region Variables

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

        private string Emp_CnxCentral
        {
            get { return ConfigurationManager.AppSettings.Get("strConnectionCentral"); }
        }



        #endregion Variables

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
            if (Session["PortalRegion"] != null)
            {
                List<Portakey> Lista = (List<Portakey>)Session["PortalRegion"];
                GrdRegion.DataSource = Lista;
                GrdRegion.DataBind();

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
                        Session["PortalRegion"] = null;
                        GrdRegion.DataSource = null;
                        GrdRegion.DataBind();
                        if (Request.QueryString["Id"] != null)
                        {
                            int tipo = Convert.ToInt32(Request.QueryString["Tipo"].ToString());
                            int id_Cd = Convert.ToInt32(Request.QueryString["IdCD"].ToString());
                            int id = Convert.ToInt32(Request.QueryString["Id"].ToString());
                            consultar(id, tipo, id_Cd);
                            cargargrid(id, tipo, id_Cd);
                            Session["IdRegion"] = null;

                            if (tipo != 2)
                            {
                                BtnGuardar.Visible = false;
                                BtnGuardar.Enabled = false;

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                danger(string.Concat(ex.Message, "Page_Load_error"));
            }
        }

        public void consultar(int Id, int Tipo, int id_Cd)
        {
            List<Portakey> List = new List<Portakey>();
            Portakey Registro = new Portakey();
            Registro.Id_Emp = sesion.Id_Emp;
            Registro.Id_Portal = Id;
            Registro.Tipo = Tipo;
            Registro.id_Cd = id_Cd;
            CN_PortalKey cn = new CN_PortalKey();
            cn.ConsultarMAtriz(Registro, ref List, sesion.Emp_Cnx);

            if (List.Count > 0)
            {
                TxtMatriz.Value = List.First().NombreMatriz;
                TxtCorreo.Text = List.First().Correo;
            }
        }

        private void cargargrid(int id, int tipo, int id_Cd)
        {
            List<Portakey> List = new List<Portakey>();
            Portakey Registro = new Portakey();
            Registro.Id_Emp = sesion.Id_Emp;
            Registro.Id_Portal = id;
            Registro.Tipo = tipo;
            Registro.id_Cd = id_Cd;

            CN_PortalKey cn = new CN_PortalKey();
            cn.ConsultarRegion(Registro, ref List, sesion.Emp_Cnx);

            Session["PortalRegion"] = List;
            GrdRegion.DataSource = List;
            GrdRegion.DataBind();
        }

        protected void BtnEditar_ServerClick(object sender, EventArgs e)
        {
            BootstrapButton btn = sender as BootstrapButton;
            GridViewDataItemTemplateContainer container = btn.NamingContainer as GridViewDataItemTemplateContainer;
            int index = container.VisibleIndex;


            string Id = DataBinder.Eval(container.DataItem, "Id_Region").ToString().Trim();
            string nombre = DataBinder.Eval(container.DataItem, "NombreRegion").ToString().Trim();

            Session["IdRegion"] = Id;
            TxtRegion.Text = nombre;
        }

        protected void Btneliminar_ServerClick(object sender, EventArgs e)
        {
            try
            {
                BootstrapButton btn = sender as BootstrapButton;
                GridViewDataItemTemplateContainer container = btn.NamingContainer as GridViewDataItemTemplateContainer;
                int index = container.VisibleIndex;


                string Id = DataBinder.Eval(container.DataItem, "Id_Region").ToString().Trim();
                string nombre = DataBinder.Eval(container.DataItem, "NombreRegion").ToString().Trim();

                int verificador = 0;
                CN_PortalKey Cn = new CN_PortalKey();
                Portakey Datos = new Portakey();
                List<Portakey> lista = new List<Portakey>();

                int tipo = Convert.ToInt32(Request.QueryString["Tipo"].ToString());
                int id_Cd = Convert.ToInt32(Request.QueryString["IdCD"].ToString());
                int id_portal = Convert.ToInt32(Request.QueryString["Id"].ToString());

                Datos.Id_Emp = sesion.Id_Emp;
                Datos.Id_Portal = Convert.ToInt32(id_portal);
                Datos.Id_Region = Convert.ToInt32(Id);

                Cn.EliminarRegion(Datos, ref verificador, sesion.Emp_Cnx);

                if (verificador != 0)
                {
                    info("Se Elimino la Información Correctamente");
                    cargargrid(id_portal, tipo, id_Cd);
                    Limpiar();
                }
            }
            catch (Exception ex)
            {
                warning("Error al intentar Eliminar Datos.");
            }
        }

        protected void BtnGuardar_ServerClick(object sender, EventArgs e)
        {
            try
            {

                int verificador = 0;
                string mensajeInfo = "";

                int tipo = Convert.ToInt32(Request.QueryString["Tipo"].ToString());
                int id_Cd = Convert.ToInt32(Request.QueryString["IdCD"].ToString());
                int id_portal = Convert.ToInt32(Request.QueryString["Id"].ToString());

                CN_PortalKey Cn = new CN_PortalKey();
                Portakey Datos = new Portakey();
                List<Portakey> lista = new List<Portakey>();

                if (TxtRegion.Text == "")
                {
                    info("Favor de capturar la Región");
                    return;
                }

                Datos.Id_Emp = sesion.Id_Emp;
                Datos.Id_Portal = id_portal;
                Datos.NombreRegion = TxtRegion.Text;


                List<Portakey> Lista = new List<Portakey>();


                if (mensajeInfo != "")
                {
                    info(mensajeInfo);
                    return;
                }

                if (Session["IdRegion"] != null)
                {
                    string Id = Session["IdRegion"].ToString();
                    Datos.Id_Region = Convert.ToInt32(Id);
                    Cn.ModificarRegion(Datos, ref verificador, sesion.Emp_Cnx);

                    if (verificador != 0)
                    {
                        info("Se Actualizo la Información Correctamente");
                        Session["IdRegion"] = null;
                        cargargrid(id_portal, tipo, id_Cd);
                        Limpiar();
                    }
                }
                else
                {
                    Cn.InsertarRegion(Datos, ref verificador, sesion.Emp_Cnx);

                    if (verificador != 0)
                    {
                        info("Se guardo la Información Correctamente");
                        cargargrid(id_portal, tipo, id_Cd);
                        Limpiar();
                    }
                }
            }
            catch (Exception ex)
            {
                warning(ex.Message);
            }
        }

        public void Limpiar()
        {
            TxtRegion.Text = "";
        }

        protected void BtnEditar_Init(object sender, EventArgs e)
        {
            BootstrapButton btn = sender as BootstrapButton;
            GridViewDataItemTemplateContainer container = btn.NamingContainer as GridViewDataItemTemplateContainer;
            int index = container.VisibleIndex;
            string tipo = Request.QueryString["Tipo"].ToString();

            if (tipo.ToUpper() == "1" || tipo.ToUpper() == "3" || tipo.ToUpper() == "4")
            {
                btn.Visible = false;
            }
            else
            {
                btn.Visible = true;
            }
        }

        protected void Btneliminar_Init(object sender, EventArgs e)
        {
            BootstrapButton btn = sender as BootstrapButton;
            GridViewDataItemTemplateContainer container = btn.NamingContainer as GridViewDataItemTemplateContainer;
            int index = container.VisibleIndex;
            string tipo = Request.QueryString["Tipo"].ToString();

            if (tipo.ToUpper() == "1" || tipo.ToUpper() == "3" || tipo.ToUpper() == "4")
            {
                btn.Visible = false;
            }
            else
            {
                btn.Visible = true;
            }
        }
    }
}