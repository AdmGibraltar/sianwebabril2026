using CapaEntidad;
using CapaNegocios;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIANWEB
{
    public partial class RepComisionesPresupuestoConsultaRik : System.Web.UI.Page
    {


        #region variables

        private Sesion Sesion
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

        private static string fileName = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Plantilla_Presupuestos.xls");
        public string NombreArchivo;
        public string NombreHojaExcel;


        private List<ComisionesPresupuesto> ListVertical
        {
            get { return (List<ComisionesPresupuesto>)Session["PresupuestosConsultarVertical" + Session.SessionID]; }
            set { Session["PresupuestosConsultarVertical" + Session.SessionID] = value; }

        }

        private List<ComisionesPresupuesto> ListHorizontal
        {
            get { return (List<ComisionesPresupuesto>)Session["PresupuestosConsultar" + Session.SessionID]; }
            set { Session["PresupuestosConsultar" + Session.SessionID] = value; }

        }
        private List<ComisionesPresupuesto> ListReal
        {
            get { return (List<ComisionesPresupuesto>)Session["PresupuestosConsultarReal" + Session.SessionID]; }
            set { Session["PresupuestosConsultarReal" + Session.SessionID] = value; }

        }

        private string Emp_CnxCentral
        {
            get { return ConfigurationManager.AppSettings.Get("strConnectionSIANCentral"); }
        }

        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }


        #endregion variables



        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                //panel movible 


                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }
                else
                {
                    if (Sesion.Id_Rik > 0)
                    {
                        this.HF_Rik.Value = Convert.ToString(Sesion.Id_Rik);
                    }

                    this.TblEncabezado.Visible = false;

                    if (!Page.IsPostBack)
                    {

                        this.ValidarPermisos();
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            return;
                        }

                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_Cve.Value = randObj.Next().ToString();


                        this.TblEncabezado.Visible = false;
                        // LoadModulosActivos();
                        CargarRepresentantes();


                    }

                }

                //grVertical.CustomColumnDisplayText += grdHorizontal_CustomColumnDisplayText;
                //ASPxGridView1.CustomColumnDisplayText += grdHorizontal_CustomColumnDisplayText;



            }
            catch (Exception ex)
            {
                mensajes(string.Concat(ex.Message, "Page_Load_error"));
            }
        }
        #endregion Page_Load


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
                    _PermisoImprimir = Permiso.PImprimir;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {

            List<ComisionesPresupuesto> List = new List<ComisionesPresupuesto>();



            if (ListVertical != null)
            {
                List = ListVertical;
                grVertical.DataSource = List;
                grVertical.DataBind();

            }

            if (ListHorizontal != null)
            {
                List = ListHorizontal;
                grdHorizontal.DataSource = List;
                grdHorizontal.DataBind();

            }

            if (ListReal != null)
            {
                List = ListReal;
                grdRealUp.DataSource = List;
                grdRealUp.DataBind();

            }


            //else
            //{
            //    List = GetListVertical();
            //    DataTable dt = new DataTable();

            //    dt = Funcion.Convertidor<ComisionesPresupuesto>.ListaToDatatable(List);
            //    grVertical.DataSource = dt;
            //    grVertical.DataBind();


            //}

        }

        #region cargar información

        /// <summary>
        /// FUNCION QUE CARGA EL GRID DE HORIZONTAL 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        private List<ComisionesPresupuesto> GetList()
        {
            try
            {
                CN_ComisionesPresupuesto clsComisionesPresupuesto = new CN_ComisionesPresupuesto();
                List<ComisionesPresupuesto> list = new List<ComisionesPresupuesto>();

                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                ComisionesPresupuesto presupuesto = new ComisionesPresupuesto();
                presupuesto.Id_Emp = session.Id_Emp;
                presupuesto.Num_Cdi = session.Id_Cd_Ver;
                //JFCV 29nov2016 INICIO agregar los filtros para que se realice la busqueda 
                //if (cmdCdi.SelectedIndex == 0)
                //{
                //    presupuesto.Num_Cdi = session.Id_Cd_Ver;
                //}
                //else
                //{
                //    presupuesto.Num_Cdi = Convert.ToInt32(cmdCdi.SelectedItem.Value);
                //}

                if (cmbRik.SelectedIndex != -1)
                {
                    presupuesto.Id_Rik = Convert.ToInt32(cmbRik.SelectedItem.Value);
                }
                //else
                //{
                //    presupuesto.Anio = Convert.ToInt32(cmbAnio.SelectedItem.Value);
                //}

                if (session.Id_Rik > 0)
                {
                    presupuesto.Id_Rik = session.Id_Rik;
                }


                if (cmbAnio.SelectedIndex == -1)
                {
                    presupuesto.Anio = session.Id_Cd_Ver;
                }
                else
                {
                    presupuesto.Anio = Convert.ToInt32(cmbAnio.SelectedItem.Value);
                }



                clsComisionesPresupuesto.CapComisionesPresupuesto_List(presupuesto, Emp_CnxCentral, ref list);



                ListHorizontal = list;
                Session["PresupuestosConsultar" + Session.SessionID] = list;


                return list;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private List<ComisionesPresupuesto> GetListUPReal()
        {
            try
            {
                CN_ComisionesPresupuesto clsComisionesPresupuesto = new CN_ComisionesPresupuesto();
                List<ComisionesPresupuesto> list = new List<ComisionesPresupuesto>();

                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                ComisionesPresupuesto presupuesto = new ComisionesPresupuesto();
                presupuesto.Id_Emp = session.Id_Emp;
                presupuesto.Num_Cdi = session.Id_Cd_Ver;
                //JFCV 29nov2016 INICIO agregar los filtros para que se realice la busqueda 
                //if (cmdCdi.SelectedIndex == 0)
                //{
                //    presupuesto.Num_Cdi = session.Id_Cd_Ver;
                //}
                //else
                //{
                //    presupuesto.Num_Cdi = Convert.ToInt32(cmdCdi.SelectedItem.Value);
                //}

                if (cmbRik.SelectedIndex != -1)
                {
                    presupuesto.Id_Rik = Convert.ToInt32(cmbRik.SelectedItem.Value);
                }
                //else
                //{
                //    presupuesto.Anio = Convert.ToInt32(cmbAnio.SelectedItem.Value);
                //}

                if (session.Id_Rik > 0)
                {
                    presupuesto.Id_Rik = session.Id_Rik;
                }


                if (cmbAnio.SelectedIndex == -1)
                {
                    presupuesto.Anio = session.Id_Cd_Ver;
                }
                else
                {
                    presupuesto.Anio = Convert.ToInt32(cmbAnio.SelectedItem.Value);
                }



                clsComisionesPresupuesto.CapComisionesPresupuesto_UPReal(presupuesto, Emp_CnxCentral, ref list);



                ListReal = list;
                Session["PresupuestosConsultarReal" + Session.SessionID] = list;


                return list;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// FUNCION QUE CARGA EL GRID DE VERTICALES 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private List<ComisionesPresupuesto> GetListVertical()
        {
            try
            {
                CN_ComisionesPresupuesto clsComisionesPresupuesto = new CN_ComisionesPresupuesto();
                List<ComisionesPresupuesto> list = new List<ComisionesPresupuesto>();

                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                ComisionesPresupuesto presupuesto = new ComisionesPresupuesto();
                presupuesto.Id_Emp = session.Id_Emp;
                presupuesto.Num_Cdi = session.Id_Cd_Ver;
                //JFCV 29nov2016 INICIO agregar los filtros para que se realice la busqueda 

                presupuesto.Num_Cdi = session.Id_Cd_Ver;
                //}
                //else
                //{
                //    presupuesto.Num_Cdi = Convert.ToInt32(cmdCdi.SelectedItem.Value);
                //}
                if (cmbAnio.SelectedIndex == -1)
                {
                    presupuesto.Anio = session.Id_Cd_Ver;
                }
                else
                {
                    presupuesto.Anio = Convert.ToInt32(cmbAnio.SelectedItem.Value);
                }


                presupuesto.Id_Rik = Convert.ToInt32(cmbRik.SelectedValue.ToString());

                if (session.Id_Rik > 0)
                {
                    presupuesto.Id_Rik = session.Id_Rik;
                }


                clsComisionesPresupuesto.CapComisionesPresupuestoPorRik(presupuesto, Emp_CnxCentral, ref list);

                ListVertical = list;
                Session["PresupuestosConsultarVertical" + Session.SessionID] = list;

                return list;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadModulosActivos()
        {
            var dt = new DataTable();
            try
            {
                //grdHorizontal.DataSource = GetList();
                //grdHorizontal.DataBind();

                grVertical.DataSource = GetListVertical();
                grVertical.DataBind();

            }
            catch (Exception ex)
            {
                mensajes("Error al cargar la lista de Empresas.");

            }
        }

        private void CargarRepresentantes()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                CN__Comun cn_comun = new CN__Comun();
                cn_comun.LlenaCombo(2, sesion.Id_Cd_Ver, sesion.Emp_Cnx, "spCatRik_ComboTodos", ref cmbRik, true);

                if (sesion.Id_TU == 2)
                {
                    //this.trRik.Visible = false;

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion

        #region Eventos del Grid



        private void mensajes(string mensaje)
        {
            this.TblEncabezado.Visible = true;
            lblMensaje.Text = mensaje;
            lblMensaje.Visible = true;

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "modalMensaje('" + mensaje + "')", true);
        }


        #region exportar a excell vertical


        protected void btnGenerarEx_Click(object sender, EventArgs e)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            try
            {

                if (cmbRik.SelectedIndex > 0)
                {





                    //traer datos de la BD 


                    List<ComisionesPresupuesto> list = new List<ComisionesPresupuesto>();
                    List<ComisionesPresupuesto> listReal = new List<ComisionesPresupuesto>();
                    List<ComisionesPresupuesto> listpresupuesto = new List<ComisionesPresupuesto>();
                    list = GetListVertical();
                    listReal = GetListUPReal();
                    listpresupuesto = GetList();


                    list = ListVertical;
                    listReal = ListReal;
                    listpresupuesto = ListHorizontal;



                    string nombre = cmbRik.Text;
                    int i = 8;  // = renglón 

                    using (var workbook = new XLWorkbook())
                    {


                        var HojaExcel = workbook.Worksheets.Add(nombre);

                        foreach (ComisionesPresupuesto lista2 in list)
                        {

                            if (i == 8)
                            {

                                //var HojaExcel = workbook.Worksheets.Add(nombre);

                                HojaExcel.Range("E2", "E4").Style.Font.Bold = true;
                                //HojaExcel.Range("A9", "AH9").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                                //HojaExcel.Range("A9", "AH9").Style.Font.FontColor = XLColor.White;

                                #region encabezados
                                HojaExcel.Cell("E2").Value = "Rik";
                                HojaExcel.Cell("E3").Value = "Número RIK";
                                HojaExcel.Cell("E4").Value = "Cdi";
                                HojaExcel.Cell("F2").Value = nombre;
                                HojaExcel.Cell("F3").Value = TxtId_Rik.Text;
                                HojaExcel.Cell("F3").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                                HojaExcel.Cell("F4").Value = Sesion.Cd_Nombre;

                                HojaExcel.Cell("M5").Value = "INSTRUCCIONES";
                                HojaExcel.Cell("M6").Value = "1.- Agregar en la fila amarilla el presupuesto mensual de UP";
                                HojaExcel.Cell("M7").Value = "2.- Agregar en la fila azul de la UB2 desde SIAN WEB";
                                HojaExcel.Cell("M9").Value = "*se recomienda modificar únicamente las celdas editables ( amarillo y azul )";

                                HojaExcel.Cell("A23").Value = "PPTO UP";
                                HojaExcel.Cell("A26").Value = "REPORTE UB 2";

                                HojaExcel.Cell("M10").Value = "Editables";
                                HojaExcel.Cell("M11").Style.Fill.BackgroundColor = XLColor.BabyBlue;
                                HojaExcel.Cell("M11").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                HojaExcel.Cell("M11").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                HojaExcel.Cell("M11").Style.Font.Bold = true;
                                HojaExcel.Cell("M11").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                HojaExcel.Cell("M12").Style.Fill.BackgroundColor = XLColor.ArylideYellow;
                                HojaExcel.Cell("M12").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                HojaExcel.Cell("M12").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                HojaExcel.Cell("M12").Style.Font.Bold = true;
                                HojaExcel.Cell("M12").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;




                                HojaExcel.Cell("D7").Value = "MES";
                                HojaExcel.Cell("D7").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                                HojaExcel.Cell("D7").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                HojaExcel.Cell("D7").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


                                HojaExcel.Cell("E7").Value = "BASE";
                                HojaExcel.Cell("E7").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                                HojaExcel.Cell("E7").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                HojaExcel.Cell("E7").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                                HojaExcel.Cell("F6").Value = "UTILIDAD ";
                                HojaExcel.Cell("F6").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                                HojaExcel.Cell("F6").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                HojaExcel.Cell("F6").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                                HojaExcel.Cell("G6").Value = "PRIMA ";
                                HojaExcel.Cell("G6").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                                HojaExcel.Cell("G6").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                HojaExcel.Cell("G6").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                                HojaExcel.Cell("F7").Value = "PRESUPUESTO ";
                                HojaExcel.Cell("F7").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                                HojaExcel.Cell("F7").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                HojaExcel.Cell("F7").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                                HojaExcel.Cell("G7").Value = "REAL ";
                                HojaExcel.Cell("G7").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                                HojaExcel.Cell("G7").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                HojaExcel.Cell("G7").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


                                HojaExcel.Cell("H7").Value = "META PPTO";
                                HojaExcel.Cell("H7").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                                HojaExcel.Cell("H7").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                HojaExcel.Cell("H7").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


                                HojaExcel.Cell("I7").Value = "INCREMENTO REAL";
                                HojaExcel.Cell("I7").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                                HojaExcel.Cell("I7").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                HojaExcel.Cell("I7").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                                HojaExcel.Cell("J7").Value = "% CUMPLIMIENTO";
                                HojaExcel.Cell("J7").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                                HojaExcel.Cell("J7").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                HojaExcel.Cell("J7").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                                HojaExcel.Cell("K7").Value = "MULTIPLICADOR";
                                HojaExcel.Cell("K7").Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                                HojaExcel.Cell("K7").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                HojaExcel.Cell("K7").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                                // SEGMENTO DE PRESUPUESTO UTILIDAD PRIMA 
                                HojaExcel.Range("C22", "P22").Style.Font.Bold = true;
                                HojaExcel.Range("A23", "A26").Style.Font.Bold = true;
                                HojaExcel.Range("D25", "P25").Style.Font.Bold = true;

                                HojaExcel.Cell("I21").Value = "Presupuesto Utilidad Prima";

                                HojaExcel.Cell("C22").Value = "BASE";
                                HojaExcel.Cell("C22").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                HojaExcel.Cell("D22").Value = "ENERO";
                                HojaExcel.Cell("D22").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                HojaExcel.Cell("E22").Value = "FEBRERO";
                                HojaExcel.Cell("E22").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                HojaExcel.Cell("F22").Value = "MARZO";
                                HojaExcel.Cell("F22").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                HojaExcel.Cell("G22").Value = "ABRIL";
                                HojaExcel.Cell("G22").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                HojaExcel.Cell("H22").Value = "MAYO";
                                HojaExcel.Cell("H22").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                HojaExcel.Cell("I22").Value = "JUNIO";
                                HojaExcel.Cell("I22").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                HojaExcel.Cell("J22").Value = "JULIO";
                                HojaExcel.Cell("J22").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                HojaExcel.Cell("K22").Value = "AGOSTO";
                                HojaExcel.Cell("K22").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                HojaExcel.Cell("L22").Value = "SEPTIEMBRE";
                                HojaExcel.Cell("L22").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                HojaExcel.Cell("M22").Value = "OCTUBRE";
                                HojaExcel.Cell("M22").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                HojaExcel.Cell("N22").Value = "NOVIEMBRE";
                                HojaExcel.Cell("N22").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                HojaExcel.Cell("O22").Value = "DICIEMBRE";
                                HojaExcel.Cell("O22").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                HojaExcel.Cell("P22").Value = "META";
                                HojaExcel.Cell("P22").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                                //REAL UTILIDAD PRIMA

                                HojaExcel.Cell("I24").Value = "Real Utilidad Prima";
                                HojaExcel.Cell("D25").Value = "ENERO";
                                HojaExcel.Cell("D25").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                HojaExcel.Cell("E25").Value = "FEBRERO";
                                HojaExcel.Cell("E25").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                HojaExcel.Cell("F25").Value = "MARZO";
                                HojaExcel.Cell("F25").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                HojaExcel.Cell("G25").Value = "ABRIL";
                                HojaExcel.Cell("G25").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                HojaExcel.Cell("H25").Value = "MAYO";
                                HojaExcel.Cell("H25").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                HojaExcel.Cell("I25").Value = "JUNIO";
                                HojaExcel.Cell("I25").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                HojaExcel.Cell("J25").Value = "JULIO";
                                HojaExcel.Cell("J25").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                HojaExcel.Cell("K25").Value = "AGOSTO";
                                HojaExcel.Cell("K25").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                HojaExcel.Cell("L25").Value = "SEPTIEMBRE";
                                HojaExcel.Cell("L25").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                HojaExcel.Cell("M25").Value = "OCTUBRE";
                                HojaExcel.Cell("M25").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                HojaExcel.Cell("N25").Value = "NOVIEMBRE";
                                HojaExcel.Cell("N25").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                HojaExcel.Cell("O25").Value = "DICIEMBRE";
                                HojaExcel.Cell("O25").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                HojaExcel.Cell("P25").Value = "META";
                                HojaExcel.Cell("P25").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


                                #endregion encabezados

                            }


                            #region agrego los datos 
                            //Renglón i  comienzo en 8 esa es mi primer fila de datos
                            HojaExcel.Cell(i, 4).Value = lista2.MesLetra.ToString();

                            HojaExcel.Cell(i, 5).Value = lista2.BaseUP.ToString();
                            HojaExcel.Cell(i, 5).Style.NumberFormat.Format = "#,##0.00";
                            HojaExcel.Cell(i, 5).DataType = XLDataType.Number;


                            HojaExcel.Cell(i, 6).Value = lista2.UP_Presupuesto.ToString();
                            HojaExcel.Cell(i, 6).Style.NumberFormat.Format = "#,##0.00";
                            HojaExcel.Cell(i, 6).DataType = XLDataType.Number;


                            HojaExcel.Cell(i, 7).Value = lista2.UP.ToString();
                            HojaExcel.Cell(i, 7).Style.NumberFormat.Format = "#,##0.00";
                            HojaExcel.Cell(i, 7).DataType = XLDataType.Number;

                            HojaExcel.Cell(i, 8).Value = lista2.Meta_Ppto.ToString();
                            HojaExcel.Cell(i, 8).Style.NumberFormat.Format = "#,##0.00";
                            HojaExcel.Cell(i, 8).DataType = XLDataType.Number;


                            HojaExcel.Cell(i, 9).Value = lista2.Incremento_Real.ToString();
                            HojaExcel.Cell(i, 9).Style.NumberFormat.Format = "#,##0.00";
                            HojaExcel.Cell(i, 9).DataType = XLDataType.Number;


                            HojaExcel.Cell(i, 10).Value = lista2.Porc_Cumplimiento.ToString();
                            HojaExcel.Cell(i, 10).Style.NumberFormat.Format = "0%";
                            HojaExcel.Cell(i, 10).DataType = XLDataType.Number;


                            HojaExcel.Cell(i, 11).Value = lista2.Multiplicador.ToString();
                            HojaExcel.Cell(i, 11).Style.NumberFormat.Format = "0%";
                            HojaExcel.Cell(i, 11).DataType = XLDataType.Number;




                            //HojaExcel.Cell(3 + i, segundoRango).Value = lista2[i].Analisis > 0 ? lista2[i].Analisis.ToString() : "";
                            //HojaExcel.Cell(3 + i, segundoRango).Style.Fill.BackgroundColor = XLColor.White;
                            //HojaExcel.Cell(3 + i, segundoRango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            //HojaExcel.Cell(3 + i, segundoRango).Style.NumberFormat.Format = "$#,##0.00";
                            //HojaExcel.Cell(3 + i, segundoRango).DataType = XLDataType.Number;

                            #endregion datos


                            i++;

                        }

                        //agrego renglón de presupuestos
                        i = 23;

                        HojaExcel.Range("C23", "O23").Style.Fill.BackgroundColor = XLColor.ArylideYellow;

                        foreach (ComisionesPresupuesto listap in listpresupuesto)
                        {

                            #region agrego los datos 
                            //Renglón i   

                            HojaExcel.Cell(i, 3).Value = listap.BaseUP.ToString();

                            HojaExcel.Cell(i, 4).Value = listap.UP1.ToString();
                            HojaExcel.Cell(i, 4).Style.NumberFormat.Format = "#,##0.00";
                            HojaExcel.Cell(i, 4).DataType = XLDataType.Number;

                            HojaExcel.Cell(i, 5).Value = listap.UP2.ToString();
                            HojaExcel.Cell(i, 5).Style.NumberFormat.Format = "#,##0.00";
                            HojaExcel.Cell(i, 5).DataType = XLDataType.Number;


                            HojaExcel.Cell(i, 6).Value = listap.UP3.ToString();
                            HojaExcel.Cell(i, 6).Style.NumberFormat.Format = "#,##0.00";
                            HojaExcel.Cell(i, 6).DataType = XLDataType.Number;


                            HojaExcel.Cell(i, 7).Value = listap.UP4.ToString();
                            HojaExcel.Cell(i, 7).Style.NumberFormat.Format = "#,##0.00";
                            HojaExcel.Cell(i, 7).DataType = XLDataType.Number;

                            HojaExcel.Cell(i, 8).Value = listap.UP5.ToString();
                            HojaExcel.Cell(i, 8).Style.NumberFormat.Format = "#,##0.00";
                            HojaExcel.Cell(i, 8).DataType = XLDataType.Number;


                            HojaExcel.Cell(i, 9).Value = listap.UP6.ToString();
                            HojaExcel.Cell(i, 9).Style.NumberFormat.Format = "#,##0.00";
                            HojaExcel.Cell(i, 9).DataType = XLDataType.Number;


                            HojaExcel.Cell(i, 10).Value = listap.UP7.ToString();
                            HojaExcel.Cell(i, 10).Style.NumberFormat.Format = "#,##0.00";
                            HojaExcel.Cell(i, 10).DataType = XLDataType.Number;


                            HojaExcel.Cell(i, 11).Value = listap.UP8.ToString();
                            HojaExcel.Cell(i, 11).Style.NumberFormat.Format = "#,##0.00";
                            HojaExcel.Cell(i, 11).DataType = XLDataType.Number;

                            HojaExcel.Cell(i, 12).Value = listap.UP9.ToString();
                            HojaExcel.Cell(i, 12).Style.NumberFormat.Format = "#,##0.00";
                            HojaExcel.Cell(i, 12).DataType = XLDataType.Number;

                            HojaExcel.Cell(i, 13).Value = listap.UP10.ToString();
                            HojaExcel.Cell(i, 13).Style.NumberFormat.Format = "#,##0.00";
                            HojaExcel.Cell(i, 13).DataType = XLDataType.Number;

                            HojaExcel.Cell(i, 14).Value = listap.UP11.ToString();
                            HojaExcel.Cell(i, 14).Style.NumberFormat.Format = "#,##0.00";
                            HojaExcel.Cell(i, 14).DataType = XLDataType.Number;

                            HojaExcel.Cell(i, 15).Value = listap.UP12.ToString();
                            HojaExcel.Cell(i, 15).Style.NumberFormat.Format = "#,##0.00";
                            HojaExcel.Cell(i, 15).DataType = XLDataType.Number;

                            HojaExcel.Range("P23").FormulaA1 = "=D23+E23+F23+G23+H23+I23+J23+K23+L23+M23+N23+O23";
                            HojaExcel.Cell(i, 16).Style.NumberFormat.Format = "#,##0.00";
                            HojaExcel.Cell(i, 16).DataType = XLDataType.Number;


                            //HojaExcel.Cell(3 + i, segundoRango).Value = lista2[i].Analisis > 0 ? lista2[i].Analisis.ToString() : "";
                            //HojaExcel.Cell(3 + i, segundoRango).Style.Fill.BackgroundColor = XLColor.White;
                            //HojaExcel.Cell(3 + i, segundoRango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            //HojaExcel.Cell(3 + i, segundoRango).Style.NumberFormat.Format = "$#,##0.00";
                            //HojaExcel.Cell(3 + i, segundoRango).DataType = XLDataType.Number;

                            #endregion datos


                        }


                        //agrego renglón de presupuestos
                        i = 26;

                        HojaExcel.Range("C27", "O26").Style.Fill.BackgroundColor = XLColor.BabyBlue;

                        foreach (ComisionesPresupuesto listap in ListReal)
                        {

                            #region agrego los datos 
                            //Renglón i   



                            HojaExcel.Cell(i, 4).Value = listap.UP1.ToString();
                            HojaExcel.Cell(i, 4).Style.NumberFormat.Format = "#,##0.00";
                            HojaExcel.Cell(i, 4).DataType = XLDataType.Number;

                            HojaExcel.Cell(i, 5).Value = listap.UP2.ToString();
                            HojaExcel.Cell(i, 5).Style.NumberFormat.Format = "#,##0.00";
                            HojaExcel.Cell(i, 5).DataType = XLDataType.Number;


                            HojaExcel.Cell(i, 6).Value = listap.UP3.ToString();
                            HojaExcel.Cell(i, 6).Style.NumberFormat.Format = "#,##0.00";
                            HojaExcel.Cell(i, 6).DataType = XLDataType.Number;


                            HojaExcel.Cell(i, 7).Value = listap.UP4.ToString();
                            HojaExcel.Cell(i, 7).Style.NumberFormat.Format = "#,##0.00";
                            HojaExcel.Cell(i, 7).DataType = XLDataType.Number;

                            HojaExcel.Cell(i, 8).Value = listap.UP5.ToString();
                            HojaExcel.Cell(i, 8).Style.NumberFormat.Format = "#,##0.00";
                            HojaExcel.Cell(i, 8).DataType = XLDataType.Number;


                            HojaExcel.Cell(i, 9).Value = listap.UP6.ToString();
                            HojaExcel.Cell(i, 9).Style.NumberFormat.Format = "#,##0.00";
                            HojaExcel.Cell(i, 9).DataType = XLDataType.Number;


                            HojaExcel.Cell(i, 10).Value = listap.UP7.ToString();
                            HojaExcel.Cell(i, 10).Style.NumberFormat.Format = "#,##0.00";
                            HojaExcel.Cell(i, 10).DataType = XLDataType.Number;


                            HojaExcel.Cell(i, 11).Value = listap.UP8.ToString();
                            HojaExcel.Cell(i, 11).Style.NumberFormat.Format = "#,##0.00";
                            HojaExcel.Cell(i, 11).DataType = XLDataType.Number;

                            HojaExcel.Cell(i, 12).Value = listap.UP9.ToString();
                            HojaExcel.Cell(i, 12).Style.NumberFormat.Format = "#,##0.00";
                            HojaExcel.Cell(i, 12).DataType = XLDataType.Number;

                            HojaExcel.Cell(i, 13).Value = listap.UP10.ToString();
                            HojaExcel.Cell(i, 13).Style.NumberFormat.Format = "#,##0.00";
                            HojaExcel.Cell(i, 13).DataType = XLDataType.Number;

                            HojaExcel.Cell(i, 14).Value = listap.UP11.ToString();
                            HojaExcel.Cell(i, 14).Style.NumberFormat.Format = "#,##0.00";
                            HojaExcel.Cell(i, 14).DataType = XLDataType.Number;

                            HojaExcel.Cell(i, 15).Value = listap.UP12.ToString();
                            HojaExcel.Cell(i, 15).Style.NumberFormat.Format = "#,##0.00";
                            HojaExcel.Cell(i, 15).DataType = XLDataType.Number;

                            HojaExcel.Range("P26").FormulaA1 = "=D26+E26+F26+G26+H26+I26+J26+K26+L26+M26+N26+O26";
                            HojaExcel.Cell(i, 16).Style.NumberFormat.Format = "#,##0.00";
                            HojaExcel.Cell(i, 16).DataType = XLDataType.Number;


                            //HojaExcel.Cell(3 + i, segundoRango).Value = lista2[i].Analisis > 0 ? lista2[i].Analisis.ToString() : "";
                            //HojaExcel.Cell(3 + i, segundoRango).Style.Fill.BackgroundColor = XLColor.White;
                            //HojaExcel.Cell(3 + i, segundoRango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            //HojaExcel.Cell(3 + i, segundoRango).Style.NumberFormat.Format = "$#,##0.00";
                            //HojaExcel.Cell(3 + i, segundoRango).DataType = XLDataType.Number;

                            #endregion datos


                        }


                        HojaExcel.Columns().AdjustToContents();

                        HojaExcel.Column(13).Width = 12;
                        HojaExcel.Column(6).Width = 12;
                        HojaExcel.Column(2).Width = 3.29;
                        HojaExcel.Column(9).Width = 12.7;

                        string rutaguardado = Server.MapPath("~/Reportes/") + "ReportePreciador_" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";


                        if (File.Exists(rutaguardado))
                        {
                            File.Delete(rutaguardado);
                        }

                        workbook.SaveAs(rutaguardado);

                        workbook.SaveAs(rutaguardado);

                        string Outgoingfile = "ReportePresupuesto_" + nombre + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";
                        string ruta = Server.MapPath("~/Reportes/") + "ReportePresupuesto_" + nombre + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";
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
                else
                {

                    mensajes("Debe seleccionar un Representante.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        #endregion exportar excell


        #endregion


        protected void btnBuscar_Click(object sender, EventArgs e)
        {

            List<ComisionesPresupuesto> List = new List<ComisionesPresupuesto>();
            DataTable dt = new DataTable();

            if (cmbRik.SelectedIndex > 0)
            {

                ListVertical = null;

                grVertical.DataSource = null;
                grVertical.DataBind();

                grVertical.DataSource = GetListVertical();
                grVertical.DataBind();



                grdHorizontal.DataSource = null;
                grdHorizontal.DataBind();

                grdHorizontal.DataSource = GetList();
                grdHorizontal.DataBind();

                grdRealUp.DataSource = null;
                grdRealUp.DataBind();

                grdRealUp.DataSource = GetListUPReal();
                grdRealUp.DataBind();
            }
            else
            {
                mensajes("Debe seleccionar un Representante.");
            }
        }



    }
}