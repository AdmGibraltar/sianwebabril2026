using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using CapaEntidad;
using CapaDatos;
using CapaNegocios;
using System.IO;
using System.Collections;
using System.Globalization;
using DevExpress.Export;
using DevExpress.XtraPrinting;
using System.Data;
using DevExpress.Web.Internal;
using System.ComponentModel;
using DevExpress.Export.Xl;
using DevExpress.Web.Bootstrap;
using DevExpress.Web;
using System.Diagnostics;
using SIANWEB.API;
using System.Configuration;
using DevExpress.Utils;
using System.Web.UI.WebControls;

namespace SIANWEB
{
    public partial class ProPedidoVI_Admin2 : System.Web.UI.Page
    {
        #region Variables
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Trim().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Trim().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Trim().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Trim().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Trim().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Trim().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Trim().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Trim().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        public List<PedidoVtaInst> ListPedidoVtaInst
        {
            get { return (List<PedidoVtaInst>)Session[Session.SessionID + "ListPedidoVtaInst"]; }
            set { Session[Session.SessionID + "ListPedidoVtaInst"] = value; }
        }
        public IList<Pedido_Internet> ListPedidoInternet
        {
            get { return (IList<Pedido_Internet>)Session[Session.SessionID + "ListPedidoInternet"]; }
            set { Session[Session.SessionID + "ListPedidoInternet"] = value; }
        }
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

        public DataTable dt
        {
            get
            {
                return (DataTable)ViewState["dtPedidoVI"];
            }
            set
            {
                ViewState["dtPedidoVI"] = value;

            }
        }
        public DataTable dt_Resto
        {
            get
            {
                return (DataTable)ViewState["dtPedidoVI_Resto"];
            }
            set
            {
                ViewState["dtPedidoVI_Resto"] = value;
            }
        }

        #endregion
        #region Eventos

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["gridpedidoVI"] != null)
            {
                List<PedidoVtaInst> List = new List<PedidoVtaInst>();

                List = (List<PedidoVtaInst>)Session["gridpedidoVI"];
                campos();
                ListPedidoVtaInst = List;
                gridpedidoVI.DataSource = List.ToList();
                gridpedidoVI.DataBind();

                lblCPEd.Text = List.Count().ToString();

            }
            if (Session["gridpedidoOrderCompra"] != null)
            {
                List<PedidoVtaInst> List = new List<PedidoVtaInst>();

                List = (List<PedidoVtaInst>)Session["gridpedidoOrderCompra"];
                campos();
                ListPedidoVtaInst = List;
                gridviewOrderCompra.DataSource = List.ToList();
                gridviewOrderCompra.DataBind();
            }
            if (Session["gridpedidoInternet"] != null)
            {
                List<Pedido_Internet> List2 = new List<Pedido_Internet>();

                List2 = (List<Pedido_Internet>)Session["gridpedidoInternet"];
                campos();
                ListPedidoInternet = List2;
                rgInternet.DataSource = List2.ToList();
                gridpedidoVI.DataBind();

                lblCPEdInt.Text = ListPedidoInternet.Count().ToString();
            }
            if (Session["gridpedidoOC"] != null)
            {
                var res = Session["gridpedidoOC"];
                campos();
                Session["gridpedidoOC"] = res;
                rgDatosOC.DataSource = res;
                rgDatosOC.DataBind();
            }
        }

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
                if (this.IsPostBack)
                {
                    TabName.Value = Request.Form[TabName.UniqueID];

                }
                if (!Page.IsPostBack)
                {
                    Session["gridpedidoVI"] = null;
                    Session["gridpedidoInternet"] = null;
                    Session["gridpedidoOC"] = null;
                    Session["gridpedidoOrderCompra"] = null;
                    ValidarPermisos();
                    if (session.Cu_Modif_Pass_Voluntario == false)
                    {
                        return;
                    }
                    PopulateGridFormatConditions();
                    Inicializar();
                    campos();

                    Random randObj = new Random(DateTime.Now.Millisecond);
                    HF_ClvPag.Value = randObj.Next().ToString().Trim();
                }
            }
        }


        protected void txt_OnTextChanged(object sender, EventArgs e)
        {
            if (ddlfrecuencia.SelectedItem.Value.ToString().Trim() == "1")
            {
                int anio = int.Parse(txtanio.Text);
                CN_CatSemana Semana = new CN_CatSemana();
                List<Semana> Lista = new List<CapaEntidad.Semana>();
                Semana.ConsultaSemana_Anio(session.Id_Emp, anio, session.Emp_Cnx, ref Lista);

                var query = (from tlist in Lista
                             select new
                             {
                                 id = tlist.Id_Sem,
                                 nombre = tlist.Id_Sem + " - " + tlist.Sem_FechaIni.ToString("dd/MM/yyyy") + "-" + tlist.Sem_FechaFin.ToString("dd/MM/yyyy")
                             }).ToList();
            }
        }

        protected void PopulateGridFormatConditions()
        {
            const string IconSetSpriteResourceName = "DevExpress.Web.Css.FCISprite.css";

            gridpedidoVI.FormatConditions.Clear();

            var condition1 = new GridViewFormatConditionHighlight
            {
                Rule = GridConditionRule.Equal,
                Value1 = "C",
                FieldName = "Acs_Vigencia"
            };
            condition1.Format = GridConditionHighlightFormat.Custom;
            condition1.CellStyle.CssClass = "dxFCRule dxWeb_fcIcons_TrafficLights3_1";
            gridpedidoVI.FormatConditions.Add(condition1);

            var condition4 = new GridViewFormatConditionHighlight
            {
                Rule = GridConditionRule.Equal,
                Value1 = 0,
                FieldName = "Acs_Vigencia"
            };
            condition4.Format = GridConditionHighlightFormat.Custom;
            condition4.CellStyle.CssClass = "dxFCRule dxWeb_fcIcons_TrafficLights3_3";
            gridpedidoVI.FormatConditions.Add(condition4);

            var condition2 = new GridViewFormatConditionHighlight { Rule = GridConditionRule.Equal, Value1 = 1, FieldName = "Acs_Vigencia" };
            condition2.Format = GridConditionHighlightFormat.Custom;
            condition2.CellStyle.CssClass = "dxFCRule dxWeb_fcIcons_TrafficLights3_2";
            gridpedidoVI.FormatConditions.Add(condition2);

            var condition3 = new GridViewFormatConditionHighlight { Rule = GridConditionRule.Equal, Value1 = 2, FieldName = "Acs_Vigencia" };
            condition3.Format = GridConditionHighlightFormat.Custom;
            condition3.CellStyle.CssClass = "dxFCRule dxWeb_fcIcons_TrafficLights3_1";
            gridpedidoVI.FormatConditions.Add(condition3);

            ResourceManager.RegisterCssResource(Page, typeof(ASPxWebControl), IconSetSpriteResourceName);
        }



        protected void detailGrid_DataSelect(object sender, EventArgs e)
        {
            var prod = (sender as BootstrapGridView).GetMasterRowKeyValue();

            if (Session["campos"] == null)
            {

                string[] array = prod.ToString().Trim().Split('|');

                // array[0] = valor del campo id_Acs
                // array[1] = valor del campo num pedido
                // array[2] = valor del campo semana
                // array[3] = valor del campo anio

                if (array[1].ToString().Trim() != "0")
                {
                    if (Session["campos"] == null)
                    {
                        string pedido = array[1].ToString().Trim();

                        BootstrapGridView grid = sender as BootstrapGridView;
                        GetListDet2();
                        DataTable dtTemp = dt;
                        DataTable dtRestos = dt_Resto;
                        CargarPedido(Convert.ToInt32(pedido), ref dtTemp, ref dtRestos);

                        dtTemp.Merge(dt_Resto);

                        Session["campos"] = prod;
                        grid.DataSource = dtTemp;
                        grid.DataBind();
                    }
                }
                else if (array[0].ToString().Trim() != "0")
                {
                    if (Session["campos"] == null)
                    {
                        string IdAcs = array[0].ToString().Trim();
                        string Semana = array[2].ToString().Trim();
                        string anio = array[3].ToString().Trim();

                        BootstrapGridView grid = sender as BootstrapGridView;
                        GetListDet2();
                        DataTable dtTemp = dt;
                        CargarProductoAcys(Convert.ToInt32(IdAcs), Convert.ToInt32(Semana), Convert.ToInt32(anio), ref dtTemp);
                        Session["campos"] = prod;
                        grid.DataSource = dtTemp;
                        grid.DataBind();
                    }
                }
            }
            else
            {
                Session["campos"] = null;
            }


        }


        protected void gridpedidoVIInternetProducto_BeforePerformDataSelect(object sender, EventArgs e)
        {

            var prod = (sender as BootstrapGridView).GetMasterRowKeyValue();

            if (Session["campos"] == null)
            {

                string[] array = prod.ToString().Trim().Split('|');

                // array[0] = valor del campo id_emp
                // array[1] = valor del campo id_Cd
                // array[2] = valor del campo num_requisicion
                // array[3] = valor del campo tipoPedido


                if (Session["campos"] == null)
                {
                    string pedido = array[2].ToString().Trim();
                    string tipopedido = array[3].ToString().Trim();

                    BootstrapGridView grid = sender as BootstrapGridView;
                    GetListDet2();
                    DataTable dtTemp = dt;
                    CargarPedidoInternet(Convert.ToInt32(pedido), Convert.ToInt32(tipopedido), ref dtTemp);
                    Session["campos"] = prod;
                    grid.DataSource = dtTemp;
                    grid.DataBind();
                }

            }
            else
            {
                Session["campos"] = null;
            }

        }


        protected void detailGrid_DataSelect2(object sender, EventArgs e)
        {

            var prod = (sender as BootstrapGridView).GetMasterRowKeyValue();

            if (Session["campos"] == null)
            {

                string[] array = prod.ToString().Trim().Split('|');

                // array[0] = valor del campo id_Acs
                // array[1] = valor del campo num pedido
                // array[2] = valor del campo semana
                // array[3] = valor del campo anio

                if (array[1].ToString().Trim() != "0")
                {
                    if (Session["campos"] == null)
                    {
                        string pedido = array[1].ToString().Trim();

                        BootstrapGridView grid = sender as BootstrapGridView;
                        GetListDet2();
                        DataTable dtTemp = dt;
                        DataTable dtRestos = dt_Resto;
                        CargarPedido(Convert.ToInt32(pedido), ref dtTemp, ref dtRestos);

                        dtTemp.Merge(dt_Resto);

                        Session["campos"] = prod;
                        grid.DataSource = dtTemp;
                        grid.DataBind();
                    }
                }
            }
            else
            {
                Session["campos"] = null;
            }


        }

        protected void ButtonXLS1_Click(object sender, EventArgs e)
        {
            //gridpedidoVI.ExportXlsToResponse(new XlsExportOptionsEx { ExportType = ExportType.WYSIWYG }); 

            TabName.Value = Request.Form[TabName.UniqueID];
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "mantaintabs('" + TabName.Value + "')", true);
        }



        protected void btnrechazarLista_Click(object sender, EventArgs e)
        {

            RechazarLista();

            TabName.Value = Request.Form[TabName.UniqueID];
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "mantaintabs('" + TabName.Value + "')", true);
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunctionEdit", "EditarPedido()", true);
        }

        protected void ButtonImprimir_Click(object sender, EventArgs e)
        {
            gridviewOrderCompra.ExportXlsToResponse(new XlsExportOptionsEx { ExportType = ExportType.WYSIWYG });

            TabName.Value = Request.Form[TabName.UniqueID];
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "mantaintabs('" + TabName.Value + "')", true);
        }

        protected void btnProtocolos_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunctionPro", "Protocolos()", true);
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunctionNew", "NuevoCliente('" + _PermisoGuardar + "', '" + _PermisoModificar + "')", true);
        }

        protected void btnAbrirDetalle_Click(object sender, EventArgs e)
        {
            string id = lblcte.InnerHtml;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirNuevoDetalles", "AbrirNuevoDetalles('" + _PermisoGuardar + "', '" + _PermisoModificar + "', '" + _PermisoEliminar + "', '" + _PermisoImprimir + "')", true);
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                List<PedidoVtaInst> List = new List<PedidoVtaInst>();
                List<PedidoVtaInst> List2 = new List<PedidoVtaInst>();
                GetList(ref List, ref List2);
                campos();
                gridpedidoVI.DataSource = List.ToList();
                gridpedidoVI.DataBind();

                gridviewOrderCompra.DataSource = List2.ToList();
                gridviewOrderCompra.DataBind();

                ListPedidoInternet = GetListInternet();
                rgInternet.DataSource = this.ListPedidoInternet;
                rgInternet.DataBind();


                lblCPEd.Text = List.Count().ToString();
                lblCPEdInt.Text = ListPedidoInternet.Count().ToString();

                CargarDatosOC();
            }
            catch (Exception ex)
            {
                mensaje("Error al realizar la consulta, " + ex.Message);
            }
        }

        protected void btnRechazar_ServerClick(object sender, EventArgs e)
        {

        }

        protected void btnImprimiOC_ServerClick(object sender, EventArgs e)
        {

        }


        protected void btnDownload_ServerClick(object sender, EventArgs e)
        {

            GridViewDataItemTemplateContainer c = ((System.Web.UI.HtmlControls.HtmlButton)sender).NamingContainer as GridViewDataItemTemplateContainer;
            string Num_pedido = c.Grid.GetRowValues(c.VisibleIndex, "pedido").ToString().Trim();

            Descargae(Convert.ToInt32(Num_pedido));

            TabName.Value = "tabPedidoOrdenCompra";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "mantaintabs('" + TabName.Value + "')", true);
        }



        protected void Button1_ServerClick1(object sender, EventArgs e)
        {

            GridViewDataItemTemplateContainer c = ((System.Web.UI.HtmlControls.HtmlButton)sender).NamingContainer as GridViewDataItemTemplateContainer;
            string ID_Acs = c.Grid.GetRowValues(c.VisibleIndex, "Id_Acs").ToString().Trim();
            string Num_pedido = c.Grid.GetRowValues(c.VisibleIndex, "pedido").ToString().Trim();
            string Estatus = c.Grid.GetRowValues(c.VisibleIndex, "Estatus").ToString().Trim();

            if (Estatus == "C")
            {
                Imprimir(Convert.ToInt32(Num_pedido));
            }
            else
            {
                mensaje("Unicamente se puede imprimir documentos con estatus de captado");
            }

            TabName.Value = "tabhome";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "mantaintabs('" + TabName.Value + "')", true);
        }

        protected void txtsemanainicial_DateChanged(object sender, EventArgs e)
        {

            try
            {
                Funciones funcion = new Funciones();
                CN_CatSemana Semana = new CN_CatSemana();
                List<Semana> Lista = new List<CapaEntidad.Semana>();
                if (txtsemanainicial.Text == "")
                {
                    txtsemanainicialnum.Value = "";
                    return;
                }
                Semana.ConsultaSemana_Anio(session.Id_Emp, int.Parse(txtanio.Text), session.Emp_Cnx, ref Lista);



                var query = (from tlist in Lista
                             where tlist.Sem_FechaIni <= DateTime.Parse(txtsemanainicial.Text)
                             orderby tlist.Id_Sem descending
                             select tlist

                             ).FirstOrDefault();

                if (query != null)
                {
                    txtsemanainicialnum.Value = query.Id_Sem.ToString().Trim();
                }
                campos();
            }
            catch (Exception ex)
            {
                mensaje("Error al realizar la consulta, " + ex.Message);
            }

        }

        protected void txtsemanafinal_DateChanged(object sender, EventArgs e)
        {

            try
            {
                Funciones funcion = new Funciones();
                CN_CatSemana Semana = new CN_CatSemana();
                List<Semana> Lista = new List<CapaEntidad.Semana>();
                if (txtsemanafinal.Text == "")
                {
                    txtsemanafinalnum.Value = "";
                    return;
                }
                Semana.ConsultaSemana_Anio(session.Id_Emp, int.Parse(txtanio.Text), session.Emp_Cnx, ref Lista);

                var query = (from tlist in Lista
                             where tlist.Sem_FechaIni <= DateTime.Parse(txtsemanafinal.Text)
                             orderby tlist.Id_Sem descending
                             select tlist

                             ).FirstOrDefault();

                if (query != null)
                {
                    txtsemanafinalnum.Value = query.Id_Sem.ToString().Trim();
                }
                campos();
            }
            catch (Exception ex)
            {
                mensaje("Error al realizar la consulta, " + ex.Message);
            }

        }

        protected void BtnOrdenCompra_ServerClick(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer c = ((System.Web.UI.HtmlControls.HtmlButton)sender).NamingContainer as GridViewDataItemTemplateContainer;
            string value = c.Grid.GetRowValues(c.VisibleIndex, "Id_Acs").ToString().Trim();
            string Estatus = c.Grid.GetRowValues(c.VisibleIndex, "Estatus").ToString().Trim();
            string Pedido = c.Grid.GetRowValues(c.VisibleIndex, "pedido").ToString().Trim();
            string semana = c.Grid.GetRowValues(c.VisibleIndex, "Acs_Semana").ToString().Trim();
            string anio = c.Grid.GetRowValues(c.VisibleIndex, "Acs_Anio").ToString().Trim();
            string id_cteDirEntrega = c.Grid.GetRowValues(c.VisibleIndex, "id_cteDirEntrega").ToString().Trim();

            int id = int.Parse(value);
            if (Estatus == "P")
            {
                CaptarOrderCompra(id, Estatus, Pedido, semana, anio, id_cteDirEntrega);
            }
            else
            {
                CaptarOrderCompraPedidoCaptados(id, Estatus, Pedido, semana, anio, id_cteDirEntrega);
            }

            TabName.Value = Request.Form[TabName.UniqueID];
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "mantaintabs('" + TabName.Value + "')", true);
        }

        protected void Button2_ServerClick(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer c = ((System.Web.UI.HtmlControls.HtmlButton)sender).NamingContainer as GridViewDataItemTemplateContainer;
            string value = c.Grid.GetRowValues(c.VisibleIndex, "Id_Acs").ToString().Trim();
            string Estatus = c.Grid.GetRowValues(c.VisibleIndex, "Estatus").ToString().Trim();
            string Pedido = c.Grid.GetRowValues(c.VisibleIndex, "pedido").ToString().Trim();

          
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirRastreo", "modalRastreo('" + value + "',  '" + Pedido + "')", true);
          

            TabName.Value = "tabhome";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "mantaintabs('" + TabName.Value + "')", true);
        }

        protected void Button1_ServerClick2(object sender, EventArgs e)
        {

            GridViewDataItemTemplateContainer c = ((System.Web.UI.HtmlControls.HtmlButton)sender).NamingContainer as GridViewDataItemTemplateContainer;
            string ID_Acs = c.Grid.GetRowValues(c.VisibleIndex, "Id_Acs").ToString().Trim();
            string Num_pedido = c.Grid.GetRowValues(c.VisibleIndex, "pedido").ToString().Trim();
            string Estatus = c.Grid.GetRowValues(c.VisibleIndex, "Estatus").ToString().Trim();

            if (Estatus == "C")
            {
                Imprimir(Convert.ToInt32(Num_pedido));
            }
            else
            {
                mensaje("Unicamente se puede imprimir documentos con estatus de captado");
            }

            TabName.Value = "tabPedidoOrdenCompra";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "mantaintabs('" + TabName.Value + "')", true);
        }

        protected void Button2_ServerClick1(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer c = ((System.Web.UI.HtmlControls.HtmlButton)sender).NamingContainer as GridViewDataItemTemplateContainer;
            string value = c.Grid.GetRowValues(c.VisibleIndex, "Id_Acs").ToString().Trim();
            string Estatus = c.Grid.GetRowValues(c.VisibleIndex, "Estatus").ToString().Trim();
            string Pedido = c.Grid.GetRowValues(c.VisibleIndex, "pedido").ToString().Trim();

             
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirRastreo", "modalRastreo('" + value + "',  '" + Pedido + "')", true);
            

            TabName.Value = "tabPedidoOrdenCompra";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "mantaintabs('" + TabName.Value + "')", true);
        }

        protected void BtnUrlDownload_ServerClick(object sender, EventArgs e)
        {

            GridViewDataItemTemplateContainer c = ((System.Web.UI.HtmlControls.HtmlButton)sender).NamingContainer as GridViewDataItemTemplateContainer;

            string Pedido = c.Grid.GetRowValues(c.VisibleIndex, "tipoPedido").ToString().Trim();
            string URL = c.Grid.GetRowValues(c.VisibleIndex, "Url").ToString().Trim();

            if (Pedido == "1")
            {
                if (URL != "N/A")
                {
                    System.Diagnostics.Process.Start(URL);
                }
                else
                {
                    mensaje("No hay url Orden de compra para el pedido");
                }
            }
            else
            {
                mensaje("No hay Orden de compra para el pedido");
            }

            TabName.Value = "tabPedidoOrdenCompra";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "mantaintabs('" + TabName.Value + "')", true);


        }

        protected void ButtonReporte_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                mensaje("Error al realizar la consulta, " + ex.Message);
            }

            TabName.Value = Request.Form[TabName.UniqueID];
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "mantaintabs('" + TabName.Value + "')", true);
        }



        protected void btnconsultarSemana_Click(object sender, EventArgs e)
        {
            try
            {
                Funciones funcion = new Funciones();
                CN_CatSemana Semana = new CN_CatSemana();
                List<Semana> Lista = new List<CapaEntidad.Semana>();
                if (rdSemana.Text == "")
                {
                    textIdSemana.Value = "";
                    return;
                }
                Semana.ConsultaSemana_Anio(session.Id_Emp, int.Parse(txtanio.Text), session.Emp_Cnx, ref Lista);



                var query = (from tlist in Lista
                             where tlist.Sem_FechaIni <= DateTime.Parse(rdSemana.Text)
                             orderby tlist.Id_Sem descending
                             select tlist

                             ).FirstOrDefault();

                if (query != null)
                {
                    textIdSemana.Value = query.Id_Sem.ToString().Trim();
                }
                campos();
            }
            catch (Exception ex)
            {
                mensaje("Error al realizar la consulta, " + ex.Message);
            }
        }

        protected void btnconusltareditarPedido_Click(object sender, EventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Pedido pedido = new Pedido();

                pedido.Id_Emp = sesion.Id_Emp;
                pedido.Id_Cd = sesion.Id_Cd_Ver;
                pedido.Id_Ped = Convert.ToInt32(hdneditarPedido.Value);

                CN_CapPedido cn_capPedido = new CN_CapPedido();
                if (!cn_capPedido.ConsultaPedidoFacturacion(ref pedido, sesion.Emp_Cnx))//pedido no existe
                {

                    mensaje("El pedido no existe");
                }
                else
                {
                    string[] estatus = { "R", "X", "A", "U", "I", "C" };


                    if (!estatus.Contains(pedido.Estatus))
                    // != "A" && pedido.Estatus != "U" && pedido.Estatus != "I" && pedido.Estatus != "C")
                    {
                        //si el pedido no se encuentra en estatus asignado o autorizado  
                        mensaje("El pedido se encuentra en estatus no válido para realizar la modificación");
                    }
                    else
                    {
                        CN_CapPedidoVtaInst CN = new CN_CapPedidoVtaInst();
                        List<PedidoVtaInst> List = new List<PedidoVtaInst>();
                        PedidoVtaInst datos = new PedidoVtaInst();
                        datos.Id_Emp = sesion.Id_Emp;
                        datos.Id_Cd = sesion.Id_Cd_Ver;
                        datos.Id_Ped = Convert.ToInt32(hdneditarPedido.Value);
                        string modalidadVenta = "";
                        CN.ConsultarPedido(datos, sesion.Emp_Cnx, ref List, modalidadVenta);

                        if (List.Count > 0)
                        {
                            if (List.First().str_Tipo_pedido == "Pedidos Venta Instalada" || List.First().str_Tipo_pedido == "Pedidos Orden de Compra" || List.First().str_Tipo_pedido == "Pedidos Venta nuevo y / o esporadicos")
                            {
                                Session["PedidoCaptado" + Session.SessionID] = hdneditarPedido.Value;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirDetalles", "modalDetallePedido('" + hdneditarPedido.Value + "', '" + _PermisoGuardar + "', '" + _PermisoModificar + "', '" + _PermisoEliminar + "', '" + _PermisoImprimir + "')", true);

                            }
                            if (List.First().str_Tipo_pedido == "Pedidos Portal de Cliente" || List.First().str_Tipo_pedido == "Pedidos Internet")
                            {
                                int verificador = 0;
                                Factura factura = new Factura();

                                factura.Id_Emp = sesion.Id_Emp;
                                factura.Id_Cd = sesion.Id_Cd_Ver;
                                factura.Fac_PedNum = Convert.ToInt32(hdneditarPedido.Value);

                                string tipoPedido = pedido.Ped_Tipo.ToString();
                                string numPedido = hdneditarPedido.Value;

                                List<Factura> lista = new List<Factura>();

                                CN_Eccommerce CNProFactura_Embarque = new CN_Eccommerce();
                                CNProFactura_Embarque.ValidarPedidoFacturaEcommerce(factura, sesion.Emp_Cnx, ref lista);

                                if (lista.Count > 0)
                                {

                                    string Pedido = lista.First().Id_Ped.ToString();
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirVentana_ProPedido_Internet", "AbrirVentana_ProPedido_Internet('" + Pedido + "', '" + tipoPedido + "',  '" + numPedido + "',  '" + _PermisoGuardar + "', '" + _PermisoModificar + "', '" + _PermisoEliminar + "', '" + _PermisoImprimir + "',2020, '1')", true);


                                }
                                else
                                {
                                    mensaje("No se pueden editar los pedidos de internet");
                                }

                            }
                            if (List.First().str_Tipo_pedido == "Pedidos Orden Centralizada")
                            {

                                Session["PedidoCaptado" + Session.SessionID] = hdneditarPedido.Value;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirDetalles", "modalDetallePedidoOC('" + hdneditarPedido.Value + "', '" + _PermisoGuardar + "', '" + _PermisoModificar + "', '" + _PermisoEliminar + "', '" + _PermisoImprimir + "')", true);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje("Error al realizar la consulta, " + ex.Message);
            }

            TabName.Value = Request.Form[TabName.UniqueID];
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "mantaintabs('" + TabName.Value + "')", true);
        }


        protected void btnconusltar_Click(object sender, EventArgs e)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                PedidoVtaInst pedido = new PedidoVtaInst();
                List<PedidoVtaInst> lista = new List<CapaEntidad.PedidoVtaInst>();
                CN_CapPedidoVtaInst vtaInst = new CN_CapPedidoVtaInst();
                pedido.Id_Emp = sesion.Id_Emp;
                pedido.Id_Cd = sesion.Id_Cd_Ver;
                pedido.Id_Cte = Convert.ToInt32(hdnidcliente.Value);

                CN_CapPedido cn_capPedido = new CN_CapPedido();

                vtaInst.ConsultaClienteAcys(pedido, ref lista, sesion.Emp_Cnx);
                if (lista.Count == 0)//pedido no existe
                {
                    mensaje("El cliente no existe");
                }
                else if ((lista.OrderByDescending(x => x.Id_Acs).First().Id_Acs != 0) && (ConfigurationManager.AppSettings["ValidaAcys"].ToString() != "N"))
                {
                    var acuerdo = "El cliente ya cuenta con un acuerdo. </br> Numero de acuerdo: " + lista.OrderByDescending(x => x.Id_Acs).First().Id_Acs;
                    mensaje(acuerdo);
                }
                else
                {
                    Session["PedidoCaptado" + Session.SessionID] = hdnidcliente.Value;
                    string mensaje = string.Empty;
                    //cerrar ventana radWindow de detalle de pedido 


                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirDetalles", "modalDetalle('" + _PermisoGuardar + "', '" + _PermisoModificar + "', '" + _PermisoEliminar + "', '" + _PermisoImprimir + "', '" + HD_Anio.Value + "'," + HD_Semana.Value + ")", true);
                }
            }
            catch (Exception ex)
            {
                mensaje("Error al realizar la consulta, " + ex.Message);
            }

            TabName.Value = Request.Form[TabName.UniqueID];
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "mantaintabs('" + TabName.Value + "')", true);
        }


        protected void btnCaptarPInternet_ServerClick(object sender, EventArgs e)
        {

            GridViewDataItemTemplateContainer c = ((System.Web.UI.HtmlControls.HtmlButton)sender).NamingContainer as GridViewDataItemTemplateContainer;


            string Pedido = c.Grid.GetRowValues(c.VisibleIndex, "Num_Pedido").ToString().Trim();
            string credito = c.Grid.GetRowValues(c.VisibleIndex, "Cte_Credito").ToString().Trim();
            string Estatus = c.Grid.GetRowValues(c.VisibleIndex, "Estatus_Nombre").ToString().Trim();
            string IdCte = c.Grid.GetRowValues(c.VisibleIndex, "Id_Cte").ToString().Trim();
            string tipoPedido = c.Grid.GetRowValues(c.VisibleIndex, "tipoPedido").ToString().Trim();
            string URL = c.Grid.GetRowValues(c.VisibleIndex, "Url").ToString().Trim();
            //string tipoPedido = "0";


            if (Estatus == "Captado")
            {
                mensaje("Pedido de Internet ya captado");
                return;
            }

            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            PedidoVtaInst pedido = new PedidoVtaInst();
            CN_CapPedidoVtaInst vtaInst = new CN_CapPedidoVtaInst();
            pedido.Id_Emp = sesion.Id_Emp;
            pedido.Id_Cd = sesion.Id_Cd_Ver;
            pedido.Id_Cte = int.Parse(IdCte);
            vtaInst.Cliente_credito(ref pedido, sesion.Emp_Cnx);
            bool CreditoSusp = Convert.ToBoolean(pedido.Cte_Credito);

            if (CreditoSusp)
            {

                mensaje("Este cliente tiene el crédito suspendido");
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirVentana_ProPedido_Internet", "AbrirVentana_ProPedido_InternetInsert('" + Pedido + "', '" + tipoPedido + "' , '" + URL + "', '" + _PermisoGuardar + "', '" + _PermisoModificar + "', '" + _PermisoEliminar + "', '" + _PermisoImprimir + "',2020, '1')", true);

            }

            TabName.Value = Request.Form[TabName.UniqueID];
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "mantaintabs('" + TabName.Value + "')", true);
        }


        protected void BtnRemisionOC_ServerClick(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer c = ((System.Web.UI.HtmlControls.HtmlButton)sender).NamingContainer as GridViewDataItemTemplateContainer;
            string Pedido = c.Grid.GetRowValues(c.VisibleIndex, "Id").ToString().Trim();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirVentana_VerRemisiones_OC", "AbrirVentana_VerRemisiones_OC('" + Pedido + "')", true);

            TabName.Value = Request.Form[TabName.UniqueID];
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "mantaintabs('" + TabName.Value + "')", true);


        }

        protected void CaptarImgInternetOC_ServerClick(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer c = ((System.Web.UI.HtmlControls.HtmlButton)sender).NamingContainer as GridViewDataItemTemplateContainer;

            string Estatus = c.Grid.GetRowValues(c.VisibleIndex, "Estatus").ToString().Trim();
            string EstatusSOl = c.Grid.GetRowValues(c.VisibleIndex, "EstatusSOl").ToString().Trim();
            string id_sol = c.Grid.GetRowValues(c.VisibleIndex, "ID_sol").ToString().Trim();
            string Pedido = c.Grid.GetRowValues(c.VisibleIndex, "Id").ToString().Trim();
            string IdCte = c.Grid.GetRowValues(c.VisibleIndex, "Id_Cte").ToString().Trim();
            if (Estatus != "P")
            {
                mensaje("Solo se pueden captar pedidos con estatus pendiente");
            }
            else
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                PedidoVtaInst pedido = new PedidoVtaInst();
                CN_CapPedidoVtaInst vtaInst = new CN_CapPedidoVtaInst();
                pedido.Id_Emp = sesion.Id_Emp;
                pedido.Id_Cd = sesion.Id_Cd_Ver;
                pedido.Id_Cte = int.Parse(IdCte);
                vtaInst.Cliente_credito(ref pedido, sesion.Emp_Cnx);
                bool CreditoSusp = Convert.ToBoolean(pedido.Cte_Credito);

                if (CreditoSusp)
                {

                    mensaje("Este cliente tiene el crédito suspendido");
                    return;
                }

                if (EstatusSOl == "N")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirVentana_ProPedido_OC", "AbrirVentana_ProPedido_OC('" + Pedido + "', '" + _PermisoGuardar + "', '" + _PermisoModificar + "', '" + _PermisoEliminar + "', '" + _PermisoImprimir + "'," + "2000" + ", '" + "1" + "')", true);
                }
                else if (EstatusSOl == "P")
                {
                    mensaje("Pendiente a autorización");
                    return;
                }
                else if (EstatusSOl == "R")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirVentana_ProPedido_OC", "AbrirVentana_ProPedido_OC('" + Pedido + "', '" + _PermisoGuardar + "', '" + _PermisoModificar + "', '" + _PermisoEliminar + "', '" + _PermisoImprimir + "'," + "2000" + ", '" + "1" + "')", true);

                }
                else if (EstatusSOl == "A")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirVentana_ProPedido_OC", "AbrirVentana_ProPedido_OCAutorizacion('" + id_sol + "', '" + _PermisoGuardar + "', '" + _PermisoModificar + "', '" + _PermisoEliminar + "', '" + _PermisoImprimir + "'," + "2000" + ", '" + "1" + "')", true);
                }
            }
        }

        protected void Button1_ServerClick(object sender, EventArgs e)
        {

            GridViewDataItemTemplateContainer c = ((System.Web.UI.HtmlControls.HtmlButton)sender).NamingContainer as GridViewDataItemTemplateContainer;

            string Num_pedido = c.Grid.GetRowValues(c.VisibleIndex, "Num_Pedido").ToString().Trim();
            string Estatus = c.Grid.GetRowValues(c.VisibleIndex, "Estatus_Id").ToString().Trim();
            string Id_Pedido = c.Grid.GetRowValues(c.VisibleIndex, "Id_Ped").ToString().Trim();

            if (Estatus == "C")
            {
                Imprimir(Convert.ToInt32(Id_Pedido));
            }
            else
            {
                mensaje("Unicamente se puede imprimir documentos con estatus de captado");
            }

            TabName.Value = "tabInternet";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "mantaintabs('" + TabName.Value + "')", true);
        }
        #endregion
        #region Funciones

        private void campos()
        {
            DivTodos.Style.Remove("display");
            DivSemana.Style.Remove("display");
            DivMes.Style.Remove("display");
            DivMeses.Style.Remove("display");

            string frecuencia = ddlfrecuencia.SelectedItem == null ? "0" : ddlfrecuencia.SelectedItem.Value.ToString().Trim();
            switch (frecuencia)
            {
                case "0":
                    DivSemana.Style.Add("display", "none");
                    DivMes.Style.Add("display", "none");
                    DivMeses.Style.Add("display", "none");
                    break;
                case "1":
                    DivTodos.Style.Add("display", "none");
                    DivMes.Style.Add("display", "none");
                    DivMeses.Style.Add("display", "none");
                    break;
                case "2":
                    DivTodos.Style.Add("display", "none");
                    DivSemana.Style.Add("display", "none");
                    DivMeses.Style.Add("display", "none");
                    break;
                case "3":
                    DivTodos.Style.Add("display", "none");
                    DivSemana.Style.Add("display", "none");
                    DivMes.Style.Add("display", "none");
                    break;
                case "4":
                    DivTodos.Style.Add("display", "none");
                    DivSemana.Style.Add("display", "none");
                    DivMes.Style.Add("display", "none");
                    break;
                case "5":
                    DivTodos.Style.Add("display", "none");
                    DivSemana.Style.Add("display", "none");
                    DivMes.Style.Add("display", "none");
                    break;
            }
        }


        private void cargarComboClientesAcuerdo()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            PedidoVtaInst pedido = new PedidoVtaInst();

            List<PedidoVtaInst> lista = new List<CapaEntidad.PedidoVtaInst>();

            CN_CapPedidoVtaInst vtaInst = new CN_CapPedidoVtaInst();
            pedido.Id_Emp = sesion.Id_Emp;
            pedido.Id_Cd = sesion.Id_Cd_Ver;
            CN_CapPedido cn_capPedido = new CN_CapPedido();
            vtaInst.ConsultaClienteAcysCombo(pedido, ref lista, sesion.Emp_Cnx);

            var query = (from tlist in lista
                         group tlist by tlist.Id_Cte into g
                         select new
                         {
                             Id_Cte = g.Key,
                             Acs_NomComercial = g.Key + " - " + g.Select(x => x.Acs_NomComercial).FirstOrDefault()
                         }).ToList().OrderBy(x => x.Id_Cte);

            ddlRazonSocialCliente.DataSource = query;
            ddlRazonSocialCliente.ValueField = "Id_Cte";
            ddlRazonSocialCliente.TextField = "Acs_NomComercial";
            ddlRazonSocialCliente.DataBind();

            ddlRazonSocialCliente.Items.Insert(0, new DevExpress.Web.Bootstrap.BootstrapListEditItem("--Todos--", -1));
            ddlRazonSocialCliente.Value = "--Todos--";
        }

        private void cargarComboClientes()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            PedidoVtaInst pedido = new PedidoVtaInst();
            List<PedidoVtaInst> lista = new List<CapaEntidad.PedidoVtaInst>();
            CN_CapPedidoVtaInst vtaInst = new CN_CapPedidoVtaInst();
            pedido.Id_Emp = sesion.Id_Emp;
            pedido.Id_Cd = sesion.Id_Cd_Ver;
            CN_CapPedido cn_capPedido = new CN_CapPedido();
            vtaInst.ConsultaClienteAcysCombo(pedido, ref lista, sesion.Emp_Cnx);

            var query = (from tlist in lista
                         group tlist by tlist.Id_Cte into g
                         select new
                         {
                             Id_Cte = g.Key,
                             Acs_NomComercial = g.Key + " - " + g.Select(x => x.Acs_NomComercial).FirstOrDefault()
                         }).ToList();

            ddlRazonSocialCliente.DataSource = query;
            ddlRazonSocialCliente.ValueField = "Id_Cte";
            ddlRazonSocialCliente.TextField = "Acs_NomComercial";
            ddlRazonSocialCliente.DataBind();
        }

        private void Inicializar()
        {
            CapaNegocios.CN_CatTiposUsuario cnTipoUsuario = new CN_CatTiposUsuario();
            cargarComboClientesAcuerdo();
            Funciones funcion = new Funciones();
            Semana semana = new Semana();
            CN_CatSemana Semana = new CN_CatSemana();
            List<Semana> Lista = new List<CapaEntidad.Semana>();
            semana.Sem_FechaAct = funcion.GetLocalDateTime(session.Minutos);
            semana.Id_Emp = session.Id_Emp;
            semana.Id_Cd = session.Id_Cd_Ver;
            CN_CatSemana cn_semana = new CN_CatSemana();
            txtanio.Text = funcion.GetLocalDateTime(session.Minutos).Year.ToString().Trim();
            int verificador = 0;
            cn_semana.ConsultaSemanaActual(ref semana, session.Emp_Cnx, ref verificador);

            txtsemanainicial.Value = session.CalendarioIni;
            txtsemanafinal.Value = session.CalendarioFin;


            Semana.ConsultaSemana_Anio(session.Id_Emp, int.Parse(txtanio.Text), session.Emp_Cnx, ref Lista);
            var query = (from tlist in Lista
                         where tlist.Sem_FechaIni <= DateTime.Parse(txtsemanainicial.Text)
                         orderby tlist.Id_Sem descending
                         select tlist
                      ).FirstOrDefault();

            var query2 = (from tlist in Lista
                          where tlist.Sem_FechaIni <= DateTime.Parse(txtsemanafinal.Text)
                          orderby tlist.Id_Sem descending
                          select tlist

                     ).FirstOrDefault();

            if (query != null)
            {
                txtsemanainicialnum.Value = query.Id_Sem.ToString().Trim();
            }

            if (query2 != null)
            {
                txtsemanafinalnum.Value = query2.Id_Sem.ToString().Trim();
            }




            List<TipoUsuario> vTipoUsList = new List<TipoUsuario>();

            TipoUsuario vTipoUs = new TipoUsuario()
            {
                Id_Emp = session.Id_Emp
            };

            cnTipoUsuario.ConsultaTiposDeUsuario(vTipoUs, session.Emp_Cnx, ref vTipoUsList);

            int pedidosVI = 0;



            if (verificador > 0 && semana.Id_Sem != 0)
            {
                List<PedidoVtaInst> List = new List<PedidoVtaInst>();
                List<PedidoVtaInst> List2 = new List<PedidoVtaInst>();
                HD_Anio.Value = funcion.GetLocalDateTime(session.Minutos).Year.ToString().Trim();
                HD_Semana.Value = semana.Id_Sem.ToString().Trim();

                GetList(ref List, ref List2);

                pedidosVI = List.Count();
                lblCPEd.Text = pedidosVI.ToString();
                gridpedidoVI.DataSource = List.ToList();
                gridpedidoVI.DataBind();

                gridviewOrderCompra.DataSource = List2.ToList();
                gridviewOrderCompra.DataBind();


                ListPedidoInternet = GetListInternet();
                lblCPEdInt.Text = ListPedidoInternet.Count().ToString();
                rgInternet.DataSource = this.ListPedidoInternet;
                rgInternet.DataBind();

                CargarDatosOC();
            }

            string mensajes = "";

            if (mensajes != "")
            {
                mensaje(mensajes);
            }
        }




        public DataTable ConvertToDataTable<T>(IList<T> data)
        {

            PropertyDescriptorCollection properties =

            TypeDescriptor.GetProperties(typeof(T));

            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in properties)

                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (T item in data)
            {

                DataRow row = table.NewRow();

                foreach (PropertyDescriptor prop in properties)

                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;

                table.Rows.Add(row);

            }

            return table;

        }

        private void ImprimirPendientes(ref string mappath)
        {
            try
            {
                List<PedidoVtaInst> List = new List<PedidoVtaInst>();

                GetListPendientes(ref List);

                // Create an exporter instance.  
                IXlExporter exporter = XlExport.CreateExporter(XlDocumentFormat.Xlsx);

                mappath = HttpContext.Current.Server.MapPath("~/Reportes/RerpoteCaptacionPedidos" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx");
                // Create the FileStream object with the specified file path.  

                if (System.IO.File.Exists(mappath))
                    System.IO.File.Delete(mappath);


                using (FileStream stream = new FileStream(mappath, FileMode.Create, FileAccess.ReadWrite))
                {
                    // Create a new document and begin to write it to the specified stream.  
                    using (IXlDocument document = exporter.CreateDocument(stream))
                    {
                        // Add a new worksheet to the document.  
                        using (IXlSheet sheet = document.CreateSheet())
                        {
                            // Specify the worksheet name. 
                            sheet.Name = "Reporte de Pedidos";

                            // Create the first column and set its width.  
                            using (IXlColumn column = sheet.CreateColumn())
                            {
                                column.WidthInPixels = 100;
                            }

                            // Create the second column and set its width. 
                            using (IXlColumn column = sheet.CreateColumn())
                            {
                                column.WidthInPixels = 450;
                            }

                            // Create the third column and set the specific number format for its cells. 
                            using (IXlColumn column = sheet.CreateColumn())
                            {
                                column.WidthInPixels = 100;
                            }


                            using (IXlColumn column = sheet.CreateColumn())
                            {
                                column.WidthInPixels = 100;
                            }

                            using (IXlColumn column = sheet.CreateColumn())
                            {
                                column.WidthInPixels = 100;
                            }

                            using (IXlColumn column = sheet.CreateColumn())
                            {
                                column.WidthInPixels = 150;
                            }

                            using (IXlColumn column = sheet.CreateColumn())
                            {
                                column.WidthInPixels = 150;
                            }

                            using (IXlColumn column = sheet.CreateColumn())
                            {
                                column.WidthInPixels = 100;
                            }

                            using (IXlColumn column = sheet.CreateColumn())
                            {
                                column.WidthInPixels = 100;
                            }


                            // Specify cell font attributes. 
                            XlCellFormatting cellFormatting = new XlCellFormatting();
                            cellFormatting.Font = new XlFont();
                            cellFormatting.Font.Name = "Century Gothic";
                            cellFormatting.Font.SchemeStyle = XlFontSchemeStyles.None;

                            // Specify formatting settings for the header row. 
                            XlCellFormatting headerRowFormatting = new XlCellFormatting();
                            headerRowFormatting.CopyFrom(cellFormatting);
                            headerRowFormatting.Font.Bold = true;
                            headerRowFormatting.Fill = XlFill.SolidFill(XlColor.FromTheme(XlThemeColor.Accent2, 0.0));


                            // Create the header row. 
                            using (IXlRow row = sheet.CreateRow())
                            {
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Num. Cte";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Cliente";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Terr.";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Venta Instalada";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Semana";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Núm. Pedido";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Estatus";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Mes";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Año";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                            }

                            // Generate data for the sales report.  
                            for (int i = 0; i < List.Count; i++)
                            {
                                using (IXlRow row = sheet.CreateRow())
                                {
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Id_Cte;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Cte_Nom;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Id_Ter;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Acs_Cantidad;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Acs_Semana;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].pedido;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Acs_EstatusStr;
                                        cell.ApplyFormatting(cellFormatting);
                                    }

                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Acs_Mes;
                                        cell.ApplyFormatting(cellFormatting);
                                    }

                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Acs_Anio;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                }
                            }

                            // Enable AutoFilter for the created cell range. 
                            sheet.AutoFilterRange = sheet.DataRange;
                            // Specify formatting settings for the total row. 
                            XlCellFormatting totalRowFormatting = new XlCellFormatting();
                            totalRowFormatting.CopyFrom(cellFormatting);
                            totalRowFormatting.Font.Bold = true;
                            totalRowFormatting.Fill = XlFill.SolidFill(XlColor.FromTheme(XlThemeColor.Accent5, 0.6));


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje(ex.Message);
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
                _PermisoGuardar = Permiso.PGrabar;
                _PermisoModificar = Permiso.PModificar;
                _PermisoEliminar = Permiso.PEliminar;
                _PermisoImprimir = Permiso.PImprimir;

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
        private void GetList(ref List<PedidoVtaInst> List, ref List<PedidoVtaInst> List2)
        {
            try
            {
                string mesNombre = "";
                Funciones Funcion = new Funciones();

                CN_CapPedidoVtaInst clsCatArea = new CN_CapPedidoVtaInst();
                PedidoVtaInst pedido = new PedidoVtaInst();
                double Total = 0;
                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;
                //pedido.Estatus = DdlEstatus.SelectedItem == null ? (string)null : DdlEstatus.SelectedItem.Value.ToString().Trim();

                pedido.Estatus = "P";
                pedido.Filtro_Vigencia = ddlVigencia.SelectedItem == null ? (string)null : (ddlVigencia.SelectedItem.Value.ToString().Trim() == "3" ? (string)null : ddlVigencia.SelectedItem.Value.ToString().Trim());
                pedido.Filtro_usuario = session.Propia ? session.Id_U.ToString().Trim() : "";
                pedido.filtro_pedido = txtnumeroPed.Value == "" ? (string)null : txtnumeroPed.Value;

                pedido.Filtro_Cte = ddlRazonSocialCliente.SelectedIndex == -1 ? (string)null : ddlRazonSocialCliente.SelectedItem.Value.ToString().Trim();
                if (pedido.Filtro_Cte == "0" || pedido.Filtro_Cte == "-1")
                {
                    pedido.Filtro_Cte = (string)null;
                }

                pedido.Filtro_Frecuencia = ddlfrecuencia.SelectedItem == null ? "0" : ddlfrecuencia.SelectedItem.Value.ToString().Trim();

                pedido.Id_U = session.Id_Rik == -1 ? (int?)null : session.Id_Rik;
                string modalidadVenta = null;


                pedido.Filtro_Anios = txtanio.Text == "" ? (string)null : txtanio.Text;
                pedido.Filtro_AnioFinal = txtanio.Text == "" ? (string)null : txtanio.Text;


                pedido.Filtro_tipoPed = ddlTipoPedido.Value == null ? (string)null : ddlTipoPedido.Value.ToString() == "Todos" ? (string)null : ddlTipoPedido.Value.ToString();

                string frecuencia = ddlfrecuencia.SelectedItem == null ? "0" : ddlfrecuencia.SelectedItem.Value.ToString().Trim();
                switch (frecuencia)
                {
                    //Todos
                    case "0":
                        if (txtsemanainicialnum.Value == "" || txtsemanafinalnum.Value == "")
                        {
                            pedido.Filtro_SemIni = "";
                            pedido.Filtro_SemFin = "";
                        }
                        else
                        {
                            if (Convert.ToInt32(txtsemanainicialnum.Value.ToString().Trim()) > Convert.ToInt32(txtsemanafinalnum.Value.ToString().Trim()))
                            {
                                mensaje("La semana inicial no puede ser mayor a la semana final");
                                return;
                            }
                            pedido.Filtro_SemIni = txtsemanainicialnum.Value.ToString().Trim();
                            pedido.Filtro_SemFin = txtsemanafinalnum.Value.ToString().Trim();
                        }

                        pedido.Filtro_mes = null;
                        pedido.Filtro_mesFinal = null;
                        pedido.Filtro_Sem = null;

                        break;

                    //Semanal
                    case "1":
                        if (textIdSemana.Value == "")
                        {
                            mensaje("Favor de capturar la fecha de la frecuencia");
                        }
                        pedido.Filtro_SemIni = "";
                        pedido.Filtro_SemFin = "";
                        pedido.Filtro_mes = null;
                        pedido.Filtro_mesFinal = pedido.Filtro_mes;
                        pedido.Filtro_Sem = double.Parse(textIdSemana.Value);
                        break;

                    //Mensual
                    case "2":
                        pedido.Filtro_SemIni = "";
                        pedido.Filtro_SemFin = "";
                        pedido.Filtro_mes = ddlMes.SelectedItem.Value.ToString().Trim();
                        pedido.Filtro_mesFinal = pedido.Filtro_mes;
                        pedido.Filtro_Sem = null;
                        break;

                    //Bimestral
                    case "3":
                        pedido.Filtro_SemIni = "";
                        pedido.Filtro_SemFin = "";
                        pedido.Filtro_mes = DdlMeses.SelectedItem.Value.ToString().Trim();
                        pedido.Filtro_mesFinal = (int.Parse(pedido.Filtro_mes) + 1).ToString().Trim();
                        pedido.Filtro_Sem = null;

                        if (int.Parse(pedido.Filtro_mesFinal) > 12)
                        {
                            pedido.Filtro_mesFinal = (int.Parse(pedido.Filtro_mesFinal) - 12).ToString().Trim();
                            pedido.Filtro_AnioFinal = (int.Parse(pedido.Filtro_AnioFinal) + 1).ToString().Trim();
                        }

                        obtenerMEs(int.Parse(pedido.Filtro_mesFinal), ref mesNombre);
                        txtmsFinal.Value = mesNombre + "/" + pedido.Filtro_AnioFinal;

                        break;

                    //Trimestral
                    case "4":
                        pedido.Filtro_SemIni = "";
                        pedido.Filtro_SemFin = "";
                        pedido.Filtro_mes = DdlMeses.SelectedItem.Value.ToString().Trim();
                        pedido.Filtro_mesFinal = (int.Parse(pedido.Filtro_mes) + 2).ToString().Trim();
                        pedido.Filtro_Sem = null;

                        if (int.Parse(pedido.Filtro_mesFinal) > 12)
                        {
                            pedido.Filtro_mesFinal = (int.Parse(pedido.Filtro_mesFinal) - 12).ToString().Trim();
                            pedido.Filtro_AnioFinal = (int.Parse(pedido.Filtro_AnioFinal) + 1).ToString().Trim();
                        }

                        obtenerMEs(int.Parse(pedido.Filtro_mesFinal), ref mesNombre);
                        txtmsFinal.Value = mesNombre + "/" + pedido.Filtro_AnioFinal;

                        break;

                    //Semestral
                    case "5":
                        pedido.Filtro_SemIni = "";
                        pedido.Filtro_SemFin = "";
                        pedido.Filtro_mes = DdlMeses.SelectedItem.Value.ToString().Trim();
                        pedido.Filtro_mesFinal = (int.Parse(pedido.Filtro_mes) + 5).ToString().Trim();
                        pedido.Filtro_Sem = null;

                        if (int.Parse(pedido.Filtro_mesFinal) > 12)
                        {
                            pedido.Filtro_mesFinal = (int.Parse(pedido.Filtro_mesFinal) - 12).ToString().Trim();
                            pedido.Filtro_AnioFinal = (int.Parse(pedido.Filtro_AnioFinal) + 1).ToString().Trim();
                        }

                        obtenerMEs(int.Parse(pedido.Filtro_mesFinal), ref mesNombre);
                        txtmsFinal.Value = mesNombre + "/" + pedido.Filtro_AnioFinal;
                        break;

                }

                clsCatArea.Lista2(pedido, session.Emp_Cnx, ref List, modalidadVenta);

                clsCatArea.ListaOrderCompra(pedido, session.Emp_Cnx, ref List2, modalidadVenta);


                CN_CatTipoVenta cnTv = new CN_CatTipoVenta(session);
                foreach (PedidoVtaInst ped in List)
                {
                    Total += ped.Acs_Cantidad;

                    if (ped.Id_TG == 0)
                    {
                        ped.Id_TG = 0;
                        ped.ModalidadGarantia = "Regular"; //cnTv.Tradicional.TV_Nombre;
                    }
                }

                foreach (PedidoVtaInst ped in List2)
                {
                    Total += ped.Acs_Cantidad;

                    if (ped.Id_TG == 0)
                    {
                        ped.Id_TG = 0;
                        ped.ModalidadGarantia = "Regular"; //cnTv.Tradicional.TV_Nombre;
                    }
                }
                Session["gridpedidoVI"] = List;
                Session["gridpedidoOrderCompra"] = List2;
                //txtTotal.Text = Total.ToString("C2"); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Imprimir(int Id_Ped)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ArrayList ALValorParametrosInternos = new ArrayList();

                //Consulta centro de distribución
                CentroDistribucion Cd = new CentroDistribucion();
                new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref Cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);

                ALValorParametrosInternos.Add(sesion.Emp_Cnx);
                ALValorParametrosInternos.Add(sesion.Id_Emp);
                ALValorParametrosInternos.Add(sesion.Id_Cd_Ver);
                ALValorParametrosInternos.Add(Id_Ped);

                Type instance = null;
                instance = typeof(LibreriaReportes.PedidoImpresion);
                Session["InternParameter_Values" + Session.SessionID] = ALValorParametrosInternos;
                Session["assembly" + Session.SessionID] = instance.AssemblyQualifiedName;

                //NOTA: El estatus de impresión, lo pone cuando el reporte se carga 
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirReportePadre", "AbrirReportePadre()", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void Descargae(int Id_Ped)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CapPedidoVtaInst clsCapPedido = new CN_CapPedidoVtaInst();


                PedidoVtaInst pedido = new PedidoVtaInst();
                List<PedidoVtaInst> pedidoDescarga = new List<PedidoVtaInst>();
                pedido.Id_Ped = Id_Ped;
                pedido.Id_Cd = sesion.Id_Cd_Ver;
                pedido.Id_Emp = sesion.Id_Emp;
                clsCapPedido.CoonsultarOrdenCompra(pedido, ref pedidoDescarga, sesion.Emp_Cnx);

                if (pedidoDescarga.Count > 0)
                {
                    if (pedidoDescarga.First().archivo.Length > 0)
                    {
                        byte[] bytes = Convert.FromBase64String(pedidoDescarga.First().archivo);
                        string strDocName = pedidoDescarga.First().nombreDocumento + "." + pedidoDescarga.First().extension.Replace(".", "");
                        string filename = Server.MapPath("Reportes") + "\\" + pedidoDescarga.First().nombreDocumento + "." + pedidoDescarga.First().extension.Replace(".", "");

                        if (System.IO.File.Exists(filename))
                        {
                            System.IO.File.Delete(filename);
                        }

                        System.IO.File.WriteAllBytes(filename, bytes);

                        System.IO.FileInfo file = new System.IO.FileInfo(filename);

                        System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;

                        try
                        {
                            response.Clear();
                            response.ContentType = "application/pdf";
                            response.AppendHeader("Content-Disposition", "attachment; filename=" + strDocName + ";");
                            response.TransmitFile(filename);
                            response.Flush();
                        }
                        catch (Exception exFile)
                        {
                            // any error handling mechanism
                            throw new Exception("Error al trasmitir el archivo: " + exFile.Message);
                        }
                        finally
                        {
                            if (System.IO.File.Exists(filename))
                            {
                                System.IO.File.Delete(filename);
                            }
                            HttpContext.Current.ApplicationInstance.CompleteRequest();
                        }
                    }
                }
                else
                {
                    mensaje("El pedido no cuenta con archivo registrado");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void GridViewCustomSummary_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {


            if (e.IsTotalSummary)
            {
                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                {

                }
                else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                {
                    string currency = e.GetValue("Prd_Precio").ToString().Trim();

                }
                else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                {

                    e.TotalValue = "Total:";

                }
            }
        }

        protected void GridViewCustomSummary_CustomUnboundColumnData(object sender, DevExpress.Web.Bootstrap.BootstrapGridViewColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Prd_Precio")
                e.Value = "total:";
        }


        private void GetListPendientes(ref List<PedidoVtaInst> List)
        {
            try
            {

                Funciones Funcion = new Funciones();

                CN_CapPedidoVtaInst clsCatArea = new CN_CapPedidoVtaInst();
                PedidoVtaInst pedido = new PedidoVtaInst();
                double Total = 0;
                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;
                pedido.Estatus = "p";
                pedido.Filtro_Vigencia = "0";

                pedido.Filtro_Anios = txtanio.Text == "" ? (string)null : txtanio.Text;
                pedido.Filtro_AnioFinal = txtanio.Text == "" ? (string)null : txtanio.Text;
                string modalidadVenta = "";

                clsCatArea.Lista(pedido, session.Emp_Cnx, ref List, modalidadVenta);
                CN_CatTipoVenta cnTv = new CN_CatTipoVenta(session);
                foreach (PedidoVtaInst ped in List)
                {
                    Total += ped.Acs_Cantidad;

                    if (ped.Id_TG == 0)
                    {
                        ped.Id_TG = 0;
                        ped.ModalidadGarantia = "Regular"; //cnTv.Tradicional.TV_Nombre;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private IList<Pedido_Internet> GetListInternet()
        {
            try
            {
                Funciones Funcion = new Funciones();
                IList<Pedido_Internet> List = new List<Pedido_Internet>();
                CN_CapPedido_Internet clsCatPedidosInternet = new CN_CapPedido_Internet();
                Pedido_Internet pedido = new Pedido_Internet();


                pedido.P_Cliente = ddlRazonSocialCliente.SelectedIndex == -1 ? 0 : Convert.ToInt32(ddlRazonSocialCliente.Value.ToString().Trim());


                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;


                pedido.P_Anio_Inicio = txtanio.Text == "" ? 0 : Convert.ToInt32(txtanio.Text);
                pedido.P_Anio_Final = txtanio.Text == "" ? 0 : Convert.ToInt32(txtanio.Text);
                //pedido.P_Estatus = DdlEstatus.Value == null ? "--Todos--" :  DdlEstatus.Value.ToString().Trim();
                pedido.P_Estatus = "P";
                pedido.P_Nom_Cliente = ddlRazonSocialCliente.Value == null ? "" : ddlRazonSocialCliente.Value.ToString().Trim() != "--Todos--" ? ddlRazonSocialCliente.Text.ToString().Trim() : "";



                clsCatPedidosInternet.ConsultarPedidos(ref List, session.Emp_Cnx, pedido);
                Session["gridpedidoInternet"] = List;
                //txtTotal.Text = Total.ToString("C2");
                return List;
            }
            catch (Exception ex)
            {
                mensaje(ex.Message);
                throw ex;
            }
        }

        protected void CargarDatosOC()
        {

            List<catcnac_PedidosOc> lista = new List<catcnac_PedidosOc>();
            int cteIni = 0, cteFin = 0, terIni = 0, terFin = 0, anioIni = 0, anioFin = 0;
            string Nombre_Cliente = "";

            cteIni = ddlRazonSocialCliente.SelectedIndex == -1 ? 0 : Convert.ToInt32(ddlRazonSocialCliente.Value.ToString().Trim());
            cteFin = ddlRazonSocialCliente.SelectedIndex == -1 ? 0 : Convert.ToInt32(ddlRazonSocialCliente.Value.ToString().Trim());
            anioIni = txtanio.Text == "" ? 0 : Convert.ToInt32(txtanio.Text);
            anioFin = txtanio.Text == "" ? 0 : Convert.ToInt32(txtanio.Text);

            CN_CatCNac_OrdenCompra cn_oc = new CN_CatCNac_OrdenCompra();
            //cn_oc.ConsultarPedidosOC(cteIni, cteFin, terIni, terFin, anioIni, anioFin, Nombre_Cliente, this.DdlEstatus.Text.ToString().Trim(), ref lista, session.Emp_Cnx);
            cn_oc.ConsultarPedidosOC(cteIni, cteFin, terIni, terFin, anioIni, anioFin, Nombre_Cliente, "P", ref lista, session.Emp_Cnx);
            Session["gridpedidoOC"] = lista;
            lblcPrdOC.Text = lista.Count().ToString();
            rgDatosOC.DataSource = lista;
            rgDatosOC.DataBind();
        }

        private void RechazarPedidoVI(int Id_Acs, int Acs_Semana, int Acs_Anio, string numPedido)
        {
            try
            {
                if (Convert.ToInt32(numPedido) != 0)
                {

                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    Pedido pedido = new Pedido();
                    PedidoVtaInst pedidovtainst = new PedidoVtaInst();

                    pedido.Id_Emp = sesion.Id_Emp;
                    pedido.Id_Cd = sesion.Id_Cd_Ver;
                    pedido.Id_Ped = Convert.ToInt32(numPedido);

                    pedidovtainst.Id_Emp = sesion.Id_Emp;
                    pedidovtainst.Id_Cd = sesion.Id_Cd_Ver;
                    pedidovtainst.Id_Ped = Convert.ToInt32(numPedido);
                    pedidovtainst.Ped_Tipo = 2;


                    CN_CapPedido cn_capPedido = new CN_CapPedido();
                    if (!cn_capPedido.ConsultaPedidoFacturacion(ref pedido, sesion.Emp_Cnx))//pedido no existe
                    {

                        mensaje("El pedido no existe");
                    }
                    else
                    {
                        List<string> statusPosibles = new List<string>() { "R", "O", "A", "U", "I", "C" };
                        if (!statusPosibles.Contains(pedido.Estatus))
                        {
                            mensaje("El pedido se encuentra en estatus no válido para realizar la baja");
                            return;
                        }

                        CN_CapPedidoVtaInst CN = new CN_CapPedidoVtaInst();
                        CN_Eccommerce eco = new CN_Eccommerce();
                        List<PedidoVtaInst> List = new List<PedidoVtaInst>();
                        PedidoVtaInst datos = new PedidoVtaInst();
                        datos.Id_Emp = sesion.Id_Emp;
                        datos.Id_Cd = sesion.Id_Cd_Ver;
                        datos.Id_Ped = Convert.ToInt32(numPedido);
                        string modalidadVenta = "";
                        CN.ConsultarPedido(datos, sesion.Emp_Cnx, ref List, modalidadVenta);

                        if (List.Count > 0)
                        {
                            if (List.First().str_Tipo_pedido == "Pedidos Portal de Cliente")
                            {
                                int verificador2 = 0;
                                string estatus = "";
                                string message = "";

                                int verificador = 0;

                                APISKEY api = new APISKEY();

                                Remision Remisiones = new Remision();
                                Remisiones.Id_Ped = Convert.ToInt32(numPedido);
                                Remisiones.Id_Emp = session.Id_Emp;
                                Remisiones.Id_Cd = session.Id_Cd_Ver;

                                CN_Eccommerce CNProFactura_Embarque = new CN_Eccommerce();
                                CNProFactura_Embarque.ValidarPedidoEcommerce(Remisiones, session.Emp_Cnx, ref verificador2);
                                if (verificador2 != 0)
                                {
                                    pedidovtainst.PedExterno = verificador2;
                                    api.CancelarPedido(pedidovtainst, 2, session.Emp_Cnx, ref estatus, ref message);
                                }


                                if (estatus == "2" || estatus == "0")
                                {
                                    mensaje("Ocurrió un problema al Cancelar el Pedido. " + message);
                                    return;
                                }
                                else
                                {
                                    CN_CapPedidoVtaInst clsPedidovi = new CN_CapPedidoVtaInst();
                                    PedidoVtaInst pedidovta = new PedidoVtaInst();
                                    verificador = -1;
                                    pedidovta.Id_Emp = session.Id_Emp;
                                    pedidovta.Id_Cd = session.Id_Cd_Ver;
                                    pedidovta.Id_Acs = Convert.ToInt32(Id_Acs);
                                    pedidovta.Acs_Anio = Acs_Anio;
                                    pedidovta.Acs_Semana = Acs_Semana;
                                    pedidovta.Pedido_del = numPedido;
                                    clsPedidovi.Cancelar2(pedidovta, session.Emp_Cnx, ref verificador);
                                    if (verificador == 1)
                                    {
                                        eco.BajaPedido_Portal(pedidovtainst, ref verificador, session.Emp_Cnx);
                                        Baja("B", Convert.ToInt32(numPedido));
                                        //EnviarCorreo(Convert.ToInt32(rgRemisiones.Items[item]["Num_Cliente"].Text), Convert.ToInt32(lista.First().id_pedMag.ToString()), session.Emp_Cnx);

                                    }
                                }
                            }
                            else
                            {
                                CN_CapPedidoVtaInst clsPedidovi = new CN_CapPedidoVtaInst();
                                PedidoVtaInst pedidovta = new PedidoVtaInst();
                                int verificador = -1;
                                pedidovta.Id_Emp = session.Id_Emp;
                                pedidovta.Id_Cd = session.Id_Cd_Ver;
                                pedidovta.Id_Acs = Convert.ToInt32(Id_Acs);
                                pedidovta.Acs_Anio = Acs_Anio;
                                pedidovta.Acs_Semana = Acs_Semana;
                                pedidovta.Pedido_del = numPedido;
                                clsPedidovi.Cancelar2(pedidovta, session.Emp_Cnx, ref verificador);
                                if (verificador == 1)
                                {
                                    Baja("B", Convert.ToInt32(numPedido));
                                }

                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CargarGrid();
            }
        }

        private void Baja(string estatus, int id_ped)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CapPedido cn_cappedido = new CN_CapPedido();
                Pedido ped = new Pedido();
                ped.Id_Emp = sesion.Id_Emp;
                ped.Id_Cd = sesion.Id_Cd_Ver;
                ped.Id_Ped = Convert.ToInt32(id_ped);
                ped.Estatus = estatus;
                int verificador = 0;
                cn_cappedido.Baja(ped, sesion.Emp_Cnx, ref verificador);
                if (verificador == 1)
                {
                    CN_CapPedido cn_cappedidovtainst = new CN_CapPedido();
                    ped.Id_Emp = session.Id_Emp;
                    ped.Id_Cd = session.Id_Cd_Ver;
                    ped.Id_Ped = Convert.ToInt32(id_ped);
                    ped.Id_U = session.Id_U;
                    ped.Ped_Fecha = DateTime.Now;
                    ped.Modulo = "Captación de Pedido";
                    int verificador3 = 0;
                    cn_cappedidovtainst.logCancelarPedido(ped, session.Emp_Cnx, ref verificador3);


                    List<PedidoVtaInst> List = new List<PedidoVtaInst>();
                    List<PedidoVtaInst> List2 = new List<PedidoVtaInst>();
                    GetList(ref List, ref List2);
                    mensaje("Se dio de baja el pedido #" + ped.Id_Ped.ToString().Trim());
                }
                else if (verificador == -2)
                {
                    mensaje("El pedido se encuentra Facturado/Remisionado, no es posible darlo de baja");
                }
                else if (verificador == -3)
                {
                    mensaje("El pedido se encuentra Facturado, no es posible darlo de baja");
                }
                else if (verificador == -4)
                {
                    mensaje("El pedido se encuentra Remisionado, no es posible darlo de baja");
                }
                else
                {
                    mensaje("Ocurrió un error al intentar dar de baja");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BajaLista(string estatus, int id_ped)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CapPedido cn_cappedido = new CN_CapPedido();
                Pedido ped = new Pedido();
                ped.Id_Emp = sesion.Id_Emp;
                ped.Id_Cd = sesion.Id_Cd_Ver;
                ped.Id_Ped = Convert.ToInt32(id_ped);
                ped.Estatus = estatus;
                int verificador = 0;
                cn_cappedido.Baja(ped, sesion.Emp_Cnx, ref verificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void VentaNueva()
        {
            try
            {

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                VentaNueva();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            TabName.Value = Request.Form[TabName.UniqueID];
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "mantaintabs('" + TabName.Value + "')", true);
        }

        protected void btnLoadEditar_Click(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer c = ((System.Web.UI.HtmlControls.HtmlButton)sender).NamingContainer as GridViewDataItemTemplateContainer;
            string value = c.Grid.GetRowValues(c.VisibleIndex, "Id_Acs").ToString().Trim();
            string Estatus = c.Grid.GetRowValues(c.VisibleIndex, "Estatus").ToString().Trim();
            string Pedido = c.Grid.GetRowValues(c.VisibleIndex, "pedido").ToString().Trim();
            string semana = c.Grid.GetRowValues(c.VisibleIndex, "Acs_Semana").ToString().Trim();
            string anio = c.Grid.GetRowValues(c.VisibleIndex, "Acs_Anio").ToString().Trim();
            string id_cteDirEntrega = c.Grid.GetRowValues(c.VisibleIndex, "id_cteDirEntrega").ToString().Trim();
            string EstatusSOl = c.Grid.GetRowValues(c.VisibleIndex, "EstatusSOl").ToString().Trim();
            string id_sol = c.Grid.GetRowValues(c.VisibleIndex, "ID_sol").ToString().Trim();
            int id = int.Parse(value);

            Captar(id, Estatus, Pedido, semana, anio, id_cteDirEntrega, EstatusSOl, id_sol);

            TabName.Value = Request.Form[TabName.UniqueID];
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "mantaintabs('" + TabName.Value + "')", true);
        }

        protected void btnRastreo_Click(object sender, EventArgs e)
        {

            GridViewDataItemTemplateContainer c = ((System.Web.UI.HtmlControls.HtmlButton)sender).NamingContainer as GridViewDataItemTemplateContainer;
            string value = c.Grid.GetRowValues(c.VisibleIndex, "Id_Acs").ToString().Trim();
            string Estatus = c.Grid.GetRowValues(c.VisibleIndex, "Estatus").ToString().Trim();
            string Pedido = c.Grid.GetRowValues(c.VisibleIndex, "pedido").ToString().Trim();
 
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirRastreo", "modalRastreo('" + value + "',  '" + Pedido + "')", true);
           

            TabName.Value = Request.Form[TabName.UniqueID];
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "mantaintabs('" + TabName.Value + "')", true);
        }




        protected void btnRechazar_Click(object sender, EventArgs e)
        {

            GridViewDataItemTemplateContainer c = ((System.Web.UI.HtmlControls.HtmlButton)sender).NamingContainer as GridViewDataItemTemplateContainer;
            string ID_Acs = c.Grid.GetRowValues(c.VisibleIndex, "Id_Acs").ToString().Trim();
            string Num_pedido = c.Grid.GetRowValues(c.VisibleIndex, "pedido").ToString().Trim();
            string Semana = c.Grid.GetRowValues(c.VisibleIndex, "Acs_Semana").ToString().Trim();
            string Anio = c.Grid.GetRowValues(c.VisibleIndex, "Acs_Anio").ToString().Trim();
            if (_PermisoEliminar)
            {
                RechazarPedidoVI(int.Parse(ID_Acs), int.Parse(Semana), int.Parse(Anio), Num_pedido);
            }
            else
            {
                mensaje("No tiene permisos para eliminar el pedido.");
            }
            TabName.Value = Request.Form[TabName.UniqueID];
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "mantaintabs('" + TabName.Value + "')", true);
        }

        private void obtenerMEs(int idMes, ref string mes)
        {
            string mesactual = idMes < 10 ? ("0" + idMes.ToString().Trim()) : idMes.ToString().Trim();

            string fecha = "01-" + mesactual + "-2000";

            DateTime fechaMEs = Convert.ToDateTime(fecha);
            mes = MonthName(fechaMEs.Month);
        }

        public string MonthName(int month)
        {
            DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;
            return dtinfo.GetMonthName(month);
        }

        private void Captar(int idcaptar, string estatus, string pedido, string semana, string anio, string IDDireccionEntrega, string EstatusSOl, string id_sol)
        {
            try
            {
                string permiso = string.Concat(ConfigurationManager.AppSettings["RequiereAutorizacionPedido"].ToString());


                int verificador = 0;
                if (estatus != "P")
                {
                    mensaje("Solo se pueden captar pedidos con estatus pendiente");
                    return;
                }


                List<PedidoVtaInst> List = new List<PedidoVtaInst>();
                List<PedidoVtaInst> List2 = new List<PedidoVtaInst>();
                GetList(ref List, ref List2);

                PedidoVtaInst query = (from tlist in List
                                       where tlist.Id_Acs == idcaptar
                                       select tlist).FirstOrDefault();

                if (query != null)
                {
                    bool CreditoSusp = Convert.ToBoolean(query.Cte_Credito);

                    if (CreditoSusp && permiso.ToUpper() == "N")
                    {

                        mensaje("Este cliente tiene el crédito suspendido");
                        return;
                    }

                    CN_CapAcys cn_capacys = new CN_CapAcys();
                    Acys acys = new Acys();
                    acys.Id_Emp = session.Id_Emp;
                    acys.Id_Cd = session.Id_Cd_Ver;
                    acys.Id_Acs = Convert.ToInt32(query.Id_Acs);
                    acys.Id_AcsVersion = 1;


                    cn_capacys.ConsultarvalidaracysFrecuencia(acys, ref verificador, session.Emp_Cnx);
                    if (verificador > 0)
                    {
                        mensaje("No se puede captar el pedido, la frecuencia de uno o varios productos no fue establecida correctamente, favor de validar correctamente. # de acuerdo: " + acys.Id_Acs + " ");
                        return;
                    }

                    cn_capacys.ConsultarEstatus(ref acys, session.Emp_Cnx);

                    int? idTG = (int?)(query.Id_TG);
                    string idTGComponent = "";
                    if (idTG.HasValue)
                    {
                        if (idTG.Value != 0)
                        {
                            idTGComponent = "', '" + idTG.Value;
                        }
                    }

                    if (EstatusSOl == "P" && Convert.ToInt32(id_sol) != 0)
                    {
                        mensaje("La Solicitud del Pedido esta Pendiente por Autorizar ");
                        return;
                    }

                    if (EstatusSOl == "A")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirDetalles", "modalDetalleAuto('" + id_sol + "', '" + idcaptar + "', '" + _PermisoGuardar + "', '" + _PermisoModificar + "', '" + _PermisoEliminar + "', '" + _PermisoImprimir + "'," + query.Acs_Anio + ", " + pedido + ", '" + query.Acs_Semana + "', '" + idTGComponent + " ', '" + IDDireccionEntrega + "')", true);
                    }
                    else
                    {


                        if (acys.Acs_Estatus == "B")
                        {
                            mensaje("No se puede captar el pedido, el Acuerdo esta dado de baja");

                        }
                        else
                        {

                            if (pedido == "0")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirDetalles", "modalDetalleSinPedido('" + idcaptar + "', '" + _PermisoGuardar + "', '" + _PermisoModificar + "', '" + _PermisoEliminar + "', '" + _PermisoImprimir + "'," + anio + ", '" + semana + "', '" + idTGComponent + "', '" + IDDireccionEntrega + "')", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirDetalles", "modalDetalle('" + idcaptar + "', '" + _PermisoGuardar + "', '" + _PermisoModificar + "', '" + _PermisoEliminar + "', '" + _PermisoImprimir + "'," + query.Acs_Anio + ", " + pedido + ", '" + query.Acs_Semana + "', '" + idTGComponent + " ', '" + IDDireccionEntrega + "')", true);
                            }
                        }
                    }
                }

                gridpedidoVI.DataSource = List.ToList();
                gridpedidoVI.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void CaptarOrderCompra(int idcaptar, string estatus, string pedido, string semana, string anio, string IDDireccion)
        {
            try
            {
                if (idcaptar == 0)
                {

                    mensaje("No se puede captar el pedido de orden de compra, unicamente Pedidos de venta instalada");
                    return;
                }

                List<PedidoVtaInst> List = new List<PedidoVtaInst>();
                List<PedidoVtaInst> List2 = new List<PedidoVtaInst>();
                GetList(ref List, ref List2);

                PedidoVtaInst query = (from tlist in List
                                       where tlist.Id_Acs == idcaptar
                                       select tlist).FirstOrDefault();

                if (query != null)
                {
                    bool CreditoSusp = Convert.ToBoolean(query.Cte_Credito);

                    if (CreditoSusp)
                    {

                        mensaje("Este cliente tiene el crédito suspendido");
                        return;
                    }

                    CN_CapAcys cn_capacys = new CN_CapAcys();
                    Acys acys = new Acys();
                    acys.Id_Emp = session.Id_Emp;
                    acys.Id_Cd = session.Id_Cd_Ver;
                    acys.Id_Acs = Convert.ToInt32(query.Id_Acs);
                    acys.Id_AcsVersion = 1;



                    cn_capacys.ConsultarEstatus(ref acys, session.Emp_Cnx);


                    if (acys.Acs_Estatus == "B")
                    {
                        mensaje("No se puede captar el pedido, el Acuerdo esta dado de baja");

                    }
                    else
                    {
                        int? idTG = (int?)(query.Id_TG);
                        string idTGComponent = "";
                        if (idTG.HasValue)
                        {
                            if (idTG.Value != 0)
                            {
                                idTGComponent = "', '" + idTG.Value;
                            }
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirDetalles", "modalDetalleOrdenCompra('" + idcaptar + "', '" + _PermisoGuardar + "', '" + _PermisoModificar + "', '" + _PermisoEliminar + "', '" + _PermisoImprimir + "','" + anio + "', '" + semana + "', '" + idTGComponent + "', '" + IDDireccion + "')", true);



                    }
                }

                gridpedidoVI.DataSource = List.ToList();
                gridpedidoVI.DataBind();

                gridviewOrderCompra.DataSource = List2.ToList();
                gridviewOrderCompra.DataBind();
                lblCPEd.Text = List.Count().ToString();
                lblCPEdInt.Text = ListPedidoInternet.Count().ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void CaptarOrderCompraPedidoCaptados(int idcaptar, string estatus, string pedido, string semana, string anio, string IDDireccion)
        {
            try
            {
                if (idcaptar == 0)
                {

                    mensaje("No se puede captar el pedido de orden de compra, unicamente Pedidos de venta instalada");
                    return;
                }

                List<PedidoVtaInst> List = new List<PedidoVtaInst>();
                List<PedidoVtaInst> List2 = new List<PedidoVtaInst>();
                GetList(ref List, ref List2);

                PedidoVtaInst query = (from tlist in List2
                                       where tlist.Id_Acs == idcaptar
                                       select tlist).FirstOrDefault();

                if (query != null)
                {
                    bool CreditoSusp = Convert.ToBoolean(query.Cte_Credito);

                    if (CreditoSusp)
                    {

                        mensaje("Este cliente tiene el crédito suspendido");
                        return;
                    }

                    CN_CapAcys cn_capacys = new CN_CapAcys();
                    Acys acys = new Acys();
                    acys.Id_Emp = session.Id_Emp;
                    acys.Id_Cd = session.Id_Cd_Ver;
                    acys.Id_Acs = Convert.ToInt32(query.Id_Acs);
                    acys.Id_AcsVersion = 1;



                    cn_capacys.ConsultarEstatus(ref acys, session.Emp_Cnx);


                    if (acys.Acs_Estatus == "B")
                    {
                        mensaje("No se puede captar el pedido, el Acuerdo esta dado de baja");

                    }
                    else
                    {
                        int? idTG = (int?)(query.Id_TG);
                        string idTGComponent = "";
                        if (idTG.HasValue)
                        {
                            if (idTG.Value != 0)
                            {
                                idTGComponent = "', '" + idTG.Value;
                            }
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirDetalles", "modalDetalleOrdenCompra('" + idcaptar + "', '" + _PermisoGuardar + "', '" + _PermisoModificar + "', '" + _PermisoEliminar + "', '" + _PermisoImprimir + "','" + anio + "', '" + semana + "', '" + idTGComponent + "', '" + IDDireccion + "')", true);



                    }
                }

                gridpedidoVI.DataSource = List.ToList();
                gridpedidoVI.DataBind();

                gridviewOrderCompra.DataSource = List2.ToList();
                gridviewOrderCompra.DataBind();
                lblCPEd.Text = List.Count().ToString();
                lblCPEdInt.Text = ListPedidoInternet.Count().ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void RechazarLista()
        {
            try
            {
                var products = gridpedidoVI.GetSelectedFieldValues("Id_Ped", "Acs_Semana");
                PedidoVtaInst ped;
                for (var i = 0; i < gridpedidoVI.VisibleRowCount; i++)
                {
                    if (gridpedidoVI.GetRowValues(i) != null)
                    {
                        if (gridpedidoVI.Selection.IsRowSelected(i))
                        {
                            ped = new PedidoVtaInst();
                            ped = ListPedidoVtaInst.Where(x => x.pedido == gridpedidoVI.GetRowValues(i, "pedido").ToString()).FirstOrDefault();

                            if (ped != null)
                            {
                                ped.Seleccionado = true;
                            }
                        }
                    }
                }

                int Verificador = 0;
                CN_CapPedidoVtaInst cn_clsPedidovi;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (ListPedidoVtaInst.Where(i => i.Seleccionado == true).Count() == 0)
                {
                    mensaje("Debe seleccionar al menos un elemento");
                    return;
                }

                foreach (PedidoVtaInst ped2 in ListPedidoVtaInst)
                {
                    if (ped2.Seleccionado == true)
                    {
                        if (Convert.ToInt32(ped2.pedido) != 0)
                        {

                            Pedido pedido = new Pedido();

                            pedido.Id_Emp = sesion.Id_Emp;
                            pedido.Id_Cd = sesion.Id_Cd_Ver;
                            pedido.Id_Ped = Convert.ToInt32(ped2.pedido);

                            CN_CapPedido cn_capPedido = new CN_CapPedido();
                            if (!cn_capPedido.ConsultaPedidoFacturacion(ref pedido, sesion.Emp_Cnx))//pedido no existe
                            {

                                mensaje("El pedido no existe");
                            }
                            else
                            {
                                List<string> statusPosibles = new List<string>() { "R", "O", "A", "U", "I", "C" };
                                if (!statusPosibles.Contains(pedido.Estatus))
                                {
                                    mensaje("El pedido se encuentra en estatus no válido para realizar la baja");
                                    return;
                                }

                                cn_clsPedidovi = new CN_CapPedidoVtaInst();
                                ped2.Id_Emp = sesion.Id_Emp;
                                ped2.Id_Cd = sesion.Id_Cd_Ver;
                                cn_clsPedidovi.RechazarPedidoVI2(ped2, sesion.Emp_Cnx, ref Verificador);

                                BajaLista("D", Convert.ToInt32(ped2.pedido));
                            }
                        }
                    }
                }

                List<PedidoVtaInst> List = new List<PedidoVtaInst>();
                List<PedidoVtaInst> List2 = new List<PedidoVtaInst>();
                GetList(ref List, ref List2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetWeekNumber(DateTime dtPassed)
        {
            try
            {
                CultureInfo ciCurr = CultureInfo.CurrentCulture;
                int weekNum = ciCurr.Calendar.GetWeekOfYear(dtPassed, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                return weekNum.ToString().Trim();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        private void CargarPedidoInternet(int num_pedido, int tipoPedido, ref DataTable dtTemp)
        {


            CN_CapPedido_Internet cn_capPedidoInternet = new CN_CapPedido_Internet();
            cn_capPedidoInternet.ConsultaPedido_DetalleAdmin(session.Emp_Cnx, num_pedido, tipoPedido, ref dtTemp);
            dt = dtTemp;
            Session["dtPedidoVI"] = dt;

        }




        private void CargarPedido(int idPedido, ref DataTable dtTemp, ref DataTable dtRestos)
        {
            try
            {

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (session == null)
                {

                    string[] pag = Page.Request.Url.ToString().Trim().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);

                }

                Pedido pedido = new Pedido();
                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;
                pedido.Id_Ped = idPedido;
                pedido.Ped_Tipo = 3;
                pedido.Ped_Captacion = true;

                CN_CapPedido cn_capPedido = new CN_CapPedido();
                cn_capPedido.ConsultaPedido2(ref pedido, session.Emp_Cnx);



                cn_capPedido.ConsultaCaptacionPedidoDetadmin(pedido, ref dtTemp, session.Emp_Cnx);

                dt = dtTemp;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GetListDet2()
        {
            try
            {
                dt = new DataTable();
                DataColumn dc = new DataColumn();
                dt.Columns.Add("Acs_FechaF", System.Type.GetType("System.DateTime"));
                dt.Columns.Add("Id_Prd", System.Type.GetType("System.Int64"));
                dt.Columns.Add("Prd_Descripcion", System.Type.GetType("System.String"));
                dt.Columns.Add("Prd_Presentacion", System.Type.GetType("System.String"));
                dt.Columns.Add("Prd_Unidad", System.Type.GetType("System.String"));

                dt.Columns.Add("Prd_Cantidad", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Prd_Precio", System.Type.GetType("System.Double"));
                dt.Columns.Add("Prd_Importe", System.Type.GetType("System.Double"));
                dt_Resto = dt.Clone();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void CargarProductoAcys(int IdAcs, int Semana, int anio, ref DataTable dtTemp)
        {
            try
            {
                CN_CapPedidoVtaInst cn_capPedidoVI = new CN_CapPedidoVtaInst();
                PedidoVtaInst pedido = new PedidoVtaInst();
                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;
                pedido.Id_Acs = IdAcs;
                pedido.Acs_Anio = anio;
                pedido.Acs_Semana = Semana;
                dt = null;



                List<PedidoVtaInst> List = new List<PedidoVtaInst>();
                string idTGStr = "";
                int? idTGNullable = 0;
                int idTG = 0;
                if (idTGStr != null)
                {
                    if (int.TryParse(idTGStr, out idTG))
                    {
                        idTGNullable = idTG;
                    }
                }
                cn_capPedidoVI.ConsultarDetadmin(pedido, ref List, ref dtTemp, session.Emp_Cnx, idTGNullable);

                DataTable dtTemp_Resto = this.dt_Resto;
                List<PedidoVtaInst> List2 = new List<PedidoVtaInst>();
                cn_capPedidoVI.ConsultarDet_Restoadmin(pedido, ref List2, ref dtTemp_Resto, session.Emp_Cnx);

                dtTemp.Merge(dtTemp_Resto);



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void gridviewOrderCompra_CustomButtonInitialize(object sender, BootstrapGridViewCustomButtonEventArgs e)
        {
            if (e.VisibleIndex == -1) return;
            string strTieneOC = gridviewOrderCompra.GetRowValues(e.VisibleIndex, "IsTieneOC").ToString();
            if (e.ButtonID == "btnDownload" && strTieneOC == "0")
                e.Visible = DefaultBoolean.False;
        }
        //


        protected void gridviewOrderCompra_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            try
            {
                if (e.ButtonID == "btnDownload")
                {
                    string Num_pedido = gridviewOrderCompra.GetRowValues(e.VisibleIndex, "pedido").ToString().Trim();
                    TabName.Value = "tabPedidoOrdenCompra";
                    Descargae(Convert.ToInt32(Num_pedido));


                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "mantaintabs('" + TabName.Value + "')", true);

                }
            }
            catch (Exception ex)
            {
                mensaje("Error al realizar la consulta, " + ex.Message);
            }


        }

        protected void rgInternet_CustomButtonInitialize(object sender, BootstrapGridViewCustomButtonEventArgs e)
        {
            if (e.VisibleIndex == -1) return;
            string strTieneOC = rgInternet.GetRowValues(e.VisibleIndex, "Url").ToString();
            // strTieneOC valor por defauld N/A, Contiene la url del archivo
            if (e.ButtonID == "btnInternetOC" && strTieneOC.Trim().Length < 5)
                e.Visible = DefaultBoolean.False;
        }

        protected void rgInternet_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            try
            {
                if (e.ButtonID == "btnInternetOC")
                {
                    string strUrlOC = rgInternet.GetRowValues(e.VisibleIndex, "Url").ToString().Trim();

                    TabName.Value = "tabInternet";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "mantaintabsAndOpenLink('" + strUrlOC + "');", true);

                }
            }
            catch (Exception ex)
            {
                mensaje("Error al realizar la consulta, " + ex.Message);
            }


        }

        #endregion
        #region Correo



        #endregion
        #region mensajes

        private void mensaje(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "modalMensaje('" + mensaje + "')", true);
        }

        #endregion

        protected void btnDescargarOC_Click(object sender, EventArgs e)
        {
            try
            {
                int Id_Ped = int.Parse(hdnDescargaOC.Value);
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CapPedidoVtaInst clsCapPedido = new CN_CapPedidoVtaInst();

                PedidoVtaInst pedido = new PedidoVtaInst();
                List<PedidoVtaInst> pedidoDescarga = new List<PedidoVtaInst>();
                pedido.Id_Ped = Id_Ped;
                pedido.Id_Cd = sesion.Id_Cd_Ver;
                pedido.Id_Emp = sesion.Id_Emp;
                clsCapPedido.CoonsultarOrdenCompra(pedido, ref pedidoDescarga, sesion.Emp_Cnx);

                if (pedidoDescarga.Count > 0)
                {
                    if (pedidoDescarga.First().archivo.Length > 0)
                    {
                        byte[] bytes = Convert.FromBase64String(pedidoDescarga.First().archivo);
                        string strDocName = pedidoDescarga.First().nombreDocumento + "." + pedidoDescarga.First().extension.Replace(".", "");
                        string filename = Server.MapPath("Reportes") + "\\" + pedidoDescarga.First().nombreDocumento + "." + pedidoDescarga.First().extension.Replace(".", "");

                        if (System.IO.File.Exists(filename))
                        {
                            System.IO.File.Delete(filename);
                        }

                        System.IO.File.WriteAllBytes(filename, bytes);

                        System.IO.FileInfo file = new System.IO.FileInfo(filename);

                        System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;

                        try
                        {
                            response.Clear();
                            response.ContentType = "application/pdf";
                            response.AppendHeader("Content-Disposition", "attachment; filename=" + strDocName + ";");
                            response.TransmitFile(filename);
                            response.Flush();
                        }
                        catch (Exception exFile)
                        {
                            // any error handling mechanism
                            throw new Exception("Error al trasmitir el archivo: " + exFile.Message);
                        }
                        finally
                        {
                            if (System.IO.File.Exists(filename))
                            {
                                System.IO.File.Delete(filename);
                            }
                            HttpContext.Current.ApplicationInstance.CompleteRequest();
                        }
                    }
                }
                else
                {
                    mensaje("El pedido no cuenta con archivo registrado");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CargarGrid()
        {
            try
            {
                List<PedidoVtaInst> List = new List<PedidoVtaInst>();
                List<PedidoVtaInst> List2 = new List<PedidoVtaInst>();
                GetList(ref List, ref List2);
               

                campos();
                gridpedidoVI.DataSource = List.ToList();
                gridpedidoVI.DataBind();

                gridviewOrderCompra.DataSource = List2.ToList();
                gridviewOrderCompra.DataBind();

                ListPedidoInternet = GetListInternet();
                rgInternet.DataSource = this.ListPedidoInternet;
                rgInternet.DataBind();


                lblCPEd.Text = List.Count().ToString();
                lblCPEdInt.Text = ListPedidoInternet.Count().ToString();

                CargarDatosOC();
            }
            catch (Exception ex)
            {
                mensaje("Error al realizar la consulta, " + ex.Message);
            }
        }

        protected void BtnCancelaPedido_ServerClick(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer c = ((System.Web.UI.HtmlControls.HtmlButton)sender).NamingContainer as GridViewDataItemTemplateContainer;

            string Pedido = c.Grid.GetRowValues(c.VisibleIndex, "Num_Pedido").ToString().Trim();
            string Estatus = c.Grid.GetRowValues(c.VisibleIndex, "Estatus_Nombre").ToString().Trim();
            string IdCte = c.Grid.GetRowValues(c.VisibleIndex, "Id_Cte").ToString().Trim();
            string tipoPedido = c.Grid.GetRowValues(c.VisibleIndex, "tipoPedido").ToString().Trim();
            string URL = c.Grid.GetRowValues(c.VisibleIndex, "Url").ToString().Trim();

            if (_PermisoEliminar)
            {
                if (tipoPedido == "1")
                {
                    string estatus = "";
                    string message = "";

                    int verificador = 0;
                    int verificador2 = 0;
                    CN_Eccommerce CN = new CN_Eccommerce();
                    APISKEY api = new APISKEY();

                    PedidoVtaInst pedido = new PedidoVtaInst();
                    pedido.Id_Emp = session.Id_Emp;
                    pedido.Id_Cd = session.Id_Cd_Ver;
                    pedido.Id_Ped = Convert.ToInt32(Pedido);
                    pedido.Ped_Tipo = 1;

                    Remision Remisiones = new Remision();
                    Remisiones.Id_Ped = Convert.ToInt32(Pedido);
                    Remisiones.Id_Emp = session.Id_Emp;
                    Remisiones.Id_Cd = session.Id_Cd_Ver;


                    CN_Eccommerce CNProFactura_Embarque = new CN_Eccommerce();
                    CNProFactura_Embarque.ValidarPrePedidoEcommerce(Remisiones, session.Emp_Cnx, ref verificador2);
                    if (verificador2 != 0)
                    {
                        pedido.PedExterno = verificador2;
                        api.CancelarPedido(pedido, 1, session.Emp_Cnx, ref estatus, ref message);
                    }
                    if (estatus == "2" || estatus == "0" || estatus == null)
                    {
                        mensaje("Ocurrió un problema al Cancelar el Pedido. " + message);
                        return;
                    }
                    else
                    {
                        CN.BajaPedido_Portal(pedido, ref verificador, session.Emp_Cnx);

                        if (verificador != 0)
                        {
                            CargarGrid();
                            //EnviarCorreo(Convert.ToInt32(rgRemisiones.Items[item]["Num_Cliente"].Text), Convert.ToInt32(lista.First().id_pedMag.ToString()), session.Emp_Cnx);
                        }
                    }
                }
            }
            else
            {
                mensaje("No tiene permisos para eliminar el pedido.");
            } 
        }
    }
}