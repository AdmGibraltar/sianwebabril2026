
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using Telerik.Web.UI;
using CapaEntidad;
using CapaDatos;
using CapaNegocios;
using System.IO;
using System.Runtime.Remoting;
using System.Text;
using Telerik.Reporting.Processing;
using System.Configuration;
using System.Xml;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;


namespace SIANWEB
{
    public partial class Rep_InvExceso : System.Web.UI.Page
    {
        #region Variables
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        #endregion
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        ValidarPermisos();
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }
                        Inicializar();

                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            string accionError = string.Empty;
            try
            {
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (_PermisoImprimir)
                    switch (btn.CommandName)
                    {
                        case "Envio":
                            Mostrar();
                            break;
                        case "Excel":
                            ExportarInventarioNoRota();
                            break;
                    }
                else
                    Alerta("No tiene permiso para imprimir");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ExportarInventarioNoRota()
        {
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];
            RepExcesos Exceso = new RepExcesos();

            Exceso.Id_Emp = sesion.Id_Emp;
            Exceso.Id_Cd = sesion.Id_Cd_Ver;
            Exceso.Id_U = sesion.Id_U;
            Exceso.Dias = 30;
            Exceso.Salida = 3;

            List<InventariosNoRota> ListNoRota = new List<InventariosNoRota>();
            List<InventariosNoRota> ListRota = new List<InventariosNoRota>();
            List<InventariosNoRota> ListNoRotaCierre = new List<InventariosNoRota>();
            List<InventariosNoRota> ListRotaCierre = new List<InventariosNoRota>();
            InventariosNoRota InvNoRota = new InventariosNoRota();
            CN_InventariosNoRota CN_InvNoRota = new CN_InventariosNoRota();

            //1.- Actual
            if (RblTipoRep.SelectedValue == "1")
            {
                //No Rota Actual 0
                Exceso.Rota = 0;
                CN_InvNoRota.ConsultaInventarioNoRotaDetalle(sesion.Emp_Cnx, ref ListNoRota, Exceso);
                //Rota Actual 1
                Exceso.Rota = 1;
                CN_InvNoRota.ConsultaInventarioNoRotaDetalle(sesion.Emp_Cnx, ref ListRota, Exceso);
                InvNoRotaActualDetalle(ListNoRota, ListRota);
            }
            else
            {
                //No Rota Cierre
                CN_InvNoRota.ConsultaInventarioNoRotaDetalleCierre(sesion.Emp_Cnx, ref ListNoRotaCierre, Exceso);
                InvNoRotaCierreDetalle(ListNoRotaCierre);
            }
        }

        private void InvNoRotaCierreDetalle(List<InventariosNoRota> ListNoRota)
        {
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];
            string ruta = null;
            Random rnd = new Random();
            string Cat_Nombre = null;
            DataTable dtProductos = new DataTable();
            CargarCatalogoUnico("", ref dtProductos);
            int nro = rnd.Next(0, 8);
            string tipo = "Excesos_InvCierre";
            ruta = Server.MapPath("Reportes") + "\\Reporte_" + tipo + nro + ".xlsx";

            if (File.Exists(ruta))
                File.Delete(ruta);
            if (File.Exists(Server.MapPath("Reportes") + "\\Reporte_" + tipo + nro + ".xls"))
                File.Delete(Server.MapPath("Reportes") + "\\Reporte_" + tipo + nro + ".xls");

            if ((ListNoRota != null))
            {
                if (!(ListNoRota.Count() == 0))
                {
                    using (ExcelPackage p = new ExcelPackage())
                    {
                        //set the workbook properties and add a default sheet in it
                        SetWorkbookProperties(p);
                        //Create a sheet
                        int rowIndex0 = 1;
                        int rowIndex1 = 1;

                        #region Pestaña No Rota
                        DataTable dtNoRota = new DataTable();
                        DataColumn Id_Cd = new DataColumn("Id_Cd", Type.GetType("System.Int32"));
                        DataColumn Sucursal = new DataColumn("Sucursal", Type.GetType("System.String"));
                        DataColumn Proveedor = new DataColumn("Proveedor", Type.GetType("System.Int32"));
                        DataColumn Codigo = new DataColumn("Codigo", Type.GetType("System.Int64"));
                        DataColumn Descripcion = new DataColumn("Descripcion", Type.GetType("System.String"));
                        DataColumn Monto = new DataColumn("Monto", Type.GetType("System.Double"));
                        DataColumn Cantidad = new DataColumn("Cantidad", Type.GetType("System.Int32"));
                        DataColumn Disponible = new DataColumn("Disponible", Type.GetType("System.Int32"));
                        DataColumn Estatus = new DataColumn("Estatus", Type.GetType("System.String"));
                        DataColumn Categoria = new DataColumn("Categoria", Type.GetType("System.String"));

                        dtNoRota.Columns.Add(Id_Cd);
                        dtNoRota.Columns.Add(Sucursal);
                        dtNoRota.Columns.Add(Proveedor);
                        dtNoRota.Columns.Add(Codigo);
                        dtNoRota.Columns.Add(Descripcion);
                        dtNoRota.Columns.Add(Monto);
                        dtNoRota.Columns.Add(Cantidad);
                        dtNoRota.Columns.Add(Disponible);
                        dtNoRota.Columns.Add(Estatus);
                        dtNoRota.Columns.Add(Categoria);

                        foreach (InventariosNoRota Inv_NoRota in ListNoRota.FindAll(b => b.Rota == false))
                        {
                            foreach (DataRow row in dtProductos.Select("Producto = '" + Inv_NoRota.Codigo.ToString() + "'"))
                            {
                                Cat_Nombre = row["TipoProducto"].ToString();
                            }
                            DataRow drFila = null;
                            drFila = dtNoRota.NewRow();
                            drFila["Id_Cd"] = Inv_NoRota.Id_Cd;
                            drFila["Sucursal"] = Inv_NoRota.Sucursal;
                            drFila["Proveedor"] = Inv_NoRota.Proveedor;
                            drFila["Codigo"] = Inv_NoRota.Codigo;
                            drFila["Descripcion"] = Inv_NoRota.Descripcion;
                            drFila["Monto"] = Inv_NoRota.Monto;
                            drFila["Cantidad"] = Inv_NoRota.Cantidad;
                            drFila["Disponible"] = Inv_NoRota.Disponible;
                            drFila["Estatus"] = Inv_NoRota.Estatus;
                            drFila["Categoria"] = Cat_Nombre;

                            dtNoRota.Rows.Add(drFila);
                            dtNoRota.AcceptChanges();
                        }
                        #endregion

                        #region Pestaña Rota

                        DataTable dtRota = new DataTable();
                        DataColumn Id_Cd1 = new DataColumn("Id_Cd", Type.GetType("System.Int32"));
                        DataColumn Sucursal1 = new DataColumn("Sucursal", Type.GetType("System.String"));
                        DataColumn Proveedor1 = new DataColumn("Proveedor", Type.GetType("System.Int32"));
                        DataColumn Codigo1 = new DataColumn("Codigo", Type.GetType("System.Int64"));
                        DataColumn Descripcion1 = new DataColumn("Descripcion", Type.GetType("System.String"));
                        DataColumn Monto1 = new DataColumn("Monto", Type.GetType("System.Double"));
                        DataColumn Cantidad1 = new DataColumn("Cantidad", Type.GetType("System.Int32"));
                        DataColumn Disponible1 = new DataColumn("Disponible", Type.GetType("System.Int32"));
                        DataColumn Estatus1 = new DataColumn("Estatus", Type.GetType("System.String"));
                        DataColumn Categoria1 = new DataColumn("Categoria", Type.GetType("System.String"));

                        dtRota.Columns.Add(Id_Cd1);
                        dtRota.Columns.Add(Sucursal1);
                        dtRota.Columns.Add(Proveedor1);
                        dtRota.Columns.Add(Codigo1);
                        dtRota.Columns.Add(Descripcion1);
                        dtRota.Columns.Add(Monto1);
                        dtRota.Columns.Add(Cantidad1);
                        dtRota.Columns.Add(Disponible1);
                        dtRota.Columns.Add(Estatus1);
                        dtRota.Columns.Add(Categoria1);

                        foreach (InventariosNoRota Inv_Rota in ListNoRota.FindAll(b => b.Rota == true))
                        {
                            foreach (DataRow row in dtProductos.Select("Producto = '" + Inv_Rota.Codigo.ToString() + "'"))
                            {
                                Cat_Nombre = row["TipoProducto"].ToString();
                            }

                            DataRow drFila = null;
                            drFila = dtRota.NewRow();
                            drFila["Id_Cd"] = Inv_Rota.Id_Cd;
                            drFila["Sucursal"] = Inv_Rota.Sucursal;
                            drFila["Proveedor"] = Inv_Rota.Proveedor;
                            drFila["Codigo"] = Inv_Rota.Codigo;
                            drFila["Descripcion"] = Inv_Rota.Descripcion;
                            drFila["Monto"] = Inv_Rota.Monto;
                            drFila["Cantidad"] = Inv_Rota.Cantidad;
                            drFila["Disponible"] = Inv_Rota.Disponible;
                            drFila["Estatus"] = Inv_Rota.Estatus;
                            drFila["Categoria"] = Cat_Nombre;

                            dtRota.Rows.Add(drFila);
                            dtRota.AcceptChanges();
                        }

                        #endregion

                        //Creación de pestañas para el reporte
                        Funcion func = new Funcion();
                        ExcelWorksheet wsNoRota = CreateSheet(p, func.getCDIName(sesion.Id_Cd) + " - No Rota", 1);
                        ExcelWorksheet wsRota = CreateSheet(p, func.getCDIName(sesion.Id_Cd) + " - Si Rota", 2);

                        //Reporte No Rota
                        CreateHeader(wsNoRota, ref rowIndex0, dtNoRota);
                        CreateData(wsNoRota, ref rowIndex0, dtNoRota);
                        wsNoRota.Column(1).AutoFit();
                        wsNoRota.Column(2).AutoFit();
                        wsNoRota.Column(3).AutoFit();
                        wsNoRota.Column(4).AutoFit();
                        wsNoRota.Column(5).AutoFit();
                        wsNoRota.Column(6).AutoFit();
                        wsNoRota.Column(7).AutoFit();
                        wsNoRota.Column(8).AutoFit();
                        wsNoRota.Column(9).AutoFit();
                        wsNoRota.Column(10).AutoFit();

                        //Reporte Rota
                        CreateHeader(wsRota, ref rowIndex1, dtRota);
                        CreateData(wsRota, ref rowIndex1, dtRota);
                        wsRota.Column(1).AutoFit();
                        wsRota.Column(2).AutoFit();
                        wsRota.Column(3).AutoFit();
                        wsRota.Column(4).AutoFit();
                        wsRota.Column(5).AutoFit();
                        wsRota.Column(6).AutoFit();
                        wsRota.Column(7).AutoFit();
                        wsRota.Column(8).AutoFit();
                        wsRota.Column(9).AutoFit();
                        wsRota.Column(10).AutoFit();

                        Byte[] bin = p.GetAsByteArray();
                        File.WriteAllBytes(ruta, bin);

                        if (File.Exists(ruta))
                        {
                            string ruta2 = null;
                            ruta2 = Server.MapPath("Reportes") + "\\Reporte_" + tipo + nro + ".xlsx";
                            //Response.Redirect("Reportes\\Reporte" + tipo + nro + ".xlsx", false);
                            RAM1.Redirect("Reportes\\Reporte" + tipo + nro + ".xlsx");
                        }
                    }
                }
                else
                {
                    Alerta("No existen registros para el rango de fecha solicitado o tipo de queja seleccionado. Favor de seleccionar otro rango o tipo de queja.");
                }
            }
            else
            {
                Alerta("No existen registros para el rango de fecha solicitado o tipo de queja seleccionado. Favor de seleccionar otro rango o tipo de queja.");
            }
        }

        private void InvNoRotaActualDetalle(List<InventariosNoRota> ListNoRota, List<InventariosNoRota> ListRota)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                string ruta = null;
                Random rnd = new Random();
                string Cat_Nombre = null;
                DataTable dtProductos = new DataTable();
                CargarCatalogoUnico("", ref dtProductos);
                int nro = rnd.Next(0, 8);
                string tipo = "Excesos_InvActuales";
                ruta = Server.MapPath("Reportes") + "\\Reporte_" + tipo + nro + ".xlsx";

                if (File.Exists(ruta))
                    File.Delete(ruta);
                if (File.Exists(Server.MapPath("Reportes") + "\\Reporte_" + tipo + nro + ".xls"))
                    File.Delete(Server.MapPath("Reportes") + "\\Reporte_" + tipo + nro + ".xls");

                if ((ListNoRota != null))
                {
                    if (!(ListNoRota.Count() == 0))
                    {
                        using (ExcelPackage p = new ExcelPackage())
                        {
                            //set the workbook properties and add a default sheet in it
                            //Create a sheet
                            int rowIndex0 = 1;
                            int rowIndex1 = 1;

                            #region Pestaña No Rota
                            DataTable dtNoRota = new DataTable();
                            DataColumn Id_Cd = new DataColumn("Id_Cd", Type.GetType("System.Int32"));
                            DataColumn Sucursal = new DataColumn("Sucursal", Type.GetType("System.String"));
                            DataColumn Proveedor = new DataColumn("Proveedor", Type.GetType("System.Int32"));
                            DataColumn Codigo = new DataColumn("Codigo", Type.GetType("System.Int64"));
                            DataColumn Descripcion = new DataColumn("Descripcion", Type.GetType("System.String"));
                            DataColumn Monto = new DataColumn("Monto", Type.GetType("System.Double"));
                            DataColumn Cantidad = new DataColumn("Cantidad", Type.GetType("System.Int32"));
                            DataColumn Disponible = new DataColumn("Disponible", Type.GetType("System.Int32"));
                            DataColumn Estatus = new DataColumn("Estatus", Type.GetType("System.String"));
                            DataColumn Categoria = new DataColumn("Categoria", Type.GetType("System.String"));

                            dtNoRota.Columns.Add(Id_Cd);
                            dtNoRota.Columns.Add(Sucursal);
                            dtNoRota.Columns.Add(Proveedor);
                            dtNoRota.Columns.Add(Codigo);
                            dtNoRota.Columns.Add(Descripcion);
                            dtNoRota.Columns.Add(Monto);
                            dtNoRota.Columns.Add(Cantidad);
                            dtNoRota.Columns.Add(Disponible);
                            dtNoRota.Columns.Add(Estatus);
                            dtNoRota.Columns.Add(Categoria);

                            foreach (InventariosNoRota Inv_NoRota in ListNoRota.ToList())
                            {
                                foreach (DataRow row in dtProductos.Select("Producto = '" + Inv_NoRota.Codigo.ToString() + "'"))
                                {
                                    Cat_Nombre = row["TipoProducto"].ToString();
                                }
                                DataRow drFila = null;
                                drFila = dtNoRota.NewRow();
                                drFila["Id_Cd"] = Inv_NoRota.Id_Cd;
                                drFila["Sucursal"] = Inv_NoRota.Sucursal;
                                drFila["Proveedor"] = Inv_NoRota.Proveedor;
                                drFila["Codigo"] = Inv_NoRota.Codigo;
                                drFila["Descripcion"] = Inv_NoRota.Descripcion;
                                drFila["Monto"] = Inv_NoRota.Monto;
                                drFila["Cantidad"] = Inv_NoRota.Cantidad;
                                drFila["Disponible"] = Inv_NoRota.Disponible;
                                drFila["Estatus"] = Inv_NoRota.Estatus;
                                drFila["Categoria"] = Cat_Nombre;

                                dtNoRota.Rows.Add(drFila);
                                dtNoRota.AcceptChanges();
                            }
                            #endregion

                            #region Pestaña Rota

                            DataTable dtRota = new DataTable();
                            DataColumn Id_Cd1 = new DataColumn("Id_Cd", Type.GetType("System.Int32"));
                            DataColumn Sucursal1 = new DataColumn("Sucursal", Type.GetType("System.String"));
                            DataColumn Proveedor1 = new DataColumn("Proveedor", Type.GetType("System.Int32"));
                            DataColumn Codigo1 = new DataColumn("Codigo", Type.GetType("System.Int64"));
                            DataColumn Descripcion1 = new DataColumn("Descripcion", Type.GetType("System.String"));
                            DataColumn Monto1 = new DataColumn("Monto", Type.GetType("System.Double"));
                            DataColumn Cantidad1 = new DataColumn("Cantidad", Type.GetType("System.Int32"));
                            DataColumn Disponible1 = new DataColumn("Disponible", Type.GetType("System.Int32"));
                            DataColumn Estatus1 = new DataColumn("Estatus", Type.GetType("System.String"));
                            DataColumn Categoria1 = new DataColumn("Categoria", Type.GetType("System.String"));

                            dtRota.Columns.Add(Id_Cd1);
                            dtRota.Columns.Add(Sucursal1);
                            dtRota.Columns.Add(Proveedor1);
                            dtRota.Columns.Add(Codigo1);
                            dtRota.Columns.Add(Descripcion1);
                            dtRota.Columns.Add(Monto1);
                            dtRota.Columns.Add(Cantidad1);
                            dtRota.Columns.Add(Disponible1);
                            dtRota.Columns.Add(Estatus1);
                            dtRota.Columns.Add(Categoria1);

                            foreach (InventariosNoRota Inv_Rota in ListRota.ToList())
                            {
                                foreach (DataRow row in dtProductos.Select("Producto = '" + Inv_Rota.Codigo.ToString() + "'"))
                                {
                                    Cat_Nombre = row["TipoProducto"].ToString();
                                }

                                DataRow drFila = null;
                                drFila = dtRota.NewRow();
                                drFila["Id_Cd"] = Inv_Rota.Id_Cd;
                                drFila["Sucursal"] = Inv_Rota.Sucursal;
                                drFila["Proveedor"] = Inv_Rota.Proveedor;
                                drFila["Codigo"] = Inv_Rota.Codigo;
                                drFila["Descripcion"] = Inv_Rota.Descripcion;
                                drFila["Monto"] = Inv_Rota.Monto;
                                drFila["Cantidad"] = Inv_Rota.Cantidad;
                                drFila["Disponible"] = Inv_Rota.Disponible;
                                drFila["Estatus"] = Inv_Rota.Estatus;
                                drFila["Categoria"] = Cat_Nombre;

                                dtRota.Rows.Add(drFila);
                                dtRota.AcceptChanges();
                            }


                            #endregion

                            //Creación de pestañas para el reporte
                            Funcion func = new Funcion();
                            ExcelWorksheet wsNoRota = CreateSheet(p, func.getCDIName(sesion.Id_Cd) + " - No Rota", 1);
                            ExcelWorksheet wsRota = CreateSheet(p, func.getCDIName(sesion.Id_Cd) + " - Si Rota", 2);

                            //Reporte No Rota
                            CreateHeader(wsNoRota, ref rowIndex0, dtNoRota);
                            CreateData(wsNoRota, ref rowIndex0, dtNoRota);
                            wsNoRota.Column(1).AutoFit();
                            wsNoRota.Column(2).AutoFit();
                            wsNoRota.Column(3).AutoFit();
                            wsNoRota.Column(4).AutoFit();
                            wsNoRota.Column(5).AutoFit();
                            wsNoRota.Column(6).AutoFit();
                            wsNoRota.Column(7).AutoFit();
                            wsNoRota.Column(8).AutoFit();
                            wsNoRota.Column(9).AutoFit();
                            wsNoRota.Column(10).AutoFit();

                            //Reporte Rota
                            CreateHeader(wsRota, ref rowIndex1, dtRota);
                            CreateData(wsRota, ref rowIndex1, dtRota);
                            wsRota.Column(1).AutoFit();
                            wsRota.Column(2).AutoFit();
                            wsRota.Column(3).AutoFit();
                            wsRota.Column(4).AutoFit();
                            wsRota.Column(5).AutoFit();
                            wsRota.Column(6).AutoFit();
                            wsRota.Column(7).AutoFit();
                            wsRota.Column(8).AutoFit();
                            wsRota.Column(9).AutoFit();
                            wsRota.Column(10).AutoFit();

                            Byte[] bin = p.GetAsByteArray();
                            File.WriteAllBytes(ruta, bin);

                            if (File.Exists(ruta))
                            {
                                string ruta2 = null;
                                ruta2 = Server.MapPath("Reportes") + "\\Reporte_" + tipo + nro + ".xlsx";
                                Response.Redirect("Reportes\\Reporte_" + tipo + nro + ".xlsx", false);
                            }
                        }
                    }
                    else
                    {
                        Alerta("No existen registros para el rango de fecha solicitado o tipo de queja seleccionado. Favor de seleccionar otro rango o tipo de queja.");
                    }
                }
                else
                {
                    Alerta("No existen registros para el rango de fecha solicitado o tipo de queja seleccionado. Favor de seleccionar otro rango o tipo de queja.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarCatalogoUnico(string Id_Prd, ref DataTable dtProductos)
        {

            try
            {
                wsCatalogoUnico.Service1 CatUnico = new wsCatalogoUnico.Service1();
                CatUnico.Url = ConfigurationManager.AppSettings["WS_CatalogoUnico"].ToString();

                XmlDocument xm = new System.Xml.XmlDocument();
                string Resultado = "";
                Resultado = CatUnico.XMLSGCUP_Interno("");
                xm.LoadXml(Resultado);

                StringReader theReader = new StringReader(Resultado);
                DataSet ds = new DataSet();
                ds.ReadXml(theReader);

                dtProductos = ds.Tables[0];
            }
            catch (Exception ex)
            {
                //this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }
        }

        private static void CreateHeader(ExcelWorksheet ws, ref int rowIndex, DataTable dt)
        {
            int colIndex = 1;
            foreach (DataColumn dc in dt.Columns) //Creating Headings
            {
                var cell = ws.Cells[rowIndex, colIndex];

                //Setting the background color of header cells to Gray
                var fill = cell.Style.Fill;
                fill.PatternType = ExcelFillStyle.Solid;
                fill.BackgroundColor.SetColor(Color.Azure);

                //Setting Top/left,right/bottom borders.
                var border = cell.Style.Border;
                border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;

                //Setting Value in cell
                cell.Value = dc.ColumnName;

                colIndex++;
            }
        }

        private static void CreateData(ExcelWorksheet ws, ref int rowIndex, DataTable dt)
        {
            int colIndex = 0;
            foreach (DataRow dr in dt.Rows) // Adding Data into rows
            {
                colIndex = 1;
                rowIndex++;

                foreach (DataColumn dc in dt.Columns)
                {
                    var cell = ws.Cells[rowIndex, colIndex];
                    //Setting Value in cell
                    if (dc.DataType == typeof(System.Double))
                    {
                        // JorgeRmz [30-09-22]: Formato tipo moneda.
                        cell.Value = dr[dc.ColumnName];
                        cell[rowIndex, colIndex].Style.Numberformat.Format = "$#,##0";
                    }
                    else
                    {
                        cell.Value = dr[dc.ColumnName];
                    }

                    //Setting borders of cell
                    var border = cell.Style.Border;
                    border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                    colIndex++;
                }
            }
        }

        private static void CreateFooter(ExcelWorksheet ws, ref int rowIndex, DataTable dt)
        {
            int colIndex = 0;
            foreach (DataColumn dc in dt.Columns) //Creating Formula in footers
            {
                colIndex++;
                var cell = ws.Cells[rowIndex, colIndex];

                //Setting Sum Formula
                cell.Formula = "Sum(" + ws.Cells[4, colIndex].Address + ":" + ws.Cells[rowIndex - 1, colIndex].Address + ")";

                //Setting Background fill color to Gray
                cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cell.Style.Fill.BackgroundColor.SetColor(Color.Aqua);
            }
        }

        private static string GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }

        private static ExcelWorksheet CreateSheet(ExcelPackage p, string sheetName, int index)
        {
            p.Workbook.Worksheets.Add(sheetName);
            ExcelWorksheet ws = p.Workbook.Worksheets[index];
            ws.Name = sheetName; //Setting Sheet's name
            ws.Cells.Style.Font.Size = 11; //Default font size for whole sheet
            ws.Cells.Style.Font.Name = "Calibri"; //Default Font name for whole sheet

            return ws;
        }

        private static ExcelWorksheet CreateSheet2(ExcelPackage p, string sheetName)
        {
            p.Workbook.Worksheets.Add(sheetName);
            ExcelWorksheet ws = p.Workbook.Worksheets[2];
            ws.Name = sheetName; //Setting Sheet's name
            ws.Cells.Style.Font.Size = 11; //Default font size for whole sheet
            ws.Cells.Style.Font.Name = "Calibri"; //Default Font name for whole sheet

            return ws;
        }

        private static void SetWorkbookProperties(ExcelPackage p)
        {
            //Here setting some document properties
            p.Workbook.Properties.Author = "Raúl Bórquez Martínez ";
            p.Workbook.Properties.Title = "Reportes Inventarios que no Rotan";
        }

        protected void cmbCentrosDist_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Inicializar();
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
            }
            catch (Exception ex)
            {
                ErrorManager(string.Concat(ex.Message, "Cmb_CentroDistribucion_IndexChanging_error"));
            }
        }
        #endregion
        #region Funciones
        private void ValidarPermisos()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Pagina pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                if (pag.Length > 1)
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                else
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;
                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, Sesion.Emp_Cnx);
                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;
                Permiso Permiso = new Permiso();
                Permiso.Id_U = Sesion.Id_U;
                Permiso.Id_Cd = Sesion.Id_Cd;
                Permiso.Sm_cve = pagina.Clave;
                //Esta clave depende de la pantalla
                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, Sesion.Emp_Cnx);

                if (Permiso.PAccesar == true)
                {
                    _PermisoGuardar = Permiso.PGrabar;
                    _PermisoModificar = Permiso.PModificar;
                    _PermisoEliminar = Permiso.PEliminar;
                    _PermisoImprimir = Permiso.PImprimir;
                }
                else
                    Response.Redirect("Inicio.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Inicializar()
        {
            try
            {
                cargarComboIndicador();
                cargarComboProveedor();
                cargarComboSucursal();
                cargarComboDias();
                cargarComboTipoProductos();
                CargarCentros();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void cargarComboTipoProductos()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Emp_Cnx, "spCatTipoProducto_Combo3", ref cmbTipoProducto);
                cmbTipoProducto.SelectedValue = "0";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void cargarComboDias()
        {
            try
            {//30|60|90|120
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                //this.cmbDias.Items.Insert(0, new RadComboBoxItem("--Seleccionar--", "-1"));
                this.cmbDias.Items.Insert(0, new RadComboBoxItem("30", "30"));
                this.cmbDias.Items.Insert(1, new RadComboBoxItem("60", "60"));
                //this.cmbDias.Items.Insert(3, new RadComboBoxItem("90", "90"));
                this.cmbDias.Items.Insert(2, new RadComboBoxItem("120", "120"));
                this.cmbDias.Items.Insert(3, new RadComboBoxItem("180", "180"));
                cmbDias.SelectedValue = "0";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void cargarComboSucursal()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                if (Sesion.U_MultiOfi == false)
                {
                    CN_Comun.LlenaCombo(2, Sesion.Id_Emp, Sesion.Id_U, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref cmbSucursal);
                    cmbSucursal.SelectedIndex = cmbSucursal.FindItemIndexByValue(Sesion.Id_Cd_Ver.ToString());
                    cmbSucursal.Enabled = false;
                }
                else
                {
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, Sesion.Id_Cd_Ver, Sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref cmbSucursal);
                    cmbSucursal.Items.Insert(0, new RadComboBoxItem("--Todos--", "-1"));
                    cmbSucursal.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void cargarComboProveedor()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Emp_Cnx, "spProveedores_Combo2", ref cmbProveedor);
                cmbProveedor.SelectedValue = "-1";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void cargarComboIndicador()
        {
            try
            { // Exceso de inventarios | Exceso de inventarios que rota | exceso de inventarios que no rota
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                //this.cmbIndicador.Items.Insert(0, new RadComboBoxItem("--Seleccionar--", "-2"));
                this.cmbIndicador.Items.Insert(0, new RadComboBoxItem("Exceso de inventarios", "-1"));
                this.cmbIndicador.Items.Insert(1, new RadComboBoxItem("Exceso de inventarios que rota", "1"));
                this.cmbIndicador.Items.Insert(2, new RadComboBoxItem("Exceso de inventarios que no rota", "0"));
                cmbIndicador.SelectedValue = "-1";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarCentros()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                if (Sesion.U_MultiOfi == false)
                {
                    CN_Comun.LlenaCombo(2, Sesion.Id_Emp, Sesion.Id_U, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.Visible = false;
                    this.TblEncabezado.Rows[0].Cells[2].InnerText = " " + CmbCentro.FindItemByValue(Sesion.Id_Cd_Ver.ToString()).Text;
                }
                else
                {
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, Sesion.Id_Cd_Ver, Sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = Sesion.Id_Cd_Ver.ToString();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Mostrar()
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            InvExceso inventario = new InvExceso();
            inventario.Id_Cd = sesion.Id_Cd_Ver;
            inventario.Indicador = Convert.ToInt32(cmbIndicador.SelectedValue);
            inventario.Proveedor = Convert.ToInt32(cmbProveedor.SelectedValue);
            inventario.Sucursal = Convert.ToInt32(cmbSucursal.SelectedValue);
            inventario.Dias = Convert.ToInt32(cmbDias.SelectedValue);
            inventario.TipoProductos = Convert.ToInt32(cmbTipoProducto.SelectedValue);

            RAM1.ResponseScripts.Add("popup('" + sesion.Id_U + "','" + inventario.Indicador + "','" + inventario.Proveedor + "','" + inventario.Sucursal + "','" + inventario.Dias + "','" + inventario.TipoProductos + "');");
        }
        #endregion
        #region ErrorManager
        private void Alerta(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 330, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
        private void ErrorManager()
        {
            try
            {
                this.lblMensaje.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ErrorManager(string Message)
        {
            try
            {
                this.lblMensaje.Text = Message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ErrorManager(Exception eme, string NombreFuncion)
        {
            try
            {
                this.lblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();

            }
            catch (Exception ex)
            {
                this.lblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }
        #endregion
    }
}