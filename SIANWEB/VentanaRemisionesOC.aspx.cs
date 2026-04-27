using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocios;

namespace SIANWEB
{
    public partial class VentanaRemisionesOC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                int idOC = Int32.Parse(Request.QueryString["IdOC"]);

                CN_CatCNac_OrdenCompra cnOC = new CN_CatCNac_OrdenCompra();
                var res = cnOC.ConsultarRemisionesPedido(idOC);

                rgRemisiones.DataSource = res;
                rgRemisiones.DataBind();
            }

        }
    }
}