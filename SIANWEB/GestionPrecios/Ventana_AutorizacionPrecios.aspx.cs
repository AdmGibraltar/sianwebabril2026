using CapaEntidad;
using CapaNegocios;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SIANWEB
{
    public partial class Ventana_AutorizacionPrecios : System.Web.UI.Page
    {
        #region Variables 

      
        private DataTable dt { get { return (DataTable)Session["dt" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["dt" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }



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



        private List<AlertaAutorizacion> ListDet
        {
            get { return (List<AlertaAutorizacion>)Session["lAurizacionPrecios" + Session.SessionID]; }
            set { Session["lAurizacionPrecios" + Session.SessionID] = value; }
        }


        //static int Id_ConvDet_A = -1; //id de la partida que se va actualizar
        private int Id_ConvDet_A
        {
            set { Session["Id_ConvDet_AREM" + Session.SessionID] = value; }
            get { int? st = (int?)Session["Id_ConvDet_AREM" + Session.SessionID]; return st == null ? -1 : (int)st; }
        }


        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (sesion == null)
                {
                    CerrarVentana();
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);

                }
                else
                    if (!Page.IsPostBack)
                    {
                        int GLOBAL_BloqueaFacturas = 0;
                        CapaEntidad.eSysConfiguracion SysC = new CapaEntidad.eSysConfiguracion();
                        try
                        {
                            CN_SysConfigruacion CN = new CN_SysConfigruacion();
                            SysC = CN.spSysConfiguracionById(sesion.Id_Emp, sesion.Id_Cd, 957, sesion.Emp_Cnx);
                            int iTmp = 0;
                            int.TryParse(SysC.Conf_Valor, out iTmp);
                            GLOBAL_BloqueaFacturas = iTmp;
                        }
                        catch (Exception ex)
                        {
                            GLOBAL_BloqueaFacturas = 0;
                        }

                        hdBloquea_Facturas.Value = GLOBAL_BloqueaFacturas.ToString();
         
                        Inicializar();
                       


                    }
             
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        
        protected void btnCancelar_Click(object sender, EventArgs e)
        {

            try
            {
  
                //jfcv 22sep22 CerrarVentana("CancelarAlertaPrecios");

                switch (tipoAutorizacion.Value)
                   {
                      case "1":
                        CerrarVentana("grabarVal");
                        break;
                    case "2":
                        Session["lAurizacionPrecios" + Session.SessionID] = null;
                        Session["Id_FacPrec" + Session.SessionID] = null;
                        CerrarVentana();
                        break;

                    case "4":

                        Session["Respuesta" + Session.SessionID] = true;
                        string funcion;
                        funcion = "CloseWindowPedido()";
                        string script = "<script>" + funcion + "</script>";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);

                        break;
                    case "3":
                        CerrarVentanaRem("si"); //remisiones
                        break;
                      case "5": //facturas
                        if (hdBloquea_Facturas.Value.ToString() == "1")
                        {
                            if (hdId_Tamaño.Value.ToString() == "A")
                            {
                                CerrarVentana("AceptarPrecios");
                            }
                            else
                            {
                                CerrarVentana("CancelarAlertaPrecios"); //facturas
                            }
                        }
                        else
                        {
                            //si configuración esta apagada que grabe la factura 
                            CerrarVentana("AceptarPrecios");
                        }
                        break;
                    case "6":
                        Session["lAurizacionPrecios" + Session.SessionID] = null;
                        Session["Id_FacPrec" + Session.SessionID] = null;
                        CerrarVentana();
                        break;
                    default:
                        CerrarVentana("AceptarPrecios");
                        break;
                   }
               

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        private void CargarCombo()
        {
            try
            {

                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();

                cmbMotivoTodo.Items.Clear();
                int motivo = Convert.ToInt32(ListDet[0].TipoAutorizacion.ToString());
                CN_Comun.DevLlenaCombo(2, sesion.Id_Emp, motivo, sesion.Emp_Cnx, "spCatMotivoAlerta_Combo_Todos", ref cmbMotivoTodo);


                cmbMotivoTodo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
               

            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }
 
        #endregion

        #region Funciones
        private void Inicializar()
        {
            try
            {

                ListDet = (List<AlertaAutorizacion>)Session["lAurizacionPrecios" + Session.SessionID];

                if (ListDet != null)
                {
                    txtCliente.Text = "34743";
                    txtCliente.Enabled = false;
                    txtClienteNombre.Text = "FUNDACION ADELAIDA LAFON AC";
                    txtClienteNombre.Enabled = false;
                    txtTerritorio.Text = "30302011"; //  "30202011";
                    txtTerritorio.Enabled = true;
                    if (ListDet.Count != 0)
                    {

                        txtCliente.Text = ListDet[0].Id_Cte.ToString();
                        txtCliente.Enabled = false;
                        txtClienteNombre.Text = ListDet[0].Cte_NomComercial.ToString() != "" ? ListDet[0].Cte_NomComercial.ToString() : "";
                        txtClienteNombre.Enabled = false;
                        txtTerritorio.Text = ListDet[0].Id_Ter.ToString();
                        txtTerritorio.Enabled = true;

                        if (ListDet[0].TipoAutorizacion.ToString() == "6")
                        {
                            hdId_Rik.Value = ListDet[0].IdRepresentante.ToString();
                        }
                    }
                    foreach (AlertaAutorizacion lst in ListDet)
                    {
                        lst.FechaVigencia = DateTime.Now.AddMonths(3);
                    }
                    rgDetalles.DataSource = ListDet;
                    rgDetalles.Rebind();
                    double ancho = 0;
                    foreach (GridColumn gc in rgDetalles.Columns)
                    {
                        if (gc.Display)
                        {
                            ancho = ancho + gc.HeaderStyle.Width.Value;
                        }
                    }
                    rgDetalles.Width = Unit.Pixel(Convert.ToInt32(ancho));
                    rgDetalles.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));

                    //// si viene de CRM tomar el Uafir de la variable de sesion y si no entonces calcularlo
                    ///
                    //Jfcv 22sep2022
                    tipoAutorizacion.Value = ListDet[0].TipoAutorizacion.ToString();

                    if (Session["Id_FacPrec" + Session.SessionID] != null)
                    {
                        string folior = Session["Id_FacPrec" + Session.SessionID].ToString();
                        if (tipoAutorizacion.Value == "6")
                        {
                            hdId_ReporteGP.Value = folior;
                        }
                        else

                        {
                            hdId_ReporteGP.Value = "0";
                        }
                    }


                    //jfcv en el acys también dirá ignorar 9 sep 2022
                    if (ListDet[0].TipoAutorizacion.ToString() == "2") //acys
                    {
                        //8 diciembre del 2022 
                        //si es acys que  en el botón  cancelar ponga Regresar
                        btnCancelar.InnerText = "Guardar Acys sin enviar sol";

                    }
                        if (ListDet[0].TipoAutorizacion.ToString() == "1")
                    {
                        //8 diciembre del 2022 
                        //si es acys que  en el botón  cancelar ponga Regresar
                        btnCancelar.InnerText = "Guardar y no enviar sol.";


                        EstadisticaRentabilidad er = (EstadisticaRentabilidad)Session["CalculoUafir" + Session.SessionID];


                        if (Session["Id_FacPrec" + Session.SessionID] != null)
                        {
                            string folior = Session["Id_FacPrec" + Session.SessionID].ToString();
                            if (tipoAutorizacion.Value == "6")
                            {
                                hdId_ReporteGP.Value = folior;
                            }
                            else

                            {
                                hdId_ReporteGP.Value = "0";
                            }
                        }

                        //8 diciembre del 2022 
                        //si es crm que  en el botón  cancelar ponga Regresar
                        //ya en valuación no va a decir regresar 
                        //btnCancelar.InnerText = "Regresar";

                        if (er != null)
                        {

                            txtVentas.Text = "$" + String.Format("{0:N}", er.VentaNetaMon);

                            txtUtilidadPrima.Text = "$" + String.Format("{0:N}", (er.UtilidadMon));
                            txtPorcUtilidadPrima.Text = String.Format("{0:N}", (er.UtilidadPorc)) + "%";
                            txtUafirmes.Text = " $" + String.Format("{0:N}", er.UafirMensualMon);
                            txtPorcUafirCte.Text = String.Format("{0:N}", er.UafirMensualPorc) + "%";
                            txtUafirAnual.Text = "$" + String.Format("{0:N}", (er.UafirMensualMon * 12));

                            txtUtilRemanente.Text = "$" + String.Format("{0:N}", (er.UtilidadRemanenteMon));

                        }
                        else
                        {
                            ObtenerUAFIR();
                        }
                    }
                    else
                    {
                        ObtenerUAFIR();
                    }

                    //15mayo 2025
                    txtComentarioGeneral.Visible = false;
                    if (ListDet[0].TipoAutorizacion.ToString() == "6")
                    {
                        
                        btnCancelar.InnerText = "No enviar sol.";
                        txtComentarioGeneral.Visible = true;
                    }
                    
                    CargarCombo();
                    //Validar si cliente es de cuenta Nacional O coordinada 
                    CN_AlertaAutorizacion CN_Alerta = new CN_AlertaAutorizacion();
    
                    string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];

                    string tipocuenta = "";
                    string nombrecliente = "";
 
                    CN_Alerta.ConsultaCuentaNacional(Convert.ToInt32(txtCliente.Value), sesion, ref tipocuenta, ref nombrecliente, Conexion);
                    if (tipocuenta != "")
                    {
                        txtTipoCliente.Text = tipocuenta;
                    }
                    else
                    {
                        txtTipoCliente.Text = "Local";
                    }
                    hdId_Tamaño.Value = ListDet[0].Id_Tamaño.ToString();

                }
                else
                {
                    rgDetalles.DataSource = null;
                    //rgDetalles.Rebind();
                }
 

                ///
                lblgrabarfactura.Visible = false;
                if (tipoAutorizacion.Value == "5")
                {
                    if (hdBloquea_Facturas.Value.ToString() == "1")
                    {
                        if (hdId_Tamaño.Value.ToString() != "A")
                        {
                            lblgrabarfactura.Visible = true;
                            btnCancelar.InnerText = "Regresar";
                        }
                        else
                        {
                            lblgrabarfactura.Visible = false;
                            btnCancelar.InnerText = "Guardar Fact sin enviar sol";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

       
        private void ObtenerUAFIR()
        {
            try
            {

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
                ////txtTerritorio.Text = "30302011";
                ////txtCliente.Value = 25249;

               



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

                //if ((Convert.ToInt32(cmbPeriodoCierre.SelectedValue.ToString().Substring(4, 2).ToString()) - 2) <= 0)
                //{
                //    Anio = (Convert.ToInt32(cmbPeriodoCierre.SelectedValue.Substring(0, 4).ToString()) - 1).ToString().Trim();
                //    if ((Convert.ToInt32(cmbPeriodoCierre.SelectedValue.ToString().Substring(4, 2).ToString()) - 2) <= -1)
                //    {
                //        Mes = "11";
                //    }
                //    else
                //    {
                //        Mes = "12";
                //    }
                //}
                //else
                //{
                //    Anio = cmbPeriodoCierre.SelectedValue.Substring(0, 4).ToString();
                //    Mes = (Convert.ToInt32(cmbPeriodoCierre.SelectedValue.Substring(4, 2).ToString()) - 2).ToString().Trim();
                //}

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


                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                CN_CatCalendario cn_calenda = new CN_CatCalendario();
                Calendario c = new Calendario();
                cn_calenda.ConsultaCalendarioActual(ref c, Sesion);
                if (tipoAutorizacion.Value=="6")
                {
                    Anio1 = c.Cal_Año.ToString().Trim();
                    Mes1 = c.Cal_Mes.ToString().Trim();
                }

                if (c.Cal_Año.ToString().Trim() == Anio1 && c.Cal_Mes.ToString().Trim() == Mes1) // 1==1
                {


                    /* aqui if(Mes1 = ) */

                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    ArrayList ALValorParametrosInternos = new ArrayList();

                    CentroDistribucion cd = new CentroDistribucion();
                    new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);

                    //Obtener datos de encabezado de la Valuación de proyecto
                    string Id_Cte = string.Empty, Cte_NomComercial = string.Empty, Vap_Nota = string.Empty;

                    Id_Cte = Convert.ToString(txtCliente.Value.HasValue ? txtCliente.Value : -1); //cmbCliente.SelectedValue;




                    Cte_NomComercial = txtClienteNombre.Text; //cmbCliente.SelectedItem.Text;
                    int? territorio = null;
                    if (txtTerritorio.Text != "")
                    {
                        territorio = Convert.ToInt32(txtTerritorio.Text);
                    }
                    else
                    {
                        territorio = 30302011;
                    }

                    //todo jfcv
                    //JFCV quitar esta parte ya cuando lo vaya a liberar a en operación
                    //Id_Cte = "25249";
                    //territorio = 30302011;
                    territorio = -1;
                    //txtCliente.Value = 25249;



                    DataTable dtReporteTotales = new DataTable();

                    if (hdId_ReporteGP.Value != "")
                    {
                        cmbPeriodovalue = "Semestral anterior";

                        new CN_ReporteGAP().ReporteRentabilidad_ConsultarTotales(
                                                sesion.Id_Emp
                                                , sesion.Id_Cd_Ver
                                                , Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1)
                                                , territorio
                                                , cmbPeriodovalue
                                                , cmbVentasValue
                                                , ref dtReporteTotales
                                                , hdId_ReporteGP.Value
                                                , sesion.Emp_Cnx);
                    }
                    else
                    {
                        new CN_CatCliente().ReporteRentabilidad_ConsultarTotales(
                            sesion.Id_Emp
                            , sesion.Id_Cd_Ver
                            , Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1)
                            , territorio
                            , cmbPeriodovalue
                            , cmbVentasValue
                            , ref dtReporteTotales
                            , sesion.Emp_Cnx);
                    }
                    ALValorParametrosInternos.Add(sesion.Id_Emp);
                    ALValorParametrosInternos.Add(sesion.Emp_Nombre);
                    ALValorParametrosInternos.Add(sesion.Id_Cd_Ver);
                    ALValorParametrosInternos.Add(sesion.Cd_Nombre);
                    ALValorParametrosInternos.Add(Id_Cte);
                    ALValorParametrosInternos.Add(Cte_NomComercial);
                    ALValorParametrosInternos.Add(txtTerritorio.Text);
                    ALValorParametrosInternos.Add(txtTerritorio.Text != "-1" ? txtTerritorio.Text : "Todos");
                    ALValorParametrosInternos.Add(cmbPeriodovalue);
                    ALValorParametrosInternos.Add(cmbVentasValue);
                    ALValorParametrosInternos.Add(DateTime.Now.ToString("dd/MM/yyyy"));
                    ALValorParametrosInternos.Add(DateTime.Now.ToString("hh:MM:ss"));
                    ALValorParametrosInternos.Add(sesion.Emp_Cnx);

                    double VentaNeta = Convert.ToDouble(dtReporteTotales.Rows[0]["VentaNeta"]);
                    double VentaNetaPapel = Convert.ToDouble(dtReporteTotales.Rows[0]["VentaNetaPapel"]);
                    double VentaNetaOtros = Convert.ToDouble(dtReporteTotales.Rows[0]["VentaNetaOtros"]);
                    double CostoMaterial = Convert.ToDouble(dtReporteTotales.Rows[0]["CostoMaterial"]);
                    double CostoMaterialNOPapel = Convert.ToDouble(dtReporteTotales.Rows[0]["CostoMaterialNOPapel"]);
                    double FactorFijos = 0;
                    double FactorUCS = 0;


                    Territorios CatTer = new Territorios();
                    CatTer.Id_Emp = sesion.Id_Emp;
                    CatTer.Id_Cd = sesion.Id_Cd;
                    CatTer.Id_Ter = Convert.ToInt32(txtTerritorio.Text);
                    new CN_CatTerritorios().ConsultaTerritorios(ref CatTer, sesion.Emp_Cnx);


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
                    amortizacion.Id_Emp = sesion.Id_Emp;
                    amortizacion.Id_Cd = sesion.Id_Cd_Ver;
                    amortizacion.Id_Cte = Convert.ToInt32(Id_Cte);
                    amortizacion.Id_Ter = Convert.ToInt32(txtTerritorio.Text != "" ? txtTerritorio.Text : "0");

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
                    new CN_Amortizacion().ConsultaInversionComodato(sesion.Id_Emp, sesion.Id_Cd_Ver, Convert.ToInt32(txtTerritorio.Text != "" ? txtTerritorio.Text : "0"), Convert.ToInt32(Id_Cte), sesion.Emp_Cnx, ref TotalInversionComodatos);

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
                        - (/*CostoMaterial*/ CostoMaterialNOPapel * (Convert.ToSingle(cd.Cd_Flete) / 100)) //flete
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
                        /*                        - (VentaNetaOtros * (Convert.ToSingle(cd.Cd_ContribucionGastosFijosOtros) / 100))
                                                  - (VentaNetaPapel * (Convert.ToSingle(cd.Cd_ContribucionGastosFijosPapel) / 100))   
                                                  - (VentaNeta * (Convert.ToSingle(cd.Cd_CargoUCS) / 100)); */
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


                    txtVentas.Text = "$" + String.Format("{0:N}", VentaNeta);

                    txtUtilidadPrima.Text = "$" + String.Format("{0:N}", (VentaNeta - CostoMaterial- CostoMaterialNOPapel));
                    txtPorcUtilidadPrima.Text = String.Format("{0:N}", (((VentaNeta - CostoMaterial - CostoMaterialNOPapel) / VentaNeta) * 100)) + "%";
                    txtUafirmes.Text = " $" + String.Format("{0:N}", UafirMensual);
                    txtPorcUafirCte.Text = String.Format("{0:N}", ((UafirMensual / VentaNeta) * 100)) + "%";
                    txtUafirAnual.Text = "$" + String.Format("{0:N}", (UafirMensual * 12));

                    txtUtilRemanente.Text = "$" + String.Format("{0:N}", (UafirDespuesImpuestos - CostoCapital));


                }
                else
                {

                    Sesion sesion = new Sesion();
                    sesion = (Sesion)Session["Sesion" + Session.SessionID];


                    int? territorio = null;
                    if (txtTerritorio.Text != "")
                    {
                        territorio = Convert.ToInt32(txtTerritorio.Text);
                    }
                    else
                    {
                        territorio = 0;
                    }


                    Territorios CatTer = new Territorios();
                    CatTer.Id_Emp = sesion.Id_Emp;
                    CatTer.Id_Cd = sesion.Id_Cd;
                    CatTer.Id_Ter = Convert.ToInt32(txtTerritorio.Text);
                    new CN_CatTerritorios().ConsultaTerritorios(ref CatTer, sesion.Emp_Cnx);

                    ///aqui
                    List<EstadisticaRentabilidad> Calculo = new List<EstadisticaRentabilidad>();

                    new CN_CatCliente().ReporteRentabilidad_ConsultarEstadistica(
                        sesion.Id_Emp
                        , sesion.Id_Cd_Ver
                        , Convert.ToInt32(txtCliente.Value.HasValue ? txtCliente.Value.Value : -1)
                        , territorio
                        , Anio1
                        , Mes1
                        , ref Calculo
                        , sesion.Emp_Cnx);



                    foreach (EstadisticaRentabilidad LeeResultadoCalculo in Calculo)
                    {


                        txtVentas.Text = "$" + String.Format("{0:N}", LeeResultadoCalculo.VentaNetaMon);

                        txtUtilidadPrima.Text = "$" + String.Format("{0:N}", (LeeResultadoCalculo.VentaNetaMon - LeeResultadoCalculo.CostoMaterialMon));
                        txtPorcUtilidadPrima.Text = String.Format("{0:N}", (((LeeResultadoCalculo.VentaNetaMon - LeeResultadoCalculo.CostoMaterialMon) / LeeResultadoCalculo.VentaNetaMon) * 100)) + "%";
                        txtUafirmes.Text = " $" + String.Format("{0:N}", LeeResultadoCalculo.UafirMensualMon);
                        txtPorcUafirCte.Text = String.Format("{0:N}", ((LeeResultadoCalculo.UafirMensualMon / LeeResultadoCalculo.VentaNetaMon) * 100)) + "%";
                        txtUafirAnual.Text = "$" + String.Format("{0:N}", (LeeResultadoCalculo.UafirMensualMon * 12));

                        txtUtilRemanente.Text = "$" + String.Format("{0:N}", (LeeResultadoCalculo.UtilidadRemanenteMon));
                    }

                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string ConsultarEmail(int id_u)
        {
            Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CN_CatUsuario cn_catusuario = new CN_CatUsuario();
            Usuario u = new Usuario();
            u.Id_Emp = Sesion.Id_Emp;
            u.Id_Cd = Sesion.Id_Cd_Ver;
            u.Id_U = id_u;
            string correo = "";
            cn_catusuario.ConsultaCorreoUsuario(u, Sesion.Emp_Cnx, ref correo);
            return correo;
        }

        private void CerrarVentana(string param)
        {
            try
            {
                string funcion = "CloseAndRebind2('" + param + "')";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CerrarVentanaRem(string param)
        {
            try
            {
                string funcion = "CloseAndRebindRem('" + param + "')";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        protected void rgBitacora_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                //if (e.RebindReason == GridRebindReason.ExplicitRebind)
                //rgBitacora.DataSource = getList();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void rgBitacora_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                //this.rgBitacora.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        public List<AlertaAutorizacion> getList()
        {
            try
            {


                List<AlertaAutorizacion> lAurizacionPrecios = (List<AlertaAutorizacion>)Session["lAurizacionPrecios" + Session.SessionID];


                return lAurizacionPrecios;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void Guardar(Int64 id_prd)
        {

            List<AlertaAutorizacion> ListDet = new List<AlertaAutorizacion>();

            ListDet = (List<AlertaAutorizacion>)Session["lAurizacionPrecios" + Session.SessionID];

            ListDet = ListDet.Where(x => x.Id_Prd > id_prd - 1 && x.Id_Prd < id_prd + 1).ToList();

            Session["lAurizacionPrecios" + Session.SessionID] = ListDet;
            //rgBitacora.DataSource = ListDet;

        }

        #endregion

        #region grid


        public string justificacion;
        private Int64 Id_Prd = 0;
        private string _Editable;



        protected void cmbMotivo_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            RadComboBox RadCombo = (sender as RadComboBox);
            RadCombo.SelectedIndex = RadCombo.FindItemIndexByValue((RadCombo.Parent.FindControl("lblold2") as Label).Text);
        }

        protected void rgBitacora_ItemDataBound(object sender, GridItemEventArgs e)
        {


            if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
            {
                //GridEditFormItem edititem = (GridEditFormItem)e.Item;
                //GridDataItem dataItem = (GridDataItem)edititem.ParentItem;
                //TextBox txtbx = (TextBox)dataItem["Seg_Descripcion"].FindControl("TextBox1");
                //RadComboBox combo = (RadComboBox)edititem["Seg_Descripcion"].FindControl("cmbMotivo");
                //combo.SelectedValue = txtbx.Text;
            }

        }

        private void Update(GridCommandEventArgs e)
        {
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];
            int Id_CteDet = 0;
            int Id_Ter = 0;
            string Ter_Nombre = "";
            int Id_Seg = 0;
            int uen = 0;
            int Ter_Tipo = 0;
            string DescTer_Tipo = string.Empty;
            string Seg_Descripcion = "";
            string Cte_UnidadDim = "";
            string Cte_Dimension = "";
            string Rik = "";
            string Uen = "";

            // SAUL GUERRA 20150506 BEGIN
            int Id_TerrServ = -1;
            string TerrServ = "";
            int Id_RIKServ = -1;
            string RIKServ = "";
            // SAUL GUERRA 20150506 END

            double Cte_Pesos = 0;
            double Cte_CarMP = 0;
            double Cte_GastoVarT = 0;
            double Cte_FletePaga = 0;
            double Cte_Potencial = 0;
            double Cte_PorcComision = 0;
            bool Cte_Activo = false;
            GridItem gi = null;

            DataRow[] Ar_dr;
            gi = e.Item;
            if (((RadComboBox)gi.FindControl("RadComboBox1")).Text == "" ||
              ((RadComboBox)gi.FindControl("RadComboBox2")).Text == "" ||
              ((Label)gi.FindControl("RadTextBox3")).Text == "" ||
              ((RadNumericTextBox)gi.FindControl("RadTextBox4")).Text == "" ||
              ((RadNumericTextBox)gi.FindControl("RadNumericTextBox5")).Text == "")
            {
                e.Canceled = true;
                Alerta("Todos los campos son requeridos");
                return;
            }

            try
            {
                Id_Ter = Convert.ToInt32(((RadComboBox)gi.FindControl("RadComboBox1")).SelectedValue);
                Ter_Nombre = ((RadComboBox)gi.FindControl("RadComboBox1")).Text;
            }
            catch (Exception ex)
            {
                e.Canceled = true;
                throw new Exception("Por favor, capture el Territorio");
            }
            try
            {
                Id_Seg = Convert.ToInt32(((RadComboBox)gi.FindControl("RadComboBox2")).SelectedValue);
                Seg_Descripcion = ((RadComboBox)gi.FindControl("RadComboBox2")).Text;
            }
            catch (Exception ex)
            {
                e.Canceled = true;
                throw new Exception("Por favor, capture el Segmento");
            }
            try
            {
                Cte_UnidadDim = ((Label)gi.FindControl("RadTextBox3")).Text;
                Cte_Dimension = ((RadNumericTextBox)gi.FindControl("RadTextBox4")).Text;
            }
            catch (Exception ex)
            {
                e.Canceled = true;
                throw new Exception("Por favor, capture la Unidad de Dimension");
            }
            try
            {
                Cte_Pesos = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("RadNumericTextBox5")).Text);
            }
            catch (Exception ex)
            {
                e.Canceled = true;
                throw new Exception("Por favor, capture la cantidad");
            }

            try
            {
                Cte_Potencial = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("RadNumericTextBox6")).Text);
            }
            catch (Exception ex)
            {
                e.Canceled = true;
                throw new Exception("Por favor, capture el potencial");
            }

            Id_CteDet = Convert.ToInt32(((Label)gi.FindControl("Label0")).Text);
            Cte_Activo = ((CheckBox)gi.FindControl("chk8")).Checked;
            Cte_CarMP = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("RadNumericTextBox7")).Text);
            Cte_GastoVarT = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("RadNumericTextBox8")).Text);
            Cte_FletePaga = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("RadNumericTextBox9")).Text);
            Cte_PorcComision = Convert.ToDouble(((RadNumericTextBox)gi.FindControl("txtComision")).Text);
            uen = !string.IsNullOrEmpty(((Label)gi.FindControl("lblIDUENEdit")).Text) ? Convert.ToInt32(((Label)gi.FindControl("lblIDUENEdit")).Text) : 0;

            // SAUL GUERRA 20150506 BEGIN
            Id_TerrServ = Convert.ToInt32(((RadComboBox)gi.FindControl("cbTerServ")).SelectedValue);
            TerrServ = ((RadComboBox)gi.FindControl("cbTerServ")).Text;
            Id_RIKServ = Convert.ToInt32(((Label)gi.FindControl("lblId_RIKServEdit")).Text);
            RIKServ = ((Label)gi.FindControl("lblRIKServEdit")).Text;
            // SAUL GUERRA 20150506 END

            if (!(Id_Ter > 0))
            {
                Alerta("debe seleccionar Tipo de territorio y Territorio");
                e.Canceled = true;
                return;
            }


            if (Cte_Potencial == 0)
            {
                AlertaFocus("El potencial no debe ser cero", ((RadNumericTextBox)gi.FindControl("RadNumericTextBox6")).ClientID);
                e.Canceled = true;
                return;
            }

            CN_CatTerritorios cn_catter = new CN_CatTerritorios();
            Territorios terr = new Territorios();
            terr.Id_Emp = sesion.Id_Emp;
            terr.Id_Cd = sesion.Id_Cd_Ver;
            terr.Id_Ter = Id_Ter;
            cn_catter.ConsultaTerritorios(ref terr, sesion.Emp_Cnx);
            Rik = terr.Rik_Nombre;
            Uen = terr.Uen_Descripcion;
            uen = terr.Id_Uen;

            Ar_dr = dt.Select("Id_CteDet='" + Id_CteDet + "'");
            if (Ar_dr.Length > 0)
            {
                DataRow dr;
                if (dt.Rows.Count > 0)
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dr = dt.Rows[i];
                        int UEN = !string.IsNullOrEmpty(dr["Id_Uen"].ToString()) ? Convert.ToInt32(dr["Id_Uen"].ToString()) : 0;
                        int id_ctedet = !string.IsNullOrEmpty(dr["Id_CteDet"].ToString()) ? Convert.ToInt32(dr["Id_CteDet"].ToString()) : 0;
                        bool activo = Convert.ToBoolean(dr["Cte_Activo"]);

                    }


                Ar_dr[0]["Ter_Tipo"] = Ter_Tipo;
                Ar_dr[0]["DescTer_Tipo"] = DescTer_Tipo;
                Ar_dr[0]["Id_Ter"] = Id_Ter;
                Ar_dr[0]["Ter_Nombre"] = Ter_Nombre;
                Ar_dr[0]["Id_Seg"] = Id_Seg;
                Ar_dr[0]["Seg_Descripcion"] = Seg_Descripcion;
                Ar_dr[0]["Cte_UnidadDim"] = Cte_UnidadDim;
                Ar_dr[0]["Cte_Dimension"] = Convert.ToDouble(Cte_Dimension).ToString("#,##0.00");
                Ar_dr[0]["Cte_Pesos"] = Cte_Pesos;
                Ar_dr[0]["Cte_Potencial"] = Cte_Potencial;
                Ar_dr[0]["Cte_Activo"] = Cte_Activo;
                Ar_dr[0]["rik"] = Rik;
                Ar_dr[0]["uen"] = Uen;


                Ar_dr[0]["Id_TerServ"] = Id_TerrServ;
                Ar_dr[0]["TerServ"] = TerrServ;
                Ar_dr[0]["Id_RIKServ"] = Id_RIKServ;
                Ar_dr[0]["RIKServ"] = RIKServ;


                Ar_dr[0]["Cte_ManoObra"] = Cte_CarMP;
                Ar_dr[0]["Cte_GastoTerritorio"] = Cte_GastoVarT;
                Ar_dr[0]["Cte_FletePaga"] = Cte_FletePaga;
                Ar_dr[0]["Cte_PorcComision"] = Cte_PorcComision;
                Ar_dr[0]["Id_Uen"] = uen;


                HtmlInputCheckBox chkTradicional = e.Item.FindControl("chkTradicionalEdicion") as HtmlInputCheckBox;
                HtmlInputCheckBox chkGarantia = e.Item.FindControl("chkGarantiaEdicion") as HtmlInputCheckBox;
                ////ActualizarRegistroDeDetalle(Ar_dr[0]["Id_CteDet"].ToString(), chkTradicional.Checked ? "1" : "0", chkGarantia.Checked ? "1" : "0");
                if (!chkGarantia.Checked)
                {
                    RadComboBox rcbGarantias = e.Item.FindControl("rcbGarantiasEdit") as RadComboBox;
                    foreach (var item in rcbGarantias.Items.AsEnumerable())
                    {
                        CheckBox chk = item.FindControl("chkItem") as CheckBox;
                        chk.Checked = false;
                    }
                }

                if (!chkTradicional.Checked && !chkGarantia.Checked)
                {
                    chkTradicional.Checked = true;
                }

                //ActualizarGarantiasAsociadas(e, Ar_dr[0]);
                Ar_dr[0]["Cte_Tradicional"] = chkTradicional.Checked;
                Ar_dr[0]["Cte_Garantia"] = chkGarantia.Checked;
                //Ar_dr[0].AcceptChanges();

                DataRow[] Ar_drTer;
                Ar_drTer = dt.Select("Id_Ter='" + Id_Ter + "'");

                if (Id_Ter != 60601011)
                {
                    ////CargarTerritoriosAutorizacion(Ar_drTer[0].ItemArray, "Edit", true, 4);
                }


            }
        }

        private void Delete(GridCommandEventArgs e)
        {
            int Id_CteDet = 0;
            GridItem gi = null;
            DataRow[] Ar_dr;
            DataRow dr;
            gi = e.Item;

            if (((Label)gi.FindControl("lbleditar")).Text == "False")
            {
                Alerta("Imposible eliminar, existen documentos que utilizan esta relacion");
                return;
            }

            Id_CteDet = Convert.ToInt32(((Label)gi.FindControl("Label0")).Text);
            Ar_dr = dt.Select("Id_CteDet='" + Id_CteDet + "'");
            if (Ar_dr.Length > 0)
            {
                Ar_dr[0].Delete();
                dt.AcceptChanges();
            }
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                dr = dt.Rows[x];
                dr.BeginEdit();
                dr["Id_CteDet"] = x + 1;
                dr.AcceptChanges();
            }
        }


        protected void rgBitacora_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                GridEditableItem editedItem = e.Item as GridEditableItem;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                //pruebas https://docs.telerik.com/devtools/aspnet-ajax/controls/grid/data-editing/update-records/updating-values-using-inplace-and-editforms-modes

                GridEditableItem Item = (GridEditableItem)e.Item;

                if (e.Item is GridDataItem)
                {
                    //the item is in regular mode
                    GridDataItem dataItem = e.Item as GridDataItem;
                    //do something here 
                }

                GridEditableItem editableItem = ((GridEditableItem)e.Item);
                Hashtable values = new Hashtable();
                editableItem.ExtractValues(values);


                DataRow[] Ar_dr;
                GridItem gi = null;
                gi = e.Item;
                string respuesta = "";

                Int64 Id_Prd2 = Convert.ToInt64(((Label)gi.FindControl("Label0")).Text);
                string comentarios = Convert.ToString(((RadTextBox)gi.FindControl("Comentarios")).Text);
                string combocomentarios = Convert.ToString(((RadComboBox)gi.FindControl("MotivoDropDown")).Text);


                Session["Id_prd" + Session.SessionID] = Id_Prd2;
                Session["Justificacion" + Session.SessionID] = comentarios;



                if (combocomentarios == "")
                {
                    Alerta("El campo de Comentario es requerido");
                    e.Canceled = true;
                    return;
                }





                //double PCD_PrecioAAAEsp = 0;

                //if (!String.IsNullOrEmpty((editedItem["PCD_PrecioAAAEspB"].FindControl("RadNumericTextBoxPrecioAAAEsp") as RadNumericTextBox).Text))
                //{

                //    PCD_PrecioAAAEsp = double.Parse((editedItem["PCD_PrecioAAAEspB"].FindControl("RadNumericTextBoxPrecioAAAEsp") as RadNumericTextBox).Text);

                //}


                AlertaAutorizacion c = new AlertaAutorizacion();
                c = ListDet.FirstOrDefault(o => o.Id_Prd == Id_Prd2);


                c.Justificacion = justificacion;
                c.MotivoRechazo = combocomentarios;
                c.MotivoCancelacion = comentarios;


                //rgBitacora.DataSource = ListDet;
                //rgBitacora.DataBind();




            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                e.Canceled = true;
            }
            //finally
            //{
            //    RAM1.ResponseScripts.Add("showcolum();");
            //}
        }
        #endregion

        #region grid  

        protected void rgDetalles_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            try
            {
                rgDetalles.Rebind();

            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }

        private void Borrar(GridCommandEventArgs e)
        {
            try
            {
                GridItem gi = e.Item;
                string Id_Prd;
                Id_Prd = this.rgDetalles.MasterTableView.Items[e.Item.ItemIndex]["Id_Prd"].Text;

                ListDet.Remove(ListDet.Where(x => x.Id_Prd == Int64.Parse(Id_Prd)).ToList()[0]);

                rgDetalles.DataSource = ListDet;
                rgDetalles.DataBind();


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        protected void rgDetalles_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                GridEditableItem editedItem = e.Item as GridEditableItem;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                RadNumericTextBox TxtProducto = editedItem.FindControl("RadNumericTextBox1") as RadNumericTextBox;

                GridEditableItem Item = (GridEditableItem)e.Item;
                Int64 Id_Prd = Convert.ToInt64(Item.OwnerTableView.DataKeyValues[Item.ItemIndex]["Id_Prd"]);
                double precio_vta = Convert.ToDouble(Item.OwnerTableView.DataKeyValues[Item.ItemIndex]["Precio_Vta"]);
                GridEditableItem editableItem = ((GridEditableItem)e.Item);
                Hashtable values = new Hashtable();
                editableItem.ExtractValues(values);


                DataRow[] Ar_dr;
                GridItem gi = null;
                gi = e.Item;
                string respuesta = "";

                //if (!String.IsNullOrEmpty((editedItem["PCD_PrecioVtaMin"].FindControl("RadNumericTextBoxPrecioVtaMin") as RadNumericTextBox).Text))
                //{


                string justificacion = Convert.ToString(((RadTextBox)gi.FindControl("RadTextBoxJustificacion")).Text);
                string justificacionmemo = Convert.ToString(((RadTextBox)gi.FindControl("RadTextBoxJustificacionMemo")).Text);
                string combocomentarios = Convert.ToString(((RadComboBox)gi.FindControl("MotivoDropDown")).Text);
                RadDatePicker fechavigencia = ((RadDatePicker)gi.FindControl("tpFechaVigencia"));
                //JFCV 
                int idtipomotivo = Convert.ToInt32(((RadComboBox)gi.FindControl("MotivoDropDown")).SelectedValue);

                if (combocomentarios == "")
                {
                    Alerta("El campo de Comentario es requerido");
                    e.Canceled = true;
                    return;
                }
                if (justificacionmemo == "")
                {
                    Alerta("El campo de justificación es requerido.");
                    e.Canceled = true;
                    return;
                }

                if (idtipomotivo < 1)
                {
                    Alerta("Debe elegir un motivo.");
                    e.Canceled = true;
                    return;
                }

                AlertaAutorizacion c = new AlertaAutorizacion();
                c = ListDet.FirstOrDefault(o => o.Id_Prd == Id_Prd);

                if (combocomentarios == "Otro")
                {
                    c.Id_Motivo = 3;
                    if (justificacion == "")
                    {
                        Alerta("Si elige el Mótivo Otros, debe definir el motivo.");
                        e.Canceled = true;
                        return;
                    }

                }

                int registrar = 1;
                if (idtipomotivo == 5)
                {
                    string ConexionCentral = ConfigurationManager.AppSettings["strConnectionCentral"].ToString().Trim();
                    ConvenioDet conv;
                    ConvenioDet convdet;
                    CN_Convenio cn_conv;
                    conv = new ConvenioDet();
                    convdet = new ConvenioDet();
                    cn_conv = new CN_Convenio();
                    conv.Id_Emp = sesion.Id_Emp;
                    conv.Id_Cd = sesion.Id_Cd_Ver;
                    conv.Id_Cte = Convert.ToInt32(txtCliente.Text.ToString().Trim() != "" ? txtCliente.Text.ToString().Trim() : "-1");
                    conv.Id_Prd = Id_Prd;

                    if (conv.Id_Prd <= 999999999999)
                    {
                        cn_conv.Convenio_ConsultaPrecio(conv, ref convdet, ConexionCentral);
                    }
                    if (convdet != null && convdet.PCD_PrecioAAAEsp > 0)
                    {
                        registrar = 1;
                    }
                    else
                    {
                        string mensaje = "";
                        mensaje = "Este cliente - producto no tienen un convenio vigente.";
                        RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
                        registrar = 0;
                    }
                }
                if (idtipomotivo == 6)
                {
                    //valido si es cuenta Nacional
                    if (txtTipoCliente.Text != "CN" && txtTipoCliente.Text != "CC")
                    {
                        string mensaje = "";
                        mensaje = "Este cliente no esta vinculado a una matriz de cuentas nacionales, elija otro motivo.";
                        RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
                        registrar = 0;
                    }
                }



                //JFCV
                if (registrar == 1)
                {
                    c.Id_Motivo = idtipomotivo;
                    c.Nom_Motivo = combocomentarios;
                    c.Justificacion = justificacion;
                    c.MotivoRechazo = combocomentarios;
                    c.JustificacionMemo = justificacionmemo;
                    c.FechaVigencia = Convert.ToDateTime(fechavigencia.SelectedDate);


                    rgDetalles.DataSource = ListDet;
                    rgDetalles.DataBind();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                e.Canceled = true;
            }
            finally
            {
                RAM1.ResponseScripts.Add("showcolum();");
            }
        }

        protected void rgDetalles_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                ErrorManager();
                Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
                string Id_prd = "";

                GridEditableItem Item = (GridEditableItem)e.Item;

                // string Id_ConvDet = Convert.ToString(Item.OwnerTableView.DataKeyValues[Item.ItemIndex]["Id_ConvDet"]);

                Id_prd = Convert.ToString(Item.OwnerTableView.DataKeyValues[Item.ItemIndex]["Id_Prd"]);
                ListDet.Remove(ListDet.Where(x => x.Id_Prd == Int64.Parse(Id_prd)).ToList()[0]);

                rgDetalles.DataSource = ListDet;
                rgDetalles.DataBind();

            }
            catch (Exception ex)
            {
                e.Canceled = true;
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
            finally
            {
                RAM1.ResponseScripts.Add("showcolum();");
            }
        }
        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ErrorManager();
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    List<AlertaAutorizacion> List = new List<AlertaAutorizacion>();

                    rgDetalles.DataSource = ListDet;

                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }


        }



        protected void rgDetalles_ItemCommand(object sender, GridCommandEventArgs e)
        {
            ErrorManager();
            try
            {

                GridItem gi = e.Item;

                if (e.CommandName == "enviar")
                {

                    int indice = Convert.ToInt32(e.CommandArgument);
                    GridDataItem dataItem = e.Item as GridDataItem;
                    string Id_prod = (dataItem.OwnerTableView.DataKeyValues[indice]["Id_Prd"]).ToString();

                    // int idmotivo = Convert.ToInt32((dataItem["Id_Motivo"].FindControl("MotivoDropDown") as RadComboBox).SelectedValue);


                    AlertaAutorizacion solicitud = new AlertaAutorizacion();

                    Int64 Id_Prd;
                    Id_Prd = Convert.ToInt64(Id_prod);

                    int item = indice;
                    RadGrid rgPago = PnlLogin.FindControl("rgDetalles") as RadGrid;


                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];


                    AlertaAutorizacion c = new AlertaAutorizacion();
                    c = ListDet.FirstOrDefault(o => o.Id_Prd == Id_Prd);

                    string justificacion = c.Justificacion;
                    string justificacionmemo = c.JustificacionMemo;
                    DateTime? fechavigencia = c.FechaVigencia;

                    string combocomentarios = c.MotivoRechazo;
                    int motivo = c.Id_Motivo;


                    //if ( combocomentarios== "Otro")
                    //{
                    //    c.Id_Motivo = 3;
                    //    if (justificacion == "")
                    //    {
                    //        Alerta("Debe Alimentar el motivo por el cuál desea realizar la solicitud de precio especial");
                    //        e.Canceled = true;
                    //        return;
                    //    }
                    //}

                    //if  (motivo ==-1)
                    //    {
                    //        Console.WriteLine("Default case")
                    //        Alerta("Debe Alimentar el motivo por el cuál desea realizar la solicitud de precio especial");
                    //        e.Canceled = true;
                    //        return;
                    //}

                    //switch (combocomentarios)
                    //{
                    //    case "No se tenía en existencia y se sustituyó":
                    //        c.Id_Motivo = 1;
                    //        break;

                    //    case "Así se negoció con el cliente por el RIK":
                    //        c.Id_Motivo = 2;
                    //        break;
                    //    case "Otro":
                    //        c.Id_Motivo = 3;
                    //        if (justificacion == "")
                    //        {
                    //            Alerta("Debe Alimentar el motivo por el cuál desea realizar la solicitud de precio especial");
                    //            e.Canceled = true;
                    //            return;
                    //        }
                    //        break;
                    //    default:
                    //        Console.WriteLine("Default case");
                    //        Alerta("Debe Alimentar el motivo por el cuál desea realizar la solicitud de precio especial");
                    //        e.Canceled = true;
                    //        return;
                    //};



                    if (motivo < 1)
                    {
                        Alerta("Debe Alimentar el motivo por el cuál desea realizar la solicitud de precio especial");
                        e.Canceled = true;
                        return;
                    }

                    if (c.Id_Motivo == 3)
                    {
                        if (justificacion == "")
                        {
                            Alerta("Debe Alimentar el motivo por el cuál desea realizar la solicitud de precio especial");
                            e.Canceled = true;
                            return;
                        }
                    }
                    else
                    {
                        c.Justificacion = "";
                    }

                    if (fechavigencia <= DateTime.Now)
                    {
                        Alerta("La fecha de vigencia debe ser mayor al día de hoy.");
                        e.Canceled = true;
                        return;
                    }



                    if (justificacionmemo == "")
                    {
                        Alerta("Debe Alimentar brevemente una justificación, por la cuál desea realizar la solicitud de precio especial");
                        e.Canceled = true;
                        return;
                    }

                    ///
                    ////enviar a autorización 

                    AlertaAutorizacion alertaautorizacion = new AlertaAutorizacion();
                    alertaautorizacion.Justificacion = justificacion;
                    alertaautorizacion.Estatus = 1;
                    alertaautorizacion.JustificacionMemo = justificacionmemo;
                    alertaautorizacion.FechaVigencia = fechavigencia;

                    alertaautorizacion.Id_Emp = sesion.Id_Emp;
                    alertaautorizacion.Id_Cd = sesion.Id_Cd;
                    alertaautorizacion.IdRepresentante = c.IdRepresentante;
                    alertaautorizacion.Id_Cte = Convert.ToInt32(txtCliente.Text);
                    if (c.Id_Ter == 0)
                    {
                        alertaautorizacion.Id_Ter = Convert.ToInt32(txtTerritorio.Text);
                    }
                    else
                    {
                        alertaautorizacion.Id_Ter = c.Id_Ter;
                    }
                    alertaautorizacion.Id_Ter = Convert.ToInt32(txtTerritorio.Text);
                    alertaautorizacion.TipoAutorizacion = c.TipoAutorizacion;
                    alertaautorizacion.Id_Prd = c.Id_Prd;
                    alertaautorizacion.Precio_Vta = c.Precio_Vta;
                    ///JFCV precioobjetivo 31 oct 
                    alertaautorizacion.PrecioObjetivo = c.PrecioObjetivo;
                    alertaautorizacion.Id_Tamaño = c.Id_Tamaño;
                    alertaautorizacion.Precio_MinimoRik = c.Precio_MinimoRik;
                    alertaautorizacion.IdAutorizacionAnterior = c.IdAutorizacionAnterior;
                    alertaautorizacion.Precio_MinimoGte = c.Precio_MinimoGte;
                    alertaautorizacion.Precio_VtaAutorizado = c.Precio_VtaAutorizado;
                    alertaautorizacion.FechaSolicitud = DateTime.Now;
                    alertaautorizacion.Prd_Descripcion = c.Prd_Descripcion;
                    if (c.Precio_Vta < c.Precio_MinimoGte)
                    {
                        alertaautorizacion.Req_Aut_Director = 1;
                    }
                    else
                    {
                        alertaautorizacion.Req_Aut_Director = 0;
                    }
                    alertaautorizacion.Estatus = 1;
                    alertaautorizacion.IdUSolicita = sesion.Id_U;
                    alertaautorizacion.Fecha_UltMod = DateTime.Now;
                    alertaautorizacion.Activo = true;
                    alertaautorizacion.Justificacion = c.Justificacion;
                    alertaautorizacion.JustificacionGte = "";
                    alertaautorizacion.PrecioLista = c.PrecioLista;
                    alertaautorizacion.Id_Motivo = c.Id_Motivo;

                    CN_CatTerritorios cn_terr = new CN_CatTerritorios();
                    Territorios terr = new Territorios();

                    if (alertaautorizacion.IdRepresentante == -1)
                    {
                        terr.Id_Emp = sesion.Id_Emp;
                        terr.Id_Cd = sesion.Id_Cd_Ver;
                        terr.Id_Ter = Convert.ToInt32(txtTerritorio.Text);
                        cn_terr.ConsultaTerritorios(ref terr, sesion.Emp_Cnx);

                        alertaautorizacion.IdRepresentante = Convert.ToInt32(terr.Id_Rik.ToString());
                    }

                    EstadisticaRentabilidad estrentabilidad = new EstadisticaRentabilidad();
                    if (alertaautorizacion.TipoAutorizacion == 1)
                    {
                        estrentabilidad = (EstadisticaRentabilidad)Session["CalculoUafir" + Session.SessionID];
                        estrentabilidad.Id_Cte = 0;
                    }
                    int verificador = 0;

                    string mensaje = EnviaraAutorizar(alertaautorizacion, estrentabilidad, ref verificador);


                    if (verificador > 0)
                    {
                        ListDet.Remove(ListDet.Where(x => x.Id_Prd == Int64.Parse(Id_prod)).ToList()[0]);

                        rgDetalles.DataSource = ListDet;
                        rgDetalles.DataBind();

                        Alerta(mensaje + Id_prod);


                        //JFCV 2dic eliminar envío de correo porque se hara de forma masiva
                        //EnviarCorreo(alertaautorizacion, verificador);

                        if (ListDet.Count == 0)
                        {

                            //if (tipoAutorizacion.Value == "2") //remisiones 
                            //{
                            //    try
                            //    {
                            //        Session["Respuesta" + Session.SessionID] = true;
                            //        string funcion;
                            //        funcion = "CloseWindowPedido()";
                            //        string script = "<script>" + funcion + "</script>";
                            //        ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
                            //    }
                            //    catch (Exception ex)
                            //    {
                            //        ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                            //    }
                            //}


                            //if (tipoAutorizacion.Value == "1")
                            //{
                            //    CerrarVentana("CancelarAlertaPrecios");
                            //}
                            //else
                            //{
                            //    if (tipoAutorizacion.Value == "3") //remisiones 
                            //    {

                            //        CerrarVentanaRem("si");
                            //    }
                            //    else
                            //    {
                            //        CerrarVentana("AceptarPrecios");
                            //    }
                            //}

                            //jfcv 22sep22 CerrarVentana("CancelarAlertaPrecios");

                            switch (tipoAutorizacion.Value)
                            {
                                case "1":
                                    CerrarVentana("grabarVal");
                                    break;
                                case "2":
                                    //JFCV 26 dic 2022 cuando enviaba a autorizar no eliminaba la información 
                                    Session["lAurizacionPrecios" + Session.SessionID] = null;
                                    Session["Id_FacPrec" + Session.SessionID] = null;
                                    CerrarVentana();
                                    break;

                                case "4":

                                    Session["Respuesta" + Session.SessionID] = true;
                                    string funcion;
                                    funcion = "CloseWindowPedido()";
                                    string script = "<script>" + funcion + "</script>";
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);

                                    break;
                                case "3":
                                    CerrarVentanaRem("si"); //remisiones
                                    break;
                                case "5": //facturas
                                    if (hdBloquea_Facturas.Value.ToString() == "1")
                                    {
                                        if (hdId_Tamaño.Value.ToString() == "A")
                                        {
                                            CerrarVentana("AceptarPrecios");
                                        }
                                        else
                                        {
                                            CerrarVentana("CancelarAlertaPrecios"); //facturas
                                        }
                                    }
                                    else
                                    {
                                        //si configuración esta apagada que grabe la factura 
                                        CerrarVentana("AceptarPrecios");
                                    }
                                    break;
                                default:
                                    CerrarVentana("AceptarPrecios");
                                    break;
                            }


                        }
                        else
                        {
                            Console.WriteLine("No pudo insertar la información");
                            Alerta(mensaje + Id_Prd.ToString());
                            e.Canceled = true;
                            return;
                        }


                    }
                }
                if (e.CommandName == "InitInsert")
                {
                    //cantidad_A = 0;
                    Id_ConvDet_A = 0;
                    //Id_Prd_A = 0;
                    //costo_A = 0;

                    if (!validarCamposDetalle())
                    {
                        e.Canceled = true;
                        return;
                    }

                }
                if (e.CommandName == "Edit")
                {
                    Id_ConvDet_A = 1; // int.Parse(rgDetalles.MasterTableView.Items[e.Item.ItemIndex]["Id_ConvDet"].Text);
                    //Id_Prd_A = int.Parse((rgDetalles.MasterTableView.Items[e.Item.ItemIndex]["Id_Prd"].FindControl("ProdLabel") as Label).Text);
                    //cantidad_A = 0;

                }

                switch (e.CommandName)
                {
                    case "Cancelar":
                        Borrar(e);
                        break;
                }


            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }


        }
        protected void rgDetalles_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            ErrorManager();

            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;



                ImageButton button = (ImageButton)dataItem["DeleteColumn"].Controls[0];
                string Id_prod = (dataItem.OwnerTableView.DataKeyValues[dataItem.ItemIndex]["Id_Prd"]).ToString();
                //string descripcion = (dataItem["Prd_Descripcion"].FindControl("DescripcionLabel") as Label).Text;

                //button.Attributes["onclick"] = "return confirm('¿Desea eliminar el producto a autorizar? \\nClave Key :  " +
                //    Id_prod + "\\nDescripción: " + descripcion + "\\n* Esta modificación surtirá efecto una vez que se de clic en el \\n icono de Guardar en la parte superior derecha. ')";



                button.Attributes["onclick"] = "return confirm('¿Desea eliminar el producto a autorizar? \\nClave Key :  " +
                        Id_prod + "\\n* El producto solo se elimina de la lista de productos que se enviarán a autorizar. ')";



                string strKey = Convert.ToString(((CapaEntidad.AlertaAutorizacion)dataItem.DataItem).MotivoRechazo);
                if (strKey == null)
                {
                    (dataItem["DetailColumn2"].Controls[0] as ImageButton).Visible = false;
                }
                int autorizacion = Convert.ToInt32(((CapaEntidad.AlertaAutorizacion)dataItem.DataItem).IdAutorizacionAnterior);




            }

            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
            {
                GridEditableItem editItem = (GridEditableItem)e.Item;

                Control insertbtn = (Control)editItem.FindControl("PerformInsertButton");
                if (insertbtn != null)
                { //se habilitan todos los controles
                    (e.Item.FindControl("RadNumericTextBox1") as RadNumericTextBox).Enabled = true;
                    //(editItem["importe"].Controls[0] as TextBox).Visible = false;
                }

                Control updatebtn = (Control)editItem.FindControl("UpdateButton");
                if (updatebtn != null)
                {
                    //Id_ConvDet_A = Convert.ToInt32(editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Id_ConvDet"]);
                    ((System.Web.UI.WebControls.WebControl)(editItem["PrecioObjetivo"])).Enabled = false;
                    ((System.Web.UI.WebControls.WebControl)(editItem["Precio_MinimoRik"])).Enabled = false;
                    ((System.Web.UI.WebControls.WebControl)(editItem["Precio_MinimoGte"])).Enabled = false;
                    ((System.Web.UI.WebControls.WebControl)(editItem["PrecioLista"])).Enabled = false;


                }

            }


            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem form = (GridEditableItem)e.Item;

                RadTextBox dataField = (RadTextBox)form["Justificacion"].FindControl("RadTextBoxJustificacion");
                if (!dataField.Enabled)
                {
                    dataField = (RadTextBox)form["Justificacion"].FindControl("RadTextBoxJustificacion");
                }

                dataField.Focus();
            }

            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
            {

                //----------------------------------------- JFCV  7 Feb 2023
                GridEditableItem editItem2 = (GridEditableItem)e.Item;

                RadComboBox comboMotivoDropDownPartidaItem = (RadComboBox)editItem2.FindControl("MotivoDropDown");
                List<AlertaAutorizacion> alertaAutorizaciones = new List<AlertaAutorizacion>();
                CN_AlertaAutorizacion cn_alertaauto = new CN_AlertaAutorizacion();
                cn_alertaauto.ConsultaMotivos(Convert.ToInt32(tipoAutorizacion.Value), sesion, ref alertaAutorizaciones);
                comboMotivoDropDownPartidaItem.DataTextField = "Nom_Motivo";
                comboMotivoDropDownPartidaItem.DataValueField = "Id_Motivo";
                comboMotivoDropDownPartidaItem.DataSource = alertaAutorizaciones;
                comboMotivoDropDownPartidaItem.DataBind();

            }

        }


        private bool validarCamposDetalle()
        {

            Sesion session = (Sesion)Session["Sesion" + Session.SessionID];
            session = (Sesion)Session["Sesion" + Session.SessionID];


            return true;
        }

        public string EnviaraAutorizar(AlertaAutorizacion alertaautorizacion, EstadisticaRentabilidad estrentabilidad, ref int Verificador)
        {
            try
            {
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];



                CN_AlertaAutorizacion cn_leads = new CN_AlertaAutorizacion();
                cn_leads.CapAlertaAutorizacioInsertar(alertaautorizacion, Conexion, ref Verificador);



                if (Verificador > 0)
                {
                    int verificador2 = 0;
                    alertaautorizacion.IdAutorizacionPrecio = Verificador;
                    if (alertaautorizacion.TipoAutorizacion == 1)
                    {
                        cn_leads.CapAlertaAutorizacioRentabilidadInsertar(alertaautorizacion, estrentabilidad, Conexion, ref verificador2);
                    }
                    return "Se inserto la solicitud de precio Número " + alertaautorizacion.IdAutorizacionPrecio.ToString() + " para el producto ";
                }
                else
                {
                    return "Hubo un error al registrar la solicitud de precio del producto: ";
                }



                //Alerta("La información ha sido guardada.");

                //Page.ClientScript.RegisterStartupScript(this.GetType(), "Regresar", "Mensajecerrarpantalla('La información ha sido guardada')", true);

                //


            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                return "Hubo un error al insertar el folio." + ex.Message;

            }
        }

        public string EnviaraAutorizarGPMa(AlertaAutorizacion alertaautorizacion, EstadisticaRentabilidad estrentabilidad, ref int Verificador)
        {
            try
            {
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];



                CN_AlertaAutorizacion cn_leads = new CN_AlertaAutorizacion();
                cn_leads.CapAlertaAutorizacioInsertarGPMa(alertaautorizacion, Conexion, ref Verificador);



                if (Verificador > 0)
                {
                    int verificador2 = 0;
                    alertaautorizacion.IdAutorizacionPrecio = Verificador;
                    return "Se inserto la solicitud de precio Número " + alertaautorizacion.IdAutorizacionPrecio.ToString() + " para el producto ";
                }
                else
                {
                    return "Hubo un error al registrar la solicitud de precio del producto: ";
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                return "Hubo un error al insertar el folio." + ex.Message;

            }
        }


        public string EnviaraAutorizarGPMaDetalle(AlertaAutorizacion alertaautorizacion, EstadisticaRentabilidad estrentabilidad, ref int Verificador)
        {
            try
            {
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];



                CN_AlertaAutorizacion cn_leads = new CN_AlertaAutorizacion();
                cn_leads.CapAlertaAutorizacioInsertarGPMaDet(alertaautorizacion, Conexion, ref Verificador);

                if (Verificador > 0)
                {
                   // int verificador2 = 0;
                   // alertaautorizacion.IdAutorizacionPrecio = Verificador;
                    return "Se inserto la solicitud de precio Número " + alertaautorizacion.IdAutorizacionPrecio.ToString() + " para el producto ";
                }
                else
                {
                    return "Hubo un error al registrar la solicitud de precio del producto: ";
                }



                //Alerta("La información ha sido guardada.");

                //Page.ClientScript.RegisterStartupScript(this.GetType(), "Regresar", "Mensajecerrarpantalla('La información ha sido guardada')", true);

                //


            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
                return "Hubo un error al insertar el folio." + ex.Message;

            }
        }
        
        //jfcv 17 mzo 2022
        protected void ValidarFechaVigencia_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            RadDatePicker objFecha = sender as RadDatePicker;
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            DateTime fechamáxima = new DateTime();
            fechamáxima = DateTime.Now.AddMonths(12);
            Label tipoproducto = objFecha.Parent.FindControl("Id_CprLabel") as Label;
            //RadComboBox combo = new  RadComboBox;
            //RadComboBox MotivoDropDown = combo.Parent.FindControl("MotivoDropDown") as RadComboBox;
            RadComboBox combo = new RadComboBox();
            RadComboBox MotivoDropDown = objFecha.Parent.FindControl("MotivoDropDown") as RadComboBox;

            int motivo = Convert.ToInt32(MotivoDropDown.SelectedValue);
            string motivoelegido = MotivoDropDown.SelectedValue;
            string textoelegido = MotivoDropDown.Text;


            //fecha vigencia si producto es de tipo químicos no podrán poner más de un año y el resto 6 meses
            //23 es quimicos 
            if (tipoproducto.Text != "23")
            {
                fechamáxima = DateTime.Now.AddMonths(6);
            }



            if (textoelegido == "No se tenía en existencia y se sustituyó")
            {
                DateTime fechamáxima7 = new DateTime();
                fechamáxima7 = DateTime.Now.AddDays(7);
                RadDatePicker fechavigencia = objFecha.Parent.FindControl("tpFechaVigencia") as RadDatePicker;

                if (Convert.ToDateTime(fechavigencia.SelectedDate) > fechamáxima7)
                {
                    string mensaje = "";
                    mensaje = "Si elige sustitución la fecha de vigencia no puede ser mayor a 7 días : " + fechamáxima7.ToShortDateString();
                    RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
                    fechavigencia.DbSelectedDate = fechamáxima7;
                }
            }

            if (Convert.ToDateTime(objFecha.SelectedDate) > fechamáxima)
            {
                string mensaje = "";
                mensaje = "La fecha de vigencia no puede ser mayor a la fecha : " + fechamáxima.ToShortDateString();
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
                objFecha.DbSelectedDate = fechamáxima;
                return;
            }

        }

        //Selecciono un valor en el combo de Motivo 

        protected void cmbMotivo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {



                RadComboBox combo = sender as RadComboBox;
                RadComboBox MotivoDropDown = combo.Parent.FindControl("MotivoDropDown") as RadComboBox;

                int motivo = Convert.ToInt32(MotivoDropDown.SelectedValue);

                int valornose = Convert.ToInt32(e.Value);
                string motivoelegido = MotivoDropDown.SelectedValue;
                string textoelegido = MotivoDropDown.Text;

                //ver si puedo obtener el valor del campo otro motivo y ponerlo enable si el tipo es otro 

                RadTextBox sotromotivo = combo.Parent.FindControl("RadTextBoxJustificacion") as RadTextBox;

                if (textoelegido == "No se tenía en existencia y se sustituyó")
                {
                    DateTime fechamáxima7 = new DateTime();
                    fechamáxima7 = DateTime.Now.AddDays(7);
                    RadDatePicker fechavigencia = combo.Parent.FindControl("tpFechaVigencia") as RadDatePicker;

                    if (Convert.ToDateTime(fechavigencia.SelectedDate) > fechamáxima7)
                    {
                        string mensaje = "";
                        mensaje = "Si elige sustitución la fecha de vigencia no puede ser mayor a 7 días : " + fechamáxima7.ToShortDateString();
                        RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
                        fechavigencia.DbSelectedDate = fechamáxima7;
                    }
                }

                if (textoelegido == "Otro")
                {

                    sotromotivo.Enabled = true;
                    sotromotivo.Visible = true;
                    sotromotivo.Focus();
                }
                else
                {
                    sotromotivo.Enabled = false;
                    sotromotivo.Visible = false;
                    sotromotivo.Text = "";
                    sotromotivo.Focus();
                }
                if (motivo == 5)
                {
                    string ConexionCentral = ConfigurationManager.AppSettings["strConnectionCentral"].ToString().Trim();
                    ConvenioDet conv;
                    ConvenioDet convdet;
                    CN_Convenio cn_conv;
                    conv = new ConvenioDet();
                    convdet = new ConvenioDet();
                    cn_conv = new CN_Convenio();
                    conv.Id_Emp = sesion.Id_Emp;
                    conv.Id_Cd = sesion.Id_Cd_Ver;
                    conv.Id_Cte = Convert.ToInt32(txtCliente.Text.ToString().Trim() != "" ? txtCliente.Text.ToString().Trim() : "-1");
                    Int64 Id_Prd = Convert.ToInt64(((Label)combo.Parent.FindControl("Id_PrdLabel")).Text);
                    conv.Id_Prd = Id_Prd;
                    double PrecioIngresado = Convert.ToDouble(((Label)combo.Parent.FindControl("lblPrecio_Vta")).Text);
                    //double impore =  dtTemp.Rows[x]["Prd_Precio"].ToString().Trim()!= DBNull.Value  ? (double)(dtTemp.Rows[x]["Prd_Precio"]) : 0;

                    if (Id_Prd <= 999999999999)
                    {
                        cn_conv.Convenio_ConsultaPrecio(conv, ref convdet, ConexionCentral);
                    }
                    if (convdet != null && convdet.PCD_PrecioAAAEsp > 0)
                    {

                    }
                    else
                    {
                        string mensaje = "";
                        mensaje = "Este cliente - producto no tienen un convenio vigente.";
                        RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
                        MotivoDropDown.SelectedValue = "0";
                    }
                }
                if (motivo == 6)
                {
                    //valido si es cuenta Nacional
                    if (txtTipoCliente.Text != "CN" && txtTipoCliente.Text != "CC")
                    {
                        string mensaje = "";
                        mensaje = "Este cliente no esta vinculado a una matriz de cuentas nacionales, elija otro motivo.";
                        RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
                        MotivoDropDown.SelectedValue = "0";
                    }

                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private void EnviarCorreo(AlertaAutorizacion alertaautorizacion, int NumSolicitud)
        {
            try
            {

                //if (this.TxtMensaje.Text.Trim() == "")
                //{
                //    Alerta("Debe agregar comentarios");
                //    return;

                //}


                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];


                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Cd = session.Id_Cd_Ver;
                configuracion.Id_Emp = session.Id_Emp;
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                cn_configuracion.Consulta(ref configuracion, session.Emp_Cnx);
                StringBuilder cuerpo_correo = new StringBuilder();
                cuerpo_correo.Append("<html><div align='center'>");
                cuerpo_correo.Append("<table><tr><td>");
                cuerpo_correo.Append("<IMG SRC=\"cid:companylogo\" ALIGN='left'></td>");
                cuerpo_correo.Append("<td></td>");
                cuerpo_correo.Append("</tr><tr><td colspan='2'><br><br><br></td>");
                cuerpo_correo.Append("</tr><tr>");
                cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='2'>");
                cuerpo_correo.Append("Solicitud de autorización de precios especiales # " + NumSolicitud.ToString() + " en el CDI " + session.Cd_Nombre);
                cuerpo_correo.Append("</td></tr><tr><td> &nbsp;</td> </tr>");
                cuerpo_correo.Append("<tr>");
                cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='2'>");
                //cuerpo_correo.Append("Se omitieron los precios especiales de el/los producto(s) " + string.Join(", ", Productos) +  " autorizados en/los convenio(s) " + string.Join(", ", Convenios) +".");
                cuerpo_correo.Append("</td></tr> <tr><td> &nbsp;</td> </tr>");
                cuerpo_correo.Append("<tr>");
                cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='2'>");
                //////cuerpo_correo.Append("Comentarios: " + this.TxtMensaje.Text);
                cuerpo_correo.Append("</td></tr> <tr><td> &nbsp;</td> </tr>");
                cuerpo_correo.Append("<tr>");
                cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='2'>");
                cuerpo_correo.Append("Usuario : " + session.U_Nombre);
                cuerpo_correo.Append("</td></tr>");
                cuerpo_correo.Append("<tr><td colspan='2'>");
                cuerpo_correo.Append("<center><br>");
                cuerpo_correo.Append("</td></tr></table></div>");



                cuerpo_correo.Append("<div align='center'>");
                cuerpo_correo.Append("<table><tr>");
                cuerpo_correo.Append("<td>Producto</td><td>Descripción</td><td>Precio Vta</td><td>Precio Min rik</td>");
                cuerpo_correo.Append("</tr><tr><td>" + alertaautorizacion.Id_Prd.ToString() + "</td><td>" + alertaautorizacion.Prd_Descripcion.ToString() + "</td>");
                cuerpo_correo.Append("<td> " + alertaautorizacion.PrecioObjetivo.ToString() + " </td > ");
                cuerpo_correo.Append("<td> " + alertaautorizacion.Precio_Vta.ToString() + " </td ><td> " + alertaautorizacion.Precio_MinimoRik.ToString() + " </td> ");
                cuerpo_correo.Append("</tr><tr><td colspan='2'><br><br><br></td>");
                cuerpo_correo.Append("</tr><tr>");
                cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='2'>");
                cuerpo_correo.Append("Se ha colocado una solicitud de autorización de precio especial " + NumSolicitud.ToString());
                cuerpo_correo.Append("</td></tr>");

                string[] url = Request.Url.ToString().Split(new char[] { '/' });

                cuerpo_correo.Append("<tr><td><br><center>");
                cuerpo_correo.Append("<a href='" + Request.Url.ToString().Replace(url[url.Length - 1], "") + "CapAlertaAutorizacion.aspx'" + ">");
                cuerpo_correo.Append("Solicitud de autorización de precios</a></font></center>");
                cuerpo_correo.Append("</td></tr></table></div>");
                cuerpo_correo.Append(" </html>");


                //               cuerpo_correo.Append("<style type='text/css'>  .borderbottom        {      color: #000099;        background-color: Transparent;       padding: 0px;       border: 0px solid #000099; font-size:X-Small;");
                //               cuerpo_correo.Append("	font-weight:bold; -moz-border-radius: 0px 0px 0px 0px;} .style1{  height: 56px; } ");

                //               cuerpo_correo.Append(" .RadGrid .rgHeader, .RadGrid th.rgResizeCol {  padding-top: 5px;  padding-bottom: 4px;  text-align: left;  font-weight: normal; }");

                //               cuerpo_correo.Append(" .RadGrid_Default .rgHeader, .RadGrid_Default .rgHeader a {  color: #333; }");


                //               cuerpo_correo.Append(" .RadGrid_Default .rgHeader, .RadGrid_Default th.rgResizeCol { border: 0;  border-bottom: 1px solid #828282; background: #eaeaea 0 -2300px repeat-x url(WebResource.axd?d=whwmcqlRm44kympv-sm6UgTteYdjU2eIKmFaZnwQ1UF-p6ciWxqRXv3SxRngudx6PtadEs786_GmFXZy7ppNbBmTZK4H0kNVhzfSeYcnbwmHLEcge_60dIrvhn1dr4XGwvxx7-vW7QbZSpdZyMp9KYDf1hNdU5GnpJtIlteBI181&t=635756512660000000);");
                //               cuerpo_correo.Append(" }");
                //               cuerpo_correo.Append(".RadGrid .rgRow td, .RadGrid .rgAltRow td, .RadGrid .rgEditRow td, .RadGrid .rgFooter td, .RadGrid .rgFilterRow td, .RadGrid .rgHeader, .RadGrid .rgResizeCol, .RadGrid .rgGroupHeader td {");
                //               cuerpo_correo.Append("    padding-left: 7px;");
                //               cuerpo_correo.Append("    padding-right: 7px;");
                //               cuerpo_correo.Append("}");
                //               cuerpo_correo.Append("table {");
                //               cuerpo_correo.Append("   border-collapse: separate;");
                //               cuerpo_correo.Append("    text-indent: initial;");
                //               cuerpo_correo.Append("    border-spacing: 2px;}  ");
                //               cuerpo_correo.Append(" .RadGrid_Default { border: 1px solid #828282;     background: #fff;     color: #333; }  </style> ");

                //               cuerpo_correo.Append("  <body>  <table style='font-family: Verdana; font-size: 8pt'> <tr><td align='center'>&nbsp;</td><td align='left'> ");
                //               cuerpo_correo.Append("  <div id='rgDetallesPanel'><div id='rgDetalles' class='RadGrid RadGrid_Default' autopostback='True' style='border-style:None;width:1850px;'>");
                //               cuerpo_correo.Append("  <table cellspacing='0' class='rgMasterTable' id='rgDetalles_ctl00' style='width:1850px;table-layout:auto;empty-cells:show;'> ");
                //               cuerpo_correo.Append(" 	<colgroup> <col style='width:100px' /> <col style='width:180px' /> <col style='width:80px' /> <col style='width:80px' /> <col style='width:80px' /> ");
                //               cuerpo_correo.Append(" 	<col style='width:80px' /> <col style='width:120px' /> <col style='width:120px' /> <col style='width:80px' /> <col style='width:80px' /> ");
                //               cuerpo_correo.Append("  <col style='width:80px' />  <col style='width:300px' /> <col style='width:120px' /> </colgroup> <thead> ");
                //               cuerpo_correo.Append(" 		<tr> <th scope = 'col' class='rgHeader'>Clave del Producto</th> <th scope = 'col' class='rgHeader'>Descripción</th> <th scope = 'col' class='rgHeader' style='text-align:center;'>Cantidad</th> ");
                //               cuerpo_correo.Append("<th scope = 'col' class='rgHeader' style='text-align:center;'>P.Venta<br> Ingresado</th> ");
                //               cuerpo_correo.Append("<th scope = 'col' class='rgHeader' style='text-align:center;'>Precio<br> Lista</th> ");
                //               cuerpo_correo.Append("<th scope = 'col' class='rgHeader' style='text-align:center;'>P.Venta<br> Min Rik</th> ");
                //               cuerpo_correo.Append("<th scope = 'col' class='rgHeader' style='text-align:center;'>P.Venta<br> Min Gerente</th> ");
                //               cuerpo_correo.Append(" <th scope = 'col' class='rgHeader' style='text-align:center;'>Utilidad Pvta<br> - Precio AAA</th> ");
                //               cuerpo_correo.Append("<th scope = 'col' class='rgHeader' style='text-align:center;'>% Ut</th> ");
                //               cuerpo_correo.Append("<th scope = 'col' class='rgHeader' style='text-align:center;'>Importe<br> Venta</th> ");
                //               cuerpo_correo.Append("<th scope = 'col' class='rgHeader' style='text-align:center;'>Total<br> Utilidad</th> ");
                //               cuerpo_correo.Append("<th scope = 'col' class='rgHeader'>Motivo</th> ");
                //               cuerpo_correo.Append("<th scope = 'col' class='rgHeader' style='text-align:center;'>Otro Motivo</th> " );


                // cuerpo_correo.Append("</tr></thead><tbody><tr class='rgRow' id='rgDetalles_ctl00__0'><td align = 'right' ><span> " + alertaautorizacion.Id_Prd.ToString()  + " </span></td > ");


                //   cuerpo_correo.Append("		<td align='left'><span id='rgDetalles_ctl00_ctl04_DescripcionLabel'>" + alertaautorizacion.Prd_Descripcion.Substring(1,80).ToString() + "</span></td>");
                //cuerpo_correo.Append("		<td align='right'><span id='rgDetalles_ctl00_ctl04_lblCantidad'>" + alertaautorizacion.Cantidad.ToString() + "</span></td>");
                //cuerpo_correo.Append("		<td align='right'><span id='rgDetalles_ctl00_ctl04_lblPrecio_Vta'>" + alertaautorizacion.Precio_Vta.ToString() + "</span></td>");
                //cuerpo_correo.Append("		<td align='right'><span id='rgDetalles_ctl00_ctl04_lblPrecioLista'>" + alertaautorizacion.PrecioLista.ToString() + "</span></td>");
                //cuerpo_correo.Append("		<td align='right'><span id='rgDetalles_ctl00_ctl04_lblPrecio_MinimoRik'>" + alertaautorizacion.Precio_MinimoRik.ToString() + "</span></td>");
                //cuerpo_correo.Append("		<td align='right'><span id='rgDetalles_ctl00_ctl04_lblPrecio_MinimoGte'>" + alertaautorizacion.Precio_MinimoGte.ToString() + "</span></td>");
                //cuerpo_correo.Append("		<td align='right'><span id='rgDetalles_ctl00_ctl04_lblUtilidad'>" + alertaautorizacion.Importe_Utilidad.ToString() + "</span></td>");
                //cuerpo_correo.Append("		<td align='right'><span id='rgDetalles_ctl00_ctl04_lblPorc_Utilidad'>" + alertaautorizacion.Porc_Utilidad.ToString() + " %</span></td>");
                //cuerpo_correo.Append("		<td align='right'><span id='rgDetalles_ctl00_ctl04_lblImporte'>4420</span></td>");
                //cuerpo_correo.Append("		<td align='right'><span id='rgDetalles_ctl00_ctl04_lblImporte_Utilidad'>1210.34</span></td>");
                //cuerpo_correo.Append("		<td></td><td align='left'><span id='rgDetalles_ctl00_ctl04_lblJustificacion'>por cambio producto</span></td>");

                //cuerpo_correo.Append(" </tr> 	</tbody></table></body></table></div></html>");





                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                sm.EnableSsl = true;
                MailMessage m = new MailMessage();
                m.From = new MailAddress(configuracion.Mail_Remitente);
                char[] splitchar = { ';' };
                string Correo = "francisco.cepeda@Gibraltar.com.mx";
                string[] Correos = Correo.Split(splitchar);

                foreach (string correo in Correos)
                {
                    m.To.Add(new MailAddress(correo));
                }


                m.Subject = "solicitud de aplicación de precios especiales - " + session.Cd_Nombre;
                m.IsBodyHtml = true;
                string body = cuerpo_correo.ToString();
                AlternateView vistaHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                //Esto queda dentro de un try por si llegan a cambiar la imagen el correo como quiera se mande
                try
                {
                    LinkedResource logo = new LinkedResource(MapPath(@"Imagenes/logo.jpg"), MediaTypeNames.Image.Jpeg);
                    logo.ContentId = "companylogo";
                    vistaHtml.LinkedResources.Add(logo);
                }
                catch (Exception)
                {
                }

                m.AlternateViews.Add(vistaHtml);
                try
                {
                    sm.Send(m);

                }
                catch (Exception)
                {

                    Alerta("Error al enviar el correo. Favor de revisar la configuración del sistema");
                    return;
                }


                //Session["lAurizacionPrecios" + Session.SessionID] = null;
                //Session["Id_FacPrec" + Session.SessionID] = null;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CerrarVentana()
        {
            try
            {
                string funcion;
                funcion = "CloseAndRebind()";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected string FormatearCantidad(object cantidad)
        {
            if (cantidad == null || cantidad == DBNull.Value)
                return "";
            return string.Format("{0:###,##0}", Convert.ToInt64(cantidad));
        }

        #endregion



        #region ErrorManager
        private void RadConfirm(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radconfirm('" + mensaje + "<br /><br />', confirmCallBackFn, 330, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }

        }
        private void AlertaFocus(string mensaje, string rtb)
        {
            try
            {
                RAM1.ResponseScripts.Add("AlertaFocus('" + mensaje + "','" + rtb + "');");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
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
        private void AlertaCerrar(string mensaje)
        {
            try
            {
                mensaje = mensaje.Replace(Convert.ToChar(10).ToString(), string.Empty);
                mensaje = mensaje.Replace(Convert.ToChar(13).ToString(), string.Empty);
                RAM1.ResponseScripts.Add("CloseWindowA('" + mensaje + "');");
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
                this.LblMensaje.Text = "";
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
                this.LblMensaje.Text = Message;
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
                this.LblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();

            }
            catch (Exception ex)
            {
                this.LblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }
        #endregion

        #region enviar todo 

        protected void cmbMotivoTodo_SelectedIndexChanged1(object sender, EventArgs e)
        {
            try
            {

                if (Convert.ToInt32(cmbMotivoTodo.SelectedItem.Value) == 3)
                {
                    txtOtroMotivo.Visible = true;
                    lblotromotivo.Visible = true;
                    txtOtroMotivo.Focus();
                }
                else
                {
                    txtOtroMotivo.Visible = false;
                    lblotromotivo.Visible = false;
                    txtOtroMotivo.Text = "";
                }

                if (Convert.ToInt32(cmbMotivoTodo.SelectedItem.Value) == 1)
                {

                    DateTime fechamáxima7 = new DateTime();
                    fechamáxima7 = DateTime.Now.AddDays(7);


                    string mensaje = "";
                    mensaje = "Si elige sustitución la fecha de vigencia no puede ser mayor a 7 días : " + fechamáxima7.ToShortDateString();
                    RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
                }

                if (Convert.ToInt32(cmbMotivoTodo.SelectedItem.Value) == 6)
                {
                    //valido si es cuenta Nacional
                    if (txtTipoCliente.Text != "CN" && txtTipoCliente.Text != "CC")
                    {
                        string mensaje = "";
                        mensaje = "Este cliente no esta vinculado a una matriz de cuentas nacionales, elija otro motivo.";
                        RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
                        cmbMotivoTodo.SelectedIndex = -1;
                    }

                }


            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        protected void btnActualizar_Click(object sender, EventArgs e)
        {

            try
            {

                if (txtJustificacion.Text == "")
                {
                    Alerta("Debe elegir una justificación.");
                    return;
                }

                if (Convert.ToInt32(cmbMotivoTodo.SelectedItem.Value) == -1)
                {
                    Alerta("Seleccione un motivo.");
                    return;
                }

                    //Id_CteDet = Convert.ToInt32(((Label)gi.FindControl("Label0")).Text);
                    //Ar_dr = dt.Select("Id_CteDet='" + Id_CteDet + "'");
                    //if (Ar_dr.Length > 0)
                    //{
                    //    Ar_dr[0].Delete();
                    //    dt.AcceptChanges();
                    //}

                    for (int x = 0; x < ListDet.Count; x++)
                {
                    AlertaAutorizacion c = new AlertaAutorizacion();
                    c = ListDet[x];

                    if (Convert.ToInt32(cmbMotivoTodo.SelectedItem.Value) == 3)
                    {
                        c.Id_Motivo = 3;
                        if (txtOtroMotivo.Text == "")
                        {
                            Alerta("Si elige el Mótivo Otro, debe definir el motivo.");
                            return;
                        }
                        else
                        {
                            if (c.TipoAutorizacion == 6)
                            {
                                c.Justificacion = c.Justificacion + txtOtroMotivo.Text;
                            }
                            else
                            {
                                c.Justificacion = txtOtroMotivo.Text;
                            }
                        }

                    }
                    if (txtJustificacion.Text != "")
                    {
                        if (c.TipoAutorizacion == 6)
                        {
                            c.JustificacionMemo = c.JustificacionMemo + txtJustificacion.Text;
                        }
                        else
                        {
                            c.JustificacionMemo = txtJustificacion.Text;
                        }
                         
                    }

                    if (Convert.ToInt32(cmbMotivoTodo.SelectedItem.Value) != -1)
                    {
                        c.Id_Motivo = Convert.ToInt32(cmbMotivoTodo.SelectedItem.Value);
                        c.Nom_Motivo = cmbMotivoTodo.SelectedItem.Text;
                        c.MotivoRechazo = cmbMotivoTodo.SelectedItem.Text;
                    }


                    //fecha vigencia si producto es de tipo químicos no podrán poner más de un año y el resto 6 meses
                    //23 es quimicos 
                    //10ago24 por default son 3 meses en ambos
                    if (c.Id_Cpr != 23)
                    {
                        c.FechaVigencia = DateTime.Now.AddMonths(3);
                    }
                    else
                    {
                        c.FechaVigencia = DateTime.Now.AddMonths(3);
                    }


                    if (Convert.ToInt32(cmbMotivoTodo.SelectedItem.Value) == 1)
                    {
                        DateTime fechamáxima7 = new DateTime();
                        fechamáxima7 = DateTime.Now.AddDays(7);
                        c.FechaVigencia = Convert.ToDateTime(fechamáxima7);
                    }


                }

                rgDetalles.DataSource = ListDet;
                rgDetalles.DataBind();
                btnenviartodo.Visible = true;

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void btnEnviartodo_Click(object sender, EventArgs e)
        {

            try
            {

                string mensajefinal = "Productos enviados: ";

                List<AlertaAutorizacion> ListDetcopia = new List<AlertaAutorizacion>(ListDet);

                //List<AlertaAutorizacion> ListDetcopia;

                //ListDetcopia = ListDet;


                //si el tipo autorizacion es 6 
                //enviar a grabar el encabezado y cuando rergrese el id generado 
                //que guarde en la solicitud detalle ese Id
                //
                // este id lo uso para insertar el encabezado solo una vez cuando es tipo aut 6 
                // si vae 0 inserto y si no entonces no inserto encabezado
                int insertaencabezado = 0;
                int IdAutorizacionPrecio_gpma = 0;

                for (int x = 0; x < ListDetcopia.Count; x++)
                {

                    Int64 Id_Prd;
                    Id_Prd = Convert.ToInt64(ListDetcopia[x].Id_Prd.ToString());

                    AlertaAutorizacion solicitud = new AlertaAutorizacion();
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];


                    AlertaAutorizacion c = new AlertaAutorizacion();
                    c = ListDetcopia.FirstOrDefault(o => o.Id_Prd == Id_Prd);

                    string justificacion = c.Justificacion;
                    string justificacionmemo = c.JustificacionMemo;
                    DateTime? fechavigencia = c.FechaVigencia;
                    string justificacionGeneral = txtComentarioGeneral.Text;
                    
                    string combocomentarios = c.MotivoRechazo;

                    switch (combocomentarios)
                    {
                        case "No se tenía en existencia y se sustituyó":
                            c.Id_Motivo = 1;
                            break;

                       // case "Así se negoció con el cliente por el RIK":
                       //     c.Id_Motivo = 2;
                       //    break;
                        case "Incremento en proceso":
                            c.Id_Motivo = 2;
                            break;

                        case "Precios de Competencia":
                            c.Id_Motivo = 4;
                            break;
                        case "Tiene Convenio":
                            c.Id_Motivo = 5;
                            break;
                        case "Es Cuenta Nacional":
                            c.Id_Motivo = 6;
                            break;
                        case "Otro":
                            c.Id_Motivo = 3;
                            if (justificacion == "")
                            {
                                Alerta("Debe Alimentar el motivo por el cuál desea realizar la solicitud de precio especial");
                                return;
                            }
                            break;
                        case "Precio por licitación":
                            c.Id_Motivo = 7;
                            break;

                        default:
                            Console.WriteLine("Default case");
                            if  (combocomentarios ==  null)
                            { 
                                Alerta("Debe Alimentar el motivo por el cuál desea realizar la solicitud de precio especial");
                                return;
                            }
                            c.Id_Motivo = 8;
                            break;
                    };


                    int motivo = c.Id_Motivo;
                    if (motivo < 1)
                    {
                        Alerta("Debe Alimentar el motivo por el cuál desea realizar la solicitud de precio especial");
                        return;
                    }
                    c.Id_Motivo = motivo;
                    if (c.Id_Motivo == 3)
                    {
                        if (justificacion == "")
                        {
                            Alerta("Debe Alimentar el motivo por el cuál desea realizar la solicitud de precio especial");
                            return;
                        }
                    }
                    else
                    {
                        c.Justificacion = "";
                    }

                    if (fechavigencia <= DateTime.Now)
                    {
                        Alerta("La fecha de vigencia debe ser mayor al día de hoy.");
                        return;
                    }



                    if (justificacionmemo == "")
                    {
                        Alerta("Debe Alimentar brevemente una justificación, por la cuál desea realizar la solicitud de precio especial");
                        return;
                    }

                    if ( c.TipoAutorizacion==6)
                    {
                        if (justificacionGeneral=="")
                        {
                            Alerta("Debe Alimentar adicionalmente una justificación General cuando desea realizar la solicitud de precio especial desde GPMA");
                            return;
                        }
                    }
                    ///
                    ////enviar a autorización 

                    AlertaAutorizacion alertaautorizacion = new AlertaAutorizacion();
                    alertaautorizacion.Justificacion = justificacion;
                    alertaautorizacion.Estatus = 1;
                    alertaautorizacion.JustificacionMemo = justificacionmemo;
                    alertaautorizacion.FechaVigencia = fechavigencia;

                    alertaautorizacion.Id_Emp = sesion.Id_Emp;
                    alertaautorizacion.Id_Cd = sesion.Id_Cd;
                    alertaautorizacion.IdRepresentante = c.IdRepresentante;
                    alertaautorizacion.Id_Cte = Convert.ToInt32(txtCliente.Text);
                    if (c.Id_Ter == 0)
                    {
                        alertaautorizacion.Id_Ter = Convert.ToInt32(txtTerritorio.Text);
                    }
                    else
                    {
                        alertaautorizacion.Id_Ter = c.Id_Ter;
                    }
                    alertaautorizacion.Id_Ter = Convert.ToInt32(txtTerritorio.Text);
                    alertaautorizacion.TipoAutorizacion = c.TipoAutorizacion;
                    alertaautorizacion.Id_Prd = c.Id_Prd;
                    alertaautorizacion.Precio_Vta = c.Precio_Vta;
                    ///JFCV precioobjetivo 31 oct 
                    alertaautorizacion.PrecioObjetivo = c.PrecioObjetivo;
                    alertaautorizacion.Id_Tamaño = c.Id_Tamaño;
                    alertaautorizacion.Precio_MinimoRik = c.Precio_MinimoRik;
                    alertaautorizacion.IdAutorizacionAnterior = c.IdAutorizacionAnterior;
                    alertaautorizacion.Precio_MinimoGte = c.Precio_MinimoGte;
                    alertaautorizacion.Precio_VtaAutorizado = c.Precio_VtaAutorizado;
                    alertaautorizacion.FechaSolicitud = DateTime.Now;
                    alertaautorizacion.Prd_Descripcion = c.Prd_Descripcion;
                    if (c.Precio_Vta < c.Precio_MinimoGte)
                    {
                        alertaautorizacion.Req_Aut_Director = 1;
                    }
                    else
                    {
                        alertaautorizacion.Req_Aut_Director = 0;
                    }
                    alertaautorizacion.Estatus = 1;
                    alertaautorizacion.IdUSolicita = sesion.Id_U;
                    alertaautorizacion.Fecha_UltMod = DateTime.Now;
                    alertaautorizacion.Activo = true;
                    alertaautorizacion.Justificacion = c.Justificacion;
                    alertaautorizacion.JustificacionGte = "";
                    alertaautorizacion.PrecioLista = c.PrecioLista;
                    alertaautorizacion.Id_Motivo = c.Id_Motivo;
                    alertaautorizacion.JustificacionGeneral = justificacionGeneral;
                    // Que no se grabe factura 19 junio
                    hdId_Tamaño.Value = c.Id_Tamaño;
                    CN_CatTerritorios cn_terr = new CN_CatTerritorios();
                    Territorios terr = new Territorios();

                    if (alertaautorizacion.IdRepresentante == -1)
                    {
                        terr.Id_Emp = sesion.Id_Emp;
                        terr.Id_Cd = sesion.Id_Cd_Ver;
                        terr.Id_Ter = Convert.ToInt32(txtTerritorio.Text);
                        cn_terr.ConsultaTerritorios(ref terr, sesion.Emp_Cnx);

                        alertaautorizacion.IdRepresentante = Convert.ToInt32(terr.Id_Rik.ToString());
                    }

                    EstadisticaRentabilidad estrentabilidad = new EstadisticaRentabilidad();
                    if (alertaautorizacion.TipoAutorizacion == 1)
                    {
                        estrentabilidad = (EstadisticaRentabilidad)Session["CalculoUafir" + Session.SessionID];
                        estrentabilidad.Id_Cte = 0;
                    }
                    int registrarlo = 0;
                    if (motivo == 5)
                    {
                        string ConexionCentral = ConfigurationManager.AppSettings["strConnectionCentral"].ToString().Trim();
                        ConvenioDet conv;
                        ConvenioDet convdet;
                        CN_Convenio cn_conv;
                        conv = new ConvenioDet();
                        convdet = new ConvenioDet();
                        cn_conv = new CN_Convenio();
                        conv.Id_Emp = sesion.Id_Emp;
                        conv.Id_Cd = sesion.Id_Cd_Ver;
                        conv.Id_Cte = alertaautorizacion.Id_Cte;
                        conv.Id_Prd = alertaautorizacion.Id_Prd;
                        double PrecioIngresado = alertaautorizacion.Precio_Vta;
                        //double impore =  dtTemp.Rows[x]["Prd_Precio"].ToString().Trim()!= DBNull.Value  ? (double)(dtTemp.Rows[x]["Prd_Precio"]) : 0;

                        if (conv.Id_Prd <= 999999999999)
                        {
                            cn_conv.Convenio_ConsultaPrecio(conv, ref convdet, ConexionCentral);
                        }
                        if (convdet != null && convdet.PCD_PrecioAAAEsp > 0)
                        {
                            registrarlo = 0;
                        }
                        else
                        {
                            string mensaje2 = "";
                            mensaje2 = "Este cliente - producto no tienen un convenio vigente.";
                            RAM1.ResponseScripts.Add("radalert('" + mensaje2 + "', 330, 150);");
                            registrarlo = 1;
                            mensajefinal = "Los productos que tengan el motivo de Tiene Convenio no fueron enviados.";

                        }
                    }



                    int verificador = 0;

                    if (registrarlo == 0)
                    {

                        string mensaje = "";
                        if (tipoAutorizacion.Value == "6")
                        {
                            alertaautorizacion.Id_ReporteGP = Convert.ToInt32(hdId_ReporteGP.Value);
                            alertaautorizacion.IdRepresentante = Convert.ToInt32(hdId_Rik.Value);
                            if (insertaencabezado == 0)
                            {
                                mensaje = EnviaraAutorizarGPMa(alertaautorizacion, estrentabilidad, ref verificador);
                                if (verificador > 0)
                                {
                                    IdAutorizacionPrecio_gpma = verificador;
                                    insertaencabezado = 1;
                                }
                            }
                            alertaautorizacion.IdAutorizacionPrecio = IdAutorizacionPrecio_gpma;
                            mensaje = EnviaraAutorizarGPMaDetalle(alertaautorizacion, estrentabilidad, ref verificador);
                        }
                        else
                        {
                            alertaautorizacion.IdAutorizacionPrecio = IdAutorizacionPrecio_gpma;
                            mensaje = EnviaraAutorizar(alertaautorizacion, estrentabilidad, ref verificador);
                        }

                        if (verificador > 0)
                        {
                            ListDet.Remove(ListDet.Where(xx => xx.Id_Prd == Id_Prd).ToList()[0]);

                            mensajefinal = mensajefinal + Id_Prd.ToString() + ",";

                        }
                    }

                }

                rgDetalles.DataSource = ListDet;
                rgDetalles.DataBind();
                Alerta(mensajefinal);

                if (ListDet.Count == 0)
                {

                    switch (tipoAutorizacion.Value)
                    {
                        case "1":
                            CerrarVentana("grabarVal");
                            break;
                        case "2":
                            //JFCV 26dic2022 cuando enviaba a autorizar no eliminaba la información 
                            Session["lAurizacionPrecios" + Session.SessionID] = null;
                            Session["Id_FacPrec" + Session.SessionID] = null;
                            CerrarVentana();
                            break;

                        case "4":

                            Session["Respuesta" + Session.SessionID] = true;
                            string funcion;
                            funcion = "CloseWindowPedido()";
                            string script = "<script>" + funcion + "</script>";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);

                            break;
                        case "3":
                            CerrarVentanaRem("si"); //remisiones
                            break;

                        case "6":
                            //pantalla GPMA  
                            Session["lAurizacionPrecios" + Session.SessionID] = null;
                            Session["Id_FacPrec" + Session.SessionID] = null;
                            CerrarVentana();
                            break;


                        case "5": //facturas
                            if (hdBloquea_Facturas.Value.ToString() == "1")
                            {
                                if (hdId_Tamaño.Value.ToString() == "A")
                                {
                                    CerrarVentana("AceptarPrecios");
                                }
                                else
                                {
                                    CerrarVentana("CancelarAlertaPrecios"); //facturas
                                }
                            }
                            else
                            {
                                //si configuración esta apagada que grabe la factura 
                                CerrarVentana("AceptarPrecios");
                            }
                            break;
                        default:
                            CerrarVentana("AceptarPrecios");
                            break;
                    }


                }
                else
                {
                    Console.WriteLine("No pudo insertar la información");

                    return;
                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }




        #endregion
    }
}