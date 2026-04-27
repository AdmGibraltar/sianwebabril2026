
// JUN02-2020 RFH

var _lastSelectedNode = null;
var _proyectoSeleccionado = null;
var _$campoDescripcionActual = null;

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// Productos - Retirar
function $retirarProducto($C, idCte, idOp, idPrd) {

    retirarProductoSvc(idCte, idOp, idPrd,
        function (response, textStatus, jqXHR) {
            var $lstProductos = $C.find('#lstProductos');
            var elem = $lstProductos.find('#lstElem_' + idPrd);
            elem.remove();
            var totalProductos = $C.data('_modeloListado_');
            totalProductos.i = totalProductos.i - 1;
            if (totalProductos.i == 0) {
                //$C.find('#contenidoSeccionProductos').fadeOut();
                $C.find('#productosBlankSlate').fadeIn();
            }
        },
        function (jqXHR, textStatus, error) {
            switch (jqXHR.status) {
                case 401:
                    alert('LA sesion ha expirado(4036). Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                    break;
                default:
                    PatternflyToast.showError(jqXHR.responseJSON.Message, 10000);
                    break;
            }
        },
        function (jqXHROrData, textStatus, errorOrJQXHR) {
        },
        {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy($retirarProducto, null, $C, idCte, idOp, idPrd);
            }
        }
    );
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
/*
function aceptarEditarCantidad(idPrd) {
    var dvCantidadDisplay = $('#dvCantidadDisplay_' + idPrd);
    var dvCantidadEdit = $('#dvCantidadEdit_' + idPrd);
    var dvDilucionDisplay = $('#dvDilucionDisplay_' + idPrd);
    var dvDilucionEdit = $('#dvDilucionEdit_' + idPrd);

    var dvCantidadDisplayValue = dvCantidadDisplay.find('#dvCantidadDisplayValue');
    var dvDilucionDisplayValue = dvDilucionDisplay.find('#dvDilucionDisplayValue');
    var txtCantidadEdit = dvCantidadEdit.find('#txtCantidad');
    var txtDilucionEdit = dvDilucionEdit.find('#txtDilucion');

    var $lstElem = $('#lstProductos #lstElem_' + idPrd);
    var dataObject = $lstElem.data('objetodatos');
    var objectCopy = jQuery.extend(true, {}, dataObject);
    objectCopy.COP_Cantidad = txtCantidadEdit.val();
    objectCopy.COP_Dilucion = txtDilucionEdit.val();

    $.ajax({
        url: _ApplicationUrl + '/api/CrmOportunidadesProductos/CrmOportunidadesProductos_Update', //?idCte=' + idCte + '&idOp=' + idOp + '&idPrd=' + idPrd,
        type: 'PUT',
        cache: false,
        data: JSON.stringify(objectCopy),
        contentType: 'application/json',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(aceptarEditarCantidad, this, idPrd);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        dvCantidadDisplay.show();
        dvCantidadEdit.hide();
        dvCantidadDisplayValue.text(txtCantidadEdit.val());
        dvDilucionDisplay.show();
        dvDilucionEdit.hide();
        dvDilucionDisplayValue.text(txtDilucionEdit.val());
    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado(3857). Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
            default:
                $('#toastDanger #toastDangerMessage').html(jqXHR.responseJSON.Message);
                $('#toastDanger').fadeIn();
                setTimeout(function () {
                    $('#toastDanger').fadeOut();
                }, 3000);
                break;
        }
    }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
    });
}
*/


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// Crea Renglon de Producto
// Genera una cadena representa un producto.     
// JUN06-2020

function $crearElementoDeListadoDeProductos_Obsoleto(idContenedor, E) {
    console.log('function $crearElementoDeListadoDeProductos_Obsoleto');

    var Producto_ReadOnly = false;
    var ESTATUS = $('#ESTATUS_' + E.Id_Op).val();
    var VAP_ESTATUS = $('#VAP_ESTATUS_' + E.Id_Op).val();
    var VAP_ESTATUS2 = $('#VAP_ESTATUS2_' + E.Id_Op).val();
    if (VAP_ESTATUS == "A" && VAP_ESTATUS2 == 4) {
        Producto_ReadOnly = true;
    }
    if (VAP_ESTATUS == "C" && VAP_ESTATUS2 == 4) {
        Producto_ReadOnly = true;
    }
    if (VAP_ESTATUS == "C" && VAP_ESTATUS2 == 2) {
        // En Autorizacion 
        Producto_ReadOnly = true;
    }
    if (ESTATUS == 5) {
        // Cancelado
        Producto_ReadOnly = true;
    }


    var editCommandClass = '';
    var editCommandEditAction = "javascript:$editarCantidad($('#" + idContenedor + "\')," + E.Id_Prd + ")";
    var editCommandRemoveAction = "javascript:$retirarProducto($('#" + idContenedor + "\')," + E.Id_Cte + "," + E.Id_Op + "," + E.Id_Prd + ")";
    if (_proyectoSeleccionado.EnValuacion != null) {
        // 19 Sep 2018 RFH
        // Siempre permitira la edicion a Excepcion de en Autorizacion 
        // 
        if (Producto_ReadOnly) {
            //No es editable
            //DropDown_Ellipsis = '<div class="dropdown pull-right dropdown-kebab-pf"></div>' ;
            DropDown_Ellipsis = '<div class="dropdown pull-right dropdown-kebab-pf">' +
                '<button class="btn btn-link dropdown-toggle" id="dropdownKebabRight3" aria-expanded="true" aria-haspopup="true" type="button" data-toggle="dropdown">' +
                '<span class="fa fa-ellipsis-v"></span>' +
                '</button>' +
                '<ul class="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownKebabRight3">' +
                '<li data-idrnd="4654654"> Edición no permitida.</li>' +
                '</ul>' +
                '</div>';
        } else {
            //if (_proyectoSeleccionado.EnValuacion == true || _proyectoSeleccionado.EnValuacion == 1) {
            //  editCommandClass = 'disabled-link';
            //} else {                
            //Es editable 
            DropDown_Ellipsis = '<div class="dropdown pull-right dropdown-kebab-pf">' +
                '<button class="btn btn-link dropdown-toggle" id="dropdownKebabRight3" aria-expanded="true" aria-haspopup="true" type="button" data-toggle="dropdown">' +
                '<span class="fa fa-ellipsis-v"></span>' +
                '</button>' +
                '<ul class="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownKebabRight3">' +
                '<li><a class="' + editCommandClass + '" href="' + editCommandEditAction + '">Editar</a></li>' +
                '<li><a class="' + editCommandClass + '" href="' + editCommandRemoveAction + '">Retirar</a></li>' +
                '</ul>' +
                '</div>';
        }
    }


    // 19 Sep 2018 RFH
    // Siempre permitira la edicion a Excepcion de en Autorizacion 
    //
    if (Producto_ReadOnly) {
        //DropDown_Ellipsis = '<div class="dropdown pull-right dropdown-kebab-pf"></div>' ;
        DropDown_Ellipsis = '<div class="dropdown pull-right dropdown-kebab-pf">' +
            '<button class="btn btn-link dropdown-toggle" id="dropdownKebabRight3" aria-expanded="true" aria-haspopup="true" type="button" data-toggle="dropdown">' +
            '<span class="fa fa-ellipsis-v"></span>' +
            '</button>' +
            '<ul class="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownKebabRight3">' +
            '<li>Edición no permitida.</li>' +
            '</ul>' +
            '</div>';
    } else {
        //if (_proyectoSeleccionado.Estatus===1 || _proyectoSeleccionado.Estatus===2) {
        DropDown_Ellipsis = '<div class="dropdown pull-right dropdown-kebab-pf">' +
            '<button class="btn btn-link dropdown-toggle" id="dropdownKebabRight3" aria-expanded="true" aria-haspopup="true" type="button" data-toggle="dropdown">' +
            '<span class="fa fa-ellipsis-v"></span>' +
            '</button>' +
            '<ul class="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownKebabRight3">' +
            '<li><a class="' + editCommandClass + '" href="' + editCommandEditAction + '">Editar</a></li>' +
            '<li><a class="' + editCommandClass + '" href="' + editCommandRemoveAction + '">Retirar</a></li>' +
            '</ul>' +
            '</div>';
    }
    var textoNodoDilucion = '<td>' +
        '<div id="dvDilucionDisplay_' + E.Id_Prd + '" >' +
        '<table>' +
        '<tr>' +
        '<td>Dilución</td>' +
        '<td style="text-align: right; width: 60px">' +
        '<div id="dvDilucionDisplayValue">' +
        (E.COP_Dilucion != null ? E.COP_Dilucion : '') +
        '</div>' +
        '</td>' +
        '</tr>' +
        '</table>' +
        '</div>' +
        '<div id="dvDilucionEdit_' + E.Id_Prd + '" style="display:none;">' +
        '<table>' +
        '<tr>' +
        '<td>Dilución 1:</td>' +
        '<td>' +
        '<input type="text" id="txtDilucion" value="' + (E.COP_Dilucion != null ? E.COP_Dilucion : '') + '" style="text-align: right; width: 60px;">' +
        '</td>' +
        '<td>' +
        '<a id="aAceptarDilucion"><i class="fa fa-check" aria-hidden="true"></i></a>' +
        '</td>' +
        '<td>' +
        '<a id="aCancelarDilucion"><i class="fa fa-times" aria-hidden="true"></i></a>' +
        '</td>' +
        '</tr>' +
        '</table>' +
        '</div>' +
        '<td>';
    var n = $('<div class="list-group-item list-view-pf-stacked list-view-pf-top-align row_prd_item" id="lstElem_' + E.Id_Prd + '" elementoDeLista>' +
        '<div class="list-view-pf-checkbox"><input type="checkbox" stype="margin-bottom:5px;"></div>' +
        '<div class="list-view-pf-actions">' +
        DropDown_Ellipsis +
        /*'<div class="dropdown pull-right dropdown-kebab-pf">' +
            '<button class="btn btn-link dropdown-toggle" id="dropdownKebabRight3" aria-expanded="true" aria-haspopup="true" type="button" data-toggle="dropdown">' +
                '<span class="fa fa-ellipsis-v"></span>' +
            '</button>' +
            '<ul class="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownKebabRight3">' +
                '<li><a class="' + editCommandClass + '" id="aEditCommandAction">Editar</a></li>' +
                '<li><a class="' + editCommandClass + '" href="' + editCommandRemoveAction +'" id="aRemoveCommandAction">Retirar</a></li>' +
            '</ul>' +
        '</div>' +*/
        '</div>' +
        '<div class="list-view-pf-main-info PADDING_TB_10">' +
        '<div class="list-view-pf-body">' +
        '<div class="list-view-pf-description">' +
        '<div class="list-group-item-heading">' +
        // + E.Id_Prd + '&nbsp;' + E.Nombre + '</div>' +
        //'<div class="list-group-item-text">' + E.Ruta + '</div>' + 
        '<label data-imagen="' + E.ImagenProductoAltaRes + '" onclick="ProductoExaminarImagen(this)">' + E.Id_Prd + '&nbsp;' + E.Nombre + '</label>' +
        '</div>' +
        '<div class="list-group-item-text"></div>' +
        '</div>' +
        '<div class="list-view-pf-additional-info">' +
        '<table>' +
        '<tr>' +
        '<td>' +
        '<div id="dvCantidadDisplay_' + E.Id_Prd + '">' +
        //cantidad
        '<table>' +
        '<tr>' +
        '<td>Cantidad de Producto</td>' +
        '<td style="text-align: right; width: 60px">' +
        '<div id="dvCantidadDisplayValue">' +
        E.COP_Cantidad +
        '</div>' +
        '</td>' +
        '<td> &nbsp;' + 'piezas' /*E.ProductoSerializable.Prd_UniNe*/ + '</td>' +
        '</tr>' +
        '</table>' +
        '</div>' +
        '<div id="dvCantidadEdit_' + E.Id_Prd + '" style="display:none;">' +
        '<table style="width: 160px;">' +
        '<tr>' +
        '<td>Cantidad:</td>' +
        '<td>' +
        '<input type="text" id="txtCantidad" value="' + E.COP_Cantidad + '" style="text-align: right; width: 60px;">' +
        '</td>' +
        '<td>' +
        '<a id="aAceptarEditarCantidad" style="width:30px;" >' +
        '<i class="fa fa-check" aria-hidden="true"></i>' +
        '</a>' +
        '</td>' +
        '<td>' +
        '<a id="aCancelarEditarCantidad"><i class="fa fa-times" aria-hidden="true"></i></a>' +
        '</td>' +
        '</tr>' +
        '</table>' +
        '</div>' +
        '</td>' +
        '</tr>' +
        '<tr>' +
        (E.COP_EsQuimico == true ? textoNodoDilucion : '') +
        '</tr>' +
        '</table>' +
        '</div>' +
        '</div>' +
        '</div>' +
        '</div>');
    if (_proyectoSeleccionado.EnValuacion != null) {
        if (_proyectoSeleccionado.EnValuacion == true || _proyectoSeleccionado.EnValuacion == 1) {
            n.find('#aEditCommandAction').attr('href', '#');
            //n.find('#aRemoveCommandAction').attr('href', '#');
        } else {
            n.find('#aEditCommandAction').click(function () {
                $editarCantidad(n, E.Id_Prd);
            });
            //n.find('#aRemoveCommandAction');
        }
    }
    n.find('#aAceptarEditarCantidad').click(function () {
        aceptarEditarCantidad(n, E.Id_Prd);
    });
    n.find('#aCancelarEditarCantidad').click(function () {
        $cancelarEditarCantidad(n, E.Id_Prd);
    });
    n.find('#aAceptarDilucion').click(function () {
        aceptarEditarCantidad(n, E.Id_Prd);
    });
    n.find('#aCancelarDilucion').click(function () {
        $cancelarEditarCantidad(n, E.Id_Prd);
    });
    return n;
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function _cargarProductosDeProyecto(oportunidadSeleccionada, clienteDeOportunidad, onSuccess) {
    //actualizarComandosValuacion(_proyectoSeleccionado);
    console.log(_proyectoSeleccionado);
    cargarInfoSeccionGeneral(_proyectoSeleccionado);
    _oportunidadSeleccionada = oportunidadSeleccionada;
    _clienteDeOportunidad = clienteDeOportunidad;
    limpiarListadoDeProductos();
    $.ajax({
        url: '<%=ApplicationUrl %>' + '/api/CrmOportunidadesProductos?Id_CrmOportunidad=' + oportunidadSeleccionada + '&Id_Cte=' + clienteDeOportunidad,
        cache: false,
        type: 'GET',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(cargarProductosDeProyecto, this, oportunidadSeleccionada, clienteDeOportunidad, onSuccess);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        var $lstProductos = $('#lstProductos');
        $.each(response, function (index, element) {
            var n = crearElementoDeListadoDeProductos(element);
            $lstProductos.append(n);
            var lstElem = $lstProductos.find('#lstElem_' + element.Id_Prd);
            lstElem.data('objetodatos', element);
        });

        //Se asigna el identificador del proyecto y del cliente a los campos de la forma para agregar productos
        $('#hdnAgregarProducto_Id_Op').val(oportunidadSeleccionada);
        $('#hdnAgregarProducto_Id_Cte').val(clienteDeOportunidad);

        if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
            onSuccess();
        }

    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
            default:
                $('#toastDanger #toastDangerMessage').html(jqXHR.responseJSON.Message);
                $('#toastDanger').fadeIn();
                setTimeout(function () {
                    $('#toastDanger').fadeOut();
                }, 3000);
                break;
        }
    }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {

    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function cargarProductosDeProyecto(oportunidadSeleccionada, clienteDeOportunidad, datos) {

    ///     console.log('function cargarProductosDeProyecto');

    _proyectoSeleccionado = datos;

    //Se evalua si se debe de ocultar el área de productos de aplicación

    if (_proyectoSeleccionado.Area == -1) {
        $('#contenedorProductosDeAplicacion').hide();
    } else {
        $('#contenedorProductosDeAplicacion').show();
    }
    //  console.log('2');
    //  console.log(datos);

    cargarInfoSeccionGeneral(datos);
    _oportunidadSeleccionada = oportunidadSeleccionada;
    _clienteDeOportunidad = clienteDeOportunidad;

    //            obtenerRutaDeOferta(clienteDeOportunidad, oportunidadSeleccionada);
    $('#dvDetalles:hidden').slideDown();
    $('#imgCargandoProductos').fadeIn();

    cargarListadoDeProductos(oportunidadSeleccionada, clienteDeOportunidad, _proyectoSeleccionado);

    //            $.ajax({
    //                url: '<%=ApplicationUrl %>' + '/api/CrmOportunidadesProductos?Id_CrmOportunidad=' + oportunidadSeleccionada + '&Id_Cte=' + clienteDeOportunidad,
    //                cache: false,
    //                type: 'GET',
    //                statusCode: {
    //                    401: function (jqXHR, textStatus, errorThrown) {
    //                        $('#dvDialogoInicioSesion').modal();
    //                        _onLoginSuccessful = $.proxy(cargarProductosDeProyecto, this, oportunidadSeleccionada, clienteDeOportunidad, datos);
    //                    }
    //                }
    //            }).done(function (response, textStatus, jqXHR) {
    //                _totalProductos=response.length;
    //                $('#txtProductoCantidad').attr('disabled', true);
    //                $('#btnAgregarProducto').attr('disabled', true);
    //                if(response.length>0){
    //                    $('#productosBlankSlate').hide();
    //                    $('#contenidoSeccionProductos').show();
    //                    var $lstProductos = $('#lstProductos');
    //                    $.each(response, function (index, element) {
    //                        var n = crearElementoDeListadoDeProductos(element);
    //                        $lstProductos.append(n);
    //                        var lstElem=$lstProductos.find('#lstElem_' + element.Id_Prd);
    //                        lstElem.data('objetodatos', element);
    //                    });
    //                }else{
    //                    $('#contenidoSeccionProductos').hide();
    //                    $('#productosBlankSlate').show();
    //                }
    //                //Se asigna el identificador del proyecto y del cliente a los campos de la forma para agregar productos
    //                $('#hdnAgregarProducto_Id_Op').val(oportunidadSeleccionada);
    //                $('#hdnAgregarProducto_Id_Cte').val(clienteDeOportunidad);
    //                
    //            }).fail(function (jqXHR, textStatus, error) {
    //                switch (jqXHR.status) {
    //                    case 401:
    //                        alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
    //                        break;
    //                    default:
    //                        $('#toastDanger #toastDangerMessage').html(jqXHR.responseJSON.Message);
    //                        $('#toastDanger').fadeIn();
    //                        setTimeout(function () {
    //                            $('#toastDanger').fadeOut();
    //                        }, 3000);
    //                        break;
    //                }
    //            }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
    //                $('#imgCargandoProductos').fadeOut();

    //            });
}


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// Productos
function inicializarApartadoProductos($C, dataItems) {

    ///     console.log('function inicializarApartadoProductos');

    ///<summary>Inicializa el listado de productos asociados a un proyecto</summary>
    ///<param name="$contendor" type="jqNode">Componente contenedor de la sección a inicializar</param>
    ///<param name="dataItems" type="CrmOportunidadesProductos[]">Conjunto de productos</param>
    //Se limpia el listado
    $limpiarCamposBusquedaProducto($C);
    $limpiarListadoDeProductos($C);
    //$C.find('#txtProductoCantidad').attr('disabled', true);
    //$C.find('#btnAgregarProducto').attr('disabled', true);
    if (dataItems.length > 0) {
        //$C.find('#productosBlankSlate').hide(); RFH               
        $C.find('#contenidoSeccionProductos').show();
        var $lstProductos = $C.find('#lstProductos');
        $.each(dataItems, function (index, element) {
            var n = $crearElementoDeListadoDeProductos($C[0].id, element);
            $lstProductos.append(n);
            var lstElem = $lstProductos.find('#lstElem_' + element.Id_Prd);
            lstElem.data('objetodatos', element);
        });
    } else {
        //$C.find('#contenidoSeccionProductos').hide();
        $C.find('#productosBlankSlate').show();
    }
    //Se asigna el identificador del proyecto y del cliente a los campos de la forma para agregar productos
    $C.find('#hdnAgregarProducto_Id_Op').val(_oportunidadSeleccionada);
    $C.find('#hdnAgregarProducto_Id_Cte').val(_clienteDeOportunidad);
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\        
// Productos de proyecto (2)
// Se pasa a CRM_PRoductos.JS

function cargarListadoDeProductos(idOp, idCte, datosProyecto) {
    var ESTATUS = $('#ESTATUS_' + idOp).val();
    var VAP_ESTATUS = $('#VAP_ESTATUS_' + idOp).val();
    var VAP_ESTATUS2 = $('#VAP_ESTATUS2_' + idOp).val();

    _cargarProductos(idOp, idCte, function (response, status, jqXHR) {
        var Datos = response.Datos;

        var productosDeAplicacion = {};
        var otrosProductos = Datos;

        const countInactivos = otrosProductos.filter(producto => producto.Prd_Activo == 2)?.length;
        if (countInactivos > 0) {
            alert("Dentro de tu proyecto se encuentra SKUs catalogados como inactivos, válida y aplica el cambio/actualización correspondiente");
        }
        otrosProductos.sort((a, b) => (b.Prd_Activo === 2 ? 1 : 0) - (a.Prd_Activo === 2 ? 1 : 0));

        //PRODUCTOS DE APLICACION
        /*
        var productosDeAplicacion=$.grep(response, function(element, index){
            return  element.Id_Apl==datosProyecto.Id_Apl 
                    && element.Id_Area==datosProyecto.Id_Area 
                    && element.Id_Sol==datosProyecto.Id_Sol 
                    && element.Id_Seg==datosProyecto.Id_Seg 
                    && element.Id_Uen==datosProyecto.IdUen;
        });
        //OTROS PRODUCTOS 
        var otrosProductos=$.grep(response, function(element, index){
            var elementosEncontrados=$.grep(productosDeAplicacion, function(elementProductosDeAplicacion, index){
                return element==elementProductosDeAplicacion;
            });
            return elementosEncontrados.length==0;
        });
        */

        //Se inicializan los listados de productos de la aplicación y otros productos.
        var $c_PA = $('#contendorProductos');
        $c_PA.data('_modeloListado_', _totalProductos);
        var $c_LO = $('#contenedorOtrosProductos');
        $c_LO.data('_modeloListado_', _totalOtrosProductos);
        _totalProductos.i = productosDeAplicacion.length;
        _totalOtrosProductos.i = otrosProductos.length;

        inicializarApartadoProductos($c_PA, productosDeAplicacion);

        inicializarApartadoProductos($c_LO, otrosProductos);

        $obtenerRutaDeOferta($c_PA, _clienteDeOportunidad, _oportunidadSeleccionada);

        $obtenerRutaDeOferta($c_LO, _clienteDeOportunidad, _oportunidadSeleccionada);

    }, function (jqXHR, status, error) {
    }, function (jqXHR, status, error) {
        $('#imgCargandoProductos').fadeOut();
    },
        {}
    );
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\        
// Carga Lustado de Productos de proyecto
//

function _cargarProductos(idOp, idCte, onSuccess, onFail, always, statusCodeHandlers) {

    $.ajax({
        url: _ApplicationUrl + '/api/CrmOportunidadesProductos/Get_ListadoProductosByProyecto?' +
            'Id_Op=' + idOp + '&Id_Cte=' + idCte,
        cache: false,
        type: 'GET',
        /* statusCode: {
             401: function (jqXHR, textStatus, errorThrown) {
                 $('#dvDialogoInicioSesion').modal();
                 _onLoginSuccessful = $.proxy(cargarProductosDeProyecto, this, oportunidadSeleccionada, clienteDeOportunidad, onSuccess);
             }
         } */
        //statusCodeHandlers RFH 30 05 2018 
    }).done(function (response, textStatus, jqXHR) {
        var Datos = response.Datos;
        var Estado = response.Estado;

        if (Estado == 1) {
            onSuccess(response, textStatus, jqXHR);
        }

        /*
        if (typeof (onSuccess) != undefined && typeof (onSuccess) != 'undefined') {
            onSuccess(response, textStatus, jqXHR);
        }
        */
    }).fail(function (jqXHR, textStatus, error) {
        /*if (typeof (onFail) != undefined && typeof (onFail) != 'undefined') {
        onFail(jqXHR, textStatus, error);
        }*/
        switch (jqXHR.status) {
            case 401:
                alert('La sesion ha expirado (32) Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                $('#dvDialogoInicioSesion').modal();
                break;
            default:
                var Messge = '';
                if (typeof (jqXHR.responseJSON.Message) == 'undefined') {
                    Messge = 'No se puede mostrar el error.';
                } else {
                    Messge = jqXHR.responseJSON.Message;
                }

                $('#toastDanger #toastDangerMessage').html(Messge);
                $('#toastDanger').fadeIn();
                setTimeout(function () {
                    $('#toastDanger').fadeOut();
                }, 3000);
                break;
        }

    }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
        if (typeof (always) != undefined && typeof (always) != 'undefined') {
            always();
        }
    });
}

// LANZA MODALE DE IMAGEN 

function ProductoExaminarImagen(obj) {
    var ImgFile = $(obj).data('imagen');
    var ProdName = $(obj).data('producto');

    var RutaCarpetasImagen = "http://40.124.41.101/CatalogoUnico_Pruebas/Procesos/Archivos/337/";

    if (ImgFile == '') {
        alertify.alert('La imagen del producto aun no esta lista.');
    } else {

        $('#ImagenPrd_Nombre').text(ProdName);
        $('#ImagenPrd_Img').prop('src', RutaCarpetasImagen + ImgFile);
        $('#Modal_ImageDeProducto').modal();

    }
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// Crea Renglon de Producto
// Genera una cadena representa un producto.     
function $crearElementoDeListadoDeProductos(idContenedor, E) {

    ///     console.log('function $crearElementoDeListadoDeProductos');
    /// console.log(E);
    var colorInactivo = '';
    if (E.Prd_Activo == 2) {
        colorInactivo = '#eaaeae';
    }

    var Producto_ReadOnly = false;
    var ESTATUS = $('#ESTATUS_' + E.Id_Op).val();
    var VAP_ESTATUS = $('#VAP_ESTATUS_' + E.Id_Op).val();
    var VAP_ESTATUS2 = $('#VAP_ESTATUS2_' + E.Id_Op).val();
    if (VAP_ESTATUS == "A" && VAP_ESTATUS2 == 4) {
        Producto_ReadOnly = true;
    }
    if (VAP_ESTATUS == "C" && VAP_ESTATUS2 == 4) {
        Producto_ReadOnly = true;
    }
    if (VAP_ESTATUS == "C" && VAP_ESTATUS2 == 2) {
        // En Autorizacion 
        Producto_ReadOnly = true;
    }
    if (ESTATUS == 5) {
        // Cancelado
        Producto_ReadOnly = true;
    }

    var editCommandClass = '';
    var editCommandEditAction = "javascript:$editarCantidad($('#" + idContenedor + "\')," + E.Id_Prd + ")";
    var editCommandRemoveAction = "javascript:$retirarProducto($('#" + idContenedor + "\')," + E.Id_Cte + "," + E.Id_Op + "," + E.Id_Prd + ")";
    if (_proyectoSeleccionado.EnValuacion != null) {
        // 19 Sep 2018 RFH
        // Siempre permitira la edicion a Excepcion de en Autorizacion 
        // 
        if (Producto_ReadOnly) {
            //No es editable
            //DropDown_Ellipsis = '<div class="dropdown pull-right dropdown-kebab-pf"></div>' ;
            DropDown_Ellipsis = '<div class="dropdown pull-right dropdown-kebab-pf">' +
                '<button class="btn btn-link dropdown-toggle" id="dropdownKebabRight3" aria-expanded="true" aria-haspopup="true" type="button" data-toggle="dropdown">' +
                '<span class="fa fa-ellipsis-v"></span>' +
                '</button>' +
                '<ul class="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownKebabRight3">' +
                '<li data-idrnd="4654654"> Edición no permitida.</li>' +
                '</ul>' +
                '</div>';
        } else {
            //if (_proyectoSeleccionado.EnValuacion == true || _proyectoSeleccionado.EnValuacion == 1) {
            //  editCommandClass = 'disabled-link';
            //} else {                
            //Es editable 
            DropDown_Ellipsis = '<div class="dropdown pull-right dropdown-kebab-pf">' +
                '<button class="btn btn-link dropdown-toggle" id="dropdownKebabRight3" aria-expanded="true" aria-haspopup="true" type="button" data-toggle="dropdown">' +
                '<span class="fa fa-ellipsis-v"></span>' +
                '</button>' +
                '<ul class="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownKebabRight3">' +
                '<li><a class="' + editCommandClass + '" href="' + editCommandEditAction + '">Editar</a></li>' +
                '<li><a class="' + editCommandClass + '" href="' + editCommandRemoveAction + '">Retirar</a></li>' +
                '</ul>' +
                '</div>';
        }
    }
    // 19 Sep 2018 RFH
    // Siempre permitira la edicion a Excepcion de en Autorizacion 
    //
    if (Producto_ReadOnly) {
        //DropDown_Ellipsis = '<div class="dropdown pull-right dropdown-kebab-pf"></div>' ;
        DropDown_Ellipsis = '<div class="dropdown pull-right dropdown-kebab-pf">' +
            '<button class="btn btn-link dropdown-toggle" id="dropdownKebabRight3" aria-expanded="true" aria-haspopup="true" type="button" data-toggle="dropdown">' +
            '<span class="fa fa-ellipsis-v"></span>' +
            '</button>' +
            '<ul class="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownKebabRight3">' +
            '<li>Edición no permitida.</li>' +
            '</ul>' +
            '</div>';
    } else {
        //if (_proyectoSeleccionado.Estatus===1 || _proyectoSeleccionado.Estatus===2) {
        DropDown_Ellipsis = '<div class="dropdown pull-right dropdown-kebab-pf">' +
            '<button class="btn btn-link dropdown-toggle" id="dropdownKebabRight3" aria-expanded="true" aria-haspopup="true" type="button" data-toggle="dropdown">' +
            '<span class="fa fa-ellipsis-v"></span>' +
            '</button>' +
            '<ul class="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownKebabRight3">' +
            '<li><a class="' + editCommandClass + '" href="' + editCommandEditAction + '">Editar</a></li>' +
            '<li><a class="' + editCommandClass + '" href="' + editCommandRemoveAction + '">Retirar</a></li>' +
            '</ul>' +
            '</div>';
    }

    var tbProducto = '';
    var RutaCarpetasImagen = "http://40.124.41.101/CatalogoUnico_Pruebas/Procesos/Archivos/337/";


    if (E.ImagenProductoAltaRes == '') {
        tbProducto = '<label class="lbProd" ' +
            'data-imagen="' + E.ImagenProductoAltaRes + '" ' +
            'data-producto="' + E.Nombre + '" ' +
            'onclick="ProductoExaminarImagen(this);">' + E.Id_Prd + '&nbsp;' + E.Nombre + '</label>';
    } else {
        tbProducto = '<label class="lbProd" ' +
            'data-imagen="' + E.ImagenProductoAltaRes + '" ' +
            'data-producto="' + E.Nombre + '" ' +
            'onclick="ProductoExaminarImagen(this);">' + E.Id_Prd + '&nbsp;' + E.Nombre + '</label>&nbsp;' +
            '<i class="fa fa-picture-o"></i>';
    }

    var textoNodoDilucion = '<td>' +
        '<div id="dvDilucionDisplay_' + E.Id_Prd + '" >' +
        '<table>' +
        '<tr>' +
        '<td>Dilución</td>' +
        '<td style="text-align: right; width: 60px">' +
        '<div id="dvDilucionDisplayValue">' +
        (E.COP_Dilucion != null ? E.COP_Dilucion : '') +
        '</div>' +
        '</td>' +
        '</tr>' +
        '</table>' +
        '</div>' +
        '<div id="dvDilucionEdit_' + E.Id_Prd + '" style="display:none;">' +
        '<table>' +
        '<tr>' +
        '<td>Dilución 1:</td>' +
        '<td>' +
        '<input type="text" id="txtDilucion" value="' + (E.COP_Dilucion != null ? E.COP_Dilucion : '') + '" style="text-align: right; width: 60px;">' +
        '</td>' +
        '<td>' +
        '<a id="aAceptarDilucion"><i class="fa fa-check" aria-hidden="true"></i></a>' +
        '</td>' +
        '<td>' +
        '<a id="aCancelarDilucion"><i class="fa fa-times" aria-hidden="true"></i></a>' +
        '</td>' +
        '</tr>' +
        '</table>' +
        '</div>' +
        '<td>';
    var n = $('<div class="list-group-item list-view-pf-stacked list-view-pf-top-align row_prd_item" id="lstElem_' + E.Id_Prd + '" elementoDeLista>' +
        '<div class="list-view-pf-checkbox"><input type="checkbox" stype="margin-bottom:5px;"></div>' +
        '<div class="list-view-pf-actions">' +
        DropDown_Ellipsis +
        /*'<div class="dropdown pull-right dropdown-kebab-pf">' +
        '<button class="btn btn-link dropdown-toggle" id="dropdownKebabRight3" aria-expanded="true" aria-haspopup="true" type="button" data-toggle="dropdown">' +
        '<span class="fa fa-ellipsis-v"></span>' +
        '</button>' +
        '<ul class="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownKebabRight3">' +
        '<li><a class="' + editCommandClass + '" id="aEditCommandAction">Editar</a></li>' +
        '<li><a class="' + editCommandClass + '" href="' + editCommandRemoveAction +'" id="aRemoveCommandAction">Retirar</a></li>' +
        '</ul>' +
        '</div>' +*/
        '</div>' +
        '<div class="list-view-pf-main-info PADDING_TB_10" style="background:' + colorInactivo + '">' +
        '<div class="list-view-pf-body">' +
        '<div class="list-view-pf-description">' +
        '<div class="list-group-item-heading">' +
        //E.Id_Prd + '&nbsp;' + E.Nombre + '</div>' +
        //'<div class="list-group-item-text">' + E.Ruta + '</div>' + 
        tbProducto +
        //'<label class="lbProd" data-imagen="'+E.ImagenProductoAltaRes +'" onclick="ProductoExaminarImagen(this);">'+ E.Id_Prd + '&nbsp;' + E.Nombre + '</label>'+
        //'<i class="fa fa-picture-o"></i>'+

        '</div>' +
        '<div class="list-group-item-text"></div>' +
        '</div>' +
        '<div class="list-view-pf-additional-info">' +
        '<table>' +
        '<tr>' +
        '<td>' +
        '<div id="dvCantidadDisplay_' + E.Id_Prd + '">' +
        //cantidad
        '<table>' +
        '<tr>' +
        '<td>Cantidad de Producto</td>' +
        '<td style="text-align: right; width: 60px">' +
        '<div id="dvCantidadDisplayValue">' +
        E.COP_Cantidad +
        '</div>' +
        '</td>' +
        '<td> &nbsp;' + 'piezas' /*E.ProductoSerializable.Prd_UniNe*/ + '</td>' +
        '</tr>' +
        '</table>' +
        '</div>' +
        '<div id="dvCantidadEdit_' + E.Id_Prd + '" style="display:none;">' +
        '<table style="width: 160px;">' +
        '<tr>' +
        '<td>Cantidad:</td>' +
        '<td>' +
        '<input type="text" id="txtCantidad" value="' + E.COP_Cantidad + '" style="text-align: right; width: 60px;">' +
        '</td>' +
        '<td>' +
        '<a id="aAceptarEditarCantidad" style="width:31px;">' +
        '<i class="fa fa-check" aria-hidden="true"></i>' +
        '</a>' +
        '</td>' +
        '<td>' +
        '<a id="aCancelarEditarCantidad"><i class="fa fa-times" aria-hidden="true"></i></a>' +
        '</td>' +
        '</tr>' +
        '</table>' +
        '</div>' +
        '</td>' +
        '</tr>' +
        '<tr>' +
        (E.COP_EsQuimico == true ? textoNodoDilucion : '') +
        '</tr>' +
        '</table>' +
        '</div>' +
        '</div>' +
        '</div>' +
        '</div>');
    if (_proyectoSeleccionado.EnValuacion != null) {
        if (_proyectoSeleccionado.EnValuacion == true || _proyectoSeleccionado.EnValuacion == 1) {
            n.find('#aEditCommandAction').attr('href', '#');
            //n.find('#aRemoveCommandAction').attr('href', '#');
        } else {
            n.find('#aEditCommandAction').click(function () {
                $editarCantidad(n, E.Id_Prd);
            });
            //n.find('#aRemoveCommandAction');
        }
    }
    n.find('#aAceptarEditarCantidad').click(function () {
        $aceptarEditarCantidad(n, E.Id_Prd);
    });
    n.find('#aCancelarEditarCantidad').click(function () {
        $cancelarEditarCantidad(n, E.Id_Prd);
    });
    n.find('#aAceptarDilucion').click(function () {
        $aceptarEditarCantidad(n, E.Id_Prd);
    });
    n.find('#aCancelarDilucion').click(function () {
        $cancelarEditarCantidad(n, E.Id_Prd);
    });
    return n;
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// PRODUCTO AGREGAR OTROS        
//
function $agregarProductoOtro($container, $tipo, _this) {

    var ProductoClave = $container.find('#txtProductoBusquedaOtros').val();
    var ProductoCantidad = $container.find('#txtProductoCantidadOP').val();
    var ProductoDescripcion = $container.find('#txtProductoDescripcionOP').val();
    var Id_Prd = $container.find('#hdnProductoBusquedaOP').val();

    if (ProductoClave == '' || ProductoCantidad == '' || ProductoDescripcion == '') {
        //PatternflyToast.showError('La cantidad o clave de producto no es valido.', 10000);   
        alertify.error('La cantidad o clave de producto no es valido.');
    } else {
        //alert(ProductoClave);               

        $(_this).prop('disabled', true);
        $container.find('#imgAgregandoProducto').show();
        $.ajax({
            //url: '<%=ApplicationUrl %>' + '/api/CrmOportunidadesProductos',
            url: _ApplicationUrl + '/api/CrmOportunidadesProductos/Post',
            type: 'PUT',
            data: $container.find('#frmAgregarProducto').serialize(),
            cache: false,
            statusCode: {
                401: function (jqXHR, textStatus, errorThrown) {
                    $('#dvDialogoInicioSesion').modal();
                    _onLoginSuccessful = $.proxy($agregarProductoAplicacion, $container, _this);
                }
            }
        }).done(function (response, textStatus, jqXHR) {

            var $lstProductos = $container.find('#lstProductos');
            var n = $crearElementoDeListadoDeProductos($container[0].id, response);
            $lstProductos.append(n);
            var lstElem = $lstProductos.find('#lstElem_' + response.Id_Prd);
            lstElem.data('objetodatos', response);
            $container.find('#txtProductoBusquedaOtros').val('');
            $container.find('#txtProductoCantidadOP').val('');
            $container.find('#txtProductoDescripcionOP').val('');
            $container.find('#tdProductoDescripcionOP').removeClass('has-error');
            $container.find('#spanProductoDescripcionHlp').hide();
            $container.find('#btnAgregarProducto').attr('disabled', true);
            if (_totalOtrosProductos.i == 0) {
                //$container.find('#productosBlankSlate').fadeOut();
                $container.find('#contenidoSeccionProductos').fadeIn();
            }
            _totalOtrosProductos.i = _totalOtrosProductos.i + 1;
        }).fail(function (jqXHR, textStatus, error) {
            switch (jqXHR.status) {
                case 401:
                    alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                    break;
                default:
                    //PatternflyToast.showError(jqXHR.responseJSON.Message, 10000);
                    alertify.error(jqXHR.responseJSON.Message);
                    break;
            }
            $(_this).prop('disabled', false);
        }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
            $container.find('#imgAgregandoProducto').hide();
        });
    }
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// PRODUCTO AGREGAR OTROS        
//
function $agregarProductoOtro($container, $tipo, _this) {

    var ProductoClave = $container.find('#txtProductoBusqueda').val();
    var ProductoCantidad = $container.find('#txtProductoCantidad').val();
    var ProductoDescripcion = $container.find('#txtProductoDescripcion').val();
    var Id_Prd = $container.find('#hdnProductoBusqueda').val();

    if (ProductoClave == '' || ProductoCantidad == '' || ProductoDescripcion == '') {
        //PatternflyToast.showError('La cantidad o clave de producto no es valido.', 10000);   
        alertify.error('La cantidad o clave de producto no es valido.');
        return;

    }

    //alert(ProductoClave);               

    $(_this).prop('disabled', true);

    $container.find('#imgAgregandoProducto').show();

    $.ajax({
        //url: '<%=ApplicationUrl %>' + '/api/CrmOportunidadesProductos',
        url: _ApplicationUrl + '/api/CrmOportunidadesProductos/Post',
        type: 'PUT',
        data: $container.find('#frmAgregarProducto').serialize(),
        cache: false,
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy($agregarProductoAplicacion, $container, _this);
            }
        }
    }).done(function (response, textStatus, jqXHR) {

        var $lstProductos = $container.find('#lstProductos');
        var n = $crearElementoDeListadoDeProductos($container[0].id, response);
        $lstProductos.append(n);
        var lstElem = $lstProductos.find('#lstElem_' + response.Id_Prd);
        lstElem.data('objetodatos', response);
        $container.find('#txtProductoBusqueda').val('');
        $container.find('#txtProductoCantidad').val('');
        $container.find('#txtProductoDescripcion').val('');
        $container.find('#tdProductoDescripcion').removeClass('has-error');
        $container.find('#spanProductoDescripcionHlp').hide();
        $container.find('#btnAgregarProducto').attr('disabled', true);
        if (_totalOtrosProductos.i == 0) {
            //$container.find('#productosBlankSlate').fadeOut();
            $container.find('#contenidoSeccionProductos').fadeIn();
        }
        _totalOtrosProductos.i = _totalOtrosProductos.i + 1;
    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
            default:
                //PatternflyToast.showError(jqXHR.responseJSON.Message, 10000);
                alertify.error(jqXHR.responseJSON.Message);
                break;
        }
        $(_this).prop('disabled', false);
    }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
        $container.find('#imgAgregandoProducto').hide();
    });

}


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
//
// PRODUCTO - AGREGAR de APLICACION 
// CRM Ver 1    

function $agregarProductoAplicacion($C, _this) {
    var ProductoClave = $C.find('#txtProductoBusqueda').val();
    var ProductoCantidad = $C.find('#txtProductoCantidadPDA').val();
    var ProductoDescripcion = $C.find('#txtProductoDescripcionPDA').val();
    var Id_Prd = $C.find('#hdnProductoBusquedaPDA').val();

    var fCantidad = parseFloat(ProductoCantidad);
    if (isNaN(fCantidad)) {
        fCantidad = 0;
    }
    if (fCantidad.toString() != ProductoCantidad) {
        fCantidad = 0;
    }

    if (ProductoClave == '' || ProductoCantidad == '' || ProductoDescripcion == '' || Id_Prd == '' || fCantidad == 0) {
        alertify.error('La cantidad o clave de producto no es valido.');
        return;
    }

    $(_this).prop('disabled', true);

    $C.find('#imgAgregandoProducto').show();

    $.ajax({
        url: _ApplicationUrl + '/api/CrmOportunidadesProductos',
        type: 'POST',
        data: $C.find('#frmAgregarProducto').serialize(),
        cache: false,
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $(_this).prop('disabled', false);
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy($agregarProductoAplicacion, $C, _this);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        var $lstProductos = $C.find('#lstProductos');
        var n = $crearElementoDeListadoDeProductos($C[0].id, response);
        $lstProductos.append(n);
        var lstElem = $lstProductos.find('#lstElem_' + response.Id_Prd);
        lstElem.data('objetodatos', response);
        $C.find('#txtProductoBusqueda').val('');
        $C.find('#txtProductoCantidadPDA').val('');
        $C.find('#txtProductoDescripcionPDA').val('');
        $C.find('#tdProductoDescripcionPDA').removeClass('has-error');
        $C.find('#spanProductoDescripcionPDAHlp').hide();
        $C.find('#btnAgregarProducto').attr('disabled', true);
        if (_totalProductos.i == 0) {
            //$C.find('#productosBlankSlate').fadeOut();
            $C.find('#contenidoSeccionProductos').fadeIn();
        }
        _totalProductos.i = _totalProductos.i + 1;
        $(_this).prop('disabled', false);
    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado(3684). Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
            case 409:
                alertify.error('ERROR: Intenta duplicar el producto.');
                break;
            default:
                alertify.error(jqXHR.responseJSON.Message);
                break;
        }
        $(_this).prop('disabled', false);
    }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
        $C.find('#imgAgregandoProducto').hide();
    });

}


// ACEPTAR - EDICION

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function $aceptarEditarCantidad($C, idPrd) {

    var Id_Op = $('#hdnAgregarProducto_Id_Op').val();

    console.log('Crm_Productos.js: function $aceptarEditarCantidad');

    var dvCantidadDisplay = $C.find('#dvCantidadDisplay_' + idPrd);
    var dvCantidadEdit = $C.find('#dvCantidadEdit_' + idPrd);
    var dvDilucionDisplay = $C.find('#dvDilucionDisplay_' + idPrd);
    var dvDilucionEdit = $C.find('#dvDilucionEdit_' + idPrd);

    var fCantidad = $C.find('#txtCantidad').val();
    if (isNaN(fCantidad)) {
        fCantidad = 0;
    }

    //if (ProductoClave == '' || ProductoCantidad == '' || ProductoDescripcion == '' || Id_Prd == '' || fCantidad == 0) {
    if (fCantidad <= 0) {
        alertify.error('La cantidad o clave de producto no es valido.');
        return;
    }

    var dvCantidadDisplayValue = dvCantidadDisplay.find('#dvCantidadDisplayValue');
    var dvDilucionDisplayValue = dvDilucionDisplay.find('#dvDilucionDisplayValue');
    var txtCantidadEdit = dvCantidadEdit.find('#txtCantidad');
    var txtDilucionEdit = dvDilucionEdit.find('#txtDilucion');

    //var $lstElem = $C.find('#lstProductos #lstElem_' + idPrd);
    var dataObject = $C.data('objetodatos');
    var objectCopy = jQuery.extend(true, {}, dataObject);
    objectCopy.COP_Cantidad = txtCantidadEdit.val();
    objectCopy.COP_Dilucion = txtDilucionEdit.val();

    //
    // La informacion que no esta aqui se añade en el 
    // SP

    var eCrmOportunidadesProducto = {
        'Id_Emp': 0,
        'Id_Cd': 0,
        'Id_Op': Id_Op,
        'Id_Cte': 0,
        'Id_Rik': 0,
        'Id_Uen': 0,
        'Id_Seg': 0,
        'Id_Area': 0,
        'Id_Sol': 0,
        'Id_Apl': 0,
        'Id_SubFam': 0,
        'Id_Prd': idPrd,
        'Nombre': '',
        'COP_Cantidad': fCantidad,
        'COP_Dilucion': 0,
        'COP_EsQuimico': true,
        'COP_CostoEnUso': 0.0,
        'COP_ConsumoMensual': 0.0,
        'COP_DilucionAntecedente': 1,
        'COP_DilucionConsecuente': 1,
        'AplDilucion': false,
        'Id_Val': 0,
        'Cantidad': 0,
        'Tipo': 0,
        'Accion': 2
    }

    var DataJson = JSON.stringify(eCrmOportunidadesProducto);

    $.ajax({
        url: _ApplicationUrl + '/api/CrmOportunidadesProductos/CrmOportunidadesProductos_InsertUpdate',
        data: DataJson,
        type: 'PUT',
        cache: false,
        contentType: 'application/json',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy($aceptarEditarCantidad, this, $C, idPrd);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        dvCantidadDisplay.show();
        dvCantidadEdit.hide();
        dvCantidadDisplayValue.text(txtCantidadEdit.val());

        dvDilucionDisplay.show();
        dvDilucionEdit.hide();
        dvDilucionDisplayValue.text(txtDilucionEdit.val());
    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado(3910). Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
            default:
                $('#toastDanger #toastDangerMessage').html(jqXHR.responseJSON.Message);
                $('#toastDanger').fadeIn();
                setTimeout(function () {
                    $('#toastDanger').fadeOut();
                }, 3000);
                break;
        }
    }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
    });

}

//
// Producto - INSERT UPDATE 
//
function Crm_AgregarProducto_Ver2(obj) {

    var Tipo = $(obj).data('tipo');

    var Imagen_BajaRes = '';
    var Imagen_AltaRes = '';

    if (Tipo == 1) {
        var $C = $('#contendorProductos')
        Imagen_BajaRes = $('#ProductoAplicacion_LowResImage').val();
        Imagen_AltaRes = $('#ProductoAplicacion_HighResImage').val();

        var Id_Op = $('#hf_Id_Op').val();
        var ProductoClave = $C.find('#txtProductoBusqueda').val();
        var ProductoCantidad = $C.find('#txtProductoCantidadPDA').val();
        var ProductoDescripcion = $C.find('#txtProductoDescripcionPDA').val();
        var Id_Prd = $C.find('#hdnProductoBusquedaPDA').val();

    }

    if (Tipo == 2) {
        var $C = $('#contenedorOtrosProductos');
        Imagen_BajaRes = $('#ProductoOtro_LowResImagen').val();
        Imagen_AltaRes = $('#ProductoOtro_HighResImagen').val();

        var Id_Op = $('#hf_Id_Op').val();
        var ProductoClave = $C.find('#txtProductoBusquedaOtros').val();
        var ProductoCantidad = $C.find('#txtProductoCantidadOP').val();
        var ProductoDescripcion = $C.find('#txtProductoDescripcionOP').val();
        var Id_Prd = $C.find('#hdnProductoBusquedaOP').val();

    }
    // $C, _this, Tipo
    //$('#contendorProductos'),


    var Producto_LowResImagen = $C.find('#Producto_LowResImagen').val();
    var Producto_HighResImagen = $C.find('#Producto_HighResImagen').val();

    var fCantidad = parseFloat(ProductoCantidad);
    if (isNaN(fCantidad)) {
        fCantidad = 0;
    }

    if (fCantidad.toString() != ProductoCantidad) {
        fCantidad = 0;
    }

    if (ProductoClave == '' || ProductoCantidad == '' || ProductoDescripcion == '' || Id_Prd == '' || fCantidad == 0) {
        alertify.error('La cantidad o clave de producto no es valido.');
        return;
    }

    //$(_this).prop('disabled', true);
    $(obj).prop('disabled', true);

    $C.find('#imgAgregandoProducto').show();

    //
    // La informacion que no esta aqui se añade en el 
    // SP



    var eCrmOportunidadesProducto = {
        'Id_Emp': 0,
        'Id_Cd': 0,
        'Id_Op': Id_Op,
        'Id_Cte': 0,
        'Id_Rik': 0,
        'Id_Uen': 0,
        'Id_Seg': 0,
        'Id_Area': 0,
        'Id_Sol': 0,
        'Id_Apl': 0,
        'Id_SubFam': 0,
        'Id_Prd': Id_Prd,
        'Nombre': ProductoDescripcion,
        'COP_Cantidad': fCantidad,
        'COP_Dilucion': 0,
        'COP_EsQuimico': true,
        'COP_CostoEnUso': 0.0,
        'COP_ConsumoMensual': 0.0,
        'COP_DilucionAntecedente': 1,
        'COP_DilucionConsecuente': 1,
        'AplDilucion': false,
        'Id_Val': 0,
        'Cantidad': 0,
        'Tipo': Tipo,
        'Accion': 1,
        'Imagen_BajaRes': Imagen_BajaRes,
        'Imagen_AltaRes': Imagen_AltaRes,
        'ImagenProductoAltaRes': Imagen_AltaRes

    }

    var DataJson = JSON.stringify(eCrmOportunidadesProducto);

    $.ajax({
        url: _ApplicationUrl + '/api/CrmOportunidadesProductos/CrmOportunidadesProductos_InsertUpdate',
        data: DataJson,
        //data: $C.find('#frmAgregarProducto').serialize(),
        //data: Dat,
        /*{
            //'P': JSON.stringify(eCrmOportunidadesProducto),
            'P': eCrmOportunidadesProducto,
            'Param1': 1            
        },*/
        cache: false,
        type: 'PUT',
        contentType: "application/json; utf-8",
        dataType: "json",
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $(_this).prop('disabled', false);
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy($agregarProductoAplicacion, $C, _this);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        var Datos = response.Datos;
        var Estado = response.Estado;

        if (Estado == -1) {
            alertify.alert('ERROR: Intenta duplicar el producto.');
            //$C.find('#btnAgregarProducto').attr('disabled', false);            
            $('#btnProductosOtros_Agregar').attr('disabled', false);
            return;
        }

        var $lstProductos = $C.find('#lstProductos');
        //var n = $crearElementoDeListadoDeProductos($C[0].id, response);
        var n = $crearElementoDeListadoDeProductos($C[0].id, eCrmOportunidadesProducto);

        $lstProductos.append(n);
        var lstElem = $lstProductos.find('#lstElem_' + response.Id_Prd);
        lstElem.data('objetodatos', response);

        $('#txtProductoBusquedaOtros').val('');
        $('#txtProductoBusqueda').val('');

        $C.find('#txtProductoBusqueda').val('');
        $C.find('#txtProductoBusquedaOtros').val('');
        $C.find('#txtProductoCantidadPDA').val('');
        $C.find('#txtProductoCantidadOP').val('');
        $C.find('#txtProductoDescripcionPDA').val('');
        $C.find('#txtProductoDescripcionOP').val('');
        $C.find('#tdProductoDescripcion').removeClass('has-error');
        $C.find('#spanProductoDescripcionHlp').hide();
        //$C.find('#btnAgregarProducto').attr('disabled', true);
        if (_totalProductos.i == 0) {
            //$C.find('#productosBlankSlate').fadeOut();
            $C.find('#contenidoSeccionProductos').fadeIn();
        }
        _totalProductos.i = _totalProductos.i + 1;

        //$(_this).prop('disabled', false);
        $(obj).prop('disabled', false);

    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado(3684). Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
            case 409:
                alertify.error('ERROR: Intenta duplicar el producto.');
                break;
            default:
                alertify.error(jqXHR.responseJSON.Message);
                break;
        }
        //$(_this).prop('disabled', false);
        $(obj).prop('disabled', false);

    }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
        $C.find('#imgAgregandoProducto').hide();
    });

}


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// Producto busqueda
// OBSOLETO
// OBSOLETO
// OBSOLETO 
function $cargarInfoProductoAplicacion($contenedor, idCte, idOp, idPrd) {

    $contenedor.find('#imgBuscandoProducto').fadeIn();

    $.ajax({
        //url: '<%=ApplicationUrl %>' + '/api/BusquedaProductoCatalogoUnico/?idCte=' + idCte + '&idOp=' + idOp + '&idPrd=' + idPrd,
        url: _ApplicationUrl + '/api/BusquedaProductoCatalogoUnico/?idCte=' + idCte + '&idOp=' + idOp + '&idPrd=' + idPrd,
        cache: false,
        type: 'GET',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy($cargarInfoProductoAplicacion, null, $contenedor, idCte, idOp, idPrd);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        if (response != null) {
            $contenedor.find('#spanProductoDescripcionHlp').hide();
            $contenedor.find('#tdProductoDescripcion').removeClass('has-error');
            $contenedor.find('#txtProductoCantidad').attr('disabled', false);
            $contenedor.find('#txtProductoDescripcion').val(response.CatProductoSerializable.Prd_Descripcion);
            $contenedor.find('#hdnProductoBusqueda').val(response.CatProductoSerializable.Id_Prd);
            $contenedor.find('#btnAgregarProducto').attr('disabled', false);

        } else {
            //TODO: mostrar una señal para indicar que el producto no fué encontrado
            /*$contenedor.find('#tdProductoDescripcion').addClass('has-error');
            $contenedor.find('#spanProductoDescripcionHlp').show();
            $contenedor.find('#txtProductoCantidad').attr('disabled', true);
            $contenedor.find('#tdProductoDescripcion').val('');
            $contenedor.find('#btnAgregarProducto').attr('disabled', true);*/
            alertify.error('El producto ' + idPrd + ' no se encuentra o no existe.');

        }
    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');

                break;
        }
        //PatternflyToast.showError('Ocurrió un error al obtener la información del producto', 6000);
        alertify.error('Ocurrió un error al obtener la información del producto');
    }).always(function (jqXHR, textStatus, errorThrown) {
        $contenedor.find('#imgBuscandoProducto').fadeOut();
    });
}

// * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
// JUN03-2020 RFH
// Reeplaza la funcion ARRIBA
//
// CONSULTA PRODUCTO de APLIACION

function CargarInfo_ProductoAplicacion($contenedor, Id_Cte, Id_Op, Id_Prd, TipoBusqueda) {
    //$contenedor.find('#imgBuscandoProducto').fadeIn();
    $('#spinner_ConsultarProductoAplicacion').fadeIn();
    $.ajax({
        url: _ApplicationUrl + '/api/BusquedaProductoCatalogoUnico/CRM_BusquedaProducto?Id_Cte=' + Id_Cte + '&Id_Op=' + Id_Op + '&Id_Prd=' + Id_Prd + '&Tipo=' + TipoBusqueda,
        cache: false,
        type: 'GET',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy($cargarInfoProductoAplicacion, null, $contenedor, idCte, idOp, idPrd);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        if (response != null) {
            var Estado = response.Estado;
            var resp = response.Datos;

            if (Estado == 2) {

                $('#ProductoAplicacion_LowResImage').val('');
                $('#ProductoAplicacion_HighResImage').val('');
                $('#ProductoOtro_LowResImagen').val('');
                $('#ProductoOtro_HighResImagen').val('');

                alertify.error('El producto ' + Id_Prd + ' no pertenece al catalogo o no existe.');
                $contenedor.find('#txtProductoDescripcion').val('');

                return;
            } else if (Estado == 1) {
                if (resp?._Prd_Activo == 2) {
                    alertify.error(' <h3><strong>PRODUCTO INACTIVO<strong></h3>Reemplazlo por un producto sustituo o de tener alguna duda consultar con tu area operativa').delay(90000);

                    return false;
                }

                if (TipoBusqueda == 1) {
                    $contenedor.find('#spanProductoDescripcionPDAHlp').hide();
                    $contenedor.find('#tdProductoDescripcionPDA').removeClass('has-error');
                    $contenedor.find('#txtProductoCantidadPDA').attr('disabled', false);
                    $contenedor.find('#txtProductoDescripcionPDA').val(resp._Prd_Descripcion);
                    $contenedor.find('#hdnProductoBusquedaPDA').val(resp._Id_Prd);
                    $contenedor.find('#btnAgregarProducto').attr('disabled', false);

                    $('#ProductoAplicacion_LowResImage').val(resp._ImagenProductoBajaRes);
                    $('#ProductoAplicacion_HighResImage').val(resp._ImagenProductoAltaRes);
                }

                if (TipoBusqueda == 2) {
                    $contenedor.find('#spanProductoDescripcionHlp').hide();
                    $contenedor.find('#tdProductoDescripcionOP').removeClass('has-error');
                    $contenedor.find('#txtProductoCantidadOP').attr('disabled', false);
                    $contenedor.find('#txtProductoDescripcionOP').val(resp._Prd_Descripcion);
                    $contenedor.find('#hdnProductoBusquedaOP').val(resp._Id_Prd);
                    $contenedor.find('#btnAgregarProducto').attr('disabled', false);

                    $('#ProductoOtro_LowResImagen').val(resp._ImagenProductoBajaRes);
                    $('#ProductoOtro_HighResImagen').val(resp._ImagenProductoAltaRes);
                }

                return;
            } else {
                alertify.error('Ocurrio un ERROR GRAVE (94)');
            }

        } else {
            //TODO: mostrar una señal para indicar que el producto no fué encontrado
            /*$contenedor.find('#tdProductoDescripcion').addClass('has-error');
            $contenedor.find('#spanProductoDescripcionHlp').show();
            $contenedor.find('#txtProductoCantidad').attr('disabled', true);
            $contenedor.find('#tdProductoDescripcion').val('');
            $contenedor.find('#btnAgregarProducto').attr('disabled', true);*/
            alertify.error('El producto ' + idPrd + ' no se encuentra o no existe.');

        }
    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');
                break;
        }
        //PatternflyToast.showError('Ocurrió un error al obtener la información del producto', 6000);
        alertify.error('Ocurrió un error al obtener la información del producto');
    }).always(function (jqXHR, textStatus, errorThrown) {
        //$contenedor.find('#imgBuscandoProducto').fadeOut();
        $('#spinner_ConsultarProductoAplicacion').fadeOut();

    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function cargarInfoProducto(id) {

    $('#imgBuscandoProducto').fadeIn();

    $.ajax({
        //url: '<%=ApplicationUrl %>' + '/api/CatProducto/?id=' + id,
        url: _ApplicationUrl + '/api/CatProducto/?id=' + id,
        cache: false,
        type: 'GET',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(cargarInfoProducto, null, id);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        if (response != null) {
            $contenedor.$('#spanProductoDescripcionHlp').hide();
            $('#tdProductoDescripcion').removeClass('has-error');
            $('#txtProductoCantidad').attr('disabled', false);
            $('#txtProductoDescripcion').val(response.Prd_Descripcion);
            $('#hdnProductoBusqueda').val(response.Id_Prd);
            $('#btnAgregarProducto').attr('disabled', false);

        } else {
            //TODO: mostrar una señal para indicar que el producto no fué encontrado
            $('#tdProductoDescripcion').addClass('has-error');
            $('#spanProductoDescripcionHlp').show();
            $('#txtProductoCantidad').attr('disabled', true);
            $('#tdProductoDescripcion').val('');
            $('#btnAgregarProducto').attr('disabled', true);
        }
    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');

                break;
        }
        //PatternflyToast.showError('Ocurrió un error al obtener la información del producto', 6000);
        alertify.error('Ocurrió un error al obtener la información del producto');
    }).always(function (jqXHR, textStatus, errorThrown) {
        $('#imgBuscandoProducto').fadeOut();
    });
}


//
// CONUSULTA PRODUCTO 
// Controller Entity F

function CargarInfo_Producto($contenedor, Id_Prd) {

    //$('#imgBuscandoProducto').fadeIn();
    $('#spinner_ConsultarProductoOtros').fadeIn();

    $.ajax({
        //url: '<%=ApplicationUrl %>' + '/api/CatProducto/?id=' + id,
        url: _ApplicationUrl + '/api/CatProducto/?Id_Prd=' + Id_Prd,
        cache: false,
        type: 'GET',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(cargarInfoProducto, null, Id_Prd);
            }
        }
    }).done(function (response, textStatus, jqXHR) {

        var Estado = response.Estado;
        var resp = response.Datos;

        if (Estado == 2) {
            alertify.error('El producto ' + Id_Prd + ' no pertenece al catalogo o no existe.');
            return;
        } else if (Estado == 1) {

            var resp = response.Datos;

            if (resp != null) {
                $contenedor.find('#spanProductoDescripcionHlp').hide();
                $contenedor.find('#tdProductoDescripcion').removeClass('has-error');
                $contenedor.find('#txtProductoCantidad').attr('disabled', false);
                $contenedor.find('#txtProductoDescripcion').val(resp._Prd_Descripcion);
                $contenedor.find('#hdnProductoBusqueda').val(resp._Id_Prd);
                $contenedor.find('#btnAgregarProducto').attr('disabled', false);
            } else {
                //TODO: mostrar una señal para indicar que el producto no fué encontrado
                $contenedor.find('#tdProductoDescripcion').addClass('has-error');
                $contenedor.find('#spanProductoDescripcionHlp').show();
                $contenedor.find('#txtProductoCantidad').attr('disabled', true);
                $contenedor.find('#tdProductoDescripcion').val('');
                $contenedor.find('#btnAgregarProducto').attr('disabled', true);
            }
        }

    }).fail(function (jqXHR, textStatus, error) {
        switch (jqXHR.status) {
            case 401:
                alert('LA sesion ha expirado. Por favor, haga inicio de sesion para continuar utilizando la aplicación.');

                break;
        }
        //PatternflyToast.showError('Ocurrió un error al obtener la información del producto', 6000);
        alertify.error('Ocurrió un error al obtener la información del producto');
    }).always(function (jqXHR, textStatus, errorThrown) {
        //$('#imgBuscandoProducto').fadeOut();
        $('#spinner_ConsultarProductoOtros').fadeOut();
    });
}

//  
// Consulta Producto de Aplicacion 
//
function Consultar_Producto_DeAplicacion(obj) {
    //function AgregarProducto_Busqueda(obj) {
    var Id = $(obj).val();
    if (Id != '') {
        //var Id_OP = $('#hdnAgregarProductoPDA_Id_Op').val();
        var ooOP = $('#hf_Id_Op');
        //  console.log("11");
        //  console.log(Id_OP);
        var Id_OP = ooOP.val();
        //$cargarInfoProducto($('#contendorProductos'), id);        
        CargarInfo_ProductoAplicacion($('#contendorProductos'), _proyectoSeleccionado.Id_Cte, Id_OP, Id, 1);
        //$cargarInfoProductoAplicacion($('#contendorProductos'), _proyectoSeleccionado.Id_Cte, Id_OP, //_proyectoSeleccionado.Id, Id);
    }
}

// 
// Consulta la Info del Producto - Otros 
//
function Consultar_Producto_Otros(obj) {
    //function AgregarProducto_Otros(obj) {
    var Id = $(obj).val();
    if (Id != '') {
        var ooOP = $('#hf_Id_Op');
        //  var Id_OP = $('#hdnAgregarProductoOP_Id_Op').val();
        var Id_OP = ooOP.val();
        //  console.log("22");
        //  console.log(ooOP.val() );
        //CargarInfo_Producto($('#contenedorOtrosProductos'), Id);
        CargarInfo_ProductoAplicacion($('#contenedorOtrosProductos'), _proyectoSeleccionado.Id_Cte, Id_OP, Id, 2);
        //$cargarInfoProducto($('#contenedorOtrosProductos'), Id);   
        //agregarProductoOtro($('#contenedorOtrosProductos'), Id);    
    }
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
$(document).ready(function () {

    /*
    $('#txtProductoBusqueda').blur(function (eventObject) {
    var id = $(this).val();
    if (id != '') {
    //$cargarInfoProducto($('#contendorProductos'), id);
    $cargarInfoProductoAplicacion(
    $('#contendorProductos'), 
    _proyectoSeleccionado.Id_Cte, 
    _proyectoSeleccionado.Id, 
    id);
    }
    });

    $('#txtProductoBusquedaOtros').blur(function (eventObject) {
    var id = $(this).val();
    if (id != '') {
    $cargarInfoProducto($('#contenedorOtrosProductos'), id);
    }
    });
    */

});