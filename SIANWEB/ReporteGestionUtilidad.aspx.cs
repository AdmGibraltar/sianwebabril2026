using CapaEntidad;
using CapaNegocios;
using ClosedXML.Excel;
using DevExpress.Data;
using DevExpress.Export;
using DevExpress.Web;
using DevExpress.Web.Bootstrap;
using DevExpress.XtraGrid;
using DevExpress.XtraPrinting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIANWEB
{
    public partial class ReporteGestionUtilidad : System.Web.UI.Page
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

        #region CargaIniial

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["listaMultiplicador"] != null)
            {
                List<CatMultiplicador> lista = new List<CatMultiplicador>();
                lista = (List<CatMultiplicador>)Session["listaMultiplicador"];
                gridRepresentanteMP.DataSource = lista;
                gridRepresentanteMP.DataBind();
            }

            if (Session["listaVenta"] != null)
            {

                List<CatPresupuesto> lista2 = (List<CatPresupuesto>)Session["listaVenta"];
                grDetalle.DataSource = lista2;
                grDetalle.DataBind();
            }

            if (Session["listacategoria"] != null)
            {
                List<CatPresupuesto> lista3 = new List<CatPresupuesto>();
                lista3 = (List<CatPresupuesto>)Session["listacategoria"];
                grdCategoria.DataSource = lista3;
                grdCategoria.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["listaMultiplicador"] = null;
                gridRepresentanteMP.DataSource = null;
                gridRepresentanteMP.DataBind();

                Session["listaVenta"] = null;
                grDetalle.DataSource = null;
                grDetalle.DataBind();

                Session["listacategoria"] = null;
                grdCategoria.DataSource = null;
                grdCategoria.DataBind();

                Session["RikGU"] = null;
                Session["SUcrusalGU"] = null;

                ValidarSesion();
                //ValidarPermisos(); 
                Inicializar();
            }

            Session["activeMenu"] = 7;
        }



        #endregion

        #region Procesosgrd


        decimal totalcategoriaCliente = 0;
        decimal UPCliente = 0;
        decimal acystotalcategoriaCliente = 0;
        decimal acysUPCliente = 0;


        decimal ventaVNR = 0;
        decimal vnr = 0;

        protected void gridRepresentanteMP_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if (e.IsTotalSummary)
            {
                if ((e.Item as ASPxSummaryItem).Tag == "Ubreal")
                {

                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                    {
                        totalcategoriaCliente = 0;
                        UPCliente = 0;

                    }
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                    {
                        totalcategoriaCliente += Convert.ToDecimal(e.GetValue("venta").ToString().Trim());
                        UPCliente += Convert.ToDecimal(e.GetValue("utilidadBruta").ToString().Trim());

                    }
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        if (UPCliente == 0)
                        {
                            e.TotalValue = 0;
                        }
                        else
                        {
                            e.TotalValue = UPCliente > 0 ? UPCliente / totalcategoriaCliente : 0;
                        }
                    }
                }
                if ((e.Item as ASPxSummaryItem).Tag == "acysUbreal")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                    {
                        acystotalcategoriaCliente = 0;
                        acysUPCliente = 0;

                    }
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                    {
                        acystotalcategoriaCliente += Convert.ToDecimal(e.GetValue("AcysVenta").ToString().Trim());
                        acysUPCliente += Convert.ToDecimal(e.GetValue("AcysUP").ToString().Trim());

                    }
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        if (acysUPCliente == 0)
                        {
                            e.TotalValue = 0;
                        }
                        else
                        {
                            e.TotalValue = acysUPCliente / acystotalcategoriaCliente;
                        }
                    }
                }




                if ((e.Item as ASPxSummaryItem).Tag == "VNRPorc")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                    {
                        ventaVNR = 0;
                        vnr = 0;

                    }
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                    {
                        vnr += Convert.ToDecimal(e.GetValue("VNR").ToString().Trim());
                        ventaVNR += Convert.ToDecimal(e.GetValue("venta").ToString().Trim());

                    }
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        if (acysUPCliente == 0)
                        {
                            e.TotalValue = 0;
                        }
                        else
                        {
                            e.TotalValue = vnr > 0 ? vnr / ventaVNR : 0;
                        }
                    }
                }
            }
        }



        decimal totalcategoriaProducto = 0;
        decimal UPProducto = 0;
        decimal acystotalcategoriaProducto = 0;
        decimal acysUPProducto = 0;
        protected void grDetalleCategoria_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if (e.IsTotalSummary)
            {
                if ((e.Item as ASPxSummaryItem).Tag == "Ubreal")
                {

                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                    {
                        totalcategoriaProducto = 0;
                        UPProducto = 0;

                    }
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                    {
                        totalcategoriaProducto += Convert.ToDecimal(e.GetValue("venta").ToString().Trim());
                        UPProducto += Convert.ToDecimal(e.GetValue("utilidadBruta").ToString().Trim());

                    }
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        if (UPProducto == 0)
                        {
                            e.TotalValue = 0;
                        }
                        else
                        {
                            e.TotalValue = UPProducto > 0 ? UPProducto / totalcategoriaProducto : 0;
                        }
                    }
                }
                if ((e.Item as ASPxSummaryItem).Tag == "acysUbreal")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                    {
                        acystotalcategoriaProducto = 0;
                        acysUPProducto = 0;

                    }
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                    {
                        acystotalcategoriaProducto += Convert.ToDecimal(e.GetValue("AcysVenta").ToString().Trim());
                        acysUPProducto += Convert.ToDecimal(e.GetValue("AcysUP").ToString().Trim());

                    }
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        if (acysUPProducto == 0)
                        {
                            e.TotalValue = 0;
                        }
                        else
                        {
                            e.TotalValue = acysUPProducto > 0 ? acysUPProducto / acystotalcategoriaProducto : 0;
                        }
                    }
                }
            }
        }


        decimal totalcategoria = 0;
        decimal UP = 0;
        decimal acystotalcategoria = 0;
        decimal acysUP = 0;
        protected void grDetalleProducto_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if (e.IsTotalSummary)
            {
                if ((e.Item as ASPxSummaryItem).Tag == "Ubreal")
                {

                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                    {
                        totalcategoria = 0;
                        UP = 0;

                    }
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                    {
                        totalcategoria += Convert.ToDecimal(e.GetValue("venta").ToString().Trim());
                        UP += Convert.ToDecimal(e.GetValue("utilidadBruta").ToString().Trim());

                    }
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        if (UP == 0)
                        {
                            e.TotalValue = 0;
                        }
                        else
                        {
                            e.TotalValue = UP > 0 ? UP / totalcategoria : 0;
                        }
                    }
                }
                if ((e.Item as ASPxSummaryItem).Tag == "acysUbreal")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                    {
                        acystotalcategoria = 0;
                        acysUP = 0;

                    }
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                    {
                        acystotalcategoria += Convert.ToDecimal(e.GetValue("AcysVenta").ToString().Trim());
                        acysUP += Convert.ToDecimal(e.GetValue("AcysUP").ToString().Trim());

                    }
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        if (acysUP == 0)
                        {
                            e.TotalValue = 0;
                        }
                        else
                        {
                            e.TotalValue = acysUP > 0 ? acysUP / acystotalcategoria : 0;
                        }
                    }
                }
            }
        }

        decimal totalcategoriagral = 0;
        decimal UPCategoriagral = 0;

        decimal totalcategoriagralNacional = 0;
        decimal UPCategoriagralNacional = 0;

        decimal totalcategoriagralLocal = 0;
        decimal UPCategoriagralLocal = 0;


        decimal Prestotalcategoriagral = 0;
        decimal PresUPCategoriagral = 0;

        decimal PrestotalcategoriagralNacional = 0;
        decimal PresUPCategoriagralNacional = 0;

        decimal PrestotalcategoriagralLocal = 0;
        decimal PresUPCategoriagralLocal = 0;


        protected void grdCategoria_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            if (e.IsTotalSummary)
            {
                if ((e.Item as ASPxSummaryItem).Tag == "porcubreal")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                    {
                        totalcategoriagral = 0;
                        UPCategoriagral = 0;

                    }
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                    {
                        if (Convert.ToDecimal(e.GetValue("venta").ToString().Trim()) < 0)
                        {
                            totalcategoriagral += (Convert.ToDecimal(e.GetValue("venta").ToString().Trim()) * -1);
                        }
                        else
                        {
                            totalcategoriagral += Convert.ToDecimal(e.GetValue("venta").ToString().Trim());
                        }

                        if (Convert.ToDecimal(e.GetValue("utilidadBruta").ToString().Trim()) < 0)
                        {
                            UPCategoriagral += (Convert.ToDecimal(e.GetValue("utilidadBruta").ToString().Trim()) * -1);
                        }
                        else
                        {
                            UPCategoriagral += Convert.ToDecimal(e.GetValue("utilidadBruta").ToString().Trim());
                        }
                    }
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        if (totalcategoriagral == 0)
                        {
                            e.TotalValue = 0;
                        }
                        else
                        {
                            e.TotalValue = UPCategoriagral > 0 ? UPCategoriagral / totalcategoriagral : 0;
                        }
                    }
                }

                if ((e.Item as ASPxSummaryItem).Tag == "porcubrealNacional")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                    {
                        totalcategoriagralNacional = 0;
                        UPCategoriagralNacional = 0;

                    }
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                    {
                        if (Convert.ToDecimal(e.GetValue("ventaNacional").ToString().Trim()) < 0)
                        {
                            totalcategoriagralNacional += (Convert.ToDecimal(e.GetValue("ventaNacional").ToString().Trim()) * -1);
                        }
                        else
                        {
                            totalcategoriagralNacional += Convert.ToDecimal(e.GetValue("ventaNacional").ToString().Trim());
                        }

                        if (Convert.ToDecimal(e.GetValue("utilidadBrutaNacional").ToString().Trim()) < 0)
                        {
                            UPCategoriagralNacional += (Convert.ToDecimal(e.GetValue("utilidadBrutaNacional").ToString().Trim()) * -1);
                        }
                        else
                        {
                            UPCategoriagralNacional += Convert.ToDecimal(e.GetValue("utilidadBrutaNacional").ToString().Trim());
                        }
                    }
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        if (totalcategoriagralNacional == 0)
                        {
                            e.TotalValue = 0;
                        }
                        else
                        {
                            e.TotalValue = UPCategoriagralNacional > 0 ? UPCategoriagralNacional / totalcategoriagralNacional : 0;
                        }
                    }
                }

                if ((e.Item as ASPxSummaryItem).Tag == "porcubrealLocal")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                    {
                        totalcategoriagralLocal = 0;
                        UPCategoriagralLocal = 0;

                    }
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                    {
                        if (Convert.ToDecimal(e.GetValue("ventaLocal").ToString().Trim()) < 0)
                        {
                            totalcategoriagralLocal += (Convert.ToDecimal(e.GetValue("ventaLocal").ToString().Trim()) * -1);
                        }
                        else
                        {
                            totalcategoriagralLocal += Convert.ToDecimal(e.GetValue("ventaLocal").ToString().Trim());
                        }

                        if (Convert.ToDecimal(e.GetValue("utilidadBrutaLocal").ToString().Trim()) < 0)
                        {
                            UPCategoriagralLocal += (Convert.ToDecimal(e.GetValue("utilidadBrutaLocal").ToString().Trim()) * -1);
                        }
                        else
                        {
                            UPCategoriagralLocal += Convert.ToDecimal(e.GetValue("utilidadBrutaLocal").ToString().Trim());
                        }

                    }
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        if (totalcategoriagralLocal == 0)
                        {
                            e.TotalValue = 0;
                        }
                        else
                        {
                            e.TotalValue = UPCategoriagralLocal > 0 ? UPCategoriagralLocal / totalcategoriagralLocal : 0;
                        }
                    }
                }

                if ((e.Item as ASPxSummaryItem).Tag == "PresupuestoporcubrealNacional")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                    {
                        Prestotalcategoriagral = 0;
                        PresUPCategoriagral = 0;

                    }
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                    {
                        if (Convert.ToDecimal(e.GetValue("PresupuestoventaNacional").ToString().Trim()) < 0)
                        {
                            Prestotalcategoriagral += (Convert.ToDecimal(e.GetValue("PresupuestoventaNacional").ToString().Trim()) * -1);
                        }
                        else
                        {
                            Prestotalcategoriagral += Convert.ToDecimal(e.GetValue("PresupuestoventaNacional").ToString().Trim());
                        }

                        if (Convert.ToDecimal(e.GetValue("PresupuestoutilidadBrutaNacional").ToString().Trim()) < 0)
                        {
                            PresUPCategoriagral += (Convert.ToDecimal(e.GetValue("PresupuestoutilidadBrutaNacional").ToString().Trim()) * -1);
                        }
                        else
                        {
                            PresUPCategoriagral += Convert.ToDecimal(e.GetValue("PresupuestoutilidadBrutaNacional").ToString().Trim());
                        }
                    }
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        if (Prestotalcategoriagral == 0)
                        {
                            e.TotalValue = 0;
                        }
                        else
                        {
                            e.TotalValue = PresUPCategoriagral > 0 ? PresUPCategoriagral / Prestotalcategoriagral : 0;
                        }
                    }
                }

                if ((e.Item as ASPxSummaryItem).Tag == "PresupuestoporcubrealLocal")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                    {
                        PrestotalcategoriagralNacional = 0;
                        PresUPCategoriagralNacional = 0;

                    }
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                    {
                        if (Convert.ToDecimal(e.GetValue("PresupuestoventaLocal").ToString().Trim()) < 0)
                        {
                            PrestotalcategoriagralNacional += (Convert.ToDecimal(e.GetValue("PresupuestoventaLocal").ToString().Trim()) * -1);
                        }
                        else
                        {
                            PrestotalcategoriagralNacional += Convert.ToDecimal(e.GetValue("PresupuestoventaLocal").ToString().Trim());
                        }

                        if (Convert.ToDecimal(e.GetValue("PresupuestoutilidadBrutaLocal").ToString().Trim()) < 0)
                        {
                            PresUPCategoriagralNacional += (Convert.ToDecimal(e.GetValue("PresupuestoutilidadBrutaLocal").ToString().Trim()) * -1);
                        }
                        else
                        {
                            PresUPCategoriagralNacional += Convert.ToDecimal(e.GetValue("PresupuestoutilidadBrutaLocal").ToString().Trim());
                        }
                    }
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        if (PrestotalcategoriagralNacional == 0)
                        {
                            e.TotalValue = 0;
                        }
                        else
                        {
                            e.TotalValue = PresUPCategoriagralNacional > 0 ? PresUPCategoriagralNacional / PrestotalcategoriagralNacional : 0;
                        }
                    }
                }

                if ((e.Item as ASPxSummaryItem).Tag == "Presupuestoporcubreal")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                    {
                        PrestotalcategoriagralLocal = 0;
                        PresUPCategoriagralLocal = 0;

                    }
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                    {
                        PrestotalcategoriagralLocal += Convert.ToDecimal(e.GetValue("Presupuestoventa").ToString().Trim());
                        PresUPCategoriagralLocal += Convert.ToDecimal(e.GetValue("PresupuestoutilidadBruta").ToString().Trim());

                    }
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        if (PrestotalcategoriagralLocal == 0)
                        {
                            e.TotalValue = 0;
                        }
                        else
                        {
                            e.TotalValue = PresUPCategoriagralLocal > 0 ? PresUPCategoriagralLocal / PrestotalcategoriagralLocal : 0;
                        }
                    }
                }
            }
        }

        protected void grDetalleCategoria_BeforePerformDataSelect(object sender, EventArgs e)
        {
            var prod = (sender as BootstrapGridView).GetMasterRowKeyValue();
            if (prod != null)
            {
                if (Session["camposCategoria"] == null)
                {
                    BootstrapGridView griddetalleCategoria = sender as BootstrapGridView;
                    string[] array = prod.ToString().Trim().Split('|');
                    List<CatPresupuesto> query = new List<CatPresupuesto>();
                    // array[0] = valor del campo id_cte
                    // array[1] = valor del campo id_Terr   
                    // array[2] = valor del campo FechaInicial
                    // array[3] = valor del campo fechafinal  
                    // array[4] = valor del campo Id_Cd
                    // array[5] = valor del campo Id_Rik  



                    string cliente = array[0].ToString().Trim();
                    string territorio = array[1].ToString().Trim();
                    DateTime fechaInicial = DateTime.Parse(array[2].ToString().Trim());
                    DateTime fechaFinal = DateTime.Parse(array[3].ToString().Trim());
                    int id_cd = int.Parse(array[4].ToString().Trim());
                    int Id_Rik = int.Parse(array[5].ToString().Trim());
                    CargarDatosCategoria(Convert.ToInt32(cliente), territorio, fechaInicial, fechaFinal, id_cd, Id_Rik, ref query);

                    Session["camposCategoria"] = query;
                    griddetalleCategoria.DataSource = query;
                    griddetalleCategoria.DataBind();
                }
                else
                {
                    Session["camposCategoria"] = null;
                }
            }
        }

        protected void grDetalleProducto_BeforePerformDataSelect(object sender, EventArgs e)
        {
            var prod = (sender as BootstrapGridView).GetMasterRowKeyValue();
            if (prod != null)
            {
                if (Session["camposCategoriaProdcuto"] == null)
                {
                    BootstrapGridView griddetalleProducto = sender as BootstrapGridView;
                    string[] array = prod.ToString().Trim().Split('|');
                    List<CatPresupuesto> query = new List<CatPresupuesto>();
                    // array[0] = valor del campo id_cte
                    // array[1] = valor del campo id_Terr   
                    // array[2] = valor del campo FechaInicial
                    // array[3] = valor del campo fechafinal  
                    // array[4] = valor del campo Id_Cd
                    // array[5] = valor del campo Id_Rik  
                    // array[6] = valor del campo Id_Cpr  


                    string cliente = array[0].ToString().Trim();
                    string territorio = array[1].ToString().Trim();
                    DateTime fechaInicial = DateTime.Parse(array[2].ToString().Trim());
                    DateTime fechaFinal = DateTime.Parse(array[3].ToString().Trim());
                    int id_cd = int.Parse(array[4].ToString().Trim());
                    int Id_Rik = int.Parse(array[5].ToString().Trim());
                    string Cpr_Descripcion = array[6].ToString().Trim();

                    CargarDatosProducto(Convert.ToInt32(cliente), territorio, fechaInicial, fechaFinal, id_cd, Id_Rik, Cpr_Descripcion, ref query);

                    Session["camposCategoriaProdcuto"] = query;
                    griddetalleProducto.DataSource = query;
                    griddetalleProducto.DataBind();
                }
                else
                {
                    Session["camposCategoriaProdcuto"] = null;
                }
            }
        }



        #endregion

        #region Procesos

        protected void FinishButtonClick(object sender, EventArgs e)
        {
            if (CmbSucursal.Value.ToString() != "-1")
            {
                CargarDatosGenerales();
            }
            else
            {
                mensaje("Favor de seleccionar la sucursal");
            }
        }

        protected void btnReporteCategoria_ServerClick(object sender, EventArgs e)
        {
            if (CmbSucursal.Value.ToString() != "-1")
            {
                CargarReporteCategoriaGenerales();
            }
            else
            {
                mensaje("Favor de seleccionar la sucursal");
            }
        }

        protected void btnReporteCiente_ServerClick(object sender, EventArgs e)
        {
            if (CmbSucursal.Value.ToString() != "-1")
            {
                CargarreporteVentaGenerales();
            }
            else
            {
                mensaje("Favor de seleccionar la sucursal");
            }
        }

        protected void bcmTipoReporte_SelectedIndexChanged(object sender, EventArgs e)
        {



            if (bcmTipoReporte.Value.ToString() == "2")
            {
                DateTime fechaInicial = DateTime.Parse("01/" + DateTime.Now.Month + "/" + DateTime.Now.Year);
                DateTime fechafinal = DateTime.Parse("01/" + DateTime.Now.Month + "/" + DateTime.Now.Year);

                fechaInicial = fechaInicial.AddMonths(-3);
                fechafinal = fechafinal.AddDays(-1);

                string fechaCierre = "Fecha del cierre del: " + fechaInicial.ToString("MMMM yyyy") + " al " + fechafinal.ToString("MMMM yyyy");
                lblFecha.Text = fechaCierre;
            }
            else
            {
                lblFecha.Text = "";
            }

            if (bcmTipoReporte.Value.ToString() == "3")
            {
                txtfechaIniciaGR.Value = DateTime.Now;
                txtfechaIniciaGR.Enabled = true;
            }
            else
            {
                txtfechaIniciaGR.Value = DateTime.Now;
                txtfechaIniciaGR.Enabled = false;
            }

            if (bcmTipoReporte.Value.ToString() == "4")
            {
                BcbTrimestreInicial.Enabled = true;
                txtfechaTrimestreInicial.Enabled = true;
            }
            else
            {
                BcbTrimestreInicial.Enabled = false;
                txtfechaTrimestreInicial.Enabled = false;
            }
        }

        #endregion

        #region Consultas

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

        private void Inicializar()
        {
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            if (sesion != null)
            {
                CargarTrimestre();
                CN_Comun.DevLlenaCombo(2, sesion.Id_Emp, sesion.Id_U, Emp_CnxCentral, "SP_CatCDI_combo2", ref CmbSucursal);
                CmbSucursal.Value = sesion.Id_Cd_Ver.ToString();
                CmbSucursal.Enabled = false;

                int id_Cd = Convert.ToInt32(sesion.Id_Cd_Ver.ToString());
                cargarRIk(id_Cd);

                txtfechaTrimestreInicial.Value = DateTime.Now;
                txtfechaIniciaGR.Value = DateTime.Now;
            }
            else
            {
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                Response.Redirect("login.aspx", false);


            }
        }

        private void CargarDatosGenerales()
        {
            ValidarSesion();
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            DateTime fechaInicial = sesion.CalendarioIni;
            DateTime fechafinal = sesion.CalendarioFin;
            if (bcmTipoReporte.Value.ToString() == "1")
            {
                fechaInicial = sesion.CalendarioIni;
                fechafinal = sesion.CalendarioFin;
            }
            else if (bcmTipoReporte.Value.ToString() == "2")
            {
                fechafinal = fechaInicial.AddDays(-1);
                fechaInicial = fechaInicial.AddMonths(-2);
            }
            else if (bcmTipoReporte.Value.ToString() == "3")
            {
                DateTime fecha1 = DateTime.Parse(txtfechaIniciaGR.Value.ToString());
                DateTime fecha2 = DateTime.Parse(txtfechaIniciaGR.Value.ToString()).AddMonths(1).AddDays(-1);

                fechaInicial = DateTime.Parse("01/" + fecha1.Month + "/" + fecha1.Year);
                fechafinal = DateTime.Parse("01/" + fecha2.Month + "/" + fecha2.Year).AddMonths(1).AddDays(-1);
            }
            else if (bcmTipoReporte.Value.ToString() == "4")
            {
                DateTime fechaInicialtrimestral = DateTime.Now;
                DateTime fechafinaltrimestral = DateTime.Now;
                if (BcbTrimestreInicial.Value.ToString() == "1")
                {
                    fechaInicialtrimestral = DateTime.Parse("01/01/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                    fechafinaltrimestral = DateTime.Parse("01/03/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                }
                if (BcbTrimestreInicial.Value.ToString() == "2")
                {
                    fechaInicialtrimestral = DateTime.Parse("01/04/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                    fechafinaltrimestral = DateTime.Parse("01/06/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                }
                if (BcbTrimestreInicial.Value.ToString() == "3")
                {
                    fechaInicialtrimestral = DateTime.Parse("01/07/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                    fechafinaltrimestral = DateTime.Parse("01/09/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                }
                if (BcbTrimestreInicial.Value.ToString() == "4")
                {
                    fechaInicialtrimestral = DateTime.Parse("01/10/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                    fechafinaltrimestral = DateTime.Parse("01/12/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                }


                fechaInicial = fechaInicialtrimestral;
                fechafinal = fechafinaltrimestral;
            }
            string mesAnioInicial = fechaInicial.ToString(); ;
            string mesAniofinal = fechafinal.ToString(); ;

            CN_Presupuesto cdpresupuesto = new CN_Presupuesto();
            CatPresupuesto Presupuesto = new CatPresupuesto();
            List<CatPresupuesto> ListaUtilidad = new List<CatPresupuesto>();
            List<CatPresupuesto> ListaUtilidadTotal = new List<CatPresupuesto>();
            CN_Presupuesto cnpresupuesto = new CN_Presupuesto();
            List<CatPresupuesto> ListaPresupuestoCliente = new List<CatPresupuesto>();
            List<CatPresupuesto> ListaPresupuestoCtipoCuenta = new List<CatPresupuesto>();

            List<CatPresupuesto> VentaNoRentable = new List<CatPresupuesto>();

            if (mesAnioInicial != "" && mesAniofinal != "")
            {
                #region Consulta
                Presupuesto.Id_Emp = sesion.Id_Emp;
                Presupuesto.Id_Cd = int.Parse(CmbSucursal.Value.ToString());
                Presupuesto.MesInicial = fechaInicial.Month;
                Presupuesto.AnioInicial = fechaInicial.Year;
                Presupuesto.MesFinal = fechafinal.Month;
                Presupuesto.AnioFinal = fechafinal.Year;

                string FechaInicialD = ("01" + "/" + fechaInicial.Month + "/" + fechaInicial.Year);
                string FechaFinalD = ("01" + "/" + fechafinal.Month + "/" + fechafinal.Year);

                if (Convert.ToDateTime(FechaInicialD) > Convert.ToDateTime(FechaFinalD))
                {
                    mensaje("La fecha inicial es mayor a la fecha final de la sección de observar totales.");
                    return;
                }

                Presupuesto.FechaInicial = fechaInicial;
                Presupuesto.fechafinal = fechafinal;
                Presupuesto.Id_Rik = Convert.ToInt32(cmbRikDI.Value.ToString());
                Presupuesto.Id_u = Convert.ToInt32(cmbRikDI.Value.ToString());

                cdpresupuesto.ConsultaUtilidadRIkxmesCliente(Presupuesto, ref ListaUtilidad, Emp_CnxCentral);
                cnpresupuesto.PresupuestoMensualRik(Presupuesto, ref ListaPresupuestoCliente, Emp_CnxCentral);
                cnpresupuesto.PresupuestoMensualCategoria(Presupuesto, ref ListaPresupuestoCtipoCuenta, Emp_CnxCentral);
                cnpresupuesto.ConsultaventanoRentable(Presupuesto, ref VentaNoRentable, Emp_CnxCentral);
                #endregion

                #region FiltradoXRik
                List<CatPresupuesto> ListaUtilidadrikNAcional = new List<CatPresupuesto>();
                List<CatPresupuesto> ListaUtilidadrikLocales = new List<CatPresupuesto>();
                List<CatPresupuesto> ListaUtilidadrik = new List<CatPresupuesto>();
                List<CatPresupuesto> lista = new List<CatPresupuesto>();

                List<CatPresupuesto> ListaPresupuestoNAcional = new List<CatPresupuesto>();
                List<CatPresupuesto> ListaPresupuestoLocales = new List<CatPresupuesto>();
                List<CatPresupuesto> ListaPresupuesto = new List<CatPresupuesto>();

                if (cmbRikDI.Value.ToString() != "-1")
                {
                    ListaUtilidadrikNAcional = (from tlist in ListaUtilidad
                                                where tlist.Id_Rik == Convert.ToInt32(cmbRikDI.Value.ToString())
                                                && tlist.id_matriz != 0
                                                select tlist).ToList();

                    ListaUtilidadrikLocales = (from tlist in ListaUtilidad
                                               where tlist.Id_Rik == Convert.ToInt32(cmbRikDI.Value.ToString())
                                                   && tlist.id_matriz == 0
                                               select tlist).ToList();


                    ListaPresupuestoLocales = (from tlist in ListaPresupuestoCtipoCuenta
                                               where tlist.Id_Rik == Convert.ToInt32(cmbRikDI.Value.ToString())
                                                && tlist.TipoCuenta.ToLower() == "local"
                                               select tlist).ToList();

                    ListaPresupuestoNAcional = (from tlist in ListaPresupuestoCtipoCuenta
                                                where tlist.Id_Rik == Convert.ToInt32(cmbRikDI.Value.ToString())
                                                 && tlist.TipoCuenta.ToLower() == "nacional"
                                                select tlist).ToList();


                    ListaPresupuesto = (from tlist in ListaPresupuestoCtipoCuenta
                                        where tlist.Id_Rik == Convert.ToInt32(cmbRikDI.Value.ToString())
                                        select tlist).ToList();


                    ListaUtilidadrik = (from tlist in ListaUtilidad
                                        where tlist.Id_Rik == Convert.ToInt32(cmbRikDI.Value.ToString())
                                        select tlist).ToList();

                    lista = (from tlist in ListaPresupuestoCliente
                             where tlist.Id_Rik == Convert.ToInt32(cmbRikDI.Value.ToString())
                             select tlist).ToList();
                }
                else
                {
                    ListaUtilidadrikNAcional = (from tlist in ListaUtilidad
                                                where tlist.id_matriz != 0
                                                select tlist).ToList();

                    ListaUtilidadrikLocales = (from tlist in ListaUtilidad
                                               where tlist.id_matriz == 0
                                               select tlist).ToList();


                    ListaPresupuestoLocales = (from tlist in ListaPresupuestoCtipoCuenta
                                               where tlist.TipoCuenta.ToLower() == "local"
                                               select tlist).ToList();

                    ListaPresupuestoNAcional = (from tlist in ListaPresupuestoCtipoCuenta
                                                where tlist.TipoCuenta.ToLower() == "nacional"
                                                select tlist).ToList();



                    ListaPresupuesto = (from tlist in ListaPresupuestoCtipoCuenta
                                        select tlist).ToList();

                    ListaUtilidadrik = (from tlist in ListaUtilidad
                                        select tlist).ToList();

                    lista = (from tlist in ListaPresupuestoCliente
                             select tlist).ToList();
                }
                #endregion

                #region ReporteGeneral
                List<CatMultiplicador> listaMultiplicadorquery = new List<CatMultiplicador>();
                CatMultiplicador Multiplicadorquery;

                #region CunetaNacional
                Multiplicadorquery = new CatMultiplicador();
                Multiplicadorquery.Cuenta = "Cuenta Nacionales";
                Multiplicadorquery.Id_Emp = sesion.Id_Emp;
                Multiplicadorquery.Id_Cd = int.Parse(CmbSucursal.Value.ToString());
                Multiplicadorquery.Sucursal = CmbSucursal.Text;
                Multiplicadorquery.Id_Rik = int.Parse(cmbRikDI.Value.ToString());
                Multiplicadorquery.NombreRik = cmbRikDI.Text;
                Multiplicadorquery.totalVenta = ListaUtilidadrikNAcional.Count > 0 ? ListaUtilidadrikNAcional.Sum(x => x.venta) : 0;
                Multiplicadorquery.UtilidadPrima = ListaUtilidadrikNAcional.Count > 0 ? ListaUtilidadrikNAcional.Sum(x => x.utilidadBruta) : 0;
                Multiplicadorquery.UtilidadPrimaPorc = (ListaUtilidadrikNAcional.Count > 0 ? ListaUtilidadrikNAcional.Sum(x => x.utilidadBruta) : 0) / (ListaUtilidadrikNAcional.Count > 0 ? ListaUtilidadrikNAcional.Sum(x => x.venta) : 0);
                Multiplicadorquery.TotalPresupuesto = ListaPresupuestoNAcional.Count > 0 ? ListaPresupuestoNAcional.Sum(x => x.TotalPresupuesto) : 0;
                Multiplicadorquery.UtilidadPrimaPres = ListaPresupuestoNAcional.Count > 0 ? Convert.ToDouble(ListaPresupuestoNAcional.Sum(x => x.up)) : 0;
                Multiplicadorquery.UtilidadPrimaPresPorc = Convert.ToDouble(ListaPresupuestoNAcional.Count > 0 ? ListaPresupuestoNAcional.Sum(x => x.up) : 0) / Convert.ToDouble(ListaPresupuestoNAcional.Count > 0 ? ListaPresupuestoNAcional.Sum(x => x.TotalPresupuesto) : 0);
                Multiplicadorquery.difVta = Multiplicadorquery.totalVenta - Convert.ToDouble(Multiplicadorquery.TotalPresupuesto);
                Multiplicadorquery.difup = Multiplicadorquery.UtilidadPrima - Multiplicadorquery.UtilidadPrimaPres;
                Multiplicadorquery.difpercup = Multiplicadorquery.UtilidadPrimaPorc - Multiplicadorquery.UtilidadPrimaPresPorc;
                Multiplicadorquery.VNR = 0;
                Multiplicadorquery.VNRPorc = 0;
                Multiplicadorquery.VNRpres = 0;

                listaMultiplicadorquery.Add(Multiplicadorquery);
                #endregion region

                #region CunetaLocales
                Multiplicadorquery = new CatMultiplicador();
                Multiplicadorquery.Cuenta = "Cuenta Locales";
                Multiplicadorquery.Id_Emp = sesion.Id_Emp;
                Multiplicadorquery.Id_Cd = int.Parse(CmbSucursal.Value.ToString());
                Multiplicadorquery.Sucursal = CmbSucursal.Text;
                Multiplicadorquery.Id_Rik = int.Parse(cmbRikDI.Value.ToString());
                Multiplicadorquery.NombreRik = cmbRikDI.Text;
                Multiplicadorquery.totalVenta = ListaUtilidadrikLocales.Count > 0 ? ListaUtilidadrikLocales.Sum(x => x.venta) : 0;
                Multiplicadorquery.UtilidadPrima = ListaUtilidadrikLocales.Count > 0 ? ListaUtilidadrikLocales.Sum(x => x.utilidadBruta) : 0;
                Multiplicadorquery.UtilidadPrimaPorc = (ListaUtilidadrikLocales.Count > 0 ? ListaUtilidadrikLocales.Sum(x => x.utilidadBruta) : 0) / (ListaUtilidadrikLocales.Count > 0 ? ListaUtilidadrikLocales.Sum(x => x.venta) : 0);
                Multiplicadorquery.TotalPresupuesto = ListaPresupuestoLocales.Count > 0 ? ListaPresupuestoLocales.Sum(x => x.TotalPresupuesto) : 0;
                Multiplicadorquery.UtilidadPrimaPres = ListaPresupuestoLocales.Count > 0 ? Convert.ToDouble(ListaPresupuestoLocales.Sum(x => x.up)) : 0;
                Multiplicadorquery.UtilidadPrimaPresPorc = Convert.ToDouble(ListaPresupuestoLocales.Count > 0 ? ListaPresupuestoLocales.Sum(x => x.up) : 0) / Convert.ToDouble(ListaPresupuestoLocales.Count > 0 ? ListaPresupuestoLocales.Sum(x => x.TotalPresupuesto) : 0);
                Multiplicadorquery.difVta = Multiplicadorquery.totalVenta - Convert.ToDouble(Multiplicadorquery.TotalPresupuesto);
                Multiplicadorquery.difup = Multiplicadorquery.UtilidadPrima - Multiplicadorquery.UtilidadPrimaPres;
                Multiplicadorquery.difpercup = Multiplicadorquery.UtilidadPrimaPorc - Multiplicadorquery.UtilidadPrimaPresPorc;
                Multiplicadorquery.VNR = 0;
                Multiplicadorquery.VNRPorc = 0;
                Multiplicadorquery.VNRpres = 0;

                listaMultiplicadorquery.Add(Multiplicadorquery);
                #endregion region


                #region CunetaNacional
                Multiplicadorquery = new CatMultiplicador();
                Multiplicadorquery.Cuenta = "Total";
                Multiplicadorquery.Id_Emp = sesion.Id_Emp;
                Multiplicadorquery.Id_Cd = int.Parse(CmbSucursal.Value.ToString());
                Multiplicadorquery.Sucursal = CmbSucursal.Text;
                Multiplicadorquery.Id_Rik = int.Parse(cmbRikDI.Value.ToString());
                Multiplicadorquery.NombreRik = cmbRikDI.Text;
                Multiplicadorquery.totalVenta = ListaUtilidadrik.Count > 0 ? ListaUtilidadrik.Sum(x => x.venta) : 0;
                Multiplicadorquery.UtilidadPrima = ListaUtilidadrik.Count > 0 ? ListaUtilidadrik.Sum(x => x.utilidadBruta) : 0;
                Multiplicadorquery.UtilidadPrimaPorc = (ListaUtilidadrik.Count > 0 ? ListaUtilidadrik.Sum(x => x.utilidadBruta) : 0) / (ListaUtilidadrik.Count > 0 ? ListaUtilidadrik.Sum(x => x.venta) : 0);
                Multiplicadorquery.TotalPresupuesto = ListaPresupuesto.Count > 0 ? ListaPresupuesto.Sum(x => x.TotalPresupuesto) : 0;
                Multiplicadorquery.UtilidadPrimaPres = ListaPresupuesto.Count > 0 ? Convert.ToDouble(ListaPresupuesto.Sum(x => x.up)) : 0;
                Multiplicadorquery.UtilidadPrimaPresPorc = Convert.ToDouble(ListaPresupuesto.Count > 0 ? ListaPresupuesto.Sum(x => x.up) : 0) / Convert.ToDouble(ListaPresupuesto.Count > 0 ? ListaPresupuesto.Sum(x => x.TotalPresupuesto) : 0);
                Multiplicadorquery.difVta = Multiplicadorquery.totalVenta - Convert.ToDouble(Multiplicadorquery.TotalPresupuesto);
                Multiplicadorquery.difup = Multiplicadorquery.UtilidadPrima - Multiplicadorquery.UtilidadPrimaPres;
                Multiplicadorquery.difpercup = Multiplicadorquery.UtilidadPrimaPorc - Multiplicadorquery.UtilidadPrimaPresPorc;
                Multiplicadorquery.VNR = VentaNoRentable.Count > 0 ? VentaNoRentable.Sum(x => x.VNR) : 0;
                Multiplicadorquery.VNRPorc = (VentaNoRentable.Count > 0 ? VentaNoRentable.Sum(x => x.VNR) : 0) > 0 ? (VentaNoRentable.Count > 0 ? VentaNoRentable.Sum(x => x.VNR) : 0) / (ListaUtilidadrik.Count > 0 ? ListaUtilidadrik.Sum(x => x.venta) : 0) : 0;
                Multiplicadorquery.VNRpres = lista.Count > 0 ? lista.Average(x => x.VNRPorc) : 0;

                listaMultiplicadorquery.Add(Multiplicadorquery);
                #endregion region

                Session["listaMultiplicador"] = listaMultiplicadorquery;
                gridRepresentanteMP.DataSource = listaMultiplicadorquery;
                gridRepresentanteMP.DataBind();
                #endregion

                if (listaMultiplicadorquery.Count > 0)
                {
                    Global.Visible = true;
                }

                #region Presupuesto-Venta 
                Session["PresVsVentaPresupuesto"] = lista;
                Session["PresVsVenta"] = ListaUtilidadrik;
                #endregion
            }
        }

        private void CargarReporteCategoriaGenerales()
        {
            ValidarSesion();
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            DateTime fechaInicial = sesion.CalendarioIni;
            DateTime fechafinal = sesion.CalendarioFin;
            if (bcmTipoReporte.Value.ToString() == "1")
            {
                fechaInicial = sesion.CalendarioIni;
                fechafinal = sesion.CalendarioFin;
            }
            else if (bcmTipoReporte.Value.ToString() == "2")
            {
                fechafinal = fechaInicial.AddDays(-1);
                fechaInicial = fechaInicial.AddMonths(-2);
            }
            else if (bcmTipoReporte.Value.ToString() == "3")
            {
                DateTime fecha1 = DateTime.Parse(txtfechaIniciaGR.Value.ToString());
                DateTime fecha2 = DateTime.Parse(txtfechaIniciaGR.Value.ToString()).AddMonths(1).AddDays(-1);

                fechaInicial = DateTime.Parse("01/" + fecha1.Month + "/" + fecha1.Year);
                fechafinal = DateTime.Parse("01/" + fecha2.Month + "/" + fecha2.Year).AddMonths(1).AddDays(-1);
            }
            else if (bcmTipoReporte.Value.ToString() == "4")
            {
                DateTime fechaInicialtrimestral = DateTime.Now;
                DateTime fechafinaltrimestral = DateTime.Now;
                if (BcbTrimestreInicial.Value.ToString() == "1")
                {
                    fechaInicialtrimestral = DateTime.Parse("01/01/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                    fechafinaltrimestral = DateTime.Parse("01/03/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                }
                if (BcbTrimestreInicial.Value.ToString() == "2")
                {
                    fechaInicialtrimestral = DateTime.Parse("01/04/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                    fechafinaltrimestral = DateTime.Parse("01/06/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                }
                if (BcbTrimestreInicial.Value.ToString() == "3")
                {
                    fechaInicialtrimestral = DateTime.Parse("01/07/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                    fechafinaltrimestral = DateTime.Parse("01/09/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                }
                if (BcbTrimestreInicial.Value.ToString() == "4")
                {
                    fechaInicialtrimestral = DateTime.Parse("01/10/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                    fechafinaltrimestral = DateTime.Parse("01/12/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                }

                fechaInicial = fechaInicialtrimestral;
                fechafinal = fechafinaltrimestral;
            }
            string mesAnioInicial = fechaInicial.ToString(); ;
            string mesAniofinal = fechafinal.ToString(); ;

            CN_Presupuesto cdpresupuesto = new CN_Presupuesto();
            CatPresupuesto Presupuesto = new CatPresupuesto();
            List<CatPresupuesto> ListaUtilidad = new List<CatPresupuesto>();
            CN_Presupuesto cnpresupuesto = new CN_Presupuesto();
            List<CatPresupuesto> listaPresupuestocategoria = new List<CatPresupuesto>();
            List<CatPresupuesto> ListaPresupuestoCtipoCuenta = new List<CatPresupuesto>();

            if (mesAnioInicial != "" && mesAniofinal != "")
            {
                #region Consulta
                Presupuesto.Id_Emp = sesion.Id_Emp;
                Presupuesto.Id_Cd = int.Parse(CmbSucursal.Value.ToString());
                Presupuesto.MesInicial = fechaInicial.Month;
                Presupuesto.AnioInicial = fechaInicial.Year;
                Presupuesto.MesFinal = fechafinal.Month;
                Presupuesto.AnioFinal = fechafinal.Year;

                if (Convert.ToDateTime(fechaInicial) > Convert.ToDateTime(fechafinal))
                {
                    mensaje("La fecha inicial es mayor a la fecha final de la sección de observar totales.");
                    return;
                }

                Presupuesto.FechaInicial = fechaInicial;
                Presupuesto.fechafinal = fechafinal;
                Presupuesto.Id_Rik = Convert.ToInt32(cmbRikDI.Value.ToString());
                Presupuesto.Id_u = Convert.ToInt32(cmbRikDI.Value.ToString());

                cnpresupuesto.ConsultaUtilidadcategoria(Presupuesto, ref ListaUtilidad, Emp_CnxCentral);
                cnpresupuesto.PresupuestoMensualCategoria(Presupuesto, ref ListaPresupuestoCtipoCuenta, Emp_CnxCentral);
                #endregion

                #region ReporteCategoria

                #region ReporteNAcionales 

                List<CatPresupuesto> listaategoriaNAcional = new List<CatPresupuesto>();
                List<CatPresupuesto> listacategoriaLocales = new List<CatPresupuesto>();
                List<CatPresupuesto> listacategoriaGlobal = new List<CatPresupuesto>();
                List<CatPresupuesto> categoriaGlobal = new List<CatPresupuesto>();

                listaategoriaNAcional = (from tlist in ListaUtilidad
                                         where tlist.id_matriz != 0
                                         group tlist by new { tlist.Id_Cpr, tlist.Cpr_Descripcion } into g
                                         select new CatPresupuesto
                                         {
                                             Id_Cpr = g.Key.Id_Cpr,
                                             Cpr_Descripcion = g.Key.Cpr_Descripcion,
                                             venta = g.Sum(x => x.venta),
                                             utilidadBruta = g.Sum(x => x.utilidadBruta),
                                             porcubreal = g.Sum(x => x.utilidadBruta) == 0 ? 0 : (g.Sum(x => x.utilidadBruta) / g.Sum(x => x.venta))
                                         }).ToList();

                listacategoriaLocales = (from tlist in ListaUtilidad
                                         where tlist.id_matriz == 0
                                         group tlist by new { tlist.Id_Cpr, tlist.Cpr_Descripcion } into g
                                         select new CatPresupuesto
                                         {
                                             Id_Cpr = g.Key.Id_Cpr,
                                             Cpr_Descripcion = g.Key.Cpr_Descripcion,
                                             venta = g.Sum(x => x.venta),
                                             utilidadBruta = g.Sum(x => x.utilidadBruta),
                                             porcubreal = g.Sum(x => x.utilidadBruta) == 0 ? 0 : (g.Sum(x => x.utilidadBruta) / g.Sum(x => x.venta))
                                         }).ToList();

                listacategoriaGlobal = (from tlist in ListaUtilidad
                                        group tlist by new { tlist.Id_Cpr, tlist.Cpr_Descripcion } into g
                                        select new CatPresupuesto
                                        {
                                            Id_Cpr = g.Key.Id_Cpr,
                                            Cpr_Descripcion = g.Key.Cpr_Descripcion,
                                            venta = g.Sum(x => x.venta),
                                            utilidadBruta = g.Sum(x => x.utilidadBruta),
                                            porcubreal = g.Sum(x => x.utilidadBruta) == 0 ? 0 : (g.Sum(x => x.utilidadBruta) / g.Sum(x => x.venta))
                                        }).ToList();

                categoriaGlobal = (from tlist in listacategoriaGlobal
                                   join tlist2 in listaategoriaNAcional on tlist.Id_Cpr equals tlist2.Id_Cpr into lista
                                   from tlist4 in lista.DefaultIfEmpty()
                                   join tlist3 in listacategoriaLocales on tlist.Id_Cpr equals tlist3.Id_Cpr into lista2
                                   from tlist5 in lista2.DefaultIfEmpty()
                                   select new CatPresupuesto
                                   {
                                       Id_Cpr = tlist.Id_Cpr,
                                       Cpr_Descripcion = tlist.Cpr_Descripcion,
                                       venta = tlist.venta,
                                       utilidadBruta = tlist.utilidadBruta,
                                       porcubreal = tlist.porcubreal,

                                       ventaNacional = tlist4 == null ? 0 : tlist4.venta,
                                       utilidadBrutaNacional = tlist4 == null ? 0 : tlist4.utilidadBruta,
                                       porcubrealNacional = tlist4 == null ? 0 : tlist4.porcubreal,

                                       ventaLocal = tlist5 == null ? 0 : tlist5.venta,
                                       utilidadBrutaLocal = tlist5 == null ? 0 : tlist5.utilidadBruta,
                                       porcubrealLocal = tlist5 == null ? 0 : tlist5.porcubreal
                                   }).ToList();


                List<CatPresupuesto> listaGlobal = new List<CatPresupuesto>();
                CatPresupuesto Categoria = new CatPresupuesto();

                double totalcategoria = categoriaGlobal.Sum(x => x.venta);
                double totalcategoriaup = categoriaGlobal.Sum(x => x.utilidadBruta);
                double totalcategoriaNacional = categoriaGlobal.Sum(x => x.ventaNacional);
                double totalcategoriaupNacional = categoriaGlobal.Sum(x => x.utilidadBrutaNacional);
                double totalcategoriaLocal = categoriaGlobal.Sum(x => x.ventaLocal);
                double totalcategoriaupLocal = categoriaGlobal.Sum(x => x.utilidadBrutaLocal);

                foreach (CatPresupuesto categorias in categoriaGlobal)
                {
                    if (Convert.ToInt32(cmbRikDI.Value.ToString()) != -1)
                    {
                        int id_rik = Convert.ToInt32(cmbRikDI.Value.ToString());
                        categorias.PresupuestoventaNacional = ListaPresupuestoCtipoCuenta.Where(x => x.Id_Cpr == categorias.Id_Cpr && x.TipoCuenta.ToLower() == "nacional" && x.Id_Rik == id_rik).Sum(x => x.TotalPresupuesto);
                        categorias.PresupuestoutilidadBrutaNacional = ListaPresupuestoCtipoCuenta.Where(x => x.Id_Cpr == categorias.Id_Cpr && x.TipoCuenta.ToLower() == "nacional" && x.Id_Rik == id_rik).Sum(x => x.up);
                        if (ListaPresupuestoCtipoCuenta.Where(x => x.Id_Cpr == categorias.Id_Cpr && x.TipoCuenta.ToLower() == "nacional" && x.Id_Rik == id_rik).Count() > 0)
                        {
                            if (categorias.PresupuestoventaNacional > 0)
                            {
                                categorias.PresupuestoporcubrealNacional = categorias.PresupuestoutilidadBrutaNacional / categorias.PresupuestoventaNacional;
                            }
                            else
                            {
                                categorias.PresupuestoporcubrealNacional = 0;
                            }
                        }
                        categorias.PresupuestoventaLocal = ListaPresupuestoCtipoCuenta.Where(x => x.Id_Cpr == categorias.Id_Cpr && x.TipoCuenta.ToLower() == "local" && x.Id_Rik == id_rik).Sum(x => x.TotalPresupuesto);
                        categorias.PresupuestoutilidadBrutaLocal = ListaPresupuestoCtipoCuenta.Where(x => x.Id_Cpr == categorias.Id_Cpr && x.TipoCuenta.ToLower() == "local" && x.Id_Rik == id_rik).Sum(x => x.up);
                        if (ListaPresupuestoCtipoCuenta.Where(x => x.Id_Cpr == categorias.Id_Cpr && x.TipoCuenta.ToLower() == "local" && x.Id_Rik == id_rik).Count() > 0)
                        {
                            if (categorias.PresupuestoventaLocal > 0)
                            {
                                categorias.PresupuestoporcubrealLocal = categorias.PresupuestoutilidadBrutaLocal / categorias.PresupuestoventaLocal;
                            }
                            else
                            {
                                categorias.PresupuestoporcubrealLocal = 0;
                            }
                        }
                        categorias.Presupuestoventa = ListaPresupuestoCtipoCuenta.Where(x => x.Id_Cpr == categorias.Id_Cpr && x.Id_Rik == id_rik).Sum(x => x.TotalPresupuesto);
                        categorias.PresupuestoutilidadBruta = ListaPresupuestoCtipoCuenta.Where(x => x.Id_Cpr == categorias.Id_Cpr && x.Id_Rik == id_rik).Sum(x => x.up);
                        if (ListaPresupuestoCtipoCuenta.Where(x => x.Id_Cpr == categorias.Id_Cpr && x.Id_Rik == id_rik).Count() > 0)
                        {
                            if (categorias.Presupuestoventa > 0)
                            {
                                categorias.Presupuestoporcubreal = categorias.PresupuestoutilidadBruta / categorias.Presupuestoventa;
                            }
                            else
                            {
                                categorias.Presupuestoporcubreal = 0;
                            }
                        }

                    }
                    else
                    {
                        categorias.PresupuestoventaNacional = ListaPresupuestoCtipoCuenta.Where(x => x.Id_Cpr == categorias.Id_Cpr && x.TipoCuenta.ToLower() == "nacional").Sum(x => x.TotalPresupuesto);
                        categorias.PresupuestoutilidadBrutaNacional = ListaPresupuestoCtipoCuenta.Where(x => x.Id_Cpr == categorias.Id_Cpr && x.TipoCuenta.ToLower() == "nacional").Sum(x => x.up);
                        if (ListaPresupuestoCtipoCuenta.Where(x => x.Id_Cpr == categorias.Id_Cpr && x.TipoCuenta.ToLower() == "nacional").Count() > 0)
                        {
                            if (categorias.PresupuestoventaNacional > 0)
                            {
                                categorias.PresupuestoporcubrealNacional = categorias.PresupuestoutilidadBrutaNacional / categorias.PresupuestoventaNacional;
                            }
                            else
                            {
                                categorias.PresupuestoporcubrealNacional = 0;
                            }
                        }
                        categorias.PresupuestoventaLocal = ListaPresupuestoCtipoCuenta.Where(x => x.Id_Cpr == categorias.Id_Cpr && x.TipoCuenta.ToLower() == "local").Sum(x => x.TotalPresupuesto);
                        categorias.PresupuestoutilidadBrutaLocal = ListaPresupuestoCtipoCuenta.Where(x => x.Id_Cpr == categorias.Id_Cpr && x.TipoCuenta.ToLower() == "local").Sum(x => x.up);
                        if (ListaPresupuestoCtipoCuenta.Where(x => x.Id_Cpr == categorias.Id_Cpr && x.TipoCuenta.ToLower() == "local").Count() > 0)
                        {
                            if (categorias.PresupuestoventaLocal > 0)
                            {
                                categorias.PresupuestoporcubrealLocal = categorias.PresupuestoutilidadBrutaLocal / categorias.PresupuestoventaLocal;
                            }
                            else
                            {
                                categorias.PresupuestoporcubrealLocal = 0;
                            }
                        }
                        categorias.Presupuestoventa = ListaPresupuestoCtipoCuenta.Where(x => x.Id_Cpr == categorias.Id_Cpr).Sum(x => x.TotalPresupuesto);
                        categorias.PresupuestoutilidadBruta = ListaPresupuestoCtipoCuenta.Where(x => x.Id_Cpr == categorias.Id_Cpr).Sum(x => x.up);
                        if (ListaPresupuestoCtipoCuenta.Where(x => x.Id_Cpr == categorias.Id_Cpr).Count() > 0)
                        {
                            if (categorias.Presupuestoventa > 0)
                            {
                                categorias.Presupuestoporcubreal = categorias.PresupuestoutilidadBruta / categorias.Presupuestoventa;
                            }
                            else
                            {
                                categorias.Presupuestoporcubreal = 0;
                            }
                        }
                    }
                    categorias.Mezcla = categorias.venta > 0 ? categorias.venta / totalcategoria : 0;
                    categorias.MezclaUP = categorias.utilidadBruta > 0 ? categorias.utilidadBruta / totalcategoriaup : 0;
                    categorias.MezclaNacional = categorias.ventaNacional > 0 ? categorias.ventaNacional / totalcategoriaNacional : 0;
                    categorias.MezclaUPNacional = categorias.utilidadBrutaNacional > 0 ? categorias.utilidadBrutaNacional / totalcategoriaupNacional : 0;
                    categorias.MezclaLocal = categorias.ventaLocal > 0 ? categorias.ventaLocal / totalcategoriaLocal : 0;
                    categorias.MezclaUPLocal = categorias.utilidadBrutaLocal > 0 ? categorias.utilidadBrutaLocal / totalcategoriaupLocal : 0;

                }
                #endregion

                Session["listacategoria"] = categoriaGlobal;
                grdCategoria.DataSource = categoriaGlobal;
                grdCategoria.DataBind();

                if (categoriaGlobal.Count > 0)
                {
                    categoria.Visible = true;
                }

                #endregion
            }

        }

        private void CargarreporteVentaGenerales()
        {
            ValidarSesion();
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            DateTime fechaInicial = sesion.CalendarioIni;
            DateTime fechafinal = sesion.CalendarioFin;
            if (bcmTipoReporte.Value.ToString() == "1")
            {
                fechaInicial = sesion.CalendarioIni;
                fechafinal = sesion.CalendarioFin;
            }
            else if (bcmTipoReporte.Value.ToString() == "2")
            {
                fechafinal = fechaInicial.AddDays(-1);
                fechaInicial = fechaInicial.AddMonths(-2);
            }
            else if (bcmTipoReporte.Value.ToString() == "3")
            {
                DateTime fecha1 = DateTime.Parse(txtfechaIniciaGR.Value.ToString());
                DateTime fecha2 = DateTime.Parse(txtfechaIniciaGR.Value.ToString()).AddMonths(1).AddDays(-1);

                fechaInicial = DateTime.Parse("01/" + fecha1.Month + "/" + fecha1.Year);
                fechafinal = DateTime.Parse("01/" + fecha2.Month + "/" + fecha2.Year).AddMonths(1).AddDays(-1);
            }
            else if (bcmTipoReporte.Value.ToString() == "4")
            {
                DateTime fechaInicialtrimestral = DateTime.Now;
                DateTime fechafinaltrimestral = DateTime.Now;
                if (BcbTrimestreInicial.Value.ToString() == "1")
                {
                    fechaInicialtrimestral = DateTime.Parse("01/01/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                    fechafinaltrimestral = DateTime.Parse("01/03/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                }
                if (BcbTrimestreInicial.Value.ToString() == "2")
                {
                    fechaInicialtrimestral = DateTime.Parse("01/04/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                    fechafinaltrimestral = DateTime.Parse("01/06/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                }
                if (BcbTrimestreInicial.Value.ToString() == "3")
                {
                    fechaInicialtrimestral = DateTime.Parse("01/07/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                    fechafinaltrimestral = DateTime.Parse("01/09/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                }
                if (BcbTrimestreInicial.Value.ToString() == "4")
                {
                    fechaInicialtrimestral = DateTime.Parse("01/10/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                    fechafinaltrimestral = DateTime.Parse("01/12/" + DateTime.Parse(txtfechaTrimestreInicial.Value.ToString()).Year.ToString());
                }

                fechaInicial = fechaInicialtrimestral;
                fechafinal = fechafinaltrimestral;
            }
            string mesAnioInicial = fechaInicial.ToString(); ;
            string mesAniofinal = fechafinal.ToString(); ;

            CN_Presupuesto cdpresupuesto = new CN_Presupuesto();
            CatPresupuesto Presupuesto = new CatPresupuesto();
            List<CatPresupuesto> ListaUtilidad = new List<CatPresupuesto>();
            CN_Presupuesto cnpresupuesto = new CN_Presupuesto();

            List<CatPresupuesto> VentaNoRentable = new List<CatPresupuesto>();

            if (mesAnioInicial != "" && mesAniofinal != "")
            {
                #region Consulta
                Presupuesto.Id_Emp = sesion.Id_Emp;
                Presupuesto.Id_Cd = int.Parse(CmbSucursal.Value.ToString());
                Presupuesto.MesInicial = fechaInicial.Month;
                Presupuesto.AnioInicial = fechaInicial.Year;
                Presupuesto.MesFinal = fechafinal.Month;
                Presupuesto.AnioFinal = fechafinal.Year;

                string FechaInicialD = ("01" + "/" + fechaInicial.Month + "/" + fechaInicial.Year);
                string FechaFinalD = ("01" + "/" + fechafinal.Month + "/" + fechafinal.Year);

                if (Convert.ToDateTime(FechaInicialD) > Convert.ToDateTime(FechaFinalD))
                {
                    mensaje("La fecha inicial es mayor a la fecha final de la sección de observar totales.");
                    return;
                }

                Presupuesto.FechaInicial = fechaInicial;
                Presupuesto.fechafinal = fechafinal;
                Presupuesto.Id_Rik = Convert.ToInt32(cmbRikDI.Value.ToString());
                Presupuesto.Id_u = Convert.ToInt32(cmbRikDI.Value.ToString());


                Session["RikGU"] = Presupuesto.Id_Rik;
                Session["SUcrusalGU"] = Presupuesto.Id_Cd;

                cdpresupuesto.ConsultaUtilidadRIkxmesCliente(Presupuesto, ref ListaUtilidad, Emp_CnxCentral);

                #endregion

                #region FiltradoXRik
                List<CatPresupuesto> ListaUtilidadrik = new List<CatPresupuesto>();
                List<CatPresupuesto> lista = new List<CatPresupuesto>();
                List<CatPresupuesto> ListaUtilidadrikHistorial = new List<CatPresupuesto>();
                List<CatPresupuesto> PresupuestolistaHistorial = new List<CatPresupuesto>();
                if (cmbRikDI.Value.ToString() != "-1")
                {
                    ListaUtilidadrik = (from tlist in ListaUtilidad
                                        where tlist.Id_Rik == Convert.ToInt32(cmbRikDI.Value.ToString())
                                        select tlist).ToList();
                }
                else
                {
                    ListaUtilidadrik = (from tlist in ListaUtilidad
                                        select tlist).ToList();
                }
                #endregion 

                #region ReporteDetallado

                List<CatPresupuesto> query = ListaUtilidadrik.GroupBy(x => new
                {
                    id_cte = x.id_cte,
                    id_ter = x.id_ter,
                    cte_nomcomercial = x.cte_nomcomercial
                })
              .Select(x => new CatPresupuesto
              {
                  id_cte = x.Key.id_cte,
                  cte_nomcomercial = x.Key.cte_nomcomercial,
                  id_ter = x.Key.id_ter,
                  venta = x.Sum(xx => xx.venta),
                  utilidadBruta = x.Sum(xx => xx.utilidadBruta),
                  porcubreal = x.Sum(xx => xx.utilidadBruta) > 0 ? x.Sum(xx => xx.utilidadBruta) / x.Sum(xx => xx.venta) : 0,
                  vnr = 0,
                  Matriz = "N/A",
                  AcysVenta = 0,
                  AcysUP = 0,
                  AcysUPProc = 0
              })
              .ToList();

                foreach (CatPresupuesto pres in query)
                {
                    List<CatPresupuesto> VentaNoacys = new List<CatPresupuesto>();
                    List<CatPresupuesto> VentaNoRentabledetalle = new List<CatPresupuesto>();
                    List<CatPresupuesto> Ventamatriz = new List<CatPresupuesto>();
                    Presupuesto.id_cte = pres.id_cte;
                    Presupuesto.id_ter = pres.id_ter;

                    cnpresupuesto.Consultaventanodetalle(Presupuesto, ref VentaNoRentabledetalle, Emp_CnxCentral);

                    pres.VNR = VentaNoRentabledetalle.Sum(x => x.VNR);
                    pres.VNRPorc = pres.VNR > 0 ? 1 : 0;
                    pres.FechaInicial = fechaInicial;
                    pres.fechafinal = fechafinal;
                    pres.Id_Cd = int.Parse(CmbSucursal.Value.ToString());
                    pres.Id_Rik = Convert.ToInt32(cmbRikDI.Value.ToString());
                    pres.Id_u = Convert.ToInt32(cmbRikDI.Value.ToString());
                    cnpresupuesto.Consultamatriz(Presupuesto, ref Ventamatriz, Emp_CnxCentral);

                    if (Ventamatriz.Count > 0)
                    {
                        pres.Matriz = Ventamatriz.FirstOrDefault().Matriz;
                    }

                    cnpresupuesto.Consultaventanoacysdetalle(Presupuesto, ref VentaNoacys, Emp_CnxCentral);
                    pres.AcysVenta = VentaNoacys.Sum(x => x.AcysVenta);
                    pres.AcysUP = VentaNoacys.Sum(x => x.AcysUP);
                    pres.AcysUPProc = VentaNoacys.Sum(x => x.AcysUP) > 0 ? VentaNoacys.Sum(x => x.AcysUP) / VentaNoacys.Sum(x => x.AcysVenta) : 0;
                }

                Session["listaVenta"] = query;
                grDetalle.DataSource = query;
                grDetalle.DataBind();
                #endregion region

                if (query.Count > 0)
                {
                    detallado.Visible = true;
                }
            }
        }

        private void CargarDatosCategoria(int cliente, string territorio, DateTime fechaInicial, DateTime fechaFinal, int id_cd, int Id_Rik, ref List<CatPresupuesto> query)
        {
            ValidarSesion();
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            string mesAnioInicial = fechaInicial.ToString();
            string mesAniofinal = fechaFinal.ToString();

            CatPresupuesto Presupuesto = new CatPresupuesto();
            List<CatPresupuesto> ListaUtilidad = new List<CatPresupuesto>();
            CN_Presupuesto CN = new CN_Presupuesto();

            if (mesAnioInicial != "" && mesAniofinal != "")
            {
                Presupuesto.Id_Emp = sesion.Id_Emp;
                Presupuesto.Id_Cd = id_cd;
                Presupuesto.MesInicial = fechaInicial.Month;
                Presupuesto.AnioInicial = fechaInicial.Year;
                Presupuesto.MesFinal = fechaFinal.Month;
                Presupuesto.AnioFinal = fechaFinal.Year;
                Presupuesto.id_cte = cliente;
                Presupuesto.id_ter = Convert.ToInt32(territorio);

                string FechaInicialD = ("01" + "/" + fechaInicial.Month + "/" + fechaInicial.Year);
                string FechaFinalD = ("01" + "/" + fechaFinal.Month + "/" + fechaFinal.Year);

                Presupuesto.FechaInicial = fechaInicial;
                Presupuesto.fechafinal = fechaFinal;
                Presupuesto.Id_Rik = Id_Rik;
                Presupuesto.Id_u = Id_Rik;

                CN.ConsultaUtilidadcategoria(Presupuesto, ref ListaUtilidad, Emp_CnxCentral);
                #region ReporteDetallado

                query = ListaUtilidad.GroupBy(x => new
                {
                    Id_Cpr = x.Id_Cpr,
                    Cpr_Descripcion = x.Cpr_Descripcion
                })
              .Select(x => new CatPresupuesto
              {
                  Id_Cpr = x.Key.Id_Cpr,
                  Cpr_Descripcion = x.Key.Cpr_Descripcion,
                  venta = x.Sum(xx => xx.venta),
                  utilidadBruta = x.Sum(xx => xx.utilidadBruta),
                  porcubreal = x.Average(xx => xx.utilidadBruta) > 0 ? x.Average(xx => xx.utilidadBruta) / x.Average(xx => xx.venta) : 0,
                  Mezcla = 0,
                  MezclaAcys = 0,
                  AcysVenta = 0,
                  AcysUP = 0,
                  AcysUPProc = 0,
                  Id_Emp = sesion.Id_Emp,
                  Id_Cd = id_cd,
                  FechaInicial = fechaInicial,
                  fechafinal = fechaFinal,
                  id_cte = cliente,
                  id_ter = Convert.ToInt32(territorio)
              })
              .ToList();
                #endregion region 
            }

            double total = query.Sum(x => x.venta);

            foreach (CatPresupuesto q in query)
            {
                List<CatPresupuesto> VentaNoacys = new List<CatPresupuesto>();
                q.Mezcla = q.venta / total;
                Presupuesto.Cpr_Descripcion = q.Cpr_Descripcion;
                CN.Consultaventanoacysdetalle(Presupuesto, ref VentaNoacys, Emp_CnxCentral);
                q.AcysVenta = VentaNoacys.Sum(x => x.AcysVenta);
                q.AcysUP = VentaNoacys.Sum(x => x.AcysUP);
                q.AcysUPProc = VentaNoacys.Sum(x => x.AcysUP) > 0 ? VentaNoacys.Sum(x => x.AcysUP) / VentaNoacys.Sum(x => x.AcysVenta) : 0;
            }

            double totalmezcla = query.Sum(x => x.AcysVenta);
            foreach (CatPresupuesto q in query)
            {
                q.MezclaAcys = q.AcysVenta > 0 ? q.AcysVenta / totalmezcla : 0;
            }
        }

        private void CargarDatosProducto(int cliente, string territorio, DateTime fechaInicial, DateTime fechaFinal, int id_cd, int Id_Rik, string Cpr_Descripcion, ref List<CatPresupuesto> query)
        {
            ValidarSesion();
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            string mesAnioInicial = fechaInicial.ToString();
            string mesAniofinal = fechaFinal.ToString();

            CatPresupuesto Presupuesto = new CatPresupuesto();
            List<CatPresupuesto> ListaUtilidad = new List<CatPresupuesto>();
            CN_Presupuesto CN = new CN_Presupuesto();

            if (mesAnioInicial != "" && mesAniofinal != "")
            {
                Presupuesto.Id_Emp = sesion.Id_Emp;
                Presupuesto.Id_Cd = id_cd;
                Presupuesto.MesInicial = fechaInicial.Month;
                Presupuesto.AnioInicial = fechaInicial.Year;
                Presupuesto.MesFinal = fechaFinal.Month;
                Presupuesto.AnioFinal = fechaFinal.Year;
                Presupuesto.id_cte = cliente;
                Presupuesto.id_ter = Convert.ToInt32(territorio);

                string FechaInicialD = ("01" + "/" + fechaInicial.Month + "/" + fechaInicial.Year);
                string FechaFinalD = ("01" + "/" + fechaFinal.Month + "/" + fechaFinal.Year);

                Presupuesto.FechaInicial = fechaInicial;
                Presupuesto.fechafinal = fechaFinal;
                Presupuesto.Id_Rik = Id_Rik;
                Presupuesto.Id_u = Id_Rik;
                Presupuesto.Cpr_Descripcion = Cpr_Descripcion;
                CN.ConsultaUtilidadProducto(Presupuesto, ref ListaUtilidad, Emp_CnxCentral);

                #region ReporteDetallado

                query = ListaUtilidad.GroupBy(x => new
                {
                    id_prd = x.id_prd,
                    prd_nombre = x.prd_nombre
                })
              .Select(x => new CatPresupuesto
              {
                  id_prd = x.Key.id_prd,
                  prd_nombre = x.Key.prd_nombre,
                  venta = x.Sum(xx => xx.venta),
                  utilidadBruta = x.Sum(xx => xx.utilidadBruta),
                  porcubreal = x.Sum(xx => xx.utilidadBruta) > 0 ? x.Sum(xx => xx.utilidadBruta) / x.Sum(xx => xx.venta) : 0,
                  Mezcla = 0,
                  MezclaAcys = 0,
                  AcysVenta = 0,
                  AcysUP = 0,
                  AcysUPProc = 0
              })
              .ToList();

                double total = query.Sum(x => x.venta);
                foreach (CatPresupuesto q in query)
                {
                    List<CatPresupuesto> VentaNoacys = new List<CatPresupuesto>();
                    q.Mezcla = q.venta / total;

                    Presupuesto.Cpr_Descripcion = q.Cpr_Descripcion;
                    Presupuesto.id_prd = q.id_prd;

                    CN.Consultaventanoacysdetalle(Presupuesto, ref VentaNoacys, Emp_CnxCentral);

                    q.AcysVenta = VentaNoacys.Sum(x => x.AcysVenta);
                    q.AcysUP = VentaNoacys.Sum(x => x.AcysUP);
                    q.AcysUPProc = VentaNoacys.Sum(x => x.AcysUP) > 0 ? VentaNoacys.Sum(x => x.AcysUP) / VentaNoacys.Sum(x => x.AcysVenta) : 0;


                }

                double totalmezcla = query.Sum(x => x.AcysVenta);
                foreach (CatPresupuesto q in query)
                {
                    q.MezclaAcys = q.AcysVenta > 0 ? q.AcysVenta / totalmezcla : 0;
                }
                #endregion region 
            }
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
            BcbTrimestreInicial.Value = "-1";

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

            cmbRikDI.DataSource = query;
            cmbRikDI.ValueField = "id";
            cmbRikDI.TextField = "nombre";
            cmbRikDI.DataBind();

            cmbRikDI.Items.Add("--Todos--", "-1");
            cmbRikDI.Value = "-1";



            if (Sesion.Id_Rik > 0 && Sesion.Id_TU != 3)
            {
                cmbRikDI.Value = Sesion.Id_Rik.ToString();
                cmbRikDI.ReadOnly = true;
            }
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




        #endregion

        #region Imprimir

        protected void btnImprimir_ServerClick(object sender, EventArgs e)
        {
            gridRepresentanteMP.ExportXlsToResponse("Reporte Global-" + DateTime.Now.ToString("dd-MM-yyyy"), new XlsExportOptionsEx { ExportType = ExportType.WYSIWYG });
        }

        protected void btnimprimirdetalle_ServerClick(object sender, EventArgs e)
        {
            grDetalle.ExportXlsToResponse("Reporte Venta Cliente-" + DateTime.Now.ToString("dd-MM-yyyy"), new XlsExportOptionsEx { ExportType = ExportType.WYSIWYG });
        }

        protected void btnCategoria_ServerClick(object sender, EventArgs e)
        {
            grdCategoria.ExportXlsToResponse("Reporte Categoria-" + DateTime.Now.ToString("dd-MM-yyyy"), new XlsExportOptionsEx { ExportType = ExportType.WYSIWYG });
        }

        protected void ReporteCliente_Click(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer c = ((System.Web.UI.HtmlControls.HtmlButton)sender).NamingContainer as GridViewDataItemTemplateContainer;


            string id_cte = c.Grid.GetRowValues(c.VisibleIndex, "id_cte").ToString().Trim();
            string id_Terr = c.Grid.GetRowValues(c.VisibleIndex, "id_ter").ToString().Trim();
            string FechaInicial = c.Grid.GetRowValues(c.VisibleIndex, "FechaInicial").ToString().Trim();
            string fechafinal = c.Grid.GetRowValues(c.VisibleIndex, "fechafinal").ToString().Trim();
            string Id_Cd = c.Grid.GetRowValues(c.VisibleIndex, "Id_Cd").ToString().Trim();
            string Id_Rik = c.Grid.GetRowValues(c.VisibleIndex, "Id_Rik").ToString().Trim();
            string matriz = c.Grid.GetRowValues(c.VisibleIndex, "Matriz").ToString().Trim();
            string Nombre = c.Grid.GetRowValues(c.VisibleIndex, "cte_nomcomercial").ToString().Trim();

            List<CatPresupuesto> Segmento = new List<CatPresupuesto>();
            CargarDatosCategoria(Convert.ToInt32(id_cte), id_Terr, Convert.ToDateTime(FechaInicial), Convert.ToDateTime(fechafinal), Convert.ToInt32(Id_Cd), Convert.ToInt32(Id_Rik), ref Segmento);

            using (var workbook = new XLWorkbook())
            {
                var HojaExcel = workbook.Worksheets.Add("Reporte");

                HojaExcel.Cell("A1").Value = "Matriz:";
                HojaExcel.Cell("A1").Style.Font.Bold = true;
                HojaExcel.Cell("A1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                HojaExcel.Cell("B1").Value = matriz;
                HojaExcel.Cell("B1").Style.Font.Bold = true;
                HojaExcel.Cell("B1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell("A2").Value = "Cliente:";
                HojaExcel.Cell("A2").Style.Font.Bold = true;
                HojaExcel.Cell("A2").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                HojaExcel.Cell("B2").Value = Nombre;
                HojaExcel.Cell("B2").Style.Font.Bold = true;
                HojaExcel.Cell("B2").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell("A3").Value = "Territorio:";
                HojaExcel.Cell("A3").Style.Font.Bold = true;
                HojaExcel.Cell("A3").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                HojaExcel.Cell("B3").Value = id_Terr;
                HojaExcel.Cell("B3").Style.Font.Bold = true;
                HojaExcel.Cell("B3").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                HojaExcel.Cell("A4").Value = "Sucursal:";
                HojaExcel.Cell("A4").Style.Font.Bold = true;
                HojaExcel.Cell("A4").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                HojaExcel.Cell("B4").Value = CmbSucursal.SelectedItem.Text;
                HojaExcel.Cell("B4").Style.Font.Bold = true;
                HojaExcel.Cell("B4").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                int celda = 5;

                foreach (CatPresupuesto Producto in Segmento)
                {
                    celda++;
                    HojaExcel.Cell(celda, 1).Value = Producto.Cpr_Descripcion;
                    HojaExcel.Cell(celda, 1).Style.Font.Bold = true;
                    HojaExcel.Cell(celda, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    celda++;

                    List<CatPresupuesto> Productos = new List<CatPresupuesto>();
                    int cliente = Producto.id_cte;
                    string territorio = Producto.id_ter.ToString();
                    DateTime fechaInicial = Producto.FechaInicial;
                    DateTime fechaFinal = Producto.fechafinal;
                    int id_cd = Producto.Id_Cd;
                    int rik = Convert.ToInt32(Producto.Id_Rik);
                    string Cpr_Descripcion = Producto.Cpr_Descripcion.ToString();
                    CargarDatosProducto(Convert.ToInt32(cliente), territorio, fechaInicial, fechaFinal, id_cd, rik, Cpr_Descripcion, ref Productos);

                    HojaExcel.Cell(celda, 1).Value = "Producto";
                    HojaExcel.Cell(celda, 1).Style.Font.Bold = true;
                    HojaExcel.Cell(celda, 1).Style.Fill.BackgroundColor = XLColor.Silver;
                    HojaExcel.Cell(celda, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    HojaExcel.Cell(celda, 2).Value = "Venta";
                    HojaExcel.Cell(celda, 2).Style.Font.Bold = true;
                    HojaExcel.Cell(celda, 2).Style.Fill.BackgroundColor = XLColor.Silver;
                    HojaExcel.Cell(celda, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    HojaExcel.Cell(celda, 3).Value = "UP";
                    HojaExcel.Cell(celda, 3).Style.Font.Bold = true;
                    HojaExcel.Cell(celda, 3).Style.Fill.BackgroundColor = XLColor.Silver;
                    HojaExcel.Cell(celda, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    HojaExcel.Cell(celda, 4).Value = "% UP";
                    HojaExcel.Cell(celda, 4).Style.Font.Bold = true;
                    HojaExcel.Cell(celda, 4).Style.Fill.BackgroundColor = XLColor.Silver;
                    HojaExcel.Cell(celda, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    HojaExcel.Cell(celda, 5).Value = "Mezcla";
                    HojaExcel.Cell(celda, 5).Style.Font.Bold = true;
                    HojaExcel.Cell(celda, 5).Style.Fill.BackgroundColor = XLColor.Silver;
                    HojaExcel.Cell(celda, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    HojaExcel.Cell(celda, 6).Value = "Acys Venta (Objetivo)";
                    HojaExcel.Cell(celda, 6).Style.Font.Bold = true;
                    HojaExcel.Cell(celda, 6).Style.Fill.BackgroundColor = XLColor.Silver;
                    HojaExcel.Cell(celda, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    HojaExcel.Cell(celda, 7).Value = "Acys UP (Objetivo)";
                    HojaExcel.Cell(celda, 7).Style.Font.Bold = true;
                    HojaExcel.Cell(celda, 7).Style.Fill.BackgroundColor = XLColor.Silver;
                    HojaExcel.Cell(celda, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    HojaExcel.Cell(celda, 8).Value = "Acys % UP (Objetivo)";
                    HojaExcel.Cell(celda, 8).Style.Font.Bold = true;
                    HojaExcel.Cell(celda, 8).Style.Fill.BackgroundColor = XLColor.Silver;
                    HojaExcel.Cell(celda, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    HojaExcel.Cell(celda, 9).Value = "Mezcla ACyS";
                    HojaExcel.Cell(celda, 9).Style.Font.Bold = true;
                    HojaExcel.Cell(celda, 9).Style.Fill.BackgroundColor = XLColor.Silver;
                    HojaExcel.Cell(celda, 9).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    celda++;
                    foreach (CatPresupuesto Prod in Productos)
                    {
                        HojaExcel.Cell(celda, 1).Value = Prod.prd_nombre;
                        HojaExcel.Cell(celda, 1).Style.Font.Bold = true;
                        HojaExcel.Cell(celda, 1).Style.Fill.BackgroundColor = XLColor.White;
                        HojaExcel.Cell(celda, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        HojaExcel.Cell(celda, 2).Value = Prod.venta;
                        HojaExcel.Cell(celda, 2).Style.Font.Bold = true;
                        HojaExcel.Cell(celda, 2).Style.Fill.BackgroundColor = XLColor.White;
                        HojaExcel.Cell(celda, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        HojaExcel.Cell(celda, 2).Style.NumberFormat.Format = "$#,##0.00";
                        HojaExcel.Cell(celda, 2).DataType = XLDataType.Number;

                        HojaExcel.Cell(celda, 3).Value = Prod.utilidadBruta;
                        HojaExcel.Cell(celda, 3).Style.Font.Bold = true;
                        HojaExcel.Cell(celda, 3).Style.Fill.BackgroundColor = XLColor.White;
                        HojaExcel.Cell(celda, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        HojaExcel.Cell(celda, 3).Style.NumberFormat.Format = "$#,##0.00";
                        HojaExcel.Cell(celda, 3).DataType = XLDataType.Number;

                        HojaExcel.Cell(celda, 4).Value = Prod.porcubreal;
                        HojaExcel.Cell(celda, 4).Style.Font.Bold = true;
                        HojaExcel.Cell(celda, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                        HojaExcel.Cell(celda, 4).Style.NumberFormat.Format = "###,#0%";
                        HojaExcel.Cell(celda, 4).DataType = XLDataType.Number;

                        HojaExcel.Cell(celda, 5).Value = Prod.Mezcla;
                        HojaExcel.Cell(celda, 5).Style.Font.Bold = true;
                        HojaExcel.Cell(celda, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                        HojaExcel.Cell(celda, 5).Style.NumberFormat.Format = "###,#0%";
                        HojaExcel.Cell(celda, 5).DataType = XLDataType.Number;

                        HojaExcel.Cell(celda, 6).Value = Prod.AcysVenta;
                        HojaExcel.Cell(celda, 6).Style.Font.Bold = true;
                        HojaExcel.Cell(celda, 6).Style.Fill.BackgroundColor = XLColor.White;
                        HojaExcel.Cell(celda, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        HojaExcel.Cell(celda, 6).Style.NumberFormat.Format = "$#,##0.00";
                        HojaExcel.Cell(celda, 6).DataType = XLDataType.Number;

                        HojaExcel.Cell(celda, 7).Value = Prod.AcysUP;
                        HojaExcel.Cell(celda, 7).Style.Font.Bold = true;
                        HojaExcel.Cell(celda, 7).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                        HojaExcel.Cell(celda, 7).Style.NumberFormat.Format = "###,#0%";
                        HojaExcel.Cell(celda, 7).DataType = XLDataType.Number;

                        HojaExcel.Cell(celda, 8).Value = Prod.AcysUPProc;
                        HojaExcel.Cell(celda, 8).Style.Font.Bold = true;
                        HojaExcel.Cell(celda, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                        HojaExcel.Cell(celda, 8).Style.NumberFormat.Format = "###,#0%";
                        HojaExcel.Cell(celda, 8).DataType = XLDataType.Number;

                        HojaExcel.Cell(celda, 9).Value = Prod.MezclaAcys;
                        HojaExcel.Cell(celda, 9).Style.Font.Bold = true;
                        HojaExcel.Cell(celda, 9).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                        HojaExcel.Cell(celda, 9).Style.NumberFormat.Format = "###,#0%";
                        HojaExcel.Cell(celda, 9).DataType = XLDataType.Number;
                        celda++;
                    }


                }


                HojaExcel.Columns().AdjustToContents();
                string rutaguardado = Server.MapPath("~/Reportes/") + "ReporteGestion" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";


                if (File.Exists(rutaguardado))
                {
                    File.Delete(rutaguardado);
                }

                workbook.SaveAs(rutaguardado);
                string Outgoingfile = "ReporteGestion" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";
                string ruta = Server.MapPath("~/Reportes/") + "ReporteGestion" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";
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
        #endregion

        #region webMethod 

        [WebMethod]
        public static string ObservarTotales()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];


                if (Sesion == null)
                {
                    return JsonConvert.SerializeObject(new { id = 0 });
                }

                if (HttpContext.Current.Session["listacategoria"] != null)
                {
                    List<CatPresupuesto> lista3 = new List<CatPresupuesto>();
                    lista3 = (List<CatPresupuesto>)HttpContext.Current.Session["listacategoria"];


                    double total = lista3.Sum(x => x.venta);
                    double totalUtilidad = lista3.Sum(x => x.utilidadBruta);
                    string titulo = "";
                    string datos = "";
                    string datosUtilidad = "";
                    foreach (CatPresupuesto presupuesto in lista3)
                    {
                        if (titulo == "")
                        {
                            titulo = presupuesto.Cpr_Descripcion;
                            datos = presupuesto.venta.ToString("F2");
                            datosUtilidad = presupuesto.utilidadBruta.ToString("F2");
                        }
                        else
                        {
                            titulo = titulo + "," + presupuesto.Cpr_Descripcion;
                            datos = datos + "," + presupuesto.venta.ToString("F2");
                            datosUtilidad = datosUtilidad + "," + presupuesto.utilidadBruta.ToString("F2");
                        }

                    }
                    string totalstr = "Total: " + total.ToString("c");
                    string totalutilidadstr = "Total: " + totalUtilidad.ToString("c");
                    return JsonConvert.SerializeObject(new { id = 3, titulo = titulo, datosup = datos, totalup = totalstr, datosUtilidad = datosUtilidad, totalUtilidad = totalutilidadstr });
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
        public static string PresupuestoVenta()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
                if (Sesion == null)
                {
                    return JsonConvert.SerializeObject(new { id = 0 });
                }
                if (HttpContext.Current.Session["PresVsVentaPresupuesto"] != null && HttpContext.Current.Session["PresVsVenta"] != null)
                {

                    List<CatPresupuesto> PresVsVenta = new List<CatPresupuesto>();
                    PresVsVenta = (List<CatPresupuesto>)HttpContext.Current.Session["PresVsVenta"];

                    List<CatPresupuesto> PresVsVentaPresupuesto = new List<CatPresupuesto>();
                    PresVsVentaPresupuesto = (List<CatPresupuesto>)HttpContext.Current.Session["PresVsVentaPresupuesto"];

                    DateTime AnioInicial = PresVsVenta.First().FechaInicial;
                    DateTime AnioFinal = PresVsVenta.First().fechafinal;


                    string titulo = "";
                    string datos = "";
                    string datos2 = "";
                    string datos3 = "";

                    for (var i = AnioInicial; i <= AnioFinal;)
                    {
                        int NumeroMes = AnioInicial.Month;
                        int anio = AnioInicial.Year;

                        List<CatPresupuesto> listaPresupuesto = (from tlist in PresVsVentaPresupuesto
                                                                 where tlist.Mes == NumeroMes
                                                                 && tlist.Anio == anio
                                                                 select tlist).ToList();

                        List<CatPresupuesto> listaPresVsVenta = (from tlist in PresVsVenta
                                                                 where tlist.Mes == NumeroMes
                                                                 && tlist.Anio == anio
                                                                 select tlist).ToList();
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

                        if (listaPresupuesto.Count() == 0)
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
                            if (datos2 == "")
                            {
                                datos2 = listaPresupuesto.Sum(x => x.TotalPresupuesto).ToString("F2");
                            }
                            else
                            {
                                datos2 = datos2 + "," + listaPresupuesto.Sum(x => x.TotalPresupuesto).ToString("F2");
                            }
                        }

                        if (listaPresVsVenta.Count() == 0)
                        {
                            if (datos == "")
                            {
                                datos = "0";
                            }
                            else
                            {
                                datos = datos + "," + "0";
                            }
                            if (datos3 == "")
                            {
                                datos3 = "0";
                            }
                            else
                            {
                                datos3 = datos3 + "," + "0";
                            }
                        }
                        else
                        {
                            if (datos == "")
                            {
                                datos = listaPresVsVenta.Sum(x => x.venta).ToString("F2");
                            }
                            else
                            {
                                datos = datos + "," + listaPresVsVenta.Sum(x => x.venta).ToString("F2");
                            }
                            if (datos3 == "")
                            {
                                datos3 = listaPresVsVenta.Sum(x => x.utilidadBruta).ToString("F2");
                            }
                            else
                            {
                                datos3 = datos3 + "," + listaPresVsVenta.Sum(x => x.utilidadBruta).ToString("F2");
                            }
                        }
                        AnioInicial = AnioInicial.AddMonths(1);
                        i = AnioInicial;
                    }

                    return JsonConvert.SerializeObject(new { id = 5, titulo = titulo, datos = datos, datos2 = datos2, datos3 = datos3 });
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
        public static string HistorialCliente(string idCte, string ter, string sucursal, string idRik)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];

                DateTime AnioFinal = DateTime.Now.AddMonths(-1);
                DateTime AnioInicial = AnioFinal.AddMonths(-12);

                CN_Presupuesto cdpresupuesto = new CN_Presupuesto();

                #region Historial

                DateTime FechaFinalHistoria = Convert.ToDateTime("01" + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year).AddMonths(-1);
                DateTime FechaIniciaHistorial = FechaFinalHistoria.AddMonths(-12);
                CatPresupuesto Presupuesto = new CatPresupuesto();
                Presupuesto.Id_Emp = Sesion.Id_Emp;
                Presupuesto.Id_Cd = int.Parse(sucursal);
                Presupuesto.MesInicial = AnioInicial.Month;
                Presupuesto.AnioInicial = AnioInicial.Year;
                Presupuesto.MesFinal = AnioFinal.Month;
                Presupuesto.AnioFinal = AnioFinal.Year;

                Presupuesto.FechaInicial = AnioInicial;
                Presupuesto.fechafinal = AnioFinal;
                Presupuesto.Id_Rik = Convert.ToInt32(idRik);
                Presupuesto.Id_u = Convert.ToInt32(idRik);

                Presupuesto.MesInicial = FechaIniciaHistorial.Month;
                Presupuesto.AnioInicial = FechaIniciaHistorial.Year;
                Presupuesto.MesFinal = FechaFinalHistoria.Month;
                Presupuesto.AnioFinal = FechaFinalHistoria.Year;

                Presupuesto.FechaInicial = FechaIniciaHistorial;
                Presupuesto.fechafinal = FechaFinalHistoria;
                Presupuesto.id_cte = Convert.ToInt32(idCte);
                Presupuesto.id_ter = Convert.ToInt32(ter);

                string conexion = ConfigurationManager.AppSettings.Get("strConnectionCentral");

                List<CatPresupuesto> ListaUtilidadHistorial = new List<CatPresupuesto>();
                List<CatPresupuesto> ListaPresupuestoClienteHistorial = new List<CatPresupuesto>();
                cdpresupuesto.ConsultaUtilidadCliente(Presupuesto, ref ListaUtilidadHistorial, conexion);


                #endregion

                string titulo = "";
                string datos = "";
                string datos2 = "";
                string datos3 = "";

                for (var i = AnioInicial; i <= AnioFinal;)
                {
                    int NumeroMes = AnioInicial.Month;
                    int anio = AnioInicial.Year;

                    List<CatPresupuesto> listaPresupuesto = (from tlist in ListaUtilidadHistorial
                                                             where tlist.Mes == NumeroMes
                                                             && tlist.Anio == anio
                                                             select tlist).ToList();

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

                    if (listaPresupuesto.Count() == 0)
                    {
                        if (datos == "")
                        {
                            datos = "0";
                        }
                        else
                        {
                            datos = datos + "," + "0";
                        }
                        if (datos2 == "")
                        {
                            datos2 = "0";
                        }
                        else
                        {
                            datos2 = datos2 + "," + "0";
                        }
                        if (datos3 == "")
                        {
                            datos3 = "0";
                        }
                        else
                        {
                            datos3 = datos3 + "," + "0";
                        }
                    }
                    else
                    {

                        if (datos == "")
                        {
                            datos = listaPresupuesto.Sum(x => x.utilidadBruta).ToString("F2");
                        }
                        else
                        {
                            datos = datos + "," + listaPresupuesto.Sum(x => x.utilidadBruta).ToString("F2");
                        }

                        if (datos2 == "")
                        {
                            datos2 = listaPresupuesto.Sum(x => x.utilidadBruta) > 0 ? (listaPresupuesto.Sum(x => x.utilidadBruta) / Convert.ToDouble(listaPresupuesto.Sum(x => x.venta)) * 100).ToString("F2") : "0";
                        }
                        else
                        {
                            datos2 = datos2 + "," + (listaPresupuesto.Sum(x => x.utilidadBruta) > 0 ? (listaPresupuesto.Sum(x => x.utilidadBruta) / Convert.ToDouble(listaPresupuesto.Sum(x => x.venta)) * 100).ToString("F2") : "0");
                        }

                        if (datos3 == "")
                        {
                            datos3 = listaPresupuesto.Sum(x => x.venta).ToString("F2");
                        }
                        else
                        {
                            datos3 = datos3 + "," + listaPresupuesto.Sum(x => x.venta).ToString("F2");
                        }
                    }


                    AnioInicial = AnioInicial.AddMonths(1);
                    i = AnioInicial;
                }

                return JsonConvert.SerializeObject(new { id = 5, titulo = titulo, datos = datos, datos2 = datos2, datos3 = datos3 });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = ex.Message });
            }
        }


        [WebMethod]
        public static string Historialglobal()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];

                DateTime AnioFinal = DateTime.Now.AddMonths(-1);
                DateTime AnioInicial = AnioFinal.AddMonths(-12);

                CN_Presupuesto cdpresupuesto = new CN_Presupuesto();

                #region Historial

                string idRik = HttpContext.Current.Session["RikGU"].ToString();
                string sucursal = HttpContext.Current.Session["SUcrusalGU"].ToString();

                DateTime FechaFinalHistoria = Convert.ToDateTime("01" + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year).AddMonths(-1);
                DateTime FechaIniciaHistorial = FechaFinalHistoria.AddMonths(-12);
                CatPresupuesto Presupuesto = new CatPresupuesto();
                Presupuesto.Id_Emp = Sesion.Id_Emp;
                Presupuesto.Id_Cd = int.Parse(sucursal);
                Presupuesto.MesInicial = AnioInicial.Month;
                Presupuesto.AnioInicial = AnioInicial.Year;
                Presupuesto.MesFinal = AnioFinal.Month;
                Presupuesto.AnioFinal = AnioFinal.Year;

                Presupuesto.FechaInicial = AnioInicial;
                Presupuesto.fechafinal = AnioFinal;
                Presupuesto.Id_Rik = Convert.ToInt32(idRik);
                Presupuesto.Id_u = Convert.ToInt32(idRik);

                Presupuesto.MesInicial = FechaIniciaHistorial.Month;
                Presupuesto.AnioInicial = FechaIniciaHistorial.Year;
                Presupuesto.MesFinal = FechaFinalHistoria.Month;
                Presupuesto.AnioFinal = FechaFinalHistoria.Year;

                Presupuesto.FechaInicial = FechaIniciaHistorial;
                Presupuesto.fechafinal = FechaFinalHistoria;
                Presupuesto.id_cte = -1;
                Presupuesto.id_ter = -1;

                string conexion = ConfigurationManager.AppSettings.Get("strConnectionCentral");

                List<CatPresupuesto> ListaUtilidadHistorial = new List<CatPresupuesto>();
                List<CatPresupuesto> ListaPresupuestoClienteHistorial = new List<CatPresupuesto>();
                cdpresupuesto.ConsultaUtilidadCliente(Presupuesto, ref ListaUtilidadHistorial, conexion);


                #endregion

                string titulo = "";
                string datos = "";
                string datos2 = "";
                string datos3 = "";

                for (var i = AnioInicial; i <= AnioFinal;)
                {
                    int NumeroMes = AnioInicial.Month;
                    int anio = AnioInicial.Year;

                    List<CatPresupuesto> listaPresupuesto = (from tlist in ListaUtilidadHistorial
                                                             where tlist.Mes == NumeroMes
                                                             && tlist.Anio == anio
                                                             select tlist).ToList();

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

                    if (listaPresupuesto.Count() == 0)
                    {
                        if (datos == "")
                        {
                            datos = "0";
                        }
                        else
                        {
                            datos = datos + "," + "0";
                        }
                        if (datos2 == "")
                        {
                            datos2 = "0";
                        }
                        else
                        {
                            datos2 = datos2 + "," + "0";
                        }
                        if (datos3 == "")
                        {
                            datos3 = "0";
                        }
                        else
                        {
                            datos3 = datos3 + "," + "0";
                        }
                    }
                    else
                    {

                        if (datos == "")
                        {
                            datos = listaPresupuesto.Sum(x => x.utilidadBruta).ToString("F2");
                        }
                        else
                        {
                            datos = datos + "," + listaPresupuesto.Sum(x => x.utilidadBruta).ToString("F2");
                        }

                        if (datos2 == "")
                        {
                            datos2 = listaPresupuesto.Sum(x => x.utilidadBruta) > 0 ? (listaPresupuesto.Sum(x => x.utilidadBruta) / Convert.ToDouble(listaPresupuesto.Sum(x => x.venta)) * 100).ToString("F2") : "0";
                        }
                        else
                        {
                            datos2 = datos2 + "," + (listaPresupuesto.Sum(x => x.utilidadBruta) > 0 ? (listaPresupuesto.Sum(x => x.utilidadBruta) / Convert.ToDouble(listaPresupuesto.Sum(x => x.venta)) * 100).ToString("F2") : "0");
                        }

                        if (datos3 == "")
                        {
                            datos3 = listaPresupuesto.Sum(x => x.venta).ToString("F2");
                        }
                        else
                        {
                            datos3 = datos3 + "," + listaPresupuesto.Sum(x => x.venta).ToString("F2");
                        }
                    }


                    AnioInicial = AnioInicial.AddMonths(1);
                    i = AnioInicial;
                }

                return JsonConvert.SerializeObject(new { id = 5, titulo = titulo, datos = datos, datos2 = datos2, datos3 = datos3 });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = ex.Message });
            }
        }


        #endregion 
    }
}