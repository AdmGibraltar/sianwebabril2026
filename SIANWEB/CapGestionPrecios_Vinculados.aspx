<%@ Page Title="Clientes vinculados" Language="C#" MasterPageFile="~/MasterPage/MasterPage03.Master"
    AutoEventWireup="true" CodeBehind="CapGestionPrecios_Vinculados.aspx.cs" Inherits="SIANWEB.CapGestionPrecios_Vinculados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            height: 22px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <div>
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">
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
                //Cierra la venata actual y regresa el foco a la ventana padre
                function CloseWindow() {
                    //debugger;
                    GetRadWindow().Close();
                }

                //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
                function CloseAndRebind() {
                    
                    GetRadWindow().Close();
                     
                }
                function CloseWindowA(mensaje) {
                    //debugger;
                    var cerrarWindow = radalert(mensaje, 330, 150);
                    cerrarWindow.add_close(
                            function () {
                                GetRadWindow().Close();
                            });
                }

                function onRequestStart(sender, args) {
                    if (args.get_eventTarget().indexOf("ctl00$CPH$rtb1") != -1)
                        args.set_enableAjax(false);
                }

            </script>
        </telerik:RadCodeBlock>
        <telerik:RadAjaxManager ID="RAM1" runat="server" OnAjaxRequest="RAM1_AjaxRequest" ClientEvents-OnRequestStart="onRequestStart">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RAM1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="rg1" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rtb1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="100%" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="CmbCentro">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="100%" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="ImageButton1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rg1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="lblMensaje" UpdatePanelHeight="" />
                        <telerik:AjaxUpdatedControl ControlID="rg1" LoadingPanelID="RadAjaxLoadingPanel1"
                            UpdatePanelHeight="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
        </telerik:RadAjaxLoadingPanel>
        <div runat="server" id="divPrincipal">
            <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl" OnButtonClick="rtb1_ButtonClick" >
                <Items>
                    <telerik:RadToolBarButton Width="20px" Enabled="False" />
                    <telerik:RadToolBarButton CommandName="print" Value="print" ToolTip="Exportar a excel" CssClass="excel"
                        ImageUrl="~/Imagenes/blank.png" />
                </Items>
            </telerik:RadToolBar>
            <table id="TblEncabezado" 
                style="font-family: verdana; font-size: 8pt; visibility: hidden;" runat="server"
                width="99%">
                <tr>
                    <td>
                        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                        <asp:HiddenField ID="hiddenId" runat="server" />
                        <asp:HiddenField runat="server" ID="HFId_PC"/>
                         <asp:HiddenField runat="server" ID="HFId_Cd"/>
                    <%--    <asp:HiddenField runat="server" ID="HFCapUsuario"/>
                        <asp:HiddenField runat="server" ID="HFSol_Unique"/>--%>
                    </td>
                    <td style="text-align: right" width="150px">
                        <asp:Label ID="lblCentro" runat="server" Text="Centro de distribución"></asp:Label>
                    </td>
                    <td width="150px" style="font-weight: bold">
                        <telerik:RadComboBox ID="CmbCentro" MaxHeight="300px" runat="server"
                            Width="150px" AutoPostBack="True">
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
            <table style="font-family: Verdana; font-size: 8pt">
                <tr>
                    <td>
                    </td>
                    <td>
                        <table style="font-family: Verdana; font-size: 8pt">
                         
                            <tr>
                                <td>
                                    <asp:Label ID="Label8" runat="server" Text="Convenio Key:" Font-Bold="True"></asp:Label>
                                    </td>
                                <td>
                                    <asp:Label ID="LblPC_NoConvenioKey" runat="server"></asp:Label>
                                    </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                    </td>
                                <td>
                                    &nbsp;
                                    </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                    </td>
                                <td>
                                    &nbsp;
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
                                    <asp:Label ID="Label5" runat="server" Text="Convenio proveedor:" Font-Bold="True"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblPC_NoConvenio" runat="server"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
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
                                    <asp:Label ID="Label6" runat="server" Text="Nombre de convenio:" Font-Bold="True"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblPC_Nombre" runat="server"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
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
                                    <asp:Label ID="Label7" runat="server" Text="Categoría:" Font-Bold="True"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblId_CatStr" runat="server"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;                                    
                                </td>
                            </tr>
                        </table>
                        <br />

                    </td>
                </tr>
            </table>

            <table style="font-family: Verdana; font-size: 8pt">
                <tr>
                    <td>
                     <telerik:RadSplitter ID="RadSplitter4" runat="server" Height="350px" BorderSize="0">
                            <telerik:RadPane ID="RadPane3" runat="server" Height="350px" Width="850px" BorderStyle="None">
                        <telerik:RadGrid ID="rgVinculados" runat="server" AutoGenerateColumns="False" GridLines="None"
                            PageSize="15" AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                               OnNeedDataSource="rgVinculados_NeedDataSource" CellSpacing="0" OnItemDataBound="rgVinculados_ItemDataBound"
                               OnItemCommand="rgVinculados_ItemCommand">
                                    <MasterTableView>
                                 <CommandItemSettings ShowRefreshButton="false" />
                                <Columns>
                                            <telerik:GridBoundColumn DataField="Id_Sol" UniqueName="Id_Sol" Display="false">
                                    </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_PC" UniqueName="Id_PC" Display="false">
                                    </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id_Cte" HeaderText="No. Cliente" UniqueName="Id_Cte">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" Width="90px"  />
                                    </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Sol_CteNombre" HeaderText="Nombre de cliente" UniqueName="Sol_CteNombre">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" Width="200px"  />
                                    </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="SolTer_Nombre" HeaderText="Territorio" UniqueName="SolTer_Nombre" display= "false">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" Width="200px"  />
                                    </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Sol_UsuFinal" HeaderText="Usuario final" UniqueName="Sol_UsuFinal" display= "false">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" Width="90px"  />
                                    </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CDI" HeaderText="Concesionario" UniqueName="CDI">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" Width="150px"  />
                                    </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Sol_UNombre" HeaderText="Usuario que solicita" UniqueName="Sol_UNombre">
                                        <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" Width="180px" />
                                    </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Sol_NombreAtendio" HeaderText="Usuario que atiende" UniqueName="Sol_NombreAtendio">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" Width="180px" />
                                            </telerik:GridBoundColumn>
                                            
                                            <telerik:GridBoundColumn DataField="OrigenSolicitudString" HeaderText="Orígen de vinculación"
                                                UniqueName="OrigenSolicitudString">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" Width="180px" />
                                            </telerik:GridBoundColumn>
                                            <%--<telerik:GridButtonColumn CommandName="Cancelar" HeaderText="Eliminar" ConfirmDialogType="RadWindow"
                                                ConfirmText="¿Desea ELIMINAR este cliente? " ConfirmDialogHeight="150px" ConfirmDialogWidth="350px"
                                                Text="Cancelar" UniqueName="Cancelar" Display="True" ButtonType="ImageButton"
                                            ImageUrl="~/Imagenes/blank.png" ButtonCssClass="baja">
                                                <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="50px" />
                                        </telerik:GridButtonColumn>--%>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" />
                            </MasterTableView>
                            <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                                PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} &nbsp;Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                                ShowPagerText="True" PageButtonCount="3" />
                            <ClientSettings>
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                        </telerik:RadGrid>
                        </telerik:RadPane>
                          </telerik:RadSplitter>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
