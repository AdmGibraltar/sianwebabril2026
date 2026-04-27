<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01_bootstrap.Master" AutoEventWireup="true" CodeBehind="MonitorActividades.aspx.cs" Inherits="SIANWEB.MonitorActividades" %>


<%@ Register Assembly="DevExpress.Web.Bootstrap.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/jquery-3.3.1.min.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/jquery-template/jquery.loadTemplate.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.min.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/Chart.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/chartjs-plugin-colorschemes.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/Chart.PieceLabel.min.js")%>"></script>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css")%>" />
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/css/font-awesome.min.css")%>" />
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/chartjs-plugin-labels.js")%>"></script>

    <style type="text/css">
        .form-control {
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

        .content3 {
            height: 320px;
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

        .centerText {
            text-align: center;
        }

        .footercenterText {
            text-align: center;
            font-size: medium;
            font-weight: bolder;
        }

        .gridcenterText {
            text-align: center;
            font-size: medium;
            font-weight: bolder;
        }

        .HeadercenterText {
            background-color: cornflowerblue !important;
            text-align: center;
            color: white !important;
        }

            .HeadercenterText a {
                color: white !important;
            }

        .bg-primary {
            background-color: red !important;
        }

        .BlackTextClass {
            color: black;
        }

        .RedBackgroundClass {
            background-color: red;
        }

        .GreenBackgroundClass {
            background-color: forestgreen;
        }

        .YellowBackgroundClass {
            background-color: yellow;
        }

        .WhiteTextClass {
            color: white;
        }

        .BlackTextClass {
            color: black;
        }

        .dx-overlay-wrapper {
            z-index: 5700 !important;
        }

        .dxbs-date-edit .dropdown-menu.panel {
            margin-top: 20px;
            position: fixed !important;
        }

        .dropdown-menu > li > a {
            white-space: pre-wrap !important;
        }

        .panel-body {
            padding: 0px !important;
        }

        .centerText {
            white-space: pre-wrap !important;
            font-size: small;
        }

        .RightText {
            white-space: pre-wrap !important;
            font-size: small;
        }

        .LeftText {
            white-space: pre-wrap !important;
            font-size: small;
        }
    </style>
    <script type="text/javascript"> 
        function Showmessage(message, messagetype) {
            var cssclass;
            switch (messagetype) {
                case 'Success':
                    cssclass = 'alert-success'
                    break;
                case 'Error':
                    cssclass = 'alert-danger'
                    break;
                case 'Warning':
                    cssclass = 'alert-warning'
                    break;
                default:
                    cssclass = 'alert-info'
            }
            $('#alert_container').append('<div id="alert_div" style="margin: 0;position: fixed;top: 50%;left: 10%;width: 50%;-ms-transform: translateY(-50%);transform: translate(40%, -50%); -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + ' text-center"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>  <span>' + message + '</span></div>');
        }
        function AbrirReporteVI() {
            var url = "GC/GC_RepCumplimientoVI_index.aspx";
            var win = window.open(url, '_blank');
            win.focus;
            CargarGraficas();
        }
        window.closeModalDetalle = function () {
            $('#Mapas').modal('hide');
        }
        function modalmapa(IdEmp, IdCd, Id) {
            console.log(IdEmp, IdCd, Id)
            document.getElementById('<%=frameMapa.ClientID%>').src = "https://sianwebmapas.azurewebsites.net/VerMapa.aspx?id_emp=" + IdEmp + "&id_Cd=" + IdCd + "&idAgendaMapa=" + Id;
            $("#Mapas").appendTo("body");
            $("#Mapas").modal({ "backdrop": "static" });
            $('#Mapas').modal('show');
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
        function ActividadCLiente() {
            document.getElementById('<%=iFrameActividad.ClientID%>').src = "MonitorActividadesDetalle.aspx";
            $("#modaledicion").appendTo("body");
            $("#modaledicion").modal({ "backdrop": "static" });
            $('#modaledicion').modal('show');
        }
    </script>
    <div class="container-fluid">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/load.gif" AlternateText="Loading ..."
                        ToolTip="Loading ..." Style="position: fixed; top: 45%; left: 40%;" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <dx:BootstrapPageControl ID="PageControlAutoPostBack" runat="server" TabAlign="Justify" EnableHierarchyRecreation="false">
            <TabPages>
                <dx:BootstrapTabPage Text="Reporte Generales">
                    <ContentCollection>
                        <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                            <asp:UpdatePanel runat="server" ID="UpGeneral">
                                <ContentTemplate>
                                    <%-- Registro de Actividades LLamadas y visitas --%>
                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <div class="panel panel-success">
                                            <div class="panel-heading">
                                                <h3 class="panel-title">Reporte de Actividades</h3>
                                                <span class="pull-right clickable panel-collapsed" style="margin-top: -20px !important;"><i class="glyphicon glyphicon-chevron-up"></i>
                                                </span>
                                            </div>
                                            <div class="panel-body">
                                                <div class="col-md-12" style="margin-top: 1%;">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="Label20" runat="server" Text="Sucursal" />
                                                            </div>
                                                            <div class="col-md-8">
                                                                <dx:BootstrapComboBox ID="ddlSucursal" runat="server">
                                                                </dx:BootstrapComboBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <div class="col-md-4">
                                                                Rol
                                                            </div>
                                                            <div class="col-md-8">
                                                                <dx:BootstrapComboBox ID="DllRol" runat="server" CallbackPageSize="25" DropDownStyle="DropDown" OnSelectedIndexChanged="DllRol_SelectedIndexChanged" AutoPostBack="true">
                                                                </dx:BootstrapComboBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-12" style="margin-top: 1%;">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <div class="col-md-4">
                                                                Usuario
                                                            </div>
                                                            <div class="col-md-8">
                                                                <dx:BootstrapComboBox ID="DllUSuario" runat="server" CallbackPageSize="25" DropDownStyle="DropDown">
                                                                </dx:BootstrapComboBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-12" style="margin-top: 1%;">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <div class="col-md-4">
                                                                Fecha Inicial
                                                            </div>
                                                            <div class="col-md-8">
                                                                <dx:BootstrapDateEdit runat="server" ClientInstanceName="FechaInicial" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="Fecha" EditFormat="Date">
                                                                </dx:BootstrapDateEdit>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <div class="col-md-4">
                                                                Fecha Final
                                                            </div>
                                                            <div class="col-md-8">
                                                                <dx:BootstrapDateEdit runat="server" ClientInstanceName="FechaFinal" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="FechaFinal" EditFormat="Date">
                                                                </dx:BootstrapDateEdit>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="col-md-12" style="margin-top: 5px;">
                                                    <div class="col-md-3">
                                                        <button id="BtnReporte" type="button" runat="server" class="btn btn-primary btn-sm" onserverclick="BtnReporteActividades_ServerClick">
                                                            <i class="fa fa-search" aria-hidden="true"></i>&nbsp;Consultar
                                                        </button>
                                                    </div>
                                                </div>
                                                <div runat="server" id="rsc" visible="false">
                                                    <div class="col-md-12" style="margin-top: 5px;">
                                                        <h2 class="centerText" style="color: cornflowerblue">Representante de Servicio al Cliente </h2>
                                                    </div>
                                                    <div class="col-md-12" style="margin-top: 5px;">
                                                        <div class="col-md-12" style="margin-top: 5px;">
                                                            <dx:BootstrapGridView ID="GrdActividadesRSC" ClientInstanceName="grid" runat="server" KeyFieldName="id_ActividadGral"
                                                                Width="100%" AutoGenerateColumns="False">
                                                                <SettingsBehavior AllowSelectByRowClick="true" SelectionStoringMode="DataIntegrityOptimized" />
                                                                <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" />
                                                                <SettingsEditing Mode="Batch" />
                                                                <Columns>
                                                                    <dx:BootstrapGridViewDataColumn Width="30px" FieldName="id_ActividadGral" Caption="id Actividad" Visible="false">
                                                                    </dx:BootstrapGridViewDataColumn>
                                                                    <dx:BootstrapGridViewDataColumn Width="150px" FieldName="ActividadGral" Caption="Actividad Gral" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                    </dx:BootstrapGridViewDataColumn>
                                                                    <dx:BootstrapGridViewDataColumn Width="30px" FieldName="TiempoHora" Caption="Tiempo Invertido Hr(s)" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                    </dx:BootstrapGridViewDataColumn>
                                                                    <dx:BootstrapGridViewTextColumn Width="30px" FieldName="PorcTiempo" Caption="% Tiempo Invertido" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                        <PropertiesTextEdit DisplayFormatString="p" />
                                                                    </dx:BootstrapGridViewTextColumn>
                                                                </Columns>
                                                                <Templates>
                                                                    <DetailRow>
                                                                        <dx:BootstrapGridView ID="GrdSubActividadesRSC" runat="server" Width="100%" KeyFieldName="id_ActividadGral;id_Actividad" OnBeforePerformDataSelect="GrdSubActividades_BeforePerformDataSelect"
                                                                            AutoGenerateColumns="False">
                                                                            <SettingsPager PageSize="5">
                                                                            </SettingsPager>
                                                                            <Settings ShowFooter="True" />
                                                                            <SettingsBehavior AllowSelectByRowClick="true" SelectionStoringMode="DataIntegrityOptimized" />
                                                                            <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" />
                                                                            <SettingsEditing Mode="Batch" />
                                                                            <Columns>
                                                                                <dx:BootstrapGridViewDataColumn Width="30px" FieldName="id_ActividadGral" Caption="id Actividad General" Visible="false" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                </dx:BootstrapGridViewDataColumn>
                                                                                <dx:BootstrapGridViewDataColumn Width="30px" FieldName="id_Actividad" Caption="id Actividad" Visible="false" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                </dx:BootstrapGridViewDataColumn>
                                                                                <dx:BootstrapGridViewDataColumn Width="150px" FieldName="Actividad" Caption="Actividad" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                </dx:BootstrapGridViewDataColumn>
                                                                                <dx:BootstrapGridViewDataColumn Width="30px" FieldName="TiempoHora" Caption="Tiempo Invertido Hr(s)" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                </dx:BootstrapGridViewDataColumn>
                                                                            </Columns>
                                                                            <Templates>
                                                                                <DetailRow>
                                                                                    <dx:BootstrapGridView ID="grdActividadesdesgloseRSC" runat="server" Width="100%" KeyFieldName="id_ActividadGral;id_Actividad" OnBeforePerformDataSelect="grdActividadesdesglose_BeforePerformDataSelect"
                                                                                        AutoGenerateColumns="False">
                                                                                        <SettingsPager PageSize="5">
                                                                                        </SettingsPager>
                                                                                        <Columns>
                                                                                            <dx:BootstrapGridViewDataColumn Width="30px" FieldName="ID" Caption="id" Visible="false" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                            </dx:BootstrapGridViewDataColumn>
                                                                                            <dx:BootstrapGridViewDataColumn Width="30px" FieldName="id_ActividadGral" Caption="id Actividad General" Visible="false" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                            </dx:BootstrapGridViewDataColumn>
                                                                                            <dx:BootstrapGridViewDataColumn Width="30px" FieldName="id_Actividad" Caption="id Actividad" Visible="false" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                            </dx:BootstrapGridViewDataColumn>
                                                                                            <dx:BootstrapGridViewDataColumn Width="30px" FieldName="usuario" Caption="Usuario" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                            </dx:BootstrapGridViewDataColumn>
                                                                                            <dx:BootstrapGridViewDataColumn Width="30px" FieldName="id_cte" Caption="# Cliente" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                            </dx:BootstrapGridViewDataColumn>
                                                                                            <dx:BootstrapGridViewDataColumn Width="30px" FieldName="nombre" Caption="Cliente" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                            </dx:BootstrapGridViewDataColumn>
                                                                                            <dx:BootstrapGridViewDataColumn Width="30px" FieldName="inicioEjecucion" Caption="Fecha Inicio" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                            </dx:BootstrapGridViewDataColumn>
                                                                                            <dx:BootstrapGridViewDataColumn Width="30px" FieldName="finalEjecucion" Caption="Fecha Terminación" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                            </dx:BootstrapGridViewDataColumn>
                                                                                            <dx:BootstrapGridViewDataColumn Width="30px" FieldName="TiempoHora" Caption="Tiempo Invertido Hr(s)" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                            </dx:BootstrapGridViewDataColumn>
                                                                                            <dx:BootstrapGridViewDateColumn Width="40px" Caption="Rep. Cumpl. Cliente" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                                <DataItemTemplate>
                                                                                                    <button type="button" class="btn btn-link" id="BtnCliente" title="Datos Cliente" runat="server" onserverclick="BtnCliente_ServerClick">
                                                                                                        <span class="fa fa-file"></span>
                                                                                                    </button>
                                                                                                </DataItemTemplate>
                                                                                            </dx:BootstrapGridViewDateColumn>
                                                                                            <dx:BootstrapGridViewDateColumn Width="40px" Caption="Geolocalización" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                                <DataItemTemplate>
                                                                                                    <button id="btnCordenadas" type="button" class="btn btn-link" title="Geolocalización" runat="server"
                                                                                                        onserverclick="btnCordenadas_ServerClick">
                                                                                                        <span class="fa fa-map"></span>
                                                                                                    </button>
                                                                                                </DataItemTemplate>
                                                                                            </dx:BootstrapGridViewDateColumn>
                                                                                        </Columns>
                                                                                    </dx:BootstrapGridView>
                                                                                </DetailRow>
                                                                            </Templates>
                                                                        </dx:BootstrapGridView>
                                                                    </DetailRow>
                                                                </Templates>
                                                            </dx:BootstrapGridView>
                                                        </div>
                                                        <div class="col-md-4">
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12" style="margin-top: 5px;">
                                                        <div class="col-md-12" style="margin-top: 5px;">
                                                            <dx:BootstrapGridView ID="GrdActividadesRSCCampo" ClientInstanceName="grid" runat="server" KeyFieldName="id_ActividadGral"
                                                                Width="100%" AutoGenerateColumns="False">
                                                                <SettingsBehavior AllowSelectByRowClick="true" SelectionStoringMode="DataIntegrityOptimized" />
                                                                <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" />
                                                                <SettingsEditing Mode="Batch" />
                                                                <Columns>
                                                                    <dx:BootstrapGridViewDataColumn Width="30px" FieldName="id_ActividadGral" Caption="id Actividad" Visible="false">
                                                                    </dx:BootstrapGridViewDataColumn>
                                                                    <dx:BootstrapGridViewDataColumn Width="150px" FieldName="ActividadGral" Caption="Actividad Gral" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                    </dx:BootstrapGridViewDataColumn>
                                                                    <dx:BootstrapGridViewDataColumn Width="30px" FieldName="TiempoHora" Caption="Tiempo Invertido Hr(s)" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                    </dx:BootstrapGridViewDataColumn>
                                                                    <dx:BootstrapGridViewTextColumn Width="30px" FieldName="PorcTiempo" Caption="% Tiempo Invertido" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                        <PropertiesTextEdit DisplayFormatString="p" />
                                                                    </dx:BootstrapGridViewTextColumn>
                                                                </Columns>
                                                                <Templates>
                                                                    <DetailRow>
                                                                        <dx:BootstrapGridView ID="GrdSubActividadesRSC" runat="server" Width="100%" KeyFieldName="id_ActividadGral;id_Actividad" OnBeforePerformDataSelect="GrdSubActividades_BeforePerformDataSelect"
                                                                            AutoGenerateColumns="False">
                                                                            <SettingsPager PageSize="5">
                                                                            </SettingsPager>
                                                                            <Settings ShowFooter="True" />
                                                                            <SettingsBehavior AllowSelectByRowClick="true" SelectionStoringMode="DataIntegrityOptimized" />
                                                                            <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" />
                                                                            <SettingsEditing Mode="Batch" />
                                                                            <Columns>
                                                                                <dx:BootstrapGridViewDataColumn Width="30px" FieldName="id_ActividadGral" Caption="id Actividad General" Visible="false" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                </dx:BootstrapGridViewDataColumn>
                                                                                <dx:BootstrapGridViewDataColumn Width="30px" FieldName="id_Actividad" Caption="id Actividad" Visible="false" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                </dx:BootstrapGridViewDataColumn>
                                                                                <dx:BootstrapGridViewDataColumn Width="150px" FieldName="Actividad" Caption="Actividad" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                </dx:BootstrapGridViewDataColumn>
                                                                                <dx:BootstrapGridViewDataColumn Width="30px" FieldName="TiempoHora" Caption="Tiempo Invertido Hr(s)" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                </dx:BootstrapGridViewDataColumn>
                                                                            </Columns>
                                                                            <Templates>
                                                                                <DetailRow>
                                                                                    <dx:BootstrapGridView ID="grdActividadesdesgloseRSC" runat="server" Width="100%" KeyFieldName="id_ActividadGral;id_Actividad" OnBeforePerformDataSelect="grdActividadesdesglose_BeforePerformDataSelect"
                                                                                        AutoGenerateColumns="False">
                                                                                        <SettingsPager PageSize="5">
                                                                                        </SettingsPager>
                                                                                        <Columns>
                                                                                            <dx:BootstrapGridViewDataColumn Width="30px" FieldName="ID" Caption="id" Visible="false" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                            </dx:BootstrapGridViewDataColumn>
                                                                                            <dx:BootstrapGridViewDataColumn Width="30px" FieldName="id_ActividadGral" Caption="id Actividad General" Visible="false" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                            </dx:BootstrapGridViewDataColumn>
                                                                                            <dx:BootstrapGridViewDataColumn Width="30px" FieldName="id_Actividad" Caption="id Actividad" Visible="false" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                            </dx:BootstrapGridViewDataColumn>
                                                                                            <dx:BootstrapGridViewDataColumn Width="30px" FieldName="usuario" Caption="Usuario" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                            </dx:BootstrapGridViewDataColumn>
                                                                                            <dx:BootstrapGridViewDataColumn Width="30px" FieldName="id_cte" Caption="# Cliente" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                            </dx:BootstrapGridViewDataColumn>
                                                                                            <dx:BootstrapGridViewDataColumn Width="30px" FieldName="nombre" Caption="Cliente" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                            </dx:BootstrapGridViewDataColumn>
                                                                                            <dx:BootstrapGridViewDataColumn Width="30px" FieldName="inicioEjecucion" Caption="Fecha Inicio" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                            </dx:BootstrapGridViewDataColumn>
                                                                                            <dx:BootstrapGridViewDataColumn Width="30px" FieldName="finalEjecucion" Caption="Fecha Terminación" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                            </dx:BootstrapGridViewDataColumn>
                                                                                            <dx:BootstrapGridViewDataColumn Width="30px" FieldName="TiempoHora" Caption="Tiempo Invertido Hr(s)" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                            </dx:BootstrapGridViewDataColumn>
                                                                                            <dx:BootstrapGridViewDateColumn Width="40px" Caption="Rep. Cumpl. Cliente" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                                <DataItemTemplate>
                                                                                                    <button type="button" class="btn btn-link" id="BtnCliente" title="Datos Cliente" runat="server" onserverclick="BtnCliente_ServerClick">
                                                                                                        <span class="fa fa-file"></span>
                                                                                                    </button>
                                                                                                </DataItemTemplate>
                                                                                            </dx:BootstrapGridViewDateColumn>
                                                                                            <dx:BootstrapGridViewDateColumn Width="40px" Caption="Geolocalización" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                                <DataItemTemplate>
                                                                                                    <button id="btnCordenadas" type="button" class="btn btn-link" title="Geolocalización" runat="server"
                                                                                                        onserverclick="btnCordenadas_ServerClick">
                                                                                                        <span class="fa fa-map"></span>
                                                                                                    </button>
                                                                                                </DataItemTemplate>
                                                                                            </dx:BootstrapGridViewDateColumn>
                                                                                        </Columns>
                                                                                    </dx:BootstrapGridView>
                                                                                </DetailRow>
                                                                            </Templates>
                                                                        </dx:BootstrapGridView>
                                                                    </DetailRow>
                                                                </Templates>
                                                            </dx:BootstrapGridView>
                                                        </div>
                                                        <div class="col-md-4">
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12" style="margin-top: 5px;">
                                                        <div class="col-md-12">
                                                            <div style="float: right; color: cornflowerblue">
                                                                <asp:Label runat="server" ID="lblActividadTotalRsc" Font-Size="Large" Font-Bold="true"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div runat="server" id="Asesor" visible="false">
                                                    <div class="col-md-12" style="margin-top: 5px;">
                                                        <h2 class="centerText" style="color: cornflowerblue">Asesor</h2>
                                                    </div>
                                                    <div class="col-md-12" style="margin-top: 5px;">
                                                        <div class="col-md-12" style="margin-top: 5px;">
                                                            <dx:BootstrapGridView ID="grdActividadeAsesor" ClientInstanceName="grid" runat="server" KeyFieldName="id_ActividadGral"
                                                                Width="100%" AutoGenerateColumns="False">
                                                                <SettingsBehavior AllowSelectByRowClick="true" SelectionStoringMode="DataIntegrityOptimized" />
                                                                <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" />
                                                                <SettingsEditing Mode="Batch" />
                                                                <Columns>
                                                                    <dx:BootstrapGridViewDataColumn Width="30px" FieldName="id_ActividadGral" Caption="id Actividad" Visible="false">
                                                                    </dx:BootstrapGridViewDataColumn>
                                                                    <dx:BootstrapGridViewDataColumn Width="150px" FieldName="ActividadGral" Caption="Actividad Gral" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                    </dx:BootstrapGridViewDataColumn>
                                                                    <dx:BootstrapGridViewDataColumn Width="30px" FieldName="TiempoHora" Caption="Tiempo Invertido Hr(s)" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                    </dx:BootstrapGridViewDataColumn>
                                                                    <dx:BootstrapGridViewTextColumn Width="30px" FieldName="PorcTiempo" Caption="% Tiempo Invertido" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                        <PropertiesTextEdit DisplayFormatString="p" />
                                                                    </dx:BootstrapGridViewTextColumn>
                                                                </Columns>
                                                                <Templates>
                                                                    <DetailRow>
                                                                        <dx:BootstrapGridView ID="GrdSubActividadesAsesor" runat="server" Width="100%" KeyFieldName="id_ActividadGral;id_Actividad" OnBeforePerformDataSelect="GrdSubActividades_BeforePerformDataSelect"
                                                                            AutoGenerateColumns="False">
                                                                            <SettingsPager PageSize="5">
                                                                            </SettingsPager>
                                                                            <Settings ShowFooter="True" />
                                                                            <SettingsBehavior AllowSelectByRowClick="true" SelectionStoringMode="DataIntegrityOptimized" />
                                                                            <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" />
                                                                            <SettingsEditing Mode="Batch" />
                                                                            <Columns>
                                                                                <dx:BootstrapGridViewDataColumn Width="30px" FieldName="id_ActividadGral" Caption="id Actividad General" Visible="false" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                </dx:BootstrapGridViewDataColumn>
                                                                                <dx:BootstrapGridViewDataColumn Width="30px" FieldName="id_Actividad" Caption="id Actividad" Visible="false" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                </dx:BootstrapGridViewDataColumn>
                                                                                <dx:BootstrapGridViewDataColumn Width="150px" FieldName="Actividad" Caption="Actividad" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                </dx:BootstrapGridViewDataColumn>
                                                                                <dx:BootstrapGridViewDataColumn Width="30px" FieldName="TiempoHora" Caption="Tiempo Invertido Hr(s)" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                </dx:BootstrapGridViewDataColumn>
                                                                            </Columns>
                                                                            <Templates>
                                                                                <DetailRow>
                                                                                    <dx:BootstrapGridView ID="grdActividadesdesgloseAsesor" runat="server" Width="100%" KeyFieldName="id_ActividadGral;id_Actividad" OnBeforePerformDataSelect="grdActividadesdesglose_BeforePerformDataSelect"
                                                                                        AutoGenerateColumns="False">
                                                                                        <SettingsPager PageSize="5">
                                                                                        </SettingsPager>
                                                                                        <Columns>
                                                                                            <dx:BootstrapGridViewDataColumn Width="30px" FieldName="ID" Caption="id Actividad General" Visible="false" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                            </dx:BootstrapGridViewDataColumn>
                                                                                            <dx:BootstrapGridViewDataColumn Width="30px" FieldName="id_ActividadGral" Caption="id Actividad General" Visible="false" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                            </dx:BootstrapGridViewDataColumn>
                                                                                            <dx:BootstrapGridViewDataColumn Width="30px" FieldName="id_Actividad" Caption="id Actividad" Visible="false" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                            </dx:BootstrapGridViewDataColumn>
                                                                                            <dx:BootstrapGridViewDataColumn Width="30px" FieldName="usuario" Caption="Usuario" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                            </dx:BootstrapGridViewDataColumn>
                                                                                            <dx:BootstrapGridViewDataColumn Width="30px" FieldName="id_cte" Caption="# Cliente" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                            </dx:BootstrapGridViewDataColumn>
                                                                                            <dx:BootstrapGridViewDataColumn Width="30px" FieldName="nombre" Caption="Cliente" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                            </dx:BootstrapGridViewDataColumn>
                                                                                            <dx:BootstrapGridViewDataColumn Width="30px" FieldName="inicioEjecucion" Caption="Fecha Inicio" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                            </dx:BootstrapGridViewDataColumn>
                                                                                            <dx:BootstrapGridViewDataColumn Width="30px" FieldName="finalEjecucion" Caption="Fecha Terminación" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                            </dx:BootstrapGridViewDataColumn>
                                                                                            <dx:BootstrapGridViewDataColumn Width="30px" FieldName="TiempoHora" Caption="Tiempo Invertido Hr(s)" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                            </dx:BootstrapGridViewDataColumn>
                                                                                            <dx:BootstrapGridViewDateColumn Width="40px" Caption="Rep. Cumpl. Cliente" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                                <DataItemTemplate>
                                                                                                    <button type="button" class="btn btn-link" id="BtnCliente" title="Datos Cliente" runat="server" onserverclick="BtnCliente_ServerClick">
                                                                                                        <span class="fa fa-file"></span>
                                                                                                    </button>
                                                                                                </DataItemTemplate>
                                                                                            </dx:BootstrapGridViewDateColumn>
                                                                                            <dx:BootstrapGridViewDateColumn Width="40px" Caption="Geolocalización" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                                <DataItemTemplate>
                                                                                                    <button id="btnCordenadas" type="button" class="btn btn-link" title="Geolocalización" runat="server"
                                                                                                        onserverclick="btnCordenadas_ServerClick">
                                                                                                        <span class="fa fa-map"></span>
                                                                                                    </button>
                                                                                                </DataItemTemplate>
                                                                                            </dx:BootstrapGridViewDateColumn>
                                                                                        </Columns>
                                                                                    </dx:BootstrapGridView>
                                                                                </DetailRow>
                                                                            </Templates>
                                                                        </dx:BootstrapGridView>
                                                                    </DetailRow>
                                                                </Templates>
                                                            </dx:BootstrapGridView>
                                                        </div>
                                                        <div class="col-md-12" style="margin-top: 5px;">
                                                            <dx:BootstrapGridView ID="grdAsesorCampo" ClientInstanceName="grid" runat="server" KeyFieldName="id_ActividadGral"
                                                                Width="100%" AutoGenerateColumns="False">
                                                                <SettingsBehavior AllowSelectByRowClick="true" SelectionStoringMode="DataIntegrityOptimized" />
                                                                <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" />
                                                                <SettingsEditing Mode="Batch" />
                                                                <Columns>
                                                                    <dx:BootstrapGridViewDataColumn Width="30px" FieldName="id_ActividadGral" Caption="id Actividad" Visible="false">
                                                                    </dx:BootstrapGridViewDataColumn>
                                                                    <dx:BootstrapGridViewDataColumn Width="150px" FieldName="ActividadGral" Caption="Actividad Gral" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                    </dx:BootstrapGridViewDataColumn>
                                                                    <dx:BootstrapGridViewDataColumn Width="30px" FieldName="TiempoHora" Caption="Tiempo Invertido Hr(s)" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                    </dx:BootstrapGridViewDataColumn>
                                                                    <dx:BootstrapGridViewTextColumn Width="30px" FieldName="PorcTiempo" Caption="% Tiempo Invertido" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                        <PropertiesTextEdit DisplayFormatString="p" />
                                                                    </dx:BootstrapGridViewTextColumn>
                                                                </Columns>
                                                                <Templates>
                                                                    <DetailRow>
                                                                        <dx:BootstrapGridView ID="GrdSubActividadesAsesor" runat="server" Width="100%" KeyFieldName="id_ActividadGral;id_Actividad" OnBeforePerformDataSelect="GrdSubActividades_BeforePerformDataSelect"
                                                                            AutoGenerateColumns="False">
                                                                            <SettingsPager PageSize="5">
                                                                            </SettingsPager>
                                                                            <Settings ShowFooter="True" />
                                                                            <SettingsBehavior AllowSelectByRowClick="true" SelectionStoringMode="DataIntegrityOptimized" />
                                                                            <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" />
                                                                            <SettingsEditing Mode="Batch" />
                                                                            <Columns>
                                                                                <dx:BootstrapGridViewDataColumn Width="30px" FieldName="id_ActividadGral" Caption="id Actividad General" Visible="false" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                </dx:BootstrapGridViewDataColumn>
                                                                                <dx:BootstrapGridViewDataColumn Width="30px" FieldName="id_Actividad" Caption="id Actividad" Visible="false" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                </dx:BootstrapGridViewDataColumn>
                                                                                <dx:BootstrapGridViewDataColumn Width="150px" FieldName="Actividad" Caption="Actividad" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                </dx:BootstrapGridViewDataColumn>
                                                                                <dx:BootstrapGridViewDataColumn Width="30px" FieldName="TiempoHora" Caption="Tiempo Invertido Hr(s)" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                </dx:BootstrapGridViewDataColumn>
                                                                            </Columns>
                                                                            <Templates>
                                                                                <DetailRow>
                                                                                    <dx:BootstrapGridView ID="grdActividadesdesgloseAsesor" runat="server" Width="100%" KeyFieldName="id_ActividadGral;id_Actividad" OnBeforePerformDataSelect="grdActividadesdesglose_BeforePerformDataSelect"
                                                                                        AutoGenerateColumns="False">
                                                                                        <SettingsPager PageSize="5">
                                                                                        </SettingsPager>
                                                                                        <Columns>
                                                                                            <dx:BootstrapGridViewDataColumn Width="30px" FieldName="ID" Caption="id Actividad General" Visible="false" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                            </dx:BootstrapGridViewDataColumn>
                                                                                            <dx:BootstrapGridViewDataColumn Width="30px" FieldName="id_ActividadGral" Caption="id Actividad General" Visible="false" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                            </dx:BootstrapGridViewDataColumn>
                                                                                            <dx:BootstrapGridViewDataColumn Width="30px" FieldName="id_Actividad" Caption="id Actividad" Visible="false" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                            </dx:BootstrapGridViewDataColumn>
                                                                                            <dx:BootstrapGridViewDataColumn Width="30px" FieldName="usuario" Caption="Usuario" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                            </dx:BootstrapGridViewDataColumn>
                                                                                            <dx:BootstrapGridViewDataColumn Width="30px" FieldName="id_cte" Caption="# Cliente" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                            </dx:BootstrapGridViewDataColumn>
                                                                                            <dx:BootstrapGridViewDataColumn Width="30px" FieldName="nombre" Caption="Cliente" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                            </dx:BootstrapGridViewDataColumn>
                                                                                            <dx:BootstrapGridViewDataColumn Width="30px" FieldName="inicioEjecucion" Caption="Fecha Inicio" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                            </dx:BootstrapGridViewDataColumn>
                                                                                            <dx:BootstrapGridViewDataColumn Width="30px" FieldName="finalEjecucion" Caption="Fecha Terminación" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                            </dx:BootstrapGridViewDataColumn>
                                                                                            <dx:BootstrapGridViewDataColumn Width="30px" FieldName="TiempoHora" Caption="Tiempo Invertido Hr(s)" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                            </dx:BootstrapGridViewDataColumn>
                                                                                            <dx:BootstrapGridViewDateColumn Width="40px" Caption="Rep. Cumpl. Cliente" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                                <DataItemTemplate>
                                                                                                    <button type="button" class="btn btn-link" id="BtnCliente" title="Datos Cliente" runat="server" onserverclick="BtnCliente_ServerClick">
                                                                                                        <span class="fa fa-file"></span>
                                                                                                    </button>
                                                                                                </DataItemTemplate>
                                                                                            </dx:BootstrapGridViewDateColumn>
                                                                                            <dx:BootstrapGridViewDateColumn Width="40px" Caption="Geolocalización" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                                                <DataItemTemplate>
                                                                                                    <button id="btnCordenadas" type="button" class="btn btn-link" title="Geolocalización" runat="server"
                                                                                                        onserverclick="btnCordenadas_ServerClick">
                                                                                                        <span class="fa fa-map"></span>
                                                                                                    </button>
                                                                                                </DataItemTemplate>
                                                                                            </dx:BootstrapGridViewDateColumn>
                                                                                        </Columns>
                                                                                    </dx:BootstrapGridView>
                                                                                </DetailRow>
                                                                            </Templates>
                                                                        </dx:BootstrapGridView>
                                                                    </DetailRow>
                                                                </Templates>
                                                            </dx:BootstrapGridView>
                                                        </div>
                                                        <div class="col-md-4">
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12" style="margin-top: 5px;">
                                                        <div class="col-md-12">
                                                            <div style="float: right; color: cornflowerblue">
                                                                <asp:Label runat="server" ID="lblTotalAsesor" Font-Size="Large" Font-Bold="true"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="ReporteAgenda" runat="server" visible="false" style="margin-top: 5px;">
                                                    <div class="col-md-12">
                                                        <div style="float: left">
                                                            <span id="total" style="font-size: 16px;" runat="server"></span>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-2">
                                                    </div>
                                                    <div class="col-md-8">
                                                        <canvas id="myChartAgenda" width="200" height="80"></canvas>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- reporte de perdida de clientes --%>
                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <div class="panel panel-success">
                                            <div class="panel-heading">
                                                <h3 class="panel-title">Reporte de Perdida de Clientes</h3>
                                                <span class="pull-right clickable panel-collapsed" style="margin-top: -20px !important;"><i class="glyphicon glyphicon-chevron-up"></i>
                                                </span>
                                            </div>
                                            <div class="panel-body">
                                                <div class="col-md-12" style="margin-top: 1%;">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="Label1" runat="server" Text="Sucursal" />
                                                            </div>
                                                            <div class="col-md-8">
                                                                <dx:BootstrapComboBox ID="ddlSucursalRepPerdidaCliente" runat="server">
                                                                </dx:BootstrapComboBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <div class="col-md-4" style="padding: 0%">
                                                                Fecha
                                                            </div>
                                                            <div class="col-md-8" style="padding: 0%">
                                                                <dx:BootstrapDateEdit runat="server" ClientInstanceName="fechaConsulta" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="FechaCliente"></dx:BootstrapDateEdit>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-12" style="margin-top: 5px;">
                                                    <div class="col-md-3">
                                                        <button id="BtnConsultaCliente" type="button" runat="server" class="btn btn-primary btn-sm" onserverclick="BtnConsultaCliente_ServerClick">
                                                            <i class="fa fa-search" aria-hidden="true"></i>&nbsp;Consultar
                                                        </button>
                                                        <button type="submit" runat="server" class="btn btn-default btn-sm" id="btnPerdidaClienteexcel"
                                                            onserverclick="btnPerdidaClienteexcel_ServerClick">
                                                            <i class="fa fa-file-excel-o" aria-hidden="true"></i>&nbsp;Excel
                                                        </button>
                                                    </div>

                                                </div>
                                                <div runat="server" id="Cliente" visible="false">
                                                    <div class="col-md-12" style="margin-top: 5px;">
                                                        <div class="col-md-2">
                                                        </div>
                                                        <div class="col-md-8">
                                                            <canvas id="myChartCliente" width="200" height="80"></canvas>
                                                        </div>
                                                        <div class="col-md-12" style="margin-top: 5px;">
                                                            <div class="col-md-3">
                                                            </div>
                                                            <div class="col-md-6">
                                                                <dx:BootstrapGridView ID="GrdFacturacionCliente" ClientInstanceName="grid" runat="server" KeyFieldName="id_cte"
                                                                    Width="100%" AutoGenerateColumns="False">
                                                                    <Settings ShowHeaderFilterButton="true" />
                                                                    <SettingsPager PageSize="5" NumericButtonCount="4">
                                                                        <Summary Visible="false" />
                                                                        <PageSizeItemSettings Visible="true" ShowAllItem="true" />
                                                                    </SettingsPager>
                                                                    <Columns>
                                                                        <dx:BootstrapGridViewDataColumn Width="30px" FieldName="id_cte" Caption="Núm. Cliente" CssClasses-DataCell="RightText" CssClasses-HeaderCell="centerText">
                                                                        </dx:BootstrapGridViewDataColumn>
                                                                        <dx:BootstrapGridViewDataColumn Width="150px" FieldName="nombreCliente" Caption="Nombre" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                        </dx:BootstrapGridViewDataColumn>
                                                                        <dx:BootstrapGridViewDataColumn Width="30px" FieldName="Factura" Caption="Ultima Factura" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText" Visible="true">
                                                                        </dx:BootstrapGridViewDataColumn>
                                                                        <dx:BootstrapGridViewDataColumn Width="30px" FieldName="FechaFactura" Caption="Fecha Ultima Factura" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText" Visible="true">
                                                                        </dx:BootstrapGridViewDataColumn>
                                                                        <dx:BootstrapGridViewDataColumn Width="30px" FieldName="mesFactura" Caption="mes(es) sin Facturación" CssClasses-DataCell="RightText" CssClasses-HeaderCell="centerText">
                                                                        </dx:BootstrapGridViewDataColumn>
                                                                    </Columns>
                                                                </dx:BootstrapGridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- reporte de Captacion de pedido --%>
                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <div class="panel panel-success">
                                            <div class="panel-heading">
                                                <h3 class="panel-title">Reporte de Captación de Pedido</h3>
                                                <span class="pull-right clickable panel-collapsed" style="margin-top: -20px !important;"><i class="glyphicon glyphicon-chevron-up"></i>
                                                </span>
                                            </div>
                                            <div class="panel-body">
                                                <div class="col-md-12" style="margin-top: 1%;">
                                                    <div class="col-md-12" style="margin-top: 1%;">
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <div class="col-md-4">
                                                                    <asp:Label ID="Label2" runat="server" Text="Sucursal" />
                                                                </div>
                                                                <div class="col-md-8">
                                                                    <dx:BootstrapComboBox ID="DllReporteCaptacionPedido" runat="server">
                                                                    </dx:BootstrapComboBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6" style="display: none">
                                                            <div class="form-group">
                                                                <div class="col-md-4">
                                                                    Tipo de Pedido
                                                                </div>
                                                                <div class="col-md-8">
                                                                    <dx:BootstrapComboBox ID="ddlTipoPedido" runat="server" CallbackPageSize="10" DropDownStyle="DropDown">
                                                                        <Items>
                                                                            <dx:BootstrapListEditItem Value="Todos" Text="--Todos--" Selected="true" />
                                                                            <dx:BootstrapListEditItem Value="Pedidos Internet" Text="Pedidos Internet" />
                                                                            <dx:BootstrapListEditItem Value="Pedidos Orden Centralizada" Text="Pedidos Orden Centralizada" />
                                                                            <dx:BootstrapListEditItem Value="Pedidos Orden de Compra" Text="Pedidos Orden de Compra" />
                                                                            <dx:BootstrapListEditItem Value="Pedidos Portal de Cliente" Text="Pedidos Portal de Cliente" />
                                                                            <dx:BootstrapListEditItem Value="Pedidos Venta Instalada" Text="Pedidos Venta Instalada" />
                                                                            <dx:BootstrapListEditItem Value="Pedidos Venta nuevo y/o esporádicos" Text="Pedidos Venta nuevo y/o esporádicos" />
                                                                        </Items>
                                                                    </dx:BootstrapComboBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>


                                                    <div class="col-md-12" style="margin-top: 5px;">
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <div class="col-md-4">
                                                                    Fecha
                                                                </div>
                                                                <div class="col-md-8">
                                                                    <dx:ASPxDateEdit runat="server" CssClass="form-control" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="FechaCaptacionInical" PickerType="Months">
                                                                        <CalendarProperties>
                                                                            <FastNavProperties InitialView="Years" MaxView="Years" />
                                                                        </CalendarProperties>
                                                                    </dx:ASPxDateEdit>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="col-md-12" style="margin-top: 5px;">
                                                    <div class="col-md-3">
                                                        <button id="BtnReporteCaptacion" type="button" runat="server" class="btn btn-primary btn-sm" onserverclick="BtnReporteCaptacion_ServerClick">
                                                            <i class="fa fa-search" aria-hidden="true"></i>&nbsp;Consultar
                                                        </button>

                                                        <button type="submit" runat="server" class="btn btn-default btn-sm" id="btnreporteCaptacionexcel"
                                                            onserverclick="btnreporteCaptacion_ServerClick1">
                                                            <i class="fa fa-file-excel-o" aria-hidden="true"></i>&nbsp;Excel
                                                        </button>
                                                    </div>
                                                </div>
                                                <div runat="server" id="CaptacionPedido" visible="false">
                                                    <div class="col-md-12" style="margin-top: 5px;">
                                                        <div class="col-md-1">
                                                        </div>
                                                        <div class="col-md-10">
                                                            <canvas id="myChartPedidos" width="800" height="280"></canvas>
                                                        </div>
                                                        <div class="col-md-3" style="margin-top: 5px;">
                                                        </div>
                                                        <div class="col-md-6" style="margin-top: 5px;">
                                                            <dx:BootstrapGridView ID="GrdCaptacionPedidos" ClientInstanceName="grid" runat="server" KeyFieldName="id_cte"
                                                                Width="100%" AutoGenerateColumns="False">
                                                                <SettingsPager PageSize="8" NumericButtonCount="4">
                                                                    <Summary Visible="false" />
                                                                    <PageSizeItemSettings Visible="false" ShowAllItem="false" />
                                                                </SettingsPager>
                                                                <Settings ShowFooter="true" />
                                                                <Columns>
                                                                    <dx:BootstrapGridViewDataColumn Width="40px" FieldName="Filtro_Frecuencia" Caption="Tipo Pedido" CssClasses-DataCell="RightText" CssClasses-HeaderCell="centerText">
                                                                    </dx:BootstrapGridViewDataColumn>
                                                                    <dx:BootstrapGridViewDataColumn Width="30px" FieldName="cantidad" Caption="Cantidad" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                    </dx:BootstrapGridViewDataColumn>
                                                                    <dx:BootstrapGridViewTextColumn Width="30px" FieldName="TotalFacturacion" Caption="Total Facturación" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText" Visible="true">
                                                                        <PropertiesTextEdit DisplayFormatString="c" />
                                                                    </dx:BootstrapGridViewTextColumn>
                                                                </Columns>
                                                            </dx:BootstrapGridView>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- Reporte Tracking de calendario --%>
                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <div class="panel panel-success">
                                            <div class="panel-heading">
                                                <h3 class="panel-title">Tracking de Actividades</h3>
                                                <span class="pull-right clickable panel-collapsed" style="margin-top: -20px !important;"><i class="glyphicon glyphicon-chevron-up"></i>
                                                </span>
                                            </div>
                                            <div class="panel-body">
                                                <div class="col-md-12" style="margin-top: 2%;">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="Label4" runat="server" Text="Sucursal" />
                                                            </div>
                                                            <div class="col-md-8">
                                                                <dx:BootstrapComboBox ID="DllSucursalTracking" runat="server">
                                                                </dx:BootstrapComboBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <div class="col-md-2">
                                                                Rol
                                                            </div>
                                                            <div class="col-md-10">
                                                                <dx:BootstrapComboBox ID="DllRolTracking" runat="server" CallbackPageSize="25" DropDownStyle="DropDown" OnSelectedIndexChanged="DllRolTracking_SelectedIndexChanged" AutoPostBack="true">
                                                                </dx:BootstrapComboBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-12" style="margin-top: 1%;">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <div class="col-md-4">
                                                                Usuario
                                                            </div>
                                                            <div class="col-md-8">
                                                                <dx:BootstrapComboBox ID="dllUsuarioTracking" runat="server" CallbackPageSize="25" DropDownStyle="DropDown" AutoPostBack="true">
                                                                </dx:BootstrapComboBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-12" style="margin-top: 5px;">
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="Label10" Text="Seleccionar Fecha" runat="server"> </asp:Label>
                                                            </div>
                                                            <div class="col-md-8">
                                                                <asp:RadioButtonList runat="Server" CssClass="form-control3" ID="RBFecha" RepeatDirection="Vertical" OnSelectedIndexChanged="RBFecha_SelectedIndexChanged" AutoPostBack="true">
                                                                    <asp:ListItem runat="Server" Value="1" Text="Fecha(s)" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem runat="Server" Value="2" Text="Mes/Año"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <%--Rango de Fechas--%>
                                                    <div class="col-md-8">
                                                        <div id="Rango" runat="server">
                                                            <div class="col-md-12" style="margin-top: 1%;">
                                                                <div class="col-md-6">
                                                                    <div class="form-group">
                                                                        <div class="col-md-4">
                                                                            Fecha Inicial
                                                                        </div>
                                                                        <div class="col-md-8">
                                                                            <dx:BootstrapDateEdit runat="server" ClientInstanceName="FechaInicial" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="txtFechaInicialTracking" EditFormat="Date">
                                                                            </dx:BootstrapDateEdit>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group">
                                                                        <div class="col-md-4">
                                                                            Fecha Final
                                                                        </div>
                                                                        <div class="col-md-8">
                                                                            <dx:BootstrapDateEdit runat="server" ClientInstanceName="FechaFinal" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="txtFechaFinalTracking" EditFormat="Date">
                                                                            </dx:BootstrapDateEdit>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                    </div>
                                                    <div class="col-md-4">
                                                        <%--mes y año--%>
                                                        <div runat="server" id="Mes">
                                                            <div class="col-md-12" style="margin-top: 5px;">
                                                                <div class="form-group">
                                                                    <div class="col-md-4">
                                                                        <asp:Label ID="Label11" runat="server" Text="Fecha" />
                                                                    </div>
                                                                    <div class="col-md-8">
                                                                        <dx:ASPxDateEdit runat="server" CssClass="form-control" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="txtFecha" PickerType="Months">
                                                                            <CalendarProperties>
                                                                                <FastNavProperties InitialView="Years" MaxView="Years" />
                                                                            </CalendarProperties>
                                                                        </dx:ASPxDateEdit>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <button id="btnConsultaTracking" type="button" runat="server" class="btn btn-primary btn-sm" onserverclick="btnConsultaTracking_ServerClick">
                                                        <i class="fa fa-search" aria-hidden="true"></i>&nbsp;Consultar
                                                    </button>
                                                    <button type="submit" runat="server" class="btn btn-default btn-sm" id="btnExcel"
                                                        onserverclick="btnExcel_ServerClick">
                                                        <i class="fa fa-file-excel-o" aria-hidden="true"></i>&nbsp;Excel
                                                    </button>
                                                </div>
                                                <div class="col-md-12" style="margin-top: 15px;">
                                                    <div class="col-md-6">
                                                        <dx:BootstrapGridView ID="grdmonitorgeneral" ClientInstanceName="grid" runat="server" OnCustomSummaryCalculate="grdmonitorgeneral_CustomSummaryCalculate"
                                                            KeyFieldName="id" Width="600" AutoGenerateColumns="False" CssClasses-HeaderRow="HeadercenterText" CssClasses-FooterRow="footercenterText">

                                                            <Columns>
                                                                <dx:BootstrapGridViewTextColumn FieldName="tiempo" CssClasses-HeaderCell="HeadercenterText" Width="60px" CssClasses-DataCell="gridcenterText" Caption="En Tiempo" />
                                                                <dx:BootstrapGridViewTextColumn FieldName="ejecucion" CssClasses-HeaderCell="HeadercenterText" Width="60px" CssClasses-DataCell="gridcenterText" Caption="En Ejecución" />
                                                                <dx:BootstrapGridViewTextColumn FieldName="terminadas" CssClasses-HeaderCell="HeadercenterText" Width="60px" CssClasses-DataCell="gridcenterText" Caption="Terminadas" />
                                                                <dx:BootstrapGridViewTextColumn FieldName="bajas" CssClasses-HeaderCell="HeadercenterText" Width="60px" CssClasses-DataCell="gridcenterText" Caption="Bajas" />
                                                                <dx:BootstrapGridViewTextColumn FieldName="Reprogramadas" CssClasses-HeaderCell="HeadercenterText" Width="60px" CssClasses-DataCell="gridcenterText" Caption="Reprogramadas" />
                                                                <dx:BootstrapGridViewTextColumn FieldName="vencidas" CssClasses-HeaderCell="HeadercenterText" Width="60px" CssClasses-DataCell="gridcenterText" Caption="Vencidas" />
                                                                <dx:BootstrapGridViewTextColumn FieldName="total" CssClasses-HeaderCell="HeadercenterText" Width="60px" CssClasses-DataCell="gridcenterText" Caption="Total" />
                                                            </Columns>
                                                            <Settings ShowGroupPanel="false" ShowFooter="true" ShowFilterRow="false" ShowFilterRowMenu="false" />
                                                            <TotalSummary>
                                                                <dx:ASPxSummaryItem FieldName="tiempo" ShowInColumn="tiempo" Tag="tiempo" SummaryType="Custom" DisplayFormat="P0" />
                                                                <dx:ASPxSummaryItem FieldName="ejecucion" ShowInColumn="ejecucion" Tag="ejecucion" SummaryType="Custom" DisplayFormat="P0" />
                                                                <dx:ASPxSummaryItem FieldName="terminadas" ShowInColumn="terminadas" Tag="terminadas" SummaryType="Custom" DisplayFormat="P0" />
                                                                <dx:ASPxSummaryItem FieldName="bajas" ShowInColumn="bajas" Tag="bajas" SummaryType="Custom" DisplayFormat="P0" />
                                                                <dx:ASPxSummaryItem FieldName="Reprogramadas" ShowInColumn="Reprogramadas" Tag="Reprogramadas" SummaryType="Custom" DisplayFormat="P0" />
                                                                <dx:ASPxSummaryItem FieldName="vencidas" ShowInColumn="vencidas" Tag="vencidas" SummaryType="Custom" DisplayFormat="P0" />
                                                                <dx:ASPxSummaryItem FieldName="total" ShowInColumn="total" Tag="total" SummaryType="Custom" DisplayFormat="P0" />
                                                            </TotalSummary>
                                                        </dx:BootstrapGridView>
                                                    </div>
                                                </div>
                                                <div class="col-md-12" style="margin-top: 5px;">
                                                    <dx:BootstrapGridView ID="grdTracking" ClientInstanceName="grid" runat="server"
                                                        KeyFieldName="id" Width="100%" AutoGenerateColumns="False">
                                                        <Columns>
                                                            <dx:BootstrapGridViewTextColumn FieldName="ID" CssClasses-HeaderCell="centerText" Caption="id" Visible="false" />
                                                            <dx:BootstrapGridViewTextColumn FieldName="Id_Usu" CssClasses-HeaderCell="centerText" Width="20px" Caption="Núm. Usuario" Visible="false" />
                                                            <dx:BootstrapGridViewTextColumn FieldName="estatus" CssClasses-HeaderCell="centerText" Caption="Estatus" />
                                                            <dx:BootstrapGridViewTextColumn FieldName="TipoUsuario" VisibleIndex="1" CssClasses-HeaderCell="centerText" Caption="Rol" Visible="false" />
                                                            <dx:BootstrapGridViewTextColumn FieldName="usuario" CssClasses-HeaderCell="centerText" Caption="Usuario" />
                                                            <dx:BootstrapGridViewTextColumn FieldName="id_cte" CssClasses-HeaderCell="centerText" Width="20px" Caption="Núm. Cliente" />
                                                            <dx:BootstrapGridViewTextColumn FieldName="nombre" CssClasses-HeaderCell="centerText" Caption="Nombre" />
                                                            <dx:BootstrapGridViewTextColumn FieldName="Actividad" CssClasses-HeaderCell="centerText" Width="20px" Caption="Actividad" />
                                                            <dx:BootstrapGridViewTextColumn FieldName="TipoActividad" CssClasses-HeaderCell="centerText" Width="20px" Caption="Tipo de Actividad" />
                                                            <dx:BootstrapGridViewTextColumn FieldName="FechaInicio" CssClasses-HeaderCell="centerText" Caption="Fecha Inicial Act. Programadas" />
                                                            <dx:BootstrapGridViewTextColumn FieldName="fechaFinal" CssClasses-HeaderCell="centerText" Caption="Fecha Final Act. Programadas" />

                                                            <dx:BootstrapGridViewTextColumn FieldName="fechaEliminacion" CssClasses-HeaderCell="centerText" Caption="Act. Cancelada" />
                                                            <dx:BootstrapGridViewTextColumn FieldName="Comentarios" CssClasses-HeaderCell="centerText" Caption="Motivo de Cancelación" />

                                                            <dx:BootstrapGridViewTextColumn FieldName="fechaModificacion" CssClasses-HeaderCell="centerText" Caption="Fecha Inicial Act. Reprogramada" />
                                                            <dx:BootstrapGridViewTextColumn FieldName="fechaModificacionFinal" CssClasses-HeaderCell="centerText" Caption="Fecha Final Act. Reprogramada" />
                                                            <dx:BootstrapGridViewTextColumn FieldName="inicioEjecucion" CssClasses-HeaderCell="centerText" Caption="Fecha Inicial Act. Completadas" Visible="false" />
                                                            <dx:BootstrapGridViewTextColumn FieldName="finalEjecucion" CssClasses-HeaderCell="centerText" Caption="Fecha Final  Act. Completadas" Visible="false" />
                                                            <dx:BootstrapGridViewTextColumn FieldName="TiempoMinutos" CssClasses-HeaderCell="centerText" Caption="Tiempo Invertido (Min)" />
                                                        </Columns>
                                                        <Settings ShowGroupPanel="false" ShowFooter="True" ShowFilterRow="false" ShowFilterRowMenu="true" />
                                                    </dx:BootstrapGridView>
                                                </div>
                                                <div class="col-md-12" style="margin-top: 5px;">
                                                    <asp:Label runat="server" ID="lblact" ForeColor="OrangeRed" Text="Act = Actividad(es)"></asp:Label>
                                                </div>
                                                <div class="col-md-12" style="margin-top: 5px;">
                                                    <asp:Label runat="server" ID="Label6" ForeColor="OrangeRed" Text="Actividades Reprogramadas = Actividad(es) que Actualiza la Fecha de su Programación Original"></asp:Label>
                                                </div>
                                                <div class="col-md-12" style="margin-top: 5px;">
                                                    <asp:Label runat="server" ID="Label7" ForeColor="OrangeRed" Text="Actividades Canceladas = Actividad(es) Canceladas/Baja de la Programación de Actividades"></asp:Label>
                                                </div>
                                                 <div class="col-md-12" style="margin-top: 5px;">
                                                     </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnreporteCaptacionexcel" />
                                    <asp:PostBackTrigger ControlID="btnPerdidaClienteexcel" />
                                    <asp:PostBackTrigger ControlID="btnExcel" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:BootstrapTabPage>
                <dx:BootstrapTabPage Text="Reporte Asesor">
                    <ContentCollection>
                        <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                            <asp:UpdatePanel runat="server" ID="UPAsesor">
                                <ContentTemplate>
                                    <%-- Registro Servicio de valor --%>
                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <div class="panel panel-success">
                                            <div class="panel-heading">
                                                <h3 class="panel-title">Reporte de Servicio de valor</h3>
                                                <span class="pull-right clickable panel-collapsed" style="margin-top: -20px !important;"><i class="glyphicon glyphicon-chevron-up"></i>
                                                </span>
                                            </div>
                                            <div class="panel-body">
                                                <div class="col-md-12" style="margin-top: 1%;">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="Label3" runat="server" Text="Sucursal" />
                                                            </div>
                                                            <div class="col-md-8">
                                                                <dx:BootstrapComboBox ID="dllSucursalSerivioValor" runat="server">
                                                                </dx:BootstrapComboBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-12" style="margin-top: 1%;">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <div class="col-md-4">
                                                                Usuario
                                                            </div>
                                                            <div class="col-md-8">
                                                                <dx:BootstrapComboBox ID="DllUsuarioServicioValor" runat="server" CallbackPageSize="25" DropDownStyle="DropDown">
                                                                </dx:BootstrapComboBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-12" style="margin-top: 1%;">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <div class="col-md-4">
                                                                Fecha Inicial
                                                            </div>
                                                            <div class="col-md-8">
                                                                <dx:BootstrapDateEdit runat="server" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="TxtFechaInicialServicioValor" EditFormat="Date">
                                                                </dx:BootstrapDateEdit>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <div class="col-md-4">
                                                                Fecha Final
                                                            </div>
                                                            <div class="col-md-8">
                                                                <dx:BootstrapDateEdit runat="server" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="TxtFechaFinalServicioValor" EditFormat="Date">
                                                                </dx:BootstrapDateEdit>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="col-md-12" style="margin-top: 5px;">
                                                    <div class="col-md-3">
                                                        <button id="btnServicioValor" type="button" runat="server" class="btn btn-primary btn-sm" onserverclick="btnServicioValor_ServerClick">
                                                            <i class="fa fa-search" aria-hidden="true"></i>&nbsp;Consultar
                                                        </button>
                                                    </div>
                                                </div>
                                                <div id="divServicioValor" runat="server" visible="false">
                                                    <div class="col-md-2">
                                                    </div>
                                                    <div class="col-md-8" style="height: 350px;">
                                                        <canvas id="myChartAgendaServicioValor" width="200" height="80"></canvas>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:BootstrapTabPage>
                <dx:BootstrapTabPage Text="Reporte RSC">
                    <ContentCollection>
                        <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                            <asp:UpdatePanel runat="server" ID="UPRSC">
                                <ContentTemplate>
                                    <%-- Registro Cumplimiento VI --%>
                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <div class="panel panel-success">
                                            <div class="panel-heading">
                                                <h3 class="panel-title">Reporte de Cumplimiento VI</h3>
                                                <span class="pull-right clickable panel-collapsed" style="margin-top: -20px !important;"><i class="glyphicon glyphicon-chevron-up"></i>
                                                </span>
                                            </div>
                                            <div class="panel-body">
                                                <div class="col-md-12" style="margin-top: 1%;">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="Label5" runat="server" Text="Sucursal" />
                                                            </div>
                                                            <div class="col-md-8">
                                                                <dx:BootstrapComboBox ID="DllSucuralCumplVI" runat="server">
                                                                </dx:BootstrapComboBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <div class="col-md-4">
                                                                Rol
                                                            </div>
                                                            <div class="col-md-8">
                                                                <dx:BootstrapComboBox ID="DllRolCumplVI" runat="server" CallbackPageSize="25" DropDownStyle="DropDown" OnSelectedIndexChanged="DllRolCumplVI_SelectedIndexChanged" AutoPostBack="true">
                                                                </dx:BootstrapComboBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-12" style="margin-top: 1%;">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <div class="col-md-4">
                                                                Usuario
                                                            </div>
                                                            <div class="col-md-8">
                                                                <dx:BootstrapComboBox ID="DllUsuarioCumplVi" runat="server" CallbackPageSize="25" DropDownStyle="DropDown">
                                                                </dx:BootstrapComboBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-12" style="margin-top: 1%;">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <div class="col-md-4">
                                                                Fecha
                                                            </div>
                                                            <div class="col-md-8">
                                                                <dx:ASPxDateEdit runat="server" CssClass="form-control" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="txtFechaCumplVi" PickerType="Months">
                                                                    <CalendarProperties>
                                                                        <FastNavProperties InitialView="Years" MaxView="Years" />
                                                                    </CalendarProperties>
                                                                </dx:ASPxDateEdit>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-12" style="margin-top: 5px;">
                                                    <div class="col-md-3">
                                                        <button id="BtnCumplVI" type="button" runat="server" class="btn btn-primary btn-sm" onserverclick="BtnCumplVI_ServerClick">
                                                            <i class="fa fa-search" aria-hidden="true"></i>&nbsp;Consultar
                                                        </button>
                                                        <button type="submit" class="btn btn-primary btn-sm" id="btnReportePedido" onserverclick="btnReportePedido_ServerClick"
                                                            runat="server">
                                                            <i aria-hidden="true"></i>&nbsp;Reporte Cumplimiento VI
                                                        </button>
                                                    </div>
                                                </div>
                                                <div class="col-md-12" runat="server" id="Div2">
                                                    <div class="col-md-3" runat="server" id="Div1" style="margin-top: 5px;">
                                                        <dx:BootstrapGridView ID="GrdCuentaVI" ClientInstanceName="grid" runat="server" KeyFieldName="id_cte"
                                                            Width="100%" AutoGenerateColumns="False">
                                                            <Settings ShowHeaderFilterButton="true" />
                                                            <Columns>
                                                                <dx:BootstrapGridViewDataColumn Width="150px" FieldName="descripcionTipoCuenta" Caption="Tipo de Cuenta" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                </dx:BootstrapGridViewDataColumn>
                                                                <dx:BootstrapGridViewDataColumn Width="30px" FieldName="CantidadTipoCuenta" Caption="Cantidad" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                </dx:BootstrapGridViewDataColumn>
                                                            </Columns>
                                                        </dx:BootstrapGridView>
                                                    </div>
                                                    <div class="col-md-9" id="RepCumpl" runat="server" visible="false" style="height: 450px;">
                                                        <canvas id="myChartCumplVi" width="200" height="80"></canvas>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:BootstrapTabPage>
                <dx:BootstrapTabPage Text="Reporte Ventas Servicio">
                    <ContentCollection>
                        <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                            <asp:UpdatePanel runat="server" ID="UPServicios">
                                <ContentTemplate>
                                    <%-- Registro Reporte de ventas--%>
                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <div class="panel panel-success">
                                            <div class="panel-heading">
                                                <h3 class="panel-title">Reporte de Ventas</h3>
                                                <span class="pull-right clickable panel-collapsed" style="margin-top: -20px !important;"><i class="glyphicon glyphicon-chevron-up"></i>
                                                </span>
                                            </div>
                                            <div class="panel-body">
                                                <div class="col-md-12" style="margin-top: 5px;">
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="Label8" runat="server" Text="Ordenar por:"></asp:Label>
                                                            </div>
                                                            <div class="col-md-8">
                                                                <asp:RadioButtonList runat="Server" CssClass="form-control3" ID="RBVenta" RepeatDirection="Vertical">
                                                                    <asp:ListItem runat="Server" Value="4" Text="ASC/RSC" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem runat="Server" Value="1" Text="Cliente"></asp:ListItem>
                                                                    <%--<asp:ListItem runat="Server" Value="2" Text="Producto"></asp:ListItem>--%>
                                                                    <asp:ListItem runat="Server" Value="3" Text="Territorio de Servicio"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">

                                                        <div class="col-md-12" runat="server" id="rolventa">
                                                            <div class="form-group">
                                                                <div class="col-md-4">
                                                                    Rol
                                                                </div>
                                                                <div class="col-md-8">
                                                                    <dx:BootstrapComboBox ID="dllrolVenta" runat="server" CallbackPageSize="25" DropDownStyle="DropDown">
                                                                    </dx:BootstrapComboBox>
                                                                </div>
                                                            </div>
                                                        </div>


                                                        <div class="col-md-12" style="margin-top: 5px;">
                                                            <div class="form-group">
                                                                <div class="col-md-4">
                                                                    <asp:Label ID="Label9" runat="server" Text="Año"></asp:Label>
                                                                </div>
                                                                <div class="col-md-8">
                                                                    <dx:ASPxDateEdit runat="server" CssClass="form-control" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="cmbAnio" PickerType="Years">
                                                                        <CalendarProperties>
                                                                            <FastNavProperties InitialView="Years" MaxView="Years" />
                                                                        </CalendarProperties>
                                                                    </dx:ASPxDateEdit>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-12" style="margin-top: 5px;">
                                                    <div class="col-md-3">
                                                        <button id="btnReporteVentas" type="button" runat="server" class="btn btn-primary btn-sm" onserverclick="btnReporteVentas_ServerClick">
                                                            <i class="fa fa-search" aria-hidden="true"></i>&nbsp;Consultar
                                                        </button>
                                                        <button id="BtnImprimirReporteVentas" type="button" runat="server" class="btn btn-primary btn-sm" onserverclick="BtnImprimirReporteVentas_ServerClick">
                                                            Imprimir
                                                        </button>
                                                    </div>
                                                </div>
                                                <%--cliente--%>
                                                <div class="col-md-12" runat="server" enableviewstate="false" id="GridCliente" style="display: none; margin-top: 5px; overflow-x: auto;">
                                                    <dx:BootstrapGridView ID="GrdCliente" ClientInstanceName="grid" runat="server" KeyFieldName="id_cte"
                                                        Width="100%" AutoGenerateColumns="False">
                                                        <SettingsEditing Mode="Batch" />
                                                        <Settings ShowHeaderFilterButton="true" ShowGroupPanel="True" ShowFooter="true" />
                                                        <Columns>
                                                            <dx:BootstrapGridViewDataColumn Width="30px" ExportWidth="100" FieldName="id_cte" Caption="Núm." CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                                            </dx:BootstrapGridViewDataColumn>
                                                            <dx:BootstrapGridViewDataColumn Width="150px" ExportWidth="130" FieldName="nombre_Comercial" Caption="Cliente" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                            </dx:BootstrapGridViewDataColumn>
                                                            <dx:BootstrapGridViewDataColumn Width="30px" ExportWidth="100" FieldName="id_ter" Caption="Núm. Territorio" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                            </dx:BootstrapGridViewDataColumn>
                                                            <dx:BootstrapGridViewDataColumn Width="150px" ExportWidth="130" FieldName="nombre_terr" Caption="Territorio" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                            </dx:BootstrapGridViewDataColumn>
                                                            <dx:BootstrapGridViewDataColumn Width="30px" ExportWidth="100" FieldName="id_rik" Caption="Núm. Asc/Rsc" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                            </dx:BootstrapGridViewDataColumn>
                                                            <dx:BootstrapGridViewDataColumn Width="150px" ExportWidth="130" FieldName="nombre_rik" Caption="Nombre Asc/Rsc" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                            </dx:BootstrapGridViewDataColumn>
                                                            <dx:BootstrapGridViewDataColumn Width="150px" ExportWidth="130" FieldName="tipoUsuario" Caption="Rol" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                            </dx:BootstrapGridViewDataColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes1" Caption="Enero" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes2" Caption="Febrero" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes3" Caption="Marzo" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes4" Caption="Abril" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes5" Caption="Mayo" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes6" Caption="Junio" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes7" Caption="Julio" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes8" Caption="Agosto" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes9" Caption="Septiembre" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes10" Caption="Octubre" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes11" Caption="Noviembre" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes12" Caption="Dicembre" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="total" Caption="Total" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                        </Columns>
                                                        <TotalSummary>
                                                            <dx:ASPxSummaryItem FieldName="tipoUsuario" ShowInColumn="tipoUsuario" SummaryType="count" DisplayFormat="Total:" />
                                                            <dx:ASPxSummaryItem FieldName="mes1" DisplayFormat="C" ShowInColumn="mes1" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes11" DisplayFormat="C" ShowInColumn="mes11" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes12" DisplayFormat="C" ShowInColumn="mes12" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes2" DisplayFormat="C" ShowInColumn="mes2" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes3" DisplayFormat="C" ShowInColumn="mes3" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes4" DisplayFormat="C" ShowInColumn="mes4" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes5" DisplayFormat="C" ShowInColumn="mes5" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes6" DisplayFormat="C" ShowInColumn="mes6" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes7" DisplayFormat="C" ShowInColumn="mes7" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes8" DisplayFormat="C" ShowInColumn="mes8" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes9" DisplayFormat="C" ShowInColumn="mes9" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes10" DisplayFormat="C" ShowInColumn="mes10" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="total" DisplayFormat="C" ShowInColumn="total" SummaryType="Sum" />
                                                        </TotalSummary>
                                                    </dx:BootstrapGridView>
                                                </div>
                                                <%--Producto--%>
                                                <div class="col-md-12" runat="server" enableviewstate="false" id="GridProducto" style="display: none; margin-top: 5px;">
                                                    <dx:BootstrapGridView ID="grdProducto" ClientInstanceName="grid" runat="server" KeyFieldName="id_cte"
                                                        Width="100%" AutoGenerateColumns="False">
                                                        <SettingsEditing Mode="Batch" />
                                                        <Settings ShowHeaderFilterButton="true" ShowGroupPanel="True" ShowFooter="true" />

                                                        <Columns>
                                                            <dx:BootstrapGridViewDataColumn Width="30px" ExportWidth="100" FieldName="id_cte" Caption="Núm." CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                            </dx:BootstrapGridViewDataColumn>
                                                            <dx:BootstrapGridViewDataColumn Width="150px" ExportWidth="130" FieldName="nombre_Comercial" Caption="Producto" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                            </dx:BootstrapGridViewDataColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes1" Caption="Enero" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes2" Caption="Febrero" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes3" Caption="Marzo" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes4" Caption="Abril" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes5" Caption="Mayo" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes6" Caption="Junio" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes7" Caption="Julio" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes8" Caption="Agosto" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes9" Caption="Septiembre" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes10" Caption="Octubre" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes11" Caption="Noviembre" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes12" Caption="Dicembre" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="total" Caption="Total" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                        </Columns>
                                                        <TotalSummary>
                                                            <dx:ASPxSummaryItem FieldName="nombre_Comercial" ShowInColumn="nombre_Comercial" SummaryType="None" DisplayFormat="Total:" />
                                                            <dx:ASPxSummaryItem FieldName="mes1" DisplayFormat="C" ShowInColumn="mes1" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes11" DisplayFormat="C" ShowInColumn="mes11" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes12" DisplayFormat="C" ShowInColumn="mes12" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes2" DisplayFormat="C" ShowInColumn="mes2" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes3" DisplayFormat="C" ShowInColumn="mes3" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes4" DisplayFormat="C" ShowInColumn="mes4" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes5" DisplayFormat="C" ShowInColumn="mes5" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes6" DisplayFormat="C" ShowInColumn="mes6" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes7" DisplayFormat="C" ShowInColumn="mes7" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes8" DisplayFormat="C" ShowInColumn="mes8" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes9" DisplayFormat="C" ShowInColumn="mes9" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes10" DisplayFormat="C" ShowInColumn="mes10" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="total" DisplayFormat="C" ShowInColumn="total" SummaryType="Sum" />
                                                        </TotalSummary>
                                                    </dx:BootstrapGridView>
                                                </div>
                                                <%--Territorio--%>
                                                <div class="col-md-12" runat="server" enableviewstate="false" id="GridTerritorio" style="display: none; margin-top: 5px; overflow-x: auto;">
                                                    <dx:BootstrapGridView ID="GrdTerritorio" ClientInstanceName="GrdTerritorio" runat="server" KeyFieldName="id_cte"
                                                        Width="100%" AutoGenerateColumns="False">
                                                        <SettingsEditing Mode="Batch" />
                                                        <Settings ShowHeaderFilterButton="true" ShowGroupPanel="True" ShowFooter="true" />

                                                        <Columns>
                                                            <dx:BootstrapGridViewDataColumn Width="30px" ExportWidth="100" FieldName="id_cte" Caption="Núm." CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                            </dx:BootstrapGridViewDataColumn>
                                                            <dx:BootstrapGridViewDataColumn Width="150px" ExportWidth="130" FieldName="nombre_Comercial" Caption="Territorio" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                            </dx:BootstrapGridViewDataColumn>
                                                            <dx:BootstrapGridViewDataColumn Width="30px" ExportWidth="100" FieldName="id_rik" Caption="Núm. Asc/Rsc" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                            </dx:BootstrapGridViewDataColumn>
                                                            <dx:BootstrapGridViewDataColumn Width="150px" ExportWidth="130" FieldName="nombre_rik" Caption="Nombre Asc/Rsc" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                            </dx:BootstrapGridViewDataColumn>
                                                            <dx:BootstrapGridViewDataColumn Width="150px" ExportWidth="100" FieldName="tipoUsuario" Caption="Rol" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                            </dx:BootstrapGridViewDataColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes1" Caption="Enero" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes2" Caption="Febrero" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes3" Caption="Marzo" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes4" Caption="Abril" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes5" Caption="Mayo" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes6" Caption="Junio" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes7" Caption="Julio" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes8" Caption="Agosto" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes9" Caption="Septiembre" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes10" Caption="Octubre" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes11" Caption="Noviembre" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes12" Caption="Dicembre" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="total" Caption="Total" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                        </Columns>
                                                        <TotalSummary>
                                                            <dx:ASPxSummaryItem FieldName="tipoUsuario" ShowInColumn="tipoUsuario" SummaryType="count" DisplayFormat="Total:" />
                                                            <dx:ASPxSummaryItem FieldName="mes1" DisplayFormat="C" ShowInColumn="mes1" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes11" DisplayFormat="C" ShowInColumn="mes11" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes12" DisplayFormat="C" ShowInColumn="mes12" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes2" DisplayFormat="C" ShowInColumn="mes2" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes3" DisplayFormat="C" ShowInColumn="mes3" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes4" DisplayFormat="C" ShowInColumn="mes4" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes5" DisplayFormat="C" ShowInColumn="mes5" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes6" DisplayFormat="C" ShowInColumn="mes6" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes7" DisplayFormat="C" ShowInColumn="mes7" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes8" DisplayFormat="C" ShowInColumn="mes8" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes9" DisplayFormat="C" ShowInColumn="mes9" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes10" DisplayFormat="C" ShowInColumn="mes10" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="total" DisplayFormat="C" ShowInColumn="total" SummaryType="Sum" />
                                                        </TotalSummary>
                                                    </dx:BootstrapGridView>
                                                </div>
                                                <%--RSC/Asc--%>
                                                <div class="col-md-12" runat="server" enableviewstate="false" id="GridAscRsc" style="display: none; margin-top: 5px; overflow-x: auto;">
                                                    <dx:BootstrapGridView ID="GrdAscRsc" ClientInstanceName="GrdTerritorio" runat="server" KeyFieldName="id_cte"
                                                        Width="100%" AutoGenerateColumns="False">
                                                        <SettingsEditing Mode="Batch" />
                                                        <Settings ShowHeaderFilterButton="true" ShowGroupPanel="True" ShowFooter="true" />
                                                        <Columns>
                                                            <dx:BootstrapGridViewDataColumn Width="30px" ExportWidth="100" FieldName="id_cte" Caption="Núm. Asc/Rsc" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                            </dx:BootstrapGridViewDataColumn>
                                                            <dx:BootstrapGridViewDataColumn Width="150px" ExportWidth="130" FieldName="nombre_Comercial" Caption="Nombre Asc/Rsc" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                            </dx:BootstrapGridViewDataColumn>
                                                            <dx:BootstrapGridViewDataColumn Width="150px" ExportWidth="100" FieldName="tipoUsuario" Caption="Rol" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                            </dx:BootstrapGridViewDataColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes1" Caption="Enero" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes2" Caption="Febrero" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes3" Caption="Marzo" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes4" Caption="Abril" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes5" Caption="Mayo" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes6" Caption="Junio" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes7" Caption="Julio" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes8" Caption="Agosto" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes9" Caption="Septiembre" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes10" Caption="Octubre" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes11" Caption="Noviembre" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="mes12" Caption="Dicembre" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                            <dx:BootstrapGridViewTextColumn Width="30px" ExportWidth="100" FieldName="total" Caption="Total" CssClasses-DataCell="LeftText" CssClasses-FooterCell="LeftText" CssClasses-HeaderCell="centerText">
                                                                <PropertiesTextEdit DisplayFormatString="C" />
                                                            </dx:BootstrapGridViewTextColumn>
                                                        </Columns>
                                                        <TotalSummary>
                                                            <dx:ASPxSummaryItem FieldName="tipoUsuario" ShowInColumn="tipoUsuario" SummaryType="count" DisplayFormat="Total:" />
                                                            <dx:ASPxSummaryItem FieldName="mes1" DisplayFormat="C" ShowInColumn="mes1" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes11" DisplayFormat="C" ShowInColumn="mes11" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes12" DisplayFormat="C" ShowInColumn="mes12" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes2" DisplayFormat="C" ShowInColumn="mes2" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes3" DisplayFormat="C" ShowInColumn="mes3" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes4" DisplayFormat="C" ShowInColumn="mes4" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes5" DisplayFormat="C" ShowInColumn="mes5" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes6" DisplayFormat="C" ShowInColumn="mes6" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes7" DisplayFormat="C" ShowInColumn="mes7" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes8" DisplayFormat="C" ShowInColumn="mes8" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes9" DisplayFormat="C" ShowInColumn="mes9" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="mes10" DisplayFormat="C" ShowInColumn="mes10" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem FieldName="total" DisplayFormat="C" ShowInColumn="total" SummaryType="Sum" />
                                                        </TotalSummary>
                                                    </dx:BootstrapGridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="BtnImprimirReporteVentas" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:BootstrapTabPage>
            </TabPages>
        </dx:BootstrapPageControl>

        <div class="modal" id="Mapas" data-toggle="modal" style="height: 600px !important; width: 920px !important; margin-left: auto; margin-right: auto;"
            role="dialog" style="z-index: 2220!important">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <div class="col-md-10">
                        <h4 id="h1">Mapa
                        </h4>
                    </div>
                </div>
                <div>
                    <div class="embed-responsive embed-responsive-16by9 ">
                        <iframe class="embed-responsive-item" id="frameMapa" runat="server" src=""></iframe>
                    </div>
                </div>
            </div>
        </div>
        <!-- Cosnulta de Actividad - Calendario-->
        <div id="modaledicion" class="modal" data-toggle="modal" style="z-index: 3000 !important;"
            role="dialog">
            <div class="modal-dialog" role="document" style="height: 140px !important; width: 45%;">
                <div class="modal-content">
                    <div class="modal-header" style="color: #F9F9F9 !important; background-color: #59b2f1 !important;">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        Cumplimiento V.i. Cliente 
                    </div>
                    <div class="modal-body" style="padding: 10px !important;" id="Div10">
                        <div class="embed-responsive embed-responsive-16by9 z-depth-1-half" style="padding-bottom: 33% !Important;">
                            <iframe class="embed-responsive-item" id="iFrameActividad" runat="server" src=""></iframe>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <script type="text/javascript">
        function ReporteAgenda() {
            var COLOR_Analisis = '#abebc6';
            var COLOR_Promocion = '#58d68d';
            var COLOR_Negociacion = '#f7CD6F';
            var COLOR_Cierre = '#85c1e9';
            $('#<%= UpdateProgress1.ClientID %>').css("display", "block");
            $.ajax({
                type: "POST",
                url: "MonitorActividades.aspx/MonitorAgenda",
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
                        if (id == 5) {
                            var myChar8;
                            var ctx = document.getElementById('myChartAgenda');
                            if (window.myChart8 != undefined) {
                                window.myChart8.destroy();
                            }
                            console.log(data);
                            var datos = data.datosProgramadas;
                            var datosstrArr = datos.split(",")
                            var datosArr = [];
                            datosstrArr.forEach(function (data, index, arr) {
                                datosArr.push(+data);
                            });
                            var datos2 = data.datosRealizadas;
                            var datosstrArr2 = datos2.split(",")
                            var datosArr2 = [];
                            datosstrArr2.forEach(function (data, index, arr) {
                                datosArr2.push(+data);
                            });
                            var datos3 = data.datosRecalendarizadas;
                            var datosstrArr3 = datos3.split(",")
                            var datosArr3 = [];
                            datosstrArr3.forEach(function (data, index, arr) {
                                datosArr3.push(+data);
                            });
                            var datos4 = data.datosCanceladas;
                            var datosstrArr4 = datos4.split(",")
                            var datosArr4 = [];
                            datosstrArr4.forEach(function (data, index, arr) {
                                datosArr4.push(+data);
                            });
                            console.log(data.datosTotal)
                            document.getElementById('<%=total.ClientID%>').innerHTML = 'Total de Actividades: ' + data.datosTotal;
                            var tituloArr = [];
                            tituloArr.push('LLAMADAS/VISITAS');
                            var lineChartData = {
                                labels: tituloArr,
                                datasets: [{
                                    label: 'Programadas',
                                    backgroundColor: COLOR_Analisis,
                                    data: datosArr,
                                }, {
                                    label: 'Realizadas',
                                    backgroundColor: COLOR_Promocion,
                                    data: datosArr2,
                                }, {
                                    label: 'Recalendarizadas',
                                    backgroundColor: COLOR_Negociacion,
                                    data: datosArr3,
                                }, {
                                    label: 'Canceladas',
                                    backgroundColor: COLOR_Cierre,
                                    data: datosArr4,
                                }]
                            };
                            window.myChart8 = new Chart(ctx, {
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
                                        position: 'left',
                                        labels: {
                                            padding: 10,
                                            fontSize: 9,
                                        }
                                    },
                                    scaleshowvalue: true,
                                    hoverMode: 'index',
                                    stacked: false,
                                    title: {
                                        display: false,
                                        text: 'Gestión Llamadas/Visitas',
                                    },
                                    scales: {
                                        yAxes: [{
                                            type: "linear",
                                            display: true,
                                            position: "left",
                                            id: "y-axis-1",
                                            ticks: {
                                                beginAtZero: true,
                                                steps: 1,
                                                stepValue: 1,
                                                stepSize: 10
                                            }
                                        }],
                                        xAxes: [{
                                            stacked: false,
                                            beginAtZero: true,
                                            barPercentage: 0.4,
                                            ticks: {
                                                stepSize: 1,
                                                min: 0,
                                                autoSkip: false,
                                                beginAtZero: true
                                            }
                                        }],
                                    },
                                    plugins: {
                                        colorschemes: {
                                            scheme: 'brewer.SetTwo6',
                                        },
                                        labels: {
                                            render: function (args) {
                                                //return '$' + args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                                value = '';
                                                return value;
                                            },
                                            fontColor: '#000',
                                            display: false,
                                        },
                                        datalabels: {
                                            display: false,
                                        }
                                    },
                                    tooltips: {
                                        callbacks: {
                                            label: function (tooltipItem, data) {
                                                return tooltipItem.yLabel.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
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
        function ReporteFacturacion() {
            $('#<%= UpdateProgress1.ClientID %>').css("display", "block");
            $.ajax({
                type: "POST",
                url: "MonitorActividades.aspx/ReporteFacturacion",
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                    document.location.href = 'login.aspx';
                },
                success: function (response) {
                    if (response != null && response.d != null) {
                        var data = response.d;
                        data = $.parseJSON(data);
                        var id = data.id;
                        if (id == -1) {
                            modalMensaje(errorThrown);
                        }
                        if (id == 0) {
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            document.location.href = 'login.aspx';
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
                            var datos2 = data.datos;
                            var datosstrArr2 = datos2.split(",")
                            var datosArr2 = [];
                            datosstrArr2.forEach(function (data, index, arr) {
                                datosArr2.push(+data);
                                coloR.push(dynamicColors());
                            });
                            var ctx2 = document.getElementById('myChartCliente');
                            window.myChart3 = new Chart(ctx2, {
                                type: 'line',
                                data: {
                                    labels: tituloArr,
                                    datasets: [{
                                        data: datosArr2,
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
                                        position: 'left',
                                        labels: {
                                            padding: 10,
                                            fontSize: 9,
                                        }
                                    },
                                    scaleshowvalue: true,
                                    hoverMode: 'index',
                                    stacked: false,
                                    title: {
                                        display: true,
                                        text: 'Perdida de Clientes',
                                    },
                                    scales: {
                                        yAxes: [{
                                            type: "linear",
                                            display: true,
                                            position: "left",
                                            id: "y-axis-1",
                                            ticks: {
                                                beginAtZero: true,
                                                steps: 1,
                                                stepValue: 1,
                                                stepSize: 5
                                            }
                                        }],
                                        xAxes: [{
                                            stacked: false,
                                            beginAtZero: true,
                                            barPercentage: 0.4,
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
                                                return args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                            },
                                            fontColor: '#000',
                                        }
                                    },
                                    tooltips: {
                                        callbacks: {
                                            label: function (tooltipItem, data) {
                                                return tooltipItem.yLabel.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
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
        function ReporteCaptacion() {
            $('#<%= UpdateProgress1.ClientID %>').css("display", "block");
            $.ajax({
                type: "POST",
                url: "MonitorActividades.aspx/ReporteCaptacion",
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                    document.location.href = 'login.aspx';
                },
                success: function (response) {
                    if (response != null && response.d != null) {
                        var data = response.d;
                        data = $.parseJSON(data);
                        var id = data.id;
                        if (id == -1) {
                            modalMensaje(errorThrown);
                        }
                        if (id == 0) {
                            $('#<%= UpdateProgress1.ClientID %>').css("display", "none");
                            document.location.href = 'login.aspx';
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
                            var datos2 = data.datos;
                            var datosstrArr2 = datos2.split(",")
                            var datosArr2 = [];
                            datosstrArr2.forEach(function (data, index, arr) {
                                datosArr2.push(+data);
                                coloR.push(dynamicColors());
                            });
                            var ctx2 = document.getElementById('myChartPedidos');
                            window.myChart30 = new Chart(ctx2, {
                                type: 'doughnut',
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
                                        position: 'bottom',
                                        labels: {
                                            padding: 10,
                                            fontSize: 9,
                                        }
                                    },
                                    title: {
                                        display: true,
                                        text: 'Captación de Pedido',
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
                                            return args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                        },
                                        fontColor: '#000',
                                        position: 'outside',
                                        segment: true
                                    },
                                    plugins: {
                                        colorschemes: {
                                            scheme: 'brewer.Paired12',
                                        },
                                        labels: {
                                            render: () => { }
                                        },
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
        function ReporteServicioValor() {
            var COLOR_MHA = '#abebc6';
            var COLOR_químicos = '#58d68d';
            var COLOR_Auditoria = '#f7CD6F';
            var COLOR_limpieza = '#85c1e9';
            var COLOR_productos = '#28c1e9';
            $('#<%= UpdateProgress1.ClientID %>').css("display", "block");
            $.ajax({
                type: "POST",
                url: "MonitorActividades.aspx/ReporteServicioValor",
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
                        if (id == 5) {
                            const myChart4 = document.getElementById('myChartAgendaServicioValor');
                            var ctx4 = document.getElementById('myChartAgendaServicioValor');
                            if (window.myChart4 != undefined) {
                                window.myChart4.destroy();
                            }
                            var datos = data.datosCapMHA;
                            var datosstrArr = datos.split(",")
                            var datosArr = [];
                            datosstrArr.forEach(function (data, index, arr) {
                                datosArr.push(+data);
                            });
                            var datos2 = data.datosQuimico;
                            var datosstrArr2 = datos2.split(",")
                            var datosArr2 = [];
                            datosstrArr2.forEach(function (data, index, arr) {
                                datosArr2.push(+data);
                            });
                            var datos3 = data.datosAudMha;
                            var datosstrArr3 = datos3.split(",")
                            var datosArr3 = [];
                            datosstrArr3.forEach(function (data, index, arr) {
                                datosArr3.push(+data);
                            });
                            var datos4 = data.datosLimpieza;
                            var datosstrArr4 = datos4.split(",")
                            var datosArr4 = [];
                            datosstrArr4.forEach(function (data, index, arr) {
                                datosArr4.push(+data);
                            });
                            var datos5 = data.datosProductos;
                            var datosstrArr5 = datos5.split(",")
                            var datosArr5 = [];
                            datosstrArr5.forEach(function (data, index, arr) {
                                datosArr5.push(+data);
                            });
                            var titulo = data.titulo;
                            var tituloStrArr = titulo.split(",")
                            var tituloArr = [];
                            tituloStrArr.forEach(function (data, index, arr) {
                                var nombre = data;
                                tituloArr.push(nombre);
                            });
                            var lineChartData = {
                                labels: tituloArr,
                                datasets: [{
                                    label: 'Capacitación MHA',
                                    backgroundColor: COLOR_MHA,
                                    data: datosArr,
                                }, {
                                    label: 'Capacitación químicos',
                                    backgroundColor: COLOR_químicos,
                                    data: datosArr2,
                                }, {
                                    label: 'Auditoria MHA',
                                    backgroundColor: COLOR_Auditoria,
                                    data: datosArr3,
                                }, {
                                    label: 'Auditoria limpieza',
                                    backgroundColor: COLOR_limpieza,
                                    data: datosArr4,
                                }, {
                                    label: 'Asesoría productos key, limpieza y MHA',
                                    backgroundColor: COLOR_productos,
                                    data: datosArr5,
                                }]
                            };
                            window.myChart4 = new Chart(ctx4, {
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
                                        position: 'left',
                                        labels: {
                                            padding: 10,
                                            fontSize: 9,
                                        }
                                    },
                                    scaleshowvalue: true,
                                    hoverMode: 'index',
                                    stacked: false,
                                    title: {
                                        display: false,
                                        text: 'Gestión Llamadas/Visitas',
                                    },
                                    scales: {
                                        yAxes: [{
                                            stacked: true,
                                            type: "linear",
                                            display: true,
                                            position: "left",
                                            id: "y-axis-1",
                                            ticks: {
                                                beginAtZero: true,
                                                steps: 1,
                                                stepValue: 1,
                                                stepSize: 10
                                            }
                                        }],
                                        xAxes: [{
                                            stacked: true,
                                            beginAtZero: true,
                                            barPercentage: 0.4,
                                            ticks: {
                                                stepSize: 1,
                                                min: 0,
                                                autoSkip: false,
                                                beginAtZero: true
                                            }
                                        }],
                                        x: {
                                            stacked: true,
                                        },
                                        y: {
                                            stacked: true
                                        }
                                    },
                                    responsive: true,
                                    plugins: {
                                        labels: {
                                            render: function (args) {
                                                if (args.value > 0) {
                                                    return args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                                } else {
                                                    value = '';
                                                    return value;
                                                }
                                            },
                                            fontColor: '#000',
                                        },
                                    },
                                    tooltips: {
                                        callbacks: {
                                            label: function (tooltipItem, data) {
                                                if (tooltipItem.yLabel > 0) {
                                                    return tooltipItem.yLabel.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                                } else {
                                                    value = '';
                                                    return value;
                                                }
                                            }
                                        }
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
        function ReporteCumplVi() {
            var COLOR_Analisis = '#abebc6';
            var COLOR_Promocion = '#58d68d';
            var COLOR_vta = '#08d6fd';
            $('#<%= UpdateProgress1.ClientID %>').css("display", "block");
            $.ajax({
                type: "POST",
                url: "MonitorActividades.aspx/ReporteCumpliVI",
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
                        if (id == 5) {
                            var myChartVI;
                            var ctx = document.getElementById('myChartCumplVi');
                            if (window.myChartVI != undefined) {
                                window.myChartVI.destroy();
                            }
                            console.log(data);
                            var datos = data.datosVI;
                            var datosstrArr = datos.split(",")
                            var datosArr = [];
                            datosstrArr.forEach(function (data, index, arr) {
                                datosArr.push(+data);
                            });
                            var datos2 = data.DatosFacturacion;
                            var datosstrArr2 = datos2.split(",")
                            var datosArr2 = [];
                            datosstrArr2.forEach(function (data, index, arr) {
                                datosArr2.push(+data);
                            });
                            var datos3 = data.PorcCumpli;
                            var datosstrArr3 = datos3.split(",")
                            var datosArr3 = [];
                            datosstrArr3.forEach(function (data, index, arr) {
                                datosArr3.push(+data);
                            });
                            var datos4 = data.VentaMesTotal;
                            var datosstrArr4 = datos4.split(",")
                            var datosArr4 = [];
                            datosstrArr4.forEach(function (data, index, arr) {
                                datosArr4.push(+data);
                            });
                            var tituloArr = [];
                            tituloArr.push('Reporte de Cumplimiento');
                            var lineChartData = {
                                labels: tituloArr,
                                datasets: [
                                    {
                                        label: "% Cumplimiento VI en el mes",
                                        borderColor: "rgb(255, 63, 51 )",
                                        backgroundColor: "rgb(255, 63, 51 )",
                                        fill: false,
                                        data: datosArr3,
                                        type: 'line',
                                        yAxisID: "y-axis-2"
                                    }, {
                                        label: 'Venta Total del mes',
                                        backgroundColor: COLOR_vta,
                                        data: datosArr4,
                                        yAxisID: "y-axis-1",
                                    }, {
                                        label: 'Venta del mes (Acys)',
                                        backgroundColor: COLOR_Promocion,
                                        data: datosArr2,
                                        yAxisID: "y-axis-1",
                                    }, {
                                        label: 'Venta Instalada (Acys)',
                                        backgroundColor: COLOR_Analisis,
                                        data: datosArr,
                                        yAxisID: "y-axis-1",
                                    },]
                            };
                            window.myChartVI = new Chart(ctx, {
                                type: 'bar',
                                data: lineChartData,
                                options: {
                                    responsive: true,
                                    exportEnabled: true,
                                    display: false,
                                    labels: {
                                        padding: 10,
                                        fontSize: 10,
                                        display: false,
                                    },
                                    legend: {
                                        display: true,
                                        position: 'left',
                                        labels: {
                                            padding: 10,
                                            fontSize: 9,
                                        }
                                    },
                                    scaleshowvalue: true,
                                    hoverMode: 'index',
                                    stacked: false,
                                    title: {
                                        display: false,
                                        text: 'VI vs Facturacion',
                                    },
                                    scales: {
                                        yAxes: [{
                                            type: "linear",
                                            display: true,
                                            position: "right",
                                            id: "y-axis-2",
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
                                            id: "y-axis-1",
                                            ticks: {
                                                beginAtZero: true,
                                                callback: function (value, index, ticks) {
                                                    return '$' + value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                                }
                                            }
                                        }],
                                        xAxes: [{
                                            stacked: false,
                                            beginAtZero: true,
                                            barPercentage: 0.4,
                                            ticks: {
                                                stepSize: 1,
                                                min: 0,
                                                autoSkip: false,
                                                beginAtZero: true
                                            },
                                            gridLines: {
                                                drawOnChartArea: true
                                            }
                                        }],
                                    },
                                    plugins: {
                                        colorschemes: {
                                            scheme: 'brewer.SetTwo6',
                                        },
                                        labels: {
                                            render: function (args) {
                                                //return '$' + args.value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                                                value = '';
                                                return value;
                                            },
                                            fontColor: '#000',
                                            display: false,
                                        },
                                        datalabels: {
                                            display: false,
                                        }
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
                }
            });
        }
        function graphClickEvent(event, array) {
            console.log(array);
        }
        function CargarGraficas() {
            ReporteAgenda();
            ReporteFacturacion();
            ReporteCaptacion();
            ReporteServicioValor();
            ReporteCumplVi();
        }
    </script>

</asp:Content>
