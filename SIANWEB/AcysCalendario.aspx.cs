using CapaEntidad;
using CapaNegocios;
using DevExpress.Export;
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
    public partial class AcysCalendario : System.Web.UI.Page
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
            if (Session["CalendarioAcys"] != null)
            {
                List<CapaEntidad.AcysCalendario> ActividadesRSC = (List<CapaEntidad.AcysCalendario>)Session["CalendarioAcys"];
                GrdCalendarioAcys.DataSource = ActividadesRSC;
                GrdCalendarioAcys.DataBind();

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (session == null)
                {
                    if (!Page.IsCallback)
                    {
                        string[] pag = Page.Request.Url.ToString().Trim().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                        Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                        Response.Redirect("login.aspx", false);
                    }
                }
                else
                {
                    if (!IsPostBack)
                    {
                        Session["CalendarioAcys"] = null;
                        GrdCalendarioAcys.DataSource = null;
                        GrdCalendarioAcys.DataBind();

                        DEFechaAnio.Value = DateTime.Now;
                    }
                }
            }
            catch (Exception ex)
            {
                warning(ex.Message + "- " + new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
            Session["activeMenu"] = 5;
        }

        protected void BtnConsultar_ServerClick(object sender, EventArgs e)
        {
            if (SEAcys.Value == null)
            {
                info("Favor de Completar el Campo de ACyS");
            }

            List<CapaEntidad.AcysCalendario> Lista = new List<CapaEntidad.AcysCalendario>();

            CapaEntidad.AcysCalendario Datos = new CapaEntidad.AcysCalendario();
            CN_AcysCalendario CN = new CN_AcysCalendario();
            Datos.Id_Emp = session.Id_Emp;
            Datos.Id_Cd = session.Id_Cd;
            Datos.Id_Acs = Convert.ToInt32(SEAcys.Value.ToString());
            Datos.Acs_Anio = Convert.ToInt32(DEFechaAnio.Text.ToString());
            if (SEProducto.Value != null)
            {
                Datos.Id_Prd = Convert.ToInt64(SEProducto.Value.ToString());
            }

            CN.spAcuerdosExistentesPorCliente(Datos, ref Lista, session.Emp_Cnx);
            Session["CalendarioAcys"] = Lista;
            GrdCalendarioAcys.DataSource = Lista;
            GrdCalendarioAcys.DataBind();
        }

        protected void btnExcel_ServerClick(object sender, EventArgs e)
        {
            GrdCalendarioAcys.ExportXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
        }
    }
}