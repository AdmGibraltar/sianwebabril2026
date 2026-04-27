using CapaEntidad;
using CapaNegocios;
using DevExpress.Web.Bootstrap;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIANWEB
{
    public partial class RSCAltaActividad : System.Web.UI.Page
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
            if (Session["AltaAtividadAsesor"] != null)
            {
                List<AgendaRsc> Lista = (List<AgendaRsc>)Session["AltaAtividadAsesor"];
                grdActividadesAsesor.DataSource = Lista;
                grdActividadesAsesor.DataBind();
            }
            if (Session["AltaAtividadRSC"] != null)
            {
                List<AgendaRsc> Lista = (List<AgendaRsc>)Session["AltaAtividadRSC"];
                grdActividadesRsc.DataSource = Lista;
                grdActividadesRsc.DataBind();
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
                            Session["AltaAtividadAsesor"] = null;
                            Session["AltaAtividadRSC"] = null;
                            ValidarPermisos();
                            Llenardatos();
                            cargarDatos();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                warning(ex.Message + "- " + new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
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

            if (Sesion.Id_TU != 15 && Sesion.Id_TU != 16)
            {
                DllRol.Value = "-1";
                DllRol.ReadOnly = false;
                DllRol.Enabled = true;
            }
            else
            {
                DllRol.Value = Sesion.Id_TU.ToString();
                DllRol.ReadOnly = true;
                DllRol.Enabled = false;
            }
            if (DllUSuario.Value != null)
            {
                if (DllUSuario.Value.ToString() != "-1")
                {
                    Agenda lista = new Agenda();
                    List<Agenda> listaTipo = new List<Agenda>();
                    lista.Id_Usu = Convert.ToInt32(DllUSuario.Value.ToString());

                    Agenda.TipoUsuarioConsultar(lista, ref listaTipo, session.Emp_Cnx);
                    if (listaTipo.Count() > 0)
                    {
                        DllRol.Value = listaTipo.First().id_tu.ToString();
                        DllRol.ReadOnly = true;
                        DllRol.Enabled = false;
                    }
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

            if (Sesion.Id_TU != 15 && Sesion.Id_TU != 16)
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

        private void ConsularUsuario(ref List<Agenda> list_Riks, int id_Cd)
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

            Rik.Consultarusuario(RegistroRik, ref list_Riks, session.Emp_Cnx); 
        }

        protected void DllRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Id_CD = session.Id_Cd;
            CargarUsuario(Id_CD);
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

        protected void DllUSuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarTipoUsuario();
        }

        protected void BtnGuardar_ServerClick(object sender, EventArgs e)
        {
            if (DllRol.Value.ToString() != "-1")
            {
                if (DllUSuario.Value.ToString() == "-1")
                {
                    int Id_CD = session.Id_Cd;
                    List<Agenda> list_Riks = new List<Agenda>();
                    ConsularUsuario(ref list_Riks, Id_CD);

                    foreach(Agenda registro in list_Riks)
                    {
                        if (registro.Id_Usu.ToString() != "-1")
                        {
                            int Id_usu = Convert.ToInt32(registro.Id_Usu.ToString());
                            guardarPermisosActividad(Id_usu);
                        }
                    }
                    sucess("Se guardo la información correctamente");
                    cargarDatos();
                }
                else
                {
                    if (DllUSuario.Value.ToString() != "-1")
                    {
                        int Id_usu = Convert.ToInt32(DllUSuario.Value.ToString());
                        guardarPermisosActividad(Id_usu);
                    }
                    sucess("Se guardo la información correctamente");
                    cargarDatos();
                }
            }
            else
            {
                info("Favor de seleccionar un usuario y/o rol");
            }

           
        }

        public void guardarPermisosActividad(int id_usu)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CNRSCAgenda CN = new CNRSCAgenda();
            AgendaRsc agenda = new AgendaRsc();

            int verificador = 0;

            agenda.Id_Usu = id_usu;
            CN.ActHibridasBaja(agenda, ref verificador, Sesion.Emp_Cnx);

            if (verificador == 1)
            {
                for (var i = 0; i < grdActividadesAsesor.VisibleRowCount; i++)
                {
                    if (grdActividadesAsesor.GetRowValues(i) != null)
                    {
                        if (grdActividadesAsesor.Selection.IsRowSelected(i))
                        {
                            try
                            {
                                agenda = new AgendaRsc();
                                agenda.id_ActividadGral = Convert.ToInt32(grdActividadesAsesor.GetRowValues(i, "id_ActividadGral").ToString());
                                agenda.id_Actividad = Convert.ToInt32(grdActividadesAsesor.GetRowValues(i, "id_Actividad").ToString());
                                agenda.id_tu = Convert.ToInt32(grdActividadesAsesor.GetRowValues(i, "id_tu").ToString());
                                agenda.actividaCheck = 1;

                                agenda.Id_Usu = id_usu;
                                CN.ActHibridasInsertar(agenda, ref verificador, Sesion.Emp_Cnx);
                            }
                            catch(Exception ex)
                            {

                            }
                        }
                    }
                }

                for (var i = 0; i < grdActividadesRsc.VisibleRowCount; i++)
                {
                    if (grdActividadesRsc.GetRowValues(i) != null)
                    {
                        if (grdActividadesRsc.Selection.IsRowSelected(i))
                        {
                            try
                            {
                                agenda = new AgendaRsc();
                                agenda.id_ActividadGral = Convert.ToInt32(grdActividadesRsc.GetRowValues(i, "id_ActividadGral").ToString());
                                agenda.id_Actividad = Convert.ToInt32(grdActividadesRsc.GetRowValues(i, "id_Actividad").ToString());
                                agenda.id_tu = Convert.ToInt32(grdActividadesRsc.GetRowValues(i, "id_tu").ToString());
                                agenda.actividaCheck = 1;

                                agenda.Id_Usu = id_usu;
                                CN.ActHibridasInsertar(agenda, ref verificador, Sesion.Emp_Cnx);
                            }
                            catch (Exception ex)
                            {

                            }
                        } 
                    } 
                }
            }
            
        }

        protected void btnConsultar_ServerClick1(object sender, EventArgs e)
        {
            cargarDatos();
        }

        public void cargarDatos()
        {

            grdActividadesAsesor.Selection.UnselectAll();
            grdActividadesRsc.Selection.UnselectAll();

            CNRSCAgenda CN = new CNRSCAgenda();
            AgendaRsc agenda = new AgendaRsc();
            List<AgendaRsc> List = new List<AgendaRsc>();
            List<AgendaRsc> ListAsesor = new List<AgendaRsc>();
            List<AgendaRsc> ListRSc = new List<AgendaRsc>();

            agenda.Id_Usu = Convert.ToInt32(DllUSuario.Value.ToString());
            CN.ConsultaActHibridas(agenda, ref List, session.Emp_Cnx);
            if (List.Count > 0)
            {

                ListAsesor = (from tlist in List
                              where tlist.id_tu == 16
                              select tlist).ToList();

                ListRSc = (from tlist in List
                           where tlist.id_tu == 15
                           select tlist).ToList();
            }
            Session["AltaAtividadAsesor"] = ListAsesor;
            Session["AltaAtividadRSC"] = ListRSc;
            grdActividadesAsesor.DataSource = ListAsesor;
            grdActividadesAsesor.DataBind();
            grdActividadesRsc.DataSource = ListRSc;
            grdActividadesRsc.DataBind();

            


        }


        protected void grdActividadesRsc_DataBound(object sender, EventArgs e)
        {
            int icheck = 0;
            for (int i = 0; i < grdActividadesRsc.VisibleRowCount; i++)
            {
                int estatus = Convert.ToInt32(grdActividadesRsc.GetRowValues(i, "actividaChecked"));

                if (estatus == 1)
                {
                    grdActividadesRsc.Selection.SelectRow(i);
                    icheck = 1;
                }
            }

            if (DllRol.Value != null)
            {
                if (DllRol.Value.ToString() == "15" && icheck == 0)
                {
                    grdActividadesRsc.Selection.SelectAll();
                }
            }

            
        }

        protected void grdActividadesAsesor_DataBound(object sender, EventArgs e)
        {
            int icheck = 0;
            for (int i = 0; i < grdActividadesAsesor.VisibleRowCount; i++)
            {
                int estatus = Convert.ToInt32(grdActividadesAsesor.GetRowValues(i, "actividaChecked"));

                if (estatus == 1)
                {
                    grdActividadesAsesor.Selection.SelectRow(i);
                    icheck = 1;
                }
            }
            if (DllRol.Value != null)
            {
                if (DllRol.Value.ToString() == "16" && icheck == 0)
                {
                    grdActividadesAsesor.Selection.SelectAll();
                }
            }
        }
    }
}