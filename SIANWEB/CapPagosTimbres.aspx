<%@ Page Title="Pagos Timbres" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.master"
    AutoEventWireup="true" CodeBehind="CapPagosTimbres.aspx.cs" Inherits="SIANWEB.CapPagosTimbres" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="CapaNegocios" %>
<%@ Import Namespace="CapaEntidad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <style>
     .btnTimbrar {
         width: 32px; /* Ajusta el ancho según el tamaño deseado */
         height: 32px; /* Ajusta la altura según el tamaño deseado */
         background-image: url('images/check.png'); /* Ruta de la imagen */
         background-size: cover; /* Asegura que la imagen cubra todo el botón */
         background-repeat: no-repeat;
         background-position: center;
         border: none; /* Opcional: elimina el borde */
         cursor: pointer; /* Cambia el cursor al pasar sobre el botón */
     }
 </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
   
    <telerik:radajaxmanager id="RAM1" runat="server" onajaxrequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight=""/>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnTimbrar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RgDet">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RgDet" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
              <telerik:AjaxSetting AjaxControlID="cboMetodoPago">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RgDet" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="cmbFicha">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RgDet" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>

        
    </telerik:radajaxmanager>
    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server" skin="Default">
    </telerik:radajaxloadingpanel>
    <telerik:radtoolbar id="rtb1" runat="server" width="100%" dir="rtl" onclientbuttonclicking="ToolBar_ClientClick"
        onbuttonclick="rtb1_ButtonClick">
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

        </Items>
    </telerik:radtoolbar>
    <div id="divPrincipal" runat="server">
        <table id="TblEncabezado" style="font-family: verdana; font-size: 8pt" runat="server"
            width="99%">
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td style="text-align: right" width="150px">
                    &nbsp;
                </td>
                <td width="150px">
                    &nbsp;
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt;">
            <%--<tr>
                <td width="120">
                    &nbsp;
                </td>
                <td width="30">
                    &nbsp;
                </td>
                <td width="100">
                    &nbsp;
                </td>
                <td width="10">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td width="10">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>--%>
            <tr>
                <td width="80">
                    Tipo
                </td>
                <td colspan="2">
                    <telerik:radcombobox id="cmbTipo" runat="server" width="130px">
                                                    </telerik:radcombobox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Folio" />
                </td>
                <td>
                    <telerik:radnumerictextbox id="txtClave" runat="server" width="70px" enabled="false"
                        maxlength="9" minvalue="1">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                                    </telerik:radnumerictextbox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    Fecha
                </td>
                <td>
                    <telerik:raddatepicker id="rdFechaPago" runat="server" width="100px" culture="es-MX">
                                                        <Calendar ID="Calendar1" runat="server">
                                                            <ClientEvents OnDateClick="Calendar_Click" />
                                                            <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                TodayButtonCaption="Hoy">
                                                            </FastNavigationSettings>
                                                        </Calendar>
                                                        <DateInput runat="server">
                                                            <ClientEvents OnKeyPress="handleClickEvent" />
                                                        </DateInput>
                                                        <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                    </telerik:raddatepicker>
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt; height: 100%" width="100%">
            <tr>
                <td width="120">
                    <asp:Label ID="Label4" runat="server" Text="Tipo de movimiento"></asp:Label>
                </td>
                <td width="50">
                    <telerik:radnumerictextbox id="txtMovimiento" runat="server" width="50px" minvalue="1"
                        maxlength="9">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnKeyPress="handleClickEvent" OnBlur="txt_OnBlur" OnFocus="_PreValidarFechaEnPeriodo" />
                                                    </telerik:radnumerictextbox>
                </td>
                <td align="left">
                    <telerik:radcombobox id="cmbMovimiento" runat="server" width="350px" filter="Contains"
                        changetextonkeyboardnavigation="true" markfirstmatch="true" onclientblur="Combo_ClientBlur"
                        datatextfield="Descripcion" datavaluefield="Id" enableloadondemand="true" highlighttemplateditems="true"
                        loadingmessage="Cargando..." onclientselectedindexchanged="cmb_ClientSelectedIndexChanged"
                        onclientfocus="_PreValidarFechaEnPeriodo">
                                                        <ItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 25px; text-align: center; vertical-align: top">
                                                                        <asp:Label ID="Label1" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                                                    </td>
                                                                    <td style="text-align: left">
                                                                        <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </telerik:radcombobox>
                </td>
                <td align="right">
                    <%--<telerik:RadButton ID="btnTimbrar" runat="server" Text="Timbrar">
                </telerik:RadButton>
                        
                    <asp:ImageButton ID="btnTimbrar" runat="server" ImageUrl="~/images/check.png" OnClick="Timbrar_Click" OnClientClick="return deshabilitarBoton(this);"  ToolTip="Timbrar" />
               
                        --%>
                     <div style="visibility: hidden">
                        <asp:Button ID="btnTimbrar" runat="server"  OnClick="Timbrar_Click" Text="Accion Timbrar"/>
                    </div>
                    <button type="button" id="btnAccionTimbrar" class="btnTimbrar" onclick="deshabilitarBoton(this); return false;" >&nbsp </button>
                    </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt; height: 100%" width="100%">
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <telerik:radtabstrip id="RadTabStrip1" runat="server" multipageid="RadMultiPage1"
                        selectedindex="0" onclienttabselecting="ClientTabSelecting">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="Datos &lt;u&gt;g&lt;/u&gt;enerales" AccessKey="G"
                                PageViewID="RadPageViewDGenerales" >
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="Timbres" AccessKey="T" PageViewID="RadPageViewDetalles" Selected="True">
                            </telerik:RadTab>

                        </Tabs>
                    </telerik:radtabstrip>
                    <telerik:radmultipage id="RadMultiPage1" runat="server" selectedindex="0" borderstyle="Solid"
                        borderwidth="1px" scrollbars="Hidden">
                        <%--height="370px" width="820px">--%>
                        <telerik:RadPageView ID="RadPageViewDGenerales" runat="server" Height="370px">
                            <telerik:RadSplitter ID="RadSplitter2" runat="server" Height="370px" ResizeMode="AdjacentPane"
                                ResizeWithBrowserWindow="true" BorderSize="0" Width="100.5%">
                                <telerik:RadPane ID="RadPane2" runat="server" Height="370px" OnClientResized="onResize"
                                    Scrolling="None">
                                    <div runat="server" id="Generales">
                                       
                                        <table>
                                            <tr>
                                                <td>

                                                    <telerik:RadSplitter ID="RadSplitter4" runat="server" Height="270px" ResizeMode="AdjacentPane"
                                                        ResizeWithBrowserWindow="true" BorderSize="0" Width="100%">
                                                        <telerik:RadPane ID="RadPane4" runat="server" Height="270px" BorderStyle="None" OnClientResized="onResize">
                                                            <telerik:RadGrid ID="RgGral" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                                                                GridLines="None" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                                                                OnItemCommand="RgGral_ItemCommand" OnNeedDataSource="RgGral_NeedDataSource" OnPageIndexChanged="RgGral_PageIndexChanged"
                                                                PageSize="6" OnItemCreated="RgGral_ItemCreated">
                                                                <MasterTableView NoMasterRecordsText="No se encontraron registros." >
                                                                    <Columns>
                                                                        <telerik:GridTemplateColumn DataField="rgGralId" HeaderText="Id" UniqueName="rgGralId"
                                                                            Display="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblGralId1" runat="server" Text='<%# Bind("rgGralId") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:Label ID="lblGralId2" runat="server" Text='<%# Bind("rgGralId") %>'></asp:Label>
                                                                            </EditItemTemplate>
                                                                            <HeaderStyle Width="10px" />
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn DataField="Pag_ficha" HeaderText="Ficha" UniqueName="Pag_ficha">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblFicha" runat="server" Text='<%# Bind("Pag_ficha") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <telerik:RadNumericTextBox ID="rgtxtFicha" runat="server" Width="50px" Text='<%# Bind("Pag_ficha") %>'
                                                                                    MaxLength="9" ReadOnly="true" MinValue="0">
                                                                                    <EnabledStyle HorizontalAlign="Right" />
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                    <ClientEvents OnLoad="Ficha_Load" OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>
                                                                            </EditItemTemplate>
                                                                            <HeaderStyle Width="70px" />
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn DataField="Pag_Fecha" HeaderText="Fecha" UniqueName="Pag_Fecha">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblFecha" runat="server" Text='<%# Bind("Pag_Fecha","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <telerik:RadDatePicker ID="rdFecha" runat="server" Width="100px" DbSelectedDate='<%# Bind("Pag_Fecha") %>'>
                                                                                    <Calendar ID="Calendar1" runat="server">
                                                                                        <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                                                                            TodayButtonCaption="Hoy">
                                                                                        </FastNavigationSettings>
                                                                                    </Calendar>
                                                                                    <DateInput runat="server">
                                                                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                    </DateInput>
                                                                                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                                                                                </telerik:RadDatePicker>
                                                                            </EditItemTemplate>
                                                                            <HeaderStyle Width="120px" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn DataField="Pag_Banco" HeaderText="Banco" UniqueName="Pag_Banco">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblBanco" runat="server" Text='<%# Bind("Pag_BancoStr") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <telerik:RadNumericTextBox ID="rgtxtIdBanco" runat="server" Width="50px" MaxLength="9"
                                                                                    DbValue='<%# Bind("Pag_Banco") %>' MinValue="1" OnTextChanged="txtBanco_TextChanged"
                                                                                    AutoPostBack="true">
                                                                                    <ClientEvents OnLoad="IdBanco_Load" OnBlur="txtBanco_OnBlur" OnKeyPress="handleClickEvent" />
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                                    <EnabledStyle HorizontalAlign="Right" />
                                                                                </telerik:RadNumericTextBox>
                                                                                <telerik:RadComboBox ID="cmbBancos" runat="server" Width="200px" Filter="Contains"
                                                                                    ChangeTextOnKeyBoardNavigation="true" MarkFirstMatch="true" OnClientBlur="Combo_ClientBlur"
                                                                                    DataTextField="Descripcion" DataValueField="Id" EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                                                                                    LoadingMessage="Cargando..." OnDataBinding="cmbBancos_DataBinding" Text='<%# Bind("Pag_BancoStr") %>'
                                                                                    OnClientLoad="Banco_Load" OnClientSelectedIndexChanged="cmbBanco_ClientSelectedIndexChanged"
                                                                                    MaxHeight="250px">
                                                                                    <ItemTemplate>
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td style="width: 25px; text-align: center; vertical-align: top">
                                                                                                    <asp:Label ID="LabelID" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "Id").ToString() == "-1" ? "": DataBinder.Eval(Container.DataItem, "Id").ToString() %>' />
                                                                                                </td>
                                                                                                <td style="text-align: left">
                                                                                                    <asp:Label ID="LabelDESC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descripcion") %>' />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </ItemTemplate>
                                                                                </telerik:RadComboBox>
                                                                            </EditItemTemplate>
                                                                            <HeaderStyle Width="280px" />
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn HeaderText="Cuenta" DataField="Ban_Cuenta" UniqueName="Ban_Cuenta">
                                                                            <HeaderStyle Width="120px" HorizontalAlign="Center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblBanCuenta" runat="server" Text='<%# Eval("Ban_Cuenta").ToString() %>' />
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <telerik:RadTextBox Width="100px" ID="txtBanCuenta" runat="server" ReadOnly="true"
                                                                                    Text='<%# Eval("Ban_Cuenta").ToString() %>'>
                                                                                </telerik:RadTextBox>
                                                                            </EditItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn DataField="Pag_Importe" HeaderText="Importe" UniqueName="Pag_Importe">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblImporte" runat="server" Text='<%# Bind("Pag_Importe", "{0:N2}" ) %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <telerik:RadNumericTextBox ID="rgtxtImporte" runat="server" Width="100px" Text='<%# Bind("Pag_Importe") %>'
                                                                                    MaxLength="9" MinValue="0">
                                                                                    <NumberFormat DecimalDigits="2" AllowRounding="true" />
                                                                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                                                                </telerik:RadNumericTextBox>
                                                                            </EditItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                            <HeaderStyle Width="120px" />
                                                                        </telerik:GridTemplateColumn>




                                                                      
                                                                    </Columns>
                                                                </MasterTableView>
                                                                <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                                                    FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                                                                    PageButtonCount="3" PagerTextFormat="Change page: {4} &nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                                                    PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                                                <%--<ClientSettings>
                                                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True" ScrollHeight="215px">
                                                                    </Scrolling>
                                                                </ClientSettings>--%>
                                                            </telerik:RadGrid>
                                                            
                                                        </telerik:RadPane>
                                                    </telerik:RadSplitter>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageViewDetalles" runat="server" heigth="600px">
                            <telerik:RadSplitter ID="RadSplitter1" runat="server" Height="600px" ResizeMode="AdjacentPane"
                                ResizeWithBrowserWindow="true" BorderSize="0" Width="101%">
                                <telerik:RadPane ID="RadPane1" runat="server" Height="600px" OnClientResized="onResize"
                                    BorderStyle="None" Scrolling="None">
                                    <div runat="server" id="Detalles">
                                    <table>
                                    <tr>
                                    <td>
                                    <br />
                                    </td>
                                    </tr>
                                     <tr>
                            <td>
                                Forma de pago
                            </td>
                            <td>
                                 <telerik:RadComboBox ID="cboMetodoPago" runat="server" ChangeTextOnKeyBoardNavigation="true"  autopostback="True"                                                                           EnableLoadOnDemand="true" Filter="Contains" onselectedindexchanged="cboMetodoPago_SelectedIndexChanged" MarkFirstMatch="true" Width="200px" onclientblur="Combo_ClientBlur" maxheight="300px"
                                                                            LoadingMessage="Cargando...">
                                                                        </telerik:RadComboBox>
                            </td>
                            </tr>
                            <tr>
                            <td>
                                Ficha
                            </td>
                            <td style="text-align:right;">
                                 <telerik:RadComboBox ID="cmbFicha" runat="server" ChangeTextOnKeyBoardNavigation="true"  autopostback="True" 
                                     EnableLoadOnDemand="true" Filter="Contains" onselectedindexchanged="cmbFicha_SelectedIndexChanged" MarkFirstMatch="true" Width="350px" maxheight="300px"
                                                                            LoadingMessage="Cargando...">
                                                                        </telerik:RadComboBox>
                            </td>
                            </tr>
                            <tr>
                            <td>
                              <br />
                            </td></tr>
                            </table>
                                        <%-- <table>
                                            <tr>
                                                <td>--%>
                                        <%--<asp:Panel ID="Panel1" runat="server" Width="810px"  Height="340px" ScrollBars="Horizontal">--%>
                                        <telerik:RadSplitter ID="RadSplitter3" runat="server" Height="270px" ResizeMode="AdjacentPane"
                                            ResizeWithBrowserWindow="true" BorderSize="0" Width="98%">
                                            <telerik:RadPane ID="RadPane3" runat="server" Height="250px" OnClientResized="onResize"
                                                BorderStyle="None">

                                                  
                                                <telerik:RadGrid ID="RgDet" runat="server" AllowPaging="False" AutoGenerateColumns="False"  
                                                    GridLines="None" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                                                    OnItemCommand="RgDet_ItemCommand" OnNeedDataSource="RgDet_NeedDataSource" OnPageIndexChanged="RgDet_PageIndexChanged"
                                                    PageSize="20" OnItemCreated="RgDet_ItemCreated" OnItemDataBound="RgDet_ItemDataBound" >
                                                    <MasterTableView >
                                                        <Columns>

                                                       <%--  <telerik:GridButtonColumn CommandName="Enviar" HeaderText="Enviar" Text="Enviar" UniqueName="Enviar"
                                                            Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png" ButtonCssClass="email_grid">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                                                        </telerik:GridButtonColumn>--%>
                                                        <telerik:GridButtonColumn CommandName="Comprobante" HeaderText="Historial" ConfirmDialogType="RadWindow"
                                                            ConfirmDialogHeight="150px" ConfirmDialogWidth="450px" Text="Comprobantes de complementos de pago" UniqueName="Comprobante"
                                                            Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png" ButtonCssClass="edit">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <HeaderStyle HorizontalAlign="Center" Width="55"></HeaderStyle>
                                                        </telerik:GridButtonColumn>
                                                       <%-- <telerik:GridTemplateColumn HeaderText="PDF" HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px"
                                                            UniqueName="PDF">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                                    CssClass="edit" ToolTip="Descargar" CommandName="PDF" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </telerik:GridTemplateColumn>                                                        
                                                        <telerik:GridTemplateColumn HeaderText="XML" HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px"
                                                            UniqueName="XML">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                                    CssClass="edit" ToolTip="Descargar" CommandName="XML" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </telerik:GridTemplateColumn>--%>

                                                        <telerik:GridTemplateColumn UniqueName="TimbrarVarios">
                                                            <HeaderTemplate>
                                                                <input onclick="CheckAllTimbrar(this);" type="checkbox" id="ChkTimbrarHeader" runat="server" />
                                                                Timbrar
                                                            
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="ChkTimbrar" runat="server" Style="cursor: hand" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                                        </telerik:GridTemplateColumn>
                                                                    <telerik:GridButtonColumn CommandName="Factoraje" HeaderText="Método Especial de pago" ConfirmDialogType="RadWindow"
                                                            ConfirmText="¿desea realizar el método especial de pago?</br></br>" ConfirmDialogHeight="150px"
                                                            ConfirmDialogWidth="350px" Text="Factoraje" UniqueName="Factoraje" Visible="True" ButtonType="ImageButton"
                                                            ImageUrl="~/Imagenes/blank.png" ButtonCssClass="edit">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
                                                        </telerik:GridButtonColumn>

                                                             <telerik:GridButtonColumn CommandName="Cancelar" HeaderText="Cancelación Fiscal" ConfirmDialogType="RadWindow"
                                                            ConfirmText="¿Está seguro desea cancelar el comprobante de pago?</br></br>" ConfirmDialogHeight="150px"
                                                            ConfirmDialogWidth="350px" Text="Cancelar" UniqueName="Cancelar" Visible="True" ButtonType="ImageButton"
                                                            ImageUrl="~/Imagenes/blank.png" ButtonCssClass="baja">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
                                                        </telerik:GridButtonColumn>

                                                        <telerik:GridTemplateColumn DataField="RgDId" HeaderText="Id" UniqueName="rgDetlId2" Display="false">
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridBoundColumn DataField="Id_Emp" HeaderText="Id_Emp" Display="false" UniqueName="Id_Emp">
                                                            </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Doc_Referencia" HeaderText="Doc_Referencia" Display="true" UniqueName="Id_Emp">
                                                            </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Cdi" HeaderText="Cdi" Display="false" UniqueName="Id_Emp">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Id_Cte" HeaderText="Id_Cte" Display="false" UniqueName="Id_Emp">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Id_PagDet" HeaderText="Id_PagDet" Display="false" UniqueName="Id_PagDet">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Cte_Rfc" HeaderText="Cte_Rfc" Display="false" UniqueName="Cte_Rfc">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Serie" HeaderText="Serie2" Display="false" UniqueName="Serie2">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Pag_Movimiento" HeaderText="Pag_Movimiento" Display="false" UniqueName="Pag_Movimiento">
                                                        </telerik:GridBoundColumn>
                                                            

                                                        <telerik:GridTemplateColumn DataField="Pag_MovimientoStr" HeaderText="Movimiento" UniqueName="Pag_Movimiento">
                                       
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMov" runat="server" Text='<%# Bind("Pag_MovimientoStr") %>'></asp:Label>
                                                                <asp:Label ID="lblMov1" runat="server" Text='<%# Bind("Pag_Movimiento") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="120px" />
                                                        </telerik:GridTemplateColumn>


                                                        <telerik:GridTemplateColumn DataField="Serie" HeaderText="Serie" UniqueName="Serie">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSerie" runat="server" Text='<%# Bind("Serie") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <HeaderStyle Width="60px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Pag_Referencia" HeaderText="Ref." UniqueName="Pag_Referencia">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblReferencia" runat="server" Text='<%# Bind("Doc_Referencia") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle Width="60px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Fac_FolioFiscal" HeaderText="Folio Fiscal" UniqueName="Fac_FolioFiscal">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblFolioFiscal" runat="server" Text='<%# Bind("Fac_FolioFiscal") %>'></asp:Label>
                                                            </ItemTemplate>
                                                           
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle Width="150px" />
                                                        </telerik:GridTemplateColumn>
                                                             <telerik:GridTemplateColumn DataField="FormaPago" HeaderText="Forma Pago" UniqueName="FormaPago">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("FormaPago") %>'></asp:Label>
                                                            </ItemTemplate>
                                                           
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle Width="60px" />
                                                        </telerik:GridTemplateColumn>
                                                             <telerik:GridTemplateColumn DataField="MetodoPago" HeaderText="Método Pago" UniqueName="MetodoPago">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("MetodoPago") %>'></asp:Label>
                                                            </ItemTemplate>
                                                           
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <HeaderStyle Width="60px" />
                                                        </telerik:GridTemplateColumn>

                                                         <telerik:GridTemplateColumn DataField="Pag_Importe" HeaderText="Importe" UniqueName="Pag_Importe">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblImportePag" runat="server" Text='<%# Bind("Pag_Importe", "{0:N2}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle Width="70px" />
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridTemplateColumn DataField="Cdi" HeaderText="Cdi" UniqueName="Cdi">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCdi" runat="server" Text='<%# Bind("Cdi") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle Width="60px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Id_Terr" HeaderText="Terr." UniqueName="v">

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTerr" runat="server" Text='<%# Bind("Id_Terr") %>'></asp:Label>
                                                            </ItemTemplate>
                                                       
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle Width="60px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Pag_Fecha" HeaderText="Fecha" UniqueName="Pag_Fecha">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFecha" runat="server" Text='<%# Bind("Doc_Fecha","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="110px" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Id_Cte" HeaderText="Núm." UniqueName="Id_Cte">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCte" runat="server" Text='<%# Bind("Id_Cte") %>'></asp:Label>
                                                            </ItemTemplate>
                                                          
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle Width="60px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Cte_Nombre" HeaderText="Cliente" UniqueName="Cte_Nombre">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCteNombre" runat="server" Text='<%# Bind("Cte_Nombre") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <HeaderStyle Width="250px" />
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridTemplateColumn DataField="Cte_Rfc" HeaderText="RFC" UniqueName="Cte_Rfc2">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCteRfc" runat="server" Text='<%# Bind("Cte_Rfc") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <HeaderStyle Width="105px" />
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridTemplateColumn DataField="Pag_Numero" HeaderText="Núm." UniqueName="Pag_Numero">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNumero" runat="server" Text='<%# Bind("Pag_Numero") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle Width="60px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Pag_Cheque" HeaderText="Cheque" UniqueName="Pag_Cheque">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCheque" runat="server" Text='<%# Bind("Pag_Cheque") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle Width="70px" />
                                                        </telerik:GridTemplateColumn>
                                                       
                                                        <telerik:GridTemplateColumn DataField="Doc_Estatus" HeaderText="Estatus" UniqueName="Doc_Estatus"
                                                            Display="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEstatus" runat="server" Text='<%# Bind("Doc_Estatus") %>'></asp:Label>
                                                            </ItemTemplate>
                                                         
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle Width="100px" />
                                                        </telerik:GridTemplateColumn>


                                                        <telerik:GridTemplateColumn DataField="Doc_Importe" HeaderText="Importe" UniqueName="Doc_Importe"
                                                            Display="false">

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblImporteDoc" runat="server" Text='<%# Bind("Doc_Importe") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle Width="100px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Doc_Pagado" HeaderText="Pagado" UniqueName="Doc_Pagado"
                                                            Display="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPagado" runat="server" Text='<%# Bind("Doc_Pagado") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle Width="100px" />
                                                        </telerik:GridTemplateColumn>
                                                         <%--<telerik:GridTemplateColumn HeaderText="XML Cancel." HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px"
                                                            UniqueName="XMLCancel">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="xmlCancelacion" runat="server" ImageUrl="~/Imagenes/blank.png"
                                                                    CssClass="edit" ToolTip="Descargar" CommandName="xmlCancelacion" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </telerik:GridTemplateColumn>--%>
                                                       
                                                   
                                                        </Columns>
                                                        <EditFormSettings>
                                                            <EditColumn UniqueName="EditCommandColumn1">
                                                            </EditColumn>
                                                        </EditFormSettings>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </MasterTableView>
                                                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                                                        FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                                                        PageButtonCount="3" PagerTextFormat="Change page: {4} &nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                                                        PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
                                                    <ClientSettings>
                                                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True">                                                           
                                                                    </Scrolling>
                                                                </ClientSettings>
                                                </telerik:RadGrid>
                                            </telerik:RadPane>
                                        </telerik:RadSplitter>
                                        <%--  </asp:Panel>--%>
                                        <%--   </td>
                                            </tr>
                                        </table>--%>
                                    </div>
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                        </telerik:RadPageView>
                        
                    </telerik:radmultipage>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <table width="100%">
                        <tr>
                            <td align="right">
                                <asp:Label ID="Label2" runat="server" Text="Importe de fichas"></asp:Label>
                            </td>
                            <td width="100">
                                <telerik:radnumerictextbox id="txtImporte" runat="server" width="100px" minvalue="0"
                                    readonly="True" value="0" maxlength="9">
                                    <NumberFormat DecimalDigits="2" AllowRounding="true" />
                                    <ClientEvents OnKeyPress="handleClickEvent" />
                                </telerik:radnumerictextbox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:Button ID="btnConfirm" runat="server" CssClass="buttonsHiden" Text="Button"
            OnClick="Button2_Click" />
        <asp:HiddenField ID="HiddenRebind" runat="server" />
        <asp:HiddenField ID="HF_PageName" runat="server" />
        <asp:HiddenField ID="HF_ID" runat="server" />
        <asp:HiddenField ID="HF_Timbrado" runat="server" />
        <asp:HiddenField ID="HF_FechaPago" runat="server" />
        <asp:HiddenField ID="HiddenHeight" runat="server" />
        <asp:HiddenField ID="HF_IdEmp" runat="server" />
        <asp:HiddenField ID="HF_IdCd" runat="server" />
        <asp:HiddenField ID="HF_IdCTE" runat="server" />
        <asp:HiddenField ID="HF_FPI" runat="server" />
        <asp:HiddenField ID="HF_MP" runat="server" />
        <asp:HiddenField ID="HF_NMP" runat="server" />
        <asp:HiddenField ID="HF_Centro" runat="server" />
        <asp:HiddenField ID="clientSideIsPostBack" runat="server" Value="N" />
    </div>
    <style type="text/css">
        .buttonsHiden
        {
            display: none;
        }
    </style>
    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function onResize(sender, eventArgs) {
                var postback = document.getElementById("<%=clientSideIsPostBack.ClientID %>").value;
                document.getElementById("<%= HiddenHeight.ClientID %>").value = document.documentElement.clientHeight;
                ajaxManager.ajaxRequest('panel');
            }

            function Ficha_Load(sender, args) {
                //debugger;
                var valor = sender.get_value();
                var MasterTable = rgGrid.get_masterTableView();
                var length = MasterTable.get_dataItems().length;
                if (valor == '') {
                    sender.set_value(length + 1);
                }
            }
            function AbrirVentana_PagoDif() {
                var oWnd = radopen("CapPago_Dif.aspx", "AbrirVentana_DifPago");
                oWnd.center();
                //return true;
                //GetRadWindow().BrowserWindow.AbrirVentana_PagoDif();
            }

            function AbrirVentana_PagoTimbreDetalladoComplemento(Id, IDCTE, idPagdte, Id_Fac, serie) {
                AbrirVentana_PagoTimbreDetalladoComplemento(Id, IDCTE, idPagdte, Id_Fac, serie);
                return false
            }

            function AbrirVentana_PagoTimbreDetalladoComplemento(Id, IDCTE, idPagdte, Id_Fac, serie) {
                var oWnd = radopen("CapPagosTimbresDet.aspx?Id=" + Id + "&idCte=" + IDCTE + "&idPagDte=" + idPagdte + "&IdFac=" + Id_Fac + "&serie=" + serie, +"AbrirVentana_DetalleTimbre");
                oWnd.center();
                oWnd.setSize(750, 450);
            }


            function AbrirVentana_PagoFactoraje(Id, IDCTE, idPagdte, Id_Fac, serie) {
                AbrirVentana_PagoFactoraje(Id, IDCTE, idPagdte, Id_Fac, serie);
                return false
            }

            function AbrirVentana_PagoFactoraje(Id, IDCTE, idPagdte, Id_Fac, serie) {
                var idEmp = document.getElementById('<%= HF_IdEmp.ClientID %>').value;
                var idCd = document.getElementById('<%= HF_IdCd.ClientID %>').value;

                var oWnd = radopen("CapPagoFactoraje.aspx?Prmt=2&Id_Emp=" + idEmp + "&Id_Cd=" + idCd + "&Id_Pag=" + Id + "&idCte=" + IDCTE + "&idPagDte=" + idPagdte + "&IdFac=" + Id_Fac + "&serie=" + serie, +"AbrirVentana_PagoFactoraje");
                oWnd.center();
                oWnd.setSize(450, 300);
                oWnd.add_close(HideActions);
            }

            // Eliminar función duplicada para evitar la recursión infinita.
            //function AbrirVentana_PagoSubrogacion(Id, IDCTE, idPagdte, Id_Fac, serie) {
            //    AbrirVentana_PagoSubrogacion(Id, IDCTE, idPagdte, Id_Fac, serie);
            //    return false
            //}

            function AbrirVentana_PagoSubrogacion(Id, IDCTE, idPagdte, Id_Fac, serie) {
                var idEmp = document.getElementById('<%= HF_IdEmp.ClientID %>').value;
                var idCd = document.getElementById('<%= HF_IdCd.ClientID %>').value;

                var oWnd = radopen("CapPagoSubrogacion.aspx?Prmt=2&Id_Emp=" + idEmp + "&Id_Cd=" + idCd + "&Id_Pag=" + Id + "&idCte=" + IDCTE + "&idPagDte=" + idPagdte + "&IdFac=" + Id_Fac + "&serie=" + serie, +"AbrirVentana_PagoFactoraje");
                oWnd.center();
                oWnd.setSize(450, 400);
                oWnd.add_close(HideActions);
            }


            function confirmCallBackFn(arg) {
                if (arg == true) {
                    var idEmp = document.getElementById('<%= HF_IdEmp.ClientID %>').value;
                    var idCd = document.getElementById('<%= HF_IdCd.ClientID %>').value;
                    var idPag = document.getElementById('<%= HF_ID.ClientID %>').value;
                    var idcte = document.getElementById('<%= HF_IdCTE.ClientID %>').value;

                    var oWnd = radopen("CapPagoFactoraje.aspx?Prmt=1&Id_Emp=" + idEmp + "&Id_Cd=" + idCd + "&idCte=" + idcte + "&Id_Pag=" + idPag, +"AbrirVentana_PagoFactoraje");
                    oWnd.center();
                    oWnd.setSize(450, 300);
                    oWnd.add_close(HideActions);
                }
            }

            function confirmCallBackFnSubrogacion(arg) {
                if (arg == true) {
                    var idEmp = document.getElementById('<%= HF_IdEmp.ClientID %>').value;
                    var idCd = document.getElementById('<%= HF_IdCd.ClientID %>').value;
                    var idPag = document.getElementById('<%= HF_ID.ClientID %>').value;
                    var idcte = document.getElementById('<%= HF_IdCTE.ClientID %>').value;

                    var oWnd = radopen("CapPagoSubrogacion.aspx?Prmt=1&Id_Emp=" + idEmp + "&Id_Cd=" + idCd + "&idCte=" + idcte + "&Id_Pag=" + idPag, +"AbrirVentana_PagoFactoraje");
                    oWnd.center();
                    oWnd.setSize(450, 400);
                    oWnd.add_close(HideActions);
                }
            }


            function confirmMetodoCallBackFn(arg) {
                if (arg == true) {
                    callmetodoConfirm();
                }
            }

            function callmetodoConfirm() {
                var nombre = document.getElementById('<%= HF_NMP.ClientID %>').value;
                radconfirm('Se genera REP con la forma de pago ' + nombre + ' </br> ¿Es correcto?', callMetodoexit, 350, 150);
            }

            function callMetodoexit(arg) {
                if (arg == true) {
                    var ajaxManager = $find("<%= RAM1.ClientID %>");
                    ajaxManager.ajaxRequest("ConfirmaTimbre");
                }
            }



            function HideActions(sender) {
                sender.remove_close(HideActions);
                refreshGrid();
            }

            function refreshGrid() {
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest('RebindGrid');
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
                //debugger;
                GetRadWindow().Close();
                GetRadWindow().BrowserWindow.refreshGrid(null);
            }

            function CloseAlert(mensaje) {
                var cerrarWindow = radalert(mensaje, 330, 150);
                cerrarWindow.add_close(
                    function () {
                        CloseWindow();
                    });
            }

            //--------------------------------------------------------------------------------------------------
            //Limpiar controles de formulario  
            //--------------------------------------------------------------------------------------------------
            function LimpiarControles() {
            }

            //--------------------------------------------------------------------------------------------------
            //Cuando un botón del toolBar es clickeado
            //--------------------------------------------------------------------------------------------------
            function ToolBar_ClientClick(sender, args) {

                var continuarAccion = true;
                var habilitaValidacion = false;
                var button = args.get_item();

                //habilitar/deshabilitar validators
                if (button.get_value() == 'save')
                    habilitaValidacion = true;
                else {
                    habilitaValidacion = false;
                }

                switch (button.get_value()) {
                    case 'save':
                        var radTabStrip = $find('<%= RadTabStrip1.ClientID %>');
                        if (mov.get_value() == '') {
                            radTabStrip.get_allTabs()[0].select();
                            continuarAccion = false;
                        }
                        else {
                            radTabStrip.get_allTabs()[0].select();

                        }
                        if (Page_ClientValidate()) {
                            button.set_enabled(false);
                        }
                        break;
                }
                args.set_cancel(!continuarAccion);
            }

            function txt_OnBlur(sender, args) {

            }

            function cmb_ClientSelectedIndexChanged(sender, eventArgs) {
            }

            var IdBanco;
            var NomBanco;
            function IdBanco_Load(sender, args) {
                IdBanco = sender;
            }
            function Banco_Load(sender, args) {
                NomBanco = sender;
            }
            function txtBanco_OnBlur(sender, args) {
                OnBlur(sender, NomBanco);
            }
            function cmbBanco_ClientSelectedIndexChanged(sender, eventArgs) {

                ClientSelectedIndexChanged(eventArgs.get_item(), IdBanco);
            }
            function ObtenerControlFecha() {
                return txtFecha._dateInput;
            }


            function ClientTabSelecting(sender, args) {
                args.set_cancel(false);
            }

            function _PreValidarFechaEnPeriodo() {
                //debugger;

            }
            function abrirArchivo(pagina) {
                var opciones = "toolbar=yes, location=yes, directories=yes, status=yes, menubar=yes, scrollbars=yes, resizable=yes, width=508, height=365, top=100, left=140";
                window.open(pagina, '', opciones);
            }

            function abrirArchivoCN(pagina, paginaCN) {
                var opciones = "toolbar=yes, location=yes, directories=yes, status=yes, menubar=yes, scrollbars=yes, resizable=yes, width=508, height=365, top=100, left=140";
                window.open(pagina, 'XML', opciones);
                window.open(paginaCN, 'XML Cuenta Nacional', opciones);
            }
            function AbrirFacturaPDF(WebURL) {
                var oWnd = radopen(WebURL, "AbrirVentana_ImpresionPDFFactura");
                oWnd.set_showOnTopWhenMaximized(false);
                oWnd.center();
            }
            function AbrirFacturaPDFVarias(WebURL) {
                window.open(WebURL, "_blank");
            }
            function AbrirVentana_EnviarDocumentos(Id_Emp, Id_Cd, Id_Fac) {
                var oWnd = radopen("Ventana_EnviarDocumentos.aspx?Id_Emp=" + Id_Emp
                    + "&Id_Cd=" + Id_Cd
                    + "&Id_Doc=" + Id_Fac
                    + "&Tipo=FACTURA"
                    , "AbrirVentana_EnviarDocumentos");
                oWnd.center();
            }

            function AbrirVentana_EnviarPagos(Id_Emp, Id_Cd, Id_Cte, Id_Pag, Id_Fac, Id_PagDet, serie) {
                var oWnd = radopen("Ventana_EnviarPagos.aspx?Id_Emp=" + Id_Emp
                    + "&Id_Cd=" + Id_Cd
                    + "&Id_Cte=" + Id_Cte
                    + "&Id_Pag=" + Id_Pag
                    + "&Id_Fac=" + Id_Fac
                    + "&Id_PagDet=" + Id_PagDet
                    + "&Serie=" + serie
                    + "&Tipo=PAGO"
                    , "AbrirVentana_EnviarPagos");
                oWnd.center();
            }

            function CheckAllTimbrar(sender) {
                //debugger;
                var grid = $find('<%=RgDet.ClientID %>');
                var masterTable = grid.get_masterTableView();
                var i = 0;
                var row;
                var importeTotal = 0;
                var importe;
                for (i = 0; i < masterTable.get_dataItems().length; i++) {
                    row = masterTable.get_dataItems()[i];
                    var chk = row.findElement("ChkTimbrar");
                    var lblImporte = row.findElement("lblImportePag");
                    if (chk != null) {
                        chk.checked = sender.checked;
                        if (chk.checked) {
                            importe = lblImporte.outerText.replace(',', '');
                            importeTotal = parseFloat(importeTotal) + parseFloat(importe);
                        }
                    }
                }
                document.getElementById('ctl00_CPH_txtImporte_text').value = importeTotal.toFixed(2);
            }

            function CheckTimbrar(sender) {
                //debugger;
                var grid = $find('<%=RgDet.ClientID %>');
                var masterTable = grid.get_masterTableView();
                var i = 0;
                var row;
                var importeTotal = 0;
                var importe;
                for (i = 0; i < masterTable.get_dataItems().length; i++) {
                    row = masterTable.get_dataItems()[i];
                    var chk = row.findElement("ChkTimbrar");
                    var lblImporte = row.findElement("lblImportePag");
                    if (chk != null) {
                        if (chk.checked) {
                            importe = lblImporte.outerText.replace(',', '');
                            importeTotal = parseFloat(importeTotal) + parseFloat(importe);
                        }
                    }
                }
                document.getElementById('ctl00_CPH_txtImporte_text').value = importeTotal.toFixed(2);
            }
            var contadorCiclo = 1;
           
            // Funcion recursiva controlada, con segundos de espera entre peticiones y espera de respuesta http.
            // finaliza al notificar termino de proceso en segundo plano o no existe pendiente en segundo plano.
            function ConsultaEstatusPago(nPeticion) {
                    var idPago = document.getElementById('<%= HF_ID.ClientID %>').value; 
                    var urlApp = "<%=ApplicationUrl %>" + "/api/ComplementoPago/ConsultarPago?IntPago=" + idPago;
                    

                    fetch( urlApp , {
                        method: "POST",
                        mode: "cors",
                        cache: "no-cache",
                        credentials: "same-origin",
                        redirect: "follow",
                        referrer: "no-referrer",
                        headers: {
                            'Content-Type': 'application/json'
                        }
                    }).then(function (response) {
                        if (response.status !== 404 && response.status !== 500) {
                            return response.json();
                        }
                        return Promise.reject(response.statusText)
                    }).then(function (data) {

                        console.log(data);
                        
                        if (data.intActivo == 0 && data.intNotificado == 0) {
                            let ruta = data.srlScript
                            console.log(ruta);
                            console.log(ruta.length);
                            if (ruta.length > 0) {
                                AbrirFacturaPDFVarias(ruta);
                            } 
                                
                            if (data.rslAlert.length > 0) {                                    
                                radalert(data.strRespuesta);
                                radalert(data.rslAlert);
                            } else {
                                radalert(data.strRespuesta);
                            }
                            var ajaxManager = $find("<%= RAM1.ClientID %>");
                            ajaxManager.ajaxRequest('RebindGrid');
                            console.log("Fin");
                        } else {
                            if (data.intActivo == 1) {
                                if (nPeticion == 1) {
                                    radalert(data.strRespuesta);
                                }
                                console.log("Volver a consultar, peticion: " + nPeticion);
                                // consulta recursiva
                                setTimeout(function () {
                                    contadorCiclo = nPeticion + 1; 
                                    ConsultaEstatusPago(contadorCiclo);
                                }, 8000);
                            } else {
                                console.log("Fin");
                            }
                        }
                    }).catch(function (error) {

                        if (typeof error !== "undefined") {
                            console.log(error);
                        }

                    }).finally(function () {
                        
                    });
                    
                
            };
            window.addEventListener("load", (event) => {
                ConsultaEstatusPago(contadorCiclo);
            });

            function deshabilitarBoton(boton) {
                boton.disabled = true; // Deshabilitar el botón principal

                // Usar RadAjaxManager en lugar de provocar un postback
                var ajaxManager = $find("<%= RAM1.ClientID %>");
                ajaxManager.ajaxRequest("TimbrarDocumentos");
                
                
            }


            // funcion para abilitar
            function HabilitarBoton() {
                var boton = document.getElementById('btnAccionTimbrar');
                boton.disabled = false; // Habilitar el botón
            }
            
        </script>
    </telerik:radcodeblock>
</asp:Content>
