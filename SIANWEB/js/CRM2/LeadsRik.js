/*
LeadsRiks.js 
Key Soluciones 
*/

var _renglonActual = 0;
var _columnaActual = 0;

var contador = 0;
var prospectoActualAEliminar = null;
//JFCV Leads
var leadActualCancelar = null;
var leadMotivoCancelar = null;

var _renglonDelProspectoAEliminar = null;
var _renglonDelLeadCancelar = null;
var _ROWIDX = 0

var _valorUnidadDimension = 0.0;

var _onLoginSuccessful = null;
var _indiceProspectoAActualizar = -1;
var _datosProspectoAActualizar = null;

var _clienteSeleccionado = null;

var _prospectoElegido = null;
var _bProspectoSeleccionadoDeLista = false;
var _peticionDeBusquedaNombreEmpresa = null;
var _peticionDeBusquedaExactaNombreEmpresa = null;
var _responseObjectBusquedaNombreEmpresa = null;
//JFCVCOmbos 16feb2021 
var valorcdi = 0;


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// Cargar Riks        
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

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// Cargar SUCURSALES TODAS         
// 
function Leads_CargarSucursales(Id_Cd, TipoSuc, CALLBACK_Exito, CALLBACK_Error) {

    var ajax_urls = _ApplicationUrl + '/api/CrmLeads/GetSucursales_combo?idCd=' + Id_Cd + '&idtipoSuc=' + TipoSuc;

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
            alertify.error('Error cargarsucursales::' + Mensaje);
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


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
//Territorios Asociacion
//
function cargarTerritoriosNoAsociadosAProspecto() {
    var territoriosNoAsociados = $.grep(_territoriosDeRik, function (element, index) {
        return $.grep(_territorioAsociadosAProspecto, function (element2, index2) {
            return element2.Id_Ter == element.Id_Ter;
        }).length < 1;
    });
    var $select = $('#selDetallesAsociarTerritorio_Id_Ter');
    $($select).find('option').remove();
    $.each(territoriosNoAsociados, function (index, element) {
        $($select).append('<option value="' + element.Id_Ter + '">' + element.Id_Ter + ' - ' + element.Ter_Nombre + '</option>');
    });
    $($select).selectpicker('val', 0);
    $($select).selectpicker('refresh');

    var elementosPresentersEnSelectorTerritorio = $('#selDetallesAsociarTerritorio_Id_Ter option').length;
    if (elementosPresentersEnSelectorTerritorio < 1) {
        $('#btnDetallesAsociarTerritorio_Asociar').prop('disabled', true);
    } else {
        $('#btnDetallesAsociarTerritorio_Asociar').prop('disabled', false);
    }
}



// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// Asociar Leads a Prospecto
// Se mueve a territorios.js 

function asociarLeadsAProspecto(idCliente, idlead) {
    alert("asociar lead a prospecto ");
    alert(idCliente);
    alert(idlead);

    var data = {
        IdCte: idCliente,
        IdLead: idlead
    };
    $.ajax({
        url: _ApplicationUrl + '/api/CrmLeads/',
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



// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function cargarDescripcionProspecto(objProspecto) {
    var data = objProspecto;
    $('#ddDatosGeneralesContacto').text(data.Cte_Contacto);
    $('#ddDatosGeneralesCorreoElectronico').text(data.Cte_Email);
    $('#ddDatosGeneralesTelefono').text(data.Cte_Telefono);
    $('#ddDatosGeneralesNombreComercial').text(data.Cte_NomComercial);
    $('#ddDatosGeneralesCalle').text(data.Cte_Calle);
    $('#dvSeguimiento:hidden').slideDown();
    _clienteSeleccionado = data;
    cargarNotas(data);
}



// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function selUEN$on_change() {
    var $selSegmento = $('#dvModalNuevoProyecto #selSegmento');
    var idUen = $('#dvModalNuevoProyecto #selUEN').selectpicker('val');
    despopularCascadaDependientesSelectorUENDialogoNuevoProyecto();
    cargarSegmentos(jQuery, $selSegmento, idUen);
}



// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function cargarTerritorios($, jqElement, idSeg, onSuccess, onFailure) {
    $('#imgProcesandoTerritorioDvModalNuevoProyecto').fadeIn();
    $.ajax({
        url: _ApplicationUrl + '/api/CatTerritorio/?idEmp=' + _EntidadSesion_Id_Emp + '&idCd=' + _EntidadSesion_Id_Cd + '&idRik=' + _EntidadSesion_Id_Rik + '&idSeg=' + idSeg,
        cache: false,
        type: 'GET',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(cargarTerritorios, null, $, jqElement, idSeg, onSuccess, onFailure);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        var $selTerritorio = jqElement;
        $selTerritorio.find('option').remove();
        $selTerritorio.append('<option value="0">--Seleccione--</option>');
        $.each(response, function (index, element) {
            $selTerritorio.append('<option value="' + element.Id_Ter + '">' + element.Ter_Nombre + '</option>');
        });
        $selTerritorio.selectpicker('val', 0);
        $selTerritorio.selectpicker('refresh');
        if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
            onSuccess();
        }
    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
        }

        $('#toastDanger #toastDangerMessage').html('Ocurrió una complicación al cargar los Territorios para el registro de Proyectos');
        $('#toastDanger').fadeIn();
        setTimeout(function () {
            $('#toastDanger').fadeOut();
        }, 3000);
        if (typeof (onFailure) != undefined && typeof (onFailure) != 'undefined') {
            onFailure($);
        }
    }).always(function (jqXHR, textStatus, errorThrown) {
        $('#imgProcesandoTerritorioDvModalNuevoProyecto').fadeOut();
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function cargarAreas($, jqElement, idSeg, onSuccess, onFailure) {
    $('#imgProcesandoAreaDvModalNuevoProyecto').fadeIn();
    $.ajax({
        url: _ApplicationUrl + '/api/CatArea/?idEmp=' + _EntidadSesion_Id_Emp + '&idSeg=' + idSeg,
        cache: false,
        type: 'GET',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(cargarAreas, null, $, jqElement, idSeg, onSuccess, onFailure);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        jqElement.find('option').remove();
        jqElement.append('<option value="0">--Seleccione--</option>');
        jqElement.append('<option value="-1">Otros</option>');
        $.each(response, function (index, element) {
            jqElement.append('<option value="' + element.Id_Area + '">' + element.Area_Descripcion + '</option>');
        });
        jqElement.selectpicker('val', 0);
        jqElement.selectpicker('refresh');

        habilitarSelectorDependienteDelSelectorSegmentoDialogoNuevoProyecto();

        if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
            onSuccess();
        }
    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
        }
        $('#toastDanger #toastDangerMessage').html('Ocurrió una complicación al cargar las Áreas para el registro de Proyectos');
        $('#toastDanger').fadeIn();
        setTimeout(function () {
            $('#toastDanger').fadeOut();
        }, 3000);
        if (typeof (onFailure) != undefined && typeof (onFailure) != 'undefined') {
            onFailure($);
        }
    }).always(function (jqXHR, textStatus, errorThrown) {
        $('#imgProcesandoAreaDvModalNuevoProyecto').fadeOut();
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function cargarSoluciones($, jqElement, idArea, onSuccess, onFailure) {
    $('#imgProcesandoSolucionDvModalNuevoProyecto').fadeIn();
    $.ajax({
        url: _ApplicationUrl + '/api/CatSolucion/?idEmp=' + _EntidadSesion_Id_Emp + '&idArea=' + idArea,
        cache: false,
        type: 'GET',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(cargarSoluciones, null, $, jqElement, idArea, onSuccess, onFailure);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        jqElement.find('option').remove();
        jqElement.append('<option value="0">--Seleccione--</option>');
        $.each(response, function (index, element) {
            jqElement.append('<option value="' + element.Id_Sol + '">' + element.Sol_Descripcion + '</option>');
        });
        jqElement.selectpicker('val', 0);
        jqElement.selectpicker('refresh');

        habiliatSelectorDependienteDelSelectorAreaDialogoNuevoProyecto();
        if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
            onSuccess($);
        }
    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
        }
        $('#toastDanger #toastDangerMessage').html('Ocurrió una complicación al cargar las Soluciones para el registro de Proyectos');
        $('#toastDanger').fadeIn();
        setTimeout(function () {
            $('#toastDanger').fadeOut();
        }, 3000);
        if (typeof (onFailure) != undefined && typeof (onFailure) != 'undefined') {
            onFailure($);
        }
    }).always(function (jqXHR, textStatus, errorThrown) {
        $('#imgProcesandoSolucionDvModalNuevoProyecto').fadeOut();
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function selArea$on_change() {
    var $selSolucion = $('#dvModalNuevoProyecto #selSolucion');
    var idArea = $('#dvModalNuevoProyecto #selArea').selectpicker('val');

    despopularCascadaDependientesSelectorAreaDialogoNuevoProyecto();
    if (idArea == -1) {
        _otrosSeleccionado = true;

        //poblar los listados de solución y aplicación con el elemento "Otros"
        //cargar los listados de solución y aplicación con los elementos "Otros".
        $selSolucion.find('option').remove();
        $selSolucion.append('<option value="-1">Otros</option>');
        //Se establece el valor "Otros" automáticamente en el selector de solución y aplicación
        $selSolucion.selectpicker('val', 0);
        $selSolucion.selectpicker('refresh');

        var $lstAplicacion = $('#dvModalNuevoProyecto #lstAplicacion');
        $lstAplicacion.find('div').remove();

        $('#dvModalNuevoProyecto #dvLstAplicacionesOtrosSlate').show();

    } else {
        _otrosSeleccionado = false;

        $('#dvModalNuevoProyecto #dvLstAplicacionesOtrosSlate').hide();
        cargarSoluciones(jQuery, $selSolucion, idArea);
    }
}

var _aplicacionesSeleccionadas = [];

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function cargarAplicaciones($, jqElement, idSol, onSuccess, onFailure) {
    $('#imgProcesandoAplicacionDvModalNuevoProyecto').fadeIn();
    var idUen = $('#dvModalNuevoProyecto #selUEN').selectpicker('val');
    var idSeg = $('#dvModalNuevoProyecto #selSegmento').selectpicker('val');
    var idArea = $('#dvModalNuevoProyecto #selArea').selectpicker('val');
    var idCte = $('#dvModalNuevoProyecto #hdnCliente').val();
    var idOp = $('#dvModalNuevoProyecto #hdnId_Op').val();
    var idOpVar = idOp != null ? idOp : '0';
    $.ajax({
        url: _ApplicationUrl + '/api/CatAplicacion/?idUen=' + idUen + '&idSeg=' + idSeg + '&idArea=' + idArea + '&idSol=' + idSol + '&idOp=' + idOpVar + '&idCte=' + idCte,
        cache: false,
        type: 'GET',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                //self.location='<%=ApplicationUrl %>' + '/login.aspx';
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(cargarAplicaciones, null, $, jqElement, idSol, onSuccess, onFailure);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        var $lstAplicacion = $('#lstAplicacion');
        $lstAplicacion.find('div').remove();

        jqElement.find('option').remove();
        //jqElement.append('<option value="0">--Seleccione--</option>');
        $.each(response, function (index, element) {
            jqElement.append('<option value="' + element.Id_Apl + '">' + element.Apl_Descripcion + '</option>');
            var node = $(contenidoPersonalizadoAplicacion(element, index));
            node.data('obj', element);
            node.find('[chkAplicacion]').data('obj', element);
            node.find('#txtAplVPO_' + element.Id_Apl).inputmask();
            //node.find('#txtAplVPO_' + element.Id_Apl).data('obj', element);
            $lstAplicacion.append(node);

            if (element.Apl_Activo == false) {
                $('#txtAplVPO_' + element.Id_Apl).prop('disabled', true);
            }

        });
        jqElement.selectpicker('val', 0);
        jqElement.selectpicker('refresh');
        $($lstAplicacion).iCheck({
            checkboxClass: 'icheckbox_square-blue',
            radioClass: 'iradio_square-blue'
        });
        $('input[chkAplicacion]').on('ifChecked', function (event) {
            var valoresAps = $('#selAplicacion').selectpicker('val');
            var apId = $(event.target).data('idapl');
            if (valoresAps == null) {
                valoresAps = [apId];
            } else {
                valoresAps.push(apId);
            }

            var apOpObj = $('#txtAplVPO_' + apId).data('obj');
            if (apOpObj == null) {
                var apObj = $(event.target).data('obj');
                apOpObj = {
                    Id_Emp: apObj.Id_Emp,
                    //Id_Cd: '<%=EntidadSesion.Id_Cd %>',
                    Id_Cd: _EntidadSesion_Id_Cd,
                    Id_Op: idOpVar != null ? idOpVar : 0,
                    Id_Apl: apObj.Id_Apl,
                    CrmOpAp_VPO: 0
                };
                $('#txtAplVPO_' + apId).data('obj', apOpObj);
            }
            _aplicacionesSeleccionadas.push(apOpObj);
            $('#txtAplVPO_' + apId).show();
            $('#selAplicacion').selectpicker('val', valoresAps);
            $('#selAplicacion').selectpicker('refresh');
        });
        $('input[chkAplicacion]').on('ifUnchecked', function (event) {
            var valoresAps = $('#selAplicacion').selectpicker('val');
            var apId = $(event.target).data('idapl');
            valoresAps = $.grep(valoresAps, function (value) {
                return value != apId;
            });
            _aplicacionesSeleccionadas = $.grep(_aplicacionesSeleccionadas, function (value) {
                return value.Id_Apl != apId;
            });
            $('#txtAplVPO_' + apId).hide();
            $('#selAplicacion').selectpicker('val', valoresAps);
            $('#selAplicacion').selectpicker('refresh');
        });
        habilitarSelectorDependienteDelSelectorSolucionDialogoNuevoProyecto();
        if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
            onSuccess($);
        }
    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 407:
                alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
        }
        $('#toastDanger #toastDangerMessage').html('Ocurrió una complicación al cargar las Aplicaciones para el registro de Proyectos');
        $('#toastDanger').fadeIn();
        setTimeout(function () {
            $('#toastDanger').fadeOut();
        }, 3000);
        if (typeof (onFailure) != undefined && typeof (onFailure) != 'undefined') {
            onFailure($);
        }
    }).always(function (jqXHR, textStatus, errorThrown) {
        $('#imgProcesandoAplicacionDvModalNuevoProyecto').fadeOut();
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function selSolucion$on_change() {
    var $selAplicacion = $('#dvModalNuevoProyecto #selAplicacion');
    var idSol = $('#dvModalNuevoProyecto #selSolucion').selectpicker('val');
    despopularCascadaDependientesSelectorSolucionDialogoNuevoProyecto();
    cargarAplicaciones(jQuery, $selAplicacion, idSol);
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function cerrarToastDanger($) {
    $('#toastDanger').fadeOut();
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function cerrarToastSuccess($) {
    $('#toastSuccess').fadeOut();
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function cerrarToastWarning($) {
    $('#toastWarning').fadeOut();
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function habilitarSelectorDependienteDelSelectorUENDialogoNuevoProyecto() {
    $('#selSegmento').prop('disabled', false);
    $('#selSegmento').selectpicker('refresh');
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function deshabilitarCascadaDependientesSelectorUENDialogoNuevoProyecto() {
    $('#selSegmento').selectpicker('refresh');
    deshabilitarCascadaDependientesSelectorSegmentoDialogoNuevoProyecto();
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function despopularCascadaDependientesSelectorUENDialogoNuevoProyecto() {
    $('#selSegmento').find('option').remove();
    $('#selSegmento').selectpicker('refresh');
    despopularCascadaDependientesSelectorSegmentoDialogoNuevoProyecto();
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function habilitarSelectorDependienteDelSelectorSegmentoDialogoNuevoProyecto() {
    $('#selArea').prop('disabled', false);
    $('#selArea').selectpicker('refresh');
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function deshabilitarCascadaDependientesSelectorSegmentoDialogoNuevoProyecto() {
    $('#selArea').selectpicker('refresh');
    $('#selTerritorio').selectpicker('refresh');

    deshabilitarCascadaDependientesSelectorAreaDialogoNuevoProyecto();
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function despopularCascadaDependientesSelectorSegmentoDialogoNuevoProyecto() {
    $('#selArea').find('option').remove();
    $('#selArea').selectpicker('refresh');
    despopularCascadaDependientesSelectorAreaDialogoNuevoProyecto();
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function habiliatSelectorDependienteDelSelectorAreaDialogoNuevoProyecto() {
    $('#selSolucion').prop('disabled', false);
    $('#selSolucion').selectpicker('refresh');
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function deshabilitarCascadaDependientesSelectorAreaDialogoNuevoProyecto() {
    $('#selSolucion').selectpicker('refresh');
    deshabilitarCascadaDependientesSelectorSolucionDialogoNuevoProyecto();
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function despopularCascadaDependientesSelectorAreaDialogoNuevoProyecto() {
    $('#selSolucion').find('option').remove();
    $('#selSolucion').selectpicker('refresh');
    despopularCascadaDependientesSelectorSolucionDialogoNuevoProyecto();
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function habilitarSelectorDependienteDelSelectorSolucionDialogoNuevoProyecto() {
    $('#selAplicacion').prop('disabled', false);
    $('#selAplicacion').selectpicker('refresh');
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function deshabilitarCascadaDependientesSelectorSolucionDialogoNuevoProyecto() {
    $('#selAplicacion').selectpicker('refresh');
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function despopularCascadaDependientesSelectorSolucionDialogoNuevoProyecto() {
    $('#selAplicacion').find('option').remove();
    $('#selAplicacion').selectpicker('refresh');

    var $lstAplicacion = $('#lstAplicacion');
    $lstAplicacion.find('div').remove();
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function inhabilitarSelectoresDialogoNuevoProyecto() {
    deshabilitarCascadaDependientesSelectorUENDialogoNuevoProyecto();
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Resetear_Busqueda() {

    _territorioNuevoProspecto = [];

    //limpiarFormaNuevoProspecto();
    $('#ModalNuevoProspecto #txtRFC').val('');
    $('#ModalNuevoProspecto #txtNombre').val('');
    $('#ModalNuevoProspecto #txtContacto').val('');
    $('#ModalNuevoProspecto #txtEmail').val('');
    $('#ModalNuevoProspecto #txtCalle').val('');
    $('#ModalNuevoProspecto #txtTelefono').val('');
    $('#txtRFC').val('');

    $('#txtRFC').prop('disabled', false);
    $('#txtRFC').attr('readonly', false);

    $('#tbNoCte').val('');

    $('#dvMenu_General #txtNombre').attr('readonly', false);
    $('#dvMenu_General #txtContacto').attr('readonly', false);
    $('#dvMenu_General #txtEmail').attr('readonly', false);
    $('#dvMenu_General #txtCalle').attr('readonly', false);
    $('#dvMenu_General #txtTelefono').attr('readonly', false);

    $('#ModalNuevoProspecto #hdnId_Cte').val('');
    $('#ModalNuevoProspecto #hdnCrearNuevo').val('');


    $('#btnDvModalNuevoProspectoGuardar').prop('disabled', false);
    //limpiarFormaNuevoProspecto();
    //cancelarCrearProyectoDeProspectoExistente('ModalNuevoProspecto');

    $('#ModalNuevoProspecto #lblMensajeRFC').hide();
    $('#ModalNuevoProspecto #lblRFCVacio').hide();
    $('#ModalNuevoProspecto #lblMensajeNombreEmpresa').hide();
    $('#ModalNuevoProspecto #icnRFCComprobado').hide();
    $('#ModalNuevoProspecto #icnRFCExistente').hide();

    //$('#dvMenu_Territorios').asociacionprospectoterritorio('limpiar');


    //$('#tabProspectoNuevoTerritorio a[href="#dvMenu_General"]').tab('show');            
    //$('#ModalNuevoProspecto').modal('show');
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function actualizarProspecto() {

    //     alert(  'jfcv actualizarProspecto'); 
    if ($('#frmDvModalEditarProspecto').valid() == false) {
        return;
    }
    $(this).prop('disabled', true);
    $('#imgDvModalEditarProspectoEnProgreso').fadeIn();
    $.ajax({
        url: _ApplicationUrl + '/api/CrmProspecto',
        type: 'PUT',
        cache: false,
        data: $('#frmDvModalEditarProspecto').serialize(),
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(actualizarProspecto, this);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        _datosProspectoAActualizar.Cte_NomComercial = $('#dvModalEditarProspecto #txtNombre').val();
        _datosProspectoAActualizar.Cte_Contacto = $('#dvModalEditarProspecto #txtContacto').val();
        _datosProspectoAActualizar.Cte_Email = $('#dvModalEditarProspecto #txtEmail').val();
        _datosProspectoAActualizar.Cte_Calle = $('#dvModalEditarProspecto #txtCalle').val();
        _datosProspectoAActualizar.Cte_Telefono = $('#dvModalEditarProspecto #txtTelefono').val();
        $('#toastSuccess #toastSuccessMessage').html('El prospecto ha sido actualizado con éxito');
        $('#toastSuccess').fadeIn();
        //deshabilitarCascadaDependientesSelectorUENDialogoNuevoProyecto();
        setTimeout(function () {
            $('#toastSuccess').fadeOut();
        }, 3000);
        $('#dvModalEditarProspecto').modal('hide');

    }).fail(function (jqXHR, textStatus, errorThrown) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
            default:
                $(this).modal('hide');
                $('#toastDanger #toastDangerMessage').html(jqXHR.responseJSON.ExceptionMessage);
                $('#toastDanger').fadeIn();
                setTimeout(function () {
                    $('#toastDanger').fadeOut();
                }, 3000);
                break;
        }
    }).complete(function () {
        $(this).prop('disabled', false);
        $('#imgDvModalEditarProspectoEnProgreso').fadeOut();
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
//
// Nuevo Prospecto 
//
function limpiarFormaNuevoProspecto() {
    $('#dvModalNuevoProspecto #txtRFC').val('');
    $('#dvModalNuevoProspecto #txtNombre').val('');
    $('#dvModalNuevoProspecto #txtContacto').val('');
    $('#dvModalNuevoProspecto #txtEmail').val('');
    $('#dvModalNuevoProspecto #txtCalle').val('');
    $('#dvModalNuevoProspecto #txtTelefono').val('');

    //             alert(  'jfcv limpiarFormaNuevoProspecto'); 

    var dvTerritoriosElement = $('#dvModalNuevoProspecto #dvTerritorios');
    var jqElement = $(dvTerritoriosElement).find('#selTerritorios');
    jqElement.selectpicker('val', 0);
    jqElement.selectpicker('refresh');
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
//
// Nuevo Prospecto 
//
function inicializarModalNuevoProspecto() {

    //     alert(  'jfcv inicializarModalNuevoProspecto'); 

    $('#dvModalNuevoProspecto').on('show.bs.modal', function (event) {
        //console.log('1122');
        var trigger = $(event.relatedTarget);
        $('#btnDvModalNuevoProspectoGuardar').prop('disabled', false);
        limpiarFormaNuevoProspecto();
        cancelarCrearProyectoDeProspectoExistente('dvModalNuevoProspecto');
        $('#dvModalNuevoProspecto #lblMensajeRFC').hide();
        $('#dvModalNuevoProspecto #lblRFCVacio').hide();
        $('#dvModalNuevoProspecto #lblMensajeNombreEmpresa').hide();
        $('#dvModalNuevoProspecto #icnRFCComprobado').hide();
        $('#dvModalNuevoProspecto #icnRFCExistente').hide();
        //$('#dvMenu_Territorios').asociacionprospectoterritorio('limpiar');
    });
    //'hidden.bs.modal'
    $('#dvModalNuevoProspecto').on('show.bs.modal', function (event) {
        //console.log('1136');
        $('#frmDvModalNuevoProspecto').validate().resetForm();
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Verificar_CrmProspecto(IdCte) {
    //IdCte = IdCte.trim();
    result = -1;
    if (IdCte > 0) {
        //if (IdCte!="") {        
        $.ajax({
            url: _ApplicationUrl + '/api/CatCliente/?IdCte=' + IdCte,
            type: 'GET',
            cache: false,
            async: false,
            contentType: 'application/json',
            statusCode: {}
        }).done(function (response, textStatus, jqXHR) {

            result = response;

        }).fail(function (jqXHR, textStatus, errorThrown) {
            alertify.error('Error : Verificar_CrmProspecto (' + IdCte + ')');
        }).always(function (jqXHR, textStatus, errorThrown) {

        });
    }

    return result;
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function DatosCliente_SoloLectura() {
    $('#txtRFC').attr('readonly', true);
    $('#txtRFC').attr('disabled', 'disabled');

    $('#txtNombre').attr('readonly', true);
    $('#txtContacto').attr('readonly', true);
    $('#txtEmail').attr('readonly', true);
    $('#txtCalle').attr('readonly', true);
    $('#txtTelefono').attr('readonly', true);
}



// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// actualizar Leads  

function ActualizarLeads(obj) {

    var idlead = $(obj).data('terid');
    var idRik = $(obj).data('rik');
    var idCDNuevo = $(obj).data('idcd');



    //    if (idRik > 0 && idCDNuevo > 0 ) {
    //           
    //                alertify.error('Solo elija un representante o si desea cambiar de sucursal elija otra sucursal y deje en blanco el representante.');
    //                return;
    //       }

    if (idRik == null) {
        idRik = -1;
        if (idCDNuevo == null) {
            alertify.error('Debe asignar un representante o elegir otra sucursal.');
            return;
        }

    }

    $('#imgSpinnerGuardar').css('display', 'block');

    var ajax_urls = _ApplicationUrl + '/api/CrmLeads/ActualizarAgente?idlead=' + idlead + '&idCDNuevo=' + idCDNuevo + '&idRik=' + idRik;

    $.ajax({
        url: ajax_urls,
        type: 'POST',
        cache: false,
        //        contentType: 'application/json',
        //        data: JSON.stringify(data),
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(asociarTerritorioACliente, null, idCDNuevo, idTer, idRik, idSeg, vpo, onSuccess, onFailure, always);
            }
        }
    }).done(function (response, textStatus, jqXHR) {

        ////////        if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {

        debugger;
        if (response = 1) {
            $('#imgSpinnerGuardar').css('display', 'none');
            //               asociarLeadsAProspecto(response.Prospecto.Id_Cte, IdLeads);
            //Notificaciones.agregarNotificacionRIK(new crm.navegacion.Notificacion({contenido: response.Notificacion.Notif_Contenido, tipo: response.Notificacion.Id_TipoNotificacion, id: response.Notificacion.Id_Notificacion}));            
            //traer el renglon en response y luego en lugar de add ponerle un remove 
            //  $('#tblLeads').DataTable().row.add(response).draw();

            location.reload();
            alertify.success('Cambios realizados con éxito');

        }
        else {
            alertify.error('Cambios realizados con éxito');
        }
        debugger;
        //            onSuccess(response);
        ////////        }

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
//
//PROSPECTO 
//
function guardarLead(sender) {

    debugger;
    alert('guardarLead');
    var RFC = $('#frmDvModalNuevoProspecto #txtRFC').val();
    var Nombre = $('#frmDvModalNuevoProspecto #txtNombre').val();
    var Contacto = $('#frmDvModalNuevoProspecto #txtContacto').val();
    var Email = $('#frmDvModalNuevoProspecto #txtEmail').val();
    var Calle = $('#frmDvModalNuevoProspecto #txtCalle').val();
    var Telefono = $('#frmDvModalNuevoProspecto #txtTelefono').val();
    var IdCte = $('#hdnId_Cte').val();
    IdCte = parseInt(IdCte);
    if (isNaN(IdCte)) {
        IdCte = 0;
    }

    RFC = RFC.trim();
    Nombre = Nombre.trim();
    Contacto = Contacto.trim();
    Email = Email.trim();
    Calle = Calle.trim();
    Telefono = Telefono.trim();

    Verifica = Verificar_CrmProspecto(IdCte);
    if (Verifica > 1) {
        alertify.error('Esta intentando duplicar este prospecto, debe realizar una busqueda y agregar el proyecto.');
        return;
    }

    //ABR30-2020 Modificacion para Campos Obligatorios y Opcionales

    /*
    if (RFC=='')
    {
        alertify.error('El RFC es requerido, si no lo tiene ahora puede capturar un ficticio y modificarlo mas tarde.');
        return;
    }
    */

    /*
    if (RFC=='' && Nombre=='' && Contacto=='' && Calle =='' && Telefono =='') {
        alertify.error('Debe establecer al menos el nombre.');
        return;
    }
    */

    if (IdCte <= 0) {
        if (Nombre == '' || Contacto == '') {
            alertify.error('Nombre de Empresa y Contacto Obligatorios.');
            return;
        }
        if (Email == '_@_._' && Telefono == '') {
            alertify.error('Debe establecer el Email o el Número de Teléfono.');
            return;
        }
    }


    /*
    // Si hay solo el RFC
    if (RFC!='' && Nombre=='' && Contacto=='' && Calle =='' && Telefono =='') {
        alertify.error('No es posible guardar sin información.');
        return;
    }
    */

    //JFCV LEADS traer el número de Lead en caso de que tenga uno asociado 
    var IdLeads = $('#ModalNuevoProspecto #HF_IdLeads').val();
    IdLeads = parseInt(IdLeads);


    IdLeads = $('#ModalNuevoProspecto #txtIdLead').val();

    if (isNaN(IdLeads)) {
        IdLeads = 0;
    }



    if ($('#frmDvModalNuevoProspecto').valid() == false) {
        return;
    }
    $('#imgDvModalNuevoProspectoEnProgreso').fadeIn();

    var ser = $('#frmDvModalNuevoProspecto').serialize();

    $('#imgSpinnerGuardar').css('display', 'block');

    $(sender).prop('disabled', true);
    $.ajax({
        url: _ApplicationUrl + '/api/CrmProspecto',
        type: 'POST',
        cache: false,
        data: $('#frmDvModalNuevoProspecto').serialize(),
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                //    JFCV                _onLoginSuccessful = $.proxy(guardarLead, sender);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        $('#imgSpinnerGuardar').css('display', 'none');

        if (IdLeads > 0) {

            asociarLeadsAProspecto(response.Prospecto.Id_Cte, IdLeads);

        }


        alertify.success('El prospecto ha sido creado con éxito');
        //Notificaciones.agregarNotificacionRIK(new crm.navegacion.Notificacion({contenido: response.Notificacion.Notif_Contenido, tipo: response.Notificacion.Id_TipoNotificacion, id: response.Notificacion.Id_Notificacion}));            
        $('#ModalNuevoProspecto').modal('hide');
        $('#dvSeguimiento').css('display', 'none');
        $('#tblProspectos').DataTable().row.add(response.Prospecto).draw();

    }).fail(function (jqXHR, textStatus, errorThrown) {
        $('#imgSpinnerGuardar').css('display', 'none');
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
            default:
                //$(this).modal('hide');
                //jqXHR.responseJSON.ExceptionMessage
                try {
                    console.log(jqXHR.responseJSON.InnerException.InnerException.ExceptionMessage);
                } catch (e) {
                    console.log('jqXHR.responseJSON.InnerException.InnerException.ExceptionMessage No esta definido');
                }

                alertify.error('Debe establer el nombre del prospecto o no es posible guardar el prospecto, verifique los requerimientos del CRM</br>o verifique el log del navegador.');
                //mostrarToast($('#toastDanger'), $('#dvModalNuevoProspecto'));
                setTimeout(function () {
                    $('#toastDanger').fadeOut();
                }, 3000);
                break;
        }
    }).always(function () {
        $('#imgSpinnerGuardar').css('display', 'none');
        $(sender).prop('disabled', false);
        $('#imgDvModalNuevoProspectoEnProgreso').fadeOut();
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function asociarTerritoriosAProspecto(idCte, alFinalizarExitosamente) {
    //indicador de progreso
    console.log('TERRITORIO:' + 1);
    $('#dvMenu_Territorios').asociacionprospectoterritorio('guardar', idCte, alFinalizarExitosamente);
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function eliminarProspecto($) {
    $.ajax({
        url: _ApplicationUrl + '/api/CrmProspecto/?IdCrmProspecto=' + prospectoActualAEliminar.Id_CrmProspecto + '&IdCte=' + prospectoActualAEliminar.Id_Cte,
        type: 'DELETE',
        cache: false,
        //data: prospectoActualAEliminar,
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(eliminarProspecto, null, $);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        _renglonDelProspectoAEliminar.remove();
        _renglonDelProspectoAEliminar = null;
        prospectoActualAEliminar = null;
        limpiarSeccionDatosGenerales();
        $('#tblProspectos').DataTable().draw();
        $('#dvModalEliminarProspecto').modal('hide');
        $('#toastSuccess #toastSuccessMessage').html('El prospecto se eliminó satisfactoriamente');
        $('#toastSuccess').fadeIn();
        setTimeout(function () {
            $('#toastSuccess').fadeOut();
        }, 3000);
    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('La sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
            default:
                $('#dvModalEliminarProspecto').modal('hide');
                //JFCV LEADS 
                $('#dvModalCancelarLeads').modal('hide');
                $('#toastDanger #toastDangerMessage').html(jqXHR.responseJSON.Message);
                $('#toastDanger').fadeIn();
                setTimeout(function () {
                    $('#toastDanger').fadeOut();
                }, 3000);
                break;
        }
    });
}

//JFCV LEADS 
// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function cancelarLead($) {

    leadMotivoCancelar = $('#dvModalCancelarLeads #HF_IdLeads').val();
    leadMotivoCancelar = $('#dvModalCancelarLeads #txtMotivo').val();


    $.ajax({
        url: _ApplicationUrl + '/api/CrmLeads/?IdLead=' + leadActualCancelar + '&tipocancelacion=' + 1 + '&motivocancelacion=' + leadMotivoCancelar,
        type: 'DELETE',
        cache: false,
        //data: leadActualCancelar,
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(leadMotivoCancelar, null, $);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        leadActualCancelar = null;
        leadMotivoCancelar = null;
        $('#dvModalCancelarLeads #HF_IdLeads').text('');
        $('#dvModalCancelarLeads #txtMotivo').text('');

        //JFCV LEADS 

        //            _renglonDelLeadCancelar.remove();
        //            _renglonDelLeadCancelar = null;

        $('#tblLeads').DataTable().draw();


        // $('#tblLeads').DataTable().draw();
        //jfcv leads inicializo la tabla 


        $('#dvModalCancelarLeads').modal('hide');
        $('#toastSuccess #toastSuccessMessage').html('El Lead se cancelo satisfactoriamente');
        $('#toastSuccess').fadeIn();
        setTimeout(function () {
            $('#toastSuccess').fadeOut();
        }, 4000);
        location.reload();
    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('La sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
            default:
                //JFCV LEADS 
                $('#dvModalCancelarLeads').modal('hide');
                $('#toastDanger #toastDangerMessage').html(jqXHR.responseJSON.Message);
                $('#toastDanger').fadeIn();
                setTimeout(function () {
                    $('#toastDanger').fadeOut();
                }, 3000);
                break;
        }
    });
}




// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function inicializarModalEliminarProspecto() {
    $('#dvModalEliminarProspecto').on('show.bs.modal', function (event) {
        //console.log('1499');
        var trigger = $(event.relatedTarget);
        var rowId = trigger.data('rowid');
        _renglonDelProspectoAEliminar = $('#tblProspectos').DataTable().row(rowId);
        var datosProspecto = _renglonDelProspectoAEliminar.data();
        prospectoActualAEliminar = datosProspecto;
        cargarDescripcion(rowId);
    });
}

//JFCV LEADS
// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function inicializarModalCancelarLead(idLead) {
    $('#dvModalCancelarLeads #HF_IdLeads').val(idLead);



    debugger;

    leadActualCancelar = idLead;

    var trigger = $(event.relatedTarget);
    var rowId = trigger.data('rowid');

    ///  _renglonDelLeadCancelar = $('#tblLeads').DataTable().row(rowId);
    //           var datosLeadacancelar = _renglonDelLeadCancelar.data();
    //           //prospectoActualAEliminar = datosProspecto;
    //           alert('cargue la info');
    //           alert(_renglonDelLeadCancelar.Id_Acs);

    $('#dvModalCancelarLeads').modal('show');
    // 
    //        switch (_Parametro_ControlesSoloLectura) {
    //        case 0:
    //            //Edicion       
    //            $('#btnProspectoEditarGuardar').prop('disabled',false);            
    //            break;
    //        case 1:
    //            // Solo lectura 
    //            $('#btnProspectoEditarGuardar').prop('disabled',true);            
    //            break;
    //        default:            
    //            $('#btnProspectoEditarGuardar').prop('disabled',true);
    //            break;


    //        $('#dvModalCancelarLeads').on('show.bs.modal', function (event) {
    ////            var trigger = $(event.relatedTarget);
    //            leadActualCancelar = idLead;
    //            //cargarDescripcion(rowId);

    //        });
}


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function limpiarSeccionDatosGenerales() {
    $('#ddDatosGeneralesContacto').text('');
    $('#ddDatosGeneralesCorreoElectronico').text('');
    $('#ddDatosGeneralesTelefono').text('');
    $('#ddDatosGeneralesNombreComercial').text('');
    $('#ddDatosGeneralesCalle').text('');
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
//
// Sucede hace hacer click en el boton "Guardar" del modal de "Nuevo Proyecto"
//
function crearProyecto() {

    $('#btnDvModalNuevoProyectoGuardar').attr('disabled', 'disabled');

    var bHayValorVacio = false;
    var bHayValorInvalido = false;
    var CuentaAplicacionesSeleccionadas = 0;

    // Verifica el valor VPO en las aplicaciones seleccionadas , loop para recorrer 
    for (var i = 0; i < 50; i++) {
        var VPO = $('input[name="FormaAplicaciones[' + i + '].VPO"]').length;
        if (VPO == 1) {
            var val = $('input[name="FormaAplicaciones[' + i + '].VPO"]').val();
            val = parseFloat(val);
            if (isNaN(val)) {
                val = 0;
            }
            var display = $('input[name="FormaAplicaciones[' + i + '].VPO"]').css('display');
            if (display == 'block') {
                CuentaAplicacionesSeleccionadas = CuentaAplicacionesSeleccionadas + 1;
            }
            val = parseFloat(val);
            if (display == 'block' && val <= 0) {
                bHayValorVacio = true;
            }

            if (val > 9999999) {
                bHayValorInvalido = true;
            }

        }
    }

    var bVI = $('#rbVtaInstalada').is(':checked');
    var bVE = $('#rbVtaEsporadica').is(':checked');

    if (bVI == false && bVE == false) {
        alertify.error('El tipo de venta es obligatorio (Instalada/Esporádica).');
        $('#btnDvModalNuevoProyectoGuardar').removeAttr('disabled');
        return;
    }

    if (bHayValorInvalido == true) {
        alertify.error('El VPO excede el maximo permitido por esta aplicación.');
        $('#btnDvModalNuevoProyectoGuardar').removeAttr('disabled');
        return;
    }

    // Si hay alguna con el valor sin establecer 
    if (bHayValorVacio == true) {
        alertify.error('Debe establecer el Valor VPO en las aplicaciones seleccionadas.');
        $('#btnDvModalNuevoProyectoGuardar').removeAttr('disabled');
        return;
    } else if (CuentaAplicacionesSeleccionadas <= 0) {
        alertify.error('Debe seleccionar las aplicaciones.');
        $('#btnDvModalNuevoProyectoGuardar').removeAttr('disabled');
        return;
    } else {
        //alertify.success('Se Guarda.');
    }

    var txtNombreProspecto = $('#txtNombreProspecto').text();
    txtNombreProspecto = txtNombreProspecto.trim();
    if (txtNombreProspecto.length <= 0) {
        alertify.error('ERROR : Faltan los datos del prospecto.');
        $('#btnDvModalNuevoProyectoGuardar').removeAttr('disabled');
        return;
    }

    $('#dvModalNuevoProyecto #selUEN').prop('disabled', false);
    $('#dvModalNuevoProyecto #selSegmento').prop('disabled', false);
    $(this).prop('disabled', true);
    $('#imgDvModalNuevoProyectoEnProgreso').fadeIn();
    $.ajax({
        url: _ApplicationUrl + '/api/CrmProyectoV2',
        type: 'POST',
        cache: false,
        data: $('#frmDvModalNuevoProyecto').serialize(),
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#btnDvModalNuevoProyectoGuardar').removeAttr('disabled');
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(crearProyecto, this);
            }
        }
    }).done(function (response, textStatus, jqXHR) {

        var $rbVtaEsporadica = $('#dvModalNuevoProyecto #rbVtaEsporadica');
        var bTipoVentaEsporadicaElegida = $rbVtaEsporadica.is(':checked');
        if (bTipoVentaEsporadicaElegida == true) {
            navegarAPedidoEsporadico();
        } else {
            alertify.success('El proyecto ha sido creado con éxito');
            $('#dvModalEditarProyecto').modal('hide');
            $('#btnDvModalNuevoProyectoGuardar').removeAttr('disabled');
        }
        /*    
        $.each(response.Notificaciones, function(index, element){
            Notificaciones.agregarNotificacionRIK(new crm.navegacion.Notificacion({contenido: element.Notif_Contenido, tipo: element.Id_TipoNotificacion, id: element.Id_Notificacion}));
        });
        */

        $('#dvModalNuevoProyecto').modal('hide');
    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
            case 521:
                alertify.errorsuccess('El proyecto ha sido creado con éxito');
                PatternflyToast.showWarning('El proyecto ha sido creado, pero algunas aplicaciones no pudieron ser asociadas. ' + jqXHR.responseJSON.ExceptionMessage, 10000);
                $('#dvModalNuevoProyecto').modal('hide');
                $('#btnDvModalNuevoProyectoGuardar').removeAttr('disabled');
                break;
            default:
                $('#btnDvModalNuevoProyectoGuardar').removeAttr('disabled');
                PatternflyToast.showError(jqXHR.responseJSON.Message, 10000);
                break;
        }
    }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
        $('#btnDvModalNuevoProyectoGuardar').removeAttr('disabled');
        $('#dvModalNuevoProyecto #selUEN').prop('disabled', true);
        $('#dvModalNuevoProyecto #selSegmento').prop('disabled', true);
        $('#dvModalNuevoProyecto #selUEN').selectpicker('refresh');
        $('#dvModalNuevoProyecto #selSegmento').selectpicker('refresh');
        $(this).prop('disabled', false);
        $('#imgDvModalNuevoProyectoEnProgreso').fadeOut();
    });
}

var _otrosSeleccionado = false;


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function retirarElementosDeAplicacionEnForma($forma) {
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function actualizarAplicacionesVPO(idOp, onSuccess, onFailure) {
    $.each(_aplicacionesSeleccionadas, function (index, item) {
        item.Id_Op = idOp;
    });
    $.ajax({

        url: _ApplicationUrl + '/api/CrmOportunidadesAplicacion',
        type: 'PUT',
        cache: false,
        data: JSON.stringify({
            IdOp: idOp,
            OportunidadesAplicacion: _aplicacionesSeleccionadas
        }),
        contentType: 'application/json',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(actualizarAplicacionesVPO, this, idOp, onSuccess, onFailure);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        _aplicacionesSeleccionadas = [];
        if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
            if (onSuccess != null) {
                onSuccess(response, textStatus, jqXHR);
            }
        }
    }).fail(function (jqXHR, textStatus, error) {
        if (typeof (onFailure) != undefined && typeof (onFailure) != 'undefined') {
            if (onFailure != null) {
                onFailure(jqXHR, textStatus, error);
            }
        }
    }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function limpiarFormaNuevoProyecto() {
    $('#hdnDim_Id_Uen').val(null);
    $('#hdnDim_Id_Seg').val(null);
    $('#txtDimension').val('');
    $('#txtCantidad').val('');
    $('#txtVPM').val('');
    var $selTerritorio = $('#dvModalNuevoProyecto #selTerritorio');
    $selTerritorio.selectpicker('val', 0);
    $selTerritorio.selectpicker('refresh');
    $('#dvModalNuevoProyecto #selUEN').selectpicker('val', 0);
    $('#dvModalNuevoProyecto #selUEN').selectpicker('refresh');
    despopularCascadaDependientesSelectorTerritorio();
    //despopularCascadaDependientesSelectorUENDialogoNuevoProyecto();
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function cargarTerritoriosDeProspecto($, jqElement, idRik, idProspecto, onSuccess, onFailure) {

    $('#imgProcesandoTerritorioDvModalNuevoProyecto').fadeIn();
    $.ajax({
        url: _ApplicationUrl + '/api/ProspectoTerritorio/?idEmp=' + _EntidadSesion_Id_Emp + '&idCd=' + _EntidadSesion_Id_Cd + '&idRik=' + idRik + '&idCrmProspecto=' + idProspecto,
        cache: false,
        type: 'GET',
        async: false,
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(cargarTerritoriosDeProspecto, null, $, jqElement, idRik, idProspecto, onSuccess, onFailure);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        var $selTerritorio = jqElement;
        $selTerritorio.find('option').remove();
        var ContTerritorios = 0;

        $.each(response, function (index, element) {
            var node = $('<option value="' + element.Id_Ter + '">' + element.Id_Ter + ' - ' + element.Ter_Nombre + '</option>');
            node.data('objterritorio', element);
            $selTerritorio.append(node);
            ContTerritorios++;
        });
        $selTerritorio.selectpicker('val', 0);
        $selTerritorio.selectpicker('refresh');

        if (ContTerritorios <= 0) {
            $('#divErrorEncontado').css('display', 'block');
            $('#divBotonesAccion').css('display', 'none');
        } else {
            $('#divErrorEncontado').css('display', 'none');
            $('#divBotonesAccion').css('display', 'block');
        }

        if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
            onSuccess();
        }
    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
        }

        $('#toastDanger #toastDangerMessage').html('Ocurrió una complicación al cargar los Territorios para el registro de Proyectos');
        $('#toastDanger').fadeIn();
        setTimeout(function () {
            $('#toastDanger').fadeOut();
        }, 3000);
        if (typeof (onFailure) != undefined && typeof (onFailure) != 'undefined') {
            onFailure($);
        }
    }).always(function (jqXHR, textStatus, errorThrown) {
        $('#imgProcesandoTerritorioDvModalNuevoProyecto').fadeOut();
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
//
// Nuevo Proyecto
//
function inicializarModalNuevoProyecto() {

    $('#dvModalNuevoProyecto').on('show.bs.modal', function (event) {
        //console.log('1777');
        var trigger = $(event.relatedTarget);
        $('#btnDvModalNuevoProyectoGuardar').prop('disabled', false);
        var rowId = $(trigger).data('rowidx');
        rowId = _ROWIDX;
        var datosProspecto = $('#tblProspectos').DataTable().row(rowId).data();
        if (_prospectoElegido != null) {
            datosProspecto = _prospectoElegido;
        }

        var $selTerritorio = $('#dvModalNuevoProyecto #selTerritorio');

        limpiarFormaNuevoProyecto();
        cargarTerritoriosDeProspecto($, $selTerritorio, datosProspecto.Id_Rik, datosProspecto.Id_CrmProspecto);

        $('#dvModalNuevoProyecto #hdnId_CrmProspecto').val(datosProspecto.Id_CrmProspecto);
        $('#dvModalNuevoProyecto #hdnCliente').val(datosProspecto.Id_Cte);
        //$('#dvModalNuevoProyecto #txtNombreProspecto').val(datosProspecto.Cte_NomComercial);
        $('#dvModalNuevoProyecto #txtNombreProspecto').text(datosProspecto.Cte_NomComercial);

        if (rowId != undefined) {
            cargarDescripcion(rowId);
        } else {
            cargarDescripcionProspecto(datosProspecto);
        }
        selTerritorio_on_change();

    });
    $('#dvModalNuevoProyecto').on('hide.bs.modal', function (event) {
        _prospectoElegido = null;
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function IniciaProyecto() {
    $('#dvModalNuevoProyecto').modal('show');
    //inicializarModalNuevoProyecto();
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function NuevoProyecto(obj) {
    var IdCrmProspecto = $(obj).data('idcrmprospecto');
    var RowIdx = $(obj).data('rowidx');
    _ROWIDX = RowIdx;
    cargarDescripcion(RowIdx);

    //$('#selTipoDeVenta').val('0');

    $('#selTipoDeVenta').val(0).change();

    $('#rbVtaInstalada').prop('checked', false);
    $('#rbVtaInstalada').iCheck('update');
    $('#rbVtaEsporadica').prop('checked', false);
    $('#rbVtaEsporadica').iCheck('update');


}







// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function selTerritorio$on_change(_this) {
    var value = $(_this).selectpicker('val');
    var objTerritorio = $(_this).find('option[value="' + value + '"]').data('objterritorio');
    despopularCascadaDependientesSelectorTerritorio();
    var $selUEN = $('#dvModalNuevoProyecto #selUEN');
    var $selSegmento = $('#dvModalNuevoProyecto #selSegmento');
    $('#txtDimension').val('');
    $('#txtPrecioUnidad').val('');
    if (objTerritorio != typeof (undefined) && objTerritorio != 'undefined' && objTerritorio != undefined) {
        //cargarSelUEN($selUEN, objTerritorio.CatUENSerializable);
        cargarSelUEN_Ver2($selUEN, objTerritorio.Id_Uen, objTerritorio.Uen_Descripcion);
        //cargarSegmento($selSegmento, objTerritorio.CatSegmentoSerializable);
        cargarSegmento_Ver2($selSegmento, objTerritorio);
        selSegmento$on_change();
    }
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function selTerritorio_on_change() {
    var value = $('#selTerritorio').selectpicker('val');
    var objTerritorio = $('#selTerritorio').find('option[value="' + value + '"]').data('objterritorio');
    //console.log('objTerritorio:'+objTerritorio);
    despopularCascadaDependientesSelectorTerritorio();
    var $selUEN = $('#dvModalNuevoProyecto #selUEN');
    //console.log('selUEN:'+selUEN);
    var $selSegmento = $('#dvModalNuevoProyecto #selSegmento');
    //console.log('selSegmento:'+selSegmento);
    $('#txtDimension').val('');
    $('#txtPrecioUnidad').val('');
    if (objTerritorio != typeof (undefined) && objTerritorio != 'undefined' && objTerritorio != undefined) {
        //cargarSelUEN($selUEN, objTerritorio.CatUENSerializable);
        cargarSelUEN_Ver2($selUEN, objTerritorio.Id_Uen, objTerritorio.Uen_Descripcion);
        //cargarSegmento($selSegmento, objTerritorio.CatSegmentoSerializable);
        cargarSegmento_Ver2($selSegmento, objTerritorio);
        selSegmento$on_change();
    }
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function despopularCascadaDependientesSelectorTerritorio() {
    $('#selUEN').find('option').remove();
    $('#selUEN').selectpicker('refresh');
    despopularCascadaDependientesSelectorUENDialogoNuevoProyecto();
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function cargarSegmento_Ver2(jqelement, objSeg) {
    jqelement.append('<option value="' + objSeg.Id_Seg + '">' + objSeg.Seg_Descripcion + '</option>');
    jqelement.selectpicker('val', 0);
    jqelement.selectpicker('refresh');

    $('#dvModalNuevoProyecto #hdnDim_Id_Uen').val(objSeg.Id_Uen);
    $('#dvModalNuevoProyecto #hdnDim_Id_Seg').val(objSeg.Id_Seg);
    $('#dvModalNuevoProyecto #txtDimension').val(objSeg.Seg_Unidades);
    $('#dvModalNuevoProyecto #txtPrecioUnidad').val(objSeg.Seg_ValUniDim);
    _valorUnidadDimension = objSeg.Seg_ValUniDim;
    var cantidad = $('#dvModalEditarProyecto #txtCantidad').val();
    cantidad = isNaN(cantidad) ? 0 : cantidad;
    $('#dvModalNuevoProyecto #txtVPM').val(cantidad * objSeg.Seg_ValUniDim);
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function cargarSegmento(jqelement, objSeg) {
    jqelement.append('<option value="' + objSeg.Id_Seg + '">' + objSeg.Seg_Descripcion + '</option>');
    jqelement.selectpicker('val', 0);
    jqelement.selectpicker('refresh');

    $('#dvModalNuevoProyecto #hdnDim_Id_Uen').val(objSeg.Id_Uen);
    $('#dvModalNuevoProyecto #hdnDim_Id_Seg').val(objSeg.Id_Seg);
    $('#dvModalNuevoProyecto #txtDimension').val(objSeg.Seg_Unidades);
    $('#dvModalNuevoProyecto #txtPrecioUnidad').val(objSeg.Seg_ValUniDim);
    _valorUnidadDimension = objSeg.Seg_ValUniDim;
    var cantidad = $('#dvModalEditarProyecto #txtCantidad').val();
    cantidad = isNaN(cantidad) ? 0 : cantidad;
    $('#dvModalNuevoProyecto #txtVPM').val(cantidad * objSeg.Seg_ValUniDim);
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function cargarSelUEN(jqelement, objUen) {
    jqelement.append('<option value="' + objUen.Id_Uen + '">' + objUen.Uen_Descripcion + '</option>');
    jqelement.selectpicker('val', 0);
    jqelement.selectpicker('refresh');
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function cargarSelUEN_Ver2(jqelement, Id_Uen, Uen_Descripcion) {
    jqelement.append('<option value="' + Id_Uen + '">' + Uen_Descripcion + '</option>');
    jqelement.selectpicker('val', 0);
    jqelement.selectpicker('refresh');
}


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function round(value, decimals) {
    return Number(Math.round(value + 'e' + decimals) + 'e-' + decimals);
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function contenidoPersonalizadoAplicacion(aplicacion, indice) {
    var cantidad = $('#txtCantidad').val();
    if (cantidad == '') {
        cantidad = 0;
    }
    return '<div class="list-group-item" item> ' +
        '<table>' +
        '<tr>' +
        '<td style="width: 33%;">' +
        '<h6 class="list-group-item-heading">' +
        aplicacion.Apl_Descripcion +
        '</h6>' +
        '</td>' +
        '<td style="width: 33%;" id="tdVPT"> VPT: ' +
        numeral(round(aplicacion.Apl_Potencial / 100.0 * cantidad * _valorUnidadDimension, 2)).format('$0,0.00') +
        '</td>' +
        '<td style="width: 33%;">' +
        '<div style="display: none;">' +
        '<input type="text" name="FormaAplicaciones[' + indice + '].Id_Aplicacion" value="' + aplicacion.Id_Apl + '">' +
        '</div>' +
        '<div class="row">' +
        '<div class="col-md-1">' +
        'VPO:' +
        '</div>' +
        '<div class="col-md-6">' +
        '<input type="text" id="txtAplVPO_' + aplicacion.Id_Apl + '" style="display: none;" class="form-control" onchange="txtAplVPO$onchange(this)" name="FormaAplicaciones[' + indice + '].VPO" data-inputmask="\'alias\' : \'currency\', \'autoUnmask\' : \'true\'">' + //aplicacion.Porcentaje/100.0 +
        '</div>' +
        '</div>' +
        '</td>' +
        '<td style="text-align: right;">' +
        '<input type="checkbox" id="chkApl_' + aplicacion.Id_Apl + '" data-idapl="' + aplicacion.Id_Apl + '" onchange="chkApl_onchange(this)" chkAplicacion name="FormaAplicaciones[' + indice + '].Seleccionado"/>' +
        '</td>' +
        '</tr>' +
        '</table>' +
        '</div>';
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function chkApl_onchange(sender) {
    var chk = $(sender);
    var valoresAps = $('#selAplicacion').selectpicker('val');
    var apId = chk.data('idapl');
    if (chk.is(':checked') == true) {
        valoresAps.push(apId);
    }
    else {
        valoresAps = $.grep(valoresAps, function (value) {
            return value != apId;
        });
    }
    $('#selAplicacion').selectpicker('val', valoresAps);
    $('#selAplicacion').selectpicker('refresh');
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function crearProyectoYContinuar() {
    $('#dvModalNuevoProyecto #selUEN').prop('disabled', false);
    $('#dvModalNuevoProyecto #selSegmento').prop('disabled', false);
    $(this).prop('disabled', true);
    $('#imgDvModalNuevoProyectoEnProgreso').fadeIn();
    $.ajax({
        url: _ApplicationUrl + '/api/CrmProyecto',
        type: 'POST',
        cache: false,
        data: $('#frmDvModalNuevoProyecto').serialize(),
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(crearProyectoYContinuar, this);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        actualizarAplicacionesVPO(response.Id_Op, function () {
            $('#toastSuccess #toastSuccessMessage').html('El proyecto ha sido creado con éxito. Redirigiendo...');
            $('#toastSuccess').fadeIn();
            setTimeout(function () {
                $('#toastSuccess').fadeOut();
            }, 3000);
            $('#dvModalNuevoProyecto').modal('hide');
            window.location.href = 'Proyectos.aspx?Id_Cliente=' + response.Cliente + '&Id_Op=' + response.Id_Op;
        }, function (jqXHR, textStatus, error) {
            switch (jqXHR.status) {
                case 401:
                    alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                    break;
                default:
                    $('#toastDanger #toastDangerMessage').html('Se presentó una complicación al guardar la información de las aplicaciones. Por favor, revise de nuevo la información y trate de guardarlas nuevamente.');
                    $('#toastDanger').fadeIn();
                    setTimeout(function () {
                        $('#toastDanger').fadeOut();
                    }, 3000);
                    break;
            }
        });
    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
            case 521:
                $('#toastWarning #toastWarningMessage').html('El proyecto ha sido creado, pero algunas aplicaciones no pudieron ser asociadas. ' + jqXHR.responseJSON.ExceptionMessage);
                $('#toastWarning').fadeIn();
                setTimeout(function () {
                    $('#toastWarning').fadeOut();
                }, 10000);
                $('#dvModalNuevoProyecto').modal('hide');
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
    }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
        $('#dvModalNuevoProyecto #selUEN').prop('disabled', true);
        $('#dvModalNuevoProyecto #selSegmento').prop('disabled', true);
        $('#dvModalNuevoProyecto #selUEN').selectpicker('refresh');
        $('#dvModalNuevoProyecto #selSegmento').selectpicker('refresh');
        $(this).prop('disabled', false);
        $('#imgDvModalNuevoProyectoEnProgreso').fadeOut();
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function dimensionElegida(idUen, idSeg, unidades) {
    $('#dvModalNuevoProyecto #hdnDim_Id_Uen').val(idUen);
    $('#dvModalNuevoProyecto #hdnDim_Id_Seg').val(idSeg);
    $('#dvModalNuevoProyecto #txtDimension').val(unidades);
    $('#dvModalDimension').modal('hide');
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function txtAplVPO$onchange(sender) {
    var objetoDatos = $(sender).data('obj');
    objetoDatos.CrmOpAp_VPO = $(sender).val();
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function txtRFCPH_onincomplete() {
    $(this).val('');
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function txtCantidad$onchange(sender) {
    var cantidad = $('#txtCantidad').val();
    if (isNaN(cantidad)) {
        cantidad = 0;
    }
    var precio = $('#dvModalNuevoProyecto #txtPrecioUnidad').val();
    if (precio == '')
        precio = 0;
    $('#dvModalNuevoProyecto #txtVPM').val(precio * cantidad);
    var elementos = $('#lstAplicacion [item]');
    $.each(elementos, function (index, item) {
        var objetoDatos = $(item).data('obj');
        $(item).find('#tdVPT').text('VPT: ' + numeral(round(objetoDatos.Apl_Potencial / 100.0 * cantidad * _valorUnidadDimension, 2)).format('$0,0.00'));
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
//
// Nombre de Empresa
//
function inicializarCampoNombreDeEmpresa(parent) {
    $('#' + parent + ' #txtNombre').autocomplete({
        source: function (request, response) {
            _buscarProspecto(parent, request, response);
        },
        open: function (event, ui) {
            _bProspectoSeleccionadoDeLista = false;
        },
        change: function (event, ui) {
            if (!_bProspectoSeleccionadoDeLista) {
                if (_peticionDeBusquedaNombreEmpresa != null) {
                    if (_peticionDeBusquedaNombreEmpresa.readystate != 4) {
                        try {
                            _peticionDeBusquedaNombreEmpresa.abort();
                        } catch (e) {
                            $('#toastDanger #toastDangerMessage').html(e.ToString());
                            $('#toastDanger').fadeIn();
                            setTimeout(function () {
                                $('#toastDanger').fadeOut();
                            }, 5000);
                        }
                        _responseObjectBusquedaNombreEmpresa = null;
                        if (_responseObjectBusquedaNombreEmpresa != null) {
                            try {
                                _responseObjectBusquedaNombreEmpresa([]);
                            } catch (e) {
                                $('#toastDanger #toastDangerMessage').html(e.ToString());
                                $('#toastDanger').fadeIn();
                                setTimeout(function () {
                                    $('#toastDanger').fadeOut();
                                }, 5000);
                            }
                            _responseObjectBusquedaNombreEmpresa = null;
                        }
                    }
                }
                //validarNombreEmpresaProspecto(parent, event.currentTarget);
            }
        },
        select: function (event, ui) {
            //hubo selección. Se procede a condicionar los casos para cliente o prospecto elegido
            //Se deben de preparar las condiciones necesarias para las acciones subsecuentes, pero definitivamente no serán peticiones que se generen de la forma contenida
            //en el diálogo "Nuevo Prospecto"
            //Al seleccionar un elemento, establecer el valor del RFC(si está disponible) en el campo RFC.
            _bProspectoSeleccionadoDeLista = true;
            event.preventDefault();
            $('#' + parent + ' #lblMensajeNombreEmpresa').show();
            $('#' + parent + ' #txtNombre').val(ui.item.label);
            _prospectoElegido = ui.item.data;
            $('#' + parent + ' #txtRFC').val(_prospectoElegido.Cte_Rfc);
            prospectoElegido(parent);
        }
    });
    $('#' + parent + ' #txtNombre').attr('autocomplete', 'on');
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\        
//dvModalNuevoProyecto
function crearProyectoDeProspectoExistente(parent) {
    //TODO: Establecer los valores de los campos de la forma de proyecto para asociar al prospecto elegido.
    //hdnId_CrmProspecto
    $('#hdnId_CrmProspecto').val(_prospectoElegido.Id_CrmProspecto);
    $('#hdnCliente').val(_prospectoElegido.Id_Cte);
    $('#dvModalNuevoProyecto').modal('show');
    $('#dvModalNuevoProspecto').modal('hide');
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function cancelarCrearProyectoDeProspectoExistente(parent) {
    $('#' + parent + ' #dvContacto').show();
    $('#' + parent + ' #dvEmail').show();
    $('#' + parent + ' #navDireccion').show();
    $('#' + parent + ' #tabDireccion').show();
    $('#' + parent + ' #btnCrearProyectoDeProspectoExistente').hide();
    $('#' + parent + ' #btnDvModalNuevoProspectoGuardar').show();
    $('#' + parent + ' #btnCerrar').show();
    $('#' + parent + ' #btnCancelarCrearProyectoDeProspectoExistente').hide();
    $('#' + parent + ' #dvAlertaProspectoExistente').hide();

    //Mostrar los mensajes que indican la existencia de un cliente con los valores para los campos RFC y nombre de empresa.
    $('#' + parent + ' #lblMensajeRFC').show();
    $('#' + parent + ' #lblMensajeNombreEmpresa').show();
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function prospectoElegido(parent) {
    $('#' + parent + ' #dvContacto').hide();
    $('#' + parent + ' #dvEmail').hide();
    $('#' + parent + ' #navDireccion').hide();
    $('#' + parent + ' #tabDireccion').hide();
    $('#' + parent + ' #btnCrearProyectoDeProspectoExistente').show();
    $('#' + parent + ' #btnDvModalNuevoProspectoGuardar').hide();
    $('#' + parent + ' #btnCerrar').hide();
    $('#' + parent + ' #btnCancelarCrearProyectoDeProspectoExistente').show();
    $('#' + parent + ' #dvAlertaProspectoExistente').show();
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function _buscarProspecto(parent, request, response) {
    _responseObjectBusquedaNombreEmpresa = response;
    if (_peticionDeBusquedaExactaNombreEmpresa != null) {
        if (_peticionDeBusquedaExactaNombreEmpresa.readystate != 4) {
            try {
                _peticionDeBusquedaExactaNombreEmpresa.abort();
            } catch (e) {
                $('#toastDanger #toastDangerMessage').html(e.ToString());
                $('#toastDanger').fadeIn();
                setTimeout(function () {
                    $('#toastDanger').fadeOut();
                }, 5000);
            }
            _peticionDeBusquedaExactaNombreEmpresa = null;
        }
    }
    var terminoDeBusqueda = $('#' + parent + ' #txtNombre').val();
    var $imgProspectoEnOperacion = $('#' + parent + ' #imgNombreEmpresaEnOperacion');
    $imgProspectoEnOperacion.show();
    var data = null;
    _peticionDeBusquedaNombreEmpresa = $.ajax({
        url: _ApplicationUrl + '/api/CrmProspecto?terminoDeBusqueda=' + terminoDeBusqueda + '&incluirClientes=true',
        cache: false,
        type: 'GET',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(_buscarProspecto, this, parent, request, response);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        data = $.map(response, function (p) {
            return { value: p.Id_Cte, label: p.Cte_NomComercial, data: p };
        });
    }).fail(function (jqXHR, textStatus, error) {
    }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
        $imgProspectoEnOperacion.hide();
        response(data);
        _responseObjectBusquedaNombreEmpresa = null;
        _peticionDeBusquedaNombreEmpresa = null;
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function validarNombreEmpresaProspecto(parent, target) {
    $('#' + parent + ' #imgNombreEmpresaEnOperacion').show();
    $('#' + parent + ' #lblMensajeNombreEmpresa').hide();
    _peticionDeBusquedaExactaNombreEmpresa = $.ajax({
        url: _ApplicationUrl + '/api/CatCliente?nombreEmpresa=' + $(target).val() + '&sinUsar=',
        type: 'GET',
        cache: false,
        contentType: 'application/json',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(validarNombreEmpresaProspecto, null, parent, target);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        if (response == null) {
            $('#' + parent + ' #lblMensajeNombreEmpresa').hide();
        } else {
            if ($.isArray(response)) {
                if (response.length > 0) {
                    $('#' + parent + ' #lblMensajeNombreEmpresa').show();
                } else {
                    $('#' + parent + ' #lblMensajeNombreEmpresa').hide();
                }
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
    }).always(function (jqXHR, textStatus, errorThrown) {
        $('#' + parent + ' #imgNombreEmpresaEnOperacion').hide();
        _peticionDeBusquedaExactaNombreEmpresa = null;
    });
}



// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function empujarAplicacionesEnForma($forma, aplicaciones) {
    $.each(aplicaciones, function (index, element) {
        var $chk = $('<input type="checkbox" name="FormaAplicaciones[' + index + '].Seleccionado" checked />');
        var $vpo = $('<input type="text" name="FormaAplicaciones[' + index + '].VPO" value="0" />');
        var $apl = $('<input type="text" name="FormaAplicaciones[' + index + '].Id_Apl" value="' + element + '" />');
        $forma.append($chk);
        $forma.append($vpo);
        $forma.append($apl);
    });
}


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function navegarAPedidoEsporadico() {
    //ENE7-2020 RFH 
    PatternflyToast.showSuccess('El proyecto ha sido creado con éxito.', 2000);

    /*
    PatternflyToast.showSuccess('El proyecto ha sido creado con éxito. Se le redireccionar&aacute; a la captaci&oacute;n de los productos', 6000);            
    setTimeout(function(){
        //window.location = '<%=ApplicationUrl %>/ProPedidoVI_Admin.aspx?nuevaVentana=s';
        window.location = _ApplicationUrl+'/ProPedidoVI_Admin.aspx?nuevaVentana=s';
    }, 8000);
    */
}

//JFCV   LEADS  MOSTRARLEADS
// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\    

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
//JFCV 10 nov 2020 Mostrar datos del prospecto
function MostrarLeads(obj) {


    var idcrmprospecto = $(obj).data('idcrmprospecto');
    var acsnomcomercial = $(obj).data('acsnomcomercial');
    var correo = $(obj).data('correo');
    var telefono = $(obj).data('telefono');
    var nombrecontacto = $(obj).data('nombrecontacto');


    console.log('IdCrmProspecto:' + idcrmprospecto);
    console.log('acsnomcomercial:' + acsnomcomercial);


    $('#ModalNuevoProspecto #txtNombre').val(acsnomcomercial);
    $('#ModalNuevoProspecto #txtContacto').val(nombrecontacto);
    $('#ModalNuevoProspecto #txtEmail').val(correo);
    $('#ModalNuevoProspecto #txtTelefono').val(telefono);
    $('#ModalNuevoProspecto #HF_IdLeads').val(idcrmprospecto);
    $('#ModalNuevoProspecto #txtIdLead').val(idcrmprospecto);

    $('#dvMenu_Leads').removeClass('active');
    $('#dvMenu_General').addClass('active');



}



// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function chbBuscarCliente_Sel(obj) {

    var IdCte_Actual = $(obj).data(cte);

    for (var i = 0; i < 250; i++) {
        var RowCte_Checked = $('#chbCte_' + i).is(":checked");

        if (RowCte_Checked) {
            var IdCte = $('#chbCte_' + i).data('id_cte');
            if (IdCte_Actual == IdCte) {

            } else {
                $('#chbCte_' + i).prop('checked', false);
            }
        }
    }
    //
}


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// ready
// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
$(document).ready(function () {

    // Hack to enable multiple modals by making sure the .modal-open class
    // is set to the <body> when there is at least one modal open left
    $('body').on('hidden.bs.modal', function () {
        if ($('.modal.in').length > 0) {
            $('body').addClass('modal-open');
        }
    });




    $('#dvModalNuevoProyecto [type="radio"]').iCheck({
        checkboxClass: 'icheckbox_square-blue',
        radioClass: 'iradio_square-blue'
    });



    $.validator.addMethod('territorioProspectoValido', function (value, element, arg) {
        return arg != value;
    });

    // 


    // 
    $('#frmDvModalEditarProspecto').validate({
        submitHandler: function () {
            crearProspecto();
        },
        rules: {
            'selTerritorioProspecto': { territorioProspectoValido: '0' }
        },
        messages: {
            'selTerritorioProspecto': { territorioProspectoValido: 'Por favor, elija un territorio' }
        }
    });

    // 




    //
    // Modal Contacto
    //
    $('#dvModalNuevoContacto').on('hidden.bs.modal', function () {
        //alert('xx');
        /*  if ($('#dvModalNuevoProspecto').hasClass('in')) {
            $('body').addClass('modal-open');
        }*/
        // 
    });



    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
    $.fn.dataTable.ext.errMode = function (settings, helpPage, message) {

        $($('#tblProspectos').DataTable().table().container()).popover({
            content: 'Ha ocurrido un error al cargar los prospectos. Haga click <a>aquí</a> para intentar nuevamente. Gracias.',
            html: true
        });
    };

    //Recargar_TblProspecto(_EntidadSesion_Id_Rik);
    //            alert('al inicializar le archivo  en donde mero es pero carga los prospectos jfcv');

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
    $.fn.dataTable.ext.errMode = function (settings, helpPage, message) {

        $($('#tblLeads').DataTable().table().container()).popover({
            content: 'Ha ocurrido un error al cargar los leads. Haga click <a>aquí</a> para intentar nuevamente. Gracias.',
            html: true
        });
    };




    $('#tblProspectosToolbar').css('padding', '2px 0');
    //$('#tblProyectos').DataTable({searching: false});
    var estadoPairs = [
        { value: 0, text: 'Análisis' },
        { value: 1, text: 'Promoción' },
        { value: 2, text: 'Negociación' },
        { value: 3, text: 'Cierre' }
    ];
    //createHorizontalSelectors(estadoPairs, '.hSelectorEstado');
    var defaultData = [
        {
            text: 'INSTITUCIONAL BASICA',
            icon: 'fa fa-industry',
            nodes: [
                {
                    text: 'MANUFACTURA',
                    icon: 'fa fa-road',
                    nodes: [
                        {
                            text: 'Presentación Key.doc',
                            icon: 'fa fa-file-word-o'
                        },
                        {
                            text: 'Página del portal de Key',
                            href: 'http://www.key.com.mx',
                            icon: 'fa fa-external-link'
                        }
                    ]
                },
                {
                    text: 'EDIFICIOS / TIENDAS DEPARTAMENTALES',
                    icon: 'fa fa-road',
                    nodes: [
                        {
                            text: 'Catálogo de productos.xlsx',
                            icon: 'fa fa-file-excel-o'
                        }
                    ]
                },
                {
                    text: 'COMPAÑIAS DE LIMPIEZA',
                    icon: 'fa fa-road'
                }
            ]
        },
        {
            text: 'INSTITUCIONAL ESPECIALIZADA',
            icon: 'fa fa-industry',
            nodes: [
                {
                    text: 'HOTELES',
                    icon: 'fa fa-road'
                },
                {
                    text: 'HOSPITALES',
                    icon: 'fa fa-road'
                },
                {
                    text: 'RESTAURANTES / COMEDORES INDUSTRIALES / COMISARIATOS / CINES',
                    icon: 'fa fa-road'
                },
                {
                    text: 'SUPERMERCADOS / AUTOSERVICIOS / FARMACIAS / TIENDAS DE CONVENIENCIA',
                    icon: 'fa fa-road'
                }
            ]
        },
        {
            text: 'INDUSTRIAL',
            icon: 'fa fa-industry',
            nodes: [
                {
                    text: 'INDUSTRIA EN GENERAL',
                    icon: 'fa fa-road'
                },
                {
                    text: 'INDUSTRIA DE TRANSPORTE',
                    icon: 'fa fa-road'
                }
            ]
        },
        {
            text: 'ALIMENTARIA',
            icon: 'fa fa-industry',
            nodes: [
                {
                    text: 'CARNICOS',
                    icon: 'fa fa-road'
                },
                {
                    text: 'POLLOS',
                    icon: 'fa fa-road'
                },
                {
                    text: 'LACTEOS',
                    icon: 'fa fa-road'
                },
                {
                    text: 'PANIFICACION',
                    icon: 'fa fa-road'
                },
                {
                    text: 'EMBOTELLADORAS',
                    icon: 'fa fa-road'
                },
                {
                    text: 'DIVERSOS',
                    icon: 'fa fa-road'
                }
            ]
        }
    ];

    $('#dvHerramientas').treeview({
        collapseIcon: "fa fa-angle-down",
        data: defaultData,
        expandIcon: "fa fa-angle-right",
        nodeIcon: "fa fa-folder",
        showBorder: false,
        enableLinks: true
    });

    $('#dvHerramientas').treeview('collapseAll', { silent: true });


    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
    $('#dvModalDimension').on('hidden.bs.modal', function () {
        if ($('#dvModalNuevoProyecto').hasClass('in')) {
            $('body').addClass('modal-open');
        }
    });




    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
    $('input[iCheck]').iCheck({
        checkboxClass: 'icheckbox_square-blue',
        radioClass: 'iradio_square-blue'
    });


    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
    $('#btnContactoNuevo').click(function (e) {
        // Inicializa la forma contacto             
        $('#hdContacto_Id_Emp').val(Id_Emp);
        $('#hdContacto_Id_Cd').val(Id_Cd);
        $('#hdContacto_Id_Cte').val(Id_Cte);
        $('#hdContacto_Id_Ter').val(Id_Ter);
        $('#hdContacto_Id_Consecutivo').val(0)

        $('#txtContactoNombre').val('');
        $('#txtContactoPuesto').val('');
        $('#txtContactoCumple').val('');
        $('#txtContactoCorreo').val('');
        $('#txtContactoDireccion1').val('');
        $('#txtContactoDireccion2').val('');
        $('#txtContactoTelNegocio').val('');
        $('#txtContactoTelCasa').val('');

        $('#ModalContactos').modal('hide');
        $('#modalContacto').modal('show');
    });

    inhabilitarSelectoresDialogoNuevoProyecto();
    //inicializarModalEditarProspecto();
    inicializarModalNuevoProspecto();
    inicializarModalEliminarProspecto();
    inicializarModalNuevoProyecto();

    $('input').inputmask();

    var options = {
        cellHeight: 80,
        verticalMargin: 10
    };
    $('.grid-stack').gridstack(options);

    inicializarCampoNombreDeEmpresa('dvModalNuevoProspecto');



    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
    $('#btnRFCCrearNuevo').click(function () {
        $('#hdnId_Cte').val(0);
        $('#hdnCrearNuevo').val(1);

        $('#tbRFCMultiples').css('display', 'none');
        $('#btnRFCCrearNuevo').css('display', 'none');

        $('#dvMenu_General #txtNombre').attr('readonly', false);
        $('#dvMenu_General #txtContacto').attr('readonly', false);
        $('#dvMenu_General #txtEmail').attr('readonly', false);
        $('#dvMenu_General #txtCalle').attr('readonly', false);
        $('#dvMenu_General #txtTelefono').attr('readonly', false);
    });

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
    $('#btnRFCCancelar').click(function () {
        $('#hdnId_Cte').val(0);
        $('#dvMenu_General #txtRFC').val('');
        $('#dvMenu_General #txtNombre').val('');
        $('#dvMenu_General #txtContacto').val('');
        $('#dvMenu_General #txtEmail').val('');
        $('#dvMenu_General #txtCalle').val('');
        $('#dvMenu_General #txtTelefono').val('');

        $('#tbRFCMultiples').css('display', 'none');
        $('#btnRFCCrearNuevo').css('display', 'none');

        $('#dvMenu_General #txtNombre').attr('readonly', false);
        $('#dvMenu_General #txtContacto').attr('readonly', false);
        $('#dvMenu_General #txtEmail').attr('readonly', false);
        $('#dvMenu_General #txtCalle').attr('readonly', false);
        $('#dvMenu_General #txtTelefono').attr('readonly', false);
    });

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
    $('#cmbRFCs').on('change', function () {
        objSelected = $('#cmbRFCs').find("option:selected");
        IdE = $('#cmbRFCs').find("option:selected").val();
        Cte = objSelected.data('id_cte');
        Nomcomercial = objSelected.data('cte_nomcomercial');
        Contacto = objSelected.data('cte_contacto');
        Email = objSelected.data('cte_email');
        Calle = objSelected.data('cte_calle');
        Telefono = objSelected.data('cte_telefono');

        //$('#dvMenu_General #icnRFCComprobado').hide();
        //$('#dvMenu_General #lblMensajeRFC').show();
        $('#hdnId_Cte').val(Cte);
        $('#dvMenu_General #hdnId_Cte').val(Cte);
        $('#dvMenu_General #txtNombre').val(Nomcomercial);
        $('#dvMenu_General #txtContacto').val(Contacto);
        $('#dvMenu_General #txtEmail').val(Email);
        $('#dvMenu_General #txtCalle').val(Calle);
        $('#dvMenu_General #txtTelefono').val(Telefono);
    });

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
    $('#btnBusquedaDeCatalogo').click(function () {
        btnNuevoProspecto();
        $('#lbBuscarCliente_RegEncontrados').text('En consulta de catálogo los datos son de solo lectura.');
        $('#tbBuscarCliente_Texto').val('');
        $('#tbBuscarCliente_Listado > tbody').empty();

    });


    //INICIO JFCV Cargar los Leads necesarios  

    // /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/
    ///cargo la información de la tabla de LEads 



    var namesOption = [];
    var sucoption = [];


    Territorios_CargarUsuarios(-1, 3, function (lst) {

        for (var i = 0; i < lst.length; i++) {
            namesOption.push({ id: lst[i].Id_U, name: lst[i].U_Nombre });
        }

        //        if (IdDefault == 0) {
        //            obj.prop("selectedIndex", 0);
        //        } else {
        //            obj.val(IdDefault);
        //        }

        //        if (CALLBACK_Success) {
        //            CALLBACK_Success();
        //            //obj.prop('disabled', false);
        //        }

    }, function () {
        //obj.prop('disabled', false);
    });


    Leads_CargarSucursales(-1, -1, function (lst) {

        for (var i = 0; i < lst.length; i++) {
            sucoption.push({ id: lst[i].Id_U, name: lst[i].U_Nombre });
        }


    }, function () {
        //obj.prop('disabled', false);
    });

    //JFCVCombos 16 feb 2021
    valorcdi = _EntidadSesion_Id_Cd;


    var table = $('#tblLeads').DataTable({
        'lengthMenu': [[100, 200, -1], [100, 200, "All"]],
        'bLengthChange': true,
        'paging': true,
        'pageLength': 10,
        'ordering': true,
        'language': {
            'url': 'http://cdn.datatables.net/plug-ins/1.10.12/i18n/Spanish.json'
        },
        'ajax': {
            'url': _ApplicationUrl + '/api/CrmLeads/?idEmp=' + _EntidadSesion_Id_Emp + '&idCd=' + _EntidadSesion_Id_Cd + '&idRik=' + _EntidadSesion_Id_Rik + '&idPantalla=' + 3,
            'dataSrc': ''
        },
        //JFCV agregar columna estatus control de cambios 2mzo2021
        "columns": [
            {
                'data': 'Acs_Contacto4',
                'render': function (data, type, full, meta) {
                    return '<h6><span class="' + full.Acs_Contacto4 + '">.</span></h6>';
                    // return '<button type="button" class="btn btn-' + full.Acs_Contacto4 + '" btn-circle"></button> ';
                    // return ' <img src="../../' + full.Acs_Contacto4 + '.png" alt="" class="rounded-circle">';
                }
            }, // Color ( color del circulo dependiendo dias vencdos)


            //en pantalla riks no muestra el estatus 
            //{
            //    'data': 'Acs_Modalidad',
            //    'render': function (data, type, full, meta) {
            //        return '<span  class="label label-' + full.Acs_Contacto6 + '">' + full.Acs_Modalidad + '</span>';
            //    }
            //},  //PresentarEstatus cancelado rik, asignado etc



            //            switch(lst[i].Acs_Estatus) {
            //            case 'B':
            //            AcsEstatus = '<span id="lbAcysEstaus_' + lst[i].Id_Acs + '" class="label label-default">Baja</span>';
            //    break;
            //           case 'C':
            //    AcsEstatus = '<span id="lbAcysEstaus_' + lst[i].Id_Acs + '" class="label label-info">Capturado</span>';
            //    break;
            //           case 'S':
            //    AcsEstatus = '<span id="lbAcysEstaus_' + lst[i].Id_Acs + '" class="label label-primary">Solicitado</span>';
            //    break;
            //           case 'A':
            //    AcsEstatus = '<span id="lbAcysEstaus_' + lst[i].Id_Acs + '" class="label label-success">Autorizado</span>';
            //    break;
            //           case 'R':
            //    AcsEstatus = '<span id="lbAcysEstaus_' + lst[i].Id_Acs + '" class="label label-warning">Rechazado</span>';
            //    break;
            //           default:
            //    $('#lbAcysEstaus').text('???');
            //}



            { "data": "Acs_Fecha" },
            //{ "data": "Acs_Contacto3" }, quitar medio contacto
            { "data": "Acs_NomComercial" }, //NombreEmpresa
            { "data": "Acs_Contacto2" },  //GiroEmpresa
            { "data": "Acs_Notas" }, //ProductoInteres

            //    epro.Acs_NomComercial = lds.NombreEmpresa;
            //epro.Id_Cte = Convert.ToInt32(lds.IdLeads);
            //epro.Id_Acs = Convert.ToInt32(lds.IdLeads);
            //epro.Acs_Notas = lds.ProductoInteres;
            //epro.Acs_Contacto2 = lds.GiroEmpresa;
            //epro.Id_Cd = lds.Id_Cd;
            //epro.Acs_Fecha = lds.FechaAlta;
            //epro.Acs_Contacto3 = lds.MedioComunicacion;
            //epro.Acs_Contacto = lds.NombreContacto;
            //epro.Acs_email = lds.Correo;
            //epro.Acs_Puesto = lds.Telefono;
            //epro.Acs_RecOtroDesc = lds.Comentarios;
            ////JFCV Control de cambio colores 2 mzo 2021
            //epro.Acs_Estatus = Convert.ToString(lds.Activo);
            //epro.Acs_Modalidad = lds.PresentarEstatus;
            //epro.Acs_RecOtroDesc = lds.ColorEstatus;
            //epro.Acs_Contacto4 = lds.Color;
            //epro.Acs_Contacto5 = lds.HistorialLeads; 

            //combo de representante
            //{
            //    data: null,
            //    render: function (data, type, row) {
            //        var $select = $('<select id="team_ddl" class="team_ddl"></select>',
            //            {
            //                id: row.id,
            //                value: row.name
            //            });



            //        $.each(namesOption, function (k, v) {
            //            if (1 == 1) {
            //                var $option = $("<option></option>",
            //                    {
            //                        text: v.name,
            //                        value: v.id
            //                    });

            //                //if selected_id = id then this is the selected value
            //                console.log('row id rik');
            //                console.log(row.Id_Rik);
            //                console.log(v.id);

            //                if (row.Id_Rik != null) {

            //                    if (row.Id_Rik == v.id) {  //use == instead of ===

            //                        //JFCVcombos 16feb2021
            //                        if (row.Id_Rik == -1) {
            //                            $("#sucursal_ddl").prop('disabled', false);
            //                        } else {
            //                            $("#sucursal_ddl").prop('disabled', true);
            //                        }
            //                        //JFCVcombos 16feb2021  fin
            //                        $option.attr("selected", "selected");

            //                    }
            //                }
            //                $select.append($option);
            //            }
            //        });
            //        return $select.prop("outerHTML");

            //    }
            //},
            //combo de sucursal 
            //{
            //    data: null,
            //    render: function (data, type, row) {
            //        var $select = $('<select autocomplete="on" id="sucursal_ddl" class="sucursal_ddl"></select>',
            //            {
            //                id: row.id,
            //                value: row.name
            //            });
            //        $.each(sucoption, function (k, v) {
            //            if (1 == 1) {
            //                var $option = $("<option></option>",
            //                    {
            //                        text: v.name,
            //                        value: v.id
            //                    });
            //                console.log('valor de Id_Cd en el combo');
            //                console.log(row.Id_Cd);
            //                if (row.Id_Cd == v.id) {  //use == instead of ===

            //                    //JFCVcombos 16feb2021
            //                    if (row.Id_Cd == -1) {
            //                        $("#team_ddl").prop('disabled', false);
            //                    } else {
            //                        $("#team_ddl").prop('disabled', true);
            //                    }
            //                    //JFCVcombos 16feb2021  fin

            //                    $option.attr("selected", "selected");

            //                    if (row.Id_Cd == valorcdi) {
            //                        console.log('Eligio sucursal local ');
            //                        console.log(row.Id_Cd);
            //                        console.log(valorcdi);
            //                        $("#team_ddl").prop('disabled', false);
            //                    }


            //                }
            //                $select.append($option);
            //            }
            //        });
            //        return $select.prop("outerHTML");

            //    }
            //},

            {
                "className": 'details-control',
                "orderable": false,
                "data": null,
                "defaultContent": ''
            },
            //{
            //    'data': 'Id_Acs',
            //    'render': function (data, type, full, meta) {
            //        return '<button type="button" class="btn btn-primary-outline" style="background-color:transparent; box-shadow: none; font-size: 25px;"  onclick="ActualizarLeads(this)" data-terid="' + full.Id_Acs + '" data-nombrecontacto="' + full.Acs_Contacto3 + '" ' + 'data-rik="' + full.Id_Rik + '" ' + 'data-idcd="' + full.Id_Cd + '" class="btn btn-primary">' +
            //            '<i class="fa fa-check" aria-hidden="true" ></i>' + '</button>';
            //    }
            //}

        ],
        initComplete: function () {

            $('#tblLeads tbody').on('change', 'select.team_ddl', function () {
                //get selected value
                var changed = $(this).find(":selected").val();
                console.log(changed);

                var row = table.row($(this).closest('tr'));
                var data = row.data();
                //            //Actualizo la variable con lo que tengo en el combo
                data.Id_Rik = changed;
                //alert(data.Id_Rik);
                ////JFCVcombos
                //document.getElementById("sucursal_ddl").disabled = true;
                //$("#sucursal_ddl").prop('disabled', true);
                row.invalidate().draw(false);
            });

            $('#tblLeads tbody').on('change', 'select.sucursal_ddl', function () {

                var changed = $(this).find(":selected").val();

                var row = table.row($(this).closest('tr'));
                var data = row.data();

                data.Id_Cd = changed;

                row.invalidate().draw(false);
            });


        }
        ,
        "order": [[1, 'desc']]

    });




    //        '<button type="button" onclick="btnRetirarTerritorio$click(this)" data-terid="' + element.Id_Ter + '" class="btn btn-primary">' +
    //                    '<i class="fa fa-times"></i>'+
    //                '</button>' +

    _tablaProspectos = table;

    // Add event listener for opening and closing details
    $('#tblLeads tbody').on('click', 'td.details-control', function () {
        var tr = $(this).closest('tr');
        var row = table.row(tr);
        //            _renglonDelLeadCancelar=row;
        if (row.child.isShown()) {
            // This row is already open - close it
            row.child.hide();
            tr.removeClass('shown');
        }
        else {
            // Open this row
            row.child(format(row.data())).show();
            tr.addClass('shown');
        }
    });

    /* Formatting function for row details - modify as you need */
    function format(d) {
        // `d` is the original data object for the roW     
        return '<table cellpadding="5" cellspacing="0" border="0" style="padding-left:50px;">' +
            '<tr>' +
            '<th  class="col-md-8">Nombre del Contacto:</th>' +
            '<th  class="col-md-2">Email: </th>' +
            '<th  class="col-md-2">Teléfono: </th>' +

            '</tr>' +
            '<tr>' +
            '<td  class="col-md-8">' + d.Acs_Contacto + '</td>' +

            '<td  class="col-md-2">' + d.Acs_email + '</td>' +
            '<td  class="col-md-2">' + d.Acs_Puesto + '</td>' +
            '</tr>' +

            '</table>' +
            '<table>' +


            '<tr>' +
            '<th class="col-md-10">Comentarios: </th>' +
            '<td  class="col-md-1"></td>' +
            '<td  class="col-md-1"></td>' +

            '</tr>' +
            '<tr>' +
            '<td class="col-md-10">' + d.Acs_RecOtroDesc + '</td>' +
            '<td  class="col-md-1"></td>' +
            '<td  class="col-md-1"></td>' +
            '</tr>' +
            '</table>';
        //                '<table>' +
        //                '<tr>' +
        //                        '<td class="col-md-3">' + 
        //                        '<button type="button" class="btn btn-danger" id="btncancelarLeads" ' +
        //                         ' onclick="javascript:inicializarModalCancelarLead(' + d.Id_Acs + ')" >' +
        //                          'Cancelar proyecto' +
        //                         ' </button>    ' +
        //                        ' </td>' +
        //                         ' <td class="col-md-3">' +
        //                       '<button type="button" class="btn btn-success" id="btnDesarrollar" ' +
        //                       'data-idcrmprospecto="' + d.Id_Acs + '" '+
        //                                        'data-nombrecontacto="' + d.Acs_Contacto + '" '+
        //                                         'data-correo="' + d.Acs_email + '" '+
        //                                          'data-telefono="' + d.Acs_Puesto + '" '+
        //                                           'data-acsnomcomercial="' + d.Acs_NomComercial + '" '+
        //                                        'data-rowidx="' + d.Id_Acs + '" '+

        //                        ' onclick="javascript:MostrarLeads(this)">' +
        //                         'Desarrollar Lead  </button>  </td> </tr>' +

        //            '</table>';

    }

    //FIN JFCV Cargar los Leads necesarios 


});