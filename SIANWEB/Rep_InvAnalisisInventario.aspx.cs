using System;
using System.Collections;
using System.IO;
using System.Web.UI;
using CapaDatos;
using CapaEntidad;
using CapaNegocios;
using Telerik.Reporting;
using Telerik.Reporting.Processing;
using Telerik.Web.UI;

namespace SIANWEB
{
    public partial class Rep_InvAnalisisInventario : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        #endregion
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        //ValidarPermisos();
                        CargarCentros();
                        if (!Sesion.Cu_Modif_Pass_Voluntario)
                        {
                            RadAjaxManager1.ResponseScripts.Add("(function(){var f = function(){AbrirContrasenas(); return false;Sys.Application.remove_load(f);};Sys.Application.add_load(f);})()");
                        }


                        dpFechaini.DbSelectedDate = Sesion.CalendarioIni;
                        dpFechafin.DbSelectedDate = Sesion.CalendarioFin;

                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Page_Load");
            }
        }



        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            string accionError = string.Empty;
            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;

                switch (btn.CommandName)
                {
                    case "print":
                        Abrir_Reporte(true);
                        break;
                    case "excel":
                        Abrir_Reporte(false);
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void CmbCentro_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            Sesion.Id_Cd_Ver = Convert.ToInt32(CmbCentro.SelectedItem.Value);
        }
        protected void rblRemisiones_SelectedIndexChanged(object sender, EventArgs e)
        {


            //if (rblRemisiones.SelectedValue == "1")
            //{
            //    rblDetalle.Enabled = true;
            //}
            //else
            //{
            //    rblDetalle.Enabled = false;
            //}
        }




        protected void RblTipoRep_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {//TODO que ponga la fecha o el mes 
                if (this.RblTipoRep.SelectedValue == "1")
                {

                }
                else
                {

                }

            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RblTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {//TODO que ponga la fecha o el mes 
                if (this.RblTipo.SelectedValue == "1")
                {

                }
                else
                {

                }

            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void RblOrdenado_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {//TODO que ponga la fecha o el mes 
                if (this.RblOrdenado.SelectedValue == "1")
                {

                }
                else
                {

                }

            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }



        #endregion
        #region Metodos
        private void CargarCentros()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();

                if (Sesion.U_MultiOfi == false)
                {
                    CN_Comun.LlenaCombo(2, Sesion.Id_Emp, Sesion.Id_U, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.Visible = false;
                    this.TblEncabezado.Rows[0].Cells[2].InnerText = " " + CmbCentro.FindItemByValue(Sesion.Id_Cd_Ver.ToString()).Text;
                }
                else
                {
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, Sesion.Id_Cd_Ver, Sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = Sesion.Id_Cd_Ver.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                    ((RadToolBarItem)RadToolBar1.Items.FindItemByValue("print")).Visible = _PermisoImprimir;
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
        private void Abrir_Reporte(bool a_pantalla)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ArrayList ALValorParametrosConsignacion = new ArrayList();
                CN__Comun cn_comun = new CN__Comun();





                //Consulta centro de distribución
                string Emp_Nombre = "";
                string Cd_Nombre = "";
                string U_Nombre = "";
                new CN_CapPedido().ConsultarEncabezado_RepFacPedidosPendientes(sesion, ref Emp_Nombre, ref Cd_Nombre, ref U_Nombre);
                //parametros cabecera       


                // calcular la fecha actual y final en base a este dato ALValorParametrosInternos.Add(this.RblTipoRep.SelectedValue == "1" ? null : this.cmbanio.SelectedValue);
                DateTime fechainicial = new DateTime();
                DateTime fechafinal = new DateTime();

                fechainicial = (DateTime)dpFechaini.SelectedDate.Value;
                fechafinal = (DateTime)dpFechafin.SelectedDate.Value;

                fechafinal = DateTime.Now;


                if (this.RblTipoRep.SelectedValue == "1")  //1 actual 2  cierre
                {

                    fechainicial = fechainicial.AddYears(-2);
                    int saño = fechainicial.Year;
                    string fecha = "";
                    fecha = saño.ToString() + "-01-01 00:00:00.000";
                    fechainicial = Convert.ToDateTime(fecha);
                }
                else
                {
                    fechafinal = fechainicial.AddDays(-1);

                    fechainicial = fechainicial.AddYears(-2);
                    int saño = fechainicial.Year;
                    string fecha = "";
                    fecha = saño.ToString() + "-01-01 00:00:00.000";
                    fechainicial = Convert.ToDateTime(fecha);

                }


                //Reporte de consignación parametros 
                ALValorParametrosConsignacion.Add(sesion.Emp_Cnx);
                ALValorParametrosConsignacion.Add(sesion.Id_Emp);//nombre empresa
                ALValorParametrosConsignacion.Add(sesion.Id_Cd.ToString());//nombre sucursal
                //9mayo2018 agregar el tipo de si es cierre o fecha actual 

                if (this.RblTipoRep.SelectedValue == "1")  //1 actual 2 cierre
                {
                    ALValorParametrosConsignacion.Add(1);
                }
                else
                {
                    ALValorParametrosConsignacion.Add(2);
                }


                ALValorParametrosConsignacion.Add("Producto en consignación");
                ALValorParametrosConsignacion.Add(fechafinal);//fecha final
                //oredenado por no funcionaba 10may2019 JFCV
                if (this.RblOrdenado.SelectedValue == "1")  //Ordenado por producto o por inventario 
                {
                    ALValorParametrosConsignacion.Add("1");//OrdenadoPor producto
                }
                else
                {
                    ALValorParametrosConsignacion.Add("2");//OrdenadoPor monto inventario
                }



                ALValorParametrosConsignacion.Add(Emp_Nombre);//nombre empresa
                ALValorParametrosConsignacion.Add("Todos");//nombre cdi
                ALValorParametrosConsignacion.Add(DateTime.Now.ToShortDateString() + "  " + DateTime.Now.ToShortTimeString());//fecha
                if (this.cmbDias.SelectedValue == "-1" || this.cmbDias.SelectedValue == "0" || this.cmbDias.SelectedValue == "")  //Días 
                {
                    ALValorParametrosConsignacion.Add("-1");
                }
                else
                {
                    ALValorParametrosConsignacion.Add(this.cmbDias.SelectedValue);
                }
                ALValorParametrosConsignacion.Add(this.RblOrdenado.SelectedItem.ToString());//OrdenTitulo
                if (this.cmbDias.SelectedValue == "-1" || this.cmbDias.SelectedValue == "0" || this.cmbDias.SelectedValue == "")  //Días 
                {
                    ALValorParametrosConsignacion.Add("Todos");//Dias titulo
                }
                else
                {
                    ALValorParametrosConsignacion.Add(this.cmbDias.SelectedItem.Text);//Dias titulo
                }


                //JFCV 22 jul 2019 calculo el mes actual para enviarlo al reporte y le envio si es cierre o actual
                if (this.RblTipoRep.SelectedValue == "1")  //1 actual 2 cierre ActualoCierre
                {
                    ALValorParametrosConsignacion.Add(1);
                }
                else
                {
                    ALValorParametrosConsignacion.Add(2);
                }

                CN_CatCalendario cn_calendario = new CN_CatCalendario();
                Calendario calendario = new Calendario();
                cn_calendario.ConsultaCalendarioActual(ref calendario, sesion);

                int mes_actual = calendario.Cal_Mes;
                ALValorParametrosConsignacion.Add(mes_actual);//Mes del calendario actual en SIANWEBMTY


                Type instance = null;


                if (RblTipo.SelectedValue == "1")
                {
                    instance = typeof(LibreriaReportes.ExpRep_InvAnalisisInventarioTotales);

                    Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = null;
                    Session["InternParameter_Values" + Session.SessionID + HF_ClvPag.Value] = ALValorParametrosConsignacion;
                    Session["assembly" + Session.SessionID + HF_ClvPag.Value] = instance.AssemblyQualifiedName;
                    RadAjaxManager1.ResponseScripts.Add("AbrirReporteCve('" + HF_ClvPag.Value + "');");
                }
                else
                {
                    instance = typeof(LibreriaReportes.ExpRep_InvAnalisisInventario);

                    ImprimirXLS(instance, ALValorParametrosConsignacion);
                }




            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        private void ImprimirXLS(Type instance, ArrayList ALValorParametrosConsignacion)
        {
            try
            {


                //Reporte de prueba Vigente
                Telerik.Reporting.Report RepConsignacion = (Telerik.Reporting.Report)Activator.CreateInstance(instance);
                for (int i = 0; i <= ALValorParametrosConsignacion.Count - 1; i++)
                {

                    RepConsignacion.ReportParameters[i].AllowNull = true;
                    RepConsignacion.ReportParameters[i].Value = ALValorParametrosConsignacion[i];
                }


                ReportProcessor reportProcessor = new ReportProcessor();
                RenderingResult result = reportProcessor.RenderReport("XLS", RepConsignacion, null);
                string ruta = Server.MapPath("Reportes") + "\\" + ALValorParametrosConsignacion[2] + instance.Name + ".xls";
                if (File.Exists(ruta))
                    File.Delete(ruta);
                FileStream fs = new FileStream(ruta, FileMode.Create);
                fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);
                fs.Flush();
                fs.Close();


                //reportBook.Reports[0].DocumentName = "Consignacion";  

                RadAjaxManager1.ResponseScripts.Add("startDownload('" + ALValorParametrosConsignacion[2] + instance.Name + ".xls');");


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region ErrorManager
        private void Alerta(string mensaje)
        {
            try
            {
                mensaje = mensaje.Replace(Convert.ToChar(10).ToString(), string.Empty);
                mensaje = mensaje.Replace(Convert.ToChar(13).ToString(), string.Empty);
                RadAjaxManager1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
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
    }
}
