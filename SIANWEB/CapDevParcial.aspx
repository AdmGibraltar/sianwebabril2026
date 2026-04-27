<%@ Page Title="Devolución parcial" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.master"
    AutoEventWireup="true" CodeBehind="CapDevParcial.aspx.cs" Inherits="SIANWEB.CapDevParcial" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="CapaNegocios" %>
<%@ Import Namespace="CapaEntidad" %>
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
   <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RAM1" runat="server" EnablePageHeadUpdate="False" OnAjaxRequest="RAM1_AjaxRequest1">       
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
            <telerik:AjaxSetting AjaxControlID="cmbTipo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxloadingPanel1">
                    </telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtFactura">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1">
                    </telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbFactura">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1">
                    </telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbTerritorio">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1">
                    </telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbRepresentante">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1">
                    </telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgDevParcial">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgDevParcial" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cmbCentro">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>      
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cmbCliente">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cmbTipoMovimiento">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>     
        </telerik:RadAjaxManager> 
           <telerik:RadWindowManager ID="RWMVentanas" runat="server" />
            <Windows>
                <telerik:RadWindow ID="AbrirVentana_ImpresionPDFNotaCreditoCN" runat="server" Opacity="100"
                Behaviors="Move,Close" VisibleStatusbar="False" Width="840px" Height="700px"
                Animation="Fade" KeepInScreenBounds="True" Overlay="True" Title="Nota de crédito CN"
                Modal="True" OnClientClose="CerrarWindow_Event" ReloadOnShow="true">
            </telerik:RadWindow>
                </Windows>
        </telerik:RadWindowManager>
    <div id="divPrincipal" runat="server">
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" dir="rtl" OnClientButtonClicking="ToolBar_ClientClick"
            OnButtonClick="RadToolBar1_ButtonClick">
            <Items>
                <telerik:RadToolBarButton Width="20px" Enabled="False" />
                <telerik:RadToolBarButton CommandName="save" Value="save" ToolTip="Guardar" CssClass="save"
                    ImageUrl="Imagenes/blank.png" ValidationGroup="pestaniaDetalles" />
                <telerik:RadToolBarButton CommandName="new" Value="new" ToolTip="Nuevo" CssClass="new"
                    ImageUrl="~/Imagenes/blank.png" Visible="false" />
            </Items>
        </telerik:RadToolBar>
        <table style="font-family: verdana; font-size: 8pt; height: 100%" width="100%">
            <tr>
                <td width="10">
                    &nbsp;
                </td>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>                  
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" OnTabClick="RadTabStrip1_TabClick" OnClientTabSelecting="ClientTabSelecting">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="Datos &lt;u&gt;g&lt;/u&gt;enerales" AccessKey="G"
                                PageViewID="RadPageViewDGenerales" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="De&lt;u&gt;t&lt;/u&gt;alles" AccessKey="T" PageViewID="RadPageViewDetalles"
                                OnFocus="_ValidarFechaEnPeriodo" OnKeyPress="handleClickEvent">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" AccessKey="N" Text="Facturación de C&lt;u&gt;N&lt;/u&gt;"
                                PageViewID="rpvCuentaNacional" Visible="false" Value="CuentaNacional">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" BorderStyle="Solid"
                        BorderWidth="1px" ScrollBars="Hidden">
                        <telerik:RadPageView ID="RadPageViewDGenerales" runat="server" heigth="300px">
                            <telerik:RadSplitter ID="RadSplitter2" runat="server" Height="300px" ResizeMode="AdjacentPane"
                                ResizeWithBrowserWindow="true" BorderSize="0" Width="100%">
                                <telerik:RadPane ID="RadPane2" runat="server" Height="300px" OnClientResized="onResize"
                                    BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Scrolling="None">
                                    <table>
                                        <tr>
                                            <td colspan="2">&nbsp;
                                            </td>
                                            <td width="50"></td>
                                            <td width="60">&nbsp;&nbsp;
                                            </td>
                                            <td colspan="1" width="30"></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td>&nbsp;&nbsp;
                                            </td>
                                            <td></td>
                                            <td width="10">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:Label ID="LblTipo" runat="server" Text="Tipo"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <telerik:RadComboBox ID="cmbTipo" runat="server" AutoPostBack="True" ChangeTextOnKeyBoardNavigation="true"
                                                    DataTextField="Descripcion" DataValueField="Id" Enabled="true" Filter="Contains"
                                                    LoadingMessage="Cargando..." MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                                                    Width="150px">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td style="width: 50px; text-align: center">
                                                                    <asp:Label ID="LabelID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                                        Width="50px" />
                                                                </td>
                                                                <td style="width: 200px; text-align: left">
                                                                    <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td></td>
                                            <td>
                                                <asp:Label ID="LblNumero" runat="server" Text="Número"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtNumero" runat="server" MaxLength="9" MinValue="0"
                                                    Width="50px">
                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                    <ClientEvents OnKeyPress="SoloNumerico" />
                                                </telerik:RadNumericTextBox>
                                            </td>
                                            <td></td>
                                            <td>&nbsp;
                                            </td>
                                            <td colspan="2">
                                                <asp:Label ID="LblFecha" runat="server" Text="Fecha"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="txtFecha" runat="server" Width="155px" Enabled="False">
                                                    <Calendar ID="Calendar" runat="server">
                                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                            TodayButtonCaption="Hoy" />
                                                    </Calendar>
                                                    <DateInput ID="DateInput" runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                                        MaxLength="10">
                                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                    </DateInput>
                                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelHora" runat="server" Text="Hora"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtHora" runat="server" MaxLength="50" Width="70px" Enabled="False">
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="LblFactura" runat="server" Text="Factura"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtFactura" runat="server" MaxLength="9" MinValue="1"
                                                    Width="50px" AutoPostBack="true" OnTextChanged="txtClave_TextChanged">
                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                    <ClientEvents OnBlur="txtFactura_OnBlur" OnFocus="_ValidarFechaEnPeriodo" />
                                                </telerik:RadNumericTextBox>
                                            </td>
                                            <td colspan="7">
                                                <telerik:RadComboBox ID="cmbFactura" runat="server" Width="246px" OnSelectedIndexChanged="cmbCliente_SelectedIndexChanged"
                                                    OnClientSelectedIndexChanged="cmb_ClientSelectedIndexChanged" Filter="Contains"
                                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" OnClientBlur="Producto_Blur"
                                                    EnableLoadOnDemand="true" HighlightTemplatedItems="true" LoadingMessage="Cargando..."
                                                    EnableAutomaticLoadOnDemand="True" EnableVirtualScrolling="True" ItemsPerRequest="10"
                                                    ShowMoreResultsBox="True" MaxHeight="250px" OnClientDropDownOpening="Client_Focus"
                                                    OnFocus="_ValidarFechaEnPeriodo">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td style="width: 50px; text-align: center">
                                                                    <asp:Label ID="Label1" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                                                </td>
                                                                <td style="width: 200px; text-align: left">
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                    <Localization ShowMoreFormatString="Elemento &lt;b&gt;1&lt;/b&gt;-&lt;b&gt;{0}&lt;/b&gt; de &lt;b&gt;{1}&lt;/b&gt;"
                                                        NoMatches="No hay coincidencias" />
                                                </telerik:RadComboBox>
                                                <telerik:RadTextBox ID="txtFactura2" runat="server" Width="246px" ReadOnly="True"
                                                    Enabled="False" Visible="false">
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>&nbsp;&nbsp;
                                            </td>
                                            <td colspan="2">
                                                <asp:Label ID="LblFechafac" runat="server" Text="Fecha de facturación"></asp:Label>
                                            </td>
                                            <td colspan="1">
                                                <telerik:RadDatePicker ID="txtFechafac" runat="server" Width="155px">
                                                    <Calendar ID="Calendar1" runat="server">
                                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                            TodayButtonCaption="Hoy" />
                                                    </Calendar>
                                                    <DateInput ID="DateInput1" runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                                        MaxLength="10">
                                                        <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                                                    </DateInput>
                                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="LblTipoMov" runat="server" Text="Tipo de movimiento"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtTipoMov" runat="server" MaxLength="9" MinValue="0"
                                                    Width="50px">
                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                    <ClientEvents OnBlur="txtTipoMov_OnBlur" OnKeyPress="SoloNumerico" />
                                                </telerik:RadNumericTextBox>
                                            </td>
                                            <td colspan="8">
                                                <telerik:RadComboBox ID="cmbTipoMovimento" runat="server" AutoPostBack="True" ChangeTextOnKeyBoardNavigation="true"
                                                    DataTextField="Descripcion" DataValueField="Id" Enabled="true" Filter="Contains"
                                                    LoadingMessage="Cargando..." MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                                                    OnClientSelectedIndexChanged="cmbTipoMovimento_ClientSelectedIndexChanged" Width="251px">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td style="width: 50px; text-align: center">
                                                                    <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                                        Width="50px" />
                                                                </td>
                                                                <td style="width: 200px; text-align: left">
                                                                    <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td colspan="1">&nbsp;&nbsp;
                                            </td>
                                            <td>&nbsp;&nbsp;
                                            </td>

                                            <td rowspan="6">
                                                <div id="paraNC"
                                                    style="border: 1px solid #89aee5; padding-top: 10px; height: 126px;">
                                                    <b>Datos Fiscales</b>
                                                    <table>
                                                        <tr>
                                                            <td>Uso CFDI:
                                               <telerik:RadComboBox ID="cmbNCUsoCFDI" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                   EnableLoadOnDemand="true" Filter="Contains" MarkFirstMatch="true" Width="400px"
                                                   LoadingMessage="Cargando...">
                                               </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Forma de pago:
                                                <telerik:RadComboBox ID="cmbNCFormaPago" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                    EnableLoadOnDemand="true" Filter="Contains" MarkFirstMatch="true" Width="400px"
                                                    LoadingMessage="Cargando...">
                                                </telerik:RadComboBox>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Metodo de pago:
                                                <telerik:RadComboBox ID="cmbNCMetodoPago" runat="server" ChangeTextOnKeyBoardNavigation="true"
                                                    EnableLoadOnDemand="true" Filter="Contains" MarkFirstMatch="true" Width="400px"
                                                    LoadingMessage="Cargando...">
                                                </telerik:RadComboBox>

                                                            </td>
                                                        </tr>

                                                    </table>
                                                </div>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="LblCliente" runat="server" Text="Cliente"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtCliente" runat="server" MaxLength="9" MinValue="1"
                                                    Width="50px" OnTextChanged="txtCliente_TextChanged" AutoPostBack="true">
                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                    <ClientEvents OnBlur="txtCliente_OnBlur" OnKeyPress="SoloNumerico" />
                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                </telerik:RadNumericTextBox>
                                            </td>
                                            <td colspan="8">
                                                <telerik:RadTextBox ID="txtClienteNombre" runat="server" Width="246px" ReadOnly="True"
                                                    Enabled="False">
                                                </telerik:RadTextBox>
                                            </td>
                                            <td colspan="1">&nbsp;&nbsp;
                                            </td>
                                            <td>&nbsp;&nbsp;
                                            </td>
                                            <td>&nbsp;&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="LblTerritorio" runat="server" Text="Territorio"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtTerritorio" runat="server" MaxLength="9" MinValue="0"
                                                    Width="50px">
                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                    <ClientEvents OnBlur="txtTerritorio_OnBlur" OnKeyPress="SoloNumerico" />
                                                </telerik:RadNumericTextBox>
                                            </td>
                                            <td colspan="8">
                                                <telerik:RadComboBox ID="cmbTerritorio" runat="server" AutoPostBack="True" ChangeTextOnKeyBoardNavigation="true"
                                                    DataTextField="Descripcion" DataValueField="Id" Enabled="true" Filter="Contains"
                                                    LoadingMessage="Cargando..." MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                                                    OnClientSelectedIndexChanged="cmbTerritorio_ClientSelectedIndexChanged" Width="251px">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td style="width: 50px; text-align: center">
                                                                    <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                                        Width="50px" />
                                                                </td>
                                                                <td style="width: 200px; text-align: left">
                                                                    <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td>&nbsp;&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="LblRepres" runat="server" Text="Representante"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtRepresentante" runat="server" MaxLength="9" MinValue="0"
                                                    Width="50px">
                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                    <ClientEvents OnBlur="txtRepresentante_OnBlur" OnKeyPress="SoloNumerico" />
                                                </telerik:RadNumericTextBox>
                                            </td>
                                            <td colspan="8">
                                                <telerik:RadComboBox ID="cmbRepresentante" runat="server" AutoPostBack="True" ChangeTextOnKeyBoardNavigation="true"
                                                    DataTextField="Descripcion" DataValueField="Id" Enabled="true" Filter="Contains"
                                                    LoadingMessage="Cargando..." MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                                                    OnClientSelectedIndexChanged="cmbRepresentante_ClientSelectedIndexChanged" Width="250px">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td style="width: 50px; text-align: center">
                                                                    <asp:Label ID="Label7" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>'
                                                                        Width="50px" />
                                                                </td>
                                                                <td style="width: 200px; text-align: left">
                                                                    <asp:Label ID="Label8" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="LblNota" runat="server" Text="Nta."></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtNota" runat="server" MaxLength="9" MinValue="0"
                                                    Width="90px">
                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                    <ClientEvents OnKeyPress="SoloNumerico" />
                                                </telerik:RadNumericTextBox>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="LblDescuento" runat="server" Text="Descuento"></asp:Label>
                                            </td>
                                            <td colspan="2">
                                                <telerik:RadNumericTextBox ID="txtDescuento" runat="server" MaxLength="9" MinValue="0"
                                                    Width="100px">
                                                    <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                    <ClientEvents OnKeyPress="SoloNumericoYPunto" />
                                                </telerik:RadNumericTextBox>%
                                            </td>
                                            <td colspan="9">
                                                <telerik:RadTextBox ID="txtDesc" runat="server" Width="300px">
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="LblDescuento2" runat="server" Text="Descuento"></asp:Label>
                                            </td>
                                            <td colspan="2">
                                                <telerik:RadNumericTextBox ID="txtDescuento2" runat="server" Width="100px" MaxLength="9"
                                                    MinValue="0">
                                                    <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                    <ClientEvents OnKeyPress="SoloNumericoYPunto" />
                                                </telerik:RadNumericTextBox>%
                                            </td>
                                            <td colspan="9">
                                                <telerik:RadTextBox ID="txtDesc2" runat="server" Width="300px">
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="LblNotas" runat="server" Text="Notas"></asp:Label>
                                            </td>
                                            <td colspan="12" rowspan="2">
                                                <telerik:RadTextBox ID="txtNotas" runat="server" MaxLength="250" TextMode="MultiLine"
                                                    Width="533px">
                                                    <ClientEvents OnKeyPress="SoloAlfanumerico" />
                                                </telerik:RadTextBox>
                                            </td>
                                            <td rowspan="2">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;&nbsp;
                                            </td>
                                            <td>&nbsp;&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageViewDetalles" runat="server" heigth="300px">
                            <telerik:RadSplitter ID="RadSplitter1" runat="server" Height="300px" ResizeMode="AdjacentPane"
                                ResizeWithBrowserWindow="true" BorderSize="0" Width="100%">
                                <telerik:RadPane ID="RadPane1" runat="server" Height="300px" OnClientResized="onResize" BorderStyle="None">
                                    <telerik:RadGrid ID="rgDevParcial" runat="server" GridLines="None" PageSize="15"
                                        MasterTableView-NoMasterRecordsText="No se encontraron registros." AutoGenerateColumns="False"
                                        AllowPaging="false" HeaderStyle-HorizontalAlign="Center" OnNeedDataSource="rgDevParcial_NeedDataSource"
                                        OnPageIndexChanged="rgDevParcial_PageIndexChanged" OnItemCommand="rgDevParcial_ItemCommand"
                                        OnItemDataBound="rgDevParcial_ItemDataBound" Enabled="true">
                                        <MasterTableView>
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="Id_Fac" HeaderText="Id_Fac" UniqueName="Id_Fac"
                                                    Display="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Id_FacDet" HeaderText="Id_FacDet" UniqueName="Id_FacDet"
                                                    Display="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Id_Ter" HeaderText="Núm." UniqueName="Id_Ter">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle Width="70px" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Prd_Agrupador" HeaderText="Prd_Agrupador" UniqueName="Prd_Agrupador"
                                                    Display="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Territorio1" HeaderText="Territorio" UniqueName="Territorio1">
                                                    <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Id_Prod" HeaderText="Id_Prod" UniqueName="Id_Prod"
                                                    Display="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Id_Prod" HeaderText="Núm." UniqueName="Id_Prod">
                                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Descripcion1" HeaderText="Producto" UniqueName="Descripcion1">
                                                    <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Present1" HeaderText="Presen." UniqueName="Present1">
                                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Cantidad1" HeaderText="Cantidad" UniqueName="Cantidad1">
                                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CantDevuelta" HeaderText="CantDevueltaOld" UniqueName="CantDevueltaOld"
                                                    Display="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Cant.Dev." DataField="CantDevuelta" UniqueName="CantDevuelta">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <telerik:RadNumericTextBox ID="NumCantDevuelta" runat="server" Width="50px" MaxLength="9"
                                                            MinValue="0" Text='<%# Eval("CantDevuelta") %>' OnTextChanged="NumCantDevuelta_TextChanged"
                                                            AutoPostBack="true">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                        </telerik:RadNumericTextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Precio1" HeaderText="Precio" UniqueName="Precio1">
                                                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPrecio" runat="server" Text='<%# Bind("Precio1", "{0:N2}") %>' />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="Importe1" HeaderText="Importe" UniqueName="Importe1">
                                                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblImporte" runat="server" Text='<%# Bind("Importe1", "{0:N2}") %>' />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Dev. de artículo" DataField="Devuelto" UniqueName="Devuelto">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckDevuelto" runat="server" Checked='<%# Eval("Devuelto") %>' OnCheckedChanged="ckDevuelto_CheckedChanged"
                                                            AutoPostBack="true" />
                                                        <asp:Label ID="Label9" runat="server" Text='<%# Eval("Id_FacDet") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                            FirstPageToolTip="Primera página" LastPageToolTip="Última página" PageSizeLabelText="Tama&amp;ntilde;o de p&amp;aacute;gina:"
                                            PrevPageToolTip="Página anterior" NextPageToolTip="Página siguiente" PagerTextFormat="Cambiar página: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                            ShowPagerText="True" PageButtonCount="3" />
                                    </telerik:RadGrid>
                                    <%--  </asp:Panel>--%>
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="rpvCuentaNacional" runat="server">
                            <telerik:RadSplitter ID="RadSplitter10" runat="server" Height="450px" ResizeMode="AdjacentPane"
                                ResizeWithBrowserWindow="true" BorderSize="0" Width="100%">
                                <telerik:RadPane runat="server" ID="RCuentaNacional">
                                    <table>
                                        <tr>
                                            <td width="120">
                                                <asp:Label ID="LblClienteNacional" runat="server" Text="Cliente"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="txtClienteNacional" runat="server" AutoPostBack="true" MaxLength="9"
                                                    MinValue="0" OnTextChanged="txtClienteNacional_TextChanged">
                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                    <ClientEvents OnFocus="_PreValidarFechaEnPeriodo" OnKeyPress="handleClickEvent" />
                                                </telerik:RadNumericTextBox>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtClienteNacionalNombre" runat="server" ReadOnly="True" Width="295px">
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
                                                <%--                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtClienteNacional"
                                                    Display="Dynamic" ErrorMessage="*Requerido" ForeColor="Red" ValidationGroup="guardar">
                                                </asp:RequiredFieldValidator>--%>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="ImgBuscarClienteNacional" runat="server" ImageUrl="~/Img/find16.png" OnClick="ImgBuscarClienteNacional_Click"
                                                    ToolTip="Buscar" ValidationGroup="buscar" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" colspan="8">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="LblClienteNacionalCalle" runat="server" Text="Calle"></asp:Label></td>
                                                        <td>
                                                            <telerik:RadTextBox ID="TxtClienteNacionalCalle" runat="server" ReadOnly="true"></telerik:RadTextBox>
                                                            <telerik:RadTextBox ID="txtClienteNacionalFiscal" runat="server" Visible="false"></telerik:RadTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="LblClienteNacionalNoExterior" runat="server" Text="No. Exterior"></asp:Label></td>
                                                        <td>
                                                            <telerik:RadTextBox ID="TxtClienteNacionalNoExterior" runat="server" ReadOnly="true"></telerik:RadTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="LblClienteNacionalColonia" runat="server" Text="Colonia"></asp:Label></td>
                                                        <td>
                                                            <telerik:RadTextBox ID="TxtClienteNacionalColonia" runat="server" ReadOnly="true"></telerik:RadTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="LblClienteNacionalMunicipio" runat="server" Text="Municipio"></asp:Label></td>
                                                        <td>
                                                            <telerik:RadTextBox ID="TxtClienteNacionalMunicipio" runat="server" ReadOnly="true"></telerik:RadTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="LblClienteNacionalEstado" runat="server" Text="Estado"></asp:Label></td>
                                                        <td>
                                                            <telerik:RadTextBox ID="TxtClienteNacionalEstado" runat="server" ReadOnly="true"></telerik:RadTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="LblClienteNacionalCp" runat="server" Text="C.P."></asp:Label></td>
                                                        <td>
                                                            <telerik:RadTextBox ID="TxtClienteNacionalCp" runat="server" ReadOnly="true"></telerik:RadTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="LblClienteNacionalRfc" runat="server" Text="RFC"></asp:Label></td>
                                                        <td>
                                                            <telerik:RadTextBox ID="TxtClienteNacionalRfc" runat="server" ReadOnly="true"></telerik:RadTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="LblClienteNacionalAdenda" runat="server" Text="Addenda"></asp:Label></td>
                                                        <td>
                                                            <telerik:RadTextBox ID="TxtClienteNacionalAdenda" runat="server" ReadOnly="true"></telerik:RadTextBox></td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td valign="top">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <telerik:RadTextBox ID="TxtClienteNacionalConsignar1" runat="server"></telerik:RadTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <telerik:RadTextBox ID="TxtClienteNacionalConsignar2" runat="server"></telerik:RadTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <telerik:RadTextBox ID="TxtClienteNacionalConsignar3" runat="server"></telerik:RadTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <telerik:RadTextBox ID="TxtClienteNacionalConsignar4" runat="server"></telerik:RadTextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <telerik:RadTextBox ID="TxtClienteNacionalConsignar5" runat="server"></telerik:RadTextBox></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="LblClienteNacionEmail" runat="server" Text="Email"></asp:Label></td>
                                                        <td>
                                                            <telerik:RadTextBox ID="TxtClienteNacionalEmail" runat="server"></telerik:RadTextBox></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                    <table width="99%">
                        <tr>
                            <td align="right" width="350">&nbsp;
                            </td>
                            <td align="right">&nbsp;
                            </td>
                            <td align="right">
                                &nbsp;
                            </td>
                            <td align="right" width="90">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                &nbsp;
                            </td>
                            <td align="right">
                                <asp:Label ID="Label10" runat="server" Text="Importe"></asp:Label>
                            </td>
                            <td align="right" width="125">
                                <telerik:RadNumericTextBox ID="txtImporte" runat="server" MinValue="0" MaxLength="9"
                                    CssClass="AlignRight">
                                    <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="SoloNumericoYPunto" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td align="right">
                                <asp:Label ID="LblSubtotal" runat="server" Text="Subtotal"></asp:Label>
                            </td>
                            <td width="125">
                                <telerik:RadNumericTextBox ID="txtSubtotal" runat="server" MinValue="0" MaxLength="9"
                                    CssClass="AlignRight">
                                    <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="SoloNumericoYPunto" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                &nbsp;
                            </td>
                            <td align="right" colspan="3">
                                <asp:Label ID="LblIVA" runat="server" Text="I.V.A."></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtIVA" runat="server" MinValue="0" MaxLength="9"
                                    CssClass="AlignRight">
                                    <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="SoloNumericoYPunto" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                &nbsp;
                            </td>
                            <td align="right" colspan="3">
                                <asp:Label ID="LblTotal" runat="server" Text="Total"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtTotal" runat="server" MinValue="0" MaxLength="9"
                                    CssClass="AlignRight">
                                    <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                    <ClientEvents OnKeyPress="SoloNumericoYPunto" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HiddenField ID="HiddenHeight" runat="server" />
                                <asp:HiddenField ID="clientSideIsPostBack" runat="server" Value="N" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">


            function _PreValidarFechaEnPeriodo() {
                //debugger;

                _ValidarFechaEnPeriodo();

            }

            function popup(cn) {
                var oWnd;
                if (cn) {
                    oWnd = radopen("Ventana_Buscar.aspx?cn=true", "AbrirVentana_Buscar");
                }
                else {
                    oWnd = radopen("Ventana_Buscar.aspx", "AbrirVentana_Buscar");
                }
                oWnd.center();
            }

            function popup_CC(clienteSIAN) {
                var oWnd;
                if (clienteSIAN) {
                    oWnd = radopen("Ventana_Buscar.aspx?ClienteSIAN=" + clienteSIAN, "AbrirVentana_Buscar");
                }
                else {
                    oWnd = radopen("Ventana_Buscar.aspx", "AbrirVentana_Buscar");
                }
                oWnd.center();
            }


            function onResize(sender, eventArgs) {
                var postback = document.getElementById("<%=clientSideIsPostBack.ClientID %>").value;
                //                if (postback != 'N') {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                document.getElementById("<%= HiddenHeight.ClientID %>").value = document.documentElement.clientHeight;
                ajaxManager.ajaxRequest('panel');
//                }
//                else {
//                    document.getElementById("<%=clientSideIsPostBack.ClientID %>").value = 'Y';
                //                }
            }
            function ClientTabSelecting(sender, args) {
                if ('<%= FacturaEnable %>' == '1') {
                    continuarAccion = _ValidarFechaEnPeriodo();
                }
                else {
                    continuarAccion = true;
                }
                args.set_cancel(!continuarAccion);
            }
            function Client_Focus() {
                //debugger;               
                var combo = $find("<%= cmbFactura.ClientID %>");
                combo.clearSelection();
                var txt = $find("<%= txtFactura.ClientID %>")
                txt.text = "";
            }
            function Producto_Blur() {
                var combo = $find("<%= cmbFactura.ClientID %>");
                if (combo.get_value() == "") {
                    if (combo.get_items().get_count() > 0) {
                        combo.get_items().getItem(0).select()
                    }
                }
            }
            function txtFactura_OnBlur(sender, args) {
                if ('<%= FacturaEnable %>' == '1') {
                    OnBlur(sender, $find('<%= cmbFactura.ClientID %>'));
                }
            }
            function txt_OnBlur(sender, args) {
                if ('<%= FacturaEnable %>' == '1') {
                    OnBlur(sender, $find('<%= cmbFactura.ClientID %>'));
                }
            }
            function cmbFactura_ClientSelectedIndexChanged(sender, eventArgs) {
                if ('<%= FacturaEnable %>' == '1') {
                    ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtFactura.ClientID %>'));
                }
            }
            function cmb_ClientSelectedIndexChanged(sender, eventArgs) {
                var txt = $find('<%= txtFactura.ClientID %>');
                ClientSelectedIndexChanged(eventArgs.get_item(), txt);
            }
            function txtTipoMov_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbTipoMovimento.ClientID %>'));
            }
            function cmbTipoMovimento_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtTipoMov.ClientID %>'));
            }
            function txtCliente_OnBlur(sender, args) {
            }
            function cmbCliente_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtCliente.ClientID %>'));
            }
            function txtTerritorio_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbTerritorio.ClientID %>'));
            }
            function cmbTerritorio_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtTerritorio.ClientID %>'));
            }
            function txtRepresentante_OnBlur(sender, args) {
                OnBlur(sender, $find('<%= cmbRepresentante.ClientID %>'));
            }
            function cmbRepresentante_ClientSelectedIndexChanged(sender, eventArgs) {
                ClientSelectedIndexChanged(eventArgs.get_item(), $find('<%= txtRepresentante.ClientID %>'));
            }
            function ObtenerControlFecha() {
                var txtFecha = $find('<%= txtFecha.ClientID %>');
                return txtFecha._dateInput;
            }
            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {
                //debugger;
                var continuarAccion = true;
                var habilitaValidacion = false;
                var button = args.get_item();
                //habilitar/deshabilitar validators
                switch (button.get_value()) {
                    case 'new':
                        LimpiarControles();
                        break;
                    case 'delete':
                        continuarAccion = Confirma();
                        break;
                    case 'save':
                        continuarAccion = _ValidarFechaEnPeriodo();
                        break;
                }
                args.set_cancel(!continuarAccion);
            }
            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de formulario  
            //--------------------------------------------------------------------------------------------------
            function LimpiarControles() {
                LimpiarTextBox($find('<%= txtTipoMov.ClientID %>'));
                LimpiarTextBox($find('<%= txtCliente.ClientID %>'));
                LimpiarTextBox($find('<%= txtTerritorio.ClientID %>'));
                LimpiarTextBox($find('<%= txtRepresentante.ClientID %>'));
                LimpiarTextBox($find('<%= txtNotas.ClientID %>'));
                LimpiarTextBox($find('<%= txtDesc.ClientID %>'));
                LimpiarTextBox($find('<%= txtDesc2.ClientID %>'));
                LimpiarTextBox($find('<%= txtNota.ClientID %>'));
                LimpiarTextBox($find('<%= txtDescuento.ClientID %>'));
                LimpiarTextBox($find('<%= txtDescuento2.ClientID %>'));
                LimpiarTextBox($find('<%= txtImporte.ClientID %>'));
                LimpiarTextBox($find('<%= txtSubtotal.ClientID %>'));
                LimpiarTextBox($find('<%= txtIVA.ClientID %>'));
                LimpiarTextBox($find('<%= txtTotal.ClientID %>'));
                LimpiarDatePicker($find('<%= txtFechafac.ClientID %>'));
                LimpiarComboSelectIndex0($find('<%= cmbTipoMovimento.ClientID %>'));
                LimpiarComboSelectIndex0($find('<%= cmbTerritorio.ClientID %>'));
                LimpiarComboSelectIndex0($find('<%= cmbRepresentante.ClientID %>'));
            }
            //--------------------------------------------------------------------------------------------------
            //Funciones para cerrar la ventana radWindow actual
            //--------------------------------------------------------------------------------------------------
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
                        //debugger;
                        CloseAndRebind();
                    });
            }
            //Hace un refresh sobre un control especifico, requiere una función en la ventana padre
            function CloseAndRebind() {
                //debugger;
                GetRadWindow().Close();
            }
            //Hace un refresh completo de la ventana padre = F5
            function RefreshParentPage() {
                GetRadWindow().BrowserWindow.location.reload();
            }

            //----------------------------------------------------------------------------------------------------

            function AbrirNotaCreditoPDF(oWnd) {
                var oWnd1 = radopen(oWnd, "AbrirVentana_ImpresionPDFNotaCredito");
                oWnd1.set_showOnTopWhenMaximized(false);
                oWnd1.center();
                oWnd.remove_close(AbrirNotaCreditoPDF);
            }

            function AbrirNotaCreditoPDFCN(WebURL, WebURLCN) {
                // Abre la primera ventana con WebURL
                var oWnd = radopen(WebURL, "AbrirVentana_ImpresionPDFNotaCredito");
                oWnd.set_showOnTopWhenMaximized(false);

                // Establece dimensiones fijas para la primera ventana
                oWnd.set_width(840);
                oWnd.set_height(500);
                oWnd.center();

                // Verifica si WebURLCN tiene contenido y abre la segunda ventana en posición de cascada
                if (WebURLCN != '') {
                    var oWndCN = radopen(WebURLCN, "AbrirVentana_ImpresionPDFNotaCreditoCN");
                    oWndCN.set_showOnTopWhenMaximized(false);

                    // Establece dimensiones fijas para la segunda ventana
                    oWndCN.set_width(840);
                    oWndCN.set_height(500);

                    // Posicionar la segunda ventana en cascada (desplazada de la primera)
                    var offsetX = 50;
                    var offsetY = 50;

                    // Calcular posición para la segunda ventana
                    var leftPos = offsetX;
                    var topPos = offsetY;

                    // Posicionar la segunda ventana
                    oWndCN.moveTo(leftPos, topPos);
                }
            }

        </script>
    </telerik:RadCodeBlock>
</asp:Content>