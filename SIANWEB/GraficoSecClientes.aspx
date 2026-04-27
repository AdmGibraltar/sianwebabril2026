<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GraficoSecClientes.aspx.cs" Inherits="SIANWEB.GraficoSecClientes" %>

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
                <td style=" background-color:#37aae8; font-variant:small-caps;"><b><a href='GraficaEmbudo.aspx'>Clientes</a> <%=Request.Params["Descri"] %></b></td>
                <td style=" width:10px">&nbsp;</td>
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
                                    <telerik:GridBoundColumn HeaderText="IdCte" UniqueName="Ordern" DataField="Ordern"
                                        Display="true"><HeaderStyle Width="50px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Cliente" UniqueName="RowDesc" DataField="RowDesc" Display="true">
                                        <HeaderStyle Width="250px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Vta 3 Meses" UniqueName="VtaIn" DataField="VtaIn" DataFormatString="{0:N2}">
                                        <HeaderStyle Width="80px" />
                                    </telerik:GridBoundColumn>
                                 </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                </td>
            </tr>
            <tr><td>&nbsp;</td></tr>
            <tr>
                <td valign="top" style=" border-style:double; border-width:thick">
                    <canvas id="chart" width="800px" height="400px"></canvas>

                </td>
            </tr>
        </table>
</div>
    <script type="text/javascript">

    // Codigo de configuracion de la grafica de Clientes
		var barChartData = {    
			labels: [<%= strEtiquetas %>],
			datasets: [{
				label: <%= strlabel1 %>,          //  Venta Instalada
                fill: false,
				backgroundColor: window.chartColors.blue,
				
				data: [<%=strdataset1 %> ]
			}
            ]

		};

		window.onload = function() {
			var ctx = document.getElementById('chart').getContext('2d');
			window.myBar = new Chart(ctx, {
				type: 'line',
				data: barChartData,
				options: {
					title: {
						display: true,
						text: 'Grafica de Venta 3 Meses'
					},
					tooltips: {
						enabled: false,
						mode: 'index',
						position: 'nearest'
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
