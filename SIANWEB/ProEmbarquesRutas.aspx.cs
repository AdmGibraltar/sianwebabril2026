using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DevExpress.Web;
using CapaNegocios;
using System.Data.Common;
using DevExpress.Export.Xl;
using System.IO;



namespace SIANWEB
{
    public partial class ProEmbarquesRutas : System.Web.UI.Page
    {

        #region Variables

        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        private Sesion sesion
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

        private List<Factura> ListFac
        {
            get { return (List<Factura>)Session["ListFac"]; }
            set { Session["ListFac" + Session.SessionID] = value; }
        }
        private List<Remision> ListRem
        {
            get { return (List<Remision>)Session["ListRem"]; }
            set { Session["ListRem"] = value; }
        }

        public bool ProcEmbAlm;
        #endregion Variables

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
                        //31 enero borro la info que tenga en memoria 
                        ListFac = null;
                        ListRem = null;
                        this.ValidarPermisos();
                        if (sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        this.Inicializar();

                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_Cve.Value = randObj.Next().ToString();
                        cmbEstatus.SelectedIndex = 5;


                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #region variables paginacion

            ASPxGridView2.SettingsPager.ShowDisabledButtons = true;
            ASPxGridView2.SettingsPager.ShowNumericButtons = true;
            ASPxGridView2.SettingsPager.ShowSeparators = false;
            ASPxGridView2.SettingsPager.Summary.Visible = true;
            ASPxGridView2.SettingsPager.PageSizeItemSettings.Visible = true;


            grRemisiones.SettingsPager.ShowDisabledButtons = true;
            grRemisiones.SettingsPager.ShowNumericButtons = true;
            grRemisiones.SettingsPager.ShowSeparators = false;
            grRemisiones.SettingsPager.Summary.Visible = true;
            grRemisiones.SettingsPager.PageSizeItemSettings.Visible = true;




            #endregion


        }
        private void Inicializar()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            ProcEmbAlm = Sesion.ProcEmbAlm;
            FechaDia.Date = System.DateTime.Now;
            FechaDia.Date = DateTime.Now;
            fechafin.Date = DateTime.Now;
            FechaDia.Date.AddDays(1);
            fechafin.Date.AddDays(1);
            BootstrapDateEdit1.Date = Sesion.CalendarioIni;

            //carga la información de los grids 

            List<Factura> List = new List<Factura>();
            List = GetList();
            DataTable dt = new DataTable();

            dt = Funcion.Convertidor<Factura>.ListaToDatatable(List);
            ASPxGridView2.DataSource = dt;
            ASPxGridView2.DataBind();
            ListFac = List;

            List<Remision> Listr = new List<Remision>();
            Listr = GetListRem();
            DataTable dtrem = new DataTable();
            dtrem = Funcion.Convertidor<Remision>.ListaToDatatable(Listr);

            grRemisiones.DataSource = dtrem;
            grRemisiones.DataBind();
            ListRem = Listr;

        }

        protected void Page_Init(object sender, EventArgs e)
        {

            List<Factura> List = new List<Factura>();

            if (Page.IsPostBack)
            {

                try
                {

                    if (ListFac != null)
                    {
                        List = ListFac;
                        ASPxGridView2.DataSource = List;
                        ASPxGridView2.DataBind();

                    }
                    else
                    {
                        List = GetList();
                        DataTable dt = new DataTable();

                        dt = Funcion.Convertidor<Factura>.ListaToDatatable(List);
                        ASPxGridView2.DataSource = dt;
                        ASPxGridView2.DataBind();
                    }

                    List<Remision> Listr = new List<Remision>();

                    if (ListRem != null)
                    {
                        Listr = ListRem;
                        grRemisiones.DataSource = Listr;
                        grRemisiones.DataBind();

                    }
                    else
                    {

                        Listr = GetListRem();
                        DataTable dtrem = new DataTable();
                        dtrem = Funcion.Convertidor<Remision>.ListaToDatatable(Listr);

                        grRemisiones.DataSource = dtrem;
                        grRemisiones.DataBind();
                        ListRem = Listr;
                    }
                }
                catch (Exception ex)
                {
                    BootstrapAlert(lblMensaje, ex.Message, BootstrapAlertType.Danger, true);
                    pcrevisar.ShowOnPageLoad = true;
                    BootstrapBtnGenerar.Enabled = true;
                }

            }
        }


        private void ValidarPermisos()
        {
            try
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
                Permiso.Sm_cve = pagina.Clave; //Esta clave depende de la pantalla

                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, sesion.Emp_Cnx);

                if (Permiso.PAccesar == true)
                {
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;
                }
                else
                    Response.Redirect("Inicio.aspx");

                if (!Permiso.PGrabar)
                    this.btnGrabar.Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {



            List<Factura> List = new List<Factura>();
            DataTable dt = new DataTable();
            this.ListRem = null;
            this.ListFac = null;

            this.ASPxGridView2.DataSource = null;
            this.ASPxGridView2.DataBind();
            this.grRemisiones.DataSource = null;
            this.grRemisiones.DataBind();


            dt = Funcion.Convertidor<Factura>.ListaToDatatable(this.GetList());

            this.ASPxGridView2.DataSource = dt;
            this.ASPxGridView2.DataBind();


            List<Remision> Listrem = new List<Remision>();
            Listrem = this.GetListRem();
            DataTable dtrem = new DataTable();

            dtrem = Funcion.Convertidor<Remision>.ListaToDatatable(Listrem);

            grRemisiones.DataSource = dtrem;
            grRemisiones.DataBind();

        }

        protected void BootstrapBtnGrabar_Click(object sender, EventArgs e)
        {

            this.btnGrabar.Enabled = false;
            this.Guardar();
            this.btnGrabar.Enabled = true;

        }

        protected void BootstrapBtnGenerar_Click(object sender, EventArgs e)
        {

            pcExportar.ShowOnPageLoad = true;



        }

        #region grabar embarque

        private void Guardar()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ProEmbarque emb = new ProEmbarque();
                CN_ProEmbarque cn_emb = new CN_ProEmbarque();
                //List<ProEmbarqueDet> List = (List<ProEmbarqueDet>)Session["ListDet" + Session.SessionID];

                List<ProEmbarqueDet> List = new List<ProEmbarqueDet>();

                int Verificador = 0;
                int Id_Emb = 0;

                ProEmbarqueDet e;


                //3 de nov agregue esto porque no estaba entrando no traia nada en ASPxListBox2
                var Facturass = ASPxGridView2.GetSelectedFieldValues("Id_Fac");

                for (int ii = 0; ii < Facturass.Count; ii++)
                {

                    List<Factura> factseleccionadas = ListFac.Where(m => m.Id_Fac == Convert.ToInt32(Facturass[ii])).ToList();
                    e = new ProEmbarqueDet();
                    e.UniqueID = Guid.NewGuid().ToString();
                    e.Id_Emp = sesion.Id_Emp;
                    e.Id_Cd = sesion.Id_Cd_Ver;
                    e.Id_Emb = 0;
                    e.Id_Doc = Convert.ToInt32(Facturass[ii]);//num factura
                    e.Emb_Tipo = 1;
                    e.Id_DocStr = string.Concat("F-", Facturass[ii].ToString()); //num factura
                    e.Doc_Estatus = "E";
                    e.Doc_EstatusStr = "Embarque";
                    e.Doc_Fecha = DateTime.Now;
                    e.Id_Cte = 0;
                    e.Cte_NomComercial = "";
                    e.Doc_Importe = 0;
                    List.Add(e);
                }

                var remisioness = grRemisiones.GetSelectedFieldValues("Id_Rem");

                for (int ii = 0; ii < remisioness.Count; ii++)
                {

                    e = new ProEmbarqueDet();
                    e.UniqueID = Guid.NewGuid().ToString();
                    e.Id_Emp = sesion.Id_Emp;
                    e.Id_Cd = sesion.Id_Cd_Ver;
                    e.Id_Emb = 0;
                    e.Id_Doc = Convert.ToInt32(remisioness[ii]);
                    e.Emb_Tipo = 2;
                    e.Id_DocStr = string.Concat("R-", remisioness[ii].ToString());
                    e.Doc_Estatus = "E";
                    e.Doc_EstatusStr = "Embarque";
                    e.Doc_Fecha = System.DateTime.Now;
                    e.Id_Cte = 0;
                    e.Cte_NomComercial = "";
                    e.Doc_Importe = 0;
                    List.Add(e);

                }






                //facturas
                //3 de nov comentarice esta parte porque no estaba entrando no traia nada en ASPxListBox2
                //foreach (ListEditItem li in ASPxListBox2.Items)
                //{

                //    e = new ProEmbarqueDet();
                //    e.UniqueID = Guid.NewGuid().ToString();
                //    e.Id_Emp = sesion.Id_Emp;
                //    e.Id_Cd = sesion.Id_Cd_Ver;
                //    e.Id_Emb = 0;
                //    e.Id_Doc = Convert.ToInt32(li.Value);
                //    e.Emb_Tipo = 1;
                //    e.Id_DocStr = string.Concat("F-", li.Value.ToString());
                //    e.Doc_Estatus = "E";
                //    e.Doc_EstatusStr = "Embarque";
                //    e.Doc_Fecha = DateTime.Now;
                //    e.Id_Cte = 0;
                //    e.Cte_NomComercial = "";
                //    e.Doc_Importe = 0;
                //    List.Add(e);

                //}



                ////remisiones
                //foreach (ListEditItem item in this.ASPxListBox3.Items)
                //{

                //    e = new ProEmbarqueDet();
                //    e.UniqueID = Guid.NewGuid().ToString();
                //    e.Id_Emp = sesion.Id_Emp;
                //    e.Id_Cd = sesion.Id_Cd_Ver;
                //    e.Id_Emb = 0;
                //    e.Id_Doc = Convert.ToInt32(item.Value);
                //    e.Emb_Tipo = 2;
                //    e.Id_DocStr = string.Concat("R-", item.Value.ToString());
                //    e.Doc_Estatus = "E";
                //    e.Doc_EstatusStr = "Embarque";
                //    e.Doc_Fecha = System.DateTime.Now;
                //    e.Id_Cte = 0;
                //    e.Cte_NomComercial = "";
                //    e.Doc_Importe = 0;
                //    List.Add(e);

                //}

                if (List != null)
                {
                    if (List.Count == 0)
                    {

                        BootstrapAlert(lblMensaje, "Debe ingresar al menos un documento", BootstrapAlertType.Danger, true);
                        pcrevisar.ShowOnPageLoad = true;
                        return;
                    }

                    LlenarObjetoEmbarque(sesion, ref emb);

                    cn_emb.ProEmbarque_Insertar(emb, ref Verificador, sesion.Emp_Cnx);

                    if (Verificador == 0)
                    {
                        ProEmbarquesRutas.BootstrapAlert(lblMensaje, "Error al guardar el embarque", ProEmbarquesRutas.BootstrapAlertType.Danger, true);
                        this.pcrevisar.ShowOnPageLoad = true;
                    }
                    else
                    {
                        Id_Emb = Verificador;

                        cn_emb.ProEmbarqueDet_Insertar(List, Id_Emb, ref Verificador, sesion.Emp_Cnx);

                        if (Verificador != -1)
                        {
                            ProEmbarquesRutas.BootstrapAlert(lblMensaje, "Error al guardar el detalle del embarque", ProEmbarquesRutas.BootstrapAlertType.Danger, true);
                            pcrevisar.ShowOnPageLoad = true;
                        }
                        else
                        {
                            string mensaje = string.Concat("Se creo correctamente el embarque con folio #", Id_Emb.ToString());
                            ProEmbarquesRutas.BootstrapAlert(this.lblMensaje, mensaje, ProEmbarquesRutas.BootstrapAlertType.Success, true);
                            btnExportardespuesdegrabar.Visible = true;
                            pcrevisar.ShowOnPageLoad = true;
                            txtIdEmb.Text = Id_Emb.ToString();
                            this.ASPxListBox2.Items.Clear();
                            this.ASPxListBox3.Items.Clear();
                            this.ASPxGridView2.DataSource = null;
                            this.ASPxGridView2.DataBind();
                            this.grRemisiones.DataSource = null;
                            this.grRemisiones.DataBind();

                            // RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow('", mensaje, "')"));
                        }
                    }
                    this.ListFac = null;
                    this.ListRem = null;
                }
                else
                {
                    ProEmbarquesRutas.BootstrapAlert(this.lblMensaje, "Debe ingresar al menos un documento", ProEmbarquesRutas.BootstrapAlertType.Danger, true);
                    this.pcrevisar.ShowOnPageLoad = true;
                    return;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void Modificar()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ProEmbarque emb = new ProEmbarque();
                CN_ProEmbarque cn_emb = new CN_ProEmbarque();
                List<ProEmbarqueDet> List = (List<ProEmbarqueDet>)Session["ListDet" + Session.SessionID];
                int Verificador = 0;

                if (List.Count == 0)
                {

                    BootstrapAlert(lblMensaje, "Debe ingresar al menos un documento", BootstrapAlertType.Information, true);
                    pcrevisar.ShowOnPageLoad = true;
                    return;
                }

                LlenarObjetoEmbarque(sesion, ref emb);

                cn_emb.ProEmbarque_Modificar(emb, ref Verificador, sesion.Emp_Cnx);

                if (Verificador == -1)
                {
                    cn_emb.ProEmbarqueDet_Insertar(List, emb.Id_Emb, ref Verificador, sesion.Emp_Cnx);

                    if (Verificador == -1)
                    {

                        string mensaje = "Se creo modific� el embarque con folio #" + emb.Id_Emb.ToString();
                        BootstrapAlert(lblMensaje, mensaje, BootstrapAlertType.Success, true);

                        //RAM1.ResponseScripts.Add(string.Concat(@"CloseWindow('", mensaje, "')"));
                    }
                    else
                    {
                        BootstrapAlert(lblMensaje, "Error al modificar el detalle del embarque", BootstrapAlertType.Danger, true);
                    }

                }
                else
                {

                    BootstrapAlert(lblMensaje, "Error al tratar de modificar el embarque", BootstrapAlertType.Danger, true);
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void LlenarObjetoEmbarque(Sesion sesion, ref ProEmbarque emb)
        {
            try
            {
                emb.Id_Emp = sesion.Id_Emp;
                emb.Id_Cd = sesion.Id_Cd_Ver;
                emb.Id_Emb = 1;
                emb.Emb_Destino = 2;
                emb.Emb_Fecha = System.DateTime.Now;
                emb.Emb_Dia = FechaDia.Date;
                emb.Emb_Chofer = this.txtChofer.Text.Trim();
                emb.Emb_Camioneta = this.txtCamioneta.Text.Trim();
                emb.Id_U = sesion.Id_U;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        private List<Factura> GetList()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<Factura> listFactura = new List<Factura>();
                Factura factura = new Factura();

                if (sesion != null)
                {
                    ////if (ListFac != null)
                    ////{
                    ////    listFactura = ListFac;
                    ////}

                    bool? acuse = null;

                    acuse = null;


                    bool? depuracion = null;

                    depuracion = null;

                    bool? complementaria = null;
                    complementaria = null;
                    DateTime fechafinal = fechafin.Date;
                    DateTime fechainicial = BootstrapDateEdit1.Date;


                    new CN_CapFactura().ConsultaFactura_Buscar(factura, sesion.Emp_Cnx, ref listFactura
                        , sesion.Id_Emp
                        , sesion.Id_Cd_Ver
                        , ""  // this.txtNombre.Text
                        , -1 //this.txtCliente1.Text == string.Empty ? -1 : Convert.ToInt32(this.txtCliente1.Text)
                        , -1 //this.txtCliente2.Text == string.Empty ? -1 : Convert.ToInt32(this.txtCliente2.Text)
                        , null // cmbTipo.SelectedValue
                        , cmbEstatus.SelectedItem.Value.ToString() == "0" ? null : cmbEstatus.SelectedItem.Value.ToString()
                        , fechainicial //this.fechainicial.SelectedDate == null ? DateTime.MinValue : Convert.ToDateTime(this.txtFecha1.SelectedDate)
                        , fechafinal // this.fechafinal.SelectedDate == null ? DateTime.MinValue : Convert.ToDateTime(this.txtFecha2.SelectedDate)
                        , txtFacturaIni.Text == "" ? -1 : Convert.ToInt32(this.txtFacturaIni.Text)
                        , txtFacturaFin.Text == "" ? -1 : Convert.ToInt32(this.txtFacturaFin.Text)
                        , -1
                        , -1   //this.txtPedido2.Text == string.Empty ? -1 : Convert.ToInt32(this.txtPedido2.Text)
                        , acuse
                        , depuracion
                        , -1
                        , complementaria
                        , null
                        , "No");

                    this.ListFac = listFactura;
                    this.Session["ListFac"] = listFactura;
                    return listFactura;
                }
                else
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                    return listFactura;
                }
            }
            catch (Exception ex)
            {


                throw ex;

            }
        }


        #region modulos del grid

        protected void ASPxGridView2_cbCheck_Load(object sender, EventArgs e)
        {
            ASPxCheckBox cb = (ASPxCheckBox)sender;

            GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)cb.NamingContainer;
            cb.ClientInstanceName = string.Format("cbCheck{0}", container.VisibleIndex);
            cb.Checked = ASPxGridView2.Selection.IsRowSelected(container.VisibleIndex);

            cb.ClientSideEvents.CheckedChanged = string.Format("function (s, e) {{ ASPxGridView2.SelectRowOnPage({0}, s.GetChecked()); updateSelectedKeys(s.GetChecked()); }}", container.VisibleIndex);
        }

        protected void ASPxGridView2_cbPageSelectAll_Load(object sender, EventArgs e)
        {
            ASPxCheckBox cb = (ASPxCheckBox)sender;
            ASPxGridView grid = (cb.NamingContainer as GridViewHeaderTemplateContainer).Grid;

            bool cbChecked = true;
            int start = grid.VisibleStartIndex;
            int end = grid.VisibleStartIndex + grid.SettingsPager.PageSize;
            end = (end > grid.VisibleRowCount ? grid.VisibleRowCount : end);

            //for (int i = start; i < end; i++)
            //{
            //    DataRowView dr = (DataRowView)(grid.GetRow(i));
            //    if (!grid.Selection.IsRowSelected(i))
            //    {
            //        //if (dr["IsRegistered"] == DBNull.Value || !(bool)dr["IsRegistered"])
            //        //{
            //        //    cbChecked = false;
            //        //    break;
            //        //}
            //    }
            //}
            cb.Checked = cbChecked;
        }

        #endregion


        private List<Remision> GetListRem()//C:\Documents and Settings\Usuario\mis documentos\visual studio 2010\Projects\SIANWEB\SIANWEB\CapRutaServicio.aspx
        {
            try
            {
                List<Remision> remisiones = new List<Remision>();
                Remision remision = new Remision();
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CapRemision cn_remision = new CN_CapRemision();
                int ManAut;
                ManAut = -1; //todos los demas
                DateTime fechafinal = fechafin.Date;
                DateTime fechainicial = BootstrapDateEdit1.Date;

                if (ListRem != null)
                {
                    remisiones = ListRem;
                }


                cn_remision.ConsultarRemisiones(ref remisiones, ref remision, session,
                    "",
                    -1, //txtCliente1.Text == "" ? -1 : int.Parse(txtCliente1.Text)
                    -1, //, txtCliente2.Text == "" ? -1 : int.Parse(txtCliente2.Text),
                    ManAut,
                    fechainicial, fechafinal,
                    cmbEstatus.SelectedItem.Value.ToString() == "0" ? null : cmbEstatus.SelectedItem.Value.ToString(),
                    -1,//txtPedido1.Text == "" ? -1 : int.Parse(txtPedido1.Text)
                    -1, // txtPedido2.Text == "" ? -1 : int.Parse(txtPedido2.Text),
                    -1, //txtPedido3.Text == "" ? -1 : int.Parse(txtPedido3.Text),
                    "", //TxtRem_OrdCompra.Text,
                    -1); //txtTipoId.Text == "" ? -1 : int.Parse(txtTipoId.Text));
                //, txtCliente2.Text == "" ? -1 : int.Parse(txtTipoId.Text));


                ListRem = remisiones;
                this.Session["ListRem"] = remisiones;

                return remisiones;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public enum BootstrapAlertType
        {
            Plain,
            Success,
            Information,
            Warning,
            Danger,
            Primary
        }


        private void ImprimirExcel()
        {
            try
            {
                CN_ProEmbarque clsCatArea = new CN_ProEmbarque();
                Embarques embarques = new Embarques();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];


                embarques.Id_Emb = Convert.ToInt32(txtIdEmb.Text);
                embarques.Id_Cd = sesion.Id_Cd_Ver;
                embarques.Id_Emp = sesion.Id_Emp;

                List<EmbarquesReporte> List = new List<EmbarquesReporte>();
                clsCatArea.ListaEmbarquePlanner(embarques, sesion.Emp_Cnx, ref List);

                Session["gridembarques"] = List;


                List = (List<EmbarquesReporte>)Session["gridembarques"];

                // Create an exporter instance.  
                IXlExporter exporter = XlExport.CreateExporter(XlDocumentFormat.Xlsx);

                var mappath = HttpContext.Current.Server.MapPath("~/Reportes/RerpoteCaptacionPedidos" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx");
                // Create the FileStream object with the specified file path.  

                if (File.Exists(mappath))
                    File.Delete(mappath);

                #region excel

                #region ancho de columnas
                using (FileStream stream = new FileStream(mappath, FileMode.Create, FileAccess.ReadWrite))
                {
                    // Create a new document and begin to write it to the specified stream.  
                    using (IXlDocument document = exporter.CreateDocument(stream))
                    {
                        // Add a new worksheet to the document.  
                        using (IXlSheet sheet = document.CreateSheet())
                        {


                            // Specify the worksheet name. 
                            sheet.Name = "IMPORTADOR";

                            // Create the first column and set its width.  
                            using (IXlColumn column = sheet.CreateColumn())         //no documento
                            {
                                column.WidthInPixels = 100;
                            }

                            using (IXlColumn column = sheet.CreateColumn()) // Latitud
                            {
                                column.WidthInPixels = 150;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) // Longitud
                            {
                                column.WidthInPixels = 150;
                            }

                            using (IXlColumn column = sheet.CreateColumn()) // direccion
                            {
                                column.WidthInPixels = 450;
                            }

                            // Create the third column and set the specific number format for its cells. 
                            using (IXlColumn column = sheet.CreateColumn())
                            {
                                column.WidthInPixels = 350;   //nOMBRE iTEM
                            }

                            using (IXlColumn column = sheet.CreateColumn()) //cantidad
                            {
                                column.WidthInPixels = 100;
                            }

                            using (IXlColumn column = sheet.CreateColumn()) //codigo
                            {
                                column.WidthInPixels = 100;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) // Fecha min entrega
                            {
                                column.WidthInPixels = 150;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) // fecha max entrega
                            {
                                column.WidthInPixels = 150;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) // MinVentanaHor1
                            {
                                column.WidthInPixels = 150;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) // MaxVentanaHor1
                            {
                                column.WidthInPixels = 150;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) // MinVentanaHor2
                            {
                                column.WidthInPixels = 150;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) // MaxVentanaHor2
                            {
                                column.WidthInPixels = 150;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) //Costo
                            {
                                column.WidthInPixels = 150;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) //Capacidad Uno
                            {
                                column.WidthInPixels = 150;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) //Capacidad Dos
                            {
                                column.WidthInPixels = 150;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) //Service Time
                            {
                                column.WidthInPixels = 150;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) //Importancia
                            {
                                column.WidthInPixels = 150;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) //Identificador Contacto
                            {
                                column.WidthInPixels = 150;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) //Nombrecontacto
                            {
                                column.WidthInPixels = 350;
                            }

                            using (IXlColumn column = sheet.CreateColumn()) //Telefono
                            {
                                column.WidthInPixels = 100;
                            }

                            using (IXlColumn column = sheet.CreateColumn()) // Email_contacto
                            {
                                column.WidthInPixels = 150;
                            }

                            using (IXlColumn column = sheet.CreateColumn()) // CT ORIGEN
                            {
                                column.WidthInPixels = 100;
                            }


                            #endregion ancho de columnas

                            #region Encabezados



                            XlCellFormatting cellFormatting = new XlCellFormatting();
                            cellFormatting.Font = new XlFont();
                            cellFormatting.Font.Name = "Century Gothic";
                            cellFormatting.Font.SchemeStyle = XlFontSchemeStyles.None;

                            // Specify formatting settings for the header row. 
                            XlCellFormatting headerRowFormatting = new XlCellFormatting();
                            headerRowFormatting.CopyFrom(cellFormatting);
                            headerRowFormatting.Font.Bold = true;
                            headerRowFormatting.Fill = XlFill.SolidFill(XlColor.FromTheme(XlThemeColor.Accent4, 0.0));


                            // Create the header row. 
                            using (IXlRow row = sheet.CreateRow())
                            {
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "N° DOCUMENTO";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "LATITUD";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "LONGITUD";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "DIRECCION";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "NOMBRE ITEM";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "CANTIDAD";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "CODIGO ITEM";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "FECHA MIN ENTREGA";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "FECHA MAX ENTREGA";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "MIN VENTANA HORARIA 1";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "MAX VENTANA HORARIA 1";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "MIN VENTANA HORARIA 2";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "MAX VENTANA HORARIA 2";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "COSTO ITEM";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "CAPACIDAD UNO";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "CAPACIDAD DOS";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }

                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "SERVICE TIME";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "IMPORTANCIA";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "IDENTIFICADOR CONTACTO";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "NOMBRE CONTACTO";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "TELEFONO";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "EMAIL CONTACTO";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }

                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "CT ORIGEN";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Zona";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }

                                // using (IXlCell cell = row.CreateCell())
                                //{
                                //    cell.Value = "CAMPOS AVANZADOS";
                                //    cell.ApplyFormatting(headerRowFormatting);
                                //}

                            }

                            #endregion Encabezados


                            #region Columnas datos
                            // Generate data for the sales report.  
                            for (int i = 0; i < List.Count; i++)
                            {
                                using (IXlRow row = sheet.CreateRow())
                                {
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Id_Doc;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Latitud;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Longitud;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Direccion;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Prd_Descripcion;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Fac_Cant;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Id_Prd;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {

                                        /////cell.Value = FechaDia.Date;
                                        // //JFCV 18 dic fecha de pantalla de exportar  cell.Value = List[i].Fecha_MIN_ENTREGA;
                                        //cell.Formatting = new XlCellFormatting();
                                        // cell.Formatting.NumberFormat = "dd/mm/yyyy";


                                        //cell.Value = List[i].Prd_Descripcion;
                                        //cell.ApplyFormatting(cellFormatting);

                                        //DateTime test = FechaDia.Date;
                                        string sdia = "0" + FechaDia.Date.Day.ToString().Trim();
                                        sdia = sdia.Substring(sdia.ToString().Trim().Length - 2, 2);

                                        //  string sdia = "0".Substring(2 - FechaDia.Date.Day.ToString().Trim().Length, 2);
                                        string smes = "0" + FechaDia.Date.Month.ToString().Trim();
                                        smes = smes.Substring(smes.ToString().Trim().Length - 2, 2);


                                        string sanio = FechaDia.Date.Year.ToString();

                                        cell.Value = sdia + '/' + smes + '/' + sanio;
                                        cell.ApplyFormatting(cellFormatting);


                                    }

                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        // Apply the custom number format to the date value. 
                                        // Display days as Sunday�Saturday, months as January�December, days as 1�31 and years as 1900�9999. 
                                        //JFCV 18 dic fecha de pantalla de exportar cell.Value = List[i].Fecha_Max_entrega;
                                        ////cell.Value = FechaDia.Date;
                                        ////cell.Formatting = new XlCellFormatting();
                                        ////cell.Formatting.NumberFormat = "dd/mm/yyyy";


                                        string sdia = "0" + FechaDia.Date.Day.ToString().Trim();
                                        sdia = sdia.Substring(sdia.ToString().Trim().Length - 2, 2);
                                        string smes = "0" + FechaDia.Date.Month.ToString().Trim();
                                        smes = smes.Substring(smes.ToString().Trim().Length - 2, 2);


                                        string sanio = FechaDia.Date.Year.ToString();

                                        cell.Value = sdia + '/' + smes + '/' + sanio;
                                        cell.ApplyFormatting(cellFormatting);

                                    }

                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        // Apply the custom number format to the date value. 
                                        // Display days as Sunday�Saturday, months as January�December, days as 1�31 and years as 1900�9999. 
                                        string horaam1 = List[i].MIN_VENTANAHORARIA1;
                                        cell.Value = horaam1;
                                        cell.Formatting = new XlCellFormatting();
                                        // cell.Formatting.NumberFormat = "hh:mm";
                                    }

                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        // Apply the custom number format to the date value. 
                                        // Display days as Sunday�Saturday, months as January�December, days as 1�31 and years as 1900�9999. 
                                        //cell.Value = List[i].MAX_VENTANAHORARIA1;
                                        //cell.Formatting = new XlCellFormatting();
                                        //cell.Formatting.NumberFormat = "hh:mm";
                                        //JFCV 24 enero formato horas en texto
                                        string horaam2 = List[i].MAX_VENTANAHORARIA1;
                                        cell.Value = horaam2;
                                        cell.Formatting = new XlCellFormatting();

                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        // Apply the custom number format to the date value. 
                                        // Display days as Sunday�Saturday, months as January�December, days as 1�31 and years as 1900�9999. 
                                        //cell.Value = List[i].MIN_VENTANAHORARIA2;
                                        //cell.Formatting = new XlCellFormatting();
                                        //cell.Formatting.NumberFormat = "hh:mm";
                                        //JFCV 24 enero formato horas en texto
                                        string horapm1 = List[i].MIN_VENTANAHORARIA2;
                                        cell.Value = horapm1;
                                        cell.Formatting = new XlCellFormatting();

                                    }

                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        // Apply the custom number format to the date value. 
                                        // Display days as Sunday�Saturday, months as January�December, days as 1�31 and years as 1900�9999. 
                                        //cell.Value = List[i].MAX_VENTANAHORARIA2;
                                        //cell.Formatting = new XlCellFormatting();
                                        //cell.Formatting.NumberFormat = "hh:mm";
                                        //JFCV 24 enero formato horas en texto
                                        string horapm2 = List[i].MAX_VENTANAHORARIA2;
                                        cell.Value = horapm2;
                                        cell.Formatting = new XlCellFormatting();

                                    }
                                    using (IXlCell cell = row.CreateCell())   //Costo 
                                    {
                                        cell.Value = Convert.ToString(List[i].Costo);
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = Convert.ToString(List[i].Peso);
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = Convert.ToString(List[i].Volumen);
                                        cell.ApplyFormatting(cellFormatting);
                                    }

                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].ServiceTime;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Importancia;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Id_Contacto;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].NomContacto;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Telefono;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Email_Contacto;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].CT_Destino;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Zona;
                                        cell.ApplyFormatting(cellFormatting);
                                    }

                                    //using (IXlCell cell = row.CreateCell())
                                    //{
                                    //    cell.Value = List[i].Emb_Camioneta;
                                    //    cell.ApplyFormatting(cellFormatting);
                                    //}

                                    //using (IXlCell cell = row.CreateCell())
                                    //{
                                    //    cell.Value = List[i].Campos_Avanzados;
                                    //    cell.ApplyFormatting(cellFormatting);
                                    //}
                                }
                            }

                            #endregion Columnas datos

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
                #endregion excel

                Console.WriteLine(mappath);
                // Open the XLSX document using the default application. 
                ////System.Diagnostics.Process.Start(mappath);

                if (File.Exists(mappath))
                {
                    string ruta2 = null;
                    ruta2 = Server.MapPath("Reportes") + "\\ReporteRutasEmbarquePlanner_" + txtIdEmb.Text + "_" + FechaDia.Date.Year.ToString().Trim() + FechaDia.Date.Month.ToString().Trim() + FechaDia.Date.Day.ToString().Trim() + ".xlsx";
                    if (File.Exists(ruta2))
                    {
                        File.Delete(ruta2);
                    }
                    File.Move(mappath, Server.MapPath("Reportes") + "\\ReporteRutasEmbarquePlanner_" + txtIdEmb.Text + "_" + FechaDia.Date.Year.ToString().Trim() + FechaDia.Date.Month.ToString().Trim() + FechaDia.Date.Day.ToString().Trim() + ".xlsx");
                    Response.Redirect("Reportes\\ReporteRutasEmbarquePlanner_" + txtIdEmb.Text + "_" + FechaDia.Date.Year.ToString().Trim() + FechaDia.Date.Month.ToString().Trim() + FechaDia.Date.Day.ToString().Trim() + ".xlsx", false);
                }
                BootstrapBtnGenerar.Enabled = true;

            }
            catch (Exception ex)
            {
                BootstrapAlert(lblMensaje, ex.Message, BootstrapAlertType.Danger, true);
                pcrevisar.ShowOnPageLoad = true;
                BootstrapBtnGenerar.Enabled = true;
            }
        }

        private void ImprimirExcelDriveIn()
        {
            try
            {
                CN_ProEmbarque clsCatArea = new CN_ProEmbarque();
                Embarques embarques = new Embarques();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];


                embarques.Id_Emb = Convert.ToInt32(txtIdEmb.Text);
                embarques.Id_Cd = sesion.Id_Cd_Ver;
                embarques.Id_Emp = sesion.Id_Emp;

                List<EmbarquesReporte> List = new List<EmbarquesReporte>();
                clsCatArea.ListaEmbarquePlanner(embarques, sesion.Emp_Cnx, ref List);

                Session["gridembarques"] = List;


                List = (List<EmbarquesReporte>)Session["gridembarques"];

                // Create an exporter instance.  
                IXlExporter exporter = XlExport.CreateExporter(XlDocumentFormat.Xlsx);

                var mappath = HttpContext.Current.Server.MapPath("~/Reportes/RerpoteCaptacionPedidos" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx");
                // Create the FileStream object with the specified file path.  

                if (File.Exists(mappath))
                    File.Delete(mappath);

                #region excel

                #region ancho de columnas
                using (FileStream stream = new FileStream(mappath, FileMode.Create, FileAccess.ReadWrite))
                {
                    // Create a new document and begin to write it to the specified stream.  
                    using (IXlDocument document = exporter.CreateDocument(stream))
                    {
                        // Add a new worksheet to the document.  
                        using (IXlSheet sheet = document.CreateSheet())
                        {


                            // Specify the worksheet name. 
                            sheet.Name = "IMPORTADOR";

                            // Create the first column and set its width.  
                            using (IXlColumn column = sheet.CreateColumn())         //no documento
                            {
                                column.WidthInPixels = 100;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) //Capacidad Uno
                            {
                                column.WidthInPixels = 150;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) //Capacidad Dos
                            {
                                column.WidthInPixels = 150;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) //Unidades_3
                            {
                                column.WidthInPixels = 90;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) //Prioridad
                            {
                                column.WidthInPixels = 90;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) //IDENTIFICADOR CONTACTO Identificador Contacto
                            {
                                column.WidthInPixels = 150;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) // Nombre direccion
                            {
                                column.WidthInPixels = 450;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) //Nombrecontacto
                            {
                                column.WidthInPixels = 350;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) //Tipo
                            {
                                column.WidthInPixels = 90;
                            }

                            using (IXlColumn column = sheet.CreateColumn()) //Campo nuevo calle  Dirección 1*
                            {
                                column.WidthInPixels = 200;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) //Referencias
                            {
                                column.WidthInPixels = 90;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) //Descripción
                            {
                                column.WidthInPixels = 90;
                            }





                            using (IXlColumn column = sheet.CreateColumn()) //comuna  Campo nuevo colonia
                            {
                                column.WidthInPixels = 200;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) //Campo nuevo Municipio
                            {
                                column.WidthInPixels = 200;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) //Campo nuevo estado
                            {
                                column.WidthInPixels = 200;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) //Campo nuevo país
                            {
                                column.WidthInPixels = 100;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) //Campo nuevo Codigo postal
                            {
                                column.WidthInPixels = 100;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) // Latitud
                            {
                                column.WidthInPixels = 150;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) // Longitud
                            {
                                column.WidthInPixels = 150;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) //Service Time
                            {
                                column.WidthInPixels = 150;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) // MinVentanaHor1
                            {
                                column.WidthInPixels = 150;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) // MaxVentanaHor1
                            {
                                column.WidthInPixels = 150;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) //nuevo características 
                            {
                                column.WidthInPixels = 80;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) //ZONA vehiculo ???
                            {
                                column.WidthInPixels = 100;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) //Telefono
                            {
                                column.WidthInPixels = 100;
                            }

                            using (IXlColumn column = sheet.CreateColumn()) // Email_contacto
                            {
                                column.WidthInPixels = 150;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) //cantidad
                            {
                                column.WidthInPixels = 100;
                            }

                            using (IXlColumn column = sheet.CreateColumn()) //codigo
                            {
                                column.WidthInPixels = 100;
                            }
                            // Create the third column and set the specific number format for its cells. 
                            using (IXlColumn column = sheet.CreateColumn())
                            {
                                column.WidthInPixels = 350;   //nOMBRE iTEM
                            }
                            using (IXlColumn column = sheet.CreateColumn())
                            {
                                column.WidthInPixels = 80;   // nuevo Exclusividad
                            }
                            using (IXlColumn column = sheet.CreateColumn())
                            {
                                column.WidthInPixels = 80;   // nuevo Secuencia
                            }
                            using (IXlColumn column = sheet.CreateColumn())
                            {
                                column.WidthInPixels = 80;   //nuevo Proveedor de Carga
                            }


                            using (IXlColumn column = sheet.CreateColumn()) // Inicio ventana 1 MinVentanaHor2
                            {
                                column.WidthInPixels = 150;
                            }
                            using (IXlColumn column = sheet.CreateColumn()) // Fin ventana 2 MaxVentanaHor2
                            {
                                column.WidthInPixels = 150;
                            }


                            // vacios  AD 

                            for (int ad = 0; ad < 35; ad++)
                            {
                                using (IXlColumn column = sheet.CreateColumn()) // Fin ventana 2 MaxVentanaHor2
                                {
                                    column.WidthInPixels = 80;
                                }


                            }





                            //using (IXlColumn column = sheet.CreateColumn()) // Fecha min entrega
                            //{
                            //    column.WidthInPixels = 150;
                            //}
                            //using (IXlColumn column = sheet.CreateColumn()) // fecha max entrega
                            //{
                            //    column.WidthInPixels = 150;
                            //}


                            //using (IXlColumn column = sheet.CreateColumn()) //Costo
                            //{
                            //    column.WidthInPixels = 150;
                            //}


                            //using (IXlColumn column = sheet.CreateColumn()) //Importancia
                            //{
                            //    column.WidthInPixels = 150;
                            //}

                            //using (IXlColumn column = sheet.CreateColumn()) // CT ORIGEN
                            //{
                            //    column.WidthInPixels = 100;
                            //}


                            #endregion ancho de columnas

                            #region Encabezados



                            XlCellFormatting cellFormatting = new XlCellFormatting();
                            cellFormatting.Font = new XlFont();
                            cellFormatting.Font.Name = "Century Gothic";
                            cellFormatting.Font.SchemeStyle = XlFontSchemeStyles.None;

                            // Specify formatting settings for the header row. 
                            XlCellFormatting headerRowFormatting = new XlCellFormatting();
                            headerRowFormatting.CopyFrom(cellFormatting);
                            headerRowFormatting.Font.Bold = true;
                            headerRowFormatting.Fill = XlFill.SolidFill(XlColor.FromTheme(XlThemeColor.Accent4, 0.0));


                            // Create the header row. 
                            using (IXlRow row = sheet.CreateRow())
                            {
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Código de despacho*";   //N° DOCUMENTO
                                    cell.ApplyFormatting(headerRowFormatting);
                                }

                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Unidades_1* (KG)"; //CAPACIDAD UNO
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Unidades_2 (m3)";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Unidades_3";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Prioridad";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Código de dirección*"; //IDENTIFICADOR CONTACTO
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Nombre dirección"; //DIRECCION
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Nombre Cliente";  //NOMBRE CONTACTO
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Tipo";  //Tipo
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Dirección 1*"; //calle
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Referencias"; //nuevo referencia vacio 
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Descripción"; //nuevo vacio
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Comuna*";  //colonia
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Provincia"; //Municipio
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Región";   //estado O
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "País*";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Código Postal";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Latitud";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Longitud";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Tiempo de servicio";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Inicio Ventana 1"; // MIN VENTANA HORARIA 1";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Fin Ventana 1";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "características";  //campo nuevo vacio
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Asignación vehículo";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Telefono de Contacto";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Email de Contacto";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Unidades del artículo"; //cantidad
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Código del artículo";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Descripción del artículo";  //nombre item
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Exclusividad";  //nuevo campo
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Secuencia";  //nuevo campo
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Proveedor de Carga";  //nuevo campo
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Inicio ventana 2";  //MIN VENTANA HORARIA 2
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Fin ventana 2"; //MAX VENTANA HORARIA 2
                                    cell.ApplyFormatting(headerRowFormatting);
                                }

                                ///agrego columnas en blanco 
                                ///

                                ///25 nov agregue encabezados a 5 columnas 
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Código cliente"; //AI
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Nombre de contacto"; //AJ
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Código Alternativo"; //AK
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Mail aprobar ruta"; //AL
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Mail iniciar ruta"; //AM
                                    cell.ApplyFormatting(headerRowFormatting);
                                }


                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Mail en camino a direccion";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }

                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Mail entrega finalizada";
                                    cell.ApplyFormatting(headerRowFormatting);
                                }

                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Código de ruta"; //
                                    cell.ApplyFormatting(headerRowFormatting);
                                }

                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Número de viaje"; // AQ
                                    cell.ApplyFormatting(headerRowFormatting);
                                }


                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Tipo Unidad"; // AR
                                    cell.ApplyFormatting(headerRowFormatting);
                                }

                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Tipo de Factura"; // AS
                                    cell.ApplyFormatting(headerRowFormatting);
                                }

                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Condición de Pago"; // AT
                                    cell.ApplyFormatting(headerRowFormatting);
                                }

                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Valor"; // AU
                                    cell.ApplyFormatting(headerRowFormatting);
                                }

                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Texto 4"; // AV
                                    cell.ApplyFormatting(headerRowFormatting);
                                }

                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Texto 5"; // AW 
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Texto 6"; // AX 
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Texto 7"; // AY 
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Texto 8"; // AZ 
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Texto 9"; // BA
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Texto 10"; // BB 
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Texto 11"; // BC
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Número 1"; // BD
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Número 2"; // BE
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Número 3"; // BF
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Número 4"; // BG
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Correo Conductor"; // BH
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Costo Asignación"; // BI
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Ruta Maestra"; // BJ
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Descripción despacho"; // BK
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Codigo zona de ventas"; // BL
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "unidades requeridas por item"; // BM
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Telefono contacto ruta aprobada"; // BN
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Telefono contacto ruta iniciada"; // BO
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Telefono contacto cerca del lugar"; // BP
                                    cell.ApplyFormatting(headerRowFormatting);
                                }
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = "Telefono contacto entrega"; // BQ
                                    cell.ApplyFormatting(headerRowFormatting);
                                }



                                //using (IXlCell cell = row.CreateCell())
                                //{
                                //    cell.Value = "FECHA MIN ENTREGA";
                                //    cell.ApplyFormatting(headerRowFormatting);
                                //}
                                //using (IXlCell cell = row.CreateCell())
                                //{
                                //    cell.Value = "FECHA MAX ENTREGA";
                                //    cell.ApplyFormatting(headerRowFormatting);
                                //}





                                //using (IXlCell cell = row.CreateCell())
                                //{
                                //    cell.Value = "COSTO ITEM";
                                //    cell.ApplyFormatting(headerRowFormatting);
                                //}



                                //using (IXlCell cell = row.CreateCell())
                                //{
                                //    cell.Value = "IMPORTANCIA";
                                //    cell.ApplyFormatting(headerRowFormatting);
                                //}



                                //using (IXlCell cell = row.CreateCell())
                                //{
                                //    cell.Value = "CT ORIGEN";
                                //    cell.ApplyFormatting(headerRowFormatting);
                                //}


                                //// using (IXlCell cell = row.CreateCell())
                                ////{
                                ////    cell.Value = "CAMPOS AVANZADOS";
                                ////    cell.ApplyFormatting(headerRowFormatting);
                                ////}

                            }

                            #endregion Encabezados


                            #region Columnas datos
                            // Generate data for the sales report.  
                            for (int i = 0; i < List.Count; i++)
                            {
                                using (IXlRow row = sheet.CreateRow())
                                {
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Id_Doc;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = Convert.ToString(List[i].Peso);
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = Convert.ToString(List[i].Volumen);
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())   //Unidades_3
                                    {
                                        cell.Value = "";
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())  //Prioridad
                                    {
                                        cell.Value = "";
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Id_Contacto;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].NomContacto;   // tenia  Direccion;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].NomContacto;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())  //Tipo
                                    {
                                        cell.Value = "";
                                        cell.ApplyFormatting(cellFormatting);
                                    }

                                    using (IXlCell cell = row.CreateCell())  // direccion 1 ( calle ) 
                                    {
                                        cell.Value = List[i].Calle;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())  //Refrencia K
                                    {
                                        cell.Value = "";
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())  //Descripción L
                                    {
                                        cell.Value = "";
                                        cell.ApplyFormatting(cellFormatting);
                                    }

                                    using (IXlCell cell = row.CreateCell())  // comuna M
                                    {
                                        cell.Value = List[i].Colonia;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())  // provincia N
                                    {
                                        cell.Value = List[i].Municipio;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())  //Región O
                                    {
                                        cell.Value = List[i].Estado;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Pais;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].CodigoPostal;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Latitud;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Longitud;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].ServiceTime;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        // Apply the custom number format to the date value. 
                                        // Display days as Sunday�Saturday, months as January�December, days as 1�31 and years as 1900�9999. 
                                        string horaam1 = List[i].MIN_VENTANAHORARIA1;
                                        cell.Value = horaam1;
                                        cell.Formatting = new XlCellFormatting();
                                        // cell.Formatting.NumberFormat = "hh:mm";
                                    }

                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        // Apply the custom number format to the date value. 
                                        // Display days as Sunday�Saturday, months as January�December, days as 1�31 and years as 1900�9999. 
                                        //cell.Value = List[i].MAX_VENTANAHORARIA1;
                                        //cell.Formatting = new XlCellFormatting();
                                        //cell.Formatting.NumberFormat = "hh:mm";
                                        //JFCV 24 enero formato horas en texto
                                        string horaam2 = List[i].MAX_VENTANAHORARIA1;
                                        cell.Value = horaam2;
                                        cell.Formatting = new XlCellFormatting();

                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = ""; //caracteristicas  nuevo vacio 
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Zona;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Telefono;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Email_Contacto;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Fac_Cant;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Id_Prd;
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        cell.Value = List[i].Prd_Descripcion;
                                        cell.ApplyFormatting(cellFormatting);
                                    }

                                    using (IXlCell cell = row.CreateCell())  //Exclusividad
                                    {
                                        cell.Value = "";
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())  //Secuencia
                                    {
                                        cell.Value = "";
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())  //Proveedor de Carga
                                    {
                                        cell.Value = "";
                                        cell.ApplyFormatting(cellFormatting);
                                    }

                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        // Apply the custom number format to the date value. 
                                        // Display days as Sunday�Saturday, months as January�December, days as 1�31 and years as 1900�9999. 
                                        //cell.Value = List[i].MIN_VENTANAHORARIA2;
                                        //cell.Formatting = new XlCellFormatting();
                                        //cell.Formatting.NumberFormat = "hh:mm";
                                        //JFCV 24 enero formato horas en texto
                                        string horapm1 = List[i].MIN_VENTANAHORARIA2; //inicio ventana 2
                                        cell.Value = horapm1;
                                        cell.Formatting = new XlCellFormatting();

                                    }

                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        // Apply the custom number format to the date value. 
                                        // Display days as Sunday�Saturday, months as January�December, days as 1�31 and years as 1900�9999. 
                                        //cell.Value = List[i].MAX_VENTANAHORARIA2;
                                        //cell.Formatting = new XlCellFormatting();
                                        //cell.Formatting.NumberFormat = "hh:mm";
                                        //JFCV 24 enero formato horas en texto
                                        string horapm2 = List[i].MAX_VENTANAHORARIA2; //Fin ventana 2
                                        cell.Value = horapm2;
                                        cell.Formatting = new XlCellFormatting();

                                    }

                                    // vacios  AD 

                                    using (IXlCell cell = row.CreateCell())  //AL   Código cliente
                                    {
                                        cell.Value = " ";
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())  // AJ Nombre de contacto
                                    {
                                        cell.Value = " ";
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())  // AK Código Alternativo
                                    {
                                        cell.Value = " ";
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())  // AL Mail aprobar ruta
                                    {
                                        cell.Value = " ";
                                        cell.ApplyFormatting(cellFormatting);
                                    }
                                    using (IXlCell cell = row.CreateCell())  // AM  Mail iniciar ruta
                                    {
                                        cell.Value = " ";
                                        cell.ApplyFormatting(cellFormatting);
                                    }

                                    for (int ad = 0; ad < 30; ad++)
                                    {
                                        using (IXlCell cell = row.CreateCell())  //desde celda AD hasta el final 
                                        {
                                            cell.Value = " ";
                                            cell.ApplyFormatting(cellFormatting);
                                        }

                                    }




                                    //using (IXlCell cell = row.CreateCell())
                                    //{

                                    //    /////cell.Value = FechaDia.Date;
                                    //    // //JFCV 18 dic fecha de pantalla de exportar  cell.Value = List[i].Fecha_MIN_ENTREGA;
                                    //    //cell.Formatting = new XlCellFormatting();
                                    //    // cell.Formatting.NumberFormat = "dd/mm/yyyy";


                                    //    //cell.Value = List[i].Prd_Descripcion;
                                    //    //cell.ApplyFormatting(cellFormatting);

                                    //    //DateTime test = FechaDia.Date;
                                    //    string sdia = "0" + FechaDia.Date.Day.ToString().Trim();
                                    //    sdia = sdia.Substring(sdia.ToString().Trim().Length - 2, 2);

                                    //    //  string sdia = "0".Substring(2 - FechaDia.Date.Day.ToString().Trim().Length, 2);
                                    //    string smes = "0" + FechaDia.Date.Month.ToString().Trim();
                                    //    smes = smes.Substring(smes.ToString().Trim().Length - 2, 2);


                                    //    string sanio = FechaDia.Date.Year.ToString();

                                    //    cell.Value = sdia + '/' + smes + '/' + sanio;
                                    //    cell.ApplyFormatting(cellFormatting);


                                    //}

                                    //using (IXlCell cell = row.CreateCell())
                                    //{
                                    //    // Apply the custom number format to the date value. 
                                    //    // Display days as Sunday�Saturday, months as January�December, days as 1�31 and years as 1900�9999. 
                                    //    //JFCV 18 dic fecha de pantalla de exportar cell.Value = List[i].Fecha_Max_entrega;
                                    //    ////cell.Value = FechaDia.Date;
                                    //    ////cell.Formatting = new XlCellFormatting();
                                    //    ////cell.Formatting.NumberFormat = "dd/mm/yyyy";


                                    //    string sdia = "0" + FechaDia.Date.Day.ToString().Trim();
                                    //    sdia = sdia.Substring(sdia.ToString().Trim().Length - 2, 2);
                                    //    string smes = "0" + FechaDia.Date.Month.ToString().Trim();
                                    //    smes = smes.Substring(smes.ToString().Trim().Length - 2, 2);


                                    //    string sanio = FechaDia.Date.Year.ToString();

                                    //    cell.Value = sdia + '/' + smes + '/' + sanio;
                                    //    cell.ApplyFormatting(cellFormatting);

                                    //}



                                    //using (IXlCell cell = row.CreateCell())   //Costo 
                                    //{
                                    //    cell.Value = Convert.ToString(List[i].Costo);
                                    //    cell.ApplyFormatting(cellFormatting);
                                    //}



                                    //using (IXlCell cell = row.CreateCell())
                                    //{
                                    //    cell.Value = List[i].Importancia;
                                    //    cell.ApplyFormatting(cellFormatting);
                                    //}


                                    //using (IXlCell cell = row.CreateCell())
                                    //{
                                    //    cell.Value = List[i].CT_Destino;
                                    //    cell.ApplyFormatting(cellFormatting);
                                    //}


                                    ////using (IXlCell cell = row.CreateCell())
                                    ////{
                                    ////    cell.Value = List[i].Emb_Camioneta;
                                    ////    cell.ApplyFormatting(cellFormatting);
                                    ////}

                                    ////using (IXlCell cell = row.CreateCell())
                                    ////{
                                    ////    cell.Value = List[i].Campos_Avanzados;
                                    ////    cell.ApplyFormatting(cellFormatting);
                                    ////}
                                }
                            }

                            #endregion Columnas datos

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
                #endregion excel

                Console.WriteLine(mappath);
                // Open the XLSX document using the default application. 
                ////System.Diagnostics.Process.Start(mappath);

                if (File.Exists(mappath))
                {
                    string ruta2 = null;
                    ruta2 = Server.MapPath("Reportes") + "\\ReporteRutasEmbarquePlanner_" + txtIdEmb.Text + "_" + FechaDia.Date.Year.ToString().Trim() + FechaDia.Date.Month.ToString().Trim() + FechaDia.Date.Day.ToString().Trim() + ".xlsx";
                    if (File.Exists(ruta2))
                    {
                        File.Delete(ruta2);
                    }
                    File.Move(mappath, Server.MapPath("Reportes") + "\\ReporteRutasEmbarquePlanner_" + txtIdEmb.Text + "_" + FechaDia.Date.Year.ToString().Trim() + FechaDia.Date.Month.ToString().Trim() + FechaDia.Date.Day.ToString().Trim() + ".xlsx");
                    Response.Redirect("Reportes\\ReporteRutasEmbarquePlanner_" + txtIdEmb.Text + "_" + FechaDia.Date.Year.ToString().Trim() + FechaDia.Date.Month.ToString().Trim() + FechaDia.Date.Day.ToString().Trim() + ".xlsx", false);
                }
                BootstrapBtnGenerar.Enabled = true;

            }
            catch (Exception ex)
            {
                BootstrapAlert(lblMensaje, ex.Message, BootstrapAlertType.Danger, true);
                pcrevisar.ShowOnPageLoad = true;
                BootstrapBtnGenerar.Enabled = true;
            }
        }



        public static void BootstrapAlert(Label MsgLabel, string Message, BootstrapAlertType MessageType, bool Dismissable)
        {
            string style, icon;

            style = "warning";
            icon = "warning";

            switch (MessageType)
            {
                case BootstrapAlertType.Plain:
                    style = "default";
                    icon = "";
                    break;
                case BootstrapAlertType.Success:
                    style = "success";
                    icon = "check";
                    break;

                case BootstrapAlertType.Information:
                    {
                        style = "info";
                        icon = "info-circle";
                        break;
                    }

                case BootstrapAlertType.Warning:
                    {
                        style = "warning";
                        icon = "warning";
                        break;
                    }

                case BootstrapAlertType.Danger:
                    {
                        style = "danger";
                        icon = "remove";
                        break;
                    }

                case BootstrapAlertType.Primary:
                    {
                        style = "primary";
                        icon = "info";
                        break;
                    }
            }

            if ((!MsgLabel.Page.IsPostBack | MsgLabel.Page.IsPostBack) & Message == null)
                MsgLabel.Visible = false;
            else
            {
                MsgLabel.Visible = true;
                //MsgLabel.CssClass = "alert alert-" + style + Dismissable == true ? " alert-dismissible fade in font2" : "";
                MsgLabel.CssClass = "alert alert-" + style + " alert-dismissible fade in font2";
                MsgLabel.Text = "<i class='fa fa-" + icon + "'></i>" + Message;
                if (Dismissable == true)
                    MsgLabel.Text += @"<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";
                MsgLabel.Focus();
                Message = "";

            }
        }



        /// <summary>
        /// FUNCION QUE ABRE EL MODAL Y MUESTRA LOS DATOS DE UN REGISTRO SI ES EDICION O LOS LIMPIA SI ES NUEVO REGISTRO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void callbackPanel_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                var kk = e.Parameter.Split('|');
                //EDICION
                if (kk[0] != "new") //PARAMETRO QUE INDICA QUE EL COMANDO NO ES NUEVO REGISTRO, SI NO EDICION
                {
                    lblMensaje.Visible = false;
                    int idddd = Convert.ToInt16(kk[0]);
                    btnUPDATE.Visible = true;
                    btnSave.Visible = false;
                    btnBAJA.Visible = false;
                    if (kk[1] == "baja")
                    {
                        txtIdEmb.Enabled = false;
                        FechaDia.Enabled = false;
                        btnBAJA.Visible = true;
                        btnUPDATE.Visible = false;
                    }
                    //////using (SqlConnection _conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EntitiesCRM"].ConnectionString))
                    //////{
                    //////    var cmd = new SqlCommand("SP_Configuraciones_Empresas", _conn);
                    //////    cmd.CommandType = CommandType.StoredProcedure;
                    //////    cmd.Parameters.AddWithValue("@id_Emp", idddd);
                    //////    cmd.Parameters.AddWithValue("@Accion", "SELECCIONAID");
                    //////    _conn.Open();
                    //////    SqlDataReader reader = cmd.ExecuteReader();
                    //////    while (reader.Read())
                    //////    {
                    //////        hdnIDCC.Value = reader["id_Emp"].ToString();
                    //////        txtIdEmb.Text = reader["Emp_Nombre"].ToString();
                    //////        txtDia.Text = reader["Emp_URL"].ToString();
                    //////    }
                    //////    reader.Close();
                    //////}
                }
                //NUEVO REGISTRO
                else
                {
                    clearcntrls(); //FUNCION QUE LIMPIA LOS CONTROLES 
                    btnUPDATE.Visible = false;
                    btnSave.Visible = true;
                    btnBAJA.Visible = false;
                }
            }
            catch (Exception)
            {
                lblMensaje.Visible = true;
                lblMensaje.Text = "Error al cargar el registro.";
            }
            finally
            {
                lblMensaje.Visible = true;
            }
        }

        private void clearcntrls()
        {
            FechaDia.Date = System.DateTime.Now;
            FechaDia.Date.AddDays(1);
            txtIdEmb.Text = string.Empty;
        }



        /// <summary>
        /// FUNCION QUE ACTUALIZA LOS REGISTROS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUPDATE_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection _conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EntitiesCRM"].ConnectionString))
                {
                    using (var cmd = new SqlCommand("SP_Configuraciones_Empresas", _conn))
                    {
                        cmd.Parameters.AddWithValue("@id_Emp", hdnIDCC.Value);
                        cmd.Parameters.AddWithValue("@Emp_Nombre", txtIdEmb.Text.Trim());
                        cmd.Parameters.AddWithValue("@Emp_URL", FechaDia.Value);
                        cmd.Parameters.AddWithValue("@Id_Usuario_Alta", "1");
                        cmd.Parameters.AddWithValue("@Accion", "ACTUALIZA");
                        cmd.CommandType = CommandType.StoredProcedure;
                        _conn.Open();
                        cmd.ExecuteNonQuery();
                        _conn.Close();
                        lblMensaje.Visible = true;
                        lblMensaje.Text = "Empresa Actualizada.";
                    }
                }
            }
            catch (Exception EX)
            {
                BootstrapAlert(lblMensaje, "Error al Actualizar Empresa.", BootstrapAlertType.Danger, true);
                pcrevisar.ShowOnPageLoad = true;

            }
            finally
            {
                pcrevisar.ShowOnPageLoad = false;
                //  LoadModulosActivos();
            }
        }


        /// <summary>
        /// FUNCION QUE ACTUALIZA LOS REGISTROS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBAJA_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection _conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EntitiesCRM"].ConnectionString))
                {
                    using (var cmd = new SqlCommand("SP_Configuraciones_Empresas", _conn))
                    {
                        cmd.Parameters.AddWithValue("@id_Emp", hdnIDCC.Value);
                        cmd.Parameters.AddWithValue("@Emp_Nombre", txtIdEmb.Text.Trim());
                        cmd.Parameters.AddWithValue("@Emp_URL", FechaDia.Text.Trim());
                        cmd.Parameters.AddWithValue("@Id_Usuario_Alta", "1");
                        cmd.Parameters.AddWithValue("@Accion", "BAJA");
                        cmd.CommandType = CommandType.StoredProcedure;
                        _conn.Open();
                        cmd.ExecuteNonQuery();
                        _conn.Close();
                        lblMensaje.Visible = true;
                        lblMensaje.Text = "Empresa baja.";
                    }
                }
            }
            catch (Exception EX)
            {
                lblMensaje.Visible = true;
                lblMensaje.Text = lblMensaje.Text + "Error al Actualizar Empresa.";
            }
            finally
            {
                pcrevisar.ShowOnPageLoad = false;

            }
        }
        /// <summary>
        /// FUNCION QUE GUARDA REGISTROS NUEVOS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                // ImprimirExcel();
                ImprimirExcelDriveIn();

            }
            catch (Exception)
            {
                BootstrapAlert(lblMensaje, "Error al generar el archivo de planner el Embarque.", BootstrapAlertType.Danger, true);
                pcrevisar.ShowOnPageLoad = true;

            }

        }

        /// <summary>
        /// CANCELA Y CIERRA LA EDICION O REGISTRO DE CC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.pcrevisar.ShowOnPageLoad = false;
            base.Response.Redirect("ProEmbarquesRutas.aspx");

        }


        /// <summary>
        /// FUNCION QUE GUARDA REGISTROS NUEVOS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGenerarPlan_Click(object sender, EventArgs e)
        {
            try
            {

                //ImprimirExcel();
                ImprimirExcelDriveIn();

            }
            catch (Exception)
            {
                BootstrapAlert(lblMensaje, "Error al generar archivo para planner.", BootstrapAlertType.Danger, true);
                pcrevisar.ShowOnPageLoad = true;
                this.pcrevisar.ShowOnPageLoad = true;
                this.btnGenerarPlan.Enabled = true;
            }
            this.pcExportar.ShowOnPageLoad = false;
            this.pcrevisar.ShowOnPageLoad = false;
            this.btnGenerarPlan.Enabled = true;

        }




        /// <summary>
        /// CANCELA Y CIERRA LA EXPORTACION DE DOCUMENTO EXCEL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelarExp_Click(object sender, EventArgs e)
        {
            this.pcExportar.ShowOnPageLoad = false;
            this.pcrevisar.ShowOnPageLoad = false;
            this.btnGenerarPlan.Enabled = true;
            this.btnGrabar.Enabled = true;
            base.Response.Redirect("ProEmbarquesRutas.aspx");

        }


    }
}