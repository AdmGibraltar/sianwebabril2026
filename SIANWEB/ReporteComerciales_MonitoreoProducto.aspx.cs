using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocios;
using DevExpress.Web;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;

namespace SIANWEB
{
    public partial class ReporteComerciales_MonitoreoProducto : System.Web.UI.Page
    {




        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //  cargarGrid();
                Session["activeMenu"] = 4;

                CN_Campanias cn = new CN_Campanias();
                var res = cn.ComboCampanias();

                this.cmbCampania.DataSource = res;
                cmbCampania.TextField = "Nombre";
                cmbCampania.ValueField = "Id";
                cmbCampania.DataBind();

                cmbCampania.Items[0].Selected = true;


                listaRepresentantes.DataSource = cn.ComboRIKS();
                listaRepresentantes.DataBind();

                listaProductos.DataSource = cn.ComboProductos();
                listaProductos.DataBind();

                CargarGrids(true);

            }

            if (ASPxCallbackPanel1.IsCallback)
            {
                RepGestProy_Listado.CollapseAll();
                RepGestACYS_Listado.CollapseAll();
                RepGestPed_Listado.CollapseAll();
                RepGestFac_Listado.CollapseAll();


            }

            CN_Campanias cn1 = new CN_Campanias();
            listaProductos.DataSource = cn1.ComboProductos();
            listaProductos.DataBind();

            listaRepresentantes.DataSource = cn1.ComboRIKS();
            listaRepresentantes.DataBind();

            CargarGrids();

            //RepGestProy_Listado.SettingsDetail.ExportMode = GridViewDetailExportMode.Expanded;
            //RepGestACYS_Listado.SettingsDetail.ExportMode = GridViewDetailExportMode.Expanded;
            //RepGestPed_Listado.SettingsDetail.ExportMode = GridViewDetailExportMode.Expanded;
            //RepGestFac_Listado.SettingsDetail.ExportMode = GridViewDetailExportMode.Expanded;
        }


        private void CargarGrids(Boolean primerVez = false)
        {

            var arr = listaProductos.GridView.GetSelectedFieldValues(listaProductos.KeyFieldName);
            var str = String.Join(",", arr);

            var arrRiks = listaRepresentantes.GridView.GetSelectedFieldValues(listaRepresentantes.KeyFieldName);
            var strRiks = String.Join(",", arrRiks);

            var FechaIni = dtFechaInicio.Value != null ? dtFechaInicio.Date : new DateTime(2010, 1, 1);
            var FechaFin = dtFechaFin.Value != null ? dtFechaFin.Date : new DateTime(2099, 12, 31);

            CN_Campanias cn = new CN_Campanias();
            var res = cn.ComboCampanias();

            int idCampania = 0;
            if (cmbCampania.SelectedItem == null)
                idCampania = 1;
            else
                idCampania = Int32.Parse(cmbCampania.SelectedItem.Value.ToString());


            if (rdlModo.Value.ToString() == "2") idCampania = -1;

            if (chkTipoReporte.Items[0].Selected || primerVez)
            {
                var res1 = cn.Reporte_Gestion_Proyecto_Principal(idCampania, str, FechaIni, FechaFin, strRiks);
                lblTitGestProy.Visible = true;

                RepGestProy_Listado.DataSource = res1;
                RepGestProy_Listado.DataBind();

                RepGestProy_Listado.Visible = true;
            }
            else
            {
                lblTitGestProy.Visible = false;
                RepGestProy_Listado.Visible = false;
            }

            if (chkTipoReporte.Items[1].Selected || primerVez)
            {
                lblTitGesAcys.Visible = true;
                var resAcys = cn.Reporte_Gestion_ACYS_Principal(idCampania, str, FechaIni, FechaFin, strRiks);

                RepGestACYS_Listado.DataSource = resAcys;
                RepGestACYS_Listado.DataBind();

                RepGestACYS_Listado.Visible = true;

            }
            else
            {
                lblTitGesAcys.Visible = false;
                RepGestACYS_Listado.Visible = false;
            }



            if (chkTipoReporte.Items[2].Selected || primerVez)
            {
                lblGestPedidos.Visible = true;

                var resPed = cn.Reporte_Gestion_Pedidos_Principal(idCampania, str, FechaIni, FechaFin, strRiks);

                RepGestPed_Listado.DataSource = resPed;
                RepGestPed_Listado.DataBind();

                RepGestPed_Listado.Visible = true;

            }
            else
            {
                lblGestPedidos.Visible = false;
                RepGestPed_Listado.Visible = false;
            }

            if (chkTipoReporte.Items[3].Selected || primerVez)
            {
                lblGestFacturas.Visible = true;
                var resFac = cn.Reporte_Gestion_Facturas_Principal(idCampania, str, FechaIni, FechaFin, strRiks);

                RepGestFac_Listado.DataSource = resFac;
                RepGestFac_Listado.DataBind();

                RepGestFac_Listado.Visible = true;
            }
            else
            {
                lblGestFacturas.Visible = false;
                RepGestFac_Listado.Visible = false;
            }
        }


        protected void RepGestProy_Detalle_DataBinding(object sender, EventArgs e)
        {
            var arr = listaProductos.GridView.GetSelectedFieldValues(listaProductos.KeyFieldName);
            var str = String.Join(",", arr);

            var FechaIni = dtFechaInicio.Value != null ? dtFechaInicio.Date : new DateTime(2010, 1, 1);
            var FechaFin = dtFechaFin.Value != null ? dtFechaFin.Date : new DateTime(2099, 12, 31);

            ASPxGridView detailGrid = (ASPxGridView)sender;
            object id = detailGrid.GetMasterRowKeyValue();

            int idCampania = 0;
            idCampania = Int32.Parse(cmbCampania.SelectedItem.Value.ToString());
            if (rdlModo.Value.ToString() == "2") idCampania = -1;

            CN_Campanias cn = new CN_Campanias();
            detailGrid.DataSource = cn.Reporte_Gestion_Proyecto_Detalle(idCampania, Int32.Parse(id.ToString()), str, FechaIni, FechaFin);

        }

        protected void RepGestProy_Detalle_Prod_DataBinding(object sender, EventArgs e)
        {
            var arr = listaProductos.GridView.GetSelectedFieldValues(listaProductos.KeyFieldName);
            var str = String.Join(",", arr);

            var FechaIni = dtFechaInicio.Value != null ? dtFechaInicio.Date : new DateTime(2010, 1, 1);
            var FechaFin = dtFechaFin.Value != null ? dtFechaFin.Date : new DateTime(2099, 12, 31);

            ASPxGridView detailGrid = (ASPxGridView)sender;
            object id = detailGrid.GetMasterRowKeyValue();

            int idCampania = 0;
            idCampania = Int32.Parse(cmbCampania.SelectedItem.Value.ToString());
            if (rdlModo.Value.ToString() == "2") idCampania = -1;

            CN_Campanias cn = new CN_Campanias();
            detailGrid.DataSource = cn.Reporte_Gestion_Proyecto_Detalle_Prod(Int32.Parse(id.ToString()), idCampania, str, FechaIni, FechaFin);
        }

        protected void RepGestACYS_Detalle_DataBinding(object sender, EventArgs e)
        {
            var arr = listaProductos.GridView.GetSelectedFieldValues(listaProductos.KeyFieldName);
            var str = String.Join(",", arr);

            var FechaIni = dtFechaInicio.Value != null ? dtFechaInicio.Date : new DateTime(2010, 1, 1);
            var FechaFin = dtFechaFin.Value != null ? dtFechaFin.Date : new DateTime(2099, 12, 31);

            ASPxGridView detailGrid = (ASPxGridView)sender;
            object id = detailGrid.GetMasterRowKeyValue();

            int idCampania = 0;
            idCampania = Int32.Parse(cmbCampania.SelectedItem.Value.ToString());
            if (rdlModo.Value.ToString() == "2") idCampania = -1;

            CN_Campanias cn = new CN_Campanias();
            detailGrid.DataSource = cn.Reporte_Gestion_ACYS_Detalle(idCampania, Int32.Parse(id.ToString()), str, FechaIni, FechaFin);

        }

        protected void RepGestACYS_Detalle_Prod_DataBinding(object sender, EventArgs e)
        {
            var arr = listaProductos.GridView.GetSelectedFieldValues(listaProductos.KeyFieldName);
            var str = String.Join(",", arr);

            var FechaIni = dtFechaInicio.Value != null ? dtFechaInicio.Date : new DateTime(2010, 1, 1);
            var FechaFin = dtFechaFin.Value != null ? dtFechaFin.Date : new DateTime(2099, 12, 31);

            ASPxGridView detailGrid = (ASPxGridView)sender;
            object id = detailGrid.GetMasterRowKeyValue();

            int idCampania = 0;
            idCampania = Int32.Parse(cmbCampania.SelectedItem.Value.ToString());
            if (rdlModo.Value.ToString() == "2") idCampania = -1;

            CN_Campanias cn = new CN_Campanias();
            detailGrid.DataSource = cn.Reporte_Gestion_ACYS_Detalle_Prod(idCampania, 1, Int32.Parse(id.ToString()), str, FechaIni, FechaFin);

        }

        protected void RepGestPEd_Detalle_DataBinding(object sender, EventArgs e)
        {
            var arr = listaProductos.GridView.GetSelectedFieldValues(listaProductos.KeyFieldName);
            var str = String.Join(",", arr);

            var FechaIni = dtFechaInicio.Value != null ? dtFechaInicio.Date : new DateTime(2010, 1, 1);
            var FechaFin = dtFechaFin.Value != null ? dtFechaFin.Date : new DateTime(2099, 12, 31);

            ASPxGridView detailGrid = (ASPxGridView)sender;
            object id = detailGrid.GetMasterRowKeyValue();

            int idCampania = 0;
            idCampania = Int32.Parse(cmbCampania.SelectedItem.Value.ToString());
            if (rdlModo.Value.ToString() == "2") idCampania = -1;

            CN_Campanias cn = new CN_Campanias();
            detailGrid.DataSource = cn.Reporte_Gestion_Pedidos_Detalle(idCampania, Int32.Parse(id.ToString()), str, FechaIni, FechaFin);
        }

        protected void RepGestPed_Detalle_Prod_DataBinding(object sender, EventArgs e)
        {
            var arr = listaProductos.GridView.GetSelectedFieldValues(listaProductos.KeyFieldName);
            var str = String.Join(",", arr);

            var FechaIni = dtFechaInicio.Value != null ? dtFechaInicio.Date : new DateTime(2010, 1, 1);
            var FechaFin = dtFechaFin.Value != null ? dtFechaFin.Date : new DateTime(2099, 12, 31);

            ASPxGridView detailGrid = (ASPxGridView)sender;
            object id = detailGrid.GetMasterRowKeyValue();

            int idCampania = 0;
            idCampania = Int32.Parse(cmbCampania.SelectedItem.Value.ToString());
            if (rdlModo.Value.ToString() == "2") idCampania = -1;

            CN_Campanias cn = new CN_Campanias();
            detailGrid.DataSource = cn.Reporte_Gestion_Pedidos_Detalle_Prod(idCampania, Int32.Parse(id.ToString()), str, FechaIni, FechaFin);
        }

        protected void RepGestFac_Detalle_DataBinding(object sender, EventArgs e)
        {
            var arr = listaProductos.GridView.GetSelectedFieldValues(listaProductos.KeyFieldName);
            var str = String.Join(",", arr);

            var FechaIni = dtFechaInicio.Value != null ? dtFechaInicio.Date : new DateTime(2010, 1, 1);
            var FechaFin = dtFechaFin.Value != null ? dtFechaFin.Date : new DateTime(2099, 12, 31);

            ASPxGridView detailGrid = (ASPxGridView)sender;
            object id = detailGrid.GetMasterRowKeyValue();

            int idCampania = 0;
            idCampania = Int32.Parse(cmbCampania.SelectedItem.Value.ToString());
            if (rdlModo.Value.ToString() == "2") idCampania = -1;

            CN_Campanias cn = new CN_Campanias();
            detailGrid.DataSource = cn.Reporte_Gestion_Facturas_Detalle(idCampania, Int32.Parse(id.ToString()), str, FechaIni, FechaFin);
        }

        protected void RepGestFac_Detalle_Prod_DataBinding(object sender, EventArgs e)
        {
            var arr = listaProductos.GridView.GetSelectedFieldValues(listaProductos.KeyFieldName);
            var str = String.Join(",", arr);

            var FechaIni = dtFechaInicio.Value != null ? dtFechaInicio.Date : new DateTime(2010, 1, 1);
            var FechaFin = dtFechaFin.Value != null ? dtFechaFin.Date : new DateTime(2099, 12, 31);

            ASPxGridView detailGrid = (ASPxGridView)sender;
            object id = detailGrid.GetMasterRowKeyValue();

            int idCampania = 0;
            idCampania = Int32.Parse(cmbCampania.SelectedItem.Value.ToString());
            if (rdlModo.Value.ToString() == "2") idCampania = -1;

            CN_Campanias cn = new CN_Campanias();
            detailGrid.DataSource = cn.Reporte_Gestion_Facturas_Prod(idCampania, Int32.Parse(id.ToString()), str, FechaIni, FechaFin);
        }

        protected void btnFiltro_Click(object sender, EventArgs e)
        {

        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {

            PrintingSystemBase ps = new PrintingSystemBase();
            var arr = new List<object>();

            if (chkTipoReporte.Items[0].Selected)
            {
                PrintableComponentLinkBase link1 = new PrintableComponentLinkBase(ps);
                link1.Component = grdExporter1;
                link1.RtfReportHeader = "Gestión de Proyectos";
                link1.RtfReportFooter = "        ";
                arr.Add(link1);
            }

            if (chkTipoReporte.Items[1].Selected)
            {
                PrintableComponentLinkBase link2 = new PrintableComponentLinkBase(ps);
                link2.Component = grdExporter2;
                link2.RtfReportHeader = "Gestión de ACyS";
                link2.RtfReportFooter = "        ";
                arr.Add(link2);
            }

            if (chkTipoReporte.Items[2].Selected)
            {
                PrintableComponentLinkBase link3 = new PrintableComponentLinkBase(ps);
                link3.Component = grdExporter3;
                link3.RtfReportHeader = "Gestión de Pedidos";
                link3.RtfReportFooter = "        ";
                arr.Add(link3);
            }

            if (chkTipoReporte.Items[3].Selected)
            {
                PrintableComponentLinkBase link4 = new PrintableComponentLinkBase(ps);
                link4.Component = grdExporter4;
                link4.RtfReportHeader = "Gestión de Facturas";
                link4.RtfReportFooter = "        ";
                arr.Add(link4);
            }

            CompositeLinkBase compositeLink = new CompositeLinkBase(ps);
            compositeLink.Links.AddRange(arr.ToArray());

            compositeLink.CreateDocument();
            using (MemoryStream stream = new MemoryStream())
            {
                compositeLink.ExportToXls(stream);
                WriteToResponse("Reporte_Monitoreo_Producto_" + DateTime.Now.ToString(), true, "xls", stream);
            }
            ps.Dispose();
        }

        private void WriteToResponse(string fileName, bool saveAsFile, string fileFormat, MemoryStream stream)
        {
            if (Page == null || Page.Response == null)
                return;
            string disposition = saveAsFile ? "attachment" : "inline";
            Page.Response.Clear();
            Page.Response.Buffer = false;
            Page.Response.AppendHeader("Content-Type", string.Format("application/{0}", fileFormat));
            Page.Response.AppendHeader("Content-Transfer-Encoding", "binary");
            Page.Response.AppendHeader("Content-Disposition",
                string.Format("{0}; filename={1}.{2}", disposition, fileName, fileFormat));
            Page.Response.BinaryWrite(stream.GetBuffer());
            Page.Response.End();
        }

        protected void grdExporter1_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
        {
            e.Column.Width = Unit.Pixel(200);

            if (e.RowType == GridViewRowType.Data)
            {
                e.BrickStyle.Font = new Font("sans-serif", 10);
            }

            if (e.RowType == GridViewRowType.Header)
            {
                e.BrickStyle.Font = new Font("sans-serif", 10, FontStyle.Bold);
                e.BrickStyle.BackColor = Color.Transparent;
                e.BrickStyle.ForeColor = Color.Black;
            }

            if (e.RowType == GridViewRowType.Footer)
            {

                e.BrickStyle.ForeColor = Color.FromArgb(0, 136, 215);
                e.BrickStyle.BackColor = Color.Transparent;
                e.BrickStyle.Font = new Font("sans-serif", 10, FontStyle.Bold);

            }
        }

        protected void grdExporter2_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
        {
            e.Column.Width = Unit.Pixel(200);
            if (e.RowType == GridViewRowType.Data)
            {
                e.BrickStyle.Font = new Font("sans-serif", 11);

            }
            if (e.RowType == GridViewRowType.Header)
            {
                e.BrickStyle.Font = new Font("sans-serif", 11, FontStyle.Bold);
                e.BrickStyle.BackColor = Color.Transparent;
                e.BrickStyle.ForeColor = Color.Black;
            }

            if (e.RowType == GridViewRowType.Footer)
            {
                e.BrickStyle.Font = new Font("sans-serif", 11, FontStyle.Bold);

                e.BrickStyle.ForeColor = Color.FromArgb(0, 136, 215);
                e.BrickStyle.BackColor = Color.Transparent;
            }
        }

        protected void grdExporter3_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
        {
            e.Column.Width = Unit.Pixel(200);
            if (e.RowType == GridViewRowType.Data)
            {
                e.BrickStyle.Font = new Font("sans-serif", 11);

            }
            if (e.RowType == GridViewRowType.Header)
            {
                e.BrickStyle.Font = new Font("sans-serif", 11, FontStyle.Bold);
                e.BrickStyle.BackColor = Color.Transparent;
                e.BrickStyle.ForeColor = Color.Black;
            }


            if (e.RowType == GridViewRowType.Footer)
            {
                e.BrickStyle.Font = new Font("sans-serif", 11, FontStyle.Bold);

                e.BrickStyle.ForeColor = Color.FromArgb(0, 136, 215);
                e.BrickStyle.BackColor = Color.Transparent;
            }
        }

        protected void grdExporter4_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
        {

            if (e.RowType == GridViewRowType.Data)
            {
                e.BrickStyle.Font = new Font("sans-serif", 11);

            }
            if (e.RowType == GridViewRowType.Header)
            {
                e.BrickStyle.Font = new Font("sans-serif", 11, FontStyle.Bold);
                e.BrickStyle.BackColor = Color.Transparent;
                e.BrickStyle.ForeColor = Color.Black;
            }

            if (e.RowType == GridViewRowType.Footer)
            {
                e.BrickStyle.Font = new Font("sans-serif", 11, FontStyle.Bold);

                e.BrickStyle.ForeColor = Color.FromArgb(0, 136, 215);
                e.BrickStyle.BackColor = Color.Transparent;
            }
        }
    }
}