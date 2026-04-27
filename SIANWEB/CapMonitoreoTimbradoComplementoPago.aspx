<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.Master"
    AutoEventWireup="true" CodeBehind="CapMonitoreoTimbradoComplementoPago.aspx.cs"
    Inherits="SIANWEB.CapMonitoreoTimbradoComplementoPago" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:radajaxmanager id="RAM1" runat="server" onajaxrequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                     <telerik:AjaxUpdatedControl ControlID="rgPago" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="rgPago">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgPago" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:radajaxmanager>
    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server" skin="Default">
    </telerik:radajaxloadingpanel>
    <div id="divPrincipal" runat="server">
     <telerik:RadToolBar ID="rtb1" runat="server" Width="100%" dir="rtl"  OnButtonClick="rtb1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" /> 
                <telerik:RadToolBarButton CommandName="excel" Value="excel" CssClass="Excel" ToolTip="Exportar a Excel"
                    ImageUrl="~/Imagenes/blank.png" />
            </Items>
        </telerik:RadToolBar>
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
                    <asp:Label ID="Label6" runat="server" Text="Centro de distribución"></asp:Label>
                </td>
                <td width="150px" style="font-weight: bold">
                    <telerik:radcombobox id="CmbCentro" maxheight="300px" runat="server" onselectedindexchanged="CmbCentro_SelectedIndexChanged1"
                        width="150px" autopostback="True">
                    </telerik:radcombobox>
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt">
            <tr>
                <td>
                </td>
                <table>
                    <tr>
                        <td width="80">
                        </td>
                        <td width="100">
                        </td>
                        <td width="10">
                        </td>
                        <td width="80">
                        </td>
                        <td width="100">
                        </td>
                        <td width="10">
                        </td>
                        <td width="45">
                        </td>
                        <td width="45">
                        </td>
                        <td width="100">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Fecha inicial" />
                        </td>
                        <td>
                            <telerik:raddatepicker id="txtFecha1" runat="server" culture="es-MX" width="100px">
                                    <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                        ViewSelectorText="x">
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy">
                                        </FastNavigationSettings>
                                    </Calendar>
                                    <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                </telerik:raddatepicker>
                        </td>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Fecha final" />
                        </td>
                        <td>
                            <telerik:raddatepicker id="txtFecha2" runat="server" culture="es-MX" width="100px">
                                    <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                        ViewSelectorText="x">
                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                            TodayButtonCaption="Hoy">
                                        </FastNavigationSettings>
                                    </Calendar>
                                    <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                    </DateInput>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                </telerik:raddatepicker>
                        </td>
                        <td>
                            <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/Img/find16.png" OnClick="ImageButton1_Click"
                                ToolTip="Buscar" />
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <asp:HiddenField ID="HF_ClvPag" runat="server" />
                        </td>
                    </tr>
                </table>
                <td>
                    <telerik:radsplitter id="RadSplitter1" runat="server" height="550px" resizemode="AdjacentPane"
                        resizewithbrowserwindow="true" bordersize="0" width="950">
                                <telerik:RadPane ID="RadPane1" runat="server" Height="500px" width="900px" OnClientResized="onResize"
                                    BorderStyle="None" >

                    <telerik:RadGrid ID="rgPago" runat="server" AutoGenerateColumns="False" GridLines="None"
                        EnableLinqExpressions="False" PageSize="15" AllowPaging="True" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                        OnNeedDataSource="rg_NeedDataSource" OnPageIndexChanged="rg_PageIndexChanged"
                        OnItemCommand="rg_ItemCommand" OnItemDataBound="rgPago_ItemDataBound">
                        <ClientSettings>
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                        <MasterTableView ClientDataKeyNames="Id_Pag">                            
                            <Columns> 
                            <telerik:GridBoundColumn DataField="Id_ShowLog" HeaderText="Id_ShowLog" visible="false" UniqueName="Id_ShowLog">
                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Id_Pag" HeaderText="Folio" UniqueName="Id_Pag">
                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn> 
                                 <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" visible="false" UniqueName="Id_Emp">
                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn> 
                                 <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Id_Cd" visible="false" UniqueName="Id_Cd">
                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>                                      
                                  <telerik:GridBoundColumn DataField="Fecha" HeaderText="Fecha" UniqueName="Fecha"
                                    DataFormatString="{0:dd/MM/yyyy}">
                                    <HeaderStyle Width="120px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn DataField="Observaciones" HeaderText="Observaciones" UniqueName="Observaciones">
                                    <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                    <ItemStyle HorizontalAlign="left" />
                                </telerik:GridBoundColumn>                               
                                <telerik:GridBoundColumn DataField="Atendido" HeaderText="Atendido" visible="false" UniqueName="Atendido">
                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn> 
                                <telerik:GridTemplateColumn HeaderText="Atender" AllowFiltering="false">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="Timbrar" runat="server" ImageUrl="~/Imagenes/blank.png"
                                            CssClass="edit" ToolTip="Timbrar" CommandName="Timbrar" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                        <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                            FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Cantidad de registros"
                            PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Change page: {4} Página <strong>{0}</strong> de <strong>{1}</strong>, registros <strong>{2}</strong> al <strong>{3}</strong> de <strong>{5}</strong >."
                            ShowPagerText="True" PageButtonCount="3" />
                    </telerik:RadGrid>

                       </telerik:RadPane>
                            </telerik:radsplitter>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="clientSideIsPostBack" runat="server" Value="N" />
        <asp:HiddenField ID="HiddenHeight" runat="server" />
        <asp:HiddenField ID="HiddenRebind" runat="server" />
    </div>
    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function onResize(sender, eventArgs) {
                var postback = document.getElementById("<%=clientSideIsPostBack.ClientID %>").value;
                document.getElementById("<%= HiddenHeight.ClientID %>").value = document.documentElement.clientHeight;
                ajaxManager.ajaxRequest('panel');
            }

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
            function CloseAndRebind() {
                GetRadWindow().Close();
                GetRadWindow().BrowserWindow.refreshGrid(null);
            }

            function refreshGrid() {
                //debugger;
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
            }


            function abrirArchivo(pagina) {
                var opciones = "toolbar=yes, location=yes, directories=yes, status=yes, menubar=yes, scrollbars=yes, resizable=yes, width=508, height=365, top=100, left=140";
                window.open(pagina, '', opciones);
            }

        </script>
    </telerik:radcodeblock>
</asp:Content>
