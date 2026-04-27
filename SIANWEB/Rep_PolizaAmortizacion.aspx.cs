using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using CapaEntidad;
using System.Text;
using CapaNegocios;
using System.Configuration;
using System.Data;

namespace SIANWEB
{
    public partial class Rep_PolizaAmortizacion : System.Web.UI.Page
    {
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
                        /*if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }*/
                        Inicializar();

                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();


                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {

        }

        private void Inicializar()
        {
            CargarAnios();
        }
        private void CargarAnios()
        {
            int anio = Convert.ToInt32(DateTime.Now.Year);
            RadComboBoxItem myItem = new RadComboBoxItem();
            int i = 0;
            for (int x = anio; x > anio - 3; x = x - 1)
            {
                cmbAño.Items.Insert(i, new Telerik.Web.UI.RadComboBoxItem { Text = x.ToString(), Value = x.ToString() });
            }
            cmbAño.SelectedIndex = 2;
        }

        protected void RadToolBar1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            string accionError = string.Empty;
            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (!string.IsNullOrEmpty(cmbAño.SelectedValue))
                    if (cmbAño.SelectedValue != "-1")
                        switch (btn.CommandName)
                        {
                            case "excel":
                                GenerarExcel();
                                break;
                        }
                    else
                        Alerta("Ingresar un año válido");
                else
                    Alerta("Ingresar un año válido");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GenerarExcel()
        {
            try
            {
                StringBuilder tabla = new StringBuilder();
                Funcion fn = new Funcion();
                tabla.Append("<html><head><meta http-equiv='Content-Type' content='text/html; charset=ISO-8859-1'></head><body><table style='width:1200px'>");

                EscribeDetalle(ref tabla);
                tabla.Append("</table></body></html>");
                fn.ExportarExcel(cbReport.Text + "_" + cmbAño.Text + "_" + cmbMes.Text, tabla.ToString());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void EscribeDetalle(ref StringBuilder Tabla)
        {
            try
            {

                DataTable dt = new DataTable();
                dt = GetList();
                Tabla.Append("<tr>");

                //lectura y escritura de columnas
                decimal importe = 0;
                for (int i = 0; i < dt.Columns.Count; i++)
                {

                    Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:90; background:#D3D3D3;'>");
                    Tabla.Append(dt.Columns[i].ColumnName);
                    Tabla.Append("</th>");

                }
                Tabla.Append("</tr>");

                // lectura y escritura de filas
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    Tabla.Append("<tr>");
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {

                        Tabla.Append("<td style='text-align:center'>");
                        Tabla.Append(dt.Rows[j][i].ToString());
                        Tabla.Append("</td>");

                        if (dt.Columns[i].ColumnName == "Importe")
                        {
                            importe += decimal.Parse(dt.Rows[j][i].ToString());
                        }
                    }
                    Tabla.Append("</tr>");
                }
                if (importe > 0)
                {
                    Tabla.Append("<tr>");
                    Tabla.Append("<td colspan=" + (dt.Columns.Count - 2) + " ></td>");
                    Tabla.Append("<td >TOTAL:</td>");
                    Tabla.Append("<td >" + importe + "</td>");
                    Tabla.Append("</tr>");
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private DataTable GetList()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                string StrCnx = ConfigurationManager.AppSettings.Get("strConnectionPropietarios");

                int filterCustomer = 0;
                if (cbCliente.Checked)
                {
                    filterCustomer = 1;
                }

                DataTable dtresult = new CN_Rep_PolizaAmortizacion().ConsultaReportePoliza(StrCnx, int.Parse(cbReport.SelectedValue), sesion.Id_Cd_Ver, filterCustomer, 0, int.Parse(cmbMes.SelectedValue), int.Parse(cmbAño.Text));

                return dtresult;
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
    }
}