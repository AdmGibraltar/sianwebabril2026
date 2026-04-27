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


namespace SIANWEB
{

    /*
     
     MAR02-2020 RFH Actualizado
     * .- Se agrego la columna Acs_FrecuenciaTipo_Nombre
      
     */

    public partial class CapAcys2_VisorReporte : BaseServerPage
    {
        int Id_Acs;
        int Acs_Version;
        int Id_Ter;
        int Id_Cte;

        protected void Page_Load(object sender, EventArgs e)
        {
            Int32.TryParse(Request.QueryString["Id_Acs"].ToString(), out Id_Acs);
            Int32.TryParse(Request.QueryString["Acs_Version"].ToString(), out Acs_Version);
            Int32.TryParse(Request.QueryString["Id_Ter"].ToString(), out Id_Ter);
            Int32.TryParse(Request.QueryString["Id_Cte"].ToString(), out Id_Cte);

            if (!IsPostBack)
            {
                string strImp;
                strImp = Request["__EVENTARGUMENT"];
                try
                {
                    if (Id_Acs > 0)
                    {
                        ReportViewer1.Reset();

                        ReportDataSource dsPage1 = new ReportDataSource("DataSet1", GetDataSet1(Id_Acs, Acs_Version));
                        ReportViewer1.LocalReport.DataSources.Add(dsPage1);

                        ReportDataSource dsPage2 = new ReportDataSource("DataSet2", GetDataSet2(Id_Acs, Id_Ter, Id_Cte, Acs_Version));
                        ReportViewer1.LocalReport.DataSources.Add(dsPage2);

                        string sRep = Server.MapPath("Reportes/") + "Acys2_Reporte.rdlc";
                        ReportViewer1.LocalReport.ReportPath = sRep;

                        /*
                        List<ReportParameter> Params = new List<ReportParameter>();
                        ReportParameter P1 = new ReportParameter();
                        P1.Name = "Folio";
                        P1.Values.Add("999");
                        Params.Add(P1);
                        */

                        /*
                        List<reportparameter> parameters = new List<reportparameter>();
                        parameters.Add(new ReportParameter("coveredDateFrom", txtFrom.Text));
                        parameters.Add(new ReportParameter("coveredDateTo", txtTo.Text));
                        parameters.Add(new ReportParameter("workSiteCode", worksiteCode));
                        */

                        //ReportViewer1.LocalReport.SetParameters(Params);

                        /*
                        ReportParameter[] repParams = new ReportParameter[10];
                        repParams[0] = new ReportParameter("a", "");
                        ReportParameter p1 = new ReportParameter("", "");
                        ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1 });
                        */

                        ReportViewer1.LocalReport.Refresh();
                    }
                }
                catch (Exception ex)
                {
                    int X;
                }
            }

        }

        public DataTable GetDataSet1(int Id_Acs, int Id_AcsVersion)
        {
            DataTable dt = new DataTable();
            try
            {
                CapaEntidad.eAcys2 A = new CapaEntidad.eAcys2();
                A.Id_Emp = EntidadSesion.Id_Emp;
                A.Id_Cd = EntidadSesion.Id_Cd;
                A.Id_Acs = Id_Acs;
                A.Id_AcsVersion = Id_AcsVersion;

                CapaNegocios.CN_CapAcys2 CN = new CapaNegocios.CN_CapAcys2();
                CN.Consultar_PorFolio_ver2(ref A, EntidadSesion.Id_Rik, EntidadSesion.Emp_Cnx);

                dt.Columns.Add("Id_Emp");
                dt.Columns.Add("Id_Cd");
                dt.Columns.Add("Id_Acs");
                dt.Columns.Add("Acs_Version");
                //dt.Columns.Add("Acs_EstatusStr");
                dt.Columns.Add("Acs_EstatusTexto");

                dt.Columns.Add("Cd_Nombre");

                dt.Columns.Add("Acs_Fecha");
                dt.Columns.Add("Acs_FechaInicio");
                dt.Columns.Add("Acs_FechaFin");

                dt.Columns.Add("Cte_NomComercial");
                dt.Columns.Add("Cte_Nombre");

                dt.Columns.Add("Id_Cte");

                dt.Columns.Add("ClienteDireccion");
                dt.Columns.Add("ClienteColonia");
                dt.Columns.Add("ClienteMunicipio");
                dt.Columns.Add("ClienteCodPost");
                dt.Columns.Add("ClienteRFC");
                dt.Columns.Add("Id_Ter");

                dt.Columns.Add("Acs_RikNombre");
                dt.Columns.Add("Id_Rik");

                dt.Columns.Add("Acs_Contacto5");
                dt.Columns.Add("Acs_Telefono5");
                dt.Columns.Add("Acs_Correo5");

                dt.Columns.Add("Acs_Contacto2");
                dt.Columns.Add("Acs_Telefono2");
                dt.Columns.Add("Acs_email2");

                dt.Columns.Add("Acs_Contacto3");
                dt.Columns.Add("Acs_Telefono3");
                dt.Columns.Add("Acs_email3");

                dt.Columns.Add("Acs_Contacto4");
                dt.Columns.Add("Acs_Telefono4");
                dt.Columns.Add("Acs_email4");

                var row = dt.NewRow();

                row["Id_Emp"] = 0;
                row["Id_Cd"] = A.Id_Cd;
                row["Id_Acs"] = Id_Acs;
                row["Acs_Version"] = A.Acs_version;
                row["Acs_EstatusTexto"] = A.Acs_EstatusTexto;

                row["Cd_Nombre"] = A.Cd_Nombre;

                row["Acs_Fecha"] = A.Acs_Fecha;
                row["Acs_FechaInicio"] = A.Acs_FechaInicio;
                row["Acs_FechaFin"] = A.Acs_FechaInicio;

                row["Cte_NomComercial"] = A.Cte_NomComercial;
                row["Cte_Nombre"] = A.Cte_Nombre;

                row["Id_Cte"] = A.Id_Cte;

                row["ClienteDireccion"] = A.ClienteDireccion;
                row["ClienteColonia"] = A.ClienteColonia;
                row["ClienteMunicipio"] = A.ClienteMunicipio;
                row["ClienteCodPost"] = A.ClienteCodPost;
                row["ClienteRFC"] = A.ClienteRFC;
                row["Id_Ter"] = A.Id_Ter;

                row["Acs_RikNombre"] = A.Acs_RikNombre;
                row["Id_Rik"] = A.Id_Rik;

                row["Acs_Contacto5"] = A.Acs_Contacto5;
                row["Acs_Telefono5"] = A.Acs_Telefono5;
                row["Acs_Correo5"] = A.Acs_Correo5;

                row["Acs_Contacto2"] = A.Acs_Contacto2;
                row["Acs_Telefono2"] = A.Acs_Telefono2;
                row["Acs_email2"] = A.Acs_email2;

                row["Acs_Contacto3"] = A.Acs_Contacto3;
                row["Acs_Telefono3"] = A.Acs_Telefono3;
                row["Acs_email3"] = A.Acs_email3;

                row["Acs_Contacto4"] = A.Acs_Contacto4;
                row["Acs_Telefono4"] = A.Acs_Telefono4;
                row["Acs_email4"] = A.Acs_email4;

                dt.Rows.Add(row);

                /*
                foreach (var item in lst)
                {
                    var row = dt.NewRow();
                    row["Descripcion"] = item.Prd_Descripcion;
                    row["Nombre"] = 0;
                    row["Ruta"] = 0;
                    row["ProductoSerializable"] = 0;
                    row["ProductoActual"] = 0;
                    row["ProductoActual2"] = 0;
                    row["DilucionCompuesto"] = 0;
                    row["CostoEnUso"] = 0;
                    row["CapValProyectoDet"] = 0;
                    row["Context"] = 0;
                    float fPrecio = 0;
                    float.TryParse(item.Vap_Precio.ToString(), out fPrecio);
                    row["Id_Emp"] = string.Format("{0:C}", fPrecio); //Precio
                    row["Id_Cd"] = item.Prd_Presentacion; // Presentacion
                    row["Id_Op"] = item.COP_ConsumoMensual; //CONSUMO MENSUAL(UNIDADES)
                    row["Id_Cte"] = item.ConsumoMensualLDiluidos; //CONSUMO MENSUAL(UNIDADES)
                    row["Id_Rik"] = 0;
                    decimal PrecioPorCantidad = 0;
                    PrecioPorCantidad = item.Vap_Precio * item.Vap_Cantidad;
                    row["Id_Uen"] = string.Format("{0:C}", PrecioPorCantidad);  //element.ProductoSerializable.Prd_UniEmp*element.COP_ConsumoMensual         // CONSUMO MENSUAL(L Diluidos);
                    /*
                                        float.TryParse(item.COP_ConsumoMensual.ToString(), out fConsumoMensual);
                                        float fDilucion = 0;
                                        try
                                        {
                                            float.TryParse(item.COP_DilucionAntecedente.ToString(), out  fDilucion);
                                        }
                                        catch
                                        {
                                            fDilucion = 0;
                                        }*/

                //float fConsumoMensual_LDiluidos = fConsumoMensual * (fDilucion + 1);

                //row["Id_Seg"] = item.ConsumoMensualLDiluidos; // fConsumoMensual_LDiluidos;  // CONSUMO MENSUAL DILUIDOS

                // CALCULO DE COSTO EN USO
                //bool bEsQuimico = false;                    
                //if (item.COP_EsQuimico==1)
                //{
                //                        bEsQuimico = true; 
                //}

                //float bConsumoMensual = 0;
                //float.TryParse(item.COP_ConsumoMensual.ToString(), out bConsumoMensual);

                /*float fCostoEnUso = 0;
                try {
                    float.TryParse(item.COP_ConsumoMensual.ToString() , out fCostoEnUso);
                } catch {
                    fCostoEnUso=0;
                }*/

                //float fCostoEnUso = 0;
                //fPrecio = 0;

                /*
                if (bConsumoMensual > 0)
                {
                    if (bEsQuimico == true)
                    {
                        if (item.COP_DilucionConsecuente.ToString() != "")
                        {
                            float fDilucionConsecuente = 0;
                            float.TryParse(item.COP_DilucionConsecuente.ToString(), out fDilucionConsecuente);
                            float.TryParse(item.Vap_Precio.ToString(), out fPrecio);
                            //var precio = dataItem.CapValProyectoDet.Vap_Precio;//ProductoActual.Prd_Pesos;
                            float fUnidadesPresentacion = 0;
                            float.TryParse(item. .ProductoSerializable.Prd_UniEmp.ToString(), out fUnidadesPresentacion);
                            //var unidadesPresentacion = dataItem.ProductoSerializable.Prd_UniEmp;
                            float fConsumoMensualEnLitrosDiluidos = 0;
                            fConsumoMensualEnLitrosDiluidos = (fUnidadesPresentacion * bConsumoMensual) * (fDilucionConsecuente + 1);
                            //var consumoMensualEnLitrosDiluidos = ((unidadesPresentacion * consumoMensual) * (parseInt(dilucionConsecuente) + 1));
                            if (fConsumoMensualEnLitrosDiluidos > 0)
                            {
                                fCostoEnUso = (fConsumoMensual * fPrecio) / fConsumoMensualEnLitrosDiluidos;
                            }
                            /*if (consumoMensualEnLitrosDiluidos != 0.0)
                            {
                                costoEnUso = (consumoMensual * precio) / consumoMensualEnLitrosDiluidos;
                            }*
                        }
                    }
                }
                */
                /*
                    row["Id_Area"] = string.Format("{0:C}", item.CostoEnUso);
                    row["Id_Sol"] = 0;
                    row["Id_Apl"] = 0;
                    row["Id_SubFam"] = 0;
                    row["Id_Prd"] = item.Id_Prd;
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
                    dt.Rows.Add(row);
                }
                */

            }
            catch (Exception ex)
            {
                dt = null;
            }

            return dt;
        }

        // Regresa el detalle de los productos 
        // en el documento
        public DataTable GetDataSet2(int Id_Acs, int Id_Ter, int Id_Cte, int Id_AcsVersion)
        {

            DataTable dt = new DataTable();
            try
            {
                string MensajeError = "";

                List<eAcysDet2> lst = new List<eAcysDet2>();
                CapaNegocios.CN_CapAcys CN_AD = new CN_CapAcys();
                //CapaNegocios.CN_CapAcysDet CN_AD = new CN_CapAcysDet();
                //lst = CN_AD.Consulta_ProductosDeACYS(EntidadSesion.Id_Emp, EntidadSesion.Id_Cd, Id_Cte, Id_Acs, Id_Ter, EntidadSesion);
                int TipoCuenta;
                TipoCuenta = 0;
                lst = CN_AD.ConsultarDet2(EntidadSesion.Id_Emp, EntidadSesion.Id_Cd, Id_Acs, Id_AcsVersion, EntidadSesion.Emp_Cnx, TipoCuenta, ref MensajeError);

                // Se deben agegar las columnas necesarias.                                                
                dt.Columns.Add("Id_Prd");
                dt.Columns.Add("Prd_Descripcion");
                dt.Columns.Add("Uni_Descripcion");
                dt.Columns.Add("Prd_Presentacion");
                dt.Columns.Add("Acs_Cantidad");
                dt.Columns.Add("Acs_Precio");
                dt.Columns.Add("Acs_Frecuencia");
                dt.Columns.Add("Acs_FrecuenciaTipo_Nombre");
                dt.Columns.Add("Acs_Documento");

                foreach (var item in lst)
                {
                    var row = dt.NewRow();
                    row["Id_Prd"] = item.Id_Prd;
                    row["Prd_Descripcion"] = item.Prd_Descripcion;
                    row["Uni_Descripcion"] = item.Uni_Descripcion;
                    row["Prd_Presentacion"] = item.Prd_Presentacion;
                    row["Acs_Cantidad"] = item.Acs_Cantidad;
                    row["Acs_Precio"] = item.Acs_Precio.ToString("C2");
                    row["Acs_FrecuenciaTipo_Nombre"] = item.Acs_FrecuenciaTipo_Nombre;
                    row["Acs_Frecuencia"] = item.Acs_Frecuencia;
                    row["Acs_Documento"] = item.Acs_Documento;
                    dt.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                dt = null;
            }

            return dt;
        }

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