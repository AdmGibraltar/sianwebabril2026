using System;
using System.Collections.Generic;
using System.Web.UI;
using CapaDatos;
using CapaEntidad;
using CapaNegocios;
using DevExpress.Web.Bootstrap;
using DevExpress.XtraPrinting;
using DevExpress.Export;
using DevExpress.Web;

namespace SIANWEB
{
    public partial class CapListaPreciosConsulta : System.Web.UI.Page
    {
        protected void page_init(object sender, EventArgs e)
        {
            if (Session["dtListaPre"] != null)
            {

                List<ListaPrecios> ListaReporteCostos = new List<ListaPrecios>();
                ListaReporteCostos = (List<ListaPrecios>)Session["dtListaPre"];
                grdServicio.DataSource = ListaReporteCostos;
                grdServicio.DataBind();
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["dtListaPre"] != null)
                {
                    Session["dtListaPre"] = null;
                }
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion != null)
                {

                    cargarAplicaciones();
                    cargarSubFamilia();
                    cargarTipoProducto();
                    cargarProveedor();
                    cargarCampoLista();
                    cargarDatos();
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        public void cargarDatos()
        {


            Sesion gSession = (Sesion)Session["Sesion" + Session.SessionID];
            CN__Comun CN_Comun = new CN__Comun();

            int Id_Emp = gSession.Id_Emp;
            int Id_Cd = gSession.Id_Cd;


            string aplicacion = cmbAplicacion.Text.ToString();
            string subfamilia = cmbSubFamilia.Text.ToString();
            string tipoproducto = cmbTipoProducto.Text.ToString();
            string proveedor = cmbProveedor.Text.ToString();
            string clave = Text1.Text;
            string idprd = "";
            string strlista = cmbLista.Text.ToString();




            wsListaPrecios.wsListaPrecios wslista = new wsListaPrecios.wsListaPrecios();
            wsListaPrecios.ListaPrecios[] ListaFacturaTmp = wslista.GetPriceList("key", aplicacion, subfamilia, tipoproducto, proveedor, clave, strlista, idprd);


            List<ListaPrecios> ListaFactura = new List<ListaPrecios>();
            foreach (wsListaPrecios.ListaPrecios Item in ListaFacturaTmp)
            {
                ListaPrecios lista = new ListaPrecios();

                lista.Id_Prd = Item.Id_Prd;
                lista.Descripcion = Item.Descripcion;
                lista.ESTATUS = Item.ESTATUS;
                lista.APLICACION = Item.APLICACION;
                lista.SUBFAMILIA = Item.SUBFAMILIA;
                lista.TIPOPRODUCTO = Item.TIPOPRODUCTO;
                lista.LISTADEPRECIOS = Item.LISTADEPRECIOS;
                lista.NOPROVEEDOR = Item.NOPROVEEDOR;
                lista.NOMBREPROVEEDOR = Item.NOMBREPROVEEDOR;
                lista.NODEARTDEPROVEEDOR = Item.NODEARTDEPROVEEDOR;
                lista.PAAAACTUAL = Item.PAAAACTUAL;
                lista.PLISTAACTUAL = Item.PLISTAACTUAL;
                lista.PAAAAnterior = Item.PAAAAnterior;
                lista.PLISTAANTERIOR = Item.PLISTAANTERIOR;
                lista.PAAAFUTURA = Item.PAAAFUTURA;
                lista.PLISTAFUTURA = Item.PLISTAFUTURA;
                lista.PVariacionPAAA = Item.PVariacionPAAA;
                lista.PVariacionPLISTA = Item.PVariacionPLISTA;
                lista.FECHAINICIOVIG = Item.FECHAINICIOVIG.ToString() == "" ? (DateTime?)null : Item.FECHAINICIOVIG;
                lista.FECHAFINVIG = Item.FECHAFINVIG.ToString() == "" ? (DateTime?)null : Item.FECHAFINVIG;
                lista.FECHAINICIOVIGFUT = Item.FECHAINICIOVIGFUT.ToString() == "" ? (DateTime?)null : Item.FECHAINICIOVIGFUT;
                lista.FECHAFINVIGFUT = Item.FECHAFINVIGFUT.ToString() == "" ? (DateTime?)null : Item.FECHAFINVIGFUT;

                //lista.FECHAINICIOVIG = Item.FECHAINICIOVIG;
                //lista.FECHAFINVIG = Item.FECHAFINVIG;

                //lista.FECHAINICIOVIGFUT = Item.FECHAINICIOVIGFUT;
                //lista.FECHAFINVIGFUT = Item.FECHAFINVIGFUT;
                if (Item.TIENEPRECIOFUTURO == 0)
                    lista.TIENEPRECIOFUTURO = false;
                else
                    lista.TIENEPRECIOFUTURO = true;

                lista.RESPONSABLE = Item.RESPONSABLE;
                lista.PLANEACION = Item.PLANEACION;
                lista.margenred = Item.margenred; ;
                //23 junio 2021 requerimiento 
                lista.Presentacion = Item.Presentacion;
                lista.UnidaddeVenta = Item.UnidaddeVenta;
                lista.PVariacionPAAAFUTURO = Item.PVariacionPAAAFUTURO;
                lista.PVariacionPLISTAFUTURO = Item.PVariacionPLISTAFUTURO;
                //  esta es la liga original http://40.124.41.101/wslistaprecios/wsListaPrecios.asmx
                ListaFactura.Add(lista);
            }

            Session["dtListaPre"] = ListaFactura;
            grdServicio.DataSource = ListaFactura;
            grdServicio.DataBind();

        }

        protected void btnConsultar_ServerClick(object sender, EventArgs e)
        {
            cargarDatos();
        }

        protected void cargarAplicaciones()
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];


                wsListaPrecios.wsListaPrecios wslista = new wsListaPrecios.wsListaPrecios();
                wsListaPrecios.CatalogoListaPrecios[] ListaFacturaTmp = wslista.GetAplicacion("");


                cmbAplicacion.Items.Clear();
                CatalogoListaPrecios catalogo1 = new CatalogoListaPrecios();
                catalogo1.Id_SubCatalogo = -1;
                catalogo1.Descripcion_SubCatalogo = "";
                cmbAplicacion.Items.Add(catalogo1.Descripcion_SubCatalogo, catalogo1.Id_SubCatalogo);

                List<CatalogoListaPrecios> ListaFactura = new List<CatalogoListaPrecios>();
                foreach (wsListaPrecios.CatalogoListaPrecios Item in ListaFacturaTmp)
                {
                    CatalogoListaPrecios catalogo = new CatalogoListaPrecios();
                    catalogo.Id_SubCatalogo = Item.Id_SubCatalogo;
                    catalogo.Descripcion_SubCatalogo = Item.Descripcion_SubCatalogo;
                    cmbAplicacion.Items.Add(catalogo.Descripcion_SubCatalogo, catalogo.Id_SubCatalogo);

                    ListaFactura.Add(catalogo);
                }

                cmbAplicacion.SelectedIndex = 1;
            }
            catch (Exception ex)
            {

            }
        }

        protected void cmbAplicacion_Click(object sender, EventArgs e)
        {
            BootstrapComboBox box = sender as BootstrapComboBox;
            cargarSubFamilia();
            cargarDatos();

        }

        protected void cmbReload_Click(object sender, EventArgs e)
        {
            BootstrapComboBox box = sender as BootstrapComboBox;
            cargarDatos();
        }




        public void fTextChanged(object sender, EventArgs e)
        {
            cargarDatos();
        }


        protected void cargarSubFamilia()
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];



                wsListaPrecios.wsListaPrecios wslista = new wsListaPrecios.wsListaPrecios();
                wsListaPrecios.CatalogoListaPrecios[] ListaFacturaTmp = wslista.GetSubFamilias("", Convert.ToInt32(cmbAplicacion.Value.ToString()));


                cmbSubFamilia.Items.Clear();
                CatalogoListaPrecios catalogo1 = new CatalogoListaPrecios();
                catalogo1.Id_SubCatalogo = -1;
                catalogo1.Descripcion_SubCatalogo = "";
                cmbSubFamilia.Items.Add(catalogo1.Descripcion_SubCatalogo, catalogo1.Id_SubCatalogo);

                List<CatalogoListaPrecios> ListaCatalogo = new List<CatalogoListaPrecios>();
                foreach (wsListaPrecios.CatalogoListaPrecios Item in ListaFacturaTmp)
                {
                    CatalogoListaPrecios catalogo = new CatalogoListaPrecios();
                    catalogo.Id_SubCatalogo = Item.Id_SubCatalogo;
                    catalogo.Descripcion_SubCatalogo = Item.Descripcion_SubCatalogo;
                    cmbSubFamilia.Items.Add(catalogo.Descripcion_SubCatalogo, catalogo.Id_SubCatalogo);

                    ListaCatalogo.Add(catalogo);
                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void cargarTipoProducto()
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];



                wsListaPrecios.wsListaPrecios wslista = new wsListaPrecios.wsListaPrecios();
                wsListaPrecios.CatalogoListaPrecios[] ListaFacturaTmp = wslista.GetTipoProducto("");


                cmbTipoProducto.Items.Clear();
                CatalogoListaPrecios catalogo1 = new CatalogoListaPrecios();
                catalogo1.Id_SubCatalogo = -1;
                catalogo1.Descripcion_SubCatalogo = "";
                cmbTipoProducto.Items.Add(catalogo1.Descripcion_SubCatalogo, catalogo1.Id_SubCatalogo);

                List<CatalogoListaPrecios> ListaCatalogo = new List<CatalogoListaPrecios>();
                foreach (wsListaPrecios.CatalogoListaPrecios Item in ListaFacturaTmp)
                {
                    CatalogoListaPrecios catalogo = new CatalogoListaPrecios();
                    catalogo.Id_SubCatalogo = Item.Id_SubCatalogo;
                    catalogo.Descripcion_SubCatalogo = Item.Descripcion_SubCatalogo;
                    cmbTipoProducto.Items.Add(catalogo.Descripcion_SubCatalogo, catalogo.Id_SubCatalogo);

                    ListaCatalogo.Add(catalogo);
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void cargarProveedor()
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];



                wsListaPrecios.wsListaPrecios wslista = new wsListaPrecios.wsListaPrecios();
                wsListaPrecios.CatalogoListaPrecios[] ListaFacturaTmp = wslista.GetProveedor("");


                cmbProveedor.Items.Clear();
                CatalogoListaPrecios catalogo1 = new CatalogoListaPrecios();
                catalogo1.Id_SubCatalogo = -1;
                catalogo1.Descripcion_SubCatalogo = "";
                cmbProveedor.Items.Add(catalogo1.Descripcion_SubCatalogo, catalogo1.Id_SubCatalogo);


                List<CatalogoListaPrecios> ListaCatalogo = new List<CatalogoListaPrecios>();
                foreach (wsListaPrecios.CatalogoListaPrecios Item in ListaFacturaTmp)
                {
                    CatalogoListaPrecios catalogo = new CatalogoListaPrecios();
                    catalogo.Id_SubCatalogo = Item.Id_SubCatalogo;
                    catalogo.Descripcion_SubCatalogo = Item.Descripcion_SubCatalogo;
                    cmbProveedor.Items.Add(catalogo.Descripcion_SubCatalogo, catalogo.Id_SubCatalogo);

                    ListaCatalogo.Add(catalogo);
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void cargarCampoLista()
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];



                wsListaPrecios.wsListaPrecios wslista = new wsListaPrecios.wsListaPrecios();
                wsListaPrecios.CatalogoListaPrecios[] ListaFacturaTmp = wslista.GetCampoLista("");


                cmbLista.Items.Clear();
                CatalogoListaPrecios catalogo1 = new CatalogoListaPrecios();
                catalogo1.Id_SubCatalogo = -1;
                catalogo1.Descripcion_SubCatalogo = "";
                cmbLista.Items.Add(catalogo1.Descripcion_SubCatalogo, catalogo1.Id_SubCatalogo);


                List<CatalogoListaPrecios> ListaCatalogo = new List<CatalogoListaPrecios>();
                foreach (wsListaPrecios.CatalogoListaPrecios Item in ListaFacturaTmp)
                {
                    CatalogoListaPrecios catalogo = new CatalogoListaPrecios();
                    catalogo.Id_SubCatalogo = Item.Id_SubCatalogo;
                    catalogo.Descripcion_SubCatalogo = Item.Descripcion_SubCatalogo;
                    cmbLista.Items.Add(catalogo.Descripcion_SubCatalogo, catalogo.Id_SubCatalogo);

                    ListaCatalogo.Add(catalogo);
                }
            }
            catch (Exception ex)
            {

            }
        }


        #region Acciones de los botones

        protected void btnInformacion_ServerClick(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirModal", "AbrirModal2()", true);
            Response.Redirect("CapListaPreciosConsultaDet.aspx");

        }

        protected void btnHabilitar_ServerClick(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirModal", "AbrirModal2()", true);
            Response.Redirect("CapListaPreciosHabilitar.aspx");

        }

        protected void btnLimpiar_ServerClick(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirModal", "AbrirModal2()", true);
            Response.Redirect("CapListaPreciosConsulta.aspx");

        }




        protected void btnexportar_Click(object sender, EventArgs e)
        {
            switch (DemoExportFormat.Xlsx)
            {
                //case DemoExportFormat.Pdf:
                //    gridExport.WritePdfToResponse();
                //    break;

                case DemoExportFormat.Xlsx:
                    gridExport.WriteXlsxToResponse(new XlsxExportOptionsEx { ExportType = ExportType.WYSIWYG });
                    break;
                    //case DemoExportFormat.Rtf:
                    //    gridExport.WriteRtfToResponse();
                    //    break;
                    //case DemoExportFormat.Csv:
                    //    gridExport.WriteCsvToResponse(new CsvExportOptionsEx() { ExportType = ExportType.WYSIWYG });
                    //    break;
            }
        }


        public enum DemoExportFormat
        {
            Pdf = 0,
            Xls = 1,
            Xlsx = 2,
            Rtf = 3,
            Csv = 4
        }

        //Fomatear celdas en base a su nombre 
        #endregion



    }
}