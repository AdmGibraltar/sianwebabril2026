<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/masterpage02Boostrap.Master" AutoEventWireup="true" CodeBehind="RSCEliminar.aspx.cs" Inherits="SIANWEB.RSCEliminar" %>

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
        <div class="col-md-12">
            <div class="col-md-1">
                <asp:Label runat="server" ID="lblmotivo" Text="Motivo de Cancelación"></asp:Label>
            </div>
            <div class="col-md-10">
                <dx:BootstrapTextBox ID="txtMotivo" runat="server" MaxLength="50">
                </dx:BootstrapTextBox>
            </div>
        </div>
        <div class="col-md-12" style="margin-top: 10px;">
            <button type="button" class="btn btn-primary btn-sm" id="btnMotivoCance" runat="server" onserverclick="btnMotivo_ServerClick">
                <i aria-hidden="true"></i>&nbsp;Eliminar
            </button>

            <button class="btn btn-default" data-dismiss="modal" id="btnCancelarMotivo" onclick="closemodalMotivo()">
                Cancelar</button>
        </div>
    </div>
    <div>
        <div class="messagealert" id="alert_container">
        </div>
    </div>


    <script type="text/javascript">
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


        function ShowMessageExito(message, messagetype) {
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
            window.parent.closeModalmotivo();
        }

        function closemodalMotivo() { 
            window.parent.closeModalmotivo();
        }
    </script>
</asp:Content>
