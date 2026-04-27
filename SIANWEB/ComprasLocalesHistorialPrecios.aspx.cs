using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CapaEntidad;
using CapaNegocios;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;

namespace SIANWEB
{
    public partial class ComprasLocalesHistorialPrecios : System.Web.UI.Page
    {
        public string strTabla = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            // RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        if (Request.QueryString["idPrd"] != null)
                        {
                            this.Title = "Historial de Precios [ " + Request.QueryString["idPrd"].ToString() + "]";

                            BuscaHistorialPrecios(Convert.ToInt64(Request.QueryString["idPrd"].ToString()));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.lblMensaje.Text = ex.Message;
            }
        }

        private void BuscaHistorialPrecios(long idPrd)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.AppSettings["strConnection"].ToString();
                SqlCommand cmd = new SqlCommand();
                
                cmd.CommandText = "spHistorialCambioCosto_CCL " + idPrd.ToString();
                
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        strTabla = strTabla + "<tr><td class='cell'>" + sdr["Prd_FechaInicio"].ToString() + "</td>";
                        strTabla = strTabla + "<td class='cell'>" + sdr["Prd_FechaFin"].ToString() + "</td>";
                        strTabla = strTabla + "<td class='cell'>" + sdr["Pre_Descripcion"].ToString() + "</td>";
                        strTabla = strTabla + "<td class='cell'>" + sdr["Prd_Pesos"].ToString() + "</td></tr>";
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                this.lblMensaje.Text = ex.Message;
            }
        }

    }
}