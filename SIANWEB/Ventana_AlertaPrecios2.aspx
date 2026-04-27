<%@ Page Title="Alerta" Language="C#" AutoEventWireup="true" CodeBehind="Ventana_AlertaPrecios2.aspx.cs"
    Inherits="SIANWEB.Ventana_AlertaPrecios2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
</head> 
 <style type="text/css">
     .borderbottom
            {
	            color: #000099;
	            background-color: Transparent;
	            padding: 0px;
	            border: 0px solid #000099;
	            font-size:X-Small;
	            font-weight:bold;
	            -moz-border-radius: 0px 0px 0px 0px;
	            
            }
      
     .style1
     {
         height: 56px;
     }
      
    </style>

<body>
    <form id="form1" runat="server">
      <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
      </telerik:RadScriptManager>
      <telerik:RadWindowManager ID="RWM1" runat="server" Skin="Office2007">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False"
        OnAjaxRequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PnlLogin" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="PnlLogin">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PnlLogin" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Office2007">
    </telerik:RadAjaxLoadingPanel>
    <asp:Panel ID="PnlLogin" runat="server" >
        <table style="font-family: Verdana; font-size: 8pt">

            <tr>
            <td colspan="2">
             &nbsp; &nbsp;
                <asp:Label ID="LblMensaje" runat="server"></asp:Label>
            </td>
            </tr>
    
       
    
      <tr>
                <td align="center">
                
                </td>
                <td align="left">
                     <asp:Label ID="LblMensaje1" runat="server" Text="Los precios de venta de los siguientes productos están fuera del rango de precios mínimos y máximos marcados en el Convenio de PAAA Especiales, favor de corregir para poder avanzar. " Width="500px" Font-Bold="True"></asp:Label>
                </td>
            </tr>
                <tr>
            <td>
            &nbsp;
            </td>
            </tr>
                  
            <tr>
            <td>
            &nbsp;
            </td>
            </tr> 
                       <tr>
                <td align="center">
                    &nbsp;
                </td>
                <td align="left"> 
            <telerik:RadGrid ID="rgBitacora" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="7"
            AllowFilteringByColumn="False" MasterTableView-NoMasterRecordsText="No se encontraron registros."
            OnNeedDataSource="rgBitacora_NeedDataSource" OnPageIndexChanged="rgBitacora_PageIndexChanged"
            GridLines="None" >
            <MasterTableView >
                <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                <Columns>
                 <telerik:GridBoundColumn DataField="Id_Prd" UniqueName="Id_Prd" 
                               HeaderText="Código de producto"  >             
                                       <HeaderStyle Width="200px" />
                                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="PCD_PrecioVentaConvenio" HeaderText="P. Venta Ingresado" UniqueName="PCD_PrecioVentaConvenio">
                                        <HeaderStyle Width="300px" />
                     </telerik:GridBoundColumn>
                      <telerik:GridBoundColumn DataField="PCD_PrecioVtaMin" HeaderText="P. Venta Min" UniqueName="PCD_PrecioVtaMin">
                                        <HeaderStyle Width="300px" />
                     </telerik:GridBoundColumn>
                       <telerik:GridBoundColumn DataField="PCD_PrecioVtaMax" HeaderText="P. Venta Max" UniqueName="PCD_PrecioVtaMax">
                                        <HeaderStyle Width="300px" />
                     </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Id_PC" UniqueName="Id_PC" 
                               HeaderText="Convenio Key"  >             
                                       <HeaderStyle Width="200px" />
                                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridBoundColumn>

                     <telerik:GridBoundColumn DataField="PC_Nombre" HeaderText="Nombre Convenio" UniqueName="PC_Nombre">
                                        <HeaderStyle Width="350px" />
                    </telerik:GridBoundColumn>
        
                </Columns>
            </MasterTableView>
          
        </telerik:RadGrid>
                    &nbsp; 

                </td>
            </tr>
                <tr>
                <td align="center" >
                    &nbsp;
                </td>
                <td >
                    <asp:Label ID="Label5" runat="server" Text ="Se requiere alguna aclaración:"></asp:Label><br /> 
                      <asp:Label ID="lblmensaje5" runat="server" Text ="Favor de contactar al área de Precios ó Revisar la"></asp:Label>
                      <asp:Button ID="buttonDescargarpdf3" runat="server" CssClass="borderbottom" 
                        Font-Bold="True" Font-Overline="False" Font-Underline="True" 
                        ForeColor="#0033CC" OnClick="btnDescargarPDF_Click" 
                        Text="Política de Convenios de PAAA Especiales." 
                        ToolTip="Política de Convenios de PAAA Especiales." Visible="true" 
                         />
                </td>
            </tr>
            
            <tr>
                <td align="center" colspan="2">
                     <asp:Button ID="btnAceptar" runat="server" OnClick="btnAceptar_Click" 
                        Text="Aceptar" />
                </td> 
            </tr>
        </table>
      
    </asp:Panel>
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
        <script type="text/javascript">
         
            function CloseWindowA(mensaje) {
                var cerrarWindow = radalert(mensaje, 330, 150);
                cerrarWindow.add_close(
                            function () {
                                CloseAndRebind();
                            });
            }
            
            function AlertaFocus(mensaje, control) {
                var oWnd = radalert(mensaje, 340, 150);
                //oWnd.add_close(foco(control));
                oWnd.add_close(function () {
                    var target = $find(control);
                    if (target != null) {
                        target.focus();
                    }
                });
            }
 
            function GetRadWindow() {
                var oWindow = null;
                if (window.RadWindow) {
                    oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog     
                }
                else if (window.frameElement.RadWindow) {
                    oWindow = window.frameElement.RadWindow;  //IE (and Moz as well)  
                }
                if (oWindow == null) {
                    if (window.radWindow) {
                        oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog     
                    }
                    else if (window.frameElement.radWindow) {
                        oWindow = window.frameElement.radWindow;  //IE (and Moz as well)  
                    }
                }
                return oWindow;
            } 
            window.closeModalalerta = function () {
                $('#ModalAlerta').modal('hide');
            };
            function CloseAndRebind(param) {
                window.parent.closeModalalerta();
            };
            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function RefreshParentPage() {
                GetRadWindow().BrowserWindow.location.reload();
            }
       
        </script>
    </telerik:RadCodeBlock>
    </form>
</body>
<script>
    function AbrirArchivoPDF(WebURL) {
        var oWnd = radopen(WebURL, "AbrirVentana_ImpresionPDFFactura");
        oWnd.set_showOnTopWhenMaximized(false);
        oWnd.center();
    }
</script>
</html>