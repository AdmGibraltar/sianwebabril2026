<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GraficoVentaSucursal.aspx.cs" Inherits="SIANWEB.GraficoVentaSucursal" %>

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
	        
	#chartjs-tooltip {
			opacity: 1;
			position: absolute;
			background: rgba(0, 0, 0, .7);
			color: white;
			border-radius: 3px;
			-webkit-transition: all .1s ease;
			transition: all .1s ease;
			pointer-events: none;
			-webkit-transform: translate(-50%, 0);
			transform: translate(-50%, 0);
		}

		.chartjs-tooltip-key {
			display: inline-block;
			width: 10px;
			height: 10px;
			margin-right: 10px;
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
                <td style=" background-color:#37aae8; font-variant:small-caps;"><b><a href='GraficaEmbudo.aspx'>Clientes</a></b></td>
                <td style=" width:10px">&nbsp;</td>
                <td style=" background-color:#FFFFFF; font-variant:small-caps;"><b><a href='GraficoVentas.aspx'>Venta Instalada</a></b></td>
                <td style=" width:10px">&nbsp;</td>
                <td style=" background-color:#FFFFFF; font-variant:small-caps;"><a href='#'>Venta Instalada Sucursal</a></td>
            </tr>
    </table>
        <table style="font-family: Verdana; font-size: 8pt; border-width:thick" >
            <tr><td>&nbsp;</td></tr>
            <tr>
                <td align="left" valign="top" style=" width:400px;">
                    <telerik:RadGrid ID="rgVentas" runat="server" AutoGenerateColumns="false" GridLines="Both"
                            PageSize="25" AllowPaging="true" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                            OnNeedDataSource="rgVentas_NeedDataSource" style=" z-index:10000" >
                            <MasterTableView DataKeyNames="Ordern" DataMember="listEmbudo">
                                <Columns>
                                    <telerik:GridBoundColumn HeaderText="Orden" UniqueName="Ordern" DataField="Ordern"
                                        Display="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Sucursal" UniqueName="RowDesc" DataField="RowDesc" Display="true">
                                        <HeaderStyle Width="150px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Vta Instalada" UniqueName="VtaIn" DataField="VtaIn" DataFormatString="{0:N2}">
                                        <HeaderStyle Width="80px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Vta Promedio" UniqueName="VtaPro" DataField="VtaPro" DataFormatString="{0:N2}">
                                        <HeaderStyle Width="80px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Vta Real" UniqueName="VtaRe" DataField="VtaRe" DataFormatString="{0:N2}">
                                        <HeaderStyle Width="80px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="% Vta Instalada" UniqueName="Por100VI" DataField="Por100VI" DataFormatString="{0:#%}">
                                        <HeaderStyle Width="80px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="% Vta Promedio" UniqueName="Por100Pro" DataField="Por100Pro" DataFormatString="{0:#%}">
                                        <HeaderStyle Width="80px" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td valign="top" style=" border-style:double; border-width:thick">
                    <canvas id="chart" width="400" height="400"></canvas>

                </td>
            </tr>
        </table>
</div>
    <script type="text/javascript">
    Chart.defaults.global.pointHitDetectionRadius = 1;

		var customTooltips = function(tooltip) {
			// Tooltip Element
			var tooltipEl = document.getElementById('chartjs-tooltip');

			if (!tooltipEl) {
				tooltipEl = document.createElement('div');
				tooltipEl.id = 'chartjs-tooltip';
				tooltipEl.innerHTML = '<table></table>';
				this._chart.canvas.parentNode.appendChild(tooltipEl);
			}

			// Hide if no tooltip
			if (tooltip.opacity === 0) {
				tooltipEl.style.opacity = 0;
				return;
			}

			// Set caret Position
			tooltipEl.classList.remove('above', 'below', 'no-transform');
			if (tooltip.yAlign) {
				tooltipEl.classList.add(tooltip.yAlign);
			} else {
				tooltipEl.classList.add('no-transform');
			}

			function getBody(bodyItem) {
				return bodyItem.lines;
			}

			// Set Text
			if (tooltip.body) {
				var titleLines = tooltip.title || [];
				var bodyLines = tooltip.body.map(getBody);

				var innerHtml = '<thead>';

				titleLines.forEach(function(title) {
					innerHtml += '<tr><th>' + title + '</th></tr>';
				});
				innerHtml += '</thead><tbody>';

				bodyLines.forEach(function(body, i) {
					var colors = tooltip.labelColors[i];
					var style = 'background:' + colors.backgroundColor;
					style += '; border-color:' + colors.borderColor;
					style += '; border-width: 2px';
					var span = '<span class="chartjs-tooltip-key" style="' + style + '"></span>';
					innerHtml += '<tr><td>' + span + body + '</td></tr>';
				});
				innerHtml += '</tbody>';

				var tableRoot = tooltipEl.querySelector('table');
				tableRoot.innerHTML = innerHtml;
			}

			var positionY = this._chart.canvas.offsetTop;
			var positionX = this._chart.canvas.offsetLeft;

			// Display, position, and set styles for font
			tooltipEl.style.opacity = 1;
			tooltipEl.style.left = positionX + tooltip.caretX + 'px';
			tooltipEl.style.top = positionY + tooltip.caretY + 'px';
			tooltipEl.style.fontFamily = tooltip._bodyFontFamily;
			tooltipEl.style.fontSize = tooltip.bodyFontSize + 'px';
			tooltipEl.style.fontStyle = tooltip._bodyFontStyle;
			tooltipEl.style.padding = tooltip.yPadding + 'px ' + tooltip.xPadding + 'px';
		};


    // Codigo de configuracion de la grafica de Clientes
		var barChartData = {    
			labels: [<%= strEtiquetas %>],
			datasets: [{
				label: <%= strlabel1 %>,          //  Venta Instalada
                fill: false,
				backgroundColor: window.chartColors.blue,
				
				data: [<%=strdataset1 %> ]
			}, {
				label: <%= strlabel2 %>,          //  Venta Promedio
				backgroundColor: window.chartColors.red,
				type: 'line',
                fill: false,
				data: [<%=strdataset2 %> ]
			}, {
				label: <%= strlabel3 %>,          //  Venta Real
				backgroundColor: window.chartColors.yellow,
                fill: false,
				data: [<%=strdataset3 %> ]
			}
            ]

		};

		window.onload = function() {
			var ctx = document.getElementById('chart').getContext('2d');
			window.myBar = new Chart(ctx, {
				type: 'bar',
				data: barChartData,
				options: {
					title: {
						display: true,
						text: 'Grafica de Cumplimiento de Venta Instalada'
					},
					tooltips: {
						enabled: false,
						mode: 'index',
						position: 'nearest',
						custom: customTooltips
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
		};

	</script>

    </form>
</body>
</html>
