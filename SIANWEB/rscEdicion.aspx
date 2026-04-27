<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/masterpage02Boostrap.Master" AutoEventWireup="true" CodeBehind="rscEdicion.aspx.cs" Inherits="SIANWEB.rscEdicion" %>

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
        .dvstyle {
            height: 180px !important;
        }

        .dropdown-toggle {
            height: 34px !important;
        }

        .caret {
            margin-top: 10px !important;
        }

        .dropdown-menu {
            height: 80px !important;
        }

        .modal-body {
            padding: 0px !important;
        }

        .messagealert {
            width: 100%;
            position: fixed;
            top: 0px;
            z-index: 100000;
            padding: 0;
            font-size: 15px;
        }

        .modal-header {
            padding: 5px !important;
        }
    </style>

    <div class="modal-body">
        <asp:UpdatePanel runat="server" ID="updpanel1">
            <ContentTemplate>
                <fieldset>
                    <div class="col-md-12">
                        <div class="form-inline">
                            <label id="Label1" runat="server" style="vertical-align: text-top;">
                                Rol:  
                            </label>
                            <label id="lblRol" runat="server" style="vertical-align: text-top;">
                            </label>
                            <label id="lblid" runat="server" style="vertical-align: text-top;" visible="false">
                            </label>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="form-inline">
                            <label id="Label2" runat="server" style="vertical-align: text-top;">
                                Usuario: 
                            </label>
                            <label id="lblUsuario" runat="server" style="vertical-align: text-top;">
                            </label>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="form-inline">
                            <label id="Label3" runat="server" style="vertical-align: text-top;">
                                Cliente: 
                            </label>
                            <label id="lblCliente" runat="server" style="vertical-align: text-top;">
                            </label>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="form-inline">
                            <label id="Label6" runat="server" style="vertical-align: text-top;">
                                Bracket: 
                            </label>
                            <label id="lblBracket" runat="server" style="vertical-align: text-top;">
                            </label>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="form-inline">
                            <label id="Label4" runat="server" style="vertical-align: text-top;">
                                Actividad: 
                            </label>
                            <label id="lblActividad" runat="server" style="vertical-align: text-top;">
                            </label>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="form-inline">
                            <label id="Label5" runat="server" style="vertical-align: text-top;">
                                Horario:  
                            </label>
                            <label id="lblHorario" runat="server" style="vertical-align: text-top;">
                            </label>
                        </div>
                    </div>

                </fieldset>
                <div class="col-md-12" style="margin-top: 2%;">
                    <button id="BtnModificar" type="button" runat="server" class="btn btn-primary btn-sm" onserverclick="btnAceptar_Click">
                        <i aria-hidden="true"></i>&nbsp;Actualizar
                    </button>
                    <button type="button" class="btn btn-primary btn-sm" id="btnEliminar" runat="server" onserverclick="btnAceptarEliminar_Click">
                        <i aria-hidden="true"></i>&nbsp;Eliminar
                    </button>

                    <button type="button" class="btn btn-primary btn-sm" id="BtnIniciar" runat="server" visible="false"
                        onserverclick="BtnIniciar_ServerClick">
                        <i aria-hidden="true"></i>&nbsp;Iniciar Actividad
                    </button>

                    <button type="button" class="btn btn-primary btn-sm" id="BtnTerminar" runat="server" visible="false"
                        onserverclick="BtnTerminar_ServerClick">
                        <i aria-hidden="true"></i>&nbsp;Terminar Actividad
                    </button>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="BtnModificar" />
                <asp:AsyncPostBackTrigger ControlID="btnEliminar" />
                <asp:AsyncPostBackTrigger ControlID="BtnIniciar" />
                <asp:AsyncPostBackTrigger ControlID="BtnTerminar" />
            </Triggers>
        </asp:UpdatePanel>

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
                <div class="modal-body" id="Div11" style="padding: 25px !important; overflow-y: hidden !Important;">
                    <div class="col-md-12">
                        <div class="col-md-1">
                            <i id="modalAcysMensaje_Icon" class="fa fa-exclamation-triangle_x" aria-hidden="true"></i>
                        </div>
                        <div class="col-md-10">
                            <span id="lblmensaje" runat="server"></span>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-12 ">
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
    <div id="ModalQuestion" class="modal" role="dialog" tabindex="-1" style="z-index: 1220!important">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 id="modalAcysMensaje_Titulo">Cancelación</h4>
                </div>
                <div class="modal-body" style="padding: 5px !important;" id="Div10">
                    <div class="embed-responsive embed-responsive-16by9" style="padding-bottom: 25% !Important;">
                        <iframe class="embed-responsive-item" id="iFrameMotivo" runat="server" src=""></iframe>
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
                    <div class="col-md-12">
                        <div class="col-md-1">
                            <i id="modalAcysMensaje_Icon" class="fa fa-exclamation-triangle_x" aria-hidden="true"></i>
                        </div>
                        <div class="col-md-10">
                            <span id="lblmensajeExito" runat="server"></span>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-12 ">
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
    <div>
        <div class="messagealert" id="alert_container">
        </div>
    </div>
    <script type="text/javascript">  
        function closeModalDetalle() {
            $('#modalmensajeExito').modal('hide');
            window.parent.closeModalDetalle();
        }

        function closeModalDetalleExito() {
            $('#modalmensajeExito').modal('hide');
            window.parent.closeModalDetalle();
        }

        window.closeModalmotivo = function () {
            $('#ModalQuestion').modal('hide');
            window.parent.closeModalDetalle();
        }

        function modalMensajeExito(mensaje) {
            document.getElementById('<%=lblmensajeExito.ClientID%>').innerHTML = mensaje;
            $("#modalmensajeExito").appendTo("body")
            $("#modalmensajeExito").modal({ "backdrop": "static" });
            $('#modalmensajeExito').modal('show');
        }

        function modalMensaje(mensaje) {
            document.getElementById('<%=lblmensaje.ClientID%>').innerHTML = mensaje;
            $("#modalMensaje").appendTo("body")
            $("#modalMensaje").modal({ "backdrop": "static" });
            $('#modalMensaje').modal('show');
        }


        function ModalMotivo(id) {
            document.getElementById('<%=iFrameMotivo.ClientID%>').src = "RSCEliminar.aspx?id=" + id;
            $("#ModalQuestion").appendTo("body")
            $("#ModalQuestion").modal({ "backdrop": "static" });
            $('#ModalQuestion').modal('show');
        }

        function CloseEdicion(idedicion, guardar, modificar) {
            window.parent.closeModal(idedicion, guardar, modificar);
        }

        function ShowMessage(message, messagetype) {
            var cssclass;
            $('#ModalQuestion').modal('hide');
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
</asp:Content>
