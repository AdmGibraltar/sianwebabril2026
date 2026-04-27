using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIANWEB
{
    public partial class FrmPreguntas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSi_Click(object sender, EventArgs e)
        {
            string Respuesta = "Si";
            Session["Respuesta" + Session.SessionID] = Respuesta;
            RAM1.Alert("El sistema creara una remisión correspondiente a la queja.");
            //Alerta("El sistema creara una remisión correspondiente a la queja.");
            func_cerrarventana(Respuesta);
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            string Respuesta = "No";
            Session["Respuesta" + Session.SessionID] = Respuesta;
            RAM1.Alert("El sistema creara la queja sin una remisión.");
            //Alerta("El sistema creara la queja sin una remisión.");
            func_cerrarventana(Respuesta);
        }

        protected void func_cerrarventana(string Respuesta)
        {
            string funcion = "CloseAndRebind('" + Respuesta + "')";
            string script = "<script>" + funcion + "</script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
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
                Alerta(Message);
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
                Alerta("Error: [" + NombreFuncion + "] " + eme.Message.ToString());
                //this.lblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();

            }
            catch (Exception ex)
            {
                Alerta("Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString());
                //this.lblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }
        #endregion
    }
}