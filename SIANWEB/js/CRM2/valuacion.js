
function Valuacion_Global(Id_Cte, CALLBACK_Exito, CALLBACK_Error) {

    $.ajax({
        url: _ApplicationUrl + '/api/ObtenerProyectosPorRik/spCRM_SelProyectos/?Id_Rik=' + Id_Rik + '&Id_Cte=0&Agrupado=1',
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
        var res = response.Datos;
        var estado = response.Estado;
        var mensaje = response.Mensaje;

        if (estado == 1) {
            if (CALLBACK_Exito) {
                CALLBACK_Exito(res);
                //$('#spinner_listarep').css('display', 'none');
            }
        }

    }).fail(function (jqXHR, textStatus, error) {
        $('#spinner_listarep').css('display', 'none');
        if (jqXHR.status == 401) {
            alert('La sessión ha expirado.');
            $('#dvModalPropuestaTE_ver2').modal('hide');
            $('#dvDialogoInicioSesion').modal();
        } else {
            alertify.error('Ocurrió una error: funcion Cargar_TabaUsuariosRik.');
        }
    });

}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Cargar_IndiceProyectos(Id_Rik, CALLBACK_Exito, CALLBACK_Error) {
    //$('#spinner_listarep').css('display', 'block');
    $.ajax({
        url: _ApplicationUrl + '/api/ObtenerProyectosPorRik/spCRM_SelProyectos/?Id_Rik=' + Id_Rik + '&Id_Cte=0&Agrupado=1',
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
        var res = response.Datos;
        var estado = response.Estado;
        var mensaje = response.Mensaje;

        if (estado == 1) {
            if (CALLBACK_Exito) {
                CALLBACK_Exito(res);
                //$('#spinner_listarep').css('display', 'none');
            }
        }

    }).fail(function (jqXHR, textStatus, error) {
        $('#spinner_listarep').css('display', 'none');
        if (jqXHR.status == 401) {
            alert('La sessión ha expirado.');
            $('#dvModalPropuestaTE_ver2').modal('hide');
            $('#dvDialogoInicioSesion').modal();
        } else {
            alertify.error('Ocurrió una error: funcion Cargar_TabaUsuariosRik.');
        }
    });
}

// 

function Cargar_SubTabla(Id_Rik, Id_Cte, CALLBACK_Exito, CALLBACK_Error) {
    //$('#spinner_listarep').css('display', 'block');
    $.ajax({
        url: _ApplicationUrl + '/api/ObtenerProyectosPorRik/spCRM_SelProyectos/?Id_Rik=' + Id_Rik + '&Id_Cte=' + Id_Cte + '&Agrupado=0',
        cache: false,
        type: 'GET',
        async: false,
    }).done(function (response, textStatus, jqXHR) {
        var res = response.Datos;
        var estado = response.Estado;
        var mensaje = response.Mensaje;

        if (estado == 1) {
            if (CALLBACK_Exito) {
                CALLBACK_Exito(res);
                //$('#spinner_listarep').css('display', 'none');
            }
        }
    }).fail(function (jqXHR, textStatus, error) {
        $('#spinner_listarep').css('display', 'none');
        if (jqXHR.status == 401) {
            alert('La sessión ha expirado.');
            $('#dvModalPropuestaTE_ver2').modal('hide');
            $('#dvDialogoInicioSesion').modal();
        } else {
            alertify.error('Ocurrió una error: funcion Cargar_TabaUsuariosRik.');
        }
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Cargar_ValuacionGlobales(Id_Rik, Id_Cte, CALLBACK_Exito, CALLBACK_Error) {
    //$('#spinner_listarep').css('display', 'block');
    $.ajax({
        url: _ApplicationUrl + '/api/ObtenerProyectosPorRik/spCRM_SelProyectos/?Id_Rik=' + Id_Rik + '&Id_Cte=' + Id_Cte + '&Agrupado=2',
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
        var res = response.Datos;
        var estado = response.Estado;
        var mensaje = response.Mensaje;

        if (estado == 1) {
            if (CALLBACK_Exito) {
                CALLBACK_Exito(res);
                //$('#spinner_listarep').css('display', 'none');
            }
        }

    }).fail(function (jqXHR, textStatus, error) {
        $('#spinner_listarep').css('display', 'none');
        if (jqXHR.status == 401) {
            alert('La sessión ha expirado.');
            $('#dvModalPropuestaTE_ver2').modal('hide');
            $('#dvDialogoInicioSesion').modal();
        } else {
            alertify.error('Ocurrió una error: funcion Cargar_TabaUsuariosRik.');
        }
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function CargarTablaProyectos(Tipo_Vista) {

    Cargar_IndiceProyectos(_CRM_Usuario_Rik, function (lst) {
        /* 
        $('#tblProyectosIndex').DataTable( {
            "ajax": lst
        });                    
        */

        var scrollY = "200px";
        var pageLength = 7;
        if (Tipo_Vista == 0) {
            scrollY = "200px";
            pageLength = 7;
        }
        if (Tipo_Vista == 1) {
            scrollY = "500px";
            pageLength = 15;
        }


        _tablaProyectos = $('#tblProyectosIndex').DataTable({
            /*"sDom": "<'dataTables_header' <'row' <'col-md-10' f i r> B <'col-md-1' <'#tblProyectosToolbar'> > > >" +
                "<'table-responsive'  t >" +
                "<'dataTables_footer' p >",
            */
            'pageLength': pageLength,
            "deferRender": true,
            'ordering': true,
            'scrollY': scrollY,
            'scrollCollapse': true,
            'language': {
                "processing": "<img src='../../Img/ajax-loader.gif'> Cargando...",
                'url': 'http://cdn.datatables.net/plug-ins/1.10.12/i18n/Spanish.json'
            },

            //drawCallback: function () { // this gets rid of duplicate headers
            //$('.dataTables_scrollBody thead tr').css({ display: 'collapse' }); 
            //                      },
            'data': lst,
            /*'ajax': {
                //'url': '<%=ApplicationUrl %>' + '/api/ObtenerProyectosPorRik', 
                'url': '<%=ApplicationUrl %>' + '/api/ObtenerProyectosPorRik/spCRM_SelProyectos?Id_Rik=0&Id_Cte=0', 
                'dataSrc': ''
            },*/
            /*buttons: [
                {
                    text: 'Nuevo Proyecto',
                    action: function(e, dt, node, config){
                    },
                    className: 'btn btn-default',
                    tag: 'input'
                }
            ],*/
            'columns': [
                {
                    'className': 'details-control',
                    'orderable': false,
                    'data': null,
                    'defaultContent': ''
                    //,'className': 'proyectos_col1'     
                },
                {
                    'data': '',
                    'className': 'details-control',
                    'orderable': false,
                    'render':
                        function (data, type, full, meta) {
                            return full.VinculadoCentral != '' ? full.VinculadoCentral : '';
                        }
                },
                {
                    'data': 'Id_Cte',
                    'className': 'proyectos_col_proyecto'
                },
                {
                    'data': 'NombreCliente',
                },
                /*  { 
                      'data': null,
                      'defaultContent': ''
                      /* '<button '+
                          'type="button" '+
                          'class="btn btn-primary" '+
                          //'data-toggle="modal" '+
                          //'data-target="#dvModalValuacion" '+
                          'onclick="VerValuacion(this)" '+
                          'data-modo="0" '+
                          'id="btnGenerarValuacion">'+
                              '<i class="fa fa-tasks"></i>Generar Valuación'+
                          '</button>'*,
                      'render': ''
                     /*  function (data, type, full, meta) {                                
                          return '<button '+
                          'type="button" class="btn btn-primary" '+
                          //'data-toggle="modal" '+
                          //'data-target="#dvModalValuacion" '+
                          'onclick="VerValuacion(this)" '+
                          'data-modo="0" '+
                          'id="btnGenerarValuacion_' + meta.row + '" '+
                          'data-idcte="' + full.Id_Cte + '">'+
                          '<i class="fa fa-tasks"></i>Generar Valuación'+
                      '</button>';
                  }*
              }*/
            ]
        });



        $('#dvLoading').fadeOut(1000);

    }, function () {
        console.log('ERROR: Cargar_IndiceProyectos');
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function cerrarVentanaValuacion_Generada(id) {
    var hfId_Cte = $('#hfId_Cte').val();
    $('#dvModalValuacion').modal('hide');
    $('#Slider_' + hfId_Cte).empty();
    CrearTablaHija(hfId_Cte, 0, 1);
    alertify.success('La valuación ' + id + ' ha sido creada con éxito.');
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function cerrarVentanaValuacion_Actualizada(id) {
    var hfId_Cte = $('#hfId_Cte').val();
    $('#dvModalValuacion').modal('hide');
    $('#Slider_' + hfId_Cte).empty();
    CrearTablaHija(hfId_Cte, 0, 1);
    alertify.success('La valuación ' + id + ' ha sido actualizada con éxito.');
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
$('#iframeVentanaValuacion').on('load', function () {
    console.log("C");
    if (_modoValuacion == 0) {
        $('#iframeVentanaValuacion')[0].contentWindow._externalCustomFn = cerrarVentanaValuacion_Generada;
    } else {
        $('#iframeVentanaValuacion')[0].contentWindow._externalCustomFn = cerrarVentanaValuacion_Actualizada;
    }

    $('#dvCuerpoVentanaValuacion').unblock();
});

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function EditarValuacionGlobal(idEmp, idCd, idVal, idCte) {
    //Se utiliza para determinar la rutina a llamar después de que la operación de la valuación (nueva o edición) termine con éxito.
    _modoValuacionGlobal = 1;
    $('#iframeVentanaValuacionGlobal').attr('src', 'Valuaciones/CapValGlobalProyectos.aspx?Id_Vap=' + idVal + '&Id_Emp=' + idEmp + '&Id_Cd=' + idCd + '&permisoGuardar=1&permisoModificar=1&permisoEliminar=1&permisoImprimir=1&modificable=1&Id_Cte=' + idCte);
    $('#dvCuerpoVentanaValuacionGlobal').block({ message: 'Cargando...' });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function cerrarVentanaValuacionGlobal_Generada(id) {
    $('#dvModalValuacionGlobal').modal('hide');
    recargarListadoProyectos();
    alertify.success('La valuación global ' + id + ' ha sido creada con éxito');
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function cerrarVentanaValuacionGlobal_Actualizada(id) {
    $('#dvModalValuacionGlobal').modal('hide');
    recargarListadoProyectos();
    alertify.success('La valuación global ' + id + ' ha sido actualizada con éxito(2).');
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function editarValuacion(idEmp, idCd, idVal, idCte, SoloLectura) {

    var params =
        'Parametro_IdTU=' + _Parametro_IdTU +
        '&Parametro_IdRik=' + _Parametro_IdRik +
        '&CRM_Gerente_Id=' + _CRM_Gerente_Id +
        '&CRM_Gerente_Rik=' + _CRM_Gerente_Rik +
        '&CRM_Usuario_Id=' + _CRM_Usuario_Id +
        '&CRM_Usuario_Rik=' + _CRM_Usuario_Rik;

    _modoValuacion = 1;
    $('#iframeVentanaValuacion').attr('src', '../../CapValProyectosCRMII.aspx?' + params + '&SoloLectura=' + SoloLectura + '&Id_Vap=' + idVal + '&Id_Emp=' + idEmp + '&Id_Cd=' + idCd + '&permisoGuardar=1&permisoModificar=1&permisoEliminar=1&permisoImprimir=1&modificable=1&Id_Cte=' + idCte);
    $('#dvCuerpoVentanaValuacion').block({ message: 'Cargando...' });
}


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function bloquearProyecto(idCte) {

    var params =
        'Parametro_IdTU=' + _Parametro_IdTU +
        '&Parametro_IdRik=' + _Parametro_IdRik +
        '&CRM_Gerente_Id=' + _CRM_Gerente_Id +
        '&CRM_Gerente_Rik=' + _CRM_Gerente_Rik +
        '&CRM_Usuario_Id=' + _CRM_Usuario_Id +
        '&CRM_Usuario_Rik=' + _CRM_Usuario_Rik;

    alertify.success('Valuación: ' + _Parametro_ControlesSoloLectura);
    var SoloLectura = _Parametro_ControlesSoloLectura;

    _modoValuacion = 0;
    $('#iframeVentanaValuacion').attr('src', '../../CapValProyectosCRMII.aspx?' + params + '&SoloLectura=' + SoloLectura + '&Id_Vap=0&Id_Emp=0&Id_Cd=0&permisoGuardar=1&permisoModificar=1&permisoEliminar=1&permisoImprimir=1&modificable=1&Id_Cte=' + idCte);
    $('#dvCuerpoVentanaValuacion').block({ message: 'Cargando...' });

    //            _proyectoSeleccionado.EnValuacion=true;
    //            $.ajax({
    //                url: '<%=ApplicationUrl %>' + '/api/CrmProyecto',
    //                type: 'PUT',
    //                cache: false,
    //                data: JSON.stringify(_proyectoSeleccionado),
    //                contentType: 'application/json',
    //                statusCode: {
    //                    401: function (jqXHR, textStatus, errorThrown) {
    //                        $('#dvDialogoInicioSesion').modal();
    //                        _onLoginSuccessful = $.proxy(bloquearProyecto, this, idCte);
    //                    }
    //                }
    //            }).done(function (response, textStatus, jqXHR) {
    //                $('#iframeVentanaValuacion').attr('src', '../../CapValProyectosCRMII.aspx?Id_Vap=0&Id_Emp=0&Id_Cd=0&permisoGuardar=1&permisoModificar=1&permisoEliminar=1&permisoImprimir=1&modificable=1&Id_Cte=' + idCte);
    //                _cargarProductosDeProyecto(_proyectoSeleccionado.Id, _proyectoSeleccionado.Id_Cte);
    //                $('#dvCuerpoVentanaValuacion').block({message: 'Cargando...'});
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
    //                        }, 3006);
    //                        break;
    //                }
    //            }).always(function (jqXHROrData, textStatus, errorOrJQXHR) {
    //            });
    //Se utiliza para determinar la rutina a llamar después de que la operación de la valuación (nueva o edición) termine con éxito.
}


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function VerValuacion(obj) {
    var idCte = $(obj).data('idcte');
    var modo = $(obj).data('modo');

    alertify.success('Se visualiza valuación en modo: (' + _Parametro_ControlesSoloLectura + ')');

    $('#dvModalValuacion').on('show.bs.modal', function (event) {
        if (modo == 0) {
            bloquearProyecto(idCte);
        } else {
            var idVal = $(obj).data('idval');
            // * _Parametro_ControlesSoloLectura del gerente
            editarValuacion(_EntidadSesion_Id_Emp, _EntidadSesion_Id_Cd, idVal, idCte, _Parametro_ControlesSoloLectura);
        }
    }).modal('show');
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Generar_Valuacion(obj) {
    var Id_Cte = $(obj).data('id_cte');
    var modo = $(obj).data('modo');
    var Id_Vap = $(obj).data('valuacion');
    var Id_Ter = $(obj).data('id_ter');
    var Id_Op = $(obj).data('id_op');
    var Tipo = $(obj).data('tipo'); // 1 Valuacion Indiviudal / 2 Valuacion Global
    var Id_Emp = _EntidadSesion_Id_Emp;
    var Id_Cd = _EntidadSesion_Id_Cd
    var SoloLectura = _Parametro_ControlesSoloLectura;

    $('#hfId_Cte').val(Id_Cte);

    //alertify.success('Se visualiza valuación en modo: (' + _Parametro_ControlesSoloLectura+')');    
    $('#dvModalValuacion').on('show.bs.modal', function (event) {

        //editarValuacion(_EntidadSesion_Id_Emp, _EntidadSesion_Id_Cd, idVal, id_Cte, _Parametro_ControlesSoloLectura);
        //function editarValuacion(idEmp, idCd, idVal, idCte, SoloLectura) {

        var params =
            'Id_Emp=' + Id_Emp +
            '&Id_Cd=' + Id_Cd +
            '&Tipo=' + Tipo + // 1 Val Individual , 2 Val Global
            '&Id_Vap=' + Id_Vap +
            '&Id_Ter=' + Id_Ter +
            '&Id_Cte=' + Id_Cte +
            '&Id_Op=' + Id_Op +
            '&Parametro_IdTU=' + _Parametro_IdTU +
            '&Parametro_IdRik=' + _Parametro_IdRik +
            '&CRM_Gerente_Id=' + _CRM_Gerente_Id +
            '&CRM_Gerente_Rik=' + _CRM_Gerente_Rik +
            '&CRM_Usuario_Id=' + _CRM_Usuario_Id +
            '&CRM_Usuario_Rik=' + _CRM_Usuario_Rik +
            '&SoloLectura=' + SoloLectura +
            '&permisoGuardar=1' +
            '&permisoModificar=1' +
            '&permisoEliminar=1' +
            '&permisoImprimir=1' +
            '&modificable=1';

        _modoValuacion = 1;

        $('#iframeVentanaValuacion').attr('src', '../../CapValProyectosCRMII.aspx?' + params);

        //$('#dvCuerpoVentanaValuacion').block({ message: 'Cargando...' });        
        /*
            if (modo == 0) {
                bloquearProyecto(Id_Cte);
            } else {            
                // * _Parametro_ControlesSoloLectura del gerente
                editarValuacion(_EntidadSesion_Id_Emp, _EntidadSesion_Id_Cd, idVal, id_Cte, _Parametro_ControlesSoloLectura);
            }
        */

    }).modal('show');
}


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function EditarValuacionGlobal(idEmp, idCd, idVal, idCte) {
    _modoValuacionGlobal = 1;
    $('#iframeVentanaValuacionGlobal').attr('src', 'Valuaciones/CapValGlobalProyectos.aspx?Id_Vap=' + idVal + '&Id_Emp=' + idEmp + '&Id_Cd=' + idCd + '&permisoGuardar=1&permisoModificar=1&permisoEliminar=1&permisoImprimir=1&modificable=1&Id_Cte=' + idCte);
    $('#dvCuerpoVentanaValuacionGlobal').block({ message: 'Cargando...' });
}


/*
// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
$('#dvModalValuacion').on('show.bs.modal', function (event) {
    var idCte = $(event.relatedTarget).data('idcte');
    var modo = $(event.relatedTarget).data('modo');
    if (modo == 0) {
        bloquearProyecto(idCte/*_clienteDeOportunidad*);
    } else {
        var idVal = $(event.relatedTarget).data('idval');
        //editarValuacion(<%=EntidadSesion.Id_Emp %>, <%=EntidadSesion.Id_Cd %>, idVal, idCte);
        editarValuacion(EntidadSesion_Id_Emp, EntidadSesion_Id_Cd, idVal, idCte);

    }
});
*/

   // Valuacion 
/*
$('#dvModalValuacion').on('show.bs.modal', function(event){
    var idCte=$(event.relatedTarget).data('idcte');
    var modo=$(event.relatedTarget).data('modo');
    if(modo==0){
        bloquearProyecto(idCte/*_clienteDeOportunidad*);
    }else{
        var idVal=$(event.relatedTarget).data('idval');
    editarValuacion(<%=EntidadSesion.Id_Emp %>, <%=EntidadSesion.Id_Cd %>, idVal, idCte);
    }
});
*/