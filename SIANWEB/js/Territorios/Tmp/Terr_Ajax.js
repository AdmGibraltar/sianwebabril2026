
// 

function Territorios_CargarIndice(
    Ter_Pag_Actual, Ter_Pag_PageSize,  
    TextoBuscar, TipoRepresentante, Ter_Activo,
    CALLBACK_Exito, CALLBACK_Error
) {

    $('#spinner_Indice').css('display', 'block');

    var ajax_urls = _ApplicationUrl + '/api/Territorio/?' +
        'PageNumber=' + Ter_Pag_Actual +
        '&PageSize=' + Ter_Pag_PageSize +
        '&TextoBuscar=' + TextoBuscar +
        '&TipoRepresentante=' + TipoRepresentante +
        '&Ter_Activo='+Ter_Activo;

    $.ajax({
        url: ajax_urls,
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
        var lst = response.Datos;
        var Estado = response.Estado;
        var Mensaje = response.Mensaje;

        try {
            ACyS_RegistroEncontrados = lst[0].RecordCount;
        } catch (err) {
            ACyS_RegistroEncontrados = 0;
        }
        //console.log('ACyS_RegistroEncontrados:' + ACyS_RegistroEncontrados);
        if (Estado == 1) {
            if (CALLBACK_Exito) {
                CALLBACK_Exito(lst);
                $('#spinner_Indice').css('display', 'none');
            }
        } else {
            alertify.error('Error Territorios_CargarIndice::' + Mensaje);
        }

    }).fail(function (jqXHR, textStatus, error) {
        $('#spinner_Indice').css('display', 'none');

        if (jqXHR.status == 401) {
            alert('La sessión ha expirado.');
            //$('#dvModalPropuestaTE_ver2').modal('hide');
            $('#dvDialogoInicioSesion').appendTo("body").modal('show');
        } else {
            alertify.error('Error:' + jqXHR.responseText);
            if (CALLBACK_Error) {
                CALLBACK_Error();
            }
        }
    });
}


//

function Territorios_CargarById(Id_Ter, CALLBACK_Exito, CALLBACK_Error) {
    var ajax_urls = _ApplicationUrl + '/api/Territorio/Get?Id_Ter=' + Id_Ter+'&Par1=0';
    $.ajax({
        url: ajax_urls,
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
        var Rec = response.Datos;
        var Estado = response.Estado;
        var Mensaje = response.Mensaje;

        if (Estado == 1) {
            if (CALLBACK_Exito) {
                CALLBACK_Exito(Rec);
            }
        } else {
            alertify.error('Error Territorios_CargarIndice::' + Mensaje);
        }

    }).fail(function (jqXHR, textStatus, error) {

        if (jqXHR.status == 401) {
            alert('La sessión ha expirado.');
            //$('#dvModalPropuestaTE_ver2').modal('hide');
            $('#dvDialogoInicioSesion').appendTo("body").modal('show');
        } else {
            alertify.error('Error:' + jqXHR.responseText);
            if (CALLBACK_Error) {
                CALLBACK_Error();
            }
        }
    });
}

//

function Territorio_CrearSolicitud(
    ClaveTerritorio, IdRepresentantes, Territorio, Activo,
    CALLBACK_Exito, CALLBACK_Error) {
                                                              
    var ajax_urls = _ApplicationUrl + '/api/CatTerritorio/Territorio_CrearSolicitud?'+                                                                              
    'ClaveTerritorio='+ClaveTerritorio+'&IdRepresentante='+IdRepresentantes+'&Territorio='+Territorio+'&Activo='+Activo;

    $.ajax({
        url: ajax_urls,
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
        var Rec = response.Datos;
        var Estado = response.Estado;
        var Mensaje = response.Mensaje;
        if (Estado == 1) {
            if (CALLBACK_Exito) {
                CALLBACK_Exito(Rec);
            }
        } else {
            alertify.error('Error Territorios_CargarIndice::' + Mensaje);
        }

    }).fail(function (jqXHR, textStatus, error) {

        if (jqXHR.status == 401) {
            alert('La sessión ha expirado.');
            //$('#dvModalPropuestaTE_ver2').modal('hide');
            $('#dvDialogoInicioSesion').appendTo("body").modal('show');
        } else {
            alertify.error('Error:' + jqXHR.responseText);
            if (CALLBACK_Error) {
                CALLBACK_Error();
            }
        }
    });
}

// GUARDAR TERRITORIO 

function Territorio_Crear(DataTer,        
    CALLBACK_Exito, CALLBACK_Error) {

    switch (DataTer.TipoTerritorio) {
        case 1: 
        case 2: 
            DataTer.Segmento = -1;
            DataTer.UEN= -1;
        break;
        case 3: 
        case 4:             
        break;
    }
                                                          
    var ajax_urls = _ApplicationUrl + '/api/CatTerritorio/Territorio_InsertUpdate?'+     
    'IdInsertUpdate=' + DataTer.IdInsertUpdate +
    '&Id_Ter=' + DataTer.Id_Ter +
    '&Ter_Nombre=' + DataTer.Ter_Nombre + 
    '&Id_Uen=' + DataTer.Id_Uen +
    '&Id_Rik=' + DataTer.Id_Rik+
    '&Id_Seg=' + DataTer.Id_Seg +
    '&Id_TipoCliente=' + DataTer.Id_TipoCliente +
    '&Id_Local=' + DataTer.Id_Local +
    '&Id_TipoRepresentante=' + DataTer.Id_TipoRepresentante +
    '&Ter_Activo='+DataTer.Ter_Activo +
    '&Cve_Terr='+ DataTer.Cve_Ter;
        
    $.ajax({
        url: ajax_urls,
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
        var Rec = response.Datos;
        var Estado = response.Estado;
        var Mensaje = response.Mensaje;
        if (Estado == 1 || Estado == 2) {
            if (CALLBACK_Exito) {
                CALLBACK_Exito(Rec);
            }
        } else {
            alertify.error('Error Territorios_CargarIndice::' + Mensaje);
        }

    }).fail(function (jqXHR, textStatus, error) {

        if (jqXHR.status == 401) {
            alert('La sessión ha expirado.');
            //$('#dvModalPropuestaTE_ver2').modal('hide');
            $('#dvDialogoInicioSesion').appendTo("body").modal('show');
        } else {
            alertify.error('Error:' + jqXHR.responseText);
            if (CALLBACK_Error) {
                CALLBACK_Error();
            }
        }
    });
}


//

function Territorio_Maximo(Prefix, TipoRep, TipoCliente, IdUen, IdSeg, CALLBACK_Exito, CALLBACK_Error) {

    var ajax_urls = _ApplicationUrl + '/api/CatTerritorio/Territorio_Maximo?Prefix=' +
    Prefix + '&TipoRep=' + TipoRep + '&TipoCliente=' + TipoCliente + '&IdUen=' + IdUen + '&IdSeg=' + IdSeg;

    $.ajax({
        url: ajax_urls,
        cache: false,
        type: 'GET',
        async:false,
    }).done(function (response, textStatus, jqXHR) {
        var Rec = response.Datos;
        var Estado = response.Estado;
        var Mensaje = response.Mensaje;

        if (Estado == 1) {
            if (CALLBACK_Exito) {
                CALLBACK_Exito(Rec);
            }
        } else {
            alertify.error('Error Territorios_CargarIndice::' + Mensaje);
        }

    }).fail(function (jqXHR, textStatus, error) {

        if (jqXHR.status == 401) {
            alert('La sessión ha expirado.');
            //$('#dvModalPropuestaTE_ver2').modal('hide');
            $('#dvDialogoInicioSesion').appendTo("body").modal('show');
        } else {
            alertify.error('Error:' + jqXHR.responseText);
            if (CALLBACK_Error) {
                CALLBACK_Error();
            }
        }
    });
}

// TERRITORIO DETALLE 

function Territorio_Detalle(Id_Ter, ParametroBusqueda, CALLBACK_Exito, CALLBACK_Error) {

    var ajax_urls = _ApplicationUrl + '/api/CatTerritorio/ConsultaTerritoriosCliente_ById?ClaveTerritorio=' +Id_Ter+'&ParametroBusqueda='+ParametroBusqueda;

    $.ajax({
        url: ajax_urls,
        cache: false,
        type: 'GET',
        async:false,
    }).done(function (response, textStatus, jqXHR) {
        var Rec = response.Datos;
        var Estado = response.Estado;
        var Mensaje = response.Mensaje;

        if (Estado == 1) {
            if (CALLBACK_Exito) {
                CALLBACK_Exito(Rec);
            }
        } else {
            alertify.error('Error Territorio_Detalle:' + Mensaje);
        }

    }).fail(function (jqXHR, textStatus, error) {

        if (jqXHR.status == 401) {
            alert('La sessión ha expirado.');
            //$('#dvModalPropuestaTE_ver2').modal('hide');
            $('#dvDialogoInicioSesion').appendTo("body").modal('show');
        } else {
            alertify.error('Error:' + jqXHR.responseText);
            if (CALLBACK_Error) {
                CALLBACK_Error();
            }
        }
    });
}
