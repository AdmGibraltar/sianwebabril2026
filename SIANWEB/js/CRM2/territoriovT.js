// 
// Key Quimica 
// Ene 2019 RFH 
// Consulta los territios por cliente
// 

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// Territorios Asociacion       
// 
function retirarAsignacionDeTerritorio(idCliente, idTerritorio, onSuccess, onFailure, always) {
    $.ajax({
        //url: _ApplicationUrl+ '/api/CatClienteDet/?idCte=' + idCliente + '&idTer=' + idTerritorio,
        url: _ApplicationUrl + '/api/CatClienteDet/ELIMINAR_TERRITORIOS_XCTE?Id_Cte=' + idCliente + '&Id_Ter=' + idTerritorio,
        type: 'DELETE',
        cache: false,
        //contentType: 'application/json',
        //data: JSON.stringify(data),
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(retirarAsignacionDeTerritorio, null, idCliente, idTerritorio, onSuccess, onFailure, always);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        var Estatus = response.Estado;
        var Datos = response.Datos;

        if (Estatus == -1) {
            alertify.error('No es posible eliminar el territorio, es posible que este enlazado a un proyecto.');
        } else {
            if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
                onSuccess(response);
            }
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
        if (typeof (onFailure) != undefined && typeof (onFailure) != 'undefined') {
            onFailure($);
        }
    }).always(function (jqXHR, textStatus, errorThrown) {
        if (typeof (always) != undefined && typeof (always) != 'undefined') {
            always($);
        }
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function CONSULTAR_TERRITORIOS_XCTE(Id_Cte, Id_Rik, CALLBACK_Exito, CALLBACK_Error) {
    //AsociacionProspectoTerritorio.prototype._cargarTerritoriosAsociados=function(idCte, onSuccess, onFailure, always){
    //console.log('DEBUG(function function CONSULTAR_TERRITORIOS_XCTE '+')');

    $.ajax({
        url: _ApplicationUrl + '/api/CatClienteDet/CONSULTAR_TERRITORIOS_XCTE/?Id_Cte=' + Id_Cte + '&Id_Rik=0',
        //url: '<%=ApplicationUrl %>' + '/api/CatClienteDet/?idCte=' + idCte,
        type: 'GET',
        cache: false,
        /*statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(AsociacionProspectoTerritorio.prototype._cargarTerritoriosAsociados, null, idCte, onSuccess, onFailure, always);
            }
        }*/
    }).done(function (response, textStatus, jqXHR) {
        var Datos = response.Datos;
        var Estado = response.Estado;
        if (Estado == 1) {
            CALLBACK_Exito(Datos);
        } else {
            CALLBACK_Error();
        }
        /*if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
            onSuccess(response);
        }*/
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
        if (typeof (onFailure) != undefined && typeof (onFailure) != 'undefined') {
            onFailure($);
        }
    }).always(function (jqXHR, textStatus, errorThrown) {
        if (typeof (always) != undefined && typeof (always) != 'undefined') {
            always($);
        }
    });
};

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Territorios(idRik, idProspecto, CallBack) {
    var res = [];
    $.ajax({
        //url: '<%=ApplicationUrl %>' + '/api/ProspectoTerritorio/?idEmp=' + '<%=EntidadSesion.Id_Emp %>' + '&idCd=' + '<%=EntidadSesion.Id_Cd %>' + '&idRik=' + idRik + '&idCrmProspecto=' + idProspecto,
        url: _ApplicationUrl + '/api/ProspectoTerritorio/?idEmp=' + _EntidadSesion_Id_Emp + '&idCd=' + _EntidadSesion_Id_Cd + '&idRik=' + idRik + '&idCrmProspecto=' + idProspecto,
        cache: false,
        type: 'GET',
    }).done(function (response, textStatus, jqXHR) {
        //res = response;
        $.each(response, function (index, element) {
            obj = {};
            obj.IdTerritorio = element.Id_Ter;
            obj.TerritorioNombre = element.Ter_Nombre;
            res.push(obj);
        });

        if (res.length > 0) {
            //ejecuta callback
            CallBack();
        }
        if (res.length <= 0) {
            alertify.error('No ha realizado la asociación de territorios.');
        }

    }).fail(function (jqXHR, textStatus, error) {
    }).always(function (jqXHR, textStatus, errorThrown) {
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// territorios por RIK
function ObtenerTerritorios_PorRik() {

    var TerrLst = [];

    $.ajax({
        url: _ApplicationUrl + '/api/CatTerritorio?X=1&Y=1',
        type: 'GET',
        cache: false,
        async: false,
        /*  statusCode: {
              401: function (jqXHR, textStatus, errorThrown) {
                  $('#dvDialogoInicioSesion').modal();
                  _onLoginSuccessful = $.proxy(cargarTerritorios, this, $, dvTerritoriosElement, jqElement, onSuccess, onFailure);
              }
          }*/
    }).done(function (response, textStatus, jqXHR) {
        //if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
        if (response != null) {
            TerrLst = response;
        }
        /*
            if (response != null) {
                //onSuccess(response);
                if (CALLBACK) {
                    CALLBACK(response);
                }                
            }*/

    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
            default:
                //$(this).modal('hide');
                $('#toastDanger #toastDangerMessage').html(jqXHR.responseJSON.Message);
                $('#toastDanger').fadeIn();
                setTimeout(function () {
                    $('#toastDanger').fadeOut();
                }, 3000);
                break;
        }
        if (typeof (onFailure) != undefined && typeof (onFailure) != 'undefined') {
            if (onFailure != null)
                onFailure($);
        }
    }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
        /*
        if (typeof (always) != undefined && typeof (always) != 'undefined') {
            if (always != null)
                always(jqXHROrData);
        */
    });

    return TerrLst;
}



// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
//JFCV 10 nov 2020 cargar datos de Leads


function DesarrollarLead(d) {
    alert('DesarrollarLead   Leads   ');
    alert(d);

};

function cargarDatosLeads(rowIdx) {

    console.log('DEBUG(function ' + arguments.callee.name + ')');


    debugger;
    var table = $('#tblLeads').DataTable();

    var data2 = $('#tblLeads').DataTable().row(rowIdx).data();


    $('#ModalNuevoProspecto #txtNombre').val(data2.Acs_NomComercial);
    $('#ModalNuevoProspecto #txtContacto').val(data2.Acs_Contacto);
    $('#ModalNuevoProspecto #txtEmail').val(data2.Acs_email);
    $('#ModalNuevoProspecto #txtTelefono').val(data2.Acs_Puesto);
}


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
//PROSPECTO / Grid - Cargar en seccion de SEGUIMIENTO la Información del prospecto.
//
function cargarDescripcion(rowIdx) {

    console.log('DEBUG(function ' + arguments.callee.name + ')');

    // DATOS GENERALES

    switch (_Parametro_ControlesSoloLectura) {
        case 0:
            //Edicion                        
            $('#btnCrearNota').prop('disabled', false);
            $('#btnDetallesAsociarTerritorio_Asociar').prop('disabled', false);
            break;
        case 1:
            // Solo lectura 
            $('#btnCrearNota').prop('disabled', true);
            $('#btnDetallesAsociarTerritorio_Asociar').prop('disabled', true);
            break;
    }

    //$('#tabSeguimiento a[href="#dvGeneral"]').tab('show');  

    var data = $('#tblProspectos').DataTable().row(rowIdx).data();

    $('#ddDatosGeneralesContacto').text(data.Cte_Contacto);
    $('#ddDatosGeneralesCorreoElectronico').text(data.Cte_Email);
    $('#ddDatosGeneralesTelefono').text(data.Cte_Telefono);
    $('#ddDatosGeneralesNombreComercial').text(data.Cte_NomComercial);
    $('#ddDatosGeneralesCalle').text(data.Cte_Calle);
    $('#dvSeguimiento:hidden').slideDown();

    //$('#ProspectoNombre').html('<strong>Seguimiento : ' + data.Cte_NomComercial + '</strong>');
    $('#hfDatosGenerales_Id_Cd').val(data.Id_Cd);
    $('#hfDatosGenerales_Id_CrmProspecto').val(data.Id_CrmProspecto);
    $('#hfDatosGenerales_Id_CrmTipoCliente').val(data.Id_CrmTipoCliente);
    $('#hfDatosGenerales_Id_Cte').val(data.Id_Cte);
    $('#hfDatosGenerales_Id_Emp').val(data.Id_Emp);
    $('#hfDatosGenerales_Id_Rik').val(data.Id_Rik);
    $('#hfDatosGenerales_Id_Ter').val(data.Id_Ter_Temporal);


    $('#dg_uenError').hide()
    $('#dg_segError').hide()
    $('#dg_tipoClienteError').hide()
    $('#dg_uen').attr('disabled', false)
    $('#dg_segmento').attr('disabled', false)
    $('#dg_tipoClientes').attr('disabled', false)

    if (data.Id_Uen > 0) {
        $('#dg_uen').val(data.Id_Uen)
        $('#dg_uen').attr('disabled', true)
    } else {
        $('#dg_uenError').show()
        $('#dg_uen').val(-1)
    }

    if (data.Id_Uen > 0 && data.Id_Seg > 0) cargarSegmentos(data.Id_Uen, data.Id_Seg)
    else if (data.Id_Uen > 0) cargarSegmentos(data.Id_Uen, null)
    else cargarSegmentos(-1, null)

    if (data.Id_Seg > 0) $('#dg_segmento').attr('disabled', true)
    else $('#dg_segError').show()

    if (data.Id_TCte > 0) {
        $('#dg_tipoClientes').val(data.Id_TCte)
        $('#dg_tipoClientes').attr('disabled', true)
    } else {
        $('#dg_tipoClientes').val(-1)
        $('#dg_tipoClienteError').show()
    }

    if (data.Id_Uen > 0 && data.Id_Seg > 0 && data.Id_TCte > 0) {
        $('#dg_mostrarBotonGuardar').hide()
        
    } else {
        $('#dg_mostrarBotonGuardar').show()
    }
    $('#dg_loader').hide()

    /*if (data.Id_Uen > 0 && data.Id_Seg > 0 && data.Id_TCte > 0) {
        $('#dg_uen').val(data.Id_Uen)
        cargarSegmentos(data.Id_Uen, data.Id_Seg)

        $('#dg_tipoClientes').val(data.Id_TCte)
        $('#dg_loader').hide()
        $('#dg_mostrarBotonGuardar').hide()
        $('#dg_uen').attr('disabled', true)
        $('#dg_segmento').attr('disabled', true)
        $('#dg_tipoClientes').attr('disabled', true)
    } else {
        $('#dg_uen').val(-1)
        cargarSegmentos(-1, null)
        $('#dg_tipoClientes').val(-1)
        $('#dg_mostrarBotonGuardar').show()
        $('#dg_uen').attr('disabled', false)
        $('#dg_segmento').attr('disabled', false)
        $('#dg_tipoClientes').attr('disabled', false)
        $('#dg_uenError').hide()
        $('#dg_segError').hide()
        $('#dg_tipoClienteError').hide()
    }*/

    _clienteSeleccionado = data;

    // NOTAS 
    cargarNotas(data);

    //cargarTerritoriosAsociadosAProspecto(data.Id_Cte, $.proxy(cargarTerritoriosAsociadosAProspectoSucceeded, null), $.proxy(cargarTerritoriosNoAsociadosAProspectoFailed, null));            
    //$('#dvSeccionTerritorios').asociacionprospectoterritorio({modo: $.fn.asociacionprospectoterritorio.MODOS.PERSISTENTE, idCte: data.Id_Cte, idRik: <%=EntidadSesion.Id_Rik %>});

    // TERRITORIOS

    //SEGMENTO ORIGINAL - BEGIN

    /*    
    $('#dvSeccionTerritorios').asociacionprospectoterritorio({
        modo: $.fn.asociacionprospectoterritorio.MODOS.PERSISTENTE,
        idCte: data.Id_Cte,
        idRik: _EntidadSesion_Id_Rik
    });
    */

    //SEGMENTO ORIGINAL - END 

    $('#Spinner_Seguimiento').css('display', 'block');
    $('#btnDetallesAsociarTerritorio_Asociar').prop('disabled', true); // boton de asociacion

    console.log(data)
    CONSULTAR_TERRITORIOS_XCTE(data.Id_Cte, data.Id_Rik, function (res) {

        $('#selDetallesAsociarTerritorio_Id_Ter').empty();
        $('#tblDetallesTerritoriosAsociados tbody').empty();

        if (res === null) {
            $('#selDetallesAsociarTerritorio_Id_Ter').prop('disabled', true);
            $('#Spinner_Seguimiento').css('display', 'none');
            $('#btnDetallesAsociarTerritorio_Asociar').prop('disabled', false); // boton de asociacion
            return;
        } else {
            $('#selDetallesAsociarTerritorio_Id_Ter').prop('disabled', false);
        }

        // Exito        
        // Territorios ACTIVOS & DE_RIK & No Asociados
        //$($select).find('option').remove();

        if (res.length < 0) {
            $('#selDetallesAsociarTerritorio_Id_Ter').prop('disabled', true);
        } else {
            $('#selDetallesAsociarTerritorio_Id_Ter').prop('disabled', false);
        }

        var vpometap = 0;
        var entro = 'N';

        for (var i = 0; i < res.length; i++) {
            if (res[i].Ter_Activo == 1 && res[i].TerDeRik == 1 && res[i].Ter_Asociado == 0) {
                //console.log('Territorio en combo: '+res[i].Id_Terr);                                        
                $('#selDetallesAsociarTerritorio_Id_Ter').append('<option ' +
                    'data-id_ter=' + res[i].Id_Terr + ' ' +
                    'data-terderik=' + res[i].TerDeRik + ' ' +
                    'data-ter_asociado=' + res[i].TerDeRik + ' ' +
                    'data-ter_vpometa=' + res[i].VPOMeta + ' ' +
                    'value="' + res[i].Id_Terr + '">' +
                    res[i].Id_Terr + ' - ' + res[i].Ter_Nombre +
                    '</option>');
                if (entro == 'N') {
                    vpometap = res[i].VPOMeta;
                    entro = 'S';
                }
            }
        }

        $('#selDetallesAsociarTerritorio_Id_Ter').selectpicker('val', 0);
        $('#selDetallesAsociarTerritorio_Id_Ter').selectpicker('refresh');
        $('txtDetallesAsociarTerritorio_PotencialMeta').val(vpometap);

        for (var i = 0; i < res.length; i++) {
            // Territorios ACTIVOS & Asociados
            if (res[i].Ter_Activo == 1 && res[i].Ter_Asociado == 1) {

                var Disables_btnRetirarTerritorio = '';
                if (res[i].TerDeRik == 0) {
                    var Disables_btnRetirarTerritorio = 'disabled';
                }

                var Cte_Potencial = parseFloat(res[i].Cte_Potencial).toFixed(2);
                if (isNaN(Cte_Potencial)) {
                    Cte_Potencial = 0;
                }

                //Cte_Potencial = Cte_Potencial.formatMoney(2, '.', ',');
                Cte_Potencial = parseFloat(Cte_Potencial).formatMoney(2, '.', ',');

                var VPOMetaAs = parseFloat(res[i].VPOMeta).toFixed(2);
                if (isNaN(VPOMetaAs)) {
                    VPOMetaAs = 0;
                }
                VPOMetaAs = parseFloat(VPOMetaAs).formatMoney(2, '.', ',');



                var Row = $('<tr id="' + res[i].Id_Terr + '">' +
                    '<td>' + res[i].Id_Terr + '<input type="hidden" id="hdnId_Ter" name="TerritoriosAsociados" value="' + res[i].Id_Terr + '"/>' +
                    '</td>' +
                    '<td> ' + res[i].Ter_Nombre + ' </td>' +
                    //'<td style="text-align: center; vertical-align:middle;">' +  res[i]. vpoFormateado + '<div style="display: none;">'+
                    '<td style="text-align: center; vertical-align:middle;">' + Cte_Potencial +
                    /*  '<div style="display: none;">'+
                          '<input type="text" id="txtCte_Potencial" name="Territorios" value="' + res[i].Cte_Potencial + '" />'+
                      '</div>'+*/
                    '</td>' +
                    '<td style="text-align: center; vertical-align:middle;">' + VPOMetaAs +
                    '</td>' +
                    '<td style="text-align: center; vertical-align:middle;">' +
                    //'<a id="aContactos"><i class="fa fa-user-plus fa-2x" aria-hidden="true"></i></a>'+
                    '<a id="btnContactos" ' +
                    'data-id_ter="' + res[i].Id_Terr + '" ' +
                    'data-id_seg="' + res[i].Id_Terr + '" ' +
                    'data-id_ctedet="' + res[i].Id_Terr + '"  ' +
                    'onclick="ModalListadoContactosShow(this);" >' +
                    '<i class="fa fa-user-plus fa-2x"></i>' +
                    '</a>' +
                    '</td>' +
                    /*'<td style="text-align: center; vertical-align:middle;">'+
                       '<button class="btn btn-primary"><i class="fa fa-tasks"></i></button>'+
                    '</td>'+*/
                    '<td style="text-align: center; vertical-align:middle;">' +
                    '<button ' +
                    'id="btnRetirarTerritorio" ' +
                    'type="button" ' +
                    'data-id_terr="' + res[i].Id_Terr + '" ' +
                    'data-ter_activo="' + res[i].Ter_Activo + '" ' +
                    'data-ter_asociado="' + res[i].Ter_Asociado + '" ' +
                    'data-terderik="' + res[i].TerDeRik + '" ' +
                    'data-terid="' + res[i].Id_Terr + '" ' +
                    'class="btn btn-primary" ' +
                    'onclick="btnRetirarTerritorio(this);" ' +
                    Disables_btnRetirarTerritorio + '>' +
                    '<i class="fa fa-times"></i>' +
                    '</button>' +
                    '</td>' +
                    '</tr>');

                $('#tblDetallesTerritoriosAsociados tbody').append(Row);
            }
        }

        $('#Spinner_Seguimiento').css('display', 'none');
        $('#btnDetallesAsociarTerritorio_Asociar').prop('disabled', false); // boton de asociacion

    }, function () {
        // Error 
        $('#Spinner_Seguimiento').css('display', 'none');
        $('#btnDetallesAsociarTerritorio_Asociar').prop('disabled', false); // boton de asociacion
        alert('Error');
    }
    );
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// Terrotorios Asociacion
//
/*
function btnDetallesAsociarTerritorio_Asociar$click(sender){    
    var idTer=$('#selDetallesAsociarTerritorio_Id_Ter').selectpicker('val');
    var territorioSeleccionado=$.grep(_territoriosDeRik, function(element, index){
        return element.Id_Ter==idTer;
    });
    var vpo=$('#txtDetallesAsociarTerritorio_Potencial').val();    
    asociarTerritorioACliente(_clienteSeleccionado.Id_Cte, idTer, _EntidadSesion_Id_Rik, territorioSeleccionado.Id_Seg, vpo, $.proxy(territorioAsociadoConExito, null));
}
*/

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// Asociar Territorio
function asociarTerritorioACliente(idCliente, idTer, idRik, idSeg, vpo, onSuccess, onFailure, always) {
    var data = {
        IdCte: idCliente,
        IdRik: idRik,
        IdTer: idTer,
        IdSeg: idSeg,
        VPO: vpo
    };
    $.ajax({
        url: _ApplicationUrl + '/api/CatClienteDet/',
        type: 'POST',
        cache: false,
        contentType: 'application/json',
        data: JSON.stringify(data),
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(asociarTerritorioACliente, null, idCliente, idTer, idRik, idSeg, vpo, onSuccess, onFailure, always);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
            onSuccess(response);
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
        if (typeof (onFailure) != undefined && typeof (onFailure) != 'undefined') {
            onFailure($);
        }
    }).always(function (jqXHR, textStatus, errorThrown) {
        if (typeof (always) != undefined && typeof (always) != 'undefined') {
            always($);
        }
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function btnRetirarTerritorio(sender) {
    var Id_Ter = $(sender).data('id_terr');
    retirarAsignacionDeTerritorio(_clienteSeleccionado.Id_Cte, Id_Ter, $.proxy(btnRetirarTerritorioExitosa, null, Id_Ter));
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
//
// Territorios Asociacion
//
function territorioAsociadoConExito(response) {

    console.log('DEBUG(function territorioAsociadoConExito)');

    //agregar el elemento al conjunto de _territorioAsociadosAProspecto
    _territorioAsociadosAProspecto.push(response);

    //remover el elemento elegido del selector de territorio
    $('#selDetallesAsociarTerritorio_Id_Ter option[value="' + response.Id_Ter + '"]').remove();
    $('#selDetallesAsociarTerritorio_Id_Ter').selectpicker('refresh');
    //agregar una nueva fila al listado de territorios asociados
    var tabletbody = $('#tblDetallesTerritoriosAsociados tbody');

    var Cte_Potencial = parseFloat(response.Cte_Potencial).toFixed(2);
    if (isNaN(Cte_Potencial)) {
        Cte_Potencial = 0;
    }

    Cte_Potencial = parseFloat(Cte_Potencial).formatMoney(2, '.', ',');

    var newRow = $('<tr id="' + response.Id_Ter + '">' +
        '<td>' + response.Id_Ter + '</td>' +
        '<td> ' + response.CatTerritorioSerializable.Ter_Nombre + ' </td>' +
        '<td style="text-align: center; vertical-align:middle;">' + Cte_Potencial + '</td>' +
        '<td style="text-align: center; vertical-align:middle;">' +
        //'<button class="btn btn-primary"><i class="'+ICON_NUEVO+'"></i></button>' +
        '<a id="btnContactos" ' +
        'data-id_ter="' + response.Id_Ter + '" ' +
        'data-id_seg="' + response.Id_Ter + '" ' +
        'data-id_ctedet="' + response.Id_Ter + '"  ' +
        'onclick="ModalListadoContactosShow(this);" >' +
        '<i class="fa fa-user-plus fa-2x"></i>' +
        '</a>' +
        '</td>' +
        '<td style="text-align: center">' +
        '<button ' +
        'type="button" ' +
        'data-id_terr="' + response.Id_Ter + '" ' +
        'data-terid="' + response.Id_Ter + '" ' +
        'onclick="btnRetirarTerritorio(this)" ' +
        'data-terid="' + response.Id_Ter + '" ' +
        'class="btn btn-primary">' +
        '<i class="fa fa-times"></i>' +
        '</button>' +
        '</td></tr>');

    $(tabletbody).append(newRow);
    $('#txtDetallesAsociarTerritorio_Potencial').val('');

    var elementosPresentersEnSelectorTerritorio = $('#selDetallesAsociarTerritorio_Id_Ter option').length;
    if (elementosPresentersEnSelectorTerritorio < 1) {
        $('#btnDetallesAsociarTerritorio_Asociar').prop('disabled', true);
    } else {
        $('#btnDetallesAsociarTerritorio_Asociar').prop('disabled', false);
    }
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
$(document).ready(function () {

    $('#btnDetallesAsociarTerritorio_Asociar').click(function (e) {
        // 26Ene2022 RFH
        var vpo = $('#txtDetallesAsociarTerritorio_Potencial').val();
        vpo = parseFloat(vpo);
        if (isNaN(vpo)) {
            vpo = 0;
        }
        if (vpo <= 0) {
            alertify.alert('El Valor Potencial Observado no puede ser Cero.');
            return;
        }
        var idTer = $('#selDetallesAsociarTerritorio_Id_Ter').selectpicker('val');
        var territorioSeleccionado = $.grep(_territoriosDeRik, function (element, index) {
            return element.Id_Ter == idTer;
        });
        if (!idTer) {
            alertify.alert('El territorio es requerido');
            return;
        }

        //var vpo = $('#txtDetallesAsociarTerritorio_Potencial').val();
        asociarTerritorioACliente(
            _clienteSeleccionado.Id_Cte, idTer, _EntidadSesion_Id_Rik, territorioSeleccionado.Id_Seg, vpo, $.proxy(territorioAsociadoConExito, null)
        );
    });

    /*
    $('#btnRetirarTerritorio').click(function (e) {
        
    });
    */

});