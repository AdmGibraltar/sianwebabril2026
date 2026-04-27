/*

Key Quimica Dic 2018 

24 Dic 2018 RFH 

*/

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function CARGAR_INDICE_ORDENES(PageNumber, PageSize) {

    var tbFechaInicio = '';
    var tbFechaFin = '';
    var AplicarFecha = '';
    var AplicarFolio = '';

    var chbPorCliente = $('#chbPorCliente').is(':checked');
    var chbPorFolios = $('#chbPorFolios').is(':checked');
    var tbFolioIncial = $('#tbFolioIncial').val();
    var tbFolioFinal = $('#tbFolioFinal').val();
    
    var chbPorFechas = $('#chbPorFechas').is(':checked');
    if (chbPorFechas) {
        tbFechaInicio = $('#tbFechaInicio').val();
        tbFechaFin = $('#tbFechaFin').val();
    } else {
        tbFechaInicio = '';
        tbFechaFin = '';
    }

    $('#btnCargandoIndice').show();

    $('#tblAcysIndex > tbody').empty();

    Cargar_IndiceOrden_Ajax(
        PageNumber, PageSize,
        chbPorFechas, tbFechaInicio, tbFechaFin,
        chbPorFolios, tbFolioIncial, tbFolioFinal,
   function (lst) {

       try {
           if (lst.length <= 0) {
               $('#btnCargandoIndice').hide();
               alertify.success('No se encontraro registro.');
               return;
           }
       } catch (err) {
           alertify.success('Error: Ocurrio un error al tratar de cargar la información.');
           return;
       }

       $('#tblAcysIndex > tbody').empty();

       for (var i = 0; i < lst.length; i++) {
           var AcsEstatus = '';
           //console.log(lst[i].Id_Acs + "-" + lst[i].Acs_Estatus);
           switch (lst[i].Acs_Estatus) {
               case 'B':
                   AcsEstatus = '<span id="lbAcysEstaus_' + lst[i].Id_Acs + '" class="label label-default">Baja</span>';
                   break;
               case 'C':
                   AcsEstatus = '<span id="lbAcysEstaus_' + lst[i].Id_Acs + '" class="label label-info">Capturado</span>';
                   break;
               case 'S':
                   AcsEstatus = '<span id="lbAcysEstaus_' + lst[i].Id_Acs + '" class="label label-primary">Solicitado</span>';
                   break;
               case 'A':
                   AcsEstatus = '<span id="lbAcysEstaus_' + lst[i].Id_Acs + '" class="label label-success">Autorizado</span>';
                   break;
               case 'R':
                   AcsEstatus = '<span id="lbAcysEstaus_' + lst[i].Id_Acs + '" class="label label-warning">Rechazado</span>';
                   break;
               default:
                   $('#lbAcysEstaus').text('???');
           }

           var row = $('<tr>');
           row.append($('<td>').append(lst[i].Id_Acs));
           row.append($('<td style="text-align: center;">').append(lst[i].Acs_version));
           row.append($('<td style="text-align: center; vertical-align: middle;">').append(
           //lst[i].Acs_Estatus)
            '<p id="lbEstatusTexto_' + lst[i].Id_Acs + '" style="vertical-align: middle!important; maring-top:5px;">' + AcsEstatus + '</p>'

            ));
           row.append($('<td style="text-align: center;">').append(lst[i].Id_Cte));
           row.append($('<td style="text-align: left;">').append(lst[i].Cte_NomComercial));
           row.append($('<td style="text-align: center;">').append(lst[i].Id_Ter));
           row.append($('<td style="text-align: center;">').append(lst[i].Id_Rik));
           row.append($('<td style="text-align: center;">').append(lst[i].Acs_Fecha));
           row.append($('<td style="text-align: center;">').append(lst[i].Acs_FechaInicio));
           row.append($('<td style="text-align: center;">').append(lst[i].Acs_FechaFin));
           row.append($('<td style="text-align: center;">').append(lst[i].Acs_Vencido));
           row.append($('<td style="text-align: center;">').append(lst[i].Acs_Modalidad));

           row.append($('<td style="text-align: center">').append(
                '<button ' +
                    'data-id_acs=' + lst[i].Id_Acs + ' ' +
                    'data-acs_version=' + lst[i].Acs_version + ' ' +
                    'data-id_rik=' + lst[i].Id_Rik + ' ' +
                    'data-Id_Cte=' + lst[i].Id_Cte + ' ' +
                    'onclick="btnAcysOrden_Editar(this)" ' +
                    'type="button" ' +
                    'class="btn btn-default btn-sm"' +
                    'title="Abre el documento para edición." ' +
                '>' +
                    '<i class="fa fa-pencil-square-o"></i>' +
                '</button>'
            ));

           /*            
           row.append($('<td style="text-align: center">').append(
           '<button ' +
           'data-id_acs=' + lst[i].Id_Acs + ' ' +
           'data-acs_version=' + lst[i].Acs_Version + ' ' +
           'data-id_rik=' + lst[i].Id_Rik + ' ' +
           'data-Id_Cte=' + lst[i].Id_Cte + ' ' +
           'onclick="btnAcysOrdenCliente_Eliminar(this)" ' +
           'type="button" ' +
           'class="btn btn-default btn-sm"' +
           'title="Eliminar el documento" ' +               
           '>' +
           '<i class="fa fa-times" aria-hidden="true"></i>'+
           '</button>'
           ));

           row.append($('<td style="text-align: center">').append(
           '<button ' +
           'data-id_acs=' + lst[i].Id_Acs + ' ' +
           'data-acs_version=' + lst[i].Acs_Version + ' ' +
           'data-id_rik=' + lst[i].Id_Rik + ' ' +
           'data-Id_Cte=' + lst[i].Id_Cte + ' ' +
           'onclick="btnAcysOrdenCliente_Imprimir(this)" ' +
           'type="button" ' +
           'class="btn btn-default btn-sm"' +
           'title="Imprimir el documento" ' +               
           '>' +
           '<i class="fa fa-print"></i>'+
           '</button>'
           ));

           row.append($('<td style="text-align: center">').append(
           '<button ' +
           'data-id_acs=' + lst[i].Id_Acs + ' ' +
           'data-acs_version=' + lst[i].Acs_Version + ' ' +
           'data-id_rik=' + lst[i].Id_Rik + ' ' +
           'data-Id_Cte=' + lst[i].Id_Cte + ' ' +
           'onclick="btnAcysOrdenCliente_Reenviar(this)" ' +
           'type="button" ' +
           'class="btn btn-default btn-sm"' +
           'title="Renovar el documento" ' +               
           '>' +
           '<i class="fa fa-retweet" aria-hidden="true"></i>'+
           '</button>'
           ));

           row.append($('<td style="text-align: center">').append(
           '<button ' +
           'data-id_acs=' + lst[i].Id_Acs + ' ' +
           'data-acs_version=' + lst[i].Acs_Version + ' ' +
           'data-id_rik=' + lst[i].Id_Rik + ' ' +
           'data-Id_Cte=' + lst[i].Id_Cte + ' ' +
           'onclick="btnAcysOrdenCliente_Enviar(this)" ' +
           'type="button" ' +
           'class="btn btn-default btn-sm"' +
           'title="Enviar el documento" ' +               
           '>' +
           '<i class="fa fa-share" aria-hidden="true"></i>'+
           '</button>'
           ));

           row.append($('<td style="text-align: center">').append(
           '<button ' +
           'data-id_acs=' + lst[i].Id_Acs + ' ' +
           'data-acs_version=' + lst[i].Acs_Version + ' ' +
           'data-id_rik=' + lst[i].Id_Rik + ' ' +
           'data-Id_Cte=' + lst[i].Id_Cte + ' ' +
           'onclick="btnAcysOrdenCliente_Versiones(this)" ' +
           'type="button" ' +
           'class="btn btn-default btn-sm"' +
           'title="Versiones del documento" ' +               
           '>' +
           '<i class="fa fa-info" aria-hidden="true"></i>'+
           '</button>'
           ));

           row.append($('<td style="text-align: center">').append(
           '<button ' +
           'data-id_acs=' + lst[i].Id_Acs + ' ' +
           'data-acs_version=' + lst[i].acs_version + ' ' +
           'data-id_rik=' + lst[i].Id_Rik + ' ' +
           'data-Id_Cte=' + lst[i].Id_Cte + ' ' +
           'onclick="btnAcysOrdenCliente_Autorizar(this)" ' +
           'type="button" ' +
           'class="btn btn-default btn-sm"' +
           'title="Autorizar el documento" ' +               
           '>' +
           '<i class="fa fa-check-square-o" aria-hidden="true"></i>'+
           '</button>'
           ));
           */
           
           $('#tblAcysIndex > tbody').append(row);
       }

       Paginacion_Inicializar(ACyS_PaginaActual, ACyS_RegistroEncontrados);

       $('#btnCargandoIndice').hide();

   },
   function () {
        // CallBack_Error
       $('#btnCargandoIndice').hide();

   });
    
    //
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
$(document).ready(function () {

    console.log('Inicio: Acs2_OrdenIndex.js');

    Paginacion_Inicializar(1, 100);

    alertify.logPosition("bottom right");
        
    CARGAR_INDICE_ORDENES(1, ACyS_RenglonesXPagina);

    /*
    $('#tbFechaInicio').datepicker();
    $('#tbFechaFin').datepicker();
    */

    $('.datepicker').Zebra_DatePicker({
        format: 'd/m/Y'
    });

    $('#btnCargarListado').click(function () {
        ACyS_PaginaActual = 1;
        CARGAR_INDICE_ORDENES(1, ACyS_RenglonesXPagina);
        //Paginacion_Inicializar(1, 100);
        //Paginacion_Inicializar(PaginaActual, ACyS_RegistroEncontrados);
    });

    $('#btnOrdenBajarReporte').click(function () {
        $('#spinner_BajarReporte').css('display', 'block');
    });

    $('#chbPorFechas').click(function () {
        var chbPorFechas = $('#chbPorFechas').is(':checked');
        if (chbPorFechas) {
            $('#rowFechas').css('display', 'block');
        } else {
            $('#rowFechas').css('display', 'none');
            $('#tbFechaInicio').val('');
            $('#tbFechaFin').val('');
        }
    });

    $('#chbPorFolios').click(function () {
        var chbPorFechas = $('#chbPorFolios').is(':checked');
        if (chbPorFechas) {
            $('#rowFolios').css('display', 'block');
        } else {
            $('#rowFolios').css('display', 'none');
            $('#tbFolioIncial').val('');
            $('#tbFolioFinal').val('');
        }
    });

    $('#chbPorCliente').click(function () {
        var chbPorFechas = $('#chbPorCliente').is(':checked');
        if (chbPorFechas) {
            $('#rowCliente').css('display', 'block');
        } else {
            $('#rowCliente').css('display', 'none');
        }
    });

    $('#btnReducirVentana').click(function () {
        $('#modalAcys').find('.modal-dialog').css({
            width: '800px', //probably not needed
            height: 'auto', //probably not needed 
            'max-height': '100%'
        });

        $('#btnExpandirVentana').css('display', 'block');
        $('#btnReducirVentana').css('display', 'none');
    });

    $('#btnExpandirVentana').click(function () {
        $('#modalAcys').find('.modal-dialog').css({
            width: '100%', //probably not needed
            height: 'auto', //probably not needed 
            'max-height': '100%'
        });

        $('#btnExpandirVentana').css('display', 'block');
        $('#btnReducirVentana').css('display', 'none');
    });


    //
});