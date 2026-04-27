<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CapCancelarNCreditoVencido.aspx.cs" Inherits="SIANWEB.CapCancelarNCreditoVencido" MasterPageFile="~/MasterPage/MasterPage03.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/ComboMultipleColumns.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .MyImageButton
        {
            cursor: hand;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
  
            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var continuarAccion = true;
                var habilitaValidacion = false;
                var button = args.get_item();
                //habilitar/deshabilitar validators
                if (button.get_value() == 'save')
                    habilitaValidacion = true;
                else {
                    habilitaValidacion = false;
                }
             
                //if (tabSeleccionada == 'Datos generales')
                switch (button.get_value()) {
                    case 'new':
                     
                        break;
                    case 'save':
                        //select tab datos generales
                       
                        break;
                }
                if (continuarAccion == true) {
                    GetRadWindow().BrowserWindow.ActivarBanderaRebind();
                }
                args.set_cancel(!continuarAccion);
            }
            
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow)
                    oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog      
                else if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow; //IE (and Moz as well)      
                return oWindow;
            }

            function CloseWindow(mensaje) {
                //debugger;
                var cerrarWindow = radalert(mensaje, 330, 150, tituloMensajes);
                cerrarWindow.add_close(
                            function () {
                                Close();
                            });
            }
            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function Close() {
                GetRadWindow().Close();
            }
            function CloseAndRebind() {
                //debugger;
                GetRadWindow().Close();
                GetRadWindow().BrowserWindow.refreshGrid();
            }
            //Hace un refresh completo de la ventana padre = F5
            function RefreshParentPage() {
                GetRadWindow().BrowserWindow.location.reload();
            }
       
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest"
        EnablePageHeadUpdate="False">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadToolBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div runat="server" id="divPrincipal">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnClientButtonClicking="ToolBar_ClientClick"
            OnButtonClick="RadToolBar1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" CausesValidation="false" />
                <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                    ImageUrl="Imagenes/blank.png" ValidationGroup="guardar" />
            </Items>
        </telerik:RadToolBar>
         <asp:Label ID="lblMensaje" runat="server"></asp:Label>

            <table style="padding-top:5%">
                  <tr>
                      <td width="200"> </td>
                      <td width="150"> </td>
                      <td width="100"> </td>
                      <td width="100"></td>
                      <td width="50"> </td>
                      <td width="150"></td>
                  </tr>
                   <tr>
                        <td></td>
                       <td>
                           <asp:Label ID="label2" runat="server" Text="Nota de Crédito"></asp:Label>
                       </td>
                       <td colspan="6">
                           <asp:Label ID="lblDocumento" runat="server" Text="###"></asp:Label>
                       </td>
                   </tr>
                <tr>
                     <td></td>
                    <td>
                        <asp:Label ID="label1" runat="server" Text="Motivo"></asp:Label>
                    </td>
                    <td colspan="6">
                          <telerik:RadComboBox ID="cmbMotivo" runat="server" AutoPostBack="True" ChangeTextOnKeyBoardNavigation="true"
                            DataTextField="Motivo" OnSelectedIndexChanged="cmbMotivo_SelectedIndexChanged" DataValueField="Id" Filter="Contains" MarkFirstMatch="true"
                            Width="220px">
                        </telerik:RadComboBox>
                    </td>
                </tr>
                 <tr id="trOtroMotivo" style="display:none" runat="server">
                      <td></td>
                     <td>
                         <asp:Label ID="label4" runat="server" Text="Otro"></asp:Label>
                     </td>
                     <td colspan="6">
                         <asp:TextBox ID="txtOtroMotivo" runat="server" TextMode="MultiLine" Width="200"></asp:TextBox>
                     </td>
                 </tr>
            </table>
      
    </div>
</asp:Content>
