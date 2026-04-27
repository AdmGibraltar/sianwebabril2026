using CapaEntidad;
using CapaNegocios;
using DevExpress.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIANWEB
{
    public partial class CapAlertaAutorizacionGPMaDet : System.Web.UI.Page
    {
        #region Variables
        public DataTable dt
        {
            get
            {
                return (DataTable)Session["dtPedidoVI" + Session.SessionID];
            }
            set
            {
            }
        }

        public DataTable dt_Resto
        {
            get
            {
                return (DataTable)Session["dtPedidoVI_Resto" + Session.SessionID];
            }
            set
            {

            }
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

        private string Emp_CnxCentral
        {
            get { return ConfigurationManager.AppSettings.Get("strConnectionCentral"); }
        }

        #endregion

        #region Eventos

        protected void Page_Init(object sender, EventArgs e)
        {
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
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
   
                            List<string> Productos = (List<string>)Session["ProdsAutorizacion" + Session.SessionID];

                            List<AlertaAutorizacion> listaAutorizacionPrecios = new List<AlertaAutorizacion>();

                            listaAutorizacionPrecios = (List<AlertaAutorizacion>)Session["lAurizacionPrecios" + Session.SessionID];

                            txtUsuario.Value = Sesion.Id_U.ToString();
                            txtId_Cd.Value = Sesion.Id_Cd_Ver.ToString();


                        }
                        //if (Request.QueryString["Orden"] != null)
                        //{
                        //    CargarDatosEncabezado(Convert.ToInt32(Request.QueryString["Orden"]));
                        //}
                        //else if (Request.QueryString["Id_Acs"] != null && Request.QueryString["cte"] != null)
                        //{
                        //    CargarDatosEncabezado(Convert.ToInt32(Request.QueryString["Id_Acs"]));
                        //}
                        //else
                        //{
                        //    mensaje("No se ha especificado el Id de la solicitud de autorización de precios.");
                        //}

                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                     
                }
                catch (Exception ex)
                {
                    mensaje(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                }
            }
        }


        //private void CargarDatosEncabezado(int id)
        //{
        //    // Implementar carga de datos del encabezado
        //}

        [WebMethod]
        public static object GetProductos(int id_cte,int idAutorizacionPrecio)
        {
            try
            {
                AlertaAutorizacion alertaautorizacion = new AlertaAutorizacion();
                List<AlertaAutorizacion> listaProductos = new List<AlertaAutorizacion>();
                CN_AlertaAutorizacion cn_alertaautorizacion = new CN_AlertaAutorizacion();

                // Obtener la sesión actual
                Sesion MySesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
                if (MySesion == null)
                {
                    return new { success = false, message = "La sesión ha expirado. Por favor, vuelva a iniciar sesión." };
                }
                const string FECHA_SIN_FILTRO_INICIAL = "2000-01-01 00:00:00";
                const string FECHA_SIN_FILTRO_FINAL = "2050-01-01 00:00:00";
                // Configurar los parámetros de búsqueda
                alertaautorizacion.Id_Emp = MySesion.Id_Emp;
                alertaautorizacion.Id_Cd = MySesion.Id_Cd_Ver;
                alertaautorizacion.IdAutorizacionPrecio = idAutorizacionPrecio;
                alertaautorizacion.Id_ReporteGP = -1;

                alertaautorizacion.Estatus = 1;
                alertaautorizacion.Id_Cte = id_cte;
                alertaautorizacion.TipoAutorizacion = -1;
                alertaautorizacion.IdRepresentante = -1;
                alertaautorizacion.FechaInicial = DateTime.Parse(FECHA_SIN_FILTRO_INICIAL);
                alertaautorizacion.FechaFinal = DateTime.Parse(FECHA_SIN_FILTRO_FINAL); 
                
                // Obtener la conexión central desde una instancia de la clase
                CapAlertaAutorizacionGPMaDet instance = new CapAlertaAutorizacionGPMaDet();
                string empCnxCentral = instance.Emp_CnxCentral;

                // Llamar al método de la capa de negocio
                cn_alertaautorizacion.AlertaPrecioConsultaGPMaDetalle(alertaautorizacion, ref listaProductos, "PAUTGTEDET", empCnxCentral);

                if (listaProductos.Count == 0)
                {
                    return new { success = true, data = new List<AlertaAutorizacion>(), message = "No se encontraron productos." };
                }
                else
                    {
                    int solicitud = listaProductos[0].IdAutorizacionPrecio;
                    // Convertir la lista de productos a un formato adecuado para el cliente
                    foreach (var producto in listaProductos)
                    {
                        producto.Prd_Descripcion = WebUtility.HtmlEncode(producto.Prd_Descripcion);
                    }
                }

                return new { success = true, data = listaProductos };
            }
            catch (Exception ex)
            {
                return new { success = false, message = "Error al cargar productos: " + ex.Message };
            }
        }

        [WebMethod]
        public static object GetDatosEncabezado(int id_cte, int idAutorizacionPrecio)
        {
            try
            {
                AlertaAutorizacion alertaautorizacion = new AlertaAutorizacion();
                CN_AlertaAutorizacion cn_alertaautorizacion = new CN_AlertaAutorizacion();
                List<AlertaAutorizacion> datosEncabezado = new List<AlertaAutorizacion>();

                // Obtener la sesión actual
                Sesion MySesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
                if (MySesion == null)
                {
                    return new { success = false, message = "La sesión ha expirado. Por favor, vuelva a iniciar sesión." };
                }


                // Configurar los parámetros de búsqueda
                alertaautorizacion.Id_Emp = MySesion.Id_Emp;
                alertaautorizacion.Id_Cd = MySesion.Id_Cd_Ver;
                alertaautorizacion.IdAutorizacionPrecio = idAutorizacionPrecio;
                alertaautorizacion.Id_ReporteGP = -1;
               
                alertaautorizacion.Estatus = 1;
                alertaautorizacion.Id_Cte = id_cte;
                alertaautorizacion.TipoAutorizacion = -1;
                alertaautorizacion.IdRepresentante = -1;
                alertaautorizacion.Id_Cte = id_cte;    
                alertaautorizacion.FechaInicial = DateTime.Parse("2000-01-01 00:00:00");
                alertaautorizacion.FechaFinal = DateTime.Parse("2050-01-01 00:00:00");
                // Obtener los datos del encabezado
                //AlertaAutorizacion datosEncabezado = cn_alertaautorizacion.ObtenerDatosEncabezado(alertaautorizacion);
                

                // Obtener la conexión central desde una instancia de la clase
                CapAlertaAutorizacionGPMaDet instance = new CapAlertaAutorizacionGPMaDet();
                string empCnxCentral = instance.Emp_CnxCentral;

                // Llamar al método de la capa de negocio
                cn_alertaautorizacion.AlertaPrecioConsultaGPMaDetalle(alertaautorizacion, ref datosEncabezado, "Encabezado", empCnxCentral);



                #region Uafir 


                /*Obtiene Información de Periodos*/

                String PeriodoMes1 = "";
                String PeriodoMes2 = "";
                String PeriodoMes3 = "";
                String Cero = "";
                String NombreMes = "";

                String Anio = "";
                String Mes = "";


                String Mes1 = "";
                String Mes2 = "";
                String Mes3 = "";

                String Anio1 = "";
                String Anio2 = "";
                String Anio3 = "";

                //nuevas declaraciones 
                String MesesConsiderados = "";
                String cmbPeriodovalue = "Trimestral anterior";
                String cmbVentasValue = "Integral";


                //todo jfcv
                //JFCV quitar esta parte ya cuando lo vaya a liberar a en operación
                String cmbPeriodoCierre = "202102";
                Anio = "2021";
                Mes = DateTime.Now.Month.ToString();


                if (Mes == "1") { NombreMes = "Enero"; }
                if (Mes == "2") { NombreMes = "Febrero"; }
                if (Mes == "3") { NombreMes = "Marzo"; }
                if (Mes == "4") { NombreMes = "Abril"; }
                if (Mes == "5") { NombreMes = "Mayo"; }
                if (Mes == "6") { NombreMes = "Junio"; }
                if (Mes == "7") { NombreMes = "Julio"; }
                if (Mes == "8") { NombreMes = "Agosto"; }
                if (Mes == "9") { NombreMes = "Septiembre"; }
                if (Mes == "10") { NombreMes = "Octubre"; }
                if (Mes == "11") { NombreMes = "Noviembre"; }
                if (Mes == "12") { NombreMes = "Diciembre"; }

                PeriodoMes1 = NombreMes + "-" + Anio;
                Mes1 = Mes;
                Anio1 = Anio;



                if ((Convert.ToInt32(cmbPeriodoCierre.ToString().Substring(4, 2).ToString()) - 1) == 0)
                {
                    Anio = (Convert.ToInt32(cmbPeriodoCierre.Substring(0, 4).ToString()) - 1).ToString().Trim();
                    Mes = "12";
                }
                else
                {
                    Anio = cmbPeriodoCierre.Substring(0, 4).ToString();
                    Mes = (Convert.ToInt32(cmbPeriodoCierre.Substring(4, 2).ToString()) - 1).ToString().Trim();
                }

                if (Mes == "1") { NombreMes = "Enero"; }
                if (Mes == "2") { NombreMes = "Febrero"; }
                if (Mes == "3") { NombreMes = "Marzo"; }
                if (Mes == "4") { NombreMes = "Abril"; }
                if (Mes == "5") { NombreMes = "Mayo"; }
                if (Mes == "6") { NombreMes = "Junio"; }
                if (Mes == "7") { NombreMes = "Julio"; }
                if (Mes == "8") { NombreMes = "Agosto"; }
                if (Mes == "9") { NombreMes = "Septiembre"; }
                if (Mes == "10") { NombreMes = "Octubre"; }
                if (Mes == "11") { NombreMes = "Noviembre"; }
                if (Mes == "12") { NombreMes = "Diciembre"; }

                PeriodoMes2 = NombreMes + "-" + Anio;
                Mes2 = Mes;
                Anio2 = Anio;

                if (Mes == "1") { NombreMes = "Enero"; }
                if (Mes == "2") { NombreMes = "Febrero"; }
                if (Mes == "3") { NombreMes = "Marzo"; }
                if (Mes == "4") { NombreMes = "Abril"; }
                if (Mes == "5") { NombreMes = "Mayo"; }
                if (Mes == "6") { NombreMes = "Junio"; }
                if (Mes == "7") { NombreMes = "Julio"; }
                if (Mes == "8") { NombreMes = "Agosto"; }
                if (Mes == "9") { NombreMes = "Septiembre"; }
                if (Mes == "10") { NombreMes = "Octubre"; }
                if (Mes == "11") { NombreMes = "Noviembre"; }
                if (Mes == "12") { NombreMes = "Diciembre"; }

                PeriodoMes3 = NombreMes + "-" + Anio;
                Mes3 = Mes;
                Anio3 = Anio;
                MesesConsiderados = "Periodos considerados : " + PeriodoMes3 + ", " + PeriodoMes2 + " y " + PeriodoMes1; //text

                /*Obtiene Información de Periodos*/


                CN_CatCalendario cn_calenda = new CN_CatCalendario();
                Calendario c = new Calendario();
                cn_calenda.ConsultaCalendarioActual(ref c, MySesion);

                Anio1 = c.Cal_Año.ToString().Trim();
                Mes1 = c.Cal_Mes.ToString().Trim();





                /* aqui if(Mes1 = ) */


                ArrayList ALValorParametrosInternos = new ArrayList();

                CentroDistribucion cd = new CentroDistribucion();
                new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref cd, MySesion.Id_Cd_Ver, MySesion.Id_Emp, MySesion.Emp_Cnx);

                //Obtener datos de encabezado de la Valuación de proyecto
                 
                string Cte_NomComercial = string.Empty;
                string  Vap_Nota = string.Empty;

                




                Cte_NomComercial = datosEncabezado[0].Cte_NomComercial;  
                int? territorio = null;
                territorio = datosEncabezado[0].Id_Ter;

                //if (txtTerritorio.Text != "")
                //{
                //    territorio = Convert.ToInt32(txtTerritorio.Text);
                //}
                //else
                //{
                //    territorio = 30302011;
                //}

                //todo jfcv
                //JFCV quitar esta parte ya cuando lo vaya a liberar a en operación
                //Id_Cte = "25249";
                //territorio = 30302011;
                //territorio = -1;
                //txtCliente.Value = 25249;



                DataTable dtReporteTotales = new DataTable();

                
                    cmbPeriodovalue = "Semestral anterior";

                    new CN_ReporteGAP().ReporteRentabilidad_ConsultarTotales(
                                            MySesion.Id_Emp
                                            , MySesion.Id_Cd_Ver
                                            , id_cte
                                            , territorio
                                            , cmbPeriodovalue
                                            , cmbVentasValue
                                            , ref dtReporteTotales
                                            , Convert.ToString(datosEncabezado[0].Id_ReporteGP)
                                            , MySesion.Emp_Cnx);
                
               
                ALValorParametrosInternos.Add(MySesion.Id_Emp);
                ALValorParametrosInternos.Add(MySesion.Emp_Nombre);
                ALValorParametrosInternos.Add(MySesion.Id_Cd_Ver);
                ALValorParametrosInternos.Add(MySesion.Cd_Nombre);
                ALValorParametrosInternos.Add(id_cte);
                ALValorParametrosInternos.Add(Cte_NomComercial);
                ALValorParametrosInternos.Add(territorio);
                ALValorParametrosInternos.Add(territorio != 0 ? territorio : -1);
                ALValorParametrosInternos.Add(cmbPeriodovalue);
                ALValorParametrosInternos.Add(cmbVentasValue);
                ALValorParametrosInternos.Add(DateTime.Now.ToString("dd/MM/yyyy"));
                ALValorParametrosInternos.Add(DateTime.Now.ToString("hh:MM:ss"));
                ALValorParametrosInternos.Add(MySesion.Emp_Cnx);

                double VentaNeta = Convert.ToDouble(dtReporteTotales.Rows[0]["VentaNeta"]);
                double VentaNetaPapel = Convert.ToDouble(dtReporteTotales.Rows[0]["VentaNetaPapel"]);
                double VentaNetaOtros = Convert.ToDouble(dtReporteTotales.Rows[0]["VentaNetaOtros"]);
                double CostoMaterial = Convert.ToDouble(dtReporteTotales.Rows[0]["CostoMaterial"]);
                double CostoMaterialNOPapel = Convert.ToDouble(dtReporteTotales.Rows[0]["CostoMaterialNOPapel"]);
                double FactorFijos = 0;
                double FactorUCS = 0;


                Territorios CatTer = new Territorios();
                CatTer.Id_Emp = MySesion.Id_Emp;
                CatTer.Id_Cd = MySesion.Id_Cd;
                CatTer.Id_Ter = Convert.ToInt32(territorio);
                new CN_CatTerritorios().ConsultaTerritorios(ref CatTer, MySesion.Emp_Cnx);


                if (VentaNeta < 5000) FactorFijos = 17.5;
                if (VentaNeta >= 5000 && VentaNeta < 10000) FactorFijos = 16.84;
                if (VentaNeta >= 10000 && VentaNeta < 15000) FactorFijos = 16.18;
                if (VentaNeta >= 15000 && VentaNeta < 20000) FactorFijos = 15.53;
                if (VentaNeta >= 20000 && VentaNeta < 25000) FactorFijos = 14.87;
                if (VentaNeta >= 25000 && VentaNeta < 30000) FactorFijos = 14.21;
                if (VentaNeta >= 30000 && VentaNeta < 35000) FactorFijos = 13.55;
                if (VentaNeta >= 35000 && VentaNeta < 40000) FactorFijos = 12.89;
                if (VentaNeta >= 40000 && VentaNeta < 45000) FactorFijos = 12.24;
                if (VentaNeta >= 45000 && VentaNeta < 50000) FactorFijos = 11.58;
                if (VentaNeta >= 50000 && VentaNeta < 55000) FactorFijos = 10.92;
                if (VentaNeta >= 55000 && VentaNeta < 60000) FactorFijos = 10.26;
                if (VentaNeta >= 60000 && VentaNeta < 65000) FactorFijos = 9.61;
                if (VentaNeta >= 65000 && VentaNeta < 70000) FactorFijos = 8.95;
                if (VentaNeta >= 70000 && VentaNeta < 75000) FactorFijos = 8.29;
                if (VentaNeta >= 75000 && VentaNeta < 80000) FactorFijos = 7.63;
                if (VentaNeta >= 80000 && VentaNeta < 85000) FactorFijos = 6.97;
                if (VentaNeta >= 85000 && VentaNeta < 90000) FactorFijos = 6.32;
                if (VentaNeta >= 90000 && VentaNeta < 100000) FactorFijos = 5.66;
                if (VentaNeta >= 100000) FactorFijos = 5.0;


                if (VentaNeta < 5000) FactorUCS = 3.5;
                if (VentaNeta >= 5000 && VentaNeta < 10000) FactorUCS = 3.0;
                if (VentaNeta >= 10000 && VentaNeta < 25000) FactorUCS = 2.5;
                if (VentaNeta >= 25000 && VentaNeta < 50000) FactorUCS = 2;
                if (VentaNeta >= 50000 && VentaNeta < 100000) FactorUCS = 1.5;
                if (VentaNeta >= 100000) FactorUCS = 1;


                double AmortizacionTotal = Convert.ToDouble(dtReporteTotales.Rows[0]["AmortizacionTotal"]);
                double Prd_PesConTecnico = Convert.ToDouble(dtReporteTotales.Rows[0]["Prd_PesConTecnico"]);
                double UtilidadBruta = Convert.ToDouble(dtReporteTotales.Rows[0]["UtilidadBruta"]);

                double Cte_CarMP = Convert.ToDouble(dtReporteTotales.Rows[0]["Cte_CarMP"]);
                double Cte_GasVarT = Convert.ToDouble(dtReporteTotales.Rows[0]["Cte_GasVarT"]);
                double Cte_FletePaga = 0; //Convert.ToDouble(dtReporteTotales.Rows[0]["Cte_FletePaga"]);
                double DiasRotacion = Convert.ToDouble(dtReporteTotales.Rows[0]["DiasRotacion"]);

                string listaClavesProd = dtReporteTotales.Rows[0]["Id_PrdStr"].ToString();

                // ------------------------------------------------
                // calcular amortizacion de cada producto
                // ------------------------------------------------
                if (listaClavesProd.Length > 0) listaClavesProd = listaClavesProd.Substring(0, listaClavesProd.Length - 1);
                string[] arrayId_Prd = listaClavesProd.Split(new char[] { ',' });
                //objeto amortizacion
                Amortizacion amortizacion = new Amortizacion();
                amortizacion.Id_Emp = MySesion.Id_Emp;
                amortizacion.Id_Cd = MySesion.Id_Cd_Ver;
                amortizacion.Id_Cte = Convert.ToInt32(id_cte);
                amortizacion.Id_Ter = Convert.ToInt32(territorio);

                //obtener productos con amortización del producto
                int anioActual = DateTime.Now.Year;
                int mesActual = DateTime.Now.Month;


                //calcular financiamiento de proveedores
                double financiamientoProveedores = (((((CostoMaterial / 30) * cd.Cd_Dias.Value) / cd.Cd_Dias.Value) * cd.Cd_DiasFinanciaProv) * (1 + (Convert.ToSingle(cd.Cd_Iva) / 100)));
                if (double.IsNaN(financiamientoProveedores) || double.IsInfinity(financiamientoProveedores))
                {
                    financiamientoProveedores = 0;
                }


                double TotalInversionComodatos = 0;
                new CN_Amortizacion().ConsultaInversionComodato(MySesion.Id_Emp, MySesion.Id_Cd_Ver, Convert.ToInt32(territorio), id_cte, MySesion.Emp_Cnx, ref TotalInversionComodatos);

                //calcular inversion total en activos
                double inversionTotalActivos
                    = (((VentaNeta / 30) * DiasRotacion) * (1 + (Convert.ToSingle(cd.Cd_Iva) / 100)))
                    + ((CostoMaterial / 30) * cd.Cd_Dias.Value)
                    + ((CostoMaterial / 30) * cd.Cd_DiasInv.Value)
                    //+ (TotalInversionComodatos * Convert.ToSingle(cd.Cd_FactorInvComodato))
                    + ((VentaNeta / 30) * cd.Cd_FactorConvActFijo.Value);
                if (double.IsNaN(inversionTotalActivos) || double.IsInfinity(inversionTotalActivos))
                {
                    inversionTotalActivos = 0;
                }

                //calcular utilidad bruta
                UtilidadBruta =
                    VentaNeta
                    - CostoMaterial
                    - Cte_CarMP
                    - ( CostoMaterialNOPapel * (Convert.ToSingle(cd.Cd_Flete) / 100)) //flete
                    - AmortizacionTotal
                    - Prd_PesConTecnico;
                if (double.IsNaN(UtilidadBruta) || double.IsInfinity(UtilidadBruta))
                {
                    UtilidadBruta = 0;
                }

                double UtilidadMarginal = 0;

                //calcular utilidad marginal
                if (CatTer.Id_TipoRepresentante == 4)
                {
                    UtilidadMarginal =
                        UtilidadBruta
                        - (UtilidadBruta * 10 / 100)
                        - Cte_GasVarT
                        //- (VentaNeta * (Convert.ToSingle(cd.Cd_OtrosGastosVar) / 100))
                        - Cte_FletePaga;
                }
                else
                {
                    UtilidadMarginal =
                        UtilidadBruta
                        - (UtilidadBruta * (Convert.ToSingle(cd.Cd_ComisionRik) / 100))
                        - Cte_GasVarT
                        //- (VentaNeta * (Convert.ToSingle(cd.Cd_OtrosGastosVar) / 100))
                        - Cte_FletePaga;
                }




                if (double.IsNaN(UtilidadMarginal) || double.IsInfinity(UtilidadMarginal))
                {
                    UtilidadMarginal = 0;
                }

                //calcular Uafir mensual
                double UafirMensual =
                    UtilidadMarginal
                    - (VentaNeta * (Convert.ToSingle(FactorFijos) / 100))
                    - (VentaNeta * (Convert.ToSingle(FactorUCS) / 100));
                if (double.IsNaN(UafirMensual) || double.IsInfinity(UafirMensual))
                {
                    UafirMensual = 0;
                }

                //calcular Costo de capital
                double CostoCapital = (Math.Round(inversionTotalActivos, 2) - financiamientoProveedores) * (cd.Cd_TasaCetes.Value + cd.Cd_TasaIncCostoCapital.Value) / 100;
                if (double.IsNaN(CostoCapital) || double.IsInfinity(CostoCapital))
                {
                    CostoCapital = 0;
                }
                //calcular Uafir después de impuestos
                double UafirDespuesImpuestos = (UafirMensual * 12) - ((UafirMensual * 12) * (Convert.ToSingle(cd.Cd_ISRyPTU) / 100));
                if (double.IsNaN(UafirDespuesImpuestos) || double.IsInfinity(UafirDespuesImpuestos))
                {
                    UafirDespuesImpuestos = 0;
                }


                //calcular porcentaje de utilidad remanente
                double UtilidadRemanentePorc = (UafirDespuesImpuestos / (inversionTotalActivos - financiamientoProveedores) * 100) - (cd.Cd_TasaCetes.Value + cd.Cd_TasaIncCostoCapital.Value);
                if (double.IsNaN(UtilidadRemanentePorc) || double.IsInfinity(UtilidadRemanentePorc))
                {
                    UtilidadRemanentePorc = 0;
                }

                ALValorParametrosInternos.Add(DiasRotacion); //txtCtaCobrarPorc
                ALValorParametrosInternos.Add(cd.Cd_Dias); //"txtInvDiasCant"
                ALValorParametrosInternos.Add(cd.Cd_DiasInv); //"txtInvConsigDiasCant"
                ALValorParametrosInternos.Add(UtilidadRemanentePorc / 100); //"txtUtilidadRemanentePorc"

                double ctaPorCobrar = ((VentaNeta / 30) * DiasRotacion) * (1 + (Convert.ToSingle(cd.Cd_Iva) / 100));
                if (double.IsNaN(ctaPorCobrar) || double.IsInfinity(ctaPorCobrar))
                {
                    ctaPorCobrar = 0;
                }
                ALValorParametrosInternos.Add(ctaPorCobrar); //"txtCtaCobrar"

                ALValorParametrosInternos.Add((CostoMaterial / 30) * cd.Cd_Dias); //"txtInvDias"
                ALValorParametrosInternos.Add(cd.Cd_FactorInvComodato); //"txtInvComodatoOtrosProdCant"
                ALValorParametrosInternos.Add(TotalInversionComodatos * Convert.ToSingle(cd.Cd_FactorInvComodato)); //"txtInvComodatoOtrosProd"
                ALValorParametrosInternos.Add((CostoMaterial / 30) * cd.Cd_DiasInv); //"txtInvConsigDias"

                ALValorParametrosInternos.Add(financiamientoProveedores); //"txtFinanProv"

                ALValorParametrosInternos.Add(inversionTotalActivos - financiamientoProveedores); //"txtInvActivosNetos" OP'N
                ALValorParametrosInternos.Add(inversionTotalActivos); //"txtInvTotalActivos"
                ALValorParametrosInternos.Add((VentaNeta / 30) * cd.Cd_FactorConvActFijo); //"txtInvActivosFijos"
                ALValorParametrosInternos.Add(UtilidadRemanentePorc / 100 * (inversionTotalActivos - financiamientoProveedores)); //"txtUtilidadRemanente"

                double txtUafirActivos = UafirDespuesImpuestos / (inversionTotalActivos - financiamientoProveedores) * 100;
                if (double.IsNaN(txtUafirActivos) || double.IsInfinity(txtUafirActivos))
                {
                    txtUafirActivos = 0;
                }
                ALValorParametrosInternos.Add(txtUafirActivos / 100); //"txtUafirActivos"

                ALValorParametrosInternos.Add(Convert.ToSingle(cd.Cd_TasaCetes + cd.Cd_TasaIncCostoCapital) / 100); //"txtCostoCapital"
                ALValorParametrosInternos.Add(VentaNeta); //"txtVentaNetaMon"
                ALValorParametrosInternos.Add(CostoMaterial); //"txtCostoMaterialMon"
                ALValorParametrosInternos.Add(/*CostoMaterial*/CostoMaterialNOPapel * (Convert.ToSingle(cd.Cd_Flete) / 100)); //"txtFleteMon"
                ALValorParametrosInternos.Add(Cte_CarMP); //"txtManoObraMon"

                ALValorParametrosInternos.Add(UtilidadBruta); //"txtUtilidadMon"
                ALValorParametrosInternos.Add(Prd_PesConTecnico); //"txtCostoServEquipoMon"
                ALValorParametrosInternos.Add(AmortizacionTotal); //"txtAmortizacionMon"
                if (CatTer.Id_TipoRepresentante == 4)
                {
                    ALValorParametrosInternos.Add((UtilidadBruta * 10 / 100)); //"txtComisionRepMon"
                }
                else
                {
                    ALValorParametrosInternos.Add(UtilidadBruta * (Convert.ToSingle(cd.Cd_ComisionRik) / 100)); //"txtComisionRepMon"
                }

                ALValorParametrosInternos.Add(VentaNetaOtros * (Convert.ToSingle(cd.Cd_ContribucionGastosFijosOtros) / 100)); //"txtContribucionGastosFijosOtrosMon"
                ALValorParametrosInternos.Add(VentaNetaPapel * (Convert.ToSingle(cd.Cd_ContribucionGastosFijosPapel) / 100)); //"txtContribucionGastosFijosPapelMon"
                ALValorParametrosInternos.Add(UafirMensual); //"txtUafirMensualMon"
                ALValorParametrosInternos.Add(VentaNeta * (Convert.ToSingle(cd.Cd_CargoUCS) / 100)); //"txtCargoUCSMon"
                ALValorParametrosInternos.Add(Cte_FletePaga); //"txtFletesPagadosMon"
                ALValorParametrosInternos.Add(0); //"txtOtrosGastosVariablesMon"  VentaNeta * (Convert.ToSingle(cd.Cd_OtrosGastosVar) / 100)
                ALValorParametrosInternos.Add(0); //"txtGastosVariablesMon" Cte_GasVarT
                ALValorParametrosInternos.Add(UafirMensual * 12); //"txtUafirAnualMon"

                ALValorParametrosInternos.Add(CostoCapital); //"txtCostoCapitalMon"

                ALValorParametrosInternos.Add(UafirDespuesImpuestos - CostoCapital); //"txtUtilidadRemanenteMon"
                ALValorParametrosInternos.Add(UafirDespuesImpuestos); //"txtUafirDespuesImpMon"
                ALValorParametrosInternos.Add(cd.Cd_ISRyPTU); //"txtISRyPTU"
                double txtISRyPTUMon = (UafirMensual * 12) * (Convert.ToSingle(cd.Cd_ISRyPTU) / 100);
                if (double.IsNaN(txtISRyPTUMon) || double.IsInfinity(txtISRyPTUMon))
                {
                    txtISRyPTUMon = 0;
                }
                ALValorParametrosInternos.Add(txtISRyPTUMon); //"txtISRyPTUMon"

                //ALValorParametrosInternos.Add(-1); //"txtISRyPTUPorc"
                //ALValorParametrosInternos.Add(-1); //"txtUafirDespuesImpPorc"
                //ALValorParametrosInternos.Add(-1); //"txtUtilidadRemanentePorc2"
                //ALValorParametrosInternos.Add(-1); //"txtCostoCapitalPorc"
                //ALValorParametrosInternos.Add(-1); //"txtUafirAnualPorc"
                double txtGastosVariablesPorc = (Cte_GasVarT / VentaNeta) * 100;
                if (double.IsNaN(txtGastosVariablesPorc) || double.IsInfinity(txtGastosVariablesPorc))
                {
                    txtGastosVariablesPorc = 0;
                }
                ALValorParametrosInternos.Add(txtGastosVariablesPorc / 100); //"txtGastosVariablesPorc"
                double txtOtrosGastosVariablesPorc = 0; //((VentaNeta * (Convert.ToSingle(cd.Cd_OtrosGastosVar) / 100)) / VentaNeta) * 100;
                if (double.IsNaN(txtOtrosGastosVariablesPorc) || double.IsInfinity(txtOtrosGastosVariablesPorc))
                {
                    txtOtrosGastosVariablesPorc = 0;
                }
                ALValorParametrosInternos.Add(0); //"txtOtrosGastosVariablesPorc"  txtOtrosGastosVariablesPorc / 100)
                double txtFletesPagadosPorc = (Cte_FletePaga / VentaNeta) * 100;
                if (double.IsNaN(txtFletesPagadosPorc) || double.IsInfinity(txtFletesPagadosPorc))
                {
                    txtFletesPagadosPorc = 0;
                }
                ALValorParametrosInternos.Add(txtFletesPagadosPorc / 100); //"txtFletesPagadosPorc"
                double txtCargoUCSPorc = ((VentaNeta * (Convert.ToSingle(cd.Cd_CargoUCS) / 100)) / VentaNeta) * 100;
                if (double.IsNaN(txtCargoUCSPorc) || double.IsInfinity(txtCargoUCSPorc))
                {
                    txtCargoUCSPorc = 0;
                }
                ALValorParametrosInternos.Add(txtCargoUCSPorc / 100); //"txtCargoUCSPorc"
                double txtUafirMensualPorc = (UafirMensual / VentaNeta) * 100;
                if (double.IsNaN(txtUafirMensualPorc) || double.IsInfinity(txtUafirMensualPorc))
                {
                    txtUafirMensualPorc = 0;
                }
                ALValorParametrosInternos.Add(txtUafirMensualPorc / 100); //"txtUafirMensualPorc"
                double txtContribucionGastosFijosPapelPorc = (/*(VentaNetaPapel * (Convert.ToSingle(cd.Cd_ContribucionGastosFijosPapel) / 100))*/(VentaNeta * (Convert.ToSingle(FactorFijos) / 100)) / VentaNeta) * 100;
                if (double.IsNaN(txtContribucionGastosFijosPapelPorc) || double.IsInfinity(txtContribucionGastosFijosPapelPorc))
                {
                    txtContribucionGastosFijosPapelPorc = 0;
                }
                ALValorParametrosInternos.Add(txtContribucionGastosFijosPapelPorc / 100); //"txtContribucionGastosFijosPapelPorc"
                double txtContribucionGastosFijosOtrosPorc = ((VentaNetaOtros * (Convert.ToSingle(cd.Cd_ContribucionGastosFijosOtros) / 100)) / VentaNeta) * 100;
                if (double.IsNaN(txtContribucionGastosFijosOtrosPorc) || double.IsInfinity(txtContribucionGastosFijosOtrosPorc))
                {
                    txtContribucionGastosFijosOtrosPorc = 0;
                }
                ALValorParametrosInternos.Add(txtContribucionGastosFijosOtrosPorc / 100); //"txtContribucionGastosFijosOtrosPorc"
                double txtAmortizacionPorc = (AmortizacionTotal / VentaNeta) * 100;
                if (double.IsNaN(txtAmortizacionPorc) || double.IsInfinity(txtAmortizacionPorc))
                {
                    txtAmortizacionPorc = 0;
                }
                ALValorParametrosInternos.Add(txtAmortizacionPorc / 100); //"txtAmortizacionPorc"
                double txtCostoServEquipoPorc = (Prd_PesConTecnico / VentaNeta) * 100;
                if (double.IsNaN(txtCostoServEquipoPorc) || double.IsInfinity(txtCostoServEquipoPorc))
                {
                    txtCostoServEquipoPorc = 0;
                }
                ALValorParametrosInternos.Add(txtCostoServEquipoPorc / 100); //"txtCostoServEquipoPorc"

                double txtComisionRepPorc = 0;

                if (CatTer.Id_TipoRepresentante == 4)
                {
                    txtComisionRepPorc = ((UtilidadBruta * 10 / 100) / VentaNeta) * 100;
                }
                else
                {
                    txtComisionRepPorc = ((UtilidadBruta * (Convert.ToSingle(cd.Cd_ComisionRik) / 100)) / VentaNeta) * 100;
                }

                if (double.IsNaN(txtComisionRepPorc) || double.IsInfinity(txtComisionRepPorc))
                {
                    txtComisionRepPorc = 0;
                }
                ALValorParametrosInternos.Add(txtComisionRepPorc / 100); //"txtComisionRepPorc"
                double txtUtilidadPorc = (UtilidadBruta / VentaNeta) * 100;
                if (double.IsNaN(txtUtilidadPorc) || double.IsInfinity(txtUtilidadPorc))
                {
                    txtUtilidadPorc = 0;
                }
                ALValorParametrosInternos.Add(txtUtilidadPorc / 100); //"txtUtilidadPorc"
                double txtManoObraPorc = (Cte_CarMP / VentaNeta) * 100;
                if (double.IsNaN(txtManoObraPorc) || double.IsInfinity(txtManoObraPorc))
                {
                    txtManoObraPorc = 0;
                }
                ALValorParametrosInternos.Add(txtManoObraPorc / 100); //"txtManoObraPorc"

                ALValorParametrosInternos.Add(cd.Cd_Flete); //"txtFletePorc"

                double txtFletePorc2 = ((/*CostoMaterial*/CostoMaterialNOPapel * (Convert.ToSingle(cd.Cd_Flete) / 100)) / VentaNeta) * 100;
                if (double.IsNaN(txtFletePorc2) || double.IsInfinity(txtFletePorc2))
                {
                    txtFletePorc2 = 0;
                }
                ALValorParametrosInternos.Add(txtFletePorc2 / 100); //"txtFletePorc2"
                double txtCostoMaterialPorc = (CostoMaterial / VentaNeta) * 100;
                if (double.IsNaN(txtCostoMaterialPorc) || double.IsInfinity(txtCostoMaterialPorc))
                {
                    txtCostoMaterialPorc = 0;
                }
                ALValorParametrosInternos.Add(txtCostoMaterialPorc / 100); //"txtCostoMaterialPorc"


                ALValorParametrosInternos.Add(UtilidadMarginal); //"txtUtilidadMarginalMon"
                double txtUtilidadMarginalPorc = (UtilidadMarginal / VentaNeta) * 100;
                if (double.IsNaN(txtUtilidadMarginalPorc) || double.IsInfinity(txtUtilidadMarginalPorc))
                {
                    txtUtilidadMarginalPorc = 0;
                }
                ALValorParametrosInternos.Add(txtUtilidadMarginalPorc / 100); //"txtUtilidadMarginalPorc"
 
            
                        #endregion
                if (datosEncabezado != null)
                {
                    double porcUafir = VentaNeta != 0
                        ? Math.Round((UafirMensual / VentaNeta) * 100, 2)
                        : 0;
                    return new
                    {
                        success = true,
                        data = new
                        {
                            Cliente = datosEncabezado[0].Id_Cte,
                            ClienteNombre = datosEncabezado[0].Cte_NomComercial,
                            Territorio = datosEncabezado[0].Id_Ter,
                            IdAutorizacionPrecio = datosEncabezado[0].IdAutorizacionPrecio,
                            Id_Cd = datosEncabezado[0].Id_Cd,
                            FechaSolicitud = datosEncabezado[0].FechaSolicitud,
                            TipoAutorizacion = datosEncabezado[0].TipoAutorizacion,
                            NomTipoSolicitud = datosEncabezado[0].NomTipoSolicitud,
                            NomEstatus = datosEncabezado[0].NomEstatus,
                            Tipo_Cliente=datosEncabezado[0].Tipo_Cliente,
                            Id_Tamaño = datosEncabezado[0].Id_Tamaño,
                            Ventas = double.IsInfinity(VentaNeta) ? 0 : Math.Round(VentaNeta, 2),
                            UtilidadPrima = double.IsInfinity(VentaNeta - CostoMaterial ) ? 0
                            : Math.Round(VentaNeta - CostoMaterial , 2),
                            PorcUtilidadPrima = double.IsInfinity(VentaNeta) ? 0
                            : (Math.Round(VentaNeta - CostoMaterial, 2)/ VentaNeta )*100,

                            UafirMes = double.IsInfinity(UafirMensual) ? 0 : Math.Round(UafirMensual, 2),
                            UafirAnual = double.IsInfinity(UafirMensual * 12) ? 0 : Math.Round(UafirMensual * 12, 2),
                            PorcUafir = double.IsInfinity(porcUafir) ? 0 : porcUafir,
                            UtilRemanente = double.IsInfinity(UafirDespuesImpuestos - CostoCapital) ? 0
                            : Math.Round(UafirDespuesImpuestos - CostoCapital, 2)
                            ,Nom_CDI = datosEncabezado[0].Nom_CDI

                        }
                    };


                    //txtVentas= "$" + String.Format("{0:N}", VentaNeta),
                    //txtUtilidadPrima = "$" + String.Format("{0:N}", (VentaNeta - CostoMaterial - CostoMaterialNOPapel)),
                    //txtPorcUtilidadPrima  = String.Format("{0:N}", (((VentaNeta - CostoMaterial - CostoMaterialNOPapel) / VentaNeta) * 100)) + "%",
                    //txtUafirmes = " $" + String.Format("{0:N}", UafirMensual),
                    //txtPorcUafirCte = String.Format("{0:N}", ((UafirMensual / VentaNeta) * 100)) + "%",
                    //txtUafirAnual = "$" + String.Format("{0:N}", (UafirMensual * 12)),

                    //txtUtilRemanente = "$" + String.Format("{0:N}", (UafirDespuesImpuestos - CostoCapital))

                }
                else
                {
                    return new { success = false, message = "No se encontraron datos del encabezado" };
                }
            }
            catch (Exception ex)
            {
                return new { success = false, message = "Error al obtener datos del encabezado: " + ex.Message };
            }
        }


        [WebMethod]
        public static object AceptarProducto(int id, int id_cte, int id_autorizacion_det, decimal precio_vta, string fecha_vigencia, string justificacion)
        {
            try
            {
                // Obtener la sesión actual
                Sesion MySesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
                if (MySesion == null)
                {
                    return new { success = false, message = "La sesión ha expirado. Por favor, vuelva a iniciar sesión." };
                }

                // Validar los parámetros
                if (string.IsNullOrEmpty(justificacion))
                {
                    return new { success = false, message = "La justificación es requerida" };
                }

                DateTime fechaVigencia;
                if (!DateTime.TryParse(fecha_vigencia, out fechaVigencia))
                {
                    return new { success = false, message = "Formato de fecha inválido" };
                }

                // Llamar al método existente con los parámetros convertidos
                string resultado = AutorizarSolicitud(
                    id_autorizacion_det,
                    MySesion.Id_U,
                    justificacion,
                    Convert.ToDouble(precio_vta),
                    fechaVigencia,
                    MySesion.Id_Cd_Ver,
                    1 // reqau por defecto
                );

                return new { success = true, message = resultado };
            }
            catch (Exception ex)
            {
                return new { success = false, message = "Error al autorizar el producto: " + ex.Message };
            }
        }


        protected void lnkEquivalencia_Click(object sender, EventArgs e)
        {
            GridViewDataItemTemplateContainer c = ((LinkButton)sender).NamingContainer as GridViewDataItemTemplateContainer;
            string id_acs = Request.QueryString["Id_Acs"].ToString();
            string id_cte = Request.QueryString["cte"].ToString();
            string Id_Prd = c.Grid.GetRowValues(c.VisibleIndex, "Id_Prd").ToString();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Equivalencia", "AbrirEquivalencia('" + Id_Prd + "', '" + id_acs + "' , '" + id_cte + "')", true);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                Session["Respuesta" + Session.SessionID] = false;
                string funcion;
                funcion = "CloseWindow()";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                mensaje(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                Session["Respuesta" + Session.SessionID] = true;
                string funcion;
                funcion = "CloseWindow()";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                mensaje(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        #endregion

        #region Funciones

        //protected void btnAutorizarTodo_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
 

        //        // Crear la conexión usando la configuración existente
        //        string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];

        //        // Preparar objeto para actualización
        //        GestionIncrementoPrecios encabezado = new GestionIncrementoPrecios();
   
        //        encabezado.Id_Estatus = 4; // Estado de autorización

        //        // Ejecutar la actualización masiva
        //        int verificador = 0;
        //       // CN_ReporteGAP.AutorizarMasivo(encabezado, Conexion, ref verificador);

        //        // Mostrar mensaje de resultado
        //        string mensaje = verificador > 0
        //            ? "La autorización masiva se realizó correctamente."
        //            : "No se pudo completar la autorización masiva.";

        //        ScriptManager.RegisterStartupScript(this, GetType(), "Resultado",
        //            $"alert('{mensaje}');", true);

        //        // Recargar la página o grid si es necesario
        //        if (verificador > 0)
        //        {
        //            Response.Redirect(Request.RawUrl);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejar el error y mostrar mensaje al usuario
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Error",
        //            $"alert('Error al procesar la autorización masiva: {ex.Message}');", true);
        //    }
        //}
         
 
        protected void btnSalir_Click(object sender, EventArgs e)
        {
            try
            {
                Session["Respuesta" + Session.SessionID] = false;
                string funcion;
                funcion = "CloseWindow()";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                mensaje(new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }

        private DataTable GetListDet()
        {
            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("Id_Prd", System.Type.GetType("System.Int64"));
            dtTemp.Columns.Add("Prd_Descripcion", System.Type.GetType("System.String"));
            dtTemp.Columns.Add("Prd_Cantidad", System.Type.GetType("System.Int32"));
            dtTemp.Columns.Add("Prd_Asignado", System.Type.GetType("System.Int32"));
            dtTemp.Columns.Add("Prd_InvFinal", System.Type.GetType("System.Int32"));
            dtTemp.Columns.Add("Prd_Disponible", System.Type.GetType("System.Int32"));

            CN_CatProducto cn_catproducto = new CN_CatProducto();
            Producto pr = new Producto();
            List<int> actuales;

            foreach (DataRow dr in dt.Rows)
            {

                actuales = new List<int>();
                cn_catproducto.ConsultaProducto_Disponible(session.Id_Emp, session.Id_Cd_Ver, dr["Id_Prd"].ToString(), ref actuales, session.Emp_Cnx);
                if (Convert.ToInt32(dr["Prd_Cantidad"]) > Convert.ToInt32(actuales[2]))
                {
                    dtTemp.Rows.Add(new object[] { dr["Id_Prd"], dr["Prd_Descripcion"], dr["Prd_Cantidad"], actuales[1], actuales[0], actuales[2] });
                }
            }

            if (dt_Resto != null)
            {
                foreach (DataRow dr in dt_Resto.Rows)
                {

                    actuales = new List<int>();
                    cn_catproducto.ConsultaProducto_Disponible(session.Id_Emp, session.Id_Cd_Ver, dr["Id_Prd"].ToString(), ref actuales, session.Emp_Cnx);
                    if (Convert.ToInt32(dr["Prd_Cantidad"]) > Convert.ToInt32(actuales[2]))
                    {
                        dtTemp.Rows.Add(new object[] { dr["Id_Prd"], dr["Prd_Descripcion"], dr["Prd_Cantidad"], actuales[1], actuales[0], actuales[2] });
                    }
                }
            }

            return dtTemp;
        }
        private DataTable GetListPrecio()
        {
            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("Id_Prd", System.Type.GetType("System.Int64"));
            dtTemp.Columns.Add("Prd_Descripcion", System.Type.GetType("System.String"));
            dtTemp.Columns.Add("Precio_Convenido", System.Type.GetType("System.Int32"));
            dtTemp.Columns.Add("Precio_Captado", System.Type.GetType("System.Double"));

            CN_CatProducto cn_catproducto = new CN_CatProducto();
            Producto pr = new Producto();

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["Prd_PrecioAcys"] == DBNull.Value)
                {
                    dr["Prd_PrecioAcys"] = 0;
                }
                if (Convert.ToInt64(dr["Id_PrdOld"]) != -1 && Convert.ToDouble(dr["Prd_Precio"]) != Convert.ToDouble(dr["Prd_PrecioAcys"]))
                {
                    dtTemp.Rows.Add(new object[] { dr["Id_Prd"], dr["Prd_Descripcion"], dr["Prd_PrecioAcys"], dr["Prd_Precio"] });
                }
            }

            if (dt_Resto != null)
            {
                foreach (DataRow dr in dt_Resto.Rows)
                {
                    if (dr["Prd_PrecioAcys"] == DBNull.Value)
                    {
                        dr["Prd_PrecioAcys"] = 0;
                    }
                    if (Convert.ToInt64(dr["Id_Prd"]) != -1 && Convert.ToDouble(dr["Prd_Precio"]) != Convert.ToDouble(dr["Prd_PrecioAcys"]))
                    {
                        dtTemp.Rows.Add(new object[] { dr["Id_Prd"], dr["Prd_Descripcion"], dr["Prd_PrecioAcys"], dr["Prd_Precio"] });
                    }
                }
            }

            return dtTemp;
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string AutorizarFolio(int parametro, string justificacion, string usuario, string Precio_Vta, string fechavigencia, string id_cd, string reqaut)
        {
               
            try
            { 

            int usuarioautoriza = Convert.ToInt32(usuario);
            double precio = Convert.ToDouble(Precio_Vta);
            DateTime fechav = Convert.ToDateTime(fechavigencia);
            int reqau = Convert.ToInt32(reqaut);
            int idcd = Convert.ToInt32(id_cd);

                String resultado = AutorizarSolicitud(parametro, usuarioautoriza, justificacion, precio, fechav, idcd, reqau);
            return resultado;
        }

            catch (Exception ex)
            {
                throw ex;
            }

	}

        public static string AutorizarSolicitud(int folio, int usuarioautoriza, string justificacion, double precio, DateTime fechav, int id_cd, int reqaut)
        {
            try
            {


                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];

                int Verificador = 0;


                CN_AlertaAutorizacion cn_alerta = new CN_AlertaAutorizacion();

                AlertaAutorizacion alerta = new AlertaAutorizacion();

                if (folio != 0)
                {
                    alerta.IdAutorizacionPrecio = Convert.ToInt32(folio);
                    alerta.IdUsuarioGteAutorizo = usuarioautoriza;
                    alerta.Justificacion = justificacion;
                    alerta.Estatus = 88;
                    alerta.FechaVigencia = fechav;
                    alerta.Id_Cd = id_cd;
                    alerta.Precio_Vta = precio;
                    alerta.Req_Aut_Director = reqaut;
                    cn_alerta.AutorizarSolicitudGerente(alerta, ref Verificador, Conexion);


                    string correousuario =   ""; 
                    string nombrecliente = "";
                    string prd_Descripcion = "";
                    string correodireccion = "";
                    Double precio_MinimoRik = 0;
                    Double precioObjetivo = 0;
                    Int64 id_Prd = 0;
                    int req_aut = 0;


                    cn_alerta.ConsultaAlertaCorreo(alerta, ref correousuario, ref correodireccion, ref nombrecliente, ref prd_Descripcion, ref precio_MinimoRik, ref precioObjetivo, ref id_Prd ,ref req_aut, Conexion);

                    alerta.Req_Aut_Director = req_aut;

                    if (correousuario != "")
                    {
                        alerta.Cte_NomComercial = nombrecliente;
                        alerta.Id_Prd = id_Prd;
                        alerta.Prd_Descripcion = prd_Descripcion;
                        alerta.Precio_MinimoRik = precio_MinimoRik;
                        alerta.PrecioObjetivo = precioObjetivo;
                        alerta.Id_Emp = 1;
                        if (correodireccion != "" && alerta.Req_Aut_Director == 1)
                        {
                            correousuario = correousuario + ";" + correodireccion;
                        }

                        var mc = new CapAlertaAutorizacionGPMaDet();

                        string url = (HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath + "/").Replace("//", "/").Replace("http:/", "http://");
                       
                        EnviarCorreo(alerta,    correousuario, 1, url);
                    }

                    

                    //Alerta("La información ha sido guardada.");

                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "Regresar", "Mensajecerrarpantalla('La información ha sido guardada')", true);

                    //
                }
                return "Se Autorizo el folio correctamente.";

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //Cancelar Folio 
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string RechazarFolioGPMA(string parametro, string motivorechazo, string idmotivorechazo, string justificarechazo, string usuario, string idcd)
        {
            int usuarioautoriza = Convert.ToInt32(usuario);
            int id_cd = Convert.ToInt32(idcd);

            String resultado = RechazarFolioGPMA(parametro, usuarioautoriza, motivorechazo, idmotivorechazo, justificarechazo, id_cd);
            return resultado;
        }

        public static string RechazarFolioGPMA(string folio, int usuarioautoriza, string motivorechazo, string idmotivorechazo, string justificarechazo, int id_cd)
        {
            try
            {
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
                string WebURLtempPDF = string.Concat(System.Configuration.ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), "");
                 
                int Verificador = 0;
                CN_AlertaAutorizacion cn_alertas = new CN_AlertaAutorizacion();
                AlertaAutorizacion alerta = new AlertaAutorizacion();
                //conv.PC_NoConvenioKey = this.HFTipoOp.Value == "0" ? int.Parse(this.TxtKeyConvenio.Text) : 0;

                //conv.PC_FechaInicio = this.TxtPC_FechaInicioConv.SelectedDate.Value != null ? this.TxtPC_FechaInicioConv.SelectedDate.Value : DateTime.Now;

                if (folio != "0")
                {
                    alerta.IdAutorizacionPrecio = Convert.ToInt32(folio);
                    alerta.IdUsuarioGteAutorizo = usuarioautoriza;
                    alerta.Justificacion = justificarechazo;
                    alerta.Estatus = 99;
                    alerta.Id_Emp = 1;
                    alerta.Id_Cd = id_cd;
                    //agregar idmotivorechazo
                    alerta.MotivoRechazo = motivorechazo;
                    alerta.Id_MotivoRechazo = Convert.ToInt32(idmotivorechazo);
                    //cancelar solicitud
                    cn_alertas.AutorizarSolicitudGerente(alerta, ref Verificador, Conexion);
                }

                string correousuario = "";
                string nombrecliente = "";
                string prd_Descripcion = "";
                string coreodireccion = "";
                Double precio_MinimoRik = 0;
                Double precioObjetivo = 0;
                Int64 id_Prd = 0;
                int req_aut = 0;


                cn_alertas.ConsultaAlertaCorreo(alerta, ref correousuario, ref coreodireccion, ref nombrecliente, ref prd_Descripcion, ref precio_MinimoRik, ref precioObjetivo, ref id_Prd, ref req_aut,  Conexion);

                alerta.Req_Aut_Director = req_aut;

                if (correousuario != "")
                {
                    alerta.Cte_NomComercial = nombrecliente;
                    alerta.Id_Prd = id_Prd;
                    alerta.Prd_Descripcion = prd_Descripcion;
                    alerta.Precio_MinimoRik = precio_MinimoRik;
                    alerta.PrecioObjetivo = precioObjetivo;
                    alerta.Id_Emp = 1;

                    var mc = new CapAlertaAutorizacionGPMaDet();

                    string url  = (HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath + "/").Replace("//", "/").Replace("http:/", "http://");
                    

                    EnviarCorreo(alerta, correousuario, 0, url);
                }

                if (coreodireccion != "" && alerta.Req_Aut_Director == 1)
                {
                    //EnviarCorreoDireccion();
                }


                return "Se Rechazo el folio # " + folio;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        #endregion

        #region Eventos
        protected void txtPrecio_Vta_ValueChanged(object sender, EventArgs e)
        {
            //ASPxSpinEdit se = (ASPxSpinEdit)sender;
            //ASPxComboBox1.SelectedIndex = Convert.ToInt32(se.Value);
             
            //if (Convert.ToDouble(txtPrecio_Vta.Value) <= 0)
            //{
            //    Alerta("Debe alimentar el precio de venta ingresado:  ");
            //}
            //else
            //{
            //    if (Convert.ToDouble(txtPrecio_Vta.Value) >= Convert.ToDouble(txtPrecio_MinimoGte.Value))
            //    {
            //        chkReq_Aut_Director.Items[0].Selected = false;
            //    }
            //    else
            //    {
            //        chkReq_Aut_Director.Items[0].Selected = true;
            //    }
            //}
        }


        protected void DateFechaVigencia_DateChanged(object sender, CalendarCustomDisabledDateEventArgs e)
        {
            if (e.Date.DayOfWeek == DayOfWeek.Wednesday)
                e.IsDisabled = true;
            //DateTime fechav = DateFechaVigencia.Date;
            //DateTime fechamax = DateFechaVigencia.Date;
             
            //if (Convert.ToInt32(txtId_Cpr.Value) != 23)
            //{
            //    fechamax = DateTime.Now.AddMonths(6);
            //}
            //else
            //{
            //    fechamax = DateTime.Now.AddMonths(12);
            //}
            //if (fechav > fechamax)
            //{
            //    Alerta("La fecha tecleada es mayor a la Fecha máxima de vigencia." + fechamax.ToString());
            //    //DateFechaVigencia.Date = fechamax;

            //}

        }


        public static string EnviarCorreo(AlertaAutorizacion alertaautorizacion,     string correo, int autoriza, string url)
        {
            try
            {
                //obtener la conección de sianweb para poder obtener la configuración 
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnection"];



                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Cd = alertaautorizacion.Id_Cd;
                configuracion.Id_Emp = alertaautorizacion.Id_Emp;   
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                cn_configuracion.Consulta(ref configuracion, Conexion);
                StringBuilder cuerpo_correo = new StringBuilder();
                cuerpo_correo.Append("<html><div align='center'>");
                cuerpo_correo.Append("<table><tr><td>");
                cuerpo_correo.Append("<IMG SRC=\"cid:companylogo\" ALIGN='left'></td>");
                cuerpo_correo.Append("<td></td>");
                cuerpo_correo.Append("</tr><tr><td colspan='2'><br><br><br></td>");
                cuerpo_correo.Append("</tr><tr>");
                cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='2'>");

                if (alertaautorizacion.Req_Aut_Director == 1)
                {
                    cuerpo_correo.Append("Solicitud de precios especiales # " + alertaautorizacion.IdAutorizacionPrecio.ToString());
                    if (autoriza == 1)
                    {
                        cuerpo_correo.Append(" Ha sido Auorizada por el Gerente</td></tr><tr><td> Y requiere ser autorizada por Dirección.</td> </tr>");
                    }
                    else
                    {
                        cuerpo_correo.Append(" Ha sido Rechazada por el Gerente</td></tr><tr><td> Motivo de Rechazo: " + alertaautorizacion.Justificacion + "</td> </tr>");
                    }
                }
                else
                {
                    cuerpo_correo.Append("Su Solicitud de precios especiales # " + alertaautorizacion.IdAutorizacionPrecio.ToString());
                    if (autoriza == 1)
                    {
                        cuerpo_correo.Append(" Ha sido Auorizada por el Gerente</td></tr><tr><td> &nbsp;</td> </tr>");
                    }
                    else
                    {
                        cuerpo_correo.Append(" Ha sido Rechazada por el Gerente</td></tr><tr><td> Motivo de Rechazo: " + alertaautorizacion.Justificacion + "</td> </tr>");
                    }
                }


                cuerpo_correo.Append("<tr>");
                cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='2'>");
                
                cuerpo_correo.Append("</td></tr> <tr><td> &nbsp;</td> </tr>");
                cuerpo_correo.Append("<tr>");
                cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='2'>");
          
                cuerpo_correo.Append("</td></tr> <tr><td> &nbsp;</td> </tr>");
                cuerpo_correo.Append("<tr>");
                cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='2'>");
                //cuerpo_correo.Append("Autorizada por : " + session.U_Nombre);
                cuerpo_correo.Append("</td></tr>");
                cuerpo_correo.Append("<tr><td colspan='2'>");
                cuerpo_correo.Append("<center><br>");
                cuerpo_correo.Append("</td></tr></table></div>");

                cuerpo_correo.Append("<div align='center'>");
                cuerpo_correo.Append("<table><tr>");
                cuerpo_correo.Append("<td>Producto</td><td>Descripción</td><td>Precio Vta<br>Autorizado</td><td>Precio<br>Objetivo</td><td>Precio <br>Min rik</td>");
                cuerpo_correo.Append("</tr><tr><td>" + alertaautorizacion.Id_Prd.ToString() + "</td><td>" + alertaautorizacion.Prd_Descripcion.ToString() + "</td>");
                cuerpo_correo.Append("<td> " + String.Format("{0:###,###,##0.00}", alertaautorizacion.Precio_Vta) + " </td > <td> " + String.Format("{0:###,###,##0.00}", alertaautorizacion.PrecioObjetivo) + " </td><td> " + String.Format("{0:###,###,##0.00}", alertaautorizacion.Precio_MinimoRik) + " </td> ");
                cuerpo_correo.Append("</tr><tr><td colspan='2'><br><br><br></td>");
                cuerpo_correo.Append("</tr><tr>");
                cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='2'>");
                if (alertaautorizacion.Req_Aut_Director == 1)
                {
                    cuerpo_correo.Append("Favor de dar seguimiento a esta solictud del cliente: " +  alertaautorizacion.Cte_NomComercial);
                }
                else
                { 
                    cuerpo_correo.Append("Favor de dar seguimiento al documento que tenga pendiente de este producto con el cliente: " + alertaautorizacion.Cte_NomComercial); 
                }
                cuerpo_correo.Append("</td></tr>");


               

                
                cuerpo_correo.Append("<tr><td><br><center>");
                cuerpo_correo.Append("<a href='" + url + "CapAlertaConsulta.aspx'" + ">");
                cuerpo_correo.Append("Consulta solicitudes de autorización de precios</a></font></center>");
                cuerpo_correo.Append("</td></tr></table></div>");
                cuerpo_correo.Append(" </html>");
 
               



                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                sm.EnableSsl = true;
                MailMessage m = new MailMessage();
                m.From = new MailAddress(configuracion.Mail_Remitente);
                char[] splitchar = { ';' };
                //correo = "francisco.cepeda@Gibraltar.com.mx";
                string[] Correos = correo.Split(splitchar);

                
                //m.Bcc.Add(new MailAddress("francisco.cepeda@gibraltar.com.mx"));
 
                foreach (string mail in Correos)
                {
                    m.To.Add(new MailAddress(mail));
                }

                if (alertaautorizacion.Req_Aut_Director == 1)
                {
                    if (autoriza == 1)
                    {
                        m.Subject = "Solicitud de aplicación de precios especiales # " + alertaautorizacion.IdAutorizacionPrecio.ToString() + " , pendiente de autorizar";
                    }
                    else
                    {
                        m.Subject = "Solicitud de aplicación de precios especiales # " + alertaautorizacion.IdAutorizacionPrecio.ToString() + " , ha sido Rechazada";
                    }

                }
                else
                {
                    if (autoriza == 1)
                    {
                        m.Subject = "Solicitud de aplicación de precios especiales # " + alertaautorizacion.IdAutorizacionPrecio.ToString() + " , ha sido Autorizada";
                    }
                    else
                    {
                        m.Subject = "Solicitud de aplicación de precios especiales # " + alertaautorizacion.IdAutorizacionPrecio.ToString() + " , ha sido Rechazada";
                    }
                }
                m.IsBodyHtml = true;
                string body = cuerpo_correo.ToString();
                AlternateView vistaHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                //Esto queda dentro de un try por si llegan a cambiar la imagen el correo como quiera se mande
                ////try
                ////{
                ////    LinkedResource logo = new LinkedResource(MapPath(@"Imagenes/logo.jpg"), MediaTypeNames.Image.Jpeg)
                ////    {
                ////        ContentId = "companylogo"
                ////    };
                ////    vistaHtml.LinkedResources.Add(logo);
                ////}
                ////catch (Exception)
                ////{
                ////}

                m.AlternateViews.Add(vistaHtml);
                try
                {
                    sm.Send(m);

                }
                catch (Exception)
                {

                    return "Error al enviar el correo. Favor de revisar la configuración del sistema";

                }


                //Session["lAurizacionPrecios" + Session.SessionID] = null;
                //Session["Id_FacPrec" + Session.SessionID] = null;

                //CerrarVentana("AceptarPrecios");
                return "Se envío el correo de forma correcta ";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        #endregion



        protected Sesion sesionn
        {
            get
            {
                if (HttpContext.Current.Session != null)
                {
                    return (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
                }
                return null;
            }
        }

        #region mensaje

        private void mensaje(string mensaje)
        {
            //lblMensaje.Text = mensaje;
        }

        private void Alerta(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "modalMensaje('" + mensaje + "')", true);
        }

        #endregion
    }
}