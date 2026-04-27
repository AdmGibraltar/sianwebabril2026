using CapaEntidad;
using CapaNegocios;
using DocumentFormat.OpenXml.Office2013.Excel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;

namespace SIANWEB.API
{
    public class APISKEY
    {
        public class Api
        {
            public string username { get; set; }
            public string password { get; set; }
        }


        public class canceledOrder
        {
            public int order_external_id { get; set; }
            public List<itemsentrega> items { get; set; }
        }

        public class Orders
        {
            public int order_external_id { get; set; }
            public DateTime promised_date { get; set; }
            public List<items> items { get; set; }
        }


        public class items
        {
            public string sku { get; set; }
            public string original_sku { get; set; }
            public int qty { get; set; }
            public decimal price { get; set; }
            public string comments { get; set; }
        }


        public class itemsEnvio
        {
            public string sku { get; set; }
            public int qty { get; set; }
            public int store_id { get; set; }
        }

        public class itemsentrega
        {
            public string sku { get; set; }
            public int qty { get; set; }
        }


        public class Invoice
        {
            public int order_external_id { get; set; }
            public int invoice_number { get; set; }
            public List<itemsentrega> items { get; set; }
            public string pdf_file { get; set; }
            public string xml_file { get; set; }

        }

        public class shipment
        {
            public int order_external_id { get; set; }
            public List<itemsEnvio> items { get; set; }
        }

        public class delivery
        {
            public int order_external_id { get; set; }
            public List<itemsentrega> items { get; set; }
        }


        public class remission
        {
            public int order_external_id { get; set; }
            public int remission_number { get; set; }
            public List<remissionIems> items { get; set; }
            public string pdf_file { get; set; }
        }


        public class remissionIems
        {
            public string sku { get; set; }
            public int qty { get; set; }

        }


        public class result
        {
            [JsonProperty(PropertyName = "status")]
            public string status { get; set; }

            [JsonProperty(PropertyName = "message")]
            public string message { get; set; }


        }


        public class Warning
        {
            public string id { get; set; }
        }



        public void Autentificar(int id_cd, ref string token)
        {
            try
            {
                string conexion = ConfigurationManager.AppSettings["strConnectionCentral"].ToString().Trim();
                CN_CapPedidoVtaInst cn = new CN_CapPedidoVtaInst();
                string validarToken = "";
                int tipo = 0;
                cn.Actualizartokenportalcliente(ref validarToken, tipo, conexion);

                if (validarToken != "")
                {
                    token = validarToken;
                }
                else
                {


                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    string URL = "https://www.portalkey.com.mx/rest/V1/integration/admin/token";
                    //string URL = "https://staging-5em2ouy-gdksx3o4l3okm.us-5.magentosite.cloud/rest/V1/integration/admin/token";

                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(URL);

                    var Api = new Api();
                    Api.username = "erp_user";
                    Api.password = "gX7uEBvVyjuH";

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(Api);
                    var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
                    var response = client.PostAsync(URL, data).Result;
                    token = Regex.Replace(response.Content.ReadAsStringAsync().Result, @"[^\w\s.!@$%^&*()\-\/]+", "");

                    tipo = 1;
                    cn.Actualizartokenportalcliente(ref token, tipo, conexion);
                }
            }
            catch (Exception ex)
            {
                logPortalCliente(id_cd, -1, "Token portal del cliente", -1, ex.InnerException.ToString());
            }
        }

        public void logPortalCliente(int id_Cd, int PedidoExterno, string TipoAPi, int Estatus, string mensaje)
        {
            int verificador = 0;
            string conexion = ConfigurationManager.AppSettings["strConnectionCentral"].ToString().Trim();
            CN_CapPedidoVtaInst cn = new CN_CapPedidoVtaInst();
            cn.logPortalCliente(id_Cd, PedidoExterno, TipoAPi, Estatus, mensaje, ref verificador, conexion);
        }


        public void ConsutarlogPortalClienteRemision(int id_Cd, int PedidoExterno, string TipoAPi, int estatus, ref int verificador)
        {
            string conexion = ConfigurationManager.AppSettings["strConnectionCentral"].ToString().Trim();
            CN_CapPedidoVtaInst cn = new CN_CapPedidoVtaInst();
            cn.ConsutarlogPortalClienteRemision(id_Cd, PedidoExterno, TipoAPi, estatus, ref verificador, conexion);
        }

        public void ModificarPedido(PedidoVtaInst Ped, DataTable dt, DataTable dtDelete, string conexion, ref string estatus, ref string message)
        {
            try
            {
                string token = "";
                Autentificar(Ped.Id_Cd, ref token);

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                string URL = "https://www.portalkey.com.mx/rest/V1/orders/external/update";
                //string URL = "https://staging-5em2ouy-gdksx3o4l3okm.us-5.magentosite.cloud/rest/V1/orders/external/update";

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(URL);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var authenticationString = $"key_quimica_dev:K3yQu1m1caSt4gingD3v23";
                var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));


                var Api = new Orders();

                Api.order_external_id = Convert.ToInt32(Ped.PedExterno);
                Api.promised_date = Ped.Ped_FechaEntrega;

                List<items> lista = new List<items>();
                items items;
                for (int x = 0; x < dt.Rows.Count; x++)
                {

                    items = new items();
                    items.sku = dt.Rows[x]["Id_Prd"].ToString();
                    items.original_sku = dt.Rows[x]["Id_PrdOld"].ToString();
                    items.qty = Convert.ToInt32(dt.Rows[x]["Prd_Cantidad"].ToString());
                    items.price = Convert.ToDecimal(dt.Rows[x]["Prd_Precio"].ToString());
                    items.comments = null;


                    PedidoVtaInst Registro = new PedidoVtaInst();
                    List<PedidoVtaInst> Listaprodsust = new List<PedidoVtaInst>();
                    CN_CapPedidoVtaInst PedSus = new CN_CapPedidoVtaInst();
                    Registro.Id_Cd = Ped.Id_Cd;
                    Registro.Id_Cte = Ped.Id_Cte;
                    Registro.Id_Prd = Convert.ToInt64(items.sku);
                    Registro.Id_Ped = Ped.Id_Ped;
                    PedSus.ConsultaProductoSustituto(Registro, ref Listaprodsust, conexion);
                    if (Listaprodsust.Count > 0)
                    {
                        items.sku = Listaprodsust.First().prodOriginal.ToString();
                        items.qty = Listaprodsust.First().cantidad;
                        items.price = Convert.ToDecimal(Listaprodsust.First().Precio.ToString());
                    }

                    lista.Add(items);
                }

                if (dtDelete != null)
                {
                    for (int x = 0; x < dtDelete.Rows.Count; x++)
                    {

                        items = new items();
                        items.sku = dtDelete.Rows[x]["Id_Prd"].ToString();
                        items.original_sku = dtDelete.Rows[x]["Id_PrdOld"].ToString();
                        items.qty = Convert.ToInt32(dtDelete.Rows[x]["Prd_Cantidad"].ToString());
                        items.price = Convert.ToDecimal(dtDelete.Rows[x]["Prd_Precio"].ToString());
                        items.comments = null;

                        PedidoVtaInst Registro = new PedidoVtaInst();
                        List<PedidoVtaInst> Listaprodsust = new List<PedidoVtaInst>();
                        CN_CapPedidoVtaInst PedSus = new CN_CapPedidoVtaInst();
                        Registro.Id_Cd = Ped.Id_Cd;
                        Registro.Id_Cte = Ped.Id_Cte;
                        Registro.Id_Prd = Convert.ToInt64(items.sku);
                        Registro.Id_Ped = Ped.Id_Ped;
                        PedSus.ConsultaProductoSustituto(Registro, ref Listaprodsust, conexion);
                        if (Listaprodsust.Count > 0)
                        {
                            items.sku = Listaprodsust.First().prodOriginal.ToString();
                            items.qty = Listaprodsust.First().cantidad;
                            items.price = Convert.ToDecimal(Listaprodsust.First().Precio.ToString());
                        }

                        lista.Add(items);
                    }
                }
                Api.items = lista;

                var json = JsonConvert.SerializeObject(new { order = Api });
                var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
                var response = client.PostAsync(URL, data).Result;

                JavaScriptSerializer js = new JavaScriptSerializer();
                string jsonString = response.Content.ReadAsStringAsync().Result;
                result blogObject = js.Deserialize<result>(jsonString);
                estatus = blogObject.status;
                message = blogObject.message;

                if (estatus == "1")
                {
                    int verificador = 0;
                    CN_Eccommerce CN = new CN_Eccommerce();
                    CN.EnviarCorreoCaptacion(Ped.PedExterno, Ped.Id_Cd, ref verificador, conexion);
                }

                logPortalCliente(Ped.Id_Cd, Convert.ToInt32(Ped.PedExterno), "ModificarPedido", Convert.ToInt32(estatus), message);
            }
            catch (Exception ex)
            {
                logPortalCliente(Ped.Id_Cd, Convert.ToInt32(Ped.PedExterno), "ModificarPedido", -1, ex.Message);
            }
        }

        public void CancelarPedido(PedidoVtaInst Ped, int tipoCancelacion, string conexion, ref string estatus, ref string message)
        {
            try
            {
                List<PedidoDet> det = new List<PedidoDet>();
                CN_CapPedido cn_capPedido = new CN_CapPedido();
                Ped.TipoCancelacion = tipoCancelacion;
                cn_capPedido.spCappedidoDetalle_Consultar(Ped, ref det, conexion);

                if (det.Count() > 0)
                {
                    string token = "";
                    Autentificar(Ped.Id_Cd, ref token);

                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    string URL = "https://www.portalkey.com.mx/rest/V1/orders/external/canceled";
                    //string URL = "https://staging-5em2ouy-gdksx3o4l3okm.us-5.magentosite.cloud/rest/V1/orders/external/canceled";

                    HttpClient client = new HttpClient();


                    var authenticationString = $"key_quimica_dev:K3yQu1m1caSt4gingD3v23";
                    var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));

                    client.BaseAddress = new Uri(URL);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var Api = new canceledOrder();


                    Api.order_external_id = Convert.ToInt32(Ped.PedExterno);
                    List<itemsentrega> lista = new List<itemsentrega>();

                    foreach (PedidoDet detalle in det)
                    {
                        itemsentrega items = new itemsentrega();
                        items.sku = detalle.Id_Prd.ToString(); ;
                        items.qty = detalle.Ped_Cantidad;
                        lista.Add(items);
                    }
                    Api.items = lista;

                    var json = JsonConvert.SerializeObject(new { canceledOrder = Api });


                    var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
                    var response = client.PostAsync(URL, data).Result;

                    JavaScriptSerializer js = new JavaScriptSerializer();
                    string jsonString = response.Content.ReadAsStringAsync().Result;
                    result blogObject = js.Deserialize<result>(jsonString);


                    estatus = blogObject.status;
                    message = blogObject.message;
                }
                else
                {
                    estatus = "2";
                    message = "error en consultar la información";
                }

                if (estatus == "1")
                {
                    int verificador = 0;
                    CN_Eccommerce CN = new CN_Eccommerce();
                    CN.EnviarCorreoCancelacion(Ped.PedExterno, Ped.Id_Cd, ref verificador, conexion);
                }
                logPortalCliente(Ped.Id_Cd, Convert.ToInt32(Ped.PedExterno), "Cancelación ", Convert.ToInt32(estatus), message);
            }
            catch (Exception ex)
            {
                logPortalCliente(Ped.Id_Cd, Convert.ToInt32(Ped.PedExterno), "Cancelación", -1, ex.Message);
            }
        }

        public void FacturacionPedido(Factura factura, string conexion, ref string estatus, ref string message)
        {
            try
            {
                List<PedidoDet> det = new List<PedidoDet>();
                string WebURLtempPDF = factura.pdf;
                string WebURLtempXml = factura.xml;


                //WebURLtempPDF = "http://13.85.9.131/sianwebmty/xmlSAT/FACTURA_1_110_136240.pdf";
                //WebURLtempXml = "http://13.85.9.131/sianwebmty/Reportes/archivoXml1063Fac136240.xml";

                CN_CapPedido cn_capPedido = new CN_CapPedido();

                cn_capPedido.spCapFacturaDetalle_Consultar(factura, ref det, conexion);

                if (det.Count() > 0)
                {
                    string token = "";
                    Autentificar(factura.Id_Cd, ref token);

                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    string URL = "https://www.portalkey.com.mx/rest/V1/orders/external/invoiced";
                    //string URL = "https://staging-5em2ouy-gdksx3o4l3okm.us-5.magentosite.cloud/rest/V1/orders/external/invoiced";

                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(URL);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var authenticationString = $"key_quimica_dev:K3yQu1m1caSt4gingD3v23";
                    var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));


                    var Api = new Invoice();


                    Api.order_external_id = factura.id_pedMag;
                    Api.invoice_number = factura.Id_Fac;
                    List<itemsentrega> lista = new List<itemsentrega>();

                    foreach (PedidoDet detalle in det)
                    {
                        itemsentrega items = new itemsentrega();
                        items.sku = detalle.Id_Prd.ToString(); ;
                        items.qty = detalle.Ped_Cantidad;


                        PedidoVtaInst Registro = new PedidoVtaInst();
                        List<PedidoVtaInst> Listaprodsust = new List<PedidoVtaInst>();
                        CN_CapPedidoVtaInst PedSus = new CN_CapPedidoVtaInst();
                        Registro.Id_Cd = factura.Id_Cd;
                        Registro.Id_Cte = factura.Id_Cte;
                        Registro.Id_Prd = Convert.ToInt64(items.sku);
                        Registro.Id_Ped = Convert.ToInt32(factura.Fac_PedNum);
                        PedSus.ConsultaProductoSustituto(Registro, ref Listaprodsust, conexion);
                        if (Listaprodsust.Count > 0)
                        {
                            items.sku = Listaprodsust.First().prodOriginal.ToString();
                            items.qty = Listaprodsust.First().cantidad;
                        }

                        lista.Add(items);
                    }
                    Api.items = lista;

                    Api.pdf_file = WebURLtempPDF.Replace('\\', '/');
                    Api.xml_file = WebURLtempXml.Replace('\\', '/');


                    var json = JsonConvert.SerializeObject(new { invoice = Api });


                    var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
                    var response = client.PostAsync(URL, data).Result;

                    JavaScriptSerializer js = new JavaScriptSerializer();
                    string jsonString = response.Content.ReadAsStringAsync().Result;
                    result blogObject = js.Deserialize<result>(jsonString);


                    estatus = blogObject.status;
                    message = blogObject.message;
                }
                else
                {
                    estatus = "2";
                    message = "error en consultar la información";
                }
                logPortalCliente(factura.Id_Cd, Convert.ToInt32(factura.id_pedMag), "Facturacion ", Convert.ToInt32(estatus), message);
            }
            catch (Exception ex)
            {
                logPortalCliente(factura.Id_Cd, Convert.ToInt32(factura.id_pedMag), "Facturacion", -1, ex.Message);
            }
        }

        public void RemisionPedido(Remision Remisiones, string url, string conexion, ref string estatus, ref string message)
        {
            try
            {
                List<RemisionDet> det = new List<RemisionDet>();
                Remision registros = new Remision();
                registros.Id_Emp = Remisiones.Id_Emp;
                registros.Id_Cd = Remisiones.Id_Cd;
                registros.Id_Rem = Remisiones.Id_Rem;
                registros.Id_Ped = Convert.ToInt32(Remisiones.Id_Ped);

                Sesion sesion = new Sesion();
                sesion.Id_Emp = Remisiones.Id_Emp;
                sesion.Id_Cd = Remisiones.Id_Cd;
                sesion.Emp_Cnx = conexion;

                CN_CapRemision Cn = new CN_CapRemision();
                int Pedido = Convert.ToInt32(Remisiones.Id_Ped);

                Cn.ConsultarRemisionesDetalle(sesion, registros, ref det);

                int verificador = 0;
                ConsutarlogPortalClienteRemision(Remisiones.Id_Cd, Convert.ToInt32(Remisiones.id_pedMag), "Remision", 1, ref verificador);
                if (verificador == 0)
                {
                    if (det.Count() > 0)
                    {
                        string token = "";
                        Autentificar(Remisiones.Id_Cd, ref token);

                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        string URL = "https://www.portalkey.com.mx/rest/V1/orders/external/remission";
                        //string URL = "https://staging-5em2ouy-gdksx3o4l3okm.us-5.magentosite.cloud/rest/V1/orders/external/remission";

                        HttpClient client = new HttpClient();
                        client.BaseAddress = new Uri(URL);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                        var authenticationString = $"key_quimica_dev:K3yQu1m1caSt4gingD3v23";
                        var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));


                        var Api = new remission();


                        Api.order_external_id = Remisiones.id_pedMag;
                        Api.remission_number = Remisiones.Id_Rem;

                        List<remissionIems> lista = new List<remissionIems>();

                        foreach (RemisionDet detalle in det)
                        {
                            remissionIems items = new remissionIems();
                            items.sku = detalle.Id_Prd.ToString();
                            items.qty = detalle.Rem_Cant;

                            PedidoVtaInst Registro = new PedidoVtaInst();
                            List<PedidoVtaInst> Listaprodsust = new List<PedidoVtaInst>();
                            CN_CapPedidoVtaInst PedSus = new CN_CapPedidoVtaInst();
                            Registro.Id_Cd = Remisiones.Id_Cd;
                            Registro.Id_Cte = Remisiones.Id_Cte;
                            Registro.Id_Prd = Convert.ToInt64(items.sku);
                            Registro.Id_Ped = Convert.ToInt32(Remisiones.Id_Ped);
                            PedSus.ConsultaProductoSustituto(Registro, ref Listaprodsust, conexion);
                            if (Listaprodsust.Count > 0)
                            {
                                items.sku = Listaprodsust.First().prodOriginal.ToString();
                                items.qty = Listaprodsust.First().cantidad;
                            }

                            lista.Add(items);
                        }
                        Api.items = lista;
                        Api.pdf_file = url.Replace('\\', '/'); ;
                        //Api.pdf_file = "http://13.85.9.131/sianwebmty/xmlSAT/FACTURA_1_110_136240.pdf";


                        var json = JsonConvert.SerializeObject(new { remission = Api });


                        var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
                        var response = client.PostAsync(URL, data).Result;

                        JavaScriptSerializer js = new JavaScriptSerializer();
                        string jsonString = response.Content.ReadAsStringAsync().Result;
                        result blogObject = js.Deserialize<result>(jsonString);


                        estatus = blogObject.status;
                        message = blogObject.message;
                    }
                    else
                    {
                        estatus = "2";
                        message = "error en consultar la información";
                    }


                    logPortalCliente(Remisiones.Id_Cd, Convert.ToInt32(Remisiones.id_pedMag), "Remision", Convert.ToInt32(estatus), message);
                }
            }
            catch (Exception ex)
            {
                logPortalCliente(Remisiones.Id_Cd, Convert.ToInt32(Remisiones.id_pedMag), "Remision", -1, ex.Message);
            }

        }

        public void EnvioPedido(Factura factura, string conexion, ref string estatus, ref string message)
        {
            try
            {
                List<PedidoDet> det = new List<PedidoDet>();
                Pedido pedido = new Pedido();
                pedido.Id_Emp = factura.Id_Emp;
                pedido.Id_Cd = factura.Id_Cd;
                pedido.Id_Ped = Convert.ToInt32(factura.Fac_PedNum);


                CN_CapPedido cn_capPedido = new CN_CapPedido();


                cn_capPedido.spCapFacturaDetalle_Consultar(factura, ref det, conexion);

                if (det.Count() > 0)
                {
                    string token = "";
                    Autentificar(factura.Id_Cd, ref token);

                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    string URL = "https://www.portalkey.com.mx/rest/V1/orders/external/shipped";
                    //string URL = "https://staging-5em2ouy-gdksx3o4l3okm.us-5.magentosite.cloud/rest/V1/orders/external/shipped";

                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(URL);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


                    var Api = new shipment();

                    Api.order_external_id = factura.id_pedMag;

                    List<itemsEnvio> lista = new List<itemsEnvio>();

                    foreach (PedidoDet detalle in det)
                    {
                        itemsEnvio items = new itemsEnvio();
                        items.sku = detalle.Id_Prd.ToString(); ;
                        items.qty = detalle.Ped_Cantidad;
                        items.store_id = pedido.Id_Cd;

                        PedidoVtaInst Registro = new PedidoVtaInst();
                        List<PedidoVtaInst> Listaprodsust = new List<PedidoVtaInst>();
                        CN_CapPedidoVtaInst PedSus = new CN_CapPedidoVtaInst();
                        Registro.Id_Cd = factura.Id_Cd;
                        Registro.Id_Cte = factura.Id_Cte;
                        Registro.Id_Prd = Convert.ToInt64(items.sku);
                        Registro.Id_Ped = Convert.ToInt32(factura.Fac_PedNum);
                        PedSus.ConsultaProductoSustituto(Registro, ref Listaprodsust, conexion);
                        if (Listaprodsust.Count > 0)
                        {
                            items.sku = Listaprodsust.First().prodOriginal.ToString();
                            items.qty = Listaprodsust.First().cantidad;
                        }


                        lista.Add(items);
                    }
                    Api.items = lista;

                    var json = JsonConvert.SerializeObject(new { shipment = Api });


                    var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
                    var response = client.PostAsync(URL, data).Result;

                    JavaScriptSerializer js = new JavaScriptSerializer();

                    string jsonString = response.Content.ReadAsStringAsync().Result;
                    result blogObject = js.Deserialize<result>(jsonString);


                    estatus = blogObject.status;
                    message = blogObject.message;
                }
                else
                {
                    estatus = "2";
                    message = "error en consultar la información";
                }

                if (estatus == "1")
                {
                    int verificador = 0;
                    CN_Eccommerce CN = new CN_Eccommerce();
                    CN.EnviarCorreoEnvio(factura.id_pedMag, factura.Id_Cd, factura.Id_Fac, 0, ref verificador, conexion);
                }
                logPortalCliente(factura.Id_Cd, Convert.ToInt32(factura.id_pedMag), "Envio pedido ", Convert.ToInt32(estatus), message);
            }
            catch (Exception ex)
            {
                logPortalCliente(factura.Id_Cd, Convert.ToInt32(factura.id_pedMag), "Envio pedido", -1, ex.Message);
            }
        }

        public void EnvioPedidRemision(Remision factura, string conexion, ref string estatus, ref string message)
        {
            try
            {
                List<RemisionDet> det = new List<RemisionDet>();
                Pedido pedido = new Pedido();
                pedido.Id_Emp = factura.Id_Emp;
                pedido.Id_Cd = factura.Id_Cd;
                pedido.Id_Ped = Convert.ToInt32(factura.Id_Rem);


                CN_CapRemision cn_capPedido = new CN_CapRemision();


                cn_capPedido.ConsultarRemisionesDetalle(factura, ref det, conexion);


                if (det.Count() > 0)
                {
                    string token = "";
                    Autentificar(factura.Id_Cd, ref token);

                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    string URL = "https://www.portalkey.com.mx/rest/V1/orders/external/shipped";
                    //string URL = "https://staging-5em2ouy-gdksx3o4l3okm.us-5.magentosite.cloud/rest/V1/orders/external/shipped";

                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(URL);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);



                    var Api = new shipment();

                    Api.order_external_id = factura.id_pedMag;

                    List<itemsEnvio> lista = new List<itemsEnvio>();

                    foreach (RemisionDet detalle in det)
                    {
                        itemsEnvio items = new itemsEnvio();
                        items.sku = detalle.Id_Prd.ToString(); ;
                        items.qty = detalle.Rem_Cant;
                        items.store_id = pedido.Id_Cd;

                        PedidoVtaInst Registro = new PedidoVtaInst();
                        List<PedidoVtaInst> Listaprodsust = new List<PedidoVtaInst>();
                        CN_CapPedidoVtaInst PedSus = new CN_CapPedidoVtaInst();
                        Registro.Id_Cd = factura.Id_Cd;
                        Registro.Id_Cte = factura.Id_Cte;
                        Registro.Id_Prd = Convert.ToInt64(items.sku);
                        Registro.Id_Ped = Convert.ToInt32(factura.Fac_PedNum);
                        PedSus.ConsultaProductoSustituto(Registro, ref Listaprodsust, conexion);
                        if (Listaprodsust.Count > 0)
                        {
                            items.sku = Listaprodsust.First().prodOriginal.ToString();
                            items.qty = Listaprodsust.First().cantidad;
                        }

                        lista.Add(items);
                    }
                    Api.items = lista;

                    var json = JsonConvert.SerializeObject(new { shipment = Api });


                    var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
                    var response = client.PostAsync(URL, data).Result;

                    JavaScriptSerializer js = new JavaScriptSerializer();

                    string jsonString = response.Content.ReadAsStringAsync().Result;
                    result blogObject = js.Deserialize<result>(jsonString);


                    estatus = blogObject.status;
                    message = blogObject.message;
                }
                else
                {
                    estatus = "2";
                    message = "error en consultar la información";
                }

                if (estatus == "1")
                {
                    int verificador = 0;
                    CN_Eccommerce CN = new CN_Eccommerce();
                    CN.EnviarCorreoEnvio(factura.id_pedMag, factura.Id_Cd, 0, factura.Id_Rem, ref verificador, conexion);
                }
                logPortalCliente(factura.Id_Cd, Convert.ToInt32(factura.id_pedMag), "envio remision ", Convert.ToInt32(estatus), message);
            }
            catch (Exception ex)
            {
                logPortalCliente(factura.Id_Cd, Convert.ToInt32(factura.id_pedMag), "envio remision", -1, ex.Message);
            }
        }

        public void entregaPedido(Factura factura, string conexion, ref string estatus, ref string message)
        {
            try
            {
                List<PedidoDet> det = new List<PedidoDet>();
                CN_CapPedido cn_capPedido = new CN_CapPedido();


                cn_capPedido.spCapFacturaDetalle_Consultar(factura, ref det, conexion);

                if (det.Count() > 0)
                {
                    string token = "";
                    Autentificar(factura.Id_Cd, ref token);

                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    string URL = "https://www.portalkey.com.mx/rest/V1/orders/external/delivered";
                    //string URL = "https://staging-5em2ouy-gdksx3o4l3okm.us-5.magentosite.cloud/rest/V1/orders/external/delivered";

                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(URL);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


                    var Api = new delivery();

                    Api.order_external_id = factura.id_pedMag;

                    List<itemsentrega> lista = new List<itemsentrega>();

                    foreach (PedidoDet detalle in det)
                    {
                        itemsentrega items = new itemsentrega();
                        items.sku = detalle.Id_Prd.ToString(); ;
                        items.qty = detalle.Ped_Cantidad;

                        PedidoVtaInst Registro = new PedidoVtaInst();
                        List<PedidoVtaInst> Listaprodsust = new List<PedidoVtaInst>();
                        CN_CapPedidoVtaInst PedSus = new CN_CapPedidoVtaInst();
                        Registro.Id_Cd = factura.Id_Cd;
                        Registro.Id_Cte = factura.Id_Cte;
                        Registro.Id_Prd = Convert.ToInt64(items.sku);
                        Registro.Id_Ped = Convert.ToInt32(factura.Fac_PedNum);
                        PedSus.ConsultaProductoSustituto(Registro, ref Listaprodsust, conexion);
                        if (Listaprodsust.Count > 0)
                        {
                            items.sku = Listaprodsust.First().prodOriginal.ToString();
                            items.qty = Listaprodsust.First().cantidad;
                        }

                        lista.Add(items);
                    }
                    Api.items = lista;

                    var json = JsonConvert.SerializeObject(new { delivery = Api });
                    var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
                    var response = client.PostAsync(URL, data).Result;

                    JavaScriptSerializer js = new JavaScriptSerializer();

                    string jsonString = response.Content.ReadAsStringAsync().Result;
                    result blogObject = js.Deserialize<result>(jsonString);


                    estatus = blogObject.status;
                    message = blogObject.message;

                    if (message.Contains("no pudo ser enviado") || message.Contains("no pudo ser entregado"))
                    {
                        EnvioPedido(factura, conexion, ref estatus, ref message);
                        if (estatus == "2" || estatus == "0")
                        {
                            message = "Error: al Estatus de Entrega de Pedido Portal Key: " + message;
                            return;
                        }
                        else
                        {
                            entregaPedido(factura, conexion, ref estatus, ref message);
                        }
                    }
                }
                else
                {
                    estatus = "2";
                    message = "error en consultar la información";
                }

                if (estatus == "1")
                {
                    int verificador = 0;
                    CN_Eccommerce CN = new CN_Eccommerce();
                    CN.EnviarCorreoEntrega(factura.id_pedMag, factura.Id_Cd, factura.Id_Fac, 0, ref verificador, conexion);
                }
                logPortalCliente(factura.Id_Cd, Convert.ToInt32(factura.id_pedMag), "Entrega de pedido ", Convert.ToInt32(estatus), message);
            }
            catch (Exception ex)
            {
                logPortalCliente(factura.Id_Cd, Convert.ToInt32(factura.id_pedMag), "entrega de pedido", -1, ex.Message);
            }
        }

        public void entregaRemision(Remision factura, string conexion, ref string estatus, ref string message)
        {
            try
            {
                List<RemisionDet> det = new List<RemisionDet>();

                CN_CapRemision cn_capPedido = new CN_CapRemision();


                cn_capPedido.ConsultarRemisionesDetalle(factura, ref det, conexion);

                if (det.Count() > 0)
                {
                    string token = "";
                    Autentificar(factura.Id_Cd, ref token);

                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    string URL = "https://www.portalkey.com.mx/rest/V1/orders/external/delivered";
                    //string URL = "https://staging-5em2ouy-gdksx3o4l3okm.us-5.magentosite.cloud/rest/V1/orders/external/delivered";

                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(URL);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var Api = new delivery();
                    Api.order_external_id = factura.id_pedMag;

                    List<itemsentrega> lista = new List<itemsentrega>();

                    foreach (RemisionDet detalle in det)
                    {
                        itemsentrega items = new itemsentrega();
                        items.sku = detalle.Id_Prd.ToString(); ;
                        items.qty = detalle.Rem_Cant;

                        PedidoVtaInst Registro = new PedidoVtaInst();
                        List<PedidoVtaInst> Listaprodsust = new List<PedidoVtaInst>();
                        CN_CapPedidoVtaInst PedSus = new CN_CapPedidoVtaInst();
                        Registro.Id_Cd = factura.Id_Cd;
                        Registro.Id_Cte = factura.Id_Cte;
                        Registro.Id_Prd = Convert.ToInt64(items.sku);
                        Registro.Id_Ped = Convert.ToInt32(factura.Fac_PedNum);
                        PedSus.ConsultaProductoSustituto(Registro, ref Listaprodsust, conexion);
                        if (Listaprodsust.Count > 0)
                        {
                            items.sku = Listaprodsust.First().prodOriginal.ToString();
                            items.qty = Listaprodsust.First().cantidad;
                        }

                        lista.Add(items);
                    }
                    Api.items = lista;

                    var json = JsonConvert.SerializeObject(new { delivery = Api });
                    var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
                    var response = client.PostAsync(URL, data).Result;

                    JavaScriptSerializer js = new JavaScriptSerializer();

                    string jsonString = response.Content.ReadAsStringAsync().Result;
                    result blogObject = js.Deserialize<result>(jsonString);


                    estatus = blogObject.status;
                    message = blogObject.message;

                    if (message.Contains("no pudo ser enviado"))
                    {
                        EnvioPedidRemision(factura, conexion, ref estatus, ref message);
                        if (estatus == "2" || estatus == "0")
                        {
                            message = "Error: al Estatus de Entrega de Pedido Portal Key: " + message;
                            return;
                        }
                        else
                        {
                            entregaRemision(factura, conexion, ref estatus, ref message);
                        }
                    }


                    if (estatus == "1")
                    {
                        int verificador = 0;
                        CN_Eccommerce CN = new CN_Eccommerce();
                        CN.EnviarCorreoEntrega(factura.id_pedMag, factura.Id_Cd, 0, factura.Id_Rem, ref verificador, conexion);
                    }

                }
                else
                {
                    estatus = "2";
                    message = "error en consultar la información";
                }
                logPortalCliente(factura.Id_Cd, Convert.ToInt32(factura.id_pedMag), "entrega remision ", Convert.ToInt32(estatus), message);
            }
            catch (Exception ex)
            {
                logPortalCliente(factura.Id_Cd, Convert.ToInt32(factura.id_pedMag), "entrega remision", -1, ex.Message);
            }
        }

    }
}