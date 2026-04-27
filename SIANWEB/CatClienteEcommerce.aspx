<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage01_bootstrap.Master" AutoEventWireup="true" CodeBehind="CatClienteEcommerce.aspx.cs" Inherits="SIANWEB.CatClienteEcommerce" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/jquery-3.3.1.min.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/js/bootstrap.min.js")%>"></script>
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/bootstrap-3.3.7-dist/css/bootstrap.min.css")%>">
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/Librerias/fontawesome-free-4.6.3/css/font-awesome.min.css")%>">


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

        .center {
            position: absolute;
            left: 50%;
            top: 50%;
            transform: translate(-50%, -50%);
        }

        .body {
            font-family: 'Varela Round', sans-serif;
        }

        .modal-confirm {
            color: #636363;
            width: 550px;
        }

            .modal-confirm .modal-content {
                padding: 20px;
                border-radius: 5px;
                border: none;
            }

            .modal-confirm .modal-header {
                padding: 0 15px;
                border-bottom: none;
                position: relative;
            }

            .modal-confirm h4 {
                display: inline-block;
                font-size: 26px;
            }

            .modal-confirm .close {
                position: absolute;
                top: -5px;
                right: -5px;
            }

            .modal-confirm .modal-body {
                color: #999;
            }

            .modal-confirm .modal-footer {
                background: #ecf0f1;
                border-color: #e6eaec;
                text-align: right;
                margin: 0 -20px -20px;
                border-radius: 0 0 5px 5px;
            }

            .modal-confirm .btn {
                color: #666;
                border-radius: 4px;
                transition: all 0.4s;
                border: none;
                padding: 8px 20px;
                outline: none !important;
            }

            .modal-confirm .btn-info {
                background: #b0c1c6;
            }

                .modal-confirm .btn-info:hover, .modal-confirm .btn-info:focus {
                    background: #92a9af;
                }

            .modal-confirm .btn-danger {
                background: #f15e5e;
            }

                .modal-confirm .btn-danger:hover, .modal-confirm .btn-danger:focus {
                    background: #ee3535;
                }

            .modal-confirm .modal-footer .btn + .btn {
                margin-left: 10px;
            }

        .trigger-btn {
            display: inline-block;
            margin: 100px auto;
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
            $('#alert_container').append('<div id="alert_div" style="margin: 0;position: fixed;top: 50%;left: 10%;width: 50%;-ms-transform: translateY(-50%);transform: translate(40%, -50%); -webkit-box-shadow: 3px 4px 6px #999; z-index: inherit;" class="alert fade in ' + cssclass + ' text-center"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>  <span>' + message + '</span></div>');
            //$('#alert_container').append('<div id="alert_div" style="margin: 0;position: absolute;top: 50%;left: 10%;-ms-transform: translateY(-50%);transform: translateY(-50%);display: none;width: 80%" class="alert fade in ' + cssclass + ' text-center"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');
        }

        window.closeModalDetalle = function () {
            $('#Detalles').modal('hide');
        }
        function AbrirModalDetalles() {

            document.getElementById('<%=frameDetalle.ClientID%>').src = "CatClienteEcommerceInsert.aspx";
            $("#Detalles").appendTo("body");
            $("#Detalles").modal({ "backdrop": "static" });
            $('#Detalles').modal('show');
        }
        function AbrirModalDetallesUpdate(id) {
            document.getElementById('<%=frameDetalle.ClientID%>').src = "CatClienteEcommerceInsert.aspx?id=" + id;
            $("#Detalles").appendTo("body");
            $("#Detalles").modal({ "backdrop": "static" });
            $('#Detalles').modal('show');
        }

        $(document).ready(function () {
            $('#Detalles').on('hidden.bs.modal', function () {
                location.reload();
            })

            $('#ModalMatriz').on('hidden.bs.modal', function () {
                location.reload();
            });
        });


        function Nuevo() {
            document.getElementById('<%=IFrameMatriz.ClientID%>').src = "PortalKey_Alta.aspx";
            $("#ModalMatriz").appendTo("body");
            $("#ModalMatriz").modal({ "backdrop": "static" });
            $('#ModalMatriz').modal('show');
        }

        function Editar(ID, tipo, Cdi) {
            document.getElementById('<%=IFrameMatriz.ClientID%>').src = "PortalKey_Alta.aspx?Id=" + ID + "&Tipo=" + tipo + "&IdCd=" + Cdi;
            $("#ModalMatriz").appendTo("body");
            $("#ModalMatriz").modal({ "backdrop": "static" });
            $('#ModalMatriz').modal('show');
        }

        function UsuarioClientes(ID, tipo, Cdi) {
            document.getElementById('<%=IFRameUsuCli.ClientID%>').src = "PortalKey_UsuClientes.aspx?Id=" + ID + "&Tipo=" + tipo + "&IdCD=" + Cdi;
            $("#ModalUsuCli").appendTo("body");
            $("#ModalUsuCli").modal({ "backdrop": "static" });
            $('#ModalUsuCli').modal('show');
        }

        function Region(ID, tipo, Cdi) {
            document.getElementById('<%=IFrameRegion.ClientID%>').src = "PortalKey_Region.aspx?Id=" + ID + "&Tipo=" + tipo + "&IdCD=" + Cdi;
            $("#ModalRegion").appendTo("body");
            $("#ModalRegion").modal({ "backdrop": "static" });
            $('#ModalRegion').modal('show');
        }

        function Permisos(ID, tipo, Cdi) {
            document.getElementById('<%=IframePErmisos.ClientID%>').src = "PortalKey_Permisos.aspx?Id=" + ID + "&Tipo=" + tipo + "&IdCD=" + Cdi;
            $("#ModalPermisos").appendTo("body");
            $("#ModalPermisos").appendTo("body");
            $("#ModalPermisos").modal({ "backdrop": "static" });
            $('#ModalPermisos').modal('show');

        }

        window.closeModalDetalleMatriz = function () {
            $('#ModalMatriz').modal('hide');
        }


        function Confirmacion(Id) {
            $("#ModalConfirmacion").appendTo("body");
            $("#ModalConfirmacion").modal({ "backdrop": "static" });
            $('#ModalConfirmacion').modal('show');
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
        <asp:UpdatePanel runat="server" ID="updpanel">
            <ContentTemplate>
                <div class="col-md-12" style="font-family: verdana; font-size: 8pt; margin-top: 5px;" runat="server"
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
                 <div class="col-md-12" style="margin-top: 5px;">
                <div class="panel panel-success" style="margin-top: 5px;">
                    <div class="panel-heading" style="height: 50px;">
                        <div class="col-md-9">
                            <h3 class="panel-title">Portal de Clientes</h3>
                        </div>
                        <div class="col-md-3">

                            <button type="submit" runat="server" class="btn btn-default btn-sm" id="BtnNuevoCliente"
                                onserverclick="btnInsert_ServerClick">
                                <i class="fa fa-plus" aria-hidden="true"></i>&nbsp;Alta Cliente
                            </button>
                            <button type="submit" runat="server" class="btn btn-default btn-sm" id="BtnNuevaMatriz" 
                                onserverclick="BtnNuevo_ServerClick">
                                <i class="fa fa-plus" aria-hidden="true"></i>&nbsp;Alta Matriz
                            </button>
                            <button type="submit" runat="server" class="btn btn-success btn-sm" id="btnreporteexcel"
                                onserverclick="btnExcel_ServerClick">
                                <i class="fa fa-file-excel-o" aria-hidden="true"></i>&nbsp;Excel
                            </button>
                        </div>
                    </div>
                    <div class="panel-body  ">

                        <div class="col-md-12" style="margin-top: 1%;">
                            <dx:BootstrapGridView ID="grdAdmon" ClientInstanceName="grid" runat="server" KeyFieldName="Nombre"
                                Width="100%" AutoGenerateColumns="False">
                                <SettingsPager PageSize="10" NumericButtonCount="4">
                                    <Summary Visible="false" />
                                    <PageSizeItemSettings Visible="false" ShowAllItem="false" />
                                </SettingsPager>
                                <Settings ShowFooter="true" />
                                <Settings ShowHeaderFilterButton="true" />
                                <Columns>
                                    <dx:BootstrapGridViewDataColumn Width="30px" FieldName="Id_Portal" Caption="Id" Visible="false">
                                    </dx:BootstrapGridViewDataColumn>
                                    <dx:BootstrapGridViewDataColumn Width="30px" FieldName="Tipo" Caption="Id" Visible="false">
                                    </dx:BootstrapGridViewDataColumn>
                                    <dx:BootstrapGridViewDataColumn Width="40px" FieldName="id_Cd" Caption="CDi" CssClasses-DataCell="RightText" CssClasses-HeaderCell="centerText" Visible="false">
                                    </dx:BootstrapGridViewDataColumn>
                                    <dx:BootstrapGridViewDataColumn Width="40px" FieldName="clave" Caption="Clave" CssClasses-DataCell="RightText" CssClasses-HeaderCell="centerText">
                                    </dx:BootstrapGridViewDataColumn>
                                    <dx:BootstrapGridViewDataColumn Width="30px" FieldName="Id_Usu" Caption="Rik" Visible="false">
                                    </dx:BootstrapGridViewDataColumn>
                                    <dx:BootstrapGridViewDataColumn Width="80px" FieldName="NombreMatriz" Caption="Matriz">
                                    </dx:BootstrapGridViewDataColumn>
                                    <dx:BootstrapGridViewDataColumn Width="30px" FieldName="NombreTipo" Caption="Tipo Cuenta">
                                    </dx:BootstrapGridViewDataColumn>
                                    <dx:BootstrapGridViewDataColumn Width="30px" FieldName="Correo" Caption="Correo Principal">
                                    </dx:BootstrapGridViewDataColumn>
                                    <dx:BootstrapGridViewDataColumn FieldName="Editar" Width="40px" Caption="Editar">
                                        <DataItemTemplate>
                                            <dx:BootstrapButton runat="server" ID="btnEditar" CssClasses-Icon="fa fa-pencil" OnClick="btnEditar_ServerClick" OnInit="btnEditar_Init"></dx:BootstrapButton>
                                        </DataItemTemplate>
                                    </dx:BootstrapGridViewDataColumn>
                                    <dx:BootstrapGridViewDataColumn FieldName="Region" Width="40px" Caption="Región">
                                        <DataItemTemplate>
                                            <dx:BootstrapButton runat="server" ID="BtnRegion" CssClasses-Icon="fa fa-globe" OnClick="BtnRegion_ServerClick" OnInit="BtnRegion_Init"></dx:BootstrapButton>
                                        </DataItemTemplate>
                                    </dx:BootstrapGridViewDataColumn>
                                    <dx:BootstrapGridViewDataColumn Width="40px"  FieldName="Unidades"  Caption="Usuario/Unidades">
                                        <DataItemTemplate>
                                            <dx:BootstrapButton runat="server" ID="BtnUsuarios" CssClasses-Icon="fa fa-user" OnClick="BtnUsuarios_ServerClick" OnInit="BtnUsuarios_Init"></dx:BootstrapButton>
                                        </DataItemTemplate>
                                    </dx:BootstrapGridViewDataColumn>
                                    <dx:BootstrapGridViewDataColumn Width="40px" FieldName="Permisos" Caption="Permisos">
                                        <DataItemTemplate>
                                            <dx:BootstrapButton runat="server" ID="BtnPermisos" CssClasses-Icon="fa fa-check" OnClick="BtnPermisos_ServerClick" OnInit="BtnPermisos_Init"></dx:BootstrapButton>
                                        </DataItemTemplate>
                                    </dx:BootstrapGridViewDataColumn>
                                    <dx:BootstrapGridViewDataColumn Width="40px" FieldName="Eliminar" Caption="Eliminar">
                                        <DataItemTemplate>
                                            <dx:BootstrapButton runat="server" ID="BtnEliminar" CssClasses-Icon="fa fa-trash-o" OnClick="Btneliminar_ServerClick" OnInit="BtnEliminar_Init"></dx:BootstrapButton>
                                        </DataItemTemplate>
                                    </dx:BootstrapGridViewDataColumn>
                                </Columns>

                            </dx:BootstrapGridView>
                        </div>
                    </div>
                </div>
                     </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnreporteexcel" />
            </Triggers>
        </asp:UpdatePanel>

        <div>
            <div class="messagealert" id="alert_container">
            </div>
        </div>
        <div class="modal" id="Detalles" data-toggle="modal" style="width: 730px !important; height: 350px !important; margin: auto; overflow-y: hidden;"
            role="dialog">
            <div class="modal-content" style="height: 350px !important;">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <div class="col-md-10">
                        <h4 id="h1">Alta/Edicion Cuenta Local 
                        </h4>
                    </div>
                </div>
                <div>
                    <div class="embed-responsive embed-responsive-16by9 ">
                        <iframe class="embed-responsive-item" id="frameDetalle" runat="server" src=""></iframe>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal center" id="ModalMatriz" data-toggle="modal" style="width: 730px !important; height: 350px !important; margin: auto; overflow-y: hidden;">
            <div class="modal-content" style="height: 350px !important;">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <div class="col-xs-10">
                        <h4 id="h1">Alta/Edición Matriz
                        </h4>
                    </div>
                </div>
                <div>
                    <div class="embed-responsive embed-responsive-16by9 ">
                        <iframe class="embed-responsive-item" id="IFrameMatriz" runat="server"></iframe>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal" id="ModalRegion" data-toggle="modal" style="width: 730px !important; height: 550px !important; margin: auto; overflow-y: hidden;">
            <div class="modal-content" style="height: 550px !important;">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <div class="col-xs-10">
                        <h4 id="h1">Alta/Edición Región 
                        </h4>
                    </div>
                </div>
                <div>
                    <div class="embed-responsive embed-responsive-16by9" style="padding-bottom: 450px !important;">
                        <iframe class="embed-responsive-item" id="IFrameRegion" runat="server"></iframe>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal" id="ModalUsuCli" data-toggle="modal" style="width: 930px; height: 550px; margin: auto; overflow-y: hidden;">
            <div class="modal-content" style="height: 550px !important;">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <div class="col-xs-10">
                        <h4 id="h1">Alta/Edición Usuario y Unidades
                        </h4>
                    </div>
                </div>
                <div>
                    <div class="embed-responsive embed-responsive-16by9 " style="padding-bottom: -1px !important;">
                        <iframe class="embed-responsive-item" id="IFRameUsuCli" runat="server"></iframe>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal" id="ModalPermisos" data-toggle="modal" style="width: 1100px; height: 600px; margin: auto; overflow-y: hidden;">
            <div class="modal-content" style="height: 600px;">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <div class="col-xs-10">
                        <h4 id="h1">Permisos
                        </h4>
                    </div>
                </div>
                <div>
                    <div class="embed-responsive embed-responsive-16by9 " style="padding-bottom: -1px !important;">
                        <iframe class="embed-responsive-item" id="IframePErmisos" runat="server"></iframe>
                    </div>
                </div>
            </div>
        </div>
        <div id="ModalConfirmacion" class="modal fade">
            <div class="modal-dialog modal-confirm">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Confirmación</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    </div>
                    <div class="modal-body">
                        <p>¿Desea Eliminar el Usuario del Portal?</p>
                    </div>
                    <div class="modal-footer">
                        <a href="#" class="btn btn-info" data-dismiss="modal">Cancel</a>
                        <button runat="server" id="btnEliminarConfirm" class="btn btn-primary" style="color: white;"
                            onserverclick="BtneliminarConfirmacion_Click">
                            Aceptar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
