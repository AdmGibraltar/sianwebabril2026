<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01_bootstrap.Master"
    AutoEventWireup="true" CodeBehind="ReporteGestionUtilidad.aspx.cs" Inherits="SIANWEB.ReporteGestionUtilidad" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <script src="js/jquery-3.3.1.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/jquery-template/jquery.loadTemplate.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.min.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/Chart.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/chartjs-plugin-colorschemes.js")%>"></script>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css")%>" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/bootstrap-select.min.js") %>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/Chart.PieceLabel.min.js")%>"></script>
    <link href="<%=Page.ResolveUrl("~/Librerias/Zebra_Datepicker-master/dist/css/bootstrap/zebra_datepicker.css")%>"
        rel="stylesheet" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/Zebra_Datepicker-master/dist/zebra_datepicker.src.js") %>"></script>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/css/font-awesome.min.css")%>" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/icheck.min.js")%>"></script>

    <link href="<%=Page.ResolveUrl("~/Librerias/timepicker/jquery.timepicker.css")%>"
        rel="stylesheet" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/timepicker/jquery.timepicker.min.js") %>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/timepicker/jquery.timepicker.js") %>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/chartjs-plugin-labels.js")%>"></script>
    <script type="text/javascript"> 
        $(document).on('click', '.panel-heading span.clickable', function (e) {
            var $this = $(this);
            if (!$this.hasClass('panel-collapsed')) {
                $this.parents('.panel').find('.panel-body').slideUp();
                $this.addClass('panel-collapsed');
                $this.find('i').removeClass('glyphicon-chevron-down').addClass('glyphicon-chevron-up');
            } else {
                $this.parents('.panel').find('.panel-body').slideDown();
                $this.removeClass('panel-collapsed');
                $this.find('i').removeClass('glyphicon-chevron-up').addClass('glyphicon-chevron-down');
            }
        })
        function ModalUpload() {
            document.getElementById('<%=iFrameCargar.ClientID%>').src = "CargarArchivos.aspx?tipo=5"
            $('#ModalUpload').modal('hide');
            $("#ModalUpload").appendTo("body");
            $("#ModalUpload").modal({ backdrop: 'static', keyboard: false });
            $('#ModalUpload').modal('show');
        }
    </script>
    <style type="text/css">
        form-control {
            width: 100%;
            height: 34px !important;
            padding: 6px 12px !important;
        }
        .dxbs-footer-row {
            background-color: lightcyan;
            font-weight: bold;
        }
        .list-group {
            height: 212px !Important;
        }
        .dropdown-toggle {
            height: 34px !important;
        }
        .dropdown-toggle-date {
            height: 30px !important;
            margin-top: -6px;
            padding-left: 12px;
            padding-right: 10px;
            margin-right: -13px;
        }
        .panel-success > .panel-heading {
            color: #F9F9F9 !important;
            background-color: #59b2f1 !important;
        }
        .panel-success {
            border-color: #d1d1d1 !important;
        }
        .caret {
            margin-top: 10px !important;
        }
        .row {
            margin-top: 40px;
            padding: 0 10px;
        }
        .clickable {
            cursor: pointer;
        }
        .panel-heading span {
            margin-top: -20px;
            font-size: 15px;
        }
        #wizHeader li .prevStep {
            background-color: #26617f;
        }
            #wizHeader li .prevStep:after {
                border-left-color: #26617f !important;
            }
        #wizHeader li .currentStep {
            background-color: #39a5dc;
        }
            #wizHeader li .currentStep:after {
                border-left-color: #39a5dc !important;
            }
        #wizHeader li .nextStep {
            background-color: #C2C2C2;
        }
            #wizHeader li .nextStep:after {
                border-left-color: #C2C2C2 !important;
            }
        #wizHeader {
            list-style: none;
            overflow: hidden;
            font: 18px Helvetica, Arial, Sans-Serif;
            margin: 0px;
            padding: 0px;
        }
            #wizHeader li {
                float: left;
            }
                #wizHeader li a {
                    color: white;
                    text-decoration: none;
                    padding: 10px 0 10px 55px;
                    background: brown; /* fallback color */
                    background: hsla(34,85%,35%,1);
                    position: relative;
                    display: block;
                    float: left;
                }
                    #wizHeader li a:after {
                        content: " ";
                        display: block;
                        width: 0;
                        height: 0;
                        border-top: 50px solid transparent; /* Go big on the size, and let overflow hide */
                        border-bottom: 50px solid transparent;
                        border-left: 30px solid hsla(34,85%,35%,1);
                        position: absolute;
                        top: 50%;
                        margin-top: -50px;
                        left: 100%;
                        z-index: 2;
                    }
                    #wizHeader li a:before {
                        content: " ";
                        display: block;
                        width: 0;
                        height: 0;
                        border-top: 50px solid transparent;
                        border-bottom: 50px solid transparent;
                        border-left: 30px solid white;
                        position: absolute;
                        top: 50%;
                        margin-top: -50px;
                        margin-left: 1px;
                        left: 100%;
                        z-index: 1;
                    }
                #wizHeader li:first-child a {
                    padding-left: 10px;
                }
                #wizHeader li:last-child {
                    padding-right: 50px;
                }
                #wizHeader li a:hover {
                    background: #FE9400;
                }
                    #wizHeader li a:hover:after {
                        border-left-color: #FE9400 !important;
                    }
        .content {
            height: 180px;
        }
        .boxes {
            height: 100px;
            overflow: auto;
            width: 300px;
        }
        .boxes2 {
            height: 200px;
            overflow: auto;
            width: 300px;
        }
        .checkbox, .radio {
            margin-top: 2px !important;
            margin-bottom: 10px !important;
        }
        .content2 {
            height: 310px;
            overflow: auto;
        }
        .form-control2 {
            display: block !important;
            width: 100% !important;
            height: 40px !important;
            padding: 0px 0px !important;
            line-height: 1.42857143 !important;
            color: #555 !important;
            background-color: #fff !important;
            background-image: none !important;
            border: 1px solid #ccc !important;
            -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075) !important;
            box-shadow: inset 0 1px 1px rgba(0,0,0,.075) !important;
            -webkit-transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s !important;
            -o-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s !important;
            transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s !important;
        }
        .centerText {
            text-align: center;
        }
    </style>
    <div class="container-fluid">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/load.gif" AlternateText="Loading ..."
                        ToolTip="Loading ..." Style="position: fixed; top: 45%; left: 40%;" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
       

           <div class="col-md-12">
            <h2 style="font-weight: bolder">Reportes de Gestión de Utilidad </h2>
        </div>
        <div class="col-md-12" style="margin-top: 15px;">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <h3 class="panel-title">Filtro(s)</h3>
                    <span class="pull-right clickable"><i class="glyphicon glyphicon-chevron-up"></i>
                    </span>
                </div>
                <div class="panel-body">
                    <div class="col-md-12">
                        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                            <ContentTemplate>
                                <div class="col-md-12" style="margin-top: 5px">
                                    <div class="col-md-5" style="margin-top: 5px">
                                        <div class="form-group">
                                            <div class="col-md-4">
                                                <asp:Label ID="Label3" runat="server" Text="Sucursal" />
                                            </div>
                                            <div class="col-md-8">
                                                <dx:BootstrapComboBox ID="CmbSucursal" runat="server" ClientInstanceName="SucursalOT"  Enabled="false" ReadOnly="true">
                                                </dx:BootstrapComboBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-5" style="margin-top: 5px;">
                                        <div class="form-group">
                                            <div class="col-md-4">
                                                <asp:Label ID="Label4" runat="server" Text="Representante" />
                                            </div>
                                            <div class="col-md-8">
                                                <dx:BootstrapComboBox ID="cmbRikDI" runat="server" ClientInstanceName="RikOt">
                                                </dx:BootstrapComboBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12" style="margin-top: 5px">
                                    <div class="col-md-12" style="margin-top: 5px">
                                        Selecciones Tipos de reporte a generar:
                                    </div>
                                    <div class="col-md-4" style="margin-top: 5px">
                                        <div class="col-md-12" style="margin-top: 5px">
                                            <dx:BootstrapRadioButtonList ID="bcmTipoReporte" ClientInstanceName="tipoSucursalvsSucursal" OnSelectedIndexChanged="bcmTipoReporte_SelectedIndexChanged" AutoPostBack="true" runat="server" RepeatColumns="1">
                                                <Items>
                                                    <dx:BootstrapListEditItem Value="1" Text="Mes Actual" Selected="true"></dx:BootstrapListEditItem>
                                                    <dx:BootstrapListEditItem Value="2" Text="3 Meses Anteriores (Meses Cerrados)"></dx:BootstrapListEditItem>
                                                    <dx:BootstrapListEditItem Value="3" Text="Mes a Seleccionar"></dx:BootstrapListEditItem>
                                                    <dx:BootstrapListEditItem Value="4" Text="Trimestre a Seleccionar"></dx:BootstrapListEditItem>
                                                </Items>
                                            </dx:BootstrapRadioButtonList>
                                        </div>
                                        <div class="col-md-12" style="margin-top: 5px">
                                            <asp:Label runat="server" ID="lblFecha" Text=""></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-6" style="margin-top: 5px;">
                                        <div class="col-md-12" style="margin-top: 5px;">
                                            <div class="col-md-6" style="margin-top: 5px">
                                                <div class="form-group">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label1" runat="server" Text="Mes a Seleccionar" />
                                                    </div>
                                                    <div class="col-md-8">
                                                        <dx:ASPxDateEdit runat="server" Enabled="false" CssClass="form-control" ClientInstanceName="Fechafinal" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="txtfechaIniciaGR" PickerType="Months">
                                                            <CalendarProperties>
                                                                <FastNavProperties InitialView="Years" MaxView="Years" />
                                                            </CalendarProperties>
                                                        </dx:ASPxDateEdit>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12" style="margin-top: 5px;">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label13" runat="server" Text="Trimestre" />
                                                    </div>
                                                    <div class="col-md-8">
                                                        <dx:BootstrapComboBox ID="BcbTrimestreInicial" runat="server">
                                                        </dx:BootstrapComboBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label32" runat="server" Text="Año" />
                                                    </div>
                                                    <div class="col-md-8">
                                                        <dx:ASPxDateEdit runat="server" CssClass="form-control" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="txtfechaTrimestreInicial" PickerType="Years">
                                                            <CalendarProperties>
                                                                <FastNavProperties InitialView="Years" MaxView="Years" />
                                                            </CalendarProperties>
                                                        </dx:ASPxDateEdit>
                                                    </div>
                                                </div>
                                            </div>
                                        </div> 
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
        <%-- Reporte Global --%>
        <asp:UpdatePanel runat="server" ID="upanelGlobal" UpdateMode="Always">
            <ContentTemplate>
                <div class="col-md-12" style="margin-top: 15px;">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h3 class="panel-title">Reporte Global</h3> 
                        </div>
                        <div class="panel-body">
                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="col-md-6">
                                </div> 
                                <div class="col-md-6">
                                    <button id="btnclick" type="button" class="btn btn-primary" title="Consultar Reporte"
                                        runat="server" onserverclick="FinishButtonClick">
                                        <span>Consultar Reporte</span>
                                    </button>
                                    <button id="btnBucaPVV" type="button" class="btn btn-primary" title="Gráfica Presupuesto vs Venta"
                                        runat="server" onclick="BtnPreVsVta()">
                                        <span>Gráfica Presupuesto vs Venta</span>
                                    </button>
                                    <button type="submit" runat="server" class="btn btn-defausm" id="btnImprimir"
                                        visible="true" onserverclick="btnImprimir_ServerClick">
                                        <i class="fa fa-print" aria-hidden="true"></i>&nbsp;Exportar a Excel
                                    </button>
                                </div>
                            </div>
                            <div runat="server" id="Global" visible="false"> 
                            <div class="col-md-12" style="margin-top: 5px;">
                                <dx:BootstrapGridView ID="gridRepresentanteMP" runat="server" KeyFieldName="id_rik"
                                    Width="100%" AutoGenerateColumns="False">
                                    <Columns>
                                        <dx:BootstrapGridViewTextColumn FieldName="Cuenta" CssClasses-HeaderCell="centerText" Caption="Cuenta">
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="totalVenta" CssClasses-HeaderCell="centerText" Caption="Vta Real">
                                            <PropertiesTextEdit DisplayFormatString="c0" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="TotalPresupuesto" CssClasses-HeaderCell="centerText" Caption="Vta Ptto">
                                            <PropertiesTextEdit DisplayFormatString="c0" />
                                        </dx:BootstrapGridViewTextColumn>
                                             <dx:BootstrapGridViewTextColumn FieldName="difVta" CssClasses-HeaderCell="centerText" Caption="Δ Vta">
                                            <PropertiesTextEdit DisplayFormatString="c0" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="UtilidadPrima" CssClasses-HeaderCell="centerText" Caption="UP Real">
                                            <PropertiesTextEdit DisplayFormatString="c0" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="UtilidadPrimaPres" CssClasses-HeaderCell="centerText" Caption="UP Ptto">
                                            <PropertiesTextEdit DisplayFormatString="c0" />
                                        </dx:BootstrapGridViewTextColumn>
                                           <dx:BootstrapGridViewTextColumn FieldName="difup" CssClasses-HeaderCell="centerText" Caption="Δ Up">
                                            <PropertiesTextEdit DisplayFormatString="c0" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="UtilidadPrimaPorc" CssClasses-HeaderCell="centerText" Caption=" % UP Real">
                                            <PropertiesTextEdit DisplayFormatString="p" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="UtilidadPrimaPresPorc" CssClasses-HeaderCell="centerText" Caption="% UP Ptto">
                                            <PropertiesTextEdit DisplayFormatString="p" />
                                        </dx:BootstrapGridViewTextColumn>
                                            <dx:BootstrapGridViewTextColumn FieldName="difpercup" CssClasses-HeaderCell="centerText" Caption="Δ % Up">
                                            <PropertiesTextEdit DisplayFormatString="p" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="VNR" CssClasses-HeaderCell="centerText" Width="100" Caption="VNR (3m)">
                                            <PropertiesTextEdit DisplayFormatString="c0" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="VNRPorc" CssClasses-HeaderCell="centerText" Caption="% VNR (3m)">
                                            <PropertiesTextEdit DisplayFormatString="p" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="VNRpres" CssClasses-HeaderCell="centerText" Caption="% VNR Ptto">
                                            <PropertiesTextEdit DisplayFormatString="p" />
                                        </dx:BootstrapGridViewTextColumn>
                                    </Columns>
                                </dx:BootstrapGridView>
                            </div>
                            <div class="col-md-12">
                                <canvas id="myChartPreVsVta" width="1500" height="500"></canvas>
                            </div>
                                </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnImprimir" />
                <asp:PostBackTrigger ControlID="btnimprimirdetalle" />
                <asp:PostBackTrigger ControlID="btnCategoria" />
                <asp:AsyncPostBackTrigger ControlID="BtnEstadisticaDetallada" />
                <asp:AsyncPostBackTrigger ControlID="btnclick" />
                <asp:AsyncPostBackTrigger ControlID="btnReporteCategoria" />
                <asp:AsyncPostBackTrigger ControlID="btnReporteCiente" />
                <asp:PostBackTrigger ControlID="grDetalle" />
            </Triggers>
        </asp:UpdatePanel>
        <%-- Reporte Categoria --%>
        <asp:UpdatePanel runat="server" ID="uppanleCatgoria" UpdateMode="Always">
            <ContentTemplate>
                <div class="col-md-12" style="margin-top: 15px;">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h3 class="panel-title">Reporte de Ventas: Categoria</h3> 
                        </div>
                        <div class="panel-body">
                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="col-md-6">
                                </div>
                                <div class="col-md-6">
                                    <button id="btnReporteCategoria" type="button" class="btn btn-primary" title="Consultar Reporte"
                                        runat="server" onserverclick="btnReporteCategoria_ServerClick">
                                        <span>Consultar Reporte</span>
                                    </button> 
                                    <button id="BtnEstadisticaDetallada" type="button" class="btn btn-primary" title="Grafica"
                                        runat="server" onclick="BtnEstadisticaDetallada()">
                                        <span>Gráfica Categoria</span>
                                    </button> 
                                    <button type="submit" runat="server" class="btn btn-default" id="btnCategoria"
                                        visible="true" onserverclick="btnCategoria_ServerClick">
                                        <i class="fa fa-print" aria-hidden="true"></i>&nbsp;Exportar a Excel
                                    </button>
                                </div>
                            </div>
                            <div runat="server" id="categoria" visible="false"> 
                            <div class="col-md-12" style="margin-top: 5px;">
                                <dx:BootstrapGridView ID="grdCategoria" ClientInstanceName="gridCategoria" runat="server" KeyFieldName="id_rik" OnCustomSummaryCalculate="grdCategoria_CustomSummaryCalculate"
                                    Width="100%" AutoGenerateColumns="False">
                                    <Settings ShowFooter="True" />
                                     <Settings HorizontalScrollBarMode="Auto" />
                                    <Columns>
                                        <dx:BootstrapGridViewTextColumn FieldName="Id_Cpr" Caption="id_CPR" CssClasses-HeaderCell="centerText" Visible="false">
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="Cpr_Descripcion" CssClasses-HeaderCell="centerText" Caption="Categoria" Width="120" Fixed="true">
                                        </dx:BootstrapGridViewTextColumn>
                                            <dx:BootstrapGridViewBandColumn Caption="Total General" CssClasses-HeaderCell="centerText">
                                            <Columns>
                                                <dx:BootstrapGridViewTextColumn FieldName="venta" CssClasses-HeaderCell="centerText" Caption="Vta" Width="80">
                                                    <PropertiesTextEdit DisplayFormatString="c0" />
                                                </dx:BootstrapGridViewTextColumn>
                                                 <dx:BootstrapGridViewTextColumn FieldName="Presupuestoventa" CssClasses-HeaderCell="centerText" Caption="Vta Ptto" Width="80">
                                                    <PropertiesTextEdit DisplayFormatString="c0" />
                                                </dx:BootstrapGridViewTextColumn>
                                                <dx:BootstrapGridViewTextColumn FieldName="utilidadBruta" CssClasses-HeaderCell="centerText" Caption="UP" Width="80">
                                                    <PropertiesTextEdit DisplayFormatString="c0" />
                                                </dx:BootstrapGridViewTextColumn>
                                                 <dx:BootstrapGridViewTextColumn FieldName="PresupuestoutilidadBruta" CssClasses-HeaderCell="centerText" Caption="UP Ptto" Width="80">
                                                    <PropertiesTextEdit DisplayFormatString="c0" />
                                                </dx:BootstrapGridViewTextColumn>
                                                <dx:BootstrapGridViewTextColumn FieldName="porcubreal" CssClasses-HeaderCell="centerText" Caption="% UP" Width="80">
                                                    <PropertiesTextEdit DisplayFormatString="p" />
                                                </dx:BootstrapGridViewTextColumn>
                                                <dx:BootstrapGridViewTextColumn FieldName="Presupuestoporcubreal" CssClasses-HeaderCell="centerText" Caption="% UP Ptto" Width="80">
                                                    <PropertiesTextEdit DisplayFormatString="p" />
                                                </dx:BootstrapGridViewTextColumn>
                                                <dx:BootstrapGridViewTextColumn FieldName="Mezcla" CssClasses-HeaderCell="centerText" Caption="Mezcla Vta" Width="80">
                                                    <PropertiesTextEdit DisplayFormatString="p" />
                                                </dx:BootstrapGridViewTextColumn>
                                                <dx:BootstrapGridViewTextColumn FieldName="MezclaUP" CssClasses-HeaderCell="centerText" Caption="Mezcla % UP" Width="80">
                                                    <PropertiesTextEdit DisplayFormatString="p" />
                                                </dx:BootstrapGridViewTextColumn>
                                            </Columns>
                                        </dx:BootstrapGridViewBandColumn>
                                         <dx:BootstrapGridViewBandColumn Caption="Cuentas Locales" CssClasses-HeaderCell="centerText">
                                            <Columns>
                                                <dx:BootstrapGridViewTextColumn FieldName="ventaLocal" CssClasses-HeaderCell="centerText" Caption="Vta" Width="80">
                                                    <PropertiesTextEdit DisplayFormatString="c0" />
                                                </dx:BootstrapGridViewTextColumn>
                                                 <dx:BootstrapGridViewTextColumn FieldName="PresupuestoventaLocal" CssClasses-HeaderCell="centerText" Caption="Vta Ptto"  Width="80">
                                                    <PropertiesTextEdit DisplayFormatString="c0" />
                                                </dx:BootstrapGridViewTextColumn>
                                                <dx:BootstrapGridViewTextColumn FieldName="utilidadBrutaLocal" CssClasses-HeaderCell="centerText" Caption="UP" Width="80">
                                                    <PropertiesTextEdit DisplayFormatString="c0" />
                                                </dx:BootstrapGridViewTextColumn>
                                                 <dx:BootstrapGridViewTextColumn FieldName="PresupuestoutilidadBrutaLocal" CssClasses-HeaderCell="centerText" Caption="UP Ptto" Width="80">
                                                    <PropertiesTextEdit DisplayFormatString="c0" />
                                                </dx:BootstrapGridViewTextColumn>
                                                <dx:BootstrapGridViewTextColumn FieldName="porcubrealLocal" CssClasses-HeaderCell="centerText" Caption="% UP" Width="80">
                                                    <PropertiesTextEdit DisplayFormatString="p" />
                                                </dx:BootstrapGridViewTextColumn>
                                                  <dx:BootstrapGridViewTextColumn FieldName="PresupuestoporcubrealLocal" CssClasses-HeaderCell="centerText" Caption="% UP Ptto" Width="80">
                                                    <PropertiesTextEdit DisplayFormatString="p" />
                                                </dx:BootstrapGridViewTextColumn>
                                                <dx:BootstrapGridViewTextColumn FieldName="MezclaLocal" CssClasses-HeaderCell="centerText" Caption="Mezcla Vta" Width="80">
                                                    <PropertiesTextEdit DisplayFormatString="p" />
                                                </dx:BootstrapGridViewTextColumn>
                                                <dx:BootstrapGridViewTextColumn FieldName="MezclaUPLocal" CssClasses-HeaderCell="centerText" Caption="Mezcla % UP" Width="80">
                                                    <PropertiesTextEdit DisplayFormatString="p" />
                                                </dx:BootstrapGridViewTextColumn>
                                            </Columns>
                                        </dx:BootstrapGridViewBandColumn>
                                        <dx:BootstrapGridViewBandColumn Caption="Cuentas Nacionales" CssClasses-HeaderCell="centerText">
                                            <Columns>
                                                <dx:BootstrapGridViewTextColumn FieldName="ventaNacional" CssClasses-HeaderCell="centerText" Caption="Vta" Width="80">
                                                    <PropertiesTextEdit DisplayFormatString="c0" />
                                                </dx:BootstrapGridViewTextColumn>
                                                <dx:BootstrapGridViewTextColumn FieldName="PresupuestoventaNacional" CssClasses-HeaderCell="centerText" Caption="Venta Ptto" Width="80">
                                                    <PropertiesTextEdit DisplayFormatString="c0" />
                                                </dx:BootstrapGridViewTextColumn>
                                                <dx:BootstrapGridViewTextColumn FieldName="utilidadBrutaNacional" CssClasses-HeaderCell="centerText" Caption="UP" Width="80">
                                                    <PropertiesTextEdit DisplayFormatString="c0" />
                                                </dx:BootstrapGridViewTextColumn>
                                                  <dx:BootstrapGridViewTextColumn FieldName="PresupuestoutilidadBrutaNacional" CssClasses-HeaderCell="centerText" Caption="UP Ptto" Width="80">
                                                    <PropertiesTextEdit DisplayFormatString="c0" />
                                                </dx:BootstrapGridViewTextColumn>
                                                <dx:BootstrapGridViewTextColumn FieldName="porcubrealNacional" CssClasses-HeaderCell="centerText" Caption="% UP" Width="80">
                                                    <PropertiesTextEdit DisplayFormatString="p" />
                                                </dx:BootstrapGridViewTextColumn>
                                                  <dx:BootstrapGridViewTextColumn FieldName="PresupuestoporcubrealNacional" CssClasses-HeaderCell="centerText" Caption="% UP Ptto" Width="80">
                                                    <PropertiesTextEdit DisplayFormatString="p" />
                                                </dx:BootstrapGridViewTextColumn>
                                                <dx:BootstrapGridViewTextColumn FieldName="MezclaNacional" CssClasses-HeaderCell="centerText" Caption="Mezcla Vta" Width="80">
                                                    <PropertiesTextEdit DisplayFormatString="p" />
                                                </dx:BootstrapGridViewTextColumn>
                                                <dx:BootstrapGridViewTextColumn FieldName="MezclaUPNacional" CssClasses-HeaderCell="centerText" Caption="Mezcla % UP" Width="80">
                                                    <PropertiesTextEdit DisplayFormatString="p" />
                                                </dx:BootstrapGridViewTextColumn> 
                                            </Columns>
                                        </dx:BootstrapGridViewBandColumn>
                                        
                                     
                                    </Columns>
                                    <TotalSummary>
                                        <dx:ASPxSummaryItem FieldName="venta" SummaryType="Sum" DisplayFormat="c0" />
                                        <dx:ASPxSummaryItem FieldName="utilidadBruta" SummaryType="Sum" DisplayFormat="c0" />
                                        <%--<dx:ASPxSummaryItem FieldName="porcubreal" SummaryType="Average" DisplayFormat="p" />--%>
                                        <dx:ASPxSummaryItem FieldName="porcubreal" ShowInColumn="porcubreal" Tag="porcubreal" SummaryType="Custom" DisplayFormat="p" />
                                        <dx:ASPxSummaryItem FieldName="Mezcla" SummaryType="Sum" DisplayFormat="p" />
                                        <dx:ASPxSummaryItem FieldName="MezclaUP" SummaryType="Sum" DisplayFormat="p" />

                                        <dx:ASPxSummaryItem FieldName="ventaNacional" SummaryType="Sum" DisplayFormat="c0" />
                                        <dx:ASPxSummaryItem FieldName="utilidadBrutaNacional" SummaryType="Sum" DisplayFormat="c0" />
                                        <%--<dx:ASPxSummaryItem FieldName="porcubreal" SummaryType="Average" DisplayFormat="p" />--%>
                                        <dx:ASPxSummaryItem FieldName="porcubrealNacional" ShowInColumn="porcubrealNacional" Tag="porcubrealNacional" SummaryType="Custom" DisplayFormat="p" />
                                        <dx:ASPxSummaryItem FieldName="MezclaNacional" SummaryType="Sum" DisplayFormat="p" />
                                        <dx:ASPxSummaryItem FieldName="MezclaUPNacional" SummaryType="Sum" DisplayFormat="p" />
                                        <dx:ASPxSummaryItem FieldName="ventaLocal" SummaryType="Sum" DisplayFormat="c0" />
                                        <dx:ASPxSummaryItem FieldName="utilidadBrutaLocal" SummaryType="Sum" DisplayFormat="c0" />
                                        <%--<dx:ASPxSummaryItem FieldName="porcubreal" SummaryType="Average" DisplayFormat="p" />--%>
                                        <dx:ASPxSummaryItem FieldName="porcubrealLocal" ShowInColumn="porcubrealLocal" Tag="porcubrealLocal" SummaryType="Custom" DisplayFormat="p" />
                                        <dx:ASPxSummaryItem FieldName="MezclaLocal" SummaryType="Sum" DisplayFormat="p" />
                                        <dx:ASPxSummaryItem FieldName="MezclaUPLocal" SummaryType="Sum" DisplayFormat="p" />

                                        <dx:ASPxSummaryItem FieldName="PresupuestoventaNacional" SummaryType="Sum" DisplayFormat="c0" />
                                        <dx:ASPxSummaryItem FieldName="PresupuestoutilidadBrutaNacional" SummaryType="Sum" DisplayFormat="c0" />
                                        <dx:ASPxSummaryItem FieldName="PresupuestoventaLocal" SummaryType="Sum" DisplayFormat="c0" />
                                        <dx:ASPxSummaryItem FieldName="PresupuestoutilidadBrutaLocal" SummaryType="Sum" DisplayFormat="c0" />
                                        <dx:ASPxSummaryItem FieldName="Presupuestoventa" SummaryType="Sum" DisplayFormat="c0" />
                                        <dx:ASPxSummaryItem FieldName="PresupuestoutilidadBruta" SummaryType="Sum" DisplayFormat="c0" />

                                         <dx:ASPxSummaryItem FieldName="PresupuestoporcubrealNacional" ShowInColumn="PresupuestoporcubrealNacional" Tag="PresupuestoporcubrealNacional" SummaryType="Custom" DisplayFormat="p" />
                                         <dx:ASPxSummaryItem FieldName="PresupuestoporcubrealLocal" ShowInColumn="PresupuestoporcubrealLocal" Tag="PresupuestoporcubrealLocal" SummaryType="Custom" DisplayFormat="p" />
                                         <dx:ASPxSummaryItem FieldName="Presupuestoporcubreal" ShowInColumn="Presupuestoporcubreal" Tag="Presupuestoporcubreal" SummaryType="Custom" DisplayFormat="p" />

                                    </TotalSummary>
                                </dx:BootstrapGridView>
                            </div>
                            <div class="col-md-12" style="margin-top: 5px">
                                <div class="col-md-6">
                                    <div style="float: right">
                                        <span id="total" runat="server"></span>
                                    </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <canvas id="myChart" style="width: 250px; height: 150px"></canvas>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div style="float: right">
                                        <span id="total2" runat="server"></span>
                                    </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <canvas id="myChartpieup" style="width: 250px; height: 150px"></canvas>
                                    </div>
                                </div>
                            </div>
                                </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnImprimir" />
                <asp:PostBackTrigger ControlID="btnimprimirdetalle" />
                <asp:PostBackTrigger ControlID="btnCategoria" />
                <asp:AsyncPostBackTrigger ControlID="BtnEstadisticaDetallada" />
                <asp:AsyncPostBackTrigger ControlID="btnclick" />
                <asp:AsyncPostBackTrigger ControlID="btnReporteCategoria" />
                <asp:AsyncPostBackTrigger ControlID="btnReporteCiente" />
                <asp:PostBackTrigger ControlID="grDetalle" />
            </Triggers>
        </asp:UpdatePanel>
        <%-- Reporte Detallado --%>
        <asp:UpdatePanel runat="server" ID="uppaneldetallado" UpdateMode="Always">
            <ContentTemplate>
                <div class="col-md-12" style="margin-top: 15px;">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h3 class="panel-title">Reporte de ventas: Cliente</h3> 
                        </div>
                        <div class="panel-body">
                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="col-md-6">
                                </div>
                                <div class="col-md-6">
                                    <button id="btnReporteCiente" type="button" class="btn btn-primary" title="Consultar Reporte"
                                        runat="server" onserverclick="btnReporteCiente_ServerClick">
                                        <span>Consultar Reporte</span>
                                    </button> 
                                    <button id="btnHistoricoGlobal" type="button" class="btn btn-primary" title="Gráfica Tendencia UP"
                                        runat="server" onclick="BtnHistoricoGlobal()">
                                        <span>Gráfica Tendencia UP</span>
                                    </button> 
                                    <button type="submit" runat="server" class="btn btn-default" id="btnimprimirdetalle"
                                        visible="true" onserverclick="btnimprimirdetalle_ServerClick">
                                        <i class="fa fa-print" aria-hidden="true"></i>&nbsp;Exportar a Excel
                                    </button>
                                </div>
                            </div>
                            <div runat="server" id="detallado" visible="false"> 
                            <div class="col-md-12" style="margin-top: 5px;">
                                <dx:BootstrapGridView ID="grDetalle" ClientInstanceName="griddetalla" OnCustomSummaryCalculate="gridRepresentanteMP_CustomSummaryCalculate" runat="server" KeyFieldName="id_cte;id_ter;FechaInicial;fechafinal;Id_Cd;Id_Rik"
                                    Width="100%" AutoGenerateColumns="False">
                                    <Settings ShowFilterRow="true" ShowFilterRowMenu="true" ShowFilterRowMenuLikeItem="true" />
                                    <SettingsBehavior AllowSelectByRowClick="true" SelectionStoringMode="DataIntegrityOptimized" />
                                    <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" />
                                    <SettingsEditing Mode="Batch" />
                                    <Columns>
                                        <dx:BootstrapGridViewTextColumn FieldName="FechaInicial" Caption="FechaInicial" CssClasses-HeaderCell="centerText" Visible="false">
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="fechafinal" Caption="fechafinal" CssClasses-HeaderCell="centerText" Visible="false">
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="Id_Cd" Caption="Id_Cd" CssClasses-HeaderCell="centerText" Visible="false">
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="Id_Rik" Caption="Id_Rik" CssClasses-HeaderCell="centerText" Visible="false">
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="Matriz" CssClasses-HeaderCell="centerText" Caption="Matriz">
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="id_cte" CssClasses-HeaderCell="centerText" Caption="No. cliente">
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="cte_nomcomercial" CssClasses-HeaderCell="centerText" Caption="Cliente" Width="80px">
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="id_ter" CssClasses-HeaderCell="centerText" Caption="Territorio">
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="venta" CssClasses-HeaderCell="centerText" Caption="Vta">
                                            <PropertiesTextEdit DisplayFormatString="c0" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="utilidadBruta" CssClasses-HeaderCell="centerText" Caption="UP">
                                            <PropertiesTextEdit DisplayFormatString="c0" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="porcubreal" CssClasses-HeaderCell="centerText" Caption="% UP" Width="60px">
                                            <PropertiesTextEdit DisplayFormatString="p" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="VNR" CssClasses-HeaderCell="centerText" Width="100" Caption="VNR (3m)">
                                            <PropertiesTextEdit DisplayFormatString="c0" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="VNRPorc" CssClasses-HeaderCell="centerText" Caption="% VNR" Width="60px">
                                            <PropertiesTextEdit DisplayFormatString="P" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="AcysVenta" CssClasses-HeaderCell="centerText" Caption="Acys Vta (Objetivo) ">
                                            <PropertiesTextEdit DisplayFormatString="c0" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="AcysUP" CssClasses-HeaderCell="centerText" Caption="Acys UP (Objetivo)">
                                            <PropertiesTextEdit DisplayFormatString="c0" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="AcysUPProc" CssClasses-HeaderCell="centerText" Caption="Acys % UP (Objetivo)" Width="60px">
                                            <PropertiesTextEdit DisplayFormatString="p" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewCommandColumn Width="20px">
                                            <CustomButtons>
                                                <dx:BootstrapGridViewCommandColumnCustomButton ID="myButton" Text="Gráfica Tendencia UP"></dx:BootstrapGridViewCommandColumnCustomButton>
                                            </CustomButtons>
                                        </dx:BootstrapGridViewCommandColumn>
                                        <dx:BootstrapGridViewDateColumn Width="40px" Caption="Reporte">
                                            <DataItemTemplate>
                                                <button type="button" class="btn btn-link" title="Reporte" runat="server" onserverclick="ReporteCliente_Click">
                                                    <span class="fa fa-pencil"></span>
                                                </button>
                                            </DataItemTemplate>
                                        </dx:BootstrapGridViewDateColumn>
                                    </Columns>
                                    <Templates>
                                        <DetailRow>
                                            <dx:BootstrapGridView ID="grDetalleCategoria" ClientInstanceName="grid" OnCustomSummaryCalculate="grDetalleCategoria_CustomSummaryCalculate" OnBeforePerformDataSelect="grDetalleCategoria_BeforePerformDataSelect"
                                                runat="server" KeyFieldName="id_cte;id_ter;FechaInicial;fechafinal;Id_Cd;Id_Rik;Cpr_Descripcion"
                                                Width="100%" AutoGenerateColumns="False">
                                                <Settings ShowFilterRow="true" ShowFilterRowMenu="true" ShowFilterRowMenuLikeItem="true" />
                                                <SettingsBehavior AllowSelectByRowClick="true" SelectionStoringMode="DataIntegrityOptimized" />
                                                <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" />
                                                <SettingsEditing Mode="Batch" />
                                                <Columns>
                                                    <dx:BootstrapGridViewTextColumn FieldName="FechaInicial" CssClasses-HeaderCell="centerText" Caption="FechaInicial" Visible="false">
                                                    </dx:BootstrapGridViewTextColumn>
                                                    <dx:BootstrapGridViewTextColumn FieldName="fechafinal" CssClasses-HeaderCell="centerText" Caption="fechafinal" Visible="false">
                                                    </dx:BootstrapGridViewTextColumn>
                                                    <dx:BootstrapGridViewTextColumn FieldName="Id_Cd" CssClasses-HeaderCell="centerText" Caption="Id_Cd" Visible="false">
                                                    </dx:BootstrapGridViewTextColumn>
                                                    <dx:BootstrapGridViewTextColumn FieldName="Id_Rik" CssClasses-HeaderCell="centerText" Caption="Id_Rik" Visible="false">
                                                    </dx:BootstrapGridViewTextColumn>
                                                    <dx:BootstrapGridViewTextColumn FieldName="id_cte" CssClasses-HeaderCell="centerText" Caption="No. cliente" Visible="false">
                                                    </dx:BootstrapGridViewTextColumn>
                                                    <dx:BootstrapGridViewTextColumn FieldName="id_ter" CssClasses-HeaderCell="centerText" Caption="terr" Visible="false">
                                                    </dx:BootstrapGridViewTextColumn>
                                                    <dx:BootstrapGridViewTextColumn FieldName="Id_Cpr" CssClasses-HeaderCell="centerText" Caption="id_CPR" Visible="false">
                                                    </dx:BootstrapGridViewTextColumn>
                                                    <dx:BootstrapGridViewTextColumn FieldName="Cpr_Descripcion" CssClasses-HeaderCell="centerText" Caption="Categoria" Settings-AllowHeaderFilter="True">
                                                    </dx:BootstrapGridViewTextColumn>
                                                    <dx:BootstrapGridViewTextColumn FieldName="venta" CssClasses-HeaderCell="centerText" Caption="Vta">
                                                        <PropertiesTextEdit DisplayFormatString="c0" />
                                                    </dx:BootstrapGridViewTextColumn>
                                                    <dx:BootstrapGridViewTextColumn FieldName="utilidadBruta" CssClasses-HeaderCell="centerText" Caption="UP">
                                                        <PropertiesTextEdit DisplayFormatString="c0" />
                                                    </dx:BootstrapGridViewTextColumn>
                                                    <dx:BootstrapGridViewTextColumn FieldName="porcubreal" CssClasses-HeaderCell="centerText" Caption="% UP">
                                                        <PropertiesTextEdit DisplayFormatString="p" />
                                                    </dx:BootstrapGridViewTextColumn>
                                                    <dx:BootstrapGridViewTextColumn FieldName="Mezcla" CssClasses-HeaderCell="centerText" Caption="Mezcla">
                                                        <PropertiesTextEdit DisplayFormatString="p" />
                                                    </dx:BootstrapGridViewTextColumn>
                                                    <dx:BootstrapGridViewTextColumn FieldName="AcysVenta" CssClasses-HeaderCell="centerText" Caption="Acys Vta (Objetivo)">
                                                        <PropertiesTextEdit DisplayFormatString="c0" />
                                                    </dx:BootstrapGridViewTextColumn>
                                                    <dx:BootstrapGridViewTextColumn FieldName="AcysUP" CssClasses-HeaderCell="centerText" Caption="Acys UP (Objetivo)">
                                                        <PropertiesTextEdit DisplayFormatString="c0" />
                                                    </dx:BootstrapGridViewTextColumn>
                                                    <dx:BootstrapGridViewTextColumn FieldName="AcysUPProc" CssClasses-HeaderCell="centerText" Caption="Acys % UP (Objetivo)">
                                                        <PropertiesTextEdit DisplayFormatString="p" />
                                                    </dx:BootstrapGridViewTextColumn>
                                                    <dx:BootstrapGridViewTextColumn FieldName="MezclaAcys" CssClasses-HeaderCell="centerText" Caption="Mezcla ACyS">
                                                        <PropertiesTextEdit DisplayFormatString="p" />
                                                    </dx:BootstrapGridViewTextColumn>

                                                </Columns>
                                                <Templates>
                                                    <DetailRow>
                                                        <dx:BootstrapGridView ID="grDetalleProducto" ClientInstanceName="grid" OnCustomSummaryCalculate="grDetalleProducto_CustomSummaryCalculate" OnBeforePerformDataSelect="grDetalleProducto_BeforePerformDataSelect"
                                                            runat="server" KeyFieldName="Id_Cpr"
                                                            Width="100%" AutoGenerateColumns="False">
                                                            <Settings ShowFooter="True" ShowFilterRow="true" ShowFilterRowMenu="true" ShowFilterRowMenuLikeItem="true" />
                                                            <Columns>
                                                                <dx:BootstrapGridViewTextColumn FieldName="id_prd" CssClasses-HeaderCell="centerText" Caption="id_producto" Visible="false">
                                                                </dx:BootstrapGridViewTextColumn>
                                                                <dx:BootstrapGridViewTextColumn FieldName="prd_nombre" CssClasses-HeaderCell="centerText" Caption="Producto" Settings-AllowHeaderFilter="True">
                                                                </dx:BootstrapGridViewTextColumn>
                                                                <dx:BootstrapGridViewTextColumn FieldName="venta" CssClasses-HeaderCell="centerText" Caption="Vta">
                                                                    <PropertiesTextEdit DisplayFormatString="c0" />
                                                                </dx:BootstrapGridViewTextColumn>
                                                                <dx:BootstrapGridViewTextColumn FieldName="utilidadBruta" CssClasses-HeaderCell="centerText" Caption="UP">
                                                                    <PropertiesTextEdit DisplayFormatString="c0" />
                                                                </dx:BootstrapGridViewTextColumn>
                                                                <dx:BootstrapGridViewTextColumn FieldName="porcubreal" CssClasses-HeaderCell="centerText" Caption="% UP">
                                                                    <PropertiesTextEdit DisplayFormatString="p" />
                                                                </dx:BootstrapGridViewTextColumn>
                                                                <dx:BootstrapGridViewTextColumn FieldName="Mezcla" CssClasses-HeaderCell="centerText" Caption="Mezcla">
                                                                    <PropertiesTextEdit DisplayFormatString="p" />
                                                                </dx:BootstrapGridViewTextColumn>
                                                                <dx:BootstrapGridViewTextColumn FieldName="AcysVenta" CssClasses-HeaderCell="centerText" Caption="Acys Vta (Objetivo)">
                                                                    <PropertiesTextEdit DisplayFormatString="c0" />
                                                                </dx:BootstrapGridViewTextColumn>
                                                                <dx:BootstrapGridViewTextColumn FieldName="AcysUP" CssClasses-HeaderCell="centerText" Caption="Acys UP (Objetivo)">
                                                                    <PropertiesTextEdit DisplayFormatString="c0" />
                                                                </dx:BootstrapGridViewTextColumn>
                                                                <dx:BootstrapGridViewTextColumn FieldName="AcysUPProc" CssClasses-HeaderCell="centerText" Caption="Acys % UP (Objetivo)">
                                                                    <PropertiesTextEdit DisplayFormatString="p" />
                                                                </dx:BootstrapGridViewTextColumn>
                                                                <dx:BootstrapGridViewTextColumn FieldName="MezclaAcys" CssClasses-HeaderCell="centerText" Caption="Mezcla ACyS">
                                                                    <PropertiesTextEdit DisplayFormatString="p" />
                                                                </dx:BootstrapGridViewTextColumn>
                                                            </Columns>
                                                            <SettingsPager PageSize="20" NumericButtonCount="6">
                                                                <Summary Visible="false" />
                                                                <PageSizeItemSettings Visible="true" ShowAllItem="true" />
                                                            </SettingsPager>
                                                            <TotalSummary>
                                                                <dx:ASPxSummaryItem FieldName="venta" SummaryType="Sum" DisplayFormat="c0" />
                                                                <dx:ASPxSummaryItem FieldName="utilidadBruta" SummaryType="Sum" DisplayFormat="c0" />
                                                                <dx:ASPxSummaryItem FieldName="Mezcla" SummaryType="Sum" DisplayFormat="p" />
                                                                <dx:ASPxSummaryItem FieldName="AcysVenta" SummaryType="Sum" DisplayFormat="c0" />
                                                                <dx:ASPxSummaryItem FieldName="AcysUP" SummaryType="Sum" DisplayFormat="c0" />
                                                                <dx:ASPxSummaryItem FieldName="porcubreal" ShowInColumn="porcubreal" Tag="Ubreal" SummaryType="Custom" DisplayFormat="p" />
                                                                <dx:ASPxSummaryItem FieldName="AcysUPProc" ShowInColumn="AcysUPProc" Tag="acysUbreal" SummaryType="Custom" DisplayFormat="p" />
                                                                <dx:ASPxSummaryItem FieldName="MezclaAcys" SummaryType="Sum" DisplayFormat="p" />
                                                            </TotalSummary>
                                                        </dx:BootstrapGridView>
                                                    </DetailRow>
                                                </Templates>
                                                <SettingsPager PageSize="20" NumericButtonCount="6">
                                                    <Summary Visible="false" />
                                                    <PageSizeItemSettings Visible="true" ShowAllItem="true" />
                                                </SettingsPager>
                                                <TotalSummary>
                                                    <dx:ASPxSummaryItem FieldName="venta" SummaryType="Sum" DisplayFormat="c0" />
                                                    <dx:ASPxSummaryItem FieldName="utilidadBruta" SummaryType="Sum" DisplayFormat="c0" />
                                                    <dx:ASPxSummaryItem FieldName="Mezcla" SummaryType="Sum" DisplayFormat="p" />
                                                    <dx:ASPxSummaryItem FieldName="AcysVenta" SummaryType="Sum" DisplayFormat="c0" />
                                                    <dx:ASPxSummaryItem FieldName="AcysUP" SummaryType="Sum" DisplayFormat="c0" />
                                                    <dx:ASPxSummaryItem FieldName="porcubreal" ShowInColumn="porcubreal" Tag="Ubreal" SummaryType="Custom" DisplayFormat="p" />
                                                    <dx:ASPxSummaryItem FieldName="AcysUPProc" ShowInColumn="AcysUPProc" Tag="acysUbreal" SummaryType="Custom" DisplayFormat="p" />
                                                    <dx:ASPxSummaryItem FieldName="MezclaAcys" SummaryType="Sum" DisplayFormat="p" />
                                                </TotalSummary>
                                                <Settings ShowGroupPanel="false" ShowFooter="True" ShowFilterRow="false" ShowFilterRowMenu="true" />
                                            </dx:BootstrapGridView>
                                        </DetailRow>
                                    </Templates>
                                    <SettingsPager PageSize="20" NumericButtonCount="6">
                                        <Summary Visible="false" />
                                        <PageSizeItemSettings Visible="true" ShowAllItem="true" />
                                    </SettingsPager>
                                    <TotalSummary>
                                        <dx:ASPxSummaryItem FieldName="venta" SummaryType="Sum" DisplayFormat="c0" />
                                        <dx:ASPxSummaryItem FieldName="utilidadBruta" SummaryType="Sum" DisplayFormat="c0" />
                                        <dx:ASPxSummaryItem FieldName="VNR" SummaryType="Sum" DisplayFormat="c0" />
                                        <dx:ASPxSummaryItem FieldName="AcysVenta" SummaryType="Sum" DisplayFormat="c0" />
                                        <dx:ASPxSummaryItem FieldName="AcysUP" SummaryType="Sum" DisplayFormat="c0" />
                                        <dx:ASPxSummaryItem FieldName="porcubreal" ShowInColumn="porcubreal" Tag="Ubreal" SummaryType="Custom" DisplayFormat="p" />
                                        <dx:ASPxSummaryItem FieldName="AcysUPProc" ShowInColumn="AcysUPProc" Tag="acysUbreal" SummaryType="Custom" DisplayFormat="p" />
                                        <dx:ASPxSummaryItem FieldName="porcubreal" ShowInColumn="porcubreal" Tag="porcubreal" SummaryType="Custom" DisplayFormat="p" />

                                    </TotalSummary>
                                    <Settings ShowGroupPanel="false" ShowFooter="True" ShowFilterRow="false" ShowFilterRowMenu="true" />

                                    <ClientSideEvents CustomButtonClick="function(s, e) {  
        if(e.buttonID == 'myButton'){  
                             var rowVisibleIndex = e.visibleIndex;  
                var rowKeyValue = s.GetRowKey(rowVisibleIndex);  
                                     OnGetRowValues(rowKeyValue);         
                         }  
    }" />
                                </dx:BootstrapGridView>
                            </div>
                            <div class="col-md-12">
                                <canvas id="myCharthistorialVsVta" width="1200" height="500"></canvas>
                            </div>
                                </div>
                        </div>
                    </div>
                </div>

            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnImprimir" />
                <asp:PostBackTrigger ControlID="btnimprimirdetalle" />
                <asp:PostBackTrigger ControlID="btnCategoria" />
                <asp:AsyncPostBackTrigger ControlID="BtnEstadisticaDetallada" />
                <asp:AsyncPostBackTrigger ControlID="btnclick" />
                <asp:AsyncPostBackTrigger ControlID="btnReporteCategoria" />
                <asp:AsyncPostBackTrigger ControlID="btnReporteCiente" />
                <asp:PostBackTrigger ControlID="grDetalle" />
            </Triggers>
        </asp:UpdatePanel>
           
    </div>
    <div id="modalMensaje" class="modal" role="dialog" tabindex="-1" style="z-index: 1220!important;"
        style="display: none;">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 id="modalAcysMensaje_Titulo">Mensaje</h4>
                </div>
                <div class="modal-body" id="Div11" style="overflow-y: hidden !Important;">
                    <div class="col-md-12">
                        <div class="col-md-1">
                            <i id="modalMensaje_Icon" class="fa fa-exclamation-triangle_x" aria-hidden="true"></i>
                        </div>
                        <div class="col-md-10">
                            <span id="lblmensaje2" runat="server"></span>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-12">
                            <button class="btn btn-default" data-dismiss="modal" id="Button9">
                                Ok</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
     <div id="ModalUpload" data-keyboard="false" data-backdrop="static" data-toggle="modal"
        style="height: 800px !important" class="modal" role="dialog" style="z-index: 2220!important">
        <div class="modal-dialog modal-dialog2" role="document" style="height: 120px !important;">
            <!-- Modal content-->
            <div class="modal-content modal-content2">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    Cargar Documento
                </div>
                <div>
                    <div class="embed-responsive embed-responsive-16by9 " style="padding-bottom: 40% !Important;">
                        <iframe class="embed-responsive-item" id="iFrameCargar" runat="server" src=""></iframe>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function modalMensaje(mensaje) {
            document.getElementById('<%=lblmensaje2.ClientID%>').innerHTML = mensaje;
            $("#modalMensaje").appendTo("body");
            $("#modalMensaje").modal({ "backdrop": "static" });
            $('#modalMensaje').modal('show');
        }
        function BtnEstadisticaDetallada() {
            $('#<%= UpdateProgress1.ClientID %>').css("display", "block");
            $.ajax({
                type: "POST",
                url: "ReporteGestionUtilidad.aspx/ObservarTotales",
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                    document.getElementById('<%=lblmensaje2.ClientID%>').innerHTML = errorThrown;
                    $("#modalMensaje").appendTo("body");
                    $("#modalMensaje").modal({ "backdrop": "static" });
                    $('#modalMensaje').modal('show');
                },
                success: function (response) {
                    if (response != null && response.d != null) {
                        var data = response.d;
                        data = $.parseJSON(data);
                        var id = data.id;
                        if (id == -1) {
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            document.getElementById('<%=lblmensaje2.ClientID%>').innerHTML = data.men;
                            $("#modalMensaje").appendTo("body");
                            $("#modalMensaje").modal({ "backdrop": "static" });
                            $('#modalMensaje').modal('show');
                        }
                        if (id == 0) {
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            document.location.href = 'login.aspx';
                        }
                        if (id == 1) {
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            document.getElementById('<%=lblmensaje2.ClientID%>').innerHTML = 'Favor de seleccionar la sucursal de la sección de observar totales.';
                            $("#modalMensaje").appendTo("body");
                            $("#modalMensaje").modal({ "backdrop": "static" });
                            $('#modalMensaje').modal('show');
                        }
                        if (id == 2) {
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            document.getElementById('<%=lblmensaje2.ClientID%>').innerHTML = 'Favor de capturar los campos de fecha de la sección de observar totales.';
                            $("#modalMensaje").appendTo("body");
                            $("#modalMensaje").modal({ "backdrop": "static" });
                            $('#modalMensaje').modal('show');
                        }
                        if (id == 4) {
                            document.getElementById('<%=lblmensaje2.ClientID%>').innerHTML = 'La fecha inicial es mayor a la fecha final de la sección de observar totales.';
                            $("#modalMensaje").appendTo("body");
                            $("#modalMensaje").modal({ "backdrop": "static" });
                            $('#modalMensaje').modal('show');
                        }
                        if (id == 3) {
                            var ict_unit = [];
                            var efficiency = [];
                            var coloR = [];
                            var dynamicColors = function () {
                                var r = Math.floor(Math.random() * 255);
                                var g = Math.floor(Math.random() * 255);
                                var b = Math.floor(Math.random() * 255);
                                return "rgb(" + r + "," + g + "," + b + ")";
                            };
                            document.getElementById('<%=total.ClientID%>').innerHTML = data.totalup;
                            document.getElementById('<%=total2.ClientID%>').innerHTML = data.totalUtilidad;
                            var titulo = data.titulo;
                            var tituloStrArr = titulo.split(",")
                            var tituloArr = [];
                            tituloStrArr.forEach(function (data, index, arr) {
                                var nombre = data;
                                tituloArr.push(nombre);
                            });
                            var datos = data.datosup;
                            var datosstrArr = datos.split(",")
                            var datosArr = [];
                            datosstrArr.forEach(function (data, index, arr) {
                                datosArr.push(data);
                            });
                            var datos2 = data.datosUtilidad;
                            var datosstrArr2 = datos2.split(",")
                            var datosArr2 = [];
                            datosstrArr2.forEach(function (data, index, arr) {
                                datosArr2.push(+data);
                                coloR.push(dynamicColors());
                            });
                            var ctx = document.getElementById('myChart');
                            var ctx2 = document.getElementById('myChartpieup');
                            window.myChart = new Chart(ctx, {
                                type: 'pie',
                                data: {
                                    labels: tituloArr,
                                    datasets: [{
                                        data: datosArr,
                                        borderWidth: 1
                                    }]
                                },
                                options: {
                                    responsive: true,
                                    display: 'auto',
                                    legend: {
                                        position: 'left',
                                        labels: {
                                            padding: 10,
                                            fontSize: 9,
                                        }
                                    },
                                    title: {
                                        display: true,
                                        text: 'Mezcla en Ventas',
                                    },
                                    tooltips: {
                                        callbacks: {
                                            label: function (tooltipItem, data) {
                                                var indice = tooltipItem.index;
                                                return data.labels[indice] + ': $' + data.datasets[0].data[indice].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                            }
                                        }
                                    },
                                    pieceLabel: {
                                        render: function (args) {
                                            return '$' + args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                        },
                                        fontColor: '#000',
                                        position: 'outside',
                                        segment: true
                                    },
                                    plugins: {
                                        colorschemes: {
                                            scheme: 'brewer.Paired12',
                                        },
                                        labels: [
                                            {
                                                render: 'percentage',
                                                fontStyle: 'bold',
                                                fontColor: '#fff',
                                            }
                                        ],
                                    }
                                }
                            });
                            window.myChart3 = new Chart(ctx2, {
                                type: 'pie',
                                data: {
                                    labels: tituloArr,
                                    datasets: [{
                                        data: datosArr2,
                                        borderWidth: 1
                                    }]
                                },
                                options: {
                                    responsive: true,
                                    display: 'auto',
                                    legend: {
                                        position: 'left',
                                        labels: {
                                            padding: 10,
                                            fontSize: 9,
                                        }
                                    },
                                    title: {
                                        display: true,
                                        text: 'Mezcla de UP',
                                    },
                                    tooltips: {
                                        callbacks: {
                                            label: function (tooltipItem, data) {
                                                var indice = tooltipItem.index;
                                                return data.labels[indice] + ': $' + data.datasets[0].data[indice].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                            }
                                        }
                                    },
                                    pieceLabel: {
                                        render: function (args) {
                                            return '$' + args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                        },
                                        fontColor: '#000',
                                        position: 'outside',
                                        segment: true
                                    },
                                    plugins: {
                                        colorschemes: {
                                            scheme: 'brewer.Paired12',
                                        },
                                        labels: [
                                            {
                                                render: 'percentage',
                                                fontStyle: 'bold',
                                                fontColor: '#fff',
                                            }
                                        ],
                                    }
                                }
                            });
                        }
                    }
                },
                complete: function (response) {
                    $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                },
            });
        }
        function BtnPreVsVta() {
            $('#<%= UpdateProgress1.ClientID %>').css("display", "block");
            $.ajax({
                type: "POST",
                url: "ReporteGestionUtilidad.aspx/PresupuestoVenta",
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                    modalMensaje(errorThrown);
                },
                success: function (response) {
                    if (response != null && response.d != null) {
                        var data = response.d;
                        data = $.parseJSON(data);
                        var id = data.id;
                        if (id == -1) {
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            modalMensaje(data.men);
                        }
                        if (id == 0) {
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            document.location.href = 'login.aspx';
                        }
                        if (id == 1) {
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            modalMensaje('Favor de seleccionar la sucursal.');
                        }
                        if (id == 2) {
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            modalMensaje('Favor de realizar la consulta de información.');
                        }
                        if (id == 5) {
                            var myChart4;
                            var ctx = document.getElementById('myChartPreVsVta');
                            if (window.myChart4 != undefined) {
                                window.myChart4.destroy();
                            }
                            var titulo = data.titulo;
                            var tituloStrArr = titulo.split(",")
                            var tituloArr = [];
                            tituloStrArr.forEach(function (data, index, arr) {
                                var nombre = data;
                                tituloArr.push(nombre);
                            });
                            var datos = data.datos;
                            var datosstrArr = datos.split(",")
                            var datosArr = [];
                            datosstrArr.forEach(function (data, index, arr) {
                                datosArr.push(+data);
                            });
                            var datos2 = data.datos2;
                            var datosstrArr2 = datos2.split(",")
                            var datosArr2 = [];
                            datosstrArr2.forEach(function (data, index, arr) {
                                datosArr2.push(+data);
                            });
                            var datos3 = data.datos3;
                            var datosstrArr3 = datos3.split(",")
                            var datosArr3 = [];
                            datosstrArr3.forEach(function (data, index, arr) {
                                datosArr3.push(+data);
                            });
                            var lineChartData = {
                                labels: tituloArr,
                                datasets: [{
                                    label: "Venta real",
                                    borderColor: "rgb(205,92,92)",
                                    backgroundColor: "rgb(205,92,92)",
                                    fill: false,
                                    data: datosArr,
                                    yAxisID: "y-axis-1",
                                }, {
                                    label: "UP real",
                                    borderColor: "rgb(113, 205, 92)",
                                    backgroundColor: "rgb(113, 205, 92)",
                                    fill: false,
                                    data: datosArr3,
                                    yAxisID: "y-axis-1",
                                }, {
                                    label: "Presupuesto de venta",
                                    borderColor: "rgb(0, 191, 255)",
                                    backgroundColor: "rgb(0, 191, 255)",
                                    fill: false,
                                    data: datosArr2,
                                    yAxisID: "y-axis-1"
                                }]
                            };
                            window.myChart4 = new Chart(ctx, {
                                type: 'bar',
                                data: lineChartData,
                                options: {
                                    responsive: true,
                                    display: false,
                                    labels: {
                                        padding: 10,
                                        fontSize: 10,
                                        display: false,
                                    },
                                    legend: {
                                        display: true,
                                    },
                                    scaleshowvalue: true,
                                    hoverMode: 'index',
                                    stacked: false,
                                    title: {
                                        display: true,
                                        text: 'Gráfica Presupuesto-Venta',
                                    },
                                    scales: {
                                        yAxes: [{
                                            type: "linear",
                                            display: false,
                                            position: "left",
                                            id: "y-axis-2",
                                        }, {
                                            type: "linear",
                                            display: true,
                                            position: "left",
                                            id: "y-axis-1",
                                            // grid line settings
                                            gridLines: {
                                                drawOnChartArea: false, // only want the grid lines for one axis to show up
                                            },
                                        }],
                                        xAxes: [{
                                            stacked: false,
                                            beginAtZero: true,
                                            ticks: {
                                                stepSize: 1,
                                                min: 0,
                                                autoSkip: false,
                                                beginAtZero: true
                                            }
                                        }],
                                    },
                                    plugins: {
                                        labels: {
                                            render: function (args) {
                                                return ' $' + args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                            },
                                            fontColor: '#000',
                                        }
                                    },
                                    tooltips: {
                                        callbacks: {
                                            label: function (tooltipItem, data) {
                                                return ' $' + tooltipItem.yLabel.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                            }
                                        }
                                    },
                                }
                            });
                        }
                    }
                },
                complete: function (response) {
                    $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                },
            });
        }
        function OnGridFocusedRowChanged() {
            var rowVisibleIndex = e.visibleIndex;
            var rowKeyValue = s.GetRowKey(rowVisibleIndex);
        }
        function OnGetRowValues(values) {
            var arrayDeCadenas = values.split('|');
            $('#<%= UpdateProgress1.ClientID %>').css("display", "block");
            var idCte = arrayDeCadenas[0];
            var ter = arrayDeCadenas[1];
            var idcd = arrayDeCadenas[4];
            var idrik = arrayDeCadenas[5];
            var dataValue = "{idCte: '" + idCte + "', ter: '" + ter + "', sucursal: '" + idcd + "', idRik: '" + idrik + "'}";
            $.ajax({
                type: "POST",
                url: "ReporteGestionUtilidad.aspx/HistorialCliente",
                data: dataValue,
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                    modalMensaje(errorThrown);
                },
                success: function (response) {
                    if (response != null && response.d != null) {
                        var data = response.d;
                        data = $.parseJSON(data);
                        var id = data.id;
                        if (id == -1) {
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            modalMensaje(data.men);
                        }
                        if (id == 0) {
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            document.location.href = 'login.aspx';
                        }
                        if (id == 1) {
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            modalMensaje('Favor de seleccionar la sucursal.');
                        }
                        if (id == 2) {
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            modalMensaje('Favor de capturar El representante .');
                        }
                        if (id == 3) {
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            modalMensaje('Favor de capturar los campos de fecha.');
                        }
                        if (id == 4) {
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            modalMensaje('La fecha inicial es mayor a la fecha final de la sección de observar totales.');
                        }
                        if (id == 5) {
                            var myChart4;
                            var ctx = document.getElementById('myCharthistorialVsVta');
                            if (window.myChart4 != undefined) {
                                window.myChart4.destroy();
                            }
                            var titulo = data.titulo;
                            var tituloStrArr = titulo.split(",")
                            var tituloArr = [];
                            tituloStrArr.forEach(function (data, index, arr) {
                                var nombre = data;
                                tituloArr.push(nombre);
                            });
                            var datos = data.datos;
                            var datosstrArr = datos.split(",")
                            var datosArr = [];
                            datosstrArr.forEach(function (data, index, arr) {
                                datosArr.push(+data);
                            });
                            var datos2 = data.datos2;
                            var datosstrArr2 = datos2.split(",")
                            var datosArr2 = [];
                            datosstrArr2.forEach(function (data, index, arr) {
                                datosArr2.push(+data);
                            });
                            var datos3 = data.datos3;
                            var datosstrArr3 = datos3.split(",")
                            var datosArr3 = [];
                            datosstrArr3.forEach(function (data, index, arr) {
                                datosArr3.push(+data);
                            });
                            var lineChartData = {
                                labels: tituloArr,
                                datasets: [{
                                    label: "% UB",
                                    borderColor: "rgb(255, 63, 51 )",
                                    backgroundColor: "rgb(255, 63, 51 )",
                                    fill: false,
                                    data: datosArr2,
                                    type: 'line',
                                    yAxisID: "y-axis-1"
                                },
                                {
                                    label: "Venta",
                                    borderColor: "rgb(51, 88, 255",
                                    backgroundColor: "rgb(51, 88, 255)",
                                    fill: false,
                                    data: datosArr3,
                                    yAxisID: "y-axis-2",
                                }, {
                                    label: "UB",
                                    borderColor: "rgb(51, 255, 60 )",
                                    backgroundColor: "rgb(51, 255, 60 )",
                                    fill: false,
                                    data: datosArr,
                                    yAxisID: "y-axis-2",
                                }]
                            };
                            window.myChart4 = new Chart(ctx, {
                                type: 'bar',
                                data: lineChartData,
                                options: {
                                    responsive: true,
                                    display: false,
                                    labels: {
                                        padding: 10,
                                        fontSize: 10,
                                        display: false,
                                    },
                                    legend: {
                                        display: true,
                                    },
                                    scaleshowvalue: true,
                                    hoverMode: 'index',
                                    stacked: false,
                                    title: {
                                        display: true,
                                        text: 'Tendencia Histoica UP - Cliente',
                                    },
                                    scales: {
                                        yAxes: [{
                                            type: "linear",
                                            display: true,
                                            position: "right",
                                            id: "y-axis-1",
                                            gridLines: {
                                                drawOnArea: false,
                                            },
                                            ticks: {
                                                min: 0,
                                                max: 100,
                                                stepSize: 20,
                                                callback: function (value) {
                                                    return (value).toFixed(0) + '%'; // convert it to percentage
                                                },
                                            },
                                        }, {
                                            type: "linear",
                                            display: true,
                                            position: "left",
                                            id: "y-axis-2",
                                            // grid line settings
                                            gridLines: {
                                                drawOnChartArea: false, // only want the grid lines for one axis to show up
                                            },
                                        }],
                                        xAxes: [{
                                            stacked: false,
                                            beginAtZero: true,
                                            ticks: {
                                                stepSize: 1,
                                                min: 0,
                                                autoSkip: false,
                                                beginAtZero: true
                                            }
                                        }],
                                    },
                                    plugins: {
                                        labels: false,
                                    },
                                    tooltips: {
                                        callbacks: {
                                            label: function (t, d) {
                                                if (t.datasetIndex === 0) {
                                                    var xLabel = d.datasets[t.datasetIndex].label;
                                                    var yLabel = t.yLabel + '%';
                                                    return xLabel + ': ' + yLabel;
                                                } else {
                                                    var xLabel = d.datasets[t.datasetIndex].label;
                                                    var yLabel = t.yLabel >= 1000 ? '$' + t.yLabel.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") : '$' + t.yLabel;
                                                    return xLabel + ': ' + yLabel;
                                                }
                                            }
                                        }
                                    },
                                }
                            });
                        }
                    }
                },
                complete: function (response) {
                    $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                },
            });
        }
        function BtnHistoricoGlobal() {
            $('#<%= UpdateProgress1.ClientID %>').css("display", "block");
            $.ajax({
                type: "POST",
                url: "ReporteGestionUtilidad.aspx/Historialglobal",
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                    modalMensaje(errorThrown);
                },
                success: function (response) {
                    if (response != null && response.d != null) {
                        var data = response.d;
                        data = $.parseJSON(data);
                        var id = data.id;
                        if (id == -1) {
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            modalMensaje(data.men);
                        }
                        if (id == 0) {
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            document.location.href = 'login.aspx';
                        }
                        if (id == 1) {
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            modalMensaje('Favor de seleccionar la sucursal.');
                        }
                        if (id == 2) {
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            modalMensaje('Favor de capturar El representante .');
                        }
                        if (id == 3) {
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            modalMensaje('Favor de capturar los campos de fecha.');
                        }
                        if (id == 4) {
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            modalMensaje('La fecha inicial es mayor a la fecha final de la sección de observar totales.');
                        }
                        if (id == 5) {
                            var myChart4;
                            var ctx = document.getElementById('myCharthistorialVsVta');
                            if (window.myChart4 != undefined) {
                                window.myChart4.destroy();
                            }
                            var titulo = data.titulo;
                            var tituloStrArr = titulo.split(",")
                            var tituloArr = [];
                            tituloStrArr.forEach(function (data, index, arr) {
                                var nombre = data;
                                tituloArr.push(nombre);
                            });
                            var datos = data.datos;
                            var datosstrArr = datos.split(",")
                            var datosArr = [];
                            datosstrArr.forEach(function (data, index, arr) {
                                datosArr.push(+data);
                            });
                            var datos2 = data.datos2;
                            var datosstrArr2 = datos2.split(",")
                            var datosArr2 = [];
                            datosstrArr2.forEach(function (data, index, arr) {
                                datosArr2.push(+data);
                            });
                            var datos3 = data.datos3;
                            var datosstrArr3 = datos3.split(",")
                            var datosArr3 = [];
                            datosstrArr3.forEach(function (data, index, arr) {
                                datosArr3.push(+data);
                            });
                            var lineChartData = {
                                labels: tituloArr,
                                datasets: [{
                                    label: "% UB",
                                    borderColor: "rgb(255, 63, 51 )",
                                    backgroundColor: "rgb(255, 63, 51 )",
                                    fill: false,
                                    data: datosArr2,
                                    type: 'line',
                                    yAxisID: "y-axis-1"
                                },
                                {
                                    label: "Venta",
                                    borderColor: "rgb(51, 88, 255",
                                    backgroundColor: "rgb(51, 88, 255)",
                                    fill: false,
                                    data: datosArr3,
                                    yAxisID: "y-axis-2",
                                }, {
                                    label: "UB",
                                    borderColor: "rgb(51, 255, 60 )",
                                    backgroundColor: "rgb(51, 255, 60 )",
                                    fill: false,
                                    data: datosArr,
                                    yAxisID: "y-axis-2",
                                }]
                            };
                            window.myChart4 = new Chart(ctx, {
                                type: 'bar',
                                data: lineChartData,
                                options: {
                                    responsive: true,
                                    display: false,
                                    labels: {
                                        padding: 10,
                                        fontSize: 10,
                                        display: false,
                                    },
                                    legend: {
                                        display: true,
                                    },
                                    scaleshowvalue: true,
                                    hoverMode: 'index',
                                    stacked: false,
                                    title: {
                                        display: true,
                                        text: 'Tendencia Histoica UP - Global',
                                    },
                                    scales: {
                                        yAxes: [{
                                            type: "linear",
                                            display: true,
                                            position: "right",
                                            id: "y-axis-1",
                                            gridLines: {
                                                drawOnArea: false,
                                            },
                                            ticks: {
                                                min: 0,
                                                max: 100,
                                                stepSize: 20,
                                                callback: function (value) {
                                                    return (value).toFixed(0) + '%'; // convert it to percentage
                                                },
                                            },
                                        }, {
                                            type: "linear",
                                            display: true,
                                            position: "left",
                                            id: "y-axis-2",
                                            // grid line settings
                                            gridLines: {
                                                drawOnChartArea: false, // only want the grid lines for one axis to show up
                                            },
                                            ticks: {
                                                min: 0, 
                                                callback: function (value) {
                                                    return '$' + ((value < 1000000) ? value : value / 1000000 + 'M'); // convert it to percentage
                                                },
                                            },
                                        }],
                                        xAxes: [{
                                            stacked: false,
                                            beginAtZero: true,
                                            ticks: {
                                                stepSize: 1,
                                                min: 0,
                                                autoSkip: false,
                                                beginAtZero: true
                                            }
                                        }],
                                    },
                                    plugins: {
                                        labels: false,
                                    },
                                    tooltips: {
                                        callbacks: {
                                            label: function (t, d) {
                                                if (t.datasetIndex === 0) {
                                                    var xLabel = '$' + d.datasets[t.datasetIndex].label;
                                                    var yLabel = t.yLabel + '%';
                                                    return xLabel + ': ' + yLabel;
                                                } else {
                                                    var xLabel = d.datasets[t.datasetIndex].label;
                                                    var yLabel = t.yLabel >= 1000 ? '$' + t.yLabel.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") : '$' + t.yLabel;
                                                    return xLabel + ': ' + yLabel;
                                                }
                                            }
                                        }
                                    },
                                }
                            });
                        }
                    }
                },
                complete: function (response) {
                    $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                },
            });
        }
    </script>
</asp:Content>