using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIANWEB
{
    public partial class AutorizacionVinculacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSi_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["Rfc"] != null)
            {
                txtRfc.Value = Request.QueryString["Rfc"];
            }
            steptwo.Style.Add("display", "inline");
            stepOne.Style.Add("display", "none");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                string funcion = "CloseWindow(" + session.Id_U + "," + session.Id_Cd + ",'" + session.U_Nombre + "'," + 1 + ")";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void btnSolicitar_Click(object sender, EventArgs e)
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                string funcion = "CloseWindow(" + session.Id_U + "," + session.Id_Cd + ",'" + session.U_Nombre + "'," + 2 + ")";
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}