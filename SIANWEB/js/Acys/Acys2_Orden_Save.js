/*
Key Quimica Dic 2018 
09Dic-2020 RFH Corrige validacion de dias de pago.
24 Dic 2018 RFH 
9 Abr 2019 Actualizado RFH 
X
*/

// 
// GUARDAR ORDEN 
// 

function frmOrden_Guardar() {
    //console.log('frmOrden_Guardar');

    var Spinner = $('#spinner_AcysOrden');
    Spinner.css('display', 'block');

    // SE HACE COPAI DE ENITDA PRINCIPAL

    A = {};
    A = ACyS_Entidad;

    //xls
    var Id_Emp = $('#hfId_Emp').val();
    var Id_Cd = $('#hfId_Cd').val();
    var Id_Acs = $('#hfId_Acs').val();
    var Acs_version = $('#hfId_AcsVersion').val();
    var Acys_FechaInicio = $('#tbAcys_FechaInicio').val();
    var Acys_FechaFin = $('#tbAcys_FechaFin').val();
    //var dlAcys_Estatus = $('#ddlAcys_Estatus').val();

    // MODALIDAD PEDIDO 

    var Acs_Modalidad = $('#chbAcs_Modalidad').is(":checked");
    var Acs_OrdenAbiertaConRep = $('#chbOrdenAbiertaConRep').is(":checked");

    if ((Acs_Modalidad == false && Acs_OrdenAbiertaConRep == false) || (Acs_Modalidad == true && Acs_OrdenAbiertaConRep == true)) {
        alertify.alert('No ha registrado la modalida de pedido (Frecuencia Establecida o Orden Abierta con Reposición), por favor seleccione solo una.');
        Spinner.css('display', 'none');
        return;
    }

    // ACEPTA PARCIALIDADES

    var ParcialidadesSi = $('#selAcs_ParcialidadesSi').val();
    if (ParcialidadesSi == 1) {
        /* 5AGO2021 RFH */
        var Parcialidad_Entrega_Rem = CheckBox_ToInt($('#cbParcialidad_Entrega_Rem'));
        var Parcialidad_Entrega_Fac = CheckBox_ToInt($('#cbParcialidad_Entrega_Fac'));
        if (Parcialidad_Entrega_Rem == 0 && Parcialidad_Entrega_Fac == 0) {
            alertify.alert('Debe establecer el tipo de "Parcialidades entrega" en sección "Recepcion de pedidos" -> "Modalidad de Pedido" .');
            Spinner.css('display', 'none');
            return;
        }
    }

    // DIAS DE RECEPCION 

    var Acs_RecRevLunes = CheckBox_ToInt($('#chbAcs_RecRevLunes'));
    var Acs_RecRevMartes = CheckBox_ToInt($('#chbAcs_RecRevMartes'));
    var Acs_RecRevMiercoles = CheckBox_ToInt($('#chbAcs_RecRevMiercoles'));
    var Acs_RecRevJueves = CheckBox_ToInt($('#chbAcs_RecRevJueves'));
    var Acs_RecRevViernes = CheckBox_ToInt($('#chbAcs_RecRevViernes'));
    var Acs_RecRevSabado = CheckBox_ToInt($('#chbAcs_RecRevSabado'));
    var Acs_RecRevDomingo = CheckBox_ToInt($('#chbAcs_RecRevDomingo'));
    var Acs_RecRevCualquierDia = CheckBox_ToInt($('#chbAcs_RecRevCualquierDia'));

    if (Acs_RecRevLunes == 0 && Acs_RecRevMartes == 0 && Acs_RecRevMiercoles && Acs_RecRevJueves == 0 && Acs_RecRevViernes == 0 && Acs_RecRevSabado == 0 && Acs_RecRevDomingo == 0 && Acs_RecRevCualquierDia == 0) {
        alertify.alert('No ha registrado el "Día de Recepción" en la sección "Documento para entrega y recepción" (Debe seleccionar al menos uno).');
        Spinner.css('display', 'none');
        return;
    }

    // HORARIOS DE ENTREGA 

    var Acs_TimePicker1 = $('#tbAcs_TimePicker1').val();
    lenAcs_TimePicker1 = Acs_TimePicker1.length;
    var Acs_TimePicker2 = $('#tbAcs_TimePicker2').val();
    lenAcs_TimePicker2 = Acs_TimePicker2.length;
    var Acs_TimePicker3 = $('#tbAcs_TimePicker3').val();
    lenAcs_TimePicker3 = Acs_TimePicker3.length;
    var Acs_TimePicker4 = $('#tbAcs_TimePicker4').val();
    lenAcs_TimePicker4 = Acs_TimePicker4.length;

    var HE_Completos = false;

    if (lenAcs_TimePicker1 > 0 && lenAcs_TimePicker2 > 0) {
        HE_Completos = true;
    }

    if (lenAcs_TimePicker3 > 0 && lenAcs_TimePicker4 > 0) {
        HE_Completos = true;
    }

    //if (lenAcs_TimePicker1 ==0 || lenAcs_TimePicker2 ==0 || lenAcs_TimePicker3 == 0 || lenAcs_TimePicker4==0)
    if (HE_Completos == false) {
        alertify.alert('No ha establecido los "Documento para entrega y recepción" -><br> "Horarios de entrega".');
        Spinner.css('display', 'none');
        return;
    }

    // PERSONA QUE RECIBE 

    var Acs_RecPersonaRecibe = $('#tbAcs_RecPersonaRecibe').val();
    lenAcs_RecPersonaRecibe = Acs_RecPersonaRecibe.length;

    if (lenAcs_RecPersonaRecibe <= 0) {
        alertify.alert('No ha registrado la información de "Persona que recibe" en sección "Documento para entrega y recepción".');
        Spinner.css('display', 'none');
        return;
    }

    // CITA PARA ENTREGA

    var chbAcs_RecCitaSinCita = CheckBox_ToInt($('#chbAcs_RecCitaSinCita'));
    var chbAcs_RecCitaMismoDia = CheckBox_ToInt($('#chbAcs_RecCitaMismoDia'));
    var chbAcs_RecCitaPrevia = CheckBox_ToInt($('#chbAcs_RecCitaPrevia'));
    if (chbAcs_RecCitaSinCita == 0 && chbAcs_RecCitaMismoDia == 0 && chbAcs_RecCitaPrevia == 0) {
        alertify.alert('No ha registrado la opción "Cita pra entrega".');
        Spinner.css('display', 'none');
        return;
    }

    // AREA DE RECIBO    
    var chbAcs_RecAreaPropia = CheckBox_ToInt($('#chbAcs_RecAreaPropia'));
    var chbAcs_RecAreaPlaza = CheckBox_ToInt($('#chbAcs_RecAreaPlaza'));
    var chbAcs_RecAreaCalle = CheckBox_ToInt($('#chbAcs_RecAreaCalle'));
    if (chbAcs_RecAreaPropia == 0 && chbAcs_RecAreaPlaza == 0 && chbAcs_RecAreaCalle == 0) {
        alertify.alert('No ha registrado la opción "Área de recibo".');
        Spinner.css('display', 'none');
        return;
    }
    A.Acs_RecAreaPropia = chbAcs_RecAreaPropia;
    A.Acs_RecAreaPlaza = chbAcs_RecAreaPlaza;
    A.Acs_RecAreaCalle = chbAcs_RecAreaCalle;


    // ESPECIFICACIONES ADICIONALES

    var chkAcs_RecDocFactKeyEnt = CheckBox_ToInt($('#chkAcs_RecDocFactKeyEnt'));
    var chkAcs_RecDocOrdCompraEnt = CheckBox_ToInt($('#chkAcs_RecDocOrdCompraEnt'));
    var chkAcs_RecDocOrdReposEnt = CheckBox_ToInt($('#chkAcs_RecDocOrdReposEnt'));
    var chkAcs_RecDocCopPedidoEnt = CheckBox_ToInt($('#chkAcs_RecDocCopPedidoEnt'));
    var chkAcs_RecDocRemisionEnt = CheckBox_ToInt($('#chkAcs_RecDocRemisionEnt'));
    var chkAcs_RecDocCertificadoEnt = CheckBox_ToInt($('#chkAcs_RecDocCertificadoEnt'));

    var chkAcs_RecDocFactKeyRec = CheckBox_ToInt($('#chkAcs_RecDocFactKeyRec'));
    var chkAcs_RecDocOrdCompraRec = CheckBox_ToInt($('#chkAcs_RecDocOrdCompraRec'));
    var chkAcs_RecDocOrdReposRec = CheckBox_ToInt($('#chkAcs_RecDocOrdReposRec'));
    var chkAcs_RecDocCopPedidoRec = CheckBox_ToInt($('#chkAcs_RecDocCopPedidoRec'));
    var chkAcs_RecDocRemisionRec = CheckBox_ToInt($('#chkAcs_RecDocRemisionRec'));
    var chkAcs_RecDocCertificadoRec = CheckBox_ToInt($('#chkAcs_RecDocCertificadoRec'));

    // VALIDACION 

    if (chkAcs_RecDocFactKeyEnt == 0 && chkAcs_RecDocOrdCompraEnt == 0 && chkAcs_RecDocOrdReposEnt == 0 &&
        chkAcs_RecDocCopPedidoEnt == 0 && chkAcs_RecDocRemisionEnt == 0 && chkAcs_RecDocCertificadoEnt == 0) {
        alertify.alert('Debe establecer al menos una opción "En Recepción de pedidos" -> "Especificaciónes adicionales".');
        Spinner.css('display', 'none');
        return;
    }

    var Acs_ParcialidadesSi = $('#selAcs_ParcialidadesSi').val();


    if (Acs_Modalidad == true) {
        A.Acs_Modalidad = 'A';
    } else {
        A.Acs_Modalidad = 'B';
    }

    if (Acs_OrdenAbiertaConRep == true) {
        A.Acs_OrdenAbiertaConRep = 1;
    } else {
        A.Acs_OrdenAbiertaConRep = 2;
    }

    // ¿ ACEPTA PARCIALIADES ?

    if (ParcialidadesSi == 1) {
        A.Acs_ParcialidadesSi = 1;
        A.Acs_ParcialidadesNo = 0;
    } else {
        A.Acs_ParcialidadesSi = 0;
        A.Acs_ParcialidadesNo = 1;
    }

    // TIPO PARCIALIDAD

    if (Parcialidad_Entrega_Rem == 1) {
        A.Acs_ParcialidadTipo = 1
    }
    if (Parcialidad_Entrega_Fac == 1) {
        A.Acs_ParcialidadTipo = 2
    }


    //

    A.Id_Emp = Id_Emp;
    A.Id_Cd = Id_Cd;
    A.Id_Acs = Id_Acs;
    A.Acs_Version = Acs_version;

    // FORMA DE ENVIO DE PEDIDO

    A.Acs_RecCorreo = CheckBox_ToInt($('#chbRecCorreo'));

    A.Acs_RecTelefono = CheckBox_ToInt($('#chbRecTelefono'));

    A.Acs_RecWhatsApp = CheckBox_ToInt($('#chb_RecWhatsApp'));

    A.Acs_RecRIK = CheckBox_ToInt($('#chbRecRIK'));

    A.Acs_ReqConfirmacion = CheckBox_ToInt($('#chbRecConfirmacion'));

    A.Acs_RecOtro = CheckBox_ToInt($('#chbRecOtro'));

    A.Acs_RecOtroDesc = $('#tbAcs_RecOtroDesc').val();
    A.Acs_PedidoEncargadoEnviar = $('#tbAcs_PedidoEncargadoEnviar').val();
    A.Acs_PedidoPuesto = $('#tbAcs_PedidoPuesto').val();
    A.Acs_PedidoTelefono = $('#tbAcs_PedidoTelefono').val();
    A.Acs_PedidoTelefono2 = $('#tbAcs_PedidoTelefono2').val();
    A.Acs_PedidoEmail = $('#tbAcs_PedidoEmail').val();
    A.Acs_PedidoEmail2 = $('#tbAcs_PedidoEmail2').val();

    //
    // DOCUMENTOS PARA ENTREGA Y RECEPCION
    //
    // L M M J V S D CD

    A.Acs_RecRevLunes = Acs_RecRevLunes;
    A.Acs_RecRevMartes = Acs_RecRevMartes;
    A.Acs_RecRevMiercoles = Acs_RecRevMiercoles;
    A.Acs_RecRevJueves = Acs_RecRevJueves;
    A.Acs_RecRevViernes = Acs_RecRevViernes;
    A.Acs_RecRevSabado = Acs_RecRevSabado;
    A.Acs_RecRevDomingo = Acs_RecRevDomingo;
    A.Acs_RecRevCualquierDia = Acs_RecRevCualquierDia;

    A.Acs_TimePicker1 = Acs_TimePicker1;
    A.Acs_TimePicker2 = Acs_TimePicker2;
    A.Acs_TimePicker3 = Acs_TimePicker3;
    A.Acs_TimePicker4 = Acs_TimePicker4;

    A.Acs_RecPersonaRecibe = $('#tbAcs_RecPersonaRecibe').val();

    //Cita para entregar        
    A.Acs_RecCitaSinCita = CheckBox_ToInt(($('#chbAcs_RecCitaSinCita')));
    A.Acs_RecCitaMismoDia = CheckBox_ToInt(($('#chbAcs_RecCitaMismoDia')));
    A.Acs_RecCitaPrevia = CheckBox_ToInt(($('#chbAcs_RecCitaPrevia')));

    // Area Recibo         

    // ESTACIONAMIENTO 
    var Acs_RecEstCortesia = CheckBox_ToInt($('#chkAcs_RecEstCortesia'));
    A.Acs_RecEstCortesia = Acs_RecEstCortesia;

    var Acs_RecEstCosto = CheckBox_ToInt($('#chkAcs_RecEstCosto'));
    A.Acs_RecEstCosto = Acs_RecEstCosto;

    // MONTO                                  
    var Acs_RecEstMonto = $('#tbAcs_RecEstMonto').val();
    Acs_RecEstMonto = parseFloat(Acs_RecEstMonto);
    if (isNaN(Acs_RecEstMonto)) {
        Acs_RecEstMonto = 0;
    }
    A.Acs_RecEstMonto = Acs_RecEstMonto;

    // ESPECIFICACIONES ADICIONALES
    // Facturas Key    
    A.Acs_RecDocFactKeyEnt = CheckBox_ToInt($('#chkAcs_RecDocFactKeyEnt'));
    A.Acs_RecDocFactKeyEntCop = $('#tbAcs_RecDocFactKeyEntCop').val();
    A.Acs_RecDocFactKeyRec = CheckBox_ToInt($('#chkAcs_RecDocFactKeyRec'));
    A.Acs_RecDocFactKeyRecCop = $('#tbAcs_RecDocFactKeyRecCop').val();
    // Orden de compra/release

    // FACTURAS KEY
    A.Acs_RecDocOrdCompraEnt = CheckBox_ToInt($('#chkAcs_RecDocOrdCompraEnt'));
    A.Acs_RecDocOrdCompraEntCop = $('#tbAcs_RecDocOrdCompraEntCop').val();
    A.Acs_RecDocOrdCompraRec = CheckBox_ToInt($('#chkAcs_RecDocOrdCompraRec'));
    A.Acs_RecDocOrdCompraRecCop = $('#tbAcs_RecDocOrdCompraRecCop').val();

    // ORDEN REPOSICION
    A.Acs_RecDocOrdReposEnt = CheckBox_ToInt($('#chkAcs_RecDocOrdReposEnt'));
    A.Acs_RecDocOrdReposEntCop = $('#tbAcs_RecDocOrdReposEntCop').val();
    A.Acs_RecDocOrdReposRec = CheckBox_ToInt($('#chkAcs_RecDocOrdReposRec'));
    A.Acs_RecDocOrdReposRecCop = $('#tbAcs_RecDocOrdReposRecCop').val();

    // COPIA PEDIDO
    A.Acs_RecDocCopPedidoEnt = CheckBox_ToInt(($('#chkAcs_RecDocCopPedidoEnt')));
    A.Acs_RecDocCopPedidoEntCop = $('#tbAcs_RecDocCopPedidoEntCop').val();
    A.Acs_RecDocCopPedidoRec = CheckBox_ToInt(($('#chkAcs_RecDocCopPedidoRec')));
    A.Acs_RecDocCopPedidoRecCop = $('#tbAcs_RecDocCopPedidoRecCop').val();

    // REMISION
    A.Acs_RecDocRemisionEnt = CheckBox_ToInt(($('#chkAcs_RecDocRemisionEnt')));
    A.Acs_RecDocRemisionEntCop = $('#tbAcs_RecDocRemisionEntCop').val();
    A.Acs_RecDocRemisionRec = CheckBox_ToInt(($('#chkAcs_RecDocRemisionRec')));
    A.Acs_RecDocRemisionRecCop = $('#tbAcs_RecDocRemisionRecCop').val();

    // CERTIFICADO DE CALIDAD 1
    A.Acs_RecDocCertificadoEnt = CheckBox_ToInt(($('#chkAcs_RecDocCertificadoEnt')));
    A.Acs_RecDocCertificadoEntCop = $('#tbAcs_RecDocCertificadoEntCop').val();
    A.Acs_RecDocCertificadoRec = CheckBox_ToInt(($('#chkAcs_RecDocCertificadoRec')));
    A.Acs_RecDocCertificadoRecCop = $('#tbAcs_RecDocCertificadoRecCop').val();



    //
    // CONDICIONES DE PAGO 
    //
    // -> Documentos para entrega recepcion 

    // PAGO DE FACTURAS 

    //OCT16-2019
    A.Acs_CorreoRecibirFacturas = $('#tbAcs_CorreoRecibirFacturas').val();
    A.Acs_CorreoRecibirComplemento = $('#tbAcs_CorreoRecibirComplemento').val();
    A.Acs_CorreoRecibir_NA = CheckBox_ToInt(($('#chbAcs_CorreoRecibir_NA')));

    // ELECTRONCA 

    A.RevFacEmail = CheckBox_ToInt(($('#chkRevFacEmail')));
    A.RevFacEmailTexto = $('#txtRevFacEmailTexto').val();
    A.RevFacEmailTexto2 = $('#txtRevFacEmailTexto2').val();

    A.RevFacPortal = CheckBox_ToInt(($('#chkRevFacPortal')));
    A.RevFacPortalTexto = $('#txtRevFacPortalTexto').val();
    A.RevFacHttp = $('#txtRevFacHttp').val();
    A.RevFacUsuario = $('#txtRevFacUsuario').val();
    A.RevFacContrasenia = $('#txtRevFacContrasenia').val();

    if (A.RevFacPortal == 1 && (A.RevFacPortalTexto.length <= 0 || A.RevFacHttp.length <= 0 || A.RevFacUsuario.length <= 0 || A.RevFacContrasenia.length <= 0)) {
        alertify.alert('Los campos en la sección de "Pago de Fcturas" -> "Eléctronica" son obligatorio.');
        Spinner.css('display', 'none');
        return;
    }

    //
    A.Acs_DocEntregaFormaPago = $('#cbAcs_DocEntregaFormaPago').val();

    // Metodo Forma de Pago

    // ESPECIFIACIONES ADICIONALES

    // Factura Key     
    A.ACS_chk62DocFactKeyEnt = CheckBox_ToInt($('#chbACS_chk62DocFactKeyEnt'));
    A.ACS_txt62DocFactKeyEntCop = $('#tbACS_txt62DocFactKeyEntCop').val();
    A.ACS_chk62DocFactKeyRec = CheckBox_ToInt($('#chbACS_chk62DocFactKeyRec'));
    A.ACS_txt62DocFactKeyRecCop = $('#tbACS_txt62DocFactKeyRecCop').val();

    // Orden Compra 
    A.ACS_chk62DocOrdCompraEnt = CheckBox_ToInt($('#chbACS_chk62DocOrdCompraEnt'));
    A.ACS_txt62DocOrdCompraEntCop = $('#tbACS_txt62DocOrdCompraEntCop').val();
    A.ACS_chk62DocOrdCompraRec = CheckBox_ToInt($('#chbACS_chk62DocOrdCompraRec'));
    A.ACS_txt62DocOrdCompraRecCop = $('#tbACS_txt62DocOrdCompraRecCop').val();

    // Orden Reposicion
    A.ACS_chk62DocOrdReposEnt = CheckBox_ToInt($('#chbACS_chk62DocOrdReposEnt'));
    A.ACS_txt62DocOrdReposEntCop = $('#tbACS_txt62DocOrdReposEntCop').val();
    A.ACS_chk62DocOrdReposRec = CheckBox_ToInt($('#chbACS_chk62DocOrdReposRec'));
    A.ACS_txt62DocOrdReposRecCop = $('#tbACS_txt62DocOrdReposRecCop').val();

    //Copia de Pedido
    A.ACS_chk62DocCopPedidoEnt = CheckBox_ToInt($('#chbACS_chk62DocCopPedidoEnt'));
    A.ACS_txt62DocCopPedidoEntCop = $('#tbACS_txt62DocCopPedidoEntCop').val();
    A.ACS_chk62DocCopPedidoRec = CheckBox_ToInt($('#chbACS_chk62DocCopPedidoRec'));
    A.ACS_txt62DocCopPedidoRecCop = $('#tbACS_txt62DocCopPedidoRecCop').val();

    // Remision
    A.ACS_chk62DocRemisionEnt = CheckBox_ToInt($('#chbACS_chk62DocRemisionEnt'));
    A.ACS_txt62DocRemisionEntCop = $('#tbACS_txt62DocRemisionEntCop').val();
    A.ACS_chk62DocRemisionRec = CheckBox_ToInt($('#chbACS_chk62DocRemisionRec'));
    A.ACS_txt62DocRemisionRecCop = $('#tbACS_txt62DocRemisionRecCop').val();

    // CERTIFICADO DE CALIDAD 2
    A.ACS_chk62DocCertificadoEnt = CheckBox_ToInt($('#chbACS_chk62DocCertificadoEnt'));
    A.ACS_txt62DocCertificadoEntCop = $('#tbACS_txt62DocCertificadoEntCop').val();
    A.ACS_chk62DocCertificadoRec = CheckBox_ToInt($('#chbACS_chk62DocCertificadoRec'));
    A.ACS_txt62DocCertificadoRecCop = $('#tbACS_txt62DocCertificadoRecCop').val();

    /*
    // Especififacione Adicionales 
    
    A.Acs_CondPagEntFac = CheckBox_ToInt($('#chbAcs_CondPagEntFac'));    
    A.Acs_CondPagEntFacCop= $('#tbAcs_CondPagEntFacCop').val();
    A.Acs_CondPagReFac = CheckBox_ToInt($('#chbAcs_CondPagReFac'));  
    A.Acs_CondPagReFacCop= $('#tbAcs_CondPagReFacCop').val();       
    A.Acs_CondPagEntOrdCom = CheckBox_ToInt($('#chbAcs_CondPagEntOrdCom'));  
    A.Acs_CondPagEntOrdComCop= $('#tbAcs_CondPagEntOrdComCop').val();           
    A.Acs_CondPagReOrdCom = CheckBox_ToInt($('#chbAcs_CondPagReOrdCom'));  
    A.Acs_CondPagReOrdComCop= $('#tbAcs_CondPagReOrdComCop').val(); 
    
    A.Acs_CondPagEntOrdRep = CheckBox_ToInt($('#chbAcs_CondPagEntOrdRep'));    
    A.Acs_CondPagEntOrdRepCop= $('#tbAcs_CondPagEntOrdRepCop').val();   
    A.Acs_CondPagReOrdCom = CheckBox_ToInt($('#chbAcs_CondPagReOrdCom'));    
    A.Acs_CondPagReOrdRepCop= $('#tbAcs_CondPagReOrdRepCop').val();   
    A.Acs_CondPagEntCopPed= CheckBox_ToInt($('#chbAcs_CondPagEntCopPed'));    
    A.Acs_CondPagEntCopPedCop= $('#tbAcs_CondPagEntCopPedCop').val();   
    A.Acs_CondPagReCopPed= CheckBox_ToInt($('#chbAcs_CondPagReCopPed'));    
    A.Acs_CondPagReCopPedCop= $('#tbAcs_CondPagReCopPedCop').val();          
    A.Acs_CondPagEntPagRem= CheckBox_ToInt($('#chbAcs_CondPagEntPagRem'));    
    A.Acs_CondPagEntPagRemCop= $('#tbAAcs_CondPagEntPagRemCop').val();   
    A.Acs_CondPagRePagRem = CheckBox_ToInt($('#chbAcs_CondPagRePagRem'));
    A.Acs_CondPagRePagRemCop = $('#tbAcs_CondPagRePagRemCop').val();   
    */

    // Servicios de Valor

    A.Acs_VisFrecuencia = $('#tbVis_Frecuencia').val();

    // SERVICIO TECNICO

    // L M M J V S D CD
    A.ACS_chk63Aplicar = CheckBox_ToInt($('#chb_ST_Aplicar'));
    A.ACS_chk63Lunes = CheckBox_ToInt($('#chb_ST_Lunes'));
    A.ACS_chk63Martes = CheckBox_ToInt($('#chb_ST_Martes'));
    A.ACS_chk63Miercoles = CheckBox_ToInt($('#chb_ST_Miercoles'));
    A.ACS_chk63Jueves = CheckBox_ToInt($('#chb_ST_Jueves'));
    A.ACS_chk63Viernes = CheckBox_ToInt($('#chb_ST_Viernes'));
    A.ACS_chk63Sabado = CheckBox_ToInt($('#chb_ST_Sabado'));
    A.ACS_chk63Domingo = CheckBox_ToInt($('#chb_ST_Domingo'));
    A.ACS_chk63CualquierDia = CheckBox_ToInt($('#chb_ST_CualquierDia'));

    A.ACS_chk63MismoDia = CheckBox_ToInt($('#chb_ST_MismoDia'));
    A.ACS_chk63Previa = CheckBox_ToInt($('#chb_ST_Previa'));

    A.ACS_Rad63TimePicker163 = $('#tb_ST_HorariosRecep1').val();
    A.ACS_Rad63TimePicker263 = $('#tb_ST_HorariosRecep2').val();
    A.ACS_Rad63TimePicker363 = $('#tb_ST_HorariosRecep3').val();
    A.ACS_Rad63TimePicker463 = $('#tb_ST_HorariosRecep4').val();

    A.ACS_Chk63Mismodia = CheckBox_ToInt($('#chb_ST_MismoDia'));
    A.ACS_Chk63Sincita = CheckBox_ToInt($('#chb_ST_MismoDia'));
    A.ACS_Chk63Previa = CheckBox_ToInt($('#chb_ST_MismoDia'));

    //OCT23-2019 RFH
    //MAY31-2020 RFH
    A.ServTecnico.ServRelleno = CheckBox_ToInt($('#ChkServTecnicoRelleno'));
    A.ServTecnico.ServPreventivo = CheckBox_ToInt($('#ChkServMantenimiento'));
    A.ServTecnico.QuienRecibe = $('#tbST_QuienRecibe').val();
    A.ServTecnico.FuncionQuienRecibe = $('#tbST_FuncionQuienRecibe').val();

    A.ServTecnico.Frecuencia = $('#ddlST_Frecuencia').val();
    A.ST_Relleno = CheckBox_ToInt($('#chb_ST_Relleno'));
    A.ST_Mantenimiento = CheckBox_ToInt($('#chb_ST_Mantenimiento'));


    // 
    // SERVICIO CAPACITACION 
    // 

    A.ServCapacitacion.Aplicar = CheckBox_ToInt($('#chb_ServCap_Aplicar'));
    A.ServCapacitacion.Tipo = 0;
    A.ServCapacitacion.Tipo1 = $('#sel_ServCap_Tipo1').val();
    A.ServCapacitacion.Tipo2 = $('#sel_ServCap_Tipo2').val();

    // L M M J V S D CD                                                  
    A.ServCapacitacion.Lunes = CheckBox_ToInt($('#chb_ServCap_Lunes'));
    A.ServCapacitacion.Martes = CheckBox_ToInt($('#chb_ServCap_Martes'));
    A.ServCapacitacion.Miercoles = CheckBox_ToInt($('#chb_ServCap_Miercoles'));
    A.ServCapacitacion.Jueves = CheckBox_ToInt($('#chb_ServCap_Jueves'));
    A.ServCapacitacion.Viernes = CheckBox_ToInt($('#chb_ServCap_Viernes'));
    A.ServCapacitacion.Sabado = CheckBox_ToInt($('#chb_ServCap_Sabado'));
    A.ServCapacitacion.Domingo = CheckBox_ToInt($('#chb_ServCap_Domingo'));
    A.ServCapacitacion.CualquierDia = CheckBox_ToInt($('#chb_ServCap_CualquierDia'));

    A.ServCapacitacion.HorariosRecep1 = $('#tb_ServCap_HorariosRecep1').val();
    A.ServCapacitacion.HorariosRecep2 = $('#tb_ServCap_HorariosRecep2').val();
    A.ServCapacitacion.HorariosRecep3 = $('#tb_ServCap_HorariosRecep3').val();
    A.ServCapacitacion.HorariosRecep4 = $('#tb_ServCap_HorariosRecep4').val();

    A.ServCapacitacion.CitaServ_MismoDia = CheckBox_ToInt($('#chb_ServCap_MismoDia'));
    A.ServCapacitacion.CitaServ_Previa = CheckBox_ToInt($('#chb_ServCap_Previa'));

    A.ServCapacitacion.Frecuencia = $('#ddlServCap_Frecuencia').val();

    // 
    // SERVICIO AUDITORIA
    // 

    A.ServAuditoria.Aplicar = CheckBox_ToInt($('#chb_ServAud_Aplicar'));
    A.ServAuditoria.Tipo = 0;

    A.ServAuditoria.Tipo1 = $('#sel_ServAud_Tipo1').val();
    A.ServAuditoria.Tipo2 = $('#sel_ServAud_Tipo2').val();

    // L M M J V S D CD
    A.ServAuditoria.Lunes = CheckBox_ToInt($('#chb_ServAud_Lunes'));
    A.ServAuditoria.Martes = CheckBox_ToInt($('#chb_ServAud_Martes'));
    A.ServAuditoria.Miercoles = CheckBox_ToInt($('#chb_ServAud_Miercoles'));
    A.ServAuditoria.Jueves = CheckBox_ToInt($('#chb_ServAud_Jueves'));
    A.ServAuditoria.Viernes = CheckBox_ToInt($('#chb_ServAud_Viernes'));
    A.ServAuditoria.Sabado = CheckBox_ToInt($('#chb_ServAud_Sabado'));
    A.ServAuditoria.Domingo = CheckBox_ToInt($('#chb_ServAud_Domingo'));
    A.ServAuditoria.CualquierDia = CheckBox_ToInt($('#chb_ServAud_CualquierDia'));

    A.ServAuditoria.CitaServ_MismoDia = CheckBox_ToInt($('#chb_ServAud_MismoDia'));
    A.ServAuditoria.CitaServ_Previa = CheckBox_ToInt($('#chb_ServAud_Previa'));

    A.ServAuditoria.HorariosRecep1 = $('#tb_ServAud_HorariosRecep1').val();
    A.ServAuditoria.HorariosRecep2 = $('#tb_ServAud_HorariosRecep2').val();
    A.ServAuditoria.HorariosRecep3 = $('#tb_ServAud_HorariosRecep3').val();
    A.ServAuditoria.HorariosRecep4 = $('#tb_ServAud_HorariosRecep4').val();

    A.ServAuditoria.Frecuencia = $('#ddlServAud_Frecuencia').val();

    // 
    // SERVICIO ASESORIA
    // 

    A.ACS_chk62Aplicar = CheckBox_ToInt($('#chb_ServAsesoria_Aplicar'));

    A.ACS_chk62Tipo1 = $('#sel_ServAsesoria_Tipo1').val();
    A.ACS_chk62Tipo2 = $('#sel_ServAsesoria_Tipo2').val();

    // L M M J V S D CD
    A.ACS_chk62Lunes = CheckBox_ToInt($('#chb_ServAsesoria_Lunes'));
    A.ACS_chk62Martes = CheckBox_ToInt($('#chb_ServAsesoria_Martes'));
    A.ACS_chk62Miercoles = CheckBox_ToInt($('#chb_ServAsesoria_Miercoles'));
    A.ACS_chk62Jueves = CheckBox_ToInt($('#chb_ServAsesoria_Jueves'));
    A.ACS_chk62Viernes = CheckBox_ToInt($('#chb_ServAsesoria_Viernes'));
    A.ACS_chk62Sabado = CheckBox_ToInt($('#chb_ServAsesoria_Sabado'));
    A.ACS_chk62Domingo = CheckBox_ToInt($('#chb_ServAsesoria_Domingo'));
    A.ACS_chk62CualquierDia = CheckBox_ToInt($('#chb_ServAsesoria_CualquierDia'));

    A.ACS_Chk62Mismodia = CheckBox_ToInt($('#chb_ServAsesoria_MismoDia'));
    A.ACS_Chk62Previa = CheckBox_ToInt($('#chb_ServAsesoria_Previa'));

    A.ACS_RadTimePicker162 = $('#tb_ServAsesoria_HorariosRecep1').val();
    A.ACS_RadTimePicker262 = $('#tb_ServAsesoria_HorariosRecep2').val();
    A.ACS_RadTimePicker362 = $('#tb_ServAsesoria_HorariosRecep3').val();
    A.ACS_RadTimePicker462 = $('#tb_ServAsesoria_HorariosRecep4').val();

    A.ServAsesoria.Frecuencia = $('#ddlServAsesoria_Frecuencia').val();

    /*
    A.Acs_PedidoEncargadoEnviar = $('#tbAcs_PedidoEncargadoEnviar').val();
    A.Acs_PedidoPuesto = $('#tbAcs_PedidoPuesto').val();
    A.Acs_PedidoTelefono = $('#tbAcs_PedidoTelefono').val();
    A.Acs_PedidoTelefono2 = $('#tbAcs_PedidoTelefono2').val();
    A.Acs_PedidoEmail = $('#tbAcs_PedidoEmail').val();
    A.Acs_PedidoEmail2 = $('#tbAcs_PedidoEmail2').val();
    */
   //         

    var Acs_RevisionFolio = CheckBox_ToInt($('#chkRevFolio'));
    var Acs_RevisionEntAlmacen = CheckBox_ToInt($('#chkRevRntrada'));
    var Acs_RevisionOrdenCompra = CheckBox_ToInt($('#chkRevOrden'));
    var Acs_RevisionRepConsumo = CheckBox_ToInt($('#chkRevReporte'));
    var Acs_RevisionCopiaFactura = CheckBox_ToInt($('#chkRevCopia'));
      
    var Acs_RevisionOtroDoc = $('#tbRevFac_Otro').val();

    var Acs_PagoContraEntrega = CheckBox_ToInt($('#chkRevFacContraEntrega'));
    var Acs_VisitaGestorCobranza = CheckBox_ToInt($('#chkRevFacVisGestorCobranza'));

    A.Acs_RevisionFolio = Acs_RevisionFolio;
    A.Acs_RevisionEntAlmacen = Acs_RevisionEntAlmacen;
    A.Acs_RevisionOrdenCompra = Acs_RevisionOrdenCompra;
    A.Acs_RevisionRepConsumo = Acs_RevisionRepConsumo;
    A.Acs_RevisionCopiaFactura = Acs_RevisionCopiaFactura;

    A.Acs_RevisionOtroDoc = Acs_RevisionOtroDoc;

    A.Acs_PagoContraEntrega = Acs_PagoContraEntrega;
    A.Acs_VisitaGestorCobranza = Acs_VisitaGestorCobranza;


    if (A.Acs_PedidoEncargadoEnviar == "" || A.Acs_PedidoPuesto == "" || A.Acs_PedidoTelefono == "" || A.Acs_PedidoEmail == "") {
        Spinner.css('display', 'none');
        alertify.alert('La información de "Encargado de enviar pedido" es obligatorio en seccion "Forma de envio de pedido".');
        return;
    }

    A.Acs_ComentariosRecomendaciones = $('#tbAcs_ComentariosRecomendaciones').val();

    // Acys2_Orden_Ajax.js ->
    AcysOrden_Guargar_Ajax(A, Spinner, function (RES) {
        Spinner.css('display', 'none');
        if (RES.Estado == -1) {
            alertify.alert('Ocurrio un error grave al tratar de guardar el documento.');
        } else {

            if (RES.Estado == 8) {
                alertify.alert('Se actualizo el Control de Orden:' + Id_Acs + '.');
                $('#modalOrdenCliente').modal('hide');
            } else {
                alertify.alert('Ocurrio un error grave al tratar de guardar el documento (561).');
            }
        }

    });

}