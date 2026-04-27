/*
Key Quimica Dic 2018 
24 Dic 2018 RFH 
9 Abr 2019 Actualizado RFH 
*/

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function ConvertirEnter_SiNoVacio(val) {
    var Res = '';
    var tmpVal = 0;

    tmpVal = parseInt(val);
    if (isNaN(tmpVal)) {
        tmpVal = 0;
    }

    if (tmpVal > 0) {
        Res = tmpVal;
    } else {
        Res = '';
    }

    return Res;
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
//OCT24-2019
function OrdenCliente_CargarCombos() {

    Cargar_Combo_FrecuenciaVisita($("#tbVis_Frecuencia"), function () {
        $('#tbVis_Frecuencia').val(0);
    });

    Cargar_Combo_FrecuenciaVisita($("#ddlST_Frecuencia"), function () {
        $('#ddlST_Frecuencia').val(0);
    });

    Cargar_Combo_FrecuenciaVisita($("#ddlServCap_Frecuencia"), function () {
        $('#ddlServCap_Frecuencia').val(0);
    });

    Cargar_Combo_FrecuenciaVisita($("#ddlServAud_Frecuencia"), function () {
        $('#ddlServAud_Frecuencia').val(0);
    });

    Cargar_Combo_FrecuenciaVisita($("#ddlServAsesoria_Frecuencia"), function () {
        $('#ddlServAsesoria_Frecuencia').val(0);
    });

    //

    Cargar_ComboTipo_TipoServicio(1, $("#sel_ServCap_Tipo1"), function () {
        $('#sel_ServCap_Tipo1').val(0);
        console.log('sel_ServCap_Tipo1:' + 0);
    });

    Cargar_ComboTipo_TipoServicio(2, $("#sel_ServAud_Tipo1"), function () {
        $("#sel_ServAud_Tipo1").val(0);
    });

    Cargar_ComboTipo_TipoServicio(3, $("#sel_ServAsesoria_Tipo1"), function () {
        $('#sel_ServAsesoria_Tipo1').val(0);
    });

}


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// LIMPIAR TODOS LOS CAMPOS 
// ODEN DEL CLIENTE de ACYS
// 
function OrdenCliente_Inicializar() {

    $('#lbOrdenClienteTitulo').text('Control de Ordenes / Folio:');

    // General 
    $('#hfId_Emp').val(0);
    $('#hfId_Cd').val(0);
    $('#hfId_Acs').val(0);
    $('#hfId_AcsVersion').val(0);

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    // TAB :: Cliente                
    //

    // NUMERO
    $('#tbId_Cte').text(0);

    //NOMBRE
    $('#tbCte_Nombre').text('');

    $('#tbCDI').text('');

    // CUENTA CORPORATIVA
    $('#chbEspAd_CuentaCorporativa').val(0);

    //Nombre Comercial
    $('#tbNombreComercial').text('');

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    // TAB :: RECEPCION DE PEDIDIOS
    //

    // Frecuencia Establecida            
    $('#chbAcs_Modalidad').prop('checked', false);
    // Orden Abierta con Reposicion 
    $('#chbOrdenAbiertaConRep').prop('checked', false);

    // Acepta Parcialdiades    
    $('#selAcs_ParcialidadesSi').val(0);

    // RECEPION DE PEDIDOS
    // -> MODALIDAD DE PEDIDO
    $('#tblAceptaParcialidades').css('disaply', 'none');
    $('#cbParcialidad_Entrega_Rem').prop('checked', false);
    $('#cbParcialidad_Entrega_Fac').prop('checked', false);

    // Email
    $('#chbRecCorreo').prop('checked', false);

    // Telefono 
    $('#chbRecTelefono').prop('checked', false);

    // Whatsup TODO: No existe hay que crear el campo y todo lo que implica             
    $('#chbRecWhatsApp').prop('checked', false);

    // Recolectado por RIK            
    $('#chbRecRIK').prop('checked', false);

    // TODO: Requiere Confirmacion de Pedido            
    $('#chbRecConfirmacion').prop('checked', false);

    // TODO: Otro            
    $('#chbRecOtro').prop('checked', false);

    $('#txtRecDocOtro').val('');

    // TODO: Encargado de enviar pedido 
    $('#tbAcs_PedidoEncargadoEnviar').val('');
    // Puesto             
    $('#tbAcs_PedidoPuesto').val("ERROR");
    // Telefono                        
    $('#tbAcs_PedidoTelefono').val('');
    $('#tbAcs_PedidoTelefono2').val('');
    // Mail pedido
    $('#tbAcs_PedidoEmail').val('');
    $('#tbAcs_PedidoEmail2').val('');


    // PANEL : DOCUMENTOS PARA ENTREGA RECEPCION
    // Lunes Martes Miercoles Jueves Viernes Otros 
    // L M M J V S D CD
    $('#chbAcs_RecRevLunes').prop('checked', false);
    $('#chbAcs_RecRevMartes').prop('checked', false);
    $('#chbAcs_RecRevMiercoles').prop('checked', false);
    $('#chbAcs_RecRevJueves').prop('checked', false);
    $('#chbAcs_RecRevViernes').prop('checked', false);
    $('#chbAcs_RecRevSabado').prop('checked', false);
    $('#chbAcs_RecRevDomingo').prop('checked', false);
    $('#chbAcs_RecRevCualquierDia').prop('checked', false);

    // Horarios Entrega
    $('#tbAcs_TimePicker1').val('');
    $('#tbAcs_TimePicker2').val('');
    $('#tbAcs_TimePicker3').val('');
    $('#tbAcs_TimePicker4').val('');

    //Persona recibe
    $('#tbAcs_RecPersonaRecibe').val('');

    // Cita para entregar 
    $('#chbAcs_RecCitaSinCita').prop('checked', false); // Sin cita
    $('#chbAcs_RecCitaMismoDia').prop('checked', false); // Mismo Dia     
    $('#chbAcs_RecCitaPrevia').prop('checked', false); // Previa

    //
    // Areas de recibo
    //

    $('#chbAcs_RecAreaPropia').prop('checked', false); // Propia             
    $('#chbAcs_RecAreaPlaza').prop('checked', false); // Plaza    
    $('#chbAcs_RecAreaCalle').prop('checked', false); // Calle

    //
    // Estacionamiento 
    //

    $('#chkAcs_RecEstCortesia').prop('checked', false); // Cortecia    
    $('#chkAcs_RecEstCosto').prop('checked', false); // Costo     
    $('#tbAcs_RecEstMonto').val(''); // Monto 

    //
    // 2 RECEPCION DE PEDIDOS / 2.2 Documentos Entrega Recepcion 
    // Especificaciones Adicionales (1) 
    //
    $('#chbAcs_EspecsAdic1').prop('checked', false);
    $('#faAcs_EspecsAdic1').removeClass('fa fa-minus-circle fa-lg');
    $('#faAcs_EspecsAdic1').addClass('fa fa-plus-circle fa-lg');
    $('#divAcs_EspecsAdic1').css('display', 'none');

    $('#divAcs_EspecsAdic1').css('display', 'none');

    // Factura Key        
    $('#chkAcs_RecDocFactKeyEnt').prop('checked', false);
    $('#tbAcs_RecDocFactKeyEntCop').val('');
    $('#chkAcs_RecDocFactKeyRec').prop('checked', false);
    $('#tbAcs_RecDocFactKeyRecCop').val('');

    // Orden de compra
    $('#chkAcs_RecDocOrdCompraEnt').prop('checked', false);
    $('#tbAcs_RecDocOrdCompraEntCop').val('');
    $('#chkAcs_RecDocOrdCompraRec').prop('checked', false);
    $('#tbAcs_RecDocOrdCompraRecCop').val('');

    //Orden de Reposicion 
    $('#chkAcs_RecDocOrdReposEnt').prop('checked', false);
    $('#tbAcs_RecDocOrdReposEntCop').val('');
    $('#chkAcs_RecDocOrdReposRec').prop('checked', false);
    $('#tbAcs_RecDocOrdReposRecCop').val('');

    //Copia de Pedido
    $('#chkAcs_RecDocCopPedidoEnt').prop('checked', false);
    $('#tbAcs_RecDocCopPedidoEntCop').val('');
    $('#chkAcs_RecDocCopPedidoRec').prop('checked', false);
    $('#tbAcs_RecDocCopPedidoRecCop').val('');

    //Remision 
    $('#chkAcs_RecDocRemisionEnt').prop('checked', false);
    $('#tbAcs_RecDocRemisionEntCop').val('');
    $('#chkAcs_RecDocRemisionRec').prop('checked', false);
    $('#tbAcs_RecDocRemisionRecCop').val('');

    //CERTIFICADO 1         
    $('#chkAcs_RecDocCertificadoEnt').prop('checked', false);
    $('#tbAcs_RecDocCertificadoEntCop').val('');
    $('#chkAcs_RecDocCertificadoRec').prop('checked', false);
    $('#tbAcs_RecDocCertificadoRecCop').val('');

    //
    // Condiciones de Pago
    // Documentos para entrega y recepcion
    // Especificaciones Adicionales  (2)
    //
    $('#chbACS_chk62DocFactKeyEnt').prop('checked', false);
    $('#tbACS_txt62DocFactKeyEntCop').val('');
    $('#chbACS_chk62DocFactKeyRec').prop('checked', false);
    $('#tbACS_txt62DocFactKeyRecCop').val('');

    $('#chbACS_chk62DocOrdCompraEnt').prop('checked', false);
    $('#tbACS_txt62DocOrdCompraEntCop').val('');
    $('#chbACS_chk62DocOrdCompraRec').prop('checked', false);
    $('#tbACS_txt62DocOrdCompraRecCop').val('');

    $('#chbACS_chk62DocOrdReposEnt').prop('checked', false);
    $('#tbACS_txt62DocOrdReposEntCop').val('');
    $('#chbACS_chk62DocOrdReposRec').prop('checked', false);
    $('#tbACS_txt62DocOrdReposRecCop').val('');

    $('#chbACS_chk62DocCopPedidoEnt').prop('checked', false);
    $('#tbACS_txt62DocCopPedidoEntCop').val('');
    $('#chbACS_chk62DocCopPedidoRec').prop('checked', false);
    $('#tbACS_txt62DocCopPedidoRecCop').val('');

    $('#chbACS_chk62DocRemisionEnt').prop('checked', false);
    $('#tbACS_txt62DocRemisionEntCop').val('');
    $('#chbACS_chk62DocRemisionRec').prop('checked', false);
    $('#tbACS_txt62DocRemisionRecCop').val('');

    $('#chbACS_chk62DocRemisionEnt').prop('checked', false);
    $('#tbACS_txt62DocRemisionEntCop').val('');
    $('#chbACS_chk62DocRemisionRec').prop('checked', false);
    $('#tbACS_txt62DocRemisionRecCop').val('');

    // CERTIFICADO 2
    $('#chbACS_chk62DocCertificadoEnt').prop('checked', false);
    $('#tbACS_txt62DocCertificadoEntCop').val('');
    $('#chbACS_chk62DocCertificadoRec').prop('checked', false);
    $('#tbACS_txt62DocCertificadoRecCop').val('');

    // Metodo Forma de Pago 
    $('#tbMetodoFormaPago').val('');

    // Agenda 
    $('#tbMetodoFormaPago').val('');

    // Uso de CFDI
    $('#tbMetodoFormaPago').val('');

    // 5 CONDICIONES DE PAGO / 5.3 Pago de Facturas / Especificaciones Adicinales

    // Controles de Cliente / MODO SOLO LECTURA

    // Documentos para entrega y recepción / MODO SOLO LECTURA

    $('#chkCredito').prop('checked', false);
    $('#chkCredito').attr("disabled", "disabled");

    $('#chkContado').prop('checked', false);
    $('#chkContado').attr("disabled", "disabled");

    $('#txtDias').val('');
    $('#txtDias').attr("disabled", "disabled");
    $('#txtDias').prop('readonly', true);

    $('#txtLimite').val('');
    $('#txtLimite').attr("disabled", "disabled");
    $('#txtLimite').prop('readonly', true);

    // cmbCPBancoDeposita
    $('#cmbCPBancoDeposita').attr("disabled", "disabled");
    $('#cmbCPBancoDeposita').prop('readonly', true);

    $('#txtCPNumCuenta').val('');
    $('#txtCPNumCuenta').attr("disabled", "disabled");
    $('#txtCPNumCuenta').prop('readonly', true);

    $('#txtCPRefTecleada').val('');
    $('#txtCPRefTecleada').attr("disabled", "disabled");
    $('#txtCPRefTecleada').prop('readonly', true);

    // Formas de pago

    Empty_Disable_Chb('#chkEfectivo');
    Empty_Disable_Chb('#chkFactoraje');
    Empty_Disable_Chb('#chkTransferencia');
    Empty_Disable_Chb('#chkCheque');
    Empty_Disable_Chb('#chkDeposito');
    Empty_Disable_Chb('#chkTarjetaDebito');
    Empty_Disable_Chb('#chkTarjetaCredito');

    Empty_Disable_Tb('#txtCPUnicoDia');
    Empty_Disable_Tb('#txtCPDiaMaximo');
    Empty_Disable_Tb('#txtCPCuentasPagar');
    Empty_Disable_Tb('#txtCPCuentaPago');

    $('#txtCPDiasPagoDireccion').val('');
    $('#txtCPColonia').val('');
    $('#txtCPMunicipio').val('');

    $('#txtCPEstado').val('');
    $('#txtCPCP').val('');

    $('#txtCPCiudad').val('');
    $('#txtCPTelefonos').val('');

    //
    // REVISION DE FACTURAS
    //

    Empty_Disable_Chb('#chkRevisionLunes');
    Empty_Disable_Chb('#chkRevisionMartes');
    Empty_Disable_Chb('#chkRevisionMiercoles');
    Empty_Disable_Chb('#chkRevisionJueves');
    Empty_Disable_Chb('#chkRevisionViernes');
    Empty_Disable_Chb('#chkRevisionSabado');
    Empty_Disable_Chb('#chkRevisionDomingo');
    Empty_Disable_Chb('#chkRevisionCualquierDia');

    //Horarios de revision
    Empty_Disable_Tb('#tpRevisionMañanaInicio');
    Empty_Disable_Tb('#tpRevisionMañanaFin');
    Empty_Disable_Tb('#tpRevisionTardeInicio');
    Empty_Disable_Tb('#tpRevisionTardeFin');

    // Test - MAY27-2020
    // Documentos adicionales
    $('#chkRevFolio').prop('checked', false);
    $('#chkRevRntrada').prop('checked', false);
    $('#chkRevOrden').prop('checked', false);
    $('#chkRevReporte').prop('checked', false);
    $('#chkRevCopia').prop('checked', false);

    // MAY31-2020
    //Empty_Disable_Chb('#chkRevFolio');
    //Empty_Disable_Chb('#chkRevRntrada');
    //Empty_Disable_Chb('#chkRevOrden');
    //Empty_Disable_Chb('#chkRevReporte');
    //Empty_Disable_Chb('#chkRevCopia');




    // Pago de facturas
    // L M M J V S D CD
    Empty_Disable_Chb('#chkPagoLunes');
    Empty_Disable_Chb('#chkPagoMartes');
    Empty_Disable_Chb('#chkPagoMiercoles');
    Empty_Disable_Chb('#chkPagoJueves');
    Empty_Disable_Chb('#chkPagoViernes');
    Empty_Disable_Chb('#chkPagoSabado');
    Empty_Disable_Chb('#chkPagoDomingo');
    Empty_Disable_Chb('#chkPagoCualquierDia');

    Empty_Disable_Tb('#tpPagoMañanaInicio');
    Empty_Disable_Tb('#tpPagoMañanaFin');
    Empty_Disable_Tb('#tpPagoTardeInicio');
    Empty_Disable_Tb('#tpPagoTardeFin');

    $('#chkRevFacContraEntrega').prop('checked', false);
    $('#chkRevFacVisGestorCobranza').prop('checked', false);

    Empty_Disable_Tb('#txtRevFacDireccion');
    Empty_Disable_Tb('#txtRevFacColonia');
    Empty_Disable_Tb('#txtRevFacMunicipio');
    Empty_Disable_Tb('#txtRevFacEstado');
    Empty_Disable_Tb('#txtRevFacCP');

    Empty_Disable_Tb('#txtRevFacCiudad');
    Empty_Disable_Tb('#txtRevFacTeléfonos');

    $('#CPH_chk').prop('checked', false);

    $('#txtRevFacEmailTexto').val('');
    $('#chkRevFacPortal').prop('checked', false);
    $('#txtRevFacPortalTexto').val('');
    $('#txtRevFacHttp').val('');
    $('#txtRevFacUsuario').val('');
    $('#txtRevFacContrasenia').val('');

    $('#chbCondPag_EspAdic').prop('checked', false);
    $('#divCondPago_EspAdic').css('display', 'none');

    $('#chbCondPag_EspAdic').prop('checked', false);

    $('#chbEA1_FactKeyChb1').prop('checked', false);
    $('#tbEA1_FactKeyCopias1').val('');
    $('#chbEA1_FactKeyChb2').prop('checked', false);
    $('#tbEA1_FactKeyCopias2').val('');

    $('#chbEA1_Ordcompchb1').prop('checked', false);
    $('#tbEA1_OrdcompCopias1').val('');
    $('#chbEA1_Ordcompchb2').prop('checked', false);
    $('#tbEA1_OrdcompCopias2').val('');

    $('#chEA1_OrdRepChb1').prop('checked', false);
    $('#tbEA1_OrdRepCopias1').val('');
    $('#chEA1_OrdRepChb2').prop('checked', false);
    $('#tbEA1_OrdRepCopias2').val('');

    $('#chbEA1_CopPedChb1').prop('checked', false);
    $('#tbEA1_CopPedCopias1').val('');
    $('#chbEA1_CopPedChb2').prop('checked', false);
    $('#tbEA1_CopPedCopias2').val('');

    $('#chbEA1_RemChb1').prop('checked', false);
    $('#tbEA1_RemCopias1').val('');
    $('#chbEA1_RemChb2').prop('checked', false);
    $('#tbEA1_RemCopias2').val('');


    /* /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    Visita de Representante (6.1)
    /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ */

    $('#tbVis_Frecuencia').val(0);

    $('#tbVis_Frecuencia').val(0);

    /*  
    /\/\/\/\/\/\/\/\/\/\/\ 
    -> 
    1/4 Servicio Tecnico (6.3) 
    /\/\/\/\/\/\/\/\/\/\/\ 
    */

    // Check Servicio de Reyeno
    //$('#chbReq_ServTec').val('');

    // Dias de recepcion
    /*
    $('#chk63Lunes').val('');
    $('#chk63Martes').val();
    $('#chk63Miercoles').val(RES.ACS_chk63Miercoles);
    $('#chk63Jueves').val(RES.ACS_chk63Jueves);
    $('#chk63Viernes').val(RES.ACS_chk63Viernes);
    $('#chk63Sabado').val(RES.ACS_chk63Sabado);
    */

    //Horarios de recepcion 
    /*
    $('#Rad63TimePicker163').val(RES.ACS_Rad63TimePicker163);
    $('#Rad63TimePicker263').val(RES.ACS_Rad63TimePicker263);
    $('#Rad63TimePicker363').val(RES.ACS_Rad63TimePicker363);
    $('#Rad63TimePicker463').val(RES.ACS_Rad63TimePicker463);
    */
    //Cita para servicio 

    // Mismo Dia
    $('#Chk63Mismodia').val('');
    // Previa
    $('#Chk63Previa').val('');

    //Servicio de reyeno 
    $('#ChkServTecnicoRelleno').prop('checked', false);

    //Servicio preventivo
    $('#ChkServMantenimiento').prop('checked', false);

    /*  
    /\/\/\/\/\/\/\/\/\/\/\ 
    -> 2/4 Capacitacion (=) 
    /\/\/\/\/\/\/\/\/\/\/\ 
    */

    // Tipo de capacitacion
    //$('#ddlDs_Cap_TipoCapacitacion').val(0);

    // Campos Nuevos 
    /*
    $('#chkDS_C_Lunes').val(RES.ACS_DS_C_Lunes);
    $('#chkDS_C_Martes').val(RES.ACS_DS_C_Martes);
    $('#chkDS_C_Miercoles').val(RES.ACS_DS_C_Miercoles);
    $('#chkDS_C_ueves').val(RES.ACS_DS_C_Jueves);
    $('#chkDS_C_Viernes').val(RES.ACS_DS_C_Viernes);
    $('#chkDS_C_Cualquier').val(RES.ACS_DS_Cualquier);
    */

    // Horarios de recepcion
    /*
    $('#tbTsCap_HRec1').val(RES.ACS_tbTsCap_HRec1);
    $('#tbTsCap_HRec2').val(RES.ACS_tbTsCap_HRec2);
    $('#tbTsCap_HRec3').val(RES.ACS_tbTsCap_HRec3)
    $('#tbTsCap_HRec4').val(RES.ACS_tbTsCap_HRec4)
    */

    //Cita para servicio:
    /*
    // Mismo Dia 
    $('#tbTsCap_CitaServ').prop('checked', RES.ACS_TsCap_CitaServ);
    // Previa 
    $('#tbTsCap_MismoDia').prop('checked', RES.ACS_TsCap_MismoDia);
    */


    /* /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    -> 3/4 Auditoria (?)
    */
    /*
    // Tipo de capacitacion
    $('#ddlDs_Aud_TipoCapacitacion').val(0);
    // Campos Nuevos 
    $('#chkDS_A_Lunes').val(RES.ACS_DS_A_Lunes);
    $('#chkDS_A_Martes').val(RES.ACS_DS_A_Martes);
    $('#chkDS_A_Miercoles').val(RES.ACS_DS_A_Miercoles);
    $('#chkDS_A_ueves').val(RES.ACS_DS_A_Jueves);
    $('#chkDS_A_Viernes').val(RES.ACS_DS_A_Viernes);
    $('#chkDS_A_Cualquier').val(RES.ACS_DS_A_Cualquier);
    // Horarios de recepcion
    $('#tbTsCap_A_HRec1').val(RES.ACS_tbTsCap_A_HRec1);
    $('#tbTsCap_A_HRec2').val(RES.ACS_tbTsCap_A_HRec2);
    $('#tbTsCap_A_HRec3').val(RES.ACS_tbTsCap_A_HRec3)
    $('#tbTsCap_A_HRec4').val(RES.ACS_tbTsCap_A_HRec4)
    //Cita para servicio:
    // Mismo Dia 
    $('#tbTsCap_A_CitaServ').prop('checked', RES.ACS_TsCap_A_CitaServ);
    // Previa 
    $('#tbTsCap_A_MismoDia').prop('checked', RES.ACS_TsCap_A_MismoDia);
    */

    /*
    $('#tbAcys_Folio').val('');
    $('#tbAcys_FechaInicio').val('');
    $('#tbAcys_FechaFin').val('');
    $('#ddlAcys_Estatus').val('');
    $('#tbAcys_CteNombre').val('');
    $('#tbAcys_CteNumero').val('');
    $('#tbAcys_CteDireccion').val('');
    $('#tbAcys_CteCol').val('');
    $('#tbAcys_CteMunicipio').val('');
    $('#tbAcys_CteCP').val('');
    $('#tbAcys_CteRFC').val('');
    $('#tbAcys_CteTerritorio').val('');
    $('#tbAcys_RikMombre').val('');
    $('#tbAcys_RikNumero').val('');
    */

    // * FROMA DE ENVIO DE PEDIDO

    // Recepcion de pedidos        
    $('#chbRecCorreo').prop('checked', false);
    $('#chbRecTelefono').prop('checked', false);
    $('#chbFormaEnvio_whats').prop('checked', false); //***        
    $('#chbRecConfirmacion').prop('checked', false);
    $('#chbFormaEnvio_confirma').prop('checked', false);
    $('#chbRecOtro').prop('checked', false);
    $('#chbRecOtroDesc').prop('checked', false);

    $('#tbAcs_PedidoEncargadoEnviar').val('');
    $('#tbAcs_PedidoPuesto').val('');
    $('#tbAcs_PedidoTelefono').val('');
    $('#tbAcs_PedidoTelefono2').val('');
    $('#tbAcs_PedidoEmail').val('');
    $('#tbAcs_PedidoEmail2').val('');

    //
    // SERVICIOS DE VALOR 
    //            

    //- SERVICIO TECNICO 
    //- SERVICIO TECNICO 
    //- SERVICIO TECNICO 

    $('#chb_ST_Aplicar').prop('checked', false);
    $('#trReqServTec').css('display', 'none');

    // L M M J V S D CD
    $('#chb_ST_Lunes').prop('checked', false);
    $('#chb_ST_Martes').prop('checked', false);
    $('#chb_ST_Miercoles').prop('checked', false);
    $('#chb_ST_Jueves').prop('checked', false);
    $('#chb_ST_Viernes').prop('checked', false);
    $('#chb_ST_Sabado').prop('checked', false);
    $('#chb_ST_Domingo').prop('checked', false);
    $('#chb_ST_CualquierDia').prop('checked', false);

    $('#tb_ST_HorariosRecep1').val('');
    $('#tb_ST_HorariosRecep2').val('');
    $('#tb_ST_HorariosRecep3').val('');
    $('#tb_ST_HorariosRecep4').val('');

    $('#chb_ST_MismoDia').prop('checked', false);
    $('#chb_ST_Previa').prop('checked', false);

    $('#chb_ST_Relleno').prop('checked', false);
    $('#Chb_ST_Mantenimiento').prop('checked', false);

    //
    // 
    // CAPACITACION 
    // 
    //

    $('#chb_ServCapacitacion').prop('checked', false);
    $('#trReqServCapacitacion').css('display', 'none');

    $('#sel_ServCap_Tipo1').val(0);

    $('#sel_ServCap_Tipo2').val(0);
    $('#sel_ServCap_Tipo2').empty();
    $('#sel_ServCap_Tipo2').append('<option value="0">-</option>')

    // L M M J V S D CD
    $('#chb_ServCap_Lunes').prop('checked', false);
    $('#chb_ServCap_Martes').prop('checked', false);
    $('#chb_ServCap_Miercoles').prop('checked', false);
    $('#chb_ServCap_Jueves').prop('checked', false);
    $('#chb_ServCap_Viernes').prop('checked', false);
    $('#chb_ServCap_Sabado').prop('checked', false);
    $('#chb_ServCap_Domingo').prop('checked', false);
    $('#chb_ServCap_CualquierDia').prop('checked', false);

    $('#tb_ServCap_HorariosRecep1').val('');
    $('#tb_ServCap_HorariosRecep2').val('');
    $('#tb_ServCap_HorariosRecep3').val('');
    $('#tb_ServCap_HorariosRecep4').val('');

    $('#chb_ServCap_MismoDia').prop('checked', false);
    $('#chb_ServCap_Previa').prop('checked', false);

    //
    // 
    // SERVICIO AUDITORIA 
    // 
    //

    $('#chbReqServAuditoria').prop('checked', false);
    $('#trReqServAuditoria').css('display', 'none');

    $("#sel_ServAud_Tipo1").val(0);
    $("#sel_ServAud_Tipo2").val(0);
    $('#sel_ServAud_Tipo2').empty();
    $('#sel_ServAud_Tipo2').append('<option value="0">-</option>')

    // L M M J V S D CD
    $('#chb_ServAud_Lunes').prop('checked', false);
    $('#chb_ServAud_Martes').prop('checked', false);
    $('#chb_ServAud_Miercoles').prop('checked', false);
    $('#chb_ServAud_Jueves').prop('checked', false);
    $('#chb_ServAud_Viernes').prop('checked', false);
    $('#chb_ServAud_Sabado').prop('checked', false);
    $('#chb_ServAud_Domingo').prop('checked', false);
    $('#chb_ServAud_CualquierDia').prop('checked', false);

    $('#tb_ServAud_HorariosRecep1').val('');
    $('#tb_ServAud_HorariosRecep2').val('');
    $('#tb_ServAud_HorariosRecep3').val('');
    $('#tb_ServAud_HorariosRecep4').val('');

    $('#chb_ServAud_MismoDia').prop('checked', false);
    $('#chb_ServAud_Previa').prop('checked', false);

    //
    //- Servicio Asesoria 
    //- SERVICIO ASESORIA 
    //- SERVICIO ASESORIA 
    //- SERVICIO ASESORIA 
    //
    $('#chb_ServAsesoria_Aplicar').prop('checked', false);
    $('#trReqServAsesoria').css('display', 'none');

    $('#sel_ServAsesoria_Tipo1').val(0);

    $('#sel_ServAsesoria_Tipo2').val(0);
    $('#sel_ServAsesoria_Tipo2').empty();
    $('#sel_ServAsesoria_Tipo2').append('<option value="0">-</option>')

    // L M M J V S D CD
    $('#chb_ServAsesoria_Lunes').prop('checked', false);
    $('#chb_ServAsesoria_Martes').prop('checked', false);
    $('#chb_ServAsesoria_Miercoles').prop('checked', false);
    $('#chb_ServAsesoria_Jueves').prop('checked', false);
    $('#chb_ServAsesoria_Viernes').prop('checked', false);
    $('#chb_ServAsesoria_Sabado').prop('checked', false);
    $('#chb_ServAsesoria_Domingo').prop('checked', false);

    $('#chb_ServAsesoria_CualquierDia').prop('checked', false);

    $('#tb_ServAsesoria_HorariosRecep1').val('');
    $('#tb_ServAsesoria_HorariosRecep2').val('');
    $('#tb_ServAsesoria_HorariosRecep3').val('');
    $('#tb_ServAsesoria_HorariosRecep4').val('');

    $('#chb_ServAsesoria_MismoDia').prop('checked', false);
    $('#chb_ServAsesoria_Previa').prop('checked', false);

    //
    // Documentos entrega resepccion 
    //
    // L M M J V S D CD
    $('#chkRecRevLunes').prop('checked', false);
    $('#chkRecRevMartes').prop('checked', false);
    $('#chkRecRevMiercoles').prop('checked', false);
    $('#chkRecRevJueves').prop('checked', false);
    $('#chkRecRevViernes').prop('checked', false);
    $('#chkRecRevSabado').prop('checked', false);
    $('#chkRecRevDomingo').prop('checked', false);
    $('#chkRecRevCualquierDia').prop('checked', false);

    $('#tbAcs_TimePicker1').val('');
    $('#tbAcs_TimePicker2').val('');
    $('#tbAcs_TimePicker3').val('');
    $('#tbAcs_TimePicker4').val('');
    $('#tbAcs_RecPersonaRecibe').val('');

    $('#chbAcs_RecCitaSinCita').prop('checked', false);
    $('#chbAcs_RecCitaMismoDia').prop('checked', false);
    $('#chbAcs_RecCitaPrevia').prop('checked', false);

    $('#chbAcs_RecCitaPrevia').prop('checked', false);

    //$('#tbAcs_RecCitaContacto').val(RES.Acs_RecCitaContacto);
    //$('#tbAcs_RecCitaTelefono').val(RES.Acs_RecCitaTelefono);
    //$('#tbAcs_RecCitaContacto').val(RES.Acs_RecCitaContacto);

    // Area de Recibo

    $('#chbAcs_RecAreaPropia').prop('checked', false);
    $('#chbAcs_RecAreaPlaza').prop('checked', false);
    $('#chbAcs_RecAreaCalle').prop('checked', false);
    $('#chbAcs_RecEstCortesia').prop('checked', false);
    $('#chbAcs_RecEstCosto').prop('checked', false);
    $('#tbAcs_RecEstMonto').val('');

    $('#tbAcs_ComentariosRecomendaciones').val('');

    /*
    $('#tbId_Cte').val('');
    $('#tbCte_Nombre').text('');
    // Especificaciones adicionales
    $('#tbCDI').val('');    
    $('#lbEspAd_CuentaCorporativa').text('No');
    
    //Modalidad de Pedido
    $('#chbAcs_Modalidad').prop('checked', false);
    $('#chbFrecEstablecida').prop('checked', false);
    $('#chbOrdenAbierta').prop('checked', false);
    $('#chbAceptaParcialidades').prop('checked', false);
    
    //Forma de envío de pedido    
    $('#chbRecCorreo').prop('checked', false);
    $('#chbRecTelefono').prop('checked', false);
    $('#chbRecWhatsApp').prop('checked', false);
    // Recolectado por Rik
    $('#chbRecRIK').prop('checked', false);
    $('#chbRecConfirmacion').prop('checked', false);
    $('#chbRecOtro').prop('checked', false);
    $('#chbRecOtroDesc').prop('checked', false);
    $('#tbAcs_PedidoEncargadoEnviar').prop('checked', false);
    $('#tbAcs_PedidoPuesto').prop('checked', false);
    $('#tbAcs_PedidoTelefono').prop('checked', false);
    $('#tbAcs_PedidoEmail').prop('checked', false);
    // DETALLE DE SERVICIO 
    $('#chbReqServTec').val('');
    $('trReqServTec').css('display', 'none');
    $('#chbCapacitacion').val('');
    $('trCapacitacion').css('display', 'none');
    $('#chbAuditoria').val('');
    $('trAuditoria').css('display', 'none');
    $('#chbReqServAsesoria').val('');
    $('#trReqServAsesoria').css('display', 'none');
    */
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Inicializar_Orden() {

    $('#tbCliente_Numero').val('');
    $('#tbCliente_Nombre').val('');

    $('#tbCDI').text('');
    $('#tbCuentaCorp').val('');
    $('#tbNombreComercial').text('');

    /*
    $('#tbAcys_Folio').val('');
    $('#tbAcys_FechaInicio').val('');
    $('#tbAcys_FechaFin').val('');
    $('#ddlAcys_Estatus').val('');
    $('#tbAcys_CteNombre').val('');
    $('#tbAcys_CteNumero').val('');
    $('#tbAcys_CteDireccion').val('');
    $('#tbAcys_CteCol').val('');
    $('#tbAcys_CteMunicipio').val('');
    $('#tbAcys_CteCP').val('');
    $('#tbAcys_CteRFC').val('');
    $('#tbAcys_CteTerritorio').val('');
    $('#tbAcys_RikMombre').val('');
    $('#tbAcys_RikNumero').val('');
    $('#tblAcuerdoProds tbody').empty();
    LISTAPRODUCTOS_CONTADOR = 0;
    ListadoProductos_AddRow();
    */
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
$(document).ready(function () {

    OrdenCliente_CargarCombos();

    /*
    $('#tbHorarioEntrega_Ini1 .time').timepicker({
    'showDuration': true,
    'timeFormat': 'g:ia'
    });
    $('#tbHorarioEntrega_Fin1 .time').timepicker({
    'showDuration': true,
    'timeFormat': 'g:ia'
    });
    */


    // Revision de Facturas

    $('#tpRevisionMañanaInicio').timepicker();
    $('#tpRevisionMañanaFin').timepicker();
    $('#tpRevisionTardeInicio').timepicker();
    $('#tpRevisionTardeFin').timepicker();

    // Pago de Facturas

    $('#tpPagoMañanaInicio').timepicker();
    $('#tpPagoMañanaFin').timepicker();
    $('#tpPagoTardeInicio').timepicker();
    $('#tpPagoTardeFin').timepicker();

    $('#tbAcs_TimePicker1').timepicker();
    $('#tbAcs_TimePicker2').timepicker();
    $('#tbAcs_TimePicker3').timepicker();
    $('#tbAcs_TimePicker4').timepicker();

    $('#tbHorarioEntrega_Ini1').timepicker();
    $('#tbHorarioEntrega_Fin1').timepicker();

    $('#tbHorarioEntrega_Ini2').timepicker();
    $('#tbHorarioEntrega_Fin2').timepicker();

    $('#tb_ST_HorariosRecep1').timepicker();
    $('#tb_ST_HorariosRecep2').timepicker();
    $('#tb_ST_HorariosRecep3').timepicker();
    $('#tb_ST_HorariosRecep4').timepicker();

    // Servicio Técnico        
    $('#tb_ST_HorariosRecep1').timepicker();
    $('#tb_ST_HorariosRecep2').timepicker();
    $('#tb_ST_HorariosRecep3').timepicker();
    $('#tb_ST_HorariosRecep4').timepicker();

    //Capacitacion     
    $('#tb_ServCap_HorariosRecep1').timepicker();
    $('#tb_ServCap_HorariosRecep2').timepicker();
    $('#tb_ServCap_HorariosRecep3').timepicker();
    $('#tb_ServCap_HorariosRecep4').timepicker();

    //Asesoria    
    $('#tb_ServAsesoria_HorariosRecep1').timepicker();
    $('#tb_ServAsesoria_HorariosRecep2').timepicker();
    $('#tb_ServAsesoria_HorariosRecep3').timepicker();
    $('#tb_ServAsesoria_HorariosRecep4').timepicker();
    //Auditoria
    $('#tb_ServAud_HorariosRecep1').timepicker();
    $('#tb_ServAud_HorariosRecep2').timepicker();
    $('#tb_ServAud_HorariosRecep3').timepicker();
    $('#tb_ServAud_HorariosRecep4').timepicker();

    // 
    $('#chbDocEntrRecep_EspAdic').change(function () {
        var chbEspecificacionAdic = $('#chbDocEntrRecep_EspAdic').is(":checked");
        if (chbEspecificacionAdic) {
            $('#divRecepcionPed_EspAdic').css('display', 'block');
        } else {
            $('#divRecepcionPed_EspAdic').css('display', 'none');
        }
    });


    // Recepcion de pedidos
    // Documentos Entrega Recepcion
    // Especificaciones Adicionales
    /*
    $('#chbAcs_EspecsAdic1').change(function () {
    var chbEspecificacionAdic = $('#chbAcs_EspecsAdic1').is(":checked");
    if (chbEspecificacionAdic) {
    $('#divAcs_EspecsAdic1').css('display', 'block');
    } else {
    $('#divAcs_EspecsAdic1').css('display', 'none');
    }
    });
    */

    // Documentos para entrega y recepción
    // Especificaciones Adicionales
    $('#btnAcs_EspecsAdic1').click(function () {
        var chb = $('#chbAcs_EspecsAdic1').is(":checked");
        if (chb) {
            // Desplegado checkado remover Plus Add Minus
            $('#faAcs_EspecsAdic1').removeClass('fa fa-minus-circle fa-lg');
            $('#faAcs_EspecsAdic1').addClass('fa fa-plus-circle fa-lg');
            $('#chbAcs_EspecsAdic1').prop('checked', false);
            //                
            $('#divAcs_EspecsAdic1').css('display', 'none');
        } else {
            // Ocultar nocheckado remover Minus ad Plus 
            $('#faAcs_EspecsAdic1').removeClass('fa fa-plus-circle fa-lg');
            $('#faAcs_EspecsAdic1').addClass('fa fa-minus-circle fa-lg');
            $('#chbAcs_EspecsAdic1').prop('checked', true);
            //
            $('#divAcs_EspecsAdic1').css('display', 'block');
        }
    });

    // Documentos para entrega y recepción
    // Especificaciones Adicionales
    $('#btnDER_EspecsAdic2').click(function () {
        var chb = $('#chbDER_EspAdic2').is(":checked");
        if (chb) {
            // Desplegado checkado remover Plus Add Minus
            $('#faDER_EspecsAdic2').removeClass('fa fa-minus-circle fa-lg');
            $('#faDER_EspecsAdic2').addClass('fa fa-plus-circle fa-lg');
            $('#chbDER_EspAdic2').prop('checked', false);
            //                
            $('#divDER_EspecsAdic2').css('display', 'none');
        } else {
            // Ocultar nocheckado remover Minus ad Plus 
            $('#faDER_EspecsAdic2').removeClass('fa fa-plus-circle fa-lg');
            $('#faDER_EspecsAdic2').addClass('fa fa-minus-circle fa-lg');
            $('#chbDER_EspAdic2').prop('checked', true);
            //
            $('#divDER_EspecsAdic2').css('display', 'block');
        }
    });


    //
    $('#chbCondPag_EspAdic').change(function () {
        var chbEspecificacionAdic = $('#chbCondPag_EspAdic').is(":checked");
        if (chbEspecificacionAdic) {
            $('#divCondPago_EspAdic').css('display', 'block');
        } else {
            $('#divCondPago_EspAdic').css('display', 'none');
        }
    });

    // Chb Servicio Tecnico 
    $('#chb_ST_Aplicar').change(function () {
        var chbServicioTec = $('#chb_ST_Aplicar').is(":checked");
        if (chbServicioTec) {
            $('#trReqServTec').css('display', 'block');
            $('#trReqServTec_hr').css('display', 'none');
        } else {
            $('#trReqServTec').css('display', 'none');
            $('#trReqServTec_hr').css('display', 'block');
        }
    });

    //  Servicios Capacitacion 
    $('#chb_ServCap_Aplicar').change(function () {
        var chbServicioTec = $('#chb_ServCap_Aplicar').is(":checked");
        if (chbServicioTec) {
            $('#trReqServCapacitacion').css('display', 'block');
            $('#tr_ServCap_Hr').css('display', 'none');
        } else {
            $('#trReqServCapacitacion').css('display', 'none');
            $('#tr_ServCap_Hr').css('display', 'block');
        }
    });

    $('#chb_ServAud_Aplicar').change(function () {
        var chbServicioTec = $('#chb_ServAud_Aplicar').is(":checked");
        if (chbServicioTec) {
            $('#trReqServAuditoria').css('display', 'block');
            $('#hr_ServAud_hr').css('display', 'none');
        } else {
            $('#trReqServAuditoria').css('display', 'none');
            $('#hr_ServAud_hr').css('display', 'block');
        }
    });

    //
    $('#chb_ServAsesoria_Aplicar').change(function () {
        var chbServicioTec = $('#chb_ServAsesoria_Aplicar').is(":checked");
        if (chbServicioTec) {
            $('#trReqServAsesoria').css('display', 'block');
        } else {
            $('#trReqServAsesoria').css('display', 'none');
        }
    });

    // Expandir El Modal
    $('#btnOrden_Expandir').click(function () {
        $('#modalOrdenCliente').find('.modal-dialog').css({
            width: 'auto', //probably not needed
            height: 'auto', //probably not needed 
            'max-height': '100%'
        });

        $('#btnOrden_Expandir').css('display', 'none');
        $('#btnOrden_Reducir').css('display', 'block');
    });

    // Reduce el Modal
    $('#btnOrden_Reducir').click(function () {
        $('#modalOrdenCliente').find('.modal-dialog').css({
            width: '800px', //probably not needed
            height: 'auto', //probably not needed 
            'max-height': '100%'
        });

        $('#btnOrden_Expandir').css('display', 'block');
        $('#btnOrden_Reducir').css('display', 'none');
    });

    // Boton Cancelar 
    $('#btnOrdenCancelar').click(function () {
        $('#modalOrdenCliente').modal('hide');
        OrdenCliente_Inicializar();
    });

    //
    // GUARDAR
    //
    $('#btnOrdenGuardar').click(function () {
        frmOrden_Guardar();
    });

    $(document).on('change', 'select[id="sel_ServCap_Tipo1"]', function () {
        OpcionSel = $(this).find("option:selected").val();
        Cargar_ComboTipo_TipoServicio(OpcionSel, $('#sel_ServCap_Tipo2'), function () {
            $('#sel_ServCap_Tipo2').val(0);
            console.log('sel_ServCap_Tipo1:0');
        });
    });

    $(document).on('change', 'select[id="sel_ServAud_Tipo1"]', function () {
        OpcionSel = $(this).find("option:selected").val();
        Cargar_ComboTipo_TipoServicio(OpcionSel, $('#sel_ServAud_Tipo2'));
    });

    $(document).on('change', 'select[id="sel_ServAsesoria_Tipo1"]', function () {
        OpcionSel = $(this).find("option:selected").val();
        Cargar_ComboTipo_TipoServicio(OpcionSel, $('#sel_ServAsesoria_Tipo2'));
    });

    $(document).on('change', 'select[id="selAcs_ParcialidadesSi"]', function () {
        OpcionSel = $(this).find("option:selected").val();
        //alert(OpcionSel);

        if (OpcionSel == 1) {
            $('#tblAceptaParcialidades').css('display', 'block');
        } else {
            $('#tblAceptaParcialidades').css('display', 'none');
        }
    });

});