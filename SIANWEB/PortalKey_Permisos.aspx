<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/masterpage02Boostrap.Master" AutoEventWireup="true" CodeBehind="PortalKey_Permisos.aspx.cs" Inherits="SIANWEB.PortalKey_Permisos" %>

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
      .modal-body{
          overflow-y:scroll;
          height: 100%;
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
                <div class="col-xs-12" style="margin-top: 5px">
                    <div class="col-xs-6">
                        <div class="form-group">
                            <div class="col-xs-4">
                                <asp:Label ID="Label2" runat="server" Text="Matriz" />
                            </div>
                            <div class="col-xs-8">
                                <dx:BootstrapTextBox ID="TxtMatriz" runat="server" ReadOnly="true" Enabled="false">
                                </dx:BootstrapTextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-6">
                        <div class="form-group">
                            <div class="col-xs-4">
                                <asp:Label ID="Label4" runat="server" Text="Correo Principal" />
                            </div>
                            <div class="col-xs-8">
                                <dx:BootstrapTextBox ID="TxtCorreo" runat="server" ReadOnly="true" Enabled="false">
                                </dx:BootstrapTextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xs-12" style="margin-top: 5px">
                    <div class="col-xs-6">
                        <div class="form-group">
                            <div class="col-xs-4">
                                <asp:Label ID="LblNombre" runat="server" Text="Usuario" />
                            </div>
                            <div class="col-xs-8">
                                <dx:BootstrapComboBox ID="BCBUsuario" runat="server" AutoPostBack="true" OnSelectedIndexChanged="BCBUsuario_SelectedIndexChanged">
                                </dx:BootstrapComboBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-6">
                        <div class="form-group">
                            <div class="col-xs-4">
                                <asp:Label ID="Label1" runat="server" Text="Correo" />
                            </div>
                            <div class="col-xs-8">
                                <dx:BootstrapTextBox ID="TxtCorreoPermisos" runat="server" ReadOnly="true" Enabled="false">
                                </dx:BootstrapTextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xs-12" style="margin-top: 5px;">
                      <div class="col-xs-10"></div>
                      <div class="col-xs-2">
                    <button type="submit" runat="server" class="btn btn-primary btn-sm" id="BtnGuardar"
                        onserverclick="BtnGuardar_ServerClick">
                        <i></i>&nbsp;Guardar
                    </button>
                          </div>
                </div>
                <div class="col-xs-12" style="margin-top: 5px;">
                    <div class="col-xs-12" style="margin-top: 5px;">
                        <dx:BootstrapGridView ID="GrdPermisos" ClientInstanceName="gridCliente" runat="server"
                            KeyFieldName="Id_Region;id_cte;id_Cd"
                            Width="100%" AutoGenerateColumns="False" OnDataBound="GrdPermisos_DataBound">
                              <Settings ShowGroupPanel="false" />
                            <SettingsBehavior AutoExpandAllGroups="true" AllowSort="false" AllowSelectByRowClick="true"   /> 
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>

                            <Columns>
                                <dx:BootstrapGridViewDataColumn Width="30px" FieldName="NombreRegion" Caption="Región" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText" GroupIndex="0">
                                </dx:BootstrapGridViewDataColumn>
                                <dx:BootstrapGridViewDataColumn Width="40px" FieldName="id_Cd" Caption="CDI" CssClasses-DataCell="RightText" CssClasses-HeaderCell="centerText" Visible="false">
                                </dx:BootstrapGridViewDataColumn>
                                <dx:BootstrapGridViewDataColumn Width="40px" FieldName="Sucursal" Caption="Sucursal" CssClasses-DataCell="RightText" CssClasses-HeaderCell="centerText">
                                </dx:BootstrapGridViewDataColumn>
                                <dx:BootstrapGridViewDataColumn Width="40px" FieldName="Id_Region" Caption="Nombre" CssClasses-DataCell="RightText" CssClasses-HeaderCell="centerText" Visible="false">
                                </dx:BootstrapGridViewDataColumn>
                                <dx:BootstrapGridViewDataColumn Width="30px" FieldName="unidad" Caption="Unidad" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                </dx:BootstrapGridViewDataColumn>
                                <dx:BootstrapGridViewDataColumn Width="30px" FieldName="id_cte" Caption="# Cliente" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                </dx:BootstrapGridViewDataColumn>
                                <dx:BootstrapGridViewDataColumn Width="130px" FieldName="nombreCliente" Caption="Nombre" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                </dx:BootstrapGridViewDataColumn>
                                <dx:BootstrapGridViewDataColumn Width="40px" FieldName="Id_Direccion" Caption="DireccionEntrega" CssClasses-DataCell="RightText" CssClasses-HeaderCell="centerText" Visible="false">
                                </dx:BootstrapGridViewDataColumn>
                                <dx:BootstrapGridViewDataColumn Width="130px" FieldName="DireccionCompleta" Caption="Direción Entrega" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                                </dx:BootstrapGridViewDataColumn>
                                <dx:BootstrapGridViewCheckColumn FieldName="permiso" Width="30px" Caption="Permiso" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText"></dx:BootstrapGridViewCheckColumn>
                                <dx:BootstrapGridViewCommandColumn Name="permiso"  ShowSelectCheckbox="True" Width="20px" Caption="Activar/Desactivar Permiso"></dx:BootstrapGridViewCommandColumn> 
                            </Columns>
                        </dx:BootstrapGridView>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>





</asp:Content>

