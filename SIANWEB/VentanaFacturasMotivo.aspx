<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VentanaFacturasMotivo.aspx.cs" Inherits="SIANWEB.VentanaFacturasMotivo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 410px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="All" />
        <telerik:RadSkinManager ID="RadSkinManager1" runat="server" PersistenceMode="Session"
            Skin="Outlook">
        </telerik:RadSkinManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RadGrid1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnBuscar1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        
        <table id="Table2" style="font-family: verdana; font-size: 8pt; width: 800px;" runat="server">
            <tr>               
                <td colspan = "3" style="text-align: center;" class="style1">
                    <asp:Label ID="Label2" runat="server" Text="¿Qué deseas hacer con esta factura?" Font-Bold="True" Font-Size="Small"></asp:Label>
                    <br><br>
                </td>                
            </tr>
            <tr>
                <td colspan="3"><b>Atención:</b> Si después de la baja se requiere generar otra factura para el mismo cliente y producto,<br /> se recomienda crearla primero para registrar el folio correspondiente.<br><br></td>
                </tr>
            <tr>
                <td><b>Motivo de baja</b></td>
                <td><b>No. Factura (Nuevo)</b></td>     
                <td>&nbsp;</td>
            </tr>
                <tr>
                <td>
                     <asp:DropDownList ID="cmbMotivo" runat="server" style="width: 400px;">
 </asp:DropDownList>
               
                </td>
                <td>
                    <asp:TextBox id="txtFolio" TextMode="Number" Columns="20" step="1"  runat="server" />
                </td>
                <td>
                <asp:Button ID="btnBaja" runat="server" Text="Baja Factura" onclick="btnBaja_Click" style="width: 120px;" />
                </td>
            </tr>
                <tr>
                <td colspan="3">
                    <asp:Label runat="server" ID="lblMensajeBaja" ForeColor="Red"></asp:Label>
                    <hr></td>
                </tr>
                <tr>
                <td colspan="2">
                    Botón para accesar a la ventana de refacturación
                </td>
                <td>
                    <asp:Button ID="btnRefacturar" runat="server" Text="Refacturar" onclick="btnRefacturar_Click" style="width: 120px;"/>
                 </td>   
            </tr>
            <tr>
                <td colspan="3"><hr></td>
                </tr>
                <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                    <td class="style1">
                    <asp:Button ID="btnCancelar" runat="server" Text="Salir" onclick="btnCancelar_Click" style="width: 120px;" />
                    </td>
            </tr>
        </table>
       
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">
                function GetRadWindow() {
                    var oWindow = null;
                    if (window.radWindow)
                        oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog       
                    else if (window.frameElement.radWindow)
                        oWindow = window.frameElement.radWindow; //IE (and Moz as well)       
                    return oWindow;
                }
                //Cierra la venata actual y regresa el foco a la ventana padre
                function CloseWindow() {
                    GetRadWindow().Close();
                }
                //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
                function CloseAndRebind(param) {
                    GetRadWindow().Close();
                    GetRadWindow().BrowserWindow.SeleccinarOpcion(param);
                }
                
            </script>
        </telerik:RadCodeBlock>
    </div>
    </form>
</body>
</html>
