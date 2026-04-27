<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pro_CN_Vinculacion.aspx.cs" Inherits="SIANWEB.Pro_CN_Vinculacion"
    MasterPageFile="~/MasterPage/masterpage02Boostrap.master" %>

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
        .modal-body {
            max-height: calc(100%);
            overflow-y: scroll;
        }

        .nav-tabs a, .nav-tabs a:hover, .nav-tabs a:focus {
            outline: 0;
        }

        .dropdown-toggle {
            height: 34px !important;
        }

        .caret {
            margin-top: 10px !important;
        }

        .centerText {
            text-align: center;
        }

        #dropZone {
            padding: 20px;
            margin: -20px;
        }

        .ResultFileName {
            text-overflow: ellipsis;
        }


        .margen {
            margin-left: 10px;
        }

        .RadForm_Outlook.rfdHeading h4 {
            border-bottom: solid 0px #6788be !important;
        }

        form-control {
            width: 100%;
            height: 34px !important;
            padding: 6px 12px !important;
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

        .dxbs-date-edit .dropdown-menu.panel {
            margin-top: 60px;
            padding-top: 0px;
            position: fixed !important;
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

        .modal-body .cmbItemWrap span {
            word-wrap: break-word;
            white-space: normal;
        }

        .dropdown-menu > li > a {
            white-space: pre-wrap !important;
        }
    </style>

    <div class="modal-body" id="Div2">
        <asp:UpdateProgress ID="updateProgress" runat="server">
            <ProgressTemplate>
                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/load.gif" AlternateText="Loading ..."
                        ToolTip="Loading ..." Style="padding: position: fixed; top: 45%; left: 40%;" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="updPanelVinculacion" runat="server">
            <ContentTemplate>
                <div class="col-md-12" style="margin-top: 5px;">
                    <div class="panel panel-success">
                        <div class="panel-heading" style="height: 50px;">
                            <div class="col-md-12">
                                <div class="col-md-10">
                                    <h3 style="margin-top: 5px !important;">Solicitud de
                                        <asp:Label ID="lblTitulo" runat="server" Text="Vinculación" /></h3>
                                </div>
                                <div class="col-md-2">
                                    <button id="btnAceptar" type="button" runat="server" class="btn btn-primary btn-sm" onserverclick="btnAceptar_Click">
                                        Aceptar
                                    </button>
                                </div>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-md-12" style="margin-top: 5px;">
                                <fieldset>
                                    <legend>
                                        <h4>Datos Generales </h4>
                                    </legend>
                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <div class="form-group">
                                            <div class="col-md-2">
                                                <asp:Label ID="Label25" runat="server" Text="Nombre" />
                                            </div>
                                            <div class="col-md-10">
                                                <asp:Label ID="txtNombreEstructura" runat="server" Text="" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <div class="form-group">
                                            <div class="col-md-2">
                                                <asp:Label ID="Label102" runat="server" Text="CD" />
                                            </div>
                                            <div class="col-md-10">
                                                <asp:Label ID="txtSucursalNombre" runat="server" Text="" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <div class="form-group">
                                            <div class="col-md-2">
                                                <asp:Label ID="Label1" runat="server" Text="Fecha" />
                                            </div>
                                            <div class="col-md-10">
                                                <asp:Label ID="txtFechas" runat="server" Text="" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <div class="form-group">
                                            <div class="col-md-2">
                                                <asp:Label ID="Label2" runat="server" Text="Usuario" />
                                            </div>
                                            <div class="col-md-10">
                                                <asp:Label ID="txtUsuario" runat="server" Text="" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <div class="form-group">
                                            <div class="col-md-2">
                                                <asp:Label ID="Label3" runat="server" Text="ACYS" />
                                            </div>
                                            <div class="col-md-10">
                                                <asp:Label ID="txtACYS" runat="server" Text="" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-6" style="padding-left: 0px !important">
                                                    <asp:Label ID="Label4" runat="server" Text="Núm. Cliente" />
                                                </div>
                                                <div class="col-md-6">
                                                    <dx:BootstrapSpinEdit ID="txtClienteSIAN" runat="server" Enabled="True" MaxLength="9" AutoPostBack="true"
                                                        Width="100px" OnValueChanged="txtClienteSIAN_ValueChanged">
                                                        <SpinButtons ShowIncrementButtons="false" ShowLargeIncrementButtons="false" />
                                                    </dx:BootstrapSpinEdit>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label16" runat="server" Text="Terr." />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapComboBox ID="cmbTerritorio" runat="server" CallbackPageSize="25" DropDownStyle="DropDown">
                                                    </dx:BootstrapComboBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <div class="col-md-4" style="padding-left: 0px !important">
                                                    <asp:Label ID="Label5" runat="server" Text="Razón Social" />
                                                </div>
                                                <div class="col-md-8" style="margin-left: -5px;">
                                                    <dx:BootstrapTextBox ID="txtRazonSocial" runat="server" Text="" ReadOnly="true" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>

                            <div class="col-md-12" style="margin-top: 5px;">
                                <fieldset>
                                    <legend>
                                        <h4>Datos fiscales SIAN CENTRAL</h4>
                                    </legend>
                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <div class="col-md-4" style="padding-left: 0px !important">
                                                    <asp:Label ID="Label346" runat="server" Text="Razónes sociales activas" /></td>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:ImageButton ID="ImgBuscarDireccionEntrega" runat="server" ImageUrl="~/Img/find16.png"
                                                        ToolTip="Buscar" ValidationGroup="buscar" Visible="True" OnClick="ImgBuscarDireccionEntrega_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-4" style="padding-left: 0px !important">
                                                    <asp:Label ID="Label66" runat="server" Text="Calle" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapTextBox ID="txtCalle" runat="server" Text="" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label56" runat="server" Text="Número interior" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapTextBox ID="txtNumInterior" runat="server" Text=""/>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label8" runat="server" Text="Número Exterior" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapTextBox ID="txtNumExterior" runat="server" Text=""/>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-4" style="padding-left: 0px !important">
                                                    <asp:Label ID="Label46" runat="server" Text="Colonia" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapTextBox ID="txtColonia" runat="server" Text="" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label9" runat="server" Text="Municipio" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapTextBox ID="txtMunicipio" runat="server" Text="" />
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-4" style="padding-left: 0px !important">
                                                    <asp:Label ID="Label36" runat="server" Text="C.P." />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapTextBox ID="txtCP" runat="server" Text="" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label7" runat="server" Text="Estado" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapTextBox ID="txtEstado" runat="server" Text="" />
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-4" style="padding-left: 0px !important">
                                                    <asp:Label ID="Label26" runat="server" Text="Teléfono(s)" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapTextBox ID="txtTelefonos" runat="server" Text="" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label10" runat="server" Text="FAX" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapTextBox ID="txtFAX" runat="server" Text="" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label11" runat="server" Text="RFC" />
                                                </div>
                                                <div class="col-md-8">
                                                    <dx:BootstrapTextBox ID="txtRFC" runat="server" Text="" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <div class="col-md-12" style="margin-top: 5px;">
                                    <fieldset>
                                        <div class="col-md-12" style="margin-top: 5px;">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <div class="col-md-4" style="padding-left: 0px !important">
                                                        <asp:Label ID="Lab6" runat="server" Text="Remisión Cta Nac" />
                                                    </div>
                                                    <div class="col-md-8">
                                                        <dx:BootstrapComboBox ID="cmbRemision_Cta_Nac" runat="server" CallbackPageSize="25" DropDownStyle="DropDown">
                                                        </dx:BootstrapComboBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label12" runat="server" Text="Asesor del Cliente" />
                                                    </div>
                                                    <div class="col-md-8">
                                                        <dx:BootstrapComboBox ID="cmbAsesorId" runat="server" CallbackPageSize="25" DropDownStyle="DropDown">
                                                        </dx:BootstrapComboBox>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-md-12" style="margin-top: 5px;">
                                            <asp:Label ID="Label13" runat="server" Text="Comentarios" /></td>
                                        </div>
                                        <div class="col-md-12" style="margin-top: 5px;">
                                            <asp:TextBox ID="txtComentarios" runat="server" Text="" TextMode="MultiLine" Width="600px" Height="50px" /></td>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

         <div id="ModalDireccion" class="modal" data-toggle="modal" style="z-index: 3000 !important;"
            role="dialog">
            <div class="modal-dialog" role="document" style="height: 560px !important;  width: 80%;""> 
                <div class="modal-content" style="height: 550px !important;">
                    <div class="modal-header" style="color: #F9F9F9 !important;   background-color: #59b2f1 !important;">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button> 
                       Solicitud Cuenta Nacionales
                    </div>
                    <div class="modal-body" style="padding: 5px !important;" id="Div10">
                        <div class="embed-responsive embed-responsive-16by9" style="padding-bottom: 45% !Important;"> 
                            <iframe class="embed-responsive-item" id="iFrameDireccion" runat="server" src=""></iframe>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="messagealert" id="alert_container">
        </div>
    </div>  

    <script type="text/javascript">

        function AbrirBuscarDireccionEntrega(idMatriz) {
            
            document.getElementById('<%=iFrameDireccion.ClientID%>').src = "Ventana_Buscar.aspx?CN_IdMatriz=" + idMatriz, "AbrirVentana_BuscarPrecio";
            $("#ModalDireccion").appendTo("body");
            $("#ModalDireccion").modal({ "backdrop": "static" });
            $('#ModalDireccion').modal('show');
        }

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
</asp:Content>
