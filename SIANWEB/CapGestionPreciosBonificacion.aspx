<%@ Page Title="Reporte de bonificaciones" Language="C#" MasterPageFile="~/MasterPage/MasterPage03.Master"
    AutoEventWireup="true" CodeBehind="CapGestionPreciosBonificacion.aspx.cs" Inherits="SIANWEB.CapGestionPreciosBonificacion" %>

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
                    //debugger;
                    GetRadWindow().Close();
                    //GetRadWindow().BrowserWindow.refreshGrid();
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


                function callConfirmacancelacion(mensaje) {
                    radconfirm(mensaje, 90, 480, confirmCallBackFn);
                }


                function confirmCallBackFn(arg) {
                    var ajaxManager = $find("<%=RAM1.ClientID%>");
                    if (arg) {
                        ajaxManager.ajaxRequest('Cancelarcte');
                    }
                    else {
                        ajaxManager.ajaxRequest('cancel');
                    }
                }

                //Validaciones especiales
                function ValidacionesEspeciales() {
                    //debugger;

                    //obtener controles de formulario de inserión/edición de Grid
                    var datePickerFechaInicio = $find('<%= dpFecha1.ClientID %>');
                    var datePickerFechaFin = $find('<%= dpFecha2.ClientID %>');

                    //realizar validaciones
                    var fechaInicio = null;
                    var fechaFin = null;

                    fechaInicio = datePickerFechaInicio._dateInput.get_selectedDate();
                    fechaFin = datePickerFechaFin._dateInput.get_selectedDate();

                    //validar rango correcto de fechas.
                    if (fechaInicio != null && fechaFin != null && (fechaInicio > fechaFin)) {
                        var mensage = 'La fecha inicial, no debe ser mayor a la fecha final';
                        var alerta = radalert(mensage, 330, 150, tituloMensajes);

                        alerta.add_close(function () { datePickerFechaInicio._dateInput.focus(); });
                        return false
                    }

                    return true;
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
                        <%--Estas variables las uso al cancelar un cliente--%>
                        <asp:HiddenField runat="server" ID="HFId_PCElim"/>
                        <asp:HiddenField runat="server" ID="HFId_SolElim"/>
                        <asp:HiddenField runat="server" ID="HFId_CteElim"/>
                        <asp:HiddenField runat="server" ID="HFId_CDelim"/>
                        <asp:HiddenField runat="server" ID="HFId_CteNom"/>
                        <asp:HiddenField runat="server" ID="HFId_CDIElim"/>

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
                        <asp:Label ID="Label8" runat="server" Text="Fecha inicial:" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="dpFecha1" runat="server" Width="120px" DateInput-MaxLength="10"
                            Enabled="true">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                    TodayButtonCaption="Hoy">
                                </FastNavigationSettings>
                            </Calendar>
                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                            </DateInput>
                            <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                        </telerik:RadDatePicker>
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
                        <asp:Label ID="Label5" runat="server" Text="Fecha final:" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="dpFecha2" runat="server" Width="120px" DateInput-MaxLength="10"
                            Enabled="True">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                <FastNavigationSettings CancelButtonCaption="Cancelar" OkButtonCaption="Aceptar"
                                    TodayButtonCaption="Hoy">
                                </FastNavigationSettings>
                            </Calendar>
                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                <ClientEvents OnKeyPress="SoloNumericoYDiagonal" />
                            </DateInput>
                            <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Abrir el calendario" />
                        </telerik:RadDatePicker>
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
                        <asp:Label ID="Label6" runat="server" Text="Categoría:" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        &nbsp;
                        <telerik:RadComboBox ID="CmbCategoria" MaxHeight="300px" runat="server" Width="150px">
                        </telerik:RadComboBox>
                    </td>
                   
                    <td colspan="2" Visible="False">
                        <asp:RadioButtonList runat="Server" ID="RblTipoRep" RepeatDirection="Vertical" AutoPostBack="True" Visible="False">
                            <asp:ListItem runat="Server" Value="1" Text="Versión Key" Selected="True"></asp:ListItem>
                            <asp:ListItem runat="Server" Value="2" Text="Versión proveedor"></asp:ListItem>
                        </asp:RadioButtonList>
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
            <table style="font-family: Verdana; font-size: 8pt">
                <tr>
                    <td>
                     <telerik:RadSplitter ID="RadSplitter4" runat="server" Height="350px" BorderSize="0">
                            <telerik:RadPane ID="RadPane3" runat="server" Height="350px" Width="850px" BorderStyle="None">
                             </telerik:RadPane>
                        </telerik:RadSplitter>
                     </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
