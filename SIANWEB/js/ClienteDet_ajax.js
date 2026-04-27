
// TERRITORIOS DE CLIENTE
function ClienteDet_TerritoriosCte(Id_Cte, CALLBACK) {

    $.ajax({
        url: _ApplicationUrl + '/api/CatClienteDet/Buscar_TerritoriosCte?Id_Cte=' + Id_Cte,
        type: 'GET',
        cache: false,
        contentType: 'application/json',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(comprobarRFC, null, parent, rfc);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        var Datos = response.Datos;
        var Estado = response.Estado;
        var Mensaje = response.Estado;

        if (Estado == 1) {
            CALLBACK(Datos);
        } else {

        }

    }).fail(function (jqXHR, textStatus, errorThrown) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
            default:
                $(this).modal('hide');
                $('#toastDanger #toastDangerMessage').html(jqXHR.responseJSON.Message);
                $('#toastDanger').fadeIn();
                setTimeout(function () {
                    $('#toastDanger').fadeOut();
                }, 3000);
                break;
        }
    }).always(function (jqXHR, textStatus, errorThrown) {
        //$('#' + parent + ' #imgRFCEnOperacion').hide();
    });
    //$('#spinnerBuscar').css('display', 'none');
    //  
}

// TERRITORIOS DE CLIENTE
function TerritoriosPorRik(Id_Rik, CALLBACK_Exito, CALLBACK_Error) {

    $.ajax({
        url: _ApplicationUrl + '/api/CatTerritorio/GetTerritoriosPorRik?Id_Rik=' + Id_Rik + '&Par1=0&Par2=0',
        type: 'GET',
        async: false,
        cache: false,
        contentType: 'application/json',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(comprobarRFC, null, parent, rfc);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        var Datos = response.Datos;
        var Estado = response.Estado;
        var Mensaje = response.Estado;

        if (Estado == 1) {
            if (CALLBACK_Exito) {
                CALLBACK_Exito(Datos);
            }
        } else {
            if (CALLBACK_Error) {
                CALLBACK_Error();
            }
        }

    }).fail(function (jqXHR, textStatus, errorThrown) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
            default:
                CALLBACK_Error();
                break;
        }
    }).always(function (jqXHR, textStatus, errorThrown) {
        //
    });
}