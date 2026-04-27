// CARGA REPCuRESENTANTES
function Cargar_Representante(idZona, CALLBACK_Exito) {
    $.ajax({
        url: _ApplicationUrl + '/api/CrmRepresentante?Id_CD=0&Id_U=0',
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
        Estado = response.Estado;
        listado = response.Datos;
        //console.log(response);        
        $('#ddlRepresentante').empty();
        /*$('#ddlRepresentante').append(
            $('<option>').val(0).text('-- Seleccione --')
        );*/

        if (Estado == 1) {
            var ID = 0;
            for (var i = 0; i < listado.length; i++) {
                $('#ddlRepresentante').append(
                    $('<option data-Id_U=' + listado[i].Id_U + ' data-IdRik=' + listado[i].Id_Rik + ' >').val(listado[i].Id_U).text(listado[i].U_Nombre)
                );
                ID = listado[i].Id_U;
            }
            $('#ddlRepresentante').val(ID);

            if (hfId_Rik > 0) {
                $('#ddlRepresentante').val(hfId_Rik);
            }
            if (Id_TU == 2 || Id_TU == 3 || Id_TU == 4 || Id_TU == 5 || Id_TU == 1) {
                $('#ddlRepresentante').removeAttr('disabled');
            } else {
                $('#ddlRepresentante').prop('disabled', 'disabled');
            }
        }

        if (CALLBACK_Exito) {
            CALLBACK_Exito();
        }
    }).fail(function (jqXHR, textStatus, error) {
        alertify.error('Error: Carga de representantes');
    });
}

function btnCargarAgenda() {
    var Fecha = $('#tbFecha').val();
    Fecha = format_YYYYMMDD_2(Fecha);
    var Id_Rik = $('#ddlRepresentante').find(':selected').data('idrik');
    var Id_U = $('#ddlRepresentante').find(':selected').data('id_u');

    $.ajax({
        url: _ApplicationUrl + '/api/GCCrmVisita?Id_U=' + Id_U + '&Id_Rik=' + Id_Rik + '&Fecha=' + Fecha,
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
        Lst = response.Datos;
        console.log(listado);

        $('#tblCrmVisita > tbody').empty();

        for (i = 0; i < Lst.length; i++) {

            var row = $('<tr id="DocRow_' + Lst[i].IdCrmVisita + '">');

            row.append($('<td>').append(
                '<label>' + Lst[i].IdCrmVisita + '</label>'
            ));

            row.append($('<td>').append(
                '<label>' + Lst[i].Fecha + '</br>' + Lst[i].Inicio + '-' + Lst[i].Fin + '</label>'
            ));

            row.append($('<td class="text-center">').append(
                '<label>' + Lst[i].Motivo + '</label>'
            ));

            row.append($('<td class="text-left" >').append(
                '<label>' + Lst[i].Cliente.Cte_NomComercial + '</label>'
            ));

            row.append($('<td class="text-left" >').append(
                '<label>' + Lst[i].VPO + '</label>'
            ));

            row.append($('<td>').append(
                '<label>' + Lst[i].Comentarios + '</label>'
            ));

            Estatus = Lst[i].CheckEstatus;
            Estatus = parseInt(Estatus);
            switch (Estatus) {
                case 0:
                    row.append($('<td class="text-center">').append(
                        '<span class="label label-default">En espera</span>'
                    ));
                    break;
                case 1:
                    row.append($('<td class="text-center">').append(
                        '<span class="label label-danger">Iniciado</span>'
                    ));
                    break;
                case 2:
                    row.append($('<td class="text-center">').append(
                        '<span class="label label-info">Terminado</span>'
                    ));
                    break;
            }

            row.append($('<td style="width:40px;" class="text-center">').append(
                '<i class="fa fa-map-marker" aria-hidden="true"></i>'
            ));


            $('#tblCrmVisita > tbody').append(row);
        }

    }).fail(function (jqXHR, textStatus, error) {
        alertify.error('Error: Carga de representantes');
    });
}

// 
$(document).ready(function () {

    $('#tbFecha').Zebra_DatePicker({
        format: 'd/m/Y',
        always_visible: $('#container'),
        onSelect: function () {
            //var id_u = $('#ddlRepresentante').find(':selected').data('id_u');
            //alert(id_u);
            btnCargarAgenda();
        }
    });

    Cargar_Representante(0, function () {
    });

});
