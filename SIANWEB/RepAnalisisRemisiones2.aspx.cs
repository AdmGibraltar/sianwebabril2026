using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocios;
using CapaEntidad;
using ClosedXML.Excel;
using System.IO;

namespace SIANWEB
{
    public partial class RepAnalisisRemisiones2 : System.Web.UI.Page
    {
        #region Variables
        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoEliminar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoEliminar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private bool _PermisoImprimir { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoImprimir" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        #endregion

        #region Mensajes 
        private void ShowMessage(string Message, string type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }
        private void sucess(string mensaje)
        {
            ShowMessage(mensaje, "Success");
        }
        private void danger(string mensaje)
        {
            ShowMessage(mensaje, "Error");
        }
        private void warning(string mensaje)
        {
            ShowMessage(mensaje, "Warning");
        }
        private void info(string mensaje)
        {
            ShowMessage(mensaje, "Info");
        }
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
                        CargarCentros();
                        dpFechaini.Value = Sesion.CalendarioIni;
                        dpFechafin.Value = Sesion.CalendarioFin;
                        Random randObj = new Random(DateTime.Now.Millisecond);
                        HF_ClvPag.Value = randObj.Next().ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                warning(ex.Message.ToString() + "Page_Load");
            }
        }

        protected void CmbCentro_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            Sesion.Id_Cd_Ver = Convert.ToInt32(CmbCentro.SelectedItem.Value);
        }

        protected void rblRemisiones_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (rblRemisiones.SelectedValue == "1")
            //{
            //    rblDetalle.Enabled = true;
            //}
            //else
            //{
            //    rblDetalle.Enabled = false;
            //}
        }

        protected void RblTipoRep_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {//TODO que ponga la fecha o el mes 
                if (this.RblTipoRep.Value.ToString() == "1")
                {

                }
                else
                {

                }

            }
            catch (Exception ex)
            {
                warning(ex.Message.ToString() + " " + new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }


        #endregion
        #region Metodos
        private void CargarCentros()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();

                if (Sesion.U_MultiOfi == false)
                {
                    CN_Comun.DevLlenaCombo(2, Sesion.Id_Emp, Sesion.Id_U, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.Value = Sesion.Id_Cd_Ver.ToString();
                    this.CmbCentro.Enabled = false;
                }
                else
                {
                    CN_Comun.DevLlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, Sesion.Id_Cd_Ver, Sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.Value = Sesion.Id_Cd_Ver.ToString();
                    this.CmbCentro.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ValidarPermisos()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];

                Pagina pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                if (pag.Length > 1)
                {
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name + "?" + pag[1];
                }
                else
                {
                    pagina.Url = (new System.IO.FileInfo(Page.Request.Url.AbsolutePath)).Name;
                }
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
                {
                    Response.Redirect("Inicio.aspx");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void BtnExcel_ServerClick(object sender, EventArgs e)
        {
            ImprimirExcel();
        }


        private void ImprimirExcel()
        {
            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];

            int año = DateTime.Now.AddYears(-2).Year;

            string fecha = "01-01-" + año;

            using (var workbook = new XLWorkbook())
            {
                CN_ReporteRemisiones CN = new CN_ReporteRemisiones();

                int count = 13;
                int idMatriz = -1;
                int id_cte = -1;
                double totalCte = 0;
                double TotalMAtriz = 0;
                double TotalGral = 0;

                string Emp_Nombre = "";
                string Cd_Nombre = "";
                string U_Nombre = "";
                new CN_CapPedido().ConsultarEncabezado_RepFacPedidosPendientes(Sesion, ref Emp_Nombre, ref Cd_Nombre, ref U_Nombre);

                DateTime fechainicial = new DateTime();
                DateTime fechafinal = new DateTime();

                fechainicial = (DateTime)dpFechaini.Value;
                fechafinal = (DateTime)dpFechafin.Value;

                fechafinal = DateTime.Now;

                if (RblTipoRep.Value.ToString() == "1")  //1 actual 2  cierre
                {

                    fechainicial = fechainicial.AddYears(-2);
                    int saño = fechainicial.Year;
                    fecha = "";
                    fecha = saño.ToString() + "-01-01 00:00:00.000";
                    fechainicial = Convert.ToDateTime(fecha);
                }
                else
                {
                    fechafinal = fechainicial.AddDays(-1);

                    fechainicial = fechainicial.AddYears(-2);
                    int saño = fechainicial.Year;
                    fecha = "";
                    fecha = saño.ToString() + "-01-01 00:00:00.000";
                    fechainicial = Convert.ToDateTime(fecha);

                }

                //Reporte mov 80 Factura
                #region Mov80Factura
                var Excel80Factura = workbook.Worksheets.Add("Mov 80-Facturado");

                #region Encabezado
                ReporteRemisiones Datos80Factura = new ReporteRemisiones();
                List<ReporteRemisiones> lista80Factura = new List<ReporteRemisiones>();
                Datos80Factura.Id_Emp = Sesion.Id_Emp;
                Datos80Factura.Id_Cd = Sesion.Id_Cd_Ver;
                Datos80Factura.Vencido = 0;
                Datos80Factura.FechaIni = fechainicial;
                Datos80Factura.FechaFin = fechafinal;
                Datos80Factura.TipoRemision = 80;
                CN.Rep_RemisionesFacturas(Datos80Factura, ref lista80Factura, Sesion.Emp_Cnx);

                Excel80Factura.Cell(1, 1).Value = "Sucursal";
                Excel80Factura.Cell(1, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                Excel80Factura.Cell(1, 2).Value = Sesion.Id_Cd_Ver + " - " + Sesion.Cd_Nombre;
                Excel80Factura.Cell(1, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                Excel80Factura.Cell(1, 4).Value = Emp_Nombre;
                Excel80Factura.Cell(1, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                Excel80Factura.Cell(2, 1).Value = "Usuario";
                Excel80Factura.Cell(2, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                Excel80Factura.Cell(2, 2).Value = Sesion.U_Nombre;
                Excel80Factura.Cell(2, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                Excel80Factura.Cell(2, 4).Value = "Control de Remisiones";
                Excel80Factura.Cell(2, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                Excel80Factura.Cell(3, 1).Value = "Fecha del Documento";
                Excel80Factura.Cell(3, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                Excel80Factura.Cell(3, 2).Value = DateTime.Now.ToString("dd/MM/yyy");
                Excel80Factura.Cell(3, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                Excel80Factura.Cell(4, 1).Value = "Fecha Inical";
                Excel80Factura.Cell(4, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                Excel80Factura.Cell(4, 2).Value = fechainicial.ToString("dd/MM/yyy"); ;
                Excel80Factura.Cell(4, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                Excel80Factura.Cell(5, 1).Value = "Fecha Final";
                Excel80Factura.Cell(5, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                Excel80Factura.Cell(5, 2).Value = fechafinal.ToString("dd/MM/yyy");
                Excel80Factura.Cell(5, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                Excel80Factura.Cell(6, 1).Value = "Cliente";
                Excel80Factura.Cell(6, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                Excel80Factura.Cell(6, 2).Value = "Todos";
                Excel80Factura.Cell(6, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                Excel80Factura.Cell(7, 1).Value = "Territorio";
                Excel80Factura.Cell(7, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                Excel80Factura.Cell(7, 2).Value = "Todos";
                Excel80Factura.Cell(7, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                Excel80Factura.Cell(8, 1).Value = "Vencidos";
                Excel80Factura.Cell(8, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                Excel80Factura.Cell(8, 2).Value = "No";
                Excel80Factura.Cell(8, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                Excel80Factura.Cell(9, 1).Value = "Nivel";
                Excel80Factura.Cell(9, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                Excel80Factura.Cell(9, 2).Value = "Detalle";
                Excel80Factura.Cell(9, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                Excel80Factura.Cell(10, 1).Value = "Tipo de Remisión";
                Excel80Factura.Cell(10, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                Excel80Factura.Range(10, 2, 10, 4).Merge();
                Excel80Factura.Range(10, 2, 10, 4).Value = "80 Transferencia de Cuentas Nacionales";
                Excel80Factura.Range(10, 2, 10, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                #endregion

                #region Cuerpo

                Excel80Factura.Cell(12, 1).Value = "Remisión";
                Excel80Factura.Cell(12, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                Excel80Factura.Cell(12, 2).Value = "Factura";
                Excel80Factura.Cell(12, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                Excel80Factura.Cell(12, 3).Value = "Fecha";
                Excel80Factura.Cell(12, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                Excel80Factura.Cell(12, 4).Value = "Núm de Producto";
                Excel80Factura.Cell(12, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                Excel80Factura.Cell(12, 5).Value = "Descripción de Producto";
                Excel80Factura.Cell(12, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                Excel80Factura.Cell(12, 6).Value = "Unidades";
                Excel80Factura.Cell(12, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                Excel80Factura.Cell(12, 7).Value = "Importe";
                Excel80Factura.Cell(12, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                count = 13;
                idMatriz = -1;
                id_cte = -1;
                totalCte = 0;
                TotalMAtriz = 0;
                TotalGral = 0;

                foreach (ReporteRemisiones registros in lista80Factura)
                {
                    if (idMatriz == -1)
                    {
                        idMatriz = registros.id_matriz;
                        Excel80Factura.Cell(count, 1).Value = "Matriz";
                        Excel80Factura.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Factura.Cell(count, 2).Value = registros.id_matriz;
                        Excel80Factura.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Factura.Range(count, 3, count, 4).Merge();
                        Excel80Factura.Cell(count, 3).Value = registros.nombreMatriz;
                        Excel80Factura.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                    }
                    if (id_cte == -1)
                    {
                        id_cte = registros.Id_Cte;

                        Excel80Factura.Cell(count, 1).Value = "Cliente";
                        Excel80Factura.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Factura.Cell(count, 2).Value = registros.Id_Cte;
                        Excel80Factura.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Factura.Range(count, 3, count, 4).Merge();
                        Excel80Factura.Cell(count, 3).Value = registros.Cte_NomComercial;
                        Excel80Factura.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                    }

                    if (idMatriz != registros.id_matriz)
                    {
                        Excel80Factura.Cell(count, 6).Value = "Total por Cliente";
                        Excel80Factura.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Factura.Cell(count, 7).Value = totalCte.ToString("C2");
                        Excel80Factura.Cell(count, 7).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                        totalCte = 0;

                        Excel80Factura.Cell(count, 6).Value = "Total por Matriz";
                        Excel80Factura.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Factura.Cell(count, 7).Value = TotalMAtriz.ToString("C2");
                        Excel80Factura.Cell(count, 7).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                        TotalMAtriz = 0;

                        idMatriz = registros.id_matriz;
                        Excel80Factura.Cell(count, 1).Value = "Matriz";
                        Excel80Factura.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Factura.Cell(count, 2).Value = registros.id_matriz;
                        Excel80Factura.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Factura.Range(count, 3, count, 4).Merge();
                        Excel80Factura.Cell(count, 3).Value = registros.nombreMatriz;
                        Excel80Factura.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;

                        id_cte = registros.Id_Cte;
                        Excel80Factura.Cell(count, 1).Value = "Cliente";
                        Excel80Factura.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Factura.Cell(count, 2).Value = registros.Id_Cte;
                        Excel80Factura.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Factura.Range(count, 3, count, 4).Merge();
                        Excel80Factura.Cell(count, 3).Value = registros.Cte_NomComercial;
                        Excel80Factura.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;

                        Excel80Factura.Cell(count, 1).Value = registros.Id_Rem;
                        Excel80Factura.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Factura.Cell(count, 2).Value = registros.Folio;
                        Excel80Factura.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Factura.Cell(count, 3).Value = registros.Rem_Fecha;
                        Excel80Factura.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Factura.Cell(count, 4).Value = registros.Id_Prd;
                        Excel80Factura.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Factura.Cell(count, 5).Value = registros.Prd_Descripcion;
                        Excel80Factura.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Factura.Cell(count, 6).Value = registros.SaldoUnidades;
                        Excel80Factura.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        double total = registros.SaldoUnidades * registros.Prd_Pesos;
                        Excel80Factura.Cell(count, 7).Value = total.ToString("C2");
                        Excel80Factura.Cell(count, 7).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                        totalCte = totalCte + total;
                        TotalMAtriz = TotalMAtriz + total;
                        TotalGral = TotalGral + total;
                        count = count + 1;
                    }
                    else
                    {
                        if (id_cte != registros.Id_Cte)
                        {
                            Excel80Factura.Cell(count, 6).Value = "Total por Cliente";
                            Excel80Factura.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            Excel80Factura.Cell(count, 7).Value = totalCte.ToString("C2");
                            Excel80Factura.Cell(count, 7).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            count = count + 1;
                            totalCte = 0;

                            id_cte = registros.Id_Cte;
                            Excel80Factura.Cell(count, 1).Value = "Cliente";
                            Excel80Factura.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            Excel80Factura.Cell(count, 2).Value = registros.Id_Cte;
                            Excel80Factura.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            Excel80Factura.Range(count, 3, count, 4).Merge();
                            Excel80Factura.Cell(count, 3).Value = registros.Cte_NomComercial;
                            Excel80Factura.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            count = count + 1;

                            Excel80Factura.Cell(count, 1).Value = registros.Id_Rem;
                            Excel80Factura.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            Excel80Factura.Cell(count, 2).Value = registros.Folio;
                            Excel80Factura.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            Excel80Factura.Cell(count, 3).Value = registros.Rem_Fecha;
                            Excel80Factura.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            Excel80Factura.Cell(count, 4).Value = registros.Id_Prd;
                            Excel80Factura.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            Excel80Factura.Cell(count, 5).Value = registros.Prd_Descripcion;
                            Excel80Factura.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            Excel80Factura.Cell(count, 6).Value = registros.SaldoUnidades;
                            Excel80Factura.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            double total = registros.SaldoUnidades * registros.Prd_Pesos;
                            Excel80Factura.Cell(count, 7).Value = total.ToString("C2");
                            Excel80Factura.Cell(count, 7).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                            totalCte = totalCte + total;
                            TotalMAtriz = TotalMAtriz + total;
                            TotalGral = TotalGral + total;
                            count = count + 1;
                        }
                        else
                        {
                            Excel80Factura.Cell(count, 1).Value = registros.Id_Rem;
                            Excel80Factura.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            Excel80Factura.Cell(count, 2).Value = registros.Folio;
                            Excel80Factura.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            Excel80Factura.Cell(count, 3).Value = registros.Rem_Fecha;
                            Excel80Factura.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            Excel80Factura.Cell(count, 4).Value = registros.Id_Prd;
                            Excel80Factura.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            Excel80Factura.Cell(count, 5).Value = registros.Prd_Descripcion;
                            Excel80Factura.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            Excel80Factura.Cell(count, 6).Value = registros.SaldoUnidades;
                            Excel80Factura.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            double total = registros.SaldoUnidades * registros.Prd_Pesos;
                            Excel80Factura.Cell(count, 7).Value = total.ToString("C2");
                            Excel80Factura.Cell(count, 7).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                            totalCte = totalCte + total;
                            TotalMAtriz = TotalMAtriz + total;
                            TotalGral = TotalGral + total;
                            count = count + 1;
                        }
                    }
                }

                if (TotalMAtriz != 0)
                {
                    Excel80Factura.Cell(count, 6).Value = "Total por Cliente";
                    Excel80Factura.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    Excel80Factura.Cell(count, 7).Value = totalCte.ToString("C2");
                    Excel80Factura.Cell(count, 7).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    totalCte = 0;

                    Excel80Factura.Cell(count, 6).Value = "Total por Matriz";
                    Excel80Factura.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    Excel80Factura.Cell(count, 7).Value = TotalMAtriz.ToString("C2");
                    Excel80Factura.Cell(count, 7).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    TotalMAtriz = 0;

                    Excel80Factura.Cell(count, 6).Value = "Total";
                    Excel80Factura.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    Excel80Factura.Cell(count, 7).Value = TotalGral.ToString("C2");
                    Excel80Factura.Cell(count, 7).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    TotalGral = 0;
                }
                #endregion
                Excel80Factura.Columns().AdjustToContents();
                Excel80Factura.PageSetup.PageOrientation = XLPageOrientation.Portrait;
                Excel80Factura.PageSetup.Margins.Top = 0.5;
                Excel80Factura.PageSetup.Margins.Bottom = 0.3;
                Excel80Factura.PageSetup.Margins.Left = 0.5;
                Excel80Factura.PageSetup.Margins.Right = 0.3;
                Excel80Factura.PageSetup.Margins.Footer = 0;
                Excel80Factura.PageSetup.Margins.Header = 0;
                Excel80Factura.PageSetup.PagesWide = 1;
                Excel80Factura.PageSetup.PaperSize = XLPaperSize.A4Paper;
                Excel80Factura.PageSetup.VerticalDpi = 600;
                Excel80Factura.PageSetup.HorizontalDpi = 600;
                #endregion
                //Reporte mov 80 Vigente
                #region Mov80vigente

                ReporteRemisiones DatosVigente = new ReporteRemisiones();
                List<ReporteRemisiones> listaVigente = new List<ReporteRemisiones>();
                DatosVigente.Id_Emp = Sesion.Id_Emp;
                DatosVigente.Id_Cd = Sesion.Id_Cd_Ver;
                DatosVigente.Vencido = 0;
                DatosVigente.FechaIni = fechainicial;
                DatosVigente.FechaFin = fechafinal;
                DatosVigente.TipoRemision = 80;
                CN.Rep_RemisionesVencidasCN(DatosVigente, ref listaVigente, Sesion.Emp_Cnx);

                var Excel80Vigente = workbook.Worksheets.Add("Mov 80-Vigente");

                #region Encabezado

                Excel80Vigente.Cell(1, 1).Value = "Sucursal";
                Excel80Vigente.Cell(1, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                Excel80Vigente.Cell(1, 2).Value = Sesion.Id_Cd_Ver + " - " + Sesion.Cd_Nombre;
                Excel80Vigente.Cell(1, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                Excel80Vigente.Cell(1, 4).Value = Emp_Nombre;
                Excel80Vigente.Cell(1, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                Excel80Vigente.Cell(2, 1).Value = "Usuario";
                Excel80Vigente.Cell(2, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                Excel80Vigente.Cell(2, 2).Value = Sesion.U_Nombre;
                Excel80Vigente.Cell(2, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                Excel80Vigente.Cell(2, 4).Value = "Control de Remisiones";
                Excel80Vigente.Cell(2, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                Excel80Vigente.Cell(3, 1).Value = "Fecha del Documento";
                Excel80Vigente.Cell(3, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                Excel80Vigente.Cell(3, 2).Value = DateTime.Now.ToString("dd/MM/yyy");
                Excel80Vigente.Cell(3, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                Excel80Vigente.Cell(4, 1).Value = "Fecha Inical";
                Excel80Vigente.Cell(4, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                Excel80Vigente.Cell(4, 2).Value = fechainicial.ToString("dd/MM/yyy"); ;
                Excel80Vigente.Cell(4, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                Excel80Vigente.Cell(5, 1).Value = "Fecha Final";
                Excel80Vigente.Cell(5, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                Excel80Vigente.Cell(5, 2).Value = fechafinal.ToString("dd/MM/yyy");
                Excel80Vigente.Cell(5, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                Excel80Vigente.Cell(6, 1).Value = "Cliente";
                Excel80Vigente.Cell(6, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                Excel80Vigente.Cell(6, 2).Value = "Todos";
                Excel80Vigente.Cell(6, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                Excel80Vigente.Cell(7, 1).Value = "Territorio";
                Excel80Vigente.Cell(7, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                Excel80Vigente.Cell(7, 2).Value = "Todos";
                Excel80Vigente.Cell(7, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                Excel80Vigente.Cell(8, 1).Value = "Vencidos";
                Excel80Vigente.Cell(8, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                Excel80Vigente.Cell(8, 2).Value = "No";
                Excel80Vigente.Cell(8, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                Excel80Vigente.Cell(9, 1).Value = "Nivel";
                Excel80Vigente.Cell(9, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                Excel80Vigente.Cell(9, 2).Value = "Detalle";
                Excel80Vigente.Cell(9, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                Excel80Vigente.Cell(10, 1).Value = "Tipo de Remisión";
                Excel80Vigente.Cell(10, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                Excel80Vigente.Range(10, 2, 10, 4).Merge();
                Excel80Vigente.Range(10, 2, 10, 4).Value = "80 Transferencia de Cuentas Nacionales";
                Excel80Vigente.Range(10, 2, 10, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                #endregion

                #region Cuerpo

                Excel80Vigente.Cell(12, 1).Value = "Remisión";
                Excel80Vigente.Cell(12, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                Excel80Vigente.Cell(12, 2).Value = "Fecha";
                Excel80Vigente.Cell(12, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                Excel80Vigente.Cell(12, 3).Value = "Núm de Producto";
                Excel80Vigente.Cell(12, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                Excel80Vigente.Cell(12, 4).Value = "Descripción de Producto";
                Excel80Vigente.Cell(12, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                Excel80Vigente.Cell(12, 5).Value = "Unidades";
                Excel80Vigente.Cell(12, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                Excel80Vigente.Cell(12, 6).Value = "Importe";
                Excel80Vigente.Cell(12, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                count = 13;
                idMatriz = -1;
                id_cte = -1;
                totalCte = 0;
                TotalMAtriz = 0;
                TotalGral = 0;

                foreach (ReporteRemisiones registros in listaVigente)
                {
                    if (idMatriz == -1)
                    {
                        idMatriz = registros.id_matriz;
                        Excel80Vigente.Cell(count, 1).Value = "Matriz";
                        Excel80Vigente.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Vigente.Cell(count, 2).Value = registros.id_matriz;
                        Excel80Vigente.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Vigente.Range(count, 3, count, 4).Merge();
                        Excel80Vigente.Cell(count, 3).Value = registros.nombreMatriz;
                        Excel80Vigente.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                    }
                    if (id_cte == -1)
                    {
                        id_cte = registros.Id_Cte;

                        Excel80Vigente.Cell(count, 1).Value = "Cliente";
                        Excel80Vigente.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Vigente.Cell(count, 2).Value = registros.Id_Cte;
                        Excel80Vigente.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Vigente.Range(count, 3, count, 4).Merge();
                        Excel80Vigente.Cell(count, 3).Value = registros.Cte_NomComercial;
                        Excel80Vigente.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                    }

                    if (idMatriz != registros.id_matriz)
                    {
                        Excel80Vigente.Cell(count, 5).Value = "Total por Cliente";
                        Excel80Vigente.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Vigente.Cell(count, 6).Value = totalCte.ToString("C2");
                        Excel80Vigente.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                        totalCte = 0;

                        Excel80Vigente.Cell(count, 5).Value = "Total por Matriz";
                        Excel80Vigente.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Vigente.Cell(count, 6).Value = TotalMAtriz.ToString("C2");
                        Excel80Vigente.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                        TotalMAtriz = 0;

                        idMatriz = registros.id_matriz;
                        Excel80Vigente.Cell(count, 1).Value = "Matriz";
                        Excel80Vigente.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Vigente.Cell(count, 2).Value = registros.id_matriz;
                        Excel80Vigente.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Vigente.Range(count, 3, count, 4).Merge();
                        Excel80Vigente.Cell(count, 3).Value = registros.nombreMatriz;
                        Excel80Vigente.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;

                        id_cte = registros.Id_Cte;
                        Excel80Vigente.Cell(count, 1).Value = "Cliente";
                        Excel80Vigente.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Vigente.Cell(count, 2).Value = registros.Id_Cte;
                        Excel80Vigente.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Vigente.Range(count, 3, count, 4).Merge();
                        Excel80Vigente.Cell(count, 3).Value = registros.Cte_NomComercial;
                        Excel80Vigente.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;

                        Excel80Vigente.Cell(count, 1).Value = registros.Id_Rem;
                        Excel80Vigente.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Vigente.Cell(count, 2).Value = registros.Rem_Fecha;
                        Excel80Vigente.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Vigente.Cell(count, 3).Value = registros.Id_Prd;
                        Excel80Vigente.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Vigente.Cell(count, 4).Value = registros.Prd_Descripcion;
                        Excel80Vigente.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Vigente.Cell(count, 5).Value = registros.SaldoUnidades;
                        Excel80Vigente.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        double total = registros.SaldoUnidades * registros.Prd_Pesos;
                        Excel80Vigente.Cell(count, 6).Value = total.ToString("C2");
                        Excel80Vigente.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                        totalCte = totalCte + total;
                        TotalMAtriz = TotalMAtriz + total;
                        TotalGral = TotalGral + total;
                        count = count + 1;
                    }
                    else
                    {
                        if (id_cte != registros.Id_Cte)
                        {
                            Excel80Vigente.Cell(count, 5).Value = "Total por Cliente";
                            Excel80Vigente.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            Excel80Vigente.Cell(count, 6).Value = totalCte.ToString("C2");
                            Excel80Vigente.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            count = count + 1;
                            totalCte = 0;

                            id_cte = registros.Id_Cte;
                            Excel80Vigente.Cell(count, 1).Value = "Cliente";
                            Excel80Vigente.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            Excel80Vigente.Cell(count, 2).Value = registros.Id_Cte;
                            Excel80Vigente.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            Excel80Vigente.Range(count, 3, count, 4).Merge();
                            Excel80Vigente.Cell(count, 3).Value = registros.Cte_NomComercial;
                            Excel80Vigente.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            count = count + 1;

                            Excel80Vigente.Cell(count, 1).Value = registros.Id_Rem;
                            Excel80Vigente.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            Excel80Vigente.Cell(count, 2).Value = registros.Rem_Fecha;
                            Excel80Vigente.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            Excel80Vigente.Cell(count, 3).Value = registros.Id_Prd;
                            Excel80Vigente.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            Excel80Vigente.Cell(count, 4).Value = registros.Prd_Descripcion;
                            Excel80Vigente.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            Excel80Vigente.Cell(count, 5).Value = registros.SaldoUnidades;
                            Excel80Vigente.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            double total = registros.SaldoUnidades * registros.Prd_Pesos;
                            Excel80Vigente.Cell(count, 6).Value = total.ToString("C2");
                            Excel80Vigente.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                            totalCte = totalCte + total;
                            TotalMAtriz = TotalMAtriz + total;
                            TotalGral = TotalGral + total;
                            count = count + 1;
                        }
                        else
                        {
                            Excel80Vigente.Cell(count, 1).Value = registros.Id_Rem;
                            Excel80Vigente.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            Excel80Vigente.Cell(count, 2).Value = registros.Rem_Fecha;
                            Excel80Vigente.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            Excel80Vigente.Cell(count, 3).Value = registros.Id_Prd;
                            Excel80Vigente.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            Excel80Vigente.Cell(count, 4).Value = registros.Prd_Descripcion;
                            Excel80Vigente.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            Excel80Vigente.Cell(count, 5).Value = registros.SaldoUnidades;
                            Excel80Vigente.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            double total = registros.SaldoUnidades * registros.Prd_Pesos;
                            Excel80Vigente.Cell(count, 6).Value = total.ToString("C2");
                            Excel80Vigente.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                            totalCte = totalCte + total;
                            TotalMAtriz = TotalMAtriz + total;
                            TotalGral = TotalGral + total;
                            count = count + 1;
                        }
                    }
                }

                if (TotalMAtriz != 0)
                {
                    Excel80Vigente.Cell(count, 5).Value = "Total por Cliente";
                    Excel80Vigente.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    Excel80Vigente.Cell(count, 6).Value = totalCte.ToString("C2");
                    Excel80Vigente.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    totalCte = 0;

                    Excel80Vigente.Cell(count, 5).Value = "Total por Matriz";
                    Excel80Vigente.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    Excel80Vigente.Cell(count, 6).Value = TotalMAtriz.ToString("C2");
                    Excel80Vigente.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    TotalMAtriz = 0;

                    Excel80Vigente.Cell(count, 5).Value = "Total";
                    Excel80Vigente.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    Excel80Vigente.Cell(count, 6).Value = TotalGral.ToString("C2");
                    Excel80Vigente.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    TotalGral = 0;
                }


                #endregion
                Excel80Vigente.Columns().AdjustToContents();

                Excel80Vigente.PageSetup.PageOrientation = XLPageOrientation.Portrait;
                Excel80Vigente.PageSetup.Margins.Top = 0.5;
                Excel80Vigente.PageSetup.Margins.Bottom = 0.3;
                Excel80Vigente.PageSetup.Margins.Left = 0.5;
                Excel80Vigente.PageSetup.Margins.Right = 0.3;
                Excel80Vigente.PageSetup.Margins.Footer = 0;
                Excel80Vigente.PageSetup.Margins.Header = 0;
                Excel80Vigente.PageSetup.PagesWide = 1;
                Excel80Vigente.PageSetup.PaperSize = XLPaperSize.A4Paper;
                Excel80Vigente.PageSetup.VerticalDpi = 600;
                Excel80Vigente.PageSetup.HorizontalDpi = 600;
                #endregion
                //Reporte mov 80 Vencido
                #region Mov80Vencida

                ReporteRemisiones Datos80Vencida = new ReporteRemisiones();
                List<ReporteRemisiones> lista80Vencida = new List<ReporteRemisiones>();
                Datos80Vencida.Id_Emp = Sesion.Id_Emp;
                Datos80Vencida.Id_Cd = Sesion.Id_Cd_Ver;
                Datos80Vencida.Vencido = 1;
                Datos80Vencida.FechaIni = fechainicial;
                Datos80Vencida.FechaFin = fechafinal;
                Datos80Vencida.TipoRemision = 80;
                CN.Rep_RemisionesVencidasCN(Datos80Vencida, ref lista80Vencida, Sesion.Emp_Cnx);


                var Excel80Vencida = workbook.Worksheets.Add("Mov 80-Vencida");

                #region Encabezado

                Excel80Vencida.Cell(1, 1).Value = "Sucursal";
                Excel80Vencida.Cell(1, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                Excel80Vencida.Cell(1, 2).Value = Sesion.Id_Cd_Ver + " - " + Sesion.Cd_Nombre;
                Excel80Vencida.Cell(1, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                Excel80Vencida.Cell(1, 4).Value = Emp_Nombre;
                Excel80Vencida.Cell(1, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                Excel80Vencida.Cell(2, 1).Value = "Usuario";
                Excel80Vencida.Cell(2, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                Excel80Vencida.Cell(2, 2).Value = Sesion.U_Nombre;
                Excel80Vencida.Cell(2, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                Excel80Vencida.Cell(2, 4).Value = "Control de Remisiones";
                Excel80Vencida.Cell(2, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                Excel80Vencida.Cell(3, 1).Value = "Fecha del Documento";
                Excel80Vencida.Cell(3, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                Excel80Vencida.Cell(3, 2).Value = DateTime.Now.ToString("dd/MM/yyy");
                Excel80Vencida.Cell(3, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                Excel80Vencida.Cell(4, 1).Value = "Fecha Inical";
                Excel80Vencida.Cell(4, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                Excel80Vencida.Cell(4, 2).Value = fechainicial.ToString("dd/MM/yyy"); ;
                Excel80Vencida.Cell(4, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                Excel80Vencida.Cell(5, 1).Value = "Fecha Final";
                Excel80Vencida.Cell(5, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                Excel80Vencida.Cell(5, 2).Value = fechafinal.ToString("dd/MM/yyy");
                Excel80Vencida.Cell(5, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                Excel80Vencida.Cell(6, 1).Value = "Cliente";
                Excel80Vencida.Cell(6, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                Excel80Vencida.Cell(6, 2).Value = "Todos";
                Excel80Vencida.Cell(6, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                Excel80Vencida.Cell(7, 1).Value = "Territorio";
                Excel80Vencida.Cell(7, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                Excel80Vencida.Cell(7, 2).Value = "Todos";
                Excel80Vencida.Cell(7, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                Excel80Vencida.Cell(8, 1).Value = "Vencidos";
                Excel80Vencida.Cell(8, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                Excel80Vencida.Cell(8, 2).Value = "Si";
                Excel80Vencida.Cell(8, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                Excel80Vencida.Cell(9, 1).Value = "Nivel";
                Excel80Vencida.Cell(9, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                Excel80Vencida.Cell(9, 2).Value = "Detalle";
                Excel80Vencida.Cell(9, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                Excel80Vencida.Cell(10, 1).Value = "Tipo de Remisión";
                Excel80Vencida.Cell(10, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                Excel80Vencida.Range(10, 2, 10, 4).Merge();
                Excel80Vencida.Range(10, 2, 10, 4).Value = "80 Transferencia de Cuentas Nacionales";
                Excel80Vencida.Range(10, 2, 10, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                #endregion

                #region Cuerpo

                Excel80Vencida.Cell(12, 1).Value = "Remisión";
                Excel80Vencida.Cell(12, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                Excel80Vencida.Cell(12, 2).Value = "Fecha";
                Excel80Vencida.Cell(12, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                Excel80Vencida.Cell(12, 3).Value = "Núm de Producto";
                Excel80Vencida.Cell(12, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                Excel80Vencida.Cell(12, 4).Value = "Descripción de Producto";
                Excel80Vencida.Cell(12, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                Excel80Vencida.Cell(12, 5).Value = "Unidades";
                Excel80Vencida.Cell(12, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                Excel80Vencida.Cell(12, 6).Value = "Importe";
                Excel80Vencida.Cell(12, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                count = 13;
                idMatriz = -1;
                id_cte = -1;
                totalCte = 0;
                TotalMAtriz = 0;
                TotalGral = 0;

                foreach (ReporteRemisiones registros in lista80Vencida)
                {
                    if (idMatriz == -1)
                    {
                        idMatriz = registros.id_matriz;
                        Excel80Vencida.Cell(count, 1).Value = "Matriz";
                        Excel80Vencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Vencida.Cell(count, 2).Value = registros.id_matriz;
                        Excel80Vencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Vencida.Range(count, 3, count, 4).Merge();
                        Excel80Vencida.Cell(count, 3).Value = registros.nombreMatriz;
                        Excel80Vencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                    }
                    if (id_cte == -1)
                    {
                        id_cte = registros.Id_Cte;

                        Excel80Vencida.Cell(count, 1).Value = "Cliente";
                        Excel80Vencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Vencida.Cell(count, 2).Value = registros.Id_Cte;
                        Excel80Vencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Vencida.Range(count, 3, count, 4).Merge();
                        Excel80Vencida.Cell(count, 3).Value = registros.Cte_NomComercial;
                        Excel80Vencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                    }

                    if (idMatriz != registros.id_matriz)
                    {
                        Excel80Vencida.Cell(count, 5).Value = "Total por Cliente";
                        Excel80Vencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Vencida.Cell(count, 6).Value = totalCte.ToString("C2");
                        Excel80Vencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                        totalCte = 0;

                        Excel80Vencida.Cell(count, 5).Value = "Total por Matriz";
                        Excel80Vencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Vencida.Cell(count, 6).Value = TotalMAtriz.ToString("C2");
                        Excel80Vencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                        TotalMAtriz = 0;

                        idMatriz = registros.id_matriz;
                        Excel80Vencida.Cell(count, 1).Value = "Matriz";
                        Excel80Vencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Vencida.Cell(count, 2).Value = registros.id_matriz;
                        Excel80Vencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Vencida.Range(count, 3, count, 4).Merge();
                        Excel80Vencida.Cell(count, 3).Value = registros.nombreMatriz;
                        Excel80Vencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;

                        id_cte = registros.Id_Cte;
                        Excel80Vencida.Cell(count, 1).Value = "Cliente";
                        Excel80Vencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Vencida.Cell(count, 2).Value = registros.Id_Cte;
                        Excel80Vencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Vencida.Range(count, 3, count, 4).Merge();
                        Excel80Vencida.Cell(count, 3).Value = registros.Cte_NomComercial;
                        Excel80Vencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;

                        Excel80Vencida.Cell(count, 1).Value = registros.Id_Rem;
                        Excel80Vencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Vencida.Cell(count, 2).Value = registros.Rem_Fecha;
                        Excel80Vencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Vencida.Cell(count, 3).Value = registros.Id_Prd;
                        Excel80Vencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Vencida.Cell(count, 4).Value = registros.Prd_Descripcion;
                        Excel80Vencida.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        Excel80Vencida.Cell(count, 5).Value = registros.SaldoUnidades;
                        Excel80Vencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        double total = registros.SaldoUnidades * registros.Prd_Pesos;
                        Excel80Vencida.Cell(count, 6).Value = total.ToString("C2");
                        Excel80Vencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                        totalCte = totalCte + total;
                        TotalMAtriz = TotalMAtriz + total;
                        TotalGral = TotalGral + total;
                        count = count + 1;
                    }
                    else
                    {
                        if (id_cte != registros.Id_Cte)
                        {
                            Excel80Vencida.Cell(count, 5).Value = "Total por Cliente";
                            Excel80Vencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            Excel80Vencida.Cell(count, 6).Value = totalCte.ToString("C2");
                            Excel80Vencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            count = count + 1;
                            totalCte = 0;

                            id_cte = registros.Id_Cte;
                            Excel80Vencida.Cell(count, 1).Value = "Cliente";
                            Excel80Vencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            Excel80Vencida.Cell(count, 2).Value = registros.Id_Cte;
                            Excel80Vencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            Excel80Vencida.Range(count, 3, count, 4).Merge();
                            Excel80Vencida.Cell(count, 3).Value = registros.Cte_NomComercial;
                            Excel80Vencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            count = count + 1;

                            Excel80Vencida.Cell(count, 1).Value = registros.Id_Rem;
                            Excel80Vencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            Excel80Vencida.Cell(count, 2).Value = registros.Rem_Fecha;
                            Excel80Vencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            Excel80Vencida.Cell(count, 3).Value = registros.Id_Prd;
                            Excel80Vencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            Excel80Vencida.Cell(count, 4).Value = registros.Prd_Descripcion;
                            Excel80Vencida.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            Excel80Vencida.Cell(count, 5).Value = registros.SaldoUnidades;
                            Excel80Vencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            double total = registros.SaldoUnidades * registros.Prd_Pesos;
                            Excel80Vencida.Cell(count, 6).Value = total.ToString("C2");
                            Excel80Vencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                            totalCte = totalCte + total;
                            TotalMAtriz = TotalMAtriz + total;
                            TotalGral = TotalGral + total;
                            count = count + 1;
                        }
                        else
                        {
                            Excel80Vencida.Cell(count, 1).Value = registros.Id_Rem;
                            Excel80Vencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            Excel80Vencida.Cell(count, 2).Value = registros.Rem_Fecha;
                            Excel80Vencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            Excel80Vencida.Cell(count, 3).Value = registros.Id_Prd;
                            Excel80Vencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            Excel80Vencida.Cell(count, 4).Value = registros.Prd_Descripcion;
                            Excel80Vencida.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            Excel80Vencida.Cell(count, 5).Value = registros.SaldoUnidades;
                            Excel80Vencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            double total = registros.SaldoUnidades * registros.Prd_Pesos;
                            Excel80Vencida.Cell(count, 6).Value = total.ToString("C2");
                            Excel80Vencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                            totalCte = totalCte + total;
                            TotalMAtriz = TotalMAtriz + total;
                            TotalGral = TotalGral + total;
                            count = count + 1;
                        }
                    }
                }

                if (TotalMAtriz != 0)
                {
                    Excel80Vencida.Cell(count, 5).Value = "Total por Cliente";
                    Excel80Vencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    Excel80Vencida.Cell(count, 6).Value = totalCte.ToString("C2");
                    Excel80Vencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    totalCte = 0;

                    Excel80Vencida.Cell(count, 5).Value = "Total por Matriz";
                    Excel80Vencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    Excel80Vencida.Cell(count, 6).Value = TotalMAtriz.ToString("C2");
                    Excel80Vencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    TotalMAtriz = 0;

                    Excel80Vencida.Cell(count, 5).Value = "Total";
                    Excel80Vencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    Excel80Vencida.Cell(count, 6).Value = TotalGral.ToString("C2");
                    Excel80Vencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    TotalGral = 0;
                }

                #endregion
                Excel80Vencida.Columns().AdjustToContents();

                Excel80Vencida.PageSetup.PageOrientation = XLPageOrientation.Portrait;
                Excel80Vencida.PageSetup.Margins.Top = 0.5;
                Excel80Vencida.PageSetup.Margins.Bottom = 0.3;
                Excel80Vencida.PageSetup.Margins.Left = 0.5;
                Excel80Vencida.PageSetup.Margins.Right = 0.3;
                Excel80Vencida.PageSetup.Margins.Footer = 0;
                Excel80Vencida.PageSetup.Margins.Header = 0;
                Excel80Vencida.PageSetup.PagesWide = 1;
                Excel80Vencida.PageSetup.PaperSize = XLPaperSize.A4Paper;
                Excel80Vencida.PageSetup.VerticalDpi = 600;
                Excel80Vencida.PageSetup.HorizontalDpi = 600;
                #endregion
                //Reporte de Facturas vigentes
                #region MovFactVigente

                ReporteRemisiones DatosFactVigente = new ReporteRemisiones();
                List<ReporteRemisiones> listaFactVigente = new List<ReporteRemisiones>();
                DatosFactVigente.Id_Emp = Sesion.Id_Emp;
                DatosFactVigente.Id_Cd = Sesion.Id_Cd_Ver;
                DatosFactVigente.Vencido = 0;
                DatosFactVigente.FechaIni = fechainicial;
                DatosFactVigente.FechaFin = fechafinal;
                DatosFactVigente.TipoRemision = 63;
                CN.Rep_RemisionesVencidas(DatosFactVigente, ref listaFactVigente, Sesion.Emp_Cnx);


                var ExcelFactVigente = workbook.Worksheets.Add("PXF_VIGENTE");

                #region Encabezado

                ExcelFactVigente.Cell(1, 1).Value = "Sucursal";
                ExcelFactVigente.Cell(1, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelFactVigente.Cell(1, 2).Value = Sesion.Id_Cd_Ver + " - " + Sesion.Cd_Nombre;
                ExcelFactVigente.Cell(1, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelFactVigente.Cell(1, 4).Value = Emp_Nombre;
                ExcelFactVigente.Cell(1, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelFactVigente.Cell(2, 1).Value = "Usuario";
                ExcelFactVigente.Cell(2, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelFactVigente.Cell(2, 2).Value = Sesion.U_Nombre;
                ExcelFactVigente.Cell(2, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelFactVigente.Cell(2, 4).Value = "Control de Remisiones";
                ExcelFactVigente.Cell(2, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelFactVigente.Cell(3, 1).Value = "Fecha del Documento";
                ExcelFactVigente.Cell(3, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelFactVigente.Cell(3, 2).Value = DateTime.Now.ToString("dd/MM/yyy");
                ExcelFactVigente.Cell(3, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelFactVigente.Cell(4, 1).Value = "Fecha Inical";
                ExcelFactVigente.Cell(4, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelFactVigente.Cell(4, 2).Value = fechainicial.ToString("dd/MM/yyy"); ;
                ExcelFactVigente.Cell(4, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelFactVigente.Cell(5, 1).Value = "Fecha Final";
                ExcelFactVigente.Cell(5, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelFactVigente.Cell(5, 2).Value = fechafinal.ToString("dd/MM/yyy");
                ExcelFactVigente.Cell(5, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelFactVigente.Cell(6, 1).Value = "Cliente";
                ExcelFactVigente.Cell(6, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelFactVigente.Cell(6, 2).Value = "Todos";
                ExcelFactVigente.Cell(6, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelFactVigente.Cell(7, 1).Value = "Territorio";
                ExcelFactVigente.Cell(7, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelFactVigente.Cell(7, 2).Value = "Todos";
                ExcelFactVigente.Cell(7, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelFactVigente.Cell(8, 1).Value = "Vencidos";
                ExcelFactVigente.Cell(8, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelFactVigente.Cell(8, 2).Value = "NO";
                ExcelFactVigente.Cell(8, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelFactVigente.Cell(9, 1).Value = "Nivel";
                ExcelFactVigente.Cell(9, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelFactVigente.Cell(9, 2).Value = "Detalle";
                ExcelFactVigente.Cell(9, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelFactVigente.Cell(10, 1).Value = "Tipo de Remisión";
                ExcelFactVigente.Cell(10, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelFactVigente.Range(10, 2, 10, 4).Merge();
                ExcelFactVigente.Range(10, 2, 10, 4).Value = "Pendiente por Facturar";
                ExcelFactVigente.Range(10, 2, 10, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                #endregion

                #region Cuerpo

                ExcelFactVigente.Cell(12, 1).Value = "Remisión";
                ExcelFactVigente.Cell(12, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelFactVigente.Cell(12, 2).Value = "Fecha";
                ExcelFactVigente.Cell(12, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelFactVigente.Cell(12, 3).Value = "Núm de Producto";
                ExcelFactVigente.Cell(12, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelFactVigente.Cell(12, 4).Value = "Descripción de Producto";
                ExcelFactVigente.Cell(12, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelFactVigente.Cell(12, 5).Value = "Unidades";
                ExcelFactVigente.Cell(12, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelFactVigente.Cell(12, 6).Value = "Importe";
                ExcelFactVigente.Cell(12, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                count = 13;
                idMatriz = -1;
                id_cte = -1;
                totalCte = 0;
                TotalMAtriz = 0;
                TotalGral = 0;

                foreach (ReporteRemisiones registros in listaFactVigente)
                {
                    if (idMatriz == -1)
                    {
                        idMatriz = registros.Id_Ter;
                        ExcelFactVigente.Cell(count, 1).Value = "Territorio";
                        ExcelFactVigente.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelFactVigente.Cell(count, 2).Value = registros.Id_Ter;
                        ExcelFactVigente.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelFactVigente.Range(count, 3, count, 4).Merge();
                        ExcelFactVigente.Cell(count, 3).Value = registros.Ter_Nombre;
                        ExcelFactVigente.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                    }
                    if (id_cte == -1)
                    {
                        id_cte = registros.Id_Cte;

                        ExcelFactVigente.Cell(count, 1).Value = "Cliente";
                        ExcelFactVigente.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelFactVigente.Cell(count, 2).Value = registros.Id_Cte;
                        ExcelFactVigente.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelFactVigente.Range(count, 3, count, 4).Merge();
                        ExcelFactVigente.Cell(count, 3).Value = registros.Cte_NomComercial;
                        ExcelFactVigente.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                    }

                    if (idMatriz != registros.Id_Ter)
                    {
                        ExcelFactVigente.Cell(count, 5).Value = "Total por Cliente";
                        ExcelFactVigente.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelFactVigente.Cell(count, 6).Value = totalCte.ToString("C2");
                        ExcelFactVigente.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                        totalCte = 0;

                        ExcelFactVigente.Cell(count, 5).Value = "Total por Territorio";
                        ExcelFactVigente.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelFactVigente.Cell(count, 6).Value = TotalMAtriz.ToString("C2");
                        ExcelFactVigente.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                        TotalMAtriz = 0;

                        idMatriz = registros.Id_Ter;
                        ExcelFactVigente.Cell(count, 1).Value = "Territorio";
                        ExcelFactVigente.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelFactVigente.Cell(count, 2).Value = registros.Id_Ter;
                        ExcelFactVigente.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelFactVigente.Range(count, 3, count, 4).Merge();
                        ExcelFactVigente.Cell(count, 3).Value = registros.Ter_Nombre;
                        ExcelFactVigente.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;

                        id_cte = registros.Id_Cte;
                        ExcelFactVigente.Cell(count, 1).Value = "Cliente";
                        ExcelFactVigente.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelFactVigente.Cell(count, 2).Value = registros.Id_Cte;
                        ExcelFactVigente.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelFactVigente.Range(count, 3, count, 4).Merge();
                        ExcelFactVigente.Cell(count, 3).Value = registros.Cte_NomComercial;
                        ExcelFactVigente.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;

                        ExcelFactVigente.Cell(count, 1).Value = registros.Id_Rem;
                        ExcelFactVigente.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelFactVigente.Cell(count, 2).Value = registros.Rem_Fecha;
                        ExcelFactVigente.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelFactVigente.Cell(count, 3).Value = registros.Id_Prd;
                        ExcelFactVigente.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelFactVigente.Cell(count, 4).Value = registros.Prd_Descripcion;
                        ExcelFactVigente.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelFactVigente.Cell(count, 5).Value = registros.SaldoUnidades;
                        ExcelFactVigente.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        double total = registros.SaldoUnidades * registros.Prd_Pesos;
                        ExcelFactVigente.Cell(count, 6).Value = total.ToString("C2");
                        ExcelFactVigente.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                        totalCte = totalCte + total;
                        TotalMAtriz = TotalMAtriz + total;
                        TotalGral = TotalGral + total;
                        count = count + 1;
                    }
                    else
                    {
                        if (id_cte != registros.Id_Cte)
                        {
                            ExcelFactVigente.Cell(count, 5).Value = "Total por Cliente";
                            ExcelFactVigente.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelFactVigente.Cell(count, 6).Value = totalCte.ToString("C2");
                            ExcelFactVigente.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            count = count + 1;
                            totalCte = 0;

                            id_cte = registros.Id_Cte;
                            ExcelFactVigente.Cell(count, 1).Value = "Cliente";
                            ExcelFactVigente.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelFactVigente.Cell(count, 2).Value = registros.Id_Cte;
                            ExcelFactVigente.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelFactVigente.Range(count, 3, count, 4).Merge();
                            ExcelFactVigente.Cell(count, 3).Value = registros.Cte_NomComercial;
                            ExcelFactVigente.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            count = count + 1;

                            ExcelFactVigente.Cell(count, 1).Value = registros.Id_Rem;
                            ExcelFactVigente.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelFactVigente.Cell(count, 2).Value = registros.Rem_Fecha;
                            ExcelFactVigente.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelFactVigente.Cell(count, 3).Value = registros.Id_Prd;
                            ExcelFactVigente.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelFactVigente.Cell(count, 4).Value = registros.Prd_Descripcion;
                            ExcelFactVigente.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelFactVigente.Cell(count, 5).Value = registros.SaldoUnidades;
                            ExcelFactVigente.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            double total = registros.SaldoUnidades * registros.Prd_Pesos;
                            ExcelFactVigente.Cell(count, 6).Value = total.ToString("C2");
                            ExcelFactVigente.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                            totalCte = totalCte + total;
                            TotalMAtriz = TotalMAtriz + total;
                            TotalGral = TotalGral + total;
                            count = count + 1;
                        }
                        else
                        {
                            ExcelFactVigente.Cell(count, 1).Value = registros.Id_Rem;
                            ExcelFactVigente.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelFactVigente.Cell(count, 2).Value = registros.Rem_Fecha;
                            ExcelFactVigente.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelFactVigente.Cell(count, 3).Value = registros.Id_Prd;
                            ExcelFactVigente.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelFactVigente.Cell(count, 4).Value = registros.Prd_Descripcion;
                            ExcelFactVigente.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelFactVigente.Cell(count, 5).Value = registros.SaldoUnidades;
                            ExcelFactVigente.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            double total = registros.SaldoUnidades * registros.Prd_Pesos;
                            ExcelFactVigente.Cell(count, 6).Value = total.ToString("C2");
                            ExcelFactVigente.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                            totalCte = totalCte + total;
                            TotalMAtriz = TotalMAtriz + total;
                            TotalGral = TotalGral + total;
                            count = count + 1;
                        }
                    }
                }

                if (TotalMAtriz != 0)
                {
                    ExcelFactVigente.Cell(count, 5).Value = "Total por Cliente";
                    ExcelFactVigente.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    ExcelFactVigente.Cell(count, 6).Value = totalCte.ToString("C2");
                    ExcelFactVigente.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    totalCte = 0;

                    ExcelFactVigente.Cell(count, 5).Value = "Total por Territorio";
                    ExcelFactVigente.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    ExcelFactVigente.Cell(count, 6).Value = TotalMAtriz.ToString("C2");
                    ExcelFactVigente.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    TotalMAtriz = 0;

                    ExcelFactVigente.Cell(count, 5).Value = "Total";
                    ExcelFactVigente.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    ExcelFactVigente.Cell(count, 6).Value = TotalGral.ToString("C2");
                    ExcelFactVigente.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    TotalGral = 0;
                }

                #endregion
                ExcelFactVigente.Columns().AdjustToContents();

                ExcelFactVigente.PageSetup.PageOrientation = XLPageOrientation.Portrait;
                ExcelFactVigente.PageSetup.Margins.Top = 0.5;
                ExcelFactVigente.PageSetup.Margins.Bottom = 0.3;
                ExcelFactVigente.PageSetup.Margins.Left = 0.5;
                ExcelFactVigente.PageSetup.Margins.Right = 0.3;
                ExcelFactVigente.PageSetup.Margins.Footer = 0;
                ExcelFactVigente.PageSetup.Margins.Header = 0;
                ExcelFactVigente.PageSetup.PagesWide = 1;
                ExcelFactVigente.PageSetup.PaperSize = XLPaperSize.A4Paper;
                ExcelFactVigente.PageSetup.VerticalDpi = 600;
                ExcelFactVigente.PageSetup.HorizontalDpi = 600;
                #endregion 
                //Reporte de Facturas vencidas
                #region MovFactVencida

                ReporteRemisiones DatosFactVencida = new ReporteRemisiones();
                List<ReporteRemisiones> listaFactVencida = new List<ReporteRemisiones>();
                DatosFactVencida.Id_Emp = Sesion.Id_Emp;
                DatosFactVencida.Id_Cd = Sesion.Id_Cd_Ver;
                DatosFactVencida.Vencido = 1;
                DatosFactVencida.FechaIni = fechainicial;
                DatosFactVencida.FechaFin = fechafinal;
                DatosFactVencida.TipoRemision = 63;
                CN.Rep_RemisionesVencidas(DatosFactVencida, ref listaFactVencida, Sesion.Emp_Cnx);


                var ExcelFactVencida = workbook.Worksheets.Add("PXF_VENCIDA");

                #region Encabezado

                ExcelFactVencida.Cell(1, 1).Value = "Sucursal";
                ExcelFactVencida.Cell(1, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelFactVencida.Cell(1, 2).Value = Sesion.Id_Cd_Ver + " - " + Sesion.Cd_Nombre;
                ExcelFactVencida.Cell(1, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelFactVencida.Cell(1, 4).Value = Emp_Nombre;
                ExcelFactVencida.Cell(1, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelFactVencida.Cell(2, 1).Value = "Usuario";
                ExcelFactVencida.Cell(2, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelFactVencida.Cell(2, 2).Value = Sesion.U_Nombre;
                ExcelFactVencida.Cell(2, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelFactVencida.Cell(2, 4).Value = "Control de Remisiones";
                ExcelFactVencida.Cell(2, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelFactVencida.Cell(3, 1).Value = "Fecha del Documento";
                ExcelFactVencida.Cell(3, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelFactVencida.Cell(3, 2).Value = DateTime.Now.ToString("dd/MM/yyy");
                ExcelFactVencida.Cell(3, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelFactVencida.Cell(4, 1).Value = "Fecha Inical";
                ExcelFactVencida.Cell(4, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelFactVencida.Cell(4, 2).Value = fechainicial.ToString("dd/MM/yyy"); ;
                ExcelFactVencida.Cell(4, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelFactVencida.Cell(5, 1).Value = "Fecha Final";
                ExcelFactVencida.Cell(5, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelFactVencida.Cell(5, 2).Value = fechafinal.ToString("dd/MM/yyy");
                ExcelFactVencida.Cell(5, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelFactVencida.Cell(6, 1).Value = "Cliente";
                ExcelFactVencida.Cell(6, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelFactVencida.Cell(6, 2).Value = "Todos";
                ExcelFactVencida.Cell(6, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelFactVencida.Cell(7, 1).Value = "Territorio";
                ExcelFactVencida.Cell(7, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelFactVencida.Cell(7, 2).Value = "Todos";
                ExcelFactVencida.Cell(7, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelFactVencida.Cell(8, 1).Value = "Vencidos";
                ExcelFactVencida.Cell(8, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelFactVencida.Cell(8, 2).Value = "Si";
                ExcelFactVencida.Cell(8, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelFactVencida.Cell(9, 1).Value = "Nivel";
                ExcelFactVencida.Cell(9, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelFactVencida.Cell(9, 2).Value = "Detalle";
                ExcelFactVencida.Cell(9, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelFactVencida.Cell(10, 1).Value = "Tipo de Remisión";
                ExcelFactVencida.Cell(10, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelFactVencida.Range(10, 2, 10, 4).Merge();
                ExcelFactVencida.Range(10, 2, 10, 4).Value = "Pendiente por Facturar";
                ExcelFactVencida.Range(10, 2, 10, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                #endregion

                #region Cuerpo

                ExcelFactVencida.Cell(12, 1).Value = "Remisión";
                ExcelFactVencida.Cell(12, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelFactVencida.Cell(12, 2).Value = "Fecha";
                ExcelFactVencida.Cell(12, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelFactVencida.Cell(12, 3).Value = "Núm de Producto";
                ExcelFactVencida.Cell(12, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelFactVencida.Cell(12, 4).Value = "Descripción de Producto";
                ExcelFactVencida.Cell(12, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelFactVencida.Cell(12, 5).Value = "Unidades";
                ExcelFactVencida.Cell(12, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelFactVencida.Cell(12, 6).Value = "Importe";
                ExcelFactVencida.Cell(12, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                count = 13;
                idMatriz = -1;
                id_cte = -1;
                totalCte = 0;
                TotalMAtriz = 0;
                TotalGral = 0;

                foreach (ReporteRemisiones registros in listaFactVencida)
                {
                    if (idMatriz == -1)
                    {
                        idMatriz = registros.Id_Ter;
                        ExcelFactVencida.Cell(count, 1).Value = "Territorio";
                        ExcelFactVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelFactVencida.Cell(count, 2).Value = registros.Id_Ter;
                        ExcelFactVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelFactVencida.Range(count, 3, count, 4).Merge();
                        ExcelFactVencida.Cell(count, 3).Value = registros.Ter_Nombre;
                        ExcelFactVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                    }
                    if (id_cte == -1)
                    {
                        id_cte = registros.Id_Cte;

                        ExcelFactVencida.Cell(count, 1).Value = "Cliente";
                        ExcelFactVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelFactVencida.Cell(count, 2).Value = registros.Id_Cte;
                        ExcelFactVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelFactVencida.Range(count, 3, count, 4).Merge();
                        ExcelFactVencida.Cell(count, 3).Value = registros.Cte_NomComercial;
                        ExcelFactVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                    }

                    if (idMatriz != registros.Id_Ter)
                    {
                        ExcelFactVencida.Cell(count, 5).Value = "Total por Cliente";
                        ExcelFactVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelFactVencida.Cell(count, 6).Value = totalCte.ToString("C2");
                        ExcelFactVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                        totalCte = 0;

                        ExcelFactVencida.Cell(count, 5).Value = "Total por Territorio";
                        ExcelFactVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelFactVencida.Cell(count, 6).Value = TotalMAtriz.ToString("C2");
                        ExcelFactVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                        TotalMAtriz = 0;

                        idMatriz = registros.Id_Ter;
                        ExcelFactVencida.Cell(count, 1).Value = "Territorio";
                        ExcelFactVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelFactVencida.Cell(count, 2).Value = registros.Id_Ter;
                        ExcelFactVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelFactVencida.Range(count, 3, count, 4).Merge();
                        ExcelFactVencida.Cell(count, 3).Value = registros.Ter_Nombre;
                        ExcelFactVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;

                        id_cte = registros.Id_Cte;
                        ExcelFactVencida.Cell(count, 1).Value = "Cliente";
                        ExcelFactVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelFactVencida.Cell(count, 2).Value = registros.Id_Cte;
                        ExcelFactVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelFactVencida.Range(count, 3, count, 4).Merge();
                        ExcelFactVencida.Cell(count, 3).Value = registros.Cte_NomComercial;
                        ExcelFactVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;

                        ExcelFactVencida.Cell(count, 1).Value = registros.Id_Rem;
                        ExcelFactVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelFactVencida.Cell(count, 2).Value = registros.Rem_Fecha;
                        ExcelFactVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelFactVencida.Cell(count, 3).Value = registros.Id_Prd;
                        ExcelFactVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelFactVencida.Cell(count, 4).Value = registros.Prd_Descripcion;
                        ExcelFactVencida.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelFactVencida.Cell(count, 5).Value = registros.SaldoUnidades;
                        ExcelFactVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        double total = registros.SaldoUnidades * registros.Prd_Pesos;
                        ExcelFactVencida.Cell(count, 6).Value = total.ToString("C2");
                        ExcelFactVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                        totalCte = totalCte + total;
                        TotalMAtriz = TotalMAtriz + total;
                        TotalGral = TotalGral + total;
                        count = count + 1;
                    }
                    else
                    {
                        if (id_cte != registros.Id_Cte)
                        {
                            ExcelFactVencida.Cell(count, 5).Value = "Total por Cliente";
                            ExcelFactVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelFactVencida.Cell(count, 6).Value = totalCte.ToString("C2");
                            ExcelFactVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            count = count + 1;
                            totalCte = 0;

                            id_cte = registros.Id_Cte;
                            ExcelFactVencida.Cell(count, 1).Value = "Cliente";
                            ExcelFactVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelFactVencida.Cell(count, 2).Value = registros.Id_Cte;
                            ExcelFactVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelFactVencida.Range(count, 3, count, 4).Merge();
                            ExcelFactVencida.Cell(count, 3).Value = registros.Cte_NomComercial;
                            ExcelFactVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            count = count + 1;

                            ExcelFactVencida.Cell(count, 1).Value = registros.Id_Rem;
                            ExcelFactVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelFactVencida.Cell(count, 2).Value = registros.Rem_Fecha;
                            ExcelFactVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelFactVencida.Cell(count, 3).Value = registros.Id_Prd;
                            ExcelFactVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelFactVencida.Cell(count, 4).Value = registros.Prd_Descripcion;
                            ExcelFactVencida.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelFactVencida.Cell(count, 5).Value = registros.SaldoUnidades;
                            ExcelFactVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            double total = registros.SaldoUnidades * registros.Prd_Pesos;
                            ExcelFactVencida.Cell(count, 6).Value = total.ToString("C2");
                            ExcelFactVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                            totalCte = totalCte + total;
                            TotalMAtriz = TotalMAtriz + total;
                            TotalGral = TotalGral + total;
                            count = count + 1;
                        }
                        else
                        {
                            ExcelFactVencida.Cell(count, 1).Value = registros.Id_Rem;
                            ExcelFactVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelFactVencida.Cell(count, 2).Value = registros.Rem_Fecha;
                            ExcelFactVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelFactVencida.Cell(count, 3).Value = registros.Id_Prd;
                            ExcelFactVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelFactVencida.Cell(count, 4).Value = registros.Prd_Descripcion;
                            ExcelFactVencida.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelFactVencida.Cell(count, 5).Value = registros.SaldoUnidades;
                            ExcelFactVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            double total = registros.SaldoUnidades * registros.Prd_Pesos;
                            ExcelFactVencida.Cell(count, 6).Value = total.ToString("C2");
                            ExcelFactVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                            totalCte = totalCte + total;
                            TotalMAtriz = TotalMAtriz + total;
                            TotalGral = TotalGral + total;
                            count = count + 1;
                        }
                    }
                }

                if (TotalMAtriz != 0)
                {
                    ExcelFactVencida.Cell(count, 5).Value = "Total por Cliente";
                    ExcelFactVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    ExcelFactVencida.Cell(count, 6).Value = totalCte.ToString("C2");
                    ExcelFactVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    totalCte = 0;

                    ExcelFactVencida.Cell(count, 5).Value = "Total por Territorio";
                    ExcelFactVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    ExcelFactVencida.Cell(count, 6).Value = TotalMAtriz.ToString("C2");
                    ExcelFactVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    TotalMAtriz = 0;

                    ExcelFactVencida.Cell(count, 5).Value = "Total";
                    ExcelFactVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    ExcelFactVencida.Cell(count, 6).Value = TotalGral.ToString("C2");
                    ExcelFactVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    TotalGral = 0;
                }

                #endregion
                ExcelFactVencida.Columns().AdjustToContents();
                ExcelFactVencida.PageSetup.PageOrientation = XLPageOrientation.Portrait;
                ExcelFactVencida.PageSetup.Margins.Top = 0.5;
                ExcelFactVencida.PageSetup.Margins.Bottom = 0.3;
                ExcelFactVencida.PageSetup.Margins.Left = 0.5;
                ExcelFactVencida.PageSetup.Margins.Right = 0.3;
                ExcelFactVencida.PageSetup.Margins.Footer = 0;
                ExcelFactVencida.PageSetup.Margins.Header = 0;
                ExcelFactVencida.PageSetup.PagesWide = 1;
                ExcelFactVencida.PageSetup.PaperSize = XLPaperSize.A4Paper;
                ExcelFactVencida.PageSetup.VerticalDpi = 600;
                ExcelFactVencida.PageSetup.HorizontalDpi = 600;
                #endregion   
                //Reporte de prueba Vigente
                #region MovPrueVigente

                ReporteRemisiones DatosPrueVigente = new ReporteRemisiones();
                List<ReporteRemisiones> listaPrueVigente = new List<ReporteRemisiones>();
                DatosPrueVigente.Id_Emp = Sesion.Id_Emp;
                DatosPrueVigente.Id_Cd = Sesion.Id_Cd_Ver;
                DatosPrueVigente.Vencido = 0;
                DatosPrueVigente.FechaIni = fechainicial;
                DatosPrueVigente.FechaFin = fechafinal;
                DatosPrueVigente.TipoRemision = 64;
                CN.Rep_RemisionesVencidas(DatosPrueVigente, ref listaPrueVigente, Sesion.Emp_Cnx);


                var ExcelPrueVigente = workbook.Worksheets.Add("PRUEBA VIGENTE");

                #region Encabezado

                ExcelPrueVigente.Cell(1, 1).Value = "Sucursal";
                ExcelPrueVigente.Cell(1, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelPrueVigente.Cell(1, 2).Value = Sesion.Id_Cd_Ver + " - " + Sesion.Cd_Nombre;
                ExcelPrueVigente.Cell(1, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelPrueVigente.Cell(1, 4).Value = Emp_Nombre;
                ExcelPrueVigente.Cell(1, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelPrueVigente.Cell(2, 1).Value = "Usuario";
                ExcelPrueVigente.Cell(2, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelPrueVigente.Cell(2, 2).Value = Sesion.U_Nombre;
                ExcelPrueVigente.Cell(2, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelPrueVigente.Cell(2, 4).Value = "Control de Remisiones";
                ExcelPrueVigente.Cell(2, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelPrueVigente.Cell(3, 1).Value = "Fecha del Documento";
                ExcelPrueVigente.Cell(3, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelPrueVigente.Cell(3, 2).Value = DateTime.Now.ToString("dd/MM/yyy");
                ExcelPrueVigente.Cell(3, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelPrueVigente.Cell(4, 1).Value = "Fecha Inical";
                ExcelPrueVigente.Cell(4, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelPrueVigente.Cell(4, 2).Value = fechainicial.ToString("dd/MM/yyy"); ;
                ExcelPrueVigente.Cell(4, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelPrueVigente.Cell(5, 1).Value = "Fecha Final";
                ExcelPrueVigente.Cell(5, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelPrueVigente.Cell(5, 2).Value = fechafinal.ToString("dd/MM/yyy");
                ExcelPrueVigente.Cell(5, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelPrueVigente.Cell(6, 1).Value = "Cliente";
                ExcelPrueVigente.Cell(6, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelPrueVigente.Cell(6, 2).Value = "Todos";
                ExcelPrueVigente.Cell(6, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelPrueVigente.Cell(7, 1).Value = "Territorio";
                ExcelPrueVigente.Cell(7, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelPrueVigente.Cell(7, 2).Value = "Todos";
                ExcelPrueVigente.Cell(7, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelPrueVigente.Cell(8, 1).Value = "Vencidos";
                ExcelPrueVigente.Cell(8, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelPrueVigente.Cell(8, 2).Value = "NO";
                ExcelPrueVigente.Cell(8, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelPrueVigente.Cell(9, 1).Value = "Nivel";
                ExcelPrueVigente.Cell(9, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelPrueVigente.Cell(9, 2).Value = "Detalle";
                ExcelPrueVigente.Cell(9, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelPrueVigente.Cell(10, 1).Value = "Tipo de Remisión";
                ExcelPrueVigente.Cell(10, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelPrueVigente.Range(10, 2, 10, 4).Merge();
                ExcelPrueVigente.Range(10, 2, 10, 4).Value = "Prueba";
                ExcelPrueVigente.Range(10, 2, 10, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                #endregion

                #region Cuerpo

                ExcelPrueVigente.Cell(12, 1).Value = "Remisión";
                ExcelPrueVigente.Cell(12, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelPrueVigente.Cell(12, 2).Value = "Fecha";
                ExcelPrueVigente.Cell(12, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelPrueVigente.Cell(12, 3).Value = "Núm de Producto";
                ExcelPrueVigente.Cell(12, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelPrueVigente.Cell(12, 4).Value = "Descripción de Producto";
                ExcelPrueVigente.Cell(12, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelPrueVigente.Cell(12, 5).Value = "Unidades";
                ExcelPrueVigente.Cell(12, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelPrueVigente.Cell(12, 6).Value = "Importe";
                ExcelPrueVigente.Cell(12, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                count = 13;
                idMatriz = -1;
                id_cte = -1;
                totalCte = 0;
                TotalMAtriz = 0;
                TotalGral = 0;

                foreach (ReporteRemisiones registros in listaPrueVigente)
                {
                    if (idMatriz == -1)
                    {
                        idMatriz = registros.Id_Ter;
                        ExcelPrueVigente.Cell(count, 1).Value = "Territorio";
                        ExcelPrueVigente.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelPrueVigente.Cell(count, 2).Value = registros.Id_Ter;
                        ExcelPrueVigente.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelPrueVigente.Range(count, 3, count, 4).Merge();
                        ExcelPrueVigente.Cell(count, 3).Value = registros.Ter_Nombre;
                        ExcelPrueVigente.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                    }
                    if (id_cte == -1)
                    {
                        id_cte = registros.Id_Cte;

                        ExcelPrueVigente.Cell(count, 1).Value = "Cliente";
                        ExcelPrueVigente.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelPrueVigente.Cell(count, 2).Value = registros.Id_Cte;
                        ExcelPrueVigente.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelPrueVigente.Range(count, 3, count, 4).Merge();
                        ExcelPrueVigente.Cell(count, 3).Value = registros.Cte_NomComercial;
                        ExcelPrueVigente.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                    }

                    if (idMatriz != registros.Id_Ter)
                    {
                        ExcelPrueVigente.Cell(count, 5).Value = "Total por Cliente";
                        ExcelPrueVigente.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelPrueVigente.Cell(count, 6).Value = totalCte.ToString("C2");
                        ExcelPrueVigente.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                        totalCte = 0;

                        ExcelPrueVigente.Cell(count, 5).Value = "Total por Territorio";
                        ExcelPrueVigente.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelPrueVigente.Cell(count, 6).Value = TotalMAtriz.ToString("C2");
                        ExcelPrueVigente.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                        TotalMAtriz = 0;

                        idMatriz = registros.Id_Ter;
                        ExcelPrueVigente.Cell(count, 1).Value = "Territorio";
                        ExcelPrueVigente.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelPrueVigente.Cell(count, 2).Value = registros.Id_Ter;
                        ExcelPrueVigente.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelPrueVigente.Range(count, 3, count, 4).Merge();
                        ExcelPrueVigente.Cell(count, 3).Value = registros.Ter_Nombre;
                        ExcelPrueVigente.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;

                        id_cte = registros.Id_Cte;
                        ExcelPrueVigente.Cell(count, 1).Value = "Cliente";
                        ExcelPrueVigente.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelPrueVigente.Cell(count, 2).Value = registros.Id_Cte;
                        ExcelPrueVigente.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelPrueVigente.Range(count, 3, count, 4).Merge();
                        ExcelPrueVigente.Cell(count, 3).Value = registros.Cte_NomComercial;
                        ExcelPrueVigente.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;

                        ExcelPrueVigente.Cell(count, 1).Value = registros.Id_Rem;
                        ExcelPrueVigente.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelPrueVigente.Cell(count, 2).Value = registros.Rem_Fecha;
                        ExcelPrueVigente.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelPrueVigente.Cell(count, 3).Value = registros.Id_Prd;
                        ExcelPrueVigente.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelPrueVigente.Cell(count, 4).Value = registros.Prd_Descripcion;
                        ExcelPrueVigente.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelPrueVigente.Cell(count, 5).Value = registros.SaldoUnidades;
                        ExcelPrueVigente.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        double total = registros.SaldoUnidades * registros.Prd_Pesos;
                        ExcelPrueVigente.Cell(count, 6).Value = total.ToString("C2");
                        ExcelPrueVigente.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                        totalCte = totalCte + total;
                        TotalMAtriz = TotalMAtriz + total;
                        TotalGral = TotalGral + total;
                        count = count + 1;
                    }
                    else
                    {
                        if (id_cte != registros.Id_Cte)
                        {
                            ExcelPrueVigente.Cell(count, 5).Value = "Total por Cliente";
                            ExcelPrueVigente.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelPrueVigente.Cell(count, 6).Value = totalCte.ToString("C2");
                            ExcelPrueVigente.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            count = count + 1;
                            totalCte = 0;

                            id_cte = registros.Id_Cte;
                            ExcelPrueVigente.Cell(count, 1).Value = "Cliente";
                            ExcelPrueVigente.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelPrueVigente.Cell(count, 2).Value = registros.Id_Cte;
                            ExcelPrueVigente.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelPrueVigente.Range(count, 3, count, 4).Merge();
                            ExcelPrueVigente.Cell(count, 3).Value = registros.Cte_NomComercial;
                            ExcelPrueVigente.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            count = count + 1;

                            ExcelPrueVigente.Cell(count, 1).Value = registros.Id_Rem;
                            ExcelPrueVigente.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelPrueVigente.Cell(count, 2).Value = registros.Rem_Fecha;
                            ExcelPrueVigente.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelPrueVigente.Cell(count, 3).Value = registros.Id_Prd;
                            ExcelPrueVigente.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelPrueVigente.Cell(count, 4).Value = registros.Prd_Descripcion;
                            ExcelPrueVigente.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelPrueVigente.Cell(count, 5).Value = registros.SaldoUnidades;
                            ExcelPrueVigente.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            double total = registros.SaldoUnidades * registros.Prd_Pesos;
                            ExcelPrueVigente.Cell(count, 6).Value = total.ToString("C2");
                            ExcelPrueVigente.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                            totalCte = totalCte + total;
                            TotalMAtriz = TotalMAtriz + total;
                            TotalGral = TotalGral + total;
                            count = count + 1;
                        }
                        else
                        {
                            ExcelPrueVigente.Cell(count, 1).Value = registros.Id_Rem;
                            ExcelPrueVigente.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelPrueVigente.Cell(count, 2).Value = registros.Rem_Fecha;
                            ExcelPrueVigente.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelPrueVigente.Cell(count, 3).Value = registros.Id_Prd;
                            ExcelPrueVigente.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelPrueVigente.Cell(count, 4).Value = registros.Prd_Descripcion;
                            ExcelPrueVigente.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelPrueVigente.Cell(count, 5).Value = registros.SaldoUnidades;
                            ExcelPrueVigente.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            double total = registros.SaldoUnidades * registros.Prd_Pesos;
                            ExcelPrueVigente.Cell(count, 6).Value = total.ToString("C2");
                            ExcelPrueVigente.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                            totalCte = totalCte + total;
                            TotalMAtriz = TotalMAtriz + total;
                            TotalGral = TotalGral + total;
                            count = count + 1;
                        }
                    }
                }

                if (TotalMAtriz != 0)
                {
                    ExcelPrueVigente.Cell(count, 5).Value = "Total por Cliente";
                    ExcelPrueVigente.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    ExcelPrueVigente.Cell(count, 6).Value = totalCte.ToString("C2");
                    ExcelPrueVigente.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    totalCte = 0;

                    ExcelPrueVigente.Cell(count, 5).Value = "Total por Territorio";
                    ExcelPrueVigente.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    ExcelPrueVigente.Cell(count, 6).Value = TotalMAtriz.ToString("C2");
                    ExcelPrueVigente.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    TotalMAtriz = 0;

                    ExcelPrueVigente.Cell(count, 5).Value = "Total";
                    ExcelPrueVigente.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    ExcelPrueVigente.Cell(count, 6).Value = TotalGral.ToString("C2");
                    ExcelPrueVigente.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    TotalGral = 0;
                }

                #endregion
                ExcelPrueVigente.Columns().AdjustToContents();
                ExcelPrueVigente.PageSetup.PageOrientation = XLPageOrientation.Portrait;
                ExcelPrueVigente.PageSetup.Margins.Top = 0.5;
                ExcelPrueVigente.PageSetup.Margins.Bottom = 0.3;
                ExcelPrueVigente.PageSetup.Margins.Left = 0.5;
                ExcelPrueVigente.PageSetup.Margins.Right = 0.3;
                ExcelPrueVigente.PageSetup.Margins.Footer = 0;
                ExcelPrueVigente.PageSetup.Margins.Header = 0;
                ExcelPrueVigente.PageSetup.PagesWide = 1;
                ExcelPrueVigente.PageSetup.PaperSize = XLPaperSize.A4Paper;
                ExcelPrueVigente.PageSetup.VerticalDpi = 600;
                ExcelPrueVigente.PageSetup.HorizontalDpi = 600;
                #endregion 
                //Reporte de prueba Vencidas
                #region MovPrueVencida

                ReporteRemisiones DatosPrueVencida = new ReporteRemisiones();
                List<ReporteRemisiones> listaPrueVencida = new List<ReporteRemisiones>();
                DatosPrueVencida.Id_Emp = Sesion.Id_Emp;
                DatosPrueVencida.Id_Cd = Sesion.Id_Cd_Ver;
                DatosPrueVencida.Vencido = 1;
                DatosPrueVencida.FechaIni = fechainicial;
                DatosPrueVencida.FechaFin = fechafinal;
                DatosPrueVencida.TipoRemision = 64;
                CN.Rep_RemisionesVencidas(DatosPrueVencida, ref listaPrueVencida, Sesion.Emp_Cnx);


                var ExcelPrueVencida = workbook.Worksheets.Add("PRUEBA VENCIDA");

                #region Encabezado

                ExcelPrueVencida.Cell(1, 1).Value = "Sucursal";
                ExcelPrueVencida.Cell(1, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelPrueVencida.Cell(1, 2).Value = Sesion.Id_Cd_Ver + " - " + Sesion.Cd_Nombre;
                ExcelPrueVencida.Cell(1, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelPrueVencida.Cell(1, 4).Value = Emp_Nombre;
                ExcelPrueVencida.Cell(1, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelPrueVencida.Cell(2, 1).Value = "Usuario";
                ExcelPrueVencida.Cell(2, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelPrueVencida.Cell(2, 2).Value = Sesion.U_Nombre;
                ExcelPrueVencida.Cell(2, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelPrueVencida.Cell(2, 4).Value = "Control de Remisiones";
                ExcelPrueVencida.Cell(2, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelPrueVencida.Cell(3, 1).Value = "Fecha del Documento";
                ExcelPrueVencida.Cell(3, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelPrueVencida.Cell(3, 2).Value = DateTime.Now.ToString("dd/MM/yyy");
                ExcelPrueVencida.Cell(3, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelPrueVencida.Cell(4, 1).Value = "Fecha Inical";
                ExcelPrueVencida.Cell(4, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelPrueVencida.Cell(4, 2).Value = fechainicial.ToString("dd/MM/yyy"); ;
                ExcelPrueVencida.Cell(4, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelPrueVencida.Cell(5, 1).Value = "Fecha Final";
                ExcelPrueVencida.Cell(5, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelPrueVencida.Cell(5, 2).Value = fechafinal.ToString("dd/MM/yyy");
                ExcelPrueVencida.Cell(5, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelPrueVencida.Cell(6, 1).Value = "Cliente";
                ExcelPrueVencida.Cell(6, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelPrueVencida.Cell(6, 2).Value = "Todos";
                ExcelPrueVencida.Cell(6, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelPrueVencida.Cell(7, 1).Value = "Territorio";
                ExcelPrueVencida.Cell(7, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelPrueVencida.Cell(7, 2).Value = "Todos";
                ExcelPrueVencida.Cell(7, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelPrueVencida.Cell(8, 1).Value = "Vencidos";
                ExcelPrueVencida.Cell(8, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelPrueVencida.Cell(8, 2).Value = "SI";
                ExcelPrueVencida.Cell(8, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelPrueVencida.Cell(9, 1).Value = "Nivel";
                ExcelPrueVencida.Cell(9, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelPrueVencida.Cell(9, 2).Value = "Detalle";
                ExcelPrueVencida.Cell(9, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelPrueVencida.Cell(10, 1).Value = "Tipo de Remisión";
                ExcelPrueVencida.Cell(10, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelPrueVencida.Range(10, 2, 10, 4).Merge();
                ExcelPrueVencida.Range(10, 2, 10, 4).Value = "Prueba";
                ExcelPrueVencida.Range(10, 2, 10, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                #endregion

                #region Cuerpo

                ExcelPrueVencida.Cell(12, 1).Value = "Remisión";
                ExcelPrueVencida.Cell(12, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelPrueVencida.Cell(12, 2).Value = "Fecha";
                ExcelPrueVencida.Cell(12, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelPrueVencida.Cell(12, 3).Value = "Núm de Producto";
                ExcelPrueVencida.Cell(12, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelPrueVencida.Cell(12, 4).Value = "Descripción de Producto";
                ExcelPrueVencida.Cell(12, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelPrueVencida.Cell(12, 5).Value = "Unidades";
                ExcelPrueVencida.Cell(12, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelPrueVencida.Cell(12, 6).Value = "Importe";
                ExcelPrueVencida.Cell(12, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                count = 13;
                idMatriz = -1;
                id_cte = -1;
                totalCte = 0;
                TotalMAtriz = 0;
                TotalGral = 0;

                foreach (ReporteRemisiones registros in listaPrueVencida)
                {
                    if (idMatriz == -1)
                    {
                        idMatriz = registros.Id_Ter;
                        ExcelPrueVencida.Cell(count, 1).Value = "Territorio";
                        ExcelPrueVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelPrueVencida.Cell(count, 2).Value = registros.Id_Ter;
                        ExcelPrueVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelPrueVencida.Range(count, 3, count, 4).Merge();
                        ExcelPrueVencida.Cell(count, 3).Value = registros.Ter_Nombre;
                        ExcelPrueVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                    }
                    if (id_cte == -1)
                    {
                        id_cte = registros.Id_Cte;

                        ExcelPrueVencida.Cell(count, 1).Value = "Cliente";
                        ExcelPrueVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelPrueVencida.Cell(count, 2).Value = registros.Id_Cte;
                        ExcelPrueVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelPrueVencida.Range(count, 3, count, 4).Merge();
                        ExcelPrueVencida.Cell(count, 3).Value = registros.Cte_NomComercial;
                        ExcelPrueVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                    }

                    if (idMatriz != registros.Id_Ter)
                    {
                        ExcelPrueVencida.Cell(count, 5).Value = "Total por Cliente";
                        ExcelPrueVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelPrueVencida.Cell(count, 6).Value = totalCte.ToString("C2");
                        ExcelPrueVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                        totalCte = 0;

                        ExcelPrueVencida.Cell(count, 5).Value = "Total por Territorio";
                        ExcelPrueVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelPrueVencida.Cell(count, 6).Value = TotalMAtriz.ToString("C2");
                        ExcelPrueVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                        TotalMAtriz = 0;

                        idMatriz = registros.Id_Ter;
                        ExcelPrueVencida.Cell(count, 1).Value = "Territorio";
                        ExcelPrueVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelPrueVencida.Cell(count, 2).Value = registros.Id_Ter;
                        ExcelPrueVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelPrueVencida.Range(count, 3, count, 4).Merge();
                        ExcelPrueVencida.Cell(count, 3).Value = registros.Ter_Nombre;
                        ExcelPrueVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;

                        id_cte = registros.Id_Cte;
                        ExcelPrueVencida.Cell(count, 1).Value = "Cliente";
                        ExcelPrueVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelPrueVencida.Cell(count, 2).Value = registros.Id_Cte;
                        ExcelPrueVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelPrueVencida.Range(count, 3, count, 4).Merge();
                        ExcelPrueVencida.Cell(count, 3).Value = registros.Cte_NomComercial;
                        ExcelPrueVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;

                        ExcelPrueVencida.Cell(count, 1).Value = registros.Id_Rem;
                        ExcelPrueVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelPrueVencida.Cell(count, 2).Value = registros.Rem_Fecha;
                        ExcelPrueVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelPrueVencida.Cell(count, 3).Value = registros.Id_Prd;
                        ExcelPrueVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelPrueVencida.Cell(count, 4).Value = registros.Prd_Descripcion;
                        ExcelPrueVencida.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelPrueVencida.Cell(count, 5).Value = registros.SaldoUnidades;
                        ExcelPrueVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        double total = registros.SaldoUnidades * registros.Prd_Pesos;
                        ExcelPrueVencida.Cell(count, 6).Value = total.ToString("C2");
                        ExcelPrueVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                        totalCte = totalCte + total;
                        TotalMAtriz = TotalMAtriz + total;
                        TotalGral = TotalGral + total;
                        count = count + 1;
                    }
                    else
                    {
                        if (id_cte != registros.Id_Cte)
                        {
                            ExcelPrueVencida.Cell(count, 5).Value = "Total por Cliente";
                            ExcelPrueVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelPrueVencida.Cell(count, 6).Value = totalCte.ToString("C2");
                            ExcelPrueVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            count = count + 1;
                            totalCte = 0;

                            id_cte = registros.Id_Cte;
                            ExcelPrueVencida.Cell(count, 1).Value = "Cliente";
                            ExcelPrueVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelPrueVencida.Cell(count, 2).Value = registros.Id_Cte;
                            ExcelPrueVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelPrueVencida.Range(count, 3, count, 4).Merge();
                            ExcelPrueVencida.Cell(count, 3).Value = registros.Cte_NomComercial;
                            ExcelPrueVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            count = count + 1;

                            ExcelPrueVencida.Cell(count, 1).Value = registros.Id_Rem;
                            ExcelPrueVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelPrueVencida.Cell(count, 2).Value = registros.Rem_Fecha;
                            ExcelPrueVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelPrueVencida.Cell(count, 3).Value = registros.Id_Prd;
                            ExcelPrueVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelPrueVencida.Cell(count, 4).Value = registros.Prd_Descripcion;
                            ExcelPrueVencida.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelPrueVencida.Cell(count, 5).Value = registros.SaldoUnidades;
                            ExcelPrueVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            double total = registros.SaldoUnidades * registros.Prd_Pesos;
                            ExcelPrueVencida.Cell(count, 6).Value = total.ToString("C2");
                            ExcelPrueVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                            totalCte = totalCte + total;
                            TotalMAtriz = TotalMAtriz + total;
                            TotalGral = TotalGral + total;
                            count = count + 1;
                        }
                        else
                        {
                            ExcelPrueVencida.Cell(count, 1).Value = registros.Id_Rem;
                            ExcelPrueVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelPrueVencida.Cell(count, 2).Value = registros.Rem_Fecha;
                            ExcelPrueVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelPrueVencida.Cell(count, 3).Value = registros.Id_Prd;
                            ExcelPrueVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelPrueVencida.Cell(count, 4).Value = registros.Prd_Descripcion;
                            ExcelPrueVencida.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelPrueVencida.Cell(count, 5).Value = registros.SaldoUnidades;
                            ExcelPrueVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            double total = registros.SaldoUnidades * registros.Prd_Pesos;
                            ExcelPrueVencida.Cell(count, 6).Value = total.ToString("C2");
                            ExcelPrueVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                            totalCte = totalCte + total;
                            TotalMAtriz = TotalMAtriz + total;
                            TotalGral = TotalGral + total;
                            count = count + 1;
                        }
                    }
                }

                if (TotalMAtriz != 0)
                {
                    ExcelPrueVencida.Cell(count, 5).Value = "Total por Cliente";
                    ExcelPrueVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    ExcelPrueVencida.Cell(count, 6).Value = totalCte.ToString("C2");
                    ExcelPrueVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    totalCte = 0;

                    ExcelPrueVencida.Cell(count, 5).Value = "Total por Territorio";
                    ExcelPrueVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    ExcelPrueVencida.Cell(count, 6).Value = TotalMAtriz.ToString("C2");
                    ExcelPrueVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    TotalMAtriz = 0;

                    ExcelPrueVencida.Cell(count, 5).Value = "Total";
                    ExcelPrueVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    ExcelPrueVencida.Cell(count, 6).Value = TotalGral.ToString("C2");
                    ExcelPrueVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    TotalGral = 0;
                }

                #endregion
                ExcelPrueVencida.Columns().AdjustToContents();
                ExcelPrueVencida.PageSetup.PageOrientation = XLPageOrientation.Portrait;
                ExcelPrueVencida.PageSetup.Margins.Top = 0.5;
                ExcelPrueVencida.PageSetup.Margins.Bottom = 0.3;
                ExcelPrueVencida.PageSetup.Margins.Left = 0.5;
                ExcelPrueVencida.PageSetup.Margins.Right = 0.3;
                ExcelPrueVencida.PageSetup.Margins.Footer = 0;
                ExcelPrueVencida.PageSetup.Margins.Header = 0;
                ExcelPrueVencida.PageSetup.PagesWide = 1;
                ExcelPrueVencida.PageSetup.PaperSize = XLPaperSize.A4Paper;
                ExcelPrueVencida.PageSetup.VerticalDpi = 600;
                ExcelPrueVencida.PageSetup.HorizontalDpi = 600;
                #endregion
                //Reporte de No conformes Vigente
                #region MovNCVIGENTE

                ReporteRemisiones DatosNCVIGENTE = new ReporteRemisiones();
                List<ReporteRemisiones> listaNCVIGENTE = new List<ReporteRemisiones>();
                DatosNCVIGENTE.Id_Emp = Sesion.Id_Emp;
                DatosNCVIGENTE.Id_Cd = Sesion.Id_Cd_Ver;
                DatosNCVIGENTE.Vencido = 0;
                DatosNCVIGENTE.FechaIni = fechainicial;
                DatosNCVIGENTE.FechaFin = fechafinal;
                DatosNCVIGENTE.TipoRemision = 65;
                CN.Rep_RemisionesVencidas(DatosNCVIGENTE, ref listaNCVIGENTE, Sesion.Emp_Cnx);


                var ExcelNCVIGENTE = workbook.Worksheets.Add("NC VIGENTE");

                #region Encabezado

                ExcelNCVIGENTE.Cell(1, 1).Value = "Sucursal";
                ExcelNCVIGENTE.Cell(1, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNCVIGENTE.Cell(1, 2).Value = Sesion.Id_Cd_Ver + " - " + Sesion.Cd_Nombre;
                ExcelNCVIGENTE.Cell(1, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNCVIGENTE.Cell(1, 4).Value = Emp_Nombre;
                ExcelNCVIGENTE.Cell(1, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelNCVIGENTE.Cell(2, 1).Value = "Usuario";
                ExcelNCVIGENTE.Cell(2, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNCVIGENTE.Cell(2, 2).Value = Sesion.U_Nombre;
                ExcelNCVIGENTE.Cell(2, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNCVIGENTE.Cell(2, 4).Value = "Control de Remisiones";
                ExcelNCVIGENTE.Cell(2, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelNCVIGENTE.Cell(3, 1).Value = "Fecha del Documento";
                ExcelNCVIGENTE.Cell(3, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNCVIGENTE.Cell(3, 2).Value = DateTime.Now.ToString("dd/MM/yyy");
                ExcelNCVIGENTE.Cell(3, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelNCVIGENTE.Cell(4, 1).Value = "Fecha Inical";
                ExcelNCVIGENTE.Cell(4, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNCVIGENTE.Cell(4, 2).Value = fechainicial.ToString("dd/MM/yyy"); ;
                ExcelNCVIGENTE.Cell(4, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelNCVIGENTE.Cell(5, 1).Value = "Fecha Final";
                ExcelNCVIGENTE.Cell(5, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNCVIGENTE.Cell(5, 2).Value = fechafinal.ToString("dd/MM/yyy");
                ExcelNCVIGENTE.Cell(5, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelNCVIGENTE.Cell(6, 1).Value = "Cliente";
                ExcelNCVIGENTE.Cell(6, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNCVIGENTE.Cell(6, 2).Value = "Todos";
                ExcelNCVIGENTE.Cell(6, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelNCVIGENTE.Cell(7, 1).Value = "Territorio";
                ExcelNCVIGENTE.Cell(7, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNCVIGENTE.Cell(7, 2).Value = "Todos";
                ExcelNCVIGENTE.Cell(7, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelNCVIGENTE.Cell(8, 1).Value = "Vencidos";
                ExcelNCVIGENTE.Cell(8, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNCVIGENTE.Cell(8, 2).Value = "NO";
                ExcelNCVIGENTE.Cell(8, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelNCVIGENTE.Cell(9, 1).Value = "Nivel";
                ExcelNCVIGENTE.Cell(9, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNCVIGENTE.Cell(9, 2).Value = "Detalle";
                ExcelNCVIGENTE.Cell(9, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelNCVIGENTE.Cell(10, 1).Value = "Tipo de Remisión";
                ExcelNCVIGENTE.Cell(10, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNCVIGENTE.Range(10, 2, 10, 4).Merge();
                ExcelNCVIGENTE.Range(10, 2, 10, 4).Value = "Producto no conforme";
                ExcelNCVIGENTE.Range(10, 2, 10, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                #endregion

                #region Cuerpo

                ExcelNCVIGENTE.Cell(12, 1).Value = "Remisión";
                ExcelNCVIGENTE.Cell(12, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelNCVIGENTE.Cell(12, 2).Value = "Fecha";
                ExcelNCVIGENTE.Cell(12, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelNCVIGENTE.Cell(12, 3).Value = "Núm de Producto";
                ExcelNCVIGENTE.Cell(12, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelNCVIGENTE.Cell(12, 4).Value = "Descripción de Producto";
                ExcelNCVIGENTE.Cell(12, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelNCVIGENTE.Cell(12, 5).Value = "Unidades";
                ExcelNCVIGENTE.Cell(12, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelNCVIGENTE.Cell(12, 6).Value = "Importe";
                ExcelNCVIGENTE.Cell(12, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                count = 13;
                idMatriz = -1;
                id_cte = -1;
                totalCte = 0;
                TotalMAtriz = 0;
                TotalGral = 0;

                foreach (ReporteRemisiones registros in listaNCVIGENTE)
                {
                    if (idMatriz == -1)
                    {
                        idMatriz = registros.Id_Ter;
                        ExcelNCVIGENTE.Cell(count, 1).Value = "Territorio";
                        ExcelNCVIGENTE.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNCVIGENTE.Cell(count, 2).Value = registros.Id_Ter;
                        ExcelNCVIGENTE.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNCVIGENTE.Range(count, 3, count, 4).Merge();
                        ExcelNCVIGENTE.Cell(count, 3).Value = registros.Ter_Nombre;
                        ExcelNCVIGENTE.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                    }
                    if (id_cte == -1)
                    {
                        id_cte = registros.Id_Cte;

                        ExcelNCVIGENTE.Cell(count, 1).Value = "Cliente";
                        ExcelNCVIGENTE.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNCVIGENTE.Cell(count, 2).Value = registros.Id_Cte;
                        ExcelNCVIGENTE.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNCVIGENTE.Range(count, 3, count, 4).Merge();
                        ExcelNCVIGENTE.Cell(count, 3).Value = registros.Cte_NomComercial;
                        ExcelNCVIGENTE.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                    }

                    if (idMatriz != registros.Id_Ter)
                    {
                        ExcelNCVIGENTE.Cell(count, 5).Value = "Total por Cliente";
                        ExcelNCVIGENTE.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNCVIGENTE.Cell(count, 6).Value = totalCte.ToString("C2");
                        ExcelNCVIGENTE.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                        totalCte = 0;

                        ExcelNCVIGENTE.Cell(count, 5).Value = "Total por Territorio";
                        ExcelNCVIGENTE.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNCVIGENTE.Cell(count, 6).Value = TotalMAtriz.ToString("C2");
                        ExcelNCVIGENTE.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                        TotalMAtriz = 0;

                        idMatriz = registros.Id_Ter;
                        ExcelNCVIGENTE.Cell(count, 1).Value = "Territorio";
                        ExcelNCVIGENTE.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNCVIGENTE.Cell(count, 2).Value = registros.Id_Ter;
                        ExcelNCVIGENTE.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNCVIGENTE.Range(count, 3, count, 4).Merge();
                        ExcelNCVIGENTE.Cell(count, 3).Value = registros.Ter_Nombre;
                        ExcelNCVIGENTE.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;

                        id_cte = registros.Id_Cte;
                        ExcelNCVIGENTE.Cell(count, 1).Value = "Cliente";
                        ExcelNCVIGENTE.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNCVIGENTE.Cell(count, 2).Value = registros.Id_Cte;
                        ExcelNCVIGENTE.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNCVIGENTE.Range(count, 3, count, 4).Merge();
                        ExcelNCVIGENTE.Cell(count, 3).Value = registros.Cte_NomComercial;
                        ExcelNCVIGENTE.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;

                        ExcelNCVIGENTE.Cell(count, 1).Value = registros.Id_Rem;
                        ExcelNCVIGENTE.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNCVIGENTE.Cell(count, 2).Value = registros.Rem_Fecha;
                        ExcelNCVIGENTE.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNCVIGENTE.Cell(count, 3).Value = registros.Id_Prd;
                        ExcelNCVIGENTE.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNCVIGENTE.Cell(count, 4).Value = registros.Prd_Descripcion;
                        ExcelNCVIGENTE.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNCVIGENTE.Cell(count, 5).Value = registros.SaldoUnidades;
                        ExcelNCVIGENTE.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        double total = registros.SaldoUnidades * registros.Prd_Pesos;
                        ExcelNCVIGENTE.Cell(count, 6).Value = total.ToString("C2");
                        ExcelNCVIGENTE.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                        totalCte = totalCte + total;
                        TotalMAtriz = TotalMAtriz + total;
                        TotalGral = TotalGral + total;
                        count = count + 1;
                    }
                    else
                    {
                        if (id_cte != registros.Id_Cte)
                        {
                            ExcelNCVIGENTE.Cell(count, 5).Value = "Total por Cliente";
                            ExcelNCVIGENTE.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNCVIGENTE.Cell(count, 6).Value = totalCte.ToString("C2");
                            ExcelNCVIGENTE.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            count = count + 1;
                            totalCte = 0;

                            id_cte = registros.Id_Cte;
                            ExcelNCVIGENTE.Cell(count, 1).Value = "Cliente";
                            ExcelNCVIGENTE.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNCVIGENTE.Cell(count, 2).Value = registros.Id_Cte;
                            ExcelNCVIGENTE.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNCVIGENTE.Range(count, 3, count, 4).Merge();
                            ExcelNCVIGENTE.Cell(count, 3).Value = registros.Cte_NomComercial;
                            ExcelNCVIGENTE.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            count = count + 1;

                            ExcelNCVIGENTE.Cell(count, 1).Value = registros.Id_Rem;
                            ExcelNCVIGENTE.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNCVIGENTE.Cell(count, 2).Value = registros.Rem_Fecha;
                            ExcelNCVIGENTE.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNCVIGENTE.Cell(count, 3).Value = registros.Id_Prd;
                            ExcelNCVIGENTE.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNCVIGENTE.Cell(count, 4).Value = registros.Prd_Descripcion;
                            ExcelNCVIGENTE.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNCVIGENTE.Cell(count, 5).Value = registros.SaldoUnidades;
                            ExcelNCVIGENTE.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            double total = registros.SaldoUnidades * registros.Prd_Pesos;
                            ExcelNCVIGENTE.Cell(count, 6).Value = total.ToString("C2");
                            ExcelNCVIGENTE.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                            totalCte = totalCte + total;
                            TotalMAtriz = TotalMAtriz + total;
                            TotalGral = TotalGral + total;
                            count = count + 1;
                        }
                        else
                        {
                            ExcelNCVIGENTE.Cell(count, 1).Value = registros.Id_Rem;
                            ExcelNCVIGENTE.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNCVIGENTE.Cell(count, 2).Value = registros.Rem_Fecha;
                            ExcelNCVIGENTE.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNCVIGENTE.Cell(count, 3).Value = registros.Id_Prd;
                            ExcelNCVIGENTE.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNCVIGENTE.Cell(count, 4).Value = registros.Prd_Descripcion;
                            ExcelNCVIGENTE.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNCVIGENTE.Cell(count, 5).Value = registros.SaldoUnidades;
                            ExcelNCVIGENTE.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            double total = registros.SaldoUnidades * registros.Prd_Pesos;
                            ExcelNCVIGENTE.Cell(count, 6).Value = total.ToString("C2");
                            ExcelNCVIGENTE.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                            totalCte = totalCte + total;
                            TotalMAtriz = TotalMAtriz + total;
                            TotalGral = TotalGral + total;
                            count = count + 1;
                        }
                    }
                }

                if (TotalMAtriz != 0)
                {
                    ExcelNCVIGENTE.Cell(count, 5).Value = "Total por Cliente";
                    ExcelNCVIGENTE.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    ExcelNCVIGENTE.Cell(count, 6).Value = totalCte.ToString("C2");
                    ExcelNCVIGENTE.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    totalCte = 0;

                    ExcelNCVIGENTE.Cell(count, 5).Value = "Total por Territorio";
                    ExcelNCVIGENTE.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    ExcelNCVIGENTE.Cell(count, 6).Value = TotalMAtriz.ToString("C2");
                    ExcelNCVIGENTE.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    TotalMAtriz = 0;


                    ExcelNCVIGENTE.Cell(count, 5).Value = "Total";
                    ExcelNCVIGENTE.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    ExcelNCVIGENTE.Cell(count, 6).Value = TotalGral.ToString("C2");
                    ExcelNCVIGENTE.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    TotalGral = 0;
                }

                #endregion
                ExcelNCVIGENTE.Columns().AdjustToContents();

                ExcelNCVIGENTE.PageSetup.PageOrientation = XLPageOrientation.Portrait;
                ExcelNCVIGENTE.PageSetup.Margins.Top = 0.5;
                ExcelNCVIGENTE.PageSetup.Margins.Bottom = 0.3;
                ExcelNCVIGENTE.PageSetup.Margins.Left = 0.5;
                ExcelNCVIGENTE.PageSetup.Margins.Right = 0.3;
                ExcelNCVIGENTE.PageSetup.Margins.Footer = 0;
                ExcelNCVIGENTE.PageSetup.Margins.Header = 0;
                ExcelNCVIGENTE.PageSetup.PagesWide = 1;
                ExcelNCVIGENTE.PageSetup.PaperSize = XLPaperSize.A4Paper;
                ExcelNCVIGENTE.PageSetup.VerticalDpi = 600;
                ExcelNCVIGENTE.PageSetup.HorizontalDpi = 600;
                #endregion
                //Reporte de No Conforme Vencidas
                #region MovNCVencida

                ReporteRemisiones DatosNCVencida = new ReporteRemisiones();
                List<ReporteRemisiones> listaNCVencida = new List<ReporteRemisiones>();
                DatosNCVencida.Id_Emp = Sesion.Id_Emp;
                DatosNCVencida.Id_Cd = Sesion.Id_Cd_Ver;
                DatosNCVencida.Vencido = 1;
                DatosNCVencida.FechaIni = fechainicial;
                DatosNCVencida.FechaFin = fechafinal;
                DatosNCVencida.TipoRemision = 65;
                CN.Rep_RemisionesVencidas(DatosNCVencida, ref listaNCVencida, Sesion.Emp_Cnx);


                var ExcelNCVencida = workbook.Worksheets.Add("NC VENCIDA");

                #region Encabezado

                ExcelNCVencida.Cell(1, 1).Value = "Sucursal";
                ExcelNCVencida.Cell(1, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNCVencida.Cell(1, 2).Value = Sesion.Id_Cd_Ver + " - " + Sesion.Cd_Nombre;
                ExcelNCVencida.Cell(1, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNCVencida.Cell(1, 4).Value = Emp_Nombre;
                ExcelNCVencida.Cell(1, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelNCVencida.Cell(2, 1).Value = "Usuario";
                ExcelNCVencida.Cell(2, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNCVencida.Cell(2, 2).Value = Sesion.U_Nombre;
                ExcelNCVencida.Cell(2, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNCVencida.Cell(2, 4).Value = "Control de Remisiones";
                ExcelNCVencida.Cell(2, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelNCVencida.Cell(3, 1).Value = "Fecha del Documento";
                ExcelNCVencida.Cell(3, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNCVencida.Cell(3, 2).Value = DateTime.Now.ToString("dd/MM/yyy");
                ExcelNCVencida.Cell(3, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelNCVencida.Cell(4, 1).Value = "Fecha Inical";
                ExcelNCVencida.Cell(4, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNCVencida.Cell(4, 2).Value = fechainicial.ToString("dd/MM/yyy"); ;
                ExcelNCVencida.Cell(4, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelNCVencida.Cell(5, 1).Value = "Fecha Final";
                ExcelNCVencida.Cell(5, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNCVencida.Cell(5, 2).Value = fechafinal.ToString("dd/MM/yyy");
                ExcelNCVencida.Cell(5, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelNCVencida.Cell(6, 1).Value = "Cliente";
                ExcelNCVencida.Cell(6, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNCVencida.Cell(6, 2).Value = "Todos";
                ExcelNCVencida.Cell(6, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelNCVencida.Cell(7, 1).Value = "Territorio";
                ExcelNCVencida.Cell(7, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNCVencida.Cell(7, 2).Value = "Todos";
                ExcelNCVencida.Cell(7, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelNCVencida.Cell(8, 1).Value = "Vencidos";
                ExcelNCVencida.Cell(8, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNCVencida.Cell(8, 2).Value = "SI";
                ExcelNCVencida.Cell(8, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelNCVencida.Cell(9, 1).Value = "Nivel";
                ExcelNCVencida.Cell(9, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNCVencida.Cell(9, 2).Value = "Detalle";
                ExcelNCVencida.Cell(9, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelNCVencida.Cell(10, 1).Value = "Tipo de Remisión";
                ExcelNCVencida.Cell(10, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNCVencida.Range(10, 2, 10, 4).Merge();
                ExcelNCVencida.Range(10, 2, 10, 4).Value = "Producto no conforme";
                ExcelNCVencida.Range(10, 2, 10, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                #endregion

                #region Cuerpo

                ExcelNCVencida.Cell(12, 1).Value = "Remisión";
                ExcelNCVencida.Cell(12, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelNCVencida.Cell(12, 2).Value = "Fecha";
                ExcelNCVencida.Cell(12, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelNCVencida.Cell(12, 3).Value = "Núm de Producto";
                ExcelNCVencida.Cell(12, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelNCVencida.Cell(12, 4).Value = "Descripción de Producto";
                ExcelNCVencida.Cell(12, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelNCVencida.Cell(12, 5).Value = "Unidades";
                ExcelNCVencida.Cell(12, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelNCVencida.Cell(12, 6).Value = "Importe";
                ExcelNCVencida.Cell(12, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                count = 13;
                idMatriz = -1;
                id_cte = -1;
                totalCte = 0;
                TotalGral = 0;
                TotalMAtriz = 0;

                foreach (ReporteRemisiones registros in listaNCVencida)
                {
                    if (idMatriz == -1)
                    {
                        idMatriz = registros.Id_Ter;
                        ExcelNCVencida.Cell(count, 1).Value = "Territorio";
                        ExcelNCVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNCVencida.Cell(count, 2).Value = registros.Id_Ter;
                        ExcelNCVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNCVencida.Range(count, 3, count, 4).Merge();
                        ExcelNCVencida.Cell(count, 3).Value = registros.Ter_Nombre;
                        ExcelNCVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                    }
                    if (id_cte == -1)
                    {
                        id_cte = registros.Id_Cte;

                        ExcelNCVencida.Cell(count, 1).Value = "Cliente";
                        ExcelNCVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNCVencida.Cell(count, 2).Value = registros.Id_Cte;
                        ExcelNCVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNCVencida.Range(count, 3, count, 4).Merge();
                        ExcelNCVencida.Cell(count, 3).Value = registros.Cte_NomComercial;
                        ExcelNCVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                    }

                    if (idMatriz != registros.Id_Ter)
                    {
                        ExcelNCVencida.Cell(count, 5).Value = "Total por Cliente";
                        ExcelNCVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNCVencida.Cell(count, 6).Value = totalCte.ToString("C2");
                        ExcelNCVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                        totalCte = 0;

                        ExcelNCVencida.Cell(count, 5).Value = "Total por Territorio";
                        ExcelNCVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNCVencida.Cell(count, 6).Value = TotalMAtriz.ToString("C2");
                        ExcelNCVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                        TotalMAtriz = 0;

                        idMatriz = registros.Id_Ter;
                        ExcelNCVencida.Cell(count, 1).Value = "Territorio";
                        ExcelNCVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNCVencida.Cell(count, 2).Value = registros.Id_Ter;
                        ExcelNCVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                        ExcelNCVencida.Cell(count, 3).Value = registros.Ter_Nombre;
                        ExcelNCVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;

                        id_cte = registros.Id_Cte;
                        ExcelNCVencida.Cell(count, 1).Value = "Cliente";
                        ExcelNCVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNCVencida.Cell(count, 2).Value = registros.Id_Cte;
                        ExcelNCVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNCVencida.Range(count, 3, count, 4).Merge();
                        ExcelNCVencida.Cell(count, 3).Value = registros.Cte_NomComercial;
                        ExcelNCVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;

                        ExcelNCVencida.Cell(count, 1).Value = registros.Id_Rem;
                        ExcelNCVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNCVencida.Cell(count, 2).Value = registros.Rem_Fecha;
                        ExcelNCVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNCVencida.Cell(count, 3).Value = registros.Id_Prd;
                        ExcelNCVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNCVencida.Cell(count, 4).Value = registros.Prd_Descripcion;
                        ExcelNCVencida.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNCVencida.Cell(count, 5).Value = registros.SaldoUnidades;
                        ExcelNCVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        double total = registros.SaldoUnidades * registros.Prd_Pesos;
                        ExcelNCVencida.Cell(count, 6).Value = total.ToString("C2");
                        ExcelNCVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                        totalCte = totalCte + total;
                        TotalMAtriz = TotalMAtriz + total;
                        TotalGral = TotalGral + total;
                        count = count + 1;
                    }
                    else
                    {
                        if (id_cte != registros.Id_Cte)
                        {
                            ExcelNCVencida.Cell(count, 5).Value = "Total por Cliente";
                            ExcelNCVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNCVencida.Cell(count, 6).Value = totalCte.ToString("C2");
                            ExcelNCVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            count = count + 1;
                            totalCte = 0;

                            id_cte = registros.Id_Cte;
                            ExcelNCVencida.Cell(count, 1).Value = "Cliente";
                            ExcelNCVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNCVencida.Cell(count, 2).Value = registros.Id_Cte;
                            ExcelNCVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNCVencida.Range(count, 3, count, 4).Merge();
                            ExcelNCVencida.Cell(count, 3).Value = registros.Cte_NomComercial;
                            ExcelNCVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            count = count + 1;

                            ExcelNCVencida.Cell(count, 1).Value = registros.Id_Rem;
                            ExcelNCVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNCVencida.Cell(count, 2).Value = registros.Rem_Fecha;
                            ExcelNCVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNCVencida.Cell(count, 3).Value = registros.Id_Prd;
                            ExcelNCVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNCVencida.Cell(count, 4).Value = registros.Prd_Descripcion;
                            ExcelNCVencida.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNCVencida.Cell(count, 5).Value = registros.SaldoUnidades;
                            ExcelNCVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            double total = registros.SaldoUnidades * registros.Prd_Pesos;
                            ExcelNCVencida.Cell(count, 6).Value = total.ToString("C2");
                            ExcelNCVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                            totalCte = totalCte + total;
                            TotalMAtriz = TotalMAtriz + total;
                            TotalGral = TotalGral + total;
                            count = count + 1;
                        }
                        else
                        {
                            ExcelNCVencida.Cell(count, 1).Value = registros.Id_Rem;
                            ExcelNCVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNCVencida.Cell(count, 2).Value = registros.Rem_Fecha;
                            ExcelNCVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNCVencida.Cell(count, 3).Value = registros.Id_Prd;
                            ExcelNCVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNCVencida.Cell(count, 4).Value = registros.Prd_Descripcion;
                            ExcelNCVencida.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNCVencida.Cell(count, 5).Value = registros.SaldoUnidades;
                            ExcelNCVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            double total = registros.SaldoUnidades * registros.Prd_Pesos;
                            ExcelNCVencida.Cell(count, 6).Value = total.ToString("C2");
                            ExcelNCVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                            totalCte = totalCte + total;
                            TotalMAtriz = TotalMAtriz + total;
                            TotalGral = TotalGral + total;
                            count = count + 1;
                        }
                    }
                }

                if (TotalMAtriz != 0)
                {
                    ExcelNCVencida.Cell(count, 5).Value = "Total por Cliente";
                    ExcelNCVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    ExcelNCVencida.Cell(count, 6).Value = totalCte.ToString("C2");
                    ExcelNCVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    totalCte = 0;

                    ExcelNCVencida.Cell(count, 5).Value = "Total por Territorio";
                    ExcelNCVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    ExcelNCVencida.Cell(count, 6).Value = TotalMAtriz.ToString("C2");
                    ExcelNCVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    TotalMAtriz = 0;

                    ExcelNCVencida.Cell(count, 5).Value = "Total";
                    ExcelNCVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    ExcelNCVencida.Cell(count, 6).Value = TotalGral.ToString("C2");
                    ExcelNCVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    TotalGral = 0;
                }

                #endregion
                ExcelNCVencida.Columns().AdjustToContents();
                ExcelNCVencida.PageSetup.PageOrientation = XLPageOrientation.Portrait;
                ExcelNCVencida.PageSetup.Margins.Top = 0.5;
                ExcelNCVencida.PageSetup.Margins.Bottom = 0.3;
                ExcelNCVencida.PageSetup.Margins.Left = 0.5;
                ExcelNCVencida.PageSetup.Margins.Right = 0.3;
                ExcelNCVencida.PageSetup.Margins.Footer = 0;
                ExcelNCVencida.PageSetup.Margins.Header = 0;
                ExcelNCVencida.PageSetup.PagesWide = 1;
                ExcelNCVencida.PageSetup.PaperSize = XLPaperSize.A4Paper;
                ExcelNCVencida.PageSetup.VerticalDpi = 600;
                ExcelNCVencida.PageSetup.HorizontalDpi = 600;
                #endregion
                //Reporte de No Encontrado Vigente
                #region MovNEVigente

                ReporteRemisiones DatosNEVigente = new ReporteRemisiones();
                List<ReporteRemisiones> listaNEVigente = new List<ReporteRemisiones>();
                DatosNEVigente.Id_Emp = Sesion.Id_Emp;
                DatosNEVigente.Id_Cd = Sesion.Id_Cd_Ver;
                DatosNEVigente.Vencido = 0;
                DatosNEVigente.FechaIni = fechainicial;
                DatosNEVigente.FechaFin = fechafinal;
                DatosNEVigente.TipoRemision = 61;
                CN.Rep_RemisionesVencidas(DatosNEVigente, ref listaNEVigente, Sesion.Emp_Cnx);


                var ExcelNEVigente = workbook.Worksheets.Add("NO_ENCONTRADO_VIG");

                #region Encabezado

                ExcelNEVigente.Cell(1, 1).Value = "Sucursal";
                ExcelNEVigente.Cell(1, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNEVigente.Cell(1, 2).Value = Sesion.Id_Cd_Ver + " - " + Sesion.Cd_Nombre;
                ExcelNEVigente.Cell(1, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNEVigente.Cell(1, 4).Value = Emp_Nombre;
                ExcelNEVigente.Cell(1, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelNEVigente.Cell(2, 1).Value = "Usuario";
                ExcelNEVigente.Cell(2, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNEVigente.Cell(2, 2).Value = Sesion.U_Nombre;
                ExcelNEVigente.Cell(2, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNEVigente.Cell(2, 4).Value = "Control de Remisiones";
                ExcelNEVigente.Cell(2, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelNEVigente.Cell(3, 1).Value = "Fecha del Documento";
                ExcelNEVigente.Cell(3, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNEVigente.Cell(3, 2).Value = DateTime.Now.ToString("dd/MM/yyy");
                ExcelNEVigente.Cell(3, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelNEVigente.Cell(4, 1).Value = "Fecha Inical";
                ExcelNEVigente.Cell(4, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNEVigente.Cell(4, 2).Value = fechainicial.ToString("dd/MM/yyy"); ;
                ExcelNEVigente.Cell(4, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelNEVigente.Cell(5, 1).Value = "Fecha Final";
                ExcelNEVigente.Cell(5, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNEVigente.Cell(5, 2).Value = fechafinal.ToString("dd/MM/yyy");
                ExcelNEVigente.Cell(5, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelNEVigente.Cell(6, 1).Value = "Cliente";
                ExcelNEVigente.Cell(6, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNEVigente.Cell(6, 2).Value = "Todos";
                ExcelNEVigente.Cell(6, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelNEVigente.Cell(7, 1).Value = "Territorio";
                ExcelNEVigente.Cell(7, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNEVigente.Cell(7, 2).Value = "Todos";
                ExcelNEVigente.Cell(7, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelNEVigente.Cell(8, 1).Value = "Vencidos";
                ExcelNEVigente.Cell(8, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNEVigente.Cell(8, 2).Value = "NO";
                ExcelNEVigente.Cell(8, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelNEVigente.Cell(9, 1).Value = "Nivel";
                ExcelNEVigente.Cell(9, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNEVigente.Cell(9, 2).Value = "Detalle";
                ExcelNEVigente.Cell(9, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelNEVigente.Cell(10, 1).Value = "Tipo de Remisión";
                ExcelNEVigente.Cell(10, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNEVigente.Range(10, 2, 10, 4).Merge();
                ExcelNEVigente.Range(10, 2, 10, 4).Value = "Producto no encontrado vigente";
                ExcelNEVigente.Range(10, 2, 10, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                #endregion

                #region Cuerpo

                ExcelNEVigente.Cell(12, 1).Value = "Remisión";
                ExcelNEVigente.Cell(12, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelNEVigente.Cell(12, 2).Value = "Fecha";
                ExcelNEVigente.Cell(12, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelNEVigente.Cell(12, 3).Value = "Núm de Producto";
                ExcelNEVigente.Cell(12, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelNEVigente.Cell(12, 4).Value = "Descripción de Producto";
                ExcelNEVigente.Cell(12, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelNEVigente.Cell(12, 5).Value = "Unidades";
                ExcelNEVigente.Cell(12, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelNEVigente.Cell(12, 6).Value = "Importe";
                ExcelNEVigente.Cell(12, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                count = 13;
                idMatriz = -1;
                id_cte = -1;
                totalCte = 0;
                TotalMAtriz = 0;
                TotalGral = 0;

                foreach (ReporteRemisiones registros in listaNEVigente)
                {
                    if (idMatriz == -1)
                    {
                        idMatriz = registros.Id_Ter;
                        ExcelNEVigente.Cell(count, 1).Value = "Territorio";
                        ExcelNEVigente.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNEVigente.Cell(count, 2).Value = registros.Id_Ter;
                        ExcelNEVigente.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNEVigente.Range(count, 3, count, 4).Merge();
                        ExcelNEVigente.Cell(count, 3).Value = registros.Ter_Nombre;
                        ExcelNEVigente.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                    }
                    if (id_cte == -1)
                    {
                        id_cte = registros.Id_Cte;

                        ExcelNEVigente.Cell(count, 1).Value = "Cliente";
                        ExcelNEVigente.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNEVigente.Cell(count, 2).Value = registros.Id_Cte;
                        ExcelNEVigente.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNEVigente.Range(count, 3, count, 4).Merge();
                        ExcelNEVigente.Cell(count, 3).Value = registros.Cte_NomComercial;
                        ExcelNEVigente.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                    }

                    if (idMatriz != registros.Id_Ter)
                    {
                        ExcelNEVigente.Cell(count, 5).Value = "Total por Cliente";
                        ExcelNEVigente.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNEVigente.Cell(count, 6).Value = totalCte.ToString("C2");
                        ExcelNEVigente.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                        totalCte = 0;

                        ExcelNEVigente.Cell(count, 5).Value = "Total por Territorio";
                        ExcelNEVigente.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNEVigente.Cell(count, 6).Value = TotalMAtriz.ToString("C2");
                        ExcelNEVigente.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                        TotalMAtriz = 0;

                        idMatriz = registros.Id_Ter;
                        ExcelNEVigente.Cell(count, 1).Value = "Territorio";
                        ExcelNEVigente.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNEVigente.Cell(count, 2).Value = registros.Id_Ter;
                        ExcelNEVigente.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNEVigente.Range(count, 3, count, 4).Merge();
                        ExcelNEVigente.Cell(count, 3).Value = registros.Ter_Nombre;
                        ExcelNEVigente.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;

                        id_cte = registros.Id_Cte;
                        ExcelNEVigente.Cell(count, 1).Value = "Cliente";
                        ExcelNEVigente.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNEVigente.Cell(count, 2).Value = registros.Id_Cte;
                        ExcelNEVigente.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNEVigente.Range(count, 3, count, 4).Merge();
                        ExcelNEVigente.Cell(count, 3).Value = registros.Cte_NomComercial;
                        ExcelNEVigente.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;

                        ExcelNEVigente.Cell(count, 1).Value = registros.Id_Rem;
                        ExcelNEVigente.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNEVigente.Cell(count, 2).Value = registros.Rem_Fecha;
                        ExcelNEVigente.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNEVigente.Cell(count, 3).Value = registros.Id_Prd;
                        ExcelNEVigente.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNEVigente.Cell(count, 4).Value = registros.Prd_Descripcion;
                        ExcelNEVigente.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNEVigente.Cell(count, 5).Value = registros.SaldoUnidades;
                        ExcelNEVigente.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        double total = registros.SaldoUnidades * registros.Prd_Pesos;
                        ExcelNEVigente.Cell(count, 6).Value = total.ToString("C2");
                        ExcelNEVigente.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                        totalCte = totalCte + total;
                        TotalMAtriz = TotalMAtriz + total;
                        TotalGral = TotalGral + total;
                        count = count + 1;
                    }
                    else
                    {
                        if (id_cte != registros.Id_Cte)
                        {
                            ExcelNEVigente.Cell(count, 5).Value = "Total por Cliente";
                            ExcelNEVigente.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNEVigente.Cell(count, 6).Value = totalCte.ToString("C2");
                            ExcelNEVigente.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            count = count + 1;
                            totalCte = 0;

                            id_cte = registros.Id_Cte;
                            ExcelNEVigente.Cell(count, 1).Value = "Cliente";
                            ExcelNEVigente.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNEVigente.Cell(count, 2).Value = registros.Id_Cte;
                            ExcelNEVigente.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNEVigente.Range(count, 3, count, 4).Merge();
                            ExcelNEVigente.Cell(count, 3).Value = registros.Cte_NomComercial;
                            ExcelNEVigente.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            count = count + 1;

                            ExcelNEVigente.Cell(count, 1).Value = registros.Id_Rem;
                            ExcelNEVigente.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNEVigente.Cell(count, 2).Value = registros.Rem_Fecha;
                            ExcelNEVigente.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNEVigente.Cell(count, 3).Value = registros.Id_Prd;
                            ExcelNEVigente.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNEVigente.Cell(count, 4).Value = registros.Prd_Descripcion;
                            ExcelNEVigente.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNEVigente.Cell(count, 5).Value = registros.SaldoUnidades;
                            ExcelNEVigente.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            double total = registros.SaldoUnidades * registros.Prd_Pesos;
                            ExcelNEVigente.Cell(count, 6).Value = total.ToString("C2");
                            ExcelNEVigente.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                            totalCte = totalCte + total;
                            TotalMAtriz = TotalMAtriz + total;
                            TotalGral = TotalGral + total;
                            count = count + 1;
                        }
                        else
                        {
                            ExcelNEVigente.Cell(count, 1).Value = registros.Id_Rem;
                            ExcelNEVigente.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNEVigente.Cell(count, 2).Value = registros.Rem_Fecha;
                            ExcelNEVigente.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNEVigente.Cell(count, 3).Value = registros.Id_Prd;
                            ExcelNEVigente.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNEVigente.Cell(count, 4).Value = registros.Prd_Descripcion;
                            ExcelNEVigente.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNEVigente.Cell(count, 5).Value = registros.SaldoUnidades;
                            ExcelNEVigente.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            double total = registros.SaldoUnidades * registros.Prd_Pesos;
                            ExcelNEVigente.Cell(count, 6).Value = total.ToString("C2");
                            ExcelNEVigente.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                            totalCte = totalCte + total;
                            TotalMAtriz = TotalMAtriz + total;
                            TotalGral = TotalGral + total;
                            count = count + 1;
                        }
                    }
                }

                if (TotalMAtriz != 0)
                {
                    ExcelNEVigente.Cell(count, 5).Value = "Total por Cliente";
                    ExcelNEVigente.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    ExcelNEVigente.Cell(count, 6).Value = totalCte.ToString("C2");
                    ExcelNEVigente.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    totalCte = 0;

                    ExcelNEVigente.Cell(count, 5).Value = "Total por Territorio";
                    ExcelNEVigente.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    ExcelNEVigente.Cell(count, 6).Value = TotalMAtriz.ToString("C2");
                    ExcelNEVigente.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    TotalMAtriz = 0;

                    ExcelNEVigente.Cell(count, 5).Value = "Total";
                    ExcelNEVigente.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    ExcelNEVigente.Cell(count, 6).Value = TotalGral.ToString("C2");
                    ExcelNEVigente.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    TotalGral = 0;
                }

                #endregion
                ExcelNEVigente.Columns().AdjustToContents();
                ExcelNEVigente.PageSetup.PageOrientation = XLPageOrientation.Portrait;
                ExcelNEVigente.PageSetup.Margins.Top = 0.5;
                ExcelNEVigente.PageSetup.Margins.Bottom = 0.3;
                ExcelNEVigente.PageSetup.Margins.Left = 0.5;
                ExcelNEVigente.PageSetup.Margins.Right = 0.3;
                ExcelNEVigente.PageSetup.Margins.Footer = 0;
                ExcelNEVigente.PageSetup.Margins.Header = 0;
                ExcelNEVigente.PageSetup.PagesWide = 1;
                ExcelNEVigente.PageSetup.PaperSize = XLPaperSize.A4Paper;
                ExcelNEVigente.PageSetup.VerticalDpi = 600;
                ExcelNEVigente.PageSetup.HorizontalDpi = 600;
                #endregion
                //Reporte de No Encontrado Vencidas
                #region MovNEVencida

                ReporteRemisiones DatosNEVencida = new ReporteRemisiones();
                List<ReporteRemisiones> listaNEVencida = new List<ReporteRemisiones>();
                DatosNEVencida.Id_Emp = Sesion.Id_Emp;
                DatosNEVencida.Id_Cd = Sesion.Id_Cd_Ver;
                DatosNEVencida.Vencido = 1;
                DatosNEVencida.FechaIni = fechainicial;
                DatosNEVencida.FechaFin = fechafinal;
                DatosNEVencida.TipoRemision = 61;
                CN.Rep_RemisionesVencidas(DatosNEVencida, ref listaNEVencida, Sesion.Emp_Cnx);


                var ExcelNEVencida = workbook.Worksheets.Add("NO_ENCONTRADO_VEN");

                #region Encabezado

                ExcelNEVencida.Cell(1, 1).Value = "Sucursal";
                ExcelNEVencida.Cell(1, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNEVencida.Cell(1, 2).Value = Sesion.Id_Cd_Ver + " - " + Sesion.Cd_Nombre;
                ExcelNEVencida.Cell(1, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNEVencida.Cell(1, 4).Value = Emp_Nombre;
                ExcelNEVencida.Cell(1, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelNEVencida.Cell(2, 1).Value = "Usuario";
                ExcelNEVencida.Cell(2, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNEVencida.Cell(2, 2).Value = Sesion.U_Nombre;
                ExcelNEVencida.Cell(2, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNEVencida.Cell(2, 4).Value = "Control de Remisiones";
                ExcelNEVencida.Cell(2, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelNEVencida.Cell(3, 1).Value = "Fecha del Documento";
                ExcelNEVencida.Cell(3, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNEVencida.Cell(3, 2).Value = DateTime.Now.ToString("dd/MM/yyy");
                ExcelNEVencida.Cell(3, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelNEVencida.Cell(4, 1).Value = "Fecha Inical";
                ExcelNEVencida.Cell(4, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNEVencida.Cell(4, 2).Value = fechainicial.ToString("dd/MM/yyy"); ;
                ExcelNEVencida.Cell(4, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelNEVencida.Cell(5, 1).Value = "Fecha Final";
                ExcelNEVencida.Cell(5, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNEVencida.Cell(5, 2).Value = fechafinal.ToString("dd/MM/yyy");
                ExcelNEVencida.Cell(5, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelNEVencida.Cell(6, 1).Value = "Cliente";
                ExcelNEVencida.Cell(6, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNEVencida.Cell(6, 2).Value = "Todos";
                ExcelNEVencida.Cell(6, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelNEVencida.Cell(7, 1).Value = "Territorio";
                ExcelNEVencida.Cell(7, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNEVencida.Cell(7, 2).Value = "Todos";
                ExcelNEVencida.Cell(7, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelNEVencida.Cell(8, 1).Value = "Vencidos";
                ExcelNEVencida.Cell(8, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNEVencida.Cell(8, 2).Value = "SI";
                ExcelNEVencida.Cell(8, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelNEVencida.Cell(9, 1).Value = "Nivel";
                ExcelNEVencida.Cell(9, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNEVencida.Cell(9, 2).Value = "Detalle";
                ExcelNEVencida.Cell(9, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelNEVencida.Cell(10, 1).Value = "Tipo de Remisión";
                ExcelNEVencida.Cell(10, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelNEVencida.Range(10, 2, 10, 4).Merge();
                ExcelNEVencida.Range(10, 2, 10, 4).Value = "Producto no encontrado vencidas";
                ExcelNEVencida.Range(10, 2, 10, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                #endregion

                #region Cuerpo

                ExcelNEVencida.Cell(12, 1).Value = "Remisión";
                ExcelNEVencida.Cell(12, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelNEVencida.Cell(12, 2).Value = "Fecha";
                ExcelNEVencida.Cell(12, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelNEVencida.Cell(12, 3).Value = "Núm de Producto";
                ExcelNEVencida.Cell(12, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelNEVencida.Cell(12, 4).Value = "Descripción de Producto";
                ExcelNEVencida.Cell(12, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelNEVencida.Cell(12, 5).Value = "Unidades";
                ExcelNEVencida.Cell(12, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelNEVencida.Cell(12, 6).Value = "Importe";
                ExcelNEVencida.Cell(12, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                count = 13;
                idMatriz = -1;
                id_cte = -1;
                totalCte = 0;
                TotalMAtriz = 0;
                TotalGral = 0;

                foreach (ReporteRemisiones registros in listaNEVencida)
                {
                    if (idMatriz == -1)
                    {
                        idMatriz = registros.Id_Ter;
                        ExcelNEVencida.Cell(count, 1).Value = "Territorio";
                        ExcelNEVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNEVencida.Cell(count, 2).Value = registros.Id_Ter;
                        ExcelNEVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNEVencida.Range(count, 3, count, 4).Merge();
                        ExcelNEVencida.Cell(count, 3).Value = registros.Ter_Nombre;
                        ExcelNEVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                    }
                    if (id_cte == -1)
                    {
                        id_cte = registros.Id_Cte;

                        ExcelNEVencida.Cell(count, 1).Value = "Cliente";
                        ExcelNEVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNEVencida.Cell(count, 2).Value = registros.Id_Cte;
                        ExcelNEVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNEVencida.Range(count, 3, count, 4).Merge();
                        ExcelNEVencida.Cell(count, 3).Value = registros.Cte_NomComercial;
                        ExcelNEVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                    }

                    if (idMatriz != registros.Id_Ter)
                    {
                        ExcelNEVencida.Cell(count, 5).Value = "Total por Cliente";
                        ExcelNEVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNEVencida.Cell(count, 6).Value = totalCte.ToString("C2");
                        ExcelNEVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                        totalCte = 0;

                        ExcelNEVencida.Cell(count, 5).Value = "Total por Territorio";
                        ExcelNEVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNEVencida.Cell(count, 6).Value = TotalMAtriz.ToString("C2");
                        ExcelNEVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                        TotalMAtriz = 0;

                        idMatriz = registros.Id_Ter;
                        ExcelNEVencida.Cell(count, 1).Value = "Territorio";
                        ExcelNEVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNEVencida.Cell(count, 2).Value = registros.Id_Ter;
                        ExcelNEVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNEVencida.Range(count, 3, count, 4).Merge();
                        ExcelNEVencida.Cell(count, 3).Value = registros.Ter_Nombre;
                        ExcelNEVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;

                        id_cte = registros.Id_Cte;
                        ExcelNEVencida.Cell(count, 1).Value = "Cliente";
                        ExcelNEVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNEVencida.Cell(count, 2).Value = registros.Id_Cte;
                        ExcelNEVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNEVencida.Range(count, 3, count, 4).Merge();
                        ExcelNEVencida.Cell(count, 3).Value = registros.Cte_NomComercial;
                        ExcelNEVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;

                        ExcelNEVencida.Cell(count, 1).Value = registros.Id_Rem;
                        ExcelNEVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNEVencida.Cell(count, 2).Value = registros.Rem_Fecha;
                        ExcelNEVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNEVencida.Cell(count, 3).Value = registros.Id_Prd;
                        ExcelNEVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNEVencida.Cell(count, 4).Value = registros.Prd_Descripcion;
                        ExcelNEVencida.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelNEVencida.Cell(count, 5).Value = registros.SaldoUnidades;
                        ExcelNEVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        double total = registros.SaldoUnidades * registros.Prd_Pesos;
                        ExcelNEVencida.Cell(count, 6).Value = total.ToString("C2");
                        ExcelNEVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                        totalCte = totalCte + total;
                        TotalMAtriz = TotalMAtriz + total;
                        TotalGral = TotalGral + total;
                        count = count + 1;
                    }
                    else
                    {
                        if (id_cte != registros.Id_Cte)
                        {
                            ExcelNEVencida.Cell(count, 5).Value = "Total por Cliente";
                            ExcelNEVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNEVencida.Cell(count, 6).Value = totalCte.ToString("C2");
                            ExcelNEVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            count = count + 1;
                            totalCte = 0;

                            id_cte = registros.Id_Cte;
                            ExcelNEVencida.Cell(count, 1).Value = "Cliente";
                            ExcelNEVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNEVencida.Cell(count, 2).Value = registros.Id_Cte;
                            ExcelNEVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNEVencida.Range(count, 3, count, 4).Merge();
                            ExcelNEVencida.Cell(count, 3).Value = registros.Cte_NomComercial;
                            ExcelNEVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            count = count + 1;

                            ExcelNEVencida.Cell(count, 1).Value = registros.Id_Rem;
                            ExcelNEVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNEVencida.Cell(count, 2).Value = registros.Rem_Fecha;
                            ExcelNEVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNEVencida.Cell(count, 3).Value = registros.Id_Prd;
                            ExcelNEVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNEVencida.Cell(count, 4).Value = registros.Prd_Descripcion;
                            ExcelNEVencida.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNEVencida.Cell(count, 5).Value = registros.SaldoUnidades;
                            ExcelNEVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            double total = registros.SaldoUnidades * registros.Prd_Pesos;
                            ExcelNEVencida.Cell(count, 6).Value = total.ToString("C2");
                            ExcelNEVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                            totalCte = totalCte + total;
                            TotalMAtriz = TotalMAtriz + total;
                            TotalGral = TotalGral + total;
                            count = count + 1;
                        }
                        else
                        {
                            ExcelNEVencida.Cell(count, 1).Value = registros.Id_Rem;
                            ExcelNEVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNEVencida.Cell(count, 2).Value = registros.Rem_Fecha;
                            ExcelNEVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNEVencida.Cell(count, 3).Value = registros.Id_Prd;
                            ExcelNEVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNEVencida.Cell(count, 4).Value = registros.Prd_Descripcion;
                            ExcelNEVencida.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelNEVencida.Cell(count, 5).Value = registros.SaldoUnidades;
                            ExcelNEVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            double total = registros.SaldoUnidades * registros.Prd_Pesos;
                            ExcelNEVencida.Cell(count, 6).Value = total.ToString("C2");
                            ExcelNEVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                            totalCte = totalCte + total;
                            TotalMAtriz = TotalMAtriz + total;
                            TotalGral = TotalGral + total;
                            count = count + 1;
                        }
                    }
                }

                if (TotalMAtriz != 0)
                {
                    ExcelNEVencida.Cell(count, 5).Value = "Total por Cliente";
                    ExcelNEVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    ExcelNEVencida.Cell(count, 6).Value = totalCte.ToString("C2");
                    ExcelNEVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    totalCte = 0;

                    ExcelNEVencida.Cell(count, 5).Value = "Total por Territorio";
                    ExcelNEVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    ExcelNEVencida.Cell(count, 6).Value = TotalMAtriz.ToString("C2");
                    ExcelNEVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    TotalMAtriz = 0;

                    ExcelNEVencida.Cell(count, 5).Value = "Total";
                    ExcelNEVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    ExcelNEVencida.Cell(count, 6).Value = TotalGral.ToString("C2");
                    ExcelNEVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    TotalGral = 0;
                }

                #endregion
                ExcelNEVencida.Columns().AdjustToContents();
                ExcelNEVencida.PageSetup.PageOrientation = XLPageOrientation.Portrait;
                ExcelNEVencida.PageSetup.Margins.Top = 0.5;
                ExcelNEVencida.PageSetup.Margins.Bottom = 0.3;
                ExcelNEVencida.PageSetup.Margins.Left = 0.5;
                ExcelNEVencida.PageSetup.Margins.Right = 0.3;
                ExcelNEVencida.PageSetup.Margins.Footer = 0;
                ExcelNEVencida.PageSetup.Margins.Header = 0;
                ExcelNEVencida.PageSetup.PagesWide = 1;
                ExcelNEVencida.PageSetup.PaperSize = XLPaperSize.A4Paper;
                ExcelNEVencida.PageSetup.VerticalDpi = 600;
                ExcelNEVencida.PageSetup.HorizontalDpi = 600;
                #endregion
                //Reporte de Consignacion
                #region Consignacion

                ReporteConsignacion DatosConsignacion = new ReporteConsignacion();
                List<ReporteConsignacion> listaConsignacion = new List<ReporteConsignacion>();
                DatosConsignacion.Id_Emp = Sesion.Id_Emp;
                DatosConsignacion.Id_Cd = Sesion.Id_Cd_Ver;
                DatosConsignacion.fecha = fechafinal;

                CN.Rep_Consigancion(DatosConsignacion, ref listaConsignacion, Sesion.Emp_Cnx);


                var ExcelConsig = workbook.Worksheets.Add("Reporte Consig");

                #region Encabezado

                ExcelConsig.Range(1, 3, 1, 5).Merge();
                ExcelConsig.Range(1, 3, 1, 5).Value = Emp_Nombre;
                ExcelConsig.Range(1, 3, 1, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelConsig.Range(2, 3, 2, 5).Merge();
                ExcelConsig.Range(2, 3, 2, 5).Value = "Rotación de inventarios en consignación";
                ExcelConsig.Range(2, 3, 2, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelConsig.Cell(2, 6).Value = "Fecha";
                ExcelConsig.Cell(2, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelConsig.Cell(2, 7).Value = DateTime.Now.ToString("dd/MM/yyy");
                ExcelConsig.Cell(2, 7).Style.Border.OutsideBorder = XLBorderStyleValues.None;


                #endregion

                #region Cuerpo
                ExcelConsig.Range(3, 12, 3, 15).Merge();
                ExcelConsig.Range(3, 12, 3, 15).Value = "Importe";
                ExcelConsig.Range(3, 12, 3, 15).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ExcelConsig.Cell(4, 1).Value = "CDI";
                ExcelConsig.Cell(4, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelConsig.Cell(4, 2).Value = "Sucursal";
                ExcelConsig.Cell(4, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelConsig.Cell(4, 3).Value = "Cliente";
                ExcelConsig.Cell(4, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelConsig.Cell(4, 4).Value = "Nombre del Cliente";
                ExcelConsig.Cell(4, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelConsig.Cell(4, 5).Value = "Código";
                ExcelConsig.Cell(4, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelConsig.Cell(4, 6).Value = "Descripción";
                ExcelConsig.Cell(4, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelConsig.Cell(4, 7).Value = "Presentación";
                ExcelConsig.Cell(4, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelConsig.Cell(4, 8).Value = "Unidad";
                ExcelConsig.Cell(4, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelConsig.Cell(4, 9).Value = "Cantidad";
                ExcelConsig.Cell(4, 9).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelConsig.Cell(4, 10).Value = "Precio AAA";
                ExcelConsig.Cell(4, 10).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelConsig.Cell(4, 11).Value = "Importe";
                ExcelConsig.Cell(4, 11).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelConsig.Cell(4, 12).Value = "Antepenúltimo mes cerrado";
                ExcelConsig.Cell(4, 12).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelConsig.Cell(4, 13).Value = "Penúltimo mes cerrado";
                ExcelConsig.Cell(4, 13).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelConsig.Cell(4, 14).Value = "Último mes cerrado";
                ExcelConsig.Cell(4, 14).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelConsig.Cell(4, 15).Value = "Promedio Venta";
                ExcelConsig.Cell(4, 15).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelConsig.Cell(4, 16).Value = "Monto Promedio";
                ExcelConsig.Cell(4, 16).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelConsig.Cell(4, 17).Value = "Rotación";
                ExcelConsig.Cell(4, 17).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelConsig.Cell(4, 18).Value = "Menor a 30 días";
                ExcelConsig.Cell(4, 18).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelConsig.Cell(4, 19).Value = "Mayor a 30 días P.AAA";
                ExcelConsig.Cell(4, 19).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelConsig.Cell(4, 20).Value = "Retirar o Facturar Unidades";
                ExcelConsig.Cell(4, 20).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                count = 5;
                foreach (ReporteConsignacion registros in listaConsignacion)
                {
                    ExcelConsig.Cell(count, 1).Value = registros.CD_Nom;
                    ExcelConsig.Cell(count, 2).Value = registros.Id_Cd;
                    ExcelConsig.Cell(count, 3).Value = registros.Id_Cte;
                    ExcelConsig.Cell(count, 4).Value = registros.Cte_NomComercial;
                    ExcelConsig.Cell(count, 5).Value = registros.Id_Prd;
                    ExcelConsig.Cell(count, 6).Value = registros.Prd_Descripcion;
                    ExcelConsig.Cell(count, 7).Value = registros.Prd_Presentacion;
                    ExcelConsig.Cell(count, 8).Value = registros.Id_Uni;
                    ExcelConsig.Cell(count, 9).Value = registros.Prd_InvFinal;
                    ExcelConsig.Cell(count, 10).Value = registros.Prd_PrecioAAA;
                    ExcelConsig.Cell(count, 11).Value = registros.ImporteInventario;
                    ExcelConsig.Cell(count, 12).Value = registros.Antepenultimo;
                    ExcelConsig.Cell(count, 13).Value = registros.Penultimo;
                    ExcelConsig.Cell(count, 14).Value = registros.Ultimo;
                    ExcelConsig.Cell(count, 15).Value = registros.Promedio;
                    ExcelConsig.Cell(count, 16).Value = registros.CostoPromedio;
                    ExcelConsig.Cell(count, 17).Value = registros.Rotacion;
                    ExcelConsig.Cell(count, 18).Value = registros.Vigente;
                    ExcelConsig.Cell(count, 19).Value = registros.Vencido;
                    ExcelConsig.Cell(count, 20).Value = registros.RetiraroFacturar;

                    count = count + 1;
                }

                #endregion
                ExcelConsig.Columns().AdjustToContents();
                ExcelConsig.PageSetup.PageOrientation = XLPageOrientation.Portrait;
                ExcelConsig.PageSetup.Margins.Top = 0.5;
                ExcelConsig.PageSetup.Margins.Bottom = 0.3;
                ExcelConsig.PageSetup.Margins.Left = 0.5;
                ExcelConsig.PageSetup.Margins.Right = 0.3;
                ExcelConsig.PageSetup.Margins.Footer = 0;
                ExcelConsig.PageSetup.Margins.Header = 0;
                ExcelConsig.PageSetup.PagesWide = 1;
                ExcelConsig.PageSetup.PaperSize = XLPaperSize.A4Paper;
                ExcelConsig.PageSetup.VerticalDpi = 600;
                ExcelConsig.PageSetup.HorizontalDpi = 600;
                #endregion

                //RBM Agosto 2024
                //Reporte devolucion a cedis Vigente mov 78
                #region devolucion a cedis vigente
                ReporteRemisiones DatosDCVIG = new ReporteRemisiones();
                List<ReporteRemisiones> listaDCVIG = new List<ReporteRemisiones>();
                DatosDCVIG.Id_Emp = Sesion.Id_Emp;
                DatosDCVIG.Id_Cd = Sesion.Id_Cd_Ver;
                DatosDCVIG.Vencido = 0;
                DatosDCVIG.FechaIni = fechainicial;
                DatosDCVIG.FechaFin = fechafinal;
                DatosDCVIG.TipoRemision = 78;
                CN.Rep_RemisionesVencidas(DatosDCVIG, ref listaDCVIG, Sesion.Emp_Cnx);


                var ExcelDCVIG = workbook.Worksheets.Add("DEVOLUCIONCEDIS_VIG");

                #region Encabezado

                ExcelDCVIG.Cell(1, 1).Value = "Sucursal";
                ExcelDCVIG.Cell(1, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDCVIG.Cell(1, 2).Value = Sesion.Id_Cd_Ver + " - " + Sesion.Cd_Nombre;
                ExcelDCVIG.Cell(1, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDCVIG.Cell(1, 4).Value = Emp_Nombre;
                ExcelDCVIG.Cell(1, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelDCVIG.Cell(2, 1).Value = "Usuario";
                ExcelDCVIG.Cell(2, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDCVIG.Cell(2, 2).Value = Sesion.U_Nombre;
                ExcelDCVIG.Cell(2, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDCVIG.Cell(2, 4).Value = "Control de Remisiones";
                ExcelDCVIG.Cell(2, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelDCVIG.Cell(3, 1).Value = "Fecha del Documento";
                ExcelDCVIG.Cell(3, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDCVIG.Cell(3, 2).Value = DateTime.Now.ToString("dd/MM/yyy");
                ExcelDCVIG.Cell(3, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelDCVIG.Cell(4, 1).Value = "Fecha Inical";
                ExcelDCVIG.Cell(4, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDCVIG.Cell(4, 2).Value = fechainicial.ToString("dd/MM/yyy"); ;
                ExcelDCVIG.Cell(4, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelDCVIG.Cell(5, 1).Value = "Fecha Final";
                ExcelDCVIG.Cell(5, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDCVIG.Cell(5, 2).Value = fechafinal.ToString("dd/MM/yyy");
                ExcelDCVIG.Cell(5, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelDCVIG.Cell(6, 1).Value = "Cliente";
                ExcelDCVIG.Cell(6, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDCVIG.Cell(6, 2).Value = "Todos";
                ExcelDCVIG.Cell(6, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelDCVIG.Cell(7, 1).Value = "Territorio";
                ExcelDCVIG.Cell(7, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDCVIG.Cell(7, 2).Value = "Todos";
                ExcelDCVIG.Cell(7, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelDCVIG.Cell(8, 1).Value = "Vencidos";
                ExcelDCVIG.Cell(8, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDCVIG.Cell(8, 2).Value = "NO";
                ExcelDCVIG.Cell(8, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelDCVIG.Cell(9, 1).Value = "Nivel";
                ExcelDCVIG.Cell(9, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDCVIG.Cell(9, 2).Value = "Detalle";
                ExcelDCVIG.Cell(9, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelDCVIG.Cell(10, 1).Value = "Tipo de Remisión";
                ExcelDCVIG.Cell(10, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDCVIG.Range(10, 2, 10, 4).Merge();
                ExcelDCVIG.Range(10, 2, 10, 4).Value = "Producto Devolución a Cedis";
                ExcelDCVIG.Range(10, 2, 10, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                #endregion

                #region Cuerpo

                ExcelDCVIG.Cell(12, 1).Value = "Remisión";
                ExcelDCVIG.Cell(12, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelDCVIG.Cell(12, 2).Value = "Fecha";
                ExcelDCVIG.Cell(12, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelDCVIG.Cell(12, 3).Value = "Núm de Producto";
                ExcelDCVIG.Cell(12, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelDCVIG.Cell(12, 4).Value = "Descripción de Producto";
                ExcelDCVIG.Cell(12, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelDCVIG.Cell(12, 5).Value = "Unidades";
                ExcelDCVIG.Cell(12, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelDCVIG.Cell(12, 6).Value = "Importe";
                ExcelDCVIG.Cell(12, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                count = 13;
                idMatriz = -1;
                id_cte = -1;
                totalCte = 0;
                TotalMAtriz = 0;
                TotalGral = 0;

                foreach (ReporteRemisiones registros in listaDCVIG)
                {
                    if (idMatriz == -1)
                    {
                        idMatriz = registros.Id_Ter;
                        ExcelDCVIG.Cell(count, 1).Value = "Territorio";
                        ExcelDCVIG.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDCVIG.Cell(count, 2).Value = registros.Id_Ter;
                        ExcelDCVIG.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDCVIG.Range(count, 3, count, 4).Merge();
                        ExcelDCVIG.Cell(count, 3).Value = registros.Ter_Nombre;
                        ExcelDCVIG.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                    }
                    if (id_cte == -1)
                    {
                        id_cte = registros.Id_Cte;

                        ExcelDCVIG.Cell(count, 1).Value = "Cliente";
                        ExcelDCVIG.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDCVIG.Cell(count, 2).Value = registros.Id_Cte;
                        ExcelDCVIG.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDCVIG.Range(count, 3, count, 4).Merge();
                        ExcelDCVIG.Cell(count, 3).Value = registros.Cte_NomComercial;
                        ExcelDCVIG.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                    }

                    if (idMatriz != registros.Id_Ter)
                    {
                        ExcelDCVIG.Cell(count, 5).Value = "Total por Cliente";
                        ExcelDCVIG.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDCVIG.Cell(count, 6).Value = totalCte.ToString("C2");
                        ExcelDCVIG.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                        totalCte = 0;

                        ExcelDCVIG.Cell(count, 5).Value = "Total por Territorio";
                        ExcelDCVIG.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDCVIG.Cell(count, 6).Value = TotalMAtriz.ToString("C2");
                        ExcelDCVIG.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                        TotalMAtriz = 0;

                        idMatriz = registros.Id_Ter;
                        ExcelDCVIG.Cell(count, 1).Value = "Territorio";
                        ExcelDCVIG.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDCVIG.Cell(count, 2).Value = registros.Id_Ter;
                        ExcelDCVIG.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDCVIG.Range(count, 3, count, 4).Merge();
                        ExcelDCVIG.Cell(count, 3).Value = registros.Ter_Nombre;
                        ExcelDCVIG.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;

                        id_cte = registros.Id_Cte;
                        ExcelDCVIG.Cell(count, 1).Value = "Cliente";
                        ExcelDCVIG.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDCVIG.Cell(count, 2).Value = registros.Id_Cte;
                        ExcelDCVIG.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDCVIG.Range(count, 3, count, 4).Merge();
                        ExcelDCVIG.Cell(count, 3).Value = registros.Cte_NomComercial;
                        ExcelDCVIG.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;

                        ExcelDCVIG.Cell(count, 1).Value = registros.Id_Rem;
                        ExcelDCVIG.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDCVIG.Cell(count, 2).Value = registros.Rem_Fecha;
                        ExcelDCVIG.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDCVIG.Cell(count, 3).Value = registros.Id_Prd;
                        ExcelDCVIG.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDCVIG.Cell(count, 4).Value = registros.Prd_Descripcion;
                        ExcelDCVIG.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDCVIG.Cell(count, 5).Value = registros.SaldoUnidades;
                        ExcelDCVIG.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        double total = registros.SaldoUnidades * registros.Prd_Pesos;
                        ExcelDCVIG.Cell(count, 6).Value = total.ToString("C2");
                        ExcelDCVIG.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                        totalCte = totalCte + total;
                        TotalMAtriz = TotalMAtriz + total;
                        TotalGral = TotalGral + total;
                        count = count + 1;
                    }
                    else
                    {
                        if (id_cte != registros.Id_Cte)
                        {
                            ExcelDCVIG.Cell(count, 5).Value = "Total por Cliente";
                            ExcelDCVIG.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDCVIG.Cell(count, 6).Value = totalCte.ToString("C2");
                            ExcelDCVIG.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            count = count + 1;
                            totalCte = 0;

                            id_cte = registros.Id_Cte;
                            ExcelDCVIG.Cell(count, 1).Value = "Cliente";
                            ExcelDCVIG.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDCVIG.Cell(count, 2).Value = registros.Id_Cte;
                            ExcelDCVIG.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDCVIG.Range(count, 3, count, 4).Merge();
                            ExcelDCVIG.Cell(count, 3).Value = registros.Cte_NomComercial;
                            ExcelDCVIG.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            count = count + 1;

                            ExcelDCVIG.Cell(count, 1).Value = registros.Id_Rem;
                            ExcelDCVIG.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDCVIG.Cell(count, 2).Value = registros.Rem_Fecha;
                            ExcelDCVIG.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDCVIG.Cell(count, 3).Value = registros.Id_Prd;
                            ExcelDCVIG.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDCVIG.Cell(count, 4).Value = registros.Prd_Descripcion;
                            ExcelDCVIG.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDCVIG.Cell(count, 5).Value = registros.SaldoUnidades;
                            ExcelDCVIG.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            double total = registros.SaldoUnidades * registros.Prd_Pesos;
                            ExcelDCVIG.Cell(count, 6).Value = total.ToString("C2");
                            ExcelDCVIG.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                            totalCte = totalCte + total;
                            TotalMAtriz = TotalMAtriz + total;
                            TotalGral = TotalGral + total;
                            count = count + 1;
                        }
                        else
                        {
                            ExcelDCVIG.Cell(count, 1).Value = registros.Id_Rem;
                            ExcelDCVIG.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDCVIG.Cell(count, 2).Value = registros.Rem_Fecha;
                            ExcelDCVIG.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDCVIG.Cell(count, 3).Value = registros.Id_Prd;
                            ExcelDCVIG.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDCVIG.Cell(count, 4).Value = registros.Prd_Descripcion;
                            ExcelDCVIG.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDCVIG.Cell(count, 5).Value = registros.SaldoUnidades;
                            ExcelDCVIG.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            double total = registros.SaldoUnidades * registros.Prd_Pesos;
                            ExcelDCVIG.Cell(count, 6).Value = total.ToString("C2");
                            ExcelDCVIG.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                            totalCte = totalCte + total;
                            TotalMAtriz = TotalMAtriz + total;
                            TotalGral = TotalGral + total;
                            count = count + 1;
                        }
                    }
                }

                if (TotalMAtriz != 0)
                {
                    ExcelDCVIG.Cell(count, 5).Value = "Total por Cliente";
                    ExcelDCVIG.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    ExcelDCVIG.Cell(count, 6).Value = totalCte.ToString("C2");
                    ExcelDCVIG.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    totalCte = 0;

                    ExcelDCVIG.Cell(count, 5).Value = "Total por Territorio";
                    ExcelDCVIG.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    ExcelDCVIG.Cell(count, 6).Value = TotalMAtriz.ToString("C2");
                    ExcelDCVIG.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    TotalMAtriz = 0;

                    ExcelDCVIG.Cell(count, 5).Value = "Total";
                    ExcelDCVIG.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    ExcelDCVIG.Cell(count, 6).Value = TotalGral.ToString("C2");
                    ExcelDCVIG.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    TotalGral = 0;
                }

                #endregion
                ExcelDCVIG.Columns().AdjustToContents();
                ExcelDCVIG.PageSetup.PageOrientation = XLPageOrientation.Portrait;
                ExcelDCVIG.PageSetup.Margins.Top = 0.5;
                ExcelDCVIG.PageSetup.Margins.Bottom = 0.3;
                ExcelDCVIG.PageSetup.Margins.Left = 0.5;
                ExcelDCVIG.PageSetup.Margins.Right = 0.3;
                ExcelDCVIG.PageSetup.Margins.Footer = 0;
                ExcelDCVIG.PageSetup.Margins.Header = 0;
                ExcelDCVIG.PageSetup.PagesWide = 1;
                ExcelDCVIG.PageSetup.PaperSize = XLPaperSize.A4Paper;
                ExcelDCVIG.PageSetup.VerticalDpi = 600;
                ExcelDCVIG.PageSetup.HorizontalDpi = 600;
                #endregion
                //Reporte devolucion a cedis vencido mov 23
                #region devolución a cedis vencido

                ReporteRemisiones DatosDCVencida = new ReporteRemisiones();
                List<ReporteRemisiones> listaDCVencida = new List<ReporteRemisiones>();
                DatosDCVencida.Id_Emp = Sesion.Id_Emp;
                DatosDCVencida.Id_Cd = Sesion.Id_Cd_Ver;
                DatosDCVencida.Vencido = 1;
                DatosDCVencida.FechaIni = fechainicial;
                DatosDCVencida.FechaFin = fechafinal;
                DatosDCVencida.TipoRemision = 78;
                CN.Rep_RemisionesVencidas(DatosDCVencida, ref listaDCVencida, Sesion.Emp_Cnx);


                var ExcelDCVencida = workbook.Worksheets.Add("DEVOLUCIONCEDIS_VEN");

                #region Encabezado

                ExcelDCVencida.Cell(1, 1).Value = "Sucursal";
                ExcelDCVencida.Cell(1, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDCVencida.Cell(1, 2).Value = Sesion.Id_Cd_Ver + " - " + Sesion.Cd_Nombre;
                ExcelDCVencida.Cell(1, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDCVencida.Cell(1, 4).Value = Emp_Nombre;
                ExcelDCVencida.Cell(1, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelDCVencida.Cell(2, 1).Value = "Usuario";
                ExcelDCVencida.Cell(2, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDCVencida.Cell(2, 2).Value = Sesion.U_Nombre;
                ExcelDCVencida.Cell(2, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDCVencida.Cell(2, 4).Value = "Control de Remisiones";
                ExcelDCVencida.Cell(2, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelDCVencida.Cell(3, 1).Value = "Fecha del Documento";
                ExcelDCVencida.Cell(3, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDCVencida.Cell(3, 2).Value = DateTime.Now.ToString("dd/MM/yyy");
                ExcelDCVencida.Cell(3, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelDCVencida.Cell(4, 1).Value = "Fecha Inical";
                ExcelDCVencida.Cell(4, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDCVencida.Cell(4, 2).Value = fechainicial.ToString("dd/MM/yyy"); ;
                ExcelDCVencida.Cell(4, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelDCVencida.Cell(5, 1).Value = "Fecha Final";
                ExcelDCVencida.Cell(5, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDCVencida.Cell(5, 2).Value = fechafinal.ToString("dd/MM/yyy");
                ExcelDCVencida.Cell(5, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelDCVencida.Cell(6, 1).Value = "Cliente";
                ExcelDCVencida.Cell(6, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDCVencida.Cell(6, 2).Value = "Todos";
                ExcelDCVencida.Cell(6, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelDCVencida.Cell(7, 1).Value = "Territorio";
                ExcelDCVencida.Cell(7, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDCVencida.Cell(7, 2).Value = "Todos";
                ExcelDCVencida.Cell(7, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelDCVencida.Cell(8, 1).Value = "Vencidos";
                ExcelDCVencida.Cell(8, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDCVencida.Cell(8, 2).Value = "SI";
                ExcelDCVencida.Cell(8, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelDCVencida.Cell(9, 1).Value = "Nivel";
                ExcelDCVencida.Cell(9, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDCVencida.Cell(9, 2).Value = "Detalle";
                ExcelDCVencida.Cell(9, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelDCVencida.Cell(10, 1).Value = "Tipo de Remisión";
                ExcelDCVencida.Cell(10, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDCVencida.Range(10, 2, 10, 4).Merge();
                ExcelDCVencida.Range(10, 2, 10, 4).Value = "Producto Devolución a Cedis";
                ExcelDCVencida.Range(10, 2, 10, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                #endregion

                #region Cuerpo

                ExcelDCVencida.Cell(12, 1).Value = "Remisión";
                ExcelDCVencida.Cell(12, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelDCVencida.Cell(12, 2).Value = "Fecha";
                ExcelDCVencida.Cell(12, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelDCVencida.Cell(12, 3).Value = "Núm de Producto";
                ExcelDCVencida.Cell(12, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelDCVencida.Cell(12, 4).Value = "Descripción de Producto";
                ExcelDCVencida.Cell(12, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelDCVencida.Cell(12, 5).Value = "Unidades";
                ExcelDCVencida.Cell(12, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelDCVencida.Cell(12, 6).Value = "Importe";
                ExcelDCVencida.Cell(12, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                count = 13;
                idMatriz = -1;
                id_cte = -1;
                totalCte = 0;
                TotalMAtriz = 0;
                TotalGral = 0;

                foreach (ReporteRemisiones registros in listaDCVencida)
                {
                    if (idMatriz == -1)
                    {
                        idMatriz = registros.Id_Ter;
                        ExcelDCVencida.Cell(count, 1).Value = "Territorio";
                        ExcelDCVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDCVencida.Cell(count, 2).Value = registros.Id_Ter;
                        ExcelDCVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDCVencida.Range(count, 3, count, 4).Merge();
                        ExcelDCVencida.Cell(count, 3).Value = registros.Ter_Nombre;
                        ExcelDCVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                    }
                    if (id_cte == -1)
                    {
                        id_cte = registros.Id_Cte;

                        ExcelDCVencida.Cell(count, 1).Value = "Cliente";
                        ExcelDCVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDCVencida.Cell(count, 2).Value = registros.Id_Cte;
                        ExcelDCVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDCVencida.Range(count, 3, count, 4).Merge();
                        ExcelDCVencida.Cell(count, 3).Value = registros.Cte_NomComercial;
                        ExcelDCVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                    }

                    if (idMatriz != registros.Id_Ter)
                    {
                        ExcelDCVencida.Cell(count, 5).Value = "Total por Cliente";
                        ExcelDCVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDCVencida.Cell(count, 6).Value = totalCte.ToString("C2");
                        ExcelDCVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                        totalCte = 0;

                        ExcelDCVencida.Cell(count, 5).Value = "Total por Territorio";
                        ExcelDCVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDCVencida.Cell(count, 6).Value = TotalMAtriz.ToString("C2");
                        ExcelDCVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                        TotalMAtriz = 0;

                        idMatriz = registros.Id_Ter;
                        ExcelDCVencida.Cell(count, 1).Value = "Territorio";
                        ExcelDCVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDCVencida.Cell(count, 2).Value = registros.Id_Ter;
                        ExcelDCVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDCVencida.Range(count, 3, count, 4).Merge();
                        ExcelDCVencida.Cell(count, 3).Value = registros.Ter_Nombre;
                        ExcelDCVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;

                        id_cte = registros.Id_Cte;
                        ExcelDCVencida.Cell(count, 1).Value = "Cliente";
                        ExcelDCVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDCVencida.Cell(count, 2).Value = registros.Id_Cte;
                        ExcelDCVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDCVencida.Range(count, 3, count, 4).Merge();
                        ExcelDCVencida.Cell(count, 3).Value = registros.Cte_NomComercial;
                        ExcelDCVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;

                        ExcelDCVencida.Cell(count, 1).Value = registros.Id_Rem;
                        ExcelDCVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDCVencida.Cell(count, 2).Value = registros.Rem_Fecha;
                        ExcelDCVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDCVencida.Cell(count, 3).Value = registros.Id_Prd;
                        ExcelDCVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDCVencida.Cell(count, 4).Value = registros.Prd_Descripcion;
                        ExcelDCVencida.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDCVencida.Cell(count, 5).Value = registros.SaldoUnidades;
                        ExcelDCVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        double total = registros.SaldoUnidades * registros.Prd_Pesos;
                        ExcelDCVencida.Cell(count, 6).Value = total.ToString("C2");
                        ExcelDCVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                        totalCte = totalCte + total;
                        TotalMAtriz = TotalMAtriz + total;
                        TotalGral = TotalGral + total;
                        count = count + 1;
                    }
                    else
                    {
                        if (id_cte != registros.Id_Cte)
                        {
                            ExcelDCVencida.Cell(count, 5).Value = "Total por Cliente";
                            ExcelDCVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDCVencida.Cell(count, 6).Value = totalCte.ToString("C2");
                            ExcelDCVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            count = count + 1;
                            totalCte = 0;

                            id_cte = registros.Id_Cte;
                            ExcelDCVencida.Cell(count, 1).Value = "Cliente";
                            ExcelDCVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDCVencida.Cell(count, 2).Value = registros.Id_Cte;
                            ExcelDCVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDCVencida.Range(count, 3, count, 4).Merge();
                            ExcelDCVencida.Cell(count, 3).Value = registros.Cte_NomComercial;
                            ExcelDCVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            count = count + 1;

                            ExcelDCVencida.Cell(count, 1).Value = registros.Id_Rem;
                            ExcelDCVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDCVencida.Cell(count, 2).Value = registros.Rem_Fecha;
                            ExcelDCVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDCVencida.Cell(count, 3).Value = registros.Id_Prd;
                            ExcelDCVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDCVencida.Cell(count, 4).Value = registros.Prd_Descripcion;
                            ExcelDCVencida.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDCVencida.Cell(count, 5).Value = registros.SaldoUnidades;
                            ExcelDCVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            double total = registros.SaldoUnidades * registros.Prd_Pesos;
                            ExcelDCVencida.Cell(count, 6).Value = total.ToString("C2");
                            ExcelDCVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                            totalCte = totalCte + total;
                            TotalMAtriz = TotalMAtriz + total;
                            TotalGral = TotalGral + total;
                            count = count + 1;
                        }
                        else
                        {
                            ExcelDCVencida.Cell(count, 1).Value = registros.Id_Rem;
                            ExcelDCVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDCVencida.Cell(count, 2).Value = registros.Rem_Fecha;
                            ExcelDCVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDCVencida.Cell(count, 3).Value = registros.Id_Prd;
                            ExcelDCVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDCVencida.Cell(count, 4).Value = registros.Prd_Descripcion;
                            ExcelDCVencida.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDCVencida.Cell(count, 5).Value = registros.SaldoUnidades;
                            ExcelDCVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            double total = registros.SaldoUnidades * registros.Prd_Pesos;
                            ExcelDCVencida.Cell(count, 6).Value = total.ToString("C2");
                            ExcelDCVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                            totalCte = totalCte + total;
                            TotalMAtriz = TotalMAtriz + total;
                            TotalGral = TotalGral + total;
                            count = count + 1;
                        }
                    }
                }

                if (TotalMAtriz != 0)
                {
                    ExcelDCVencida.Cell(count, 5).Value = "Total por Cliente";
                    ExcelDCVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    ExcelDCVencida.Cell(count, 6).Value = totalCte.ToString("C2");
                    ExcelDCVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    totalCte = 0;

                    ExcelDCVencida.Cell(count, 5).Value = "Total por Territorio";
                    ExcelDCVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    ExcelDCVencida.Cell(count, 6).Value = TotalMAtriz.ToString("C2");
                    ExcelDCVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    TotalMAtriz = 0;

                    ExcelDCVencida.Cell(count, 5).Value = "Total";
                    ExcelDCVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    ExcelDCVencida.Cell(count, 6).Value = TotalGral.ToString("C2");
                    ExcelDCVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    TotalGral = 0;
                }

                #endregion
                ExcelDCVencida.Columns().AdjustToContents();
                ExcelDCVencida.PageSetup.PageOrientation = XLPageOrientation.Portrait;
                ExcelDCVencida.PageSetup.Margins.Top = 0.5;
                ExcelDCVencida.PageSetup.Margins.Bottom = 0.3;
                ExcelDCVencida.PageSetup.Margins.Left = 0.5;
                ExcelDCVencida.PageSetup.Margins.Right = 0.3;
                ExcelDCVencida.PageSetup.Margins.Footer = 0;
                ExcelDCVencida.PageSetup.Margins.Header = 0;
                ExcelDCVencida.PageSetup.PagesWide = 1;
                ExcelDCVencida.PageSetup.PaperSize = XLPaperSize.A4Paper;
                ExcelDCVencida.PageSetup.VerticalDpi = 600;
                ExcelDCVencida.PageSetup.HorizontalDpi = 600;
                #endregion

                //Reporte diferencia de embarque vigente mov 79
                #region diferencia de embarque vigente
                ReporteRemisiones DatosDEVIG = new ReporteRemisiones();
                List<ReporteRemisiones> listaDEVIG = new List<ReporteRemisiones>();
                DatosDEVIG.Id_Emp = Sesion.Id_Emp;
                DatosDEVIG.Id_Cd = Sesion.Id_Cd_Ver;
                DatosDEVIG.Vencido = 0;
                DatosDEVIG.FechaIni = fechainicial;
                DatosDEVIG.FechaFin = fechafinal;
                DatosDEVIG.TipoRemision = 79;
                CN.Rep_RemisionesVencidas(DatosDEVIG, ref listaDEVIG, Sesion.Emp_Cnx);


                var ExcelDEVIG = workbook.Worksheets.Add("DIFERENCIAEMBCEDIS_VIG");

                #region Encabezado

                ExcelDEVIG.Cell(1, 1).Value = "Sucursal";
                ExcelDEVIG.Cell(1, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDEVIG.Cell(1, 2).Value = Sesion.Id_Cd_Ver + " - " + Sesion.Cd_Nombre;
                ExcelDEVIG.Cell(1, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDEVIG.Cell(1, 4).Value = Emp_Nombre;
                ExcelDEVIG.Cell(1, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelDEVIG.Cell(2, 1).Value = "Usuario";
                ExcelDEVIG.Cell(2, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDEVIG.Cell(2, 2).Value = Sesion.U_Nombre;
                ExcelDEVIG.Cell(2, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDEVIG.Cell(2, 4).Value = "Control de Remisiones";
                ExcelDEVIG.Cell(2, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelDEVIG.Cell(3, 1).Value = "Fecha del Documento";
                ExcelDEVIG.Cell(3, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDEVIG.Cell(3, 2).Value = DateTime.Now.ToString("dd/MM/yyy");
                ExcelDEVIG.Cell(3, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelDEVIG.Cell(4, 1).Value = "Fecha Inical";
                ExcelDEVIG.Cell(4, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDEVIG.Cell(4, 2).Value = fechainicial.ToString("dd/MM/yyy"); ;
                ExcelDEVIG.Cell(4, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelDEVIG.Cell(5, 1).Value = "Fecha Final";
                ExcelDEVIG.Cell(5, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDEVIG.Cell(5, 2).Value = fechafinal.ToString("dd/MM/yyy");
                ExcelDEVIG.Cell(5, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelDEVIG.Cell(6, 1).Value = "Cliente";
                ExcelDEVIG.Cell(6, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDEVIG.Cell(6, 2).Value = "Todos";
                ExcelDEVIG.Cell(6, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelDEVIG.Cell(7, 1).Value = "Territorio";
                ExcelDEVIG.Cell(7, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDEVIG.Cell(7, 2).Value = "Todos";
                ExcelDEVIG.Cell(7, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelDEVIG.Cell(8, 1).Value = "Vencidos";
                ExcelDEVIG.Cell(8, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDEVIG.Cell(8, 2).Value = "NO";
                ExcelDEVIG.Cell(8, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelDEVIG.Cell(9, 1).Value = "Nivel";
                ExcelDEVIG.Cell(9, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDEVIG.Cell(9, 2).Value = "Detalle";
                ExcelDEVIG.Cell(9, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelDEVIG.Cell(10, 1).Value = "Tipo de Remisión";
                ExcelDEVIG.Cell(10, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDEVIG.Range(10, 2, 10, 4).Merge();
                ExcelDEVIG.Range(10, 2, 10, 4).Value = "Producto Diferencia Embarque a Cedis";
                ExcelDEVIG.Range(10, 2, 10, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                #endregion

                #region Cuerpo

                ExcelDEVIG.Cell(12, 1).Value = "Remisión";
                ExcelDEVIG.Cell(12, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelDEVIG.Cell(12, 2).Value = "Fecha";
                ExcelDEVIG.Cell(12, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelDEVIG.Cell(12, 3).Value = "Núm de Producto";
                ExcelDEVIG.Cell(12, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelDEVIG.Cell(12, 4).Value = "Descripción de Producto";
                ExcelDEVIG.Cell(12, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelDEVIG.Cell(12, 5).Value = "Unidades";
                ExcelDEVIG.Cell(12, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelDEVIG.Cell(12, 6).Value = "Importe";
                ExcelDEVIG.Cell(12, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                count = 13;
                idMatriz = -1;
                id_cte = -1;
                totalCte = 0;
                TotalMAtriz = 0;
                TotalGral = 0;

                foreach (ReporteRemisiones registros in listaDEVIG)
                {
                    if (idMatriz == -1)
                    {
                        idMatriz = registros.Id_Ter;
                        ExcelDEVIG.Cell(count, 1).Value = "Territorio";
                        ExcelDEVIG.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDEVIG.Cell(count, 2).Value = registros.Id_Ter;
                        ExcelDEVIG.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDEVIG.Range(count, 3, count, 4).Merge();
                        ExcelDEVIG.Cell(count, 3).Value = registros.Ter_Nombre;
                        ExcelDEVIG.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                    }
                    if (id_cte == -1)
                    {
                        id_cte = registros.Id_Cte;

                        ExcelDEVIG.Cell(count, 1).Value = "Cliente";
                        ExcelDEVIG.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDEVIG.Cell(count, 2).Value = registros.Id_Cte;
                        ExcelDEVIG.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDEVIG.Range(count, 3, count, 4).Merge();
                        ExcelDEVIG.Cell(count, 3).Value = registros.Cte_NomComercial;
                        ExcelDEVIG.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                    }

                    if (idMatriz != registros.Id_Ter)
                    {
                        ExcelDEVIG.Cell(count, 5).Value = "Total por Cliente";
                        ExcelDEVIG.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDEVIG.Cell(count, 6).Value = totalCte.ToString("C2");
                        ExcelDEVIG.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                        totalCte = 0;

                        ExcelDEVIG.Cell(count, 5).Value = "Total por Territorio";
                        ExcelDEVIG.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDEVIG.Cell(count, 6).Value = TotalMAtriz.ToString("C2");
                        ExcelDEVIG.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                        TotalMAtriz = 0;

                        idMatriz = registros.Id_Ter;
                        ExcelDEVIG.Cell(count, 1).Value = "Territorio";
                        ExcelDEVIG.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDEVIG.Cell(count, 2).Value = registros.Id_Ter;
                        ExcelDEVIG.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDEVIG.Range(count, 3, count, 4).Merge();
                        ExcelDEVIG.Cell(count, 3).Value = registros.Ter_Nombre;
                        ExcelDEVIG.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;

                        id_cte = registros.Id_Cte;
                        ExcelDEVIG.Cell(count, 1).Value = "Cliente";
                        ExcelDEVIG.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDEVIG.Cell(count, 2).Value = registros.Id_Cte;
                        ExcelDEVIG.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDEVIG.Range(count, 3, count, 4).Merge();
                        ExcelDEVIG.Cell(count, 3).Value = registros.Cte_NomComercial;
                        ExcelDEVIG.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;

                        ExcelDEVIG.Cell(count, 1).Value = registros.Id_Rem;
                        ExcelDEVIG.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDEVIG.Cell(count, 2).Value = registros.Rem_Fecha;
                        ExcelDEVIG.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDEVIG.Cell(count, 3).Value = registros.Id_Prd;
                        ExcelDEVIG.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDEVIG.Cell(count, 4).Value = registros.Prd_Descripcion;
                        ExcelDEVIG.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDEVIG.Cell(count, 5).Value = registros.SaldoUnidades;
                        ExcelDEVIG.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        double total = registros.SaldoUnidades * registros.Prd_Pesos;
                        ExcelDEVIG.Cell(count, 6).Value = total.ToString("C2");
                        ExcelDEVIG.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                        totalCte = totalCte + total;
                        TotalMAtriz = TotalMAtriz + total;
                        TotalGral = TotalGral + total;
                        count = count + 1;
                    }
                    else
                    {
                        if (id_cte != registros.Id_Cte)
                        {
                            ExcelDEVIG.Cell(count, 5).Value = "Total por Cliente";
                            ExcelDEVIG.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDEVIG.Cell(count, 6).Value = totalCte.ToString("C2");
                            ExcelDEVIG.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            count = count + 1;
                            totalCte = 0;

                            id_cte = registros.Id_Cte;
                            ExcelDEVIG.Cell(count, 1).Value = "Cliente";
                            ExcelDEVIG.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDEVIG.Cell(count, 2).Value = registros.Id_Cte;
                            ExcelDEVIG.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDEVIG.Range(count, 3, count, 4).Merge();
                            ExcelDEVIG.Cell(count, 3).Value = registros.Cte_NomComercial;
                            ExcelDEVIG.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            count = count + 1;

                            ExcelDEVIG.Cell(count, 1).Value = registros.Id_Rem;
                            ExcelDEVIG.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDEVIG.Cell(count, 2).Value = registros.Rem_Fecha;
                            ExcelDEVIG.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDEVIG.Cell(count, 3).Value = registros.Id_Prd;
                            ExcelDEVIG.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDEVIG.Cell(count, 4).Value = registros.Prd_Descripcion;
                            ExcelDEVIG.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDEVIG.Cell(count, 5).Value = registros.SaldoUnidades;
                            ExcelDEVIG.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            double total = registros.SaldoUnidades * registros.Prd_Pesos;
                            ExcelDEVIG.Cell(count, 6).Value = total.ToString("C2");
                            ExcelDEVIG.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                            totalCte = totalCte + total;
                            TotalMAtriz = TotalMAtriz + total;
                            TotalGral = TotalGral + total;
                            count = count + 1;
                        }
                        else
                        {
                            ExcelDEVIG.Cell(count, 1).Value = registros.Id_Rem;
                            ExcelDEVIG.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDEVIG.Cell(count, 2).Value = registros.Rem_Fecha;
                            ExcelDEVIG.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDEVIG.Cell(count, 3).Value = registros.Id_Prd;
                            ExcelDEVIG.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDEVIG.Cell(count, 4).Value = registros.Prd_Descripcion;
                            ExcelDEVIG.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDEVIG.Cell(count, 5).Value = registros.SaldoUnidades;
                            ExcelDEVIG.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            double total = registros.SaldoUnidades * registros.Prd_Pesos;
                            ExcelDEVIG.Cell(count, 6).Value = total.ToString("C2");
                            ExcelDEVIG.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                            totalCte = totalCte + total;
                            TotalMAtriz = TotalMAtriz + total;
                            TotalGral = TotalGral + total;
                            count = count + 1;
                        }
                    }
                }

                if (TotalMAtriz != 0)
                {
                    ExcelDEVIG.Cell(count, 5).Value = "Total por Cliente";
                    ExcelDEVIG.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    ExcelDEVIG.Cell(count, 6).Value = totalCte.ToString("C2");
                    ExcelDEVIG.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    totalCte = 0;

                    ExcelDEVIG.Cell(count, 5).Value = "Total por Territorio";
                    ExcelDEVIG.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    ExcelDEVIG.Cell(count, 6).Value = TotalMAtriz.ToString("C2");
                    ExcelDEVIG.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    TotalMAtriz = 0;

                    ExcelDEVIG.Cell(count, 5).Value = "Total";
                    ExcelDEVIG.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    ExcelDEVIG.Cell(count, 6).Value = TotalGral.ToString("C2");
                    ExcelDEVIG.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    TotalGral = 0;
                }

                #endregion
                ExcelDEVIG.Columns().AdjustToContents();
                ExcelDEVIG.PageSetup.PageOrientation = XLPageOrientation.Portrait;
                ExcelDEVIG.PageSetup.Margins.Top = 0.5;
                ExcelDEVIG.PageSetup.Margins.Bottom = 0.3;
                ExcelDEVIG.PageSetup.Margins.Left = 0.5;
                ExcelDEVIG.PageSetup.Margins.Right = 0.3;
                ExcelDEVIG.PageSetup.Margins.Footer = 0;
                ExcelDEVIG.PageSetup.Margins.Header = 0;
                ExcelDEVIG.PageSetup.PagesWide = 1;
                ExcelDEVIG.PageSetup.PaperSize = XLPaperSize.A4Paper;
                ExcelDEVIG.PageSetup.VerticalDpi = 600;
                ExcelDEVIG.PageSetup.HorizontalDpi = 600;

                #endregion
                //Reporte diferencia de embarque vencido mov 24
                #region diferencia de embarque vencido

                ReporteRemisiones DatosDEVencida = new ReporteRemisiones();
                List<ReporteRemisiones> listaDEVencida = new List<ReporteRemisiones>();
                DatosDEVencida.Id_Emp = Sesion.Id_Emp;
                DatosDEVencida.Id_Cd = Sesion.Id_Cd_Ver;
                DatosDEVencida.Vencido = 1;
                DatosDEVencida.FechaIni = fechainicial;
                DatosDEVencida.FechaFin = fechafinal;
                DatosDEVencida.TipoRemision = 79;
                CN.Rep_RemisionesVencidas(DatosDEVencida, ref listaDEVencida, Sesion.Emp_Cnx);


                var ExcelDEVencida = workbook.Worksheets.Add("DIFERENCIAEMBCEDIS_VEN");

                #region Encabezado

                ExcelDEVencida.Cell(1, 1).Value = "Sucursal";
                ExcelDEVencida.Cell(1, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDEVencida.Cell(1, 2).Value = Sesion.Id_Cd_Ver + " - " + Sesion.Cd_Nombre;
                ExcelDEVencida.Cell(1, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDEVencida.Cell(1, 4).Value = Emp_Nombre;
                ExcelDEVencida.Cell(1, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelDEVencida.Cell(2, 1).Value = "Usuario";
                ExcelDEVencida.Cell(2, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDEVencida.Cell(2, 2).Value = Sesion.U_Nombre;
                ExcelDEVencida.Cell(2, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDEVencida.Cell(2, 4).Value = "Control de Remisiones";
                ExcelDEVencida.Cell(2, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelDEVencida.Cell(3, 1).Value = "Fecha del Documento";
                ExcelDEVencida.Cell(3, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDEVencida.Cell(3, 2).Value = DateTime.Now.ToString("dd/MM/yyy");
                ExcelDEVencida.Cell(3, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelDEVencida.Cell(4, 1).Value = "Fecha Inical";
                ExcelDEVencida.Cell(4, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDEVencida.Cell(4, 2).Value = fechainicial.ToString("dd/MM/yyy"); ;
                ExcelDEVencida.Cell(4, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelDEVencida.Cell(5, 1).Value = "Fecha Final";
                ExcelDEVencida.Cell(5, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDEVencida.Cell(5, 2).Value = fechafinal.ToString("dd/MM/yyy");
                ExcelDEVencida.Cell(5, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelDEVencida.Cell(6, 1).Value = "Cliente";
                ExcelDEVencida.Cell(6, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDEVencida.Cell(6, 2).Value = "Todos";
                ExcelDEVencida.Cell(6, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelDEVencida.Cell(7, 1).Value = "Territorio";
                ExcelDEVencida.Cell(7, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDEVencida.Cell(7, 2).Value = "Todos";
                ExcelDEVencida.Cell(7, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelDEVencida.Cell(8, 1).Value = "Vencidos";
                ExcelDEVencida.Cell(8, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDEVencida.Cell(8, 2).Value = "SI";
                ExcelDEVencida.Cell(8, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelDEVencida.Cell(9, 1).Value = "Nivel";
                ExcelDEVencida.Cell(9, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDEVencida.Cell(9, 2).Value = "Detalle";
                ExcelDEVencida.Cell(9, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                ExcelDEVencida.Cell(10, 1).Value = "Tipo de Remisión";
                ExcelDEVencida.Cell(10, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                ExcelDEVencida.Range(10, 2, 10, 4).Merge();
                ExcelDEVencida.Range(10, 2, 10, 4).Value = "Diferencia embarques vencidas";
                ExcelDEVencida.Range(10, 2, 10, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                #endregion

                #region Cuerpo

                ExcelDEVencida.Cell(12, 1).Value = "Remisión";
                ExcelDEVencida.Cell(12, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelDEVencida.Cell(12, 2).Value = "Fecha";
                ExcelDEVencida.Cell(12, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelDEVencida.Cell(12, 3).Value = "Núm de Producto";
                ExcelDEVencida.Cell(12, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelDEVencida.Cell(12, 4).Value = "Descripción de Producto";
                ExcelDEVencida.Cell(12, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelDEVencida.Cell(12, 5).Value = "Unidades";
                ExcelDEVencida.Cell(12, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ExcelDEVencida.Cell(12, 6).Value = "Importe";
                ExcelDEVencida.Cell(12, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                count = 13;
                idMatriz = -1;
                id_cte = -1;
                totalCte = 0;
                TotalMAtriz = 0;
                TotalGral = 0;

                foreach (ReporteRemisiones registros in listaDEVencida)
                {
                    if (idMatriz == -1)
                    {
                        idMatriz = registros.Id_Ter;
                        ExcelDEVencida.Cell(count, 1).Value = "Territorio";
                        ExcelDEVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDEVencida.Cell(count, 2).Value = registros.Id_Ter;
                        ExcelDEVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDEVencida.Range(count, 3, count, 4).Merge();
                        ExcelDEVencida.Cell(count, 3).Value = registros.Ter_Nombre;
                        ExcelDEVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                    }
                    if (id_cte == -1)
                    {
                        id_cte = registros.Id_Cte;

                        ExcelDEVencida.Cell(count, 1).Value = "Cliente";
                        ExcelDEVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDEVencida.Cell(count, 2).Value = registros.Id_Cte;
                        ExcelDEVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDEVencida.Range(count, 3, count, 4).Merge();
                        ExcelDEVencida.Cell(count, 3).Value = registros.Cte_NomComercial;
                        ExcelDEVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                    }

                    if (idMatriz != registros.Id_Ter)
                    {
                        ExcelDEVencida.Cell(count, 5).Value = "Total por Cliente";
                        ExcelDEVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDEVencida.Cell(count, 6).Value = totalCte.ToString("C2");
                        ExcelDEVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                        totalCte = 0;

                        ExcelDEVencida.Cell(count, 5).Value = "Total por Territorio";
                        ExcelDEVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDEVencida.Cell(count, 6).Value = TotalMAtriz.ToString("C2");
                        ExcelDEVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;
                        TotalMAtriz = 0;

                        idMatriz = registros.Id_Ter;
                        ExcelDEVencida.Cell(count, 1).Value = "Territorio";
                        ExcelDEVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDEVencida.Cell(count, 2).Value = registros.Id_Ter;
                        ExcelDEVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDEVencida.Range(count, 3, count, 4).Merge();
                        ExcelDEVencida.Cell(count, 3).Value = registros.Ter_Nombre;
                        ExcelDEVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;

                        id_cte = registros.Id_Cte;
                        ExcelDEVencida.Cell(count, 1).Value = "Cliente";
                        ExcelDEVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDEVencida.Cell(count, 2).Value = registros.Id_Cte;
                        ExcelDEVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDEVencida.Range(count, 3, count, 4).Merge();
                        ExcelDEVencida.Cell(count, 3).Value = registros.Cte_NomComercial;
                        ExcelDEVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        count = count + 1;

                        ExcelDEVencida.Cell(count, 1).Value = registros.Id_Rem;
                        ExcelDEVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDEVencida.Cell(count, 2).Value = registros.Rem_Fecha;
                        ExcelDEVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDEVencida.Cell(count, 3).Value = registros.Id_Prd;
                        ExcelDEVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDEVencida.Cell(count, 4).Value = registros.Prd_Descripcion;
                        ExcelDEVencida.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        ExcelDEVencida.Cell(count, 5).Value = registros.SaldoUnidades;
                        ExcelDEVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                        double total = registros.SaldoUnidades * registros.Prd_Pesos;
                        ExcelDEVencida.Cell(count, 6).Value = total.ToString("C2");
                        ExcelDEVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                        totalCte = totalCte + total;
                        TotalMAtriz = TotalMAtriz + total;
                        TotalGral = TotalGral + total;
                        count = count + 1;
                    }
                    else
                    {
                        if (id_cte != registros.Id_Cte)
                        {
                            ExcelDEVencida.Cell(count, 5).Value = "Total por Cliente";
                            ExcelDEVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDEVencida.Cell(count, 6).Value = totalCte.ToString("C2");
                            ExcelDEVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            count = count + 1;
                            totalCte = 0;

                            id_cte = registros.Id_Cte;
                            ExcelDEVencida.Cell(count, 1).Value = "Cliente";
                            ExcelDEVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDEVencida.Cell(count, 2).Value = registros.Id_Cte;
                            ExcelDEVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDEVencida.Range(count, 3, count, 4).Merge();
                            ExcelDEVencida.Cell(count, 3).Value = registros.Cte_NomComercial;
                            ExcelDEVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            count = count + 1;

                            ExcelDEVencida.Cell(count, 1).Value = registros.Id_Rem;
                            ExcelDEVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDEVencida.Cell(count, 2).Value = registros.Rem_Fecha;
                            ExcelDEVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDEVencida.Cell(count, 3).Value = registros.Id_Prd;
                            ExcelDEVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDEVencida.Cell(count, 4).Value = registros.Prd_Descripcion;
                            ExcelDEVencida.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDEVencida.Cell(count, 5).Value = registros.SaldoUnidades;
                            ExcelDEVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            double total = registros.SaldoUnidades * registros.Prd_Pesos;
                            ExcelDEVencida.Cell(count, 6).Value = total.ToString("C2");
                            ExcelDEVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                            totalCte = totalCte + total;
                            TotalMAtriz = TotalMAtriz + total;
                            TotalGral = TotalGral + total;
                            count = count + 1;
                        }
                        else
                        {
                            ExcelDEVencida.Cell(count, 1).Value = registros.Id_Rem;
                            ExcelDEVencida.Cell(count, 1).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDEVencida.Cell(count, 2).Value = registros.Rem_Fecha;
                            ExcelDEVencida.Cell(count, 2).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDEVencida.Cell(count, 3).Value = registros.Id_Prd;
                            ExcelDEVencida.Cell(count, 3).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDEVencida.Cell(count, 4).Value = registros.Prd_Descripcion;
                            ExcelDEVencida.Cell(count, 4).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            ExcelDEVencida.Cell(count, 5).Value = registros.SaldoUnidades;
                            ExcelDEVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                            double total = registros.SaldoUnidades * registros.Prd_Pesos;
                            ExcelDEVencida.Cell(count, 6).Value = total.ToString("C2");
                            ExcelDEVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;

                            totalCte = totalCte + total;
                            TotalMAtriz = TotalMAtriz + total;
                            TotalGral = TotalGral + total;
                            count = count + 1;
                        }
                    }
                }

                if (TotalMAtriz != 0)
                {
                    ExcelDEVencida.Cell(count, 5).Value = "Total por Cliente";
                    ExcelDEVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    ExcelDEVencida.Cell(count, 6).Value = totalCte.ToString("C2");
                    ExcelDEVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    totalCte = 0;

                    ExcelDEVencida.Cell(count, 5).Value = "Total por Territorio";
                    ExcelDEVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    ExcelDEVencida.Cell(count, 6).Value = TotalMAtriz.ToString("C2");
                    ExcelDEVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    TotalMAtriz = 0;

                    ExcelDEVencida.Cell(count, 5).Value = "Total";
                    ExcelDEVencida.Cell(count, 5).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    ExcelDEVencida.Cell(count, 6).Value = TotalGral.ToString("C2");
                    ExcelDEVencida.Cell(count, 6).Style.Border.OutsideBorder = XLBorderStyleValues.None;
                    count = count + 1;
                    TotalGral = 0;
                }

                #endregion
                ExcelDEVencida.Columns().AdjustToContents();
                ExcelDEVencida.PageSetup.PageOrientation = XLPageOrientation.Portrait;
                ExcelDEVencida.PageSetup.Margins.Top = 0.5;
                ExcelDEVencida.PageSetup.Margins.Bottom = 0.3;
                ExcelDEVencida.PageSetup.Margins.Left = 0.5;
                ExcelDEVencida.PageSetup.Margins.Right = 0.3;
                ExcelDEVencida.PageSetup.Margins.Footer = 0;
                ExcelDEVencida.PageSetup.Margins.Header = 0;
                ExcelDEVencida.PageSetup.PagesWide = 1;
                ExcelDEVencida.PageSetup.PaperSize = XLPaperSize.A4Paper;
                ExcelDEVencida.PageSetup.VerticalDpi = 600;
                ExcelDEVencida.PageSetup.HorizontalDpi = 600;
                #endregion


                string rutaguardado = Server.MapPath("~/Reportes/") + "ReporteRemisiones-" + Sesion.Cd_Nombre + "-" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";

                if (File.Exists(rutaguardado))
                {
                    File.Delete(rutaguardado);
                }

                workbook.SaveAs(rutaguardado);
                string Outgoingfile = "ReporteRemisiones-" + Sesion.Cd_Nombre + "-" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";
                string ruta = Server.MapPath("~/Reportes/") + "ReporteRemisiones-" + Sesion.Cd_Nombre + "-" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";
                // Prepare the response
                HttpResponse httpResponse = Response;
                httpResponse.Clear();
                httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                httpResponse.AddHeader("Content-Disposition", "attachment; filename=" + Outgoingfile);



                // Flush the workbook to the Response.OutputStream
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    workbook.SaveAs(memoryStream);
                    memoryStream.WriteTo(httpResponse.OutputStream);
                    memoryStream.Close();
                }

                httpResponse.End();
            }
        }
        #endregion
    }
}