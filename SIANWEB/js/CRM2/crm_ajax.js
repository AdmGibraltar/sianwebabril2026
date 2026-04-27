/*

MAY06-2020 RGH Creación 


*/


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Preparar_Grafica(CALLBACK_Exito) {

    var IdRik = $('#ddlRepresentantesComercial').val();
    console.log('IdRik: ' + IdRik);
    $('#Spinner_FullDashboard').css('display', 'block');
    $('#tbInforme1 > tbody').empty();

    if (typeof (idZona) == 'undefined' || idZona == null) {
        idZona = 0;
    }

    $.ajax({
        url: _ApplicationUrl + '/api/CrmInforme/?' +
        'TipoReporte=1' +
        '&Zona=-1' +
        '&Representante=' + IdRik +
        '&Periodo=-1' +
        '&Monto1=0' +
        '&Monto2=99999999',
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
        if (CALLBACK_Exito) {
            CALLBACK_Exito(response);
            $('#Spinner_FullDashboard').css('display', 'none');
        }
    }).fail(function (jqXHR, textStatus, error) {
        $('#Spinner_Cargando').css('display', 'none');
        //alertify.error('Ocurrió una complicación al cargar las UENs para el registro de Proyectos');                
    });
}
