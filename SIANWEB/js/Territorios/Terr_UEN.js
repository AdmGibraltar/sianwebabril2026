// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function SelAll_CatUEN(Id_Emp, Id_Uen, CALLBACK_Success,CALLBACK_Error) {
    $.ajax({
        url: _ApplicationUrl + '/api/CatUEN/SelCatUen?Id_Emp=0&Id_Uen=0',
        cache: false,
        type: 'GET',
        async: false,
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
        CALLBACK_Error();
    });
}

//

function UEN_CargarDdl(obj) {
    SelAll_CatUEN(0, 0, function (lst) {
        obj.empty();
        for (var i = 0; i < lst.length; i++) {
            obj.append('<option value=' + lst[i].Id_Uen + '>' + lst[i].Id_Uen + ' ' + lst[i].Descripcion + '</option>');
        }        
        obj.prop("selectedIndex", 0);
   });
}

//

function UEN_CargarDdl_wCB(obj, CALLBACK_Success) {

    obj.prop('disabled', true);

    SelAll_CatUEN(0, 0, function (lst) {

        console.log('Listado UEN');
        console.log(lst);
        
        obj.empty();
        for (var i = 0; i < lst.length; i++) {
            obj.append('<option value=' + lst[i].Id_Uen + '>' + lst[i].Id_Uen + ' .-' + lst[i].Descripcion + '</option>');
        }
        obj.prop("selectedIndex", 0);

        if (CALLBACK_Success) {
            CALLBACK_Success();
            obj.prop('disabled', false);
        }

    }, function () {
        obj.prop('disabled', false);
    });
}

// 

$(document).ready(function () {

});