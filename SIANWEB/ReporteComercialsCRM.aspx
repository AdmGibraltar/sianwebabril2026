<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/ReporteCom.Master"
    AutoEventWireup="true" CodeBehind="ReporteComercialsCRM.aspx.cs" Inherits="SIANWEB.ReporteComercialsCRM" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.Bootstrap" Assembly="DevExpress.Web.Bootstrap.v19.2" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v19.2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <link href="<%=Page.ResolveUrl("~/js/alertify.js-master/src/css/alertify.css")%>"
        rel="stylesheet" />
    <script src="js/jquery-3.3.1.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/jquery-template/jquery.loadTemplate.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.min.js")%>"></script>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css")%>" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/bootstrap-select.min.js") %>"></script>
    <link href="<%=Page.ResolveUrl("~/Librerias/Zebra_Datepicker-master/dist/css/bootstrap/zebra_datepicker.css")%>"
        rel="stylesheet" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/Zebra_Datepicker-master/dist/zebra_datepicker.src.js") %>"></script>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/css/font-awesome.min.css")%>" />

    <script src="<%=Page.ResolveUrl("~/js/icheck.min.js")%>"></script>
    <link href="<%=Page.ResolveUrl("~/Librerias/timepicker/jquery.timepicker.css")%>"
        rel="stylesheet" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/timepicker/jquery.timepicker.min.js") %>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/timepicker/jquery.timepicker.js") %>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/excel/FileSaver.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/excel/jszip.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/excel/myexcel.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/Chart.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/chartjs-plugin-labels.js")%>"></script>
    <script type="text/javascript"> 

        function modalMensaje(mensaje) {
            document.getElementById('<%=lblmensaje2.ClientID%>').innerHTML = mensaje;
            $("#modalMensaje").appendTo("body")
            $("#modalMensaje").modal({ "backdrop": "static" });
            $('#modalMensaje').modal('show');
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
            <h2 style="font-weight: bolder">Reportes de proyectos </h2>
        </div>
        <ul class="nav nav-tabs" id="tabPage">
            <li class="active"><a href="#tabDatos" data-toggle="tab">Graficos de proyectos</a>
            </li>
            <li><a href="#tabdownload" data-toggle="tab">Descargar información</a> </li>
            <li><a href="#tabreportIQ" data-toggle="tab">Reporte Impulsos</a> </li>
        </ul>

        <div class="tab-content">
            <%--TAB Graficos de resultados --%>
            <div class="tab-pane fade in active" id="tabDatos">
                <div class="row" style="margin-top: 15px;">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h3 class="panel-title">Observar Totales</h3>
                            <span class="pull-right clickable panel-collapsed"><i class="glyphicon glyphicon-chevron-up"></i>
                            </span>
                        </div>
                        <div class="panel-body panel-collapsed" style="display: none;">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel43" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="col-md-6">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label1" runat="server" Text="Sucursal" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="CMBSucursal" runat="server" AutoPostBack="true" ClientInstanceName="SucursalOT" OnSelectedIndexChanged="CMBSucursal_SelectedIndexChanged">
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12" style="margin-top: 5px;">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label18" runat="server" Text="Rik" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="CMBRIK" runat="server" ClientInstanceName="RikOt">
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12" style="margin-top: 5px">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label6" runat="server" Text="Fecha Inicial" />
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
                                        <div class="col-md-12" style="margin-top: 5px">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label7" runat="server" Text="Fecha Final" />
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
                                        <div class="col-md-12" style="margin-top: 5px;">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label19" runat="server" Text="Instalada/Esporadica" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="CMBTipo" runat="server" ClientInstanceName="tipoOt">
                                                        <Items>
                                                            <dx:BootstrapListEditItem Value="0" Text="--Todos--" Selected="true">
                                                            </dx:BootstrapListEditItem>
                                                            <dx:BootstrapListEditItem Value="1" Text="Instalada">
                                                            </dx:BootstrapListEditItem>
                                                            <dx:BootstrapListEditItem Value="2" Text="Esporadica">
                                                            </dx:BootstrapListEditItem>
                                                        </Items>
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12" style="margin-top: 5px;">
                                            <div class="col-md-2">
                                                <button id="BTNObservarTotales" type="button" class="btn btn-primary" title="Generar estadistica"
                                                    runat="server" onclick="BtnObservarTotales()">
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
                                                <canvas id="myChartbar" width="700" height="250"></canvas>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="CMBSucursal" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <div class="row" style="margin-top: -15px;">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h3 class="panel-title">Comparar representantes</h3>
                            <span class="pull-right clickable panel-collapsed"><i class="glyphicon glyphicon-chevron-up"></i>
                            </span>
                        </div>
                        <div class="panel-body panel-collapsed" style="display: none;">
                            <asp:UpdatePanel runat="server" ID="updpanel3" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="col-md-6">
                                        <asp:Wizard ID="WRikvsRik" runat="server" DisplaySideBar="false" CssClass="wizard" ActiveStepIndex="0" OnNextButtonClick="wizClaimInfo_NextButtonClick" OnPreviousButtonClick="WRikvsRik_PreviousButtonClick">
                                            <WizardSteps>
                                                <asp:WizardStep ID="WizardStep1" runat="server" Title="1">
                                                    <div class="content">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <center>
                                                                    Seleccione la Sucursal:
                                                                </center>
                                                            </div>
                                                            <div class="col-md-12">

                                                                <div class="form-group">
                                                                    <div class="col-md-4">
                                                                        <asp:Label ID="Label21" runat="server" Text="Sucursal" />
                                                                    </div>
                                                                    <div class="col-md-8">
                                                                        <dx:BootstrapComboBox ID="CMBSucursalRIk" AutoPostBack="true" OnSelectedIndexChanged="CMBSucursalRIk_SelectedIndexChanged" runat="server">
                                                                        </dx:BootstrapComboBox>
                                                                    </div>
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

                                                            <div class="form-group">
                                                                <div class="col-md-4">
                                                                    <asp:Label ID="Label8" runat="server" Text="Fecha Inicial" />
                                                                </div>
                                                                <div class="col-md-8">
                                                                    <dx:ASPxDateEdit runat="server" ClientInstanceName="AnioInicialrvr" ID="TxtAnioInicialrvr" CssClass="form-control" ButtonStyle-CssClass=" btn  dropdown-toggle-date" PickerType="Months">
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
                                                                    <asp:Label ID="Label9" runat="server" Text="Fecha Final" />
                                                                </div>
                                                                <div class="col-md-8">
                                                                    <dx:ASPxDateEdit runat="server" ClientInstanceName="AnioFinalrvr" ID="TxtAnioFinalrvr" CssClass="form-control" ButtonStyle-CssClass=" btn  dropdown-toggle-date" PickerType="Months">
                                                                        <CalendarProperties>
                                                                            <FastNavProperties InitialView="Years" MaxView="Years" />
                                                                        </CalendarProperties>
                                                                    </dx:ASPxDateEdit>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:WizardStep>
                                                <asp:WizardStep ID="WizardStep4" runat="server" Title="4">
                                                    <div class="content">
                                                        <div class="col-md-12" style="margin-top: 5px">
                                                            Selecciones Datos a comparar:
                                                        </div>
                                                        <div class="col-md-12" style="margin-top: 5px">
                                                            <dx:BootstrapRadioButtonList ID="RBLTipoRepresentante" ClientInstanceName="tiporikvsrik" runat="server" RepeatColumns="1">
                                                                <Items>
                                                                    <dx:BootstrapListEditItem Value="0" Text="Cantidad de proyectos" Selected="true"></dx:BootstrapListEditItem>
                                                                    <dx:BootstrapListEditItem Value="1" Text="Monto de Proyectos"></dx:BootstrapListEditItem>
                                                                </Items>
                                                            </dx:BootstrapRadioButtonList>
                                                        </div>
                                                    </div>
                                                </asp:WizardStep>
                                            </WizardSteps>
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
                                            <FinishNavigationTemplate>
                                                <asp:Button ID="FinishPreviousButton" CssClass="btn btn-inverse btn-custom-sm" runat="server" CausesValidation="False" CommandName="MovePrevious"
                                                    Text="Anterior" />
                                                <button id="FinishButton2" type="button" class="btn btn-primary"
                                                    runat="server" onclick="ConsultarrikVsrikCRm()">
                                                    <span>Consultar</span>
                                                </button>
                                            </FinishNavigationTemplate>
                                            <StartNextButtonStyle CssClass="btn btn-primary btn-custom-sm" />
                                            <StepPreviousButtonStyle CssClass="btn btn-inverse btn-custom-sm" />
                                            <StepNextButtonStyle CssClass="btn btn-primary btn-custom-sm" />
                                            <FinishPreviousButtonStyle CssClass="btn btn-inverse btn-custom-sm" />
                                            <FinishCompleteButtonStyle CssClass="btn btn-success btn-custom-sm" />
                                        </asp:Wizard>
                                    </div>
                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <canvas id="myRvR" width="700" height="550"></canvas>
                                    </div>
                                    <asp:HiddenField runat="server" ID="fecharikvsrikinicial" Value="" />
                                    <asp:HiddenField runat="server" ID="fecharikvsrikfinal" Value="" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="WRikvsRik" />

                                </Triggers>
                            </asp:UpdatePanel>
                           
                        </div>
                    </div>
                </div> 
                <div class="row" style="margin-top: -15px; display:none">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h3 class="panel-title">Comparar sucursales</h3>
                            <span class="pull-right clickable panel-collapsed"><i class="glyphicon glyphicon-chevron-up"></i>
                            </span>
                        </div>
                        <div class="panel-body panel-collapsed" style="display: none;">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="col-md-4">
                                        <div class="col-md-12">
                                            Seleccione la Sucursal a comparar: 
                                        </div>
                                        <div class="col-md-6">
                                            <div class="boxes2">
                                                <dx:BootstrapCheckBoxList ID="CmbSucursalSvS" ClientInstanceName="sucursalevsv" runat="server" RepeatColumns="1">
                                                </dx:BootstrapCheckBoxList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-8">
                                        <div class="col-md-6" style="margin-top: 5px">
                                            <div class="form-group">
                                                <asp:Label ID="Label10" runat="server" Text="Fecha Inicial" />
                                           
                                            <div class="col-md-8">
                                                <dx:ASPxDateEdit runat="server" ClientInstanceName="AnioInicialrsvs" CssClass="form-control" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="txtAnioinicialsvs" PickerType="Months">
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
                                                <asp:Label ID="Label11" runat="server" Text="Fecha Final" />
                                            </div>
                                            <div class="col-md-8">
                                                <dx:ASPxDateEdit runat="server" ClientInstanceName="AnioFinalsvs" CssClass="form-control" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="txtanioFinalSvs" PickerType="Months">
                                                    <CalendarProperties>
                                                        <FastNavProperties InitialView="Years" MaxView="Years" />
                                                    </CalendarProperties>
                                                </dx:ASPxDateEdit>
                                            </div>
                                        </div>
                                    </div>
                                      </div>

                                    <div class="col-md-12" style="margin-top: 5px">
                                        Selecciones Datos a comparar:
                                    </div>
                                    <div class="col-md-12" style="margin-top: 5px">
                                        <dx:BootstrapRadioButtonList ID="tiposvs" ClientInstanceName="tiposvs" runat="server" RepeatColumns="1">
                                            <Items>
                                                <dx:BootstrapListEditItem Value="0" Text="Cantidad de proyectos" Selected="true"></dx:BootstrapListEditItem>
                                                <dx:BootstrapListEditItem Value="1" Text="Monto de Proyectos"></dx:BootstrapListEditItem>
                                            </Items>
                                        </dx:BootstrapRadioButtonList>
                                    </div> 
                                    <div class="col-md-12" style="margin-top: 5px">
                                        <button id="FinishButton" type="button" class="btn btn-primary"
                                            runat="server" onclick="graficaSucursalVsSucursalCRM()">
                                            <span>Consultar</span>
                                        </button>
                                    </div> 
                                    <div class="col-md-12" style="margin-top: 5px">
                                        <canvas id="mysvs" width="1000" height="450"></canvas>
                                    </div>
                                </ContentTemplate> 
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            <%--TAB Descargar informacion--%>
            <div class="tab-pane fade" id="tabdownload">
                <div class="col-md-12" style="margin-top: 15px;">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h3 class="panel-title">Generación de reportes</h3>
                            <span class="pull-right clickable"><i class="glyphicon glyphicon-chevron-up"></i>
                            </span>
                        </div>
                        <div class="panel-body">
                            <div class="col-md-6">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                    <ContentTemplate>
                                        <asp:Wizard ID="Wizard2" runat="server" DisplaySideBar="false" CssClass="wizard" ActiveStepIndex="0" OnNextButtonClick="Wizard2_NextButtonClick" OnPreviousButtonClick="Wizard2_PreviousButtonClick">

                                            <WizardSteps>
                                                <asp:WizardStep ID="WizardStep6" runat="server" Title="1">
                                                    <div class="content">

                                                        <div class="col-md-6" style="margin-top: 5px">
                                                            Selecciones Tipos de reporte a generar:
                                                        </div>
                                                        <div class="col-md-12" style="margin-top: 5px">

                                                            <dx:BootstrapRadioButtonList ID="bcmTipoReporte" ClientInstanceName="tipoSucursalvsSucursal" OnSelectedIndexChanged="bcmTipoReporte_SelectedIndexChanged" AutoPostBack="true" runat="server" RepeatColumns="1">
                                                                <Items>
                                                                    <dx:BootstrapListEditItem Value="0" Text="Reporte mes actual" Selected="true" Badge-Text="Reporte que abarca información desde el 1ero del mes actual hasta la fecha"></dx:BootstrapListEditItem>
                                                                    <dx:BootstrapListEditItem Value="1" Text="Reporte 3 meses anteriores" Badge-Text="Reporte que abarca información desde tres meses anteriores al actual"></dx:BootstrapListEditItem>
                                                                    <dx:BootstrapListEditItem Value="2" Text="Reporte global" Badge-Text="Reporte que abarca todos los meses definidos"></dx:BootstrapListEditItem>
                                                                </Items>
                                                            </dx:BootstrapRadioButtonList>
                                                        </div>
                                                        <div class="col-md-12" style="margin-top: 5px">
                                                            <div class="form-group">
                                                                <div class="col-md-4">
                                                                    <asp:Label ID="txtfechaInicialDI" runat="server" Text="Fecha Inicial" />
                                                                </div>
                                                                <div class="col-md-8">
                                                                    <dx:ASPxDateEdit runat="server" CssClass="form-control" ClientInstanceName="FechaInicial" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="txtfechaIniciaGR" PickerType="Months">
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
                                                                    <asp:Label ID="Label2" runat="server" Text="Fecha Final" />
                                                                </div>
                                                                <div class="col-md-8">
                                                                    <dx:ASPxDateEdit runat="server" CssClass="form-control" ClientInstanceName="Fechafinal" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="txtfechafinalGR" PickerType="Months">
                                                                        <CalendarProperties>
                                                                            <FastNavProperties InitialView="Years" MaxView="Years" />
                                                                        </CalendarProperties>
                                                                    </dx:ASPxDateEdit>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </asp:WizardStep>
                                                <asp:WizardStep ID="WizardStep7" runat="server" Title="2">
                                                    <div class="content">

                                                        <div class="col-md-12" style="margin-top: 5px">
                                                            Filtrar por:
                                                        </div>
                                                        <div class="col-md-12" style="margin-top: 5px">
                                                            <div class="form-group">
                                                                <div class="col-md-4">
                                                                    <asp:Label ID="Label3" runat="server" Text="Sucursal" />
                                                                </div>
                                                                <div class="col-md-8">
                                                                    <dx:BootstrapComboBox ID="cmbGenerarReporte" runat="server" AutoPostBack="true" ClientInstanceName="SucursalOT" OnSelectedIndexChanged="cmbGenerarReporte_SelectedIndexChanged">
                                                                    </dx:BootstrapComboBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-12" style="margin-top: 5px;">
                                                            <div class="form-group">
                                                                <div class="col-md-4">
                                                                    <asp:Label ID="Label4" runat="server" Text="Rik" />
                                                                </div>
                                                                <div class="col-md-8">
                                                                    <dx:BootstrapComboBox ID="cmbRikDI" runat="server" ClientInstanceName="RikOt">
                                                                    </dx:BootstrapComboBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-12" style="margin-top: 5px;">
                                                            <div class="form-group">
                                                                <div class="col-md-4">
                                                                    <asp:Label ID="Label5" runat="server" Text="Instalada/Esporadica" />
                                                                </div>
                                                                <div class="col-md-8">
                                                                    <dx:BootstrapComboBox ID="cmbTipoVenta" runat="server" ClientInstanceName="tipoOt">
                                                                        <Items>
                                                                            <dx:BootstrapListEditItem Value="0" Text="--Todos--" Selected="true">
                                                                            </dx:BootstrapListEditItem>
                                                                            <dx:BootstrapListEditItem Value="1" Text="Instalada">
                                                                            </dx:BootstrapListEditItem>
                                                                            <dx:BootstrapListEditItem Value="2" Text="Esporadica">
                                                                            </dx:BootstrapListEditItem>
                                                                        </Items>
                                                                    </dx:BootstrapComboBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </asp:WizardStep>
                                                <asp:WizardStep ID="WizardStep8" runat="server" Title="3">
                                                    <div class="content">

                                                        <div class="col-md-12" style="margin-top: 5px">
                                                            Selecciones los pasos del nivel profesional de venta que desea añadir al reporte:
                                                        </div>
                                                        <div class="col-md-12" style="margin-top: 5px">
                                                            <dx:BootstrapCheckBox runat="server" ID="ChAnalisis" Text="Analisis">
                                                            </dx:BootstrapCheckBox>
                                                        </div>
                                                        <div class="col-md-12" style="margin-top: 5px">
                                                            <dx:BootstrapCheckBox runat="server" ID="chPresentacion" Text="Presentación">
                                                            </dx:BootstrapCheckBox>
                                                        </div>
                                                        <div class="col-md-12" style="margin-top: 5px">
                                                            <dx:BootstrapCheckBox runat="server" ID="chnegociacion" Text="Negociación">
                                                            </dx:BootstrapCheckBox>
                                                        </div>
                                                        <div class="col-md-12" style="margin-top: 5px">
                                                            <dx:BootstrapCheckBox runat="server" ID="chCierre" Text="Cierre">
                                                            </dx:BootstrapCheckBox>
                                                        </div>
                                                        <div class="col-md-12" style="margin-top: 5px">
                                                            <dx:BootstrapCheckBox runat="server" ID="chCancelado" Text="Cancelado">
                                                            </dx:BootstrapCheckBox>
                                                        </div>

                                                    </div>
                                                </asp:WizardStep>
                                            </WizardSteps>
                                            <HeaderTemplate>
                                                <ul id="wizHeader">
                                                    <asp:Repeater ID="SideBarList" runat="server">
                                                        <ItemTemplate>
                                                            <li><a class="<%# GetClassForWizardStep2(Container.DataItem) %>" title="<%#Eval("Name")%>">
                                                                <%# Eval("Name")%></a> </li>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ul>
                                            </HeaderTemplate>
                                            <FinishNavigationTemplate>
                                                <asp:Button ID="FinishPreviousButton" CssClass="btn btn-inverse btn-custom-sm" runat="server" CausesValidation="False" CommandName="MovePrevious"
                                                    Text="Anterior" />
                                                <button id="FinishButton" type="button" class="btn btn-primary" onclick="FinishButtonClick()"
                                                    runat="server">
                                                    <span>Consultar</span>
                                                </button>
                                            </FinishNavigationTemplate>
                                            <StartNextButtonStyle CssClass="btn btn-primary btn-custom-sm" />
                                            <StepPreviousButtonStyle CssClass="btn btn-inverse btn-custom-sm" />
                                            <StepNextButtonStyle CssClass="btn btn-primary btn-custom-sm" />
                                            <FinishPreviousButtonStyle CssClass="btn btn-inverse btn-custom-sm" />
                                            <FinishCompleteButtonStyle CssClass="btn btn-success btn-custom-sm" />
                                        </asp:Wizard>
                                        <div style="display: none">
                                            <button id="btnclick" type="button" class="btn btn-primary" title="Consultar "
                                                runat="server" onserverclick="FinishButtonClick">
                                                <span>PDF</span>
                                            </button>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="Wizard2" />
                                        <asp:PostBackTrigger ControlID="btnclick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--TAB Reporte de Impulsos Quimicos--%>
            <div class="tab-pane fade" id="tabreportIQ">
                <div class="col-md-12" style="margin-top: 15px;">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h3 class="panel-title">Reporte Proyectos de Impulsos Quimicos</h3>
                            <span class="pull-right clickable"><i class="glyphicon glyphicon-chevron-up"></i>
                            </span>
                        </div>
                        <div class="panel-body">
                            <div class="col-md-6">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Wizard ID="WizardReIQ" runat="server" DisplaySideBar="false" CssClass="wizard" ActiveStepIndex="0" OnNextButtonClick="WizardReIQ_NextButtonClick" OnPreviousButtonClick="WizardReIQ_PreviousButtonClick">
                                        
                                            <WizardSteps>
                                                <asp:WizardStep ID="WizardStep5" runat="server" Title="1">
                                                    <div class="content">

                                                        <div class="col-md-6" style="margin-top: 5px">
                                                            Periodo:
                                                        </div>
                                                        <div class="col-md-12" style="margin-top: 5px">
                                                            <div class="form-group">
                                                                <div class="col-md-4">
                                                                    <asp:Label ID="Label15" runat="server" Text="Fecha Inicial" />
                                                                </div>
                                                                <div class="col-md-8">
                                                                    <dx:ASPxDateEdit runat="server" CssClass="form-control" ClientInstanceName="FechaInicial_RptIQ" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="drpFecInicialRptIQ" PickerType="Months">
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
                                                                    <asp:Label ID="Label16" runat="server" Text="Fecha Final" />
                                                                </div>
                                                                <div class="col-md-8">
                                                                    <dx:ASPxDateEdit runat="server" CssClass="form-control" ClientInstanceName="Fechafinal_RptIQ" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="drpFecFinalRptIQ" PickerType="Months">
                                                                        <CalendarProperties>
                                                                            <FastNavProperties InitialView="Years" MaxView="Years" />
                                                                        </CalendarProperties>
                                                                    </dx:ASPxDateEdit>
                                                                </div>
                                                            </div>
                                                            
                                                        </div>
                                                    </div>
                                                </asp:WizardStep>
                                                <asp:WizardStep ID="WizardStep9" runat="server" Title="2">
                                                    <div class="content">

                                                        <div class="col-md-12" style="margin-top: 5px">
                                                            Filtrar por:
                                                        </div>
                                                        <div class="col-md-12" style="margin-top: 5px">
                                                            <div class="form-group">
                                                                <div class="col-md-4">
                                                                    <asp:Label ID="Label17" runat="server" Text="Sucursal" />
                                                                </div>
                                                                <div class="col-md-8">                                                                    
                                                                    <dx:BootstrapComboBox ID="CMBSucursalIQ" runat="server" AutoPostBack="true" ClientInstanceName="SucursalOT" 
                                                                        OnSelectedIndexChanged="CMBSucursalIQ_SelectedIndexChanged" Visible="true" enabled="false">
                                                                    </dx:BootstrapComboBox>
                                                                    <dx:BootstrapCheckBox runat="server" ID="bschkTodasSucusalesDelGrupo" Text="Todos los del Grupo" Enabled="true" 
                                                                        OnValueChanged="bschkTodasSucusalesDelGrupo_ValueChanged" Visible="false"> 
                                                                    </dx:BootstrapCheckBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-12" style="margin-top: 5px;">
                                                            <div class="form-group">
                                                                <div class="col-md-4">
                                                                    <asp:Label ID="Label20" runat="server" Text="Rik" />
                                                                </div>
                                                                <div class="col-md-8">
                                                                    <dx:BootstrapComboBox ID="drpGridRiks2" runat="server" ClientInstanceName="RikOT" Visible="true">
                                                                    </dx:BootstrapComboBox>
                                                                 </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-12" style="margin-top: 5px;margin-bottom:5px">
                                                             <div class="form-group">
                                                                <div class="col-md-4" >
                                                                    <asp:Label ID="Label25" runat="server" Text="Mostrar a nivel" />
                                                                </div>
                                                                <div class="col-md-4" style="margin-top: 5px; text-align:left; text-wrap:none;">
                                                                    <dx:BootstrapRadioButtonList runat="server" ID="bsRdlstAgrupadorQuimicos" RepeatColumns="1" Width="300px" >
                                                                        <Items>
                                                                            <dx:BootstrapListEditItem Text="Tipo de Producto" Value="1" Selected="true" />
                                                                            <dx:BootstrapListEditItem Text="UEN" Value="2" />
                                                                        </Items>
                                                                    </dx:BootstrapRadioButtonList>
                                                                    
                                                                </div>
                                                             </div>
                                                        </div>
                                                    </div>
                                                </asp:WizardStep>
                                                <asp:WizardStep ID="WizardStep10" runat="server" Title="3">
                                                    <div class="content">

                                                        <div class="col-md-12" style="margin-top: 5px">
                                                            Selecciones el tipo de producto que desea analizar en el reporte:
                                                        </div>
                                                        <div class="col-md-12" style="margin-top: 5px">
                                                            <dx:BootstrapCheckBox runat="server" ID="bsChkTipoProdTodos" Text="Todos" Checked="true" OnCheckedChanged="bsChkTipoProdTodos_CheckedChanged">
                                                            </dx:BootstrapCheckBox>
                                                        </div>
                                                        <div class="col-md-12" style="margin-top: 5px">
                                                            <dx:BootstrapCheckBox runat="server" ID="bsChkTipoProdDosifica" Text="DOSIF/DESP" OnCheckedChanged="bsChkTipoProdDosifica_CheckedChanged">
                                                            </dx:BootstrapCheckBox>
                                                        </div>
                                                        <div class="col-md-12" style="margin-top: 5px">
                                                            <dx:BootstrapCheckBox runat="server" ID="bsChkTipoProdOtros" Text="OTROS" OnCheckedChanged="bsChkTipoProdOtros_CheckedChanged">
                                                            </dx:BootstrapCheckBox>
                                                        </div>
                                                        <div class="col-md-12" style="margin-top: 5px">
                                                            <dx:BootstrapCheckBox runat="server" ID="bsChkTipoProdPapel" Text="PAPEL" OnCheckedChanged="bsChkTipoProdPapel_CheckedChanged">
                                                            </dx:BootstrapCheckBox>
                                                        </div>
                                                        <div class="col-md-12" style="margin-top: 5px">
                                                            <dx:BootstrapCheckBox runat="server" ID="bsChkQuimicos" Text="QUIMICOS" OnCheckedChanged="bsChkQuimicos_CheckedChanged">
                                                            </dx:BootstrapCheckBox>
                                                        </div>
                                                        <div class="col-md-12" style="margin-top: 5px">
                                                            <dx:BootstrapCheckBox runat="server" ID="bsChkSuplemento" Text="SUPLEMENTOS" OnCheckedChanged="bsChkSuplemento_CheckedChanged">
                                                            </dx:BootstrapCheckBox>
                                                        </div>

                                                    </div>
                                                </asp:WizardStep>
                                                <asp:WizardStep ID="WizardStep11" runat="server" Title="4">
                                                    <div class="content">

                                                        <div class="col-md-12" style="margin-top: 5px">
                                                            Selecciona la aplicacion que desea analizar en el reporte:
                                                        </div>
                                                        <div class="col-md-12" style="margin-top: 5px">
                                                            <asp:TextBox runat="server" ID="txtSearchApp"  OnTextChanged="txtSearchApp_TextChanged" AutoPostBack="true" ToolTip="Buscar Aplicacion por Nombre" Width="220px"></asp:TextBox>
                                                             
                                                            
                                                            <br />
                                                            <asp:CheckBox runat="server" ID="chkTodos" Text="Todos" AutoPostBack="true" OnCheckedChanged="chkTodos_CheckedChanged" Checked="true" />
                                                        </div>
                                                        

                                                        <div class="col-md-12" style="margin-top: 5px">
                                                            <div class="form-group">
                                                                <div class="col-md-8" style="width: 775px; height: 250px; overflow-y: scroll; ">
                                                                    <asp:CheckBoxList runat="server" ID="chklstAplicaciones" AutoPostBack="False"  RepeatColumns="2"
                                                                        CellSpacing="2" CellPadding="2" Width="750px" />
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </asp:WizardStep>
                                            </WizardSteps>
                                            <FinishNavigationTemplate>
                                                <asp:Button ID="Button1" CssClass="btn btn-inverse btn-custom-sm" runat="server" CausesValidation="False" CommandName="MovePrevious"
                                                    Text="Anterior" />
                                                <button id="btnExcelQuimicos" type="button" class="btn btn-primary" onclick="FinishButtonQuimicosClick()"
                                                    runat="server">
                                                    <span>Generar</span>
                                                </button>
                                            </FinishNavigationTemplate>
                                            <StartNextButtonStyle CssClass="btn btn-primary btn-custom-sm" />
                                            <StepPreviousButtonStyle CssClass="btn btn-inverse btn-custom-sm" />
                                            <StepNextButtonStyle CssClass="btn btn-primary btn-custom-sm" />
                                            <FinishPreviousButtonStyle CssClass="btn btn-inverse btn-custom-sm" />
                                            <FinishCompleteButtonStyle CssClass="btn btn-success btn-custom-sm" />

                                        </asp:Wizard>
                                        <div style="display: none">
                                            <button id="btnGeneraExcel" type="button" class="btn btn-primary" title="Generar"
                                                runat="server" onserverclick="FinishButtonClick"></button>
                                            <asp:TextBox ID="txtCualEs" runat="server" Text="0" />
                                            <!-- btnGeneraExcel_ServerClick -->
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="WizardReIQ" />
                                        <asp:PostBackTrigger ControlID="btnclick" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
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

        function FinishButtonClick() {
            console.log("prueba");
            var clickButton = document.getElementById("<%= btnclick.ClientID %>");
            console.log("clickButton");
            clickButton.click();
        }

        function FinishButtonQuimicosClick() {
            var T2 = document.getElementById('<%=txtCualEs.ClientID%>');
              T2.value = "1";
              var clickButtonQUI = document.getElementById("<%= btnclick.ClientID %>");
              /// var clickButtonQUI = document.getElementById("<%= btnGeneraExcel.ClientID %>");
              clickButtonQUI.click();
          }

        function BtnObservarTotales() {
            $('#<%= UpdateProgress1.ClientID %>').css("display", "block");
                var seleccion = tipoOt.GetValue();
                var Sucursal = SucursalOT.GetValue();
            var rik = RikOt.GetValue();


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

            var dataValue = "{Seleccion: '" + seleccion + "', sucursal: '" + Sucursal + "', rik: '" + rik + "', mesAnioInicial: '" + myDate + "', mesAniofinal: '" + myDateFinal + "'}";

                $.ajax({
                    type: "POST",
                    url: "ReporteComercialsCRM.aspx/ObservarTotalesCRM",
                    data: dataValue,
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    error: function (XMLHttpRequest, textStatus, errorThrown) {

                        modalMensaje(errorThrown);
                    },
                    success: function (response) {

                        if (response != null && response.d != null) {
                            var data = response.d;

                            data = $.parseJSON(data);

                            var id = data.id;

                            if (id == -1) {

                                modalMensaje(data.men);
                            }

                            if (id == 1) {

                                modalMensaje("Favor de seleccionar la sucursal de la sección de observar totales.");
                            }

                            if (id == 2) {

                                modalMensaje("Favor de seleccionar la fecha inicial y/o final de la sección de observar totales.");
                            }

                            if (id == 4) {

                                modalMensaje("La fecha inicial es mayor a la fecha final de la sección de observar totales.");
                            }
                            if (id == 3) {


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

                                var titulo2 = data.titulo2;
                                var tituloStrArr2 = titulo2.split(",")
                                var tituloArr2 = [];

                                tituloStrArr2.forEach(function (data, index, arr) {
                                    var nombre = data;
                                    tituloArr2.push(nombre);
                                });


                                var datos2 = data.datos2;
                                var datosstrArr2 = datos2.split(",")
                                var datosArr2 = [];

                                datosstrArr2.forEach(function (data, index, arr) {
                                    datosArr2.push(+data);

                                });


                                var myChart2;
                                var myChart3;
                                var ctx = document.getElementById('myChartPie');
                                var ctx2 = document.getElementById('myChartbar');


                                if (window.myChart2 != undefined) {

                                    window.myChart2.destroy();
                                }

                                if (window.myChart3 != undefined) {

                                    window.myChart3.destroy();
                                }
                                window.myChart2 = new Chart(ctx, {
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

                                window.myChart3 = new Chart(ctx2, {
                                    type: 'bar',
                                    data: {
                                        labels: tituloArr,
                                        datasets: [{

                                            data: datosArr,
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
                                                padding: 15

                                            }
                                        },
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
                                            text: 'Monto de proyectos',
                                        },
                                        scaleshowvalue: true,
                                        scales: {
                                            xAxes: [{
                                                stacked: false,
                                                beginAtZero: true,

                                                ticks: {
                                                    stepSize: 1,
                                                    min: 0,
                                                    autoSkip: false
                                                },

                                            }],
                                            yAxes: [{
                                                display: true,
                                                scaleLabel: {
                                                    display: true,
                                                    labelString: 'Monto'
                                                }
                                            }]
                                        },
                                        plugins: {

                                            labels: {
                                                render: function (args) {
                                                    return '$' + args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
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

        function ConsultarrikVsrikCRm() {
           
            var seleccion = tiporikvsrik.GetValue();
            var fecha = document.getElementById('<%=fecharikvsrikinicial.ClientID%>').value;
            var fecha2 = document.getElementById('<%=fecharikvsrikfinal.ClientID%>').value;
            var dateParts = fecha.split("/");
            var dateParts2 = fecha2.split("/");
            var currentdate = new Date(+dateParts[2], dateParts[1] - 1, +dateParts[0]);  
            var currentdate2 = new Date(+dateParts2[2], dateParts2[1] - 1, +dateParts2[0]);
         

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

           

            var dataValue = "{Seleccion: '" + seleccion + "', mesAnioInicial: '" + myDate + "', mesAniofinal: '" + myDateFinal + "'}";
            $('#<%= UpdateProgress1.ClientID %>').css("display", "block");
            console.log(dataValue);
                $.ajax({
                    type: "POST",
                    url: "ReporteComercialsCRM.aspx/ConultarRikvsRikCRM",
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

                                modalMensaje(data.men);
                            }

                            if (id == 1) {

                                modalMensaje("Favor de seleccionar la sucursal de la sección de Rik vs Rik.");
                            }
                            if (id == 2) {

                                modalMensaje("Favor de seleccionar la fecha inicial y/o final de la sección de Rik vs Rik.");
                            }

                            if (id == 3) {

                                modalMensaje("La fecha inicial es mayor a la fecha final de la sección de Rik vs Rik.");
                            }
                            if (id == 5) {


                                var titulo = data.Nombre;
                                var tituloStrArr = titulo.split(",");
                                var tituloArr = [];

                                tituloStrArr.forEach(function (data, index, arr) {
                                    var nombre = data;
                                    tituloArr.push(nombre);
                                });


                                var datos = data.Analisis;
                                var datosstrArr = datos.split(",")
                                var datosArr = [];

                                datosstrArr.forEach(function (data, index, arr) {
                                    datosArr.push(+data);

                                });


                                var datos2 = data.presentacion;
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



                                var barChartData = {
                                    labels: tituloArr,
                                    datasets: [{
                                        label: 'Analisis',
                                        backgroundColor: COLOR_Analisis,
                                        data: datosArr,
                                    }, {
                                        label: 'Presentación',
                                        backgroundColor: COLOR_Promocion,
                                        data: datosArr2,
                                    }, {
                                        label: 'Negociación',
                                        backgroundColor: COLOR_Negociacion,
                                        data: datosArr3,
                                    }, {
                                        label: 'Cierre',
                                        backgroundColor: COLOR_Cierre,
                                        data: datosArr4,
                                    }
                                        , {
                                        label: 'Cancelación',
                                        backgroundColor: COLOR_Cancelado,
                                        data: datosArr5,
                                    }]

                                };


                                if (window.myBar != undefined) {

                                    window.myBar.destroy();
                                }

                                var ctx = document.getElementById('myRvR').getContext('2d');
                                window.myBar = new Chart(ctx, {
                                    type: 'bar',
                                    data: barChartData,
                                    options: {
                                        title: {
                                            display: true,
                                            text: 'Monto de proyectos',
                                        },
                                        tooltips: {
                                            mode: 'index',
                                            intersect: false,
                                        },
                                        responsive: true,
                                        display: false,

                                        labels: {
                                            fontSize: 10,
                                            display: false,
                                        },
                                        legend: {
                                            display: false,
                                        },
                                        scaleshowvalue: true,
                                        scales: {
                                            xAxes: [{
                                                stacked: true,
                                                beginAtZero: true,

                                                ticks: {
                                                    stepSize: 1,
                                                    min: 0,
                                                    autoSkip: false,
                                                    beginAtZero: true
                                                }
                                            }],
                                            yAxes: [{
                                                stacked: true,
                                                ticks: {
                                                    beginAtZero: true
                                                }
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


                                var datos = data.Analisis;
                                var datosstrArr = datos.split(",")
                                var datosArr = [];

                                datosstrArr.forEach(function (data, index, arr) {
                                    datosArr.push(+data);

                                });


                                var datos2 = data.presentacion;
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



                                var barChartData = {
                                    labels: tituloArr,
                                    datasets: [{
                                        label: 'Analisis',
                                        backgroundColor: COLOR_Analisis,
                                        data: datosArr,
                                    }, {
                                        label: 'Presentación',
                                        backgroundColor: COLOR_Promocion,
                                        data: datosArr2,
                                    }, {
                                        label: 'Negociación',
                                        backgroundColor: COLOR_Negociacion,
                                        data: datosArr3,
                                    }, {
                                        label: 'Cierre',
                                        backgroundColor: COLOR_Cierre,
                                        data: datosArr4,
                                    }
                                        , {
                                        label: 'Cancelación',
                                        backgroundColor: COLOR_Cancelado,
                                        data: datosArr5,
                                    }]

                                };


                                if (window.myBar != undefined) {

                                    window.myBar.destroy();
                                }

                                var ctx = document.getElementById('myRvR').getContext('2d');
                                window.myBar = new Chart(ctx, {
                                    type: 'bar',
                                    data: barChartData,
                                    options: {
                                        title: {
                                            display: true,
                                            text: 'Cantidad de proyectos',
                                        },
                                        tooltips: {
                                            mode: 'index',
                                            intersect: false,
                                        },
                                        responsive: true,
                                        scaleshowvalue: true,
                                        scales: {
                                            xAxes: [{
                                                stacked: true,
                                                beginAtZero: true,

                                                ticks: {
                                                    stepSize: 1,
                                                    min: 0,
                                                    autoSkip: false,
                                                    beginAtZero: true
                                                }
                                            }],
                                            yAxes: [{
                                                stacked: true,
                                                ticks: {
                                                    beginAtZero: true
                                                }
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
        };

        function graficaSucursalVsSucursalCRM() {
            $('#<%= UpdateProgress1.ClientID %>').css("display", "block");

                var seleccion = tiposvs.GetValue();
                var Sucursal = sucursalevsv.GetSelectedValues();

            var jsDate = AnioInicialrsvs.GetDate();
            var year = jsDate.getFullYear(); // where getFullYear returns the year (four digits)  
            var month = jsDate.getMonth() + 1; // where getMonth returns the month (from 0-11)  
            var day = jsDate.getDate();   // where getDate returns the day of the month (from 1-31)  


            if (month < 10) {
                var myDate = `${day}/0${month}/${year}`
            } else {
                var myDate = `${day}/${month}/${year}`
            }

            var jsDate2 = AnioFinalsvs.GetDate();
            var year2 = jsDate2.getFullYear(); // where getFullYear returns the year (four digits)  
            var month2 = jsDate2.getMonth() + 1; // where getMonth returns the month (from 0-11)  
            var day2 = jsDate2.getDate();   // where getDate returns the day of the month (from 1-31)  

            if (month2 < 10) {
                var myDateFinal = `${day2}/0${month2}/${year2}`
            } else {
                var myDateFinal = `${day2}/${month2}/${year2}`
            }

                console.log(Sucursal);
            var dataValue = "{Seleccion: '" + seleccion + "', sucursales: '" + Sucursal + "', mesAnioInicial: '" + myDate + "', mesAniofinal: '" + myDateFinal + "'}";
                console.log(dataValue);
                $.ajax({
                    type: "POST",
                    url: "ReporteComercialsCRM.aspx/SucursalesVsSucrusalesCRM",
                    data: dataValue,
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    error: function (XMLHttpRequest, textStatus, errorThrown) {

                        modalMensaje(errorThrown);
                    },
                    success: function (response) {

                        if (response != null && response.d != null) {
                            var data = response.d;

                            data = $.parseJSON(data);

                            var id = data.id;

                            if (id == -1) {

                                modalMensaje(data.men);
                            }

                            if (id == 1) {

                                modalMensaje("Favor de seleccionar la sucursal  de la sección de sucursal vs sucursal.");
                            }
                            if (id == 2) {

                                modalMensaje("Favor de seleccionar la fecha inicial y/o final de la sección de sucursal vs sucursal.");
                            }

                            if (id == 3) {

                                modalMensaje("La fecha inicial es mayor a la fecha final  de la sección de sucursal vs sucursal.");
                            }
                            if (id == 5) {


                                var titulo = data.Nombre;
                                var tituloStrArr = titulo.split(",")
                                var tituloArr = [];

                                tituloStrArr.forEach(function (data, index, arr) {
                                    var nombre = data;
                                    tituloArr.push(nombre);
                                });


                                var datos = data.Analisis;
                                var datosstrArr = datos.split(",")
                                var datosArr = [];

                                datosstrArr.forEach(function (data, index, arr) {
                                    datosArr.push(+data);

                                });


                                var datos2 = data.presentacion;
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

                                var barChartData = {
                                    labels: tituloArr,
                                    datasets: [{
                                        label: 'Analisis',
                                        backgroundColor: COLOR_Analisis,
                                        data: datosArr,
                                    }, {
                                        label: 'Presentación',
                                        backgroundColor: COLOR_Promocion,
                                        data: datosArr2,
                                    }, {
                                        label: 'Negociación',
                                        backgroundColor: COLOR_Negociacion,
                                        data: datosArr3,
                                    }, {
                                        label: 'Cierre',
                                        backgroundColor: COLOR_Cierre,
                                        data: datosArr4,
                                    }
                                        , {
                                        label: 'Cancelación',
                                        backgroundColor: COLOR_Cancelado,
                                        data: datosArr5,
                                    }]

                                };

                                var ctx = document.getElementById('mysvs');
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


                                var datos = data.Analisis;
                                var datosstrArr = datos.split(",")
                                var datosArr = [];

                                datosstrArr.forEach(function (data, index, arr) {
                                    datosArr.push(+data);

                                });


                                var datos2 = data.presentacion;
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

                                var barChartData = {
                                    labels: tituloArr,
                                    datasets: [{
                                        label: 'Analisis',
                                        backgroundColor: COLOR_Analisis,
                                        data: datosArr,
                                    }, {
                                        label: 'Presentación',
                                        backgroundColor: COLOR_Promocion,
                                        data: datosArr2,
                                    }, {
                                        label: 'Negociación',
                                        backgroundColor: COLOR_Negociacion,
                                        data: datosArr3,
                                    }, {
                                        label: 'Cierre',
                                        backgroundColor: COLOR_Cierre,
                                        data: datosArr4,
                                    }
                                        , {
                                        label: 'Cancelación',
                                        backgroundColor: COLOR_Cancelado,
                                        data: datosArr5,
                                    }]

                                };

                                var ctx = document.getElementById('mysvs');
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
    </script>
</asp:Content>
