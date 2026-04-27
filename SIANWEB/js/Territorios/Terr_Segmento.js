// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function SelAll_CatSegmento(Id_Uen, CALLBACK_Success, CALLBACK_Error) {
    $.ajax({
        url: _ApplicationUrl + '/api/CatSegmento/SelCatSegmento?Id_Emp=0&Id_Uen=' + Id_Uen,
        cache: false,
        type: 'GET',
        async:false,
    }).done(function (response, textStatus, jqXHR) {
        var Lista = response.Datos;
        var Estado = response.Estado;
        if (Estado == 1) {
            if (CALLBACK_Success) {
                CALLBACK_Success(Lista)
            }
        } else {
            CALLBACK_Error();
        }

    }).fail(function (jqXHR, textStatus, error) {
        alertify.error(jqXHR.responseText);
    });
}

// NOV26-2019
function Consultar_Segmento(Segmento, CALLBACK_Success, CALLBACK_Error) {
    $.ajax({
        url: _ApplicationUrl + '/api/CatSegmento/Consultar_Segmento?Segmento=' + Segmento,
        cache: false,
        type: 'GET',
        async:false,
    }).done(function (response, textStatus, jqXHR) {
        var Lista = response.Datos;
        var Estado = response.Estado;
        if (Estado == 1) {
            if (CALLBACK_Success) {
                CALLBACK_Success(Lista)
            }
        } else {
            CALLBACK_Error();
        }

    }).fail(function (jqXHR, textStatus, error) {
        alertify.error(jqXHR.responseText);
    });
}
//
function Segmento_CargarDdl(IdUen, obj, CALLBACK_Success) {    
    obj.prop('disabled', true);
    SelAll_CatSegmento(IdUen, function (lst) {
        obj.empty();
        for (var i = 0; i < lst.length; i++) {
            obj.append('<option value=' + lst[i].Id_Seg + '>' + lst[i].Id_Seg + ' .-' + lst[i].Seg_Descripcion + '</option>');
        }
        obj.prop("selectedIndex", 0);
        obj.prop('disabled', false);
        if (CALLBACK_Success) {
            CALLBACK_Success();
        }
    }, function () {
        obj.prop('disabled', false);
    });
}

// 
$(document).ready(function () {

});

