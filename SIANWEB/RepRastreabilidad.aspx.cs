using CapaEntidad;
using CapaNegocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DevExpress.XtraPivotGrid;

using DevExpress.XtraPrinting;
using DevExpress.Web;
using DevExpress.Export;
using DevExpress.Utils;
using System.Data.SqlClient;
using ClosedXML.Excel;
using System.Data;
using System.IO;

namespace SIANWEB
{
    public partial class RepRastreabilidad : System.Web.UI.Page
    {
        public string strTabla = "";
        public class BRenglon
        {
            /// IdCte	NomCte	Id_Ped	FechaRemision	CantidadRemision	MontoRemision	Fecha Factura	Cantidad Factura	Monto Factura	Fecha Cobranza	Cantidad Cobranza	Monto Cobranza

            public int IdCte { get; set; }
            public string NomCte { get; set; }
            public int Id_Ped { get; set; }
            public string FechaPedido { get; set; }
            public string FechaRemision { get; set; }
            public string CantidadRemision { get; set; }
            public string MontoRemision { get; set; }
            public string CantidadFactura { get; set; }
            public string FechaFactura { get; set; }
            public string MontoFactura { get; set; }
            public string FechaCobranza { get; set; }
            public string CantidadCobranza { get; set; }
            public string MontoCobranza { get; set; }

        }


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

                        /// string Seeeleeeec = "";
                        /// GeneraCadena(ref Seeeleeeec);
                        /// strSelectoComando = Seeeleeeec;

                        this.CargaTabla();
                        //  this.PivotDataSource.SelectCommandType = SqlDataSourceCommandType.Text;
                        //  this.PivotDataSource.SelectCommand = Seeeleeeec;

                    }
                    else
                    {

                        //  this.CargaTabla();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
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

        protected void CmbCentro_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
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

        private void CargaTabla()
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
            int filtro = Convert.ToInt32(cmbTipoDocto.SelectedValue);

            FechaIstr = FechaI.ToShortDateString();
            FechaFstr = FechaF.ToShortDateString();
            BRenglon rnng = new BRenglon();
            try
            {
                BRenglon rng = new BRenglon();
                List<BRenglon> Tabla = new List<BRenglon>();
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

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRptRastreabilidad", ref dr, Parametros, Valores);
                while (dr.Read())
                {

                    rng = new BRenglon();
                    rng.IdCte = int.Parse(Convert.ToString(dr.GetValue(dr.GetOrdinal("IdCte")))) ;
                    rng.NomCte = Convert.ToString(dr.GetValue(dr.GetOrdinal("NomCte")));
                    rng.Id_Ped = int.Parse(Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_Ped")))) ;
                    rng.FechaPedido = !string.IsNullOrEmpty(Convert.ToString(dr.GetValue(dr.GetOrdinal("Ped_Fecha")))) ? Convert.ToDateTime(Convert.ToString(dr.GetValue(dr.GetOrdinal("Ped_Fecha")))).ToShortDateString() : "&nbsp;";
                    /// .ToShortDateString() 
                    ///Convert.ToDateTime(Convert.ToString(dr.GetValue(dr.GetOrdinal("FechaFactura")))).ToShortDateString()

                    rng.FechaRemision = !string.IsNullOrEmpty(Convert.ToString(dr.GetValue(dr.GetOrdinal("FechaRemision")))) ? Convert.ToDateTime( Convert.ToString(dr.GetValue(dr.GetOrdinal("FechaRemision")))).ToShortDateString() : "&nbsp;";
                    rng.CantidadRemision = !string.IsNullOrEmpty(Convert.ToString(dr.GetValue(dr.GetOrdinal("CantidadRemision")))) ? Convert.ToString(dr.GetValue(dr.GetOrdinal("CantidadRemision"))) : "&nbsp;";
                    rng.MontoRemision = !string.IsNullOrEmpty(Convert.ToString(Convert.ToString(dr.GetValue(dr.GetOrdinal("MontoRemision")))) ) ? Convert.ToString(dr.GetValue(dr.GetOrdinal("MontoRemision"))) : "&nbsp;";

                    rng.FechaFactura = !string.IsNullOrEmpty(Convert.ToString(dr.GetValue(dr.GetOrdinal("FechaFactura")))) ? Convert.ToDateTime( Convert.ToString(dr.GetValue(dr.GetOrdinal("FechaFactura")))).ToShortDateString() : "&nbsp;";
                    rng.CantidadFactura = !string.IsNullOrEmpty(Convert.ToString(dr.GetValue(dr.GetOrdinal("CantidadFactura")))) ? Convert.ToString(dr.GetValue(dr.GetOrdinal("CantidadFactura"))) : "&nbsp;";
                    rng.MontoFactura = !string.IsNullOrEmpty(Convert.ToString(Convert.ToString(dr.GetValue(dr.GetOrdinal("MontoFactura"))))) ? Convert.ToString(dr.GetValue(dr.GetOrdinal("MontoFactura"))) : "&nbsp;";

                    rng.FechaCobranza = !string.IsNullOrEmpty(Convert.ToString(dr.GetValue(dr.GetOrdinal("FechaCobranza")))) ? Convert.ToDateTime( Convert.ToString(dr.GetValue(dr.GetOrdinal("FechaCobranza")))).ToShortDateString() : "&nbsp;";
                    rng.CantidadCobranza = !string.IsNullOrEmpty(Convert.ToString(dr.GetValue(dr.GetOrdinal("CantidadCobranza")))) ? Convert.ToString(dr.GetValue(dr.GetOrdinal("CantidadCobranza"))) : "&nbsp;";
                    rng.MontoCobranza = !string.IsNullOrEmpty(Convert.ToString(Convert.ToString(dr.GetValue(dr.GetOrdinal("MontoCobranza"))))) ? Convert.ToString(dr.GetValue(dr.GetOrdinal("MontoCobranza"))) : "&nbsp;";

                    Tabla.Add(rng);
                }



                if (Tabla != null)
                {
                    strTabla = "";
                    btnExportar.Enabled = true;
                    lblMensaje.Text = "Se encontraron " + Tabla.Count.ToString() + " pedidos.";
                    foreach (BRenglon R in Tabla)
                    {
                        rnng = new BRenglon();
                        rnng = R;
                        strTabla = strTabla + "<tr>" +
                                    "<td> " + R.IdCte.ToString() + " </td>" +
                                    "<td nowrap>" + R.NomCte + "&nbsp;</td>" +
                                    "<td align='center'>&nbsp;" + R.Id_Ped.ToString() + "&nbsp;</td>" +
                                    "<td align='center'>&nbsp;" + R.FechaPedido + "&nbsp;</td>";
                        strTabla = strTabla + "<td align='center'>&nbsp;" + R.FechaRemision + "&nbsp;</td>" +
                                    "<td align='center'>&nbsp;" + R.CantidadRemision + "&nbsp;</td>" +
                                    "<td align='right'>&nbsp;" + ( R.MontoRemision == "&nbsp;" ? "&nbsp;" : string.Format("{0:C}", Convert.ToDecimal(R.MontoRemision) ) ) + "&nbsp;</td>";
                        strTabla = strTabla + "<td align='center'>&nbsp;" + R.FechaFactura + "&nbsp;</td>" +
                                    "<td align='center'>&nbsp;" + R.CantidadFactura + "&nbsp;</td>" +
                                    "<td align='right'>&nbsp;" + ( R.MontoFactura == "&nbsp;" ? "&nbsp;" : string.Format("{0:C}", Convert.ToDecimal(R.MontoFactura) ) ) + "&nbsp;</td>";
                        strTabla = strTabla + "<td align='center'>&nbsp;" + R.FechaCobranza + "&nbsp;</td>" +
                                    "<td align='center'>&nbsp;" + R.CantidadCobranza+ "&nbsp;</td>" +
                                    "<td align='right'>&nbsp;" + ( R.MontoCobranza == "&nbsp;" ? "&nbsp;" : string.Format("{0:C}", Convert.ToDecimal(R.MontoCobranza) ) ) + "&nbsp;</td>" +
                                    "</tr>";
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        private void ExportaAExcel()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (Sesion == null)
            {
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                Response.Redirect("login.aspx", false);
            }

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
            int filtro = Convert.ToInt32(cmbTipoDocto.SelectedValue);

            FechaIstr = FechaI.ToShortDateString();
            FechaFstr = FechaF.ToShortDateString();
            string filenombre = "RptRastreabilidad_" + DateTime.UtcNow.ToShortDateString().Replace("/", "") + "h" + DateTime.UtcNow.ToShortTimeString().Replace(".", "").Replace(":", "").Replace(" ", "");
            try
            {
                BRenglon rng = new BRenglon();
                List<BRenglon> Tabla = new List<BRenglon>();
                System.Data.DataSet dt = null;
                SqlDataAdapter sda = new SqlDataAdapter();

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

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRptRastreabilidad", ref dt, Parametros, Valores);

                        //  sda.Fill(dt);

                        dt.Tables[0].TableName = "ReporteRastreabilidad";

                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            foreach (DataTable dta in dt.Tables)
                            {
                                wb.Worksheets.Add(dta);
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
                throw ex;
            }

        }



        protected void btnExportar_Click(object sender, EventArgs e)
        {
            ExportaAExcel();
        }

        protected void btnActualiza_Click(object sender, EventArgs e)
        {
            CargaTabla();
        }




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