/*
Key Quimica Dic 2018 
24 Dic 2018 RFH 
*/

var LISTAPRODUCTOS_CONTADOR = 0; // Contador de partidas

function DatosCliente_Limpiar() {
    $('#tbAcys_CteNombre').val('');
    $('#tbAcys_CteNumero').val('');

    $('#tbAcys_CteDireccion').val('');
    $('#tbAcys_CteCol').val('');

    $('#tbAcys_CteMunicipio').val('');
    $('#tbAcys_CteCP').val('');
    $('#tbAcys_CteRFC').val('');

    $('#tbAcys_CteTerritorio').val('');
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
$(document).ready(function () {
    /*  
    $('#tblAcuerdoProds_Agregar').click(function () {
    ListadoProductos_AddRow();
    });
    */
    //ListadoProductos_AddRow('');


    /*
    $('.date').datepicker({
    format: 'dd/mm/yyyy'
    });
    */

    /*    
    $('.date').datepicker({
    dateFormat: 'dd/mm/yyyy'
    });
    */

    //$('.date').datepicker("option", "dateFormat", 'dd-mm-yyyy');

    /*
    $('.date').datepicker({
        format: 'dd/mm/yyyy',
        clearBtn: true
    });
    */


    /*
    $('input[id~="tbCodigo_"]').keypress(function (event) {
    var keycode = (event.keyCode ? event.keyCode : event.which);
    if (keycode == '13') {
    alert('You pressed a "enter" key in textbox');
    }
    });
    */
    });


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
    $(document).ready(function () {

        $('#modalBucarCliente_Cancelar').click(function () {
            $('#modalBuscarCliente').appendTo("body").modal('hide');
        });

        $('#btnBuscarCliente').click(function () {
            $('#tbBuscarCliente_Listado > tbody').empty();
            $('#modalBuscarCliente').appendTo("body").modal('show');
        });

        //
    });



