<%@ Page Title="Subrogación" Language="C#" MasterPageFile="~/MasterPage/MasterPage03.Master"
    AutoEventWireup="true" CodeBehind="CapPagoSubrogacion.aspx.cs" Inherits="SIANWEB.CapPagoSubrogacion" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="CapaNegocios" %>
<%@ Import Namespace="CapaEntidad" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:radajaxmanager id="RAM1" runat="server">
       <AjaxSettings>        
            <telerik:AjaxSetting  AjaxControlID="CmbCentro">
            <updatedcontrols>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" 
                        LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
                </updatedcontrols>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnTimbrar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
              <telerik:AjaxSetting AjaxControlID="btnConfirm">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:radajaxmanager>
    <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server" skin="Default">
    </telerik:radajaxloadingpanel>
    <div id="divPrincipal" runat="server">
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
                    <telerik:radcombobox id="CmbCentro" maxheight="300px" runat="server" width="150px"
                        autopostback="True">
                    </telerik:radcombobox>
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
                            <td width="80">
                            </td>
                            <td width="100">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8">
                                <asp:Label ID="Label1" runat="server" Text="Favor de ingresar la fecha del documento que entrego el cliente" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Fecha" />
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
                            </td>
                        </tr>
                        <tr>
                            <asp:ImageButton ID="btnTimbrar" runat="server" ImageUrl="~/images/check.png" OnClick="Timbrar_Click"
                                ToolTip="Timbrar" />
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:Button ID="btnConfirm" runat="server" CssClass="buttons" Text="Button" OnClick="Button1_Click" />
        <asp:HiddenField ID="hdnValor" runat="server" Value="" />
        <asp:HiddenField ID="hdnIdEmp" runat="server" Value="" />
        <asp:HiddenField ID="hdnIdCd" runat="server" Value="" />
        <asp:HiddenField ID="hdnIdPag" runat="server" Value="" />
        <asp:HiddenField ID="hdnCte" runat="server" Value="" />
        <asp:HiddenField ID="hdnIdPagDet" runat="server" Value="" />
        <asp:HiddenField ID="hdnfac" runat="server" Value="" />
    </div>
    <style type="text/css">
        .buttons
        {
            display: none;
        }
    </style>
    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function confirmCallBackFn(arg) {
            if (arg == true) {
                callConfirm();
            }
        }

        function callConfirm() {
            var nombre = document.getElementById('<%= hdnValor.ClientID %>').value;
            radconfirm('Se genera REP con fecha de ' + nombre + ' </br> ¿Es correcto?', callexit, 350, 150);
        }

        function callexit(arg) {
            if (arg == true) {
                document.getElementById('<%= btnConfirm.ClientID %>').click();
            }
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
</script>
</telerik:radcodeblock>
</asp:Content>
