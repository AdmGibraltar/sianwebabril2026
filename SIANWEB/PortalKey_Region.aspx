<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/masterpage02Boostrap.Master" AutoEventWireup="true" CodeBehind="PortalKey_Region.aspx.cs" Inherits="SIANWEB.PortalKey_Region" %>

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
                              <asp:Label ID="Label4" runat="server" Text="Correo" />
                          </div>
                          <div class="col-xs-8">
                              <dx:BootstrapTextBox ID="TxtCorreo" runat="server" ReadOnly="true" Enabled="false">
                              </dx:BootstrapTextBox>
                          </div>
                      </div>
                  </div>
              </div>
              <div class="col-xs-12" style="margin-top: 3%">
                  <div class="col-xs-6">
                      <div class="form-group">
                          <div class="col-xs-4">
                              <asp:Label ID="Label1" runat="server" Text="Región" />
                          </div>
                          <div class="col-xs-8">
                              <dx:BootstrapTextBox ID="TxtRegion" runat="server">
                              </dx:BootstrapTextBox>
                          </div>
                      </div>
                  </div>
                  <div class="col-xs-4">
                  </div>
                  <div class="col-xs-2">
                      <dx:BootstrapButton ID="BtnGuardar" ValidationGroup="VG1" runat="server" Text="Guardar" SettingsBootstrap-RenderOption="Primary"
                          OnClick="BtnGuardar_ServerClick">
                      </dx:BootstrapButton>
                  </div>
              </div>
              <div class="col-xs-12" style="margin-top: 5px;">
                  <div class="col-xs-2"></div>
                  <div class="col-xs-8">
                      <dx:BootstrapGridView ID="GrdRegion" ClientInstanceName="gridCliente" runat="server"
                          KeyFieldName="Id_Region"
                          Width="100%" AutoGenerateColumns="False">
                          <SettingsPager PageSize="10" NumericButtonCount="4">
                              <Summary Visible="false" />
                              <PageSizeItemSettings Visible="false" ShowAllItem="false" />
                          </SettingsPager>
                          <Settings ShowFooter="true" />
                          <Columns>
                              <dx:BootstrapGridViewDataColumn Width="30px" FieldName="Id_Region" Caption="Id Región" CssClasses-DataCell="RightText" CssClasses-HeaderCell="centerText">
                              </dx:BootstrapGridViewDataColumn>
                              <dx:BootstrapGridViewDataColumn Width="100px" FieldName="NombreRegion" Caption="Región" CssClasses-DataCell="LeftText" CssClasses-HeaderCell="centerText">
                              </dx:BootstrapGridViewDataColumn>
                              <dx:BootstrapGridViewDateColumn Width="40px" Caption="Editar">

                                  <DataItemTemplate>
                                      <dx:BootstrapButton ID="BtnEditar" type="button" CssClasses-Icon="fa fa-pencil" runat="server" OnClick="BtnEditar_ServerClick" OnInit="BtnEditar_Init">
                                      </dx:BootstrapButton>
                                  </DataItemTemplate>
                              </dx:BootstrapGridViewDateColumn>
                              <dx:BootstrapGridViewDateColumn Width="40px" Caption="Eliminar">
                                  <DataItemTemplate>
                                      <dx:BootstrapButton ID="Btneliminar" type="button" CssClasses-Icon="fa fa-trash-o" runat="server" OnClick="Btneliminar_ServerClick" OnInit="Btneliminar_Init">
                                      </dx:BootstrapButton>
                                  </DataItemTemplate>
                              </dx:BootstrapGridViewDateColumn>
                          </Columns>
                      </dx:BootstrapGridView>
                  </div>
              </div>
          </ContentTemplate>
          <Triggers>
              <asp:AsyncPostBackTrigger ControlID="BtnGuardar" />
          </Triggers>
      </asp:UpdatePanel>
      <div> 
          <div class="messagealert" id="alert_container">
          </div>
      </div>
  </div>
</asp:Content>
