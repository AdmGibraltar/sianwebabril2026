<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01_bootstrap.master" AutoEventWireup="true" CodeBehind="Rep_PortalKey.aspx.cs" Inherits="SIANWEB.Rep_PortalKey" %>

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
        .messagealert {
            width: 100%;
            position: fixed;
            top: 0px;
            z-index: 100000;
            padding: 0;
            font-size: 15px;
        }
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
        .form-control3 {
            width: 100% !important;
            height: 40px !important;
            padding: 0px 0px !important;
            line-height: 1.42857143 !important;
            color: #555 !important;
            background-color: #fff !important;
            background-image: none !important;
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
        <asp:UpdatePanel runat="server" ID="updpanel1">
            <ContentTemplate>
                <div class="panel panel-success" style="margin-top: 5px;">
                    <div class="panel-heading" style="height: 40px;">
                        <div class="col-md-11">
                            <h3 class="panel-title"></h3>
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="col-md-12" style="margin-top: 5px">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <div class="col-md-4">
                                        <asp:Label ID="Label2" runat="server" Text="Sucursales" />
                                    </div>
                                    <div class="col-md-8">
                                        <dx:BootstrapComboBox ID="Cmbsucursal" runat="server">
                                        </dx:BootstrapComboBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12" style="margin-top: 3%">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <div class="col-md-4">
                                        <asp:Label ID="lblPrecio" Text="Tipo de Reporte" runat="server"> </asp:Label> 
                                    </div>
                                    <div class="col-md-8">
                                        <asp:RadioButtonList runat="Server" CssClass="form-control3" ID="RbtTipo" RepeatDirection="Vertical">
                                            <asp:ListItem runat="Server" Value="1" Text="Sucursal" Selected="True"></asp:ListItem>
                                            <asp:ListItem runat="Server" Value="2" Text="Representante"></asp:ListItem>
                                            <asp:ListItem runat="Server" Value="3" Text="Cliente"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <div class="col-md-4">
                                        <asp:Label ID="Label1" Text="Seleccionar Fecha" runat="server"> </asp:Label> 
                                    </div>
                                    <div class="col-md-8">
                                        <asp:RadioButtonList runat="Server" CssClass="form-control3" ID="RBFecha" RepeatDirection="Vertical" OnSelectedIndexChanged="RBFecha_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem runat="Server" Value="1" Text="Año" Selected="True"></asp:ListItem>
                                            <asp:ListItem runat="Server" Value="2" Text="Mes/Año"></asp:ListItem>
                                            <asp:ListItem runat="Server" Value="3" Text="Trimestral"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <%--año--%>
                                <div runat="server" id="año"  style="display:none">
                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <div class="form-group">
                                            <div class="col-md-4">
                                                <asp:Label ID="Label3" runat="server" Text="Año" />
                                            </div>
                                            <div class="col-md-8">
                                                <dx:ASPxDateEdit runat="server" CssClass="form-control" ButtonStyle-CssClass=" btn  dropdown-toggle-date" ID="txtAño" PickerType="Years">
                                                    <CalendarProperties>
                                                        <FastNavProperties InitialView="Years" MaxView="Years" />
                                                    </CalendarProperties>
                                                </dx:ASPxDateEdit>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--mes y año--%>
                                <div runat="server" id="Mes"  style="display:none">
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
                                <%--trimestre--%>
                                <div runat="server" id="Trimestre"  style="display:none">
                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label13" runat="server" Text="Trimestre" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="BcbTrimestre" runat="server">
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
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
                        </div>
                        <div class="col-md-12" style="margin-top: 5px;">
                            <div class="col-md-3">
                                <button id="BtnReporte" type="button" runat="server" class="btn btn-primary btn-sm" onserverclick="BtnReporte_ServerClick">
                                    <i class="fa fa-search" aria-hidden="true"></i>&nbsp;Consultar
                                </button>

                                <button type="submit" runat="server" class="btn btn-default btn-sm" id="btnreporteexcel"
                                    onserverclick="btnreporteexcel_ServerClick">
                                    <i class="fa fa-file-excel-o" aria-hidden="true"></i>&nbsp;Excel
                                </button>
                            </div>
                        </div>
                        <div id="sucursal" runat="server" style="display:none">
                            <div class="col-md-12" style="margin-top: 5px;">
                                <dx:BootstrapGridView ID="GrdSucursal" ClientInstanceName="grid" runat="server" KeyFieldName="id_cte"
                                    Width="100%" AutoGenerateColumns="False">
                                    <SettingsPager PageSize="10" NumericButtonCount="4">
                                        <Summary Visible="false" />
                                        <PageSizeItemSettings Visible="false" ShowAllItem="false" />
                                    </SettingsPager>
                                    <Settings ShowFooter="true" />
                                    <Columns>
                                        <dx:BootstrapGridViewDataColumn Width="40px" FieldName="id_Cd" Caption="No. de Sucursal" CssClasses-DataCell="RightText" CssClasses-HeaderCell="centerText">
                                        </dx:BootstrapGridViewDataColumn>
                                        <dx:BootstrapGridViewDataColumn Width="30px" FieldName="nombre" Caption="Sucursal" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                        </dx:BootstrapGridViewDataColumn>
                                        <dx:BootstrapGridViewTextColumn Width="30px" FieldName="ClienteAlta" Caption="Clientes con usuario" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText" Visible="true">
                                            <PropertiesTextEdit DisplayFormatString="F0" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn Width="30px" FieldName="ClientePtes" Caption="Clientes sin usuario" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText" Visible="true">
                                            <PropertiesTextEdit DisplayFormatString="F0" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn Width="30px" FieldName="ClienteTotal" Caption="Total de Clientes" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText" Visible="true">
                                            <PropertiesTextEdit DisplayFormatString="F0" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn Width="30px" FieldName="PorcCliente" Caption="% de Clientes en Portal" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText" Visible="true">
                                            <PropertiesTextEdit DisplayFormatString="P2" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn Width="30px" FieldName="CantidadPedidoPortal" Caption="Pedidos Portal" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText" Visible="true">
                                            <PropertiesTextEdit DisplayFormatString="F0" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn Width="30px" FieldName="CantidadPedidoTotal" Caption="Pedidos Total" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText" Visible="true">
                                            <PropertiesTextEdit DisplayFormatString="F0" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn Width="30px" FieldName="PorcPortal" Caption="% de Pedidos Portal" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText" Visible="true">
                                            <PropertiesTextEdit DisplayFormatString="P2" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn Width="30px" FieldName="facturacionPortal" Caption="Venta Portal" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText" Visible="true">
                                            <PropertiesTextEdit DisplayFormatString="C" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn Width="30px" FieldName="facturaciontotal" Caption="Venta Total" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText" Visible="true">
                                            <PropertiesTextEdit DisplayFormatString="C" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn Width="30px" FieldName="porcfacturacionPortal" Caption="% de Venta Portal" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText" Visible="true">
                                            <PropertiesTextEdit DisplayFormatString="P2" />
                                        </dx:BootstrapGridViewTextColumn>
                                    </Columns>
                                </dx:BootstrapGridView>
                            </div>
                        </div>
                        <div id="DvRepresentante" runat="server"  style="display:none">
                            <div class="col-md-12" style="margin-top: 5px;">
                                <dx:BootstrapGridView ID="grdRepresentante" ClientInstanceName="gridRepresentante" runat="server" KeyFieldName="id_cte"
                                    Width="100%" AutoGenerateColumns="False">
                                    <SettingsPager PageSize="10" NumericButtonCount="4">
                                        <Summary Visible="false" />
                                        <PageSizeItemSettings Visible="false" ShowAllItem="false" />
                                    </SettingsPager>
                                    <Settings ShowFooter="true" />
                                    <Columns>
                                        <dx:BootstrapGridViewDataColumn Width="40px" FieldName="id_Cd" Caption="No. de Sucursal" CssClasses-DataCell="RightText" CssClasses-HeaderCell="centerText">
                                        </dx:BootstrapGridViewDataColumn>
                                        <dx:BootstrapGridViewDataColumn Width="30px" FieldName="nombre" Caption="Sucursal" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                        </dx:BootstrapGridViewDataColumn>
                                        <dx:BootstrapGridViewDataColumn Width="30px" FieldName="id_rik" Caption="No. de Rik" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                        </dx:BootstrapGridViewDataColumn>
                                        <dx:BootstrapGridViewDataColumn Width="40px" FieldName="nombreRik" Caption="Representante" CssClasses-DataCell="RightText" CssClasses-HeaderCell="centerText">
                                        </dx:BootstrapGridViewDataColumn>
                                        <dx:BootstrapGridViewTextColumn Width="30px" FieldName="ClienteAlta" Caption="Clientes con usuario" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText" Visible="true">
                                            <PropertiesTextEdit DisplayFormatString="F0" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn Width="30px" FieldName="ClientePtes" Caption="Clientes sin usuario" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText" Visible="true">
                                            <PropertiesTextEdit DisplayFormatString="F0" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn Width="30px" FieldName="ClienteTotal" Caption="Total de Clientes" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText" Visible="true">
                                            <PropertiesTextEdit DisplayFormatString="F0" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn Width="30px" FieldName="PorcCliente" Caption="% de Clientes en Portal" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText" Visible="true">
                                            <PropertiesTextEdit DisplayFormatString="P2" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn Width="30px" FieldName="CantidadPedidoPortal" Caption="Pedidos Portal" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText" Visible="true">
                                            <PropertiesTextEdit DisplayFormatString="F0" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn Width="30px" FieldName="CantidadPedidoTotal" Caption="Pedidos Total" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText" Visible="true">
                                            <PropertiesTextEdit DisplayFormatString="F0" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn Width="30px" FieldName="PorcPortal" Caption="% de Pedidos Portal" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText" Visible="true">
                                            <PropertiesTextEdit DisplayFormatString="P2" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn Width="30px" FieldName="facturacionPortal" Caption="Venta Portal" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText" Visible="true">
                                            <PropertiesTextEdit DisplayFormatString="C" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn Width="30px" FieldName="facturaciontotal" Caption="Venta Total" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText" Visible="true">
                                            <PropertiesTextEdit DisplayFormatString="C" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn Width="30px" FieldName="porcfacturacionPortal" Caption="% de Venta Portal" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText" Visible="true">
                                            <PropertiesTextEdit DisplayFormatString="P2" />
                                        </dx:BootstrapGridViewTextColumn>
                                    </Columns>
                                </dx:BootstrapGridView>
                            </div>
                        </div>
                        <div id="DvCliente" runat="server"  style="display:none">
                            <div class="col-md-12" style="margin-top: 5px;">
                                <dx:BootstrapGridView ID="grdCliente" ClientInstanceName="gridCliente" runat="server" KeyFieldName="id_cte"
                                    Width="100%" AutoGenerateColumns="False">
                                    <SettingsPager PageSize="10" NumericButtonCount="4">
                                        <Summary Visible="false" />
                                        <PageSizeItemSettings Visible="false" ShowAllItem="false" />
                                    </SettingsPager>
                                    <Settings ShowFooter="true" />
                                    <Columns>
                                        <dx:BootstrapGridViewDataColumn Width="40px" FieldName="id_Cd" Caption="No. de Sucursal" CssClasses-DataCell="RightText" CssClasses-HeaderCell="centerText">
                                        </dx:BootstrapGridViewDataColumn>
                                        <dx:BootstrapGridViewDataColumn Width="30px" FieldName="nombre" Caption="Sucursal" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                        </dx:BootstrapGridViewDataColumn>
                                        <dx:BootstrapGridViewDataColumn Width="30px" FieldName="id_cte" Caption="No. de Cliente" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                        </dx:BootstrapGridViewDataColumn>
                                        <dx:BootstrapGridViewDataColumn Width="40px" FieldName="nombreCliente" Caption="Cliente" CssClasses-DataCell="RightText" CssClasses-HeaderCell="centerText">
                                        </dx:BootstrapGridViewDataColumn>
                                        <dx:BootstrapGridViewDataColumn Width="30px" FieldName="id_rik" Caption="No. de Rik" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                        </dx:BootstrapGridViewDataColumn>
                                        <dx:BootstrapGridViewDataColumn Width="40px" FieldName="nombreRik" Caption="Representante" CssClasses-DataCell="RightText" CssClasses-HeaderCell="centerText">
                                        </dx:BootstrapGridViewDataColumn>
                                        <dx:BootstrapGridViewCheckColumn Width="30px" FieldName="ClientedeAlta" Caption="Usuario Portal" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText" Visible="true">
                                        </dx:BootstrapGridViewCheckColumn>
                                        <dx:BootstrapGridViewTextColumn Width="30px" FieldName="CantidadPedidoPortal" Caption="Pedidos Portal" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText" Visible="true">
                                            <PropertiesTextEdit DisplayFormatString="F0" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn Width="30px" FieldName="CantidadPedidoTotal" Caption="Pedidos Total" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText" Visible="true">
                                            <PropertiesTextEdit DisplayFormatString="F0" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn Width="30px" FieldName="PorcPortal" Caption="% de Pedidos Portal" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText" Visible="true">
                                            <PropertiesTextEdit DisplayFormatString="P2" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn Width="30px" FieldName="facturacionPortal" Caption="Venta Portal" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText" Visible="true">
                                            <PropertiesTextEdit DisplayFormatString="C" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn Width="30px" FieldName="facturaciontotal" Caption="Venta Total" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText" Visible="true">
                                            <PropertiesTextEdit DisplayFormatString="C" />
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn Width="30px" FieldName="porcfacturacionPortal" Caption="% de Venta Portal" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText" Visible="true">
                                            <PropertiesTextEdit DisplayFormatString="P2" />
                                        </dx:BootstrapGridViewTextColumn>
                                    </Columns>
                                </dx:BootstrapGridView>
                            </div>
                        </div> 
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnreporteexcel" />
                <asp:AsyncPostBackTrigger ControlID="RBFecha" />
            </Triggers>
        </asp:UpdatePanel>
        <div>
            <div class="messagealert" id="alert_container">
            </div>
        </div>
    </div>
</asp:Content>