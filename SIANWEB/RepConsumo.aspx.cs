using CapaEntidad;
using CapaNegocios;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIANWEB
{
    public partial class RepConsumo : System.Web.UI.Page
    {
        public string strTabla = "";

        public class BRenglon
        {
            //  Id_Cte  Docto IdDocto Cte_NomComercial FechaDocto  Id_Prd Prd_Descripcion Prd_Presentacion Cantidad    Costo Monto

            public int Id_Cte { get; set; }
            public string Docto { get; set; }
            public int IdDocto { get; set; }
            public string Cte_NomComercial { get; set; }
            public string FechaDocto { get; set; }
            public string Id_Prd { get; set; }
            public string Prd_Descripcion { get; set; }
            public string Prd_Presentacion { get; set; }
            public int Cantidad { get; set; }
            public string Costo { get; set; }
            public string Monto { get; set; }
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

                        this.CargaTabla();
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


        protected void btnActualiza_Click(object sender, EventArgs e)
        {
            CargaTabla();
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            ExportaAExcel();
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

            string Pedidos = this.txtLProdctos.Text.Trim();
            int filtro = Convert.ToInt32(cmbTipoDocto.SelectedValue);

            FechaIstr = FechaI.ToShortDateString();
            FechaFstr = FechaF.ToShortDateString();
            BRenglon rnng = new BRenglon();
            try
            {
                BRenglon rng = new BRenglon();
                List<BRenglon> Tabla = new List<BRenglon>();
                SqlDataReader dr = null;
                
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Sesion.Emp_Cnx);
                string[] Parametros = {
                                        "@FechaIni",
                                        "@FechaFin",
                                        "@strProducto",
                                        "@iEstatus"
                                      };
                object[] Valores = new object[] {
                                        FechaIstr,
                                        FechaFstr,
                                        Pedidos,
                                        filtro

                                        };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRptConsumoPorCodigo", ref dr, Parametros, Valores);
                while (dr.Read())
                {
                    //  Id_Cte  Docto IdDocto Cte_NomComercial FechaDocto  Id_Prd Prd_Descripcion Prd_Presentacion Cantidad    Costo Monto

                    rng = new BRenglon();
                    rng.Id_Cte = int.Parse(Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_Cte"))));
                    rng.Docto = Convert.ToString(dr.GetValue(dr.GetOrdinal("Docto")));
                    rng.IdDocto = int.Parse(Convert.ToString(dr.GetValue(dr.GetOrdinal("IdDocto"))));
                    /// .ToShortDateString()
                    ///Convert.ToDateTime(Convert.ToString(dr.GetValue(dr.GetOrdinal("FechaFactura")))).ToShortDateString()
                    rng.Cte_NomComercial = Convert.ToString(dr.GetValue(dr.GetOrdinal("Cte_NomComercial")));
                    rng.FechaDocto = !string.IsNullOrEmpty(Convert.ToString(dr.GetValue(dr.GetOrdinal("FechaDocto")))) ? Convert.ToDateTime(Convert.ToString(dr.GetValue(dr.GetOrdinal("FechaDocto")))).ToShortDateString() : "&nbsp;";

                    rng.Id_Prd = Convert.ToString(dr.GetValue(dr.GetOrdinal("Id_Prd")));
                    rng.Prd_Descripcion = Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_Descripcion")));
                    rng.Prd_Presentacion = Convert.ToString(dr.GetValue(dr.GetOrdinal("Prd_Presentacion")));

                    rng.Cantidad = int.Parse(Convert.ToString(dr.GetValue(dr.GetOrdinal("Cantidad"))));
                    rng.Costo = !string.IsNullOrEmpty(Convert.ToString(Convert.ToString(dr.GetValue(dr.GetOrdinal("Costo"))))) ? Convert.ToString(dr.GetValue(dr.GetOrdinal("Costo"))) : "&nbsp;";
                    rng.Monto = !string.IsNullOrEmpty(Convert.ToString(Convert.ToString(dr.GetValue(dr.GetOrdinal("Monto"))))) ? Convert.ToString(dr.GetValue(dr.GetOrdinal("Monto"))) : "&nbsp;";


                    Tabla.Add(rng);
                }



                if (Tabla != null)
                {
                    strTabla = "";
                    btnExportar.Enabled = true;
                    lblMensaje.Text = "Se encontraron " + Tabla.Count.ToString() + " registros.";
                    foreach (BRenglon R in Tabla)
                    {
                        //  Docto IdDocto Id_Cte Cte_NomComercial FechaDocto  Id_Prd Prd_Descripcion Prd_Presentacion Cantidad    Costo Monto

                        /// rnng = new BRenglon();
                        //  rnng = R;
                        strTabla = strTabla + "<tr>" +
                                    "<td align='center'>" + R.Docto + "</td>" +
                                    "<td align='center'>" + R.IdDocto.ToString() + "</td>" +
                                    "<td align='center'>" + R.Id_Cte.ToString() + "</td>" +
                                    "<td nowrap>" + R.Cte_NomComercial + "</td>";
                        strTabla = strTabla + "<td align='center'>" + R.FechaDocto + "</td>" +
                                    "<td align='center'>" + R.Id_Prd + "</td>" +
                                    "<td nowrap>" + R.Prd_Descripcion + "</td>";
                                    ///    "<td align='center'>" + R.Prd_Presentacion + "</td>"
                        strTabla = strTabla + "<td align='center'>" + R.Cantidad + "</td>" +
                                    "<td align='right'>" + R.Costo + "</td>" + 
                                    "<td align='right'>" + R.Monto + "</td>" +
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

            string Pedidos = this.txtLProdctos.Text.Trim();
            int filtro = Convert.ToInt32(cmbTipoDocto.SelectedValue);

            FechaIstr = FechaI.ToShortDateString();
            FechaFstr = FechaF.ToShortDateString();


            string filenombre = "RptConsumo_" + DateTime.UtcNow.ToShortDateString().Replace("/", "") + "h" + DateTime.UtcNow.ToShortTimeString().Replace(".", "").Replace(":", "").Replace(" ", "");
            
            try
            {
                BRenglon rng = new BRenglon();
                List<BRenglon> Tabla = new List<BRenglon>();
                SqlDataReader dr = null;
                System.Data.DataSet dt = null;
                SqlDataAdapter sda = new SqlDataAdapter();

                
                CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Sesion.Emp_Cnx);
                string[] Parametros = {
                                        "@FechaIni",
                                        "@FechaFin",
                                        "@strProducto",
                                        "@iEstatus"
                                      };
                object[] Valores = new object[] {
                                        FechaIstr,
                                        FechaFstr,
                                        Pedidos,
                                        filtro

                                        };

                SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spRptConsumoPorCodigo", ref dt, Parametros, Valores);

                //  sda.Fill(dt);

                dt.Tables[0].TableName = "ReporteConsumo";

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