// 
// 29 Mar 2019
//

var tbNumProyectos = 0;
var tbMontoProyectos = 0;
var tbAvanceMes = 0;
var tbCantidadCerrados = 0;
var tbMontoCerrados = 0;

var VALORES = ["0", "0", "0", "0", "0"];
var VALORES2 = ["1", "2", "3", "4", "5"];

var COLOR_Analisis='#abebc6';
var COLOR_Promocion='#58d68d';
var COLOR_Negociacion='#f7CD6F';
var COLOR_Cierre='#85c1e9';
var COLOR_Cancelado='#d7DBDD';

var color = Chart.helpers.color;

var barChartData1 = {
    labels: ['Análisis', 'Promoción', 'Negociación', 'Cierre','Cancelado'],
    datasets: [{
	label: '',
				            backgroundColor: [
						        COLOR_Analisis,
                                COLOR_Promocion,
                                COLOR_Negociacion,
                                COLOR_Cierre,
                                COLOR_Cancelado
					        ],
				            borderColor: window.chartColors.blue,
				            borderWidth: 1,
    data: [
                VALORES[0],
                VALORES[1],
                VALORES[2],
                VALORES[3],
                VALORES[4]
            ]
	    }                   
    ]
};

var barChartData2 = {
   datasets: [{
					data: [
						VALORES2[0],
                        VALORES2[1],
                        VALORES2[2],
                        VALORES2[3],
                        VALORES2[4]
					],
					backgroundColor: [
						COLOR_Analisis,
                        COLOR_Promocion,
                        COLOR_Negociacion,
                        COLOR_Cierre,
                        COLOR_Cancelado
					],
					label: 'My dataset' // for legend
				}],
				labels: [
					'Análisis',
					'Promoción',
					'Negociación',
					'Cierre',
					'Cancelado'
    ]
};

var chartColors = window.chartColors;

var ctx1 = document.getElementById('canvas1').getContext('2d');
var ctx2 = document.getElementById('canvas2').getContext('2d');	                   


// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Inicializa_Grafica() {        
         
    VALORES[0] = "0";
    VALORES[1] = "0";
    VALORES[2] = "0";
    VALORES[3] = "0";
    VALORES[4] = "0";
            
    Preparar_Grafica(function (response) {

        //  console.log(response);

        var Data = response;

        var Total_A = 0;
        var Total_P = 0;
        var Total_N = 0;
        var Total_C = 0;
        var Total_Ca = 0;

        var Count_A = 0;
        var Count_P = 0;
        var Count_N = 0;
        var Count_C = 0;
        var Count_Ca = 0;

                    
        for (var i=0; i<Data.length; i++) { 

            //console.log(Data[i]);

                if (Data[i].Analisis.trim().length>0 && 
                Data[i].Presentacion.trim().length==0 && 
                Data[i].Negociacion.trim().length==0 && 
                Data[i].Cierre.trim().length==0 && 
                Data[i].Cancelacion.trim().length==0) {
                Total_A =Total_A + Data[i].MontoProyecto;        
                Count_A = Count_A + 1;
            }
            if (Data[i].Analisis.trim().length>0 && 
                Data[i].Presentacion.trim().length>0 && 
                Data[i].Negociacion.trim().length==0 && 
                Data[i].Cierre.trim().length==0 && 
                Data[i].Cancelacion.trim().length==0) {
                Total_P =Total_P + Data[i].MontoProyecto;        
                Count_P = Count_P + 1;
            }
            // Negociacion 
            if (Data[i].Analisis.trim().length>0 && 
                //Data[i].Presentacion.trim().length>0 && 
                Data[i].Negociacion.trim().length>0 && 
                Data[i].Cierre.trim().length==0 && 
                Data[i].Cancelacion.trim().length==0) {
                Total_N=Total_N + Data[i].MontoProyecto;        
                Count_N = Count_N + 1;
            }
            // Cierre
            if (Data[i].Analisis.trim().length>0 && 
                //Data[i].Presentacion.trim().length>0 && 
                //Data[i].Negociacion.trim().length>0 && 
                Data[i].Cierre.trim().length>0 && 
                Data[i].Cancelacion.trim().length==0) {
                Total_C=Total_C + Data[i].MontoProyecto;        
                Count_C = Count_C + 1;
            }
            // Cancelacion 
            if (
                //Data[i].Analisis.trim().length>0 && 
                //Data[i].Presentacion.trim().length>0 && 
                //Data[i].Negociacion.trim().length>0 && 
                //Data[i].Cierre.trim().length>0 && 
                Data[i].Cancelacion.trim().length>0) {
                Total_Ca=Total_Ca + Data[i].MontoProyecto;        
                Count_Ca = Count_Ca + 1;
            }
        } 
        VALORES[0] = Total_A;
        VALORES[1] = Total_P;
        VALORES[2] = Total_N;
        VALORES[3] = Total_C;
        VALORES[4] = Total_Ca;                      

        VALORES2[0] = Count_A;
        VALORES2[1] = Count_P;
        VALORES2[2] = Count_N;
        VALORES2[3] = Count_C;
        VALORES2[4] = Count_Ca;                      

        //  console.log(VALORES);

                    
            //var ctx1 = document.getElementById('canvas1').getContext('2d');
			window.myBar = new Chart(ctx1, {
				type: 'bar',
				data: barChartData1,
				options: {
					responsive: true,
					legend: {
						position: 'botom',
					},
				title: {
					display: true,
					text: 'DISTRIBUCIÓN'
				},
                scales: {
					xAxes: [{
						display: true,
						scaleLabel: {
							display: true,
							labelString: 'Etapa'
						},
                        label: function(tooltipItem, chart){
                            var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
                            return datasetLabel + ': $ '+number_format(tooltipItem.yLabel, 2);
                        }

					}],
					yAxes: [{
						display: true,
						scaleLabel: {
							display: true,
							labelString: 'Monto'
						}
					}]
				},
                tooltips: {
                    callbacks: {
                        label: function(tooltipItem, chart){
                            var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
                            return datasetLabel + ': $ ' + number_format(tooltipItem.yLabel, 2);
                        }
                    }
                }

			}
        });
                
        var randomScalingFactor = function() {
			return Math.round(Math.random() * 100);
		};
		
		var config = {
            type: 'doughnut',
			data: barChartData2,
			options: {
				responsive: true,
				legend: {
					position: 'right',
				},
				title: {
					display: true,
					text: 'Actividad'
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
        
        window.myPolarArea = new Chart(ctx2, config);              

        }
    );
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
function Grafica_Reload(CALLBACK_Exito) {
        
    VALORES[0] = "0";
    VALORES[1] = "0";
    VALORES[2] = "0";
    VALORES[3] = "0";
    VALORES[4] = "0";
            
    Preparar_Grafica(function (response) {

        var Data = response;

        //  console.log(Data);

        var Total_A = 0;
        var Total_P = 0;
        var Total_N = 0;
        var Total_C = 0;
        var Total_Ca = 0;

        var Count_A = 0;
        var Count_P = 0;
        var Count_N = 0;
        var Count_C = 0;
        var Count_Ca = 0;
                            
        for (var i=0; i<Data.length; i++) { 

        //console.log(Data[i]);

        if (Data[i].Analisis.trim().length>0 && 
            Data[i].Presentacion.trim().length==0 && 
            Data[i].Negociacion.trim().length==0 && 
            Data[i].Cierre.trim().length==0 && 
            Data[i].Cancelacion.trim().length==0) {
            Total_A =Total_A + Data[i].MontoProyecto;        
            Count_A = Count_A + 1;
        }
        if (Data[i].Analisis.trim().length>0 && 
            Data[i].Presentacion.trim().length>0 && 
            Data[i].Negociacion.trim().length==0 && 
            Data[i].Cierre.trim().length==0 && 
            Data[i].Cancelacion.trim().length==0) {
            Total_P =Total_P + Data[i].MontoProyecto;        
            Count_P = Count_P + 1;
        }
        // Negociacion 
        if (Data[i].Analisis.trim().length>0 && 
            //Data[i].Presentacion.trim().length>0 && 
            Data[i].Negociacion.trim().length>0 && 
            Data[i].Cierre.trim().length==0 && 
            Data[i].Cancelacion.trim().length==0) {
            Total_N=Total_N + Data[i].MontoProyecto;        
            Count_N = Count_N + 1;
        }
        // Cierre
        if (Data[i].Analisis.trim().length>0 && 
            //Data[i].Presentacion.trim().length>0 && 
            //Data[i].Negociacion.trim().length>0 && 
            Data[i].Cierre.trim().length>0 && 
            Data[i].Cancelacion.trim().length==0) {
            Total_C=Total_C + Data[i].MontoProyecto;        
            Count_C = Count_C + 1;
        }
        // Cancelacion 
        if (
            //Data[i].Analisis.trim().length>0 && 
            //Data[i].Presentacion.trim().length>0 && 
            //Data[i].Negociacion.trim().length>0 && 
            //Data[i].Cierre.trim().length>0 && 
            Data[i].Cancelacion.trim().length>0) {
            Total_Ca=Total_Ca + Data[i].MontoProyecto;        
            Count_Ca = Count_Ca + 1;
        }
        } 
        VALORES[0] = Total_A;
        VALORES[1] = Total_P;
        VALORES[2] = Total_N;
        VALORES[3] = Total_C;
        VALORES[4] = Total_Ca;                      

        VALORES2[0] = Count_A;
        VALORES2[1] = Count_P;
        VALORES2[2] = Count_N;
        VALORES2[3] = Count_C;
        VALORES2[4] = Count_Ca;   
        
        tbNumProyectos = Count_A + Count_P + Count_N + Count_C + Count_Ca;   
        tbCantidadCerrados = Count_C;
        tbMontoCerrados = Total_C; 
        tbMontoProyectos =   Total_A + Total_P + Total_N + Total_C + Total_Ca;              
        
        //  console.log(VALORES);
        
         var zero = Math.random() < 0.2 ? true : false;
         var i = 0;

         barChartData1.datasets[0].data =[
            VALORES[0],
            VALORES[1],
            VALORES[2],
            VALORES[3],
            VALORES[4]            
         ]
	     window.myBar.update();    

         barChartData2.datasets[0].data =[
            VALORES2[0],
            VALORES2[1],
            VALORES2[2],
            VALORES2[3],
            VALORES2[4]            
         ]
	     window.myPolarArea.update();    

         if (CALLBACK_Exito) {
            CALLBACK_Exito();
         }
         
    });         
}

//
// ABR17-2020 RFH Creacion  
//
function Cargar_Metas(CALLBACK_Exito) {

    var Id_Rik = $('#ddlRepresentantesComercial').val();
    console.log('IdRik: '+Id_Rik);
    $('#Spinner_FullDashboard').css('display','block');
    
    $.ajax({
        url: _ApplicationUrl + '/api/CrmMetas/GetMetas_PorRik?Id_Rik='+Id_Rik,
        cache: false,
        type: 'GET',
    }).done(function (response, textStatus, jqXHR) {                
        var Estado = response.Estado;
        var Datos = response.Datos;
        
        if (Estado==1) {
            if (CALLBACK_Exito) {
                CALLBACK_Exito(Datos);
                $('#Spinner_FullDashboard').css('display','none');
            }                
        } else {
            alertify.error('Error al cargar metas.');                
        }        
        
    }).fail(function (jqXHR, textStatus, error) {
        $('#Spinner_Cargando').css('display','none');                
        //alertify.error('Ocurrió una complicación al cargar las UENs para el registro de Proyectos');                
    });
}

// /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\
$(document).ready(function () {

    $('#btnRefrescarGrafica').click(function () {

        var tbPeriodoInicio = $('#tbPeriodoInicio').val();
        var tbPeriodoFin = $('#tbPeriodoFin').val();

        alert('Periodo:'+tbPeriodoInicio+' - '+tbPeriodoFin);

        /*
        var ctx1 = document.getElementById('canvas1').getContext('2d');
        ctx1.clear();

        var ctx2 = document.getElementById('canvas2').getContext('2d');
        ctx2.clear();
        */
        //Inicializa_Grafica();
        Grafica_Reload(function () {
           //tbNumProyectos = numeral(tbNumProyectos).format('$0,0.00');
           tbMontoProyectos = numeral(tbMontoProyectos).format('$0,0');
           //tbAvanceMes = numeral(tbAvanceMes).format('$0,0.00');
           tbMontoCerrados = numeral(tbMontoCerrados).format('$0,0');
            $('#tbNumProyectos').val(tbNumProyectos);
            $('#tbMontoProyectos').val(tbMontoProyectos);
            $('#tbAvanceMes').val(tbAvanceMes);
            $('#tbCantidadCerrados').val(tbCantidadCerrados);
            $('#tbMontoCerrados').val(tbMontoCerrados);
        });        
        /*
        Cargar_Metas(function (REG) {
            $('#<%= NumProyectos.ClientID %>').val(REG.Met_Proyectos);
            $('#<%= MontoProyectos.ClientID %>').val(REG.Met_MontoProyecto);
            $('#<%= AvanceMes.ClientID %>').val(REG.Met_Avances);
            $('#<%= CantidadCerrados.ClientID %>').val(REG.Met_CantCerrado);
            $('#<%= MontoCerrados.ClientID %>').val(REG.Met_MontCerrado);
        });    
        */
    });

    window.onload = function() {        
        Inicializa_Grafica();
    };

});







