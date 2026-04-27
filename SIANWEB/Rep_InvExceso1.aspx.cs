using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using Telerik.Web.UI;
using CapaNegocios;
using System.IO;
using CapaDatos;
using LibreriaReportes;
using System.Collections;
using System.Text;
using System.Data;
using FusionCharts.Charts;


namespace SIANWEB
{
    public partial class Rep_InvExceso1 : System.Web.UI.Page
    {
        #region Variables
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

        #endregion
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (ValidarSesion())
                {
                    if (!Page.IsPostBack)
                    {
                        if (sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }

                        Funciones funcion = new Funciones();
                        int Centro = Convert.ToInt32(Page.Request.QueryString["Centro"]);
                        lblLeyenda.Text = lblLeyenda.Text.Replace("[Sucursal]", Centro == -1 ? "Todos" : Centro.ToString());
                        lblLeyenda.Text = lblLeyenda.Text.Replace("[Fecha]", funcion.GetLocalDateTime(sesion.Minutos).ToString("dd/MM/yyyy hh:mm:ss tt"));
                    }
                    cargargrafica();

                    //GeneraGraficaDistribucion();

                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        #endregion
        #region Funciones
        private bool ValidarSesion()
        {
            try
            {
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    //Session["dir" + Session.SessionID] = pag[pag.Length - 1];                    Response.Redirect("login.aspx" , false);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);

                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void cargargrafica()
        {

            var dataValuePair = new List<KeyValuePair<string, double>>();
            dataValuePair.Add(new KeyValuePair<string, double>("Venezuela", 290));
            dataValuePair.Add(new KeyValuePair<string, double>("Saudi", 260));
            dataValuePair.Add(new KeyValuePair<string, double>("Canada", 180));
            dataValuePair.Add(new KeyValuePair<string, double>("Iran", 140));
            dataValuePair.Add(new KeyValuePair<string, double>("Russia", 115));
            dataValuePair.Add(new KeyValuePair<string, double>("UAE", 100));
            dataValuePair.Add(new KeyValuePair<string, double>("US", 30));
            dataValuePair.Add(new KeyValuePair<string, double>("China", 30));
            StringBuilder jsonData = new StringBuilder();
            StringBuilder data = new StringBuilder();

            // store chart config name-config value pair
            Dictionary<string, string> chartConfig = new Dictionary<string, string>();
            chartConfig.Add("caption", "Countries With Most Oil Reserves [2017-18]");
            chartConfig.Add("subCaption", "In MMbbl = One Million barrels");
            chartConfig.Add("xAxisName", "Country");
            chartConfig.Add("yAxisName", "Reserves (MMbbl)");
            chartConfig.Add("numberSuffix", "k");
            chartConfig.Add("theme", "fusion");

            // json data to use as chart data source
            jsonData.Append("{'chart':{");
            foreach (var config in chartConfig)
            {
                jsonData.AppendFormat("'{0}':'{1}',", config.Key, config.Value);
            }
            jsonData.Replace(",", "},", jsonData.Length - 1, 1);

            // build  data object from label-value pair
            data.Append("'data':[");
            foreach (KeyValuePair<string, double> pair in dataValuePair)
            {
                data.AppendFormat("{{'label':'{0}','value':'{1}'}},", pair.Key, pair.Value);
            }
            data.Replace(",", "]", data.Length - 1, 1);
            jsonData.Append(data.ToString());
            jsonData.Append("}");

            //Create chart instance
            // charttype, chartID, width, height, data format, data
            Chart MyFirstChart = new Chart("column2d", "first_chart", "800", "550", "json", jsonData.ToString());

            // render chart
            Literal1.Text = MyFirstChart.Render();


        }

        public void GeneraGraficaDistribucion()
        {
            ValidarSesion();
            string caption = "Exceso de inventario";
            string subcaption = "Click en la columna para ver el detalle";
            string yAxsisName = "Costo de exceso de inventario";
            string xAxsisName = "";
            double Total = 0;

            int dias = Convert.ToInt32(Page.Request.QueryString["Dias"]);
            int proveedor = Convert.ToInt32(Page.Request.QueryString["Proveedor"]);
            int centro = Convert.ToInt32(Page.Request.QueryString["Centro"]);
            int tproducto = Convert.ToInt32(Page.Request.QueryString["Tproducto"]);
            int indicador = Convert.ToInt32(Page.Request.QueryString["indicador"]);

            string indicadorTexto = "";

            if (indicador == -1)
                indicadorTexto = " TODOS";
            else if (indicador == 1)
                indicadorTexto = " que ROTA";
            else
                indicadorTexto = " que NO ROTA";


            DataTable valores = new DataTable();
            valores.Columns.Add("Dias");
            valores.Columns.Add("Costo");
            CN_Rep_InvExceso cn_exceso = new CN_Rep_InvExceso();
            RepExcesos exceso = new RepExcesos();
            Funciones funcionf = new Funciones();
            exceso.Id_Emp = sesion.Id_Emp;
            exceso.Id_Cd = sesion.Id_Cd_Ver;
            exceso.Centro = centro;
            exceso.Proveedor = proveedor;
            exceso.Dias = dias;
            //exceso.FechaReporte =  funcionf.GetLocalDateTime(sesion.Minutos);
            exceso.Tproducto = tproducto;
            exceso.Salida = 1;
            exceso.Id_U = sesion.Id_U;
            exceso.Indicador = indicador;
            cn_exceso.ConsultaGrafica(exceso, ref valores, sesion.Emp_Cnx);

            //valores.Add(0);
            //valores.Add(91457);
            //valores.Add(567638);
            //valores.Add(254054);
            //valores.Add(148640);
            //valores.Add(1164030);

            int columnas = 0;
            switch (dias)
            {
                case 30:
                    columnas = 6;
                    break;
                case 60:
                    columnas = 3;
                    break;
                case 90:
                case 120:
                    columnas = 2;
                    break;
            }
            double value;
            StringBuilder xmlData = new StringBuilder();
            xmlData.Append("<chart subCaption='" + subcaption + "' Caption='" + caption + indicadorTexto + "' xAxisName='" + xAxsisName + "' yAxisName='" + yAxsisName + "' showValues='1' formatNumberScale='0' showBorder='0' numberPrefix='$' showSum='1' decimals='4'>");
            for (int i = 0; i < columnas; i++)
            {
                if (valores.Select("Dias='" + (dias * (i + 1)) + "'").Count() == 0)
                {
                    value = 0;
                }
                else
                {
                    value = Convert.ToDouble(valores.Select("Dias='" + (dias * (i + 1)) + "'")[0].ItemArray[1]);
                }
                xmlData.Append("<set label='Días: " + (dias * (i + 1)).ToString() + "' value='" + value + "' link='javascript:myJS(" + proveedor + "," + centro + "," + tproducto + ", " + (dias * (i + 1)).ToString() + "," + indicador + "," + dias + ")'/>");
                Total += value;
            }
            xmlData.Append("<set label='Total' value='" + Total + "' link='javascript:myJS(" + proveedor + "," + centro + "," + tproducto + ", -1 ," + indicador + "," + dias + ")'/>");
            xmlData.Append("<styles>");
            xmlData.Append("<definition>");
            xmlData.Append("<style name='myCaptionFont' type='font' font='Arial' size='14' bold='1'/>");
            xmlData.Append("</definition>");
            xmlData.Append("<application>");
            xmlData.Append("<apply toObject='Caption' styles='myCaptionFont'/>");
            xmlData.Append("</application>");
            xmlData.Append("</styles>");
            xmlData.Append("</chart>");

            Chart factoryOutput = new Chart("msline", "myChart", "600", "350", "xml", xmlData.ToString());
            Literal1.Text = factoryOutput.Render();


            //return InfoSoftGlobal.FusionCharts.RenderChartHTML("FusionCharts/Column3D", "", xmlData.ToString(), "myNext", "100%", "300", false);
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
    }
}