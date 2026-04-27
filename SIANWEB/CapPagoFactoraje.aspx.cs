using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocios;
using Telerik.Web.UI;
using System.IO;
using System.Xml;
using System.Text;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;

namespace SIANWEB
{
    public partial class CapPagoFactoraje : System.Web.UI.Page
    {


        private class Timbrado
        {
            public CapaEntidad.Clientes Cliente { get; set; }
            public List<CapaEntidad.Factura> Facturas { get; set; }
            public IntermediarioFinanciero ListaIntermediarioPago { get; set; }
            public TipoMoneda TipoMoneda { get; set; }
        }

        private string Emp_CnxCob
        {
            get { return ConfigurationManager.AppSettings.Get("strConnectionCobranza"); }
        }

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
                    Context.Items.Add("href", pag[pag.Length - 1]);
                    Session["dir" + Session.SessionID] = pag[pag.Length - 1];
                    Response.Redirect("login.aspx", false);
                }
                else
                {
                    if (Page.IsPostBack == false)
                    {


                        if (Request.QueryString["Prmt"] != null)
                        {
                            //Si el Prmt es igual a 1 es factoraje general de todos los sello que no cuentan ya con su timbre
                            if (Request.QueryString["Prmt"].ToString() == "1")
                            {
                                hdnIdEmp.Value = Request.QueryString["Id_Emp"].ToString();
                                hdnIdCd.Value = Request.QueryString["Id_Cd"].ToString();
                                hdnIdPag.Value = Request.QueryString["Id_Pag"].ToString();
                                hdnCte.Value = Request.QueryString["idCte"].ToString();
                            }
                            if (Request.QueryString["Prmt"].ToString() == "2")
                            {
                                hdnIdEmp.Value = Request.QueryString["Id_Emp"].ToString();
                                hdnIdCd.Value = Request.QueryString["Id_Cd"].ToString();
                                hdnIdPag.Value = Request.QueryString["Id_Pag"].ToString();
                                hdnCte.Value = Request.QueryString["idCte"].ToString();
                                hdnIdPagDet.Value = Request.QueryString["idPagDte"].ToString();
                                hdnfac.Value = Request.QueryString["IdFac"].ToString();
                            }
                            if (Request.QueryString["Prmt"].ToString() == "3")
                            {
                                hdnIdEmp.Value = Request.QueryString["Id_Emp"].ToString();
                                hdnIdCd.Value = Request.QueryString["Id_Cd"].ToString();
                                hdnIdPag.Value = Request.QueryString["Id_Pag"].ToString();
                                hdnCte.Value = Request.QueryString["idCte"].ToString();
                                hdnIdPagDet.Value = Request.QueryString["idPagDte"].ToString();
                            }
                        }

                        cargarCboUsuarios();
                        CargarCentros();
                        this.cboIntermediario.Focus();
                    }
                }
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
                CN__Comun comun = new CN__Comun();
                comun.CambiarCdVer(CmbCentro.SelectedItem, ref sesion);
                cargarCboUsuarios();

            }
            catch (Exception ex)
            {
                ErrorManager(ex, "CmbCentro_SelectedIndexChanged1");
            }
        }


        protected void cboIntermediario_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ErrorManager();
                if (this.cboIntermediario.SelectedValue != "-1")
                {
                    hdnValor.Value = cboIntermediario.SelectedItem.Text;
                    string script = "radconfirm('¿Es correcto el intermediario elegido?', confirmCallBackFn, 350, 150);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "RadButton1", script, true);
                }
                else
                {
                    this.RequiredFieldValidator1.IsValid = false;
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorManager(ex, new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name);
            }

        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            Pago listaPago = new Pago();
            string ConexionCfdII = ConfigurationManager.AppSettings["WS_CFDIImpresion"].ToString();
            string Emp_Cnx = ConfigurationManager.AppSettings["strConnection"].ToString();

            listaPago.Id_Emp = Convert.ToInt32(hdnIdEmp.Value);
            listaPago.Id_Cd = Convert.ToInt32(hdnIdCd.Value);
            listaPago.Id_Pag = Convert.ToInt32(hdnIdPag.Value);

            timbrarFactoraje(listaPago, Emp_Cnx, ConexionCfdII);
        }


        #endregion
        #region Funciones

        private void cargarCboUsuarios()
        {
            try
            {
                Sesion Sesion = new Sesion();
                Sesion = (Sesion)Session["Sesion" + Session.SessionID];
                CapaNegocios.CN__Comun CN_Comun = new CapaNegocios.CN__Comun();
                CN_Comun.LlenaCombo(Sesion.Id_Cd_Ver, Sesion.Id_Emp, Convert.ToInt32(hdnCte.Value), Sesion.Emp_Cnx, "spCatIntermediarioFinanciero_ConsultaActivos", ref this.cboIntermediario);
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
                    CN_Comun.LlenaCombo(1, Sesion.Id_Emp, Sesion.Id_U, Sesion.Id_Cd_Ver, Sesion.Id_Cd, Sesion.Emp_Cnx, "spCatCentroDistribucion_Combo", ref CmbCentro);
                    this.CmbCentro.SelectedValue = Sesion.Id_Cd_Ver.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        #endregion

        #region factoraje

        private void timbrarFactoraje(Pago pago, string conexion, string ConexionCfdII)
        {
            try
            {


                List<Banco_Ficha> list_fichas = new List<Banco_Ficha>();
                List<PagoDet> list_pagos = new List<PagoDet>();
                List<PagoDetComplemento> List_DetComplemento = new List<PagoDetComplemento>();

                CN_CapPago cn_cappago = new CN_CapPago();
                cn_cappago.ConsultaPagoTimbres(ref pago, ref list_pagos, conexion, ref List_DetComplemento);

                Timbrar(list_pagos, pago.Id_Emp, pago.Id_Cd, conexion, ConexionCfdII);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        private void Timbrar(List<PagoDet> List_DetComplemento, int Ids_Emp, int Ids_Cd, string conexion, string ConexionCfdII)
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];


                CN_ClienteIntermediarioPago ClienteInter = new CN_ClienteIntermediarioPago();
                CN_IntermediarioFinanciero CNIntermediario = new CN_IntermediarioFinanciero();
                List<IntermediarioFinanciero> ListaIntermediario = new List<IntermediarioFinanciero>();
                CN_CatTipoMoneda TipoMoneda = new CN_CatTipoMoneda();
                List<TipoMoneda> ListaTipoMoneda = new List<CapaEntidad.TipoMoneda>();
                List<Timbrado> Timbres = new List<Timbrado>();
                int id_cte;
                Timbrado Timbre = null;
                Factura Factura = null;
                int id_Emp;
                int id_Cd;
                int id_Fac;
                int id_Pag = 0;
                int id_pagdet;
                string serie;
                string rfc;

                int pagdet = 0;

                if (Request.QueryString["Prmt"].ToString() == "2" || Request.QueryString["Prmt"].ToString() == "3")
                {
                    pagdet = Convert.ToInt32(hdnIdPagDet.Value);
                }

                for (int cont = 0; cont < List_DetComplemento.Count; cont++)
                {
                    if ((Request.QueryString["Prmt"].ToString() == "2" || Request.QueryString["Prmt"].ToString() == "3") && pagdet != List_DetComplemento[cont].Id_PagDet)
                    {
                        continue;
                    }

                    id_Emp = List_DetComplemento[cont].Pag_Id_Emp;
                    id_Fac = Convert.ToInt32(List_DetComplemento[cont].Ref);
                    id_Cd = List_DetComplemento[cont].Pag_Id_cd;
                    id_Pag = List_DetComplemento[cont].Id_Pag;
                    id_pagdet = List_DetComplemento[cont].Id_PagDet;
                    id_cte = List_DetComplemento[cont].Id_Cte;
                    rfc = List_DetComplemento[cont].Cte_Rfc;
                    serie = List_DetComplemento[cont].Pag_Serie;

                    List<PagoDetComplemento> lista = new List<PagoDetComplemento>();
                    new CN_CapPago().ConsultaListaComplementosNoTimbrados(id_Emp, id_Cd, id_Pag, id_cte, id_pagdet, conexion, ref lista);

                    if (lista.Count > 0)
                    {
                        if (lista[0].Pago_Xml != null && lista[0].Cancelacion_XML == null)
                        {
                            continue;
                        }
                    }


                    Factura = new Factura();
                    Factura.Id_Emp = id_Emp;
                    Factura.Id_Cd = id_Cd;
                    Factura.Id_Fac = id_Fac;
                    List<FacturaDet> ListaFacturaDet = new List<FacturaDet>();

                    if (id_Cd != session.Id_Cd_Ver)
                    {
                        new CN_CapFactura().ConsultaFacturaOtraBD(ref Factura, serie, conexion);
                    }
                    else
                    {
                        new CN_CapFactura().ConsultaFactura(ref Factura, conexion);
                    }

                    if (Factura.Id_FacSerie == null)
                    {
                        return;
                    }

                    TipoMoneda.ConsultaTipoMonedaXMLFacturacion(id_Emp, conexion, ref ListaTipoMoneda);

                    TipoMoneda moneda = new TipoMoneda();

                    moneda = (from tlist in ListaTipoMoneda
                              where tlist.Id_Mon == Factura.Id_Mon
                              select tlist).FirstOrDefault();




                    if (Timbres.Any(x => x.Cliente.Cte_FacRfc.Replace(" ", string.Empty) == rfc.Replace(" ", string.Empty) && x.TipoMoneda.Id_Mon == moneda.Id_Mon))
                    {
                        //agregar factura de cliente existente
                        Timbre = Timbres.FirstOrDefault<Timbrado>(x => x.Cliente.Cte_FacRfc.Replace(" ", string.Empty) == rfc.Replace(" ", string.Empty) && x.TipoMoneda.Id_Mon == moneda.Id_Mon);
                        Timbre.Facturas.Add(Factura);
                    }
                    else
                    {
                        //agregar cliente y factura
                        CapaEntidad.Clientes Cliente = new Clientes();
                        Cliente.Id_Emp = id_Emp;
                        Cliente.Id_Cd = id_Cd;
                        Cliente.Id_Cte = id_cte;
                        Cliente.Ignora_Inactivo = true;
                        CN_CatCliente clsCatClientes = new CN_CatCliente();
                        if (id_Cd != session.Id_Cd_Ver)
                        {
                            clsCatClientes.ConsultaClienteOtraBD(ref Cliente, serie, session.Emp_Cnx);
                        }
                        else
                        {
                            clsCatClientes.ConsultaClientes(ref Cliente, session.Emp_Cnx);
                        }

                        //Agregar intermediario de pago seleccionado

                        IntermediarioFinanciero Intermediario = new IntermediarioFinanciero();
                        Intermediario.Id_Emp = id_Emp;
                        Intermediario.Id_Cd = session.Id_Cd_Ver;
                        Intermediario.Id_IF = Convert.ToInt32(cboIntermediario.SelectedValue);

                        CNIntermediario.ConsultaIntermediarioBanco(Intermediario, conexion, ref ListaIntermediario);


                        Timbre = new Timbrado();
                        Timbre.Cliente = Cliente;
                        Timbre.Facturas = new List<CapaEntidad.Factura>();
                        Timbre.ListaIntermediarioPago = new IntermediarioFinanciero();
                        Timbre.ListaIntermediarioPago = ListaIntermediario.FirstOrDefault();
                        Timbre.TipoMoneda = new TipoMoneda();
                        Timbre.TipoMoneda = moneda;
                        Timbre.Facturas.Add(Factura);
                        Timbres.Add(Timbre);
                    }
                }
                if (Timbres.Count == 0)
                {
                    return;
                }
                else
                {
                    this.ImprimirTimbres(Timbres, id_Pag, Ids_Emp, Ids_Cd, conexion, ConexionCfdII);
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        private void ImprimirTimbres(List<Timbrado> Timbres, int Id_pag, int Id_Emp, int Id_CD, string conexion, string ConexionCfdII)
        {
            try
            {

                CentroDistribucion Cd = new CentroDistribucion();
                new CN_CatCentroDistribucion().ConsultarCentroDistribucion(ref Cd, Id_CD, Id_Emp, conexion);
                Pago pago = new Pago();
                List<Banco_Ficha> list_fichas = new List<Banco_Ficha>();
                List<PagoDet> list_pagos = new List<PagoDet>();
                List<PagoDetComplemento> List_DetComplemento = new List<PagoDetComplemento>();
                pago.Id_Emp = Id_Emp;
                pago.Id_Cd = Id_CD;
                pago.Id_Pag = Id_pag;
                new CN_CapPago().ConsultaPago2(ref pago, ref list_fichas, ref list_pagos, conexion, ref List_DetComplemento);
                foreach (var Timbre in Timbres)
                {
                    string nombrePDF = "PAGO_";
                    StringBuilder XML_EnviarPrueba = new StringBuilder();
                    StringBuilder XML_Enviar = new StringBuilder();
                    StringBuilder XML_Conceptos = new StringBuilder();

                    object pagoPDF = null;
                    int pag_ID = 0;
                    int verificador = -1;
                    double importes = 0;
                    object sianFacturacionElectronicaResult = null;
                    DateTime fechaPago = DateTime.Now;

                    string serie = string.Empty;
                    int folio = 0;
                    new CN_CapPago().ConsultaSerieYFolio(pago.Id_Emp, pago.Id_Cd, ref serie, ref folio, conexion);
                    //folio++;
                    //nombrePDF = nombrePDF + pago.Id_Emp.ToString() + "_" + pago.Id_Cd.ToString() + "_" + folio.ToString() + ".pdf";
                    #region construirXML
                    try
                    {
                        PagoDetComplemento pagoDetComplemento = new PagoDetComplemento();
                        List<PagoDetComplemento> ListaDocumentos = new List<PagoDetComplemento>();
                        List<PagoDetComplemento> listaFoliosRelacionados = new List<PagoDetComplemento>();
                        foreach (var Factura in Timbre.Facturas)
                        {
                            object complementoPagoPDF = null;
                            PagoDet PagoDet = list_pagos.Where(at => at.Ref.Replace(" ", string.Empty) == Factura.Id_Fac.ToString()).Select(row => row).FirstOrDefault();
                            pagoDetComplemento.Id_Emp = pago.Id_Emp;
                            pagoDetComplemento.Id_Cd = pago.Id_Cd;
                            pagoDetComplemento.Id_Pag = pago.Id_Pag;
                            pagoDetComplemento.Id_Cte = Timbre.Cliente.Id_Cte.Value;
                            pagoDetComplemento.Id_PagDet = PagoDet.Id_PagDet;
                            pagoDetComplemento.RFC = Timbre.Cliente.Cte_FacRfc;
                            fechaPago = list_fichas.Where(at => at.Pag_Ficha == PagoDet.Pag_Numero).Select(row => row.Pag_Fecha).First();
                            new CN_CapPago().ConsultaComplementoPago(ref pagoDetComplemento, ref pagoPDF, conexion);

                            if (pagoDetComplemento == null)
                            {
                                pagoDetComplemento = new PagoDetComplemento();
                                pagoDetComplemento.Id_Emp = pago.Id_Emp;
                                pagoDetComplemento.Id_Cd = pago.Id_Cd;
                                pagoDetComplemento.Id_Pag = pago.Id_Pag;
                                pagoDetComplemento.Id_Cte = Timbre.Cliente.Id_Cte.Value;
                                pagoDetComplemento.Id_PagDet = PagoDet.Id_PagDet;
                                pagoDetComplemento.Pago_Serie = serie;
                                pagoDetComplemento.Id_PagComp = folio;
                                pagoDetComplemento.RFC = Timbre.Cliente.Cte_FacRfc;
                                new CN_CapPago().InsertarComplementoPago(pagoDetComplemento, conexion, ref verificador);

                                folio = verificador;
                            }
                            else
                            {
                                folio = pagoDetComplemento.Id_PagComp;
                            }


                            pag_ID = list_pagos.First().Id_Pag;

                            //Forma de pago y uso de cfdi
                            if (!string.IsNullOrEmpty(Timbre.Cliente.Cte_PagoMetodoPago))
                            {
                                pagoDetComplemento.Cte_Fpago = Convert.ToInt32(Timbre.Cliente.Cte_PagoMetodoPago);
                            }
                            else
                            {
                                pagoDetComplemento.Cte_Fpago = Convert.ToInt32(Timbre.Facturas.First().Fac_FPago);
                            }

                            if (pagoDetComplemento.Cte_Fpago == 99)
                                pagoDetComplemento.Cte_Fpago = 3;

                            pagoDetComplemento.Cte_UsoCFDI = Timbre.Cliente.Cte_PagoUsoCFDI;


                            //Conuslta para saber cuantas veces se ha realizado una factura
                            new CN_CapPago().ConsultaDetalleComplementoPagoListaDocs(pagoDetComplemento, ref ListaDocumentos, ref complementoPagoPDF, conexion);
                            PagoDetComplemento registroFolio = new PagoDetComplemento();
                            foreach (PagoDetComplemento registroFolioRelacionados in ListaDocumentos)
                            {
                                if (registroFolioRelacionados.Pago_FolioFiscal != null && registroFolioRelacionados.Pago_FolioFiscal.Length > 0)
                                {
                                    listaFoliosRelacionados.Select(x => x.Pago_FolioFiscal == registroFolioRelacionados.Pago_FolioFiscal).ToList();
                                    if (listaFoliosRelacionados.Count == 0)
                                    {
                                        registroFolio.Pago_FolioFiscal = registroFolioRelacionados.Pago_FolioFiscal;
                                        listaFoliosRelacionados.Add(registroFolio);
                                    }
                                }
                            }

                            var PagoFactura = list_pagos.FirstOrDefault(x => x.Ref.Replace(" ", string.Empty) == Factura.Id_Fac.ToString());

                            double totalFacturado = Factura.Fac_SubTotal.Value + Factura.Fac_ImporteIva.Value;

                            var Pagado = List_DetComplemento.FirstOrDefault(x => x.pag_referencia.Replace(" ", string.Empty) == Factura.Id_Fac.ToString());

                            double saldo = 0;
                            double saldoant = 0;


                            int numParcialidad = 1;
                            try
                            {
                                if (totalFacturado <= Pagado.Total_Pagado_Timbrado)
                                {
                                    saldoant = totalFacturado;
                                    saldo = 0;
                                }
                                else
                                {
                                    saldoant = totalFacturado - Pagado.Total_Pagado_Timbrado;
                                    numParcialidad = Pagado.Parcialidad_Timbrada + 1;
                                }
                            }
                            catch (Exception ex)
                            {
                                saldoant = totalFacturado;
                                saldo = 0;
                            }

                            if (Convert.ToDouble(saldoant.ToString("N2")) < PagoFactura.Importe)
                            {
                                PagoFactura.Importe = saldoant;
                            }

                            saldo = saldoant - PagoFactura.Importe;


                            importes += PagoFactura.Importe;
                            XML_Conceptos.Append(" <Concepto");
                            XML_Conceptos.Append(" ImpSaldoInsoluto=\"" + ((saldo <= 0) ? string.Format("{0:0.00}", decimal.Zero) : string.Format("{0:0.00}", saldo)) + "\"");
                            XML_Conceptos.Append(" ImpPagado=\"" + (string.Format("{0:0.00}", PagoFactura.Importe)) + "\"");
                            XML_Conceptos.Append(" ImpSaldoAnt=\"" + string.Format("{0:0.00}", saldoant) + "\"");
                            XML_Conceptos.Append(" NumParcialidad=\"" + numParcialidad.ToString() + "\"");
                            XML_Conceptos.Append(" TipoCambioDR=\"" + Timbre.TipoMoneda.Mon_TipCambio + "\"");
                            XML_Conceptos.Append(" MonedaDR=\"" + Timbre.TipoMoneda.Mon_codigo + "\"");
                            XML_Conceptos.Append(" FPago=\"" + PagoFactura.Ref + "\"");

                            XML_Conceptos.Append(" SPago=\"" + PagoFactura.Pag_Serie + "\"");
                            //XML_Conceptos.Append(" SPago=\"" + "PRUEBA" + "\"");
                            XML_Conceptos.Append(" MetodoDePagoDR=\"" + Timbre.Cliente.Cte_MetodoPago + "\"");

                            XML_Conceptos.Append(" UUID=\"" + PagoFactura.Fac_FolioFiscal + "\" />");
                        }

                        nombrePDF = nombrePDF + pago.Id_Emp.ToString() + "_" + pago.Id_Cd.ToString() + "_" + folio.ToString() + ".pdf";

                        XML_Enviar.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                        XML_Enviar.Append("<Comprobante");
                        XML_Enviar.Append(" ComprobanteVersion=\"" + Timbre.Cliente.Cte_VersionCFDI + "\"");

                        XML_Enviar.Append(" MetodoPago=\"" + pagoDetComplemento.Cte_Fpago.ToString().PadLeft(2, '0') + "\"");
                        XML_Enviar.Append(" CliNum=\"" + Timbre.ListaIntermediarioPago.Banco.ToString() + "\"");
                        XML_Enviar.Append(" Correo=\"" + Timbre.ListaIntermediarioPago.Correo + "\"");
                        XML_Enviar.Append(" Notas=\"" + "//////" + "\"");
                        XML_Enviar.Append(" tipoCambio=\"" + Timbre.TipoMoneda.Mon_TipCambio + "\"");
                        XML_Enviar.Append(" tipoMoneda=\"" + Timbre.TipoMoneda.Mon_Abrev + "\"");
                        XML_Enviar.Append(" tipoMovimiento=\"" + "PAGO" + "\"");
                        XML_Enviar.Append(" tipoDeComprobante=\"" + "PAGO" + "\"");
                        XML_Enviar.Append(" total=\"" + importes.ToString() + "\"");
                        XML_Enviar.Append(" fecha=\"" + string.Format("{0:s}", DateTime.Now) + "\"");

                        XML_Enviar.Append(" folio=\"" + folio + "\"");

                        XML_Enviar.Append(" Id_Pag=\"" + pago.Id_Pag.ToString() + "\"");
                        XML_Enviar.Append(" serie=\"" + serie + "\" >");
                        //XML_Enviar.Append(" serie=\"" + "PRUEBA" + "\" >");

                        //Si la factura ya tenia anteriormente factura se relaciona.
                        if (listaFoliosRelacionados.Count > 0)
                        {
                            XML_Enviar.Append(" <CfdiRelacionados");
                            XML_Enviar.Append(" TipoRelacion=\"" + "04" + "\" >");
                            foreach (PagoDetComplemento Complemento in listaFoliosRelacionados)
                            {
                                XML_Enviar.Append(" <CfdiRelacionado");
                                XML_Enviar.Append(" UUID=\"" + Complemento.Pago_FolioFiscal + "\" />");
                            }
                            XML_Enviar.Append("</CfdiRelacionados>");
                        }
                        XML_Enviar.Append(" <Emisor");
                        XML_Enviar.Append(" numero=\"" + Cd.Cd_Numero + "\"");
                        XML_Enviar.Append(" rfc=\"" + Cd.Cd_Rfc + "\" />");

                        XML_Enviar.Append(" <Receptor");
                        XML_Enviar.Append(" rfc=\"" + HttpUtility.HtmlEncode(Timbre.ListaIntermediarioPago.RFC) + "\"");
                        XML_Enviar.Append(" nombre=\"" + HttpUtility.HtmlEncode(Timbre.ListaIntermediarioPago.RazonSocial) + "\"");
                        XML_Enviar.Append(" UsoCFDI=\"" + pagoDetComplemento.Cte_UsoCFDI + "\"");
                        XML_Enviar.Append(" Regimen=\"" + Timbre.Cliente.Cte_RegimenFiscal + "\" >");
                        XML_Enviar.Append(" <Domicilio");
                        XML_Enviar.Append(" codigoPostal=\"" + string.Empty + "\"");
                        XML_Enviar.Append(" pais=\"" + "México" + "\"");
                        XML_Enviar.Append(" estado=\"" + string.Empty + "\"");
                        XML_Enviar.Append(" municipio=\"" + string.Empty + "\"");
                        XML_Enviar.Append(" colonia=\"" + string.Empty + "\"");
                        XML_Enviar.Append(" noInterior=\"" + "" + "\"");
                        XML_Enviar.Append(" noExterior=\"" + string.Empty + "\"");
                        XML_Enviar.Append(" calle=\"" + string.Empty + "\" />");
                        XML_Enviar.Append(" <DatosdePago");
                        if (!string.IsNullOrEmpty(Timbre.Cliente.Cte_PagoBanco))
                        {

                            XML_Enviar.Append(" NomBancoOrdExt=\"" + Timbre.Cliente.Cte_PagoBanco + "\"");
                            XML_Enviar.Append(" CtaOrdenante=\"" + Timbre.Cliente.Cte_PagoNumeroCuenta + "\"");
                            XML_Enviar.Append(" RfcEmisorCtaOrd=\"" + Timbre.Cliente.Cte_PagoBancoRfc + "\"");
                        }

                        XML_Enviar.Append(" FechaPago=\"" + string.Format("{0:s}", fechaPago) + "\"");
                        XML_Enviar.Append(" NumOperacion=\"" + Id_pag.ToString() + "\" />");
                        XML_Enviar.Append(" </Receptor>");

                        XML_Enviar.Append(" <Conceptos>");
                        XML_Enviar.Append(XML_Conceptos);
                        XML_Enviar.Append("</Conceptos>");
                        XML_Enviar.Append("</Comprobante>");
                        #endregion construirXML


                        XmlDocument xml = new XmlDocument();
                        xml.LoadXml(XML_Enviar.ToString());

                        StringWriter sw = new StringWriter();
                        XmlTextWriter tx = new XmlTextWriter(sw);
                        xml.WriteTo(tx);
                        string xmlString = sw.ToString();

                        WebReference.Service1 sianFacturacionElectronica = new WebReference.Service1();
                        if (Timbre.Facturas.Count() > 40)
                        {
                            int tiempo = 6000 * Timbre.Facturas.Count();
                            sianFacturacionElectronica.Timeout = tiempo;
                        }
                        sianFacturacionElectronica.Url = ConexionCfdII;
                        sianFacturacionElectronicaResult = sianFacturacionElectronica.ObtieneCFD(xmlString);
                    }
                    catch (Exception ex)
                    {
                        foreach (var Factura in Timbre.Facturas)
                        {
                            int ver = 0;
                            PagoDet PagoDet = list_pagos.Where(at => at.Ref.Replace(" ", string.Empty) == Factura.Id_Fac.ToString()).Select(row => row).FirstOrDefault();
                            new CN_CapPago().BorrarComplementoPago(Id_Emp, Id_CD, PagoDet.Id_Pag, PagoDet.Id_PagDet, folio, conexion, ref ver);
                        }
                        return;
                    }
                    XmlDocument xmlSAT = new XmlDocument();
                    xmlSAT.LoadXml(sianFacturacionElectronicaResult.ToString());

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
                    int idPag = 0;


                    #region analizaXML
                    foreach (XmlNode nodoSistemaFacturacion in xmlSAT.ChildNodes)
                    {
                        if (nodoSistemaFacturacion.Name == "Comprobante")
                        {

                            selloSAT = nodoSistemaFacturacion.Attributes["sello"].Value;

                            foreach (XmlNode Nodo_nivel2 in nodoSistemaFacturacion.ChildNodes)
                            {
                                if (Nodo_nivel2.Name == "AddendaKey")
                                {
                                    foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                    {
                                        if (Nodo_nivel3.Name == "PDF")
                                        {
                                            stringPDF = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                            idPag = Convert.ToInt32(Nodo_nivel3.Attributes["Id_Pag"].Value);
                                        }
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

                            VersionCFDI = nodoSistemaFacturacion.Attributes["Version"].Value;
                            foreach (XmlNode Nodo_nivel2 in nodoSistemaFacturacion.ChildNodes)
                            {

                                if (Nodo_nivel2.Name == "AddendaKey")
                                {
                                    foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                    {
                                        if (Nodo_nivel3.Name == "PDF")
                                        {
                                            stringPDF = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                            idPag = Convert.ToInt32(Nodo_nivel3.Attributes["Id_Pag"].Value);
                                        }
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

                                            selloSAT = nodo.Attributes["sello"].Value;

                                            foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                                            {
                                                if (Nodo_nivel2.Name == "AddendaKey")
                                                {
                                                    foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                                    {
                                                        if (Nodo_nivel3.Name == "PDF")
                                                        {
                                                            stringPDF = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                                            idPag = Convert.ToInt32(Nodo_nivel3.Attributes["Id_Pag"].Value);
                                                        }
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

                                            VersionCFDI = nodo.Attributes["Version"].Value;
                                            foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                                            {


                                                if (Nodo_nivel2.Name == "AddendaKey")
                                                {
                                                    foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                                    {
                                                        if (Nodo_nivel3.Name == "PDF")
                                                        {
                                                            stringPDF = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                                            idPag = Convert.ToInt32(Nodo_nivel3.Attributes["Id_Pag"].Value);
                                                        }
                                                        if (Nodo_nivel3.Name == "ERROR")
                                                        {
                                                            errorNum = Nodo_nivel3.Attributes["Numero"].Value;
                                                            errorText = Nodo_nivel3.Attributes["Texto"].Value;
                                                        }
                                                        if (Nodo_nivel3.Name == "IDPAG")
                                                        {
                                                            idPag = Convert.ToInt32(Nodo_nivel3.Attributes["Numero"].Value);
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

                                                selloSATCN = nodo.Attributes["sello"].Value;

                                                foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                                                {
                                                    if (Nodo_nivel2.Name == "AddendaKey")
                                                    {
                                                        foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                                        {
                                                            if (Nodo_nivel3.Name == "PDF")
                                                            {
                                                                stringPDFCN = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                                                idPag = Convert.ToInt32(Nodo_nivel3.Attributes["Id_Pag"].Value);
                                                            }
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

                                                VersionCFDI = nodo.Attributes["Version"].Value;
                                                foreach (XmlNode Nodo_nivel2 in nodo.ChildNodes)
                                                {

                                                    if (Nodo_nivel2.Name == "AddendaKey")
                                                    {
                                                        foreach (XmlNode Nodo_nivel3 in Nodo_nivel2.ChildNodes)
                                                        {
                                                            if (Nodo_nivel3.Name == "PDF")
                                                            {
                                                                stringPDFCN = Nodo_nivel3.Attributes["ArchivoPDF"].Value;
                                                                idPag = Convert.ToInt32(Nodo_nivel3.Attributes["Id_Pag"].Value);
                                                            }
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
                    #endregion analizaXML

                    #region imprimirResultado
                    if (errorNum != "0")
                    {
                        this.Alerta(errorText.Remove(0, 1).Replace("'", "\"").Replace("|", "<br>"), 660, 150);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(errorNumCN) && errorNumCN != "0")
                        {

                        }
                        else
                        {
                            if (idPag == pago.Id_Pag)
                            {
                                List<PagoDetComplemento> complementos = new List<PagoDetComplemento>();
                                new CN_CapPago().ConsultaListaComplementosPago(pago.Id_Emp, pago.Id_Cd, pago.Id_Pag, Timbre.Cliente.Id_Cte.Value, folio, conexion, ref complementos);

                                List<PagoDet> ListaPagoDet = new List<PagoDet>();
                                PagoDet PagoDet;
                                foreach (var Factura in Timbre.Facturas)
                                {
                                    PagoDet = new PagoDet();
                                    PagoDet = list_pagos.Where(at => at.Ref.Replace(" ", string.Empty) == Factura.Id_Fac.ToString()).Select(row => row).First();
                                    ListaPagoDet.Add(PagoDet);
                                }

                                List<PagoDetComplemento> query = (from TComplementos in complementos
                                                                  join TListaPagoDet in ListaPagoDet on TComplementos.Id_PagDet equals TListaPagoDet.Id_PagDet
                                                                  where TComplementos.Id_PagComp == folio
                                                                  select TComplementos).ToList();

                                foreach (PagoDetComplemento item in query)
                                {
                                    item.Pago_FolioFiscal = folioFiscal;
                                    item.Pago_Pdf = this.Base64ToByte(stringPDF);
                                    if (!string.IsNullOrEmpty(Timbre.Cliente.Cte_PagoMetodoPago))
                                    {
                                        item.Cte_Fpago = Convert.ToInt32(Timbre.Cliente.Cte_PagoMetodoPago);
                                    }
                                    else
                                    {
                                        item.Cte_Fpago = Convert.ToInt32(Timbre.Facturas.First().Fac_FPago);
                                        if (item.Cte_Fpago == 99)
                                            item.Cte_Fpago = 3;
                                    }

                                    item.Cte_UsoCFDI = Timbre.Cliente.Cte_PagoUsoCFDI;
                                    System.Data.SqlTypes.SqlXml sqlXml;
                                    sqlXml = new System.Data.SqlTypes.SqlXml(new XmlTextReader(xmlSAT.OuterXml, XmlNodeType.Document, null));
                                    item.Pago_Xml = sqlXml;
                                    item.FechaCreacionXML = DateTime.Now;
                                    new CN_CapPago().ModificarComplementoPago(item, conexion, true, ref verificador);
                                    if (verificador < 0)
                                    {
                                        Alerta("No se pudo actualizar la información en el timbre");

                                    }
                                    else
                                    {

                                    }
                                }
                                string tempPDFname = nombrePDF;
                                string URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                                string WebURLtempPDF = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFname);
                                string URLtempXML;


                                URLtempXML = Server.MapPath("xmlSAT") + "\\PAGO_" + folio.ToString() + ".txt";
                                this.ByteToTempPDF(URLtempPDF, this.Base64ToByte(stringPDF));

                                System.Data.SqlTypes.SqlXml Xml;
                                Xml = new System.Data.SqlTypes.SqlXml(new XmlTextReader(xmlSAT.OuterXml, XmlNodeType.Document, null));
                                string docXML = "";

                                docXML = Xml.Value;

                                this.ByteToTempPDF(URLtempPDF, this.Base64ToByte(stringPDF));
                                RAM1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDFVarias('", WebURLtempPDF, "')"));

                                ObtenerPDFXML(this.Base64ToByte(stringPDF), docXML, query.FirstOrDefault(), ref WebURLtempPDF, ref URLtempXML);
                                EnviarCorreo(Timbre.Cliente.Cte_NomComercial, Timbre.ListaIntermediarioPago.Correo, WebURLtempPDF, URLtempXML);



                            }

                        }

                    }
                    #endregion imprimirResultado
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ObtenerPDFXML(object PDF, string XML, PagoDetComplemento pagoDetComplemento, ref string URLtempPDF, ref string URLtempXML)
        {
            try
            {
                Sesion sesion = (Sesion)Session["Sesion" + Session.SessionID];


                object pagoPDF = null;

                new CN_CapPago().ConsultaComplementoPagoConsultaDetlista(ref pagoDetComplemento, ref pagoPDF, sesion.Emp_Cnx);

                if (pagoDetComplemento == null)
                {
                    Alerta("El pago no ha sido timbrado");
                }
                else
                {
                    if (pagoDetComplemento.Pago_Xml == null)
                        Alerta("Los archivos no fueron generados al momento de timbrar. Favor de timbrar de nuevo");
                    else
                    {
                        byte[] archivoPdf = (byte[])PDF;
                        byte[] archivoPdfCN = PDF != System.DBNull.Value ? (byte[])PDF : new byte[0];
                        if (archivoPdf.Length > 0)
                        {
                            string tempPDFname = "PAGO";
                            tempPDFname = tempPDFname + pagoDetComplemento.Id_Emp.ToString() + "_" + pagoDetComplemento.Id_Cd.ToString() + "_" + pagoDetComplemento.Id_PagComp.ToString() + ".pdf";
                            URLtempPDF = Server.MapPath(string.Concat(ConfigurationManager.AppSettings["URLtempPDF"].ToString(), tempPDFname));
                            string WebURLtempPDF = string.Concat(ConfigurationManager.AppSettings["WebURLtempPDF"].ToString(), tempPDFname);
                            this.ByteToTempPDF(URLtempPDF, archivoPdf);
                            //RAM1.ResponseScripts.Add(string.Concat(@"AbrirFacturaPDFVarias('", WebURLtempPDF, "')"));
                        }

                        System.IO.StreamWriter sws = null;
                        URLtempXML = Server.MapPath("xmlSAT") + "\\PAGO_" + pagoDetComplemento.Id_PagComp.ToString() + ".txt";

                        if (File.Exists(URLtempXML))
                            File.Delete(URLtempXML);
                        if (File.Exists(Server.MapPath("xmlSAT") + "\\PAGO_" + pagoDetComplemento.Id_PagComp.ToString() + ".xml"))
                            File.Delete(Server.MapPath("xmlSAT") + "\\PAGO_" + pagoDetComplemento.Id_PagComp.ToString() + ".xml");
                        sws = new System.IO.StreamWriter(URLtempXML, false, Encoding.UTF8);
                        sws.WriteLine(pagoDetComplemento.Pago_Xml.ToString());
                        sws.Close();
                        File.Move(URLtempXML, Server.MapPath("xmlSAT") + "\\PAGO_" + pagoDetComplemento.Id_PagComp.ToString() + ".xml");
                        URLtempXML = Server.MapPath("xmlSAT") + "\\PAGO_" + pagoDetComplemento.Id_PagComp.ToString() + ".xml";
                    }
                }
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



        private void EnviarCorreo(string razonSocial, string CorreoInter, string URLtempPDF, string URLtempXML)
        {
            try
            {
                Sesion session = new Sesion();
                session = (Sesion)Session["Sesion" + Session.SessionID];
                string fecha = DateTime.Now.ToString("dd/MM/yyyy");
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
                cuerpo_correo.Append("A quien corresponda. <br><br> fecha: " + fecha + "<br>");
                cuerpo_correo.Append("Se le hace llegar los documentos PDF Y XMl <br>");
                cuerpo_correo.Append("Correspondiente al cliente con razón social " + razonSocial);
                cuerpo_correo.Append("</td></tr><tr><td colspan='2'>");
                cuerpo_correo.Append("<center><br>");
                cuerpo_correo.Append("</td></tr></table></div>");
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                SmtpClient sm = new SmtpClient(configuracion.Mail_Servidor, Convert.ToInt32(configuracion.Mail_Puerto));
                sm.Credentials = new NetworkCredential(configuracion.Mail_Usuario, configuracion.Mail_Contraseña);
                sm.EnableSsl = true;
                MailMessage m = new MailMessage();
                m.From = new MailAddress(configuracion.Mail_Remitente);
                char[] splitchar = { ';' };
                string[] Correos = CorreoInter.Split(splitchar);

                foreach (string correo in Correos)
                {
                    m.To.Add(new MailAddress(correo));
                }

                m.Attachments.Add(new Attachment(URLtempPDF));
                m.Attachments.Add(new Attachment(URLtempXML));
                m.Subject = "PDF y XML";
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
    }
}