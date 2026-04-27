using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocios;
using Newtonsoft.Json;

namespace SIANWEB.GestionPrecios
{
    public partial class Clientesget : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string id_rik = Request.QueryString["representante"];
             string cliente = Request.QueryString["cliente"];
            string producto = Request.QueryString["producto"];
            int tipoReporte = Convert.ToInt32(Request.QueryString["tipoReporte"]);
          int idreportegp = Convert.ToInt32(Request.QueryString["idreportegp"]);

            if (IsPostBack) return;

            string action = Request.QueryString["action"];
            if (action == "getClientes")
            {
                ObtenerClientes(id_rik, cliente, producto, tipoReporte);
            }
            if (action == "getClientesdetalle")
            {
                getClientesdetalle(id_rik, cliente, producto, tipoReporte);
            }
            

        }

        private void ObtenerClientes(string id_rik, string cliente, string producto,int tipoReporte)
        {
            try
            {
                // Aquí puedes agregar lógica para obtener el parámetro de vendedor si es necesario
                
                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnection"];
                Usuario usuario = new Usuario();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_ReporteGAP cnreportegap = new CN_ReporteGAP();
                List<CapaEntidad.GestionIncrementoPrecios> listreporteGAP = new List<CapaEntidad.GestionIncrementoPrecios>();
                CapaEntidad.GestionIncrementoPrecios reporteGAP = new CapaEntidad.GestionIncrementoPrecios();

                //reporteGAP.Id_Cd = Convert.ToInt32(CmbSucursal.Value.ToString());
                reporteGAP.Id_Cd = sesion.Id_Cd_Ver; ;
                reporteGAP.IdReporteGP =   Convert.ToInt32(Request.QueryString["idreportegp"]);

                reporteGAP.Activo = 3;

                reporteGAP.Id_Emp = sesion.Id_Emp;
                if (cliente == null ||  cliente == "")
                {
                    reporteGAP.Id_Cte = -1;
                }
                else
                {
                    reporteGAP.Id_Cte = Convert.ToInt32(cliente);
                }


                if(id_rik==null || id_rik == "")
                {
                    reporteGAP.Id_Rik = -1;
                 
                }
                else
                {
                    reporteGAP.Id_Rik = Convert.ToInt32(id_rik);
                }

                if (reporteGAP.Id_Rik == 0)
                {

                    reporteGAP.Id_Rik = -1;
                }
                reporteGAP.Id_Rik = sesion.Id_TU == 2 ? Convert.ToInt32(sesion.Id_Rik.ToString()) : reporteGAP.Id_Rik;


                if (producto == "")
                {
                    reporteGAP.Id_Prd = -1;
                }
                else
                {
                    reporteGAP.Id_Prd = Convert.ToInt64(producto);
                }

                //if (ASPxDropDownEdit1.Text == "")
                //{
                reporteGAP.Id_Tamaño = "A;B;C;D;E";
                //}
                //else
                //{
                //    reporteGAP.Id_Tamaño = ASPxDropDownEdit1.Text;
                //}


                //reporteGAP.Id_Cd = Convert.ToInt32(this.CmbSucursal.SelectedItem.Value);



                ////reporteGAP.Mes = Convert.ToInt32(this.CmbMes.SelectedItem.Value);
                ////reporteGAP.Año = Convert.ToInt32(this.CmbAnio.SelectedItem.Value);
                reporteGAP.Mes = 5;
                reporteGAP.Año = 2024;
                reporteGAP.Id_TipoReporte = tipoReporte;

                List<GestionIncrementoPrecios>  clientes = new List<GestionIncrementoPrecios>();

                if (tipoReporte == 0 && reporteGAP.Id_Cte < 1)
                {
                    Response.Clear();
                    Response.ContentType = "application/json";
                    Response.Write("[]"); // JSON vacío
                    Response.Flush();
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                    return;
                }
                else
                {
                    clientes = CN_ReporteGAP.ConsultaListaReporteGAP_Producto2(reporteGAP, ref listreporteGAP, Conexion);
                }


                //ClienteBL.ObtenerClientesConVentasPromedio(usuario, id_rik, Conexion);

                //si son muchos clientes regreso vacios para qu eno marque error 
                if (clientes.Count() > 1500 &&  tipoReporte == 0 )
                {
                    // Si el tamaño es mayor que el límite, retornamos un JSON vacío
                    Response.Clear();
                    Response.ContentType = "application/json";
                    Response.Write("[]"); // JSON vacío
                    Response.Flush();
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                    return;
                }

                // Serializar a JSON y escribir la respuesta
                string json = JsonConvert.SerializeObject(clientes);


                Response.ContentType = "application/json";
                Response.Write(json);
                Response.Flush(); // Envía la salida al cliente
                HttpContext.Current.ApplicationInstance.CompleteRequest(); // Finaliza la solicitud de forma segura


            }
            catch (Exception ex)
            {
                Response.Clear();
                Response.StatusCode = 500;
                Response.ContentType = "application/json";
                var errorDetails = new
                {
                    error = ex.Message,
                    stackTrace = ex.StackTrace
                };

                Response.Write(JsonConvert.SerializeObject(errorDetails));
                Response.Flush();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }


        private void getClientesdetalle(string id_rik, string cliente, string producto, int tipoReporte)
        {
            try
            {
                // Validar si el cliente aún está conectado antes de procesar
                if (Response.IsClientConnected)
                {
                    // Aquí puedes agregar lógica para obtener el parámetro de vendedor si es necesario

                    string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnection"];
                    Usuario usuario = new Usuario();
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    CN_ReporteGAP cnreportegap = new CN_ReporteGAP();
                    List<CapaEntidad.GestionIncrementoPrecios> listreporteGAP = new List<CapaEntidad.GestionIncrementoPrecios>();
                    CapaEntidad.GestionIncrementoPrecios reporteGAP = new CapaEntidad.GestionIncrementoPrecios();

                    //reporteGAP.Id_Cd = Convert.ToInt32(CmbSucursal.Value.ToString());
                    reporteGAP.Id_Cd = sesion.Id_Cd_Ver; ;
                    reporteGAP.IdReporteGP = Convert.ToInt32(Request.QueryString["idreportegp"]);

                    reporteGAP.Activo = 3;

                    reporteGAP.Id_Emp = sesion.Id_Emp;
                    if (cliente == null || cliente == "")
                    {
                        reporteGAP.Id_Cte = -1;
                    }
                    else
                    {
                        reporteGAP.Id_Cte = Convert.ToInt32(cliente);
                    }


                    if (id_rik == null || id_rik == "")
                    {
                        reporteGAP.Id_Rik = -1;

                    }
                    else
                    {
                        reporteGAP.Id_Rik = Convert.ToInt32(id_rik);
                    }

                    if (reporteGAP.Id_Rik == 0)
                    {

                        reporteGAP.Id_Rik = -1;
                    }
                    reporteGAP.Id_Rik = sesion.Id_TU == 2 ? Convert.ToInt32(sesion.Id_Rik.ToString()) : reporteGAP.Id_Rik;


                    if (producto == "")
                    {
                        reporteGAP.Id_Prd = -1;
                    }
                    else
                    {
                        reporteGAP.Id_Prd = Convert.ToInt64(producto);
                    }

                    reporteGAP.Id_Tamaño = "A;B;C;D;E";

                    reporteGAP.Mes = 5;
                    reporteGAP.Año = 2024;
                    reporteGAP.Id_TipoReporte = tipoReporte;

                    List<GestionIncrementoPrecios> clientes = new List<GestionIncrementoPrecios>();

                    if (tipoReporte == 0 && reporteGAP.Id_Cte < 1)
                    {
                        if (Response.IsClientConnected)
                        {
                            Response.Clear();
                            Response.ContentType = "application/json";
                            Response.Write("[]");
                            Response.Flush();
                            HttpContext.Current.ApplicationInstance.CompleteRequest();
                        }
                        return;


                    }
                    else
                    {
                        clientes = CN_ReporteGAP.ConsultaListaReporteGAP_Producto2(reporteGAP, ref listreporteGAP, Conexion);
                    }

                    if (clientes.Count() > 1500 && tipoReporte == 0)
                    {
                        // Si el tamaño es mayor que el límite, retornamos un JSON vacío
                        Response.Clear();
                        Response.ContentType = "application/json";
                        Response.Write("[]"); // JSON vacío
                        Response.Flush();
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                        return;
                    }

                    // Serializar a JSON y escribir la respuesta
                    if (Response.IsClientConnected)
                    {
                        string json = JsonConvert.SerializeObject(clientes);


                        Response.ContentType = "application/json";
                        Response.Write(json);
                        Response.Flush();
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }
                }
                else
                {
                    // El cliente ya no está conectado, terminar la solicitud
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }

            }
            catch (Exception ex)
            {
                if (Response.IsClientConnected)
                {
                    Response.Clear();
                    Response.StatusCode = 500;
                    Response.ContentType = "application/json";
                    var errorDetails = new
                    {
                        error = ex.Message,
                        stackTrace = ex.StackTrace
                    };
                    Response.Write(JsonConvert.SerializeObject(errorDetails));
                    Response.Flush();
                }
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }

    }
}

