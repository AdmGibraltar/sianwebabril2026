
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
        excel.set(0, 0, line + 1, "Reporte Venta Documento Producto", formatTitulo);

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
            border: "thin #333333,thin #333333,thin #333333,thin #333333",
            format: '$#,##0;[Red]-$#,##0',
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

        var headers = ["#", "Tipo de Documento", "Folio Fiscal/UUID", "Número de Documento", "Fecha Cancelación", "Estatus del Documento", " Código de Cliente/Proveedor", "Cliente/Proveedor", "Grupo", "# Territorio","Territorio", "Fecha de Contabilización", "Total del Documento", "Impuesto Total", "SubTotal", "Código de Producto", "Descripción", "Cantidad", "Precio", "LineTotal"];

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

        var contador = 0;
        for (var i = 0; i < Lst?.length; i++) {
            // Imprime registro Normal
            var filExe = 0;
            contador++;
            excel.set(0, filExe, line, contador, formatCell_C);

            filExe += 1;
            excel.set(0, filExe, line, Lst[i].Tipo, formatCell_C); // PRODUCTO
            filExe += 1;
            excel.set(0, filExe, line, Lst[i].UUID, formatCell_C); // PRODUCTO
            filExe += 1;
            excel.set(0, filExe, line, Lst[i].NumeroDocumento, formatCell_C); // PRODUCTO

            //filExe += 1;
            //excel.set(0, filExe, line, Lst[i].Cancelado, formatCell_C); // PRODUCTO

            filExe += 1;
            excel.set(0, filExe, line, Lst[i].FechaCan, formatCell_C); // PRODUCTO


            filExe += 1;
            excel.set(0, filExe, line, Lst[i].EstatusDoc, formatCell_C); // PRODUCTO


            filExe += 1;
            excel.set(0, filExe, line, Lst[i].Id_Cliente, formatCell_C); // PRODUCTO

            filExe += 1;
            excel.set(0, filExe, line, Lst[i].Cliente, formatCell_C); // PRODUCTO


            filExe += 1;
            excel.set(0, filExe, line, Lst[i].Grupo, formatCell_C); // PRODUCTO

            filExe += 1;
            excel.set(0, filExe, line, Lst[i].Id_Terr, formatCell_C); // PRODUCTO

            filExe += 1;
            excel.set(0, filExe, line, Lst[i].Territorio, formatCell_C); // PRODUCTO


            filExe += 1;
            excel.set(0, filExe, line, Lst[i].FechaContabilizacion, formatCell_C); // PRODUCT


            filExe += 1;
            var TotalDoc = 0; // 
            TotalDoc = parseFloat(Lst[i].TotalDoc);
            //TotalDoc = TotalDoc.formatMoney(2, '.', ',');
            excel.set(0, filExe, line, TotalDoc, formatCell_R);

            filExe += 1;
            var ImpuestoTotal = 0; // 
            ImpuestoTotal = parseFloat(Lst[i].ImpuestoTotal);
            //ImpuestoTotal = ImpuestoTotal.formatMoney(2, '.', ',');
            excel.set(0, filExe, line, ImpuestoTotal, formatCell_R);

            filExe += 1;
            var SubTotal = 0; // 
            SubTotal = parseFloat(Lst[i].SubTotal);
            //SubTotal = SubTotal.formatMoney(2, '.', ',');
            excel.set(0, filExe, line, SubTotal, formatCell_R);

            filExe += 1;
            excel.set(0, filExe, line, Lst[i].ItemCode, formatCell_C); // PRODUCTO

            filExe += 1;
            excel.set(0, filExe, line, Lst[i].Dscription, formatCell_C); // PRODUCTO

            filExe += 1;
            excel.set(0, filExe, line, Lst[i].Quantity, formatCell_C); // PRODUCTO

            filExe += 1;
            var Price = 0; // 
            Price = parseFloat(Lst[i].Price);
            //Price = Price.formatMoney(2, '.', ',');
            excel.set(0, filExe, line, Price, formatCell_R);

            filExe += 1;
            var LineTotal = 0; // 
            LineTotal = parseFloat(Lst[i].LineTotal);
           // LineTotal = LineTotal.formatMoney(2, '.', ',');
            excel.set(0, filExe, line, LineTotal, formatCell_R); // PRODUCTO


            line = line + 1;
        }

        //Periodo=Periodo.replace(/-/g,'');
        excel.generate("Reporte Venta Documento Producto _" + FechaFormateada + ".xlsx");

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



        var PARMS = '/api/UtilidadPrima/sp_ReportUtilidadVenta?' +
            //'PageNumber=' + GC_PageNumber + '&PageSize=' + ACyS_RenglonesXPagina +
            '&Anio=' + Anio +
            '&Mes=' + Mes + '&tipo=1';

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
            var cont = 0;
            for (i = 0; i < Lst?.length; i++) {
                cont++;
                var row = $('<tr id="TitleRow_' + i + '">');

                row.append($('<td class="text-center">').append(
                    '' + cont + ''
                ));
                row.append($('<td class="text-center">').append(
                    '' + Lst[i].Tipo + ''
                ));
                row.append($('<td class="text-center">').append(
                    '' + Lst[i].UUID + ''
                ));

                row.append($('<td class="text-left">').append(
                    '' + Lst[i].NumeroDocumento + ''
                ));
                //row.append($('<td class="text-left">').append(
                //    '' + Lst[i].Cancelado + ''
                //));

                row.append($('<td class="text-left">').append(
                    '' + Lst[i].FechaCan + ''
                ));

                row.append($('<td class="text-left">').append(
                    '' + Lst[i].EstatusDoc + ''
                ));

                row.append($('<td class="text-left">').append(
                    '' + Lst[i].Id_Cliente + ''
                ));
                row.append($('<td class="text-left">').append(
                    '' + Lst[i].Cliente + ''
                ));
                row.append($('<td class="text-left">').append(
                    '' + Lst[i].Grupo + ''
                ));

                row.append($('<td class="text-left">').append(
                    '' + Lst[i].Id_Terr + ''
                ));

                row.append($('<td class="text-left">').append(
                    '' + Lst[i].Territorio + ''
                ))

                row.append($('<td class="text-left">').append(
                    '' + Lst[i].FechaContabilizacion + ''
                ));

                var TotalDoc = 0;
                TotalDoc = parseFloat(Lst[i].TotalDoc);
                TotalDoc = TotalDoc.formatMoney(2, '.', ',');

                row.append($('<td class="text-center" style="width:100px;">').append('$' + TotalDoc));

                var ImpuestoTotal = 0;
                ImpuestoTotal = parseFloat(Lst[i].ImpuestoTotal);
                ImpuestoTotal = ImpuestoTotal.formatMoney(2, '.', ',');
                row.append($('<td class="text-center" style="width:100px;">').append('$' + ImpuestoTotal));


                var SubTotal = 0;
                SubTotal = parseFloat(Lst[i].SubTotal);
                SubTotal = SubTotal.formatMoney(2, '.', ',');

                row.append($('<td class="text-center" style="width:100px;">').append('$' + SubTotal));

                row.append($('<td class="text-left">').append(
                    '' + Lst[i].ItemCode + ''
                ));

                row.append($('<td class="text-left">').append(
                    '' + Lst[i].Dscription + ''
                ));

                row.append($('<td class="text-left">').append(
                    '' + Lst[i].Quantity + ''
                ));

                var Price = 0;
                Price = parseFloat(Lst[i].Price);
                Price = Price.formatMoney(2, '.', ',');

                row.append($('<td class="text-center" style="width:100px;">').append('$' + Price));

                var LineTotal = 0;
                LineTotal = parseFloat(Lst[i].LineTotal);
                LineTotal = LineTotal.formatMoney(2, '.', ',');

                row.append($('<td class="text-center" style="width:100px;">').append('$' + LineTotal));



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