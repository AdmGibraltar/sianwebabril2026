using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using CapaEntidad;
using CapaNegocios;
using System.IO;
using System.Configuration;
using System.Text;
using System.Xml;

namespace SIANWEB
{
    public partial class CapCFDITimbres : System.Web.UI.Page
    {
        #region Variables 

        private bool _PermisoGuardar { get { if (Session["Sesion" + Session.SessionID] == null) { return false; } return (bool)Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]]; } set { Session["PermisoGuardar" + Session.SessionID + Page.Request.Url.ToString().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]] = value; } }

        public static string param { get; set; }

        public static Int32 Tipo { get; set; }


        public DataTable objdtListaProd { get; set; }
        protected DataTable objdtTablaProd { get { if (ViewState["objdtTablaProd"] != null) { return (DataTable)ViewState["objdtTablaProd"]; } else { return objdtListaProd; } } set { ViewState["objdtTablaProd"] = value; } }

        #endregion

        #region Metodos
        protected void Page_Load(object sender, EventArgs e)
        {
            Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            CartaPorte carta = new CartaPorte();
            if (Sesion == null)
            {
                string[] pag = Page.Request.Url.ToString().Split(new string[] { "/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    carta.Id_Doc = Convert.ToInt32(Page.Request.QueryString["Id_Doc"]);
                    carta.Id_Cte = Convert.ToInt32(Page.Request.QueryString["Id_Cte"]);
                    carta.Nom_Cliente = Page.Request.QueryString["Nombre"];
                    carta.CodigoPostal = Page.Request.QueryString["CodigoPostal"].Trim();
                    carta.Colonia = Page.Request.QueryString["Colonia"];
                    carta.TipoDoc = Page.Request.QueryString["TipoDoc"];
                    _PermisoGuardar = Convert.ToBoolean(Page.Request.QueryString["permitirModificar"]);

                    Inicializacion(carta);
                }
            }
        }

        private void Inicializacion(CartaPorte carta)
        {
            Getlist(carta);
            txtdocumento.Text = carta.Id_Doc.ToString();
            txtCliente.Text = carta.Id_Cte.ToString();
            txtNombre.Text = carta.Nom_Cliente;
            txtTipoDoc.Text = carta.TipoDoc;
            if (carta.CodigoPostal == "")
            {
                txtcp.Text = "0";
            }
            else
            {
                txtcp.Text = carta.CodigoPostal.ToString();
            }
            string Colonia = carta.Colonia.ToString();
            CargaColonias(int.Parse(txtcp.Text));
        }

        private void Getlist(CartaPorte carta)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            try
            {
                CN_CapRemision CN = new CN_CapRemision();                   
                DataSet dtCfdi = new DataSet();
                new CN_CapRemision().ConsultaCDFI(sesion,  ref carta, ref dtCfdi);
                RgDet.DataSource = dtCfdi.Tables[0];//.Select("Cfdi_Estatus = '1'");
                RgDet.Rebind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void descargarXML(int Id_Doc, int Id_CFDI)
        {
            Remision rem = new Remision();
            Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            object CFDI_Xml = "NULL";
            object CFDI_Pdf = "NULL";
            CN_CapRemision remision = new CN_CapRemision();
            remision.Consulta_CartaPorteSAT(Id_Doc, Id_CFDI, Sesion, ref CFDI_Pdf, ref CFDI_Xml, ref Id_CFDI);
            if (Id_CFDI > 0)
            {
                string ruta = null;

                System.IO.StreamWriter sw = null;
                ruta = Server.MapPath("xmlSAT") + "\\TRASLADO_" + Sesion.Id_Cd.ToString() + "_" + Id_CFDI.ToString() + ".txt";

                if (File.Exists(ruta))
                    File.Delete(ruta);
                if (File.Exists(Server.MapPath("xmlSAT") + "\\TRASLADO_" + Sesion.Id_Cd.ToString() + "_" + Id_CFDI.ToString() + ".xml"))
                    File.Delete(Server.MapPath("xmlSAT") + "\\TRASLADO_" + Sesion.Id_Cd.ToString() + "_" + Id_CFDI.ToString() + ".xml");
                sw = new System.IO.StreamWriter(ruta, false, Encoding.UTF8);
                sw.WriteLine(CFDI_Xml.ToString());
                sw.Close();
                File.Move(ruta, Server.MapPath("xmlSAT") + "\\TRASLADO_" + Sesion.Id_Cd.ToString() + "_" + Id_CFDI.ToString() + ".xml");

                RAM1.ResponseScripts.Add(string.Concat(@"abrirArchivo('xmlSAT\\TRASLADO_" + Sesion.Id_Cd.ToString() + "_", Id_CFDI.ToString(), ".xml')"));
            }
            else
            {
                Alerta("Este documento no cuenta con un archivo XML traslado generado.");
            }
        }

        private void descargarPDF(int Id_Doc, int Id_CFDI)
        {
            Remision rem = new Remision();
            Sesion Sesion = (Sesion)Session["Sesion" + Session.SessionID];
            object CFDI_Xml = "NULL";
            object CFDI_Pdf = "NULL";
            CN_CapRemision remision = new CN_CapRemision();
            remision.Consulta_CartaPorteSAT(Id_Doc, Id_CFDI, Sesion, ref CFDI_Pdf, ref CFDI_Xml, ref Id_CFDI);

            if (Id_CFDI > 0)
            {
                byte[] archivoPdf = (byte[])CFDI_Pdf;
                if (archivoPdf.Length > 0)
                {
                    string tempPDFname = string.Concat("TRASLADO_"
                             , Sesion.Id_Emp.ToString()
                             , "_", Sesion.Id_Cd.ToString()
                             , "_", Id_CFDI.ToString()
                             , ".pdf");
                    string URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                    string WebURLtempPDF = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFname);
                    this.ByteToTempPDF(URLtempPDF, archivoPdf);
                    // ------------------------------------------------------------------------------------------------
                    // Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                    // ------------------------------------------------------------------------------------------------
                    RAM1.ResponseScripts.Add("AbrirCFDITrasladoPDF('" + WebURLtempPDF + "')");
                }
            }
            else
            {
                Alerta("Este documento no cuenta con un archivo PDF traslado generado.");
            }
        }
        private void ImprimirCDFITraslado(int Id_Emp, int Id_Cd, int Id_Doc, string Tipo)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CartaPorte carta = new CartaPorte();
                carta.Id_Emp = Id_Emp;
                carta.Id_Cd = Id_Cd;
                carta.Id_Doc = Id_Doc;
                object CFDI_Xml = "NULL";
                object CFDI_Pdf = "NULL";
                int Id_CFDI = 0;
                int CFDI_Estatus = 0;
                Remision remision = new Remision();
                Factura factura = new Factura();
                CN_CapRemision CN = new CN_CapRemision();
                List<FacturaDet> detalleFactura = new List<FacturaDet>();
                List<RemisionDet> detalleRemision = new List<RemisionDet>();
                CN.Consulta_CFDISAT(carta, sesion.Emp_Cnx, ref CFDI_Pdf, ref CFDI_Xml, ref Id_CFDI, ref CFDI_Estatus);
                if (Id_CFDI == 0)
                {

                    StringBuilder XML_Enviar = new StringBuilder();
                    StringBuilder XML_Detalle = new StringBuilder();
                    StringBuilder XML_Mercancia = new StringBuilder();
                    StringBuilder XML_Origen = new StringBuilder();
                    StringBuilder XML_Destino = new StringBuilder();

                    RemisionDet RemDet = new RemisionDet();
                    ClienteDirEntrega Entrega = new ClienteDirEntrega();
                    DataSet dsDirecciones = new DataSet();
                    DataSet dsCliente = new DataSet();
                    int verificador = 0;

                    ////Consulta Remision 
                    //Remision remision = new Remision();
                    //new CN_CapRemision().ConsultarEncabezadoImprimir(sesion, Id_Rem, ref remision, 0);

                    ////Consulta Detalle de remision
                    //List<RemisionDet> detalles = new List<RemisionDet>();
                    //new CN_CapRemision().ConsultaRemisionDetalle(sesion, carta, ref detalles);

                    if (txtTipoDoc.Text == "Remision")
                    {
                        //Consulta Remision 
                        new CN_CapRemision().ConsultarEncabezadoImprimir(sesion, Id_Doc, ref remision, 0);

                        //Consulta Detalle de remision
                        new CN_CapRemision().ConsultaRemisionDetalle(sesion, carta, ref detalleRemision);
                    }

                    if (txtTipoDoc.Text == "Factura")
                    {
                        //Consulta Remision 
                        bool valor = false;
                        factura.Id_Emp = sesion.Id_Emp;
                        factura.Id_Cd = sesion.Id_Cd_Ver;
                        factura.Id_Fac = carta.Id_Doc;
                        new CN_CapFactura().ConsultaFacturaEncabezado(ref factura, sesion.Emp_Cnx, ref valor);

                        //Consulta Detalle de remision
                        new CN_CapRemision().ConsultaFacturaDetalle(sesion, carta, ref detalleFactura);
                    }

                    if (txtTipoDoc.Text == "Remision")
                    {
                        carta.Id_Cte = remision.Id_Cte;
                    }
                    else
                    {
                        carta.Id_Cte = factura.Id_Cte;
                    }

                    //Consulta datos de clientes
                    Clientes Cliente = new Clientes();
                    carta.Id_Cte = int.Parse(txtCliente.Text);
                    carta.CodigoPostal = txtcp.Text;
                    carta.Colonia = CmbColonia.SelectedItem.Text;
                    new CN_CapRemision().ConsultaClienteCDFI(sesion, carta, ref Cliente);

                    //Consulta datos de clientes
                    string Serie = "";
                    new CN_CapRemision().ConsultaFolioCDFI(sesion, ref Id_CFDI, ref Serie);


                    #region construirXML

                    if (txtTipoDoc.Text == "Remision")
                    {
                        foreach (RemisionDet Det in detalleRemision)
                        {
                            XML_Detalle.Append(" <Concepto");
                            XML_Detalle.Append(" ClaveProdServ=\"" + Det.Producto.Prd_ClaveProdServ + "\"");
                            XML_Detalle.Append(" ClaveUnidad=\"" + Det.Producto.Prd_ClaveUnidad + "\"");
                            XML_Detalle.Append(" cantidad=\"" + Det.Rem_Cant + "\"");
                            XML_Detalle.Append(" noIdentificacion=\"" + Det.Id_Prd + "\"");
                            XML_Detalle.Append(" descripcion=\"" + Det.Prd_Descripcion.Replace("\"" , "&quot;") + "\"");
                            XML_Detalle.Append(" valorUnitario=\"" + (Det.Rem_Precio).ToString() + "\"");

                            XML_Detalle.Append(" importe=\"" + (Det.Rem_Precio * Det.Rem_Cant).ToString() + "\"");
                            XML_Detalle.Append(" tasaIva=\"" + ((Det.Rem_Precio * Det.Rem_Cant) * 0.16).ToString() + "\"");
                            XML_Detalle.Append(" Marca=\"" + "" + "\" />");
                        }

                        foreach (RemisionDet Det in detalleRemision)
                        {
                            XML_Mercancia.Append(" <Mercancia");
                            XML_Mercancia.Append(" IdProd=\"" + Det.Id_Prd + "\"");
                            XML_Mercancia.Append(" Cantidad=\"" + Det.Rem_Cant + "\"");
                            XML_Mercancia.Append(" IdOrigen=\"" + "OR000001" + "\"");
                            XML_Mercancia.Append(" IdDestino=\"" + "DE000001" + "\" />");
                        }
                    }
                    else
                    {
                        foreach (FacturaDet Det in detalleFactura)
                        {
                            XML_Detalle.Append(" <Concepto");
                            XML_Detalle.Append(" ClaveProdServ=\"" + Det.Producto.Prd_ClaveProdServ + "\"");
                            XML_Detalle.Append(" ClaveUnidad=\"" + Det.Producto.Prd_ClaveUnidad + "\"");
                            XML_Detalle.Append(" cantidad=\"" + Det.Fac_Cant + "\"");
                            XML_Detalle.Append(" noIdentificacion=\"" + Det.Id_Prd + "\"");
                            XML_Detalle.Append(" descripcion=\"" + Det.Producto.Descripcion + "\"");
                            XML_Detalle.Append(" valorUnitario=\"" + (Det.Fac_Precio).ToString() + "\"");

                            XML_Detalle.Append(" importe=\"" + (Det.Fac_Precio * Det.Fac_Cant).ToString() + "\"");
                            XML_Detalle.Append(" tasaIva=\"" + ((Det.Fac_Precio * Det.Fac_Cant) * 0.16).ToString() + "\"");
                            XML_Detalle.Append(" Marca=\"" + "" + "\" />");
                        }

                        foreach (FacturaDet Det in detalleFactura)
                        {
                            XML_Mercancia.Append(" <Mercancia");
                            XML_Mercancia.Append(" IdProd=\"" + Det.Id_Prd + "\"");
                            XML_Mercancia.Append(" Cantidad=\"" + Det.Fac_Cant + "\"");
                            XML_Mercancia.Append(" IdOrigen=\"" + "OR000001" + "\"");
                            XML_Mercancia.Append(" IdDestino=\"" + "DE000001" + "\" />");
                        }
                    }

                    string TipoMoneda = "";
                    if (Cliente.Id_Mon == 2)
                        TipoMoneda = "pesos";
                    else
                        TipoMoneda = "dolares";

                    double SubTotal = 0;
                    double Total = 0;
                    string Nota = "";
                    string UCorreo = "";
                    int Id_Cte = 0;

                    if (txtTipoDoc.Text == "Remision")
                    {
                        SubTotal = remision.Rem_Subtotal;
                        Total = remision.Rem_Total;
                        Nota = remision.Rem_Nota;
                        UCorreo = remision.UCorreo;
                        Id_Cte = remision.Id_Cte;
                    }
                    else
                    {
                        SubTotal = Convert.ToDouble(factura.Fac_SubTotal);
                        Total = Convert.ToDouble(factura.Fac_ImporteIva + factura.Fac_SubTotal);
                        Nota = factura.Fac_Notas;
                        UCorreo = factura.Cte_Email;
                        Id_Cte = factura.Id_Cte;
                    }


                    XML_Enviar.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                    XML_Enviar.Append("<Comprobante");
                    XML_Enviar.Append(" serie=\"" + Serie.ToString() + "\""); // 
                    XML_Enviar.Append(" folio =\"" + Id_CFDI.ToString() + "\"");
                    XML_Enviar.Append(" UUII=\"" + "" + "\"");
                    XML_Enviar.Append(" fecha=\"" + DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") + "\"");
                    XML_Enviar.Append(" formaDePago=\"" + Cliente.Cte_MetodoPago + "\"");
                    XML_Enviar.Append(" subTotal=\"" + SubTotal + "\"");
                    XML_Enviar.Append(" total=\"" + Total + "\"");
                    XML_Enviar.Append(" tipoDeComprobante=\"" + "ingreso" + "\"");
                    XML_Enviar.Append(" Sustituye=\"" + "" + "\"");
                    XML_Enviar.Append(" tipoMovimiento=\"" + "TRASLADO" + "\"");
                    XML_Enviar.Append(" tipoMoneda=\"" + TipoMoneda + "\"");
                    XML_Enviar.Append(" tipoCambio=\"" + "" + "\"");
                    XML_Enviar.Append(" movimientoCancelar=\"" + "" + "\"");
                    XML_Enviar.Append(" ConceptoDescuento1=\"" + "descto" + "\"");
                    XML_Enviar.Append(" TasaDescuento1=\"" + "0" + "\"");
                    XML_Enviar.Append(" ConceptoDescuento2=\"" + "descto" + "\"");
                    XML_Enviar.Append(" TasaDescuento2=\"" + "0" + "\"");
                    XML_Enviar.Append(" Notas=\"" + Nota + "\"");
                    XML_Enviar.Append(" Correo=\"" + UCorreo + "\"");
                    XML_Enviar.Append(" CliNum=\"" + Id_Cte + "\"");
                    XML_Enviar.Append(" MetodoPago=\"" + Cliente.Cte_PagoMetodoPago + "\"");
                    XML_Enviar.Append(" CuentaBancaria=\"" + "" + "\"");
                    XML_Enviar.Append(" Referencia=\"" + "" + "\"");
                    XML_Enviar.Append(" ComprobanteVersion=\"" + "4.0" + "\" >");

                    //Emisor 
                    XML_Enviar.Append("<Emisor");
                    XML_Enviar.Append(" rfc=\"" + "KQU6911016X5" + "\"");
                    XML_Enviar.Append(" numero=\"" + sesion.Id_Cd_Ver + "\" />");

                    //Receptor
                    XML_Enviar.Append("<Receptor");
                    XML_Enviar.Append(" rfc=\"" + Cliente.Cte_FacRfc + "\"");
                    XML_Enviar.Append(" nombre=\"" + Cliente.Cte_NomComercial + "\"");
                    XML_Enviar.Append(" UsoCFDI=\"" + Cliente.Cte_UsoCFDI + "\" >");
                    //Domicilio
                    XML_Enviar.Append("<Domicilio");
                    XML_Enviar.Append(" calle=\"" + Cliente.Cte_FacCalle + "\"");
                    XML_Enviar.Append(" noExterior=\"" + Cliente.Cte_FacNumero.Trim() + "\"");
                    XML_Enviar.Append(" colonia=\"" + Cliente.Cte_FacColonia + "\"");
                    XML_Enviar.Append(" municipio=\"" + Cliente.Cte_FacMunicipio + "\"");
                    XML_Enviar.Append(" estado=\"" + Cliente.Cte_Estado + "\"");
                    XML_Enviar.Append(" pais=\"" + Cliente.Cte_Pais + "\"");
                    XML_Enviar.Append(" codigoPostal=\"" + Cliente.Cte_FacCp + "\" />");
                    XML_Enviar.Append("</Receptor>");

                    //Conceptos
                    XML_Enviar.Append("<Conceptos>");
                    XML_Enviar.Append(XML_Detalle);
                    XML_Enviar.Append("</Conceptos>");

                    XML_Enviar.Append("<Impuestos totalImpuestosTraslados=\"" + "0" + "\" >");
                    XML_Enviar.Append("<Traslados>");
                    XML_Enviar.Append("<Traslado ");
                    XML_Enviar.Append(" impuesto=\"" + "IVA" + "\"");
                    XML_Enviar.Append(" tasa=\"" + "0" + "\"");
                    XML_Enviar.Append(" importe=\"" + "0" + "\" />");

                    XML_Enviar.Append("</Traslados>");
                    XML_Enviar.Append("</Impuestos>");


                    XML_Enviar.Append("</Comprobante>");
                    #endregion construirXML

                    // --------------------------------------
                    // Consulta centro de distribución
                    // --------------------------------------
                    CentroDistribucion Cd = new CentroDistribucion();
                    new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref Cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);
                    // --------------------------------------
                    // cargar xml que se envia a SAT
                    // --------------------------------------
                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(XML_Enviar.ToString());

                    // --------------------------------------//
                    // --------------------------------------//
                    //         LLENAR DATOS DEL XML        --//
                    // --------------------------------------//
                    // --------------------------------------//
                    #region Llenar datos a Enviar

                    CN_CatTipoMoneda cn_moneda = new CN_CatTipoMoneda();
                    TipoMoneda tm = new TipoMoneda();
                    tm.Id_Emp = sesion.Id_Emp;
                    tm.Id_Mon = Cliente.Id_Mon;
                    cn_moneda.ConsultaTipoMonedaIndividual(ref tm, sesion.Emp_Cnx);

                    //encabezado
                    XmlNode Comprobante = xml.SelectSingleNode("Comprobante");
                    Comprobante.Attributes["serie"].Value = Serie.ToString();
                    Comprobante.Attributes["folio"].Value = Id_CFDI.ToString();
                    Comprobante.Attributes["fecha"].Value = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                    Comprobante.Attributes["subTotal"].Value = SubTotal.ToString();
                    Comprobante.Attributes["total"].Value = Total.ToString();
                    Comprobante.Attributes["tipoDeComprobante"].Value = "ingreso";
                    Comprobante.Attributes["tipoMovimiento"].Value = "TRASLADO";
                    Comprobante.Attributes["tipoMoneda"].Value = tm.Mon_Abrev;
                    Comprobante.Attributes["tipoCambio"].Value = tm.Mon_TipCambio.ToString();
                    //Comprobante.Attributes["leyendaFacturaEspecial"].Value = ""; //
                    //Comprobante.Attributes["movimientoacancelar"].Value = ""; //
                    Comprobante.Attributes["CliNum"].Value = Id_Cte.ToString();
                    Comprobante.Attributes["Notas"].Value = Nota;

                    Comprobante.Attributes["MetodoPago"].Value = "99"; //.Substring(1, 2 - Cliente.Cte_PagoMetodoPago.Trim().Length);

                    Comprobante.Attributes["CuentaBancaria"].Value = "0";
                    //Comprobante.Attributes["Motivo"].Value = "";
                    //consultar datos del cliente de la nota de credito
                    Clientes cliente = new Clientes();
                    cliente.Id_Emp = sesion.Id_Emp;
                    cliente.Id_Cd = sesion.Id_Cd_Ver;
                    cliente.Id_Cte = Convert.ToInt32(Id_Cte);
                    new CN_CatCliente().ConsultaClientes(ref cliente, sesion.Emp_Cnx);

                    //receptor
                    XmlNode Receptor = Comprobante.SelectSingleNode("Receptor");
                    Receptor.Attributes["rfc"].Value = cliente.Cte_FacRfc;
                    Receptor.Attributes["nombre"].Value = cliente.Cte_NomComercial.ToString();
                    Receptor.Attributes["UsoCFDI"].Value = cliente.Cte_UsoCFDI;
                    Comprobante.Attributes["formaDePago"].Value = cliente.Cte_MetodoPago;
                    Comprobante.Attributes["ComprobanteVersion"].Value = "3.3";

                    //
                    XmlNode Emisor = Comprobante.SelectSingleNode("Emisor");
                    Emisor.Attributes["rfc"].Value = Cd.Cd_Rfc;
                    Emisor.Attributes["numero"].Value = Cd.Id_Cd.ToString();
                    //Domicilio

                    if (cliente.Cte_Colonia == "")
                    {
                        Alerta("Favor de revisar en el catalogo de cliente que el código postal y la colonia sean correctos.");
                        return;
                    }

                    XmlNode Domicilio = Receptor.SelectSingleNode("Domicilio");
                    Domicilio.Attributes["calle"].Value = cliente.Cte_FacCalle;
                    Domicilio.Attributes["noExterior"].Value = cliente.Cte_FacNumero.Trim();
                    Domicilio.Attributes["colonia"].Value = cliente.Cte_FacColonia;
                    Domicilio.Attributes["municipio"].Value = cliente.Cte_FacMunicipio;
                    Domicilio.Attributes["estado"].Value = cliente.Cte_Estado;
                    Domicilio.Attributes["pais"].Value = cliente.Cte_Pais;
                    Domicilio.Attributes["codigoPostal"].Value = cliente.Cte_FacCp.Trim();

                    // ---------------------
                    // Conceptos --> partidas = producto
                    // Detalle --> productoDetalle
                    // ---------------------              
                    XmlNode Conceptos = Comprobante.SelectSingleNode("Conceptos");
                    XmlNode producto = Conceptos.SelectSingleNode("Concepto");
                    XmlNode Impuestos = Comprobante.SelectSingleNode("Impuestos");

                    #endregion
                    // --------------------------------------
                    // convertir XML a string 
                    // --------------------------------------
                    StringWriter sw = new StringWriter();
                    XmlTextWriter tx = new XmlTextWriter(sw);
                    xml.WriteTo(tx);
                    string xmlString = sw.ToString();


                    // ------------------------------------------------------
                    // ENVIAR XML al servicio de la aplicacion de KEY
                    // ------------------------------------------------------
                    XmlDocument xmlSAT = new XmlDocument();

                    WebReference.Service1 sianFacturacionElectronica = new WebReference.Service1();
                    sianFacturacionElectronica.Url = ConfigurationManager.AppSettings["WS_CFDIImpresion"].ToString();
                    object sianFacturacionElectronicaResult = sianFacturacionElectronica.ObtieneCFD(xmlString);

                    // ------------------------------------------------------
                    string stringPDF = string.Empty;
                    string selloSAT = string.Empty;
                    string folioFiscal = string.Empty;
                    string errorNum = string.Empty;
                    string errorText = string.Empty;

                    xmlSAT.LoadXml(sianFacturacionElectronicaResult.ToString());
                    int TSAT = 1;
                    foreach (XmlNode nodo in xmlSAT.ChildNodes)
                    {
                        if (nodo.Name == "Comprobante")
                        {
                            TSAT = 1;
                            selloSAT = nodo.Attributes["Sello"].Value;
                            foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                            {
                                if (Nodo_nivel2.Name == "AddendaKey")
                                {
                                    foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                    {
                                        if (Nodo_nivel3.Name == "PDF")
                                            stringPDF = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                        if (Nodo_nivel3.Name == "ERROR")
                                        {
                                            errorNum = Nodo_nivel3.Attributes["Numero"].Value;
                                            errorText = Nodo_nivel3.Attributes["Texto"].Value;
                                        }
                                    }
                                    nodo.RemoveChild(Nodo_nivel2);
                                }
                            }
                        }
                        else if (nodo.Name == "cfdi:Comprobante")
                        {
                            TSAT = 2;
                            foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                            {
                                if (Nodo_nivel2.Name == "AddendaKey")
                                {
                                    foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                    {
                                        if (Nodo_nivel3.Name == "PDF")
                                            stringPDF = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                        if (Nodo_nivel3.Name == "ERROR")
                                        {
                                            errorNum = Nodo_nivel3.Attributes["Numero"].Value;
                                            errorText = Nodo_nivel3.Attributes["Texto"].Value;
                                        }
                                    }

                                    nodo.RemoveChild(Nodo_nivel2);
                                }

                                if (Nodo_nivel2.Name == "cfdi:Complemento")
                                {
                                    XmlNode Nodo_nivel3;
                                    Nodo_nivel3 = Nodo_nivel2.FirstChild;
                                    selloSAT = Nodo_nivel3.Attributes["SelloSAT"].Value;
                                    folioFiscal = Nodo_nivel3.Attributes["UUID"].Value;
                                }

                            }

                        }
                    }

                    if (errorNum != "0")
                        Alerta(string.Concat("El servicio de KEY ha devuelto el siguiente error:<br/>", errorText.Replace("'", "\"")));
                    else
                    {
                        //CartaPorte carta = new CartaPorte();
                        carta.Id_Emp = sesion.Id_Emp;
                        carta.Id_Cd = sesion.Id_Cd_Ver;
                        carta.Id_Doc = Id_Doc;
                        carta.Id_CFDI = Id_CFDI;
                        carta.CFDI_Fecha = DateTime.Now;
                        carta.CFDI_Sello = selloSAT;
                        carta.CFDI_FolioFiscal = folioFiscal;
                        carta.CFDI_Cancelacion = "NULL";
                        System.Data.SqlTypes.SqlXml sqlXml
                            = new System.Data.SqlTypes.SqlXml(new XmlTextReader(xmlSAT.OuterXml, XmlNodeType.Document, null));
                        carta.CFDI_Xml = sqlXml;
                        carta.CFDI_Pdf = this.Base64ToByte(stringPDF);
                        verificador = 0;
                        carta.CFDI_Estatus = "1";
                        carta.TipoDoc = txtTipoDoc.Text;
                        new CN_CapRemision().InsertarCFDITrasladoSAT(carta, sesion.Emp_Cnx, ref verificador);
                        Getlist(carta);

                        //// ------------------------------
                        //// Abrir PDF de CFDI Traslado
                        //// ------------------------------
                        //string tempPDFname = string.Concat("TRASLADO_"
                        //        , carta.Id_Emp.ToString()
                        //        , "_", carta.Id_Cd.ToString()
                        //        , "_", carta.Id_CFDI.ToString()
                        //        , ".pdf");
                        //string URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                        //string WebURLtempPDF = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFname);
                        //this.ByteToTempPDF(URLtempPDF, this.Base64ToByte(stringPDF));
                        //// ------------------------------------------------------------------------------------------------
                        //// Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                        //// ------------------------------------------------------------------------------------------------
                        //RAM1.ResponseScripts.Add("AbrirCFDITrasladoPDF('" + WebURLtempPDF + "')");

                        descargarPDF(Id_Doc, Id_CFDI);
                    }
                }
                else
                {
                    Alerta("Este Documento ya cuenta con un CFDI de traslado, se puede descargar en la opción de PDF o XML.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
       }

        public void CancelarCFDI(int Id_Emp, int Id_Cd, int Id_Doc)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CartaPorte carta = new CartaPorte();
                carta.Id_Emp = Id_Emp;
                carta.Id_Cd = Id_Cd;
                carta.Id_Doc = Id_Doc;
                object CFDI_Xml = "NULL";
                object CFDI_Pdf = "NULL";
                int Id_CFDI = 0;
                int CFDI_Estatus = 0;
                CN_CapRemision CN = new CN_CapRemision();

                CN.Consulta_CFDISAT(carta, sesion.Emp_Cnx, ref CFDI_Pdf, ref CFDI_Xml, ref Id_CFDI, ref CFDI_Estatus);

                //Consultar Cfdi
               // new CN_CapCartaPorte().ConsultaCfdi(ref carta, sesion.Emp_Cnx);
                carta.Id_U = sesion.Id_U;
                string RFC = string.Empty;
                string UUID = string.Empty;
                

                XmlDocument xmlBD = new XmlDocument();
                int TSATCANCELACION = 1;

                if (CFDI_Xml != null)
                {
                    if (CFDI_Estatus.ToString() != "2")
                    {
                        xmlBD.LoadXml(CFDI_Xml.ToString());
                        foreach (XmlNode nodo in xmlBD.ChildNodes)
                        {
                            if (nodo.Name == "Comprobante")
                            {
                                TSATCANCELACION = 1;
                            }
                            else if (nodo.Name == "cfdi:Comprobante")
                            {
                                TSATCANCELACION = 2;
                                foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                                {
                                    if (Nodo_nivel2.Name == "cfdi:Complemento")
                                    {
                                        XmlNode Nodo_nivel3;
                                        Nodo_nivel3 = Nodo_nivel2.FirstChild;
                                        UUID = Nodo_nivel3.Attributes["UUID"].Value;
                                    }
                                    if (Nodo_nivel2.Name == "cfdi:Emisor")
                                    {
                                        RFC = Nodo_nivel2.Attributes["Rfc"].Value;
                                    }
                                }
                            }
                        }
                    }
                }


                if (TSATCANCELACION == 2)
                {
                    string valorResultadoCancelacion = "0";
                    WS_CFDICancelacion.Service1 ws = new WS_CFDICancelacion.Service1();
                    ws.Url = ConfigurationManager.AppSettings["WS_CFDICancelacion"].ToString();
                    String respuestaCancelacion = ws.CancelacionWS("" + RFC + "," + UUID + "");
                    XmlDocument XmlCancelacion = new XmlDocument();
                    XmlCancelacion.LoadXml(respuestaCancelacion);
                    foreach (XmlNode nodo in XmlCancelacion.ChildNodes)
                    {
                        if (nodo.Name == "Acuse")
                        {
                            foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                            {
                                if (Nodo_nivel2.Name == "Folios")
                                {
                                    foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                    {
                                        if (Nodo_nivel3.Name == "EstatusUUID")
                                        {
                                            valorResultadoCancelacion = Nodo_nivel3.InnerText;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    string valorResultadoCancelacionTexto = string.Empty;
                    switch (valorResultadoCancelacion)
                    {
                        case "202":
                            valorResultadoCancelacionTexto = "Documento Previamente Cancelado";
                            break;
                        case "203":
                            valorResultadoCancelacionTexto = "Documento No corresponda al emisor";
                            break;
                        case "204":
                            valorResultadoCancelacionTexto = "Documento No Aplicable para cancelación";
                            break;
                        case "205":
                            valorResultadoCancelacionTexto = "Documento No Existe emisión";
                            break;
                        default:
                            valorResultadoCancelacionTexto = "No se hizo conexión con el servicio de cancelación";
                            break;
                    }
                    if (valorResultadoCancelacion != "201")
                    {
                        this.Alerta(valorResultadoCancelacionTexto);
                        //return;
                    }


                    carta.CFDI_EstatusCanc = Convert.ToInt32(valorResultadoCancelacion);
                    carta.CFDI_AcuseCanc = respuestaCancelacion;
                    int verificador1 = 0;
                    //new CN_CapCartaPorte().Actualiza_CFDI_AcuseCancelacionSAT(carta, sesion.Emp_Cnx, ref verificador1);
                }

                //ImprimirFactura(sesion.Id_Emp, sesion.Id_Cd, factura.Id_Fac, "", "CANCELACION", string.Concat("Canc. F-", factura.Id_Fac.ToString()), false);
                //if (carta.CFDI_Estatus != "B")
                //{ }
                //  rgFactura.Rebind();
                Alerta("Cfdi cancelado exitosamente.");
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        private byte[] Base64ToByte(string data)
        {
            byte[] filebytes = null;
            try
            {
                if (!string.IsNullOrEmpty(data))
                {
                    filebytes = Convert.FromBase64String(data);
                }
                return filebytes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ByteToTempPDF(string tempPath, byte[] filebytes)
        {
            if (File.Exists(tempPath))
            {
                File.Delete(tempPath);
            }
            FileStream fs = new FileStream(tempPath,
                FileMode.CreateNew,
                FileAccess.Write,
                FileShare.None);
            fs.Write(filebytes, 0, filebytes.Length);
            fs.Close();
        }

        protected void RAM1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            try
            {
                string cmd = e.Argument.ToString();
                switch (cmd)
                {
                    case "RebindGrid":
                        //Inicializar();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }

        protected void rtb1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            try
            {
                ErrorManager();
                RadToolBarButton btn = e.Item as RadToolBarButton;
                if (btn.CommandName == "save")
                {
                    //if (Page.IsValid)
                    //Guardar();
                }
                else if (btn.CommandName == "new")
                {
                    //Nuevo();
                }
                else if (btn.CommandName == "undo")
                {
                    //CerrarVentana();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "rtb1_ButtonClick");
            }
        }

        protected void Timbrar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (HF_Centro.Value != "")
                {
                    Alerta("No se puede realizar factura de un pago externo");
                }
            }
            catch (Exception ex)
            {

                ErrorManager(ex, "Timbrar_Click");
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //timbrardocumentos(cboMetodoPago.SelectedValue);
        }

        protected void RgDet_ItemCommand(object source, GridCommandEventArgs e)
        {
            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            int Id_Doc;
            int Id_Cfdi;
            string FolioFiscal;

            try
            {
                ErrorManager();
                Int32 item = default(Int32);
                if (e.Item == null) return;
                item = e.Item.ItemIndex;
                GridItem gi = e.Item;
                if (item >= 0)
                {

                    Int32.TryParse(gi.Cells[5].Text, out Id_Cfdi);
                    FolioFiscal = gi.Cells[6].Text;
                    string Estatus = gi.Cells[7].Text;
                    Int32.TryParse(txtdocumento.Text, out Id_Doc);
                    DateTime fechaPeriodoInicio = sesion.CalendarioIni;
                    DateTime fechaPeriodoFinal = sesion.CalendarioFin;

                    //Se valida si existe CFDI 
                    object CFDI_Xml = "NULL";
                    object CFDI_Pdf = "NULL";
                    int Id_CFDI = 0;
                    CN_CapRemision remision = new CN_CapRemision();
                    remision.Consulta_CartaPorteSAT(Id_Doc, Id_CFDI, sesion, ref CFDI_Pdf, ref CFDI_Xml, ref Id_CFDI);

                    switch (e.CommandName)
                    {
                        case "Historial":
                            if (Id_CFDI > 0)
                            {
                                RAM1.ResponseScripts.Add("return AbrirVentana_CFDITimbreDet('" + sesion.Id_Emp + "','" + sesion.Id_Cd_Ver + "','" + Id_Doc + "','" + txtTipoDoc.Text + "')");
                            }
                            else 
                            {
                                Alerta("Este documento no cuenta con historial de movimientos.");
                                return;
                            }
                            break;
                        case "PDF":
                            this.descargarPDF(Id_Doc, Id_Cfdi);
                            break;
                        case "XML":
                            this.descargarXML(Id_Doc, Id_Cfdi);
                            break;
                        case "CFDI":
                            if (ValidarCodigoPostal(txtcp.Text) == 0)
                            {
                                Alerta("Favor de proporcionar un valor valido para el código postal.");
                                return;
                            }
                            if (CmbColonia.SelectedValue == "-1")
                            {
                                Alerta("Es necesario seleccionar una colonia del listado sugerido por el sistema.");
                                return;
                            }
                            if (FolioFiscal != "&nbsp;" && _PermisoGuardar == true)
                            {
                                Alerta("Este documento ya cuenta con un Cfdi activo y no es posible generar otro.");
                                //this.descargarPDF(Id_Doc, Id_Cfdi);
                                //break;
                                return;
                            }
                            else if (_PermisoGuardar == false)
                            {
                                Alerta("La remisión o factura se encuentra en un estatus no valido para esta peración.");
                                return;
                            }
                            else
                            {
                                this.ImprimirCDFITraslado(sesion.Id_Emp, sesion.Id_Cd, Id_Doc, "TRASLADO");
                                break;
                            }
                        case "CartaPorte":
                            if (ValidarCodigoPostal(txtcp.Text) == 0)
                            {
                                Alerta("Favor de proporcionar un valor valido para el código postal.");
                                return;
                            }
                            if (CmbColonia.SelectedValue ==  "-1")
                            {
                                Alerta("Es necesario seleccionar una colonia del listado sugerido por el sistema.");
                                return;
                            }
                            if (FolioFiscal != "&nbsp;" && _PermisoGuardar == true)
                            {
                                Alerta("Este documento ya cuenta con una carta porte activa y no es posible generar otra.");
                                //this.descargarPDF(Id_Doc, Id_Cfdi);
                                //break;
                                return;
                            }
                            else if (_PermisoGuardar == false)
                            {
                                Alerta("La remisión o factura se encuentra en un estatus no valido para esta peración.");
                                return;
                            }
                            else
                            {
                                RAM1.ResponseScripts.Add("return AbrirVentana_CFDITraslado('" + txtdocumento.Text + "','" + txtCliente.Text + "','" + txtNombre.Text + "','" + txtcp.Text + "','" + CmbColonia.SelectedItem.Text + "' , '" + txtTipoDoc.Text + "')");
                                break;
                            }
                        case "Cancelar":
                            if (FolioFiscal == "&nbsp;")
                            {
                                Alerta("Este documento no puede ser cancelado, ya que no cuenta con un folio fiscal valido. ");
                            }
                            else
                            {
                                //if (_PermisoGuardar == false)
                                //{
                                //    Alerta("La remisión o factura se encuentra en un estatus no valido para esta operación.");
                                //    return;
                                //}

                                //CancelarCFDI(sesion.Id_Emp, sesion.Id_Cd_Ver, Id_Doc);
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        private int ValidarCodigoPostal(string codigopostal)
        {
            try
            {
                if (codigopostal == "" || codigopostal == "0")
                {
                    Alerta("Para continuar, favor de proporcuionar un valor valido para el campo codigo postal ejemplo: 56200.");
                    CmbColonia.Items.Clear();
                    CmbColonia.Text = "";
                    txtcp.Text = "";
                    txtcp.Focus();
                    return 0;
                }
                return 1;

            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }

        protected void RgDet_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                RgDet.Rebind();
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void RgDet_ItemCreated(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridEditableItem && e.Item.IsInEditMode)
                {

                    GridEditableItem item = e.Item as GridEditableItem;
                    (item.FindControl("rgReferencia") as RadTextBox).Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void RgDet_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (e.RebindReason == GridRebindReason.ExplicitRebind)
                {
                    //rgProductos.DataSource = this.objdtTablaProd;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }

        protected void RgDet_ItemDataBound(object sender, GridItemEventArgs e)
        {
            //if (e.Item is GridDataItem)
            //{
            //    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
            //    int Id_Cfdi = 0;
            //    string FolioFiscal = "";
            //    CartaPorte carta = new CartaPorte();
            //    carta.Id_Emp = sesion.Id_Emp;
            //    carta.Id_Cd = sesion.Id_Cd;
            //    carta.Id_Doc = int.Parse(txtdocumento.Text);
            //    carta.TipoDoc = txtTipoDoc.Text;

            //    DataSet dtCfdi = new DataSet();
            //    new CN_CapRemision().ConsultaCDFI(sesion, ref carta, ref dtCfdi);
            //    GridItem item = (GridItem)e.Item;

            //    //foreach (DataRow row in dtCfdi.Tables[0].Select("Cfdi_Estatus <> 'Cancelado' "))
            //    //{
            //    //    Id_Cfdi = int.Parse(row["Id_Cfdi"].ToString());
            //    //    FolioFiscal = row["CFDI_FolioFiscal"].ToString();
            //    //}

            //    //if (dtCfdi.Tables[0].Rows.Count == 1 && FolioFiscal == "")
            //    //{
            //    //    item.FindControl("gbcHistorial").Visible = true;
            //    //    item.FindControl("gbcCfdi").Visible = true;
            //    //    item.FindControl("gbcCartaPorte").Visible = true;
            //    //}

            //    //if (dtCfdi.Tables[0].Rows.Count == 1 && FolioFiscal != "")
            //    //{
            //    //    item.FindControl("gbcHistorial").Visible = false;
            //    //    item.FindControl("gbcCfdi").Visible = false;
            //    //    item.FindControl("gbcCartaPorte").Visible = false;

            //    //}

            //    //if (_PermisoGuardar == false) 
            //    //{
            //    //    item.FindControl("gbcCancelar").Visible = false;
            //    //    item.FindControl("gbcXML").Visible = false;
            //    //    item.FindControl("gbcPDF").Visible = false;
            //    //    item.FindControl("gbcCfdi").Visible = false;
            //    //    item.FindControl("gbcCartaPorte").Visible = false;
            //    //}
            //}
        }


        #endregion

        #region ErrorManager
        private void AlertaFocus(string mensaje, string rtb)
        {
            try
            {
                //RAM1.ResponseScripts.Add("AlertaFocus('" + mensaje + "','" + rtb + "');");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
        private void Alerta(string mensaje, int largo = 330, int ancho = 150)
        {
            try
            {
                RAM1.ResponseScripts.Add("radalert('" + mensaje + "', " + largo.ToString() + "," + ancho.ToString() + ");");
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


        protected void txtcp_TextChanged(object sender, EventArgs e)
        {
            Sesion sesion = new Sesion();
            sesion = (Sesion)Session["Sesion" + Session.SessionID];

            if (ValidarCodigoPostal(txtcp.Text) == 0)
                return;
            CargaColonias(int.Parse(txtcp.Text));

        }

        private void CargaColonias(int cp)
        {
            try
            {
                Sesion Sesion = new CapaEntidad.Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CmbColonia.Items.Clear();
                CmbColonia.Text = "";
                System.Collections.Generic.List<Comun> Lista = new System.Collections.Generic.List<Comun>();
                CapaNegocios.CN__Comun comun = new CN__Comun();
                comun.LlenaCombo(cp, Sesion.Emp_Cnx, "spCatColonias_Combo", ref CmbColonia);
                if (Lista.Count > 0)
                {
                    CmbColonia.DataSource = Lista;
                    CmbColonia.DataValueField = "Id";
                    CmbColonia.DataTextField = "Descripcion";
                    CmbColonia.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}