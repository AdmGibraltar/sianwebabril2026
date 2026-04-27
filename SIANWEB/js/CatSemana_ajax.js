// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function ConsultaSemanaActual(Fecha, CALLBACK_Exito, CALLBACK_Error) {
    
    $.ajax({                                   
        url: _ApplicationUrl + '/api/CatSemana/ConsultaSemanaActual?Fecha='+ Fecha,        
        cache: false,
        type: 'GET',                
    }).done(function (response, textStatus, jqXHR) {
        //Export_Excel_Informe1(response);
        //console.log(response);
        var RES = response.Datos;
        var Estado = response.Estado;

        if (Estado ==1) {
            CALLBACK_Exito(RES);            
        } else {
            CALLBACK_Error(RES);
        }

    }).fail(function (jqXHR, textStatus, error) {
        if (jqXHR.status == 401) {
            alert('La sessión ha expirado.');
            $('#dvModalPropuestaTE_ver2').modal('hide');
            $('#dvDialogoInicioSesion').modal();
        } else {
            alertify.error('Ocurrió una error: funcion Cargar_PropuestaTecnoEconomica.');
        }
    });
}