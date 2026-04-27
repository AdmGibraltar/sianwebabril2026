using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Data;
using System.IO;
using OfficeOpenXml;
using System.Web.Services;
using System.Drawing;
using CapaNegocios;
using System.Linq;
using SIANWEB.Core.UI;
//pdf Spire 
using Spire.Pdf;
using Spire.Pdf.Graphics;
using Spire.Pdf.Tables;
using System.Globalization;
using Spire.Pdf.Annotations;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;

namespace SIANWEB.GestionPrecios
{




    public partial class GestionIncrementoClientesDetalle : BaseServerPage
    {
        public static Sesion MySesion { get; set; }
        public bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        public bool _PermisoModificar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoModificar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }
        private Sesion sesion { get { return (Sesion)Session["Sesion" + Session.SessionID]; } set { Session["Sesion" + Session.SessionID] = value; } }

        public int Usuario_Tipo;
        public int Id_TU; // Tipo Usuario 3.- Gerente         
        public int Id_Rik; // Representante Institucional Key RIK , para recibir parametro 
        public int Id_CD;
        public int Id_U;
        public string CDI_Nombre;
        public string Fecha;


        protected void Page_Load(object sender, EventArgs e)
        {

            MySesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (MySesion == null)
            {
                string script = "<script>closeThisWindow()</script>";
                ScriptManager.RegisterStartupScript(this, GetType(), "closeThisWindow()", script, false);
                return;
            }

            if (Session["dtGestionincrementoclientes"] != null)
            {
                Session["dtGestionincrementoclientes"] = null;
            }


            Sesion Sesion = new Sesion();
            Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            if (Sesion != null)
            {
                //hId_TU.Value = Convert.ToString(Sesion.Id_TU);
                inicializar();
            }
            else
            {
                Response.Redirect("Login.aspx");
            }


            Usuario_Tipo = sesion.Id_TU;
            Id_TU = sesion.Id_TU;
            Id_CD = sesion.Id_Cd;
            Id_Rik = sesion.Id_Rik;
            Id_U = sesion.Id_U;
            CDI_Nombre = sesion.Cd_Nombre;
            Fecha = sesion.CalendarioIni.ToString("MM/dd/yyyy");

            ////cmbTipoReporte.SelectedIndex = 0;
            //CmbSucursal.SelectedIndex = 0;


            // ObtenerClientes();
            if (IsPostBack) return;

            //string action = Request.QueryString["action"];
            //if (action == "getClientes")
            //{
            //    ObtenerClientes();
            //}

        }

        private void inicializar()
        {

            CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
            string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionSIANCentral"];

            string idCte = Request.QueryString["idcte"];
            string sucursal = Request.QueryString["sucursal"];
            string representante = Request.QueryString["representante"];
            string nomestatus = Request.QueryString["nomestatus"];

            estatus.Value = nomestatus;

        }




        //obtener un excel
        // Clase auxiliar para deserializar los datos JSON
        public class Cliente
        {
            public int Id_Rik { get; set; }
            public string Nombre_Rik { get; set; }
            public int Id_Cte { get; set; }
            public string Cte_NomComercial { get; set; }
            public int Id_Prd { get; set; }
            public string NomProducto { get; set; }
            public decimal PrecioNegociadoProy { get; set; }
            public decimal Ventas_Proyectadas { get; set; }


        }

        [WebMethod]
        public static String GetClientes(List<Cliente> clientes, int dias, int id_cte, string nom_comercial, string representante)
        {

            // Crear DataTable desde la lista
            DataTable dt = ConvertirADataTable(clientes);


            // string Outgoingfile = "Cotizacion" + clientes[0].Id_Cte + "-" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";
            string Outgoingfile = "Cotizacion" + id_cte + "-" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";
            //string rutaArchivo = HttpContext.Current.Server.MapPath("~/Reportes/Clientes.xlsx");
            string rutaRelativa = $"~/Reportes/{Outgoingfile}";
            string rutaArchivo = HttpContext.Current.Server.MapPath("~/Reportes/" + Outgoingfile);
            string rutaCompleta = HttpContext.Current.Server.MapPath(rutaRelativa);


            GenerarExcel(dt, rutaArchivo, dias, id_cte, nom_comercial, representante);

            // Construir URL de retorno
            Uri requestUrl = HttpContext.Current.Request.Url;
            string baseUrl = $"{requestUrl.Scheme}://{requestUrl.Host}:{requestUrl.Port}";
            string archivoUrl = $"{baseUrl}/Reportes/{Outgoingfile}";

            return archivoUrl;

            //DataTable dt = ObtenerClientesDeBD(); // Método que carga datos en un DataTable
            //var clientes = new List<Cliente>();

            //foreach (DataRow row in dt.Rows)
            //{
            //    clientes.Add(new Cliente
            //    {
            //        Id = Convert.ToInt32(row["Id"]),
            //        Nombre = row["Nombre"].ToString(),
            //        Saldo = Convert.ToDecimal(row["Saldo"])
            //    });
            //}
        }




        private static DataTable ConvertirADataTable(List<Cliente> clientes)
        {
            DataTable dt = new DataTable("Clientes");

            dt.Columns.Add("[Código]", typeof(int));
            dt.Columns.Add("Descripción", typeof(string));
            dt.Columns.Add("Precio", typeof(decimal));


            foreach (var cliente in clientes)
            {
                dt.Rows.Add(cliente.Id_Prd, cliente.NomProducto, cliente.PrecioNegociadoProy);
            }

            return dt;
        }

        private static void GenerarExcel(DataTable dt, string rutaArchivo, int dias, int id_cte, string nom_comercial, string representante)
        {
            // Crear archivo Excel con EPPlus
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Cotizacion");

                worksheet.Cells["A1"].Value = "No. Cliente: ";
                worksheet.Cells["A1"].Style.Font.Bold = true;
                worksheet.Cells["A2"].Value = "Nombre: ";
                worksheet.Cells["A2"].Style.Font.Bold = true;


                worksheet.Cells["B1"].Value = id_cte;
                worksheet.Cells["B1"].Style.Font.Bold = true;
                worksheet.Cells["B2"].Value = nom_comercial;
                worksheet.Cells["B2"].Style.Font.Bold = true;

                //worksheet.Cells["A1"].Style.Border = XLBorderStyleValues.Thin;



                worksheet.Cells["A3"].LoadFromDataTable(dt, true);
                worksheet.Cells["A3"].Style.Font.Bold = true;
                // Opcional: formato de encabezados
                using (var range = worksheet.Cells[3, 1, 3, dt.Columns.Count])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                }

                package.SaveAs(new FileInfo(rutaArchivo));
            }
        }



        [WebMethod]
        public static string GenerarHoja2(List<Cliente> clientes, int dias, int id_cte, string nom_comercial, string representante, string nombrerepresentante, string telefonorik, string fechainicio)
        {
            try
            {

                string nombreArchivo = "Cotizacion" + id_cte + "-" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";
                string rutaRelativa = $"~/Reportes/{nombreArchivo}";
                string rutaArchivo = HttpContext.Current.Server.MapPath("~/Reportes/" + nombreArchivo);
                string rutaCompleta = HttpContext.Current.Server.MapPath(rutaRelativa);

                string rutaLogo = HttpContext.Current.Server.MapPath("~/Img/KeyActprecios.png");
                string rutaLogo2 = HttpContext.Current.Server.MapPath("~/Img/key_logo_reciente.png");
                string piedepagina = HttpContext.Current.Server.MapPath("~/Img/piedepagina.png");
                string rutapiedepagina = HttpContext.Current.Server.MapPath("~/Img/piedepagina.png");

                string rutaImagen1 = HttpContext.Current.Server.MapPath("~/Img/banxico.png");
                string rutaimagen2 = HttpContext.Current.Server.MapPath("~/Img/inegi.png");

                MySesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
                //if (MySesion == null)
                //{
                //    response.Message = "connection close";
                //    return response;
                //}


                string empresa = MySesion.Emp_Nombre;


                //Cargar datos cliente 

                Clientes cte = new Clientes();
                cte.Id_Cte = id_cte;
                cte.Id_Emp = MySesion.Id_Emp;
                cte.Id_Cd = MySesion.Id_Cd_Ver;
                //cte.Id_Rik = txtRepresentante.Value.HasValue ? (int)txtRepresentante.Value.Value : (sesion.Id_Rik > 0 ? sesion.Id_Rik : 0);
                CN_CatCliente cnCliente = new CN_CatCliente();
                try
                {
                    cnCliente.ConsultaClientes(ref cte, MySesion.Emp_Cnx);
                }
                catch (Exception ex)
                {
                    //AlertaFocus(ex.Message, txtCliente.ClientID);
                    //txtClienteNombre.Text = "";
                    //txtCliente.Text = "";
                    //if (cmbTerritorio.Items.Count > 0)
                    //{
                    //    cmbTerritorio.SelectedIndex = 1;
                    //    cmbTerritorio.Text = cmbTerritorio.Items[1].Text;
                    //    txtTerritorio.Text = cmbTerritorio.Items[1].Text;

                    //} //txtTerritorio.Value = null;
                    //Limpiar();

                }

                string nombrecom = cte.Cte_NomComercial;
                string calle = cte.Cte_FacCalle;
                string colonia = cte.Cte_FacColonia;
                //txtClienteMunicipio.Text = cte.Cte_FacMunicipio;
                //txtClienteEstado.Text = cte.Cte_FacEstado;
                //txtClienteRFC.Text = cte.Cte_FacRfc;
                //txtClienteCodPost.Text = cte.Cte_FacCp;

                string contacto = cte.Cte_Contacto;
                //txtPuesto.Text = cte.ct;
                string telefono_cliente = cte.Cte_Telefono;
                int diascredito = cte.Cte_CondPago;
                ///DESCOMENTARIZAR TODO 
                ///---correo = correo +" ; " cte.Cte_Email;

                using (var package = new ExcelPackage())
                {
                    // Crear hoja
                    ExcelWorksheet hoja = package.Workbook.Worksheets.Add("Clientes");

                    // Agregar logo


                    if (File.Exists(rutaLogo2))
                    {
                        var imagen = hoja.Drawings.AddPicture("Logo2", new FileInfo(rutaLogo2));
                        imagen.SetPosition(0, 0, 0, 0); // Posicionar en la esquina superior izquierda
                        //imagen.SetSize(100, 50); // Ajustar tamaño
                    }


                    hoja.View.ShowGridLines = false;

                    hoja.Cells["I2"].Value = $"Fecha: {DateTime.Now:dd/MM/yyyy}";
                    hoja.Cells["I2"].Style.Font.Italic = true;
                    hoja.Cells["I2"].Style.Font.Bold = true;
                    hoja.Cells["I3"].Value = "Key Química SA de CV";
                    hoja.Cells["I4"].Value = "KQU6911016X5";
                    //hoja.Cells["I5"].Value = " *telefono key*";
                    //hoja.Cells["I6"].Value = " *dirección*";
                    using (var range = hoja.Cells["I2:I6"])
                    {
                        range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right; // Alineación justificada
                        range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top; // Alineación vertical superior
                        range.Style.Font.Bold = true;
                        range.Style.Font.Italic = true;
                        range.Style.Font.Size = 11; // Tamaño de fuente adecuado para lectura 
                    }

                    hoja.Cells["A7"].Value = "PROPUESTA ECONÓMICA";

                    hoja.Cells["C9"].Value = "Empresa: ";
                    hoja.Cells["C10"].Value = "Atención: ";
                    hoja.Cells["D9"].Value = nom_comercial;
                    hoja.Cells["D10"].Value = contacto;
                    using (var range = hoja.Cells["A7:C10"])
                    {
                        range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left; // Alineación justificada
                        range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top; // Alineación vertical superior
                        range.Style.Font.Bold = true;

                        range.Style.Font.Size = 11; // Tamaño de fuente adecuado para lectura

                    }

                    // Encabezado con el nombre de la empresa y fecha actual
                    //hoja.Cells["A1"].Value = id_cte + " - "+ nom_comercial;
                    //hoja.Cells["A1"].Style.Font.Size = 14;
                    //hoja.Cells["A1"].Style.Font.Bold = true;


                    //using (var range = hoja.Cells["J138:K142"])
                    //{
                    //    range.Style.Font.Bold = true;
                    //    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                    //}
                    //using (var range = hoja.Cells["C16:J74"])
                    //{
                    //    range.Style.Font.Bold = false;
                    //    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                    //}



                    hoja.Cells["B12"].Value = "Key es una empresa 100% mexicana con más de 50 años de experiencia en la industria, lo que nos ha dado el conocimiento para "
                    + "entender que en la actualidad los clientes son más demandantes con la limpieza y con espacios más limpios y sanos, por lo que "
                    + "nos hemos especializado en no solo ofrecer químicos y accesorios, sino en desarrollar un programa completo de higiene, "
                    + "personalizado, según las necesidades de cada cliente, que les ayude a cumplir con altos estándares de limpieza de manera simple y a "
                    + "un costo óptimo. "
                    + "Esperando podamos colaborar e integrar un equipo, nos reiteramos a sus órdenes. ";
                    using (var range = hoja.Cells["B12:H18"])
                    {
                        range.Merge = true; // Combina las celdas para dar formato como bloque de texto único
                        range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Justify; // Alineación justificada
                        range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top; // Alineación vertical superior
                        range.Style.WrapText = true; // Habilita el ajuste automático de texto
                        range.Style.Font.Bold = false; // Desactiva negrita para texto regular
                        range.Style.Font.Size = 11; // Tamaño de fuente adecuado para lectura
                        range.AutoFitColumns(); // Ajusta automáticamente el ancho de las columnas
                    }



                    // Encabezados de la tabla
                    hoja.Cells["C20"].Value = "Código";
                    hoja.Cells["D20"].Value = "Descripción";
                    hoja.Cells["E20"].Value = "Precio";

                    using (var range = hoja.Cells["C20:E20"])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                        range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    }

                    // Llenar datos de la tabla
                    int row = 21;
                    foreach (var cliente in clientes)
                    {
                        hoja.Cells[row, 3].Value = cliente.Id_Prd;
                        hoja.Cells[row, 4].Value = cliente.NomProducto;
                        hoja.Cells[row, 5].Value = cliente.PrecioNegociadoProy;
                        // Aplicar formato numérico con comas
                        hoja.Cells[row, 5].Style.Numberformat.Format = "#,##0.00";
                        row++;
                    }

                    // Ajustar el ancho de las columnas

                    hoja.Column(3).Width = 15; // Id_prd
                    hoja.Column(4).Width = 40; // Descripción
                    hoja.Column(5).Width = 20; // Precio 


                    row++;

                    hoja.Cells[row, 1].Value = "Beneficios Tangibles";
                    row++;
                    hoja.Cells[row, 1].Value = "Logro de estándares de limpieza: Instalaciones limpias que de manera sostenida cumplan con todos los estándares de higiene";


                    row++;

                    hoja.Cells[row, 1].Value = "Costo óptimo: Reducción de costo mensual integral de la limpieza";
                    row++;
                    hoja.Cells[row, 1].Value = "Simplificación: La función de la limpieza hecha simple y práctica.";
                    row++;
                    row++;
                    hoja.Cells[row, 1].Value = "Términos y Condiciones";
                    hoja.Cells[row, 1].Style.Font.Bold = true;
                    hoja.Cells[row, 1].Style.Font.UnderLine = true;
                    row++;
                    hoja.Cells[row, 1].Value = " *Vigencia de esta cotización es de " + dias + " días a partir de la fecha presentada.";
                    row++;

                    hoja.Cells[row, 1].Value = "* Precios No Incluyen IVA";
                    row++;
                    hoja.Cells[row, 1].Value = "* Entrega a domicilio";
                    row++;
                    hoja.Cells[row, 1].Value = "* Precios entran en vigor a partir del " + fechainicio;
                    row++;
                    hoja.Cells[row, 1].Value = "* Los precios convenidos se respetarán y cualquier cambio será comunicado al cliente con 15 días de anticipación.";
                    row++;
                    hoja.Cells[row, 1].Value = "* En caso de que nuestra solución contemple la instalación de equipos dosificadores, éstos se ofrecerán en calidad de";
                    row++;
                    hoja.Cells[row, 1].Value = "préstamo, para lo cual se firmará un Contrato de Comodato entre ambas partes.";
                    row++;
                    hoja.Cells[row, 1].Value = "* Se establece que las condiciones de crédito serán de " + diascredito + " días naturales a partir de la fecha de revisión de nuestra factura.";
                    row++;

                    hoja.Cells[row, 1].Value = "* En caso de que haya demora en los pagos, Key Soluciones de Limpieza suspenderá el suministro de productos y servicios.";

                    row++;

                    hoja.Cells[row, 1].Value = "* Para formalizar los términos, condiciones y alcance de nuestro servicio, se firmará un Convenio que garantice el cumplimiento";
                    row++;
                    hoja.Cells[row, 1].Value = "del mismo para ambas partes.";
                    row++;
                    row++;
                    row++;
                    //hoja.Cells[row, 3].Value = "Agradeciendo la oportunidad para poder presentarle nuestra propuesta, me despido de usted esperando poder atenderle próximamente";

                    string rango = $"C{row}:F{row + 1}";

                    hoja.Cells[rango].Merge = true; // Combinar celdas dentro del rango
                    hoja.Cells[rango].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    hoja.Cells[rango].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    hoja.Cells[rango].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                    hoja.Cells[rango].Style.WrapText = true; // Habilita el ajuste automático de texto

                    // Asignar valor al rango
                    hoja.Cells[rango].Value = "Agradeciendo la oportunidad para poder presentarle nuestra propuesta, me despido de usted esperando poder atenderle próximamente";

                    row = row + 3;
                    rango = $"C{row}:F{row}";
                    hoja.Cells[rango].Style.Font.Bold = true;
                    hoja.Cells[rango].Style.WrapText = true; // Habilita el ajuste automático de texto
                    hoja.Cells[rango].Merge = true; // Combinar celdas dentro del rango
                    hoja.Cells[rango].Value = nombrerepresentante + " , " + telefonorik;

                    hoja.PrinterSettings.PrintArea = hoja.Cells["A1:J200"]; // Ajusta el rango de impresión según tus datos

                    row = row + 3;
                    if (File.Exists(rutapiedepagina))
                    {
                        var imagen = hoja.Drawings.AddPicture("piedepagina", new FileInfo(rutapiedepagina));
                        imagen.SetPosition(row, 0, 0, 0); // Posicionar en la esquina superior izquierda
                        imagen.SetSize(1000, 100); // Ajustar tamaño
                    }



                    hoja.PrinterSettings.Orientation = eOrientation.Portrait; // Orientación horizontal
                    hoja.PrinterSettings.PaperSize = ePaperSize.Letter; // Tamaño de papel
                    hoja.PrinterSettings.HorizontalCentered = true; // Centrar horizontalmente
                    hoja.PrinterSettings.VerticalCentered = false; // No centrar verticalmente
                    hoja.PrinterSettings.FitToWidth = 1; // Ajustar al ancho de una página
                    hoja.PrinterSettings.FitToHeight = 0; // No limitar el alto


                    // Agregar encabezado con imagen
                    ////var headerFooter = hoja.HeaderFooter;
                    ////headerFooter.OddHeader.CenteredText = "Cotización de Servicios\nEmpresa XYZ";

                    ////string rutaImagen = HttpContext.Current.Server.MapPath("~/Img/piedepagina.png");


                    ////if (File.Exists(rutaImagen))
                    ////{
                    ////   // headerFooter.OddHeader.InsertPicture(rutaImagen, PictureAlignment.Centered);

                    ////    using (Image img = Image.FromFile(rutaImagen)) // Cargar la imagen
                    ////    {
                    ////        headerFooter.OddHeader.InsertPicture(img, PictureAlignment.Centered);
                    ////    }

                    ////}
                    ////else
                    ////{
                    ////    throw new FileNotFoundException($"No se encontró la imagen: {rutaImagen}");
                    ////}



                    ////// Agregar pie de página con texto
                    ////headerFooter.OddFooter.LeftAlignedText = "Página &P de &N"; // Muestra "Página X de Y"
                    ////headerFooter.OddFooter.RightAlignedText = $"Generado el {DateTime.Now:dd/MM/yyyy}";

                    ////// Ajustar para que el encabezado/pie de página aparezcan
                    ////hoja.PrinterSettings.ShowHeaders = true;
                    ////hoja.PrinterSettings.ShowGridLines = false; // Opcional, para quitar líneas de cuadrícula

                    // Guardar archivo
                    FileInfo file = new FileInfo(rutaCompleta);
                    package.SaveAs(file);
                }

                //pruebas de archivo 
                string rutaBase = AppDomain.CurrentDomain.BaseDirectory;
                string subCarpeta = "Reportes";
                string rutaCompleta2 = Path.Combine(rutaBase, subCarpeta, nombreArchivo);
                string archivoUrl = rutaCompleta2;
                //pruebas de archivo fin 

                Uri requestUrl = HttpContext.Current.Request.Url;
                string baseUrl = $"{requestUrl.Scheme}://{requestUrl.Host}:{requestUrl.Port}";
                string archivoUrl2 = $"{baseUrl}/Reportes/{nombreArchivo}";
                Console.WriteLine(baseUrl);
                Console.WriteLine(archivoUrl2);
                Console.WriteLine(archivoUrl);
                string urlBase = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath;
                Console.WriteLine("urlBase", urlBase);
                // Construir la URL completa del archivo
                string archivoUrl3 = $"{urlBase}/Reportes/{nombreArchivo}";
                Console.WriteLine(archivoUrl3);
                return archivoUrl3;
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public class GestionIpDet
        {
            public Int64 Id_Prd { get; set; }
            public string NomProducto { get; set; }
            public string NomCategoria { get; set; }
            public double Unidades { get; set; }
            public double PrecioVenta { get; set; }
            public double Ventas { get; set; }
            public double MgRed_MensualPesos { get; set; }
            public double MgRed_MensualPorc { get; set; }


            public double PrecioObjetivoProy { get; set; }
            public double PrecioListaProy { get; set; }
            public double PrecioMinRikProy { get; set; }
            public double PrecioGteProy { get; set; }
            public double PrecioNegociadoProy { get; set; }
            public double PorcIncrementoProy { get; set; }
            public double DescuentoSobrePlistaProy { get; set; }
            public double UnidadesProyectadas { get; set; }
            public double VentaProy { get; set; }
            public double MgRed_PesosProy { get; set; }
            public double MgRed_PorcProy { get; set; }
            public string Comentarios { get; set; }
            public double CostoAAAAFuturo { get; set; }
            public string NomEstatus { get; set; }

            public int Id_Pc { get; set; }
            //public string CategoriaConv { get; set; }
            public double CostoAAAActual { get; set; }
            //public double PrecioListaActual { get; set; }
            //public double PrecioVentaPromedioActual { get; set; }
            public string Pc_NoConvenio { get; set; }
            public string PC_Nombre { get; set; }
            public string NombreCategoriaConvenio { get; set; }



            public GestionIpDet()
            {


                this.Id_Prd = 0;
                this.NomProducto = "";
                this.NomCategoria = "";
                this.Unidades = 0.00;
                this.PrecioVenta = 0;
                this.Ventas = 0;
                this.MgRed_MensualPesos = 0;
                this.MgRed_MensualPorc = 0;
                this.PrecioObjetivoProy = 0;
                this.PrecioListaProy = 0;
                this.PrecioMinRikProy = 0;
                this.PrecioGteProy = 0;
                this.PrecioNegociadoProy = 0;
                this.PorcIncrementoProy = 0;
                this.DescuentoSobrePlistaProy = 0;

                this.UnidadesProyectadas = 0;
                this.VentaProy = 0;
                this.MgRed_PesosProy = 0;
                this.MgRed_PorcProy = 0;
                this.Comentarios = "";
                this.CostoAAAAFuturo = 0;
                this.NomEstatus = "";

                this.Id_Pc = 0;
                this.CostoAAAActual = 0;
                this.Pc_NoConvenio = "";
                this.PC_Nombre = "";
                this.NombreCategoriaConvenio = "";

                //this.CategoriaConv = "";

                //  this.PrecioListaActual = 0;
                // this.PrecioVentaPromedioActual = 0;



            }
        }

        [WebMethod]
        public static string GenerarListado(List<GestionIpDet> clientes, int dias, int id_cte, string nom_comercial, string representante, string nombrerepresentante, string telefonorik, string fechainicio)
        {
            try
            {

                string nombreArchivo = "ListaArticulos-" + id_cte + "-" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";
                string rutaRelativa = $"~/Reportes/{nombreArchivo}";
                string rutaArchivo = HttpContext.Current.Server.MapPath("~/Reportes/" + nombreArchivo);
                string rutaCompleta = HttpContext.Current.Server.MapPath(rutaRelativa);

                string rutaLogo = HttpContext.Current.Server.MapPath("~/Img/KeyActprecios.png");
                string rutaLogo2 = HttpContext.Current.Server.MapPath("~/Img/key_logo_reciente.png");
                string piedepagina = HttpContext.Current.Server.MapPath("~/Img/piedepagina.png");
                string rutapiedepagina = HttpContext.Current.Server.MapPath("~/Img/piedepagina.png");

                string rutaImagen1 = HttpContext.Current.Server.MapPath("~/Img/banxico.png");
                string rutaimagen2 = HttpContext.Current.Server.MapPath("~/Img/inegi.png");

                MySesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
                //if (MySesion == null)
                //{
                //    response.Message = "connection close";
                //    return response;
                //}


                string empresa = MySesion.Emp_Nombre;


                //Cargar datos cliente 

                Clientes cte = new Clientes();
                cte.Id_Cte = id_cte;
                cte.Id_Emp = MySesion.Id_Emp;
                cte.Id_Cd = MySesion.Id_Cd_Ver;
                //cte.Id_Rik = txtRepresentante.Value.HasValue ? (int)txtRepresentante.Value.Value : (sesion.Id_Rik > 0 ? sesion.Id_Rik : 0);
                CN_CatCliente cnCliente = new CN_CatCliente();
                try
                {
                    cnCliente.ConsultaClientes(ref cte, MySesion.Emp_Cnx);
                }
                catch (Exception ex)
                {
                    //AlertaFocus(ex.Message, txtCliente.ClientID);
                    //txtClienteNombre.Text = "";
                    //txtCliente.Text = "";
                    //if (cmbTerritorio.Items.Count > 0)
                    //{
                    //    cmbTerritorio.SelectedIndex = 1;
                    //    cmbTerritorio.Text = cmbTerritorio.Items[1].Text;
                    //    txtTerritorio.Text = cmbTerritorio.Items[1].Text;

                    //} //txtTerritorio.Value = null;
                    //Limpiar();

                }

                string nombrecom = cte.Cte_NomComercial;
                string calle = cte.Cte_FacCalle;
                string colonia = cte.Cte_FacColonia;
                //txtClienteMunicipio.Text = cte.Cte_FacMunicipio;
                //txtClienteEstado.Text = cte.Cte_FacEstado;
                //txtClienteRFC.Text = cte.Cte_FacRfc;
                //txtClienteCodPost.Text = cte.Cte_FacCp;

                string contacto = cte.Cte_Contacto;
                //txtPuesto.Text = cte.ct;
                string telefono_cliente = cte.Cte_Telefono;
                int diascredito = cte.Cte_CondPago;



                using (var package = new ExcelPackage())
                {
                    // Crear hoja
                    ExcelWorksheet hoja = package.Workbook.Worksheets.Add("Listado");

                    // Agregar logo


                    if (File.Exists(rutaLogo2))
                    {
                        var imagen = hoja.Drawings.AddPicture("Logo2", new FileInfo(rutaLogo2));
                        imagen.SetPosition(0, 0, 0, 0); // Posicionar en la esquina superior izquierda
                        //imagen.SetSize(100, 50); // Ajustar tamaño
                    }


                    hoja.View.ShowGridLines = false;

                    hoja.Cells["I2"].Value = $"Fecha: {DateTime.Now:dd/MM/yyyy}";
                    hoja.Cells["I2"].Style.Font.Italic = true;
                    hoja.Cells["I2"].Style.Font.Bold = true;
                    hoja.Cells["I3"].Value = "Key Química SA de CV";
                    hoja.Cells["I4"].Value = "KQU6911016X5";
                    //hoja.Cells["I5"].Value = " *telefono key*";
                    //hoja.Cells["I6"].Value = " *dirección*";
                    using (var range = hoja.Cells["I2:I6"])
                    {
                        range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right; // Alineación justificada
                        range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top; // Alineación vertical superior
                        range.Style.Font.Bold = true;
                        range.Style.Font.Italic = true;
                        range.Style.Font.Size = 11; // Tamaño de fuente adecuado para lectura 
                    }

                    hoja.Cells["A7"].Value = "LISTADO DE PRODUCTOS";

                    hoja.Cells["C9"].Value = "Empresa: ";
                    hoja.Cells["C10"].Value = "Atención: ";
                    hoja.Cells["D9"].Value = nom_comercial;
                    hoja.Cells["D10"].Value = contacto;
                    using (var range = hoja.Cells["A7:C10"])
                    {
                        range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left; // Alineación justificada
                        range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top; // Alineación vertical superior
                        range.Style.Font.Bold = true;

                        range.Style.Font.Size = 11; // Tamaño de fuente adecuado para lectura

                    }

                    // Encabezado con el nombre de la empresa y fecha actual
                    //hoja.Cells["A1"].Value = id_cte + " - "+ nom_comercial;
                    //hoja.Cells["A1"].Style.Font.Size = 14;
                    //hoja.Cells["A1"].Style.Font.Bold = true;


                    //using (var range = hoja.Cells["J138:K142"])
                    //{
                    //    range.Style.Font.Bold = true;
                    //    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                    //}
                    //using (var range = hoja.Cells["C16:J74"])
                    //{
                    //    range.Style.Font.Bold = false;
                    //    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                    //}


                    // Escribir encabezados de la tabla
                    int filaInicial = 11;
                    int columna = 1;




                    // Agregar encabezados
                    var propiedades = typeof(GestionIpDet).GetProperties();
                    for (int i = 0; i < propiedades.Length; i++)
                    {

                        if (propiedades[i].Name != "Pc_NoConvenio" && propiedades[i].Name != "PC_Nombre" && propiedades[i].Name != "NombreCategoriaConvenio" && propiedades[i].Name != "CategoriaConv" && propiedades[i].Name != "PrecioListaActual" && propiedades[i].Name != "PrecioVentaPromedioActual" && propiedades[i].Name != "CostoAAAActual")
                        {
                            hoja.Cells[filaInicial, i + 1].Value = propiedades[i].Name;
                            hoja.Cells[filaInicial, i + 1].Style.Font.Bold = true;
                            hoja.Cells[filaInicial, i + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            hoja.Cells[filaInicial, i + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                            hoja.Cells[filaInicial, i + 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        }
                    }

                    string rango = $"E{filaInicial}";

                    // Agregar datos
                    for (int i = 0; i < clientes.Count; i++)
                    {
                        filaInicial++;
                        for (int j = 0; j < propiedades.Length; j++)
                        {
                            if (propiedades[j].Name != "Pc_NoConvenio" && propiedades[j].Name != "PC_Nombre" && propiedades[j].Name != "NombreCategoriaConvenio" && propiedades[j].Name != "CategoriaConv" && propiedades[j].Name != "PrecioListaActual" && propiedades[j].Name != "PrecioVentaPromedioActual" && propiedades[j].Name != "CostoAAAActual")
                            {
                                hoja.Cells[filaInicial, j + 1].Value = propiedades[j].GetValue(clientes[i]);
                            }
                        }
                    }

                    int row = filaInicial + 2;
                    rango = rango + $":U{ filaInicial  }";

                    hoja.Cells[rango].Style.Numberformat.Format = "#,##0.00";
                    hoja.Cells[rango].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;


                    //foreach (var cliente in clientes)
                    //{
                    //    hoja.Cells[row, 3].Value = cliente.Id_Prd;
                    //    hoja.Cells[row, 4].Value = cliente.NomProducto;
                    //    hoja.Cells[row, 5].Value = cliente.PrecioNegociadoProy;
                    //    // Aplicar formato numérico con comas
                    //    hoja.Cells[row, 5].Style.Numberformat.Format = "#,##0.00";
                    //    hoja.Cells[row, 5].Style.Numberformat.Format = "#,##0.00";

                    //    row++;
                    //}

                    // Ajustar el ancho de las columnas

                    hoja.Column(2).Width = 40; // Id_prd
                    hoja.Column(3).Width = 20; // Id_prd


                    row++;


                    hoja.PrinterSettings.Orientation = eOrientation.Landscape; // Orientación horizontal


                    // Guardar archivo
                    FileInfo file = new FileInfo(rutaCompleta);
                    package.SaveAs(file);
                }

                //Uri requestUrl = HttpContext.Current.Request.Url;
                //string baseUrl = $"{requestUrl.Scheme}://{requestUrl.Host}:{requestUrl.Port}";
                //string archivoUrl = $"{baseUrl}/Reportes/{nombreArchivo}";

                //return archivoUrl;

                //pruebas de archivo 
                string rutaBase = AppDomain.CurrentDomain.BaseDirectory;
                string subCarpeta = "Reportes";
                string rutaCompleta2 = Path.Combine(rutaBase, subCarpeta, nombreArchivo);
                string archivoUrl = rutaCompleta2;
                //pruebas de archivo fin 

                Uri requestUrl = HttpContext.Current.Request.Url;
                string baseUrl = $"{requestUrl.Scheme}://{requestUrl.Host}:{requestUrl.Port}";
                string archivoUrl2 = $"{baseUrl}/Reportes/{nombreArchivo}";
                Console.WriteLine(baseUrl);
                Console.WriteLine(archivoUrl2);
                Console.WriteLine(archivoUrl);
                string urlBase = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath;
                Console.WriteLine("urlBase", urlBase);
                // Construir la URL completa del archivo
                string archivoUrl3 = $"{urlBase}/Reportes/{nombreArchivo}";
                Console.WriteLine(archivoUrl3);
                return archivoUrl3;

            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        [WebMethod]
        public static string Guardar(List<GestionIncrementoPrecios> clientes, string id_tamaño, string tipocuenta, int id_matriz, string nombre_matriz, int dias, int id_cte, string nom_comercial, string representante, string nombrerepresentante, string telefonorik, string fechainicio)
        {
            try
            {
                MySesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
                //if (MySesion == null)
                //{
                //    response.Message = "connection close";
                //    return response;
                //}


                string empresa = MySesion.Emp_Nombre;


                //Cargar datos cliente 

                Clientes cte = new Clientes();
                cte.Id_Cte = id_cte;
                cte.Id_Emp = MySesion.Id_Emp;
                cte.Id_Cd = MySesion.Id_Cd_Ver;
                //cte.Id_Rik = txtRepresentante.Value.HasValue ? (int)txtRepresentante.Value.Value : (sesion.Id_Rik > 0 ? sesion.Id_Rik : 0);
                CN_CatCliente cnCliente = new CN_CatCliente();
                try
                {
                    cnCliente.ConsultaClientes(ref cte, MySesion.Emp_Cnx);
                }
                catch (Exception ex)
                {
                    //AlertaFocus(ex.Message, txtCliente.ClientID);
                    //txtClienteNombre.Text = "";
                    //txtCliente.Text = "";
                    //if (cmbTerritorio.Items.Count > 0)
                    //{
                    //    cmbTerritorio.SelectedIndex = 1;
                    //    cmbTerritorio.Text = cmbTerritorio.Items[1].Text;
                    //    txtTerritorio.Text = cmbTerritorio.Items[1].Text;

                    //} //txtTerritorio.Value = null;
                    //Limpiar();

                }

                string nombrecom = cte.Cte_NomComercial;
                string contacto = cte.Cte_Contacto;
                string telefono_cliente = cte.Cte_Telefono;
                int diascredito = cte.Cte_CondPago;


                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
                Usuario usuario = new Usuario();
                CN_ReporteGAP cnreportegap = new CN_ReporteGAP();
                GestionIncrementoPrecios encabezado = new GestionIncrementoPrecios();
                CapaEntidad.GestionIncrementoPrecios reporteGP = new CapaEntidad.GestionIncrementoPrecios();

                //reporteGAP.Id_Cd = Convert.ToInt32(CmbSucursal.Value.ToString());
                encabezado.Id_Cd = MySesion.Id_Cd_Ver;
                encabezado.Id_Emp = MySesion.Id_Emp;
                encabezado.Id_Rik = Convert.ToInt32(representante);
                encabezado.Id_Cte = id_cte;
                encabezado.Cte_NomComercial = nombrecom;

                encabezado.Id_Matriz = id_matriz;
                encabezado.NombreMatriz = nombre_matriz;
                encabezado.TipoCuenta = tipocuenta;
                encabezado.Id_Tamaño = id_tamaño;
                encabezado.Ventas = 0;
                encabezado.VentaProy = 0;
                encabezado.Var_VentaMonto = 0;
                encabezado.MgRed_MontoActual = 0;
                encabezado.MgRed_Proyectada = 0;
                encabezado.VarMgRed_Monto = 0;
                encabezado.VarMgRed_Porc = 0;
                encabezado.NomEstatus = "Analizado";

                encabezado.Telefono = telefonorik;
                encabezado.DiasVigencia = dias;
                encabezado.FechaInicioIncremento = Convert.ToDateTime(fechainicio);
                encabezado.PorcentualIncremental = 0.00;
                encabezado.IdUsuario = MySesion.Id_U;

                int verificador = 0;
                verificador = CN_ReporteGAP.InsertarGestionIncremento(encabezado, clientes, Conexion, ref verificador);

                return verificador.ToString();
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }


        [WebMethod]
        public static object GetPropuesta(int id_cte, string representante, int idreportegp)
        {
            try
            {

                MySesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
                string empresa = MySesion.Emp_Nombre;

                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
                Usuario usuario = new Usuario();
                CN_ReporteGAP cnreportegap = new CN_ReporteGAP();
                GestionIncrementoPrecios encabezado = new GestionIncrementoPrecios();
                CapaEntidad.GestionIncrementoPrecios reporteGP = new CapaEntidad.GestionIncrementoPrecios();

                encabezado.Id_Cd = MySesion.Id_Cd_Ver;
                encabezado.Id_Emp = MySesion.Id_Emp;
                encabezado.Id_Rik = Convert.ToInt32(representante);
                encabezado.Id_Cte = id_cte;

                encabezado.PorcentualIncremental = 0.00;

                int idreportegap = idreportegp;
                object datos = new object();
                object dd = new object();
                dd = CN_ReporteGAP.ConsultaPropuesta(id_cte, Convert.ToInt32(representante), MySesion.Id_Cd_Ver, MySesion.Emp_Cnx, ref idreportegap, ref datos);
                return datos;
            }
            catch (Exception ex)
            {
                //AlertaFocus(ex.Message, txtCliente.ClientID);
                //txtClienteNombre.Text = "";
                //txtCliente.Text = "";
                //if (cmbTerritorio.Items.Count > 0)
                //{
                //    cmbTerritorio.SelectedIndex = 1;
                //    cmbTerritorio.Text = cmbTerritorio.Items[1].Text;
                //    txtTerritorio.Text = cmbTerritorio.Items[1].Text;

                //} //txtTerritorio.Value = null;
                //Limpiar();
                return null;

            }

        }



        [WebMethod]
        public static string Cerrar(List<GestionIncrementoPrecios> clientes, string id_tamaño, string tipocuenta, int id_matriz, string nombre_matriz, int dias, int id_cte, string nom_comercial, string representante, string nombrerepresentante, string telefonorik, string fechainicio, int IdPropuestaGP)
        {
            try
            {

                MySesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];


                string empresa = MySesion.Emp_Nombre;

                //Cargar datos cliente 

                Clientes cte = new Clientes();
                cte.Id_Cte = id_cte;
                cte.Id_Emp = MySesion.Id_Emp;
                cte.Id_Cd = MySesion.Id_Cd_Ver;
                //cte.Id_Rik = txtRepresentante.Value.HasValue ? (int)txtRepresentante.Value.Value : (sesion.Id_Rik > 0 ? sesion.Id_Rik : 0);
                CN_CatCliente cnCliente = new CN_CatCliente();
                try
                {
                    cnCliente.ConsultaClientes(ref cte, MySesion.Emp_Cnx);
                }
                catch (Exception ex)
                {
                    //AlertaFocus(ex.Message, txtCliente.ClientID);


                }

                string nombrecom = cte.Cte_NomComercial;
                string calle = cte.Cte_FacCalle;
                string colonia = cte.Cte_FacColonia;
                //txtClienteMunicipio.Text = cte.Cte_FacMunicipio;
                //txtClienteEstado.Text = cte.Cte_FacEstado;
                //txtClienteRFC.Text = cte.Cte_FacRfc;
                //txtClienteCodPost.Text = cte.Cte_FacCp;

                string contacto = cte.Cte_Contacto;
                //txtPuesto.Text = cte.ct;
                string telefono_cliente = cte.Cte_Telefono;
                int diascredito = cte.Cte_CondPago;


                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
                Usuario usuario = new Usuario();
                CN_ReporteGAP cnreportegap = new CN_ReporteGAP();
                GestionIncrementoPrecios encabezado = new GestionIncrementoPrecios();
                CapaEntidad.GestionIncrementoPrecios reporteGP = new CapaEntidad.GestionIncrementoPrecios();

                //reporteGAP.Id_Cd = Convert.ToInt32(CmbSucursal.Value.ToString());
                encabezado.Id_Cd = MySesion.Id_Cd_Ver;
                encabezado.Id_Emp = MySesion.Id_Emp;
                encabezado.Id_Rik = Convert.ToInt32(representante);
                encabezado.Id_Cte = id_cte;
                encabezado.Cte_NomComercial = nombrecom;

                encabezado.Id_Matriz = id_matriz;
                encabezado.NombreMatriz = nombre_matriz;
                encabezado.TipoCuenta = tipocuenta;
                encabezado.Id_Tamaño = id_tamaño;
                encabezado.Ventas = 0;
                encabezado.VentaProy = 0;
                encabezado.Var_VentaMonto = 0;
                encabezado.MgRed_MontoActual = 0;
                encabezado.MgRed_Proyectada = 0;
                encabezado.VarMgRed_Monto = 0;
                encabezado.VarMgRed_Porc = 0;
                encabezado.NomEstatus = "Analizado";

                encabezado.Telefono = telefonorik;
                encabezado.DiasVigencia = dias;
                encabezado.FechaInicioIncremento = Convert.ToDateTime(fechainicio);
                encabezado.PorcentualIncremental = 0.00;
                encabezado.Id_Estatus = 4;
                encabezado.IdUsuario = MySesion.Id_U;
                encabezado.IdReporteGP = IdPropuestaGP;
                int verificador = 0;
                verificador = CN_ReporteGAP.CerrarGestionIncremento(encabezado, Conexion, ref verificador);


                return "";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        [WebMethod]
        public static string EnviarPropuesta(int id_cte, string nom_comercial, string representante, string nombrerepresentante, string telefonorik, string fechainicio, int idPropuestaGP)
        {
            try
            {

                MySesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];


                string empresa = MySesion.Emp_Nombre;
                string correo = MySesion.U_Correo;

                //Cargar datos cliente 

                Clientes cte = new Clientes();
                cte.Id_Cte = id_cte;
                cte.Id_Emp = MySesion.Id_Emp;
                cte.Id_Cd = MySesion.Id_Cd_Ver;
                //cte.Id_Rik = txtRepresentante.Value.HasValue ? (int)txtRepresentante.Value.Value : (sesion.Id_Rik > 0 ? sesion.Id_Rik : 0);
                CN_CatCliente cnCliente = new CN_CatCliente();
                try
                {
                    cnCliente.ConsultaClientes(ref cte, MySesion.Emp_Cnx);
                }
                catch (Exception ex)
                {
                    //AlertaFocus(ex.Message, txtCliente.ClientID);

                }

                string nombrecom = cte.Cte_NomComercial;
                string calle = cte.Cte_FacCalle;
                string colonia = cte.Cte_FacColonia;


                string contacto = cte.Cte_Contacto;

                string telefono_cliente = cte.Cte_Telefono;
                int diascredito = cte.Cte_CondPago;


                string Conexion = System.Configuration.ConfigurationManager.AppSettings["strConnectionCentral"];
                Usuario usuario = new Usuario();
                CN_ReporteGAP cnreportegap = new CN_ReporteGAP();
                GestionIncrementoPrecios encabezado = new GestionIncrementoPrecios();
                CapaEntidad.GestionIncrementoPrecios reporteGP = new CapaEntidad.GestionIncrementoPrecios();

                //reporteGAP.Id_Cd = Convert.ToInt32(CmbSucursal.Value.ToString());
                encabezado.Id_Cd = MySesion.Id_Cd_Ver;
                encabezado.Id_Emp = MySesion.Id_Emp;
                encabezado.Id_Rik = Convert.ToInt32(representante);
                encabezado.Id_Cte = id_cte;
                encabezado.Cte_NomComercial = nombrecom;

                encabezado.NomEstatus = "Precios Aceptados";

                encabezado.Telefono = telefonorik;

                encabezado.FechaInicioIncremento = Convert.ToDateTime(fechainicio);
                encabezado.PorcentualIncremental = 0.00;
                encabezado.Id_Estatus = 3;
                encabezado.IdUsuario = MySesion.Id_U;


                encabezado.IdReporteGP = idPropuestaGP;
                int verificador = 0;
                verificador = CN_ReporteGAP.CerrarGestionIncremento(encabezado, Conexion, ref verificador);


                return "";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        [WebMethod]
        public void ExportarAExcel(List<CapaEntidad.GestionIncrementoPrecios> clientes)
        {
            try
            {
                using (ExcelPackage excel = new ExcelPackage())
                {
                    var hoja = excel.Workbook.Worksheets.Add("Clientes");

                    // Agregar encabezados
                    var propiedades = typeof(GestionIncrementoPrecios).GetProperties();
                    for (int i = 0; i < propiedades.Length; i++)
                    {
                        hoja.Cells[1, i + 1].Value = propiedades[i].Name;
                        hoja.Cells[1, i + 1].Style.Font.Bold = true;
                    }

                    // Agregar datos
                    for (int i = 0; i < clientes.Count; i++)
                    {
                        for (int j = 0; j < propiedades.Length; j++)
                        {
                            hoja.Cells[i + 2, j + 1].Value = propiedades[j].GetValue(clientes[i]);
                        }
                    }

                    // Descargar el archivo
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=Clientes.xlsx");
                    HttpContext.Current.Response.BinaryWrite(excel.GetAsByteArray());
                    HttpContext.Current.Response.End();
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.StatusCode = 500;
                HttpContext.Current.Response.Write(ex.Message);
                HttpContext.Current.Response.End();
            }
        }
        [WebMethod]
        public void ExportarAExcel2(List<Dictionary<string, object>> clientes)
        {
            try
            {
                using (ExcelPackage excel = new ExcelPackage())
                {
                    var hoja = excel.Workbook.Worksheets.Add("Clientes");

                    // Agregar encabezados
                    if (clientes.Any())
                    {
                        var columnas = clientes.First().Keys.ToList(); // Obtener nombres de columnas
                        for (int i = 0; i < columnas.Count; i++)
                        {
                            hoja.Cells[1, i + 1].Value = columnas[i];
                            hoja.Cells[1, i + 1].Style.Font.Bold = true;
                        }

                        // Agregar datos
                        for (int i = 0; i < clientes.Count; i++)
                        {
                            for (int j = 0; j < columnas.Count; j++)
                            {
                                hoja.Cells[i + 2, j + 1].Value = clientes[i][columnas[j]];
                            }
                        }
                    }

                    // Descargar el archivo
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=Clientesdetalles.xlsx");
                    HttpContext.Current.Response.BinaryWrite(excel.GetAsByteArray());
                    HttpContext.Current.Response.End();
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.StatusCode = 500;
                HttpContext.Current.Response.Write(ex.Message);
                HttpContext.Current.Response.End();
            }
        }

        [WebMethod]
        public static string GenerarPDF(List<Cliente> clientes, int dias, int id_cte, string nom_comercial, string representante, string nombrerepresentante, string telefonorik, string fechainicio, string correo)
        {

            try
            {

                PdfDocument pdf = new PdfDocument();

                PdfPageBase paginapresentacion = pdf.Pages.Add();
                PdfFont tituloFont = new PdfFont(PdfFontFamily.Helvetica, 16f, PdfFontStyle.Bold);
                PdfFont textoFont = new PdfFont(PdfFontFamily.TimesRoman, 10f);
                PdfFont textoFontBold = new PdfFont(PdfFontFamily.TimesRoman, 10f, PdfFontStyle.Bold);
                PdfFont textoFontBoldPresentacion = new PdfFont(PdfFontFamily.TimesRoman, 12f, PdfFontStyle.Bold);
                PdfStringFormat formatoCentrado = new PdfStringFormat(PdfTextAlignment.Center);
                PdfStringFormat formatoDerecha = new PdfStringFormat(PdfTextAlignment.Right);
                PdfStringFormat formatoIzquierda = new PdfStringFormat(PdfTextAlignment.Left);
                PdfStringFormat formatojustificado = new PdfStringFormat(PdfTextAlignment.Justify);
                PdfBrush brocha = PdfBrushes.Black;
                //declarar 
                PdfTrueTypeFont fuenteTextofinal = new PdfTrueTypeFont(new Font("Arial", 9f), true);


                string nombreArchivo = "Cotizacion" + id_cte + "-" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";
                string rutaRelativa = $"~/Reportes/{nombreArchivo}";
                string rutaArchivo = HttpContext.Current.Server.MapPath("~/Reportes/" + nombreArchivo);
                string rutaCompleta = HttpContext.Current.Server.MapPath(rutaRelativa);

                string rutaLogo = HttpContext.Current.Server.MapPath("~/Img/KeyActprecios.png");
                string rutaLogo2 = HttpContext.Current.Server.MapPath("~/Img/key_logo_reciente.png");
                string piedepagina = HttpContext.Current.Server.MapPath("~/Img/piedepagina.png");
                string rutapiedepagina = HttpContext.Current.Server.MapPath("~/Img/piedepagina.png");
                PdfImage imagenPiedepagina = PdfImage.FromFile(rutapiedepagina);

                string rutaImagen1 = HttpContext.Current.Server.MapPath("~/Img/aum2025.png");
                string rutaimagen2 = HttpContext.Current.Server.MapPath("~/Img/inegi.png");

                MySesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
                //if (MySesion == null)
                //{
                //    response.Message = "connection close";
                //    return response;
                //}


                string empresa = MySesion.Emp_Nombre;

                //que envie el correo al que recibe de la pagina
                //string correo = MySesion.U_Correo;

                //Cargar datos cliente 

                Clientes cte = new Clientes();
                cte.Id_Cte = id_cte;
                cte.Id_Emp = MySesion.Id_Emp;
                cte.Id_Cd = MySesion.Id_Cd_Ver;
                //cte.Id_Rik = txtRepresentante.Value.HasValue ? (int)txtRepresentante.Value.Value : (sesion.Id_Rik > 0 ? sesion.Id_Rik : 0);
                CN_CatCliente cnCliente = new CN_CatCliente();
                try
                {
                    cnCliente.ConsultaClientes(ref cte, MySesion.Emp_Cnx);
                }
                catch (Exception ex)
                {


                }

                string nombrecom = cte.Cte_NomComercial;
                string calle = cte.Cte_FacCalle;
                string colonia = cte.Cte_FacColonia;

                string contacto = cte.Cte_Contacto;
                //txtPuesto.Text = cte.ct;
                string telefono_cliente = cte.Cte_Telefono;
                int diascredito = cte.Cte_CondPago;
                //que envie el correo al que recibe de la pagina
                //correo = cte.Cte_Email;
                #region Inicia a insertar datos en pdf 1
                // Crear hoja




                if (File.Exists(rutaLogo))
                {
                    PdfImage imagenpresentacion = PdfImage.FromFile(rutaLogo);
                    paginapresentacion.Canvas.DrawImage(imagenpresentacion, new RectangleF(0, 0, 580, 70));
                }


                // Obtener el ancho y alto de la página
                float anchoPagina1 = paginapresentacion.Canvas.ClientSize.Width;
                float altoImagen1 = (imagenPiedepagina.PhysicalDimension.Height / imagenPiedepagina.PhysicalDimension.Width) * anchoPagina1;

                // Dibujar la imagen en la parte inferior de la página
                float posicionY1 = paginapresentacion.Canvas.ClientSize.Height - altoImagen1;
                paginapresentacion.Canvas.DrawImage(imagenPiedepagina, 0, posicionY1, anchoPagina1, altoImagen1);

                string titulo = "Reporte de Clientes";




                // Crear cultura española
                CultureInfo cultura = new CultureInfo("es-MX");


                // Formatear la fecha 
                string fechaFormateada = DateTime.Now.ToString("dd 'de' MMMM 'de' yyyy", cultura);


                int renglon1 = 90;

                //paginapresentacion.Canvas.DrawString($"        {fechaFormateada}", textoFontBold, brocha, 480, renglon1, formatoDerecha);

                //renglon1 = renglon1 + 30;
                //paginapresentacion.Canvas.DrawString("Estimado cliente", textoFontBoldPresentacion, brocha, 84, renglon1, formatoDerecha);

                //renglon1 = renglon1 + 70; //20


                //INICIO 

                // Ciudad + fecha
                paginapresentacion.Canvas.DrawString($" {fechaFormateada}.", textoFontBold, brocha, 400, renglon1, formatoDerecha);

                renglon1 += 30;

                // Saludo
                paginapresentacion.Canvas.DrawString("Estimado cliente:", textoFontBoldPresentacion, brocha, 50, renglon1, formatoIzquierda);

                renglon1 += 30;

                // Configuración párrafos
                PdfTrueTypeFont fuenteTexto = new PdfTrueTypeFont(new Font("Arial", 10f), true);
                PdfStringFormat formatoJustificado = new PdfStringFormat { Alignment = PdfTextAlignment.Justify };

                // Párrafo 1
                string textoParrafo = "En primer lugar, queremos reiterarle nuestro firme compromiso de continuar ofreciéndole las mejores soluciones disponibles al precio más competitivo posible, así como nuestro acompañamiento y servicio constantes, aun en un entorno de mercado particularmente desafiante como el actual.";

                RectangleF rect = new RectangleF(50, renglon1, paginapresentacion.Canvas.ClientSize.Width - 100, 60);
                paginapresentacion.Canvas.DrawString(textoParrafo, fuenteTexto, brocha, rect, formatoJustificado);
                //anterior 
                float anchoPagina = paginapresentacion.Canvas.ClientSize.Width;
                float altoImagen;
                altoImagen = 140; // Alto de las imágenes
                float anchoColumna = anchoPagina; // / 2; // Dividir en dos columnas


                renglon1 += 60;

                // Párrafo 2
                textoParrafo = "Como es de su conocimiento, la situación geopolítica internacional ha generado una alta volatilidad en los precios internacionales del petróleo (ver anexos), el cual constituye el principal insumo base para la producción de solventes. A ello se suman incrementos significativos en los costos de fletes internacionales y locales, derivados tanto de la coyuntura geopolítica como del cierre de rutas estratégicas, como el Canal de Ormuz, así como del incremento sostenido en el precio del diésel. Todos estos factores impactan de manera directa nuestros costos logísticos y operativos.";

                rect = new RectangleF(50, renglon1, paginapresentacion.Canvas.ClientSize.Width - 100, 120);
                paginapresentacion.Canvas.DrawString(textoParrafo, fuenteTexto, brocha, rect, formatoJustificado);

                renglon1 += 90;

                // Párrafo 3
                textoParrafo = "Adicionalmente, se ha presentado una afectación relevante debido a los aranceles aplicados en México a los derivados de hidrocarburos, lo que complica la disponibilidad y eleva el costo de las principales materias primas utilizadas en la elaboración de solventes. En conjunto, estos elementos generan un entorno de alta presión sobre toda la cadena de suministro.";

                rect = new RectangleF(50, renglon1, paginapresentacion.Canvas.ClientSize.Width - 100, 80);
                paginapresentacion.Canvas.DrawString(textoParrafo, fuenteTexto, brocha, rect, formatoJustificado);

                renglon1 += 60;

                // Párrafo 4
                textoParrafo = "Derivado de lo anterior, nos vemos en la necesidad de informarle que será indispensable realizar ajustes inmediatos en los precios de ciertos productos. Asimismo, no nos es posible comprometernos a mantener precios fijos en el mediano y largo plazo, ya que los ajustes en materias primas y logística continúan presentándose de manera acelerada e impredecible.";

                rect = new RectangleF(50, renglon1, paginapresentacion.Canvas.ClientSize.Width - 100, 70);
                paginapresentacion.Canvas.DrawString(textoParrafo, fuenteTexto, brocha, rect, formatoJustificado);

                renglon1 += 60;

                // Párrafo 5
                textoParrafo = "En este contexto, recomendamos evitar órdenes abiertas, así como convenios de suministro a largo plazo bajo esquemas de precio fijo, ya que no podríamos garantizar su sostenibilidad bajo las condiciones actuales del mercado. De igual forma, nos será imposible respetar back orders que contemplen condiciones de precio previamente acordadas.";

                rect = new RectangleF(50, renglon1, paginapresentacion.Canvas.ClientSize.Width - 100, 70);
                paginapresentacion.Canvas.DrawString(textoParrafo, fuenteTexto, brocha, rect, formatoJustificado);

                renglon1 += 60;

                // Párrafo 6
                textoParrafo = "Queremos dejar en claro que, en caso de que el precio del petróleo muestre una tendencia sostenida a la baja, realizaremos los ajustes correspondientes en nuestros precios de venta, reflejando dichas condiciones de mercado de manera responsable y transparente. No obstante, es importante considerar que, históricamente, los precios del petróleo tienden a incrementarse de forma más rápida de lo que disminuyen, por lo que los ajustes a la baja suelen darse de manera gradual.";

                rect = new RectangleF(50, renglon1, paginapresentacion.Canvas.ClientSize.Width - 100, 80);
                paginapresentacion.Canvas.DrawString(textoParrafo, fuenteTexto, brocha, rect, formatoJustificado);

                renglon1 += 80;

                // Cierre
                textoParrafo = "Agradecemos sinceramente su comprensión y la confianza que ha depositado en nosotros. Quedamos a su disposición para analizar alternativas de suministro, esquemas de compra y soluciones que se adapten a esta coyuntura, con el objetivo de continuar fortaleciendo nuestra relación comercial.";

                rect = new RectangleF(50, renglon1, paginapresentacion.Canvas.ClientSize.Width - 100, 60);
                paginapresentacion.Canvas.DrawString(textoParrafo, fuenteTexto, brocha, rect, formatoJustificado);

                renglon1 += 70;

                // Despedida
                paginapresentacion.Canvas.DrawString("Reciba un cordial saludo,", fuenteTexto, brocha, 50, renglon1);

                renglon1 += 30;

                // Firma
                paginapresentacion.Canvas.DrawString("ATENTAMENTE", textoFontBold, brocha, 50, renglon1);

                renglon1 += 30;

                paginapresentacion.Canvas.DrawString("Equipo Comercial Key Química", fuenteTexto, brocha, 50, renglon1);


                #endregion pagina 1 

                #region Inicia a insertar datos en pdf pagina2
                PdfPageBase pagina = pdf.Pages.Add();


                if (File.Exists(rutaLogo2))
                {
                    //var imagen = hoja.Drawings.AddPicture("Logo2", new FileInfo(rutaLogo2));
                    //imagen.SetPosition(0, 0, 0, 0); // Posicionar en la esquina superior izquierda
                    //                                //imagen.SetSize(100, 50); // Ajustar tamaño
                    PdfImage imagen = PdfImage.FromFile(rutaLogo2);
                    pagina.Canvas.DrawImage(imagen, new RectangleF(0, 0, 140, 80));


                }



                pagina.Canvas.DrawString($"Fecha: {DateTime.Now:dd/MM/yyyy}", textoFontBold, brocha, 510, 10, formatoDerecha);
                pagina.Canvas.DrawString("Key Química SA de CV", textoFontBold, brocha, 510, 25, formatoDerecha);
                pagina.Canvas.DrawString("KQU6911016X5", textoFontBold, brocha, 510, 40, formatoDerecha);
                PdfStringFormat formatoaDerecha = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);


                titulo = "PROPUESTA ECONÓMICA";
                pagina.Canvas.DrawString(titulo, textoFontBold, brocha, 10, 80, formatojustificado);

                pagina.Canvas.DrawString("Empresa: ", textoFontBold, brocha, 10, 100, formatojustificado);
                pagina.Canvas.DrawString("Atención: ", textoFontBold, brocha, 10, 110, formatojustificado);

                pagina.Canvas.DrawString(nom_comercial, textoFont, brocha, 110, 100, formatoIzquierda);
                pagina.Canvas.DrawString(contacto, textoFont, brocha, 110, 110, formatoIzquierda);


                //parrafo

                textoParrafo = "Key es una empresa 100% mexicana con más de 50 años de experiencia en la industria, lo que nos ha dado el conocimiento para "
                   + "entender que en la actualidad los clientes son más demandantes con la limpieza y con espacios más limpios y sanos, por lo que "
                   + "nos hemos especializado en no solo ofrecer químicos y accesorios, sino en desarrollar un programa completo de higiene, "
                   + "personalizado, según las necesidades de cada cliente, que les ayude a cumplir con altos estándares de limpieza de manera simple y a "
                   + "un costo óptimo. ";


                int renglon = 140;
                formatoJustificado.Alignment = PdfTextAlignment.Justify;
                RectangleF rectanguloTexto = new RectangleF(10, renglon, pagina.Canvas.ClientSize.Width - 20, 100);

                // Dibujar el párrafo en el PDF
                pagina.Canvas.DrawString(textoParrafo, fuenteTexto, brocha, rectanguloTexto, formatoJustificado);


                titulo = "Esperando podamos colaborar e integrar un equipo, nos reiteramos a sus órdenes. ";
                pagina.Canvas.DrawString(titulo, fuenteTexto, brocha, 10, 200, formatojustificado);



                PdfTable tablaPDF = new PdfTable
                {
                    Style =
                    {
                        ShowHeader = true,
                        HeaderStyle = { Font = new PdfFont(PdfFontFamily.Helvetica, 10f, PdfFontStyle.Bold), BackgroundBrush = PdfBrushes.LightGray },
                        DefaultStyle = { Font = fuenteTexto, TextBrush = brocha }
                    }
                };

                tablaPDF.Style.HeaderSource = PdfHeaderSource.ColumnCaptions;
                tablaPDF.Style.DefaultStyle.Font = new PdfTrueTypeFont(new Font("Arial", 8f), true);

                var tablaDatos = new DataTable();
                tablaDatos.Columns.Add("Producto");
                tablaDatos.Columns.Add("Descripción");
                tablaDatos.Columns.Add("Precio");

                renglon = 230;
                int iclientes = 0;
                foreach (var cliente in clientes)
                {
                    renglon = renglon + 10;
                    iclientes++;
                    tablaDatos.Rows.Add(cliente.Id_Prd, cliente.NomProducto, cliente.PrecioNegociadoProy.ToString("N2", CultureInfo.CreateSpecificCulture("es-MX")));
                }


                tablaPDF.DataSource = tablaDatos;


                tablaPDF.Columns[0].Width = 70;
                tablaPDF.Columns[1].Width = 280;
                tablaPDF.Columns[2].Width = 70;
                tablaPDF.Columns[2].StringFormat = formatoaDerecha;

                //PdfLayoutResult resultadoTabla = tablaPDF.Draw(pagina, new PointF(20, 220));

                //as lo tenia en mi c´digo 27ene2025
                //tablaPDF.Draw(pagina, new PointF(20, 220));

                float altoDisponible = pagina.Canvas.ClientSize.Height - 200; // Espacio disponible
                                                                              //while (resultadoTabla.Bounds.Bottom + 30 > altoDisponible)
                                                                              //{
                                                                              //    pagina = pdf.Pages.Add(); // Agregar nueva página
                                                                              //    renglon = 20; // Reiniciar renglón en la nueva página
                                                                              //    resultadoTabla = tablaPDF.Draw(pagina, new PointF(20, renglon));
                                                                              //    renglon = 20 + (iclientes*10);
                                                                              //}
                                                                              // Variables de control
                int totalRenglones = tablaDatos.Rows.Count;
                int renglonesPorPagina = 25;
                int renglonesMostrados = 0;
                renglon = 220;

                while (renglonesMostrados < totalRenglones)
                {
                    // Definir cuántos renglones mostrar en esta página
                    int renglonesEnEstaPagina = renglonesMostrados == 0 ? 25 : 70;
                    renglonesEnEstaPagina = Math.Min(renglonesEnEstaPagina, totalRenglones - renglonesMostrados);

                    // Extraer los datos de la tabla para esta página
                    DataTable tablaPagina = tablaDatos.Clone();
                    for (int i = renglonesMostrados; i < renglonesMostrados + renglonesEnEstaPagina; i++)
                    {
                        tablaPagina.ImportRow(tablaDatos.Rows[i]);
                    }

                    // Dibujar la tabla
                    PdfTable tablaTemp = new PdfTable { DataSource = tablaPagina };
                    tablaTemp.Style = tablaPDF.Style;

                    tablaTemp.Columns[0].Width = 70;
                    tablaTemp.Columns[1].Width = 260;
                    tablaTemp.Columns[2].Width = 70;
                    tablaTemp.Columns[2].StringFormat = formatoaDerecha;

                    PdfLayoutResult resultadoTabla = tablaTemp.Draw(pagina, new PointF(20, renglon));

                    // Agregar pie de página con imagen
                    //string rutaImagen = "footer_image.jpg";
                    //PdfImage imagenPiedepagina = PdfImage.FromFile(rutaImagen);
                    //float anchoPagina = pagina.Canvas.ClientSize.Width;
                    //float altoImagen = (imagenPiedepagina.PhysicalDimension.Height / imagenPiedepagina.PhysicalDimension.Width) * anchoPagina;
                    //float posicionY = pagina.Canvas.ClientSize.Height - altoImagen - 20;
                    //pagina.Canvas.DrawImage(imagenPiedepagina, 0, posicionY, anchoPagina, altoImagen);

                    // Actualizar el número de renglones mostrados
                    renglonesMostrados += renglonesEnEstaPagina;

                    // Si quedan más registros, crear una nueva página
                    if (renglonesMostrados < totalRenglones)
                    {

                        altoImagen = (imagenPiedepagina.PhysicalDimension.Height / imagenPiedepagina.PhysicalDimension.Width) * anchoPagina;

                        // Dibujar la imagen en la parte inferior de la página
                        float posicionY2 = pagina.Canvas.ClientSize.Height - altoImagen;
                        pagina.Canvas.DrawImage(imagenPiedepagina, 0, posicionY2 + 5, anchoPagina, altoImagen);
                        pagina = pdf.Pages.Add();
                        renglon = 30;
                    }
                    else
                    {
                        renglon = (int)resultadoTabla.Bounds.Bottom + 10;
                    }
                }



                // Agregar texto final después de la tabla



                //agregar el texto de la despedida debajo del grid 
                renglon = renglon + 10;
                pagina.Canvas.DrawString("Beneficios Tangibles", textoFontBold, brocha, 90, renglon, formatoDerecha);
                renglon = renglon + 10;
                pagina.Canvas.DrawString("Logro de estándares de limpieza: ", textoFontBold, brocha, 0, renglon, formatojustificado);
                pagina.Canvas.DrawString("Instalaciones limpias que de manera sostenida cumplan con todos los estándares de higiene.", fuenteTextofinal, brocha, 145, renglon, formatojustificado);
                renglon = renglon + 10;
                pagina.Canvas.DrawString("Costo óptimo:  ", textoFontBold, brocha, 0, renglon, formatojustificado);
                pagina.Canvas.DrawString("Reducción de costo mensual integral de la limpieza.", fuenteTextofinal, brocha, 67, renglon, formatojustificado);
                renglon = renglon + 20;
                pagina.Canvas.DrawString("Términos y Condiciones", textoFontBold, brocha, 0, renglon, formatojustificado);
                renglon = renglon + 10;
                pagina.Canvas.DrawString("* Vigencia de esta cotización es de " + dias + " días a partir de la fecha presentada.", fuenteTextofinal, brocha, 0, renglon, formatojustificado);
                renglon = renglon + 10;
                pagina.Canvas.DrawString("* Precios No Incluyen IVA", fuenteTextofinal, brocha, 0, renglon, formatojustificado);
                renglon = renglon + 10;
                pagina.Canvas.DrawString("* Entrega a domicilio", fuenteTextofinal, brocha, 0, renglon, formatojustificado);
                renglon = renglon + 10;
                pagina.Canvas.DrawString("* Precios entran en vigor a partir del " + fechainicio, fuenteTextofinal, brocha, 0, renglon, formatojustificado);
                renglon = renglon + 10;
                pagina.Canvas.DrawString("* Los precios convenidos se respetarán y cualquier cambio será comunicado al cliente con 15 días de anticipación.", fuenteTextofinal, brocha, 0, renglon, formatojustificado);
                renglon = renglon + 10;
                textoParrafo = "* En caso de que nuestra solución contemple la instalación de equipos dosificadores, éstos se ofrecerán en calidad de préstamo, para lo cual se firmará un Contrato de Comodato entre ambas partes.";
                formatoJustificado.Alignment = PdfTextAlignment.Justify;
                rectanguloTexto = new RectangleF(0, renglon, pagina.Canvas.ClientSize.Width - 20, 30);

                // Dibujar el párrafo en el PDF
                pagina.Canvas.DrawString(textoParrafo, fuenteTextofinal, brocha, rectanguloTexto, formatoJustificado);
                renglon = renglon + 20;
                pagina.Canvas.DrawString("* Se establece que las condiciones de crédito serán de " + diascredito + " días naturales a partir de la fecha de revisión de nuestra factura.", fuenteTextofinal, brocha, 0, renglon, formatojustificado);
                renglon = renglon + 10;
                pagina.Canvas.DrawString("* En caso de que haya demora en los pagos, Key Soluciones de Limpieza suspenderá el suministro de productos y servicios.", fuenteTextofinal, brocha, 0, renglon, formatojustificado);
                renglon = renglon + 10;
                pagina.Canvas.DrawString("* Para formalizar los términos, condiciones y alcance de nuestro servicio, se firmará un Convenio que garantice el cumplimiento.", fuenteTextofinal, brocha, 0, renglon, formatojustificado);

                //parrafo de agradecimiento
                renglon = renglon + 20;
                textoParrafo = "Agradeciendo la oportunidad para poder presentarle nuestra propuesta, me despido de usted esperando poder atenderle próximamente";
                //PdfTrueTypeFont fuenteTexto = new PdfTrueTypeFont(new Font("Arial", 10f), true);
                //PdfStringFormat formatoJustificado = new PdfStringFormat();
                formatoJustificado.Alignment = PdfTextAlignment.Justify;
                RectangleF agradecimiento = new RectangleF(0, renglon, paginapresentacion.Canvas.ClientSize.Width - 20, 200);

                // Dibujar el párrafo en el PDF nombre del rik y telefono
                pagina.Canvas.DrawString(textoParrafo, fuenteTexto, brocha, agradecimiento, formatoJustificado);
                renglon = renglon + 40;
                textoParrafo = nombrerepresentante + "  ,  " + telefonorik;
                formatoJustificado.Alignment = PdfTextAlignment.Justify;
                rectanguloTexto = new RectangleF(0, renglon, pagina.Canvas.ClientSize.Width, 30);

                // Dibujar el párrafo en el PDF
                pagina.Canvas.DrawString(textoParrafo, fuenteTextofinal, brocha, rectanguloTexto, formatoCentrado);



                // Obtener el ancho y alto de la página

                altoImagen = (imagenPiedepagina.PhysicalDimension.Height / imagenPiedepagina.PhysicalDimension.Width) * anchoPagina;

                // Dibujar la imagen en la parte inferior de la página
                float posicionY = pagina.Canvas.ClientSize.Height - altoImagen;
                pagina.Canvas.DrawImage(imagenPiedepagina, 0, posicionY + 5, anchoPagina, altoImagen);

                pdf.SaveToFile(rutaCompleta);
                // Cerrar el documento
                pdf.Close();

                Console.WriteLine($"PDF generado en: {rutaCompleta}");



                #endregion termina insertar datos en pdf 



                //pruebas de archivo 
                string rutaBase = AppDomain.CurrentDomain.BaseDirectory;
                string subCarpeta = "Reportes";
                string rutaCompleta2 = Path.Combine(rutaBase, subCarpeta, nombreArchivo);
                string archivoUrl = rutaCompleta2;
                //pruebas de archivo fin 

                Uri requestUrl = HttpContext.Current.Request.Url;
                string baseUrl = $"{requestUrl.Scheme}://{requestUrl.Host}:{requestUrl.Port}";
                string archivoUrl2 = $"{baseUrl}/Reportes/{nombreArchivo}";
                Console.WriteLine(baseUrl);
                Console.WriteLine(archivoUrl2);
                Console.WriteLine(archivoUrl);
                string urlBase = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath;
                Console.WriteLine("urlBase", urlBase);
                // Construir la URL completa del archivo
                string archivoUrl3 = $"{urlBase}/Reportes/{nombreArchivo}";
                Console.WriteLine(archivoUrl3);




                #region enviar correo 


                string correoUsuario = MySesion.U_Correo;

                // ENVIO DE CORREO 

                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Cd = MySesion.Id_Cd_Ver;
                configuracion.Id_Emp = MySesion.Id_Emp;
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                cn_configuracion.Consulta(ref configuracion, MySesion.Emp_Cnx);


                string s_temp_file = "";
                s_temp_file = HttpContext.Current.Server.MapPath("~/GestionPrecios/GestionIncrementoEmailTemplate.htm");
                string Tmp = string.Empty;
                StringBuilder SB = new StringBuilder();

                using (StreamReader reader = new StreamReader(s_temp_file))
                {
                    Tmp = reader.ReadToEnd();
                    SB.Append(Tmp);
                }
                //string LigaServidor = "http://40.84.229.61/siancentral/CL_Autorizaciones.aspx?";

                //SB.Replace("{AUT_PARAMESTROS}", LigaServidor + "SolFolio=" + SolFolio.ToString() + "&Id_Cd=" + Id_Cd.ToString() + "&TipoCompra=" + TipoCompra + "&TipoSolicitud=" + TipoSolicitud.ToString());
                SB.Replace("{TITULO}", "Aviso de Ajuste en Nuestros Precios");
                SB.Replace("{EMPRESA}", MySesion.Emp_Nombre);
                SB.Replace("{ATENCION}", contacto);
                SB.Replace("{CENTRO}", MySesion.Id_Cd.ToString() + " - " + MySesion.Cd_Nombre);
                SB.Replace("{REPRESENTANTE}", nombrerepresentante);
                SB.Replace("{TELEFONO}", telefonorik);
                SB.Replace("{FECHAINICIO}", fechainicio);
                //   SB.Replace("{CORREO_AUTORIZADOR}", LstAutorizadores[0].Responsable + " (" + LstAutorizadores[0].Correo + ")");
                string body = SB.ToString();

                AlternateView vistaHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);

                //this.RespaldoCorreo(hfNumSolicitudAbasto.Value, body, correo);
                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(2525));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                sm.EnableSsl = false;
                MailMessage m = new MailMessage();
                //  string[] eVirtual = configuracion.Mail_EVirtual.Split(',');
                m.From = new MailAddress(configuracion.Mail_Remitente);


                string rutaPDF = rutaCompleta; //  @"C:\ruta\del\archivo.pdf";
                Attachment adjunto = new Attachment(rutaPDF, MediaTypeNames.Application.Pdf);
                m.Attachments.Add(adjunto);
                //string correo = "ing.rborquez@gmail.com, raul.borquez@gibraltar.com.mx,dianela.morales@key.com.mx,servicios.informatica@gibraltar.com.mx";

                //this.CorreosAutorizadorxMotivoxApp(ref correo, TipoSolicitud, IdAplicacion);

                //descomentar para pruebas 
                //correo = "whylfredo.valero@gibraltar.com.mx,orlando.guzman@gibraltar.com.mx";
                //correo = "francisco.cepeda@gibraltar.com.mx";
                correo = correo.Replace(";", ",");
                string[] eVirtual2 = correo.Split(',');
                int reng = 1;

                reng = 0;

                foreach (string core in eVirtual2)
                {
                    if (core != " ")
                    {
                        if (reng == 0)
                        {
                            m.To.Add(new MailAddress(core));
                            reng = 1;
                        }
                        else
                        {
                            m.CC.Add(new MailAddress(core));
                        }
                    }
                }

                //m.Bcc.Add(new MailAddress("dianela.morales@key.com.mx"));
                // m.Bcc.Add(new MailAddress("francisco.cepeda@gibraltar.com.mx"));
                if (correo != correoUsuario)
                {
                    m.CC.Add(new MailAddress(correoUsuario));
                }


                m.Subject = "Propuesta económica para  " + nom_comercial;
                m.IsBodyHtml = true;
                try
                {
                    //LinkedResource logo = new LinkedResource(MapPath(@"Imagenes/logo.jpg"), MediaTypeNames.Image.Jpeg);
                    //logo.ContentId = "companylogo";
                    //vistaHtml.LinkedResources.Add(logo);
                }
                catch (Exception)
                {
                }
                m.AlternateViews.Add(vistaHtml);

                int CorreoEnviado = 0;
                try
                {
                    sm.Send(m);
                    CorreoEnviado = 1;

                }
                catch (Exception exx)
                {
                    CorreoEnviado = -1;
                }


                #endregion enviar correo


                return archivoUrl3;
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }



        //[WebMethod]
        //public static string GenerarPDF_2026AbrAnterior(List<Cliente> clientes, int dias, int id_cte, string nom_comercial, string representante, string nombrerepresentante, string telefonorik, string fechainicio, string correo)
        //{

        //    try
        //    {

        //        PdfDocument pdf = new PdfDocument();

        //        PdfPageBase paginapresentacion = pdf.Pages.Add();
        //        PdfFont tituloFont = new PdfFont(PdfFontFamily.Helvetica, 16f, PdfFontStyle.Bold);
        //        PdfFont textoFont = new PdfFont(PdfFontFamily.TimesRoman, 10f);
        //        PdfFont textoFontBold = new PdfFont(PdfFontFamily.TimesRoman, 10f, PdfFontStyle.Bold);
        //        PdfFont textoFontBoldPresentacion = new PdfFont(PdfFontFamily.TimesRoman, 12f, PdfFontStyle.Bold);
        //        PdfStringFormat formatoCentrado = new PdfStringFormat(PdfTextAlignment.Center);
        //        PdfStringFormat formatoDerecha = new PdfStringFormat(PdfTextAlignment.Right);
        //        PdfStringFormat formatoIzquierda = new PdfStringFormat(PdfTextAlignment.Left);
        //        PdfStringFormat formatojustificado = new PdfStringFormat(PdfTextAlignment.Justify);
        //        PdfBrush brocha = PdfBrushes.Black;


        //        string nombreArchivo = "Cotizacion" + id_cte + "-" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";
        //        string rutaRelativa = $"~/Reportes/{nombreArchivo}";
        //        string rutaArchivo = HttpContext.Current.Server.MapPath("~/Reportes/" + nombreArchivo);
        //        string rutaCompleta = HttpContext.Current.Server.MapPath(rutaRelativa);

        //        string rutaLogo = HttpContext.Current.Server.MapPath("~/Img/KeyActprecios.png");
        //        string rutaLogo2 = HttpContext.Current.Server.MapPath("~/Img/key_logo_reciente.png");
        //        string piedepagina = HttpContext.Current.Server.MapPath("~/Img/piedepagina.png");
        //        string rutapiedepagina = HttpContext.Current.Server.MapPath("~/Img/piedepagina.png");
        //        PdfImage imagenPiedepagina = PdfImage.FromFile(rutapiedepagina);

        //        string rutaImagen1 = HttpContext.Current.Server.MapPath("~/Img/aum2025.png");
        //        string rutaimagen2 = HttpContext.Current.Server.MapPath("~/Img/inegi.png");

        //        MySesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
        //        //if (MySesion == null)
        //        //{
        //        //    response.Message = "connection close";
        //        //    return response;
        //        //}


        //        string empresa = MySesion.Emp_Nombre;

        //        //que envie el correo al que recibe de la pagina
        //        //string correo = MySesion.U_Correo;

        //        //Cargar datos cliente 

        //        Clientes cte = new Clientes();
        //        cte.Id_Cte = id_cte;
        //        cte.Id_Emp = MySesion.Id_Emp;
        //        cte.Id_Cd = MySesion.Id_Cd_Ver;
        //        //cte.Id_Rik = txtRepresentante.Value.HasValue ? (int)txtRepresentante.Value.Value : (sesion.Id_Rik > 0 ? sesion.Id_Rik : 0);
        //        CN_CatCliente cnCliente = new CN_CatCliente();
        //        try
        //        {
        //            cnCliente.ConsultaClientes(ref cte, MySesion.Emp_Cnx);
        //        }
        //        catch (Exception ex)
        //        {


        //        }

        //        string nombrecom = cte.Cte_NomComercial;
        //        string calle = cte.Cte_FacCalle;
        //        string colonia = cte.Cte_FacColonia;

        //        string contacto = cte.Cte_Contacto;
        //        //txtPuesto.Text = cte.ct;
        //        string telefono_cliente = cte.Cte_Telefono;
        //        int diascredito = cte.Cte_CondPago;
        //        //que envie el correo al que recibe de la pagina
        //        //correo = cte.Cte_Email;
        //        #region Inicia a insertar datos en pdf 1
        //        // Crear hoja




        //        if (File.Exists(rutaLogo))
        //        {
        //            PdfImage imagenpresentacion = PdfImage.FromFile(rutaLogo);
        //            paginapresentacion.Canvas.DrawImage(imagenpresentacion, new RectangleF(0, 0, 580, 70));
        //        }


        //        // Obtener el ancho y alto de la página
        //        float anchoPagina1 = paginapresentacion.Canvas.ClientSize.Width;
        //        float altoImagen1 = (imagenPiedepagina.PhysicalDimension.Height / imagenPiedepagina.PhysicalDimension.Width) * anchoPagina1;

        //        // Dibujar la imagen en la parte inferior de la página
        //        float posicionY1 = paginapresentacion.Canvas.ClientSize.Height - altoImagen1;
        //        paginapresentacion.Canvas.DrawImage(imagenPiedepagina, 0, posicionY1, anchoPagina1, altoImagen1);

        //        string titulo = "Reporte de Clientes";




        //        // Crear cultura española
        //        CultureInfo cultura = new CultureInfo("es-MX");


        //        // Formatear la fecha 
        //        string fechaFormateada = DateTime.Now.ToString("dd 'de' MMMM 'de' yyyy", cultura);


        //        int renglon1 = 100;
        //        //paginapresentacion.Canvas.DrawString($"Fecha: {DateTime.Now:dd/MM/yyyy}", textoFontBold, brocha, 510, renglon1, formatoDerecha);
        //        paginapresentacion.Canvas.DrawString($"        {fechaFormateada}", textoFontBold, brocha, 480, renglon1, formatoDerecha);

        //        renglon1 = renglon1 + 30;
        //        paginapresentacion.Canvas.DrawString("Estimado cliente", textoFontBoldPresentacion, brocha, 84, renglon1, formatoDerecha);

        //        renglon1 = renglon1 + 70; //20

        //        string textoParrafo = "Siendo consistentes con nuestro compromiso de servirles con los más altos estándares,\r\nconsideramos necesario informarles que durante el año 2025, nuestra industria ha sido impactada\r\npor incrementos en Materias Primas, Mano de Obra, Gastos de Fabricación, Empaques y gastos\r\nlogísticos. ";

        //        PdfTrueTypeFont fuenteTexto = new PdfTrueTypeFont(new Font("Arial", 10f), true);
        //        PdfTrueTypeFont fuenteTextofinal = new PdfTrueTypeFont(new Font("Arial", 9f), true);
        //        PdfStringFormat formatoJustificado = new PdfStringFormat();
        //        formatoJustificado.Alignment = PdfTextAlignment.Justify;
        //        RectangleF rectanguloTexto1 = new RectangleF(0, renglon1, paginapresentacion.Canvas.ClientSize.Width - 20, 240);//200

        //        // Dibujar el párrafo en el PDF
        //        paginapresentacion.Canvas.DrawString(textoParrafo, fuenteTexto, brocha, rectanguloTexto1, formatoJustificado);

        //        //parrafo 2   
        //        renglon1 = renglon1 + 90;//20
        //        textoParrafo = "Estos sucesos, nos obligan a presentar incrementos en nuestros productos que rondara en promedio el 5.6%. ";
        //        RectangleF rectanguloTexto2 = new RectangleF(0, renglon1, paginapresentacion.Canvas.ClientSize.Width - 20, 200);//220
        //        paginapresentacion.Canvas.DrawString(textoParrafo, fuenteTexto, brocha, rectanguloTexto2, formatoJustificado);

        //        renglon1 = renglon1 + 70;//40
        //        textoParrafo = "Por nuestra parte, seguimos enfocados en buscar y generar alternativas que ayuden a minimizar el\r\nimpacto económico, esto aunado a un servicio de excelencia por nuestros expertos y brindando\r\nsoluciones que se ajusten a las necesidades de nuestros clientes.";
        //        RectangleF rectanguloTexto3a = new RectangleF(0, renglon1, paginapresentacion.Canvas.ClientSize.Width - 20, 240);//220
        //        paginapresentacion.Canvas.DrawString(textoParrafo, fuenteTexto, brocha, rectanguloTexto3a, formatoJustificado);

        //        renglon1 = renglon1 + 80;//50


        //        float anchoPagina = paginapresentacion.Canvas.ClientSize.Width;
        //        float altoImagen;
        //        altoImagen =  140; // Alto de las imágenes
        //        float anchoColumna = anchoPagina; // / 2; // Dividir en dos columnas


        //        renglon1 = renglon1 + 20; 

        //        textoParrafo = "Será un placer seguir colaborando en sus procesos de limpieza, mantenimiento y desinfección\r\ntratando de llevarle las mejores propuestas y soluciones para su negocio.";
        //        RectangleF rectanguloTexto3 = new RectangleF(0, renglon1, paginapresentacion.Canvas.ClientSize.Width - 20, 60);
        //        paginapresentacion.Canvas.DrawString(textoParrafo, fuenteTexto, brocha, rectanguloTexto3, formatoJustificado);

        //        renglon1 = renglon1 + 80;



        //        titulo = "ATENTAMENTE";
        //        paginapresentacion.Canvas.DrawString(titulo, fuenteTexto, brocha, 0, renglon1, formatojustificado);

        //        renglon1 = renglon1 + 40;
        //        titulo = "Equipo Comercial Key Química";
        //        paginapresentacion.Canvas.DrawString(titulo, fuenteTexto, brocha, 0, renglon1, formatojustificado);



        //        #endregion pagina 1 

        //        #region Inicia a insertar datos en pdf pagina2
        //        PdfPageBase pagina = pdf.Pages.Add();


        //        if (File.Exists(rutaLogo2))
        //        {
        //            //var imagen = hoja.Drawings.AddPicture("Logo2", new FileInfo(rutaLogo2));
        //            //imagen.SetPosition(0, 0, 0, 0); // Posicionar en la esquina superior izquierda
        //            //                                //imagen.SetSize(100, 50); // Ajustar tamaño
        //            PdfImage imagen = PdfImage.FromFile(rutaLogo2);
        //            pagina.Canvas.DrawImage(imagen, new RectangleF(0, 0, 140, 80));


        //        }



        //        pagina.Canvas.DrawString($"Fecha: {DateTime.Now:dd/MM/yyyy}", textoFontBold, brocha, 510, 10, formatoDerecha);
        //        pagina.Canvas.DrawString("Key Química SA de CV", textoFontBold, brocha, 510, 25, formatoDerecha);
        //        pagina.Canvas.DrawString("KQU6911016X5", textoFontBold, brocha, 510, 40, formatoDerecha);
        //        PdfStringFormat formatoaDerecha = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);


        //        titulo = "PROPUESTA ECONÓMICA";
        //        pagina.Canvas.DrawString(titulo, textoFontBold, brocha, 10, 80, formatojustificado);

        //        pagina.Canvas.DrawString("Empresa: ", textoFontBold, brocha, 10, 100, formatojustificado);
        //        pagina.Canvas.DrawString("Atención: ", textoFontBold, brocha, 10, 110, formatojustificado);

        //        pagina.Canvas.DrawString(nom_comercial, textoFont, brocha, 110, 100, formatoIzquierda);
        //        pagina.Canvas.DrawString(contacto, textoFont, brocha, 110, 110, formatoIzquierda);


        //        //parrafo

        //        textoParrafo = "Key es una empresa 100% mexicana con más de 50 años de experiencia en la industria, lo que nos ha dado el conocimiento para "
        //           + "entender que en la actualidad los clientes son más demandantes con la limpieza y con espacios más limpios y sanos, por lo que "
        //           + "nos hemos especializado en no solo ofrecer químicos y accesorios, sino en desarrollar un programa completo de higiene, "
        //           + "personalizado, según las necesidades de cada cliente, que les ayude a cumplir con altos estándares de limpieza de manera simple y a "
        //           + "un costo óptimo. ";


        //        int renglon = 140;
        //        formatoJustificado.Alignment = PdfTextAlignment.Justify;
        //        RectangleF rectanguloTexto = new RectangleF(10, renglon, pagina.Canvas.ClientSize.Width - 20, 100);

        //        // Dibujar el párrafo en el PDF
        //        pagina.Canvas.DrawString(textoParrafo, fuenteTexto, brocha, rectanguloTexto, formatoJustificado);


        //        titulo = "Esperando podamos colaborar e integrar un equipo, nos reiteramos a sus órdenes. ";
        //        pagina.Canvas.DrawString(titulo, fuenteTexto, brocha, 10, 200, formatojustificado);



        //        PdfTable tablaPDF = new PdfTable
        //        {
        //            Style =
        //            {
        //                ShowHeader = true,
        //                HeaderStyle = { Font = new PdfFont(PdfFontFamily.Helvetica, 10f, PdfFontStyle.Bold), BackgroundBrush = PdfBrushes.LightGray },
        //                DefaultStyle = { Font = fuenteTexto, TextBrush = brocha }
        //            }
        //        };

        //        tablaPDF.Style.HeaderSource = PdfHeaderSource.ColumnCaptions;
        //        tablaPDF.Style.DefaultStyle.Font = new PdfTrueTypeFont(new Font("Arial", 8f), true);

        //        var tablaDatos = new DataTable();
        //        tablaDatos.Columns.Add("Producto");
        //        tablaDatos.Columns.Add("Descripción");
        //        tablaDatos.Columns.Add("Precio");

        //        renglon = 230;
        //        int iclientes = 0;
        //        foreach (var cliente in clientes)
        //        {
        //            renglon = renglon + 10;
        //            iclientes++;
        //            tablaDatos.Rows.Add(cliente.Id_Prd, cliente.NomProducto, cliente.PrecioNegociadoProy.ToString("N2", CultureInfo.CreateSpecificCulture("es-MX")));
        //        }


        //        tablaPDF.DataSource = tablaDatos;


        //        tablaPDF.Columns[0].Width = 70;
        //        tablaPDF.Columns[1].Width = 280;
        //        tablaPDF.Columns[2].Width = 70;
        //        tablaPDF.Columns[2].StringFormat = formatoaDerecha;

        //        //PdfLayoutResult resultadoTabla = tablaPDF.Draw(pagina, new PointF(20, 220));

        //        //as lo tenia en mi c´digo 27ene2025
        //        //tablaPDF.Draw(pagina, new PointF(20, 220));

        //        float altoDisponible = pagina.Canvas.ClientSize.Height - 200; // Espacio disponible
        //                                                                      //while (resultadoTabla.Bounds.Bottom + 30 > altoDisponible)
        //                                                                      //{
        //                                                                      //    pagina = pdf.Pages.Add(); // Agregar nueva página
        //                                                                      //    renglon = 20; // Reiniciar renglón en la nueva página
        //                                                                      //    resultadoTabla = tablaPDF.Draw(pagina, new PointF(20, renglon));
        //                                                                      //    renglon = 20 + (iclientes*10);
        //                                                                      //}
        //                                                                      // Variables de control
        //        int totalRenglones = tablaDatos.Rows.Count;
        //        int renglonesPorPagina = 25;
        //        int renglonesMostrados = 0;
        //        renglon = 220;

        //        while (renglonesMostrados < totalRenglones)
        //        {
        //            // Definir cuántos renglones mostrar en esta página
        //            int renglonesEnEstaPagina = renglonesMostrados == 0 ? 25 : 70;
        //            renglonesEnEstaPagina = Math.Min(renglonesEnEstaPagina, totalRenglones - renglonesMostrados);

        //            // Extraer los datos de la tabla para esta página
        //            DataTable tablaPagina = tablaDatos.Clone();
        //            for (int i = renglonesMostrados; i < renglonesMostrados + renglonesEnEstaPagina; i++)
        //            {
        //                tablaPagina.ImportRow(tablaDatos.Rows[i]);
        //            }

        //            // Dibujar la tabla
        //            PdfTable tablaTemp = new PdfTable { DataSource = tablaPagina };
        //            tablaTemp.Style = tablaPDF.Style;

        //            tablaTemp.Columns[0].Width = 70;
        //            tablaTemp.Columns[1].Width = 260;
        //            tablaTemp.Columns[2].Width = 70;
        //            tablaTemp.Columns[2].StringFormat = formatoaDerecha;

        //            PdfLayoutResult resultadoTabla = tablaTemp.Draw(pagina, new PointF(20, renglon));

        //            // Agregar pie de página con imagen
        //            //string rutaImagen = "footer_image.jpg";
        //            //PdfImage imagenPiedepagina = PdfImage.FromFile(rutaImagen);
        //            //float anchoPagina = pagina.Canvas.ClientSize.Width;
        //            //float altoImagen = (imagenPiedepagina.PhysicalDimension.Height / imagenPiedepagina.PhysicalDimension.Width) * anchoPagina;
        //            //float posicionY = pagina.Canvas.ClientSize.Height - altoImagen - 20;
        //            //pagina.Canvas.DrawImage(imagenPiedepagina, 0, posicionY, anchoPagina, altoImagen);

        //            // Actualizar el número de renglones mostrados
        //            renglonesMostrados += renglonesEnEstaPagina;

        //            // Si quedan más registros, crear una nueva página
        //            if (renglonesMostrados < totalRenglones)
        //            {

        //                altoImagen = (imagenPiedepagina.PhysicalDimension.Height / imagenPiedepagina.PhysicalDimension.Width) * anchoPagina;

        //                // Dibujar la imagen en la parte inferior de la página
        //                float posicionY2 = pagina.Canvas.ClientSize.Height - altoImagen;
        //                pagina.Canvas.DrawImage(imagenPiedepagina, 0, posicionY2 + 5, anchoPagina, altoImagen);
        //                pagina = pdf.Pages.Add();
        //                renglon = 30;
        //            }
        //            else
        //            {
        //                renglon = (int)resultadoTabla.Bounds.Bottom + 10;
        //            }
        //        }



        //        // Agregar texto final después de la tabla



        //        //agregar el texto de la despedida debajo del grid 
        //        renglon = renglon + 10;
        //        pagina.Canvas.DrawString("Beneficios Tangibles", textoFontBold, brocha, 90, renglon, formatoDerecha);
        //        renglon = renglon + 10;
        //        pagina.Canvas.DrawString("Logro de estándares de limpieza: ", textoFontBold, brocha, 0, renglon, formatojustificado);
        //        pagina.Canvas.DrawString("Instalaciones limpias que de manera sostenida cumplan con todos los estándares de higiene.", fuenteTextofinal, brocha, 145, renglon, formatojustificado);
        //        renglon = renglon + 10;
        //        pagina.Canvas.DrawString("Costo óptimo:  ", textoFontBold, brocha, 0, renglon, formatojustificado);
        //        pagina.Canvas.DrawString("Reducción de costo mensual integral de la limpieza.", fuenteTextofinal, brocha, 67, renglon, formatojustificado);
        //        renglon = renglon + 20;
        //        pagina.Canvas.DrawString("Términos y Condiciones", textoFontBold, brocha, 0, renglon, formatojustificado);
        //        renglon = renglon + 10;
        //        pagina.Canvas.DrawString("* Vigencia de esta cotización es de " + dias + " días a partir de la fecha presentada.", fuenteTextofinal, brocha, 0, renglon, formatojustificado);
        //        renglon = renglon + 10;
        //        pagina.Canvas.DrawString("* Precios No Incluyen IVA", fuenteTextofinal, brocha, 0, renglon, formatojustificado);
        //        renglon = renglon + 10;
        //        pagina.Canvas.DrawString("* Entrega a domicilio", fuenteTextofinal, brocha, 0, renglon, formatojustificado);
        //        renglon = renglon + 10;
        //        pagina.Canvas.DrawString("* Precios entran en vigor a partir del " + fechainicio, fuenteTextofinal, brocha, 0, renglon, formatojustificado);
        //        renglon = renglon + 10;
        //        pagina.Canvas.DrawString("* Los precios convenidos se respetarán y cualquier cambio será comunicado al cliente con 15 días de anticipación.", fuenteTextofinal, brocha, 0, renglon, formatojustificado);
        //        renglon = renglon + 10;
        //        textoParrafo = "* En caso de que nuestra solución contemple la instalación de equipos dosificadores, éstos se ofrecerán en calidad de préstamo, para lo cual se firmará un Contrato de Comodato entre ambas partes.";
        //        formatoJustificado.Alignment = PdfTextAlignment.Justify;
        //        rectanguloTexto = new RectangleF(0, renglon, pagina.Canvas.ClientSize.Width - 20, 30);

        //        // Dibujar el párrafo en el PDF
        //        pagina.Canvas.DrawString(textoParrafo, fuenteTextofinal, brocha, rectanguloTexto, formatoJustificado);
        //        renglon = renglon + 20;
        //        pagina.Canvas.DrawString("* Se establece que las condiciones de crédito serán de " + diascredito + " días naturales a partir de la fecha de revisión de nuestra factura.", fuenteTextofinal, brocha, 0, renglon, formatojustificado);
        //        renglon = renglon + 10;
        //        pagina.Canvas.DrawString("* En caso de que haya demora en los pagos, Key Soluciones de Limpieza suspenderá el suministro de productos y servicios.", fuenteTextofinal, brocha, 0, renglon, formatojustificado);
        //        renglon = renglon + 10;
        //        pagina.Canvas.DrawString("* Para formalizar los términos, condiciones y alcance de nuestro servicio, se firmará un Convenio que garantice el cumplimiento.", fuenteTextofinal, brocha, 0, renglon, formatojustificado);

        //        //parrafo de agradecimiento
        //        renglon = renglon + 20;
        //        textoParrafo = "Agradeciendo la oportunidad para poder presentarle nuestra propuesta, me despido de usted esperando poder atenderle próximamente";
        //        //PdfTrueTypeFont fuenteTexto = new PdfTrueTypeFont(new Font("Arial", 10f), true);
        //        //PdfStringFormat formatoJustificado = new PdfStringFormat();
        //        formatoJustificado.Alignment = PdfTextAlignment.Justify;
        //        RectangleF agradecimiento = new RectangleF(0, renglon, paginapresentacion.Canvas.ClientSize.Width - 20, 200);

        //        // Dibujar el párrafo en el PDF nombre del rik y telefono
        //        pagina.Canvas.DrawString(textoParrafo, fuenteTexto, brocha, agradecimiento, formatoJustificado);
        //        renglon = renglon + 40;
        //        textoParrafo = nombrerepresentante + "  ,  " + telefonorik;
        //        formatoJustificado.Alignment = PdfTextAlignment.Justify;
        //        rectanguloTexto = new RectangleF(0, renglon, pagina.Canvas.ClientSize.Width, 30);

        //        // Dibujar el párrafo en el PDF
        //        pagina.Canvas.DrawString(textoParrafo, fuenteTextofinal, brocha, rectanguloTexto, formatoCentrado);



        //        // Obtener el ancho y alto de la página

        //        altoImagen = (imagenPiedepagina.PhysicalDimension.Height / imagenPiedepagina.PhysicalDimension.Width) * anchoPagina;

        //        // Dibujar la imagen en la parte inferior de la página
        //        float posicionY = pagina.Canvas.ClientSize.Height - altoImagen;
        //        pagina.Canvas.DrawImage(imagenPiedepagina, 0, posicionY + 5, anchoPagina, altoImagen);

        //        pdf.SaveToFile(rutaCompleta);
        //        // Cerrar el documento
        //        pdf.Close();

        //        Console.WriteLine($"PDF generado en: {rutaCompleta}");



        //        #endregion termina insertar datos en pdf 



        //        //pruebas de archivo 
        //        string rutaBase = AppDomain.CurrentDomain.BaseDirectory;
        //        string subCarpeta = "Reportes";
        //        string rutaCompleta2 = Path.Combine(rutaBase, subCarpeta, nombreArchivo);
        //        string archivoUrl = rutaCompleta2;
        //        //pruebas de archivo fin 

        //        Uri requestUrl = HttpContext.Current.Request.Url;
        //        string baseUrl = $"{requestUrl.Scheme}://{requestUrl.Host}:{requestUrl.Port}";
        //        string archivoUrl2 = $"{baseUrl}/Reportes/{nombreArchivo}";
        //        Console.WriteLine(baseUrl);
        //        Console.WriteLine(archivoUrl2);
        //        Console.WriteLine(archivoUrl);
        //        string urlBase = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath;
        //        Console.WriteLine("urlBase", urlBase);
        //        // Construir la URL completa del archivo
        //        string archivoUrl3 = $"{urlBase}/Reportes/{nombreArchivo}";
        //        Console.WriteLine(archivoUrl3);




        //        #region enviar correo 


        //        string correoUsuario = MySesion.U_Correo;

        //        // ENVIO DE CORREO 

        //        ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
        //        configuracion.Id_Cd = MySesion.Id_Cd_Ver;
        //        configuracion.Id_Emp = MySesion.Id_Emp;
        //        CN_Configuracion cn_configuracion = new CN_Configuracion();
        //        cn_configuracion.Consulta(ref configuracion, MySesion.Emp_Cnx);


        //        string s_temp_file = "";
        //        s_temp_file = HttpContext.Current.Server.MapPath("~/GestionPrecios/GestionIncrementoEmailTemplate.htm");
        //        string Tmp = string.Empty;
        //        StringBuilder SB = new StringBuilder();

        //        using (StreamReader reader = new StreamReader(s_temp_file))
        //        {
        //            Tmp = reader.ReadToEnd();
        //            SB.Append(Tmp);
        //        }
        //        //string LigaServidor = "http://40.84.229.61/siancentral/CL_Autorizaciones.aspx?";

        //        //SB.Replace("{AUT_PARAMESTROS}", LigaServidor + "SolFolio=" + SolFolio.ToString() + "&Id_Cd=" + Id_Cd.ToString() + "&TipoCompra=" + TipoCompra + "&TipoSolicitud=" + TipoSolicitud.ToString());
        //        SB.Replace("{TITULO}", "Aviso de Ajuste en Nuestros Precios");
        //        SB.Replace("{EMPRESA}", MySesion.Emp_Nombre);
        //        SB.Replace("{ATENCION}", contacto);
        //        SB.Replace("{CENTRO}", MySesion.Id_Cd.ToString() + " - " + MySesion.Cd_Nombre);
        //        SB.Replace("{REPRESENTANTE}", nombrerepresentante);
        //        SB.Replace("{TELEFONO}", telefonorik);
        //        SB.Replace("{FECHAINICIO}", fechainicio);
        //        //   SB.Replace("{CORREO_AUTORIZADOR}", LstAutorizadores[0].Responsable + " (" + LstAutorizadores[0].Correo + ")");
        //        string body = SB.ToString();

        //        AlternateView vistaHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);

        //        //this.RespaldoCorreo(hfNumSolicitudAbasto.Value, body, correo);
        //        SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(2525));
        //        sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
        //        sm.EnableSsl = false;
        //        MailMessage m = new MailMessage();
        //        //  string[] eVirtual = configuracion.Mail_EVirtual.Split(',');
        //        m.From = new MailAddress(configuracion.Mail_Remitente);


        //        string rutaPDF = rutaCompleta; //  @"C:\ruta\del\archivo.pdf";
        //        Attachment adjunto = new Attachment(rutaPDF, MediaTypeNames.Application.Pdf);
        //        m.Attachments.Add(adjunto);
        //        //string correo = "ing.rborquez@gmail.com, raul.borquez@gibraltar.com.mx,dianela.morales@key.com.mx,servicios.informatica@gibraltar.com.mx";

        //        //this.CorreosAutorizadorxMotivoxApp(ref correo, TipoSolicitud, IdAplicacion);

        //        //descomentar para pruebas 
        //        //correo = "whylfredo.valero@gibraltar.com.mx,orlando.guzman@gibraltar.com.mx";
        //        //correo = "francisco.cepeda@gibraltar.com.mx";
        //        correo = correo.Replace(";", ",");
        //        string[] eVirtual2 = correo.Split(',');
        //        int reng = 1;

        //        reng = 0;

        //        foreach (string core in eVirtual2)
        //        {
        //            if (core != " ")
        //            {
        //                if (reng == 0)
        //                {
        //                    m.To.Add(new MailAddress(core));
        //                    reng = 1;
        //                }
        //                else
        //                {
        //                    m.CC.Add(new MailAddress(core));
        //                }
        //            }
        //        }

        //        //m.Bcc.Add(new MailAddress("dianela.morales@key.com.mx"));
        //        // m.Bcc.Add(new MailAddress("francisco.cepeda@gibraltar.com.mx"));
        //        if (correo != correoUsuario)
        //        {
        //            m.CC.Add(new MailAddress(correoUsuario));
        //        }


        //        m.Subject = "Propuesta económica para  " + nom_comercial;
        //        m.IsBodyHtml = true;
        //        try
        //        {
        //            //LinkedResource logo = new LinkedResource(MapPath(@"Imagenes/logo.jpg"), MediaTypeNames.Image.Jpeg);
        //            //logo.ContentId = "companylogo";
        //            //vistaHtml.LinkedResources.Add(logo);
        //        }
        //        catch (Exception)
        //        {
        //        }
        //        m.AlternateViews.Add(vistaHtml);

        //        int CorreoEnviado = 0;
        //        try
        //        {
        //            sm.Send(m);
        //            CorreoEnviado = 1;

        //        }
        //        catch (Exception exx)
        //        {
        //            CorreoEnviado = -1;
        //        }


        //        #endregion enviar correo


        //        return archivoUrl3;
        //    }
        //    catch (Exception ex)
        //    {
        //        return $"Error: {ex.Message}";
        //    }
        //}


        //[WebMethod]
        //public static string GenerarPDF_V2025(List<Cliente> clientes, int dias, int id_cte, string nom_comercial, string representante, string nombrerepresentante, string telefonorik, string fechainicio,string correo)
        //{
        //    //esta versión es entes del 5 de noviembre del 2025 

        //    try
        //    {

        //        PdfDocument pdf = new PdfDocument();

        //        PdfPageBase paginapresentacion = pdf.Pages.Add();
        //        PdfFont tituloFont = new PdfFont(PdfFontFamily.Helvetica, 16f, PdfFontStyle.Bold);
        //        PdfFont textoFont = new PdfFont(PdfFontFamily.TimesRoman, 10f);
        //        PdfFont textoFontBold = new PdfFont(PdfFontFamily.TimesRoman, 10f, PdfFontStyle.Bold);
        //        PdfStringFormat formatoCentrado = new PdfStringFormat(PdfTextAlignment.Center);
        //        PdfStringFormat formatoDerecha = new PdfStringFormat(PdfTextAlignment.Right);
        //        PdfStringFormat formatoIzquierda = new PdfStringFormat(PdfTextAlignment.Left);
        //        PdfStringFormat formatojustificado = new PdfStringFormat(PdfTextAlignment.Justify);
        //        PdfBrush brocha = PdfBrushes.Black;


        //        string nombreArchivo = "Cotizacion" + id_cte + "-" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";
        //        string rutaRelativa = $"~/Reportes/{nombreArchivo}";
        //        string rutaArchivo = HttpContext.Current.Server.MapPath("~/Reportes/" + nombreArchivo);
        //        string rutaCompleta = HttpContext.Current.Server.MapPath(rutaRelativa);

        //        string rutaLogo = HttpContext.Current.Server.MapPath("~/Img/KeyActprecios.png");
        //        string rutaLogo2 = HttpContext.Current.Server.MapPath("~/Img/key_logo_reciente.png");
        //        string piedepagina = HttpContext.Current.Server.MapPath("~/Img/piedepagina.png");
        //        string rutapiedepagina = HttpContext.Current.Server.MapPath("~/Img/piedepagina.png");
        //        PdfImage imagenPiedepagina = PdfImage.FromFile(rutapiedepagina);

        //        string rutaImagen1 = HttpContext.Current.Server.MapPath("~/Img/aum2025.png");
        //        string rutaimagen2 = HttpContext.Current.Server.MapPath("~/Img/inegi.png");

        //        MySesion = (Sesion)HttpContext.Current.Session["Sesion" + HttpContext.Current.Session.SessionID];
        //        //if (MySesion == null)
        //        //{
        //        //    response.Message = "connection close";
        //        //    return response;
        //        //}


        //        string empresa = MySesion.Emp_Nombre;

        //        //que envie el correo al que recibe de la pagina
        //        //string correo = MySesion.U_Correo;

        //        //Cargar datos cliente 

        //        Clientes cte = new Clientes();
        //        cte.Id_Cte = id_cte;
        //        cte.Id_Emp = MySesion.Id_Emp;
        //        cte.Id_Cd = MySesion.Id_Cd_Ver;
        //        //cte.Id_Rik = txtRepresentante.Value.HasValue ? (int)txtRepresentante.Value.Value : (sesion.Id_Rik > 0 ? sesion.Id_Rik : 0);
        //        CN_CatCliente cnCliente = new CN_CatCliente();
        //        try
        //        {
        //            cnCliente.ConsultaClientes(ref cte, MySesion.Emp_Cnx);
        //        }
        //        catch (Exception ex)
        //        {


        //        }

        //        string nombrecom = cte.Cte_NomComercial;
        //        string calle = cte.Cte_FacCalle;
        //        string colonia = cte.Cte_FacColonia;

        //        string contacto = cte.Cte_Contacto;
        //        //txtPuesto.Text = cte.ct;
        //        string telefono_cliente = cte.Cte_Telefono;
        //        int diascredito = cte.Cte_CondPago;
        //        //que envie el correo al que recibe de la pagina
        //        //correo = cte.Cte_Email;
        //        #region Inicia a insertar datos en pdf 1
        //        // Crear hoja




        //        if (File.Exists(rutaLogo))
        //        {
        //            PdfImage imagenpresentacion = PdfImage.FromFile(rutaLogo);
        //            paginapresentacion.Canvas.DrawImage(imagenpresentacion, new RectangleF(0, 0, 550, 70));
        //        }


        //        // Obtener el ancho y alto de la página
        //        float anchoPagina1 = paginapresentacion.Canvas.ClientSize.Width;
        //        float altoImagen1 = (imagenPiedepagina.PhysicalDimension.Height / imagenPiedepagina.PhysicalDimension.Width) * anchoPagina1;

        //        // Dibujar la imagen en la parte inferior de la página
        //        float posicionY1 = paginapresentacion.Canvas.ClientSize.Height - altoImagen1;
        //        paginapresentacion.Canvas.DrawImage(imagenPiedepagina, 0, posicionY1, anchoPagina1, altoImagen1);

        //        string titulo = "Reporte de Clientes";
        //        //pagina.Canvas.DrawString(titulo, textoFont, PdfBrushes.Black, pagina.Canvas.ClientSize.Width / 2, 20, formatoCentrado);
        //        int renglon1 = 100;
        //        paginapresentacion.Canvas.DrawString($"Fecha: {DateTime.Now:dd/MM/yyyy}", textoFontBold, brocha, 510, renglon1, formatoDerecha);
        //        renglon1 = renglon1 + 30;
        //        paginapresentacion.Canvas.DrawString("A quien corresponda:", textoFontBold, brocha, 94, renglon1, formatoDerecha);

        //        renglon1 = renglon1 + 20;

        //        string textoParrafo = "En Key Química, mantenemos nuestro compromiso con la calidad, la transparencia y la mejora continua en los productos y servicios que ofrecemos. En este sentido, informamos que, a partir del 01 de agosto de 2025. ";

        //        PdfTrueTypeFont fuenteTexto = new PdfTrueTypeFont(new Font("Arial", 10f), true);
        //        PdfTrueTypeFont fuenteTextofinal = new PdfTrueTypeFont(new Font("Arial", 9f), true);
        //        PdfStringFormat formatoJustificado = new PdfStringFormat();
        //        formatoJustificado.Alignment = PdfTextAlignment.Justify;
        //        RectangleF rectanguloTexto1 = new RectangleF(0, renglon1, paginapresentacion.Canvas.ClientSize.Width - 20, 200);

        //        // Dibujar el párrafo en el PDF
        //        paginapresentacion.Canvas.DrawString(textoParrafo, fuenteTexto, brocha, rectanguloTexto1, formatoJustificado);

        //        //parrafo 2 estos cambios 
        //        renglon1 = renglon1 + 30;
        //        textoParrafo = "Durante el mes de mayo del presente año, la industria química nacional enfrentó un incremento considerable en los costos de materias primas esenciales, particularmente en la sosa cáustica líquida, insumo clave en la formulación de productos químicos de limpieza. ";
        //        RectangleF rectanguloTexto2 = new RectangleF(0, renglon1, paginapresentacion.Canvas.ClientSize.Width - 20, 220);
        //        paginapresentacion.Canvas.DrawString(textoParrafo, fuenteTexto, brocha, rectanguloTexto2, formatoJustificado);

        //        renglon1 = renglon1 + 40;
        //        textoParrafo = "Este aumento se debe principalmente a la resolución publicada en el Diario Oficial de la Federación (DOF) el 29 de mayo de 2025, mediante la cual se mantiene la cuota compensatoria definitiva a las importaciones de sosa cáustica líquida originarias de los Estados Unidos de América, bajo la fracción arancelaria 2815.12.01 ";
        //        RectangleF rectanguloTexto3a = new RectangleF(0, renglon1, paginapresentacion.Canvas.ClientSize.Width - 20, 220);
        //        paginapresentacion.Canvas.DrawString(textoParrafo, fuenteTexto, brocha, rectanguloTexto3a, formatoJustificado);

        //        renglon1 = renglon1 + 50;


        //        float anchoPagina = paginapresentacion.Canvas.ClientSize.Width;
        //        float altoImagen = 140; // Alto de las imágenes
        //        float anchoColumna = anchoPagina; // / 2; // Dividir en dos columnas

        //        if (File.Exists(rutaImagen1))
        //        {
        //            PdfImage imagenH1 = PdfImage.FromFile(rutaImagen1);
        //            //paginapresentacion.Canvas.DrawImage(imagenH1, new RectangleF(10, renglon1, 100, 50));
        //            paginapresentacion.Canvas.DrawImage(imagenH1, 0, renglon1, anchoColumna, altoImagen);


        //        }
        //        //if (File.Exists(rutaImagen1))
        //        //{
        //        //    PdfImage imagenH2 = PdfImage.FromFile(rutaImagen1);
        //        //    //paginapresentacion.Canvas.DrawImage(imagenH2, new RectangleF(250, renglon1, 100, 50));
        //        //    paginapresentacion.Canvas.DrawImage(imagenH2, anchoColumna, renglon1, anchoColumna, altoImagen);
        //        //}
        //        renglon1 = renglon1 + 150;
        //        //primer liga 
        //        string url1 = "https://dof.gob.mx/nota_detalle.php?codigo=5758617&fecha=29/05/2025&print=true ";
        //         PdfFont fuente = new PdfFont(PdfFontFamily.Helvetica, 10f);
        //        PdfBrush pincel = PdfBrushes.Blue;
        //        //PointF posicionTexto1 = new PointF(10, renglon1);
        //        //paginapresentacion.Canvas.DrawString(texto1, fuente, pincel, posicionTexto1);
        //        //RectangleF areaEnlace1 = new RectangleF(posicionTexto1, fuente.MeasureString(texto1));
        //        //PdfUriAnnotation enlace1 = new PdfUriAnnotation(areaEnlace1) { Uri = url1 };
        //        //paginapresentacion.AnnotationsWidget.Add(enlace1);


        //        float inicioX = 0; // Inicia en la columna 0
        //        float inicioY = renglon1; // Inicia en el renglón 200

        //        RectangleF areaTexto1 = new RectangleF(inicioX, inicioY, 400, 50); // Se expandirá automáticamente

        //        paginapresentacion.Canvas.DrawString(url1, fuente, pincel, areaTexto1, formatoJustificado);

        //        PdfUriAnnotation enlace1 = new PdfUriAnnotation(areaTexto1)
        //        {
        //            Uri = url1,
        //            Border = new PdfAnnotationBorder(0) // Esto elimina el borde
        //        };
        //        paginapresentacion.AnnotationsWidget.Add(enlace1);



        //        //RectangleF rectangulobanxico = new RectangleF(10, renglon1, paginapresentacion.Canvas.ClientSize.Width /2, renglon1+40);

        //        //// Dibujar el párrafo en el PDF
        //        //paginapresentacion.Canvas.DrawString(url1, fuenteTexto, brocha, rectangulobanxico, formatoJustificado);


        //        // Agregar la segunda liga debajo de la primera
        //        //string url2 = "https://www.banxico.org.mx/SieInternet/consultarDirectorioInternetAction.do?sector=6&accion=consultarCuadro&idCuadro=CF102&locale=es";


        //        //inicioX = 250; // Inicia en la columna 0
        //        //inicioY = renglon1; // Inicia en el renglón 200

        //        //RectangleF areaTexto2 = new RectangleF(inicioX, inicioY, 250, 50); // Se expandirá automáticamente
        //        //paginapresentacion.Canvas.DrawString(url2, fuente, pincel, areaTexto2, formatoJustificado);

        //        //// Agregar hipervínculo a la primera URL
        //        //PdfUriAnnotation enlace2 = new PdfUriAnnotation(areaTexto2)
        //        //{
        //        //    Uri = url2,
        //        //    Border = new PdfAnnotationBorder(0) // Esto elimina el borde
        //        //};
        //        //paginapresentacion.AnnotationsWidget.Add(enlace2);


        //        renglon1 = renglon1 + 40;
        //        textoParrafo = "Dicha cuota se calcula como la diferencia entre el precio de exportación (base seca al 100%) y el precio de referencia actualizado, sin exceder un margen de discriminación de precios del 54.79%, con un precio de referencia vigente de 288.71 USD por tonelada métrica. ";
        //        RectangleF rectanguloTexto3 = new RectangleF(0, renglon1, paginapresentacion.Canvas.ClientSize.Width - 20, 60);
        //        paginapresentacion.Canvas.DrawString(textoParrafo, fuenteTexto, brocha, rectanguloTexto3, formatoJustificado);

        //        renglon1 = renglon1 + 40;

        //        textoParrafo = "Como resultado de esta medida arancelaria, los precios del insumo han tenido un ajuste significativo: ";
        //        RectangleF rectanguloTexto4 = new RectangleF(0, renglon1, paginapresentacion.Canvas.ClientSize.Width - 20, 40);
        //        paginapresentacion.Canvas.DrawString(textoParrafo, fuenteTexto, brocha, rectanguloTexto4, formatoJustificado);

        //        renglon1 = renglon1 + 20;
        //        textoParrafo = "Estos factores, ajenos a nuestro control, nos obligan a realizar un ajuste en nuestros precios, con el objetivo de garantizar la continuidad operativa y la calidad de nuestros productos. ";
        //        RectangleF rectanguloTexto5 = new RectangleF(0, renglon1, paginapresentacion.Canvas.ClientSize.Width - 20, 40);
        //        paginapresentacion.Canvas.DrawString(textoParrafo, fuenteTexto, brocha, rectanguloTexto5, formatoJustificado);

        //        renglon1 = renglon1 + 30;

        //        textoParrafo = "Agradecemos su comprensión y reiteramos nuestra disposición para colaborar estrechamente con ustedes, ofreciendo soluciones adaptadas a sus necesidades y respaldadas por un equipo técnico altamente capacitado. ";
        //        RectangleF rectanguloTexto6 = new RectangleF(0, renglon1, paginapresentacion.Canvas.ClientSize.Width - 20, 40);
        //        paginapresentacion.Canvas.DrawString(textoParrafo, fuenteTexto, brocha, rectanguloTexto6, formatoJustificado);

        //        renglon1 = renglon1 + 50;

        //        titulo = "ATENTAMENTE";
        //        paginapresentacion.Canvas.DrawString(titulo, fuenteTexto, brocha, 0, renglon1, formatojustificado);

        //        renglon1 = renglon1 + 40;
        //        titulo = "Equipo Comercial Key Química";
        //        paginapresentacion.Canvas.DrawString(titulo, fuenteTexto, brocha, 0, renglon1, formatojustificado);



        //        #endregion pagina 1 

        //        #region Inicia a insertar datos en pdf pagina2
        //        PdfPageBase pagina = pdf.Pages.Add();


        //        if (File.Exists(rutaLogo2))
        //        {
        //            //var imagen = hoja.Drawings.AddPicture("Logo2", new FileInfo(rutaLogo2));
        //            //imagen.SetPosition(0, 0, 0, 0); // Posicionar en la esquina superior izquierda
        //            //                                //imagen.SetSize(100, 50); // Ajustar tamaño
        //            PdfImage imagen = PdfImage.FromFile(rutaLogo2);
        //            pagina.Canvas.DrawImage(imagen, new RectangleF(0, 0, 140, 80));


        //        }



        //        pagina.Canvas.DrawString($"Fecha: {DateTime.Now:dd/MM/yyyy}", textoFontBold, brocha, 510, 10, formatoDerecha);
        //        pagina.Canvas.DrawString("Key Química SA de CV", textoFontBold, brocha, 510, 25, formatoDerecha);
        //        pagina.Canvas.DrawString("KQU6911016X5", textoFontBold, brocha, 510, 40, formatoDerecha);
        //        PdfStringFormat formatoaDerecha = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);


        //        titulo = "PROPUESTA ECONÓMICA";
        //        pagina.Canvas.DrawString(titulo, textoFontBold, brocha, 10, 80, formatojustificado);

        //        pagina.Canvas.DrawString("Empresa: ", textoFontBold, brocha, 10, 100, formatojustificado);
        //        pagina.Canvas.DrawString("Atención: ", textoFontBold, brocha, 10, 110, formatojustificado);

        //        pagina.Canvas.DrawString(nom_comercial, textoFont, brocha, 110, 100, formatoIzquierda);
        //        pagina.Canvas.DrawString(contacto, textoFont, brocha, 110, 110, formatoIzquierda);


        //        //parrafo

        //        textoParrafo = "Key es una empresa 100% mexicana con más de 50 años de experiencia en la industria, lo que nos ha dado el conocimiento para "
        //           + "entender que en la actualidad los clientes son más demandantes con la limpieza y con espacios más limpios y sanos, por lo que "
        //           + "nos hemos especializado en no solo ofrecer químicos y accesorios, sino en desarrollar un programa completo de higiene, "
        //           + "personalizado, según las necesidades de cada cliente, que les ayude a cumplir con altos estándares de limpieza de manera simple y a "
        //           + "un costo óptimo. ";


        //        int renglon = 140;
        //        formatoJustificado.Alignment = PdfTextAlignment.Justify;
        //        RectangleF rectanguloTexto = new RectangleF(10, renglon, pagina.Canvas.ClientSize.Width - 20, 100);

        //        // Dibujar el párrafo en el PDF
        //        pagina.Canvas.DrawString(textoParrafo, fuenteTexto, brocha, rectanguloTexto, formatoJustificado);


        //        titulo = "Esperando podamos colaborar e integrar un equipo, nos reiteramos a sus órdenes. ";
        //        pagina.Canvas.DrawString(titulo, fuenteTexto, brocha, 10, 200, formatojustificado);



        //        PdfTable tablaPDF = new PdfTable
        //        {
        //            Style =
        //            {
        //                ShowHeader = true,
        //                HeaderStyle = { Font = new PdfFont(PdfFontFamily.Helvetica, 10f, PdfFontStyle.Bold), BackgroundBrush = PdfBrushes.LightGray },
        //                DefaultStyle = { Font = fuenteTexto, TextBrush = brocha }
        //            }
        //        };

        //        tablaPDF.Style.HeaderSource = PdfHeaderSource.ColumnCaptions;
        //        tablaPDF.Style.DefaultStyle.Font = new PdfTrueTypeFont(new Font("Arial", 8f), true);

        //        var tablaDatos = new DataTable();
        //        tablaDatos.Columns.Add("Producto");
        //        tablaDatos.Columns.Add("Descripción");
        //        tablaDatos.Columns.Add("Precio");

        //        renglon = 230;
        //        int iclientes = 0;
        //        foreach (var cliente in clientes)
        //        {
        //            renglon = renglon + 10;
        //            iclientes++;
        //            tablaDatos.Rows.Add(cliente.Id_Prd, cliente.NomProducto, cliente.PrecioNegociadoProy.ToString("N2", CultureInfo.CreateSpecificCulture("es-MX")));
        //        }


        //        tablaPDF.DataSource = tablaDatos;


        //        tablaPDF.Columns[0].Width = 70;
        //        tablaPDF.Columns[1].Width = 280;
        //        tablaPDF.Columns[2].Width = 70;
        //        tablaPDF.Columns[2].StringFormat = formatoaDerecha;

        //        //PdfLayoutResult resultadoTabla = tablaPDF.Draw(pagina, new PointF(20, 220));

        //        //as lo tenia en mi c´digo 27ene2025
        //        //tablaPDF.Draw(pagina, new PointF(20, 220));

        //        float altoDisponible = pagina.Canvas.ClientSize.Height - 200; // Espacio disponible
        //                                                                      //while (resultadoTabla.Bounds.Bottom + 30 > altoDisponible)
        //                                                                      //{
        //                                                                      //    pagina = pdf.Pages.Add(); // Agregar nueva página
        //                                                                      //    renglon = 20; // Reiniciar renglón en la nueva página
        //                                                                      //    resultadoTabla = tablaPDF.Draw(pagina, new PointF(20, renglon));
        //                                                                      //    renglon = 20 + (iclientes*10);
        //                                                                      //}
        //                                                                      // Variables de control
        //        int totalRenglones = tablaDatos.Rows.Count;
        //        int renglonesPorPagina = 25;
        //        int renglonesMostrados = 0;
        //        renglon = 220;

        //        while (renglonesMostrados < totalRenglones)
        //        {
        //            // Definir cuántos renglones mostrar en esta página
        //            int renglonesEnEstaPagina = renglonesMostrados == 0 ? 25 : 70;
        //            renglonesEnEstaPagina = Math.Min(renglonesEnEstaPagina, totalRenglones - renglonesMostrados);

        //            // Extraer los datos de la tabla para esta página
        //            DataTable tablaPagina = tablaDatos.Clone();
        //            for (int i = renglonesMostrados; i < renglonesMostrados + renglonesEnEstaPagina; i++)
        //            {
        //                tablaPagina.ImportRow(tablaDatos.Rows[i]);
        //            }

        //            // Dibujar la tabla
        //            PdfTable tablaTemp = new PdfTable { DataSource = tablaPagina };
        //            tablaTemp.Style = tablaPDF.Style;

        //            tablaTemp.Columns[0].Width = 70;
        //            tablaTemp.Columns[1].Width = 260;
        //            tablaTemp.Columns[2].Width = 70;
        //            tablaTemp.Columns[2].StringFormat = formatoaDerecha;

        //            PdfLayoutResult resultadoTabla = tablaTemp.Draw(pagina, new PointF(20, renglon));

        //            // Agregar pie de página con imagen
        //            //string rutaImagen = "footer_image.jpg";
        //            //PdfImage imagenPiedepagina = PdfImage.FromFile(rutaImagen);
        //            //float anchoPagina = pagina.Canvas.ClientSize.Width;
        //            //float altoImagen = (imagenPiedepagina.PhysicalDimension.Height / imagenPiedepagina.PhysicalDimension.Width) * anchoPagina;
        //            //float posicionY = pagina.Canvas.ClientSize.Height - altoImagen - 20;
        //            //pagina.Canvas.DrawImage(imagenPiedepagina, 0, posicionY, anchoPagina, altoImagen);

        //            // Actualizar el número de renglones mostrados
        //            renglonesMostrados += renglonesEnEstaPagina;

        //            // Si quedan más registros, crear una nueva página
        //            if (renglonesMostrados < totalRenglones)
        //            {

        //                altoImagen = (imagenPiedepagina.PhysicalDimension.Height / imagenPiedepagina.PhysicalDimension.Width) * anchoPagina;

        //                // Dibujar la imagen en la parte inferior de la página
        //                float posicionY2 = pagina.Canvas.ClientSize.Height - altoImagen;
        //                pagina.Canvas.DrawImage(imagenPiedepagina, 0, posicionY2 + 5, anchoPagina, altoImagen);
        //                pagina = pdf.Pages.Add();
        //                renglon = 30;
        //            }
        //            else
        //            {
        //                renglon = (int)resultadoTabla.Bounds.Bottom + 10;
        //            }
        //        }



        //        // Agregar texto final después de la tabla



        //        //agregar el texto de la despedida debajo del grid 
        //        renglon = renglon + 10;
        //        pagina.Canvas.DrawString("Beneficios Tangibles", textoFontBold, brocha, 90, renglon, formatoDerecha);
        //        renglon = renglon + 10;
        //        pagina.Canvas.DrawString("Logro de estándares de limpieza: ", textoFontBold, brocha, 0, renglon, formatojustificado);
        //        pagina.Canvas.DrawString("Instalaciones limpias que de manera sostenida cumplan con todos los estándares de higiene.", fuenteTextofinal, brocha, 145, renglon, formatojustificado);
        //        renglon = renglon + 10;
        //        pagina.Canvas.DrawString("Costo óptimo:  ", textoFontBold, brocha, 0, renglon, formatojustificado);
        //        pagina.Canvas.DrawString("Reducción de costo mensual integral de la limpieza.", fuenteTextofinal, brocha, 67, renglon, formatojustificado);
        //        renglon = renglon + 20;
        //        pagina.Canvas.DrawString("Términos y Condiciones", textoFontBold, brocha, 0, renglon, formatojustificado);
        //        renglon = renglon + 10;
        //        pagina.Canvas.DrawString("* Vigencia de esta cotización es de " + dias + " días a partir de la fecha presentada.", fuenteTextofinal, brocha, 0, renglon, formatojustificado);
        //        renglon = renglon + 10;
        //        pagina.Canvas.DrawString("* Precios No Incluyen IVA", fuenteTextofinal, brocha, 0, renglon, formatojustificado);
        //        renglon = renglon + 10;
        //        pagina.Canvas.DrawString("* Entrega a domicilio", fuenteTextofinal, brocha, 0, renglon, formatojustificado);
        //        renglon = renglon + 10;
        //        pagina.Canvas.DrawString("* Precios entran en vigor a partir del " + fechainicio, fuenteTextofinal, brocha, 0, renglon, formatojustificado);
        //        renglon = renglon + 10;
        //        pagina.Canvas.DrawString("* Los precios convenidos se respetarán y cualquier cambio será comunicado al cliente con 15 días de anticipación.", fuenteTextofinal, brocha, 0, renglon, formatojustificado);
        //        renglon = renglon + 10;
        //        textoParrafo = "* En caso de que nuestra solución contemple la instalación de equipos dosificadores, éstos se ofrecerán en calidad de préstamo, para lo cual se firmará un Contrato de Comodato entre ambas partes.";
        //        formatoJustificado.Alignment = PdfTextAlignment.Justify;
        //        rectanguloTexto = new RectangleF(0, renglon, pagina.Canvas.ClientSize.Width - 20, 30);

        //        // Dibujar el párrafo en el PDF
        //        pagina.Canvas.DrawString(textoParrafo, fuenteTextofinal, brocha, rectanguloTexto, formatoJustificado);
        //        renglon = renglon + 20;
        //        pagina.Canvas.DrawString("* Se establece que las condiciones de crédito serán de " + diascredito + " días naturales a partir de la fecha de revisión de nuestra factura.", fuenteTextofinal, brocha, 0, renglon, formatojustificado);
        //        renglon = renglon + 10;
        //        pagina.Canvas.DrawString("* En caso de que haya demora en los pagos, Key Soluciones de Limpieza suspenderá el suministro de productos y servicios.", fuenteTextofinal, brocha, 0, renglon, formatojustificado);
        //        renglon = renglon + 10;
        //        pagina.Canvas.DrawString("* Para formalizar los términos, condiciones y alcance de nuestro servicio, se firmará un Convenio que garantice el cumplimiento.", fuenteTextofinal, brocha, 0, renglon, formatojustificado);

        //        //parrafo de agradecimiento
        //        renglon = renglon + 20;
        //        textoParrafo = "Agradeciendo la oportunidad para poder presentarle nuestra propuesta, me despido de usted esperando poder atenderle próximamente";
        //        //PdfTrueTypeFont fuenteTexto = new PdfTrueTypeFont(new Font("Arial", 10f), true);
        //        //PdfStringFormat formatoJustificado = new PdfStringFormat();
        //        formatoJustificado.Alignment = PdfTextAlignment.Justify;
        //        RectangleF agradecimiento = new RectangleF(0, renglon, paginapresentacion.Canvas.ClientSize.Width - 20, 200);

        //        // Dibujar el párrafo en el PDF nombre del rik y telefono
        //        pagina.Canvas.DrawString(textoParrafo, fuenteTexto, brocha, agradecimiento, formatoJustificado);
        //        renglon = renglon + 40;
        //        textoParrafo = nombrerepresentante + "  ,  " + telefonorik;
        //        formatoJustificado.Alignment = PdfTextAlignment.Justify;
        //        rectanguloTexto = new RectangleF(0, renglon, pagina.Canvas.ClientSize.Width, 30);

        //        // Dibujar el párrafo en el PDF
        //        pagina.Canvas.DrawString(textoParrafo, fuenteTextofinal, brocha, rectanguloTexto, formatoCentrado);



        //        // Obtener el ancho y alto de la página

        //        altoImagen = (imagenPiedepagina.PhysicalDimension.Height / imagenPiedepagina.PhysicalDimension.Width) * anchoPagina;

        //        // Dibujar la imagen en la parte inferior de la página
        //        float posicionY = pagina.Canvas.ClientSize.Height - altoImagen;
        //        pagina.Canvas.DrawImage(imagenPiedepagina, 0, posicionY + 5, anchoPagina, altoImagen);

        //        pdf.SaveToFile(rutaCompleta);
        //        // Cerrar el documento
        //        pdf.Close();

        //        Console.WriteLine($"PDF generado en: {rutaCompleta}");



        //        #endregion termina insertar datos en pdf 



        //        //pruebas de archivo 
        //        string rutaBase = AppDomain.CurrentDomain.BaseDirectory;
        //        string subCarpeta = "Reportes";
        //        string rutaCompleta2 = Path.Combine(rutaBase, subCarpeta, nombreArchivo);
        //        string archivoUrl = rutaCompleta2;
        //        //pruebas de archivo fin 

        //        Uri requestUrl = HttpContext.Current.Request.Url;
        //        string baseUrl = $"{requestUrl.Scheme}://{requestUrl.Host}:{requestUrl.Port}";
        //        string archivoUrl2 = $"{baseUrl}/Reportes/{nombreArchivo}";
        //        Console.WriteLine(baseUrl);
        //        Console.WriteLine(archivoUrl2);
        //        Console.WriteLine(archivoUrl);
        //        string urlBase = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath;
        //        Console.WriteLine("urlBase", urlBase);
        //        // Construir la URL completa del archivo
        //        string archivoUrl3 = $"{urlBase}/Reportes/{nombreArchivo}";
        //        Console.WriteLine(archivoUrl3);




        //        #region enviar correo 


        //        string correoUsuario = MySesion.U_Correo;

        //        // ENVIO DE CORREO 

        //        ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
        //        configuracion.Id_Cd = MySesion.Id_Cd_Ver;
        //        configuracion.Id_Emp = MySesion.Id_Emp;
        //        CN_Configuracion cn_configuracion = new CN_Configuracion();
        //        cn_configuracion.Consulta(ref configuracion, MySesion.Emp_Cnx);


        //        string s_temp_file = "";
        //        s_temp_file = HttpContext.Current.Server.MapPath("~/GestionPrecios/GestionIncrementoEmailTemplate.htm");
        //        string Tmp = string.Empty;
        //        StringBuilder SB = new StringBuilder();

        //        using (StreamReader reader = new StreamReader(s_temp_file))
        //        {
        //            Tmp = reader.ReadToEnd();
        //            SB.Append(Tmp);
        //        }
        //        //string LigaServidor = "http://40.84.229.61/siancentral/CL_Autorizaciones.aspx?";

        //        //SB.Replace("{AUT_PARAMESTROS}", LigaServidor + "SolFolio=" + SolFolio.ToString() + "&Id_Cd=" + Id_Cd.ToString() + "&TipoCompra=" + TipoCompra + "&TipoSolicitud=" + TipoSolicitud.ToString());
        //        SB.Replace("{TITULO}", "Aviso de Ajuste en Nuestros Precios");
        //        SB.Replace("{EMPRESA}", MySesion.Emp_Nombre);
        //        SB.Replace("{ATENCION}", contacto);
        //        SB.Replace("{CENTRO}", MySesion.Id_Cd.ToString() + " - " + MySesion.Cd_Nombre);
        //        SB.Replace("{REPRESENTANTE}", nombrerepresentante);
        //        SB.Replace("{TELEFONO}", telefonorik);
        //        SB.Replace("{FECHAINICIO}", fechainicio);
        //        //   SB.Replace("{CORREO_AUTORIZADOR}", LstAutorizadores[0].Responsable + " (" + LstAutorizadores[0].Correo + ")");
        //        string body = SB.ToString();

        //        AlternateView vistaHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);

        //        //this.RespaldoCorreo(hfNumSolicitudAbasto.Value, body, correo);
        //        SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(2525));
        //        sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
        //        sm.EnableSsl = false;
        //        MailMessage m = new MailMessage();
        //        //  string[] eVirtual = configuracion.Mail_EVirtual.Split(',');
        //        m.From = new MailAddress(configuracion.Mail_Remitente);


        //        string rutaPDF = rutaCompleta; //  @"C:\ruta\del\archivo.pdf";
        //        Attachment adjunto = new Attachment(rutaPDF, MediaTypeNames.Application.Pdf);
        //        m.Attachments.Add(adjunto);
        //        //string correo = "ing.rborquez@gmail.com, raul.borquez@gibraltar.com.mx,dianela.morales@key.com.mx,servicios.informatica@gibraltar.com.mx";

        //        //this.CorreosAutorizadorxMotivoxApp(ref correo, TipoSolicitud, IdAplicacion);

        //        //descomentar para pruebas 
        //        //correo = "whylfredo.valero@gibraltar.com.mx,orlando.guzman@gibraltar.com.mx";
        //        //correo = "francisco.cepeda@gibraltar.com.mx";
        //        correo = correo.Replace(";", ",");
        //        string[] eVirtual2 = correo.Split(',');
        //        int reng = 1;

        //        reng = 0;

        //        foreach (string core in eVirtual2)
        //        {
        //            if (core != " ")
        //            {
        //                if (reng == 0)
        //                {
        //                    m.To.Add(new MailAddress(core));
        //                    reng = 1;
        //                }
        //                else
        //                {
        //                    m.CC.Add(new MailAddress(core));
        //                }
        //            }
        //        }

        //        //m.Bcc.Add(new MailAddress("dianela.morales@key.com.mx"));
        //        // m.Bcc.Add(new MailAddress("francisco.cepeda@gibraltar.com.mx"));
        //        if (correo != correoUsuario)
        //        {
        //            m.CC.Add(new MailAddress(correoUsuario));
        //        }


        //        m.Subject = "Propuesta económica para  " + nom_comercial;
        //        m.IsBodyHtml = true;
        //        try
        //        {
        //            //LinkedResource logo = new LinkedResource(MapPath(@"Imagenes/logo.jpg"), MediaTypeNames.Image.Jpeg);
        //            //logo.ContentId = "companylogo";
        //            //vistaHtml.LinkedResources.Add(logo);
        //        }
        //        catch (Exception)
        //        {
        //        }
        //        m.AlternateViews.Add(vistaHtml);

        //        int CorreoEnviado = 0;
        //        try
        //        {
        //            sm.Send(m);
        //            CorreoEnviado = 1;

        //        }
        //        catch (Exception exx)
        //        {
        //            CorreoEnviado = -1;
        //        }


        //        #endregion enviar correo


        //        return archivoUrl3;
        //    }
        //    catch (Exception ex)
        //    {
        //        return $"Error: {ex.Message}";
        //    }
        //}

    }


}

