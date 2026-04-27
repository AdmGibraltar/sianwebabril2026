<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.Master"
    AutoEventWireup="true" CodeBehind="ProIncompleto_Picking.aspx.cs" Inherits="SIANWEB.ProIncompleto_Picking" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:radajaxmanager id="RAM1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rtb1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                         />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ImageButton1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblMensaje"  />
                    <telerik:AjaxUpdatedControl ControlID="rgPedido" LoadingPanelID="RadAjaxLoadingPanel1"
                         />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:radajaxmanager>
    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server" skin="Default">
    </telerik:radajaxloadingpanel>
    <telerik:radtoolbar id="rtb1" runat="server" width="100%" dir="rtl" onbuttonclick="rtb1_ButtonClick">
        <Items>
            <telerik:RadToolBarButton Width="20px" Enabled="False" />
            <telerik:RadToolBarButton CommandName="mail" Value="mail" CssClass="mail" ToolTip="Correo"
                ImageUrl="~/Imagenes/blank.png" />
            <telerik:RadToolBarButton CommandName="print" Value="print" CssClass="print" ToolTip="Imprimir"
                ImageUrl="~/Imagenes/blank.png" />
            <telerik:RadToolBarButton CommandName="delete" Value="delete" CssClass="delete" ToolTip="Eliminar"
                ImageUrl="~/Imagenes/blank.png" />
            <telerik:RadToolBarButton CommandName="undo" Value="undo" CssClass="undo" ToolTip="Regresar"
                ImageUrl="~/Imagenes/blank.png" />
            <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                ImageUrl="~/Imagenes/blank.png" ValidationGroup="guardar" />
            <telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Nuevo" CssClass="new"
                ImageUrl="~/Imagenes/blank.png" />
        </Items>
    </telerik:radtoolbar>
    <div id="divPrincipal" runat="server">
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    &nbsp; &nbsp;
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Producto"></asp:Label>
                            </td>
                            <td colspan="6">
                                <telerik:radnumerictextbox id="txtProducto" runat="server" width="70px" minvalue="1"
                                    readonly="true" MaxValue="999999999999999">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:radnumerictextbox>
                                <telerik:radtextbox onpaste="return false" id="txtProductoNombre" runat="server"
                                    width="250px" readonly="true">
                                </telerik:radtextbox>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="Label7" runat="server" Text="Presentación" />
                            </td>
                            <td>
                                <telerik:radnumerictextbox id="TxtPresentacion" runat="server" width="70px" 
                                    readonly="true">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:radnumerictextbox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="Unidades" />
                            </td>
                            <td>
                                <telerik:radtextbox id="TxtUnidades" runat="server" width="70px"
                                    readonly="true">
                                </telerik:radtextbox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>


                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Inventario" />
                            </td>
                            <td>
                                <telerik:radnumerictextbox id="txtInventario" runat="server" width="70px" 
                                    maxlength="9" readonly="true">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:radnumerictextbox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Asignado" />
                            </td>
                            <td>
                                <telerik:radnumerictextbox id="txtAsignado" runat="server" width="70px"
                                    maxlength="9" readonly="true">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:radnumerictextbox>
                            </td>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="Disponible" />
                            </td>
                            <td>
                                <telerik:radnumerictextbox id="txtDisponible" runat="server" width="70px" 
                                    maxlength="9" readonly="true">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:radnumerictextbox>
                                &nbsp;&nbsp;Confirmar<asp:ImageButton ID="ImbConfirmar" runat="server" ImageUrl="~/Imagenes/check2.png" Width="16px" Height="16px" OnClick="ImbConfirmar_Click" />
                            </td>
                        </tr>


                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="No Encontrado" />
                            </td>
                            <td>
                                <telerik:radnumerictextbox id="TxtNoEncontrado" runat="server" width="70px" minvalue="0"
                                    maxlength="9">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:radnumerictextbox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="No Conforme" />
                            </td>
                            <td>
                                <telerik:radnumerictextbox id="TxtNoConforme" runat="server" width="70px" minvalue="0"
                                    maxlength="9">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:radnumerictextbox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>




                        </table>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="HiddenRebind" runat="server" Value="false" />
    <asp:HiddenField ID="HF_Ped" runat="server" />
    <asp:HiddenField ID="HF_Guardar" runat="server" Value="true" />
    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function CloseAlert(mensaje) {
                var cerrarWindow = radalert(mensaje, 330, 150);
                cerrarWindow.add_close(
                    function () {
                        CloseWindow();
                    });
            }
            //Cierra la venata actual y regresa el foco a la ventana padre
            function CloseWindow() {
                GetRadWindow().Close();
            }
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow)
                    oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog       
                else if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow; //IE (and Moz as well)       
                return oWindow;
            }
           </script>
    </telerik:radcodeblock>
</asp:Content>
