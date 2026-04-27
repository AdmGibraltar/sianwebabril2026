// 5OCT-2021 RFH
// 19Ago-2022 RFH Actualizado
// 30Ago-2022 RFH Actualizado

const DISPLAY = true;
const BORDER = true;
const CHART_AREA = true;
const TICKS = true;

var tbNumProyectos = 0;
var tbMontoProyectos = 0;
var tbAvanceMes = 0;
var tbCantidadCerrados = 0;
var tbMontoCerrados = 0;

var VALORES = ["0", "0", "0"];
var VALORES2 = ["1", "2", "3"];

var COLOR_Analisis = '#abebc6';
var COLOR_Promocion = '#58d68d';
var COLOR_Negociacion = '#f7CD6F';
var COLOR_Cierre = '#85c1e9';
var COLOR_Cancelado = '#d7DBDD';

//var color = Chart.helpers.color;
//var chartColors = window.chartColors;
var myPolarArea = null;
var ctx1 = document.getElementById('canvas1').getContext('2d');
var ctx2 = document.getElementById('canvas2').getContext('2d');

var DatosConsulta;



function Preparar_Grafica(CALLBACK_Exito) {
    var IdRik = 869;
    $('#Spinner_FullDashboard').css('display', 'block');

    if (typeof (idZona) == 'undefined' || idZona == null) {
        idZona = 0;
    }

    $.ajax({
        url: _ApplicationUrl + '/api/CrmInforme/?' +
            'TipoReporte=1' +
            '&Zona=-1' +
            '&Representante=' + IdRik +
            '&Periodo=-1' +
            '&Monto1=0' +
            '&Monto2=99999999',
        cache: false,
        type: 'GET'
    }).done(function (response, textStatus, jqXHR) {
        if (CALLBACK_Exito) {
            CALLBACK_Exito(response);
            $('#Spinner_FullDashboard').css('display', 'none');
        }
    }).fail(function (jqXHR, textStatus, error) {
        $('#Spinner_Cargando').css('display', 'none');
        //alertify.error('Ocurrió una complicación al cargar las UENs para el registro de Proyectos');                
    });
}

var Graficas = {

    IniciaGrafica_1: function (lst) {

        console.log(lst);

        var Nombres = [];
        for (var i = 0; i < lst.length; i++) {
            Nombres.push(lst[i].Rik_Nombre);
        }

        var DataSet1 = [];
        var DataSet2 = [];
        for (var i = 0; i < lst.length; i++) {
            DataSet1.push(lst[i].Integralidad);
            DataSet2.push(lst[i].Integralidad_Obs);
        }

        var ColorDs1 = [];
        var ColorDs2 = [];
        for (var i = 0; i < lst.length; i++) {
            ColorDs1.push('#38C0FF');
            ColorDs2.push('#FF5757');
        }
        var DataGrafica1 = {
            datasets: [
                {
                    label: 'Integralidad Teórico',
                    data: DataSet1, //[5, 10, 15],
                    backgroundColor: ColorDs1, //[COLOR_Analisis, COLOR_Promocion, COLOR_Negociacion],
                    borderColor: '#FF2D00',
                },
                {
                    label: 'Integralidad Observada',
                    data: DataSet2, //[10, 20, 30],
                    backgroundColor: ColorDs2, // [COLOR_Analisis, COLOR_Promocion, COLOR_Negociacion],
                    borderColor: '#88EC00',
                },
            ],
            labels: Nombres // ['A', 'B', 'C']
        };

        var ConfigGrafica1 = {
            //type: 'doughnut',
            type: 'bar',
            data: DataGrafica1,
            options: {
                responsive: true,
                legend: {
                    position: 'right',
                },
                title: {
                    display: true,
                    text: '% de Integralidad'
                },
                /*scale: {
                    ticks: {
                        beginAtZero: true
                    },
                    reverse: false
                },*/
                animation: {
                    animateRotate: false,
                    animateScale: true
                }
            }
        };

        //var ctx1 = document.getElementById('canvas1').getContext('2d');

        window.myBar = new Chart(ctx1, ConfigGrafica1);

    },

    // 19Jul2022 Update Se inverten valores
    IniciaGrafica_2: function (GraficaTitulos, Vals1, Vals2) {
        const DATA_COUNT = 3;
        var DataGrafica2 = {
            datasets: [
                {
                    label: 'Int. Val Teórico',
                    //data: [Vals1[2], Vals1[1], Vals1[0]],
                    data: [Vals1[0], Vals1[1], Vals1[2]],
                    backgroundColor: [COLOR_Analisis, COLOR_Promocion, COLOR_Negociacion],
                    borderColor: '#FF2D00',
                    backgroundColor: '',
                },
                {
                    label: 'Int. Val Observado',
                    //data: [Vals2[2], Vals2[1], Vals2[0]],
                    data: [Vals2[0], Vals2[1], Vals2[2]],
                    backgroundColor: [COLOR_Analisis, COLOR_Promocion, COLOR_Negociacion],
                    borderColor: '#88EC00',
                    backgroundColor: '',
                },
            ],
            labels: [GraficaTitulos[0], GraficaTitulos[1], GraficaTitulos[2]]
            //labels: [GraficaTitulos[2], GraficaTitulos[1], GraficaTitulos[0]]
        };

        var ConfigGrafica2 = {
            //type: 'doughnut',
            type: 'line',
            data: DataGrafica2,
            options: {
                responsive: true,
                legend: {
                    position: 'right',
                },
                title: {
                    display: true,
                    text: 'Integralidad'
                },
                /*scale: {
                    ticks: {
                        beginAtZero: true
                    },
                    reverse: false
                },*/
                animation: {
                    animateRotate: false,
                    animateScale: false
                }
            }
        };
        $('#DivCanvas2').empty();
        $('#DivCanvas2').append('<canvas id="canvas2" style="display: block; width: 100%; height: 563px;" width="1126" height="563" class="chartjs-render-monitor"></canvas>');
        ctx2 = document.getElementById('canvas2').getContext('2d');
        if (myPolarArea instanceof Chart) {
            myPolarArea.destroy();
            console.log('myPolarArea.destroy();');
        }
        var myPolarArea = new Chart(ctx2, ConfigGrafica2);
    }
}

function Inicializa_Grafica() {
    VALORES[0] = "0";
    VALORES[1] = "0";
    VALORES[2] = "0";
    Preparar_Grafica(function (response) { });
}

var CatRepresentante = {

    CargarCombo: function (CALLBACK_Exito) {

        //$('#spinner_GCIndice').css('display', 'block');
        //$('#btnCargarListado').attr('disabled', true);

        $.ajax({
            url: _ApplicationUrl + '/api/CrmRepresentante/Get_List?Id_Cd=0',
            cache: false,
            type: 'GET'
        }).done(function (response, textStatus, jqXHR) {
            Estado = response.Estado;
            listado = response.Datos;
            //console.log(response);        
            var ddlRep = $('#ddlRepresentante').empty();
            ddlRep.append(
                $('<option data-Id_U=0>').val(0).text('-- Todos --')
            );
            if (Estado == 1) {
                var ID = 0;
                for (var i = 0; i < listado.length; i++) {
                    ddlRep.append(
                        $('<option data-Id_U=' + listado[i].Id_U + ' data-IdRik=' + listado[i].Id_Rik + ' >').val(listado[i].Id_Rik).text(listado[i].U_Nombre)
                    );
                    ID = listado[i].Id_U;
                }

                hfId_Rik = parseInt(hfId_Rik);
                if (isNaN(hfId_Rik)) {
                    hfId_Rik = 0;
                }

                hfId_TU = parseInt(hfId_TU);
                if (isNaN(hfId_TU)) {
                    hfId_TU = 0;
                }
                if (hfId_TU == 3) {
                    // Si es gerente
                    ddlRep.removeAttr('disabled');
                } else {
                    ddlRep.val(hfId_Rik);
                    ddlRep.prop('disabled', 'disabled');
                }
                $('#spinner_GCIndice').css('display', 'none');
                $('#btnCargarListado').attr('disabled', false);
            }
            if (CALLBACK_Exito) {
                CALLBACK_Exito();
            }
        }).fail(function (jqXHR, textStatus, error) {
            //$('#spinner_GCIndice').css('display', 'none');
            //$('#btnCargarListado').attr('disabled', false);
            alertify.error('Error: CatRepresentante.Cargar_Representante');
            console.log(jqXHR);
        });
    },

    CargarListadoCheck: function (CALLBACK_Exito) {

        $.ajax({
            url: _ApplicationUrl + '/api/CrmRepresentante/Get_List?Id_Cd=0',
            cache: false,
            type: 'GET'
        }).done(function (response, textStatus, jqXHR) {
            Estado = response.Estado;
            listado = response.Datos;
            //console.log(response);        

            $('#tblRepresentantesCheck > tbody').empty();

            hfId_TU = parseInt(hfId_TU);
            if (isNaN(hfId_TU)) {
                hfId_TU = 0;
            }
            var CheckProDisabled = 'disabled';
            if (hfId_TU == 3) {
                // Si es gerente
                CheckProDisabled = '';
            } else {
                CheckProDisabled = 'disabled';
            }

            if (Estado == 1) {
                var ID = 0;
                for (var i = 0; i < listado.length; i++) {
                    var row = $('<tr>');
                    row.append($('<td class="text-center">').append(
                        '<input id="chbRespresentante_' + i + '" data-id_rik=' + listado[i].Id_Rik + ' type="checkbox" ' + CheckProDisabled + '/>'
                    ));
                    row.append($('<td class="text-left" >').append(
                        '<label>' + listado[i].U_Nombre + '</label>'
                    ));
                    $('#tblRepresentantesCheck > tbody').append(row);
                }
            }
            if (CALLBACK_Exito) {
                CALLBACK_Exito();
            }
        }).fail(function (jqXHR, textStatus, error) {
            alertify.error('Error: CatRepresentante.CargarListadoCheck');
            console.log(jqXHR);
        });
    }
}

function Cargar_Segmentos(Id_Rik, CALLBACK_Exito) {
    $('#spinner_Cargando').css('display', 'block');
    Id_Rik = parseInt(Id_Rik);
    if (isNaN(Id_Rik)) {
        Id_Rik = 0;
    }
    if (Id_Rik == 0) {
        var ddl = $('#ddlFiltroSegmentos').empty();
        ddl.append(
            $('<option data-Id_U=0>').val(0).text('-- Todos --')
        );
        if (CALLBACK_Exito) {
            CALLBACK_Exito();
            $('#spinner_Cargando').css('display', 'none');
        }
        $('#spinner_Cargando').css('display', 'none');
    } else {
        $.ajax({
            url: _ApplicationUrl + '/api/CatSegmento/Consultar_SegmentoByRik?Id_Cd=0&Id_Rik=' + Id_Rik,
            cache: false,
            type: 'GET',
            async: false,
        }).done(function (response, textStatus, jqXHR) {
            Estado = response.Estado;
            listado = response.Datos;
            var ddl = $('#ddlFiltroSegmentos').empty();
            ddl.append(
                $('<option data-Id_U=0>').val(0).text('-- Todos --')
            );
            if (Estado == 1) {
                var ID = 0;
                for (var i = 0; i < listado.length; i++) {
                    ddl.append(
                        $('<option data-Id_Seg=' + listado[i].Id_Seg + ' >').val(listado[i].Id_Seg).text(listado[i].Seg_Descripcion)
                    );
                    ID = listado[i].Id_U;
                }
                ddl.val(ID);
            }
            if (CALLBACK_Exito) {
                $('#spinner_Cargando').css('display', 'none');
                CALLBACK_Exito();
            }
        }).fail(function (jqXHR, textStatus, error) {
            $('#spinner_Cargando').css('display', 'none');
            alertify.error('Error: Cargar_Segmentos(327)');
            console.log(jqXHR);
        });
    }
}
/*aqui hay q ver como carga clientes*/
function Cargar_Cliente(Id_Rik, CALLBACK_Exito) {
    Id_Rik = parseInt(Id_Rik);
    if (isNaN(Id_Rik)) {
        Id_Rik = 0;
    }
    /*if (Id_Rik == 0) {
        var ddl = $('#ddlRazonSocialCtes').empty();
        ddl.append(
            $('<option data-Id_U=0>').val(0).text('-- Todos --')
        );
    } else {*/
        $.ajax({
            url: _ApplicationUrl + '/api/CatCliente/spCrmInt_Clientes?Id_Emp=0&Id_Cd=0&Id_Rik=' + Id_Rik,
            cache: false,
            type: 'GET',
            async: false,
        }).done(function (response, textStatus, jqXHR) {
            Estado = response.Estado;
            listado = response.Datos;

            var ddl = $('#ddlRazonSocialCtes').empty();
            ddl.append(
                $('<option data-Id_U=0>').val(0).text('-- Todos --')
            );
            if (Estado == 1) {
                for (var i = 0; i < listado.length; i++) {
                    ddl.append(
                        $('<option data-Id_Cte=' + listado[i].Id_Cte + ' >').val(listado[i].Id_Cte).text(listado[i].NomComercial)
                    );
                }
                ddl.val(0);
            }
            if (CALLBACK_Exito) {
                CALLBACK_Exito();
            }
        }).fail(function (jqXHR, textStatus, error) {

            alertify.error('Error: Cargar_Cliente');
            console.log(jqXHR);
        });

    /*}*/
}

var Integralidad = {

    CargarClientes() {
        var Id_Rik = $('#ddlRepresentante').val();
        $('#spinner_Cargando').css('display', 'block');
        Cargar_Segmentos(Id_Rik, function () {
            Cargar_Cliente(Id_Rik, function () {
                $("#ddlRazonSocialCtes").val(0);
                $('#spinner_Cargando').css('display', 'none');
            });
        });
    },
    actualizarVPOMeta: function() {
        var Id_Cte = $('#hf_Id_CteVPOMeta').val();
var Id_Ter = $('#dr_id_terVPOMeta').text();
var VPOMeta = $('#txtPotencialMeta').val();


$.ajax({
    url: _ApplicationUrl + '/api/CatIntegralidad/spCrmInt_Integralidad_ActualizaVPOMeta' +
        '?Id_Cte=' + Id_Cte + '&Id_Ter=' + Id_Ter + '&VPOMeta=' + VPOMeta + "&Par4=0",
    cache: false,
    type: 'GET'
}).done(function (response, textStatus, jqXHR) {
    Estado = response.Estado;
    //listado = response.Datos;

    if (Estado == 1) {
        alertify.success('Se actualizo correctamente');
        $('#modalIntegralidadVPOMeta').modal('hide');
        //$('#modalIntegralidadInicial').prop('disabled', true);
        //$('#modalIntegralidadInicial').modal('show');
        $('#modalIntegralidadInicial').trigger('focus')
        Integralidad.btnAplicar_Click();
    } else {
        alertify.error('Ocurrio un error: spCrmInt_Integralidad_ActualizaVPOMeta');
    }

}).fail(function (jqXHR, textStatus, error) {
    //$('#spinner_Cargando').css('display', 'none');
    alertify.error('Error:  spCrmInt_Integralidad_ActualizaVPOMeta');
    console.log(jqXHR);
});

},
    GenerarResultados: function () {

        $('#btnCompararRepresentantes').attr('disabled', true);

        $('#spinner_Mostrar').css('display', 'block');
        var RiksSeleccionados = '';

        for (var i = 0; i < 100; i++) {
            var chRrep = $('#chbRespresentante_' + i).is(':checked');
            if (typeof (chRrep) != 'undefined' && chRrep == true) {
                var Id_Rik = $('#chbRespresentante_' + i).data('id_rik');
                RiksSeleccionados = RiksSeleccionados + Id_Rik + ',';
            }
        }
        console.log('RiksSeleccionados:' + RiksSeleccionados + '<');
        if (RiksSeleccionados == '') {
            $('#spinner_Mostrar').css('display', 'none');
            $('#btnCompararRepresentantes').attr('disabled', false);
            alertify.success('No ha seleccionado representante');
            return;
        }

        $.ajax({
            url: _ApplicationUrl + '/api/CatIntegralidad/spCrmIntegralidad_PorRik' +
                '?RiksSeleccionados=' + RiksSeleccionados + '&param1=0',
            cache: false,
            type: 'GET'
        }).done(function (response, textStatus, jqXHR) {
            Estado = response.Estado;
            listado = response.Datos;

            if (Estado == 1) {
                //Integralidad.DespegarResultados(listado);
                Graficas.IniciaGrafica_1(listado);
                $('#spinner_Mostrar').css('display', 'none');
                $('#btnCompararRepresentantes').attr('disabled', false);
            } else {
                alertify.error('Ocurrio un error: spCrmInt_IntegralidadMes');
            }

        }).fail(function (jqXHR, textStatus, error) {
            $('#spinner_Mostrar').css('display', 'none');
            $('#btnCompararRepresentantes').attr('disabled', false);
            alertify.error('Error:  Integralidad.Consultar');
            console.log(jqXHR);
        });
    },

    ConsultaMes: function (Cal_Anio, Cal_Mes, CALLBACK_Exito) {

        $('#spinner_Cargando').css('display', 'block');

        var Id_Cte = $('#hf_Id_Cte').val();
        var Id_Rik = $('#hf_Id_Rik').val();
        var Id_Ter = $('#dr_id_ter').text();
        var Id_Seg = $('#hf_Id_Seg').val();

        $.ajax({
            url: _ApplicationUrl + '/api/CatIntegralidad/spCrmInt_MapaAplicaciones_Detalle_Ver2' +
                '?Id_Cte=' + Id_Cte + '&Id_Ter=' + Id_Ter + '&Id_Usu=' + Id_Rik + '&Id_Seg=' + Id_Seg + '&Cal_Anio=' + Cal_Anio + '&Cal_Mes=' + Cal_Mes,
            cache: false,
            type: 'GET'
        }).done(function (response, textStatus, jqXHR) {
            Estado = response.Estado;
            listado = response.Datos;

            if (Estado == 1) {
                if (CALLBACK_Exito) {
                    CALLBACK_Exito(listado);
                    $('#spinner_Cargando').css('display', 'none');
                }
            } else {
                alertify.error('Ocurrio un error: spCrmInt_IntegralidadMes');
            }

        }).fail(function (jqXHR, textStatus, error) {
            $('#spinner_Cargando').css('display', 'none');
            alertify.error('Error:  Integralidad.Consultar');
            console.log(jqXHR);
        });
    },

    // DESPLEGAR Detalle del MES

    DesplegarIntMes: function (RowNo, Lst, Id_Rik, Id_Cte, VPT) {
        var Id_Area = '';
        var Id_Sol = '';
        var Id_Op = '';
        var Id_Apl = '';
        var TitulosOp = 0;
        var tbl = $('#tblDetalle_' + RowNo).empty();

        for (var i = 0; i < Lst.length; i++) {
            // AREA
            if (Id_Area == Lst[i].Id_Area) {
            } else {
                var row = $('<tr>');
                row.append($('<td colspan="12" class="text-left">').append(
                    '<label>Area: ' + Lst[i].Id_Area + ' ' + Lst[i].Area_Descripcion + '</label>'
                ));
                tbl.append(row);
                Id_Area = Lst[i].Id_Area;
            }

            // SOLUCION 
            if (Id_Sol == Lst[i].Id_Sol) {
            } else {
                // Inserta Area
                var row = $('<tr>');
                row.append($('<td style="width:50px;" >').append('&nbsp;'));
                row.append($('<td class="text-left" colspan="11">').append(
                    '<label>Solución: ' + Lst[i].Id_Sol + ' ' + Lst[i].Sol_Descripcion + '</label>'
                ));
                tbl.append(row);
                Id_Sol = Lst[i].Id_Sol;
                TitulosOp = 1;
            }

            // ID_OP
            var Id_Op = Lst[i].Id_Op;
            Id_Op = parseInt(Id_Op);
            if (isNaN(Id_Op)) {
                Id_Op = 0;
            }

            if (TitulosOp == 1) {
                var row = $('<tr>');
                row.append($('<td>').append('&nbsp;'));
                row.append($('<td>').append('&nbsp;'));
                row.append($('<td style="width:100px; color:white; background-color:#0088d7;" class="text-center">').append('No. Proyecto'));
                row.append($('<td style="color:white; background-color:#0088d7;" class="text-center">').append('Aplicacion'));
                row.append($('<td style="width:80px; color:white; background-color:#0088d7;" class="text-center">').append('%'));
                row.append($('<td style="width:50px; color:white; background-color:#0088d7;" class="text-center">').append('VPT'));
                row.append($('<td style="width:50px; color:white; background-color:#0088d7;" class="text-center">').append('VPO'));
                row.append($('<td style="width:100px; color:white; background-color:#0088d7;" class="text-center">').append('VPO Meta'));
                row.append($('<td style="width:100px; color:white; background-color:#0088d7;" class="text-center">').append('Venta Real'));
                row.append($('<td style="width:100px; color:white; background-color:#0088d7;" class="text-center">').append('GAP Teórico'));
                row.append($('<td style="width:120px; color:white; background-color:#0088d7;" class="text-center">').append('GAP Observado'));
                row.append($('<td style="width:50px; color:white; background-color:#0088d7;" >').append('CRM'));
                tbl.append(row);
            }

            if (Id_Op == 0) {
                TitulosOp = 0;
            }

            // No. Proyecto
            var row = $('<tr>');

            var Venta_Real_f = parseFloat(Lst[i].Venta_Real);

            if (Venta_Real_f <= 0) {
                row.append($('<td colspan="2" class="text-center">').append('<img src="../Imagenes/salir.png">'));
            } else {
                row.append($('<td colspan="2" class="text-center">').append('<img src="../Imagenes/check.png">'));
            }



            var Id_Op = Lst[i].Id_Op;
            Id_Op = parseInt(Id_Op);
            if (isNaN(Id_Op)) {
                Id_Op = 0;
            }
            if (Id_Op > 0) {
                row.append($('<td class="text-center">').append('<label class="font-weight: bold;">' + Lst[i].Id_Op + '</label>'));
            } else {
                row.append($('<td class="text-center">').append(Id_Op));
            }

            // Aplicacion 
            row.append($('<td>').append(Lst[i].Apl_Descripcion));

            // %
            var Potencial = Lst[i].Apl_Potencial;
            Potencial = parseInt(Potencial);
            if (isNaN(Potencial)) {
                Potencial = 0;
            }
            row.append($('<td>').append(Potencial));

            // VPT
            var D = VPT * (Potencial / 100);
            D = parseFloat(D);
            var XVPT = D;
            if (isNaN(D)) {
                D = 0;
            }
            //var VPT= 0;
            //VPT = parseFloat(Lst[i].VPT);
            D = D.formatMoney(2, '.', ',');
            row.append($('<td class="text-center" >').append('$' + D));

            // VPO
            var VPO = 0;
            VPO = parseFloat(Lst[i].VPO);
            VPO = VPO.formatMoney(2, '.', ',');
            row.append($('<td class="text-center" >').append('$' + VPO));

            // VPOMeta
            var VPOMeta = 0;
            VPOMeta = parseFloat(Lst[i].VPOMeta);
            VPOMeta = VPOMeta.formatMoney(2, '.', ',');
            row.append($('<td class="text-center" >').append('$' + VPOMeta));

            // Venta Real 
            var Venta_Real = 0;
            Venta_Real = parseFloat(Lst[i].Venta_Real);
            Venta_Real = Venta_Real.formatMoney(2, '.', ',');

            if (Venta_Real_f <= 0) {
                row.append($('<td class="text-center" style="color:red!important;">').append('$' + Venta_Real));
            } else {
                row.append($('<td class="text-center" >').append('$' + Venta_Real));
            }
            // GAP Teorico
            var GAP_Teorico = 0;
            //GAP_Teorico = parseFloat(Lst[i].GAP_Teorico);
            GAP_Teorico = parseFloat(Lst[i].Venta_Real) - XVPT;
            GAP_Teorico = GAP_Teorico.formatMoney(2, '.', ',');
            row.append($('<td class="text-center" >').append('$' + GAP_Teorico));
            // GAP Observado
            var GAP_Observado = 0;
            //GAP_Observado = parseFloat(Lst[i].GAP_Observado);
            GAP_Observado = parseFloat(Lst[i].Venta_Real) - parseFloat(Lst[i].VPO);
            GAP_Observado = GAP_Observado.formatMoney(2, '.', ',');
            row.append($('<td class="text-center" >').append('$' + GAP_Observado));
            // CRM Link 
            if (Id_Op > 0) {
                row.append($('<td>').append());
            } else {
                row.append($('<td style="width:50px;" class="text-center">').append(
                    '<a tooltip="Nuevo proyecto" target="_blank" href="ProspectosV2.aspx?FlagIntegralidad=1&Id_Cte=' + Id_Cte + '&Id_Rik=' + Id_Rik + '&Id_Op=' + Lst[i].Id_Op + '&Id_Area=' + Lst[i].Id_Area + '&Id_Sol=' + Lst[i].Id_Sol + '">' +
                    '<i class="fa fa-file-o" aria-hidden="true"></i>' +
                    '</a > '
                ));
            }
            tbl.append(row);
            Id_Op = Lst[i].Id_Op;
        }



    },

    // Detalle del MES
    ExpandirInt: function (Obj) {
        var Mes = $(Obj).data('mes');
        var Cal_Mes = $(Obj).data('cal_mes');
        var Cal_Anio = $(Obj).data('cal_anio');
        var RowNo = $(Obj).data('rowno');
        var Id_Rik = $(Obj).data('id_rik');
        var Id_Cte = $(Obj).data('id_cte');
        var VPT = $(Obj).data('vpt');
        //var Expand = $(Obj).data('expand');
        var Expand = $('#hfDetalleFlag_' + RowNo).val();
        Expand = parseInt(Expand);
        if (isNaN(Expand)) {
            Expand = 0;
        }
        if (Expand == 0) {
            $('#DetalleRow_' + RowNo).css('display', '');
            $('#hfDetalleFlag_' + RowNo).val(1);
        } else {
            $('#DetalleRow_' + RowNo).css('display', 'none');
            $('#hfDetalleFlag_' + RowNo).val(0);
            $('#tblDetalle_' + RowNo).empty();
            return;
        }
        Integralidad.ConsultaMes(Cal_Anio, Cal_Mes, function (Lst) {
            Integralidad.DesplegarIntMes(RowNo, Lst, Id_Rik, Id_Cte, VPT);
        });
    },
    // Paso 3 Historial de Integralidad
    DesplegarInt: function (Lst, Id_Rik, Id_Cte) {
        console.log('DesplegarInt: ');
        console.log(Lst);

        var GranTotalVenta = 0;
        var GranTotalVPT_Total = 0;
        var GranTotalVPO_Total = 0;
        var GranTotalVPOMeta_Total = 0;
        var GranTotalVenta_Total = 0;
        var GranTotalGAP_Total = 0;
        var GranTotalGAP_Obs_Total = 0;
        var GranTotal_int_T = 0; //Se calcula
        var GranTotal_int_O = 0; //Se calcula
        var GranTotal_OpTeo = 0; //Se calcula
        var GranTotal_OpObs = 0; //Se calcula

        $('#tblHistorial > tbody').empty();

        $('#tblHistorialTotales > tbody').empty();

        var GraficaTitulos = ["A", "B", "C"];
        var GraficaValores1 = [0, 0, 0];
        var GraficaValores2 = [0, 0, 0];





        for (var i = 0; i < Lst.length; i++) {
            var VPO_Total = 0;
            var VPOMeta_Total = 0;
            var VPT_Total = 0;
            var Venta_Total = 0;
            var GAP_Total = 0;
            var GAP_Obs_Total = 0;

            // Segmentos 
            var Seg = Lst[i].Segmentos;
            for (var S = 0; S < Seg.length; S++) {
                var App = Seg[S];
                for (var A = 0; A < App.Aplicaciones.length; A++) {
                    VPO_Total = VPO_Total + App.Aplicaciones[A].VPO;
                    VPOMeta_Total = VPOMeta_Total + App.Aplicaciones[A].VPOMeta
                    VPT_Total = VPT_Total + App.Aplicaciones[A].VPT;
                    Venta_Total = Venta_Total + App.Aplicaciones[A].Venta;
                    GAP_Obs_Total = GAP_Obs_Total + App.Aplicaciones[A].GAP_Observado;
                    GAP_Total = GAP_Total + App.Aplicaciones[A].GAP_Teorico;
                }
            }

            var row = $('<tr id="trMes_' + i + '">');
            // COL 1
            row.append($('<td class="text-center" style="width:50px;" >').append(
                '<input type="hidden" id="hfDetalleFlag_' + i + '" value="0" />' +
                '<i id="PlusCircle_' + i + '" ' +
                'data-cal_anio="' + Lst[i].Anio + '" ' +
                'data-cal_mes="' + Lst[i].MesNum + '" ' +
                'data-id_rik="' + Id_Rik + '" ' +
                'data-id_cte="' + Id_Cte + '" ' +
                'data-vpt="' + VPT_Total + '" ' +
                'data-rowno="' + i + '" ' +
                'style="margin:5px:" ' +
                'onclick="Integralidad.ExpandirInt(this);"  ' +
                'class="fa fa-plus-circle" aria-hidden="true"></i>'
            ));

            // COL 2 MES
            row.append($('<td>').append(Lst[i].Mes + ' ' + Lst[i].Anio));

            GraficaTitulos[i] = Lst[i].Mes + ' de ' + Lst[i].Anio;

            // COL 3	Venta (Est. de venta)
            var Venta = 0;
            Venta = parseFloat(Lst[i].Venta);

            GranTotalVenta = GranTotalVenta + Venta;

            Venta = Venta.formatMoney2(2, '.', ',');
            row.append($('<td>').append(Venta));


            var fVPT_Total_V=0;
            // COL 4 VPT
            fVPT_Total_V = parseFloat(VPT_Total)
            VPT_Total = parseFloat(VPT_Total);

            GranTotalVPT_Total = GranTotalVPT_Total + VPT_Total;

            var fVPT_Total = parseFloat(VPT_Total);
            VPT_Total = VPT_Total.formatMoney2(2, '.', ',');
            row.append($('<td>').append(VPT_Total));


	    var VPO_Total_V =0;
            // COL 5 VPO
	    VPO_Total_V = parseFloat(VPO_Total);
            VPO_Total = parseFloat(VPO_Total);

            GranTotalVPO_Total = GranTotalVPO_Total + VPO_Total;

            var fVPO_Total = parseFloat(VPO_Total);
            VPO_Total = VPO_Total.formatMoney2(2, '.', ',');
            row.append($('<td>').append(VPO_Total));



            var VPOMeta_Total_V = 0;
            // COL 5 VPO Meta
            VPOMeta_Total_V = parseFloat(VPOMeta_Total);
            VPOMeta_Total = parseFloat(VPOMeta_Total);

            GranTotalVPOMeta_Total = GranTotalVPOMeta_Total + VPOMeta_Total;

            var fVPOMeta_Total = parseFloat(VPOMeta_Total);
            VPOMeta_Total = VPOMeta_Total.formatMoney2(2, '.', ',');
            row.append($('<td>').append(VPOMeta_Total));




            // COL 6 Venta (Aplicacion-Proyecto)
            var fVenta_Total = 0;
            Venta_Total = parseFloat(Venta_Total);
            fVenta_Total = parseFloat(Venta_Total);

            GranTotalVenta_Total = GranTotalVenta_Total + parseFloat(Venta_Total);

            Venta_Total = Venta_Total.formatMoney2(2, '.', ',');
            row.append($('<td>').append(Venta_Total));



	    var GAP_Total_V=0;
            // COL 7 GAP vs Teórico int_T

	    GAP_Total_V = parseFloat(GAP_Total);
            GAP_Total = parseFloat(GAP_Total);

        

            if (isNaN(GAP_Total)) {
                GAP_Total = 0;
            }

            GranTotalGAP_Total = GranTotalGAP_Total + GAP_Total;

            if (GAP_Total > 0) {
                GAP_Total = GAP_Total.formatMoney2(2, '.', ',');
                //row.append($('<td>').append('$' + GAP_Total));
                row.append($('<td>').append('<label style="color:green!important;">' + GAP_Total + '</label>'));
            } else {
                GAP_Total = GAP_Total.formatMoney2(2, '.', ',');
                //row.append($('<td>').append('$' + GAP_Total));
                row.append($('<td>').append('<label style="color:red!important;">' + GAP_Total + '</label>'));
            }

            // COL 8 GAP vs Observado
            //GAP_Obs_Total = parseFloat(GAP_Obs_Total);
            //GAP_Obs_Total = GAP_Obs_Total.formatMoney(2, '.', ',');
            //row.append($('<td>').append('$' + GAP_Obs_Total));

	    var GAP_Obs_Total_V=0;

	    GAP_Obs_Total_V = parseFloat(GAP_Obs_Total);
            GAP_Obs_Total = parseFloat(GAP_Obs_Total);
            if (isNaN(GAP_Obs_Total)) {
                GAP_Obs_Total = 0;
            }

            GranTotalGAP_Obs_Total = GranTotalGAP_Obs_Total + GAP_Obs_Total;
            if (GAP_Obs_Total < 0) {
                GAP_Obs_Total = GAP_Obs_Total.formatMoney2(2, '.', ',');
                row.append($('<td class="text-center;">').append(
                    '<label style="color:red!important;">' + GAP_Obs_Total + '</label>')

                );
            } else {
                GAP_Obs_Total = GAP_Obs_Total.formatMoney2(2, '.', ',');
                row.append($('<td class="text-center;">').append(
                    '<label style="color:green!important;">' + GAP_Obs_Total + '</label>'
                )
                );
            }

            // COL 9 Integralidad Vs Teorico
            var int_T = 0;
            if (fVPT_Total <= 0) {
                int_T = 0;
            } else {
                // 30Ago2022 - RFH 
                int_T = (fVenta_Total / fVPT_Total) * 100;
                //int_T = (fVPT_Total / fVenta_Total) * 100;
                int_T = parseFloat(int_T);
            }
            if (isNaN(int_T)) {
                int_T = 0;
            }


            //int_T = int_T.formatMoney(2, '.', ',');
            //row.append($('<td>').append(int_T + '%'));





            if (int_T <= 60) { // 0-60
                let _int_T = int_T.formatMoney(2, '.', ',');
                row.append($('<td class="text-center" style="background-color: red!important;">').append(
                    '<label style="color:white!important;">' + _int_T + '%</label>'
                ));
            }
            if (int_T > 60 && int_T <= 79) { // 60-79
                let _int_T = int_T.formatMoney(2, '.', ',');
                row.append($('<td class="text-center" style="background-color: yellow!important;">').append(
                    '<label style="color:black!important;">' + _int_T + '%</label>'
                ));
            }
            if (int_T > 79) { // >=80
                let _int_T = int_T.formatMoney(2, '.', ',');
                row.append($('<td class="text-center" style="background-color: green!important;">').append(
                    '<label style="color:white!important;">' + _int_T + '%</label>'
                ));
            }

            // COL 10 Integralidad Vs Obs
            var int_O = 0;
            int_O = (fVenta_Total / fVPO_Total) * 100;
            int_O = parseFloat(int_O);
            if (isNaN(int_O)) {
                int_O = 0;
            }
            //int_O = int_O.formatMoney(2, '.', ',');
            //row.append($('<td>').append(int_O + '%'));



            if (int_O <= 60) { // 0-60
                int_O = int_O.formatMoney(2, '.', ',');
                row.append($('<td class="text-center" style="background-color: red!important;">').append(
                    '<label style="color:white!important;">' + int_O + '%</label>'
                ));
            }
            if (int_O > 60 && int_O <= 79) { // 60-79
                int_O = int_O.formatMoney(2, '.', ',');
                row.append($('<td class="text-center" style="background-color: yellow!important;">').append(
                    '<label style="color:black!important;">' + int_O + '%</label>'
                ));
            }
            if (int_O > 79) { // >=80
                // int_O = int_T.formatMoney(2, '.', ',');
                int_O = int_O.formatMoney(2, '.', ',');
                row.append($('<td class="text-center" style="background-color: green!important;">').append(
                    '<label style="color:white!important;">' + int_O + '%</label>'
                ));
            }

            // COL 11 - Oportundad Vs Teorico 
            //int_T = (fVPT_Total / fVenta_Total) * 100;
            //var OpTeo = (int_T - 100)

            //if (fVenta_Total > 0) { antes
	   

            /*
            if (fVPT_Total_V > 0) { 
                //var OpTeo = (fVPT_Total / fVenta_Total) * 100; antes
                var OpTeo = (1 - (GAP_Total_V  / fVPT_Total_V)) * 100;
                //GAP_Teorico
                OpTeo = parseFloat(OpTeo);
            } else {
                OpTeo = 0;
            }
            */

            OpTeo = int_T - 100;

            if (isNaN(OpTeo)) {
                OpTeo = 0;
            }
            

            

            //OpTeo = OpTeo.formatMoney(2, '.', ',');
            //row.append($('<td>').append(OpTeo + '%'));
            if (OpTeo > 0) {
                OpTeo = OpTeo.formatMoney(2, '.', ',');
                row.append($('<td class="text-center">').append(
                    '<label data-col="11" style="color:green!important;">' + OpTeo + '%</label>'
                ));
            } else {
                OpTeo = OpTeo.formatMoney(2, '.', ',');
                row.append($('<td class="text-center">').append(
                    '<label data-col="11" style="color:red!important;">' + OpTeo + '%</label>'
                ));
            }

            // Oportundad Vs Obs

	    var OpObs = 0;

            /*
            if (VPO_Total_V > 0) { 
                var OpObs  = (1 - (GAP_Obs_Total_V  / VPO_Total_V)) * 100;
                OpObs = parseFloat(OpObs);
            } else {
                OpObs = 0;
            }
            */



            //var OpObs = (int_O - 100);
            //OpObs = parseFloat(OpObs);

            OpObs = 100 - int_O;

            if (isNaN(OpObs)) {
                OpObs = 0;
            }



            //OpObs = OpObs.formatMoney(2, '.', ',');
            //row.append($('<td>').append(OpObs + '%'));



            if (OpObs > 0) {
                OpObs = OpObs.formatMoney(2, '.', ',');
                row.append($('<td class="text-center">').append(
                    '<label data-col="12" style="color:red!important;">' + OpObs + '%</label>'
                ));

            } else {
                OpObs = OpObs.formatMoney(2, '.', ',');
                row.append($('<td class="text-center">').append(
                    '<label data-col="12" style="color:green!important;">' + OpObs + '%</label>'
                ));

            }

            $('#tblHistorial > tbody').append(row);

            GraficaValores1[i] = int_T;
            GraficaValores2[i] = int_O;

            // LINEA DETALLE            
            var row = $('<tr id="DetalleRow_' + i + '">');

            // ROW Vacio Oculto
            row.append($('<td style="width:50px;" colspan="12">').append(
                '<table class="table table-hover table-bordered" id="tblDetalle_' + i + '" style="width:100%;">' +
                '<tbody></tbody>' +
                '</table>'
            ));

            $('#tblHistorial > tbody').append(row);
            $('#DetalleRow_' + i).css('display', 'none');
        }
        var rowTotales = $('<tr id="TotalesHistorial">');
        rowTotales.append($('<td>').append("Totales Promedio"));

        GranTotalVenta = GranTotalVenta / 3;
        GranTotalVenta = GranTotalVenta.formatMoney2(2, '.', ',');
        rowTotales.append($('<td>').append(GranTotalVenta));

        GranTotalVPT_Total = GranTotalVPT_Total / 3;

        var GranTotalVPT_Total_f = GranTotalVPT_Total;
        GranTotalVPT_Total = GranTotalVPT_Total.formatMoney2(2, '.', ',');
        rowTotales.append($('<td>').append(GranTotalVPT_Total));

        GranTotalVPO_Total = GranTotalVPO_Total / 3;
        var GranTotalVPO_Total_f = GranTotalVPO_Total;
        GranTotalVPO_Total = GranTotalVPO_Total.formatMoney2(2, '.', ',');
        rowTotales.append($('<td>').append(GranTotalVPO_Total));

        GranTotalVPOMeta_Total = GranTotalVPOMeta_Total / 3;
        var GranTotalVPOMeta_Total_f = GranTotalVPOMeta_Total;
        GranTotalVPOMeta_Total = GranTotalVPOMeta_Total.formatMoney2(2, '.', ',');
        rowTotales.append($('<td>').append(GranTotalVPOMeta_Total));

        GranTotalVenta_Total = GranTotalVenta_Total / 3;
        var GranTotalVenta_Total_f = GranTotalVenta_Total;
        GranTotalVenta_Total = GranTotalVenta_Total.formatMoney2(2, '.', ',');
        rowTotales.append($('<td>').append(GranTotalVenta_Total));

        GranTotalGAP_Total = GranTotalGAP_Total / 3;

        var GranTotalGAP_Total_f = GranTotalGAP_Total;

        if (GranTotalGAP_Total > 0) {
            GranTotalGAP_Total = GranTotalGAP_Total.formatMoney2(2, '.', ',');
            rowTotales.append($('<td>').append('<label style="color:green!important;">' + GranTotalGAP_Total + '</label>'));
        } else {
            GranTotalGAP_Total = GranTotalGAP_Total.formatMoney2(2, '.', ',');
            rowTotales.append($('<td>').append('<label style="color:red!important;">' + GranTotalGAP_Total + '</label>'));
        }

        GranTotalGAP_Obs_Total = GranTotalGAP_Obs_Total / 3;
        var GranTotalGAP_Obs_Total_f = GranTotalGAP_Obs_Total;
        if (GranTotalGAP_Obs_Total < 0) {
            GranTotalGAP_Obs_Total = GranTotalGAP_Obs_Total.formatMoney2(2, '.', ',');
            rowTotales.append($('<td class="text-center;">').append(
                '<label style="color:red!important;">' + GranTotalGAP_Obs_Total + '</label>')

            );
        } else {
            GranTotalGAP_Obs_Total = GranTotalGAP_Obs_Total.formatMoney2(2, '.', ',');
            rowTotales.append($('<td class="text-center;">').append(
                '<label style="color:green!important;">' + GranTotalGAP_Obs_Total + '</label>'
            )
            );
        }



        var int_T = 0;
        if (GranTotalVPT_Total_f <= 0) {   
            int_T = 0;
        } else {
            // 30Ago2022 - RFH 
            int_T = (GranTotalVenta_Total_f / GranTotalVPT_Total_f) * 100;
            //int_T = (fVPT_Total / fVenta_Total) * 100;
            int_T = parseFloat(int_T);
        }
        if (isNaN(int_T)) {
            int_T = 0;
        }


        //int_T = int_T.formatMoney(2, '.', ',');
        //row.append($('<td>').append(int_T + '%'));





        if (int_T <= 60) { // 0-60
            let _int_T = int_T.formatMoney(2, '.', ',');
            rowTotales.append($('<td class="text-center" style="background-color: red!important;">').append(
                '<label style="color:white!important;">' + _int_T + '%</label>'
            ));
        }
        if (int_T > 60 && int_T <= 79) { // 60-79
            let _int_T = int_T.formatMoney(2, '.', ',');
            rowTotales.append($('<td class="text-center" style="background-color: yellow!important;">').append(
                '<label style="color:black!important;">' + _int_T + '%</label>'
            ));
        }
        if (int_T > 79) { // >=80
            let _int_T = int_T.formatMoney(2, '.', ',');
            rowTotales.append($('<td class="text-center" style="background-color: green!important;">').append(
                '<label style="color:white!important;">' + _int_T + '%</label>'
            ));
        }


        var int_O = 0;   ////caca
        int_O = (GranTotalVenta_Total_f / GranTotalVPO_Total_f) * 100; 
        int_O = parseFloat(int_O);
        if (isNaN(int_O)) {
            int_O = 0;
        }
        //int_O = int_O.formatMoney(2, '.', ',');
        //row.append($('<td>').append(int_O + '%'));



        if (int_O <= 60) { // 0-60
            int_O = int_O.formatMoney(2, '.', ',');
            rowTotales.append($('<td class="text-center" style="background-color: red!important;">').append(
                '<label style="color:white!important;">' + int_O + '%</label>'
            ));
        }
        if (int_O > 60 && int_O <= 79) { // 60-79
            int_O = int_O.formatMoney(2, '.', ',');
            rowTotales.append($('<td class="text-center" style="background-color: yellow!important;">').append(
                '<label style="color:black!important;">' + int_O + '%</label>'
            ));
        }
        if (int_O > 79) { // >=80
            // int_O = int_T.formatMoney(2, '.', ',');
            int_O = int_O.formatMoney(2, '.', ',');
            rowTotales.append($('<td class="text-center" style="background-color: green!important;">').append(
                '<label style="color:white!important;">' + int_O + '%</label>'
            ));
        }

        /*
        if (GranTotalVPT_Total_f > 0) {    
            var OpTeo = (1 - (GranTotalGAP_Total_f / GranTotalVPT_Total_f)) * 100;
            OpTeo = parseFloat(OpTeo);
        } else {
            OpTeo = 0;
        }*/



        OpTeo = int_T - 100;

        if (isNaN(OpTeo)) {
            OpTeo = 0;
        }

        //OpTeo = OpTeo.formatMoney(2, '.', ',');
        //row.append($('<td>').append(OpTeo + '%'));
        if (OpTeo > 0) {
            OpTeo = OpTeo.formatMoney(2, '.', ',');
            rowTotales.append($('<td class="text-center">').append(
                '<label data-col="11" style="color:green!important;">' + OpTeo + '%</label>'
            ));
        } else {
            OpTeo = OpTeo.formatMoney(2, '.', ',');
            rowTotales.append($('<td class="text-center">').append(
                '<label data-col="11" style="color:red!important;">' + OpTeo + '%</label>'
            ));
        }


        var OpObs = 0;

        /*
        if (GranTotalVPO_Total_f > 0) {   
            var OpObs = (1 - (GranTotalGAP_Obs_Total_f / GranTotalVPO_Total_f)) * 100;
            OpObs = parseFloat(OpObs);
        } else {
            OpObs = 0;
        }*/


        //var OpObs = (int_O - 100);
        //OpObs = parseFloat(OpObs);

        OpObs = 100 - int_O;

        if (isNaN(OpObs)) {
            OpObs = 0;
        }



        //OpObs = OpObs.formatMoney(2, '.', ',');
        //row.append($('<td>').append(OpObs + '%'));



        if (OpObs > 0) {
            OpObs = OpObs.formatMoney(2, '.', ',');
            rowTotales.append($('<td class="text-center">').append(
                '<label data-col="12" style="color:red!important;">' + OpObs + '%</label>'
            ));

        } else {

            OpObs = OpObs.formatMoney(2, '.', ',');
            rowTotales.append($('<td class="text-center">').append(
                '<label data-col="12" style="color:green!important;">' + OpObs + '%</label>'
            ));
        }

        //var GranTotal_OpObs = 0; //Se calcula
        $('#tblHistorialTotales > tbody').append(rowTotales);

        Graficas.IniciaGrafica_2(GraficaTitulos, GraficaValores1, GraficaValores2);

    },
    // DETALLE
    // Paso 2
    spCrmInt_IntegralidadMes: function (Id_Cte, Id_Rik, Id_Ter, Id_Seg, CALLBACK_Exito, CALLBACK_Error) {
        $('#spinner_Cargando').css('display', 'block');
        $.ajax({
            url: _ApplicationUrl + '/api/CatIntegralidad/spCrmInt_IntegralidadMes_Ver2?Id_Cte=' + Id_Cte + '&Id_Rik=' + Id_Rik + '&Id_Ter=' + Id_Ter + '&Id_Seg=' + Id_Seg + '&Par4=0',
            cache: false,
            type: 'GET'
        }).done(function (response, textStatus, jqXHR) {
            Estado = response.Estado;
            listado = response.Datos;
            if (Estado == 1) {
                if (CALLBACK_Exito) {
                    CALLBACK_Exito(listado, Id_Rik);
                    $('#spinner_Cargando').css('display', 'none');
                }
            } else {
                alertify.error('Ocurrio un error: spCrmInt_IntegralidadMes');
            }
        }).fail(function (jqXHR, textStatus, error) {
            if (CALLBACK_Error) {
                CALLBACK_Error();
            }
            $('#spinner_Cargando').css('display', 'none');
            alertify.error('Error: Integralidad.spCrmInt_IntegralidadMes');
            console.log(jqXHR);
        });
    },
    // Paso 1
    DesplegarEditarVPOMeta: function (obj) {
        var id_rik = $(obj).data('id_rik');
        var reptesentante = $(obj).data('reptesentante');
        var id_cte = $(obj).data('id_cte');
        var cliente = $(obj).data('cliente');
        var id_ter = $(obj).data('id_ter');
        var id_seg = $(obj).data('id_seg');
        var segmento = $(obj).data('segmento');
        var cantidad = $(obj).data('cantidad');
        var seg_unidades = $(obj).data('seg_unidades');
        var seg_valunidim = $(obj).data('seg_valunidim');
        var VPOMeta = $(obj).data('dvpometa');



        $('#modalIntegralidadVPOMeta').appendTo("body").modal('show');
        $('#dr_repVPOMeta').text(reptesentante);
        $('#dr_clienteVPOMeta').text(id_cte + ' - ' + cliente);
        $('#dr_id_terVPOMeta').text(id_ter);
        $('#dr_segmentoVPOMeta').text(segmento);
        $('#dr_val_stdVPOMeta').text(seg_valunidim);
        $('#dr_cantidadVPOMeta').text(cantidad);
        $('#dr_seg_unidadesVPOMeta').text(seg_unidades);
        $('#hf_Id_CteVPOMeta').val(id_cte);
        $('#hf_Id_RikVPOMeta').val(id_rik);
        $('#hf_Id_SegVPOMeta').val(id_seg);
        $('#txtPotencialMeta').val(VPOMeta);

    },
    // Paso 1
    DesplegarDetalle: function (obj) {
        var id_rik = $(obj).data('id_rik');
        var reptesentante = $(obj).data('reptesentante');
        var id_cte = $(obj).data('id_cte');
        var cliente = $(obj).data('cliente');
        var id_ter = $(obj).data('id_ter');
        var id_seg = $(obj).data('id_seg');
        var segmento = $(obj).data('segmento');
        var cantidad = $(obj).data('cantidad');
        var seg_unidades = $(obj).data('seg_unidades');
        var seg_valunidim = $(obj).data('seg_valunidim');

        $('#tblHistorial > tbody').empty();
        $('#modalIntegralidad').appendTo("body").modal('show');
        $('#spinner_HI').css("display", 'block');
        $('#dr_rep').text(reptesentante);
        $('#dr_cliente').text(id_cte + ' - ' + cliente);
        $('#dr_id_ter').text(id_ter);
        $('#dr_segmento').text(segmento);
        $('#dr_val_std').text(seg_valunidim);
        $('#dr_cantidad').text(cantidad);
        $('#dr_seg_unidades').text(seg_unidades);
        $('#hf_Id_Cte').val(id_cte);
        $('#hf_Id_Rik').val(id_rik);
        $('#hf_Id_Seg').val(id_seg);

        Integralidad.spCrmInt_IntegralidadMes(id_cte, id_rik, id_ter, id_seg, function (Lst) {
            console.log(Lst);
            Integralidad.DesplegarInt(Lst, id_rik, id_cte);
            $('#spinner_HI').css("display", 'none');
        }, function () {
            //Error
            $('#spinner_HI').css("display", 'none');
        });

    },

    Consultar: function (Riks, Segs, Ctes, CALLBACK_Exito, CALLBACK_Error) {

        //$('#spinner_Cargando').css('display', 'block');

        $.ajax({
            //url: _ApplicationUrl + '/api/CatIntegralidad/spCrmInt_IntegralidadMes_Ver2?' +
            url: _ApplicationUrl + '/api/CatIntegralidad/spCrmInt_IntegralidadMes_Por_RIK_Listado_Ver2?' +
                'Riks=' + Riks + '&Ctes=' + Ctes + '&Segs=' + Segs + '&Opcion2=0',
            cache: false,
            type: 'GET',
            async: true,
        }).done(function (response, textStatus, jqXHR) {
            Estado = response.Estado;
            listado = response.Datos;
            if (Estado == 1) {
                if (CALLBACK_Exito) {
                    CALLBACK_Exito(listado);
                    //$('#spinner_Cargando').css('display', 'none');
                }
            } else {
                //$('#spinner_Cargando').css('display', 'none');
                alertify.error('Error:  Integralidad.Consultar(992)');
                if (CALLBACK_Error) {
                    CALLBACK_Error();
                }
            }
        }).fail(function (jqXHR, textStatus, error) {
            //$('#spinner_Cargando').css('display', 'none');
            alertify.error('Error:  Integralidad.Consultar(997)');
            console.log(jqXHR);
            if (CALLBACK_Error) {
                CALLBACK_Error();
            }
        });
    },
    // CLICK en Columna
    btnColumna_Click: function (Obj) {
        let Col = $(Obj).data("col_id"); // Columna
        let Dir = $(Obj).attr("data-dir"); // Direccion de Ordenado
        Dir = parseInt(Dir);
        if (isNaN(Dir)) {
            Dir = 0;
        }
        if (typeof (DatosConsulta) == 'undefined') {
            alertify.alert('Debe relizar la consulta de integralidad.');
        } else {

            switch (Col) {
                case 1: // No Cliente
                    switch (Dir) {
                        case 0:
                            DatosConsulta = DatosConsulta.sort(function (a, b) {
                                if (a.Id_Cte > b.Id_Cte) {
                                    return 1;
                                }
                                if (a.Id_Cte < b.Id_Cte) {
                                    return -1;
                                }
                                return 0;
                            });
                            $('#ColumnaOrden_' + Col).attr('data-dir', 1);
                            break;
                        case 1:
                            DatosConsulta = DatosConsulta.sort(function (a, b) {
                                if (a.Id_Cte < b.Id_Cte) {
                                    return 1;
                                }
                                if (a.Id_Cte > b.Id_Cte) {
                                    return -1;
                                }
                                return 0;
                            });
                            $('#ColumnaOrden_' + Col).attr('data-dir', 0);
                            break;
                    }
                    break;
                case 2:// Cliente (Nombre)
                    switch (Dir) {
                        case 0:
                            DatosConsulta = DatosConsulta.sort(function (a, b) {
                                if (a.Cte_NomComercial > b.Cte_NomComercial) {
                                    return 1;
                                }
                                if (a.Cte_NomComercial < b.Cte_NomComercial) {
                                    return -1;
                                }
                                return 0;
                            });
                            $('#ColumnaOrden_' + Col).attr('data-dir', 1);
                            break;
                        case 1:
                            DatosConsulta = DatosConsulta.sort(function (a, b) {
                                if (a.Cte_NomComercial < b.Cte_NomComercial) {
                                    return 1;
                                }
                                if (a.Cte_NomComercial > b.Cte_NomComercial) {
                                    return -1;
                                }
                                return 0;
                            });
                            $('#ColumnaOrden_' + Col).attr('data-dir', 0);
                            break;
                    }
                    break;
                case 3:// Territorio
                    switch (Dir) {
                        case 0:
                            DatosConsulta = DatosConsulta.sort(function (a, b) {
                                if (a.Id_Ter > b.Id_Ter) {
                                    return 1;
                                }
                                if (a.Id_Ter < b.Id_Ter) {
                                    return -1;
                                }
                                return 0;
                            });
                            $('#ColumnaOrden_' + Col).attr('data-dir', 1);
                            break;
                        case 1:
                            DatosConsulta = DatosConsulta.sort(function (a, b) {
                                if (a.Id_Ter < b.Id_Ter) {
                                    return 1;
                                }
                                if (a.Id_Ter > b.Id_Ter) {
                                    return -1;
                                }
                                return 0;
                            });
                            $('#ColumnaOrden_' + Col).attr('data-dir', 0);
                            break;
                    }
                    break;
                case 4:// Segmento
                    switch (Dir) {
                        case 0:
                            DatosConsulta = DatosConsulta.sort(function (a, b) {
                                if (a.Seg_Descripcion > b.Seg_Descripcion) {
                                    return 1;
                                }
                                if (a.Seg_Descripcion < b.Seg_Descripcion) {
                                    return -1;
                                }
                                return 0;
                            });
                            $('#ColumnaOrden_' + Col).attr('data-dir', 1);
                            break;
                        case 1:
                            DatosConsulta = DatosConsulta.sort(function (a, b) {
                                if (a.Seg_Descripcion < b.Seg_Descripcion) {
                                    return 1;
                                }
                                if (a.Seg_Descripcion > b.Seg_Descripcion) {
                                    return -1;
                                }
                                return 0;
                            });
                            $('#ColumnaOrden_' + Col).attr('data-dir', 0);
                            break;
                    }
                    break;
                case 5:// Cantidad
                    switch (Dir) {
                        case 0:
                            DatosConsulta = DatosConsulta.sort(function (a, b) {
                                if (a.Cantidad > b.Cantidad) {
                                    return 1;
                                }
                                if (a.Cantidad < b.Cantidad) {
                                    return -1;
                                }
                                return 0;
                            });
                            $('#ColumnaOrden_' + Col).attr('data-dir', 1);
                            break;
                        case 1:
                            DatosConsulta = DatosConsulta.sort(function (a, b) {
                                if (a.Cantidad < b.Cantidad) {
                                    return 1;
                                }
                                if (a.Cantidad > b.Cantidad) {
                                    return -1;
                                }
                                return 0;
                            });
                            $('#ColumnaOrden_' + Col).attr('data-dir', 0);
                            break;
                    }
                    break;
                case 6:// Unidad

                    break;

                case 61:// Prmedio Trim
                    switch (Dir) {
                        case 0:
                            DatosConsulta = DatosConsulta.sort(function (a, b) {
                                if (a.VPO > b.VPO) {
                                    return 1;
                                }
                                if (a.VPO < b.VPO) {
                                    return -1;
                                }
                                return 0;
                            });
                            $('#ColumnaOrden_' + Col).attr('data-dir', 1);
                            break;
                        case 1:
                            DatosConsulta = DatosConsulta.sort(function (a, b) {
                                if (a.VPO < b.VPO) {
                                    return 1;
                                }
                                if (a.VPO > b.VPO) {
                                    return -1;
                                }
                                return 0;
                            });
                            $('#ColumnaOrden_' + Col).attr('data-dir', 0);
                            break;
                    }
                    break;
                case 62:// Prmedio Trim
                    switch (Dir) {
                        case 0:
                            DatosConsulta = DatosConsulta.sort(function (a, b) {
                                if (a.VPOMeta > b.VPOMeta) {
                                    return 1;
                                }
                                if (a.VPOMeta < b.VPOMeta) {
                                    return -1;
                                }
                                return 0;
                            });
                            $('#ColumnaOrden_' + Col).attr('data-dir', 1);
                            break;
                        case 1:
                            DatosConsulta = DatosConsulta.sort(function (a, b) {
                                if (a.VPOMeta < b.VPOMeta) {
                                    return 1;
                                }
                                if (a.VPOMeta > b.VPOMeta) {
                                    return -1;
                                }
                                return 0;
                            });
                            $('#ColumnaOrden_' + Col).attr('data-dir', 0);
                            break;
                    }
                    break;
                case 7:// Prmedio Trim
                    switch (Dir) {
                        case 0:
                            DatosConsulta = DatosConsulta.sort(function (a, b) {
                                if (a.PromedioTrimestral > b.PromedioTrimestral) {
                                    return 1;
                                }
                                if (a.PromedioTrimestral < b.PromedioTrimestral) {
                                    return -1;
                                }
                                return 0;
                            });
                            $('#ColumnaOrden_' + Col).attr('data-dir', 1);
                            break;
                        case 1:
                            DatosConsulta = DatosConsulta.sort(function (a, b) {
                                if (a.PromedioTrimestral < b.PromedioTrimestral) {
                                    return 1;
                                }
                                if (a.PromedioTrimestral > b.PromedioTrimestral) {
                                    return -1;
                                }
                                return 0;
                            });
                            $('#ColumnaOrden_' + Col).attr('data-dir', 0);
                            break;
                    }
                    break;
                case 8:// Integ vs teo
                    switch (Dir) {
                        case 0:
                            DatosConsulta = DatosConsulta.sort(function (a, b) {
                                if (a.Integralidad > b.Integralidad) {
                                    return 1;
                                }
                                if (a.Integralidad < b.Integralidad) {
                                    return -1;
                                }
                                return 0;
                            });
                            $('#ColumnaOrden_' + Col).attr('data-dir', 1);
                            break;
                        case 1:
                            DatosConsulta = DatosConsulta.sort(function (a, b) {
                                if (a.Integralidad < b.Integralidad) {
                                    return 1;
                                }
                                if (a.Integralidad > b.Integralidad) {
                                    return -1;
                                }
                                return 0;
                            });
                            $('#ColumnaOrden_' + Col).attr('data-dir', 0);
                            break;
                    }
                    break;
                case 9:// Integ vs Obs
                    switch (Dir) {
                        case 0:
                            DatosConsulta = DatosConsulta.sort(function (a, b) {
                                if (a.Integralidad_Obs > b.Integralidad_Obs) {
                                    return 1;
                                }
                                if (a.Integralidad_Obs < b.Integralidad_Obs) {
                                    return -1;
                                }
                                return 0;
                            });
                            $('#ColumnaOrden_' + Col).attr('data-dir', 1);
                            break;
                        case 1:
                            DatosConsulta = DatosConsulta.sort(function (a, b) {
                                if (a.Integralidad_Obs < b.Integralidad_Obs) {
                                    return 1;
                                }
                                if (a.Integralidad_Obs > b.Integralidad_Obs) {
                                    return -1;
                                }
                                return 0;
                            });
                            $('#ColumnaOrden_' + Col).attr('data-dir', 0);
                            break;
                    }
                    break;
            }
            $('#tblIntegralidad > tbody').empty();
            Integralidad.Desplegar(DatosConsulta);
        }
    },

    // Click en Boton Aplicar en Pestalla de Intergalidad 
    btnAplicar_Click: function () {
        $('#spinner_Cargando').css('display', 'block');
        let Rik = $('#ddlRepresentante').val();
        let Seg = $('#ddlFiltroSegmentos').val();
        let Ctes = $('#ddlRazonSocialCtes').val();
        Integralidad.Consultar(Rik, Seg, Ctes, function (Lst) {
            DatosConsulta = Lst;
            $('#tblIntegralidad > tbody').empty();
            Integralidad.Desplegar(Lst);
            $('#spinner_Cargando').css('display', 'none');
        }, function () {
            $('#spinner_Cargando').css('display', 'none');
        });
    },

    // Click en Boton Aplicar en Pestalla de Intergalidad 
    btnBajarReporteExcel_Click: function () {
        let Rik = $('#ddlRepresentante').val();
        let lbRik = $('#ddlRepresentante option:selected').text();
        let Seg = $('#ddlFiltroSegmentos').val();
        let lbSeg = $('#ddlFiltroSegmentos option:selected').text();
        let Ctes = $('#ddlRazonSocialCtes').val();
        let lbCtes = $('#ddlRazonSocialCtes option:selected').text();
        /*
        Integralidad.Consultar(Rik, Seg, Ctes, function (Lst) {
            IntegralidadXls.Descarar(Rik, lbRik, Seg, lbSeg, Ctes, lbCtes, Lst);
        });
        */
        if (typeof (DatosConsulta) == 'undefined') {
            alertify.error('Debe realizar la consulta.');
        } else {
            IntegralidadXls.Descarar(Rik, lbRik, Seg, lbSeg, Ctes, lbCtes, DatosConsulta);
        }
    },

    // Desplegra Listado de 3 Meses Seleccionados
    Desplegar: function (Lst) {
        if (Lst.length <= 0) {
            alertify.alert('No se encontro información con esos parametros.');
        }
        console.log(Lst);
        $('#tblIntegralidad > tbody').empty();
        for (var i = 0; i < Lst.length; i++) {
            var row = $('<tr>');
            //row.append($('<td>').append(Lst[i].Id_Rik));
            row.append($('<td>').append(
                Lst[i].Id_Cte
            ));
            row.append($('<td>').append(
                Lst[i].Cte_NomComercial
            ));
            row.append($('<td>').append(
                Lst[i].Id_Ter
            ));
            row.append($('<td>').append(
                Lst[i].Seg_Descripcion
            ));
            row.append($('<td class="text-right">').append(
                Lst[i].Cantidad
            ));
            row.append($('<td>').append(
                Lst[i].Seg_Unidades
            ));

            var VPO = Lst[i].VPO;
            VPO = parseFloat(VPO);
            VPO = VPO.formatMoney(2, '.', ',');
            row.append($('<td class="text-right">').append(
                '$' + VPO
            ));

            var VPOMeta = Lst[i].VPOMeta;
            VPOMeta = parseFloat(VPOMeta);
            VPOMeta = VPOMeta.formatMoney(2, '.', ',');
            row.append($('<td class="text-right">').append(
                '$' + VPOMeta
                + '<button ' +
                ' title="Editar Valor portencial meta"' +
                ' data-id_rik="' + Lst[i].Id_Rik + '" ' +
                ' data-reptesentante="' + Lst[i].Rik_Nombre + '" ' +
                ' data-id_cte="' + Lst[i].Id_Cte + '" ' +
                ' data-cliente="' + Lst[i].Cte_NomComercial + '" ' +
                ' data-id_ter="' + Lst[i].Id_Ter + '" ' +
                ' data-id_seg="' + Lst[i].Id_Seg + '" ' +
                ' data-segmento="' + Lst[i].Seg_Descripcion + '" ' +
                ' data-seg_unidades="' + Lst[i].Seg_Unidades + '" ' +
                ' data-seg_valunidim="' + Lst[i].Seg_ValUniDim + '" ' +
                ' data-cantidad="' + Lst[i].Cantidad + '" ' +
                ' data-dvpometa="' + Lst[i].VPOMeta + '" ' +
                ' onclick="Integralidad.DesplegarEditarVPOMeta(this);" ' +
                'class="btn btn-primary">' +
                '<i class="btn btn-primary" aria-hidden="true"></i>' +
                '</button>'
            ));

            var PromedioTrimestral = Lst[i].PromedioTrimestral;
            PromedioTrimestral = parseFloat(PromedioTrimestral);
            //PromedioTrimestral = PromedioTrimestral * 100;
            PromedioTrimestral = PromedioTrimestral.formatMoney(2, '.', ',');
            row.append($('<td class="text-right">').append(
                '$' + PromedioTrimestral
            ));

            var Integralidad = Lst[i].Integralidad;
            Integralidad = parseFloat(Integralidad);
            Integralidad = Integralidad * 100;
            Integralidad = Integralidad.formatMoney(2, '.', ',');
            row.append($('<td class="text-right">').append(
                Integralidad + '%'
            ));

            var Integralidad_Obs = Lst[i].Integralidad_Obs;
            Integralidad_Obs = parseFloat(Integralidad_Obs);
            Integralidad_Obs = Integralidad_Obs * 100;
            Integralidad_Obs = Integralidad_Obs.formatMoney(2, '.', ',');
            row.append($('<td class="text-right">').append(
                Integralidad_Obs + '%'
            ));

            row.append($('<td class="text-center">').append(
                '<button ' +
                ' data-id_rik="' + Lst[i].Id_Rik + '" ' +
                ' data-reptesentante="' + Lst[i].Rik_Nombre + '" ' +
                ' data-id_cte="' + Lst[i].Id_Cte + '" ' +
                ' data-cliente="' + Lst[i].Cte_NomComercial + '" ' +
                ' data-id_ter="' + Lst[i].Id_Ter + '" ' +
                ' data-id_seg="' + Lst[i].Id_Seg + '" ' +
                ' data-segmento="' + Lst[i].Seg_Descripcion + '" ' +
                ' data-seg_unidades="' + Lst[i].Seg_Unidades + '" ' +
                ' data-seg_valunidim="' + Lst[i].Seg_ValUniDim + '" ' +
                ' data-cantidad="' + Lst[i].Cantidad + '" ' +
                ' onclick="Integralidad.DesplegarDetalle(this);" ' +
                'class="btn btn-primary">' +
                '<i class="fa fa-search" aria-hidden="true"></i>' +
                '</button>'
            ));

            $('#tblIntegralidad > tbody').append(row);
        }
    }
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\

$(document).ready(function () {

    // Cargar Combo REPRESENTANTES
    CatRepresentante.CargarCombo(function () {
        $('#spinner_Cargando').css('display', 'block');
        //$('#ddlRepresentante').val(0);
        var Id_Rik = $('#ddlRepresentante').val();
        // Cargar SEGMENTOS
        Cargar_Segmentos(Id_Rik, function () {
            // Cargar Combo CLIENTES
            Cargar_Cliente(Id_Rik, function () {
                $('#spinner_Cargando').css('display', 'none');
            });
        });
    });

    CatRepresentante.CargarListadoCheck(function () {
        //$('#ddlRepresentante').val(0);
        //Inicializa_Grafica();
        var GraficaTitulos = ['A', 'B', 'C'];
        var Vals1 = [0, 0, 0];
        var Vals2 = [0, 0, 0];
        //Graficas.IniciaGrafica_2(GraficaTitulos, Vals1, Vals2);
    });

    $(document).on('change', 'select[id="ddlRepresentante"]', function () {
        var Id_Rik = $('#ddlRepresentante').val();
        $('#spinner_Cargando').css('display', 'block');
        Cargar_Segmentos(Id_Rik, function () {
            Cargar_Cliente(Id_Rik, function () {
                $("#ddlRazonSocialCtes").val(0);
                $('#spinner_Cargando').css('display', 'none');
            });
        });
    });

    $('#spinner_Cargando').css('display', 'none');

    $("#ddlRazonSocialCtes").combobox();
    $("#toggle").on("click", function () {
        $("#ddlRazonSocialCtes").toggle();
    });


});