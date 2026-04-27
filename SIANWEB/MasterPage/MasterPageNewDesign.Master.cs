using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;

namespace SIANWEB.MasterPage
{
    public partial class MasterPageNewDesign : System.Web.UI.MasterPage
    {
        #region "Propiedades"
        public int AñoInicio
        {
            get
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (sesion == null)
                {
                    return 1900;
                }
                else
                {
                    return ((Sesion)Session["Sesion" + Session.SessionID]).CalendarioIni.Year;
                }

            }
            set { }
        }
        public int MesInicio
        {
            get
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (sesion == null)
                {
                    return 1;
                }
                else
                {
                    return (((Sesion)Session["Sesion" + Session.SessionID]).CalendarioIni.Month - 1);
                }

            }
            set { }
        }
        public int DiaInicio
        {
            get
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (sesion == null)
                {
                    return 1;
                }
                else
                {
                    return ((Sesion)Session["Sesion" + Session.SessionID]).CalendarioIni.Day;
                }

            }
            set { }
        }
        public int AñoFin
        {
            get
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (sesion == null)
                {
                    return 1900;
                }
                else
                {
                    return ((Sesion)Session["Sesion" + Session.SessionID]).CalendarioFin.Year;
                }

            }
            set { }
        }
        public int MesFin
        {
            get
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (sesion == null)
                {
                    return 1;
                }
                else
                {
                    return (((Sesion)Session["Sesion" + Session.SessionID]).CalendarioFin.Month - 1);
                }

            }
            set { }
        }
        public int DiaFin
        {
            get
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (sesion == null)
                {
                    return 1;
                }
                else
                {
                    return ((Sesion)Session["Sesion" + Session.SessionID]).CalendarioFin.Day;
                }

            }
            set { }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Sesion" + Session.SessionID] == null) return;

            if (!Page.IsPostBack)
            {
                if (!Page.ClientScript.IsClientScriptIncludeRegistered("prototype"))
                {
                    /*prototype.js afecta vue js
                     */
                    if (!Page.Request.Url.ToString().Contains("ConfigCliente.aspx") &&
                        !Page.Request.Url.ToString().Contains("CatClientesV3.aspx") &&
                        !Page.Request.Url.ToString().Contains("Rep_Compras.aspx"))
                    {
                        Page.ClientScript.RegisterClientScriptInclude("prototype", ResolveUrl("../prototype.js"));
                    }
                }

                var sesion = (Sesion)Session["Sesion" + Session.SessionID];
                var DT = (DataTable)Session["DTMenu" + Session.SessionID];
                RadMenu1.DataSource = DT;
                RadMenu1.DataFieldID = "Sm_Cve";
                RadMenu1.DataFieldParentID = "Sm_Sm_Cve";
                RadMenu1.DataNavigateUrlField = "Sm_Href";
                RadMenu1.DataTextField = "Sm_Desc";
                RadMenu1.DataValueField = "Sm_Cve";

                RadMenu1.DataBind();
                if (RadMenu1.FindItemByValue("1") != null)
                    RadMenu1.FindItemByValue("1").ImageUrl = @"~\Imagenes\" + DT.Select("Sm_Cve=2")[0]["Sm_Img"];
                if (RadMenu1.FindItemByValue("2") != null)
                    RadMenu1.FindItemByValue("2").ImageUrl = @"~\Imagenes\" + DT.Select("Sm_Cve=2")[0]["Sm_Img"];
                if (RadMenu1.FindItemByValue("22") != null)
                    RadMenu1.FindItemByValue("22").ImageUrl = @"~\Imagenes\" + DT.Select("Sm_Cve=22")[0]["Sm_Img"];
                if (RadMenu1.FindItemByValue("33") != null)
                    RadMenu1.FindItemByValue("33").ImageUrl = @"~\Imagenes\" + DT.Select("Sm_Cve=33")[0]["Sm_Img"];
                if (RadMenu1.FindItemByValue("54") != null)
                    RadMenu1.FindItemByValue("54").ImageUrl = @"~\Imagenes\" + DT.Select("Sm_Cve=54")[0]["Sm_Img"];
                if (RadMenu1.FindItemByValue("72") != null)
                    RadMenu1.FindItemByValue("72").ImageUrl = @"~\Imagenes\" + DT.Select("Sm_Cve=72")[0]["Sm_Img"];
                if (RadMenu1.FindItemByValue("144") != null)
                    RadMenu1.FindItemByValue("144").ImageUrl = @"~\Imagenes\" + DT.Select("Sm_Cve=144")[0]["Sm_Img"];

                lblNombre.InnerText = sesion.U_Nombre;
            }
        }
    }
}