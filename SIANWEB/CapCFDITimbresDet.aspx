<%@ Page Title="Historico de CFDI" Language="C#" MasterPageFile="~/MasterPage/MasterPage03.Master" AutoEventWireup="true" CodeBehind="CapCFDITimbresDet.aspx.cs" Inherits="SIANWEB.CapCFDITimbresDet" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="CapaNegocios" %>
<%@ Import Namespace="CapaEntidad" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">


<telerik:radajaxmanager id="RAM1" runat="server">
<AjaxSettings>
<telerik:AjaxSetting AjaxControlID="RAM1">
<UpdatedControls>
<telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
</UpdatedControls>
</telerik:AjaxSetting>
<telerik:AjaxSetting AjaxControlID="RgDetcompl">
<UpdatedControls>
<telerik:AjaxUpdatedControl ControlID="RgDetcompl" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelHeight="" />
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
                    &nbsp;
                </td>
                <td width="150px">
                    &nbsp;
                </td>
            </tr>
        </table>
        <table style="font-family: Verdana; font-size: 8pt; height: 90%" width="90%" border ="0">
            <tr>
                <td>
                    <telerik:radgrid id="RgDetcompl" runat="server" allowpaging="true" autogeneratecolumns="False"
                        gridlines="None" mastertableview-nomasterrecordstext="No se encontraron registros."
                        onitemcommand="RgDet_ItemCommand" onneeddatasource="RgDet_NeedDataSource" onpageindexchanged="RgDet_PageIndexChanged"
                        pagesize="10" onitemcreated="RgDet_ItemCreated" onitemdatabound="RgDet_ItemDataBound">
                    <MasterTableView >
                        <Columns>
                            <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Sucursal" Display="true" UniqueName="Id_Cd">
                                <HeaderStyle HorizontalAlign="Center" Width="25"></HeaderStyle>
                                </telerik:GridBoundColumn>
                                
                            <telerik:GridBoundColumn DataField="Id_Doc" HeaderText="Documento" Display="true" UniqueName="Id_Doc">
                                <HeaderStyle HorizontalAlign="Center" Width="25"></HeaderStyle>
                                </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Id_CFDI" HeaderText="CFDI" UniqueName="Id_CFDI" Display="true">
                                <HeaderStyle HorizontalAlign="Center" Width="25"></HeaderStyle>
                                </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="CFDI_FolioFiscal" HeaderText="Folio Fiscal" Display="true" UniqueName="CFDI_FolioFiscal">
                                <HeaderStyle HorizontalAlign="Center" Width="250"></HeaderStyle>
                                </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="CFDI_Estatus" HeaderText="Estatus" Display="true" UniqueName="CFDI_Estatus">
                                <HeaderStyle HorizontalAlign="Center" Width="35"></HeaderStyle>
                            </telerik:GridBoundColumn>
                         <telerik:GridTemplateColumn HeaderText="XML Cancel" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px"
                            UniqueName="XMLCancel">
                            <ItemTemplate>
                                <asp:ImageButton ID="xmlCancelacion" runat="server" ImageUrl="~/Imagenes/blank.png"
                                    CssClass="edit" ToolTip="Descargar" CommandName="xmlCancelacion" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </telerik:GridTemplateColumn>

           

                        </Columns>
                        <HeaderStyle HorizontalAlign="Center" />
                    </MasterTableView>
                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                        FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                        PageButtonCount="3" PagerTextFormat="Change page: {4} &nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                        PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
                </telerik:radgrid>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="HiddenRebind" runat="server" />
    <asp:HiddenField ID="HF_PageName" runat="server" />
    <asp:HiddenField ID="HF_ID" runat="server" />
    <asp:HiddenField ID="HF_Timbrado" runat="server" />
    <asp:HiddenField ID="HF_FechaPago" runat="server" />
    <asp:HiddenField ID="HF_Serie" runat="server" />
    <asp:HiddenField ID="HF_IDFac" runat="server" />
    <asp:HiddenField ID="HiddenHeight" runat="server" />
    <asp:HiddenField ID="HF_CTE" runat="server" />
    <asp:HiddenField ID="HF_RegPga" runat="server" />
    <asp:HiddenField ID="clientSideIsPostBack" runat="server" Value="N" />
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

    
    function AbrirFacturaPDF(WebURL) {
        var oWnd = radopen(WebURL, "AbrirVentana_ImpresionPDFFactura");
        oWnd.set_showOnTopWhenMaximized(false);
        oWnd.center();
    }

    function AbrirFacturaPDFVarias(WebURL) {
        window.open(WebURL, "_blank");
    }

    function abrirArchivoCN(pagina, paginaCN) {
        var opciones = "toolbar=yes, location=yes, directories=yes, status=yes, menubar=yes, scrollbars=yes, resizable=yes, width=508, height=365, top=100, left=140";
        window.open(pagina, 'XML', opciones);
        window.open(paginaCN, 'XML Cuenta Nacional', opciones);
    }

    function abrirArchivo(pagina) {
        var opciones = "toolbar=yes, location=yes, directories=yes, status=yes, menubar=yes, scrollbars=yes, resizable=yes, width=508, height=365, top=100, left=140";
        window.open(pagina, '', opciones);
    }  
</script>
</telerik:radcodeblock>
</asp:Content>
