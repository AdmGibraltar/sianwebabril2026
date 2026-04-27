
// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function SelAll_CatTipoCliente(CALLBACK_Success) {
    $.ajax({
        url: _ApplicationUrl + '/api/TipoCliente',
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
        var Lista = response.Datos;
        var Estado = response.Estado;
        if (Estado == 1) {
            if (CALLBACK_Success) {
                CALLBACK_Success(Lista)
            }
        }
    }).fail(function (jqXHR, textStatus, error) {
        alertify.error(jqXHR.responseText);
    });
}

//

function TipoCliente_CargarDdl(obj) {

    obj.prop('disabled', true);

    SelAll_CatTipoCliente(function (lst) {
        obj.empty();
        for (var i = 0; i < lst.length; i++) {
            obj.append('<option value=' + lst[i].Id_TCte + '>' + lst[i].Id_TCte + ' .-' + lst[i].TCte_Descripcion + '</option>');
        }
        obj.prop("selectedIndex", 0);
        obj.prop('disabled', false);
    }, function () {
        obj.prop('disabled', false);
    });
}

// 

$(document).ready(function () {

});

