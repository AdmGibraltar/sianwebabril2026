using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocios;
using System.Configuration;
using Telerik.Web.UI;
using System.Text;
using System.Net;
using System.IO;
using System.Collections;
using System.Xml;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net.Mime;


namespace SIANWEB
{
    public partial class Ventana_EnviarDocumentos : System.Web.UI.Page
    {
        #region Variables 

        string URLtempPDF;
        string URLtempXML;

        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    this.LblTipo.Text = Page.Request.QueryString["Tipo"].ToString();
                    this.LblDocumento.Text = Page.Request.QueryString["Id_Doc"].ToString();
                    this.HFId_Emp.Value = Page.Request.QueryString["Id_Emp"].ToString();
                    this.HFId_Cd.Value = Page.Request.QueryString["Id_Cd"].ToString();
                    ConsultaCorreo();

                }

            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }
        }
        protected void btnEnviar_Click(object sender, EventArgs e)
        {

            try
            {
                ObtenerPDFXML();
                EnviarCorreo();

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


            }
            catch (Exception ex)
            {
                ErrorManager(ex, "RAM1_AjaxRequest");
            }
        }
        #endregion

        #region Funciones
        private void ConsultaCorreo()
        {
            try
            {
                Clientes cte = new Clientes();
                CN_CatCliente cn_cte = new CN_CatCliente();
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

                cn_cte.ConsultaClienteCorrreos(int.Parse(HFId_Cd.Value), int.Parse(this.LblDocumento.Text), ref cte, sesion.Emp_Cnx);

                this.LblId_CteStr.Text = cte.Cte_NomComercial;
                this.TxtCorreos.Text = cte.Cte_Email;


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        private void ObtenerPDFXML()
        {

            int verificador = 0;
            int Id_Emp = int.Parse(HFId_Emp.Value);
            int Id_Cd = int.Parse(HFId_Cd.Value);
            int Id_Fac = int.Parse(LblDocumento.Text);
            string movimiento = this.LblTipo.Text;
            string agregado_nota_cancelacion = "";

            Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];

            try
            {

                List<FacturaDet> listaFacturaDet = new List<FacturaDet>();
                CN_CapFactura cn_factura = new CN_CapFactura();
                Factura factura = new Factura();
                Factura facturaNacional = new Factura();
                factura.Id_Emp = sesion.Id_Emp;
                factura.Id_Cd = sesion.Id_Cd_Ver;
                factura.Id_Fac = Id_Fac;
                facturaNacional.Id_Emp = sesion.Id_Emp;
                facturaNacional.Id_Cd = sesion.Id_Cd_Ver;
                facturaNacional.Id_Fac = Id_Fac;

                cn_factura.ConsultaFactura(ref factura, ref listaFacturaDet, sesion.Emp_Cnx);
                cn_factura.ConsultaFacturaNacional(ref facturaNacional, sesion.Emp_Cnx);

                List<AdendaDet> listCabT = new List<AdendaDet>();
                List<AdendaDet> listDetT = new List<AdendaDet>();
                List<AdendaDet> listCabR = new List<AdendaDet>();
                List<AdendaDet> listDetR = new List<AdendaDet>();
                List<AdendaDet> listCabNacionalT = new List<AdendaDet>();
                List<AdendaDet> listDetNacionalT = new List<AdendaDet>();
                new CN_CapFactura().ConsultarAdenda(Id_Emp, Id_Cd, Id_Fac, "1", "2", ref listCabT, ref listDetT, sesion.Emp_Cnx);
                new CN_CapFactura().ConsultarAdenda(sesion.Id_Emp, sesion.Id_Cd_Ver, Id_Fac, "7", "8", ref listCabR, ref listDetR, sesion.Emp_Cnx);
                new CN_CapFactura().ConsultarAdendaNacional(Id_Emp, Id_Cd, Id_Fac, "1", "2", ref listCabNacionalT, ref listDetNacionalT, sesion.Emp_Cnx);
                // -------------------------------------------------------------------------------------------
                // Consulta productos de factura especial de la tabla 'CapFacturaEspecialDet' si esque la factura especial existe
                // esto es si es una actualización de factura --> si el parametro Folio trae un Id de factura
                // -------------------------------------------------------------------------------------------
                List<FacturaDet> listaProdFacturaEspecialFinal = new List<FacturaDet>();
                new CN_CapFactura().ConsultaFacturaEspecialDetalle(ref listaProdFacturaEspecialFinal
                    , sesion.Emp_Cnx
                    , Id_Emp
                    , Id_Cd
                    , Id_Fac
                    , factura.Id_Cte);
                // -------------------------------------------------------------------------------------------

                #region variable XML a enviar
                StringBuilder XML_Enviar = new StringBuilder();
                XML_Enviar.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                XML_Enviar.Append("<Comprobante");
                XML_Enviar.Append(" serie=\"\"");
                XML_Enviar.Append(" folio=\"\"");
                XML_Enviar.Append(" fecha=\"\"");
                XML_Enviar.Append(" formaDePago=\"\"");
                XML_Enviar.Append(" subTotal=\"\"");
                XML_Enviar.Append(" total=\"\"");

                XML_Enviar.Append(" tipoDeComprobante=\"\"");
                XML_Enviar.Append(" Sustituye=\"\"");
                XML_Enviar.Append(" tipoMovimiento=\"\""); //FACTURA,NOTA DE CARGO, NOTA DE CEDITO ,CANCELACION FACTURA,CANCELACION NOTA DE CARGO
                XML_Enviar.Append(" tipoMoneda=\"\""); //MN= MONEDA NACIONAL, MA = MONEDA AMERICANA depende del catalogo del SIAN
                XML_Enviar.Append(" tipoCambio=\"\""); //IMPORTE VIGENTE DEL CAMBIO DEPENDIENDO DEL TIPO DE MONEDA
                XML_Enviar.Append(" leyendaFacturaEspecial=\"\""); //LEYENDA DE FACTURA ESPECIAL: LOS DATOS DEL DETALLE REAL DE LA FACTURA PERO DELIMITADOS POR /
                XML_Enviar.Append(" movimientoacancelar=\"\""); //SI ES CANCELACION FACTURA HAY QUE INDICAR QUE FACTURA ESTA CANCELANDO APLICA LO MISMO PARA LA NOTA DE CARGO
                XML_Enviar.Append(" ConceptoDescuento1=\"\"");
                XML_Enviar.Append(" TasaDescuento1=\"\"");
                XML_Enviar.Append(" ConceptoDescuento2=\"\"");
                XML_Enviar.Append(" TasaDescuento2=\"\"");
                XML_Enviar.Append(" Notas=\"\"");
                XML_Enviar.Append(" Correo=\"\"");
                XML_Enviar.Append(" CliNum=\"\"");

                XML_Enviar.Append(" MetodoPago=\"\"");
                XML_Enviar.Append(" CuentaBancaria=\"\"");
                XML_Enviar.Append(" Referencia=\"\"");
                XML_Enviar.Append(" ComprobanteVersion=\"\"");
                XML_Enviar.Append(">");
                XML_Enviar.Append(" <Emisor");
                XML_Enviar.Append(" rfc=\"\"");
                XML_Enviar.Append(" numero=\"\" />");
                XML_Enviar.Append(" <Receptor");
                XML_Enviar.Append(" rfc=\"\"");
                XML_Enviar.Append(" nombre=\"\"");
                XML_Enviar.Append(" UsoCFDI=\"\">");
                XML_Enviar.Append(" <Domicilio");
                XML_Enviar.Append(" calle=\"\"");
                XML_Enviar.Append(" noExterior=\"\"");
                XML_Enviar.Append(" colonia=\"\"");
                XML_Enviar.Append(" municipio=\"\"");
                XML_Enviar.Append(" estado=\"\"");
                XML_Enviar.Append(" pais=\"\"");
                XML_Enviar.Append(" codigoPostal=\"\" />");
                XML_Enviar.Append(" </Receptor>");
                XML_Enviar.Append(" <Conceptos>");
                XML_Enviar.Append(" <Concepto");
                XML_Enviar.Append(" ClaveProdServ=\"\"");
                XML_Enviar.Append(" ClaveUnidad=\"\"");
                XML_Enviar.Append(" cantidad=\"\"");
                XML_Enviar.Append(" noIdentificacion=\"\"");
                XML_Enviar.Append(" descripcion=\"\"");
                XML_Enviar.Append(" valorUnitario=\"\"");
                XML_Enviar.Append(" importe=\"\" />");
                XML_Enviar.Append(" </Conceptos>");
                XML_Enviar.Append(" <Impuestos");
                XML_Enviar.Append(" totalImpuestosTrasladados=\"\">");
                XML_Enviar.Append(" <Traslados>");
                XML_Enviar.Append(" <Traslado");
                XML_Enviar.Append(" impuesto=\"\"");
                XML_Enviar.Append(" tasa=\"\"");
                XML_Enviar.Append(" importe=\"\" />");
                XML_Enviar.Append(" </Traslados>");

                if ((factura.Fac_RetIva == true) && (factura.Fac_ImporteRetencion > 0))
                {
                    XML_Enviar.Append(" <Retenidos>");
                    XML_Enviar.Append(" <Retenido");
                    XML_Enviar.Append(" importe=\"\"");
                    XML_Enviar.Append(" impuesto=\"\" />");
                    XML_Enviar.Append(" </Retenidos>");
                }
                XML_Enviar.Append(" </Impuestos>");

                XML_Enviar.Append(" <Addenda>");

                //ADENDA CABECERA
                XML_Enviar.Append(" <cabecera");
                XML_Enviar.Append(" Pedido=\"\"");
                XML_Enviar.Append(" Requisicion=\"\"");
                XML_Enviar.Append(" consignarRenglon1=\"\"");
                XML_Enviar.Append(" consignarRenglon2=\"\"");
                XML_Enviar.Append(" consignarRenglon3=\"\"");
                XML_Enviar.Append(" consignarRenglon4=\"\"");
                XML_Enviar.Append(" consignarRenglon5=\"\"");
                XML_Enviar.Append(" Conducto=\"\"");
                XML_Enviar.Append(" CondicionesPago=\"\"");
                XML_Enviar.Append(" NumeroGuia=\"\"");
                XML_Enviar.Append(" ControlPedido=\"\"");
                XML_Enviar.Append(" OrdenEmbarque=\"\"");
                XML_Enviar.Append(" Zona=\"\"");
                XML_Enviar.Append(" Territorio=\"\"");
                XML_Enviar.Append(" Agente=\"\"");
                XML_Enviar.Append(" NumeroDocumentoAduanero=\"\"");
                XML_Enviar.Append(" Formulo=\"\"");
                XML_Enviar.Append(" Autorizo=\"\"");
                XML_Enviar.Append(" FormaRevision=\"\"");
                XML_Enviar.Append(" Logistica=\"\"");
                XML_Enviar.Append(" NombreAddenda=\"\"");
                foreach (AdendaDet det in listCabT)
                {
                    XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                }
                foreach (AdendaDet det in listCabR)
                {
                    XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                }
                XML_Enviar.Append("/>");


                //ADENDA DETALLE
                if (listaProdFacturaEspecialFinal.Count > 0)
                {
                    foreach (FacturaDet fd in listaProdFacturaEspecialFinal)
                    {
                        XML_Enviar.Append(" <Detalle");
                        XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                        XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                        XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");

                        string primerNodo = "";
                        int primerfila = 0;
                        foreach (AdendaDet det in listDetT)
                        {

                            if (fd.Id_Prd == det.Id_Prd)
                            {
                                if (primerfila == 0)
                                { // COPIAMOS EL NOMBRE DEL PRIMER NODO PARA COMPARAR CUANDO INICIE UNA NUEVA ADENDA
                                    primerNodo = det.Nodo;
                                }
                                if (primerfila > 0 && det.Nodo == primerNodo)
                                {
                                    XML_Enviar.Append("/>");//CERRAMOS LA ADENDA
                                    // ABRIMOS UNA NUEVA ADENDA
                                    XML_Enviar.Append(" <Detalle");
                                    XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                                    XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                                    XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");
                                }

                                XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                                primerfila++;
                            }
                        }

                        primerNodo = "";
                        primerfila = 0;
                        foreach (AdendaDet det in listDetR)
                        {

                            if (fd.Id_Prd == det.Id_Prd)
                            {
                                if (primerfila == 0)
                                { // COPIAMOS EL NOMBRE DEL PRIMER NODO PARA COMPARAR CUANDO INICIE UNA NUEVA ADENDA
                                    primerNodo = det.Nodo;
                                }
                                if (primerfila > 0 && det.Nodo == primerNodo)
                                {
                                    XML_Enviar.Append("/>");//CERRAMOS LA ADENDA
                                    // ABRIMOS UNA NUEVA ADENDA
                                    XML_Enviar.Append(" <Detalle");
                                    XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                                    XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                                    XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");
                                }

                                XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                                primerfila++;
                            }
                        }

                        XML_Enviar.Append("/>");
                    }
                }
                else
                {
                    //NUEVO METODO PARA IMPRIMIR DETALLES DE ADENDA
                    //NUEVO METODO PARA IMPRIMIR DETALLES DE ADENDA
                    foreach (FacturaDet fd in listaFacturaDet)
                    {
                        XML_Enviar.Append(" <Detalle");
                        XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                        XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                        XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");

                        string primerNodo = "";
                        int primerfila = 0;
                        foreach (AdendaDet det in listDetT)
                        {

                            if (fd.Id_Prd == det.Id_Prd)
                            {
                                if (primerfila == 0)
                                { // COPIAMOS EL NOMBRE DEL PRIMER NODO PARA COMPARAR CUANDO INICIE UNA NUEVA ADENDA
                                    primerNodo = det.Nodo;
                                }
                                if (primerfila > 0 && det.Nodo == primerNodo)
                                {
                                    XML_Enviar.Append("/>");//CERRAMOS LA ADENDA
                                    // ABRIMOS UNA NUEVA ADENDA
                                    XML_Enviar.Append(" <Detalle");
                                    XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                                    XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                                    XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");
                                }

                                XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                                primerfila++;
                            }
                        }

                        primerNodo = "";
                        primerfila = 0;
                        foreach (AdendaDet det in listDetR)
                        {

                            if (fd.Id_Prd == det.Id_Prd)
                            {
                                if (primerfila == 0)
                                { // COPIAMOS EL NOMBRE DEL PRIMER NODO PARA COMPARAR CUANDO INICIE UNA NUEVA ADENDA
                                    primerNodo = det.Nodo;
                                }
                                if (primerfila > 0 && det.Nodo == primerNodo)
                                {
                                    XML_Enviar.Append("/>");//CERRAMOS LA ADENDA
                                    // ABRIMOS UNA NUEVA ADENDA
                                    XML_Enviar.Append(" <Detalle");
                                    XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                                    XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                                    XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");
                                }

                                XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                                primerfila++;
                            }
                        }

                        XML_Enviar.Append("/>");
                    }

                }
                XML_Enviar.Append(" </Addenda>");


                if (facturaNacional != null)
                {
                    if (movimiento != "CANCELACION")
                    {
                        //COMPROBANTE NACIONAL
                        XML_Enviar.Append(" <ComprobanteCN");
                        XML_Enviar.Append(" CliNum=\"\"");
                        XML_Enviar.Append(">");
                        XML_Enviar.Append(" <Conceptos>");
                        XML_Enviar.Append(" <Concepto");
                        XML_Enviar.Append(" cantidad=\"\"");
                        XML_Enviar.Append(" noIdentificacion=\"\"");
                        XML_Enviar.Append(" descripcion=\"\"");
                        XML_Enviar.Append(" valorUnitario=\"\"");
                        XML_Enviar.Append(" importe=\"\" />");
                        XML_Enviar.Append(" </Conceptos>");

                        //ADENDA NACIONAL
                        XML_Enviar.Append(" <AddendaCN>");

                        //ADENDA NACIONAL CABECERA
                        XML_Enviar.Append(" <CabeceraCN");
                        XML_Enviar.Append(" Pedido=\"\"");
                        XML_Enviar.Append(" Requisicion=\"\"");
                        XML_Enviar.Append(" consignarRenglon1=\"\"");
                        XML_Enviar.Append(" consignarRenglon2=\"\"");
                        XML_Enviar.Append(" consignarRenglon3=\"\"");
                        XML_Enviar.Append(" consignarRenglon4=\"\"");
                        XML_Enviar.Append(" consignarRenglon5=\"\"");
                        XML_Enviar.Append(" Conducto=\"\"");
                        XML_Enviar.Append(" CondicionesPago=\"\"");
                        XML_Enviar.Append(" NumeroGuia=\"\"");
                        XML_Enviar.Append(" ControlPedido=\"\"");
                        XML_Enviar.Append(" OrdenEmbarque=\"\"");
                        XML_Enviar.Append(" Zona=\"\"");
                        XML_Enviar.Append(" Territorio=\"\"");
                        XML_Enviar.Append(" Agente=\"\"");
                        XML_Enviar.Append(" NumeroDocumentoAduanero=\"\"");
                        XML_Enviar.Append(" Formulo=\"\"");
                        XML_Enviar.Append(" Autorizo=\"\"");

                        XML_Enviar.Append(" NombreAddenda=\"\"");
                        foreach (AdendaDet det in listCabNacionalT)
                        {
                            XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                        }
                        XML_Enviar.Append("/>");


                        //ADENDA NACIONAL DETALLE

                        //NUEVO METODO PARA IMPRIMIR DETALLES DE ADENDA
                        //NUEVO METODO PARA IMPRIMIR DETALLES DE ADENDA
                        foreach (FacturaDet fd in listaFacturaDet)
                        {
                            XML_Enviar.Append(" <Detalle");
                            XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                            XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                            XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");

                            string primerNodo = "";
                            int primerfila = 0;
                            foreach (AdendaDet det in listDetNacionalT)
                            {

                                if (fd.Id_Prd == det.Id_Prd)
                                {
                                    if (primerfila == 0)
                                    { // COPIAMOS EL NOMBRE DEL PRIMER NODO PARA COMPARAR CUANDO INICIE UNA NUEVA ADENDA
                                        primerNodo = det.Nodo;
                                    }
                                    if (primerfila > 0 && det.Nodo == primerNodo)
                                    {
                                        XML_Enviar.Append("/>");//CERRAMOS LA ADENDA
                                        // ABRIMOS UNA NUEVA ADENDA
                                        XML_Enviar.Append(" <Detalle");
                                        XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                                        XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                                        XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");
                                    }

                                    XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                                    primerfila++;
                                }
                            }

                            XML_Enviar.Append("/>");
                        }

                        XML_Enviar.Append(" </AddendaCN>");

                        XML_Enviar.Append(" </ComprobanteCN>");
                    }
                    else
                    {
                        XML_Enviar.Append("<ComprobanteCN UUID=\"" + factura.Fac_FolioFiscalCN + "\" Folio=\"" + factura.Fac_FolioCN.ToString() + "\" Serie=\"" + factura.Serie + "\" />");
                        facturaNacional = null;
                    }
                }
                XML_Enviar.Append(" </Comprobante>");


                //TERMINA NUEVO METODO PARA IMPRIMIR DETALLES DE ADENDA
                //TERMINA NUEVO METODO PARA IMPRIMIR DETALLES DE ADENDA

                //foreach (FacturaDet fd in listaFacturaDet)
                //{
                //    XML_Enviar.Append(" <Detalle");
                //    XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                //    XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                //    XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\""); 
                //    foreach (AdendaDet det in listDetT)
                //    {
                //        if (fd.Id_Prd == det.Id_Prd)
                //        {
                //            XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                //        }
                //    }
                //    foreach (AdendaDet det in listDetR)
                //    {
                //        if (fd.Id_Prd == det.Id_Prd)
                //        {
                //            XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                //        }
                //    }
                //    XML_Enviar.Append("/>");
                //}






                #endregion

                #region Codigo pruebas

                //PruebaServicio.Service1 servicio = new PruebaServicio.Service1();
                //float suma = servicio.Suma(Convert.ToSingle(txtNumero1.Text), Convert.ToSingle(txtNumero2.Text));
                //this.Alerta(suma.ToString());

                //Uri objURI = new Uri("");
                //WebRequest objWebRequest = WebRequest.Create(objURI);
                //WebResponse objWebResponse = objWebRequest.GetResponse();
                //Stream objStream = objWebResponse.GetResponseStream();
                //StreamReader objStreamReader = new StreamReader(objStream);
                //string responseText = objStreamReader.ReadToEnd();

                #endregion

                // --------------------------------------
                // Consulta centro de distribución
                // --------------------------------------
                CentroDistribucion Cd = new CentroDistribucion();
                new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref Cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);
                // --------------------------------------------------------------------
                // Consulta detalle de factura para generar lista de productos
                // --------------------------------------------------------------------
                //if (factura.Fac_Sello != "" && factura.Fac_Sello != null && movimiento == "FACTURA")
                //{
                //    //Abre el XML y carga el PDF de la factura
                //    object resultado = null;
                //    cn_factura.ConsultarFacturaSAT(ref factura, sesion.Emp_Cnx, ref resultado);
                //    byte[] archivoPdf = (byte[])resultado;
                //    if (archivoPdf.Length > 0)
                //    {
                //        string tempPDFname = string.Concat("FACTURA_"
                //                 , factura.Id_Emp.ToString()
                //                 , "_", factura.Id_Cd.ToString()
                //                 , "_", factura.Id_U.ToString()
                //                 , ".pdf");
                //        string URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                //        string WebURLtempPDF = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFname);

                //        this.ByteToTempPDF(URLtempPDF, archivoPdf);
                //        // ------------------------------------------------------------------------------------------------
                //        // Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                //        // ------------------------------------------------------------------------------------------------

                //        RAM1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDF('", WebURLtempPDF, "')"));
                //    }
                //    else
                //        this.DisplayMensajeAlerta("TempPDFNoData");
                //}
                //else
                //{
                // --------------------------------------
                // cargar xml de factura que se envia a SAT
                // --------------------------------------
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(XML_Enviar.ToString());

                // --------------------------------------//
                // --------------------------------------//
                //         LLENAR DATOS DEL XML          //
                // --------------------------------------//
                // --------------------------------------//
                #region Llenar datos factura a Enviar
                //encabezado
                XmlNode Comprobante = xml.SelectSingleNode("Comprobante");
                Clientes cliente = new Clientes();
                cliente.Id_Emp = factura.Id_Emp;
                cliente.Id_Cd = factura.Id_Cd;
                cliente.Id_Cte = factura.Id_Cte;
                new CN_CatCliente().ConsultaClientes(ref cliente, sesion.Emp_Cnx);

                Comprobante.Attributes["serie"].Value = factura.Serie;
                Comprobante.Attributes["folio"].Value = factura.Folio_cancelacion > 0 ? factura.Folio_cancelacion.ToString() : factura.Id_Fac.ToString();
                Comprobante.Attributes["fecha"].Value = string.Format("{0:s}", factura.Fac_Fecha);
                Comprobante.Attributes["formaDePago"].Value = cliente.Cte_MetodoPago;/*"PAGO EN UNA SOLA EXHIBICION"*/;
                Comprobante.Attributes["subTotal"].Value = factura.Fac_SubTotal == null ? "0" : Math.Round((double)factura.Fac_SubTotal, 2).ToString();
                Comprobante.Attributes["total"].Value = (Math.Round((double)factura.Fac_SubTotal, 2) + Math.Round((double)factura.Fac_ImporteIva, 2)).ToString();
                Comprobante.Attributes["tipoDeComprobante"].Value = "ingreso";
                Comprobante.Attributes["Sustituye"].Value = factura.Fac_Refactura;
                Comprobante.Attributes["tipoMovimiento"].Value = factura.Folio_cancelacion > 0 ? "Cancelacion" : movimiento;
                Comprobante.Attributes["tipoMoneda"].Value = factura.Mon_Unidad;
                Comprobante.Attributes["tipoCambio"].Value = factura.Mon_TipCambio.ToString();
                Comprobante.Attributes["leyendaFacturaEspecial"].Value = ""; //
                Comprobante.Attributes["movimientoacancelar"].Value = ""; //

                Comprobante.Attributes["ConceptoDescuento1"].Value = factura.Fac_Desc1;
                Comprobante.Attributes["TasaDescuento1"].Value = factura.Fac_DescPorcen1 == null ? string.Empty : factura.Fac_DescPorcen1.ToString();
                Comprobante.Attributes["ConceptoDescuento2"].Value = factura.Fac_Desc2;
                Comprobante.Attributes["TasaDescuento2"].Value = factura.Fac_DescPorcen2 == null ? string.Empty : factura.Fac_DescPorcen2.ToString();
                Comprobante.Attributes["Correo"].Value = factura.Cte_Email;
                Comprobante.Attributes["CliNum"].Value = factura.Id_Cte.ToString();

                Comprobante.Attributes["MetodoPago"].Value = "00".Substring(1, 2 - factura.Fac_FPago.Trim().Length) + factura.Fac_FPago.Trim();
                Comprobante.Attributes["CuentaBancaria"].Value = factura.Fac_UDigitos.ToString();
                Comprobante.Attributes["Referencia"].Value = cliente.Cte_Referencia;
                Comprobante.Attributes["ComprobanteVersion"].Value = "3.3";
                XmlNode Emisor = Comprobante.SelectSingleNode("Emisor");
                Emisor.Attributes["rfc"].Value = Cd.Cd_Rfc;
                Emisor.Attributes["numero"].Value = Cd.Cd_Numero;
                //receptor
                XmlNode Receptor = Comprobante.SelectSingleNode("Receptor");
                Receptor.Attributes["rfc"].Value = factura.Fac_CteRfc;
                Receptor.Attributes["nombre"].Value = factura.Cte_NomComercial;
                Receptor.Attributes["UsoCFDI"].Value = cliente.Cte_UsoCFDI;


                //Domicilio
                XmlNode Domicilio = Receptor.SelectSingleNode("Domicilio");
                Domicilio.Attributes["calle"].Value = HttpUtility.HtmlEncode(cliente.Cte_FacCalle); // factura.Fac_CteCalle.Replace("\"", "");
                Domicilio.Attributes["noExterior"].Value = HttpUtility.HtmlEncode(cliente.Cte_FacNumero);// factura.Fac_CteNumero;
                Domicilio.Attributes["colonia"].Value = HttpUtility.HtmlEncode(cliente.Cte_FacColonia);// factura.Fac_CteColonia;
                Domicilio.Attributes["municipio"].Value = HttpUtility.HtmlEncode(cliente.Cte_FacMunicipio);// factura.Fac_CteMunicipio;
                Domicilio.Attributes["estado"].Value = HttpUtility.HtmlEncode(cliente.Cte_FacEstado);// factura.Fac_CteEstado;
                Domicilio.Attributes["pais"].Value = "México";
                Domicilio.Attributes["codigoPostal"].Value = cliente.Cte_FacCp;// factura.Fac_CteCp;
                // ---------------------
                // Conceptos --> partidas = producto
                // Detalle --> productoDetalle
                // ---------------------         
                XmlNode Conceptos = Comprobante.SelectSingleNode("Conceptos");
                XmlNode producto = Conceptos.SelectSingleNode("Concepto");
                XmlNode Addenda = Comprobante.SelectSingleNode("Addenda");



                XmlNode ComprobanteCN = Comprobante.SelectNodes("ComprobanteCN").Count > 0 ? Comprobante.SelectSingleNode("ComprobanteCN") : null;
                XmlNode AddendaCN = ComprobanteCN != null ? ComprobanteCN.SelectSingleNode("AddendaCN") : null;
                XmlNode ConceptosCN = ComprobanteCN != null ? ComprobanteCN.SelectSingleNode("Conceptos") : null;
                XmlNode productoCN = ConceptosCN != null ? ConceptosCN.SelectSingleNode("Concepto") : null;



                if (facturaNacional != null)
                {
                    ComprobanteCN.Attributes["CliNum"].Value = facturaNacional != null ? facturaNacional.Id_Cte.ToString() : "0";
                }


                //Si existe una factura especial, en los nodos de conceptos del producto se pone
                //los productos de la factura especial
                //si no, se pone los datos de productos de la factura original
                StringBuilder NotaProductosOriginales = new StringBuilder();
                if (listaProdFacturaEspecialFinal.Count > 0)
                {
                    foreach (FacturaDet facturaDet in listaProdFacturaEspecialFinal)
                    {

                        XmlNode prd = producto.Clone();
                        prd.Attributes["noIdentificacion"].Value = facturaDet.Producto.Id_PrdEsp;
                        prd.Attributes["descripcion"].Value = facturaDet.Producto.Prd_DescripcionEspecial.Replace("\"", "");
                        prd.Attributes["cantidad"].Value = facturaDet.Fac_Cant.ToString();
                        prd.Attributes["valorUnitario"].Value = Math.Round(facturaDet.Fac_Precio, 2).ToString();
                        prd.Attributes["importe"].Value = Math.Round(facturaDet.Fac_Importe, 2).ToString();

                        prd.Attributes["ClaveProdServ"].Value = "01010101";
                        prd.Attributes["ClaveUnidad"].Value = "H87";
                        producto.ParentNode.AppendChild(prd);

                    }

                    foreach (FacturaDet facturaDet in listaFacturaDet)
                    {
                        NotaProductosOriginales.Append("/");
                        NotaProductosOriginales.Append(facturaDet.Id_Prd.ToString());
                        NotaProductosOriginales.Append("/");
                        NotaProductosOriginales.Append(Math.Round(facturaDet.Fac_Precio, 2).ToString());
                        NotaProductosOriginales.Append("/");
                        NotaProductosOriginales.Append(facturaDet.Fac_Cant.ToString());
                    }
                }
                else
                {
                    foreach (FacturaDet facturaDet in listaFacturaDet)
                    {
                        XmlNode prd = producto.Clone();
                        prd.Attributes["noIdentificacion"].Value = facturaDet.Id_Prd.ToString();
                        prd.Attributes["descripcion"].Value = facturaDet.Producto.Prd_Descripcion.Replace("\"", "");
                        prd.Attributes["cantidad"].Value = facturaDet.Fac_Cant.ToString();
                        prd.Attributes["valorUnitario"].Value = Math.Round(facturaDet.Fac_Precio, 2).ToString();
                        prd.Attributes["importe"].Value = Math.Round(facturaDet.Fac_Importe, 2).ToString();
                        prd.Attributes["ClaveProdServ"].Value = facturaDet.Fac_ClaveProdServ.ToString();
                        prd.Attributes["ClaveUnidad"].Value = facturaDet.Fac_ClaveUnidad.ToString();
                        producto.ParentNode.AppendChild(prd);
                        if (facturaNacional != null)
                        {
                            XmlNode prdCN = productoCN.Clone();
                            prdCN.Attributes["noIdentificacion"].Value = facturaDet.Id_Prd.ToString();
                            prdCN.Attributes["descripcion"].Value = facturaDet.Producto.Prd_Descripcion.Replace("\"", "");
                            prdCN.Attributes["cantidad"].Value = facturaDet.Fac_Cant.ToString();
                            prdCN.Attributes["valorUnitario"].Value = Math.Round(facturaDet.Fac_Precio, 2).ToString();
                            prdCN.Attributes["importe"].Value = Math.Round(facturaDet.Fac_Importe, 2).ToString();
                            productoCN.ParentNode.AppendChild(prdCN);
                        }

                    }
                }
                producto.ParentNode.RemoveChild(xml.SelectNodes("//Concepto").Item(0));

                if (facturaNacional != null)
                {
                    productoCN.ParentNode.RemoveChild(xml.SelectNodes("//ComprobanteCN//Conceptos//Concepto").Item(0));
                }



                //Impuestos
                XmlNode Impuestos = Comprobante.SelectSingleNode("Impuestos");
                Impuestos.Attributes["totalImpuestosTrasladados"].Value = factura.Fac_ImporteIva == null ? "0" : factura.Fac_ImporteIva.ToString();

                //Traslado (impuestos desgloce)
                XmlNode Traslados = Impuestos.SelectSingleNode("Traslados");
                XmlNode Traslado = Traslados.SelectSingleNode("Traslado");
                Traslado.Attributes["impuesto"].Value = "IVA";
                if (cliente.BPorcientoIVA == true)
                    Traslado.Attributes["tasa"].Value = cliente.PorcientoIVA.ToString();
                else
                    Traslado.Attributes["tasa"].Value = Cd.Cd_IvaPedidosFacturacion.ToString();
                Traslado.Attributes["importe"].Value = factura.Fac_ImporteIva == null ? "0" : Math.Round((double)factura.Fac_ImporteIva, 2).ToString();

                if ((factura.Fac_RetIva == true) && (factura.Fac_ImporteRetencion > 0))
                {
                    XmlNode Retenidos = Impuestos.SelectSingleNode("Retenidos");
                    XmlNode Retenido = Retenidos.SelectSingleNode("Retenido");
                    Retenido.Attributes["importe"].Value = factura.Fac_ImporteRetencion == null ? "0" : Math.Round((double)factura.Fac_ImporteRetencion, 2).ToString();
                    Retenido.Attributes["impuesto"].Value = "IVA";
                }

                //Addenda
                XmlNode cabecera = Addenda.SelectSingleNode("cabecera");
                cabecera.Attributes["Pedido"].Value = factura.Fac_PedNum == null ? string.Empty : factura.Fac_PedNum.ToString();
                cabecera.Attributes["Requisicion"].Value = factura.Fac_Req == null ? string.Empty : factura.Fac_Req.ToString();
                //consulta datos cliente                 
                cabecera.Attributes["consignarRenglon1"].Value = factura.Fac_Contacto;
                cabecera.Attributes["consignarRenglon2"].Value = string.Concat(factura.Fac_CteCalle.Replace("\"", ""), " ", factura.Fac_CteNumero);
                cabecera.Attributes["consignarRenglon3"].Value = factura.Fac_CteColonia;
                cabecera.Attributes["consignarRenglon4"].Value = string.Concat(factura.Fac_CteMunicipio, " ", factura.Fac_CteEstado, " ", factura.Fac_CteCp);
                cabecera.Attributes["consignarRenglon5"].Value = "México";
                cabecera.Attributes["Conducto"].Value = factura.Fac_Conducto;
                cabecera.Attributes["CondicionesPago"].Value = factura.Fac_CondEntrega;
                cabecera.Attributes["NumeroGuia"].Value = factura.Fac_NumeroGuia;
                cabecera.Attributes["ControlPedido"].Value = factura.Fac_PedNum == null ? string.Empty : factura.Fac_PedNum.ToString();
                cabecera.Attributes["OrdenEmbarque"].Value = factura.Id_Emb == null ? string.Empty : factura.Id_Emb.ToString();
                cabecera.Attributes["Zona"].Value = factura.Id_Cd.ToString(); //Cd.Cd_Descripcion;
                cabecera.Attributes["Territorio"].Value = factura.Id_Ter.ToString(); //factura.Ter_Nombre == null ? string.Empty : factura.Ter_Nombre;
                cabecera.Attributes["Agente"].Value = factura.Id_Rik == null ? string.Empty : factura.Id_Rik.ToString();
                cabecera.Attributes["NumeroDocumentoAduanero"].Value = factura.Fac_Req == null ? string.Empty : factura.Fac_Req.ToString();
                cabecera.Attributes["Formulo"].Value = factura.U_Nombre; //Cd.Cd_CobranzaPersonaFormula;
                //cabecera.Attributes["FormaRevision"].Value = cliente.Cte_FormaRevision; //Cd.Cd_CobranzaPersonaFormula;
                //cabecera.Attributes["Logistica"].Value = cliente.Cte_Logistica; //Cd.Cd_CobranzaPersonaFormula;

                cabecera.Attributes["Autorizo"].Value = Cd.Cd_CobranzaPersonaAutoriza;
                cabecera.Attributes["NombreAddenda"].Value = cliente.Ade_Nombre;


                //Addenda Nacional
                if (facturaNacional != null)
                {
                    XmlNode cabeceraCN = AddendaCN.SelectSingleNode("CabeceraCN");
                    cabeceraCN.Attributes["Pedido"].Value = factura.Fac_PedNum == null ? string.Empty : factura.Fac_PedNum.ToString();
                    cabeceraCN.Attributes["Requisicion"].Value = factura.Fac_Req == null ? string.Empty : factura.Fac_Req.ToString();
                    //consulta datos cliente                 
                    cabeceraCN.Attributes["consignarRenglon1"].Value = factura.Fac_Contacto;
                    cabeceraCN.Attributes["consignarRenglon2"].Value = string.Concat(facturaNacional.Fac_CteCalle.Replace("\"", ""), " ", facturaNacional.Fac_CteNumero);
                    cabeceraCN.Attributes["consignarRenglon3"].Value = facturaNacional.Fac_CteColonia;
                    cabeceraCN.Attributes["consignarRenglon4"].Value = string.Concat(facturaNacional.Fac_CteMunicipio, " ", facturaNacional.Fac_CteEstado, " ", facturaNacional.Fac_CteCp).Replace('É', 'E');
                    cabeceraCN.Attributes["consignarRenglon5"].Value = "Mexico";
                    cabeceraCN.Attributes["Conducto"].Value = factura.Fac_Conducto;
                    cabeceraCN.Attributes["CondicionesPago"].Value = factura.Fac_CondEntrega;
                    cabeceraCN.Attributes["NumeroGuia"].Value = factura.Fac_NumeroGuia;
                    cabeceraCN.Attributes["ControlPedido"].Value = factura.Fac_PedNum == null ? string.Empty : factura.Fac_PedNum.ToString();
                    cabeceraCN.Attributes["OrdenEmbarque"].Value = factura.Id_Emb == null ? string.Empty : factura.Id_Emb.ToString();
                    cabeceraCN.Attributes["Zona"].Value = factura.Id_Cd.ToString(); //Cd.Cd_Descripcion;
                    cabeceraCN.Attributes["Territorio"].Value = factura.Id_Ter.ToString(); //factura.Ter_Nombre == null ? string.Empty : factura.Ter_Nombre;
                    cabeceraCN.Attributes["Agente"].Value = factura.Id_Rik == null ? string.Empty : factura.Id_Rik.ToString();
                    cabeceraCN.Attributes["NumeroDocumentoAduanero"].Value = factura.Fac_Req == null ? string.Empty : factura.Fac_Req.ToString();
                    cabeceraCN.Attributes["Formulo"].Value = Cd.Cd_CobranzaPersonaFormula;
                    cabeceraCN.Attributes["Autorizo"].Value = Cd.Cd_CobranzaPersonaAutoriza;
                    cabeceraCN.Attributes["NombreAddenda"].Value = facturaNacional.Fac_CteAdeNombre;//cliente.Ade_Nombre;
                }


                Factura factura_remision = new Factura();
                factura_remision.Id_Emp = factura.Id_Emp;
                factura_remision.Id_Cd = factura.Id_Cd;
                factura_remision.Id_Fac = factura.Id_Fac;
                string agregado_nota = "";
                cn_factura.FacturaRemision_Nota(factura_remision, sesion.Emp_Cnx, ref agregado_nota);
                StringBuilder NotaCompleta = new StringBuilder();

                NotaCompleta.Append(agregado_nota + "//");
                NotaCompleta.Append(NotaProductosOriginales.ToString() + "//");
                NotaCompleta.Append(factura.Fac_Notas + "//");
                NotaCompleta.Append(agregado_nota_cancelacion);
                Comprobante.Attributes["Notas"].Value = NotaCompleta.ToString();

                /*
                if (!ValidaImpresionFactura(xml)) 
                {
                    Alerta("No se puede Imprimir Documento: Detalle de factura no coincide con total, Revise factura");
                    return;
                    
                }*/

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
                // -------- ----------------------------------------------
                XmlDocument xmlSAT = new XmlDocument();

                int TSAT = 1;

                XmlDocument xmlBD = new XmlDocument();

                if (factura.Fac_Xml != null && factura.Fac_Xml != "")
                {
                    xmlBD.LoadXml(factura.Fac_Xml.ToString());

                    foreach (XmlNode nodo in xmlBD.ChildNodes)
                    {
                        if (nodo.Name == "Comprobante")
                        {
                            TSAT = 1;
                        }
                        else if (nodo.Name == "cfdi:Comprobante")
                        {
                            TSAT = 2;

                        }
                    }
                }


                //sian_cfd.Service1 sianFacturacionElectronica = new sian_cfd.Service1();

                //                if (TSAT == 2 && tienePDF)
                //                {
                //                    descargarPDF(Id_Fac);
                //                    return;
                //                }

                WebReference.Service1 sianFacturacionElectronica = new WebReference.Service1();
                sianFacturacionElectronica.Url = ConfigurationManager.AppSettings["WS_CFDIImpresion"].ToString();
                object sianFacturacionElectronicaResult = sianFacturacionElectronica.ObtieneCFD(xmlString);

                if (movimiento == "CANCELACION")
                {
                    string XmLRegex = string.Empty;
                    XmLRegex = Regex.Replace(sianFacturacionElectronicaResult.ToString(), @"(?s)(?<=<cfdi:Addenda>).+?(?=</cfdi:Addenda>)", "");
                    XmLRegex = XmLRegex.Replace("<cfdi:Addenda>", "");
                    XmLRegex = XmLRegex.Replace("</cfdi:Addenda>", "");
                    xmlSAT.LoadXml(XmLRegex);
                }
                else
                {
                    xmlSAT.LoadXml(sianFacturacionElectronicaResult.ToString());
                }



                //System.IO.StreamWriter sws = null;
                //URLtempXML = Server.MapPath("xmlSAT") + "\\FACTURA_" + Id_Fac.ToString() + ".txt";

                //if (File.Exists(URLtempXML))
                //    File.Delete(URLtempXML);
                //if (File.Exists(Server.MapPath("xmlSAT") + "\\FACTURA_" + Id_Fac.ToString() + ".xml"))
                //    File.Delete(Server.MapPath("xmlSAT") + "\\FACTURA_" + Id_Fac.ToString() + ".xml");
                //sws = new System.IO.StreamWriter(URLtempXML, false, Encoding.UTF8);
                //sws.WriteLine(sianFacturacionElectronicaResult.ToString());
                //sws.Close();
                //File.Move(URLtempXML, Server.MapPath("xmlSAT") + "\\FACTURA_" + Id_Fac.ToString() + ".xml");
                //URLtempXML = Server.MapPath("xmlSAT") + "\\FACTURA_" + Id_Fac.ToString() + ".xml";




                //*********************************************//
                //* Procesar XML recibido de servicio de SAT  *//  aqui
                //*********************************************//
                string stringPDF = string.Empty;
                string stringPDFCN = string.Empty;
                string selloSAT = string.Empty;
                string selloSATCN = string.Empty;
                string folioFiscal = string.Empty;
                string folioFiscalCN = string.Empty;
                string errorNum = string.Empty;
                string errorText = string.Empty;
                string errorNumCN = string.Empty;
                string errorTextCN = string.Empty;
                string VersionCFDI = string.Empty;


                TSAT = 1;

                foreach (XmlNode nodoSistemaFacturacion in xmlSAT.ChildNodes)
                {
                    if (nodoSistemaFacturacion.Name == "Comprobante")
                    {
                        TSAT = 1;
                        selloSAT = nodoSistemaFacturacion.Attributes["sello"].Value;

                        foreach (XmlNode Nodo_nivel2 in nodoSistemaFacturacion.ChildNodes)
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

                                nodoSistemaFacturacion.RemoveChild(Nodo_nivel2);
                            }


                        }
                    }
                    else if (nodoSistemaFacturacion.Name == "cfdi:Comprobante")
                    {
                        TSAT = 2;
                        VersionCFDI = nodoSistemaFacturacion.Attributes["Version"].Value;
                        foreach (XmlNode Nodo_nivel2 in nodoSistemaFacturacion.ChildNodes)
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

                                nodoSistemaFacturacion.RemoveChild(Nodo_nivel2);
                            }

                            if (Nodo_nivel2.Name == "cfdi:Complemento")
                            {
                                foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                {
                                    if (Nodo_nivel3.Name == "tfd:TimbreFiscalDigital")
                                    {
                                        if (VersionCFDI == "3.2")
                                        {
                                            selloSAT = Nodo_nivel3.Attributes["selloSAT"].Value;
                                        }
                                        else
                                        {
                                            selloSAT = Nodo_nivel3.Attributes["SelloSAT"].Value;
                                        }
                                        folioFiscal = Nodo_nivel3.Attributes["UUID"].Value;
                                    }
                                }

                            }

                        }

                    }
                    if (nodoSistemaFacturacion.Name == "SistemaFacturacion")
                    {
                        foreach (XmlNode nodoXmlSAT in nodoSistemaFacturacion.ChildNodes)
                        {
                            if (nodoXmlSAT.Name == "ComprobanteCDIK")
                            {
                                foreach (XmlNode nodo in nodoXmlSAT.ChildNodes)
                                {
                                    if (nodo.Name == "Comprobante")
                                    {
                                        TSAT = 1;
                                        selloSAT = nodo.Attributes["sello"].Value;

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
                                        VersionCFDI = nodo.Attributes["Version"].Value;
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

                                                if (VersionCFDI == "3.2")
                                                {
                                                    selloSAT = Nodo_nivel3.Attributes["selloSAT"].Value;
                                                }
                                                else
                                                {
                                                    selloSAT = Nodo_nivel3.Attributes["SelloSAT"].Value;
                                                }

                                                folioFiscal = Nodo_nivel3.Attributes["UUID"].Value;
                                            }

                                        }

                                    }
                                }
                            }
                            else
                            {
                                if (nodoXmlSAT.Name == "ComprobanteKSL")
                                {
                                    foreach (XmlNode nodo in nodoXmlSAT.ChildNodes)
                                    {
                                        if (nodo.Name == "Comprobante")
                                        {
                                            TSAT = 1;
                                            selloSATCN = nodo.Attributes["sello"].Value;

                                            foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                                            {
                                                if (Nodo_nivel2.Name == "AddendaKey")
                                                {
                                                    foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                                    {
                                                        if (Nodo_nivel3.Name == "PDF")
                                                            stringPDFCN = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                                        if (Nodo_nivel3.Name == "ERROR")
                                                        {
                                                            errorNumCN = Nodo_nivel3.Attributes["Numero"].Value;
                                                            errorTextCN = Nodo_nivel3.Attributes["Texto"].Value;
                                                        }
                                                    }

                                                    nodo.RemoveChild(Nodo_nivel2);
                                                }


                                            }
                                        }
                                        else if (nodo.Name == "cfdi:Comprobante")
                                        {
                                            TSAT = 2;
                                            VersionCFDI = nodo.Attributes["Version"].Value;
                                            foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                                            {

                                                if (Nodo_nivel2.Name == "AddendaKey")
                                                {
                                                    foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                                    {
                                                        if (Nodo_nivel3.Name == "PDF")
                                                            stringPDFCN = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                                        if (Nodo_nivel3.Name == "ERROR")
                                                        {
                                                            errorNumCN = Nodo_nivel3.Attributes["Numero"].Value;
                                                            errorTextCN = Nodo_nivel3.Attributes["Texto"].Value;
                                                        }
                                                    }

                                                    nodo.RemoveChild(Nodo_nivel2);
                                                }

                                                if (Nodo_nivel2.Name == "cfdi:Complemento")
                                                {
                                                    XmlNode Nodo_nivel3;
                                                    Nodo_nivel3 = Nodo_nivel2.FirstChild;
                                                    if (VersionCFDI == "3.2")
                                                    {
                                                        selloSATCN = Nodo_nivel3.Attributes["selloSAT"].Value;
                                                    }
                                                    else
                                                    {
                                                        selloSATCN = Nodo_nivel3.Attributes["SelloSAT"].Value;
                                                    }
                                                    folioFiscalCN = Nodo_nivel3.Attributes["UUID"].Value;

                                                }

                                            }

                                        }
                                    }

                                }
                            }
                        }
                    }

                }



                if (errorNum != "0")
                {
                    this.Alerta(string.Concat(errorText.Replace("'", "\"")));

                    /* factura.Fac_Sello = selloSAT;
                     System.Data.SqlTypes.SqlXml sqlXml
                         = new System.Data.SqlTypes.SqlXml(new XmlTextReader(xmlSAT.OuterXml, XmlNodeType.Document, null));
                     factura.Fac_Xml = sqlXml;
                     factura.Fac_Pdf = this.Base64ToByte(stringPDF);
                     verificador = 0;
                     new CN_CapFactura().ModificarFacturaSAT(factura, sesion.Emp_Cnx, ref verificador);*/
                }
                else
                {

                    //ComprobanteSAT.RemoveChild(AddendaSAT);

                    if ((facturaNacional != null) && (errorNumCN != "0"))
                    {
                        this.Alerta(string.Concat(errorTextCN.Replace("'", "\"")));
                    }
                    else
                    {
                        factura.Fac_Sello = selloSAT;
                        factura.Fac_SelloCN = selloSATCN;

                        System.Data.SqlTypes.SqlXml sqlXml;
                        System.Data.SqlTypes.SqlXml sqlXmlCN;

                        if (xmlSAT.SelectNodes("SistemaFacturacion").Count > 0)
                        {
                            //sqlXml = sqlXml.Value.Replace("ComprobanteCDIK", "").;
                            sqlXml = new System.Data.SqlTypes.SqlXml(new XmlTextReader(xmlSAT.SelectSingleNode("//SistemaFacturacion//ComprobanteCDIK").OuterXml.Replace("<ComprobanteCDIK>", "").Replace("</ComprobanteCDIK>", ""), XmlNodeType.Document, null));
                            sqlXmlCN = new System.Data.SqlTypes.SqlXml(new XmlTextReader(xmlSAT.SelectSingleNode("//SistemaFacturacion//ComprobanteKSL").OuterXml.Replace("<ComprobanteKSL>", "").Replace("</ComprobanteKSL>", ""), XmlNodeType.Document, null));
                            factura.Fac_FolioCN = int.Parse(xmlSAT.SelectSingleNode("//SistemaFacturacion//ComprobanteKSL").FirstChild.Attributes["folio"].Value == string.Empty ? "0" : xmlSAT.SelectSingleNode("//SistemaFacturacion//ComprobanteKSL").FirstChild.Attributes["folio"].Value);
                        }
                        else
                        {
                            sqlXml = new System.Data.SqlTypes.SqlXml(new XmlTextReader(xmlSAT.OuterXml, XmlNodeType.Document, null));
                            sqlXmlCN = null;
                            factura.Fac_FolioCN = null;
                        }


                        if (movimiento != "CANCELACION")
                        {

                            factura.Fac_Xml = sqlXml;
                            factura.Fac_XmlCN = sqlXmlCN;
                            factura.Fac_FolioFiscal = folioFiscal;
                            factura.Fac_FolioFiscalCN = folioFiscalCN;

                            //AOG Enviar el XML sin nodo addenda
                            System.IO.StreamWriter sws = null;
                            URLtempXML = Server.MapPath("xmlSAT") + "\\FACTURA_" + Id_Fac.ToString() + ".txt";

                            if (File.Exists(URLtempXML))
                                File.Delete(URLtempXML);
                            if (File.Exists(Server.MapPath("xmlSAT") + "\\FACTURA_" + Id_Fac.ToString() + ".xml"))
                                File.Delete(Server.MapPath("xmlSAT") + "\\FACTURA_" + Id_Fac.ToString() + ".xml");
                            sws = new System.IO.StreamWriter(URLtempXML, false, Encoding.UTF8);
                            sws.WriteLine(sqlXml.Value.ToString());
                            sws.Close();
                            sws.Dispose();
                            File.Move(URLtempXML, Server.MapPath("xmlSAT") + "\\FACTURA_" + Id_Fac.ToString() + ".xml");
                            URLtempXML = Server.MapPath("xmlSAT") + "\\FACTURA_" + Id_Fac.ToString() + ".xml";
                        }

                        factura.Fac_Pdf = this.Base64ToByte(stringPDF);
                        factura.Fac_PdfCN = this.Base64ToByte(stringPDFCN);

                        #region reporte factura


                        #endregion

                        // ---------------------------------------------------------------------------------------------
                        // Se actualiza el estatus de la factura a Impreso (I)
                        // ---------------------------------------------------------------------------------------------
                        if (movimiento != "CANCELACION")
                        {
                            factura.Fac_Estatus = "I";
                            new CN_CapFactura().ModificarFacturaSAT(factura, sesion.Emp_Cnx, ref verificador);
                        }
                        else
                        {
                            factura.Fac_Estatus = "B";
                        }
                        verificador = 0;


                        // -----------------------
                        // Abrir PDF de factura
                        // -----------------------
                        string tempPDFname = string.Concat("FACTURA_", factura.Id_Fac.ToString(), ".pdf");
                        URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                        string WebURLtempPDF = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFname);



                        string tempPDFCNname = string.Concat("FACTURACN_", factura.Id_Emp.ToString(), "_", factura.Id_Cd.ToString(), "_", factura.Id_Fac.ToString(), ".pdf");
                        string URLtempPDFCN = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFCNname));
                        string WebURLtempPDFCN = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFCNname);

                        this.ByteToTempPDF(URLtempPDF, this.Base64ToByte(stringPDF));
                    }
                    //}
                }


            }
            //}

            catch (Exception ex)
            {
                throw ex;
            }

        }
        /*        private void ObtenerPDFXML()
                {
                    Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                    try
                    {
                        int verificador = 0;
                        int Id_Emp = int.Parse(HFId_Emp.Value);
                        int Id_Cd = int.Parse(HFId_Cd.Value);
                        int Id_Fac = int.Parse(LblDocumento.Text);
                        string movimiento = this.LblTipo.Text;
                        string agregado_nota_cancelacion = "";
                        List<FacturaDet> listaFacturaDet = new List<FacturaDet>();
                        CN_CapFactura cn_factura = new CN_CapFactura();
                        Factura factura = new Factura();
                        factura.Id_Emp = sesion.Id_Emp;
                        factura.Id_Cd = sesion.Id_Cd_Ver;
                        factura.Id_Fac = Id_Fac;
                        cn_factura.ConsultaFactura(ref factura, ref listaFacturaDet, sesion.Emp_Cnx);
                        List<AdendaDet> listCabT = new List<AdendaDet>();
                        List<AdendaDet> listDetT = new List<AdendaDet>();
                        List<AdendaDet> listCabR = new List<AdendaDet>();
                        List<AdendaDet> listDetR = new List<AdendaDet>();
                        new CN_CapFactura().ConsultarAdenda(Id_Emp, Id_Cd, Id_Fac, "1", "2", ref listCabT, ref listDetT, sesion.Emp_Cnx);
                        new CN_CapFactura().ConsultarAdenda(sesion.Id_Emp, sesion.Id_Cd_Ver, Id_Fac, "7", "8", ref listCabR, ref listDetR, sesion.Emp_Cnx);
                        // -------------------------------------------------------------------------------------------
                        // Consulta productos de factura especial de la tabla 'CapFacturaEspecialDet' si esque la factura especial existe
                        // esto es si es una actualización de factura --> si el parametro Folio trae un Id de factura
                        // -------------------------------------------------------------------------------------------
                        List<FacturaDet> listaProdFacturaEspecialFinal = new List<FacturaDet>();
                        new CN_CapFactura().ConsultaFacturaEspecialDetalle(ref listaProdFacturaEspecialFinal
                            , sesion.Emp_Cnx
                            , Id_Emp
                            , Id_Cd
                            , Id_Fac
                            , factura.Id_Cte);
                        // -------------------------------------------------------------------------------------------
                        #region variable XML a enviar
                        StringBuilder XML_Enviar = new StringBuilder();
                        XML_Enviar.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                        XML_Enviar.Append("<Comprobante");
                        XML_Enviar.Append(" serie=\"\"");
                        XML_Enviar.Append(" folio=\"\"");
                        XML_Enviar.Append(" fecha=\"\"");
                        XML_Enviar.Append(" formaDePago=\"\"");
                        XML_Enviar.Append(" subTotal=\"\"");
                        XML_Enviar.Append(" total=\"\"");
                        XML_Enviar.Append(" tipoDeComprobante=\"\"");
                        XML_Enviar.Append(" Sustituye=\"\"");
                        XML_Enviar.Append(" tipoMovimiento=\"\""); //FACTURA,NOTA DE CARGO, NOTA DE CEDITO ,CANCELACION FACTURA,CANCELACION NOTA DE CARGO
                        XML_Enviar.Append(" tipoMoneda=\"\""); //MN= MONEDA NACIONAL, MA = MONEDA AMERICANA depende del catalogo del SIAN
                        XML_Enviar.Append(" tipoCambio=\"\""); //IMPORTE VIGENTE DEL CAMBIO DEPENDIENDO DEL TIPO DE MONEDA
                        XML_Enviar.Append(" leyendaFacturaEspecial=\"\""); //LEYENDA DE FACTURA ESPECIAL: LOS DATOS DEL DETALLE REAL DE LA FACTURA PERO DELIMITADOS POR /
                        XML_Enviar.Append(" movimientoacancelar=\"\""); //SI ES CANCELACION FACTURA HAY QUE INDICAR QUE FACTURA ESTA CANCELANDO APLICA LO MISMO PARA LA NOTA DE CARGO
                        XML_Enviar.Append(" ConceptoDescuento1=\"\"");
                        XML_Enviar.Append(" TasaDescuento1=\"\"");
                        XML_Enviar.Append(" ConceptoDescuento2=\"\"");
                        XML_Enviar.Append(" TasaDescuento2=\"\"");
                        XML_Enviar.Append(" Notas=\"\"");
                        XML_Enviar.Append(" Correo=\"\"");
                        XML_Enviar.Append(" CliNum=\"\"");
                        XML_Enviar.Append(" MetodoPago=\"\"");
                        XML_Enviar.Append(" CuentaBancaria=\"\"");
                        XML_Enviar.Append(" Referencia=\"\"");
                        XML_Enviar.Append(">");
                        XML_Enviar.Append(" <Emisor");
                        XML_Enviar.Append(" rfc=\"\"");
                        XML_Enviar.Append(" numero=\"\" />");
                        XML_Enviar.Append(" <Receptor");
                        XML_Enviar.Append(" rfc=\"\"");
                        XML_Enviar.Append(" nombre=\"\">");
                        XML_Enviar.Append(" <Domicilio");
                        XML_Enviar.Append(" calle=\"\"");
                        XML_Enviar.Append(" noExterior=\"\"");
                        XML_Enviar.Append(" colonia=\"\"");
                        XML_Enviar.Append(" municipio=\"\"");
                        XML_Enviar.Append(" estado=\"\"");
                        XML_Enviar.Append(" pais=\"\"");
                        XML_Enviar.Append(" codigoPostal=\"\" />");
                        XML_Enviar.Append(" </Receptor>");
                        XML_Enviar.Append(" <Conceptos>");
                        XML_Enviar.Append(" <Concepto");
                        XML_Enviar.Append(" cantidad=\"\"");
                        XML_Enviar.Append(" noIdentificacion=\"\"");
                        XML_Enviar.Append(" descripcion=\"\"");
                        XML_Enviar.Append(" valorUnitario=\"\"");
                        XML_Enviar.Append(" importe=\"\" />");
                        XML_Enviar.Append(" </Conceptos>");
                        XML_Enviar.Append(" <Impuestos");
                        XML_Enviar.Append(" totalImpuestosTrasladados=\"\">");
                        XML_Enviar.Append(" <Traslados>");
                        XML_Enviar.Append(" <Traslado");
                        XML_Enviar.Append(" impuesto=\"\"");
                        XML_Enviar.Append(" tasa=\"\"");
                        XML_Enviar.Append(" importe=\"\" />");
                        XML_Enviar.Append(" </Traslados>");
                        if ((factura.Fac_RetIva == true) && (factura.Fac_ImporteRetencion > 0))
                        {
                            XML_Enviar.Append(" <Retenidos>");
                            XML_Enviar.Append(" <Retenido");
                            XML_Enviar.Append(" importe=\"\"");
                            XML_Enviar.Append(" impuesto=\"\" />");
                            XML_Enviar.Append(" </Retenidos>");
                        }
                        XML_Enviar.Append(" </Impuestos>");
                        XML_Enviar.Append(" <Addenda>");
                        //ADENDA CABECERA
                        XML_Enviar.Append(" <cabecera");
                        XML_Enviar.Append(" Pedido=\"\"");
                        XML_Enviar.Append(" Requisicion=\"\"");
                        XML_Enviar.Append(" consignarRenglon1=\"\"");
                        XML_Enviar.Append(" consignarRenglon2=\"\"");
                        XML_Enviar.Append(" consignarRenglon3=\"\"");
                        XML_Enviar.Append(" consignarRenglon4=\"\"");
                        XML_Enviar.Append(" consignarRenglon5=\"\"");
                        XML_Enviar.Append(" Conducto=\"\"");
                        XML_Enviar.Append(" CondicionesPago=\"\"");
                        XML_Enviar.Append(" NumeroGuia=\"\"");
                        XML_Enviar.Append(" ControlPedido=\"\"");
                        XML_Enviar.Append(" OrdenEmbarque=\"\"");
                        XML_Enviar.Append(" Zona=\"\"");
                        XML_Enviar.Append(" Territorio=\"\"");
                        XML_Enviar.Append(" Agente=\"\"");
                        XML_Enviar.Append(" NumeroDocumentoAduanero=\"\"");
                        XML_Enviar.Append(" Formulo=\"\"");
                        XML_Enviar.Append(" Autorizo=\"\"");
                        XML_Enviar.Append(" NombreAddenda=\"\"");
                        foreach (AdendaDet det in listCabT)
                        {
                            XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                        }
                        foreach (AdendaDet det in listCabR)
                        {
                            XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                        }
                        XML_Enviar.Append("/>");
                        //ADENDA DETALLE
                        if (listaProdFacturaEspecialFinal.Count > 0)
                        {
                            foreach (FacturaDet fd in listaProdFacturaEspecialFinal)
                            {
                                XML_Enviar.Append(" <Detalle");
                                XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                                XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                                XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");
                                string primerNodo = "";
                                int primerfila = 0;
                                foreach (AdendaDet det in listDetT)
                                {
                                    if (fd.Id_Prd == det.Id_Prd)
                                    {
                                        if (primerfila == 0)
                                        { // COPIAMOS EL NOMBRE DEL PRIMER NODO PARA COMPARAR CUANDO INICIE UNA NUEVA ADENDA
                                            primerNodo = det.Nodo;
                                        }
                                        if (primerfila > 0 && det.Nodo == primerNodo)
                                        {
                                            XML_Enviar.Append("/>");//CERRAMOS LA ADENDA
                                            // ABRIMOS UNA NUEVA ADENDA
                                            XML_Enviar.Append(" <Detalle");
                                            XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                                            XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                                            XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");
                                        }
                                        XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                                        primerfila++;
                                    }
                                }
                                primerNodo = "";
                                primerfila = 0;
                                foreach (AdendaDet det in listDetR)
                                {
                                    if (fd.Id_Prd == det.Id_Prd)
                                    {
                                        if (primerfila == 0)
                                        { // COPIAMOS EL NOMBRE DEL PRIMER NODO PARA COMPARAR CUANDO INICIE UNA NUEVA ADENDA
                                            primerNodo = det.Nodo;
                                        }
                                        if (primerfila > 0 && det.Nodo == primerNodo)
                                        {
                                            XML_Enviar.Append("/>");//CERRAMOS LA ADENDA
                                            // ABRIMOS UNA NUEVA ADENDA
                                            XML_Enviar.Append(" <Detalle");
                                            XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                                            XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                                            XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");
                                        }
                                        XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                                        primerfila++;
                                    }
                                }
                                XML_Enviar.Append("/>");
                            }
                        }
                        else
                        {
                            //NUEVO METODO PARA IMPRIMIR DETALLES DE ADENDA
                            //NUEVO METODO PARA IMPRIMIR DETALLES DE ADENDA
                            foreach (FacturaDet fd in listaFacturaDet)
                            {
                                XML_Enviar.Append(" <Detalle");
                                XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                                XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                                XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");
                                string primerNodo = "";
                                int primerfila = 0;
                                foreach (AdendaDet det in listDetT)
                                {
                                    if (fd.Id_Prd == det.Id_Prd)
                                    {
                                        if (primerfila == 0)
                                        { // COPIAMOS EL NOMBRE DEL PRIMER NODO PARA COMPARAR CUANDO INICIE UNA NUEVA ADENDA
                                            primerNodo = det.Nodo;
                                        }
                                        if (primerfila > 0 && det.Nodo == primerNodo)
                                        {
                                            XML_Enviar.Append("/>");//CERRAMOS LA ADENDA
                                            // ABRIMOS UNA NUEVA ADENDA
                                            XML_Enviar.Append(" <Detalle");
                                            XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                                            XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                                            XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");
                                        }
                                        XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                                        primerfila++;
                                    }
                                }
                                primerNodo = "";
                                primerfila = 0;
                                foreach (AdendaDet det in listDetR)
                                {
                                    if (fd.Id_Prd == det.Id_Prd)
                                    {
                                        if (primerfila == 0)
                                        { // COPIAMOS EL NOMBRE DEL PRIMER NODO PARA COMPARAR CUANDO INICIE UNA NUEVA ADENDA
                                            primerNodo = det.Nodo;
                                        }
                                        if (primerfila > 0 && det.Nodo == primerNodo)
                                        {
                                            XML_Enviar.Append("/>");//CERRAMOS LA ADENDA
                                            // ABRIMOS UNA NUEVA ADENDA
                                            XML_Enviar.Append(" <Detalle");
                                            XML_Enviar.Append(" noProducto=\"" + fd.Id_Prd + "\"");
                                            XML_Enviar.Append(" UnidadMedida=\"" + fd.Producto.Prd_Presentacion.Trim() + " " + fd.Producto.Prd_UniNs + "\"");
                                            XML_Enviar.Append(" UnidadFiscal=\"" + fd.Producto.U_Descripcion.Trim() + "\"");
                                        }
                                        XML_Enviar.Append(" " + det.Nodo + "=\"" + det.Valor + "\"");
                                        primerfila++;
                                    }
                                }
                                XML_Enviar.Append("/>");
                            }
                        }
                        XML_Enviar.Append(" </Addenda>");
                        XML_Enviar.Append(" </Comprobante>");
                        #endregion
                        // --------------------------------------
                        // Consulta centro de distribución
                        // --------------------------------------
                        CentroDistribucion Cd = new CentroDistribucion();
                        new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref Cd, sesion.Id_Cd_Ver, sesion.Id_Emp, sesion.Emp_Cnx);

                        // --------------------------------------
                        // cargar xml de factura que se envia a SAT
                        // --------------------------------------
                        XmlDocument xml = new XmlDocument();
                        xml.LoadXml(XML_Enviar.ToString());
                        // --------------------------------------//
                        // --------------------------------------//
                        //         LLENAR DATOS DEL XML          //
                        // --------------------------------------//
                        // --------------------------------------//
                        #region Llenar datos factura a Enviar
                        //encabezado
                        XmlNode Comprobante = xml.SelectSingleNode("Comprobante");
                        Clientes cliente = new Clientes();
                        cliente.Id_Emp = factura.Id_Emp;
                        cliente.Id_Cd = factura.Id_Cd;
                        cliente.Id_Cte = factura.Id_Cte;
                        new CN_CatCliente().ConsultaClientes(ref cliente, sesion.Emp_Cnx);
                        Comprobante.Attributes["serie"].Value = factura.Serie;
                        Comprobante.Attributes["folio"].Value = factura.Folio_cancelacion > 0 ? factura.Folio_cancelacion.ToString() : factura.Id_Fac.ToString();
                        Comprobante.Attributes["fecha"].Value = string.Format("{0:s}", factura.Fac_Fecha);
                        Comprobante.Attributes["formaDePago"].Value = "PAGO EN UNA SOLA EXHIBICION";
                        Comprobante.Attributes["subTotal"].Value = factura.Fac_SubTotal == null ? "0" : Math.Round((double)factura.Fac_SubTotal, 2).ToString();
                        Comprobante.Attributes["total"].Value = (Math.Round((double)factura.Fac_SubTotal, 2) + Math.Round((double)factura.Fac_ImporteIva, 2)).ToString();
                        Comprobante.Attributes["tipoDeComprobante"].Value = "ingreso";
                        Comprobante.Attributes["Sustituye"].Value = factura.Fac_Refactura;
                        Comprobante.Attributes["tipoMovimiento"].Value = movimiento;
                        Comprobante.Attributes["tipoMoneda"].Value = factura.Mon_Unidad;
                        Comprobante.Attributes["tipoCambio"].Value = factura.Mon_TipCambio.ToString();
                        Comprobante.Attributes["leyendaFacturaEspecial"].Value = ""; //
                        Comprobante.Attributes["movimientoacancelar"].Value = ""; //
                        Comprobante.Attributes["ConceptoDescuento1"].Value = factura.Fac_Desc1;
                        Comprobante.Attributes["TasaDescuento1"].Value = factura.Fac_DescPorcen1 == null ? string.Empty : factura.Fac_DescPorcen1.ToString();
                        Comprobante.Attributes["ConceptoDescuento2"].Value = factura.Fac_Desc2;
                        Comprobante.Attributes["TasaDescuento2"].Value = factura.Fac_DescPorcen2 == null ? string.Empty : factura.Fac_DescPorcen2.ToString();
                        Comprobante.Attributes["Correo"].Value = factura.Cte_Email;
                        Comprobante.Attributes["CliNum"].Value = factura.Id_Cte.ToString();
                        Comprobante.Attributes["MetodoPago"].Value = FormaPagoNombre(factura.Fac_FPago);
                        Comprobante.Attributes["CuentaBancaria"].Value = factura.Fac_UDigitos.ToString();
                        Comprobante.Attributes["Referencia"].Value = cliente.Cte_Referencia;
                        XmlNode Emisor = Comprobante.SelectSingleNode("Emisor");
                        Emisor.Attributes["rfc"].Value = Cd.Cd_Rfc;
                        Emisor.Attributes["numero"].Value = Cd.Cd_Numero;
                        //receptor
                        XmlNode Receptor = Comprobante.SelectSingleNode("Receptor");
                        Receptor.Attributes["rfc"].Value = factura.Fac_CteRfc;
                        Receptor.Attributes["nombre"].Value = factura.Cte_NomComercial;
                        //Domicilio
                        XmlNode Domicilio = Receptor.SelectSingleNode("Domicilio");
                        Domicilio.Attributes["calle"].Value = HttpUtility.HtmlEncode(cliente.Cte_FacCalle); // factura.Fac_CteCalle.Replace("\"", "");
                        Domicilio.Attributes["noExterior"].Value = HttpUtility.HtmlEncode(cliente.Cte_FacNumero);// factura.Fac_CteNumero;
                        Domicilio.Attributes["colonia"].Value = HttpUtility.HtmlEncode(cliente.Cte_FacColonia);// factura.Fac_CteColonia;
                        Domicilio.Attributes["municipio"].Value = HttpUtility.HtmlEncode(cliente.Cte_FacMunicipio);// factura.Fac_CteMunicipio;
                        Domicilio.Attributes["estado"].Value = HttpUtility.HtmlEncode(cliente.Cte_FacEstado);// factura.Fac_CteEstado;
                        Domicilio.Attributes["pais"].Value = "México";
                        Domicilio.Attributes["codigoPostal"].Value = cliente.Cte_FacCp;// factura.Fac_CteCp;
                        // ---------------------
                        // Conceptos --> partidas = producto
                        // Detalle --> productoDetalle
                        // ---------------------         
                        XmlNode Conceptos = Comprobante.SelectSingleNode("Conceptos");
                        XmlNode producto = Conceptos.SelectSingleNode("Concepto");
                        XmlNode Addenda = Comprobante.SelectSingleNode("Addenda");
                        //Si existe una factura especial, en los nodos de conceptos del producto se pone
                        //los productos de la factura especial
                        //si no, se pone los datos de productos de la factura original
                        StringBuilder NotaProductosOriginales = new StringBuilder();
                        if (listaProdFacturaEspecialFinal.Count > 0)
                        {
                            foreach (FacturaDet facturaDet in listaProdFacturaEspecialFinal)
                            {
                                XmlNode prd = producto.Clone();
                                prd.Attributes["noIdentificacion"].Value = facturaDet.Producto.Id_PrdEsp;
                                prd.Attributes["descripcion"].Value = facturaDet.Producto.Prd_DescripcionEspecial.Replace("\"", "");
                                prd.Attributes["cantidad"].Value = facturaDet.Fac_Cant.ToString();
                                prd.Attributes["valorUnitario"].Value = Math.Round(facturaDet.Fac_Precio, 2).ToString();
                                prd.Attributes["importe"].Value = Math.Round(facturaDet.Fac_ImporteE, 2).ToString();
                                producto.ParentNode.AppendChild(prd);
                            }
                            foreach (FacturaDet facturaDet in listaFacturaDet)
                            {
                                NotaProductosOriginales.Append("/");
                                NotaProductosOriginales.Append(facturaDet.Id_Prd.ToString());
                                NotaProductosOriginales.Append("/");
                                NotaProductosOriginales.Append(Math.Round(facturaDet.Fac_Precio, 2).ToString());
                                NotaProductosOriginales.Append("/");
                                NotaProductosOriginales.Append(facturaDet.Fac_Cant.ToString());
                            }
                        }
                        else
                        {
                            foreach (FacturaDet facturaDet in listaFacturaDet)
                            {
                                XmlNode prd = producto.Clone();
                                prd.Attributes["noIdentificacion"].Value = facturaDet.Id_Prd.ToString();
                                prd.Attributes["descripcion"].Value = facturaDet.Producto.Prd_Descripcion.Replace("\"", "");
                                prd.Attributes["cantidad"].Value = facturaDet.Fac_Cant.ToString();
                                prd.Attributes["valorUnitario"].Value = Math.Round(facturaDet.Fac_Precio, 2).ToString();
                                prd.Attributes["importe"].Value = Math.Round(facturaDet.Fac_Importe, 2).ToString();
                                producto.ParentNode.AppendChild(prd);
                            }
                        }
                        producto.ParentNode.RemoveChild(xml.SelectNodes("//Concepto").Item(0));
                        //Impuestos
                        XmlNode Impuestos = Comprobante.SelectSingleNode("Impuestos");
                        Impuestos.Attributes["totalImpuestosTrasladados"].Value = factura.Fac_ImporteIva == null ? "0" : factura.Fac_ImporteIva.ToString();
                        //Traslado (impuestos desgloce)
                        XmlNode Traslados = Impuestos.SelectSingleNode("Traslados");
                        XmlNode Traslado = Traslados.SelectSingleNode("Traslado");
                        Traslado.Attributes["impuesto"].Value = "IVA";
                        if (cliente.BPorcientoIVA == true)
                            Traslado.Attributes["tasa"].Value = cliente.PorcientoIVA.ToString();
                        else
                            Traslado.Attributes["tasa"].Value = Cd.Cd_IvaPedidosFacturacion.ToString();
                        Traslado.Attributes["importe"].Value = factura.Fac_ImporteIva == null ? "0" : Math.Round((double)factura.Fac_ImporteIva, 2).ToString();
                        if ((factura.Fac_RetIva == true) && (factura.Fac_ImporteRetencion > 0))
                        {
                            XmlNode Retenidos = Impuestos.SelectSingleNode("Retenidos");
                            XmlNode Retenido = Retenidos.SelectSingleNode("Retenido");
                            Retenido.Attributes["importe"].Value = factura.Fac_ImporteRetencion == null ? "0" : Math.Round((double)factura.Fac_ImporteRetencion, 2).ToString();
                            Retenido.Attributes["impuesto"].Value = "IVA";
                        }
                        //Addenda
                        XmlNode cabecera = Addenda.SelectSingleNode("cabecera");
                        cabecera.Attributes["Pedido"].Value = factura.Fac_PedNum == null ? string.Empty : factura.Fac_PedNum.ToString();
                        cabecera.Attributes["Requisicion"].Value = factura.Fac_Req == null ? string.Empty : factura.Fac_Req.ToString();
                        //consulta datos cliente                 
                        cabecera.Attributes["consignarRenglon1"].Value = factura.Fac_Contacto;
                        cabecera.Attributes["consignarRenglon2"].Value = string.Concat(factura.Fac_CteCalle.Replace("\"", ""), " ", factura.Fac_CteNumero);
                        cabecera.Attributes["consignarRenglon3"].Value = factura.Fac_CteColonia;
                        cabecera.Attributes["consignarRenglon4"].Value = string.Concat(factura.Fac_CteMunicipio, " ", factura.Fac_CteEstado, " ", factura.Fac_CteCp);
                        cabecera.Attributes["consignarRenglon5"].Value = "México";
                        cabecera.Attributes["Conducto"].Value = factura.Fac_Conducto;
                        cabecera.Attributes["CondicionesPago"].Value = factura.Fac_CondEntrega;
                        cabecera.Attributes["NumeroGuia"].Value = factura.Fac_NumeroGuia;
                        cabecera.Attributes["ControlPedido"].Value = factura.Fac_PedNum == null ? string.Empty : factura.Fac_PedNum.ToString();
                        cabecera.Attributes["OrdenEmbarque"].Value = factura.Id_Emb == null ? string.Empty : factura.Id_Emb.ToString();
                        cabecera.Attributes["Zona"].Value = factura.Id_Cd.ToString(); //Cd.Cd_Descripcion;
                        cabecera.Attributes["Territorio"].Value = factura.Id_Ter.ToString(); //factura.Ter_Nombre == null ? string.Empty : factura.Ter_Nombre;
                        cabecera.Attributes["Agente"].Value = factura.Id_Rik == null ? string.Empty : factura.Id_Rik.ToString();
                        cabecera.Attributes["NumeroDocumentoAduanero"].Value = factura.Fac_Req == null ? string.Empty : factura.Fac_Req.ToString();
                        cabecera.Attributes["Formulo"].Value = Cd.Cd_CobranzaPersonaFormula;
                        cabecera.Attributes["Autorizo"].Value = Cd.Cd_CobranzaPersonaAutoriza;
                        cabecera.Attributes["NombreAddenda"].Value = cliente.Ade_Nombre;
                        Factura factura_remision = new Factura();
                        factura_remision.Id_Emp = factura.Id_Emp;
                        factura_remision.Id_Cd = factura.Id_Cd;
                        factura_remision.Id_Fac = factura.Id_Fac;
                        string agregado_nota = "";
                        cn_factura.FacturaRemision_Nota(factura_remision, sesion.Emp_Cnx, ref agregado_nota);
                        StringBuilder NotaCompleta = new StringBuilder();
                        NotaCompleta.Append(agregado_nota + "//");
                        NotaCompleta.Append(NotaProductosOriginales.ToString() + "//");
                        NotaCompleta.Append(factura.Fac_Notas + "//");
                        NotaCompleta.Append(agregado_nota_cancelacion);
                        Comprobante.Attributes["Notas"].Value = NotaCompleta.ToString();
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
                        // -------- ----------------------------------------------
                        XmlDocument xmlSAT = new XmlDocument();
                        int TSAT = 1;
                        XmlDocument xmlBD = new XmlDocument();
                        if (factura.Fac_Xml != null && factura.Fac_Xml != "")
                        {
                            xmlBD.LoadXml(factura.Fac_Xml.ToString());
                            foreach (XmlNode nodo in xmlBD.ChildNodes)
                            {
                                if (nodo.Name == "Comprobante")
                                {
                                    TSAT = 1;
                                }
                                else if (nodo.Name == "cfdi:Comprobante")
                                {
                                    TSAT = 2;
                                }
                            }
                        }
                        WebReference.Service1 sianFacturacionElectronica = new WebReference.Service1();
                        sianFacturacionElectronica.Url = ConfigurationManager.AppSettings["WS_CFDIImpresion"].ToString();
                        object sianFacturacionElectronicaResult = sianFacturacionElectronica.ObtieneCFD(xmlString);
                        if (movimiento == "CANCELACION")
                        {
                            string XmLRegex = string.Empty;
                            XmLRegex = Regex.Replace(sianFacturacionElectronicaResult.ToString(), @"(?s)(?<=<cfdi:Addenda>).+?(?=</cfdi:Addenda>)", "");
                            XmLRegex = XmLRegex.Replace("<cfdi:Addenda>", "");
                            XmLRegex = XmLRegex.Replace("</cfdi:Addenda>", "");
                            xmlSAT.LoadXml(XmLRegex);
                        }
                        else
                        {
                            xmlSAT.LoadXml(sianFacturacionElectronicaResult.ToString());
                        }


                        System.IO.StreamWriter sws = null;
                        URLtempXML = Server.MapPath("xmlSAT") + "\\FACTURA_"  + Id_Fac.ToString() + ".txt";
                        if (File.Exists(URLtempXML))
                            File.Delete(URLtempXML);
                        if (File.Exists(Server.MapPath("xmlSAT") + "\\FACTURA_"  + Id_Fac.ToString() + ".xml"))
                            File.Delete(Server.MapPath("xmlSAT") + "\\FACTURA_" + Id_Fac.ToString() + ".xml");
                        sws = new System.IO.StreamWriter(URLtempXML, false, Encoding.UTF8);
                        sws.WriteLine(sianFacturacionElectronicaResult.ToString());
                        sws.Close();
                        File.Move(URLtempXML, Server.MapPath("xmlSAT") + "\\FACTURA_" + Id_Fac.ToString() + ".xml");
                        URLtempXML = Server.MapPath("xmlSAT") + "\\FACTURA_" + Id_Fac.ToString() + ".xml";
                        string stringPDF = string.Empty;
                        string selloSAT = string.Empty;
                        string folioFiscal = string.Empty;
                        string errorNum = string.Empty;
                        string errorText = string.Empty;
                        TSAT = 1;
                        foreach (XmlNode nodo in xmlSAT.ChildNodes)
                        {
                            if (nodo.Name == "Comprobante")
                            {
                                TSAT = 1;
                                selloSAT = nodo.Attributes["sello"].Value;
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
                                        selloSAT = Nodo_nivel3.Attributes["selloSAT"].Value;
                                        folioFiscal = Nodo_nivel3.Attributes["UUID"].Value;
                                    }
                                }
                            }
                        }
                        if (errorNum != "0")
                        {
                            this.Alerta(string.Concat(errorText.Replace("'", "\"")));
                        }
                        else
                        {
                            factura.Fac_Sello = selloSAT;
                            System.Data.SqlTypes.SqlXml sqlXml
                                = new System.Data.SqlTypes.SqlXml(new XmlTextReader(xmlSAT.OuterXml, XmlNodeType.Document, null));
                            if (movimiento != "CANCELACION")
                            {
                                factura.Fac_Xml = sqlXml;
                                factura.Fac_FolioFiscal = folioFiscal;
                            }
                            factura.Fac_Pdf = this.Base64ToByte(stringPDF);
                            // ---------------------------------------------------------------------------------------------
                            // Se actualiza el estatus de la factura a Impreso (I)
                            // ---------------------------------------------------------------------------------------------
                            if (movimiento != "CANCELACION")
                            {
                                factura.Fac_Estatus = "I";
                                new CN_CapFactura().ModificarFacturaSAT(factura, sesion.Emp_Cnx, ref verificador);
                            }
                            else
                            {
                                factura.Fac_Estatus = "B";
                            }
                            verificador = 0;
                            // -----------------------
                            // Abrir PDF de factura
                            // -----------------------
                            string tempPDFname = string.Concat("FACTURA_", factura.Id_Fac.ToString(), ".pdf");
                            URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                            string WebURLtempPDF = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFname);
                            this.ByteToTempPDF(URLtempPDF, this.Base64ToByte(stringPDF));
                            //// ------------------------------------------------------------------------------------------------
                            //// Ejecuta funciós JS para abrir una nueva ventada de Explorador y visualizar el archivo PDF
                            //// ------------------------------------------------------------------------------------------------
                            //RAM1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDF('", WebURLtempPDF, "')"));
                        }

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
         */
        private bool ValidaImpresionFactura(XmlDocument xml)
        {


            XmlNodeList nodeList = xml.SelectNodes("//Concepto");

            XmlNode Comprobante = xml.SelectSingleNode("Comprobante");

            decimal subtotal = decimal.Parse(Comprobante.Attributes["subTotal"].Value);

            decimal totalDetalle = 0;

            decimal descuento1 = decimal.Parse(Comprobante.Attributes["TasaDescuento1"].Value);
            decimal descuento2 = decimal.Parse(Comprobante.Attributes["TasaDescuento2"].Value);


            foreach (XmlNode concepto in nodeList)
            {

                totalDetalle += decimal.Parse(concepto.Attributes["importe"].Value);
            }


            if (descuento1 > 0)
            {
                totalDetalle = totalDetalle - (totalDetalle * (descuento1 / 100));
            }

            if (descuento2 > 0)
            {
                totalDetalle = totalDetalle - (totalDetalle * (descuento2 / 100));
            }


            if (Math.Round(subtotal, 2) != Math.Round(totalDetalle, 2, MidpointRounding.AwayFromZero))
            {
                return false;
            }



            return true;
        }
        private string FormaPagoNombre(string Id_Fpa)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CN_CatFormaPago cncatformapago = new CN_CatFormaPago();
                FormaPago fpago = new FormaPago();
                fpago.Id_Emp = sesion.Id_Emp;
                if (Id_Fpa != "")
                {
                    fpago.Id_Fpa = Convert.ToInt32(Id_Fpa == "" ? "1" : Id_Fpa);
                    cncatformapago.ConsultaFormaPago(ref fpago, sesion.Emp_Cnx);
                }
                else
                {
                    fpago.Descripcion = "";
                }
                return fpago.Descripcion;
            }
            catch (Exception ex)
            {

                throw ex;
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
        private void EnviarCorreo()
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];

                ConfiguracionGlobal configuracion = new ConfiguracionGlobal();
                configuracion.Id_Cd = session.Id_Cd_Ver;
                configuracion.Id_Emp = session.Id_Emp;
                CN_Configuracion cn_configuracion = new CN_Configuracion();
                cn_configuracion.Consulta(ref configuracion, session.Emp_Cnx);
                StringBuilder cuerpo_correo = new StringBuilder();
                cuerpo_correo.Append("<div align='center'>");
                cuerpo_correo.Append("<table><tr><td>");
                cuerpo_correo.Append("<IMG SRC=\"cid:companylogo\" ALIGN='left'></td>");
                cuerpo_correo.Append("<td></td>");
                cuerpo_correo.Append("</tr><tr><td colspan='2'><br><br><br></td>");
                cuerpo_correo.Append("</tr><tr>");
                cuerpo_correo.Append("<td colspan='2'><b><font face='Tahoma' size='2'>");
                cuerpo_correo.Append("Estimado cliente se le envian PDF y XML de la factura #" + this.LblDocumento.Text);
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");
                cuerpo_correo.Append("<center><br>");
                cuerpo_correo.Append("</td></tr></table></div>");

                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                sm.EnableSsl = false;
                MailMessage m = new MailMessage();
                m.From = new MailAddress(configuracion.Mail_Remitente);
                char[] splitchar = { ';' };
                string[] Correos = this.TxtCorreos.Text.Split(splitchar);

                foreach (string correo in Correos)
                {
                    m.To.Add(new MailAddress(correo));
                }

                m.Attachments.Add(new Attachment(URLtempPDF));
                m.Attachments.Add(new Attachment(URLtempXML));
                m.Subject = "PDF y XML Factura  #" + this.LblDocumento.Text;
                m.IsBodyHtml = true;
                string body = cuerpo_correo.ToString();
                AlternateView vistaHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                //Esto queda dentro de un try por si llegan a cambiar la imagen el correo como quiera se mande
                try
                {
                    LinkedResource logo = new LinkedResource(MapPath(@"Imagenes/logo.jpg"), MediaTypeNames.Image.Jpeg);
                    logo.ContentId = "companylogo";
                    vistaHtml.LinkedResources.Add(logo);
                }
                catch (Exception)
                {
                }

                m.AlternateViews.Add(vistaHtml);
                try
                {
                    sm.Send(m);

                }
                catch (Exception)
                {

                    Alerta("Error al enviar el correo. Favor de revisar la configuración del sistema");
                    return;
                }

                AlertaCerrar("Se han enviado el correo de manera exitosa");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ErrorManager
        private void RadConfirm(string mensaje)
        {
            try
            {
                RAM1.ResponseScripts.Add("radconfirm('" + mensaje + "<br /><br />', confirmCallBackFn, 330, 150);");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }

        }
        private void AlertaFocus(string mensaje, string rtb)
        {
            try
            {
                RAM1.ResponseScripts.Add("AlertaFocus('" + mensaje + "','" + rtb + "');");
            }
            catch (Exception ex)
            {
                ErrorManager(ex, "Alerta");
            }
        }
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
                this.LblMensaje.Text = "";
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
                this.LblMensaje.Text = Message;
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
                this.LblMensaje.Text = "Error: [" + NombreFuncion + "] " + eme.Message.ToString();

            }
            catch (Exception ex)
            {
                this.LblMensaje.Text = "Error grave: " + eme.Message.ToString() + " --> " + ex.Message.ToString();
            }
        }
        #endregion
    }
}