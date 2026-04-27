using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using System.Data;
using System.IO;
using CapaNegocios;
using ClosedXML.Excel;
using System.Configuration;
using System.Web.Services;
using Newtonsoft.Json;
using DevExpress.XtraPrinting;
using DevExpress.Export;
using Spire.Xls;

namespace SIANWEB
{
    public partial class ReporteComerciales : System.Web.UI.Page
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

        #region Eventos 

        /// <summary>
        /// Precarga la informacion de los grid view  de:
        /// Presupuesto
        /// Multiplicador
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["SPresupuesto"] != null)
            {

                List<CatPresupuesto> lista = (List<CatPresupuesto>)Session["SPresupuesto"];
                RgPresupuesto.DataSource = lista;
                RgPresupuesto.DataBind();
                UpdatePanel2.Update();
            }
            if (Session["SMultiplicador"] != null)
            {

                List<CatMultiplicador> listaMultiplicador = new List<CatMultiplicador>();
                listaMultiplicador = (List<CatMultiplicador>)Session["SMultiplicador"];
                BGVMultiplicador.DataSource = listaMultiplicador;
                BGVMultiplicador.DataBind();
                UpdatePanel4.Update();
            }
            if (Session["listaMultiplicador"] != null)
            {
                List<CatMultiplicador> lista = new List<CatMultiplicador>();
                lista = (List<CatMultiplicador>)Session["listaMultiplicador"];
                gridRepresentanteMP.DataSource = lista;
                gridRepresentanteMP.DataBind();
                UPdBusacarinfo.Update();
                UpdatePanel6.Update();
            }
        }

        /// <summary>
        /// Inicio de la funciones de la pantalla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                ValidarPermisos();
                Session["Sucursal"] = null;
                Session["Representante"] = null;
                Session["TipoRepresentante"] = null;
                Session["SPresupuesto"] = null;
                Session["SMultiplicador"] = null;
                Session["listaMultiplicador"] = null;
                gridRepresentanteMP.DataSource = null;
                gridRepresentanteMP.DataBind();
                RgPresupuesto.DataSource = null;
                RgPresupuesto.DataBind();
                UpdatePanel2.Update();
                UpdatePanel6.Update();
                WCompararRepresentantes.PreRender += new EventHandler(WCompararRepresentantes_PreRender);
                if (sesion != null)
                {
                    inicializar();

                }
            }
            Session["activeMenu"] = 1;
        }





        protected void BTNDescargarMultiplicador_ServerClick(object sender, EventArgs e)
        {
            BGVMultiplicador.ExportXlsToResponse(new XlsExportOptionsEx { ExportType = ExportType.WYSIWYG });
        }

        protected void BtnDescargarEstadisticaPresupuesto_ServerClick(object sender, EventArgs e)
        {
            RgPresupuesto.ExportXlsToResponse(new XlsExportOptionsEx { ExportType = ExportType.WYSIWYG });
        }

        protected void btndescragrBuscarInformacion_ServerClick(object sender, EventArgs e)
        {
            gridRepresentanteMP.ExportXlsToResponse(new XlsExportOptionsEx { ExportType = ExportType.WYSIWYG });
        }


        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            cargarPresupuesto();
        }

        protected void btnBuscarMultiplicador_Click(object sender, ImageClickEventArgs e)
        {
            cargarMultiplicador();
        }

        protected void CmbSucursalRepresentante_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id_Cd = Convert.ToInt32(CMBSucursalRepresentante.Value.ToString());
            cargarRIk(id_Cd);
            updpanel3.Update();
        }



        /// <summary>
        /// evento que se llama en la seccion de comparar representante
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void wizClaimInfo_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {

            if (WCompararRepresentantes.WizardSteps[e.CurrentStepIndex].Title == "1")
            {
                if (CMBSucursalRepresentante.Value.ToString() == "-1")
                {
                    e.Cancel = true;
                }
                else
                {
                    Session["Sucursal"] = CMBSucursalRepresentante.Value.ToString();
                }
            }

            if (WCompararRepresentantes.WizardSteps[e.CurrentStepIndex].Title == "2")
            {
                txtrepre.Value = "";
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
                    txtrepre.Value = representante;
                    Session["Representante"] = representante;
                }

            }
            if (WCompararRepresentantes.WizardSteps[e.CurrentStepIndex].Title == "3")
            {
                Session["TipoRepresentante"] = RBLTipoRepresentante.Value.ToString();

            }
            WCompararRepresentantes.PreRender += new EventHandler(WCompararRepresentantes_PreRender);
            updpanel3.Update();
        }

        protected void cmbSucursalBinformacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id_Cd = Convert.ToInt32(cmbSucursalBinformacion.Value.ToString());
            cargarRIkBuscarInformacion(id_Cd);
            UPdBusacarinfo.Update();
            UpdatePanel6.Update();
        }



        /// <summary>
        /// funcion para que se visualize los steps
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void WCompararRepresentantes_PreRender(object sender, EventArgs e)
        {
            Repeater SideBarList = WCompararRepresentantes.FindControl("HeaderContainer").FindControl("SideBarList") as Repeater;
            SideBarList.DataSource = WCompararRepresentantes.WizardSteps;
            SideBarList.DataBind();
            updpanel3.Update();
        }

        /// <summary>
        /// eventos para el wizard botones
        /// </summary>
        /// <param name="wizardStep"></param>
        /// <returns></returns>
        protected string GetClassForWizardStep(object wizardStep)
        {
            WizardStep step = wizardStep as WizardStep;

            if (step == null)
            {
                return "";
            }
            int stepIndex = WCompararRepresentantes.WizardSteps.IndexOf(step);

            if (stepIndex < WCompararRepresentantes.ActiveStepIndex)
            {
                return "prevStep";
            }
            else if (stepIndex > WCompararRepresentantes.ActiveStepIndex)
            {
                return "nextStep";
            }
            else
            {
                return "currentStep";
            }
        }

        protected void btnBuscarInformacion_ServerClick(object sender, EventArgs e)
        {
            ValidarSesion();
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            string mesAnioInicial = txtfechaInicialBuscarInformacion.Text;
            string mesAniofinal = txtfechaFinalBuscarInformacion.Text;

            CN_Presupuesto cdpresupuesto = new CN_Presupuesto();
            CatPresupuesto Presupuesto = new CatPresupuesto();
            List<CatPresupuesto> listaPresupuesto = new List<CatPresupuesto>();
            CN_Presupuesto presupuesto = new CN_Presupuesto();
            List<CatPresupuesto> listaPresupuesto2 = new List<CatPresupuesto>();
            List<CatMultiplicador> listaMultiplicador = new List<CatMultiplicador>();
            CN_Multiplicador CN_multiplicador = new CN_Multiplicador();
            CatMultiplicador CatMultiplicador = new CatMultiplicador();

            if (int.Parse(cmbSucursalBinformacion.Value.ToString()) == -1)
            {
                mensaje("Favor de seleccionar la sucursal en seccion de buscar información.");
                return;
            }
            if (mesAnioInicial != "" && mesAniofinal != "")
            {

                Presupuesto.Id_Emp = sesion.Id_Emp;
                Presupuesto.Id_Cd = int.Parse(cmbSucursalBinformacion.Value.ToString());


                Presupuesto.MesInicial = Convert.ToInt32(mesAnioInicial.Split('/')[0]);
                Presupuesto.AnioInicial = Convert.ToInt32(mesAnioInicial.Split('/')[1]);
                Presupuesto.MesFinal = Convert.ToInt32(mesAniofinal.Split('/')[0]);
                Presupuesto.AnioFinal = Convert.ToInt32(mesAniofinal.Split('/')[1]);

                string FechaInicialD = ("01" + "/" + mesAnioInicial.Split('/')[0] + "/" + mesAnioInicial.Split('/')[1]);
                string FechaFinalD = ("01" + "/" + mesAniofinal.Split('/')[0] + "/" + mesAniofinal.Split('/')[1]);

                if (Convert.ToDateTime(FechaInicialD) > Convert.ToDateTime(FechaFinalD))
                {
                    mensaje("La fecha inicial es mayor a la fecha final de la sección de observar totales.");
                    return;
                }

                DateTime FechaInicial2 = DateTime.Parse(FechaInicialD);
                DateTime FechaFinal2 = DateTime.Parse(FechaFinalD).AddMonths(1).AddDays(-1);
                Presupuesto.FechaInicial = FechaInicial2;
                Presupuesto.fechafinal = FechaFinal2;
                Presupuesto.Id_Rik = Convert.ToInt32(cmbBuscarRepresentante.Value.ToString());
                Presupuesto.Id_u = Convert.ToInt32(cmbBuscarRepresentante.Value.ToString());

                CatMultiplicador.FechaInicial = FechaInicial2;
                CatMultiplicador.FechaFinal = FechaFinal2;
                CatMultiplicador.Id_Emp = Sesion.Id_Emp;
                CatMultiplicador.Id_Cd = int.Parse(cmbSucursalBinformacion.Value.ToString());

                cdpresupuesto.ConsultaUtilidadRIkxmes(Presupuesto, ref listaPresupuesto, Emp_CnxCentral);
                presupuesto.ConsultaPresupuestoMesualPvvRIk(Presupuesto, ref listaPresupuesto2, Emp_CnxCentral);
                CN_multiplicador.ConsultaMltiplicadorMRIk(CatMultiplicador, ref listaMultiplicador, Emp_CnxCentral);

                foreach (CatMultiplicador registro in listaMultiplicador)
                {
                    string mes = "";
                    ObtenerMEs(ref mes, registro.Mes);
                    registro.Nombre_Mes = mes;
                }

                string FechaInicial = ("01" + "/" + mesAnioInicial.Split('/')[0] + "/" + mesAnioInicial.Split('/')[1]);
                string FechaFinal = ("01" + "/" + mesAniofinal.Split('/')[0] + "/" + mesAniofinal.Split('/')[1]);

                List<CatMultiplicador> listaMultiplicadorquery = new List<CatMultiplicador>();
                CatMultiplicador Multiplicadorquery;

                if (Convert.ToInt32(cmbBuscarRepresentante.Value.ToString()) != -1)
                {
                    FechaInicial = ("01" + "/" + mesAnioInicial.Split('/')[0] + "/" + mesAnioInicial.Split('/')[1]);
                    FechaFinal = ("01" + "/" + mesAniofinal.Split('/')[0] + "/" + mesAniofinal.Split('/')[1]);


                    while (DateTime.Parse(FechaInicial) <= DateTime.Parse(FechaFinal))
                    {
                        Multiplicadorquery = new CatMultiplicador();
                        Multiplicadorquery.Id_Emp = sesion.Id_Emp;
                        Multiplicadorquery.Id_Cd = int.Parse(cmbSucursalBinformacion.Value.ToString());
                        Multiplicadorquery.Sucursal = cmbSucursalBinformacion.Text;
                        Multiplicadorquery.Id_Rik = int.Parse(cmbBuscarRepresentante.Value.ToString());
                        Multiplicadorquery.NombreRik = cmbBuscarRepresentante.Text;


                        List<CatPresupuesto> LsitaVenta = (from tlist in listaPresupuesto
                                                           where tlist.Id_Rik == Convert.ToInt32(cmbBuscarRepresentante.Value.ToString())
                                                           && tlist.Mes == DateTime.Parse(FechaInicial).Month
                                                           && tlist.Anio == DateTime.Parse(FechaInicial).Year
                                                           select tlist).ToList();

                        List<CatPresupuesto> lista = (from tlist in listaPresupuesto2
                                                      where tlist.Id_Rik == Convert.ToInt32(cmbBuscarRepresentante.Value.ToString())
                                                      && tlist.Mes == DateTime.Parse(FechaInicial).Month
                                                      && tlist.Anio == DateTime.Parse(FechaInicial).Year
                                                      select tlist).ToList();

                        List<CatMultiplicador> LMultiplicador = (from tlist in listaMultiplicador
                                                                 where tlist.Id_Rik == Convert.ToInt32(cmbBuscarRepresentante.Value.ToString())
                                                       && tlist.Mes == DateTime.Parse(FechaInicial).Month
                                                  && tlist.Anio == DateTime.Parse(FechaInicial).Year
                                                                 select tlist).ToList();

                        Multiplicadorquery.Mes = DateTime.Parse(FechaInicial).Month;

                        string mes = "";
                        ObtenerMEs(ref mes, DateTime.Parse(FechaInicial).Month);
                        Multiplicadorquery.Nombre_Mes = mes;


                        Multiplicadorquery.Anio = DateTime.Parse(FechaInicial).Year;
                        Multiplicadorquery.totalVenta = LsitaVenta.Count > 0 ? LsitaVenta[0].venta : 0;
                        Multiplicadorquery.TotalPresupuesto = lista.Count > 0 ? lista[0].TotalPresupuesto : 0;
                        Multiplicadorquery.TotalMultiplicador = LMultiplicador.Count > 0 ? (LMultiplicador[0].TotalMultiplicador / 100) : 0;

                        FechaInicial = DateTime.Parse(FechaInicial).AddMonths(1).ToString();
                        listaMultiplicadorquery.Add(Multiplicadorquery);
                    }


                }
                else
                {
                    for (var i = 0; i < cmbBuscarRepresentante.Items.Count(); i++)
                    {
                        if (Convert.ToInt32(cmbBuscarRepresentante.Items[i].Value.ToString()) != -1)
                        {
                            FechaInicial = ("01" + "/" + mesAnioInicial.Split('/')[0] + "/" + mesAnioInicial.Split('/')[1]);
                            FechaFinal = ("01" + "/" + mesAniofinal.Split('/')[0] + "/" + mesAniofinal.Split('/')[1]);

                            while (DateTime.Parse(FechaInicial) <= DateTime.Parse(FechaFinal))
                            {
                                Multiplicadorquery = new CatMultiplicador();
                                Multiplicadorquery.Id_Emp = sesion.Id_Emp;
                                Multiplicadorquery.Id_Cd = int.Parse(cmbSucursalBinformacion.Value.ToString());
                                Multiplicadorquery.Sucursal = cmbSucursalBinformacion.Text;
                                Multiplicadorquery.Id_Rik = int.Parse(cmbBuscarRepresentante.Items[i].Value.ToString());
                                Multiplicadorquery.NombreRik = cmbBuscarRepresentante.Items[i].Text;


                                List<CatPresupuesto> LsitaVenta = (from tlist in listaPresupuesto
                                                                   where tlist.Id_Rik == Convert.ToInt32(cmbBuscarRepresentante.Items[i].Value.ToString())
                                                                   && tlist.Mes == DateTime.Parse(FechaInicial).Month
                                                                   && tlist.Anio == DateTime.Parse(FechaInicial).Year
                                                                   select tlist).ToList();

                                List<CatPresupuesto> lista = (from tlist in listaPresupuesto2
                                                              where tlist.Id_Rik == Convert.ToInt32(cmbBuscarRepresentante.Items[i].Value.ToString())
                                                              && tlist.Mes == DateTime.Parse(FechaInicial).Month
                                                              && tlist.Anio == DateTime.Parse(FechaInicial).Year
                                                              select tlist).ToList();

                                List<CatMultiplicador> LMultiplicador = (from tlist in listaMultiplicador
                                                                         where tlist.Id_Rik == Convert.ToInt32(cmbBuscarRepresentante.Items[i].Value.ToString())
                                                               && tlist.Mes == DateTime.Parse(FechaInicial).Month
                                                          && tlist.Anio == DateTime.Parse(FechaInicial).Year
                                                                         select tlist).ToList();

                                Multiplicadorquery.Mes = DateTime.Parse(FechaInicial).Month;


                                string mes = "";
                                ObtenerMEs(ref mes, DateTime.Parse(FechaInicial).Month);
                                Multiplicadorquery.Nombre_Mes = mes;


                                Multiplicadorquery.Anio = DateTime.Parse(FechaInicial).Year;
                                Multiplicadorquery.totalVenta = LsitaVenta.Count > 0 ? LsitaVenta[0].venta : 0;
                                Multiplicadorquery.TotalPresupuesto = lista.Count > 0 ? lista[0].TotalPresupuesto : 0;
                                Multiplicadorquery.TotalMultiplicador = LMultiplicador.Count > 0 ? (LMultiplicador[0].TotalMultiplicador / 100) : 0;

                                FechaInicial = DateTime.Parse(FechaInicial).AddMonths(1).ToString();
                                listaMultiplicadorquery.Add(Multiplicadorquery);
                            }
                        }
                    }
                }
                listaMultiplicadorquery = listaMultiplicadorquery.OrderBy(x => x.Mes).ToList();


                Session["listaMultiplicador"] = listaMultiplicadorquery;
                gridRepresentanteMP.DataSource = listaMultiplicadorquery;
                gridRepresentanteMP.DataBind();
                UPdBusacarinfo.Update();
                UpdatePanel6.Update();
            }

        }


        protected void Button4_ServerClick(object sender, EventArgs e)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            int trimestreInicial = Convert.ToInt32(BcbTrimestreInicial.Value.ToString());
            int anioInicial = Convert.ToDateTime(txtfechaTrimestreInicial.Value.ToString()).Year;
            int trimesFinal = Convert.ToInt32(BcbTrimestreFinal.Value.ToString());
            int anioFinal = Convert.ToDateTime(TxtFechaTrimestreFinal.Value.ToString()).Year;

            using (var workbook = new XLWorkbook())
            {
                var HojaExcel = workbook.Worksheets.Add("Reportes");

                if (trimestreInicial != -1 && trimesFinal != -1)
                {


                    HojaExcel.Cell("A1").Value = CMBTrimestreSucursal.Text.ToString();
                    HojaExcel.Cell("A1").Style.Font.Bold = true;
                    HojaExcel.Cell("A1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                    decimal trimini = Convert.ToDecimal(anioInicial.ToString() + "." + trimestreInicial.ToString());
                    decimal trimfin = Convert.ToDecimal(anioFinal.ToString() + "." + trimesFinal.ToString());

                    int Primerrango = 2;
                    int segundoRango = 5;

                    for (var i = trimini; i <= trimfin;)
                    {

                        CN_Presupuesto cdpresupuesto = new CN_Presupuesto();
                        CatPresupuesto Presupuesto = new CatPresupuesto();
                        List<CatPresupuesto> listaPresupuesto = new List<CatPresupuesto>();
                        List<CatPresupuesto> listaPresupuestototaltrimestral = new List<CatPresupuesto>();
                        CN_Presupuesto presupuesto = new CN_Presupuesto();
                        List<CatPresupuesto> listaPresupuesto2 = new List<CatPresupuesto>();
                        CN_Multiplicador CN_multiplicador = new CN_Multiplicador();
                        CatMultiplicador CatMultiplicador = new CatMultiplicador();
                        List<CatMultiplicador> listaMultiplicador = new List<CatMultiplicador>();

                        Presupuesto.Id_Emp = sesion.Id_Emp;
                        Presupuesto.Id_Cd = int.Parse(CMBTrimestreSucursal.Value.ToString());
                        string fechaini = i.ToString().Split('.')[1];
                        string anio = i.ToString().Split('.')[0];


                        Presupuesto.trimestre = Convert.ToInt32(fechaini);
                        Presupuesto.anioTrimestre = Convert.ToInt32(anio);

                        DateTime FechaInicial2 = DateTime.Now;
                        DateTime FechaFinal2 = DateTime.Now;

                        if (fechaini == "1")
                        {
                            FechaInicial2 = DateTime.Parse("01/01/" + anio);
                            FechaFinal2 = FechaInicial2.AddMonths(3).AddDays(-1);
                        }
                        if (fechaini == "2")
                        {
                            FechaInicial2 = DateTime.Parse("01/04/" + anio);
                            FechaFinal2 = FechaInicial2.AddMonths(3).AddDays(-1);
                        }
                        if (fechaini == "3")
                        {
                            FechaInicial2 = DateTime.Parse("01/07/" + anio);
                            FechaFinal2 = FechaInicial2.AddMonths(3).AddDays(-1);
                        }
                        if (fechaini == "4")
                        {
                            FechaInicial2 = DateTime.Parse("01/10/" + anio);
                            FechaFinal2 = FechaInicial2.AddMonths(3).AddDays(-1);
                        }


                        Presupuesto.FechaInicial = FechaInicial2;
                        Presupuesto.fechafinal = FechaFinal2;



                        Presupuesto.MesInicial = FechaInicial2.Month;
                        Presupuesto.AnioInicial = FechaInicial2.Year;
                        Presupuesto.MesFinal = FechaFinal2.Month;
                        Presupuesto.AnioFinal = FechaFinal2.Year;



                        CatMultiplicador.FechaInicial = FechaInicial2;
                        CatMultiplicador.FechaFinal = FechaFinal2;
                        CatMultiplicador.Id_Emp = Sesion.Id_Emp;
                        CatMultiplicador.Id_Cd = int.Parse(CMBTrimestreSucursal.Value.ToString());

                        cdpresupuesto.ConsultaPresupuestoMesualRIk(Presupuesto, ref listaPresupuesto, Emp_CnxCentral);
                        cdpresupuesto.ConsultaUtilidadRIk(Presupuesto, ref listaPresupuesto2, Emp_CnxCentral);
                        CN_multiplicador.ConsultaMltiplicadorMRIk(CatMultiplicador, ref listaMultiplicador, Emp_CnxCentral);
                        cdpresupuesto.ConsultaVentaTotalTrimestral(Presupuesto, ref listaPresupuestototaltrimestral, Emp_CnxCentral);



                        HojaExcel.Range(HojaExcel.Cell(1, Primerrango), HojaExcel.Cell(1, segundoRango)).Merge();
                        HojaExcel.Range(HojaExcel.Cell(1, Primerrango), HojaExcel.Cell(1, segundoRango)).Value = "T" + fechaini + "- " + anio;
                        HojaExcel.Range(HojaExcel.Cell(1, Primerrango), HojaExcel.Cell(1, segundoRango)).Style.Font.Italic = true;
                        HojaExcel.Range(HojaExcel.Cell(1, Primerrango), HojaExcel.Cell(1, segundoRango)).Style.Fill.BackgroundColor = XLColor.Silver;
                        HojaExcel.Range(HojaExcel.Cell(1, Primerrango), HojaExcel.Cell(1, segundoRango)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        HojaExcel.Range(HojaExcel.Cell(1, Primerrango), HojaExcel.Cell(1, segundoRango)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


                        HojaExcel.Range(HojaExcel.Cell(2, 1), HojaExcel.Cell(2, 1)).Value = "Representante";
                        HojaExcel.Range(HojaExcel.Cell(2, 1), HojaExcel.Cell(2, 1)).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                        HojaExcel.Range(HojaExcel.Cell(2, 1), HojaExcel.Cell(2, 1)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                        HojaExcel.Cell(2, Primerrango).Value = "Prom.vta" + " T" + fechaini;
                        HojaExcel.Cell(2, Primerrango).Style.Fill.BackgroundColor = XLColor.Silver;
                        HojaExcel.Cell(2, Primerrango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                        HojaExcel.Cell(2, Primerrango + 1).Value = "Pptp" + " T" + fechaini;
                        HojaExcel.Cell(2, Primerrango + 1).Style.Fill.BackgroundColor = XLColor.Silver;
                        HojaExcel.Cell(2, Primerrango + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                        HojaExcel.Cell(2, Primerrango + 2).Value = "%Cumpl";
                        HojaExcel.Cell(2, Primerrango + 2).Style.Fill.BackgroundColor = XLColor.Silver;
                        HojaExcel.Cell(2, Primerrango + 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        HojaExcel.Cell(2, Primerrango + 3).Value = "%Multiplicador";
                        HojaExcel.Cell(2, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.Silver;
                        HojaExcel.Cell(2, Primerrango + 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;



                        if (BCMRepresentanteTrimestral.Value.ToString() != "-1")
                        {
                            int j = 0;
                            HojaExcel.Range(HojaExcel.Cell(3 + j, 1), HojaExcel.Cell(3 + j, 1));

                            HojaExcel.Range(HojaExcel.Cell(3 + j, 1), HojaExcel.Cell(3 + j, 1)).Value = BCMRepresentanteTrimestral.Text;
                            HojaExcel.Range(HojaExcel.Cell(3 + j, 1), HojaExcel.Cell(3 + j, 1)).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                            HojaExcel.Range(HojaExcel.Cell(3 + j, 1), HojaExcel.Cell(3 + j, 1)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;



                            List<CatPresupuesto> lista = (from tlist in listaPresupuesto
                                                          where tlist.Id_Rik == Convert.ToInt32(BCMRepresentanteTrimestral.Value.ToString())
                                                          select tlist).ToList();

                            List<CatPresupuesto> LsitaVenta = (from tlist in listaPresupuesto2
                                                               where tlist.id_ter == Convert.ToInt32(BCMRepresentanteTrimestral.Value.ToString())
                                                               select tlist).ToList();

                            List<CatMultiplicador> LMultiplicador = (from tlist in listaMultiplicador
                                                                     where tlist.Id_Rik == Convert.ToInt32(BCMRepresentanteTrimestral.Value.ToString())
                                                                     select tlist).ToList();



                            HojaExcel.Cell(3 + j, Primerrango).Value = LsitaVenta.Count > 0 ? (LsitaVenta[0].venta / 3).ToString() : "0";
                            HojaExcel.Cell(3 + j, Primerrango).Style.Fill.BackgroundColor = XLColor.White;
                            HojaExcel.Cell(3 + j, Primerrango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            HojaExcel.Cell(3 + j, Primerrango).Style.NumberFormat.Format = "$0.00";
                            HojaExcel.Cell(3 + j, Primerrango).DataType = XLDataType.Number;



                            HojaExcel.Cell(3 + j, Primerrango + 1).Value = lista.Count > 0 ? (lista[0].TotalPresupuesto / 3).ToString() : "0";
                            HojaExcel.Cell(3 + j, Primerrango + 1).Style.Fill.BackgroundColor = XLColor.White;
                            HojaExcel.Cell(3 + j, Primerrango + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            HojaExcel.Cell(3 + j, Primerrango + 1).Style.NumberFormat.Format = "$0.00";
                            HojaExcel.Cell(3 + j, Primerrango + 1).DataType = XLDataType.Number;


                            if (double.Parse(lista.Count > 0 ? lista[0].TotalPresupuesto.ToString() : "0") > 0)
                            {
                                double valor = ((double.Parse(lista.Count > 0 ? lista[0].TotalPresupuesto.ToString() : "0") / double.Parse(LsitaVenta.Count > 0 ? LsitaVenta[0].venta.ToString() : "0")));
                                HojaExcel.Cell(3 + j, Primerrango + 2).Value = valor;
                                HojaExcel.Cell(3 + j, Primerrango + 2).DataType = XLDataType.Number;
                                if (valor > 80)
                                {
                                    HojaExcel.Cell(3 + j, Primerrango + 2).Style.Fill.BackgroundColor = XLColor.LightGreen;
                                }
                                else if (valor > 60)
                                {
                                    HojaExcel.Cell(3 + j, Primerrango + 2).Style.Fill.BackgroundColor = XLColor.Yellow;
                                }
                                else
                                {
                                    HojaExcel.Cell(3 + j, Primerrango + 2).Style.Fill.BackgroundColor = XLColor.PaleVioletRed;
                                }
                            }
                            else
                            {
                                HojaExcel.Cell(3 + j, Primerrango + 2).Value = "0%";
                                HojaExcel.Cell(3 + j, Primerrango + 2).Style.Fill.BackgroundColor = XLColor.PaleVioletRed;
                            }
                            HojaExcel.Cell(3 + j, Primerrango + 2).Style.NumberFormat.Format = "##,###%";
                            HojaExcel.Cell(3 + j, Primerrango + 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                            if (LMultiplicador.Count > 0)
                            {
                                double totalMultiplicador = Convert.ToDouble(LMultiplicador.Sum(x => x.TotalMultiplicador).ToString()) / 3;
                                if (totalMultiplicador > .80)
                                {
                                    HojaExcel.Cell(3 + j, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.LightGreen;
                                }
                                else if (totalMultiplicador > .60)
                                {
                                    HojaExcel.Cell(3 + j, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.Yellow;
                                }
                                else
                                {
                                    HojaExcel.Cell(3 + j, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.PaleVioletRed;
                                }
                            }
                            else
                            {
                                HojaExcel.Cell(3 + j, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.PaleVioletRed;
                            }
                            double totalMultipli = LMultiplicador.Count > 0 ? Convert.ToDouble((LMultiplicador.Sum(x => x.TotalMultiplicador) / 3).ToString()) : 0;

                            HojaExcel.Cell(3 + j, Primerrango + 3).Value = totalMultipli / 100;
                            HojaExcel.Cell(3 + j, Primerrango + 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            HojaExcel.Cell(3 + j, Primerrango + 3).Style.NumberFormat.Format = "##,###%";
                            HojaExcel.Cell(3 + j, Primerrango + 3).DataType = XLDataType.Number;


                            Primerrango = segundoRango + 1;
                            segundoRango = Primerrango + 3;
                        }
                        else
                        {
                            for (var j = 0; j < BCMRepresentanteTrimestral.Items.Count(); j++)
                            {
                                if (BCMRepresentanteTrimestral.Items[j].Value != null)
                                {

                                    HojaExcel.Range(HojaExcel.Cell(3 + j, 1), HojaExcel.Cell(3 + j, 1)).Merge();
                                    HojaExcel.Range(HojaExcel.Cell(3 + j, 1), HojaExcel.Cell(3 + j, 1)).Value = BCMRepresentanteTrimestral.Items[j].Text;
                                    HojaExcel.Range(HojaExcel.Cell(3 + j, 1), HojaExcel.Cell(3 + j, 1)).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                                    HojaExcel.Range(HojaExcel.Cell(3 + j, 1), HojaExcel.Cell(3 + j, 1)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                                    List<CatPresupuesto> lista = new List<CatPresupuesto>();
                                    List<CatPresupuesto> LsitaVenta = new List<CatPresupuesto>();

                                    lista = (from tlist in listaPresupuesto
                                             where tlist.Id_Rik == Convert.ToInt32(BCMRepresentanteTrimestral.Items[j].Value.ToString())

                                             select tlist).ToList();

                                    LsitaVenta = (from tlist in listaPresupuesto2
                                                  where tlist.id_ter == Convert.ToInt32(BCMRepresentanteTrimestral.Items[j].Value.ToString())

                                                  select tlist).ToList();

                                    List<CatMultiplicador> LMultiplicador = (from tlist in listaMultiplicador
                                                                             where tlist.Id_Rik == Convert.ToInt32(BCMRepresentanteTrimestral.Items[j].Value.ToString())
                                                                             select tlist).ToList();



                                    HojaExcel.Cell(3 + j, Primerrango).Value = LsitaVenta.Count > 0 ? (LsitaVenta[0].venta / 3).ToString() : "0";
                                    HojaExcel.Cell(3 + j, Primerrango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                    HojaExcel.Cell(3 + j, Primerrango).Style.NumberFormat.Format = "$0.00";
                                    HojaExcel.Cell(3 + j, Primerrango).DataType = XLDataType.Number;



                                    HojaExcel.Cell(3 + j, Primerrango + 1).Value = lista.Count > 0 ? (lista[0].TotalPresupuesto / 3).ToString() : "0";
                                    HojaExcel.Cell(3 + j, Primerrango + 1).Style.Fill.BackgroundColor = XLColor.White;
                                    HojaExcel.Cell(3 + j, Primerrango + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                    HojaExcel.Cell(3 + j, Primerrango + 1).Style.NumberFormat.Format = "$0.00";
                                    HojaExcel.Cell(3 + j, Primerrango + 1).DataType = XLDataType.Number;


                                    if (double.Parse(LsitaVenta.Count > 0 ? LsitaVenta[0].utilidadBruta.ToString() : "0") > 0)
                                    {
                                        double listaVnta = ((double.Parse(lista.Count > 0 ? (lista[0].TotalPresupuesto / 3).ToString() : "0") / double.Parse(LsitaVenta.Count > 0 ? (LsitaVenta[0].venta / 3).ToString() : "0")));

                                        HojaExcel.Cell(3 + j, Primerrango + 2).Value = listaVnta;
                                        HojaExcel.Cell(3 + j, Primerrango + 2).Style.Fill.BackgroundColor = XLColor.LightGreen;
                                    }
                                    else
                                    {
                                        HojaExcel.Cell(3 + j, Primerrango + 2).Value = "0";
                                        HojaExcel.Cell(3 + j, Primerrango + 2).Style.Fill.BackgroundColor = XLColor.White;
                                    }
                                    HojaExcel.Cell(3 + j, Primerrango + 2).Style.NumberFormat.Format = "##,###%";
                                    HojaExcel.Cell(3 + j, Primerrango + 2).DataType = XLDataType.Number;

                                    if (double.Parse(LsitaVenta.Count > 0 ? LsitaVenta[0].utilidadBruta.ToString() : "0") > 0)
                                    {
                                        double valor = ((double.Parse(lista.Count > 0 ? (lista[0].TotalPresupuesto / 3).ToString() : "0") / double.Parse(LsitaVenta.Count > 0 ? (LsitaVenta[0].venta / 3).ToString() : "0")) * 100);


                                        if (valor > 80)
                                        {
                                            HojaExcel.Cell(3 + j, Primerrango + 2).Style.Fill.BackgroundColor = XLColor.LightGreen;
                                        }
                                        else if (valor > 60)
                                        {
                                            HojaExcel.Cell(3 + j, Primerrango + 2).Style.Fill.BackgroundColor = XLColor.Yellow;
                                        }
                                        else
                                        {
                                            HojaExcel.Cell(3 + j, Primerrango + 2).Style.Fill.BackgroundColor = XLColor.PaleVioletRed;
                                        }
                                    }
                                    else
                                    {
                                        HojaExcel.Cell(3 + j, Primerrango + 2).Value = "0%";
                                        HojaExcel.Cell(3 + j, Primerrango + 2).Style.Fill.BackgroundColor = XLColor.PaleVioletRed;
                                    }

                                    HojaExcel.Cell(3 + j, Primerrango + 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                                    if (LMultiplicador.Count > 0)
                                    {
                                        double totalMultiplicador = Convert.ToDouble(LMultiplicador.Sum(x => x.TotalMultiplicador).ToString()) / 3;
                                        if (totalMultiplicador > .80)
                                        {
                                            HojaExcel.Cell(3 + j, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.LightGreen;
                                        }
                                        else if (totalMultiplicador > .60)
                                        {
                                            HojaExcel.Cell(3 + j, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.Yellow;
                                        }
                                        else
                                        {
                                            HojaExcel.Cell(3 + j, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.PaleVioletRed;
                                        }
                                    }
                                    else
                                    {
                                        HojaExcel.Cell(3 + j, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.PaleVioletRed;
                                    }

                                    double totalMult = Convert.ToDouble(LMultiplicador.Count > 0 ? (LMultiplicador.Sum(x => x.TotalMultiplicador) / 3).ToString() : "0");
                                    HojaExcel.Cell(3 + j, Primerrango + 3).Value = totalMult / 100;
                                    HojaExcel.Cell(3 + j, Primerrango + 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                    HojaExcel.Cell(3 + j, Primerrango + 3).Style.NumberFormat.Format = "##,###%";
                                    HojaExcel.Cell(3 + j, Primerrango + 3).DataType = XLDataType.Number;


                                }
                            }
                            Primerrango = segundoRango + 1;
                            segundoRango = Primerrango + 3;
                        }

                        if (Convert.ToInt32(fechaini) < 4)
                        {
                            fechaini = (Convert.ToInt32(fechaini) + 1).ToString();
                        }
                        else
                        {
                            fechaini = "1";
                            anio = (Convert.ToInt32(anio) + 1).ToString();
                        }
                        i = Convert.ToDecimal(anio.ToString() + "." + fechaini.ToString());
                    }

                    if (BCMRepresentanteTrimestral.Value.ToString() != "-1")
                    {
                        Primerrango = 2;
                        segundoRango = 5;

                        trimini = Convert.ToDecimal(anioInicial.ToString() + "." + trimestreInicial.ToString());
                        trimfin = Convert.ToDecimal(anioFinal.ToString() + "." + trimesFinal.ToString());

                        for (var i = trimini; i <= trimfin;)
                        {
                            string fechaini = i.ToString().Split('.')[1];
                            string anio = i.ToString().Split('.')[0];
                            DateTime FechaInicial2 = DateTime.Now;
                            DateTime FechaFinal2 = DateTime.Now;

                            if (fechaini == "1")
                            {
                                FechaInicial2 = DateTime.Parse("01/01/" + anio);
                                FechaFinal2 = FechaInicial2.AddMonths(3).AddDays(-1);
                            }
                            if (fechaini == "2")
                            {
                                FechaInicial2 = DateTime.Parse("01/04/" + anio);
                                FechaFinal2 = FechaInicial2.AddMonths(3).AddDays(-1);
                            }
                            if (fechaini == "3")
                            {
                                FechaInicial2 = DateTime.Parse("01/07/" + anio);
                                FechaFinal2 = FechaInicial2.AddMonths(3).AddDays(-1);
                            }
                            if (fechaini == "4")
                            {
                                FechaInicial2 = DateTime.Parse("01/10/" + anio);
                                FechaFinal2 = FechaInicial2.AddMonths(3).AddDays(-1);
                            }
                            CN_Presupuesto cdpresupuesto = new CN_Presupuesto();
                            CatPresupuesto Presupuesto = new CatPresupuesto();

                            Presupuesto.Id_Emp = sesion.Id_Emp;
                            Presupuesto.Id_Cd = int.Parse(CMBTrimestreSucursal.Value.ToString());
                            Presupuesto.AnioInicial = Convert.ToInt32(anio);
                            Presupuesto.AnioFinal = Convert.ToInt32(anio);

                            List<CatPresupuesto> listaPresupuestototaltrimestral = new List<CatPresupuesto>();
                            cdpresupuesto.ConsultaVentaTotalTrimestral(Presupuesto, ref listaPresupuestototaltrimestral, Emp_CnxCentral);


                            List<CatPresupuesto> lista = (from tlist in listaPresupuestototaltrimestral
                                                          where tlist.Mes == FechaInicial2.Month
                                                          && tlist.Anio == FechaInicial2.Year
                                                          select tlist).ToList();



                            HojaExcel.Cell(4, 1).Value = "Total:";
                            HojaExcel.Cell(4, 1).Style.Fill.BackgroundColor = XLColor.Yellow;
                            HojaExcel.Cell(4, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                            HojaExcel.Cell(4, Primerrango).FormulaA1 = "=Sum(" + HojaExcel.Cell(3, Primerrango).Address + ":" + HojaExcel.Cell(3, Primerrango).Address + ")";
                            HojaExcel.Cell(4, Primerrango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            HojaExcel.Cell(4, Primerrango).Style.Fill.BackgroundColor = XLColor.Yellow;
                            HojaExcel.Cell(4, Primerrango).Style.NumberFormat.Format = "$0.00";
                            HojaExcel.Cell(4, Primerrango).DataType = XLDataType.Number;


                            HojaExcel.Cell(4, Primerrango + 1).FormulaA1 = "=Sum(" + HojaExcel.Cell(3, Primerrango + 1).Address + ":" + HojaExcel.Cell(3, Primerrango + 1).Address + ")";
                            HojaExcel.Cell(4, Primerrango + 1).Style.Fill.BackgroundColor = XLColor.Yellow;
                            HojaExcel.Cell(4, Primerrango + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            HojaExcel.Cell(4, Primerrango + 1).Style.NumberFormat.Format = "$0.00";
                            HojaExcel.Cell(4, Primerrango + 1).DataType = XLDataType.Number;


                            HojaExcel.Cell(4, Primerrango + 2).FormulaA1 = "=((" + HojaExcel.Cell(4, Primerrango + 1).Address + "/" + HojaExcel.Cell(4, Primerrango).Address + "))";
                            HojaExcel.Cell(4, Primerrango + 2).Style.Fill.BackgroundColor = XLColor.Yellow;
                            HojaExcel.Cell(4, Primerrango + 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            HojaExcel.Cell(4, Primerrango + 2).Style.NumberFormat.Format = "##,###%";

                            HojaExcel.Cell(4, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.Yellow;
                            HojaExcel.Cell(4, Primerrango + 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                            Primerrango = segundoRango + 1;
                            segundoRango = Primerrango + 3;


                            if (Convert.ToInt32(fechaini) < 4)
                            {
                                fechaini = (Convert.ToInt32(fechaini) + 1).ToString();
                            }
                            else
                            {
                                fechaini = "1";
                                anio = (Convert.ToInt32(anio) + 1).ToString();
                            }
                            i = Convert.ToDecimal(anio.ToString() + "." + fechaini.ToString());
                        }
                    }
                    else
                    {
                        Primerrango = 2;
                        segundoRango = 5;

                        trimini = Convert.ToDecimal(anioInicial.ToString() + "." + trimestreInicial.ToString());
                        trimfin = Convert.ToDecimal(anioFinal.ToString() + "." + trimesFinal.ToString());

                        for (var i = trimini; i <= trimfin;)
                        {
                            string fechaini = i.ToString().Split('.')[1];
                            string anio = i.ToString().Split('.')[0];
                            DateTime FechaInicial2 = DateTime.Now;
                            DateTime FechaFinal2 = DateTime.Now;

                            if (fechaini == "1")
                            {
                                FechaInicial2 = DateTime.Parse("01/01/" + anio);
                                FechaFinal2 = FechaInicial2.AddMonths(3).AddDays(-1);
                            }
                            if (fechaini == "2")
                            {
                                FechaInicial2 = DateTime.Parse("01/04/" + anio);
                                FechaFinal2 = FechaInicial2.AddMonths(3).AddDays(-1);
                            }
                            if (fechaini == "3")
                            {
                                FechaInicial2 = DateTime.Parse("01/07/" + anio);
                                FechaFinal2 = FechaInicial2.AddMonths(3).AddDays(-1);
                            }
                            if (fechaini == "4")
                            {
                                FechaInicial2 = DateTime.Parse("01/10/" + anio);
                                FechaFinal2 = FechaInicial2.AddMonths(3).AddDays(-1);
                            }

                            CN_Presupuesto cdpresupuesto = new CN_Presupuesto();
                            CatPresupuesto Presupuesto = new CatPresupuesto();

                            Presupuesto.Id_Emp = sesion.Id_Emp;
                            Presupuesto.Id_Cd = int.Parse(CMBTrimestreSucursal.Value.ToString());
                            Presupuesto.AnioInicial = Convert.ToInt32(anio);
                            Presupuesto.AnioFinal = Convert.ToInt32(anio);
                            Presupuesto.FechaInicial = FechaInicial2;
                            Presupuesto.fechafinal = FechaFinal2;

                            List<CatPresupuesto> listaPresupuestototaltrimestral = new List<CatPresupuesto>();
                            List<CatPresupuesto> listaRemisiontotaltrimestral = new List<CatPresupuesto>();
                            List<CatPresupuesto> listaPresupuesto = new List<CatPresupuesto>();


                            cdpresupuesto.ConsultaVentaTotalTrimestral(Presupuesto, ref listaPresupuestototaltrimestral, Emp_CnxCentral);
                            cdpresupuesto.ConsultRemisionTotalTrimestral(Presupuesto, ref listaRemisiontotaltrimestral, Emp_CnxCentral);
                            cdpresupuesto.ConsultaPresupuestoMesualRIk(Presupuesto, ref listaPresupuesto, Emp_CnxCentral);

                            List<CatPresupuesto> lista = (from tlist in listaPresupuestototaltrimestral
                                                          where tlist.Mes == FechaInicial2.Month
                                                          && tlist.Anio == FechaInicial2.Year
                                                          select tlist).ToList();

                            List<CatPresupuesto> listaremision = (from tlist in listaRemisiontotaltrimestral
                                                                  where tlist.Mes == FechaInicial2.Month
                                                                  && tlist.Anio == FechaInicial2.Year
                                                                  select tlist).ToList();


                            List<CatPresupuesto> lista2 = (from tlist in listaPresupuesto
                                                           where tlist.Id_Rik == 80
                                                           select tlist).ToList();



                            HojaExcel.Range(HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 1, 1), HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 1, 1)).Value = "Movimiento 80";
                            HojaExcel.Range(HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 1, 1), HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 1, 1)).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                            HojaExcel.Range(HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 1, 1), HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 1, 1)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 1, Primerrango).Value = listaremision.First().TotalPresupuesto;
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 1, Primerrango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 1, Primerrango).Style.Fill.BackgroundColor = XLColor.White;
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 1, Primerrango).Style.NumberFormat.Format = "$0.00";
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 1, Primerrango).DataType = XLDataType.Number;


                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 1, Primerrango + 1).Value = lista2.Count > 0 ? (lista2[0].TotalPresupuesto / 3).ToString() : "0";
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 1, Primerrango + 1).Style.Fill.BackgroundColor = XLColor.White;
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 1, Primerrango + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 1, Primerrango + 1).Style.NumberFormat.Format = "$0.00";
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 1, Primerrango + 1).DataType = XLDataType.Number;


                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 1, Primerrango + 2).FormulaA1 = "=((" + HojaExcel.Cell(4, Primerrango + 1).Address + "/" + HojaExcel.Cell(4, Primerrango).Address + "))";
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 1, Primerrango + 2).Style.Fill.BackgroundColor = XLColor.White;
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 1, Primerrango + 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, 1).Value = "Total:";
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, 1).Style.Fill.BackgroundColor = XLColor.Yellow;
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, Primerrango).FormulaA1 = "=Sum(" + HojaExcel.Cell(3 + 0, Primerrango).Address + ":" + HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 1, Primerrango).Address + ")";
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, Primerrango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, Primerrango).Style.Fill.BackgroundColor = XLColor.Yellow;
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, Primerrango).Style.NumberFormat.Format = "$0.00";
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, Primerrango).DataType = XLDataType.Number;


                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, Primerrango + 1).FormulaA1 = "=Sum(" + HojaExcel.Cell(3 + 0, Primerrango + 1).Address + ":" + HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 2, Primerrango + 1).Address + ")";
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, Primerrango + 1).Style.Fill.BackgroundColor = XLColor.Yellow;
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, Primerrango + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, Primerrango + 1).Style.NumberFormat.Format = "$0.00";
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, Primerrango + 1).DataType = XLDataType.Number;


                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, Primerrango + 2).FormulaA1 = "=((" + HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, Primerrango + 1).Address + "/" + HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, Primerrango).Address + "))";
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, Primerrango + 2).Style.Fill.BackgroundColor = XLColor.Yellow;
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, Primerrango + 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, Primerrango + 2).Style.NumberFormat.Format = "##,###%";
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.Yellow;
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, Primerrango + 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                            Primerrango = segundoRango + 1;
                            segundoRango = Primerrango + 3;


                            if (Convert.ToInt32(fechaini) < 4)
                            {
                                fechaini = (Convert.ToInt32(fechaini) + 1).ToString();
                            }
                            else
                            {
                                fechaini = "1";
                                anio = (Convert.ToInt32(anio) + 1).ToString();
                            }
                            i = Convert.ToDecimal(anio.ToString() + "." + fechaini.ToString());
                        }
                    }
                }
                else
                {
                    mensaje("Se debe de seleccionar los trimestre a consultar");
                    return;
                }
                HojaExcel.Columns().AdjustToContents();

                string rutaguardado = Server.MapPath("~/Reportes/") + "ReporteComercialesTrimestral_" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";


                if (File.Exists(rutaguardado))
                {
                    File.Delete(rutaguardado);
                }

                workbook.SaveAs(rutaguardado);

            }
            string ruta = Server.MapPath("~/Reportes/") + "ReporteComercialesTrimestral_" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";
            string ruta2 = Server.MapPath("~/Reportes/") + "ReporteComercialesTrimestral_" + DateTime.Now.ToString("ddMMyyyy") + ".PDF";

            Workbook workbook2 = new Workbook();
            workbook2.LoadFromFile(ruta);
            workbook2.ConverterSetting.SheetFitToPage = true;
            workbook2.SaveToFile(ruta2, FileFormat.PDF);


            System.IO.FileInfo file = new System.IO.FileInfo(ruta2);


            string Outgoingfile = "ReporteComercialesTrimestral_" + DateTime.Now.ToString("ddMMyyyy") + ".pdf";
            if (file.Exists)
            {
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Outgoingfile);
                Response.WriteFile(file.FullName);

            }
            else
            {

                Response.Write("This file does not exist.");
            }
        }

        protected void btnTrimestralDescarga_ServerClick(object sender, EventArgs e)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            int trimestreInicial = Convert.ToInt32(BcbTrimestreInicial.Value.ToString());
            int anioInicial = Convert.ToDateTime(txtfechaTrimestreInicial.Value.ToString()).Year;
            int trimesFinal = Convert.ToInt32(BcbTrimestreFinal.Value.ToString());
            int anioFinal = Convert.ToDateTime(TxtFechaTrimestreFinal.Value.ToString()).Year;

            using (var workbook = new XLWorkbook())
            {
                var HojaExcel = workbook.Worksheets.Add("Reportes");

                if (trimestreInicial != -1 && trimesFinal != -1)
                {


                    HojaExcel.Cell("A1").Value = CMBTrimestreSucursal.Text.ToString();
                    HojaExcel.Cell("A1").Style.Font.Bold = true;
                    HojaExcel.Cell("A1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                    decimal trimini = Convert.ToDecimal(anioInicial.ToString() + "." + trimestreInicial.ToString());
                    decimal trimfin = Convert.ToDecimal(anioFinal.ToString() + "." + trimesFinal.ToString());

                    int Primerrango = 2;
                    int segundoRango = 5;

                    for (var i = trimini; i <= trimfin;)
                    {

                        CN_Presupuesto cdpresupuesto = new CN_Presupuesto();
                        CatPresupuesto Presupuesto = new CatPresupuesto();
                        List<CatPresupuesto> listaPresupuesto = new List<CatPresupuesto>();
                        List<CatPresupuesto> listaPresupuestototaltrimestral = new List<CatPresupuesto>();
                        CN_Presupuesto presupuesto = new CN_Presupuesto();
                        List<CatPresupuesto> listaPresupuesto2 = new List<CatPresupuesto>();
                        CN_Multiplicador CN_multiplicador = new CN_Multiplicador();
                        CatMultiplicador CatMultiplicador = new CatMultiplicador();
                        List<CatMultiplicador> listaMultiplicador = new List<CatMultiplicador>();

                        Presupuesto.Id_Emp = sesion.Id_Emp;
                        Presupuesto.Id_Cd = int.Parse(CMBTrimestreSucursal.Value.ToString());
                        string fechaini = i.ToString().Split('.')[1];
                        string anio = i.ToString().Split('.')[0];


                        Presupuesto.trimestre = Convert.ToInt32(fechaini);
                        Presupuesto.anioTrimestre = Convert.ToInt32(anio);

                        DateTime FechaInicial2 = DateTime.Now;
                        DateTime FechaFinal2 = DateTime.Now;

                        if (fechaini == "1")
                        {
                            FechaInicial2 = DateTime.Parse("01/01/" + anio);
                            FechaFinal2 = FechaInicial2.AddMonths(3).AddDays(-1);
                        }
                        if (fechaini == "2")
                        {
                            FechaInicial2 = DateTime.Parse("01/04/" + anio);
                            FechaFinal2 = FechaInicial2.AddMonths(3).AddDays(-1);
                        }
                        if (fechaini == "3")
                        {
                            FechaInicial2 = DateTime.Parse("01/07/" + anio);
                            FechaFinal2 = FechaInicial2.AddMonths(3).AddDays(-1);
                        }
                        if (fechaini == "4")
                        {
                            FechaInicial2 = DateTime.Parse("01/10/" + anio);
                            FechaFinal2 = FechaInicial2.AddMonths(3).AddDays(-1);
                        }


                        Presupuesto.FechaInicial = FechaInicial2;
                        Presupuesto.fechafinal = FechaFinal2;



                        Presupuesto.MesInicial = FechaInicial2.Month;
                        Presupuesto.AnioInicial = FechaInicial2.Year;
                        Presupuesto.MesFinal = FechaFinal2.Month;
                        Presupuesto.AnioFinal = FechaFinal2.Year;



                        CatMultiplicador.FechaInicial = FechaInicial2;
                        CatMultiplicador.FechaFinal = FechaFinal2;
                        CatMultiplicador.Id_Emp = Sesion.Id_Emp;
                        CatMultiplicador.Id_Cd = int.Parse(CMBTrimestreSucursal.Value.ToString());

                        cdpresupuesto.ConsultaPresupuestoMesualRIk(Presupuesto, ref listaPresupuesto, Emp_CnxCentral);
                        cdpresupuesto.ConsultaUtilidadRIk(Presupuesto, ref listaPresupuesto2, Emp_CnxCentral);
                        CN_multiplicador.ConsultaMltiplicadorMRIk(CatMultiplicador, ref listaMultiplicador, Emp_CnxCentral);
                        cdpresupuesto.ConsultaVentaTotalTrimestral(Presupuesto, ref listaPresupuestototaltrimestral, Emp_CnxCentral);



                        HojaExcel.Range(HojaExcel.Cell(1, Primerrango), HojaExcel.Cell(1, segundoRango)).Merge();
                        HojaExcel.Range(HojaExcel.Cell(1, Primerrango), HojaExcel.Cell(1, segundoRango)).Value = "T" + fechaini + "- " + anio;
                        HojaExcel.Range(HojaExcel.Cell(1, Primerrango), HojaExcel.Cell(1, segundoRango)).Style.Font.Italic = true;
                        HojaExcel.Range(HojaExcel.Cell(1, Primerrango), HojaExcel.Cell(1, segundoRango)).Style.Fill.BackgroundColor = XLColor.Silver;
                        HojaExcel.Range(HojaExcel.Cell(1, Primerrango), HojaExcel.Cell(1, segundoRango)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        HojaExcel.Range(HojaExcel.Cell(1, Primerrango), HojaExcel.Cell(1, segundoRango)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


                        HojaExcel.Range(HojaExcel.Cell(2, 1), HojaExcel.Cell(2, 1)).Value = "Representante";
                        HojaExcel.Range(HojaExcel.Cell(2, 1), HojaExcel.Cell(2, 1)).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                        HojaExcel.Range(HojaExcel.Cell(2, 1), HojaExcel.Cell(2, 1)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                        HojaExcel.Cell(2, Primerrango).Value = "Prom.vta" + " T" + fechaini;
                        HojaExcel.Cell(2, Primerrango).Style.Fill.BackgroundColor = XLColor.Silver;
                        HojaExcel.Cell(2, Primerrango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                        HojaExcel.Cell(2, Primerrango + 1).Value = "Pptp" + " T" + fechaini;
                        HojaExcel.Cell(2, Primerrango + 1).Style.Fill.BackgroundColor = XLColor.Silver;
                        HojaExcel.Cell(2, Primerrango + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                        HojaExcel.Cell(2, Primerrango + 2).Value = "%Cumpl";
                        HojaExcel.Cell(2, Primerrango + 2).Style.Fill.BackgroundColor = XLColor.Silver;
                        HojaExcel.Cell(2, Primerrango + 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        HojaExcel.Cell(2, Primerrango + 3).Value = "%Multiplicador";
                        HojaExcel.Cell(2, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.Silver;
                        HojaExcel.Cell(2, Primerrango + 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;



                        if (BCMRepresentanteTrimestral.Value.ToString() != "-1")
                        {
                            int j = 0;
                            HojaExcel.Range(HojaExcel.Cell(3 + j, 1), HojaExcel.Cell(3 + j, 1));

                            HojaExcel.Range(HojaExcel.Cell(3 + j, 1), HojaExcel.Cell(3 + j, 1)).Value = BCMRepresentanteTrimestral.Text;
                            HojaExcel.Range(HojaExcel.Cell(3 + j, 1), HojaExcel.Cell(3 + j, 1)).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                            HojaExcel.Range(HojaExcel.Cell(3 + j, 1), HojaExcel.Cell(3 + j, 1)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;



                            List<CatPresupuesto> lista = (from tlist in listaPresupuesto
                                                          where tlist.Id_Rik == Convert.ToInt32(BCMRepresentanteTrimestral.Value.ToString())
                                                          select tlist).ToList();

                            List<CatPresupuesto> LsitaVenta = (from tlist in listaPresupuesto2
                                                               where tlist.id_ter == Convert.ToInt32(BCMRepresentanteTrimestral.Value.ToString())
                                                               select tlist).ToList();

                            List<CatMultiplicador> LMultiplicador = (from tlist in listaMultiplicador
                                                                     where tlist.Id_Rik == Convert.ToInt32(BCMRepresentanteTrimestral.Value.ToString())
                                                                     select tlist).ToList();



                            HojaExcel.Cell(3 + j, Primerrango).Value = LsitaVenta.Count > 0 ? (LsitaVenta[0].venta / 3).ToString() : "0";
                            HojaExcel.Cell(3 + j, Primerrango).Style.Fill.BackgroundColor = XLColor.White;
                            HojaExcel.Cell(3 + j, Primerrango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            HojaExcel.Cell(3 + j, Primerrango).Style.NumberFormat.Format = "$0.00";
                            HojaExcel.Cell(3 + j, Primerrango).DataType = XLDataType.Number;



                            HojaExcel.Cell(3 + j, Primerrango + 1).Value = lista.Count > 0 ? (lista[0].TotalPresupuesto / 3).ToString() : "0";
                            HojaExcel.Cell(3 + j, Primerrango + 1).Style.Fill.BackgroundColor = XLColor.White;
                            HojaExcel.Cell(3 + j, Primerrango + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            HojaExcel.Cell(3 + j, Primerrango + 1).Style.NumberFormat.Format = "$0.00";
                            HojaExcel.Cell(3 + j, Primerrango + 1).DataType = XLDataType.Number;


                            if (double.Parse(lista.Count > 0 ? lista[0].TotalPresupuesto.ToString() : "0") > 0)
                            {
                                double valor = ((double.Parse(lista.Count > 0 ? lista[0].TotalPresupuesto.ToString() : "0") / double.Parse(LsitaVenta.Count > 0 ? LsitaVenta[0].venta.ToString() : "0")));
                                HojaExcel.Cell(3 + j, Primerrango + 2).Value = valor;
                                HojaExcel.Cell(3 + j, Primerrango + 2).DataType = XLDataType.Number;
                                if (valor > 80)
                                {
                                    HojaExcel.Cell(3 + j, Primerrango + 2).Style.Fill.BackgroundColor = XLColor.LightGreen;
                                }
                                else if (valor > 60)
                                {
                                    HojaExcel.Cell(3 + j, Primerrango + 2).Style.Fill.BackgroundColor = XLColor.Yellow;
                                }
                                else
                                {
                                    HojaExcel.Cell(3 + j, Primerrango + 2).Style.Fill.BackgroundColor = XLColor.PaleVioletRed;
                                }
                            }
                            else
                            {
                                HojaExcel.Cell(3 + j, Primerrango + 2).Value = "0%";
                                HojaExcel.Cell(3 + j, Primerrango + 2).Style.Fill.BackgroundColor = XLColor.PaleVioletRed;
                            }
                            HojaExcel.Cell(3 + j, Primerrango + 2).Style.NumberFormat.Format = "##,###%";
                            HojaExcel.Cell(3 + j, Primerrango + 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                            if (LMultiplicador.Count > 0)
                            {
                                double totalMultiplicador = Convert.ToDouble(LMultiplicador.Sum(x => x.TotalMultiplicador).ToString()) / 3;
                                if (totalMultiplicador > .80)
                                {
                                    HojaExcel.Cell(3 + j, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.LightGreen;
                                }
                                else if (totalMultiplicador > .60)
                                {
                                    HojaExcel.Cell(3 + j, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.Yellow;
                                }
                                else
                                {
                                    HojaExcel.Cell(3 + j, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.PaleVioletRed;
                                }
                            }
                            else
                            {
                                HojaExcel.Cell(3 + j, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.PaleVioletRed;
                            }
                            double totalMultipli = LMultiplicador.Count > 0 ? Convert.ToDouble((LMultiplicador.Sum(x => x.TotalMultiplicador) / 3).ToString()) : 0;

                            HojaExcel.Cell(3 + j, Primerrango + 3).Value = totalMultipli / 100;
                            HojaExcel.Cell(3 + j, Primerrango + 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            HojaExcel.Cell(3 + j, Primerrango + 3).Style.NumberFormat.Format = "##,###%";
                            HojaExcel.Cell(3 + j, Primerrango + 3).DataType = XLDataType.Number;


                            Primerrango = segundoRango + 1;
                            segundoRango = Primerrango + 3;
                        }
                        else
                        {
                            for (var j = 0; j < BCMRepresentanteTrimestral.Items.Count(); j++)
                            {
                                if (BCMRepresentanteTrimestral.Items[j].Value != null)
                                {

                                    HojaExcel.Range(HojaExcel.Cell(3 + j, 1), HojaExcel.Cell(3 + j, 1)).Merge();
                                    HojaExcel.Range(HojaExcel.Cell(3 + j, 1), HojaExcel.Cell(3 + j, 1)).Value = BCMRepresentanteTrimestral.Items[j].Text;
                                    HojaExcel.Range(HojaExcel.Cell(3 + j, 1), HojaExcel.Cell(3 + j, 1)).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                                    HojaExcel.Range(HojaExcel.Cell(3 + j, 1), HojaExcel.Cell(3 + j, 1)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                                    List<CatPresupuesto> lista = new List<CatPresupuesto>();
                                    List<CatPresupuesto> LsitaVenta = new List<CatPresupuesto>();

                                    lista = (from tlist in listaPresupuesto
                                             where tlist.Id_Rik == Convert.ToInt32(BCMRepresentanteTrimestral.Items[j].Value.ToString())

                                             select tlist).ToList();

                                    LsitaVenta = (from tlist in listaPresupuesto2
                                                  where tlist.id_ter == Convert.ToInt32(BCMRepresentanteTrimestral.Items[j].Value.ToString())

                                                  select tlist).ToList();

                                    List<CatMultiplicador> LMultiplicador = (from tlist in listaMultiplicador
                                                                             where tlist.Id_Rik == Convert.ToInt32(BCMRepresentanteTrimestral.Items[j].Value.ToString())
                                                                             select tlist).ToList();



                                    HojaExcel.Cell(3 + j, Primerrango).Value = LsitaVenta.Count > 0 ? (LsitaVenta[0].venta / 3).ToString() : "0";
                                    HojaExcel.Cell(3 + j, Primerrango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                    HojaExcel.Cell(3 + j, Primerrango).Style.NumberFormat.Format = "$0.00";
                                    HojaExcel.Cell(3 + j, Primerrango).DataType = XLDataType.Number;



                                    HojaExcel.Cell(3 + j, Primerrango + 1).Value = lista.Count > 0 ? (lista[0].TotalPresupuesto / 3).ToString() : "0";
                                    HojaExcel.Cell(3 + j, Primerrango + 1).Style.Fill.BackgroundColor = XLColor.White;
                                    HojaExcel.Cell(3 + j, Primerrango + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                    HojaExcel.Cell(3 + j, Primerrango + 1).Style.NumberFormat.Format = "$0.00";
                                    HojaExcel.Cell(3 + j, Primerrango + 1).DataType = XLDataType.Number;


                                    if (double.Parse(LsitaVenta.Count > 0 ? LsitaVenta[0].utilidadBruta.ToString() : "0") > 0)
                                    {
                                        double listaVnta = ((double.Parse(lista.Count > 0 ? (lista[0].TotalPresupuesto / 3).ToString() : "0") / double.Parse(LsitaVenta.Count > 0 ? (LsitaVenta[0].venta / 3).ToString() : "0")));

                                        HojaExcel.Cell(3 + j, Primerrango + 2).Value = listaVnta;
                                        HojaExcel.Cell(3 + j, Primerrango + 2).Style.Fill.BackgroundColor = XLColor.LightGreen;
                                    }
                                    else
                                    {
                                        HojaExcel.Cell(3 + j, Primerrango + 2).Value = "0";
                                        HojaExcel.Cell(3 + j, Primerrango + 2).Style.Fill.BackgroundColor = XLColor.White;
                                    }
                                    HojaExcel.Cell(3 + j, Primerrango + 2).Style.NumberFormat.Format = "##,###%";
                                    HojaExcel.Cell(3 + j, Primerrango + 2).DataType = XLDataType.Number;

                                    if (double.Parse(LsitaVenta.Count > 0 ? LsitaVenta[0].utilidadBruta.ToString() : "0") > 0)
                                    {
                                        double valor = ((double.Parse(lista.Count > 0 ? (lista[0].TotalPresupuesto / 3).ToString() : "0") / double.Parse(LsitaVenta.Count > 0 ? (LsitaVenta[0].venta / 3).ToString() : "0")) * 100);


                                        if (valor > 80)
                                        {
                                            HojaExcel.Cell(3 + j, Primerrango + 2).Style.Fill.BackgroundColor = XLColor.LightGreen;
                                        }
                                        else if (valor > 60)
                                        {
                                            HojaExcel.Cell(3 + j, Primerrango + 2).Style.Fill.BackgroundColor = XLColor.Yellow;
                                        }
                                        else
                                        {
                                            HojaExcel.Cell(3 + j, Primerrango + 2).Style.Fill.BackgroundColor = XLColor.PaleVioletRed;
                                        }
                                    }
                                    else
                                    {
                                        HojaExcel.Cell(3 + j, Primerrango + 2).Value = "0%";
                                        HojaExcel.Cell(3 + j, Primerrango + 2).Style.Fill.BackgroundColor = XLColor.PaleVioletRed;
                                    }

                                    HojaExcel.Cell(3 + j, Primerrango + 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                                    if (LMultiplicador.Count > 0)
                                    {
                                        double totalMultiplicador = Convert.ToDouble(LMultiplicador.Sum(x => x.TotalMultiplicador).ToString()) / 3;
                                        if (totalMultiplicador > .80)
                                        {
                                            HojaExcel.Cell(3 + j, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.LightGreen;
                                        }
                                        else if (totalMultiplicador > .60)
                                        {
                                            HojaExcel.Cell(3 + j, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.Yellow;
                                        }
                                        else
                                        {
                                            HojaExcel.Cell(3 + j, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.PaleVioletRed;
                                        }
                                    }
                                    else
                                    {
                                        HojaExcel.Cell(3 + j, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.PaleVioletRed;
                                    }

                                    double totalMult = Convert.ToDouble(LMultiplicador.Count > 0 ? (LMultiplicador.Sum(x => x.TotalMultiplicador) / 3).ToString() : "0");
                                    HojaExcel.Cell(3 + j, Primerrango + 3).Value = totalMult / 100;
                                    HojaExcel.Cell(3 + j, Primerrango + 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                    HojaExcel.Cell(3 + j, Primerrango + 3).Style.NumberFormat.Format = "##,###%";
                                    HojaExcel.Cell(3 + j, Primerrango + 3).DataType = XLDataType.Number;


                                }
                            }
                            Primerrango = segundoRango + 1;
                            segundoRango = Primerrango + 3;
                        }

                        if (Convert.ToInt32(fechaini) < 4)
                        {
                            fechaini = (Convert.ToInt32(fechaini) + 1).ToString();
                        }
                        else
                        {
                            fechaini = "1";
                            anio = (Convert.ToInt32(anio) + 1).ToString();
                        }
                        i = Convert.ToDecimal(anio.ToString() + "." + fechaini.ToString());
                    }

                    if (BCMRepresentanteTrimestral.Value.ToString() != "-1")
                    {
                        Primerrango = 2;
                        segundoRango = 5;

                        trimini = Convert.ToDecimal(anioInicial.ToString() + "." + trimestreInicial.ToString());
                        trimfin = Convert.ToDecimal(anioFinal.ToString() + "." + trimesFinal.ToString());

                        for (var i = trimini; i <= trimfin;)
                        {
                            string fechaini = i.ToString().Split('.')[1];
                            string anio = i.ToString().Split('.')[0];
                            DateTime FechaInicial2 = DateTime.Now;
                            DateTime FechaFinal2 = DateTime.Now;

                            if (fechaini == "1")
                            {
                                FechaInicial2 = DateTime.Parse("01/01/" + anio);
                                FechaFinal2 = FechaInicial2.AddMonths(3).AddDays(-1);
                            }
                            if (fechaini == "2")
                            {
                                FechaInicial2 = DateTime.Parse("01/04/" + anio);
                                FechaFinal2 = FechaInicial2.AddMonths(3).AddDays(-1);
                            }
                            if (fechaini == "3")
                            {
                                FechaInicial2 = DateTime.Parse("01/07/" + anio);
                                FechaFinal2 = FechaInicial2.AddMonths(3).AddDays(-1);
                            }
                            if (fechaini == "4")
                            {
                                FechaInicial2 = DateTime.Parse("01/10/" + anio);
                                FechaFinal2 = FechaInicial2.AddMonths(3).AddDays(-1);
                            }
                            CN_Presupuesto cdpresupuesto = new CN_Presupuesto();
                            CatPresupuesto Presupuesto = new CatPresupuesto();

                            Presupuesto.Id_Emp = sesion.Id_Emp;
                            Presupuesto.Id_Cd = int.Parse(CMBTrimestreSucursal.Value.ToString());
                            Presupuesto.AnioInicial = Convert.ToInt32(anio);
                            Presupuesto.AnioFinal = Convert.ToInt32(anio);

                            List<CatPresupuesto> listaPresupuestototaltrimestral = new List<CatPresupuesto>();
                            cdpresupuesto.ConsultaVentaTotalTrimestral(Presupuesto, ref listaPresupuestototaltrimestral, Emp_CnxCentral);


                            List<CatPresupuesto> lista = (from tlist in listaPresupuestototaltrimestral
                                                          where tlist.Mes == FechaInicial2.Month
                                                          && tlist.Anio == FechaInicial2.Year
                                                          select tlist).ToList();



                            HojaExcel.Cell(4, 1).Value = "Total:";
                            HojaExcel.Cell(4, 1).Style.Fill.BackgroundColor = XLColor.Yellow;
                            HojaExcel.Cell(4, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                            HojaExcel.Cell(4, Primerrango).FormulaA1 = "=Sum(" + HojaExcel.Cell(3, Primerrango).Address + ":" + HojaExcel.Cell(3, Primerrango).Address + ")";
                            HojaExcel.Cell(4, Primerrango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            HojaExcel.Cell(4, Primerrango).Style.Fill.BackgroundColor = XLColor.Yellow;
                            HojaExcel.Cell(4, Primerrango).Style.NumberFormat.Format = "$0.00";
                            HojaExcel.Cell(4, Primerrango).DataType = XLDataType.Number;


                            HojaExcel.Cell(4, Primerrango + 1).FormulaA1 = "=Sum(" + HojaExcel.Cell(3, Primerrango + 1).Address + ":" + HojaExcel.Cell(3, Primerrango + 1).Address + ")";
                            HojaExcel.Cell(4, Primerrango + 1).Style.Fill.BackgroundColor = XLColor.Yellow;
                            HojaExcel.Cell(4, Primerrango + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            HojaExcel.Cell(4, Primerrango + 1).Style.NumberFormat.Format = "$0.00";
                            HojaExcel.Cell(4, Primerrango + 1).DataType = XLDataType.Number;


                            HojaExcel.Cell(4, Primerrango + 2).FormulaA1 = "=((" + HojaExcel.Cell(4, Primerrango + 1).Address + "/" + HojaExcel.Cell(4, Primerrango).Address + "))";
                            HojaExcel.Cell(4, Primerrango + 2).Style.Fill.BackgroundColor = XLColor.Yellow;
                            HojaExcel.Cell(4, Primerrango + 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            HojaExcel.Cell(4, Primerrango + 2).Style.NumberFormat.Format = "##,###%";

                            HojaExcel.Cell(4, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.Yellow;
                            HojaExcel.Cell(4, Primerrango + 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                            Primerrango = segundoRango + 1;
                            segundoRango = Primerrango + 3;


                            if (Convert.ToInt32(fechaini) < 4)
                            {
                                fechaini = (Convert.ToInt32(fechaini) + 1).ToString();
                            }
                            else
                            {
                                fechaini = "1";
                                anio = (Convert.ToInt32(anio) + 1).ToString();
                            }
                            i = Convert.ToDecimal(anio.ToString() + "." + fechaini.ToString());
                        }
                    }
                    else
                    {
                        Primerrango = 2;
                        segundoRango = 5;

                        trimini = Convert.ToDecimal(anioInicial.ToString() + "." + trimestreInicial.ToString());
                        trimfin = Convert.ToDecimal(anioFinal.ToString() + "." + trimesFinal.ToString());

                        for (var i = trimini; i <= trimfin;)
                        {
                            string fechaini = i.ToString().Split('.')[1];
                            string anio = i.ToString().Split('.')[0];
                            DateTime FechaInicial2 = DateTime.Now;
                            DateTime FechaFinal2 = DateTime.Now;

                            if (fechaini == "1")
                            {
                                FechaInicial2 = DateTime.Parse("01/01/" + anio);
                                FechaFinal2 = FechaInicial2.AddMonths(3).AddDays(-1);
                            }
                            if (fechaini == "2")
                            {
                                FechaInicial2 = DateTime.Parse("01/04/" + anio);
                                FechaFinal2 = FechaInicial2.AddMonths(3).AddDays(-1);
                            }
                            if (fechaini == "3")
                            {
                                FechaInicial2 = DateTime.Parse("01/07/" + anio);
                                FechaFinal2 = FechaInicial2.AddMonths(3).AddDays(-1);
                            }
                            if (fechaini == "4")
                            {
                                FechaInicial2 = DateTime.Parse("01/10/" + anio);
                                FechaFinal2 = FechaInicial2.AddMonths(3).AddDays(-1);
                            }

                            CN_Presupuesto cdpresupuesto = new CN_Presupuesto();
                            CatPresupuesto Presupuesto = new CatPresupuesto();

                            Presupuesto.Id_Emp = sesion.Id_Emp;
                            Presupuesto.Id_Cd = int.Parse(CMBTrimestreSucursal.Value.ToString());
                            Presupuesto.AnioInicial = Convert.ToInt32(anio);
                            Presupuesto.AnioFinal = Convert.ToInt32(anio);
                            Presupuesto.FechaInicial = FechaInicial2;
                            Presupuesto.fechafinal = FechaFinal2;

                            List<CatPresupuesto> listaPresupuestototaltrimestral = new List<CatPresupuesto>();
                            List<CatPresupuesto> listaRemisiontotaltrimestral = new List<CatPresupuesto>();
                            List<CatPresupuesto> listaPresupuesto = new List<CatPresupuesto>();


                            cdpresupuesto.ConsultaVentaTotalTrimestral(Presupuesto, ref listaPresupuestototaltrimestral, Emp_CnxCentral);
                            cdpresupuesto.ConsultRemisionTotalTrimestral(Presupuesto, ref listaRemisiontotaltrimestral, Emp_CnxCentral);
                            cdpresupuesto.ConsultaPresupuestoMesualRIk(Presupuesto, ref listaPresupuesto, Emp_CnxCentral);

                            List<CatPresupuesto> lista = (from tlist in listaPresupuestototaltrimestral
                                                          where tlist.Mes == FechaInicial2.Month
                                                          && tlist.Anio == FechaInicial2.Year
                                                          select tlist).ToList();

                            List<CatPresupuesto> listaremision = (from tlist in listaRemisiontotaltrimestral
                                                                  where tlist.Mes == FechaInicial2.Month
                                                                  && tlist.Anio == FechaInicial2.Year
                                                                  select tlist).ToList();


                            List<CatPresupuesto> lista2 = (from tlist in listaPresupuesto
                                                           where tlist.Id_Rik == 80
                                                           select tlist).ToList();



                            HojaExcel.Range(HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 1, 1), HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 1, 1)).Value = "Movimiento 80";
                            HojaExcel.Range(HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 1, 1), HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 1, 1)).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                            HojaExcel.Range(HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 1, 1), HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 1, 1)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 1, Primerrango).Value = listaremision.First().TotalPresupuesto;
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 1, Primerrango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 1, Primerrango).Style.Fill.BackgroundColor = XLColor.White;
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 1, Primerrango).Style.NumberFormat.Format = "$0.00";
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 1, Primerrango).DataType = XLDataType.Number;


                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 1, Primerrango + 1).Value = lista2.Count > 0 ? (lista2[0].TotalPresupuesto / 3).ToString() : "0";
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 1, Primerrango + 1).Style.Fill.BackgroundColor = XLColor.White;
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 1, Primerrango + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 1, Primerrango + 1).Style.NumberFormat.Format = "$0.00";
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 1, Primerrango + 1).DataType = XLDataType.Number;


                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 1, Primerrango + 2).FormulaA1 = "=((" + HojaExcel.Cell(4, Primerrango + 1).Address + "/" + HojaExcel.Cell(4, Primerrango).Address + "))";
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 1, Primerrango + 2).Style.Fill.BackgroundColor = XLColor.White;
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 1, Primerrango + 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, 1).Value = "Total:";
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, 1).Style.Fill.BackgroundColor = XLColor.Yellow;
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, Primerrango).FormulaA1 = "=Sum(" + HojaExcel.Cell(3 + 0, Primerrango).Address + ":" + HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 1, Primerrango).Address + ")";
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, Primerrango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, Primerrango).Style.Fill.BackgroundColor = XLColor.Yellow;
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, Primerrango).Style.NumberFormat.Format = "$0.00";
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, Primerrango).DataType = XLDataType.Number;


                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, Primerrango + 1).FormulaA1 = "=Sum(" + HojaExcel.Cell(3 + 0, Primerrango + 1).Address + ":" + HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count - 2, Primerrango + 1).Address + ")";
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, Primerrango + 1).Style.Fill.BackgroundColor = XLColor.Yellow;
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, Primerrango + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, Primerrango + 1).Style.NumberFormat.Format = "$0.00";
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, Primerrango + 1).DataType = XLDataType.Number;


                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, Primerrango + 2).FormulaA1 = "=((" + HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, Primerrango + 1).Address + "/" + HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, Primerrango).Address + "))";
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, Primerrango + 2).Style.Fill.BackgroundColor = XLColor.Yellow;
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, Primerrango + 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, Primerrango + 2).Style.NumberFormat.Format = "##,###%";
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.Yellow;
                            HojaExcel.Cell(3 + BCMRepresentanteTrimestral.Items.Count, Primerrango + 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                            Primerrango = segundoRango + 1;
                            segundoRango = Primerrango + 3;


                            if (Convert.ToInt32(fechaini) < 4)
                            {
                                fechaini = (Convert.ToInt32(fechaini) + 1).ToString();
                            }
                            else
                            {
                                fechaini = "1";
                                anio = (Convert.ToInt32(anio) + 1).ToString();
                            }
                            i = Convert.ToDecimal(anio.ToString() + "." + fechaini.ToString());
                        }
                    }
                }
                else
                {
                    mensaje("Se debe de seleccionar los trimestre a consultar");
                    return;
                }
                HojaExcel.Columns().AdjustToContents();

                string rutaguardado = Server.MapPath("~/Reportes/") + "ReporteComercialesTrimestral_" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";


                if (File.Exists(rutaguardado))
                {
                    File.Delete(rutaguardado);
                }

                workbook.SaveAs(rutaguardado);



                string Outgoingfile = Server.MapPath("~/Reportes/") + "ReporteComercialesTrimestral_" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";
                string ruta = Server.MapPath("~/Reportes/") + "ReporteComercialesTrimestral_" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";
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

        protected void BtnPdfDescaegar_ServerClick(object sender, EventArgs e)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_Presupuesto cdpresupuesto = new CN_Presupuesto();
            CatPresupuesto Presupuesto = new CatPresupuesto();
            List<CatPresupuesto> listaPresupuesto = new List<CatPresupuesto>();
            CN_Presupuesto presupuesto = new CN_Presupuesto();
            List<CatPresupuesto> listaPresupuesto2 = new List<CatPresupuesto>();
            List<CatPresupuesto> listaPresupuestototal = new List<CatPresupuesto>();
            List<CatPresupuesto> listaremisionTotal = new List<CatPresupuesto>();
            CN_Multiplicador CN_multiplicador = new CN_Multiplicador();
            CatMultiplicador CatMultiplicador = new CatMultiplicador();
            List<CatMultiplicador> listaMultiplicador = new List<CatMultiplicador>();

            string mesAnioInicial = txtFechaInicialDescargar.Text;
            string mesAniofinal = txtFechaFinalDescargar.Text;

            Presupuesto.Id_Emp = sesion.Id_Emp;
            Presupuesto.Id_Cd = int.Parse(cmbSucrusalDescargar.Value.ToString());

            if (int.Parse(cmbSucrusalDescargar.Value.ToString()) == -1)
            {
                mensaje("Favor de selecciona la sucrusal.");
                return;
            }

            if (mesAnioInicial != "" && mesAniofinal != "")
            {
                Presupuesto.MesInicial = Convert.ToInt32(mesAnioInicial.Split('/')[0]);
                Presupuesto.AnioInicial = Convert.ToInt32(mesAnioInicial.Split('/')[1]);
                Presupuesto.MesFinal = Convert.ToInt32(mesAniofinal.Split('/')[0]);
                Presupuesto.AnioFinal = Convert.ToInt32(mesAniofinal.Split('/')[1]);

                string FechaInicialD = ("01" + "/" + mesAnioInicial.Split('/')[0] + "/" + mesAnioInicial.Split('/')[1]);
                string FechaFinalD = ("01" + "/" + mesAniofinal.Split('/')[0] + "/" + mesAniofinal.Split('/')[1]);

                if (Convert.ToDateTime(FechaInicialD) > Convert.ToDateTime(FechaFinalD))
                {
                    mensaje("La fecha inicial es mayor a la fecha final de la sección de observar totales.");
                    return;
                }

                DateTime FechaInicial2 = DateTime.Parse(FechaInicialD);
                DateTime FechaFinal2 = DateTime.Parse(FechaFinalD).AddMonths(1).AddDays(-1);
                Presupuesto.FechaInicial = FechaInicial2;
                Presupuesto.fechafinal = FechaFinal2;
                Presupuesto.Id_Rik = Convert.ToInt32(cmbRepresentanteDescargar.Value.ToString());
                Presupuesto.Id_u = Convert.ToInt32(cmbRepresentanteDescargar.Value.ToString());

                CatMultiplicador.FechaInicial = FechaInicial2;
                CatMultiplicador.FechaFinal = FechaFinal2;
                CatMultiplicador.Id_Emp = Sesion.Id_Emp;
                CatMultiplicador.Id_Cd = int.Parse(cmbSucrusalDescargar.Value.ToString());

                cdpresupuesto.ConsultaUtilidadRIkxmes(Presupuesto, ref listaPresupuesto, Emp_CnxCentral);
                presupuesto.ConsultaPresupuestoMesualPvvRIk(Presupuesto, ref listaPresupuesto2, Emp_CnxCentral);
                presupuesto.ConsultaVentaTotal(Presupuesto, ref listaPresupuestototal, Emp_CnxCentral);
                presupuesto.ConsultaRemisionTotal(Presupuesto, ref listaremisionTotal, Emp_CnxCentral);
                CN_multiplicador.ConsultaMltiplicadorMRIk(CatMultiplicador, ref listaMultiplicador, Emp_CnxCentral);
            }


            using (var workbook = new XLWorkbook())
            {
                var HojaExcel = workbook.Worksheets.Add("Reporte");

                HojaExcel.Cell("A1").Value = cmbSucrusalDescargar.Text.ToString();
                HojaExcel.Cell("A1").Style.Font.Bold = true;
                HojaExcel.Cell("A1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                string FechaInicial = ("01" + "/" + mesAnioInicial.Split('/')[0] + "/" + mesAnioInicial.Split('/')[1]);
                string FechaFinal = ("01" + "/" + mesAniofinal.Split('/')[0] + "/" + mesAniofinal.Split('/')[1]);

                int Primerrango = 2;
                int segundoRango = 6;
                while (DateTime.Parse(FechaInicial) <= DateTime.Parse(FechaFinal))
                {
                    HojaExcel.Range(HojaExcel.Cell(1, Primerrango), HojaExcel.Cell(1, segundoRango)).Merge();
                    HojaExcel.Range(HojaExcel.Cell(1, Primerrango), HojaExcel.Cell(1, segundoRango)).Value = DateTime.Parse(FechaInicial).Month + "- " + DateTime.Parse(FechaInicial).Year;
                    HojaExcel.Range(HojaExcel.Cell(1, Primerrango), HojaExcel.Cell(1, segundoRango)).Style.Font.Italic = true;
                    HojaExcel.Range(HojaExcel.Cell(1, Primerrango), HojaExcel.Cell(1, segundoRango)).Style.Font.FontSize = 13;
                    HojaExcel.Range(HojaExcel.Cell(1, Primerrango), HojaExcel.Cell(1, segundoRango)).Style.Fill.BackgroundColor = XLColor.Silver;
                    HojaExcel.Range(HojaExcel.Cell(1, Primerrango), HojaExcel.Cell(1, segundoRango)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    HojaExcel.Range(HojaExcel.Cell(1, Primerrango), HojaExcel.Cell(1, segundoRango)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    HojaExcel.Range(HojaExcel.Cell(1, Primerrango), HojaExcel.Cell(1, segundoRango)).Style.DateFormat.Format = "MMMM yyyy";


                    HojaExcel.Cell(2, 1).Value = "Representante";
                    HojaExcel.Cell(2, 1).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                    HojaExcel.Cell(2, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                    HojaExcel.Cell(2, Primerrango).Value = "Venta";
                    HojaExcel.Cell(2, Primerrango).Style.Fill.BackgroundColor = XLColor.Silver;
                    HojaExcel.Cell(2, Primerrango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                    HojaExcel.Cell(2, Primerrango + 1).Value = "Presupuesto";
                    HojaExcel.Cell(2, Primerrango + 1).Style.Fill.BackgroundColor = XLColor.Silver;
                    HojaExcel.Cell(2, Primerrango + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    HojaExcel.Cell(2, Primerrango + 2).Value = "Utilidad Bruta Real";
                    HojaExcel.Cell(2, Primerrango + 2).Style.Fill.BackgroundColor = XLColor.Silver;
                    HojaExcel.Cell(2, Primerrango + 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    HojaExcel.Cell(2, Primerrango + 3).Value = "Cumplimiento";
                    HojaExcel.Cell(2, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.Silver;
                    HojaExcel.Cell(2, Primerrango + 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                    HojaExcel.Cell(2, Primerrango + 4).Value = "Multiplicador";
                    HojaExcel.Cell(2, Primerrango + 4).Style.Fill.BackgroundColor = XLColor.Silver;
                    HojaExcel.Cell(2, Primerrango + 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    FechaInicial = DateTime.Parse(FechaInicial).AddMonths(1).ToString();
                    Primerrango = segundoRango + 1;
                    segundoRango = Primerrango + 4;
                }

                if (cmbRepresentanteDescargar.Value.ToString() != "-1")
                {
                    int i = 0;

                    HojaExcel.Cell(3 + i, 1).Value = cmbRepresentanteDescargar.Text;
                    HojaExcel.Cell(3 + i, 1).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                    HojaExcel.Cell(3 + i, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    FechaInicial = ("01" + "/" + mesAnioInicial.Split('/')[0] + "/" + mesAnioInicial.Split('/')[1]);
                    FechaFinal = ("01" + "/" + mesAniofinal.Split('/')[0] + "/" + mesAniofinal.Split('/')[1]);
                    Primerrango = 2;
                    segundoRango = 6;

                    while (DateTime.Parse(FechaInicial) <= DateTime.Parse(FechaFinal))
                    {



                        List<CatPresupuesto> LsitaVenta = (from tlist in listaPresupuesto
                                                           where tlist.Id_Rik == Convert.ToInt32(cmbRepresentanteDescargar.Value.ToString())
                                                           && tlist.Mes == DateTime.Parse(FechaInicial).Month
                                                           && tlist.Anio == DateTime.Parse(FechaInicial).Year
                                                           select tlist).ToList();

                        List<CatPresupuesto> lista = (from tlist in listaPresupuesto2
                                                      where tlist.Id_Rik == Convert.ToInt32(cmbRepresentanteDescargar.Value.ToString())
                                                      && tlist.Mes == DateTime.Parse(FechaInicial).Month
                                                      && tlist.Anio == DateTime.Parse(FechaInicial).Year
                                                      select tlist).ToList();

                        List<CatMultiplicador> LMultiplicador = (from tlist in listaMultiplicador
                                                                 where tlist.Id_Rik == Convert.ToInt32(cmbRepresentanteDescargar.Value.ToString())
                                                       && tlist.Mes == DateTime.Parse(FechaInicial).Month
                                                  && tlist.Anio == DateTime.Parse(FechaInicial).Year
                                                                 select tlist).ToList();




                        HojaExcel.Cell(3 + i, Primerrango).Value = LsitaVenta.Count > 0 ? LsitaVenta[0].venta.ToString() : "0";
                        HojaExcel.Cell(3 + i, Primerrango).Style.Fill.BackgroundColor = XLColor.White;
                        HojaExcel.Cell(3 + i, Primerrango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        HojaExcel.Cell(3 + i, Primerrango).Style.NumberFormat.Format = "$0.00";
                        HojaExcel.Cell(3 + i, Primerrango).DataType = XLDataType.Number;


                        HojaExcel.Cell(3 + i, Primerrango + 1).Value = lista.Count > 0 ? lista[0].TotalPresupuesto.ToString() : "0";
                        HojaExcel.Cell(3 + i, Primerrango + 1).Style.Fill.BackgroundColor = XLColor.White;
                        HojaExcel.Cell(3 + i, Primerrango + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        HojaExcel.Cell(3 + i, Primerrango + 1).Style.NumberFormat.Format = "$0.00";
                        HojaExcel.Cell(3 + i, Primerrango + 1).DataType = XLDataType.Number;


                        if (LsitaVenta.Count > 0)
                        {
                            HojaExcel.Cell(3 + i, Primerrango + 2).Style.Fill.BackgroundColor = XLColor.White;
                            HojaExcel.Cell(3 + i, Primerrango + 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        }
                        else
                        {
                            HojaExcel.Cell(3 + i, Primerrango + 2).Style.Fill.BackgroundColor = XLColor.White;
                            HojaExcel.Cell(3 + i, Primerrango + 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        }

                        HojaExcel.Cell(3 + i, Primerrango + 2).Value = LsitaVenta.Count > 0 ? LsitaVenta[0].porcubreal.ToString() + "%" : "0%";
                        HojaExcel.Cell(3 + i, Primerrango + 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                        HojaExcel.Cell(3 + i, Primerrango + 2).Style.NumberFormat.Format = "###,##%";
                        HojaExcel.Cell(3 + i, Primerrango + 2).DataType = XLDataType.Number;

                        if (double.Parse(lista.Count > 0 ? lista[0].TotalPresupuesto.ToString() : "0") > 0)
                        {
                            double valor = ((double.Parse(LsitaVenta.Count > 0 ? LsitaVenta[0].venta.ToString() : "0") / double.Parse(lista.Count > 0 ? lista[0].TotalPresupuesto.ToString() : "0")));

                            if (valor > 0.80)
                            {
                                HojaExcel.Cell(3 + i, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.LightGreen;
                            }
                            else if (valor > 0.60)
                            {
                                HojaExcel.Cell(3 + i, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.Yellow;
                            }
                            else
                            {
                                HojaExcel.Cell(3 + i, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.PaleVioletRed;
                            }
                            HojaExcel.Cell(3 + i, Primerrango + 3).Value = valor.ToString();
                            HojaExcel.Cell(3 + i, Primerrango + 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                        }
                        else
                        {
                            HojaExcel.Cell(3 + i, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.PaleVioletRed;
                            HojaExcel.Cell(3 + i, Primerrango + 3).Value = "0%";
                        }
                        HojaExcel.Cell(3 + i, Primerrango + 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        HojaExcel.Cell(3 + i, Primerrango + 3).Style.NumberFormat.Format = "##,###%";
                        HojaExcel.Cell(3 + i, Primerrango + 3).DataType = XLDataType.Number;


                        if (LMultiplicador.Count > 0)
                        {
                            if (Convert.ToDouble(LMultiplicador[0].TotalMultiplicador) > .80)
                            {
                                HojaExcel.Cell(3 + i, Primerrango + 4).Style.Fill.BackgroundColor = XLColor.LightGreen;
                            }
                            else if (Convert.ToDouble(LMultiplicador[0].TotalMultiplicador) > .60)
                            {
                                HojaExcel.Cell(3 + i, Primerrango + 4).Style.Fill.BackgroundColor = XLColor.Yellow;
                            }
                            else
                            {
                                HojaExcel.Cell(3 + i, Primerrango + 4).Style.Fill.BackgroundColor = XLColor.PaleVioletRed;
                            }
                        }
                        else
                        {
                            HojaExcel.Cell(3 + i, Primerrango + 4).Style.Fill.BackgroundColor = XLColor.PaleVioletRed;
                        }
                        HojaExcel.Cell(3 + i, Primerrango + 4).Value = LMultiplicador.Count > 0 ? (LMultiplicador[0].TotalMultiplicador / 100).ToString() : "0";
                        HojaExcel.Cell(3 + i, Primerrango + 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        HojaExcel.Cell(3 + i, Primerrango + 4).Style.NumberFormat.Format = "##,###%";
                        HojaExcel.Cell(3 + i, Primerrango + 4).DataType = XLDataType.Number;

                        FechaInicial = DateTime.Parse(FechaInicial).AddMonths(1).ToString();
                        Primerrango = segundoRango + 1;
                        segundoRango = Primerrango + 4;
                    }
                }
                else
                {

                    for (var i = 0; i < cmbRepresentanteDescargar.Items.Count(); i++)
                    {


                        if (Convert.ToInt32(cmbRepresentanteDescargar.Items[i].Value.ToString()) != -1)
                        {
                            HojaExcel.Cell(3 + i, 1).Value = cmbRepresentanteDescargar.Items[i].Text;
                            HojaExcel.Cell(3 + i, 1).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                            HojaExcel.Cell(3 + i, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                            FechaInicial = ("01" + "/" + mesAnioInicial.Split('/')[0] + "/" + mesAnioInicial.Split('/')[1]);
                            FechaFinal = ("01" + "/" + mesAniofinal.Split('/')[0] + "/" + mesAniofinal.Split('/')[1]);
                            Primerrango = 2;
                            segundoRango = 6;

                            while (DateTime.Parse(FechaInicial) <= DateTime.Parse(FechaFinal))
                            {



                                List<CatPresupuesto> LsitaVenta = (from tlist in listaPresupuesto
                                                                   where tlist.Id_Rik == Convert.ToInt32(cmbRepresentanteDescargar.Items[i].Value.ToString())
                                                                   && tlist.Mes == DateTime.Parse(FechaInicial).Month
                                                                   && tlist.Anio == DateTime.Parse(FechaInicial).Year
                                                                   select tlist).ToList();

                                List<CatPresupuesto> lista = (from tlist in listaPresupuesto2
                                                              where tlist.Id_Rik == Convert.ToInt32(cmbRepresentanteDescargar.Items[i].Value.ToString())
                                                              && tlist.Mes == DateTime.Parse(FechaInicial).Month
                                                              && tlist.Anio == DateTime.Parse(FechaInicial).Year
                                                              select tlist).ToList();

                                List<CatMultiplicador> LMultiplicador = (from tlist in listaMultiplicador
                                                                         where tlist.Id_Rik == Convert.ToInt32(cmbRepresentanteDescargar.Items[i].Value.ToString())
                                                               && tlist.Mes == DateTime.Parse(FechaInicial).Month
                                                          && tlist.Anio == DateTime.Parse(FechaInicial).Year
                                                                         select tlist).ToList();


                                HojaExcel.Cell(3 + i, Primerrango).Value = LsitaVenta.Count > 0 ? LsitaVenta[0].venta.ToString() : "0";
                                HojaExcel.Cell(3 + i, Primerrango).Style.Fill.BackgroundColor = XLColor.White;
                                HojaExcel.Cell(3 + i, Primerrango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                HojaExcel.Cell(3 + i, Primerrango).Style.NumberFormat.Format = "$0.00";
                                HojaExcel.Cell(3 + i, Primerrango).DataType = XLDataType.Number;

                                HojaExcel.Cell(3 + i, Primerrango + 1).Value = lista.Count > 0 ? lista[0].TotalPresupuesto.ToString() : "0";
                                HojaExcel.Cell(3 + i, Primerrango + 1).Style.Fill.BackgroundColor = XLColor.White;
                                HojaExcel.Cell(3 + i, Primerrango + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                HojaExcel.Cell(3 + i, Primerrango + 1).Style.NumberFormat.Format = "$0.00";
                                HojaExcel.Cell(3 + i, Primerrango + 1).DataType = XLDataType.Number;



                                if (LsitaVenta.Count > 0)
                                {
                                    HojaExcel.Cell(3 + i, Primerrango + 2).Style.Fill.BackgroundColor = XLColor.White;
                                    HojaExcel.Cell(3 + i, Primerrango + 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                }
                                else
                                {
                                    HojaExcel.Cell(3 + i, Primerrango + 2).Style.Fill.BackgroundColor = XLColor.White;
                                    HojaExcel.Cell(3 + i, Primerrango + 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                }
                                HojaExcel.Cell(3 + i, Primerrango + 2).Value = LsitaVenta.Count > 0 ? (LsitaVenta[0].porcubreal).ToString() + "%" : "0%";
                                HojaExcel.Cell(3 + i, Primerrango + 2).DataType = XLDataType.Number;
                                HojaExcel.Cell(3 + i, Primerrango + 2).Style.NumberFormat.Format = "###,##%";


                                if (double.Parse(lista.Count > 0 ? lista[0].TotalPresupuesto.ToString() : "0") > 0)
                                {
                                    double valor = ((double.Parse(LsitaVenta.Count > 0 ? LsitaVenta[0].venta.ToString() : "0") / double.Parse(lista.Count > 0 ? lista[0].TotalPresupuesto.ToString() : "0")));

                                    if (valor >= 0.80)
                                    {
                                        HojaExcel.Cell(3 + i, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.LightGreen;
                                    }
                                    else if (valor >= 0.60)
                                    {
                                        HojaExcel.Cell(3 + i, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.Yellow;
                                    }
                                    else
                                    {
                                        HojaExcel.Cell(3 + i, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.PaleVioletRed;
                                    }
                                    HojaExcel.Cell(3 + i, Primerrango + 3).Value = valor.ToString();
                                    HojaExcel.Cell(3 + i, Primerrango + 3).Style.NumberFormat.Format = "###,##%";
                                }
                                else
                                {
                                    HojaExcel.Cell(3 + i, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.PaleVioletRed;
                                    HojaExcel.Cell(3 + i, Primerrango + 3).Value = "0";
                                    HojaExcel.Cell(3 + i, Primerrango + 3).Style.NumberFormat.Format = "###,##%";
                                }
                                HojaExcel.Cell(3 + i, Primerrango + 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                HojaExcel.Cell(3 + i, Primerrango + 3).DataType = XLDataType.Number;

                                HojaExcel.Cell(3 + i, Primerrango + 4).Value = "Multiplicador";
                                if (LMultiplicador.Count > 0)
                                {
                                    if (Convert.ToDouble(LMultiplicador[0].TotalMultiplicador) > .80)
                                    {
                                        HojaExcel.Cell(3 + i, Primerrango + 4).Style.Fill.BackgroundColor = XLColor.LightGreen;
                                    }
                                    else if (Convert.ToDouble(LMultiplicador[0].TotalMultiplicador) > .60)
                                    {
                                        HojaExcel.Cell(3 + i, Primerrango + 4).Style.Fill.BackgroundColor = XLColor.Yellow;
                                    }
                                    else
                                    {
                                        HojaExcel.Cell(3 + i, Primerrango + 4).Style.Fill.BackgroundColor = XLColor.PaleVioletRed;
                                    }
                                }
                                else
                                {
                                    HojaExcel.Cell(3 + i, Primerrango + 4).Style.Fill.BackgroundColor = XLColor.PaleVioletRed;
                                }
                                HojaExcel.Cell(3 + i, Primerrango + 4).Value = LMultiplicador.Count > 0 ? (LMultiplicador[0].TotalMultiplicador / 100).ToString() : "0";
                                HojaExcel.Cell(3 + i, Primerrango + 4).Style.NumberFormat.Format = "###,##%";
                                HojaExcel.Cell(3 + i, Primerrango + 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                HojaExcel.Cell(3 + i, Primerrango + 4).DataType = XLDataType.Number;

                                FechaInicial = DateTime.Parse(FechaInicial).AddMonths(1).ToString();
                                Primerrango = segundoRango + 1;
                                segundoRango = Primerrango + 4;
                            }
                        }
                    }


                    HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, 1).Value = "Movimiento 80";
                    HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, 1).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                    HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    FechaInicial = ("01" + "/" + mesAnioInicial.Split('/')[0] + "/" + mesAnioInicial.Split('/')[1]);
                    FechaFinal = ("01" + "/" + mesAniofinal.Split('/')[0] + "/" + mesAniofinal.Split('/')[1]);
                    Primerrango = 2;
                    segundoRango = 6;

                    while (DateTime.Parse(FechaInicial) <= DateTime.Parse(FechaFinal))
                    {

                        List<CatPresupuesto> lista = (from tlist in listaPresupuestototal
                                                      where tlist.Mes == DateTime.Parse(FechaInicial).Month
                                                      && tlist.Anio == DateTime.Parse(FechaInicial).Year
                                                      select tlist).ToList();

                        List<CatPresupuesto> listaRemision = (from tlist in listaremisionTotal
                                                              where tlist.Mes == DateTime.Parse(FechaInicial).Month
                                                              && tlist.Anio == DateTime.Parse(FechaInicial).Year
                                                              select tlist).ToList();

                        List<CatPresupuesto> lista2 = (from tlist in listaPresupuesto2
                                                       where tlist.Id_Rik == 80
                                                       && tlist.Mes == DateTime.Parse(FechaInicial).Month
                                                       && tlist.Anio == DateTime.Parse(FechaInicial).Year
                                                       select tlist).ToList();



                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango).Value = (listaRemision.Count == 0 ? 0 : listaRemision.First().TotalPresupuesto);
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango).Style.Fill.BackgroundColor = XLColor.White;
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango).Style.NumberFormat.Format = "$0.00";
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango).DataType = XLDataType.Number;

                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango + 1).Value = lista2.Count > 0 ? lista2[0].TotalPresupuesto.ToString() : "0";
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango + 1).Style.Fill.BackgroundColor = XLColor.White;
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango + 1).Style.NumberFormat.Format = "$0.00";
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango + 1).DataType = XLDataType.Number;

                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango + 2).Style.Fill.BackgroundColor = XLColor.White;
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango + 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango + 2).Style.NumberFormat.Format = "###,##%";



                        if (double.Parse(lista2.Count > 0 ? lista2[0].TotalPresupuesto.ToString() : "0") > 0)
                        {
                            double valor = ((double.Parse(listaRemision.Count == 0 ? "0" : listaRemision.First().TotalPresupuesto.ToString()) / double.Parse(lista2.Count > 0 ? lista2[0].TotalPresupuesto.ToString() : "0")));

                            if (valor >= 0.80)
                            {
                                HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.LightGreen;
                            }
                            else if (valor >= 0.60)
                            {
                                HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.Yellow;
                            }
                            else
                            {
                                HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.PaleVioletRed;
                            }
                            HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango + 3).Value = valor.ToString();
                            HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango + 3).Style.NumberFormat.Format = "###,##%";
                        }
                        else
                        {
                            HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.PaleVioletRed;
                            HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango + 3).Value = "0";
                            HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango + 3).Style.NumberFormat.Format = "###,##%";
                        }
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango + 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango + 3).DataType = XLDataType.Number;


                        FechaInicial = DateTime.Parse(FechaInicial).AddMonths(1).ToString();
                        Primerrango = segundoRango + 1;
                        segundoRango = Primerrango + 4;
                    }



                    HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), 1).Value = "total:";
                    HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), 1).Style.Fill.BackgroundColor = XLColor.Yellow;
                    HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    FechaInicial = ("01" + "/" + mesAnioInicial.Split('/')[0] + "/" + mesAnioInicial.Split('/')[1]);
                    FechaFinal = ("01" + "/" + mesAniofinal.Split('/')[0] + "/" + mesAniofinal.Split('/')[1]);
                    Primerrango = 2;
                    segundoRango = 6;

                    while (DateTime.Parse(FechaInicial) <= DateTime.Parse(FechaFinal))
                    {
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), Primerrango).FormulaA1 = "=Sum(" + HojaExcel.Cell(3 + 0, Primerrango).Address + ":" + HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango).Address + ")";
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), Primerrango).Style.Fill.BackgroundColor = XLColor.Yellow;
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), Primerrango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), Primerrango).Style.NumberFormat.Format = "$0.00";
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), Primerrango).DataType = XLDataType.Number;

                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), Primerrango + 1).FormulaA1 = "=Sum(" + HojaExcel.Cell(3 + 0, Primerrango + 1).Address + ":" + HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango + 1).Address + ")";
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), Primerrango + 1).Style.Fill.BackgroundColor = XLColor.Yellow;
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), Primerrango + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), Primerrango + 1).Style.NumberFormat.Format = "$0.00";
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), Primerrango + 1).DataType = XLDataType.Number;

                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), Primerrango + 2).Style.Fill.BackgroundColor = XLColor.Yellow;
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), Primerrango + 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), Primerrango + 3).FormulaA1 = "=((" + HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), Primerrango).Address + "/" + HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), Primerrango + 1).Address + "))";
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), Primerrango + 3).Style.Fill.BackgroundColor = XLColor.Yellow;
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), Primerrango + 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), Primerrango + 3).Style.NumberFormat.Format = "###,##%";

                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), Primerrango + 4).Style.Fill.BackgroundColor = XLColor.Yellow;
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), Primerrango + 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                        FechaInicial = DateTime.Parse(FechaInicial).AddMonths(1).ToString();
                        Primerrango = segundoRango + 1;
                        segundoRango = Primerrango + 4;
                    }
                }

                HojaExcel.Columns().AdjustToContents();
                string rutaguardado = Server.MapPath("~/Reportes/") + "ReporteComerciales_" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";


                if (File.Exists(rutaguardado))
                {
                    File.Delete(rutaguardado);
                }

                workbook.SaveAs(rutaguardado);

            }
            string ruta = Server.MapPath("~/Reportes/") + "ReporteComerciales_" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";
            string ruta2 = Server.MapPath("~/Reportes/") + "ReporteComerciales_" + DateTime.Now.ToString("ddMMyyyy") + ".PDF";

            Workbook workbook2 = new Workbook();
            workbook2.LoadFromFile(ruta);
            workbook2.ConverterSetting.SheetFitToPage = true;
            workbook2.SaveToFile(ruta2, FileFormat.PDF);


            System.IO.FileInfo file = new System.IO.FileInfo(ruta2);


            string Outgoingfile = "ReporteComerciales_" + DateTime.Now.ToString("ddMMyyyy") + ".pdf";
            if (file.Exists)
            {
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Outgoingfile);
                Response.WriteFile(file.FullName);

            }
            else
            {
                Response.Write("This file does not exist.");
            }
        }

        protected void Button5_ServerClick(object sender, EventArgs e)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_Presupuesto cdpresupuesto = new CN_Presupuesto();
            CatPresupuesto Presupuesto = new CatPresupuesto();
            List<CatPresupuesto> listaPresupuesto = new List<CatPresupuesto>();
            CN_Presupuesto presupuesto = new CN_Presupuesto();
            List<CatPresupuesto> listaPresupuesto2 = new List<CatPresupuesto>();
            List<CatPresupuesto> listaPresupuestototal = new List<CatPresupuesto>();
            List<CatPresupuesto> listaremisionTotal = new List<CatPresupuesto>();
            CN_Multiplicador CN_multiplicador = new CN_Multiplicador();
            CatMultiplicador CatMultiplicador = new CatMultiplicador();
            List<CatMultiplicador> listaMultiplicador = new List<CatMultiplicador>();

            string mesAnioInicial = txtFechaInicialDescargar.Text;
            string mesAniofinal = txtFechaFinalDescargar.Text;

            Presupuesto.Id_Emp = sesion.Id_Emp;
            Presupuesto.Id_Cd = int.Parse(cmbSucrusalDescargar.Value.ToString());
            if (int.Parse(cmbSucrusalDescargar.Value.ToString()) == -1)
            {
                mensaje("Favor de selecciona la sucrusal.");
                return;
            }
            if (mesAnioInicial != "" && mesAniofinal != "")
            {

                Presupuesto.MesInicial = Convert.ToInt32(mesAnioInicial.Split('/')[0]);
                Presupuesto.AnioInicial = Convert.ToInt32(mesAnioInicial.Split('/')[1]);
                Presupuesto.MesFinal = Convert.ToInt32(mesAniofinal.Split('/')[0]);
                Presupuesto.AnioFinal = Convert.ToInt32(mesAniofinal.Split('/')[1]);

                string FechaInicialD = ("01" + "/" + mesAnioInicial.Split('/')[0] + "/" + mesAnioInicial.Split('/')[1]);
                string FechaFinalD = ("01" + "/" + mesAniofinal.Split('/')[0] + "/" + mesAniofinal.Split('/')[1]);

                if (Convert.ToDateTime(FechaInicialD) > Convert.ToDateTime(FechaFinalD))
                {
                    mensaje("La fecha inicial es mayor a la fecha final de la sección de observar totales.");
                    return;
                }

                DateTime FechaInicial2 = DateTime.Parse(FechaInicialD);
                DateTime FechaFinal2 = DateTime.Parse(FechaFinalD).AddMonths(1).AddDays(-1);
                Presupuesto.FechaInicial = FechaInicial2;
                Presupuesto.fechafinal = FechaFinal2;
                Presupuesto.Id_Rik = Convert.ToInt32(cmbRepresentanteDescargar.Value.ToString());
                Presupuesto.Id_u = Convert.ToInt32(cmbRepresentanteDescargar.Value.ToString());
                Presupuesto.AnioInicial = FechaInicial2.Year;
                Presupuesto.Aniofinal = FechaFinal2.Year;

                CatMultiplicador.FechaInicial = FechaInicial2;
                CatMultiplicador.FechaFinal = FechaFinal2;
                CatMultiplicador.Id_Emp = Sesion.Id_Emp;
                CatMultiplicador.Id_Cd = int.Parse(cmbSucrusalDescargar.Value.ToString());

                cdpresupuesto.ConsultaUtilidadRIkxmes(Presupuesto, ref listaPresupuesto, Emp_CnxCentral);
                presupuesto.ConsultaPresupuestoMesualPvvRIk(Presupuesto, ref listaPresupuesto2, Emp_CnxCentral);
                presupuesto.ConsultaVentaTotal(Presupuesto, ref listaPresupuestototal, Emp_CnxCentral);
                presupuesto.ConsultaRemisionTotal(Presupuesto, ref listaremisionTotal, Emp_CnxCentral);
                CN_multiplicador.ConsultaMltiplicadorMRIk(CatMultiplicador, ref listaMultiplicador, Emp_CnxCentral);
            }


            using (var workbook = new XLWorkbook())
            {
                var HojaExcel = workbook.Worksheets.Add("Reporte");

                HojaExcel.Cell("A1").Value = cmbSucrusalDescargar.Text.ToString();
                HojaExcel.Cell("A1").Style.Font.Bold = true;
                HojaExcel.Cell("A1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                string FechaInicial = ("01" + "/" + mesAnioInicial.Split('/')[0] + "/" + mesAnioInicial.Split('/')[1]);
                string FechaFinal = ("01" + "/" + mesAniofinal.Split('/')[0] + "/" + mesAniofinal.Split('/')[1]);

                int Primerrango = 2;
                int segundoRango = 6;
                while (DateTime.Parse(FechaInicial) <= DateTime.Parse(FechaFinal))
                {
                    HojaExcel.Range(HojaExcel.Cell(1, Primerrango), HojaExcel.Cell(1, segundoRango)).Merge();
                    HojaExcel.Range(HojaExcel.Cell(1, Primerrango), HojaExcel.Cell(1, segundoRango)).Value = DateTime.Parse(FechaInicial).Month + "- " + DateTime.Parse(FechaInicial).Year;
                    HojaExcel.Range(HojaExcel.Cell(1, Primerrango), HojaExcel.Cell(1, segundoRango)).Style.Font.Italic = true;
                    HojaExcel.Range(HojaExcel.Cell(1, Primerrango), HojaExcel.Cell(1, segundoRango)).Style.Font.FontSize = 13;
                    HojaExcel.Range(HojaExcel.Cell(1, Primerrango), HojaExcel.Cell(1, segundoRango)).Style.Fill.BackgroundColor = XLColor.Silver;
                    HojaExcel.Range(HojaExcel.Cell(1, Primerrango), HojaExcel.Cell(1, segundoRango)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    HojaExcel.Range(HojaExcel.Cell(1, Primerrango), HojaExcel.Cell(1, segundoRango)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    HojaExcel.Range(HojaExcel.Cell(1, Primerrango), HojaExcel.Cell(1, segundoRango)).Style.DateFormat.Format = "MMMM yyyy";


                    HojaExcel.Cell(2, 1).Value = "Representante";
                    HojaExcel.Cell(2, 1).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                    HojaExcel.Cell(2, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                    HojaExcel.Cell(2, Primerrango).Value = "Venta";
                    HojaExcel.Cell(2, Primerrango).Style.Fill.BackgroundColor = XLColor.Silver;
                    HojaExcel.Cell(2, Primerrango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                    HojaExcel.Cell(2, Primerrango + 1).Value = "Presupuesto";
                    HojaExcel.Cell(2, Primerrango + 1).Style.Fill.BackgroundColor = XLColor.Silver;
                    HojaExcel.Cell(2, Primerrango + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    HojaExcel.Cell(2, Primerrango + 2).Value = "Utilidad Bruta Real";
                    HojaExcel.Cell(2, Primerrango + 2).Style.Fill.BackgroundColor = XLColor.Silver;
                    HojaExcel.Cell(2, Primerrango + 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    HojaExcel.Cell(2, Primerrango + 3).Value = "Cumplimiento";
                    HojaExcel.Cell(2, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.Silver;
                    HojaExcel.Cell(2, Primerrango + 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                    HojaExcel.Cell(2, Primerrango + 4).Value = "Multiplicador";
                    HojaExcel.Cell(2, Primerrango + 4).Style.Fill.BackgroundColor = XLColor.Silver;
                    HojaExcel.Cell(2, Primerrango + 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    FechaInicial = DateTime.Parse(FechaInicial).AddMonths(1).ToString();
                    Primerrango = segundoRango + 1;
                    segundoRango = Primerrango + 4;
                }

                if (cmbRepresentanteDescargar.Value.ToString() != "-1")
                {
                    int i = 0;

                    HojaExcel.Cell(3 + i, 1).Value = cmbRepresentanteDescargar.Text;
                    HojaExcel.Cell(3 + i, 1).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                    HojaExcel.Cell(3 + i, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    FechaInicial = ("01" + "/" + mesAnioInicial.Split('/')[0] + "/" + mesAnioInicial.Split('/')[1]);
                    FechaFinal = ("01" + "/" + mesAniofinal.Split('/')[0] + "/" + mesAniofinal.Split('/')[1]);
                    Primerrango = 2;
                    segundoRango = 6;

                    while (DateTime.Parse(FechaInicial) <= DateTime.Parse(FechaFinal))
                    {



                        List<CatPresupuesto> LsitaVenta = (from tlist in listaPresupuesto
                                                           where tlist.Id_Rik == Convert.ToInt32(cmbRepresentanteDescargar.Value.ToString())
                                                           && tlist.Mes == DateTime.Parse(FechaInicial).Month
                                                           && tlist.Anio == DateTime.Parse(FechaInicial).Year
                                                           select tlist).ToList();

                        List<CatPresupuesto> lista = (from tlist in listaPresupuesto2
                                                      where tlist.Id_Rik == Convert.ToInt32(cmbRepresentanteDescargar.Value.ToString())
                                                      && tlist.Mes == DateTime.Parse(FechaInicial).Month
                                                      && tlist.Anio == DateTime.Parse(FechaInicial).Year
                                                      select tlist).ToList();

                        List<CatMultiplicador> LMultiplicador = (from tlist in listaMultiplicador
                                                                 where tlist.Id_Rik == Convert.ToInt32(cmbRepresentanteDescargar.Value.ToString())
                                                       && tlist.Mes == DateTime.Parse(FechaInicial).Month
                                                  && tlist.Anio == DateTime.Parse(FechaInicial).Year
                                                                 select tlist).ToList();




                        HojaExcel.Cell(3 + i, Primerrango).Value = LsitaVenta.Count > 0 ? LsitaVenta[0].venta.ToString() : "0";
                        HojaExcel.Cell(3 + i, Primerrango).Style.Fill.BackgroundColor = XLColor.White;
                        HojaExcel.Cell(3 + i, Primerrango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        HojaExcel.Cell(3 + i, Primerrango).Style.NumberFormat.Format = "$0.00";
                        HojaExcel.Cell(3 + i, Primerrango).DataType = XLDataType.Number;


                        HojaExcel.Cell(3 + i, Primerrango + 1).Value = lista.Count > 0 ? lista[0].TotalPresupuesto.ToString() : "0";
                        HojaExcel.Cell(3 + i, Primerrango + 1).Style.Fill.BackgroundColor = XLColor.White;
                        HojaExcel.Cell(3 + i, Primerrango + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        HojaExcel.Cell(3 + i, Primerrango + 1).Style.NumberFormat.Format = "$0.00";
                        HojaExcel.Cell(3 + i, Primerrango + 1).DataType = XLDataType.Number;


                        if (LsitaVenta.Count > 0)
                        {
                            HojaExcel.Cell(3 + i, Primerrango + 2).Style.Fill.BackgroundColor = XLColor.White;
                            HojaExcel.Cell(3 + i, Primerrango + 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        }
                        else
                        {
                            HojaExcel.Cell(3 + i, Primerrango + 2).Style.Fill.BackgroundColor = XLColor.White;
                            HojaExcel.Cell(3 + i, Primerrango + 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        }

                        HojaExcel.Cell(3 + i, Primerrango + 2).Value = LsitaVenta.Count > 0 ? LsitaVenta[0].porcubreal.ToString() + "%" : "0%";
                        HojaExcel.Cell(3 + i, Primerrango + 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                        HojaExcel.Cell(3 + i, Primerrango + 2).Style.NumberFormat.Format = "###,##%";
                        HojaExcel.Cell(3 + i, Primerrango + 2).DataType = XLDataType.Number;

                        if (double.Parse(lista.Count > 0 ? lista[0].TotalPresupuesto.ToString() : "0") > 0)
                        {
                            double valor = ((double.Parse(LsitaVenta.Count > 0 ? LsitaVenta[0].venta.ToString() : "0") / double.Parse(lista.Count > 0 ? lista[0].TotalPresupuesto.ToString() : "0")));

                            if (valor > 0.80)
                            {
                                HojaExcel.Cell(3 + i, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.LightGreen;
                            }
                            else if (valor > 0.60)
                            {
                                HojaExcel.Cell(3 + i, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.Yellow;
                            }
                            else
                            {
                                HojaExcel.Cell(3 + i, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.PaleVioletRed;
                            }
                            HojaExcel.Cell(3 + i, Primerrango + 3).Value = valor.ToString();
                            HojaExcel.Cell(3 + i, Primerrango + 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                        }
                        else
                        {
                            HojaExcel.Cell(3 + i, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.PaleVioletRed;
                            HojaExcel.Cell(3 + i, Primerrango + 3).Value = "0%";
                        }
                        HojaExcel.Cell(3 + i, Primerrango + 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        HojaExcel.Cell(3 + i, Primerrango + 3).Style.NumberFormat.Format = "##,###%";
                        HojaExcel.Cell(3 + i, Primerrango + 3).DataType = XLDataType.Number;

                        HojaExcel.Cell(3 + i, Primerrango + 4).Value = "Multiplicador";
                        if (LMultiplicador.Count > 0)
                        {
                            if (Convert.ToDouble(LMultiplicador[0].TotalMultiplicador) > .80)
                            {
                                HojaExcel.Cell(3 + i, Primerrango + 4).Style.Fill.BackgroundColor = XLColor.LightGreen;
                            }
                            else if (Convert.ToDouble(LMultiplicador[0].TotalMultiplicador) > .60)
                            {
                                HojaExcel.Cell(3 + i, Primerrango + 4).Style.Fill.BackgroundColor = XLColor.Yellow;
                            }
                            else
                            {
                                HojaExcel.Cell(3 + i, Primerrango + 4).Style.Fill.BackgroundColor = XLColor.PaleVioletRed;
                            }
                        }
                        else
                        {
                            HojaExcel.Cell(3 + i, Primerrango + 4).Style.Fill.BackgroundColor = XLColor.PaleVioletRed;
                        }
                        HojaExcel.Cell(3 + i, Primerrango + 4).Value = LMultiplicador.Count > 0 ? (LMultiplicador[0].TotalMultiplicador / 100).ToString() : "0";
                        HojaExcel.Cell(3 + i, Primerrango + 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        HojaExcel.Cell(3 + i, Primerrango + 4).Style.NumberFormat.Format = "##,###%";
                        HojaExcel.Cell(3 + i, Primerrango + 4).DataType = XLDataType.Number;

                        FechaInicial = DateTime.Parse(FechaInicial).AddMonths(1).ToString();
                        Primerrango = segundoRango + 1;
                        segundoRango = Primerrango + 4;
                    }
                }
                else
                {

                    for (var i = 0; i < cmbRepresentanteDescargar.Items.Count(); i++)
                    {


                        if (Convert.ToInt32(cmbRepresentanteDescargar.Items[i].Value.ToString()) != -1)
                        {
                            HojaExcel.Cell(3 + i, 1).Value = cmbRepresentanteDescargar.Items[i].Text;
                            HojaExcel.Cell(3 + i, 1).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                            HojaExcel.Cell(3 + i, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                            FechaInicial = ("01" + "/" + mesAnioInicial.Split('/')[0] + "/" + mesAnioInicial.Split('/')[1]);
                            FechaFinal = ("01" + "/" + mesAniofinal.Split('/')[0] + "/" + mesAniofinal.Split('/')[1]);
                            Primerrango = 2;
                            segundoRango = 6;

                            while (DateTime.Parse(FechaInicial) <= DateTime.Parse(FechaFinal))
                            {



                                List<CatPresupuesto> LsitaVenta = (from tlist in listaPresupuesto
                                                                   where tlist.Id_Rik == Convert.ToInt32(cmbRepresentanteDescargar.Items[i].Value.ToString())
                                                                   && tlist.Mes == DateTime.Parse(FechaInicial).Month
                                                                   && tlist.Anio == DateTime.Parse(FechaInicial).Year
                                                                   select tlist).ToList();

                                List<CatPresupuesto> lista = (from tlist in listaPresupuesto2
                                                              where tlist.Id_Rik == Convert.ToInt32(cmbRepresentanteDescargar.Items[i].Value.ToString())
                                                              && tlist.Mes == DateTime.Parse(FechaInicial).Month
                                                              && tlist.Anio == DateTime.Parse(FechaInicial).Year
                                                              select tlist).ToList();

                                List<CatMultiplicador> LMultiplicador = (from tlist in listaMultiplicador
                                                                         where tlist.Id_Rik == Convert.ToInt32(cmbRepresentanteDescargar.Items[i].Value.ToString())
                                                               && tlist.Mes == DateTime.Parse(FechaInicial).Month
                                                          && tlist.Anio == DateTime.Parse(FechaInicial).Year
                                                                         select tlist).ToList();


                                HojaExcel.Cell(3 + i, Primerrango).Value = LsitaVenta.Count > 0 ? LsitaVenta[0].venta.ToString() : "0";
                                HojaExcel.Cell(3 + i, Primerrango).Style.Fill.BackgroundColor = XLColor.White;
                                HojaExcel.Cell(3 + i, Primerrango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                HojaExcel.Cell(3 + i, Primerrango).Style.NumberFormat.Format = "$0.00";
                                HojaExcel.Cell(3 + i, Primerrango).DataType = XLDataType.Number;

                                HojaExcel.Cell(3 + i, Primerrango + 1).Value = lista.Count > 0 ? lista[0].TotalPresupuesto.ToString() : "0";
                                HojaExcel.Cell(3 + i, Primerrango + 1).Style.Fill.BackgroundColor = XLColor.White;
                                HojaExcel.Cell(3 + i, Primerrango + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                HojaExcel.Cell(3 + i, Primerrango + 1).Style.NumberFormat.Format = "$0.00";
                                HojaExcel.Cell(3 + i, Primerrango + 1).DataType = XLDataType.Number;



                                if (LsitaVenta.Count > 0)
                                {
                                    HojaExcel.Cell(3 + i, Primerrango + 2).Style.Fill.BackgroundColor = XLColor.White;
                                    HojaExcel.Cell(3 + i, Primerrango + 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                }
                                else
                                {
                                    HojaExcel.Cell(3 + i, Primerrango + 2).Style.Fill.BackgroundColor = XLColor.White;
                                    HojaExcel.Cell(3 + i, Primerrango + 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                }
                                HojaExcel.Cell(3 + i, Primerrango + 2).Value = LsitaVenta.Count > 0 ? (LsitaVenta[0].porcubreal).ToString() + "%" : "0%";
                                HojaExcel.Cell(3 + i, Primerrango + 2).DataType = XLDataType.Number;
                                HojaExcel.Cell(3 + i, Primerrango + 2).Style.NumberFormat.Format = "###,##%";


                                if (double.Parse(lista.Count > 0 ? lista[0].TotalPresupuesto.ToString() : "0") > 0)
                                {
                                    double valor = ((double.Parse(LsitaVenta.Count > 0 ? LsitaVenta[0].venta.ToString() : "0") / double.Parse(lista.Count > 0 ? lista[0].TotalPresupuesto.ToString() : "0")));

                                    if (valor >= 0.80)
                                    {
                                        HojaExcel.Cell(3 + i, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.LightGreen;
                                    }
                                    else if (valor >= 0.60)
                                    {
                                        HojaExcel.Cell(3 + i, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.Yellow;
                                    }
                                    else
                                    {
                                        HojaExcel.Cell(3 + i, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.PaleVioletRed;
                                    }
                                    HojaExcel.Cell(3 + i, Primerrango + 3).Value = valor.ToString();
                                    HojaExcel.Cell(3 + i, Primerrango + 3).Style.NumberFormat.Format = "###,##%";
                                }
                                else
                                {
                                    HojaExcel.Cell(3 + i, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.PaleVioletRed;
                                    HojaExcel.Cell(3 + i, Primerrango + 3).Value = "0";
                                    HojaExcel.Cell(3 + i, Primerrango + 3).Style.NumberFormat.Format = "###,##%";
                                }
                                HojaExcel.Cell(3 + i, Primerrango + 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                HojaExcel.Cell(3 + i, Primerrango + 3).DataType = XLDataType.Number;

                                HojaExcel.Cell(3 + i, Primerrango + 4).Value = "Multiplicador";
                                if (LMultiplicador.Count > 0)
                                {
                                    if (Convert.ToDouble(LMultiplicador[0].TotalMultiplicador) > .80)
                                    {
                                        HojaExcel.Cell(3 + i, Primerrango + 4).Style.Fill.BackgroundColor = XLColor.LightGreen;
                                    }
                                    else if (Convert.ToDouble(LMultiplicador[0].TotalMultiplicador) > .60)
                                    {
                                        HojaExcel.Cell(3 + i, Primerrango + 4).Style.Fill.BackgroundColor = XLColor.Yellow;
                                    }
                                    else
                                    {
                                        HojaExcel.Cell(3 + i, Primerrango + 4).Style.Fill.BackgroundColor = XLColor.PaleVioletRed;
                                    }
                                }
                                else
                                {
                                    HojaExcel.Cell(3 + i, Primerrango + 4).Style.Fill.BackgroundColor = XLColor.PaleVioletRed;
                                }
                                HojaExcel.Cell(3 + i, Primerrango + 4).Value = LMultiplicador.Count > 0 ? (LMultiplicador[0].TotalMultiplicador / 100).ToString() : "0";
                                HojaExcel.Cell(3 + i, Primerrango + 4).Style.NumberFormat.Format = "###,##%";
                                HojaExcel.Cell(3 + i, Primerrango + 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                HojaExcel.Cell(3 + i, Primerrango + 4).DataType = XLDataType.Number;

                                FechaInicial = DateTime.Parse(FechaInicial).AddMonths(1).ToString();
                                Primerrango = segundoRango + 1;
                                segundoRango = Primerrango + 4;
                            }
                        }
                    }


                    HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, 1).Value = "Movimiento 80";
                    HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, 1).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                    HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    FechaInicial = ("01" + "/" + mesAnioInicial.Split('/')[0] + "/" + mesAnioInicial.Split('/')[1]);
                    FechaFinal = ("01" + "/" + mesAniofinal.Split('/')[0] + "/" + mesAniofinal.Split('/')[1]);
                    Primerrango = 2;
                    segundoRango = 6;

                    while (DateTime.Parse(FechaInicial) <= DateTime.Parse(FechaFinal))
                    {

                        List<CatPresupuesto> lista = (from tlist in listaPresupuestototal
                                                      where tlist.Mes == DateTime.Parse(FechaInicial).Month
                                                      && tlist.Anio == DateTime.Parse(FechaInicial).Year
                                                      select tlist).ToList();

                        List<CatPresupuesto> listaRemision = (from tlist in listaremisionTotal
                                                              where tlist.Mes == DateTime.Parse(FechaInicial).Month
                                                              && tlist.Anio == DateTime.Parse(FechaInicial).Year
                                                              select tlist).ToList();

                        List<CatPresupuesto> lista2 = (from tlist in listaPresupuesto2
                                                       where tlist.Id_Rik == 80
                                                       && tlist.Mes == DateTime.Parse(FechaInicial).Month
                                                       && tlist.Anio == DateTime.Parse(FechaInicial).Year
                                                       select tlist).ToList();



                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango).Value = (listaRemision.Count == 0 ? 0 : listaRemision.First().TotalPresupuesto);
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango).Style.Fill.BackgroundColor = XLColor.White;
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango).Style.NumberFormat.Format = "$0.00";
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango).DataType = XLDataType.Number;

                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango + 1).Value = lista2.Count > 0 ? lista2[0].TotalPresupuesto.ToString() : "0";
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango + 1).Style.Fill.BackgroundColor = XLColor.White;
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango + 1).Style.NumberFormat.Format = "$0.00";
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango + 1).DataType = XLDataType.Number;

                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango + 2).Style.Fill.BackgroundColor = XLColor.White;
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango + 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango + 2).Style.NumberFormat.Format = "###,##%";



                        if (double.Parse(lista2.Count > 0 ? lista2[0].TotalPresupuesto.ToString() : "0") > 0)
                        {
                            double valor = ((double.Parse(listaRemision.Count == 0 ? "0" : listaRemision.First().TotalPresupuesto.ToString()) / double.Parse(lista2.Count > 0 ? lista2[0].TotalPresupuesto.ToString() : "0")));

                            if (valor >= 0.80)
                            {
                                HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.LightGreen;
                            }
                            else if (valor >= 0.60)
                            {
                                HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.Yellow;
                            }
                            else
                            {
                                HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.PaleVioletRed;
                            }
                            HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango + 3).Value = valor.ToString();
                            HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango + 3).Style.NumberFormat.Format = "###,##%";
                        }
                        else
                        {
                            HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango + 3).Style.Fill.BackgroundColor = XLColor.PaleVioletRed;
                            HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango + 3).Value = "0";
                            HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango + 3).Style.NumberFormat.Format = "###,##%";
                        }
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango + 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango + 3).DataType = XLDataType.Number;


                        FechaInicial = DateTime.Parse(FechaInicial).AddMonths(1).ToString();
                        Primerrango = segundoRango + 1;
                        segundoRango = Primerrango + 4;
                    }



                    HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), 1).Value = "total:";
                    HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), 1).Style.Fill.BackgroundColor = XLColor.Yellow;
                    HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    FechaInicial = ("01" + "/" + mesAnioInicial.Split('/')[0] + "/" + mesAnioInicial.Split('/')[1]);
                    FechaFinal = ("01" + "/" + mesAniofinal.Split('/')[0] + "/" + mesAniofinal.Split('/')[1]);
                    Primerrango = 2;
                    segundoRango = 6;

                    while (DateTime.Parse(FechaInicial) <= DateTime.Parse(FechaFinal))
                    {
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), Primerrango).FormulaA1 = "=Sum(" + HojaExcel.Cell(3 + 0, Primerrango).Address + ":" + HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango).Address + ")";
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), Primerrango).Style.Fill.BackgroundColor = XLColor.Yellow;
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), Primerrango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), Primerrango).Style.NumberFormat.Format = "$0.00";
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), Primerrango).DataType = XLDataType.Number;

                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), Primerrango + 1).FormulaA1 = "=Sum(" + HojaExcel.Cell(3 + 0, Primerrango + 1).Address + ":" + HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count() - 1, Primerrango + 1).Address + ")";
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), Primerrango + 1).Style.Fill.BackgroundColor = XLColor.Yellow;
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), Primerrango + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), Primerrango + 1).Style.NumberFormat.Format = "$0.00";
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), Primerrango + 1).DataType = XLDataType.Number;

                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), Primerrango + 2).Style.Fill.BackgroundColor = XLColor.Yellow;
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), Primerrango + 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), Primerrango + 3).FormulaA1 = "=((" + HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), Primerrango).Address + "/" + HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), Primerrango + 1).Address + "))";
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), Primerrango + 3).Style.Fill.BackgroundColor = XLColor.Yellow;
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), Primerrango + 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), Primerrango + 3).Style.NumberFormat.Format = "###,##%";

                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), Primerrango + 4).Style.Fill.BackgroundColor = XLColor.Yellow;
                        HojaExcel.Cell(3 + cmbRepresentanteDescargar.Items.Count(), Primerrango + 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                        FechaInicial = DateTime.Parse(FechaInicial).AddMonths(1).ToString();
                        Primerrango = segundoRango + 1;
                        segundoRango = Primerrango + 4;
                    }
                }

                HojaExcel.Columns().AdjustToContents();
                string rutaguardado = Server.MapPath("~/Reportes/") + "ReporteComerciales_" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";


                if (File.Exists(rutaguardado))
                {
                    File.Delete(rutaguardado);
                }

                workbook.SaveAs(rutaguardado);
                string Outgoingfile = "ReporteComerciales_" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";
                string ruta = Server.MapPath("~/Reportes/") + "ReporteComerciales_" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";
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




        protected void cmbSucursalPresupuestovsVenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_Rik Rik = new CN_Rik();
            Rik RegistroRik = new Rik();
            RegistroRik.Id_Emp = Sesion.Id_Emp;
            RegistroRik.Id_Cd = Convert.ToInt32(cmbSucursalPresupuestovsVenta.Value.ToString());
            RegistroRik.Id_Rik = -1;
            List<Rik> list_Riks = new List<Rik>();

            Rik.ConsultaRIk(RegistroRik, ref list_Riks, Emp_CnxCentral);



            var query = (from m in list_Riks
                         group m.nombre_Rik by m.Id_Rik into g
                         select new { id = g.Key, nombre = g.First() }).ToList();

            cmbRepresentanteVSVenta.DataSource = query;
            cmbRepresentanteVSVenta.ValueField = "id";
            cmbRepresentanteVSVenta.TextField = "nombre";
            cmbRepresentanteVSVenta.DataBind();

            cmbRepresentanteVSVenta.Items.Add("--Seleccionar--", "-1");
            cmbRepresentanteVSVenta.Value = "-1";
            UpdatePanel3.Update();
        }

        protected void cargarPresupuestovsVenta()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_Rik Rik = new CN_Rik();
            Rik RegistroRik = new Rik();
            RegistroRik.Id_Emp = Sesion.Id_Emp;
            RegistroRik.Id_Cd = Convert.ToInt32(cmbSucursalPresupuestovsVenta.Value.ToString());
            RegistroRik.Id_Rik = -1;
            List<Rik> list_Riks = new List<Rik>();

            Rik.ConsultaRIk(RegistroRik, ref list_Riks, Emp_CnxCentral);



            var query = (from m in list_Riks
                         group m.nombre_Rik by m.Id_Rik into g
                         select new { id = g.Key, nombre = g.First() }).ToList();

            cmbRepresentanteVSVenta.DataSource = query;
            cmbRepresentanteVSVenta.ValueField = "id";
            cmbRepresentanteVSVenta.TextField = "nombre";
            cmbRepresentanteVSVenta.DataBind();

            cmbRepresentanteVSVenta.Items.Add("--Seleccionar--", "-1");
            cmbRepresentanteVSVenta.Value = "-1";
            UpdatePanel3.Update();
        }


        protected void WCompararRepresentantes_PreviousButtonClick(object sender, WizardNavigationEventArgs e)
        {
            WCompararRepresentantes.PreRender += new EventHandler(WCompararRepresentantes_PreRender);
            updpanel3.Update();
        }

        #endregion

        #region Funciones


        /// <summary>
        /// Funcion que valida la sesion
        /// </summary>
        private void ValidarSesion()
        {
            try
            {
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                    return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// funciona que valida los permiso de la pagina
        /// </summary>
        private void ValidarPermisos()
        {
            try
            {
                if (sesion != null)
                {
                    Session["guardar"] = null;
                    Session["modificar"] = null;
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

                    if (Permiso.PAccesar == true)
                    {
                        _PermisoGuardar = Permiso.PGrabar;
                        _PermisoModificar = Permiso.PModificar;
                        Session["guardar"] = Permiso.PGrabar;
                        Session["modificar"] = Permiso.PModificar;
                    }
                    else
                        Response.Redirect("Inicio.aspx");

                }
                else
                {
                    Response.Redirect("login.aspx");
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Funcion que inicializa toda la informacion inicial de la pantalla y los campos
        /// </summary>
        private void inicializar()
        {
            CargarTrimestre();
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();

            CN_Comun.DevLlenaCombo(2, sesion.Id_Emp, sesion.Id_U, Emp_CnxCentral, "SP_CatCDI_combo2", ref CmbSucursal);

            CMBSucursalRepresentante.Items.AddRange(CmbSucursal.Items.Cast<Object>().ToArray());
            cmbSucursalBinformacion.Items.AddRange(CmbSucursal.Items.Cast<Object>().ToArray());
            cmbSucursalPresupuestovsVenta.Items.AddRange(CmbSucursal.Items.Cast<Object>().ToArray());
            BCMSucursalMultiplicador.Items.AddRange(CmbSucursal.Items.Cast<Object>().ToArray());
            CMBSucursalOT.Items.AddRange(CmbSucursal.Items.Cast<Object>().ToArray());
            cmbSucrusalDescargar.Items.AddRange(CmbSucursal.Items.Cast<Object>().ToArray());
            CMBTrimestreSucursal.Items.AddRange(CmbSucursal.Items.Cast<Object>().ToArray());

            CmbSucursal.Value = sesion.Id_Cd_Ver.ToString();
            CMBSucursalRepresentante.Value = sesion.Id_Cd_Ver.ToString();
            cmbSucursalBinformacion.Value = sesion.Id_Cd_Ver.ToString();
            cmbSucursalPresupuestovsVenta.Value = sesion.Id_Cd_Ver.ToString();
            CMBSucursalOT.Value = sesion.Id_Cd_Ver.ToString();
            BCMSucursalMultiplicador.Value = sesion.Id_Cd_Ver.ToString();
            cmbSucrusalDescargar.Value = sesion.Id_Cd_Ver.ToString();
            CMBTrimestreSucursal.Value = sesion.Id_Cd_Ver.ToString();


            CmbSucursal.ReadOnly = true;
            CMBSucursalRepresentante.ReadOnly = true;
            cmbSucursalBinformacion.ReadOnly = true;
            cmbSucursalPresupuestovsVenta.ReadOnly = true;
            CMBSucursalOT.ReadOnly = true;
            BCMSucursalMultiplicador.ReadOnly = true;
            cmbSucrusalDescargar.ReadOnly = true;
            CMBTrimestreSucursal.ReadOnly = true;

            int id_Cd = Convert.ToInt32(CMBSucursalRepresentante.Value.ToString());
            cargarRIk(id_Cd);
            updpanel3.Update();

            int id_Cd2 = Convert.ToInt32(cmbSucursalBinformacion.Value.ToString());
            cargarRIkBuscarInformacion(id_Cd2);
            UPdBusacarinfo.Update();
            UpdatePanel6.Update();

            int id_Cd3 = Convert.ToInt32(cmbSucrusalDescargar.Value.ToString());
            cargarRIkDescargarInformacion(id_Cd3);

            int id_Cd4 = Convert.ToInt32(CMBTrimestreSucursal.Value.ToString());
            cargarRIkDescargarInformacionTrimestral(id_Cd4);

            cargarPresupuestovsVenta();

            TXTAnioInicialOT.Value = DateTime.Now;
            TXTAnioFinalOT.Value = DateTime.Now;

            txtfechaFinalBuscarInformacion.Value = DateTime.Now;
            txtfechaInicialBuscarInformacion.Value = DateTime.Now;
            txtFechaFinalDescargar.Value = DateTime.Now;
            txtFechaFinalPVV.Value = DateTime.Now;
            TXTFechaFinalRepresentante.Value = DateTime.Now;
            TXTFechaFinalRepresentante.Value = DateTime.Now;
            txtFechaInicialDescargar.Value = DateTime.Now;
            txtFechaInicialPVV.Value = DateTime.Now;
            TXTFechaInicialRepresentante.Value = DateTime.Now;

            TXTFechaInicialPresupuesto.Value = DateTime.Now;
            txtFechaFinalPresupuesto.Value = DateTime.Now;
            TxtFechaMultiplicador.Value = DateTime.Now;

            txtfechaTrimestreInicial.Value = DateTime.Now;
            TxtFechaTrimestreFinal.Value = DateTime.Now;

            cmbBuscarRepresentante.Items.Add("--Todos--", "-1");
            cmbBuscarRepresentante.Value = "-1";

            cmbRepresentanteVSVenta.Items.Add("--Seleccionar--", "-1");
            cmbRepresentanteVSVenta.Value = "-1";

            cmbRepresentanteDescargar.Items.Add("--Todos--", "-1");
            cmbRepresentanteDescargar.Value = "-1";

            BCMRepresentanteTrimestral.Items.Add("--Todos--", "-1");
            BCMRepresentanteTrimestral.Value = "-1";

        }

        protected void cmbSucrusalDescargar_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id_Cd = Convert.ToInt32(cmbSucrusalDescargar.Value.ToString());
            cargarRIkDescargarInformacion(id_Cd);
        }

        protected void CMBTrimestreSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id_Cd = Convert.ToInt32(CMBTrimestreSucursal.Value.ToString());
            cargarRIkDescargarInformacionTrimestral(id_Cd);
        }

        /// <summary>
        /// Cargar la información del trimestre
        /// </summary>
        private void CargarTrimestre()
        {
            List<Mes> ListaMes = new List<Mes>();
            Mes RegistroMes;

            int MesActual = DateTime.Now.Month;

            RegistroMes = new Mes();
            RegistroMes.Id_mes = "-1";
            RegistroMes.NombreMes = "--Seleccionar--";
            ListaMes.Add(RegistroMes);

            RegistroMes = new Mes();
            RegistroMes.Id_mes = "1";
            RegistroMes.NombreMes = "trimestre 1";
            ListaMes.Add(RegistroMes);

            RegistroMes = new Mes();
            RegistroMes.Id_mes = "2";
            RegistroMes.NombreMes = "trimestre 2";
            ListaMes.Add(RegistroMes);

            RegistroMes = new Mes();
            RegistroMes.Id_mes = "3";
            RegistroMes.NombreMes = "trimestre 3";
            ListaMes.Add(RegistroMes);

            RegistroMes = new Mes();
            RegistroMes.Id_mes = "4";
            RegistroMes.NombreMes = "trimestre 4";
            ListaMes.Add(RegistroMes);


            BcbTrimestreInicial.DataSource = ListaMes;
            BcbTrimestreInicial.ValueField = "Id_mes";
            BcbTrimestreInicial.TextField = "NombreMes";
            BcbTrimestreInicial.DataBind();

            BcbTrimestreFinal.DataSource = ListaMes;
            BcbTrimestreFinal.ValueField = "Id_mes";
            BcbTrimestreFinal.TextField = "NombreMes";
            BcbTrimestreFinal.DataBind();

            BcbTrimestreInicial.Value = "-1";
            BcbTrimestreFinal.Value = "-1";
        }

        /// <summary>
        /// Funcion que carga la informacion de seccion de presupuestos
        /// </summary>
        private void cargarPresupuesto()
        {
            try
            {
                ValidarSesion();
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CatPresupuesto Presupuesto = new CatPresupuesto();
                List<CatPresupuesto> listaPresupuesto = new List<CatPresupuesto>();

                if (TXTFechaInicialPresupuesto.Value == null)
                {
                    mensaje("Favor de seleccionar la fecha inical para la consulta de presupuesto.");
                    return;
                }

                if (txtFechaFinalPresupuesto.Value == null)
                {
                    mensaje("Favor de seleccionar la fecha final para la consulta de presupuesto.");
                    return;
                }

                if (DateTime.Parse(TXTFechaInicialPresupuesto.Value.ToString()) > DateTime.Parse(txtFechaFinalPresupuesto.Value.ToString()))
                {
                    mensaje("La fecha inicial es mayor a la fecha final.");
                    return;
                }
                if (CmbSucursal.Value.ToString() == "-1")
                {
                    mensaje("Favor de seleccionar la sucursal para la consulta de presupuesto.");
                    return;
                }

                Presupuesto.Id_Emp = Sesion.Id_Emp;
                Presupuesto.Id_Cd = int.Parse(CmbSucursal.Value.ToString());
                Presupuesto.FechaInicial = DateTime.Parse(TXTFechaInicialPresupuesto.Value.ToString()).Date;
                Presupuesto.fechafinal = DateTime.Parse(txtFechaFinalPresupuesto.Value.ToString()).Date;
                CN_Presupuesto presupuesto = new CN_Presupuesto();
                presupuesto.ConsultaPresupuestoRIk(Presupuesto, ref listaPresupuesto, Emp_CnxCentral);

                foreach (CatPresupuesto registro in listaPresupuesto)
                {
                    string mes = "";
                    ObtenerMEs(ref mes, registro.Mes);
                    registro.Nombre_Mes = mes;
                }


                Session["SPresupuesto"] = listaPresupuesto;
                RgPresupuesto.DataSource = listaPresupuesto;
                RgPresupuesto.DataBind();

                UpdatePanel2.Update();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Carga la informacion del rik y los agrupa por id
        /// </summary>
        /// <param name="id_Cd"></param>
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

            Rik.ConsultaRIk(RegistroRik, ref list_Riks, Emp_CnxCentral);

            var query = (from m in list_Riks
                         group m.nombre_Rik by m.Id_Rik into g
                         select new { id = g.Key, nombre = g.First() }).ToList();

            RBLRepresentante.DataSource = query;
            RBLRepresentante.ValueField = "id";
            RBLRepresentante.TextField = "nombre";
            RBLRepresentante.DataBind();
        }

        /// <summary>
        /// carga la informacion de grid de seccion de multiplicador
        /// </summary>
        private void cargarMultiplicador()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CatMultiplicador Multiplicador = new CatMultiplicador();
                List<CatMultiplicador> listaMultiplicador = new List<CatMultiplicador>();
                CN_Multiplicador CNMultiplicador = new CN_Multiplicador();

                int Mes = DateTime.Parse(TxtFechaMultiplicador.Value.ToString()).Month;
                int Anio = DateTime.Parse(TxtFechaMultiplicador.Value.ToString()).Year;

                Multiplicador.Id_Emp = Sesion.Id_Emp;
                Multiplicador.Id_Cd = int.Parse(BCMSucursalMultiplicador.Value.ToString());
                Multiplicador.Mes = Mes;
                Multiplicador.Anio = Anio;


                CNMultiplicador.ConsultaMultiplicadorRIk(Multiplicador, ref listaMultiplicador, Emp_CnxCentral);

                foreach (CatMultiplicador registro in listaMultiplicador)
                {
                    string mes = "";
                    ObtenerMEs(ref mes, registro.Mes);
                    registro.Nombre_Mes = mes;
                }

                Session["SMultiplicador"] = listaMultiplicador;
                BGVMultiplicador.DataSource = listaMultiplicador;
                BGVMultiplicador.DataBind();
                UpdatePanel4.Update();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void ObtenerMEs(ref string mes, int NumeroMes)
        {
            if (NumeroMes == 1)
            {
                mes = "Enero";
            }
            if (NumeroMes == 2)
            {
                mes = "Febrero";
            }
            if (NumeroMes == 3)
            {
                mes = "Marzo";
            }
            if (NumeroMes == 4)
            {
                mes = "Abril";
            }
            if (NumeroMes == 5)
            {
                mes = "Mayo";
            }
            if (NumeroMes == 6)
            {
                mes = "Junio";
            }
            if (NumeroMes == 7)
            {
                mes = "Julio";
            }
            if (NumeroMes == 8)
            {
                mes = "Agosto";
            }
            if (NumeroMes == 9)
            {
                mes = "Septiembre";
            }
            if (NumeroMes == 10)
            {
                mes = "Octubre";
            }
            if (NumeroMes == 11)
            {
                mes = "Noviembre";
            }
            if (NumeroMes == 12)
            {
                mes = "Diciembre";
            }
        }


        #endregion 

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

        protected void WCompararRepresentantes_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "comparar", "comparar()", false);
            updpanel3.Update();
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

            Rik.ConsultaRIk(RegistroRik, ref list_Riks, Emp_CnxCentral);



            var query = (from m in list_Riks
                         group m.nombre_Rik by m.Id_Rik into g
                         select new { id = g.Key, nombre = g.First() }).ToList();

            cmbBuscarRepresentante.DataSource = query;
            cmbBuscarRepresentante.ValueField = "id";
            cmbBuscarRepresentante.TextField = "nombre";
            cmbBuscarRepresentante.DataBind();

            cmbBuscarRepresentante.Items.Add("--Todos--", "-1");
            cmbBuscarRepresentante.Value = "-1";

        }

        private void cargarRIkDescargarInformacion(int id_Cd)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_Rik Rik = new CN_Rik();
            Rik RegistroRik = new Rik();
            RegistroRik.Id_Emp = Sesion.Id_Emp;
            RegistroRik.Id_Cd = id_Cd;
            RegistroRik.Id_Rik = -1;
            List<Rik> list_Riks = new List<Rik>();

            Rik.ConsultaRIk(RegistroRik, ref list_Riks, Emp_CnxCentral);



            var query = (from m in list_Riks
                         group m.nombre_Rik by m.Id_Rik into g
                         select new { id = g.Key, nombre = g.First() }).ToList();

            cmbRepresentanteDescargar.DataSource = query;
            cmbRepresentanteDescargar.ValueField = "id";
            cmbRepresentanteDescargar.TextField = "nombre";
            cmbRepresentanteDescargar.DataBind();

            cmbRepresentanteDescargar.Items.Add("--Todos--", "-1");
            cmbRepresentanteDescargar.Value = "-1";

        }

        private void cargarRIkDescargarInformacionTrimestral(int id_Cd)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_Rik Rik = new CN_Rik();
            Rik RegistroRik = new Rik();
            RegistroRik.Id_Emp = Sesion.Id_Emp;
            RegistroRik.Id_Cd = id_Cd;
            RegistroRik.Id_Rik = -1;
            List<Rik> list_Riks = new List<Rik>();

            Rik.ConsultaRIk(RegistroRik, ref list_Riks, Emp_CnxCentral);



            var query = (from m in list_Riks
                         group m.nombre_Rik by m.Id_Rik into g
                         select new { id = g.Key, nombre = g.First() }).ToList();

            BCMRepresentanteTrimestral.DataSource = query;
            BCMRepresentanteTrimestral.ValueField = "id";
            BCMRepresentanteTrimestral.TextField = "nombre";
            BCMRepresentanteTrimestral.DataBind();

            BCMRepresentanteTrimestral.Items.Add("--Todos--", "-1");
            BCMRepresentanteTrimestral.Value = "-1";

        }

        #endregion

        #region webMethod



        [WebMethod]
        public static string ObservarTotales(string mesAnioInicial, string mesAniofinal, string seleccion, string Sucursal)
        {
            try
            {
                string Emp_CnxCentral = ConfigurationManager.AppSettings.Get("strConnectionCentral");
                CN_Presupuesto CN_Pres = new CN_Presupuesto();
                CatPresupuesto Presupuesto = new CatPresupuesto();
                List<CatPresupuesto> listaPresupuesto = new List<CatPresupuesto>();
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];

                if (Sesion == null)
                {
                    return JsonConvert.SerializeObject(new { id = 0 });
                }
                int Tiposeleccion = Convert.ToInt32(seleccion);

                if (int.Parse(Sucursal) == -1)
                {
                    return JsonConvert.SerializeObject(new { id = 1 });
                }
                if (mesAnioInicial != "" && mesAniofinal != "")
                {
                    if (DateTime.Parse(mesAnioInicial) > DateTime.Parse(mesAniofinal))
                    {
                        return JsonConvert.SerializeObject(new { id = 4 });
                    }
                    if (Tiposeleccion == 1) //Presupuesto
                    {
                        DateTime FechaInicial2 = DateTime.Parse(mesAnioInicial);
                        DateTime FechaFinal2 = DateTime.Parse(mesAniofinal).AddMonths(1).AddDays(-1);


                        Presupuesto.FechaInicial = FechaInicial2;
                        Presupuesto.fechafinal = FechaFinal2;
                        Presupuesto.Id_Cd = int.Parse(Sucursal);

                        CN_Pres.ConsultaPresupuestoMesualRIk(Presupuesto, ref listaPresupuesto, Emp_CnxCentral);
                        decimal total = 0;
                        string titulo = "";
                        string datos = "";
                        foreach (CatPresupuesto presupuesto in listaPresupuesto)
                        {
                            total = total + presupuesto.TotalPresupuesto;
                            if (titulo == "")
                            {
                                titulo = presupuesto.NombreRik;
                                datos = presupuesto.TotalPresupuesto.ToString();
                            }
                            else
                            {
                                titulo = titulo + "," + presupuesto.NombreRik;
                                datos = datos + "," + presupuesto.TotalPresupuesto.ToString();
                            }

                        }
                        string totalstr = "Total: " + total.ToString("c");
                        return JsonConvert.SerializeObject(new { id = 3, titulo = titulo, datos = datos, total = totalstr });
                    }
                    else //Venta
                    {
                        DateTime fecha = DateTime.Parse(mesAnioInicial);
                        DateTime fechaFinal = DateTime.Parse(mesAniofinal).AddMonths(1).AddDays(-1);
                        Presupuesto.Id_Emp = Sesion.Id_Emp;
                        Presupuesto.Id_Cd = int.Parse(Sucursal);
                        Presupuesto.MesInicial = fecha.Month;
                        Presupuesto.AnioInicial = fecha.Year;
                        Presupuesto.MesFinal = fechaFinal.Month;
                        Presupuesto.AnioFinal = fechaFinal.Year;

                        CN_Pres.ConsultaUtilidadRIk(Presupuesto, ref listaPresupuesto, Emp_CnxCentral);
                        double total = 0;
                        string titulo = "";
                        string datos = "";
                        foreach (CatPresupuesto presupuesto in listaPresupuesto)
                        {
                            total = total + presupuesto.venta;
                            if (titulo == "")
                            {
                                titulo = presupuesto.ter_nombre;
                                datos = presupuesto.venta.ToString("F2");
                            }
                            else
                            {
                                titulo = titulo + "," + presupuesto.ter_nombre;
                                datos = datos + "," + presupuesto.venta.ToString("F2");
                            }
                        }
                        string totalstr = "Total: " + total.ToString("c");
                        return JsonConvert.SerializeObject(new { id = 3, titulo = titulo, datos = datos, total = totalstr });
                    }
                }
                else
                {
                    return JsonConvert.SerializeObject(new { id = 2 });
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = ex.Message });
            }
        }


        [WebMethod]
        public static string CompararRepresentantes(string mesAnioInicial, string mesAniofinal)
        {
            try
            {
                string Emp_CnxCentral = ConfigurationManager.AppSettings.Get("strConnectionCentral");
                CN_Presupuesto CN_Presupuesto = new CN_Presupuesto();
                CatPresupuesto Presupuesto = new CatPresupuesto();
                List<CatPresupuesto> listaPresupuesto = new List<CatPresupuesto>();

                Sesion Sesion = new Sesion();
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
                if (Sesion == null)
                {
                    return JsonConvert.SerializeObject(new { id = 0 });
                }
                string sucursal = HttpContext.Current.Session["Sucursal"].ToString();
                string rik = HttpContext.Current.Session["Representante"].ToString();
                string Seleccion = HttpContext.Current.Session["TipoRepresentante"].ToString();

                string titulo = "";
                string datos = "";

                int seleccion = Convert.ToInt32(Seleccion);


                List<int> myList = new List<int>();

                if (int.Parse(sucursal) == -1)
                {
                    return JsonConvert.SerializeObject(new { id = 1 });
                }
                else
                {
                    if (rik != "")
                    {
                        string[] id_rik = rik.Split(',');
                        foreach (var value in id_rik)
                        {
                            myList.Add(Convert.ToInt32(value));
                        }
                    }

                    if (myList.Count == 0)
                    {
                        return JsonConvert.SerializeObject(new { id = 2 });
                    }
                    else
                    {
                        if (mesAnioInicial == "" || mesAniofinal == "")
                        {
                            return JsonConvert.SerializeObject(new { id = 3 });
                        }
                        else
                        {
                            if (Convert.ToDateTime(mesAnioInicial) > Convert.ToDateTime(mesAniofinal))
                            {
                                return JsonConvert.SerializeObject(new { id = 4 });
                            }
                            if (seleccion == 0) // Ventas
                            {
                                Presupuesto.MesInicial = Convert.ToDateTime(mesAnioInicial).Month;
                                Presupuesto.AnioInicial = Convert.ToDateTime(mesAnioInicial).Year;
                                Presupuesto.MesFinal = Convert.ToDateTime(mesAniofinal).Month;
                                Presupuesto.AnioFinal = Convert.ToDateTime(mesAniofinal).Year;
                                Presupuesto.Id_Emp = Sesion.Id_Emp;
                                Presupuesto.Id_Cd = int.Parse(sucursal);
                                CN_Presupuesto.ConsultaUtilidadRIk(Presupuesto, ref listaPresupuesto, Emp_CnxCentral);

                                List<CatPresupuesto> lista = (from tlist in listaPresupuesto
                                                              where myList.Contains(tlist.id_ter)
                                                              select tlist).ToList();

                                foreach (CatPresupuesto presupuesto in lista)
                                {
                                    if (titulo == "")
                                    {
                                        titulo = presupuesto.ter_nombre;
                                        datos = presupuesto.venta.ToString("F2");
                                    }
                                    else
                                    {
                                        titulo = titulo + "," + presupuesto.ter_nombre;
                                        datos = datos + "," + presupuesto.venta.ToString("F2");
                                    }
                                }
                                return JsonConvert.SerializeObject(new { id = 5, titulo = titulo, datos = datos });
                            }
                            else if (seleccion == 1) // Utilidad
                            {
                                Presupuesto.MesInicial = Convert.ToDateTime(mesAnioInicial).Month;
                                Presupuesto.AnioInicial = Convert.ToDateTime(mesAnioInicial).Year;
                                Presupuesto.MesFinal = Convert.ToDateTime(mesAniofinal).Month;
                                Presupuesto.AnioFinal = Convert.ToDateTime(mesAniofinal).Year;
                                Presupuesto.Id_Emp = Sesion.Id_Emp;
                                Presupuesto.Id_Cd = int.Parse(sucursal);

                                CN_Presupuesto.ConsultaUtilidadRIk(Presupuesto, ref listaPresupuesto, Emp_CnxCentral);

                                List<CatPresupuesto> lista = (from tlist in listaPresupuesto
                                                              where myList.Contains(tlist.id_ter)
                                                              select tlist).ToList();

                                foreach (CatPresupuesto presupuesto in lista)
                                {
                                    if (titulo == "")
                                    {
                                        titulo = presupuesto.ter_nombre;
                                        datos = presupuesto.utilidadBruta.ToString("F2");
                                    }
                                    else
                                    {
                                        titulo = titulo + "," + presupuesto.ter_nombre;
                                        datos = datos + "," + presupuesto.utilidadBruta.ToString("F2");
                                    }
                                }
                                return JsonConvert.SerializeObject(new { id = 7, titulo = titulo, datos = datos });
                            }
                            else if (seleccion == 2) // Presupuesto
                            {
                                DateTime FechaInicial2 = DateTime.Parse(mesAnioInicial);
                                DateTime FechaFinal2 = DateTime.Parse(mesAniofinal).AddMonths(1).AddDays(-1); ;
                                Presupuesto.FechaInicial = FechaInicial2;
                                Presupuesto.fechafinal = FechaFinal2;
                                Presupuesto.Id_Emp = Sesion.Id_Emp;
                                Presupuesto.Id_Cd = int.Parse(sucursal);

                                CN_Presupuesto.ConsultaPresupuestoMesualRIk(Presupuesto, ref listaPresupuesto, Emp_CnxCentral);

                                List<CatPresupuesto> lista = (from tlist in listaPresupuesto
                                                              where myList.Contains(tlist.Id_Rik)
                                                              select tlist).ToList();


                                foreach (CatPresupuesto presupuesto in lista)
                                {
                                    if (titulo == "")
                                    {
                                        titulo = presupuesto.NombreRik;
                                        datos = presupuesto.TotalPresupuesto.ToString("F2");
                                    }
                                    else
                                    {
                                        titulo = titulo + "," + presupuesto.NombreRik;
                                        datos = datos + "," + presupuesto.TotalPresupuesto.ToString("F2");
                                    }
                                }
                                return JsonConvert.SerializeObject(new { id = 8, titulo = titulo, datos = datos });


                            }
                            else if (seleccion == 3) // Multiplicador
                            {
                                CN_Multiplicador CN_multiplicador = new CN_Multiplicador();
                                CatMultiplicador CatMultiplicador = new CatMultiplicador();
                                List<CatMultiplicador> listaMultiplicador = new List<CatMultiplicador>();

                                DateTime FechaInicial2 = DateTime.Parse(mesAnioInicial);
                                DateTime FechaFinal2 = DateTime.Parse(mesAniofinal).AddMonths(1).AddDays(-1);
                                CatMultiplicador.FechaInicial = FechaInicial2;
                                CatMultiplicador.FechaFinal = FechaFinal2;
                                CatMultiplicador.Id_Emp = Sesion.Id_Emp;
                                CatMultiplicador.Id_Cd = int.Parse(sucursal);

                                CN_multiplicador.ConsultaMltiplicadorMensualRIk(CatMultiplicador, ref listaMultiplicador, Emp_CnxCentral);

                                List<CatMultiplicador> lista = (from tlist in listaMultiplicador
                                                                where myList.Contains(tlist.Id_Rik)
                                                                select tlist).ToList();

                                foreach (CatMultiplicador presupuesto in lista)
                                {
                                    if (titulo == "")
                                    {
                                        titulo = presupuesto.NombreRik;
                                        datos = (presupuesto.TotalMultiplicador).ToString("F2");
                                    }
                                    else
                                    {
                                        titulo = titulo + "," + presupuesto.NombreRik;
                                        datos = datos + "," + (presupuesto.TotalMultiplicador).ToString("F2");
                                    }
                                }
                                return JsonConvert.SerializeObject(new { id = 6, titulo = titulo, datos = datos });
                            }
                            else
                            {
                                return JsonConvert.SerializeObject(new { id = 4 });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = ex.Message });
            }
        }

        [WebMethod]
        public static string btnBucaPVV_ServerClick(string mesAnioInicial, string mesAniofinal, string sucursal, string idRik)
        {
            try
            {
                string Emp_CnxCentral = ConfigurationManager.AppSettings.Get("strConnectionCentral");
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
                if (Sesion == null)
                {
                    return JsonConvert.SerializeObject(new { id = 0 });
                }
                CN_Presupuesto cdpresupuesto = new CN_Presupuesto();
                CatPresupuesto Presupuesto = new CatPresupuesto();
                List<CatPresupuesto> listaPresupuesto = new List<CatPresupuesto>();
                CN_Presupuesto presupuesto = new CN_Presupuesto();
                List<CatPresupuesto> listaPresupuesto2 = new List<CatPresupuesto>();

                Presupuesto.Id_Emp = Sesion.Id_Emp;
                Presupuesto.Id_Cd = int.Parse(sucursal);
                if (Convert.ToInt32(sucursal) == -1)
                {

                    return JsonConvert.SerializeObject(new { id = 1 });
                }
                else if (mesAnioInicial == "" || mesAniofinal == "")
                {
                    return JsonConvert.SerializeObject(new { id = 3 });
                }
                else if (Convert.ToInt32(idRik) == -1)
                {
                    return JsonConvert.SerializeObject(new { id = 2 });
                }
                else
                {

                    Presupuesto.MesInicial = DateTime.Parse(mesAnioInicial).Month;
                    Presupuesto.AnioInicial = DateTime.Parse(mesAnioInicial).Year;
                    Presupuesto.MesFinal = DateTime.Parse(mesAniofinal).Month;
                    Presupuesto.AnioFinal = DateTime.Parse(mesAniofinal).Year;

                    DateTime FechaInicial2 = DateTime.Parse(mesAnioInicial);
                    DateTime FechaFinal2 = DateTime.Parse(mesAniofinal).AddMonths(1).AddDays(-1);
                    Presupuesto.FechaInicial = FechaInicial2;
                    Presupuesto.fechafinal = FechaFinal2;
                    Presupuesto.Id_Rik = Convert.ToInt32(idRik);
                    Presupuesto.Id_u = Convert.ToInt32(idRik);

                    cdpresupuesto.ConsultaUtilidadRIkxmes(Presupuesto, ref listaPresupuesto, Emp_CnxCentral);
                    presupuesto.ConsultaPresupuestoMesualPvvRIk(Presupuesto, ref listaPresupuesto2, Emp_CnxCentral);


                    DateTime AnioInicial = DateTime.Parse(mesAnioInicial);
                    DateTime AnioFinal = DateTime.Parse(mesAniofinal);
                    if (AnioInicial > AnioFinal)
                    {
                        return JsonConvert.SerializeObject(new { id = 4 });
                    }

                    string titulo = "";

                    string datos = "";
                    string datos2 = "";

                    for (var i = AnioInicial; i <= AnioFinal;)
                    {

                        List<CatPresupuesto> lista2 = (from tlist in listaPresupuesto
                                                       where tlist.Id_Rik == Convert.ToInt32(idRik) &&
                                                       tlist.fecha == AnioInicial
                                                       select tlist).ToList();

                        List<CatPresupuesto> lista = (from tlist in listaPresupuesto2
                                                      where tlist.Id_Rik == Convert.ToInt32(idRik) &&
                                                      tlist.fecha == AnioInicial
                                                      select tlist).ToList();

                        int NumeroMes = AnioInicial.Month;
                        string mes = "";

                        if (NumeroMes == 1)
                        {
                            mes = "Enero";
                        }
                        if (NumeroMes == 2)
                        {
                            mes = "Febrero";
                        }
                        if (NumeroMes == 3)
                        {
                            mes = "Marzo";
                        }
                        if (NumeroMes == 4)
                        {
                            mes = "Abril";
                        }
                        if (NumeroMes == 5)
                        {
                            mes = "Mayo";
                        }
                        if (NumeroMes == 6)
                        {
                            mes = "Junio";
                        }
                        if (NumeroMes == 7)
                        {
                            mes = "Julio";
                        }
                        if (NumeroMes == 8)
                        {
                            mes = "Agosto";
                        }
                        if (NumeroMes == 9)
                        {
                            mes = "Septiembre";
                        }
                        if (NumeroMes == 10)
                        {
                            mes = "Octubre";
                        }
                        if (NumeroMes == 11)
                        {
                            mes = "Noviembre";
                        }
                        if (NumeroMes == 12)
                        {
                            mes = "Diciembre";
                        }

                        if (titulo == "")
                        {
                            titulo = mes + " " + AnioInicial.Year.ToString();
                        }
                        else
                        {
                            titulo = titulo + "," + mes + " " + AnioInicial.Year.ToString();
                        }

                        if (lista2.Count() == 0)
                        {
                            if (datos == "")
                            {
                                datos = "0";
                            }
                            else
                            {
                                datos = datos + "," + "0";
                            }
                        }
                        else
                        {
                            foreach (CatPresupuesto registro in lista2)
                            {
                                if (datos == "")
                                {
                                    datos = registro.venta.ToString("F2");
                                }
                                else
                                {
                                    datos = datos + "," + registro.venta.ToString("F2");
                                }
                            }
                        }

                        if (lista.Count() == 0)
                        {
                            if (datos2 == "")
                            {
                                datos2 = "0";
                            }
                            else
                            {
                                datos2 = datos2 + "," + "0";
                            }
                        }
                        else
                        {
                            foreach (CatPresupuesto registro2 in lista)
                            {
                                if (datos2 == "")
                                {
                                    datos2 = registro2.TotalPresupuesto.ToString("F2");
                                }
                                else
                                {
                                    datos2 = datos2 + "," + registro2.TotalPresupuesto.ToString("F2");
                                }
                            }
                        }

                        AnioInicial = AnioInicial.AddMonths(1);
                        i = AnioInicial;
                    }

                    return JsonConvert.SerializeObject(new { id = 5, titulo = titulo, datos = datos, datos2 = datos2 });
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = ex.Message });
            }
        }

        #endregion
    }
}