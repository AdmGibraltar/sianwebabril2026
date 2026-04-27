<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01_bootstrap.Master" AutoEventWireup="true" CodeBehind="Rep_FacturacionDoble.aspx.cs" Inherits="SIANWEB.Rep_FacturacionDoble" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/jquery-3.3.1.min.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.min.js")%>"></script>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css")%>">
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/css/font-awesome.min.css")%>">

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
            height: 40px;
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

        .dxbs-listbox > li > .checkbox > label > input[type="checkbox"], .dxbs-listbox > ul > li > .checkbox > label > input[type="checkbox"], .dxbs-listbox .dxbs-list-selectall > .checkbox > label > input[type="checkbox"] {
            position: static !important;
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

        function AbrirFacturaPDF(WebURL) {
            window.open(WebURL, "_blank");
        }

        function abrirArchivo(pagina) {
            console.log(pagina)
            window.open(pagina, "_blank");
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
        <asp:UpdatePanel runat="server" ID="updRepDobleFacturacion">
            <ContentTemplate>
                <div class="col-md-12" style="font-family: verdana; font-size: 8pt" runat="server"
                    width="99%">
                    <div class="col-md-12" style="text-align: right" width="150px">
                        <div class="form-group">
                            <div class="col-md-9">
                            </div>
                            <div class="col-md-1">
                                <asp:Label ID="TblEncabezado" runat="server" Text="Centro de distribución"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <dx:BootstrapComboBox ID="CmbCentro" runat="server" OnSelectedIndexChanged="CmbCentro_TextChanged" AutoPostBack="True">
                                </dx:BootstrapComboBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <div class="col-md-10">
                                <h3 class="panel-title">Reporte de Facturación Nacional</h3>
                            </div>
                            <div class="col-md-2" style="margin-top: -5px;">
                                <button type="submit" runat="server" class="btn btn-default btn-sm" id="btnExcel"
                                    onserverclick="btnExcel_ServerClick">
                                    <i class="fa fa-file-excel-o" aria-hidden="true"></i>&nbsp;Excel
                                </button>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div runat="server" id="divPrincipal">
                                <div class="col-md-12" style="font-family: Verdana; font-size: 8pt">
                                    <div class="col-md-12" style="margin-top: 1%;">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    Fecha Inicial
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapDateEdit ID="TxtfechaIni" runat="server">
                                                    </dx:BootstrapDateEdit>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    Fecha Final
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapDateEdit ID="TxtFechaFinal" runat="server">
                                                    </dx:BootstrapDateEdit>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <button type="submit" runat="server" class="btn btn-default" id="BtnCosnultar"
                                            onserverclick="BtnCosnultar_ServerClick">
                                            <span>Consultar</span>
                                        </button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    <dx:BootstrapGridView ID="GrdFacturacion" Width="100%" ClientInstanceName="grid" runat="server"
                        KeyFieldName="Id_Fac" AutoGenerateColumns="False">
                        <SettingsBehavior AllowSort="false" AllowSelectByRowClick="true" SelectionStoringMode="DataIntegrityOptimized" />
                        <SettingsEditing Mode="Batch" />
                        <Settings ShowHeaderFilterButton="true" HorizontalScrollBarMode="Auto" />
                        <SettingsPager PageSize="10" NumericButtonCount="6">
                            <Summary Visible="false" />
                            <PageSizeItemSettings Visible="true" ShowAllItem="true" />
                        </SettingsPager>
                        <Columns>
                            <dx:BootstrapGridViewTextColumn FieldName="Id_Cd" Caption="Id_Cd"
                                Visible="false">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="Id_Fac" Caption="# Factura" Fixed="true">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="Nombreestatus" Caption="Estatus">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="Id_Cte" Caption="# Cliente">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="Cte_NomComercial" Caption="Cliente" Width="200px">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewDateColumn FieldName="Fac_Fecha" Caption="Fecha">
                            </dx:BootstrapGridViewDateColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="Fac_SubTotal" Caption="Importe">
                                <PropertiesTextEdit DisplayFormatString="c" />
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="Fac_ImporteIva" Caption="I.V.A.">
                                <PropertiesTextEdit DisplayFormatString="c" />
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="Total" Caption="Total">
                                <PropertiesTextEdit DisplayFormatString="c" />
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="Fac_FolioFiscal" Width="310" Caption="Folio Fiscal">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewDateColumn FieldName="FolioAE"  Width="160" Caption="# Factura Central">
                            </dx:BootstrapGridViewDateColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="NombreestatusCN"   Width="160" Caption="Estatus Central">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewTextColumn FieldName="Fac_FolioFiscalCN"  Width="310" Caption="Folio Fiscal Central">
                            </dx:BootstrapGridViewTextColumn>
                            <dx:BootstrapGridViewDataColumn Width="80px" FieldName="PDF" Caption="PDF">
                                <DataItemTemplate>
                                    <button id="BtnPDF" type="button" class="btn btn-link" title="Descargar PDF"
                                        runat="server" onserverclick="BtnPDF_ServerClick">
                                        <span class="fa fa-file-pdf-o"></span>
                                    </button>
                                </DataItemTemplate>
                            </dx:BootstrapGridViewDataColumn>
                            <dx:BootstrapGridViewDataColumn Width="80px" FieldName="XML" Caption="XML">
                                <DataItemTemplate>
                                    <button id="BtnXML" type="button" class="btn btn-link" title="Descargar XML"
                                        runat="server" onserverclick="BtnXML_ServerClick">
                                        <span class="fa fa-file-archive-o"></span>
                                    </button>
                                </DataItemTemplate>
                            </dx:BootstrapGridViewDataColumn>
                            <dx:BootstrapGridViewDataColumn Width="120px" FieldName="PDFCentral" Caption="PDF Central">
                                <DataItemTemplate>
                                    <button id="BTNPDFCentral" type="button" class="btn btn-link" title="Descargar PDF Central"
                                        runat="server" onserverclick="BTNPDFCentral_ServerClick">
                                        <span class="fa fa-file-pdf-o"></span>
                                    </button>
                                </DataItemTemplate>
                            </dx:BootstrapGridViewDataColumn>
                            <dx:BootstrapGridViewDataColumn Width="120px" FieldName="XMLCentral" Caption="XML Central">
                                <DataItemTemplate>
                                    <button id="BtnXMLCentral" type="button" class="btn btn-link" title="Descargar XML Central"
                                        runat="server" onserverclick="BtnXMLCentral_ServerClick">
                                        <span class="fa fa-file-archive-o"></span>
                                    </button>
                                </DataItemTemplate>
                            </dx:BootstrapGridViewDataColumn>
                        </Columns>
                    </dx:BootstrapGridView>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="BtnCosnultar" />
                <asp:PostBackTrigger ControlID="btnExcel" />
                <asp:PostBackTrigger ControlID="GrdFacturacion" />
            </Triggers>
        </asp:UpdatePanel>

        <div class="messagealert" id="alert_container">
        </div>
    </div>
</asp:Content>
