using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocios;
using ClosedXML.Excel;
using Newtonsoft.Json;

using Syncfusion.XlsIO;
using System.Drawing;

using SIANWEB.Helper;

namespace SIANWEB
{
    public partial class ReporteComercialsCRM : System.Web.UI.Page
    {

        #region Variables
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private Sesion sesion { get { return (Sesion)Session["Sesion" + Session.SessionID]; } set { Session["Sesion" + Session.SessionID] = value; } }



        private string Emp_CnxCentral
        {
            get { return ConfigurationManager.AppSettings.Get("strConnectionCentral"); }
        }

        #endregion

        public static List<Comun> ListaSucursales = new List<Comun>();
        public static List<Comun> ListaAplicaciones = new List<Comun>();
        public static List<Comun> cListaApps = new List<Comun>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ValidarSesion();
                ValidarPermisos();
                Session["SucursalRIk"] = null;
                Session["RepresentanteRik"] = null;
                Session["nombreRik"] = null;

                WRikvsRik.PreRender += new EventHandler(WRikvsRik_PreRender);
                Wizard2.PreRender += new EventHandler(W2_PreRender);
                Inicializar();

                WRikvsRik.PreRender += new EventHandler(WRikvsRik_PreRender);
                WizardReIQ.PreRender += new EventHandler(WizardReIQ_PreRender);

            }

            Session["activeMenu"] = 2;
        }

        private void ValidarPermisos()
        {
            try
            {
                if (sesion != null)
                {
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
                }
                else
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void WizardReIQ_PreRender(object sender, EventArgs e)
        {
            Repeater SideBarList2 = WizardReIQ.FindControl("HeaderContainer").FindControl("Repeater1") as Repeater;
            if (SideBarList2 != null)
            {
                SideBarList2.DataSource = WizardReIQ.WizardSteps;
                SideBarList2.DataBind();
                UpdatePanel2.Update();
            }
        }

        protected void WRikvsRik_PreRender(object sender, EventArgs e)
        {
            Repeater SideBarList = WRikvsRik.FindControl("HeaderContainer").FindControl("SideBarList") as Repeater;
            SideBarList.DataSource = WRikvsRik.WizardSteps;
            SideBarList.DataBind();
            updpanel3.Update();

        }

        protected void W2_PreRender(object sender, EventArgs e)
        {
            Repeater SideBarList = Wizard2.FindControl("HeaderContainer").FindControl("SideBarList") as Repeater;
            SideBarList.DataSource = Wizard2.WizardSteps;
            SideBarList.DataBind();
            updpanel3.Update();
        }

        protected void WizardRep_PreRender(object sender, EventArgs e)
        {
            //Repeater SideBarList2 = WizardRep.FindControl("HeaderContainer").FindControl("Repeater1") as Repeater;
            //if (SideBarList2 != null)
            //{
            //    SideBarList2.DataSource = WizardRep.WizardSteps;
            //    SideBarList2.DataBind();
            //    UpdatePanel2.Update();
            //}
        }


        protected string GetClassForWizardStep(object wizardStep)
        {
            WizardStep step = wizardStep as WizardStep;

            if (step == null)
            {
                return "";
            }
            int stepIndex = WRikvsRik.WizardSteps.IndexOf(step);

            if (stepIndex < WRikvsRik.ActiveStepIndex)
            {
                return "prevStep";
            }
            else if (stepIndex > WRikvsRik.ActiveStepIndex)
            {
                return "nextStep";
            }
            else
            {
                return "currentStep";
            }
        }

        protected string GetClassForWizardStep2(object wizardStep)
        {
            WizardStep step = wizardStep as WizardStep;

            if (step == null)
            {
                return "";
            }
            int stepIndex = Wizard2.WizardSteps.IndexOf(step);

            if (stepIndex < Wizard2.ActiveStepIndex)
            {
                return "prevStep";
            }
            else if (stepIndex > Wizard2.ActiveStepIndex)
            {
                return "nextStep";
            }
            else
            {
                return "currentStep";
            }
        }

        protected void FinishButtonClick(object sender, EventArgs e)
        {
            if (txtCualEs.Text == "1")
            {
                ReporteCRMQuimicos CRMQuimicos = new ReporteCRMQuimicos();
                if (DatosRptQuimicos(ref CRMQuimicos))
                {
                    RptImpulsosQuimicos(CRMQuimicos);
                }
                return;
            }

            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_ReporteCRM cn = new CN_ReporteCRM();
            ReporteCRM CRM = new ReporteCRM();
            List<ReporteCRM> listaCRM = new List<ReporteCRM>();
            List<ReporteCRM> listaCRMRik = new List<ReporteCRM>();

            if (cmbGenerarReporte.Value.ToString() == "-1")
            {
                mensaje("Favor de seleccionar una sucursal para el documento de descarga");
                return;
            }

            DateTime fechaInicial = DateTime.Now;
            DateTime fechafinal = DateTime.Now;
            if (bcmTipoReporte.Value.ToString() == "0")
            {
                fechafinal = fechafinal.AddMonths(1).AddDays(-1);
            }

            if (bcmTipoReporte.Value.ToString() == "1")
            {
                fechaInicial = fechaInicial.AddMonths(-3);
                fechafinal = fechafinal.AddDays(-1);
            }

            if (bcmTipoReporte.Value.ToString() == "2")
            {
                fechaInicial = DateTime.Parse(txtfechaIniciaGR.Value.ToString());
                fechafinal = DateTime.Parse(txtfechafinalGR.Value.ToString());
            }

            if (fechaInicial > fechafinal)
            {
                mensaje("La fecha inicial es mayor a la fecha final de la sección de observar totales.");
                return;
            }


            CRM.Id_Emp = sesion.Id_Emp;
            CRM.Id_Cd = int.Parse(cmbGenerarReporte.Value.ToString());
            if (cmbRikDI.Value.ToString() != "-1")
            {
                CRM.Id_Rik = cmbRikDI.Value.ToString();
                CRM.TipoVenta = Convert.ToInt32(cmbTipoVenta.Value.ToString());
                CRM.fechainicio = fechaInicial;
                CRM.fechafinal = fechafinal.AddMonths(1).AddDays(-1);

                cn.ConsultaReporteExcelMontoProyecto(CRM, ref listaCRM, Emp_CnxCentral);
            }
            else
            {
                CRM.Id_Rik = cmbRikDI.Value.ToString();
                CRM.TipoVenta = Convert.ToInt32(cmbTipoVenta.Value.ToString());
                CRM.fechainicio = fechaInicial;
                CRM.fechafinal = fechafinal.AddMonths(1).AddDays(-1);

                cn.ConsultaReporteExcelMontoProyecto(CRM, ref listaCRM, Emp_CnxCentral);
            }
            CN_Rik Rik = new CN_Rik();
            Rik RegistroRik = new Rik();
            RegistroRik.Id_Emp = Sesion.Id_Emp;
            RegistroRik.Id_Cd = int.Parse(cmbGenerarReporte.Value.ToString()); ;
            RegistroRik.Id_Rik = -1;
            List<Rik> list_Riks = new List<Rik>();

            Rik.ConsultaRIkCrm(RegistroRik, ref list_Riks, Emp_CnxCentral);

            List<Rik> query = (from m in list_Riks
                               group m.nombre_Rik by m.Id_Rik into g
                               select new Rik { id = g.Key, nombre = g.First() }).ToList();

            var idrik = (from tlist2 in query
                         select tlist2.id).ToList();

            listaCRMRik = (from tlist in listaCRM.ToList()
                           join tlist2 in query.ToList() on tlist.rik equals tlist2.id

                           select new ReporteCRM
                           {
                               id_proyect = tlist.id_proyect,
                               fechaCreacion = tlist.fechaCreacion,
                               uenDescripcion = tlist.uenDescripcion,
                               FECHAcierre = tlist.FECHAcierre,
                               rik = tlist.rik,
                               nombre_cliente = tlist.nombre_cliente,
                               nombre_rik = tlist.nombre_rik,
                               Analisis = tlist.Analisis,
                               Presentacion = tlist.Presentacion,
                               NEgociacion = tlist.NEgociacion,
                               Cierre = tlist.Cierre,
                               Cancelacion = tlist.Cancelacion,
                               TipoVentaStr = tlist.TipoVentaStr
                           }).ToList();



            var groupedCustomerList = listaCRMRik.GroupBy(u => u.rik).Select(grp => grp.ToList()).ToList();





            using (var workbook = new XLWorkbook())
            {
                foreach (List<ReporteCRM> lista2 in groupedCustomerList)
                {
                    if (lista2[0].nombre_rik != "")
                    {
                        string nombre = lista2[0].nombre_rik;
                        if (nombre.Length > 30)
                        {
                            nombre = nombre.ToString().Substring(0, 30);
                        }

                        var HojaExcel = workbook.Worksheets.Add(nombre);

                        HojaExcel.Cell("A2").Value = "CDI";
                        HojaExcel.Cell("A2").Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                        HojaExcel.Cell("A2").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        HojaExcel.Cell("A2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


                        HojaExcel.Cell("B2").Value = "Rik";
                        HojaExcel.Cell("B2").Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                        HojaExcel.Cell("B2").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        HojaExcel.Cell("B2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


                        HojaExcel.Cell("C2").Value = "UEN";
                        HojaExcel.Cell("C2").Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                        HojaExcel.Cell("C2").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        HojaExcel.Cell("C2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


                        HojaExcel.Cell("D2").Value = "Id PRoy";
                        HojaExcel.Cell("D2").Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                        HojaExcel.Cell("D2").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        HojaExcel.Cell("D2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


                        HojaExcel.Cell("E2").Value = "VI/VE";
                        HojaExcel.Cell("E2").Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                        HojaExcel.Cell("E2").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        HojaExcel.Cell("E2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


                        HojaExcel.Cell("F2").Value = "Prospecto/Cliente";
                        HojaExcel.Cell("F2").Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                        HojaExcel.Cell("F2").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        HojaExcel.Cell("F2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;



                        int Primerrango = 7;
                        int segundoRango = 7;


                        if (ChAnalisis.Checked)
                        {
                            HojaExcel.Cell(2, segundoRango).Value = "Analisis";
                            HojaExcel.Cell(2, segundoRango).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                            HojaExcel.Cell(2, segundoRango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            HojaExcel.Cell(2, segundoRango).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            segundoRango = segundoRango + 1;
                        }
                        if (chPresentacion.Checked)
                        {

                            HojaExcel.Cell(2, segundoRango).Value = "Presentación";
                            HojaExcel.Cell(2, segundoRango).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                            HojaExcel.Cell(2, segundoRango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            HojaExcel.Cell(2, segundoRango).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            segundoRango = segundoRango + 1;
                        }
                        if (chnegociacion.Checked)
                        {

                            HojaExcel.Cell(2, segundoRango).Value = "Negociación";
                            HojaExcel.Cell(2, segundoRango).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                            HojaExcel.Cell(2, segundoRango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            HojaExcel.Cell(2, segundoRango).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            segundoRango = segundoRango + 1;
                        }
                        if (chCierre.Checked)
                        {

                            HojaExcel.Cell(2, segundoRango).Value = "Cierre";
                            HojaExcel.Cell(2, segundoRango).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                            HojaExcel.Cell(2, segundoRango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            HojaExcel.Cell(2, segundoRango).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            segundoRango = segundoRango + 1;
                        }
                        if (chCancelado.Checked)
                        {

                            HojaExcel.Cell(2, segundoRango).Value = "Cancelación";
                            HojaExcel.Cell(2, segundoRango).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                            HojaExcel.Cell(2, segundoRango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            HojaExcel.Cell(2, segundoRango).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            segundoRango = segundoRango + 1;
                        }


                        HojaExcel.Cell(2, segundoRango).Value = "Fecha Alta";
                        HojaExcel.Cell(2, segundoRango).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                        HojaExcel.Cell(2, segundoRango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        HojaExcel.Cell(2, segundoRango).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        segundoRango = segundoRango + 1;


                        HojaExcel.Cell(2, segundoRango).Value = "Fecha Cierre";
                        HojaExcel.Cell(2, segundoRango).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                        HojaExcel.Cell(2, segundoRango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        HojaExcel.Cell(2, segundoRango).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        segundoRango = segundoRango + 1;


                        HojaExcel.Cell(2, segundoRango).Value = "Tiempo transcurrido en meses";
                        HojaExcel.Cell(2, segundoRango).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                        HojaExcel.Cell(2, segundoRango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        HojaExcel.Cell(2, segundoRango).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        segundoRango = Primerrango;


                        for (var i = 0; i < lista2.Count(); i++)
                        {

                            HojaExcel.Cell(3 + i, 1).Value = cmbGenerarReporte.Text;
                            HojaExcel.Cell(3 + i, 1).Style.Fill.BackgroundColor = XLColor.White;
                            HojaExcel.Cell(3 + i, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                            HojaExcel.Cell(3 + i, 2).Value = lista2[i].nombre_rik;
                            HojaExcel.Cell(3 + i, 2).Style.Fill.BackgroundColor = XLColor.White;
                            HojaExcel.Cell(3 + i, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            HojaExcel.Cell(3 + i, 2).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;

                            HojaExcel.Cell(3 + i, 3).Value = lista2[i].uenDescripcion;
                            HojaExcel.Cell(3 + i, 3).Style.Fill.BackgroundColor = XLColor.White;
                            HojaExcel.Cell(3 + i, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            HojaExcel.Cell(3 + i, 3).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;

                            HojaExcel.Cell(3 + i, 4).Value = lista2[i].id_proyect;
                            HojaExcel.Cell(3 + i, 4).Style.Fill.BackgroundColor = XLColor.White;
                            HojaExcel.Cell(3 + i, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            HojaExcel.Cell(3 + i, 4).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;

                            HojaExcel.Cell(3 + i, 5).Value = lista2[i].TipoVentaStr;
                            HojaExcel.Cell(3 + i, 5).Style.Fill.BackgroundColor = XLColor.White;
                            HojaExcel.Cell(3 + i, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            HojaExcel.Cell(3 + i, 5).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;

                            HojaExcel.Cell(3 + i, 6).Value = lista2[i].nombre_cliente;
                            HojaExcel.Cell(3 + i, 6).Style.Fill.BackgroundColor = XLColor.White;
                            HojaExcel.Cell(3 + i, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                            if (ChAnalisis.Checked)
                            {

                                HojaExcel.Cell(3 + i, segundoRango).Value = lista2[i].Analisis > 0 ? lista2[i].Analisis.ToString() : "";
                                HojaExcel.Cell(3 + i, segundoRango).Style.Fill.BackgroundColor = XLColor.White;
                                HojaExcel.Cell(3 + i, segundoRango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                HojaExcel.Cell(3 + i, segundoRango).Style.NumberFormat.Format = "$0.00";
                                HojaExcel.Cell(3 + i, segundoRango).DataType = XLDataType.Number;
                                segundoRango = segundoRango + 1;
                            }
                            if (chPresentacion.Checked)
                            {

                                HojaExcel.Cell(3 + i, segundoRango).Value = lista2[i].Presentacion > 0 ? lista2[i].Presentacion.ToString() : "";
                                HojaExcel.Cell(3 + i, segundoRango).Style.Fill.BackgroundColor = XLColor.White;
                                HojaExcel.Cell(3 + i, segundoRango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                HojaExcel.Cell(3 + i, segundoRango).Style.NumberFormat.Format = "$0.00";
                                HojaExcel.Cell(3 + i, segundoRango).DataType = XLDataType.Number;
                                segundoRango = segundoRango + 1;

                            }
                            if (chnegociacion.Checked)
                            {

                                HojaExcel.Cell(3 + i, segundoRango).Value = lista2[i].NEgociacion > 0 ? lista2[i].NEgociacion.ToString() : "";
                                HojaExcel.Cell(3 + i, segundoRango).Style.Fill.BackgroundColor = XLColor.White;
                                HojaExcel.Cell(3 + i, segundoRango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                HojaExcel.Cell(3 + i, segundoRango).Style.NumberFormat.Format = "$0.00";
                                HojaExcel.Cell(3 + i, segundoRango).DataType = XLDataType.Number;
                                segundoRango = segundoRango + 1;
                            }
                            if (chCierre.Checked)
                            {

                                HojaExcel.Cell(3 + i, segundoRango).Value = lista2[i].Cierre > 0 ? lista2[i].Cierre.ToString() : "";
                                HojaExcel.Cell(3 + i, segundoRango).Style.Fill.BackgroundColor = XLColor.White;
                                HojaExcel.Cell(3 + i, segundoRango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                HojaExcel.Cell(3 + i, segundoRango).Style.NumberFormat.Format = "$0.00";
                                HojaExcel.Cell(3 + i, segundoRango).DataType = XLDataType.Number;
                                segundoRango = segundoRango + 1;
                            }
                            if (chCancelado.Checked)
                            {

                                HojaExcel.Cell(3 + i, segundoRango).Value = lista2[i].Cancelacion > 0 ? lista2[i].Cancelacion.ToString() : "";
                                HojaExcel.Cell(3 + i, segundoRango).Style.Fill.BackgroundColor = XLColor.White;
                                HojaExcel.Cell(3 + i, segundoRango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                HojaExcel.Cell(3 + i, segundoRango).Style.NumberFormat.Format = "$0.00";
                                HojaExcel.Cell(3 + i, segundoRango).DataType = XLDataType.Number;
                                segundoRango = segundoRango + 1;
                            }


                            HojaExcel.Cell(3 + i, segundoRango).Value = DateTime.Parse(lista2[i].fechaCreacion.ToString()).ToString("dd/MM/yyyy");
                            HojaExcel.Cell(3 + i, segundoRango).Style.Fill.BackgroundColor = XLColor.White;
                            HojaExcel.Cell(3 + i, segundoRango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            segundoRango = segundoRango + 1;

                            HojaExcel.Cell(3 + i, segundoRango).Value = lista2[i].FECHAcierre == null ? "" : DateTime.Parse(lista2[i].FECHAcierre.ToString()).ToString("dd/MM/yyyy");
                            HojaExcel.Cell(3 + i, segundoRango).Style.Fill.BackgroundColor = XLColor.White;
                            HojaExcel.Cell(3 + i, segundoRango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            segundoRango = segundoRango + 1;

                            DateTime end = DateTime.Now;
                            DateTime start = DateTime.Parse(lista2[i].fechaCreacion.ToString());

                            TimeSpan difference = (end - start);

                            double dif = difference.TotalDays / 30;

                            HojaExcel.Cell(3 + i, segundoRango).Value = dif.ToString("F2");

                            if (dif < 2.9)
                            {
                                HojaExcel.Cell(3 + i, segundoRango).Style.Fill.BackgroundColor = XLColor.LightGreen;
                            }
                            else if (dif < 5.9)
                            {
                                HojaExcel.Cell(3 + i, segundoRango).Style.Fill.BackgroundColor = XLColor.Yellow;
                            }
                            else
                            {
                                HojaExcel.Cell(3 + i, segundoRango).Style.Fill.BackgroundColor = XLColor.PaleVioletRed;
                            }
                            HojaExcel.Cell(3 + i, segundoRango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            segundoRango = Primerrango;
                        }


                        segundoRango = Primerrango;
                        if (ChAnalisis.Checked)
                        {
                            HojaExcel.Cell(1, segundoRango).FormulaA1 = "=Sum(" + HojaExcel.Cell(3, segundoRango).Address + ":" + HojaExcel.Cell(3 + lista2.Count() - 1, segundoRango).Address + ")";
                            HojaExcel.Cell(1, segundoRango).Style.Fill.BackgroundColor = XLColor.Yellow;
                            HojaExcel.Cell(1, segundoRango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            HojaExcel.Cell(1, segundoRango).Style.NumberFormat.Format = "$0.00";
                            HojaExcel.Cell(1, segundoRango).DataType = XLDataType.Number;
                            segundoRango = segundoRango + 1;
                        }
                        if (chPresentacion.Checked)
                        {

                            HojaExcel.Cell(1, segundoRango).FormulaA1 = "=Sum(" + HojaExcel.Cell(3, segundoRango).Address + ":" + HojaExcel.Cell(3 + lista2.Count() - 1, segundoRango).Address + ")";
                            HojaExcel.Cell(1, segundoRango).Style.Fill.BackgroundColor = XLColor.Yellow;
                            HojaExcel.Cell(1, segundoRango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            HojaExcel.Cell(1, segundoRango).Style.NumberFormat.Format = "$0.00";
                            HojaExcel.Cell(1, segundoRango).DataType = XLDataType.Number;
                            segundoRango = segundoRango + 1;

                        }
                        if (chnegociacion.Checked)
                        {

                            HojaExcel.Cell(1, segundoRango).FormulaA1 = "=Sum(" + HojaExcel.Cell(3, segundoRango).Address + ":" + HojaExcel.Cell(3 + lista2.Count() - 1, segundoRango).Address + ")";
                            HojaExcel.Cell(1, segundoRango).Style.Fill.BackgroundColor = XLColor.Yellow;
                            HojaExcel.Cell(1, segundoRango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            HojaExcel.Cell(1, segundoRango).Style.NumberFormat.Format = "$0.00";
                            HojaExcel.Cell(1, segundoRango).DataType = XLDataType.Number;
                            segundoRango = segundoRango + 1;
                        }
                        if (chCierre.Checked)
                        {

                            HojaExcel.Cell(1, segundoRango).FormulaA1 = "=Sum(" + HojaExcel.Cell(3, segundoRango).Address + ":" + HojaExcel.Cell(3 + lista2.Count() - 1, segundoRango).Address + ")";
                            HojaExcel.Cell(1, segundoRango).Style.Fill.BackgroundColor = XLColor.Yellow;
                            HojaExcel.Cell(1, segundoRango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            HojaExcel.Cell(1, segundoRango).Style.NumberFormat.Format = "$0.00";
                            HojaExcel.Cell(1, segundoRango).DataType = XLDataType.Number;
                            segundoRango = segundoRango + 1;
                        }
                        if (chCancelado.Checked)
                        {

                            HojaExcel.Cell(1, segundoRango).FormulaA1 = "=Sum(" + HojaExcel.Cell(3, segundoRango).Address + ":" + HojaExcel.Cell(3 + lista2.Count() - 1, segundoRango).Address + ")";
                            HojaExcel.Cell(1, segundoRango).Style.Fill.BackgroundColor = XLColor.Yellow;
                            HojaExcel.Cell(1, segundoRango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            HojaExcel.Cell(1, segundoRango).Style.NumberFormat.Format = "$0.00";
                            HojaExcel.Cell(1, segundoRango).DataType = XLDataType.Number;
                            segundoRango = segundoRango + 1;
                        }


                        HojaExcel.Cell(1, segundoRango).Style.Fill.BackgroundColor = XLColor.White;
                        HojaExcel.Cell(1, segundoRango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        segundoRango = segundoRango + 1;

                        HojaExcel.Cell(1, segundoRango).Style.Fill.BackgroundColor = XLColor.White;
                        HojaExcel.Cell(1, segundoRango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        segundoRango = segundoRango + 1;

                        DateTime fechaActual = DateTime.Now;

                        HojaExcel.Cell(1, segundoRango).Value = fechaActual.ToString("dd/MM/yyyy");
                        HojaExcel.Cell(1, segundoRango).Style.Fill.BackgroundColor = XLColor.Yellow;
                        HojaExcel.Cell(1, segundoRango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        HojaExcel.Columns().AdjustToContents();
                    }

                }
                string rutaguardado = Server.MapPath("~/Reportes/") + "ReporteCRM_" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";


                if (File.Exists(rutaguardado))
                {
                    File.Delete(rutaguardado);
                }

                workbook.SaveAs(rutaguardado);

                workbook.SaveAs(rutaguardado);

                string Outgoingfile = "ReporteCRM_" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";
                string ruta = Server.MapPath("~/Reportes/") + "ReporteCRM_" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";
                // Prepare the response
                HttpResponse httpResponse = Response;
                httpResponse.Clear();
                httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                httpResponse.AddHeader("Content-Disposition", "attachment; filename=" + Outgoingfile);



                // Flush the workbook to the Response.OutputStream
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    workbook.SaveAs(memoryStream);
                    memoryStream.WriteTo(httpResponse.OutputStream);
                    memoryStream.Close();
                }

                httpResponse.End();
            }
        }

        protected void bcmTipoReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bcmTipoReporte.Value.ToString() == "2")
            {

                txtfechaIniciaGR.Value = DateTime.Now;
                txtfechafinalGR.Value = DateTime.Now;

                txtfechaIniciaGR.Enabled = true;
                txtfechafinalGR.Enabled = true
                    ;
            }

            else
            {
                txtfechaIniciaGR.Value = DateTime.Now;
                txtfechafinalGR.Value = DateTime.Now;

                txtfechaIniciaGR.Enabled = false;
                txtfechafinalGR.Enabled = false;
            }
        }

        /// <summary>
        /// evento que se llama en la seccion de comparar representante
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void wizClaimInfo_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {

            if (WRikvsRik.WizardSteps[e.CurrentStepIndex].Title == "1")
            {
                if (CMBSucursalRIk.Value.ToString() == "-1")
                {

                    e.Cancel = true;
                }

            }



            if (WRikvsRik.WizardSteps[e.CurrentStepIndex].Title == "1")
            {
                if (CMBSucursalRIk.Value.ToString() == "-1")
                {
                    e.Cancel = true;
                }
                else
                {
                    Session["SucursalRIk"] = CMBSucursalRIk.Value.ToString();
                }
                WRikvsRik.PreRender += new EventHandler(WRikvsRik_PreRender);

            }

            if (WRikvsRik.WizardSteps[e.CurrentStepIndex].Title == "2")
            {

                string representante = "";
                List<int> myList = new List<int>();

                for (var i = 0; i < RBLRepresentante.Items.Count; i++)
                {
                    if (RBLRepresentante.Items[i].Selected)
                    {
                        int value = Convert.ToInt32(RBLRepresentante.Items[i].Value.ToString());
                        myList.Add(value);
                        if (representante == "")
                        {
                            representante = value.ToString();
                        }
                        else
                        {
                            representante = representante + "," + value.ToString();
                        }
                    }
                }

                if (myList.Count == 0)
                {
                    e.Cancel = true;
                }
                else
                {
                    Session["RepresentanteRik"] = representante;
                }
                WRikvsRik.PreRender += new EventHandler(WRikvsRik_PreRender);
            }

            if (WRikvsRik.WizardSteps[e.CurrentStepIndex].Title == "3")
            {
                DateTime fecha1 = DateTime.Parse(TxtAnioInicialrvr.Value.ToString());
                DateTime fecha2 = DateTime.Parse(TxtAnioFinalrvr.Value.ToString()).AddMonths(1).AddDays(-1);
                fecharikvsrikinicial.Value = fecha1.ToString("dd/MM/yyyy");
                fecharikvsrikfinal.Value = fecha2.ToString("dd/MM/yyyy");

                //FechaInicial_RptIQ.Value = fecha1.ToString("dd/MM/yyyy");
                //Fechafinal_RptIQ.Value = fecha2.ToString("dd/MM/yyyy");
            }
            updpanel3.Update();
        }


        protected void Wizard2_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            Wizard2.PreRender += new EventHandler(W2_PreRender);
            updpanel3.Update();
        }

        protected void Wizard2_PreviousButtonClick(object sender, WizardNavigationEventArgs e)
        {
            Wizard2.PreRender += new EventHandler(W2_PreRender);
            updpanel3.Update();
        }

        protected void WRikvsRik_PreviousButtonClick(object sender, WizardNavigationEventArgs e)
        {
            WRikvsRik.PreRender += new EventHandler(WRikvsRik_PreRender);
            updpanel3.Update();
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
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Inicializar()
        {
            ValidarSesion();
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            if (sesion != null)
            {
                CN_Comun.DevLlenaCombo(2, sesion.Id_Emp, sesion.Id_U, Emp_CnxCentral, "SP_CatCDI_combo2", ref CMBSucursal);
                CMBSucursalRIk.Items.AddRange(CMBSucursal.Items.Cast<Object>().ToArray());
                CmbSucursalSvS.Items.AddRange(CMBSucursal.Items.Cast<Object>().ToArray());
                CmbSucursalSvS.Items.Remove(CmbSucursalSvS.Items.FindByValue("-1"));

                CMBSucursal.Value = sesion.Id_Cd_Ver.ToString();
                //cmbSucursales.Value = sesion.Id_Cd_Ver.ToString();
                CMBSucursalRIk.Value = sesion.Id_Cd_Ver.ToString();

                Session["nombreRik"] = null;
                int id_Cd = Convert.ToInt32(CMBSucursalRIk.Value.ToString());
                cargarRIk(id_Cd);
                updpanel3.Update();

                int id_Cd2 = Convert.ToInt32(CMBSucursal.Value.ToString());
                cargarRIkBuscarInformacion(id_Cd2);
                UpdatePanel43.Update();

                CMBSucursalRIk.ReadOnly = true;
                CMBSucursal.ReadOnly = true;

                CMBRIK.Items.Add("--Todos--", "-1");
                CMBRIK.Value = "-1";

                cmbRikDI.Items.Add("--Todos--", "-1");
                cmbRikDI.Value = "-1";

                txtfechaIniciaGR.Value = DateTime.Now.ToString();
                txtfechafinalGR.Value = DateTime.Now.ToString();

                //Fechas para pestaña de 
                drpFecInicialRptIQ.Value = DateTime.Now.ToString();
                drpFecFinalRptIQ.Value = DateTime.Now.ToString();

                cmbGenerarReporte.Items.AddRange(CMBSucursal.Items.Cast<Object>().ToArray());
                cmbGenerarReporte.Value = "-1";
                cmbGenerarReporte.ReadOnly = true;
                cmbGenerarReporte.Value = sesion.Id_Cd_Ver.ToString();

                int id_CdcmbGenerarReporte = Convert.ToInt32(cmbGenerarReporte.Value.ToString());
                cargarRIkID(id_CdcmbGenerarReporte);

                CMBSucursalRIk.ReadOnly = true;
                cargarRIkID(id_Cd);
                cargarRIkID2(id_Cd);
                txtfechaIniciaGR.Value = DateTime.Now;
                txtfechafinalGR.Value = DateTime.Now;

                TxtAnioFinalrvr.Value = DateTime.Now;
                txtanioFinalSvs.Value = DateTime.Now;
                TxtAnioInicialrvr.Value = DateTime.Now;
                txtAnioinicialsvs.Value = DateTime.Now;
                TXTAnioFinalOT.Value = DateTime.Now;
                TXTAnioInicialOT.Value = DateTime.Now;

                drpFecInicialRptIQ.Value = DateTime.Now;
                drpFecFinalRptIQ.Value = DateTime.Now;

                Session["sucursales"] = CMBSucursal.Items.Cast<Object>().ToArray();
                CMBSucursalIQ.Items.AddRange(CMBSucursal.Items.Cast<Object>().ToArray());
                ListaSucursales = new List<Comun>();
            }
            else
            {
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                Response.Redirect("login.aspx", false);
            }
        }

        protected void cmbSucursales_ValueChanged(object sender, EventArgs e)
        {
        }

        protected void CMBSucursalIQ_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void bschkTodasSucusalesDelGrupo_ValueChanged(object sender, EventArgs e)
        {
        }

        protected void CMBSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id_Cd = Convert.ToInt32(CMBSucursal.Value.ToString());
            cargarRIkBuscarInformacion(id_Cd);
            UpdatePanel43.Update();
        }

        private void cargarRIkBuscarInformacion(int id_Cd)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_Rik Rik = new CN_Rik();
            Rik RegistroRik = new Rik();
            RegistroRik.Id_Emp = Sesion.Id_Emp;
            RegistroRik.Id_Cd = id_Cd;
            RegistroRik.Id_Rik = -1;
            List<Rik> list_Riks = new List<Rik>();

            Rik.ConsultaRIkCrm(RegistroRik, ref list_Riks, Emp_CnxCentral);



            var query = (from m in list_Riks
                         group m.nombre_Rik by m.Id_Rik into g
                         select new { id = g.Key, nombre = g.First() }).ToList();

            CMBRIK.DataSource = query;
            CMBRIK.ValueField = "id";
            CMBRIK.TextField = "nombre";
            CMBRIK.DataBind();

            CMBRIK.Items.Add("--Todos--", "-1");
            CMBRIK.Value = "-1";

        }

        protected void CMBSucursalRIk_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["nombreRik"] = null;
            int id_Cd = Convert.ToInt32(CMBSucursalRIk.Value.ToString());
            cargarRIk(id_Cd);
            updpanel3.Update();
        }

        protected void cmbGenerarReporte_SelectedIndexChanged(object sender, EventArgs e)
        {

            int id_Cd = Convert.ToInt32(cmbGenerarReporte.Value.ToString());
            cargarRIkID(id_Cd);
        }

        private void cargarRIkID2(int id_Cd)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_Rik Rik = new CN_Rik();
            Rik RegistroRik = new Rik();

            RegistroRik.Id_Emp = Sesion.Id_Emp;
            RegistroRik.Id_Cd = id_Cd;
            RegistroRik.Id_Rik = -1;
            List<Rik> list_Riks = new List<Rik>();

            Rik.ConsultaRIkCrm(RegistroRik, ref list_Riks, Emp_CnxCentral);

            List<Rik> query = (from m in list_Riks
                               group m.nombre_Rik by m.Id_Rik into g
                               select new Rik { id = g.Key, nombre = g.First() }).ToList();


            drpGridRiks2.DataSource = query;
            drpGridRiks2.ValueField = "id";
            drpGridRiks2.TextField = "nombre";
            drpGridRiks2.DataBind();

            drpGridRiks2.Items.Add("--Todos--", "-1");
            drpGridRiks2.Value = "-1";
        }

        private void cargarRIkID(int id_Cd)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_Rik Rik = new CN_Rik();
            Rik RegistroRik = new Rik();

            RegistroRik.Id_Emp = Sesion.Id_Emp;
            RegistroRik.Id_Cd = id_Cd;
            RegistroRik.Id_Rik = -1;
            List<Rik> list_Riks = new List<Rik>();

            Rik.ConsultaRIkCrm(RegistroRik, ref list_Riks, Emp_CnxCentral);

            List<Rik> query = (from m in list_Riks
                               group m.nombre_Rik by m.Id_Rik into g
                               select new Rik { id = g.Key, nombre = g.First() }).ToList();


            cmbRikDI.DataSource = query;
            cmbRikDI.ValueField = "id";
            cmbRikDI.TextField = "nombre";
            cmbRikDI.DataBind();

            cmbRikDI.Items.Add("--Todos--", "-1");
            cmbRikDI.Value = "-1";
        }


        private void cargarRIk(int id_Cd)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_Rik Rik = new CN_Rik();
            Rik RegistroRik = new Rik();

            RegistroRik.Id_Emp = Sesion.Id_Emp;
            RegistroRik.Id_Cd = id_Cd;
            RegistroRik.Id_Rik = -1;
            List<Rik> list_Riks = new List<Rik>();

            Rik.ConsultaRIkCrm(RegistroRik, ref list_Riks, Emp_CnxCentral);

            List<Rik> query = (from m in list_Riks
                               group m.nombre_Rik by m.Id_Rik into g
                               select new Rik { id = g.Key, nombre = g.First() }).ToList();

            Session["nombreRik"] = query;
            RBLRepresentante.DataSource = query;
            RBLRepresentante.ValueField = "id";
            RBLRepresentante.TextField = "nombre";
            RBLRepresentante.DataBind();
        }

        private void cargarRIkRI(int id_Cd)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_Rik Rik = new CN_Rik();
            Rik RegistroRik = new Rik();

            RegistroRik.Id_Emp = Sesion.Id_Emp;
            RegistroRik.Id_Cd = id_Cd;
            RegistroRik.Id_Rik = -1;
            List<Rik> list_Riks = new List<Rik>();

            Rik.ConsultaRIkCrm(RegistroRik, ref list_Riks, Emp_CnxCentral);

            List<Rik> query = (from m in list_Riks
                               group m.nombre_Rik by m.Id_Rik into g
                               select new Rik { id = g.Key, nombre = g.First() }).ToList();

            Session["nombreRik"] = query;
            RBLRepresentante.DataSource = query;
            RBLRepresentante.ValueField = "id";
            RBLRepresentante.TextField = "nombre";
            RBLRepresentante.DataBind();
        }


        #region Mensajes

        /// <summary>
        /// Abre el modal de mensaje
        /// </summary>
        /// <param name="mensaje"></param>
        private void mensaje(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "modalMensaje('" + mensaje + "')", true);
        }

        /// <summary>
        /// Abre el modal de mensaje si se requiere con pregunta
        /// </summary>
        /// <param name="mensaje"></param>
        private void mensajeDecision(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modalQuestion", "modalQuestion('" + mensaje + "')", true);
        }

        #endregion

        #region webmethod

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static string ObservarTotalesCRM(string Seleccion, string sucursal, string rik, string mesAnioInicial, string mesAniofinal)
        {
            try
            {
                string Emp_CnxCentral = ConfigurationManager.AppSettings.Get("strConnectionCentral");
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];

                CN_ReporteCRM CN = new CN_ReporteCRM();
                List<ReporteCRM> listaReporteCRM = new List<ReporteCRM>();

                ReporteCRM RegistroReporteCRM = new ReporteCRM();

                if (sucursal == "-1")
                {
                    return JsonConvert.SerializeObject(new { id = 1 });
                }
                if (mesAnioInicial == "" && mesAniofinal == "")
                {
                    return JsonConvert.SerializeObject(new { id = 2 });
                }
                if (DateTime.Parse(mesAnioInicial) > DateTime.Parse(mesAniofinal))
                {
                    return JsonConvert.SerializeObject(new { id = 4 });
                }

                List<ReporteCRM> listaReportepie = new List<ReporteCRM>();


                RegistroReporteCRM.Id_Emp = Sesion.Id_Emp;
                RegistroReporteCRM.Id_Cd = Convert.ToInt32(sucursal);

                RegistroReporteCRM.Id_Rik = rik;
                RegistroReporteCRM.TipoVenta = Convert.ToInt32(Seleccion);
                RegistroReporteCRM.fechainicio = DateTime.Parse(mesAnioInicial);
                RegistroReporteCRM.fechafinal = DateTime.Parse(mesAniofinal);

                CN.ConsultaReporteGraficaCantidad(RegistroReporteCRM, ref listaReportepie, Emp_CnxCentral);

                string titulo2 = "";
                string datos2 = "";
                foreach (ReporteCRM presupuesto in listaReportepie)
                {
                    if (titulo2 == "")
                    {
                        titulo2 = presupuesto.Nombre;
                        datos2 = presupuesto.Total.ToString("F2");
                    }
                    else
                    {
                        titulo2 = titulo2 + "," + presupuesto.Nombre;
                        datos2 = datos2 + "," + presupuesto.Total.ToString("F2");
                    }
                }



                CN.ConsultaReporteGrafica(RegistroReporteCRM, ref listaReporteCRM, Emp_CnxCentral);



                string titulo = "";
                string datos = "";
                foreach (ReporteCRM presupuesto in listaReporteCRM)
                {
                    if (titulo == "")
                    {
                        titulo = presupuesto.Nombre;
                        datos = presupuesto.Total.ToString("F2");
                    }
                    else
                    {
                        titulo = titulo + "," + presupuesto.Nombre;
                        datos = datos + "," + presupuesto.Total.ToString("F2");
                    }
                }
                return JsonConvert.SerializeObject(new { id = 3, titulo = titulo, datos = datos, titulo2 = titulo2, datos2 = datos2 });



            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = ex.Message });
            }
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static string ConultarRikvsRikCRM(string Seleccion, string mesAnioInicial, string mesAniofinal)
        {
            try
            {
                string sucursal = HttpContext.Current.Session["SucursalRIk"].ToString();
                string rik = HttpContext.Current.Session["RepresentanteRik"].ToString();
                string Emp_CnxCentral = ConfigurationManager.AppSettings.Get("strConnectionCentral");
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];

                CN_ReporteCRM CN = new CN_ReporteCRM();


                ReporteCRM RegistroReporteCRM = new ReporteCRM();

                List<int> myList = new List<int>();
                RegistroReporteCRM.Id_Emp = Sesion.Id_Emp;
                RegistroReporteCRM.Id_Cd = Convert.ToInt32(sucursal);


                if (rik != "")
                {

                    string[] id_rik = rik.Split(',');
                    foreach (var value in id_rik)
                    {
                        myList.Add(Convert.ToInt32(value));
                    }
                }

                if (mesAnioInicial == "" && mesAniofinal == "")
                {
                    return JsonConvert.SerializeObject(new { id = 2 });
                }
                if (DateTime.Parse(mesAnioInicial) > DateTime.Parse(mesAniofinal))
                {
                    return JsonConvert.SerializeObject(new { id = 3 });
                }

                string nombre = "";
                string datosanalisis = "";
                string datosanPresentación = "";
                string datosanNegociación = "";
                string datosanCierre = "";
                string datosCancelación = "";

                if (Convert.ToInt32(Seleccion) == 0)
                {

                    foreach (int nombrerik in myList)
                    {
                        List<ReporteCRM> listaReporteCRM = new List<ReporteCRM>();

                        List<ReporteCRM> listaReporteCRMAnalisis = new List<ReporteCRM>();
                        List<ReporteCRM> listaReporteCRMPresentacion = new List<ReporteCRM>();
                        List<ReporteCRM> listaReporteCRMNegociacion = new List<ReporteCRM>();
                        List<ReporteCRM> listaReporteCRMCierre = new List<ReporteCRM>();
                        List<ReporteCRM> listaReporteCRMCancelacion = new List<ReporteCRM>();
                        List<Rik> RegistroRik = (List<Rik>)HttpContext.Current.Session["nombreRik"];

                        Rik list = (from tlist in RegistroRik
                                    where tlist.id == nombrerik
                                    select tlist).FirstOrDefault();

                        RegistroReporteCRM.Id_Rik = nombrerik.ToString();
                        RegistroReporteCRM.fechainicio = DateTime.Parse(mesAnioInicial);
                        RegistroReporteCRM.fechafinal = DateTime.Parse(mesAniofinal);

                        CN.ConsultaReporteGraficaCantidadProyecto(RegistroReporteCRM, ref listaReporteCRMAnalisis, ref listaReporteCRMPresentacion,
                  ref listaReporteCRMNegociacion, ref listaReporteCRMCierre, ref listaReporteCRMCancelacion, Emp_CnxCentral);



                        if (nombre == "")
                        {
                            nombre = list.nombre;
                        }
                        else
                        {
                            nombre = nombre + "," + list.nombre;
                        }

                        if (listaReporteCRMAnalisis.Count() == 0)
                        {
                            if (datosanalisis == "")
                            {
                                datosanalisis = "0"; ;
                            }
                            else
                            {
                                datosanalisis = datosanalisis + "," + "0";
                            }
                        }
                        else
                        {
                            foreach (ReporteCRM ana in listaReporteCRMAnalisis)
                            {
                                if (datosanalisis == "")
                                {
                                    datosanalisis = ana.Total.ToString("F2");
                                }
                                else
                                {
                                    datosanalisis = datosanalisis + "," + ana.Total.ToString("F2");
                                }
                            }
                        }

                        if (listaReporteCRMPresentacion.Count() == 0)
                        {
                            if (datosanPresentación == "")
                            {
                                datosanPresentación = "0"; ;
                            }
                            else
                            {
                                datosanPresentación = datosanPresentación + "," + "0";
                            }
                        }
                        else
                        {
                            foreach (ReporteCRM ana in listaReporteCRMPresentacion)
                            {
                                if (datosanPresentación == "")
                                {
                                    datosanPresentación = ana.Total.ToString("F2");
                                }
                                else
                                {
                                    datosanPresentación = datosanPresentación + "," + ana.Total.ToString("F2");
                                }
                            }
                        }

                        if (listaReporteCRMNegociacion.Count() == 0)
                        {
                            if (datosanNegociación == "")
                            {
                                datosanNegociación = "0"; ;
                            }
                            else
                            {
                                datosanNegociación = datosanNegociación + "," + "0";
                            }
                        }
                        else
                        {
                            foreach (ReporteCRM ana in listaReporteCRMNegociacion)
                            {
                                if (datosanNegociación == "")
                                {
                                    datosanNegociación = ana.Total.ToString("F2");
                                }
                                else
                                {
                                    datosanNegociación = datosanNegociación + "," + ana.Total.ToString("F2");
                                }
                            }
                        }

                        if (listaReporteCRMCierre.Count() == 0)
                        {
                            if (datosanCierre == "")
                            {
                                datosanCierre = "0"; ;
                            }
                            else
                            {
                                datosanCierre = datosanCierre + "," + "0";
                            }
                        }
                        else
                        {
                            foreach (ReporteCRM ana in listaReporteCRMCierre)
                            {
                                if (datosanCierre == "")
                                {
                                    datosanCierre = ana.Total.ToString("F2");
                                }
                                else
                                {
                                    datosanCierre = datosanCierre + "," + ana.Total.ToString("F2");
                                }
                            }
                        }

                        if (listaReporteCRMCancelacion.Count() == 0)
                        {
                            if (datosCancelación == "")
                            {
                                datosCancelación = "0"; ;
                            }
                            else
                            {
                                datosCancelación = datosCancelación + "," + "0";
                            }
                        }
                        else
                        {
                            foreach (ReporteCRM ana in listaReporteCRMCancelacion)
                            {
                                if (datosCancelación == "")
                                {
                                    datosCancelación = ana.Total.ToString("F2");
                                }
                                else
                                {
                                    datosCancelación = datosCancelación + "," + ana.Total.ToString("F2");
                                }
                            }
                        }
                    }

                    return JsonConvert.SerializeObject(new
                    {
                        id = 4,
                        Nombre = nombre,
                        Analisis = datosanalisis,
                        presentacion = datosanPresentación,
                        Negociación = datosanNegociación,
                        Cierre = datosanCierre,
                        Cancelación = datosCancelación
                    });

                }
                else
                {

                    foreach (int nombrerik in myList)
                    {

                        List<ReporteCRM> listaReporteCRM = new List<ReporteCRM>();

                        List<ReporteCRM> listaReporteCRMAnalisis = new List<ReporteCRM>();
                        List<ReporteCRM> listaReporteCRMPresentacion = new List<ReporteCRM>();
                        List<ReporteCRM> listaReporteCRMNegociacion = new List<ReporteCRM>();
                        List<ReporteCRM> listaReporteCRMCierre = new List<ReporteCRM>();
                        List<ReporteCRM> listaReporteCRMCancelacion = new List<ReporteCRM>();
                        List<Rik> RegistroRik = (List<Rik>)HttpContext.Current.Session["nombreRik"];

                        Rik list = (from tlist in RegistroRik
                                    where tlist.id == nombrerik
                                    select tlist).FirstOrDefault();

                        RegistroReporteCRM.Id_Rik = nombrerik.ToString();
                        RegistroReporteCRM.fechainicio = DateTime.Parse(mesAnioInicial);
                        RegistroReporteCRM.fechafinal = DateTime.Parse(mesAniofinal);

                        CN.ConsultaReporteGraficaMontoProyecto(RegistroReporteCRM, ref listaReporteCRMAnalisis, ref listaReporteCRMPresentacion,
                  ref listaReporteCRMNegociacion, ref listaReporteCRMCierre, ref listaReporteCRMCancelacion, Emp_CnxCentral);



                        if (nombre == "")
                        {
                            nombre = list.nombre;
                        }
                        else
                        {
                            nombre = nombre + "," + list.nombre;
                        }

                        if (listaReporteCRMAnalisis.Count() == 0)
                        {
                            if (datosanalisis == "")
                            {
                                datosanalisis = "0"; ;
                            }
                            else
                            {
                                datosanalisis = datosanalisis + "," + "0";
                            }
                        }
                        else
                        {
                            foreach (ReporteCRM ana in listaReporteCRMAnalisis)
                            {
                                if (datosanalisis == "")
                                {
                                    datosanalisis = ana.Total.ToString("F2");
                                }
                                else
                                {
                                    datosanalisis = datosanalisis + "," + ana.Total.ToString("F2");
                                }
                            }
                        }

                        if (listaReporteCRMPresentacion.Count() == 0)
                        {
                            if (datosanPresentación == "")
                            {
                                datosanPresentación = "0"; ;
                            }
                            else
                            {
                                datosanPresentación = datosanPresentación + "," + "0";
                            }
                        }
                        else
                        {
                            foreach (ReporteCRM ana in listaReporteCRMPresentacion)
                            {
                                if (datosanPresentación == "")
                                {
                                    datosanPresentación = ana.Total.ToString("F2");
                                }
                                else
                                {
                                    datosanPresentación = datosanPresentación + "," + ana.Total.ToString("F2");
                                }
                            }
                        }

                        if (listaReporteCRMNegociacion.Count() == 0)
                        {
                            if (datosanNegociación == "")
                            {
                                datosanNegociación = "0"; ;
                            }
                            else
                            {
                                datosanNegociación = datosanNegociación + "," + "0";
                            }
                        }
                        else
                        {
                            foreach (ReporteCRM ana in listaReporteCRMNegociacion)
                            {
                                if (datosanNegociación == "")
                                {
                                    datosanNegociación = ana.Total.ToString("F2");
                                }
                                else
                                {
                                    datosanNegociación = datosanNegociación + "," + ana.Total.ToString("F2");
                                }
                            }
                        }

                        if (listaReporteCRMCierre.Count() == 0)
                        {
                            if (datosanCierre == "")
                            {
                                datosanCierre = "0"; ;
                            }
                            else
                            {
                                datosanCierre = datosanCierre + "," + "0";
                            }
                        }
                        else
                        {
                            foreach (ReporteCRM ana in listaReporteCRMCierre)
                            {
                                if (datosanCierre == "")
                                {
                                    datosanCierre = ana.Total.ToString("F2");
                                }
                                else
                                {
                                    datosanCierre = datosanCierre + "," + ana.Total.ToString("F2");
                                }
                            }
                        }

                        if (listaReporteCRMCancelacion.Count() == 0)
                        {
                            if (datosCancelación == "")
                            {
                                datosCancelación = "0"; ;
                            }
                            else
                            {
                                datosCancelación = datosCancelación + "," + "0";
                            }
                        }
                        else
                        {
                            foreach (ReporteCRM ana in listaReporteCRMCancelacion)
                            {
                                if (datosCancelación == "")
                                {
                                    datosCancelación = ana.Total.ToString("F2");
                                }
                                else
                                {
                                    datosCancelación = datosCancelación + "," + ana.Total.ToString("F2");
                                }
                            }
                        }
                    }

                    return JsonConvert.SerializeObject(new
                    {
                        id = 5,
                        Nombre = nombre,
                        Analisis = datosanalisis,
                        presentacion = datosanPresentación,
                        Negociación = datosanNegociación,
                        Cierre = datosanCierre,
                        Cancelación = datosCancelación
                    });


                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = ex.Message });
            }
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static string SucursalesVsSucrusalesCRM(string Seleccion, string sucursales, string mesAnioInicial, string mesAniofinal)
        {
            try
            {
                string Emp_CnxCentral = ConfigurationManager.AppSettings.Get("strConnectionCentral");
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];

                CN_ReporteCRM CN = new CN_ReporteCRM();

                ReporteCRM RegistroReporteCRM = new ReporteCRM();

                if (sucursales == " ")
                {
                    return JsonConvert.SerializeObject(new { id = 1 });
                }
                else if (mesAnioInicial == "" && mesAniofinal == "")
                {
                    return JsonConvert.SerializeObject(new { id = 2 });
                }
                else if (DateTime.Parse(mesAnioInicial) > DateTime.Parse(mesAniofinal))
                {
                    return JsonConvert.SerializeObject(new { id = 3 });
                }
                else
                {

                    string nombre = "";
                    string datosanalisis = "";
                    string datosanPresentación = "";
                    string datosanNegociación = "";
                    string datosanCierre = "";
                    string datosCancelación = "";

                    if (Seleccion == "0")
                    {
                        List<int> ListaSucursales = new List<int>();
                        string[] id_rik = sucursales.Split(',');
                        foreach (var value in id_rik)
                        {
                            ListaSucursales.Add(Convert.ToInt32(value));
                        }



                        foreach (int id in ListaSucursales)
                        {
                            string nombreCdi = "";

                            CN.consultaCDI2(Sesion.Id_Emp, id, ref nombreCdi, Emp_CnxCentral);

                            List<ReporteCRM> listaReporteCRM = new List<ReporteCRM>();

                            List<ReporteCRM> listaReporteCRMAnalisis = new List<ReporteCRM>();
                            List<ReporteCRM> listaReporteCRMPresentacion = new List<ReporteCRM>();
                            List<ReporteCRM> listaReporteCRMNegociacion = new List<ReporteCRM>();
                            List<ReporteCRM> listaReporteCRMCierre = new List<ReporteCRM>();
                            List<ReporteCRM> listaReporteCRMCancelacion = new List<ReporteCRM>();

                            RegistroReporteCRM.Id_Emp = Sesion.Id_Emp;
                            RegistroReporteCRM.Id_Cd = Convert.ToInt32(id);

                            RegistroReporteCRM.Id_Rik = null;
                            RegistroReporteCRM.TipoVenta = Convert.ToInt32("0");
                            RegistroReporteCRM.fechainicio = DateTime.Parse(mesAnioInicial);
                            RegistroReporteCRM.fechafinal = DateTime.Parse(mesAniofinal);

                            CN.ConsultaReporteGraficaCantidad(RegistroReporteCRM, ref listaReporteCRMAnalisis, ref listaReporteCRMPresentacion,
           ref listaReporteCRMNegociacion, ref listaReporteCRMCierre, ref listaReporteCRMCancelacion, Emp_CnxCentral);

                            if (nombre == "")
                            {
                                nombre = nombreCdi;
                            }
                            else
                            {
                                nombre = nombre + "," + nombreCdi;
                            }

                            if (listaReporteCRMAnalisis.Count() == 0)
                            {
                                if (datosanalisis == "")
                                {
                                    datosanalisis = "0"; ;
                                }
                                else
                                {
                                    datosanalisis = datosanalisis + "," + "0";
                                }
                            }
                            else
                            {
                                foreach (ReporteCRM ana in listaReporteCRMAnalisis)
                                {
                                    if (datosanalisis == "")
                                    {
                                        datosanalisis = ana.Total.ToString("F2");
                                    }
                                    else
                                    {
                                        datosanalisis = datosanalisis + "," + ana.Total.ToString("F2");
                                    }
                                }
                            }

                            if (listaReporteCRMPresentacion.Count() == 0)
                            {
                                if (datosanPresentación == "")
                                {
                                    datosanPresentación = "0"; ;
                                }
                                else
                                {
                                    datosanPresentación = datosanPresentación + "," + "0";
                                }
                            }
                            else
                            {
                                foreach (ReporteCRM ana in listaReporteCRMPresentacion)
                                {
                                    if (datosanPresentación == "")
                                    {
                                        datosanPresentación = ana.Total.ToString("F2");
                                    }
                                    else
                                    {
                                        datosanPresentación = datosanPresentación + "," + ana.Total.ToString("F2");
                                    }
                                }
                            }

                            if (listaReporteCRMNegociacion.Count() == 0)
                            {
                                if (datosanNegociación == "")
                                {
                                    datosanNegociación = "0"; ;
                                }
                                else
                                {
                                    datosanNegociación = datosanNegociación + "," + "0";
                                }
                            }
                            else
                            {
                                foreach (ReporteCRM ana in listaReporteCRMNegociacion)
                                {
                                    if (datosanNegociación == "")
                                    {
                                        datosanNegociación = ana.Total.ToString("F2");
                                    }
                                    else
                                    {
                                        datosanNegociación = datosanNegociación + "," + ana.Total.ToString("F2");
                                    }
                                }
                            }

                            if (listaReporteCRMCierre.Count() == 0)
                            {
                                if (datosanCierre == "")
                                {
                                    datosanCierre = "0"; ;
                                }
                                else
                                {
                                    datosanCierre = datosanCierre + "," + "0";
                                }
                            }
                            else
                            {
                                foreach (ReporteCRM ana in listaReporteCRMCierre)
                                {
                                    if (datosanCierre == "")
                                    {
                                        datosanCierre = ana.Total.ToString("F2");
                                    }
                                    else
                                    {
                                        datosanCierre = datosanCierre + "," + ana.Total.ToString("F2");
                                    }
                                }
                            }

                            if (listaReporteCRMCancelacion.Count() == 0)
                            {
                                if (datosCancelación == "")
                                {
                                    datosCancelación = "0"; ;
                                }
                                else
                                {
                                    datosCancelación = datosCancelación + "," + "0";
                                }
                            }
                            else
                            {
                                foreach (ReporteCRM ana in listaReporteCRMCancelacion)
                                {
                                    if (datosCancelación == "")
                                    {
                                        datosCancelación = ana.Total.ToString("F2");
                                    }
                                    else
                                    {
                                        datosCancelación = datosCancelación + "," + ana.Total.ToString("F2");
                                    }
                                }
                            }

                        }

                        return JsonConvert.SerializeObject(new
                        {
                            id = 4,
                            Nombre = nombre,
                            Analisis = datosanalisis,
                            presentacion = datosanPresentación,
                            Negociación = datosanNegociación,
                            Cierre = datosanCierre,
                            Cancelación = datosCancelación
                        });

                    }
                    else
                    {

                        List<int> ListaSucursales = new List<int>();
                        string[] id_rik = sucursales.Split(',');
                        foreach (var value in id_rik)
                        {
                            ListaSucursales.Add(Convert.ToInt32(value));
                        }


                        foreach (int id in ListaSucursales)
                        {
                            string nombreCdi = "";

                            CN.consultaCDI2(Sesion.Id_Emp, id, ref nombreCdi, Emp_CnxCentral);

                            List<ReporteCRM> listaReporteCRM = new List<ReporteCRM>();

                            List<ReporteCRM> listaReporteCRMAnalisis = new List<ReporteCRM>();
                            List<ReporteCRM> listaReporteCRMPresentacion = new List<ReporteCRM>();
                            List<ReporteCRM> listaReporteCRMNegociacion = new List<ReporteCRM>();
                            List<ReporteCRM> listaReporteCRMCierre = new List<ReporteCRM>();
                            List<ReporteCRM> listaReporteCRMCancelacion = new List<ReporteCRM>();


                            RegistroReporteCRM.Id_Emp = Sesion.Id_Emp;
                            RegistroReporteCRM.Id_Cd = Convert.ToInt32(id);

                            RegistroReporteCRM.Id_Rik = null;
                            RegistroReporteCRM.TipoVenta = Convert.ToInt32("0");
                            RegistroReporteCRM.fechainicio = DateTime.Parse(mesAnioInicial);
                            RegistroReporteCRM.fechafinal = DateTime.Parse(mesAniofinal);

                            CN.ConsultaReporteGrafica(RegistroReporteCRM, ref listaReporteCRMAnalisis, ref listaReporteCRMPresentacion,
           ref listaReporteCRMNegociacion, ref listaReporteCRMCierre, ref listaReporteCRMCancelacion, Emp_CnxCentral);

                            if (nombre == "")
                            {
                                nombre = nombreCdi;
                            }
                            else
                            {
                                nombre = nombre + "," + nombreCdi;
                            }


                            if (listaReporteCRMAnalisis.Count() == 0)
                            {
                                if (datosanalisis == "")
                                {
                                    datosanalisis = "0"; ;
                                }
                                else
                                {
                                    datosanalisis = datosanalisis + "," + "0";
                                }
                            }
                            else
                            {
                                foreach (ReporteCRM ana in listaReporteCRMAnalisis)
                                {
                                    if (datosanalisis == "")
                                    {
                                        datosanalisis = ana.Total.ToString("F2");
                                    }
                                    else
                                    {
                                        datosanalisis = datosanalisis + "," + ana.Total.ToString("F2");
                                    }
                                }
                            }

                            if (listaReporteCRMPresentacion.Count() == 0)
                            {
                                if (datosanPresentación == "")
                                {
                                    datosanPresentación = "0"; ;
                                }
                                else
                                {
                                    datosanPresentación = datosanPresentación + "," + "0";
                                }
                            }
                            else
                            {
                                foreach (ReporteCRM ana in listaReporteCRMPresentacion)
                                {
                                    if (datosanPresentación == "")
                                    {
                                        datosanPresentación = ana.Total.ToString("F2");
                                    }
                                    else
                                    {
                                        datosanPresentación = datosanPresentación + "," + ana.Total.ToString("F2");
                                    }
                                }
                            }

                            if (listaReporteCRMNegociacion.Count() == 0)
                            {
                                if (datosanNegociación == "")
                                {
                                    datosanNegociación = "0"; ;
                                }
                                else
                                {
                                    datosanNegociación = datosanNegociación + "," + "0";
                                }
                            }
                            else
                            {
                                foreach (ReporteCRM ana in listaReporteCRMNegociacion)
                                {
                                    if (datosanNegociación == "")
                                    {
                                        datosanNegociación = ana.Total.ToString("F2");
                                    }
                                    else
                                    {
                                        datosanNegociación = datosanNegociación + "," + ana.Total.ToString("F2");
                                    }
                                }
                            }

                            if (listaReporteCRMCierre.Count() == 0)
                            {
                                if (datosanCierre == "")
                                {
                                    datosanCierre = "0"; ;
                                }
                                else
                                {
                                    datosanCierre = datosanCierre + "," + "0";
                                }
                            }
                            else
                            {
                                foreach (ReporteCRM ana in listaReporteCRMCierre)
                                {
                                    if (datosanCierre == "")
                                    {
                                        datosanCierre = ana.Total.ToString("F2");
                                    }
                                    else
                                    {
                                        datosanCierre = datosanCierre + "," + ana.Total.ToString("F2");
                                    }
                                }
                            }

                            if (listaReporteCRMCancelacion.Count() == 0)
                            {
                                if (datosCancelación == "")
                                {
                                    datosCancelación = "0"; ;
                                }
                                else
                                {
                                    datosCancelación = datosCancelación + "," + "0";
                                }
                            }
                            else
                            {
                                foreach (ReporteCRM ana in listaReporteCRMCancelacion)
                                {
                                    if (datosCancelación == "")
                                    {
                                        datosCancelación = ana.Total.ToString("F2");
                                    }
                                    else
                                    {
                                        datosCancelación = datosCancelación + "," + ana.Total.ToString("F2");
                                    }
                                }
                            }

                        }

                    }

                    return JsonConvert.SerializeObject(new
                    {
                        id = 5,
                        Nombre = nombre,
                        Analisis = datosanalisis,
                        presentacion = datosanPresentación,
                        Negociación = datosanNegociación,
                        Cierre = datosanCierre,
                        Cancelación = datosCancelación
                    });

                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = ex.Message });
            }
        }


        #endregion


        private void CargarAplicaciones()
        {
            try
            {
                Sesion Sesion = new CapaEntidad.Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<Comun> Lista = new List<Comun>();
                CN_InventariosNoRota CN = new CN_InventariosNoRota();

                List<Comun> ListaDosifica = new List<Comun>();
                List<Comun> ListaOtros = new List<Comun>();
                List<Comun> ListaPapel = new List<Comun>();
                List<Comun> ListaQuimicos = new List<Comun>();
                List<Comun> ListaSuplementoss = new List<Comun>();
                ListaAplicaciones = new List<Comun>();

                CN.LlenaCombo(0, Emp_CnxCentral, "spCatAplicacionesCategoria_Combo", ref Lista);
                ///     Lista = Lista.Where(x => x.Id != 1).ToList();
                if (bsChkTipoProdTodos.Checked)
                {
                    ListaAplicaciones = Lista.Select(aplica => new Comun { Id = aplica.Id, Descripcion = aplica.Descripcion.Trim().Substring(aplica.Descripcion.IndexOf('|') + 2).ToString() }).ToList();
                }
                else
                {
                    if (bsChkTipoProdDosifica.Checked)
                        ListaDosifica = Lista.Where(x => x.Descripcion.Contains("DOSIF / DESP |")).Select(aplica => new Comun { Id = aplica.Id, Descripcion = aplica.Descripcion.Trim().Substring(aplica.Descripcion.IndexOf('|') + 2).ToString() }).ToList();

                    if (bsChkTipoProdOtros.Checked)
                        ListaOtros = Lista.Where(x => x.Descripcion.Contains("OTROS |")).Select(aplica => new Comun { Id = aplica.Id, Descripcion = aplica.Descripcion.Trim().Substring(aplica.Descripcion.IndexOf('|') + 2).ToString() }).ToList();

                    if (bsChkTipoProdPapel.Checked)
                        ListaPapel = Lista.Where(x => x.Descripcion.Contains("PAPEL |")).Select(aplica => new Comun { Id = aplica.Id, Descripcion = aplica.Descripcion.Trim().Substring(aplica.Descripcion.IndexOf('|') + 2).ToString() }).ToList();

                    if (bsChkQuimicos.Checked)
                        ListaQuimicos = Lista.Where(x => x.Descripcion.Contains("QUIMICOS |")).Select(aplica => new Comun { Id = aplica.Id, Descripcion = aplica.Descripcion.Trim().Substring(aplica.Descripcion.IndexOf('|') + 2).ToString() }).ToList();

                    if (bsChkSuplemento.Checked)
                        ListaSuplementoss = Lista.Where(x => x.Descripcion.Contains("SUPLEMENTOS |")).Select(aplica => new Comun { Id = aplica.Id, Descripcion = aplica.Descripcion.Trim().Substring(aplica.Descripcion.IndexOf('|') + 2).ToString() }).ToList();

                    ListaAplicaciones.AddRange(ListaDosifica);
                    ListaAplicaciones.AddRange(ListaOtros);
                    ListaAplicaciones.AddRange(ListaPapel);
                    ListaAplicaciones.AddRange(ListaQuimicos);
                    ListaAplicaciones.AddRange(ListaSuplementoss);
                }
                this.chklstAplicaciones.Items.Clear();

                cListaApps.Clear();
                cListaApps.AddRange(ListaAplicaciones.GroupBy(i => new { i.Id, i.Descripcion }).Select(x => x.FirstOrDefault()).OrderBy(o => o.Descripcion));

                this.chklstAplicaciones.DataSource = cListaApps.GroupBy(i => new { i.Id, i.Descripcion }).Select(x => x.FirstOrDefault()).OrderBy(o => o.Descripcion);
                this.chklstAplicaciones.DataValueField = "Id";
                this.chklstAplicaciones.DataTextField = "Descripcion";
                this.chklstAplicaciones.DataBind();
                foreach (ListItem chk in this.chklstAplicaciones.Items)
                {
                    chk.Selected = true;
                }

                /*
                chklstAplicaciones.DataSource
                cmbAplicacion.DataSource = ListaAplicaciones.Distinct().OrderBy(i => i.Descripcion);
                cmbAplicacion.DataBind();
                cmbAplicacion.Enabled = true;
                */
            }
            catch (Exception ex)
            {
                throw ex;
                ///     this.lblMensaje.Text = "Error, " + ex.Message;
            }
        }

        protected void bsChkTipoProdTodos_CheckedChanged(object sender, EventArgs e)
        {
            CargarAplicaciones();
        }

        protected void bschkTodasLasAplicaciones_ValueChanged(object sender, EventArgs e)
        {
            CargarAplicaciones();
        }

        protected void bsChkTipoProdDosifica_CheckedChanged(object sender, EventArgs e)
        {
            CargarAplicaciones();
        }

        protected void bsChkTipoProdOtros_CheckedChanged(object sender, EventArgs e)
        {
            CargarAplicaciones();
        }

        protected void bsChkTipoProdPapel_CheckedChanged(object sender, EventArgs e)
        {
            CargarAplicaciones();
        }

        protected void bsChkQuimicos_CheckedChanged(object sender, EventArgs e)
        {
            CargarAplicaciones();
        }

        protected void bsChkSuplemento_CheckedChanged(object sender, EventArgs e)
        {
            CargarAplicaciones();
        }

        protected void txtSearchApp_TextChanged(object sender, EventArgs e)
        {
            string filter_param = txtSearchApp.Text.ToUpper();
            /// var items = chklstAplicaciones.Items.Cast<string>().ToList();

            List<Comun> filtered = cListaApps
                .Where(x => x.Descripcion.ToUpper().Contains(txtSearchApp.Text.ToUpper()))
                .ToList();

            chklstAplicaciones.Items.Clear();
            chklstAplicaciones.DataSource = filtered.GroupBy(i => new { i.Id, i.Descripcion }).Select(x => x.FirstOrDefault()).OrderBy(o => o.Descripcion);
            if (String.IsNullOrWhiteSpace(txtSearchApp.Text))
            {
                chklstAplicaciones.DataSource = cListaApps;
            }

            this.chklstAplicaciones.DataValueField = "Id";
            this.chklstAplicaciones.DataTextField = "Descripcion";
            this.chklstAplicaciones.DataBind();
            foreach (ListItem chk in this.chklstAplicaciones.Items)
            {
                chk.Selected = true;
            }

        }
        protected void WizardReIQ_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            //WizardReIQ.PreRender += new EventHandler(WizardReIQ_PreRender);
            //UpdatePanel3.Update();
        }

        protected void WizardReIQ_PreviousButtonClick(object sender, WizardNavigationEventArgs e)
        {
            //WizardReIQ.PreRender += new EventHandler(WizardReIQ_PreRender);
            //UpdatePanel3.Update();
        }


        public void SelectDeselectAll(bool? v)
        {
            if (v == true)
            {
                for (int i = 0; i < chklstAplicaciones.Items.Count; i++)
                    chklstAplicaciones.Items[i].Selected = true;
            }
            else if (v == false)
                chklstAplicaciones.ClearSelection();
        }

        protected void chkTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTodos.Checked)
                SelectDeselectAll(true);
            else if (!chkTodos.Checked)
                SelectDeselectAll(false);
        }

        bool DatosRptQuimicos(ref ReporteCRMQuimicos CRMQuimicos)
        {
            bool salida = false;

            DateTime fechaInicial = DateTime.Now;
            DateTime fechafinal = DateTime.Now;

            fechaInicial = drpFecInicialRptIQ.Date;
            fechafinal = drpFecFinalRptIQ.Date;
            fechafinal = fechafinal.AddMonths(1).AddDays(-1);

            if (fechaInicial > fechafinal)
            {
                mensaje("La fecha inicial es mayor a la fecha final de la sección de observar totales.");
                return salida;
            }

            string strAplicaciones = "";

            foreach (ListItem chk in this.chklstAplicaciones.Items)
            {
                if (chk.Selected == true)
                {
                    strAplicaciones = strAplicaciones + chk.Text.Trim().ToString() + " |";
                }
            }
            if (strAplicaciones == "")
            {
                mensaje("Favor de seleccionar una aplicacion para generar el documento");
                return salida;
            }

            CRMQuimicos.Id_Emp = sesion.Id_Emp;

            //var arrSucursales = cmbSucursales.GridView.GetSelectedFieldValues(cmbSucursales.KeyFieldName);
            //var arrStrSucursales = cmbSucursales.Text;
            //string strSucursales = "";
            //string strSucursalesName = "";
            //if (arrSucursales.Count() != 0)
            //{
            //    strSucursales = String.Join(",", arrSucursales);
            //    strSucursalesName = String.Join(",", arrStrSucursales);
            //}

            CRMQuimicos.IdCdis = sesion.Id_Cd_Ver.ToString();

            int TiposProductoReporte = 0;  /// Ninguna
            string strTiposProductoReporte = "";

            if (bsChkTipoProdTodos.Checked)
            {
                TiposProductoReporte = 1;
                strTiposProductoReporte = "TODOS,";
            }
            else
            {
                if (bsChkTipoProdDosifica.Checked)
                {
                    TiposProductoReporte = 3;
                    strTiposProductoReporte = strTiposProductoReporte + " 'DOSIF/DESP', ";
                }
                if (bsChkTipoProdOtros.Checked)
                {
                    TiposProductoReporte = 4;
                    strTiposProductoReporte = strTiposProductoReporte + " 'OTROS',";
                }
                if (bsChkTipoProdPapel.Checked)
                {
                    TiposProductoReporte = 5;
                    strTiposProductoReporte = strTiposProductoReporte + " 'PAPEL',";
                }
                if (bsChkQuimicos.Checked)
                {
                    TiposProductoReporte = 6;
                    strTiposProductoReporte = strTiposProductoReporte + " 'QUIMICOS',";
                }
                if (bsChkSuplemento.Checked)
                {
                    TiposProductoReporte = 7;
                    strTiposProductoReporte = strTiposProductoReporte + " 'SUPLEMENTOS',";
                }
            }

            if (strTiposProductoReporte == "")
            {
                mensaje("Favor de seleccionar una sucursal para generar el documento");
                return salida;
            }

            CRMQuimicos.strAplicaciones = strAplicaciones;
            CRMQuimicos.TipoProductoReporte = TiposProductoReporte;
            strTiposProductoReporte = strTiposProductoReporte.Substring(0, strTiposProductoReporte.Length - 1);
            CRMQuimicos.strTipoProductos = strTiposProductoReporte;



            CRMQuimicos.IdRIKs = drpGridRiks2.Value.ToString();
            //  CRMQuimicos.TipoVenta = Convert.ToInt32(cmbTipoVenta.Value.ToString());
            CRMQuimicos.fechainicio = fechaInicial;
            CRMQuimicos.fechafinal = fechafinal;

            CRMQuimicos.TipoReporte = Convert.ToInt32(bsRdlstAgrupadorQuimicos.SelectedItem.Value.ToString());

            return true;
        }
        void RptImpulsosQuimicos(ReporteCRMQuimicos CRMQuimikos)
        {
            bool PorClosed = true;

            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_ReporteCRM cn = new CN_ReporteCRM();
            List<ReporteCRMQuimicos> listaCRMQuimicos = new List<ReporteCRMQuimicos>();
            List<ReporteCRMQuimicos> listaCRMQuimicosxSucursal = new List<ReporteCRMQuimicos>();
            List<ReporteCRMQuimicos> listaCRMQuimicosxSucursalxRIK = new List<ReporteCRMQuimicos>();
            List<ReporteCRMQuimicos> listaData = new List<ReporteCRMQuimicos>();
            List<ReporteCRM> listaCRMRik = new List<ReporteCRM>();


            //var arrSucursales = cmbSucursales.GridView.GetSelectedFieldValues(cmbSucursales.KeyFieldName);
            //var arrStrSucursales = cmbSucursales.Text;
            string strSucursales = CMBSucursalIQ.TextField;
            string strSucursalesName = CMBSucursalIQ.ValueField;
            //if (arrSucursales.Count() != 0)
            //{
            //    strSucursales = String.Join(",", arrSucursales);
            //    strSucursalesName = String.Join(",", arrStrSucursales);
            //}


            string strTiposProductoReporte = "";

            if (bsChkTipoProdTodos.Checked)
            {
                strTiposProductoReporte = "TODOS,";
            }
            else
            {
                if (bsChkTipoProdDosifica.Checked)
                {
                    strTiposProductoReporte = strTiposProductoReporte + " 'DOSIF/DESP', ";
                }
                if (bsChkTipoProdOtros.Checked)
                {
                    strTiposProductoReporte = strTiposProductoReporte + " 'OTROS',";
                }
                if (bsChkTipoProdPapel.Checked)
                {
                    strTiposProductoReporte = strTiposProductoReporte + " 'PAPEL',";
                }
                if (bsChkQuimicos.Checked)
                {
                    strTiposProductoReporte = strTiposProductoReporte + " 'QUIMICOS',";
                }
                if (bsChkSuplemento.Checked)
                {
                    strTiposProductoReporte = strTiposProductoReporte + " 'SUPLEMENTOS',";
                }
            }
            strTiposProductoReporte = strTiposProductoReporte.Substring(0, strTiposProductoReporte.Length - 1);

            cn.ConsultaReporteExcelImpulsosQuimicos(CRMQuimikos, ref listaCRMQuimicos, ref listaCRMQuimicosxSucursal, ref listaCRMQuimicosxSucursalxRIK, ref listaData, Emp_CnxCentral);

            var lstDisitnct = listaData.Where(aa => aa.Data_Estatus != 1).GroupBy(l => new { l.Data_Situacion, l.Data_Id_Cd })
                .Select(g => new
                {
                    Situacion = g.Key.Data_Situacion,
                    IdCD = g.Key.Data_Id_Cd,
                    Kount = g.Select(l => l.Data_IdProyecto).Distinct().Count()
                });

            int iTotalProy = lstDisitnct.Where(aa => aa.Situacion == 2 || aa.Situacion == 1).Select(a => a.Kount).Sum();
            int iActualProy = lstDisitnct.Where(aa => aa.Situacion == 2).Select(a => a.Kount).Sum();
            int iNuevoProy = lstDisitnct.Where(aa => aa.Situacion == 1).Select(a => a.Kount).Sum();
            int iCerradoProy = lstDisitnct.Where(aa => aa.Situacion == 3).Select(a => a.Kount).Sum();
            int iCancelProy = lstDisitnct.Where(aa => aa.Situacion == 4).Select(a => a.Kount).Sum();

            int iTotalProyCDI = 0;
            int iActualProyCDI = 0;
            int iNuevoProyCDI = 0;
            int iCerradoProyCDI = 0;
            int iCancelProyCDI = 0;

            int iTotalProyCDIRIK = 0;
            int iActualProyCDIRIK = 0;
            int iNuevoProyCDIRIK = 0;
            int iCerradoProyCDIRIK = 0;
            int iCancelProyCDIRIK = 0;

            var LstGeneral = listaCRMQuimicos.GroupBy(l => l.Id_Cd)
              .Select(la =>
                    new
                    {
                        idCDI = la.Key,
                        NoProyectos = iTotalProy,           /// la.Distinct(s => s.Data_IdProyecto),
                        SumaProyectos = la.Sum(s => s.MontoFinal_Monto),
                        NoProyActuales = iActualProy,       /// la.Sum(s => s.ProyectosActuales_Proy),
                        SumaProyActuales = la.Sum(s => s.ProyectosActuales_Monto),
                        NoProyNuevos = iNuevoProy,          /// la.Sum(s => s.ProyectosNuevos_Proy),
                        SumaProyNuevos = la.Sum(s => s.ProyectosNuevos_Monto),
                        NoProyCerrados = iCerradoProy,      /// la.Sum(s => s.ProyectosCerrados_Proy),
                        SumaProyCerrados = la.Sum(s => s.ProyectosCerrados_Monto),
                        NoProyCancelados = iCancelProy,     /// la.Sum(s => s.ProyectosCancelados_Proy),
                        SumaProyCancelados = la.Sum(s => s.ProyectosCancelados_Monto),
                    })
              .ToList();

            var LstCDI = listaCRMQuimicosxSucursal.GroupBy(l => l.Id_Cd)
                          .Select(la =>
                                new
                                {
                                    idCDI = la.Key,
                                    NoProyectos = la.Sum(s => s.MontoFinal_Proy),
                                    SumaProyectos = la.Sum(s => s.MontoFinal_Monto),
                                    NoProyActuales = la.Sum(s => s.ProyectosActuales_Proy),
                                    SumaProyActuales = la.Sum(s => s.ProyectosActuales_Monto),
                                    NoProyNuevos = la.Sum(s => s.ProyectosNuevos_Proy),
                                    SumaProyNuevos = la.Sum(s => s.ProyectosNuevos_Monto),
                                    NoProyCerrados = la.Sum(s => s.ProyectosCerrados_Proy),
                                    SumaProyCerrados = la.Sum(s => s.ProyectosCerrados_Monto),
                                    NoProyCancelados = la.Sum(s => s.ProyectosCancelados_Proy),
                                    SumaProyCancelados = la.Sum(s => s.ProyectosCancelados_Monto),
                                })
                          .ToList();

            var LstCDIRIK = listaCRMQuimicosxSucursalxRIK.GroupBy(l => new { l.Id_RIK, l.Id_Cd })
                          .Select(la =>
                                new
                                {
                                    idCDI = la.Key.Id_Cd,
                                    idRIIK = la.Key.Id_RIK,
                                    NoProyectos = la.Sum(s => s.MontoFinal_Proy),
                                    SumaProyectos = la.Sum(s => s.MontoFinal_Monto),
                                    NoProyActuales = la.Sum(s => s.ProyectosActuales_Proy),
                                    SumaProyActuales = la.Sum(s => s.ProyectosActuales_Monto),
                                    NoProyNuevos = la.Sum(s => s.ProyectosNuevos_Proy),
                                    SumaProyNuevos = la.Sum(s => s.ProyectosNuevos_Monto),
                                    NoProyCerrados = la.Sum(s => s.ProyectosCerrados_Proy),
                                    SumaProyCerrados = la.Sum(s => s.ProyectosCerrados_Monto),
                                    NoProyCancelados = la.Sum(s => s.ProyectosCancelados_Proy),
                                    SumaProyCancelados = la.Sum(s => s.ProyectosCancelados_Monto),
                                })
                          .ToList();

            string strCDII = "";
            string strCDII2 = "";
            int cont = 7;
            ExportExcel exportador = new ExportExcel();

            if (PorClosed)
            {
                using (var workboook = new XLWorkbook())
                {
                    var HojaXcel = workboook.Worksheets.Add("General");

                    HojaXcel.Range("A1").SetValue("Periodo:");
                    HojaXcel.Range("B1").SetValue(drpFecInicialRptIQ.Value.ToString());
                    HojaXcel.Range("B1:K1").Merge();
                    HojaXcel.Range("A2").SetValue("Tipo de producto:");
                    HojaXcel.Range("B2").SetValue(strTiposProductoReporte);
                    HojaXcel.Range("B2:K2").Merge();
                    HojaXcel.Range("A3").SetValue("Sucursal:");
                    HojaXcel.Range("B3").SetValue(strSucursalesName);
                    HojaXcel.Range("B3:K3").Merge();

                    HojaXcel.Range("A5:A6").Merge();
                    exportador.CeldaHeaderAgrupador("B5:C5", "Monto Final", ref HojaXcel);
                    exportador.CeldaHeader("B6", "#", ref HojaXcel);
                    exportador.CeldaHeader("C6", "$", ref HojaXcel);

                    exportador.CeldaHeaderAgrupador("D5:E5", "Proyectos actuales", ref HojaXcel);
                    exportador.CeldaHeader("D6", "#", ref HojaXcel);
                    exportador.CeldaHeader("E6", "$", ref HojaXcel);

                    exportador.CeldaHeaderAgrupador("F5:G5", "Proyectos nuevos", ref HojaXcel);
                    exportador.CeldaHeader("F6", "#", ref HojaXcel);
                    exportador.CeldaHeader("G6", "$", ref HojaXcel);

                    exportador.CeldaHeaderAgrupador("H5:I5", "Proyectos cerrados", ref HojaXcel);
                    exportador.CeldaHeader("H6", "#", ref HojaXcel);
                    exportador.CeldaHeader("I6", "$", ref HojaXcel);

                    exportador.CeldaHeaderAgrupador("J5:K5", "Proyectos cancelados", ref HojaXcel);
                    exportador.CeldaHeader("J6", "#", ref HojaXcel);
                    exportador.CeldaHeader("K6", "$", ref HojaXcel);

                    foreach (ReporteCRMQuimicos itemm in listaCRMQuimicos)
                    {

                        if (strCDII == "")
                        {
                            strCDII = itemm.strCDI;
                            exportador.CeldaDatosBold("A" + cont.ToString() + ":A" + cont.ToString(), "General", XLAlignmentHorizontalValues.Left, ref HojaXcel);

                            var itemLstGnl = LstGeneral.Where(k => k.idCDI == itemm.Id_Cd)
                              .FirstOrDefault();

                            exportador.CeldaDatoInt("B" + cont.ToString(), itemLstGnl.NoProyectos, XLAlignmentHorizontalValues.Center, true, false, ref HojaXcel);
                            exportador.CeldaDatosFloat("C" + cont.ToString(), (float)itemLstGnl.SumaProyectos, XLAlignmentHorizontalValues.Right, true, false, ref HojaXcel);

                            exportador.CeldaDatoInt("D" + cont.ToString(), itemLstGnl.NoProyActuales, XLAlignmentHorizontalValues.Center, true, false, ref HojaXcel);
                            exportador.CeldaDatosFloat("E" + cont.ToString(), (float)itemLstGnl.SumaProyActuales, XLAlignmentHorizontalValues.Right, true, false, ref HojaXcel);

                            exportador.CeldaDatoInt("F" + cont.ToString(), itemLstGnl.NoProyNuevos, XLAlignmentHorizontalValues.Center, true, false, ref HojaXcel);
                            exportador.CeldaDatosFloat("G" + cont.ToString(), (float)itemLstGnl.SumaProyNuevos, XLAlignmentHorizontalValues.Right, true, false, ref HojaXcel);

                            exportador.CeldaDatoInt("H" + cont.ToString(), itemLstGnl.NoProyCerrados, XLAlignmentHorizontalValues.Center, true, false, ref HojaXcel);
                            exportador.CeldaDatosFloat("I" + cont.ToString(), (float)itemLstGnl.SumaProyCerrados, XLAlignmentHorizontalValues.Right, true, false, ref HojaXcel);

                            exportador.CeldaDatoInt("J" + cont.ToString(), itemLstGnl.NoProyCancelados, XLAlignmentHorizontalValues.Center, true, false, ref HojaXcel);
                            exportador.CeldaDatosFloat("K" + cont.ToString(), (float)itemLstGnl.SumaProyCancelados, XLAlignmentHorizontalValues.Right, true, false, ref HojaXcel);

                            cont = cont + 1;
                        }
                        else
                        {
                            if (strCDII != itemm.strCDI)
                            {
                                strCDII = itemm.strCDI;
                                exportador.CeldaDatosBold("A" + cont.ToString() + ":A" + cont.ToString(), "General", XLAlignmentHorizontalValues.Left, ref HojaXcel);

                                var itemLstGnl = LstGeneral.Where(k => k.idCDI == itemm.Id_Cd)
                                  .FirstOrDefault();

                                exportador.CeldaDatoInt("B" + cont.ToString(), itemLstGnl.NoProyectos, XLAlignmentHorizontalValues.Center, true, false, ref HojaXcel);
                                exportador.CeldaDatosFloat("C" + cont.ToString(), (float)itemLstGnl.SumaProyectos, XLAlignmentHorizontalValues.Right, true, false, ref HojaXcel);

                                exportador.CeldaDatoInt("D" + cont.ToString(), itemLstGnl.NoProyActuales, XLAlignmentHorizontalValues.Center, true, false, ref HojaXcel);
                                exportador.CeldaDatosFloat("E" + cont.ToString(), (float)itemLstGnl.SumaProyActuales, XLAlignmentHorizontalValues.Right, true, false, ref HojaXcel);

                                exportador.CeldaDatoInt("F" + cont.ToString(), itemLstGnl.NoProyNuevos, XLAlignmentHorizontalValues.Center, true, false, ref HojaXcel);
                                exportador.CeldaDatosFloat("G" + cont.ToString(), (float)itemLstGnl.SumaProyNuevos, XLAlignmentHorizontalValues.Right, true, false, ref HojaXcel);

                                exportador.CeldaDatoInt("H" + cont.ToString(), itemLstGnl.NoProyCerrados, XLAlignmentHorizontalValues.Center, true, false, ref HojaXcel);
                                exportador.CeldaDatosFloat("I" + cont.ToString(), (float)itemLstGnl.SumaProyCerrados, XLAlignmentHorizontalValues.Right, true, false, ref HojaXcel);

                                exportador.CeldaDatoInt("J" + cont.ToString(), itemLstGnl.NoProyCancelados, XLAlignmentHorizontalValues.Center, true, false, ref HojaXcel);
                                exportador.CeldaDatosFloat("K" + cont.ToString(), (float)itemLstGnl.SumaProyCancelados, XLAlignmentHorizontalValues.Right, true, false, ref HojaXcel);

                                cont = cont + 1;
                            }
                        }


                        /// DescAgrupador   TotalProyectos TotalProyectos_Monto    ProyectosActuales ProyectosActuales_Monto 
                        /// ProyectosNuevo ProyectosNuevo_Monto    ProyectosCerrados ProyectosCerrados_Monto ProyectosCancelados ProyectosCancelados_Monto
                        exportador.CeldaDatos("A" + cont.ToString(), itemm.strAgrupador, XLAlignmentHorizontalValues.Left, ref HojaXcel);

                        exportador.CeldaDatoInt("B" + cont.ToString(), itemm.MontoFinal_Proy, XLAlignmentHorizontalValues.Center, false, false, ref HojaXcel);
                        exportador.CeldaDatosFloat("C" + cont.ToString(), (float)itemm.MontoFinal_Monto, XLAlignmentHorizontalValues.Right, false, false, ref HojaXcel);

                        exportador.CeldaDatoInt("D" + cont.ToString(), itemm.ProyectosActuales_Proy, XLAlignmentHorizontalValues.Center, false, false, ref HojaXcel);
                        exportador.CeldaDatosFloat("E" + cont.ToString(), (float)itemm.ProyectosActuales_Monto, XLAlignmentHorizontalValues.Right, false, false, ref HojaXcel);

                        exportador.CeldaDatoInt("F" + cont.ToString(), itemm.ProyectosNuevos_Proy, XLAlignmentHorizontalValues.Center, false, false, ref HojaXcel);
                        exportador.CeldaDatosFloat("G" + cont.ToString(), (float)itemm.ProyectosNuevos_Monto, XLAlignmentHorizontalValues.Right, false, false, ref HojaXcel);

                        exportador.CeldaDatoInt("H" + cont.ToString(), itemm.ProyectosCerrados_Proy, XLAlignmentHorizontalValues.Center, false, false, ref HojaXcel);
                        exportador.CeldaDatosFloat("I" + cont.ToString(), (float)itemm.ProyectosCerrados_Monto, XLAlignmentHorizontalValues.Right, false, false, ref HojaXcel);

                        exportador.CeldaDatoInt("J" + cont.ToString(), itemm.ProyectosCancelados_Proy, XLAlignmentHorizontalValues.Center, false, false, ref HojaXcel);
                        exportador.CeldaDatosFloat("K" + cont.ToString(), (float)itemm.ProyectosCancelados_Monto, XLAlignmentHorizontalValues.Right, false, false, ref HojaXcel);

                        cont = cont + 1;
                    }

                    foreach (var item in HojaXcel.ColumnsUsed())
                    {
                        item.AdjustToContents();
                    }

                    ///-----------------------------------------------------------------------------------------------------

                    var HojaXcel2 = workboook.Worksheets.Add("Sucursal");
                    HojaXcel2.Range("A3:A4").Merge();
                    exportador.CeldaHeaderAgrupador("B3:C3", "Monto Final", ref HojaXcel2);
                    exportador.CeldaHeader("B4", "#", ref HojaXcel2);
                    exportador.CeldaHeader("C4", "$", ref HojaXcel2);

                    exportador.CeldaHeaderAgrupador("D3:E3", "Proyectos actuales", ref HojaXcel2);
                    exportador.CeldaHeader("D4", "#", ref HojaXcel2);
                    exportador.CeldaHeader("E4", "$", ref HojaXcel2);

                    exportador.CeldaHeaderAgrupador("F3:G3", "Proyectos nuevos", ref HojaXcel2);
                    exportador.CeldaHeader("F4", "#", ref HojaXcel2);
                    exportador.CeldaHeader("G4", "$", ref HojaXcel2);

                    exportador.CeldaHeaderAgrupador("H3:I3", "Proyectos cerrados", ref HojaXcel2);
                    exportador.CeldaHeader("H4", "#", ref HojaXcel2);
                    exportador.CeldaHeader("I4", "$", ref HojaXcel2);

                    exportador.CeldaHeaderAgrupador("J3:K3", "Proyectos cancelados", ref HojaXcel2);
                    exportador.CeldaHeader("J4", "#", ref HojaXcel2);
                    exportador.CeldaHeader("K4", "$", ref HojaXcel2);

                    cont = 5;
                    strCDII = "";
                    foreach (ReporteCRMQuimicos itemm in listaCRMQuimicosxSucursal)
                    {
                        if (strCDII == "")
                        {
                            strCDII = itemm.strCDI;
                            exportador.CeldaDatosBold("A" + cont.ToString() + ":A" + cont.ToString(), itemm.strCDI, XLAlignmentHorizontalValues.Left, ref HojaXcel2);

                            var itemLstCDI = LstCDI.Where(k => k.idCDI == itemm.Id_Cd)
                              .FirstOrDefault();


                            var lstDisitnctCDI = listaData.Where(aa => aa.Data_Estatus != 1 && aa.Data_Id_Cd == itemm.Id_Cd).GroupBy(l => new { l.Data_Situacion, l.Data_Id_Cd })
                                .Select(g => new
                                {
                                    Situacion = g.Key.Data_Situacion,
                                    id_Cd = g.Key.Data_Id_Cd,
                                    Kount = g.Select(l => l.Data_IdProyecto).Distinct().Count()
                                });

                            iTotalProyCDI = lstDisitnctCDI.Where(aa => aa.Situacion == 2 || aa.Situacion == 1).Select(a => a.Kount).Sum();
                            iActualProyCDI = lstDisitnctCDI.Where(aa => aa.Situacion == 2).Select(a => a.Kount).Sum();
                            iNuevoProyCDI = lstDisitnctCDI.Where(aa => aa.Situacion == 1).Select(a => a.Kount).Sum();
                            iCerradoProyCDI = lstDisitnctCDI.Where(aa => aa.Situacion == 3).Select(a => a.Kount).Sum();
                            iCancelProyCDI = lstDisitnctCDI.Where(aa => aa.Situacion == 4).Select(a => a.Kount).Sum();


                            exportador.CeldaDatoInt("B" + cont.ToString(), iTotalProyCDI /* itemLstCDI.NoProyectos */ , XLAlignmentHorizontalValues.Center, true, false, ref HojaXcel2);
                            exportador.CeldaDatosFloat("C" + cont.ToString(), (float)itemLstCDI.SumaProyectos, XLAlignmentHorizontalValues.Right, true, false, ref HojaXcel2);

                            exportador.CeldaDatoInt("D" + cont.ToString(), iActualProyCDI /* itemLstCDI.NoProyActuales */ , XLAlignmentHorizontalValues.Center, true, false, ref HojaXcel2);
                            exportador.CeldaDatosFloat("E" + cont.ToString(), (float)itemLstCDI.SumaProyActuales, XLAlignmentHorizontalValues.Right, true, false, ref HojaXcel2);

                            exportador.CeldaDatoInt("F" + cont.ToString(), iNuevoProyCDI /* itemLstCDI.NoProyNuevos */, XLAlignmentHorizontalValues.Center, true, false, ref HojaXcel2);
                            exportador.CeldaDatosFloat("G" + cont.ToString(), (float)itemLstCDI.SumaProyNuevos, XLAlignmentHorizontalValues.Right, true, false, ref HojaXcel2);

                            exportador.CeldaDatoInt("H" + cont.ToString(), iCerradoProyCDI /* itemLstCDI.NoProyCerrados*/ , XLAlignmentHorizontalValues.Center, true, false, ref HojaXcel2);
                            exportador.CeldaDatosFloat("I" + cont.ToString(), (float)itemLstCDI.SumaProyCerrados, XLAlignmentHorizontalValues.Right, true, false, ref HojaXcel2);

                            exportador.CeldaDatoInt("J" + cont.ToString(), iCancelProyCDI /* itemLstCDI.NoProyCancelados */, XLAlignmentHorizontalValues.Center, true, false, ref HojaXcel2);
                            exportador.CeldaDatosFloat("K" + cont.ToString(), (float)itemLstCDI.SumaProyCancelados, XLAlignmentHorizontalValues.Right, true, false, ref HojaXcel2);

                            cont = cont + 1;
                        }
                        else
                        {
                            if (strCDII != itemm.strCDI)
                            {
                                strCDII = itemm.strCDI;
                                exportador.CeldaDatosBold("A" + cont.ToString() + ":A" + cont.ToString(), itemm.strCDI, XLAlignmentHorizontalValues.Left, ref HojaXcel2);

                                var itemLstCDI = LstCDI.Where(k => k.idCDI == itemm.Id_Cd)
                                  .FirstOrDefault();

                                var lstDisitnctCDI = listaData.Where(aa => aa.Data_Estatus != 1 && aa.Data_Id_Cd == itemm.Id_Cd).GroupBy(l => new { l.Data_Situacion, l.Data_Id_Cd })
                                    .Select(g => new
                                    {
                                        Situacion = g.Key.Data_Situacion,
                                        id_Cd = g.Key.Data_Id_Cd,
                                        Kount = g.Select(l => l.Data_IdProyecto).Distinct().Count()
                                    });

                                iTotalProyCDI = lstDisitnctCDI.Where(aa => aa.Situacion == 2 || aa.Situacion == 1).Select(a => a.Kount).Sum();
                                iActualProyCDI = lstDisitnctCDI.Where(aa => aa.Situacion == 2).Select(a => a.Kount).Sum();
                                iNuevoProyCDI = lstDisitnctCDI.Where(aa => aa.Situacion == 1).Select(a => a.Kount).Sum();
                                iCerradoProyCDI = lstDisitnctCDI.Where(aa => aa.Situacion == 3).Select(a => a.Kount).Sum();
                                iCancelProyCDI = lstDisitnctCDI.Where(aa => aa.Situacion == 4).Select(a => a.Kount).Sum();

                                exportador.CeldaDatoInt("B" + cont.ToString(), iTotalProyCDI /* itemLstCDI.NoProyectos */, XLAlignmentHorizontalValues.Center, true, false, ref HojaXcel2);
                                exportador.CeldaDatosFloat("C" + cont.ToString(), (float)itemLstCDI.SumaProyectos, XLAlignmentHorizontalValues.Right, true, false, ref HojaXcel2);

                                exportador.CeldaDatoInt("D" + cont.ToString(), iActualProyCDI /* itemLstCDI.NoProyActuales */, XLAlignmentHorizontalValues.Center, true, false, ref HojaXcel2);
                                exportador.CeldaDatosFloat("E" + cont.ToString(), (float)itemLstCDI.SumaProyActuales, XLAlignmentHorizontalValues.Right, true, false, ref HojaXcel2);

                                exportador.CeldaDatoInt("F" + cont.ToString(), iNuevoProyCDI /* itemLstCDI.NoProyNuevos */, XLAlignmentHorizontalValues.Center, true, false, ref HojaXcel2);
                                exportador.CeldaDatosFloat("G" + cont.ToString(), (float)itemLstCDI.SumaProyNuevos, XLAlignmentHorizontalValues.Right, true, false, ref HojaXcel2);

                                exportador.CeldaDatoInt("H" + cont.ToString(), iCerradoProyCDI /* itemLstCDI.NoProyCerrados */, XLAlignmentHorizontalValues.Center, true, false, ref HojaXcel2);
                                exportador.CeldaDatosFloat("I" + cont.ToString(), (float)itemLstCDI.SumaProyCerrados, XLAlignmentHorizontalValues.Right, true, false, ref HojaXcel2);

                                exportador.CeldaDatoInt("J" + cont.ToString(), iCancelProyCDI /* itemLstCDI.NoProyCancelados */ , XLAlignmentHorizontalValues.Center, true, false, ref HojaXcel2);
                                exportador.CeldaDatosFloat("K" + cont.ToString(), (float)itemLstCDI.SumaProyCancelados, XLAlignmentHorizontalValues.Right, true, false, ref HojaXcel2);

                                cont = cont + 1;
                            }
                        }
                        /// DescAgrupador   TotalProyectos TotalProyectos_Monto    ProyectosActuales ProyectosActuales_Monto 
                        /// ProyectosNuevo ProyectosNuevo_Monto    ProyectosCerrados ProyectosCerrados_Monto ProyectosCancelados ProyectosCancelados_Monto
                        exportador.CeldaDatos("A" + cont.ToString(), itemm.strAgrupador, XLAlignmentHorizontalValues.Left, ref HojaXcel2);

                        exportador.CeldaDatoInt("B" + cont.ToString(), itemm.MontoFinal_Proy, XLAlignmentHorizontalValues.Center, false, false, ref HojaXcel2);
                        exportador.CeldaDatosFloat("C" + cont.ToString(), (float)itemm.MontoFinal_Monto, XLAlignmentHorizontalValues.Right, false, false, ref HojaXcel2);

                        exportador.CeldaDatoInt("D" + cont.ToString(), itemm.ProyectosActuales_Proy, XLAlignmentHorizontalValues.Center, false, false, ref HojaXcel2);
                        exportador.CeldaDatosFloat("E" + cont.ToString(), (float)itemm.ProyectosActuales_Monto, XLAlignmentHorizontalValues.Right, false, false, ref HojaXcel2);

                        exportador.CeldaDatoInt("F" + cont.ToString(), itemm.ProyectosNuevos_Proy, XLAlignmentHorizontalValues.Center, false, false, ref HojaXcel2);
                        exportador.CeldaDatosFloat("G" + cont.ToString(), (float)itemm.ProyectosNuevos_Monto, XLAlignmentHorizontalValues.Right, false, false, ref HojaXcel2);

                        exportador.CeldaDatoInt("H" + cont.ToString(), itemm.ProyectosCerrados_Proy, XLAlignmentHorizontalValues.Center, false, false, ref HojaXcel2);
                        exportador.CeldaDatosFloat("I" + cont.ToString(), (float)itemm.ProyectosCerrados_Monto, XLAlignmentHorizontalValues.Right, false, false, ref HojaXcel2);

                        exportador.CeldaDatoInt("J" + cont.ToString(), itemm.ProyectosCancelados_Proy, XLAlignmentHorizontalValues.Center, false, false, ref HojaXcel2);
                        exportador.CeldaDatosFloat("K" + cont.ToString(), (float)itemm.ProyectosCancelados_Monto, XLAlignmentHorizontalValues.Right, false, false, ref HojaXcel2);

                        cont = cont + 1;
                    }

                    foreach (var item in HojaXcel2.ColumnsUsed())
                    {
                        item.AdjustToContents();
                    }

                    ///-----------------------------------------------------------------------------------------------------

                    var HojaXcel3 = workboook.Worksheets.Add("RIK");
                    HojaXcel3.Range("A3:A4").Merge();
                    exportador.CeldaHeaderAgrupador("B3:C3", "Monto Final", ref HojaXcel3);
                    exportador.CeldaHeader("B4", "#", ref HojaXcel3);
                    exportador.CeldaHeader("C4", "$", ref HojaXcel3);

                    exportador.CeldaHeaderAgrupador("D3:E3", "Proyectos actuales", ref HojaXcel3);
                    exportador.CeldaHeader("D4", "#", ref HojaXcel3);
                    exportador.CeldaHeader("E4", "$", ref HojaXcel3);

                    exportador.CeldaHeaderAgrupador("F3:G3", "Proyectos nuevos", ref HojaXcel3);
                    exportador.CeldaHeader("F4", "#", ref HojaXcel3);
                    exportador.CeldaHeader("G4", "$", ref HojaXcel3);

                    exportador.CeldaHeaderAgrupador("H3:I3", "Proyectos cerrados", ref HojaXcel3);
                    exportador.CeldaHeader("H4", "#", ref HojaXcel3);
                    exportador.CeldaHeader("I4", "$", ref HojaXcel3);

                    exportador.CeldaHeaderAgrupador("J3:K3", "Proyectos cancelados", ref HojaXcel3);
                    exportador.CeldaHeader("J4", "#", ref HojaXcel3);
                    exportador.CeldaHeader("K4", "$", ref HojaXcel3);


                    string strCDIIR = "";
                    /// List<ReporteCRMQuimicos> LstCDI = new List<ReporteCRMQuimicos>();
                    string strRIIK = "";
                    cont = 5;


                    foreach (ReporteCRMQuimicos itemm in listaCRMQuimicosxSucursalxRIK)
                    {
                        if (strCDIIR == "")
                        {
                            strCDIIR = itemm.strCDI;

                            exportador.CeldaDatosBold("A" + cont.ToString() + ":A" + cont.ToString(), itemm.strCDI, XLAlignmentHorizontalValues.Left, ref HojaXcel3);

                            var itemLstCDI = LstCDI.Where(k => k.idCDI == itemm.Id_Cd)
                              .FirstOrDefault();


                            var lstDisitnctCDI = listaData.Where(aa => aa.Data_Estatus != 1 && aa.Data_Id_Cd == itemm.Id_Cd).GroupBy(l => new { l.Data_Situacion, l.Data_Id_Cd })
                                .Select(g => new
                                {
                                    Situacion = g.Key.Data_Situacion,
                                    id_Cd = g.Key.Data_Id_Cd,
                                    Kount = g.Select(l => l.Data_IdProyecto).Distinct().Count()
                                });

                            iTotalProyCDI = lstDisitnctCDI.Where(aa => aa.Situacion == 2 || aa.Situacion == 1).Select(a => a.Kount).Sum();
                            iActualProyCDI = lstDisitnctCDI.Where(aa => aa.Situacion == 2).Select(a => a.Kount).Sum();
                            iNuevoProyCDI = lstDisitnctCDI.Where(aa => aa.Situacion == 1).Select(a => a.Kount).Sum();
                            iCerradoProyCDI = lstDisitnctCDI.Where(aa => aa.Situacion == 3).Select(a => a.Kount).Sum();
                            iCancelProyCDI = lstDisitnctCDI.Where(aa => aa.Situacion == 4).Select(a => a.Kount).Sum();


                            exportador.CeldaDatoInt("B" + cont.ToString(), iTotalProyCDI /* itemLstCDI.NoProyectos */ , XLAlignmentHorizontalValues.Center, true, false, ref HojaXcel3);
                            exportador.CeldaDatosFloat("C" + cont.ToString(), (float)itemLstCDI.SumaProyectos, XLAlignmentHorizontalValues.Right, true, false, ref HojaXcel3);

                            exportador.CeldaDatoInt("D" + cont.ToString(), iActualProyCDI /* itemLstCDI.NoProyActuales */ , XLAlignmentHorizontalValues.Center, true, false, ref HojaXcel3);
                            exportador.CeldaDatosFloat("E" + cont.ToString(), (float)itemLstCDI.SumaProyActuales, XLAlignmentHorizontalValues.Right, true, false, ref HojaXcel3);

                            exportador.CeldaDatoInt("F" + cont.ToString(), iNuevoProyCDI /* itemLstCDI.NoProyNuevos */ , XLAlignmentHorizontalValues.Center, true, false, ref HojaXcel3);
                            exportador.CeldaDatosFloat("G" + cont.ToString(), (float)itemLstCDI.SumaProyNuevos, XLAlignmentHorizontalValues.Right, true, false, ref HojaXcel3);

                            exportador.CeldaDatoInt("H" + cont.ToString(), iCerradoProyCDI /* itemLstCDI.NoProyCerrados */ , XLAlignmentHorizontalValues.Center, true, false, ref HojaXcel3);
                            exportador.CeldaDatosFloat("I" + cont.ToString(), (float)itemLstCDI.SumaProyCerrados, XLAlignmentHorizontalValues.Right, true, false, ref HojaXcel3);

                            exportador.CeldaDatoInt("J" + cont.ToString(), iCancelProyCDI  /*  itemLstCDI.NoProyCancelados */ , XLAlignmentHorizontalValues.Center, true, false, ref HojaXcel3);
                            exportador.CeldaDatosFloat("K" + cont.ToString(), (float)itemLstCDI.SumaProyCancelados, XLAlignmentHorizontalValues.Right, true, false, ref HojaXcel3);

                            cont = cont + 1;
                        }
                        else
                        {
                            if (strCDIIR != itemm.strCDI)
                            {
                                strCDIIR = itemm.strCDI;
                                exportador.CeldaDatosBold("A" + cont.ToString() + ":A" + cont.ToString(), itemm.strCDI, XLAlignmentHorizontalValues.Left, ref HojaXcel3);

                                var itemLstCDI = LstCDI.Where(k => k.idCDI == itemm.Id_Cd)
                                  .FirstOrDefault();

                                var lstDisitnctCDI = listaData.Where(aa => aa.Data_Estatus != 1 && aa.Data_Id_Cd == itemm.Id_Cd).GroupBy(l => new { l.Data_Situacion, l.Data_Id_Cd })
                                    .Select(g => new
                                    {
                                        Situacion = g.Key.Data_Situacion,
                                        id_Cd = g.Key.Data_Id_Cd,
                                        Kount = g.Select(l => l.Data_IdProyecto).Distinct().Count()
                                    });


                                iTotalProyCDI = lstDisitnctCDI.Where(aa => aa.Situacion == 2 || aa.Situacion == 1).Select(a => a.Kount).Sum();
                                iActualProyCDI = lstDisitnctCDI.Where(aa => aa.Situacion == 2).Select(a => a.Kount).Sum();
                                iNuevoProyCDI = lstDisitnctCDI.Where(aa => aa.Situacion == 1).Select(a => a.Kount).Sum();
                                iCerradoProyCDI = lstDisitnctCDI.Where(aa => aa.Situacion == 3).Select(a => a.Kount).Sum();
                                iCancelProyCDI = lstDisitnctCDI.Where(aa => aa.Situacion == 4).Select(a => a.Kount).Sum();

                                exportador.CeldaDatoInt("B" + cont.ToString(), iTotalProyCDI /* itemLstCDI.NoProyectos */ , XLAlignmentHorizontalValues.Center, true, false, ref HojaXcel3);
                                exportador.CeldaDatosFloat("C" + cont.ToString(), (float)itemLstCDI.SumaProyectos, XLAlignmentHorizontalValues.Right, true, false, ref HojaXcel3);

                                exportador.CeldaDatoInt("D" + cont.ToString(), iActualProyCDI /* itemLstCDI.NoProyActuales */ , XLAlignmentHorizontalValues.Center, true, false, ref HojaXcel3);
                                exportador.CeldaDatosFloat("E" + cont.ToString(), (float)itemLstCDI.SumaProyActuales, XLAlignmentHorizontalValues.Right, true, false, ref HojaXcel3);

                                exportador.CeldaDatoInt("F" + cont.ToString(), iNuevoProyCDI /* itemLstCDI.NoProyNuevos */ , XLAlignmentHorizontalValues.Center, true, false, ref HojaXcel3);
                                exportador.CeldaDatosFloat("G" + cont.ToString(), (float)itemLstCDI.SumaProyNuevos, XLAlignmentHorizontalValues.Right, true, false, ref HojaXcel3);

                                exportador.CeldaDatoInt("H" + cont.ToString(), iCerradoProyCDI /* itemLstCDI.NoProyCerrados */ , XLAlignmentHorizontalValues.Center, true, false, ref HojaXcel3);
                                exportador.CeldaDatosFloat("I" + cont.ToString(), (float)itemLstCDI.SumaProyCerrados, XLAlignmentHorizontalValues.Right, true, false, ref HojaXcel3);

                                exportador.CeldaDatoInt("J" + cont.ToString(), iCancelProyCDI  /*  itemLstCDI.NoProyCancelados */ , XLAlignmentHorizontalValues.Center, true, false, ref HojaXcel3);
                                exportador.CeldaDatosFloat("K" + cont.ToString(), (float)itemLstCDI.SumaProyCancelados, XLAlignmentHorizontalValues.Right, true, false, ref HojaXcel3);

                                cont = cont + 1;
                            }
                        }

                        if (strRIIK == "")
                        {
                            strRIIK = itemm.strRIK;
                            exportador.CeldaDatosItalic("A" + cont.ToString() + ":A" + cont.ToString(), itemm.strRIK, XLAlignmentHorizontalValues.Left, ref HojaXcel3);
                            HojaXcel3.Range("A" + cont.ToString() + ":A" + cont.ToString()).Style.Font.Bold = true;
                            HojaXcel3.Range("A" + cont.ToString() + ":A" + cont.ToString()).Style.Font.Italic = true;

                            var itemLstCDIRIK = LstCDIRIK.Where(k => k.idCDI == itemm.Id_Cd && k.idRIIK == itemm.Id_RIK)
                              .FirstOrDefault();

                            var lstDisitnctCDIRIK = listaData.Where(aa => aa.Data_Estatus != 1 && aa.Data_Id_Cd == itemm.Id_Cd && aa.Data_IdRik == itemm.Id_RIK).GroupBy(l => new { l.Data_Situacion, l.Data_Id_Cd, l.Data_IdRik })
                                .Select(g => new
                                {
                                    Situacion = g.Key.Data_Situacion,
                                    id_Cd = g.Key.Data_Id_Cd,
                                    id_Rik = g.Key.Data_IdRik,
                                    Kount = g.Select(l => l.Data_IdProyecto).Distinct().Count()
                                });

                            iTotalProyCDIRIK = lstDisitnctCDIRIK.Where(aa => aa.Situacion == 2 || aa.Situacion == 1).Select(a => a.Kount).Sum();
                            iActualProyCDIRIK = lstDisitnctCDIRIK.Where(aa => aa.Situacion == 2).Select(a => a.Kount).Sum();
                            iNuevoProyCDIRIK = lstDisitnctCDIRIK.Where(aa => aa.Situacion == 1).Select(a => a.Kount).Sum();
                            iCerradoProyCDIRIK = lstDisitnctCDIRIK.Where(aa => aa.Situacion == 3).Select(a => a.Kount).Sum();
                            iCancelProyCDIRIK = lstDisitnctCDIRIK.Where(aa => aa.Situacion == 4).Select(a => a.Kount).Sum();


                            exportador.CeldaDatoInt("B" + cont.ToString(), iTotalProyCDIRIK /* itemLstCDIRIK.NoProyectos */, XLAlignmentHorizontalValues.Center, true, true, ref HojaXcel3);
                            exportador.CeldaDatosFloat("C" + cont.ToString(), (float)itemLstCDIRIK.SumaProyectos, XLAlignmentHorizontalValues.Right, true, true, ref HojaXcel3);

                            exportador.CeldaDatoInt("D" + cont.ToString(), iActualProyCDIRIK /* itemLstCDIRIK.NoProyActuales */, XLAlignmentHorizontalValues.Center, false, true, ref HojaXcel3);
                            exportador.CeldaDatosFloat("E" + cont.ToString(), (float)itemLstCDIRIK.SumaProyActuales, XLAlignmentHorizontalValues.Right, true, true, ref HojaXcel3);

                            exportador.CeldaDatoInt("F" + cont.ToString(), iNuevoProyCDIRIK /* itemLstCDIRIK.NoProyNuevos */, XLAlignmentHorizontalValues.Center, true, true, ref HojaXcel3);
                            exportador.CeldaDatosFloat("G" + cont.ToString(), (float)itemLstCDIRIK.SumaProyNuevos, XLAlignmentHorizontalValues.Right, true, true, ref HojaXcel3);

                            exportador.CeldaDatoInt("H" + cont.ToString(), iCerradoProyCDIRIK /* itemLstCDIRIK.NoProyCerrados */, XLAlignmentHorizontalValues.Center, false, true, ref HojaXcel3);
                            exportador.CeldaDatosFloat("I" + cont.ToString(), (float)itemLstCDIRIK.SumaProyCerrados, XLAlignmentHorizontalValues.Right, true, true, ref HojaXcel3);

                            exportador.CeldaDatoInt("J" + cont.ToString(), iCancelProyCDIRIK /* itemLstCDIRIK.NoProyCancelados */, XLAlignmentHorizontalValues.Center, false, true, ref HojaXcel3);
                            exportador.CeldaDatosFloat("K" + cont.ToString(), (float)itemLstCDIRIK.SumaProyCancelados, XLAlignmentHorizontalValues.Right, true, true, ref HojaXcel3);

                            cont = cont + 1;
                        }
                        else
                        {
                            if (strRIIK != itemm.strRIK)
                            {
                                strRIIK = itemm.strRIK;
                                exportador.CeldaDatosItalic("A" + cont.ToString() + ":A" + cont.ToString(), itemm.strRIK, XLAlignmentHorizontalValues.Left, ref HojaXcel3);
                                HojaXcel3.Range("A" + cont.ToString() + ":A" + cont.ToString()).Style.Font.Bold = true;
                                HojaXcel3.Range("A" + cont.ToString() + ":A" + cont.ToString()).Style.Font.Italic = true;

                                var itemLstCDIRIK = LstCDIRIK.Where(k => k.idCDI == itemm.Id_Cd && k.idRIIK == itemm.Id_RIK)
                                  .FirstOrDefault();

                                var lstDisitnctCDIRIK = listaData.Where(aa => aa.Data_Estatus != 1 && aa.Data_Id_Cd == itemm.Id_Cd && aa.Data_IdRik == itemm.Id_RIK).GroupBy(l => new { l.Data_Situacion, l.Data_Id_Cd, l.Data_IdRik })
                                    .Select(g => new
                                    {
                                        Situacion = g.Key.Data_Situacion,
                                        id_Cd = g.Key.Data_Id_Cd,
                                        id_Rik = g.Key.Data_IdRik,
                                        Kount = g.Select(l => l.Data_IdProyecto).Distinct().Count()
                                    });

                                iTotalProyCDIRIK = lstDisitnctCDIRIK.Where(aa => aa.Situacion == 2 || aa.Situacion == 1).Select(a => a.Kount).Sum();
                                iActualProyCDIRIK = lstDisitnctCDIRIK.Where(aa => aa.Situacion == 2).Select(a => a.Kount).Sum();
                                iNuevoProyCDIRIK = lstDisitnctCDIRIK.Where(aa => aa.Situacion == 1).Select(a => a.Kount).Sum();
                                iCerradoProyCDIRIK = lstDisitnctCDIRIK.Where(aa => aa.Situacion == 3).Select(a => a.Kount).Sum();
                                iCancelProyCDIRIK = lstDisitnctCDIRIK.Where(aa => aa.Situacion == 4).Select(a => a.Kount).Sum();


                                exportador.CeldaDatoInt("B" + cont.ToString(), iTotalProyCDIRIK /* itemLstCDIRIK.NoProyectos */, XLAlignmentHorizontalValues.Center, true, true, ref HojaXcel3);
                                exportador.CeldaDatosFloat("C" + cont.ToString(), (float)itemLstCDIRIK.SumaProyectos, XLAlignmentHorizontalValues.Right, true, true, ref HojaXcel3);

                                exportador.CeldaDatoInt("D" + cont.ToString(), iActualProyCDIRIK /* itemLstCDIRIK.NoProyActuales */, XLAlignmentHorizontalValues.Center, false, true, ref HojaXcel3);
                                exportador.CeldaDatosFloat("E" + cont.ToString(), (float)itemLstCDIRIK.SumaProyActuales, XLAlignmentHorizontalValues.Right, true, true, ref HojaXcel3);

                                exportador.CeldaDatoInt("F" + cont.ToString(), iNuevoProyCDIRIK /* itemLstCDIRIK.NoProyNuevos */, XLAlignmentHorizontalValues.Center, true, true, ref HojaXcel3);
                                exportador.CeldaDatosFloat("G" + cont.ToString(), (float)itemLstCDIRIK.SumaProyNuevos, XLAlignmentHorizontalValues.Right, true, true, ref HojaXcel3);

                                exportador.CeldaDatoInt("H" + cont.ToString(), iCerradoProyCDIRIK /* itemLstCDIRIK.NoProyCerrados */, XLAlignmentHorizontalValues.Center, false, true, ref HojaXcel3);
                                exportador.CeldaDatosFloat("I" + cont.ToString(), (float)itemLstCDIRIK.SumaProyCerrados, XLAlignmentHorizontalValues.Right, true, true, ref HojaXcel3);

                                exportador.CeldaDatoInt("J" + cont.ToString(), iCancelProyCDIRIK /* itemLstCDIRIK.NoProyCancelados */, XLAlignmentHorizontalValues.Center, false, true, ref HojaXcel3);
                                exportador.CeldaDatosFloat("K" + cont.ToString(), (float)itemLstCDIRIK.SumaProyCancelados, XLAlignmentHorizontalValues.Right, true, true, ref HojaXcel3);

                                cont = cont + 1;
                            }
                        }



                        /// DescAgrupador   TotalProyectos TotalProyectos_Monto    ProyectosActuales ProyectosActuales_Monto 
                        /// ProyectosNuevo ProyectosNuevo_Monto    ProyectosCerrados ProyectosCerrados_Monto ProyectosCancelados ProyectosCancelados_Monto
                        exportador.CeldaDatos("A" + cont.ToString(), itemm.strAgrupador, XLAlignmentHorizontalValues.Left, ref HojaXcel3);

                        exportador.CeldaDatoInt("B" + cont.ToString(), itemm.MontoFinal_Proy, XLAlignmentHorizontalValues.Center, false, false, ref HojaXcel3);
                        exportador.CeldaDatosFloat("C" + cont.ToString(), (float)itemm.MontoFinal_Monto, XLAlignmentHorizontalValues.Right, false, false, ref HojaXcel3);

                        exportador.CeldaDatoInt("D" + cont.ToString(), itemm.ProyectosActuales_Proy, XLAlignmentHorizontalValues.Center, false, false, ref HojaXcel3);
                        exportador.CeldaDatosFloat("E" + cont.ToString(), (float)itemm.ProyectosActuales_Monto, XLAlignmentHorizontalValues.Right, false, false, ref HojaXcel3);

                        exportador.CeldaDatoInt("F" + cont.ToString(), itemm.ProyectosNuevos_Proy, XLAlignmentHorizontalValues.Center, false, false, ref HojaXcel3);
                        exportador.CeldaDatosFloat("G" + cont.ToString(), (float)itemm.ProyectosNuevos_Monto, XLAlignmentHorizontalValues.Right, false, false, ref HojaXcel3);

                        exportador.CeldaDatoInt("H" + cont.ToString(), itemm.ProyectosCerrados_Proy, XLAlignmentHorizontalValues.Center, false, false, ref HojaXcel3);
                        exportador.CeldaDatosFloat("I" + cont.ToString(), (float)itemm.ProyectosCerrados_Monto, XLAlignmentHorizontalValues.Right, false, false, ref HojaXcel3);

                        exportador.CeldaDatoInt("J" + cont.ToString(), itemm.ProyectosCancelados_Proy, XLAlignmentHorizontalValues.Center, false, false, ref HojaXcel3);
                        exportador.CeldaDatosFloat("K" + cont.ToString(), (float)itemm.ProyectosCancelados_Monto, XLAlignmentHorizontalValues.Right, false, false, ref HojaXcel3);

                        cont = cont + 1;
                    }


                    foreach (var item in HojaXcel3.ColumnsUsed())
                    {
                        item.AdjustToContents();
                    }

                    ///         NUEVA HOJA DE DATA
                    ///---------------------------------------------------------------------

                    var HojaXcel4 = workboook.Worksheets.Add("DATA");
                    /// Id_Emp Id_Cd   IdProyecto Estatus Est_Descripcion Mes Año FechaCreacion   
                    /// Id_UEN Uen_Descripcion Id_Apl Apl_Descripcion Id_Seg Seg_Descripcion Id_Ter Ter_Nombre  
                    /// IdRik Rik_Nombre  Id_Cte cte_nomComercial    IdProducto TipoProducto    PrecLista COP_Cantidad    TipoVenta 
                    /// FechaAnalisis   MontoAnalisis FechaPresentacion   MontoPresentacion FechaNegociacion    MontoNegociacion FechaCierre MontoCierre FechaCancelacion    MontoCancelacion FechaModificacion   FechaCotizacion MontoProyecto   Situacion

                    exportador.CeldaHeader("A1", "Id_Emp", ref HojaXcel4);
                    exportador.CeldaHeader("B1", "Id_Cd", ref HojaXcel4);
                    exportador.CeldaHeader("C1", "IdProyecto", ref HojaXcel4);
                    exportador.CeldaHeader("D1", "Estatus", ref HojaXcel4);
                    exportador.CeldaHeader("E1", "Est_Descripcion", ref HojaXcel4);
                    exportador.CeldaHeader("F1", "Mes", ref HojaXcel4);
                    exportador.CeldaHeader("G1", "Año", ref HojaXcel4);
                    exportador.CeldaHeader("H1", "FechaCreacion", ref HojaXcel4);

                    exportador.CeldaHeader("I1", "Id_UEN", ref HojaXcel4);
                    exportador.CeldaHeader("J1", "Uen_Descripcion", ref HojaXcel4);
                    exportador.CeldaHeader("K1", "Id_Apl", ref HojaXcel4);
                    exportador.CeldaHeader("L1", "Apl_Descripcion", ref HojaXcel4);
                    exportador.CeldaHeader("M1", "Id_Seg", ref HojaXcel4);
                    exportador.CeldaHeader("N1", "Seg_Descripcion", ref HojaXcel4);
                    exportador.CeldaHeader("O1", "Id_Ter", ref HojaXcel4);
                    exportador.CeldaHeader("P1", "Ter_Nombre", ref HojaXcel4);

                    exportador.CeldaHeader("Q1", "IdRik", ref HojaXcel4);
                    exportador.CeldaHeader("R1", "Rik_Nombre", ref HojaXcel4);
                    exportador.CeldaHeader("S1", "Id_Cte", ref HojaXcel4);
                    exportador.CeldaHeader("T1", "cte_nomComercial", ref HojaXcel4);
                    exportador.CeldaHeader("U1", "IdProducto", ref HojaXcel4);
                    exportador.CeldaHeader("V1", "TipoProducto", ref HojaXcel4);
                    exportador.CeldaHeader("W1", "PrecLista", ref HojaXcel4);
                    exportador.CeldaHeader("X1", "COP_Cantidad", ref HojaXcel4);
                    exportador.CeldaHeader("Y1", "TipoVenta", ref HojaXcel4);

                    exportador.CeldaHeader("Z1", "FechaAnalisis", ref HojaXcel4);
                    exportador.CeldaHeader("AA1", "MontoAnalisis", ref HojaXcel4);
                    exportador.CeldaHeader("AB1", "FechaPresentacion", ref HojaXcel4);
                    exportador.CeldaHeader("AC1", "MontoPresentacion", ref HojaXcel4);
                    exportador.CeldaHeader("AD1", "FechaNegociacion", ref HojaXcel4);
                    exportador.CeldaHeader("AE1", "MontoNegociacion", ref HojaXcel4);
                    exportador.CeldaHeader("AF1", "FechaCierre", ref HojaXcel4);
                    exportador.CeldaHeader("AG1", "MontoCierre", ref HojaXcel4);

                    exportador.CeldaHeader("AH1", "FechaCancelacion", ref HojaXcel4);
                    exportador.CeldaHeader("AI1", "MontoCancelacion", ref HojaXcel4);
                    exportador.CeldaHeader("AJ1", "FechaModificacion", ref HojaXcel4);
                    exportador.CeldaHeader("AK1", "FechaCotizacion", ref HojaXcel4);
                    exportador.CeldaHeader("AL1", "MontoProyecto", ref HojaXcel4);
                    exportador.CeldaHeader("AM1", "Situacion", ref HojaXcel4);

                    cont = 2;

                    foreach (ReporteCRMQuimicos itemd in listaData)
                    {

                        exportador.CeldaDatoInt("A" + cont.ToString(), itemd.Data_Id_Emp, XLAlignmentHorizontalValues.Center, false, false, ref HojaXcel4);
                        exportador.CeldaDatoInt("B" + cont.ToString(), itemd.Data_Id_Cd, XLAlignmentHorizontalValues.Center, false, false, ref HojaXcel4);
                        exportador.CeldaDatoInt("C" + cont.ToString(), itemd.Data_IdProyecto, XLAlignmentHorizontalValues.Center, false, false, ref HojaXcel4);
                        exportador.CeldaDatoInt("D" + cont.ToString(), itemd.Data_Estatus, XLAlignmentHorizontalValues.Center, false, false, ref HojaXcel4);
                        exportador.CeldaDatos("E" + cont.ToString(), itemd.Data_Est_Descripcion, XLAlignmentHorizontalValues.Left, ref HojaXcel4);
                        exportador.CeldaDatoInt("F" + cont.ToString(), itemd.Data_Mes, XLAlignmentHorizontalValues.Center, false, false, ref HojaXcel4);
                        exportador.CeldaDatoInt("G" + cont.ToString(), itemd.Data_Año, XLAlignmentHorizontalValues.Center, false, false, ref HojaXcel4);
                        exportador.CeldaDatosDate("H" + cont.ToString(), (DateTime?)itemd.Data_FechaCreacion, XLAlignmentHorizontalValues.Right, false, false, ref HojaXcel4);

                        exportador.CeldaDatoInt("I" + cont.ToString(), itemd.Data_Id_UEN, XLAlignmentHorizontalValues.Center, false, false, ref HojaXcel4);
                        exportador.CeldaDatos("J" + cont.ToString(), itemd.Data_Uen_Descripcion, XLAlignmentHorizontalValues.Left, ref HojaXcel4);
                        exportador.CeldaDatoInt("K" + cont.ToString(), itemd.Data_Id_Apl, XLAlignmentHorizontalValues.Center, false, false, ref HojaXcel4);
                        exportador.CeldaDatos("L" + cont.ToString(), itemd.Data_Apl_Descripcion, XLAlignmentHorizontalValues.Left, ref HojaXcel4);
                        exportador.CeldaDatoInt("M" + cont.ToString(), itemd.Data_Id_Seg, XLAlignmentHorizontalValues.Center, false, false, ref HojaXcel4);
                        exportador.CeldaDatos("N" + cont.ToString(), itemd.Data_Seg_Descripcion, XLAlignmentHorizontalValues.Left, ref HojaXcel4);
                        exportador.CeldaDatoInt("O" + cont.ToString(), itemd.Data_Id_Ter, XLAlignmentHorizontalValues.Center, false, false, ref HojaXcel4);
                        exportador.CeldaDatos("P" + cont.ToString(), itemd.Data_Ter_Nombre, XLAlignmentHorizontalValues.Left, ref HojaXcel4);

                        exportador.CeldaDatoInt("Q" + cont.ToString(), itemd.Data_IdRik, XLAlignmentHorizontalValues.Center, false, false, ref HojaXcel4);
                        exportador.CeldaDatos("R" + cont.ToString(), itemd.Data_Rik_Nombre, XLAlignmentHorizontalValues.Left, ref HojaXcel4);
                        exportador.CeldaDatoInt("S" + cont.ToString(), itemd.Data_Id_Cte, XLAlignmentHorizontalValues.Center, false, false, ref HojaXcel4);
                        exportador.CeldaDatos("T" + cont.ToString(), itemd.Data_cte_nomComercial, XLAlignmentHorizontalValues.Left, ref HojaXcel4);
                        exportador.CeldaDatoInt("U" + cont.ToString(), itemd.Data_IdProducto, XLAlignmentHorizontalValues.Center, false, false, ref HojaXcel4);
                        exportador.CeldaDatos("V" + cont.ToString(), itemd.Data_TipoProducto, XLAlignmentHorizontalValues.Left, ref HojaXcel4);
                        exportador.CeldaDatosFloat("W" + cont.ToString(), (float)itemd.Data_PrecLista, XLAlignmentHorizontalValues.Right, false, false, ref HojaXcel4);
                        exportador.CeldaDatoInt("X" + cont.ToString(), itemd.Data_COP_Cantidad, XLAlignmentHorizontalValues.Center, false, false, ref HojaXcel4);
                        exportador.CeldaDatos("Y" + cont.ToString(), itemd.Data_TipoVenta, XLAlignmentHorizontalValues.Left, ref HojaXcel4);

                        exportador.CeldaDatosDate("Z" + cont.ToString(), (DateTime?)itemd.Data_FechaAnalisis, XLAlignmentHorizontalValues.Right, false, false, ref HojaXcel4);
                        exportador.CeldaDatosFloat("AA" + cont.ToString(), (float)itemd.Data_MontoAnalisis, XLAlignmentHorizontalValues.Right, false, false, ref HojaXcel4);
                        exportador.CeldaDatosDate("AB" + cont.ToString(), (DateTime?)itemd.Data_FechaPresentacion, XLAlignmentHorizontalValues.Right, false, false, ref HojaXcel4);
                        exportador.CeldaDatosFloat("AC" + cont.ToString(), (float)itemd.Data_MontoPresentacion, XLAlignmentHorizontalValues.Right, false, false, ref HojaXcel4);
                        exportador.CeldaDatosDate("AD" + cont.ToString(), (DateTime?)itemd.Data_FechaNegociacion, XLAlignmentHorizontalValues.Right, false, false, ref HojaXcel4);
                        exportador.CeldaDatosFloat("AE" + cont.ToString(), (float)itemd.Data_MontoNegociacion, XLAlignmentHorizontalValues.Right, false, false, ref HojaXcel4);
                        exportador.CeldaDatosDate("AF" + cont.ToString(), (DateTime?)itemd.Data_FechaCierre, XLAlignmentHorizontalValues.Right, false, false, ref HojaXcel4);
                        exportador.CeldaDatosFloat("AG" + cont.ToString(), (float)itemd.Data_MontoCierre, XLAlignmentHorizontalValues.Right, false, false, ref HojaXcel4);

                        exportador.CeldaDatosDate("AH" + cont.ToString(), (DateTime?)itemd.Data_FechaCancelacion, XLAlignmentHorizontalValues.Right, false, false, ref HojaXcel4);
                        exportador.CeldaDatosFloat("AI" + cont.ToString(), (float)itemd.Data_MontoCancelacion, XLAlignmentHorizontalValues.Right, false, false, ref HojaXcel4);
                        exportador.CeldaDatosDate("AJ" + cont.ToString(), (DateTime?)itemd.Data_FechaModificacion, XLAlignmentHorizontalValues.Right, false, false, ref HojaXcel4);
                        exportador.CeldaDatosDate("AK" + cont.ToString(), (DateTime?)itemd.Data_FechaCotizacion, XLAlignmentHorizontalValues.Right, false, false, ref HojaXcel4);
                        exportador.CeldaDatosFloat("AL" + cont.ToString(), (float)itemd.Data_MontoProyecto, XLAlignmentHorizontalValues.Right, false, false, ref HojaXcel4);
                        exportador.CeldaDatoInt("AM" + cont.ToString(), itemd.Data_Situacion, XLAlignmentHorizontalValues.Center, false, false, ref HojaXcel4);

                        cont = cont + 1;
                    }




                    foreach (var item in HojaXcel4.ColumnsUsed())
                    {
                        item.AdjustToContents();
                    }
                    string archivooo = "ReporteCRMImpulsosQuimicos_" + DateTime.Now.ToShortDateString().Replace("/", "") + "h" + DateTime.Now.ToShortTimeString().Replace(".", "").Replace(":", "").Replace(" ", "") + ".xlsx";
                    ///     ExportaEczel(workbook);
                    ////    ExpooortaExcel(workboook, archivooo);
                    if (exportador.ServicioGrabaArchivo(workboook, Server.MapPath("~/Reportes/") + archivooo))
                    {
                        exportador.ServicioBajaArchivo(Server.MapPath("~/Reportes/"), archivooo);
                    }
                    ////    exportador.ServicioExportaExcelClosedXML(workboook, Response, Server.MapPath("~/Reportes/") + archivooo, archivooo);
                }

            }
            else
            {
                using (ExcelEngine excelEngine = new ExcelEngine())
                {
                    IApplication application = excelEngine.Excel;
                    /// application.DefaultVersion = ExcelVersion.Excel2013;
                    application.DefaultVersion = ExcelVersion.Xlsx;
                    string[] cadHojas = new string[3] { "General", "Sucursal", "RIK" }; ///   , "Sucursal", "RIK" };
                    IWorkbook LibroExcelSF = application.Workbooks.Create(cadHojas);

                    IWorksheet HojaExcelSF = LibroExcelSF.Worksheets[0];

                    HojaExcelSF.Range["A1"].Text = "Periodo:";
                    HojaExcelSF.Range["B1"].Text = drpFecInicialRptIQ.Value.ToString();
                    HojaExcelSF.Range["B1:K1"].Merge();
                    HojaExcelSF.Range["A2"].Text = "Tipo de producto:";
                    HojaExcelSF.Range["B2"].Text = strTiposProductoReporte;
                    HojaExcelSF.Range["B2:K2"].Merge();
                    HojaExcelSF.Range["A3"].Text = "Sucursal:";
                    HojaExcelSF.Range["B3"].Text = CMBSucursalIQ.Text;
                    HojaExcelSF.Range["B3:K3"].Merge();

                    HojaExcelSF.Range["A5:A6"].Merge();
                    exportador.CeldaHeaderAgrupadorSF("B5:C5", "Monto Final", ref HojaExcelSF);
                    exportador.CeldaHeaderSF("B6", "#", ref HojaExcelSF);
                    exportador.CeldaHeaderSF("C6", "$", ref HojaExcelSF);

                    exportador.CeldaHeaderAgrupadorSF("D5:E5", "Proyectos actuales", ref HojaExcelSF);
                    exportador.CeldaHeaderSF("D6", "#", ref HojaExcelSF);
                    exportador.CeldaHeaderSF("E6", "$", ref HojaExcelSF);

                    exportador.CeldaHeaderAgrupadorSF("F5:G5", "Proyectos nuevos", ref HojaExcelSF);
                    exportador.CeldaHeaderSF("F6", "#", ref HojaExcelSF);
                    exportador.CeldaHeaderSF("G6", "$", ref HojaExcelSF);

                    exportador.CeldaHeaderAgrupadorSF("H5:I5", "Proyectos cerrados", ref HojaExcelSF);
                    exportador.CeldaHeaderSF("H6", "#", ref HojaExcelSF);
                    exportador.CeldaHeaderSF("I6", "$", ref HojaExcelSF);

                    exportador.CeldaHeaderAgrupadorSF("J5:K5", "Proyectos cancelados", ref HojaExcelSF);
                    exportador.CeldaHeaderSF("J6", "#", ref HojaExcelSF);
                    exportador.CeldaHeaderSF("K6", "$", ref HojaExcelSF);

                    foreach (ReporteCRMQuimicos itemm in listaCRMQuimicos)
                    {
                        if (strCDII2 == "")
                        {
                            strCDII2 = itemm.strCDI;
                            exportador.CeldaDatosSF("A" + cont.ToString() + ":A" + cont.ToString(), itemm.strCDI, ExcelHAlign.HAlignLeft, true, false, ref HojaExcelSF);

                            var itemLstGnnl = LstGeneral.Where(k => k.idCDI == itemm.Id_Cd)
                              .FirstOrDefault();

                            exportador.CeldaDatoIntSF("B" + cont.ToString(), itemLstGnnl.NoProyectos, ExcelHAlign.HAlignCenter, true, false, ref HojaExcelSF);
                            exportador.CeldaDatosFloatSF("C" + cont.ToString(), (float)itemLstGnnl.SumaProyectos, ExcelHAlign.HAlignRight, true, false, ref HojaExcelSF);

                            exportador.CeldaDatoIntSF("D" + cont.ToString(), itemLstGnnl.NoProyActuales, ExcelHAlign.HAlignCenter, true, false, ref HojaExcelSF);
                            exportador.CeldaDatosFloatSF("E" + cont.ToString(), (float)itemLstGnnl.SumaProyActuales, ExcelHAlign.HAlignRight, true, false, ref HojaExcelSF);

                            exportador.CeldaDatoIntSF("F" + cont.ToString(), itemLstGnnl.NoProyNuevos, ExcelHAlign.HAlignCenter, true, false, ref HojaExcelSF);
                            exportador.CeldaDatosFloatSF("G" + cont.ToString(), (float)itemLstGnnl.SumaProyNuevos, ExcelHAlign.HAlignRight, true, false, ref HojaExcelSF);

                            exportador.CeldaDatoIntSF("H" + cont.ToString(), itemLstGnnl.NoProyCerrados, ExcelHAlign.HAlignCenter, true, false, ref HojaExcelSF);
                            exportador.CeldaDatosFloatSF("I" + cont.ToString(), (float)itemLstGnnl.SumaProyCerrados, ExcelHAlign.HAlignRight, true, false, ref HojaExcelSF);

                            exportador.CeldaDatoIntSF("J" + cont.ToString(), itemLstGnnl.NoProyCancelados, ExcelHAlign.HAlignCenter, true, false, ref HojaExcelSF);
                            exportador.CeldaDatosFloatSF("K" + cont.ToString(), (float)itemLstGnnl.SumaProyCancelados, ExcelHAlign.HAlignRight, true, false, ref HojaExcelSF);

                            cont = cont + 1;
                        }
                        else
                        {
                            if (strCDII2 != itemm.strCDI)
                            {
                                strCDII2 = itemm.strCDI;
                                exportador.CeldaDatosSF("A" + cont.ToString() + ":A" + cont.ToString(), itemm.strCDI, ExcelHAlign.HAlignLeft, true, false, ref HojaExcelSF);

                                var itemLstGnnl = LstCDI.Where(k => k.idCDI == itemm.Id_Cd)
                                  .FirstOrDefault();

                                exportador.CeldaDatoIntSF("B" + cont.ToString(), itemLstGnnl.NoProyectos, ExcelHAlign.HAlignCenter, true, false, ref HojaExcelSF);
                                exportador.CeldaDatosFloatSF("C" + cont.ToString(), (float)itemLstGnnl.SumaProyectos, ExcelHAlign.HAlignRight, true, false, ref HojaExcelSF);

                                exportador.CeldaDatoIntSF("D" + cont.ToString(), itemLstGnnl.NoProyActuales, ExcelHAlign.HAlignCenter, true, false, ref HojaExcelSF);
                                exportador.CeldaDatosFloatSF("E" + cont.ToString(), (float)itemLstGnnl.SumaProyActuales, ExcelHAlign.HAlignRight, true, false, ref HojaExcelSF);

                                exportador.CeldaDatoIntSF("F" + cont.ToString(), itemLstGnnl.NoProyNuevos, ExcelHAlign.HAlignCenter, true, false, ref HojaExcelSF);
                                exportador.CeldaDatosFloatSF("G" + cont.ToString(), (float)itemLstGnnl.SumaProyNuevos, ExcelHAlign.HAlignRight, true, false, ref HojaExcelSF);

                                exportador.CeldaDatoIntSF("H" + cont.ToString(), itemLstGnnl.NoProyCerrados, ExcelHAlign.HAlignCenter, true, false, ref HojaExcelSF);
                                exportador.CeldaDatosFloatSF("I" + cont.ToString(), (float)itemLstGnnl.SumaProyCerrados, ExcelHAlign.HAlignRight, true, false, ref HojaExcelSF);

                                exportador.CeldaDatoIntSF("J" + cont.ToString(), itemLstGnnl.NoProyCancelados, ExcelHAlign.HAlignCenter, true, false, ref HojaExcelSF);
                                exportador.CeldaDatosFloatSF("K" + cont.ToString(), (float)itemLstGnnl.SumaProyCancelados, ExcelHAlign.HAlignRight, true, false, ref HojaExcelSF);

                                cont = cont + 1;
                            }
                        }

                        /// DescAgrupador   TotalProyectos TotalProyectos_Monto    ProyectosActuales ProyectosActuales_Monto 
                        /// ProyectosNuevo ProyectosNuevo_Monto    ProyectosCerrados ProyectosCerrados_Monto ProyectosCancelados ProyectosCancelados_Monto
                        exportador.CeldaDatosSF("A" + cont.ToString(), itemm.strAgrupador, ExcelHAlign.HAlignLeft, true, false, ref HojaExcelSF);

                        exportador.CeldaDatoIntSF("B" + cont.ToString(), itemm.MontoFinal_Proy, ExcelHAlign.HAlignCenter, false, false, ref HojaExcelSF);
                        exportador.CeldaDatosFloatSF("C" + cont.ToString(), (float)itemm.MontoFinal_Monto, ExcelHAlign.HAlignRight, false, false, ref HojaExcelSF);

                        exportador.CeldaDatoIntSF("D" + cont.ToString(), itemm.ProyectosActuales_Proy, ExcelHAlign.HAlignCenter, false, false, ref HojaExcelSF);
                        exportador.CeldaDatosFloatSF("E" + cont.ToString(), (float)itemm.ProyectosActuales_Monto, ExcelHAlign.HAlignRight, false, false, ref HojaExcelSF);

                        exportador.CeldaDatoIntSF("F" + cont.ToString(), itemm.ProyectosNuevos_Proy, ExcelHAlign.HAlignCenter, false, false, ref HojaExcelSF);
                        exportador.CeldaDatosFloatSF("G" + cont.ToString(), (float)itemm.ProyectosNuevos_Monto, ExcelHAlign.HAlignRight, false, false, ref HojaExcelSF);

                        exportador.CeldaDatoIntSF("H" + cont.ToString(), itemm.ProyectosCerrados_Proy, ExcelHAlign.HAlignCenter, false, false, ref HojaExcelSF);
                        exportador.CeldaDatosFloatSF("I" + cont.ToString(), (float)itemm.ProyectosCerrados_Monto, ExcelHAlign.HAlignRight, false, false, ref HojaExcelSF);

                        exportador.CeldaDatoIntSF("J" + cont.ToString(), itemm.ProyectosCancelados_Proy, ExcelHAlign.HAlignCenter, false, false, ref HojaExcelSF);
                        exportador.CeldaDatosFloatSF("K" + cont.ToString(), (float)itemm.ProyectosCancelados_Monto, ExcelHAlign.HAlignRight, false, false, ref HojaExcelSF);

                        cont = cont + 1;
                    }

                    HojaExcelSF.UsedRange.AutofitColumns();

                    ///-----------------------------------------------------------------------------------------------------

                    IWorksheet HojaExcelSF2 = LibroExcelSF.Worksheets[1];

                    HojaExcelSF2.Range["A3:A4"].Merge();
                    exportador.CeldaHeaderAgrupadorSF("B3:C3", "Monto Final", ref HojaExcelSF2);
                    exportador.CeldaHeaderSF("B4", "#", ref HojaExcelSF2);
                    exportador.CeldaHeaderSF("C4", "$", ref HojaExcelSF2);

                    exportador.CeldaHeaderAgrupadorSF("D3:E3", "Proyectos actuales", ref HojaExcelSF2);
                    exportador.CeldaHeaderSF("D4", "#", ref HojaExcelSF2);
                    exportador.CeldaHeaderSF("E4", "$", ref HojaExcelSF2);

                    exportador.CeldaHeaderAgrupadorSF("F3:G3", "Proyectos nuevos", ref HojaExcelSF2);
                    exportador.CeldaHeaderSF("F4", "#", ref HojaExcelSF2);
                    exportador.CeldaHeaderSF("G4", "$", ref HojaExcelSF2);

                    exportador.CeldaHeaderAgrupadorSF("H3:I3", "Proyectos cerrados", ref HojaExcelSF2);
                    exportador.CeldaHeaderSF("H4", "#", ref HojaExcelSF2);
                    exportador.CeldaHeaderSF("I4", "$", ref HojaExcelSF2);

                    exportador.CeldaHeaderAgrupadorSF("J3:K3", "Proyectos cancelados", ref HojaExcelSF2);
                    exportador.CeldaHeaderSF("J4", "#", ref HojaExcelSF2);
                    exportador.CeldaHeaderSF("K4", "$", ref HojaExcelSF2);

                    cont = 5;
                    foreach (ReporteCRMQuimicos itemm in listaCRMQuimicosxSucursal)
                    {
                        if (strCDII2 == "")
                        {
                            strCDII2 = itemm.strCDI;
                            exportador.CeldaDatosSF("A" + cont.ToString() + ":A" + cont.ToString(), itemm.strCDI, ExcelHAlign.HAlignLeft, true, false, ref HojaExcelSF2);

                            var itemLstCDI = LstCDI.Where(k => k.idCDI == itemm.Id_Cd)
                              .FirstOrDefault();

                            exportador.CeldaDatoIntSF("B" + cont.ToString(), itemLstCDI.NoProyectos, ExcelHAlign.HAlignCenter, true, false, ref HojaExcelSF2);
                            exportador.CeldaDatosFloatSF("C" + cont.ToString(), (float)itemLstCDI.SumaProyectos, ExcelHAlign.HAlignRight, true, false, ref HojaExcelSF2);

                            exportador.CeldaDatoIntSF("D" + cont.ToString(), itemLstCDI.NoProyActuales, ExcelHAlign.HAlignCenter, true, false, ref HojaExcelSF2);
                            exportador.CeldaDatosFloatSF("E" + cont.ToString(), (float)itemLstCDI.SumaProyActuales, ExcelHAlign.HAlignRight, true, false, ref HojaExcelSF2);

                            exportador.CeldaDatoIntSF("F" + cont.ToString(), itemLstCDI.NoProyNuevos, ExcelHAlign.HAlignCenter, true, false, ref HojaExcelSF2);
                            exportador.CeldaDatosFloatSF("G" + cont.ToString(), (float)itemLstCDI.SumaProyNuevos, ExcelHAlign.HAlignRight, true, false, ref HojaExcelSF2);

                            exportador.CeldaDatoIntSF("H" + cont.ToString(), itemLstCDI.NoProyCerrados, ExcelHAlign.HAlignCenter, true, false, ref HojaExcelSF2);
                            exportador.CeldaDatosFloatSF("I" + cont.ToString(), (float)itemLstCDI.SumaProyCerrados, ExcelHAlign.HAlignRight, true, false, ref HojaExcelSF2);

                            exportador.CeldaDatoIntSF("J" + cont.ToString(), itemLstCDI.NoProyCancelados, ExcelHAlign.HAlignCenter, true, false, ref HojaExcelSF2);
                            exportador.CeldaDatosFloatSF("K" + cont.ToString(), (float)itemLstCDI.SumaProyCancelados, ExcelHAlign.HAlignRight, true, false, ref HojaExcelSF2);

                            cont = cont + 1;
                        }
                        else
                        {
                            if (strCDII2 != itemm.strCDI)
                            {
                                strCDII2 = itemm.strCDI;
                                exportador.CeldaDatosSF("A" + cont.ToString() + ":A" + cont.ToString(), itemm.strCDI, ExcelHAlign.HAlignLeft, true, false, ref HojaExcelSF2);

                                var itemLstCDI = LstCDI.Where(k => k.idCDI == itemm.Id_Cd)
                                  .FirstOrDefault();

                                exportador.CeldaDatoIntSF("B" + cont.ToString(), itemLstCDI.NoProyectos, ExcelHAlign.HAlignCenter, true, false, ref HojaExcelSF2);
                                exportador.CeldaDatosFloatSF("C" + cont.ToString(), (float)itemLstCDI.SumaProyectos, ExcelHAlign.HAlignRight, true, false, ref HojaExcelSF2);

                                exportador.CeldaDatoIntSF("D" + cont.ToString(), itemLstCDI.NoProyActuales, ExcelHAlign.HAlignCenter, true, false, ref HojaExcelSF2);
                                exportador.CeldaDatosFloatSF("E" + cont.ToString(), (float)itemLstCDI.SumaProyActuales, ExcelHAlign.HAlignRight, true, false, ref HojaExcelSF2);

                                exportador.CeldaDatoIntSF("F" + cont.ToString(), itemLstCDI.NoProyNuevos, ExcelHAlign.HAlignCenter, true, false, ref HojaExcelSF2);
                                exportador.CeldaDatosFloatSF("G" + cont.ToString(), (float)itemLstCDI.SumaProyNuevos, ExcelHAlign.HAlignRight, true, false, ref HojaExcelSF2);

                                exportador.CeldaDatoIntSF("H" + cont.ToString(), itemLstCDI.NoProyCerrados, ExcelHAlign.HAlignCenter, true, false, ref HojaExcelSF2);
                                exportador.CeldaDatosFloatSF("I" + cont.ToString(), (float)itemLstCDI.SumaProyCerrados, ExcelHAlign.HAlignRight, true, false, ref HojaExcelSF2);

                                exportador.CeldaDatoIntSF("J" + cont.ToString(), itemLstCDI.NoProyCancelados, ExcelHAlign.HAlignCenter, true, false, ref HojaExcelSF2);
                                exportador.CeldaDatosFloatSF("K" + cont.ToString(), (float)itemLstCDI.SumaProyCancelados, ExcelHAlign.HAlignRight, true, false, ref HojaExcelSF2);

                                cont = cont + 1;
                            }
                        }
                        /// DescAgrupador   TotalProyectos TotalProyectos_Monto    ProyectosActuales ProyectosActuales_Monto 
                        /// ProyectosNuevo ProyectosNuevo_Monto    ProyectosCerrados ProyectosCerrados_Monto ProyectosCancelados ProyectosCancelados_Monto
                        exportador.CeldaDatosSF("A" + cont.ToString(), itemm.strAgrupador, ExcelHAlign.HAlignLeft, false, false, ref HojaExcelSF2);

                        exportador.CeldaDatoIntSF("B" + cont.ToString(), itemm.MontoFinal_Proy, ExcelHAlign.HAlignCenter, false, false, ref HojaExcelSF2);
                        exportador.CeldaDatosFloatSF("C" + cont.ToString(), (float)itemm.MontoFinal_Monto, ExcelHAlign.HAlignRight, false, false, ref HojaExcelSF2);

                        exportador.CeldaDatoIntSF("D" + cont.ToString(), itemm.ProyectosActuales_Proy, ExcelHAlign.HAlignCenter, false, false, ref HojaExcelSF2);
                        exportador.CeldaDatosFloatSF("E" + cont.ToString(), (float)itemm.ProyectosActuales_Monto, ExcelHAlign.HAlignRight, false, false, ref HojaExcelSF2);

                        exportador.CeldaDatoIntSF("F" + cont.ToString(), itemm.ProyectosNuevos_Proy, ExcelHAlign.HAlignCenter, false, false, ref HojaExcelSF2);
                        exportador.CeldaDatosFloatSF("G" + cont.ToString(), (float)itemm.ProyectosNuevos_Monto, ExcelHAlign.HAlignRight, false, false, ref HojaExcelSF2);

                        exportador.CeldaDatoIntSF("H" + cont.ToString(), itemm.ProyectosCerrados_Proy, ExcelHAlign.HAlignCenter, false, false, ref HojaExcelSF2);
                        exportador.CeldaDatosFloatSF("I" + cont.ToString(), (float)itemm.ProyectosCerrados_Monto, ExcelHAlign.HAlignRight, false, false, ref HojaExcelSF2);

                        exportador.CeldaDatoIntSF("J" + cont.ToString(), itemm.ProyectosCancelados_Proy, ExcelHAlign.HAlignCenter, false, false, ref HojaExcelSF2);
                        exportador.CeldaDatosFloatSF("K" + cont.ToString(), (float)itemm.ProyectosCancelados_Monto, ExcelHAlign.HAlignRight, false, false, ref HojaExcelSF2);

                        cont = cont + 1;
                    }

                    HojaExcelSF2.UsedRange.AutofitColumns();

                    ///-----------------------------------------------------------------------------------------------------

                    IWorksheet HojaExcelSF3 = LibroExcelSF.Worksheets[2];

                    HojaExcelSF.Range["A3:A4"].Merge();
                    exportador.CeldaHeaderAgrupadorSF("B3:C3", "Monto Final", ref HojaExcelSF);
                    exportador.CeldaHeaderSF("B4", "#", ref HojaExcelSF);
                    exportador.CeldaHeaderSF("C4", "$", ref HojaExcelSF);

                    exportador.CeldaHeaderAgrupadorSF("D3:E3", "Proyectos actuales", ref HojaExcelSF);
                    exportador.CeldaHeaderSF("D4", "#", ref HojaExcelSF);
                    exportador.CeldaHeaderSF("E4", "$", ref HojaExcelSF);

                    exportador.CeldaHeaderAgrupadorSF("F3:G3", "Proyectos nuevos", ref HojaExcelSF);
                    exportador.CeldaHeaderSF("F4", "#", ref HojaExcelSF);
                    exportador.CeldaHeaderSF("G4", "$", ref HojaExcelSF);

                    exportador.CeldaHeaderAgrupadorSF("H3:I3", "Proyectos cerrados", ref HojaExcelSF);
                    exportador.CeldaHeaderSF("H4", "#", ref HojaExcelSF);
                    exportador.CeldaHeaderSF("I4", "$", ref HojaExcelSF);

                    exportador.CeldaHeaderAgrupadorSF("J3:K3", "Proyectos cancelados", ref HojaExcelSF);
                    exportador.CeldaHeaderSF("J4", "#", ref HojaExcelSF);
                    exportador.CeldaHeaderSF("K4", "$", ref HojaExcelSF);

                    string strCDIIR = "";
                    string strRIIK = "";
                    cont = 5;

                    foreach (ReporteCRMQuimicos itemm in listaCRMQuimicosxSucursalxRIK)
                    {
                        if (strCDIIR == "")
                        {
                            strCDIIR = itemm.strCDI;

                            exportador.CeldaDatosSF("A" + cont.ToString() + ":A" + cont.ToString(), itemm.strCDI, ExcelHAlign.HAlignLeft, true, false, ref HojaExcelSF);

                            var itemLstCDI = LstCDI.Where(k => k.idCDI == itemm.Id_Cd)
                              .FirstOrDefault();

                            exportador.CeldaDatoIntSF("B" + cont.ToString(), itemLstCDI.NoProyectos, ExcelHAlign.HAlignCenter, true, false, ref HojaExcelSF);
                            exportador.CeldaDatosFloatSF("C" + cont.ToString(), (float)itemLstCDI.SumaProyectos, ExcelHAlign.HAlignRight, true, false, ref HojaExcelSF);

                            exportador.CeldaDatoIntSF("D" + cont.ToString(), itemLstCDI.NoProyActuales, ExcelHAlign.HAlignCenter, true, false, ref HojaExcelSF);
                            exportador.CeldaDatosFloatSF("E" + cont.ToString(), (float)itemLstCDI.SumaProyActuales, ExcelHAlign.HAlignRight, true, false, ref HojaExcelSF);

                            exportador.CeldaDatoIntSF("F" + cont.ToString(), itemLstCDI.NoProyNuevos, ExcelHAlign.HAlignCenter, true, false, ref HojaExcelSF);
                            exportador.CeldaDatosFloatSF("G" + cont.ToString(), (float)itemLstCDI.SumaProyNuevos, ExcelHAlign.HAlignRight, true, false, ref HojaExcelSF);

                            exportador.CeldaDatoIntSF("H" + cont.ToString(), itemLstCDI.NoProyCerrados, ExcelHAlign.HAlignCenter, true, false, ref HojaExcelSF);
                            exportador.CeldaDatosFloatSF("I" + cont.ToString(), (float)itemLstCDI.SumaProyCerrados, ExcelHAlign.HAlignRight, true, false, ref HojaExcelSF);

                            exportador.CeldaDatoIntSF("J" + cont.ToString(), itemLstCDI.NoProyCancelados, ExcelHAlign.HAlignCenter, true, false, ref HojaExcelSF);
                            exportador.CeldaDatosFloatSF("K" + cont.ToString(), (float)itemLstCDI.SumaProyCancelados, ExcelHAlign.HAlignRight, true, false, ref HojaExcelSF);

                            cont = cont + 1;
                        }
                        else
                        {
                            if (strCDIIR != itemm.strCDI)
                            {
                                strCDIIR = itemm.strCDI;
                                exportador.CeldaDatosSF("A" + cont.ToString() + ":A" + cont.ToString(), itemm.strCDI, ExcelHAlign.HAlignLeft, true, false, ref HojaExcelSF);

                                var itemLstCDI = LstCDI.Where(k => k.idCDI == itemm.Id_Cd)
                                  .FirstOrDefault();

                                exportador.CeldaDatoIntSF("B" + cont.ToString(), itemLstCDI.NoProyectos, ExcelHAlign.HAlignCenter, true, false, ref HojaExcelSF);
                                exportador.CeldaDatosFloatSF("C" + cont.ToString(), (float)itemLstCDI.SumaProyectos, ExcelHAlign.HAlignRight, true, false, ref HojaExcelSF);

                                exportador.CeldaDatoIntSF("D" + cont.ToString(), itemLstCDI.NoProyActuales, ExcelHAlign.HAlignCenter, true, false, ref HojaExcelSF);
                                exportador.CeldaDatosFloatSF("E" + cont.ToString(), (float)itemLstCDI.SumaProyActuales, ExcelHAlign.HAlignRight, true, false, ref HojaExcelSF);

                                exportador.CeldaDatoIntSF("F" + cont.ToString(), itemLstCDI.NoProyNuevos, ExcelHAlign.HAlignCenter, true, false, ref HojaExcelSF);
                                exportador.CeldaDatosFloatSF("G" + cont.ToString(), (float)itemLstCDI.SumaProyNuevos, ExcelHAlign.HAlignRight, true, false, ref HojaExcelSF);

                                exportador.CeldaDatoIntSF("H" + cont.ToString(), itemLstCDI.NoProyCerrados, ExcelHAlign.HAlignCenter, true, false, ref HojaExcelSF);
                                exportador.CeldaDatosFloatSF("I" + cont.ToString(), (float)itemLstCDI.SumaProyCerrados, ExcelHAlign.HAlignRight, true, false, ref HojaExcelSF);

                                exportador.CeldaDatoIntSF("J" + cont.ToString(), itemLstCDI.NoProyCancelados, ExcelHAlign.HAlignCenter, true, false, ref HojaExcelSF);
                                exportador.CeldaDatosFloatSF("K" + cont.ToString(), (float)itemLstCDI.SumaProyCancelados, ExcelHAlign.HAlignRight, true, false, ref HojaExcelSF);

                                cont = cont + 1;
                            }
                        }

                        if (strRIIK == "")
                        {
                            strRIIK = itemm.strRIK;
                            exportador.CeldaDatosSF("A" + cont.ToString() + ":A" + cont.ToString(), itemm.strRIK, ExcelHAlign.HAlignLeft, false, true, ref HojaExcelSF);

                            var itemLstCDIRIK = LstCDIRIK.Where(k => k.idCDI == itemm.Id_Cd && k.idRIIK == itemm.Id_RIK)
                              .FirstOrDefault();

                            exportador.CeldaDatoIntSF("B" + cont.ToString(), itemLstCDIRIK.NoProyectos, ExcelHAlign.HAlignCenter, false, true, ref HojaExcelSF);
                            exportador.CeldaDatosFloatSF("C" + cont.ToString(), (float)itemLstCDIRIK.SumaProyectos, ExcelHAlign.HAlignRight, true, false, ref HojaExcelSF);

                            exportador.CeldaDatoIntSF("D" + cont.ToString(), itemLstCDIRIK.NoProyActuales, ExcelHAlign.HAlignCenter, false, true, ref HojaExcelSF);
                            exportador.CeldaDatosFloatSF("E" + cont.ToString(), (float)itemLstCDIRIK.SumaProyActuales, ExcelHAlign.HAlignRight, true, false, ref HojaExcelSF);

                            exportador.CeldaDatoIntSF("F" + cont.ToString(), itemLstCDIRIK.NoProyNuevos, ExcelHAlign.HAlignCenter, false, true, ref HojaExcelSF);
                            exportador.CeldaDatosFloatSF("G" + cont.ToString(), (float)itemLstCDIRIK.NoProyNuevos, ExcelHAlign.HAlignRight, false, true, ref HojaExcelSF);

                            exportador.CeldaDatoIntSF("H" + cont.ToString(), itemLstCDIRIK.NoProyCerrados, ExcelHAlign.HAlignCenter, false, true, ref HojaExcelSF);
                            exportador.CeldaDatosFloatSF("I" + cont.ToString(), (float)itemLstCDIRIK.SumaProyCerrados, ExcelHAlign.HAlignRight, false, true, ref HojaExcelSF);

                            exportador.CeldaDatoIntSF("J" + cont.ToString(), itemLstCDIRIK.NoProyCancelados, ExcelHAlign.HAlignCenter, false, true, ref HojaExcelSF);
                            exportador.CeldaDatosFloatSF("K" + cont.ToString(), (float)itemLstCDIRIK.SumaProyCancelados, ExcelHAlign.HAlignRight, false, true, ref HojaExcelSF);

                            cont = cont + 1;
                        }
                        else
                        {
                            if (strRIIK != itemm.strRIK)
                            {
                                strRIIK = itemm.strRIK;
                                exportador.CeldaDatosSF("A" + cont.ToString() + ":A" + cont.ToString(), itemm.strRIK, ExcelHAlign.HAlignLeft, false, true, ref HojaExcelSF);

                                var itemLstCDIRIK = LstCDIRIK.Where(k => k.idCDI == itemm.Id_Cd && k.idRIIK == itemm.Id_RIK)
                                  .FirstOrDefault();

                                exportador.CeldaDatoIntSF("B" + cont.ToString(), itemLstCDIRIK.NoProyectos, ExcelHAlign.HAlignCenter, false, true, ref HojaExcelSF);
                                exportador.CeldaDatosFloatSF("C" + cont.ToString(), (float)itemLstCDIRIK.SumaProyectos, ExcelHAlign.HAlignRight, false, true, ref HojaExcelSF);

                                exportador.CeldaDatoIntSF("D" + cont.ToString(), itemLstCDIRIK.NoProyActuales, ExcelHAlign.HAlignCenter, false, true, ref HojaExcelSF);
                                exportador.CeldaDatosFloatSF("E" + cont.ToString(), (float)itemLstCDIRIK.SumaProyActuales, ExcelHAlign.HAlignRight, false, true, ref HojaExcelSF);

                                exportador.CeldaDatoIntSF("F" + cont.ToString(), itemLstCDIRIK.NoProyNuevos, ExcelHAlign.HAlignCenter, false, true, ref HojaExcelSF);
                                exportador.CeldaDatosFloatSF("G" + cont.ToString(), (float)itemLstCDIRIK.NoProyNuevos, ExcelHAlign.HAlignRight, false, true, ref HojaExcelSF);

                                exportador.CeldaDatoIntSF("H" + cont.ToString(), itemLstCDIRIK.NoProyCerrados, ExcelHAlign.HAlignCenter, false, true, ref HojaExcelSF);
                                exportador.CeldaDatosFloatSF("I" + cont.ToString(), (float)itemLstCDIRIK.SumaProyCerrados, ExcelHAlign.HAlignRight, false, true, ref HojaExcelSF);

                                exportador.CeldaDatoIntSF("J" + cont.ToString(), itemLstCDIRIK.NoProyCancelados, ExcelHAlign.HAlignCenter, false, true, ref HojaExcelSF);
                                exportador.CeldaDatosFloatSF("K" + cont.ToString(), (float)itemLstCDIRIK.SumaProyCancelados, ExcelHAlign.HAlignRight, false, true, ref HojaExcelSF);

                                cont = cont + 1;
                            }
                        }



                        /// DescAgrupador   TotalProyectos TotalProyectos_Monto    ProyectosActuales ProyectosActuales_Monto 
                        /// ProyectosNuevo ProyectosNuevo_Monto    ProyectosCerrados ProyectosCerrados_Monto ProyectosCancelados ProyectosCancelados_Monto
                        exportador.CeldaDatosSF("A" + cont.ToString(), itemm.strAgrupador, ExcelHAlign.HAlignLeft, false, false, ref HojaExcelSF);

                        exportador.CeldaDatoIntSF("B" + cont.ToString(), itemm.MontoFinal_Proy, ExcelHAlign.HAlignCenter, false, false, ref HojaExcelSF);
                        exportador.CeldaDatosFloatSF("C" + cont.ToString(), (float)itemm.MontoFinal_Monto, ExcelHAlign.HAlignRight, false, false, ref HojaExcelSF);

                        exportador.CeldaDatoIntSF("D" + cont.ToString(), itemm.ProyectosActuales_Proy, ExcelHAlign.HAlignCenter, false, false, ref HojaExcelSF);
                        exportador.CeldaDatosFloatSF("E" + cont.ToString(), (float)itemm.ProyectosActuales_Monto, ExcelHAlign.HAlignRight, false, false, ref HojaExcelSF);

                        exportador.CeldaDatoIntSF("F" + cont.ToString(), itemm.ProyectosNuevos_Proy, ExcelHAlign.HAlignCenter, false, false, ref HojaExcelSF);
                        exportador.CeldaDatosFloatSF("G" + cont.ToString(), (float)itemm.ProyectosNuevos_Monto, ExcelHAlign.HAlignRight, false, false, ref HojaExcelSF);

                        exportador.CeldaDatoIntSF("H" + cont.ToString(), itemm.ProyectosCerrados_Proy, ExcelHAlign.HAlignCenter, false, false, ref HojaExcelSF);
                        exportador.CeldaDatosFloatSF("I" + cont.ToString(), (float)itemm.ProyectosCerrados_Monto, ExcelHAlign.HAlignRight, false, false, ref HojaExcelSF);

                        exportador.CeldaDatoIntSF("J" + cont.ToString(), itemm.ProyectosCancelados_Proy, ExcelHAlign.HAlignCenter, false, false, ref HojaExcelSF);
                        exportador.CeldaDatosFloatSF("K" + cont.ToString(), (float)itemm.ProyectosCancelados_Monto, ExcelHAlign.HAlignRight, false, false, ref HojaExcelSF);

                        cont = cont + 1;
                    }

                    HojaExcelSF3.UsedRange.AutofitColumns();


                    /// string rutaguardado = Server.MapPath("~/Reportes/") + "ReporteCRMImpulsosQuimicos_" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";
                    string xfilenaaaame = "ReporteCRMImpulsosQuimicos_" + DateTime.Now.ToShortDateString().Replace("/", "") + "h" + DateTime.Now.ToShortTimeString().Replace(".", "").Replace(":", "").Replace(" ", "") + ".xlsx";
                    exportador.ServicioExportaExcelSyncF(LibroExcelSF, Response, xfilenaaaame);
                }
            }
        }

    }
}