<%@ Page Title="Detalle Timbres" Language="C#" MasterPageFile="~/MasterPage/MasterPage03.Master"
    AutoEventWireup="true" CodeBehind="CapPagosTimbresDet.aspx.cs" Inherits="SIANWEB.CapPagosTimbresDet" %>

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
        <table style="font-family: Verdana; font-size: 8pt; height: 100%" width="100%">
            <tr>
                <td>
                    <telerik:radsplitter id="RadSplitter1" runat="server" height="450px" resizemode="AdjacentPane"
                        resizewithbrowserwindow="true" bordersize="0" width="101%">
                                <telerik:RadPane ID="RadPane1" runat="server" Height="400px" OnClientResized="onResize"
                                    BorderStyle="None" >
                    <telerik:radgrid id="RgDetcompl" runat="server" allowpaging="true" autogeneratecolumns="False"
                        gridlines="None" mastertableview-nomasterrecordstext="No se encontraron registros."
                        onitemcommand="RgDet_ItemCommand" onneeddatasource="RgDet_NeedDataSource" onpageindexchanged="RgDet_PageIndexChanged"
                        pagesize="15" onitemcreated="RgDet_ItemCreated" onitemdatabound="RgDet_ItemDataBound">
                    <MasterTableView >
                        <Columns>
                        <telerik:GridButtonColumn CommandName="Enviar" HeaderText="Enviar" Text="Enviar" UniqueName="Enviar"
                            Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png" ButtonCssClass="email_grid">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                        </telerik:GridButtonColumn>                                                     
                        <telerik:GridTemplateColumn HeaderText="PDF" HeaderStyle-HorizontalAlign="Center"
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
                        </telerik:GridTemplateColumn>
                         <telerik:GridTemplateColumn HeaderText="XML Cancel." HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ItemStyle-Width="35px"
                            UniqueName="XMLCancel">
                            <ItemTemplate>
                                <asp:ImageButton ID="xmlCancelacion" runat="server" ImageUrl="~/Imagenes/blank.png"
                                    CssClass="edit" ToolTip="Descargar" CommandName="xmlCancelacion" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="50"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </telerik:GridTemplateColumn>

                        <telerik:GridBoundColumn DataField="Id_Pag" HeaderText="Id_Pag" Display="false" UniqueName="Id_Pag">
                            </telerik:GridBoundColumn>                        
                        <telerik:GridBoundColumn DataField="Id_PagComp" HeaderText="Id_PagComp" Display="false" UniqueName="Id_PagComp">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Id_PagDetTimb" HeaderText="Id_PagDetTimb" Display="false" UniqueName="Id_PagDetTimb">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Id_PagDet" HeaderText="Id_PagDet" Display="false" UniqueName="Id_PagDet">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn DataField="Pago_Serie" HeaderText="Serie" UniqueName="Serie">
                            <ItemTemplate>
                                <asp:Label ID="lblSerie" runat="server" Text='<%# Bind("Pago_Serie") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle Width="60px" />
                        </telerik:GridTemplateColumn>                      
                        <telerik:GridTemplateColumn DataField="Pago_FolioFiscal" HeaderText="Folio Fiscal" UniqueName="Fac_FolioFiscal">
                            <ItemTemplate>
                                <asp:Label ID="LblFolioFiscal" runat="server" Text='<%# Bind("Pago_FolioFiscal") %>'></asp:Label>
                            </ItemTemplate>                                                           
                            <ItemStyle HorizontalAlign="Right" />
                            <HeaderStyle Width="260px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn DataField="Id_Cte" HeaderText="Núm." UniqueName="Id_Cte">
                            <ItemTemplate>
                                <asp:Label ID="lblCte" runat="server" Text='<%# Bind("Id_Cte") %>'></asp:Label>
                            </ItemTemplate>                                                          
                            <ItemStyle HorizontalAlign="Right" />
                            <HeaderStyle Width="60px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn DataField="FechaCreacionXML" HeaderText="Fecha doc. XML" UniqueName="FechaCreacionXML">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFecha" runat="server" Text='<%# Bind("FechaCreacionXML", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <HeaderStyle Width="70px" />
                                                        </telerik:GridTemplateColumn>
                             <telerik:GridTemplateColumn DataField="Estatus" HeaderText="Estatus XML" UniqueName="Estatus">
                            <ItemTemplate>
                                <asp:Label ID="lblestatus" runat="server" Text='<%# Bind("Estatus") %>'></asp:Label>
                            </ItemTemplate>                                                          
                            <ItemStyle HorizontalAlign="Right" />
                            <HeaderStyle Width="60px" />
                        </telerik:GridTemplateColumn>

                        </Columns>
                        <HeaderStyle HorizontalAlign="Center" />
                    </MasterTableView>
                    <PagerStyle NextPagesToolTip="Páginas siguientes" PrevPagesToolTip="Páginas anteriores"
                        FirstPageToolTip="Primera página" LastPageToolTip="Última página" NextPageToolTip="Página siguiente"
                        PageButtonCount="3" PagerTextFormat="Change page: {4} &nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; al &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong &gt;."
                        PageSizeLabelText="Cantidad de registros" PrevPageToolTip="Página anterior" ShowPagerText="True" />
                </telerik:radgrid>
                     </telerik:RadPane>
                            </telerik:radsplitter>
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

    function AbrirVentana_EnviarPagos(Id_Emp, Id_Cd, Id_Cte, Id_Pag, Id_Fac, Id_PagDet, serie, Id_PagtimbDet) {
       
           AbrirVentana_EnviarPagos(Id_Emp, Id_Cd, Id_Cte, Id_Pag, Id_Fac, Id_PagDet, serie, Id_PagtimbDet);
        return false
    }

        function AbrirVentana_EnviarPagos(Id_Emp, Id_Cd, Id_Cte, Id_Pag, Id_Fac, Id_PagDet, serie, Id_PagtimbDet) {
  
            var oWnd = radopen("Ventana_EnviarPagos.aspx?Id_Emp=" + Id_Emp
                        + "&Id_Cd=" + Id_Cd
                        + "&Id_Cte=" + Id_Cte
                        + "&Id_Pag=" + Id_Pag
                        + "&Id_Fac=" + Id_Fac
                        + "&Id_PagDet=" + Id_PagDet
                        + "&Serie=" + serie
                        + "&IDPagTimPag=" + Id_PagtimbDet
                        + "&Tipo=PAGO"
                        , "AbrirVentana_EnviarPagos");
            oWnd.center();
        }

    function AbrirVentana_EnviarDocumentos(Id_Emp, Id_Cd, Id_Fac) {
        var oWnd = radopen("Ventana_EnviarDocumentos.aspx?Id_Emp=" + Id_Emp
                    + "&Id_Cd=" + Id_Cd
                    + "&Id_Doc=" + Id_Fac
                    + "&Tipo=FACTURA"
                    , "AbrirVentana_EnviarDocumentos");
        oWnd.center();
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
