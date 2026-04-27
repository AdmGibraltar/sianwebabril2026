
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

var Representantes = {

    CargaCombo: function (IdZona, CALLBACK_Exito, CALLBACK_Error) {
        let Anio = $('#tbAnio').val();
        let Mes = $('#cmbMes').val();
        let Rol = $('#CmbRol').find(':selected').data('rol');
        $('#btnCargarListado').attr('disabled', true);
        $.ajax({
            url: _ApplicationUrl + '/api/GC/spCatRik_ComboTodos_ver3?Anio=' + Anio + '&Mes=' + Mes + '&Rol=' + Rol + '&Id_Param5=0',
            cache: false,
            type: 'GET'
        }).done(function (response, textStatus, jqXHR) {
            Estado = response.Estado;
            listado = response.Datos;
            //console.log(response);        
            var ddl = $('#ddlRepresentante').empty();
            ddl.append(
                $('<option data-Id_U=0>').val(0).text('-- Todos --')
            );

            if (Estado == 1) {
                var ID = 0;
                for (var i = 0; i < listado.length; i++) {
                    ddl.append(
                        $('<option data-Id_U=' + listado[i].Id_U + ' data-IdRik=' + listado[i].Id_Rik + ' >').val(listado[i].Id_U).text(listado[i].U_Nombre)
                    );
                    ID = listado[i].Id_U;
                }
                ddl.val(ID);
                if (hfId_Rik > 0) {
                    ddl.val(hfId_Rik);
                }
                //if (Id_TU == 2 || Id_TU == 3 || Id_TU == 4 || Id_TU == 5 || Id_TU == 1 || Id_TU == 14 || Id_TU == 15) {
                //    ddl.removeAttr('disabled');
                //} else {
                //    ddl.prop('disabled', 'disabled');
                //}

                $('#ddlRepresentante').val(0);

                //$('#spinner_GCIndice').css('display', 'none');
                $('#btnCargarListado').attr('disabled', false);
                if (CALLBACK_Exito) {
                    CALLBACK_Exito();
                }
            } else {
                if (CALLBACK_Error) {
                    CALLBACK_Error();
                }
            }
        }).fail(function (jqXHR, textStatus, error) {
            //$('#spinner_GCIndice').css('display', 'none');
            if (CALLBACK_Error) {
                CALLBACK_Error();
            }
            $('#btnCargarListado').attr('disabled', false);
            alertify.error('Error: Carga de representantes');
            console.log(jqXHR);
        });
    }
}

var Territorios = {

    Cargar_Territorios_Ver2: function (Id_Rik, CALLBACK_Exito, CALLBACK_Error) {
        let Anio = $('#tbAnio').val();
        let Mes = $('#cmbMes').val();
        $('#btnCargarListado').attr('disabled', true);

        $.ajax({
            url: _ApplicationUrl + '/api/GC/spCatTerritorio_PorRik_Ver2?Anio=' + Anio + '&Mes=' + Mes + '&Id_Rik=' + Id_Rik + '&Par1=0&Par2=0&Par3=0',
            cache: false,
            type: 'GET'
        }).done(function (response, textStatus, jqXHR) {
            Estado = response.Estado;
            listado = response.Datos;

            var ddl = $('#ddlTerritorio').empty();
            ddl.append(
                $('<option data-Id_Ter=0>').val(0).text('-- Todos --')
            );
            if (Estado == 1) {
                var ID = 0;
                for (var i = 0; i < listado.length; i++) {
                    ddl.append(
                        $('<option data-Id_Ter' + listado[i].Id_Ter + ' >').val(listado[i].Id_Ter).text(listado[i].Id_Ter + ' ' + listado[i].Ter_Nombre)
                    );
                    ID = listado[i].Id_Ter;
                }
                ddl.val(0);
                $('#btnCargarListado').attr('disabled', false);
                if (CALLBACK_Exito) {
                    CALLBACK_Exito();
                }
            } else {
                alertify.error('Error: Cargar_Territorios_Ver2(108)');
                if (CALLBACK_Error) {
                    CALLBACK_Error();
                }
            }
        }).fail(function (jqXHR, textStatus, error) {
            $('#btnCargarListado').attr('disabled', false);
            alertify.error('Error:' + jqXHR.responseText);
            console.log(jqXHR);
            if (CALLBACK_Error) {
                CALLBACK_Error();
            }
        });
    },

    Cargar_Territorios: function (Id_Rik, CALLBACK_Exito) {
        $('#spinner_GCIndice').css('display', 'block');
        $('#btnCargarListado').attr('disabled', true);

        $.ajax({
            url: _ApplicationUrl + '/api/CatTerritorio/GetTerritoriosPorRik?Id_Rik=' + Id_Rik + '&Par1=0&Par2=0',
            cache: false,
            type: 'GET'
        }).done(function (response, textStatus, jqXHR) {
            Estado = response.Estado;
            listado = response.Datos;

            var ddl = $('#ddlTerritorio').empty();
            ddl.append(
                $('<option data-Id_Ter=0>').val(0).text('-- Todos --')
            );
            if (Estado == 1) {
                var ID = 0;
                for (var i = 0; i < listado.length; i++) {
                    ddl.append(
                        $('<option data-Id_Ter' + listado[i].Id_Ter + ' >').val(listado[i].Id_Ter).text(listado[i].Id_Ter + ' ' + listado[i].Ter_Nombre)
                    );
                    ID = listado[i].Id_Ter;
                }
                ddl.val(0);
                $('#spinner_GCIndice').css('display', 'none');
                $('#btnCargarListado').attr('disabled', false);
            }
            if (CALLBACK_Exito) {
                CALLBACK_Exito();
            }
        }).fail(function (jqXHR, textStatus, error) {
            $('#spinner_GCIndice').css('display', 'none');
            $('#btnCargarListado').attr('disabled', false);
            alertify.error('Error:' + jqXHR.responseText);
            console.log(jqXHR);
        });
    }

}

var RepCumplimientoVI = {

    Cargar_DetalleAjax: function (Id_Cte, CALLBACK_Exito) {

        //var Id_Cte = $(Obj).data('id_cte');
        //alert(Id_Cte);
        /*
        var hfDetalleFlag = $('#hfDetalleFlag_' + Id_Cte).val();
        if (hfDetalleFlag == 1) {
            $('#Drill_' + Id_Cte).toggleClass("down");
            $('#DetalleRow_' + Id_Cte).css('display', 'none');
            $('#tblDetalle_' + Id_Cte + ' > tbody').empty();
            $('#hfDetalleFlag_' + Id_Cte).val(0);
            return;
        }
        //alert(Id_Cte);
        $('#spinner_GCIndice').css('display', 'block');
        $('#DetalleRow_' + Id_Cte).css('display', '');
        */

        var Anio = $('#tbAnio').val();
        var Mes = $('#cmbMes').find(':selected').data('mes');
        var Id_Rik = $('#ddlRepresentante').val();
        var Id_Ter = $('#ddlTerritorio').val();
        var Id_U = $('#ddlRepresentante').find(':selected').data('id_u');
        var TextoBuscar = $('#tbTextoBuscar').val();
        let Rol = $('#CmbRol').find(':selected').data('rol');


        var NombreCliente = $('#tbNombreCliente').val();


        var PARMS = '/api/GC/spRepCumplimientoVI_ver3?' +
            //'PageNumber=' + GC_PageNumber + '&PageSize=' + ACyS_RenglonesXPagina +
            '&Anio=' + Anio +
            '&Mes=' + Mes +
            '&Id_Uen=0' +
            '&Id_Seg=0' +
            '&Id_Rik=' + Id_Rik +
            '&Id_Ter=' + Id_Ter +
            '&Id_Cte=' + Id_Cte +
            '&NombreCliente=' + NombreCliente +
            '&Tipo=2' +  // 1 Listado Clientes / 2 Detalle Productos
            '&TextoBuscar=' + TextoBuscar +
            '&CampoOrden=' + GC_Orden +
            '&OrdenDir=' + GC_OrdenDir +
            '&TipoCliente=' + -1 +
            '&Rol=' + Rol +
            '&Param12=0';



        $.ajax({
            url: _ApplicationUrl + PARMS,
            cache: false,
            type: 'GET',
            async: false,
            statusCode: {
                401: function (jqXHR, textStatus, errorThrown) {
                    $('#dvDialogoInicioSesion').appendTo("body").modal();
                    _onLoginSuccessful = $.proxy(Consulta_Producto, null, $, Id_Prd, rowno, Spinner, CALLBACK);
                }
            }
        }).done(function (response, textStatus, jqXHR) {
            Lst = response.Datos;
            Estado = response.Estado;
            if (Estado == 1) {
                if (CALLBACK_Exito) {
                    CALLBACK_Exito(Lst);
                }
            }
        }).fail(function (jqXHR, textStatus, error) {

            if (jqXHR.status == 401) {
                alert('La sessión ha expirado.');
                $('#dvDialogoInicioSesion').appendTo("body").modal('show');
            }

            $('#spinner_GCIndice').css('display', 'none');
            $('#btnCargarListado').attr('disabled', false);

            //alertify.error(jqXHR.responseText);
            //console.log(jqXHR);
        });
    },

    Exportar_Excel: function (Obj, CALLBACK_Exito) {

        var Desglosado = $(Obj).data('desglosado');
        //alert(Desglosado);
        $('#spinner_GCIndice').css('display', 'block');
        var Anio = $('#tbAnio').val();
        var Mes = $('#cmbMes').find(':selected').data('mes');
        var tipoCliente = $('#cmbTipoCuenta').find(':selected').data('tcuenta');
        var Id_Rik = $('#ddlRepresentante').val();
        var Id_Ter = $('#ddlTerritorio').val();
        var Id_U = $('#ddlRepresentante').find(':selected').data('id_u');
        var TextoBuscar = $('#tbTextoBuscar').val();
        var IdCte = $('#tbNumeroCliente').val();
        let Rol = $('#CmbRol').find(':selected').data('rol');

        IdCte = parseInt(IdCte);
        if (isNaN(IdCte)) {
            IdCte = 0;
        }
        var NombreCliente = $('#tbNombreCliente').val();


        var PARMS = '/api/GC/spRepCumplimientoVI_ver3?' +
            '&Anio=' + Anio +
            '&Mes=' + Mes +
            '&Id_Uen=0' +
            '&Id_Seg=0' +
            '&Id_Rik=' + Id_Rik +
            '&Id_Ter=' + Id_Ter +
            '&Id_Cte=' + IdCte +
            '&NombreCliente=' + NombreCliente +
            '&Tipo=1' +  // 1 Listado Clientes / 2 Detalle Productos
            '&TextoBuscar=' + TextoBuscar +
            '&CampoOrden=' + GC_Orden +
            '&OrdenDir=' + GC_OrdenDir +
            '&TipoCliente=' + tipoCliente +
            '&Rol=' + Rol +
            '&Param12=0';



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
            Estado = response.Estado;
            if (Estado == 1) {
                if (CALLBACK_Exito) {
                    CALLBACK_Exito();
                }
            }
            $('#spinner_GCIndice').css('display', 'none');
        }).fail(function (jqXHR, textStatus, error) {
            if (jqXHR.status == 401) {
                alert('La sessión ha expirado.');
                $('#dvDialogoInicioSesion').appendTo("body").modal('show');
            }
            $('#spinner_GCIndice').css('display', 'none');
            alertify.error(jqXHR.responseText);

        });
    },

    Bajar_Excel: function (Lst, Desglosado) {

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
        var Representante = $('#ddlRepresentantesComercial option:selected').text();

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
        Fecha = Fecha.format("dd/mm/yyyy");
        excel.set(0, 0, line, "Cumplimiento de Venta Instalada", formatTitulo);
        excel.set(0, 0, line + 1, "Periodo: " + Anio + " " + Mes + " ", formatTitulo);
        excel.set(0, 0, line + 3, "Representante: " + Representante, formatTitulo);
        excel.set(0, 0, line + 3, "Fecha: " + Fecha, formatTitulo);
        //excel.set(0, 0, line + 1, "", formatTitulo);                
        //excel.set(0,0,line+3,"Periodo: "+Periodo, formatTitulo);             
        var CDI_Nombre = 'NOMBRE CDI';
        excel.set(0, 0, line + 4, "CDS : " + CDI_Nombre, formatTitulo);

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

        var headers = ["Num. Cte.", "Producto", "Cliente / Producto", "Venta Total del mes", "Venta del Mes (Acys)", "Vta. Instalada (Acys)",
            "Vta. del Mes vs VI", "% Cumplimiento VI en el mes", "Venta Prom. Trim. Ant.", "Prom. Trim. Ant. vs VI", "% Cumpl. Acys vs Prom. Trim. Ant"];

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

        for (var i = 0; i < Lst.length; i++) {
            // Imprime registro Normal

            excel.set(0, 0, line, Lst[i].Id_Cte, formatCell_C);
            excel.set(0, 1, line, "-", formatCell_C); // PRODUCTO
            excel.set(0, 2, line, Lst[i].Cte_NomComercial, formatCell);

            /*
            var V1 = 0; // Venta Instalda Consolidada
            V1 = parseFloat(Lst[i].MontoConsolidado);
            Total1 = Total1 + V1;
            V1 = V1.formatMoney(2, '.', ',');
            excel.set(0, 3, line, V1, formatCell_R);
            */

            var V1 = 0; // 
            V1 = parseFloat(Lst[i].VtaMesTot);
            Total1 = Total1 + V1;
            V1 = V1.formatMoney(2, '.', ',');
            excel.set(0, 3, line, V1, formatCell_R);

            var V2 = 0; // Venta del Mes
            V2 = parseFloat(Lst[i].VtaMes);
            Total2 = Total2 + V2;
            V2 = V2.formatMoney(2, '.', ',');
            excel.set(0, 4, line, V2, formatCell_R);

            var V3 = 0; // Vta Instalda (Acsys)
            V3 = parseFloat(Lst[i].VtaInst);
            Total3 = Total3 + V3;
            V3 = V3.formatMoney(2, '.', ',');
            excel.set(0, 5, line, V3, formatCell_R);

            var V4 = 0; // Vta del mes vs VI
            V4 = parseFloat(Lst[i].MESVI);
            Total4 = Total4 + V4;
            V4 = V4.formatMoney(2, '.', ',');
            excel.set(0, 6, line, V4, formatCell_R);

            var V5 = 0; // % Cumplimineto VI en el mes
            V5 = parseFloat(Lst[i].PorcMes);
            V5 = V5 * 100;
            Total5 = Total5 + V5;
            excel.set(0, 7, line, V5 + '%', formatCell_C);

            var V9 = 0; // Venta Promedio Trimestre Anterior
            V9 = parseFloat(Lst[i].VtaProm);
            V9 = V9;
            Total9 = Total9 + V9;
            V9 = V9.formatMoney(0, '.', ',');
            excel.set(0, 8, line, V9, formatCell_R);

            var V6 = 0; // Prom. Trim. Ant Vs Mes
            V6 = parseFloat(Lst[i].TRIMVI);
            Total6 = Total6 + V6;
            V6 = V6.formatMoney(2, '.', ',');
            excel.set(0, 9, line, V6, formatCell_R);

            var V7 = 0; // % Cumplimiento Acys vs Prom. Trim. Ant. 
            V7 = parseFloat(Lst[i].PorcTRIM);
            Total7 = Total7 + V7;
            V7 = V7 * 100;
            V7 = V7.formatMoney(2, '.', ',');
            excel.set(0, 10, line, V7 + '%', formatCell_R);

            /*
            var V8 = 0; // Venta Total del Mes
            V8 = parseFloat(Lst[i].VtaMesTot);
            Total8 = Total8 + V8;
            V8 = V8.formatMoney(2, '.', ',');
            excel.set(0, 11, line, V8, formatCell_R);
            */

            line = line + 1;

            // DEGLOSADO
            if (Desglosado == 1) {
                RepCumplimientoVI.Cargar_DetalleAjax(Lst[i].Id_Cte, function (LstDetalle) {
                    //RepCumplimientoVI.Bajar_Excel(Lst, Desglosado);
                    for (var y = 0; y < LstDetalle.length; y++) {
                        excel.set(0, 0, line, '', formatCell_Amarillo_X);
                        excel.set(0, 1, line, LstDetalle[y].Id_Prd, formatCell_C); // PRODUCTO
                        excel.set(0, 2, line, LstDetalle[y].Prd_Descripcion, formatCell);

                        /*
                        var V1 = 0; // Venta Instalda Consolidada
                        V1 = parseFloat(LstDetalle[y].MontoConsolidado);
                        V1 = V1.formatMoney(2, '.', ',');
                        excel.set(0, 3, line, V1, formatCell_R);
                        */
                        var V1 = 0; // Venta Instalda Consolidada
                        V1 = parseFloat(LstDetalle[y].VtaMesTot);
                        V1 = V1.formatMoney(2, '.', ',');
                        excel.set(0, 3, line, V1, formatCell_R);

                        var V2 = 0; // Venta del Mes
                        V2 = parseFloat(LstDetalle[y].VtaMes);
                        V2 = V2.formatMoney(2, '.', ',');
                        excel.set(0, 4, line, V2, formatCell_R);

                        var V3 = 0; // Vta Instalda (Acsys)
                        V3 = parseFloat(LstDetalle[y].VtaInst);
                        V3 = V3.formatMoney(2, '.', ',');
                        excel.set(0, 5, line, V3, formatCell_R);

                        var V4 = 0; // Vta del mes vs VI
                        V4 = parseFloat(LstDetalle[y].MESVI);
                        V4 = V4.formatMoney(2, '.', ',');
                        excel.set(0, 6, line, V4, formatCell_R);

                        var V5 = 0; // % Cumplimineto VI en el mes
                        V5 = parseFloat(LstDetalle[y].PorcMes);
                        V5 = V5 * 100;
                        excel.set(0, 7, line, V5 + '%', formatCell_C);

                        var V9 = 0; // Venta Promedio Trimestre Anterior
                        V9 = parseFloat(LstDetalle[y].VtaProm);
                        V9 = V9;
                        V9 = V9.formatMoney(0, '.', ',');
                        excel.set(0, 8, line, V9, formatCell_R);

                        var V6 = 0; // Prom. Trim. Ant Vs Mes
                        V6 = parseFloat(LstDetalle[y].TRIMVI);
                        V6 = V6.formatMoney(2, '.', ',');
                        excel.set(0, 9, line, V6, formatCell_R);

                        var V7 = 0; // % Cumplimiento Acys vs Prom. Trim. Ant. 
                        V7 = parseFloat(LstDetalle[y].PorcTRIM);
                        V7 = V7 * 100;
                        V7 = V7.formatMoney(2, '.', ',');
                        excel.set(0, 10, line, V7 + '%', formatCell_R);

                        /*
                        var V8 = 0; // Venta Total del Mes
                        V8 = parseFloat(LstDetalle[y].VtaMesTot);
                        V8 = V8.formatMoney(2, '.', ',');
                        excel.set(0, 11, line, V8, formatCell_R);
                        */

                        line = line + 1;
                    }
                });
            }
        }

        //Periodo=Periodo.replace(/-/g,'');
        excel.generate("Reporte Venta Instalada" + "1" + ".xlsx");

        $('#spinner_GCIndice').css('display', 'none');
        $('#btnCargarListado').attr('disabled', false);

    },

    AbrirAcuerdo: function (Obj, IdProd = 0) {
        var Id_Acs = $(Obj).data('id_acs');
        var Acs_version = $(Obj).data('acs_version');
        var ProcedenciaRepVI = $(Obj).data('procedencia_rep_vi');
        $('#modalListadoAcys').modal('hide');
        btnAcys_Editar(Obj, IdProd);
    },

}

var Reporte = {

    btnExportaExcel_Click: function (Obj) {
        var Desglosado = $(Obj).data('desglosado');
        RepCumplimientoVI.Exportar_Excel(Desglosado, function () {
            RepCumplimientoVI.Bajar_Excel(Lst, Desglosado);
        });
    },

    BuscarRepresentante_PorCliente: function (Id_Cte) {
        $('#spinner_GCIndice').css('display', 'block');
        var $Texto = $('#tbBuscarRepresentante').val();
        $.ajax({
            url: _ApplicationUrl + '/api/CrmRepresentante/spCatUsuario_SearchByCte?Id1=1&Id2=0&Id_Cte=' + Id_Cte,
            cache: false,
            type: 'GET'
        }).done(function (response, textStatus, jqXHR) {
            Estado = response.Estado;
            listado = response.Datos;

            $('#tblSeleccionUsuario > tbody').empty();

            if (Estado == 1) {
                for (var i = 0; i < listado.length; i++) {
                    if (listado[i].Id_U > 0) {
                        var row = $('<tr>');
                        row.append($('<td>').append(
                            listado[i].Id_Rik
                        ));
                        row.append($('<td>').append(
                            listado[i].U_Nombre
                        ));
                        row.append($('<td>').append(
                            listado[i].Id_Ter
                        ));
                        row.append($('<td class="text-center">').append(
                            '<button ' +
                            ' data-id_rik="' + listado[i].Id_Rik + '" ' +
                            ' data-id_u="' + listado[i].Id_U + '" ' +
                            ' data-u_nombre="' + listado[i].U_Nombre + '" ' +
                            ' data-id_cte="' + Id_Cte + '" ' +
                            ' onclick="RepCumplimientoVI.IniciaAcysNuevo(this);" ' +
                            'class="button">' +
                            '<span>Ok</span>' +
                            '</button>'
                        ));
                        $('#tblSeleccionUsuario > tbody').append(row);
                    }
                }
                $('#spinner_GCIndice').css('display', 'none');
            }
        }).fail(function (jqXHR, textStatus, error) {
            alertify.error('Error: Carga de representantes');

        });
    },

    EditarAcuerdo: function (Obj) {
        var Id_Cte = $(Obj).data('id_cte');
        var Id_Rik = $(Obj).data('id_rik');
        $('#modalListadoAcys').appendTo("body").modal('show');
        //$('#spinner_GCIndice').css('display', 'block');
        var Anio = $('#tbAnio').val();
        var Mes = $('#cmbMes').find(':selected').data('mes');
        var Id_Rik = $('#ddlRepresentante').val();
        var Id_U = $('#ddlRepresentante').find(':selected').data('id_u');
        var TextoBucar = $('#tbTextoBuscar').val();

        var PARMS = '/api/CatAcys/spAcuerdosExistentesPorCliente?&IdCte=' + Id_Cte + '&IdRik=' + Id_Rik

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
            Lst = response?.Datos || [];

            Reporte.crearRowAcuerdo(Lst, Id_Cte);
        
        }).fail(function (jqXHR, textStatus, error) {
            if (jqXHR.status == 401) {
                alert('La sessión ha expirado.');
                $('#dvDialogoInicioSesion').appendTo("body").modal('show');
            }
            $('#spinner_GCIndice').css('display', 'none');
            alertify.error(jqXHR.responseText);

        });
    },
    crearRowAcuerdo: function (Lst, Id_Cte, IdProd = 0, VtaMes = 0) {
        // CARGA TABLA DE ACUERDOS
        $('#tblAcuerdos > tbody').empty();
        for (i = 0; i < Lst.length; i++) {

            let Acs_Estatus = Lst[i].Acs_Estatus;
            if (Acs_Estatus == 'A') {
                Acs_Estatus = 'Autorizado';
            } if (Acs_Estatus == 'B') {
                Acs_Estatus = 'Baja';
            } if (Acs_Estatus == 'C') {
                Acs_Estatus = 'Capturado';
            }

            var row = $('<tr id="TitleRow_' + Lst[i].Id_Acs + '">');
            row.append($('<td class="text-center" style="width:100px; ">').append(
                '<label>' + Lst[i].Id_Acs + '</label>'
            ));
            //row.append($('<td class="text-center" style="width:100px; ">').append(
            //    '<label>' + Lst[i].Id_AcsVersion + '</label>'
            //));
            row.append($('<td class="text-center" style="width:100px; ">').append(
                '<label>' + Acs_Estatus + '</label>'
            ));
            row.append($('<td class="text-center" style="width:100px; ">').append(
                '<label>' + Lst[i].Id_Ter + '</label>'
            ));
            row.append($('<td class="text-center" style="width:100px; ">').append(
                '<label>' + Lst[i].Id_Rik + '</label>'
            ));
            row.append($('<td class="text-center" style="width:100px; ">').append(
                '<label>' + Lst[i].Acs_FechaFin == null ? '' : Lst[i].Acs_FechaFin + '</label>'
            ));

            let btnOpenMod = "RepCumplimientoVI.AbrirAcuerdo(this);";
            if (IdProd > 0) {
                btnOpenMod = "Reporte.AbrirModalguardaProducto(this);";
            }
            row.append($('<td>').append(
                '<button id="btnCargarListado" ' +
                'type="button" class="btn btn-default btn-sm"' +
                'data-procedencia_rep_vi="1"' +
                'data-id_acs="' + Lst[i].Id_Acs + '"' +
                'data-acs_version="' + Lst[i].Id_AcsVersion + '"' +
                'data-id_cte="' + Id_Cte + '"' +
                'data-id_ter="' + Lst[i].Id_Ter + '"' +
                'data-cod_prod="' + IdProd + '"' +
                'data-vtames="' + VtaMes + '"' +
                'onclick="' + btnOpenMod + '">' +
                '<i class="fa fa-pencil" aria-hidden="true"></i>' +
                '</button>'
            ));

            $('#tblAcuerdos > tbody').append(row);
        }
    },
    CrearAcuerdo: function (Obj) {
        $Id_Cte = $(Obj).data('id_cte');
        //MODAL BUSQUEDA DE USUARIO
        $('#modalListadoUsuarios').appendTo("body").modal('show');
        $('#tblSeleccionUsuario > tbody').empty();
        Reporte.BuscarRepresentante_PorCliente($Id_Cte);
    },

    PreCargaIndice: function (Obj) {
        let Anio = $('#tbAnio').val();
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
        Reporte.Cargar_Indice(Anio, Mes, GC_Orden);
    },

    Cargar_Detalle: function (Id_Cte, tipoC = '', id_rik = 0, AcysCount = 0) {
        let hfDetalleFlag = $('#hfDetalleFlag_' + Id_Cte).val();
        if (hfDetalleFlag == 1) {
            $('#Drill_' + Id_Cte).toggleClass("down");
            $('#DetalleRow_' + Id_Cte).css('display', 'none');
            $('#tblDetalle_' + Id_Cte + ' > tbody').empty();
            $('#hfDetalleFlag_' + Id_Cte).val(0);
            return;
        }
        $('#spinner_GCIndice').css('display', 'block');
        $('#btnCargarListado').attr('disabled', true);
        $('#DetalleRow_' + Id_Cte).css('display', '');
        let Anio = $('#tbAnio').val();
        let Mes = $('#cmbMes').find(':selected').data('mes');
        let Rol = $('#CmbRol').find(':selected').data('rol');
        let Id_Rik = $('#ddlRepresentante').val();
        var Id_Ter = $('#ddlTerritorio').val();
        let Id_U = $('#ddlRepresentante').find(':selected').data('id_u');
        let TextoBuscar = $('#tbTextoBuscar').val();


        var NombreCliente = $('#tbNombreCliente').val();


        let PARMS = '/api/GC/spRepCumplimientoVI_ver3?' +
            //'PageNumber=' + GC_PageNumber + '&PageSize=' + ACyS_RenglonesXPagina +
            '&Anio=' + Anio +
            '&Mes=' + Mes +
            '&Id_Uen=0' +
            '&Id_Seg=0' +
            '&Id_Rik=' + Id_Rik +
            '&Id_Ter=' + Id_Ter +
            '&Id_Cte=' + Id_Cte +
            '&NombreCliente=' + NombreCliente +
            '&Tipo=2' +  // 1 Listado Clientes / 2 Detalle Productos
            '&TextoBuscar=' + TextoBuscar +
            '&CampoOrden=' + GC_Orden +
            '&OrdenDir=' + GC_OrdenDir +
            '&TipoCliente=' + -1 +
            '&Rol=' + Rol +
            '&Param12=0';



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
            $('#Drill_' + Id_Cte).toggleClass("down");
            $('#tblDetalle_' + Id_Cte + ' > tbody').empty();
            //ACyS_RegistroEncontrados = Lst[0].RegistrosEcontrados;
            ACyS_RegistroEncontrados = 0;
            for (i = 0; i < Lst.length; i++) {
                let row = $('<tr id="ProdRow_' + Lst[i].IdCrmVisita + '">');

                let btnCargaProdAcys = `<button type="button" onclick=" Reporte.ConsultarAcuerdosIG(${Id_Cte},${Id_Rik}, ${Lst[i].Id_Prd}, ${AcysCount},${Lst[i].VtaProm})" class="btn btn-sm btn-info" title="Cargar producto en Acys">+</button>`
                if (tipoC == 'CN' || Lst[i].TipoVTA == 1) {
                    btnCargaProdAcys = '<label></label>';
                }

                row.append($('<td class="text-center" style="width:60px; min-width:60px;" >').append(
                    '' + btnCargaProdAcys
                ));

                if (Lst[i].TipoPTA == 0) {
                    // VI del Acys Azul
                    if (Lst[i].TipoVTA == 1) {
                        row.append($('<td class="text-left" style="width:90px; background-color:#037ef3">').append(
                            '&nbsp;&nbsp;' + '<label style="">' + Lst[i].Id_Prd + '<label>'
                        ));
                    }
                    // VE del Acys Naranja
                    if (Lst[i].TipoVTA == 0) {
                        row.append($('<td class="text-left" style="width:90px; background-color:#ffc845">').append(
                            '&nbsp;&nbsp;' + '<label style="">' + Lst[i].Id_Prd + '<label>'
                        ));
                    }
                    // Trimestral
                    /*
                    if (Lst[i].TipoVTA == 2) {
                        row.append($('<td class="text-left" style="width:90px; background-color:#caccd1">').append(
                            '&nbsp;&nbsp;' + '<label style="">' + Lst[i].Id_Prd + '<label>'
                        ));
                    }
                    */
                } else {
                    /*
                    row.append($('<td class="text-left" style="width:90px; background-color:#caccd1">').append(
                            '&nbsp;&nbsp;' + '<label style="">' + Lst[i].Id_Prd + '<label>'
                    ));
                    */
                    // VI del Acys Azul
                    if (Lst[i].TipoVTA == 1) {
                        row.append($('<td class="text-left" style="width:90px; background-color:#037ef3">').append(
                            '&nbsp;&nbsp;' + '<label style="">' + Lst[i].Id_Prd + '<label>'
                        ));
                    } else {
                        // Si esta Dentro del Acys se marca en color azul
                        // GRIS Productos Vendidos en Ultimos Tres Meses Fuera del Acys
                        row.append($('<td class="text-left" style="width:90px; background-color:#caccd1">').append(
                            '&nbsp;&nbsp;' + '<label style="">' + Lst[i].Id_Prd + '<label>'
                        ));
                    }
                    /*
                    // VE del Acys Naranja
                    if (Lst[i].TipoVTA == 0) {
                        row.append($('<td class="text-left" style="width:90px; background-color:#ffc845">').append(
                            '&nbsp;&nbsp;' + '<label style="">' + Lst[i].Id_Prd + '<label>'
                        ));
                    }
                    */
                }
                // COL 2

                row.append($('<td class="text-left" >').append(
                    Lst[i].Prd_Descripcion + '</label>'
                ));
                /*
                var V1 = 0;
                V1 = parseFloat(Lst[i].MontoConsolidado);
                V1 = V1.formatMoney(2, '.', ',');
                row.append($('<td class="text-right" style="width:100px;">').append(V1));
                */

                // COL 3
                var V8 = 0;
                V8 = parseFloat(Lst[i].VtaMesTot);
                V8 = V8.formatMoney(2, '.', ',');
                row.append($('<td class="text-center" style="width:100px;">').append('$' + V8));

                // COL 4
                var V2 = 0;
                V2 = parseFloat(Lst[i].VtaMes);
                V2 = V2.formatMoney(2, '.', ',');
                row.append($('<td class="text-center" style="width:100px;">').append('$' + V2));

                // COL 5
                var V3 = 0;
                V3 = parseFloat(Lst[i].VtaInst);
                V3 = V3.formatMoney(2, '.', ',');
                row.append($('<td class="text-center" style="width:100px;">').append('$' + V3));

                // COL 6
                var V4 = 0;
                V4 = parseFloat(Lst[i].MESVI);
                V4 = V4.formatMoney(2, '.', ',');
                row.append($('<td class="text-center" style="width:100px;">').append('$' + V4));

                var V5 = 0;
                V5 = parseFloat(Lst[i].PorcMes);
                V5 = V5 * 100;
                V5 = parseInt(V5);
                V5 = V5.formatMoney(0, '.', ',');

                var cssColor = Reporte.ColPorcentaje_Color(V5);
                // COL 7
                row.append($('<td class="text-center" style="width:100px;">').append(
                    '<span title="" class="' + cssColor + '">' + V5 + ' %</span>'
                ));

                //row.append($('<td class="text-center" style="width:100px;">').append(V5 + ' %'));
                // COL 8
                var V9 = 0;
                V9 = parseFloat(Lst[i].VtaProm);
                V9 = V9.formatMoney(2, '.', ',');
                row.append($('<td class="text-center" style="width:100px;">').append('$' + V9));

                // COL 9
                var V6 = 0;
                V6 = parseFloat(Lst[i].TRIMVI);
                V6 = V6.formatMoney(2, '.', ',');
                row.append($('<td class="text-center" style="width:100px;">').append('$' + V6));



                var V7 = 0;
                V7 = parseFloat(Lst[i].PorcTRIM);
                V7 = V7 * 100;
                V7 = parseInt(V7);
                var cssColor3 = Reporte.ColPorcentaje_Color(V7);
                V7 = V7.formatMoney(0, '.', ',');
                // COL 10 
                row.append($('<td class="text-center;" style="width:100px;">').append(
                    '<span id="lbHorasUtilizadas_2080" title="" class="' + cssColor3 + '">' + V7 + ' %</span>'
                ));



                $('#tblDetalle_' + Id_Cte + ' > tbody').append(row);
            }

            $('#spinner_GCIndice').css('display', 'none');
            $('#btnCargarListado').attr('disabled', false);
            $('#hfDetalleFlag_' + Id_Cte).val(1);

        }).fail(function (jqXHR, textStatus, error) {

            if (jqXHR.status == 401) {
                alert('La sessión ha expirado.');
                $('#dvDialogoInicioSesion').appendTo("body").modal('show');
            }

            $('#spinner_GCIndice').css('display', 'none');
            $('#btnCargarListado').attr('disabled', false);
            alertify.error(jqXHR.responseText);

        });
    },
    ConsultarAcuerdosIG: function (Id_Cte, Id_Rik, Id_Prd, AcysCount, VtaMes) {

        if (AcysCount <= 0) {
            alertify.alert('Cliente sin registro de ACyS, favor de generar el ACyS correspondiente');
            return false;
        }

        var PARMS = '/api/CatAcys/spAcuerdosExistentesPorCliente?&IdCte=' + Id_Cte + '&IdRik=' + Id_Rik

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
            Lst = response?.Datos || [];

            if (Lst.length <= 0) {
                alertify.alert('No hay acuerdos');
                return;
            }
            if (Lst.length > 1) {
                $('#modalListadoAcys').appendTo("body").modal('show');
                Reporte.crearRowAcuerdo(Lst, Id_Cte, Id_Prd, VtaMes);
               
            } else {
                const { Id_Acs, Id_AcsVersion, Id_Cte, Id_Ter } = Lst[0];
                var Obj = document.createElement('div');
                Obj.classList.add('miClase'); // Agregar una clase ficticia para identificar el elemento

                // Asignar valores al atributo data utilizando JavaScript
                Obj.dataset.id_acs = Id_Acs;
                Obj.dataset.acs_version = Id_AcsVersion;
                Obj.dataset.id_cte = Id_Cte;
                Obj.dataset.id_ter = Id_Ter;
                Obj.dataset.cod_prod = Id_Prd;
                Obj.dataset.vtames = VtaMes;
                Obj.dataset.procedencia_rep_vi = "1";
                Reporte.AbrirModalguardaProducto(Obj);
            }
          
        }).fail(function (jqXHR, textStatus, error) {
            if (jqXHR.status == 401) {
                alert('La sessión ha expirado.');
                $('#dvDialogoInicioSesion').appendTo("body").modal('show');
            }
            $('#spinner_GCIndice').css('display', 'none');
            alertify.error(jqXHR.responseText);

        });
    },

    AbrirModalguardaProducto: function (Obj) {

        var btnGuardaProdIG = document.getElementById("btnGuardaProdIG");
        var CloseGuardaProdIG = document.getElementById("CloseGuardaProdIG");
        var iconbtnCloseGuardaProdIG = document.getElementById("iconbtnCloseGuardaProdIG");
        btnGuardaProdIG.disabled = false;
        CloseGuardaProdIG.disabled = false;
        iconbtnCloseGuardaProdIG.disabled = false;

        let Id_Acs = $(Obj).data('id_acs');
        let Id_AcsVersion = $(Obj).data('acs_version');
        let id_cte = $(Obj).data('id_cte');
        let thfId_Ter = $(Obj).data('id_ter');
        let tbCodigo = $(Obj).data('cod_prod');
        let VtaMes = $(Obj).data('vtames');
        document.getElementById('lblCantidadSelect').value = 1;

        $('#modalGuardaProdIG').appendTo("body").modal({
            backdrop: 'static',
            keyboard: false
        });

        document.getElementById('msjExistAsysDet').innerHTML = '';
        document.getElementById('lblNoAcysProdSelect').innerHTML = Id_Acs;
        document.getElementById('lblCodigoProdSelect').value = tbCodigo;
        document.getElementById('lblMontoProdSelect').value = VtaMes;

        document.getElementById('IdAcsSelect').value = Id_Acs;
        document.getElementById('IdVersionSelect').value = Id_AcsVersion;
        document.getElementById('IdTerSelect').value = thfId_Ter;

        $.ajax({
            url: _ApplicationUrl + '/api/CatAcysDet/SP_CapAcysDet_Exist' +
                '?IdAcys=' + Id_Acs +
                '&Id_Prod=' + tbCodigo,
            cache: false,
            type: 'GET',
            statusCode: {
                401: function (jqXHR, textStatus, errorThrown) {
                    $('#dvDialogoInicioSesion').appendTo("body").modal();
                    _onLoginSuccessful = $.proxy(Cargar_AcysByIdDetalle_Ajax, null, $, Id_Acys, Acs_Version, Acs_Estatus, CALLBACK);
                }
            }
        }).done(function (response, textStatus, jqXHR) {
            var lst = response;
            if (response > 0) {
                btnGuardaProdIG.disabled = true;
                document.getElementById('msjExistAsysDet').innerHTML = 'El producto ya fue agregado al acys';
            }
            var Mensaje = response.Mensaje;
            var Estado = response.Estado;
            if (Estado == 1) {
                if (CALLBACK_Exito) {
                    CALLBACK_Exito(lst);
                }
            }

            if (Estado == -1) {

                CALLBACK_Error(Mensaje);
            }

        }).fail(function (jqXHR, textStatus, error) {
            if (jqXHR.status == 401) {
                alert('La sessión ha expirado.');
                $('#dvModalPropuestaTE_ver2').modal('hide');
                $('#dvDialogoInicioSesion').appendTo("body").modal();
            } else {
                alertify.success('Error: funcion EXEC_spCapAcys2_GetLogs_Ajax.');
            }
        });

    },
    GuardaDetalleModal: function () {
        let tbCodigo = document.getElementById('lblCodigoProdSelect').value;

        let id_acs = document.getElementById('IdAcsSelect').value;
        let tbprecio = document.getElementById('lblMontoProdSelect').value;

        let tbcant = document.getElementById('lblCantidadSelect').value;
        let id_acsversion = document.getElementById('IdVersionSelect').value;
        let thfid_ter = document.getElementById('IdTerSelect').value;
        let lstdetalleproductos = [];
        var row = {
            'id_acys': 0,
            'id_matriz': 0,
            'id_emp': 0,
            'id_cd': 0,
            'id_acs': id_acs,
            'id_acsdet': 0,
            'id_reg': 0,
            'id_prd': tbCodigo,
            'acs_cantidad': tbcant,
            'acs_frecuencia': null,
            'acs_frecuenciatipo': null,
            'acs_lunes': 0,
            'acs_martes': 0,
            'acs_miercoles': 0,
            'acs_jueves': 0,
            'acs_viernes': 0,
            'acs_sabado': 0,
            'acs_documento': '',
            'acs_precio': tbprecio,
            'id_ter': thfid_ter,
            'acs_ultscpt': 0,
            'acs_ultacpt': 0,
            'acs_modalidad': 0,
            'acs_consigfechainicio': 0,
            'acs_consigfechafin': 0,
            'acs_canttotal': 0,
            'id_acsversion': id_acsversion,
            'id_tg': 0,
            'requiereoc': ''  // 29 items
        }
        lstdetalleproductos.push(row);

        var btnGuardaProdIG = document.getElementById("btnGuardaProdIG");
        var CloseGuardaProdIG = document.getElementById("CloseGuardaProdIG");
        var iconbtnCloseGuardaProdIG = document.getElementById("iconbtnCloseGuardaProdIG");
        btnGuardaProdIG.disabled = true;
        CloseGuardaProdIG.disabled = true;
        iconbtnCloseGuardaProdIG.disabled = true;

        Acys2_DetalleProductosUpdate_RptVti(id_acs, id_acsversion, 1, lstdetalleproductos,
            function () {
                $('#modalGuardaProdIG').modal('hide');
                btnGuardaProdIG.disabled = false;
                CloseGuardaProdIG.disabled = false;
                iconbtnCloseGuardaProdIG.disabled = false;

            }, function () { // callback error
                btnGuardaProdIG.disabled = false;
                CloseGuardaProdIG.disabled = false;
                iconbtnCloseGuardaProdIG.disabled = false;
                alertify.alert('error: en listado de productos');
            });
    },
    ColPorcentaje_Color: function (Valor_Porc) {
        var cssColor = 'Label_PorCVI_Rojo';
        if (Valor_Porc <= 60) {
            cssColor = 'Label_PorCVI_Rojo';
        } else if (Valor_Porc > 60 && Valor_Porc < 79) {
            cssColor = 'Label_PorCVI_Naranja';
        } else if (Valor_Porc > 80 && Valor_Porc <= 120) {
            cssColor = 'Label_PorCVI_Verde';
        } else if (Valor_Porc > 120 && Valor_Porc <= 140) {
            //cssColor = 'Label_PorCVI_Amarillo';            
            cssColor = 'Label_PorCVI_Naranja';
        } else if (Valor_Porc > 140) {
            cssColor = 'Label_PorCVI_Rojo';
        }
        return cssColor;
    },

    // INDICE 
    Cargar_Indice: function (Anio, Mes, Orden) {
        $('#spinner_GCIndice').css('display', 'block');
        $('#btnCargarListado').attr('disabled', true);
        //var Anio = $('#tbAnio').val();
        //var Mes = $('#cmbMes').find(':selected').data('mes');
        var Id_Rik = $('#ddlRepresentante').val();
        var tipoCliente = $('#cmbTipoCuenta').find(':selected').data('tcuenta');
        var Id_Ter = $('#ddlTerritorio').val();
        var IdCte = $('#tbNumeroCliente').val();
        let Rol = $('#CmbRol').find(':selected').data('rol');
        IdCte = parseInt(IdCte);
        if (isNaN(IdCte)) {
            IdCte = 0;
        }
        var NombreCliente = $('#tbNombreCliente').val();


        //var Id_U = $('#ddlRepresentante').find(':selected').data('id_u');
        $('#btnCargarListado').prop('title', 'Representante: ' + Id_Rik);
        var TextoBuscar = $('#tbTextoBuscar').val();


        var PARMS = '/api/GC/spRepCumplimientoVI_ver3?' +
            //'PageNumber=' + GC_PageNumber + '&PageSize=' + ACyS_RenglonesXPagina +
            '&Anio=' + Anio +
            '&Mes=' + Mes +
            '&Id_Uen=0' +
            '&Id_Seg=0' +
            '&Id_Rik=' + Id_Rik +
            '&Id_Ter=' + Id_Ter +
            '&Id_Cte=' + IdCte +
            '&NombreCliente=' + NombreCliente +
            '&Tipo=1' +  // 1 Listado Clientes / 2 Detalle Productos
            '&TextoBuscar=' + TextoBuscar +
            '&CampoOrden=' + GC_Orden +
            '&OrdenDir=' + GC_OrdenDir +
            '&TipoCliente=' + tipoCliente +
            '&Rol=' + Rol +
            '&Param12=0';

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

            var CatRows = Lst.length;

            for (i = 0; i < Lst.length; i++) {
                var row = $('<tr id="TitleRow_' + Lst[i].Id_Cte + '">');
                // Col 1 
                row.append($('<td class="text-center" style="width:150px; display:none;" >').append(
                    '<label>' +
                    '<select id="cmbPrioridad_' + Lst[i].Id_Cte + '" class="form-control">' +
                    '<option>-</option>' +
                    '<option>Bajo</option>' +
                    '<option>Medio</option>' +
                    '<option>Alto</option>' +
                    '<option>Urgente</option>' +
                    '</select>' +
                    '</label>'
                ));

                //row.append($('<td class="text-center">').append(
                //    '<label class="LinkCte">' + Lst[i].descripcionTipoCuenta + '</label>'
                //));

                row.append($('<td>').append(
                    '<table style="cursor:pointer;" onclick="Reporte.Cargar_Detalle(' + Lst[i].Id_Cte + ',\'' + Lst[i].descripcionTipoCuenta + '\',' + Id_Rik + ',' + Lst[i].AcysCount + ');"  data-id_cte="' + Lst[i].Id_Cte + '"' +
                    'data-id_rik="' + Id_Rik + '" >' +
                    '<tr>' +
                    '<td>' +
                    '<i ' +
                    'id="Drill_' + Lst[i].Id_Cte + '" ' +
                    'class="fa fa-chevron-right rotate_w" ' +
                    'style="margin-left:15px; margin-top:0px;"></i>' +
                    '</td>' +
                    '<td class="">' +
                    Lst[i].descripcionTipoCuenta +
                    '</td>' +
                    '</tr>' +
                    '</table>'
                ));

                row.append($('<td class="text-center">').append(
                    '<label class="LinkCte">' + Lst[i].Id_Cte + '</label>'
                ));

                //// Col 1 
                //row.append($('<td>').append(
                //    '<table style="cursor:pointer;" onclick="Reporte.Cargar_Detalle(' + Lst[i].Id_Cte + ');" data-id_cte="' + Lst[i].Id_Cte + '" >' +
                //    '<tr>' +
                //    '<td>' +
                //    '<i ' +
                //    'id="Drill_' + Lst[i].Id_Cte + '" ' +
                //    'class="fa fa-chevron-right rotate_w" ' +
                //    'style="margin-left:15px; margin-top:0px;"></i>' +
                //    '</td>' +
                //    '<td class="">' +
                //    Lst[i].Id_Cte +
                //    '</td>' +
                //    '</tr>' +
                //    '</table>'
                //)); 

                row.append($('<td class="text-left">').append(
                    '<label class="LinkCte">' + Lst[i].Cte_NomComercial + '</label>'
                ));
                // Col 3, Venta Total del Mes
                var V8 = 0;
                V8 = parseFloat(Lst[i].VtaMesTot);
                Total8 = Total8 + V8;
                V8 = V8.formatMoney(2, '.', ',');
                row.append($('<td class="text-center" style="width:100px;">').append('$' + V8));
                // Col4, Venta del Mes
                var V2 = 0;
                V2 = parseFloat(Lst[i].VtaMes);
                Total2 = Total2 + V2;
                V2 = V2.formatMoney(2, '.', ',');
                row.append($('<td class="text-center" style="width:100px;">').append('$' + V2));
                // Col 5, Vta Instalda (Acsys)
                var V3 = 0;
                V3 = parseFloat(Lst[i].VtaInst);
                Total3 = Total3 + V3;
                V3 = V3.formatMoney(2, '.', ',');
                row.append($('<td class="text-center" style="width:100px;">').append('$' + V3));
                // Col 6, Vta del mes(Acys) vs VI
                var V4 = 0;
                V4 = parseFloat(Lst[i].MESVI);
                Total4 = Total4 + V4;
                V4 = V4.formatMoney(2, '.', ',');
                row.append($('<td class="text-center" style="width:100px;">').append('$' + V4));
                // Col 7, % Cumplimineto VI en el mes
                var V5 = 0;
                V5 = parseFloat(Lst[i].PorcMes);
                V5 = V5 * 100;
                V5 = parseInt(V5);
                Total5 = Total5 + V5;
                //V5 = V5.formatMoney(0, '.', ',');
                //row.append($('<td class="text-center" style="width:100px;">').append(V5 + ' %'));

                var cssColor = Reporte.ColPorcentaje_Color(V5);

                row.append($('<td class="text-center" style="width:100px;">').append(
                    '<span id="lbHorasUtilizadas_2080" title="" class="' + cssColor + '">' + V5 + ' %</span>'
                ));

                // Col 8,  Venta Promedio Trimestre Anterior
                var V9 = 0;
                V9 = parseFloat(Lst[i].VtaProm);
                Total9 = Total9 + V9;
                V9 = V9.formatMoney(2, '.', ',');
                row.append($('<td class="text-center" style="width:100px;">').append('$' + V9));
                // COL 9,  Prom. Trim. Ant Vs VI
                var V6 = 0;
                V6 = parseFloat(Lst[i].TRIMVI);
                Total6 = Total6 + V6;
                V6 = V6.formatMoney(2, '.', ',');
                row.append($('<td class="text-center" style="width:100px;">').append('$' + V6));
                // % Cumplimiento Acys vs Prom. Trim. Ant. 
                var V7 = 0;
                V7 = parseFloat(Lst[i].PorcTRIM);
                Total7 = Total7 + V7;
                V7 = V7 * 100;
                V7 = parseInt(V7);
                var cssColor3 = Reporte.ColPorcentaje_Color(V7);
                V7 = V7.formatMoney(0, '.', ',');
                //row.append($('<td class="text-center" style="width:100px;">').append(V7 + ' %'));
                row.append($('<td class="text-center" style="width:100px;">').append(
                    '<span id="lbHorasUtilizadas_2080" title="" class="' + cssColor3 + '">' + V7 + ' %</span>'
                ));

                if (Lst[i].AcysCount <= 0) {
                    // CREAR
                    row.append($('<td class="text-center">').append(
                        '<button id="btnCargarListado_' + Lst[i].Id_Cte + '" ' +
                        'type="button" class="btn btn-warning btn-sm"' +
                        'data-id_cte="' + Lst[i].Id_Cte + '"' +
                        'data-id_rik="' + Id_Rik + '"' +
                        'tooltip="Haga click para iniciar la creación del Acys." ' +
                        'onclick="Reporte.CrearAcuerdo(this);">' +
                        '<i class="fa fa-plus" aria-hidden="true"></i>' +
                        '</button>'
                    ));
                } else {
                    // EDITAR 
                    row.append($('<td class="text-center">').append(
                        '<button id="btnCargarListado_' + Lst[i].Id_Cte + '" ' +
                        'type="button" class="btn btn-primary btn-sm"' +
                        'data-id_cte="' + Lst[i].Id_Cte + '"' +
                        'data-id_rik="' + Id_Rik + '"' +
                        'onclick="Reporte.EditarAcuerdo(this);">' +
                        '<i class="fa fa-search" aria-hidden="true"></i>' +
                        '</button>'
                    ));
                }

                $('#tblReporteDinamico > tbody').append(row);

                var row = $('<tr id="DetalleRow_' + Lst[i].Id_Cte + '">');

                row.append($('<td class="text-center" colspan="11">').append(
                    '<input type="hidden" id="hfDetalleFlag_' + Lst[i].Id_Cte + '" value="0" />' +
                    '<table class="table table-hover table-bordered" id="tblDetalle_' + Lst[i].Id_Cte + '" style="width:100%;">' +
                    '<tbody></tbody>' +
                    '</table>'
                ));

                $('#tblReporteDinamico > tbody').append(row);
                $('#DetalleRow_' + Lst[i].Id_Cte).css('display', 'none');

            }

            Total1 = Total1.formatMoney(2, '.', ',');
            $('#lbTotalCol1').text('$' + Total1);

            Total2 = Total2.formatMoney(2, '.', ',');
            $('#lbTotalCol2').text('$' + Total2);

            Total3 = Total3.formatMoney(2, '.', ',');
            $('#lbTotalCol3').text('$' + Total3);

            Total4 = Total4.formatMoney(2, '.', ',');
            $('#lbTotalCol4').text('$' + Total4);

            // 29OCT-2021 RFH
            var fT2 = Total2.replace(/,/g, '');
            fT2 = parseFloat(fT2);
            var fT3 = Total3.replace(/,/g, '');
            fT3 = parseFloat(fT3);

            console.log(Total2 + ' - ' + fT2);
            console.log(Total3 + ' - ' + fT3);

            var Total5 = fT2 / fT3;
            Total5 = Total5 * 100;
            console.log(Total5);
            var cssColor1 = Reporte.ColPorcentaje_Color(Total5);
            var fTotal5 = Total5.formatMoney(0, '.', '');
            $('#lbTotalCol5').html('<span id="lbHorasUtilizadas_2080" title="" class="' + cssColor1 + '">' + fTotal5 + ' %</span>');

            Total9 = Total9.formatMoney(2, '.', ',');
            $('#lbTotalCol9').text('$' + Total9);

            Total6 = Total6.formatMoney(2, '.', ',');
            $('#lbTotalCol6').text('$' + Total6);

            Total7 = Total7.formatMoney(2, '.', ',');
            $('#lbTotalCol7').text(Total7);

            Total8 = Total8.formatMoney(2, '.', ',');
            //var fTotal7 = Total8.formatMoney(0, '.', ',');
            $('#lbTotalCol8').text('$' + Total8);

            // 29 OCT-2021
            var fT9 = Total9.replace(/,/g, '');
            fT9 = parseFloat(fT9);
            var fT3 = Total3.replace(/,/g, '');
            fT3 = parseFloat(fT3);

            var Total7 = fT9 / fT3;
            Total7 = Total7 * 100;
            var cssColor1 = Reporte.ColPorcentaje_Color(Total7);
            var fTotal7 = Total7.formatMoney(0, '.', '');
            $('#lbTotalCol7').html('<span class="' + cssColor1 + '">' + fTotal7 + ' %</span>');

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

    $('body').on('hidden.bs.modal', function () {
        if ($('.modal.in').length > 0) {
            $('body').addClass('modal-open');
        }
    });

    $('#tbFecha').Zebra_DatePicker({
        format: 'd/m/Y',
        always_visible: $('#container'),
        onSelect: function () {
            //var id_u = $('#ddlRepresentante').find(':selected').data('id_u');
            //alert(id_u);
            btnCargarAgenda();
        }
    });

    $('#chbTipo').click(function () {
        var chbPorFechas = $('#chbTipo').is(':checked');
        if (chbPorFechas) {
            $('#rowTipo').css('display', 'block');
        } else {
            $('#rowTipo').css('display', 'none');
        }
    });

    $('#chbCliente').click(function () {
        var chbPorFechas = $('#chbCliente').is(':checked');
        if (chbPorFechas) {
            $('#rowCliente').css('display', 'block');
        } else {
            $('#rowCliente').css('display', 'none');
        }
    });

    $('#btnGuardaProdIG').click(function () {
        Reporte.GuardaDetalleModal();
    });

    var Fecha = new Date(FechaCalenda);
    var Mes = Fecha.getMonth() + 1;
    var Anio = Fecha.getFullYear();
    $('#tbAnio').val(Anio);
    $('#cmbMes').val(Mes);

    GC_PaginaActual = 1;

    //Cargar_Representante(0, function () {
    Representantes.CargaCombo(0, function () {

        Id_Rik = $('#ddlRepresentante').val();

        Territorios.Cargar_Territorios_Ver2(0, function () {
            //Cargar_Territorios(Id_Rik, function () {
            //RepCumplimientoVI.Cargar_Indice(GC_PaginaActual);
        });

        Id_Rik = parseInt(Id_Rik);
        if (isNaN(Id_Rik)) {
            Id_Rik = 0;
        }
        if (Id_Rik == -1) {
            $('#ddlTerritoriosDe').attr('disabled', true);
            $('#ddlTerritoriosDe').val(2);
        } else {
            $('#ddlTerritoriosDe').attr('disabled', true);
            $('#ddlTerritoriosDe').val(1);
        }
    });

    $(document).on('change', 'select[id="cmbMes"]', function () {
        $('#spinner_GCIndice').css('display', 'block');
        Representantes.CargaCombo(0, function () {
            Id_Rik = $('#ddlRepresentante').val();
            Territorios.Cargar_Territorios_Ver2(Id_Rik, function () {
                $('#spinner_GCIndice').css('display', 'none');
            }, function () {
                $('#spinner_GCIndice').css('display', 'none');
            });
        }, function () {
            $('#spinner_GCIndice').css('display', 'none');
        });
    });

    $(document).on('change', 'select[id="ddlRepresentante"]', function () {
        Id_Rik = $('#ddlRepresentante').val();
        Territorios.Cargar_Territorios_Ver2(Id_Rik, function () { });
    });


    $(document).on('change', 'select[id="CmbRol"]', function () {
        Representantes.CargaCombo(0, function () {

            Id_Rik = $('#ddlRepresentante').val();
            Territorios.Cargar_Territorios_Ver2(0, function () {
                //Cargar_Territorios(Id_Rik, function () {
                //RepCumplimientoVI.Cargar_Indice(GC_PaginaActual);
            });
            Id_Rik = parseInt(Id_Rik);
            if (isNaN(Id_Rik)) {
                Id_Rik = 0;
            }
            if (Id_Rik == -1) {
                $('#ddlTerritoriosDe').attr('disabled', true);
                $('#ddlTerritoriosDe').val(2);
            } else {
                $('#ddlTerritoriosDe').attr('disabled', true);
                $('#ddlTerritoriosDe').val(1);
            }
        });
    });


});