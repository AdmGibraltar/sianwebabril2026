using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIANWEB
{
    public partial class MonitorActividadesDetalle : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["CumpliVIDetalle"] != null)
            {
                List<eClienteBuscar> lst = new List<eClienteBuscar>();
                lst = (List<eClienteBuscar>)Session["CumpliVIDetalle"];
                grdCliente.DataSource = lst;
                grdCliente.DataBind();

            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CumpliVIDetalle"] != null)
                {
                    List<eClienteBuscar> lst = new List<eClienteBuscar>();
                    lst = (List<eClienteBuscar>)Session["CumpliVIDetalle"];

                    lblCliente.InnerText = lst.First().Cte_NomComercial;
                    lblTipo.InnerText = lst.First().tipoCliente;

                    grdCliente.DataSource = lst;
                    grdCliente.DataBind();
                }
            }
        }
    }
}