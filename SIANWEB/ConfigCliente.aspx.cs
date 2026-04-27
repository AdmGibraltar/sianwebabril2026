using CapaDatos;
using CapaEntidad;
using CapaNegocios;
using Dapper;
using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;


namespace SIANWEB
{
    public partial class ConfigCliente : System.Web.UI.Page
    {
        static readonly string conn = ConfigurationManager.AppSettings["strConnection"];
        static bool PermisoModificar
        {
            get { return HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID] != null && (bool)HttpContext.Current.Session["PermisoModificar" + HttpContext.Current.Session.SessionID + HttpContext.Current.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; }
            set { HttpContext.Current.Session["PermisoModificar" + HttpContext.Current.Session.SessionID + HttpContext.Current.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            var sesion = (Sesion)Session[$"Sesion{Session.SessionID}"];
            if (sesion == null)
            {
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                Response.Redirect("login.aspx", false);
            }
            else if (!Page.IsPostBack)
            {
                ValidarPermisos();
                appIIsName.Text = Request.ApplicationPath;
            }
        }

        void ValidarPermisos()
        {
            try
            {
                var currentSesion = (Sesion)Session["Sesion" + Session.SessionID];
                var pagina = new Pagina();
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                string url = (new FileInfo(Page.Request.Url.AbsolutePath)).Name + (pag.Length > 1 ? ("?" + pag[1]) : "");

                pagina.Url = url;

                new CN_Pagina().PaginaConsultar(ref pagina, currentSesion.Emp_Cnx);

                Session["Head" + Session.SessionID] = pagina.Path;

                var permiso = new Permiso
                {
                    Id_U = currentSesion.Id_U,
                    Id_Cd = currentSesion.Id_Cd,
                    Sm_cve = pagina.Clave
                };
                //Esta clave depende de la pantalla
                new CD_PermisosU().ValidaPermisosUsuario(ref permiso, currentSesion.Emp_Cnx);

                if (permiso.PAccesar)
                {
                    PermisoModificar = permiso.PModificar;
                }
                else Response.Redirect("Inicio.aspx");
            }
            catch { }
        }

        public class ConfigClientModel
        {
            public int ClientId { get; set; }
            public string ClientName { get; set; }
            public bool AllowsBilling { get; set; }
            public bool CreditSuspended { get; set; }
        }

        public class ResponseJson
        {
            public string Message { get; set; }
            public bool Status { get; set; }
        }

        [WebMethod]
        public static List<ConfigClientModel> GetLast10Clients(string search)
        {
            var clients = new List<ConfigClientModel>();

            string condition = "";
            if (search != "")
            {
                var isNumeric = int.TryParse(search, out int n);
                if (isNumeric) condition = $" where cast(id_cte as varchar) like '{search}%'";
                else condition = $" where cte_nomcomercial like '%{search}%'";
            }

            using (var db = new SqlConnection(conn))
            {
                db.Open();
                
                string query = $@"
                select top(10) id_cte, cte_nomComercial, cte_facturacion, cte_creditosuspendido 
                from catcliente 
                {condition} 
                order by id_cte desc";

                clients = db.Query(query)
                    .Select(x => new ConfigClientModel
                    {
                        ClientId = x.id_cte,
                        ClientName = x.cte_nomComercial,
                        AllowsBilling = x.cte_facturacion,
                        CreditSuspended = x.cte_creditosuspendido
                    }).ToList();
            }
            return clients;
        }

        [WebMethod]
        public static ResponseJson ChangeBillingStatus(int clientId, bool status)
        {
            var result = new ResponseJson();
            var currentSesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];

            using (IDbConnection db = new SqlConnection(conn))
            {
                db.Open();
                try
                {
                    string query = @"update catcliente 
                    set 
                    cte_facturacion = @status,
                    cte_modfecha = @date,
                    id_umod = @user 
                    where id_cte = @id";
                    db.Execute(query, new
                    {
                        status,
                        date = DateTime.Now,
                        user = currentSesion.Id_U,
                        id = clientId
                    });
                    result.Status = true;
                }
                catch (Exception ex) { result.Message = ex.Message; }
            }

            return result;
        }

        [WebMethod]
        public static ResponseJson ChangeCreditSuspended(int clientId, bool status)
        {
            var result = new ResponseJson();
            var currentSesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];

            using (IDbConnection db = new SqlConnection(conn))
            {
                db.Open();
                try
                {
                    string query = @"update catcliente 
                    set 
                    cte_creditosuspendido = @status,
                    cte_modfecha = @date,
                    id_umod = @user 
                    where id_cte = @id";
                    db.Execute(query, new
                    {
                        status,
                        date = DateTime.Now,
                        user = currentSesion.Id_U,
                        id = clientId
                    });
                    result.Status = true;
                }
                catch (Exception ex) { result.Message = ex.Message; }
            }

            return result;
        }

        [WebMethod]
        public static ResponseJson UploadExcel(string base64, string fileName)
        {
            var result = new ResponseJson();
            var currentSesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];

            try
            {
                string path = $"{HttpContext.Current.Server.MapPath("~/App_Data/RadUploadTemp")}\\{fileName}";

                if (File.Exists(path)) File.Delete(path);
                File.WriteAllBytes(path, Convert.FromBase64String(base64));

                string strConnection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0 xml;HDR=YES;IMEX=1;\"";
                var oledb = new OleDbConnection(strConnection);
                oledb.Open();

                DataTable dt = oledb.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                OleDbCommand cmd = new OleDbCommand("select * from [" + dt.Rows[0].ItemArray[2].ToString().Replace("'", "") + "]", oledb);

                OleDbDataAdapter dad = new OleDbDataAdapter
                {
                    SelectCommand = cmd
                };
                DataSet ds = new DataSet();
                dad.Fill(ds);
                oledb.Close();

                if (ds != null)
                {
                    var rows = ds.Tables[0].Rows.Cast<DataRow>().ToList();

                    //validaciones
                    if (!rows.Any(x => x[1].ToString() == "0" || x[1].ToString() == "1"))
                    {
                        result.Message = "El valor del campo 'PermiteFacturar' debe ser 0 o 1";
                        return result;
                    }

                    if (!rows.Any(x => x[2].ToString() == "0" || x[2].ToString() == "1"))
                    {
                        result.Message = "El valor del campo 'CreditoSuspendido' debe ser 0 o 1";
                        return result;
                    }

                    using (IDbConnection db = new SqlConnection(conn))
                    {
                        db.Open();
                        using (var transaction = db.BeginTransaction())
                        {
                            var now = DateTime.Now;
                            foreach (var row in rows)
                            {
                                var query = @"update catcliente 
                                set 
                                cte_facturacion = @status_facturacion,
                                cte_creditosuspendido = @status_creditoSuspendido,
                                cte_modfecha = @date,
                                id_umod = @user 
                                where id_cte = @id";

                                var parameters = new DynamicParameters();
                                parameters.Add("@status_facturacion", row[1].ToString());
                                parameters.Add("@status_creditoSuspendido", row[2].ToString());
                                parameters.Add("@date", now);
                                parameters.Add("@user", currentSesion.Id_U);
                                parameters.Add("@id", row[0].ToString());

                                db.Execute(query, parameters, transaction);
                            }

                            transaction.Commit();
                            result.Status = true;
                        }
                    }
                }
            }
            catch (Exception ex) { result.Message = ex.Message; }
            return result;
        }
    }
}