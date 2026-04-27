using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocios;

namespace SIANWEB
{
    public partial class VentanaRemisionesOC2 : System.Web.UI.Page
    {

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["remisionOC2"] != null)
            {
                var res = Session["remisionOC2"];
                rgRemisiones.DataSource = res;
                rgRemisiones.DataBind();
            }
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                Session["remisionOC2"] = null;
                int idOC = Int32.Parse(Request.QueryString["IdOC"]);

                CN_CatCNac_OrdenCompra cnOC = new CN_CatCNac_OrdenCompra();
                var res = cnOC.ConsultarRemisionesPedido(idOC);
                Session["remisionOC2"] = res;
                rgRemisiones.DataSource = res;
                rgRemisiones.DataBind();
            }

        }
    }
}