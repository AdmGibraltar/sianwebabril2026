var Result_List = []; 

//

function Establece_TipoRepresentante(TT, CALLBACK_Success) {
    TT = parseInt(TT);
    switch (TT) 
    {
            case 1:
            case 2:
            case 5:
            case 6: //1 RSC //2 ASESOR                
                $('#ddlUEN').prop("selectedIndex", 0);
                $('#ddlUEN').attr('disabled', true);
                $('#tbUEN').val();
            
                $('#ddlSegmento').prop("selectedIndex", 0);
                $('#ddlSegmento').attr('disabled', true);
                $('#tbSegmento').val();

                // OCULTAR
                $('#div_Uen').addClass('tbl-Oculto');
                $('#div_Uen').removeClass('tbl-Visible');

                $('#div_Segmento').addClass('tbl-Oculto');
                $('#div_Segmento').removeClass('tbl-Visible');

                break;
            case 3:
            case 4: //RIK
                
                $('#ddlUEN').prop("selectedIndex", 0);
                $('#ddlUEN').attr('disabled', false);
                
                $('#ddlSegmento').prop("selectedIndex", 0);
                $('#ddlSegmento').attr('disabled', false);

                // DESPLEGAR
                $('#div_Uen').addClass('tbl-Visible');
                $('#div_Uen').removeClass('tbl-Oculto');

                $('#div_Segmento').addClass('tbl-Visible');
                $('#div_Segmento').removeClass('tbl-Oculto');
                
                break;
    }
    if (CALLBACK_Success) {
        CALLBACK_Success();
    }
}

//

function Desplegar_Indice(lst) {

    $('#tblTerritorios > tbody').empty();

    for (i=0; i<lst.length; i++) {
        var row = $('<tr id="RonNo_' + i + '">');

        /*
        if (lst[i].IdAutorizacion > 0) {
            row.append($('<td class="ColClave">').append(
                '<label>' + lst[i].Id_Ter + '</label>'
            )); mm
        } else {
            row.append($('<td class="ColClave">').append(
                lst[i].Id_Ter
            ));
        }
        */

        if (lst[i].IdAutorizacion > 0) {
            row.append($('<td class="ColEstatus">').append(
                '<span class="badge badge-success" style="margin-top:4px; background-color:#dbebfa; color:#000;">' + lst[i].Id_Ter + '</span>'
            ));
        } else {
            row.append($('<td class="ColEstatus">').append(
                lst[i].Id_Ter 
            ));
        }           

        row.append($('<td class="ColDesc">').append(            
            '<label ' +
                'data-id_ter="' + lst[i].Id_Ter + '" ' +
		        'onclick="Territorio_Editar(this);" ' +		                        
                'class="p_ColDesc" '+
            '>'+
                lst[i].Descripcion +
            '</label>'
        ));

        row.append($('<td class="ColUEN">').append(
            lst[i].Uen_Descripcion
        ));

        row.append($('<td class="ColRepr">').append(
            lst[i].Rik_Nombre
        ));

        row.append($('<td class="ColSegm">').append(
            lst[i].Seg_Nombre 
        ));

        row.append($('<td class="ColTClie">').append(
            lst[i].TipoCliente_Nombre
        ));

        row.append($('<td class="ColTR">').append(
            lst[i].TipoRepresentante_Nombre
        ));
        
        if (lst[i].Estatus == true) {
            row.append($('<td class="ColEstatus">').append(
                '<span class="badge badge-success" style="margin-top:4px; background-color:#2dde98;">Activo</span>'
            ));
        } else {
            row.append($('<td class="ColEstatus">').append(
                '<span class="badge badge-secondary" style="margin-top:4px;">Inactivo</span>'
            ));
        }

        row.append($('<td class="ColEditar">').append(            
            '<table class="w100">'+
                '<tr>'+
                    '<td>'+

                    '<button ' +
		                'type="button" ' +
                        'data-id_ter="' + lst[i].Id_Ter + '" ' +
		                'onclick="Modal_Territorio_Detalle(this);" ' +		        
		                'class="btn btn-sm btn-Editar" ' +
		                '> ' +
                            '<i class="fa fa-list-alt fa-1x">&nbsp;Detalle</i>'+
			        '</button>'+

                    '</td>'+
                    '<td>' +

                    '<button ' +
		                'type="button" ' +
                        'data-id_ter="' + lst[i].Id_Ter + '" ' +
		                'onclick="Territorio_Editar(this);" ' +		        
		                'class="btn btn-sm btn-Editar" ' +
		                '> ' +
                            '<i class="fa fa-edit fa-1x"></i>'+
			        '</button>'+         

                    '</td>'+
                '</tr>'+
            '</table>'
        ));

        $('#tblTerritorios').append(row);
    }

    var RecordCount = 0;    
    try {
        RecordCount = parseInt(lst[0].RecordCount);
        if (isNaN(RecordCount)) {
            RecordCount = 0;
        }
    }
    catch (error) {
        RecordCount = 0;
    }

    PaginacionTerritorios_Inicializar(Paginacion_Actual, RecordCount);
}

//

function TerritoriosModal_AplicarPermisos(P) {
    $('#btnTerritorio_Guardar').prop('disabled', true);
    $('#btnTerritorio_Activar').prop('disabled', true);
    $('#btnTerritorio_Desactivar').prop('disabled', true);
    if (P.PGrabar) {
        $('#btnTerritorio_Guardar').prop('disabled', false);
    }
    if (P.PModificar) {
        $('#btnTerritorio_Activar').prop('disabled', false);
        $('#btnTerritorio_Desactivar').prop('disabled', false);
    }
}

//

function Inicializa_Modal() {

    $('#tbUEN').val('');
    
    $('#ddSegmento').empty();
    $('#tbSegmento').empty();

    $('#tbTipoCliente').val('');

    $('#tbClaveTerritorio').val('');

    $('#baTerritorioInactvo').text('Inactivo');
    $('#baTerritorioActvo').text('Activo');

    $('#tbRepresentante').val('');

    $('#div_SolocitudCambio').addClass('tbl-Oculto');
    $('#div_SolocitudCambio').removeClass('tbl-Visible_SolocitudCambio ');

    //$('#btnCrearSolicitudDeCambio').css('display', 'inline-block');
    $('#btnCrearSolicitudDeCambio').css('display', 'none');
    $('#btnCrearSolicitudDeCambio').prop('disabled', true);

    $('#btnTerritorio_Activar').css('display', 'none');
    $('#btnTerritorio_Activar').prop('disabled', true);

    $('#btnTerritorio_Desactivar').css('display', 'none');
    $('#btnTerritorio_Desactivar').prop('disabled', true);

    $('#hfIdAutorizacion').val('');

    $('#tbSolicitudCambio_Terr').val('');

    $('#hfIdInsertUpdate').val(0);

    $('#tbIdLocal').val('');
    $('#tbTerritorio').val('');

    $('#hfGuardarCambioDeEstado ').val(0);

    
}

//
function Inicializar_Combos() {
    // Carga Uen y Segmento 

    UEN_CargarDdl_wCB($('#ddlUEN'), function () {
        var Uen = $('#ddlUEN').val();
        $('#tbUEN').val(Uen);
        Segmento_CargarDdl($('#ddlUEN').val(), $('#ddlSegmento'), function () {
            var Seg = $('#ddlSegmento').val();
            $('#tbSegmento').val(Seg);
        });
        //$('#tbSegmento').prop('disabled', true);
    });
        
}

//

function Modo_Cargando() {        
    $('#btnCrearSolicitudDeCambio').prop('disabled', true);
    $('#btnTerritorio_Cancelar').prop('disabled', true);
    $('#btnTerritorio_Guardar').prop('disabled', true);
}

//

function Modo_Cargando_Terminado() {
    $('#btnCrearSolicitudDeCambio').prop('disabled', false);
    $('#btnTerritorio_Cancelar').prop('disabled', false);
    $('#btnTerritorio_Guardar').prop('disabled', false);
}

// 

function Modal_Estado(estado) {
    $('#ddlTipoTerritorio').prop('disabled', !estado);
    $('#ddlUEN').prop('disabled', !estado);
    $('#ddlSegmento').prop('disabled', !estado);
    $('#tbTipoCliente').prop('readonly', !estado);
    $('#ddlTipoCliente').prop('disabled', !estado);
    //$('#tbClaveTerritorio').prop('readonly', !estado);
    $('#tbIdLocal').prop('readonly', !estado);
    $('#tbIdRepresentante').prop('readonly', !estado);
    $('#ddlRepresentante').prop('disabled', !estado);
    $('#tbTerritorio').prop('readonly', !estado);    

}

// EDITAR - EDITAR - EDITAR - EDITAR - EDITAR - EDITAR - EDITAR - EDITAR 

function Territorio_Editar(obj) {

    $('#spinner_modalTerritorio').css('display', 'block');   

    Modo_Cargando();     

    var Id_Ter = $(obj).data('id_ter');

    Inicializa_Modal();

    $('#hfIdInsertUpdate').val(1);    
    
    TipoCliente_CargarDdl($('#ddlTipoCliente'));

    $('#modalTerritorio').appendTo("body").modal('show');

    Territorios_CargarById(Id_Ter, function (Rec) {

        Modo_Cargando();

        $('#tbTerritorio').val(Rec.Descripcion);

        if (Rec.Estatus == 1) {
            $('#baTerritorioActvo').css('display', 'block');
            $('#baTerritorioInactvo').css('display', 'none');
            $('#btnTerritorio_Activar').css('display', 'none');
            $('#btnTerritorio_Desactivar').css('display', 'block');
        } else {
            $('#baTerritorioActvo').css('display', 'none');
            $('#baTerritorioInactvo').css('display', 'block');
            $('#btnTerritorio_Activar').css('display', 'block');
            $('#btnTerritorio_Desactivar').css('display', 'none');
        }

        $('#ddlTipoTerritorio').val(Rec.Id_TipoRepresentante);

        Establece_TipoRepresentante(Rec.Id_TipoRepresentante, function () {
            Inicializar_Combos();
        });

        switch (Rec.Id_TipoRepresentante) {
            case 1:
                break;
            case 3:
                break;
        }

        $('#ddlUEN').val(Rec.Id_Uen);

        Segmento_CargarDdl(Rec.Id_Uen, $('#ddlSegmento'), function () {
            var Seg = $('#ddlSegmento').val();
            $('#tbSegmento').val(Seg);
            //$('#tbSegmento').prop('disabled', true);
            $('#ddlSegmento').prop('disabled', true);
        });

        //Usuarios
        TerrUsuarios_CargarCombos(Rec.Id_Uen, Rec.Id_TipoRepresentante, $('#ddlRepresentante'), Rec.Id_Rik, function (lst) {
            var Rep = $('#ddlRepresentante').val();
            $('#tbIdRepresentante').val(Rep);
            $('#tbIdRepresentante').prop('disabled', true);
            $('#ddlRepresentante').prop('disabled', true);
        }, function () {
            //Error
            $('#tbIdRepresentante').prop('disabled', true);
            $('#ddlRepresentante').prop('disabled', true);
        })

        // CONSULTA Y APLICA PERMISOS 
        Cargar_PermisosUsuario(Territorios_Pagina, function (Permiso) {
            TerritoriosModal_AplicarPermisos(Permiso);
        }, function () {
        });

        //
        // CONSULTA SI TIENE PENDIENTE AUTORIZACIONES
        //
        Modo_Cargando();

        if (Rec.IdAutorizacion > 0) {
            TerrUsuarios_CargarCombos(Rec.Id_Uen, Rec.Id_TipoRepresentante, $('#ddlSolicitudCambio_Representante'), 0,
            function (lst) {
                $('#ddlSolicitudCambio_Representante').val(Rec.Aut_IdRepresentante);
            }, function () {
            });

            $('#tbSolicitudCambio_Representante').val(Rec.Aut_IdRepresentante)
            $('#ddlSolicitudCambio_Representante').prop('disabled', true);

            $('#tbSolicitudCambio_Terr').val(Rec.Aut_Territorio)
            $('#tbSolicitudCambio_Terr').prop('readonly', true);


            $('#div_SolocitudCambio').addClass('tbl-Visible_SolocitudCambio');
            $('#div_SolocitudCambio').removeClass('tbl-Oculto');
            $('#btnCrearSolicitudDeCambio').css('display', 'none');
            $('#btnCancelarSolicitudCambios').css('display', 'none');

        } else {
            $('#btnCrearSolicitudDeCambio').css('display', 'inline-block');
            $('#tbSolicitudCambio_Representante').prop('enabled', false);
            $('#tbSolicitudCambio_Terr').val(Rec.Aut_Territorio)
            $('#tbSolicitudCambio_Terr').prop('readonly', false);
        }

        Modo_Cargando_Terminado();

        //Modo_Cargando_Terminado();
        $('#tbUEN').val(Rec.Id_Uen);
        $('#ddlSegmento').val(Rec.Id_Seg);
        $('#tbSegmento').val(Rec.Id_Seg);
        $('#ddlTipoCliente').val(Rec.Id_TipoCliente);
        $('#tbTipoCliente').val(Rec.Id_TipoCliente);
        $('#tbClaveTerritorio').val(Rec.Id_Ter);
        $('#tbIdLocal').val(Rec.Id_Local);
        $('#tbRepresentante').val(Rec.Id_Rik);

        if (Rec.Estatus) {
            $('#btnTerrAut_Activo').css('display','block');
            $('#btnTerrAut_Inactivo').css('display', 'none');
        } else {
            $('#btnTerrAut_Activo').css('display', 'none');
            $('#btnTerrAut_Inactivo').css('display', 'block');
        }

        Modal_Estado(false);

        // Si tiene una solicitud de cambio no permitira guardar.
        if (Rec.IdAutorizacion > 0) {
            $('#btnTerritorio_Guardar').prop('disabled', true);
        }

        $('#spinner_modalTerritorio').css('display', 'none');
    }, function () {
        Modo_Cargando_Terminado();
        $('#spinner_modalTerritorio').css('display', 'none');
    });

}


// NUEVO - NUEVO - NUEVO - NUEVO - NUEVO - NUEVO - NUEVO - NUEVO - 

function Modal_Territorio_Nuevo() {

    $('#spinner_modalTerritorio').css('display', 'block');

    Inicializa_Modal();
    
    var TT = $('#ddlTipoTerritorio').val();

    Establece_TipoRepresentante(TT, function () {
        Inicializar_Combos();
        Calcular_Territorio();
    });   

    TipoCliente_CargarDdl($('#ddlTipoCliente'));
    $('#modalTerritorio').appendTo("body").modal('show');

    //Usuarios
    var TT = $('#ddlTipoTerritorio').val();
    TerrUsuarios_CargarCombos(0, TT, $('#ddlRepresentante'), 0, function (lst) {
        var Rep = $('#ddlRepresentante').val();
        $('#tbIdRepresentante').val(Rep);
        $('#ddlRepresentante').prop('disabled', false);
    }, function () {
        //ERROR
    });

    Modal_Estado(true);

    Calcular_Territorio();

    $('#btnTerrAut_Activo').css('display', 'block');
    $('#btnTerrAut_Inactivo').css('display', 'none');
            
    setTimeout(function () {
        $('#spinner_modalTerritorio').css('display', 'none');
    }, 2000);
}

//

function Detalle_Cargar_Tbl(lst) {

    $('#tblTerritorioDetalle > tbody').empty();

    for (i = 0; i < lst.length; i++) {
        var row = $('<tr id="RonNo_' + i + '">');
        row.append($('<td class="ColUEN">').append(
                lst[i].Id_Cte
            ));
        row.append($('<td class="ColRepr">').append(
                lst[i].Cte_NomComercial
            ));

        row.append($('<td class="ColRepr">').append(
                lst[i].Id_Terr
            ));

        if (lst[i].Cte_Activo == 1) {
            row.append($('<td class="ColEstatus">').append(
                    '<span class="badge badge-success" style="margin-top:4px; background-color:#2dde98;">Activo</span>'
                ));
        } else {
            row.append($('<td class="ColEstatus">').append(
                    '<span class="badge badge-secondary" style="margin-top:4px;">Inactivo</span>'
                ));
        }

        $('#tblTerritorioDetalle').append(row);
    }

}

// TRRITORIO - DETALLE

function Modal_Territorio_Detalle(obj) {
    $('#spinner_Indice').css('display', 'block');
    $('#spinner_modalDetalle').css('display', 'block');
    var Id_Ter = $(obj).data('id_ter');
    $('#hfModal_Id_Ter').val(Id_Ter);
    $('#lbTerritorioDetalle_Count').text('0 Segistros encontados.');
    Territorio_Detalle(Id_Ter, '',
    function (lst) {
        $('#modalDetalle').appendTo("body").modal('show');
        Result_List = lst;
        $('#lbTerritorioDetalle_Count').text(lst.length + ' Registros encontados.');
        Detalle_Cargar_Tbl(lst);
        $('#spinner_Indice').css('display', 'none');
        $('#spinner_modalDetalle').css('display', 'none');                
    },
    function () {
        $('#lbTerritorioDetalle_Count').text('Puede que haya ocurrido un error en la consulta.');
        $('#spinner_Indice').css('display', 'none');
        $('#spinner_modalDetalle').css('display', 'none');
    });
} 

// CALCULA TERRRITORIO - CALCULA TERRRITORIO - CALCULA TERRRITORIO - CALCULA TERRRITORIO

function Calcular_Territorio() {

    $('#spinner_modalTerritorio').css('display', 'block');
        
    var TipoRepresentante = $('#ddlTipoTerritorio').val();
    
    if ($('#ddlTipoTerritorio') === null) {
        TipoRepresentante = '';
    }

    if (TipoRepresentante.length == 1) {
        TipoRepresentante = TipoRepresentante;
    }

    if (TipoRepresentante == '1' || TipoRepresentante == '2' || TipoRepresentante == '5' || TipoRepresentante == '6') {
        $('#tbUEN').val('');        
        $('#tbSegmento').val('');
    }
        
    var UEN = $('#tbUEN').val();
    if ($('#tbUEN') === null) {
        UEN = '';
    }
    if (UEN.length == 1) {
        UEN = '0'+UEN ;
    }

    // Segmento
    var Segmento = $('#tbSegmento').val();
    if (Segmento === null) {
        Segmento = '';
    }
    if (Segmento.length > 0) {
        //Segmento = '0' + Segmento;
        Consultar_Segmento(Segmento, function (lst) {
            var Seg_IdXUen = lst[0].Seg_IdXUen.trim();
            if (Seg_IdXUen.length == 1) {
                Segmento = '0' + lst[0].Seg_IdXUen.trim();
            } else {
                Segmento = Seg_IdXUen;
            }
        }, function () {
        });
    }
    
    var TipoCliente = $('#ddlTipoCliente').val();
    if (TipoCliente === null) {
        TipoCliente = '';
    }
    if (TipoCliente.length == 1) {
        TipoCliente = '0'+TipoCliente;
    }
    if (TipoCliente == '-1') {
        TipoCliente = '';
    }

    var Territorio_Prefijo = TipoRepresentante + UEN + Segmento + TipoCliente;

    UEN = parseInt(UEN);
    if (isNaN(UEN)) {
        UEN = 0;
    }

    Segmento = parseInt(Segmento);
    if (isNaN(Segmento)) {
        Segmento = 0;
    }

    TipoCliente = parseInt(TipoCliente);
    if (isNaN(TipoCliente)) {
        TipoCliente = 0;
    }
    
    Territorio_Maximo(Territorio_Prefijo, TipoRepresentante, TipoCliente, UEN, Segmento, function (TerritorioNuevo) {
        $('#tbClaveTerritorio').val(Territorio_Prefijo+TerritorioNuevo);    
    }, function () {
        $('#tbClaveTerritorio').val('Error');
    });

    setTimeout(function () {
        $('#spinner_modalTerritorio').css('display', 'none');
    }, 2000);


}

// GUARDAR

function Guardar_Territorio() {
    var hfIdAutorizacion = $('#hfIdAutorizacion').val();
    var CrearSolicitud = false;    
    if (hfIdAutorizacion == "0") {
        CrearSolicitud = true;
    }
    
    var tbTipoCliente = $('#tbTipoCliente').val();
    tbTipoCliente = tbTipoCliente.trim();
    if (tbTipoCliente.length <= 0) {
        alertify.alert('El valor en el campo "Tipo de cliente" es obligatorio.');
        return;
    }

    var tbIdLocal = $('#tbIdLocal').val();
    tbIdLocal = tbIdLocal.trim();
    if (tbIdLocal.length <= 0) {
        alertify.alert('El valor en el campo "Id Local" es obligatorio.');
        return;
    }

    var tbTerritorio = $('#tbTerritorio').val();
    tbTerritorio = tbTerritorio.trim();
    if (tbTerritorio.length <= 0) {
        alertify.alert('El valor en el campo "Territorio autorizado -> Territorio" es obligatorio.');
        return;
    }
    
    var GuardarCambioDeEstado = $('#hfGuardarCambioDeEstado').val();
    // CREAR SOLICITUD DE CAMBIO 
    if (CrearSolicitud == true) {

        var ClaveTerritorio = $('#tbClaveTerritorio').val();
        var IdRepresentantes = $('#tbSolicitudCambio_Representante').val();
                             
        var Territorio = $('#tbSolicitudCambio_Terr').val();
        var Activo = $('#ddlSolicitudCambio_Activo').val();

        Territorio_CrearSolicitud(
            ClaveTerritorio, IdRepresentantes, Territorio, Activo,
        function (Res) {
            $('#modalTerritorio').modal('hide');
            alertify.success('Se ha creado la solicitud de autorización.');
            Pre_Territorios_CargarIndice();
        },
        function () {            
            alertify.error('ERROR: No se pudo crear la solicitud de autorización.');
        });

    } else {
        
        // GUARDAR NUEVO ACTUALIZAR
        var IdInsertUpdate = $('#hfIdInsertUpdate').val();

        // 1 = Actualizar
        if (IdInsertUpdate ==1) {
            alertify.error('Administraci&oacute;n de territorios no permite actualizaci&oacute;n.');
            return;
        }

        var Id_Ter = $('#tbClaveTerritorio').val();
        var Ter_Nombre = $('#tbTerritorio').val();
        var Id_Uen = $('#ddlUEN').val();
        Id_Uen = parseInt(Id_Uen);
        if (isNaN(Id_Uen)) {
            Id_Uen = 0;
        }
        var Id_Rik = $('#ddlRepresentante').val();
        Id_Rik = parseInt(Id_Rik);
        if (isNaN(Id_Rik)) {
            Id_Rik = 0;
        }
        var Id_Seg = $('#ddlSegmento').val();
        Id_Seg = parseInt(Id_Seg);
        if (isNaN(Id_Seg)) {
            Id_Seg = 0;
        }
        var Id_TipoCliente= $('#ddlTipoCliente').val();        
        var Id_Local= $('#tbIdLocal').val();
        var Id_TipoRepresentante = $('#ddlTipoTerritorio').val();
        var Ter_Activo = $('#hfTer_Activo').val();
                
        DataTer = {
            'IdInsertUpdate': IdInsertUpdate,
            'Id_Ter': Id_Ter,
            'Ter_Nombre': Ter_Nombre,
            'Id_Uen': Id_Uen,
            'Id_Rik': Id_Rik,
            'Id_Seg': Id_Seg,
            'Id_TipoCliente': Id_TipoCliente,
            'Id_Local': Id_Local,
            'Id_TipoRepresentante': Id_TipoRepresentante,
            'Ter_Activo': Ter_Activo,
            'Cve_Ter': Id_Ter
        }

        Territorio_Crear(DataTer,
        function () {
            //Exito
            $('#modalTerritorio').modal('hide');
            if (IdInsertUpdate == 0) {
                alertify.success('Se a creado el registro de territorio ' + Id_Ter + ' exitosamente.');
            } else {
                alertify.success('Se a actualizado exitosamente el territorio: ' + Id_Ter + '.');
            }

            Pre_Territorios_CargarIndice();

        }, function () {
            //Fail
            alertify.error('ERROR: No se pudo crear el territorio: ' + Territorio + '.');
        });
    }

}

//

function Pre_Territorios_CargarIndice() {
    var Ter_Activo = $('#ddlTer_Activo').val();
    Territorios_CargarIndice(
        Paginacion_Actual, 
        Paginacion_PageSize,
        $('#tbParametroBusqueda').val(),
        $('#ddlTipoRepresentante').val(),
        Ter_Activo, 
    function (RES) {
        Desplegar_Indice(RES)
    },
    function () {

    }
    );
}

//

function Prec_Terr_BajasReporte() {
    var Ter_Activo = $('#ddlTer_Activo').val();
    Territorios_CargarIndice(
        1, 1000,        
        $('#tbParametroBusqueda').val(),
        $('#ddlTipoRepresentante').val(),
        Ter_Activo,
    function (RES) {

        Terr_BajarReporteExcel(RES);

    },
    function () {

    }
    );
}

// DOCUMENT READY

$(document).ready(function () {

    // Hack to enable multiple modals by making sure the .modal-open class
    // is set to the <body> when there is at least one modal open left
    $('body').on('hidden.bs.modal', function () {
        if ($('.modal.in').length > 0) {
            $('body').addClass('modal-open');
        }
    });

    //
    $('.datepicker').Zebra_DatePicker({
        format: 'd/m/Y'
    });

    //
    $('#ddlUEN').on('change', function () {
        var UEN = $('#ddlUEN').val();
        $('#tbUEN').val(UEN);
        Segmento_CargarDdl($('#ddlUEN').val(), $('#ddlSegmento'), function () {
            var Seg = $('#ddlSegmento').val();
            $('#tbSegmento').val(Seg);
            Calcular_Territorio();
        });
    });

    //
    $('#ddlRepresentante').on('change', function () {
        var Rep = $('#ddlRepresentante').val();
        $('#tbIdRepresentante').val(Rep);
    });

    //
    $('#ddlSegmento').on('change', function () {
        var Seg = $('#ddlSegmento').val();
        $('#tbSegmento').val(Seg);
        Calcular_Territorio();
    });

    // DDL TIPO CLIENTE (change)
    $('#ddlTipoCliente').on('change', function () {
        $('#tbTipoCliente').val($('#ddlTipoCliente').val());
        Calcular_Territorio();
    });

    //
    $('#ddlSolicitudCambio_Representante').on('change', function () {
        var ddlSolicitudCambio_Representante = $('#ddlSolicitudCambio_Representante').val();
        $('#tbSolicitudCambio_Representante').val(ddlSolicitudCambio_Representante);
    });

    // DDL TERRITORIO (change)
    $('#ddlTipoTerritorio').on('change', function () {

        $('#spinner_modalTerritorio').css('display', 'block');

        var TT = $('#ddlTipoTerritorio').val();
        Establece_TipoRepresentante(TT, function () {
            Inicializar_Combos();
            Calcular_Territorio();
        });

        //Usuarios
        TerrUsuarios_CargarCombos(0, TT, $('#ddlRepresentante'), 0, function (lst) {
            var Rep = $('#ddlRepresentante').val();
            $('#tbIdRepresentante').val(Rep);
            $('#ddlRepresentante').prop('disabled', false);
        }, function () {
            $('#ddlRepresentante').prop('disabled', true);
        });

        setTimeout(function () {
            $('#spinner_modalTerritorio').css('display', 'none');
        }, 2000);

    });

    //
    PaginacionTerritorios_Inicializar(1, 100);

    //
    $('#btnTerritorio_Cancelar').click(function () {
        $('#modalTerritorio').modal('hide');
    });

    //
    $('#btnTerritorio_Guardar').click(function () {
        Guardar_Territorio();
    });

    // CREAR SOLICITUD
    $('#btnCrearSolicitudDeCambio').click(function () {
        $('#hfIdAutorizacion').val(0);
        $('#spinner_modalTerritorio').css('display', 'block');

        $('#div_SolocitudCambio').addClass('tbl-Visible_SolocitudCambio ');
        $('#div_SolocitudCambio').removeClass('tbl-Oculto');

        $('#btnCrearSolicitudDeCambio').css('display', 'none');

        var Id_Uen = $('#tbUEN').val();
        var Id_TipoRepresentante = $('#ddlTipoTerritorio').val();

        TerrUsuarios_CargarCombos(Id_Uen, Id_TipoRepresentante, $('#ddlSolicitudCambio_Representante'), 0,
        function (lst) {
            var Rep = $('#ddlRepresentante').val();
            $('#ddlSolicitudCambio_Representante').val(Rep);
        }, function () {
        });

        $('#ddlSolicitudCambio_Representante').prop('disabled', false);

        var ClaveTerritorio = $('#tbTerritorio').val();
        $('#tbSolicitudCambio_Terr').val(ClaveTerritorio);

        setTimeout(function () {
            $('#spinner_modalTerritorio').css('display', 'none');
        }, 2000);

    });

    //
    $('#btnCancelarSolicitudCambios').click(function () {
        $('#div_SolocitudCambio').addClass('tbl-Oculto');
        $('#div_SolocitudCambio').removeClass('tbl-Visible_SolocitudCambio ');
        $('#btnCrearSolicitudDeCambio').css('display', 'inline-block');
    });

    // NUEVO 
    $('#btnTerritorio_Nuevo').click(function () {
        Modal_Territorio_Nuevo();
    });

    // NUEVO 
    $('#btnTerr_DescargarReporte').click(function () {
        Prec_Terr_BajasReporte();
    });

    //
    $('#btnCargarListado').click(function () {
        Paginacion_Actual = 1;

        Pre_Territorios_CargarIndice();
        /*
        Territorios_CargarIndice(
        $('#tbParametroBusqueda').val(),
        $('#ddlTipoRepresentante').val(),
        function (RES) {
        Desplegar_Indice(RES)
        },
        function () {
        }
        );
        */
    });

    //

    Pre_Territorios_CargarIndice();

    $('#btnModalDetalle_Buscar').click(function () {
        $('#spinner_modalDetalle').css('display', 'block');
        var tbDetalle_Buscar = $('#tbDetalle_Buscar').val();
        var Modal_Id_Ter = $('#hfModal_Id_Ter').val();
        Territorio_Detalle(Modal_Id_Ter, tbDetalle_Buscar, function (lst) {
            Result_List = lst;
            $('#lbTerritorioDetalle_Count').text(lst.length + ' Registros encontados.');
            Detalle_Cargar_Tbl(lst);
            $('#spinner_modalDetalle').css('display', 'none');
        },
        function () {
            $('#lbTerritorioDetalle_Count').text('Error>> Puede que haya ocurrido un error en la consulta.');            
            $('#spinner_modalDetalle').css('display', 'none');
        });
    });

    $('#btnTerritorio_Activar').click(function () {
        $('#hfTer_Activo').val(1);
        $('#hfGuardarCambioDeEstado').val(1);
        $('#btnTerritorio_Activar').css('display', 'none');
        $('#baTerritorioInactvo').text('Debe guardar para Activar el territorio');
    });

    $('#btnTerritorio_Desactivar').click(function () {
        $('#hfTer_Activo').val(0);
        $('#hfGuardarCambioDeEstado').val(1);
        $('#btnTerritorio_Desactivar').css('display', 'none');
        $('#baTerritorioActvo').text('Debe guardar para Desactivar el territorio');
    });

});