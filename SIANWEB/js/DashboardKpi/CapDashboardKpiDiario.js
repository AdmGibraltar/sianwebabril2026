function ConsultaDSKpiDiario() {


    let urlApp = ApplicationUrl + "/api/DashboardKpiDiario/ConsultarKpiDiario";

    fetch(urlApp, {
        method: "GET",
        mode: "cors",
        cache: "no-cache",
        credentials: "same-origin",
        redirect: "follow",
        referrer: "no-referrer",
        headers: {
            'Content-Type': 'application/json'
        }
    }).then(function (response) {
        if (response.status !== 404 && response.status !== 500) {
            return response.json();
        }
        return Promise.reject(response.statusText)
    }).then(function (dataRespuesta) {

        console.log(dataRespuesta);
        InicializarDS(dataRespuesta);

    }).catch(function (error) {

        if (typeof error !== "undefined") {
            console.log(error);
        }

    }).finally(function () {

    });


};

Chart.register(ChartDataLabels);

//Chart.defaults.set('plugins.datalabels', {
//    color: '#FE777B'
//});
function GraficaDona(gElement, gData, gLabel, bgColor = 1, isEtqDato = false) {
    let gElmDona = document.getElementById(gElement);
    
    let valueBgColor;
    let valueBorderColor;
    switch (bgColor) {
        case 1: // Azul
            valueBgColor = ["rgba(54, 162, 235, 0.8)", "rgba(75, 192, 192, 0.2)"];
            valueBorderColor = ["rgba(54, 162, 235)", "rgba(75, 192, 192)"];
            break;
        case 2: // verde
            valueBgColor = ["rgba(0, 204, 102, 0.8)", "rgba(153, 255, 102, 0.4)"];
            valueBorderColor = ["rgba(0, 204, 102)", "rgba(153, 255, 102)"];
            break;
        case 3: // amarillo
            valueBgColor = ["rgba(255, 204, 0, 0.6) ", "rgba(230, 230, 0, 0.4)"];
            valueBorderColor = ["rgba(255, 204, 0)", "rgb(230, 230, 0)"];
            break;
        case 4: // Rojo
            valueBgColor = ["rgba(204, 0, 0, 0.8)", "rgba(255, 102, 102, 0.8)"];
            valueBorderColor = ["rgba(204, 0, 0)", "rgba(255, 102, 102)"];
            break;
        case 5: // Azul orcuro
            valueBgColor = ["rgba(51, 102, 153, 0.8)", "rgba(75, 192, 192, 0.2)"];
            valueBorderColor = ["rgba(51, 102, 153)", "rgba(75, 192, 192)"];
            break;
        case 6: // colores
            valueBgColor = ["rgba(54, 162, 235, 0.8)", "rgba(0, 204, 102, 0.8)", "rgba(255, 204, 0, 0.6) "];
            valueBorderColor = ["rgba(54, 162, 235)", "rgba(0, 204, 102)", "rgba(255, 204, 0)"];
            break;
        default:
            valueBgColor = ["rgba(54, 162, 235, 0.8)", "rgba(75, 192, 192, 0.2)"];
            valueBorderColor = ["rgba(54, 162, 235)", "rgba(75, 192, 192)"];
            break;


    }
    let gDonaData = {
        labels: gLabel,
        datasets: [{
            data: gData,
            backgroundColor: valueBgColor,
            borderColor: valueBorderColor,
            borderWidth: 1,
            datalabels: {
                anchor: 'start'
            }
        }],

        hoverOffset: 4,


    };

    let objOption;

    if (isEtqDato) {
        objOption = {
            responsive: true,
            legend: {
                display: false
            },
            plugins: {
                legend: {
                    display: false,

                },
                tooltip: {
                    enabled: false
                },
                datalabels: {
                    backgroundColor: function (context) {
                        return context.dataset.backgroundColor;
                    },
                    borderColor: 'white',
                    borderRadius: 10,
                    borderWidth: 1,
                    color: 'white',
                    display: function (context) {
                        var dataset = context.dataset;
                        var count = dataset.data.length;
                        var value = dataset.data[context.dataIndex];
                        //if (value < 1) {
                        //    value += 2;
                        //}
                        let valor = 0;
                        if (context.dataIndex == 0) {
                            valor = value;// > count * 1.5;

                        }

                        return valor;
                    },
                    font: {
                        weight: 'bold'
                    },
                    padding: 6,
                    formatter: function (value, context) {
                        let miEtiqueta = "";
                        if (context.dataIndex == 0) {
                            miEtiqueta = context.chart.data.labels[context.dataIndex]
                        }
                        return miEtiqueta;
                    }
                }
            }
        };
        //tooltips: {
        //    enabled: false
        //    //callbacks: {
        //    //    label: function (tooltipItem, data) {
        //    //        var label = data.labels[tooltipItem.datasetIndex] || '';  
        //    //        console.log(tooltipItem);
        //    //        console.log(label);
        //    //        return label;
        //    //    }

        //    //}
        //}

    } else {
        objOption = {
            responsive: true,
            legend: {
                display: false
            },
            // tooltips: {
            //    enabled: false
            //}
            plugins: {
                legend: {
                    display: false,

                },
                tooltip: {
                    enabled: false
                },
                datalabels: {
                    backgroundColor: function (context) {
                        return context.dataset.backgroundColor;
                    },
                    borderColor: 'white',
                    borderRadius: 10,
                    borderWidth: 1,
                    color: 'white',
                    display: function (context) {
                        var dataset = context.dataset;
                        var count = dataset.data.length;
                        var value = dataset.data[context.dataIndex];
                        //if (value < 1) {
                        //    value += 2;
                        //}
                        let valor = 0;
                        if (context.dataIndex == 0) {
                            valor = value;// > count * 1.5;

                        }

                        return valor;
                    },
                    font: {
                        weight: 'bold'
                    },
                    padding: 6,
                    formatter: function (value, context) {
                        let miEtiqueta = "";
                        if (context.dataIndex == 0) {
                            miEtiqueta = context.chart.data.labels[context.dataIndex]
                        }
                        return miEtiqueta;
                    }
                }
            }
        }
    }

    let gDona = new Chart(gElmDona, {
        type: 'doughnut',
        data: gDonaData,
        options: objOption

    });

}

function GraficaDonaFactura(gElement, gData, gLabel) {
    let gElmDona = document.getElementById(gElement);

    let valueBgColor;
    let valueBorderColor;

    valueBgColor = ["rgba(204, 0, 0, 0.8)","rgba(54, 162, 235, 0.8)", "rgba(0, 204, 102, 0.8)"];
    valueBorderColor = ["rgba(204, 0, 0)","rgba(54, 162, 235)", "rgba(0, 204, 102)"];

    let gDonaData = {
        labels: gLabel,
        datasets: [{
            data: gData,
            backgroundColor: valueBgColor,
            borderColor: valueBorderColor,
            borderWidth: 1
        }],
        hoverOffset: 4,
    };

    let objOption = {
        responsive: true,
        maintainAspectRatio: true,
        plugins: {
            legend: {
                display: true,
                position: 'top',
                padding: {
                    top: 10,
                    bottom: 40,
                    left: 10,
                    right: 10
                },
                labels: {
                    boxWidth: 15,
                    boxHeight: 15,
                    padding: 10,
                    font: {
                        size: 11
                    },
                    usePointStyle: false
                }
            },
            tooltip: {
                enabled: false
            },
            datalabels: {
                anchor: 'end',
                align: function (context) {
                    var index = context.dataIndex;
                    var count = context.dataset.data.length;
                    var angle = (index * 360 / count) + 90;
                    return angle > 180 ? 'left' : 'right';
                },
                offset: 10,
                backgroundColor: function (context) {
                    return context.dataset.backgroundColor[context.dataIndex];
                },
                borderColor: 'white',
                borderRadius: 5,
                borderWidth: 1,
                color: 'black',
                font: {
                    weight: 'bold',
                    size: 11
                },
                padding: 4,
                formatter: function (value, context) {
                    if (value === 0) return '';
                    return Math.round(value);
                },
                display: function (context) {
                    var value = context.dataset.data[context.dataIndex];
                    return value > 0;
                }
            }
        },
        layout: {
            padding: {
                top: 5,
                bottom: 20,
                left: 0,
                right: 20
            }
        }
    };

    let gDona = new Chart(gElmDona, {
        type: 'pie',
        data: gDonaData,
        options: objOption
    });
}

function GraficaBarraHorizontal(gElement, gData, gLabel) {

    var grafica = new Chart(document.getElementById(gElement),
        {
            type: "bar",
            data: {
                labels: gLabel,
                datasets: [{
                    axis: 'y',
                    label: 'Avance de presupuesto',
                    data: gData,
                    fill: false,
                    backgroundColor: ["rgba(255, 99, 132, 0.2)", "rgba(255, 159, 64, 0.2)", "rgba(255, 205, 86, 0.2)", "rgba(75, 192, 192, 0.2)", "rgba(54, 162, 235, 0.2)", "rgba(153, 102, 255, 0.2)", "rgba(201, 203, 207, 0.2)"],
                    borderColor: ["rgb(255, 99, 132)", "rgb(255, 159, 64)", "rgb(255, 205, 86)", "rgb(75, 192, 192)", "rgb(54, 162, 235)", "rgb(153, 102, 255)", "rgb(201, 203, 207)"],
                    borderWidth: 1
                }]
            },
            options: {
                indexAxis: 'y',
                plugins: {
                    legend: {
                        display: false,

                    },
                    tooltip: {
                        callbacks: {
                            label: function (context) {
                                console.log(context);
                                let label = context.dataset.label || '';

                                if (label) {
                                    label += ': ';
                                }
                                if (context.parsed.x !== null) {
                                    label += context.parsed.x + " %";
                                }
                                return label;
                            }
                        }
                    }
                },
            }
        });

}


function GraficasLeft() {

    let gRVigentes = document.getElementById('graficaRemisionesVigentes');
    let gRVencidas = document.getElementById('graficaRemisionesVencidas');

    let gDataRVigentes = {
        datasets: [{
            data: [80, 20],
            backgroundColor: ['blue', 'gray']
        }],
        labels: ['a', 'b']
    };
    var gObjRVigentes = new Chart(gRVigentes.getContext('2d'), {
        type: 'doughnut',
        data: gDataRVigentes,
        options: {
            responsive: true,
            legend: {
                display: false
            }
        }
    });

    let gDataRVencidas = {
        datasets: [{
            data: [80, 20],
            backgroundColor: ['blue', 'gray']
        }],
        labels: ['a', 'b']
    };

    var gObjRVencidas = new Chart(gRVencidas.getContext('2d'), {
        type: 'doughnut',
        data: gDataRVencidas,
        options: {
            responsive: true,
            legend: {
                display: false
            }
        }
    });

}

function ConsultarDatos() {

}
function InicializarDS(datosDS) {

    let datosHeader = datosDS.objHeader;
    let datosKpiGeneral = datosDS.objKpiGeneral;
    let datosTtlCartera = datosDS.objTtlCartera;
    let datosKpiRik = datosDS.lstKpiRik;
    let datosKpiFactura = datosDS.objKpiFactura;    


    /*  Inicia Header  */
    $("#utilidadPPTOPorcentaje").html(datosHeader.utilidadPPTOPorcentaje);
    $("#utilidadDiaPorcentaje").html(datosHeader.utilidadDiaPorcentaje);
    $("#utilidadPptoMoneda").html(datosHeader.utilidadPptoMoneda);
    $("#utilidadDiaMoneda").html(datosHeader.utilidadDiaMoneda);
    $("#fechaDashBoard").html(datosHeader.fechaDashBoard);
    $("#cdi_nombre").html(datosHeader.NombreCD);
    

    let gHeader1Data = [];
    gHeader1Data.push(datosHeader.gCumplimientoUBPorcenjeData1);
    gHeader1Data.push(datosHeader.gCumplimientoUBPorcenjeData2);

    let gHeader1Label = [];
    gHeader1Label.push(datosHeader.gCumplimientoUBPorcenjeLabel1);
    gHeader1Label.push(datosHeader.gCumplimientoUBPorcenjeLabel2);

    let gHeader2Data = [];
    gHeader2Data.push(datosHeader.gCumplimientoUBPesosData1);
    gHeader2Data.push(datosHeader.gCumplimientoUBPesosData2);

    let gHeader2Label = [];
    gHeader2Label.push(datosHeader.gCumplimientoUBPesosLabel1);
    gHeader2Label.push(datosHeader.gCumplimientoUBPesosLabel2);


    GraficaDona("graficaHeader1", gHeader1Data, gHeader1Label, 1, true);
    GraficaDona("graficaHeader2", gHeader2Data, gHeader2Label, 1, true);

    /*  Fin Header  */

    /*  Inicio Contenedor columna izquierda  */
    $("#presupuestoGnrl").html(datosKpiGeneral.presupuestoGnrl);
    $("#presupuestoGnrlRestante").html(datosKpiGeneral.presupuestoGnrlRestante);
    $("#cumplimientoGnrl").html(datosKpiGeneral.cumplimientoGnrl);
    $("#cumplimientoGnrlPorcentaje").html(datosKpiGeneral.cumplimientoGnrlPorcentaje);

    $("#remisionesPxFVigentes").html(datosKpiGeneral.remisionesPxFVigentes);
    $("#remisionesPxFVencidas").html(datosKpiGeneral.remisionesPxFVencidas);
    $("#remisionesPxFGnrl").html(datosKpiGeneral.remisionesPxFGnrl);

    let gGnrlRemisionesVigentes = [];
    gGnrlRemisionesVigentes.push(datosKpiGeneral.gVigentesData1);
    gGnrlRemisionesVigentes.push(datosKpiGeneral.gVigentesData2);

    let gGnrlRemisionesVigentesLabel = [];
    gGnrlRemisionesVigentesLabel.push(datosKpiGeneral.gVigentesLabel1);
    gGnrlRemisionesVigentesLabel.push(datosKpiGeneral.gVigentesLabel2);

    let gGnrlRemisionesVencidas = [];
    gGnrlRemisionesVencidas.push(datosKpiGeneral.gVencidasData1);
    gGnrlRemisionesVencidas.push(datosKpiGeneral.gVencidasData2);

    let gGnrlRemisionesVencidasLabel = [];
    gGnrlRemisionesVencidasLabel.push(datosKpiGeneral.gVencidasLabel1);
    gGnrlRemisionesVencidasLabel.push(datosKpiGeneral.gVencidasLabel2)

    GraficaDona("graficaRemisionesVigentes", gGnrlRemisionesVigentes, gGnrlRemisionesVigentesLabel, 1);
    GraficaDona("graficaRemisionesVencidas", gGnrlRemisionesVencidas, gGnrlRemisionesVencidasLabel, 5);

    // asignar los valores a los elementos: , , , .
    console.log(datosKpiFactura);

    $("#spnNumBaja").html(datosKpiFactura.NumBaja);
    $("#spnNumFacturas").html(datosKpiFactura.NumFacturas);
    $("#spnNumRefacturado").html(datosKpiFactura.NumRefacturado);
    $("#spnImporteBaja").html(datosKpiFactura.ImporteBaja);
    $("#spnImporteFacturas").html(datosKpiFactura.ImporteFacturas);
    $("#spnImporteRefacturado").html(datosKpiFactura.ImporteRefacturado);
    $("#spnImporteGeneral").html(datosKpiFactura.ImporteGeneral);

    let gGnrlFacturasNum = [];
    gGnrlFacturasNum.push(datosKpiFactura.gFacturaNumData1);
    gGnrlFacturasNum.push(datosKpiFactura.gFacturaNumData2);
    gGnrlFacturasNum.push(datosKpiFactura.gFacturaNumData3);

    let gGnrlFacturasNumLabel = [];
    gGnrlFacturasNumLabel.push("Total Canceladas");
    gGnrlFacturasNumLabel.push("Total Refacturado");
    gGnrlFacturasNumLabel.push("Total Facturas");

    let gGnrlFacturasImporte = [];
    gGnrlFacturasImporte.push(datosKpiFactura.gFacturaImporteData1);
    gGnrlFacturasImporte.push(datosKpiFactura.gFacturaImporteData2);
    gGnrlFacturasImporte.push(datosKpiFactura.gFacturaImporteData3);

    let gGnrlFacturasImporteLabel = [];
    gGnrlFacturasImporteLabel.push("Importe Canceladas");
    gGnrlFacturasImporteLabel.push("Importe Refacturado");
    gGnrlFacturasImporteLabel.push("Importe Facturas");

    GraficaDonaFactura("graficaNumFacturas", gGnrlFacturasNum, gGnrlFacturasNumLabel);
    GraficaDonaFactura("graficaImporteFacturas", gGnrlFacturasImporte, gGnrlFacturasImporteLabel);

    /*  Fin Contenedor columna izquierda  */

    /*  Inicio  Totales Cartera  */

    $("#ttlCarteraCobranza").html(datosTtlCartera.ttlCarteraCobranza);
    $("#ttlCarteraTiempo").html(datosTtlCartera.ttlCarteraTiempo);
    $("#ttlCarteraMenos30dias").html(datosTtlCartera.ttlCarteraMenos30dias);
    $("#ttlCarteraMas30dias").html(datosTtlCartera.ttlCarteraMas30dias);

    let gTtlCarteraTiempoValue = [];
    gTtlCarteraTiempoValue.push(datosTtlCartera.gTiempoData1);
    gTtlCarteraTiempoValue.push(datosTtlCartera.gTiempoData2);

    let gTtlCarteraTiempoLabel = [];
    gTtlCarteraTiempoLabel.push(datosTtlCartera.gTiempoLabel1);
    gTtlCarteraTiempoLabel.push(datosTtlCartera.gTiempoLabel2);

    let gTtlCarteraMenos30Value = [];
    gTtlCarteraMenos30Value.push(datosTtlCartera.gMenos30Data1);
    gTtlCarteraMenos30Value.push(datosTtlCartera.gMenos30Data2);

    let gTtlCarteraMenos30Label = [];
    gTtlCarteraMenos30Label.push(datosTtlCartera.gMenos30Label1);
    gTtlCarteraMenos30Label.push(datosTtlCartera.gMenos30Label2);

    let gTtlCarteraMas30Value = [];
    gTtlCarteraMas30Value.push(datosTtlCartera.gMas30Data1);
    gTtlCarteraMas30Value.push(datosTtlCartera.gMas30Data2);

    let gTtlCarteraMas30Label = [];
    gTtlCarteraMas30Label.push(datosTtlCartera.gMas30Label1);
    gTtlCarteraMas30Label.push(datosTtlCartera.gMas30Label2)

    GraficaDona("graficaTtlCarteraTiempo", gTtlCarteraTiempoValue, gTtlCarteraTiempoLabel, 2);
    GraficaDona("graficaTtlCarteraMenos30dias", gTtlCarteraMenos30Value, gTtlCarteraMenos30Label, 3);
    GraficaDona("graficaTtlCarteraMas30dias", gTtlCarteraMas30Value, gTtlCarteraMas30Label, 4);

    /*  Fin Totales Cartera */
    /*  Inicio KPI Rik */
    // let ttlItem = Object.keys(datosKpiRik).length;

    let gRikKpiValue = [];
    let gRikKpiLabel = [];

    let gRemisionesRikValue = [];
    let gRemisionesRikLabel = [];

    let ttlItem = datosKpiRik.length;

    let gCumplimientoVentaRikDatos = [];
    let gCumplimientoVentaRikEtiqueta = [];

    for (let index = 0; index < ttlItem; index++) {

        CrearTargetaRik(index);

        $("#NombreRik_" + index).html(datosKpiRik[index].NombreRik);
        $("#presupuestoRik_" + index).html(datosKpiRik[index].presupuestoRik);
        $("#cumplimientoRik_" + index).html(datosKpiRik[index].cumplimientoRik);
        $("#cteActivoRik_" + index).html(datosKpiRik[index].cteActivoRik);
        $("#carteraTiempoRik_" + index).html(datosKpiRik[index].carteraTiempoRik);
        $("#carteraVencidaRik_" + index).html(datosKpiRik[index].carteraVencidaRik);
        $("#remisionesTiempoRik_" + index).html(datosKpiRik[index].remisionesTiempoRik);
        $("#remisionesVencidaRik_" + index).html(datosKpiRik[index].remisionesVencidaRik);

        gRikKpiValue = [];
        gRikKpiLabel = [];

        gRemisionesRikValue = [];
        gRemisionesRikLabel = [];


        gCumplimientoVentaRikDatos.push(datosKpiRik[index].gKpiRikDato1);
        gCumplimientoVentaRikEtiqueta.push(acortarNombre(datosKpiRik[index].NombreRik));

        gRikKpiValue.push(datosKpiRik[index].gKpiRikDato1);
        gRikKpiValue.push(datosKpiRik[index].gKpiRikDato2);

        gRikKpiLabel.push(datosKpiRik[index].gKpiRikLabel1);
        gRikKpiLabel.push(datosKpiRik[index].gKpiRikLabel2);

        gRemisionesRikValue.push(datosKpiRik[index].gRemisionesRikDato1);
        gRemisionesRikValue.push(datosKpiRik[index].gRemisionesRikDato2);

        gRemisionesRikLabel.push(datosKpiRik[index].gRemisionesRikLabel1);
        gRemisionesRikLabel.push(datosKpiRik[index].gRemisionesRikLabel2);

        GraficaDona("graficaRikKpi_" + index, gRikKpiValue, gRikKpiLabel, 1);
        GraficaDona("graficaRemisionesRik_" + index, gRemisionesRikValue, gRemisionesRikLabel, 5);

        //$("#" + index).html(datosKpiRik[i].);

        console.log(index);
    }

    /*  Fin KPI Rik */

    GraficaBarraHorizontal("graficaCumplimientoVentaRik", gCumplimientoVentaRikDatos, gCumplimientoVentaRikEtiqueta);
}

function acortarNombre(nombreCompleto) {
    // Divide el nombre completo en partes
    //let partes = nombreCompleto.split(' ');

    //// Mientras el nombre completo tenga más de 25 caracteres
    //while (nombreCompleto.length > 22 && partes.length > 1) {
    //    // Elimina el último apellido
    //    partes.pop();
    //    // Une las partes restantes
    //    nombreCompleto = partes.join(' ');
    //}
    if (nombreCompleto.length > 20) {
        nombreCompleto = nombreCompleto.substring(0, 20) +".";
    }
    return nombreCompleto;
}


function CrearTargetaRik(index) {
    let targetaHtml = `<div class="col-sm-6 col-xl-4 col-slim"> 
                        <div class="panel panel-default">
                            <div class="panel-body bg-blanco-opaco">
                                <div class="row">
                                    <div class="col-sm-12 col-slim">
                                     <p><span class="etiqueta-grande" id="NombreRik_`+ index + `"></span>   </p>
                                    </div>
                                    <div class="col-sm-6 col-slim">                                       
                                        <p>Presupuesto <span id="presupuestoRik_`+ index + `" class="dato-kpi-rik"></span></p>
                                        <p>Cumplimiento <span id="cumplimientoRik_`+ index + `" class="dato-kpi-rik"></span></p>
                                        </br>
                                         <p > <img src="Imagenes/Ds_Billetes.png" width="40" height="30"></p>
                                    </div>
                                    <div class="col-sm-6 col-slim">
                                        <div class="row">
                                            <div class="col-sm-6 col-slim text-center">
                                                <p>Avance de presupuesto </p>
                                                <div class="g-content-rik">
                                                    <canvas id="graficaRikKpi_`+ index + `" ></canvas>
                                                </div>
                                            </div>
                                            <div class="col-sm-6 col-slim text-center">
                                                <img src="Imagenes/Ds_all_user.png" width="50" height="50">
                                                <p>Clientes Activos: <span id="cteActivoRik_`+ index + `" class="dato-kpi-rik"></span></p>                                                
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row  text-center" style="margin-top: 5px;" >
                                    <div class="col-sm-4 col-slim">
                                        <p><b>Cartera de cobranza</b><br>
                                        En tiempo <br> <span id="carteraTiempoRik_`+ index + `" class="dato-kpi-rik"></span></p>
                                        <p>Vencida <br><span id="carteraVencidaRik_`+ index + `" class="dato-kpi-rik"></span></p>
                                    </div>
                                    <div class="col-sm-5 col-slim">
                                        <p><b>Remisiones PXF</b></br>
                                        En tiempo <br><span id="remisionesTiempoRik_`+ index + `" class="dato-kpi-rik"></span></p>
                                        <p>Vencida <br><span id="remisionesVencidaRik_`+ index + `" class="dato-kpi-rik"></span></p>
                                    </div>
                                    <div class="col-sm-3 col-slim  text-center">
                                         <p> % U.B. </p>
                                        <div class="g-content-rik">
                                            <canvas id="graficaRemisionesRik_`+ index + `"  ></canvas>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div > `;

    $("#targetaRik").append(targetaHtml);
}


window.addEventListener("load", (event) => {
    ConsultaDSKpiDiario();
});

//Chart.defaults.plugins.tooltip = function (tooltip) {
//    // Tooltip Element
//    var tooltipEl = $('#chartjs-tooltip');
//    if (!tooltipEl[0]) {
//        $('body').append('<div id="chartjs-tooltip"></div>');
//        tooltipEl = $('#chartjs-tooltip');
//    }
//    // Hide if no tooltip
//    if (!tooltip.opacity) {
//        tooltipEl.css({
//            opacity: 0
//        });
//        $('.chartjs-wrap canvas').each(function (index, el) {
//            $(el).css('cursor', 'default');
//        });
//        return;
//    }
//    $(this._chart.canvas).css('cursor', 'pointer');
//    // Set caret Position
//    tooltipEl.removeClass('above below no-transform');
//    if (tooltip.yAlign) {
//        tooltipEl.addClass(tooltip.yAlign);
//    } else {
//        tooltipEl.addClass('no-transform');
//    }
//    // Set Text
//    if (tooltip.body) {
//        var innerHtml = [
//            (tooltip.beforeTitle || []).join('\n'), (tooltip.title || []).join('\n'), (tooltip.afterTitle || []).join('\n'), (tooltip.beforeBody || []).join('\n'), (tooltip.body || []).join('\n'), (tooltip.afterBody || []).join('\n'), (tooltip.beforeFooter || [])
//                .join('\n'), (tooltip.footer || []).join('\n'), (tooltip.afterFooter || []).join('\n')
//        ];
//        tooltipEl.html(innerHtml.join('\n'));
//    }
//    // Find Y Location on page
//    var top = 0;

//    if (tooltip.yAlign) {
//        var ch = 0;
//        if (tooltip.caretHeight) {
//            ch = tooltip.caretHeight;
//        }
//        if (tooltip.yAlign == 'above') {
//            top = tooltip.y - ch - tooltip.caretPadding;
//        } else {
//            top = tooltip.y + ch + tooltip.caretPadding;
//        }
//    }

//    var position = $('#chart-area2').offset(); /* location within document */

//    // Display, position, and set styles for font
//    tooltipEl.css({
//        opacity: 1,
//        width: tooltip.width ? (tooltip.width + 'px') : 'auto',
//        left: position.left + tooltip.x + 'px',
//        top: position.top + top + 'px',
//        fontFamily: tooltip._fontFamily,
//        fontSize: tooltip.fontSize,
//        fontStyle: tooltip._fontStyle,
//        padding: tooltip.yPadding + 'px ' + tooltip.xPadding + 'px',
//    });
//};