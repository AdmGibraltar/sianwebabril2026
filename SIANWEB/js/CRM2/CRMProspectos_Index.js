/*

    Key Quimica
    Mayo23-2019 

*/

var CRM_PAGE_SIZE=10;
var CRM_PAGE_INDEX=0;
var CRM_PAGE_RECORDS_FOUND=0;


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Cargar_Indice_Ajax(
    TextoBuscar, PageIndex, PageSize, CALLBACK_Exito, CALLBACK_Error
) {

    
    $.ajax({
        url: _ApplicationUrl + '/api/CRMProspectos/' +
        '?' +
        'TextoBuscar=' + TextoBuscar +
        '&PageSize=' + PageSize,
        '&PageIndex=' + PageIndex,
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
        //Export_Excel_Informe1(response);
        //console.log(response);
        var lst = response.Datos;
        var Estado = response.Estado;
        var Mensaje = response.Mensaje;

        try {
            CRM_PAGE_RECORDS_FOUND = lst[0].RecordCount;
        } catch (err) {
            CRM_PAGE_RECORDS_FOUND= 0;
        }

        console.log('CRM_PAGE_RECORDS_FOUND:' + CRM_PAGE_RECORDS_FOUND);

        if (Estado == 1) {
            if (CALLBACK) {
                CALLBACK(lst);
            }
        } else {
            alertify.error('Error: Cargar_Indice_Ajax, ' + Mensaje);
        }

    }).fail(function (jqXHR, textStatus, error) {

        if (CALLBACK_Error) {
            CALLBACK_Error(textStatus);
        }

        if (jqXHR.status == 401) {
            alert('La sessión ha expirado.');
            //$('#dvModalPropuestaTE_ver2').modal('hide');
            $('#dvDialogoInicioSesion').appendTo("body").modal('show');
        } else {
            alertify.success('Ocurrió una error: funcion Cargar_PropuestaTecnoEconomica,' + Mensaje);

        }
    });
}

function Limpiar_Grid() {

}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Cargar_Indice() {

    console.log('Cargar_Indice()');

    var Text = $('#TextoBuscar').val();
    
    Limpiar_Grid();

    Cargar_Indice_Ajax(Text, CRM_PAGE_INDEX, CRM_PAGE_SIZE, function (lst) {
        // Exito 
        console.log(lst);

    },
    function () {
        // Error

    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
$(document).ready(function () {

    Cargar_Indice();

});