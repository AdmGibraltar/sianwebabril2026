using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocios;

namespace SIANWEB
{
    public partial class RscAltaCita : System.Web.UI.Page
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

        #region mensaje

        private void mensajeAlerta(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "modalMensaje('" + mensaje + "')", true);
        }

        private void mensajeExito(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "modalMensajeExito('" + mensaje + "')", true);
        }
        #endregion


        protected void Page_Load(object sender, EventArgs e)
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
                if (!Page.IsPostBack)
                { 
                    ValidarPermisos(); 
                    Inicializar();
                    Session["PreAgendaTipoCliente"] = null;
                    if (Request.QueryString["id"] != null)
                    {
                        CargarDAtosActividad(Convert.ToInt32(Request.QueryString["id"].ToString()));
                    }

                    if (Request.QueryString["idPReagenda"] != null)
                    {
                        CargarDatosPreAgenda(Convert.ToInt32(Request.QueryString["idPReagenda"].ToString()));
                    }
                     
                }
            }
        }

        public void CargarDAtosActividad(int id)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            int Usuario_Tipo = session.Id_TU;
            int ID_CD = session.Id_Cd;
            int Id_Rik = session.Id_Rik;
            int Id_U = session.Id_U;
            if (Sesion.Id_TU != 15 && Sesion.Id_TU != 16)
            {
                Id_U = -1;
            }

            AgendaRsc datos = new AgendaRsc();
            List<Agenda> Lista = new List<Agenda>();

            CNRSCAgenda CN = new CNRSCAgenda();
            AgendaRsc Registro = new AgendaRsc();

            datos.Id_Emp = session.Id_Emp;
            datos.Id_Usu = Id_U;
            datos.Id_Cd = ID_CD;
            datos.ID = id; 
            
            CN.ConsultarAgendaDetallada(datos, ref Lista, session.Emp_Cnx);

            if (Lista.Count > 0)
            {
                if(Lista.First().id_agendaGrupal.ToString() != "0")
                {
                    BtnGuardar.Visible = false;
                    BtnGuardar.Disabled = true; 
                }

                cargarTipoUsuario(); 
                cargarClientes(); 

                DllRol.Value = Lista.First().id_tu.ToString(); 
                CargarUsuario(session.Id_Cd); 
                DllUSuario.Value = Lista.First().Id_Usu.ToString();
                DllCLiente.Value = Lista.First().id_cte.ToString();

                cargarActividadgeneral();
                ConsultarClienteTipoCriterio();
                DllActividadGeneral.Value = Lista.First().id_ActividadGral.ToString();
                CargarACtividad(Convert.ToInt32(Lista.First().id_ActividadGral.ToString()));
                DllActividad.Value = Lista.First().id_Actividad.ToString(); 

                txtObservaciones.Text = Lista.First().Comentarios.ToString();

                Fecha.Value = Convert.ToDateTime(Lista.First().FechaInicio.ToString());
                dllFechaFinal.Value = Convert.ToDateTime(Lista.First().fechaFinal.ToString());

                HorarioInicial.Value = Convert.ToDateTime(Lista.First().FechaInicio.ToString());
                HorarioFinal.Value = Convert.ToDateTime(Lista.First().fechaFinal.ToString());


                DllRol.Enabled = false;
                DllUSuario.Enabled = false;
                DllActividadGeneral.Enabled = false;
                DllActividad.Enabled = false;
                DllCLiente.Enabled = false;
                chkRecurrente.Enabled = false;
            }
        }


        public void CargarDatosPreAgenda(int id)
        {
           
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            int Usuario_Tipo = session.Id_TU;
            int ID_CD = session.Id_Cd;
            int Id_Rik = session.Id_Rik;
            int Id_U = session.Id_U;
            if (Sesion.Id_TU != 15 && Sesion.Id_TU != 16)
            {
                Id_U = -1;
            }

            AgendaRsc datos = new AgendaRsc();
            List<Agenda> Lista = new List<Agenda>();

            CNRSCAgenda CN = new CNRSCAgenda();
            AgendaRsc Registro = new AgendaRsc();

            datos.Id_Emp = session.Id_Emp;
            datos.Id_Usu = Id_U;
            datos.Id_Cd = ID_CD;
            datos.ID = id;

            CN.ConsultarPreAgendaDetallada(datos, ref Lista, session.Emp_Cnx);

            if (Lista.Count > 0)
            {
                Session["PreAgendaTipoCliente"] = Lista.First().tipo.ToString();
                cargarTipoUsuario(); 
               

                DllRol.Value = Lista.First().id_tu.ToString();
                CargarUsuario(session.Id_Cd);
                DllUSuario.Value = Lista.First().Id_Usu.ToString();

                cargarClientes();
                DllCLiente.Value = Lista.First().id_cte.ToString();
                cargarActividadgeneral();
                ConsultarClienteTipoCriterio();


                Fecha.Value = Convert.ToDateTime(Lista.First().FechaInicio.ToString());
                dllFechaFinal.Value = Convert.ToDateTime(Lista.First().fechaFinal.ToString());

                HorarioInicial.Value = Convert.ToDateTime(Lista.First().FechaInicio.ToString());
                HorarioFinal.Value = Convert.ToDateTime(Lista.First().fechaFinal.ToString());


                DllRol.Enabled = false;
                DllUSuario.Enabled = false; 
                DllCLiente.Enabled = false; 
            }
        }

        private void ValidarPermisos()
        {
            try
            {
                Pagina pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Trim().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                if (pag.Length > 1)
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                else
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;

                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, session.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;
                Permiso Permiso = new Permiso();
                Permiso.Id_U = session.Id_U;
                Permiso.Id_Cd = session.Id_Cd;
                Permiso.Sm_cve = pagina.Clave;
                //Esta clave depende de la pantalla

                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, session.Emp_Cnx);
                //this.rtb1.Items[1].Visible = false;

                //if (Permiso.PAccesar == true)
                //{
                //_PermisoGuardar = Permiso.PGrabar;
                //_PermisoModificar = Permiso.PModificar;
                //_PermisoEliminar = Permiso.PEliminar;
                //_PermisoImprimir = Permiso.PImprimir;

                if (session.Id_Rik != -1 || session.Id_TU == 2)
                { //Captura de pedidos por parte del representante
                    CN_CatCentroDistribucion catcentro = new CN_CatCentroDistribucion();
                    CentroDistribucion cd = new CentroDistribucion();
                    catcentro.ConsultarCentroDistribucion(ref cd, session.Id_Cd_Ver, session.Id_Emp, session.Emp_Cnx);
                    if (!cd.Cd_ActivaCapPedRep)
                    {

                    }
                }
                //}
                //else
                //    Response.Redirect("Inicio.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Inicializar()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                int Usuario_Tipo = session.Id_TU;
                int Id_CD = session.Id_Cd;
                int Id_Rik = session.Id_Rik;
                int Id_U = session.Id_U;
                int Id_TU = session.Id_TU;

                cargarTipoUsuario();
                CargarUsuario(Id_CD);
                cargarClientes();

                if (Sesion.Id_TU != 15 && Sesion.Id_TU != 16)
                {
                    DllActividadGeneral.Items.Add("--Todos--", "-1");
                    DllActividadGeneral.Value = "-1";
                    DllActividad.Items.Add("--Todos--", "-1");
                    DllActividad.Value = "-1";
                }
                else
                {
                    cargarActividadgeneral();
                    DllActividad.Items.Add("--Todos--", "-1");
                    DllActividad.Value = "-1";
                }


                Fecha.Value = DateTime.Now;
                dllFechaFinal.Value = DateTime.Now;
                dllFechaFinal.Enabled = false;
            }
            catch(Exception ex)
            {
                mensajeAlerta("Error al iniciar el modulo - " + ex.Message);
            }
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

        private void cargarActividadgeneral()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID]; 
            List<RSCMotivo> List = new List<RSCMotivo>();
            CNRSCAgenda Cn = new CNRSCAgenda();
            Cn.ConsultaMotivo(Sesion.Id_Emp, Convert.ToInt32(DllUSuario.Value.ToString()), Convert.ToInt32(DllCLiente.Value.ToString()), ref List, Sesion.Emp_Cnx);

            DllActividadGeneral.DataSource = List;
            DllActividadGeneral.ValueField = "id";
            DllActividadGeneral.TextField = "descripción";
            DllActividadGeneral.DataBind(); 
            DllActividadGeneral.Value = "-1";
        }

        private void CargarACtividad(int idActividad)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID]; 
            List<RSCMotivo> List = new List<RSCMotivo>();
            CNRSCAgenda Cn = new CNRSCAgenda();
            Cn.ConsultarSubMotivo(Sesion.Id_Emp, idActividad, Convert.ToInt32(DllCLiente.Value.ToString()),  Convert.ToInt32(DllUSuario.Value.ToString()) , ref List, Sesion.Emp_Cnx);

            DllActividad.DataSource = List;
            DllActividad.ValueField = "id";
            DllActividad.TextField = "descripción";
            DllActividad.DataBind(); 
            DllActividad.Value = "-1";
        }

        protected void DllRol_SelectedIndexChanged1(object sender, EventArgs e)
        {
            int Id_CD = session.Id_Cd;
            CargarUsuario(Id_CD);
            cargarActividadgeneral();
            cargarClientes();
            DllActividad.DataSource = null;
            DllActividad.DataBind();
            if (DllActividadGeneral.Value.ToString() != "-1")
            {
                CargarACtividad(Convert.ToInt32(DllActividadGeneral.Value.ToString()));
            }
            else
            {
                DllActividad.Items.Add("--Todos--", "-1");
                DllActividad.Value = "-1";
            }
            txtBracket.Text = "";
        }

        protected void DllUSuario_SelectedIndexChanged1(object sender, EventArgs e)
        {
            cargarClientes();
            cargarActividadgeneral(); 
            DllActividad.DataSource = null;
            DllActividad.DataBind();
            if (DllActividadGeneral.Value.ToString() != "-1")
            {
                CargarACtividad(Convert.ToInt32(DllActividadGeneral.Value.ToString()));
            }
            else
            {
                DllActividad.Items.Add("--Todos--", "-1");
                DllActividad.Value = "-1";
            }
            txtBracket.Text = "";
        }

        protected void chkRecurrente_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRecurrente.Checked)
            {
                DdlFrecuencia.ReadOnly = false;
                DdlFrecuencia.Enabled = true;
                idDia.Visible = false;
                idSemana.Visible = false;
                idMes.Visible = false;
                dllFechaFinal.Enabled = true;
                DdlFrecuencia.Value = "0";
            }
            else
            {
                DdlFrecuencia.ReadOnly = true;
                DdlFrecuencia.Enabled = false;
                idDia.Visible = false;
                idSemana.Visible = false;
                idMes.Visible = false;
                dllFechaFinal.Value = Fecha.Value;
                dllFechaFinal.Enabled = false;
                DdlFrecuencia.Value = "0";
            }
        }

        protected void DdlFrecuencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DdlFrecuencia.Value.ToString() == "0")
            {
                idDia.Visible = false;
                idSemana.Visible = false;
                idMes.Visible = false;
            }
            if (DdlFrecuencia.Value.ToString() == "1")
            {
                idDia.Visible = true;
                idSemana.Visible = false;
                idMes.Visible = false;
            }
            if (DdlFrecuencia.Value.ToString() == "2")
            {
                idDia.Visible = false;
                idSemana.Visible = true;
                idMes.Visible = false;
            }
            if (DdlFrecuencia.Value.ToString() == "3")
            {
                idDia.Visible = false;
                idSemana.Visible = false;
                idMes.Visible = true;
            }
        }

        protected void DllActividadGeneral_SelectedIndexChanged(object sender, EventArgs e)
        {
            DllActividad.DataSource = null;
            DllActividad.DataBind();
            if (DllActividadGeneral.Value.ToString() != "-1")
            {
                CargarACtividad(Convert.ToInt32(DllActividadGeneral.Value.ToString()));
            }
            else
            { 
                DllActividad.Items.Add("--Todos--", "-1");
                DllActividad.Value = "-1";
            }
        }

        protected void Fecha_DateChanged(object sender, EventArgs e)
        {
            if (!chkRecurrente.Checked)
            {
                dllFechaFinal.Value = Fecha.Value;
            }
        }

        protected void BtnGuardar_ServerClick(object sender, EventArgs e)
        { 
            if (Request.QueryString["id"] != null)
            {
                Actualizar(Convert.ToInt32(Request.QueryString["id"].ToString()));
            }
            else
            {
                insertar(); 
            }
        }

        public void insertar()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CNRSCAgenda CN = new CNRSCAgenda();
                AgendaRsc Registro = new AgendaRsc();

                int verificador = 0;
                Registro.Id_Cd = session.Id_Cd;
                Registro.id_tu = Convert.ToInt32(DllRol.Value.ToString());
                Registro.id_cte = Convert.ToInt32(DllCLiente.Value.ToString());
                Registro.id_ActividadGral = Convert.ToInt32(DllActividadGeneral.Value.ToString());
                Registro.id_Actividad = Convert.ToInt32(DllActividad.Value.ToString());
                Registro.Id_Usu = Convert.ToInt32(DllUSuario.Value.ToString()); 
                Registro.tipo = txtBracket.Text; 
                Registro.Comentarios = txtObservaciones.Text;

                if (Convert.ToInt32(DllCLiente.Value.ToString()) == 0)
                {
                    mensajeAlerta("Favor de seleccionar un cliente");
                    return;
                }

                if (Convert.ToInt32(DllRol.Value.ToString()) == -1)
                {
                    mensajeAlerta("Favor de seleccionar un rol");
                    return;
                }
                if (Convert.ToInt32(DllActividadGeneral.Value.ToString()) == -1)
                {
                    mensajeAlerta("Favor de seleccionar una actividad general");
                    return;
                }
                if (Convert.ToInt32(DllActividad.Value.ToString()) == -1)
                {
                    mensajeAlerta("Favor de seleccionar un actividad");
                    return;
                }
                if (HorarioInicial.Value == null || HorarioFinal.Value == null)
                {
                    mensajeAlerta("Favor de seleccionar un horario para la actividad");
                    return;
                }
                if (!chkRecurrente.Checked)
                {
                    Registro.FechaInicio = DateTime.Parse(Fecha.Value.ToString()).Date + DateTime.Parse(HorarioInicial.Value.ToString()).TimeOfDay;
                    Registro.fechaFinal = DateTime.Parse(dllFechaFinal.Value.ToString()).Date + DateTime.Parse(HorarioFinal.Value.ToString()).TimeOfDay;

                    if (Registro.fechaFinal < Registro.FechaInicio)
                    {
                        mensajeAlerta("La fecha final debe ser menor a la fecha inicial");
                        return;
                    }
                    CN.AltaAgendaDetallada(Registro, ref verificador, session.Emp_Cnx);

                    if (verificador == 0)
                    {
                        if (Request.QueryString["idPReagenda"] != null)
                        {
                            actualizarPrecarga(Convert.ToInt32(Request.QueryString["idPReagenda"]));
                        }
                        mensajeExito("Se guardo la actividad");
                        return;
                    }
                }
                else
                {
                    DateTime fechaInical = DateTime.Parse(Fecha.Value.ToString()).Date + DateTime.Parse(HorarioInicial.Value.ToString()).TimeOfDay;
                    DateTime fechaterminal = DateTime.Parse(Fecha.Value.ToString()).Date + DateTime.Parse(HorarioFinal.Value.ToString()).TimeOfDay;
                    DateTime fechaFinal = DateTime.Parse(dllFechaFinal.Value.ToString()).Date + DateTime.Parse(HorarioFinal.Value.ToString()).TimeOfDay;

                    if (Convert.ToInt32(DdlFrecuencia.Value.ToString()) != 0)
                    {
                        if (Convert.ToInt32(DdlFrecuencia.Value.ToString()) == 1)
                        {
                            int dia = Convert.ToInt32(ddlDia.Value.ToString());
                            while (fechaterminal <= fechaFinal)
                            {
                                Registro.FechaInicio = fechaInical;
                                Registro.fechaFinal = fechaterminal;
                                CN.AltaAgendaDetallada(Registro, ref verificador, session.Emp_Cnx);

                                fechaInical = fechaInical.AddDays(dia);
                                fechaterminal = fechaterminal.AddDays(dia);
                                string nameday = fechaInical.ToString("dddd");
                                if (nameday == "domingo")
                                {
                                    fechaInical = fechaInical.AddDays(1);
                                    fechaterminal = fechaterminal.AddDays(1);
                                }
                            }
                            if (verificador == 0)
                            {
                                if (Request.QueryString["idPReagenda"] != null)
                                {
                                    actualizarPrecarga(Convert.ToInt32(Request.QueryString["idPReagenda"]));
                                }
                                mensajeExito("Se guardo las actividades");
                                return;
                            }
                        }
                        else if (Convert.ToInt32(DdlFrecuencia.Value.ToString()) == 2)
                        {
                            int semana = Convert.ToInt32(ddlSemana.Value.ToString());
                            while (fechaterminal <= fechaFinal)
                            {
                                Registro.FechaInicio = fechaInical;
                                Registro.fechaFinal = fechaterminal;
                                CN.AltaAgendaDetallada(Registro, ref verificador, session.Emp_Cnx);

                                fechaInical = fechaInical.AddDays(semana);
                                fechaterminal = fechaterminal.AddDays(semana);

                                string nameday = fechaInical.ToString("dddd");
                                if (nameday == "Domingo")
                                {
                                    fechaInical = fechaInical.AddDays(1);
                                    fechaterminal = fechaterminal.AddDays(1);
                                }
                            }

                            if (verificador == 0)
                            {
                                if (Request.QueryString["idPReagenda"] != null)
                                {
                                    actualizarPrecarga(Convert.ToInt32(Request.QueryString["idPReagenda"]));
                                }
                                mensajeExito("Se guardo las actividades");
                                return;
                            }
                        }
                        else if (Convert.ToInt32(DdlFrecuencia.Value.ToString()) == 3)
                        {
                            int mes = Convert.ToInt32(ddlMes.Value.ToString());
                            while (fechaterminal <= fechaFinal)
                            {
                                Registro.FechaInicio = fechaInical;
                                Registro.fechaFinal = fechaterminal;

                                CN.AltaAgendaDetallada(Registro, ref verificador, session.Emp_Cnx);

                                fechaInical = fechaInical.AddMonths(mes);
                                fechaterminal = fechaterminal.AddDays(mes);

                                string nameday = fechaInical.ToString("dddd");
                                if (nameday == "Domingo")
                                {
                                    fechaInical = fechaInical.AddDays(1);
                                    fechaterminal = fechaterminal.AddDays(1);
                                }
                            }

                            if (verificador == 0)
                            {

                                if (Request.QueryString["idPReagenda"] != null)
                                {
                                    actualizarPrecarga(Convert.ToInt32(Request.QueryString["idPReagenda"]));
                                }
                                mensajeExito("Se guardo las actividades");
                                return;
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                mensajeAlerta("Error al momento de guardar / actualizar las actividades - " + ex.Message.ToString());
            } 
        } 


        public void actualizarPrecarga(int id)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CNRSCAgenda CN = new CNRSCAgenda();
            AgendaRsc Registro = new AgendaRsc();
            int verificador = 0;
            Registro.ID = id;
            Registro.Id_Cd = session.Id_Cd;
            Registro.id_tu = Convert.ToInt32(DllRol.Value.ToString());
            Registro.id_cte = Convert.ToInt32(DllCLiente.Value.ToString());
            Registro.id_ActividadGral = Convert.ToInt32(DllActividadGeneral.Value.ToString());
            Registro.id_Actividad = Convert.ToInt32(DllActividad.Value.ToString());
            Registro.Id_Usu = Convert.ToInt32(DllUSuario.Value.ToString());

            CN.ActualizarPrecarga(Registro, ref verificador, session.Emp_Cnx);
        }

        public void Actualizar(int id)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CNRSCAgenda CN = new CNRSCAgenda();
                AgendaRsc Registro = new AgendaRsc();
                  List<AgendaRsc> Lista = new List<AgendaRsc>();
                int verificador = 0;
                Registro.ID = id;
                Registro.Id_Emp = session.Id_Emp;
                Registro.Id_Cd = session.Id_Cd;
                Registro.id_tu = Convert.ToInt32(DllRol.Value.ToString());
                Registro.id_cte = Convert.ToInt32(DllCLiente.Value.ToString());
                Registro.id_ActividadGral = Convert.ToInt32(DllActividadGeneral.Value.ToString());
                Registro.id_Actividad = Convert.ToInt32(DllActividad.Value.ToString());
                Registro.Id_Usu = Convert.ToInt32(DllUSuario.Value.ToString());

                Registro.Comentarios = txtObservaciones.Text;

                CN.ConsultarAgendaDetalladaLog(Registro, ref Lista, session.Emp_Cnx);

                if (Lista.Count == 0)
                {
                    Registro.FechaInicio = DateTime.Parse(Fecha.Value.ToString()).Date + DateTime.Parse(HorarioInicial.Value.ToString()).TimeOfDay;
                    Registro.fechaFinal = DateTime.Parse(dllFechaFinal.Value.ToString()).Date + DateTime.Parse(HorarioFinal.Value.ToString()).TimeOfDay;

                    if (Registro.fechaFinal < Registro.FechaInicio)
                    {
                        mensajeAlerta("La fecha final debe ser menor a la fecha inicial");
                        return;
                    }
                    CN.ActualizarAgendaDetallada(Registro, ref verificador, session.Emp_Cnx);

                    if (verificador == 0)
                    {
                        mensajeExito("Se Actualizo la actividad");
                        return;
                    }
                } 
                else
                {
                    mensajeAlerta("no se puede Actualizar la actividad, la actividad ya fue modificado anteriormente");
                    return;
                }
            }

            catch (Exception ex)
            {
                mensajeAlerta("Error al momento de guardar / actualizar las actividades - " + ex.Message.ToString());
            } 
        }

        protected void DllCLiente_SelectedIndexChanged(object sender, EventArgs e)
        { 
            ConsultarClienteTipoCriterio();
            cargarActividadgeneral();
            DllActividad.DataSource = null;
            DllActividad.DataBind();
            if (DllActividadGeneral.Value.ToString() != "-1")
            {
                CargarACtividad(Convert.ToInt32(DllActividadGeneral.Value.ToString()));
            }
            else
            {
                DllActividad.Items.Add("--Todos--", "-1");
                DllActividad.Value = "-1";
            }
        }

        public void ConsultarClienteTipoCriterio()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID]; 
            CNRSCAgenda Cn = new CNRSCAgenda();

            AgendaRsc Registro = new AgendaRsc();
            List<Agenda> Lista = new List<Agenda>(); 
            Registro.Id_Emp = session.Id_Emp;
            Registro.Id_Cd = session.Id_Cd; 
            Registro.id_cte = Convert.ToInt32(DllCLiente.Value.ToString());

            Cn.ConsultarClienteTipoCriterio(Registro,  ref Lista, Sesion.Emp_Cnx);

            if(Lista.Count > 0)
            {
                txtBracket.Text = Lista.First().tipo;
            }
            else
            {
                txtBracket.Text = "D";
            }
             
        }
    }
}