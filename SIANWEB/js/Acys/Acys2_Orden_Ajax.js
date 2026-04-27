/*

Key Quimica Dic 2018 

24 Dic 2018 RFH 

*/


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Cargar_Combo_FrecuenciaVisita(Selector, CALLBACK) {
    $.ajax({                         
        url: _ApplicationUrl + '/api/CatFrecuenciaVisita/Listado?Id=0',
        cache: false,
        type: 'GET',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').appendTo("body").modal();
                _onLoginSuccessful = $.proxy(Cargar_Combo_FrecuenciaVisita, null, $, Selector, CALLBACK);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        var RES = response.Datos;
        var Estado = response.Estado;

        if (Estado == 1) {            
            //var Selector= Selector.clear();
            Selector.empty();
            Selector.append('<option value="0">-</option>');
            for (var i=0; i<RES.length; i++) {
                Selector.append('<option value='+RES[i].Id_Frecuencia+'>'+RES[i].Frecuencia+'</option>');
            }
            Selector.val(0);

            if (CALLBACK) {
                CALLBACK();
            }
        }    

    }).fail(function (jqXHR, textStatus, error) {
        if (jqXHR.status == 401) {
            alert('La sessión ha expirado.');
            $('#dvModalPropuestaTE_ver2').modal('hide');
            $('#dvDialogoInicioSesion').appendTo("body").modal('show');            
        } else {
            alertify.success('Ocurrió una error: funcion Cargar_Combo_FrecuenciaVisita.');
        }
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Cargar_ComboTipo_TipoServicio(IdTipoServicio, Selector, CALLBACK) {
    $.ajax({
        url: _ApplicationUrl + '/api/CapAcysServicioValorTipo/?IdTipoServicio=' + IdTipoServicio,
        cache: false,
        type: 'GET',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').appendTo("body").modal();
                _onLoginSuccessful = $.proxy(Cargar_ComboTipo_TipoServicio, null, $, IdTipoServicio, Selector, CALLBACK);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        var RES = response.Datos;
        var Estado = response.Estado;

        if (Estado == 1) {            
            //var Selector= Selector.clear();
            Selector.empty();
            Selector.append('<option value="0">-</option>');
            for (var i=0; i<RES.length; i++) {
                Selector.append('<option value='+RES[i].IdCapAcys_ServicioValor_Tipo+'>'+RES[i].TipoServicioNombre+'</option>');
            }
            Selector.val(0);

            if (CALLBACK) {
                CALLBACK();
            }
        }    

    }).fail(function (jqXHR, textStatus, error) {
        if (jqXHR.status == 401) {
            alert('La sessión ha expirado.');
            $('#dvModalPropuestaTE_ver2').modal('hide');
            $('#dvDialogoInicioSesion').appendTo("body").modal('show');            
        } else {
            alertify.success('Ocurrió una error: funcion Cargar_ComboTipo_TipoServicio.');
        }
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// Hay dos funciones omo esta una para cada documento
// 8 Oct 2018 RFH Actualizado
function Cargar_IndiceOrden_Ajax(
    PageNumber, PageSize,
    AplicaFecha, FechaIni, FechaFin,
    AplicaFolio, FolioIni, FolioFin, CALLBACK, CALLBACK_Error
) {
    
    var Territorio = 0;
    var IdCte = 0;
    var IdCte = $('#tbNumeroCliente').val();
    IdCte = parseInt(IdCte);
    if (isNaN(IdCte)) {
        IdCte = 0;
    }
    
    var IdRik = 0;
        
    if (AplicaFecha) {
        AplicaFecha = 1;
    } else {
        AplicaFecha = 0;
    }

    if (AplicaFolio) {
        AplicaFolio = 1;
    } else {
        AplicaFolio = 0;
    }

    FolioIni = parseInt(FolioIni);
    if (isNaN(FolioIni)) {
        FolioIni = 0;
    }

    FolioFin = parseInt(FolioFin);
    if (isNaN(FolioFin)) {
        FolioFin = 0;
    }
    
    ddlEstatus = $('#ddlEstatus').val();
    ddlModalidad = $('#ddlModalidad').val();
    
    if (typeof (PageNumber) != "undefined") {
        $.ajax({
            url: _ApplicationUrl + '/api/CatAcysOrden/' +
            '?' +
            'PageNumber=' + PageNumber +
            '&PageSize=' + PageSize +
            '&AplicaFecha=' + AplicaFecha +
            '&FechaIni=' + FechaIni +
            '&FechaFin=' + FechaFin +
            '&AplicaFolio=' + AplicaFolio +
            '&FolioIni=' + FolioIni +
            '&FolioFin=' + FolioFin +
            '&Estatus=' + ddlEstatus +
            '&Territorio=' + Territorio +
            '&IdCte=' + IdCte +
            '&IdRik=' + IdRik +
            '&Id_Modalidad=' + ddlModalidad,
            cache: false,
            type: 'GET',
            statusCode: {
                401: function (jqXHR, textStatus, errorThrown) {
                    $('#dvDialogoInicioSesion').appendTo("body").modal();
                    _onLoginSuccessful = $.proxy(Cargar_IndiceOrden_Ajax, null, $, PageNumber, PageSize,
                    AplicaFecha, FechaIni, FechaFin,
                    AplicaFolio, FolioIni, FolioFin, CALLBACK, CALLBACK_Error);
                }
            }
        }).done(function (response, textStatus, jqXHR) {
            //Export_Excel_Informe1(response);
            //console.log(response);
            lst = response;

            try {
                ACyS_RegistroEncontrados = lst[0].RecordCount;
            } catch (err) {
                ACyS_RegistroEncontrados = 0;
            }
            
            console.log('ACyS_RegistroEncontrados:' + ACyS_RegistroEncontrados);

            if (CALLBACK) {
                CALLBACK(lst);
            }
        }).fail(function (jqXHR, textStatus, error) {

            if (CALLBACK_Error) {
                CALLBACK_Error();
            }

            if (jqXHR.status == 401) {
                alert('La sessión ha expirado.');
                $('#dvModalPropuestaTE_ver2').modal('hide');
                $('#dvDialogoInicioSesion').appendTo("body").modal('show');
            } else {
                alertify.success('Ocurrió una error: funcion Cargar_IndiceOrden_Ajax.<br>' + jqXHR.responseText);
                console.log(jqXHR.responseText);
            }
        });
    }
    //
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Cargar_OrdenById_Ajax(Id_Acys, Acs_Version, CALLBACK) {

    var Estatus = '';
    var FolioIni = '';
    var FolioFin = '';
    var Territorio = 0;
    var IdCte = 0;
    var IdRik = 0;

    $.ajax({
        url: _ApplicationUrl + '/api/CatAcys/' +
        '?IdAcys=' + Id_Acys +
        '&AcsVersion=' + Acs_Version,
        cache: false,
        type: 'GET',
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').appendTo("body").modal();
                _onLoginSuccessful = $.proxy(Cargar_OrdenById_Ajax, null, $, Id_Acys, Acs_Version, CALLBACK);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        //Export_Excel_Informe1(response);
        //console.log(response);
        var RES = response.Datos;
        var Estado = response.Estado;

        if (Estado == 1) {
            if (CALLBACK) {
                CALLBACK(RES);
            }
        } else {
            alertify.success('Error: Cargar_OrdenById_Ajax();');
        }

    }).fail(function (jqXHR, textStatus, error) {
        if (jqXHR.status == 401) {
            alert('La sessión ha expirado.');
            $('#dvModalPropuestaTE_ver2').modal('hide');
            $('#dvDialogoInicioSesion').appendTo("body").modal('show');
        } else {
            alertify.success('Ocurrió una error: funcion Cargar_OrdenById_Ajax.');
        }
    });
}


// 
//
//
function AcysOrden_Guargar_Ajax(AcysOrden, Spinner, CALLBACK) {    
    Spinner.css('display','block');
    $.ajax({
        // CatAcysOrdenController.cs ->
        url: _ApplicationUrl + '/api/CatAcysOrden/InsertUpdate',
        data: JSON.stringify(AcysOrden),
        cache: false,
        type: 'PUT',
        contentType: "application/json; utf-8",
        dataType: "json",
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').appendTo("body").modal();
                _onLoginSuccessful = $.proxy(AcysOrden_Guargar_Ajax, null, $, AcysOrden, Spinner, CALLBACK);
            }
        }
    }).done(function (response, textStatus, jqXHR) {
        Spinner.css('display','none');        
        var RES = response;
        if (CALLBACK) {
            CALLBACK(RES);
        }
    }).fail(function (jqXHR, textStatus, error) {
        Spinner.css('display','none');
        if (jqXHR.status == 401) {
            alert('La sessión ha expirado.');
            $('#dvModalPropuestaTE_ver2').modal('hide');
            $('#dvDialogoInicioSesion').appendTo("body").modal('show');            
        } else {
            alertify.success('Ocurrió una error: funcion AcysOrden_Guargar_Ajax.');
        }
    });
    //
}

