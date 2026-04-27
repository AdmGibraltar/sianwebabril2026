<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GraficaEmbudo.aspx.cs" Inherits="SIANWEB.GraficaEmbudo" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
<script type="text/javascript" src="js/Chart.bundle.js"></script>
<script type="text/javascript" src="js/utils.js"></script>
<script type="text/javascript" src="js/chart.funnel.bundled.js"></script>
<script type="text/javascript" src="js/modernizr.custom.04022.js"></script>
<script type="text/javascript" src="js/jquery.min.js"></script>
<link rel="stylesheet" type="text/css" href="js/style3.css" />
    <style>
	canvas {
		-moz-user-select: none;
		-webkit-user-select: none;
		-ms-user-select: none;
	}
	</style>
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest" ClientEvents-OnRequestStart="onRequestStart">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
         <ClientEvents OnRequestStart="onRequestStart" />
    </telerik:RadAjaxManager>
    
    <div runat="server" id="divPrincipal">
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td><asp:Label ID="lblMensaje" runat="server"></asp:Label></td>
                <td style="text-align: right" width="150px">&nbsp;</td>
                <td width="150px" style="font-weight: bold">&nbsp;<asp:HiddenField ID="HF_Cve" runat="server" /></td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 12pt;">
            <tr>
                <td style=" background-color:#FFFFFF; font-variant:small-caps;"><a href='#'>Clientes</a></td>
                <td style=" width:10px">&nbsp;</td>
                <td style=" background-color:#FFFFFF; font-variant:small-caps;"><b><a href='GraficoVentas.aspx'>Venta Instalada</a></b></td>
                <td style=" width:10px">&nbsp;</td>
                <td style=" background-color:#FFFFFF; font-variant:small-caps;"><b><a href='GraficoVentaSucursal.aspx'>Venta Instalada Sucursal</a></b></td>
            </tr>
    </table>
        <table style="font-family: Verdana; font-size: 8pt; border-width:thick" >
            <tr><td>&nbsp;</td></tr>
            <tr>
                <td align="left" valign="top" style=" width:400px;">
                    <telerik:RadGrid ID="rgEmbudo" runat="server" AutoGenerateColumns="false" GridLines="Both"
                            PageSize="25" AllowPaging="true" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                            OnNeedDataSource="rgEmbudo_NeedDataSource" style=" z-index:10000" >
                            <MasterTableView DataKeyNames="Ordern" DataMember="listEmbudo">
                                <Columns>
                                    <telerik:GridBoundColumn HeaderText="Orden" UniqueName="Ordern" DataField="Ordern"
                                        Display="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Cliente" UniqueName="RowDesc" DataField="RowDesc" Display="true">
                                        <HeaderStyle Width="150px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Visita Personal" UniqueName="A" DataField="A" >
                                        <HeaderStyle Width="80px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Llamada Telefonica" UniqueName="B" DataField="B" >
                                        <HeaderStyle Width="80px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Correo Electronico" UniqueName="C" DataField="C" >
                                        <HeaderStyle Width="80px" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                </td>
                <td>&nbsp;</td>
                <td valign="top" style=" border-style:double; border-width:thick">
                    <canvas id="chart-area" height="250"  width="400" ></canvas>
                </td>
            </tr>
            <tr>
                <td valign="top" style=" border-style:double; border-width:thick">
                    <canvas id="chart-area2" height="250"  width="400" ></canvas>
                </td>
                <td>&nbsp;</td>
                <td valign="top" style=" border-style:double; border-width:thick">
                    <canvas id="chart-area3" height="250"  width="400" ></canvas>
                </td>
            </tr>
            <tr>
                <td>
                    <canvas id="canvas" width="4" height="4" style="visibility:hidden"></canvas>
                </td>
            </tr>
        </table>
</div>
    <script type="text/javascript">
    // Codigo de configuracion de la grafica de Clientes
		var barChartData = {    
			labels: [<%= strEtiquetas %>],
			datasets: [{
				label: '<%= strlabel1 %>',
                type: 'line',
                fill: false,
				backgroundColor: window.chartColors.green,
				
				data: [<%=strdataset1 %> ]
			}, {
				label: '<%= strlabel2 %>',
				backgroundColor: window.chartColors.red,
				
                fill: false,
				data: [<%=strdataset2 %> ]
			}, {
				label: '<%= strlabel3 %>',
				backgroundColor: window.chartColors.green,
				
                fill: false,
				data: [<%=strdataset3 %> ]
			}, {
				label: '<%= strlabel4 %>',
				backgroundColor: window.chartColors.yellow,
				
                fill: false,
				data: [<%=strdataset4 %> ]
			}
            ]
		};

var config1 = {
        type: 'funnel',
        data: {
            datasets: [{
                data: [<%=strdataset11 %>],
                backgroundColor: [
                    "#36A2EB","#FFCE56", "#d80d21","#44dd60"
                ],
                hoverBackgroundColor: [
                    "#36A2EB","#FFCE56", "#d80d21","#44dd60"
                ]
            }],
            labels: [
                "<%= strlabel1 %>",
                "<%= strlabel2 %>",
                "<%= strlabel3 %>",
                "<%= strlabel4 %>"
            ]
        },
        options: {
            responsive: true,
            sort: 'desc',
            legend: {
                position: 'top'
            },
            title: {
                display: true,
                text: 'Visita Personal'
            },
            animation: {
                animateScale: true,
                animateRotate: true
            }
        }
    };
        
var config2 = {
        type: 'funnel',
        data: {
            datasets: [{
                data: [<%=strdataset22 %>],
                backgroundColor: [
                    "#36A2EB","#FFCE56", "#d80d21","#44dd60"
                ],
                hoverBackgroundColor: [
                    "#36A2EB","#FFCE56", "#d80d21","#44dd60"
                ]
            }],
            labels: [
                "<%= strlabel1 %>",
                "<%= strlabel2 %>",
                "<%= strlabel3 %>",
                "<%= strlabel4 %>"
            ]
        },
        options: {
            responsive: true,
            sort: 'desc',
            legend: {
                position: 'top'
            },
            title: {
                display: true,
                text: 'Llamada Telefonica'
            },
            animation: {
                animateScale: true,
                animateRotate: true
            }
        }
    };

    
var config3 = {
        type: 'funnel',
        data: {
            datasets: [{
                data: [<%=strdataset33 %>],
                backgroundColor: [
                    "#36A2EB","#FFCE56", "#d80d21","#44dd60"
                ],
                hoverBackgroundColor: [
                    "#36A2EB","#FFCE56", "#d80d21","#44dd60"
                ]
            }],
            labels: [
                "<%= strlabel1 %>",
                "<%= strlabel2 %>",
                "<%= strlabel3 %>",
                "<%= strlabel4 %>"
            ]
        },
        options: {
            responsive: true,
            sort: 'desc',
            legend: {
                position: 'top'
            },
            title: {
                display: true,
                text: 'Correo Electronico'
            },
            animation: {
                animateScale: true,
                animateRotate: true
            }
        }
    };

    		window.onload = function() {
			var ctx = document.getElementById('canvas').getContext('2d');
			window.myBar = new Chart(ctx, {
				type: 'bar',
				data: barChartData,
				options: {
					title: {
						display: true,
						text: 'Grafica de Avance de Captura'
					},
					tooltips: {
						mode: 'index',
						intersect: false
					},
					responsive: true,
					scales: {
						xAxes: [{
							stacked: false,
						}],
						yAxes: [{
							stacked: false
						}]
					}
				}
			});

            var ctx2 = document.getElementById("chart-area").getContext("2d");
            var myChart = new Chart(ctx2, config1);
            window.myDoughnut = myChart;

            var ctx3 = document.getElementById("chart-area2").getContext("2d");
            var myChart2 = new Chart(ctx3, config2)
            window.myDoughnut = myChart2;

            var ctx4 = document.getElementById("chart-area3").getContext("2d");
            var myChart3  = new Chart(ctx4, config3)
            window.myDoughnut = myChart3

            var canvas = document.getElementById("chart-area");
            canvas.onclick = function(evt){
            var activePoints = myChart.getElementsAtEvent(evt);
            var firstPoint = activePoints[0];
            var label = myChart.data.labels[firstPoint._index];
            var value = myChart.data.datasets[firstPoint._datasetIndex].data[firstPoint._index];
            document.location.href = "GraficoSecClientes.aspx?Tipo=1&Descri=" + label;
            };

            var canvas2 = document.getElementById("chart-area2");
            canvas2.onclick = function(evt){
            var activePoints = myChart2.getElementsAtEvent(evt);
            var firstPoint = activePoints[0];
            var label = myChart2.data.labels[firstPoint._index];
            var value = myChart2.data.datasets[firstPoint._datasetIndex].data[firstPoint._index];
            document.location.href = "GraficoSecClientes.aspx?Tipo=3&Descri=" + label;
            };
            
            var canvas3 = document.getElementById("chart-area3");
            canvas3.onclick = function(evt){
            var activePoints = myChart3.getElementsAtEvent(evt);
            var firstPoint = activePoints[0];
            var label = myChart3.data.labels[firstPoint._index];
            var value = myChart3.data.datasets[firstPoint._datasetIndex].data[firstPoint._index];
            document.location.href = "GraficoSecClientes.aspx?Tipo=2&Descri=" + label;
            };

        //////////////
		};

	</script>

    </form>
</body>
</html>
