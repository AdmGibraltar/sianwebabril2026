using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.Text;
using System.Net;
using System.IO;
using Telerik.Reporting.Processing;
using System.Data.OleDb;
using Telerik.Web.UI.GridExcelBuilder;
using System.Collections;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;

namespace SIANWEB
{
    public partial class CapGestionPreciosH : System.Web.UI.Page
    {
        #region Variables
        public string NombreArchivo;
        public string NombreHojaExcel;

        public string NombreArchivopdf;
        public string NombreHojaPdf;

        private List<ConvenioHis> ListDet
        {
            get { return (List<ConvenioHis>)Session["ListDet" + Session.SessionID + HF_Cve.Value]; }
            set { Session["ListDet" + Session.SessionID + HF_Cve.Value] = value; }
        }
        public bool _PermisoGuardar
        {
            get
            {
                if (Session["Sesion" + Session.SessionID] == null)
                { return false; }
                return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]];
            }
            set
            { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; }
        }
        public bool _PermisoModificar
        {
            get
            {
                if (Session["Sesion" + Session.SessionID] == null)
                { return false; }
                return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]];
            }
            set
            { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; }
        }
        private Sesion sesion
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

        public DataTable dt_detalles
        {
            get
            {
                return Session["dt_DetallesRem" + Session.SessionID] as DataTable;
            }
            set
            {
                Session["dt_DetallesRem" + Session.SessionID] = value;
            }
        }
       
         
        //static int Id_ConvDet_A = -1; //id de la partida que se va actualizar
        private int Id_ConvDet_A
        {
            set { Session["Id_ConvDet_AREM" + Session.SessionID] = value; }
            get { int? st = (int?)Session["Id_ConvDet_AREM" + Session.SessionID]; return st == null ? -1 : (int)st; }
        }
        
        
        //static int Id_Prd_A;
        private int Id_Prd_A
        {
            set { Session["Id_Prd_AREM" + Session.SessionID] = value; }
            get { return (int)Session["Id_Prd_AREM" + Session.SessionID]; }
        }

        //JFCV TODO eliminar lo que no ocupe 

        #endregion Variables

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                if (Sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }
                else
                {

                    if (!Page.IsPostBack)
                    {
                        this.ValidarPermisos();
                        if (Sesion.Cu_Modif_Pass_Voluntario == false)
                        {
                            RAM1.ResponseScripts.Add("AbrirContrasenas(); return false;");
                            return;
                        }

                        HFId_PC.Value = Page.Request.QueryString["Id_PC"].ToString();
                        this.TxtKeyConvenio.Text = Page.Request.QueryString["Id_PC"].ToString();
                        HFTipoOp.Value = Page.Request.QueryString["TipoOp"].ToString();
                        ValidarPermisos();
                        this.CargarCentros();
                       
                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_Cve.Value = randObj.Next().ToString();
                        this.TblEncabezado.Visible = false;
                        List<ConvenioDet> List = new List<ConvenioDet>();
 

                        crearDT();
                        if (this.HFId_PC.Value != "0")
                        {
                            this.rgDetalles.DataSource = ConsultaConvenioHis();
                            rgDetalles.DataBind();
                            rgDetalles.Rebind();
                        }
                        else
                        {
                            //TODO JFCV cargar el número de convenio siguiente 
                            this.TxtKeyConvenio.Text = string.Empty; 
                            rgDetalles.DataSource = dt_detalles;
                            rgDetalles.Rebind();
                        }
                        //JFCV quitar, si es necesario TODO


                        //////rgDetalles.MasterTableView.Columns[rgDetalles.MasterTableView.Columns.Count - 1].Display = false;
                        //////rgDetalles.MasterTableView.Columns[rgDetalles.MasterTableView.Columns.Count - 2].Display = false;
                        //double ancho = 0;
                        //foreach (GridColumn gc in rgDetalles.Columns)
                        //{
                        //    if (gc.Display)
                        //    {
                        //        ancho = ancho + gc.HeaderStyle.Width.Value;
                        //    }
                        //}
                        //rgDetalles.Width = Unit.Pixel(Convert.ToInt32(ancho));
                        //rgDetalles.MasterTableView.Width = Unit.Pixel(Convert.ToInt32(ancho));



                    }

                   
                }

            }
            catch (Exception ex)
            {
                this.DisplayMensajeAlerta(string.Concat(ex.Message, "Page_Load_error"));
            }
        }

        #region Eventos

        //rg convenio
        protected void rgDetalles_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            try
            {
                rgDetalles.Rebind();

            }
            catch (Exception ex)
            {

                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }


        protected void CmbCentro_SelectedIndexChanged1(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Sesion sesion = new Sesion();
                sesion = (Sesion)Session["Sesion" + Session.SessionID];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);

                    Session["dir" + Session.SessionID] = pag[pag.Length - 1]; Response.Redirect("login.aspx", false);
                }
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);


            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                switch (e.Argument.ToString())
                {
                    case "RebindGrid":
                        //rg ConvenioDet.Rebind();
                        rgDetalles.Rebind();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }
        protected void rtb1_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;

                if (Page.IsValid)
                {
                    if (btn.CommandName == "Exportahistorial")
                    {


                        ExportarHistorial();
                        
                    }
                        
                }
            }
            catch (Exception ex)
                        {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
      
        protected void Customvalidator1_ServerValidate(object source, ServerValidateEventArgs args)
                            {
            try
            {
                //args.IsValid = (RadUpload1.InvalidFiles.Count == 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
                            }

                           
        private void ExportarHistorial()
        {
            try
            {

                StringBuilder tabla = new StringBuilder();
                tabla.Append("<html><head><meta http-equiv='Content-Type' content='text/html; charset=ISO-8859-1'></head><body><table style='width:700px'>");
                EscribeDetalleConv(ref tabla);
                tabla.Append("</table></body></html>");
                //CN__Comun cn_comun = new CN__Comun();
                //cn_comun.ExportarExcel("HistorialConvenio" + this.TxtKeyConvenio.Text, tabla.ToString());
                string ruta = Server.MapPath("Reportes") + "\\HistorialConvenio.txt";
                StreamWriter sw = new StreamWriter(ruta, false, Encoding.UTF8);
                sw.WriteLine("<html xmlns='http://www.w3.org/1999/xhtml'>");
                sw.WriteLine("<head>");
                sw.WriteLine("<meta http-equiv='content-type' content='text/html; charset=UTF-8' />");
                sw.WriteLine("<title>");
                sw.WriteLine("Page-");
                sw.WriteLine(Guid.NewGuid().ToString());
                sw.WriteLine("</title>");
                sw.WriteLine("</head>");
                sw.WriteLine("<body>");
                sw.Write(tabla);
                sw.WriteLine("</body>");
                sw.WriteLine("</html>");
                sw.Close();
                if (File.Exists(ruta))
                {
                    string ruta2 = null;
                    ruta2 = Server.MapPath("Reportes") + "\\HistorialConvenio.xls";
                    if (File.Exists(ruta2))
                    {
                        File.Delete(ruta2);
                    }
                    File.Move(ruta, Server.MapPath("Reportes") + "\\HistorialConvenio.xls");
                    Response.Redirect("Reportes\\HistorialConvenio.xls", false);
                    //Response.Flush();
                    //Response.End();

                }
 

           
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
      
   private void EscribeDetalleConv(ref StringBuilder Tabla)
        {
            try
            {
                String width;

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_Convenio cn_conv = new CN_Convenio();
                Convenio conv = new Convenio();
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
                DataTable dt = null;

                conv.Filtro_TipoFiltro = -1;
                conv.Filtro_Vencido = -1;
                conv.Filtro_Id_Cat = -1;
                conv.Filtro_Valor = "-1";
                conv.Filtro_Id_Cd = -1;
                conv.Id_PC = Convert.ToInt32(this.TxtKeyConvenio.Text);
                cn_conv.ConsultaConvenioDetHis(conv, ref dt, Conexion);
                
                Tabla.Append("<tr><td><div style ='color:Blue' >KEY QUIMICA SA DE CV </div></td><td>/ Historial de convenio ");

                Tabla.Append("</td></tr>");

                Tabla.Append("<tr>");


                //lectura y escritura de columnas
                for (int i = 0; i < dt.Columns.Count; i++)
                {

                    if (dt.Columns[i].ColumnName == "Id_PC")
                    {
                        width = (i == 0) ? "100px" : "120px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Convenio Key");
                        Tabla.Append("</th>");
                    }
                    else
                        if (dt.Columns[i].ColumnName == "PC_NoConvenio")
                        {
                            width = (i == 0) ? "100px" : "120px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("Convenio proveedor");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "PC_Nombre")
                        {
                            width = (i == 0) ? "180px" : "210px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("Nombre de convenio");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "Cat_DescCorta")
                        {
                            width = (i == 0) ? "50px" : "70px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("Categoría");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "Cat_Nombre")
                        {
                            width = (i == 0) ? "120px" : "150px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("Nombre de categoría");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "Estatus")
                        {
                            width = (i == 0) ? "50px" : "70px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("Estatus");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "Id_Prd")
                        {
                            width = (i == 0) ? "50px" : "70px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("Clave Key");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "PCD_ClaveProv")
                        {
                            width = (i == 0) ? "50px" : "70px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("Clave prov.");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "Prd_Descripcion")
                        {
                            width = (i == 0) ? "350px" : "380px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("Descripción de Producto");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "PCD_PrecioVtaMin")
                        {
                            width = (i == 0) ? "70px" : "90px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("P. Venta Min.");
                            Tabla.Append("</th>");
                        }

                        else if (dt.Columns[i].ColumnName == "PCD_PrecioVtaMax")
                        {
                            width = (i == 0) ? "70px" : "90px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("P. Venta Max.");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "PCD_PrecioAAAEspA")
                        {
                            width = (i == 0) ? "110px" : "130px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("<b> Anterior / PAAA Esp.");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "PCD_FechaInicioA")
                        {
                            width = (i == 0) ? "110px" : "130px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("<b> Anterior / Fecha inicio");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "PCD_FechaFinA")
                        {
                            width = (i == 0) ? "100px" : "120px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("<b> Anterior / Fecha fin");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "PCD_PrecioAAAEsp")
                        {
                            width = (i == 0) ? "90px" : "110px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("<b> Actual / PAAA Esp. ");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "PCD_FechaInicio")
                        {
                            width = (i == 0) ? "100px" : "120px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("<b> Actual / Fecha inicio ");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "PCD_FechaFin")
                        {
                            width = (i == 0) ? "110px" : "130px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("<b> Actual / Fecha fin");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "PCD_PrecioAAAEspC")
                        {
                            width = (i == 0) ? "100px" : "120px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("<b> Futuro / PAAA Esp. ");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "PCD_FechaInicioC")
                        {
                            width = (i == 0) ? "110px" : "130px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("<b> Futuro / Fecha inicio ");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "PCD_FechaFinC")
                        {
                            width = (i == 0) ? "90px" : "110px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("<b> Futuro / Fecha fin");
                            Tabla.Append("</th>");
                        }
                        
                        else if (dt.Columns[i].ColumnName == "PCD_PrecioVentaConvenio")
                        {
                            width = (i == 0) ? "90px" : "110px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("P.Venta Convenio");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "PCD_Margen")
                        {
                            width = (i == 0) ? "90px" : "110px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("Margen ");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "Pc_FechaMod")
                        {
                            width = (i == 0) ? "90px" : "110px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("<b> Fecha de modificación");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "UsuarioMod")
                        {
                            width = (i == 0) ? "220px" : "220px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("Usuario responsable");
                            Tabla.Append("</th>");
                        }

                }
                Tabla.Append("</tr>");
                // lectura y escritura de filas
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    Tabla.Append("<tr>");
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {

                        if (dt.Columns[i].ColumnName == "Id_PC")
                        {
                            Tabla.Append("<td   style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else
                            if (dt.Columns[i].ColumnName == "PC_NoConvenio")
                            {
                                Tabla.Append("<td   style='text-align:left'>");
                                Tabla.Append(dt.Rows[j][i].ToString());
                                Tabla.Append("</td>");
                            }
                            else if (dt.Columns[i].ColumnName == "PC_Nombre")
                            {
                                Tabla.Append("<td   style='text-align:left'>");
                                Tabla.Append(dt.Rows[j][i].ToString());
                                Tabla.Append("</td>");
                            }
                            else if (dt.Columns[i].ColumnName == "Cat_DescCorta")
                            {
                                Tabla.Append("<td   style='text-align:center'>");
                                Tabla.Append(dt.Rows[j][i].ToString());
                                Tabla.Append("</td>");
                            }
                            else if (dt.Columns[i].ColumnName == "Cat_Nombre")
                            {
                                Tabla.Append("<td   style='text-align:left'>");
                                Tabla.Append(dt.Rows[j][i].ToString());
                                Tabla.Append("</td>");
                            }
                            else if (dt.Columns[i].ColumnName == "Estatus")
                            {
                                Tabla.Append("<td   style='text-align:center'>");
                                Tabla.Append(dt.Rows[j][i].ToString());
                                Tabla.Append("</td>");
                            }
                            else if (dt.Columns[i].ColumnName == "Id_Prd")
                            {
                                Tabla.Append("<td   style='text-align:right'>");
                                Tabla.Append(dt.Rows[j][i].ToString());
                                Tabla.Append("</td>");
                            }
                            else if (dt.Columns[i].ColumnName == "PCD_ClaveProv")
                            {
                                Tabla.Append("<td   style='text-align:right'>");
                                Tabla.Append(dt.Rows[j][i].ToString());
                                Tabla.Append("</td>");
                            }
                            else if (dt.Columns[i].ColumnName == "Prd_Descripcion")
                            {
                                Tabla.Append("<td   style='text-align:left'>");
                                Tabla.Append(dt.Rows[j][i].ToString());
                                Tabla.Append("</td>");
                            }

                            else if (dt.Columns[i].ColumnName == "PCD_PrecioVtaMin")
                            {
                                if (dt.Rows[j][i].ToString() != "")
                                {
                                    double valor = double.Parse(dt.Rows[j][i].ToString());
                                    Tabla.Append("<td   style='text-align:right'>");
                                    Tabla.Append(valor.ToString("C2"));
                                    Tabla.Append("</td>");
                                }
                                else
                                {
                                    Tabla.Append("<td   style='text-align:right'>");
                                    Tabla.Append(dt.Rows[j][i].ToString());
                                    Tabla.Append("</td>");
                                }
                            }
                            else if (dt.Columns[i].ColumnName == "PCD_PrecioVtaMax")
                            {
                                if (dt.Rows[j][i].ToString() != "")
                                {
                                    double valor = double.Parse(dt.Rows[j][i].ToString());
                                    Tabla.Append("<td   style='text-align:right'>");
                                    Tabla.Append(valor.ToString("C2"));
                                    Tabla.Append("</td>");
                                }
                                else
                                {
                                    Tabla.Append("<td   style='text-align:right'>");
                                    Tabla.Append(dt.Rows[j][i].ToString());
                                    Tabla.Append("</td>");
                                }
                            }


                            else if (dt.Columns[i].ColumnName == "PCD_PrecioAAAEspA")
                            {
                                if (dt.Rows[j][i].ToString() != "")
                                {
                                    double valor = double.Parse(dt.Rows[j][i].ToString());
                                    Tabla.Append("<td   style='text-align:right'>");
                                    Tabla.Append(valor.ToString("C2"));
                                    Tabla.Append("</td>");
                                }
                                else
                                {
                                    Tabla.Append("<td   style='text-align:right'>");
                                    Tabla.Append(dt.Rows[j][i].ToString());
                                    Tabla.Append("</td>");
                                }
                            }
                            else if (dt.Columns[i].ColumnName == "PCD_FechaInicioA")
                            {
                                if (dt.Rows[j][i].ToString() != "")
                                {
                                    DateTime valor = DateTime.Parse(dt.Rows[j][i].ToString());
                                    Tabla.Append("<td   style='text-align:center'>");
                                    Tabla.Append(valor.ToShortDateString());
                                    Tabla.Append("</td>");
                                }
                                else
                                {
                                    Tabla.Append("<td   style='text-align:center'>");
                                    Tabla.Append(dt.Rows[j][i].ToString());
                                    Tabla.Append("</td>");
                                }

                            }
                            else if (dt.Columns[i].ColumnName == "PCD_FechaFinA")
                            {
                                if (dt.Rows[j][i].ToString() != "")
                                {
                                    DateTime valor = DateTime.Parse(dt.Rows[j][i].ToString());
                                    Tabla.Append("<td   style='text-align:center'>");
                                    Tabla.Append(valor.ToShortDateString());
                                    Tabla.Append("</td>");
                                }
                                else
                                {
                                    Tabla.Append("<td   style='text-align:center'>");
                                    Tabla.Append(dt.Rows[j][i].ToString());
                                    Tabla.Append("</td>");
                                }

                            }
                            else if (dt.Columns[i].ColumnName == "PCD_PrecioAAAEsp")
                            {
                                if (dt.Rows[j][i].ToString() != "")
                                {
                                    double valor = double.Parse(dt.Rows[j][i].ToString());
                                    Tabla.Append("<td   style='text-align:right'>");
                                    Tabla.Append(valor.ToString("C2"));
                                    Tabla.Append("</td>");
                                }
                                else
                                {
                                    Tabla.Append("<td   style='text-align:right'>");
                                    Tabla.Append(dt.Rows[j][i].ToString());
                                    Tabla.Append("</td>");
                                }
                            }
                            else if (dt.Columns[i].ColumnName == "PCD_FechaInicio")
                            {
                                if (dt.Rows[j][i].ToString() != "")
                                {
                                    DateTime valor = DateTime.Parse(dt.Rows[j][i].ToString());
                                    Tabla.Append("<td   style='text-align:center'>");
                                    Tabla.Append(valor.ToShortDateString());
                                    Tabla.Append("</td>");
                                }
                                else
                                {
                                    Tabla.Append("<td   style='text-align:center'>");
                                    Tabla.Append(dt.Rows[j][i].ToString());
                                    Tabla.Append("</td>");
                                }

                            }
                            else if (dt.Columns[i].ColumnName == "PCD_FechaFin")
                            {
                                if (dt.Rows[j][i].ToString() != "")
                                {
                                    DateTime valor = DateTime.Parse(dt.Rows[j][i].ToString());
                                    Tabla.Append("<td   style='text-align:center'>");
                                    Tabla.Append(valor.ToShortDateString());
                                    Tabla.Append("</td>");
                                }
                                else
                                {
                                    Tabla.Append("<td   style='text-align:center'>");
                                    Tabla.Append(dt.Rows[j][i].ToString());
                                    Tabla.Append("</td>");
                                }

                            }

                            else if (dt.Columns[i].ColumnName == "PCD_PrecioAAAEspC")
                            {
                                if (dt.Rows[j][i].ToString() != "")
                                {
                                    double valor = double.Parse(dt.Rows[j][i].ToString());
                                    Tabla.Append("<td   style='text-align:right'>");
                                    Tabla.Append(valor.ToString("C2"));
                                    Tabla.Append("</td>");
                                }
                                else
                                {
                                    Tabla.Append("<td   style='text-align:right'>");
                                    Tabla.Append(dt.Rows[j][i].ToString());
                                    Tabla.Append("</td>");
                                }
                            }
                            else if (dt.Columns[i].ColumnName == "PCD_FechaInicioC")
                            {
                                if (dt.Rows[j][i].ToString() != "")
                                {
                                    DateTime valor = DateTime.Parse(dt.Rows[j][i].ToString());
                                    Tabla.Append("<td   style='text-align:center'>");
                                    Tabla.Append(valor.ToShortDateString());
                                    Tabla.Append("</td>");
                                }
                                else
                                {
                                    Tabla.Append("<td   style='text-align:center'>");
                                    Tabla.Append(dt.Rows[j][i].ToString());
                                    Tabla.Append("</td>");
                                }

                            }
                            else if (dt.Columns[i].ColumnName == "PCD_FechaFinC")
                            {
                                if (dt.Rows[j][i].ToString() != "")
                                {
                                    DateTime valor = DateTime.Parse(dt.Rows[j][i].ToString());
                                    Tabla.Append("<td   style='text-align:center'>");
                                    Tabla.Append(valor.ToShortDateString());
                                    Tabla.Append("</td>");
                                }
                                else
                                {
                                    Tabla.Append("<td   style='text-align:center'>");
                                    Tabla.Append(dt.Rows[j][i].ToString());
                                    Tabla.Append("</td>");
                                }

                            }

                           
                            else if (dt.Columns[i].ColumnName == "PCD_PrecioVentaConvenio")
                            {
                                if (dt.Rows[j][i].ToString() != "")
                                {
                                    double valor = double.Parse(dt.Rows[j][i].ToString());
                                    Tabla.Append("<td   style='text-align:right'>");
                                    Tabla.Append(valor.ToString("C2"));
                                    Tabla.Append("</td>");
                                }
                                else
                                {
                                    Tabla.Append("<td   style='text-align:right'>");
                                    Tabla.Append(dt.Rows[j][i].ToString());
                                    Tabla.Append("</td>");
                                }
                            }
                            else if (dt.Columns[i].ColumnName == "PCD_Margen")
                            {
                                if (dt.Rows[j][i].ToString() != "")
                                {
                                    double valor = double.Parse(dt.Rows[j][i].ToString());
                                    Tabla.Append("<td   style='text-align:right'>");
                                    Tabla.Append(valor.ToString("P"));
                                    Tabla.Append("</td>");
                                }
                                else
                                {
                                    Tabla.Append("<td   style='text-align:right'>");
                                    Tabla.Append(dt.Rows[j][i].ToString());
                                    Tabla.Append("</td>");
                                }
                            }
                            else if (dt.Columns[i].ColumnName == "Pc_FechaMod")
                            {
                                if (dt.Rows[j][i].ToString() != "")
                                {
                                    DateTime valor = DateTime.Parse(dt.Rows[j][i].ToString());
                                    Tabla.Append("<td   style='text-align:center'>");
                                    Tabla.Append(valor.ToShortDateString());
                                    Tabla.Append("</td>");
                                }
                                else
                                {
                                    Tabla.Append("<td   style='text-align:center'>");
                                    Tabla.Append(dt.Rows[j][i].ToString());
                                    Tabla.Append("</td>");
                                }

                            }
                            else if (dt.Columns[i].ColumnName == "UsuarioMod")
                            {
                                Tabla.Append("<td   style='text-align:left'>");
                                Tabla.Append(dt.Rows[j][i].ToString());
                                Tabla.Append("</td>");
                            }




                    }
                }
                Tabla.Append("</tr>");
                Tabla.Append("<tr>");
                Tabla.Append("<td>");
                Tabla.Append("&nbsp; &nbsp;</td>");
                Tabla.Append("</tr>");



            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

      
     
       
        #endregion Eventos

        #region Funciones
        private void ValidarPermisos()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Pagina pagina = new Pagina();
                pagina.Url = "CapGestionPreciosH.aspx";

                CN_Pagina CapaNegocio = new CN_Pagina();
                CapaNegocio.PaginaConsultar(ref pagina, Sesion.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;
                this.Title = pagina.Descripcion;

                Permiso Permiso = new Permiso();
                Permiso.Id_U = Sesion.Id_U;
                Permiso.Id_Cd = Sesion.Id_Cd;
                Permiso.Sm_cve = pagina.Clave;

                CapaDatos.CD_PermisosU CN_PermisosU = new CapaDatos.CD_PermisosU();
                CN_PermisosU.ValidaPermisosUsuario(ref Permiso, Sesion.Emp_Cnx);

                _PermisoGuardar = Permiso.PGrabar;
                _PermisoModificar = Permiso.PModificar;


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
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, sesion.Id_Cd_Ver, sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = Sesion.Id_Cd_Ver.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
      
       
        private string DescripcionProducto(int Id_Prd)
        {
            try
            {
                string Prd_Descripcion = string.Empty;

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatProducto cn_prd = new CN_CatProducto();
                cn_prd.ConsultaProducto_Descripcion(Id_Prd, ref Prd_Descripcion, sesion.Emp_Cnx);


                return Prd_Descripcion;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private string ProductoClaveProveedor(int Id_Prd)
        {
            try
            {
                string Prd_ClaveProveedor = string.Empty;

                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatProducto cn_prd = new CN_CatProducto();
                cn_prd.ConsultaProducto_ClaveProveedor(Id_Prd, ref Prd_ClaveProveedor, sesion.Emp_Cnx);



                return Prd_ClaveProveedor;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private List<ConvenioHis> ConsultaConvenioHis()
        {
            try
            {
                CN_Convenio cn_conv = new CN_Convenio();
                ConvenioHis conv = new ConvenioHis();
                List<ConvenioHis> List = new List<ConvenioHis>();
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                int Id_PC = int.Parse(HFId_PC.Value);
                cn_conv.ConsultaConvenioHis(Id_PC, ref conv, ref List, Conexion);
               
                //cn_conv.ConsultaConvenioDet(Id_PC, ref List, Conexion);
                return ListDet = List;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
       
         

        #endregion Funciones

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
        private void Alerta2(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', 600, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
        private void AlertaCerrar(string mensaje)
        {
            try
            {
                mensaje = mensaje.Replace(Convert.ToChar(10).ToString(), string.Empty);
                mensaje = mensaje.Replace(Convert.ToChar(13).ToString(), string.Empty);
                RAM1.ResponseScripts.Add("CloseWindowA('" + mensaje + "');");
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
        private void DisplayMensajeAlerta(string mensaje)
        {
            if (mensaje.Contains("Page_Load_error"))
                Alerta("Error al cargar la página");
            else
                if (mensaje.Contains("Impresion_error"))
                    Alerta("Error al momento de imprimir");
                else
                    Alerta(string.Concat("No se pudo realizar la operación solicitada.<br/>", mensaje.Replace("'", "\"")));
        }
        #endregion



          
        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ErrorManager();
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    List<ConvenioDet> List = new List<ConvenioDet>();
                    if (this.HFId_PC.Value != "0")
                    {
                        this.rgDetalles.DataSource = ConsultaConvenioHis();
                    }
                    else
                    {
                        //rgDetalles.DataSource = dt_detalles;
                        rgDetalles.DataSource = ListDet;

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }


        }

        protected void rgDetalles_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            ErrorManager();
            try
            {
                 
                if (e.CommandName == "Edit")
                {
                    Id_ConvDet_A = 1; // int.Parse(rgDetalles.MasterTableView.Items[e.Item.ItemIndex]["Id_ConvDet"].Text);
                    Id_Prd_A = int.Parse((rgDetalles.MasterTableView.Items[e.Item.ItemIndex]["Id_Prd"].FindControl("ProdLabel") as Label).Text);
                 
                            
                }

                switch (e.CommandName)
                {
                    case "Cancelar":
                       
                        break;
                    case "Imprimir":
                        int Id_PC = int.Parse(this.rgDetalles.MasterTableView.Items[e.Item.ItemIndex]["Id_PC"].Text);
                        int Id_PCH = int.Parse(this.rgDetalles.MasterTableView.Items[e.Item.ItemIndex]["Id_PCH"].Text);

                        Imprimir(Id_PC, Id_PCH);
 
                        break;
                }

                 


            }

            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }


        }

        private void Imprimir(int Id_PC, int Id_PCH)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                ArrayList ALValorParametrosInternos = new ArrayList();
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];

                //ALValorParametrosInternos.Add(sesion.Emp_Cnx);
                 ALValorParametrosInternos.Add(Conexion);
                ALValorParametrosInternos.Add(Id_PC);
                ALValorParametrosInternos.Add(Id_PCH);
               

                Type instance = null;

                instance = typeof(LibreriaReportes.RepPrecioConvenioHis);


                //Session["InternParameter_Values" + Session.SessionID + HF_Cve.Value] = ALValorParametrosInternos;
                //Session["assembly" + Session.SessionID + HF_Cve.Value] = instance.AssemblyQualifiedName;
                //RAM1.ResponseScripts.Add("AbrirReporte('" + HF_Cve.Value + "');");

                Session["InternParameter_Values" + Session.SessionID] = ALValorParametrosInternos;
                Session["assembly" + Session.SessionID] = instance.AssemblyQualifiedName;
                RAM1.ResponseScripts.Add("AbrirReportePadre()");


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void rgDetalles_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            ErrorManager();
            if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
            {
                GridEditableItem editItem = (GridEditableItem)e.Item;

                //RadComboBox RadComboBoxTerr = editItem.FindControl("RadComboBoxTerr") as RadComboBox;
                //Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                //new CN__Comun().LlenaCombo(sesion.Id_Emp, sesion.Id_Cd_Ver, -1, sesion.Emp_Cnx, "spCatTerritorioCte_Combo", ref RadComboBoxTerr);
                //Label TerrIdDesc = (e.Item.FindControl("TerrIdDesc") as Label);




                //RadComboBox RadComboBoxTipoSalida = e.Item.FindControl("RadComboBoxTipoSalida") as RadComboBox;
                //new CN__Comun().LlenaCombo(sesion.Id_Emp, sesion.Id_Cd_Ver, -1, sesion.Emp_Cnx, "sp_CatRemisionesTipoSalida", ref RadComboBoxTipoSalida);



                //RadComboBox RadComboBoxConceptoTipoSalida = e.Item.FindControl("RadComboBoxConceptoTipoSalida") as RadComboBox;
                //new CN__Comun().LlenaCombo(sesion.Id_Emp, sesion.Id_Cd_Ver, -1, sesion.Emp_Cnx, "sp_CatRemisionesConceptoTipoSalida", ref RadComboBoxConceptoTipoSalida);

                Control insertbtn = (Control)editItem.FindControl("PerformInsertButton");
                if (insertbtn != null)
                { //se habilitan todos los controles
                    //RadComboBoxTerr.Enabled = true;
                    (e.Item.FindControl("RadNumericTextBox1") as RadNumericTextBox).Enabled = true;
                    //(editItem["importe"].Controls[0] as TextBox).Visible = false;
                }

                Control updatebtn = (Control)editItem.FindControl("UpdateButton");
                if (updatebtn != null)
                {
                    //se llenan y deshabilita cmb territorio y txtterritorio, cmb producto y txt producto.
                    //comboterritorio
                    //(editItem.FindControl("RadComboBoxTerr") as RadComboBox).SelectedValue = editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Terr"].ToString();//
                    //(editItem.FindControl("txtter1") as RadNumericTextBox).Text = editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Terr"].ToString();//txtterr
                    //(editItem.FindControl("RadComboBoxTerr") as RadComboBox).Enabled = false;
                    //(editItem.FindControl("txtter1") as RadNumericTextBox).Enabled = true;
                    //producto
                    (editItem.FindControl("RadNumericTextBox1") as RadNumericTextBox).Text = editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Id_Prd"].ToString();//txtproducto
                    (e.Item.FindControl("RadNumericTextBox1") as RadNumericTextBox).Enabled = false;//txtbox id del producto                    
                    //editItem["importe"].Controls[1].Visible = false;
                    Id_ConvDet_A = Convert.ToInt32(editItem.OwnerTableView.DataKeyValues[editItem.ItemIndex]["Id_ConvDet"]);
                }

            }

            //TODO: AGREGAR PARA PONER EL FOCUS
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem form = (GridEditableItem)e.Item;
                RadNumericTextBox dataField = (RadNumericTextBox)form["PCD_PrecioVtaMin"].FindControl("RadNumericTextBoxPrecioVtaMin");
                if (!dataField.Enabled)
                {
                    dataField = (RadNumericTextBox)form["PCD_PrecioVtaMin"].FindControl("RadNumericTextBoxPrecioVtaMin");
                }

                dataField.Focus();
            }
            //-----------------------------------------
        }

        protected void SelTodo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                for (int x = 0; x < rgDetalles.Items.Count; x++)
                    (rgDetalles.Items[x].FindControl("chkSeleccionar") as System.Web.UI.WebControls.CheckBox).Checked = (sender as System.Web.UI.WebControls.CheckBox).Checked && (rgDetalles.Items[x].FindControl("chkSeleccionar") as System.Web.UI.WebControls.CheckBox).Visible;
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void Sel_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ErrorManager();
                System.Web.UI.WebControls.CheckBox chk = sender as System.Web.UI.WebControls.CheckBox;
                if ((chk.Parent.Parent as GridDataItem).Cells[10].Text == "N")
                {
                    Alerta("El convenio no tiene historial");
                    chk.Checked = false;
                    return;
                }
           
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
      
        private void crearDT()
        {
            dt_detalles = new DataTable();
            dt_detalles.Columns.Add("Id_PCH");
            dt_detalles.Columns.Add("Id_PC");

            dt_detalles.Columns.Add("PC_NoConvenio");
            dt_detalles.Columns.Add("PC_Nombre");
            dt_detalles.Columns.Add("Cat_DescCorta");

            dt_detalles.Columns.Add("PC_Nombreusuario");
            dt_detalles.Columns.Add("PC_FechaMod", typeof(DateTime));
             

        }

        private void GenerarRemisiones()
        {
            try
            {
                for (int x = 0; x < rgDetalles.Items.Count; x++)
                {
                   // if ((rgDetalles.Items[x].FindControl("chkSeleccionar") as System.Web.UI.WebControls.CheckBox).Checked)
                        //GuardarRemision(Convert.ToInt32(rgDetalles..Items[x]["Id_Ped"].Text));
                }
                rgDetalles.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
       
    }
}