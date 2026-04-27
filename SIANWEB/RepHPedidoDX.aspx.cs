using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Drawing;

using CapaDatos;
using CapaNegocios;
using CapaEntidad;

using Telerik.Web.UI;

using DevExpress.Web.ASPxPivotGrid;
using DevExpress.XtraPivotGrid;

using DevExpress.XtraPrinting;
using DevExpress.Web;
using DevExpress.Export;
using DevExpress.Utils;

using System.IO;

using Syncfusion.XlsIO;
using ClosedXML.Excel;
using System.Globalization;
using System.Threading;

namespace SIANWEB
{
    public partial class RepHPedidoDX : System.Web.UI.Page
    {
        public class DetalleRenglonTrackingPedido
        {
            public int Id_Emp { get; set; }
            public int Id_Cd { get; set; }
            public int IdCte { get; set; }
            public string NomCte { get; set; }
            public string RSC { get; set; }
            public int Id_Ped { get; set; }
            public int IdProducto { get; set; }
            public string Producto { get; set; }
            public string FechaCaptura { get; set; }
            public int CantidadCaptura { get; set; }
            public float MontoCaptura{ get; set; }
            public string FechaCompromiso { get; set; }
            public int CantidadCompromiso { get; set; }
            public float MontoCompromiso { get; set; }
            public string FechaAsignacion { get; set; }
            public int CantidadAsignacion { get; set; }
            public float MontoAsignacion { get; set; }
            public string FechaPicking { get; set; }
            public int CantidadPicking { get; set; }
            public float MontoPicking { get; set; }
            public string FechaRemision { get; set; }
            public int CantidadRemision { get; set; }
            public float MontoRemision { get; set; }
            public string FechaFactura { get; set; }
            public int CantidadFactura { get; set; }
            public float MontoFactura { get; set; }
            public string FechaEmbarque { get; set; }
            public int CantidadEmbarque { get; set; }
            public float MontoEmbarque { get; set; }
            public string FechaEntrega { get; set; }
            public int CantidadEntrega { get; set; }
            public float MontoEntrega { get; set; }
            public string FechaCobranza { get; set; }
            public int CantidadCobranza { get; set; }
            public float MontoCobranza { get; set; }
        }

        #region Variables

        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        
        #endregion

        public string strPedTot = "";
        public string strPedCom = "";
        public string strPedInc = "";
        public string strPedComMonto = "";
        public string strPedIncMonto = "";

        public string strParTot = "";
        public string strParCom = "";
        public string strParInc = "";
        public string strParComMonto = "";
        public string strParIncMonto = "";
        
        public string strPedComPerce = "";
        public string strPedIncPerce = "";
        public string strParComPerce = "";
        public string strParIncPerce = "";

        public string strPedConPicking = "";
        public string strPedSinPicking = "";
        public string strPedCPickPorcen = "";
        public string strPedSPickPorcen = "";
        public string strPedConPickingMonto = "";
        public string strPedSinPickingMonto = "";

        public string strSelectoComando = "";
        #region Metodos

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
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        CargarCentros();

                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();

                        this.rdFechaInicial.DbSelectedDate = Sesion.CalendarioIni;
                        this.rdFechaFinal.DbSelectedDate = Sesion.CalendarioFin;

                        string Seeeleeeec = "";
                        GeneraCadena(ref Seeeleeeec);
                        strSelectoComando = Seeeleeeec;
                        //this.PivotDataSource.ConnectionString = Sesion.Emp_Cnx;
                        this.PivotDataSource.SelectCommandType = SqlDataSourceCommandType.Text;
                        this.PivotDataSource.SelectCommand = Seeeleeeec;    // "spHistorialPedidos2";

                        GeneraResumen(Seeeleeeec);
                    }
                    else
                    {
                        //  this.PivotDataSource.ConnectionString = "ConnectionStrings:sianwebmtyConnectionString";
                        this.CargaTabla();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        

        protected void cmbCentrosDist_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }

        #endregion

        #region Eventos

        private void GeneraResumen(string seleelect)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            DateTime FechaI = Convert.ToDateTime(this.rdFechaInicial.SelectedDate.ToString());
            string FechaIstr = "";
            FechaIstr = FechaI.Year.ToString();
            if (FechaI.Month >= 10)
            {
                FechaIstr = FechaIstr + FechaI.Month.ToString();
            }
            else
            {
                FechaIstr = FechaIstr + "0" + FechaI.Month.ToString();
            }
            if (FechaI.Day >= 10)
            {
                FechaIstr = FechaIstr + FechaI.Day.ToString();
            }
            else
            {
                FechaIstr = FechaIstr + "0" + FechaI.Day.ToString();
            }

            DateTime FechaF = Convert.ToDateTime(this.rdFechaFinal.SelectedDate.ToString());
            string FechaFstr = "";
            FechaFstr =  FechaF.Year.ToString();
            if (FechaF.Month >= 10)
            {
                FechaFstr = FechaFstr + FechaF.Month.ToString();
            }
            else
            {
                FechaFstr = FechaFstr + "0" + FechaF.Month.ToString();
            }
            if (FechaF.Day >= 10)
            {
                FechaFstr = FechaFstr + FechaF.Day.ToString();
            }
            else
            {
                FechaFstr = FechaFstr + "0" + FechaF.Day.ToString();
            }

            string Pedidos = this.txtLPdidos.Text.Trim();
            int filtro = Convert.ToInt32(cmbEstatusPedido.SelectedValue);

                FechaIstr = FechaI.ToShortDateString();
                FechaFstr = FechaF.ToShortDateString();
            try
            {
                SqlDataReader dr = null;
                long verificador = 0;
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Sesion.Emp_Cnx);
                string[] Parametros = { 
                                        "@FechaIni", 
                                        "@FechaFin", 
                                        "@strPedido",
                                        "@iEstatus"
                                      };
                object[] Valores = new object[] { 
                                        FechaIstr,
                                        FechaFstr,
                                        Pedidos,
                                        filtro

                                        };

                //  SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spHistorialPedidos3", ref dr, Parametros, Valores);
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spHistorialPedidos3vPk", ref dr, Parametros, Valores);
                while (dr.Read())
                {

                    strPedTot = Convert.ToString(dr.GetValue(dr.GetOrdinal("PedTotales")));
                    strPedCom = Convert.ToString(dr.GetValue(dr.GetOrdinal("PedCompletas")));
                    strPedInc = Convert.ToString(dr.GetValue(dr.GetOrdinal("PedIncompletas")));

                    strParTot = Convert.ToString(dr.GetValue(dr.GetOrdinal("ParTotales")));
                    strParCom = Convert.ToString(dr.GetValue(dr.GetOrdinal("ParCompletas")));
                    strParInc = Convert.ToString(dr.GetValue(dr.GetOrdinal("ParIncompletas")));

                    strPedComMonto = Convert.ToString(dr.GetValue(dr.GetOrdinal("PedComMonto")));
                    strPedIncMonto = Convert.ToString(dr.GetValue(dr.GetOrdinal("PedIncMonto")));
                    strParComMonto = Convert.ToString(dr.GetValue(dr.GetOrdinal("ParComMonto")));
                    strParIncMonto = Convert.ToString(dr.GetValue(dr.GetOrdinal("ParIncMonto")));

                    strPedComPerce = Convert.ToString(dr.GetValue(dr.GetOrdinal("PedComPorcen")));
                    strPedIncPerce = Convert.ToString(dr.GetValue(dr.GetOrdinal("PedIncPorcen")));
                    strParComPerce = Convert.ToString(dr.GetValue(dr.GetOrdinal("ParComPorcen")));
                    strParIncPerce = Convert.ToString(dr.GetValue(dr.GetOrdinal("ParIncPorcen")));

                    strPedConPicking = Convert.ToString(dr.GetValue(dr.GetOrdinal("PedConPicking")));
                    strPedSinPicking = Convert.ToString(dr.GetValue(dr.GetOrdinal("PedSinPicking")));
                    strPedCPickPorcen = Convert.ToString(dr.GetValue(dr.GetOrdinal("PedCPickPorcen")));
                    strPedSPickPorcen = Convert.ToString(dr.GetValue(dr.GetOrdinal("PedSPickPorcen")));

                    strPedConPickingMonto = Convert.ToString(dr.GetValue(dr.GetOrdinal("PedConPickingMonto")));
                    strPedSinPickingMonto = Convert.ToString(dr.GetValue(dr.GetOrdinal("PedSinPickingMonto")));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        private void GeneraCadena(ref string Seeelect)
        {
            string Pedidos = this.txtLPdidos.Text;

            //  Seeelect = "Exec spHistorialPedidos2 ";
            Seeelect = "Exec spHistorialPedidos2vPk ";

            if ((rdFechaInicial.ValidationDate != "") && (rdFechaFinal.ValidationDate != ""))
            {

                DateTime FechaI = Convert.ToDateTime(this.rdFechaInicial.SelectedDate.ToString());
                Seeelect = Seeelect + "'" + FechaI.Year.ToString();
                if (FechaI.Month >= 10)
                {
                    Seeelect = Seeelect + FechaI.Month.ToString();
                }
                else
                {
                    Seeelect = Seeelect + "0" + FechaI.Month.ToString();
                }
                if (FechaI.Day >= 10)
                {
                    Seeelect = Seeelect + FechaI.Day.ToString() + "'";
                }
                else
                {
                    Seeelect = Seeelect + "0" + FechaI.Day.ToString() + "'";
                }


                DateTime FechaF = Convert.ToDateTime(this.rdFechaFinal.SelectedDate.ToString());

                Seeelect = Seeelect + ", '" + FechaF.Year.ToString();
                if (FechaF.Month >= 10)
                {
                    Seeelect = Seeelect + FechaF.Month.ToString();
                }
                else
                {
                    Seeelect = Seeelect + "0" + FechaF.Month.ToString();
                }
                if (FechaF.Day >= 10)
                {
                    Seeelect = Seeelect + FechaF.Day.ToString();
                }
                else
                {
                    Seeelect = Seeelect + "0" + FechaF.Day.ToString();
                }

                if (Pedidos == "")
                {

                    Seeelect = Seeelect + "', Null";
                }
                else
                {
                    Seeelect = Seeelect + "', '" + Pedidos + "'";
                }
            }
            else
            {
                Seeelect = Seeelect + "Null, Null, ";

                if (Pedidos == "")
                {

                    Seeelect = Seeelect + " Null";
                }
                else
                {
                    Seeelect = Seeelect + " '" + Pedidos + "'";
                }
            }

            if (cmbEstatusPedido.SelectedValue == "0")
            {
                Seeelect = Seeelect + ", 0"; 
            }
            else
            {
                Seeelect = Seeelect + ", " + cmbEstatusPedido.SelectedValue;
            }


        }

        protected void buttonSaveAs_Click(object sender, EventArgs e)
        {
            try
            {
                ASPxPivotGridExporter1.OptionsPrint.MergeColumnFieldValues = true;
                ASPxPivotGridExporter1.OptionsPrint.MergeRowFieldValues = true;

                const string fileName = "PivotGrid";
                XlsxExportOptionsEx options;
                options = new XlsxExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG };
                ASPxPivotGridExporter1.ExportXlsxToResponse(fileName, options);
                /*
                        options = new XlsxExportOptionsEx()
                        {
                            ExportType = ExportType.DataAware,
                            AllowGrouping = allowGroupingCheckBox.Checked ? DefaultBoolean.True : DefaultBoolean.False,
                            TextExportMode = exportCellValuesAsText.Checked ? TextExportMode.Text : TextExportMode.Value,
                            AllowFixedColumns = allowFixedColumns.Checked ? DefaultBoolean.True : DefaultBoolean.False,
                            AllowFixedColumnHeaderPanel = allowFixedColumns.Checked ? DefaultBoolean.True : DefaultBoolean.False,
                            RawDataMode = exportRawData.Checked
                        };
                        ASPxPivotGridExporter1.ExportXlsxToResponse(fileName, options);
                */
            }
            catch (Exception ex)
            {
                this.lblMensaje.Text = ex.Message;
                throw ex;
            }
        }

        #endregion

        #region Funciones

        private void CargaTabla()
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                //List<RptPivotPedido> listflujos = new List<RptPivotPedido>();
                //  CN_CatCriterioCitas clsCatCriterioCitas = new CN_CatCriterioCitas();
                //  string Pedidos = this.txtLPdidos.Text;
                string Seeele1ct = "";
                //  clsCatCriterioCitas.ReporteHistorialPedido(FechaI, FechaF, Pedidos, session.Emp_Cnx, ref listflujos);

                GeneraCadena(ref Seeele1ct);
                //  this.PivotDataSource.ConnectionString = session.Emp_Cnx;
                //  Seeelect = "Exec spHistorialPedidos2";
                /*
                if ((rdFechaInicial.ValidationDate != "") && (rdFechaFinal.ValidationDate != ""))
                {

                    DateTime FechaI = Convert.ToDateTime(this.rdFechaInicial.SelectedDate.ToString());
                    Seeelect = Seeelect + "'" + FechaI.Year.ToString();
                    if (FechaI.Month >= 10)
                    {
                        Seeelect = Seeelect + FechaI.Month.ToString();
                    }
                    else
                    {
                        Seeelect = Seeelect + "0" + FechaI.Month.ToString();
                    }
                    if (FechaI.Day >= 10)
                    {
                        Seeelect = Seeelect + FechaI.Day.ToString() + "'";
                    }
                    else
                    {
                        Seeelect = Seeelect + "0" + FechaI.Day.ToString() + "'";
                    }


                    DateTime FechaF = Convert.ToDateTime(this.rdFechaFinal.SelectedDate.ToString());

                    Seeelect = Seeelect + ", '" + FechaF.Year.ToString();
                    if (FechaF.Month >= 10)
                    {
                        Seeelect = Seeelect + FechaF.Month.ToString();
                    }
                    else
                    {
                        Seeelect = Seeelect + "0" + FechaF.Month.ToString();
                    }
                    if (FechaF.Day >= 10)
                    {
                        Seeelect = Seeelect + FechaF.Day.ToString();
                    }
                    else
                    {
                        Seeelect = Seeelect + "0" + FechaF.Day.ToString();
                    }

                    if (Pedidos == "")
                    {

                        Seeelect = Seeelect + "', Null";
                    }
                    else
                    {
                        Seeelect = Seeelect + "', '" + Pedidos + "'";
                    }
                }
                else
                {
                    Seeelect = Seeelect + "Null, Null, ";
                    
                    if (Pedidos == "")
                    {

                        Seeelect = Seeelect + " Null";
                    }
                    else
                    {
                        Seeelect = Seeelect + " '" + Pedidos + "'";
                    }
                }

                if (cmbEstatusPedido.SelectedValue == "0")
                {
                    Seeelect = Seeelect + ", " + cmbEstatusPedido.SelectedValued;
                }
                else
                {
                    Seeelect = Seeelect + ", Null";
                }

                  */  

                this.PivotDataSource.SelectCommandType = SqlDataSourceCommandType.Text;
                this.PivotDataSource.SelectCommand = Seeele1ct;

                /// ASPxPivotGrid1.DataBind();

                //  this.PivotDataSource.DataBind();
                //  this.PivotDataSource.SelectParameters["FechaIni"].DefaultValue = this.rdFechaInicial.SelectedDate.ToString();
                //  this.PivotDataSource.SelectParameters["FechaFin"].DefaultValue = this.rdFechaFinal.SelectedDate.ToString();
                //  this.PivotDataSource.SelectParameters["FechaFin"].DefaultValue = Pedidos;

                //  this.PivotDataSource.SelectParameters

                //  ASPxPivotGrid1.ReloadData();
                //  if (ASPxPivotGrid1.Fields.Count != 0)
                //  return;

                // Creates pivot grid fields for all data source fields.
                //  ASPxPivotGrid1.RetrieveFields();

                // Locates the pivot grid fields in appropriate areas.
                /*
                ASPxPivotGrid1.Fields["NomCte"].Area = PivotArea.RowArea;
                ASPxPivotGrid1.Fields["IdPedido"].Area = PivotArea.RowArea;
                ASPxPivotGrid1.Fields["TipoFecha"].Area = PivotArea.ColumnArea;
                ASPxPivotGrid1.Fields["Fecha"].Area = PivotArea.DataArea;
                ASPxPivotGrid1.Fields["Fecha"].ValueFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                ASPxPivotGrid1.Fields["Fecha"].ValueFormat.FormatString= "D";
                */

                //  ASPxPivotGrid1.Fields["Fecha"].SummaryType = PivotGridFieldBase.DefaultDateFormat;
                //  ASPxPivotGrid1.Fields["Fecha"].GroupInterval = PivotGroupInterval.DateYear;
                GeneraResumen(Seeele1ct);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnActualiza_Click(object sender, EventArgs e)
        {
            CargaTabla();
        }


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
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                else
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;

                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, Sesion.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;
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
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
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

        protected void btnExportarExcelDirecto_Click(object sender, EventArgs e)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            DateTime FechaI = Convert.ToDateTime(this.rdFechaInicial.SelectedDate.ToString());
            string FechaIstr = "";
            FechaIstr = FechaI.Year.ToString();
            if (FechaI.Month >= 10)
            {
                FechaIstr = FechaIstr + FechaI.Month.ToString();
            }
            else
            {
                FechaIstr = FechaIstr + "0" + FechaI.Month.ToString();
            }
            if (FechaI.Day >= 10)
            {
                FechaIstr = FechaIstr + FechaI.Day.ToString();
            }
            else
            {
                FechaIstr = FechaIstr + "0" + FechaI.Day.ToString();
            }

            DateTime FechaF = Convert.ToDateTime(this.rdFechaFinal.SelectedDate.ToString());
            string FechaFstr = "";
            FechaFstr = FechaF.Year.ToString();
            if (FechaF.Month >= 10)
            {
                FechaFstr = FechaFstr + FechaF.Month.ToString();
            }
            else
            {
                FechaFstr = FechaFstr + "0" + FechaF.Month.ToString();
            }
            if (FechaF.Day >= 10)
            {
                FechaFstr = FechaFstr + FechaF.Day.ToString();
            }
            else
            {
                FechaFstr = FechaFstr + "0" + FechaF.Day.ToString();
            }

            string Pedidos = this.txtLPdidos.Text.Trim();
            int filtro = Convert.ToInt32(cmbEstatusPedido.SelectedValue);

            /// string filenombre = "ReporteSeguimientoPedidos_" + FechaI.ToShortDateString().Replace("/", "") + "h" + FechaI.ToShortTimeString().Replace(".", "").Replace(":", "").Replace(" ", "") + "__" + FechaF.ToShortDateString().Replace("/", "") + "h" + FechaF.ToShortTimeString().Replace(".", "").Replace(":", "").Replace(" ", "");

            string url = "WebForm2.aspx?sFechaI=" + FechaIstr + "&sFechaF=" + FechaFstr + "&sPedidos=" + Pedidos + "&sFiltro=" + filtro;
            /// + "&sfilename=" + filenombre;
            string s = "window.open('" + url + "', 'popup_window', 'width=900,height=800,left=100,top=50,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

            /*
            bool endRequest = false;
            try
            {
                SqlDataReader dr = null;
                DataSet ds = new DataSet();
                
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Sesion.Emp_Cnx);
                string[] Parametros = {
                                        "@FechaIni",
                                        "@FechaFin",
                                        "@strPedido",
                                        "@iEstatus",
                                        "@sCliente",
                                      };
                object[] Valores = new object[] {
                                        FechaIstr,
                                        FechaFstr,
                                        Pedidos,
                                        filtro,
                                        null,
                                        };

                /// SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spHistorialPedidos2vPkFlat", ref dr, Parametros, Valores);
                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spHistorialPedidos2vPkFlat", ref ds, Parametros, Valores);

                DetalleRenglonTrackingPedido DetaRng = new DetalleRenglonTrackingPedido();
                List<DetalleRenglonTrackingPedido> lstDetRpt = new List<DetalleRenglonTrackingPedido>(); 

                ds.Tables[0].TableName = "SeguimientoPedidos";

                / *
                while (dr.Read())
                {
                    DetaRng = new DetalleRenglonTrackingPedido();

                    DetaRng.Id_Emp = int.Parse(dr["Id_Emp"].ToString());
                    DetaRng.Id_Cd = int.Parse(dr["Id_Cd"].ToString());
                    DetaRng.IdCte = int.Parse(dr["IdCte"].ToString());
                    DetaRng.NomCte = dr["NomCte"].ToString().Trim();
                    DetaRng.RSC = dr["RSC"].ToString().Trim();
                    DetaRng.Id_Ped = int.Parse(dr["Id_Ped"].ToString());
                    DetaRng.IdProducto = int.Parse(dr["IdProducto"].ToString());
                    DetaRng.Producto = dr["Producto"].ToString().Trim();
                    DetaRng.FechaCaptura = dr["FechaCaptura"].ToString();   ///  DateTime.Parse(dr["FechaCaptura"].ToString());
                    DetaRng.CantidadCaptura = int.Parse(dr["Cantidad Captura"].ToString());
                    DetaRng.MontoCaptura = float.Parse(dr["Monto Captura"].ToString());

                    DetaRng.FechaCompromiso = dr["Fecha Compromiso"].ToString(); 
                    ///  (string.IsNullOrEmpty(DateTime.Parse(dr["Fecha Compromiso"].ToString(), new CultureInfo("es-MX")).ToString()) ? "" : DateTime.Parse(dr["Fecha Compromiso"].ToString(), new CultureInfo("es-MX")).ToString() );
                    DetaRng.CantidadCompromiso = int.Parse(dr["Cantidad Compromiso"].ToString());
                    DetaRng.MontoCompromiso = float.Parse(dr["Monto Compromiso"].ToString());

                    DetaRng.FechaAsignacion = dr["Fecha Asignacion"].ToString(); 
                    /// (string.IsNullOrEmpty(DateTime.Parse(dr["Fecha Asignacion"].ToString(), new CultureInfo("es-MX")).ToString()) ? "" : DateTime.Parse(dr["Fecha Asignacion"].ToString(), new CultureInfo("es-MX")).ToString() );                     
                    DetaRng.CantidadAsignacion = int.Parse(dr["Cantidad Asignacion"].ToString());
                    DetaRng.MontoAsignacion = float.Parse(dr["Monto Asignacion"].ToString());

                    DetaRng.FechaPicking = dr["Fecha Picking"].ToString(); ///   (string.IsNullOrEmpty(DateTime.Parse(dr["Fecha Picking"].ToString(), new CultureInfo("es-MX")).ToString()) ? "" : DateTime.Parse(dr["Fecha Picking"].ToString(), new CultureInfo("es-MX")).ToString() );                     
                    DetaRng.CantidadPicking = int.Parse(dr["Cantidad Picking"].ToString());
                    DetaRng.MontoPicking = float.Parse(dr["Monto Picking"].ToString());

                    DetaRng.FechaRemision = dr["Fecha Remision"].ToString();    ///  (string.IsNullOrEmpty(DateTime.Parse(dr["Fecha Remision"].ToString(), new CultureInfo("es-MX")).ToString()) ? "" : DateTime.Parse(dr["Fecha Remision"].ToString(), new CultureInfo("es-MX")).ToString() );
                    DetaRng.CantidadRemision = int.Parse(dr["Cantidad Remision"].ToString());
                    DetaRng.MontoRemision = float.Parse(dr["Monto Remision"].ToString());

                    DetaRng.FechaFactura = dr["Fecha Factura"].ToString();      ///    (string.IsNullOrEmpty(DateTime.Parse(dr["Fecha Factura"].ToString(), new CultureInfo("es-MX")).ToString()) ? "" : DateTime.Parse(dr["Fecha Factura"].ToString(), new CultureInfo("es-MX")).ToString() ); 
                    DetaRng.CantidadFactura = int.Parse(dr["Cantidad Factura"].ToString());
                    DetaRng.MontoFactura = float.Parse(dr["Monto Factura"].ToString());

                    DetaRng.FechaEmbarque = dr["Fecha Embarque"].ToString();    ///     (string.IsNullOrEmpty(DateTime.Parse(dr["Fecha Embarque"].ToString(), new CultureInfo("es-MX")).ToString()) ? "" : DateTime.Parse(dr["Fecha Embarque"].ToString(), new CultureInfo("es-MX")).ToString() );
                    DetaRng.CantidadEmbarque = int.Parse(dr["Cantidad Embarque"].ToString());
                    DetaRng.MontoEmbarque = float.Parse(dr["Monto Embarque"].ToString());

                    DetaRng.FechaEntrega = dr["Fecha Entrega"].ToString();      ///      (string.IsNullOrEmpty(DateTime.Parse(dr["Fecha Entrega"].ToString(), new CultureInfo("es-MX")).ToString()) ? "" : DateTime.Parse(dr["Fecha Entrega"].ToString(), new CultureInfo("es-MX")).ToString() );
                    DetaRng.CantidadEntrega = int.Parse(dr["Cantidad Entrega"].ToString());
                    DetaRng.MontoEntrega = float.Parse(dr["Monto Entrega"].ToString());

                    DetaRng.FechaCobranza = dr["Fecha Cobranza"].ToString();        ///  (string.IsNullOrEmpty(DateTime.Parse(dr["Fecha Cobranza"].ToString(), new CultureInfo("es-MX")).ToString()) ? "" : DateTime.Parse(dr["Fecha Cobranza"].ToString(), new CultureInfo("es-MX")).ToString() );
                    DetaRng.CantidadCobranza = int.Parse(dr["Cantidad Cobranza"].ToString());
                    DetaRng.MontoCobranza = float.Parse(dr["Monto Cobranza"].ToString());
                    
                    lstDetRpt.Add(DetaRng);
                }
                * /

                #region NvoProcesoExcel
                / *
                XLWorkbook wb = new XLWorkbook();
                var ws3 = wb.Worksheets.Add("Seguimiento de Pedidos");
                RptPedidosNoAgrupado(lstDetRpt, FechaI, FechaF, ref ws3);
                AjustaHoja(ref ws3);

                
                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + filenombre + ".xlsx");
                
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    /// MyMemoryStream.WriteTo(Response.OutputStream);
                    //      Response.Flush();
                    ///     Response.End();
                    try
                    {
                        //Write HTTP output
                        HttpContext.Current.Response.Write(Response.OutputStream);
                    }
                    catch (Exception exc) { }
                    finally
                    {
                        try
                        {
                            //stop processing the script and return the current result
                            HttpContext.Current.Response.End();
                        }
                        catch (Exception ex) { }
                        finally
                        {
                            //Sends the response buffer
                            HttpContext.Current.Response.Flush();
                            // Prevents any other content from being sent to the browser
                            HttpContext.Current.Response.SuppressContent = true;
                            //Directs the thread to finish, bypassing additional processing
                            HttpContext.Current.ApplicationInstance.CompleteRequest();
                            //Suspends the current thread
                            Thread.Sleep(1);
                        }
                    }

                }
                * /
                #endregion


                /// Exportaaa(wb, ds, filenombre, 1);
                
                using (XLWorkbook wb = new XLWorkbook())
                {

                    foreach (DataTable dt in ds.Tables)
                    {
                        wb.Worksheets.Add(dt);
                    }

                        ///     HttpContext.Current.ApplicationInstance.CompleteRequest();

                    //Export the Excel file.
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=" + filenombre + ".xlsx");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        //  Response.Flush();
                        //  HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                        //  HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                        //  HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
                        //  Response.End();
                        endRequest = true;
                    }

                }
                

            }
            catch (Exception ex)
            {
                this.lblMensaje.Text = ex.Message;
                throw ex;
            }
            finally
            {
                if (endRequest)
                    Response.End();
            }
            */
        }

        void RptPedidosNoAgrupado(List<DetalleRenglonTrackingPedido> listaDetalle, DateTime FecInicial, DateTime FecFinal, ref IXLWorksheet wssz)
        {
            try
            {
                /// Encabezado Superior
                wssz.Range("A2:F2").Merge();
                wssz.Range("A2").SetValue("Reporte Traking de Pedidos");
                wssz.Range("A2:F2").Style.Font.Bold = true;
                wssz.Range("A2:F2").Style.Font.FontSize = 16;

                wssz.Range("H2:I2").Merge();
                wssz.Range("H2").SetValue("del " + FecInicial.ToString("dd/MM/yyyy") + " al " + FecFinal.ToString("dd/MM/yyyy"));

                ///     Encabezado Tabla

                wssz.Range("A4:A5").Merge();
                CeldaHeader("A4:A5", "ID EMP", ref wssz);
                wssz.Range("A4:A5").Style.Alignment.WrapText = true;

                wssz.Range("B4:B5").Merge();
                CeldaHeader("B4:B5", "ID CD", ref wssz);
                wssz.Range("B4:B5").Style.Alignment.WrapText = true;

                wssz.Range("C4:C5").Merge();
                CeldaHeader("C4:C5", "ID CTE", ref wssz);
                wssz.Range("C4:C5").Style.Alignment.WrapText = true;

                wssz.Range("D4:D5").Merge();
                CeldaHeader("D4:D5", "CLIENTE", ref wssz);
                wssz.Range("D4:D5").Style.Alignment.WrapText = true;

                wssz.Range("E4:E5").Merge();
                CeldaHeader("E4:E5", "RSC", ref wssz);
                wssz.Range("E4:E5").Style.Alignment.WrapText = true;

                wssz.Range("F4:F5").Merge();
                CeldaHeader("F4:F5", "PEDIDO", ref wssz);
                wssz.Range("F4:F5").Style.Alignment.WrapText = true;

                wssz.Range("G4:G5").Merge();
                CeldaHeader("G4:G5", "ID PRODUCTO", ref wssz);
                wssz.Range("G4:G5").Style.Alignment.WrapText = true;

                wssz.Range("H4:H5").Merge();
                CeldaHeader("H4:H5", "PRODUCTO", ref wssz);
                wssz.Range("H4:H5").Style.Alignment.WrapText = true;

                wssz.Range("I4:I5").Merge();
                CeldaHeader("I4:I5", "FECHA CAPTURA", ref wssz);
                wssz.Range("I4:I5").Style.Alignment.WrapText = true;

                wssz.Range("J4:J5").Merge();
                CeldaHeader("J4:J5", "CANTIDAD CAPTURA", ref wssz);
                wssz.Range("J4:J5").Style.Alignment.WrapText = true;

                wssz.Range("K4:K5").Merge();
                CeldaHeader("K4:K5", "MONTO CAPTURA", ref wssz);
                wssz.Range("K4:K5").Style.Alignment.WrapText = true;

                wssz.Range("L4:L5").Merge();
                CeldaHeader("L4:L5", "FECHA COMPROMISO", ref wssz);
                wssz.Range("L4:L5").Style.Alignment.WrapText = true;

                wssz.Range("M4:M5").Merge();
                CeldaHeader("M4:M5", "CANTIDAD COMPROMISO", ref wssz);
                wssz.Range("M4:M5").Style.Alignment.WrapText = true;

                wssz.Range("N4:N5").Merge();
                CeldaHeader("N4:N5", "MONTO COMPROMISO", ref wssz);
                wssz.Range("N4:N5").Style.Alignment.WrapText = true;

                wssz.Range("O4:O5").Merge();
                CeldaHeader("O4:O5", "FECHA ASIGNACION", ref wssz);
                wssz.Range("O4:O5").Style.Alignment.WrapText = true;

                wssz.Range("P4:P5").Merge();
                CeldaHeader("P4:P5", "CANTIDAD ASIGNACION", ref wssz);
                wssz.Range("P4:P5").Style.Alignment.WrapText = true;

                wssz.Range("Q4:Q5").Merge();
                CeldaHeader("Q4:Q5", "MONTO ASIGNACION", ref wssz);
                wssz.Range("Q4:Q5").Style.Alignment.WrapText = true;

                wssz.Range("R4:R5").Merge();
                CeldaHeader("R4:R5", "FECHA PICKING", ref wssz);
                wssz.Range("R4:R5").Style.Alignment.WrapText = true;

                wssz.Range("S4:S5").Merge();
                CeldaHeader("S4:S5", "CANTIDAD PICKING", ref wssz);
                wssz.Range("S4:S5").Style.Alignment.WrapText = true;

                wssz.Range("T4:T5").Merge();
                CeldaHeader("T4:T5", "MONTO PICKING", ref wssz);
                wssz.Range("T4:T5").Style.Alignment.WrapText = true;

                wssz.Range("U4:U5").Merge();
                CeldaHeader("U4:U5", "FECHA REMISION", ref wssz);
                wssz.Range("U4:U5").Style.Alignment.WrapText = true;

                wssz.Range("V4:V5").Merge();
                CeldaHeader("V4:V5", "CANTIDAD REMISION", ref wssz);
                wssz.Range("V4:V5").Style.Alignment.WrapText = true;

                wssz.Range("W4:W5").Merge();
                CeldaHeader("W4:W5", "MONTO REMISION", ref wssz);
                wssz.Range("W4:W5").Style.Alignment.WrapText = true;

                wssz.Range("X4:X5").Merge();
                CeldaHeader("X4:X5", "FECHA FACTURA", ref wssz);
                wssz.Range("X4:X5").Style.Alignment.WrapText = true;

                wssz.Range("Y4:Y5").Merge();
                CeldaHeader("Y4:Y5", "CANTIDAD FACTURA", ref wssz);
                wssz.Range("Y4:Y5").Style.Alignment.WrapText = true;

                wssz.Range("Z4:Z5").Merge();
                CeldaHeader("Z4:Z5", "MONTO FACTURA", ref wssz);
                wssz.Range("Z4:Z5").Style.Alignment.WrapText = true;

                wssz.Range("AA4:AA5").Merge();
                CeldaHeader("AA4:AA5", "FECHA EMBARQUE", ref wssz);
                wssz.Range("AA4:AA5").Style.Alignment.WrapText = true;

                wssz.Range("AB4:AB5").Merge();
                CeldaHeader("AB4:AB5", "CANTIDAD EMBARQUE", ref wssz);
                wssz.Range("AB4:AB5").Style.Alignment.WrapText = true;

                wssz.Range("AC4:AC5").Merge();
                CeldaHeader("AC4:AC5", "MONTO EMBARQUE", ref wssz);
                wssz.Range("AC4:AC5").Style.Alignment.WrapText = true;

                wssz.Range("AD4:AD5").Merge();
                CeldaHeader("AD4:AD5", "FECHA ENTREGA", ref wssz);
                wssz.Range("AD4:AD5").Style.Alignment.WrapText = true;

                wssz.Range("AE4:AE5").Merge();
                CeldaHeader("AE4:AE5", "CANTIDAD ENTREGA", ref wssz);
                wssz.Range("AE4:AE5").Style.Alignment.WrapText = true;

                wssz.Range("AF4:AF5").Merge();
                CeldaHeader("AF4:AF5", "MONTO ENTREGA", ref wssz);
                wssz.Range("AF4:AF5").Style.Alignment.WrapText = true;

                wssz.Range("AG4:AG5").Merge();
                CeldaHeader("AG4:AG5", "FECHA COBRANZA", ref wssz);
                wssz.Range("AG4:AG5").Style.Alignment.WrapText = true;

                wssz.Range("AH4:AH5").Merge();
                CeldaHeader("AH4:AH5", "CANTIDAD COBRANZA", ref wssz);
                wssz.Range("AH4:AH5").Style.Alignment.WrapText = true;

                wssz.Range("AI4:AI5").Merge();
                CeldaHeader("AI4:AI5", "MONTO COBRANZA", ref wssz);
                wssz.Range("AI4:AI7").Style.Alignment.WrapText = true;

                /*
                int AquiVa = 8;
                foreach (DetalleRenglonTrackingPedido E in listaDetalle)
                {
                    CeldaDatos("A" + AquiVa.ToString(), E.Id_Emp.ToString(), XLAlignmentHorizontalValues.Center, ref wssz);
                    CeldaDatos("B" + AquiVa.ToString(), E.Id_Cd.ToString(), XLAlignmentHorizontalValues.Center, ref wssz);
                    CeldaDatos("C" + AquiVa.ToString(), E.IdCte.ToString(), XLAlignmentHorizontalValues.Center, ref wssz);
                    CeldaDatos("D" + AquiVa.ToString(), E.NomCte, XLAlignmentHorizontalValues.Left, ref wssz);
                    CeldaDatos("E" + AquiVa.ToString(), E.RSC, XLAlignmentHorizontalValues.Left, ref wssz);
                    CeldaDatos("F" + AquiVa.ToString(), E.Id_Ped.ToString(), XLAlignmentHorizontalValues.Center, ref wssz);
                    CeldaDatos("G" + AquiVa.ToString(), E.IdProducto.ToString(), XLAlignmentHorizontalValues.Center, ref wssz);
                    CeldaDatos("H" + AquiVa.ToString(), E.Producto, XLAlignmentHorizontalValues.Left, ref wssz);

                    CeldaDatos("I" + AquiVa.ToString(), E.FechaCaptura.ToString(), XLAlignmentHorizontalValues.Center, ref wssz);
                    CeldaDatos("J" + AquiVa.ToString(), E.CantidadCaptura.ToString(), XLAlignmentHorizontalValues.Left, ref wssz);
                    CeldaDatos("K" + AquiVa.ToString(), E.MontoCaptura.ToString(), XLAlignmentHorizontalValues.Right, ref wssz);

                    CeldaDatos("L" + AquiVa.ToString(), E.FechaCompromiso.ToString(), XLAlignmentHorizontalValues.Center, ref wssz);
                    CeldaDatos("M" + AquiVa.ToString(), E.CantidadCompromiso.ToString(), XLAlignmentHorizontalValues.Left, ref wssz);
                    CeldaDatos("N" + AquiVa.ToString(), E.MontoCompromiso.ToString(), XLAlignmentHorizontalValues.Right, ref wssz);

                    CeldaDatos("O" + AquiVa.ToString(), E.FechaAsignacion.ToString(), XLAlignmentHorizontalValues.Center, ref wssz);
                    CeldaDatos("P" + AquiVa.ToString(), E.CantidadAsignacion.ToString(), XLAlignmentHorizontalValues.Left, ref wssz);
                    CeldaDatos("Q" + AquiVa.ToString(), E.MontoAsignacion.ToString(), XLAlignmentHorizontalValues.Right, ref wssz);

                    CeldaDatos("R" + AquiVa.ToString(), E.FechaPicking.ToString(), XLAlignmentHorizontalValues.Center, ref wssz);
                    CeldaDatos("S" + AquiVa.ToString(), E.CantidadPicking.ToString(), XLAlignmentHorizontalValues.Left, ref wssz);
                    CeldaDatos("T" + AquiVa.ToString(), E.MontoPicking.ToString(), XLAlignmentHorizontalValues.Right, ref wssz);

                    CeldaDatos("U" + AquiVa.ToString(), E.FechaRemision.ToString(), XLAlignmentHorizontalValues.Center, ref wssz);
                    CeldaDatos("V" + AquiVa.ToString(), E.CantidadRemision.ToString(), XLAlignmentHorizontalValues.Left, ref wssz);
                    CeldaDatos("W" + AquiVa.ToString(), E.MontoRemision.ToString(), XLAlignmentHorizontalValues.Right, ref wssz);

                    CeldaDatos("X" + AquiVa.ToString(), E.FechaFactura.ToString(), XLAlignmentHorizontalValues.Center, ref wssz);
                    CeldaDatos("Y" + AquiVa.ToString(), E.CantidadFactura.ToString(), XLAlignmentHorizontalValues.Left, ref wssz);
                    CeldaDatos("Z" + AquiVa.ToString(), E.MontoFactura.ToString(), XLAlignmentHorizontalValues.Right, ref wssz);

                    CeldaDatos("AA" + AquiVa.ToString(), E.FechaEmbarque.ToString(), XLAlignmentHorizontalValues.Center, ref wssz);
                    CeldaDatos("AB" + AquiVa.ToString(), E.CantidadEmbarque.ToString(), XLAlignmentHorizontalValues.Left, ref wssz);
                    CeldaDatos("AC" + AquiVa.ToString(), E.MontoEmbarque.ToString(), XLAlignmentHorizontalValues.Right, ref wssz);

                    CeldaDatos("AD" + AquiVa.ToString(), E.FechaEntrega.ToString(), XLAlignmentHorizontalValues.Center, ref wssz);
                    CeldaDatos("AE" + AquiVa.ToString(), E.CantidadEntrega.ToString(), XLAlignmentHorizontalValues.Left, ref wssz);
                    CeldaDatos("AF" + AquiVa.ToString(), E.MontoEntrega.ToString(), XLAlignmentHorizontalValues.Right, ref wssz);

                    CeldaDatos("AG" + AquiVa.ToString(), E.FechaCobranza.ToString(), XLAlignmentHorizontalValues.Center, ref wssz);
                    CeldaDatos("AH" + AquiVa.ToString(), E.CantidadCobranza.ToString(), XLAlignmentHorizontalValues.Left, ref wssz);
                    CeldaDatos("AI" + AquiVa.ToString(), E.MontoCobranza.ToString(), XLAlignmentHorizontalValues.Right, ref wssz);


                    AquiVa = AquiVa + 1;
                }
                */
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        void AjustaHoja(ref IXLWorksheet wssts)
        {
            foreach (var item in wssts.ColumnsUsed())
            {
                item.AdjustToContents();
            }
        }
        
        void CeldaHeader(string rango, string etiquetea, ref IXLWorksheet wsss)
        {
            try
            {
                wsss.Range(rango).SetValue(etiquetea);
                wsss.Range(rango).Style.Font.Bold = true;
                wsss.Range(rango).Style.Font.FontColor = XLColor.White;
                wsss.Range(rango).Style.Fill.BackgroundColor = XLColor.FromHtml("#3385ff");
                wsss.Range(rango).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wsss.Range(rango).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                wsss.Range(rango).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
            }
            catch (Exception ex)
            {
                /// errorMsgH3.InnerHtml = "CeldaHeader :: " + ex.Message;
                throw ex;
            }
        }
        
        void CeldaDatos(string rango, string etiquetea, XLAlignmentHorizontalValues HAlign, ref IXLWorksheet wsss)
        {
            try
            {
                wsss.Cell(rango).SetValue(etiquetea);
                wsss.Cell(rango).Style.Alignment.Horizontal = HAlign;
                wsss.Cell(rango).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
                wsss.Cell(rango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void CeldaDatoInt(string rango, int intNumero, XLAlignmentHorizontalValues HAlign, ref IXLWorksheet wsss)
        {
            wsss.Cell(rango).Value = intNumero;
            wsss.Cell(rango).Style.Alignment.Horizontal = HAlign;
            wsss.Cell(rango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        }



        void Exportaaa(XLWorkbook workbook2, DataSet xds, string xfilenombre, int xi)
        {
            try
            {
                using (ExcelEngine excelEngine = new ExcelEngine())
                {
                    IApplication application = excelEngine.Excel;
                    /// application.DefaultVersion = ExcelVersion.Excel2013;
                    application.DefaultVersion = ExcelVersion.Xlsx;
                    IWorkbook workbook = application.Workbooks.Create(xi);
                    /// IWorkbook workbook = application.Workbooks.Create(xds.Tables.Count);
                    /// 
                    int ihoja = 0;
                    foreach (DataTable dt in xds.Tables)
                    {
                        IWorksheet worksheet = workbook.Worksheets[ihoja];
                        //Add DataTable as Worksheet.
                        //Import DataTable to the worksheet
                        worksheet.ImportDataTable(dt, true, 1, 1);
                        worksheet.Name = dt.TableName;

                        //Creating Excel table or list object and apply style to the table
                        IListObject table = worksheet.ListObjects.Create("TrackingPedidos_" + ihoja.ToString(), worksheet.UsedRange);

                        table.BuiltInTableStyle = TableBuiltInStyles.TableStyleMedium2;

                        //Autofit the columns
                        worksheet.UsedRange.AutofitColumns();

                        ///	wb.Worksheets.Add(dt);
                        ihoja = ihoja + 1;
                    }

                    //Saving the workbook as stream
                    /// FileStream stream = new FileStream(xfilenombre + ".xlsx", FileMode.Create, FileAccess.ReadWrite);
                    //  workbook.SaveAs(stream);
                    //  stream.Dispose();

                    workbook.SaveAs(xfilenombre + ".xlsx", Response, ExcelDownloadType.Open, ExcelHttpContentType.Excel2016);
                }
            }
            catch (Exception ex)
            {
                //Alerta(ex.Message);
                //ErrorManager(ex.Message);
                throw ex;
            }
        }


    }
}