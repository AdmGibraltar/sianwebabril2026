using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CapaEntidad;
using CapaNegocios;
using System.Data;
using System.IO;
using System.Reflection;
using ClosedXML.Excel;

namespace SIANWEB.Dashboard
{
    public partial class dshReporteVentaDinamico : System.Web.UI.Page
    {
        public string TotalAcumulado = "";
        public string TotalUnidades = "";
        public string TotalTrimestre = "";
        public string VentaMes = "";
        public string strTrimestre = "";
        public string strEstemes = "";

        public string strListadoClientes = "";
        public string strListadoProductos = "";
        public string strListadoAgrupador = "";
        public string strHeaderRpt = "";
        public string strTablaRpt = "";

        public string strTituloDelReporte = "";

        public List<RVDReporte> ExcelReporteVentas;

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
                    Response.Redirect("../Login.aspx", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        hdfopcMenu.Value = "1";
                        CargarCentros();
                        CargarAño();
                        CargarAñosSemana();
                        llenaDashboard();
                    }
                }
            }
            catch (Exception ex)
            {
                this.ErrorManager(ex, "Page_Load");
            }
        }


        #region funciones

        void CargarCentros()
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_dshIndicadoresACyS clsdshIndicadoresACyS = new CN_dshIndicadoresACyS();
                List<Renglon> lista = new List<Renglon>();

                clsdshIndicadoresACyS.LlenaRenglones(sesion.Id_Emp, sesion.Id_Cd, sesion.Id_U, "spCatCentroDistribucion_Combo", sesion.Emp_Cnx,
                    ref lista);
                this.drpCDI.DataValueField = "idRng";
                this.drpCDI.DataTextField = "sValor";
                this.drpCDI.DataSource = lista.OrderBy(i => i.sValor);
                this.drpCDI.DataBind();

                this.drpCDI.SelectedValue = sesion.Id_Cd_Ver.ToString();
                if (lista.Count == 1)
                {
                    this.drpCDI.Enabled = false;
                    parrCDI.InnerText = "Centro de Distribución: " + drpCDI.SelectedItem.Text + "";
                    drpCDI.Visible = false;
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private void CargarAño()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_dshIndicadoresACyS clsdshIndicadoresACyS = new CN_dshIndicadoresACyS();
                List<Renglon> lista = new List<Renglon>();

                clsdshIndicadoresACyS.LlenaRenglones3(Sesion.Id_Emp, Sesion.Id_Cd, Sesion.Id_U, "spCatCalendarioAnhio2_Combo", Sesion.Emp_Cnx,
                    ref lista);
                this.cmbAniio.DataValueField = "idRng";
                this.cmbAniio.DataTextField = "sValor";
                this.cmbAniio.DataSource = lista.Where(w => w.idRng >= (DateTime.Now.Year - 10)).ToList();
                this.cmbAniio.DataBind();

                cmbAniio.SelectedIndex = cmbAniio.Items.Count - 1;

            }
            catch (Exception ex)
            {
                this.ErrorManager(ex, "llenaDashboard");
            }
        }

        void CargarAñosSemana()
        {
            int anio = Convert.ToInt32(DateTime.Now.Year);
            ListItem item = new ListItem();
            for (int x = anio; x > anio - 3; x = x - 1)
            {
                item = new ListItem();
                item.Value = x.ToString();
                item.Text = x.ToString();
                cmbAniosSemana.Items.Add(item);
            }
            cmbAniosSemana.SelectedIndex = 0;
        }

        #endregion

        #region LlenarDashboard

        void llenaDashboard()
        {
            try
            {
                List<RVDIndicadores> ListIndicadores = new List<RVDIndicadores>();
                List<RVDReporte> ListReporteSinAgrupar = new List<RVDReporte>();
                /// Obtiene Datos
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_dshIndicadoresACyS clsdshReporteVentas = new CN_dshIndicadoresACyS();

                int iYear = DateTime.Now.Year;
                if (this.cmbAniio.SelectedValue != "")
                {
                    iYear = Convert.ToInt32(this.cmbAniio.SelectedValue);
                }

                int iTipo = Convert.ToInt32(hdfopcMenu.Value);
                if (hdfdsgProducto.Value == "1")
                {
                    iTipo = iTipo + 2;
                }
                if (hdfdsgCliente.Value == "1")
                {
                    iTipo = iTipo + 1;
                }

                int Indicador = 0;              ///     Ambos (por default)
                if (radMonto.Checked)           ///     Solo Pesos
                {
                    Indicador = 1;
                }
                else
                {
                    if (radUnidades.Checked)    /// Solo Unidades
                    {
                        Indicador = 2;
                    }
                }

                clsdshReporteVentas.ConsultaDashboardRVD(sesion.Id_Emp, sesion.Id_Cd, iYear, sesion.Id_U, iTipo, Indicador, sesion.Emp_Cnx,
                    ref ListIndicadores, ref ListReporteSinAgrupar
                    );
                GeneraIndicadores(ListIndicadores);

                ExcelReporteVentas = new List<RVDReporte>();
                ExcelReporteVentas = ListReporteSinAgrupar;
                LlenaReporte(ListReporteSinAgrupar, Indicador);

            }
            catch (Exception ex)
            {
                this.ErrorManager(ex, "llenaDashboard");
            }
        }

        void GeneraIndicadores(List<RVDIndicadores> ListaIndicador)
        {
            foreach (RVDIndicadores Indicar in ListaIndicador)
            {
                TotalAcumulado = Indicar.TotalAcumulado;
                TotalUnidades = Indicar.TotalUnidades;
                TotalTrimestre = Indicar.TotalTrimestre;
                VentaMes = Indicar.TotalDelMes;
                strTrimestre = Indicar.strTrimestre;
                strEstemes = Indicar.strEsteMes;
            }

        }

        void LlenaReporte(List<RVDReporte> ListaReporte, int iIndicador)
        {
            try
            {
                strHeaderRpt = "";
                strTablaRpt = "";
                strListadoClientes = "";
                strListadoProductos = "";
                strListadoAgrupador = "";
                int r = 1;
                int iiiTipo = Convert.ToInt32(hdfopcMenu.Value);
                if (iiiTipo == 1)
                {
                    strTituloDelReporte = "Reporte De Ventas Por Cliente";
                }
                else
                {
                    if (iiiTipo == 10)
                    {
                        strTituloDelReporte = "Reporte De Ventas Por Producto";
                    }
                    else
                    {
                        if ((iiiTipo >= 20) && (iiiTipo <= 39))
                        {
                            strTituloDelReporte = "Reporte De Ventas Por Territorio";
                            if (iiiTipo == 20)
                            {
                                strTituloDelReporte = strTituloDelReporte + " De Ventas";
                            }
                            if (iiiTipo == 30)
                            {
                                strTituloDelReporte = strTituloDelReporte + " De Servicio";
                            }
                        }
                        else
                        {
                            if ((iiiTipo >= 40) && (iiiTipo <= 59))
                            {
                                strTituloDelReporte = "Reporte De Ventas Por Representante";
                                if (iiiTipo == 40)
                                {
                                    strTituloDelReporte = strTituloDelReporte + " RIK";
                                }
                                if (iiiTipo == 50)
                                {
                                    strTituloDelReporte = strTituloDelReporte + " RSC";
                                }
                            }
                        }
                    }
                }


                if (hdfdsgProducto.Value == "1")
                {
                    iiiTipo = iiiTipo + 2;
                    strTituloDelReporte = strTituloDelReporte + " desglosado por Producto";
                    if (hdfdsgCliente.Value == "1")
                    {
                        strTituloDelReporte = strTituloDelReporte + " y Por Cliente";
                    }
                }
                if (hdfdsgCliente.Value == "1")
                {
                    iiiTipo = iiiTipo + 1;
                    if (hdfdsgProducto.Value != "1")
                    {
                        strTituloDelReporte = strTituloDelReporte + " desglosado por Cliente";
                    }
                }

                string strNombre = "";
                foreach (int idCte in ListaReporte.Select(o => o.Id).Distinct().ToList())
                {
                    strNombre = ListaReporte.Where(no => no.Id == idCte).FirstOrDefault().Nombre;

                    if (iiiTipo == 10)
                    {
                        strListadoProductos = strListadoProductos + ((strListadoProductos.Length == 0) ? " " : ", ") + "\"" + strNombre.Replace('"', ' ').Replace('\'', ' ').Trim() + "\"";
                    }
                    else
                    {
                        if ((iiiTipo >= 20))
                        {
                            strListadoAgrupador = strListadoAgrupador + ((strListadoAgrupador.Length == 0) ? " " : ", ") + "\"" + strNombre.Replace('"', ' ').Replace('\'', ' ').Trim() + "\"";
                        }
                        else
                        {
                            strListadoClientes = strListadoClientes + ((strListadoClientes.Length == 0) ? " " : ", ") + "\"" + strNombre.Replace('"', ' ').Replace('\'', ' ').Trim() + "\"";
                        }
                    }

                }

                strNombre = "";
                foreach (Int64 idPrd in ListaReporte.Select(o => o.IdPrd).Distinct().ToList())
                {
                    if (idPrd != 0)
                    {
                        strNombre = ListaReporte.Where(no => no.IdPrd == idPrd).FirstOrDefault().PdrDescrip;
                        if ((iiiTipo == 23) || (iiiTipo == 21) || (iiiTipo == 33) || (iiiTipo == 31) || (iiiTipo == 43) || (iiiTipo == 41) || (iiiTipo == 53) || (iiiTipo == 51))
                        {
                            strListadoClientes = strListadoClientes + ((strListadoClientes.Length == 0) ? " " : ", ") + "\"" + strNombre.Replace('"', ' ').Replace('\'', ' ').Trim() + "\"";
                        }
                        else
                        {
                            strListadoProductos = strListadoProductos + ((strListadoProductos.Length == 0) ? " " : ", ") + "\"" + strNombre.Replace('"', ' ').Replace('\'', ' ').Trim() + "\"";
                        }
                    }
                }

                strNombre = "";
                foreach (int idAgr in ListaReporte.Select(o => o.IdAgrupa).Distinct().ToList())
                {
                    if (idAgr != 0)
                    {
                        strNombre = ListaReporte.Where(no => no.IdAgrupa == idAgr).FirstOrDefault().Agrupador;
                        if ((iiiTipo == 23) || (iiiTipo == 33) || (iiiTipo == 43) || (iiiTipo == 53))
                        {
                            strListadoProductos = strListadoProductos + ((strListadoProductos.Length == 0) ? " " : ", ") + "\"" + strNombre.Replace('"', ' ').Replace('\'', ' ').Trim() + "\"";
                        }
                        else
                        {
                            strListadoAgrupador = strListadoAgrupador + ((strListadoAgrupador.Length == 0) ? " " : ", ") + "\"" + strNombre.Replace('"', ' ').Replace('\'', ' ').Trim() + "\"";
                        }

                    }
                }

                string NombreCteAgrupa = "";
                string NombreAgrupador2 = "";
                List<RVDReporte> lista = new List<RVDReporte>();

                string strFiltroN0 = "";
                string strFiltroN1 = "";
                string strFiltroN2 = "";

                if (iiiTipo < 10)
                {
                    /// es la opcion del cliente
                    strFiltroN0 = hdfFiltroCliente.Value;
                    strFiltroN1 = hdfFiltroProducto.Value;
                }
                else
                {
                    if ((iiiTipo >= 20))
                    {
                        strFiltroN0 = hdfFiltroTerritorio.Value;
                        ///strFiltroN1 = hdfFiltroTerritorio.Value;
                        if ((hdfdsgProducto.Value == "1") || (hdfdsgCliente.Value == "1"))
                        {
                            if ((hdfdsgProducto.Value == "1") && (hdfdsgCliente.Value == "1"))
                            {
                                strFiltroN1 = hdfFiltroCliente.Value;
                                strFiltroN2 = hdfFiltroProducto.Value;
                            }
                            else
                            {
                                if (hdfdsgProducto.Value == "1")
                                {
                                    strFiltroN1 = hdfFiltroProducto.Value;
                                }
                                else
                                {
                                    strFiltroN1 = hdfFiltroCliente.Value;
                                }
                            }

                        }
                    }
                    else
                    {
                        /// es la opcion de producto, solo tiene ese filtro
                        strFiltroN0 = hdfFiltroProducto.Value;
                    }
                }

                /// Seleccion de Headers
                if (iIndicador == 0)                 /// Con Pesos y Unidades
                {
                    if (chkIncluyeUtilidad.Checked)
                        strHeaderRpt = FormatoMontoYUnidadesConUtilidad();
                    else
                        strHeaderRpt = FormatoMontoYUnidadesSinUtilidad();
                }
                else
                {
                    if (iIndicador == 1)            /// Solo Pesos 
                    {
                        if (chkIncluyeUtilidad.Checked)
                            strHeaderRpt = FormatoMontoConUtilidad();
                        else
                            strHeaderRpt = FormatoMontoSinUtilidad();
                    }
                    else
                    {
                        if (iIndicador == 2)        /// Solo Unidades
                        {
                            strHeaderRpt = FormatoUnidades();
                        }
                    }
                }

                lista = ListaReporte.Where(f => f.Nombre.Trim() == ((strFiltroN0 != "") ? strFiltroN0 : f.Nombre.Trim()) && f.PdrDescrip.Trim() == ((strFiltroN1 != "") ? strFiltroN1 : f.PdrDescrip.Trim()) && f.Agrupador.Trim() == ((strFiltroN2 != "") ? strFiltroN2 : f.Agrupador.Trim())).ToList();

                /// Total de Totales
                strTablaRpt = renglonRptTotalDeTotales(lista, "<b>Totales :</b>", iIndicador, chkIncluyeUtilidad.Checked);

                foreach (RVDReporte renglon in lista)
                {

                    if ((hdfdsgProducto.Value == "1") || (hdfdsgCliente.Value == "1"))
                    {
                        if (NombreCteAgrupa.Length == 0)
                        {
                            NombreCteAgrupa = renglon.Nombre.Trim();
                            strTablaRpt = strTablaRpt + renglonRptTotalDeTotales(lista.Where(f => f.Nombre.Trim() == NombreCteAgrupa).ToList(), "<i>Total " + NombreCteAgrupa + " :</i>", iIndicador, chkIncluyeUtilidad.Checked);
                            strTablaRpt = strTablaRpt + "<tr>" +
                                    "<td style='text-align:left; background-color: #3385ff; color:white;' class='border border-size-1 bd-black' colspan='29'><b>&nbsp;" + renglon.Id.ToString() + " " + renglon.Nombre.Trim() + "</td></tr>";
                        }
                        else
                        {
                            if (NombreCteAgrupa != renglon.Nombre.Trim())
                            {
                                NombreCteAgrupa = renglon.Nombre.Trim();
                                strTablaRpt = strTablaRpt + renglonRptTotalDeTotales(lista.Where(f => f.Nombre.Trim() == NombreCteAgrupa).ToList(), "<i>Total " + NombreCteAgrupa + " :</i>", iIndicador, chkIncluyeUtilidad.Checked);
                                strTablaRpt = strTablaRpt + "<tr>" +
                                    "<td style='text-align:left; background-color: #3385ff; color:white;' class='border border-size-1 bd-black' colspan='29'><b>&nbsp;" + renglon.Id.ToString() + " " + renglon.Nombre.Trim() + "</td></tr>";
                            }
                        }

                        if (renglon.Agrupador.Trim() != "")
                        {
                            if (NombreAgrupador2.Length == 0)
                            {
                                NombreAgrupador2 = renglon.PdrDescrip.Trim();
                                strTablaRpt = strTablaRpt + renglonRptTotalDeTotales(lista.Where(f => f.PdrDescrip.Trim() == NombreAgrupador2).ToList(), "<i>Total " + NombreAgrupador2 + " :</i>", iIndicador, chkIncluyeUtilidad.Checked);
                                strTablaRpt = strTablaRpt + "<tr><td>&nbsp;</td>" +
                                        "<td style='text-align:left; background-color: #3385ff; color:white;' class='border border-size-1 bd-black' colspan='28'><b>&nbsp;" + renglon.IdPrd.ToString() + " " + renglon.PdrDescrip.Trim() + "</td></tr>";
                            }
                            else
                            {
                                if (NombreAgrupador2 != renglon.PdrDescrip.Trim())
                                {
                                    NombreAgrupador2 = renglon.PdrDescrip.Trim();
                                    strTablaRpt = strTablaRpt + renglonRptTotalDeTotales(lista.Where(f => f.PdrDescrip.Trim() == NombreAgrupador2).ToList(), "<i>Total " + NombreAgrupador2 + " :</i>", iIndicador, chkIncluyeUtilidad.Checked);
                                    strTablaRpt = strTablaRpt + "<tr><td>&nbsp;</td>" +
                                        "<td style='text-align:left; background-color: #3385ff; color:white;' class='border border-size-1 bd-black' colspan='28'><b>&nbsp;" + renglon.IdPrd.ToString() + " " + renglon.PdrDescrip.Trim() + "</td></tr>";
                                }
                            }
                        }

                        strTablaRpt = strTablaRpt + renglonRptAG(renglon, r, iIndicador, chkIncluyeUtilidad.Checked);
                    }
                    else
                    {
                        strTablaRpt = strTablaRpt + renglonRptSA(renglon, r, iIndicador, chkIncluyeUtilidad.Checked);
                    }
                    r++;

                }

                //  hdfFiltroCliente.Value = "";
                //  hdfFiltroProducto.Value = "";
            }
            catch (Exception ex)
            {
                this.ErrorManager(ex, "LlenaReporte");
            }
        }

        string renglonRptSA(RVDReporte R, int re, int iIndicador, bool bUtilidad)
        {
            string salida = "";
            salida = "<tr>" +
                    "<td style='text-align:center;' class='border border-size-1 bd-black'>" + re.ToString() + "</td>" +
                    "<td style='text-align:center;' class='border border-size-1 bd-black'>" + R.Id.ToString() + "</td>" +
                    "<td style='text-align:left;' class='border border-size-1 bd-black'>" + R.Nombre.Trim() + "</td>" +
                    ((iIndicador == 0) || (iIndicador == 1) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.Enero.ToString("C") + "</td>" : "") +
                    ((iIndicador == 0) || (iIndicador == 2) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UnEnero.ToString() + "</td>" : "") +
                    ((bUtilidad) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UtEnero.ToString("C") + "</td>" : "") +
                    ((iIndicador == 0) || (iIndicador == 1) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.Febrero.ToString("C") + "</td>" : "") +
                    ((iIndicador == 0) || (iIndicador == 2) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UnFebrero.ToString() + "</td>" : "") +
                    ((bUtilidad) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UtFebrero.ToString("C") + "</td>" : "") +
                    ((iIndicador == 0) || (iIndicador == 1) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.Marzo.ToString("C") + "</td>" : "") +
                    ((iIndicador == 0) || (iIndicador == 2) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UnMarzo.ToString() + "</td>" : "") +
                    ((bUtilidad) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UtMarzo.ToString("C") + "</td>" : "") +
                    ((iIndicador == 0) || (iIndicador == 1) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.Abril.ToString("C") + "</td>" : "") +
                    ((iIndicador == 0) || (iIndicador == 2) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UnAbril.ToString() + "</td>" : "") +
                    ((bUtilidad) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UtAbril.ToString("C") + "</td>" : "") +
                    ((iIndicador == 0) || (iIndicador == 1) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.Mayo.ToString("C") + "</td>" : "") +
                    ((iIndicador == 0) || (iIndicador == 2) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UnMayo.ToString() + "</td>" : "") +
                    ((bUtilidad) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UtMayo.ToString("C") + "</td>" : "") +
                    ((iIndicador == 0) || (iIndicador == 1) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.Junio.ToString("C") + "</td>" : "") +
                    ((iIndicador == 0) || (iIndicador == 2) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UnJunio.ToString() + "</td>" : "") +
                    ((bUtilidad) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UtJunio.ToString("C") + "</td>" : "") +
                    ((iIndicador == 0) || (iIndicador == 1) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.Julio.ToString("C") + "</td>" : "") +
                    ((iIndicador == 0) || (iIndicador == 2) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UnJulio.ToString() + "</td>" : "") +
                    ((bUtilidad) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UtJulio.ToString("C") + "</td>" : "") +
                    ((iIndicador == 0) || (iIndicador == 1) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.Agosto.ToString("C") + "</td>" : "") +
                    ((iIndicador == 0) || (iIndicador == 2) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UnAgosto.ToString() + "</td>" : "") +
                    ((bUtilidad) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UtAgosto.ToString("C") + "</td>" : "") +
                    ((iIndicador == 0) || (iIndicador == 1) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.Septiembre.ToString("C") + "</td>" : "") +
                    ((iIndicador == 0) || (iIndicador == 2) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UnSeptiembre.ToString() + "</td>" : "") +
                    ((bUtilidad) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UtSeptiembre.ToString("C") + "</td>" : "") +
                    ((iIndicador == 0) || (iIndicador == 1) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.Octubre.ToString("C") + "</td>" : "") +
                    ((iIndicador == 0) || (iIndicador == 2) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UnOctubre.ToString() + "</td>" : "") +
                    ((bUtilidad) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UtOctubre.ToString("C") + "</td>" : "") +
                    ((iIndicador == 0) || (iIndicador == 1) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.Noviembre.ToString("C") + "</td>" : "") +
                    ((iIndicador == 0) || (iIndicador == 2) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UnNoviembre.ToString() + "</td>" : "") +
                    ((bUtilidad) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UtNoviembre.ToString("C") + "</td>" : "") +
                    ((iIndicador == 0) || (iIndicador == 1) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.Diciembre.ToString("C") + "</td>" : "") +
                    ((iIndicador == 0) || (iIndicador == 2) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UnDiciembre.ToString() + "</td>" : "") +
                    ((bUtilidad) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UtDiciembre.ToString("C") + "</td>" : "") +
                    ((iIndicador == 0) || (iIndicador == 1) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.Total.ToString("C") + "</td>" : "") +
                    ((iIndicador == 0) || (iIndicador == 2) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UnTotal.ToString() + "</td>" : "") +
                    ((bUtilidad) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UtTotal.ToString("C") + "</td>" : "") +
                "</tr>";

            return salida;
        }

        string renglonRptAG(RVDReporte R, int re, int iIndicador, bool bUtilidad)
        {
            string salida = "";
            string texto = (R.Agrupador.Trim() != "") ? R.Agrupador.Trim() : R.PdrDescrip.Trim();
            Int64 itexto = (R.Agrupador.Trim() != "") ? R.IdAgrupa : R.IdPrd;
            salida = "<tr>" +
                "<td style='text-align:center;' class='border border-size-1 bd-black'>" + re.ToString() + "</td>" +
                "<td style='text-align:center;' class='border border-size-1 bd-black'>" + itexto.ToString() + "</td>" +
                "<td style='text-align:left;' class='border border-size-1 bd-black'>" + texto + "</td>" +
                ((iIndicador == 0) || (iIndicador == 1) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.Enero.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 2) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UnEnero.ToString() + "</td>" : "") +
                ((bUtilidad) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UtEnero.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 1) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.Febrero.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 2) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UnFebrero.ToString() + "</td>" : "") +
                ((bUtilidad) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UtFebrero.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 1) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.Marzo.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 2) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UnMarzo.ToString() + "</td>" : "") +
                ((bUtilidad) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UtMarzo.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 1) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.Abril.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 2) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UnAbril.ToString() + "</td>" : "") +
                ((bUtilidad) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UtAbril.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 1) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.Mayo.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 2) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UnMayo.ToString() + "</td>" : "") +
                ((bUtilidad) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UtMayo.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 1) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.Junio.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 2) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UnJunio.ToString() + "</td>" : "") +
                ((bUtilidad) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UtJunio.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 1) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.Julio.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 2) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UnJulio.ToString() + "</td>" : "") +
                ((bUtilidad) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UtJulio.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 1) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.Agosto.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 2) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UnAgosto.ToString() + "</td>" : "") +
                ((bUtilidad) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UtAgosto.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 1) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.Septiembre.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 2) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UnSeptiembre.ToString() + "</td>" : "") +
                ((bUtilidad) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UtSeptiembre.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 1) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.Octubre.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 2) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UnOctubre.ToString() + "</td>" : "") +
                ((bUtilidad) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UtOctubre.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 1) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.Noviembre.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 2) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UnNoviembre.ToString() + "</td>" : "") +
                ((bUtilidad) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UtNoviembre.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 1) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.Diciembre.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 2) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UnDiciembre.ToString() + "</td>" : "") +
                ((bUtilidad) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UtDiciembre.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 1) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.Total.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 2) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UnTotal.ToString() + "</td>" : "") +
                ((bUtilidad) ? "<td style='text-align:right;' class='border border-size-1 bd-black'>" + R.UtTotal.ToString("C") + "</td>" : "") +
                "</tr>";

            return salida;
        }

        string renglonRptTotalDeTotales(List<RVDReporte> listado, string strTotal, int iIndicador, bool bUtilidad)
        {
            string salida = "";
            float TotalTotal = listado.Select(m => m.Enero + m.Febrero + m.Marzo + m.Abril + m.Mayo + m.Junio + m.Julio + m.Agosto + m.Septiembre + m.Octubre + m.Noviembre + m.Diciembre).Sum();
            int UnTotalTotal = listado.Select(m => m.UnEnero + m.UnFebrero + m.UnMarzo + m.UnAbril + m.UnMayo + m.UnJunio + m.UnJulio + m.UnAgosto + m.UnSeptiembre + m.UnOctubre + m.UnNoviembre + m.UnDiciembre).Sum();
            float UtTotalTotal = listado.Select(m => m.UtEnero + m.UtFebrero + m.UtMarzo + m.UtAbril + m.UtMayo + m.UtJunio + m.UtJulio + m.UtAgosto + m.UtSeptiembre + m.UtOctubre + m.UtNoviembre + m.UtDiciembre).Sum();

            float TotalEnero = listado.Select(m => m.Enero).Sum();
            float TotalFebrero = listado.Select(m => m.Febrero).Sum();
            float TotalMarzo = listado.Select(m => m.Marzo).Sum();
            float TotalAbril = listado.Select(m => m.Abril).Sum();
            float TotalMayo = listado.Select(m => m.Mayo).Sum();
            float TotalJunio = listado.Select(m => m.Junio).Sum();
            float TotalJulio = listado.Select(m => m.Julio).Sum();
            float TotalAgosto = listado.Select(m => m.Agosto).Sum();
            float TotalSeptiembre = listado.Select(m => m.Septiembre).Sum();
            float TotalOctubre = listado.Select(m => m.Octubre).Sum();
            float TotalNoviembre = listado.Select(m => m.Noviembre).Sum();
            float TotalDiciembre = listado.Select(m => m.Diciembre).Sum();

            int TotalUnEnero = listado.Select(m => m.UnEnero).Sum();
            int TotalUnFebrero = listado.Select(m => m.UnFebrero).Sum();
            int TotalUnMarzo = listado.Select(m => m.UnMarzo).Sum();
            int TotalUnAbril = listado.Select(m => m.UnAbril).Sum();
            int TotalUnMayo = listado.Select(m => m.UnMayo).Sum();
            int TotalUnJunio = listado.Select(m => m.UnJunio).Sum();
            int TotalUnJulio = listado.Select(m => m.UnJulio).Sum();
            int TotalUnAgosto = listado.Select(m => m.UnAgosto).Sum();
            int TotalUnSeptiembre = listado.Select(m => m.UnSeptiembre).Sum();
            int TotalUnOctubre = listado.Select(m => m.UnOctubre).Sum();
            int TotalUnNoviembre = listado.Select(m => m.UnNoviembre).Sum();
            int TotalUnDiciembre = listado.Select(m => m.UnDiciembre).Sum();

            float TotalUtEnero = listado.Select(m => m.UtEnero).Sum();
            float TotalUtFebrero = listado.Select(m => m.UtFebrero).Sum();
            float TotalUtMarzo = listado.Select(m => m.UtMarzo).Sum();
            float TotalUtAbril = listado.Select(m => m.UtAbril).Sum();
            float TotalUtMayo = listado.Select(m => m.UtMayo).Sum();
            float TotalUtJunio = listado.Select(m => m.UtJunio).Sum();
            float TotalUtJulio = listado.Select(m => m.UtJulio).Sum();
            float TotalUtAgosto = listado.Select(m => m.UtAgosto).Sum();
            float TotalUtSeptiembre = listado.Select(m => m.UtSeptiembre).Sum();
            float TotalUtOctubre = listado.Select(m => m.UtOctubre).Sum();
            float TotalUtNoviembre = listado.Select(m => m.UtNoviembre).Sum();
            float TotalUtDiciembre = listado.Select(m => m.UtDiciembre).Sum();


            salida = "<tr><td style='text-align:end; background-color: #3385ff; color:white;' class='border border-size-1 bd-black' colspan='3'>" + strTotal + "&nbsp;</td>" +
                ((iIndicador == 0) || (iIndicador == 1) ? "<td style='text-align:right; background-color: #3385ff; color:white;' class='border border-size-1 bd-black'>" + TotalEnero.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 2) ? "<td style='text-align:right; background-color: #3385ff; color:white;' class='border border-size-1 bd-black'>" + TotalUnEnero.ToString() + "</td>" : "") +
                ((bUtilidad) ? "<td style='text-align:right; background-color: #3385ff; color:white;' class='border border-size-1 bd-black'>" + TotalUtEnero.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 1) ? "<td style='text-align:right; background-color: #3385ff; color:white;' class='border border-size-1 bd-black'>" + TotalFebrero.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 2) ? "<td style='text-align:right; background-color: #3385ff; color:white;' class='border border-size-1 bd-black'>" + TotalUnFebrero.ToString() + "</td>" : "") +
                ((bUtilidad) ? "<td style='text-align:right; background-color: #3385ff; color:white;' class='border border-size-1 bd-black'>" + TotalUtFebrero.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 1) ? "<td style='text-align:right; background-color: #3385ff; color:white;' class='border border-size-1 bd-black'>" + TotalMarzo.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 2) ? "<td style='text-align:right; background-color: #3385ff; color:white;' class='border border-size-1 bd-black'>" + TotalUnMarzo.ToString() + "</td>" : "") +
                ((bUtilidad) ? "<td style='text-align:right; background-color: #3385ff; color:white;' class='border border-size-1 bd-black'>" + TotalUtMarzo.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 1) ? "<td style='text-align:right; background-color: #3385ff; color:white;' class='border border-size-1 bd-black'>" + TotalAbril.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 2) ? "<td style='text-align:right; background-color: #3385ff; color:white;' class='border border-size-1 bd-black'>" + TotalUnAbril.ToString() + "</td>" : "") +
                ((bUtilidad) ? "<td style='text-align:right; background-color: #3385ff; color:white;' class='border border-size-1 bd-black'>" + TotalUtAbril.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 1) ? "<td style='text-align:right; background-color: #3385ff; color:white;' class='border border-size-1 bd-black'>" + TotalMayo.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 2) ? "<td style='text-align:right; background-color: #3385ff; color:white;' class='border border-size-1 bd-black'>" + TotalUnMayo.ToString() + "</td>" : "") +
                ((bUtilidad) ? "<td style='text-align:right; background-color: #3385ff; color:white;' class='border border-size-1 bd-black'>" + TotalUtMayo.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 1) ? "<td style='text-align:right; background-color: #3385ff; color:white;' class='border border-size-1 bd-black'>" + TotalJunio.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 2) ? "<td style='text-align:right; background-color: #3385ff; color:white;' class='border border-size-1 bd-black'>" + TotalUnJunio.ToString() + "</td>" : "") +
                ((bUtilidad) ? "<td style='text-align:right; background-color: #3385ff; color:white;' class='border border-size-1 bd-black'>" + TotalUtJunio.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 1) ? "<td style='text-align:right; background-color: #3385ff; color:white;' class='border border-size-1 bd-black'>" + TotalJulio.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 2) ? "<td style='text-align:right; background-color: #3385ff; color:white;' class='border border-size-1 bd-black'>" + TotalUnJulio.ToString() + "</td>" : "") +
                ((bUtilidad) ? "<td style='text-align:right; background-color: #3385ff; color:white;' class='border border-size-1 bd-black'>" + TotalUtJulio.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 1) ? "<td style='text-align:right; background-color: #3385ff; color:white;' class='border border-size-1 bd-black'>" + TotalAgosto.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 2) ? "<td style='text-align:right; background-color: #3385ff; color:white;' class='border border-size-1 bd-black'>" + TotalUnAgosto.ToString() + "</td>" : "") +
                ((bUtilidad) ? "<td style='text-align:right; background-color: #3385ff; color:white;' class='border border-size-1 bd-black'>" + TotalUtAgosto.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 1) ? "<td style='text-align:right; background-color: #3385ff; color:white;' class='border border-size-1 bd-black'>" + TotalSeptiembre.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 2) ? "<td style='text-align:right; background-color: #3385ff; color:white;' class='border border-size-1 bd-black'>" + TotalUnSeptiembre.ToString() + "</td>" : "") +
                ((bUtilidad) ? "<td style='text-align:right; background-color: #3385ff; color:white;' class='border border-size-1 bd-black'>" + TotalUtSeptiembre.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 1) ? "<td style='text-align:right; background-color: #3385ff; color:white;' class='border border-size-1 bd-black'>" + TotalOctubre.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 2) ? "<td style='text-align:right; background-color: #3385ff; color:white;' class='border border-size-1 bd-black'>" + TotalUnOctubre.ToString() + "</td>" : "") +
                ((bUtilidad) ? "<td style='text-align:right; background-color: #3385ff; color:white;' class='border border-size-1 bd-black'>" + TotalUtOctubre.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 1) ? "<td style='text-align:right; background-color: #3385ff; color:white;' class='border border-size-1 bd-black'>" + TotalNoviembre.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 2) ? "<td style='text-align:right; background-color: #3385ff; color:white;' class='border border-size-1 bd-black'>" + TotalUnNoviembre.ToString() + "</td>" : "") +
                ((bUtilidad) ? "<td style='text-align:right; background-color: #3385ff; color:white;' class='border border-size-1 bd-black'>" + TotalUtNoviembre.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 1) ? "<td style='text-align:right; background-color: #3385ff; color:white;' class='border border-size-1 bd-black'>" + TotalDiciembre.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 2) ? "<td style='text-align:right; background-color: #3385ff; color:white;' class='border border-size-1 bd-black'>" + TotalUnDiciembre.ToString() + "</td>" : "") +
                ((bUtilidad) ? "<td style='text-align:right; background-color: #3385ff; color:white;' class='border border-size-1 bd-black'>" + TotalUtDiciembre.ToString("C") + "</td>" : "") +
                ((iIndicador == 0) || (iIndicador == 1) ? "<td style='text-align:right; background-color: #3385ff; color:white;' class='border border-size-1 bd-black'><b>" + TotalTotal.ToString("C") + "</b></td>" : "") +
                ((iIndicador == 0) || (iIndicador == 2) ? "<td style='text-align:right; background-color: #3385ff; color:white;' class='border border-size-1 bd-black'><b>" + UnTotalTotal.ToString() + "</b></td>" : "") +
                ((bUtilidad) ? "<td style='text-align:right; background-color: #3385ff; color:white;' class='border border-size-1 bd-black'><b>" + UtTotalTotal.ToString("C") + "</b></td>" : "") +
                "</tr>";

            return salida;
        }

        private List<RVDSemanal> ReporteVentaSemanal()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                List<VentaSemanal> lisVentasSem = new List<VentaSemanal>();
                VentaSemanal semanal = new VentaSemanal();

                List<RVDSemanal> lisVentasSem2 = new List<RVDSemanal>();
                CN_dshIndicadoresACyS clsdshReporteVentas = new CN_dshIndicadoresACyS();



                string AnioSemana = cmbAniosSemana.Items[cmbAniosSemana.SelectedIndex].Value;
                int Mov_80 = 0;
                if (chkIncMov80.Checked)
                {
                    Mov_80 = 1;
                }
                /*
                new CN_Rep_VenEstadisticaVentas().ConsultaVentaSem_Territorio(semanal, sesion.Emp_Cnx, ref lisVentasSem, 
                    sesion.Id_Emp, sesion.Id_Cd_Ver, 
                    AnioSemana
                    , null, null, null
                    , 0
                    , 0
                    , 0
                    , Mov_80
                    );
                */
                clsdshReporteVentas.ConsultaRVDSemanal(sesion.Id_Emp, sesion.Id_Cd_Ver, sesion.Emp_Cnx, Convert.ToInt32(AnioSemana), Mov_80, ref lisVentasSem2);

                return lisVentasSem2;
            }
            catch (Exception ex)
            {
                this.ErrorManager(ex, "ReporteVentaSemanal");
                throw ex;
            }
        }

        #endregion

        #region ErrorManager

        private void Alerta(string mensaje)
        {
            try
            {
                ///     RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
                this.lblMensaje.Text = mensaje;
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

        #region Metodos

        protected void btnActualiza_Click(object sender, EventArgs e)
        {
            if (chkRptXSemana.Checked == false)
            {
                if (lblFilXCliente.Value == "")
                {
                    hdfFiltroCliente.Value = "";
                }
                if (lblFilXProducto.Value == "")
                {
                    hdfFiltroProducto.Value = "";
                }
                if (lblFilXTerritorio.Value == "")
                {
                    hdfFiltroTerritorio.Value = "";
                }

                if (chkDXProducto0.Checked)
                {
                    hdfdsgProducto.Value = "1";
                }
                else
                {
                    hdfdsgProducto.Value = "";
                }
                if (chkDXCliente0.Checked)
                {
                    hdfdsgCliente.Value = "1";
                }
                else
                {
                    hdfdsgCliente.Value = "";
                }

                llenaDashboard();
            }

        }

        protected void chkDXProducto_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDXProducto0.Checked)
            {
                hdfdsgProducto.Value = "1";
            }
            else
            {
                hdfdsgProducto.Value = "";
            }
            if (chkDXCliente0.Checked)
            {
                hdfdsgCliente.Value = "1";
            }
            else
            {
                hdfdsgCliente.Value = "";
            }
            llenaDashboard();
        }

        protected void chkDXCliente_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDXProducto0.Checked)
            {
                hdfdsgProducto.Value = "1";
            }
            else
            {
                hdfdsgProducto.Value = "";
            }
            if (chkDXCliente0.Checked)
            {
                hdfdsgCliente.Value = "1";
            }
            else
            {
                hdfdsgCliente.Value = "";
            }
            llenaDashboard();
        }

        protected void rdbXRSC_CheckedChanged(object sender, EventArgs e)
        {
            if (hdfopcMenu.Value == "20")
            {
                hdfopcMenu.Value = "30";
            }

            if (hdfopcMenu.Value == "40")
            {
                hdfopcMenu.Value = "50";
            }

            if (chkDXProducto0.Checked)
            {
                hdfdsgProducto.Value = "1";
            }
            else
            {
                hdfdsgProducto.Value = "";
            }
            if (chkDXCliente0.Checked)
            {
                hdfdsgCliente.Value = "1";
            }
            else
            {
                hdfdsgCliente.Value = "";
            }
            llenaDashboard();
        }

        protected void rdbXRIK_CheckedChanged(object sender, EventArgs e)
        {
            if (hdfopcMenu.Value == "30")
            {
                hdfopcMenu.Value = "20";
            }

            if (hdfopcMenu.Value == "50")
            {
                hdfopcMenu.Value = "40";
            }

            if (chkDXProducto0.Checked)
            {
                hdfdsgProducto.Value = "1";
            }
            else
            {
                hdfdsgProducto.Value = "";
            }
            if (chkDXCliente0.Checked)
            {
                hdfdsgCliente.Value = "1";
            }
            else
            {
                hdfdsgCliente.Value = "";
            }
            llenaDashboard();
        }

        protected void btnExcelGrafica_Click(object sender, ImageClickEventArgs e)
        {
            string strRpt = "";
            string strOpcion = "";
            string strAgrupador1 = "";
            string strAgrupador2 = "";
            string strEnc = "";
            string strEnc2 = "";

            llenaDashboard();

            int iTipo = Convert.ToInt32(hdfopcMenu.Value);
            if (hdfdsgProducto.Value == "1")
            {
                iTipo = iTipo + 2;
            }
            if (hdfdsgCliente.Value == "1")
            {
                iTipo = iTipo + 1;
            }

            #region Titulos

            /*
             * 	--	1: Cte sin desglose
             * 	--	3: Cte desglosado 1 nivel:Producto
             * 	--	10: Prd sin desglose
             * 	--	20: TerVta sin desglose
             * 	--	21: TerVta desglosado 1 nivel: Cliente
             * 	--	22: TerVta desglosado 2 nivel: Producto
             * 	--	23: TerVta desglosado 3 nivel: Cliente y Producto
             * 	--		30: TerSrv sin desglose
             * 	--		31: TerSrv desglosado 1 nivel:Cliente
             * 	--		32: TerSrv desglosado 2 nivel: Producto
             * 	--		33: TerSrv desglosado 3 nivel: Cliente y Producto
             * 	--	40: RIK sin desglose
             * 	--	41: RIK desglosado 1 nivel:Cliente
             * 	--	42: RIK desglosado 2 nivel: Producto
             * 	--	43: RIK desglosado 3 nivel: Cliente y Producto
             * 	--		50: RSC sin desglose
             * 	--		51: RSC desglosado 1 nivel:Cliente
             * 	--		52: RSC desglosado 2 nivel: Producto
             * 	--		53: RIK desglosado 3 nivel: Cliente y Producto
            */

            if (iTipo == 1)
            {
                strRpt = "RptVtaXCliente";
                strOpcion = "Reporte por Cliente";
                strAgrupador1 = "";
                strAgrupador2 = "";
                strEnc = "Cliente";
                strEnc2 = "";
            }
            if (iTipo == 3)
            {
                strRpt = "RptVtaXClienteXProducto";
                strOpcion = "Reporte por Cliente";
                strAgrupador1 = "Desglosado por Producto";
                strAgrupador2 = "";
                strEnc = "Cliente";
                strEnc2 = "Producto";
            }

            if (iTipo == 10)
            {
                strRpt = "RptVtaXProducto";
                strOpcion = "Reporte por Producto";
                strAgrupador1 = "";
                strAgrupador2 = "";
                strEnc = "Producto";
                strEnc2 = "";
            }

            if (iTipo == 20)
            {
                strRpt = "RptVtaXTerrVta";
                strOpcion = "Reporte por Territorio de Venta";
                strAgrupador1 = "";
                strAgrupador2 = "";
                strEnc = "Territorio";
                strEnc2 = "";
            }
            if (iTipo == 21)
            {
                strRpt = "RptVtaXTerrVtaXProducto";
                strOpcion = "Reporte por Territorio de Venta";
                strAgrupador1 = "Desglosado por Producto";
                strAgrupador2 = "";
                strEnc = "Territorio";
                strEnc2 = "Producto";
            }
            if (iTipo == 22)
            {
                strRpt = "RptVtaXTerrVtaXCliente";
                strOpcion = "Reporte por Territorio de Venta";
                strAgrupador1 = "Desglosado por Cliente";
                strAgrupador2 = "";
                strEnc = "Territorio";
                strEnc2 = "Cliente";
            }
            if (iTipo == 23)
            {
                strRpt = "RptVtaXTerrVtaXClienteXProducto";
                strOpcion = "Reporte por Territorio de Venta";
                strAgrupador1 = "Desglosado por Cliente";
                strAgrupador2 = "Y por Producto";

                strEnc = "Cliente";
                strEnc2 = "Producto";
            }

            if (iTipo == 30)
            {
                strRpt = "RptVtaXTerrSrv";
                strOpcion = "Reporte por Territorio de Servicio";
                strAgrupador1 = "";
                strAgrupador2 = "";
                strEnc = "Territorio";
                strEnc2 = "";
            }
            if (iTipo == 31)
            {
                strRpt = "RptVtaXTerrSrvXProducto";
                strOpcion = "Reporte por Territorio de Servicio";
                strAgrupador1 = "Desglosado por Producto";
                strAgrupador2 = "";
                strEnc = "Territorio";
                strEnc2 = "Producto";
            }
            if (iTipo == 32)
            {
                strRpt = "RptVtaXTerrSrvXCliente";
                strOpcion = "Reporte por Territorio de Servicio";
                strAgrupador1 = "Desglosado por Cliente";
                strAgrupador2 = "";
                strEnc = "Territorio";
                strEnc2 = "Cliente";
            }
            if (iTipo == 33)
            {
                strRpt = "RptVtaXTerrSrvXClienteXProducto";
                strOpcion = "Reporte por Territorio de Servicio";
                strAgrupador1 = "Desglosado por Cliente";
                strAgrupador2 = "Y por Producto";
                strEnc = "Cliente";
                strEnc2 = "Producto";
            }

            if (iTipo == 40)
            {
                strRpt = "RptVtaXRepRIK";
                strOpcion = "Reporte por Representante RIK";
                strAgrupador1 = "";
                strAgrupador2 = "";
                strEnc = "Representante";
                strEnc2 = "";
            }
            if (iTipo == 41)
            {
                strRpt = "RptVtaXRepRIKXProducto";
                strOpcion = "Reporte por Representante RIK";
                strAgrupador1 = "Desglosado por Producto";
                strAgrupador2 = "";
                strEnc = "Representante";
                strEnc2 = "Producto";
            }
            if (iTipo == 42)
            {
                strRpt = "RptVtaXRepRIKXCliente";
                strOpcion = "Reporte por Representante RIK";
                strAgrupador1 = "Desglosado por Cliente";
                strAgrupador2 = "";
                strEnc = "Representante";
                strEnc2 = "Cliente";
            }
            if (iTipo == 43)
            {
                strRpt = "RptVtaXRepRIKXClienteXProducto";
                strOpcion = "Reporte por Representante RIK";
                strAgrupador1 = "Desglosado por Cliente";
                strAgrupador2 = "Y por Producto";
                strEnc = "Cliente";
                strEnc2 = "Producto";
            }

            if (iTipo == 50)
            {
                strRpt = "RptVtaXRepRSC";
                strOpcion = "Reporte por Representante RSC";
                strAgrupador1 = "";
                strAgrupador2 = "";

                strEnc = "Representante";
                strEnc2 = "";
            }
            if (iTipo == 51)
            {
                strRpt = "RptVtaXRepRSCXProducto";
                strOpcion = "Reporte por Representante RSC";
                strAgrupador1 = "Desglosado por Producto";
                strAgrupador2 = "";
                strEnc = "Representante";
                strEnc2 = "Producto";
            }
            if (iTipo == 52)
            {
                strRpt = "RptVtaXRepRSCXCliente";
                strOpcion = "Reporte por Representante RSC";
                strAgrupador1 = "Desglosado por Cliente";
                strAgrupador2 = "";
                strEnc = "Representante";
                strEnc2 = "Cliente";
            }
            if (iTipo == 53)
            {
                strRpt = "RptVtaXRepRSCXClienteXProducto";
                strOpcion = "Reporte por Representante RSC";
                strAgrupador1 = "Desglosado por Cliente";
                strAgrupador2 = "Y por Producto";
                strEnc = "Cliente";
                strEnc2 = "Producto";
            }
            #endregion

            List<RVDReporte> DatosDelRpt = new List<RVDReporte>();

            string strFiltroN0 = "";
            string strFiltroN1 = "";
            string strFiltroN2 = "";

            if (iTipo < 10)
            {
                /// es la opcion del cliente
                strFiltroN0 = hdfFiltroCliente.Value;
                strFiltroN1 = hdfFiltroProducto.Value;
            }
            else
            {
                if ((iTipo >= 20))
                {
                    strFiltroN0 = hdfFiltroTerritorio.Value;
                    if ((hdfdsgProducto.Value == "1") || (hdfdsgCliente.Value == "1"))
                    {
                        if ((hdfdsgProducto.Value == "1") && (hdfdsgCliente.Value == "1"))
                        {
                            strFiltroN1 = hdfFiltroCliente.Value;
                            strFiltroN2 = hdfFiltroProducto.Value;
                        }
                        else
                        {
                            if (hdfdsgProducto.Value == "1")
                            {
                                strFiltroN1 = hdfFiltroProducto.Value;
                            }
                            else
                            {
                                strFiltroN1 = hdfFiltroCliente.Value;
                            }
                        }
                    }
                }
                else
                {
                    /// es la opcion de producto, solo tiene ese filtro
                    strFiltroN0 = hdfFiltroProducto.Value;
                }
            }

            int Indicador = 0;              ///     Ambos (por default)
            if (radMonto.Checked)           ///     Solo Pesos
            {
                Indicador = 1;
            }
            else
            {
                if (radUnidades.Checked)    /// Solo Unidades
                {
                    Indicador = 2;
                }
            }

            DatosDelRpt = ExcelReporteVentas.Where(f => f.Nombre.Trim() == ((strFiltroN0 != "") ? strFiltroN0 : f.Nombre.Trim()) && f.PdrDescrip.Trim() == ((strFiltroN1 != "") ? strFiltroN1 : f.PdrDescrip.Trim()) && f.Agrupador.Trim() == ((strFiltroN2 != "") ? strFiltroN2 : f.Agrupador.Trim())).ToList();

            xlsReporteDeVentas(iTipo, strRpt, strEnc, strEnc2, strOpcion, strAgrupador1, strAgrupador2, Indicador, DatosDelRpt);
        }

        protected void btnExcelReporteSemana_Click(object sender, EventArgs e)
        {
            try
            {
                ///     RVDSemanal
                //  List<VentaSemanal> ListaVentasSem = ReporteVentaSemanal();
                List<RVDSemanal> ListaVentasSem = ReporteVentaSemanal();
                if (ListaVentasSem.Count >= 0)
                {
                    string strMov80 = "";
                    string strAnios = "Del Año " + cmbAniosSemana.Items[cmbAniosSemana.SelectedIndex].Value;
                    if (chkIncMov80.Checked)
                    {
                        strMov80 = "Considerando Ventas de Remisiones con Mov 80";
                    }

                    xlsReporteDeVentaSemanal("ReporteVentaXSemana", strAnios, strMov80, ListaVentasSem);
                }
            }
            catch (Exception ex)
            {
                this.ErrorManager(ex, "btnExcelReporteSemana_Click");
                throw ex;
            }
        }

        #endregion

        #region Excel

        void xlsReporteDeVentas(int tipo, string wsName, string strEncab, string strEncab2, string strA4, string strA5, string strA6, int iIndicador, List<RVDReporte> ListaRptVta)
        {

            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            string filenombre = "RptVtaCDI" + sesion.Id_Cd.ToString() + "_" + DateTime.UtcNow.ToShortDateString().Replace("/", "") + "h" + DateTime.UtcNow.ToShortTimeString().Replace(".", "").Replace(":", "").Replace(" ", "");
            try
            {
                XLWorkbook wb = new XLWorkbook();

                if ((tipo != 53) && (tipo != 43) && (tipo != 33) && (tipo != 23))
                {
                    if (strEncab2 == "")
                    {
                        var ws = wb.Worksheets.Add(wsName);
                        RenglonReporteSinAgrupador(strEncab, strA4, strA5, strA6, iIndicador, ListaRptVta, ref ws);
                    }
                    else
                    {
                        var wsa = wb.Worksheets.Add(wsName);
                        RenglonReporteAgrupadoN1(strEncab, strEncab2, strA4, strA5, strA6, iIndicador, ListaRptVta, ref wsa);
                    }
                }
                else
                {
                    RenglonReportePorPaginas(strEncab, strEncab2, strA4, strA5, strA6, iIndicador, ListaRptVta, ref wb);
                }

                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + filenombre + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                this.ErrorManager(ex, "CumplimientoCaptura");
                throw ex;
            }
        }

        void xlsReporteDeVentaSemanal(string wsName, string strAnios, string strMov80, List<RVDSemanal> ListaRptVta)
        {

            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            string filenombre = "RptVtaXSemanaCDI" + sesion.Id_Cd.ToString() + "_" + DateTime.UtcNow.ToShortDateString().Replace("/", "") + "h" + DateTime.UtcNow.ToShortTimeString().Replace(".", "").Replace(":", "").Replace(" ", "");
            try
            {
                XLWorkbook wb = new XLWorkbook();

                var ws = wb.Worksheets.Add(wsName);
                RenglonReporteXSemana(strAnios, strMov80, ListaRptVta, ref ws);

                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + filenombre + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                this.ErrorManager(ex, "CumplimientoCaptura");
                throw ex;
            }
        }

        void RenglonReporteXSemana(string strAnios, string strMov80, List<RVDSemanal> ListaRpt, ref IXLWorksheet wss)
        {
            try
            {
                #region Encabezado

                wss.Range("A2:F2").Merge();
                wss.Range("A2").SetValue("Reporte de Ventas Por Semana");
                wss.Range("A2:F2").Style.Font.Bold = true;
                wss.Range("A2:F2").Style.Font.FontSize = 16;


                wss.Range("H2:I2").Merge();
                wss.Range("H2").SetValue("al: " + DateTime.Now.ToString("dd/MM/yyyy"));
                ///     wss.Range("A3:J3").Style.Fill.BackgroundColor = XLColor.LightSlateGray;

                wss.Range("A3:F3").Merge();
                wss.Range("A3").SetValue("CDI " + drpCDI.SelectedItem.Text);
                wss.Range("A3:F3").Style.Font.Bold = true;
                wss.Range("A3:F3").Style.Font.FontSize = 12;

                wss.Range("A4:F4").Merge();
                wss.Range("A4").SetValue(strAnios);
                wss.Range("A4:F4").Style.Font.Italic = true;
                wss.Range("A4:F4").Style.Font.FontSize = 12;

                wss.Range("A5:F5").Merge();
                wss.Range("A5").SetValue(strMov80);
                wss.Range("A5:F5").Style.Font.Italic = true;
                wss.Range("A5:F5").Style.Font.FontSize = 12;

                #endregion

                #region HeaderRpt

                CeldaHeader("A7", "Id Territorio Servicio", ref wss);
                CeldaHeader("B7", "Territorio Servicio", ref wss);
                CeldaHeader("C7", "Id Territorio Venta", ref wss);
                CeldaHeader("D7", "Territorio Venta", ref wss);
                CeldaHeader("E7", "Id Cliente", ref wss);
                CeldaHeader("F7", "Cliente", ref wss);
                CeldaHeader("G7", "id Prd", ref wss);
                CeldaHeader("H7", "Producto", ref wss);
                CeldaHeader("I7", "Unidades", ref wss);
                CeldaHeader("J7", "Importe", ref wss);
                CeldaHeader("K7", "Semana", ref wss);
                CeldaHeader("L7", "Año", ref wss);
                CeldaHeader("M7", "Mes", ref wss);

                #endregion


                #region Datos

                int i = 8;
                foreach (RVDSemanal rng in ListaRpt)
                {
                    CeldaDatos("A" + i.ToString(), rng.Id_TerSrv.ToString(), XLAlignmentHorizontalValues.Center, ref wss);
                    CeldaDatos("B" + i.ToString(), rng.Nom_TerSrv.ToString(), XLAlignmentHorizontalValues.Left, ref wss);
                    CeldaDatos("C" + i.ToString(), rng.Id_Ter.ToString(), XLAlignmentHorizontalValues.Center, ref wss);
                    CeldaDatos("D" + i.ToString(), rng.Nom_Ter.ToString(), XLAlignmentHorizontalValues.Left, ref wss);
                    CeldaDatos("E" + i.ToString(), rng.Id_Cte.ToString(), XLAlignmentHorizontalValues.Center, ref wss);
                    CeldaDatos("F" + i.ToString(), rng.Nom_Cte, XLAlignmentHorizontalValues.Left, ref wss);
                    CeldaDatos("G" + i.ToString(), rng.Id_prd.ToString(), XLAlignmentHorizontalValues.Right, ref wss);
                    CeldaDatos("H" + i.ToString(), rng.Nom_Prd, XLAlignmentHorizontalValues.Left, ref wss);

                    CeldaDatos("I" + i.ToString(), rng.Unidades.ToString(), XLAlignmentHorizontalValues.Center, ref wss);
                    CeldaDatos("J" + i.ToString(), rng.Importe.ToString("C"), XLAlignmentHorizontalValues.Right, ref wss);

                    CeldaDatos("K" + i.ToString(), rng.Semana.ToString(), XLAlignmentHorizontalValues.Left, ref wss);
                    CeldaDatos("L" + i.ToString(), rng.Anio.ToString(), XLAlignmentHorizontalValues.Right, ref wss);
                    CeldaDatos("M" + i.ToString(), rng.Mes, XLAlignmentHorizontalValues.Left, ref wss);

                    i++;
                }

                #endregion

                wss.Columns().AdjustToContents();
            }
            catch (Exception ex)
            {
                this.ErrorManager(ex, "RenglonReporteXSemana");
                throw ex;
            }
        }

        void RenglonReporteSinAgrupador(string strEncmain, string strA4, string strA5, string strA6, int iIndicador, List<RVDReporte> ListaRpt, ref IXLWorksheet wss)
        {
            int i = 10;
            try
            {
                #region Encabezado

                wss.Range("A2:F2").Merge();
                wss.Range("A2").SetValue("Reporte de Ventas");
                wss.Range("A2:F2").Style.Font.Bold = true;
                wss.Range("A2:F2").Style.Font.FontSize = 16;
                ///     wss.Range("A2:J2").Style.Font.FontColor = XLColor.White;
                ///     wss.Range("A2:J2").Style.Fill.BackgroundColor = XLColor.LightSlateGray;

                wss.Range("H2:I2").Merge();
                wss.Range("H2").SetValue("al: " + DateTime.Now.ToString("dd/MM/yyyy"));
                ///     wss.Range("A3:J3").Style.Fill.BackgroundColor = XLColor.LightSlateGray;

                wss.Range("A3:F3").Merge();
                wss.Range("A3").SetValue("CDI " + drpCDI.SelectedItem.Text);
                wss.Range("A3:F3").Style.Font.Bold = true;
                wss.Range("A3:F3").Style.Font.FontSize = 12;

                wss.Range("A4:F4").Merge();
                wss.Range("A4").SetValue(strA4);
                wss.Range("A4:F4").Style.Font.Italic = true;
                wss.Range("A4:F4").Style.Font.FontSize = 12;

                wss.Range("A5:F5").Merge();
                wss.Range("A5").SetValue(strA5);
                wss.Range("A5:F5").Style.Font.Italic = true;
                wss.Range("A5:F5").Style.Font.FontSize = 12;

                wss.Range("A6:F6").Merge();
                wss.Range("A6").SetValue(strA6);
                wss.Range("A6:F6").Style.Font.Italic = true;
                wss.Range("A6:F6").Style.Font.FontSize = 12;

                #endregion

                #region HeaderRpt

                if (iIndicador == 1)     /// Pesos
                {
                    if (chkIncluyeUtilidad.Checked)
                        HeaderExcelMontoConUtilidadSA(strEncmain, ref wss);
                    else
                        HeaderExcelMontoSinUtilidadSA(strEncmain, ref wss);
                }
                if (iIndicador == 2)     /// Unidades
                {
                    HeaderExcelUnidadesSA(strEncmain, ref wss);
                }
                if (iIndicador == 0)     /// Pesos y Unidades
                {
                    if (chkIncluyeUtilidad.Checked)
                        HeaderExcelMontoYUnidadesConUtilidadSA(strEncmain, ref wss);
                    else
                        HeaderExcelMontoYUnidadesSinUtilidadSA(strEncmain, ref wss);
                }

                #endregion

                #region TotalDeTotaLes

                DatosExcelTotalDeTotalesSA(ListaRpt, i, "Totales :", iIndicador, chkIncluyeUtilidad.Checked, ref wss);
                i++;

                #endregion

                #region Datos

                foreach (RVDReporte rng in ListaRpt)
                {
                    if (iIndicador == 1)     /// Pesos
                    {
                        if (chkIncluyeUtilidad.Checked)
                            DatosExcelMontosConUtilidadSA(i, rng, ref wss);
                        else
                            DatosExcelMontosSinUtilidadSA(i, rng, ref wss);
                    }
                    if (iIndicador == 2)     /// Unidades
                    {
                        DatosExcelUnidadesSinUtilidadSA(i, rng, ref wss);
                    }
                    if (iIndicador == 0)     /// Pesos y Unidades
                    {
                        if (chkIncluyeUtilidad.Checked)
                            DatosExcelMontosYUnidadesConUtilidadSA(i, rng, ref wss);
                        else
                            DatosExcelMontosYUnidadesSinUtilidadSA(i, rng, ref wss);
                    }

                    i++;
                }

                #endregion

                wss.Columns().AdjustToContents();
            }
            catch (Exception ex)
            {
                this.ErrorManager(ex, "RenglonReporteSinAgrupador");
                throw ex;
            }
        }

        void RenglonReporteAgrupadoN1(string strEncmain, string strEncsec, string strA4, string strA5, string strA6, int iIndicador, List<RVDReporte> ListaRpt, ref IXLWorksheet wss)
        {
            int i = 10;
            string ultimaCol = "";
            try
            {
                #region Encabezado

                wss.Range("A2:F2").Merge();
                wss.Range("A2").SetValue("Reporte de Ventas");
                wss.Range("A2:F2").Style.Font.Bold = true;
                wss.Range("A2:F2").Style.Font.FontSize = 16;

                wss.Range("H2:I2").Merge();
                wss.Range("H2").SetValue("al: " + DateTime.Now.ToString("dd/MM/yyyy"));

                wss.Range("A3:F3").Merge();
                wss.Range("A3").SetValue("CDI " + drpCDI.SelectedItem.Text);
                wss.Range("A3:F3").Style.Font.Bold = true;
                wss.Range("A3:F3").Style.Font.FontSize = 12;

                wss.Range("A4:F4").Merge();
                wss.Range("A4").SetValue(strA4);
                wss.Range("A4:F4").Style.Font.Italic = true;
                wss.Range("A4:F4").Style.Font.FontSize = 12;

                wss.Range("A5:F5").Merge();
                wss.Range("A5").SetValue(strA5);
                wss.Range("A5:F5").Style.Font.Italic = true;
                wss.Range("A5:F5").Style.Font.FontSize = 12;

                wss.Range("A6:F6").Merge();
                wss.Range("A6").SetValue(strA6);
                wss.Range("A6:F6").Style.Font.Italic = true;
                wss.Range("A6:F6").Style.Font.FontSize = 12;

                #endregion

                #region HeaderRpt

                if (iIndicador == 1)     /// Pesos
                {
                    if (chkIncluyeUtilidad.Checked)
                    {
                        ultimaCol = "AC";
                        HeaderExcelMontoConUtilidadAG(strEncmain, strEncsec, ref wss);
                    }
                    else
                    {
                        ultimaCol = "P";
                        HeaderExcelMontoSinUtilidadAG(strEncmain, strEncsec, ref wss);
                    }
                }
                if (iIndicador == 2)     /// Unidades
                {
                    ultimaCol = "P";
                    HeaderExcelUnidadesAG(strEncmain, strEncsec, ref wss);
                }
                if (iIndicador == 0)     /// Pesos y Unidades
                {
                    if (chkIncluyeUtilidad.Checked)
                    {
                        ultimaCol = "AP";
                        HeaderExcelMontoYUnidadesConUtilidadAG(strEncmain, strEncsec, ref wss);
                    }
                    else
                    {
                        ultimaCol = "AC";
                        HeaderExcelMontoYUnidadesSinUtilidadAG(strEncmain, strEncsec, ref wss);
                    }
                }

                #endregion

                #region TotalDeTotaLes

                DatosExcelTotalDeTotalesAG(ListaRpt, i, "Totales :", iIndicador, chkIncluyeUtilidad.Checked, ref wss);
                i++;

                #endregion

                #region Datos
                string NombreCteAgrupa = "";
                int iFilaInicial = i;
                int iFilaFinal = i;
                foreach (RVDReporte rng in ListaRpt)
                {
                    if (NombreCteAgrupa.Length == 0)
                    {
                        NombreCteAgrupa = rng.Id.ToString() + " " + rng.Nombre.Trim();

                        DatosExcelTotalDeTotalesAG(ListaRpt.Where(f => f.Id.ToString() + " " + f.Nombre.Trim() == NombreCteAgrupa).ToList(), i, "Total " + NombreCteAgrupa.Replace("&nbsp;", ""), iIndicador, chkIncluyeUtilidad.Checked, ref wss);
                        i++;
                        iFilaFinal = i;

                        wss.Range("A" + i.ToString() + ":" + ultimaCol.Trim() + i.ToString()).Merge();
                        CeldaAgrupador("A" + i.ToString() + ":" + ultimaCol.Trim() + i.ToString(), NombreCteAgrupa, ref wss);
                        i++;
                        iFilaInicial = i;
                    }
                    else
                    {
                        if (NombreCteAgrupa != rng.Id.ToString() + " " + rng.Nombre.Trim())
                        {
                            NombreCteAgrupa = rng.Id.ToString() + " " + rng.Nombre.Trim();

                            wss.Rows(iFilaInicial, i - 1).Group();
                            DatosExcelTotalDeTotalesAG(ListaRpt.Where(f => f.Id.ToString() + " " + f.Nombre.Trim() == NombreCteAgrupa).ToList(), i, "Total " + NombreCteAgrupa.Replace("&nbsp;", ""), iIndicador, chkIncluyeUtilidad.Checked, ref wss);
                            i++;
                            iFilaFinal = i;

                            wss.Range("A" + i.ToString() + ":" + ultimaCol.Trim() + i.ToString()).Merge();
                            CeldaAgrupador("A" + i.ToString() + ":" + ultimaCol.Trim() + i.ToString(), NombreCteAgrupa, ref wss);
                            i++;
                            iFilaInicial = i;
                        }
                    }

                    if (iIndicador == 1)     /// Pesos
                    {
                        if (chkIncluyeUtilidad.Checked)
                            DatosExcelMontosConUtilidadAG(i, rng, ref wss);
                        else
                            DatosExcelMontosSinUtilidadAG(i, rng, ref wss);
                    }
                    if (iIndicador == 2)     /// Unidades
                    {
                        DatosExcelUnidadesSinUtilidadAG(i, rng, ref wss);
                    }
                    if (iIndicador == 0)     /// Pesos y Unidades
                    {
                        if (chkIncluyeUtilidad.Checked)
                            DatosExcelMontosYUnidadesConUtilidadAG(i, rng, ref wss);
                        else
                            DatosExcelMontosYUnidadesSinUtilidadAG(i, rng, ref wss);
                    }
                    i++;
                }
                wss.Rows(iFilaInicial, i - 1).Group();

                #endregion

                wss.Columns().AdjustToContents();
            }
            catch (Exception ex)
            {
                this.ErrorManager(ex, "RenglonReporteAgrupadoN1");
                throw ex;
            }
        }

        void RenglonReportePorPaginas(string strEncmain, string strEncsec, string strA4, string strA5, string strA6, int iIndicador, List<RVDReporte> ListaRpt, ref XLWorkbook wbb)
        {
            int i = 10;
            string strHoja = "";
            List<int> ListaTerr = new List<int>();
            List<RVDReporte> ListaDeCliente = new List<RVDReporte>();

            try
            {
                ListaTerr = ListaRpt.Select(o => o.Id).Distinct().ToList();
                foreach (int idTerr in ListaTerr)
                {
                    i = 10;
                    strHoja = idTerr.ToString() + " " + ListaRpt.Where(no => no.Id == idTerr).FirstOrDefault().Nombre;
                    var wssp = wbb.Worksheets.Add(strHoja.Substring(0, strHoja.Length));

                    ListaDeCliente = ListaRpt.Where(o => o.Id == idTerr).ToList();

                    #region Encabezado

                    wssp.Range("A2:F2").Merge();
                    wssp.Range("A2").SetValue("Reporte de Ventas");
                    wssp.Range("A2:F2").Style.Font.Bold = true;
                    wssp.Range("A2:F2").Style.Font.FontSize = 16;

                    wssp.Range("H2:I2").Merge();
                    wssp.Range("H2").SetValue("al: " + DateTime.Now.ToString("dd/MM/yyyy"));

                    wssp.Range("A3:F3").Merge();
                    wssp.Range("A3").SetValue("CDI " + drpCDI.SelectedItem.Text + " - " + strHoja);
                    wssp.Range("A3:F3").Style.Font.Bold = true;
                    wssp.Range("A3:F3").Style.Font.FontSize = 12;

                    wssp.Range("A4:F4").Merge();
                    wssp.Range("A4").SetValue(strA4);
                    wssp.Range("A4:F4").Style.Font.Italic = true;
                    wssp.Range("A4:F4").Style.Font.FontSize = 12;

                    wssp.Range("A5:F5").Merge();
                    wssp.Range("A5").SetValue(strA5);
                    wssp.Range("A5:F5").Style.Font.Italic = true;
                    wssp.Range("A5:F5").Style.Font.FontSize = 12;

                    wssp.Range("A6:F6").Merge();
                    wssp.Range("A6").SetValue(strA6);
                    wssp.Range("A6:F6").Style.Font.Italic = true;
                    wssp.Range("A6:F6").Style.Font.FontSize = 12;

                    #endregion

                    #region HeaderRpt

                    wssp.Range("A8:A9").Merge();
                    CeldaHeader("A8", strEncmain, ref wssp);

                    wssp.Range("B8:B9").Merge();
                    CeldaHeader("B8:B9", "Id", ref wssp);

                    wssp.Range("C8:C9").Merge();
                    CeldaHeader("C8:C9", strEncsec, ref wssp);

                    wssp.Range("D8:I8").Merge();
                    CeldaHeader("D8:I8", "1er Trimestre", ref wssp);

                    wssp.Range("J8:O8").Merge();
                    CeldaHeader("J8:O8", "2do Trimestre", ref wssp);

                    wssp.Range("P8:U8").Merge();
                    CeldaHeader("P8:U8", "3er Trimestre", ref wssp);

                    wssp.Range("V8:AA8").Merge();
                    CeldaHeader("V8:AA8", "4to Trimestre", ref wssp);

                    wssp.Range("AB8:AB9").Merge();
                    CeldaHeader("AB8:AB9", "Total", ref wssp);

                    wssp.Range("AC8:AC9").Merge();
                    CeldaHeader("AC8:AC9", "Total Unidades", ref wssp);

                    CeldaHeader("D9", "Enero", ref wssp);
                    CeldaHeader("E9", "U Enero", ref wssp);
                    CeldaHeader("F9", "Febrero", ref wssp);
                    CeldaHeader("G9", "U Febrero", ref wssp);
                    CeldaHeader("H9", "Marzo", ref wssp);
                    CeldaHeader("I9", "U Marzo", ref wssp);

                    CeldaHeader("J9", "Abril", ref wssp);
                    CeldaHeader("K9", "U Abril", ref wssp);
                    CeldaHeader("L9", "Mayo", ref wssp);
                    CeldaHeader("M9", "U Mayo", ref wssp);
                    CeldaHeader("N9", "Junio", ref wssp);
                    CeldaHeader("O9", "U Junio", ref wssp);

                    CeldaHeader("P9", "Julio", ref wssp);
                    CeldaHeader("Q9", "U Julio", ref wssp);
                    CeldaHeader("R9", "Agosto", ref wssp);
                    CeldaHeader("S9", "U Agosto", ref wssp);
                    CeldaHeader("T9", "Septiembre", ref wssp);
                    CeldaHeader("U9", "U Septiembre", ref wssp);

                    CeldaHeader("V9", "Octubre", ref wssp);
                    CeldaHeader("W9", "U Octubre", ref wssp);
                    CeldaHeader("X9", "Noviembre", ref wssp);
                    CeldaHeader("Y9", "U Noviembre", ref wssp);
                    CeldaHeader("Z9", "Diciembre", ref wssp);
                    CeldaHeader("AA9", "U Diciembre", ref wssp);

                    #endregion

                    #region TotalDeTotaLes

                    DatosExcelTotalDeTotalesAG(ListaDeCliente, i, "Totales " + strHoja + " :", iIndicador, chkIncluyeUtilidad.Checked, ref wssp);
                    i++;

                    #endregion

                    #region Datos

                    string NombreCteAgrupa = "";
                    int iFilaInicial = i;
                    int iFilaFinal = i;
                    foreach (RVDReporte rng in ListaDeCliente)
                    {
                        if (NombreCteAgrupa.Length == 0)
                        {
                            NombreCteAgrupa = rng.IdPrd.ToString() + " " + rng.PdrDescrip.Trim();
                            DatosExcelTotalDeTotalesAG(ListaDeCliente.Where(f => f.IdPrd.ToString() + " " + f.PdrDescrip.Trim() == NombreCteAgrupa && f.Id == idTerr).ToList(), i, "Totales " + NombreCteAgrupa.Replace("&nbsp;", "") + " :", iIndicador, chkIncluyeUtilidad.Checked, ref wssp);
                            i++;
                            iFilaFinal = i;

                            wssp.Range("A" + i.ToString() + ":AC" + i.ToString()).Merge();
                            CeldaAgrupador("A" + i.ToString() + ":AC" + i.ToString(), NombreCteAgrupa, ref wssp);
                            i++;
                            iFilaInicial = i;
                        }
                        else
                        {
                            if (NombreCteAgrupa != rng.IdPrd.ToString() + " " + rng.PdrDescrip.Trim())
                            {
                                NombreCteAgrupa = rng.IdPrd.ToString() + " " + rng.PdrDescrip.Trim();
                                iFilaFinal = i;
                                wssp.Rows(iFilaInicial, iFilaFinal).Group();
                                DatosExcelTotalDeTotalesAG(ListaDeCliente.Where(f => f.IdPrd.ToString() + " " + f.PdrDescrip.Trim() == NombreCteAgrupa && f.Id == idTerr).ToList(), i, "Totales " + NombreCteAgrupa.Replace("&nbsp;", "") + " :", iIndicador, chkIncluyeUtilidad.Checked, ref wssp);
                                i++;

                                wssp.Range("A" + i.ToString() + ":AC" + i.ToString()).Merge();
                                CeldaAgrupador("A" + i.ToString() + ":AC" + i.ToString(), NombreCteAgrupa, ref wssp);
                                i++;
                                iFilaInicial = i;
                            }
                        }

                        CeldaDatos("B" + i.ToString(), rng.IdAgrupa.ToString(), XLAlignmentHorizontalValues.Center, ref wssp);
                        CeldaDatos("C" + i.ToString(), rng.Agrupador.ToString(), XLAlignmentHorizontalValues.Left, ref wssp);

                        CeldaDatos("D" + i.ToString(), rng.Enero.ToString("C"), XLAlignmentHorizontalValues.Right, ref wssp);
                        CeldaDatos("E" + i.ToString(), rng.UnEnero.ToString(), XLAlignmentHorizontalValues.Right, ref wssp);
                        CeldaDatos("F" + i.ToString(), rng.Febrero.ToString("C"), XLAlignmentHorizontalValues.Right, ref wssp);
                        CeldaDatos("G" + i.ToString(), rng.UnFebrero.ToString(), XLAlignmentHorizontalValues.Right, ref wssp);
                        CeldaDatos("H" + i.ToString(), rng.Marzo.ToString("C"), XLAlignmentHorizontalValues.Right, ref wssp);
                        CeldaDatos("I" + i.ToString(), rng.UnMarzo.ToString(), XLAlignmentHorizontalValues.Right, ref wssp);

                        CeldaDatos("J" + i.ToString(), rng.Abril.ToString("C"), XLAlignmentHorizontalValues.Right, ref wssp);
                        CeldaDatos("K" + i.ToString(), rng.UnAbril.ToString(), XLAlignmentHorizontalValues.Right, ref wssp);
                        CeldaDatos("L" + i.ToString(), rng.Mayo.ToString("C"), XLAlignmentHorizontalValues.Right, ref wssp);
                        CeldaDatos("M" + i.ToString(), rng.UnMayo.ToString(), XLAlignmentHorizontalValues.Right, ref wssp);
                        CeldaDatos("N" + i.ToString(), rng.Junio.ToString("C"), XLAlignmentHorizontalValues.Right, ref wssp);
                        CeldaDatos("O" + i.ToString(), rng.UnJunio.ToString(), XLAlignmentHorizontalValues.Right, ref wssp);

                        CeldaDatos("P" + i.ToString(), rng.Julio.ToString("C"), XLAlignmentHorizontalValues.Right, ref wssp);
                        CeldaDatos("Q" + i.ToString(), rng.UnJulio.ToString(), XLAlignmentHorizontalValues.Right, ref wssp);
                        CeldaDatos("R" + i.ToString(), rng.Agosto.ToString("C"), XLAlignmentHorizontalValues.Right, ref wssp);
                        CeldaDatos("S" + i.ToString(), rng.UnAgosto.ToString(), XLAlignmentHorizontalValues.Right, ref wssp);
                        CeldaDatos("T" + i.ToString(), rng.Septiembre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wssp);
                        CeldaDatos("U" + i.ToString(), rng.UnSeptiembre.ToString(), XLAlignmentHorizontalValues.Right, ref wssp);

                        CeldaDatos("V" + i.ToString(), rng.Octubre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wssp);
                        CeldaDatos("W" + i.ToString(), rng.UnOctubre.ToString(), XLAlignmentHorizontalValues.Right, ref wssp);
                        CeldaDatos("X" + i.ToString(), rng.Noviembre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wssp);
                        CeldaDatos("Y" + i.ToString(), rng.UnNoviembre.ToString(), XLAlignmentHorizontalValues.Right, ref wssp);
                        CeldaDatos("Z" + i.ToString(), rng.Diciembre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wssp);
                        CeldaDatos("AA" + i.ToString(), rng.UnDiciembre.ToString(), XLAlignmentHorizontalValues.Right, ref wssp);

                        CeldaDatos("AB" + i.ToString(), rng.Total.ToString("C"), XLAlignmentHorizontalValues.Right, ref wssp);
                        CeldaDatos("AC" + i.ToString(), rng.UnTotal.ToString(), XLAlignmentHorizontalValues.Right, ref wssp);

                        i++;
                    }

                    #endregion

                    wssp.Columns().AdjustToContents();
                }
            }
            catch (Exception ex)
            {
                this.ErrorManager(ex, "RenglonReportePorPaginas");
                throw ex;
            }
        }

        void CeldaHeader(string rango, string etiquetea, ref IXLWorksheet wsss)
        {
            wsss.Range(rango).SetValue(etiquetea);
            wsss.Range(rango).Style.Font.Bold = true;
            wsss.Range(rango).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            wsss.Range(rango).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            wsss.Range(rango).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
        }

        void CeldaAgrupador(string rango, string etiquetea, ref IXLWorksheet wsss)
        {
            wsss.Range(rango).SetValue(etiquetea);
            wsss.Range(rango).Style.Font.Bold = true;
            wsss.Range(rango).Style.Font.FontColor = XLColor.White;
            wsss.Range(rango).Style.Fill.BackgroundColor = XLColor.FromHtml("#3385ff");
            wsss.Range(rango).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            wsss.Range(rango).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            wsss.Range(rango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        }

        void CeldaTotal(string rango, string etiquetea, XLAlignmentHorizontalValues HAlign, bool bBold, bool bItalic, ref IXLWorksheet wsss)
        {
            wsss.Range(rango).SetValue(etiquetea);
            wsss.Range(rango).Style.Font.Bold = bBold;
            wsss.Range(rango).Style.Font.Italic = bItalic;
            wsss.Range(rango).Style.Font.FontColor = XLColor.White;
            wsss.Range(rango).Style.Fill.BackgroundColor = XLColor.FromHtml("#3385ff");
            wsss.Range(rango).Style.Alignment.Horizontal = HAlign;
            wsss.Range(rango).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            wsss.Range(rango).Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
        }

        void CeldaDatos(string rango, string etiquetea, XLAlignmentHorizontalValues HAlign, ref IXLWorksheet wsss)
        {
            wsss.Cell(rango).SetValue(etiquetea);
            wsss.Cell(rango).Style.Alignment.Horizontal = HAlign;
            wsss.Cell(rango).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
            wsss.Cell(rango).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        }

        #endregion

        #region FormatoHeaders

        #region Pagina
        string FormatoMontoConUtilidad()
        {
            string strsalida = "";

            strsalida = "<tr>" +
                "<th class='text-center border border-size-2 bd-black' style='width:20px; text-align:center;' rowspan='2'></th>" +
                "<th class='text-center border border-size-2 bd-black' style='width:100px;  text-align:center;' rowspan='2' >Núm.</th>" +
                "<th class='text-secondary border border-size-2 bd-black' style='width:250px; text-align:center;' rowspan='2'>Descripción</th>" +
                "<th class='text-secondary border border-size-2 bd-black' style=' text-align:center;' colspan='6'>1er Trimestre</th>" +
                "<th class='text-secondary border border-size-2 bd-black' style=' text-align:center;' colspan='6'>2do Trimestre</th>" +
                "<th class='text-secondary border border-size-2 bd-black' style=' text-align:center;' colspan='6'>3er Trimestre</th>" +
                "<th class='text-secondary border border-size-2 bd-black' style=' text-align:center;' colspan='6'>4to Trimestre</th>" +
                "<th class='text-secondary border border-size-2 bd-black' style='width:150px; text-align:center;' rowspan='2'>Total</th>" +
                "<th class='text-secondary border border-size-2 bd-black' style='width:150px; text-align:center;' rowspan='2'>Total Utilidad</th></tr>";

            strsalida = strsalida + "<tr>" +
                "<th id='hcEneroM' class='text-center border border-size-2 bd-black'>Enero</th>" +
                "<th id='hcEneroUt' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Ut Enero</th>" +
                "<th id='hcFebreroM' class='text-center border border-size-2 bd-black'>Febrero</th>" +
                "<th id='hcFebreroUt' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Ut Febrero</th>" +
                "<th id='hcMarzoM' class='text-center border border-size-2 bd-black'>Marzo</th>" +
                "<th id='hcMarzoUt' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Ut Marzo</th>" +
                "<th id='hcAbrilM' class='text-center border border-size-2 bd-black'>Abril</th>" +
                "<th id='hcAbrilUt' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Ut Abril</th>" +
                "<th id='hcMayoM' class='text-center border border-size-2 bd-black'>Mayo</th>" +
                "<th id='hcMayoUt' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Ut Mayo</th>" +
                "<th id='hcJunioM' class='text-center border border-size-2 bd-black'>Junio</th>" +
                "<th id='hcJunioUt' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Ut Junio</th>" +
                "<th id='hcJulioM' class='text-center border border-size-2 bd-black'>Julio</th>" +
                "<th id='hcJulioUt' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Ut Julio</th>" +
                "<th id='hcAgostoM' class='text-center border border-size-2 bd-black'>Agosto</th>" +
                "<th id='hcAgostoUt' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Ut Agosto</th>" +
                "<th id='hcSeptiembreM' class='text-center border border-size-2 bd-black'>Septiembre</th>" +
                "<th id='hcSeptiembreUt' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Ut Septiembre</th>" +
                "<th id='hcOctubreM' class='text-center border border-size-2 bd-black'>Octubre</th>" +
                "<th id='hcOctubreUt' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Ut Octubre</th>" +
                "<th id='hcNoviembreM' class='text-center border border-size-2 bd-black'>Noviembre</th>" +
                "<th id='hcNoviembreUt' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Ut Noviembre</th>" +
                "<th id='hcDiciembreM' class='text-center border border-size-2 bd-black'>Diciembre</th>" +
                "<th id='hcDiciembreUt' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Ut Diciembre</th></tr>";

            return strsalida;
        }

        string FormatoMontoSinUtilidad()
        {
            string strsalida = "";

            strsalida = "<tr>" +
                "<th class='text-center border border-size-2 bd-black' style='width:20px; text-align:center;' rowspan='2'></th>" +
                "<th class='text-center border border-size-2 bd-black' style='width:100px;  text-align:center;' rowspan='2' >Núm.</th>" +
                "<th class='text-secondary border border-size-2 bd-black' style='width:250px; text-align:center;' rowspan='2'>Descripción</th>" +
                "<th class='text-secondary border border-size-2 bd-black' style=' text-align:center;' colspan='3'>1er Trimestre</th>" +
                "<th class='text-secondary border border-size-2 bd-black' style=' text-align:center;' colspan='3'>2do Trimestre</th>" +
                "<th class='text-secondary border border-size-2 bd-black' style=' text-align:center;' colspan='3'>3er Trimestre</th>" +
                "<th class='text-secondary border border-size-2 bd-black' style=' text-align:center;' colspan='3'>4to Trimestre</th>" +
                "<th class='text-secondary border border-size-2 bd-black' style='width:150px; text-align:center;' rowspan='2'>Total</th></tr>";

            strsalida = strsalida + "<tr>" +
                "<th id='hcEneroM' class='text-center border border-size-2 bd-black'>Enero</th>" +
                "<th id='hcFebreroM' class='text-center border border-size-2 bd-black'>Febrero</th>" +
                "<th id='hcMarzoM' class='text-center border border-size-2 bd-black'>Marzo</th>" +
                "<th id='hcAbrilM' class='text-center border border-size-2 bd-black'>Abril</th>" +
                "<th id='hcMayoM' class='text-center border border-size-2 bd-black'>Mayo</th>" +
                "<th id='hcJunioM' class='text-center border border-size-2 bd-black'>Junio</th>" +
                "<th id='hcJulioM' class='text-center border border-size-2 bd-black'>Julio</th>" +
                "<th id='hcAgostoM' class='text-center border border-size-2 bd-black'>Agosto</th>" +
                "<th id='hcSeptiembreM' class='text-center border border-size-2 bd-black'>Septiembre</th>" +
                "<th id='hcOctubreM' class='text-center border border-size-2 bd-black'>Octubre</th>" +
                "<th id='hcNoviembreM' class='text-center border border-size-2 bd-black'>Noviembre</th>" +
                "<th id='hcDiciembreM' class='text-center border border-size-2 bd-black'>Diciembre</th></tr>";

            return strsalida;
        }

        string FormatoUnidades()
        {
            string strsalida = "";

            strsalida = "<tr>" +
                "<th class='text-center border border-size-2 bd-black' style='width:20px; text-align:center;' rowspan='2'></th>" +
                "<th class='text-center border border-size-2 bd-black' style='width:100px;  text-align:center;' rowspan='2' >Núm.</th>" +
                "<th class='text-secondary border border-size-2 bd-black' style='width:250px; text-align:center;' rowspan='2'>Descripción</th>" +
                "<th class='text-secondary border border-size-2 bd-black' style=' text-align:center;' colspan='3'>1er Trimestre</th>" +
                "<th class='text-secondary border border-size-2 bd-black' style=' text-align:center;' colspan='3'>2do Trimestre</th>" +
                "<th class='text-secondary border border-size-2 bd-black' style=' text-align:center;' colspan='3'>3er Trimestre</th>" +
                "<th class='text-secondary border border-size-2 bd-black' style=' text-align:center;' colspan='3'>4to Trimestre</th>" +
                "<th class='text-secondary border border-size-2 bd-black' style='width:150px; text-align:center;' rowspan='2'>Total Unidades</th></tr>";

            strsalida = strsalida + "<tr>" +
                "<th id='hcEneroUt' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Un Enero</th>" +
                "<th id='hcFebreroUt' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Un Febrero</th>" +
                "<th id='hcMarzoUt' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Un Marzo</th>" +
                "<th id='hcAbrilUt' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Un Abril</th>" +
                "<th id='hcMayoUt' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Un Mayo</th>" +
                "<th id='hcJunioUt' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Un Junio</th>" +
                "<th id='hcJulioUt' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Un Julio</th>" +
                "<th id='hcAgostoUt' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Un Agosto</th>" +
                "<th id='hcSeptiembreUt' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Un Septiembre</th>" +
                "<th id='hcOctubreUt' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Un Octubre</th>" +
                "<th id='hcNoviembreUt' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Un Noviembre</th>" +
                "<th id='hcDiciembreUt' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Un Diciembre</th></tr>";

            return strsalida;
        }

        string FormatoMontoYUnidadesConUtilidad()
        {
            string strsalida = "";

            strsalida = "<tr>" +
                "<th class='text-center border border-size-2 bd-black' style='width:20px; text-align:center;' rowspan='2'></th>" +
                "<th class='text-center border border-size-2 bd-black' style='width:100px;  text-align:center;' rowspan='2' >Núm.</th>" +
                "<th class='text-secondary border border-size-2 bd-black' style='width:250px; text-align:center;' rowspan='2'>Descripción</th>" +
                "<th class='text-secondary border border-size-2 bd-black' style=' text-align:center;' colspan='9'>1er Trimestre</th>" +
                "<th class='text-secondary border border-size-2 bd-black' style=' text-align:center;' colspan='9'>2do Trimestre</th>" +
                "<th class='text-secondary border border-size-2 bd-black' style=' text-align:center;' colspan='9'>3er Trimestre</th>" +
                "<th class='text-secondary border border-size-2 bd-black' style=' text-align:center;' colspan='9'>4to Trimestre</th>" +
                "<th class='text-secondary border border-size-2 bd-black' style='width:150px; text-align:center;' rowspan='2'>Total</th>" +
                "<th class='text-secondary border border-size-2 bd-black' style='width:150px; text-align:center;' rowspan='2'>Total Unidades</th>" +
                "<th class='text-secondary border border-size-2 bd-black' style='width:150px; text-align:center;' rowspan='2'>Total Utilidad</th></tr>";


            strsalida = strsalida + "<tr>" +
              "<th id='hcEneroM' class='text-center border border-size-2 bd-black'>Enero</th>" +
              "<th id='hcEneroUn' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Un Enero</th>" +
              "<th id='hcEneroUt' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Ut Enero</th>" +
              "<th id='hcFebreroM' class='text-center border border-size-2 bd-black'>Febrero</th>" +
              "<th id='hcFebreroUn' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Un Febrero</th>" +
              "<th id='hcFebreroUt' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Ut Febrero</th>" +
              "<th id='hcMarzoM' class='text-center border border-size-2 bd-black'>Marzo</th>" +
              "<th id='hcMarzoUn' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Un Marzo</th>" +
              "<th id='hcMarzoUt' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Ut Marzo</th>" +
              "<th id='hcAbrilM' class='text-center border border-size-2 bd-black'>Abril</th>" +
              "<th id='hcAbrilUn' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Un Abril</th>" +
              "<th id='hcAbrilUt' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Ut Abril</th>" +
              "<th id='hcMayoM' class='text-center border border-size-2 bd-black'>Mayo</th>" +
              "<th id='hcMayoUn' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Un Mayo</th>" +
              "<th id='hcMayoUt' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Ut Mayo</th>" +
              "<th id='hcJunioM' class='text-center border border-size-2 bd-black'>Junio</th>" +
              "<th id='hcJunioUn' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Un Junio</th>" +
              "<th id='hcJunioUt' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Ut Junio</th>" +
              "<th id='hcJulioM' class='text-center border border-size-2 bd-black'>Julio</th>" +
              "<th id='hcJulioUn' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Un Julio</th>" +
              "<th id='hcJulioUt' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Ut Julio</th>" +
              "<th id='hcAgostoM' class='text-center border border-size-2 bd-black'>Agosto</th>" +
              "<th id='hcAgostoUt' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Un Agosto</th>" +
              "<th id='hcAgostoUt' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Ut Agosto</th>" +
              "<th id='hcSeptiembreM' class='text-center border border-size-2 bd-black'>Septiembre</th>" +
              "<th id='hcSeptiembreUn' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Un Septiembre</th>" +
              "<th id='hcSeptiembreUt' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Ut Septiembre</th>" +
              "<th id='hcOctubreM' class='text-center border border-size-2 bd-black'>Octubre</th>" +
              "<th id='hcOctubreUn' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Un Octubre</th>" +
              "<th id='hcOctubreUt' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Ut Octubre</th>" +
              "<th id='hcNoviembreM' class='text-center border border-size-2 bd-black'>Noviembre</th>" +
              "<th id='hcNoviembreUn' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Un Noviembre</th>" +
              "<th id='hcNoviembreUt' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Ut Noviembre</th>" +
              "<th id='hcDiciembreM' class='text-center border border-size-2 bd-black'>Diciembre</th>" +
              "<th id='hcDiciembreUn' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Un Diciembre</th>" +
              "<th id='hcDiciembreUt' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Ut Diciembre</th></tr>";

            return strsalida;
        }

        string FormatoMontoYUnidadesSinUtilidad()
        {
            string strsalida = "";

            strsalida = "<tr>" +
                "<th class='text-center border border-size-2 bd-black' style='width:20px; text-align:center;' rowspan='2'></th>" +
                "<th class='text-center border border-size-2 bd-black' style='width:100px;  text-align:center;' rowspan='2' >Núm.</th>" +
                "<th class='text-secondary border border-size-2 bd-black' style='width:250px; text-align:center;' rowspan='2'>Descripción</th>" +
                "<th class='text-secondary border border-size-2 bd-black' style=' text-align:center;' colspan='6'>1er Trimestre</th>" +
                "<th class='text-secondary border border-size-2 bd-black' style=' text-align:center;' colspan='6'>2do Trimestre</th>" +
                "<th class='text-secondary border border-size-2 bd-black' style=' text-align:center;' colspan='6'>3er Trimestre</th>" +
                "<th class='text-secondary border border-size-2 bd-black' style=' text-align:center;' colspan='6'>4to Trimestre</th>" +
                "<th class='text-secondary border border-size-2 bd-black' style='width:150px; text-align:center;' rowspan='2'>Total</th>" +
                "<th class='text-secondary border border-size-2 bd-black' style='width:150px; text-align:center;' rowspan='2'>Total Unidades</th></tr>";


            strsalida = strsalida + "<tr>" +
              "<th id='hcEneroM' class='text-center border border-size-2 bd-black'>Enero</th>" +
              "<th id='hcEneroUn' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Un Enero</th>" +
              "<th id='hcFebreroM' class='text-center border border-size-2 bd-black'>Febrero</th>" +
              "<th id='hcFebreroUn' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Un Febrero</th>" +
              "<th id='hcMarzoM' class='text-center border border-size-2 bd-black'>Marzo</th>" +
              "<th id='hcMarzoUn' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Un Marzo</th>" +
              "<th id='hcAbrilM' class='text-center border border-size-2 bd-black'>Abril</th>" +
              "<th id='hcAbrilUn' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Un Abril</th>" +
              "<th id='hcMayoM' class='text-center border border-size-2 bd-black'>Mayo</th>" +
              "<th id='hcMayoUn' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Un Mayo</th>" +
              "<th id='hcJunioM' class='text-center border border-size-2 bd-black'>Junio</th>" +
              "<th id='hcJunioUn' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Un Junio</th>" +
              "<th id='hcJulioM' class='text-center border border-size-2 bd-black'>Julio</th>" +
              "<th id='hcJulioUn' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Un Julio</th>" +
              "<th id='hcAgostoM' class='text-center border border-size-2 bd-black'>Agosto</th>" +
              "<th id='hcAgostoUt' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Un Agosto</th>" +
              "<th id='hcSeptiembreM' class='text-center border border-size-2 bd-black'>Septiembre</th>" +
              "<th id='hcSeptiembreUn' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Un Septiembre</th>" +
              "<th id='hcOctubreM' class='text-center border border-size-2 bd-black'>Octubre</th>" +
              "<th id='hcOctubreUn' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Un Octubre</th>" +
              "<th id='hcNoviembreM' class='text-center border border-size-2 bd-black'>Noviembre</th>" +
              "<th id='hcNoviembreUn' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Un Noviembre</th>" +
              "<th id='hcDiciembreM' class='text-center border border-size-2 bd-black'>Diciembre</th>" +
              "<th id='hcDiciembreUn' class='text-center border border-size-2 bd-black' style=' white-space:nowrap;'>Un Diciembre</th></tr>";

            return strsalida;
        }

        #endregion

        #region Excel

        #region ExcelSinAgrupar

        void HeaderExcelMontoSinUtilidadSA(string strEncmain, ref IXLWorksheet wwsss)
        {
            wwsss.Range("A8:A9").Merge();
            CeldaHeader("A8", "Id", ref wwsss);

            wwsss.Range("B8:B9").Merge();
            CeldaHeader("B8:B9", strEncmain, ref wwsss);

            wwsss.Range("C8:E8").Merge();
            CeldaHeader("C8:E8", "1er Trimestre", ref wwsss);

            wwsss.Range("F8:H8").Merge();
            CeldaHeader("F8:H8", "2do Trimestre", ref wwsss);

            wwsss.Range("I8:K8").Merge();
            CeldaHeader("I8:K8", "3er Trimestre", ref wwsss);

            wwsss.Range("L8:N8").Merge();
            CeldaHeader("L8:N8", "4to Trimestre", ref wwsss);

            wwsss.Range("O8:O9").Merge();
            CeldaHeader("O8:O9", "Total", ref wwsss);

            CeldaHeader("C9", "Enero", ref wwsss);
            CeldaHeader("D9", "Febrero", ref wwsss);
            CeldaHeader("E9", "Marzo", ref wwsss);

            CeldaHeader("F9", "Abril", ref wwsss);
            CeldaHeader("G9", "Mayo", ref wwsss);
            CeldaHeader("H9", "Junio", ref wwsss);

            CeldaHeader("I9", "Julio", ref wwsss);
            CeldaHeader("J9", "Agosto", ref wwsss);
            CeldaHeader("K9", "Septiembre", ref wwsss);

            CeldaHeader("L9", "Octubre", ref wwsss);
            CeldaHeader("M9", "Noviembre", ref wwsss);
            CeldaHeader("N9", "Diciembre", ref wwsss);

        }

        void HeaderExcelMontoConUtilidadSA(string strEncmain, ref IXLWorksheet wwsss)
        {
            wwsss.Range("A8:A9").Merge();
            CeldaHeader("A8", "Id", ref wwsss);

            wwsss.Range("B8:B9").Merge();
            CeldaHeader("B8:B9", strEncmain, ref wwsss);

            wwsss.Range("C8:H8").Merge();
            CeldaHeader("C8:H8", "1er Trimestre", ref wwsss);

            wwsss.Range("I8:N8").Merge();
            CeldaHeader("I8:N8", "2do Trimestre", ref wwsss);

            wwsss.Range("O8:T8").Merge();
            CeldaHeader("O8:T8", "3er Trimestre", ref wwsss);

            wwsss.Range("U8:Z8").Merge();
            CeldaHeader("U8:Z8", "4to Trimestre", ref wwsss);

            wwsss.Range("AA8:AA9").Merge();
            CeldaHeader("AA8:AA9", "Total", ref wwsss);

            wwsss.Range("AB8:AB9").Merge();
            CeldaHeader("AB8:AB9", "Total Utilidades", ref wwsss);

            CeldaHeader("C9", "Enero", ref wwsss);
            CeldaHeader("D9", "Ut Enero", ref wwsss);
            CeldaHeader("E9", "Febrero", ref wwsss);
            CeldaHeader("F9", "Ut Febrero", ref wwsss);
            CeldaHeader("G9", "Marzo", ref wwsss);
            CeldaHeader("H9", "Ut Marzo", ref wwsss);

            CeldaHeader("I9", "Abril", ref wwsss);
            CeldaHeader("J9", "Ut Abril", ref wwsss);
            CeldaHeader("K9", "Mayo", ref wwsss);
            CeldaHeader("L9", "Ut Mayo", ref wwsss);
            CeldaHeader("M9", "Junio", ref wwsss);
            CeldaHeader("N9", "Ut Junio", ref wwsss);

            CeldaHeader("O9", "Julio", ref wwsss);
            CeldaHeader("P9", "Ut Julio", ref wwsss);
            CeldaHeader("Q9", "Agosto", ref wwsss);
            CeldaHeader("R9", "Ut Agosto", ref wwsss);
            CeldaHeader("S9", "Septiembre", ref wwsss);
            CeldaHeader("T9", "Ut Septiembre", ref wwsss);

            CeldaHeader("U9", "Octubre", ref wwsss);
            CeldaHeader("V9", "Ut Octubre", ref wwsss);
            CeldaHeader("W9", "Noviembre", ref wwsss);
            CeldaHeader("X9", "Ut Noviembre", ref wwsss);
            CeldaHeader("Y9", "Diciembre", ref wwsss);
            CeldaHeader("Z9", "Ut Diciembre", ref wwsss);
        }

        void HeaderExcelUnidadesSA(string strEncmain, ref IXLWorksheet wwsss)
        {
            wwsss.Range("A8:A9").Merge();
            CeldaHeader("A8", "Id", ref wwsss);

            wwsss.Range("B8:B9").Merge();
            CeldaHeader("B8:B9", strEncmain, ref wwsss);

            wwsss.Range("C8:E8").Merge();
            CeldaHeader("C8:E8", "1er Trimestre", ref wwsss);

            wwsss.Range("F8:H8").Merge();
            CeldaHeader("F8:H8", "2do Trimestre", ref wwsss);

            wwsss.Range("I8:K8").Merge();
            CeldaHeader("I8:K8", "3er Trimestre", ref wwsss);

            wwsss.Range("L8:N8").Merge();
            CeldaHeader("L8:N8", "4to Trimestre", ref wwsss);

            wwsss.Range("O8:O9").Merge();
            CeldaHeader("O8:O9", "Total Unidades", ref wwsss);

            CeldaHeader("C9", "Enero", ref wwsss);
            CeldaHeader("D9", "Febrero", ref wwsss);
            CeldaHeader("E9", "Marzo", ref wwsss);

            CeldaHeader("F9", "Abril", ref wwsss);
            CeldaHeader("G9", "Mayo", ref wwsss);
            CeldaHeader("H9", "Junio", ref wwsss);

            CeldaHeader("I9", "Julio", ref wwsss);
            CeldaHeader("J9", "Agosto", ref wwsss);
            CeldaHeader("K9", "Septiembre", ref wwsss);

            CeldaHeader("L9", "Octubre", ref wwsss);
            CeldaHeader("M9", "Noviembre", ref wwsss);
            CeldaHeader("N9", "Diciembre", ref wwsss);

        }

        void HeaderExcelMontoYUnidadesSinUtilidadSA(string strEncmain, ref IXLWorksheet wwsss)
        {
            wwsss.Range("A8:A9").Merge();
            CeldaHeader("A8", "Id", ref wwsss);

            wwsss.Range("B8:B9").Merge();
            CeldaHeader("B8:B9", strEncmain, ref wwsss);

            wwsss.Range("C8:H8").Merge();
            CeldaHeader("C8:H8", "1er Trimestre", ref wwsss);

            wwsss.Range("I8:N8").Merge();
            CeldaHeader("I8:N8", "2do Trimestre", ref wwsss);

            wwsss.Range("O8:T8").Merge();
            CeldaHeader("O8:T8", "3er Trimestre", ref wwsss);

            wwsss.Range("U8:Z8").Merge();
            CeldaHeader("U8:Z8", "4to Trimestre", ref wwsss);

            wwsss.Range("AA8:AA9").Merge();
            CeldaHeader("AA8:AA9", "Total", ref wwsss);

            wwsss.Range("AB8:AB9").Merge();
            CeldaHeader("AB8:AB9", "Total Unidades", ref wwsss);

            CeldaHeader("C9", "Enero", ref wwsss);
            CeldaHeader("D9", "U Enero", ref wwsss);
            CeldaHeader("E9", "Febrero", ref wwsss);
            CeldaHeader("F9", "U Febrero", ref wwsss);
            CeldaHeader("G9", "Marzo", ref wwsss);
            CeldaHeader("H9", "U Marzo", ref wwsss);

            CeldaHeader("I9", "Abril", ref wwsss);
            CeldaHeader("J9", "U Abril", ref wwsss);
            CeldaHeader("K9", "Mayo", ref wwsss);
            CeldaHeader("L9", "U Mayo", ref wwsss);
            CeldaHeader("M9", "Junio", ref wwsss);
            CeldaHeader("N9", "U Junio", ref wwsss);

            CeldaHeader("O9", "Julio", ref wwsss);
            CeldaHeader("P9", "U Julio", ref wwsss);
            CeldaHeader("Q9", "Agosto", ref wwsss);
            CeldaHeader("R9", "U Agosto", ref wwsss);
            CeldaHeader("S9", "Septiembre", ref wwsss);
            CeldaHeader("T9", "U Septiembre", ref wwsss);

            CeldaHeader("U9", "Octubre", ref wwsss);
            CeldaHeader("V9", "U Octubre", ref wwsss);
            CeldaHeader("W9", "Noviembre", ref wwsss);
            CeldaHeader("X9", "U Noviembre", ref wwsss);
            CeldaHeader("Y9", "Diciembre", ref wwsss);
            CeldaHeader("Z9", "U Diciembre", ref wwsss);

        }

        void HeaderExcelMontoYUnidadesConUtilidadSA(string strEncmain, ref IXLWorksheet wwsss)
        {
            wwsss.Range("A8:A9").Merge();
            CeldaHeader("A8", "Id", ref wwsss);

            wwsss.Range("B8:B9").Merge();
            CeldaHeader("B8:B9", strEncmain, ref wwsss);

            wwsss.Range("C8:K8").Merge();
            CeldaHeader("C8:K8", "1er Trimestre", ref wwsss);

            wwsss.Range("L8:T8").Merge();
            CeldaHeader("L8:T8", "2do Trimestre", ref wwsss);

            wwsss.Range("U8:AC8").Merge();
            CeldaHeader("U8:AC8", "3er Trimestre", ref wwsss);

            wwsss.Range("AD8:AL8").Merge();
            CeldaHeader("AD8:AL8", "4to Trimestre", ref wwsss);

            wwsss.Range("AM8:AM9").Merge();
            CeldaHeader("AM8:AM9", "Total", ref wwsss);

            wwsss.Range("AN8:AN9").Merge();
            CeldaHeader("AN8:AN9", "Total Unidades", ref wwsss);

            wwsss.Range("AO8:AO9").Merge();
            CeldaHeader("AO8:AO9", "Total Utilidades", ref wwsss);

            CeldaHeader("C9", "Enero", ref wwsss);
            CeldaHeader("D9", "U Enero", ref wwsss);
            CeldaHeader("E9", "Ut Enero", ref wwsss);
            CeldaHeader("F9", "Febrero", ref wwsss);
            CeldaHeader("G9", "U Febrero", ref wwsss);
            CeldaHeader("H9", "Ut Febrero", ref wwsss);
            CeldaHeader("I9", "Marzo", ref wwsss);
            CeldaHeader("J9", "U Marzo", ref wwsss);
            CeldaHeader("K9", "Ut Marzo", ref wwsss);

            CeldaHeader("L9", "Abril", ref wwsss);
            CeldaHeader("M9", "U Abril", ref wwsss);
            CeldaHeader("N9", "Ut Abril", ref wwsss);
            CeldaHeader("O9", "Mayo", ref wwsss);
            CeldaHeader("P9", "U Mayo", ref wwsss);
            CeldaHeader("Q9", "Ut Mayo", ref wwsss);
            CeldaHeader("R9", "Junio", ref wwsss);
            CeldaHeader("S9", "U Junio", ref wwsss);
            CeldaHeader("T9", "Ut Junio", ref wwsss);

            CeldaHeader("U9", "Julio", ref wwsss);
            CeldaHeader("V9", "U Julio", ref wwsss);
            CeldaHeader("W9", "Ut Julio", ref wwsss);
            CeldaHeader("X9", "Agosto", ref wwsss);
            CeldaHeader("Y9", "U Agosto", ref wwsss);
            CeldaHeader("Z9", "Ut Agosto", ref wwsss);
            CeldaHeader("AA9", "Septiembre", ref wwsss);
            CeldaHeader("AB9", "U Septiembre", ref wwsss);
            CeldaHeader("AC9", "Ut Septiembre", ref wwsss);

            CeldaHeader("AD9", "Octubre", ref wwsss);
            CeldaHeader("AE9", "U Octubre", ref wwsss);
            CeldaHeader("AF9", "Ut Octubre", ref wwsss);
            CeldaHeader("AG9", "Noviembre", ref wwsss);
            CeldaHeader("AH9", "U Noviembre", ref wwsss);
            CeldaHeader("AI9", "Ut Noviembre", ref wwsss);
            CeldaHeader("AJ9", "Diciembre", ref wwsss);
            CeldaHeader("AK9", "U Diciembre", ref wwsss);
            CeldaHeader("AL9", "Ut Diciembre", ref wwsss);

        }

        #endregion

        #region ExcelAgrupados

        void HeaderExcelMontoSinUtilidadAG(string strEncmain, string strEncsec, ref IXLWorksheet wwsss)
        {
            wwsss.Range("A8:A9").Merge();
            CeldaHeader("A8", strEncmain, ref wwsss);

            wwsss.Range("B8:B9").Merge();
            CeldaHeader("B8:B9", "Id", ref wwsss);

            wwsss.Range("C8:C9").Merge();
            CeldaHeader("C8:C9", strEncsec, ref wwsss);

            wwsss.Range("D8:F8").Merge();
            CeldaHeader("D8:F8", "1er Trimestre", ref wwsss);

            wwsss.Range("G8:I8").Merge();
            CeldaHeader("G8:I8", "2do Trimestre", ref wwsss);

            wwsss.Range("J8:L8").Merge();
            CeldaHeader("J8:L8", "3er Trimestre", ref wwsss);

            wwsss.Range("M8:O8").Merge();
            CeldaHeader("M8:O8", "4to Trimestre", ref wwsss);

            wwsss.Range("P8:P9").Merge();
            CeldaHeader("P8:P9", "Total", ref wwsss);

            CeldaHeader("D9", "Enero", ref wwsss);
            CeldaHeader("E9", "Febrero", ref wwsss);
            CeldaHeader("F9", "Marzo", ref wwsss);

            CeldaHeader("G9", "Abril", ref wwsss);
            CeldaHeader("H9", "Mayo", ref wwsss);
            CeldaHeader("I9", "Junio", ref wwsss);

            CeldaHeader("J9", "Julio", ref wwsss);
            CeldaHeader("K9", "Agosto", ref wwsss);
            CeldaHeader("L9", "Septiembre", ref wwsss);

            CeldaHeader("M9", "Octubre", ref wwsss);
            CeldaHeader("N9", "Noviembre", ref wwsss);
            CeldaHeader("O9", "Diciembre", ref wwsss);
        }

        void HeaderExcelMontoConUtilidadAG(string strEncmain, string strEncsec, ref IXLWorksheet wwsss)
        {
            wwsss.Range("A8:A9").Merge();
            CeldaHeader("A8", strEncmain, ref wwsss);

            wwsss.Range("B8:B9").Merge();
            CeldaHeader("B8:B9", "Id", ref wwsss);

            wwsss.Range("C8:C9").Merge();
            CeldaHeader("C8:C9", strEncsec, ref wwsss);

            wwsss.Range("D8:I8").Merge();
            CeldaHeader("D8:I8", "1er Trimestre", ref wwsss);

            wwsss.Range("J8:O8").Merge();
            CeldaHeader("J8:O8", "2do Trimestre", ref wwsss);

            wwsss.Range("P8:U8").Merge();
            CeldaHeader("P8:U8", "3er Trimestre", ref wwsss);

            wwsss.Range("V8:AA8").Merge();
            CeldaHeader("V8:AA8", "4to Trimestre", ref wwsss);

            wwsss.Range("AB8:AB9").Merge();
            CeldaHeader("AB8:AB9", "Total", ref wwsss);

            wwsss.Range("AC8:AC9").Merge();
            CeldaHeader("AC8:AC9", "Total Utilidades", ref wwsss);

            CeldaHeader("D9", "Enero", ref wwsss);
            CeldaHeader("E9", "Ut Enero", ref wwsss);
            CeldaHeader("F9", "Febrero", ref wwsss);
            CeldaHeader("G9", "Ut Febrero", ref wwsss);
            CeldaHeader("H9", "Marzo", ref wwsss);
            CeldaHeader("I9", "Ut Marzo", ref wwsss);

            CeldaHeader("J9", "Abril", ref wwsss);
            CeldaHeader("K9", "Ut Abril", ref wwsss);
            CeldaHeader("L9", "Mayo", ref wwsss);
            CeldaHeader("M9", "Ut Mayo", ref wwsss);
            CeldaHeader("N9", "Junio", ref wwsss);
            CeldaHeader("O9", "Ut Junio", ref wwsss);

            CeldaHeader("P9", "Julio", ref wwsss);
            CeldaHeader("Q9", "Ut Julio", ref wwsss);
            CeldaHeader("R9", "Agosto", ref wwsss);
            CeldaHeader("S9", "Ut Agosto", ref wwsss);
            CeldaHeader("T9", "Septiembre", ref wwsss);
            CeldaHeader("U9", "Ut Septiembre", ref wwsss);

            CeldaHeader("V9", "Octubre", ref wwsss);
            CeldaHeader("W9", "Ut Octubre", ref wwsss);
            CeldaHeader("X9", "Noviembre", ref wwsss);
            CeldaHeader("Y9", "Ut Noviembre", ref wwsss);
            CeldaHeader("Z9", "Diciembre", ref wwsss);
            CeldaHeader("AA9", "Ut Diciembre", ref wwsss);
        }

        void HeaderExcelUnidadesAG(string strEncmain, string strEncsec, ref IXLWorksheet wwsss)
        {
            wwsss.Range("A8:A9").Merge();
            CeldaHeader("A8", strEncmain, ref wwsss);

            wwsss.Range("B8:B9").Merge();
            CeldaHeader("B8:B9", "Id", ref wwsss);

            wwsss.Range("C8:C9").Merge();
            CeldaHeader("C8:C9", strEncsec, ref wwsss);

            wwsss.Range("D8:F8").Merge();
            CeldaHeader("D8:F8", "1er Trimestre", ref wwsss);

            wwsss.Range("G8:I8").Merge();
            CeldaHeader("G8:I8", "2do Trimestre", ref wwsss);

            wwsss.Range("J8:L8").Merge();
            CeldaHeader("J8:L8", "3er Trimestre", ref wwsss);

            wwsss.Range("M8:O8").Merge();
            CeldaHeader("M8:O8", "4to Trimestre", ref wwsss);

            wwsss.Range("P8:P9").Merge();
            CeldaHeader("P8:P9", "Total Unidades", ref wwsss);

            CeldaHeader("D9", "Un Enero", ref wwsss);
            CeldaHeader("E9", "Un Febrero", ref wwsss);
            CeldaHeader("F9", "Un Marzo", ref wwsss);

            CeldaHeader("G9", "Un Abril", ref wwsss);
            CeldaHeader("H9", "Un Mayo", ref wwsss);
            CeldaHeader("I9", "Un Junio", ref wwsss);

            CeldaHeader("J9", "Un Julio", ref wwsss);
            CeldaHeader("K9", "Un Agosto", ref wwsss);
            CeldaHeader("L9", "Un Septiembre", ref wwsss);

            CeldaHeader("M9", "Un Octubre", ref wwsss);
            CeldaHeader("N9", "Un Noviembre", ref wwsss);
            CeldaHeader("O9", "Un Diciembre", ref wwsss);

        }

        void HeaderExcelMontoYUnidadesSinUtilidadAG(string strEncmain, string strEncsec, ref IXLWorksheet wwsss)
        {
            wwsss.Range("A8:A9").Merge();
            CeldaHeader("A8", strEncmain, ref wwsss);

            wwsss.Range("B8:B9").Merge();
            CeldaHeader("B8:B9", "Id", ref wwsss);

            wwsss.Range("C8:C9").Merge();
            CeldaHeader("C8:C9", strEncsec, ref wwsss);

            wwsss.Range("D8:I8").Merge();
            CeldaHeader("D8:I8", "1er Trimestre", ref wwsss);

            wwsss.Range("J8:O8").Merge();
            CeldaHeader("J8:O8", "2do Trimestre", ref wwsss);

            wwsss.Range("P8:U8").Merge();
            CeldaHeader("P8:U8", "3er Trimestre", ref wwsss);

            wwsss.Range("V8:AA8").Merge();
            CeldaHeader("V8:AA8", "4to Trimestre", ref wwsss);

            wwsss.Range("AB8:AB9").Merge();
            CeldaHeader("AB8:AB9", "Total", ref wwsss);

            wwsss.Range("AC8:AC9").Merge();
            CeldaHeader("AC8:AC9", "Total Unidades", ref wwsss);

            CeldaHeader("D9", "Enero", ref wwsss);
            CeldaHeader("E9", "Un Enero", ref wwsss);
            CeldaHeader("F9", "Febrero", ref wwsss);
            CeldaHeader("G9", "Un Febrero", ref wwsss);
            CeldaHeader("H9", "Marzo", ref wwsss);
            CeldaHeader("I9", "Un Marzo", ref wwsss);

            CeldaHeader("J9", "Abril", ref wwsss);
            CeldaHeader("K9", "Un Abril", ref wwsss);
            CeldaHeader("L9", "Mayo", ref wwsss);
            CeldaHeader("M9", "Un Mayo", ref wwsss);
            CeldaHeader("N9", "Junio", ref wwsss);
            CeldaHeader("O9", "Un Junio", ref wwsss);

            CeldaHeader("P9", "Julio", ref wwsss);
            CeldaHeader("Q9", "Un Julio", ref wwsss);
            CeldaHeader("R9", "Agosto", ref wwsss);
            CeldaHeader("S9", "Un Agosto", ref wwsss);
            CeldaHeader("T9", "Septiembre", ref wwsss);
            CeldaHeader("U9", "Un Septiembre", ref wwsss);

            CeldaHeader("V9", "Octubre", ref wwsss);
            CeldaHeader("W9", "Un Octubre", ref wwsss);
            CeldaHeader("X9", "Noviembre", ref wwsss);
            CeldaHeader("Y9", "Un Noviembre", ref wwsss);
            CeldaHeader("Z9", "Diciembre", ref wwsss);
            CeldaHeader("AA9", "Un Diciembre", ref wwsss);

        }

        void HeaderExcelMontoYUnidadesConUtilidadAG(string strEncmain, string strEncsec, ref IXLWorksheet wwsss)
        {
            wwsss.Range("A8:A9").Merge();
            CeldaHeader("A8", strEncmain, ref wwsss);

            wwsss.Range("B8:B9").Merge();
            CeldaHeader("B8:B9", "Id", ref wwsss);

            wwsss.Range("C8:C9").Merge();
            CeldaHeader("C8:C9", strEncsec, ref wwsss);

            wwsss.Range("C8:L8").Merge();
            CeldaHeader("C8:L8", "1er Trimestre", ref wwsss);

            wwsss.Range("M8:U8").Merge();
            CeldaHeader("M8:U8", "2do Trimestre", ref wwsss);

            wwsss.Range("V8:AD8").Merge();
            CeldaHeader("V8:AD8", "3er Trimestre", ref wwsss);

            wwsss.Range("AE8:AM8").Merge();
            CeldaHeader("AE8:AM8", "4to Trimestre", ref wwsss);

            wwsss.Range("AN8:AN9").Merge();
            CeldaHeader("AN8:AN9", "Total", ref wwsss);

            wwsss.Range("AO8:AO9").Merge();
            CeldaHeader("AO8:AO9", "Total Unidades", ref wwsss);

            wwsss.Range("AP8:AP9").Merge();
            CeldaHeader("AP8:AP9", "Total Utilidades", ref wwsss);

            CeldaHeader("D9", "Enero", ref wwsss);
            CeldaHeader("E9", "U Enero", ref wwsss);
            CeldaHeader("F9", "Ut Enero", ref wwsss);
            CeldaHeader("G9", "Febrero", ref wwsss);
            CeldaHeader("H9", "U Febrero", ref wwsss);
            CeldaHeader("I9", "Ut Febrero", ref wwsss);
            CeldaHeader("J9", "Marzo", ref wwsss);
            CeldaHeader("K9", "U Marzo", ref wwsss);
            CeldaHeader("L9", "Ut Marzo", ref wwsss);

            CeldaHeader("M9", "Abril", ref wwsss);
            CeldaHeader("N9", "U Abril", ref wwsss);
            CeldaHeader("O9", "Ut Abril", ref wwsss);
            CeldaHeader("P9", "Mayo", ref wwsss);
            CeldaHeader("Q9", "U Mayo", ref wwsss);
            CeldaHeader("R9", "Ut Mayo", ref wwsss);
            CeldaHeader("S9", "Junio", ref wwsss);
            CeldaHeader("T9", "U Junio", ref wwsss);
            CeldaHeader("U9", "Ut Junio", ref wwsss);

            CeldaHeader("V9", "Julio", ref wwsss);
            CeldaHeader("W9", "U Julio", ref wwsss);
            CeldaHeader("X9", "Ut Julio", ref wwsss);
            CeldaHeader("Y9", "Agosto", ref wwsss);
            CeldaHeader("Z9", "U Agosto", ref wwsss);
            CeldaHeader("AA9", "Ut Agosto", ref wwsss);
            CeldaHeader("AB9", "Septiembre", ref wwsss);
            CeldaHeader("AC9", "U Septiembre", ref wwsss);
            CeldaHeader("AD9", "Ut Septiembre", ref wwsss);

            CeldaHeader("AE9", "Octubre", ref wwsss);
            CeldaHeader("AF9", "U Octubre", ref wwsss);
            CeldaHeader("AG9", "Ut Octubre", ref wwsss);
            CeldaHeader("AH9", "Noviembre", ref wwsss);
            CeldaHeader("AI9", "U Noviembre", ref wwsss);
            CeldaHeader("AJ9", "Ut Noviembre", ref wwsss);
            CeldaHeader("AK9", "Diciembre", ref wwsss);
            CeldaHeader("AL9", "U Diciembre", ref wwsss);
            CeldaHeader("AM9", "Ut Diciembre", ref wwsss);

        }

        #endregion

        #region ExcelPorPaginas

        void HeaderExcelMontoSinUtilidadPP(string strEncmain, string strEncsec, ref IXLWorksheet wwsss)
        {
            wwsss.Range("A8:A9").Merge();
            CeldaHeader("A8", strEncmain, ref wwsss);

            wwsss.Range("B8:B9").Merge();
            CeldaHeader("B8:B9", "Id", ref wwsss);

            wwsss.Range("C8:C9").Merge();
            CeldaHeader("C8:C9", strEncsec, ref wwsss);

            wwsss.Range("D8:F8").Merge();
            CeldaHeader("D8:F8", "1er Trimestre", ref wwsss);

            wwsss.Range("G8:I8").Merge();
            CeldaHeader("G8:I8", "2do Trimestre", ref wwsss);

            wwsss.Range("K8:L8").Merge();
            CeldaHeader("K8:L8", "3er Trimestre", ref wwsss);

            wwsss.Range("M8:O8").Merge();
            CeldaHeader("M8:O8", "4to Trimestre", ref wwsss);

            wwsss.Range("P8:P9").Merge();
            CeldaHeader("P8:P9", "Total", ref wwsss);

            CeldaHeader("D9", "Enero", ref wwsss);
            CeldaHeader("E9", "Febrero", ref wwsss);
            CeldaHeader("F9", "Marzo", ref wwsss);

            CeldaHeader("G9", "Abril", ref wwsss);
            CeldaHeader("H9", "Mayo", ref wwsss);
            CeldaHeader("I9", "Junio", ref wwsss);

            CeldaHeader("J9", "Julio", ref wwsss);
            CeldaHeader("K9", "Agosto", ref wwsss);
            CeldaHeader("L9", "Septiembre", ref wwsss);

            CeldaHeader("M9", "Octubre", ref wwsss);
            CeldaHeader("N9", "Noviembre", ref wwsss);
            CeldaHeader("O9", "Diciembre", ref wwsss);
        }

        void HeaderExcelMontoConUtilidadPP(string strEncmain, string strEncsec, ref IXLWorksheet wwsss)
        {
            wwsss.Range("A8:A9").Merge();
            CeldaHeader("A8", strEncmain, ref wwsss);

            wwsss.Range("B8:B9").Merge();
            CeldaHeader("B8:B9", "Id", ref wwsss);

            wwsss.Range("C8:C9").Merge();
            CeldaHeader("C8:C9", strEncsec, ref wwsss);

            wwsss.Range("D8:I8").Merge();
            CeldaHeader("D8:I8", "1er Trimestre", ref wwsss);

            wwsss.Range("J8:O8").Merge();
            CeldaHeader("J8:O8", "2do Trimestre", ref wwsss);

            wwsss.Range("P8:U8").Merge();
            CeldaHeader("P8:U8", "3er Trimestre", ref wwsss);

            wwsss.Range("V8:AA8").Merge();
            CeldaHeader("V8:AA8", "4to Trimestre", ref wwsss);

            wwsss.Range("AB8:AB9").Merge();
            CeldaHeader("AB8:AB9", "Total", ref wwsss);

            wwsss.Range("AC8:AC9").Merge();
            CeldaHeader("AC8:AC9", "Total Utilidades", ref wwsss);

            CeldaHeader("D9", "Enero", ref wwsss);
            CeldaHeader("E9", "Ut Enero", ref wwsss);
            CeldaHeader("F9", "Febrero", ref wwsss);
            CeldaHeader("G9", "Ut Febrero", ref wwsss);
            CeldaHeader("H9", "Marzo", ref wwsss);
            CeldaHeader("I9", "Ut Marzo", ref wwsss);

            CeldaHeader("J9", "Abril", ref wwsss);
            CeldaHeader("K9", "Ut Abril", ref wwsss);
            CeldaHeader("L9", "Mayo", ref wwsss);
            CeldaHeader("M9", "Ut Mayo", ref wwsss);
            CeldaHeader("N9", "Junio", ref wwsss);
            CeldaHeader("O9", "Ut Junio", ref wwsss);

            CeldaHeader("P9", "Julio", ref wwsss);
            CeldaHeader("Q9", "Ut Julio", ref wwsss);
            CeldaHeader("R9", "Agosto", ref wwsss);
            CeldaHeader("S9", "Ut Agosto", ref wwsss);
            CeldaHeader("T9", "Septiembre", ref wwsss);
            CeldaHeader("U9", "Ut Septiembre", ref wwsss);

            CeldaHeader("V9", "Octubre", ref wwsss);
            CeldaHeader("W9", "Ut Octubre", ref wwsss);
            CeldaHeader("X9", "Noviembre", ref wwsss);
            CeldaHeader("Y9", "Ut Noviembre", ref wwsss);
            CeldaHeader("Z9", "Diciembre", ref wwsss);
            CeldaHeader("AA9", "Ut Diciembre", ref wwsss);
        }

        void HeaderExcelUnidadesPP(string strEncmain, string strEncsec, ref IXLWorksheet wwsss)
        {
            wwsss.Range("A8:A9").Merge();
            CeldaHeader("A8", "Id", ref wwsss);

            wwsss.Range("B8:B9").Merge();
            CeldaHeader("B8:B9", strEncmain, ref wwsss);

            wwsss.Range("C8:E8").Merge();
            CeldaHeader("C8:E8", "1er Trimestre", ref wwsss);

            wwsss.Range("F8:H8").Merge();
            CeldaHeader("F8:H8", "2do Trimestre", ref wwsss);

            wwsss.Range("I8:K8").Merge();
            CeldaHeader("I8:K8", "3er Trimestre", ref wwsss);

            wwsss.Range("L8:N8").Merge();
            CeldaHeader("L8:N8", "4to Trimestre", ref wwsss);

            wwsss.Range("O8:O9").Merge();
            CeldaHeader("O8:O9", "Total Unidades", ref wwsss);

            CeldaHeader("C9", "Enero", ref wwsss);
            CeldaHeader("D9", "Febrero", ref wwsss);
            CeldaHeader("E9", "Marzo", ref wwsss);

            CeldaHeader("F9", "Abril", ref wwsss);
            CeldaHeader("G9", "Mayo", ref wwsss);
            CeldaHeader("H9", "Junio", ref wwsss);

            CeldaHeader("I9", "Julio", ref wwsss);
            CeldaHeader("J9", "Agosto", ref wwsss);
            CeldaHeader("K9", "Septiembre", ref wwsss);

            CeldaHeader("L9", "Octubre", ref wwsss);
            CeldaHeader("M9", "Noviembre", ref wwsss);
            CeldaHeader("N9", "Diciembre", ref wwsss);

        }

        void HeaderExcelMontoYUnidadesSinUtilidadPP(string strEncmain, string strEncsec, ref IXLWorksheet wwsss)
        {
            wwsss.Range("A8:A9").Merge();
            CeldaHeader("A8", "Id", ref wwsss);

            wwsss.Range("B8:B9").Merge();
            CeldaHeader("B8:B9", strEncmain, ref wwsss);

            wwsss.Range("C8:H8").Merge();
            CeldaHeader("C8:H8", "1er Trimestre", ref wwsss);

            wwsss.Range("I8:N8").Merge();
            CeldaHeader("I8:N8", "2do Trimestre", ref wwsss);

            wwsss.Range("O8:T8").Merge();
            CeldaHeader("O8:T8", "3er Trimestre", ref wwsss);

            wwsss.Range("U8:Z8").Merge();
            CeldaHeader("U8:Z8", "4to Trimestre", ref wwsss);

            wwsss.Range("AA8:AA9").Merge();
            CeldaHeader("AA8:AA9", "Total", ref wwsss);

            wwsss.Range("AB8:AB9").Merge();
            CeldaHeader("AB8:AB9", "Total Unidades", ref wwsss);

            CeldaHeader("C9", "Enero", ref wwsss);
            CeldaHeader("D9", "U Enero", ref wwsss);
            CeldaHeader("E9", "Febrero", ref wwsss);
            CeldaHeader("F9", "U Febrero", ref wwsss);
            CeldaHeader("G9", "Marzo", ref wwsss);
            CeldaHeader("H9", "U Marzo", ref wwsss);

            CeldaHeader("I9", "Abril", ref wwsss);
            CeldaHeader("J9", "U Abril", ref wwsss);
            CeldaHeader("K9", "Mayo", ref wwsss);
            CeldaHeader("L9", "U Mayo", ref wwsss);
            CeldaHeader("M9", "Junio", ref wwsss);
            CeldaHeader("N9", "U Junio", ref wwsss);

            CeldaHeader("O9", "Julio", ref wwsss);
            CeldaHeader("P9", "U Julio", ref wwsss);
            CeldaHeader("Q9", "Agosto", ref wwsss);
            CeldaHeader("R9", "U Agosto", ref wwsss);
            CeldaHeader("S9", "Septiembre", ref wwsss);
            CeldaHeader("T9", "U Septiembre", ref wwsss);

            CeldaHeader("U9", "Octubre", ref wwsss);
            CeldaHeader("V9", "U Octubre", ref wwsss);
            CeldaHeader("W9", "Noviembre", ref wwsss);
            CeldaHeader("X9", "U Noviembre", ref wwsss);
            CeldaHeader("Y9", "Diciembre", ref wwsss);
            CeldaHeader("Z9", "U Diciembre", ref wwsss);

        }

        void HeaderExcelMontoYUnidadesConUtilidadPP(string strEncmain, string strEncsec, ref IXLWorksheet wwsss)
        {
            wwsss.Range("A8:A9").Merge();
            CeldaHeader("A8", "Id", ref wwsss);

            wwsss.Range("B8:B9").Merge();
            CeldaHeader("B8:B9", strEncmain, ref wwsss);

            wwsss.Range("C8:K8").Merge();
            CeldaHeader("C8:K8", "1er Trimestre", ref wwsss);

            wwsss.Range("L8:T8").Merge();
            CeldaHeader("L8:T8", "2do Trimestre", ref wwsss);

            wwsss.Range("U8:AC8").Merge();
            CeldaHeader("U8:AC8", "3er Trimestre", ref wwsss);

            wwsss.Range("AD8:AL8").Merge();
            CeldaHeader("AD8:AL8", "4to Trimestre", ref wwsss);

            wwsss.Range("AM8:AM9").Merge();
            CeldaHeader("AM8:AM9", "Total", ref wwsss);

            wwsss.Range("AN8:AN9").Merge();
            CeldaHeader("AN8:AN9", "Total Unidades", ref wwsss);

            wwsss.Range("AO8:AO9").Merge();
            CeldaHeader("AO8:AO9", "Total Utilidades", ref wwsss);

            CeldaHeader("C9", "Enero", ref wwsss);
            CeldaHeader("D9", "U Enero", ref wwsss);
            CeldaHeader("E9", "Ut Enero", ref wwsss);
            CeldaHeader("F9", "Febrero", ref wwsss);
            CeldaHeader("G9", "U Febrero", ref wwsss);
            CeldaHeader("H9", "Ut Febrero", ref wwsss);
            CeldaHeader("I9", "Marzo", ref wwsss);
            CeldaHeader("J9", "U Marzo", ref wwsss);
            CeldaHeader("K9", "Ut Marzo", ref wwsss);

            CeldaHeader("L9", "Abril", ref wwsss);
            CeldaHeader("M9", "U Abril", ref wwsss);
            CeldaHeader("N9", "Ut Abril", ref wwsss);
            CeldaHeader("O9", "Mayo", ref wwsss);
            CeldaHeader("P9", "U Mayo", ref wwsss);
            CeldaHeader("Q9", "Ut Mayo", ref wwsss);
            CeldaHeader("R9", "Junio", ref wwsss);
            CeldaHeader("S9", "U Junio", ref wwsss);
            CeldaHeader("T9", "Ut Junio", ref wwsss);

            CeldaHeader("U9", "Julio", ref wwsss);
            CeldaHeader("V9", "U Julio", ref wwsss);
            CeldaHeader("W9", "Ut Julio", ref wwsss);
            CeldaHeader("X9", "Agosto", ref wwsss);
            CeldaHeader("Y9", "U Agosto", ref wwsss);
            CeldaHeader("Z9", "Ut Agosto", ref wwsss);
            CeldaHeader("AA9", "Septiembre", ref wwsss);
            CeldaHeader("AB9", "U Septiembre", ref wwsss);
            CeldaHeader("AC9", "Ut Septiembre", ref wwsss);

            CeldaHeader("AD9", "Octubre", ref wwsss);
            CeldaHeader("AE9", "U Octubre", ref wwsss);
            CeldaHeader("AF9", "Ut Octubre", ref wwsss);
            CeldaHeader("AG9", "Noviembre", ref wwsss);
            CeldaHeader("AH9", "U Noviembre", ref wwsss);
            CeldaHeader("AI9", "Ut Noviembre", ref wwsss);
            CeldaHeader("AJ9", "Diciembre", ref wwsss);
            CeldaHeader("AK9", "U Diciembre", ref wwsss);
            CeldaHeader("AL9", "Ut Diciembre", ref wwsss);

        }

        #endregion

        #endregion

        #endregion

        #region TotalesExcel

        void DatosExcelTotalDeTotalesSA(List<RVDReporte> listado, int ii, string strTotal, int iIndica, bool bUtil, ref IXLWorksheet wwsss)
        {
            float TotalTotal = listado.Select(m => m.Enero + m.Febrero + m.Marzo + m.Abril + m.Mayo + m.Junio + m.Julio + m.Agosto + m.Septiembre + m.Octubre + m.Noviembre + m.Diciembre).Sum();
            int UnTotalTotal = listado.Select(m => m.UnEnero + m.UnFebrero + m.UnMarzo + m.UnAbril + m.UnMayo + m.UnJunio + m.UnJulio + m.UnAgosto + m.UnSeptiembre + m.UnOctubre + m.UnNoviembre + m.UnDiciembre).Sum();
            float UtTotalTotal = listado.Select(m => m.UtEnero + m.UtFebrero + m.UtMarzo + m.UtAbril + m.UtMayo + m.UtJunio + m.UtJulio + m.UtAgosto + m.UtSeptiembre + m.UtOctubre + m.UtNoviembre + m.UtDiciembre).Sum();

            float TotalEnero = listado.Select(m => m.Enero).Sum();
            float TotalFebrero = listado.Select(m => m.Febrero).Sum();
            float TotalMarzo = listado.Select(m => m.Marzo).Sum();
            float TotalAbril = listado.Select(m => m.Abril).Sum();
            float TotalMayo = listado.Select(m => m.Mayo).Sum();
            float TotalJunio = listado.Select(m => m.Junio).Sum();
            float TotalJulio = listado.Select(m => m.Julio).Sum();
            float TotalAgosto = listado.Select(m => m.Agosto).Sum();
            float TotalSeptiembre = listado.Select(m => m.Septiembre).Sum();
            float TotalOctubre = listado.Select(m => m.Octubre).Sum();
            float TotalNoviembre = listado.Select(m => m.Noviembre).Sum();
            float TotalDiciembre = listado.Select(m => m.Diciembre).Sum();

            int TotalUnEnero = listado.Select(m => m.UnEnero).Sum();
            int TotalUnFebrero = listado.Select(m => m.UnFebrero).Sum();
            int TotalUnMarzo = listado.Select(m => m.UnMarzo).Sum();
            int TotalUnAbril = listado.Select(m => m.UnAbril).Sum();
            int TotalUnMayo = listado.Select(m => m.UnMayo).Sum();
            int TotalUnJunio = listado.Select(m => m.UnJunio).Sum();
            int TotalUnJulio = listado.Select(m => m.UnJulio).Sum();
            int TotalUnAgosto = listado.Select(m => m.UnAgosto).Sum();
            int TotalUnSeptiembre = listado.Select(m => m.UnSeptiembre).Sum();
            int TotalUnOctubre = listado.Select(m => m.UnOctubre).Sum();
            int TotalUnNoviembre = listado.Select(m => m.UnNoviembre).Sum();
            int TotalUnDiciembre = listado.Select(m => m.UnDiciembre).Sum();

            float TotalUtEnero = listado.Select(m => m.UtEnero).Sum();
            float TotalUtFebrero = listado.Select(m => m.UtFebrero).Sum();
            float TotalUtMarzo = listado.Select(m => m.UtMarzo).Sum();
            float TotalUtAbril = listado.Select(m => m.UtAbril).Sum();
            float TotalUtMayo = listado.Select(m => m.UtMayo).Sum();
            float TotalUtJunio = listado.Select(m => m.UtJunio).Sum();
            float TotalUtJulio = listado.Select(m => m.UtJulio).Sum();
            float TotalUtAgosto = listado.Select(m => m.UtAgosto).Sum();
            float TotalUtSeptiembre = listado.Select(m => m.UtSeptiembre).Sum();
            float TotalUtOctubre = listado.Select(m => m.UtOctubre).Sum();
            float TotalUtNoviembre = listado.Select(m => m.UtNoviembre).Sum();
            float TotalUtDiciembre = listado.Select(m => m.UtDiciembre).Sum();

            wwsss.Range("A" + ii.ToString() + ":B" + ii.ToString()).Merge();
            CeldaTotal("A" + ii.ToString(), strTotal, XLAlignmentHorizontalValues.Right, true, false, ref wwsss);
            /// Pesos       indicador 1
            /// Unidades    indicador 2
            /// Ambos       indicador 0
            /// bUtil       incluye utilidad
            if ((iIndica == 1) && (bUtil == false))
            {
                CeldaTotal("C" + ii.ToString(), TotalEnero.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("D" + ii.ToString(), TotalFebrero.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("E" + ii.ToString(), TotalMarzo.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);

                CeldaTotal("F" + ii.ToString(), TotalAbril.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("G" + ii.ToString(), TotalMayo.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("H" + ii.ToString(), TotalJunio.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);

                CeldaTotal("I" + ii.ToString(), TotalJulio.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("J" + ii.ToString(), TotalAgosto.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("K" + ii.ToString(), TotalSeptiembre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);

                CeldaTotal("L" + ii.ToString(), TotalOctubre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("M" + ii.ToString(), TotalNoviembre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("N" + ii.ToString(), TotalDiciembre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);

                CeldaTotal("O" + ii.ToString(), TotalTotal.ToString("C"), XLAlignmentHorizontalValues.Right, true, true, ref wwsss);
            }

            if ((iIndica == 1) && (bUtil))
            {
                CeldaTotal("C" + ii.ToString(), TotalEnero.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("D" + ii.ToString(), TotalUtEnero.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("E" + ii.ToString(), TotalFebrero.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("F" + ii.ToString(), TotalUtFebrero.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("G" + ii.ToString(), TotalMarzo.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("H" + ii.ToString(), TotalUtMarzo.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);

                CeldaTotal("I" + ii.ToString(), TotalAbril.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("J" + ii.ToString(), TotalUtAbril.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("K" + ii.ToString(), TotalMayo.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("L" + ii.ToString(), TotalUtMayo.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("M" + ii.ToString(), TotalJunio.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("N" + ii.ToString(), TotalUtJunio.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);

                CeldaTotal("O" + ii.ToString(), TotalJulio.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("P" + ii.ToString(), TotalUtJulio.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("Q" + ii.ToString(), TotalAgosto.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("R" + ii.ToString(), TotalUtAgosto.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("S" + ii.ToString(), TotalSeptiembre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("T" + ii.ToString(), TotalUtSeptiembre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);

                CeldaTotal("U" + ii.ToString(), TotalOctubre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("V" + ii.ToString(), TotalUtOctubre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("W" + ii.ToString(), TotalNoviembre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("X" + ii.ToString(), TotalUtNoviembre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("Y" + ii.ToString(), TotalDiciembre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("Z" + ii.ToString(), TotalUtDiciembre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);

                CeldaTotal("AA" + ii.ToString(), TotalTotal.ToString("C"), XLAlignmentHorizontalValues.Right, true, true, ref wwsss);
                CeldaTotal("AB" + ii.ToString(), UtTotalTotal.ToString("C"), XLAlignmentHorizontalValues.Right, true, true, ref wwsss);
            }

            if ((iIndica == 2))
            {
                CeldaTotal("C" + ii.ToString(), TotalUnEnero.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("D" + ii.ToString(), TotalUnFebrero.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("E" + ii.ToString(), TotalUnMarzo.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);

                CeldaTotal("F" + ii.ToString(), TotalUnAbril.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("G" + ii.ToString(), TotalUnMayo.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("H" + ii.ToString(), TotalUnJunio.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);

                CeldaTotal("I" + ii.ToString(), TotalUnJulio.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("J" + ii.ToString(), TotalUnAgosto.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("K" + ii.ToString(), TotalUnSeptiembre.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);

                CeldaTotal("L" + ii.ToString(), TotalUnOctubre.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("M" + ii.ToString(), TotalUnNoviembre.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("N" + ii.ToString(), TotalUnDiciembre.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);

                CeldaTotal("O" + ii.ToString(), UnTotalTotal.ToString(), XLAlignmentHorizontalValues.Right, true, true, ref wwsss);
            }

            if ((iIndica == 0) && (bUtil == false))
            {
                CeldaTotal("C" + ii.ToString(), TotalEnero.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("D" + ii.ToString(), TotalUnEnero.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("E" + ii.ToString(), TotalFebrero.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("F" + ii.ToString(), TotalUnFebrero.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("G" + ii.ToString(), TotalMarzo.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("H" + ii.ToString(), TotalUnMarzo.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);

                CeldaTotal("I" + ii.ToString(), TotalAbril.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("J" + ii.ToString(), TotalUnAbril.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("K" + ii.ToString(), TotalMayo.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("L" + ii.ToString(), TotalUnMayo.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("M" + ii.ToString(), TotalJunio.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("N" + ii.ToString(), TotalUnJunio.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);

                CeldaTotal("O" + ii.ToString(), TotalJulio.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("P" + ii.ToString(), TotalUnJulio.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("Q" + ii.ToString(), TotalAgosto.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("R" + ii.ToString(), TotalUnAgosto.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("S" + ii.ToString(), TotalSeptiembre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("T" + ii.ToString(), TotalUnSeptiembre.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);

                CeldaTotal("U" + ii.ToString(), TotalOctubre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("V" + ii.ToString(), TotalUnOctubre.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("W" + ii.ToString(), TotalNoviembre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("X" + ii.ToString(), TotalUnNoviembre.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("Y" + ii.ToString(), TotalDiciembre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("Z" + ii.ToString(), TotalUnDiciembre.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);

                CeldaTotal("AA" + ii.ToString(), TotalTotal.ToString("C"), XLAlignmentHorizontalValues.Right, true, true, ref wwsss);
                CeldaTotal("AB" + ii.ToString(), UnTotalTotal.ToString(), XLAlignmentHorizontalValues.Right, true, true, ref wwsss);
            }

            if ((iIndica == 0) && (bUtil))
            {
                CeldaTotal("C" + ii.ToString(), TotalEnero.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("D" + ii.ToString(), TotalUnEnero.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("E" + ii.ToString(), TotalUtEnero.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("F" + ii.ToString(), TotalFebrero.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("G" + ii.ToString(), TotalUnFebrero.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("H" + ii.ToString(), TotalUtFebrero.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("I" + ii.ToString(), TotalMarzo.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("J" + ii.ToString(), TotalUnMarzo.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("K" + ii.ToString(), TotalUtMarzo.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);

                CeldaTotal("L" + ii.ToString(), TotalAbril.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("M" + ii.ToString(), TotalUnAbril.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("N" + ii.ToString(), TotalUtAbril.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("O" + ii.ToString(), TotalMayo.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("P" + ii.ToString(), TotalUnMayo.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("Q" + ii.ToString(), TotalUtMayo.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("R" + ii.ToString(), TotalJunio.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("S" + ii.ToString(), TotalUnJunio.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("T" + ii.ToString(), TotalUtJunio.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);

                CeldaTotal("U" + ii.ToString(), TotalJulio.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("V" + ii.ToString(), TotalUnJulio.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("W" + ii.ToString(), TotalUtJulio.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("X" + ii.ToString(), TotalAgosto.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("Y" + ii.ToString(), TotalUnAgosto.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("Z" + ii.ToString(), TotalUtAgosto.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("AA" + ii.ToString(), TotalSeptiembre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("AB" + ii.ToString(), TotalUnSeptiembre.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("AC" + ii.ToString(), TotalUtSeptiembre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);

                CeldaTotal("AD" + ii.ToString(), TotalOctubre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("AE" + ii.ToString(), TotalUnOctubre.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("AF" + ii.ToString(), TotalUtOctubre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("AG" + ii.ToString(), TotalNoviembre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("AH" + ii.ToString(), TotalUnNoviembre.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("AI" + ii.ToString(), TotalUtNoviembre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("AJ" + ii.ToString(), TotalDiciembre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("AK" + ii.ToString(), TotalUnDiciembre.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("AL" + ii.ToString(), TotalUtDiciembre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);

                CeldaTotal("AM" + ii.ToString(), TotalTotal.ToString("C"), XLAlignmentHorizontalValues.Right, true, true, ref wwsss);
                CeldaTotal("AN" + ii.ToString(), UnTotalTotal.ToString(), XLAlignmentHorizontalValues.Right, true, true, ref wwsss);
                CeldaTotal("AO" + ii.ToString(), UtTotalTotal.ToString("C"), XLAlignmentHorizontalValues.Right, true, true, ref wwsss);
            }

        }

        void DatosExcelTotalDeTotalesAG(List<RVDReporte> listado, int ii, string strTotal, int iIndica, bool bUtil, ref IXLWorksheet wwsss)
        {
            float TotalTotal = listado.Select(m => m.Enero + m.Febrero + m.Marzo + m.Abril + m.Mayo + m.Junio + m.Julio + m.Agosto + m.Septiembre + m.Octubre + m.Noviembre + m.Diciembre).Sum();
            int UnTotalTotal = listado.Select(m => m.UnEnero + m.UnFebrero + m.UnMarzo + m.UnAbril + m.UnMayo + m.UnJunio + m.UnJulio + m.UnAgosto + m.UnSeptiembre + m.UnOctubre + m.UnNoviembre + m.UnDiciembre).Sum();
            float UtTotalTotal = listado.Select(m => m.UtEnero + m.UtFebrero + m.UtMarzo + m.UtAbril + m.UtMayo + m.UtJunio + m.UtJulio + m.UtAgosto + m.UtSeptiembre + m.UtOctubre + m.UtNoviembre + m.UtDiciembre).Sum();

            float TotalEnero = listado.Select(m => m.Enero).Sum();
            float TotalFebrero = listado.Select(m => m.Febrero).Sum();
            float TotalMarzo = listado.Select(m => m.Marzo).Sum();
            float TotalAbril = listado.Select(m => m.Abril).Sum();
            float TotalMayo = listado.Select(m => m.Mayo).Sum();
            float TotalJunio = listado.Select(m => m.Junio).Sum();
            float TotalJulio = listado.Select(m => m.Julio).Sum();
            float TotalAgosto = listado.Select(m => m.Agosto).Sum();
            float TotalSeptiembre = listado.Select(m => m.Septiembre).Sum();
            float TotalOctubre = listado.Select(m => m.Octubre).Sum();
            float TotalNoviembre = listado.Select(m => m.Noviembre).Sum();
            float TotalDiciembre = listado.Select(m => m.Diciembre).Sum();

            int TotalUnEnero = listado.Select(m => m.UnEnero).Sum();
            int TotalUnFebrero = listado.Select(m => m.UnFebrero).Sum();
            int TotalUnMarzo = listado.Select(m => m.UnMarzo).Sum();
            int TotalUnAbril = listado.Select(m => m.UnAbril).Sum();
            int TotalUnMayo = listado.Select(m => m.UnMayo).Sum();
            int TotalUnJunio = listado.Select(m => m.UnJunio).Sum();
            int TotalUnJulio = listado.Select(m => m.UnJulio).Sum();
            int TotalUnAgosto = listado.Select(m => m.UnAgosto).Sum();
            int TotalUnSeptiembre = listado.Select(m => m.UnSeptiembre).Sum();
            int TotalUnOctubre = listado.Select(m => m.UnOctubre).Sum();
            int TotalUnNoviembre = listado.Select(m => m.UnNoviembre).Sum();
            int TotalUnDiciembre = listado.Select(m => m.UnDiciembre).Sum();

            float TotalUtEnero = listado.Select(m => m.UtEnero).Sum();
            float TotalUtFebrero = listado.Select(m => m.UtFebrero).Sum();
            float TotalUtMarzo = listado.Select(m => m.UtMarzo).Sum();
            float TotalUtAbril = listado.Select(m => m.UtAbril).Sum();
            float TotalUtMayo = listado.Select(m => m.UtMayo).Sum();
            float TotalUtJunio = listado.Select(m => m.UtJunio).Sum();
            float TotalUtJulio = listado.Select(m => m.UtJulio).Sum();
            float TotalUtAgosto = listado.Select(m => m.UtAgosto).Sum();
            float TotalUtSeptiembre = listado.Select(m => m.UtSeptiembre).Sum();
            float TotalUtOctubre = listado.Select(m => m.UtOctubre).Sum();
            float TotalUtNoviembre = listado.Select(m => m.UtNoviembre).Sum();
            float TotalUtDiciembre = listado.Select(m => m.UtDiciembre).Sum();

            wwsss.Range("A" + ii.ToString() + ":C" + ii.ToString()).Merge();
            CeldaTotal("A" + ii.ToString(), strTotal, XLAlignmentHorizontalValues.Right, true, false, ref wwsss);
            /// Pesos       indicador 1
            /// Unidades    indicador 2
            /// Ambos       indicador 0
            /// bUtil       incluye utilidad
            if ((iIndica == 1) && (bUtil == false))
            {
                CeldaTotal("D" + ii.ToString(), TotalEnero.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("E" + ii.ToString(), TotalFebrero.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("F" + ii.ToString(), TotalMarzo.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);

                CeldaTotal("G" + ii.ToString(), TotalAbril.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("H" + ii.ToString(), TotalMayo.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("I" + ii.ToString(), TotalJunio.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);

                CeldaTotal("J" + ii.ToString(), TotalJulio.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("K" + ii.ToString(), TotalAgosto.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("L" + ii.ToString(), TotalSeptiembre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);

                CeldaTotal("M" + ii.ToString(), TotalOctubre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("N" + ii.ToString(), TotalNoviembre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("O" + ii.ToString(), TotalDiciembre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);

                CeldaTotal("P" + ii.ToString(), TotalTotal.ToString("C"), XLAlignmentHorizontalValues.Right, true, true, ref wwsss);
            }

            if ((iIndica == 1) && (bUtil))
            {
                CeldaTotal("D" + ii.ToString(), TotalEnero.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("E" + ii.ToString(), TotalUtEnero.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("F" + ii.ToString(), TotalFebrero.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("G" + ii.ToString(), TotalUtFebrero.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("H" + ii.ToString(), TotalMarzo.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("I" + ii.ToString(), TotalUtMarzo.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);

                CeldaTotal("J" + ii.ToString(), TotalAbril.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("K" + ii.ToString(), TotalUtAbril.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("L" + ii.ToString(), TotalMayo.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("M" + ii.ToString(), TotalUtMayo.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("N" + ii.ToString(), TotalJunio.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("O" + ii.ToString(), TotalUtJunio.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);

                CeldaTotal("P" + ii.ToString(), TotalJulio.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("Q" + ii.ToString(), TotalUtJulio.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("R" + ii.ToString(), TotalAgosto.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("S" + ii.ToString(), TotalUtAgosto.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("T" + ii.ToString(), TotalSeptiembre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("U" + ii.ToString(), TotalUtSeptiembre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);

                CeldaTotal("V" + ii.ToString(), TotalOctubre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("W" + ii.ToString(), TotalUtOctubre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("X" + ii.ToString(), TotalNoviembre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("Y" + ii.ToString(), TotalUtNoviembre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("Z" + ii.ToString(), TotalDiciembre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("AA" + ii.ToString(), TotalUtDiciembre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);

                CeldaTotal("AB" + ii.ToString(), TotalTotal.ToString("C"), XLAlignmentHorizontalValues.Right, true, true, ref wwsss);
                CeldaTotal("AC" + ii.ToString(), UtTotalTotal.ToString("C"), XLAlignmentHorizontalValues.Right, true, true, ref wwsss);
            }

            if ((iIndica == 2))
            {
                CeldaTotal("D" + ii.ToString(), TotalUnEnero.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("E" + ii.ToString(), TotalUnFebrero.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("F" + ii.ToString(), TotalUnMarzo.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);

                CeldaTotal("G" + ii.ToString(), TotalUnAbril.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("H" + ii.ToString(), TotalUnMayo.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("I" + ii.ToString(), TotalUnJunio.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);

                CeldaTotal("J" + ii.ToString(), TotalUnJulio.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("K" + ii.ToString(), TotalUnAgosto.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("L" + ii.ToString(), TotalUnSeptiembre.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);

                CeldaTotal("M" + ii.ToString(), TotalUnOctubre.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("N" + ii.ToString(), TotalUnNoviembre.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("O" + ii.ToString(), TotalUnDiciembre.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);

                CeldaTotal("P" + ii.ToString(), UnTotalTotal.ToString(), XLAlignmentHorizontalValues.Right, true, true, ref wwsss);
            }

            if ((iIndica == 0) && (bUtil == false))
            {
                CeldaTotal("D" + ii.ToString(), TotalEnero.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("E" + ii.ToString(), TotalUnEnero.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("F" + ii.ToString(), TotalFebrero.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("G" + ii.ToString(), TotalUnFebrero.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("H" + ii.ToString(), TotalMarzo.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("I" + ii.ToString(), TotalUnMarzo.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);

                CeldaTotal("J" + ii.ToString(), TotalAbril.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("K" + ii.ToString(), TotalUnAbril.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("L" + ii.ToString(), TotalMayo.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("M" + ii.ToString(), TotalUnMayo.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("N" + ii.ToString(), TotalJunio.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("O" + ii.ToString(), TotalUnJunio.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);

                CeldaTotal("P" + ii.ToString(), TotalJulio.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("Q" + ii.ToString(), TotalUnJulio.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("R" + ii.ToString(), TotalAgosto.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("S" + ii.ToString(), TotalUnAgosto.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("T" + ii.ToString(), TotalSeptiembre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("U" + ii.ToString(), TotalUnSeptiembre.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);

                CeldaTotal("V" + ii.ToString(), TotalOctubre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("W" + ii.ToString(), TotalUnOctubre.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("X" + ii.ToString(), TotalNoviembre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("Y" + ii.ToString(), TotalUnNoviembre.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("Z" + ii.ToString(), TotalDiciembre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("AA" + ii.ToString(), TotalUnDiciembre.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);

                CeldaTotal("AB" + ii.ToString(), TotalTotal.ToString("C"), XLAlignmentHorizontalValues.Right, true, true, ref wwsss);
                CeldaTotal("AC" + ii.ToString(), UnTotalTotal.ToString(), XLAlignmentHorizontalValues.Right, true, true, ref wwsss);
            }

            if ((iIndica == 0) && (bUtil))
            {
                CeldaTotal("D" + ii.ToString(), TotalEnero.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("E" + ii.ToString(), TotalUnEnero.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("F" + ii.ToString(), TotalUtEnero.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("G" + ii.ToString(), TotalFebrero.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("H" + ii.ToString(), TotalUnFebrero.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("I" + ii.ToString(), TotalUtFebrero.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("J" + ii.ToString(), TotalMarzo.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("K" + ii.ToString(), TotalUnMarzo.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("L" + ii.ToString(), TotalUtMarzo.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);

                CeldaTotal("M" + ii.ToString(), TotalAbril.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("N" + ii.ToString(), TotalUnAbril.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("O" + ii.ToString(), TotalUtAbril.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("P" + ii.ToString(), TotalMayo.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("Q" + ii.ToString(), TotalUnMayo.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("R" + ii.ToString(), TotalUtMayo.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("S" + ii.ToString(), TotalJunio.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("T" + ii.ToString(), TotalUnJunio.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("U" + ii.ToString(), TotalUtJunio.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);

                CeldaTotal("V" + ii.ToString(), TotalJulio.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("W" + ii.ToString(), TotalUnJulio.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("X" + ii.ToString(), TotalUtJulio.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("Y" + ii.ToString(), TotalAgosto.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("Z" + ii.ToString(), TotalUnAgosto.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("AA" + ii.ToString(), TotalUtAgosto.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("AB" + ii.ToString(), TotalSeptiembre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("AC" + ii.ToString(), TotalUnSeptiembre.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("AD" + ii.ToString(), TotalUtSeptiembre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);

                CeldaTotal("AE" + ii.ToString(), TotalOctubre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("AF" + ii.ToString(), TotalUnOctubre.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("AG" + ii.ToString(), TotalUtOctubre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("AH" + ii.ToString(), TotalNoviembre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("AI" + ii.ToString(), TotalUnNoviembre.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("AJ" + ii.ToString(), TotalUtNoviembre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("AK" + ii.ToString(), TotalDiciembre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("AL" + ii.ToString(), TotalUnDiciembre.ToString(), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);
                CeldaTotal("AM" + ii.ToString(), TotalUtDiciembre.ToString("C"), XLAlignmentHorizontalValues.Right, false, true, ref wwsss);

                CeldaTotal("AN" + ii.ToString(), TotalTotal.ToString("C"), XLAlignmentHorizontalValues.Right, true, true, ref wwsss);
                CeldaTotal("AO" + ii.ToString(), UnTotalTotal.ToString(), XLAlignmentHorizontalValues.Right, true, true, ref wwsss);
                CeldaTotal("AP" + ii.ToString(), UtTotalTotal.ToString("C"), XLAlignmentHorizontalValues.Right, true, true, ref wwsss);
            }

        }

        #endregion

        #region FormatoDatos

        #region DatosSinAgrupar

        void DatosExcelMontosSinUtilidadSA(int ii, RVDReporte ren, ref IXLWorksheet wwsss)
        {
            CeldaDatos("A" + ii.ToString(), ren.Id.ToString(), XLAlignmentHorizontalValues.Center, ref wwsss);
            CeldaDatos("B" + ii.ToString(), ren.Nombre.ToString(), XLAlignmentHorizontalValues.Left, ref wwsss);

            CeldaDatos("C" + ii.ToString(), ren.Enero.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("D" + ii.ToString(), ren.Febrero.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("E" + ii.ToString(), ren.Marzo.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);

            CeldaDatos("F" + ii.ToString(), ren.Abril.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("G" + ii.ToString(), ren.Mayo.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("H" + ii.ToString(), ren.Junio.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);

            CeldaDatos("I" + ii.ToString(), ren.Julio.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("J" + ii.ToString(), ren.Agosto.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("K" + ii.ToString(), ren.Septiembre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);

            CeldaDatos("L" + ii.ToString(), ren.Octubre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("M" + ii.ToString(), ren.Noviembre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("N" + ii.ToString(), ren.Diciembre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);

            CeldaDatos("O" + ii.ToString(), ren.Total.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
        }

        void DatosExcelMontosConUtilidadSA(int ii, RVDReporte ren, ref IXLWorksheet wwsss)
        {
            CeldaDatos("A" + ii.ToString(), ren.Id.ToString(), XLAlignmentHorizontalValues.Center, ref wwsss);
            CeldaDatos("B" + ii.ToString(), ren.Nombre.ToString(), XLAlignmentHorizontalValues.Left, ref wwsss);


            CeldaDatos("C" + ii.ToString(), ren.Enero.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("D" + ii.ToString(), ren.UtEnero.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("E" + ii.ToString(), ren.Febrero.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("F" + ii.ToString(), ren.UtFebrero.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("G" + ii.ToString(), ren.Marzo.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("H" + ii.ToString(), ren.UtMarzo.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);

            CeldaDatos("I" + ii.ToString(), ren.Abril.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("J" + ii.ToString(), ren.UtAbril.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("K" + ii.ToString(), ren.Mayo.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("L" + ii.ToString(), ren.UtMayo.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("M" + ii.ToString(), ren.Junio.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("N" + ii.ToString(), ren.UtJunio.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);

            CeldaDatos("O" + ii.ToString(), ren.Julio.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("P" + ii.ToString(), ren.UtJulio.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("Q" + ii.ToString(), ren.Agosto.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("R" + ii.ToString(), ren.UtAgosto.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("S" + ii.ToString(), ren.Septiembre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("T" + ii.ToString(), ren.UtSeptiembre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);

            CeldaDatos("U" + ii.ToString(), ren.Octubre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("V" + ii.ToString(), ren.UtOctubre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("W" + ii.ToString(), ren.Noviembre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("X" + ii.ToString(), ren.UtNoviembre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("Y" + ii.ToString(), ren.Diciembre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("Z" + ii.ToString(), ren.UtDiciembre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);

            CeldaDatos("AA" + ii.ToString(), ren.Total.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("AB" + ii.ToString(), ren.UtTotal.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
        }

        void DatosExcelUnidadesSinUtilidadSA(int ii, RVDReporte ren, ref IXLWorksheet wwsss)
        {
            CeldaDatos("A" + ii.ToString(), ren.Id.ToString(), XLAlignmentHorizontalValues.Center, ref wwsss);
            CeldaDatos("B" + ii.ToString(), ren.Nombre.ToString(), XLAlignmentHorizontalValues.Left, ref wwsss);

            CeldaDatos("C" + ii.ToString(), ren.UnEnero.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("D" + ii.ToString(), ren.UnFebrero.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("E" + ii.ToString(), ren.UnMarzo.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);

            CeldaDatos("F" + ii.ToString(), ren.UnAbril.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("G" + ii.ToString(), ren.UnMayo.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("H" + ii.ToString(), ren.UnJunio.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);

            CeldaDatos("I" + ii.ToString(), ren.UnJulio.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("J" + ii.ToString(), ren.UnAgosto.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("K" + ii.ToString(), ren.UnSeptiembre.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);

            CeldaDatos("L" + ii.ToString(), ren.UnOctubre.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("M" + ii.ToString(), ren.UnNoviembre.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("N" + ii.ToString(), ren.UnDiciembre.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);

            CeldaDatos("O" + ii.ToString(), ren.UnTotal.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
        }

        void DatosExcelMontosYUnidadesSinUtilidadSA(int ii, RVDReporte ren, ref IXLWorksheet wwsss)
        {
            CeldaDatos("A" + ii.ToString(), ren.Id.ToString(), XLAlignmentHorizontalValues.Center, ref wwsss);
            CeldaDatos("B" + ii.ToString(), ren.Nombre.ToString(), XLAlignmentHorizontalValues.Left, ref wwsss);

            CeldaDatos("C" + ii.ToString(), ren.Enero.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("D" + ii.ToString(), ren.UnEnero.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("E" + ii.ToString(), ren.Febrero.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("F" + ii.ToString(), ren.UnFebrero.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("G" + ii.ToString(), ren.Marzo.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("H" + ii.ToString(), ren.UnMarzo.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);

            CeldaDatos("I" + ii.ToString(), ren.Abril.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("J" + ii.ToString(), ren.UnAbril.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("K" + ii.ToString(), ren.Mayo.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("L" + ii.ToString(), ren.UnMayo.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("M" + ii.ToString(), ren.Junio.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("N" + ii.ToString(), ren.UnJunio.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);

            CeldaDatos("O" + ii.ToString(), ren.Julio.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("P" + ii.ToString(), ren.UnJulio.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("Q" + ii.ToString(), ren.Agosto.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("R" + ii.ToString(), ren.UnAgosto.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("S" + ii.ToString(), ren.Septiembre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("T" + ii.ToString(), ren.UnSeptiembre.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);

            CeldaDatos("U" + ii.ToString(), ren.Octubre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("V" + ii.ToString(), ren.UnOctubre.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("W" + ii.ToString(), ren.Noviembre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("X" + ii.ToString(), ren.UnNoviembre.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("Y" + ii.ToString(), ren.Diciembre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("Z" + ii.ToString(), ren.UnDiciembre.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);

            CeldaDatos("AA" + ii.ToString(), ren.Total.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("AB" + ii.ToString(), ren.UnTotal.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
        }

        void DatosExcelMontosYUnidadesConUtilidadSA(int ii, RVDReporte ren, ref IXLWorksheet wwsss)
        {
            CeldaDatos("A" + ii.ToString(), ren.Id.ToString(), XLAlignmentHorizontalValues.Center, ref wwsss);
            CeldaDatos("B" + ii.ToString(), ren.Nombre.ToString(), XLAlignmentHorizontalValues.Left, ref wwsss);

            CeldaDatos("C" + ii.ToString(), ren.Enero.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("D" + ii.ToString(), ren.UnEnero.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("E" + ii.ToString(), ren.UtEnero.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("F" + ii.ToString(), ren.Febrero.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("G" + ii.ToString(), ren.UnFebrero.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("H" + ii.ToString(), ren.UtFebrero.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("I" + ii.ToString(), ren.Marzo.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("J" + ii.ToString(), ren.UnMarzo.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("K" + ii.ToString(), ren.UtMarzo.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);

            CeldaDatos("L" + ii.ToString(), ren.Abril.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("M" + ii.ToString(), ren.UnAbril.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("N" + ii.ToString(), ren.UtAbril.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("O" + ii.ToString(), ren.Mayo.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("P" + ii.ToString(), ren.UnMayo.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("Q" + ii.ToString(), ren.UtMayo.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("R" + ii.ToString(), ren.Junio.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("S" + ii.ToString(), ren.UnJunio.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("T" + ii.ToString(), ren.UtJunio.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);

            CeldaDatos("U" + ii.ToString(), ren.Julio.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("V" + ii.ToString(), ren.UnJulio.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("W" + ii.ToString(), ren.UtJulio.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("X" + ii.ToString(), ren.Agosto.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("Y" + ii.ToString(), ren.UnAgosto.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("Z" + ii.ToString(), ren.UtAgosto.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("AA" + ii.ToString(), ren.Septiembre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("AB" + ii.ToString(), ren.UnSeptiembre.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("AC" + ii.ToString(), ren.UtSeptiembre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);

            CeldaDatos("AD" + ii.ToString(), ren.Octubre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("AE" + ii.ToString(), ren.UnOctubre.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("AF" + ii.ToString(), ren.UtOctubre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("AG" + ii.ToString(), ren.Noviembre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("AH" + ii.ToString(), ren.UnNoviembre.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("AI" + ii.ToString(), ren.UtNoviembre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("AJ" + ii.ToString(), ren.Diciembre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("AK" + ii.ToString(), ren.UnDiciembre.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("AL" + ii.ToString(), ren.UtDiciembre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);

            CeldaDatos("AM" + ii.ToString(), ren.Total.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("AN" + ii.ToString(), ren.UnTotal.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("AO" + ii.ToString(), ren.UtTotal.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
        }

        #endregion

        #region DatosAgrupados

        void DatosExcelMontosSinUtilidadAG(int ii, RVDReporte ren, ref IXLWorksheet wwsss)
        {
            CeldaDatos("B" + ii.ToString(), ren.IdPrd.ToString(), XLAlignmentHorizontalValues.Center, ref wwsss);
            CeldaDatos("C" + ii.ToString(), ren.PdrDescrip.ToString(), XLAlignmentHorizontalValues.Left, ref wwsss);

            CeldaDatos("D" + ii.ToString(), ren.Enero.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("E" + ii.ToString(), ren.Febrero.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("F" + ii.ToString(), ren.Marzo.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);

            CeldaDatos("G" + ii.ToString(), ren.Abril.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("H" + ii.ToString(), ren.Mayo.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("I" + ii.ToString(), ren.Junio.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);

            CeldaDatos("J" + ii.ToString(), ren.Julio.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("K" + ii.ToString(), ren.Agosto.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("L" + ii.ToString(), ren.Septiembre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);

            CeldaDatos("M" + ii.ToString(), ren.Octubre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("N" + ii.ToString(), ren.Noviembre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("O" + ii.ToString(), ren.Diciembre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);

            CeldaDatos("P" + ii.ToString(), ren.Total.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
        }

        void DatosExcelMontosConUtilidadAG(int ii, RVDReporte ren, ref IXLWorksheet wwsss)
        {
            CeldaDatos("B" + ii.ToString(), ren.IdPrd.ToString(), XLAlignmentHorizontalValues.Center, ref wwsss);
            CeldaDatos("C" + ii.ToString(), ren.PdrDescrip.ToString(), XLAlignmentHorizontalValues.Left, ref wwsss);

            CeldaDatos("D" + ii.ToString(), ren.Enero.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("E" + ii.ToString(), ren.UtEnero.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("F" + ii.ToString(), ren.Febrero.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("G" + ii.ToString(), ren.UtFebrero.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("H" + ii.ToString(), ren.Marzo.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("I" + ii.ToString(), ren.UtMarzo.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);

            CeldaDatos("J" + ii.ToString(), ren.Abril.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("K" + ii.ToString(), ren.UtAbril.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("L" + ii.ToString(), ren.Mayo.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("M" + ii.ToString(), ren.UtMayo.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("N" + ii.ToString(), ren.Junio.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("O" + ii.ToString(), ren.UtJunio.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);

            CeldaDatos("P" + ii.ToString(), ren.Julio.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("Q" + ii.ToString(), ren.UtJulio.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("R" + ii.ToString(), ren.Agosto.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("S" + ii.ToString(), ren.UtAgosto.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("T" + ii.ToString(), ren.Septiembre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("U" + ii.ToString(), ren.UtSeptiembre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);

            CeldaDatos("V" + ii.ToString(), ren.Octubre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("W" + ii.ToString(), ren.UtOctubre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("X" + ii.ToString(), ren.Noviembre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("Y" + ii.ToString(), ren.UtNoviembre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("Z" + ii.ToString(), ren.Diciembre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("AA" + ii.ToString(), ren.UtDiciembre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);

            CeldaDatos("AB" + ii.ToString(), ren.Total.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("AC" + ii.ToString(), ren.UtTotal.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
        }

        void DatosExcelUnidadesSinUtilidadAG(int ii, RVDReporte ren, ref IXLWorksheet wwsss)
        {
            CeldaDatos("B" + ii.ToString(), ren.IdPrd.ToString(), XLAlignmentHorizontalValues.Center, ref wwsss);
            CeldaDatos("C" + ii.ToString(), ren.PdrDescrip.ToString(), XLAlignmentHorizontalValues.Left, ref wwsss);

            CeldaDatos("D" + ii.ToString(), ren.UnEnero.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("E" + ii.ToString(), ren.UnFebrero.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("F" + ii.ToString(), ren.UnMarzo.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);

            CeldaDatos("G" + ii.ToString(), ren.UnAbril.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("H" + ii.ToString(), ren.UnMayo.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("I" + ii.ToString(), ren.UnJunio.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);

            CeldaDatos("J" + ii.ToString(), ren.UnJulio.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("K" + ii.ToString(), ren.UnAgosto.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("L" + ii.ToString(), ren.UnSeptiembre.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);

            CeldaDatos("M" + ii.ToString(), ren.UnOctubre.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("N" + ii.ToString(), ren.UnNoviembre.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("O" + ii.ToString(), ren.UnDiciembre.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);

            CeldaDatos("P" + ii.ToString(), ren.UnTotal.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
        }

        void DatosExcelMontosYUnidadesSinUtilidadAG(int ii, RVDReporte ren, ref IXLWorksheet wwsss)
        {
            CeldaDatos("B" + ii.ToString(), ren.IdPrd.ToString(), XLAlignmentHorizontalValues.Center, ref wwsss);
            CeldaDatos("C" + ii.ToString(), ren.PdrDescrip.ToString(), XLAlignmentHorizontalValues.Left, ref wwsss);

            CeldaDatos("D" + ii.ToString(), ren.Enero.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("E" + ii.ToString(), ren.UnEnero.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("F" + ii.ToString(), ren.Febrero.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("G" + ii.ToString(), ren.UnFebrero.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("H" + ii.ToString(), ren.Marzo.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("I" + ii.ToString(), ren.UnMarzo.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);

            CeldaDatos("J" + ii.ToString(), ren.Abril.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("K" + ii.ToString(), ren.UnAbril.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("L" + ii.ToString(), ren.Mayo.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("M" + ii.ToString(), ren.UnMayo.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("N" + ii.ToString(), ren.Junio.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("O" + ii.ToString(), ren.UnJunio.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);

            CeldaDatos("P" + ii.ToString(), ren.Julio.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("Q" + ii.ToString(), ren.UnJulio.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("R" + ii.ToString(), ren.Agosto.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("S" + ii.ToString(), ren.UnAgosto.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("T" + ii.ToString(), ren.Septiembre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("U" + ii.ToString(), ren.UnSeptiembre.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);

            CeldaDatos("V" + ii.ToString(), ren.Octubre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("W" + ii.ToString(), ren.UnOctubre.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("X" + ii.ToString(), ren.Noviembre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("Y" + ii.ToString(), ren.UnNoviembre.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("Z" + ii.ToString(), ren.Diciembre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("AA" + ii.ToString(), ren.UnDiciembre.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);

            CeldaDatos("AB" + ii.ToString(), ren.Total.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("AC" + ii.ToString(), ren.UnTotal.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
        }

        void DatosExcelMontosYUnidadesConUtilidadAG(int ii, RVDReporte ren, ref IXLWorksheet wwsss)
        {
            CeldaDatos("B" + ii.ToString(), ren.IdPrd.ToString(), XLAlignmentHorizontalValues.Center, ref wwsss);
            CeldaDatos("C" + ii.ToString(), ren.PdrDescrip.ToString(), XLAlignmentHorizontalValues.Left, ref wwsss);

            CeldaDatos("D" + ii.ToString(), ren.Enero.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("E" + ii.ToString(), ren.UnEnero.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("F" + ii.ToString(), ren.UtEnero.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("G" + ii.ToString(), ren.Febrero.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("H" + ii.ToString(), ren.UnFebrero.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("I" + ii.ToString(), ren.UtFebrero.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("J" + ii.ToString(), ren.Marzo.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("K" + ii.ToString(), ren.UnMarzo.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("L" + ii.ToString(), ren.UtMarzo.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);

            CeldaDatos("M" + ii.ToString(), ren.Abril.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("N" + ii.ToString(), ren.UnAbril.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("O" + ii.ToString(), ren.UtAbril.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("P" + ii.ToString(), ren.Mayo.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("Q" + ii.ToString(), ren.UnMayo.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("R" + ii.ToString(), ren.UtMayo.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("S" + ii.ToString(), ren.Junio.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("T" + ii.ToString(), ren.UnJunio.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("U" + ii.ToString(), ren.UtJunio.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);

            CeldaDatos("V" + ii.ToString(), ren.Julio.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("W" + ii.ToString(), ren.UnJulio.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("X" + ii.ToString(), ren.UtJulio.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("Y" + ii.ToString(), ren.Agosto.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("Z" + ii.ToString(), ren.UnAgosto.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("AA" + ii.ToString(), ren.UtAgosto.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("AB" + ii.ToString(), ren.Septiembre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("AC" + ii.ToString(), ren.UnSeptiembre.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("AD" + ii.ToString(), ren.UtSeptiembre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);

            CeldaDatos("AE" + ii.ToString(), ren.Octubre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("AF" + ii.ToString(), ren.UnOctubre.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("AG" + ii.ToString(), ren.UtOctubre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("AH" + ii.ToString(), ren.Noviembre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("AI" + ii.ToString(), ren.UnNoviembre.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("AJ" + ii.ToString(), ren.UtNoviembre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("AK" + ii.ToString(), ren.Diciembre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("AL" + ii.ToString(), ren.UnDiciembre.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("AM" + ii.ToString(), ren.UtDiciembre.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);

            CeldaDatos("AN" + ii.ToString(), ren.Total.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("AO" + ii.ToString(), ren.UnTotal.ToString(), XLAlignmentHorizontalValues.Right, ref wwsss);
            CeldaDatos("AP" + ii.ToString(), ren.UtTotal.ToString("C"), XLAlignmentHorizontalValues.Right, ref wwsss);
        }

        #endregion

        #endregion

    }
}