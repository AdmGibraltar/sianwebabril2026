using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

using Telerik.Web.UI;
using CapaEntidad;
using System.Collections;
using CapaNegocios;
using CapaDatos;
using DevExpress.Web;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.BandedGrid;
using System.Web.Services;
using Newtonsoft.Json;
using DevExpress.Web.Bootstrap;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Text;
using System.Globalization;
using System.IO;
using System.Configuration;
using System.Web.Caching;
using System.Security.Cryptography;

namespace SIANWEB
{
    public class UploadedFilesStorage
    {
        public string Path { get; set; }
        public string Key { get; set; }
        public DateTime LastUsageTime { get; set; }

        public IList<UploadedFileInfo> Files { get; set; }
    }

    public class UploadedFileInfo
    {
        public string UniqueFileName { get; set; }
        public string OriginalFileName { get; set; }
        public string FilePath { get; set; }
        public string FileSize { get; set; }
    }

    public partial class ProPedidoVI2 : System.Web.UI.Page
    {



        #region Variables

        private const string SaveLockPrefix = "SAVE_LOCK_";
        private const int SaveLockSeconds = 12; // ventana de bloqueo corta
        private const int IdempotencyWindowSeconds = 90; // ventana de deduplicación


        protected string SubmissionID
        {
            get
            {
                return HiddenField.Get("SubmissionID").ToString().Trim();
            }
            set
            {
                HiddenField.Set("SubmissionID", value);
            }
        }
        UploadedFilesStorage UploadedFilesStorage
        {
            get { return UploadControlHelper2.GetUploadedFilesStorageByKey(SubmissionID); }
        }
        protected void Page_PreLoad(object sender, EventArgs e)
        {
            UploadControlHelper2.RemoveOldStorages();
        }


        public DataTable dt
        {
            get
            {
                return (DataTable)Session["dtPedidoVI" + Session.SessionID];

            }
            set
            {
                Session["dtPedidoVI" + Session.SessionID] = value;

            }
        }

        public DataTable dtNuevaLista
        {
            get
            {
                return (DataTable)Session["dtPedidoVILista" + Session.SessionID];
            }
            set
            {
                Session["dtPedidoVILista" + Session.SessionID] = value;
            }
        }

        public DataTable dt_Resto
        {
            get
            {
                return (DataTable)Session["dtPedidoVI_Resto" + Session.SessionID];
            }
            set
            {
                Session["dtPedidoVI_Resto" + Session.SessionID] = value;
            }
        }

        public ArrayList al
        {
            get
            {
                return (ArrayList)Session["Borrados" + Session.SessionID];
            }
            set { Session["Borrados" + Session.SessionID] = value; }
        }
        double iva_cd
        {
            get
            {
                double? _iva_cd = (double?)Session["iva_cd" + Session.SessionID + Page.Request.Url.ToString().Trim().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]];
                return _iva_cd == null ? 0 : (double)_iva_cd;
            }
            set
            {
                Session["iva_cd" + Session.SessionID + Page.Request.Url.ToString().Trim().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value;
            }
        }

        private bool terr = false;
        private bool prod = false;

        public Sesion session
        {
            get
            {
                return (Sesion)Session["Sesion" + Session.SessionID];
            }
            set
            {
                Session["Sesion" + Session.SessionID] = value;

            }
        }
        private object _producto;
        public object producto
        {
            get { return _producto; }
            set { _producto = value; }
        }
        public int productoNuevo = 0;
        public int Id_TG
        {
            get
            {
                string _idTGStr = Request.QueryString["Id_TG"];
                int _idTG = 0;
                if (_idTGStr != null)
                {
                    if (int.TryParse(_idTGStr, out _idTG))
                    {
                        return _idTG;
                    }
                }
                return _idTG;
            }
        }

        private void MensajeSolicitud(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "modalMensajesolicitud('" + mensaje + "')", true);
        }

        #endregion
        #region Eventos


        protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            if (Session["Prod"] != null)
            {
                DataTable dtTemp = (DataTable)Session["Prod"];

                rg1.DataSource = dtTemp;
                rg1.DataBind();
            }
        }

        protected void grid_Restos_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            if (Session["Restos"] != null)
            {
                DataTable dtTemp = (DataTable)Session["Restos"];

                rg1_Restos.DataSource = dtTemp;
                rg1_Restos.DataBind();
            }
        }

        protected void rg1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            DataTable dtTemp = (DataTable)Session["Prod"];
            if (Session["nuevaLista"] != null)
            {
                dtNuevaLista = (DataTable)Session["nuevaLista"];
            }
            else
            {
                dtNuevaLista = new DataTable();
                dtNuevaLista.Columns.Add("Id_Prdold", System.Type.GetType("System.Int64"));
                dtNuevaLista.Columns.Add("Id_Prd", System.Type.GetType("System.Int64"));
                dtNuevaLista.Columns.Add("Estatus", System.Type.GetType("System.Int32"));
                dtNuevaLista.Columns.Add("Tipo", System.Type.GetType("System.String"));
                dtNuevaLista.Columns.Add("Acs_Doc", System.Type.GetType("System.String"));
                dtNuevaLista.Columns.Add("ACS_ReqOC", System.Type.GetType("System.String"));
                dtNuevaLista.Columns.Add("Prd_Original", System.Type.GetType("System.Int32"));
            }
            ASPxGridView gridView = (ASPxGridView)sender;

            e.Cancel = true;
            foreach (DataRow registo in dtTemp.Rows)
            {
                if (registo["Id_Prd"].ToString() == e.Keys["Id_Prd"].ToString())
                {
                    IDictionaryEnumerator enumerator = e.NewValues.GetEnumerator();
                    decimal cantidad = 0;
                    decimal importe = 0;
                    decimal total = 0;
                    enumerator.Reset();
                    while (enumerator.MoveNext())
                    {
                        if (enumerator.Key.ToString() == "Prd_Cantidad")
                        {
                            cantidad = decimal.Parse(enumerator.Value.ToString());
                            registo[enumerator.Key.ToString().Trim()] = enumerator.Value;
                        }
                        else if (enumerator.Key.ToString() == "Prd_Precio")
                        {
                            importe = decimal.Parse(enumerator.Value.ToString());
                            registo[enumerator.Key.ToString().Trim()] = enumerator.Value;
                        }
                        else if (enumerator.Key.ToString() == "Prd_Importe")
                        {
                            total = cantidad * importe;
                            if (decimal.Parse(enumerator.Value.ToString()) != total)
                            {
                                registo[enumerator.Key.ToString().Trim()] = total.ToString();
                            }
                            else
                            {
                                registo[enumerator.Key.ToString().Trim()] = enumerator.Value;
                            }
                        }
                        else
                        {
                            registo[enumerator.Key.ToString().Trim()] = enumerator.Value;
                        }
                    }


                    DataRow dr;
                    dr = dtNuevaLista.NewRow();
                    dr["Id_Prdold"] = registo[0];
                    dr["Id_Prd"] = registo[1];
                    dr["Estatus"] = 2;
                    dr["Tipo"] = "VI";
                    dtNuevaLista.Rows.Add(dr);
                    break;
                }
            }
            gridView.CancelEdit();
            e.Cancel = true;

            Session["Prod"] = dtTemp;
            Session["nuevaLista"] = dtNuevaLista;

            rg1.DataSource = dtTemp;
            rg1.DataBind();
            Consultar_IVA_Cliente();
            calcularsubtotal();
        }

        protected void rg1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {

            DataTable dtTemp = (DataTable)Session["Prod"];

            ASPxGridView gridView = (ASPxGridView)sender;
            decimal cantidad = 0;
            decimal importe = 0;
            decimal total = 0;
            DataRow row = dtTemp.NewRow();
            IDictionaryEnumerator enumerator = e.NewValues.GetEnumerator();
            enumerator.Reset();
            while (enumerator.MoveNext())
            {
                if (enumerator.Key.ToString().Trim() != "Count")
                {

                    if (enumerator.Key.ToString() == "Prd_Cantidad")
                    {
                        cantidad = decimal.Parse(enumerator.Value.ToString());
                        row[enumerator.Key.ToString().Trim()] = enumerator.Value == null ? DBNull.Value : enumerator.Value;
                    }
                    else if (enumerator.Key.ToString() == "Prd_Precio")
                    {
                        importe = decimal.Parse(enumerator.Value.ToString());
                        row[enumerator.Key.ToString().Trim()] = enumerator.Value == null ? DBNull.Value : enumerator.Value;
                    }
                    else if (enumerator.Key.ToString() == "Prd_Importe")
                    {
                        total = cantidad * importe;
                        if (decimal.Parse(enumerator.Value.ToString()) != total)
                        {
                            row[enumerator.Key.ToString().Trim()] = total.ToString();
                        }
                        else
                        {
                            row[enumerator.Key.ToString().Trim()] = enumerator.Value == null ? DBNull.Value : enumerator.Value;
                        }
                    }
                    else
                    {
                        row[enumerator.Key.ToString().Trim()] = enumerator.Value == null ? DBNull.Value : enumerator.Value;
                    }
                }
            }

            gridView.CancelEdit();
            e.Cancel = true;
            dtTemp.Rows.Add(row);
            Session["Prod"] = dtTemp;
            rg1.DataSource = dtTemp;
            rg1.DataBind();
            Consultar_IVA_Cliente();
            calcularsubtotal();
        }

        public static string Mensajes(string mensaje)
        {
            string script = @"<script type='text/javascript'> alert('" + mensaje + "'); </script>";
            return script;
        }

        [WebMethod]
        protected void rg1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                DataTable dtTemp = (DataTable)Session["Prod"];
                //if (ValidarCambios() == 0)
                //{
                //    //mensaje("Este pedido se encuentra asignado, si desea realizar cambios, favor de desasignar el pedido.");
                //    string mensaje = "Este pedido se encuentra asignado, si desea realizar cambios, favor de desasignar el pedido.";
                //    ScriptManager.RegisterStartupScript(this, typeof(System.Web.UI.Page), "Alerta", ProPedidoVI2.Mensajes(mensaje), false);
                //    return;
                //}
                //else
                //{

                DataTable dtNuevaLista;
                if (Session["nuevaLista"] != null)
                {
                    dtNuevaLista = (DataTable)Session["nuevaLista"];
                }
                else
                {
                    dtNuevaLista = new DataTable();
                    dtNuevaLista.Columns.Add("Id_Prdold", System.Type.GetType("System.Int64"));
                    dtNuevaLista.Columns.Add("Id_Prd", System.Type.GetType("System.Int64"));
                    dtNuevaLista.Columns.Add("Estatus", System.Type.GetType("System.Int32"));
                    dtNuevaLista.Columns.Add("Tipo", System.Type.GetType("System.String"));
                    dtNuevaLista.Columns.Add("Acs_Doc", System.Type.GetType("System.String"));
                    dtNuevaLista.Columns.Add("ACS_ReqOC", System.Type.GetType("System.String"));
                }
                DataRow dr;

                foreach (DataRow registo in dtTemp.Rows)
                {
                    if (registo["Id_Prd"].ToString() == e.Keys["Id_Prd"].ToString())
                    {
                        dr = dtNuevaLista.NewRow();
                        dr["Id_Prdold"] = registo["Id_Prd"];
                        dr["Id_Prd"] = registo["Id_Prd"];
                        dr["Estatus"] = 3;
                        dr["Tipo"] = "VI";
                        dtNuevaLista.Rows.Add(dr);

                        registo.Delete();
                        break;
                    }
                }

                e.Cancel = true;
                ((BootstrapGridView)sender).JSProperties["cpIsUpdated"] = 1;

                Session["Prod"] = dtTemp;
                Session["nuevaLista"] = dtNuevaLista;
                Consultar_IVA_Cliente();
                calcularsubtotal();
                rg1.DataSource = dtTemp;
                rg1.DataBind();
                //}

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void rg1_Restos_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            DataTable dtTemp = (DataTable)Session["Restos"];
            if (Session["nuevaLista"] != null)
            {
                dtNuevaLista = (DataTable)Session["nuevaLista"];
            }
            else
            {
                dtNuevaLista = new DataTable();
                dtNuevaLista.Columns.Add("Id_Prdold", System.Type.GetType("System.Int64"));
                dtNuevaLista.Columns.Add("Id_Prd", System.Type.GetType("System.Int64"));
                dtNuevaLista.Columns.Add("Estatus", System.Type.GetType("System.Int32"));
                dtNuevaLista.Columns.Add("Tipo", System.Type.GetType("System.String"));
                dtNuevaLista.Columns.Add("Acs_Doc", System.Type.GetType("System.String"));
                dtNuevaLista.Columns.Add("ACS_ReqOC", System.Type.GetType("System.String"));
            }
            ASPxGridView gridView = (ASPxGridView)sender;



            e.Cancel = true;

            foreach (DataRow registo in dtTemp.Rows)
            {
                if (registo["Id_Prd"].ToString() == e.Keys["Id_Prd"].ToString())
                {

                    decimal cantidad = 0;
                    decimal importe = 0;
                    decimal total = 0;

                    IDictionaryEnumerator enumerator = e.NewValues.GetEnumerator();
                    enumerator.Reset();
                    while (enumerator.MoveNext())
                    {
                        if (enumerator.Key.ToString() == "Prd_Cantidad")
                        {
                            cantidad = decimal.Parse(enumerator.Value.ToString());
                            registo[enumerator.Key.ToString().Trim()] = enumerator.Value;
                        }
                        else if (enumerator.Key.ToString() == "Prd_Precio")
                        {
                            importe = decimal.Parse(enumerator.Value.ToString());
                            registo[enumerator.Key.ToString().Trim()] = enumerator.Value;
                        }
                        else if (enumerator.Key.ToString() == "Prd_Importe")
                        {
                            total = cantidad * importe;
                            if (decimal.Parse(enumerator.Value.ToString()) != total)
                            {
                                registo[enumerator.Key.ToString().Trim()] = total.ToString();
                            }
                            else
                            {
                                registo[enumerator.Key.ToString().Trim()] = enumerator.Value;
                            }
                        }
                        else
                        {
                            registo[enumerator.Key.ToString().Trim()] = enumerator.Value;
                        }
                    }


                    DataRow dr;
                    dr = dtNuevaLista.NewRow();
                    dr["Id_Prdold"] = registo[0];
                    dr["Id_Prd"] = registo[1];
                    dr["Estatus"] = 2;
                    dr["Tipo"] = "NE";
                    dtNuevaLista.Rows.Add(dr);
                    break;
                }
            }

            gridView.CancelEdit();
            e.Cancel = true;

            Session["Restos"] = dtTemp;
            Session["nuevaLista"] = dtNuevaLista;

            rg1_Restos.DataSource = dtTemp;
            rg1_Restos.DataBind();
            Consultar_IVA_Cliente();
            calcularsubtotal();
        }

        protected void rg1_Restos_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            decimal cantidad = 0;
            decimal importe = 0;
            decimal total = 0;
            DataTable dtTemp = (DataTable)Session["Restos"];
            if (Session["nuevaLista"] != null)
            {
                dtNuevaLista = (DataTable)Session["nuevaLista"];
            }
            else
            {
                dtNuevaLista = new DataTable();
                dtNuevaLista.Columns.Add("Id_Prdold", System.Type.GetType("System.Int64"));
                dtNuevaLista.Columns.Add("Id_Prd", System.Type.GetType("System.Int64"));
                dtNuevaLista.Columns.Add("Estatus", System.Type.GetType("System.Int32"));
                dtNuevaLista.Columns.Add("Tipo", System.Type.GetType("System.String"));
                dtNuevaLista.Columns.Add("Acs_Doc", System.Type.GetType("System.String"));
                dtNuevaLista.Columns.Add("ACS_ReqOC", System.Type.GetType("System.String"));
            }
            ASPxGridView gridView = (ASPxGridView)sender;

            DataRow row = dtTemp.NewRow();
            IDictionaryEnumerator enumerator = e.NewValues.GetEnumerator();
            enumerator.Reset();

            string idProd = "0";
            while (enumerator.MoveNext())
            {
                if (enumerator.Key.ToString().Trim() != "Count")
                {
                    row[enumerator.Key.ToString().Trim()] = enumerator.Value == null ? DBNull.Value : enumerator.Value;

                    if (enumerator.Key.ToString().Trim() == "Id_Prd")
                    {
                        idProd = enumerator.Value == null ? "" : enumerator.Value.ToString().Trim();
                    }

                    if (enumerator.Key.ToString() == "Prd_Cantidad")
                    {
                        cantidad = decimal.Parse(enumerator.Value.ToString());
                        row[enumerator.Key.ToString().Trim()] = enumerator.Value == null ? DBNull.Value : enumerator.Value;
                    }
                    else if (enumerator.Key.ToString() == "Prd_Precio")
                    {
                        importe = decimal.Parse(enumerator.Value.ToString());
                        row[enumerator.Key.ToString().Trim()] = enumerator.Value == null ? DBNull.Value : enumerator.Value;
                    }
                    else if (enumerator.Key.ToString() == "Prd_Importe")
                    {
                        total = cantidad * importe;
                        if (decimal.Parse(enumerator.Value.ToString()) != total)
                        {
                            row[enumerator.Key.ToString().Trim()] = total.ToString();
                        }
                        else
                        {
                            row[enumerator.Key.ToString().Trim()] = enumerator.Value == null ? DBNull.Value : enumerator.Value;
                        }
                    }
                    else
                    {
                        row[enumerator.Key.ToString().Trim()] = enumerator.Value == null ? DBNull.Value : enumerator.Value;
                    }
                }
            }

            DataRow dr;
            dr = dtNuevaLista.NewRow();
            dr["Id_Prdold"] = idProd;
            dr["Id_Prd"] = idProd;
            dr["Estatus"] = 1;
            dr["Tipo"] = "NE";
            dtNuevaLista.Rows.Add(dr);


            gridView.CancelEdit();
            e.Cancel = true;
            dtTemp.Rows.Add(row);

            Session["Restos"] = dtTemp;
            Session["nuevaLista"] = dtNuevaLista;
            rg1_Restos.DataSource = dtTemp;
            rg1_Restos.DataBind();
            Consultar_IVA_Cliente();
            calcularsubtotal();


        }

        protected void btneliminarSelccionar_ServerClick(object sender, EventArgs e)
        {
            DataTable dtTemp = (DataTable)Session["Restos"];

            if (Session["nuevaLista"] != null)
            {
                dtNuevaLista = (DataTable)Session["nuevaLista"];
            }
            else
            {
                dtNuevaLista = new DataTable();
                dtNuevaLista.Columns.Add("Id_Prdold", System.Type.GetType("System.Int64"));
                dtNuevaLista.Columns.Add("Id_Prd", System.Type.GetType("System.Int64"));
                dtNuevaLista.Columns.Add("Estatus", System.Type.GetType("System.Int32"));
                dtNuevaLista.Columns.Add("Tipo", System.Type.GetType("System.String"));
                dtNuevaLista.Columns.Add("Acs_Doc", System.Type.GetType("System.String"));
                dtNuevaLista.Columns.Add("ACS_ReqOC", System.Type.GetType("System.String"));
            }


            int rowcount = rg1_Restos.VisibleRowCount;
            for (var i = rowcount; i >= 0; i--)
            {
                if (rg1_Restos.GetRowValues(i) != null)
                {
                    if (rg1_Restos.Selection.IsRowSelected(i))
                    {

                        long idProd = Convert.ToInt64(rg1_Restos.GetRowValues(i, "Id_Prd").ToString().Trim());


                        DataRow dr;
                        dr = dtNuevaLista.NewRow();
                        dr["Id_Prdold"] = idProd;
                        dr["Id_Prd"] = idProd;
                        dr["Estatus"] = 3;
                        dr["Tipo"] = "NE";
                        dtNuevaLista.Rows.Add(dr);

                        foreach (DataRow registro in dtTemp.Rows)
                        {
                            if (registro["Id_Prd"].ToString() == idProd.ToString())
                            {
                                registro.Delete();
                                break;
                            }
                        }
                    }
                }
            }

            Session["Restos"] = dtTemp;
            Session["nuevaLista"] = dtNuevaLista;

            rg1.JSProperties["cpDelete"] = "delete";
            rg1_Restos.DataSource = dtTemp;
            rg1_Restos.DataBind();
            Consultar_IVA_Cliente();
            calcularsubtotal();

            TabName.Value = Request.Form[TabName.UniqueID];
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "mantaintabs('" + TabName.Value + "')", true);
        }

        protected void Button2_ServerClick(object sender, EventArgs e)
        {
            DataTable dtTemp = (DataTable)Session["Prod"];
            DataTable dtNuevaLista;
            if (Session["nuevaLista"] != null)
            {
                dtNuevaLista = (DataTable)Session["nuevaLista"];
            }
            else
            {
                dtNuevaLista = new DataTable();
                dtNuevaLista.Columns.Add("Id_Prdold", System.Type.GetType("System.Int64"));
                dtNuevaLista.Columns.Add("Id_Prd", System.Type.GetType("System.Int64"));
                dtNuevaLista.Columns.Add("Estatus", System.Type.GetType("System.Int32"));
                dtNuevaLista.Columns.Add("Tipo", System.Type.GetType("System.String"));
                dtNuevaLista.Columns.Add("Acs_Doc", System.Type.GetType("System.String"));
                dtNuevaLista.Columns.Add("ACS_ReqOC", System.Type.GetType("System.String"));
            }

            if (ValidarCambios() == 0)
            {
                mensajeInfo("Este pedido se encuentra asignado, si desea realizar cambios, favor de desasignar el pedido.");
                return;
            }
            else
            {

                int rowcount = rg1.VisibleRowCount;
                for (var i = rowcount; i >= 0; i--)
                {
                    if (rg1.GetRowValues(i) != null)
                    {
                        if (rg1.Selection.IsRowSelected(i))
                        {
                            long idProd = Convert.ToInt64(rg1.GetRowValues(i, "Id_Prd").ToString().Trim());


                            DataRow dr;
                            dr = dtNuevaLista.NewRow();
                            dr["Id_Prdold"] = idProd;
                            dr["Id_Prd"] = idProd;
                            dr["Estatus"] = 3;
                            dr["Tipo"] = "VI";
                            dtNuevaLista.Rows.Add(dr);

                            foreach (DataRow registro in dtTemp.Rows)
                            {
                                if (registro["Id_Prd"].ToString() == idProd.ToString())
                                {
                                    registro.Delete();
                                    break;
                                }
                            }
                        }
                    }
                }

                Session["Prod"] = dtTemp;
                Session["nuevaLista"] = dtNuevaLista;

                rg1.DataSource = dtTemp;
                rg1.DataBind();


                Consultar_IVA_Cliente();
                calcularsubtotal();

                TabName.Value = Request.Form[TabName.UniqueID];
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "mantaintabs('" + TabName.Value + "')", true);
            }
        }

        private void ValidarPermisos()
        {
            try
            {

                bool _PermisoGuardar = bool.Parse(Request.QueryString["PermisoGuardar"].ToString().Trim());
                bool _PermisoModificar = bool.Parse(Request.QueryString["PermisoModificar"].ToString().Trim());

                if (_PermisoGuardar == false & _PermisoModificar == false)
                {
                    btncaptacion_Guardar.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void rg1_Restos_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            DataTable dtTemp = (DataTable)Session["Restos"];
            if (Session["nuevaLista"] != null)
            {
                dtNuevaLista = (DataTable)Session["nuevaLista"];
            }
            else
            {
                dtNuevaLista = new DataTable();
                dtNuevaLista.Columns.Add("Id_Prdold", System.Type.GetType("System.Int64"));
                dtNuevaLista.Columns.Add("Id_Prd", System.Type.GetType("System.Int64"));
                dtNuevaLista.Columns.Add("Estatus", System.Type.GetType("System.Int32"));
                dtNuevaLista.Columns.Add("Tipo", System.Type.GetType("System.String"));
                dtNuevaLista.Columns.Add("Acs_Doc", System.Type.GetType("System.String"));
                dtNuevaLista.Columns.Add("ACS_ReqOC", System.Type.GetType("System.String"));
            }
            ASPxGridView gridView = (ASPxGridView)sender;
            DataRow dr;

            foreach (DataRow registo in dtTemp.Rows)
            {
                if (registo["Id_Prd"].ToString() == e.Keys["Id_Prd"].ToString())
                {
                    dr = dtNuevaLista.NewRow();
                    dr["Id_Prdold"] = registo["Id_Prd"];
                    dr["Id_Prd"] = registo["Id_Prd"];
                    dr["Estatus"] = 3;
                    dr["Tipo"] = "NE";
                    dtNuevaLista.Rows.Add(dr);
                    e.Cancel = true;

                    registo.Delete();
                    break;
                }
            }


            Session["Restos"] = dtTemp;
            Session["nuevaLista"] = dtNuevaLista;

            rg1.JSProperties["cpDelete"] = "delete";
            rg1_Restos.DataSource = dtTemp;
            rg1_Restos.DataBind();
            Consultar_IVA_Cliente();
            calcularsubtotal();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            cargarComboClientesAcuerdo();

            if (Session["Prod"] != null)
            {
                DataTable dtTemp = (DataTable)Session["Prod"];

                rg1.DataSource = dtTemp;
                rg1.DataBind();
            }

            if (Session["Restos"] != null)
            {
                DataTable dt_Resto = (DataTable)Session["Restos"];

                rg1_Restos.DataSource = dt_Resto;
                rg1_Restos.DataBind();
            }

            var comboboxColumn = rg1.DataColumns["ACS_ReqOC"] as DevExpress.Web.Bootstrap.BootstrapGridViewComboBoxColumn;
            DevExpress.Web.Internal.ReflectionUtils.SetNonPublicInstancePropertyValue(comboboxColumn.PropertiesComboBox, "DataSecurityMode", DevExpress.Web.DataSecurityMode.Default);

            var comboboxColumnRestos = rg1_Restos.DataColumns["ACS_ReqOC"] as DevExpress.Web.Bootstrap.BootstrapGridViewComboBoxColumn;
            DevExpress.Web.Internal.ReflectionUtils.SetNonPublicInstancePropertyValue(comboboxColumnRestos.PropertiesComboBox, "DataSecurityMode", DevExpress.Web.DataSecurityMode.Default);

            var comboboxColumndoc = rg1.DataColumns["Acs_Doc"] as DevExpress.Web.Bootstrap.BootstrapGridViewComboBoxColumn;
            DevExpress.Web.Internal.ReflectionUtils.SetNonPublicInstancePropertyValue(comboboxColumndoc.PropertiesComboBox, "DataSecurityMode", DevExpress.Web.DataSecurityMode.Default);

            var comboboxColumnRestosdoc = rg1_Restos.DataColumns["Acs_Doc"] as DevExpress.Web.Bootstrap.BootstrapGridViewComboBoxColumn;
            DevExpress.Web.Internal.ReflectionUtils.SetNonPublicInstancePropertyValue(comboboxColumnRestosdoc.PropertiesComboBox, "DataSecurityMode", DevExpress.Web.DataSecurityMode.Default);

        }



        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                Session["Semana"] = null;
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                rg1.JSProperties["cpDelete"] = "";
                txtPedCaptadorPor.Value = Sesion.U_Nombre;
                if (Sesion == null)
                {
                    CerrarVentana();
                }
                else
                {

                    if (this.IsPostBack)
                    {
                        TabName.Value = Request.Form[TabName.UniqueID];

                    }
                    if (!Page.IsPostBack)
                    {


                        SubmissionID = UploadControlHelper2.GenerateUploadedFilesStorageKey();
                        UploadControlHelper2.AddUploadedFilesStorage(SubmissionID);

                        ValidarPermisos();
                        Session["Prod"] = null;
                        Session["nuevaLista"] = null;
                        Session["Restos"] = null;

                        _nuevoPedidoSinProgramar = false;
                        HF_pedido.Value = _nuevoPedidoSinProgramar.ToString().Trim();
                        Session["Id_Ped" + Session.SessionID] = null;
                        Session["dtPedidoVI" + Session.SessionID] = null;
                        Session["Borrados" + Session.SessionID] = null; ;
                        Session["ProductosNoAcys"] = null;
                        Session.Add("ProductosNoAcys", new List<int>());


                        HF_Emp_Cnx.Value = Sesion.Emp_Cnx;
                        HF_IdCd.Value = session.Id_Cd_Ver.ToString().Trim();
                        HF_IdEmp.Value = session.Id_Emp.ToString().Trim();


                        rg1.DataSource = null;
                        rg1.DataBind();

                        rg1_Restos.DataSource = null;
                        rg1_Restos.DataBind();


                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            return;
                        }
                        if (Session["Borrados" + Session.SessionID] == null)
                        {
                            Session["Borrados" + Session.SessionID] = new ArrayList();
                        }
                        Inicializar();
                        al = new ArrayList();

                    }
                }
            }
            catch (Exception ex)
            {
                mensajeError(ex.ToString().Trim() + ", " + new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        void ValidateInputData()
        {
            bool isInvalid = UploadedFilesTokenBox.Tokens.Count == 0;
            if (isInvalid)
                throw new Exception("No se a cargado ningun documento.");
        }

        protected void ProcessSubmit(UploadedFileInfo fileInfo, ref string base64)
        {
            // process uploaded files here
            byte[] fileContent = File.ReadAllBytes(fileInfo.FilePath);
            base64 = Convert.ToBase64String(fileContent);
        }

        protected void DocumentsUploadControl_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            bool isSubmissionExpired = false;
            if (UploadedFilesStorage == null)
            {
                isSubmissionExpired = true;
                UploadControlHelper2.AddUploadedFilesStorage(SubmissionID);
            }
            UploadedFileInfo tempFileInfo = UploadControlHelper2.AddUploadedFileInfo(SubmissionID, e.UploadedFile.FileName);

            e.UploadedFile.SaveAs(tempFileInfo.FilePath);

            if (e.IsValid)
                e.CallbackData = tempFileInfo.UniqueFileName + "|" + isSubmissionExpired;
        }


        protected void cargarArchivo(ref string nombredoc, ref string extension, ref string archivobase64)
        {
            ValidateInputData();

            List<UploadedFileInfo> resultFileInfos = new List<UploadedFileInfo>();

            bool allFilesExist = true;

            if (UploadedFilesStorage == null)
                UploadedFilesTokenBox.Tokens = new TokenCollection();

            foreach (string fileName in UploadedFilesTokenBox.Tokens)
            {
                UploadedFileInfo demoFileInfo = UploadControlHelper2.GetDemoFileInfo(SubmissionID, fileName);
                FileInfo fileInfo = new FileInfo(demoFileInfo.FilePath);
                nombredoc = fileName;
                extension = Path.GetExtension(fileName);

                if (fileInfo.Exists)
                {
                    ProcessSubmit(demoFileInfo, ref archivobase64);
                    demoFileInfo.FileSize = fileInfo.Length.ToString().Trim();
                    resultFileInfos.Add(demoFileInfo);
                }
                else
                    allFilesExist = false;
            }



            if (allFilesExist && resultFileInfos.Count > 0)
            {
                UploadedFilesTokenBox.ErrorText = "Archivos cargados exitosamente.";
                UploadedFilesTokenBox.IsValid = false;

                UploadControlHelper2.RemoveUploadedFilesStorage(SubmissionID);
                ASPxEdit.ClearEditorsInContainer(FormLayout, true);
            }
            else
            {
                UploadedFilesTokenBox.ErrorText = "Arhivo no cargado. Revise la informacion del archivo.";
                UploadedFilesTokenBox.IsValid = false;
            }
        }


        protected void btnguardar_Click(object sender, EventArgs e)
        {
            string lockKey;
            if (!TryAcquireSaveLock(out lockKey))
            {
                mensajeInfo("Ya hay un guardado en proceso. Por favor, no presione Guardar varias veces. Reintente mas tarde.");
                return;
            }

            try
            {
                PreGuardar();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError($"Guardar: error en PreGuardar. Sesión={Session.SessionID}, Mensaje={ex.Message}");
                mensajeError("Ocurrió un problema al preparar el guardado. Intente nuevamente.");
                ClearLastPedidoHash();
                throw;
            }
            finally
            {
                ReleaseSaveLock(lockKey);
            }
        }

        protected void btnDireccion_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Direccion", "AbrirBuscarDireccionEntrega()", true);
        }

        protected void btnActualizardatos_Click(object sender, EventArgs e)
        {
            CargarDatosAcuerdo();
        }

        protected void btnActualizarDireccion_Click(object sender, EventArgs e)
        {
            cargardireccion();
        }

        protected void btnCoreccion(object sender, EventArgs e)
        {
            if (Session["Respuesta" + Session.SessionID] != null)
            {
                if (Convert.ToBoolean(Session["Respuesta" + Session.SessionID]))
                {
                    if (Request.QueryString["IdAutorizacion"] != null)
                    {
                        HF_ID.Value = "";
                    }
                    Guardar();

                }
            }

        }

        public void cargardireccion()
        {
            if (Session["Id_Buscar" + Session.SessionID] != null)
            {

                if (HF_Param.Value == "precio")
                {
                    if (Session["Id_Buscar" + Session.SessionID] != null)
                    {
                        long idProducto = Convert.ToInt64(Session["Id_Buscar" + Session.SessionID].ToString().Trim());
                        cmbProductoDet_TextChanged(idProducto);

                    }

                }
                if (HF_Param.Value == "direccion")
                {
                    CN_CatCliente clsCliente = new CN_CatCliente();
                    ClienteDirEntrega cliente = new ClienteDirEntrega();
                    Sesion session2 = (Sesion)Session["Sesion" + Session.SessionID];
                    session2 = (Sesion)Session["Sesion" + Session.SessionID];
                    cliente.Id_Emp = session2.Id_Emp;
                    cliente.Id_Cd = session2.Id_Cd_Ver;

                    cliente.Id_CteDirEntrega = Int32.Parse(Session["Id_Buscar" + Session.SessionID].ToString().Trim()) - 1;
                    cliente.Id_Cte = Int32.Parse(Session["Descripcion_Buscar" + Session.SessionID].ToString().Trim());
                    clsCliente.ConsultaClienteDirEntrega(cliente, session2.Emp_Cnx);
                    txtCalle.Text = cliente.Cte_Calle;
                    txtNo.Text = cliente.Cte_Numero;
                    txtCp.Text = cliente.Cte_Cp.Trim();
                    txtColonia.Text = cliente.Cte_Colonia;
                    txtMunicipio.Text = cliente.Cte_Municipio;
                    txtEstado.Text = cliente.Cte_Estado;
                }
            }
        }

        protected void txtProducto_Load(object sender, EventArgs e)
        {
            producto = sender;
        }


        protected void cmbProductoDet_TextChanged(long idProducto)
        {

            CN_CatProducto cn_catproducto = new CN_CatProducto();
            Producto pr = new Producto();
            try
            {

                DataTable dt = (DataTable)Session["Prod"];
                DataTable dt_Resto = (DataTable)Session["Restos"];

                DataRow[] Ar_dr;
                Ar_dr = dt.Select("Id_prd='" + idProducto + "'");

                DataRow[] Ar_dr_Restos;
                Ar_dr_Restos = dt_Resto.Select("Id_prd='" + idProducto + "'");


                CN_CapAcys cnCa = new CN_CapAcys();
                if (txtIdTer.Value == "")
                {
                    TabName.Value = "tabAcuerdoEconomico";
                    mensajeInfo("Por favor, capture un territorio en la vista \"Datos Generales\"");

                    return;
                }
                if (txtIdCte.Value == "")
                {
                    TabName.Value = "tabAcuerdoEconomico";
                    mensajeInfo("Por favor, capture un territorio en la vista \"Datos Generales\"");

                    return;
                }
                if (txtIdRik.Value == "")
                {
                    TabName.Value = "tabAcuerdoEconomico";
                    mensajeInfo("Por favor, capture un representante de ventas en la vista \"Datos Generales\"");

                    return;
                }
                if (_nuevoPedidoSinProgramar && cnCa.ExisteProductoEnGarantia(session.Id_Emp, session.Id_Cd, idProducto, Convert.ToInt32(txtIdTer.Value), Convert.ToInt32(txtIdCte.Value), Convert.ToInt32(txtIdRik.Value), session))
                {
                    TabName.Value = "tabAcuerdoEconomico";
                    mensajeInfo("Solo se aceptan productos con modalidad de venta convencional que no sean parte del ACYS. Por favor, ingrese otro código de producto.");

                    return;
                }
                if (Ar_dr.Length > 0)
                {
                    TabName.Value = "tabAcuerdoEconomico";
                    mensajeInfo("Producto ya capturado");

                    return;
                }

                if (Ar_dr_Restos.Length > 0)
                {
                    TabName.Value = "tabAcuerdoEconomico";
                    mensajeInfo("Producto ya capturado");

                    return;
                }

                if (string.IsNullOrEmpty(txtClave.Text))
                    productoNuevo = 1;
                pr.Id_Cte = !string.IsNullOrEmpty(txtIdCte.Value) ? Convert.ToInt32(txtIdCte.Value) : 0;


                cn_catproducto.ConsultaProductos(ref pr, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, idProducto, ref productoNuevo, 2);

                DataTable dtTemp_Resto;
                if (Session["Restos"] != null)
                {
                    dtTemp_Resto = (DataTable)Session["Restos"];
                }
                else
                {
                    dtTemp_Resto = new DataTable();
                }


                dtTemp_Resto.Rows.Add(new object[] {
                    idProducto,
                    idProducto,
                    pr.Prd_Descripcion,
                    pr.Prd_Presentacion,
                    pr.Prd_UniNs,

                    -100,
                    -100,
                    -100,

                    0,
                    pr.Prd_Precio,
                    pr.Prd_Precio,
                    0,
                    "F",

                    DBNull.Value,
                    DBNull.Value,
                    DBNull.Value,
                    DBNull.Value,
                    DBNull.Value,

                    0,
                    DBNull.Value,
                    DBNull.Value,
                    DBNull.Value,
                    0,
                    0
                    ,pr.Prd_Activo
            });


                string ConexionCentral = ConfigurationManager.AppSettings["strConnectionCentral"].ToString().Trim();
                ConvenioDet conv;
                ConvenioDet convdet;
                CN_Convenio cn_conv;
                List<string> Productos = new List<string>();

                List<ConvenioDet> lconveniosdet = new List<ConvenioDet>();
                ConvenioDet lconvdet = new ConvenioDet();


                if (dtTemp_Resto.Rows.Count > 0)
                {
                    for (int x = 0; x < dtTemp_Resto.Rows.Count; x++)
                    {

                        conv = new ConvenioDet();
                        convdet = new ConvenioDet();
                        cn_conv = new CN_Convenio();
                        conv.Id_Emp = session.Id_Emp;
                        conv.Id_Cd = session.Id_Cd_Ver;
                        conv.Id_Cte = Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1");
                        conv.Id_Prd = Convert.ToInt64(dtTemp_Resto.Rows[x]["Id_Prd"]);
                        double PrecioIngresado = Convert.ToDouble(dtTemp_Resto.Rows[x]["Prd_Precio"]);

                        if (Convert.ToInt64(dtTemp_Resto.Rows[x]["Id_Prd"]) <= 999999999999)
                        {
                            //cn_conv.Convenio_ConsultaPrecio(conv, ref convdet, ConexionCentral);

                            //if (convdet != null && convdet.PCD_PrecioAAAEsp > 0)
                            //{
                            //    dtTemp_Resto.Rows[x]["Prd_PrecioLista"] = convdet.PCD_PrecioAAAEsp.ToString();
                            //}
                            //else
                            //{
                            Producto producto = new Producto();
                            //obtener datos de producto
                            CN_CatProducto clsProducto = new CN_CatProducto();
                            producto.Id_Cte = Convert.ToInt32(Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1"));
                            int productoNuevo = 0;
                            clsProducto.ConsultaProductos(ref producto, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, Convert.ToInt64(dtTemp_Resto.Rows[x]["Id_Prd"]), ref productoNuevo, 0);

                            if (0 < producto.Prd_PLista)
                            {
                                dtTemp_Resto.Rows[x]["Prd_PrecioLista"] = producto.Prd_PLista.ToString();
                            }
                            //}
                        }
                    }
                }

                Session["Restos"] = dtTemp_Resto;
                rg1_Restos.DataSource = dtTemp_Resto;
                rg1_Restos.DataBind();
            }
            catch (Exception ex)
            {
                mensajeError(ex.Message);
            }


            TabName.Value = "tabAcuerdoEconomico";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "mantaintabs('" + TabName.Value + "')", true);
        }

        protected void chkMod_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chkbox = (sender as CheckBox);

                (chkbox.Parent.FindControl("cmbDia") as RadComboBox).Enabled = chkbox.Checked;
                (chkbox.Parent.FindControl("txtFrecuencia") as RadNumericTextBox).Enabled = chkbox.Checked;

            }
            catch (Exception ex)
            {
                mensajeError(ex.Message.ToString().Trim() + ", " + new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }







        //PRODUCTOS
        protected void cmbProducto_DataBound(object sender, EventArgs e)
        {
            RadComboBox comboBox = ((RadComboBox)sender);
            comboBox.Items.Insert(0, new RadComboBoxItem("-- Seleccionar --", "-1"));
            string id = ((RadNumericTextBox)comboBox.Parent.Parent.FindControl("txtProd")).Text;
            if (id != "")
            {
                comboBox.SelectedIndex = comboBox.FindItemIndexByValue(id);
            }
        }

        //CLIENTE
        protected void cargarClienteNuevo(int idCliente)
        {
            try
            {
                ddlClienteNom.Value = idCliente.ToString().Trim();
                ddlClienteNom.ReadOnly = true;
                ddlClienteNom.Enabled = false;
                txtClave.Text = "";
                txtContactoNom.Text = "";
                txtContactoPuesto.Text = "";
                txtContactoTel.Text = "";
                txtContactoMail.Text = "";
                txtCalle.Text = "";
                txtNo.Text = "";
                txtColonia.Text = "";
                txtMunicipio.Text = "";
                txtEstado.Text = "";
                ChkOrdCompra.Checked = false;
                chkFolio.Checked = false;
                ChkOrdReposicion.Checked = false;

                CHKFacKey.Checked = false;
                CHKRemision.Checked = false;
                CHKCopiaPed.Checked = false;


                lblFacturakey.Enabled = true;
                lblOrdenCompraCopias.Enabled = true;
                lblOrdenRepo.Enabled = true;

                lblremision.Enabled = true;
                lblCopia.Enabled = true;
                lblFolio.Enabled = true;

                TxtEOtro.Value = "";

                txtPagoCorreo.Text = "";
                txtPagoNombre.Text = "";
                txtPagoTelefono.Text = "";

                txtAlmacenCorreo.Text = "";
                txtAlmacenNombre.Text = "";
                txtAlmacenTelefono.Text = "";

                txtComprasCorreo.Text = "";
                txtComprasNombre.Text = "";
                txtComprasTelefono.Text = "";

                TxtMtoCorreo.Text = "";
                TxtMtoNombre.Text = "";
                TxtMtoTelefono.Text = "";


                txtContactoNom.Enabled = true;
                txtContactoPuesto.Enabled = true;
                txtContactoTel.Enabled = true;
                txtContactoMail.Enabled = true;
                /*
                txtCalle.Enabled = false;
                txtNo.Enabled = false;
                txtColonia.Enabled = false;
                txtMunicipio.Enabled = false;
                txtEstado.Enabled = false;
                */

                //ChkOrdCompra.Disabled = false;
                //chkFolio.Disabled = false;
                //ChkOrdReposicion.Disabled = false;

                //CHKFacKey.Disabled = false;
                //CHKRemision.Disabled = false;
                //CHKCopiaPed.Disabled = false;


                lblFacturakey.Enabled = true;
                lblOrdenCompraCopias.Enabled = true;
                lblOrdenRepo.Enabled = true;

                lblremision.Enabled = true;
                lblCopia.Enabled = true;
                lblFolio.Enabled = true;

                txtPagoCorreo.Enabled = true;
                txtPagoNombre.Enabled = true;
                txtPagoTelefono.Enabled = true;

                txtAlmacenCorreo.Enabled = true;
                txtAlmacenNombre.Enabled = true;
                txtAlmacenTelefono.Enabled = true;

                txtComprasCorreo.Enabled = true;
                txtComprasNombre.Enabled = true;
                txtComprasTelefono.Enabled = true;

                TxtMtoCorreo.Enabled = true;
                TxtMtoNombre.Enabled = true;
                TxtMtoTelefono.Enabled = true;



                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                PedidoVtaInst pedido = new PedidoVtaInst();
                List<PedidoVtaInst> lista = new List<CapaEntidad.PedidoVtaInst>();
                CN_CapPedidoVtaInst vtaInst = new CN_CapPedidoVtaInst();
                pedido.Id_Emp = sesion.Id_Emp;
                pedido.Id_Cd = sesion.Id_Cd_Ver;
                pedido.Id_Cte = idCliente;

                CN_CapPedido cn_capPedido = new CN_CapPedido();


                CargarCliente();

                pedido = new PedidoVtaInst();
                CN_CapPedidoVtaInst cn_capPedidoVI = new CN_CapPedidoVtaInst();

                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;
                pedido.Id_Acs = 0;
                pedido.Acs_Semana = 0;
                pedido.Acs_Anio = 0;

                if (txtIdCte.Value != "")
                    pedido.Id_Cte = Int32.Parse(txtIdCte.Value);

                GetListDet();
                DataTable dtTemp_Resto = this.dt_Resto;

                List<PedidoVtaInst> List2 = new List<PedidoVtaInst>();
                cn_capPedidoVI.ConsultarDet_Resto(pedido, ref List2, ref dtTemp_Resto, session.Emp_Cnx, Id_TG);


                string ConexionCentral = ConfigurationManager.AppSettings["strConnectionCentral"].ToString().Trim();
                ConvenioDet conv;
                ConvenioDet convdet;
                CN_Convenio cn_conv;
                List<string> Productos = new List<string>();

                List<ConvenioDet> lconveniosdet = new List<ConvenioDet>();
                ConvenioDet lconvdet = new ConvenioDet();


                if (dt.Rows.Count > 0)
                {
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        if (Convert.ToDouble(dt.Rows[x]["Prd_PrecioLista"]) == 0)
                        {
                            conv = new ConvenioDet();
                            convdet = new ConvenioDet();
                            cn_conv = new CN_Convenio();
                            conv.Id_Emp = session.Id_Emp;
                            conv.Id_Cd = session.Id_Cd_Ver;
                            conv.Id_Cte = Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1");
                            conv.Id_Prd = Convert.ToInt64(dt.Rows[x]["Id_Prd"]);
                            double PrecioIngresado = Convert.ToDouble(dt.Rows[x]["Prd_Precio"]);

                            if (Convert.ToInt64(dt.Rows[x]["Id_Prd"]) <= 999999999999)
                            {
                                //cn_conv.Convenio_ConsultaPrecio(conv, ref convdet, ConexionCentral);

                                //if (convdet != null && convdet.PCD_PrecioAAAEsp > 0)
                                //{
                                //    dt.Rows[x]["Prd_PrecioLista"] = convdet.PCD_PrecioAAAEsp;
                                //}
                                //else
                                //{
                                Producto producto = new Producto();
                                //obtener datos de producto
                                CN_CatProducto clsProducto = new CN_CatProducto();
                                producto.Id_Cte = Convert.ToInt32(Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1"));
                                int productoNuevo = 0;
                                clsProducto.ConsultaProductos(ref producto, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, Convert.ToInt64(dt.Rows[x]["Id_Prd"]), ref productoNuevo, 0);

                                if (0 < producto.Prd_PLista)
                                {
                                    dt.Rows[x]["Prd_PrecioLista"] = producto.Prd_PLista;
                                }
                                //}
                            }
                        }
                    }
                }


                if (dt_Resto.Rows.Count > 0)
                {
                    for (int x = 0; x < dt_Resto.Rows.Count; x++)
                    {
                        if (Convert.ToDouble(dt_Resto.Rows[x]["Prd_PrecioLista"]) == 0)
                        {
                            conv = new ConvenioDet();
                            convdet = new ConvenioDet();
                            cn_conv = new CN_Convenio();
                            conv.Id_Emp = session.Id_Emp;
                            conv.Id_Cd = session.Id_Cd_Ver;
                            conv.Id_Cte = Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1");
                            conv.Id_Prd = Convert.ToInt64(dt_Resto.Rows[x]["Id_Prd"]);
                            double PrecioIngresado = Convert.ToDouble(dt_Resto.Rows[x]["Prd_Precio"]);

                            if (Convert.ToInt64(dt_Resto.Rows[x]["Id_Prd"]) <= 999999999999)
                            {
                                //cn_conv.Convenio_ConsultaPrecio(conv, ref convdet, ConexionCentral);

                                //if (convdet != null && convdet.PCD_PrecioAAAEsp > 0)
                                //{
                                //    dt_Resto.Rows[x]["Prd_PrecioLista"] = convdet.PCD_PrecioAAAEsp.ToString();
                                //}
                                //else
                                //{
                                Producto producto = new Producto();
                                //obtener datos de producto
                                CN_CatProducto clsProducto = new CN_CatProducto();
                                producto.Id_Cte = Convert.ToInt32(Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1"));
                                int productoNuevo = 0;
                                clsProducto.ConsultaProductos(ref producto, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, Convert.ToInt64(dt_Resto.Rows[x]["Id_Prd"]), ref productoNuevo, 0);

                                if (0 < producto.Prd_PLista)
                                {
                                    dt_Resto.Rows[x]["Prd_PrecioLista"] = producto.Prd_PLista.ToString();
                                }
                                //}
                            }
                        }
                    }
                }


                Session["Prod"] = dt;
                Session["Restos"] = dt_Resto;

                rg1_Restos.DataSource = dtTemp_Resto;
                rg1_Restos.DataBind();
                Consultar_IVA_Cliente();
                calcularsubtotal();

            }
            catch (Exception ex)
            {
                mensajeError(ex.Message.ToString().Trim());
            }
        }

        //TERRITORIO
        protected void txtTerritorioNom_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (ddlTerritorioNom.SelectedItem.Value != "" && ddlTerritorioNom.SelectedItem.Value != "-1")
            {
                CN_CatTerritorios cn_terr = new CN_CatTerritorios();
                Territorios terr = new Territorios();
                terr.Id_Emp = session.Id_Emp;
                terr.Id_Cd = session.Id_Cd_Ver;
                terr.Id_Ter = Convert.ToInt32(ddlTerritorioNom.SelectedItem.Value);
                cn_terr.ConsultaTerritorios(ref terr, session.Emp_Cnx);
                txtRikNom.Value = terr.Rik_Nombre;
                txtIdRik.Value = terr.Id_Rik.ToString().Trim();
                txtIdTer.Value = ddlTerritorioNom.SelectedItem.Value.ToString().Trim();
            }
        }


        protected void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                string Id_prd = ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtProd")).Text;
                CN_CapPedidoVtaInst pedido_vta = new CN_CapPedidoVtaInst();
                int verificador = 0;
                pedido_vta.ConsultarAAAEspecial(Sesion.Id_Emp, Sesion.Id_Cd_Ver, double.Parse(txtIdCte.Value), Id_prd, ref verificador, Sesion.Emp_Cnx);

                if (verificador > 0)
                {
                    mensajeInfo("El producto cuenta con precio AAA especial" + ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtPrecio")).ClientID);
                }
                double cantidad = ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtCantidad")).Value.HasValue ? ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtCantidad")).Value.Value : 0;
                double precio = ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtPrecio")).Value.HasValue ? ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtPrecio")).Value.Value : 0;
                double importe = cantidad * precio;
                ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtImporte")).Text = importe.ToString("#,##0.00");
            }
            catch (Exception ex)
            {
                mensajeError(ex + ", " + new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                RadNumericTextBox rdtn = (RadNumericTextBox)sender;

                string cantidad = ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtCantidad")).Text;
                string precio = ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtPrecio")).Text;
                //string Asignado = ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtAsignar")).Text;
                int Prd_Cantidad = 0;
                double Prd_Precio = 0;

                if (cantidad != "")
                {
                    if (int.Parse(cantidad) <= 0)
                    {
                        mensajeInfo("La cantidad debe ser mayor a 0" + ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtCantidad")).ClientID);
                        return;
                    }
                }
                else
                {
                    mensajeInfo("La cantidad debe ser mayor a 0" + ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtCantidad")).ClientID);
                    rdtn.Value = 0;
                    return;
                }

                if (!string.IsNullOrEmpty(cantidad))
                    Prd_Cantidad = Convert.ToInt32(((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtCantidad")).Text);
                if (!string.IsNullOrEmpty(precio))
                    Prd_Precio = Convert.ToDouble(((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtPrecio")).Text);

                string Id_prd = ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtProd")).Text;
                ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtImporte")).DbValue = Prd_Cantidad * Prd_Precio;

                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                List<int> Actuales = new List<int>();
                CN_CatProducto catproducto = new CN_CatProducto();
                catproducto.ConsultaProducto_Disponible(Sesion.Id_Emp, Sesion.Id_Cd_Ver, Id_prd, ref Actuales, Sesion.Emp_Cnx);

                if (Actuales.Count > 0)
                {
                    if (Prd_Cantidad > Actuales[2])
                    {
                        mensajeInfo("Inventario disponible insuficiente, <br>Inventario final: " + Actuales[0] + " <br>Asignado: " + Actuales[1] + " <br>Disponible: " + Actuales[2] + ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtCantidad")).ClientID);
                        return;
                    }
                    else
                    {
                        ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtPrecio")).Focus();
                    }
                }
                else
                {
                    ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtPrecio")).Focus();
                }
                CN_CapPedidoVtaInst pedido_vta = new CN_CapPedidoVtaInst();
                int verificador = 0;
                if (!string.IsNullOrEmpty(txtIdCte.Value))
                    pedido_vta.ConsultarAAAEspecial(Sesion.Id_Emp, Sesion.Id_Cd_Ver, double.Parse(txtIdCte.Value), Id_prd, ref verificador, Sesion.Emp_Cnx);
                if (verificador > 0)
                {
                    mensajeInfo("El producto cuenta con precio AAA especial" + ((RadNumericTextBox)(sender as RadNumericTextBox).Parent.FindControl("txtPrecio")).ClientID);
                }
            }
            catch (Exception ex)
            {
                mensajeError(ex + new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void ImgBuscarDireccionEntrega_Click(object sender, ImageClickEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CallFucDireccion", "AbrirBuscarDireccionEntrega()", true);
        }
        #endregion
        #region Funciones

        private bool TryAcquireSaveLock(out string cacheKey)
        {
            cacheKey = $"{SaveLockPrefix}{Session.SessionID}";
            // Evita doble ejecución simultánea por sesión
            var cache = HttpRuntime.Cache;
            var added = cache.Add(
                cacheKey,
                true,
                null,
                DateTime.UtcNow.AddSeconds(SaveLockSeconds),
                Cache.NoSlidingExpiration,
                CacheItemPriority.NotRemovable,
                null
            );
            // Si "added" es null, la clave no existía y se añadió: lock adquirido.
            return added == null;
        }

        private void ReleaseSaveLock(string cacheKey)
        {
            if (!string.IsNullOrEmpty(cacheKey))
                HttpRuntime.Cache.Remove(cacheKey);
        }

        private string CalculatePedidoContentHash(DataTable dt, DataTable dtRestos)
        {
            var sb = new StringBuilder();

            // Campos relevantes que identifican el pedido actual (cabecera)
            sb.Append(session?.Id_Emp ?? 0).Append('|')
              .Append(session?.Id_Cd_Ver ?? 0).Append('|')
              .Append(txtIdCte.Value ?? "").Append('|')
              .Append(txtIdTer.Value ?? "").Append('|')
              .Append(txtIdRik.Value ?? "").Append('|')
              .Append(TxtPed_ReqAcys.Text ?? "").Append('|')
              .Append(rdFechaF.Text ?? "").Append('|')
              .Append(rdFechaE.Text ?? "").Append('|')
              .Append(txtSubtotal.Value ?? "").Append('|')
              .Append(txtTotal.Value ?? "");

            // Detalle ordenado determinísticamente
            var items = new List<string>();
            void add(DataTable t)
            {
                if (t == null) return;
                foreach (DataRow r in t.Rows)
                {
                    var id = r.Table.Columns.Contains("Id_Prd") && r["Id_Prd"] != DBNull.Value ? r["Id_Prd"].ToString().Trim() : "0";
                    var cant = r.Table.Columns.Contains("Prd_Cantidad") && r["Prd_Cantidad"] != DBNull.Value ? r["Prd_Cantidad"].ToString().Trim() : "0";
                    var precio = r.Table.Columns.Contains("Prd_Precio") && r["Prd_Precio"] != DBNull.Value ? Convert.ToDouble(r["Prd_Precio"]).ToString("0.000", CultureInfo.InvariantCulture) : "0.000";
                    items.Add($"{id}:{cant}:{precio}");
                }
            }
            add(dt);
            add(dtRestos);
            items.Sort(StringComparer.Ordinal);
            foreach (var it in items)
                sb.Append('|').Append(it);

            using (var sha = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(sb.ToString());
                var hash = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        // Helper para leer/escribir huella y tiempo en Session con nombres claros
        private string GetLastPedidoHash() => Session["LAST_PEDIDO_HASH"] as string;
        private DateTime GetLastPedidoHashTime()
        {
            var o = Session["LAST_PEDIDO_HASH_TIME"];
            return o is DateTime dt ? dt : DateTime.MinValue;
        }
        private void SetLastPedidoHash(string hash)
        {
            Session["LAST_PEDIDO_HASH"] = hash;
            Session["LAST_PEDIDO_HASH_TIME"] = DateTime.UtcNow;
        }
        private void ClearLastPedidoHash()
        {
            Session["LAST_PEDIDO_HASH"] = null;
            Session["LAST_PEDIDO_HASH_TIME"] = null;
        }

        // Para solicitudes (clave separada)
        private string GetLastSolicitudHash() => Session["LAST_PEDIDO_SOL_HASH"] as string;
        private DateTime GetLastSolicitudHashTime()
        {
            var o = Session["LAST_PEDIDO_SOL_HASH_TIME"];
            return o is DateTime dt ? dt : DateTime.MinValue;
        }
        private void SetLastSolicitudHash(string hash)
        {
            Session["LAST_PEDIDO_SOL_HASH"] = hash;
            Session["LAST_PEDIDO_SOL_HASH_TIME"] = DateTime.UtcNow;
        }
        private void ClearLastSolicitudHash()
        {
            Session["LAST_PEDIDO_SOL_HASH"] = null;
            Session["LAST_PEDIDO_SOL_HASH_TIME"] = null;
        }

        private void PreGuardar()
        {
            try
            {
                if (Request.QueryString["OrdenCompra"] != null)
                {
                    if (Convert.ToBoolean(Request.QueryString["OrdenCompra"].ToString().Trim()))
                    {
                        ValidateInputData();
                    }
                }
                int productoNuevo = 0;
                DataTable dt = (DataTable)Session["Prod"];
                DataTable dt_Resto = (DataTable)Session["Restos"];
                string mensajestr = "";

                if (Request.QueryString["OrdenCompra"] != null)
                {
                    if (Convert.ToBoolean(Request.QueryString["OrdenCompra"].ToString().Trim()))
                    {
                        if (TxtPed_ReqAcys.Text.Trim() == "")
                        {
                            if (mensajestr == "")
                            {
                                mensajestr = "Favor de  capturar la Orden de compra.";
                            }
                            else
                            {
                                mensajestr = mensajestr + "</br>" + "capturar la Orden de compra.";
                            }
                        }
                    }
                }

                if (txtContactoNom.Text == "" || txtContactoPuesto.Text == "" || txtContactoMail.Text == "" || txtContactoTel.Text == "")
                {
                    if (mensajestr == "")
                    {
                        mensajestr = "Favor de  capturar la Información de la sección de:</br> Contacto.";
                    }
                    else
                    {
                        mensajestr = mensajestr + "</br>" + "Contacto.";
                    }
                }

                if (txtCalle.Text == "" || txtNo.Text == "" || txtCp.Text == "" || txtColonia.Text == "" || txtMunicipio.Text == "")
                {
                    if (mensajestr == "")
                    {
                        mensajestr = "Favor de capturar la Información de la sección de:</br> Dirección de entrega producto.";
                    }
                    else
                    {
                        mensajestr = mensajestr + "</br>" + "Dirección de entrega producto.";
                    }
                }

                if (txtPagoNombre.Text == "" || txtPagoTelefono.Text == "" || txtPagoCorreo.Text == "" || txtComprasNombre.Text == "" || txtComprasTelefono.Text == "" || txtComprasCorreo.Text == "")
                {
                    if (mensajestr == "")
                    {
                        mensajestr = "Favor de  capturar la Información de la sección de:</br>   Contactos de cliente.";
                    }
                    else
                    {
                        mensajestr = mensajestr + "</br>" + "Contactos de cliente.";
                    }
                }

                if (ChkOrdCompra.Checked == false && ChkOrdReposicion.Checked == false && chkFolio.Checked == false && TxtEOtro.Value == "" && CHKFacKey.Checked == false
                && CHKRemision.Checked == false
                && CHKCopiaPed.Checked == false)
                {

                    if (mensajestr == "")
                    {
                        mensajestr = "Favor de  capturar la Información de la sección de:</br>   Documentación requerida para entrega.";
                    }
                    else
                    {
                        mensajestr = mensajestr + "</br>" + " Documentación requerida para entrega.";
                    }
                }

                if (ChkOrdCompra.Checked == true && TxtPed_ReqAcys.Text.Trim() == "")
                {
                    if (mensajestr == "")
                    {
                        mensajestr = "Favor de  capturar la Orden de compra.";
                    }
                    else
                    {
                        mensajestr = mensajestr + "</br>" + "capturar la Orden de compra.";
                    }
                }



                if (dt.Rows.Count == 0 && dt_Resto.Rows.Count == 0)
                {
                    if (mensajestr == "")
                    {
                        mensajestr = "Se requiere que se capture como mínimo un producto.";
                    }
                    else
                    {
                        mensajestr = mensajestr + "</br>" + "Se requiere que se capture como mínimo un producto.";
                    }
                }

                if (this.rdFechaF.Text == "")
                {
                    if (mensajestr == "")
                    {
                        mensajestr = "Se requiere que se capture la fecha facturación.";
                    }
                    else
                    {
                        mensajestr = mensajestr + "</br>" + "Se requiere que se capture la fecha facturación.";
                    }
                }

                if (this.rdFechaE.Text == "")
                {

                    if (mensajestr == "")
                    {
                        mensajestr = "Se requiere que se capture la fecha compromiso de entrega.";
                    }
                    else
                    {
                        mensajestr = mensajestr + "</br>" + "Se requiere que se capture la fecha compromiso de entrega.";
                    }
                }

                if (txtIdTer.Value.ToString() == "-1")
                {
                    if (mensajestr == "")
                    {
                        mensajestr = "Favor de  verificar el territorio.";
                    }
                    else
                    {
                        mensajestr = mensajestr + "</br>" + "Favor de  verificar el territorio.";
                    }
                }



                double importe = txtSubtotal.Value != "" ? Convert.ToDouble(txtSubtotal.Value) : 0;
                if (importe == 0)
                {
                    if (mensajestr == "")
                    {
                        mensajestr = "Favor de revisar la captura de los productos de la pestaña de:</br> Detalle.";
                    }
                    else
                    {
                        mensajestr = mensajestr + "</br>" + "Contacto.";
                    }
                }


                if (mensajestr != "")
                {
                    mensajeInfo(mensajestr);
                    return;
                }

                string produObsoleto = "";
                string produObsoletoRestos = "";
                //foreach (DataRow dataR in dt.Rows)
                //{
                //    if (dt.Select("Id_Prd = " + dataR["Id_Prd"].ToString().Trim()).Length > 1)
                //    {
                //        mensaje("El producto " + dataR["Id_Prd"].ToString().Trim() + " no puede ser captado mas de una vez en el mismo pedido.");
                //        return;
                //    }
                //    if (dt_Resto.Select("Id_Prd = " + dataR["Id_Prd"].ToString().Trim()).Length > 1)
                //    {
                //        mensaje("El producto " + dataR["Id_Prd"].ToString().Trim() + " no puede ser captado mas de una vez en el mismo pedido.");
                //        return;
                //    }

                //    CN_CatProducto cn_catproducto = new CN_CatProducto();

                //    if (string.IsNullOrEmpty(txtClave.Text))
                //    {
                //        productoNuevo = 1;
                //    }
                //    Producto pr = new Producto();

                //    pr.Id_Cte = !string.IsNullOrEmpty(txtIdCte.Value) ? Convert.ToInt32(txtIdCte.Value) : 0;
                //    try
                //    {
                //        cn_catproducto.ConsultaProductos(ref pr, session.Emp_Cnx, session.Id_Emp, session.Id_Cd, Convert.ToInt64(dataR["Id_Prd"].ToString().Trim()), ref productoNuevo, 2);
                //    }
                //    catch (Exception)
                //    {
                //        if (produObsoleto == "")
                //        {
                //            produObsoleto = dataR["Id_Prd"].ToString().Trim();
                //        }
                //        else
                //        {
                //            produObsoleto = produObsoleto + ", " + dataR["Id_Prd"].ToString().Trim();
                //        }
                //    }
                //}

                //foreach (DataRow dataR in dt_Resto.Rows)
                //{
                //    if (dt.Select("Id_Prd = " + dataR["Id_Prd"].ToString().Trim()).Length > 1)
                //    {
                //        mensaje("El producto " + dataR["Id_Prd"].ToString().Trim() + " no puede ser captado mas de una vez en el mismo pedido.");
                //        return;
                //    }
                //    if (dt_Resto.Select("Id_Prd = " + dataR["Id_Prd"].ToString().Trim()).Length > 1)
                //    {
                //        mensaje("El producto " + dataR["Id_Prd"].ToString().Trim() + " no puede ser captado mas de una vez en el mismo pedido.");
                //        return;
                //    }

                //    CN_CatProducto cn_catproducto = new CN_CatProducto();

                //    if (string.IsNullOrEmpty(txtClave.Text.ToString()))
                //    {
                //        productoNuevo = 1;
                //    }

                //    Producto pr = new Producto();

                //    pr.Id_Cte = !string.IsNullOrEmpty(txtIdCte.Value) ? Convert.ToInt32(txtIdCte.Value) : 0;
                //    try
                //    {
                //        cn_catproducto.ConsultaProductos(ref pr, session.Emp_Cnx, session.Id_Emp, session.Id_Cd, Convert.ToInt64(dataR["Id_Prd"].ToString().Trim()), ref productoNuevo, 2);
                //    }
                //    catch (Exception)
                //    {
                //        if (produObsoletoRestos == "")
                //        {
                //            produObsoletoRestos = dataR["Id_Prd"].ToString().Trim();
                //        }
                //        else
                //        {
                //            produObsoletoRestos = produObsoletoRestos + ", " + dataR["Id_Prd"].ToString().Trim();
                //        }

                //    }
                //}

                //string mensajeObsoleto = "";
                //if (produObsoleto != "" || produObsoletoRestos != "")
                //{
                //    if (produObsoleto != "")
                //    {
                //        mensajeObsoleto = "Los siguientes Producto(s) de venta instalada esta(n) obsoleto(s) y no tiene(n) Existencia(s)" + produObsoleto + "</br>";
                //    }

                //    if (produObsoletoRestos != "")
                //    {
                //        if (mensajeObsoleto == "")
                //        {
                //            mensajeObsoleto = "Los siguientes Producto(s) de venta nueva y/o esporadica esta(n) obsoleto(s) y no tiene(n) Existencia(s)" + produObsoletoRestos + "</br>";
                //        }
                //        else
                //        {
                //            mensajeObsoleto = mensajeObsoleto + "Los siguientes Producto(s) de venta nueva y/o esporadica esta(n) obsoleto(s) y no tiene(n) Existencia(s)" + produObsoletoRestos + "</br>";
                //        }
                //    }
                //    mensaje(mensajeObsoleto);
                //    return;
                //}
                Session["dtPedidoVI" + Session.SessionID] = dt;

                foreach (DataRow dr in dt.Rows)
                {

                    if (dr["Ped_Asignar"].ToString().Trim() == "")
                        dr["Ped_Asignar"] = 0;

                    if (Convert.ToInt32(dr["Ped_Asignar"]) > 0)
                    {
                        mensajeInfo("Este pedido se encuentra asignado, si desea realizar cambios, favor de desasignar el pedido.");
                        return;
                    }
                    CN_CatProducto cn_catproducto = new CN_CatProducto();
                    Producto pr = new Producto();
                    List<int> actuales = new List<int>();
                    cn_catproducto.ConsultaProducto_Disponible(session.Id_Emp, session.Id_Cd_Ver, dr["Id_Prd"].ToString().Trim(), ref actuales, session.Emp_Cnx);

                    if (Request.QueryString["IdAutorizacion"] != null)
                    {
                        if ((Convert.ToInt32(dr["Prd_Cantidad"]) - (Convert.ToInt32(dr["Ped_Asignar"] == DBNull.Value ? 0 : dr["Ped_Asignar"]))) > actuales[2])
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "inventario", "AbrirVentana_InvIns('" + rdFechaF.Text.ToString().Trim().Replace("/", "") + "', '" + this.TxtPed_ReqAcys.Text + "', '" + txtClave.Text + "')", true);

                            return;
                        }
                    }
                    else
                    {
                        if (!ValidarCredito())
                        {
                            if ((Convert.ToInt32(dr["Prd_Cantidad"]) - (Convert.ToInt32(dr["Ped_Asignar"] == DBNull.Value ? 0 : dr["Ped_Asignar"]))) > actuales[2])
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "inventario", "AbrirVentana_InvIns('" + rdFechaF.Text.ToString().Trim().Replace("/", "") + "', '" + this.TxtPed_ReqAcys.Text + "', '" + txtClave.Text + "')", true);

                                return;
                            }
                        }
                    }
                }

                foreach (DataRow dr in dt_Resto.Rows)
                {
                    if (dr["Ped_Asignar"].ToString().Trim() == "")
                        dr["Ped_Asignar"] = 0;

                    if (Convert.ToInt32(dr["Ped_Asignar"]) > 0)
                    {
                        mensajeInfo("Este pedido se encuentra asignado, si desea realizar cambios, favor de desasignar el pedido.");
                        return;
                    }
                    CN_CatProducto cn_catproducto = new CN_CatProducto();
                    Producto pr = new Producto();
                    List<int> actuales = new List<int>();
                    cn_catproducto.ConsultaProducto_Disponible(session.Id_Emp, session.Id_Cd_Ver, dr["Id_Prd"].ToString().Trim(), ref actuales, session.Emp_Cnx);

                    if (Request.QueryString["IdAutorizacion"] != null)
                    {
                        if ((Convert.ToInt32(dr["Prd_Cantidad"]) - (Convert.ToInt32(dr["Ped_Asignar"] == DBNull.Value ? 0 : dr["Ped_Asignar"]))) > actuales[2])
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "inventario", "AbrirVentana_InvIns('" + rdFechaF.Text.ToString().Trim().Replace("/", "") + "', '" + this.TxtPed_ReqAcys.Text + "', '" + txtClave.Text + "')", true);

                            return;
                        }
                    }
                    else
                    {
                        if (!ValidarCredito())
                        {
                            if ((Convert.ToInt32(dr["Prd_Cantidad"]) - (Convert.ToInt32(dr["Ped_Asignar"] == DBNull.Value ? 0 : dr["Ped_Asignar"]))) > actuales[2])
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "inventario", "AbrirVentana_InvIns('" + rdFechaF.Text.ToString().Trim().Replace("/", "") + "', '" + this.TxtPed_ReqAcys.Text + "', '" + txtClave.Text + "')", true);

                                return;
                            }
                        }
                    }
                }


                if (Request.QueryString["IdAutorizacion"] != null)
                {
                    HF_ID.Value = "";
                    Guardar();
                }
                else
                {
                    if (ValidarCredito())
                    {
                        MensajeSolicitud("Este cliente tiene el crédito suspendido. </br>  ¿Desea Solicitar Autorización del pedido?");
                        return;
                    }
                    else
                    {
                        Guardar();
                    }
                }
            }
            catch (Exception ex)
            {
                mensajeError(ex.Message);
            }


        }

        public bool ValidarCredito()
        {
            string permiso = string.Concat(ConfigurationManager.AppSettings["RequiereAutorizacionPedido"].ToString());


            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            PedidoVtaInst pedido = new PedidoVtaInst();
            CN_CapPedidoVtaInst vtaInst = new CN_CapPedidoVtaInst();
            pedido.Id_Emp = sesion.Id_Emp;
            pedido.Id_Cd = sesion.Id_Cd_Ver;
            pedido.Id_Cte = int.Parse(txtIdCte.Value);
            vtaInst.Cliente_credito(ref pedido, sesion.Emp_Cnx);
            bool CreditoSusp = Convert.ToBoolean(pedido.Cte_Credito);

            if (CreditoSusp && permiso.ToUpper() != "N")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private int ValidarCambios()
        {
            try
            {
                DataTable dt = (DataTable)Session["Prod"];
                DataTable dt_Resto = (DataTable)Session["Restos"];
                int Respuesta = 1;

                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["Ped_Asignar"].ToString().Trim() == "")
                    {
                        dr["Ped_Asignar"] = 0;
                        Respuesta = 1;
                    }
                    if (Convert.ToInt32(dr["Ped_Asignar"]) > 0)
                    {
                        mensajeInfo("Este pedido se encuentra asignado, si desea realizar cambios, favor de desasignar el pedido.");
                        Respuesta = 0;
                    }
                }

                foreach (DataRow dr in dt_Resto.Rows)
                {
                    if (dr["Ped_Asignar"].ToString().Trim() == "")
                    {
                        dr["Ped_Asignar"] = 0;
                        Respuesta = 1;
                    }

                    if (Convert.ToInt32(dr["Ped_Asignar"]) > 0)
                    {
                        mensajeInfo("Este pedido se encuentra asignado, si desea realizar cambios, favor de desasignar el pedido.");
                        Respuesta = 0;
                    }
                }
                return Respuesta;
            }
            catch (Exception ex)
            {
                mensajeError(ex.Message);
                return 0;
            }
        }


        private void Inicializar()
        {
            divupload.Visible = false;
            lblFacturakey.Text = "0";
            lblOrdenCompraCopias.Text = "0";
            lblOrdenRepo.Text = "0";

            lblremision.Text = "0";
            lblCopia.Text = "0";
            lblFolio.Text = "0";
            CN_CatCentroDistribucion cd = new CN_CatCentroDistribucion();
            CentroDistribucion centroDistribucion = new CentroDistribucion();
            ListaModifcarEstatus();
            double iva = iva_cd;
            cd.ConsultarIva(session.Id_Emp, session.Id_Cd_Ver, ref iva, session.Emp_Cnx);
            iva_cd = iva;

            LlenarComboUsoCfdi();
            cargarComboClientesAcuerdo();

            if (Request.QueryString["IdAutorizacion"] != null)
            {
                txtFolio.Text = Request.QueryString["IdAutorizacion"].ToString().Trim();
                CargarPedidoAutorizado();
                if (Convert.ToBoolean(Request.QueryString["OrdenCompra"].ToString().Trim()))
                {
                    divupload.Visible = true;
                }
            }
            else
            {

                if (Request.QueryString["OrdenCompra"] != null)
                {
                    if (Convert.ToBoolean(Request.QueryString["OrdenCompra"].ToString().Trim()))
                    {
                        txtFolio.Value = MaximoId();
                        CargarAcuerdo();
                        if ((txtClave.Text) == "")
                            productoNuevo = 1;
                        divupload.Visible = true;
                    }

                    if (Request.QueryString["IdDireccion"] != null)
                    {

                        CN_CatCliente clsCliente = new CN_CatCliente();
                        ClienteDirEntrega cliente = new ClienteDirEntrega();
                        Sesion session2 = (Sesion)Session["Sesion" + Session.SessionID];
                        session2 = (Sesion)Session["Sesion" + Session.SessionID];
                        cliente.Id_Emp = session2.Id_Emp;
                        cliente.Id_Cd = session2.Id_Cd_Ver;
                        if (Convert.ToInt32(Request.QueryString["IdDireccion"]) != -1)
                        {
                            cliente.Id_CteDirEntrega = Int32.Parse(Request.QueryString["IdDireccion"].ToString());
                        }
                        else
                        {
                            cliente.Id_CteDirEntrega = 0;
                        }

                        cliente.Id_Cte = Int32.Parse(txtIdCte.Value);
                        clsCliente.ConsultaClienteDirEntrega(cliente, session2.Emp_Cnx);
                        txtCalle.Text = cliente.Cte_Calle;
                        txtNo.Text = cliente.Cte_Numero;
                        txtCp.Text = cliente.Cte_Cp.Trim();
                        txtColonia.Text = cliente.Cte_Colonia;
                        txtMunicipio.Text = cliente.Cte_Municipio;
                        txtEstado.Text = cliente.Cte_Estado;

                    }
                }
                if (Request.QueryString["idPNuevo"] != null)
                {
                    txtFolio.Value = MaximoId();
                    cargarClienteNuevo(Convert.ToInt32(Request.QueryString["idPNuevo"].ToString().Trim()));
                }
                else if (Request.QueryString["idP"] != null)
                {
                    txtFolio.Value = Request.QueryString["idP"].ToString().Trim();
                    CargarPedido();
                }
                else
                {
                    if (Request.QueryString["IdVI"] != null)
                    {
                        if (Session["PedidoCaptado" + Session.SessionID] != null)
                            txtFolio.Value = Session["PedidoCaptado" + Session.SessionID].ToString().Trim();
                        CargarPedido();
                        Session["PedidoCaptado" + Session.SessionID] = null;
                    }
                    else
                        if (Request.QueryString["IdPeInt"] != null)
                    {
                        txtFolio.Value = Request.QueryString["IdPeInt"].ToString().Trim();
                        this.CargarPedidoInternet(Convert.ToInt32(Request.QueryString["IdPeInt"]), 0);
                    }
                    else
                    {
                        txtFolio.Value = MaximoId();
                        if (Request.QueryString["id"] != "-1")
                        {
                            txtClave.Value = Request.QueryString["id"];
                            CargarAcuerdo();
                            if ((txtClave.Text) == "")
                                productoNuevo = 1;
                        }

                        if (Request.QueryString["IdDireccion"] != null)
                        {
                            CN_CatCliente clsCliente = new CN_CatCliente();
                            ClienteDirEntrega cliente = new ClienteDirEntrega();
                            Sesion session2 = (Sesion)Session["Sesion" + Session.SessionID];
                            session2 = (Sesion)Session["Sesion" + Session.SessionID];
                            cliente.Id_Emp = session2.Id_Emp;
                            cliente.Id_Cd = session2.Id_Cd_Ver;
                            if (Convert.ToInt32(Request.QueryString["IdDireccion"]) != -1)
                            {
                                cliente.Id_CteDirEntrega = Int32.Parse(Request.QueryString["IdDireccion"].ToString());
                            }
                            else
                            {
                                cliente.Id_CteDirEntrega = 0;
                            }
                            cliente.Id_Cte = Int32.Parse(txtIdCte.Value);
                            clsCliente.ConsultaClienteDirEntrega(cliente, session2.Emp_Cnx);
                            txtCalle.Text = cliente.Cte_Calle;
                            txtNo.Text = cliente.Cte_Numero;
                            txtCp.Text = cliente.Cte_Cp.Trim();
                            txtColonia.Text = cliente.Cte_Colonia;
                            txtMunicipio.Text = cliente.Cte_Municipio;
                            txtEstado.Text = cliente.Cte_Estado;
                        }
                    }
                }
            }

            int intIdCte = Convert.ToInt32(txtIdCte.Value.ToString().Trim());
            ConsultarClienteFechaEntrega(session.Emp_Cnx, intIdCte, session.Id_Cd);

            bool _PermisoGuardar = bool.Parse(Request.QueryString["PermisoGuardar"].ToString().Trim());
            bool _PermisoModificar = bool.Parse(Request.QueryString["PermisoModificar"].ToString().Trim());
        }

        private string Nombre(int p)
        {
            switch (p)
            {
                case 1: return "Ene.";
                case 2: return "Feb.";
                case 3: return "Mar.";
                case 4: return "Abr.";
                case 5: return "May.";
                case 6: return "Jun.";
                case 7: return "Jul.";
                case 8: return "Ago.";
                case 9: return "Sep.";
                case 10: return "Oct.";
                case 11: return "Nov.";
                case 12: return "Dic.";
                default: return "";

            }
        }
        private void CargarAcuerdo()
        {
            try
            {
                int verificador = 0;

                DateTime fechaf = default(DateTime);
                Funciones funcion = new Funciones();

                if (Request.QueryString["Id"] != "" && Request.QueryString["Id"] != null)
                {
                    PedidoVtaInst pedido = new PedidoVtaInst();
                    pedido.Id_Emp = session.Id_Emp;
                    pedido.Id_Cd = session.Id_Cd_Ver;
                    pedido.Id_Acs = Convert.ToInt32(Request.QueryString["Id"]);
                    Clientes cc = new Clientes();
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    sesion = (Sesion)Session["Sesion" + Session.SessionID];

                    CN_CapPedidoVtaInst cn_capPedidoVI = new CN_CapPedidoVtaInst();
                    cn_capPedidoVI.Consultar2(ref pedido, session.Emp_Cnx, ref verificador, ref cc);

                    if (verificador == 1)
                    {

                        ddlClienteNom.Value = pedido.Id_Cte.ToString().Trim();
                        txtIdCte.Value = pedido.Id_Cte.ToString().Trim();
                        txtRikNom.Value = pedido.Rik_Nombre.Trim();
                        txtIdRik.Value = pedido.Id_Rik.ToString().Trim();

                        ddlTerritorioNom.Value = pedido.Ter_Nombre;
                        txtIdTer.Value = pedido.Id_Ter.ToString().Trim();

                        txtContactoNom.Value = pedido.Acs_Contacto.Trim();
                        txtContactoTel.Value = pedido.Acs_Telefono.Trim();
                        txtContactoMail.Value = pedido.Acs_email.Trim();
                        txtContactoPuesto.Value = pedido.Acs_Puesto.Trim();

                        txtCalle.Value = cc.Cte_Calle.Trim();
                        txtNo.Value = cc.Cte_Numero.ToString().Trim().Replace(" ", "");
                        txtCp.Value = cc.Cte_Cp.Trim();
                        txtMunicipio.Value = cc.Cte_Municipio.Trim();
                        txtEstado.Value = cc.Cte_Estado.Trim();
                        txtColonia.Value = cc.Cte_Colonia.Trim();


                        txtContactoNom.Value = pedido.Acs_PedidoEncargadoEnviar.Trim();
                        txtContactoTel.Value = pedido.Acs_PedidoTelefono.Trim();
                        txtContactoMail.Value = pedido.Acs_PedidoEmail.Trim();
                        txtContactoPuesto.Value = pedido.Acs_PedidoPuesto.Trim();
                        ChkOrdCompra.Checked = pedido.Acs_ReqOrden;
                        ChkOrdReposicion.Checked = pedido.Acs_ReqDocReposicion;
                        chkFolio.Checked = pedido.Acs_ReqDocFolio;

                        CHKFacKey.Checked = pedido.Acs_ReqFacturaKey;
                        CHKRemision.Checked = pedido.ACS_ReqRemision;
                        CHKCopiaPed.Checked = pedido.Acs_ReqCopia;

                        lblFacturakey.Text = pedido.Acs_ReqFacturaKeyCop.ToString().Trim();
                        lblOrdenCompraCopias.Text = pedido.Acs_ReqOrdencop.ToString().Trim();
                        lblOrdenRepo.Text = pedido.Acs_ReqDocReposicioncop.ToString().Trim();
                        lblremision.Text = pedido.ACS_ReqRemisionCop.ToString().Trim();
                        lblCopia.Text = pedido.Acs_ReqCopiaCop.ToString().Trim();
                        lblFolio.Text = pedido.Acs_ReqDocFoliocop.ToString().Trim();

                        TxtEOtro.Value = pedido.Acs_ReqDocOtro;

                        //Llena la informacion de compras

                        txtComprasNombre.Value = pedido.Acs_Contacto2;
                        if (pedido.Acs_Telefono2 == "0")
                        {
                            txtComprasTelefono.Value = "";
                        }
                        else
                        {
                            txtComprasTelefono.Value = pedido.Acs_Telefono2;
                        }
                        txtComprasCorreo.Value = pedido.Acs_Email2;

                        //Llena la información de almacen
                        txtAlmacenNombre.Value = pedido.Acs_Contacto3;
                        if (pedido.Acs_Telefono3 == "0")
                        {
                            txtAlmacenTelefono.Value = "";
                        }
                        else
                        {
                            txtAlmacenTelefono.Value = pedido.Acs_Telefono3;
                        }
                        txtAlmacenCorreo.Value = pedido.Acs_Email3;


                        //Llena la información de Mantenimiento
                        TxtMtoNombre.Value = pedido.Acs_Contacto4;
                        if (pedido.Acs_Telefono4 == "0")
                        {
                            TxtMtoTelefono.Value = "";
                        }
                        else
                        {
                            TxtMtoTelefono.Value = pedido.Acs_Telefono4;
                        }
                        TxtMtoCorreo.Value = pedido.Acs_Email4;


                        //Llena la información de Pagos
                        txtPagoNombre.Value = pedido.Acs_Contacto5;
                        if (pedido.Acs_Telefono5 == "0")
                        {
                            txtPagoTelefono.Value = "";
                        }
                        else
                        {
                            txtPagoTelefono.Value = pedido.Acs_Telefono5;
                        }
                        txtPagoCorreo.Value = pedido.Acs_Email5;

                        ddUsoCfdi.Value = pedido.UsoCFDI?.ToString().Trim();


                        txtContactoNom.Enabled = true;
                        txtContactoTel.Enabled = true;
                        txtContactoMail.Enabled = true;
                        txtContactoPuesto.Enabled = true;

                        txtPagoCorreo.Enabled = true;
                        txtPagoNombre.Enabled = true;
                        txtPagoTelefono.Enabled = true;

                        txtAlmacenCorreo.Enabled = true;
                        txtAlmacenNombre.Enabled = true;
                        txtAlmacenTelefono.Enabled = true;

                        TxtMtoCorreo.Enabled = true;
                        TxtMtoNombre.Enabled = true;
                        TxtMtoTelefono.Enabled = true;

                        txtComprasCorreo.Enabled = true;
                        txtComprasNombre.Enabled = true;
                        txtComprasTelefono.Enabled = true;

                        txtIdCte.Disabled = true;
                        ddlClienteNom.Enabled = false;
                        txtIdTer.Disabled = true;
                        ddlTerritorioNom.Enabled = false;

                        txtIdRik.Disabled = true;
                        txtRikNom.Disabled = true;

                        //ChkOrdCompra.Disabled = true;
                        //chkFolio.Disabled = true;
                        //ChkOrdReposicion.Disabled = true;

                        //CHKFacKey.Disabled = true;
                        //CHKRemision.Disabled = true;
                        //CHKCopiaPed.Disabled = true;

                        //lblFacturakey.Enabled = false;
                        //lblOrdenCompraCopias.Enabled = false;
                        //lblOrdenRepo.Enabled = false;

                        //lblremision.Enabled = false;
                        //lblCopia.Enabled = false;
                        //lblFolio.Enabled = false;


                    }
                    else
                    {
                        mensajeInfo("No se encontro");
                    }

                    pedido.Id_Emp = session.Id_Emp;
                    pedido.Id_Cd = session.Id_Cd_Ver;
                    pedido.Id_Acs = Convert.ToInt32(Request.QueryString["Id"]);
                    pedido.Acs_Anio = Convert.ToInt32(Request.QueryString["Anio"]);
                    if (Session["Semana"] != null)
                    {
                        pedido.Acs_Semana = Convert.ToInt32(Session["Semana"]);
                    }
                    else
                    {
                        pedido.Acs_Semana = Convert.ToInt32(Request.QueryString["Semana"]);
                    }
                    dt = null;

                    GetListDet();
                    DataTable dtTemp = dt;
                    List<PedidoVtaInst> List = new List<PedidoVtaInst>();
                    string idTGStr = Request.QueryString["Id_TG"];
                    int? idTGNullable = 0;
                    int idTG = 0;
                    if (idTGStr != null)
                    {
                        if (int.TryParse(idTGStr, out idTG))
                        {
                            idTGNullable = idTG;
                        }
                    }
                    cn_capPedidoVI.ConsultarDet2(pedido, ref List, ref dtTemp, session.Emp_Cnx, idTGNullable);

                    DataTable dtTemp_Resto = this.dt_Resto;
                    List<PedidoVtaInst> List2 = new List<PedidoVtaInst>();
                    cn_capPedidoVI.ConsultarDet_Resto(pedido, ref List2, ref dtTemp_Resto, session.Emp_Cnx, Id_TG);



                    dt = dtTemp;
                    dt_Resto = dtTemp_Resto;

                    CN_CatProducto cn_catproducto = new CN_CatProducto();
                    Producto pr;
                    foreach (DataRow dr in dt.Rows)
                    {
                        pr = new Producto();
                        pr.Id_Emp = session.Id_Emp;
                        pr.Id_Cd = session.Id_Cd_Ver;
                        pr.Id_Prd = Convert.ToInt64(dr["Id_prd"]);

                        cn_catproducto.ConsultarVentas(ref pr, Convert.ToInt32(txtIdCte.Value), session.Emp_Cnx);

                        dr["mes1"] = pr.ventaMes[0].ToString().Trim();
                        dr["mes2"] = pr.ventaMes[1].ToString().Trim();
                        dr["mes3"] = pr.ventaMes[2].ToString().Trim();
                    }

                    foreach (DataRow dr in dt_Resto.Rows)
                    {
                        pr = new Producto();
                        pr.Id_Emp = session.Id_Emp;
                        pr.Id_Cd = session.Id_Cd_Ver;
                        pr.Id_Prd = Convert.ToInt64(dr["Id_prd"]);

                        cn_catproducto.ConsultarVentas(ref pr, Convert.ToInt32(txtIdCte.Value), session.Emp_Cnx);

                        dr["mes1"] = pr.ventaMes[0].ToString().Trim();
                        dr["mes2"] = pr.ventaMes[1].ToString().Trim();
                        dr["mes3"] = pr.ventaMes[2].ToString().Trim();
                    }
                }
                else
                {
                    ddlTerritorioNom.AutoPostBack = true;
                    txtIdCte.Disabled = true;
                    txtIdTer.Disabled = true;

                    txtContactoNom.Enabled = true;
                    txtContactoPuesto.Enabled = true;
                    txtContactoMail.Enabled = true;
                    txtContactoTel.Enabled = true;
                    /*
                    txtCalle.Enabled = false;
                    txtNo.Enabled = false;
                    txtCp.Enabled = false;
                    txtColonia.Enabled = false;
                    txtMunicipio.Enabled = false;
                    txtEstado.Enabled = false;
                    */

                    GetListDet();
                    fechaf = Convert.ToDateTime(funcion.GetLocalDateTime(session.Minutos).ToShortDateString()) > session.CalendarioFin ? session.CalendarioFin : Convert.ToDateTime(funcion.GetLocalDateTime(session.Minutos).ToShortDateString());
                    fechaf = fechaf.AddDays(1);
                    _nuevoPedidoSinProgramar = true;
                    HF_pedido.Value = _nuevoPedidoSinProgramar.ToString().Trim();
                }


                string ConexionCentral = ConfigurationManager.AppSettings["strConnectionCentral"].ToString().Trim();
                ConvenioDet conv;
                ConvenioDet convdet;
                CN_Convenio cn_conv;
                List<string> Productos = new List<string>();

                List<ConvenioDet> lconveniosdet = new List<ConvenioDet>();
                ConvenioDet lconvdet = new ConvenioDet();


                if (dt.Rows.Count > 0)
                {
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        if (Convert.ToDouble(dt.Rows[x]["Prd_PrecioLista"]) == 0)
                        {
                            conv = new ConvenioDet();
                            convdet = new ConvenioDet();
                            cn_conv = new CN_Convenio();
                            conv.Id_Emp = session.Id_Emp;
                            conv.Id_Cd = session.Id_Cd_Ver;
                            conv.Id_Cte = Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1");
                            conv.Id_Prd = Convert.ToInt64(dt.Rows[x]["Id_Prd"]);
                            double PrecioIngresado = Convert.ToDouble(dt.Rows[x]["Prd_Precio"]);

                            if (Convert.ToInt64(dt.Rows[x]["Id_Prd"]) <= 999999999999)
                            {
                                //cn_conv.Convenio_ConsultaPrecio(conv, ref convdet, ConexionCentral);

                                //if (convdet != null && convdet.PCD_PrecioAAAEsp > 0)
                                //{
                                //    dt.Rows[x]["Prd_PrecioLista"] = convdet.PCD_PrecioAAAEsp;
                                //}
                                //else
                                //{
                                Producto producto = new Producto();
                                //obtener datos de producto
                                CN_CatProducto clsProducto = new CN_CatProducto();
                                producto.Id_Cte = Convert.ToInt32(Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1"));
                                int productoNuevo = 0;
                                clsProducto.ConsultaProductos(ref producto, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, Convert.ToInt64(dt.Rows[x]["Id_Prd"]), ref productoNuevo, 0);

                                if (0 < producto.Prd_PLista)
                                {
                                    dt.Rows[x]["Prd_PrecioLista"] = producto.Prd_PLista;
                                }
                                //}
                            }
                        }
                    }
                }


                if (dt_Resto.Rows.Count > 0)
                {
                    for (int x = 0; x < dt_Resto.Rows.Count; x++)
                    {
                        if (Convert.ToDouble(dt_Resto.Rows[x]["Prd_PrecioLista"]) == 0)
                        {
                            conv = new ConvenioDet();
                            convdet = new ConvenioDet();
                            cn_conv = new CN_Convenio();
                            conv.Id_Emp = session.Id_Emp;
                            conv.Id_Cd = session.Id_Cd_Ver;
                            conv.Id_Cte = Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1");
                            conv.Id_Prd = Convert.ToInt64(dt_Resto.Rows[x]["Id_Prd"]);
                            double PrecioIngresado = Convert.ToDouble(dt_Resto.Rows[x]["Prd_Precio"]);

                            if (Convert.ToInt64(dt_Resto.Rows[x]["Id_Prd"]) <= 999999999999)
                            {
                                //cn_conv.Convenio_ConsultaPrecio(conv, ref convdet, ConexionCentral);

                                //if (convdet != null && convdet.PCD_PrecioAAAEsp > 0)
                                //{
                                //    dt_Resto.Rows[x]["Prd_PrecioLista"] = convdet.PCD_PrecioAAAEsp.ToString();
                                //}
                                //else
                                //{
                                Producto producto = new Producto();
                                //obtener datos de producto
                                CN_CatProducto clsProducto = new CN_CatProducto();
                                producto.Id_Cte = Convert.ToInt32(Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1"));
                                int productoNuevo = 0;
                                clsProducto.ConsultaProductos(ref producto, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, Convert.ToInt64(dt_Resto.Rows[x]["Id_Prd"]), ref productoNuevo, 0);

                                if (0 < producto.Prd_PLista)
                                {
                                    dt_Resto.Rows[x]["Prd_PrecioLista"] = producto.Prd_PLista.ToString();
                                }
                                //}
                            }
                        }
                    }
                }


                Session["Prod"] = dt;
                Session["Restos"] = dt_Resto;


                rg1.DataSource = dt;
                rg1.DataBind();

                rg1_Restos.DataSource = dt_Resto;
                rg1_Restos.DataBind();

                string _idTGStr = Request.QueryString["Id_TG"];
                int _idTG = 0;
                if (_idTGStr != null)
                {
                    if (int.TryParse(_idTGStr, out _idTG))
                    {


                    }
                }

                foreach (DataRow i in dt.Rows)
                {
                    if (i["Acs_FechaF"] != DBNull.Value)
                    {
                        if (fechaf > Convert.ToDateTime(i["Acs_FechaF"]) || fechaf == default(DateTime))
                        {
                            fechaf = Convert.ToDateTime(i["Acs_FechaF"]);
                        }
                    }
                }

                if (fechaf.Year != 1)
                {
                    rdFechaF.Value = fechaf;
                }

                DateTime fecha_actual = funcion.GetLocalDateTime(session.Minutos).AddDays(1);
                rdFechaF.Value = fecha_actual > session.CalendarioFin ? session.CalendarioFin : fecha_actual;

                CN_CatSemana CnSemana = new CN_CatSemana();
                Semana semana = new Semana();
                semana.Id_Emp = session.Id_Emp;
                semana.Id_Cd = session.Id_Cd_Ver;
                semana.Sem_FechaAct = Convert.ToDateTime(funcion.GetLocalDateTime(session.Minutos).ToShortDateString());
                verificador = -1;
                CnSemana.ConsultaSemana(ref semana, session.Emp_Cnx, ref verificador);

                if (verificador > -1)
                {
                    HF_FechaActual.Value = rdFechaF.Value.ToString().Trim();
                    HF_InicioSemana.Value = semana.Sem_FechaIni.ToString().Trim();
                    HF_FinSemana.Value = semana.Sem_FechaFin.ToString().Trim();
                }
                Consultar_IVA_Cliente();
                calcularsubtotal();

            }
            catch (Exception ex)
            {
                mensajeError(ex.Message);
                throw ex;
            }
        }

        private void ConsultarClienteFechaEntrega(string strConexion, int intIdCte, int intIdCd)
        {

            int intDiasEntrega = 0;
            int intEsModificable = 1;
            DateTime fechaEntrega = new DateTime();
            string strFechaEntrega = string.Empty;

            CN_CapPedido cN_CapPedido = new CN_CapPedido();
            cN_CapPedido.ConsultarClienteFechaEntrega(strConexion, intIdCte, intIdCd, ref strFechaEntrega, ref intDiasEntrega, ref fechaEntrega, ref intEsModificable);
            if (!string.IsNullOrEmpty(strFechaEntrega))
            {
                rdFechaE.Value = Convert.ToDateTime(strFechaEntrega);
                lblDiasEntrega.Visible = true;
                lblDiasEntrega.Text = "Este cliente cuenta con un maximo de " + intDiasEntrega.ToString() + " dias para su entrega.";
                HF_FechaEntregaCompromiso.Value = fechaEntrega.ToString("yyyy-MM-dd");
                if (intEsModificable != 1)
                {
                    rdFechaE.Enabled = false;
                }
            }
        }

        private void CargarDatosAcuerdo()
        {
            try
            {
                int verificador = 0;

                DateTime fechaf = default(DateTime);
                Funciones funcion = new Funciones();

                if (HF_ID.Value != null)
                {
                    txtClave.Text = HF_ID.Value;

                    PedidoVtaInst pedido = new PedidoVtaInst();
                    pedido.Id_Emp = session.Id_Emp;
                    pedido.Id_Cd = session.Id_Cd_Ver;
                    pedido.Id_Acs = Convert.ToInt32(HF_ID.Value);
                    Clientes cc = new Clientes();
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    sesion = (Sesion)Session["Sesion" + Session.SessionID];

                    CN_CapPedidoVtaInst cn_capPedidoVI = new CN_CapPedidoVtaInst();
                    cn_capPedidoVI.Consultar2(ref pedido, session.Emp_Cnx, ref verificador, ref cc);

                    if (verificador == 1)
                    {

                        ddlClienteNom.Value = pedido.Id_Cte.ToString().Trim();
                        txtIdCte.Value = pedido.Id_Cte.ToString().Trim();
                        txtRikNom.Value = pedido.Rik_Nombre;
                        txtIdRik.Value = pedido.Id_Rik.ToString().Trim();

                        ddlTerritorioNom.Value = pedido.Ter_Nombre;
                        txtIdTer.Value = pedido.Id_Ter.ToString().Trim();

                        txtContactoNom.Value = pedido.Acs_Contacto;
                        txtContactoTel.Value = pedido.Acs_Telefono;
                        txtContactoMail.Value = pedido.Acs_email;
                        txtContactoPuesto.Value = pedido.Acs_Puesto;

                        txtCalle.Value = cc.Cte_Calle;
                        txtNo.Value = cc.Cte_Numero.ToString().Trim().Replace(" ", "");
                        txtCp.Value = cc.Cte_Cp;
                        txtMunicipio.Value = cc.Cte_Municipio;
                        txtEstado.Value = cc.Cte_Estado;
                        txtColonia.Value = cc.Cte_Colonia;


                        txtContactoNom.Value = pedido.Acs_PedidoEncargadoEnviar;
                        txtContactoTel.Value = pedido.Acs_PedidoTelefono;
                        txtContactoMail.Value = pedido.Acs_PedidoEmail;
                        txtContactoPuesto.Value = pedido.Acs_PedidoPuesto;
                        ChkOrdCompra.Checked = pedido.DocOrdenCompra;
                        ChkOrdReposicion.Checked = pedido.DocOrdReposEnt;
                        chkFolio.Checked = pedido.DocCopPedidoEnt;

                        CHKFacKey.Checked = pedido.Acs_ReqFacturaKey;
                        CHKRemision.Checked = pedido.ACS_ReqRemision;
                        CHKCopiaPed.Checked = pedido.Acs_ReqCopia;

                        TxtEOtro.Value = pedido.Acs_ReqDocOtro;

                        //Llena la informacion de compras

                        txtComprasNombre.Value = pedido.Acs_Contacto2;
                        if (pedido.Acs_Telefono2 == "0")
                        {
                            txtComprasTelefono.Value = "";
                        }
                        else
                        {
                            txtComprasTelefono.Value = pedido.Acs_Telefono2;
                        }
                        txtComprasCorreo.Value = pedido.Acs_Email2;

                        //Llena la información de almacen
                        txtAlmacenNombre.Value = pedido.Acs_Contacto3;
                        if (pedido.Acs_Telefono3 == "0")
                        {
                            txtAlmacenTelefono.Value = "";
                        }
                        else
                        {
                            txtAlmacenTelefono.Value = pedido.Acs_Telefono3;
                        }
                        txtAlmacenCorreo.Value = pedido.Acs_Email3;


                        //Llena la información de Mantenimiento
                        TxtMtoNombre.Value = pedido.Acs_Contacto4;
                        if (pedido.Acs_Telefono4 == "0")
                        {
                            TxtMtoTelefono.Value = "";
                        }
                        else
                        {
                            TxtMtoTelefono.Value = pedido.Acs_Telefono4;
                        }
                        TxtMtoCorreo.Value = pedido.Acs_Email4;


                        //Llena la información de Pagos
                        txtPagoNombre.Value = pedido.Acs_Contacto5;
                        if (pedido.Acs_Telefono5 == "0")
                        {
                            txtPagoTelefono.Value = "";
                        }
                        else
                        {
                            txtPagoTelefono.Value = pedido.Acs_Telefono5;
                        }
                        txtPagoCorreo.Value = pedido.Acs_Email5;




                        txtContactoNom.Enabled = false;
                        txtContactoTel.Enabled = false;
                        txtContactoMail.Enabled = false;
                        txtContactoPuesto.Enabled = false;

                        txtPagoCorreo.Enabled = false;
                        txtPagoNombre.Enabled = false;
                        txtPagoTelefono.Enabled = false;

                        txtAlmacenCorreo.Enabled = false;
                        txtAlmacenNombre.Enabled = false;
                        txtAlmacenTelefono.Enabled = false;

                        TxtMtoCorreo.Enabled = false;
                        TxtMtoNombre.Enabled = false;
                        TxtMtoTelefono.Enabled = false;

                        txtComprasCorreo.Enabled = false;
                        txtComprasNombre.Enabled = false;
                        txtComprasTelefono.Enabled = false;

                        txtIdCte.Disabled = true;
                        txtIdTer.Disabled = true;
                        ddlTerritorioNom.Enabled = false;

                        txtIdRik.Disabled = true;
                        txtRikNom.Disabled = true;

                        //ChkOrdCompra.Disabled = true;
                        //chkFolio.Disabled = true;
                        //ChkOrdReposicion.Disabled = true;

                        //CHKFacKey.Disabled = true;
                        //CHKRemision.Disabled = true;
                        //CHKCopiaPed.Disabled = true;


                        //lblFacturakey.Enabled = false;
                        //lblOrdenCompraCopias.Enabled = false;
                        //lblOrdenRepo.Enabled = false;

                        //lblremision.Enabled = false;
                        //lblCopia.Enabled = false;
                        //lblFolio.Enabled = false;
                    }
                    else
                    {
                        mensajeInfo("No se encontro");
                    }

                    pedido.Id_Emp = session.Id_Emp;
                    pedido.Id_Cd = session.Id_Cd_Ver;
                    pedido.Id_Acs = Convert.ToInt32(HF_ID.Value);
                    pedido.Acs_Anio = Convert.ToInt32(HF_Anio.Value);
                    pedido.Acs_Semana = Convert.ToInt32(HF_Sem.Value);
                    dt = null;

                    GetListDet();
                    DataTable dtTemp = dt;
                    List<PedidoVtaInst> List = new List<PedidoVtaInst>();
                    string idTGStr = Request.QueryString["Id_TG"];
                    int? idTGNullable = 0;
                    int idTG = 0;
                    if (idTGStr != null)
                    {
                        if (int.TryParse(idTGStr, out idTG))
                        {
                            idTGNullable = idTG;
                        }
                    }
                    cn_capPedidoVI.ConsultarDet2(pedido, ref List, ref dtTemp, session.Emp_Cnx, idTGNullable);

                    DataTable dtTemp_Resto = this.dt_Resto;
                    List<PedidoVtaInst> List2 = new List<PedidoVtaInst>();
                    cn_capPedidoVI.ConsultarDet_Resto(pedido, ref List2, ref dtTemp_Resto, session.Emp_Cnx, Id_TG);



                    dt = dtTemp;
                    dt_Resto = dtTemp_Resto;

                    CN_CatProducto cn_catproducto = new CN_CatProducto();
                    Producto pr;
                    foreach (DataRow dr in dt.Rows)
                    {
                        pr = new Producto();
                        pr.Id_Emp = session.Id_Emp;
                        pr.Id_Cd = session.Id_Cd_Ver;
                        pr.Id_Prd = Convert.ToInt64(dr["Id_prd"]);

                        cn_catproducto.ConsultarVentas(ref pr, Convert.ToInt32(txtIdCte.Value), session.Emp_Cnx);

                        dr["mes1"] = pr.ventaMes[0].ToString().Trim();
                        dr["mes2"] = pr.ventaMes[1].ToString().Trim();
                        dr["mes3"] = pr.ventaMes[2].ToString().Trim();
                    }

                    foreach (DataRow dr in dt_Resto.Rows)
                    {
                        pr = new Producto();
                        pr.Id_Emp = session.Id_Emp;
                        pr.Id_Cd = session.Id_Cd_Ver;
                        pr.Id_Prd = Convert.ToInt64(dr["Id_prd"]);

                        cn_catproducto.ConsultarVentas(ref pr, Convert.ToInt32(txtIdCte.Value), session.Emp_Cnx);

                        dr["mes1"] = pr.ventaMes[0].ToString().Trim();
                        dr["mes2"] = pr.ventaMes[1].ToString().Trim();
                        dr["mes3"] = pr.ventaMes[2].ToString().Trim();
                    }
                }
                else
                {
                    ddlTerritorioNom.AutoPostBack = true;
                    txtIdCte.Disabled = true;
                    txtIdTer.Disabled = true;

                    txtContactoNom.Enabled = true;
                    txtContactoPuesto.Enabled = true;
                    txtContactoMail.Enabled = true;
                    txtContactoTel.Enabled = true;
                    /*
                    txtCalle.Enabled = false;
                    txtNo.Enabled = false;
                    txtCp.Enabled = false;
                    txtColonia.Enabled = false;
                    txtMunicipio.Enabled = false;
                    txtEstado.Enabled = false;
                    */

                    GetListDet();
                    fechaf = Convert.ToDateTime(funcion.GetLocalDateTime(session.Minutos).ToShortDateString()) > session.CalendarioFin ? session.CalendarioFin : Convert.ToDateTime(funcion.GetLocalDateTime(session.Minutos).ToShortDateString());
                    fechaf = fechaf.AddDays(1);
                    _nuevoPedidoSinProgramar = true;
                    HF_pedido.Value = _nuevoPedidoSinProgramar.ToString().Trim();
                }


                string ConexionCentral = ConfigurationManager.AppSettings["strConnectionCentral"].ToString().Trim();
                ConvenioDet conv;
                ConvenioDet convdet;
                CN_Convenio cn_conv;
                List<string> Productos = new List<string>();

                List<ConvenioDet> lconveniosdet = new List<ConvenioDet>();
                ConvenioDet lconvdet = new ConvenioDet();


                if (dt.Rows.Count > 0)
                {
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        if (Convert.ToDouble(dt.Rows[x]["Prd_PrecioLista"]) == 0)
                        {
                            conv = new ConvenioDet();
                            convdet = new ConvenioDet();
                            cn_conv = new CN_Convenio();
                            conv.Id_Emp = session.Id_Emp;
                            conv.Id_Cd = session.Id_Cd_Ver;
                            conv.Id_Cte = Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1");
                            conv.Id_Prd = Convert.ToInt64(dt.Rows[x]["Id_Prd"]);
                            double PrecioIngresado = Convert.ToDouble(dt.Rows[x]["Prd_Precio"]);

                            if (Convert.ToInt64(dt.Rows[x]["Id_Prd"]) <= 999999999999)
                            {
                                //cn_conv.Convenio_ConsultaPrecio(conv, ref convdet, ConexionCentral);

                                //if (convdet != null && convdet.PCD_PrecioAAAEsp > 0)
                                //{
                                //    dt.Rows[x]["Prd_PrecioLista"] = convdet.PCD_PrecioAAAEsp;
                                //}
                                //else
                                //{
                                Producto producto = new Producto();
                                //obtener datos de producto
                                CN_CatProducto clsProducto = new CN_CatProducto();
                                producto.Id_Cte = Convert.ToInt32(Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1"));
                                int productoNuevo = 0;
                                clsProducto.ConsultaProductos(ref producto, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, Convert.ToInt64(dt.Rows[x]["Id_Prd"]), ref productoNuevo, 0);

                                if (0 < producto.Prd_PLista)
                                {
                                    dt.Rows[x]["Prd_PrecioLista"] = producto.Prd_PLista;
                                }
                                //}
                            }
                        }
                    }
                }


                if (dt_Resto.Rows.Count > 0)
                {
                    for (int x = 0; x < dt_Resto.Rows.Count; x++)
                    {
                        if (Convert.ToDouble(dt_Resto.Rows[x]["Prd_PrecioLista"]) == 0)
                        {
                            conv = new ConvenioDet();
                            convdet = new ConvenioDet();
                            cn_conv = new CN_Convenio();
                            conv.Id_Emp = session.Id_Emp;
                            conv.Id_Cd = session.Id_Cd_Ver;
                            conv.Id_Cte = Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1");
                            conv.Id_Prd = Convert.ToInt64(dt_Resto.Rows[x]["Id_Prd"]);
                            double PrecioIngresado = Convert.ToDouble(dt_Resto.Rows[x]["Prd_Precio"]);

                            if (Convert.ToInt64(dt_Resto.Rows[x]["Id_Prd"]) <= 999999999999)
                            {
                                //cn_conv.Convenio_ConsultaPrecio(conv, ref convdet, ConexionCentral);

                                //if (convdet != null && convdet.PCD_PrecioAAAEsp > 0)
                                //{
                                //    dt_Resto.Rows[x]["Prd_PrecioLista"] = convdet.PCD_PrecioAAAEsp.ToString();
                                //}
                                //else
                                //{
                                Producto producto = new Producto();
                                //obtener datos de producto
                                CN_CatProducto clsProducto = new CN_CatProducto();
                                producto.Id_Cte = Convert.ToInt32(Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1"));
                                int productoNuevo = 0;
                                clsProducto.ConsultaProductos(ref producto, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, Convert.ToInt64(dt_Resto.Rows[x]["Id_Prd"]), ref productoNuevo, 0);

                                if (0 < producto.Prd_PLista)
                                {
                                    dt_Resto.Rows[x]["Prd_PrecioLista"] = producto.Prd_PLista.ToString();
                                }
                                //}
                            }
                        }
                    }
                }


                Session["Prod"] = dt;
                Session["Restos"] = dt_Resto;


                rg1.DataSource = dt;
                rg1.DataBind();

                rg1_Restos.DataSource = dt_Resto;
                rg1_Restos.DataBind();

                string _idTGStr = Request.QueryString["Id_TG"];
                int _idTG = 0;
                if (_idTGStr != null)
                {
                    if (int.TryParse(_idTGStr, out _idTG))
                    {


                    }
                }

                foreach (DataRow i in dt.Rows)
                {
                    if (i["Acs_FechaF"] != DBNull.Value)
                    {
                        if (fechaf > Convert.ToDateTime(i["Acs_FechaF"]) || fechaf == default(DateTime))
                        {
                            fechaf = Convert.ToDateTime(i["Acs_FechaF"]);
                        }
                    }
                }

                if (fechaf.Year != 1)
                {
                    rdFechaF.Value = fechaf;
                }

                DateTime fecha_actual = funcion.GetLocalDateTime(session.Minutos).AddDays(1);
                rdFechaF.Value = fecha_actual > session.CalendarioFin ? session.CalendarioFin : fecha_actual;


                CN_CatSemana CnSemana = new CN_CatSemana();
                Semana semana = new Semana();
                semana.Id_Emp = session.Id_Emp;
                semana.Id_Cd = session.Id_Cd_Ver;
                semana.Sem_FechaAct = Convert.ToDateTime(funcion.GetLocalDateTime(session.Minutos).ToShortDateString());
                verificador = -1;
                CnSemana.ConsultaSemana(ref semana, session.Emp_Cnx, ref verificador);

                if (verificador > -1)
                {
                    HF_FechaActual.Value = rdFechaF.Value.ToString().Trim();
                    HF_InicioSemana.Value = semana.Sem_FechaIni.ToString().Trim();
                    HF_FinSemana.Value = semana.Sem_FechaFin.ToString().Trim();
                }
                Consultar_IVA_Cliente();
                calcularsubtotal();

            }
            catch (Exception ex)
            {
                mensajeError(ex.Message);
                throw ex;
            }
        }


        public void calcularsubtotal()
        {
            double imp = 0;

            DataTable dt = (DataTable)Session["Prod"];

            if (dt != null)
            {
                foreach (DataRow i in dt.Rows)
                {
                    imp += Convert.ToDouble(i["Prd_Importe"] == DBNull.Value ? 0 : i["Prd_Importe"]);
                }
            }


            DataTable drestos = (DataTable)Session["Restos"];

            if (drestos != null)
            {
                foreach (DataRow i in drestos.Rows)
                {
                    imp += Convert.ToDouble(i["Prd_Importe"] == DBNull.Value ? 0 : i["Prd_Importe"]);
                }
            }


            txtSubtotal.Value = imp.ToString("F2");

            double iva_cd = 0;
            CN_CatCentroDistribucion cn = new CN_CatCentroDistribucion();
            cn.ConsultarIva(session.Id_Emp, session.Id_Cd_Ver, ref iva_cd, session.Emp_Cnx);

            double iva = (Convert.ToDouble(HD_IVAfacturacion.Value.ToString()) * imp / 100);
            double total = double.Parse(txtSubtotal.Value) + iva;
            txtIva.Value = iva.ToString("F2");
            txtTotal.Value = total.ToString("F2");

        }




        public bool _nuevoPedidoSinProgramar
        {
            get
            {
                return (bool)Session["_nuevoPedidoSinProgramar"];
            }
            set
            {
                Session["_nuevoPedidoSinProgramar"] = value;
            }
        }


        private void CargarPedidoInternet(int num_pedido, int tipoPedido)
        {
            try
            {
                //  HF_ID.Value = txtFolio.Text;
                Sesion sesion = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                Pedido_Internet pedido = new Pedido_Internet();
                ClienteDirEntrega dirEntrega = new ClienteDirEntrega();

                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;
                pedido.Num_Pedido = num_pedido;
                pedido.tipoPedido = tipoPedido;

                CN_CapPedido_Internet cn_capPedidoInternet = new CN_CapPedido_Internet();
                cn_capPedidoInternet.ConsultarPedido_Datos(session.Emp_Cnx, ref pedido, ref dirEntrega, num_pedido, tipoPedido);

                ddlClienteNom.SelectedItem.Text = pedido.Nom_Cliente;
                txtIdCte.Value = pedido.Id_Cte.ToString().Trim();
                TxtPed_ReqAcys.Value = pedido.Num_Pedido.ToString().Trim();

                CargarTerritorios();

                txtContactoNom.Value = pedido.Nombre_Usuario;
                txtContactoTel.Value = pedido.Telefono_Usuario;
                txtContactoMail.Value = pedido.Cuenta_Usuario;
                txtContactoPuesto.Value = "Usuario Internet";

                txtCalle.Value = dirEntrega.Cte_Calle;
                txtNo.Value = dirEntrega.ToString().Trim().Replace(" ", "");

                int res = 0;
                if (Int32.TryParse(dirEntrega.Cte_Cp, out res)) txtCp.Value = dirEntrega.Cte_Cp;

                txtMunicipio.Value = dirEntrega.Cte_Municipio;
                txtEstado.Value = dirEntrega.Cte_Estado;
                txtColonia.Value = dirEntrega.Cte_Colonia;


                txtSubtotal.Value = pedido.Subtotal.ToString().Trim();
                txtIva.Value = pedido.IVA.ToString().Trim();
                txtTotal.Value = pedido.Total.ToString().Trim();

                //Edsg Desactiva los campos
                TxtPed_ReqAcys.Enabled = false;



                GetListDet();

                DataTable dtTemp = dt;
                cn_capPedidoInternet.ConsultarPedido_Detalle(session.Emp_Cnx, num_pedido, 0, ref dtTemp);
                dt = dtTemp;

                CN_CatProducto cn_catproducto = new CN_CatProducto();
                Producto pr;
                foreach (DataRow dr in dt.Rows)
                {
                    pr = new Producto();
                    pr.Id_Emp = session.Id_Emp;
                    pr.Id_Cd = session.Id_Cd_Ver;
                    pr.Id_Prd = Convert.ToInt64(dr["Id_prd"]);

                    cn_catproducto.ConsultarVentas(ref pr, Convert.ToInt32(txtIdCte.Value), session.Emp_Cnx);

                    dr["mes1"] = pr.ventaMes[0].ToString().Trim();
                    dr["mes2"] = pr.ventaMes[1].ToString().Trim();
                    dr["mes3"] = pr.ventaMes[2].ToString().Trim();
                }
                rg1.DataBind();


            }
            catch (Exception ex)
            {
                mensajeError(ex.Message);
                throw ex;
            }
        }

        private void CargarPedido()
        {
            try
            {
                HF_ID.Value = txtFolio.Text;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                session = (Sesion)Session["Sesion" + Session.SessionID];
                Pedido pedido = new Pedido();
                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;
                pedido.Id_Ped = Convert.ToInt32(txtFolio.Value);

                CN_CapPedido cn_capPedido = new CN_CapPedido();
                cn_capPedido.ConsultaPedido(ref pedido, session.Emp_Cnx);

                ddlClienteNom.Value = pedido.Id_Cte.ToString().Trim();
                txtIdCte.Value = pedido.Id_Cte.ToString().Trim();

                ddlClienteNom.Enabled = false;
                ddlTerritorioNom.Enabled = false;

                CargarTerritorios();

                TxtPed_ReqAcys.Value = pedido.Ped_OrdenCompra != null || pedido.Ped_OrdenCompra != "" ? pedido.Requisicion : pedido.Ped_OrdenCompra;
                txtRikNom.Value = pedido.Rik_Nombre;
                txtIdRik.Value = pedido.Id_Rik.ToString().Trim();
                ddlTerritorioNom.Value = pedido.Id_Ter.ToString().Trim();
                txtIdTer.Value = pedido.Id_Ter.ToString().Trim();

                txtContactoNom.Value = pedido.Ped_Solicito;
                txtContactoTel.Value = pedido.Ped_SolicitoTel;
                txtContactoMail.Value = pedido.Ped_SolicitoEmail;
                txtContactoPuesto.Value = pedido.Ped_SolicitoPuesto;

                txtCalle.Value = pedido.Ped_ConsignadoCalle;

                txtNo.Value = pedido.Ped_ConsignadoNo.ToString().Trim().Replace(" ", "");
                txtCp.Value = pedido.Ped_ConsignadoCp;
                txtMunicipio.Value = pedido.Ped_ConsignadoMunicipio;
                txtEstado.Value = pedido.Ped_ConsignadoEstado;
                txtColonia.Value = pedido.Ped_ConsignadoColonia;
                ddUsoCfdi.Value = pedido.UsoCFDI?.ToString().Trim();
                /*
                txtCalle.Enabled = false;
                txtNo.Enabled = false;
                txtCp.Enabled = false;
                txtColonia.Enabled = false;
                txtMunicipio.Enabled = false;
                txtEstado.Enabled = false;
                */

                txtObservaciones.Value = pedido.Ped_Observaciones;
                Session["Semana"] = pedido.Ped_AcysSemana.ToString().Trim();

                txtClave.Value = pedido.Id_Acs.ToString().Trim();
                txtSubtotal.Value = pedido.Ped_Subtotal.ToString().Trim();
                txtIva.Value = pedido.Ped_Iva.ToString().Trim();
                txtTotal.Value = pedido.Ped_Total.ToString().Trim();

                rdFechaF.Value = pedido.Ped_FechFactAcys;
                rdFechaE.Value = pedido.Ped_FechaEntrega;

                if (pedido.FechaFacAcys.Year != 1)
                {
                    rdFechaF.Value = pedido.FechaFacAcys;
                }

                TxtPed_ReqAcys.Value = pedido.Requisicion;
                if (pedido.Ped_OrdenCompra != "")
                {
                    TxtPed_ReqAcys.Value = pedido.Ped_OrdenCompra;
                }
                ChkOrdCompra.Checked = pedido.Ped_ReqOrden;
                ChkOrdReposicion.Checked = pedido.Acs_ReqDocReposicion;
                chkFolio.Checked = pedido.Acs_ReqDocFolio;

                CHKFacKey.Checked = pedido.Acs_ReqFacturaKey;
                CHKRemision.Checked = pedido.ACS_ReqRemision;
                CHKCopiaPed.Checked = pedido.Acs_ReqCopia;

                lblFacturakey.Text = pedido.Acs_ReqFacturaKeyCop.ToString().Trim();
                lblOrdenCompraCopias.Text = pedido.Acs_ReqOrdencop.ToString().Trim();
                lblOrdenRepo.Text = pedido.Acs_ReqDocReposicioncop.ToString().Trim();

                lblremision.Text = pedido.ACS_ReqRemisionCop.ToString().Trim();
                lblCopia.Text = pedido.Acs_ReqCopiaCop.ToString().Trim();
                lblFolio.Text = pedido.Acs_ReqDocFoliocop.ToString().Trim();

                TxtEOtro.Value = pedido.Acs_ReqDocOtro;

                //Llena la informacion de compras

                txtComprasNombre.Value = pedido.acs_contacto2;
                if (pedido.acs_telefono2 == "0")
                {
                    txtComprasTelefono.Value = "";
                }
                else
                {
                    txtComprasTelefono.Value = pedido.acs_telefono2;
                }
                txtComprasCorreo.Value = pedido.acs_email2;

                //Llena la información de almacen
                txtAlmacenNombre.Value = pedido.acs_contacto3;
                if (pedido.acs_telefono3 == "0")
                {
                    txtAlmacenTelefono.Value = "";
                }
                else
                {
                    txtAlmacenTelefono.Value = pedido.acs_telefono3;
                }
                txtAlmacenCorreo.Value = pedido.acs_email3;


                //Llena la información de Mantenimiento
                TxtMtoNombre.Value = pedido.acs_contacto4;
                if (pedido.acs_telefono4 == "0")
                {
                    TxtMtoTelefono.Value = "";
                }
                else
                {
                    TxtMtoTelefono.Value = pedido.acs_telefono4;
                }
                TxtMtoCorreo.Value = pedido.acs_email4;


                //Llena la información de Pagos
                txtPagoNombre.Value = pedido.acs_contacto5;
                if (pedido.acs_telefono5 == "0")
                {
                    txtPagoTelefono.Value = "";
                }
                else
                {
                    txtPagoTelefono.Value = pedido.acs_telefono5;
                }
                txtPagoCorreo.Value = pedido.acs_email5;

                pedido.Ped_Tipo = 3;
                pedido.Ped_Captacion = true;

                GetListDet();
                DataTable dtTemp = dt;
                DataTable dtRestos = dt_Resto;

                cn_capPedido.ConsultaCaptacionPedidoDet(pedido, ref dtTemp, ref dtRestos, session.Emp_Cnx);

                dt = dtTemp;
                dt_Resto = dtRestos;

                CN_CatProducto cn_catproducto = new CN_CatProducto();
                Producto pr;
                foreach (DataRow dr in dt.Rows)
                {
                    pr = new Producto();
                    pr.Id_Emp = session.Id_Emp;
                    pr.Id_Cd = session.Id_Cd_Ver;
                    pr.Id_Prd = Convert.ToInt64(dr["Id_prd"]);

                    cn_catproducto.ConsultarVentas(ref pr, Convert.ToInt32(txtIdCte.Value), session.Emp_Cnx);

                    dr["mes1"] = pr.ventaMes[0].ToString().Trim();
                    dr["mes2"] = pr.ventaMes[1].ToString().Trim();
                    dr["mes3"] = pr.ventaMes[2].ToString().Trim();
                }

                foreach (DataRow dr in dt_Resto.Rows)
                {
                    pr = new Producto();
                    pr.Id_Emp = session.Id_Emp;
                    pr.Id_Cd = session.Id_Cd_Ver;
                    pr.Id_Prd = Convert.ToInt64(dr["Id_prd"]);

                    cn_catproducto.ConsultarVentas(ref pr, Convert.ToInt32(txtIdCte.Value), session.Emp_Cnx);

                    dr["mes1"] = pr.ventaMes[0].ToString().Trim();
                    dr["mes2"] = pr.ventaMes[1].ToString().Trim();
                    dr["mes3"] = pr.ventaMes[2].ToString().Trim();
                }


                string ConexionCentral = ConfigurationManager.AppSettings["strConnectionCentral"].ToString().Trim();
                ConvenioDet conv;
                ConvenioDet convdet;
                CN_Convenio cn_conv;
                List<string> Productos = new List<string>();

                List<ConvenioDet> lconveniosdet = new List<ConvenioDet>();
                ConvenioDet lconvdet = new ConvenioDet();


                if (dt.Rows.Count > 0)
                {
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        if (Convert.ToDouble(dt.Rows[x]["Prd_PrecioLista"]) == 0)
                        {
                            conv = new ConvenioDet();
                            convdet = new ConvenioDet();
                            cn_conv = new CN_Convenio();
                            conv.Id_Emp = session.Id_Emp;
                            conv.Id_Cd = session.Id_Cd_Ver;
                            conv.Id_Cte = Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1");
                            conv.Id_Prd = Convert.ToInt64(dt.Rows[x]["Id_Prd"]);
                            double PrecioIngresado = Convert.ToDouble(dt.Rows[x]["Prd_Precio"]);

                            if (Convert.ToInt64(dt.Rows[x]["Id_Prd"]) <= 999999999999)
                            {
                                //cn_conv.Convenio_ConsultaPrecio(conv, ref convdet, ConexionCentral);

                                //if (convdet != null && convdet.PCD_PrecioAAAEsp > 0)
                                //{
                                //    dt.Rows[x]["Prd_PrecioLista"] = convdet.PCD_PrecioAAAEsp;
                                //}
                                //else
                                //{
                                Producto producto = new Producto();
                                //obtener datos de producto
                                CN_CatProducto clsProducto = new CN_CatProducto();
                                producto.Id_Cte = Convert.ToInt32(Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1"));
                                int productoNuevo = 0;
                                clsProducto.ConsultaProductos(ref producto, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, Convert.ToInt64(dt.Rows[x]["Id_Prd"]), ref productoNuevo, 0);

                                if (0 < producto.Prd_PLista)
                                {
                                    dt.Rows[x]["Prd_PrecioLista"] = producto.Prd_PLista;
                                }
                                //}
                            }
                        }
                    }
                }


                if (dt_Resto.Rows.Count > 0)
                {
                    for (int x = 0; x < dt_Resto.Rows.Count; x++)
                    {
                        if (Convert.ToDouble(dt_Resto.Rows[x]["Prd_PrecioLista"]) == 0)
                        {
                            conv = new ConvenioDet();
                            convdet = new ConvenioDet();
                            cn_conv = new CN_Convenio();
                            conv.Id_Emp = session.Id_Emp;
                            conv.Id_Cd = session.Id_Cd_Ver;
                            conv.Id_Cte = Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1");
                            conv.Id_Prd = Convert.ToInt64(dt_Resto.Rows[x]["Id_Prd"]);
                            double PrecioIngresado = Convert.ToDouble(dt_Resto.Rows[x]["Prd_Precio"]);

                            if (Convert.ToInt64(dt_Resto.Rows[x]["Id_Prd"]) <= 999999999999)
                            {
                                //cn_conv.Convenio_ConsultaPrecio(conv, ref convdet, ConexionCentral);

                                //if (convdet != null && convdet.PCD_PrecioAAAEsp > 0)
                                //{
                                //    dt_Resto.Rows[x]["Prd_PrecioLista"] = convdet.PCD_PrecioAAAEsp.ToString();
                                //}
                                //else
                                //{
                                Producto producto = new Producto();
                                //obtener datos de producto
                                CN_CatProducto clsProducto = new CN_CatProducto();
                                producto.Id_Cte = Convert.ToInt32(Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1"));
                                int productoNuevo = 0;
                                clsProducto.ConsultaProductos(ref producto, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, Convert.ToInt64(dt_Resto.Rows[x]["Id_Prd"]), ref productoNuevo, 0);

                                if (0 < producto.Prd_PLista)
                                {
                                    dt_Resto.Rows[x]["Prd_PrecioLista"] = producto.Prd_PLista.ToString();
                                }
                                //}
                            }
                        }
                    }
                }


                Session["Prod"] = dt;
                rg1.DataSource = dt;
                rg1.DataBind();

                Session["Restos"] = dt_Resto;
                rg1_Restos.DataSource = dt_Resto;
                rg1_Restos.DataBind();


                string _idTGStr = Request.QueryString["Id_TG"];
                int _idTG = 0;
                if (_idTGStr != null)
                {
                    if (int.TryParse(_idTGStr, out _idTG))
                    {


                    }
                }

                Consultar_IVA_Cliente();

                Funciones funcion = new Funciones();
                CN_CatSemana CnSemana = new CN_CatSemana();
                Semana semana = new Semana();
                semana.Id_Emp = session.Id_Emp;
                semana.Id_Cd = session.Id_Cd_Ver;
                semana.Sem_FechaAct = funcion.GetLocalDateTime(session.Minutos);
                int verificador = -1;
                CnSemana.ConsultaSemana(ref semana, session.Emp_Cnx, ref verificador);

                if (verificador > -1)
                {
                    HF_FechaActual.Value = rdFechaF.ToString().Trim();
                    HF_InicioSemana.Value = semana.Sem_FechaIni.ToString().Trim();
                    HF_FinSemana.Value = semana.Sem_FechaFin.ToString().Trim();
                }

                if (txtClave.Value != "")
                {
                    txtContactoNom.Enabled = true;
                    txtContactoTel.Enabled = true;
                    txtContactoMail.Enabled = true;
                    txtContactoPuesto.Enabled = true;

                    txtPagoCorreo.Enabled = true;
                    txtPagoNombre.Enabled = true;
                    txtPagoTelefono.Enabled = true;

                    txtAlmacenCorreo.Enabled = true;
                    txtAlmacenNombre.Enabled = true;
                    txtAlmacenTelefono.Enabled = true;

                    TxtMtoCorreo.Enabled = true;
                    TxtMtoNombre.Enabled = true;
                    TxtMtoTelefono.Enabled = true;

                    txtComprasCorreo.Enabled = true;
                    txtComprasNombre.Enabled = true;
                    txtComprasTelefono.Enabled = true;

                    txtIdCte.Disabled = true;
                    ddlClienteNom.Enabled = false;
                    txtIdTer.Disabled = true;
                    ddlTerritorioNom.Enabled = false;

                    txtIdRik.Disabled = true;
                    txtRikNom.Disabled = true;

                    //ChkOrdCompra.Disabled = true;
                    //chkFolio.Disabled = true;
                    //ChkOrdReposicion.Disabled = true;

                    //CHKFacKey.Disabled = true;
                    //CHKRemision.Disabled = true;
                    //CHKCopiaPed.Disabled = true;


                    //lblFacturakey.Enabled = false;
                    //lblOrdenCompraCopias.Enabled = false;
                    //lblOrdenRepo.Enabled = false;

                    //lblremision.Enabled = false;
                    //lblCopia.Enabled = false;
                    //lblFolio.Enabled = false;

                }
                else
                {

                    txtContactoNom.Enabled = true;
                    txtContactoPuesto.Enabled = true;
                    txtContactoMail.Enabled = true;
                    txtContactoTel.Enabled = true;
                    /*
                    txtCalle.Enabled = false;
                    txtNo.Enabled = false;
                    txtCp.Enabled = false;
                    txtColonia.Enabled = false;
                    txtMunicipio.Enabled = false;
                    txtEstado.Enabled = false;
                    */
                }
            }
            catch (Exception ex)
            {
                mensajeError(ex.Message);
                throw ex;
            }
        }

        private void CargarInfoAcys(int Id_Acys, int AcysMes, int AcysAnio)
        {
            try
            {
                int verificador = 0;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                PedidoVtaInst pedido = new PedidoVtaInst();
                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;
                pedido.Id_Acs = Id_Acys;
                Clientes cc = new Clientes();
                CN_CapPedidoVtaInst cn_capPedidoVI = new CN_CapPedidoVtaInst();
                cn_capPedidoVI.Consultar2(ref pedido, session.Emp_Cnx, ref verificador, ref cc);

                txtCalle.Value = cc.Cte_Calle;
                txtNo.Value = cc.Cte_Numero;
                txtCp.Value = cc.Cte_Cp;
                txtMunicipio.Value = cc.Cte_Municipio;
                txtEstado.Value = cc.Cte_Estado;
                txtColonia.Value = cc.Cte_Colonia;


                txtContactoNom.Value = pedido.Acs_PedidoEncargadoEnviar;
                txtContactoTel.Value = pedido.Acs_PedidoTelefono;
                txtContactoMail.Value = pedido.Acs_PedidoEmail;
                txtContactoPuesto.Value = pedido.Acs_PedidoPuesto;
                this.ChkOrdCompra.Checked = pedido.Acs_ReqOrden;
                this.ChkOrdReposicion.Checked = pedido.Acs_ReqDocReposicion;
                this.chkFolio.Checked = pedido.Acs_ReqDocFolio;

                CHKFacKey.Checked = pedido.Acs_ReqFacturaKey;
                CHKRemision.Checked = pedido.ACS_ReqRemision;
                CHKCopiaPed.Checked = pedido.Acs_ReqCopia;


                this.TxtEOtro.Value = pedido.Acs_ReqDocOtro;


                //Llena la informacion de compras

                txtComprasNombre.Value = pedido.Acs_Contacto2;
                if (pedido.Acs_Telefono2 == "0")
                {
                    txtComprasTelefono.Value = "";
                }
                else
                {
                    txtComprasTelefono.Value = pedido.Acs_Telefono2;
                }
                txtComprasCorreo.Value = pedido.Acs_Email2;

                //Llena la información de almacen
                txtAlmacenNombre.Value = pedido.Acs_Contacto3;
                if (pedido.Acs_Telefono3 == "0")
                {
                    txtAlmacenTelefono.Value = "";
                }
                else
                {
                    txtAlmacenTelefono.Value = pedido.Acs_Telefono3;
                }
                txtAlmacenCorreo.Value = pedido.Acs_Email3;


                //Llena la información de Mantenimiento
                TxtMtoNombre.Value = pedido.Acs_Contacto4;
                if (pedido.Acs_Telefono4 == "0")
                {
                    TxtMtoTelefono.Value = "";
                }
                else
                {
                    TxtMtoTelefono.Value = pedido.Acs_Telefono4;
                }
                TxtMtoCorreo.Value = pedido.Acs_Email4;


                //Llena la información de Pagos
                txtPagoNombre.Value = pedido.Acs_Contacto5;
                if (pedido.Acs_Telefono5 == "0")
                {
                    txtPagoTelefono.Value = "";
                }
                else
                {
                    txtPagoTelefono.Value = pedido.Acs_Telefono5;
                }
                txtPagoCorreo.Value = pedido.Acs_Email5;


                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;
                pedido.Id_Acs = Id_Acys;
                pedido.Acs_Semana = AcysMes;
                pedido.Acs_Anio = AcysAnio;
                List<PedidoVtaInst> List = new List<PedidoVtaInst>();
                cn_capPedidoVI.ConsultarDetAcys(pedido, ref List, session.Emp_Cnx);

            }
            catch (Exception ex)
            {
                mensajeError(ex.Message);
                throw ex;
            }
        }
        private void CargarTerritorios()
        {
            try
            {
                List<Territorios> Territorio = new List<Territorios>();
                CD_CatTerritorios CDterritorios = new CD_CatTerritorios();
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();

                CDterritorios.TerritorioCliente_Combo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, Convert.ToInt32(txtIdCte.Value.Length > 0 ? Int32.Parse(txtIdCte.Value) : -1), Sesion.Id_Rik < 0 ? (int?)null : Sesion.Id_Rik, null, Sesion.Emp_Cnx, ref Territorio);


                ddlTerritorioNom.DataSource = Territorio;
                ddlTerritorioNom.ValueField = "Id_Ter";
                ddlTerritorioNom.TextField = "Descripcion";
                ddlTerritorioNom.DataBind();



                if (ddlTerritorioNom.Items.Count > 1)
                {
                    ddlTerritorioNom.SelectedIndex = 1;
                    ddlTerritorioNom.SelectedItem.Text = ddlTerritorioNom.Items[1].Text;
                    txtIdTer.Value = ddlTerritorioNom.Items[1].Value.ToString().Trim();
                }
                else
                {
                    if (Request.QueryString["id"] != "-1")
                    {
                        CDterritorios.TerritorioCliente_Combo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, Convert.ToInt32(txtIdCte.Value.Length > 0 ? Int32.Parse(txtIdCte.Value) : -1), Sesion.Id_Rik < 0 ? (int?)null : Sesion.Id_Rik, 0, Sesion.Emp_Cnx, ref Territorio);


                        ddlTerritorioNom.DataSource = Territorio;
                        ddlTerritorioNom.ValueField = "Id_Ter";
                        ddlTerritorioNom.TextField = "Descripcion";
                        ddlTerritorioNom.DataBind();


                        if (ddlTerritorioNom.Items.Count > 1)
                        {
                            ddlTerritorioNom.SelectedIndex = 1;
                            ddlTerritorioNom.SelectedItem.Text = ddlTerritorioNom.Items[1].Text;
                            txtIdTer.Value = ddlTerritorioNom.Items[1].Value.ToString().Trim();
                        }
                    }
                    else
                    {
                        txtIdTer.Value = "";
                        ddlTerritorioNom.Items.Clear();
                        ddlTerritorioNom.Text = "";
                    }
                }
                if (ddlTerritorioNom.SelectedItem.Value != "")
                {
                    CN_CatTerritorios cn_terr = new CN_CatTerritorios();
                    Territorios terr = new Territorios();
                    terr.Id_Emp = session.Id_Emp;
                    terr.Id_Cd = session.Id_Cd_Ver;
                    terr.Id_Ter = Convert.ToInt32(ddlTerritorioNom.SelectedItem.Value);
                    cn_terr.ConsultaTerritorios(ref terr, session.Emp_Cnx);

                    txtRikNom.Value = terr.Rik_Nombre;
                    txtIdRik.Value = terr.Id_Rik.ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                mensajeError(ex.Message);
                throw ex;
            }
        }

        private List<eUsoCfdi> CargarUsoCfdi()
        {
            var result = new List<eUsoCfdi>();

            using (var cn = new SqlConnection(session.Emp_Cnx))
            {
                try
                {
                    cn.Open();

                    using (var cmd = new SqlCommand("SELECT Id, Descripcion FROM siancentral.siancentral.dbo.usocfdi", cn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new eUsoCfdi
                            {
                                Id = reader["Id"].ToString(),
                                Descripcion = reader["Descripcion"].ToString(),
                            });
                        }
                    }
                }
                catch
                {
                    // Manejo de error
                }
            }

            return result;
        }

        private void LlenarComboUsoCfdi()
        {
            var datos = CargarUsoCfdi();

            ddUsoCfdi.DataSource = datos;
            ddUsoCfdi.ValueField = "Id";
            ddUsoCfdi.TextField = "Descripcion";
            ddUsoCfdi.DataBind();
        }

        private void LimpiarRegistro(RadComboBox rdBox)
        {
            ((RadNumericTextBox)rdBox.Parent.Parent.FindControl("txtProd")).Text = string.Empty;
            ((Label)rdBox.Parent.Parent.FindControl("lblPres2")).Text = string.Empty;
            ((Label)rdBox.Parent.Parent.FindControl("lblUnidad2")).Text = string.Empty;
            ((RadNumericTextBox)rdBox.Parent.Parent.FindControl("txtPrecio")).Text = "0";
            ((RadNumericTextBox)rdBox.Parent.Parent.FindControl("txtCantidad")).Text = "0";
            ((RadNumericTextBox)rdBox.Parent.Parent.FindControl("txtImporte")).Text = "0";
        }
        private string MaximoId()
        {
            try
            {
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                return CN_Comun.Maximo(Sesion.Id_Emp, Sesion.Id_Cd_Ver, "CapPedido", "Id_Ped", Sesion.Emp_Cnx, "spCatLocal_Maximo");
            }
            catch (Exception ex)
            {
                mensajeError(ex.Message);
                throw ex;
            }
        }

        private void CargarProductos(RadComboBox sender)
        {
            try
            {
                RadComboBox productoDet = (sender.Parent.Parent.FindControl("cmbDescr") as RadComboBox);
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(session.Id_Emp, session.Id_Cd_Ver, Convert.ToInt32(sender.SelectedValue), session.Emp_Cnx, "spCatProdSeg_Combo", ref productoDet);
            }
            catch (Exception ex)
            {
                mensajeError(ex.Message);
                throw ex;
            }
        }
        private void CerrarVentana()
        {
            try
            {
                string funcion;
                if (this.HiddenRebind.Value == "0")
                {
                    funcion = "CloseWindow()";
                }
                else
                {
                    funcion = "CloseAndRebind()";
                }
                string script = "<script>" + funcion + "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), funcion, script, false);
            }
            catch (Exception ex)
            {
                mensajeError(ex.Message);
                throw ex;
            }

        }
        private void Imprimir(int Id_Ped)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ArrayList ALValorParametrosInternos = new ArrayList();


                CentroDistribucion Cd = new CentroDistribucion();
                new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref Cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);

                ALValorParametrosInternos.Add(sesion.Emp_Cnx);
                ALValorParametrosInternos.Add(sesion.Id_Emp);
                ALValorParametrosInternos.Add(sesion.Id_Cd_Ver);
                ALValorParametrosInternos.Add(Id_Ped);

                Type instance = null;
                instance = typeof(LibreriaReportes.PedidoImpresion);
                Session["InternParameter_Values" + Session.SessionID] = ALValorParametrosInternos;
                Session["assembly" + Session.SessionID] = instance.AssemblyQualifiedName;


                //RAM1.ResponseScripts.Add("AbrirReportePadre();");
            }
            catch (Exception ex)
            {
                mensajeError(ex.Message);
                throw ex;
            }
        }
        private void imprimir()
        {
            try
            {
                CapaDatos.Funciones funciones = new CapaDatos.Funciones();

                CapaDatos.Funciones fecha = new CapaDatos.Funciones();
                Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ArrayList ALValorParametrosInternos = new ArrayList();
                CapaDatos.Funciones CD = new CapaDatos.Funciones();

                ALValorParametrosInternos.Add(Sesion.Id_Emp);
                ALValorParametrosInternos.Add(Sesion.Emp_Cnx);

                Type instance = null;
                instance = typeof(LibreriaReportes.ReportePrueba);
                Session["InternParameter_Values" + Session.SessionID] = ALValorParametrosInternos;
                Session["assembly" + Session.SessionID] = instance.AssemblyQualifiedName;
                //RAM1.ResponseScripts.Add("AbrirReportePadre()");
            }
            catch (Exception ex)
            {
                mensajeError(ex.Message);
                throw ex;
            }
        }

        private void cargarComboClientesAcuerdo()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (sesion != null)
            {
                PedidoVtaInst pedido = new PedidoVtaInst();
                List<PedidoVtaInst> lista = new List<CapaEntidad.PedidoVtaInst>();
                CN_CapPedidoVtaInst vtaInst = new CN_CapPedidoVtaInst();
                pedido.Id_Emp = sesion.Id_Emp;
                pedido.Id_Cd = sesion.Id_Cd_Ver;
                CN_CapPedido cn_capPedido = new CN_CapPedido();
                vtaInst.ConsultaClienteAcysCombo(pedido, ref lista, sesion.Emp_Cnx);

                var query = (from tlist in lista
                             group tlist by tlist.Id_Cte into g
                             select new
                             {
                                 Id_Cte = g.Key,
                                 Acs_NomComercial = g.Key + " - " + g.Select(x => x.Acs_NomComercial).FirstOrDefault()
                             }).ToList().OrderBy(x => x.Id_Cte);

                ddlClienteNom.DataSource = query;
                ddlClienteNom.ValueField = "Id_Cte";
                ddlClienteNom.TextField = "Acs_NomComercial";
                ddlClienteNom.DataBind();
            }
        }

        private void CargarCliente()
        {
            txtIdCte.Value = ddlClienteNom.SelectedItem.Value.ToString().Trim();

            Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            bool cancelar = false;
            Clientes cte = new Clientes();
            cte.Id_Emp = Sesion.Id_Emp;
            cte.Id_Cd = Sesion.Id_Cd_Ver;
            cte.Id_Cte = ddlClienteNom.SelectedIndex.ToString().Trim() != "-1" ? int.Parse(ddlClienteNom.SelectedItem.Value.ToString().Trim()) : -1;
            cte.Id_Rik = Sesion.Id_Rik;
            CN_CatCliente catcliente = new CN_CatCliente();
            try
            {
                catcliente.ConsultaClientes(ref cte, Sesion.Emp_Cnx);
                //if (!cte.Cte_Facturacion)
                //{
                //    mensaje("CUIDADO: Este cliente se encuentra bloqueado por parte de cobranza; favor de aclarar su situación de créditos");
                //    cancelar = true;
                //}


                //if (cte.Cte_CreditoSuspendido)
                //{

                //    mensaje("Este cliente tiene el crédito suspendido");
                //    cancelar = true;
                //}

            }
            catch (Exception ex)
            {
                mensajeError(ex.Message);
                cancelar = true;
            }

            CargarTerritorios();

            txtContactoNom.Value = cte.Cte_Contacto;
            txtContactoMail.Value = cte.Cte_Email;
            txtContactoTel.Value = cte.Cte_Telefono;
            txtCalle.Value = cte.Cte_Calle;
            txtColonia.Value = cte.Cte_Colonia;
            txtEstado.Value = cte.Cte_Estado;
            txtNo.Value = cte.Cte_Numero;
            ddUsoCfdi.Value = cte.Cte_UsoCFDI?.ToString().Trim();

            if (cte.Cte_Cp != null)
            {
                if (cte.Cte_Cp.Trim() != "")
                {
                    txtCp.Value = cte.Cte_Cp;
                }
            }
            txtMunicipio.Value = cte.Cte_Municipio;

            if (cancelar)
            {

                ddlClienteNom.SelectedItem.Value = "";
                txtIdCte.Value = "";
                txtIdTer.Value = "";
                txtIdRik.Value = "";
                ddlTerritorioNom.Items.Clear();
                ddlTerritorioNom.SelectedIndex = -1;
                txtIdRik.Value = "";
                txtRikNom.Value = "";

            }

        }

        protected void ddlDocEntrega_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["Prod"];
            DataTable dtTemp = (DataTable)Session["Restos"];
            DataTable dtNuevaLista = (DataTable)Session["nuevaLista"];

            if (ddlDocEntrega.Value.ToString().Trim() != "-1")
            {
                if (dt != null)
                {
                    foreach (DataRow row in dt.AsEnumerable())
                    {
                        row.BeginEdit();
                        row["Acs_Doc"] = ddlDocEntrega.Value.ToString().Trim();
                        row.EndEdit();
                    }
                    Session["Prod"] = dt;
                }

                if (dtTemp != null)
                {
                    foreach (DataRow row in dtTemp.AsEnumerable())
                    {
                        row.BeginEdit();
                        row["Acs_Doc"] = ddlDocEntrega.Value.ToString().Trim();
                        row.EndEdit();
                    }
                    Session["Restos"] = dtTemp;
                }

                if (dtNuevaLista != null)
                {
                    foreach (DataRow row in dtNuevaLista.AsEnumerable())
                    {
                        row.BeginEdit();
                        row["Acs_Doc"] = ddlDocEntrega.Value.ToString().Trim();
                        row.EndEdit();
                    }

                    Session["nuevaLista"] = dtNuevaLista;
                }

                Session["Prod"] = dt;
                Session["Restos"] = dtTemp;
                Session["nuevaLista"] = dtNuevaLista;


                rg1.DataSource = dt;
                rg1.DataBind();

                rg1_Restos.DataSource = dtTemp;
                rg1_Restos.DataBind();
                Consultar_IVA_Cliente();
                calcularsubtotal();
            }

            TabName.Value = Request.Form[TabName.UniqueID];
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "mantaintabs('" + TabName.Value + "')", true);
        }

        protected void dddlRequiereOrdenCompra_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["Prod"];
            DataTable dtTemp = (DataTable)Session["Restos"];
            DataTable dtNuevaLista = (DataTable)Session["nuevaLista"];

            if (dddlRequiereOrdenCompra.Value.ToString().Trim() != "-1")
            {
                if (dt != null)
                {
                    foreach (DataRow row in dt.AsEnumerable())
                    {
                        row.BeginEdit();
                        row["ACS_ReqOC"] = dddlRequiereOrdenCompra.Value.ToString().Trim();
                        row.EndEdit();
                    }
                    Session["Prod"] = dt;
                }

                if (dtTemp != null)
                {
                    foreach (DataRow row in dtTemp.AsEnumerable())
                    {
                        row.BeginEdit();
                        row["ACS_ReqOC"] = dddlRequiereOrdenCompra.Value.ToString().Trim();
                        row.EndEdit();
                    }
                    Session["Restos"] = dtTemp;
                }

                if (dtNuevaLista != null)
                {
                    foreach (DataRow row in dtNuevaLista.AsEnumerable())
                    {
                        row.BeginEdit();
                        row["ACS_ReqOC"] = dddlRequiereOrdenCompra.Value.ToString().Trim();
                        row.EndEdit();
                    }

                    Session["nuevaLista"] = dtNuevaLista;
                }
                rg1.DataSource = dt;
                rg1.DataBind();

                rg1_Restos.DataSource = dtTemp;
                rg1_Restos.DataBind();
                Consultar_IVA_Cliente();
                calcularsubtotal();
            }

            TabName.Value = Request.Form[TabName.UniqueID];
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "mantaintabs('" + TabName.Value + "')", true);
        }

        private void GetListDet()
        {
            try
            {
                dt = new DataTable();
                DataColumn dc = new DataColumn();
                dt.Columns.Add("Id_PrdOld", System.Type.GetType("System.Int64"));
                dt.Columns.Add("Id_Prd", System.Type.GetType("System.Int64"));
                dt.Columns.Add("Prd_Descripcion", System.Type.GetType("System.String"));
                dt.Columns.Add("Prd_Presentacion", System.Type.GetType("System.String"));
                dt.Columns.Add("Prd_Unidad", System.Type.GetType("System.String"));

                dt.Columns.Add("Mes1", System.Type.GetType("System.Double"));
                dt.Columns.Add("Mes2", System.Type.GetType("System.Double"));
                dt.Columns.Add("Mes3", System.Type.GetType("System.Double"));

                dt.Columns.Add("Prd_Cantidad", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Prd_Precio", System.Type.GetType("System.Double"));
                dt.Columns.Add("Prd_PrecioAcys", System.Type.GetType("System.Double"));
                dt.Columns.Add("Prd_Importe", System.Type.GetType("System.Double"));
                dt.Columns.Add("Acs_Doc", System.Type.GetType("System.String"));

                dt.Columns.Add("Acs_FechaF", System.Type.GetType("System.DateTime"));
                dt.Columns.Add("Mod", System.Type.GetType("System.Boolean"));
                dt.Columns.Add("Acs_Dia", System.Type.GetType("System.String"));
                dt.Columns.Add("Acs_DiaStr", System.Type.GetType("System.String"));
                dt.Columns.Add("Acs_Frecuencia", System.Type.GetType("System.Int32"));

                dt.Columns.Add("Prd_RemFact", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Ped_Asignar", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Id_TG", System.Type.GetType("System.String"));
                dt.Columns.Add("Id_Acs", System.Type.GetType("System.Int32"));
                dt.Columns.Add("ACS_ReqOC", System.Type.GetType("System.String"));

                dt.Columns.Add("Prd_PrecioLista", System.Type.GetType("System.Double"));
                dt.Columns.Add("Prd_Original", System.Type.GetType("System.Int32"));
                dt.Columns.Add("Prd_Activo", System.Type.GetType("System.Int32"));
                dt_Resto = dt.Clone();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ListaModifcarEstatus()
        {
            dtNuevaLista = new DataTable();
            dtNuevaLista.Columns.Add("Id_Prd", System.Type.GetType("System.Int64"));
            dtNuevaLista.Columns.Add("Estatus", System.Type.GetType("System.Int32"));
            dtNuevaLista.Columns.Add("Tipo", System.Type.GetType("System.String"));
            dtNuevaLista.Columns.Add("Acs_Doc", System.Type.GetType("System.String"));
            dtNuevaLista.Columns.Add("ACS_ReqOC", System.Type.GetType("System.String"));
        }

        private void Guardar()
        {

            try
            {

                bool _PermisoGuardar = bool.Parse(Request.QueryString["PermisoGuardar"].ToString().Trim());
                bool _PermisoModificar = bool.Parse(Request.QueryString["PermisoModificar"].ToString().Trim());

                DataTable dt = (DataTable)Session["Prod"];
                DataTable dtTemp = (DataTable)Session["Restos"];
                string currentHash = CalculatePedidoContentHash(dt, dtTemp);
                string lastHash = GetLastPedidoHash();
                DateTime lastTime = GetLastPedidoHashTime();


                if (dt.Rows.Count == 0 && dtTemp.Rows.Count == 0)
                {
                    mensajeInfo("No ha agregado ningún producto en la sección de detalle");
                    return;
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow[] dr = dt.Select("Acs_Doc = ''");
                    if (dr.Length > 0)
                    {
                        mensajeInfo("No se seleccionó documento de entrega para el producto <b>" + dr[0][1] + " - " + dr[0][2] + "</b>");
                        return;
                    }
                }

                if (dtTemp.Rows.Count > 0)
                {
                    DataRow[] drRestos = dtTemp.Select("Acs_Doc = ''");
                    if (drRestos.Length > 0)
                    {
                        mensajeInfo("No se seleccionó documento de entrega para el producto <b>" + drRestos[0][1] + " - " + drRestos[0][2] + "</b>");
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(lastHash) && lastHash == currentHash && (DateTime.UtcNow - lastTime).TotalSeconds < IdempotencyWindowSeconds)
                {
                    mensajeInfo("Se detectó un guardado reciente con la misma información. No se duplicó el pedido.");
                    return;
                }

                if (txtClave.Text == "")
                {
                    txtClave.Text = "0";
                }

                int verificador = -1;
                Funciones funcion = new Funciones();
                PedidoVtaInst pedido = new PedidoVtaInst();
                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;
                pedido.Ped_Fecha = funcion.GetLocalDateTime(session.Minutos);

                pedido.Id_Rik = Convert.ToInt32(txtIdRik.Value);
                pedido.Id_Cte = Convert.ToInt32(txtIdCte.Value);

                if (txtIdTer.Value == "" || txtIdTer.Value == "-1")
                {
                    mensajeInfo("El Territorio es Requerido");
                    return;

                }
                pedido.Id_Ter = Convert.ToInt32(txtIdTer.Value);

                pedido.Pedido_del = txtFolio.Value.ToString().Trim().Trim();
                if (TxtPed_ReqAcys.Value != null && TxtPed_ReqAcys.Value != "")
                {
                    pedido.Requisicion = TxtPed_ReqAcys.Value.ToString().Trim().Trim();
                    pedido.Ped_OrdenEntrega = TxtPed_ReqAcys.Value.ToString().Trim().Trim();
                }
                pedido.Ped_Solicito = txtContactoNom.Text;
                pedido.Ped_Flete = string.Empty;

                pedido.Ped_CondEntrega = 0;
                pedido.Ped_FechaEntrega = Convert.ToDateTime(rdFechaE.Value);


                pedido.Ped_Observaciones = txtObservaciones.Text;
                pedido.Ped_DescPorcen1 = 0;
                pedido.Ped_DescPorcen2 = 0;
                pedido.Ped_Desc1 = string.Empty;
                pedido.Ped_Desc2 = string.Empty;
                pedido.Ped_Importe = txtSubtotal.Value != "" ? Convert.ToDouble(txtSubtotal.Value) : 0;
                pedido.Ped_Subtotal = txtSubtotal.Value != "" ? Convert.ToDouble(txtSubtotal.Value) : 0;
                pedido.Ped_Iva = txtIva.Value != "" ? Convert.ToDouble(txtIva.Value) : 0;
                pedido.Ped_Total = txtTotal.Value != "" ? Convert.ToDouble(txtTotal.Value) : 0;

                pedido.Id_U = session.Id_U;
                pedido.Id_Acs = Convert.ToInt32(txtClave.Value);
                pedido.Ped_SolicitoTel = txtContactoTel.Text;
                pedido.Ped_SolicitoEmail = txtContactoMail.Text;
                pedido.Ped_SolicitoPuesto = txtContactoPuesto.Text;
                pedido.Ped_ConsignadoCalle = txtCalle.Text;
                pedido.Ped_ConsignadoNo = txtNo.Text;
                pedido.Ped_ConsignadoCp = txtCp.Text;
                pedido.Ped_ConsignadoMunicipio = txtMunicipio.Text;
                pedido.Ped_ConsignadoEstado = txtEstado.Text;
                pedido.Ped_ConsignadoColonia = txtColonia.Text;
                pedido.Ped_ReqOrden = ChkOrdCompra.Checked;

                pedido.Acs_ReqFacturaKey = CHKFacKey.Checked;
                pedido.ACS_ReqRemision = CHKRemision.Checked;
                pedido.Acs_ReqCopia = CHKCopiaPed.Checked;

                pedido.Ped_Comentarios = txtObservaciones.Text;

                pedido.Acs_ReqFacturaKeyCop = Convert.ToInt32(lblFacturakey.Text);
                pedido.Acs_ReqOrdencop = Convert.ToInt32(lblOrdenCompraCopias.Text);
                pedido.Acs_ReqOrdencop = Convert.ToInt32(lblOrdenRepo.Text);
                pedido.ACS_ReqRemisionCop = Convert.ToInt32(lblremision.Text);
                pedido.Acs_ReqCopiaCop = Convert.ToInt32(lblCopia.Text);
                pedido.Acs_ReqDocFoliocop = Convert.ToInt32(lblFolio.Text);

                pedido.Ped_OrdenCompra = TxtPed_ReqAcys.Text;
                pedido.Id_Acs = Convert.ToInt32(txtClave.Value);
                pedido.Estatus = "U";
                pedido.Ped_Tipo = txtClave.Text == "" || txtClave.Text == "0" ? 4 : 3;

                // Edsg Proyecto Internet

                pedido.FechaFacAcys = Convert.ToDateTime(rdFechaF.Value);
                pedido.PedAcys = txtFolio.Value.ToString().Trim();
                if (TxtPed_ReqAcys.Value != null)
                {
                    if (TxtPed_ReqAcys.Value.ToString().Trim() != "")
                    {
                        pedido.ReqAcys = TxtPed_ReqAcys.Value.ToString().Trim().Trim();
                        pedido.OcAcys = TxtPed_ReqAcys.Value.ToString().Trim().Trim();
                    }
                }
                if (Request.QueryString["Anio"] != null)
                {
                    pedido.Ped_AcysAnio = Convert.ToInt32(Request.QueryString["Anio"]);
                }
                else
                {
                    pedido.Ped_AcysAnio = DateTime.Now.Year;

                }
                if (Session["Semana"] != null)
                {
                    pedido.Ped_AcysSemana = Convert.ToInt32(Session["Semana"].ToString().Trim());
                }
                else if (Request.QueryString["Semana"] != null)
                {
                    pedido.Ped_AcysSemana = Convert.ToInt32(Request.QueryString["Semana"]);
                }
                else
                {
                    CN_CatSemana Semana = new CN_CatSemana();
                    List<Semana> Lista = new List<CapaEntidad.Semana>();
                    Semana.ConsultaSemana_Anio(session.Id_Emp, int.Parse(funcion.GetLocalDateTime(session.Minutos).Year.ToString().Trim()), session.Emp_Cnx, ref Lista);

                    var query = (from tlist in Lista
                                 where tlist.Sem_FechaIni <= DateTime.Now
                                 orderby tlist.Id_Sem descending
                                 select tlist
                                 ).FirstOrDefault();

                    if (query != null)
                    {
                        pedido.Ped_AcysSemana = query.Id_Sem;
                    }
                }

                pedido.Acs_ReqDocFolio = chkFolio.Checked;
                pedido.Acs_ReqDocReposicion = ChkOrdReposicion.Checked;
                pedido.Acs_ReqDocOtro = TxtEOtro.Value;

                //Llena la informacion de compras

                pedido.Acs_Contacto2 = txtComprasNombre.Text;
                pedido.Acs_Telefono2 = txtComprasTelefono.Text;
                pedido.Acs_Email2 = txtComprasCorreo.Text;

                //Llena la información de almacen
                pedido.Acs_Contacto3 = txtAlmacenNombre.Text;
                pedido.Acs_Telefono3 = txtAlmacenTelefono.Text;
                pedido.Acs_Email3 = txtAlmacenCorreo.Text;


                //Llena la información de Mantenimiento
                pedido.Acs_Contacto4 = TxtMtoNombre.Text;
                pedido.Acs_Telefono4 = TxtMtoTelefono.Text;
                pedido.Acs_Email4 = TxtMtoCorreo.Text;


                //Llena la información de Pagos
                pedido.Acs_Contacto5 = txtPagoNombre.Text;
                pedido.Acs_Telefono5 = txtPagoTelefono.Text;
                pedido.Acs_Email5 = txtPagoCorreo.Text;
                pedido.UsoCFDI = ddUsoCfdi.Value?.ToString();

                if ((Request.QueryString["IdDireccion"] != null))
                {
                    if (Convert.ToInt32(Request.QueryString["IdDireccion"]) != -1)
                    {
                        pedido.id_cteDirEntrega = Int32.Parse(Request.QueryString["IdDireccion"].ToString());
                    }
                    else
                    {
                        pedido.id_cteDirEntrega = 0;
                    }
                }
                else
                {
                    pedido.id_cteDirEntrega = 0;
                }

                CN_CapPedidoVtaInst clsCapPedido = new CN_CapPedidoVtaInst();

                //JFCV convenios, validar el precio minimo y maximo 
                #region inicio validar precios convenio



                //JFCV 11 agosto del 2022 
                //leer configuración para ver si se valida o no el precio 
                // configuración 953 conf de Pedidos  
                // si el valor es 0 no valida y si es 1 si va a validar 
                int GLOBAL_ValidaPrecioMinimoRik = 0;
                CapaEntidad.eSysConfiguracion SysC = new CapaEntidad.eSysConfiguracion();
                try
                {
                    CN_SysConfigruacion CN = new CN_SysConfigruacion();
                    SysC = CN.spSysConfiguracionById(session.Id_Emp, session.Id_Cd, 953, session.Emp_Cnx);
                    int iTmp = 0;
                    int.TryParse(SysC.Conf_Valor, out iTmp);
                    GLOBAL_ValidaPrecioMinimoRik = iTmp;
                }
                catch (Exception ex)
                {
                    GLOBAL_ValidaPrecioMinimoRik = 0;
                }
                string ConexionCentral = ConfigurationManager.AppSettings["strConnectionCentral"].ToString().Trim();
                ConvenioDet conv;
                ConvenioDet convdet;
                CN_Convenio cn_conv;
                List<string> Productos = new List<string>();

                List<ConvenioDet> lconveniosdet = new List<ConvenioDet>();
                ConvenioDet lconvdet = new ConvenioDet();

                string prodAAA = "";
                //JFCV Validar si tiene precios inferiores a los precios minimos
                AlertaAutorizacion alertaaut;
                AlertaAutorizacion alertaautdet;
                CN_AlertaAutorizacion cn_alertaautorizacion;

                List<string> ProductosAlerta = new List<string>();
                List<AlertaAutorizacion> lalertasautdet = new List<AlertaAutorizacion>();
                AlertaAutorizacion pasoalertaautdet = new AlertaAutorizacion();

                //reviso si la variable de sesion ya tiene un folio guardado
                //si no lo tiene es que e sla primera vez que entra a grabar 
                //si ya lo tiene valido que sea el mismo que el que estoy 
                int procesar = 1;

                if (Session["Id_FacPrec" + Session.SessionID] != null)
                {
                    string folior = Session["Id_FacPrec" + Session.SessionID].ToString();
                    if (folior == txtFolio.Value.ToString().Trim())
                    {
                        procesar = 0;
                    }
                }

                //la primera vez que entra a esta rutina guarda en variable de sesion 
                //el folio si acaso tiene productos que requirieron de validación
                //entonces la segunda vez que entra ya esta ese dato guardado 
                //y si el folio es el mismo quiere decir que se reejecuto el grabar y que ya no debe validar.


                //JFCV  fin
                if (dt.Rows.Count > 0)
                {
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {

                        conv = new ConvenioDet();
                        convdet = new ConvenioDet();
                        cn_conv = new CN_Convenio();
                        conv.Id_Emp = session.Id_Emp;
                        conv.Id_Cd = session.Id_Cd_Ver;
                        conv.Id_Cte = Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1");
                        conv.Id_Prd = Convert.ToInt64(dt.Rows[x]["Id_Prd"]);
                        double PrecioIngresado = Convert.ToDouble(dt.Rows[x]["Prd_Precio"]);
                        //double impore =  dt.Rows[x]["Prd_Precio"].ToString().Trim()!= DBNull.Value  ? (double)(dt.Rows[x]["Prd_Precio"]) : 0;

                        if (Convert.ToInt64(dt.Rows[x]["Id_Prd"]) <= 999999999999)
                        {
                            //double PrecioIngresado = Convert.ToDouble(item2.OwnerTableView.DataKeyValues[item2.ItemIndex]["Prd_Importe"]);
                            //double impore = (item2.FindControl("Prd_Importe") as RadNumericTextBox).Value.HasValue ? (double)(item2.FindControl("Prd_Importe") as RadNumericTextBox).Value : 0;
                            cn_conv.Convenio_ConsultaPrecio(conv, ref convdet, ConexionCentral);



                            int agregar = 0;

                            if (convdet != null && convdet.PCD_PrecioAAAEsp > 0)
                            {
                                if (convdet.PCD_PrecioVtaMin != 0 || convdet.PCD_PrecioVtaMax != 0)
                                {
                                    #region pvtamin y pvtamax  dif de cero 
                                    if (Math.Round(PrecioIngresado, 3) < convdet.PCD_PrecioVtaMin)
                                    {
                                        agregar = 1;
                                        /* if (Convert.ToDouble(PrecioIngresado) < convdet.PCD_PrecioAAAEsp)
                                         {
                                             if (prodAAA != "")
                                             {
                                                 prodAAA = prodAAA + ", " + Convert.ToInt64(dt.Rows[x]["Id_Prd"]).ToString();
                                             }
                                             else
                                             {
                                                 prodAAA = Convert.ToInt64(dt.Rows[x]["Id_Prd"]).ToString();
                                             }
                                       }*/
                                    }
                                    else if (convdet.PCD_PrecioVtaMin == 0 && convdet.PCD_PrecioVtaMax != 0)
                                    {
                                        // vta minima es igual a cero y vta max dif 0 
                                        // si precio es mayor a vta max manda aviso 
                                        if (Math.Round(PrecioIngresado, 3) > convdet.PCD_PrecioVtaMax)
                                        {
                                            agregar = 1;
                                        }
                                    }
                                    else if (Math.Round(PrecioIngresado, 3) > convdet.PCD_PrecioVtaMax)
                                    {
                                        agregar = 1;
                                    }

                                    if (agregar == 1)
                                    {
                                        Productos.Add(convdet.Id_Prd.ToString().Trim());

                                        lconvdet = new ConvenioDet();
                                        lconvdet.PC_Nombre = convdet.PC_Nombre;
                                        lconvdet.Id_PC = convdet.Id_PC;
                                        lconvdet.Id_Prd = convdet.Id_Prd;
                                        lconvdet.PCD_PrecioVtaMax = convdet.PCD_PrecioVtaMax;
                                        lconvdet.PCD_PrecioVtaMin = convdet.PCD_PrecioVtaMin;
                                        lconvdet.PCD_PrecioVentaConvenio = PrecioIngresado;
                                        lconveniosdet.Add(lconvdet);
                                    }
                                    #endregion
                                }  // si pvtamin y pvtamax son cero
                            }
                            else
                            {
                                Producto producto = new Producto();
                                //obtener datos de producto
                                CN_CatProducto clsProducto = new CN_CatProducto();
                                producto.Id_Cte = Convert.ToInt32(Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1"));
                                int productoNuevo = 0;
                                clsProducto.ConsultaProductos(ref producto, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, Convert.ToInt64(dt.Rows[x]["Id_Prd"]), ref productoNuevo, 0);

                                if (Convert.ToDouble(PrecioIngresado) < producto.Prd_AAA)
                                {
                                    if (prodAAA != "")
                                    {
                                        prodAAA = prodAAA + ", " + Convert.ToInt64(dt.Rows[x]["Id_Prd"]).ToString();
                                    }
                                    else
                                    {
                                        prodAAA = Convert.ToInt64(dt.Rows[x]["Id_Prd"]).ToString();
                                    }

                                }
                            }
                            //JFCV Valido si el precio de venta es menor al minimo
                            #region validar precios que requieran autorización

                            //JFCV 11 abril 2023 Los precios de cuentas nacionales no deben validarse , así que comento esta parte 
                            //donde recorre el grid Rg1 que es el datatable dt , ya que esos productos son cuentas nacionales y no debe valiarlos  

                            //if (GLOBAL_ValidaPrecioMinimoRik == 1)
                            //{
                            //    alertaaut = new AlertaAutorizacion();
                            //    alertaautdet = new AlertaAutorizacion();
                            //    cn_alertaautorizacion = new CN_AlertaAutorizacion();

                            //    if (procesar == 1)
                            //    {
                            //        alertaaut.Id_Emp = session.Id_Emp;
                            //        alertaaut.Id_Cd = session.Id_Cd_Ver;
                            //        alertaaut.Id_Cte = Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1");
                            //        alertaaut.Cte_NomComercial = ddlClienteNom.Text.Trim() != "" ? ddlClienteNom.Text.Trim() : "";
                            //        alertaaut.Id_Prd = Convert.ToInt64(dt.Rows[x]["Id_Prd"]);
                            //        alertaaut.IdRepresentante = Convert.ToInt32(txtIdRik.Value);
                            //        alertaaut.Id_Ter = Convert.ToInt32(txtIdTer.Value);
                            //        double Preciodefactura = Convert.ToDouble(dt.Rows[x]["Prd_Precio"]);
                            //        alertaaut.Precio_Vta = Preciodefactura;
                            //        cn_alertaautorizacion.AlertaPrecioConsultaPrecio(alertaaut, ref alertaautdet, ConexionCentral);

                            //        if (alertaautdet != null && alertaautdet.Precio_MinimoRik > 0)
                            //        {
                            //            if (Math.Round(Preciodefactura, 3) < alertaautdet.PrecioObjetivo)
                            //            {

                            //                ProductosAlerta.Add(alertaautdet.Id_Prd.ToString().Trim());

                            //                pasoalertaautdet = new AlertaAutorizacion();

                            //                pasoalertaautdet.Id_Emp = session.Id_Emp;
                            //                pasoalertaautdet.Id_Cd = session.Id_Cd_Ver;
                            //                pasoalertaautdet.Id_Cte = Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1");
                            //                pasoalertaautdet.IdRepresentante = Convert.ToInt32(txtIdRik.Value);
                            //                pasoalertaautdet.Id_Ter = Convert.ToInt32(txtIdTer.Value);
                            //                pasoalertaautdet.IdAutorizacionAnterior = alertaautdet.IdAutorizacionAnterior;
                            //                pasoalertaautdet.Id_Prd = alertaautdet.Id_Prd;
                            //                pasoalertaautdet.Precio_MinimoRik = Math.Round(Convert.ToDouble(alertaautdet.Precio_MinimoRik), 3);
                            //                pasoalertaautdet.Precio_MinimoGte = Math.Round(Convert.ToDouble(alertaautdet.Precio_MinimoGte), 3);
                            //                pasoalertaautdet.Precio_Vta = Preciodefactura;
                            //                pasoalertaautdet.PrecioLista = alertaautdet.PrecioLista;
                            //                pasoalertaautdet.Cte_NomComercial = alertaaut.Cte_NomComercial;
                            //                pasoalertaautdet.Prd_Descripcion = alertaautdet.Prd_Descripcion;
                            //                pasoalertaautdet.PrecioLista = alertaautdet.PrecioLista;
                            //                pasoalertaautdet.Cantidad = Convert.ToInt32(dt.Rows[x]["Prd_Cantidad"]);
                            //                pasoalertaautdet.Precio_AAA = alertaautdet.Precio_AAA;
                            //                pasoalertaautdet.Utilidad = alertaautdet.Utilidad;
                            //                pasoalertaautdet.Porc_Utilidad = alertaautdet.Utilidad / Preciodefactura;
                            //                pasoalertaautdet.Importe = Preciodefactura * pasoalertaautdet.Cantidad;
                            //                pasoalertaautdet.Importe_Utilidad = pasoalertaautdet.Utilidad * pasoalertaautdet.Cantidad;
                            //                pasoalertaautdet.Justificacion = "";
                            //                pasoalertaautdet.Prd_Descripcion = Convert.ToString(dt.Rows[x]["Prd_Descripcion"]);
                            //                pasoalertaautdet.Cte_NomComercial = ddlClienteNom.Text.Trim() != "" ? ddlClienteNom.Text.Trim() : "";
                            //                pasoalertaautdet.TipoAutorizacion = 4; //Pedido
                            //                pasoalertaautdet.FechaVigencia = DateTime.Now.AddMonths(12);
                            //                pasoalertaautdet.Id_Cpr = alertaautdet.Id_Cpr;
                            //                pasoalertaautdet.JustificacionMemo = "";
                            //                pasoalertaautdet.Id_Tamaño = alertaautdet.Id_Tamaño;
                            //                pasoalertaautdet.PrecioObjetivo = alertaautdet.PrecioObjetivo;
                            //                lalertasautdet.Add(pasoalertaautdet);

                            //            }

                            //}
                            //}
                            //}
                            #endregion validar precios que requieran autorización
                            //JFCV fin Valido si el precio de venta es menor al minimo
                        }// si prod es menor a 999999999999
                    }  //Termina ciclo  convenios  
                }  // si tiene artículos 

                //valido también los productos de este otro dataset 
                //JFCV 11abril que son del grid rg1_Restos 
                if (dtTemp.Rows.Count > 0)
                {

                    for (int x = 0; x < dtTemp.Rows.Count; x++)
                    {

                        conv = new ConvenioDet();
                        convdet = new ConvenioDet();
                        cn_conv = new CN_Convenio();
                        conv.Id_Emp = session.Id_Emp;
                        conv.Id_Cd = session.Id_Cd_Ver;
                        conv.Id_Cte = Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1");
                        conv.Id_Prd = Convert.ToInt64(dtTemp.Rows[x]["Id_Prd"]);
                        double PrecioIngresado = Convert.ToDouble(dtTemp.Rows[x]["Prd_Precio"]);
                        //double impore =  dtTemp.Rows[x]["Prd_Precio"].ToString().Trim()!= DBNull.Value  ? (double)(dtTemp.Rows[x]["Prd_Precio"]) : 0;

                        if (Convert.ToInt64(dtTemp.Rows[x]["Id_Prd"]) <= 999999999999)
                        {
                            //double PrecioIngresado = Convert.ToDouble(item2.OwnerTableView.DataKeyValues[item2.ItemIndex]["Prd_Importe"]);
                            //double impore = (item2.FindControl("Prd_Importe") as RadNumericTextBox).Value.HasValue ? (double)(item2.FindControl("Prd_Importe") as RadNumericTextBox).Value : 0;
                            cn_conv.Convenio_ConsultaPrecio(conv, ref convdet, ConexionCentral);



                            int agregar = 0;

                            if (convdet != null && convdet.PCD_PrecioAAAEsp > 0)
                            {
                                if (convdet.PCD_PrecioVtaMin != 0 || convdet.PCD_PrecioVtaMax != 0)
                                {
                                    #region pvtamin y pvtamax  dif de cero 
                                    if (Math.Round(PrecioIngresado, 3) < convdet.PCD_PrecioVtaMin)
                                    {
                                        agregar = 1;
                                        /*if (Convert.ToDouble(PrecioIngresado) < convdet.PCD_PrecioAAAEsp)
                                        {
                                            if (prodAAA != "")
                                            {
                                                prodAAA = prodAAA + ", " + Convert.ToInt64(dtTemp.Rows[x]["Id_Prd"]).ToString();
                                            }
                                            else
                                            {
                                                prodAAA = Convert.ToInt64(dtTemp.Rows[x]["Id_Prd"]).ToString();
                                            }
                            Juan Jo Lo que solicitamos es que la regla siga aplicando de manera general, pero en caso de que el cliente este ligado a algún convenio entonces si le permita facturar por debajo del precio aaa especial.
                            Autorizado por Dirección
                                            
                                    }
                                    */
                                    }
                                    else if (convdet.PCD_PrecioVtaMin == 0 && convdet.PCD_PrecioVtaMax != 0)
                                    {
                                        // vta minima es igual a cero y vta max dif 0 
                                        // si precio es mayor a vta max manda aviso 
                                        if (Math.Round(PrecioIngresado, 3) > convdet.PCD_PrecioVtaMax)
                                        {
                                            agregar = 1;
                                        }
                                    }
                                    else if (Math.Round(PrecioIngresado, 3) > convdet.PCD_PrecioVtaMax)
                                    {
                                        agregar = 1;
                                    }

                                    if (agregar == 1)
                                    {
                                        Productos.Add(convdet.Id_Prd.ToString().Trim());

                                        lconvdet = new ConvenioDet();
                                        lconvdet.PC_Nombre = convdet.PC_Nombre;
                                        lconvdet.Id_PC = convdet.Id_PC;
                                        lconvdet.Id_Prd = convdet.Id_Prd;
                                        lconvdet.PCD_PrecioVtaMax = convdet.PCD_PrecioVtaMax;
                                        lconvdet.PCD_PrecioVtaMin = convdet.PCD_PrecioVtaMin;
                                        lconvdet.PCD_PrecioVentaConvenio = PrecioIngresado;
                                        lconveniosdet.Add(lconvdet);
                                    }
                                    #endregion
                                }  // si pvtamin y pvtamax son cero
                            }
                            else
                            {
                                Producto producto = new Producto();
                                //obtener datos de producto
                                CN_CatProducto clsProducto = new CN_CatProducto();
                                producto.Id_Cte = Convert.ToInt32(Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1"));
                                int productoNuevo = 0;
                                clsProducto.ConsultaProductos(ref producto, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, Convert.ToInt64(dtTemp.Rows[x]["Id_Prd"]), ref productoNuevo, 0);

                                if (Convert.ToDouble(PrecioIngresado) < producto.Prd_AAA)
                                {
                                    if (prodAAA != "")
                                    {
                                        prodAAA = prodAAA + ", " + Convert.ToInt64(dtTemp.Rows[x]["Id_Prd"]).ToString();
                                    }
                                    else
                                    {
                                        prodAAA = Convert.ToInt64(dtTemp.Rows[x]["Id_Prd"]).ToString();
                                    }

                                }
                            }

                            //JFCV Valido si el precio de venta es menor al minimo
                            #region validar precios que requieran autorización
                            if (GLOBAL_ValidaPrecioMinimoRik == 1)
                            {
                                if (procesar == 1)
                                {
                                    alertaaut = new AlertaAutorizacion();
                                    alertaautdet = new AlertaAutorizacion();
                                    cn_alertaautorizacion = new CN_AlertaAutorizacion();
                                    alertaaut.Id_Emp = session.Id_Emp;
                                    alertaaut.Id_Cd = session.Id_Cd_Ver;
                                    alertaaut.Id_Cte = Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1");
                                    alertaaut.Cte_NomComercial = ddlClienteNom.Text.Trim() != "" ? ddlClienteNom.Text.Trim() : "";
                                    alertaaut.Id_Prd = Convert.ToInt64(dtTemp.Rows[x]["Id_Prd"]);
                                    alertaaut.IdRepresentante = Convert.ToInt32(txtIdRik.Value);
                                    alertaaut.Id_Ter = Convert.ToInt32(txtIdTer.Value);
                                    double Preciodefactura = Convert.ToDouble(dtTemp.Rows[x]["Prd_Precio"]);
                                    alertaaut.Precio_Vta = Preciodefactura;
                                    cn_alertaautorizacion.AlertaPrecioConsultaPrecio(alertaaut, ref alertaautdet, ConexionCentral);

                                    if (alertaautdet != null && alertaautdet.Precio_MinimoRik > 0)
                                    {

                                        //JFCV 2NOV precioObjetivo if (Math.Round(Preciodefactura, 3) < alertaautdet.Precio_MinimoRik)
                                        if (Math.Round(Preciodefactura, 3) < alertaautdet.PrecioObjetivo)
                                        {
                                            ProductosAlerta.Add(alertaautdet.Id_Prd.ToString().Trim());
                                            pasoalertaautdet = new AlertaAutorizacion();

                                            pasoalertaautdet.Id_Emp = session.Id_Emp;
                                            pasoalertaautdet.Id_Cd = session.Id_Cd_Ver;
                                            pasoalertaautdet.Id_Cte = Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1");
                                            pasoalertaautdet.IdRepresentante = Convert.ToInt32(txtIdRik.Value);
                                            pasoalertaautdet.Id_Ter = Convert.ToInt32(txtIdTer.Value);  //jfcv 22sep Convert.ToInt32(ddlTerritorioNom.Value);
                                            pasoalertaautdet.IdAutorizacionAnterior = alertaautdet.IdAutorizacionAnterior;
                                            pasoalertaautdet.Id_Prd = alertaautdet.Id_Prd;
                                            pasoalertaautdet.Precio_MinimoRik = Math.Round(Convert.ToDouble(alertaautdet.Precio_MinimoRik), 3);
                                            pasoalertaautdet.Precio_MinimoGte = Math.Round(Convert.ToDouble(alertaautdet.Precio_MinimoGte), 3);
                                            pasoalertaautdet.Precio_Vta = Preciodefactura;
                                            pasoalertaautdet.PrecioLista = alertaautdet.PrecioLista;
                                            pasoalertaautdet.Cte_NomComercial = alertaaut.Cte_NomComercial;
                                            pasoalertaautdet.Prd_Descripcion = alertaautdet.Prd_Descripcion;
                                            pasoalertaautdet.PrecioLista = alertaautdet.PrecioLista;
                                            pasoalertaautdet.Cantidad = Convert.ToInt32(dtTemp.Rows[x]["Prd_Cantidad"]);
                                            pasoalertaautdet.Precio_AAA = alertaautdet.Precio_AAA;
                                            pasoalertaautdet.Utilidad = alertaautdet.Utilidad;
                                            pasoalertaautdet.Porc_Utilidad = alertaautdet.Utilidad / Preciodefactura;
                                            pasoalertaautdet.Importe = Preciodefactura * pasoalertaautdet.Cantidad;
                                            pasoalertaautdet.Importe_Utilidad = pasoalertaautdet.Utilidad * pasoalertaautdet.Cantidad;
                                            pasoalertaautdet.Justificacion = "";
                                            pasoalertaautdet.Prd_Descripcion = Convert.ToString(dtTemp.Rows[x]["Prd_Descripcion"]);
                                            pasoalertaautdet.Cte_NomComercial = ddlClienteNom.Text.Trim() != "" ? ddlClienteNom.Text.Trim() : "";
                                            pasoalertaautdet.TipoAutorizacion = 4; //Pedido
                                            pasoalertaautdet.FechaVigencia = DateTime.Now.AddMonths(12);
                                            pasoalertaautdet.Id_Cpr = alertaautdet.Id_Cpr;
                                            pasoalertaautdet.JustificacionMemo = "";
                                            //JFCV 2NOV precioObjetivo 
                                            pasoalertaautdet.Id_Tamaño = alertaautdet.Id_Tamaño;
                                            pasoalertaautdet.PrecioObjetivo = alertaautdet.PrecioObjetivo;
                                            lalertasautdet.Add(pasoalertaautdet);

                                        }

                                    }
                                }
                            }
                            #endregion validar precios que requieran autorización
                            //JFCV fin Valido si el precio de venta es menor al minimo
                        }// si prod es menor a 999999999999
                    }  //Termina ciclo  convenios  
                }// si tiene artículos 


                if (prodAAA != "")
                {
                    mensajeInfo("El precio de venta de los siguiente produtos no puede ser menor al Precio AAA del producto: " + prodAAA);
                    return;
                }

                Session["ProdsConv" + Session.SessionID] = null;
                Session["Id_FacPrec" + Session.SessionID] = null;
                Session["lConvPrecios" + Session.SessionID] = null;

                if (Productos.Count > 0 && lconveniosdet.Count > 0)
                {
                    Session["ProdsConv" + Session.SessionID] = Productos;
                    Session["lConvPrecios" + Session.SessionID] = lconveniosdet;
                    Session["Id_FacPrec" + Session.SessionID] = txtFolio.Value.ToString().Trim();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirVentana_AlertaPrecios", "AbrirVentana_AlertaPrecios()", true);
                    return;
                }

                //JFCV  alerta de precios autorización
                //JFCV  Validar si tiene precios inferiores a los precios minimos

                Session["ProdsAutorizacion" + Session.SessionID] = null;
                Session["Id_FacPrec" + Session.SessionID] = null;
                Session["lAurizacionPrecios" + Session.SessionID] = null;

                if (procesar == 1) //23sep22 agregue esta condición para que cuando este regrabando no se detenga por los precios alertas
                {
                    if (ProductosAlerta.Count > 0 && lalertasautdet.Count > 0)
                    {
                        Session["ProdsAutorizacion" + Session.SessionID] = ProductosAlerta;
                        Session["lAurizacionPrecios" + Session.SessionID] = lalertasautdet;
                        Session["Id_FacPrec" + Session.SessionID] = txtFolio.Value.ToString().Trim();



                        ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirVentana_AlertaAutorizacionPrecios", "AbrirVentana_AlertaAutorizacionPrecios()", true);


                        return;
                    }
                }
                //JFCV  FIn Validar si tiene precios inferiores a los precios minimos


                #endregion inicio validar precios convenio


                Session["Id_FacPrec" + Session.SessionID] = null;
                if (HF_ID.Value == "")
                {
                    if (!_PermisoGuardar)
                    {
                        mensajeInfo("No tiene permisos para grabar");
                        return;
                    }


                    long _prd = 0;
                    foreach (DataRow rows in dt.AsEnumerable())
                    {
                        _prd = Convert.ToInt64((rows[1]));
                        PedidoVtaInst pvi = new PedidoVtaInst();
                        pvi.Id_Emp = session.Id_Emp;
                        pvi.Id_Cd = session.Id_Cd_Ver;
                        pvi.Id_Cte = Convert.ToInt32(txtIdCte.Value);
                        pvi.Id_Ter = Convert.ToInt32(txtIdTer.Value);
                        pvi.Id_Acs = Convert.ToInt32(txtClave.Value);
                        clsCapPedido.ConsultarPedidoExistente(pvi, _prd, session.Emp_Cnx, ref verificador);

                        if (verificador == 1)
                        {
                            mensajeInfo("El producto " + _prd.ToString().Trim() + " ya ha sido captado por otro usuario");

                            return;
                        }
                    }


                    foreach (DataRow rows in dtTemp.AsEnumerable())
                    {
                        _prd = Convert.ToInt64((rows[1]));
                        PedidoVtaInst pvi = new PedidoVtaInst();
                        pvi.Id_Emp = session.Id_Emp;
                        pvi.Id_Cd = session.Id_Cd_Ver;
                        pvi.Id_Cte = Convert.ToInt32(txtIdCte.Value);
                        pvi.Id_Ter = Convert.ToInt32(txtIdTer.Value);
                        pvi.Id_Acs = Convert.ToInt32(txtClave.Value);
                        clsCapPedido.ConsultarPedidoExistente(pvi, _prd, session.Emp_Cnx, ref verificador);

                        if (verificador == 1)
                        {
                            mensajeInfo("El producto " + _prd.ToString().Trim() + " ya ha sido captado por otro usuario");

                            return;
                        }
                    }

                    string nombre = "";
                    string extension = "";
                    string archivo = "";
                    if (Request.QueryString["OrdenCompra"] != null)
                    {

                        if (Convert.ToBoolean(Request.QueryString["OrdenCompra"].ToString().Trim()))
                        {
                            cargarArchivo(ref nombre, ref extension, ref archivo);

                        }

                    }


                    if (Convert.ToInt32(txtClave.Text) != 0)
                    {
                        CapaEntidad.Acys acys = new Acys();
                        acys.Id_Emp = session.Id_Emp;
                        acys.Id_Cd = session.Id_Cd;
                        acys.Id_Acs = Convert.ToInt32(txtClave.Text);
                        CN_CapAcys cnCapAcys = new CN_CapAcys();
                        cnCapAcys.ConsultaUltimaVersion(ref acys, session.Emp_Cnx);

                        clsCapPedido.Insertar(pedido, dt, dtTemp, session.Emp_Cnx, ref verificador, Id_TG, acys.Id_AcsVersion);

                        if (verificador >= 1)
                        {
                            SetLastPedidoHash(currentHash);
                            if (Request.QueryString["OrdenCompra"] != null)
                            {
                                if (Convert.ToBoolean(Request.QueryString["OrdenCompra"].ToString().Trim()))
                                {

                                    pedido.Id_Ped = verificador;
                                    pedido.Id_Acs = Convert.ToInt32(txtClave.Text);
                                    pedido.OrdenCompra = TxtPed_ReqAcys.Text;
                                    pedido.Id_Cte = Convert.ToInt32(txtIdCte.Value);
                                    pedido.nombreDocumento = nombre;
                                    pedido.extension = extension;
                                    pedido.archivo = archivo;
                                    clsCapPedido.InsertarOrderCompra(pedido, session.Emp_Cnx, ref verificador, Id_TG, acys.Id_AcsVersion);
                                }
                            }


                            Session["Id_Ped" + Session.SessionID] = verificador;

                            if (Request.QueryString["IdAutorizacion"] != null)
                            {
                                int id_Sol = Convert.ToInt32(Request.QueryString["IdAutorizacion"].ToString().Trim());
                                CaptarPedido(id_Sol, verificador);
                            }

                            mensajeExitoClaro("Se realizo la captación del pedido con el folio: " + verificador.ToString().Trim().Trim());


                        }
                        else
                        {
                            mensajeInfo("Ocurrió un error al intentar guardar el pedido");
                        }

                    }
                    else
                    {
                        clsCapPedido.Insertar(pedido, dt, dtTemp, session.Emp_Cnx, ref verificador, Id_TG, null);

                        if (verificador >= 1)
                        {
                            SetLastPedidoHash(currentHash);
                            if (Request.QueryString["OrdenCompra"] != null)
                            {
                                if (Convert.ToBoolean(Request.QueryString["OrdenCompra"].ToString().Trim()))
                                {

                                    pedido.Id_Ped = verificador;
                                    pedido.Id_Acs = Convert.ToInt32(txtClave.Text);
                                    pedido.OrdenCompra = TxtPed_ReqAcys.Text;
                                    pedido.Id_Cte = Convert.ToInt32(txtIdCte.Value);
                                    pedido.nombreDocumento = nombre;
                                    pedido.extension = extension;
                                    pedido.archivo = archivo;
                                    clsCapPedido.InsertarOrderCompra(pedido, session.Emp_Cnx, ref verificador, Id_TG, 0);
                                }
                            }

                            int cliente = 0;
                            int Usuario = 0;
                            Session["Id_Ped" + Session.SessionID] = verificador;
                            clsCapPedido.guardarInformacionPedidosSinAcys(pedido, session.Emp_Cnx, ref cliente, ref Usuario);
                            string mensajes = "";
                            //EnviarCorreo(ddlClienteNom.SelectedItem.Text, session.U_Nombre, cliente, Usuario, session.Emp_Cnx, mensajes);

                            if (mensajes != "")
                            {
                                if (Request.QueryString["IdAutorizacion"] != null)
                                {
                                    int id_Sol = Convert.ToInt32(Request.QueryString["IdAutorizacion"].ToString().Trim());
                                    CaptarPedido(id_Sol, verificador);
                                }
                                mensajeExitoClaro("Se realizo la captación del pedido con el folio: " + verificador.ToString().Trim().Trim() + ". <br> " + mensajes);
                            }
                            else
                            {
                                if (Request.QueryString["IdAutorizacion"] != null)
                                {
                                    int id_Sol = Convert.ToInt32(Request.QueryString["IdAutorizacion"].ToString().Trim());
                                    CaptarPedido(id_Sol, verificador);
                                }
                                mensajeExitoClaro("Se realizo la captación del pedido con el folio: " + verificador.ToString().Trim().Trim());
                            }
                            btncaptacion_Guardar.Enabled = false;
                        }
                        else
                        {
                            mensajeError("Ocurrió un error al intentar guardar el pedido");
                        }
                    }

                }

                else
                {
                    if (!_PermisoModificar)
                    {
                        mensajeInfo("No tiene permisos para modificar");
                        return;
                    }


                    pedido.Id_Ped = Convert.ToInt32(HF_ID.Value);
                    int captado = 0;
                    if (Request.QueryString["IdVI"] != null)
                        captado = Convert.ToInt32(txtFolio.Value);

                    clsCapPedido.Modificar(pedido, dt, dtTemp, session.Emp_Cnx, captado, ref verificador, al, Id_TG);

                    if (verificador >= 1)
                    {
                        SetLastPedidoHash(currentHash);
                        Session["Id_Ped" + Session.SessionID] = verificador;
                        if (Request.QueryString["IdAutorizacion"] != null)
                        {
                            int id_Sol = Convert.ToInt32(Request.QueryString["IdAutorizacion"].ToString().Trim());
                            CaptarPedido(id_Sol, verificador);
                        }
                        mensajeExitoClaro("Se actualizo la información de el pedido: " + verificador);

                        btncaptacion_Guardar.Enabled = false;
                    }
                    else
                    {
                        mensajeError("Ocurrió un error al intentar guardar el pedido");
                    }
                }
            }
            catch (Exception ex)
            {

                System.Diagnostics.Trace.TraceError($"Guardar: fallo. Sesión={Session.SessionID}, Mensaje={ex.Message}");
                mensajeError("No se pudo completar el guardado. Verifique su conexión y reintente.");
                // Permite reintento liberando huella
                ClearLastPedidoHash();
                throw;
            }
        }

        private void CalcularTotales()
        {
            try
            {
                double subtotal = 0;

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    subtotal += Convert.ToDouble(dt.Rows[x]["Prd_Importe"]);
                }

                for (int x = 0; x < dt_Resto.Rows.Count; x++)
                {
                    subtotal += Convert.ToDouble(dt.Rows[x]["Prd_Importe"]);
                }

                double iva = subtotal * Convert.ToDouble(HD_IVAfacturacion.Value.ToString()) / 100;
                double total = subtotal + iva;

                txtSubtotal.Value = subtotal.ToString("F2"); ;
                txtIva.Value = iva.ToString("F2"); ;
                txtTotal.Value = total.ToString("F2"); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void deshabilitarcontroles(System.Web.UI.ControlCollection controles_contenidos)
        {
            for (int x = 0; x < controles_contenidos.Count; x++)
            {
                string Type = controles_contenidos[x].GetType().FullName;

                if (Type.Contains("RadMultiPage") || Type.Contains("RadPageView") || Type.Contains("Panel"))
                {
                    deshabilitarcontroles(controles_contenidos[x].Controls);
                }

                switch (Type.Replace("Telerik.Web.UI.", ""))
                {
                    case "RadNumericTextBox":
                        (controles_contenidos[x] as RadNumericTextBox).Enabled = false;
                        break;
                    case "RadTextBox":
                        (controles_contenidos[x] as RadTextBox).Enabled = false;
                        break;
                    case "RadComboBox":
                        (controles_contenidos[x] as RadComboBox).Enabled = false;
                        break;
                    case "RadDatePicker":
                        (controles_contenidos[x] as RadDatePicker).Enabled = false;
                        break;
                    case "RadDateTimePicker":
                        (controles_contenidos[x] as RadDateTimePicker).Enabled = false;
                        break;
                }
                if (Type.Contains("CheckBox"))
                {
                    (controles_contenidos[x] as CheckBox).Enabled = false;
                }

                if (Type.Contains("ImageButton"))
                {
                    (controles_contenidos[x] as ImageButton).Enabled = false;
                }
            }
        }

        private void EnviarCorreo(string Cliente, string Usuario, int cantidadCliente, int cantidadUsuario, string conexion, string mensage)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                PedidoVtaInst pedido = new PedidoVtaInst();
                List<UsuarioCorreo> ListaCorreoUsuario = new List<UsuarioCorreo>();

                int Vcliente = 0;
                int VUsuario = 0;
                if (cantidadCliente == 4)
                {
                    Vcliente = 1;

                }
                else if (cantidadCliente == 6)
                {
                    Vcliente = 2;
                }

                if (cantidadUsuario == 4)
                {
                    VUsuario = 1;

                }
                else if (cantidadUsuario == 6)
                {
                    VUsuario = 2;
                }

                if (Vcliente != 0 || VUsuario != 0)
                {
                    CN_enviarCorreo Envia = new CN_enviarCorreo();
                    Envia.ConsultarCorreoUsuario(session.Id_Emp, session.Id_Cd, conexion, ref ListaCorreoUsuario);
                    string fecha = DateTime.Now.ToString("dd/MM/yyyy");
                    ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                    configuracion.Id_Cd = session.Id_Emp;
                    configuracion.Id_Emp = session.Id_Cd;
                    CN_Configuracion cn_configuracion = new CN_Configuracion();
                    cn_configuracion.Consulta(ref configuracion, conexion);
                    StringBuilder cuerpo_correo = new StringBuilder();

                    cuerpo_correo.Append("<div align='center'>");
                    cuerpo_correo.Append("<table><tr><td>");
                    cuerpo_correo.Append("<td></td>");
                    cuerpo_correo.Append("</tr><tr><td colspan='2'><br><br><br></td>");
                    cuerpo_correo.Append("</tr><tr>");
                    cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='2'>");

                    cuerpo_correo.Append("Se les comunica por este medio. <br> ");
                    if (Vcliente != 0)
                    {
                        cuerpo_correo.Append("Que el usuario:" + Cliente + ". <br>");
                        cuerpo_correo.Append("A realizado: " + (Vcliente == 1 ? 5 : 10) + " veces captacion de pedidos a clientes que no cuentan con ACYS. <br>");

                    }
                    if (VUsuario != 0)
                    {
                        cuerpo_correo.Append("Que el cliente:" + Usuario + ". <br>");
                        cuerpo_correo.Append("A solicitado: " + (VUsuario == 1 ? 5 : 10) + " pedidos por este medio a la sucursal. <br>");
                        cuerpo_correo.Append("Revisar si el usuario no requiere un Acys. <br>");
                    }
                    cuerpo_correo.Append("Favor de tomar la medidas correspondientes. <br>");
                    cuerpo_correo.Append("Sistema automático de reporte de captación de pedido.  Fecha de realización: " + fecha + "<br>");

                    cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");
                    cuerpo_correo.Append("<center><br>");
                    cuerpo_correo.Append("</td></tr></table></div>");
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                    SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                    sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);

                    MailMessage m = new MailMessage();
                    m.From = new MailAddress(configuracion.Mail_Remitente, "Captacion de pedido");
                    m.Subject = "Reporte ";
                    foreach (UsuarioCorreo correo in ListaCorreoUsuario)
                    {
                        m.To.Add(new MailAddress(correo.U_Correo));
                    }

                    if (Vcliente == 2 || VUsuario == 2)
                    {
                        m.To.Add(new MailAddress("alejandra.benavente@key.com.mx"));
                        m.To.Add(new MailAddress("rafael.vazquez@key.com.mx"));
                        m.To.Add(new MailAddress("erikrgc@hotmail.com"));
                    }

                    m.IsBodyHtml = true;

                    string body = cuerpo_correo.ToString().Trim();
                    AlternateView vistaHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);

                    m.AlternateViews.Add(vistaHtml);
                    try
                    {
                        sm.Send(m);
                    }
                    catch (Exception)
                    {
                        mensage = "Fallo en enviar el correo electronico";
                    }
                }
            }
            catch (Exception)
            {
                mensage = "Fallo en la configuración del sistema";
            }
        }






        private void Consultar_IVA_Cliente()
        {
            string IVA_Cliente = "NO";
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (txtIdCte.Value.ToString() != string.Empty && txtIdCte.Value.ToString() != "-1")
            {
                Clientes cliente = new Clientes();
                cliente.Id_Emp = sesion.Id_Emp;
                cliente.Id_Cd = sesion.Id_Cd_Ver;
                cliente.Id_Rik = sesion.Id_Rik;
                cliente.Id_Cte = Convert.ToInt32(txtIdCte.Value.ToString());
                new CN_CatCliente().ConsultaClientes(ref cliente, sesion.Emp_Cnx);

                if (cliente.BPorcientoIVA == true)
                {
                    if (cliente.PorcientoIVA == 0 || cliente.PorcientoIVA == null)
                    {
                        mensajeInfo("El porcentaje de IVA no está establecido, debe ser Mayor a Cero");
                        return;
                    }
                    else
                    {
                        HD_IVAfacturacion.Value = cliente.PorcientoIVA.ToString();
                        IVATEXTO.Text = "IVA : " + cliente.PorcientoIVA.ToString() + "%";
                        IVA_Cliente = "SI";
                    }
                }
            }

            if (IVA_Cliente == "NO")
            {
                // Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CentroDistribucion cd = new CentroDistribucion();
                new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);
                HD_IVAfacturacion.Value = cd.Cd_IvaPedidosFacturacion.ToString();
                IVATEXTO.Text = "IVA : " + cd.Cd_IvaPedidosFacturacion.ToString() + "%";
            }
        }

        private void CargarPedidoAutorizado()
        {
            try
            {
                HF_ID.Value = txtFolio.Text;
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                session = (Sesion)Session["Sesion" + Session.SessionID];
                Pedido pedido = new Pedido();
                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;
                pedido.Id_Ped = Convert.ToInt32(txtFolio.Value);

                CN_CapPedido cn_capPedido = new CN_CapPedido();
                cn_capPedido.ConsultaPedidoAutorizado(ref pedido, session.Emp_Cnx);

                ddlClienteNom.Value = pedido.Id_Cte.ToString().Trim();
                txtIdCte.Value = pedido.Id_Cte.ToString().Trim();

                ddlClienteNom.Enabled = false;
                ddlTerritorioNom.Enabled = false;

                CargarTerritorios();

                TxtPed_ReqAcys.Value = pedido.Ped_OrdenCompra != null || pedido.Ped_OrdenCompra != "" ? pedido.Requisicion : pedido.Ped_OrdenCompra;
                txtRikNom.Value = pedido.Rik_Nombre;
                txtIdRik.Value = pedido.Id_Rik.ToString().Trim();
                ddlTerritorioNom.Value = pedido.Id_Ter.ToString().Trim();
                txtIdTer.Value = pedido.Id_Ter.ToString().Trim();

                txtContactoNom.Value = pedido.Ped_Solicito;
                txtContactoTel.Value = pedido.Ped_SolicitoTel;
                txtContactoMail.Value = pedido.Ped_SolicitoEmail;
                txtContactoPuesto.Value = pedido.Ped_SolicitoPuesto;

                txtCalle.Value = pedido.Ped_ConsignadoCalle;

                txtNo.Value = pedido.Ped_ConsignadoNo.ToString().Trim().Replace(" ", "");
                txtCp.Value = pedido.Ped_ConsignadoCp;
                txtMunicipio.Value = pedido.Ped_ConsignadoMunicipio;
                txtEstado.Value = pedido.Ped_ConsignadoEstado;
                txtColonia.Value = pedido.Ped_ConsignadoColonia;
                /*
                txtCalle.Enabled = false;
                txtNo.Enabled = false;
                txtCp.Enabled = false;
                txtColonia.Enabled = false;
                txtMunicipio.Enabled = false;
                txtEstado.Enabled = false;
                */

                txtObservaciones.Value = pedido.Ped_Observaciones;
                Session["Semana"] = pedido.Ped_AcysSemana.ToString().Trim();

                txtClave.Value = pedido.Id_Acs.ToString().Trim();
                txtSubtotal.Value = pedido.Ped_Subtotal.ToString().Trim();
                txtIva.Value = pedido.Ped_Iva.ToString().Trim();
                txtTotal.Value = pedido.Ped_Total.ToString().Trim();

                rdFechaF.Value = pedido.Ped_FechFactAcys;
                rdFechaE.Value = pedido.Ped_FechaEntrega;

                if (pedido.FechaFacAcys.Year != 1)
                {
                    rdFechaF.Value = pedido.FechaFacAcys;
                }

                TxtPed_ReqAcys.Value = pedido.Requisicion;
                if (pedido.Ped_OrdenCompra != "")
                {
                    TxtPed_ReqAcys.Value = pedido.Ped_OrdenCompra;
                }
                ChkOrdCompra.Checked = pedido.Ped_ReqOrden;
                ChkOrdReposicion.Checked = pedido.Acs_ReqDocReposicion;
                chkFolio.Checked = pedido.Acs_ReqDocFolio;

                CHKFacKey.Checked = pedido.Acs_ReqFacturaKey;
                CHKRemision.Checked = pedido.ACS_ReqRemision;
                CHKCopiaPed.Checked = pedido.Acs_ReqCopia;

                lblFacturakey.Text = pedido.Acs_ReqFacturaKeyCop.ToString().Trim();
                lblOrdenCompraCopias.Text = pedido.Acs_ReqOrdencop.ToString().Trim();
                lblOrdenRepo.Text = pedido.Acs_ReqDocReposicioncop.ToString().Trim();

                lblremision.Text = pedido.ACS_ReqRemisionCop.ToString().Trim();
                lblCopia.Text = pedido.Acs_ReqCopiaCop.ToString().Trim();
                lblFolio.Text = pedido.Acs_ReqDocFoliocop.ToString().Trim();

                TxtEOtro.Value = pedido.Acs_ReqDocOtro;

                //Llena la informacion de compras

                txtComprasNombre.Value = pedido.acs_contacto2;
                if (pedido.acs_telefono2 == "0")
                {
                    txtComprasTelefono.Value = "";
                }
                else
                {
                    txtComprasTelefono.Value = pedido.acs_telefono2;
                }
                txtComprasCorreo.Value = pedido.acs_email2;

                //Llena la información de almacen
                txtAlmacenNombre.Value = pedido.acs_contacto3;
                if (pedido.acs_telefono3 == "0")
                {
                    txtAlmacenTelefono.Value = "";
                }
                else
                {
                    txtAlmacenTelefono.Value = pedido.acs_telefono3;
                }
                txtAlmacenCorreo.Value = pedido.acs_email3;


                //Llena la información de Mantenimiento
                TxtMtoNombre.Value = pedido.acs_contacto4;
                if (pedido.acs_telefono4 == "0")
                {
                    TxtMtoTelefono.Value = "";
                }
                else
                {
                    TxtMtoTelefono.Value = pedido.acs_telefono4;
                }
                TxtMtoCorreo.Value = pedido.acs_email4;


                //Llena la información de Pagos
                txtPagoNombre.Value = pedido.acs_contacto5;
                if (pedido.acs_telefono5 == "0")
                {
                    txtPagoTelefono.Value = "";
                }
                else
                {
                    txtPagoTelefono.Value = pedido.acs_telefono5;
                }
                txtPagoCorreo.Value = pedido.acs_email5;

                pedido.Ped_Tipo = 3;
                pedido.Ped_Captacion = true;

                GetListDet();
                DataTable dtTemp = dt;
                DataTable dtRestos = dt_Resto;

                cn_capPedido.ConsultaCaptacionPedidoAutorizado(pedido, ref dtTemp, ref dtRestos, session.Emp_Cnx);

                dt = dtTemp;
                dt_Resto = dtRestos;

                CN_CatProducto cn_catproducto = new CN_CatProducto();
                Producto pr;
                foreach (DataRow dr in dt.Rows)
                {
                    pr = new Producto();
                    pr.Id_Emp = session.Id_Emp;
                    pr.Id_Cd = session.Id_Cd_Ver;
                    pr.Id_Prd = Convert.ToInt64(dr["Id_prd"]);

                    cn_catproducto.ConsultarVentas(ref pr, Convert.ToInt32(txtIdCte.Value), session.Emp_Cnx);

                    dr["mes1"] = pr.ventaMes[0].ToString().Trim();
                    dr["mes2"] = pr.ventaMes[1].ToString().Trim();
                    dr["mes3"] = pr.ventaMes[2].ToString().Trim();
                }

                foreach (DataRow dr in dt_Resto.Rows)
                {
                    pr = new Producto();
                    pr.Id_Emp = session.Id_Emp;
                    pr.Id_Cd = session.Id_Cd_Ver;
                    pr.Id_Prd = Convert.ToInt64(dr["Id_prd"]);

                    cn_catproducto.ConsultarVentas(ref pr, Convert.ToInt32(txtIdCte.Value), session.Emp_Cnx);

                    dr["mes1"] = pr.ventaMes[0].ToString().Trim();
                    dr["mes2"] = pr.ventaMes[1].ToString().Trim();
                    dr["mes3"] = pr.ventaMes[2].ToString().Trim();
                }


                string ConexionCentral = ConfigurationManager.AppSettings["strConnectionCentral"].ToString().Trim();
                ConvenioDet conv;
                ConvenioDet convdet;
                CN_Convenio cn_conv;
                List<string> Productos = new List<string>();

                List<ConvenioDet> lconveniosdet = new List<ConvenioDet>();
                ConvenioDet lconvdet = new ConvenioDet();


                if (dt.Rows.Count > 0)
                {
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        if (Convert.ToDouble(dt.Rows[x]["Prd_PrecioLista"]) == 0)
                        {
                            conv = new ConvenioDet();
                            convdet = new ConvenioDet();
                            cn_conv = new CN_Convenio();
                            conv.Id_Emp = session.Id_Emp;
                            conv.Id_Cd = session.Id_Cd_Ver;
                            conv.Id_Cte = Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1");
                            conv.Id_Prd = Convert.ToInt64(dt.Rows[x]["Id_Prd"]);
                            double PrecioIngresado = Convert.ToDouble(dt.Rows[x]["Prd_Precio"]);

                            if (Convert.ToInt64(dt.Rows[x]["Id_Prd"]) <= 999999999999)
                            {
                                //cn_conv.Convenio_ConsultaPrecio(conv, ref convdet, ConexionCentral);

                                //if (convdet != null && convdet.PCD_PrecioAAAEsp > 0)
                                //{
                                //    dt.Rows[x]["Prd_PrecioLista"] = convdet.PCD_PrecioAAAEsp;
                                //}
                                //else
                                //{
                                Producto producto = new Producto();
                                //obtener datos de producto
                                CN_CatProducto clsProducto = new CN_CatProducto();
                                producto.Id_Cte = Convert.ToInt32(Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1"));
                                int productoNuevo = 0;
                                clsProducto.ConsultaProductos(ref producto, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, Convert.ToInt64(dt.Rows[x]["Id_Prd"]), ref productoNuevo, 0);

                                if (0 < producto.Prd_PLista)
                                {
                                    dt.Rows[x]["Prd_PrecioLista"] = producto.Prd_PLista;
                                }
                                //}
                            }
                        }
                    }
                }


                if (dt_Resto.Rows.Count > 0)
                {
                    for (int x = 0; x < dt_Resto.Rows.Count; x++)
                    {
                        if (Convert.ToDouble(dt_Resto.Rows[x]["Prd_PrecioLista"]) == 0)
                        {
                            conv = new ConvenioDet();
                            convdet = new ConvenioDet();
                            cn_conv = new CN_Convenio();
                            conv.Id_Emp = session.Id_Emp;
                            conv.Id_Cd = session.Id_Cd_Ver;
                            conv.Id_Cte = Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1");
                            conv.Id_Prd = Convert.ToInt64(dt_Resto.Rows[x]["Id_Prd"]);
                            double PrecioIngresado = Convert.ToDouble(dt_Resto.Rows[x]["Prd_Precio"]);

                            if (Convert.ToInt64(dt_Resto.Rows[x]["Id_Prd"]) <= 999999999999)
                            {
                                //cn_conv.Convenio_ConsultaPrecio(conv, ref convdet, ConexionCentral);

                                //if (convdet != null && convdet.PCD_PrecioAAAEsp > 0)
                                //{
                                //    dt_Resto.Rows[x]["Prd_PrecioLista"] = convdet.PCD_PrecioAAAEsp.ToString();
                                //}
                                //else
                                //{
                                Producto producto = new Producto();
                                //obtener datos de producto
                                CN_CatProducto clsProducto = new CN_CatProducto();
                                producto.Id_Cte = Convert.ToInt32(Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1"));
                                int productoNuevo = 0;
                                clsProducto.ConsultaProductos(ref producto, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, Convert.ToInt64(dt_Resto.Rows[x]["Id_Prd"]), ref productoNuevo, 0);

                                if (0 < producto.Prd_PLista)
                                {
                                    dt_Resto.Rows[x]["Prd_PrecioLista"] = producto.Prd_PLista.ToString();
                                }
                                //}
                            }
                        }
                    }
                }


                Session["Prod"] = dt;
                rg1.DataSource = dt;
                rg1.DataBind();

                Session["Restos"] = dt_Resto;
                rg1_Restos.DataSource = dt_Resto;
                rg1_Restos.DataBind();


                string _idTGStr = Request.QueryString["Id_TG"];
                int _idTG = 0;
                if (_idTGStr != null)
                {
                    if (int.TryParse(_idTGStr, out _idTG))
                    {


                    }
                }

                Consultar_IVA_Cliente();

                Funciones funcion = new Funciones();
                CN_CatSemana CnSemana = new CN_CatSemana();
                Semana semana = new Semana();
                semana.Id_Emp = session.Id_Emp;
                semana.Id_Cd = session.Id_Cd_Ver;
                semana.Sem_FechaAct = funcion.GetLocalDateTime(session.Minutos);
                int verificador = -1;
                CnSemana.ConsultaSemana(ref semana, session.Emp_Cnx, ref verificador);

                if (verificador > -1)
                {
                    HF_FechaActual.Value = rdFechaF.ToString().Trim();
                    HF_InicioSemana.Value = semana.Sem_FechaIni.ToString().Trim();
                    HF_FinSemana.Value = semana.Sem_FechaFin.ToString().Trim();
                }

                if (txtClave.Value != "")
                {
                    txtContactoNom.Enabled = true;
                    txtContactoTel.Enabled = true;
                    txtContactoMail.Enabled = true;
                    txtContactoPuesto.Enabled = true;

                    txtPagoCorreo.Enabled = true;
                    txtPagoNombre.Enabled = true;
                    txtPagoTelefono.Enabled = true;

                    txtAlmacenCorreo.Enabled = true;
                    txtAlmacenNombre.Enabled = true;
                    txtAlmacenTelefono.Enabled = true;

                    TxtMtoCorreo.Enabled = true;
                    TxtMtoNombre.Enabled = true;
                    TxtMtoTelefono.Enabled = true;

                    txtComprasCorreo.Enabled = true;
                    txtComprasNombre.Enabled = true;
                    txtComprasTelefono.Enabled = true;

                    txtIdCte.Disabled = true;
                    ddlClienteNom.Enabled = false;
                    txtIdTer.Disabled = true;
                    ddlTerritorioNom.Enabled = false;

                    txtIdRik.Disabled = true;
                    txtRikNom.Disabled = true;

                    //ChkOrdCompra.Disabled = true;
                    //chkFolio.Disabled = true;
                    //ChkOrdReposicion.Disabled = true;

                    //CHKFacKey.Disabled = true;
                    //CHKRemision.Disabled = true;
                    //CHKCopiaPed.Disabled = true;


                    //lblFacturakey.Enabled = false;
                    //lblOrdenCompraCopias.Enabled = false;
                    //lblOrdenRepo.Enabled = false;

                    //lblremision.Enabled = false;
                    //lblCopia.Enabled = false;
                    //lblFolio.Enabled = false;

                }
                else
                {

                    txtContactoNom.Enabled = true;
                    txtContactoPuesto.Enabled = true;
                    txtContactoMail.Enabled = true;
                    txtContactoTel.Enabled = true;
                    /*
                    txtCalle.Enabled = false;
                    txtNo.Enabled = false;
                    txtCp.Enabled = false;
                    txtColonia.Enabled = false;
                    txtMunicipio.Enabled = false;
                    txtEstado.Enabled = false;
                    */
                }
            }
            catch (Exception ex)
            {
                mensajeError(ex.Message);
                throw ex;
            }
        }

        private void EnviarCorreosolicitud(int id_ped, string conexion)
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Cd = session.Id_Cd_Ver;
                configuracion.Id_Emp = session.Id_Emp;
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                cn_configuracion.Consulta(ref configuracion, session.Emp_Cnx);
                StringBuilder cuerpo_correo = new StringBuilder();


                cuerpo_correo.Append("<div>");
                cuerpo_correo.Append("<table><tr><td>");
                cuerpo_correo.Append("<td></td>");
                cuerpo_correo.Append("</tr><tr><td colspan='2'><br><br><br></td>");
                cuerpo_correo.Append("</tr><tr>");
                cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='2'>");

                cuerpo_correo.Append("Buen día, Se les Comunica por este medio. <br><br> ");

                cuerpo_correo.Append("Se ha Solicitado la Autorización del Pedido: " + id_ped + "<br><br>");
                cuerpo_correo.Append("Sucursal: " + session.Cd_Nombre + " <br>");
                cuerpo_correo.Append("Usuario que Solicita: " + session.U_Nombre + " <br>");
                cuerpo_correo.Append("Cliente: " + int.Parse(txtIdCte.Value).ToString() + " - " + ddlClienteNom.Text.Trim() != "" ? ddlClienteNom.Text.Trim() : "" + " <br>");

                cuerpo_correo.Append(" <br>  <br> Sistema Automático de Solicitud de Pedidos Clientes Crédito Bloqueado. <br>  Fecha de Realización: " + DateTime.Now.ToString("dd/MM/yyyy") + "<br>");
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");
                cuerpo_correo.Append("<br>");
                cuerpo_correo.Append("</td></tr></table></div>");

                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);

                MailMessage m = new MailMessage();
                m.From = new MailAddress(configuracion.Mail_Remitente, "Solicitud de Autorizacion de Pedido");
                m.Subject = "Solicitud de Autorizacion de Pedido";
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                m.To.Add(new MailAddress(ConfigurationManager.AppSettings["CorreoGral"].ToString()));
                m.To.Add(new MailAddress(ConfigurationManager.AppSettings["CorreoGerente"].ToString()));

                //m.To.Add(new MailAddress(configuracion.mail_AutorizacionAcys_Gerente));
                //m.To.Add(new MailAddress("servicios.informatica@gibraltar.com.mx"));
                m.IsBodyHtml = true;

                string body = cuerpo_correo.ToString().Trim();
                AlternateView vistaHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);

                m.AlternateViews.Add(vistaHtml);
                try
                {
                    sm.Send(m);
                }
                catch (Exception ex)
                {
                    mensajeError("Fallo en enviar el correo electronico" + ex.Message.ToString());
                }
            }
            catch (Exception ex2)
            {
                mensajeError("Fallo en la configuración del correo" + ex2.Message.ToString());
            }
        }

        protected void ButtonSolicitud_ServerClick(object sender, EventArgs e)
        {
            string lockKey;
            if (!TryAcquireSaveLock(out lockKey))
            {
                mensajeInfo("Ya hay una solicitud en proceso. Por favor, no presione varias veces. Reintente en unos momentos.");
                return;
            }

            try
            {
                GuardarSolicitud();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError($"Solicitud: error en GuardarSolicitud. Sesión={Session.SessionID}, Mensaje={ex.Message}");
                mensajeError("Ocurrió un problema al preparar la solicitud. Intente nuevamente.");
                ClearLastSolicitudHash();
                throw;
            }
            finally
            {
                ReleaseSaveLock(lockKey);
            }
        }

        private void GuardarSolicitud()
        {
            try
            {

                bool _PermisoGuardar = bool.Parse(Request.QueryString["PermisoGuardar"].ToString().Trim());
                bool _PermisoModificar = bool.Parse(Request.QueryString["PermisoModificar"].ToString().Trim());

                DataTable dt = (DataTable)Session["Prod"];
                DataTable dtTemp = (DataTable)Session["Restos"];
                string currentHash = CalculatePedidoContentHash(dt, dtTemp);
                string lastHash = GetLastPedidoHash();
                DateTime lastTime = GetLastPedidoHashTime();


                if (dt.Rows.Count == 0 && dtTemp.Rows.Count == 0)
                {
                    mensajeInfo("No ha agregado ningún producto en la sección de detalle");
                    return;
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow[] dr = dt.Select("Acs_Doc = ''");
                    if (dr.Length > 0)
                    {
                        mensajeInfo("No se seleccionó documento de entrega para el producto <b>" + dr[0][1] + " - " + dr[0][2] + "</b>");
                        return;
                    }
                }

                if (dtTemp.Rows.Count > 0)
                {
                    DataRow[] drRestos = dtTemp.Select("Acs_Doc = ''");
                    if (drRestos.Length > 0)
                    {
                        mensajeInfo("No se seleccionó documento de entrega para el producto <b>" + drRestos[0][1] + " - " + drRestos[0][2] + "</b>");
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(lastHash) && lastHash == currentHash && (DateTime.UtcNow - lastTime).TotalSeconds < IdempotencyWindowSeconds)
                {
                    mensajeInfo("Se detectó un guardado reciente con la misma información. No se duplicó el pedido.");
                    return;
                }

                if (txtClave.Text == "")
                {
                    txtClave.Text = "0";
                }

                int verificador = -1;
                Funciones funcion = new Funciones();
                PedidoVtaInst pedido = new PedidoVtaInst();
                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;
                pedido.Ped_Fecha = funcion.GetLocalDateTime(session.Minutos);

                pedido.Id_Rik = Convert.ToInt32(txtIdRik.Value);
                pedido.Id_Cte = Convert.ToInt32(txtIdCte.Value);

                if (txtIdTer.Value == "" || txtIdTer.Value == "-1")
                {
                    mensajeInfo("El Territorio es Requerido");
                    return;

                }
                pedido.Id_Ter = Convert.ToInt32(txtIdTer.Value);

                pedido.Pedido_del = txtFolio.Value.ToString().Trim().Trim();
                if (TxtPed_ReqAcys.Value != null && TxtPed_ReqAcys.Value != "")
                {
                    pedido.Requisicion = TxtPed_ReqAcys.Value.ToString().Trim().Trim();
                    pedido.Ped_OrdenEntrega = TxtPed_ReqAcys.Value.ToString().Trim().Trim();
                }
                pedido.Ped_Solicito = txtContactoNom.Text;
                pedido.Ped_Flete = string.Empty;

                pedido.Ped_CondEntrega = 0;
                pedido.Ped_FechaEntrega = Convert.ToDateTime(rdFechaE.Value);


                pedido.Ped_Observaciones = txtObservaciones.Text;
                pedido.Ped_DescPorcen1 = 0;
                pedido.Ped_DescPorcen2 = 0;
                pedido.Ped_Desc1 = string.Empty;
                pedido.Ped_Desc2 = string.Empty;
                pedido.Ped_Importe = txtSubtotal.Value != "" ? Convert.ToDouble(txtSubtotal.Value) : 0;
                pedido.Ped_Subtotal = txtSubtotal.Value != "" ? Convert.ToDouble(txtSubtotal.Value) : 0;
                pedido.Ped_Iva = txtIva.Value != "" ? Convert.ToDouble(txtIva.Value) : 0;
                pedido.Ped_Total = txtTotal.Value != "" ? Convert.ToDouble(txtTotal.Value) : 0;

                pedido.Id_U = session.Id_U;
                pedido.Id_Acs = Convert.ToInt32(txtClave.Value);
                pedido.Ped_SolicitoTel = txtContactoTel.Text;
                pedido.Ped_SolicitoEmail = txtContactoMail.Text;
                pedido.Ped_SolicitoPuesto = txtContactoPuesto.Text;
                pedido.Ped_ConsignadoCalle = txtCalle.Text;
                pedido.Ped_ConsignadoNo = txtNo.Text;
                pedido.Ped_ConsignadoCp = txtCp.Text;
                pedido.Ped_ConsignadoMunicipio = txtMunicipio.Text;
                pedido.Ped_ConsignadoEstado = txtEstado.Text;
                pedido.Ped_ConsignadoColonia = txtColonia.Text;
                pedido.Ped_ReqOrden = ChkOrdCompra.Checked;

                pedido.Acs_ReqFacturaKey = CHKFacKey.Checked;
                pedido.ACS_ReqRemision = CHKRemision.Checked;
                pedido.Acs_ReqCopia = CHKCopiaPed.Checked;

                pedido.Ped_Comentarios = txtObservaciones.Text;

                pedido.Acs_ReqFacturaKeyCop = Convert.ToInt32(lblFacturakey.Text);
                pedido.Acs_ReqOrdencop = Convert.ToInt32(lblOrdenCompraCopias.Text);
                pedido.Acs_ReqOrdencop = Convert.ToInt32(lblOrdenRepo.Text);
                pedido.ACS_ReqRemisionCop = Convert.ToInt32(lblremision.Text);
                pedido.Acs_ReqCopiaCop = Convert.ToInt32(lblCopia.Text);
                pedido.Acs_ReqDocFoliocop = Convert.ToInt32(lblFolio.Text);

                pedido.Ped_OrdenCompra = TxtPed_ReqAcys.Text;
                pedido.Id_Acs = Convert.ToInt32(txtClave.Value);
                pedido.Estatus = "P";
                pedido.Ped_Tipo = txtClave.Text == "" || txtClave.Text == "0" ? 4 : 3;

                // Edsg Proyecto Internet

                pedido.FechaFacAcys = Convert.ToDateTime(rdFechaF.Value);
                pedido.PedAcys = txtFolio.Value.ToString().Trim();
                if (TxtPed_ReqAcys.Value != null)
                {
                    if (TxtPed_ReqAcys.Value.ToString().Trim() != "")
                    {
                        pedido.ReqAcys = TxtPed_ReqAcys.Value.ToString().Trim().Trim();
                        pedido.OcAcys = TxtPed_ReqAcys.Value.ToString().Trim().Trim();
                    }
                }
                if (Request.QueryString["Anio"] != null)
                {
                    pedido.Ped_AcysAnio = Convert.ToInt32(Request.QueryString["Anio"]);
                }
                else
                {
                    pedido.Ped_AcysAnio = DateTime.Now.Year;

                }
                if (Session["Semana"] != null)
                {
                    pedido.Ped_AcysSemana = Convert.ToInt32(Session["Semana"].ToString().Trim());
                }
                else if (Request.QueryString["Semana"] != null)
                {
                    pedido.Ped_AcysSemana = Convert.ToInt32(Request.QueryString["Semana"]);
                }
                else
                {
                    CN_CatSemana Semana = new CN_CatSemana();
                    List<Semana> Lista = new List<CapaEntidad.Semana>();
                    Semana.ConsultaSemana_Anio(session.Id_Emp, int.Parse(funcion.GetLocalDateTime(session.Minutos).Year.ToString().Trim()), session.Emp_Cnx, ref Lista);

                    var query = (from tlist in Lista
                                 where tlist.Sem_FechaIni <= DateTime.Now
                                 orderby tlist.Id_Sem descending
                                 select tlist
                                 ).FirstOrDefault();

                    if (query != null)
                    {
                        pedido.Ped_AcysSemana = query.Id_Sem;
                    }
                }

                pedido.Acs_ReqDocFolio = chkFolio.Checked;
                pedido.Acs_ReqDocReposicion = ChkOrdReposicion.Checked;
                pedido.Acs_ReqDocOtro = TxtEOtro.Value;

                //Llena la informacion de compras

                pedido.Acs_Contacto2 = txtComprasNombre.Text;
                pedido.Acs_Telefono2 = txtComprasTelefono.Text;
                pedido.Acs_Email2 = txtComprasCorreo.Text;

                //Llena la información de almacen
                pedido.Acs_Contacto3 = txtAlmacenNombre.Text;
                pedido.Acs_Telefono3 = txtAlmacenTelefono.Text;
                pedido.Acs_Email3 = txtAlmacenCorreo.Text;


                //Llena la información de Mantenimiento
                pedido.Acs_Contacto4 = TxtMtoNombre.Text;
                pedido.Acs_Telefono4 = TxtMtoTelefono.Text;
                pedido.Acs_Email4 = TxtMtoCorreo.Text;


                //Llena la información de Pagos
                pedido.Acs_Contacto5 = txtPagoNombre.Text;
                pedido.Acs_Telefono5 = txtPagoTelefono.Text;
                pedido.Acs_Email5 = txtPagoCorreo.Text;

                if ((Request.QueryString["IdDireccion"] != null))
                {
                    if (Convert.ToInt32(Request.QueryString["IdDireccion"]) != -1)
                    {
                        pedido.id_cteDirEntrega = Int32.Parse(Request.QueryString["IdDireccion"].ToString());
                    }
                    else
                    {
                        pedido.id_cteDirEntrega = 0;
                    }
                }
                else
                {
                    pedido.id_cteDirEntrega = 0;
                }

                CN_CapPedidoVtaInst clsCapPedido = new CN_CapPedidoVtaInst();

                //JFCV convenios, validar el precio minimo y maximo 
                #region inicio validar precios convenio



                //JFCV 11 agosto del 2022 
                //leer configuración para ver si se valida o no el precio 
                // configuración 953 conf de Pedidos  
                // si el valor es 0 no valida y si es 1 si va a validar 
                int GLOBAL_ValidaPrecioMinimoRik = 0;
                CapaEntidad.eSysConfiguracion SysC = new CapaEntidad.eSysConfiguracion();
                try
                {
                    CN_SysConfigruacion CN = new CN_SysConfigruacion();
                    SysC = CN.spSysConfiguracionById(session.Id_Emp, session.Id_Cd, 953, session.Emp_Cnx);
                    int iTmp = 0;
                    int.TryParse(SysC.Conf_Valor, out iTmp);
                    GLOBAL_ValidaPrecioMinimoRik = iTmp;
                }
                catch (Exception ex)
                {
                    GLOBAL_ValidaPrecioMinimoRik = 0;
                }
                string ConexionCentral = ConfigurationManager.AppSettings["strConnectionCentral"].ToString().Trim();
                ConvenioDet conv;
                ConvenioDet convdet;
                CN_Convenio cn_conv;
                List<string> Productos = new List<string>();

                List<ConvenioDet> lconveniosdet = new List<ConvenioDet>();
                ConvenioDet lconvdet = new ConvenioDet();

                string prodAAA = "";
                //JFCV Validar si tiene precios inferiores a los precios minimos
                AlertaAutorizacion alertaaut;
                AlertaAutorizacion alertaautdet;
                CN_AlertaAutorizacion cn_alertaautorizacion;

                List<string> ProductosAlerta = new List<string>();
                List<AlertaAutorizacion> lalertasautdet = new List<AlertaAutorizacion>();
                AlertaAutorizacion pasoalertaautdet = new AlertaAutorizacion();

                //reviso si la variable de sesion ya tiene un folio guardado
                //si no lo tiene es que e sla primera vez que entra a grabar 
                //si ya lo tiene valido que sea el mismo que el que estoy 
                int procesar = 1;

                if (Session["Id_FacPrec" + Session.SessionID] != null)
                {
                    string folior = Session["Id_FacPrec" + Session.SessionID].ToString();
                    if (folior == txtFolio.Value.ToString().Trim())
                    {
                        procesar = 0;
                    }
                }

                //la primera vez que entra a esta rutina guarda en variable de sesion 
                //el folio si acaso tiene productos que requirieron de validación
                //entonces la segunda vez que entra ya esta ese dato guardado 
                //y si el folio es el mismo quiere decir que se reejecuto el grabar y que ya no debe validar.


                //JFCV  fin
                if (dt.Rows.Count > 0)
                {
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {

                        conv = new ConvenioDet();
                        convdet = new ConvenioDet();
                        cn_conv = new CN_Convenio();
                        conv.Id_Emp = session.Id_Emp;
                        conv.Id_Cd = session.Id_Cd_Ver;
                        conv.Id_Cte = Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1");
                        conv.Id_Prd = Convert.ToInt64(dt.Rows[x]["Id_Prd"]);
                        double PrecioIngresado = Convert.ToDouble(dt.Rows[x]["Prd_Precio"]);
                        //double impore =  dt.Rows[x]["Prd_Precio"].ToString().Trim()!= DBNull.Value  ? (double)(dt.Rows[x]["Prd_Precio"]) : 0;

                        if (Convert.ToInt64(dt.Rows[x]["Id_Prd"]) <= 999999999999)
                        {
                            //double PrecioIngresado = Convert.ToDouble(item2.OwnerTableView.DataKeyValues[item2.ItemIndex]["Prd_Importe"]);
                            //double impore = (item2.FindControl("Prd_Importe") as RadNumericTextBox).Value.HasValue ? (double)(item2.FindControl("Prd_Importe") as RadNumericTextBox).Value : 0;
                            cn_conv.Convenio_ConsultaPrecio(conv, ref convdet, ConexionCentral);



                            int agregar = 0;

                            if (convdet != null && convdet.PCD_PrecioAAAEsp > 0)
                            {
                                if (convdet.PCD_PrecioVtaMin != 0 || convdet.PCD_PrecioVtaMax != 0)
                                {
                                    #region pvtamin y pvtamax  dif de cero 
                                    if (Math.Round(PrecioIngresado, 3) < convdet.PCD_PrecioVtaMin)
                                    {
                                        agregar = 1;
                                        /* if (Convert.ToDouble(PrecioIngresado) < convdet.PCD_PrecioAAAEsp)
                                         {
                                             if (prodAAA != "")
                                             {
                                                 prodAAA = prodAAA + ", " + Convert.ToInt64(dt.Rows[x]["Id_Prd"]).ToString();
                                             }
                                             else
                                             {
                                                 prodAAA = Convert.ToInt64(dt.Rows[x]["Id_Prd"]).ToString();
                                             }
                                       }*/
                                    }
                                    else if (convdet.PCD_PrecioVtaMin == 0 && convdet.PCD_PrecioVtaMax != 0)
                                    {
                                        // vta minima es igual a cero y vta max dif 0 
                                        // si precio es mayor a vta max manda aviso 
                                        if (Math.Round(PrecioIngresado, 3) > convdet.PCD_PrecioVtaMax)
                                        {
                                            agregar = 1;
                                        }
                                    }
                                    else if (Math.Round(PrecioIngresado, 3) > convdet.PCD_PrecioVtaMax)
                                    {
                                        agregar = 1;
                                    }

                                    if (agregar == 1)
                                    {
                                        Productos.Add(convdet.Id_Prd.ToString().Trim());

                                        lconvdet = new ConvenioDet();
                                        lconvdet.PC_Nombre = convdet.PC_Nombre;
                                        lconvdet.Id_PC = convdet.Id_PC;
                                        lconvdet.Id_Prd = convdet.Id_Prd;
                                        lconvdet.PCD_PrecioVtaMax = convdet.PCD_PrecioVtaMax;
                                        lconvdet.PCD_PrecioVtaMin = convdet.PCD_PrecioVtaMin;
                                        lconvdet.PCD_PrecioVentaConvenio = PrecioIngresado;
                                        lconveniosdet.Add(lconvdet);
                                    }
                                    #endregion
                                }  // si pvtamin y pvtamax son cero
                            }
                            else
                            {
                                Producto producto = new Producto();
                                //obtener datos de producto
                                CN_CatProducto clsProducto = new CN_CatProducto();
                                producto.Id_Cte = Convert.ToInt32(Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1"));
                                int productoNuevo = 0;
                                clsProducto.ConsultaProductos(ref producto, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, Convert.ToInt64(dt.Rows[x]["Id_Prd"]), ref productoNuevo, 0);

                                if (Convert.ToDouble(PrecioIngresado) < producto.Prd_AAA)
                                {
                                    if (prodAAA != "")
                                    {
                                        prodAAA = prodAAA + ", " + Convert.ToInt64(dt.Rows[x]["Id_Prd"]).ToString();
                                    }
                                    else
                                    {
                                        prodAAA = Convert.ToInt64(dt.Rows[x]["Id_Prd"]).ToString();
                                    }

                                }
                            }
                            //JFCV Valido si el precio de venta es menor al minimo
                            #region validar precios que requieran autorización

                            //JFCV 11 abril 2023 Los precios de cuentas nacionales no deben validarse , así que comento esta parte 
                            //donde recorre el grid Rg1 que es el datatable dt , ya que esos productos son cuentas nacionales y no debe valiarlos  

                            //if (GLOBAL_ValidaPrecioMinimoRik == 1)
                            //{
                            //    alertaaut = new AlertaAutorizacion();
                            //    alertaautdet = new AlertaAutorizacion();
                            //    cn_alertaautorizacion = new CN_AlertaAutorizacion();

                            //    if (procesar == 1)
                            //    {
                            //        alertaaut.Id_Emp = session.Id_Emp;
                            //        alertaaut.Id_Cd = session.Id_Cd_Ver;
                            //        alertaaut.Id_Cte = Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1");
                            //        alertaaut.Cte_NomComercial = ddlClienteNom.Text.Trim() != "" ? ddlClienteNom.Text.Trim() : "";
                            //        alertaaut.Id_Prd = Convert.ToInt64(dt.Rows[x]["Id_Prd"]);
                            //        alertaaut.IdRepresentante = Convert.ToInt32(txtIdRik.Value);
                            //        alertaaut.Id_Ter = Convert.ToInt32(txtIdTer.Value);
                            //        double Preciodefactura = Convert.ToDouble(dt.Rows[x]["Prd_Precio"]);
                            //        alertaaut.Precio_Vta = Preciodefactura;
                            //        cn_alertaautorizacion.AlertaPrecioConsultaPrecio(alertaaut, ref alertaautdet, ConexionCentral);

                            //        if (alertaautdet != null && alertaautdet.Precio_MinimoRik > 0)
                            //        {
                            //            if (Math.Round(Preciodefactura, 3) < alertaautdet.PrecioObjetivo)
                            //            {

                            //                ProductosAlerta.Add(alertaautdet.Id_Prd.ToString().Trim());

                            //                pasoalertaautdet = new AlertaAutorizacion();

                            //                pasoalertaautdet.Id_Emp = session.Id_Emp;
                            //                pasoalertaautdet.Id_Cd = session.Id_Cd_Ver;
                            //                pasoalertaautdet.Id_Cte = Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1");
                            //                pasoalertaautdet.IdRepresentante = Convert.ToInt32(txtIdRik.Value);
                            //                pasoalertaautdet.Id_Ter = Convert.ToInt32(txtIdTer.Value);
                            //                pasoalertaautdet.IdAutorizacionAnterior = alertaautdet.IdAutorizacionAnterior;
                            //                pasoalertaautdet.Id_Prd = alertaautdet.Id_Prd;
                            //                pasoalertaautdet.Precio_MinimoRik = Math.Round(Convert.ToDouble(alertaautdet.Precio_MinimoRik), 3);
                            //                pasoalertaautdet.Precio_MinimoGte = Math.Round(Convert.ToDouble(alertaautdet.Precio_MinimoGte), 3);
                            //                pasoalertaautdet.Precio_Vta = Preciodefactura;
                            //                pasoalertaautdet.PrecioLista = alertaautdet.PrecioLista;
                            //                pasoalertaautdet.Cte_NomComercial = alertaaut.Cte_NomComercial;
                            //                pasoalertaautdet.Prd_Descripcion = alertaautdet.Prd_Descripcion;
                            //                pasoalertaautdet.PrecioLista = alertaautdet.PrecioLista;
                            //                pasoalertaautdet.Cantidad = Convert.ToInt32(dt.Rows[x]["Prd_Cantidad"]);
                            //                pasoalertaautdet.Precio_AAA = alertaautdet.Precio_AAA;
                            //                pasoalertaautdet.Utilidad = alertaautdet.Utilidad;
                            //                pasoalertaautdet.Porc_Utilidad = alertaautdet.Utilidad / Preciodefactura;
                            //                pasoalertaautdet.Importe = Preciodefactura * pasoalertaautdet.Cantidad;
                            //                pasoalertaautdet.Importe_Utilidad = pasoalertaautdet.Utilidad * pasoalertaautdet.Cantidad;
                            //                pasoalertaautdet.Justificacion = "";
                            //                pasoalertaautdet.Prd_Descripcion = Convert.ToString(dt.Rows[x]["Prd_Descripcion"]);
                            //                pasoalertaautdet.Cte_NomComercial = ddlClienteNom.Text.Trim() != "" ? ddlClienteNom.Text.Trim() : "";
                            //                pasoalertaautdet.TipoAutorizacion = 4; //Pedido
                            //                pasoalertaautdet.FechaVigencia = DateTime.Now.AddMonths(12);
                            //                pasoalertaautdet.Id_Cpr = alertaautdet.Id_Cpr;
                            //                pasoalertaautdet.JustificacionMemo = "";
                            //                pasoalertaautdet.Id_Tamaño = alertaautdet.Id_Tamaño;
                            //                pasoalertaautdet.PrecioObjetivo = alertaautdet.PrecioObjetivo;
                            //                lalertasautdet.Add(pasoalertaautdet);

                            //            }

                            //}
                            //}
                            //}
                            #endregion validar precios que requieran autorización
                            //JFCV fin Valido si el precio de venta es menor al minimo
                        }// si prod es menor a 999999999999
                    }  //Termina ciclo  convenios  
                }  // si tiene artículos 

                //valido también los productos de este otro dataset 
                //JFCV 11abril que son del grid rg1_Restos 
                if (dtTemp.Rows.Count > 0)
                {
                    for (int x = 0; x < dtTemp.Rows.Count; x++)
                    {

                        conv = new ConvenioDet();
                        convdet = new ConvenioDet();
                        cn_conv = new CN_Convenio();
                        conv.Id_Emp = session.Id_Emp;
                        conv.Id_Cd = session.Id_Cd_Ver;
                        conv.Id_Cte = Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1");
                        conv.Id_Prd = Convert.ToInt64(dtTemp.Rows[x]["Id_Prd"]);
                        double PrecioIngresado = Convert.ToDouble(dtTemp.Rows[x]["Prd_Precio"]);
                        //double impore =  dtTemp.Rows[x]["Prd_Precio"].ToString().Trim()!= DBNull.Value  ? (double)(dtTemp.Rows[x]["Prd_Precio"]) : 0;

                        if (Convert.ToInt64(dtTemp.Rows[x]["Id_Prd"]) <= 999999999999)
                        {
                            //double PrecioIngresado = Convert.ToDouble(item2.OwnerTableView.DataKeyValues[item2.ItemIndex]["Prd_Importe"]);
                            //double impore = (item2.FindControl("Prd_Importe") as RadNumericTextBox).Value.HasValue ? (double)(item2.FindControl("Prd_Importe") as RadNumericTextBox).Value : 0;
                            cn_conv.Convenio_ConsultaPrecio(conv, ref convdet, ConexionCentral);



                            int agregar = 0;

                            if (convdet != null && convdet.PCD_PrecioAAAEsp > 0)
                            {
                                if (convdet.PCD_PrecioVtaMin != 0 || convdet.PCD_PrecioVtaMax != 0)
                                {
                                    #region pvtamin y pvtamax  dif de cero 
                                    if (Math.Round(PrecioIngresado, 3) < convdet.PCD_PrecioVtaMin)
                                    {
                                        agregar = 1;
                                        /*if (Convert.ToDouble(PrecioIngresado) < convdet.PCD_PrecioAAAEsp)
                                        {
                                            if (prodAAA != "")
                                            {
                                                prodAAA = prodAAA + ", " + Convert.ToInt64(dtTemp.Rows[x]["Id_Prd"]).ToString();
                                            }
                                            else
                                            {
                                                prodAAA = Convert.ToInt64(dtTemp.Rows[x]["Id_Prd"]).ToString();
                                            }
                            Juan Jo Lo que solicitamos es que la regla siga aplicando de manera general, pero en caso de que el cliente este ligado a algún convenio entonces si le permita facturar por debajo del precio aaa especial.
                            Autorizado por Dirección
                                            
                                    }
                                    */
                                    }
                                    else if (convdet.PCD_PrecioVtaMin == 0 && convdet.PCD_PrecioVtaMax != 0)
                                    {
                                        // vta minima es igual a cero y vta max dif 0 
                                        // si precio es mayor a vta max manda aviso 
                                        if (Math.Round(PrecioIngresado, 3) > convdet.PCD_PrecioVtaMax)
                                        {
                                            agregar = 1;
                                        }
                                    }
                                    else if (Math.Round(PrecioIngresado, 3) > convdet.PCD_PrecioVtaMax)
                                    {
                                        agregar = 1;
                                    }

                                    if (agregar == 1)
                                    {
                                        Productos.Add(convdet.Id_Prd.ToString().Trim());

                                        lconvdet = new ConvenioDet();
                                        lconvdet.PC_Nombre = convdet.PC_Nombre;
                                        lconvdet.Id_PC = convdet.Id_PC;
                                        lconvdet.Id_Prd = convdet.Id_Prd;
                                        lconvdet.PCD_PrecioVtaMax = convdet.PCD_PrecioVtaMax;
                                        lconvdet.PCD_PrecioVtaMin = convdet.PCD_PrecioVtaMin;
                                        lconvdet.PCD_PrecioVentaConvenio = PrecioIngresado;
                                        lconveniosdet.Add(lconvdet);
                                    }
                                    #endregion
                                }  // si pvtamin y pvtamax son cero
                            }
                            else
                            {
                                Producto producto = new Producto();
                                //obtener datos de producto
                                CN_CatProducto clsProducto = new CN_CatProducto();
                                producto.Id_Cte = Convert.ToInt32(Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1"));
                                int productoNuevo = 0;
                                clsProducto.ConsultaProductos(ref producto, session.Emp_Cnx, session.Id_Emp, session.Id_Cd_Ver, Convert.ToInt64(dtTemp.Rows[x]["Id_Prd"]), ref productoNuevo, 0);

                                if (Convert.ToDouble(PrecioIngresado) < producto.Prd_AAA)
                                {
                                    if (prodAAA != "")
                                    {
                                        prodAAA = prodAAA + ", " + Convert.ToInt64(dtTemp.Rows[x]["Id_Prd"]).ToString();
                                    }
                                    else
                                    {
                                        prodAAA = Convert.ToInt64(dtTemp.Rows[x]["Id_Prd"]).ToString();
                                    }

                                }
                            }

                            //JFCV Valido si el precio de venta es menor al minimo
                            #region validar precios que requieran autorización
                            if (GLOBAL_ValidaPrecioMinimoRik == 1)
                            {
                                if (procesar == 1)
                                {
                                    alertaaut = new AlertaAutorizacion();
                                    alertaautdet = new AlertaAutorizacion();
                                    cn_alertaautorizacion = new CN_AlertaAutorizacion();
                                    alertaaut.Id_Emp = session.Id_Emp;
                                    alertaaut.Id_Cd = session.Id_Cd_Ver;
                                    alertaaut.Id_Cte = Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1");
                                    alertaaut.Cte_NomComercial = ddlClienteNom.Text.Trim() != "" ? ddlClienteNom.Text.Trim() : "";
                                    alertaaut.Id_Prd = Convert.ToInt64(dtTemp.Rows[x]["Id_Prd"]);
                                    alertaaut.IdRepresentante = Convert.ToInt32(txtIdRik.Value);
                                    alertaaut.Id_Ter = Convert.ToInt32(txtIdTer.Value);
                                    double Preciodefactura = Convert.ToDouble(dtTemp.Rows[x]["Prd_Precio"]);
                                    alertaaut.Precio_Vta = Preciodefactura;
                                    cn_alertaautorizacion.AlertaPrecioConsultaPrecio(alertaaut, ref alertaautdet, ConexionCentral);

                                    if (alertaautdet != null && alertaautdet.Precio_MinimoRik > 0)
                                    {

                                        //JFCV 2NOV precioObjetivo if (Math.Round(Preciodefactura, 3) < alertaautdet.Precio_MinimoRik)
                                        if (Math.Round(Preciodefactura, 3) < alertaautdet.PrecioObjetivo)
                                        {
                                            ProductosAlerta.Add(alertaautdet.Id_Prd.ToString().Trim());
                                            pasoalertaautdet = new AlertaAutorizacion();

                                            pasoalertaautdet.Id_Emp = session.Id_Emp;
                                            pasoalertaautdet.Id_Cd = session.Id_Cd_Ver;
                                            pasoalertaautdet.Id_Cte = Convert.ToInt32(txtIdCte.Value.ToString().Trim() != "" ? txtIdCte.Value.ToString().Trim() : "-1");
                                            pasoalertaautdet.IdRepresentante = Convert.ToInt32(txtIdRik.Value);
                                            pasoalertaautdet.Id_Ter = Convert.ToInt32(txtIdTer.Value);  //jfcv 22sep Convert.ToInt32(ddlTerritorioNom.Value);
                                            pasoalertaautdet.IdAutorizacionAnterior = alertaautdet.IdAutorizacionAnterior;
                                            pasoalertaautdet.Id_Prd = alertaautdet.Id_Prd;
                                            pasoalertaautdet.Precio_MinimoRik = Math.Round(Convert.ToDouble(alertaautdet.Precio_MinimoRik), 3);
                                            pasoalertaautdet.Precio_MinimoGte = Math.Round(Convert.ToDouble(alertaautdet.Precio_MinimoGte), 3);
                                            pasoalertaautdet.Precio_Vta = Preciodefactura;
                                            pasoalertaautdet.PrecioLista = alertaautdet.PrecioLista;
                                            pasoalertaautdet.Cte_NomComercial = alertaaut.Cte_NomComercial;
                                            pasoalertaautdet.Prd_Descripcion = alertaautdet.Prd_Descripcion;
                                            pasoalertaautdet.PrecioLista = alertaautdet.PrecioLista;
                                            pasoalertaautdet.Cantidad = Convert.ToInt32(dtTemp.Rows[x]["Prd_Cantidad"]);
                                            pasoalertaautdet.Precio_AAA = alertaautdet.Precio_AAA;
                                            pasoalertaautdet.Utilidad = alertaautdet.Utilidad;
                                            pasoalertaautdet.Porc_Utilidad = alertaautdet.Utilidad / Preciodefactura;
                                            pasoalertaautdet.Importe = Preciodefactura * pasoalertaautdet.Cantidad;
                                            pasoalertaautdet.Importe_Utilidad = pasoalertaautdet.Utilidad * pasoalertaautdet.Cantidad;
                                            pasoalertaautdet.Justificacion = "";
                                            pasoalertaautdet.Prd_Descripcion = Convert.ToString(dtTemp.Rows[x]["Prd_Descripcion"]);
                                            pasoalertaautdet.Cte_NomComercial = ddlClienteNom.Text.Trim() != "" ? ddlClienteNom.Text.Trim() : "";
                                            pasoalertaautdet.TipoAutorizacion = 4; //Pedido
                                            pasoalertaautdet.FechaVigencia = DateTime.Now.AddMonths(12);
                                            pasoalertaautdet.Id_Cpr = alertaautdet.Id_Cpr;
                                            pasoalertaautdet.JustificacionMemo = "";
                                            //JFCV 2NOV precioObjetivo 
                                            pasoalertaautdet.Id_Tamaño = alertaautdet.Id_Tamaño;
                                            pasoalertaautdet.PrecioObjetivo = alertaautdet.PrecioObjetivo;
                                            lalertasautdet.Add(pasoalertaautdet);

                                        }

                                    }
                                }
                            }
                            #endregion validar precios que requieran autorización
                            //JFCV fin Valido si el precio de venta es menor al minimo
                        }// si prod es menor a 999999999999
                    }  //Termina ciclo  convenios  
                }// si tiene artículos 


                if (prodAAA != "")
                {
                    mensajeInfo("El precio de venta de los siguiente produtos no puede ser menor al Precio AAA del producto: " + prodAAA);
                    return;
                }


                //Parte comentada ya que como es una solicitus primero se verificara que se autorize el pedido

                //Session["ProdsConv" + Session.SessionID] = null;
                //Session["Id_FacPrec" + Session.SessionID] = null;
                //Session["lConvPrecios" + Session.SessionID] = null;

                //if (Productos.Count > 0 && lconveniosdet.Count > 0)
                //{
                //    Session["ProdsConv" + Session.SessionID] = Productos;
                //    Session["lConvPrecios" + Session.SessionID] = lconveniosdet;
                //    Session["Id_FacPrec" + Session.SessionID] = txtFolio.Value.ToString().Trim();
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirVentana_AlertaPrecios", "AbrirVentana_AlertaPrecios()", true);
                //    return;
                //}

                ////JFCV  alerta de precios autorización
                ////JFCV  Validar si tiene precios inferiores a los precios minimos

                //Session["ProdsAutorizacion" + Session.SessionID] = null;
                //Session["Id_FacPrec" + Session.SessionID] = null;
                //Session["lAurizacionPrecios" + Session.SessionID] = null;

                //if (procesar == 1) //23sep22 agregue esta condición para que cuando este regrabando no se detenga por los precios alertas
                //{
                //    if (ProductosAlerta.Count > 0 && lalertasautdet.Count > 0)
                //    {
                //        Session["ProdsAutorizacion" + Session.SessionID] = ProductosAlerta;
                //        Session["lAurizacionPrecios" + Session.SessionID] = lalertasautdet;
                //        Session["Id_FacPrec" + Session.SessionID] = txtFolio.Value.ToString().Trim();



                //        ScriptManager.RegisterStartupScript(this, this.GetType(), "AbrirVentana_AlertaAutorizacionPrecios", "AbrirVentana_AlertaAutorizacionPrecios()", true);


                //        return;
                //    }
                //}
                ////JFCV  FIn Validar si tiene precios inferiores a los precios minimos


                #endregion inicio validar precios convenio




                Session["Id_FacPrec" + Session.SessionID] = null;
                if (HF_ID.Value == "")
                {
                    if (!_PermisoGuardar)
                    {
                        mensajeInfo("No tiene permisos para grabar");
                        return;
                    }


                    long _prd = 0;
                    foreach (DataRow rows in dt.AsEnumerable())
                    {
                        _prd = Convert.ToInt64((rows[1]));
                        PedidoVtaInst pvi = new PedidoVtaInst();
                        pvi.Id_Emp = session.Id_Emp;
                        pvi.Id_Cd = session.Id_Cd_Ver;
                        pvi.Id_Cte = Convert.ToInt32(txtIdCte.Value);
                        pvi.Id_Ter = Convert.ToInt32(txtIdTer.Value);
                        pvi.Id_Acs = Convert.ToInt32(txtClave.Value);
                        clsCapPedido.ConsultarPedidoExistente(pvi, _prd, session.Emp_Cnx, ref verificador);

                        if (verificador == 1)
                        {
                            mensajeInfo("El producto " + _prd.ToString().Trim() + " ya ha sido captado por otro usuario");

                            return;
                        }
                    }


                    foreach (DataRow rows in dtTemp.AsEnumerable())
                    {
                        _prd = Convert.ToInt64((rows[1]));
                        PedidoVtaInst pvi = new PedidoVtaInst();
                        pvi.Id_Emp = session.Id_Emp;
                        pvi.Id_Cd = session.Id_Cd_Ver;
                        pvi.Id_Cte = Convert.ToInt32(txtIdCte.Value);
                        pvi.Id_Ter = Convert.ToInt32(txtIdTer.Value);
                        pvi.Id_Acs = Convert.ToInt32(txtClave.Value);
                        clsCapPedido.ConsultarPedidoExistente(pvi, _prd, session.Emp_Cnx, ref verificador);

                        if (verificador == 1)
                        {
                            mensajeInfo("El producto " + _prd.ToString().Trim() + " ya ha sido captado por otro usuario");
                            return;
                        }
                    }

                    string nombre = "";
                    string extension = "";
                    string archivo = "";
                    if (Request.QueryString["OrdenCompra"] != null)
                    {

                        if (Convert.ToBoolean(Request.QueryString["OrdenCompra"].ToString().Trim()))
                        {
                            cargarArchivo(ref nombre, ref extension, ref archivo);

                        }

                    }

                    if (Convert.ToInt32(txtClave.Text) != 0)
                    {
                        CapaEntidad.Acys acys = new Acys();
                        acys.Id_Emp = session.Id_Emp;
                        acys.Id_Cd = session.Id_Cd;
                        acys.Id_Acs = Convert.ToInt32(txtClave.Text);
                        CN_CapAcys cnCapAcys = new CN_CapAcys();
                        cnCapAcys.ConsultaUltimaVersion(ref acys, session.Emp_Cnx);

                        clsCapPedido.InsertarAutorizacion(pedido, dt, dtTemp, session.Emp_Cnx, ref verificador, Id_TG, acys.Id_AcsVersion);

                        if (verificador >= 1)
                        {
                            SetLastPedidoHash(currentHash);
                            if (Request.QueryString["OrdenCompra"] != null)
                            {
                                if (Convert.ToBoolean(Request.QueryString["OrdenCompra"].ToString().Trim()))
                                {

                                    pedido.Id_Ped = verificador;
                                    pedido.Id_Acs = Convert.ToInt32(txtClave.Text);
                                    pedido.OrdenCompra = TxtPed_ReqAcys.Text;
                                    pedido.Id_Cte = Convert.ToInt32(txtIdCte.Value);
                                    pedido.nombreDocumento = nombre;
                                    pedido.extension = extension;
                                    pedido.archivo = archivo;
                                    clsCapPedido.InsertarSolicitudOrderCompra(pedido, session.Emp_Cnx, ref verificador, Id_TG, acys.Id_AcsVersion);
                                }
                            }


                            Session["Id_Ped" + Session.SessionID] = verificador;
                            EnviarCorreosolicitud(verificador, session.Emp_Cnx);
                            mensajeExitoClaro("Se realizo la Solicitud para captar el pedido con el folio: " + verificador.ToString().Trim().Trim());


                        }
                        else
                        {
                            mensajeError("Ocurrió un error al intentar guardar el pedido");
                        }

                    }
                    else
                    {
                        clsCapPedido.InsertarAutorizacion(pedido, dt, dtTemp, session.Emp_Cnx, ref verificador, Id_TG, null);

                        if (verificador >= 1)
                        {
                            SetLastPedidoHash(currentHash);
                            if (Request.QueryString["OrdenCompra"] != null)
                            {
                                if (Convert.ToBoolean(Request.QueryString["OrdenCompra"].ToString().Trim()))
                                {

                                    pedido.Id_Ped = verificador;
                                    pedido.Id_Acs = Convert.ToInt32(txtClave.Text);
                                    pedido.OrdenCompra = TxtPed_ReqAcys.Text;
                                    pedido.Id_Cte = Convert.ToInt32(txtIdCte.Value);
                                    pedido.nombreDocumento = nombre;
                                    pedido.extension = extension;
                                    pedido.archivo = archivo;
                                    clsCapPedido.InsertarSolicitudOrderCompra(pedido, session.Emp_Cnx, ref verificador, Id_TG, 0);
                                }
                            }

                            string mensajes = "";
                            //EnviarCorreo(ddlClienteNom.SelectedItem.Text, session.U_Nombre, cliente, Usuario, session.Emp_Cnx, mensajes);

                            if (mensajes != "")
                            {
                                EnviarCorreosolicitud(verificador, session.Emp_Cnx);
                                mensajeExitoClaro("Se realizo la Solicitud para captar el pedido con el folio: " + verificador.ToString().Trim().Trim() + ". <br> " + mensajes);
                            }
                            else
                            {
                                EnviarCorreosolicitud(verificador, session.Emp_Cnx);
                                mensajeExitoClaro("Se realizo la Solicitud para captar el pedido con el folio: " + verificador.ToString().Trim().Trim());
                            }
                            btncaptacion_Guardar.Enabled = false;
                        }
                        else
                        {
                            mensajeError("Ocurrió un error al intentar guardar el pedido");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError($"GuardarSolicitud: fallo. Sesión={Session.SessionID}, Mensaje={ex.Message}");
                mensajeError("No se pudo completar la solicitud. Verifique su conexión y reintente.");
                ClearLastSolicitudHash();
                throw;
            }
        }

        public void CaptarPedido(int id_Sol, int id_ped)
        {
            try
            {
                CN_CapPedidoVtaInst CN = new CN_CapPedidoVtaInst();
                int verificador = 0;
                PedidoVtaInst pedido = new PedidoVtaInst();
                pedido.Id_Emp = session.Id_Emp;
                pedido.Id_Cd = session.Id_Cd_Ver;
                pedido.Id_U = session.Id_U;
                pedido.id_Sol = Convert.ToInt32(id_Sol);
                pedido.Id_Ped = id_ped;
                pedido.Estatus = "C";

                CN.ActualizarSolicitudPedido(pedido, session.Emp_Cnx, ref verificador);


            }
            catch (Exception ex)
            {
                new Exception("Error al monento de rechazar la solicitud");
            }
        }

        #endregion

        #region mensaje 

        private void mensajeInfo(string mensaje)
        {
            // Mensaje informativo
            ScriptManager.RegisterStartupScript(this, this.GetType(), "MensajeInfo", $"modalMensaje('{HttpUtility.HtmlEncode(mensaje)}')", true);
        }
        private void mensajeError(string mensaje)
        {
            // Mensaje de error claro
            ScriptManager.RegisterStartupScript(this, this.GetType(), "MensajeError", $"modalMensaje('<b>Error:</b> {HttpUtility.HtmlEncode(mensaje)}')", true);
        }
        private void mensajeExitoClaro(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "MensajeOk", $"modalMensajeExito('{HttpUtility.HtmlEncode(mensaje)}')", true);
        }
        #endregion

        #region webMethod

        [WebMethod]
        public static string cmbProductoDetRestos(string IdProd, string idterr, string idCte, string IdRik, string clave, string IdCd, string IdEmp, string EmpCnx, string pedidoProg)
        {
            CN_CatProducto cn_catproducto = new CN_CatProducto();
            Producto pr = new Producto();
            Producto pr2 = new Producto();
            int productoNuevo = 0;
            try
            {

                CN_CapAcys cnCa = new CN_CapAcys();
                if (idterr == "")
                {
                    return JsonConvert.SerializeObject(new { id = 1 });

                }
                if (idCte == "")
                {
                    return JsonConvert.SerializeObject(new { id = 2 });
                }
                if (IdRik == "")
                {
                    return JsonConvert.SerializeObject(new { id = 3 });
                }
                pedidoProg = pedidoProg == "0" ? "false" : pedidoProg;
                pedidoProg = pedidoProg == "1" ? "true" : pedidoProg;


                DataTable dt = (DataTable)HttpContext.Current.Session["Prod"];
                DataTable dt_Restos = (DataTable)HttpContext.Current.Session["Restos"];

                if (dt != null)
                {
                    foreach (DataRow i in dt.Rows)
                    {
                        if (i["Id_Prd"].ToString().Trim() == IdProd)
                        {
                            return JsonConvert.SerializeObject(new { id = 5 });
                        }

                    }
                }

                if (dt_Restos != null)
                {
                    foreach (DataRow j in dt_Restos.Rows)
                    {

                        if (j["Id_Prd"].ToString().Trim() == IdProd)
                        {
                            return JsonConvert.SerializeObject(new { id = 5 });
                        }
                    }
                }

                if (bool.Parse(pedidoProg) && cnCa.ExisteProductoEnGarantia(Convert.ToInt32(IdEmp), Convert.ToInt32(IdCd), Convert.ToInt64(IdProd), Convert.ToInt32(idterr), Convert.ToInt32(idCte), Convert.ToInt32(IdRik), EmpCnx))
                {
                    return JsonConvert.SerializeObject(new { id = 4 });
                }

                if (string.IsNullOrEmpty(clave))
                {
                    productoNuevo = 1;
                }
                pr.Id_Cte = !string.IsNullOrEmpty(idCte) ? Convert.ToInt32(idCte) : 0;
                cn_catproducto.ConsultaProductos(ref pr, EmpCnx, Convert.ToInt32(IdEmp), Convert.ToInt32(IdCd), Convert.ToInt64(IdProd), ref productoNuevo, 2);

                cn_catproducto.ConsultarVentas(ref pr2, Convert.ToInt32(idCte), EmpCnx);

                return JsonConvert.SerializeObject(new { id = 0, Presentacion = pr.Prd_Presentacion, PrdUni = pr.Prd_UniNs, Cant = 0, Precio = pr.Prd_Precio, imp = pr.Prd_Precio, PRecioLista = pr.Prd_PLista, Descripcion = pr.Prd_Descripcion, mes1 = pr2.ventaMes[0].ToString().Trim(), mes2 = pr2.ventaMes[1].ToString().Trim(), mes3 = pr2.ventaMes[2].ToString().Trim(), Prd_Activo = pr.Prd_Activo, Prd_InvFinal = pr.Prd_InvFinal });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = ex.Message.ToString().Trim() });

            }
        }

        [WebMethod]
        public static string txtCantidad_TextChanged(string cantidad, string precio, string idCte, string Id_prd, string IdCd, string IdEmp, string original, string asignado, string EmpCnx)
        {
            try
            {
                int Prd_Cantidad = 0;
                double Prd_Precio = 0;
                double importe = 0;

                if (cantidad != "")
                {
                    if (int.Parse(cantidad) <= 0)
                    {
                        return JsonConvert.SerializeObject(new { id = 1 });
                    }
                }
                else
                {
                    return JsonConvert.SerializeObject(new { id = 1 });
                }

                if (!string.IsNullOrEmpty(cantidad))
                    Prd_Cantidad = Convert.ToInt32(cantidad);
                if (!string.IsNullOrEmpty(precio))
                    Prd_Precio = Convert.ToDouble(precio);

                importe = Prd_Cantidad * Prd_Precio;

                List<int> Actuales = new List<int>();
                CN_CatProducto catproducto = new CN_CatProducto();
                catproducto.ConsultaProducto_Disponible(Convert.ToInt32(IdEmp), Convert.ToInt32(IdCd), Id_prd, ref Actuales, EmpCnx);

                CN_CapPedidoVtaInst pedido_vta = new CN_CapPedidoVtaInst();
                int verificador = 0;
                if (!string.IsNullOrEmpty(idCte))
                    pedido_vta.ConsultarAAAEspecial(Convert.ToInt32(IdEmp), Convert.ToInt32(IdCd), Convert.ToInt32(idCte), Id_prd, ref verificador, EmpCnx);
                if (verificador > 0)
                {
                    return JsonConvert.SerializeObject(new { id = 3 });
                }
                else
                {
                    if (Actuales.Count > 0)
                    {
                        if (Prd_Cantidad > int.Parse(original) && int.Parse(original) != 0 && int.Parse(asignado) > 0)
                        {
                            importe = int.Parse(original) * Prd_Precio;
                            return JsonConvert.SerializeObject(new { id = 2, cantidad = original, importe = importe });
                        }
                        else
                        {
                            return JsonConvert.SerializeObject(new { id = 0, importe = importe });
                        }
                    }
                    else
                    {
                        return JsonConvert.SerializeObject(new { id = 0, importe = importe });

                    }
                }
            }
            catch (Exception)
            {
                return JsonConvert.SerializeObject(new { id = -1, mensaje = new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name });
            }
        }

        [WebMethod]
        public static string txtPrecio_TextChanged(string cantidad, string precio, string idCte, string Id_prd, string IdCd, string IdEmp, string EmpCnx)
        {
            try
            {
                CN_CapPedidoVtaInst pedido_vta = new CN_CapPedidoVtaInst();
                int verificador = 0;
                pedido_vta.ConsultarAAAEspecial(Convert.ToInt32(IdEmp), Convert.ToInt32(IdCd), Convert.ToDouble(idCte), Id_prd, ref verificador, EmpCnx);

                if (verificador > 0)
                {
                    return JsonConvert.SerializeObject(new { id = 1 });
                }

                double importe = double.Parse(cantidad) * double.Parse(precio);
                return JsonConvert.SerializeObject(new { id = 0, importe = importe });
            }
            catch (Exception)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name });

            }
        }

        [WebMethod(EnableSession = true)]
        public static string CalcularTotalVisible(string IdCd, string IdEmp, string IVA, string EmpCnx)
        {
            try
            {
                double imp = 0;

                DataTable dt = (DataTable)HttpContext.Current.Session["Prod"];
                DataTable dt_Restos = (DataTable)HttpContext.Current.Session["Restos"];

                if (dt != null)
                {
                    foreach (DataRow i in dt.Rows)
                    {
                        imp += Convert.ToDouble(i["Prd_Importe"] == DBNull.Value ? 0 : i["Prd_Importe"]);
                    }
                }

                if (dt_Restos != null)
                {
                    foreach (DataRow j in dt_Restos.Rows)
                    {
                        imp += Convert.ToDouble(j["Prd_Importe"] == DBNull.Value ? 0 : j["Prd_Importe"]);
                    }
                }
                imp.ToString().Trim();

                imp.ToString("F2").Trim();
                string IVASistema = (imp * double.Parse(IVA) / 100).ToString("F2").Trim();
                string totalImporte = (Convert.ToDouble(imp) + Convert.ToDouble(IVASistema)).ToString("F2").Trim();

                return JsonConvert.SerializeObject(new { id = 1, subtotal = imp, iva = IVASistema, total = totalImporte });
            }
            catch (Exception)
            {
                return JsonConvert.SerializeObject(new { id = -1, men = "Error al realizar el el total de importe" });
            }

        }

        #endregion

        protected void ddlTerritorioNom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTerritorioNom.Value.ToString() != "")
            {
                CN_CatTerritorios cn_terr = new CN_CatTerritorios();
                Territorios terr = new Territorios();
                terr.Id_Emp = session.Id_Emp;
                terr.Id_Cd = session.Id_Cd_Ver;
                terr.Id_Ter = Convert.ToInt32(ddlTerritorioNom.Value.ToString());
                cn_terr.ConsultaTerritorios(ref terr, session.Emp_Cnx);
                txtRikNom.Value = terr.Rik_Nombre;
                txtIdRik.Value = terr.Id_Rik.ToString();
                txtIdTer.Value = ddlTerritorioNom.Value.ToString();
            }
        }

        protected void rg1_DataBound(object sender, EventArgs e)
        {

        }

        protected void rg1_HtmlDataCellPrepared(object sender, BootstrapGridViewTableDataCellEventArgs e)
        {
            bool inactivos = false;
            if (e.DataColumn.FieldName == "Id_Prd")
            {

                var prdActivoValue = e.GetValue("Prd_Activo");

                int prdActivo = 1;

                if (prdActivoValue != DBNull.Value)
                {
                    prdActivo = Convert.ToInt32(prdActivoValue);
                }

                if (prdActivo == 2)
                {
                    e.Cell.BackColor = System.Drawing.Color.Red;
                    e.Cell.ForeColor = System.Drawing.Color.White;
                    inactivos = true;
                }

                if (inactivos)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(System.Web.UI.Page), "Alerta", ProPedidoVI2.Mensajes("Hay productos inactivos en el detalle de captación de pedidos"), false);
                }
            }
        }

        protected void rg1_Restos_HtmlDataCellPrepared(object sender, BootstrapGridViewTableDataCellEventArgs e)
        {
            bool inactivos = false;
            if (e.DataColumn.FieldName == "Id_Prd")
            {
                // Obtén el valor de la fila actual para Prd_ACtivo
                var prdActivoValue = e.GetValue("Prd_Activo");

                int prdActivo = 1;

                if (prdActivoValue != DBNull.Value)
                {
                    prdActivo = Convert.ToInt32(prdActivoValue);
                }

                // Verifica la condición
                if (prdActivo == 2)
                {
                    // Cambia el color de fondo de la celda a rojo
                    e.Cell.BackColor = System.Drawing.Color.Red;
                    e.Cell.ForeColor = System.Drawing.Color.White;
                    inactivos = true;
                }
            }

            if (inactivos)
            {
                ScriptManager.RegisterStartupScript(this, typeof(System.Web.UI.Page), "Alerta", ProPedidoVI2.Mensajes("Hay productos inactivos en el detalle de captación de pedidos"), false);
            }
        }
    }

    public static class UploadControlHelper2
    {
        const int DisposeTimeout = 5;
        const string TempDirectory = "~/UploadControl/Temp/";
        static readonly object storageListLocker = new object();

        static HttpContext Context { get { return HttpContext.Current; } }
        static string RootDirectory { get { return Context.Request.MapPath(TempDirectory); } }

        static IList<UploadedFilesStorage> uploadedFilesStorageList;
        static IList<UploadedFilesStorage> UploadedFilesStorageList
        {
            get
            {
                return uploadedFilesStorageList;
            }
        }

        static UploadControlHelper2()
        {
            uploadedFilesStorageList = new List<UploadedFilesStorage>();
        }

        static string CreateTempDirectoryCore()
        {
            string uploadDirectory = Path.Combine(RootDirectory, Path.GetRandomFileName());
            Directory.CreateDirectory(uploadDirectory);

            return uploadDirectory;
        }
        public static UploadedFilesStorage GetUploadedFilesStorageByKey(string key)
        {
            lock (storageListLocker)
            {
                return GetUploadedFilesStorageByKeyUnsafe(key);
            }
        }
        static UploadedFilesStorage GetUploadedFilesStorageByKeyUnsafe(string key)
        {
            UploadedFilesStorage storage = UploadedFilesStorageList.Where(i => i.Key == key).SingleOrDefault();
            if (storage != null)
                storage.LastUsageTime = DateTime.Now;
            return storage;
        }
        public static string GenerateUploadedFilesStorageKey()
        {
            return Guid.NewGuid().ToString("N");
        }
        public static void AddUploadedFilesStorage(string key)
        {
            lock (storageListLocker)
            {
                UploadedFilesStorage storage = new UploadedFilesStorage
                {
                    Key = key,
                    Path = CreateTempDirectoryCore(),
                    LastUsageTime = DateTime.Now,
                    Files = new List<UploadedFileInfo>()
                };
                UploadedFilesStorageList.Add(storage);
            }
        }
        public static void RemoveUploadedFilesStorage(string key)
        {
            lock (storageListLocker)
            {
                UploadedFilesStorage storage = GetUploadedFilesStorageByKeyUnsafe(key);
                if (storage != null)
                {
                    Directory.Delete(storage.Path, true);
                    UploadedFilesStorageList.Remove(storage);
                }
            }
        }
        public static void RemoveOldStorages()
        {
            if (!Directory.Exists(RootDirectory))
                Directory.CreateDirectory(RootDirectory);

            lock (storageListLocker)
            {
                string[] existingDirectories = Directory.GetDirectories(RootDirectory);
                foreach (string directoryPath in existingDirectories)
                {
                    UploadedFilesStorage storage = UploadedFilesStorageList.Where(i => i.Path == directoryPath).SingleOrDefault();
                    if (storage == null || (DateTime.Now - storage.LastUsageTime).TotalMinutes > DisposeTimeout)
                    {
                        Directory.Delete(directoryPath, true);
                        if (storage != null)
                            UploadedFilesStorageList.Remove(storage);
                    }
                }
            }
        }
        public static UploadedFileInfo AddUploadedFileInfo(string key, string originalFileName)
        {
            UploadedFilesStorage currentStorage = GetUploadedFilesStorageByKey(key);
            UploadedFileInfo fileInfo = new UploadedFileInfo
            {
                FilePath = Path.Combine(currentStorage.Path, Path.GetRandomFileName()),
                OriginalFileName = originalFileName,
                UniqueFileName = GetUniqueFileName(currentStorage, originalFileName)
            };
            currentStorage.Files.Add(fileInfo);

            return fileInfo;
        }
        public static UploadedFileInfo GetDemoFileInfo(string key, string fileName)
        {
            UploadedFilesStorage currentStorage = GetUploadedFilesStorageByKey(key);
            return currentStorage.Files.Where(i => i.UniqueFileName == fileName).SingleOrDefault();
        }
        public static string GetUniqueFileName(UploadedFilesStorage currentStorage, string fileName)
        {
            string baseName = Path.GetFileNameWithoutExtension(fileName);
            string ext = Path.GetExtension(fileName);
            int index = 1;

            while (currentStorage.Files.Any(i => i.UniqueFileName == fileName))
                fileName = string.Format("{0} ({1}){2}", baseName, index++, ext);

            return fileName;
        }
    }
}