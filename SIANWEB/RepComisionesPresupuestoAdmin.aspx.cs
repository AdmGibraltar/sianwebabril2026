using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.UI;
using CapaEntidad;
using CapaNegocios;
using DevExpress.Export;
using DevExpress.Web;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraPrinting;


namespace SIANWEB
{
    public partial class RepComisionesPresupuestoAdmin : System.Web.UI.Page
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
        public event CustomColumnDisplayTextEventHandler CustomColumnDisplayText;

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

                    //Sesion.Id_Rik = 453;



                    //if (Sesion.Id_TU == 2)
                    //{
                    //    this.HF_Rik.Value = "1";

                    //}
                    //else
                    //{
                    //    this.HF_Rik.Value = "0";
                    //}



                    if (!Page.IsPostBack)
                    {

                        this.ValidarPermisos();
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            //RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }

                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_Cve.Value = randObj.Next().ToString();


                        //CapaNegocios.CN__Comun CN_Comun2 = new CapaNegocios.CN__Comun();
                        //CN_Comun2.LlenaCombo(1, Sesion.Emp_Cnx, "SpCatCdi_Combo", ref cmbAnio);


                        this.TblEncabezado.Visible = false;
                        LoadModulosActivos();


                    }

                }
                if (ASPxWebControl.GlobalTheme.Contains("Metropolis"))
                    ASPxSplitter1.Styles.Pane.CssClass = "metropolisPane";
                else if (ASPxWebControl.GlobalTheme.Contains("Youthful"))
                    ASPxSplitter1.Styles.Pane.CssClass = "youthfulPane";
                else
                    ASPxSplitter1.Styles.Pane.CssClass = string.Empty;
                //fin panel movible 


                grVertical.CustomColumnDisplayText += grdHorizontal_CustomColumnDisplayText;
                ASPxGridView1.CustomColumnDisplayText += grdHorizontal_CustomColumnDisplayText;



            }
            catch (Exception ex)
            {
                mensaje(string.Concat(ex.Message, "Page_Load_error"));
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
            else
            {
                List = GetListVertical();
                DataTable dt = new DataTable();

                dt = Funcion.Convertidor<ComisionesPresupuesto>.ListaToDatatable(List);
                grVertical.DataSource = dt;
                grVertical.DataBind();


            }

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





                ListVertical = list;
                Session["PresupuestosConsultar" + Session.SessionID] = list;


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


                if (session.Id_Rik > 0)
                {
                    presupuesto.Id_Rik = session.Id_Rik;
                }

                //session.Id_Rik = 453;


                clsComisionesPresupuesto.CapComisionesPresupuestoVertical_List(presupuesto, Emp_CnxCentral, ref list);

                ListVertical = list;
                Session["PresupuestosConsultarVertical" + Session.SessionID] = list;

                return list;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// FUNCION QUE CARGA LOS DATOS DE LA GRAFICA
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private List<ComisionesPresupuesto> GetListGrafica()
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


                if (session.Id_Rik > 0)
                {
                    presupuesto.Id_Rik = session.Id_Rik;
                }

                //session.Id_Rik = 453;


                clsComisionesPresupuesto.CapComisionesPresupuestoVertical_List(presupuesto, Emp_CnxCentral, ref list);

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
                lblMensaje.Text = "Error al cargar la lista de Empresas.";
                lblMensaje.Visible = true;
            }
        }


        #endregion


        #region Eventos del Grid

        private void mensaje(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "modalMensaje('" + mensaje + "')", true);
        }

        #region exportar a excell vertical


        protected void BootBtnVerticalExportar_Click(object sender, EventArgs e)
        {

            grVerticalReporter.WriteXlsxToResponse(new XlsxExportOptionsEx { ExportType = ExportType.WYSIWYG });

        }

        #endregion exportar excell


        #endregion


        #region Fomatear celdas en base a su nombre
        protected void grdHorizontal_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {

            switch (e.Column.FieldName)
            {
                case "Nom_Empleado":
                    e.DisplayText = FormatPrecio(e.Value);
                    e.Column.Width = 400;
                    break;
                case "Id_Rik":
                    e.Column.Width = 80;
                    break;
                case "Anio":
                    e.DisplayText = FormatPrecio(e.Value);
                    e.Column.Width = 80;
                    break;
                case "Venta":
                    e.DisplayText = FormatPrecio(e.Value);
                    e.Column.Width = 100;
                    break;
                case "Base":
                    e.DisplayText = FormatPrecio(e.Value);
                    break;
                case "BaseUP":
                    e.DisplayText = FormatPrecio(e.Value);
                    break;
                case "BaseUp_Porc":
                    e.DisplayText = FormatPrecio(e.Value);
                    break;
                case "UP":
                    e.DisplayText = FormatPrecio(e.Value);
                    break;
                case "UP_Porc":
                    e.DisplayText = FormatPorcentaje(e.Value);
                    break;

                case "UP_Porc1":
                    e.DisplayText = FormatPorcentaje(e.Value);
                    break;
                case "Venta1":
                    e.DisplayText = FormatPrecio(e.Value);
                    e.Column.Width = 100;
                    break;
                case "UP1":
                    e.DisplayText = FormatPrecio(e.Value);
                    break;

                case "Venta2":
                    e.DisplayText = FormatPrecio(e.Value);
                    e.Column.Width = 100;
                    break;
                case "UP2":
                    e.DisplayText = FormatPrecio(e.Value);
                    e.Column.Width = 100;
                    break;
                case "UP_Porc2":
                    e.DisplayText = FormatPorcentaje(e.Value);
                    break;
                case "Venta3":
                    e.DisplayText = FormatPrecio(e.Value);
                    e.Column.Width = 100;
                    break;
                case "UP3":
                    e.DisplayText = FormatPrecio(e.Value);
                    break;
                case "UP_Porc3":
                    e.DisplayText = FormatPorcentaje(e.Value);
                    break;
                case "Venta4":
                    e.DisplayText = FormatPrecio(e.Value);
                    e.Column.Width = 100;
                    break;
                case "UP4":
                    e.DisplayText = FormatPrecio(e.Value);
                    break;
                case "UP_Porc4":
                    e.DisplayText = FormatPorcentaje(e.Value);
                    break;
                case "Venta5":
                    e.DisplayText = FormatPrecio(e.Value);
                    e.Column.Width = 100;
                    break;
                case "UP5":
                    e.DisplayText = FormatPrecio(e.Value);
                    break;
                case "UP_Porc5":
                    e.DisplayText = FormatPorcentaje(e.Value);
                    break;
                case "Venta6":
                    e.DisplayText = FormatPrecio(e.Value);
                    e.Column.Width = 100;
                    break;
                case "UP6":
                    e.DisplayText = FormatPrecio(e.Value);
                    break;
                case "UP_Porc6":
                    e.DisplayText = FormatPorcentaje(e.Value);
                    break;
                case "Venta7":
                    e.DisplayText = FormatPrecio(e.Value);
                    e.Column.Width = 100;
                    break;
                case "UP7":
                    e.DisplayText = FormatPrecio(e.Value);
                    break;
                case "UP_Porc7":
                    e.DisplayText = FormatPorcentaje(e.Value);
                    break;
                case "Venta8":
                    e.DisplayText = FormatPrecio(e.Value);
                    e.Column.Width = 100;
                    break;
                case "UP8":
                    e.DisplayText = FormatPrecio(e.Value);
                    break;
                case "UP_Porc8":
                    e.DisplayText = FormatPorcentaje(e.Value);
                    break;
                case "UP_Porc9":
                    e.DisplayText = FormatPorcentaje(e.Value);
                    break;
                case "Venta9":
                    e.DisplayText = FormatPrecio(e.Value);
                    e.Column.Width = 100;
                    break;
                case "UP9":
                    e.DisplayText = FormatPrecio(e.Value);
                    break;
                case "UP_Porc10":
                    e.DisplayText = FormatPorcentaje(e.Value);
                    break;
                case "Venta10":
                    e.DisplayText = FormatPrecio(e.Value);
                    break;
                case "UP10":
                    e.DisplayText = FormatPrecio(e.Value);
                    break;
                case "UP_Porc11":
                    e.DisplayText = FormatPorcentaje(e.Value);
                    break;
                case "Venta11":
                    e.DisplayText = FormatPrecio(e.Value);
                    e.Column.Width = 100;
                    break;
                case "UP11":
                    e.DisplayText = FormatPrecio(e.Value);
                    break;
                case "UP_Porc12":
                    e.DisplayText = FormatPorcentaje(e.Value);
                    break;
                case "Venta12":
                    e.DisplayText = FormatPrecio(e.Value);
                    e.Column.Width = 100;
                    break;
                case "UP12":
                    e.DisplayText = FormatPrecio(e.Value);
                    break;

                case "Real1":
                    e.DisplayText = FormatPrecio(e.Value);
                    e.Column.Width = 100;
                    break;

                case "Real2":
                    e.DisplayText = FormatPrecio(e.Value);
                    e.Column.Width = 100;
                    break;

                case "Real3":
                    e.DisplayText = FormatPrecio(e.Value);
                    e.Column.Width = 100;
                    break;

                case "Real4":
                    e.DisplayText = FormatPrecio(e.Value);
                    e.Column.Width = 100;
                    break;

                case "Real5":
                    e.DisplayText = FormatPrecio(e.Value);
                    e.Column.Width = 100;
                    break;

                case "Real6":
                    e.DisplayText = FormatPrecio(e.Value);
                    e.Column.Width = 100;
                    break;

                case "Real7":
                    e.DisplayText = FormatPrecio(e.Value);
                    e.Column.Width = 100;
                    break;

                case "Real8":
                    e.DisplayText = FormatPrecio(e.Value);
                    e.Column.Width = 100;
                    break;

                case "Real9":
                    e.DisplayText = FormatPrecio(e.Value);
                    e.Column.Width = 100;
                    break;

                case "Real10":
                    e.DisplayText = FormatPrecio(e.Value);
                    e.Column.Width = 100;
                    break;

                case "Real11":
                    e.DisplayText = FormatPrecio(e.Value);
                    e.Column.Width = 100;
                    break;

                case "Real12":
                    e.DisplayText = FormatPrecio(e.Value);
                    e.Column.Width = 100;
                    break;

            }
        }
        private string FormatPorcentaje(object val)
        {
            return string.Format("{0:p2}", val);
        }
        private string FormatPrecio(object val)
        {
            return string.Format("{0:c2}", val);
        }
        #endregion


        #region ACTUALIZA LA GRAFICA

        ////Actauliza la gráfica con el contenido de la gráfica 

        /// <summary>
        /// FUNCION QUE AL ENTRAR LLENA LA GRÄFICA
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        protected void ASPxGridView_DataBound(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                BootstrapChart1.DataSource = GetBootstrapChartData();
                BootstrapChart1.DataBind();

            }
        }


        /// <summary>
        /// FUNCION QUE LLENA LA GRAFICA
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        protected void ASPxCallbackPanel_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            BootstrapChart1.DataSource = GetBootstrapChartData();
            BootstrapChart1.DataBind();

        }

        #region comentar llenado del grid anterior
        //protected BootstrapChartDataItem[] GetBootstrapChartData()
        //{
        //    DataRow gridDataRow;

        //    BootstrapChartDataItem[] BootstrapChartDataItems = new BootstrapChartDataItem[ASPxGridView1.VisibleRowCount];


        //    if (!IsPostBack)
        //    {
        //     //   ASPxGridView1.DataBind();

        //        for (int i = 0; i < ASPxGridView1.VisibleRowCount; i++)
        //        {
        //            gridDataRow = (ASPxGridView1.GetRow(i) as DataRowView).Row;
        //            BootstrapChartDataItems[i] = new BootstrapChartDataItem(gridDataRow.Field<string>("Nom_Empleado"), gridDataRow.Field<decimal>("Venta1"), gridDataRow.Field<decimal>("Real1"), gridDataRow.Field<decimal>("Venta2"), gridDataRow.Field<decimal>("Real2"), gridDataRow.Field<decimal>("Venta3"), gridDataRow.Field<decimal>("Real3"),
        //                gridDataRow.Field<decimal>("Venta4"), gridDataRow.Field<decimal>("Real4"), gridDataRow.Field<decimal>("Venta5"), gridDataRow.Field<decimal>("Real5"), gridDataRow.Field<decimal>("Venta6"), gridDataRow.Field<decimal>("Real6") );
        //                //, gridDataRow.Field<decimal>("Venta7"), gridDataRow.Field<decimal>("Real7"), gridDataRow.Field<decimal>("Venta8"), gridDataRow.Field<decimal>("Real8"), gridDataRow.Field<decimal>("Venta9"), gridDataRow.Field<decimal>("Real9")
        //                //, gridDataRow.Field<decimal>("Venta10"), gridDataRow.Field<decimal>("Real10"), gridDataRow.Field<decimal>("Venta11"), gridDataRow.Field<decimal>("Real11"), gridDataRow.Field<decimal>("Venta12"), gridDataRow.Field<decimal>("Real12"));
        //        }


        //    }
        //    else
        //    {

        //        int rik = Convert.ToInt32(tbNumRepresentante.Text);

        //        for (int i = 0; i < ASPxGridView1.VisibleRowCount; i++)
        //        {

        //            gridDataRow = (ASPxGridView1.GetRow(i) as DataRowView).Row;
        //            if ( rik == gridDataRow.Field<int>("Id_Rik"))
        //            {

        //            BootstrapChartDataItems[i] = new BootstrapChartDataItem(gridDataRow.Field<string>("Nom_Empleado"), gridDataRow.Field<decimal>("Venta1"), gridDataRow.Field<decimal>("Real1"), gridDataRow.Field<decimal>("Venta2"), gridDataRow.Field<decimal>("Real2"), gridDataRow.Field<decimal>("Venta3"), gridDataRow.Field<decimal>("Real3"),
        //                gridDataRow.Field<decimal>("Venta4"), gridDataRow.Field<decimal>("Real4"), gridDataRow.Field<decimal>("Venta5"), gridDataRow.Field<decimal>("Real5"), gridDataRow.Field<decimal>("Venta6"), gridDataRow.Field<decimal>("Real6"));
        //                //, gridDataRow.Field<decimal>("Venta7"), gridDataRow.Field<decimal>("Real7"), gridDataRow.Field<decimal>("Venta8"), gridDataRow.Field<decimal>("Real8"), gridDataRow.Field<decimal>("Venta9"), gridDataRow.Field<decimal>("Real9")
        //                //, gridDataRow.Field<decimal>("Venta10"), gridDataRow.Field<decimal>("Real10"), gridDataRow.Field<decimal>("Venta11"), gridDataRow.Field<decimal>("Real11"), gridDataRow.Field<decimal>("Venta12"), gridDataRow.Field<decimal>("Real12"));
        //            }
        //        }

        //    }


        //    return BootstrapChartDataItems;
        //}
        #endregion comentar llenado del grid anterior
        protected BootstrapChartDataItem[] GetBootstrapChartData()
        {
            DataRow gridDataRow;

            BootstrapChartDataItem[] BootstrapChartDataItems = new BootstrapChartDataItem[ASPxGridView1.VisibleRowCount * 12];


            if (!IsPostBack)
            {
                //   ASPxGridView1.DataBind();

                for (int i = 0; i < ASPxGridView1.VisibleRowCount; i++)
                {

                    gridDataRow = (ASPxGridView1.GetRow(i) as DataRowView).Row;
                    //ri ++;
                    //BootstrapChartDataItems[ri] = new BootstrapChartDataItem(gridDataRow.Field<string>("Nom_Empleado"), "Presupuesto", gridDataRow.Field<decimal>("Venta1"),  gridDataRow.Field<decimal>("Venta2"),  gridDataRow.Field<decimal>("Venta3"),
                    //    gridDataRow.Field<decimal>("Venta4"), gridDataRow.Field<decimal>("Venta5"),  gridDataRow.Field<decimal>("Venta6"));
                    //ri++;
                    //BootstrapChartDataItems[ri] = new BootstrapChartDataItem(gridDataRow.Field<string>("Nom_Empleado"), "Venta", gridDataRow.Field<decimal>("Real1"), gridDataRow.Field<decimal>("Real2"), gridDataRow.Field<decimal>("Real3"),
                    //    gridDataRow.Field<decimal>("Real4"),  gridDataRow.Field<decimal>("Real5"),  gridDataRow.Field<decimal>("Real6"));
                    int ri = 0;

                    BootstrapChartDataItems[ri] = new BootstrapChartDataItem(gridDataRow.Field<string>("Nom_Empleado"), "Ene", gridDataRow.Field<decimal>("Venta1"), gridDataRow.Field<decimal>("Real1"));
                    ri++;
                    BootstrapChartDataItems[ri] = new BootstrapChartDataItem(gridDataRow.Field<string>("Nom_Empleado"), "Feb", gridDataRow.Field<decimal>("Venta2"), gridDataRow.Field<decimal>("Real2"));
                    ri++;
                    BootstrapChartDataItems[ri] = new BootstrapChartDataItem(gridDataRow.Field<string>("Nom_Empleado"), "Mzo", gridDataRow.Field<decimal>("Venta3"), gridDataRow.Field<decimal>("Real3"));
                    ri++;
                    BootstrapChartDataItems[ri] = new BootstrapChartDataItem(gridDataRow.Field<string>("Nom_Empleado"), "Abr", gridDataRow.Field<decimal>("Venta4"), gridDataRow.Field<decimal>("Real4"));
                    ri++;
                    BootstrapChartDataItems[ri] = new BootstrapChartDataItem(gridDataRow.Field<string>("Nom_Empleado"), "May", gridDataRow.Field<decimal>("Venta5"), gridDataRow.Field<decimal>("Real5"));
                    ri++;
                    BootstrapChartDataItems[ri] = new BootstrapChartDataItem(gridDataRow.Field<string>("Nom_Empleado"), "Jun", gridDataRow.Field<decimal>("Venta6"), gridDataRow.Field<decimal>("Real6"));
                    ri++;
                    BootstrapChartDataItems[ri] = new BootstrapChartDataItem(gridDataRow.Field<string>("Nom_Empleado"), "Jul", gridDataRow.Field<decimal>("Venta7"), gridDataRow.Field<decimal>("Real7"));
                    ri++;
                    BootstrapChartDataItems[ri] = new BootstrapChartDataItem(gridDataRow.Field<string>("Nom_Empleado"), "Ago", gridDataRow.Field<decimal>("Venta8"), gridDataRow.Field<decimal>("Real8"));
                    ri++;
                    BootstrapChartDataItems[ri] = new BootstrapChartDataItem(gridDataRow.Field<string>("Nom_Empleado"), "Sep", gridDataRow.Field<decimal>("Venta9"), gridDataRow.Field<decimal>("Real9"));
                    ri++;
                    BootstrapChartDataItems[ri] = new BootstrapChartDataItem(gridDataRow.Field<string>("Nom_Empleado"), "Oct", gridDataRow.Field<decimal>("Venta10"), gridDataRow.Field<decimal>("Real10"));
                    ri++;
                    BootstrapChartDataItems[ri] = new BootstrapChartDataItem(gridDataRow.Field<string>("Nom_Empleado"), "Nov", gridDataRow.Field<decimal>("Venta11"), gridDataRow.Field<decimal>("Real11"));
                    ri++;
                    BootstrapChartDataItems[ri] = new BootstrapChartDataItem(gridDataRow.Field<string>("Nom_Empleado"), "Dic", gridDataRow.Field<decimal>("Venta12"), gridDataRow.Field<decimal>("Real12"));

                }
            }
            else
            {
                int rik = Convert.ToInt32(tbNumRepresentante.Text);

                for (int i = 0; i < ASPxGridView1.VisibleRowCount; i++)
                {
                    gridDataRow = (ASPxGridView1.GetRow(i) as DataRowView).Row;
                    if (rik == gridDataRow.Field<int>("Id_Rik"))
                    {
                        //BootstrapChartDataItems[i] = new BootstrapChartDataItem(gridDataRow.Field<string>("Nom_Empleado"), gridDataRow.Field<decimal>("Venta1"), gridDataRow.Field<decimal>("Real1"), gridDataRow.Field<decimal>("Venta1"), gridDataRow.Field<decimal>("Real2"), gridDataRow.Field<decimal>("Venta3"), gridDataRow.Field<decimal>("Real3"),
                        //    gridDataRow.Field<decimal>("Venta4"), gridDataRow.Field<decimal>("Real4"), gridDataRow.Field<decimal>("Venta5"), gridDataRow.Field<decimal>("Real5"), gridDataRow.Field<decimal>("Venta6"), gridDataRow.Field<decimal>("Real6"));
                        ////, gridDataRow.Field<decimal>("Venta7"), gridDataRow.Field<decimal>("Real7"), gridDataRow.Field<decimal>("Venta8"), gridDataRow.Field<decimal>("Real8"), gridDataRow.Field<decimal>("Venta9"), gridDataRow.Field<decimal>("Real9")
                        ////, gridDataRow.Field<decimal>("Venta10"), gridDataRow.Field<decimal>("Real10"), gridDataRow.Field<decimal>("Venta11"), gridDataRow.Field<decimal>("Real11"), gridDataRow.Field<decimal>("Venta12"), gridDataRow.Field<decimal>("Real12"));

                        //gridDataRow = (ASPxGridView1.GetRow(i) as DataRowView).Row;
                        //ri2++;
                        //BootstrapChartDataItems[ri] = new BootstrapChartDataItem(gridDataRow.Field<string>("Nom_Empleado"), "Presupuesto", gridDataRow.Field<decimal>("Venta1"), gridDataRow.Field<decimal>("Venta2"), gridDataRow.Field<decimal>("Venta3"),
                        //    gridDataRow.Field<decimal>("Venta4"), gridDataRow.Field<decimal>("Venta5"), gridDataRow.Field<decimal>("Venta6"));
                        //ri2++;
                        //BootstrapChartDataItems[ri] = new BootstrapChartDataItem(gridDataRow.Field<string>("Nom_Empleado"), "Venta", gridDataRow.Field<decimal>("Real1"), gridDataRow.Field<decimal>("Real2"), gridDataRow.Field<decimal>("Real3"),
                        //    gridDataRow.Field<decimal>("Real4"), gridDataRow.Field<decimal>("Real5"), gridDataRow.Field<decimal>("Real6"));

                        int ri2 = 0;

                        BootstrapChartDataItems[ri2] = new BootstrapChartDataItem(gridDataRow.Field<string>("Nom_Empleado"), "Ene", gridDataRow.Field<decimal>("Venta1"), gridDataRow.Field<decimal>("Real1"));
                        ri2++;
                        BootstrapChartDataItems[ri2] = new BootstrapChartDataItem(gridDataRow.Field<string>("Nom_Empleado"), "Feb", gridDataRow.Field<decimal>("Venta2"), gridDataRow.Field<decimal>("Real2"));
                        ri2++;
                        BootstrapChartDataItems[ri2] = new BootstrapChartDataItem(gridDataRow.Field<string>("Nom_Empleado"), "Mzo", gridDataRow.Field<decimal>("Venta3"), gridDataRow.Field<decimal>("Real3"));
                        ri2++;
                        BootstrapChartDataItems[ri2] = new BootstrapChartDataItem(gridDataRow.Field<string>("Nom_Empleado"), "Abr", gridDataRow.Field<decimal>("Venta4"), gridDataRow.Field<decimal>("Real4"));
                        ri2++;
                        BootstrapChartDataItems[ri2] = new BootstrapChartDataItem(gridDataRow.Field<string>("Nom_Empleado"), "May", gridDataRow.Field<decimal>("Venta5"), gridDataRow.Field<decimal>("Real5"));
                        ri2++;
                        BootstrapChartDataItems[ri2] = new BootstrapChartDataItem(gridDataRow.Field<string>("Nom_Empleado"), "Jun", gridDataRow.Field<decimal>("Venta6"), gridDataRow.Field<decimal>("Real6"));
                        ri2++;
                        BootstrapChartDataItems[ri2] = new BootstrapChartDataItem(gridDataRow.Field<string>("Nom_Empleado"), "Jul", gridDataRow.Field<decimal>("Venta7"), gridDataRow.Field<decimal>("Real7"));
                        ri2++;
                        BootstrapChartDataItems[ri2] = new BootstrapChartDataItem(gridDataRow.Field<string>("Nom_Empleado"), "Ago", gridDataRow.Field<decimal>("Venta8"), gridDataRow.Field<decimal>("Real8"));
                        ri2++;
                        BootstrapChartDataItems[ri2] = new BootstrapChartDataItem(gridDataRow.Field<string>("Nom_Empleado"), "Sep", gridDataRow.Field<decimal>("Venta9"), gridDataRow.Field<decimal>("Real9"));
                        ri2++;
                        BootstrapChartDataItems[ri2] = new BootstrapChartDataItem(gridDataRow.Field<string>("Nom_Empleado"), "Oct", gridDataRow.Field<decimal>("Venta10"), gridDataRow.Field<decimal>("Real10"));
                        ri2++;
                        BootstrapChartDataItems[ri2] = new BootstrapChartDataItem(gridDataRow.Field<string>("Nom_Empleado"), "Nov", gridDataRow.Field<decimal>("Venta11"), gridDataRow.Field<decimal>("Real11"));
                        ri2++;
                        BootstrapChartDataItems[ri2] = new BootstrapChartDataItem(gridDataRow.Field<string>("Nom_Empleado"), "Dic", gridDataRow.Field<decimal>("Venta12"), gridDataRow.Field<decimal>("Real12"));

                    }
                }

            }


            return BootstrapChartDataItems;
        }


        #endregion


        protected void btnBuscar_Click(object sender, EventArgs e)
        {

            List<ComisionesPresupuesto> List = new List<ComisionesPresupuesto>();
            DataTable dt = new DataTable();

            ListVertical = null;



            //grdHorizontal.DataSource = null;
            //grdHorizontal.DataBind();

            //grdHorizontal.DataSource = GetList();
            //grdHorizontal.DataBind();


            grVertical.DataSource = null;
            grVertical.DataBind();

            grVertical.DataSource = GetListVertical();
            grVertical.DataBind();



            ASPxGridViewExporter gvExporter = new ASPxGridViewExporter();
            gvExporter.ID = "gridExport";
            gvExporter.GridViewID = "grdHorizontal";
            gvExporter.DataBind();


            ASPxGridViewExporter grVerticalReporter = new ASPxGridViewExporter();
            gvExporter.ID = "grVerticalReporter";
            gvExporter.GridViewID = "grVertical";
            gvExporter.DataBind();


        }


        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("Inicio.aspx", false);

        }






    }
}