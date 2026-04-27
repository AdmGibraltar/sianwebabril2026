<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/masterpage02Boostrap.Master" AutoEventWireup="true" CodeBehind="CatClienteEcommerceInsert.aspx.cs" Inherits="SIANWEB.CatClienteEcommerceInsert" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/jquery-3.3.1.min.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.min.js")%>"></script>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css")%>">
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/css/font-awesome.min.css")%>">
    <script type="text/javascript">
        function closeModalDetalle() {
            $('#modalmensajeExito').modal('hide');
            window.parent.closeModalDetalle();
        };
        function closeModalDetalleExito() {
            $('#modalmensajeExito').modal('hide');
            window.parent.closeModalDetalle();
        };
        function modalMensajeExito(mensaje) {
            document.getElementById('<%=lblmensajeExito.ClientID%>').innerHTML = mensaje;
            $("#modalmensajeExito").appendTo("body")
            $("#modalmensajeExito").modal({ "backdrop": "static" });
            $('#modalmensajeExito').modal('show');
        }
        function modalMensaje(mensaje) {
            document.getElementById('<%=lblmensaje.ClientID%>').innerHTML = mensaje;
            $("#modalAcysMensaje").appendTo("body")
            $("#modalAcysMensaje").modal({ "backdrop": "static" });
            $('#modalAcysMensaje').modal('show');
        }
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

    <div class="modal-body">
        <asp:UpdateProgress ID="updateProgress" runat="server">
            <ProgressTemplate>
                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/load.gif" AlternateText="Loading ..."
                        ToolTip="Loading ..." Style="padding: position: fixed; top: 45%; left: 40%;" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div class="col-xs-12" style="margin-top: 5px;">
                    <div class="form-group">
                        <div class="col-xs-4">
                            <asp:label for="txtNombreContacto">Cliente</asp:label>
                        </div>
                        <div class="col-xs-8">
                            <dx:BootstrapComboBox runat="server" ID="CmbCliente">
                                <ValidationSettings ValidationGroup="VG1">
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                            </dx:BootstrapComboBox>
                        </div>
                    </div>
                    </div>
                     <div class="col-xs-12" style="margin-top: 5px;">
                    <div class="form-group">
                        <div class="col-xs-4">
                            <asp:label for="txtNombreContacto">Usuario (Correo Electronico)</asp:label>
                        </div>
                        <div class="col-xs-8">
                            <dx:BootstrapTextBox runat="server" ID="txtUsuario">
                                <ValidationSettings ValidationGroup="VG1">
                                    <RequiredField IsRequired="true" />
                                    <RegularExpression ValidationExpression="^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$"/>
                                </ValidationSettings>
                            </dx:BootstrapTextBox>
                        </div>
                    </div>
                </div>
                <div class="col-xs-12" style="margin-top: 5px">
                    <div class="col-xs-6" style="padding-left: 0px !important;">
                        <div class="form-group">
                            <div class="col-xs-4">
                                <asp:Label ID="Label1" runat="server" Text="Nombre(s)" />
                            </div>
                            <div class="col-xs-8" style="padding-left: 20px;">
                                <dx:BootstrapTextBox ID="TxtNombre" runat="server">
                                </dx:BootstrapTextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-6">
                        <div class="form-group">
                            <div class="col-xs-4">
                                <asp:Label ID="Label3" runat="server" Text="Apellido(s)" />
                            </div>
                            <div class="col-xs-8">
                                <dx:BootstrapTextBox ID="txtApellido" runat="server" Width="187">
                                </dx:BootstrapTextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xs-12" style="margin-top: 5px;">
                    <div class="col-xs-10">
                    </div>
                    <div class="col-xs-2">
                        <dx:BootstrapButton ID="btncaptacion_Guardar" ValidationGroup="VG1" runat="server" Text="Guardar" SettingsBootstrap-RenderOption="Primary"
                            OnClick="btnGuardar_Click">
                        </dx:BootstrapButton>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div id="modalAcysMensaje" class="modal" role="dialog" tabindex="-1" style="z-index: 1220!important;"
        style="display: none;">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 id="modalAcysMensaje_Titulo">Mensaje</h4>
                </div>
                <div class="modal-body" id="Div11" style="padding: 25px !important; overflow-y: hidden !Important;">
                    <div class="col-xs-12">
                        <div class="col-xs-1">
                            <i id="modalAcysMensaje_Icon" class="fa fa-exclamation-triangle_x" aria-hidden="true"></i>
                        </div>
                        <div class="col-xs-10">
                            <span id="lblmensaje" runat="server"></span>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-xs-12 ">
                            <table>
                                <tr>
                                    <td>
                                        <button class="btn btn-default" data-dismiss="modal" id="Button9">
                                            Ok</button>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="modalmensajeExito" class="modal" role="dialog" tabindex="-1" style="z-index: 1220!important;"
        style="display: none;">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 id="modalAcysMensaje_Titulo">Mensaje</h4>
                </div>
                <div class="modal-body" id="Div11" style="padding: 25px !important; overflow-y: hidden !Important;">
                    <div class="col-xs-12">
                        <div class="col-xs-1">
                            <i id="modalAcysMensaje_Icon" class="fa fa-exclamation-triangle_x" aria-hidden="true"></i>
                        </div>
                        <div class="col-xs-10">
                            <span id="lblmensajeExito" runat="server"></span>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-xs-12 ">
                            <table>
                                <tr>
                                    <td>
                                        <button class="btn btn-default" id="Button9" onclick="closeModalDetalleExito()">
                                            Aceptar</button>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
