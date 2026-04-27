using SIANWEB.Core.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocios;
using CapaEntidad;
using DevExpress.Web;
using CapaModelo;
using DevExpress.XtraCharts;
using System.Text;
using ClosedXML.Excel;
using System.Data;
using System.IO;
using DevExpress.Utils;
using System.Globalization;
using System.Threading;

namespace SIANWEB.PortalRIK.GestionPromocion
{
    public partial class Integralidad : BaseServerPage
    {

        private List<CatCRM_IntMapaApps> listaApps;

        private List<CatCRM_IntegralidadMes> listaIntMes;


        private double VPO_Total;
        private double VPT_Total;
        private double Venta_Total;
        private double GAP_Total;

        private double GAP_Obs_Total;

        protected void Page_Load(object sender, EventArgs e)
        {

            CultureInfo ci = new CultureInfo("es-MX", false);
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;



            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            if (!IsPostBack)
            {
                Session["activeMenu"] = 10;
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();

                //spCrmInt_ComboRIKs
                CN_Comun.DevLlenaCombo(1, sesion.Id_Emp, sesion.Id_U, sesion.Emp_Cnx, "spCrmInt_ComboRIKs", ref ddlComboRik);

                CN_Comun.DevLlenaCombo(1, sesion.Id_Emp, 0, sesion.Emp_Cnx, "spCrmInt_Clientes", ref ddlRazonSocialCliente);

                CN_Comun.DevLlenaCombo(1, sesion.Id_Emp, 0, sesion.Emp_Cnx, "spCrmInt_Segmento", ref ddlComboSegmentos);


                var riks = ddlComboRik.DataSource;



                chkListaRiks.DataSource = riks;
                chkListaRiks.TextField = "Descripcion";
                chkListaRiks.ValueField = "Id";
                chkListaRiks.DataBind();


                DateTimeScaleOptions dateTimeScaleOptions = ((XYDiagram)chart.Diagram).AxisX.DateTimeScaleOptions;

                dateTimeScaleOptions.WorkdaysOnly = true;
                dateTimeScaleOptions.ScaleMode = ScaleMode.Manual;
                dateTimeScaleOptions.AggregateFunction = AggregateFunction.Average;
                dateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Month;
                dateTimeScaleOptions.GridSpacing = 1;



            }



            if (ASPxCallbackPanel1.IsCallback)
            {
                // Intentionally pauses server-side processing,
                // to demonstrate the Loading Panel functionality.
                //if (!enableAnimation)
                //    System.Threading.Thread.Sleep(500);
                CargarGrid();

            }


            if (ASPxCallbackPanel2.IsCallback)
            {
                CargarRepresentantes();

            }


            if (ASPxCallbackPanel3.IsCallback)
            {
                CargarCombos();

            }



            if (modalInt.IsCallback)
            {
                // Intentionally pauses server-side processing,
                // to demonstrate the Loading Panel functionality.
                int i = 0;
            }


            string selectedValue = string.Empty;
        }
        private void CargarCombos()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();

            CN_Comun.DevLlenaCombo(1, sesion.Id_Emp, Int32.Parse(ddlComboRik.Value.ToString()), sesion.Emp_Cnx, "spCrmInt_Clientes", ref ddlRazonSocialCliente);

            CN_Comun.DevLlenaCombo(1, sesion.Id_Emp, Int32.Parse(ddlComboRik.Value.ToString()), sesion.Emp_Cnx, "spCrmInt_Segmento", ref ddlComboSegmentos);

            ddlRazonSocialCliente.SelectedIndex = -1;
            ddlComboSegmentos.SelectedIndex = -1;

        }

        private void CargarGrid()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            string conexion = sesion.Emp_Cnx;
            CN_CatCRMInt_Integralidad cn = new CN_CatCRMInt_Integralidad();

            string Ctes = "", Riks = "", Segs = "";

            if (ddlRazonSocialCliente.Value != null) Ctes = ddlRazonSocialCliente.Value.ToString();
            if (ddlComboRik.Value != null) Riks = ddlComboRik.Value.ToString();
            if (ddlComboSegmentos.Value != null) Segs = ddlComboSegmentos.Value.ToString();

            var res = cn.ListadoRiks(sesion.Id_Emp, sesion.Id_Cd, Riks, Ctes, Segs, conexion);

            dgListadoRiks.DataSource = res;
            dgListadoRiks.DataBind();

        }

        private void CargarRepresentantes()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            string conexion = sesion.Emp_Cnx;
            CN_CatCRMInt_Integralidad cn = new CN_CatCRMInt_Integralidad();

            //var riks = String.Join(",", chkListaRiks.Value);
            int i = 0;
            StringBuilder riks = new StringBuilder("");
            for (i = 0; i < chkListaRiks.SelectedItems.Count; i++)
            {
                riks.Append(chkListaRiks.SelectedItems[i].Value.ToString() + ",");
            }

            var res = cn.ListadoIntegralidadRIK(sesion.Id_Emp, sesion.Id_Cd, riks.ToString(), sesion.Emp_Cnx);

            var resAgr = res.GroupBy(t => new { ID = t.Rik_Nombre })
                   .Select(g => new
                   {
                       Integralidad = g.Average(p => Math.Round(p.Integralidad * 100, 2)),
                       Integralidad_Obs = g.Average(p => Math.Round(p.Integralidad_Obs * 100, 2)),
                       Rik_Nombre = g.Key.ID
                   }).ToList();


            chartRiks.DataSource = resAgr;
            chartRiks.DataBind();

            chartRiks.Series[0].LabelsVisibility = DefaultBoolean.True;
            chartRiks.Series[1].LabelsVisibility = DefaultBoolean.True;
            chartRiks.Visible = true;

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrid();
        }

        protected void modalInt_WindowCallback(object source, DevExpress.Web.PopupWindowCallbackArgs e)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            string conexion = sesion.Emp_Cnx;

            string[] fields = { "Rik_Nombre", "Cd_Nombre", "Cte_NomComercial", "Id_Ter", "Seg_Descripcion", "Id_Rik","Id_Cte", "Venta","VPT","VPO",
                                "Seg_ValUniDim", "Cantidad", "Id_Seg", "Seg_Unidades" };
            object[] values = dgListadoRiks.GetRowValues(Int32.Parse(e.Parameter), fields) as object[];

            lblRIK.Text = values[0].ToString();
            lblCliente.Text = values[6].ToString() + " - " + values[2].ToString();
            lblTerritorio.Text = values[3].ToString();
            lblSegmento.Text = values[4].ToString();

            //lblVenta.Text = values[7].ToString();
            //lblVPO.Text = values[9].ToString();
            //lblVPT.Text = values[8].ToString();
            //lblIntegralidad.Text = "0";

            lblValStd.Text = values[10].ToString();
            lblCantidad.Text = values[11].ToString();
            lblUnidades.Text = values[13].ToString();


            CN_CatCRMInt_Integralidad cn = new CN_CatCRMInt_Integralidad();

            listaIntMes = cn.ListadoAplicaciones(Int32.Parse(values[6].ToString()), Int32.Parse(values[5].ToString()), Int32.Parse(values[3].ToString()), Int32.Parse(values[12].ToString()), conexion);

            rptIntegralidadMes.DataSource = listaIntMes;
            rptIntegralidadMes.DataBind();


            //var res1 = listaApps.Select(x => new CatCRM_IntArea
            //{
            //    Id_Area = x.Id_Area,
            //    Area_Descripcion = x.Area_Descripcion
            //}
            //).GroupBy(x => new { x.Id_Area, x.Area_Descripcion }).Select(x => x.FirstOrDefault()).ToList();

            //rptAreas.DataSource = res1;
            //rptAreas.DataBind();



        }
        protected void rpTablaAplicaciones_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            ASPxGridView grid = (ASPxGridView)e.Item.FindControl("dgListadoAplicaciones");

            var res = ((CapaModelo.CatCRM_IntMapaApps)e.Item.DataItem);
            var lista = res.Aplicaciones;

            foreach (CatCRM_IntAplicaciones app in lista)
            {
                app.Id_Seg = res.Id_Seg;
                app.Id_Area = res.Id_Area;
                app.Id_Sol = res.Id_Sol;
            }


            grid.DataSource = lista;
            grid.DataBind();


        }

        protected void rptAreas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            int idarea = ((CapaModelo.CatCRM_IntArea)e.Item.DataItem).Id_Area;
            var res = listaApps.Where(x => x.Id_Area == idarea).ToList();


            Repeater rep = (Repeater)e.Item.FindControl("rpTablaAplicaciones");



            rep.DataSource = res;
            rep.DataBind();

        }

        protected void rptIntegralidadMes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            VPO_Total = 0;
            VPT_Total = 0;
            Venta_Total = 0;
            GAP_Total = 0;
            GAP_Obs_Total = 0;

            int anio = ((CapaModelo.CatCRM_IntegralidadMes)e.Item.DataItem).Anio;

            var listaSegmentos = ((CatCRM_IntegralidadMes)e.Item.DataItem).Segmentos;



            listaApps = listaSegmentos;

            Repeater rep = (Repeater)e.Item.FindControl("rptAreas");
            var res = listaSegmentos;

            var res1 = res.Select(x => new CatCRM_IntArea
            {
                Id_Area = x.Id_Area,
                Area_Descripcion = x.Area_Descripcion
            }
            ).GroupBy(x => new { x.Id_Area, x.Area_Descripcion }).Select(x => x.FirstOrDefault()).ToList();


            rep.DataSource = res1;
            rep.DataBind();


            CatCRM_IntegralidadMes integralidadReg = (CatCRM_IntegralidadMes)e.Item.DataItem;


            foreach (CatCRM_IntMapaApps seg in integralidadReg.Segmentos)
            {
                foreach (CatCRM_IntAplicaciones app in seg.Aplicaciones)
                {
                    VPO_Total += app.VPO;
                    VPT_Total += app.VPT;
                    Venta_Total += app.Venta;
                    GAP_Obs_Total += app.GAP_Observado;
                    GAP_Total += app.GAP_Teorico;
                }

            }



            var lblVPTMes = (ASPxLabel)e.Item.FindControl("lblVPTMes");
            var lblVPOMes = (ASPxLabel)e.Item.FindControl("lblVPOMes");
            var lblVentaRealMes = (ASPxLabel)e.Item.FindControl("lblVentaRealMes");
            var lblIntegralidadTeorica = (ASPxLabel)e.Item.FindControl("lblIntegralidadTeorica");
            var lblIntegralidadObs = (ASPxLabel)e.Item.FindControl("lblIntegralidadObs");

            var lblGAP_Teorico = (ASPxLabel)e.Item.FindControl("lblGAP_Teorico");
            var lblGAP_Observado = (ASPxLabel)e.Item.FindControl("lblGAP_Observado");

            var lblOpTeorica = (ASPxLabel)e.Item.FindControl("lblOpTeorica");
            var lblOpObservada = (ASPxLabel)e.Item.FindControl("lblOpObservada");



            lblVPTMes.Text = "$" + Math.Round(VPT_Total, 2).ToString();
            lblVPOMes.Text = "$" + Math.Round(VPO_Total, 2).ToString();
            lblVentaRealMes.Text = "$" + Math.Round(Venta_Total, 2).ToString();
            lblGAP_Teorico.Text = "$" + Math.Round(GAP_Total, 2).ToString();
            lblGAP_Observado.Text = "$" + Math.Round(GAP_Obs_Total, 2).ToString();



            double int_T, int_O = 0;
            int_T = Math.Round((Venta_Total / VPT_Total) * 100, 2);
            int_O = Math.Round((Venta_Total / VPO_Total) * 100, 2);

            if (VPT_Total > 0)
            {
                lblIntegralidadTeorica.Text = int_T.ToString() + "%";
                lblIntegralidadObs.Text = int_O.ToString() + "%";

                lblOpTeorica.Text = Math.Round(100 - int_T, 2).ToString() + "%";
                lblOpObservada.Text = Math.Round(100 - int_O, 2).ToString() + "%";

            }
            else
            {
                lblIntegralidadTeorica.Text = 0 + "%";
                lblIntegralidadObs.Text = 0 + "%";
            }

            //chart.Series[0].Points.Add(new SeriesPoint(integralidadReg.Mes.Substring(0,3) + " " + integralidadReg.Anio, int_T));
            //chart.Series[1].Points.Add(new SeriesPoint(integralidadReg.Mes.Substring(0, 3) + " " + integralidadReg.Anio, int_O));

            chart.Series[0].Points.Add(new SeriesPoint(new DateTime(integralidadReg.Anio, integralidadReg.MesNum, 1), int_T));
            chart.Series[1].Points.Add(new SeriesPoint(new DateTime(integralidadReg.Anio, integralidadReg.MesNum, 1), int_O));

        }

        protected void dgListadoRiks_PageIndexChanged(object sender, EventArgs e)
        {
            CargarGrid();
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            string conexion = sesion.Emp_Cnx;

            CN_CatCRMInt_Integralidad cn = new CN_CatCRMInt_Integralidad();

            DataTable listaExcel = cn.ListadoIntegralidadRIK_Excel(sesion.Id_Emp, sesion.Id_Cd, conexion);


            //Name of File  

            string ruta = Server.MapPath("~/Reportes/") + "Integralidad_" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";

            using (XLWorkbook wb = new XLWorkbook())
            {
                //Add DataTable in worksheet  
                wb.Worksheets.Add(listaExcel, "Reporte Integralidad");


                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(ruta);

                    //Return xlsx Excel File  


                    System.IO.FileInfo file = new System.IO.FileInfo(ruta);

                    if (file.Exists)
                    {
                        Response.Clear();
                        Response.ContentType = "application/xslx";
                        Response.AddHeader("Content-Length", file.Length.ToString());
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + "Integralidad_" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx");
                        Response.WriteFile(file.FullName);

                    }
                    else
                    {

                        Response.Write("This file does not exist.");
                    }




                }
            }


        }

        protected void dgListadoAplicaciones_DataBound(object sender, EventArgs e)
        {

        }
    }
}