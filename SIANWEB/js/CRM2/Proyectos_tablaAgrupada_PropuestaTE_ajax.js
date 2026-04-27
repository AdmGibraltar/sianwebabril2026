

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// 7 Feb 2019
function InsertUpdate_ReporteParametros(
    Id_Op, tbNombreAtencion, tbNombreRik, tbRepresentanteClienteNombre,
    tbDireccion1, tbDireccion2,
    CALLBACK_Exito
) {
    $.ajax({
        url: _ApplicationUrl + '/api/CatPropuestaTecnoEconomicaEnc/GuardarCamposDeReporte?' +
            'Id_Op=' + Id_Op +
            '&tbNombreAtencion=' + tbNombreAtencion +
            '&tbRepresentanteClienteNombre=' + tbRepresentanteClienteNombre +
            '&tbNombreRik=' + tbNombreRik +
            '&tbDireccion1=' + tbDireccion1 +
            '&tbDireccion2=' + tbDireccion2,
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
        var Datos = response.Datos;
        var Estado = response.Estado;

        if (Datos == -1) {
            alertify.error('Error: Debe llenar todos los datos requeridos, NO se actualizo.');
        } else {
            if (Estado == 1) {
                if (CALLBACK_Exito) {
                    CALLBACK_Exito();
                }
            } else {
                alertify.error('Error: No fue posible actualizar la información.');
            }
        }
    }).fail(function (jqXHR, textStatus, error) {
        if (jqXHR.status == 401) {
            alert('La sessión ha expirado.');
            $('#dvModalPropuestaTE_ver2').modal('hide');
            $('#dvDialogoInicioSesion').modal();
        } else {
            alertify.error('Ocurrió una error: funcion Cargar_PropuestaTecnoEconomicaEnc.');
        }
    });
}


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// MAY25-2020 RFH
function InsertUpdate_ReporteParametros_Ver2(
    eCRM_ValuacionCamposAdicionales, CALLBACK_Exito, CALLBACK_Error
) {

    var sJSON = JSON.stringify(eCRM_ValuacionCamposAdicionales);

    $.ajax({
        //url: _ApplicationUrl + '/api/CatPropuestaTecnoEconomicaEnc/GuardarCamposDeReporte_Ver2',
        url: _ApplicationUrl + '/api/CatPropuestaTecnoEconomicaEnc/GuardarCamposDeReporte_Ver3',
        data: sJSON,
        cache: false,
        type: 'PUT',
        contentType: "application/json; utf-8",
    }).done(function (response, textStatus, jqXHR) {
        var Datos = response.Datos;
        var Estado = response.Estado;

        if (Datos == -1) {
            //alertify.error('Error: Debe llenar todos los datos requeridos, NO se actualizo.');       
            if (CALLBACK_Error) {
                CALLBACK_Error();
            }
        } else {
            if (Estado == 1) {
                if (CALLBACK_Exito) {
                    CALLBACK_Exito();
                }
            } else {
                alertify.error('Error: No fue posible actualizar la información.');
            }
        }
    }).fail(function (jqXHR, textStatus, error) {
        if (jqXHR.status == 401) {
            alert('La sessión ha expirado.');
            $('#dvModalPropuestaTE_ver2').modal('hide');
            $('#dvDialogoInicioSesion').modal();
        } else {
            alertify.error('Ocurrió una error: funcion Cargar_PropuestaTecnoEconomicaEnc.');
        }

        if (CALLBACK_Error) {
            CALLBACK_Error();
        }

    });
}



// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// 7 Feb 2019
function Get_ReporteParametros(Id_Op, CALLBACK_Exito, CALLBACK_Error) {
    $.ajax({
        url: _ApplicationUrl + '/api/CatPropuestaTecnoEconomicaEnc/GetCamposDeReporte_Ver2?Id_Op=' + Id_Op + '&Param1=0',
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
        var Datos = response.Datos;
        var Estado = response.Estado;
        if (Estado == 1) {
            if (CALLBACK_Exito) {
                CALLBACK_Exito(Datos);
            }
        } else {
            alertify.error('Error: No fue posible actualizar la información.');
        }
    }).fail(function (jqXHR, textStatus, error) {

        if (CALLBACK_Error) {
            CALLBACK_Error();
        }

        if (jqXHR.status == 401) {
            alert('La sessión ha expirado.');
            $('#dvModalPropuestaTE_ver2').modal('hide');
            $('#modalCamposDelReporte').modal('hide');
            $('#dvDialogoInicioSesion').modal();
        } else {
            alertify.error('Error: Get_ReporteParametros(...');
        }
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// Encabezado
function Cargar_PropuestaTecnoEconomicaEnc(CRM_Usuario_Rik, Id_Op, Id_Cte, Id_Val, CALLBACK) {

    $.ajax({
        url: _ApplicationUrl + '/api/CatPropuestaTecnoEconomicaEnc/?CRM_Usuario_Rik=' + CRM_Usuario_Rik + '&Enc=0&Id_Op=' + Id_Op + '&Id_Cte=' + Id_Cte + '&Id_Val=' + Id_Val,
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
        //Export_Excel_Informe1(response);
        //console.log(response);
        lst = response;
        var Vap_Estatus = lst.Vap_Estatus;
        var Vap_Estatus2 = lst.Vap_Estatus2;

        if (CALLBACK) {
            CALLBACK(Vap_Estatus, Vap_Estatus2);
        }

    }).fail(function (jqXHR, textStatus, error) {
        if (jqXHR.status == 401) {
            alert('La sessión ha expirado.');
            $('#dvModalPropuestaTE_ver2').modal('hide');
            $('#dvDialogoInicioSesion').modal();
        } else {
            alertify.error('Ocurrió una error: funcion Cargar_PropuestaTecnoEconomicaEnc.');
        }
    });
}

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
// Vwe 2
// Encabezado
//
function Cargar_PropuestaTecnoEconomicaEnc_Ver2(
    CRM_Usuario_Rik, Id_Op, Id_Cte, Id_Val, CALLBACK) {
    $.ajax({
        url: _ApplicationUrl + '/api/CatPropuestaTecnoEconomicaEnc/spCRMCapValProyecto?' +
            'CRM_Usuario_Rik=' + CRM_Usuario_Rik + '&Id_Op=' + Id_Op + '&Id_Cte=' + Id_Cte + '&Id_Val=' + Id_Val,
        cache: false,
        type: 'GET',
        async: false,
    }).done(function (response, textStatus, jqXHR) {
        var Datos = response.Datos;
        var Estado = response.Estado;
        var Mensaje = response.Mensaje;

        if (Estado == 1) {
            lst = response;
            //var Vap_Estatus = Datos.Vap_Estatus;
            //var Vap_Estatus2 = Datos.Vap_Estatus2;

            if (CALLBACK) {

                CALLBACK(Datos);
                //CALLBACK(Datos, Vap_Estatus, Vap_Estatus2);            

            } else {
                alertify.error('ERROR:' + Mensaje);
            }
        }

    }).fail(function (jqXHR, textStatus, error) {
        if (jqXHR.status == 401) {
            alert('La sessión ha expirado.');
            $('#dvModalPropuestaTE_ver2').modal('hide');
            $('#dvDialogoInicioSesion').modal();
        } else {
            alertify.error('Ocurrió una error: funcion Cargar_PropuestaTecnoEconomicaEnc.');
        }
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
// CRM - Propuesta TecnoEconomica - Detalle
//
function Cargar_PropuestaTecnoEconomica(
    CRM_Usuario_Rik, Id_Op, Id_Cte, Id_Val, CALLBACK) {

    $.ajax({
        url: _ApplicationUrl + '/api/CatPropuestaTecnoEconomica/?CRM_Usuario_Rik=' + CRM_Usuario_Rik + '&Id_Op=' + Id_Op + '&Id_Cte=' + Id_Cte + '&Id_Val=' + Id_Val,
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
        //Export_Excel_Informe1(response);
        //console.log(response);
        lst = response;

        if (CALLBACK) {
            CALLBACK();
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

// JUN12-2020 RFH Detalle Ver 2

function Cargar_PropuestaTecnoEconomica_Ver2(
    CRM_Usuario_Rik, Id_Op, Id_Cte, Id_Val, Id_Ptp, CALLBACK) {

    $.ajax({
        url: _ApplicationUrl + '/api/CatPropuestaTecnoEconomica/CRM_ObtenerPropuestaEconomica_Ver2?' +
            'CRM_Usuario_Rik=' + CRM_Usuario_Rik + '&Id_Op=' + Id_Op + '&Id_Cte=' + Id_Cte + '&Id_Val=' + Id_Val + '&Id_Ptp=' + Id_Ptp,
        cache: false,
        type: 'GET',
        async: false,
    }).done(function (response, textStatus, jqXHR) {
        var Datos = response.Datos;
        var Estado = response.Estado;
        var Mensaje = response.Mensaje;

        if (Estado == 1) {
            if (CALLBACK) {
                CALLBACK(Datos);
            }
        } else {
            alertify.error('Ocurrió una error al tratar de cargar el documento (240)');
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

// Detalle de Propuesta Tecno Economica - Ver 3 14AGO-2020 RFH
function Cargar_PropuestaTecnoEconomica_Ver3(
    CRM_Usuario_Rik, Id_Op, Id_Cte, Id_Val, Id_Ptp, CALLBACK) {

    $.ajax({
        url: _ApplicationUrl + '/api/CatPropuestaTecnoEconomica/CRM_ObtenerPropuestaEconomica_Ver3?' +
            'CRM_Usuario_Rik=' + CRM_Usuario_Rik + '&Id_Op=' + Id_Op + '&Id_Cte=' + Id_Cte + '&Id_Val=' + Id_Val + '&Id_Ptp=' + Id_Ptp + '&ParamVer3=0',
        cache: false,
        type: 'GET',
        async: false,
    }).done(function (response, textStatus, jqXHR) {
        var Datos = response.Datos;
        var Estado = response.Estado;
        var Mensaje = response.Mensaje;

        if (Estado == 1) {
            if (CALLBACK) {
                CALLBACK(Datos);
            }
        } else {
            alertify.error('Ocurrió una error al tratar de cargar el documento (240)');
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


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Update_OportunidadesProductos(
    Id_Op, Id_Val, Id_Cte, Id_Prd, Cantidad,
    AplDilucion, DilucionA, DilucionC, CPT_ProductoActual, CPT_SituacionActual,
    CPT_VentajasKey, CPT_RecursoImagenProductoActual, CPT_RecursoImagensolucionKey,
    COP_CostoEnUso, CALLBACK) {

    $.ajax({
        url: _ApplicationUrl + '/api/CatPropuestaTecnoEconomica/?' +
            'Id_Op=' + Id_Op +
            '&Id_Val=' + Id_Val +
            '&Id_Cte=' + Id_Cte +
            '&Id_Prd=' + Id_Prd +
            '&Cantidad=' + Cantidad +
            '&AplDilucion=' + AplDilucion +
            '&DilucionA=' + DilucionA +
            '&DilucionC=' + DilucionC +
            '&CPT_ProductoActual=' + CPT_ProductoActual +
            '&CPT_SituacionActual=' + CPT_SituacionActual +
            '&CPT_VentajasKey=' + CPT_VentajasKey +
            '&CPT_RecursoImagenProductoActual=' + CPT_RecursoImagenProductoActual +
            '&CPT_RecursoImagensolucionKey=' + CPT_RecursoImagensolucionKey +
            '&COP_CostoEnUso=' + COP_CostoEnUso,
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
        if (CALLBACK) {
            CALLBACK();
        }

    }).fail(function (jqXHR, textStatus, error) {
        if (jqXHR.status == 401) {
            alert('La sessión ha expirado.');
            $('#dvModalPropuestaTE_ver2').modal('hide');
            $('#dvDialogoInicioSesion').modal();
        } else {
            alertify.error('Ocurrió una error: funcion Update_OportunidadesProductos.');
        }
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\       
function crearRecursoArchivoUsandoFormData(Op, Archivo, idRep, status) {
    var file_data = $('#file').prop('files')[0];
    var form_data = new FormData();
    form_data.append('file', file_data);


    $.ajax({
        url: _ApplicationUrl + '/api/RepositorioCrearRecursoArchivo?IdDocento=99&IdTipoDoc=88',
        type: 'POST',
        cache: false,
        contentType: false, //'multipart/form-data',
        data: form_data,
        processData: false,
        statusCode: status,
        beforeSend: function (jqXHR, settings) {
            /*if(jqXHR.upload){
                jqXHR.upload.addEventListener('progress', $.proxy(_this._actualizarBarraProgresoTransferencia, _this), false);
            }*/
            //alert(url);
        },
    }).done(function (response, textStatus, jqXHR) {

        var Estatus = response.Estatus;
        var Hash = response.Hash;

        if (Estatus == 1) {
            var Contenedor = $('#modalCargaImagen_Contenedor').val();
            $(Contenedor).attr("src", _ApplicationUrl + '/imgupload/' + Hash);
            $('#modalCargaRecurso').modal('hide');
        } else {
            alertify.error('Error al cargar la imagen.');
        }

    }).fail(function (jqXHR, textStatus, errorThrown) {
        if (jqXHR.status == 401) {
            alert('La sessión ha expirado.');
            $('#modalCargaRecurso').modal('hide');
            $('#dvModalPropuestaTE_ver2').modal('hide');
            $('#dvDialogoInicioSesion').modal();
        } else {
            alertify.error('Ocurrió una error: funcion crearRecursoArchivoUsandoFormData.');
        }
    }).always(function (jqXHR, textStatus, errorThrown) {
        console.log(3);
    }).error(function (data, status) {
        alertify.error('Ocurrió una error: funcion crearRecursoArchivoUsandoFormData.');
    });

}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function aceptarPropuestaTecnoEconomica(idVal) {

    PatternflyToast.showProgress('Generando ACYS...');
    $.ajax({
        url: _ApplicationUrl + '/api/AceptarPropuestaTecnoEconomica/?idVal=' + idVal,
        type: 'GET',
        cache: false,
        statusCode: {
            401: function (jqXHR, textStatus, errorThrown) {
                $('#dvDialogoInicioSesion').modal();
                _onLoginSuccessful = $.proxy(aceptarPropuestaTecnoEconomica, null, idVal);
            }
        }
    }).done(function (response, textStatus, jqXHR) {

        $('#dvModalPropuestaTE_ver2').modal('hide');
        PatternflyToast.showSuccess('El ACYS con folio ' + response + ' de la propuesta ha sido generado satisfactoriamente', 8000);

        var hfId_Cte = $('#hfId_Cte').val();
        $('#Slider_' + hfId_Cte).empty();
        CrearTablaHija(hfId_Cte, 0, 1);



    }).fail(function (jqXHR, textStatus, errorThrown) {

        if (jqXHR.status == 401) {
            alert('La sessión ha expirado.');
            $('#dvModalPropuestaTE_ver2').modal('hide');
            $('#dvDialogoInicioSesion').modal();
        } else {
            if (jqXHR.status == 500) {
                console.log(jqXHR)
                console.log(jqXHR.responseText)
                var responseText = jQuery.parseJSON(jqXHR.responseText);
                console.log(responseText.ExceptionMessage);

                alert(responseText.ExceptionMessage);
            }
            else {
                alertify.error('Ocurrió una error: funcion aceptarPropuestaTecnoEconomica.');
            }
        }

    }).always(function (jqXHR, textStatus, errorThrown) {
        console.log(3);
    }).error(function (data, status) {
        alertify.error('Ocurrió una error: funcion aceptarPropuestaTecnoEconomica.');
    });

}

