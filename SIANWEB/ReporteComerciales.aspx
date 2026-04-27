<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/ReporteCom.Master"
    AutoEventWireup="true" CodeBehind="ReporteComerciales.aspx.cs" Inherits="SIANWEB.ReporteComerciales" %>

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
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/excel/FileSaver.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/excel/jszip.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/excel/myexcel.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/chartjs-plugin-labels.js")%>"></script>
    <script type="text/javascript"> 
        function ModalUpload() {
            document.getElementById('<%=iFrameCargar.ClientID%>').src = "CargarArchivos.aspx?tipo=1"
            $('#ModalUpload').modal('hide');
            $("#ModalUpload").appendTo("body");
            $("#ModalUpload").modal({ backdrop: 'static', keyboard: false });
            $('#ModalUpload').modal('show');
        }
        function ModalUploadMultiplicador() {
            document.getElementById('<%=iFrameCargar.ClientID%>').src = "CargarArchivos.aspx?tipo=2"
            $('#ModalUpload').modal('hide');
            $("#ModalUpload").appendTo("body");
            $("#ModalUpload").modal({ backdrop: 'static', keyboard: false });
            $('#ModalUpload').modal('show');
        }
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
            <h2 style="font-weight: bolder">Reportes de resultados </h2>
        </div>
        <ul class="nav nav-tabs" id="tabPage">
            <li class="active"><a href="#tabDatos" data-toggle="tab">
                <h5>Generar Resultados</h5>
            </a>
            </li>
            <li><a href="#tabInfo" data-toggle="tab">
                <h5>Descargar información</h5>
            </a></li>
            <li><a href="#tabPresu" data-toggle="tab"  style="display:none">
                <h5>Definir Presupuesto</h5>
            </a></li>
            <li><a href="#tabMulti" data-toggle="tab" style="display:none">
                <h5>Definir Multiplicador</h5>
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
                                        <div class="col-md-2"></div>
                                        <div class="col-md-4">
                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label20" runat="server" Text="Sucursal" />
                                                    </div>
                                                    <div class="col-md-8">
                                                        <dx:BootstrapComboBox ID="CMBSucursalOT" ClientInstanceName="SucursalOT" runat="server">
                                                        </dx:BootstrapComboBox>
                                                    </div>
                                                </div>
                                            </div>
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
                                            <div class="col-md-12">
                                                <dx:BootstrapRadioButtonList ID="BRLTipoOT" ClientInstanceName="TipoOT" runat="server" RepeatColumns="1">
                                                    <Items>
                                                        <dx:BootstrapListEditItem Value="0" Text="Ventas" Selected="true"></dx:BootstrapListEditItem>
                                                        <dx:BootstrapListEditItem Value="1" Text="Presupuesto"></dx:BootstrapListEditItem>
                                                    </Items>
                                                </dx:BootstrapRadioButtonList>
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
                    
                <%-- Comparacion de Representantes --%>
                <div class="row" style="margin-top: -10px;">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h3 class="panel-title">Comparar Representantes</h3>
                            <span class="pull-right clickable panel-collapsed"><i class="glyphicon glyphicon-chevron-up"></i>
                            </span>
                        </div>
                        <div class="panel-body panel-collapsed" style="display: none;">
                            <asp:UpdatePanel runat="server" ID="updpanel3" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:Wizard ID="WCompararRepresentantes" runat="server" DisplaySideBar="false" CssClass="wizard" ActiveStepIndex="0" OnNextButtonClick="wizClaimInfo_NextButtonClick" OnPreviousButtonClick="WCompararRepresentantes_PreviousButtonClick">
                                                <WizardSteps>
                                                    <asp:WizardStep ID="WizardStep1" runat="server" Title="1">
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
                                                                        <dx:BootstrapComboBox ID="CMBSucursalRepresentante" AutoPostBack="true" OnSelectedIndexChanged="CmbSucursalRepresentante_SelectedIndexChanged" ClientInstanceName="SucursalRepresentante" runat="server">
                                                                        </dx:BootstrapComboBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:WizardStep>
                                                    <asp:WizardStep ID="WizardStep2" runat="server" Title="2">
                                                        <div class="content2">
                                                            <div class="col-md-12" style="margin-top: 5px">
                                                                Selecciones los representante a comparar:
                                                            </div>
                                                            <div class="col-md-12" style="margin-top: 5px">
                                                                <dx:BootstrapListBox CssClasses-Control="form-control2" ID="RBLRepresentante" SelectionMode="CheckColumn" EnableSelectAll="true" ClientInstanceName="ListaRepresentante" runat="server">
                                                                </dx:BootstrapListBox>
                                                            </div>
                                                        </div>
                                                    </asp:WizardStep>
                                                    <asp:WizardStep ID="WizardStep3" runat="server" Title="3">
                                                        <div class="content">
                                                            <div class="col-md-12" style="margin-top: 5px">
                                                                Selecciones Datos a comparar:
                                                            </div>
                                                            <div class="col-md-12" style="margin-top: 5px">
                                                                <dx:BootstrapRadioButtonList ID="RBLTipoRepresentante" ClientInstanceName="TipoRepresentante" runat="server" RepeatColumns="1">
                                                                    <Items>
                                                                        <dx:BootstrapListEditItem Value="0" Text="Ventas" Selected="true"></dx:BootstrapListEditItem>
                                                                        <dx:BootstrapListEditItem Value="1" Text="Utilidad Bruta"></dx:BootstrapListEditItem>
                                                                        <dx:BootstrapListEditItem Value="2" Text="Presupuesto"></dx:BootstrapListEditItem>
                                                                        <dx:BootstrapListEditItem Value="3" Text="Multiplicador"></dx:BootstrapListEditItem>
                                                                    </Items>
                                                                </dx:BootstrapRadioButtonList>
                                                            </div>
                                                        </div>
                                                    </asp:WizardStep>
                                                    <asp:WizardStep ID="WizardStep4" runat="server" Title="4">
                                                        <div class="content">
                                                            <div class="col-md-12" style="margin-top: 5px">
                                                                Seleccione fecha a comparar:
                                                            </div>
                                                            <div class="col-md-12" style="margin-top: 5px">
                                                                <div class="form-group">
                                                                    <div class="col-md-4">
                                                                        <asp:Label ID="Label22" runat="server" Text="Fecha Inicial" />
                                                                    </div>
                                                                    <div class="col-md-8">
                                                                        <dx:ASPxDateEdit runat="server" CssClass="form-control" ClientInstanceName="FechaInicialRepresentante" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="TXTFechaInicialRepresentante" PickerType="Months">
                                                                            <CalendarProperties>
                                                                                <FastNavProperties InitialView="Years" MaxView="Years" />
                                                                            </CalendarProperties>
                                                                        </dx:ASPxDateEdit>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-12" style="margin-top: 5px">
                                                                <div class="form-group">
                                                                    <div class="col-md-4">
                                                                        <asp:Label ID="Label23" runat="server" Text="Fecha Final" />
                                                                    </div>
                                                                    <div class="col-md-8">
                                                                        <dx:ASPxDateEdit runat="server" CssClass="form-control" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="TXTFechaFinalRepresentante" ClientInstanceName="FechaFinalRepresentante" PickerType="Months">
                                                                            <CalendarProperties>
                                                                                <FastNavProperties InitialView="Years" MaxView="Years" />
                                                                            </CalendarProperties>
                                                                        </dx:ASPxDateEdit>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </asp:WizardStep>
                                                </WizardSteps>
                                                <StepNavigationTemplate>

                                                    <asp:Button ID="StepPreviousButton" runat="server" CausesValidation="False" CssClass="btn btn-inverse btn-custom-sm"
                                                        CommandName="MovePrevious" Text="Anterior" />

                                                    <asp:Button ID="StepNextButton" runat="server" CommandName="MoveNext" class="btn btn-primary"
                                                        Text="Siguiente" />

                                                </StepNavigationTemplate>

                                                <FinishNavigationTemplate>
                                                    <asp:Button ID="FinishPreviousButton" CssClass="btn btn-inverse btn-custom-sm" runat="server" CausesValidation="False" CommandName="MovePrevious"
                                                        Text="Anterior" />
                                                    <button id="FinishButton" type="button" class="btn btn-primary"
                                                        runat="server" onclick="BtnCompararRepresentante()">
                                                        <span>Consultar</span>
                                                    </button>
                                                </FinishNavigationTemplate>
                                                <HeaderTemplate>
                                                    <ul id="wizHeader">
                                                        <asp:Repeater ID="SideBarList" runat="server">
                                                            <ItemTemplate>
                                                                <li><a class="<%#  GetClassForWizardStep(Container.DataItem) %>" title="<%#Eval("Name")%>">
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
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <canvas id="myChartCompRep" style="width: 700px; height: 250px"></canvas>
                                        </div>
                                    </div>
                                </ContentTemplate>

                            </asp:UpdatePanel>
                            <asp:HiddenField runat="server" ID="txtrepre" Value="" />
                        </div>
                    </div>
                </div>
                <%-- Buscar Informacion --%>
                <div class="row" style="margin-top: -10px;">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h3 class="panel-title">Buscar Información</h3>
                            <span class="pull-right clickable panel-collapsed"><i class="glyphicon glyphicon-chevron-up"></i>
                            </span>
                        </div>
                        <div class="panel-body panel-collapsed" style="display: none;">
                            <asp:UpdatePanel runat="server" ID="UPdBusacarinfo" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label24" runat="server" Text="Sucursal" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="cmbSucursalBinformacion" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbSucursalBinformacion_SelectedIndexChanged">
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label30" runat="server" Text="Representante" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="cmbBuscarRepresentante" runat="server">
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label25" runat="server" Text="Fecha Inicial" />
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
                                    <asp:AsyncPostBackTrigger ControlID="cmbSucursalBinformacion" />
                                    <asp:AsyncPostBackTrigger ControlID="btnBuscarInformacion" />

                                </Triggers>
                            </asp:UpdatePanel>

                            <div class="col-md-1">
                                <button type="submit" runat="server" class="btn btn-primary" id="btndescragrBuscarInformacion"
                                    onserverclick="btndescragrBuscarInformacion_ServerClick">
                                    <span>Descargar</span>
                                </button>
                            </div>
                            <asp:UpdatePanel runat="server" ID="UpdatePanel6" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="col-md-12" style="margin-top: 5px;">

                                        <dx:BootstrapGridView ID="gridRepresentanteMP" ClientInstanceName="grid" runat="server" KeyFieldName="id_rik"
                                            Width="100%" AutoGenerateColumns="False">
                                            <Columns>
                                                <dx:BootstrapGridViewDataColumn Width="50px" FieldName="Id_cd" Caption="Id" Visible="false">
                                                </dx:BootstrapGridViewDataColumn>
                                                <dx:BootstrapGridViewDataColumn Width="30px" FieldName="Sucursal" Caption="Sucursal">
                                                </dx:BootstrapGridViewDataColumn>
                                                <dx:BootstrapGridViewDataColumn Width="30px" FieldName="Id_Rik" Caption="Num Rik">
                                                </dx:BootstrapGridViewDataColumn>
                                                <dx:BootstrapGridViewDataColumn Width="50px" FieldName="NombreRik" Caption="Nombre">
                                                </dx:BootstrapGridViewDataColumn>
                                                <dx:BootstrapGridViewDataColumn Width="30px" FieldName="Nombre_Mes" Caption="mes" HorizontalAlign="Center">
                                                </dx:BootstrapGridViewDataColumn>
                                                <dx:BootstrapGridViewDataColumn Width="30px" FieldName="Anio" Caption="Año" HorizontalAlign="Center">
                                                </dx:BootstrapGridViewDataColumn> 
                                                   <dx:BootstrapGridViewTextColumn Width="30px" FieldName="totalVenta" Caption="Venta">
                                                    <PropertiesTextEdit DisplayFormatString="c" />
                                                </dx:BootstrapGridViewTextColumn>
                                                <dx:BootstrapGridViewTextColumn Width="30px" FieldName="TotalPresupuesto" Caption="Presupuesto">
                                                    <PropertiesTextEdit DisplayFormatString="c" />
                                                </dx:BootstrapGridViewTextColumn>
                                                <dx:BootstrapGridViewTextColumn Width="30px" FieldName="TotalMultiplicador" Caption="Multiplicador">
                                                    <PropertiesTextEdit DisplayFormatString="p" />
                                                </dx:BootstrapGridViewTextColumn>
                                            </Columns>
                                        </dx:BootstrapGridView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <%-- Presupuesto contra renta representante --%>
                <div class="row" style="margin-top: -10px;">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h3 class="panel-title">Presupuesto contra Venta</h3>
                            <span class="pull-right clickable panel-collapsed"><i class="glyphicon glyphicon-chevron-up"></i>
                            </span>
                        </div>
                        <div class="panel-body panel-collapsed" style="display: none;">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="col-md-4">
                                        <div class="row">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label27" runat="server" Text="Sucursal" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="cmbSucursalPresupuestovsVenta" ClientInstanceName="SucursalPV" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbSucursalPresupuestovsVenta_SelectedIndexChanged">
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 5px;">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label28" runat="server" Text="Representante" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="cmbRepresentanteVSVenta" ClientInstanceName="RepresentantePV" runat="server">
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 5px">
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
                                        <div class="row" style="margin-top: 5px">
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
                                        <div class="row" style="margin-top: 5px">
                                            <div class="col-md-2">
                                                <button id="btnBucaPVV" type="button" class="btn btn-primary" title="Consultar "
                                                    runat="server" onclick="BtnPreVsVta()">
                                                    <span>Consultar</span>
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-8">
                                        <canvas id="myChartPreVsVta" width="800" height="300"></canvas>
                                    </div>
                                </ContentTemplate>

                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            <%--TAB Descargar informacion --%>
            <div class="tab-pane fade" id="tabInfo">
                <div class="row" style="margin-top: 15px">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h3 class="panel-title">Reporte Mensual
                            </h3>
                            <span class="pull-right clickable"><i class="glyphicon glyphicon-chevron-down"></i>
                            </span>
                        </div>
                        <div class="panel-body">
                            <asp:UpdatePanel runat="server" ID="upddescargar">
                                <ContentTemplate>
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label8" runat="server" Text="Sucursal" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="cmbSucrusalDescargar" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbSucrusalDescargar_SelectedIndexChanged">
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label9" runat="server" Text="Representante" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="cmbRepresentanteDescargar" runat="server">
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label10" runat="server" Text="Fecha Inicial" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:ASPxDateEdit runat="server" CssClass="form-control" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="txtFechaInicialDescargar" PickerType="Months">
                                                        <CalendarProperties>
                                                            <FastNavProperties InitialView="Years" MaxView="Years" />
                                                        </CalendarProperties>
                                                    </dx:ASPxDateEdit>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label11" runat="server" Text="Fecha Final" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:ASPxDateEdit runat="server" CssClass="form-control" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="txtFechaFinalDescargar" PickerType="Months">
                                                        <CalendarProperties>
                                                            <FastNavProperties InitialView="Years" MaxView="Years" />
                                                        </CalendarProperties>
                                                    </dx:ASPxDateEdit>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="col-md-12" style="margin-top: 5px;">
                                <button id="Button5" type="button" class="btn btn-primary" title="Consultar "
                                    runat="server" onserverclick="Button5_ServerClick">
                                    <span>Excel</span>
                                </button>

                                <button id="BtnPdfDescaegar" type="button" class="btn btn-primary" title="Consultar "
                                    runat="server" onserverclick="BtnPdfDescaegar_ServerClick">
                                    <span>PDF</span>
                                </button>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-success" style="margin-top: -15px;">
                        <div class="panel-heading">
                            <h3 class="panel-title">Reporte Trimestral
                            </h3>
                            <span class="pull-right clickable"><i class="glyphicon glyphicon-chevron-down"></i>
                            </span>
                        </div>
                        <div class="panel-body">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel5">
                                <ContentTemplate>
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label2" runat="server" Text="Sucursal" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="CMBTrimestreSucursal" runat="server" AutoPostBack="true" OnSelectedIndexChanged="CMBTrimestreSucursal_SelectedIndexChanged">
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label6" runat="server" Text="Representante" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="BCMRepresentanteTrimestral" runat="server">
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label13" runat="server" Text="Trimestre Inicial" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="BcbTrimestreInicial" runat="server">
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
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
                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label33" runat="server" Text="Trimestre final" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="BcbTrimestreFinal" runat="server">
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label34" runat="server" Text="Año" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:ASPxDateEdit runat="server" CssClass="form-control" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="TxtFechaTrimestreFinal" PickerType="Years">
                                                        <CalendarProperties>
                                                            <FastNavProperties InitialView="Years" MaxView="Years" />
                                                        </CalendarProperties>
                                                    </dx:ASPxDateEdit>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="col-md-12" style="margin-top: 5px;">
                                <button id="btnTrimestralDescarga" type="button" class="btn btn-primary" title="Consultar "
                                    runat="server" onserverclick="btnTrimestralDescarga_ServerClick">
                                    <span>Excel</span>
                                </button>

                                <button id="Button4" type="button" class="btn btn-primary" title="Consultar "
                                    runat="server" onserverclick="Button4_ServerClick">
                                    <span>PDF</span>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--TAB Definir Presupuesto --%>
            <div class="tab-pane fade" id="tabPresu">
                <div class="row" style="margin-top: 5px">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h3 class="panel-title">Presupuesto
                            </h3>
                            <span class="pull-right clickable"><i class="glyphicon glyphicon-chevron-down"></i>
                            </span>
                        </div>
                        <div class="panel-body">
                            <asp:UpdatePanel runat="server" ID="UpdPresupuesto">
                                <ContentTemplate>
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label5" runat="server" Text="Sucursal" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="CmbSucursal" runat="server">
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        </div>
                                         <div class="col-md-12" style="margin-top:5px;">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label1" runat="server" Text="Fecha Inicial" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:ASPxDateEdit runat="server" CssClass="form-control" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="TXTFechaInicialPresupuesto" PickerType="Months">
                                                        <CalendarProperties>
                                                            <FastNavProperties InitialView="Years" MaxView="Years" />
                                                        </CalendarProperties>
                                                    </dx:ASPxDateEdit>
                                                </div>
                                            </div>
                                        </div>
                                         <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label7" runat="server" Text="Fecha Final" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:ASPxDateEdit runat="server" CssClass="form-control" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="txtFechaFinalPresupuesto" PickerType="Months">
                                                        <CalendarProperties>
                                                            <FastNavProperties InitialView="Years" MaxView="Years" />
                                                        </CalendarProperties>
                                                    </dx:ASPxDateEdit>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/Img/find16.png" ToolTip="Buscar" OnClick="btnBuscar_Click" />
                                        </div>
                                    </div>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnBuscar" />

                                </Triggers>
                            </asp:UpdatePanel>
                            <div class="row">
                                <div class="col-md-2">
                                    <button type="submit" runat="server" class="btn btn-primary" id="btnExcel"
                                        onserverclick="BtnDescargarEstadisticaPresupuesto_ServerClick">
                                        <span>Descargar Presupuesto  </span>
                                    </button>
                                </div>
                                <div class="col-md-2">
                                    <button id="btnCargarEstadisticaPresupuesto" type="button" class="btn btn-primary"
                                        title="Generar estadistica" onclick="ModalUpload()" runat="server">
                                        <span>Cargar Presupuesto  </span>
                                    </button>
                                </div>
                            </div>
                            <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <dx:BootstrapGridView ID="RgPresupuesto" ClientInstanceName="grid" runat="server" KeyFieldName="id_rik"
                                            Width="100%" AutoGenerateColumns="False">
                                            <Columns>
                                                <dx:BootstrapGridViewDataColumn Width="50px" FieldName="Id_cd" Caption="Id" Visible="false">
                                                </dx:BootstrapGridViewDataColumn>
                                                <dx:BootstrapGridViewDataColumn Width="30px" FieldName="Sucursal" Caption="Sucursal">
                                                </dx:BootstrapGridViewDataColumn>
                                                <dx:BootstrapGridViewDataColumn Width="30px" FieldName="Id_Rik" Caption="Num Rik">
                                                </dx:BootstrapGridViewDataColumn>
                                                <dx:BootstrapGridViewDataColumn Width="50px" FieldName="NombreRik" Caption="Nombre">
                                                </dx:BootstrapGridViewDataColumn>
                                                <dx:BootstrapGridViewDataColumn Width="30px" FieldName="Nombre_Mes" Caption="mes">
                                                </dx:BootstrapGridViewDataColumn>
                                                <dx:BootstrapGridViewDataColumn Width="30px" FieldName="Anio" Caption="Año" HorizontalAlign="Center">
                                                </dx:BootstrapGridViewDataColumn>
                                                <dx:BootstrapGridViewTextColumn Width="30px" FieldName="Presupuesto" Caption="Presupuesto">
                                                    <PropertiesTextEdit DisplayFormatString="c" />
                                                </dx:BootstrapGridViewTextColumn>
                                            </Columns>
                                        </dx:BootstrapGridView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>

            </div>
            <%--TAB Definir Multiplicador --%>
            <div class="tab-pane fade" id="tabMulti" style="display: none;" >

                <div class="col-md-12" style="margin-top: 5px">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h3 class="panel-title">Multiplicador
                            </h3>
                            <span class="pull-right clickable"><i class="glyphicon glyphicon-chevron-down"></i>
                            </span>
                        </div>
                        <div class="panel-body">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                <ContentTemplate>
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label3" runat="server" Text="Sucursal" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="BCMSucursalMultiplicador" runat="server">
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label4" runat="server" Text="Fecha" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:ASPxDateEdit runat="server" CssClass="form-control" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="TxtFechaMultiplicador" PickerType="Months">
                                                        <CalendarProperties>
                                                            <FastNavProperties InitialView="Years" MaxView="Years" />
                                                        </CalendarProperties>
                                                    </dx:ASPxDateEdit>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-1">
                                            <asp:ImageButton ID="btnMultiplicador" runat="server" ImageUrl="~/Img/find16.png" ToolTip="Buscar" OnClick="btnBuscarMultiplicador_Click" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnMultiplicador" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <div class="row" style="margin-top: 5px;">
                                <div class="col-md-2" style="margin-top: 5px;">
                                    <button type="submit" runat="server" class="btn btn-primary" id="Button1"
                                        onserverclick="BTNDescargarMultiplicador_ServerClick">
                                        <span>Descargar Multiplicador</span>
                                    </button>
                                </div>
                                <div class="col-md-2" style="margin-top: 5px;">
                                    <button id="BTNCargarMultiplicador" type="button" class="btn btn-primary"
                                        title="Generar estadistica" onclick="ModalUploadMultiplicador()" runat="server">
                                        <span>Cargar Multiplicador</span>
                                    </button>
                                </div>
                            </div>
                            <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <dx:BootstrapGridView ID="BGVMultiplicador" ClientInstanceName="grid" runat="server" KeyFieldName="id_rik"
                                            Width="100%" AutoGenerateColumns="False">
                                            <Columns>
                                                <dx:BootstrapGridViewDataColumn Width="50px" FieldName="Id_Cd" Caption="Id" Visible="false">
                                                </dx:BootstrapGridViewDataColumn>
                                                <dx:BootstrapGridViewDataColumn Width="30px" FieldName="Sucursal" Caption="Sucursal">
                                                </dx:BootstrapGridViewDataColumn>
                                                <dx:BootstrapGridViewDataColumn Width="30px" FieldName="Id_Rik" Caption="Num Rik">
                                                </dx:BootstrapGridViewDataColumn>
                                                <dx:BootstrapGridViewDataColumn Width="50px" FieldName="NombreRik" Caption="Nombre">
                                                </dx:BootstrapGridViewDataColumn>
                                                <dx:BootstrapGridViewDataColumn Width="30px" FieldName="Nombre_Mes" Caption="mes">
                                                </dx:BootstrapGridViewDataColumn>
                                                <dx:BootstrapGridViewDataColumn Width="30px" FieldName="Anio" Caption="Año">
                                                </dx:BootstrapGridViewDataColumn>
                                                <dx:BootstrapGridViewTextColumn Width="30px" FieldName="TotalMultiplicador" Caption="Multiplicador">
                                                    <PropertiesTextEdit DisplayFormatString="p" />
                                                </dx:BootstrapGridViewTextColumn>
                                            </Columns>
                                        </dx:BootstrapGridView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
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
                    <div class="embed-responsive embed-responsive-16by9 " style="padding-bottom: 35% !Important;">
                        <iframe class="embed-responsive-item" id="iFrameCargar" runat="server" src=""></iframe>
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
        function BtnObservarTotales() {
            $('#<%= UpdateProgress1.ClientID %>').css("display", "block");
            var seleccion = TipoOT.GetValue();
            var Sucursal = SucursalOT.GetValue();
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
                url: "ReporteComerciales.aspx/ObservarTotales",
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
        function BtnCompararRepresentante() {
            $('#<%= UpdateProgress1.ClientID %>').css("display", "block");
            var jsDate = FechaInicialRepresentante.GetDate();
            var year = jsDate.getFullYear(); // where getFullYear returns the year (four digits)  
            var month = jsDate.getMonth() + 1; // where getMonth returns the month (from 0-11)  
            var day = jsDate.getDate();   // where getDate returns the day of the month (from 1-31)  
            if (month < 10) {
                var myDate = `${day}/0${month}/${year}`
            } else {
                var myDate = `${day}/${month}/${year}`
            }
            var jsDate2 = FechaFinalRepresentante.GetDate();
            var year2 = jsDate2.getFullYear(); // where getFullYear returns the year (four digits)  
            var month2 = jsDate2.getMonth() + 1; // where getMonth returns the month (from 0-11)  
            var day2 = jsDate2.getDate();   // where getDate returns the day of the month (from 1-31)  
            if (month2 < 10) {
                var myDateFinal = `${day2}/0${month2}/${year2}`
            } else {
                var myDateFinal = `${day2}/${month2}/${year2}`
            }
            var dataValue = "{mesAnioInicial: '" + myDate + "', mesAniofinal: '" + myDateFinal + "'}";
            $.ajax({
                type: "POST",
                url: "ReporteComerciales.aspx/CompararRepresentantes",
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
                        if (id == 1) {
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            modalMensaje('Favor de seleccionar una sucursal en la seccion de comparar representante.');
                        }
                        if (id == 2) {
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            modalMensaje('Favor de seleccionar Representantes de la seccion de comparar representante.');
                        }
                        if (id == 3) {
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            modalMensaje('Favor de llenar los campos de fecha  de la seccion de comparar representante.');
                        }
                        if (id == 4) {
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            modalMensaje('Error al seleccionar el tipo de filtrado de la seccion de comparar representante.');
                        }
                        if (id == 5) {
                            var ict_unit = [];
                            var efficiency = [];
                            var coloR = [];
                            var dynamicColors = function () {
                                var r = Math.floor(Math.random() * 255);
                                var g = Math.floor(Math.random() * 255);
                                var b = Math.floor(Math.random() * 255);
                                return "rgb(" + r + "," + g + "," + b + ")";
                            };
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
                            var myChart5;
                            var ctx = document.getElementById('myChartCompRep');
                            if (window.myChart5 != undefined) {
                                window.myChart5.destroy();
                            }
                            window.myChart5 = new Chart(ctx, {
                                type: 'bar',
                                data: {
                                    labels: tituloArr,
                                    datasets: [{
                                        data: datosArr,
                                        borderWidth: 1
                                    }]
                                },
                                options: {
                                    responsive: true,
                                    display: false,
                                    labels: {
                                        fontSize: 10,
                                        display: false,
                                    },
                                    legend: {
                                        display: false,
                                    },
                                    title: {
                                        display: true,
                                        text: 'Ventas',
                                    },
                                    scaleshowvalue: true,
                                    scales: {
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
                                        yAxes: [{
                                            ticks: {
                                                beginAtZero: true
                                            }
                                        }]
                                    },
                                    tooltips: {
                                        callbacks: {
                                            label: function (tooltipItem, data) {
                                                var indice = tooltipItem.index;
                                                return data.labels[indice] + ': $' + data.datasets[0].data[indice].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                            }
                                        }
                                    },
                                    plugins: {
                                        colorschemes: {
                                            scheme: 'brewer.Paired3',
                                        },
                                        labels: [
                                            {
                                                render: function (args) {
                                                    return '$' + args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                                },
                                                arc: false,
                                                fontColor: '#000',
                                                position: 'outside',
                                            },
                                        ],
                                    },
                                },
                            });
                        }
                        if (id == 6) {
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
                            var myChart5;
                            var ctx = document.getElementById('myChartCompRep');
                            if (window.myChart5 != undefined) {
                                window.myChart5.destroy();
                            }
                            window.myChart5 = new Chart(ctx, {
                                type: 'bar',
                                data: {
                                    labels: tituloArr,
                                    datasets: [{
                                        data: datosArr,
                                        borderWidth: 1
                                    }]
                                },
                                options: {
                                    responsive: true,
                                    display: false,
                                    labels: {
                                        fontSize: 10,
                                        display: false,
                                    },
                                    legend: {
                                        display: false,
                                    },
                                    title: {
                                        display: true,
                                        text: 'Multiplicador',
                                    },
                                    scaleshowvalue: true,
                                    scales: {
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
                                        yAxes: [{
                                            ticks: {
                                                beginAtZero: true
                                            }
                                        }]
                                    },
                                    tooltips: {
                                        callbacks: {
                                            label: function (tooltipItem, data) {
                                                var indice = tooltipItem.index;
                                                return data.labels[indice] + ':' + data.datasets[0].data[indice].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',') + '%';
                                            }
                                        },
                                    },
                                    plugins: {
                                        colorschemes: {
                                            scheme: 'brewer.Paired3',
                                        },
                                        labels: [
                                            {
                                                render: function (args) {
                                                    return args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',') + '%';
                                                },
                                                arc: false,
                                                fontColor: '#000',
                                                position: 'outside',
                                            },
                                        ],
                                    },
                                },
                            });
                        }
                        if (id == 7) {
                            var ict_unit = [];
                            var efficiency = [];
                            var coloR = [];
                            var dynamicColors = function () {
                                var r = Math.floor(Math.random() * 255);
                                var g = Math.floor(Math.random() * 255);
                                var b = Math.floor(Math.random() * 255);
                                return "rgb(" + r + "," + g + "," + b + ")";
                            };
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
                            var myChart5;
                            var ctx = document.getElementById('myChartCompRep');
                            if (window.myChart5 != undefined) {
                                window.myChart5.destroy();
                            }
                            window.myChart5 = new Chart(ctx, {
                                type: 'bar',
                                data: {
                                    labels: tituloArr,
                                    datasets: [{
                                        data: datosArr,
                                        borderWidth: 1
                                    }]
                                },
                                options: {
                                    responsive: true,
                                    display: false,
                                    labels: {
                                        padding: 10,
                                        fontSize: 10,
                                        display: false,
                                    },
                                    legend: {
                                        display: false,
                                    },
                                    title: {
                                        display: true,
                                        text: 'Utilidad Bruta',
                                    },
                                    scaleshowvalue: true,
                                    scales: {
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
                                        yAxes: [{
                                            ticks: {
                                                beginAtZero: true
                                            }
                                        }]
                                    },
                                    tooltips: {
                                        callbacks: {
                                            label: function (tooltipItem, data) {
                                                var indice = tooltipItem.index;
                                                return data.labels[indice] + ': $' + data.datasets[0].data[indice].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                            }
                                        }
                                    },
                                    plugins: {
                                        colorschemes: {
                                            scheme: 'brewer.Paired3',
                                        },
                                        labels: [
                                            {
                                                render: function (args) {
                                                    return '$' + args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                                },
                                                arc: false,
                                                fontColor: '#000',
                                                position: 'outside',
                                            },
                                        ],
                                    },
                                },
                            });
                        }
                        if (id == 8) {
                            var ict_unit = [];
                            var efficiency = [];
                            var coloR = [];
                            var dynamicColors = function () {
                                var r = Math.floor(Math.random() * 255);
                                var g = Math.floor(Math.random() * 255);
                                var b = Math.floor(Math.random() * 255);
                                return "rgb(" + r + "," + g + "," + b + ")";
                            };
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
                            var myChart5;
                            var ctx = document.getElementById('myChartCompRep');
                            if (window.myChart5 != undefined) {
                                window.myChart5.destroy();
                            }
                            window.myChart5 = new Chart(ctx, {
                                type: 'bar',
                                data: {
                                    labels: tituloArr,
                                    datasets: [{
                                        data: datosArr,
                                        borderWidth: 1
                                    }]
                                },
                                options: {
                                    responsive: true,
                                    display: false,
                                    labels: {
                                        fontSize: 10,
                                        display: false,
                                    },
                                    legend: {
                                        display: false,
                                    },
                                    title: {
                                        display: true,
                                        text: 'Presupuesto',
                                    },
                                    scaleshowvalue: true,
                                    scales: {
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
                                        yAxes: [{
                                            ticks: {
                                                beginAtZero: true
                                            }
                                        }]
                                    },
                                    tooltips: {
                                        callbacks: {
                                            label: function (tooltipItem, data) {
                                                var indice = tooltipItem.index;
                                                return data.labels[indice] + ': $' + data.datasets[0].data[indice].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                            }
                                        }
                                    },
                                    plugins: {
                                        colorschemes: {
                                            scheme: 'brewer.Paired3',
                                        },
                                        labels: [
                                            {
                                                render: function (args) {
                                                    return '$' + args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                                },
                                                arc: false,
                                                fontColor: '#000',
                                                position: 'outside',
                                            }
                                        ],
                                    },
                                },
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
            var rik = RepresentantePV.GetValue();
            var Sucursal = SucursalPV.GetValue();
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
            var dataValue = "{mesAnioInicial: '" + myDate + "', mesAniofinal: '" + myDateFinal + "', sucursal: '" + Sucursal + "', idRik: '" + rik + "'}";
            $.ajax({
                type: "POST",
                url: "ReporteComerciales.aspx/btnBucaPVV_ServerClick",
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
                            var lineChartData = {
                                labels: tituloArr,
                                datasets: [{
                                    label: "Venta",
                                    borderColor: "rgb(205,92,92)",
                                    backgroundColor: "rgb(205,92,92)",
                                    fill: false,
                                    data: datosArr,
                                    yAxisID: "y-axis-1",
                                }, {
                                    label: "Presupuesto",
                                    borderColor: "rgb(0, 191, 255)",
                                    backgroundColor: "rgb(0, 191, 255)",
                                    fill: false,
                                    data: datosArr2,
                                    yAxisID: "y-axis-1"
                                }]
                            };
                            window.myChart4 = new Chart(ctx, {
                                type: 'line',
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
                                        text: 'Presupuesto contra venta',
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
    </script>
</asp:Content>