using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using ClosedXML.Excel;
using CapaEntidad;
using CapaNegocios;
using System.Data;
using System.IO;
using System.Reflection;

namespace SIANWEB.Dashboard
{
    public partial class dshNacIndicadoresACyS : System.Web.UI.Page
    {

        public string strCDIS = "";
        public string strDatoCDIS = "";

        public string strResumen = "";
        public string strMiddle = "";
        public string strLista = "";

        public List<DashboardACyS_CDIACyS> BaseCDIs;
        public List<DashboardACyS_RIKS> BaseRIKSs;

        public List<DashboardACyS_RIKS> ExcelBaseDeRiks;
        public List<DashboardACyS_DetalleRIKS> ExcelDeRiks;
        public List<DashboardACyS_Estatus> ExcelDeEstatus;

        public List<DashboardACyS_Resumen> ExcelResumen;
        public List<DashboardACyS_Clientes> ExcelClientesCon;
        public List<DashboardACyS_Clientes> ExcelClientesSin;
        public List<DashboardACyS_DetalleACyS> ExcelDetalleACyS;



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
                        CargarCentros();
                        CargarCDCs();
                        CargarFranquicias();
                        llenaDashboard();
                    }
                }
            }
            catch (Exception ex)
            {
                this.ErrorManager(ex, "Page_Load");
            }
        }

        #region Funciones

        void CargarCentros()
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_dshIndicadoresACyS clsdshIndicadoresACyS = new CN_dshIndicadoresACyS();
                List<Renglon> lista = new List<Renglon>();

                clsdshIndicadoresACyS.LlenaCDINac(1, sesion.Emp_Cnx, ref lista);

                this.lstchkCDIS.DataValueField = "idRng";
                this.lstchkCDIS.DataTextField = "sValor";
                this.lstchkCDIS.DataSource = lista; /// .OrderBy(i => i.sDescripcion);
                this.lstchkCDIS.DataBind();

                foreach (ListItem ch in lstchkCDIS.Items)
                {
                    ch.Selected = true;
                }

            }
            catch (Exception ex)
            {
                //throw ex;
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        void CargarCDCs()
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_dshIndicadoresACyS clsdshIndicadoresACyS = new CN_dshIndicadoresACyS();
                List<Renglon> lista = new List<Renglon>();

                clsdshIndicadoresACyS.LlenaCDINac(2, sesion.Emp_Cnx, ref lista);

                this.lstchkCDC.DataValueField = "idRng";
                this.lstchkCDC.DataTextField = "sValor";
                this.lstchkCDC.DataSource = lista; /// .OrderBy(i => i.sDescripcion);
                this.lstchkCDC.DataBind();

                foreach (ListItem ch in lstchkCDC.Items)
                {
                    ch.Selected = true;
                }

            }
            catch (Exception ex)
            {
                //throw ex;
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        void CargarFranquicias()
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_dshIndicadoresACyS clsdshIndicadoresACyS = new CN_dshIndicadoresACyS();
                List<Renglon> lista = new List<Renglon>();

                clsdshIndicadoresACyS.LlenaCDINac(3, sesion.Emp_Cnx, ref lista);

                this.lstchkFran.DataValueField = "idRng";
                this.lstchkFran.DataTextField = "sValor";
                this.lstchkFran.DataSource = lista; /// .OrderBy(i => i.sDescripcion);
                this.lstchkFran.DataBind();

                foreach (ListItem ch in lstchkFran.Items)
                {
                    ch.Selected = true;
                }

            }
            catch (Exception ex)
            {
                //throw ex;
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        #endregion

        #region LlenarDashboard

        void llenaDashboard()
        {
            try
            {
                List<DashboardACyS_CDIACyS> ListCDIs = new List<DashboardACyS_CDIACyS>(); 
                List<DashboardACyS_Resumen> ListResumen = new List<DashboardACyS_Resumen>();
                List<DashboardACyS_Estatus> ListEstatus = new List<DashboardACyS_Estatus>();
                List<DashboardACyS_RIKS> ListRIKSs = new List<DashboardACyS_RIKS>();
                
                List<DashboardACyS_DetalleRIKS> ListDetaleCDIs = new List<DashboardACyS_DetalleRIKS>();

                List<DashboardACyS_Clientes> ClientesCon = new List<DashboardACyS_Clientes>();
                List<DashboardACyS_Clientes> ClientesSin = new List<DashboardACyS_Clientes>();
                List<DashboardACyS_DetalleACyS> DetalleAcyS = new List<DashboardACyS_DetalleACyS>();

                List<DashboardACyS_Resumen> ListResumenTop10 = new List<DashboardACyS_Resumen>();

                /// Obtiene Datos
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_dshIndicadoresACyS clsdshIndicadoresACyS = new CN_dshIndicadoresACyS();
                int iCDI = (chkTodosCDIs.Checked ? -1 : 1);
                string siCDI = "";

                foreach (ListItem chk in lstchkCDIS.Items)
                {
                    if (chk.Selected)
                    {
                        siCDI = siCDI + chk.Value.ToString() + ",";
                    }
                }

                foreach (ListItem chk in lstchkCDC.Items)
                {
                    if (chk.Selected)
                    {
                        siCDI = siCDI + chk.Value.ToString() + ",";
                    }
                }

                foreach (ListItem chk in lstchkFran.Items)
                {
                    if (chk.Selected)
                    {
                        siCDI = siCDI + chk.Value.ToString() + ",";
                    }
                }

                string sEstatus = this.drpEstatus.SelectedItem.Value;

                clsdshIndicadoresACyS.ConsultaDashboardNacionalACyS(sesion.Id_Emp, sesion.Id_U, siCDI, sEstatus, sesion.Emp_Cnx,
                    ref ListCDIs,
                    ref ListResumen, ref ListEstatus, ref ListRIKSs, ref ListDetaleCDIs,
                    ref ClientesCon, ref ClientesSin, ref DetalleAcyS);

                // obtiene el top 10 de los CDIs
                ListResumenTop10 = ListResumen.Where(cc => cc.Ordern == 5).OrderBy(cc => cc.sValor).ToList();
                int iiicdi = 1;
                strLista = "";
                strResumen = "";

                strCDIS = "";
                strDatoCDIS = "";

                DashboardACyS_CDIACyS lCDI = new DashboardACyS_CDIACyS();
                DashboardACyS_Resumen lResumen = new DashboardACyS_Resumen();
                List<Renglon> listaGraphCDIs = new List<Renglon>();
                Renglon GrCDI = new Renglon();
                // Llena resumen y gaugers del top 10 de CDIs
                foreach (DashboardACyS_Resumen top10Res in ListResumenTop10)
                {
                    lCDI = ListCDIs.Where(aa => aa.Id_Cd == top10Res.IdCdi).FirstOrDefault();

                    strLista = strLista + "<li><a itlist='itList_" + top10Res.IdCdi.ToString() + "' href='#' " + (iiicdi == 1 ? "class='item-select-slid'" : "") + "></a></li>";

                    strMiddle = strMiddle + "<li " + (iiicdi == 1 ? "style='z-index:0; opacity:1;'" : "") + ">" +
                                "<div class='content_slider'><div>" +
                                "<table style='width: 100%; border-spacing:5px; column-width:5px'>" +
                                    "<tr><td colspan='2'><h5>CDI: " + lCDI.CDI + "</h5></td></tr><tr>";

                    strResumen = GeneraResumen(ListResumen.Where(cc => cc.IdCdi == top10Res.IdCdi).ToList());

                    strMiddle = strMiddle + strResumen;

                    strResumen = LlenaGauges(ListEstatus.Where(cc => cc.IdCdi == top10Res.IdCdi).ToList());

                    strMiddle = strMiddle + strResumen;

                    strMiddle = strMiddle + "</tr></table></div></div></li>";

                    // obtiene los datos para el top 10
                    lResumen = ListResumen.Where(cc => cc.IdCdi == top10Res.IdCdi).ToList().Where(vv => vv.Ordern == 1).FirstOrDefault();
                    GrCDI = new Renglon();
                    GrCDI.sValor = lResumen.sValor;
                    GrCDI.sDescripcion = lCDI.Id_Cd.ToString() + " " + lCDI.CDI.Trim();
                    listaGraphCDIs.Add(GrCDI);

                    if (iiicdi == 10)
                    { break; }
                    iiicdi++;
                }
                
                foreach(Renglon RR in listaGraphCDIs.OrderBy(cc => cc.sDescripcion).ToList())
                {
                    strDatoCDIS = strDatoCDIS + RR.sValor.Trim() + ", ";
                    strCDIS = strCDIS + " \"" + RR.sDescripcion  + "\", ";
                }

                BaseCDIs = new List<DashboardACyS_CDIACyS>();
                BaseCDIs = ListCDIs;

                BaseRIKSs = new List<DashboardACyS_RIKS>();
                BaseRIKSs = ListRIKSs;

                // guarada informacion para los exceles
                ExcelDeEstatus = new List<DashboardACyS_Estatus>();
                ExcelDeEstatus = ListEstatus;

                ExcelDeRiks = new List<DashboardACyS_DetalleRIKS>();
                ExcelDeRiks = ListDetaleCDIs;

                ExcelResumen = new List<DashboardACyS_Resumen>();
                ExcelResumen = ListResumen;

                ExcelClientesCon = new List<DashboardACyS_Clientes>();
                ExcelClientesCon = ClientesCon;

                ExcelClientesSin = new List<DashboardACyS_Clientes>();
                ExcelClientesSin = ClientesSin;

                ExcelDetalleACyS = new List<DashboardACyS_DetalleACyS>();
                ExcelDetalleACyS = DetalleAcyS;
            }
            catch (Exception ex)
            {
                this.ErrorManager(ex, "llenaDashboard");
            }
        }

        string GeneraResumen(List<DashboardACyS_Resumen> ListaResumen)
        {
            string cadsalida;

            cadsalida = "<td style='width:32%; vertical-align:top;  align-content:start; text-align:start; align-items:start; align-self:start;'>" +
                "<table class='table subcompact'>";

            foreach (DashboardACyS_Resumen rng in ListaResumen)
            {
                if (rng.Ordern == 1)
                {
                    cadsalida = cadsalida + "<tr><td><p class='text-secondary' style='align-content:end; text-align:end;'>Total General De Acuerdos Capturados/Actualizados:</p></td>" +
                        "<td><p class='text-secondary' style='align-content:end; align-self:end; align-items:end; text-align:end; vertical-align:top;'>" +
                        "&nbsp;<strong>" + rng.sValor + "</strong></p></td></tr>";
                }
                if (rng.Ordern == 2)
                {
                    cadsalida = cadsalida + "<tr><td><p class='text-secondary' style='align-content:end; text-align:end;'>Total De Clientes Del 80/20:</p></td>" +
                        "<td><p class='text-secondary' style='align-content:end; align-self:end; align-items:end; text-align:end; vertical-align:top;'>" +
                        "&nbsp;<strong>" + rng.sValor + "</strong></p></td></tr>";
                }
                if (rng.Ordern == 3)
                {
                    cadsalida = cadsalida + "<tr><td><p class='text-secondary' style='align-content:end; text-align:end;'>Total De Captura De ACyS Del 80/20:</p></td>" +
                        "<td><p class='text-secondary' style='align-content:end; align-self:end; align-items:end; text-align:end; vertical-align:top;'>" +
                        "&nbsp;<strong>" + rng.sValor + "</strong></p></td></tr>";
                }
                if (rng.Ordern == 4)
                {
                    cadsalida = cadsalida + "<tr><td><p class='text-secondary' style='align-content:end; text-align:end;'>Avances En Captura Del 80/20:</p></td>" +
                        "<td><p class='text-leader2' style='align-content:end; align-self:end; align-items:end; text-align:end; vertical-align:top;'>" +
                        "&nbsp;<strong>" + rng.sValor + "</strong></p></td></tr>";
                }
                if (rng.Ordern == 5)
                {
                    cadsalida = cadsalida + "<tr><td><p class='text-secondary' style='align-content:end; text-align:end;'>Total Capturado vs.Total Autorizaddo:</p></td>" +
                        "<td><p class='text-leader2' style='align-content:end; align-self:end; align-items:end; text-align:end; vertical-align:top;'>" +
                        "&nbsp;<strong>" + rng.sValor + "</strong></p></td></tr>";
                }
            }
            cadsalida = cadsalida + "</table></td>";
            return cadsalida;
        }

        string LlenaGauges(List<DashboardACyS_Estatus> ListaEstatus)
        {
            string strSalida2 = "";

            string NumCapturado = "0";
            string NumSolicitadoG = "0";
            string NumSolicitadoJ = "0";
            string NumAutorizado = "0";
            string NumOtro = "0";
            int Total = 0;

            string lblNumCapturado = "0";
            string lblNumSolicitadoG = "0";
            string lblNumSolicitadoJ = "0";
            string lblNumAutorizado = "0";
            string lblNumOtro = "0";
            string sCDI = "";

            // obtiene lo que se tenga de informacion
            foreach (DashboardACyS_Estatus rngg in ListaEstatus)
            {
                sCDI = rngg.IdCdi.ToString();
                Total = Total + rngg.NACySValor;
                /*
                 * 'Autorizado'
                 * 'Cancelado'
                 * 'Capturado'
                 * 'Solicitado
                */
                if (rngg.AcsEstatusTexto == "Autorizado")
                {
                    NumAutorizado = rngg.NACySValor.ToString();
                    lblNumAutorizado = rngg.NACySValor.ToString();
                }

                if ((rngg.AcsEstatusTexto == "Cancelado") || (rngg.AcsEstatusTexto == "Solicitado"))
                {
                    NumOtro = rngg.NACySValor.ToString();
                    lblNumOtro = rngg.NACySValor.ToString();
                }

                if (rngg.AcsEstatusTexto == "Capturado")
                {
                    NumCapturado = rngg.NACySValor.ToString();
                    lblNumCapturado = rngg.NACySValor.ToString();
                }

                if (rngg.AcsEstatusTexto == "Solicitado G")
                {
                    NumSolicitadoG = rngg.NACySValor.ToString();
                    lblNumSolicitadoG = rngg.NACySValor.ToString();
                }

                if (rngg.AcsEstatusTexto == "Solicitado J")
                {
                    NumSolicitadoJ = rngg.NACySValor.ToString();
                    lblNumSolicitadoJ = rngg.NACySValor.ToString();
                }
            }
            /// Calculo de cantidades, porcentajes y etiquetas
            NumCapturado = Math.Round((Convert.ToDouble(Convert.ToDouble(lblNumCapturado) / Convert.ToDouble(Total)) * 100), 0).ToString();
            NumSolicitadoG = Math.Round((Convert.ToDouble(Convert.ToDouble(lblNumSolicitadoG) / Convert.ToDouble(Total)) * 100), 0).ToString();
            NumSolicitadoJ = Math.Round((Convert.ToDouble(Convert.ToDouble(NumSolicitadoJ) / Convert.ToDouble(Total)) * 100), 0).ToString();
            NumAutorizado = Math.Round((Convert.ToDouble(Convert.ToDouble(lblNumAutorizado) / Convert.ToDouble(Total)) * 100), 0).ToString();
            NumOtro = Math.Round((Convert.ToDouble(Convert.ToDouble(lblNumOtro) / Convert.ToDouble(Total)) * 100), 0).ToString();

            lblNumAutorizado = lblNumAutorizado + "/ " + Total.ToString();
            lblNumOtro = lblNumOtro + "/ " + Total.ToString();
            lblNumCapturado = lblNumCapturado + "/ " + Total.ToString();
            lblNumSolicitadoJ = lblNumSolicitadoJ + "/ " + Total.ToString();
            lblNumSolicitadoG = lblNumSolicitadoG + "/ " + Total.ToString();

            // llena la cadena de salida
            strSalida2 = "<td style='width:52%; vertical-align:top; border-left:solid thin #009CD9'> " +
                "<table class='table subcompact'><tr><td style='width:25px;'>&nbsp;</td><td> " +
                "<table><tr style='width:15px'><td style='text-align:center; align-content:center; align-items:center;'> " +
                "<h6><small><strong>CAPTURADO</strong></small></h6> " +
                "<div style='text-align:center; align-content:center; align-items:center;' id='donCapturado" + sCDI + "' data-role='donut' data-value='" + NumCapturado + "' " +
                "data-hole='.6' data-stroke='#f5f5f5' data-fill='#009CD9' data-color='#FFFFFF' data-animate='25' ></div></td></tr>" +   /// 9C27B0
                "<tr><td style='text-align:center; align-content:center; align-items:center;'>" +
                "<h6><small>" + lblNumCapturado + "</small><br /><p style=' color:#9C27B0'>" + NumCapturado + "%</p></h6></td></tr></table></td>";
            
            strSalida2 = strSalida2 + "<td><table><tr style='width:15px'><td style='text-align:center; align-content:center; align-items:center;'>" +
                "<h6> <small><strong>SOLICITADO G</strong></small></h6>" +
                "<div style='text-align:center; align-content:center; align-items:center;' id='donSolicitaG" + sCDI + "' data-role='donut' data-value='" + NumSolicitadoG + "' " +
                "data-hole='.6' data-stroke='#f5f5f5' data-fill='#009CD9' data-color='#FFFFFF' data-animate='25'></div></td></tr>" +
                "<tr><td style='text-align:center; align-content:center; align-items:center;'>" +
                "<h6> <small>" + lblNumSolicitadoG + "</small><br /><p style=' color:#9C27B0'>" + NumSolicitadoG + "%</p></h6></td></tr></table></td>";
            strSalida2 = strSalida2 + "<td><table><tr style='width:15px'><td style='text-align:center; align-content:center; align-items:center;'>" +
                "<h6> <small><strong>SOLICITADO J</strong></small></h6>" +
                "<div style='text-align:center; align-content:center; align-items:center;' id='donSolicitadoJ" + sCDI + "' data-role='donut' data-value='" + NumSolicitadoJ + "' " +
                "data-hole='.6' data-stroke='#f5f5f5' data-fill='#009CD9' data-color='#FFFFFF' data-animate='25'></div></td></tr>" +
                "<tr><td style='text-align:center; align-content:center; align-items:center;'>" +
                "<h6> <small>" + lblNumSolicitadoJ + "</small><br /><p style=' color:#9C27B0'>" + NumSolicitadoJ + "%</p></h6></td></tr></table></td>";
            strSalida2 = strSalida2 + "<td><table><tr style='width:15px'><td style='text-align:center; align-content:center; align-items:center;'>" +
                "<h6> <small><strong>AUTORIZADO</strong></small></h6>" +
                "<div style='text-align:center; align-content:center; align-items:center;' id='donAutorizado" + sCDI + "' data-role='donut' data-value='" + NumAutorizado + "' " +
                "data-hole='.6' data-stroke='#f5f5f5' data-fill='#009CD9' data-color='#FFFFFF' data-animate='25' ></div></td></tr>" +
                "<tr><td style='text-align:center; align-content:center; align-items:center;'>" +
                "<h6> <small>" + lblNumAutorizado + "</small><br /><p style=' color:#9C27B0'>" + NumAutorizado + "%</p></h6></td></tr></table></td>";
            strSalida2 = strSalida2 + "<td><table><tr style='width:15px'><td style='text-align:center; align-content:center; align-items:center;'>" +
                "<h6> <small><strong>OTRO</strong></small></h6>" +
                "<div style='text-align:center; align-content:center; align-items:center;' id='donOtro" + sCDI + "' data-role='donut' data-value='" + NumOtro + "' " +
                "data-hole='.6' data-stroke='#f5f5f5' data-fill='#009CD9' data-color='#FFFFFF' data-animate='25' ></div></td></tr>" +
                "<tr><td style='text-align:center; align-content:center; align-items:center;'>" +
                "<h6> <small>" + lblNumOtro + "</small><br /><p style=' color:#9C27B0'>" + NumOtro + "%</p></h6></td></tr></table></td>";
            strSalida2 = strSalida2 + "<td style='width:5px;'>&nbsp;</td></tr></table></td>";

            return strSalida2;
        }

        /*
        void LlenaGraph(List<DashboardACyS_RIKS> ListaRIKs, List<DashboardACyS_Resumen> ListaResumen)
        {
            foreach (DashboardACyS_RIKS rik in ListaRIKs)
            {
                strDatoCDIS = strDatoCDIS + rik.NACySValor.ToString().Trim() + ", ";
                strCDIS = strCDIS + " \"" + rik.Rik_Nombre.Trim() + "\", ";
            }

        }
        */


        private void ExportaAExcel()
        {
            int i = 0;
            string filenombre = "DetalleACySxCDI_" + DateTime.UtcNow.ToShortDateString().Replace("/", "") + "h" + DateTime.UtcNow.ToShortTimeString().Replace(".", "").Replace(":", "").Replace(" ", "");
            try
            {
                DataSet ds = new DataSet();
                ds = ToDataSet(ExcelDeRiks);

                ds.Tables[i].TableName = "DetalleACySDelCDI";

                using (XLWorkbook wb = new XLWorkbook())
                {
                    foreach (DataTable dt in ds.Tables)
                    {
                        wb.Worksheets.Add(dt);
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
            }
            catch (Exception ex)
            {
                this.ErrorManager(ex, "ExportaAExcel");
                throw ex;
            }
            finally
            {
            }
        }

        public DataSet ToDataSet<DashboardACyS_DetalleRIKS>(List<DashboardACyS_DetalleRIKS> items)
        {
            DataTable dataTable = new DataTable(typeof(DashboardACyS_DetalleRIKS).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(DashboardACyS_DetalleRIKS).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (DashboardACyS_DetalleRIKS item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dataTable);
            return ds;
        }


        void AvanceDetallado(List<DashboardACyS_CDIACyS> ListaDeCDIS, 
            List<DashboardACyS_Resumen> ListaResumen,
            List<DashboardACyS_Clientes> ListaClientesCon, List<DashboardACyS_Clientes> ListaClientesSin,
            List<DashboardACyS_DetalleACyS> ListaDeDetalleACyS)
        {
            string filenombre = "ACySAvanceDetallado_" + DateTime.UtcNow.ToShortDateString().Replace("/", "") + "h" + DateTime.UtcNow.ToShortTimeString().Replace(".", "").Replace(":", "").Replace(" ", "");
            try
            {
                var wb = new XLWorkbook();

                //  var ws = wb.Worksheets.Add(wsName);
                //  FormatoAvanceDetallado(wsName, ListaResumen, ListaClientesCon, ListaClientesSin, ListaDeDetalleACyS, ref ws);
                
                foreach (DashboardACyS_CDIACyS ccCDI in ListaDeCDIS)
                {
                    string strCDI = ccCDI.Id_Cd.ToString() + "_" + ccCDI.CDI;
                    var ws = wb.Worksheets.Add(strCDI);
                    FormatoAvanceDetallado("CDI: " + strCDI, ListaResumen.Where(i => i.IdCdi == ccCDI.Id_Cd).ToList(),
                        ListaClientesCon.Where(i => i.IdCdi == ccCDI.Id_Cd).ToList(),
                        ListaClientesSin.Where(i => i.IdCdi == ccCDI.Id_Cd).ToList(),
                        ListaDeDetalleACyS.Where(i => i.IdCdi == ccCDI.Id_Cd).ToList(), ref ws);
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
                this.ErrorManager(ex, "AvanceDetallado");
                throw ex;
            }
        }

        void FormatoAvanceDetallado(string strCDI, List<DashboardACyS_Resumen> ListaResumen,
            List<DashboardACyS_Clientes> ListaClientesCon, List<DashboardACyS_Clientes> ListaClientesSin,
            List<DashboardACyS_DetalleACyS> ListaDeDetalleACyS, ref IXLWorksheet wss)
        {
            int i = 0;
            try
            {
                #region Resumen

                wss.Range("A1:C1").Merge();
                wss.Range("A1").SetValue(strCDI);
                wss.Cell("A1").Style.Font.Bold = true;

                wss.Range("I1:J1").Merge();
                wss.Range("I1").SetValue("Fecha Corte: " + DateTime.Now.ToString("dd/MM/yyyy"));

                wss.Range("B2:E2").Merge();
                foreach (DashboardACyS_Resumen rng in ListaResumen)
                {
                    wss.Range("B" + (i + 3) + ":D" + (i + 3)).Merge();
                    wss.Cell("B" + (i + 3)).Value = rng.Concepto.ToString();
                    wss.Cell("E" + (i + 3)).Value = rng.sValor.ToString();
                    i++;
                }

                // Formato
                wss.Range("E2:E" + (i + 2).ToString()).Style.Font.Bold = true;
                wss.Cell("E" + (i + 1)).Style.Font.FontSize = 14;
                /// wss.Cell("E" + (i + 1)).Style.NumberFormat.Format = "{0:p0}";
                wss.Cell("E" + (i + 1)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                wss.Cell("E" + (i + 2)).Style.Font.FontSize = 14;
                /// wss.Cell("E" + (i + 2)).Style.NumberFormat.Format = "{0:p0}";
                wss.Cell("E" + (i + 2)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                #endregion

                #region Clientes8020

                // si se incluye segun el checkbox
                if (this.chkLista8020.Checked)
                {
                    i++;    // brinco un renglon
                    wss.Range("A" + (i + 3).ToString() + ":J" + (i + 3).ToString()).Merge();
                    wss.Range("A" + (i + 3).ToString()).SetValue("Clientes que Conforman el 80/20");
                    wss.Range("A" + (i + 3).ToString()).Style.Font.Bold = true;
                    wss.Range("A" + (i + 3).ToString()).Style.Font.FontColor = XLColor.White;
                    wss.Range("A" + (i + 3).ToString()).Style.Fill.BackgroundColor = XLColor.UltramarineBlue;
                    wss.Range("A" + (i + 3).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    wss.Range("A" + (i + 3).ToString() + ":J" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    i++;
                    wss.Range("A" + (i + 3).ToString() + ":E" + (i + 3).ToString()).Merge();
                    wss.Range("A" + (i + 3).ToString()).SetValue("Clientes CON ACyS");
                    wss.Range("A" + (i + 3).ToString()).Style.Font.Bold = true;
                    wss.Range("A" + (i + 3).ToString()).Style.Font.FontColor = XLColor.White;
                    wss.Range("A" + (i + 3).ToString()).Style.Fill.BackgroundColor = XLColor.UltramarineBlue;
                    wss.Range("A" + (i + 3).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    wss.Range("A" + (i + 3).ToString() + ":E" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    wss.Range("F" + (i + 3).ToString() + ":J" + (i + 3).ToString()).Merge();
                    wss.Range("F" + (i + 3).ToString()).SetValue("Clientes SIN ACyS Autorizado/Vigente");
                    wss.Range("F" + (i + 3).ToString()).Style.Font.Bold = true;
                    wss.Range("F" + (i + 3).ToString()).Style.Font.FontColor = XLColor.White;
                    wss.Range("F" + (i + 3).ToString()).Style.Fill.BackgroundColor = XLColor.UltramarineBlue;
                    wss.Range("F" + (i + 3).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    wss.Range("F" + (i + 3).ToString() + ":J" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    i++;    // brinco un renglon
                    string celditA = "";
                    int letra = 1;
                    int iCoon = i;
                    int iSiin = i;

                    foreach (DashboardACyS_Clientes rngCte in ListaClientesCon)
                    {
                        ///     ciclo de 5 columnas
                        if (letra == 1) { celditA = "A"; }
                        if (letra == 2) { celditA = "B"; }
                        if (letra == 3) { celditA = "C"; }
                        if (letra == 4) { celditA = "D"; }
                        if (letra == 5) { celditA = "E"; letra = 0; }

                        wss.Cell(celditA + (iCoon + 3)).Value = rngCte.iIdCliente.ToString();
                        wss.Cell(celditA + (iCoon + 3)).Style.Fill.BackgroundColor = XLColor.CadmiumYellow;
                        wss.Cell(celditA + (iCoon + 3)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wss.Cell(celditA + (iCoon + 3)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        if (letra == 0) { iCoon++; }
                        letra++;
                    }

                    letra = 1; celditA = "";
                    foreach (DashboardACyS_Clientes rngCte2 in ListaClientesSin)
                    {
                        ///     ciclo de 5 columnas
                        if (letra == 1) { celditA = "F"; }
                        if (letra == 2) { celditA = "G"; }
                        if (letra == 3) { celditA = "H"; }
                        if (letra == 4) { celditA = "I"; }
                        if (letra == 5) { celditA = "J"; letra = 0; }

                        wss.Cell(celditA + (iSiin + 3)).Value = rngCte2.iIdCliente.ToString();
                        wss.Cell(celditA + (iSiin + 3)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wss.Cell(celditA + (iSiin + 3)).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        if (letra == 0) { iSiin++; }
                        letra++;
                    }

                    i = (iCoon >= iSiin) ? iCoon : iSiin;
                }

                i++;
                #endregion

                #region DetalleACyS

                // si se incluye segun el checkbox
                if (this.chkListaMatriz.Checked)
                {
                    i++;    // brinco un renglon
                    wss.Range("A" + (i + 3).ToString() + ":J" + (i + 3).ToString()).Merge();
                    wss.Range("A" + (i + 3).ToString()).SetValue("Matriz de ACyS vigentes total de clientes");
                    wss.Range("A" + (i + 3).ToString()).Style.Font.Bold = true;
                    wss.Range("A" + (i + 3).ToString()).Style.Font.FontColor = XLColor.White;
                    wss.Range("A" + (i + 3).ToString()).Style.Fill.BackgroundColor = XLColor.UltramarineBlue;
                    wss.Range("A" + (i + 3).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    wss.Range("A" + (i + 3).ToString() + ":J" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    i++;
                    wss.Cell("A" + (i + 3).ToString()).SetValue("Folio");
                    wss.Cell("A" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wss.Cell("B" + (i + 3).ToString()).SetValue("Estatus");
                    wss.Cell("B" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wss.Cell("C" + (i + 3).ToString()).SetValue("Num");
                    wss.Cell("C" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wss.Cell("D" + (i + 3).ToString()).SetValue("Cliente");
                    wss.Cell("D" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wss.Cell("E" + (i + 3).ToString()).SetValue("Terr.");
                    wss.Cell("E" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wss.Cell("F" + (i + 3).ToString()).SetValue("Rik");
                    wss.Cell("F" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wss.Cell("G" + (i + 3).ToString()).SetValue("Fecha");
                    wss.Cell("G" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wss.Cell("H" + (i + 3).ToString()).SetValue("Fecha Inicio");
                    wss.Cell("H" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wss.Cell("I" + (i + 3).ToString()).SetValue("Fecha Fin");
                    wss.Cell("I" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wss.Cell("J" + (i + 3).ToString()).SetValue("Vencido");
                    wss.Cell("J" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wss.Range("A" + (i + 3).ToString() + ":" + "J" + (i + 3).ToString()).Style.Font.Bold = true;
                    wss.Range("A" + (i + 3).ToString() + ":" + "J" + (i + 3).ToString()).Style.Font.Bold = true;
                    wss.Range("A" + (i + 3).ToString() + ":" + "J" + (i + 3).ToString()).Style.Font.FontColor = XLColor.White;
                    wss.Range("A" + (i + 3).ToString() + ":" + "J" + (i + 3).ToString()).Style.Fill.BackgroundColor = XLColor.UltramarineBlue;
                    wss.Range("A" + (i + 3).ToString() + ":" + "J" + (i + 3).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    i++;
                    //  Folio	Estatus	    NumCliente	Cliente     Territorio	RIK	FechaACyS	
                    //  FechaInicio	    FechaFin	TipoCuenta	Vencido
                    foreach (DashboardACyS_DetalleACyS Det in ListaDeDetalleACyS)
                    {
                        wss.Cell("A" + (i + 3).ToString()).SetValue(Det.Folio);
                        wss.Cell("A" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wss.Cell("B" + (i + 3).ToString()).SetValue(Det.Estatus);
                        wss.Cell("B" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wss.Cell("C" + (i + 3).ToString()).SetValue(Det.NumCliente);
                        wss.Cell("C" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wss.Cell("D" + (i + 3).ToString()).SetValue(Det.Cliente);
                        wss.Cell("D" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wss.Cell("E" + (i + 3).ToString()).SetValue(Det.Territorio);
                        wss.Cell("E" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wss.Cell("F" + (i + 3).ToString()).SetValue(Det.Rik);
                        wss.Cell("F" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wss.Cell("G" + (i + 3).ToString()).SetValue(Det.FechaACyS);
                        wss.Cell("G" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wss.Cell("H" + (i + 3).ToString()).SetValue(Det.FechaInicio);
                        wss.Cell("H" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wss.Cell("I" + (i + 3).ToString()).SetValue(Det.FechaFin);
                        wss.Cell("I" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wss.Cell("J" + (i + 3).ToString()).SetValue(Det.Vencido);
                        wss.Cell("J" + (i + 3).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        i++;
                    }

                }

                #endregion

                wss.Columns().AdjustToContents();
            }
            catch (Exception ex)
            {
                this.ErrorManager(ex, "FormatoAvanceDetallado");
                throw ex;
            }
        }


        void CumplimientoCaptura(string wsName,
            List<DashboardACyS_CDIACyS> ListaDeCDIS,
            List<DashboardACyS_RIKS> ListadoDeRIKS,
            List<DashboardACyS_Resumen> ListaResumen,
            List<DashboardACyS_Estatus> ListaEstatus, List<DashboardACyS_DetalleACyS> ListaDeDetalleACyS)
        {
            string filenombre = "ACySCumplimientodDeCaptura_" + DateTime.UtcNow.ToShortDateString().Replace("/", "") + "h" + DateTime.UtcNow.ToShortTimeString().Replace(".", "").Replace(":", "").Replace(" ", "");
            try
            {
                var wb = new XLWorkbook();

                var ws = wb.Worksheets.Add(wsName);
                FormatoCumplimientoDeCaptura(ListaDeCDIS, ListadoDeRIKS, ListaResumen, ListaEstatus, ListaDeDetalleACyS, ref ws);

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

        void FormatoCumplimientoDeCaptura(List<DashboardACyS_CDIACyS> ListaDeCDIS, List<DashboardACyS_RIKS> ListadoDeRIKS,
            List<DashboardACyS_Resumen> ListaResumen,
            List<DashboardACyS_Estatus> ListaEstatus, List<DashboardACyS_DetalleACyS> ListaDeDetalleACyS, ref IXLWorksheet wss)
        {
            int i = 0;
            DashboardACyS_RIKS RIIKK = new DashboardACyS_RIKS();
            XLColor xlColorFondo;

            try
            {
                #region Reporte

                wss.Range("A2:J2").Merge();
                wss.Range("A2").SetValue("Reporte de Cumplimiento en Captura de ACyS");
                wss.Range("A2:J2").Style.Font.Bold = true;
                wss.Range("A2:J2").Style.Font.FontSize = 14;
                wss.Range("A2:J2").Style.Font.FontColor = XLColor.White;
                wss.Range("A2:J2").Style.Fill.BackgroundColor = XLColor.LightSlateGray;

                wss.Range("A3:J3").Merge();
                wss.Range("A3").SetValue("al corte: " + DateTime.Now.ToString("dd/MM/yyyy"));
                wss.Range("A3:J3").Style.Fill.BackgroundColor = XLColor.LightSlateGray;

                //  if (this.chkDesgloseEstatus.Checked)
                {
                    /// Columna de estatus
                    wss.Range("B5:F5").Merge();
                    wss.Range("B5").SetValue("Estatus");
                    wss.Range("B5:F5").Style.Font.Bold = true;
                    wss.Range("B5:F5").Style.Fill.BackgroundColor = XLColor.CadetBlue;
                    wss.Range("B5:F5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    wss.Range("B5:F5").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                }
                /// Columnas del listado
                //  Sucursal Capturado   Solicitado G    Solicitado J    Otro Autorizado  
                //  Total de Captura Total Capturado vs Autorizado Avance de Captura de 80/20
                wss.Cell("A6").SetValue("Sucursal");
                wss.Cell("A6").Style.Font.Bold = true;
                wss.Cell("A6").Style.Font.FontColor = XLColor.White;
                wss.Cell("A6").Style.Fill.BackgroundColor = XLColor.UltramarineBlue;
                wss.Cell("A6").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wss.Cell("A6").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                /// if (this.chkDesgloseEstatus.Checked)
                {
                    wss.Cell("B6").SetValue("Capturado");
                    wss.Cell("B6").Style.Font.Bold = true;
                    ///wss.Cell("B6").Style.Font.FontColor = XLColor.White;
                    wss.Cell("B6").Style.Fill.BackgroundColor = XLColor.CadetBlue;
                    wss.Cell("B6").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    wss.Cell("B6").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    wss.Cell("C6").SetValue("Solicitado G");
                    wss.Cell("C6").Style.Font.Bold = true;
                    ///wss.Cell("C6").Style.Font.FontColor = XLColor.White;
                    wss.Cell("C6").Style.Fill.BackgroundColor = XLColor.CadetBlue;
                    wss.Cell("C6").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    wss.Cell("C6").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    wss.Cell("D6").SetValue("Solicitado J");
                    wss.Cell("D6").Style.Font.Bold = true;
                    ///wss.Cell("D6").Style.Font.FontColor = XLColor.White;
                    wss.Cell("D6").Style.Fill.BackgroundColor = XLColor.CadetBlue;
                    wss.Cell("D6").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    wss.Cell("D6").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    wss.Cell("E6").SetValue("Otro");
                    wss.Cell("E6").Style.Font.Bold = true;
                    ///wss.Cell("E6").Style.Font.FontColor = XLColor.White;
                    wss.Cell("E6").Style.Fill.BackgroundColor = XLColor.CadetBlue;
                    wss.Cell("E6").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    wss.Cell("E6").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    wss.Cell("F6").SetValue("Autorizado");
                    wss.Cell("F6").Style.Font.Bold = true;
                    ///wss.Cell("F6").Style.Font.FontColor = XLColor.White;
                    wss.Cell("F6").Style.Fill.BackgroundColor = XLColor.CadetBlue;
                    wss.Cell("F6").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    wss.Cell("F6").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                }

                wss.Cell("G6").SetValue("Total de Captura");
                wss.Cell("G6").Style.Font.Bold = true;
                wss.Cell("G6").Style.Font.FontColor = XLColor.White;
                wss.Cell("G6").Style.Fill.BackgroundColor = XLColor.UltramarineBlue;
                wss.Cell("G6").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wss.Cell("G6").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                wss.Cell("H6").SetValue("Total Capturado vs Autorizado");
                wss.Cell("H6").Style.Font.Bold = true;
                //  wss.Cell("H6").Style.Font.FontColor = XLColor.White;
                wss.Cell("H6").Style.Fill.BackgroundColor = XLColor.CadmiumYellow;
                wss.Cell("H6").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wss.Cell("H6").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                wss.Cell("I6").SetValue("Avance de Captura de 80/20");
                wss.Cell("I6").Style.Font.Bold = true;
                wss.Cell("I6").Style.Font.FontColor = XLColor.White;
                wss.Cell("I6").Style.Fill.BackgroundColor = XLColor.UltramarineBlue;
                wss.Cell("I6").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wss.Cell("I6").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                foreach (DashboardACyS_CDIACyS esteCDI in ListaDeCDIS)
                {
                    i++;
                    wss.Cell("A" + (i + 6)).SetValue("CDI " + esteCDI.Id_Cd.ToString() + " " + esteCDI.CDI);
                    wss.Cell("A" + (i + 6)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    int Total = 0;
                    int Autorizados = 0;

                    int iiCaptura = 0;
                    int iiSolicitadoG = 0;
                    int iiSolicitadoJ = 0;
                    int iiAutorizado = 0;
                    int iiOtro = 0;
                    string strRIK = "";
                    double valorRedondeado = 0;

                    foreach (DashboardACyS_Estatus rng in ListaEstatus.Where(icdi => icdi.IdCdi == esteCDI.Id_Cd).ToList())
                    {
                        if (rng.AcsEstatusTexto == "Capturado") { iiCaptura = rng.NACySValor; }
                        if (rng.AcsEstatusTexto == "Solicitado G") { iiSolicitadoG = rng.NACySValor; }
                        if (rng.AcsEstatusTexto == "Solicitado J") { iiSolicitadoJ = rng.NACySValor; }
                        if ((rng.AcsEstatusTexto == "Cancelado") || (rng.AcsEstatusTexto == "Solicitado")) { iiOtro = rng.NACySValor; }
                        if (rng.AcsEstatusTexto == "Autorizado") { iiAutorizado = rng.NACySValor; Autorizados = rng.NACySValor; }

                        //  iiCaptura = ListaDeDetalleACyS.Where(ri => ri.Rik == riik && ri.Estatus == "Capturado").Count();
                        //  iiSolicitadoG = ListaDeDetalleACyS.Where(ri => ri.Rik == riik && ri.Estatus == "Solicitado G").Count();
                        //  iiSolicitadoJ = ListaDeDetalleACyS.Where(ri => ri.Rik == riik && ri.Estatus == "Solicitado J").Count();
                        //  iiAutorizado = ListaDeDetalleACyS.Where(ri => ri.Rik == riik && ri.Estatus == "Autorizado").Count();
                        //  iiOtro = ListaDeDetalleACyS.Where(ri => ri.Rik == riik && (ri.Estatus == "Cancelado" || ri.Estatus == "Solicitado")).Count();

                        Total = Total + rng.NACySValor;

                        //wss.Cell(celditA + (i + 6).ToString()).Value = rng.NACySValor.ToString();
                        //wss.Cell(celditA + (i + 6).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        //wss.Cell(celditA + (i + 6).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    }
                    wss.Cell("B" + (i + 6).ToString()).Value = iiCaptura.ToString();
                    wss.Cell("B" + (i + 6).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wss.Cell("B" + (i + 6).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    wss.Cell("C" + (i + 6).ToString()).Value = iiSolicitadoG.ToString();
                    wss.Cell("C" + (i + 6).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wss.Cell("C" + (i + 6).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    wss.Cell("D" + (i + 6).ToString()).Value = iiSolicitadoJ.ToString();
                    wss.Cell("D" + (i + 6).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wss.Cell("D" + (i + 6).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    wss.Cell("E" + (i + 6).ToString()).Value = iiOtro.ToString();
                    wss.Cell("E" + (i + 6).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wss.Cell("E" + (i + 6).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    wss.Cell("F" + (i + 6).ToString()).Value = iiAutorizado.ToString();
                    wss.Cell("F" + (i + 6).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wss.Cell("F" + (i + 6).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    wss.Cell("G" + (i + 6).ToString()).Value = Total.ToString();
                    wss.Cell("G" + (i + 6).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wss.Cell("G" + (i + 6).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    if (Total == 0)
                    {
                        wss.Range("A" + (i + 6).ToString() + ":I" + (i + 6).ToString()).Style.Font.FontColor = XLColor.White;
                        wss.Range("A" + (i + 6).ToString() + ":I" + (i + 6).ToString()).Style.Font.Bold = true;
                        wss.Range("A" + (i + 6).ToString() + ":I" + (i + 6).ToString()).Style.Fill.BackgroundColor = XLColor.Red;
                        wss.Cell("H" + (i + 6).ToString()).SetValue("0%");
                        wss.Cell("H" + (i + 6).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wss.Cell("I" + (i + 6).ToString()).SetValue("0%");
                        wss.Cell("I" + (i + 6).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    }
                    valorRedondeado = 0;
                    valorRedondeado = Math.Round((Convert.ToDouble(Convert.ToDouble(Autorizados) / Convert.ToDouble(Total)) * 100), 0);
                    wss.Cell("H" + (i + 6).ToString()).SetValue(valorRedondeado.ToString() + "%");
                    
                    xlColorFondo = XLColor.White;
                    if (valorRedondeado <= 70)
                    {
                        xlColorFondo = XLColor.CadmiumRed;
                    }
                    else
                    {
                        if (valorRedondeado >= 91)
                        {
                            xlColorFondo = XLColor.FromArgb(0,176,80);        /// HookersGreen
                        }
                        else
                        {
                            xlColorFondo = XLColor.FromArgb(255, 128, 0);
                        }
                    }

                    wss.Cell("H" + (i + 6).ToString()).Style.Fill.BackgroundColor = xlColorFondo;
                    wss.Cell("H" + (i + 6).ToString()).Style.Font.FontColor = XLColor.White;
                    wss.Cell("H" + (i + 6).ToString()).Style.Font.Bold = true;
                    wss.Cell("H" + (i + 6).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    wss.Cell("H" + (i + 6).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    DashboardACyS_Resumen TotalVs = new DashboardACyS_Resumen();
                    TotalVs = ListaResumen.Where(a => a.IdCdi == esteCDI.Id_Cd && a.Ordern == 5).FirstOrDefault();

                    wss.Cell("I" + (i + 6).ToString()).SetValue(TotalVs.sValor);
                    wss.Cell("I" + (i + 6).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    wss.Cell("I" + (i + 6).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    // si lo pide, se desglosa la informacion por RIK
                    if (chkDetalleXRIKCumplimiento.Checked)
                    {
                        iiCaptura = 0;
                        iiSolicitadoG = 0;
                        iiSolicitadoJ = 0;
                        iiAutorizado = 0;
                        iiOtro = 0;
                        strRIK = "";
                        /// cada uno de los RIKS
                        foreach (int riik in ListaDeDetalleACyS.Where(rr => rr.IdCdi == esteCDI.Id_Cd).Select(rr => rr.Rik).Distinct().ToList())
                        {
                            i++;
                            iiCaptura = ListaDeDetalleACyS.Where(ri => ri.Rik == riik && ri.Estatus == "Capturado").Count();
                            iiSolicitadoG = ListaDeDetalleACyS.Where(ri => ri.Rik == riik && ri.Estatus == "Solicitado G").Count();
                            iiSolicitadoJ = ListaDeDetalleACyS.Where(ri => ri.Rik == riik && ri.Estatus == "Solicitado J").Count();
                            iiAutorizado = ListaDeDetalleACyS.Where(ri => ri.Rik == riik && ri.Estatus == "Autorizado").Count();
                            iiOtro = ListaDeDetalleACyS.Where(ri => ri.Rik == riik && (ri.Estatus == "Cancelado" || ri.Estatus == "Solicitado")).Count();

                            Total = ListaDeDetalleACyS.Where(ri => ri.Rik == riik).Count();

                            RIIKK = new DashboardACyS_RIKS();
                            strRIK = "";
                            RIIKK = ListadoDeRIKS.Where(ri => ri.IdRik == riik && ri.IdCdi == esteCDI.Id_Cd).FirstOrDefault();
                            if (RIIKK != null)
                            {
                                strRIK = RIIKK.Rik_Nombre;
                            }
                            
                            wss.Cell("A" + (i + 6).ToString()).Value = "RIK " + strRIK;
                            wss.Cell("A" + (i + 6).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            wss.Cell("A" + (i + 6).ToString()).Style.Alignment.Indent = 2;
                            wss.Cell("A" + (i + 6).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                            wss.Cell("B" + (i + 6).ToString()).Value = iiCaptura.ToString();
                            wss.Cell("B" + (i + 6).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            wss.Cell("B" + (i + 6).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                            wss.Cell("C" + (i + 6).ToString()).Value = iiSolicitadoG.ToString();
                            wss.Cell("C" + (i + 6).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            wss.Cell("C" + (i + 6).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                            wss.Cell("D" + (i + 6).ToString()).Value = iiSolicitadoJ.ToString();
                            wss.Cell("D" + (i + 6).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            wss.Cell("D" + (i + 6).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                            wss.Cell("E" + (i + 6).ToString()).Value = iiOtro.ToString();
                            wss.Cell("E" + (i + 6).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            wss.Cell("E" + (i + 6).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                            wss.Cell("F" + (i + 6).ToString()).Value = iiAutorizado.ToString();
                            wss.Cell("F" + (i + 6).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            wss.Cell("F" + (i + 6).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                            wss.Cell("G" + (i + 6).ToString()).Value = Total.ToString();
                            wss.Cell("G" + (i + 6).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            wss.Cell("G" + (i + 6).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


                            /// wss.Cell("H" + (i + 6).ToString()).SetValue(Math.Round((Convert.ToDouble(Convert.ToDouble(iiAutorizado) / Convert.ToDouble(Total)) * 100), 0).ToString() + "%");
                            valorRedondeado = 0;
                            valorRedondeado = Math.Round((Convert.ToDouble(Convert.ToDouble(iiAutorizado) / Convert.ToDouble(Total)) * 100), 0);
                            wss.Cell("H" + (i + 6).ToString()).SetValue(valorRedondeado.ToString() + "%");

                            xlColorFondo = XLColor.White;
                            if (valorRedondeado <= 70)
                            {
                                xlColorFondo = XLColor.CadmiumRed;
                            }
                            else
                            {
                                if (valorRedondeado >= 91)
                                {
                                    xlColorFondo = XLColor.FromArgb(0,176,80);    /// CaribbeanGreen
                                }
                                else
                                {
                                    xlColorFondo = XLColor.FromArgb(255, 128, 0);
                                }
                            }

                            wss.Cell("H" + (i + 6).ToString()).Style.Fill.BackgroundColor = xlColorFondo;
                            wss.Cell("H" + (i + 6).ToString()).Style.Font.FontColor = XLColor.White;
                            wss.Cell("H" + (i + 6).ToString()).Style.Font.Bold = true;
                            
                            wss.Cell("H" + (i + 6).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            wss.Cell("H" + (i + 6).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                            /*
                            TotalVs = new DashboardACyS_Resumen();
                            TotalVs = ListaResumen.Where(a => a.IdCdi == esteCDI.Id_Cd && a.Ordern == 5).FirstOrDefault();

                            wss.Cell("I" + (i + 6).ToString()).SetValue(TotalVs.sValor);
                            wss.Cell("I" + (i + 6).ToString()).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            wss.Cell("I" + (i + 6).ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            */
                        }
                    }
                }

                if (this.chkDesgloseEstatus.Checked == false)
                {
                    //  wss.Columns(2, 6).Hide();
                    wss.Columns(2, 6).Delete();
                }

                #endregion

                wss.Columns().AdjustToContents();
            }
            catch (Exception ex)
            {
                this.ErrorManager(ex, "FormatoCumplimientoDeCaptura");
                throw ex;
            }
        }


        void llenaDatosPorRik( ref string pstrCDI)
        {
            try
            {
                List<DashboardACyS_CDIACyS> ListCDIs = new List<DashboardACyS_CDIACyS>();
                List<DashboardACyS_Resumen> ListResumen = new List<DashboardACyS_Resumen>();
                List<DashboardACyS_RIKS> ListbaseCliente = new List<DashboardACyS_RIKS>();
                List<DashboardACyS_Clientes> ClientesCon = new List<DashboardACyS_Clientes>();
                List<DashboardACyS_Clientes> ClientesSin = new List<DashboardACyS_Clientes>();
                List<DashboardACyS_DetalleACyS> DetalleAcyS = new List<DashboardACyS_DetalleACyS>();

                /// Obtiene Datos
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_dshIndicadoresACyS clsdshIndicadoresACyS = new CN_dshIndicadoresACyS();
                string siCDI = "";
                string strCDI = "";
                foreach (ListItem chk in lstchkCDIS.Items)
                {
                    if (chk.Selected)
                    {
                        siCDI= siCDI + chk.Value.ToString() + ",";
                        strCDI = chk.Text;
                    }
                }

                pstrCDI = strCDI;
                string sEstatus = this.drpEstatus.SelectedItem.Value;

                clsdshIndicadoresACyS.ConsultaDashboardNacionalPorRIKACyS(sesion.Id_Emp, sesion.Id_U, siCDI, sEstatus, sesion.Emp_Cnx,
                    ref ListCDIs, 
                    ref ListResumen, ref ListbaseCliente,
                    ref ClientesCon, ref ClientesSin, ref DetalleAcyS);

                ExcelResumen = new List<DashboardACyS_Resumen>();
                ExcelResumen = ListResumen;

                ExcelBaseDeRiks = new List<DashboardACyS_RIKS>();
                ExcelBaseDeRiks = ListbaseCliente;

                ExcelClientesCon = new List<DashboardACyS_Clientes>();
                ExcelClientesCon = ClientesCon;

                ExcelClientesSin = new List<DashboardACyS_Clientes>();
                ExcelClientesSin = ClientesSin;

                ExcelDetalleACyS = new List<DashboardACyS_DetalleACyS>();
                ExcelDetalleACyS = DetalleAcyS;
            }
            catch (Exception ex)
            {
                this.ErrorManager(ex, "llenaDatosPorRik");
            }
        }

        void AvanceDetalladoPorRIKS(string pstrCDI, List<DashboardACyS_Resumen> ListaResumen,
            List<DashboardACyS_RIKS> ListaDeRIKS,
            List<DashboardACyS_Clientes> ListaClientesCon, List<DashboardACyS_Clientes> ListaClientesSin,
            List<DashboardACyS_DetalleACyS> ListaDeDetalleACyS)
        {
            string filenombre = "ACySAvanceDetalladoPorRIKS_" + DateTime.UtcNow.ToShortDateString().Replace("/", "") + "h" + DateTime.UtcNow.ToShortTimeString().Replace(".", "").Replace(":", "").Replace(" ", "");
            try
            {
                var wb = new XLWorkbook();

                var ws = wb.Worksheets.Add("CDI_" + pstrCDI);
                FormatoAvanceDetallado(pstrCDI, ListaResumen.Where(i => i.IdRik == 0).ToList(), ListaClientesCon, ListaClientesSin, ListaDeDetalleACyS, ref ws);

                foreach (DashboardACyS_RIKS rik in ListaDeRIKS)
                {
                    ws = wb.Worksheets.Add("RIK_" + rik.IdRik);
                    FormatoAvanceDetallado(rik.Rik_Nombre, ListaResumen.Where(i => i.IdRik == rik.IdRik).ToList(),
                        ListaClientesCon.Where(i => i.IdRik == rik.IdRik).ToList(),
                        ListaClientesSin.Where(i => i.IdRik == rik.IdRik).ToList(),
                        ListaDeDetalleACyS.Where(i => i.Rik == rik.IdRik).ToList(), ref ws);
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
                this.ErrorManager(ex, "AvanceDetallado");
                throw ex;
            }
        }



        #endregion

        #region Eventos

        protected void btnActualiza_Click(object sender, EventArgs e)
        {
            llenaDashboard();
        }

        protected void btnAvance_Click(object sender, EventArgs e)
        {
            if (this.chkDetalleXRIK.Checked == false)
            {
                llenaDashboard();
                AvanceDetallado(BaseCDIs, ExcelResumen, ExcelClientesCon, ExcelClientesSin, ExcelDetalleACyS);
            }
            else
            {
                string strCDI = "";
                llenaDashboard();
                llenaDatosPorRik(ref strCDI);
                AvanceDetalladoPorRIKS(strCDI, ExcelResumen, ExcelBaseDeRiks, ExcelClientesCon, ExcelClientesSin, ExcelDetalleACyS);
            }
                
        }

        protected void imgRptAvance_Click(object sender, ImageClickEventArgs e)
        {
            llenaDashboard();
            AvanceDetallado(BaseCDIs, ExcelResumen, ExcelClientesCon, ExcelClientesSin, ExcelDetalleACyS);
        }

        protected void btnCumplimiento_Click(object sender, EventArgs e)
        {
            llenaDashboard();
            CumplimientoCaptura("Cumplimiento", BaseCDIs, BaseRIKSs, ExcelResumen, ExcelDeEstatus, ExcelDetalleACyS);
        }

        protected void imgRptCumplimiento_Click(object sender, ImageClickEventArgs e)
        {
            llenaDashboard();
            CumplimientoCaptura("Cumplimiento", BaseCDIs, BaseRIKSs, ExcelResumen, ExcelDeEstatus, ExcelDetalleACyS);
        }


        protected void btnExcelGrafica_Click(object sender, ImageClickEventArgs e)
        {
            llenaDashboard();
            if (ExcelDeRiks != null)
            {
                if (ExcelDeRiks.Count != 0)
                {
                    ExportaAExcel();
                }
            }
        }

        protected void drpEstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListItem iitem = this.drpEstatus.SelectedItem;
            if (iitem != null)
            {
                llenaDashboard();
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

    }
}