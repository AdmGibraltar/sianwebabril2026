<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/ReporteCom.Master" EnableEventValidation="false" 
    AutoEventWireup="true" CodeBehind="ReporteComercialesLeads.aspx.cs" Inherits="SIANWEB.ReporteComercialesLeads" %>

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
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/Chart.PieceLabel.min.js")%>"></script>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css")%>" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/bootstrap-select.min.js") %>"></script>
    <link href="<%=Page.ResolveUrl("~/Librerias/Zebra_Datepicker-master/dist/css/bootstrap/zebra_datepicker.css")%>"
        rel="stylesheet" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/Zebra_Datepicker-master/dist/zebra_datepicker.src.js") %>"></script>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/css/font-awesome.min.css")%>" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/icheck.min.js")%>"></script>

    <link href="<%=Page.ResolveUrl("~/Librerias/timepicker/jquery.timepicker.css")%>"
        rel="stylesheet" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/timepicker/jquery.timepicker.min.js") %>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/timepicker/jquery.timepicker.js") %>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/excel/FileSaver.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/excel/jszip.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/excel/myexcel.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/chartjs-plugin-labels.js")%>"></script>
    <script type="text/javascript"> 
        function modalQuestion(mensaje) {
            document.getElementById('<%=lblquestion.ClientID%>').innerHTML = mensaje;
            $("#modalQuestion").appendTo("body")
            $("#modalQuestion").modal({ "backdrop": "static" });
            $('#modalQuestion').modal('show');
        }
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
    </script>
    <style type="text/css">
        form-control {
            width: 100%;
            height: 34px !important;
            padding: 6px 12px !important;
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
            height: 220px;
        }
        .boxes2 {
            height: 200px;
            overflow: auto;
            width: 300px;
        }
        .content2 {
            height: 220px;
            overflow: auto;
        }
        .boxes {
            width: 350px;
        }
        .checkbox, .radio {
            margin-top: 2px !important;
            margin-bottom: 10px !important;
        }
        .list-group {
            height: 150px !important;
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
            <h2 style="font-weight: bolder">Reportes de leads </h2>
        </div>
        <ul class="nav nav-tabs" id="tabPage">
            <li class="active"><a href="#tabDatos" data-toggle="tab">
                <h5>Generar Resultados</h5>
            </a>
            </li>
            <li><a href="#tabInfo" data-toggle="tab">
                <h5>Descargar información</h5>
            </a></li>
           
        </ul>
        <div class="tab-content">
            <%--TAB Datos generales --%>
            <div class="tab-pane fade in active" id="tabDatos">
                <%-- Observar Totales --%>

                <div class="row" style="margin-top: 15px;">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h3 class="panel-title">Observar Totales
                            </h3>
                            <span class="pull-right clickable panel-collapsed"><i class="glyphicon glyphicon-chevron-up"></i>
                            </span>
                        </div>
                        <div class="panel-body panel-collapsed" style="display: none;">
                            <asp:UpdatePanel runat="server" ID="updObservarTot" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row" style="margin-top: -10px">
                                        <div class="col-md-4">
                                            <div class="col-md-12">
                                                <dx:BootstrapRadioButtonList ID="BRLTipoOT" ClientInstanceName="TipoOT" runat="server" RepeatColumns="1">
                                                    <Items>
                                                        <dx:BootstrapListEditItem Value="0" Text="Medio de Comunicación" Selected="true"></dx:BootstrapListEditItem>
                                                        <dx:BootstrapListEditItem Value="1" Text="Giro de la Empresa"></dx:BootstrapListEditItem>
                                                    </Items>
                                                </dx:BootstrapRadioButtonList>
                                            </div>
                                        </div>
                                        <div class="col-md-4">

                                            <div class="row" style="margin-top: 5px">
                                                <div class="form-group">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label18" runat="server" Text="Fecha Inicial" />
                                                    </div>
                                                    <div class="col-md-8">
                                                        <dx:ASPxDateEdit runat="server" ClientInstanceName="AnioInicialOT" CssClass="form-control" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="TXTAnioInicialOT" PickerType="Months">
                                                            <CalendarProperties>
                                                                <FastNavProperties InitialView="Years" MaxView="Years" />
                                                            </CalendarProperties>
                                                        </dx:ASPxDateEdit>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin-top: 5px">
                                                <div class="form-group">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label19" runat="server" Text="Fecha Final" />
                                                    </div>
                                                    <div class="col-md-8">
                                                        <dx:ASPxDateEdit runat="server" ClientInstanceName="AnioFinalOT" CssClass="form-control" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="TXTAnioFinalOT" PickerType="Months">
                                                            <CalendarProperties>
                                                                <FastNavProperties InitialView="Years" MaxView="Years" />
                                                            </CalendarProperties>
                                                        </dx:ASPxDateEdit>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px;">
                                                <div class="col-md-2">
                                                    <button id="BTNObservarTotales" type="button" class="btn btn-primary" title="Generar estadistica"
                                                        runat="server" onclick="BtnObservarTotales()">
                                                        <span>Generar estadistica</span>
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 5px">
                                        <div class="col-md-12">
                                            <div style="float: right">
                                                <span id="total" runat="server"></span>
                                            </div>
                                            <div class="row" style="margin-top: 10px;">
                                                <canvas id="myChart" style="width: 700px; height: 250px"></canvas>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="BTNObservarTotales" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>


                <%-- Integrar Leads --%>

                <div class="row" style="margin-top: -10px;">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h3 class="panel-title">Integrar Leads</h3>
                            <span class="pull-right clickable panel-collapsed"><i class="glyphicon glyphicon-chevron-up"></i>
                            </span>
                        </div>
                        <div class="panel-body panel-collapsed" style="display: none;">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
                                <ContentTemplate>


                                    <%-- Inicio--%>

                                    <div class="row" style="margin-top: -10px">
                                        <div class="col-md-4">
                                        </div>
                                        <div class="col-md-8">
                                            <div class="col-md-6" style="margin-top: 5px">
                                                <div class="form-group">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label29" runat="server" Text="Fecha Inicial" />
                                                    </div>
                                                    <div class="col-md-8">
                                                        <dx:ASPxDateEdit runat="server" CssClass="form-control" ClientInstanceName="FechaInicialPV" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="txtFechaInicialPVV" PickerType="Months">
                                                            <CalendarProperties>
                                                                <FastNavProperties InitialView="Years" MaxView="Years" />
                                                            </CalendarProperties>
                                                        </dx:ASPxDateEdit>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-6" style="margin-top: 5px">
                                                <div class="form-group">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label31" runat="server" Text="Fecha Final" />
                                                    </div>
                                                    <div class="col-md-8">
                                                        <dx:ASPxDateEdit runat="server" CssClass="form-control" ClientInstanceName="FechaFinalPV" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="txtFechaFinalPVV" PickerType="Months">
                                                            <CalendarProperties>
                                                                <FastNavProperties InitialView="Years" MaxView="Years" />
                                                            </CalendarProperties>
                                                        </dx:ASPxDateEdit>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="col-md-12" style="margin-top: 5px">
                                                Selecciones tipo de datos:
                                            </div>
                                            <div class="col-md-12" style="margin-top: 5px">
                                                <dx:BootstrapRadioButtonList ID="tiposvs" ClientInstanceName="tiposvs" runat="server" RepeatColumns="1">
                                                    <Items>
                                                        <dx:BootstrapListEditItem Value="0" Text="Cantidad" Selected="true"></dx:BootstrapListEditItem>
                                                        <dx:BootstrapListEditItem Value="1" Text="Ventas"></dx:BootstrapListEditItem>
                                                    </Items>
                                                </dx:BootstrapRadioButtonList>
                                            </div>

                                            <div class="col-md-12" style="margin-top: 5px">

                                                <button id="btnBucaPVV" type="button" class="btn btn-primary" title="Consultar "
                                                    runat="server" onclick="BtnPreVsVta()">
                                                    <span>Consultar</span>
                                                </button>
                                            </div>


                                        </div>
                                    </div>


                                    <div class="col-md-12" style="margin-top: 5px">
                                    </div>

                                    <%--fin--%>

                                    <div class="col-md-4">
                                    </div>
                                    <div class="col-md-8">
                                        <canvas id="myChartPreVsVta" width="800" height="300"></canvas>
                                    </div>
                                </ContentTemplate>

                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>


                <%--TAB Graficos de Efectividad --%>

                <div class="row" style="margin-top: -10px;">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h3 class="panel-title">Efectividad</h3>
                            <span class="pull-right clickable panel-collapsed"><i class="glyphicon glyphicon-chevron-up"></i>
                            </span>
                        </div>
                        <div class="panel-body panel-collapsed" style="display: none;">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel43" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="col-md-12">
                                        <div class="col-md-6">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label1" runat="server" Text="Sucursal" />
                                                    </div>
                                                    <div class="col-md-8">
                                                        <dx:BootstrapComboBox ID="CmbSucursal" runat="server" AutoPostBack="true" ClientInstanceName="SucursalOT" OnSelectedIndexChanged="CmbSucursal_SelectedIndexChanged">
                                                        </dx:BootstrapComboBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6" style="margin-top: 5px;">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label2" runat="server" Text="Rik" />
                                                </div>
                                                <div class="col-md-8">

                                                    <dx:BootstrapListBox ID="RBLRepresentante" SelectionMode="CheckColumn" EnableSelectAll="true" Enabled="True" ClientInstanceName="ListaRepresentanteef" runat="server">
                                                    </dx:BootstrapListBox>

                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12">

                                        <div class="col-md-6" style="margin-top: 5px">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label6" runat="server" Text="Fecha Inicial" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:ASPxDateEdit runat="server" ClientInstanceName="AnioInicialEF" CssClass="form-control" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="TXTAnioInicialEF" PickerType="Months">
                                                        <CalendarProperties>
                                                            <FastNavProperties InitialView="Years" MaxView="Years" />
                                                        </CalendarProperties>
                                                    </dx:ASPxDateEdit>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6" style="margin-top: 5px">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label7" runat="server" Text="Fecha Final" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:ASPxDateEdit runat="server" ClientInstanceName="AnioFinalEF" CssClass="form-control" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="TXTAnioFinalEF" PickerType="Months">
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

                                            <div class="col-md-12" style="margin-top: 5px">
                                                Selecciones tipo de Datos:
                                            </div>
                                            <div class="col-md-12" style="margin-top: 5px">
                                                <dx:BootstrapRadioButtonList ID="TipoEf" ClientInstanceName="tipoef" runat="server" RepeatColumns="1">
                                                    <Items>
                                                        <dx:BootstrapListEditItem Value="0" Text="Cantidad de proyectos" Selected="true"></dx:BootstrapListEditItem>
                                                        <dx:BootstrapListEditItem Value="1" Text="Monto de Proyectos"></dx:BootstrapListEditItem>
                                                    </Items>
                                                </dx:BootstrapRadioButtonList>
                                            </div>

                                        </div>

                                        <div class="col-md-12" style="margin-top: 5px;">

                                            <div class="col-md-2">
                                                <button id="BTNEfectividad" type="button" class="btn btn-primary" title="Generar estadistica"
                                                    runat="server" onclick="graficaEfectividadSucursal()">
                                                    <span>Generar estadistica</span>
                                                </button>
                                            </div>
                                        </div>
                                    </div>

                                   
                                    <div class="col-md-12">
                                        <div class="col-md-6">
                                            <div class="row" style="margin-top: 10px;">
                                                <canvas id="myChartPie" width="700" height="250"></canvas>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="row" style="margin-top: 10px;">
                                                <canvas id="myefsucursal" width="700" height="250"></canvas>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="CmbSucursal" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>

                <%-- Tiempo de respuesta secciones --%>
                <div class="row" style="margin-top: -10px;">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h3 class="panel-title">Tiempo de Respuesta</h3>
                            <span class="pull-right clickable panel-collapsed"><i class="glyphicon glyphicon-chevron-up"></i>
                            </span>
                        </div>
                        <div class="panel-body panel-collapsed" style="display: none;">
                            <asp:UpdatePanel runat="server" ID="updpanel3" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-8">
                                            <asp:Wizard ID="WCompararRepresentantes" runat="server" DisplaySideBar="false" CssClass="wizard" ActiveStepIndex="0" OnNextButtonClick="wizClaimInfo_NextButtonClick" OnPreviousButtonClick="WCompararRepresentantes_PreviousButtonClick">
                                                <WizardSteps>

                                                    <asp:WizardStep ID="WizardStep1" runat="server" Title="1">
                                                        <div class="content">

                                                            <div class="col-md-12" style="margin-top: 5px;">

                                                                <div class="col-md-6">
                                                                    <div class="form-group">

                                                                        <asp:Label ID="Label9" runat="server" Text="Fecha Inicial" />

                                                                    </div>
                                                                </div>

                                                                <div class="col-md-6">

                                                                    <asp:Label ID="Label10" runat="server" Text="Fecha Final" />


                                                                </div>
                                                            </div>
                                                            <div class="col-md-12" style="margin-top: 5px;">

                                                                <div class="col-md-6">
                                                                    <div class="form-group">

                                                                        <dx:ASPxDateEdit runat="server" ClientInstanceName="AnioInicialTR" CssClass="form-control" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="TXTAnioInicialTR" PickerType="Months">
                                                                            <CalendarProperties>
                                                                                <FastNavProperties InitialView="Years" MaxView="Years" />
                                                                            </CalendarProperties>
                                                                        </dx:ASPxDateEdit>

                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <dx:ASPxDateEdit runat="server" ClientInstanceName="AnioFinalTR" CssClass="form-control" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="TXTAnioFinalTR" PickerType="Months">
                                                                        <CalendarProperties>
                                                                            <FastNavProperties InitialView="Years" MaxView="Years" />
                                                                        </CalendarProperties>
                                                                    </dx:ASPxDateEdit>
                                                                </div>


                                                            </div>


                                                        </div>
                                                    </asp:WizardStep>
                                                    <asp:WizardStep ID="WizardStep2" runat="server" Title="2">
                                                        <div class="content">
                                                            <div class="col-md-12" style="margin-top: 5px">
                                                                Seleccione la Sucursal: 
                                                            </div>
                                                            <div class="col-md-12" style="margin-top: 5px">

                                                                <div class="form-group">
                                                                    <div class="col-md-4">
                                                                        <asp:Label ID="Label21" runat="server" Text="Sucursal" />
                                                                    </div>
                                                                    <div class="col-md-8">
                                                                        <dx:BootstrapComboBox ID="CMBSucursalRepresentante" AutoPostBack="true" OnSelectedIndexChanged="CmbSucursalRepresentante_SelectedIndexChanged" ClientInstanceName="SucursalTR" runat="server">
                                                                        </dx:BootstrapComboBox>
                                                                        <dx:BootstrapComboBox ID="BootstrapComboBox1" ClientInstanceName="CmbSucursal" runat="server" Visible="false">
                                                                        </dx:BootstrapComboBox>

                                                                    </div>
                                                                </div>
                                                            </div>



                                                        </div>
                                                    </asp:WizardStep>

                                                    <asp:WizardStep ID="WizardStep3" runat="server" Title="3">
                                                        <div class="content">
                                                            <div class="col-md-12" style="margin-top: 5px;">



                                                                <div class="col-md-12" style="margin-top: 5px">
                                                                    Seleccione:
                                                                </div>
                                                                <div class="col-md-12" style="margin-top: 5px">
                                                                    <dx:BootstrapRadioButtonList ID="TipoRikGerente2" ClientInstanceName="tiporikgerente2" runat="server" RepeatColumns="1">
                                                                        <Items>
                                                                            <dx:BootstrapListEditItem Value="0" Text="Por Rik" Selected="true"></dx:BootstrapListEditItem>
                                                                            <dx:BootstrapListEditItem Value="1" Text="Por Gerente"></dx:BootstrapListEditItem>
                                                                        </Items>
                                                                    </dx:BootstrapRadioButtonList>
                                                                </div>


                                                            </div>
                                                        </div>
                                                    </asp:WizardStep>

                                                    <asp:WizardStep ID="WizardStep4" runat="server" Title="4">
                                                        <div class="content">

                                                            <div class="col-md-12" style="margin-top: 5px">

                                                                <div class="col-md-4" style="margin-top: 5px">
                                                                    Seleccione :
                                                                </div>

                                                                <div class="col-md-8" style="margin-top: 45px">

                                                                    <dx:BootstrapComboBox ID="RBLGerente2" ClientInstanceName="ListaGerente" runat="server">
                                                                    </dx:BootstrapComboBox>

                                                                </div>
                                                            </div>

                                                            <div class="col-md-12" style="margin-top: 5px">
                                                                <div class="form-group">

                                                                    <div class="col-md-8" style="margin-top: 5px">

                                                                        <dx:BootstrapListBox ID="RBLrepresentante2" SelectionMode="CheckColumn" EnableSelectAll="true" Enabled="True" ClientInstanceName="ListaRepresentante2" runat="server">
                                                                        </dx:BootstrapListBox>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                    </asp:WizardStep>


                                                </WizardSteps>
                                                <StepNavigationTemplate>
                                                    <div class="col-md-12" style="margin-top: 40px">
                                                        <asp:Button ID="StepPreviousButton" runat="server" CausesValidation="False" CssClass="btn btn-inverse btn-custom-sm"
                                                            CommandName="MovePrevious" Text="Anterior" />

                                                        <asp:Button ID="StepNextButton" runat="server" CommandName="MoveNext" class="btn btn-primary"
                                                            Text="Siguiente" />
                                                    </div>
                                                </StepNavigationTemplate>

                                                <FinishNavigationTemplate>


                                                    <div class="col-md-12" style="margin-top: 50px">
                                                        <asp:Button ID="FinishPreviousButton" CssClass="btn btn-inverse btn-custom-sm" runat="server" CausesValidation="False" CommandName="MovePrevious"
                                                            Text="Anterior" />
                                                        <button id="FinishButton" type="button" class="btn btn-primary"
                                                            runat="server" onclick="graficaTiempoRespuesta()">
                                                            <span>Generar estadística</span>
                                                        </button>
                                                    </div>
                                                </FinishNavigationTemplate>
                                                <HeaderTemplate>
                                                    <ul id="wizHeader">
                                                        <asp:Repeater ID="SideBarList" runat="server">
                                                            <ItemTemplate>
                                                                <li><a class="<%# GetClassForWizardStep(Container.DataItem) %>" title="<%#Eval("Name")%>">
                                                                    <%# Eval("Name")%></a> </li>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </ul>
                                                </HeaderTemplate>
                                                <StartNextButtonStyle CssClass="btn btn-primary btn-custom-sm" />
                                                <StepPreviousButtonStyle CssClass="btn btn-inverse btn-custom-sm" />
                                                <StepNextButtonStyle CssClass="btn btn-primary btn-custom-sm" />
                                                <FinishPreviousButtonStyle CssClass="btn btn-inverse btn-custom-sm" />
                                                <FinishCompleteButtonStyle CssClass="btn btn-success btn-custom-sm" />
                                            </asp:Wizard>

                                        </div>
                                        <div class="col-md-12" style="margin-top: 45px">
                                            <div class="col-md-2">
                                                <asp:HiddenField runat="server" ID="fecharikvsrikinicial" Value="" />
                                                <asp:HiddenField runat="server" ID="fecharikvsrikfinal" Value="" />
                                                <asp:HiddenField runat="server" ID="SelecciontipoGerente" Value="" />
                                                <asp:HiddenField runat="server" ID="SucursalRepresentante" Value="" />

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12" style="margin-top: 45px">
                                        <div class="col-md-8">
                                            <div class="col-md-12" style="margin-top: 5px">
                                                <canvas id="myTRBar" width="1000" height="450"></canvas>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <div class="col-md-8">
                                            <div class="row" style="margin-top: 10px;">
                                                <canvas id="myTRPie" width="700" height="250"></canvas>
                                            </div>
                                        </div>
                                    </div>

                                    <%-- <div class="row">
                                        <div class="col-md-12">
                                            <canvas id="myChartCompRep" style="width: 700px; height: 250px"></canvas>
                                        </div>
                                    </div>--%>
                                </ContentTemplate>

                            </asp:UpdatePanel>
                            <asp:HiddenField runat="server" ID="txtrepre" Value="" />
                        </div>
                    </div>
                </div>


                <%--cierra div de el tab de información--%>
            </div>


            <%--TAB Descargar informacion --%>
            <div class="tab-pane fade" id="tabInfo">

                <%-- Buscar Informacion --%>
                <div class="panel panel-success" style="margin-top: 15px;">
                    <div class="panel-heading">
                        <h3 class="panel-title">Buscar Información</h3>
                        <span class="pull-right clickable panel-collapsed"><i class="glyphicon glyphicon-chevron-up"></i>
                        </span>
                    </div>
                    <div class="panel-body">

                        <asp:UpdatePanel runat="server" ID="UPdBusacarinfo" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="col-md-12" style="margin-top: 5px;">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <div class="col-md-4">
                                                <asp:Label ID="Label15" runat="server" Text="Medio de Comunicación" />
                                            </div>
                                            <div class="col-md-8">
                                                <dx:BootstrapComboBox ID="CmbMedioContacto" runat="server" AutoPostBack="true">
                                                </dx:BootstrapComboBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <div class="col-md-4">
                                                <asp:Label ID="Label30" runat="server" Text="Fecha Inicial" />
                                            </div>
                                            <div class="col-md-8">
                                                <dx:ASPxDateEdit runat="server" CssClass="form-control" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="txtfechaInicialBuscarInformacion" PickerType="Months">
                                                    <CalendarProperties>
                                                        <FastNavProperties InitialView="Years" MaxView="Years" />
                                                    </CalendarProperties>
                                                </dx:ASPxDateEdit>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--//agregar medio de comunicación y giro de la empresa --%>

                                <div class="col-md-12" style="margin-top: 5px;">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <div class="col-md-4">
                                                <asp:Label ID="Label25" runat="server" Text="Giro Empresa" />
                                            </div>
                                            <div class="col-md-8">
                                                <dx:BootstrapComboBox ID="CmbGiroEmpresa" runat="server">
                                                </dx:BootstrapComboBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <div class="col-md-4">
                                                <asp:Label ID="Label26" runat="server" Text="Fecha Final" />
                                            </div>
                                            <div class="col-md-8">
                                                <dx:ASPxDateEdit runat="server" CssClass="form-control" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="txtfechaFinalBuscarInformacion" PickerType="Months">
                                                    <CalendarProperties>
                                                        <FastNavProperties InitialView="Years" MaxView="Years" />
                                                    </CalendarProperties>
                                                </dx:ASPxDateEdit>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <button id="btnBuscarInformacion" type="button" class="btn btn-primary" title="Consultar "
                                            runat="server" onserverclick="btnBuscarInformacion_ServerClick">
                                            <span>Consultar</span>
                                        </button>
                                    </div>

                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <%--<asp:AsyncPostBackTrigger ControlID="cmbSucursalBinformacion" />--%>
                                <asp:AsyncPostBackTrigger ControlID="btnBuscarInformacion" />
                                <%--<asp:AsyncPostBackTrigger ControlID="btndescragrBuscarInformacion" />--%>
                            </Triggers>
                        </asp:UpdatePanel>
                        <div class="col-md-12" style="margin-top: 5px;">
                            <div class="col-md-4">
                                <div class="col-md-4">
                                                <asp:Label ID="Label11" runat="server" Text="Estatus" />
                                            </div>
                                            <div class="col-md-8">
                                                <dx:BootstrapComboBox ID="CmbEstatus" runat="server">
                                                </dx:BootstrapComboBox>
                                            </div>
                            </div>
                            <div class="col-md-4">
                            </div>

                            <div class="col-md-2">
                                <button id="Button2" type="button" class="btn btn-warning"
                                    title="Descargar Detalle" runat="server" onserverclick="btndescragrBuscarInformacion_ServerClick">
                                    <i class="fa fa-download"></i>Descargar
                                </button>

                            </div>
                            <div class="col-md-1">

                                <dx:BootstrapComboBox ID="cmbBuscarRepresentante" runat="server" Visible="false">
                                </dx:BootstrapComboBox>



                            </div>
                        </div>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel6" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="col-md-12" style="margin-top: 5px;">

                                    <dx:BootstrapGridView ID="gridBuscar" ClientInstanceName="grid" runat="server" KeyFieldName="IdLeads"
                                        Width="100%" AutoGenerateColumns="False">
                                        <Columns>
                                            <dx:BootstrapGridViewDataColumn Width="50px" FieldName="FechaAlta" Caption="Fecha" />
                                            <dx:BootstrapGridViewDataColumn Width="50px" FieldName="IdLeads" Caption="Núm. Leads" />
                                            <dx:BootstrapGridViewDataColumn Width="80px" FieldName="NomEstatus" Caption="Estatus" />
                                            <dx:BootstrapGridViewDataColumn Width="50px" FieldName="IdMedioComunicacion" Caption="IdMedioComunicacion" Visible="false"/>
                                            <dx:BootstrapGridViewDataColumn Width="180px" FieldName="MedioComunicacion" Caption="Medio de comunicación"/>                                            
                                            <dx:BootstrapGridViewDataColumn Width="250px" FieldName="GiroEmpresa" Caption="Giro de la empresa"/>
                                            <dx:BootstrapGridViewDataColumn Width="200px" FieldName="NombreEmpresa" Caption="Nombre de la empresa"/>                                           
                                            <dx:BootstrapGridViewTextColumn Width="100px" FieldName="NumeroEmpleados" Caption="Numeros de Empleados"/>
                                            <dx:BootstrapGridViewTextColumn Width="150px" FieldName="Estado" Caption="Estado"/> 
                                            <dx:BootstrapGridViewDataColumn Width="200px" FieldName="Ciudad" Caption="Ciudad"/>
                                            <dx:BootstrapGridViewDataColumn Width="150px" FieldName="NomCDI" Caption="Sucursal"/>
                                            <dx:BootstrapGridViewDataColumn Width="180px" FieldName="NombreContacto" Caption="Nombre Contacto"  Visible="false"/>
                                            <dx:BootstrapGridViewDataColumn Width="200px" FieldName="Correo" Caption="Email"  Visible="false"/>
                                            <dx:BootstrapGridViewDataColumn Width="200px" FieldName="Telefono" Caption="Teléfono"  Visible="false"/>
                                            <dx:BootstrapGridViewDataColumn Width="300px" FieldName="ProductoInteres" Caption="Productos de interes"  Visible="false"/>
                                            <dx:BootstrapGridViewDataColumn Width="300px" FieldName="Comentarios" Caption="Comentarios" Visible="false"/>
                                            <dx:BootstrapGridViewDataColumn Width="300px" FieldName="MotivoCanceladoGerente" Caption="Motivo cancelado Gerente" Visible="false"/>
                                            <dx:BootstrapGridViewDataColumn Width="300px" FieldName="MotivoCanceladoRik"     Caption="Motivo cancelado Rik" Visible="false"/>
                                            
                                        </Columns>

                                        <SettingsPager PageSize="50" />
                                    </dx:BootstrapGridView>

                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                </div>

            </div>
            
        </div>
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
    <div id="modalQuestion" class="modal" role="dialog" tabindex="-1" style="z-index: 1220!important;"
        style="display: none;">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 id="modalAcysMensaje_Titulo">Mensaje</h4>
                </div>
                <div class="modal-body" id="Div11" style="overflow-y: hidden
    !Important;">
                    <div class="col-md-12">
                        <div class="col-md-1">
                            <i id="modalAcysMensaje_Icon" class="fa fa-exclamation-triangle_x" aria-hidden="true"></i>
                        </div>
                        <div class="col-md-10">
                            <span id="lblquestion" runat="server"></span>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-12 ">
                            <button class="btn btn-default" data-dismiss="modal" id="btnSi" runat="server">
                                Sí</button>
                            <button class="btn
    btn-default"
                                data-dismiss="modal" id="btnNo">
                                No</button>
                        </div>
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
        function getRandomColor() {
            var letters = '0123456789ABCDEF'.split('');
            var color = '#';
            for (var i = 0; i < 6; i++) {
                color += letters[Math.floor(Math.random() * 16)];
            }
            return color;
        }
        var COLOR_Analisis = '#abebc6';
        var COLOR_Promocion = '#58d68d';
        var COLOR_Negociacion = '#f7CD6F';
        var COLOR_Cierre = '#85c1e9';
        var COLOR_Cancelado = '#d7DBDD';
        var color = Chart.helpers.color;
        function BtnObservarTotales() {
            $('#<%= UpdateProgress1.ClientID %>').css("display", "block");
            var seleccion = TipoOT.GetValue();
            var Sucursal = -1;
            var jsDate = AnioInicialOT.GetDate();
            var year = jsDate.getFullYear(); // where getFullYear returns the year (four digits)  
            var month = jsDate.getMonth() + 1; // where getMonth returns the month (from 0-11)  
            var day = jsDate.getDate();   // where getDate returns the day of the month (from 1-31)  
            if (month < 10) {
                var myDate = `${day}/0${month}/${year}`
            } else {
                var myDate = `${day}/${month}/${year}`
            }
            var jsDate2 = AnioFinalOT.GetDate();
            var year2 = jsDate2.getFullYear(); // where getFullYear returns the year (four digits)  
            var month2 = jsDate2.getMonth() + 1; // where getMonth returns the month (from 0-11)  
            var day2 = jsDate2.getDate();   // where getDate returns the day of the month (from 1-31)  
            if (month2 < 10) {
                var myDateFinal = `${day2}/0${month2}/${year2}`
            } else {
                var myDateFinal = `${day2}/${month2}/${year2}`
            }
            var dataValue = "{mesAnioInicial: '" + myDate + "', mesAniofinal: '" + myDateFinal + "', seleccion: '" + seleccion + "', Sucursal: '" + Sucursal + "'}";
            $.ajax({
                type: "POST",
                url: "ReporteComercialesLeads.aspx/ObservarTotales",
                data: dataValue,
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
                            document.getElementById('<%=total.ClientID%>').innerHTML = data.total;
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
                                coloR.push(dynamicColors());
                            });
                            var myChart2;
                            var ctx = document.getElementById('myChart');
                            if (window.myChart2 != undefined) {
                                window.myChart2.destroy();
                            }
                            window.myChart2 = new Chart(ctx, {
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
                                    tooltips: {
                                        callbacks: {
                                            label: function (tooltipItem, data) {
                                                var indice = tooltipItem.index;
                                                return data.labels[indice] + ': ' + data.datasets[0].data[indice].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                            }
                                        }
                                    },
                                    pieceLabel: {
                                        render: function (args) {
                                            return '' + args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
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
            var rik = 0;
            var seleccion = tiposvs.GetValue();
            var jsDate = FechaInicialPV.GetDate();
            var year = jsDate.getFullYear(); // where getFullYear returns the year (four digits)  
            var month = jsDate.getMonth() + 1; // where getMonth returns the month (from 0-11)  
            var day = jsDate.getDate();   // where getDate returns the day of the month (from 1-31)  
            if (month < 10) {
                var myDate = `${day}/0${month}/${year}`
            } else {
                var myDate = `${day}/${month}/${year}`
            }
            var jsDate2 = FechaFinalPV.GetDate();
            var year2 = jsDate2.getFullYear(); // where getFullYear returns the year (four digits)  
            var month2 = jsDate2.getMonth() + 1; // where getMonth returns the month (from 0-11)  
            var day2 = jsDate2.getDate();   // where getDate returns the day of the month (from 1-31)  
            if (month2 < 10) {
                var myDateFinal = `${day2}/0${month2}/${year2}`
            } else {
                var myDateFinal = `${day2}/${month2}/${year2}`
            }
            var dataValue = "{mesAnioInicial: '" + myDate + "', mesAniofinal: '" + myDateFinal + "', Seleccion: '" + seleccion + "'}";
            $.ajax({
                type: "POST",
                url: "ReporteComercialesLeads.aspx/btnIntegrarLeads_ServerClick",
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
                        var cantidadsucusales = data.cs;
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
                            var titsucursal = data.titsucursal;
                            var titsucursalStrArr = titsucursal.split(",")
                            var titsucursalArr = [];
                            titsucursalStrArr.forEach(function (data, index, arr) {
                                var nombre = data;
                                titsucursalArr.push(nombre);
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
                            var datos4 = data.datos4;
                            var datosstrArr4 = datos4.split(",")
                            var datosArr4 = [];
                            datosstrArr4.forEach(function (data, index, arr) {
                                datosArr4.push(+data);
                            });
                            var datos5 = data.datos5;
                            var datosstrArr5 = datos5.split(",")
                            var datosArr5 = [];
                            datosstrArr5.forEach(function (data, index, arr) {
                                datosArr5.push(+data);
                            });
                            var datos6 = data.datos6;
                            var datosstrArr6 = datos6.split(",")
                            var datosArr6 = [];
                            datosstrArr6.forEach(function (data, index, arr) {
                                datosArr6.push(+data);
                            });
                            var datos7 = data.datos7;
                            var datosstrArr7 = datos7.split(",")
                            var datosArr7 = [];
                            tienedatos7 = 0;
                            datosstrArr7.forEach(function (data, index, arr) {
                                datosArr7.push(+data);
                            });
                            var datos8 = data.datos8;
                            var datosstrArr8 = datos8.split(",")
                            var datosArr8 = [];
                            datosstrArr8.forEach(function (data, index, arr) {
                                datosArr8.push(+data);
                            });
                            var datos9 = data.datos9;
                            var datosstrArr9 = datos9.split(",")
                            var datosArr9 = [];
                            datosstrArr9.forEach(function (data, index, arr) {
                                datosArr9.push(+data);
                            });
                            var lineChartData = {
                                labels: tituloArr,
                                datasets: [{
                                    label: titsucursalArr[0],
                                    borderColor: "rgb(205,92,92)",
                                    backgroundColor: "rgb(205,92,92)",
                                    fill: false,
                                    data: datosArr,
                                    yAxisID: "y-axis-1",
                                }, {
                                    label: titsucursalArr[1],
                                    borderColor: "rgb(0, 191, 255)",
                                    backgroundColor: "rgb(0, 191, 255)",
                                    fill: false,
                                    data: datosArr2,
                                    yAxisID: "y-axis-1",
                                }, {
                                    label: titsucursalArr[2],
                                    borderColor: "rgb(0, 199, 255)",
                                    backgroundColor: "rgb(0, 199, 255)",
                                    fill: false,
                                    data: datosArr3,
                                    yAxisID: "y-axis-1",
                                }, {
                                    label: titsucursalArr[3],
                                    borderColor: "rgba(99, 255, 132, 1)",
                                    backgroundColor: "rgba(99, 255, 132, 0.2)",
                                    fill: false,
                                    data: datosArr4,
                                    yAxisID: "y-axis-1",
                                }, {
                                    label: titsucursalArr[4],
                                    borderColor: "rgb(102, 0, 255)",
                                    backgroundColor: "rgb(102, 0, 255)",
                                    fill: false,
                                    data: datosArr5,
                                    yAxisID: "y-axis-1",
                                }, {
                                    label: titsucursalArr[5],
                                    borderColor: "rgb(153, 51, 153)",
                                    backgroundColor: "rgb(153, 51, 153)",
                                    fill: false,
                                    data: datosArr6,
                                    yAxisID: "y-axis-1",
                                }, {
                                    label: titsucursalArr[6],
                                    borderColor: "rgb(0, 102, 0)",
                                    backgroundColor: "rgb(0, 102, 0)",
                                    fill: false,
                                    data: datosArr7,
                                    yAxisID: "y-axis-1",
                                }, {
                                    label: titsucursalArr[7],
                                    borderColor: "rgb(179, 255, 255)",
                                    backgroundColor: "rgb(179, 255, 255)",
                                    fill: false,
                                    data: datosArr8,
                                    yAxisID: "y-axis-1",
                                }, {
                                    label: titsucursalArr[9],
                                    borderColor: "rgb(230, 230, 0)",
                                    backgroundColor: "rgb(230, 230, 0)",
                                    fill: false,
                                    data: datosArr9,
                                    yAxisID: "y-axis-1"
                                }]
                            };
                            var dataSets = [];
                            for (var i = 0; i < cantidadsucusales; i++) {
                                if (i == 0) {
                                    var dataSet = {
                                        label: titsucursalArr[0],
                                        borderColor: "rgb(204, 51, 0)",
                                        backgroundColor: "rgb(204, 51, 0)",
                                        fill: false,
                                        borderWidth: 1,
                                        data: datosArr,
                                        yAxisID: "y-axis-1",
                                    };
                                    dataSets.push(dataSet);
                                }
                                if (i == 1) {
                                    var dataSet = {
                                        label: titsucursalArr[1],
                                        borderColor: "rgb(0, 199, 255)",
                                        backgroundColor: "rgb(0, 199, 255)",
                                        fill: false,
                                        borderWidth: 1,
                                        data: datosArr2,
                                        yAxisID: "y-axis-1",
                                    };
                                    dataSets.push(dataSet);
                                }
                                if (i == 2) {
                                    var dataSet = {
                                        label: titsucursalArr[2],
                                        borderColor: "rgba(99, 255, 132, 1)",
                                        backgroundColor: "rgba(99, 255, 132, 0.2)",
                                        fill: false,
                                        borderWidth: 1,
                                        data: datosArr3,
                                        yAxisID: "y-axis-1",
                                    };
                                    dataSets.push(dataSet);
                                }
                                if (i == 3) {
                                    var dataSet = {
                                        label: titsucursalArr[i],
                                        borderColor: "rgb(102, 0, 255)",
                                        backgroundColor: "rgb(102, 0, 255)",
                                        fill: false,
                                        borderWidth: 1,
                                        data: datosArr4,
                                        yAxisID: "y-axis-1",
                                    };
                                    dataSets.push(dataSet);
                                }
                                if (i == 4) {
                                    var dataSet = {
                                        label: titsucursalArr[i],
                                        borderColor: "rgb(153, 51, 153)",
                                        backgroundColor: "rgb(153, 51, 153)",
                                        fill: false,
                                        borderWidth: 1,
                                        data: datosArr5,
                                        yAxisID: "y-axis-1",
                                    };
                                    dataSets.push(dataSet);
                                }
                                if (i == 5) {
                                    var dataSet = {
                                        label: titsucursalArr[i],
                                        borderColor: "rgb(0, 102, 0)",
                                        backgroundColor: "rgb(0, 102, 0)",
                                        fill: false,
                                        borderWidth: 1,
                                        data: datosArr6,
                                        yAxisID: "y-axis-1",
                                    };
                                    dataSets.push(dataSet);
                                }
                                if (i == 6) {
                                    var dataSet = {
                                        label: titsucursalArr[i],
                                        borderColor: "rgb(92, 92, 138)",
                                        backgroundColor: "rgb(92, 92, 138)",
                                        fill: false,
                                        borderWidth: 1,
                                        data: datosArr7,
                                        yAxisID: "y-axis-1",
                                    };
                                    dataSets.push(dataSet);
                                }
                                if (i == 7) {
                                    var dataSet = {
                                        label: titsucursalArr[i],
                                        borderColor: "rgb(230, 230, 0)",
                                        backgroundColor: "rgb(230, 230, 0)",
                                        fill: false,
                                        borderWidth: 1,
                                        data: datosArr8,
                                        yAxisID: "y-axis-1",
                                    };
                                    dataSets.push(dataSet);
                                }
                                if (i == 8) {
                                    var dataSet = {
                                        label: titsucursalArr[i],
                                        backgroundColor: "rgb(36, 143, 36)",
                                        borderColor: "rgb(36, 143, 36)",
                                        fill: false,
                                        borderWidth: 1,
                                        data: datosArr9,
                                        yAxisID: "y-axis-1",
                                    };
                                    dataSets.push(dataSet);
                                }
                            }
                            //data: lineChartData,
                            window.myChart4 = new Chart(ctx, {
                                type: 'line',
                                data: { labels: tituloArr, datasets: dataSets },
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
                                        text: 'Integración de Leads',
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
                                                return '$' + args.value;
                                            },
                                            fontColor: '#000',
                                        }
                                    },
                                    tooltips: {
                                        callbacks: {
                                            label: function (tooltipItem, data) {
                                                return ' ' + tooltipItem.yLabel.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                            }
                                        }
                                    },
                                }
                            });
                        }
                        if (id == 6) {
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
                            var titsucursal = data.titsucursal;
                            var titsucursalStrArr = titsucursal.split(",")
                            var titsucursalArr = [];
                            titsucursalStrArr.forEach(function (data, index, arr) {
                                var nombre = data;
                                titsucursalArr.push(nombre);
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
                            var datos4 = data.datos4;
                            var datosstrArr4 = datos4.split(",")
                            var datosArr4 = [];
                            datosstrArr4.forEach(function (data, index, arr) {
                                datosArr4.push(+data);
                            });
                            var datos5 = data.datos5;
                            var datosstrArr5 = datos5.split(",")
                            var datosArr5 = [];
                            datosstrArr5.forEach(function (data, index, arr) {
                                datosArr5.push(+data);
                            });
                            var datos6 = data.datos6;
                            var datosstrArr6 = datos6.split(",")
                            var datosArr6 = [];
                            datosstrArr6.forEach(function (data, index, arr) {
                                datosArr6.push(+data);
                            });
                            var datos7 = data.datos7;
                            var datosstrArr7 = datos7.split(",")
                            var datosArr7 = [];
                            tienedatos7 = 0;
                            datosstrArr7.forEach(function (data, index, arr) {
                                datosArr7.push(+data);
                            });
                            var datos8 = data.datos8;
                            var datosstrArr8 = datos8.split(",")
                            var datosArr8 = [];
                            datosstrArr8.forEach(function (data, index, arr) {
                                datosArr8.push(+data);
                            });
                            var datos9 = data.datos9;
                            var datosstrArr9 = datos9.split(",")
                            var datosArr9 = [];
                            datosstrArr9.forEach(function (data, index, arr) {
                                datosArr9.push(+data);
                            });
                            var lineChartData = {
                                labels: tituloArr,
                                datasets: [{
                                    label: titsucursalArr[0],
                                    borderColor: "rgb(205,92,92)",
                                    backgroundColor: "rgb(205,92,92)",
                                    fill: false,
                                    data: datosArr,
                                    yAxisID: "y-axis-1",
                                }, {
                                    label: titsucursalArr[1],
                                    borderColor: "rgb(0, 191, 255)",
                                    backgroundColor: "rgb(0, 191, 255)",
                                    fill: false,
                                    data: datosArr2,
                                    yAxisID: "y-axis-1",
                                }, {
                                    label: titsucursalArr[2],
                                    borderColor: "rgb(0, 199, 255)",
                                    backgroundColor: "rgb(0, 199, 255)",
                                    fill: false,
                                    data: datosArr3,
                                    yAxisID: "y-axis-1",
                                }, {
                                    label: titsucursalArr[3],
                                    borderColor: "rgba(99, 255, 132, 1)",
                                    backgroundColor: "rgba(99, 255, 132, 0.2)",
                                    fill: false,
                                    data: datosArr4,
                                    yAxisID: "y-axis-1",
                                }, {
                                    label: titsucursalArr[4],
                                    borderColor: "rgb(102, 0, 255)",
                                    backgroundColor: "rgb(102, 0, 255)",
                                    fill: false,
                                    data: datosArr5,
                                    yAxisID: "y-axis-1",
                                }, {
                                    label: titsucursalArr[5],
                                    borderColor: "rgb(153, 51, 153)",
                                    backgroundColor: "rgb(153, 51, 153)",
                                    fill: false,
                                    data: datosArr6,
                                    yAxisID: "y-axis-1",
                                }, {
                                    label: titsucursalArr[6],
                                    borderColor: "rgb(0, 102, 0)",
                                    backgroundColor: "rgb(0, 102, 0)",
                                    fill: false,
                                    data: datosArr7,
                                    yAxisID: "y-axis-1",
                                }, {
                                    label: titsucursalArr[7],
                                    borderColor: "rgb(179, 255, 255)",
                                    backgroundColor: "rgb(179, 255, 255)",
                                    fill: false,
                                    data: datosArr8,
                                    yAxisID: "y-axis-1",
                                }, {
                                    label: titsucursalArr[9],
                                    borderColor: "rgb(230, 230, 0)",
                                    backgroundColor: "rgb(230, 230, 0)",
                                    fill: false,
                                    data: datosArr9,
                                    yAxisID: "y-axis-1"
                                }]
                            };
                            var dataSets = [];
                            for (var i = 0; i < cantidadsucusales; i++) {
                                if (i == 0) {
                                    var dataSet = {
                                        label: titsucursalArr[0],
                                        borderColor: "rgb(204, 51, 0)",
                                        backgroundColor: "rgb(204, 51, 0)",
                                        fill: false,
                                        borderWidth: 1,
                                        data: datosArr,
                                        yAxisID: "y-axis-1",
                                    };
                                    dataSets.push(dataSet);
                                }
                                if (i == 1) {
                                    var dataSet = {
                                        label: titsucursalArr[1],
                                        borderColor: "rgb(0, 199, 255)",
                                        backgroundColor: "rgb(0, 199, 255)",
                                        fill: false,
                                        borderWidth: 1,
                                        data: datosArr2,
                                        yAxisID: "y-axis-1",
                                    };
                                    dataSets.push(dataSet);
                                }
                                if (i == 2) {
                                    var dataSet = {
                                        label: titsucursalArr[2],
                                        borderColor: "rgba(99, 255, 132, 1)",
                                        backgroundColor: "rgba(99, 255, 132, 0.2)",
                                        fill: false,
                                        borderWidth: 1,
                                        data: datosArr3,
                                        yAxisID: "y-axis-1",
                                    };
                                    dataSets.push(dataSet);
                                }
                                if (i == 3) {
                                    var dataSet = {
                                        label: titsucursalArr[i],
                                        borderColor: "rgb(102, 0, 255)",
                                        backgroundColor: "rgb(102, 0, 255)",
                                        fill: false,
                                        borderWidth: 1,
                                        data: datosArr4,
                                        yAxisID: "y-axis-1",
                                    };
                                    dataSets.push(dataSet);
                                }
                                if (i == 4) {
                                    var dataSet = {
                                        label: titsucursalArr[i],
                                        borderColor: "rgb(153, 51, 153)",
                                        backgroundColor: "rgb(153, 51, 153)",
                                        fill: false,
                                        borderWidth: 1,
                                        data: datosArr5,
                                        yAxisID: "y-axis-1",
                                    };
                                    dataSets.push(dataSet);
                                }
                                if (i == 5) {
                                    var dataSet = {
                                        label: titsucursalArr[i],
                                        borderColor: "rgb(0, 102, 0)",
                                        backgroundColor: "rgb(0, 102, 0)",
                                        fill: false,
                                        borderWidth: 1,
                                        data: datosArr6,
                                        yAxisID: "y-axis-1",
                                    };
                                    dataSets.push(dataSet);
                                }
                                if (i == 6) {
                                    var dataSet = {
                                        label: titsucursalArr[i],
                                        borderColor: "rgb(92, 92, 138)",
                                        backgroundColor: "rgb(92, 92, 138)",
                                        fill: false,
                                        borderWidth: 1,
                                        data: datosArr7,
                                        yAxisID: "y-axis-1",
                                    };
                                    dataSets.push(dataSet);
                                }
                                if (i == 7) {
                                    var dataSet = {
                                        label: titsucursalArr[i],
                                        borderColor: "rgb(230, 230, 0)",
                                        backgroundColor: "rgb(230, 230, 0)",
                                        fill: false,
                                        borderWidth: 1,
                                        data: datosArr8,
                                        yAxisID: "y-axis-1",
                                    };
                                    dataSets.push(dataSet);
                                }
                                if (i == 8) {
                                    var dataSet = {
                                        label: titsucursalArr[i],
                                        backgroundColor: "rgb(36, 143, 36)",
                                        borderColor: "rgb(36, 143, 36)",
                                        fill: false,
                                        borderWidth: 1,
                                        data: datosArr9,
                                        yAxisID: "y-axis-1",
                                    };
                                    dataSets.push(dataSet);
                                }
                            }
                            //data: lineChartData,
                            window.myChart4 = new Chart(ctx, {
                                type: 'line',
                                data: { labels: tituloArr, datasets: dataSets },
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
                                        text: 'Integración de Leads',
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
                                                return '$' + args.value;
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
        //esta gráfica es la de efectividad por sucursales 18 febrero 2021
        function graficaEfectividadSucursal() {
            $('#<%= UpdateProgress1.ClientID %>').css("display", "block");
            var jsDate = AnioInicialEF.GetDate();
            var year = jsDate.getFullYear(); // where getFullYear returns the year (four digits)  
            var month = jsDate.getMonth() + 1; // where getMonth returns the month (from 0-11)  
            var day = jsDate.getDate();   // where getDate returns the day of the month (from 1-31)  
            var seleccion = tipoef.GetValue();
            var Sucursal = SucursalOT.GetValue();
            var representantes = ListaRepresentanteef.GetSelectedValues();
            if (month < 10) {
                var myDate = `${day}/0${month}/${year}`
            } else {
                var myDate = `${day}/${month}/${year}`
            }
            var jsDate2 = AnioFinalEF.GetDate();
            var year2 = jsDate2.getFullYear(); // where getFullYear returns the year (four digits)  
            var month2 = jsDate2.getMonth() + 1; // where getMonth returns the month (from 0-11)  
            var day2 = jsDate2.getDate();   // where getDate returns the day of the month (from 1-31)  
            if (month2 < 10) {
                var myDateFinal = `${day2}/0${month2}/${year2}`
            } else {
                var myDateFinal = `${day2}/${month2}/${year2}`
            }
            console.log(Sucursal);
            var dataValue = "{Seleccion: '" + seleccion + "', sucursales: '" + Sucursal + "', mesAnioInicial: '" + myDate + "', mesAniofinal: '" + myDateFinal + "', representantes: '" + representantes + "'}";
            console.log(dataValue);
            $.ajax({
                type: "POST",
                url: "ReporteComercialesLeads.aspx/EfectividadSucursales",
                data: dataValue,
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    modalMensaje(errorThrown);
                },
                success: function (response) {
                    debugger;
                    console.log('regresa con exito');
                    if (response != null && response.d != null) {
                        var data = response.d;
                        data = $.parseJSON(data);
                        var id = data.id;
                        if (id == -1) {
                            modalMensaje(data.men);
                        }
                        if (id == 1) {
                            modalMensaje("Favor de seleccionar la sucursal en la sección de efectividad.");
                        }
                        if (id == 2) {
                            modalMensaje("Favor de seleccionar la fecha inicial y/o final de la sección de efectividad.");
                        }
                        if (id == 3) {
                            modalMensaje("La fecha inicial es mayor a la fecha final  de la sección de efectividad.");
                        }
                        if (id == 6) {
                            modalMensaje("Favor de seleccionar al menos un rik en la sección de efectividad.");
                        }
                        if (id == 5) {
                            console.log('regresa 5');
                            var titulo = data.Nombre;
                            var tituloStrArr = titulo.split(",")
                            var tituloArr = [];
                            tituloStrArr.forEach(function (data, index, arr) {
                                var nombre = data;
                                tituloArr.push(nombre);
                            });
                            //JFCV gráfica de pie
                            var titulo2 = data.titulo2;
                            var tituloStrArr2 = titulo2.split(",")
                            var tituloArr2 = [];
                            tituloStrArr2.forEach(function (data, index, arr) {
                                var nombre = data;
                                tituloArr2.push(nombre);
                            });
                                var datos = data.Analisis;  //En Desarrollo
                            var datosstrArr = datos.split(",")
                            var datosArr = [];
                            datosstrArr.forEach(function (data, index, arr) {
                                datosArr.push(+data);
                            });
                                var datos2 = data.datos2;  //para la de PIE
                            var datosstrArr2 = datos2.split(",")
                            var datosArr2 = [];
                            datosstrArr2.forEach(function (data, index, arr) {
                                datosArr2.push(+data);
                            });
                                var datos3 = data.presentacion; //En cierre
                            var datosstrArr3 = datos3.split(",")
                            var datosArr3 = [];
                            datosstrArr3.forEach(function (data, index, arr) {
                                datosArr3.push(+data);
                            });
                                var datos4 = data.Negociación; //cancelados
                            var datosstrArr4 = datos4.split(",")
                            var datosArr4 = [];
                            datosstrArr4.forEach(function (data, index, arr) {
                                datosArr4.push(+data);
                            });
                            var datos5 = data.Cancelación;
                            var datosstrArr5 = datos5.split(",")
                            var datosArr5 = [];
                            datosstrArr5.forEach(function (data, index, arr) {
                                datosArr5.push(+data);
                            });
                            if (window.myChart8 != undefined) {
                                window.myChart8.destroy();
                            }
                            //JFCV agregar la gráfica de Pie con los datos 
                            var myChart2;
                            var ctxpie = document.getElementById('myChartPie');
                            if (window.myChart2 != undefined) {
                                window.myChart2.destroy();
                            }
                            window.myChart2 = new Chart(ctxpie, {
                                type: 'pie',
                                data: {
                                    labels: tituloArr2,
                                    datasets: [{
                                        data: datosArr2,
                                        backgroundColor: [
                                            COLOR_Analisis,
                                            COLOR_Promocion,
                                            COLOR_Negociacion,
                                            COLOR_Cierre,
                                            COLOR_Cancelado
                                        ],
                                        borderWidth: 1,
                                    }]
                                },
                                options: {
                                    responsive: true,
                                    display: true,
                                    legend: {
                                        position: 'left',
                                        labels: {
                                            padding: 10,
                                            fontSize: 10,
                                        }
                                    },
                                    title: {
                                        display: true,
                                        text: 'Monto de proyectos',
                                    },
                                    tooltips: {
                                        callbacks: {
                                            label: function (tooltipItem, data) {
                                                var indice = tooltipItem.index;
                                                return data.labels[indice] + ':' + ' $' + data.datasets[0].data[indice].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                            }
                                        }
                                    },
                                    plugins: {
                                        labels: {
                                            render: function (args) {
                                                    return '$' + args.value;
                                            },
                                            arc: false,
                                            fontColor: '#000',
                                            position: 'outside'
                                        }
                                    },
                                },
                            });
                            var barChartData = {
                                labels: tituloArr,
                                datasets: [{
                                    label: 'En Desarrollo',
                                    backgroundColor: COLOR_Analisis,
                                    data: datosArr,
                                }, {
                                    label: 'En Cierre',
                                    backgroundColor: COLOR_Promocion,
                                        data: datosArr3,
                                }, {
                                    label: 'Cancelados',
                                    backgroundColor: COLOR_Negociacion,
                                        data: datosArr4,
                                }]
                            };
                            var ctx = document.getElementById('myefsucursal');
                            window.myChart8 = new Chart(ctx, {
                                type: 'bar',
                                data: barChartData,
                                options: {
                                    title: {
                                        display: true,
                                        text: 'Monto de proyectos',
                                    },
                                    tooltips: {
                                        mode: 'index',
                                        intersect: false
                                    },
                                    responsive: true,
                                    scales: {
                                        xAxes: [{
                                            stacked: true,
                                        }],
                                        yAxes: [{
                                            stacked: true
                                        }]
                                    },
                                    plugins: {
                                        labels: false,
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
                        if (id == 4) {
                            var titulo = data.Nombre;
                            var tituloStrArr = titulo.split(",")
                            var tituloArr = [];
                            tituloStrArr.forEach(function (data, index, arr) {
                                var nombre = data;
                                tituloArr.push(nombre);
                            });
                            //JFCV gráfica de pie
                            var titulo2 = data.titulo2;
                            var tituloStrArr2 = titulo2.split(",")
                            var tituloArr2 = [];
                            tituloStrArr2.forEach(function (data, index, arr) {
                                var nombre = data;
                                tituloArr2.push(nombre);
                            });
                            var datos = data.Analisis;  //En Desarrollo
                            var datosstrArr = datos.split(",")
                            var datosArr = [];
                            datosstrArr.forEach(function (data, index, arr) {
                                datosArr.push(+data);
                            });
                            var datos2 = data.datos2;  //para la de PIE
                            var datosstrArr2 = datos2.split(",")
                            var datosArr2 = [];
                            datosstrArr2.forEach(function (data, index, arr) {
                                datosArr2.push(+data);
                            });
                            var datos3 = data.presentacion; //En cierre
                            var datosstrArr3 = datos3.split(",")
                            var datosArr3 = [];
                            datosstrArr3.forEach(function (data, index, arr) {
                                datosArr3.push(+data);
                            });
                            var datos4 = data.Negociación; //cancelados
                            var datosstrArr4 = datos4.split(",")
                            var datosArr4 = [];
                            datosstrArr4.forEach(function (data, index, arr) {
                                datosArr4.push(+data);
                            });
                            var datos5 = data.Cancelación;
                            var datosstrArr5 = datos5.split(",")
                            var datosArr5 = [];
                            datosstrArr5.forEach(function (data, index, arr) {
                                datosArr5.push(+data);
                            });
                            if (window.myChart8 != undefined) {
                                window.myChart8.destroy();
                            }
                            //JFCV agregar la gráfica de Pie con los datos 
                            var myChart2;
                            var ctxpie = document.getElementById('myChartPie');
                            if (window.myChart2 != undefined) {
                                window.myChart2.destroy();
                            }
                            window.myChart2 = new Chart(ctxpie, {
                                type: 'pie',
                                data: {
                                    labels: tituloArr2,
                                    datasets: [{
                                        data: datosArr2,
                                        backgroundColor: [
                                            COLOR_Analisis,
                                            COLOR_Promocion,
                                            COLOR_Negociacion,
                                            COLOR_Cierre,
                                            COLOR_Cancelado
                                        ],
                                        borderWidth: 1,
                                    }]
                                },
                                options: {
                                    responsive: true,
                                    display: true,
                                    legend: {
                                        position: 'left',
                                        labels: {
                                            padding: 10,
                                            fontSize: 10,
                                        }
                                    },
                                    title: {
                                        display: true,
                                        text: 'Cantidad de proyectos',
                                    },
                                    tooltips: {
                                        callbacks: {
                                            label: function (tooltipItem, data) {
                                                var indice = tooltipItem.index;
                                                return data.labels[indice] + ':' + data.datasets[0].data[indice].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                            }
                                        }
                                    },
                                    plugins: {
                                        labels: {
                                            render: function (args) {
                                                return args.value;
                                            },
                                            arc: false,
                                            fontColor: '#000',
                                            position: 'outside'
                                        }
                                    },
                                },
                            });
                            var barChartData = {
                                labels: tituloArr,
                                datasets: [{
                                    label: 'En Desarrollo',
                                    backgroundColor: COLOR_Analisis,
                                    data: datosArr,
                                }, {
                                    label: 'En Cierre',
                                    backgroundColor: COLOR_Promocion,
                                    data: datosArr3,
                                }, {
                                    label: 'Cancelados',
                                    backgroundColor: COLOR_Negociacion,
                                    data: datosArr4,
                                }]
                            };
                            var ctx = document.getElementById('myefsucursal');
                            window.myChart8 = new Chart(ctx, {
                                type: 'bar',
                                data: barChartData,
                                options: {
                                    title: {
                                        display: true,
                                        text: 'Cantidad de proyectos',
                                    },
                                    tooltips: {
                                        mode: 'index',
                                        intersect: false
                                    },
                                    responsive: true,
                                    scales: {
                                        xAxes: [{
                                            stacked: true,
                                        }],
                                        yAxes: [{
                                            stacked: true
                                        }]
                                    },
                                    plugins: {
                                        labels: false,
                                    },
                                    tooltips: {
                                        callbacks: {
                                            label: function (tooltipItem, data) {
                                                return '' + tooltipItem.yLabel.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
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
        //gráfica es la de tiempo de respuesta
        function graficaTiempoRespuesta() {
            $('#<%= UpdateProgress1.ClientID %>').css("display", "block");
            var fecha = document.getElementById('<%=fecharikvsrikinicial.ClientID%>').value;
            var fecha2 = document.getElementById('<%=fecharikvsrikfinal.ClientID%>').value;
            var seleccion = document.getElementById('<%=SelecciontipoGerente.ClientID%>').value;
            var Sucursal = document.getElementById('<%=SucursalRepresentante.ClientID%>').value;
            var representantes = "";
            var gerentes = "Sergio Roberto Gimz";
            if (seleccion == "0") {
                representantes = ListaRepresentante2.GetSelectedValues();
            }
            //else
            //{
            //    //gerentes = ListaGerente.GetSelectedValue();
            //}
            var dateParts = fecha.split("/");
            var dateParts2 = fecha2.split("/");
            var currentdate = new Date(+dateParts[2], dateParts[1] - 1, +dateParts[0]);
            var currentdate2 = new Date(+dateParts2[2], dateParts2[1] - 1, +dateParts2[0]);
            //var seleccion = SelecciontipoGerente.GetValue();
            //var Sucursal = ListaGerente.GetSelectedValues();
            var jsDate = currentdate;
            var year = jsDate.getFullYear(); // where getFullYear returns the year (four digits)  
            var month = jsDate.getMonth() + 1; // where getMonth returns the month (from 0-11)  
            var day = jsDate.getDate();   // where getDate returns the day of the month (from 1-31)  
            if (month < 10) {
                var myDate = `${day}/0${month}/${year}`
            } else {
                var myDate = `${day}/${month}/${year}`
            }
            var jsDate2 = currentdate2;
            var year2 = jsDate2.getFullYear(); // where getFullYear returns the year (four digits)  
            var month2 = jsDate2.getMonth() + 1; // where getMonth returns the month (from 0-11)  
            var day2 = jsDate2.getDate();   // where getDate returns the day of the month (from 1-31)  
            if (month2 < 10) {
                var myDateFinal = `${day2}/0${month2}/${year2}`
            } else {
                var myDateFinal = `${day2}/${month2}/${year2}`
            }
            var dataValue = "{Seleccion: '" + seleccion + "', sucursales: '" + Sucursal + "', mesAnioInicial: '" + myDate + "', mesAniofinal: '" + myDateFinal + "', representantes: '" + representantes + "', gerentes: '" + gerentes + "'}";
            console.log(dataValue);
            $.ajax({
                type: "POST",
                url: "ReporteComercialesLeads.aspx/TiempoRespuesta",
                data: dataValue,
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    modalMensaje(errorThrown);
                },
                success: function (response) {
                    debugger;
                    console.log('regresa con exito');
                    if (response != null && response.d != null) {
                        var data = response.d;
                        data = $.parseJSON(data);
                        var id = data.id;
                        if (id == -1) {
                            modalMensaje(data.men);
                        }
                        if (id == 1) {
                            modalMensaje("Favor de seleccionar la sucursal en la sección de tiempo de respuesta.");
                        }
                        if (id == 2) {
                            modalMensaje("Favor de seleccionar la fecha inicial y/o final de la sección de tiempo de respuesta.");
                        }
                        if (id == 3) {
                            modalMensaje("La fecha inicial es mayor a la fecha final  de la sección de tiempo de respuesta.");
                        }
                        if (id == 6) {
                            modalMensaje("Favor de seleccionar al menos un rik en la sección de tiempo de respuesta.");
                        }
                        if (id == 5) {
                            console.log('regresa 5');
                            var titulo = data.Nombre;
                            var tituloStrArr = titulo.split(",")
                            var tituloArr = [];
                            tituloStrArr.forEach(function (data, index, arr) {
                                var nombre = data;
                                tituloArr.push(nombre);
                            });
                            //JFCV gráfica de pie
                            var titulo2 = data.titulo2;
                            var tituloStrArr2 = titulo2.split(",")
                            var tituloArr2 = [];
                            tituloStrArr2.forEach(function (data, index, arr) {
                                var nombre = data;
                                tituloArr2.push(nombre);
                            });
                            var datos = data.Analisis;
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
                            var datos3 = data.Negociación;
                            var datosstrArr3 = datos3.split(",")
                            var datosArr3 = [];
                            datosstrArr3.forEach(function (data, index, arr) {
                                datosArr3.push(+data);
                            });
                            var datos4 = data.Cierre;
                            var datosstrArr4 = datos4.split(",")
                            var datosArr4 = [];
                            datosstrArr4.forEach(function (data, index, arr) {
                                datosArr4.push(+data);
                            });
                            var datos5 = data.Cancelación;
                            var datosstrArr5 = datos5.split(",")
                            var datosArr5 = [];
                            datosstrArr5.forEach(function (data, index, arr) {
                                datosArr5.push(+data);
                            });
                            if (window.myChart8 != undefined) {
                                window.myChart8.destroy();
                            }
                            //JFCV agregar la gráfica de Pie con los datos 
                            var myChart2;
                            var ctxpie = document.getElementById('myTRPie');
                            if (window.myChart2 != undefined) {
                                window.myChart2.destroy();
                            }
                            window.myChart2 = new Chart(ctxpie, {
                                type: 'pie',
                                data: {
                                    labels: tituloArr2,
                                    datasets: [{
                                        data: datosArr2,
                                        backgroundColor: [
                                            COLOR_Analisis,
                                            COLOR_Promocion,
                                            COLOR_Negociacion,
                                            COLOR_Cierre,
                                            COLOR_Cancelado
                                        ],
                                        borderWidth: 1,
                                    }]
                                },
                                options: {
                                    responsive: true,
                                    display: true,
                                    legend: {
                                        position: 'left',
                                        labels: {
                                            padding: 10,
                                            fontSize: 10,
                                        }
                                    },
                                    title: {
                                        display: true,
                                        text: 'Monto de TR',
                                    },
                                    tooltips: {
                                        callbacks: {
                                            label: function (tooltipItem, data) {
                                                var indice = tooltipItem.index;
                                                return data.labels[indice] + ':' + ' $' + data.datasets[0].data[indice].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                            }
                                        }
                                    },
                                    plugins: {
                                        labels: {
                                            render: function (args) {
                                                return args.value;
                                            },
                                            arc: false,
                                            fontColor: '#000',
                                            position: 'outside'
                                        }
                                    },
                                },
                            });
                            var barChartData = {
                                labels: tituloArr,
                                datasets: [{
                                    label: 'Tiempo estándar',
                                    backgroundColor: COLOR_Analisis,
                                    data: datosArr,
                                }, {
                                    label: 'Tiempo límite',
                                    backgroundColor: COLOR_Promocion,
                                    data: datosArr2,
                                }, {
                                    label: 'Fuera de tiempo',
                                    backgroundColor: COLOR_Negociacion,
                                    data: datosArr3,
                                }]
                            };
                            var ctx = document.getElementById('myTRBar');
                            window.myChart8 = new Chart(ctx, {
                                type: 'bar',
                                data: barChartData,
                                options: {
                                    title: {
                                        display: true,
                                        text: 'Monto de proyectos',
                                    },
                                    tooltips: {
                                        mode: 'index',
                                        intersect: false
                                    },
                                    responsive: true,
                                    scales: {
                                        xAxes: [{
                                            stacked: true,
                                        }],
                                        yAxes: [{
                                            stacked: true
                                        }]
                                    },
                                    plugins: {
                                        labels: false,
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
                        if (id == 4) {
                            var titulo = data.Nombre;
                            var tituloStrArr = titulo.split(",")
                            var tituloArr = [];
                            tituloStrArr.forEach(function (data, index, arr) {
                                var nombre = data;
                                tituloArr.push(nombre);
                            });
                            //JFCV gráfica de pie
                            var titulo2 = data.titulo2;
                            var tituloStrArr2 = titulo2.split(",")
                            var tituloArr2 = [];
                            tituloStrArr2.forEach(function (data, index, arr) {
                                var nombre = data;
                                tituloArr2.push(nombre);
                            });
                            var datos = data.Analisis;
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
                            var datos3 = data.Negociación;
                            var datosstrArr3 = datos3.split(",")
                            var datosArr3 = [];
                            datosstrArr3.forEach(function (data, index, arr) {
                                datosArr3.push(+data);
                            });
                            var datos4 = data.presentacion;
                            var datosstrArr4 = datos4.split(",")
                            var datosArr4 = [];
                            datosstrArr4.forEach(function (data, index, arr) {
                                datosArr4.push(+data);
                            });
                            var datos5 = data.Cancelación;
                            var datosstrArr5 = datos5.split(",")
                            var datosArr5 = [];
                            datosstrArr5.forEach(function (data, index, arr) {
                                datosArr5.push(+data);
                            });
                            if (window.myChart8 != undefined) {
                                window.myChart8.destroy();
                            }
                            //JFCV agregar la gráfica de Pie con los datos 
                            var myChart2;
                            var ctxpie = document.getElementById('myTRPie');
                            if (window.myChart2 != undefined) {
                                window.myChart2.destroy();
                            }
                            window.myChart2 = new Chart(ctxpie, {
                                type: 'pie',
                                data: {
                                    labels: tituloArr2,
                                    datasets: [{
                                        data: datosArr2,
                                        backgroundColor: [
                                            COLOR_Analisis,
                                            COLOR_Promocion,
                                            COLOR_Negociacion,
                                            COLOR_Cierre,
                                            COLOR_Cancelado
                                        ],
                                        borderWidth: 1,
                                    }]
                                },
                                options: {
                                    responsive: true,
                                    display: true,
                                    legend: {
                                        position: 'left',
                                        labels: {
                                            padding: 10,
                                            fontSize: 10,
                                        }
                                    },
                                    title: {
                                        display: true,
                                        text: 'Tiempo de Respuesta',
                                    },
                                    tooltips: {
                                        callbacks: {
                                            label: function (tooltipItem, data) {
                                                var indice = tooltipItem.index;
                                                return data.labels[indice] + ':' + data.datasets[0].data[indice].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                            }
                                        }
                                    },
                                    plugins: {
                                        labels: {
                                            render: function (args) {
                                                return args.value;
                                            },
                                            arc: false,
                                            fontColor: '#000',
                                            position: 'outside'
                                        }
                                    },
                                },
                            });
                            var barChartData = {
                                labels: tituloArr,
                                datasets: [{
                                    label: 'Tiempo estándar',
                                    backgroundColor: COLOR_Analisis,
                                    data: datosArr,
                                }, {
                                    label: 'Tiempo límite',
                                    backgroundColor: COLOR_Promocion,
                                    data: datosArr4,
                                }, {
                                    label: 'Fuera de tiempo',
                                    backgroundColor: COLOR_Negociacion,
                                    data: datosArr3,
                                }]
                            };
                            debugger;
                            var ctx = document.getElementById('myTRBar');
                            window.myChart8 = new Chart(ctx, {
                                type: 'bar',
                                data: barChartData,
                                options: {
                                    title: {
                                        display: true,
                                        text: 'Tiempo de respuesta',
                                    },
                                    tooltips: {
                                        mode: 'index',
                                        intersect: false
                                    },
                                    responsive: true,
                                    scales: {
                                        xAxes: [{
                                            stacked: true,
                                        }],
                                        yAxes: [{
                                            stacked: true
                                        }]
                                    },
                                    plugins: {
                                        labels: false,
                                    },
                                    tooltips: {
                                        callbacks: {
                                            label: function (tooltipItem, data) {
                                                return '' + tooltipItem.yLabel.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
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