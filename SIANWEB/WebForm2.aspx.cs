using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CapaEntidad;
using ClosedXML.Excel;
using static SIANWEB.RepHPedidoDX;

namespace SIANWEB
{
    public partial class WebForm2 : System.Web.UI.Page
    {

        public string sFechaI = "";
        public string sFechaF = "";
        public string sPedidos = "";
        public string sFiltro = "";
        public string sFilename = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                sFechaI = Convert.ToString(System.Web.HttpContext.Current.Request.QueryString["sFechaI"]);
                sFechaF = Convert.ToString(System.Web.HttpContext.Current.Request.QueryString["sFechaF"]);
                sPedidos = Convert.ToString(System.Web.HttpContext.Current.Request.QueryString["sPedidos"]);
                sFiltro = Convert.ToString(System.Web.HttpContext.Current.Request.QueryString["sFiltro"]);
                sFilename = "ReporteSeguimientoPedidos_" + sFechaI + "__" + sFechaF;
                 
                
                LlenaDatos();
            }

        }


        void LlenaDatos()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            SqlDataReader dr = null;
            DataSet ds = new DataSet();

            CapaDatos.CD_Datos CapaDatos = new CapaDatos.CD_Datos(Sesion.Emp_Cnx);
            string[] Parametros = {
                                        "@FechaIni",
                                        "@FechaFin",
                                        "@strPedido",
                                        "@iEstatus",
                                        "@sCliente",
                                      };
            object[] Valores = new object[] {
                                        sFechaI,
                                        sFechaF,
                                        sPedidos,
                                        sFiltro,
                                        null,
                                        };

            /// SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spHistorialPedidos2vPkFlat", ref dr, Parametros, Valores);
            SqlCommand sqlcmd = CapaDatos.GenerarSqlCommand("spHistorialPedidos2vPkFlat", ref ds, Parametros, Valores);

            DetalleRenglonTrackingPedido DetaRng = new DetalleRenglonTrackingPedido();
            List<DetalleRenglonTrackingPedido> lstDetRpt = new List<DetalleRenglonTrackingPedido>();

            ds.Tables[0].TableName = "SeguimientoPedidos";


            using (XLWorkbook wb = new XLWorkbook())
            {

                foreach (DataTable dt in ds.Tables)
                {
                    wb.Worksheets.Add(dt);
                }

                ///     HttpContext.Current.ApplicationInstance.CompleteRequest();

                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=" + sFilename + ".xlsx");
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    //  HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                    //  HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                    //  HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
                    Response.End();
                    /// endRequest = true;
                }

                ///  if (endRequest)
                    /// Response.End();
            }

        }

    }
}