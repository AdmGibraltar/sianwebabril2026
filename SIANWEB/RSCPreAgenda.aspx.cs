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
    public class tipobracket
    {
        public string bracket { get; set; }
        public string desc { get; set; }
    }

    public partial class RSCPreAgenda : System.Web.UI.Page
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
            if (Session["PReAgenda"] != null)
            { 
                List<AgendaRsc> Lista = (List<AgendaRsc>)Session["PReAgenda"]; 
                grdPreAgenda.DataSource = Lista;
                grdPreAgenda.DataBind();
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
                    if (ValidarSesion())
                    {
                        if (!IsPostBack)
                        {
                            Session["PReAgenda"] = null; 
                            ValidarPermisos();
                            Llenardatos();
                            cargarBracket();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                warning(ex.Message + "- " + new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }
 
        protected void ButtonExport_ServerClick(object sender, EventArgs e)
        {
            grdPreAgenda.ExportXlsToResponse(new XlsExportOptionsEx { ExportType = ExportType.WYSIWYG }); 
        }

        protected void btnConsulta_ServerClick(object sender, EventArgs e)
        {
            cargarDatos();
        } 

        protected void DllRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Id_CD = session.Id_Cd;
            CargarUsuario(Id_CD);
        }

        protected void Agendar_ServerClick(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer c = ((System.Web.UI.HtmlControls.HtmlButton)sender).NamingContainer as GridViewDataItemTemplateContainer; 
            string ID = c.Grid.GetRowValues(c.VisibleIndex, "ID").ToString().Trim(); 
            ScriptManager.RegisterStartupScript(this, this.GetType(), "abriragendaPedido", "abriragendaPedido('" + ID + "')", true);

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

                pagina.Url = "acys/" + pagina.Url;

                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, Sesion.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;
            }
            catch (Exception ex)
            {
                this.Title = "Error";
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

        private void Llenardatos()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            int Id_CD = session.Id_Cd;
            List<AgendaRsc> Lista = new List<AgendaRsc>();

            cargarTipoUsuario();
            CargarUsuario(Id_CD);
            cargarClientes();


            Fecha.Value = session.CalendarioIni;
            FechaFinal.Value = session.CalendarioFin;
            cargarDatos();
        }

        private void cargarTipoUsuario()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CNRSCAgenda Agenda = new CNRSCAgenda();
            List<Agenda> list_Riks = new List<Agenda>();

            Agenda.ConsultarTipoUsuario(ref list_Riks, session.Emp_Cnx);

            DllRol.DataSource = list_Riks;
            DllRol.ValueField = "id_tu";
            DllRol.TextField = "Descripcion";
            DllRol.DataBind();

            if (Sesion.Id_TU != 15 && Sesion.Id_TU != 16 && Sesion.Id_TU != 31)
            {
                DllRol.Value = "-1";
                DllRol.ReadOnly = false;
                DllRol.Enabled = true;
            }
            else
            {
                if (Sesion.Id_TU == 31)
                {
                    DllRol.Value = "15";
                    DllRol.ReadOnly = true;
                    DllRol.Enabled = false;
                }
                else
                {
                    DllRol.Value = Sesion.Id_TU.ToString();
                    DllRol.ReadOnly = true;
                    DllRol.Enabled = false;
                }
            }
        }

        private void CargarUsuario(int id_Cd)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CNRSCAgenda Rik = new CNRSCAgenda();
            Agenda RegistroRik = new Agenda();

            RegistroRik.Id_Emp = Sesion.Id_Emp;
            RegistroRik.Id_Cd = id_Cd;
            RegistroRik.id_tu = Convert.ToInt32(DllRol.Value.ToString());
            if (Sesion.Id_TU != 15 && Sesion.Id_TU != 16)
            {
                RegistroRik.Id_Usu = -1;
            }
            else
            {
                RegistroRik.Id_Usu = session.Id_U;
            }

            List<Agenda> list_Riks = new List<Agenda>();

            Rik.Consultarusuario(RegistroRik, ref list_Riks, session.Emp_Cnx);

            DllUSuario.DataSource = list_Riks;
            DllUSuario.ValueField = "Id_Usu";
            DllUSuario.TextField = "nombre";
            DllUSuario.DataBind();

            if (Sesion.Id_TU != 15 && Sesion.Id_TU != 16 && Sesion.Id_TU != 31)
            {
                DllUSuario.Value = "-1";
                DllUSuario.ReadOnly = false;
                DllUSuario.Enabled = true;
            }
            else
            {
                DllUSuario.Value = Sesion.Id_U.ToString();
                DllUSuario.ReadOnly = true;
                DllUSuario.Enabled = false;
            }
        }

        private void cargarDatos()
        {
            int Id_CD = session.Id_Cd;
            List<AgendaRsc> Lista = new List<AgendaRsc>();

            AgendaRsc agenda = new AgendaRsc();
            agenda.Id_Emp = session.Id_Emp;
            agenda.Id_Cd = session.Id_Cd;
            agenda.Id_Usu = Convert.ToInt32(DllUSuario.Value.ToString());
            agenda.id_tu = Convert.ToInt32(DllRol.Value.ToString());
            agenda.id_cte = Convert.ToInt32(DllCLiente.Value.ToString());
            agenda.id_ActividadGral = -1;
            agenda.id_Actividad = -1;
            agenda.FechaInicio = Convert.ToDateTime(Fecha.Text);
            agenda.fechaFinal = Convert.ToDateTime(FechaFinal.Text);

            CNRSCAgenda CN = new CNRSCAgenda();
            CN.CargarPreagenda(agenda, ref Lista, session.Emp_Cnx);
            Session["PReAgenda"] = Lista;
            grdPreAgenda.DataSource = Lista;
            grdPreAgenda.DataBind(); 
        }

        public void cargarBracket()
        {
            List<tipobracket> bracket = new List<tipobracket>();
            tipobracket registro = new tipobracket(); 
            registro.bracket = "A";
            registro.desc = "Cliente 80/20";
            bracket.Add(registro);

            registro = new tipobracket(); 
            registro.bracket = "B";
            registro.desc = "Potencial a Integralizar";
            bracket.Add(registro); 

            registro = new tipobracket();
            registro.bracket = "C";
            registro.desc = "Venta a Mostrador/Pulverizado";
            bracket.Add(registro);

            registro = new tipobracket();
            registro.bracket = "CN";
            registro.desc = "Cuenta Nacional";
            bracket.Add(registro);

            registro = new tipobracket();
            registro.bracket = "D";
            registro.desc = "Cliente Nuevo";
            bracket.Add(registro);

            grdBrakets.DataSource = bracket;
            grdBrakets.DataBind();
        }

        protected void DllUSuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarClientes();
        }

        private void cargarClientes()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            AgendaRsc Agenda = new AgendaRsc();
            List<Cliente> lista = new List<Cliente>();
            CN_CapPedidoVtaInst vtaInst = new CN_CapPedidoVtaInst();
            Agenda.Id_Emp = sesion.Id_Emp;
            Agenda.Id_Cd = sesion.Id_Cd_Ver;
            Agenda.Id_Usu = Convert.ToInt32(DllUSuario.Value.ToString());
            CN_CapPedido cn_capPedido = new CN_CapPedido();
            vtaInst.ConsultarAgendaCliente(Agenda, ref lista, sesion.Emp_Cnx);

            var query = (from tlist in lista
                         group tlist by tlist.idCte into g
                         select new
                         {
                             Id_Cte = g.Key,
                             nombre = g.Select(x => x.nombre).FirstOrDefault()
                         }).ToList();

            DllCLiente.DataSource = query;
            DllCLiente.ValueField = "Id_Cte";
            DllCLiente.TextField = "nombre";
            DllCLiente.DataBind();

            DllCLiente.Value = "0";
        }

        protected void btnCancelar_ServerClick(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer c = ((System.Web.UI.HtmlControls.HtmlButton)sender).NamingContainer as GridViewDataItemTemplateContainer;
            string ID = c.Grid.GetRowValues(c.VisibleIndex, "ID").ToString().Trim();

            int verificacion = 0;

            CNRSCAgenda CN = new CNRSCAgenda();
            AgendaRsc agenda = new AgendaRsc();
            agenda.ID = Convert.ToInt32(ID);
            agenda.Id_Emp = session.Id_Emp;
            agenda.Id_Cd = session.Id_Cd;

            CN.PreagendaBaja(agenda, ref verificacion, session.Emp_Cnx);

            if (verificacion == 1)
            {
                cargarDatos();
            }
        }
    }
}