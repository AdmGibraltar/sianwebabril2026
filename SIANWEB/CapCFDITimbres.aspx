<%@ Page Title="Administrador de CFDI" Language="C#" MasterPageFile="~/MasterPage/MasterPage02.Master" AutoEventWireup="true" CodeBehind="CapCFDITimbres.aspx.cs" Inherits="SIANWEB.CapCFDITimbres" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH" runat="server">
    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function onResize(sender, eventArgs) {
                var postback = document.getElementById("<%=clientSideIsPostBack.ClientID %>").value;
                document.getElementById("<%= HiddenHeight.ClientID %>").value = document.documentElement.clientHeight;
                ajaxManager.ajaxRequest('panel');
            }


            function AbrirVentana_CFDITimbreDet(Id_Emp, Id_Cd, Id_Doc, TipoDoc) {
                AbrirVentana_CFDITimbreDet(Id_Emp, Id_Cd, Id_Doc, TipoDoc);
                return false
            }


            function confirmMetodoCallBackFn(arg) {
                if (arg == true) {
                    callmetodoConfirm();
                }
            }

            function callMetodoexit(arg) {
                if (arg == true) {
                    document.getElementById('<%= btnConfirm.ClientID %>').click();
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

            function CloseWindow(mensaje) {
                var cerrarWindow = radalert(mensaje, 350, 150, tituloMensajes);
                cerrarWindow.add_close(
                    function () {
                        CloseAndRebind();
                        RefreshParentPage();
                    });
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

            function AbrirCFDITrasladoPDF(WebURL) {
                var oWnd = window.radopen(WebURL, 'AbrirVentana_ImpresionPDFCFDITraslado');
                oWnd.center();
            }
            function abrirArchivo(pagina) {
                debugger;
                var opciones = "toolbar=yes, location=yes, directories=yes, status=yes, menubar=yes, scrollbars=yes, resizable=yes, width=508, height=365, top=100, left=140";
                window.open(pagina, '', opciones);
            }

            //function AbrirVentana_CFDITraslado(Id_Rem) {
            //    AbrirVentana_CFDITraslado(Id_Rem);
            //    return false;
            //}

            //RBM 22112021  CFDI TRASLADO
            function AbrirVentana_CFDITraslado(Id_Doc, Id_Cte, Nombre, CodigoPostal, Colonia, TipoDoc) {
                AbrirVentana_CFDITraslado(Id_Doc, Id_Cte, Nombre, CodigoPostal, Colonia, TipoDoc);
                return false;
            }

            function AbrirVentana_CFDITraslado(Id_Doc, Id_Cte, Nombre, CodigoPostal, Colonia, TipoDoc) {
                var oWnd = radopen("CapCartaPorte.aspx?Id_Doc=" + Id_Doc
                    + "&Id_Cte=" + Id_Cte
                    + "&Nombre=" + Nombre
                    + "&CodigoPostal=" + CodigoPostal
                    + "&Colonia=" + Colonia
                    + "&TipoDoc=" + TipoDoc
                    , "AbrirVentana_CFDITraslado");
                oWnd.center();
                oWnd.Maximize();
            }

            function AbrirVentana_CFDITimbreDet(Id_Emp, Id_Cd, Id_Doc, TipoDoc) {
                AbrirVentana_CFDITimbreDet(Id_Emp, Id_Cd, Id_Doc, TipoDoc);
                return false;
            }

            function AbrirVentana_CFDITimbreDet(Id_Emp, Id_Cd, Id_Doc, TipoDoc) {
                var oWnd = radopen("CapCFDITimbresDet.aspx?Id_Emp=" + Id_Emp
                    + "&Id_Cd=" + Id_Cd
                    + "&Id_Doc=" + Id_Doc
                    + "&TipoDoc=" + TipoDoc
                    , "AbrirVentana_CFDITimbreDet");
                oWnd.center();
                oWnd.Maximize();
            }

            //End
        </script>
    </telerik:radcodeblock>        
  

    <telerik:radajaxmanager id="RAM1" runat="server" onajaxrequest="RAM1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RAM1">
             <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divPrincipal" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelHeight="" />
                         <telerik:AjaxUpdatedControl ControlID="RgDet" UpdatePanelHeight="" />
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
        <table style="font-family: Verdana; font-size: 8pt;">
            <tr>
                <td>
                    <asp:Label ID="lblDocumento" runat="server" Text="Documento" />
                </td>
                <td>
                    <telerik:radnumerictextbox id="txtdocumento" runat="server" width="70px" enabled="false"
                        maxlength="9" minvalue="1">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                                    </telerik:radnumerictextbox>
                </td>
                <td>
                    <asp:Label ID="lblTipoDoc" runat="server" Text="Tipo Documento" />
                </td>
                <td>

                     <asp:TextBox ID="txtTipoDoc" runat="server" Enabled="false" Width="250px"></asp:TextBox>

                </td>
             </tr>
             <tr>
                 <td>
                    <asp:Label ID="lblCliente" runat="server" Text="Cliente" />
                </td>
                <td>
                    <telerik:radnumerictextbox id="txtCliente" runat="server" width="70px" enabled="false"
                        maxlength="9" minvalue="1">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                                    </telerik:radnumerictextbox>
                </td>
                 <td>
                     <asp:Label ID="lblNombre" runat="server" Text="Nombre" />
                 </td>
                 <td>
                     <asp:TextBox ID="txtNombre" runat="server" Enabled="true" Width="350px"></asp:TextBox>
                 </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCodigoPostal" runat="server" Text="Código Postal" />
                </td>
                <td>
                    <telerik:radnumerictextbox id="txtcp" runat="server" width="70px" enabled="true" maxlength="5" 
                        AutoPostBack="true" OnTextChanged="txtcp_TextChanged" EmptyMessage="52000">
                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        <ClientEvents OnKeyPress="handleClickEvent" />
                                                    </telerik:radnumerictextbox>
                </td>
                <td>
                    <asp:Label ID="lblColonia" runat="server" Text="Colonia" />
                </td>
                <td>
                    <telerik:RadComboBox ID="CmbColonia" runat="server" width="350px" filter="Contains" AutoPostBack="true"
                        changetextonkeyboardnavigation="true" markfirstmatch="true" onclientblur="Combo_ClientBlur"
                        datatextfield="Descripcion" datavaluefield="Id" enableloadondemand="true" highlighttemplateditems="true"
                        loadingmessage="Cargando..." CssClass="auto-style1" Readonly="true">
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
                    </telerik:RadComboBox>
                </td>
                <td>
  
                </td>
                
            </tr>
        </table>
    
        <table style="font-family: Verdana; font-size: 8pt; height: 80%" width="75%">
            <tr>
                <td>
                </td>
                <td>
                    NOTA: El código postal y colonia son importantes para generar CFDI con o sin Carta porte, estos valores se cargan del catálogo del cliente en las direcciones de entrega,
                    si estos campos se encuentran vacios favor de seleccionar el correspondiente para que el sistema pueda continuar.
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <telerik:RadGrid ID="RgDet" runat="server" AllowPaging="False" AutoGenerateColumns="False"  
                        GridLines="None" MasterTableView-NoMasterRecordsText="No se encontraron registros."
                        OnItemCommand="RgDet_ItemCommand" OnNeedDataSource="RgDet_NeedDataSource" OnPageIndexChanged="RgDet_PageIndexChanged"
                        PageSize="5" OnItemCreated="RgDet_ItemCreated" OnItemDataBound="RgDet_ItemDataBound" >
                        <MasterTableView >
                            <Columns>

                        <%--    <telerik:GridButtonColumn CommandName="Historial" HeaderText="Historial" ConfirmDialogType="RadWindow"
                                ConfirmDialogHeight="150px" ConfirmDialogWidth="450px" Text="Historial de CFDI" UniqueName="Historial"
                                Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png" ButtonCssClass="edit">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="20"></HeaderStyle>
                            </telerik:GridButtonColumn>--%>

                            <telerik:GridBoundColumn DataField="Id_Cd" HeaderText="Sucursal" Display="false" UniqueName="Id_Cd">
                                <HeaderStyle HorizontalAlign="Center" Width="25"></HeaderStyle>
                                </telerik:GridBoundColumn>
                                
                            <telerik:GridBoundColumn DataField="Id_Doc" HeaderText="Documento" Display="true" UniqueName="Id_Doc">
                                <HeaderStyle HorizontalAlign="Center" Width="25"></HeaderStyle>
                                </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Id_CFDI" HeaderText="CFDI" UniqueName="Id_CFDI" Display="true">
                                <HeaderStyle HorizontalAlign="Center" Width="15"></HeaderStyle>
                                </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="CFDI_FolioFiscal" HeaderText="Folio Fiscal" Display="true" UniqueName="CFDI_FolioFiscal">
                                <HeaderStyle HorizontalAlign="Center" Width="100"></HeaderStyle>
                                </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="CFDI_Estatus" HeaderText="Estatus" Display="false" UniqueName="CFDI_Estatus">
                                <HeaderStyle HorizontalAlign="Center" Width="35"></HeaderStyle>
                            </telerik:GridBoundColumn>

                            <%--<telerik:GridButtonColumn CommandName="Cancelar" HeaderText="Cancelación Fiscal" ConfirmDialogType="RadWindow"
                                ConfirmText="¿Está seguro desea cancelar el CFDI de Traslado?</br></br>" ConfirmDialogHeight="150px"
                                ConfirmDialogWidth="350px" Text="Cancelar" UniqueName="Cancelar" Visible="True" ButtonType="ImageButton"
                                ImageUrl="~/Imagenes/blank.png" ButtonCssClass="baja" Display="true">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="30px"></HeaderStyle>
                            </telerik:GridButtonColumn>--%>

                            <telerik:GridButtonColumn CommandName="PDF" HeaderText="Descargar Pdf" ConfirmDialogType="RadWindow"  
                                ConfirmDialogHeight="150px" ConfirmDialogWidth="450px" Text="Descargar Pdf de CFDI" UniqueName="Pdf"
                                Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png" ButtonCssClass="edit">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="15"></HeaderStyle>
                            </telerik:GridButtonColumn>

                            <telerik:GridButtonColumn CommandName="XML" HeaderText="Descargar Xml" ConfirmDialogType="RadWindow"
                                ConfirmDialogHeight="150px" ConfirmDialogWidth="450px" Text="Descargar Xml de CFDI" UniqueName="Xml"
                                Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png" ButtonCssClass="edit">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="15"></HeaderStyle>
                            </telerik:GridButtonColumn>
    

                                 <telerik:GridButtonColumn CommandName="CFDI" HeaderText="Generar Traslado" ConfirmDialogType="RadWindow"
                                Text="CFDI" UniqueName="CFDI" Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png" ButtonCssClass="edit">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="15"></HeaderStyle>
                                </telerik:GridButtonColumn>

                                 <telerik:GridButtonColumn CommandName="CartaPorte" HeaderText="Generar Carta Porte" ConfirmDialogType="RadWindow" 
                                Text="Carta Porte" UniqueName="CartaPorte" Visible="True" ButtonType="ImageButton" ImageUrl="~/Imagenes/blank.png" ButtonCssClass="edit">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="20"></HeaderStyle>
                                </telerik:GridButtonColumn>

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
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                   
                </td>
            </tr>
        </table>
        <asp:Button ID="btnConfirm" runat="server" CssClass="buttonsHiden" Text="Button" OnClick="Button2_Click" Visible="false"/>
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

   
</asp:Content>

