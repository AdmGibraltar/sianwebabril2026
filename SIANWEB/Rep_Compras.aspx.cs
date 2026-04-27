using System.Web;
using System;
using CapaEntidad;
using CapaDatos;
using CapaNegocios;
using System.IO;
using System.Web.Services;
using DevExpress.XtraCharts.Native;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Configuration;
using Dapper;
using System.Linq;
using System.Collections;
using Telerik.Reporting.Processing;

namespace SIANWEB
{
    public partial class Rep_Compras : System.Web.UI.Page
    {
        private static readonly string conn = ConfigurationManager.AppSettings.Get("strConnection");

        private static bool PermisoImprimir
        {
            get
            {
                if (HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID] == null) return false;
                return (bool)HttpContext.Current.Session["PermisoImprimir" + HttpContext.Current.Session.SessionID + HttpContext.Current.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]];
            }
            set
            {
                HttpContext.Current.Session["PermisoImprimir" + HttpContext.Current.Session.SessionID + HttpContext.Current.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var sesion = (Sesion)Session[$"Sesion{Session.SessionID}"];
                if (sesion == null)
                {
                    string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        appIIsName.Text = Request.ApplicationPath;
                        ValidarPermisos();
                    }
                }
            }
            catch { }
        }

        private void ValidarPermisos()
        {
            try
            {
                var sesion = new Sesion();
                sesion = (Sesion)Session[$"Sesion{Session.SessionID}"];

                var pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "? " }, StringSplitOptions.RemoveEmptyEntries);
                if (pag.Length > 1)
                    pagina.Url = $"{new FileInfo(Page.Request.Url.AbsolutePath).Name}?{pag[1]}";
                else pagina.Url = new FileInfo(Page.Request.Url.AbsolutePath).Name;

                new CN_Pagina().PaginaConsultar(ref pagina, sesion.Emp_Cnx);

                var permiso = new Permiso
                {
                    Id_U = sesion.Id_U,
                    Id_Cd = sesion.Id_Cd,
                    Sm_cve = pagina.Clave
                };

                new CD_PermisosU().ValidaPermisosUsuario(ref permiso, sesion.Emp_Cnx);
                if (permiso.PAccesar)
                {
                    PermisoImprimir = permiso.PImprimir;
                }
                else Response.Redirect("Inicio.aspx");
            }
            catch { }
        }











        [WebMethod]
        public static List<SelectModel> GetProveedores()
        {
            var result = new List<SelectModel>();
            try
            {
                using (var db = new SqlConnection(conn))
                {
                    db.Open();

                    string query = "select Id_Pvd, Pvd_Descripcion from CatProveedor where Pvd_Activo = 1";

                    result = db.Query(query).Select(x => new SelectModel
                    {
                        Id = x.Id_Pvd,
                        Description = x.Pvd_Descripcion
                    }).ToList();
                }
            }
            catch { }
            return result;
        }

        [WebMethod]
        [Obsolete]
        public static ResponseJson GetReport(ModelReporte model)
        {
            var result = new ResponseJson();
            try
            {
                if (!PermisoImprimir)
                {
                    result.Mensaje = "No tiene permisos para imprimir";
                    return result;
                }

                var sesion = new Sesion();
                sesion = (Sesion)HttpContext.Current.Session[$"Sesion{HttpContext.Current.Session.SessionID}"];

                ArrayList parameter = new ArrayList
                {
                    model.Proveedor == "-1" ? null : model.Proveedor,
                    model.FechaInicio.Split('T')[0],
                    model.FechaFinal.Split('T')[0],
                    sesion.Emp_Cnx
                };
                Type instance = null;
                instance = typeof(LibreriaReportes.Rep_Compras);


                result.Estatus = true;
                result.Mensaje = instance.Name + ".xls";
                ImprimirXLS(parameter, instance);
            }
            catch (Exception ex) { result.Mensaje = ex.Message; }
            return result;
        }

        [Obsolete]
        static void ImprimirXLS(ArrayList list, Type instance)
        {
            try
            {
                Telerik.Reporting.Report report1 = (Telerik.Reporting.Report)Activator.CreateInstance(instance);
                for (int i = 0; i <= list.Count - 1; i++)
                {
                    report1.ReportParameters[i].AllowNull = true;
                    report1.ReportParameters[i].Value = list[i];
                }
                ReportProcessor reportProcessor = new ReportProcessor();
                RenderingResult result = reportProcessor.RenderReport("XLS", report1, null);
                string ruta = HttpContext.Current.Server.MapPath("Reportes") + "\\" + instance.Name + ".xls";
                if (File.Exists(ruta))
                    File.Delete(ruta);
                FileStream fs = new FileStream(ruta, FileMode.Create);
                fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);
                fs.Flush();
                fs.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region models
        public class ResponseJson
        {
            public string Mensaje { get; set; }
            public bool Estatus { get; set; } = false;
        }

        public class ModelReporte
        {
            public string Proveedor { get; set; }
            public string FechaInicio { get; set; }
            public string FechaFinal { get; set; }
        }

        public class SelectModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
        }
        #endregion
    }
}