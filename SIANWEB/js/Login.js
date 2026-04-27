/*

Key Quimica Dic 2018 

24 Dic 2018 RFH 

*/


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
function redireccionarALogin() {
    self.location = _ApplicationUrl + '/login.aspx';
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
function login($) {
    $('#wrnDvDialogoInicioSesion').fadeOut();
    $.ajax({
        url: _ApplicationUrl + '/api/Login/',                  
        data: $('#frmDvDialogoInicioSesion').serialize(),
        cache: false,
        type: 'POST',
        statusCode: {
            506: function (jqXHR, textStatus, errorThrown) {
                //Manejar el caso apropiado
            },
            507: function (jqXHR, textStatus, errorThrown) {
                //Manejar el caso apropiado
            },
            508: function (jqXHR, textStatus, errorThrown) {
                //Manejar el caso apropiado
            },
            509: function (jqXHR, textStatus, errorThrown) {
                //Manejar el caso apropiado
            },
            510: function (jqXHR, textStatus, errorThrown) {
                //Manejar el caso apropiado
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        $('#dvDialogoInicioSesion').modal('hide');
        if (_onLoginSuccessful != null) {
            _onLoginSuccessful();
        }
    }).fail(function (jqXHR, textStatus, error) {
        //Mostrar el toast con el mensaje de error; retirar las llamadas para mostrar el toast en cada uno de los casos de código de respuesta, y solo manejar las acciones de los casos en particular por código.
        $('#wrnDvDialogoInicioSesion #msgWrnDvDialogoInicioSesion').html(jqXHR.responseJSON.Message);
        $('#wrnDvDialogoInicioSesion').fadeIn()
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
function login_ajax() {

    var User = $('#Username').val();
    var Password = $('#Password').val();
    
    $.ajax({
        url: _ApplicationUrl + '/api/Login/?User='+User+'&Pass='+Password,        
        cache: false,
        type: 'POST',
        statusCode: {
            506: function (jqXHR, textStatus, errorThrown) {
                //Manejar el caso apropiado
            },
            507: function (jqXHR, textStatus, errorThrown) {
                //Manejar el caso apropiado
            },
            508: function (jqXHR, textStatus, errorThrown) {
                //Manejar el caso apropiado
            },
            509: function (jqXHR, textStatus, errorThrown) {
                //Manejar el caso apropiado
            },
            510: function (jqXHR, textStatus, errorThrown) {
                //Manejar el caso apropiado
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        $('#dvDialogoInicioSesion').modal('hide');
        if (_onLoginSuccessful != null) {
            _onLoginSuccessful();
        }
    }).fail(function (jqXHR, textStatus, error) {
        //Mostrar el toast con el mensaje de error; retirar las llamadas para mostrar el toast en cada uno de los casos de código de respuesta, y solo manejar las acciones de los casos en particular por código.
        $('#wrnDvDialogoInicioSesion #msgWrnDvDialogoInicioSesion').html(jqXHR.responseJSON.Message);
        $('#wrnDvDialogoInicioSesion').fadeIn()
    });
}


$(document).ready(function () {

});