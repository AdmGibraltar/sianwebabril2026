<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CargaSolicitudOC.aspx.cs" Inherits="SIANWEB.CargaSolicitudOC"
    MasterPageFile="~/MasterPage/MasterPage01_bootstrap.master" %>


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

        <div runat="server" id="divPrincipal">
            <div class="col-md-12">
                <div class="panel panel-success" style="margin-top: 5px;">
                    <div class="panel-heading" style="height: 40px;">
                        <div class="col-md-10">
                            <h3 class="panel-title">Autorización de Precio - AAA</h3>
                        </div>
                    </div>
                    <div class="panel-body">
                        <div runat="server" id="div1">
                            <div id="filtros" runat="server">
                                <div class="col-md-12">
                                    <div class="col-md-4" style="margin-top: 5px;">
                                        <div class="form-group">
                                            <div class="col-md-4">
                                                <asp:Label ID="Label8" runat="server" Text="Sucursal" />
                                            </div>
                                            <div class="col-md-8">
                                                <dx:BootstrapComboBox ID="CMBSucursal" runat="server">
                                                </dx:BootstrapComboBox>
                                            </div>
                                        </div>
                                    </div>
                                     <div class="col-md-4">
                                        <div class="form-group">
                                            <div class="col-md-4">
                                                <asp:Label ID="lblEstatus" runat="server" Text="Estatus"></asp:Label>
                                            </div>
                                            <div class="col-md-8">
                                                <dx:BootstrapComboBox ID="CMBEstatus" runat="server">
                                                </dx:BootstrapComboBox>
                                            </div>
                                        </div>
                                    </div>
                                </div> 
                                <div class="col-md-12" style="margin-top: 5px;">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <div class="col-md-4">
                                                Fecha Inicial
                                            </div>
                                            <div class="col-md-8">
                                                <dx:BootstrapDateEdit ID="CMBFechaInicial" runat="server">
                                                    <ValidationSettings ValidationGroup="Validation">
                                                        <RequiredField IsRequired="true" ErrorText="Campo requerido" />
                                                    </ValidationSettings>
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
                                                <dx:BootstrapDateEdit ID="CMBFechaFinal" runat="server">
                                                    <ValidationSettings ValidationGroup="Validation">
                                                        <RequiredField IsRequired="true" ErrorText="Campo requerido" />
                                                    </ValidationSettings>
                                                </dx:BootstrapDateEdit>
                                            </div>
                                        </div>
                                    </div>
                                       <div class="col-md-4"> 
                                             <asp:LinkButton  CssClass="btn btn-default btn-sm" ID="btnBuscar" runat="server"
                                            OnClick="btnBuscar_Click">
                                            <i class="fa fa-search" aria-hidden="true"></i>&nbsp;Buscar
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12 text-center" style="margin-top: 5px;">
                                <dx:BootstrapGridView ID="gridviewOrderCompra" ClientInstanceName="grid" runat="server"
                                    KeyFieldName="id_Sol;Id_Cte" Width="100%" AutoGenerateColumns="False"> 
                                    <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" />
                                    <SettingsEditing Mode="Batch" />
                                    <Columns> 
                                        <dx:BootstrapGridViewDateColumn   Caption="Aceptar">
                                            <DataItemTemplate>
                                                <button id="btnAcceptar" type="button" class="btn btn-link" title="Aceptar"
                                                    runat="server" onserverclick="btnAcceptar_ServerClick">
                                                    <span class="fa fa-check"></span>
                                                </button>
                                            </DataItemTemplate>
                                        </dx:BootstrapGridViewDateColumn>
                                        <dx:BootstrapGridViewDateColumn Caption="Rechazar">
                                            <DataItemTemplate>
                                                <button id="BtnRechazar" type="button" class="btn btn-link" title="Rechazar"
                                                    runat="server" onserverclick="BtnRechazar_ServerClick">
                                                    <span class="fa fa-remove"></span>
                                                </button>
                                            </DataItemTemplate>
                                        </dx:BootstrapGridViewDateColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="id_Sol" Caption="Solicitud" CssClasses-HeaderCell="centerText" />
                                        <dx:BootstrapGridViewTextColumn FieldName="estatusSTR" CssClasses-HeaderCell="centerText" Caption="Estatus" />
                                        <dx:BootstrapGridViewTextColumn FieldName="OrdenCompra" CssClasses-HeaderCell="centerText" Caption="OC Centralizada"
                                            HorizontalAlign="Left" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Id_Cte" CssClasses-HeaderCell="centerText" HorizontalAlign="Center" Caption="Núm. Cliente" />
                                        <dx:BootstrapGridViewTextColumn FieldName="Cte_Nom" CssClasses-HeaderCell="centerText" HorizontalAlign="Center" Caption="Cliente" />
                                    </Columns>
                                    <Templates>
                                        <DetailRow>
                                            <dx:BootstrapGridView ID="gridpedidoVIProductoOrdenCompra" runat="server" Width="80%" OnBeforePerformDataSelect="gridpedidoVIProductoOrdenCompra_BeforePerformDataSelect"
                                                AutoGenerateColumns="False">
                                                <SettingsPager PageSize="5">
                                                </SettingsPager>
                                                <Columns>
                                                    <dx:BootstrapGridViewTextColumn FieldName="Id_PrdOri" HorizontalAlign="Right" Caption="Producto Original" CssClasses-HeaderCell="centerText">
                                                    </dx:BootstrapGridViewTextColumn>
                                                    <dx:BootstrapGridViewTextColumn FieldName="Prd_DescripcionOri" CssClasses-HeaderCell="centerText" ReadOnly="true" CssClasses-DataCell="leftText"
                                                        Caption="Descripción">
                                                    </dx:BootstrapGridViewTextColumn> 
                                                    <dx:BootstrapGridViewTextColumn FieldName="Id_Prd" HorizontalAlign="Right" Caption="Producto Sustituto" CssClasses-HeaderCell="centerText">
                                                    </dx:BootstrapGridViewTextColumn>
                                                    <dx:BootstrapGridViewTextColumn FieldName="Prd_Descripcion" CssClasses-HeaderCell="centerText" ReadOnly="true" CssClasses-DataCell="leftText"
                                                        Caption="Descripción producto sustituo">
                                                    </dx:BootstrapGridViewTextColumn>
                                                    <dx:BootstrapGridViewSpinEditColumn FieldName="Importe" CssClasses-HeaderCell="centerText" CssClasses-DataCell="rightText"
                                                        Caption="Precio producto sustituto">
                                                        <PropertiesSpinEdit DisplayFormatString="{0:C}">
                                                        </PropertiesSpinEdit>
                                                    </dx:BootstrapGridViewSpinEditColumn>
                                                      <dx:BootstrapGridViewSpinEditColumn FieldName="Ped_Cantidad" CssClasses-HeaderCell="centerText" CssClasses-DataCell="rightText" Visible="false"
                                                        Caption="Cantidad">
                                                        <PropertiesSpinEdit DisplayFormatString="{0:C}">
                                                        </PropertiesSpinEdit>
                                                    </dx:BootstrapGridViewSpinEditColumn>
                                                    <dx:BootstrapGridViewSpinEditColumn FieldName="AAA" CssClasses-HeaderCell="centerText" CssClasses-DataCell="rightText"
                                                        Caption="Precio AAA">
                                                        <PropertiesSpinEdit DisplayFormatString="{0:C}">
                                                        </PropertiesSpinEdit>
                                                    </dx:BootstrapGridViewSpinEditColumn>
                                                </Columns>
                                            </dx:BootstrapGridView>
                                        </DetailRow>
                                    </Templates>
                                    <Settings ShowGroupPanel="false" ShowFooter="True" ShowFilterRow="false" ShowFilterRowMenu="true" />
                                </dx:BootstrapGridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="HF_ClvPag" runat="server" />
        </div>

    </div>
    <div class="messagealert" id="alert_container">
    </div>

</asp:Content>

<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style1 {
            height: 26px;
        }
    </style>
</asp:Content>
