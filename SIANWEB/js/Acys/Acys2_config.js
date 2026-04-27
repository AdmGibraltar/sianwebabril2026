/*
    Key Quimica Dic 2018 
    24 Dic 2018 Cracion RFH 
    9 Abrl 2019 Actualizado RFH
*/

var ACyS_PaginasDesplegar = 10; // desplegar en navegaro 
var ACyS_RenglonesXPagina = 15;
var ACyS_Paginacion_MaxPages = 15;
var ACyS_PaginaActual = 1;
var ACyS_MaxPaginas = 5;
var ACyS_RegistroEncontrados = 0;
var ACyS_ContadorListaProductos = 0;
var ACyS_CN_ContadorListaProductos = 0;
var ACyS_ContenedorBtnAplicar = 0;

var Cte = {
    'Cte_Credito': 0,
    'Cte_CondPago': 0,
    'Cte_LimCobr': 0,
    'Cte_Contado': 0,
    //FORMAS DE PAGO
    'Cte_Efectivo': 0,
    'Cte_Factoraje': 0,
    'Cte_Transferencia': 0,
    'Cte_Cheque': 0,
    'Cte_TarjetaDebito': 0,
    'Cte_TarjetaDebito': 0,
    'Cte_Deposito': 0,
    //REVISION
    // L M M J V S D CD
    'Cte_RLunes': 0,
    'Cte_RMartes': 0,
    'Cte_RMiercoles': 0,
    'Cte_RJueves': 0,
    'Cte_RViernes': 0,
    'Cte_RSabado': 0,
    'Cte_RDomingo': 0,
    'Cte_RCualquierDia': 0,

    'Cte_RHoraam1': 0,
    'Cte_RHoraam2': 0,
    'Cte_RHorapm1': 0,
    'Cte_RHorapm2': 0,
    //PAGO
    // L M M J V S D CD
    'Cte_CPLunes': 0,
    'Cte_CPMartes': 0,
    'Cte_CPMiercoles': 0,
    'Cte_CPJueves': 0,
    'Cte_CPViernes': 0,
    'Cte_CPSabado': 0,
    'Cte_CPDomingo': 0,
    'Cte_CPCualquierDia': 0,

    'Cte_PHoraam1': 0,
    'Cte_PHoraam2': 0,
    'Cte_PHorapm1': 0,
    'Cte_PHorapm2': 0
}

var ACyS_Servicio = {

    'Aplicar': 0,
    'Tipo': 0,
    // L M M J V S D CD
    'Lunes': 0,
    'Martes': 0,
    'Miercoles': 0,
    'Jueves': 0,
    'Viernes': 0,
    'Sabado': 0,
    'Domingo': 0,

    'CualquierDia': 0,

    'HorariosRecep1': "",
    'HorariosRecep2': "",
    'HorariosRecep3': "",
    'HorariosRecep4': ""
}

var ACyS_Entidad = {
    'Id_Emp': 0,
    'Id_Cd': 0,
    'Id_Acs': 0,
    'Id_Ter': 0,
    'Id_Rik': 0,
    'Id_Cte': 0,
    'Id_Ade': 0, // Adenda viene del cliente

    'Cte_PagoUsoCFDI': 0,

    'Acs_NomComercial': '',
    'Acs_Fecha': "",

    'Cd_Nombre': '',
    'Id_Corp': 0,

    // Contacto PRINCIPAL
    'Acs_Contacto': "",
    'Acs_Puesto': "",
    'Acs_Telefono': "",
    'Acs_email': "",

    'Acs_Contacto2': '',
    'Acs_Telefono2': '',
    'Acs_Email2': '',

    'Acs_Contacto3': '',
    'Acs_Telefono3': '',
    'Acs_email3': '',

    'Acs_Contacto4': '',
    'Acs_Telefono4': '',
    'Acs_email4': '',

    'Acs_Contacto5': '',
    'Acs_Telefono5': '',
    'Acs_email5': '',

    'Acs_Contacto6': "",
    'Acs_Telefono6': 0,
    'Acs_email6': "",
    'Acs_NumPrv': "",
    'Acs_Ruta1': 0,
    'Acs_Ruta2': 0,
    'Acs_ReqOrden': 0,
    'Acs_VigenciaApartir': '',
    'Acs_ReqConfirmacion': 0,
    'Acs_RecCorreo': 0,
    'Acs_RecFax': 0,
    'Acs_RecTelefono': 0,
    'Acs_RecRepresentante': 0,
    'Acs_RecOtro': 0,
    'Acs_RecOtroDesc': "",
    'Acs_Estatus': "",
    'Id_U': 0,
    'Acs_Semana': 0,
    'Acs_VigenciaTermina': "",
    'Acs_PedidoEncargadoEnviar': "",
    'Acs_PedidoPuesto': "",
    'Acs_Pedidotelefono': "", 'Acs_Pedidotelefono2': "",
    'Acs_PedidoEmail': "", 'Acs_PedidoEmail2': "",
    'Acs_RecDocReposicion': 0,
    'Acs_RecDocFolio': 0,
    'Acs_RecDocOtro': "",
    'Acs_Contado': 0,
    'Acs_VisitaOtro': "",
    'Acs_ReqServAsesoria': 0,
    'Acs_ReqServTecnicoRelleno': 0,
    'Acs_ReqServMantenimiento': 0,
    'Acs_Notas': "",
    'Acs_ContactoRepVenta': 0,
    'Acs_ContactoRepVentaTel': "",
    'Acs_ContactoRepVentaEmail': "",
    'Acs_ContactoRepServ': 0,
    'Acs_ContactoRepServTel': "",
    'Acs_ContactoRepServEmail': "",
    'Acs_ContactoJefServ': 0,
    'Acs_ContactoJefServTel': "",
    'Acs_ContactoJefServEmail': "",
    'Acs_ContactoAseServ': 0,
    'Acs_ContactoAseServTel': "",
    'Acs_ContactoAseServEmail': "",
    'Acs_ContactoJefOper': 0,
    'Acs_ContactoJefOperTel': "",
    'Acs_ContactoJefOperEmail': "",
    'Acs_ContactoCAlmRep': 0,
    'Acs_ContactoCAlmRepTel': "",
    'Acs_ContactoCAlmRepEmail': "",
    'Acs_ContactoCServTec': 0,
    'Acs_ContactoCServTecTel': "",
    'Acs_ContactoCServTecEmail': "",
    'Acs_ContactoCCreCob': 0,
    'Acs_ContactoCCreCobTel': "",
    'Acs_ContactoCCreCobEmail': "",
    'Acs_FechaInicio': '',
    'Acs_FechaFin': '',
    'Acs_Modalidad': "", // MODALIDAD
    'Acs_OrdenAbietaConReposicion': "", // ORDEN ABIERTA CON REPOSICION

    'Acs_ConsigFechaFin': "",
    'Acs_CanTotal': 0,
    'Acs_VisFrecuencia': 0,
    'Acs_version': 0,
    'Id_AcsVersion': 0,
    'Id_CteDirEntrega': 0,
    'Id_Val': 0,
    'EsCuentaNacional': "",
    'Acs_Sucursal': "",

    'Acs_ParcialidadesSi': 0,

    'Acs_ParcialidadesNo': 0,
    'Acs_ConfirmacionPedidosSI': 0,
    'Acs_ConfirmacionPedidosnO': 0,

    //
    // 2.2 DOCUMENTOS PARA ENTREGA Y RESEPCION 
    //
    // L M M J V S D CD
    'Acs_RecRevLunes': 0,
    'Acs_RecRevMartes': 0,
    'Acs_RecRevMiercoles': 0,
    'Acs_RecRevJueves': 0,
    'Acs_RecRevViernes': 0,
    'Acs_RecRevSabado': 0,
    'Acs_RecRevDomingo': 0,
    'Acs_RecRevCualquierDia': 0,

    'Acs_TimePicker1': "",
    'Acs_TimePicker2': "",
    'Acs_TimePicker3': "",
    'Acs_TimePicker4': "",
    'Acs_RecPersonaRecibe': "",
    'Acs_RecPuesto': "",
    'Acs_RecCitaMismoDia': 0,
    //
    'Acs_RecCitaSinCita': 0,
    'Acs_RecCitaPrevia': 0,
    'Acs_RecCitaContacto': "",
    'Acs_RecCitaTelefono': "",
    'Acs_RecCitaDiasdeAnticipacion': 0,
    'Acs_RecAreaPropia': 0,
    'Acs_RecAreaPlaza': 0,
    'Acs_RecAreaCalle': 0,
    'Acs_RecAreaAvTransitada': 0,
    'Acs_RecEstCortesia': 0,
    'Acs_RecEstCosto': 0,
    'Acs_RecEstMonto': 0,

    // Recepcion de Documentos
    'Acs_RecDocFactFranquiciaEnt': 0,
    'Acs_RecDocFactFranquiciaEntCop': 0,
    'Acs_RecDocFactFranquiciaRec': 0,
    'Acs_RecDocFactFranquiciaRecCop': 0,
    // Facturas Key
    'Acs_RecDocFactKeyEnt': 0,
    'Acs_RecDocFactKeyEntCop': '',
    'Acs_RecDocFactKeyRec': 0,
    'Acs_RecDocFactKeyRecCop': '',
    // Orden Compra 
    'Acs_RecDocOrdCompraEnt': 0,
    'Acs_RecDocOrdCompraEntCop': '',
    'Acs_RecDocOrdCompraRec': 0,
    'Acs_RecDocOrdCompraRecCop': '',
    // Orden de Reposicion
    'Acs_RecDocOrdReposEnt': 0,
    'Acs_RecDocOrdReposEntCop': '',
    'Acs_RecDocOrdReposRec': 0,
    'Acs_RecDocOrdReposRecCop': '',
    //Copia Pedidos
    'Acs_RecDocCopPedidoEnt': 0,
    'Acs_RecDocCopPedidoEntCop': '',
    'Acs_RecDocCopPedidoRec': 0,
    'Acs_RecDocCopPedidoRecCop': '',
    //Remision
    'Acs_RecDocRemisionEnt': 0,
    'Acs_RecDocRemisionEntCop': '',
    'Acs_RecDocRemisionRec': 0,
    'Acs_RecDocRemisionRecCop': '',

    //CERTIFICADO CALIDAD 1
    'Acs_RecDocCertificadoEnt': 0,
    'Acs_RecDocCertificadoEntCop': '',
    'Acs_RecDocCertificadoRec': 0,
    'Acs_RecDocCertificadoRecCop': '',

    // Pago de facturas 
    // OCT16-2019
    'Acs_CorreoRecibirFacturas': '',
    'Acs_CorreoRecibirComplemento': '',
    'Acs_CorreoRecibir_NA': '',

    // Eléctronica
    'RevFacEmail': 0,
    'RevFacEmailTexto': '',
    'RevFacEmailTexto2': '',
    'RevFacPortal': 0,
    'RevFacPortalTexto': '',
    'RevFacHttp': '',
    'RevFacUsuario': '',
    'RevFacContrasenia': '',

    'Acs_DocEntregaFormaPago': 0,

    'ACS_RecDocFolioEnt': 0,
    'ACS_RecDocFolioEntCop': 0,
    'ACS_RecDocFolioRec': 0,
    'ACS_RecDocFolioRecCop': 0,

    'ACS_RecDocContraRecEnt': 0,
    'ACS_RecDocContraRecEntCop': 0,
    'ACS_RecDocContraRecRec': 0,
    'ACS_RecDocContraRecRecCop': 0,

    'ACS_RecDocEntAlmacenEnt': 0,
    'ACS_RecDocEntAlmacenEntCop': 0,
    'ACS_RecDocEntAlmacenRec': 0,
    'ACS_RecDocEntAlmacenRecCop': 0,

    'ACS_RecDocSopServicioEnt': 0,
    'ACS_RecDocSopServicioEntCop': 0,
    'ACS_RecDocSopServicioRec': 0,
    'ACS_RecDocSopServicioRecCop': 0,

    'ACS_RecDocNomFirmaEnt': 0,
    'ACS_RecDocNomFirmaEntCop': 0,
    'ACS_RecDocNomFirmaoRec': 0,
    'ACS_RecDocNomFirmaRecCop': 0,

    'ACS_RecCitaEnt': 0,
    'ACS_RecCitaEntCop': 0,
    'ACS_RecCitaRec': 0,
    'ACS_RecCitaRecCop': 0,

    'ACS_RecOtroRec': "",

    // Servicios de Asesoria

    'ACS_chk62Aplicar': 0,
    'ACS_chk62Tipo': 0,
    'ACS_chk62Tipo2': 0,

    // L M M J V S D CD
    'ACS_chk62Lunes': 0,
    'ACS_chk62Martes': 0,
    'ACS_chk62Miercoles': 0,
    'ACS_chk62Jueves': 0,
    'ACS_chk62Viernes': 0,
    'ACS_chk62Sabado': 0,
    'ACS_chk62Domingo': 0,
    'ACS_chk62CualquierDia': 0,

    'ACS_chk62MismoDia': 0,
    'ACS_chk62Previa': 0,

    'ACS_RadTimePicker162': "",
    'ACS_RadTimePicker262': "",
    'ACS_RadTimePicker362': "",
    'ACS_RadTimePicker462': "",

    'ACS_txtRecPersonaRecibe62': "",
    'ACS_txtRecPuesto62': "",

    'ACS_Chk62Mismodia': 0,
    'ACS_Chk62Sincita': 0,
    'ACS_Chk62Previa': 0,
    'ACS_txt62CitaContacto': "",
    'ACS_txt62CitaTelefono': "",
    'ACS_txt62CitaDiasdeAnticipacion': 0,
    'ACS_chk62AreaPropia': 0,
    'ACS_chk62AreaPlaza': 0,
    'ACS_chk62AreaCalle': 0,
    'ACS_chk62AreaAvTransitada': 0,
    'ACS_chk62EstCortesia': 0,
    'ACS_chk62EstCosto': 0,
    'ACS_txt62EstMonto': 0,
    'ACS_txt62ClienteDireccion': "",
    'ACS_txt62ClienteColonia': "",
    'ACS_txt62ClienteMunicipio': "",
    'ACS_txt62ClienteEstado': "",
    'ACS_txt62ClienteCodPost': "",

    // Servicio CAPACITACION 
    // L M M J V S D CD
    'ACS_ServCap_Lunes': 0,
    'ACS_ServCap_Martes': 0,
    'ACS_ServCap_Miercoles': 0,
    'ACS_ServCap_Jueves': 0,
    'ACS_ServCap_Viernes': 0,
    'ACS_ServCap_Sabado': 0,
    'ACS_ServCap_Domingo': 0,
    'ACS_ServCap_CualquierDia': 0,

    'ACS_ServCap_HorariosRecep1': "",
    'ACS_ServCap_HorariosRecep2': "",
    'ACS_ServCap_HorariosRecep3': "",
    'ACS_ServCap_HorariosRecep4': "",

    // Servicio AUDITORIA 
    // L M M J V S D CD
    'ACS_ServAud_Lunes': 0,
    'ACS_ServAud_Martes': 0,
    'ACS_ServAud_Miercoles': 0,
    'ACS_ServAud_Jueves': 0,
    'ACS_ServAud_Viernes': 0,
    'ACS_ServAud_Sabado': 0,
    'ACS_ServAud_Domingo': 0,
    'ACS_ServAud_CualquierDia': 0,

    'ACS_ServAud_HorariosRecep1': "",
    'ACS_ServAud_HorariosRecep2': "",
    'ACS_ServAud_HorariosRecep3': "",
    'ACS_ServAud_HorariosRecep4': "",

    //Condiciones de Pago 
    //Documentos 

    'Acs_CondPagFac': 0,
    'Acs_CondPagFacCop': '',

    'Acs_CondPagOrdCom': 0,
    'Acs_CondPagOrdComCop': '',

    'Acs_CondPagOrdRep': 0,
    'Acs_CondPagOrdRepCop': '',

    'Acs_CondPagCopPed': 0,
    'Acs_CondPagCopPedCop': '',

    'Acs_CondPagRem': 0,
    'Acs_CondPagRemCop': '',

    // Documentos para entrega y recepción
    // ESPECIFICACIONES ADICIONALES
    // Factura Franquicia

    'ACS_chk62DocFactFranquiciaEnt': 0,
    'ACS_txt62DocFactFranquiciaEntCop': 0,
    'ACS_chk62DocFactFranquiciaRec': 0,
    'ACS_txt62DocFactFranquiciaRecCop': 0,

    // FACTURAS KEY
    'ACS_chk62DocFactKeyEnt': 0,
    'ACS_txt62DocFactKeyEntCop': 0,
    'ACS_chk62DocFactKeyRec': 0,
    'ACS_txt62DocFactKeyRecCop': 0,

    //ORDEN DE COMRRA RELEASE
    'ACS_chk62DocOrdCompraEnt': 0,
    'ACS_txt62DocOrdCompraEntCop': 0,
    'ACS_chk62DocOrdCompraRec': 0,
    'ACS_txt62DocOrdCompraRecCop': 0,

    // ORDEN DE REPSICION
    'ACS_chk62DocOrdReposEnt': 0,
    'ACS_txt62DocOrdReposEntCop': 0,
    'ACS_chk62DocOrdReposRec': 0,
    'ACS_txt62DocOrdReposRecCop': 0,

    // COPIA DE PEDIDO
    'ACS_chk62DocCopPedidoEnt': 0,
    'ACS_txt62DocCopPedidoEntCop': 0,
    'ACS_chk62DocCopPedidoRec': 0,
    'ACS_txt62DocCopPedidoRecCop': 0,

    // REMISION 
    'ACS_chk62DocRemisionEnt': 0,
    'ACS_txt62DocRemisionEntCop': 0,
    'ACS_chk62DocRemisionRec': 0,
    'ACS_txt62DocRemisionRecCop': 0,

    // CERTIFICADO CALIDAD
    'ACS_chk62DocCertificadoEnt': 0,
    'ACS_txt62DocCertificadoEntCop': 0,
    'ACS_chk62DocCertificadoRec': 0,
    'ACS_txt62DocCertificadoRecCop': 0,


    'ACS_chk62DocFolioEnt': 0,
    'ACS_txt62DocFolioEntCop': 0,
    'ACS_chk62DocFolioRec': 0,
    'ACS_txt62DocFolioRecCop': 0,

    'ACS_chk62DocContraRecEnt': 0,
    'ACS_txt62DocContraRecEntCop': 0,
    'ACS_chk62DocContraRecRec': 0,
    'ACS_txt62DocContraRecRecCop': 0,

    'ACS_chk62DocEntAlmacenEnt': 0,
    'ACS_txt62DocEntAlmacenEntCop': 0,
    'ACS_chk62DocEntAlmacenRec': 0,
    'ACS_txt62DocEntAlmacenRecCop': 0,

    'ACS_chk62DocSopServicioEnt': 0,
    'ACS_txt62DocSopServicioEntCop': 0,
    'ACS_chk62DocSopServicioRec': 0,
    'ACS_txt62DocSopServicioRecCop': 0,

    'ACS_chk62DocNomFirmaEnt': 0,
    'ACS_txt62DocNomFirmaEntCop': 0,
    'ACS_chk62DocNomFirmaoRec': 0,
    'ACS_txt62DocNomFirmaRecCop': 0,

    // Servicios de Valor
    // Servicio Tecnico.

    'ACS_chk62CitaEnt': 0,
    'ACS_txt62CitaEntCop': 0,
    'ACS_chk62CitaRec': 0,
    'ACS_txt62CitaRecCop': 0,

    // Servicio Tecnico 
    // L M M J V S D CD
    'ACS_chk63Aplicar': 0,
    'ACS_chk63Lunes': 0,
    'ACS_chk63Martes': 0,
    'ACS_chk63Miercoles': 0,
    'ACS_chk63Jueves': 0,
    'ACS_chk63Viernes': 0,
    'ACS_chk63Sabado': 0,
    'ACS_chk63Domingo': 0,
    'ACS_chk63CualquierDia': 0,

    'ACS_chk63MismoDia': 0,
    'ACS_chk63Previa': 0,

    'ACS_Rad63TimePicker163': "",
    'ACS_Rad63TimePicker263': "",
    'ACS_Rad63TimePicker363': "",
    'ACS_Rad63TimePicker463': "",

    'ACS_txtRecPersonaRecibe63': "",
    'ACS_txtRecPuesto63': "",
    'ACS_Chk63Mismodia': 0,
    'ACS_Chk63Sincita': 0,
    'ACS_Chk63Previa': 0,
    'ACS_txt63CitaContacto': "",
    'ACS_txt63CitaTelefono': "",
    'ACS_txt63CitaDiasdeAnticipacion': 0,
    'ACS_chk63AreaPropia': 0,
    'ACS_chk63AreaPlaza': 0,
    'ACS_chk63AreaCalle': 0,
    'ACS_chk63AreaAvTransitada': 0,
    'ACS_chk63EstCortesia': 0,
    'ACS_chk63EstCosto': 0,
    'ACS_txt63EstMonto': 0,
    'ACS_txt63ClienteDireccion': "",
    'ACS_txt63ClienteColonia': "",
    'ACS_txt63ClienteMunicipio': "",
    'ACS_txt63ClienteEstado': "",
    'ACS_txt63ClienteCodPost': "",

    'ACS_chk63DocFactFranquiciaEnt': 0,
    'ACS_txt63DocFactFranquiciaEntCop': 0,
    'ACS_chk63DocFactFranquiciaRec': 0,
    'ACS_txt63DocFactFranquiciaRecCop': 0,

    'ACS_chk63DocFactKeyEnt': 0,
    'ACS_txt63DocFactKeyEntCop': 0,
    'ACS_chk63DocFactKeyRec': 0,
    'ACS_txt63DocFactKeyRecCop': 0,

    'ACS_chk63DocOrdCompraEnt': 0,
    'ACS_txt63DocOrdCompraEntCop': 0,
    'ACS_chk63DocOrdCompraRec': 0,
    'ACS_txt63DocOrdCompraRecCop': 0,

    'ACS_chk63DocOrdReposEnt': 0,
    'ACS_txt63DocOrdReposEntCop': 0,
    'ACS_chk63DocOrdReposRec': 0,
    'ACS_txt63DocOrdReposRecCop': 0,

    'ACS_chk63DocCopPedidoEnt': 0,
    'ACS_txt63DocCopPedidoEntCop': 0,
    'ACS_chk63DocCopPedidoRec': 0,
    'ACS_txt63DocCopPedidoRecCop': 0,

    'ACS_chk63DocRemisionEnt': 0,
    'ACS_txt63DocRemisionEntCop': 0,
    'ACS_chk63DocRemisionRec': 0,
    'ACS_txt63DocRemisionRecCop': 0,

    'ACS_chk63DocFolioEnt': 0,
    'ACS_txt63DocFolioEntCop': 0,
    'ACS_chk63DocFolioRec': 0,
    'ACS_txt63DocFolioRecCop': 0,

    'ACS_chk63DocContraRecEnt': 0,
    'ACS_txt63DocContraRecEntCop': 0,
    'ACS_chk63DocContraRecRec': 0,
    'ACS_txt63DocContraRecRecCop': 0,

    'ACS_chk63DocEntAlmacenEnt': 0,
    'ACS_txt63DocEntAlmacenEntCop': 0,
    'ACS_chk63DocEntAlmacenRec': 0,
    'ACS_txt63DocEntAlmacenRecCop': 0,

    'ACS_chk63DocSopServicioEnt': 0,
    'ACS_txt63DocSopServicioEntCop': 0,
    'ACS_chk63DocSopServicioRec': 0,
    'ACS_txt63DocSopServicioRecCop': 0,

    'ACS_chk63DocNomFirmaEnt': 0,
    'ACS_txt63DocNomFirmaEntCop': 0,
    'ACS_chk63DocNomFirmaoRec': 0,
    'ACS_txt63DocNomFirmaRecCop': 0,

    'ACS_chk63CitaEnt': 0,
    'ACS_txt63CitaEntCop': 0,
    'ACS_chk63CitaRec': 0,
    'ACS_txt63CitaRecCop': 0,
    'Acs_NumericTextBox': 0,
    'Rec_Whats': 0,
    'Acs_OrdenAbiertaConRep': 0,

    'Acs_ParcialidadTipo': 0,

    'Acs_RecWhatsApp': 0,
    'Acs_RecRIK': 0,
    'Acs_RecPedWhats': 0,
    'Acs_EspecsAdic1': 0,
    'ACS_chk63CualquierDia': 0,
    'ACS_ST_Aplicar': 0,
    'ACS_SA_Aplicar': 0,
    //'ACS_chk62CualquierDia': 0,
    // L M M J V S D CD
    'ACS_SA_Tipo': 0,
    'ACS_SC_Lunes': 0,
    'ACS_SC_Martes': 0,
    'ACS_SC_Miercoles': 0,
    'ACS_SC_Jueves': 0,
    'ACS_SC_Viernes': 0,
    'ACS_SC_Sabado': 0,
    'ACS_SC_Domingo': 0,
    'ACS_SC_CualquierDia': 0,

    'ACS_SC_Aplicar': 0,
    'ACS_SC_Horario1': "",
    'ACS_SC_Horario2': "",
    'ACS_SC_Horario3': "",
    'ACS_SC_Horario4': "",
    'ACS_SC_CitaPrev_MismoDia': 0,
    'ACS_SC_CitaPrev_Pevia': 0,
    'ACS_SC_CitaPrev_Tipo': 0,
    'ACS_Aud_Aplicar': 0,
    'ACS_Aud_CitaPrev_Tipo': 0,

    // SERVICIO AUDITORIA
    // L M M J V S D CD
    'ACS_Aud_Lunes': 0,
    'ACS_Aud_Martes': 0,
    'ACS_Aud_Miercoles': 0,
    'ACS_Aud_Jueves': 0,
    'ACS_Aud_Viernes': 0,
    'ACS_Aud_Sabado': 0,
    'ACS_Aud_Domingo': 0,
    'ACS_Aud_CualquierDia': 0,

    'ACS_Aud_Horario1': "",
    'ACS_Aud_Horario2': "",
    'ACS_Aud_Horario3': "",
    'ACS_Aud_Horario4': "",

    'ACS_Aud_CitaPrev_MismoDia': 0,
    'ACS_Aud_CitaPrev_Pevia': 0,
    'ACS_SC_Tipo': 0,
    'ACS_Aud_Tipo': 0,

    'Acs_CondPagEntFac': 0,
    'Acs_CondPagEntFacCop': "",
    'Acs_CondPagReFac': 0,
    'Acs_CondPagReFacCop': "",

    'Acs_CondPagEntOrdCom': 0,
    'Acs_CondPagEntOrdComCop': "",
    'Acs_CondPagReOrdCom': 0,
    'Acs_CondPagReOrdComCop': "",

    'Acs_CondPagEntOrdRep': 0,
    'Acs_CondPagEntOrdRepCop': "",
    'Acs_CondPagReOrdRep': 0,
    'Acs_CondPagReOrdRepCop': "",

    'Acs_CondPagEntCopPed': 0,
    'Acs_CondPagEntCopPedCop': "",
    'Acs_CondPagReCopPed': 0,
    'Acs_CondPagReCopPedCop': "",

    'Acs_CondPagEntPagRem': 0,
    'Acs_CondPagEntPagRemCop': "",
    'Acs_CondPagRePagRem': 0,
    'Acs_CondPagRePagRemCop': "",

    'Documento_PermiteEditara': 0,

    'RevFacEmail': 0,
    'RevFacEmailTexto': "",
    'RevFacPortal': 0,
    'RevFacPortalTexto': "",
    'RevFacHttp': "",
    'RevFacUsuario': "",
    'RevFacContrasenia': "",

    'ServTecnico': {
        'Aplicar': 0,
        'Tipo': 0, 'Tipo1': 0, 'Tipo2': 0,
        // L M M J V S D CD
        'Lunes': 0,
        'Martes': 0,
        'Miercoles': 0,
        'Jueves': 0,
        'Viernes': 0,
        'Sabado': 0,
        'Domingo': 0,
        'CualquierDia': 0,

        'HorariosRecep1': "",
        'HorariosRecep2': "",
        'HorariosRecep3': "",
        'HorariosRecep4': "",

        'CitaServ_MismoDia': 0,
        'CitaServ_Previa': 0,

        'ServRelleno': 0,
        'ServPreventivo': 0,
        // OCT23-2019        
        'QuienRecibe': '',
        'FuncionQuienRecibe': '',
        'Frecuencia': 0
    },

    'ServCapacitacion': {
        'Aplicar': 0,
        'Tipo': 0, 'Tipo1': 0, 'Tipo2': 0,
        // L M M J V S D CD
        'Lunes': 0,
        'Martes': 0,
        'Miercoles': 0,
        'Jueves': 0,
        'Viernes': 0,
        'Sabado': 0,
        'Domingo': 0,
        'CualquierDia': 0,

        'HorariosRecep1': "",
        'HorariosRecep2': "",
        'HorariosRecep3': "",
        'HorariosRecep4': "",
        'CitaServ_MismoDia': 0,
        'CitaServ_Previa': 0,
        'Frecuencia': 0
    },

    'ServAuditoria': {
        'Aplicar': 0,
        'Tipo': 0, 'Tipo1': 0, 'Tipo2': 0,
        // L M M J V S D CD
        'Lunes': 0,
        'Martes': 0,
        'Miercoles': 0,
        'Jueves': 0,
        'Viernes': 0,
        'Sabado': 0,
        'Domingo': 0,
        'CualquierDia': 0,

        'HorariosRecep1': "",
        'HorariosRecep2': "",
        'HorariosRecep3': "",
        'HorariosRecep4': "",

        'CitaServ_MismoDia': 0,
        'CitaServ_Previa': 0,
        'Frecuencia': 0
    },

    'ServAsesoria': {
        'Aplicar': 0,
        'Tipo': 0, 'Tipo1': 0, 'Tipo2': 0,
        // L M M J V S D CD
        'Lunes': 0,
        'Martes': 0,
        'Miercoles': 0,
        'Jueves': 0,
        'Viernes': 0,
        'Sañbado': 0,
        'Domingo': 0,
        'CualquierDia': 0,

        'HorariosRecep1': "",
        'HorariosRecep2': "",
        'HorariosRecep3': "",
        'HorariosRecep4': "",

        'CitaServ_MismoDia': 0,
        'CitaServ_Previa': 0,
        'Frecuencia': 0
    },

    'Acs_ComentariosRecomendaciones': '',

    'Acs_RevisionFolio': 0,
    'Acs_RevisionEntAlmacen': 0,
    'Acs_RevisionOrdenCompra': 0,
    'Acs_RevisionRepConsumo': 0,
    'Acs_RevisionCopiaFactura': 0,

    'Acs_RevisionOtroDoc': '',

    'Acs_PagoContraEntrega': 0,
    'Acs_VisitaGestorCobranza': 0

}