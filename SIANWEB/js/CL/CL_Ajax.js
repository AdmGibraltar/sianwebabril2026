/*
*/

/*const { callback } = require("../Chart.bundle");*/
// GRABAR COMENTARIOS   

function AJAX_CL_GRABACOMENTARIOSCLIENTE(
    Id_Solicitud, Comentario, Vigencia, TipoSolicitud, PedidoReferencia,
    CALLBACK_Exito) {
    $.ajax({
        url: _ApplicationUrl + '/api/CL_Main/CL_GrabaComentariosCliente',
        data: {
            Id_Solicitud: Id_Solicitud,
            Comentario: Comentario,
            Vigencia: Vigencia,
            TipoSolicitud: TipoSolicitud,
            PedidoReferencia: PedidoReferencia
        },
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
        var Estado = response.Estado;
        var Datos = response.Datos;
        if (Estado == 1) {
            if (CALLBACK_Exito) {
                CALLBACK_Exito(Datos);
            }
        } else {
            alertify.error('Ocurrió una error: AJAX_CL_GRABACOMENTARIOSCLIENTE(29)');
        }
    }).fail(function (jqXHR, textStatus, error) {
        console.log('ERROR: ' + jqXHR.responseText);
        if (jqXHR.status == 401) {
            $('#dvDialogoInicioSesion').appendTo("body").modal();
        } else {
            alertify.error('Ocurrió una error:' + jqXHR.responseText);
        }
    });
}

// Listade de motivos
function ajax_CL_CodigoHomologado_Maximo_Consulta(Id_Prov, Spiner, CALLBACK_Exito) {
    $.ajax({
        url: _ApplicationUrl + '/api/CL_Main/Get_CodigoHomologado_Maximo_Consulta',
        data: {
            Id_Prov: Id_Prov
        },
        cache: false,
        type: 'GET',
    }).done(function (response, textStatus, jqXHR) {
        var Estado = response.Estado;
        var Datos = response.Datos;
        if (Estado == 1) {
            if (CALLBACK_Exito) {
                CALLBACK_Exito(Datos);
            }
        } else {
            alertify.error('Ocurrió una error: ajax_CL_CodigoHomologado_Maximo_Consulta');
        }
    }).fail(function (jqXHR, textStatus, error) {
        if (jqXHR.status == 401) {
            $('#dvDialogoInicioSesion').appendTo("body").modal();
        } else {
            alertify.error('Ocurrió una error:' + jqXHR.responseText);
        }
    });
}

var PrecioProducto = {

    Editar: function (obj) {
        $('#tblModificarPrecio').css('display', 'block');
        var id = $(obj).data('id');
        $('#rowIdModificarPrecio').val(id);
        var Pre_Descripcion = $('#Pre_Descripcion_' + id).text();
        $('#lbModificaTipoPrecio').text(Pre_Descripcion);
        var Pre_Pesos = $('#Pre_Pesos_' + id).text();
        $('#tbModificaTipoPrecio_Precio').val(Pre_Pesos);
    },

    Aplicar: function () {
        var cmbCategorias = $('#cmbCategorias').val();
        var Id = $('#rowIdModificarPrecio').val();
        var Id2 = 3;
        var Pre_Pesos = $('#tbModificaTipoPrecio_Precio').val();
        var TipoPrecio = $('#lbModificaTipoPrecio').text();
        Pre_Pesos = parseFloat(Pre_Pesos);
        if (isNaN(Pre_Pesos)) {
            if (Pre_Pesos <= 0) {
                Pre_Pesos = 0;
                alertify.error('El precio no puede ser cero.');
                return;
            }
        } else {
            if (Pre_Pesos <= 0) {
                alertify.error('El precio no puede ser cero.');
                return;
            }
        }
        // Codigo Central con Abasto Local 
        if (cmbCategorias == 1) {
            var Prd_Actual_AAA = $('#Prd_Actual_0').val();
            Prd_Actual_AAA = parseFloat(Prd_Actual_AAA);
            if (isNaN(Prd_Actual_AAA)) {
                Prd_Actual_AAA = 0;
            }

            if (Pre_Pesos > Prd_Actual_AAA) {
                //Pre_Pesos = Pre_Pesos.toFixed(2);
                $('#Pre_Pesos_' + Id2).text(Pre_Pesos);
                $('#Pre_Pesos_' + Id).text(Pre_Pesos);
                $('#tblModificarPrecio').css('display', 'none');
            }
            else {
                $('#Pre_Pesos_' + Id2).text(Prd_Actual_AAA.toFixed(2));

                Pre_Pesos = Pre_Pesos.toFixed(2);
                $('#Pre_Pesos_' + Id).text(Pre_Pesos);
                $('#tblModificarPrecio').css('display', 'none');
            }
        }
        else {
            if (TipoPrecio == 'Precio de Lista (Venta)' || TipoPrecio == 'Precio de Lista') {
                if (Id == 1 || Id == 0) {
                    Pre_Pesos = Pre_Pesos.toFixed(2);
                    $('#Pre_Pesos_' + Id).text(Pre_Pesos);
                    $('#tblModificarPrecio').css('display', 'none');
                }
            }
            else if (Id == 1 && cmbCategorias == 3)
            {
                Pre_Pesos = Pre_Pesos.toFixed(2);
                $('#Pre_Pesos_' + Id).text(Pre_Pesos);
                $('#Pre_Pesos_' + 2).text(Pre_Pesos);
                $('#tblModificarPrecio').css('display', 'none');
            }
            else if (Id == 2 && cmbCategorias == '4' || cmbCategorias == '2') {
                Pre_Pesos = Pre_Pesos.toFixed(2);
                $('#Pre_Pesos_' + Id).text(Pre_Pesos);
                $('#Pre_Pesos_' + Id2).text(Pre_Pesos);
                $('#tblModificarPrecio').css('display', 'none');
            }
            else {
                Pre_Pesos = Pre_Pesos.toFixed(2);
                $('#Pre_Pesos_' + Id).text(Pre_Pesos);
                $('#Pre_Pesos_' + Id2).text(Pre_Pesos);
                $('#Pre_Pesos_' + 2).text(Pre_Pesos);
                $('#tblModificarPrecio').css('display', 'none');
            }
        }
        },     

    Cancelar: function () {
        $('#tblModificarPrecio').css('display', 'none');
        /*
        if (jqXHR.status == 401) {
            $('#dvDialogoInicioSesion').appendTo("body").modal();
        } else {
            alertify.error('Ocurrió una error:' + jqXHR.responseText);
        }
        */
    }
    //
}



var DetallesEditar = {
    Precios: function (Id_Cd, Id_Comp, Id_Prd, RES) {
        CL_Ajax.Cargar_DatosPrecios_Ajax(Id_Comp, Id_Cd, Id_Prd,
            function (lst) {
                if (lst != null) {
                    for (var i = 0; i < lst.length; i++) {
                        //Cargar taba precios

                    }
                }
            }
        );
    },
}

var CL_Ajax2 = {
    spCLCen_ModificarCompraLocal: function (KeyArray_ClienteExclusivos, Id_Cd, Id_Comp, Id_Prd, Prd_FechaInicio, Prd_FechaFin, Prd_ClaveUnidad, Prd_ClaveProdServ, CALLBACK_Exito, CALLBACK_Error) {
        $.ajax({
            url: _ApplicationUrl + '/api/CL_Main/spCLCen_ModificarCompraLocal',
            data: {
                Id_Cd: Id_Cd,
                Id_Comp: Id_Comp,
                Id_Prd: Id_Prd,
                Prd_FechaInicio: Prd_FechaInicio,
                Prd_FechaFin: Prd_FechaFin,
                Prd_ClaveUnidad: Prd_ClaveUnidad,
                Prd_ClaveProdServ: Prd_ClaveProdServ,
                KeyArray_ClienteExclusivos: KeyArray_ClienteExclusivos,
            },
            cache: false,
            type: 'GET'
        }).done(function (response, textStatus, jqXHR) {
            var Estado = response.Estado;
            var Datos = response.Datos;

            if (Estado == 1) {
                if (CALLBACK_Exito) {
                    CALLBACK_Exito(Datos);
                }
            } else {

            }
        }).fail(function (jqXHR, textStatus, error) {
            console.log('ERROR: ' + jqXHR.responseText);
            if (CALLBACK_Error) {
                CALLBACK_Error(jqXHR.responseText);
            }
            if (jqXHR.status == 401) {
                $('#dvDialogoInicioSesion').appendTo("body").modal();
            } else {
                alertify.error('Ocurrió una error:' + jqXHR.responseText);
            }
        });
    }
}

var CL_Ajax = {
    Cargar_DatosPrecios_Ajax: function (Id_Comp, Id_Cd, Id_Prd, CALLBACK, CALLBACK_Error) {
        $.ajax({
            url: _ApplicationUrl + '/api/CL_Main/Get_DatosPreciosCL' +
                '?Id_Comp=' + Id_Comp +
                '&Id_Cd=' + Id_Cd +
                '&Id_Prd=' + Id_Prd,
            cache: false,
            type: 'GET',
            statusCode: {
                401: function (jqXHR, textStatus, errorThrown) {
                    $('#dvDialogoInicioSesion').appendTo("body").modal();
                    _onLoginSuccessful = $.proxy(Cargar_DatosPrecios_Ajax, null, $, Id_Comp, Id_Cd, Id_Prd, CALLBACK, CALLBACK_Error);
                }
            }
        }).done(function (response, textStatus, jqXHR) {
            var RES = response.Datos;
            var Estado = response.Estado;

            if (Estado == 1) {
                if (CALLBACK) {
                    CALLBACK(RES);
                }
            }

            if (Estado == -1) {
                CALLBACK_Error();
            }


        }).fail(function (jqXHR, textStatus, error) {
            if (jqXHR.status == 401) {
                alert('La sessión ha expirado.');
                $('#dvDialogoInicioSesion').appendTo("body").modal();
            } else {
                alertify.success('Error: funcion Get_DetalleCL.');
            }
        });
    }
}


// Lanza Modal Buscar Cliente
$('#btnAgregarClienteListado').click(function (e) {
    console.log(e);
    $('#modalBuscarCliente').appendTo("body").modal('show');
});

$('#btnBuscarCliente_Ok').click(function (e) {
    TextoBuscar = $('#BuscarCliente_Texto').val();
    Cliente_BuscarCliente(TextoBuscar, function (Datos) {
        if (Datos.length > 0) {
            $('#BuscarCliente_Lista_RE').text(Datos.length + ' Registros encontrados');
            var RegistrosEncontrados = Datos[0].RegistrosEcontrados;
            $('#tblBuscarCliente_Lista > tbody').empty();
            for (var i = 0; i < Datos.length; i++) {
                var row = $('<tr>');
                row.append($('<td>').append(Datos[i].Cte));
                row.append($('<td>').append(Datos[i].RFC));
                row.append($('<td>').append(Datos[i].NomComercial));
                row.append($('<td>').append(Datos[i].tipoCliente));
                if (Datos[i].Activo == 1) {
                    row.append($('<td>').append(
                        '<button ' +

                        ' data-id_cte="' + Datos[i].Cte + '" ' +
                        ' data-cte_nombre="' + Datos[i].NomComercial + '" ' +
                        ' data-tipo_cte="' + Datos[i].tipoCliente + '" ' +
                        ' onclick="btnBuscarCliente_Agregar(this);" ' +
                        'class="button">' +
                        '<span>Agregar</span>' +
                        '</button>'
                    ));
                } else {
                    row.append($('<td>').append());
                }
                $('#tblBuscarCliente_Lista tbody').append(row);
            }
        } else {
            $('#tblBuscarCliente_Lista > tbody').empty();
            $('#BuscarCliente_Lista_RE').text('No se Encontraro Registros');
        }
    });
});

function btnClienteExc_Remover(Obj) {
    var row = $(Obj).data('row');
    //rlert(row);
    $('#Row_ClientesExc_' + row).remove();
}
// Agregar Cliente al Listado
function btnBuscarCliente_Agregar(Obj) {
    var tipo_cliente = $(Obj).data('tipo_cte');
    var id_cte = $(Obj).data('id_cte');
    var cte_nombre = $(Obj).data('cte_nombre');
    CONT_ClientesExc = CONT_ClientesExc + 1;
    var row = $('<tr id="Row_ClientesExc_' + CONT_ClientesExc + '" >');

    row.append($('<td class="text-center">').append(
        '<label id="lstCE_TipoCliente_' + CONT_ClientesExc + '">' + tipo_cliente + '</label>'
    ));

    row.append($('<td class="text-center">').append(
        '<label id="lstCE_IdCte_' + CONT_ClientesExc + '">' + id_cte + '</label>'
    ));

    row.append($('<td class="text-left">').append(
        '<label id="lstCE_Nombre_' + CONT_ClientesExc + '">' + cte_nombre + '</label>'
    ));

    row.append($('<td class="text-center">').append(
        '<i class="fa fa-times fa-2x clickable" ' +
        'data-row="' + CONT_ClientesExc + '" ' +
        'onclick="btnClienteExc_Remover(this);" ' +
        '>' +
        '</i>'
    ));
    $('#tbl_ClientesExclusivos > tbody').append(row);
    $('#modalBuscarCliente').modal('hide');
}


function btnEditar_Guardar() {
    $('#btnEditar_Guardar').prop('disabled', true);
    let Id_Prd = $('#txtCodigoAbastoCL').val();
    let ProductoPadre = $('#TextId_Prd').val();
    let Id_Comp = $('#txtId_Comp').val();
    let Id_Cd = $('#txtIdCd').val();
    let IdTipoSolicitud = $('#txtIdTipoSolicitud').val();
    let Prd_Descripcion = $('#TextPrd_Descrpcion').val();
    let TipoProducto = $('#txtTipoProducto').val();
    let FamName = $('#cmbFam').val();
    let SubFamName = $('#cmbSubFam').val();
    let CausaDesabastoName = $('#cmbCausaDesabasto').val();
    let Prd_NomProvCentral = $('#txtProveedor').val();
    let Prd_CodigoProv = $('#txtCodProveedor').val();
    let Prd_DescripcionProv = $('#txtDesProveedor').val();
    let Prd_PresentacionProv = $('#cmbPresentacionProv').val();
    let KeyArray_ClienteExclusivos = null;

    //Carga Lista de precios
    ListaProductoPrecios = [];
    var i2 = 0;
    for (i = 0; i < 5; i++) {
        i2 = i2 + 1;
        var Prd_Actual = $('#Prd_Actual_' + i).val();
        var Pre_Descripcion = $('#Pre_Descripcion_' + i).text();
        var Prd_FechaInicio = $('#rdpVigencia').val();
        //Prd_FechaInicio = format_YYYYMMDD(Prd_FechaInicio);
        var Prd_FechaFin = $('#rdpVigenciaFin').val();
        //Prd_FechaFin = format_YYYYMMDD(Prd_FechaFin);
        var Pre_PesosX = $('#Pre_Pesos_' + i).text();
        var Pre_Prd = txtCodProd;  // $('#txtCodigoUsadoProd').val();
        if (Pre_Descripcion && Prd_FechaInicio && Prd_FechaFin && Pre_PesosX) {
            if (Prd_Actual == "true") {
                Prd_Actual = 1;
            } else {
                Prd_Actual = 0;
            }
            var ObjPrecio = {
                '_Id_Emp': 0,
                '_int _Id_Cd': 0,
                '_long _Id_Prd': Pre_Prd,
                '_Id_Pre': i2,
                '_Prd_Actual': Prd_Actual,
                '_Prd_FechaInicio': Prd_FechaInicio,
                '_Prd_FechaFin': Prd_FechaFin,
                '_Prd_PreDescripcion': '',
                '_Pre_Descripcion': Pre_Descripcion,
                '_Prd_PreDescripcion': Pre_Descripcion,
                '_Prd_Pesos': Pre_PesosX
            }
            ListaProductoPrecios.push(ObjPrecio);
        }
    }
    var Det_Costo = 0;
    var Pre_Pesos = 0;
    // Pedido
    let Costo = 0;
    for (i = 0; i < 5; i++) {
        let Pre_Descripcion = $('#Pre_Descripcion_' + i).text();
        if (Pre_Descripcion == 'Precio AAA código compra local') {
            Pre_Pesos = $('#Pre_Pesos_' + i).text();
            Pre_Pesos = parseFloat(Pre_Pesos);
            if (isNaN(Pre_Pesos)) {
                Pre_Pesos = 0;
            }
            Costo = Pre_Pesos;
        }
    }

    //Se valida el precio
    if (Costo <= 0) {
        alertify.alert("El precio AAA del código de compra local no puede ser CERO.");
        $('#SPINNER_Guardar').css('display', 'none');
        $('#btnGuardarCompraLocal').prop('disabled', false);
        return;
    } else {
        Det_Costo = Pre_Pesos;
    }

    // Se cargan y validan los datos del sat
    let cmbUnidadMedidaSATDesabasto = $('#cmbUnidadMedidaSATDesabasto option:selected').text();
    let cmbProdServicioSATDesabasto = $('#ddlProdServicio_SATDesabasto option:selected').text();

    let cmbUnidad = $('#cmbUnidadMedidaSATDesabasto').val();
    let cmbServicio = $('#ddlProdServicio_SATDesabasto').val();

    if (cmbUnidad == "" || cmbServicio == "") {
        alertify.alert("Falta alguno de los datos en las opciones SAT.");
        $('#SPINNER_Guardar').css('display', 'none');
        $('#btnGuardarCompraLocal').prop('disabled', false);
        return;
    }

    //Se actualizan los precios 
    if (Costo > 0) {
        for (i = 0; i < ListaProductoPrecios.length; i++) {
            var Id_Pre
            if (ListaProductoPrecios[i]._Pre_Descripcion == 'Precio AAA código key')
                Id_Pre = 1;
            if (ListaProductoPrecios[i]._Pre_Descripcion == 'Precio de Lista')
                Id_Pre = 2;
            if (ListaProductoPrecios[i]._Pre_Descripcion == 'Costo')
                Id_Pre = 3;
            if (ListaProductoPrecios[i]._Pre_Descripcion == 'Precio AAA código compra local')
                Id_Pre = 4;
            var Prd_FechaInicio = ListaProductoPrecios[i]._Prd_FechaInicio;
            var Prd_FechaFin = ListaProductoPrecios[i]._Prd_FechaFin;
            var Prd_Pesos = ListaProductoPrecios[i]._Prd_Pesos;

            AJAX_CL_UpdatePreciosCompraLocal(
                Id_Cd, Id_Prd, Id_Pre, Prd_FechaInicio, Prd_FechaFin, Prd_Pesos,
                function () { }
            );

        }
    }

    if (IdTipoSolicitud == '3') {
        // LISTADO CLIENTE EXCLUSIVOS
        var Fecha = new Date();
        KeyArray_ClienteExclusivos = Fecha.getDate() + '' + (Fecha.getMonth() + 1) + '' + Fecha.getFullYear() + '' + Fecha.getHours() + '' + Fecha.getMinutes() + '' + Fecha.getSeconds();
        var IdArray_ClientesExclusivos = 0;
        for (i = 0; i < 15; i++) {
            var lstCE_IdCte = $('#lstCE_IdCte_' + i).text();
            var lstCE_Nombre = $('#lstCE_Nombre_' + i).text();
            var lstCE_TipoCliente = $('#lstCE_TipoCliente_' + i).text();
            if (lstCE_IdCte != undefined && lstCE_Nombre != undefined && lstCE_TipoCliente != undefined && lstCE_IdCte != '' && lstCE_Nombre != '' && lstCE_TipoCliente != '') {
                AJAX_CL_InsertClienteExclusivo(
                    lstCE_IdCte, lstCE_Nombre, KeyArray_ClienteExclusivos, lstCE_TipoCliente,
                    function () { }
                );
                IdArray_ClientesExclusivos = IdArray_ClientesExclusivos + 1;
            }
        }

        if (IdArray_ClientesExclusivos == "0") {
            alertify.alert("Debe seleccionar al menos un cliente exclusivo para continuar.");
            return;
        }
    }

    let Prd_ClaveUnidad = cmbUnidadMedidaSATDesabasto.substring(0, 3);
    let Prd_ClaveProdServ = cmbProdServicioSATDesabasto.substring(0, 8);
    Id_Comp = parseInt(Id_Comp);

    CL_Ajax2.spCLCen_ModificarCompraLocal(KeyArray_ClienteExclusivos, Id_Cd, Id_Comp, Id_Prd, Prd_FechaInicio, Prd_FechaFin, Prd_ClaveUnidad, Prd_ClaveProdServ,
        function () {
            alertify.success('La solicitud ' + Id_Comp + ' ha sido modificada correctamente.');
            $('#modalEditar').appendTo("body").modal('hide');
            Indice.btn_ConsultaListado(function () {
                $('#spinner_Index').css('display', 'none');
            });
        }, function (Message) {
            alertify.error(Message);
        });
}



function btn_Editar(obj) {
    $('#spinner_Index').css('display', 'block');
    $(obj).attr('disabled', 'disabled');
    var Id_Comp = $(obj).data('id_comp')
    var Id_Cd = $(obj).data('id_cd')
    var AutorizadorId = $(obj).data('autorizadorid')
    var IdTipoSolicitud = $(obj).data('idtiposolicitud')
    var Prd_ClaveUnidad = 0
    var Prd_ClaveProdServ = 0
    var Id_Prd = $(obj).data('id_prd')
    Cargar_cbmUnindadMedidaSAT(0, function () { });
    Cargar_cmbSATProductosServicios(0, function () { });

    $('#modalEditar').appendTo("body").modal('show');
    Cargar_SolicitudDetalle_Ajax(Id_Comp, Id_Cd, AutorizadorId, function (RES) {
        Inicializar_SolicitudEditable();

        //Encabezados
        $('#txtMotivoCL').val(RES.IdTipoSolicitud + '-' + RES.TipoSolicitud);
        $('#txtCodigoKeyCL').val(RES.CodigoPadre + '-' + RES.Prd_Descripcion);
        $('#txtCodigoAbastoCL').val(RES.Id_Prd);
        $('#txtId_Comp').val(Id_Comp);
        $('#txtIdTipoSolicitud').val(IdTipoSolicitud);
        $('#cmbCategorias').val(IdTipoSolicitud);
        $('#txtIdCd').val(Id_Cd);
        //Datos Generales
        $('#TextId_Prd').val(RES.CodigoPadre);
        $('#txtCodProd').val(RES.Id_Prd);

        $('#TextPrd_Descrpcion').val(RES.Prd_Descripcion);
        $('#cmbPresentacion').val(RES.Prd_Presentacion);

        //Recalcula las fechas de vigencia
        //Inicio
        var Fecha = new Date();
        var FechaFin = new Date();
        var Dia = Fecha.getDate();
        Dia = parseInt(Dia);
        if (Dia <= 9) {
            Dia = '0' + Dia;
        }
        var Mes = 0;
        Mes = Fecha.getMonth() + 1;
        Mes = parseInt(Mes);
        if (Mes <= 9) {
            Mes = '0' + Mes;
        }
        Fecha = Dia + '/' + Mes + '/' + Fecha.getFullYear();

        $('#rdpVigencia').val(Fecha);

        if (RES.IdTipoSolicitud == "1") {
            var FechaFinal = addDaysToDate(FechaFin, 15);
            var DiaFin = FechaFinal.getDate();
            var MesFin = FechaFinal.getMonth() + 1;
            if (DiaFin <= 9) {
                DiaFin = '0' + DiaFin
            }
            if (MesFin <= 9) {
                MesFin = '0' + MesFin
            }


            FechaFin = DiaFin + '/' + MesFin + '/' + FechaFinal.getFullYear();
            $('#rdpVigenciaFin').val(FechaFin);
        } else {
            var Año = FechaFin.getFullYear(FechaFin.getFullYear() + 1);
            Año = Año + 1;
            FechaFin = Dia + '/' + Mes + '/' + Año;
            $('#rdpVigenciaFin').val(FechaFin);
        }

        //Fin

        $('#txtTipoProducto').val(RES.IdTipoProd);
        $('#cmbTipoProducto').val(RES.NomTipoProd)
        $('#cmbFam').val(RES.Aplicacion);
        $('#cmbSubFam').val(RES.Prd_SubFamilia);
        $('#txtProveedorCentral').val(RES.ProveedorCentral);
        $('#txtProveedor').val(RES.ProveedorLocal);
        $('#txtCodProveedor').val(RES.Prd_CodigoProv);
        $('#txtDesProveedor').val(RES.Prd_DescripcionProv);
        $('#cmbPresentacionProv').val(RES.Prd_PresentacionProv);
        $('#cmbUentrada').val(RES.Prd_UniNe);
        $('#txtFactorConversion').val(RES.Prd_Unico);
        $('#cmbUsalida').val(RES.Prd_UniNs);
        $('#txtUempaque').val(RES.Prd_UniEmp);
        $('#cmbCausaDesabasto').val(RES.Comentarios);

        //Datos Precios 
        LlenarPreciosCL(RES.Id_Prd, function () { });

        //SAT
        Prd_ClaveUnidad = parseInt(RES.Prd_ClaveUnidad);
        Prd_ClaveProdServ = parseInt(RES.Prd_ClaveProdServ);
        // Se cargan los datos de la solicitud existente
        $('#cmbUnidadMedidaSATDesabasto').val(Prd_ClaveUnidad);
        $('#ddlProdServicio_SATDesabasto').val(Prd_ClaveProdServ);

        CONT_ClientesExc = 0;

        //Clientes Exclusivos
        if (RES.IdTipoSolicitud <= 2) {
            $('#tbl_ClientesExclusivos > tbody').empty();
            $('#btnAgregarClienteListado').prop('disabled', true);
        }
        if (RES.IdTipoSolicitud == 3) {
            $('#tbl_ClientesExclusivos > tbody').empty();
            LlenarClientesExclusivos(Id_Comp, function () { });
            $('#btnAgregarClienteListado').prop('disabled', false);
        }

        //
        // DESPILGA LISTADO LOGS
        //

        $('#tblCompraLocalLogs > tbody').empty();

        EXEC_spCompraLocal_GetLogs_Ajax(Id_Comp, RES.Id_Cd
            , function (lstlogs) {
                // Exito            
                if (lstlogs != null) {
                    for (var i = 0; i < lstlogs.length; i++) {
                        var rowLog = $('<tr>');
                        rowLog.append($('<td style="text-align:left;">').append(
                            lstlogs[i].Fecha + ' ' + lstlogs[i].Nota
                        ));
                        $('#tblCompraLocalLogs > tbody').append(rowLog);
                    }
                    if (lstlogs.length <= 0) {
                        $('#tblCompraLocalLogs > tbody').append('No hay historial.');
                    }
                } else {
                    $('#tblCompraLocalLogs > tbody').append('No hay historial.');
                }

            }, function (Mensaje) {
                // Error            
                $('#tblCompraLocalLogs > tbody').append('Error.');
            });

        $(obj).removeAttr('disabled');
        $('#MensajeCargandoEdit').fadeOut();
        $('#spinnerTitleEdit').css('display', 'none');

    },



        function (RES) {
            $(obj).removeAttr('disabled');
            $('#spinner_Index').css('display', 'none');
            alertify.error("Ha ocurrido un error al intentar cargar la solicitud [" + Id_Comp + "]");
        });
}

function addDaysToDate(date, days) {
    var res = new Date(date);
    res.setDate(res.getDate() + days);
    return res;
}

function btn_VerDetalle(obj) {
    console.log(obj);
    $('#spinner_Index').css('display', 'block');
    $(obj).attr('disabled', 'disabled');
    var Id_Comp = $(obj).data('id_comp')
    var Id_Cd = $(obj).data('id_cd')
    var AutorizadorId = $(obj).data('autorizadorid')
    //var Id_U = $(obj).data('')
    $('#modalDetalle').appendTo("body").modal('show');

    //Carga detalles de solicitud
    //RBM Ene 2024

    Cargar_SolicitudDetalle_Ajax(Id_Comp, Id_Cd, AutorizadorId, function (RES) {
        Inicializar_Solicitud();

        var CodigoPadre = RES.CodigoPadre;
        if (CodigoPadre == "0")
            CodigoPadre = 'NA';

        $('#lbIdComp').text(Id_Comp);
        $('#tbCLFechaSol').val(format_YYYYMMDD(RES.FechaSol));
        $('#tbCLFechaVig').val(format_YYYYMMDD(RES.Vigencia));
        $('#lbCLEstatus').text(RES.EstatusAut);
        $('#lbCLId_Cd').text(RES.Id_Cd);
        $('#lbCLPermisoAutorizar').text(RES.PermisoAutorizar);
        $('#tbCLSucursal').val(RES.Cd_Nombre);
        $('#tbCLSolicitante').val(RES.Solicito_Nombre);
        $('#tbCLMotivoCompra').val(RES.TipoSolicitud);
        $('#tbCLId_Prd').val(RES.Id_Prd);
        $('#tbCLPrd_Descripcion').val(RES.Prd_Descripcion);
        $('#tbCLCodigoPadre').val(CodigoPadre);

        $('#tbCLProvLocal').val(RES.ProveedorLocal);
        $('#tbCLTipoProd').val(RES.NomTipoProd);
        $('#tbCLProvCentral').val(RES.ProveedorCentral);
        $('#tbCLClienteExc').val(RES.ClienteExc);
        $('#tbCLAplicacion').val(RES.Aplicacion);
        $('#tbCLMotivo').val(RES.Comentarios);
        $('#tbCLCosto').val("$" + RES.Costo);
        $('#tbCLAAACompraLocal').val("$" + RES.PrecioAAACL);

        $('#tbCLAAAKey').val("$" + RES.PrecioAAAKey);
        $('#tbCLPrecioLista').val("$" + RES.PrecioPublico);


        //Lenar tabla Clientes Exclusivos 
        if (RES.IdTipoSolicitud == 3) {
            $('#tbl_ClientesExc > tbody').empty();
            LlenarClientesExclusivosDetalle(Id_Comp, function () { });
            //$('#btnAgregarClienteListado').prop('disabled', false);
        }


        //
        // DESPILGA LISTADO LOGS
        //

        $('#tblCompraLocalELogs > tbody').empty();

        EXEC_spCompraLocal_GetLogs_Ajax(Id_Comp, RES.Id_Cd
            , function (lstlogs) {
                // Exito            
                if (lstlogs != null) {
                    for (var i = 0; i < lstlogs.length; i++) {
                        var rowLog = $('<tr>');
                        rowLog.append($('<td style="text-align:left;">').append(
                            lstlogs[i].Fecha + ' ' + lstlogs[i].Nota
                        ));
                        $('#tblCompraLocalELogs > tbody').append(rowLog);
                    }
                    if (lstlogs.length <= 0) {
                        $('#tblCompraLocalELogs > tbody').append('No hay historial.');
                    }
                } else {
                    $('#tblCompraLocalELogs > tbody').append('No hay historial.');
                }

            }, function (Mensaje) {
                // Error            
                $('#tblCompraLocalELogs > tbody').append('Error.');
            });


        $(obj).removeAttr('disabled');
        $('#MensajeCargando').fadeOut();
        $('#spinnerTitle').css('display', 'none');
    },

        function (CL) {
            $(obj).removeAttr('disabled');
            $('#spinner_Index').css('display', 'none');
            alertify.error("Ha ocurrido un error al intentar cargar la solicitud [" + Id_Comp + "]");
        });
}

function EXEC_spCompraLocal_GetLogs_Ajax(Id_Comp, Id_Cd, CALLBACK_Exito, CALLBACK_Error) {

    $.ajax({
        url: _ApplicationUrl + '/api/CL_Main/GetLogs' +
            '?Id_Comp=' + Id_Comp +
            '&Id_Cd=' + Id_Cd +
            '&LogId=0',
        cache: false,
        type: 'GET',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').appendTo("body").modal();
                _onLoginSuccessful = $.proxy(EXEC_spCompraLocal_GetLogs_Ajax, null, $, Id_Comp, CALLBACK);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        var lst = response.Datos;
        var Mensaje = response.Mensaje;
        var Estado = response.Estado;

        if (Estado == 1) {
            if (CALLBACK_Exito) {
                CALLBACK_Exito(lst);
            }
        }

        if (Estado == -1) {
            CALLBACK_Error(Mensaje);
        }

    }).fail(function (jqXHR, textStatus, error) {
        if (jqXHR.status == 401) {
            alert('La sessión ha expirado.');
            $('#dvModalPropuestaTE_ver2').modal('hide');
            $('#dvDialogoInicioSesion').appendTo("body").modal();
        } else {
            alertify.success('Error: funcion EXEC_spCompraLocal_GetLogs_Ajax.');
        }
    });
}


function Cargar_SolicitudDetalle_Ajax(Id_Comp, Id_Cd, AutorizadorId, CALLBACK, CALLBACK_Error) {

    $.ajax({
        url: _ApplicationUrl + '/api/CL_Main/Get_DetalleCL' +
            '?Id_Comp=' + Id_Comp +
            '&Id_Cd=' + Id_Cd +
            '&AutorizadorId=' + AutorizadorId,
        cache: false,
        type: 'GET',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').appendTo("body").modal();
                _onLoginSuccessful = $.proxy(Get_DetalleCL, null, $, Id_Comp, Id_Cd, AutorizadorId, CALLBACK, CALLBACK_Error);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        var RES = response.Datos;
        var Estado = response.Estado;

        if (Estado == 1) {
            if (CALLBACK) {
                CALLBACK(RES);
            }
        }

        if (Estado == -1) {
            CALLBACK_Error();
        }


    }).fail(function (jqXHR, textStatus, error) {
        if (jqXHR.status == 401) {
            alert('La sessión ha expirado.');
            $('#dvDialogoInicioSesion').appendTo("body").modal();
        } else {
            alertify.success('Error: funcion Get_DetalleCL.');
        }
    });
}

function LlenarPreciosCL(Id_Prd, CALLBACK_Exito) {
    PreciosComprasLocales(Id_Prd, function (Lst) {
    });

    if (CALLBACK_Exito) {
        CALLBACK_Exito();
    }
}

function LlenarClientesExclusivos(Id_Comp, CALLBACK_Exito) {
    DatosClientesExclusivos(Id_Comp, function (Lst) {
    });
    if (CALLBACK_Exito) {
        CALLBACK_Exito();
    }
}

function LlenarClientesExclusivosDetalle(Id_Comp, CALLBACK_Exito) {
    DatosClientesExclusivosDetalle(Id_Comp, function (Lst) {
    });
    if (CALLBACK_Exito) {
        CALLBACK_Exito();
    }
}

function DatosClientesExclusivos(Id_Comp, CALLBACK_Exito) {
    Cargar_DatosClientesExclusivos_Ajax(Id_Comp, function (Lst) {
        $('#tbl_ClientesExclusivos > tbody').empty();

        for (i = 0; i < Lst.length; i++) {
            CONT_ClientesExc = i + 1;
            var row = $('<tr class="" id="Row_ClientesExc_' + CONT_ClientesExc + '"">');
            row.append($('<td class="text-center">').append('<label id="lstCE_TipoCliente_' + CONT_ClientesExc + '">' + Lst[i].TipoCliente + '</label>'));
            row.append($('<td class="text-center">').append('<label id="lstCE_IdCte_' + CONT_ClientesExc + '">' + Lst[i].Id_Cte + '</label>'));
            row.append($('<td class="text-left">').append('<label id="lstCE_Nombre_' + CONT_ClientesExc + '">' + Lst[i].Nombre + '</label>'));


            row.append($('<td class="text-center">').append(
                '<i class="fa fa-times fa-2x clickable" ' +
                'data-row="' + CONT_ClientesExc + '" ' +
                'onclick="btnClienteExc_Remover(this);" ' +
                '>' +
                '</i>'
            ));

            $('#tbl_ClientesExclusivos > tbody').append(row);
        }

    });

}

function DatosClientesExclusivosDetalle(Id_Comp, CALLBACK_Exito) {
    Cargar_DatosClientesExclusivos_Ajax(Id_Comp, function (Lst) {
        $('#tbl_ClientesExc > tbody').empty();

        for (i = 0; i < Lst.length; i++) {
            CONT_ClientesExc = i + 1;
            var row = $('<tr class="" id="Row_ClientesExc_' + CONT_ClientesExc + '"">');
            row.append($('<td class="text-center">').append('<label id="lstCE_IdCte_' + CONT_ClientesExc + '">' + Lst[i].Id_Cte + '</label>'));
            row.append($('<td class="text-left">').append('<label id="lstCE_Nombre_' + CONT_ClientesExc + '">' + Lst[i].Nombre + '</label>'));

            $('#tbl_ClientesExc > tbody').append(row);
        }

    });

}


function Cargar_DatosClientesExclusivos_Ajax(Id_Comp, CALLBACK_Exito, CALLBACK_Error) {

    $.ajax({
        url: _ApplicationUrl + '/api/CL_Main/Get_DatosClientesExclusivosCL' +
            '?Id_Comp=' + Id_Comp,
        cache: false,
        type: 'GET',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').appendTo("body").modal();
                _onLoginSuccessful = $.proxy(Cargar_DatosClientesExclusivos_Ajax, null, $, Id_Comp, CALLBACK_Exito, CALLBACK_Error);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        var RES = response.Datos;
        var Estado = response.Estado;

        if (Estado == 1) {
            if (CALLBACK_Exito) {
                CALLBACK_Exito(RES);
            }
        }

        if (Estado == -1) {
            CALLBACK_Error();
        }


    }).fail(function (jqXHR, textStatus, error) {
        if (jqXHR.status == 401) {
            alert('La sessión ha expirado.');
            $('#dvDialogoInicioSesion').appendTo("body").modal();
        } else {
            alertify.success('Error: funcion Get_DetalleCL.');
        }
    });
}

function Inicializar_Solicitud() {

    $('#tbIdComp').val('');
    $('#tbIdComp').prop('readonly', true);
    $('#tbIdComp').attr("disabled", "disabled");

    $('#tbCLFechaSol').val('');
    $('#tbCLFechaSol').prop('readonly', true);
    $('#tbCLFechaSol').attr("disabled", "disabled");

    $('#tbCLFechaVig').val('');
    $('#tbCLFechaVig').prop('readonly', true);
    $('#tbCLFechaVig').attr("disabled", "disabled");

    $('#lbCLEstatus').text('-');
    //----------------------------
    $('#tbCLId_Cd').text(0);
    $('#tbCLId_Cd').attr("disabled", "disabled");
    $('#tbCLId_Cd').css('display', 'none');
    //----------------------------
    $('#lbCLPermisoAutorizar').text(0);
    $('#lbCLPermisoAutorizar').attr("disabled", "disabled");
    $('#lbCLPermisoAutorizar').css('display', 'none');
    //----------------------------
    $('#tbCLSucursal').text(0);
    $('#tbCLSucursal').prop('readonly', true);
    $('#tbCLSucursal').attr("disabled", "disabled");

    $('#tbCLSolicitante').text(0);
    $('#tbCLSolicitante').prop('readonly', true);
    $('#tbCLSolicitante').attr("disabled", "disabled");

    $('#tbCLMotivoCompra').text(0);
    $('#tbCLMotivoCompra').prop('readonly', true);
    $('#tbCLMotivoCompra').attr("disabled", "disabled");

    $('#tbCLId_Prd').text(0);
    $('#tbCLId_Prd').prop('readonly', true);
    $('#tbCLId_Prd').attr("disabled", "disabled");

    $('#tbCLPrd_Descripcion').text(0);
    $('#tbCLPrd_Descripcion').prop('readonly', true);
    $('#tbCLPrd_Descripcion').attr("disabled", "disabled");

    $('#tbCLCodigoPadre').text(0);
    $('#tbCLCodigoPadre').prop('readonly', true);
    $('#tbCLCodigoPadre').attr("disabled", "disabled");

    $('#tbCLClienteExc').text(0);
    $('#tbCLClienteExc').prop('readonly', true);
    $('#tbCLClienteExc').attr("disabled", "disabled");

    $('#tbCLProvLocal').text(0);
    $('#tbCLProvLocal').prop('readonly', true);
    $('#tbCLProvLocal').attr("disabled", "disabled");

    $('#tbCLTipoProd').text(0);
    $('#tbCLTipoProd').prop('readonly', true);
    $('#tbCLTipoProd').attr("disabled", "disabled");

    $('#tbCLProvCentral').text(0);
    $('#tbCLProvCentral').prop('readonly', true);
    $('#tbCLProvCentral').attr("disabled", "disabled");

    $('#tbCLAplicacion').text(0);
    $('#tbCLAplicacion').prop('readonly', true);
    $('#tbCLAplicacion').attr("disabled", "disabled");

    $('#tbCLMotivo').text(0);
    $('#tbCLMotivo').prop('readonly', true);
    $('#tbCLMotivo').attr("disabled", "disabled");

    $('#tbCLCosto').text(0);
    $('#tbCLCosto').prop('readonly', true);
    $('#tbCLCosto').attr("disabled", "disabled");

    $('#tbCLAAACompraLocal').text(0);
    $('#tbCLAAACompraLocal').prop('readonly', true);
    $('#tbCLAAACompraLocal').attr("disabled", "disabled");

    $('#tbCLAAAKey').text(0);
    $('#tbCLAAAKey').prop('readonly', true);
    $('#tbCLAAAKey').attr("disabled", "disabled");

    $('#tbCLPrecioLista').text(0);
    $('#tbCLPrecioLista').prop('readonly', true);
    $('#tbCLPrecioLista').attr("disabled", "disabled");

    var rowLog = $('<tr>');
    rowLog.append($('<td style="text-align:left;">').append(
        'Vacio.'
    ));
    $('#tblCLLogs > tbody').append(rowLog);

}

function Inicializar_SolicitudEditable() {

    $('#txtMotivoCL').val('');
    $('#txtMotivoCL').prop('readonly', true);
    $('#txtMotivoCL').attr("disabled", "disabled");

    $('#txtCodigoKeyCL').val('');
    $('#txtCodigoKeyCL').prop('readonly', true);
    $('#txtCodigoKeyCL').attr("disabled", "disabled");

    $('#txtCodigoAbastoCL').val('');
    $('#txtCodigoAbastoCL').prop('readonly', true);
    $('#txtCodigoAbastoCL').attr("disabled", "disabled");

    //------------------------------------
    $('#TextId_Prd').val('');
    $('#TextId_Prd').prop('readonly', true);
    $('#TextId_Prd').attr("disabled", "disabled");

    $('#txtCodProd').val('');
    $('#txtCodProd').prop('readonly', true);
    $('#txtCodProd').attr("disabled", "disabled");

    $('#TextPrd_Descrpcion').val('');
    $('#TextPrd_Descrpcion').prop('readonly', true);
    $('#TextPrd_Descrpcion').attr("disabled", "disabled");

    $('#rdpVigencia').val('');
    $('#rdpVigencia').attr("disabled", "disabled");
    $('#rdpVigencia').css('readonly', 'true');

    $('#rdpVigenciaFin').val('');
    $('#rdpVigenciaFin').attr("disabled", "disabled");
    $('#rdpVigenciaFin').css('readonly', 'true');

    $('#cmbPresentacion').val('');
    $('#cmbPresentacion').attr("disabled", "disabled");
    $('#cmbPresentacion').css('readonly', 'true');

    $('#txtTipoProducto').val(0);
    $('#txtTipoProducto').prop('readonly', true);
    $('#txtTipoProducto').attr("disabled", "disabled");

    $('#cmbTipoProducto').val('');
    $('#cmbTipoProducto').attr("disabled", "disabled");
    $('#cmbTipoProducto').css('readonly', 'true');

    $('#cmbFam').val(0);
    $('#cmbFam').prop('readonly', true);
    $('#cmbFam').attr("disabled", "disabled");

    $('#cmbSubFam').val(0);
    $('#cmbSubFam').prop('readonly', true);
    $('#cmbSubFam').attr("disabled", "disabled");

    $('#txtProveedorCentral').val(0);
    $('#txtProveedorCentral').prop('readonly', true);
    $('#txtProveedorCentral').attr("disabled", "disabled");

    $('#txtProveedor').val(0);
    $('#txtProveedor').prop('readonly', true);
    $('#txtProveedor').attr("disabled", "disabled");

    $('#txtCodProveedor').val(0);
    $('#txtCodProveedor').prop('readonly', true);
    $('#txtCodProveedor').attr("disabled", "disabled");

    $('#txtDesProveedor').val(0);
    $('#txtDesProveedor').prop('readonly', true);
    $('#txtDesProveedor').attr("disabled", "disabled");

    $('#cmbPresentacionProv').val(0);
    $('#cmbPresentacionProv').prop('readonly', true);
    $('#cmbPresentacionProv').attr("disabled", "disabled");

    $('#lbl_Val_txtpresproveedor').val(0);
    $('#lbl_Val_txtpresproveedor').prop('readonly', true);
    $('#lbl_Val_txtpresproveedor').attr("disabled", "disabled");

    $('#cmbUentrada').val('');
    $('#cmbUentrada').prop('readonly', true);
    $('#cmbUentrada').attr("disabled", "disabled");

    $('#txtFactorConversion').val(0);
    $('#txtFactorConversion').prop('readonly', true);
    $('#txtFactorConversion').attr("disabled", "disabled");

    $('#cmbUsalida').val('');
    $('#cmbUsalida').prop('readonly', true);
    $('#cmbUsalida').attr("disabled", "disabled");

    $('#txtUempaque').val(0);
    $('#txtUempaque').prop('readonly', true);
    $('#txtUempaque').attr("disabled", "disabled");

    //$('#txtMotivoDesabasto').val('');
    //$('#txtMotivoDesabasto').prop('readonly', true);
    //$('#txtMotivoDesabasto').attr("disabled", "disabled");

    $('#tbCLAplicacion').val(0);
    $('#tbCLAplicacion').prop('readonly', true);
    $('#tbCLAplicacion').attr("disabled", "disabled");

    $('#cmbCausaDesabasto').val(0);
    $('#cmbCausaDesabasto').prop('readonly', true);
    $('#cmbCausaDesabasto').attr("disabled", "disabled");

    $('#tbl_rgPrecios > tbody').empty();

    //$('#cmbUnidadMedidaSATDesabasto').val('');
    //$('#ddlProdServicio_SATDesabasto').val('');

    //LlenarListaPrecios_ComprasLocales(0, function () { });

    var rowLog = $('<tr>');
    rowLog.append($('<td style="text-align:left;">').append(
        'Vacio.'
    ));

    $('#tblCLLogs > tbody').append(rowLog);

}

// Listade de motivos
function ajax_CL_BusquedaProducto(Termino, Spiner, CallBack_Exito) {
    $(Spiner).css('display', 'block');
    $.ajax({
        url: _ApplicationUrl + '/api/CL_Main/Get_BusquedaProducto',
        data: {
            Termino: Termino
        },
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
        var Estado = response.Estado;
        var Datos = response.Datos;

        if (Estado == 1) {
            if (CallBack_Exito) {
                $(Spiner).css('display', 'none');
                CallBack_Exito(Datos);
            }
        } else {
            $(Spiner).css('display', 'none');
            alertify.error('Error Ajax: CL_Ajax.js.ajax_CL_BusquedaProducto');
        }
    }).fail(function (jqXHR, textStatus, error) {
        $(Spiner).css('display', 'none');
        //alertify.error(jqXHR.responseText);
        if (jqXHR.status == 401) {
            $('#dvDialogoInicioSesion').appendTo("body").modal();
        } else {
            alertify.error('Ocurrió una error:' + jqXHR.responseText);
        }
    });
}

function LlenarListaPrecios_ComprasLocales(IdTipoSolicitud, Id_Comp, CALLBACK_Exito) {
    let cmbMotivo = IdTipoSolicitud;
    var Fecha = new Date();
    var FechaFin = new Date();
    var Dia = Fecha.getDate();
    Dia = parseInt(Dia);
    if (Dia <= 9) {
        Dia = '0' + Dia;
    }
    var Mes = 0;
    Mes = Fecha.getMonth() + 1;
    Mes = parseInt(Mes);
    if (Mes <= 9) {
        Mes = '0' + Mes;
    }
    Fecha = Dia + '/' + Mes + '/' + Fecha.getFullYear();

    $('#rdpVigencia').val(Fecha);

    if (cmbMotivo == "1") {
        var FechaFinal = addDaysToDate(FechaFin, 15);
        var DiaFin = FechaFinal.getDate();
        var MesFin = FechaFinal.getMonth() + 1;
        if (DiaFin <= 9) {
            DiaFin = '0' + DiaFin
        }
        if (MesFin <= 9) {
            MesFin = '0' + MesFin
        }


        FechaFin = DiaFin + '/' + MesFin + '/' + FechaFinal.getFullYear();
        $('#rdpVigenciaFin').val(FechaFin);
    } else {
        var Año = FechaFin.getFullYear(FechaFin.getFullYear() + 1);
        Año = Año + 1;
        FechaFin = Dia + '/' + Mes + '/' + Año;
        $('#rdpVigenciaFin').val(FechaFin);
    }

    $('#tbl_rgPrecios > tbody').empty();
    var i = 1;
    var row = $('<tr class="" id="rowPedidoDesabastecido_' + i + '"">');
    //row.append($('<td class="text-left">').append(
    //    '<p id="Prd_FechaInicio_' + i + '">' + Fecha + '</p>'
    //));
    //row.append($('<td class="text-left">').append(
    //    '<p id="Prd_FechaFin_' + i + '">' + Fecha + '</p>'
    //));
    row.append($('<td class="text-right">').append(
        '<p id="Pre_Descripcion_' + i + '">Precio de Lista (Venta)</p>'
    ));
    row.append($('<td>').append('<label id="Pre_Pesos_' + i + '">0</label>'));
    row.append($('<td class="text-center">').append(
        '<i class="fa fa-pencil fa-2x clickable" ' +
        'onclick="PrecioProducto.Editar(this);" ' +
        'data-id=' + i + '>' +
        '</i>'
    ));
    $('#tbl_rgPrecios > tbody').append(row);

    i = 2;
    var row = $('<tr class="" id="rowPedidoDesabastecido_' + i + '"">');
    //row.append($('<td class="text-left">').append(
    //    '<p id="Prd_FechaInicio_' + i + '">' + Fecha + '</p>'
    //));
    //row.append($('<td class="text-left">').append(
    //    '<p id="Prd_FechaFin_' + i + '">' + Fecha + '</p>'
    //));
    row.append($('<td class="text-right">').append(
        '<p id="Pre_Descripcion_' + i + '">Costo</p>'
    ));
    row.append($('<td>').append('<label id="Pre_Pesos_' + i + '">0</label>'));
    row.append($('<td class="text-center">').append(
        '<i class="fa fa-pencil fa-2x clickable" ' +
        'onclick="PrecioProducto.Editar(this);" ' +
        'data-id=' + i + '>' +
        '</i>'
    ));
    $('#tbl_rgPrecios > tbody').append(row);

    i = 3;
    var row = $('<tr class="" id="rowPedidoDesabastecido_' + i + '"">');
    //row.append($('<td class="text-left">').append(
    //    '<p id="Prd_FechaInicio_' + i + '">' + Fecha + '</p>'
    //));
    //row.append($('<td class="text-left">').append(
    //    '<p id="Prd_FechaFin_' + i + '">' + Fecha + '</p>'
    //));
    row.append($('<td class="text-right">').append(
        '<p id="Pre_Descripcion_' + i + '">Precio AAA código compra local</p>'
    ));
    row.append($('<td>').append('<label id="Pre_Pesos_' + i + '">0</label>'));
    //row.append($('<td class="text-center">').append(
    //    '<i class="fa fa-pencil fa-2x clickable" ' +
    //    'onclick="PrecioProducto.Editar(this);" ' +
    //    'data-id=' + i + '>' +
    //    '</i>'
    //));
    $('#tbl_rgPrecios > tbody').append(row);

    if (CALLBACK_Exito) {
        CALLBACK_Exito();
    }
}

// Listade de motivos
function ajax_CL_Motivos(Id_MotivoCL, CallBack_Exito) {
    $.ajax({
        url: _ApplicationUrl + '/api/CL_Main/Get_ListadoMotivo',
        data: {
            Id_MotivoCL: Id_MotivoCL
        },
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
        var Estado = response.Estado;
        var Datos = response.Datos;
        var Mensaje = response.Mensaje;
        if (Estado == 1) {
            if (CallBack_Exito) {
                CallBack_Exito(Datos);
            }
        } else {
            alertify.error(Mensaje);
        }
    }).fail(function (jqXHR, textStatus, error) {
        if (jqXHR.status == 401) {
            $('#dvDialogoInicioSesion').appendTo("body").modal();
        } else {
            alertify.error('Ocurrió una error:' + jqXHR.responseText);
        }
    });
}

// Listade de motivos
function Cargar_cmbMotivos(Id_MotivoCL) {
    ajax_CL_Motivos(0, function (lst) {
        if (lst) {
            $('#cmbCategorias').empty();
            $('#cmbCategorias').append('<option value="0" >-- Seleccione -- </option>');
            for (i = 0; i < lst.length; i++) {
                $('#cmbCategorias').append('<option value="' + lst[i].Id_MotivoCL + '" >' + lst[i].Desc_MotivoCL + '</option>');
            }
        } else {
            alertify.error('Error: La funcion regreso una lista vacia (Cargar_cmbMotivos)');
        }
    });
}

// Unidad
function ajax_CL_Cargar_cmbUnidadEntrada(IdUnidad, CallBack_Exito) {
    $.ajax({
        url: _ApplicationUrl + '/api/CL_Main/spLlenarComboUnidades',
        data: {
            IdUnidad: IdUnidad,
            Id1: 0,
            Id2: 0
        },
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
        var Estado = response.Estado;
        var Datos = response.Datos;
        if (Estado == 1) {
            if (CallBack_Exito) {
                CallBack_Exito(Datos);
            }
        } else {
            alertify.error('Ocurrió una error: ajax_CL_Cargar_cmbUnidadEntrada(229);');
        }
    }).fail(function (jqXHR, textStatus, error) {
        if (jqXHR.status == 401) {
            $('#dvDialogoInicioSesion').appendTo("body").modal();
        } else {
            alertify.error('Ocurrió una error:' + jqXHR.responseText);
        }
    });
}

//  UNIDADES ENTRADA 
function Cargar_cmbUnidadEntrada(IdUnidad = 0, CALLBACK_Exito) {
    ajax_CL_Cargar_cmbUnidadEntrada(IdUnidad, function (lst) {
        $('#cmbUentrada').empty();
        $('#cmbUentrada').append('<option value="0" >-- Seleccione -- </option>');
        for (i = 0; i < lst.length; i++) {
            $('#cmbUentrada').append('<option value="' + lst[i].IdCodigo + '" >' + lst[i].Descripcion + '</option>');
        }
        if (CALLBACK_Exito) {
            CALLBACK_Exito();
        }
    });
}

//  UNIDADES SALIDA
function Cargar_cmbUnidadSalida(IdUnidad = 0, CALLBACK_Exito) {
    ajax_CL_Cargar_cmbUnidadEntrada(IdUnidad, function (lst) {
        $('#cmbUsalida').empty();
        $('#cmbUsalida').append('<option value="0" >-- Seleccione -- </option>');
        for (i = 0; i < lst.length; i++) {
            $('#cmbUsalida').append('<option value="' + lst[i].IdCodigo + '" >' + lst[i].Descripcion + '</option>');
        }
        if (CALLBACK_Exito) {
            CALLBACK_Exito();
        }
    });
}

function ajax_CL_Cargar_AABuscaProductosCompraLocalTodos(Id_Prd, CallBack_Exito, CALLBACK_Error) {
    $.ajax({
        url: _ApplicationUrl + '/api/CL_Main/CL_spAABuscaProductosCompraLocalTodos',
        data: {
            Id_Prd: Id_Prd,
            Param1: 0,
            Param2: 0
        },
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
        var Estado = response.Estado;
        var Datos = response.Datos;

        if (Estado == 1) {
            if (CallBack_Exito) {
                CallBack_Exito(Datos);
            }
        } else {
            if (CALLBACK_Error) {
                CALLBACK_Error();
            } else {
                alertify.error('Ocurrió una error: ajax_CL_Cargar_AABuscaProductosCompraLocalTodos(292)');
            }
        }
    }).fail(function (jqXHR, textStatus, error) {
        if (CALLBACK_Error) {
            CALLBACK_Error();
        }
        if (jqXHR.status == 401) {
            $('#dvDialogoInicioSesion').appendTo("body").modal();
        } else {
            alertify.error('Ocurrió una error:' + jqXHR.responseText);
        }
    });
}

function Cargar_cmb_AABuscaProductosCompraLocalTodos(Id_Prd = 0, CALLBACK_Exito) {
    ajax_CL_Cargar_AABuscaProductosCompraLocalTodos(Id_Prd, function (lst) {
        $('#cmbProductosHabiliCompraLocal').empty();
        $('#cmbProductosHabiliCompraLocal').append('<option value="0" >-- Seleccione -- </option>');
        for (i = 0; i < lst.length; i++) {
            $('#cmbProductosHabiliCompraLocal').append('<option value="' + lst[i]._Id_Prd + '" >' + lst[i]._Id_Prd + ' ' + lst[i]._Prd_Descripcion + '</option>');
        }
        if (CALLBACK_Exito) {
            CALLBACK_Exito();
        }
    });
}

function ajax_CL_Cargar_UnindadMedidaSAT(CveUnidad, CallBack_Exito) {
    $.ajax({
        url: _ApplicationUrl + '/api/CL_Main/spSATUnidadesMedida',
        data: {
            CveUnidad: CveUnidad
        },
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
        var Estado = response.Estado;
        var Datos = response.Datos;
        if (Estado == 1) {
            if (CallBack_Exito) {
                CallBack_Exito(Datos);
            }
        } else {
            alertify.error('Error en CL_Ajax.ajax_CL_Cargar_UnindadMedidaSAT(335)');
        }
    }).fail(function (jqXHR, textStatus, error) {
        //alertify.error(jqXHR.responseText);
        if (jqXHR.status == 401) {
            $('#dvDialogoInicioSesion').appendTo("body").modal();
        } else {
            alertify.error('Ocurrió una error:' + jqXHR.responseText);
        }
    });
}

//  SAT UNIDADES
function Cargar_cbmUnindadMedidaSAT(CveUnidad = null, CALLBACK_Exito) {
    ajax_CL_Cargar_UnindadMedidaSAT(CveUnidad, function (lst) {
        $('#cmbUnidadMedidaSATDesabasto').empty();
        $('#cmbUnidadMedidaSATDesabasto').append('<option value="" >-- Seleccione -- </option>');
        for (i = 0; i < lst.length; i++) {
            $('#cmbUnidadMedidaSATDesabasto').append('<option value="' + lst[i].CveUnidad + '" >' + lst[i].DescUnidad + '</option>');
        }
        if (CALLBACK_Exito) {
            CALLBACK_Exito();
        }
    });
}

function ajax_CL_Cargar_SATProductosServicios(CveProdServ, CallBack_Exito) {
    $.ajax({
        url: _ApplicationUrl + '/api/CL_Main/spSATProductosYServicios',
        data: {
            CveProdServ: CveProdServ
        },
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
        var Estado = response.Estado;
        var Datos = response.Datos;
        if (Estado == 1) {
            if (CallBack_Exito) {
                CallBack_Exito(Datos);
            }
        } else {
            alertify.error('Ocurrió una error: ajax_CL_Cargar_SATProductosServicios(379)');
        }
    }).fail(function (jqXHR, textStatus, error) {
        if (jqXHR.status == 401) {
            $('#dvDialogoInicioSesion').appendTo("body").modal();
        } else {
            alertify.error('Ocurrió una error:' + jqXHR.responseText);
        }
    });
}

//  SAT Productos y Servicios
function Cargar_cmbSATProductosServicios(CveProdServ = null, CALLBACK_Exito) {
    ajax_CL_Cargar_SATProductosServicios(CveProdServ, function (lst) {
        $('#ddlProdServicio_SATDesabasto').empty();
        $('#ddlProdServicio_SATDesabasto').append('<option value="" >-- Seleccione -- </option>');
        for (i = 0; i < lst.length; i++) {
            $('#ddlProdServicio_SATDesabasto').append('<option value="' + lst[i].CveProdServ + '" >' + lst[i].DescProdServ + '</option>');
        }
        if (CALLBACK_Exito) {
            CALLBACK_Exito();
        }
    });
}

// Listade de motivos
function ajax_CL_CausaDesabasto(Id_Causa, CallBack_Exito) {
    $.ajax({
        url: _ApplicationUrl + '/api/CL_Main/Get_ListadoCausaDesabasto',
        data: {
            Id_Causa: Id_Causa
        },
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
        var Estado = response.Estado;
        var Datos = response.Datos;
        if (Estado == 1) {
            if (CallBack_Exito) {
                CallBack_Exito(Datos);
            }
        } else {
            alertify.error('Error CL_Ajax.js.ajax_CL_CausaDesabasto');
        }
    }).fail(function (jqXHR, textStatus, error) {
        //alertify.error(jqXHR.responseText);
        if (jqXHR.status == 401) {
            $('#dvDialogoInicioSesion').appendTo("body").modal();
        } else {
            alertify.error('Ocurrió una error:' + jqXHR.responseText);
        }
    });
}

// Listade de motivos         
function Cargar_cmbCausaDesabasto(Id_Causa = 0, CALLBACK_Exito) {
    ajax_CL_CausaDesabasto(0, function (lst) {
        $('#cmbCausaDesabasto').empty();
        $('#cmbCausaDesabasto').append('<option value="0" >-- Seleccione -- </option>');
        for (i = 0; i < lst.length; i++) {
            $('#cmbCausaDesabasto').append('<option value="' + lst[i].Id + '" >' + lst[i].Descripcion + '</option>');
        }

        if (CALLBACK_Exito) {
            CALLBACK_Exito();
        }
    });
}

// Listade de motivos
function ajax_CL_TipoProducto(Id_TipoProducto, CallBack_Exito) {
    $.ajax({
        url: _ApplicationUrl + '/api/CL_Main/Get_ListadoTipoProducto',
        data: {
            Id_TipoProducto: Id_TipoProducto,
            Id_Emp: 0
        },
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
        var Estado = response.Estado;
        var Datos = response.Datos;
        if (Estado == 1) {
            if (CallBack_Exito) {
                CallBack_Exito(Datos);
            }
        } else {
            alertify.error('Ocurrió una error: ajax_CL_TipoProducto');
        }
    }).fail(function (jqXHR, textStatus, error) {
        if (jqXHR.status == 401) {
            $('#dvDialogoInicioSesion').appendTo("body").modal();
        } else {
            alertify.error('Ocurrió una error:' + jqXHR.responseText);
        }
    });
}

// Listade de motivos
function Cargar_cmbTipoProducto(Id_TipoProducto = 0, CALLBACK_Exito) {
    ajax_CL_TipoProducto(0, function (lst) {
        $('#cmbTipoProducto').empty();
        //$('#cmbTipoProducto').append('<option value="0" >-- Seleccione -- </option>');
        for (i = 0; i < lst.length; i++) {
            $('#cmbTipoProducto').append('<option value="' + lst[i].Id + '" >' + lst[i].Descripcion + '</option>');
        }
        if (CALLBACK_Exito) {
            CALLBACK_Exito();
        }
    });
}

function Cargar_cmbProductoFamiliaCte(Id_Fam_Default, Id_ProductoFamiliaCte, CALLBAK_Exito) {
    CL_Ajax.ProductoFamiliaCte(0, function (lst) {
        $('#cmbFam').empty();
        $('#cmbFam').append('<option value="0" >-- Seleccione -- </option>');
        var EstaEn_Lista = false;
        //console.log('Id_Fam_Default:'+Id_Fam_Default);
        //console.log(lst);
        for (i = 0; i < lst.length; i++) {
            $('#cmbFam').append('<option value="' + lst[i].Id + '" >' + lst[i].Descripcion + '</option>');
            if (Id_Fam_Default == lst[i].Id) {
                EstaEn_Lista = true;
                console.log('lst[i].Id:' + lst[i].Id);
            }
        }
        // Al inicio
        $('#cmbFam').val(0);
        if (EstaEn_Lista) {
            $('#cmbFam').val(Id_Fam_Default);
        }
        var Id_Familia = $('#cmbFam').val();

        if (CALLBAK_Exito) {
            CALLBAK_Exito(Id_Familia);
        }

    });
}

var CL_Ajax = {
    // Valida el Codigo. Solo debe existir uno
    //Ajax_spCL_ConsultarProducto: function (Id_Prd, CALLBACK_Exito) {
    ConsultarCodigo: function (Id_Prd, CALLBACK_Exito) {
        $.ajax({
            url: _ApplicationUrl + '/api/CL_Main/spCL_ConsultarProducto',
            data: {
                CodigoProducto: Id_Prd,
                Param2: 0
            },
            cache: false,
            type: 'GET',
        }).done(function (response, textStatus, jqXHR) {
            let Estado = response.Estado;
            let Datos = response.Datos;
            if (Estado == 1) {
                if (CALLBACK_Exito) {
                    CALLBACK_Exito(Datos);
                }
            } else {
                alertify.error(Mensaje);
            }
        }).fail(function (jqXHR, textStatus, error) {
            if (jqXHR.status == 401) {
                $('#dvDialogoInicioSesion').appendTo("body").modal();
            } else {
                alertify.error('Ocurrió una error:' + jqXHR.responseText);
            }
        });
    },

    // Listade de motivos
    ProductoSubFamiliaCte: function (Id_Familia, CallBack_Exito) {
        $.ajax({
            //url: _ApplicationUrl + '/api/CL_Main/SelCL_ListadoProductoSubFamilia',
            url: _ApplicationUrl + '/api/CL_Main/Get_ListadoProductoSubFamilia',
            data: {
                Id1: 0,
                Id2: 0,
                Id3: Id_Familia
            },
            cache: false,
            type: 'GET',
            async: false,
        }).done(function (response, textStatus, jqXHR) {
            var Estado = response.Estado;
            var Datos = response.Datos;

            if (CallBack_Exito) {
                CallBack_Exito(Datos);
            }
        }).fail(function (jqXHR, textStatus, error) {
            //alertify.error(jqXHR.responseText);
            if (jqXHR.status == 401) {
                $('#dvDialogoInicioSesion').appendTo("body").modal();
            } else {
                alertify.error('Ocurrió una error:' + jqXHR.responseText);
            }
        });
    },

    // Listade de motivos
    ProductoFamiliaCte: function (Id_ProductoFamiliaCte, CallBack_Exito) {
        $.ajax({
            url: _ApplicationUrl + '/api/CL_Main/Get_ListadoProductoFamiliaCte',
            data: {
                Id_ProductoFamiliaCte: Id_ProductoFamiliaCte,
                Id2: 0
            },
            cache: false,
            type: 'GET',
            async: false,
        }).done(function (response, textStatus, jqXHR) {
            var Estado = response.Estado;
            var Datos = response.Datos;

            if (Estado == 1) {
                if (CallBack_Exito) {
                    CallBack_Exito(Datos);
                }
            } else {
                alertify.error('Ocurrió una error: CL_Ajax.ProductoFamiliaCte()');
            }
        }).fail(function (jqXHR, textStatus, error) {
            //alertify.error(jqXHR.responseText);
            if (jqXHR.status == 401) {
                $('#dvDialogoInicioSesion').appendTo("body").modal();
            } else {
                alertify.error('Ocurrió una error:' + jqXHR.responseText);
            }
        });
    },

    // Listade de motivos
    CargaComob_SubFamilia: function (Id_Familia, CallBack_Exito, CALLBACK_Error) {
        $.ajax({
            url: _ApplicationUrl + '/api/CL_Main/Get_ListadoProductoSubFamilia',
            data: {
                Id1: 0,
                Id2: 0,
                Id3: Id_Familia
            },
            cache: false,
            type: 'GET',
        }).done(function (response, textStatus, jqXHR) {
            var Estado = response.Estado;
            var Datos = response.Datos;
            if (Estado == 1) {
                if (CallBack_Exito) {
                    CallBack_Exito(Datos);
                }
            } else {
                alertify.error('Ocurrió una error: CargaComob_SubFamilia(642)');
            }
        }).fail(function (jqXHR, textStatus, error) {
            //alertify.error(jqXHR.responseText);
            if (jqXHR.status == 401) {
                if (CALLBACK_Error) {
                    CALLBACK_Error();
                }
                $('#dvDialogoInicioSesion').appendTo("body").modal();
            } else {
                alertify.error('Ocurrió una error:' + jqXHR.responseText);
                if (CALLBACK_Error) {
                    CALLBACK_Error();
                }
            }
        });
    }

}

function Cargar_cmbProductoSubFamilia(Id_Familia, CALLBAK_Exito) {
    CL_Ajax.ProductoSubFamiliaCte(Id_Familia, function (lst) {
        $('#cmbSubFam').empty();
        //$('#cmbTipoProducto').empty();
        $('#cmbSubFam').append('<option value="0" >-- Seleccione -- </option>');
        for (i = 0; i < lst.length; i++) {
            $('#cmbSubFam').append('<option value="' + lst[i].Id + '" >' + lst[i].Descripcion + '</option>');
        }
        $('#cmbSubFam').val('0');
        if (CALLBAK_Exito) {
            CALLBAK_Exito();
        }
    });
}

//Consulta compra local a editar
//RBM Enero 2024
function CompraLocal_Consultar_Ajax(Id_Prd, CALLBACK_Exito) {
    $.ajax({
        url: _ApplicationUrl + '/api/CL_Main/Get_ConsultaCompraLocalById',
        data: {
            Id_Prd: Id_Prd,
            Catalogo: true
        },
        cache: false,
        type: 'GET',
        async: false,
    }).done(function (response, textStatus, jqXHR) {
        var Estado = response.Estado;
        var Datos = response.Datos;
        if (CALLBACK_Exito) {
            CALLBACK_Exito(Datos);

        }
    }).fail(function (jqXHR, textStatus, error) {
        //alertify.error(jqXHR.responseText);
        if (jqXHR.status == 401) {
            $('#dvDialogoInicioSesion').appendTo("body").modal();
        } else {
            alertify.error('Ocurrió una error:' + jqXHR.responseText);
        }
    });
}


// Listade de motivos
function Producto_Consultar_Ajax(Id_Prd, CALLBACK_Exito) {
    $.ajax({
        url: _ApplicationUrl + '/api/CL_Main/Get_ConsultaProductoById',
        data: {
            Id_Prd: Id_Prd,
            Catalogo: true
        },
        cache: false,
        type: 'GET',
        async: false,
    }).done(function (response, textStatus, jqXHR) {
        var Estado = response.Estado;
        var Datos = response.Datos;
        if (CALLBACK_Exito) {
            CALLBACK_Exito(Datos);

        }
    }).fail(function (jqXHR, textStatus, error) {
        //alertify.error(jqXHR.responseText);
        if (jqXHR.status == 401) {
            $('#dvDialogoInicioSesion').appendTo("body").modal();
        } else {
            alertify.error('Ocurrió una error:' + jqXHR.responseText);
        }
    });
}

function ajax_MaximoId(IdProd, Categoria, Proveedor, CALLBACK_Exito) {
    $.ajax({
        url: _ApplicationUrl + '/api/CL_Main/Get_MaximoId',
        data: {
            IdProd: IdProd,
            Categoria: Categoria,
            Proveedor: Proveedor
        },
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
        var Estado = response.Estado;
        var Datos = response.Datos;
        if (CALLBACK_Exito) {
            CALLBACK_Exito(Datos);
        }
    }).fail(function (jqXHR, textStatus, error) {
        //alertify.error(jqXHR.responseText);
        if (jqXHR.status == 401) {
            $('#dvDialogoInicioSesion').appendTo("body").modal();
        } else {
            alertify.error('Ocurrió una error:' + jqXHR.responseText);
        }
    });
}

function ajax_spCompraLocalPedidosProducto_Lista(Solicitud, Id_Producto, CALLBACK_Exito) {
}

var Pedido = {

    spCL_PedidosProducto_Lista: function (Solicitud, Id_Producto, CALLBACK_Exito) {
        $.ajax({
            url: _ApplicationUrl + '/api/CL_Main/spCL_PedidosProducto_Lista',
            data: {
                Solicitud: Solicitud,
                Id_Prd: Id_Producto
            },
            cache: false,
            type: 'GET'
        }).done(function (response, textStatus, jqXHR) {
            var Estado = response.Estado;
            var Datos = response.Datos;
            if (Estado == 1) {
                if (CALLBACK_Exito) {
                    CALLBACK_Exito(Datos);
                }
            } else {
                alertify.error(Mensaje);
            }
        }).fail(function (jqXHR, textStatus, error) {
            Spinner.css('display', 'none');
            if (jqXHR.status == 401) {
                $('#dvDialogoInicioSesion').appendTo("body").modal();
            } else {
                alertify.error('Ocurrió una error:' + jqXHR.responseText);
            }
        });
    },

    LlenarListaPedidos_Desabastecido: function (Solicitud, Id_Producto) {

        Pedido.spCL_PedidosProducto_Lista(Solicitud, Id_Producto, function (Lst) {
            //console.log(Lst);
            $('#hfCont_PedidoDesabastecido').val(Lst.length);
            $('#tblPedidoDesabastecido > tbody').empty();
            for (i = 0; i < Lst.length; i++) {
                var row = $('<tr class="" id="rowPedidoDesabastecido_' + Lst[i].Id + '"">');
                row.append($('<td>').append(
                    '<input type="hidden" id="rowPedido_' + i + '" value="' + Lst[i].Id + '" />' +
                    '<input type="checkbox" id="chb_Pedido_' + Lst[i].Id + '" class="form-control" data-id="' + Lst[i].Id + '" />'
                ));
                row.append($('<td>').append(
                    Lst[i].Id
                ));
                row.append($('<td>').append(
                    Lst[i].Descripcion
                ));
                $('#tblPedidoDesabastecido > tbody').append(row);
            }
            //if (Lst.length <= 0) {
            //    alertify.error("No se encontraron ordenes de compra de Los ultimos 3 meses con el producto, favor de seleccionar otra causa del desabasto.");
            //    $('#cmbCausaDesabasto').val(0);
            //}
        });

    }

}

function ajax_PreciosComprasLocales(Id_Prd, CALLBACK_Exito) {
    $.ajax({
        url: _ApplicationUrl + '/api/CL_Main/spCL_PreciosComprasLocales',
        data: {
            Id_Prd: Id_Prd,
            Param2: 0
        },
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
        var Estado = response.Estado;
        var Datos = response.Datos;
        if (Estado == 1) {

            if (CALLBACK_Exito) {
                CALLBACK_Exito(Datos);
            }
        } else {
            alertify.error(Mensaje);
        }
    }).fail(function (jqXHR, textStatus, error) {
        //$(Spinner).css('display', 'none');
        if (jqXHR.status == 401) {
            $('#dvDialogoInicioSesion').appendTo("body").modal();
        } else {
            alertify.error('Ocurrió una error:' + jqXHR.responseText);
        }
    });
}
//RBMQuitar comantario
function ajax_spProductoConsultaPrecios(Id_Producto, CALLBACK_Exito) {
    $.ajax({
        url: _ApplicationUrl + '/api/CL_Main/spCL_ProductoConsultaPrecios',
        data: {
            Id_Producto: Id_Producto,
            Param2: 0
        },
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
        var Estado = response.Estado;
        var Datos = response.Datos;
        if (Estado == 1) {

            if (CALLBACK_Exito) {
                CALLBACK_Exito(Datos);
            }
        } else {
            alertify.error(Mensaje);
        }
    }).fail(function (jqXHR, textStatus, error) {
        //$(Spinner).css('display', 'none');
        if (jqXHR.status == 401) {
            $('#dvDialogoInicioSesion').appendTo("body").modal();
        } else {
            alertify.error('Ocurrió una error:' + jqXHR.responseText);
        }
    });
}

function PreciosComprasLocales(Id_Prd, CALLBACK_Exito) {
    ajax_PreciosComprasLocales(Id_Prd, function (Lst) {
        $('#tbl_rgPrecios > tbody').empty();
        var cmbCategorias = $('#cmbCategorias').val();
        for (i = 0; i < Lst.length; i++) {
            var row = $('<tr class="" id="rowPedidoDesabastecido_' + Lst[i].Id + '"">');
            row.append($('<td class="text-left">').append(
                '<p id="Pre_Descripcion_' + i + '">' + Lst[i]._Pre_Descripcion + '</p>'
            ));
            row.append($('<td class="text-right">').append(
                '<input type="hidden" id="Prd_Actual_' + i + '" value="' + Lst[i]._Prd_Pesos + '" >' +
                '<p id="Pre_Pesos_' + i + '">' + Lst[i]._Prd_Pesos + '</p>'
            ));
            if (Lst[i]._Pre_Descripcion == "Costo") {
                row.append($('<td class="text-center">').append(
                    '<i class="fa fa-pencil fa-2x clickable" ' +
                    'onclick="PrecioProducto.Editar(this);" ' +
                    'data-id=' + i + '>' +
                    '</i>'
                ));
            }
            if (cmbCategorias == 3) {
                if (Lst[i]._Pre_Descripcion == "Precio de Lista") {
                    row.append($('<td class="text-center">').append(
                        '<i class="fa fa-pencil fa-2x clickable" ' +
                        'onclick="PrecioProducto.Editar(this);" ' +
                        'data-id=' + i + '>' +
                        '</i>'
                    ));
                }
            }
            $('#tbl_rgPrecios > tbody').append(row);
        }

    });

}
//         
function ProductoConsultaPrecios(Id_Producto, CALLBACK_Exito) {

    ajax_spProductoConsultaPrecios(Id_Producto, function (Lst) {
        //console.log(Lst);
        //$('#hfCont_PedidoDesabastecido').val(Lst.length);
        $('#tbl_rgPrecios > tbody').empty();

        for (i = 0; i < Lst.length; i++) {
            var row = $('<tr class="" id="rowPedidoDesabastecido_' + Lst[i].Id + '"">');
            row.append($('<td class="text-left">').append(
                '<p id="Pre_Descripcion_' + i + '">' + Lst[i]._Pre_Descripcion + '</p>'
            ));
            row.append($('<td class="text-right">').append(
                '<input type="hidden" id="Prd_Actual_' + i + '" value="' + Lst[i]._Prd_Pesos + '" >' +
                '<p id="Pre_Pesos_' + i + '">' + Lst[i]._Prd_Pesos + '</p>'
            ));
            if (Lst[i]._Pre_Descripcion == "Costo") {
                row.append($('<td class="text-center">').append(
                    '<i class="fa fa-pencil fa-2x clickable" ' +
                    'onclick="PrecioProducto.Editar(this);" ' +
                    'data-id=' + i + '>' +
                    '</i>'
                ));
                //} else {
                //    //row.append($('<td>').append(                    
                //    row.append($('<td class="text-center">').append(
                //        '<i class="fa fa-pencil fa-2x clickable" ' +
                //        'onclick="PrecioProducto.Editar(this);" ' +
                //        'data-id=' + i + '>' +
                //        '</i>'
                //    ));
            }
            $('#tbl_rgPrecios > tbody').append(row);
        }

    });

}

//
function ajaxCL_GuardarProducto(producto, CALLBACK_Exito) {
    $.ajax({
        url: _ApplicationUrl + '/api/CL_Main/spCL_PedidosProducto_Lista',
        data: {
            producto: producto
        },
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
        var Estado = response.Estado;
        var Datos = response.Datos;
        if (Estado == 1) {
            if (CALLBACK_Exito) {
                CALLBACK_Exito(Datos);
            }
        } else {
            alertify.error(Mensaje);
        }

    }).fail(function (jqXHR, textStatus, error) {
        Spinner.css('display', 'none');
        if (jqXHR.status == 401) {
            $('#dvDialogoInicioSesion').appendTo("body").modal();
        } else {
            alertify.error('Ocurrió una error:' + jqXHR.responseText);
        }

    });
}

//         
function ajax_ChecarProductoYaSolicitado(CodigoUsadoProd, Spiner, CALLBACK_Exito) {
    $(Spiner).css('display', 'block');
    $.ajax({
        url: _ApplicationUrl + '/api/CL_Main/CL_ChecarProductoYaSolicitado',
        data: {
            CodigoUsadoProd: CodigoUsadoProd,
            Param2: 0
        },
        cache: false,
        type: 'GET',
        //async: false,
    }).done(function (response, textStatus, jqXHR) {
        var Estado = response.Estado;
        var Datos = response.Datos;
        if (Estado == 1) {
            if (CALLBACK_Exito) {
                $(Spiner).css('display', 'none');
                CALLBACK_Exito(Datos);
            }
        } else {
            $(Spiner).css('display', 'none');
            alertify.error(Mensaje);
        }
    }).fail(function (jqXHR, textStatus, error) {
        $(Spiner).css('display', 'none');
        if (jqXHR.status == 401) {
            $('#dvDialogoInicioSesion').appendTo("body").modal();
        } else {
            alertify.error('Ocurrió una error:' + jqXHR.responseText);
        }
    });
}


function ajax_LlenarProdcutosHermanos(Producccto, CALLBACK_Exito) {
    $.ajax({
        url: _ApplicationUrl + '/api/CL_Main/CL_LlenarProdcutosHermanos',
        data: {
            Producccto: Producccto,
            Param1: 0,
            Param2: 0
        },
        cache: false,
        type: 'GET',
        async: false,
    }).done(function (response, textStatus, jqXHR) {
        var Estado = response.Estado;
        var Datos = response.Datos;
        if (Estado == 1) {
            if (CALLBACK_Exito) {
                CALLBACK_Exito(Datos);
            }
        } else {
            alertify.error(Mensaje);
        }
    }).fail(function (jqXHR, textStatus, error) {
        if (jqXHR.status == 401) {
            $('#dvDialogoInicioSesion').appendTo("body").modal();
        } else {
            alertify.error('Ocurrió una error:' + jqXHR.responseText);
        }
    });
}

function AJAX_INSERTARPRODUCTO_CL(Producto, Precios, CALLBACK_Exito, CALLBACK_Error) {
    var producto_JOSN = JSON.stringify(Producto);
    $.ajax({
        url: _ApplicationUrl + '/api/CL_Main/CL_InsertarProducto',
        data: producto_JOSN,
        cache: false,
        type: 'PUT',
        //async: false,
        contentType: "application/json; utf-8",
        dataType: "json",
    }).done(function (response, textStatus, jqXHR) {
        var Estado = response.Estado;
        var Datos = response.Datos;
        var Mensaje = response.Mensaje;
        if (Estado == 1) {
            if (CALLBACK_Exito) {
                CALLBACK_Exito(Datos);
            }
        } else {
            //alertify.error(Mensaje);
            if (CALLBACK_Error) {
                CALLBACK_Error(Mensaje);
            }
        }
    }).fail(function (jqXHR, textStatus, error) {
        console.log('ERROR(1118):' + jqXHR.responseText);
        if (jqXHR.status == 401) {
            $('#dvDialogoInicioSesion').appendTo("body").modal();
        } else {
            alertify.error('Ocurrió una error:' + jqXHR.responseText);
        }
    });
}

function AJAX_CL_GRABATIPOCOMPRALOCAL(Id_Solicitud, Id_TipoCompra, Comentario, CALLBACK_Exito) {

    $.ajax({
        url: _ApplicationUrl + '/api/CL_Main/CL_GrabaTipoCompraLocal',
        data: {
            Id_Solicitud: Id_Solicitud,
            Id_TipoCompra: Id_TipoCompra,
            Comentario: Comentario
        },
        cache: false,
        type: 'GET',
        async: false,
    }).done(function (response, textStatus, jqXHR) {
        var Estado = response.Estado;
        var Datos = response.Datos;
        var Mensaje = response.Mensaje;

        if (Estado == 1) {
            if (CALLBACK_Exito) {
                CALLBACK_Exito(Datos);
            }
        } else {
            alertify.error(Mensaje);
        }
    }).fail(function (jqXHR, textStatus, error) {

        if (jqXHR.status == 401) {
            $('#dvDialogoInicioSesion').appendTo("body").modal();
        } else {
            alertify.error('Ocurrió una error:' + jqXHR.responseText);
        }
    });
}

function AJAX_CL_GRABASOLOCOMENTARIOSCLIENTE(Id_Solicitud, Comentario, CALLBACK_Exito) {
    $.ajax({
        url: _ApplicationUrl + '/api/CL_Main/CL_GrabaSoloComentariosCliente',
        data: {
            Id_Solicitud: Id_Solicitud,
            Comentario: Comentario
        },
        cache: false,
        type: 'GET',
        //async: false,
    }).done(function (response, textStatus, jqXHR) {
        var Estado = response.Estado;
        var Datos = response.Datos;
        var Mensaje = response.Mensaje;

        if (Estado == 1) {
            if (CALLBACK_Exito) {
                CALLBACK_Exito(Datos);
            }
        } else {
            alertify.error(Mensaje + '(1181)');
        }
    }).fail(function (jqXHR, textStatus, error) {
        if (jqXHR.status == 401) {
            $('#dvDialogoInicioSesion').appendTo("body").modal();
        } else {
            alertify.error('Ocurrió una error:' + jqXHR.responseText);
        }
    });
}


//Actualiza Precios de compra local
function AJAX_CL_UpdatePreciosCompraLocal(Id_Cd, Id_Prd, Id_Pre, Prd_FechaInicio, Prd_FechaFin, Prd_Pesos, CALLBACK_Exito) {
    $.ajax({
        url: _ApplicationUrl + '/api/CL_Main/CL_UpdatePreciosCompraLocal',
        data: {
            Id_Cd: Id_Cd,
            Id_Prd: Id_Prd,
            Id_Pre: Id_Pre,
            Prd_FechaInicio: Prd_FechaInicio,
            Prd_FechaFin: Prd_FechaFin,
            Prd_Pesos: Prd_Pesos,
        },
        cache: false,
        type: 'GET',
        async: false,
    }).done(function (response, textStatus, jqXHR) {
        var Estado = response.Estado;
        var Datos = response.Datos;
        var Mensaje = response.Mensaje;

        if (Estado == 1) {
            if (CALLBACK_Exito) {
                CALLBACK_Exito(Datos);
            }
        } else {
            alertify.error(Mensaje);
        }
    }).fail(function (jqXHR, textStatus, error) {
        console.log('ERRO: ' + jqXHR.responseText);
        if (jqXHR.status == 401) {
            $('#dvDialogoInicioSesion').appendTo("body").modal();
        } else {
            alertify.error('Ocurrió una error:' + jqXHR.responseText);
        }
    });
}

// Inserta Cliente Exclusivo
function AJAX_CL_InsertClienteExclusivo(IdCte, Nombre, Id_Sol, TipoCliente, CALLBACK_Exito) {
    $.ajax({
        url: _ApplicationUrl + '/api/CL_Main/CL_InsertClienteExclusivo',
        data: {
            IdCte: IdCte,
            Nombre: Nombre,
            Id_Sol: Id_Sol,
            TipoCliente: TipoCliente
        },
        cache: false,
        type: 'GET',
        async: false,
    }).done(function (response, textStatus, jqXHR) {
        var Estado = response.Estado;
        var Datos = response.Datos;
        var Mensaje = response.Mensaje;

        if (Estado == 1) {
            if (CALLBACK_Exito) {
                CALLBACK_Exito(Datos);
            }
        } else {
            alertify.error(Mensaje);
        }
    }).fail(function (jqXHR, textStatus, error) {
        console.log('ERRO: ' + jqXHR.responseText);
        if (jqXHR.status == 401) {
            $('#dvDialogoInicioSesion').appendTo("body").modal();
        } else {
            alertify.error('Ocurrió una error:' + jqXHR.responseText);
        }
    });
}

// Actualiza Listado Clientes Exclusivo con Id_Sol
function AJAX_CL_InsertClienteExclusivo_UpdateSol(
    KeyArray_ClienteExclusivos, Id_Solicitud, CALLBACK_Exito) {
    $.ajax({
        url: _ApplicationUrl + '/api/CL_Main/CL_InsertClienteExclusivo_UpdateSol',
        data: {
            KeyArray_ClienteExclusivos: KeyArray_ClienteExclusivos,
            Id_Solicitud: Id_Solicitud
        },
        cache: false,
        type: 'GET',
        async: false,
    }).done(function (response, textStatus, jqXHR) {
        var Estado = response.Estado;
        var Datos = response.Datos;
        var Mensaje = response.Mensaje;

        if (Estado == 1) {
            if (CALLBACK_Exito) {
                CALLBACK_Exito(Datos);
            }
        } else {
            alertify.error(Mensaje);
        }
    }).fail(function (jqXHR, textStatus, error) {
        console.log('ERRO: ' + jqXHR.responseText);
        if (jqXHR.status == 401) {
            $('#dvDialogoInicioSesion').appendTo("body").modal();
        } else {
            alertify.error('Ocurrió una error:' + jqXHR.responseText);
        }
    });
}

function AJAX_INSERTARSOLICITUD(
    Motivo,
    Id_PrdOriginal,
    Id_Prd,
    Det_Costo,
    Det_Estatus,
    Accion,
    Prd_Descripcion,
    IdTipoProducto,
    TipoProducto,
    Familia, //10
    SubFamilia,
    Vigencia,
    IdCausaDesabasto,
    MotivoDesabasto,
    SAT_CveUnidad,
    SAT_CveProdServ,
    IdProveedor,
    ProviderName,
    IdArray_ClientesExclusivos,
    PedidoReferencia, //20
    IdAplicacion,  // 21 
    ProveedorCentral,
    CodigoProductoProv,
    DescripcionProductoProv,
    PresentacionProductoProv,
    CALLBACK_Exito,
    CALLBAK_Error) {

    //var producto_JOSN = JSON.stringify(Producto);
    $.ajax({
        url: _ApplicationUrl + '/api/CL_Main/InsertarSolicitud_CL',
        data: {
            IdMotivo: Motivo,
            Id_PrdOriginal: Id_PrdOriginal,
            Id_Prd: Id_Prd,
            Det_Costo: Det_Costo,
            Det_Estatus: Det_Estatus,
            Accion: Accion,
            Prd_Descripcion: Prd_Descripcion,
            IdTipoProducto: IdTipoProducto,
            TipoProducto: TipoProducto,
            Familia: Familia, //10
            SubFamilia: SubFamilia,
            Vigencia: Vigencia,
            IdCausaDesabasto: IdCausaDesabasto,
            MotivoDesabasto: MotivoDesabasto,
            SAT_CveUnidad: SAT_CveUnidad,
            SAT_CveProdServ: SAT_CveProdServ,
            IdProveedor: IdProveedor,
            ProviderName: ProviderName,
            IdArray_ClientesExclusivos: IdArray_ClientesExclusivos,
            PedidoReferencia: PedidoReferencia,
            IdAplicacion: IdAplicacion,
            ProveedorCentral: ProveedorCentral,
            CodigoProductoProv: CodigoProductoProv,
            DescripcionProductoProv: DescripcionProductoProv,
            PresentacionProductoProv: PresentacionProductoProv,
        },
        cache: false,
        type: 'GET',
        async: false,
    }).done(function (response, textStatus, jqXHR) {
        var Estado = response.Estado;
        var Datos = response.Datos;
        var Mensaje = response.Mensaje;

        if (Estado == 1) {
            if (CALLBACK_Exito) {
                CALLBACK_Exito(Datos, Mensaje);
            }
        } else {
            alertify.error(Mensaje);
            if (CALLBAK_Error) {
                CALLBAK_Error(Datos, Mensaje);
            }
        }
    }).fail(function (jqXHR, textStatus, error) {
        console.log('ERRRO(1311):' + jqXHR.responseText);
        if (jqXHR.status == 401) {
            $('#dvDialogoInicioSesion').appendTo("body").modal();
        } else {
            alertify.error('Ocurrió una error:' + jqXHR.responseText);
        }
    });
}

function LlenarListaPrecios_Default(Id_Prd, CALLBACK_Exito) {
    let cmbMotivo = $('#cmbCategorias').val();
    var Fecha = new Date();
    var FechaFin = new Date();
    var Dia = Fecha.getDate();
    Dia = parseInt(Dia);
    if (Dia <= 9) {
        Dia = '0' + Dia;
    }
    var Mes = 0;
    Mes = Fecha.getMonth() + 1;
    Mes = parseInt(Mes);
    if (Mes <= 9) {
        Mes = '0' + Mes;
    }
    Fecha = Dia + '/' + Mes + '/' + Fecha.getFullYear();

    $('#rdpVigencia').val(Fecha);

    if (cmbMotivo == "1") {
        var FechaFinal = addDaysToDate(FechaFin, 15);
        var DiaFin = FechaFinal.getDate();
        var MesFin = FechaFinal.getMonth() + 1;
        if (DiaFin <= 9) {
            DiaFin = '0' + DiaFin
        }
        if (MesFin <= 9) {
            MesFin = '0' + MesFin
        }

        FechaFin = DiaFin + '/' + MesFin + '/' + FechaFinal.getFullYear();
        $('#rdpVigenciaFin').val(FechaFin);
    } else {
        var Año = FechaFin.getFullYear(FechaFin.getFullYear() + 1);
        Año = Año + 1;
        FechaFin = Dia + '/' + Mes + '/' + Año;
        $('#rdpVigenciaFin').val(FechaFin);
    }

    $('#tbl_rgPrecios > tbody').empty();
    var i = 1;
    var row = $('<tr class="" id="rowPedidoDesabastecido_' + i + '"">');
    //row.append($('<td class="text-left">').append(
    //    '<p id="Prd_FechaInicio_' + i + '">' + Fecha + '</p>'
    //));
    //row.append($('<td class="text-left">').append(
    //    '<p id="Prd_FechaFin_' + i + '">' + Fecha + '</p>'
    //));
    row.append($('<td class="text-right">').append(
        '<p id="Pre_Descripcion_' + i + '">Precio de Lista (Venta)</p>'
    ));
    row.append($('<td>').append('<label id="Pre_Pesos_' + i + '">0</label>'));
    row.append($('<td class="text-center">').append(
        '<i class="fa fa-pencil fa-2x clickable" ' +
        'onclick="PrecioProducto.Editar(this);" ' +
        'data-id=' + i + '>' +
        '</i>'
    ));
    $('#tbl_rgPrecios > tbody').append(row);

    i = 2;
    var row = $('<tr class="" id="rowPedidoDesabastecido_' + i + '"">');
    //row.append($('<td class="text-left">').append(
    //    '<p id="Prd_FechaInicio_' + i + '">' + Fecha + '</p>'
    //));
    //row.append($('<td class="text-left">').append(
    //    '<p id="Prd_FechaFin_' + i + '">' + Fecha + '</p>'
    //));
    row.append($('<td class="text-right">').append(
        '<p id="Pre_Descripcion_' + i + '">Costo</p>'
    ));
    row.append($('<td>').append('<label id="Pre_Pesos_' + i + '">0</label>'));
    row.append($('<td class="text-center">').append(
        '<i class="fa fa-pencil fa-2x clickable" ' +
        'onclick="PrecioProducto.Editar(this);" ' +
        'data-id=' + i + '>' +
        '</i>'
    ));
    $('#tbl_rgPrecios > tbody').append(row);

    i = 3;
    var row = $('<tr class="" id="rowPedidoDesabastecido_' + i + '"">');
    //row.append($('<td class="text-left">').append(
    //    '<p id="Prd_FechaInicio_' + i + '">' + Fecha + '</p>'
    //));
    //row.append($('<td class="text-left">').append(
    //    '<p id="Prd_FechaFin_' + i + '">' + Fecha + '</p>'
    //));
    row.append($('<td class="text-right">').append(
        '<p id="Pre_Descripcion_' + i + '">Precio AAA código compra local</p>'
    ));
    row.append($('<td>').append('<label id="Pre_Pesos_' + i + '">0</label>'));
    //row.append($('<td class="text-center">').append(
    //    '<i class="fa fa-pencil fa-2x clickable" ' +
    //    'onclick="PrecioProducto.Editar(this);" ' +
    //    'data-id=' + i + '>' +
    //    '</i>'
    //));
    $('#tbl_rgPrecios > tbody').append(row);

    if (CALLBACK_Exito) {
        CALLBACK_Exito();
    }
}

// Listad de presentacion
function ajax_CL_Cargar_cmbPresentacion(CallBack_Exito) {
    $.ajax({
        url: _ApplicationUrl + '/api/CL_Main/spPresentacion_ComboCompraLocal',
        data: {
        },
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
        var Estado = response.Estado;
        var Datos = response.Datos;
        if (Estado == 1) {
            if (CallBack_Exito) {
                CallBack_Exito(Datos);
            }
        } else {
            alertify.error('Ocurrió una error: ajax_CL_Cargar_cmbPresentacion');
        }
    }).fail(function (jqXHR, textStatus, error) {
        //alertify.error(jqXHR.responseText);
        if (jqXHR.status == 401) {
            $('#dvDialogoInicioSesion').appendTo("body").modal();
        } else {
            alertify.error('Ocurrió una error:' + jqXHR.responseText);
        }
    });
}

function ajax_CL_Cargar_cmbPresentacionProv(CallBack_Exito) {
    $.ajax({
        url: _ApplicationUrl + '/api/CL_Main/spPresentacion_ComboCompraLocal',
        data: {
        },
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
        var Estado = response.Estado;
        var Datos = response.Datos;
        if (Estado == 1) {
            if (CallBack_Exito) {
                CallBack_Exito(Datos);
            }
        } else {
            alertify.error('Ocurrió una error: ajax_CL_Cargar_cmbPresentacion');
        }
    }).fail(function (jqXHR, textStatus, error) {
        //alertify.error(jqXHR.responseText);
        if (jqXHR.status == 401) {
            $('#dvDialogoInicioSesion').appendTo("body").modal();
        } else {
            alertify.error('Ocurrió una error:' + jqXHR.responseText);
        }
    });
}


// Lista de Presentacion
function Cargar_cmbPresentacion(CALLBACK_Exito) {
    ajax_CL_Cargar_cmbPresentacion(function (lst) {
        $('#cmbPresentacion').empty();
        //$('#').append('<option value="0" >-- Seleccione -- </option>');
        for (i = 0; i < lst.length; i++) {
            $('#cmbPresentacion').append('<option value="' + lst[i].Id + '" >' + lst[i].Descripcion + '</option>');
        }
        if (CALLBACK_Exito) {
            CALLBACK_Exito();
        }
    });
}

function Cargar_cmbPresentacionProv(CALLBACK_Exito) {
    ajax_CL_Cargar_cmbPresentacionProv(function (lst) {
        $('#cmbPresentacionProv').empty();
        //$('#').append('<option value="0" >-- Seleccione -- </option>');
        for (i = 0; i < lst.length; i++) {
            $('#cmbPresentacionProv').append('<option value="' + lst[i].Id + '" >' + lst[i].Descripcion + '</option>');
        }
        if (CALLBACK_Exito) {
            CALLBACK_Exito();
        }
    });
}