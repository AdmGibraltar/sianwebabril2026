/*
    7Mar2022 RFH
*/

var DatosConsulta;
var Parametro_Id_Cd;
var Parametro_SolFolio;


var CL_Ajax = {

    spCLCen_Det_Modificar: function (Id_Cd, Id_Comp, Estatus,
        Vigencia, Enfocada, FechaVigencia, CALLBACK_Exito, CALLBACK_Error) {
        $.ajax({
            //url: _ApplicationUrl + '/api/CL_Main/CL_spProCompraLocalDet_Modificar',
            url: _ApplicationUrl + '/api/CL_Main/spCLCen_Det_Modificar',
            data: {
                Id_Cd: Id_Cd,
                Id_Comp: Id_Comp,
                Estatus: Estatus,
                Vigencia: Vigencia,
                Enfocada: Enfocada,
                Autorizo: 0,
                FechaVigencia: FechaVigencia
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
            //alertify.error(jqXHR.responseText);
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

var OpcionesFiltro = {

    Inicializar: function (CALLBACK_Exito) {
        let d = new Date();
        let month = d.getMonth() + 1;
        let day = d.getDate();
        let year = d.getFullYear();
        if (month == 12) {
            year = year + 1;
        }
        let FechaHoy = (day < 10 ? '0' : '') + day + '/' + (month < 10 ? '0' : '') + month + '/' + year;

        //$('.datepicker').Zebra_DatePicker({
        //    format: 'd/m/Y'
        //});

        let Sucursal = 'Monterrey';

        $('#tbFechaInicial').val(FechaHoy);
        $('#tbFechaFinal').val(FechaHoy);
        $('#txtSucursal').val(Sucursal);

        if (CALLBACK_Exito) {
            CALLBACK_Exito();
        }
    }

    //AplicarSeleccion: function (CALLBACK_Exito) {
    //    for (let i = 1; i < 14; i++) {
    //        var CheckedOpcion = $('#chbOpcion_' + i).is(':checked');
    //        let EstadoDisplay = '';
    //        if (CheckedOpcion) {
    //            EstadoDisplay = 'block';
    //        } else {
    //            EstadoDisplay = 'none';
    //        }
    //        switch (i) {
    //            case 1:
    //                // Numero de solicitud
    //                $('#divFiltroNoSolicitud').css('display', EstadoDisplay);
    //                break;
    //            case 2:
    //                //Código padre del producto
    //                $('#divFiltroCodigoPadreProducto').css('display', EstadoDisplay);
    //                break;
    //            case 3:
    //                //Código local del producto
    //                $('#divFiltroCodigoProducto').css('display', EstadoDisplay);
    //                break;
    //            case 4:
    //                //4 Proveedor padre
    //                $('#divFiltroProveedorPadre').css('display', EstadoDisplay);
    //                break;
    //            case 5:
    //                //5 Proveedor local
    //                $('#divFiltroProveedorLocal').css('display', EstadoDisplay);
    //                break;
    //            case 6:
    //                //6 Estado de autorización
    //                $('#divFiltroEstado').css('display', EstadoDisplay);
    //                break;
    //            case 7:
    //                //7 Estado de vigencia
    //                $('#divFiltroVigencia').css('display', EstadoDisplay);
    //                break;
    //            case 8:
    //                //8 Estado de vencimiento
    //                $('#divFiltroVencimiento').css('display', EstadoDisplay);
    //                break;
    //            case 9:
    //                //9 Sucursal
    //                $('#divSucursal').css('display', EstadoDisplay);
    //                break;
    //            case 10:
    //                // 10 Por rango de fecha 
    //                $('#divFiltroFechasRango').css('display', EstadoDisplay);
    //                break;
    //            case 11:
    //                //11 Tipo de producto
    //                $('#divFiltroTipoProducto').css('display', EstadoDisplay);
    //                break;
    //            case 12:
    //                //12 Familia de productos
    //                $('#divFiltroFamiliaProducto').css('display', EstadoDisplay);
    //                break;
    //            case 13:
    //                //13 Motivo de compra
    //                $('#divFiltroMotivoCompra').css('display', EstadoDisplay);
    //                break;
    //        }
    //    }

    //    if (CALLBACK_Exito) {
    //        CALLBACK_Exito();
    //    }
    //}
}

var Indice = {
    // Boton Click en Columna
    btnColumna_Click: function (Obj) {
        let Col = $(Obj).data("col_id"); // Columna
        let Dir = $(Obj).attr("data-dir"); // Direccion de Ordenado
        Dir = parseInt(Dir);
        if (isNaN(Dir)) {
            Dir = 0;
        }
        $('#spinner_Index').css('display', 'block');
        $('#tbl_Listado > tbody').empty();
        console.log('Columna: ' + Col + ' dir: ' + Dir);
        if (typeof (DatosConsulta) == 'undefined') {
            alertify.alert('Debe relizar la consulta de integralidad.');
        } else {
            switch (Col) {
                case 1: //Id_Cd
                    switch (Dir) {
                        case 0:
                            DatosConsulta = DatosConsulta.sort(function (a, b) {
                                if (a.Id_Cd > b.Id_Cd) {
                                    return 1;
                                }
                                if (a.Id_Cd < b.Id_Cd) {
                                    return -1;
                                }
                                return 0;
                            });
                            $('#ColumnaOrden_' + Col).attr('data-dir', 1);
                            break;
                        case 1:
                            DatosConsulta = DatosConsulta.sort(function (a, b) {
                                if (a.Id_Cd < b.Id_Cd) {
                                    return 1;
                                }
                                if (a.Id_Cd > b.Id_Cd) {
                                    return -1;
                                }
                                return 0;
                            });
                            $('#ColumnaOrden_' + Col).attr('data-dir', 0);
                            break;
                    }
                    break;
                case 2:
                    break;
                case 3:
                    //  IdTipoSolicitud
                    switch (Dir) {
                        case 0:
                            DatosConsulta = DatosConsulta.sort(function (a, b) {
                                if (a.IdTipoSolicitud > b.IdTipoSolicitud) {
                                    return 1;
                                }
                                if (a.IdTipoSolicitud < b.IdTipoSolicitud) {
                                    return -1;
                                }
                                return 0;
                            });
                            $('#ColumnaOrden_' + Col).attr('data-dir', 1);
                            break;
                        case 1:
                            DatosConsulta = DatosConsulta.sort(function (a, b) {
                                if (a.IdTipoSolicitud < b.IdTipoSolicitud) {
                                    return 1;
                                }
                                if (a.IdTipoSolicitud > b.IdTipoSolicitud) {
                                    return -1;
                                }
                                return 0;
                            });
                            $('#ColumnaOrden_' + Col).attr('data-dir', 0);
                            break;
                    }
                    break;
                case 4:
                    // No Solicitud
                    switch (Dir) {
                        case 0:
                            DatosConsulta = DatosConsulta.sort(function (a, b) {
                                if (a.Id_Comp > b.Id_Comp) {
                                    return 1;
                                }
                                if (a.Id_Comp < b.Id_Comp) {
                                    return -1;
                                }
                                return 0;
                            });
                            $('#ColumnaOrden_' + Col).attr('data-dir', 1);
                            break;
                        case 1:
                            DatosConsulta = DatosConsulta.sort(function (a, b) {
                                if (a.Id_Comp < b.Id_Comp) {
                                    return 1;
                                }
                                if (a.Id_Comp > b.Id_Comp) {
                                    return -1;
                                }
                                return 0;
                            });
                            $('#ColumnaOrden_' + Col).attr('data-dir', 0);
                            break;
                    }
                    break;
                case 5:
                    // Fecha
                    switch (Dir) {
                        case 0:
                            DatosConsulta = DatosConsulta.sort(function (a, b) {
                                if (a.Comp_FechaSol > b.Comp_FechaSol) {
                                    return 1;
                                }
                                if (a.Comp_FechaSol < b.Comp_FechaSol) {
                                    return -1;
                                }
                                return 0;
                            });
                            $('#ColumnaOrden_' + Col).attr('data-dir', 1);
                            break;
                        case 1:
                            DatosConsulta = DatosConsulta.sort(function (a, b) {
                                if (a.Comp_FechaSol < b.Comp_FechaSol) {
                                    return 1;
                                }
                                if (a.Comp_FechaSol > b.Comp_FechaSol) {
                                    return -1;
                                }
                                return 0;
                            });
                            $('#ColumnaOrden_' + Col).attr('data-dir', 0);
                            break;
                    }
                    break;
                case 6:
                    // U_Nombre
                    switch (Dir) {
                        case 0:
                            DatosConsulta = DatosConsulta.sort(function (a, b) {
                                if (a.U_Nombre > b.U_Nombre) {
                                    return 1;
                                }
                                if (a.U_Nombre < b.U_Nombre) {
                                    return -1;
                                }
                                return 0;
                            });
                            $('#ColumnaOrden_' + Col).attr('data-dir', 1);
                            break;
                        case 1:
                            DatosConsulta = DatosConsulta.sort(function (a, b) {
                                if (a.U_Nombre < b.U_Nombre) {
                                    return 1;
                                }
                                if (a.U_Nombre > b.U_Nombre) {
                                    return -1;
                                }
                                return 0;
                            });
                            $('#ColumnaOrden_' + Col).attr('data-dir', 0);
                            break;
                    }
                    break;
                case 8:  // Id_Prd
                    switch (Dir) {
                        case 0:
                            DatosConsulta = DatosConsulta.sort(function (a, b) {
                                if (a.Id_Prd > b.Id_Prd) {
                                    return 1;
                                }
                                if (a.Id_Prd < b.Id_Prd) {
                                    return -1;
                                }
                                return 0;
                            });
                            $('#ColumnaOrden_' + Col).attr('data-dir', 1);
                            break;
                        case 1:
                            DatosConsulta = DatosConsulta.sort(function (a, b) {
                                if (a.Id_Prd < b.Id_Prd) {
                                    return 1;
                                }
                                if (a.Id_Prd > b.Id_Prd) {
                                    return -1;
                                }
                                return 0;
                            });
                            $('#ColumnaOrden_' + Col).attr('data-dir', 0);
                            break;
                    }
                    break;
                case 9:
                    //  DescripcionProducto
                    switch (Dir) {
                        case 0:
                            DatosConsulta = DatosConsulta.sort(function (a, b) {
                                if (a.DescripcionProducto > b.DescripcionProducto) {
                                    return 1;
                                }
                                if (a.DescripcionProducto < b.DescripcionProducto) {
                                    return -1;
                                }
                                return 0;
                            });
                            $('#ColumnaOrden_' + Col).attr('data-dir', 1);
                            break;
                        case 1:
                            DatosConsulta = DatosConsulta.sort(function (a, b) {
                                if (a.DescripcionProducto < b.DescripcionProducto) {
                                    return 1;
                                }
                                if (a.DescripcionProducto > b.DescripcionProducto) {
                                    return -1;
                                }
                                return 0;
                            });
                            $('#ColumnaOrden_' + Col).attr('data-dir', 0);
                            break;
                    }
                    break;
                case 10:
                    //  Fecha Vigencia
                    switch (Dir) {
                        case 0:
                            DatosConsulta = DatosConsulta.sort(function (a, b) {
                                if (a.VigenciaFecha > b.VigenciaFecha) {
                                    return 1;
                                }
                                if (a.VigenciaFecha < b.VigenciaFecha) {
                                    return -1;
                                }
                                return 0;
                            });
                            $('#ColumnaOrden_' + Col).attr('data-dir', 1);
                            break;
                        case 1:
                            DatosConsulta = DatosConsulta.sort(function (a, b) {
                                if (a.VigenciaFecha < b.VigenciaFecha) {
                                    return 1;
                                }
                                if (a.VigenciaFecha > b.VigenciaFecha) {
                                    return -1;
                                }
                                return 0;
                            });
                            $('#ColumnaOrden_' + Col).attr('data-dir', 0);
                            break;
                    }
                    break;
                case 11:
                    //  Comentarios
                    switch (Dir) {
                        case 0:
                            DatosConsulta = DatosConsulta.sort(function (a, b) {
                                if (a.Comentarios > b.Comentarios) {
                                    return 1;
                                }
                                if (a.Comentarios < b.Comentarios) {
                                    return -1;
                                }
                                return 0;
                            });
                            $('#ColumnaOrden_' + Col).attr('data-dir', 1);
                            break;
                        case 1:
                            DatosConsulta = DatosConsulta.sort(function (a, b) {
                                if (a.Comentarios < b.Comentarios) {
                                    return 1;
                                }
                                if (a.Comentarios > b.Comentarios) {
                                    return -1;
                                }
                                return 0;
                            });
                            $('#ColumnaOrden_' + Col).attr('data-dir', 0);
                            break;
                    }
                    break;
                case 12:
                    //  Estatus 
                    switch (Dir) {
                        case 0:
                            DatosConsulta = DatosConsulta.sort(function (a, b) {
                                if (a.Det_Estatus > b.Det_Estatus) {
                                    return 1;
                                }
                                if (a.Det_Estatus < b.Det_Estatus) {
                                    return -1;
                                }
                                return 0;
                            });
                            $('#ColumnaOrden_' + Col).attr('data-dir', 1);
                            break;
                        case 1:
                            DatosConsulta = DatosConsulta.sort(function (a, b) {
                                if (a.Det_Estatus < b.Det_Estatus) {
                                    return 1;
                                }
                                if (a.Det_Estatus > b.Det_Estatus) {
                                    return -1;
                                }
                                return 0;
                            });
                            $('#ColumnaOrden_' + Col).attr('data-dir', 0);
                            break;
                    }
                    break;

            }  // witch

            Indice.DesplegarDatos(DatosConsulta, function () {
                setTimeout(function () {
                    $('#spinner_Index').css('display', 'none');
                }, 500);
            });
        }
    },

    Exportar_Excel: function (Data) {
        var excel = $JExcel.new();
        var excel = $JExcel.new("Arial 9 #333333");
        var excel = $JExcel.new("Arial 9 #333333");
        var P = 'Reporte Compras Locales';

        excel.set({ sheet: 0, value: P });

        var formatCell = excel.addStyle({
            border: "thin #333333,thin #333333,thin #333333,thin #333333"
        });
        var formatCell_NB = excel.addStyle({
            border: "none,none,none,none"
        });
        var formatCell_NB_Money = excel.addStyle({
            format: '$#,##0',
            border: "none,none,none,none"
        });
        var formatCell_C = excel.addStyle({
            align: "C",
            border: "thin #333333,thin #333333,thin #333333,thin #333333"
        });
        var formatCell_C_NB = excel.addStyle({
            align: "C",
            border: "none,none,none,none"
        });
        var formatCell_L = excel.addStyle({
            align: "L",
            border: "thin #333333,thin #333333,thin #333333,thin #333333"
        });
        var formatCell_Prd = excel.addStyle({
            align: "L",
            border: "thin #333333,thin #333333,thin #333333,thin #333333",
            format: '#################',
        });
        var formatCell_L_NB = excel.addStyle({
            align: "L",
            border: "none,none,none,none"
        });
        var format_Monto = excel.addStyle({
            //format: '#,##0.00',
            align: "C",
            format: '$#,##0.00',
            border: "thin #333333,thin #333333,thin #333333,thin #333333"
        });
        // renglon amarillo
        var formatCell_Amarillo = excel.addStyle({
            align: "C",
            fill: "#FFFF00",
            border: "thin #333333,thin #333333,thin #333333,thin #333333",
            font: "Arial 9 #fff B",
            format: '$#,##0.##'
        });
        var formatCell_Amarillo_L = excel.addStyle({
            align: "L",
            fill: "#FFFF00",
            border: "thin #333333,thin #333333,thin #333333,thin #333333",
            font: "Arial 9 #fff B",
            format: '$#,##0.##'
        });
        var formatCell_Amarillo_C = excel.addStyle({
            align: "C",
            fill: "#FFFF00",
            border: "thin #333333,thin #333333,thin #333333,thin #333333",
            font: "Arial 9 #fff B",
            format: '$#,##0.00'
        });
        var formatHeader = excel.addStyle({
            align: "C",
            fill: "#dadada",
            border: "thin #333333,thin #333333,thin #333333,thin #333333",
            font: "Arial 9 #fff B"
        });
        var formatHeader_NB = excel.addStyle({
            align: "C",
            fill: "#dadada",
            border: "none,none,none,none",
            font: "Arial 9 #fff B"
        });
        var formatHeader_R_NB = excel.addStyle({
            align: "R",
            border: "none,none,none,none",
            font: "Arial 9 #fff B"
        });
        var formatTitulo = excel.addStyle({
            border: "none,none,none,none", font: "Arial 9 #0000AA B"
        });
        var dStyle = excel.addStyle({
            align: "L",
            format: "d-mmm-yy",
            border: "none,none,none,none",
            font: "Arial 9 #0000AA B"
        });

        var evenRow = excel.addStyle({ border: "none,none,none,thin #333333" });
        var oddRow = excel.addStyle({ fill: "#ECECEC", border: "none,none,none,thin #333333" });

        var line = 0;
        var Fecha = new Date();
        Fecha = Fecha.format("dd/M/yyyy");

        var NombreUsuario = '';

        var TipoReporte = '';
        var FechaInicial = $('#tbFechaInicial').val();
        var FechaFinal = $('#tbFechaInicial').val();

        line = line + 1;
        //excel.set(0, 0, line, "Fechas del " + FechaInicial + " al " + FechaFinal, formatTitulo);

        var initDate = new Date(2000, 0, 1);
        var endDate = new Date(2016, 0, 1);

        line = line + 1;
        var headers = [
            "No Solicitud",
            "#Sucursal",
            "Sucursal",
            "Solicitante",
            "Estatus",
            "# MotivoCompra Local",
            "Motivo Compra Local",
            "Fecha",
            "Código de Producto Padre",
            "Código de Producto CL",
            "Descripción de Producto",
            "Vencimiento CL",

            "#Motivo",
            "Motivo",
            "#Proveedor Central",
            "Nombre Proveedor Central",
            "#Proveedor Local",
            "Nombre Proveedor Local",

            "Id Tipo Producto",
            "Tipo de Producto",
            "Aplicación",
            "Subfamilia",

            "Presentación",
            "Unidad",
            "Costo",

            "PAAA Código CL",
            "PAAA Código key",

            "Precio Lista",
            "Autorizador Id",
            "Autorizador Nombre"

        ];

        for (var i = 0; i < headers.length; i++) {
            excel.set(0, i, line, headers[i], formatHeader);
            excel.set(0, i, undefined, "auto"); // Se agrega ancho de columnas en auto

        }

        line = line + 1;

        var Totales_Linea = 0;
        var Ultimo_Rik = 0;
        var Inicio = line + 1; // Salta Renglon
        var IdMatriz = 0;
        var SUMA_1 = 0;
        var IMPRIMIR_SUMA_1 = 0;
        var Inicia_IMP_SUMA_1 = 0;
        let Col = 0;
        for (var i = 0; i < Data.length; i++) {
            // Imprime registro NORMAL
            Col = 0;
            excel.set(0, Col, line, Data[i].Id_Comp, formatCell_C);
            Col = Col + 1;
            excel.set(0, Col, line, Data[i].Id_Cd, formatCell_C);
            Col = Col + 1;
            excel.set(0, Col, line, Data[i].Cd_Nombre, formatCell_L);
            Col = Col + 1;
            excel.set(0, Col, line, Data[i].U_Nombre, formatCell_L);
            Col = Col + 1;
            //excel.set(0, Col, line, Data[i].Det_Estatus, formatCell_C);
            if (Data[i].Det_Estatus == '0') {
                excel.set(0, Col, line, 'Pendiente', formatCell_L);
            } else if (Data[i].Det_Estatus == '1') {
                excel.set(0, Col, line, 'Autorizado', formatCell_L);
            } else if (Data[i].Det_Estatus == '2') {
                excel.set(0, Col, line, 'Rechazado', formatCell_L);
            }

            Col = Col + 1;
            excel.set(0, Col, line, Data[i].IdTipoSolicitud, formatCell_C);
            Col = Col + 1;
            excel.set(0, Col, line, Data[i].TipoSolicitudNombre, formatCell_L);
            Col = Col + 1;
            let Comp_FechaSol = Data[i].Comp_FechaSol.substring(0, 10);
            if (Comp_FechaSol == '01/01/1900' || Comp_FechaSol == '0001-01-01') {
                Comp_FechaSol = '';
            } else {
                excel.set(0, Col, line, Comp_FechaSol, formatCell_L);
            }
            Col = Col + 1;
            // Prd
            if (Data[i].Id_PrdOriginal == null) {
                excel.set(0, Col, line, 'No Aplica', formatCell_Prd);
            } else {
                excel.set(0, Col, line, Data[i].Id_PrdOriginal, formatCell_Prd);
            }
            Col = Col + 1;
            excel.set(0, Col, line, Data[i].Id_Prd, formatCell_Prd);
            Col = Col + 1;
            excel.set(0, Col, line, Data[i].ProductoNombre, formatCell_L);
            Col = Col + 1;
            let Vigencia = Data[i].Vigencia.substring(0, 10);
            if (Vigencia == '01/01/1900' || Vigencia == '0001-01-01') {
                Vigencia = '';
            } else {
                excel.set(0, Col, line, Vigencia, formatCell_L);
            }
            Col = Col + 1;
            excel.set(0, Col, line, Data[i].IdCausaDesabasto, formatCell_C);
            Col = Col + 1;
            excel.set(0, Col, line, Data[i].CausaDesabasto, formatCell_L);
            Col = Col + 1;
            excel.set(0, Col, line, Data[i].ProveedorPadreClave, formatCell_L);
            Col = Col + 1;
            excel.set(0, Col, line, Data[i].ProveedorPadreNombre, formatCell_L);
            Col = Col + 1;
            excel.set(0, Col, line, Data[i].IdProveedor, formatCell_C);
            Col = Col + 1;
            excel.set(0, Col, line, Data[i].ProveedorNombre, formatCell_L);
            Col = Col + 1;
            excel.set(0, Col, line, Data[i].IdTipoProducto, formatCell_C);
            Col = Col + 1;
            excel.set(0, Col, line, Data[i].TipoProducto, formatCell_L);
            Col = Col + 1;
            excel.set(0, Col, line, Data[i].ProductoAplicacion, formatCell_L);
            Col = Col + 1;
            excel.set(0, Col, line, Data[i].ProductoFamilia, formatCell_L);
            Col = Col + 1;
            excel.set(0, Col, line, Data[i].Prd_Presentacion, formatCell_C);
            Col = Col + 1;
            excel.set(0, Col, line, Data[i].Prd_UniEmp, formatCell_C);
            Col = Col + 1;
            excel.set(0, Col, line, Data[i].PrecioCosto, format_Monto);
            Col = Col + 1;
            excel.set(0, Col, line, Data[i].PrecioCostoCompralocal, format_Monto);
            Col = Col + 1;
            excel.set(0, Col, line, Data[i].PrecioAAA, format_Monto);
            Col = Col + 1;
            excel.set(0, Col, line, Data[i].PrecioPublico, format_Monto);
            Col = Col + 1;
            excel.set(0, Col, line, Data[i].AutorizadorId, formatCell_C);
            Col = Col + 1;
            excel.set(0, Col, line, Data[i].AutorizadorNombre, formatCell_L);
            line = line + 1;
        }
        line = line + 1;

        excel.set(0, 0, undefined, 15);
        excel.set(0, 1, undefined, 15);
        excel.set(0, 2, undefined, 25);
        excel.set(0, 3, undefined, 30);
        excel.set(0, 4, undefined, 15);
        excel.set(0, 5, undefined, 20);
        excel.set(0, 6, undefined, 40);
        excel.set(0, 7, undefined, 15);
        excel.set(0, 8, undefined, 25);
        excel.set(0, 9, undefined, 25);
        excel.set(0, 10, undefined, 80);
        excel.set(0, 11, undefined, 20);
        excel.set(0, 12, undefined, 15);
        excel.set(0, 13, undefined, 50);
        excel.set(0, 14, undefined, 20);
        excel.set(0, 15, undefined, 60);
        excel.set(0, 16, undefined, 15);
        excel.set(0, 17, undefined, 60);
        excel.set(0, 18, undefined, 15);
        excel.set(0, 19, undefined, 20);
        excel.set(0, 20, undefined, 60);
        excel.set(0, 21, undefined, 60);
        excel.set(0, 22, undefined, 20);
        excel.set(0, 23, undefined, 15);
        excel.set(0, 24, undefined, 20);
        excel.set(0, 25, undefined, 20);
        excel.set(0, 26, undefined, 20);
        excel.set(0, 27, undefined, 20);
        excel.set(0, 28, undefined, 15);
        excel.set(0, 29, undefined, 35);

        excel.generate("ReporteComprasLocales_" + Fecha + ".xlsx");
    },

    // Click Boton Consultar
    btn_DescargarResultados: function () {
        if (DatosConsulta == null) {
            alertify.success('No hay resultados para descargar.');
        } else {
            Indice.Exportar_Excel(DatosConsulta);
        }
    },

    ConsultaGraficaAjax: function (
        Sucursales, FechaInicial, FechaFinal, CALLBACK_Exito, CALLBACK_Error) {
        $.ajax({
            url: _ApplicationUrl + '/api/CL_Main/Get_ReporteMensual_Index',
            data: {
                Sucursales: Sucursales,
                FechaInicial: FechaInicial,
                FechaFinal: FechaFinal
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
                if (CALLBACK_Error) {
                    CALLBACK_Error(Datos);
                }
                return;
            }
        }).fail(function (jqXHR, textStatus, error) {
            console.log('ERROR: ' + jqXHR.responseText);
            console.log('LA SESSION SE CERRO');
            if (jqXHR.status == 401) {
                $('#dvDialogoInicioSesion').modal('show');
            } else {
                if (CALLBACK_Error) {
                    CALLBACK_Error();
                }
            }
        });
    },

    // Click Boton Grafica
    btn_DescargarResultadosGrafica: function () {

        $('#CPH_btnBotonGraficas').click();
        return;

    },


    DesplegarDatos: function (Lst, CALLBACK_Exito) {
        DatosConsulta = Lst;
        if (Lst.length <= 0) {
            alertify.success('No se encontro información.');
        }
        console.log(Lst);

        $('#tbl_Listado > tbody').empty();

        for (i = 0; i < Lst.length; i++) {

            let Data =
                'data-id_cd="' + Lst[i].Id_Cd + '" ' +
                'data-row="' + i + '" ' +
                'data-id_comp="' + Lst[i].Id_Comp + '" ' +
                'data-Id_Prd="' + Lst[i].Id_Prd + '" ' +
                'data-id_compdet="' + Lst[i].Id_CompDet + '" ' +
                'data-tiposolicitud="' + Lst[i].TipoSolicitud + '" ' +
                'data-AutorizadorId="' + Lst[i].AutorizadorId + '" ' +
                'data-idtiposolicitud="' + Lst[i].IdTipoSolicitud + '" ';

           
            var btn_VerDetalle = '<button ' +
                'id="btnVerDetalle_' + Lst[i].Id_Comp + '"' +
                Data +
                'onclick="btn_VerDetalle(this)"' +
                'type="button"' +
                'class="btn btn-default btn-sm"' +
                'title="Ver detalle de Solicitud."' + '>' +
                '<i class="fa fa-pencil-square-o"></i>' +
                '</button>';

            var btn_Editar = '<button ' +
                'id="btnEditar_' + Lst[i].Id_Comp + '"' +
                Data +
                'onclick="btn_Editar(this)"' +
                'type="button"' +
                'class="btn btn-default btn-sm"' +
                'title="Editar Solicitud."' + '>' +
                '<i class="fa fa-pencil-square-o"></i>' +
                '</button>';

            var row = $('<tr class="" id="rowSolicitud_' + i + '"" title="Comentario: ' + Lst[i].Comentarios + '">');
            //No. Solicitud
            row.append($('<td class="text-center">').append(Lst[i].Id_Comp));
            // Sucursal
            row.append($('<td>').append(Lst[i].Cd_Nombre));
            // Motivo Compra Local
            row.append($('<td class="text-center">').append(Lst[i].IdTipoSolicitud));
            row.append($('<td>').append(Lst[i].TipoSolicitudNombre));
            //Fecha           
            Comp_FechaSol = Lst[i].Comp_FechaSol;
            row.append($('<td class="text-center">').append(Comp_FechaSol.substring(0,10)));
            //Producto
            row.append($('<td>').append(Lst[i].Id_Prd));
            //Descripcion
            row.append($('<td>').append(Lst[i].DescripcionProducto));
            //Vigencia           
            Vigencia = Lst[i].VigenciaFecha;
            row.append($('<td class="text-center">').append(Vigencia.substring(0, 10)));
            //Solicitante
            row.append($('<td>').append(Lst[i].U_Nombre));
            //Estatus

            if (Lst[i].Det_Estatus == '0') {
                row.append($('<td class="text-center">').append(
                    '<div style="margin-top:5px!important;">' +
                    '<span class="label label-danger">Solicitado</span>' +
                    '</div>'
                ));

            } else {
                if (Lst[i].Det_Estatus == '1') {
                    row.append($('<td id="tdAcciones_' + i + '" class="text-center">').append(
                        '<div style="margin-top:5px!important;">' +
                        '<span class="label label-success">Autorizado</span>' +
                        '</div>'
                    ));
                }
                if (Lst[i].Det_Estatus == '2') {
                    row.append($('<td class="text-center">').append(
                        '<div style="margin-top:5px!important;">' +
                        '<span class="label label-danger">Rechazado</span>' +
                        '</div>'
                    ));
                }
                if (Lst[i].Det_Estatus == '3') {
                    row.append($('<td class="text-center">').append(
                        '<div style="margin-top:5px!important;">' +
                        '<span class="label label-danger">Error</span>' +
                        '</div>'
                    ));
                }
            }

            row.append($('<td class="text-center">').append(Lst[i].AutorizadorId));

            //Ver Detalle
            row.append($('<td id="tdAcciones_' + i + '" class="text-center">').append(
                '<table id="tblAcciones_' + Lst[i].Id_Comp + '">' +
                '<tr>' +
                '<td align ="center">' + btn_VerDetalle + '</td>' +
                '</tr>' +
                '</table>'
            ));

            //Editar
            row.append($('<td id="tdAcciones_' + i + '" class="text-center">').append(
                '<table id="tblAcciones_' + Lst[i].Id_Comp + '">' +
                '<tr>' +
                '<td align ="center">' + btn_Editar + '</td>' +
                '</tr>' +
                '</table>'
            ));
            // Autoriza
            

            $('#tbl_Listado > tbody').append(row);

            //if (Lst[i].IdTipoSolicitud == 3) {
            //    $('#tbFechaVigencia_' + i).Zebra_DatePicker({
            //        format: 'd/m/Y'
            //    });
            //}
        }
        if (CALLBACK_Exito) {
            CALLBACK_Exito();
        }
    },

    ConsultaAjax: function (LstParams, CALLBACK_Exito, CALLBACK_Error) {
        $.ajax({
            url: _ApplicationUrl + '/api/CL_Main/Get_ComprasLocales_Index',
            data: {
                
                PageNo: LstParams.PageNo,
                PageSize: LstParams.PageSize,
                Vencido: LstParams.Vencido,
                Id_Prd: LstParams.Id_Prd,
                IdProveedorLocal: LstParams.IdProveedorLocal,
                Id_Motivo: LstParams.Id_Motivo,
                Id_Comp: LstParams.Id_Comp,
                Id_Estatus : LstParams.Id_Estatus
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
                if (CALLBACK_Error) {
                    CALLBACK_Error(Datos);
                }
                return;
            }
        }).fail(function (jqXHR, textStatus, error) {
            console.log('ERROR: ' + jqXHR.responseText);
            console.log('LA SESSION SE CERRO');
            if (jqXHR.status == 401) {
                //$('#dvDialogoInicioSesion').appendTo("body").modal('show');
                $('#dvDialogoInicioSesion').modal('show');
                //$('#dvDialogoInicioSesion').show();                    
            } else {
                //alertify.error('Ocurrió una error:' + jqXHR.responseText);
                if (CALLBACK_Error) {
                    CALLBACK_Error();
                }
            }
        });
    },

    // Boton de Consulta 
    btn_ConsultaListado: function (CALLBACK_Exito, CALLBACK_Fin)
    {
        $('#spinner_Index').css('display', 'block');

        // Solicitud
        let Id_Comp = $('#txtSolicitud').val();
        if (Id_Comp == "") {
            Id_Comp = 0;
        }

        // Motivo de Compra 
        let Id_Motivo = $('#ddlMotivo').val();
        if (Id_Motivo == "") {
            Id_Motivo = 0;
        }

        // Codigo Producto
        var id_prd = $('#txtCodProducto').val();
        if (id_prd == "")
            id_prd = 0;
         var Id_PrdLocal = id_prd;

        //Folios Vencidos
        var IdVencidoLocal = $('#ddlVencido').val();
        var Id_PrdLocal = id_prd;
        var IdVencidoLocal = $('#ddlVencido').val();

        //Proveedor Local
        var Id_Prov = $('#ddlProveedorLocal').val();
        if (Id_Prov == "")
            Id_Prov = 0;
        var Id_ProvLocal = Id_Prov;

        //Estatus
        var Id_Estatus = $('#ddlEstatus').val();

 
        let LstParams = {
            'PageNo': 0,
            'PageSize': 0,
            'Vencido': IdVencidoLocal,
            'Id_Prd': Id_PrdLocal,
            'IdProveedorLocal': Id_ProvLocal,
            'Id_Comp': Id_Comp,
            'Id_Motivo': Id_Motivo,
            'Id_Estatus': Id_Estatus

        }

        $('#tbl_Listado > tbody').empty();

        Indice.ConsultaAjax(LstParams,
            function (Dat) { // CALLBACK_Exito
                Indice.DesplegarDatos(Dat, function () {
                    $('#spinner_Index').css('display', 'none');
                });
                if (CALLBACK_Exito) {
                    CALLBACK_Exito();
                }
            },
            function () { // CALLBACK_Error
                alertify.error('Ocurrio un error grave al ejecutar la consulta: btn_ConsultaListado();');
                $('#spinner_Index').css('display', 'none');
            }
        );
    }


}

var CLIndex = {
    Inicializar: function () {
        $('#spinner_Index').css('display', 'block');
        OpcionesFiltro.Inicializar(function () {
           // CLIndex.InicializaControles(function () {
                OpcionesFiltro.AplicarSeleccion(function () {
                    CLIndex.CargartblSucursales(function () {
                        CLIndex.CargarProveedores(function () {
                            Indice.btn_ConsultaListado(function () {
                                $('#spinner_Index').css('display', 'none');
                            });
                        });
                    });
                });
            //});
        });
    },

    //InicializaControles: function (CALLBACK_Exito) {
    //    $('.datepicker').Zebra_DatePicker({
    //        format: 'd/m/Y'
    //    });

    //    if (CALLBACK_Exito) {
    //        CALLBACK_Exito();
    //    }
    //},

    Sucursales_Ajax: function (Cd_Tipo, CALLBACK_Exito, CALLBACK_Error) {
        $.ajax({
            url: _ApplicationUrl + '/api/CL_Main/spCatCDI_ComboTodos_ver2',
            data: {
                Cd_Tipo: Cd_Tipo
            },
            cache: false,
            type: 'GET'
        }).done(function (response, textStatus, jqXHR) {
            let Estado = response.Estado;
            let Datos = response.Datos;
            if (Estado == 1) {
                if (CALLBACK_Exito) {
                    CALLBACK_Exito(Datos);
                }
            } else {
                if (CALLBACK_Error) {
                    CALLBACK_Error(Datos);
                }
                return;
            }
        }).fail(function (jqXHR, textStatus, error) {
            if (CALLBACK_Error) {
                CALLBACK_Error();
            }
            console.log('ERROR: ' + jqXHR.responseText);
            if (jqXHR.status == 401) {
                $('#dvDialogoInicioSesion').appendTo("body").modal();
            } else {
                alertify.error('Ocurrió una error:' + jqXHR.responseText);
            }
        });
    },

    CargarSucursales: function () {
        CLIndex.Sucursales_Ajax(1, function (Lst) {
            $('#ulSucursales').empty();
            $('#ulSucursales').append('<li>' +
                '<input type="checkbox" data-opcion="0" id="chbSucursalx_0" style="margin:5px;" onchange="OpcionesFiltro.AplicarSucursal(this);" />-- Todo --' +
                '</li>');
            for (let i = 1; i < Lst.length; i++) {
                $('#ulSucursales').append('<li>' +
                    '<input type="checkbox" data-opcion="' + i + '"  data-id_cd="' + Lst[i].Id_Cd + '"  id="chbSucursalx_' + i + '" style="margin:5px;" onchange="OpcionesFiltro.AplicarSucursal(this);" />' + Lst[i].Cd_Nombre +
                    '</li>');
            }
        }, function () {
            alertify.error('Error : CargarSucursales');
        });
    },

    CargartblSucursales: function (CALLBACK_Exito) {
        CLIndex.Sucursales_Ajax(1, function (Lst) {
            $('#tblSucursales > tbody').empty();
            //for (i = 0; i < Lst.length; i++) {
            //    var row = $('<tr class="" id="rowSolicitud_' + Lst[i].Id_Comp + '"">');
            //    row.append($('<td class="text-left">').append(
            //        '<input type="checkbox" data-opcion="' + i + '"  data-id_cd="' + Lst[i].Id_Cd + '"' +
            //        ' id="chbSucursal_' + i + '"' +
            //        //' value="' + Lst[i].Id_Cd + '"' +
            //        ' name="chbSucursal_' + i + '"' +
            //        ' style="margin:5px;" ' +
            //        //'onchange="OpcionesFiltro.AplicarSucursal(this);" ' +
            //        '/>' + Lst[i].Cd_Nombre
            //    ));
            //    $('#tblSucursales > tbody').append(row);
            //}

            if (CALLBACK_Exito) {
                CALLBACK_Exito();
            }
        }, function () {
            alertify.error('Error : CargarSucursales');
        });
    },

//Proveedores Locales
    Proveedores_Ajax: function (IdProveedorLocal, CallBack_Exito) {
        $.ajax({
            url: _ApplicationUrl + '/api/CL_Main/spProveedores_ComboCompraLocal',
            data: {
                IdProveedorLocal: IdProveedorLocal
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
                alertify.error('Error en CL_Ajax.Proveedores_Ajax()');
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

    CargarProveedores: function (CALLBACK_Exito) {
        CLIndex.Proveedores_Ajax(1, function (lst) {
            $('#ddlProveedorLocal').empty();
            //$('#ddlProveedorLocal').append('<option value="" >-- Seleccione -- </option>');
            for (i = 0; i < lst.length; i++) {
                $('#ddlProveedorLocal').append('<option value="' + lst[i].Id + '" >' + lst[i].Descripcion + '</option>');
        }
        if (CALLBACK_Exito) {
            CALLBACK_Exito();
        }
    });
}

}

function CLIndex_Inicializar() {
    //alert("XXX()");
    CLIndex.Inicializar();
}

function IniciaParametros(Id_Cd, SolFolio) {
    Parametro_Id_Cd = Id_Cd;
    Parametro_SolFolio = SolFolio;
}

function Ejecuta_BusqueDeParametros() {
    if (Parametro_Id_Cd > 0 && Parametro_SolFolio > 0) {
        $('#chbOpcion_1').prop('checked', true);
        $('#chbOpcion_10').prop('checked', false);

        //$('#chbOpcion_1').trigger("click");
        OpcionesFiltro.AplicarSeleccion(function () {
            console.log('...');
        });

        $('#tbNoSolicitud').val(Parametro_SolFolio);
        for (i = 0; i < 250; i++) {
            let Id_Cd = $('#chbSucursal_' + i).data('id_cd');
            if (Id_Cd == Parametro_Id_Cd) {
                $('#chbSucursal_' + i).prop('checked', true);
            } else {
                $('#chbSucursal_' + i).prop('checked', false);
            }
        }

        var Id_Prd = $('#txtCodProducto').val();


        Indice.btn_ConsultaListado();
    }
}

function SucursaleChechAll(Obj) {
    let Estado = $(Obj).attr('data-estado');
    Estado = parseInt(Estado);
    if (isNaN(Estado)) {
        Estado = 0;
    }
    for (var i = 0; i < 10; i++) {
        var chbControl = $('#chbSucursal_' + i);
        if (typeof (chbControl) != 'undefined') {
            if (Estado == 1) {
                chbControl.prop('checked', false);
            } else {
                chbControl.prop('checked', true);
            }
        }
    }
    if (Estado == 1) {
        $(Obj).attr('data-estado', 0);
    } else {
        $(Obj).attr('data-estado', 1);
    }
}


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
$(document).ready(function () {
    // Hack to enable multiple modals by making sure the .modal-open class
    // is set to the <body> when there is at least one modal open left
    $('body').on('hidden.bs.modal', function () {
        if ($('.modal.in').length > 0) {
            $('body').addClass('modal-open');
        }
    });

    $('#spinner_Index').css('display', 'block');

    //var tabEl = document.querySelector('button[data-bs-toggle="tab"]')
    //tabEl.addEventListener('shown.bs.tab', function (event) {
    //    event.target // newly activated tab
    //    event.relatedTarget // previous active tab
    //})

    OpcionesFiltro.Inicializar(function () {
        //CLIndex.InicializaControles(function () {
        //CLIndex.CargartblSucursales(function () {
        CLIndex.CargarProveedores(function () {
            Ejecuta_BusqueDeParametros();
            Indice.btn_ConsultaListado(function () {
                $('#spinner_Index').css('display', 'none');
            });
        });
    });

         

    $('#cmbFiltro_Estado').val(2);
});