<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/ReporteCom.Master"  EnableEventValidation="false" 
    AutoEventWireup="true" CodeBehind="dshIndicadoresACySv2.aspx.cs" Inherits="SIANWEB.Dashboard.dshIndicadoresACySv2" %>


<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <script src="js/jquery-3.3.1.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/jquery-template/jquery.loadTemplate.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.min.js")%>"></script>

    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css")%>" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/bootstrap-select.min.js") %>"></script>

    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/css/font-awesome.min.css")%>" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/icheck.min.js")%>"></script>
    
    <!-- Metro 4        ReporteCom      MasterPage01        
    <link rel="stylesheet" href="h ttps://cdn.metroui.org.ua/v4/css/metro-all.min.css"/>  
        -->
    <link  id='GoogleFontsLink' href='https://fonts.googleapis.com/css?family=Open Sans' rel='stylesheet' type='text/css'>
		<script>
            WebFontConfig = {
                google:
                    { families: ["Open Sans",] }, active: function () {
                        DrawTheChart(ChartData, ChartOptions, "chart-01", "HorizontalBar")
                    }
            };
		</script>
		<script asyn src="https://charts.livegap.com/js/webfont.js"></script>
        <script src="https://charts.livegap.com/js/Chart.min.js"></script>
        <script>
                function DrawTheChart(ChartData, ChartOptions, ChartId, ChartType) {
                    eval('var myLine = new Chart(document.getElementById(ChartId).getContext("2d")).' + ChartType + '(ChartData,ChartOptions);document.getElementById(ChartId).getContext("2d").stroke();')
                }
		</script>
        <script type="text/javascript">
            function ChkaTodos() {
                var chkBox = document.getElementById("<%=chkTodosRIKs.ClientID%>");
                var chkBoxList = document.getElementById("<%=lstchkRIKS.ClientID%>");

                var chkkValor = chkBox.checked;

                if (chkBoxList) {
                    checksDeLaLista = chkBoxList.getElementsByTagName("td");
                    var tgname = chkBoxList.getElementsByTagName("input");

                    for (i = 0; i < tgname.length; i++) {
                        if (tgname[i].type == 'checkbox') {

                            tgname[i].checked = chkkValor;
                        }
                    }

                }
                return false;
            }

            function UnChkaElTodos() {
                var chkBox = document.getElementById("<%=chkTodosRIKs.ClientID%>");
                var chkBoxList = document.getElementById("<%=lstchkRIKS.ClientID%>");

                if (chkBoxList) {
                    checksDeLaLista = chkBoxList.getElementsByTagName("td");
                    var tgname = chkBoxList.getElementsByTagName("input");

                    if (chkBox.checked) {
                        for (i = 0; i < tgname.length; i++) {
                            if (tgname[i].type == 'checkbox') {
                                if (tgname[i].checked == false) {
                                    chkBox.checked = false;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

        </script>

    <h2 style="font-weight: bolder">Tablero de Cumplimiento ACyS </h2>
    <table style=" width: 100%; border-spacing:10px; column-width:5px" class="table-condensed">
        <tr><td><asp:Label ID="lblMensaje" runat="server"></asp:Label></td></tr>
        <tr>
            <td id="headderTD">
                <table style=" width: 100%; border-spacing:5px; column-width:5px;  height: 100%;" >
                    <tr>
                        <td style="width:30%; border-spacing:5px;  vertical-align:top; border-bottom-width:1px; border-bottom-style:solid; border-bottom-color:#3d3d5c; ">
                            <table class="table-condensed">
                                <tr>
                                    <td style="vertical-align:top; border-bottom-width:1px; border-bottom-style:solid; border-bottom-color:#3d3d5c; " class="h3">
                                            &nbsp;<img src="../Images/imgAplicarFiltro.png" width="25" height="25" onclick="Actualiza();" style="cursor: pointer"/>&nbsp;Filtros&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style=" width: 100%; border-spacing:2px">
                                            <tr>
                                                <td><p class="h4 small"  id="parrCDI" runat="server" >CDI</p></td>
                                                <td>
                                                    <asp:DropDownList ID="drpCDI" runat="server" CssClass="h5 small"
                                                            OnSelectedIndexChanged="drpCDI_SelectedIndexChanged" Width="150px" AutoPostBack="true" >
                                                    </asp:DropDownList> 
                                                </td>
                                            </tr>
                                            <tr>
                                                <td><p class="h4 small">Representante:</p></td>
                                                <td><p class="h4 small">
                                                        <asp:checkbox name="chkTodosRIKs" id="chkTodosRIKs" runat="server"
                                                                onclick="ChkaTodos();" checked="true" />&nbsp;Todos los RIKS
                                                    </p>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:DropDownList ID="drpRIKs" runat="server" CssClass="h4 small"
                                                            Width="150px" AutoPostBack="true" OnSelectedIndexChanged="drpRIKs_SelectedIndexChanged" 
                                                            Visible="false" >
                                                    </asp:DropDownList>
                                                    <div style="height:100px; width:380px; overflow-y: scroll;">
                                                    <asp:CheckBoxList ID="lstchkRIKS" runat="server" onclick="UnChkaElTodos();"
                                                            CellPadding="5" Width="280px" Height="100px" TextAlign="Right" CssClass="h5" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr style="height:5px">
                                                <td><p class="h4 small">Estatus</p></td>
                                                <td>
                                                    <asp:DropDownList ID="drpEstatus" runat="server" CssClass="h4 small"
                                                        AutoPostBack="true" OnSelectedIndexChanged="drpEstatus_SelectedIndexChanged">
                                                        <asp:ListItem Value="" Text="-- TODOS --" Selected="True" />
                                                        <asp:ListItem Value="Capturado" Text="CAPTURADO" />
                                                        <asp:ListItem Value="Solicitado G" Text="SOLICITADO G" />
                                                        <asp:ListItem Value="Solicitado J" Text="SOLICITADO J" />
                                                        <asp:ListItem Value="Autorizado" Text="AUTORIZADO" />
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <!-- <asp:ListItem Value="5" Text="OTRO" /> -->
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width:10px;">&nbsp;</td>
                        <td style="width:54%; border-spacing:5px; vertical-align:top; border-bottom-width:1px; border-bottom-style:solid; border-bottom-color:#3d3d5c;  height: 100%;">
                            <table class="table-condensed" style="width:100%; column-width:10px; height: 100%;" >
                                <tr>
                                    <td class="h3" style="align-content:start; align-self:start; align-items:start; text-align:start;  border-bottom-width:1px; border-bottom-style:solid; border-bottom-color:#3d3d5c;" >
                                            &nbsp;<img src="../Images/imgBajarExcel.png" width="25" height="25" />&nbsp;Descargar Información&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style=" vertical-align:top; height: 100%;">
                                        <table style="width:100%; column-width:10px; height: 100%; vertical-align:top;" >
                                            <tr>
                                                <td style="width:10px">&nbsp;</td>
                                                <td style="width:40%; vertical-align:top;">
                                                    <table style="border-spacing:10px; height: 80%; vertical-align:top;">
                                                        <tr>
                                                            <td style="align-content:center ; align-self:center; align-items:center; text-align:center;vertical-align:top; border-spacing:10px;" >                                                                    
                                                                <asp:ImageButton runat="server" ID="imgRptAvance" ImageUrl="../Images/imgAvance01.png"  AlternateText="Reporte de Avance Detallado"
                                                                    OnClick="imgRptAvance_Click" width="30" height="30" style="vertical-align:top; "/> 
                                                            </td>
                                                            <td  style="align-content:start; align-self:start; align-items:start; text-align:start ;vertical-align:top; cursor: pointer;"
                                                                class="h4 text-nowrap" onclick="ClickAvance();" >
                                                                    &nbsp;Reporte de Avance Detallado&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td class="h4 small">
                                                                <asp:checkbox name="chkLista8020" id="chkLista8020" runat="server" 
                                                                    Text="" checked="true" />&nbsp;Listado de Clientes 80/20
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td class="h4 small">
                                                                <asp:checkbox name="chkListaMatriz" id="chkListaMatriz" runat="server" 
                                                                    Text=""  checked="true"/>&nbsp;ACyS Vigentes
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td class="h4 small" >
                                                                <asp:checkbox name="chkDetalleXRIK" id="chkDetalleXRIK" runat="server" 
                                                                    Text="" checked="false"/>&nbsp;Desglosar por RIK
                                                            </td>
                                                        </tr>
                                                        <tr><td>&nbsp;</td></tr>
                                                    </table>
                                                </td>
                                                <td style="width:5px;">&nbsp;</td>
                                                <td style="width:5px; border-left:solid thin #009CD9; ">&nbsp;</td>
                                                <td style="width:40%; vertical-align:top; ">
                                                    <table style="border-spacing:10px; height: 80%; vertical-align:top;">
                                                        <tr>
                                                            <td style="align-content:center ; align-self:center; align-items:center; text-align:center;vertical-align:top; border-spacing:10px;" >
                                                                <asp:ImageButton runat="server" ID="imgRptCumplimiento" ImageUrl="../Images/imgCumplimiento01.png"  AlternateText="Reporte de Cumplimiento en Captura"
                                                                    OnClick="imgRptCumplimiento_Click" width="30" height="30" style="vertical-align:top; "/>
                                                            </td>
                                                            <td style="align-content:start; align-self:start; align-items:start; text-align:start ;vertical-align:top; cursor: pointer;"
                                                                class="h4 text-nowrap" onclick="ClickCumplimiento();" >
                                                                    &nbsp;Cumplimiento en Captura&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td style="align-content:start; align-self:start; align-items:start; text-align:start;vertical-align:top; "
                                                                class="h4 small">
                                                                <asp:checkbox name="chkDesgloseEstatus" id="chkDesgloseEstatus" runat="server" 
                                                                    Text="" checked="true" />&nbsp;Desglose Por Estatus
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td class="h4 small" >
                                                                <asp:checkbox name="chkDetalleXRIKCumplimiento" id="chkDetalleXRIKCumplimiento" runat="server"
                                                                    Text="" checked="false"/>&nbsp;Desglosar por RIK
                                                            </td>
                                                        </tr>
                                                        <tr><td>&nbsp;</td></tr>
                                                    </table>
                                                </td>
                                                <td style="width:10px">&nbsp;
                                                    <div style="visibility:hidden">
                                                        <asp:Button ID="btnActualiza" runat="server" OnClick="btnActualiza_Click" />
                                                        <asp:Button ID="btnAvance" runat="server" OnClick="btnAvance_Click" />
                                                        <asp:Button ID="btnCumplimiento" runat="server" OnClick="btnCumplimiento_Click" />
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td id="middleTD" >
                <table style=" width: 100%; border-spacing:1px; column-width:1px"  class="table-condensed">
                    <tr>
                        <td style="width:32%; vertical-align:top;  align-content:start; text-align:start; align-items:start; align-self:start;">
                            <table class="table-condensed" style="border-spacing:1px; align-content:start; text-align:start; align-items:start; align-self:start;">
                                <!-- <tr><td colspan="2"><h5>&nbsp;&nbsp;</h5></td></tr> -->
                                <tr>
                                    <td class="h5 text-nowrap" style="align-content:end; text-align:end;">Total General De Acuerdos Capturados/Actualizados:</td>
                                    <td class="h5" style="align-content:end; align-self:end; align-items:end; text-align:end; vertical-align:top;">
                                        &nbsp;<strong><%=lblTotalGeneral %> </strong></td>
                                </tr>
                                <tr>
                                    <td class="h5 text-nowrap" style="align-content:end; text-align:end;">Total De Clientes Del 80/20:</td>
                                    <td class="h5" style="align-content:end; align-self:end; align-items:end; text-align:end; vertical-align:top;">
                                        &nbsp;<strong><%=lblTotalClientes %></strong></td>
                                </tr>
                                <tr>
                                    <td class="h5 text-nowrap" style="align-content:end; text-align:end;">Total De Captura De ACyS Del 80/20:</td>
                                    <td class="h5" style="align-content:end; align-self:end; align-items:end; text-align:end; vertical-align:top;">
                                        &nbsp;<strong><%=lblTotalCaptura %></strong></td>
                                </tr>
                                <tr>
                                    <td class="h5 text-nowrap" style="align-content:end; text-align:end;">Avances En Captura Del 80/20:</td>
                                    <td class="h4" style="align-content:end; align-self:end; align-items:end; text-align:end; vertical-align:top;">
                                        &nbsp;<strong><%=lblAvanceCaptura %></strong></td>
                                </tr>
                                <tr>
                                    <td class="h5 text-nowrap" style="align-content:end; text-align:end;">Total Capturado vs. Total Autorizaddo:</td>
                                    <td class="h4" style="align-content:end; align-self:end; align-items:end; text-align:end; vertical-align:top;">
                                        &nbsp;<strong><%=lblCapturavsAutorizado %></strong></td>
                                </tr>
                            </table>
                        </td>
                        <td style="width:10px;">&nbsp;</td>
                        <td style="width:52%; vertical-align:top;">
                            <table style="width:90%; border-spacing:5px; ">
                                <!-- <tr><td colspan="7"><h5>&nbsp;&nbsp;</h5></td></tr> -->
                                <tr>
                                    <td style="width:15px;">&nbsp;</td>
                                    <td >
                                        <table>
                                            <tr style='width:15px'>
                                                <td style="text-align:center; align-content:center; align-items:center; vertical-align: top" class="h4 small">
                                                    <strong> CAPTURADO </strong><br /><br />
                                                    <div style="text-align:center; align-content:center; align-items:center;"
                                                        id="donCapturado" data-role="donut" data-value="<%=NumCapturado%>"
                                                        data-hole=".5" data-stroke="#f5f5f5" data-fill="#009CD9" data-color="#FFFFFF"
                                                        data-animate="25" >
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:center; align-content:center; align-items:center;"
                                                    class="h4"><br />
                                                    <small><%=lblNumCapturado %></small><br /><b style=" color:#9C27B0"><%=NumCapturado %>%</b>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style='width:10px'>&nbsp;</td>
                                    <td >
                                        <table>
                                            <tr style='width:15px'>
                                                <td style="text-align:center; align-content:center; align-items:center; vertical-align: top" class="h4 small">
                                                    <strong> SOLICITADO G </strong><br /><br />
                                                    <div style="text-align:center; align-content:center; align-items:center;"
                                                        id="donSolicitaG" data-role="donut" data-value="<%=NumSolicitadoG%>"
                                                        data-hole=".5" data-stroke="#f5f5f5" data-fill="#009CD9" data-color="#FFFFFF"
                                                        data-animate="25" >
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:center; align-content:center; align-items:center;"
                                                    class="h4"><br />
                                                    <small> <%=lblNumSolicitadoG %></small><br /><b style=" color:#9C27B0"><%=NumSolicitadoG %>%</b>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style='width:10px'>&nbsp;</td>
                                    <td >
                                        <table>
                                            <tr style='width:15px'>
                                                <td style="text-align:center; align-content:center; align-items:center; vertical-align: top" class="h4 small">
                                                    <strong> SOLICITADO J </strong><br /><br />
                                                    <div style="text-align:center; align-content:center; align-items:center;"
                                                        id="donSolicitadoJ" data-role="donut" data-value="<%=NumSolicitadoJ%>"
                                                        data-hole=".5" data-stroke="#f5f5f5" data-fill="#009CD9" data-color="#FFFFFF"
                                                        data-animate="25" >
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:center; align-content:center; align-items:center;"
                                                    class="h4"><br />
                                                    <small><%=lblNumSolicitadoJ %></small><br /><b style=" color:#9C27B0"><%=NumSolicitadoJ %>%</b>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style='width:10px'>&nbsp;</td>
                                    <td >
                                        <table>
                                            <tr style='width:15px'>
                                                <td style="text-align:center; align-content:center; align-items:center; vertical-align: top" class="h4 small">
                                                    <strong> AUTORIZADO </strong><br /><br />
                                                    <div style="text-align:center; align-content:center; align-items:center;"
                                                        id="donAutorizado" data-role="donut" data-value="<%=NumAutorizado%>"
                                                        data-hole=".5" data-stroke="#f5f5f5" data-fill="#009CD9" data-color="#FFFFFF"
                                                        data-animate="25" >
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:center; align-content:center; align-items:center;"
                                                    class="h4"><br />
                                                    <small><%=lblNumAutorizado %></small><br /><b style=" color:#9C27B0"><%=NumAutorizado %>%</b>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style='width:10px'>&nbsp;</td>
                                    <td >
                                        <table>
                                            <tr style='width:15px'>
                                                <td style="text-align:center; align-content:center; align-items:center; vertical-align: top" class="h4 small">
                                                    <strong> OTRO </strong><br /><br />
                                                    <div style="text-align:center; align-content:center; align-items:center;"
                                                        id="donOtro" data-role="donut" data-value="<%=NumOtro %>"
                                                        data-hole=".5" data-stroke="#f5f5f5" data-fill="#009CD9" data-color="#FFFFFF"
                                                        data-animate="25" >
                                                    </div>
                                                    <!-- data-color="#9C27B0" -->
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:center; align-content:center; align-items:center;"
                                                    class="h4"><br />
                                                    <small><%=lblNumOtro %></small><br /><b style=" color:#9C27B0"><%=NumOtro %>%</b>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width:5px;">&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td id="graphTD" style="vertical-align:top; border-top-width:1px; border-top-style:solid; border-top-color:#3d3d5c; ">
                <table style="width: 100%; border-spacing:5px; column-width:5px" class="table-condensed" >
                    <tr>
                        <td style="vertical-align:top;  align-content:center; text-align:center; align-items:center; align-self:center;">
                            <canvas  id="chart-01" width="1000" height="550"
                                style="background-color:rgba(255,255,255,1.00);border-radius:0px;width:950px;
                                    height:550px;padding-left:20px;padding-right:10px;padding-top:0px;padding-bottom:0px">
                            </canvas>
                        </td>
                        <td style=" width:80px;vertical-align:top;  align-content:start; text-align:start; align-items:start; align-self:start;">
                            <asp:ImageButton runat="server" ID="btnExcelGrafica" ImageUrl="../Images/imgBajarExcel.png"  AlternateText="Descargar Acuerdos Por Representantes"
                                OnClick="btnExcelGrafica_Click" width="35" height="35" CssClass="rpImage"/>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <script src="https://cdn.metroui.org.ua/v4/js/metro.min.js"></script>
    <script>
        function ClickAvance() {
            var e = document.getElementById("<%=btnAvance.ClientID%>");
            e.click();
        }

        function ClickCumplimiento() {
            var e = document.getElementById("<%=btnCumplimiento.ClientID%>");
            e.click();
        }

        function Actualiza() {
            var e = document.getElementById("<%=btnActualiza.ClientID%>");
            e.click();
        }

        function Regresa() {
            location.href = "/<%=strURLBack%>";
        }
    </script>

    <script> 
        function MoreChartOptions() { }
        var ChartData = {
            labels: [ <%=strRIKS%>
                /*
                "Adrian Zafra",
                "Alexandra Saenz", 
                "Ariel Cuevas", 
                "Carlos Javier Santos",
                "Eliseo Pavon",
                "Esperanza Florez",
                "Fabian Acevedo", 
                "Jose Luis Martinez", 
                "Maribel Carrera",                
                "Teodoro Garzon",
                */
                ],
            datasets: [{
                fillColor: ['rgba(135,193,232,0.96)', 'rgba(5,140,62,1)', 'rgba(248,143,255,0.97)', 'rgba(11,216,219,0.95)', 'rgba(54,146,207,1)', 'rgba(237,26,51,0.95)', 'rgba(127,235,85,0.7)', 'rgba(97,18,97,0.95)', 'rgba(230,208,43,0.86)', 'rgba(219,91,17,0.78)',],
                strokeColor: ['rgba(52,152,219,0.5)', 'rgba(46,204,113,1)', 'rgba(167,132,181,1)', 'rgba(241,196,15,1)', 'rgba(78,169,230,1)', 'rgba(83,21,119,0.4)', 'rgba(135,242,92,1)', 'rgba(133,82,133,1)', 'rgba(176,196,167,0.2)', 'rgba(83,21,119,0.4)',],
                pointColor: "rgba(52,152,219,1)",
                markerShape: "circle",
                pointStrokeColor: "rgba(255,255,255,1.00)",
                data: [<%=strDatoRIKS%>
                    ///    21, 48, 28, 19, 96, 27, 99, 92, 68, 25,
                ],
                title: ""
            },]
        };

        ChartOptions = {
            decimalSeparator: ".", thousandSeparator: ",", spaceLeft: 12, spaceRight: 12, spaceTop: 12, spaceBottom: 12,
            ///     scaleLabel: "< %=value+''% >",
            ///      scaleLabel: "''",
            yAxisMinimumInterval: 1, scaleShowLabels: true, scaleShowLine: true, scaleLineStyle: "solid",
            scaleLineWidth: 1, scaleLineColor: "rgba(0,0,0,0.6)", scaleOverlay: true, scaleOverride: false, scaleSteps: 10,
            scaleStepWidth: 10, scaleStartValue: 0, inGraphDataShow: true,
            ///     inGraphDataTmpl: '< %=v3% >',
            inGraphDataFontFamily: "'Open Sans'", inGraphDataFontStyle: "normal bold", inGraphDataFontColor: "rgba(0,0,0,0.69)",
            inGraphDataFontSize: 16, inGraphDataPaddingX: 13, inGraphDataPaddingY: 0, inGraphDataAlign: "center", inGraphDataVAlign: "middle",
            inGraphDataXPosition: 3, inGraphDataYPosition: 2, inGraphDataAnglePosition: 2, inGraphDataRadiusPosition: 2, inGraphDataRotate: 0,
            inGraphDataPaddingAngle: 0, inGraphDataPaddingRadius: 0, inGraphDataBorders: false, inGraphDataBordersXSpace: 1,
            inGraphDataBordersYSpace: 1, inGraphDataBordersWidth: 1, inGraphDataBordersStyle: "solid", inGraphDataBordersColor: "rgba(0,0,0,1)",
            legend: true, maxLegendCols: 5, legendBlockSize: 15, legendFillColor: 'rgba(255,255,255,0.00)', legendColorIndicatorStrokeWidth: 1,
            legendPosX: -2, legendPosY: 4, legendXPadding: 0, legendYPadding: 0, legendBorders: false, legendBordersWidth: 1,
            legendBordersStyle: "solid", legendBordersColors: "rgba(102,102,102,1)", legendBordersSpaceBefore: 5, legendBordersSpaceLeft: 5,
            legendBordersSpaceRight: 5, legendBordersSpaceAfter: 5, legendSpaceBeforeText: 5, legendSpaceLeftText: 5, legendSpaceRightText: 5,
            legendSpaceAfterText: 5, legendSpaceBetweenBoxAndText: 5, legendSpaceBetweenTextHorizontal: 5, legendSpaceBetweenTextVertical: 5,
            legendFontFamily: "'Open Sans'", legendFontStyle: "normal normal", legendFontColor: "rgba(0,0,0,1)", legendFontSize: 17,
            showYAxisMin: false, rotateLabels: "smart", xAxisBottom: true, yAxisLeft: true, yAxisRight: false, graphTitleSpaceBefore: 5,
            graphTitleSpaceAfter: 5, graphTitleBorders: false, graphTitleBordersXSpace: 1, graphTitleBordersYSpace: 1, graphTitleBordersWidth: 1,
            graphTitleBordersStyle: "solid", graphTitleBordersColor: "rgba(0,0,0,1)", graphTitle: "Acuerdos Vigentes Por Representante",
            graphTitleFontFamily: "'Open Sans'", graphTitleFontStyle: "normal normal", graphTitleFontColor: "rgba(52,152,219,1)",
            graphTitleFontSize: 26, graphSubTitleSpaceBefore: 5, graphSubTitleSpaceAfter: 5, graphSubTitleBorders: false,
            graphSubTitleBordersXSpace: 1, graphSubTitleBordersYSpace: 1, graphSubTitleBordersWidth: 1, graphSubTitleBordersStyle: "solid",
            graphSubTitleBordersColor: "rgba(0,0,0,1)", graphSubTitle: "Mejores 5",
            graphSubTitleFontFamily: "'Open Sans'", graphSubTitleFontStyle: "normal normal", graphSubTitleFontColor: "rgba(102,102,102,1)",
            graphSubTitleFontSize: 16, scaleFontFamily: "'Open Sans'", scaleFontStyle: "normal normal", scaleFontColor: "rgba(0,0,0,1)",
            scaleFontSize: 14, pointLabelFontFamily: "'Open Sans'", pointLabelFontStyle: "normal normal", pointLabelFontColor: "rgba(102,102,102,1)",
            pointLabelFontSize: 16, angleShowLineOut: true, angleLineStyle: "solid", angleLineWidth: 1, angleLineColor: "rgba(0,0,0,0.1)",
            percentageInnerCutout: 50, scaleShowGridLines: true, scaleGridLineStyle: "solid", scaleGridLineWidth: 1,
            scaleGridLineColor: "rgba(0,0,0,0.22)", scaleXGridLinesStep: 2, scaleYGridLinesStep: 10, segmentShowStroke: true,
            segmentStrokeStyle: "solid", segmentStrokeWidth: 2, segmentStrokeColor: "rgba(255,255,255,1.00)", datasetStroke: true,
            datasetFill: true, datasetStrokeStyle: "solid", datasetStrokeWidth: 2, bezierCurve: true, bezierCurveTension: 0.4,
            pointDotStrokeStyle: "solid", pointDotStrokeWidth: 1, pointDotRadius: 3, pointDot: true, scaleTickSizeBottom: 5,
            scaleTickSizeTop: 5, scaleTickSizeLeft: 5, scaleTickSizeRight: 5, graphMin: 0, barShowStroke: true, barBorderRadius: 0,
            barStrokeStyle: "solid", barStrokeWidth: 0, barValueSpacing: 2, barDatasetSpacing: 5, scaleShowLabelBackdrop: true,
            scaleBackdropColor: 'rgba(255,255,255,0.75)', scaleBackdropPaddingX: 2, scaleBackdropPaddingY: 2, animationEasing: 'linear',
            animateRotate: true, animateScale: false, animationByDataset: false, animationLeftToRight: true, animationSteps: 300,
            animation: true, onAnimationComplete: function () { MoreChartOptions() }
        };
        DrawTheChart(ChartData, ChartOptions, "chart-01", "HorizontalBar");

    </script>

</asp:Content>
