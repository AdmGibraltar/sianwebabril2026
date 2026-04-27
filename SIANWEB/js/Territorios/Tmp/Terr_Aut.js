
// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Consulta_AutPendiente(ClaveTerritorio, CALLBACK_Success, CALLBACK_Error) {
    $.ajax({
        url: _ApplicationUrl + '/api/Territorio/ConsultaAutorizacionPendienteTerritorio?ClaveTerritorio=' + ClaveTerritorio,
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
        var Reg = response.Datos;
        var Estado = response.Estado;

        if (Estado == 1) {
            if (CALLBACK_Success) {
                CALLBACK_Success(Reg)
            }
        } else {
            CALLBACK_Error();
        }

    }).fail(function (jqXHR, textStatus, error) {
        alertify.error(jqXHR.responseText);
    });
}

// 

$(document).ready(function () {

});



