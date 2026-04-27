
// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Cargar_PermisosUsuario(Pagina, CALLBACK_Exito, CALLBACK_Error) {

    var ajax_urls = _ApplicationUrl + '/api/PermisosUsuario/?Pagina=' + Pagina
    $.ajax({
        url: ajax_urls,
        cache: false,
        type: 'GET',
        async:false,
    }).done(function (response, textStatus, jqXHR) {
        var Rec = response.Datos;
        var Estado = response.Estado;
        var Mensaje = response.Mensaje;

        if (Estado == 1) {
            if (CALLBACK_Exito) {
                CALLBACK_Exito(Rec);
            }
        } else {
            alertify.error('Error Territorios_CargarIndice::' + Mensaje);
        }

    }).fail(function (jqXHR, textStatus, error) {
        if (CALLBACK_Error) {            
            CALLBACK_Error();
        }
        if (jqXHR.status == 401) {
            alert('La sessión ha expirado.');
            //$('#dvModalPropuestaTE_ver2').modal('hide');
            $('#dvDialogoInicioSesion').appendTo("body").modal('show');
        } else {
            alertify.error('Error:' + jqXHR.responseText);
        }
    });
}

//

$(document).ready(function () {


});