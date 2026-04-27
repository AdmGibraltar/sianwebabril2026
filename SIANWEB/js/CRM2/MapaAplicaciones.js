/*
    Key Quimica 
    14Jun-2019 RFH 

*/

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// UEN 
function Cargar_ddlUEN(CallBack_Exito) {

    $('#Spinner_MapaAplicaciones').css('display', 'block');

    $.ajax({
        url: _ApplicationUrl + '/api/CatUEN',
        data: {
            Id_Emp: 0, Id_Uen: 0
        },
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {

        var Estado = response.Estado;
        var Datos = response.Datos;
        if (CallBack_Exito) {
            $('#Spinner_MapaAplicaciones').css('display', 'none');
            CallBack_Exito(Datos);
        }

    }).fail(function (jqXHR, textStatus, error) {
        $('#Spinner_MapaAplicaciones').css('display', 'none');
        alertify.error('Error: Carga de representantes');
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// SEGMENTO
function Cargar_ddlSegmento(idUen, CallBack_Exito) {
    $('#Spinner_MapaAplicaciones').css('display', 'block');
    $.ajax({
        url: _ApplicationUrl + '/api/CatSegmento',
        data: {
            Id_Emp: 0, Id_Uen: idUen
        },
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {

        var Estado = response.Estado;
        var Datos = response.Datos;

        if (CallBack_Exito) {
            $('#Spinner_MapaAplicaciones').css('display', 'none');
            CallBack_Exito(Datos);
        }

    }).fail(function (jqXHR, textStatus, error) {
        $('#Spinner_MapaAplicaciones').css('display', 'none');
        alertify.error('Error: Cargar_ddlSegmento');
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// AREA
function Cargar_ddlArea(idSeg, CallBack_Exito) {
    $('#Spinner_MapaAplicaciones').css('display', 'block');
    $.ajax({
        url: _ApplicationUrl + '/api/CatArea',
        data: {
            idEmp: 0, idSeg: idSeg
        },
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {

        if (CallBack_Exito) {
            $('#Spinner_MapaAplicaciones').css('display', 'none');
            CallBack_Exito(response);
        }

    }).fail(function (jqXHR, textStatus, error) {
        $('#Spinner_MapaAplicaciones').css('display', 'none');
        alertify.error('Error: Cargar_ddlAplicacion');
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// SOLUCION
function Cargar_ddlSolucion(idArea, CallBack_Exito) {
    $('#Spinner_MapaAplicaciones').css('display', 'block');
    $.ajax({
        url: _ApplicationUrl + '/api/CatSolucion',
        data: {
            idEmp: 0, idArea: idArea
        },
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {

        if (CallBack_Exito) {
            $('#Spinner_MapaAplicaciones').css('display', 'none');
            CallBack_Exito(response);
        }

    }).fail(function (jqXHR, textStatus, error) {
        $('#Spinner_MapaAplicaciones').css('display', 'none');
        alertify.error('Error: Cargar_ddlAplicacion');
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// APLICACION 
function Cargar_ddlAplicacion(idUen, idSol, CallBack_Exito) {
    $('#Spinner_MapaAplicaciones').css('display', 'block');
    $.ajax({
        url: _ApplicationUrl + '/api/CatAplicacion',
        data: {
            idEmp: 0, idSol: idSol
        },
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {

        if (CallBack_Exito) {
            $('#Spinner_MapaAplicaciones').css('display', 'none');
            CallBack_Exito(response);
        }

    }).fail(function (jqXHR, textStatus, error) {
        $('#Spinner_MapaAplicaciones').css('display', 'none');
        alertify.error('Error: Cargar_ddlAplicacion');
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Cargar_UEN_SEG_ARE_SOL_APL() {
    Cargar_ddlUEN(function (lst) {
        $('#ddlUEN').empty();
        $('#ddlUEN').append('<option value="0" >-- Todos -- </option>');
        for (i = 0; i < lst.length; i++) {
            $('#ddlUEN').append('<option value="' + lst[i].Id_Uen + '" >' + lst[i].Descripcion + '</option>');
        }
        var Id_Uen = $('#ddlUEN').val();
        Cargar_ddlSegmento(Id_Uen, function (lst) {
            $('#ddlSegmento').empty();
            $('#ddlSegmento').append('<option value="0" >-- Todos -- </option>');
            for (i = 0; i < lst.length; i++) {
                $('#ddlSegmento').append('<option value="' + lst[i].Id_Seg + '" >' + lst[i].Seg_Descripcion + '</option>');
            }
            var Id_Seg = $('#ddlSegmento').val();
            Cargar_ddlArea(Id_Seg, function (lst) {
                $('#ddlArea').empty();
                $('#ddlArea').append('<option value="0" >-- Todos -- </option>');
                for (i = 0; i < lst.length; i++) {
                    $('#ddlArea').append('<option value="' + lst[i].Id_Area+ '" >' + lst[i].Area_Descripcion + '</option>');
                }
                var Id_Area = $('#ddlArea').val();
                Cargar_ddlSolucion(Id_Area, function (lst) {
                    $('#ddlSolucion').empty();
                    $('#ddlSolucion').append('<option value="0" >-- Todos -- </option>');
                    for (i = 0; i < lst.length; i++) {
                        $('#ddlSolucion').append('<option value="' + lst[i].Id_Sol + '" >' + lst[i].Sol_Descripcion + '</option>');
                    }
                    var Id_Sol = $('#ddlSolucion').val();
                    $('#ddlAplicacion').append('<option value="0" >-- Todos -- </option>');
                    Cargar_ddlAplicacion(0, Id_Sol, function (lst) {
                        $('#ddlAplicacion').empty();
                        for (i = 0; i < lst.length; i++) {
                            $('#ddlAplicacion').append('<option value="' + lst[i].Id_Apl + '" >' + lst[i].Apl_Descripcion + '</option>');
                        }
                    });
                });
            });
        });
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Cargar_SEG_ARE_SOL_APL(Id_Uen) {            
    Cargar_ddlSegmento(Id_Uen, function (lst) {
        $('#ddlSegmento').empty();
        $('#ddlSegmento').append('<option value="0" >-- Todos -- </option>');
            for (i = 0; i < lst.length; i++) {
                $('#ddlSegmento').append('<option value="' + lst[i].Id_Seg + '" >' + lst[i].Seg_Descripcion + '</option>');
            }
            var Id_Seg = $('#ddlSegmento').val();
            Cargar_ddlArea(Id_Seg, function (lst) {
                $('#ddlArea').empty();
                $('#ddlArea').append('<option value="0" >-- Todos -- </option>');
                for (i = 0; i < lst.length; i++) {
                    $('#ddlArea').append('<option value="' + lst[i].Id_Area + '" >' + lst[i].Area_Descripcion + '</option>');
                }
                var Id_Area = $('#ddlArea').val();
                Cargar_ddlSolucion(Id_Area, function (lst) {
                    $('#ddlSolucion').empty();
                    $('#ddlSolucion').append('<option value="0" >-- Todos -- </option>');
                    for (i = 0; i < lst.length; i++) {
                        $('#ddlSolucion').append('<option value="' + lst[i].Id_Sol + '" >' + lst[i].Sol_Descripcion + '</option>');
                    }
                    var Id_Sol = $('#ddlSolucion').val();
                    Cargar_ddlAplicacion(0, Id_Sol, function (lst) {
                        $('#ddlAplicacion').empty();
                        $('#ddlAplicacion').append('<option value="0" >-- Todos -- </option>');
                        for (i = 0; i < lst.length; i++) {
                            $('#ddlAplicacion').append('<option value="' + lst[i].Id_Apl + '" >' + lst[i].Apl_Descripcion + '</option>');
                        }
                    });
                });
            });
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Cargar_ARE_SOL_APL(Id_Seg) {               
    Cargar_ddlArea(Id_Seg, function (lst) {
        $('#ddlArea').empty();
        $('#ddlArea').append('<option value="0" >-- Todos -- </option>');
        for (i = 0; i < lst.length; i++) {
            $('#ddlArea').append('<option value="' + lst[i].Id_Area + '" >' + lst[i].Area_Descripcion + '</option>');
        }
        var Id_Area = $('#ddlArea').val();
        Cargar_ddlSolucion(Id_Area, function (lst) {
            $('#ddlSolucion').empty();
            $('#ddlSolucion').append('<option value="0" >-- Todos -- </option>');
            for (i = 0; i < lst.length; i++) {
                $('#ddlSolucion').append('<option value="' + lst[i].Id_Sol + '" >' + lst[i].Sol_Descripcion + '</option>');
            }
            var Id_Seg = $('#ddlSegmento').val();
            $('#ddlSegmento').append('<option value="0" >-- Todos -- </option>');
            Cargar_ddlAplicacion(0, Id_Seg, function (lst) {
                $('#ddlAplicacion').empty();
                $('#ddlAplicacion').append('<option value="0" >-- Todos -- </option>');
                for (i = 0; i < lst.length; i++) {
                    $('#ddlAplicacion').append('<option value="' + lst[i].Id_Apl + '" >' + lst[i].Apl_Descripcion + '</option>');
                }
            });
        });
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Cargar_SOL_APL(Id_Area) {
    Cargar_ddlSolucion(Id_Area, function (lst) {
        $('#ddlSolucion').empty();
        $('#ddlSolucion').append('<option value="0" >-- Todos -- </option>');
        for (i = 0; i < lst.length; i++) {
            $('#ddlSolucion').append('<option value="' + lst[i].Id_Sol + '" >' + lst[i].Sol_Descripcion + '</option>');
        }
        var Id_Seg = $('#ddlSegmento').val();
        Cargar_ddlAplicacion(0, Id_Seg, function (lst) {
            $('#ddlAplicacion').empty();
            $('#ddlAplicacion').append('<option value="0" >-- Todos -- </option>');
                for (i = 0; i < lst.length; i++) {
                    $('#ddlAplicacion').append('<option value="' + lst[i].Id_Apl + '" >' + lst[i].Apl_Descripcion + '</option>');
            }
        });
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Cargar_APL(Id_Sol) {    
    Cargar_ddlAplicacion(0, Id_Sol , function (lst) {
        $('#ddlAplicacion').empty();
        $('#ddlAplicacion').append('<option value="0" >-- Todos -- </option>');
        for (i = 0; i < lst.length; i++) {
            $('#ddlAplicacion').append('<option value="' + lst[i].Id_Apl + '" >' + lst[i].Apl_Descripcion + '</option>');
        }
    });
}


























