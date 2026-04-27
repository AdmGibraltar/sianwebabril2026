using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaEntidad;
using Telerik.Web.UI;
using CapaNegocios;
using System.Collections;
using CapaDatos;
using System.Web.Services;
using System.IO;
using System.Xml.Serialization;
using System.Configuration;
using System.Data.OleDb;
using System.Xml;
using System.Text;
using System.Net;
using System.Web.UI.HtmlControls;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Net.Http;
using Newtonsoft.Json;
using SIANWEB.Core.UI;

namespace SIANWEB
{
    public partial class CapDashboardKpiDiario : BaseServerPage
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
               
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (Sesion == null)
                {
                    // CerrarVentana();
                }
                else
                {
                    //if (!Page.IsPostBack && Session["PosBackPagos" + Session.SessionID] == null)
                    if (!Page.IsPostBack)
                    {

                    }

                }
            }
            catch (Exception ex)
            {
               // ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

      
    }
}