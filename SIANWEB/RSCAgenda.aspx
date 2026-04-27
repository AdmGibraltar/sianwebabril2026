<%@ Page Language="C#"
    MasterPageFile="~/MasterPage/MasterPage01_bootstrap.Master"
    AutoEventWireup="true"
    CodeBehind="RSCAgenda.aspx.cs"
    Inherits="SIANWEB.GestionRSC.RSCAgenda" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/jquery-3.3.1.min.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.min.js")%>"></script>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css")%>">
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/css/font-awesome.min.css")%>">
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/icheck.min.js")%>"></script>


    <style type="text/css">
        form-control {
            width: 100%;
            height: 34px !important;
            padding: 6px 12px !important;
        }

         .modal-body {
            padding: 0px !important;
        }

        .panel-body {
            padding: 5px !important;
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


        .dxbs-spin-btn-group .increment, .dxbs-spin-btn-group .increment-l {
            display: inline !important;
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

        .dx-custom-style {
            border: dotted 3px;
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
            position:fixed !important;
        }

        .dropdown-menu > li > a 
        {
            white-space: pre-wrap !important;
        }
    </style>

    <script type="text/javascript"> 
        function ShowMessage(message, messagetype) {
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

        function NuevaCita() {
            document.getElementById('<%=iFrameCrearCita.ClientID%>').src = "RscAltaCita.aspx?";
            $("#modalCita").appendTo("body");
            $("#modalCita").modal({ "backdrop": "static" });
            $('#modalCita').modal('show'); 
        }


        function Agendar() {
            document.getElementById('<%=iFrameAgendar.ClientID%>').src = "RSCPreAgenda.aspx?";
            $("#modalAgenda").appendTo("body");
            $("#modalAgenda").modal({ "backdrop": "static" });
            $('#modalAgenda').modal('show');
         }

         

        function nuevaActividad(id) {
            document.getElementById('<%=iFrameActividad.ClientID%>').src = "rscEdicion.aspx?id=" + id;
            $("#modaledicion").appendTo("body");
            $("#modaledicion").modal({ "backdrop": "static" });
            $('#modaledicion').modal('show'); 
        } 

        window.closeModalDetalle = function () {
            $('#modalAgenda').modal('hide');
            $('#modaledicion').modal('hide');
            $('#modalCita').modal('hide');
            document.getElementById("CPH_Button1").click();
        }

        function OnMenuClick(s, e) {
            if (e.item.GetItemCount() <= 0) {
                if (e.item.name == "ExportAppointment")
                    buttonExport.DoClick();
                else
                    schedulerExport.RaiseCallback("MNUAPT|" + e.item.name);
            }
        } 

        window.closeModal = function (idedicion, guardar, modificar) {
            $('#modaledicion').modal('hide');
            if (idedicion != null) {
                document.getElementById('<%=iFrameCrearCita.ClientID%>').src = "RscAltaCita.aspx?id=" + idedicion + "";
                $("#modalCita").appendTo("body");
                $("#modalCita").modal({ "backdrop": "static" });
                $('#modalCita').modal('show');
            }
        } 

 
    </script>

    <div class="container-fluid">
        <asp:UpdateProgress ID="updateProgress" runat="server">
            <ProgressTemplate>
                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/load.gif" AlternateText="Loading ..."
                        ToolTip="Loading ..." Style="padding: position: fixed; top: 45%; left: 40%;" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel runat="server" ID="UP1">
            <ContentTemplate>
                <div class="col-md-12" style="margin-top: 5px;">
                    <div class="col-md-8" style="margin-top: 5px;">
                        <div class="panel panel-success">
                            <div class="panel-heading">
                                <h3 class="panel-title">Filtro(s)</h3>
                            </div>
                            <div class="panel-body">
                                <div class="col-md-10" style="margin-top: 1%;">
                                    <div class="form-group">
                                        <div class="col-md-2">
                                            Rol
                                        </div>
                                        <div class="col-md-10">
                                            <dx:BootstrapComboBox ID="DllRol" runat="server" CallbackPageSize="25" DropDownStyle="DropDown" OnSelectedIndexChanged="DllRol_SelectedIndexChanged" AutoPostBack="true">
                                            </dx:BootstrapComboBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-10" style="margin-top: 1%;">
                                    <div class="form-group">
                                        <div class="col-md-2">
                                            Usuario
                                        </div>
                                        <div class="col-md-10">
                                            <dx:BootstrapComboBox ID="DllUSuario" runat="server" CallbackPageSize="25" DropDownStyle="DropDown" OnSelectedIndexChanged="DllUSuario_SelectedIndexChanged1" AutoPostBack="true">
                                            </dx:BootstrapComboBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-10" style="margin-top: 1%;">
                                    <div class="form-group">
                                        <div class="col-md-2">
                                            Cliente
                                        </div>
                                        <div class="col-md-10">
                                            <dx:BootstrapComboBox ID="DllCLiente" runat="server" CallbackPageSize="25" DropDownStyle="DropDown">
                                            </dx:BootstrapComboBox>
                                        </div>
                                    </div>
                                </div> 
                                <div class="col-md-10" style="margin-top: 1%;">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <div class="col-md-4" style="padding: 0%">
                                                Fecha Inicial
                                            </div>
                                            <div class="col-md-8" style="padding: 0%">
                                                <dx:BootstrapDateEdit runat="server" ClientInstanceName="FechaInicial" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="Fecha" EditFormat="Date"> 
                                                </dx:BootstrapDateEdit>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <div class="col-md-4" style="padding: 0%">
                                                Fecha Final
                                            </div>
                                            <div class="col-md-8" style="padding: 0%">
                                                <dx:BootstrapDateEdit runat="server" ClientInstanceName="FechaFinal" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="FechaFinal" EditFormat="Date"> 
                                                </dx:BootstrapDateEdit>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4" style="margin-top: 5px;">
                        <div class="col-md-12" style="margin-top: 10px;">
                            <asp:Label runat="server" ID="lblActividad" Font-Size="Large" Font-Bold="true"></asp:Label>
                        </div>
                        <div class="col-md-12" style="margin-top: 10px;">
                            <dx:BootstrapGridView ID="GRDACtividadesdeDia" runat="server"
                                AutoGenerateColumns="False">
                                <Settings ShowColumnHeaders="false" />
                                <Columns>
                                    <dx:BootstrapGridViewImageColumn FieldName="colorACtividad" Width="30px">
                                        <PropertiesImage ImageUrlFormatString="~/Imagenes/{0}" ImageHeight="30px" />
                                    </dx:BootstrapGridViewImageColumn>
                                    <dx:BootstrapGridViewTextColumn FieldName="Actividad" HorizontalAlign="left" Caption="Estatus" CssClasses-HeaderCell="centerText">
                                    </dx:BootstrapGridViewTextColumn>
                                    <dx:BootstrapGridViewTextColumn FieldName="Cantidad" CssClasses-HeaderCell="centerText" HorizontalAlign="right" ReadOnly="true" CssClasses-DataCell="leftText"
                                        Caption="Producto">
                                    </dx:BootstrapGridViewTextColumn>
                                </Columns>
                                <TotalSummary>
                                    <dx:ASPxSummaryItem FieldName="Prd_Precio" SummaryType="Custom" DisplayFormat="Total:" />
                                    <dx:ASPxSummaryItem FieldName="Prd_Importe" SummaryType="Sum" DisplayFormat="c2"
                                        Tag="total" />
                                </TotalSummary>
                            </dx:BootstrapGridView>
                        </div>
                        <div class="col-md-12" style="margin-top: 10px; text-align: center">
                            <asp:Label runat="server" ID="lblCorte" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="col-md-12" style="margin-top: -25px;">
                    <div class="col-md-5">
                        <button id="Button1" type="button" runat="server" class="btn btn-primary btn-sm" onserverclick="btnCargarListado_ServerClick">
                            <i class="fa fa-search" aria-hidden="true"></i>&nbsp;Consultar
                        </button>

                        <button id="ButtonExport" type="button" runat="server" class="btn btn-primary btn-sm" onserverclick="ButtonExport_Click">
                            <i aria-hidden="true"></i>&nbsp;Exportar
                        </button>

                        <button id="BtnCrearCita" runat="server" type="button" class="btn btn-primary btn-sm" onserverclick="BtnCrearCita_ServerClick">
                            <i aria-hidden="true"></i>&nbsp;Nueva Actividad
                        </button>

                        <button id="btnAgendar" runat="server" type="button" class="btn btn-primary btn-sm" onserverclick="btnAgendar_ServerClick">
                            <i aria-hidden="true"></i>&nbsp; Agendar Actividad 
                        </button>
                    </div>
                </div>
                <div class="col-md-12" style="margin-top: 5px;">
                    <dx:BootstrapScheduler runat="server" ActiveViewType="Month" ID="BSAgenda" ClientSideEvents-AppointmentToolTipShowing="function onAppointmentToolTipShowing(s, e) {  
                     e.cancel = true;  
                     } "
                        OptionsBehavior-ShowFloatingActionButton="false">
                        <OptionsEditing AllowAppointmentDelete="None" AllowAppointmentEdit="None" AllowAppointmentCreate="None" />
                        <ClientSideEvents AppointmentClick="function(s, e) {  nuevaActividad(e.appointmentId);  }" /> 
                        <Storage>
                            <Appointments AutoRetrieveId="true">
                                <Mappings AppointmentId="ID" Start="StartTime" End="EndTime" Subject="Subject"
                                    Description="Descripcion"  Label="label" Status="estatus" />
                                <Labels>
                                    <dx:BootstrapAppointmentLabel Text="A"
                                        BackgroundCssClass="bg-light"  TextCssClass="text-dark"></dx:BootstrapAppointmentLabel>
                                    <dx:BootstrapAppointmentLabel Text="B"
                                        BackgroundCssClass="bg-primary" TextCssClass="text-white"></dx:BootstrapAppointmentLabel>
                                    <dx:BootstrapAppointmentLabel Text="C"
                                        BackgroundCssClass="bg-primary " TextCssClass="text-white"></dx:BootstrapAppointmentLabel>
                                    <dx:BootstrapAppointmentLabel Text="D"
                                        BackgroundCssClass="bg-primary " TextCssClass="text-white"></dx:BootstrapAppointmentLabel>
                                    <dx:BootstrapAppointmentLabel Text="N"
                                        BackgroundCssClass="bg-primary " TextCssClass="text-dark"></dx:BootstrapAppointmentLabel>
                                </Labels>
                                <Statuses>
                                    <dx:BootstrapAppointmentStatus Text="A"
                                        Type="Custom" CssClass="bg-primary "></dx:BootstrapAppointmentStatus>
                                    <dx:BootstrapAppointmentStatus Text="B"
                                        Type="Custom" CssClass="bg-primary "></dx:BootstrapAppointmentStatus>
                                    <dx:BootstrapAppointmentStatus Text="C"
                                        Type="Custom" CssClass="bg-primary "></dx:BootstrapAppointmentStatus>
                                    <dx:BootstrapAppointmentStatus Text="N"
                                        Type="Custom" CssClass="bg-primary "></dx:BootstrapAppointmentStatus>
                                </Statuses>
                            </Appointments>
                        </Storage>
                    </dx:BootstrapScheduler>
                      
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="ButtonExport" />
            </Triggers>
        </asp:UpdatePanel>
          <!-- nueva actividas/ edicion de activdad-->
        <div id="modalCita" class="modal" data-toggle="modal" style="z-index: 3000 !important;"
            role="dialog">
            <div class="modal-dialog" role="document" style="height: 560px !important;  width: 80%;""> 
                <div class="modal-content" style="height: 550px !important;">
                    <div class="modal-header" style="color: #F9F9F9 !important;   background-color: #59b2f1 !important;">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button> 
                        Alta de actividad 
                    </div>
                    <div class="modal-body" style="padding: 5px !important;" id="Div10">
                        <div class="embed-responsive embed-responsive-16by9" style="padding-bottom: 45% !Important;"> 
                            <iframe class="embed-responsive-item" id="iFrameCrearCita" runat="server" src=""></iframe>
                        </div>
                    </div>
                </div>
            </div>
        </div>
           <!-- Cosnulta de Actividad - Calebdario-->
        <div id="modaledicion" class="modal" data-toggle="modal" style="z-index: 3000 !important;"
            role="dialog">
            <div class="modal-dialog" role="document" style="height: 220px !important; width: 55%;"> 
                <div class="modal-content">
                       <div class="modal-header" style="color: #F9F9F9 !important;   background-color: #59b2f1 !important;">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button> 
                        Actividad 
                    </div>
                    <div class="modal-body" style="padding: 10px !important;" id="Div10">
                        <div class="embed-responsive embed-responsive-16by9 z-depth-1-half" style="padding-bottom: 33% !Important;"> 
                            <iframe class="embed-responsive-item" id="iFrameActividad" runat="server" src=""></iframe>
                        </div>
                    </div>
                </div>
            </div>
        </div>
           <!-- Preagenda automatica / carga excel preagenda-->
        <div id="modalAgenda" class="modal" data-toggle="modal" style="z-index: 3000 !important;"
            role="dialog">
            <div class="modal-dialog" role="document" style="height: 750px !important;  width: 100%;""> 
                <div class="modal-content" style="height: 750px !important;">
                    <div class="modal-header" style="color: #F9F9F9 !important;   background-color: #59b2f1 !important;">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button> 
                       Agendar Actividad
                    </div>
                    <div class="modal-body" style="padding: 5px !important;" id="Div10">
                        <div class="embed-responsive embed-responsive-16by9" style="padding-bottom: 45% !Important;"> 
                            <iframe class="embed-responsive-item" id="iFrameAgendar" runat="server" src=""></iframe>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div> 
</asp:Content>
