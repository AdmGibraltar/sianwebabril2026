/*
*/

function AJAX_CL_Get_ComprasLocales_Index(
    FILTRO_Aplicar1, FILTRO_Aplicar2, FILTRO_Aplicar3, FILTRO_Aplicar4, FILTRO_Aplicar5,
    FILTRO_Aplicar6, FILTRO_Aplicar7, FILTRO_Aplicar8, FILTRO_Aplicar9, FILTRO_Aplicar10,
    FILTRO_Aplicar11, FILTRO_Aplicar12, FILTRO_Aplicar13,

    FILTRO_NoSolicitud,
    FILTRO_CodigoPadreProducto,
    FILTRO_CodigoLocal,
    FILTRO_EstadoAutorizacion,
    FILTRO_EstadoVigencia,
    FILTRO_EstadoVencimiento,
    FILTRO_Sucursal,
    FILTRO_RangoFechaDe,
    FILTRO_RangoFechaA,
    FILTRO_TipoProducto,
    FILTRO_FamiliaProductos,
    FILTRO_MotivoCompra,

    PageNo, PageSize, Id_Com, Id_Estatus,
    CALLBACK_Exito, CALLBACK_ERROR) {

    $.ajax({
        url: _ApplicationUrl + '/api/CL_Main/Get_ComprasLocales_Index',
        data: {
            FILTRO_Aplicar1: FILTRO_Aplicar1,
            FILTRO_Aplicar2: FILTRO_Aplicar2,
            FILTRO_Aplicar3: FILTRO_Aplicar3,
            FILTRO_Aplicar4: FILTRO_Aplicar4,
            FILTRO_Aplicar5: FILTRO_Aplicar5,
            FILTRO_Aplicar6: FILTRO_Aplicar6,
            FILTRO_Aplicar7: FILTRO_Aplicar7,
            FILTRO_Aplicar8: FILTRO_Aplicar8,
            FILTRO_Aplicar9: FILTRO_Aplicar9,
            FILTRO_Aplicar10: FILTRO_Aplicar10,
            FILTRO_Aplicar11: FILTRO_Aplicar11,
            FILTRO_Aplicar12: FILTRO_Aplicar12,
            FILTRO_Aplicar13: FILTRO_Aplicar13,

            FILTRO_NoSolicitud: FILTRO_NoSolicitud,
            FILTRO_CodigoPadreProducto: FILTRO_CodigoPadreProducto,
            FILTRO_CodigoLocal: FILTRO_CodigoLocal,
            FILTRO_EstadoAutorizacion: FILTRO_EstadoAutorizacion,
            FILTRO_EstadoVigencia: FILTRO_EstadoVigencia,
            FILTRO_EstadoVencimiento: FILTRO_EstadoVencimiento,

            FILTRO_Sucursal: FILTRO_Sucursal,
            FILTRO_RangoFechaDe: FILTRO_RangoFechaDe,
            FILTRO_RangoFechaA: FILTRO_RangoFechaA,
            FILTRO_TipoProducto: FILTRO_TipoProducto,
            FILTRO_FamiliaProductos: FILTRO_FamiliaProductos,
            FILTRO_MotivoCompra: FILTRO_MotivoCompra,

            PageNo: 0,
            PageSize: 0,
            Id_Com: Id_Com,
            Id_Estatus: Id_Estatus
        },
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
        var Estado = response.Estado;
        var Datos = response.Datos;

        if (Estado == -1) {
            alertify.error('Ocurrio un erro al ejecutar la consulta.');
            if (CALLBACK_ERROR) {
                CALLBACK_ERROR();
            }
            return;
        }
        if (Estado == 1) {
            if (CALLBACK_Exito) {
                CALLBACK_Exito(Datos);
            }
        }
    }).fail(function (jqXHR, textStatus, error) {
        if (CALLBACK_ERROR) {
            CALLBACK_ERROR();
        }
        //alertify.error(jqXHR.responseText);
        console.log('ERROR: ' + jqXHR.responseText);
        if (jqXHR.status == 401) {
            $('#dvDialogoInicioSesion').appendTo("body").modal();
        } else {
            alertify.error('Ocurrió una error:' + jqXHR.responseText);
        }
    });



}

//
function AJAX_CL_Autorizar(
    Id_Comp, Estatus, Vigencia, CALLBACK_EXITO, CALLBACK_ERROR) {

    $.ajax({
        url: _ApplicationUrl + '/api/CL_Main/CL_spProCompraLocalDet_Modificar',
        data: {
            Id_Comp: Id_Comp,
            Estatus: Estatus,
            Vigencia: Vigencia
        },
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
        var Estado = response.Estado;
        var Datos = response.Datos;

        if (Estado == 1) {
            if (CALLBACK_EXITO) {
                CALLBACK_EXITO(Datos);
            }
        } else {

        }
    }).fail(function (jqXHR, textStatus, error) {
        //alertify.error(jqXHR.responseText);
        if (CALLBACK_ERROR) {
            CALLBACK_ERROR(jqXHR.responseText);
        }
        console.log('ERROR: ' + jqXHR.responseText);
        if (jqXHR.status == 401) {
            $('#dvDialogoInicioSesion').appendTo("body").modal();
        } else {
            alertify.error('Ocurrió una error:' + jqXHR.responseText);
        }
    });
}