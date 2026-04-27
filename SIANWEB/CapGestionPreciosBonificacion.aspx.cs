using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaEntidad;
using Telerik.Web.UI;
using CapaNegocios;
using System.Text;

namespace SIANWEB
{
    public partial class CapGestionPreciosBonificacion : System.Web.UI.Page
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
                        CargarCategoria();

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                string cmd = e.Argument.ToString();
                switch (cmd)
                {
                    case "RebindGrid":

                        break;
                    case "Cancelarcte":

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

                if (btn.CommandName == "print")
                {
                    Imprimir();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private void CargarCategoria()
        {
            try
            {
                CN__Comun cn_comun = new CN__Comun();
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];

                cn_comun.LlenaCombo(Conexion, "spCatConvCategoria_Combo", ref CmbCategoria);

            }
            catch (Exception ex)
            {

                throw ex;
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

                    if (Permiso.PGrabar == false)
                        this.rtb1.Items[1].Visible = false;
                }
                else
                    Response.Redirect("Inicio.aspx");


                //if (ConsultarAutorizacionPrecio() == "True")
                //{

                //    this.rg1.Columns[11].Visible = true ;
                //}

                //else
                //{
                //    this.rg1.Columns[11].Visible = false;

                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private DataTable GetList()
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                List<ConvenioBonificacion> List = new List<ConvenioBonificacion>();
                CN_Convenio cn_conv = new CN_Convenio();
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
                DataTable dt = null;
                DateTime FechaIni;
                DateTime FechaFin;
                FechaIni = DateTime.Parse(this.dpFecha1.SelectedDate.ToString());
                FechaFin = DateTime.Parse(this.dpFecha2.SelectedDate.ToString());

                int sañoi = FechaIni.Year;
                int smesi = FechaIni.Month;
                int sdiai = FechaIni.Day;
                int sañof = FechaFin.Year;
                int smesf = FechaFin.Month;
                int sdiaf = FechaFin.Day;
                string fecha = "";
                fecha = sañoi.ToString() + "-" + smesi.ToString() + "-" + sdiai.ToString() + " 00:00:00.000";
                FechaIni = Convert.ToDateTime(fecha);
                fecha = sañof.ToString() + "-" + smesf.ToString() + "-" + sdiaf.ToString() + " 23:59:59.000";
                FechaFin = Convert.ToDateTime(fecha);


                //fecha ini y fin 
                //categoria
                //version
                //if (this.RblTipoRep.SelectedValue == "1")  //1 actual 2  cierre
                //Idinicial idcd final
                ConvenioBonificacion conveniob = new ConvenioBonificacion();
                conveniob.Filtro_FechaInicial = FechaIni;
                conveniob.Filtro_FechaFinal = FechaFin;
                conveniob.Filtro_Id_Cat = Convert.ToInt32(this.CmbCategoria.SelectedValue);
                conveniob.Filtro_Id_Cdinicial = sesion.Id_Cd_Ver;
                conveniob.Filtro_Id_Cdfinal = sesion.Id_Cd_Ver;
                conveniob.Filtro_Version = 1;  // Convert.ToInt32(this.RblTipoRep.SelectedValue);

                cn_conv.ConsultaConvenioBonificacion(conveniob, ref dt, Conexion);


                return dt;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        private void Imprimir()
        {
            try
            {

                StringBuilder tabla = new StringBuilder();
                tabla.Append("<html><head><meta http-equiv='Content-Type' content='text/html; charset=ISO-8859-1'></head><body><table style='width:700px'>");
                EscribeEncabezado(ref tabla);
                EscribeDetalle(ref tabla);
                tabla.Append("</table></body></html>");
                CN__Comun cn_comun = new CN__Comun();
                cn_comun.ExportarExcel("ReporteBonificacion", tabla.ToString());

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void EscribeEncabezado(ref StringBuilder Tabla)
        {
            try
            {


                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                Tabla.Append("<tr>");
                //Tabla.Append("<td width='5px'><img src='http://" + HttpContext.Current.Request.Url.Authority + "/Imagenes/LogoNuevo.jpg'></td>");
                Tabla.Append("<td  colspan='6' style='width:400px; text-align:left; font-weight:bold'>Reporte de bonificaciones por Convenios de PAAA Especiales </td>");
                Tabla.Append("<td colspan ='17' style='width:400px; text-align:right; font-weight:bold'> Fecha impresión:  " + DateTime.Now.ToString() + " </td>");
                Tabla.Append("</tr>");
                Tabla.Append("<tr>");
                Tabla.Append("<td style='text-align:left;font-weight:bold'  colspan='1'> Mes y año inicial: </td> <td style='text-align:left;font-weight:bold'> " + Convert.ToDateTime(this.dpFecha1.SelectedDate).ToString("yy-MMM-dd") + "</td  <td colspan ='24' style='width:400px; text-align:right; font-weight:bold'> Usuario: " + sesion.U_Nombre + " </td>");
                Tabla.Append("</tr>");
                Tabla.Append("<tr>");
                Tabla.Append("<td style='text-align:left;font-weight:bold'  colspan='1'> Mes y año final: </td> <td style='text-align:left;font-weight:bold'> " + Convert.ToDateTime(this.dpFecha2.SelectedDate).ToString("yy-MMM-dd") + "</td>");
                Tabla.Append("</tr>");
                Tabla.Append("<tr>");
                if (this.CmbCategoria.SelectedIndex == 0)
                {
                    Tabla.Append("<td style='text-align:left;font-weight:bold'  colspan='1'> Categoría:   </td> <td style='text-align:left;font-weight:bold'> Todas </td>");
                }
                else
                {
                    Tabla.Append("<td style='text-align:left;font-weight:bold'  colspan='1'> Categoría:   </td> <td style='text-align:left;font-weight:bold'> " + this.CmbCategoria.Text + " </td>");
                }
                Tabla.Append("</tr>");
                Tabla.Append("<tr>");
                Tabla.Append("<td style='text-align:left;font-weight:bold'  colspan='1'> Cdi :   </td> <td style='text-align:left;font-weight:bold' colspan='2' > " + sesion.Id_Cd_Ver.ToString() + " - " + sesion.Cd_Nombre + " </td>");
                Tabla.Append("</tr>");
                 Tabla.Append("<td style='text-align:left;font-weight:bold'  colspan='1'> Formato:   </td> <td style='text-align:left;font-weight:bold'> Versión Key </td>");
                Tabla.Append("</tr>");
                Tabla.Append("<tr>");
                Tabla.Append("<td style='text-align:left;font-weight:bold'  colspan='1'> &nbsp;  </td> ");
                Tabla.Append("</tr>");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void EscribeDetalle(ref StringBuilder Tabla)
        {
            try
            {
                String width;

                System.Data.DataTable dt = new System.Data.DataTable();
                List<ConvenioBonificacion> List = new List<ConvenioBonificacion>();

                dt = GetList();



                Tabla.Append("<tr>");

                //lectura y escritura de columnas
                for (int i = 0; i < dt.Columns.Count; i++)
                {

                    if (dt.Columns[i].ColumnName == "fac_fecha")
                    {
                        width = (i == 0) ? "150px" : "150px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Fecha factura");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Id_Cd")
                    {
                        width = (i == 0) ? "120px" : "120px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("No. Concesionario /Zona");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "RazonSocial_Consecionario")
                    {
                        width = (i == 0) ? "220px" : "230px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Razón Social del Concesionario");
                        Tabla.Append("</th>");
                    }

                    else if (dt.Columns[i].ColumnName == "Nombrecdi")
                    {
                        width = (i == 0) ? "180px" : "180px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Nombre del Concesionario");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "id_documento")
                    {
                        width = (i == 0) ? "120px" : "120px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("No. Factura");
                        Tabla.Append("</th>");
                    }

                    else if (dt.Columns[i].ColumnName == "Id_Cte")
                    {
                        width = (i == 0) ? "60px" : "60px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("No. Cliente");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Cte_NomComercial")
                    {
                        width = (i == 0) ? "300px" : "300px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Razón social del cliente");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "codigoproveedor")
                    {
                        width = (i == 0) ? "140px" : "140px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Código proveedor");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Id_Prd")
                    {
                        width = (i == 0) ? "100px" : "100px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Código Key");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "prd_descripcion")
                    {
                        width = (i == 0) ? "300px" : "300px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Descripción de producto");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "Cantidad")
                    {
                        width = (i == 0) ? "100px" : "120px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Unidades facturadas");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "PrecioVta")
                    {
                        width = (i == 0) ? "100px" : "100px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Precio de venta");
                        Tabla.Append("</th>");
                    }

                    if (Convert.ToInt32(this.RblTipoRep.SelectedValue) == 1)
                    {

                        if (dt.Columns[i].ColumnName == "PAAANormal")
                        {
                            width = (i == 0) ? "100px" : "100px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("PAAA Normal");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "PCD_PrecioAAAEsp")
                        {
                            width = (i == 0) ? "100px" : "100px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("PAAA Especial");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "Bonificacion")
                        {
                            width = (i == 0) ? "100px" : "100px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("Bonificación");
                            Tabla.Append("</th>");
                        }
                    }
                    else
                    {
                        if (dt.Columns[i].ColumnName == "CostoCompra")
                        {
                            width = (i == 0) ? "100px" : "100px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("Costo de compra");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "PAAAEspProv")
                        {
                            width = (i == 0) ? "150px" : "150px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("Costo especial");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "Solncred")
                        {
                            width = (i == 0) ? "100px" : "100px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("Solicitud de NdCr");
                            Tabla.Append("</th>");
                        }


                    }
                    if (dt.Columns[i].ColumnName == "pcd_margen")
                    {
                        width = (i == 0) ? "100px" : "100px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Margen");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "id_pc")
                    {
                        width = (i == 0) ? "130px" : "130px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Convenio Key");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "pc_noconvenio")
                    {
                        width = (i == 0) ? "150px" : "150px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Convenio proveedor");
                        Tabla.Append("</th>");
                    }
                    else if (dt.Columns[i].ColumnName == "pc_Nombre")
                    {
                        width = (i == 0) ? "180px" : "180px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Nombre de convenio");
                        Tabla.Append("</th>");
                    }


                    if (Convert.ToInt32(this.RblTipoRep.SelectedValue) == 1)
                    {
                        if (dt.Columns[i].ColumnName == "PCD_PrecioVtaMin")
                        {
                            width = (i == 0) ? "100px" : "100px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("P.Venta Min");
                            Tabla.Append("</th>");
                        }
                        if (dt.Columns[i].ColumnName == "PCD_PrecioVtaMax")
                        {
                            width = (i == 0) ? "100px" : "100px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("P.Venta Max");
                            Tabla.Append("</th>");
                        }
                        else if (dt.Columns[i].ColumnName == "origenvinculacion")
                        {
                            width = (i == 0) ? "220px" : "250px";
                            Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                            Tabla.Append("Origen de vinculación");
                            Tabla.Append("</th>");
                        }
                    }
                    if (dt.Columns[i].ColumnName == "Categoria")
                    {
                        width = (i == 0) ? "180px" : "180px";
                        Tabla.Append("<th  align = 'Center' style='border-style: solid none solid none; width:" + width + "'>");
                        Tabla.Append("Categoría");
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

                        if (dt.Columns[i].ColumnName == "fac_fecha")
                        {
                            Tabla.Append("<td   style='text-align:center'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Id_Cd")
                        {
                            Tabla.Append("<td   style='text-align:right'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        //else if (dt.Columns[i].ColumnName == "id_concesionario")
                        //{
                        //    Tabla.Append("<td   style='text-align:right'>");
                        //    Tabla.Append(dt.Rows[j][i].ToString());
                        //    Tabla.Append("</td>");
                        //}
                        else if (dt.Columns[i].ColumnName == "RazonSocial_Consecionario")
                        {
                            Tabla.Append("<td   style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Nombrecdi")
                        {
                            Tabla.Append("<td   style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "id_documento")
                        {
                            Tabla.Append("<td   style='text-align:right'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Id_Cte")
                        {
                            Tabla.Append("<td   style='text-align:right'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }

                        else if (dt.Columns[i].ColumnName == "Cte_NomComercial")
                        {
                            Tabla.Append("<td   style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }

                        else if (dt.Columns[i].ColumnName == "codigoproveedor")
                        {
                            Tabla.Append("<td   style='text-align:right'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "Id_Prd")
                        {
                            Tabla.Append("<td   style='text-align:right'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");
                        }
                        else if (dt.Columns[i].ColumnName == "prd_descripcion")
                        {
                            Tabla.Append("<td   style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");

                        }
                        else if (dt.Columns[i].ColumnName == "Cantidad")
                        {
                            Tabla.Append("<td   style='text-align:right'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");

                        }
                        else if (dt.Columns[i].ColumnName == "PrecioVta")
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

                        else if (dt.Columns[i].ColumnName == "PAAANormal")
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


                        if (Convert.ToInt32(this.RblTipoRep.SelectedValue) == 1)
                        {
                            if (dt.Columns[i].ColumnName == "Bonificacion")
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

                        }
                        else
                        {

                            if (dt.Columns[i].ColumnName == "CostoCompra")
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
                            else if (dt.Columns[i].ColumnName == "PAAAEspProv")
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
                            else if (dt.Columns[i].ColumnName == "Solncred")
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

                        }

                        if (dt.Columns[i].ColumnName == "pcd_margen")
                        {
                            if (dt.Rows[j][i].ToString() != "")
                            {
                                double valor = double.Parse(dt.Rows[j][i].ToString());
                                Tabla.Append("<td   style='text-align:right'>");
                                Tabla.Append(valor.ToString("P2"));
                                Tabla.Append("</td>");
                            }
                            else
                            {
                                Tabla.Append("<td   style='text-align:right'>");
                                Tabla.Append(dt.Rows[j][i].ToString());
                                Tabla.Append("</td>");
                            }

                        }
                        else if (dt.Columns[i].ColumnName == "id_pc")
                        {
                            Tabla.Append("<td   style='text-align:right'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");

                        }
                        else if (dt.Columns[i].ColumnName == "pc_noconvenio")
                        {
                            Tabla.Append("<td   style='text-align:right'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");

                        }
                        else if (dt.Columns[i].ColumnName == "pc_Nombre")
                        {
                            Tabla.Append("<td   style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");

                        }

                        if (Convert.ToInt32(this.RblTipoRep.SelectedValue) == 1)
                        {
                            if (dt.Columns[i].ColumnName == "PCD_PrecioVtaMin")
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
                            else if (dt.Columns[i].ColumnName == "origenvinculacion")
                            {
                                Tabla.Append("<td   style='text-align:right'>");
                                Tabla.Append(dt.Rows[j][i].ToString());
                                Tabla.Append("</td>");

                            }
                        }
                        if (dt.Columns[i].ColumnName == "Categoria")
                        {
                            Tabla.Append("<td   style='text-align:left'>");
                            Tabla.Append(dt.Rows[j][i].ToString());
                            Tabla.Append("</td>");

                        }


                        //else
                        //{
                        //    //Domingos
                        //    if (Convert.ToInt32(dt.Rows[j][i].ToString()) == -99)
                        //    {
                        //        Tabla.Append("<td bgcolor='#0066FF' style='text-align:center'>");
                        //        Tabla.Append("0");
                        //        Tabla.Append("</td>");

                        //    }
                        //    //Incumplimiento
                        //    else if (Convert.ToInt32(dt.Rows[j][i].ToString()) == -98)
                        //    {
                        //        Tabla.Append("<td bgcolor='Yellow' style='text-align:center'>");
                        //        Tabla.Append("0");
                        //        Tabla.Append("</td>");
                        //    }
                        //    //Asueto
                        //    else if (Convert.ToInt32(dt.Rows[j][i].ToString()) == -1)
                        //    {
                        //        Tabla.Append("<td bgcolor='Red' style='text-align:center'>");
                        //        Tabla.Append("0");
                        //        Tabla.Append("</td>");
                        //    }
                        //    //Curso
                        //    else if (Convert.ToInt32(dt.Rows[j][i].ToString()) == -2)
                        //    {
                        //        Tabla.Append("<td bgcolor='Orange' style='text-align:center'>");
                        //        Tabla.Append("0");
                        //        Tabla.Append("</td>");
                        //    }
                        //    //Incapacidad
                        //    else if (Convert.ToInt32(dt.Rows[j][i].ToString()) == -3)
                        //    {
                        //        Tabla.Append("<td bgcolor='Green' style='text-align:center'>");
                        //        Tabla.Append("0");
                        //        Tabla.Append("</td>");

                        //    }
                        //    //Permiso
                        //    else if (Convert.ToInt32(dt.Rows[j][i].ToString()) == -4)
                        //    {
                        //        Tabla.Append("<td bgcolor='silver' style='text-align:center'>");
                        //        Tabla.Append("0");
                        //        Tabla.Append("</td>");
                        //    }


                        //    else
                        //    {
                        //        Tabla.Append("<td   style='text-align:center'>");
                        //        Tabla.Append(dt.Rows[j][i].ToString());
                        //        Tabla.Append("</td>");

                        //    }

                        //}


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
        #endregion
    }
}