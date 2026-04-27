<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01_bootstrap.Master" AutoEventWireup="true" CodeBehind="RepAnalisisRemisiones2.aspx.cs" Inherits="SIANWEB.RepAnalisisRemisiones2" %>



<%@ Register Assembly="DevExpress.Web.Bootstrap.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/jquery-3.3.1.min.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/jquery-template/jquery.loadTemplate.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.min.js")%>"></script>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css")%>" />
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/css/font-awesome.min.css")%>" />

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

    <div class="container-fluid">
        <div class="col-md-12" style="margin-top: 5px;">
            <div class="col-md-8" style="margin-top: 5px;">
            </div>
            <div class="col-md-4" style="margin-top: 5px;">
                <div class="col-md-4">
                    Centro de distribución
                </div>
                <div class="col-md-8">
                    <dx:BootstrapComboBox ID="CmbCentro" runat="server" CallbackPageSize="25" DropDownStyle="DropDown" OnSelectedIndexChanged="CmbCentro_SelectedIndexChanged" AutoPostBack="true">
                    </dx:BootstrapComboBox>
                </div>
            </div>
        </div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/load.gif" AlternateText="Loading ..."
                        ToolTip="Loading ..." Style="position: fixed; top: 45%; left: 40%;" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel runat="server" ID="updpanelremision">
            <ContentTemplate>
                <div class="col-md-12" style="margin-top: 5px;">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h3 class="panel-title">Reporte de Remisiones</h3>
                            <span class="pull-right clickable panel-collapsed" style="margin-top: -20px !important;"><i class="glyphicon glyphicon-chevron-up"></i>
                            </span>
                        </div>
                        <div class="panel-body">
                            <div class="col-md-12" style="margin-top: 1%;">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <div class="col-md-4">
                                            Tipo de Reporte
                                        </div>
                                        <div class="col-md-8">
                                            <dx:ASPxRadioButtonList runat="server" ID="RblTipoRep" RepeatDirection="Vertical" OnSelectedIndexChanged="RblTipoRep_SelectedIndexChanged" AutoPostBack="true">
                                                <Items>
                                                    <dx:BootstrapListEditItem Text="Actual" Value="1" Selected="true"></dx:BootstrapListEditItem>
                                                    <dx:BootstrapListEditItem Text="Cierre" Value="2"></dx:BootstrapListEditItem>
                                                </Items>
                                            </dx:ASPxRadioButtonList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12" style="margin-top: 1%; display: none">
                                <div class="col-md-8">
                                    <div id="Rango" runat="server">
                                        <div class="col-md-12" style="margin-top: 1%;">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <div class="col-md-4">
                                                        Fecha Inicial
                                                    </div>
                                                    <div class="col-md-8">
                                                        <dx:BootstrapDateEdit runat="server" ClientInstanceName="FechaInicial" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="dpFechaini" EditFormat="Date">
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
                                                        <dx:BootstrapDateEdit runat="server" ClientInstanceName="FechaFinal" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="dpFechafin" EditFormat="Date">
                                                        </dx:BootstrapDateEdit>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col.md-12" style="padding: 20px;">
                                  <button type="submit" runat="server" class="btn btn-default btn-sm" id="BtnExcel"
                                                            onserverclick="BtnExcel_ServerClick">
                                                            <i class="fa fa-file-excel-o" aria-hidden="true"></i>&nbsp;Descargar Reporte
                                                        </button>
                            </div>
                            <asp:HiddenField ID="HF_ClvPag" runat="server" />
                        </div>
                    </div>
                </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="BtnExcel" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
