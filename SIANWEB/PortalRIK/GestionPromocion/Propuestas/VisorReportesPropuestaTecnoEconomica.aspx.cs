using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocios;
using CapaModelo;
using CapaEntidad;
using Newtonsoft.Json;
using SIANWEB.Core.UI;
using System.IO;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Text;

namespace SIANWEB.PortalRIK.GestionPromocion.Propuestas
{
    public partial class VisorReportesPropuestaTecnoEconomica : System.Web.UI.Page
    {
        private int IdCliente;
        private int IdValuacion;
        private int IdOportunidad;
        //protected IEnumerable<CrmPropuestaTecnica> _DetallePropuesta = null;
        //protected IEnumerable<CrmOportunidadesProducto> _DetallePropuestaEconomica = null;

        public List<string> GetText(string text, int width)
        {
            string[] palabras = text.Split(' ');
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            int length = palabras.Length;
            List<string> resultado = new List<string>();
            for (int i = 0; i < length; i++)
            {
                sb1.AppendFormat("{0} ", palabras[i]);
                if (sb1.ToString().Length > width)
                {
                    resultado.Add(sb2.ToString());
                    sb1 = new StringBuilder();
                    sb2 = new StringBuilder();
                    i--;
                }
                else
                {
                    sb2.AppendFormat("{0} ", palabras[i]);
                }
            }
            resultado.Add(sb2.ToString());

            List<string> resultado2 = new List<string>();
            string temp;

            int index1, index2, salto;
            string target;
            int limite = resultado.Count;
            foreach (var item in resultado)
            {
                target = " ";
                temp = item.ToString().Trim();
                index1 = 0; index2 = 0; salto = 2;

                if (limite <= 1)
                {
                    resultado2.Add(temp);
                    break;
                }
                while (temp.Length <= width)
                {
                    if (temp.IndexOf(target, index2) < 0)
                    {
                        index1 = 0; index2 = 0;
                        target = target + " ";
                        salto++;
                    }
                    index1 = temp.IndexOf(target, index2);
                    temp = temp.Insert(temp.IndexOf(target, index2), " ");
                    index2 = index1 + salto;

                }
                limite--;
                resultado2.Add(temp);
            }
            return resultado2;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int IdRik;
            string IdCte;
            string IdVal;
            string IdOp;
            string IdTipoRep;
            int idTipoRep = 0;

            Int32.TryParse(Request.QueryString["IdRik"].ToString(), out IdRik);

            IdTipoRep = Request.QueryString["idTipoRep"].ToString();
            IdCte = Request.QueryString["idCte"].ToString();
            IdVal = Request.QueryString["idVal"].ToString();
            IdOp = Request.QueryString["idOp"].ToString();


            Int32.TryParse(IdOp, out IdOportunidad);
            Int32.TryParse(IdTipoRep, out idTipoRep);
            Int32.TryParse(IdCte, out IdCliente);
            Int32.TryParse(IdVal, out IdValuacion);

            if (!IsPostBack)
            {
                string strImp;
                strImp = Request["__EVENTARGUMENT"];

                try
                {
                    string CentroDistirubcion_Direccion = "";
                    CentroDistribucion cd = new CentroDistribucion();
                    new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref cd, EntidadSesion.Id_Cd, EntidadSesion.Id_Emp, EntidadSesion.Emp_Cnx);
                    CentroDistirubcion_Direccion = cd.Cd_Calle + " #" + cd.Cd_Numero + ", Col." + cd.Cd_Colonia + ", " + cd.Cd_Estado + ", " + cd.Cd_Ciudad + ", " + cd.Cd_Pais;

                    eRepValuacionParams VP = new eRepValuacionParams();
                    CN_CapRepValuacionParams Cn_vp = new CN_CapRepValuacionParams();
                    VP = Cn_vp.SpCrmOportunidades_RepValuacionProp_Ver1(EntidadSesion.Id_Emp, EntidadSesion.Id_Cd, IdOportunidad, EntidadSesion.Emp_Cnx);

                    eCRM_ValuacionCamposAdicionales_Ver2 VP2 = new eCRM_ValuacionCamposAdicionales_Ver2();
                    CN_CapRepValuacionParams Cn_vp2 = new CN_CapRepValuacionParams();
                    VP2 = Cn_vp.SpCrmOportunidades_RepValuacionProp_Ver2(
                        EntidadSesion.Id_Emp, EntidadSesion.Id_Cd, IdOportunidad, EntidadSesion.Emp_Cnx
                    );

                    string DireccionLinea1 = VP2.Key_Calle + " " + VP2.Key_No + ", " + VP2.Key_Colonia;
                    string DireccionLinea2 = "C.P. " + VP2.Key_CP + ", " + VP2.Key_Municipio + ", " + VP2.Key_Estado;

                    ReportViewer1.Reset();
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.EnableExternalImages = true;


                    /*
                    ServerReport serverRepot = ReportViewer1.ServerReport;
                    System.Net.ICredentials credentials = System.Net.CredentialCache.DefaultCredentials;
                    IReportServerCredentials rsCredentials = serverRepot.ReportServerCredentials;
                    rsCredentials.NetworkCredentials = credentials;
                    */

                    /*
                     * 
                    if (idTipoRep == 1)
                    {                        
                        
                        ReportDataSource dsPage1 = new ReportDataSource("DataSet1", GetDataSet1(IdRik, IdOportunidad));
                        ReportViewer1.LocalReport.DataSources.Add(dsPage1);
                        string sRep = Server.MapPath("../../Reportes/") + "rptPTEEncabezado.rdlc";
                        ReportViewer1.LocalReport.ReportPath = sRep;                                                
                        List<ReportParameter> p = new List<ReportParameter>();
                        p.Add(new ReportParameter("rpDireccion1", VP.Direccion1));
                        p.Add(new ReportParameter("rpDireccion2", VP.Direccion2));
                        p.Add(new ReportParameter("rpNombreRik", VP.NombreRik));
                        p.Add(new ReportParameter("rpNombreAtencion", VP.NombreAtencion));
                        p.Add(new ReportParameter("rpRepClienteNombre", VP.RepresentanteClienteNombre));
                        ReportViewer1.LocalReport.SetParameters(p.ToArray());
                        ReportViewer1.LocalReport.Refresh();
                    }
                    // PRODUCTOS VER1 
                    if (idTipoRep == 2)
                    {
                        // PRODUCTOS
                        ReportDataSource dsPage1 = new ReportDataSource("DataSet1", GetDataSet1(IdRik, IdOportunidad));
                        ReportViewer1.LocalReport.DataSources.Add(dsPage1);
                        string sRep = Server.MapPath("../../Reportes/") + "rptPTEProductos.rdlc";
                        ReportViewer1.LocalReport.ReportPath = sRep;
                        List<ReportParameter> p = new List<ReportParameter>();
                        p.Add(new ReportParameter("rpDireccion", VP.Direccion1));
                        p.Add(new ReportParameter("rpDireccion2", VP.Direccion2));
                        ReportViewer1.LocalReport.SetParameters(p.ToArray());
                        ReportViewer1.LocalReport.Refresh();
                    }
                    if (idTipoRep == 3)
                    {
                        ReportViewer1.LocalReport.EnableExternalImages = true;
                        ReportDataSource dsPage1 = new ReportDataSource("DataSet1", GetDataSetPTecnica(IdRik, IdOportunidad));
                        ReportViewer1.LocalReport.DataSources.Add(dsPage1);
                        string sRep = Server.MapPath("../../Reportes/") + "rptPTE_Tecnica2.rdlc";
                        ReportViewer1.LocalReport.ReportPath = sRep;
                        List<ReportParameter> p = new List<ReportParameter>();
                        p.Add(new ReportParameter("rpDireccion", VP.Direccion1));
                        p.Add(new ReportParameter("rpDireccion2", VP.Direccion2));
                        ReportViewer1.LocalReport.SetParameters(p.ToArray());
                        ReportViewer1.LocalReport.Refresh();
                    }
                    */

                    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
                    // VER 2 - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
                    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 


                    // 1.- ENCAVEZADO Portada 
                    if (idTipoRep == 10)
                    {
                        ReportDataSource dsPage1 = new ReportDataSource("DataSet1", GetDataSet2(IdRik, IdOportunidad, 1));
                        ReportViewer1.LocalReport.DataSources.Add(dsPage1);
                        string sRep = Server.MapPath("../../Reportes/") + "repCRM2_Encabezado.rdlc";
                        ReportViewer1.LocalReport.ReportPath = sRep;
                        List<ReportParameter> p = new List<ReportParameter>();

                        p.Add(new ReportParameter("rpDireccion1", DireccionLinea1));
                        p.Add(new ReportParameter("rpDireccion2", DireccionLinea2));
                        p.Add(new ReportParameter("rpDireccion3", VP2.Key_Nomenclatura));

                        p.Add(new ReportParameter("rpNombreRik", VP2.Key_NombreRik));

                        ReportViewer1.LocalReport.SetParameters(p.ToArray());
                        ReportViewer1.LocalReport.Refresh();
                    }

                    // 2.- PRESNTACION Atencion 
                    if (idTipoRep == 11)
                    {
                        ReportDataSource dsPage1 = new ReportDataSource("DataSet1", GetDataSet2(IdRik, IdOportunidad, 1));
                        ReportViewer1.LocalReport.DataSources.Add(dsPage1);
                        string sRep = Server.MapPath("../../Reportes/") + "repCRM2_Presentacion.rdlc";
                        ReportViewer1.LocalReport.ReportPath = sRep;
                        List<ReportParameter> p = new List<ReportParameter>();

                        p.Add(new ReportParameter("rpDireccion1", DireccionLinea1));
                        p.Add(new ReportParameter("rpDireccion2", DireccionLinea2));
                        p.Add(new ReportParameter("rpDireccion3", VP2.Key_Nomenclatura));

                        p.Add(new ReportParameter("rpNombreRik", VP2.Key_NombreRik));
                        p.Add(new ReportParameter("rpNombreAtencion", VP2.Cte_AtencionA));
                        p.Add(new ReportParameter("rpRepClienteNombre", VP2.Cte_Nombre));

                        p.Add(new ReportParameter("rpAtencion", ""));

                        /*
                        List<string> sbList1 = new List<string>(); 
                        sbList1 = this.GetText("En KEY Soluciones de Limpieza, queremos agradecerle de antemano por la oporunidad que "+
                        "nos otorga al leer este documento, nuestra carta de presentación, nuestra intención de lograr"+
                        "una colaboración con su empresa para lograr espacios más limpios y más sanos.",50);
                        p.Add(new ReportParameter("rpParrafo1", sbList1.ToString()));
                        */

                        p.Add(new ReportParameter("rpAtencionLinea1", VP2.Cte_Nombre));
                        p.Add(new ReportParameter("rpAtencionLinea2", VP2.Cte_Calle + " " + VP2.Cte_No + ", " + VP2.Cte_Colonia + ", C.P." + VP2.Cte_CP + ", " + VP2.Cte_Municipio + ", " + VP2.Cte_Estado));
                        p.Add(new ReportParameter("rpAtencionLinea3", VP2.Cte_AtencionA + ":"));

                        ReportViewer1.LocalReport.SetParameters(p.ToArray());
                        ReportViewer1.LocalReport.Refresh();
                    }

                    // 3.- PROPUESTA VALOR - Nuestra Propuesta de Valor (3)
                    if (idTipoRep == 12)
                    {
                        ReportDataSource dsPage1 = new ReportDataSource("DataSet1", GetDataSet2(IdRik, IdOportunidad, 1));
                        ReportViewer1.LocalReport.DataSources.Add(dsPage1);
                        string sRep = Server.MapPath("../../Reportes/") + "repCRM2_PropuestaValor.rdlc";
                        ReportViewer1.LocalReport.ReportPath = sRep;
                        List<ReportParameter> p = new List<ReportParameter>();

                        p.Add(new ReportParameter("rpDireccion1", DireccionLinea1));
                        p.Add(new ReportParameter("rpDireccion2", DireccionLinea2));
                        p.Add(new ReportParameter("rpDireccion3", VP2.Key_Nomenclatura));

                        p.Add(new ReportParameter("rpNombreRik", VP2.Key_NombreRik));
                        p.Add(new ReportParameter("rpNombreAtencion", VP2.Cte_AtencionA));
                        p.Add(new ReportParameter("rpRepClienteNombre", VP2.Cte_Nombre));

                        ReportViewer1.LocalReport.SetParameters(p.ToArray());
                        ReportViewer1.LocalReport.Refresh();
                    }

                    // 4.- LISTADO DE PROUDCTO / Soluciones KEY (4)
                    if (idTipoRep == 13)
                    {
                        ReportDataSource dsPage1 = new ReportDataSource("DataSet1", GetDataSet2(IdRik, IdOportunidad, 9));
                        ReportViewer1.LocalReport.DataSources.Add(dsPage1);
                        string sRep = Server.MapPath("../../Reportes/") + "repCRM2_SolucionesKey.rdlc";
                        ReportViewer1.LocalReport.ReportPath = sRep;

                        List<ReportParameter> p = new List<ReportParameter>();

                        p.Add(new ReportParameter("rpDireccion1", DireccionLinea1));
                        p.Add(new ReportParameter("rpDireccion2", DireccionLinea2));
                        p.Add(new ReportParameter("rpDireccion3", VP2.Key_Nomenclatura));

                        p.Add(new ReportParameter("rpNombreRik", VP2.Key_NombreRik));
                        p.Add(new ReportParameter("rpNombreAtencion", VP2.Cte_AtencionA));
                        p.Add(new ReportParameter("rpRepClienteNombre", VP2.Cte_Nombre));

                        ReportViewer1.LocalReport.SetParameters(p.ToArray());
                        ReportViewer1.LocalReport.Refresh();
                    }

                    // 5.- Propuesta Economica Key
                    if (idTipoRep == 14)
                    {
                        // Quimico
                        DataTable dt1 = new DataTable();
                        dt1 = GetDataSet2(IdRik, IdOportunidad, 1);
                        ReportDataSource dsPage1 = new ReportDataSource("DataSet1", dt1);
                        // Papel
                        DataTable dt2 = new DataTable();
                        dt2 = GetDataSet2(IdRik, IdOportunidad, 2);
                        ReportDataSource dsPage2 = new ReportDataSource("DataSet2", dt2);
                        // Suplementos
                        DataTable dt3 = new DataTable();
                        dt3 = GetDataSet2(IdRik, IdOportunidad, 3);
                        ReportDataSource dsPage3 = new ReportDataSource("DataSet3", dt3);
                        // Dosificadores/Despachadores
                        DataTable dt4 = new DataTable();
                        dt4 = GetDataSet2(IdRik, IdOportunidad, 4);
                        ReportDataSource dsPage4 = new ReportDataSource("DataSet4", dt4);
                        //Otros
                        DataTable dt5 = new DataTable();
                        dt5 = GetDataSet2(IdRik, IdOportunidad, 5);
                        ReportDataSource dsPage5 = new ReportDataSource("DataSet5", dt5);

                        ReportViewer1.LocalReport.DataSources.Add(dsPage1);
                        ReportViewer1.LocalReport.DataSources.Add(dsPage2);
                        ReportViewer1.LocalReport.DataSources.Add(dsPage3);
                        ReportViewer1.LocalReport.DataSources.Add(dsPage4);
                        ReportViewer1.LocalReport.DataSources.Add(dsPage5);

                        string sRep = Server.MapPath("../../Reportes/") + "repCRM2_PropEco.rdlc";
                        ReportViewer1.LocalReport.ReportPath = sRep;
                        List<ReportParameter> p = new List<ReportParameter>();

                        p.Add(new ReportParameter("rpDireccion1", DireccionLinea1));
                        p.Add(new ReportParameter("rpDireccion2", DireccionLinea2));
                        p.Add(new ReportParameter("rpDireccion3", VP2.Key_Nomenclatura));

                        p.Add(new ReportParameter("rpNombreRik", VP2.Key_NombreRik));
                        p.Add(new ReportParameter("rpNombreAtencion", VP2.Cte_AtencionA));
                        p.Add(new ReportParameter("rpRepClienteNombre", VP2.Cte_Nombre));

                        // VISIBILIDAD                        
                        if (dt1.Rows.Count > 0)
                        {
                            p.Add(new ReportParameter("rpTabla1Visible", "1"));
                        }
                        else
                        {
                            p.Add(new ReportParameter("rpTabla1Visible", "0"));
                        }
                        if (dt2.Rows.Count > 0)
                        {
                            p.Add(new ReportParameter("rpTabla2Visible", "1"));
                        }
                        else
                        {
                            p.Add(new ReportParameter("rpTabla2Visible", "0"));
                        }
                        if (dt3.Rows.Count > 0)
                        {
                            p.Add(new ReportParameter("rpTabla3Visible", "1"));
                        }
                        else
                        {
                            p.Add(new ReportParameter("rpTabla3Visible", "0"));
                        }
                        if (dt4.Rows.Count > 0)
                        {
                            p.Add(new ReportParameter("rpTabla4Visible", "1"));
                        }
                        else
                        {
                            p.Add(new ReportParameter("rpTabla4Visible", "0"));
                        }
                        if (dt5.Rows.Count > 0)
                        {
                            p.Add(new ReportParameter("rpTabla5Visible", "1"));
                        }
                        else
                        {
                            p.Add(new ReportParameter("rpTabla5Visible", "0"));
                        }

                        ReportViewer1.LocalReport.SetParameters(p.ToArray());
                        ReportViewer1.LocalReport.Refresh();
                    }

                    // Propuesta Economica Key Costo En Uso
                    if (idTipoRep == 16)
                    {
                        // Quimico
                        DataTable dt1 = new DataTable();
                        dt1 = GetDataSet2(IdRik, IdOportunidad, 1);
                        ReportDataSource dsPage1 = new ReportDataSource("DataSet1", dt1);
                        // Papel
                        DataTable dt2 = new DataTable();
                        dt2 = GetDataSet2(IdRik, IdOportunidad, 2);
                        ReportDataSource dsPage2 = new ReportDataSource("DataSet2", dt2);
                        // Suplementos
                        DataTable dt3 = new DataTable();
                        dt3 = GetDataSet2(IdRik, IdOportunidad, 3);
                        ReportDataSource dsPage3 = new ReportDataSource("DataSet3", dt3);
                        // Dosificadores/Despachadores
                        DataTable dt4 = new DataTable();
                        dt4 = GetDataSet2(IdRik, IdOportunidad, 4);
                        ReportDataSource dsPage4 = new ReportDataSource("DataSet4", dt4);
                        //Otros
                        DataTable dt5 = new DataTable();
                        dt5 = GetDataSet2(IdRik, IdOportunidad, 5);
                        ReportDataSource dsPage5 = new ReportDataSource("DataSet5", dt5);

                        ReportViewer1.LocalReport.DataSources.Add(dsPage1);
                        ReportViewer1.LocalReport.DataSources.Add(dsPage2);
                        ReportViewer1.LocalReport.DataSources.Add(dsPage3);
                        ReportViewer1.LocalReport.DataSources.Add(dsPage4);
                        ReportViewer1.LocalReport.DataSources.Add(dsPage5);

                        string sRep = Server.MapPath("../../Reportes/") + "repCRM2_PropEco_CEU.rdlc";
                        ReportViewer1.LocalReport.ReportPath = sRep;
                        List<ReportParameter> p = new List<ReportParameter>();

                        p.Add(new ReportParameter("rpDireccion1", DireccionLinea1));
                        p.Add(new ReportParameter("rpDireccion2", DireccionLinea2));
                        p.Add(new ReportParameter("rpDireccion3", VP2.Key_Nomenclatura));

                        p.Add(new ReportParameter("rpNombreRik", VP2.Key_NombreRik));
                        p.Add(new ReportParameter("rpNombreAtencion", VP2.Cte_AtencionA));
                        p.Add(new ReportParameter("rpRepClienteNombre", VP2.Cte_Nombre));

                        // VISIBILIDAD                        
                        if (dt1.Rows.Count > 0)
                        {
                            p.Add(new ReportParameter("rpTabla1Visible", "1"));
                        }
                        else
                        {
                            p.Add(new ReportParameter("rpTabla1Visible", "0"));
                        }
                        if (dt2.Rows.Count > 0)
                        {
                            p.Add(new ReportParameter("rpTabla2Visible", "1"));
                        }
                        else
                        {
                            p.Add(new ReportParameter("rpTabla2Visible", "0"));
                        }
                        if (dt3.Rows.Count > 0)
                        {
                            p.Add(new ReportParameter("rpTabla3Visible", "1"));
                        }
                        else
                        {
                            p.Add(new ReportParameter("rpTabla3Visible", "0"));
                        }
                        if (dt4.Rows.Count > 0)
                        {
                            p.Add(new ReportParameter("rpTabla4Visible", "1"));
                        }
                        else
                        {
                            p.Add(new ReportParameter("rpTabla4Visible", "0"));
                        }
                        if (dt5.Rows.Count > 0)
                        {
                            p.Add(new ReportParameter("rpTabla5Visible", "1"));
                        }
                        else
                        {
                            p.Add(new ReportParameter("rpTabla5Visible", "0"));
                        }

                        ReportViewer1.LocalReport.SetParameters(p.ToArray());
                        ReportViewer1.LocalReport.Refresh();
                    }

                    // 6.- TERMINOS Y CONDICIONES
                    if (idTipoRep == 15)
                    {
                        ReportDataSource dsPage1 = new ReportDataSource("DataSet1", GetDataSet2(IdRik, IdOportunidad, 1));
                        ReportViewer1.LocalReport.DataSources.Add(dsPage1);
                        string sRep = Server.MapPath("../../Reportes/") + "repCRM2_TerminosCond.rdlc";
                        ReportViewer1.LocalReport.ReportPath = sRep;
                        List<ReportParameter> p = new List<ReportParameter>();

                        p.Add(new ReportParameter("rpDireccion1", DireccionLinea1));
                        p.Add(new ReportParameter("rpDireccion2", DireccionLinea2));
                        p.Add(new ReportParameter("rpDireccion3", VP2.Key_Nomenclatura));

                        p.Add(new ReportParameter("rpNombreRik", VP2.Key_NombreRik));
                        p.Add(new ReportParameter("rpNombreAtencion", VP2.Cte_AtencionA));
                        p.Add(new ReportParameter("rpRepClienteNombre", VP2.Cte_Nombre));

                        string sCondicionesCredito = "";
                        sCondicionesCredito = "Se establecer que las condiciones de crédito serán de " + VP.DiasCredito.ToString() + " días naturales a partir de la fecha de revisión de nuetra factura.";
                        p.Add(new ReportParameter("rpCondicionesCredito", sCondicionesCredito));

                        ReportViewer1.LocalReport.SetParameters(p.ToArray());
                        ReportViewer1.LocalReport.Refresh();
                    }

                }
                catch (Exception ex)
                {
                    idTipoRep = idTipoRep;
                }
            }

        }

        // PROPUESTA TECNICA 
        // PROPUESTA TECNICA 
        // PROPUESTA TECNICA 
        public DataTable GetDataSetPTecnica(int IdRik, int IdOp)
        {
            DataTable dt = new DataTable();

            try
            {

                List<CapaEntidad.ePropuestaTecnoEconomicaDetalle> lst = new List<CapaEntidad.ePropuestaTecnoEconomicaDetalle>();
                CN_CrmPropuestaEconomica cnPE = new CN_CrmPropuestaEconomica();

                lst = cnPE.CRM_ObtenerPropuestaEconomica_Ver2(
                EntidadSesion.Id_Emp, EntidadSesion.Id_Cd, IdCliente, IdRik, IdOp, IdValuacion, 4, EntidadSesion
                );

                dt.Columns.Add("Id_Val");
                dt.Columns.Add("Id_Op");
                dt.Columns.Add("Id_Prd");
                dt.Columns.Add("Id_VapDet");
                dt.Columns.Add("Prd_Descripcion");
                dt.Columns.Add("Vap_Cantidad");
                dt.Columns.Add("Vap_Precio");
                dt.Columns.Add("Prd_Presentacion");
                dt.Columns.Add("COP_ConsumoMensual");
                dt.Columns.Add("ConsumoMensualL");
                dt.Columns.Add("GastoMensual");
                dt.Columns.Add("COP_DilucionAntecedente");
                dt.Columns.Add("COP_DilucionConsecuente");
                dt.Columns.Add("ConsumoMensualLDiluidos");
                dt.Columns.Add("CostoEnUso");
                dt.Columns.Add("CPT_ProductoActual");
                dt.Columns.Add("CPT_RecursoImagenProductoActual");
                dt.Columns.Add("CPT_SituacionActual");
                dt.Columns.Add("ProductoKey");
                dt.Columns.Add("CPT_RecursoImagenSolucionKey");
                dt.Columns.Add("CPT_VentajasKey");
                dt.Columns.Add("COP_EsQuimico");
                dt.Columns.Add("Prd_UniEmp");
                dt.Columns.Add("AplDilucion");

                foreach (var item in lst)
                {

                    var row = dt.NewRow();

                    row["Id_Val"] = item.Id_Val;
                    row["Id_Op"] = item.Id_Op;
                    row["Id_Prd"] = item.Id_Prd;
                    row["Id_VapDet"] = item.Id_VapDet;
                    row["Prd_Descripcion"] = item.Prd_Descripcion;
                    row["Vap_Cantidad"] = item.Vap_Cantidad;
                    row["Vap_Precio"] = item.Vap_Precio;
                    row["Prd_Presentacion"] = item.Prd_Presentacion;
                    row["COP_ConsumoMensual"] = item.COP_ConsumoMensual;
                    row["ConsumoMensualL"] = item.ConsumoMensualL;
                    row["GastoMensual"] = item.GastoMensual;
                    //row["COP_DilucionAntecedente"] = item.COP_DilucionAntecedente;
                    row["COP_DilucionConsecuente"] = item.COP_DilucionConsecuente;
                    row["ConsumoMensualLDiluidos"] = item.ConsumoMensualLDiluidos;
                    row["CostoEnUso"] = item.CostoEnUso;
                    row["CPT_ProductoActual"] = item.CPT_ProductoActual;
                    row["CPT_RecursoImagenProductoActual"] = item.CPT_RecursoImagenProductoActual;
                    row["CPT_SituacionActual"] = item.CPT_SituacionActual;
                    row["ProductoKey"] = item.ProductoKey;
                    row["CPT_RecursoImagenSolucionKey"] = item.CPT_RecursoImagenSolucionKey;
                    row["CPT_VentajasKey"] = item.CPT_VentajasKey;
                    row["COP_EsQuimico"] = item.COP_EsQuimico;
                    row["Prd_UniEmp"] = item.Prd_UniEmp;
                    row["AplDilucion"] = item.AplDilucion;
                    if (item.AplDilucion == 1)
                    {
                        row["COP_DilucionAntecedente"] = item.COP_DilucionAntecedente.ToString() + " : " + item.COP_DilucionConsecuente.ToString();
                    }
                    else
                    {
                        // 25 Ene 2018 RFH
                        //row["COP_Dilucion"] = "N/A";
                        row["COP_DilucionAntecedente"] = "";
                    }


                    /*
                    row["Descripcion"] = item.CPT_ProductoActual == null ? "" : item.CPT_ProductoActual.ToString();
                    row["Nombre"] = item.CPT_RecursoImagenProductoActual == null ? "" : item.CPT_RecursoImagenProductoActual.ToString();
                    row["Ruta"] = item.CPT_RecursoImagenSolucionKey == null ? "" : item.CPT_RecursoImagenSolucionKey.ToString();
                    row["ProductoSerializable"] = item.CPT_SituacionActual == null ? "" : item.CPT_SituacionActual.ToString();
                    row["ProductoActual"] = item.CPT_VentajasKey == null ? "" : item.CPT_VentajasKey.ToString();
                    row["ProductoActual2"] = item.Prd_Descripcion == null ? "" : item.Prd_Descripcion;
                    row["DilucionCompuesto"] = 0;
                    row["CostoEnUso"] = 0;
                    row["CapValProyectoDet"] = 0;
                    row["Context"] = 0;
                    row["Id_Emp"] = 0; //Precio
                    row["Id_Cd"] = 0; // Presentacion
                    row["Id_Op"] = 0; //CONSUMO MENSUAL(UNIDADES)
                    row["Id_Cte"] = 0; //CONSUMO MENSUAL(UNIDADES)
                    row["Id_Rik"] = 0; // DILUCION
                    row["Id_Uen"] = 0;  //element.ProductoSerializable.Prd_UniEmp*element.COP_ConsumoMensual         // CONSUMO MENSUAL(L Diluidos);
                    row["Id_Seg"] = 0;  // CONSUMO MENSUAL DILUIDOS
                    row["Id_Area"] = 0;
                    row["Id_Sol"] = 0;
                    row["Id_Apl"] = 0;
                    row["Id_SubFam"] = 0;
                    row["Id_Prd"] = 0;
                    row["COP_Cantidad"] = 0;
                    if (item.AplDilucion == 1)
                    {
                        row["COP_Dilucion"] = item.COP_DilucionAntecedente.ToString() + " : " + item.COP_DilucionConsecuente.ToString();
                    }
                    else
                    {
                        row["COP_Dilucion"] = "N/A";
                    }
                    row["COP_EsQuimico"] = 0;
                    row["COP_CostoEnUso"] = 0;
                    row["COP_ConsumoMensual"] = 0;
                    row["COP_DilucionAntecedente"] = 0;
                    row["COP_DilucionConsecuente"] = 0;
                    row["CatSegmento"] = 0;
                    row["CatSolucion"] = 0;
                    row["CatUEN"] = 0;
                    row["CatProducto"] = 0;
                    row["CatArea"] = 0;
                    row["CatAplicacion"] = 0;
                    */
                    dt.Rows.Add(row);
                }

            }
            catch (Exception ex)
            {
                dt = null;
            }
            return dt;
        }

        // PROPUESTA TECNICA 
        // PROPUESTA TECNICA 
        // PROPUESTA TECNICA 

        public DataTable GetDataSet1(int IdRik, int IdOp, int Id_Ptp)
        {
            DataTable dt = new DataTable();
            try
            {
                List<CapaEntidad.ePropuestaTecnoEconomicaDetalle> lst = new List<CapaEntidad.ePropuestaTecnoEconomicaDetalle>();
                CN_CrmPropuestaEconomica cnPE = new CN_CrmPropuestaEconomica();

                /*
                lst = cnPE.CRM_ObtenerPropuestaEconomica(
                EntidadSesion.Id_Emp, 
                EntidadSesion.Id_Cd, 
                IdCliente, 
                IdRik, 
                IdOp, 
                IdValuacion, 
                EntidadSesion);*/

                lst = cnPE.CRM_ObtenerPropuestaEconomica_Ver2(
                EntidadSesion.Id_Emp,
                EntidadSesion.Id_Cd,
                IdCliente,
                IdRik,
                IdOp,
                IdValuacion,
                Id_Ptp,
                EntidadSesion);

                //lst = cnPE.CRM_ObtenerPropuestaEconomica(EntidadSesion.Id_Emp, EntidadSesion.Id_Cd, IdCliente, EntidadSesion.Id_Rik, IdValuacion, EntidadSesion);

                dt.Columns.Add("Id_Val");
                dt.Columns.Add("Id_Op");
                dt.Columns.Add("Id_VapDet");
                dt.Columns.Add("Id_Prd");
                dt.Columns.Add("Prd_Descripcion");
                dt.Columns.Add("Vap_Precio");
                dt.Columns.Add("Prd_Presentacion");
                dt.Columns.Add("COP_ConsumoMensual");
                dt.Columns.Add("Vap_Cantidad");
                dt.Columns.Add("ConsumoMensualL");
                dt.Columns.Add("GastoMensual");
                dt.Columns.Add("COP_DilucionAntecedente");
                dt.Columns.Add("COP_DilucionConsecuente");
                dt.Columns.Add("ConsumoMensualLDiluidos");
                dt.Columns.Add("CostoEnUso");
                dt.Columns.Add("CPT_ProductoActual");
                dt.Columns.Add("CPT_RecursoImagenProductoActual");
                dt.Columns.Add("CPT_SituacionActual");
                dt.Columns.Add("ProductoKey");
                dt.Columns.Add("CPT_RecursoImagenSolucionKey");
                dt.Columns.Add("CPT_VentajasKey");
                dt.Columns.Add("COP_EsQuimico");
                dt.Columns.Add("Prd_UniEmp");
                dt.Columns.Add("AplDilucion");
                dt.Columns.Add("Id_Ptp");
                dt.Columns.Add("Prd_UniNe");
                dt.Columns.Add("ImagenProductoBajaRes");
                dt.Columns.Add("ImagenProdutoAltaRes");

                foreach (var item in lst)
                {
                    var row = dt.NewRow();
                    row["Id_Val"] = item.Id_Val;
                    row["Id_Op"] = item.Id_Op;
                    row["Id_VapDet"] = item.Id_VapDet;
                    row["Id_Prd"] = item.Id_Prd;
                    row["Prd_Descripcion"] = item.Prd_Descripcion;
                    row["Vap_Precio"] = item.Vap_Precio;
                    row["Prd_Presentacion"] = item.Prd_Presentacion;
                    row["COP_ConsumoMensual"] = item.COP_ConsumoMensual;
                    row["Vap_Cantidad"] = item.Vap_Cantidad;
                    row["ConsumoMensualL"] = item.ConsumoMensualL;
                    row["GastoMensual"] = item.GastoMensual;
                    row["COP_DilucionAntecedente"] = item.COP_DilucionAntecedente;
                    row["COP_DilucionConsecuente"] = item.COP_DilucionConsecuente;
                    row["ConsumoMensualLDiluidos"] = item.ConsumoMensualLDiluidos;
                    row["CostoEnUso"] = item.CostoEnUso;
                    row["CPT_ProductoActual"] = item.CPT_ProductoActual;
                    row["CPT_RecursoImagenProductoActual"] = item.CPT_RecursoImagenProductoActual;
                    row["CPT_SituacionActual"] = item.CPT_SituacionActual;
                    row["ProductoKey"] = item.ProductoKey;
                    row["CPT_RecursoImagenSolucionKey"] = item.CPT_RecursoImagenSolucionKey;
                    row["CPT_VentajasKey"] = item.CPT_VentajasKey;
                    row["COP_EsQuimico"] = item.COP_EsQuimico;
                    row["Prd_UniEmp"] = item.Prd_UniEmp;
                    row["AplDilucion"] = item.AplDilucion;
                    row["Id_Ptp"] = item.Id_Ptp;
                    row["Prd_UniNe"] = item.Prd_UniNe;
                    if (item.AplDilucion == 1)
                    {
                        row["COP_DilucionAntecedente"] = item.COP_DilucionAntecedente.ToString() + " : " + item.COP_DilucionConsecuente.ToString();
                    }
                    else
                    {
                        row["COP_DilucionAntecedente"] = "";
                    }
                    row["ImagenProductoBajaRes"] = item.ImagenProductoBajaRes;
                    row["ImagenProdutoAltaRes"] = item.ImagenProductoAltaRes;

                    dt.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                dt = null;
            }
            return dt;
        }


        // 
        // DATA SET 2
        // 

        public DataTable GetDataSet2(int IdRik, int IdOp, int Id_Ptp)
        {
            DataTable dt = new DataTable();
            try
            {
                List<CapaEntidad.ePropuestaTecnoEconomicaDetalle> lst = new List<CapaEntidad.ePropuestaTecnoEconomicaDetalle>();
                CN_CrmPropuestaEconomica cnPE = new CN_CrmPropuestaEconomica();

                /*
                lst = cnPE.CRM_ObtenerPropuestaEconomica(
                EntidadSesion.Id_Emp, 
                EntidadSesion.Id_Cd, 
                IdCliente, 
                IdRik, 
                IdOp, 
                IdValuacion, 
                EntidadSesion);*/

                lst = cnPE.CRM_ObtenerPropuestaEconomica_Ver3(
                EntidadSesion.Id_Emp,
                EntidadSesion.Id_Cd,
                IdCliente,
                IdRik,
                IdOp,
                IdValuacion,
                Id_Ptp,
                EntidadSesion);

                //lst = cnPE.CRM_ObtenerPropuestaEconomica(EntidadSesion.Id_Emp, EntidadSesion.Id_Cd, IdCliente, EntidadSesion.Id_Rik, IdValuacion, EntidadSesion);

                dt.Columns.Add("Id_Val");
                dt.Columns.Add("Id_Op");
                dt.Columns.Add("Id_VapDet");
                dt.Columns.Add("Id_Prd");
                dt.Columns.Add("Prd_Descripcion");
                dt.Columns.Add("PrecioDeLista");
                dt.Columns.Add("Vap_Precio");
                dt.Columns.Add("Prd_Presentacion");
                dt.Columns.Add("COP_ConsumoMensual");
                dt.Columns.Add("Vap_Cantidad");
                dt.Columns.Add("ConsumoMensualL");
                dt.Columns.Add("GastoMensual");
                dt.Columns.Add("COP_DilucionAntecedente");
                dt.Columns.Add("COP_DilucionConsecuente");
                dt.Columns.Add("ConsumoMensualLDiluidos");
                dt.Columns.Add("CostoEnUso");
                dt.Columns.Add("CPT_ProductoActual");
                dt.Columns.Add("CPT_RecursoImagenProductoActual");
                dt.Columns.Add("CPT_SituacionActual");
                dt.Columns.Add("ProductoKey");
                dt.Columns.Add("CPT_RecursoImagenSolucionKey");
                dt.Columns.Add("CPT_VentajasKey");
                dt.Columns.Add("COP_EsQuimico");
                dt.Columns.Add("Prd_UniEmp");
                dt.Columns.Add("AplDilucion");
                dt.Columns.Add("Id_Ptp");
                dt.Columns.Add("Prd_UniNe");
                dt.Columns.Add("ImagenProductoBajaRes");
                dt.Columns.Add("ImagenProdutoAltaRes");

                foreach (var item in lst)
                {
                    var row = dt.NewRow();
                    row["Id_Val"] = item.Id_Val;
                    row["Id_Op"] = item.Id_Op;
                    row["Id_VapDet"] = item.Id_VapDet;
                    row["Id_Prd"] = item.Id_Prd;
                    row["Prd_Descripcion"] = item.Prd_Descripcion;
                    row["PrecioDeLista"] = item.PrecioDeLista;
                    row["Vap_Precio"] = item.Vap_Precio;
                    row["Prd_Presentacion"] = item.Prd_Presentacion;
                    row["COP_ConsumoMensual"] = item.COP_ConsumoMensual;
                    row["Vap_Cantidad"] = item.Vap_Cantidad;
                    row["ConsumoMensualL"] = item.ConsumoMensualL;
                    row["GastoMensual"] = item.GastoMensual;
                    row["COP_DilucionAntecedente"] = item.COP_DilucionAntecedente;
                    row["COP_DilucionConsecuente"] = item.COP_DilucionConsecuente;
                    row["ConsumoMensualLDiluidos"] = item.ConsumoMensualLDiluidos;
                    row["CostoEnUso"] = item.CostoEnUso;
                    row["CPT_ProductoActual"] = item.CPT_ProductoActual;
                    row["CPT_RecursoImagenProductoActual"] = item.CPT_RecursoImagenProductoActual;
                    row["CPT_SituacionActual"] = item.CPT_SituacionActual;
                    row["ProductoKey"] = item.ProductoKey;
                    row["CPT_RecursoImagenSolucionKey"] = item.CPT_RecursoImagenSolucionKey;
                    row["CPT_VentajasKey"] = item.CPT_VentajasKey;
                    row["COP_EsQuimico"] = item.COP_EsQuimico;
                    row["Prd_UniEmp"] = item.Prd_UniEmp;
                    row["AplDilucion"] = item.AplDilucion;
                    row["Id_Ptp"] = item.Id_Ptp;
                    row["Prd_UniNe"] = item.Prd_UniNe;
                    if (item.AplDilucion == 1)
                    {
                        row["COP_DilucionAntecedente"] = item.COP_DilucionAntecedente.ToString() + " : " + item.COP_DilucionConsecuente.ToString();
                    }
                    else
                    {
                        row["COP_DilucionAntecedente"] = "";
                    }
                    row["ImagenProductoBajaRes"] = item.ImagenProductoBajaRes;
                    row["ImagenProdutoAltaRes"] = item.ImagenProductoAltaRes;

                    dt.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                dt = null;
            }
            return dt;
        }

        //

        protected Sesion EntidadSesion
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
        //
    }
}