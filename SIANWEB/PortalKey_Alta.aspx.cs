using CapaEntidad;
using CapaNegocios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIANWEB
{
    public partial class PortalKey_Alta : System.Web.UI.Page
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


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["portalpass"] = null;
                inicializar();
            }
        }


        private void inicializar()
        {
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            
            if (Request.QueryString["Id"] != null)
            {
                consultar(Convert.ToInt32(Request.QueryString["Id"].ToString()));
            }


        }

        public void consultar(int Id)
        {
            List<Portakey> List = new List<Portakey>();
            Portakey Registro = new Portakey();
            Registro.Id_Emp = sesion.Id_Emp;
            Registro.Id_Portal = Id;
            Registro.Tipo = Convert.ToInt32(Request.QueryString["Tipo"].ToString());
            Registro.id_Cd = Convert.ToInt32(Request.QueryString["IdCd"].ToString());
            CN_PortalKey cn = new CN_PortalKey();
            cn.ConsultarMAtriz(Registro, ref List, sesion.Emp_Cnx);

            if (List.Count > 0)
            {
                TxtMAtriz.Value = List.First().NombreMatriz;
                txtUsuario.Text = List.First().Correo;
                TxtNombre.Text = List.First().nombre;
                txtApellido.Text = List.First().Apellidos;
            }

            if (Convert.ToInt32(Request.QueryString["Tipo"].ToString()) != 2)
            {
                BTNGuardar.Enabled = false;
                BTNGuardar.Visible = false;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (TxtNombre.Text.Trim().Length == 0 || txtApellido.Text.Trim().Length == 0)
                {
                    info("Favor de Capturar Todos los Datos.");
                    return;
                }

                int verificador = 0;
                string mensajeInfo = "";

                CN_PortalKey Cn = new CN_PortalKey();
                Portakey Datos = new Portakey();
                List<Portakey> lista = new List<Portakey>();
                List<Portakey> listaCorreo = new List<Portakey>();
                string conexion = WebConfigurationManager.AppSettings["strConnectionCentral"];

                Datos.Id_Emp = sesion.Id_Emp;
                Datos.id_Cd = sesion.Id_Cd;
                Datos.Id_Usu = sesion.Id_U;
                Datos.NombreMatriz = TxtMAtriz.Text;
                Datos.Correo = txtUsuario.Text; 
                Datos.Tipo = 2; 
                Datos.id_cte = 0;
                Datos.nombre = TxtNombre.Text.Trim();
                Datos.Apellidos = txtApellido.Text.Trim();
                if (Request.QueryString["Id"] != null)
                {
                    Datos.Id_Portal = Convert.ToInt32(Request.QueryString["Id"].ToString());
                }
                List<Portakey> Lista = new List<Portakey>();

                Cn.ConsultarCorreo(Datos, ref listaCorreo, conexion);
                if (listaCorreo.Count > 0)
                {
                    info("El usuario no puede capturarse, el correo ya esta siendo utilizado por el cliente para el acceso al portal.");
                    return;
                } 
                
                if (mensajeInfo != "")
                {
                    info(mensajeInfo);
                    return;
                }

                if (Request.QueryString["Id"] != null)
                {
                    Datos.Id_Portal = Convert.ToInt32(Request.QueryString["Id"].ToString());
                    Cn.ModificarMAtriz(Datos, ref verificador, sesion.Emp_Cnx);

                    if (verificador != 0)
                    {
                        sucess("Se Actualizo la Información Correctamente");
                    }
                }
                else
                {
                    Cn.InsertarMAtriz(Datos, ref verificador, sesion.Emp_Cnx);

                    if (verificador != 0)
                    {
                        sucess("Se guardo la Información Correctamente");
                    }
                } 
            }
            catch (Exception ex)
            {
                warning(ex.Message);
            }
        } 
    }
}