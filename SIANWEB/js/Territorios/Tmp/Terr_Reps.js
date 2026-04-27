
//

function Territorios_CargarUsuarios(Id_Uen, TipoRik, CALLBACK_Exito, CALLBACK_Error) {

    var ajax_urls = _ApplicationUrl + '/api/CatUsuarioRik/Get_Lista_Combo?Id_Uen=' + Id_Uen + '&TipoRik=' + TipoRik;

    $.ajax({
        url: ajax_urls,
        cache: false,
        type: 'GET',
        async: false,
    }).done(function (response, textStatus, jqXHR) {
        var lst = response.Datos;
        var Estado = response.Estado;
        var Mensaje = response.Mensaje;

        if (Estado == 1) {
            if (CALLBACK_Exito) {
                CALLBACK_Exito(lst);
            }
        } else {
            alertify.error('Error Territorios_CargarIndice::' + Mensaje);
        }

    }).fail(function (jqXHR, textStatus, error) {
        if (jqXHR.status == 401) {
            alert('La sessión ha expirado.');            
            $('#dvDialogoInicioSesion').appendTo("body").modal('show');
        } else {
            alertify.error('Error:' + jqXHR.responseText);
            if (CALLBACK_Error) {
                CALLBACK_Error();
            }
        }
    });
}


//

function TerrUsuarios_CargarCombos(Id_Uen, TipoRik, obj, IdDefault, CALLBACK_Success) {

    //obj.prop('disabled', true);

    Territorios_CargarUsuarios(Id_Uen, TipoRik, function (lst) {
        obj.empty();
        for (var i = 0; i < lst.length; i++) {
            obj.append('<option value=' + lst[i].Id_U + '>' + lst[i].U_Nombre+ '</option>');
        }

        if (IdDefault == 0) {
            obj.prop("selectedIndex", 0);
        } else {
            obj.val(IdDefault);
        }

        if (CALLBACK_Success) {
            CALLBACK_Success();
            //obj.prop('disabled', false);
        }

    }, function () {
        //obj.prop('disabled', false);
    });
}

// 

$(document).ready(function () {

});