/*

    Key Quimica Dic 2018 

    24 Dic 2018 RFH 

*/

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
$(document).ready(function () {

    //OrdenCliente_CargarCombos();

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

    /*

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

    /*

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
    */
    
    //

});



