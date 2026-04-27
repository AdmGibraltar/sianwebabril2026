using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIANWEB
{
    public partial class Ventana_FacturasOC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                var pathPDF = Properties.Settings.Default.Ruta_Impresion_PDF;
               // var pathXML = Properties.Settings.Default.Fact_RutaXML;



                var folio = Request.QueryString["CFD_Folio"];
                var serie = Request.QueryString["CFD_Serie"];


                var rutaArchivo = Properties.Settings.Default.Ruta_Impresion_PDF + Properties.Settings.Default.Ruta_Impresion_TipoDoc + "_" + serie + "_" + folio.ToString() + ".pdf";
                Response.Redirect(rutaArchivo);

            }
            catch (Exception ex)
            {
                Response.Write("No se pudo imprimir el documento, intente mas tarde");
            }


        }
    }
}