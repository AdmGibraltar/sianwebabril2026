
// 27Abr2022

var ACyS_RenglonesXPagina = 12;
var GC_PaginaActual = 1;
var MaxPaginas = 5;
var ACyS_Paginacion_MaxPages = 10;
var ACyS_RegistroEncontrados = 0;
var GC_ContadorListaProductos = 0;
var GC_CN_ContadorListaProductos = 0;
var GC_ContenedorBtnAplicar = 0;
var GC_PageNumber = 1;
var GC_Orden = 11;
var GC_OrdenDir = 0;



var RepUtilidadPrima = {

    //Exportar_Excel: function (Obj, CALLBACK_Exito) {

    //    //alert(Desglosado);
    //    $('#spinner_GCIndice').css('display', 'block');
    //    var Anio = $('#tbAnio').val();
    //    var Mes = $('#cmbMes').find(':selected').data('mes');
    //    var tipoCliente = $('#cmbTipoCuenta').find(':selected').data('tcuenta');
    //    var Id_Rik = $('#ddlRepresentante').val();
    //    var Id_Ter = $('#ddlTerritorio').val();
    //    var Id_U = $('#ddlRepresentante').find(':selected').data('id_u');
    //    var TextoBuscar = $('#tbTextoBuscar').val();
    //    var IdCte = $('#tbNumeroCliente').val();
    //    let Rol = $('#CmbRol').find(':selected').data('rol');

    //    IdCte = parseInt(IdCte);
    //    if (isNaN(IdCte)) {
    //        IdCte = 0;
    //    }
    //    var NombreCliente = $('#tbNombreCliente').val();


    //    var PARMS = '/api/GC/spRepCumplimientoVI_ver3?' +
    //        '&Anio=' + Anio +
    //        '&Mes=' + Mes +
    //        '&Id_Uen=0' +
    //        '&Id_Seg=0' +
    //        '&Id_Rik=' + Id_Rik +
    //        '&Id_Ter=' + Id_Ter +
    //        '&Id_Cte=' + IdCte +
    //        '&NombreCliente=' + NombreCliente +
    //        '&Tipo=1' +  // 1 Listado Clientes / 2 Detalle Productos
    //        '&TextoBuscar=' + TextoBuscar +
    //        '&CampoOrden=' + GC_Orden +
    //        '&OrdenDir=' + GC_OrdenDir +
    //        '&TipoCliente=' + tipoCliente +
    //        '&Rol=' + Rol +
    //        '&Param12=0';



    //    $.ajax({
    //        url: _ApplicationUrl + PARMS,
    //        cache: false,
    //        type: 'GET',
    //        statusCode: {
    //            401: function (jqXHR, textStatus, errorThrown) {
    //                $('#dvDialogoInicioSesion').appendTo("body").modal();
    //                _onLoginSuccessful = $.proxy(Consulta_Producto, null, $, Id_Prd, rowno, Spinner, CALLBACK);
    //            }
    //        }
    //    }).done(function (response, textStatus, jqXHR) {
    //        Lst = response.Datos;
    //        Estado = response.Estado;
    //        if (Estado == 1) {
    //            if (CALLBACK_Exito) {
    //                CALLBACK_Exito();
    //            }
    //        }
    //        $('#spinner_GCIndice').css('display', 'none');
    //    }).fail(function (jqXHR, textStatus, error) {
    //        if (jqXHR.status == 401) {
    //            alert('La sessión ha expirado.');
    //            $('#dvDialogoInicioSesion').appendTo("body").modal('show');
    //        }
    //        $('#spinner_GCIndice').css('display', 'none');
    //        alertify.error(jqXHR.responseText);

    //    });
    //},

    formatDate: function (date) {
        var day = ("0" + date.getDate()).slice(-2);
        var month = ("0" + (date.getMonth() + 1)).slice(-2); // Los meses empiezan en 0
        var year = date.getFullYear();

        return day + "/" + month + "/" + year;
     },
    Bajar_Excel: function (Lst) {

        $('#spinner_GCIndice').css('display', 'block');
        $('#btnCargarListado').attr('disabled', true);

        //var Periodo = $('#ddPeriodo option:selected').text();
        var excel = $JExcel.new();
        var excel = $JExcel.new("Arial 9 #333333");
        var excel = $JExcel.new("Arial 9 #333333");
        var P = 1; //Periodo.replace(/-/g,'');
        excel.set({ sheet: 0, value: P });
        var evenRow = excel.addStyle({ border: "none,none,none,thin #333333" });
        var oddRow = excel.addStyle({ fill: "#ECECEC", border: "none,none,none,thin #333333" });

        var formatTitulo = excel.addStyle({
            border: "none,none,none,none", font: "Arial 9 #0000AA B"
        });

        var line = 0;

        var Mes = $('#cmbMes option:selected').text();
        var Anio = $('#tbAnio').val();

        //var rbTipo= $('#<%=rbTipo.ClientID %> option:selected').text();    
        //var rbTipo = $('#cphBodyContent_rbTipo option:selected').text();
        //var Zonas = $('#ddlZonas option:selected').text();
        var Zonas = 0;

        var dStyle = excel.addStyle({
            align: "L",
            format: "d-mmm-yy",
            border: "none,none,none,none",
            font: "Arial 9 #0000AA B"
        });

        var Fecha = new Date();
        //Fecha = Fecha.format("dd/mm/yyyy");
        var FechaFormateada = RepUtilidadPrima.formatDate(Fecha);
        //excel.set(0, 0, line, "Cumplimiento de Venta Instalada", formatTitulo);
        excel.set(0, 0, line + 1, "Reporte  Utilidad Prima Documento Producto", formatTitulo);

        excel.set(0, 0, line + 2, "Periodo: " + Anio + " " + Mes + " ", formatTitulo);
        //excel.set(0, 0, line + 3, "Representante: " + Representante, formatTitulo);
        excel.set(0, 0, line + 3, "Fecha: " + FechaFormateada, formatTitulo);
        //excel.set(0, 0, line + 1, "", formatTitulo);                
        //excel.set(0,0,line+3,"Periodo: "+Periodo, formatTitulo);             
        //var CDI_Nombre = 'NOMBRE CDI';
        //excel.set(0, 0, line + 4, "CDS : " + CDI_Nombre, formatTitulo);

        line = 6;

        var formatHeader = excel.addStyle({
            align: "C",
            fill: "#dadada",
            border: "thin #333333,thin #333333,thin #333333,thin #333333",
            font: "Arial 9 #fff B"
        });

        var formatCell = excel.addStyle({
            border: "thin #333333,thin #333333,thin #333333,thin #333333"
        });
        var formatCell_R = excel.addStyle({
            align: "R",
            border: "thin #333333,thin #333333,thin #333333,thin #333333"
        });

        var formatCell_C = excel.addStyle({
            align: "C",
            border: "thin #333333,thin #333333,thin #333333,thin #333333"
        });
        var formatCell_L = excel.addStyle({
            align: "L",
            border: "thin #333333,thin #333333,thin #333333,thin #333333"
        });
        var format_Monto = excel.addStyle({
            //format: '#,##0.00',
            align: "C",
            format: '$#,##0',
            border: "thin #333333,thin #333333,thin #333333,thin #333333"
        });
        // renglon amarillo
        var formatCell_Amarillo = excel.addStyle({
            align: "C",
            fill: "#FFFF00",
            border: "thin #333333,thin #333333,thin #333333,thin #333333",
            font: "Arial 9 #fff B",
            format: '$#,##0'
        });
        var formatCell_Amarillo_L = excel.addStyle({
            align: "L",
            fill: "#FFFF00",
            border: "thin #333333,thin #333333,thin #333333,thin #333333",
            font: "Arial 9 #fff B",
            format: '$#,##0'
        });
        var formatCell_Amarillo_C = excel.addStyle({
            align: "C",
            fill: "#FFFF00",
            border: "thin #333333,thin #333333,thin #333333,thin #333333",
            font: "Arial 9 #fff B",
            format: '$#,##0'
        });
        var formatCell_Amarillo_X = excel.addStyle({
            align: "C",
            fill: "#FFFF00",
            border: "thin #333333,thin #333333,thin #333333,thin #333333",
            font: "Arial 9 #fff B",
            format: '$#,##0'
        });

        var headers = ["Tipo de documento","# Producto","Producto", "Factura", "Venta", "Costo", "Utilidad Bruta", "% UB Real", "% UB Planeada", "Varianza U Bruta Puntos","Impacto Pesos"];

        for (var i = 0; i < headers.length; i++) {
            excel.set(0, i, 6, headers[i], formatHeader);
            excel.set(0, i, undefined, "auto");
        }

        var initDate = new Date(2000, 0, 1);
        var endDate = new Date(2016, 0, 1);

        line = 7;

        var Totales_Linea = 0;
        var Ultimo_Rik = 0;

        var Total1 = 0;
        var Total2 = 0;
        var Total3 = 0;
        var Total4 = 0;
        var Total5 = 0;
        var Total6 = 0;
        var Total7 = 0;
        var Total8 = 0;
        var Total9 = 0;

        var Inicio = line + 1; // Salta Renglon  

        for (var i = 0; i < Lst?.length; i++) {
            // Imprime registro Normal

            excel.set(0, 0, line, Lst[i].Tipo_Documento, formatCell_C);
            
            excel.set(0, 1, line, Lst[i].Id_Prd, formatCell_C); // PRODUCTO
            excel.set(0, 2, line, Lst[i].Prd_Nombre, formatCell_C); // PRODUCTO
            excel.set(0, 3, line, Lst[i].Id_Factura, formatCell_C); // PRODUCTO

            var Venta = 0; // 
            Venta = parseFloat(Lst[i].Venta);
            Venta = Venta.formatMoney(2, '.', ',');
            excel.set(0, 4, line, Venta, formatCell_R);

            var Costo = 0; // Venta del Mes
            Costo = parseFloat(Lst[i].Costo);
            Costo = Costo.formatMoney(2, '.', ',');
            excel.set(0, 5, line, Costo, formatCell_R);

            var UtilidadBruta = 0; // Vta Instalda (Acsys)
            UtilidadBruta = parseFloat(Lst[i].UtilidadBruta);
            UtilidadBruta = UtilidadBruta.formatMoney(2, '.', ',');
            excel.set(0, 6, line, UtilidadBruta, formatCell_R);

            var PorcUBReal = 0; // Vta del mes vs VI
            PorcUBReal = parseFloat(Lst[i].PorcUBReal);
            PorcUBReal = PorcUBReal.toFixed(2); ;
            excel.set(0, 7, line, PorcUBReal + '%', formatCell_R);

            var PorcUBPlaneada = 0; // % Cumplimineto VI en el mes
            PorcUBPlaneada = parseFloat(Lst[i].PorcUBPlaneada);
            PorcUBPlaneada = PorcUBPlaneada.toFixed(2);;
            excel.set(0, 8, line, PorcUBPlaneada + '%', formatCell_C);

            var VarianzaUBrutaPuntos = 0; // Venta Promedio Trimestre Anterior
            VarianzaUBrutaPuntos = parseFloat(Lst[i].VarianzaUBrutaPuntos);
           
            VarianzaUBrutaPuntos = VarianzaUBrutaPuntos.formatMoney(0, '.', ',');
            excel.set(0, 9, line, VarianzaUBrutaPuntos, formatCell_R);

            var ImpactoPesos = 0; // Prom. Trim. Ant Vs Mes
            ImpactoPesos = parseFloat(Lst[i].ImpactoPesos);
            ImpactoPesos = ImpactoPesos.formatMoney(2, '.', ',');
            excel.set(0, 10, line, ImpactoPesos, formatCell_R);

            line = line + 1;
        }

        //Periodo=Periodo.replace(/-/g,'');
        excel.generate("Reporte  Utilidad Prima _" + FechaFormateada + ".xlsx");

        $('#spinner_GCIndice').css('display', 'none');
        $('#btnCargarListado').attr('disabled', false);

    },

}

var Reporte = {

    btnExportaExcel_Click: function (Obj) {

        RepUtilidadPrima.Bajar_Excel(Lst)
     
    },
    PreCargaIndice: function (Obj) {
      /*  let Anio = $('#tbAnio').val();
        let Mes = $('#cmbMes').find(':selected').data('mes');
        var Orden = $(Obj).data('orden');
        var col_id = $(Obj).data('col_id');
        //var Dir = $(Obj).data('dir');
        var Dir = $(Obj).attr('data-dir');

        Dir = parseInt(Dir);
        if (isNaN(Dir)) {
            Dir = 0;
        }
        if (Dir == 0) {
            Dir = 1;
        } else {
            Dir = 0;
        }
        $('#ColumnaOrden_' + col_id).attr("data-dir", Dir);
        GC_Orden = Orden;
        GC_OrdenDir = Dir;
        Reporte.Cargar_Indice(Anio, Mes, GC_Orden);*/
    },

    // INDICE 
    Cargar_Indice: function (Anio, Mes, Orden) {
        $('#spinner_GCIndice').css('display', 'block');
        $('#btnCargarListado').attr('disabled', true);
        //var Anio = $('#tbAnio').val();
        //var Mes = $('#cmbMes').find(':selected').data('mes');
      


        var PARMS = '/api/UtilidadPrima/sp_ReportUtilidadPrimaDocumentoDetalle?' +
            //'PageNumber=' + GC_PageNumber + '&PageSize=' + ACyS_RenglonesXPagina +
            '&Anio=' + Anio +
            '&Mes=' + Mes;

        console.log(PARMS);

        $.ajax({
            url: _ApplicationUrl + PARMS,
            cache: false,
            type: 'GET',
            statusCode: {
                401: function (jqXHR, textStatus, errorThrown) {
                    $('#dvDialogoInicioSesion').appendTo("body").modal();
                    _onLoginSuccessful = $.proxy(Consulta_Producto, null, $, Id_Prd, rowno, Spinner, CALLBACK);
                }
            }
        }).done(function (response, textStatus, jqXHR) {
            Lst = response.Datos;
            console.log(Lst);
            $('#tblReporteDinamico > tbody').empty();

            //ACyS_RegistroEncontrados = Lst[0].RegistrosEcontrados;
            ACyS_RegistroEncontrados = 0;

            var Total1 = 0;
            var Total2 = 0;
            var Total3 = 0;
            var Total4 = 0;
            var Total5 = 0;
            var Total6 = 0;
            var Total7 = 0;
            var Total8 = 0;
            var Total9 = 0;

            var CatRows = Lst?.length;
         
            for (i = 0; i < Lst?.length; i++) {
                var row = $('<tr id="TitleRow_' + i + '">');
               
                row.append($('<td class="text-center">').append(
                    '' + Lst[i].Tipo_Documento + ''
                ));
                row.append($('<td class="text-center">').append(
                    '' + Lst[i].Id_Prd + ''
                ));
                row.append($('<td class="text-center">').append(
                    '' + Lst[i].Prd_Nombre + ''
                ));

                row.append($('<td class="text-left">').append(
                    '' + Lst[i].Id_Factura + ''
                ));

                var Venta = 0;
                Venta = parseFloat(Lst[i].Venta);
                Venta = Venta.formatMoney(2, '.', ',');

                row.append($('<td class="text-center" style="width:100px;">').append('$' + Venta));
                // Col 3, Venta Total del Mes
                var Costo = 0;
                Costo = parseFloat(Lst[i].Costo);
                Costo = Costo.formatMoney(2, '.', ',');
                
                row.append($('<td class="text-center" style="width:100px;">').append('$' + Costo));

                var UtilidadBruta = 0;
                UtilidadBruta = parseFloat(Lst[i].UtilidadBruta);
                UtilidadBruta = UtilidadBruta.formatMoney(2, '.', ',');
               
                row.append($('<td class="text-center" style="width:100px;">').append('$' + UtilidadBruta));

                var PorcUBReal = 0;
                PorcUBReal = parseFloat(Lst[i].PorcUBReal);
                PorcUBReal = PorcUBReal.toFixed(2);

                row.append($('<td class="text-center" style="width:100px;">').append('%' + PorcUBReal));

                var PorcUBPlaneada = 0;
                PorcUBPlaneada = parseFloat(Lst[i].PorcUBPlaneada);
                PorcUBPlaneada = PorcUBPlaneada.toFixed(2);;
                row.append($('<td class="text-center" style="width:100px;">').append('%' + PorcUBPlaneada));

                var VarianzaUBrutaPuntos = 0;
                VarianzaUBrutaPuntos = parseFloat(Lst[i].VarianzaUBrutaPuntos);
                VarianzaUBrutaPuntos = VarianzaUBrutaPuntos.formatMoney(2, '.', ',');

                row.append($('<td class="text-center" style="width:100px;">').append('$' + VarianzaUBrutaPuntos));

                var ImpactoPesos = 0;
                ImpactoPesos = parseFloat(Lst[i].ImpactoPesos);
                ImpactoPesos = ImpactoPesos.formatMoney(2, '.', ',');
                row.append($('<td class="text-center" style="width:100px;">').append('$' + ImpactoPesos));
                $('#tblReporteDinamico > tbody').append(row);
            }


            $('#spinner_GCIndice').css('display', 'none');
            $('#btnCargarListado').attr('disabled', false);

        }).fail(function (jqXHR, textStatus, error) {
            if (jqXHR.status == 401) {
                alert('La sessión ha expirado.');
                $('#dvDialogoInicioSesion').appendTo("body").modal('show');
            }
            $('#spinner_GCIndice').css('display', 'none');
            $('#btnCargarListado').attr('disabled', false);
            alertify.error(jqXHR.responseText);
            console.log(jqXHR);
        });
    },

    btnAplicar_Click: function () {
        let Anio = $('#tbAnio').val();
        let Mes = $('#cmbMes').find(':selected').data('mes');
        GC_PageNumber = 1;
        Reporte.Cargar_Indice(Anio, Mes, GC_Orden);
    }

}

// 
$(document).ready(function () {



    $('#tbFecha').Zebra_DatePicker({
        format: 'd/m/Y',
        always_visible: $('#container'),
        onSelect: function () {
            //var id_u = $('#ddlRepresentante').find(':selected').data('id_u');
            //alert(id_u);
            btnCargarAgenda();
        }
    });

    var Fecha = new Date(FechaCalenda);
    var Mes = Fecha.getMonth() + 1;
    var Anio = Fecha.getFullYear();
    $('#tbAnio').val(Anio);
    $('#cmbMes').val(Mes);

    GC_PaginaActual = 1;




});