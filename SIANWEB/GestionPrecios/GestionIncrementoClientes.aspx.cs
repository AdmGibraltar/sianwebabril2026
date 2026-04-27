using CapaEntidad;
using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace SIANWEB.GestionPrecios
{
    public partial class GestionIncrementoClientes : System.Web.UI.Page
    {
        #region variables
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private Sesion sesion { get { return (Sesion)Session["Sesion" + Session.SessionID]; } set { Session["Sesion" + Session.SessionID] = value; } }

        public int Usuario_Tipo;
        public int Id_TU; // Tipo Usuario 3.- Gerente         
        public int Id_Rik; // Representante Institucional Key RIK , para recibir parametro 
        public int Id_CD;
        public int Id_U;
        public string CDI_Nombre;
        public string Fecha;
        #endregion variables 
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["dtGestionincrementoclientes"] != null)
            {
                Session["dtGestionincrementoclientes"] = null;
            }
          

            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (Sesion == null)
            {
                Response.Redirect(ResolveUrl("~/Login.aspx"));
            }
            hifId_Rik.Value = sesion.Id_TU == 2 ?  sesion.Id_Rik.ToString()  : "0";
            hId_Tu.Value =    sesion.Id_TU.ToString() ;
            Usuario_Tipo = sesion.Id_TU;
            Id_TU = sesion.Id_TU;
            Id_CD = sesion.Id_Cd;
            if (sesion.Id_Rik < 0 )
            {
                Id_Rik = 0;
            }
            else
            {
                Id_Rik = sesion.Id_Rik;
            }
            Id_U = sesion.Id_U;
            CDI_Nombre = sesion.Cd_Nombre;
            Fecha = sesion.CalendarioIni.ToString("MM/dd/yyyy");

            if (IsPostBack) return;

            

        }


        [System.Web.Services.WebMethod]
        public static string ObtenerFechaCierreIncremento()
        {
 

            string fechaCierre = "";
            string connectionString = System.Configuration.ConfigurationManager.AppSettings["strConnectionSIANCentral"];
            using (var cn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("SELECT TOP 1 valor FROM CatPreciadorConfiguracion WHERE Id_Configuracion = 9", cn))
            {
                cn.Open();
                var result = cmd.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int valorFecha))
                {
                    // Convierte el int yyyymmdd a DateTime
                    string valorStr = valorFecha.ToString();
                    if (valorStr.Length == 8)
                    {
                        int anio = int.Parse(valorStr.Substring(0, 4));
                        int mes = int.Parse(valorStr.Substring(4, 2));
                        int dia = int.Parse(valorStr.Substring(6, 2));
                        var fecha = new DateTime(anio, mes, dia);
                        fechaCierre = fecha.ToString("yyyy-MM-dd"); // O el formato que prefieras
                    }
                }
            }
            return fechaCierre;
        }

    }
}

